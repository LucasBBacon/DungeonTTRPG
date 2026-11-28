using System;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;

namespace Dungeon.Core.AbilityModel;

/// <summary>
/// Ability use outside resolution (M1-11 plan calls 2 to 5): the units an ability affects, and the two validation
/// stages of AB 14.3. Every check reads the <em>view</em> the command acts on: in combat the stored state; out of combat
/// the tick-reset view (every budget fresh, every <see cref="RefillTag.Turn"/> pool refilled; AB 6.8), because a use
/// out of combat runs on a new tick as a step does.
/// </summary>
public static partial class AbilityUse
{
    // AB 14.2: one action a turn.
    private const int ActionsPerTurn = 1;

    /// <summary>
    /// The squares and units <paramref name="ability"/> used by <paramref name="user"/> on <paramref name="target"/>
    /// covers and affects, on <paramref name="s"/> as given (no view is taken). For a single-target ability: no squares,
    /// and the listed units. For an area: <see cref="Areas.Covered"/> (a burst centred on the point, a cone or line from
    /// the user's square along the aim), and the units standing on those squares, less the user (AB 8.2: "A caster is
    /// never affected by its own casts' areas"), and less the user's own faction when the ability is
    /// <see cref="TargetSpec.Selective"/> or <see cref="AbilityKind.AtWill"/> ("At-wills never hit allies"). Units are in
    /// id order. The preview and resolution read the same function. Throws <see cref="ArgumentException"/> for an unknown
    /// user, an ability with no targeting, or a target of the wrong shape.
    /// </summary>
    public static (ImmutableArray<GridPos> Squares, ImmutableArray<UnitId> Units) Affected(
        RulesContext rules, GameState s, UnitId user, AbilityDefinition ability, AbilityTarget target)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(ability);
        var holder = s.FindUnit(user) ?? throw new ArgumentException($"Unknown unit {user.Value}.", nameof(user));
        var spec = ability.Target ?? throw new ArgumentException($"Ability \"{ability.Id}\" has no targeting.", nameof(ability));

        if (spec.Shape == TargetShape.Single)
        {
            var listed = target as UnitsTarget ?? throw WrongShape(ability, target);
            ImmutableArray<UnitId> ids = listed.Units.IsDefault ? [] : listed.Units;
            var sorted = ImmutableArray.CreateBuilder<UnitId>(ids.Length);
            foreach (var unit in s.Units)
            {
                if (ids.Contains(unit.Id))
                {
                    sorted.Add(unit.Id);
                }
            }

            return (ImmutableArray<GridPos>.Empty, sorted.ToImmutable());
        }

        var squares = spec.Shape switch
        {
            TargetShape.Burst => Areas.Covered(
                s.Map, new Burst(spec.Size), (target as PointTarget ?? throw WrongShape(ability, target)).Point, null),
            TargetShape.Cone => Areas.Covered(s.Map, new Cone(spec.Size), holder.Pos, Aim(ability, target)),
            TargetShape.Line => Areas.Covered(s.Map, new Line(spec.Size), holder.Pos, Aim(ability, target)),
            _ => throw new ArgumentException($"Ability \"{ability.Id}\" has an unknown shape.", nameof(ability)),
        };

        bool spareAllies = spec.Selective || ability.Kind == AbilityKind.AtWill;
        var units = ImmutableArray.CreateBuilder<UnitId>();
        foreach (var unit in s.Units)
        {
            if (unit.Id != user && !(spareAllies && unit.Faction == holder.Faction) && squares.Contains(unit.Pos))
            {
                units.Add(unit.Id);
            }
        }

        return (squares, units.ToImmutable());
    }

    /// <summary>
    /// The action-bar stage (AB 15.4; plan call 4): the reasons that do not depend on a target, in AB 14.3's order. The
    /// first failure: <see cref="CommandError.UnknownUnit"/>; <see cref="CommandError.UnknownAbility"/> (the unit does not
    /// hold it, or the rules lack it); <see cref="CommandError.ReactionOnly"/> (a reaction is never used by command);
    /// <see cref="CommandError.NotYourTurn"/> (in combat, unless the user holds a live turn); the cost
    /// (<see cref="CommandError.NoActionLeft"/>, <see cref="CommandError.NoBonusActionLeft"/>,
    /// <see cref="CommandError.NoFreeInteraction"/>, <see cref="CommandError.NoMovementLeft"/>); then the pool spend
    /// (<see cref="Pools.CheckSpend"/>: <see cref="CommandError.NoUsesLeft"/>, then
    /// <see cref="CommandError.CampSpendUsed"/>). Otherwise <see cref="CommandError.None"/>. Out of combat it reads the
    /// tick-reset view.
    /// </summary>
    public static CommandError CheckUsable(RulesContext rules, GameState s, UnitId user, string abilityId)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        return Usable(rules, View(rules, s), user, abilityId);
    }

    /// <summary>
    /// Both stages (plan call 4): <see cref="CheckUsable"/>, then the target-dependent checks in AB 14.3's order. The
    /// target shape (<see cref="CommandError.InvalidTarget"/>): <see cref="UnitsTarget"/> only for
    /// <see cref="TargetShape.Single"/>, with 1 to the ability's count of distinct living units, none of them the user;
    /// <see cref="PointTarget"/> only for <see cref="TargetShape.Burst"/>, in bounds and not a wall;
    /// <see cref="AimTarget"/> only for <see cref="TargetShape.Cone"/> and <see cref="TargetShape.Line"/>, one of the
    /// eight directions. Then the range (<see cref="CommandError.OutOfRange"/>: Chebyshev from the user to each unit or
    /// to the point) and the sight (<see cref="CommandError.NoLineOfSight"/>: <see cref="Sight.CanSee"/> from the user's
    /// square). Last, out of combat: a use that affects a unit of another faction is
    /// <see cref="CommandError.InvalidTarget"/> until openers arrive (M1-11 Task 6), and a use that draws (it has a
    /// <c>resolve</c>, or dice in its damage) is <see cref="CommandError.NotInCombat"/> (plan call 5; D-23). The command
    /// refuses with this code.
    /// </summary>
    public static CommandError CheckTarget(
        RulesContext rules, GameState s, UnitId user, string abilityId, AbilityTarget target)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        return Targetable(rules, View(rules, s), user, abilityId, target);
    }

    /// <summary>The state a use is validated and resolved on: <paramref name="s"/> in combat, the tick-reset view out of it.</summary>
    internal static GameState View(RulesContext rules, GameState s) => s.CombatActive ? s : Engine.TickResets(rules, s);

    /// <summary><see cref="CheckUsable"/> on a view already taken.</summary>
    internal static CommandError Usable(RulesContext rules, GameState s, UnitId user, string abilityId)
    {
        var unit = s.FindUnit(user);
        if (unit is null)
        {
            return CommandError.UnknownUnit;
        }

        var ability = abilityId is null || !unit.Abilities.Contains(abilityId, StringComparer.Ordinal)
            ? null
            : rules.FindAbility(abilityId);
        if (ability is null)
        {
            return CommandError.UnknownAbility;
        }

        if (ability.Cost.Kind == CostKind.Reaction || ability.Kind == AbilityKind.Reaction)
        {
            return CommandError.ReactionOnly;
        }

        if (s.Combat is { } combat && (combat.BeforeRound1 || combat.TurnVacated || combat.CurrentUnit != user))
        {
            return CommandError.NotYourTurn;
        }

        var budget = unit.Budget;
        var cost = ability.Cost;
        var error = cost.Kind switch
        {
            CostKind.Action => budget.ActionsUsed >= ActionsPerTurn ? CommandError.NoActionLeft : CommandError.None,
            CostKind.Bonus => budget.BonusUsed ? CommandError.NoBonusActionLeft : CommandError.None,
            CostKind.Free => budget.FreeUsed ? CommandError.NoFreeInteraction : CommandError.None,
            CostKind.Move => Turns.MovementLeft(rules, s, user) < cost.Move ? CommandError.NoMovementLeft : CommandError.None,
            CostKind.None => CommandError.None,
            _ => throw new InvalidOperationException(
                $"Ability \"{ability.Id}\" has cost {cost.Kind}, which M1-11 cannot pay (M1-14, M1-16, M1-20)."),
        };
        if (error != CommandError.None)
        {
            return error;
        }

        return ability.Pool is { } pool ? Pools.CheckSpend(rules, s, user, pool.PoolId, pool.Count) : CommandError.None;
    }

    /// <summary><see cref="CheckTarget"/> on a view already taken.</summary>
    internal static CommandError Targetable(
        RulesContext rules, GameState s, UnitId user, string abilityId, AbilityTarget target)
    {
        var usable = Usable(rules, s, user, abilityId);
        if (usable != CommandError.None)
        {
            return usable;
        }

        var holder = s.FindUnit(user)!;
        var ability = rules.Ability(abilityId);
        var spec = ability.Target;
        if (spec is null || !ShapeFits(s, holder, spec, target))
        {
            return CommandError.InvalidTarget;
        }

        // Range, then sight, to each named unit or to the point; cones and lines start at the user and have neither.
        var aimed = Aimed(s, target);
        foreach (var at in aimed)
        {
            if (GridPos.Distance(holder.Pos, at) > spec.Range)
            {
                return CommandError.OutOfRange;
            }
        }

        foreach (var at in aimed)
        {
            if (!Sight.CanSee(s.Map, holder.Pos, at))
            {
                return CommandError.NoLineOfSight;
            }
        }

        if (!s.CombatActive)
        {
            // Plan call 5: a use that affects another faction is an opener; Task 6 starts combat for it.
            foreach (var id in Affected(rules, s, user, ability, target).Units)
            {
                if (s.FindUnit(id)!.Faction != holder.Faction)
                {
                    return CommandError.InvalidTarget;
                }
            }

            if (Draws(ability))
            {
                return CommandError.NotInCombat;
            }
        }

        return CommandError.None;
    }

    /// <summary>
    /// Whether resolving <paramref name="ability"/> draws: it has a <c>resolve</c> (an attack roll or a save) or dice in
    /// its damage. Out of combat such a use has no stream (plan call 5; D-23).
    /// </summary>
    internal static bool Draws(AbilityDefinition ability)
    {
        if (ability.Resolve is not null)
        {
            return true;
        }

        foreach (var part in ability.Damage)
        {
            if (part.Dice > 0)
            {
                return true;
            }
        }

        return false;
    }

    private static bool ShapeFits(GameState s, Unit user, TargetSpec spec, AbilityTarget target)
    {
        switch (spec.Shape, target)
        {
            case (TargetShape.Single, UnitsTarget listed):
                var units = listed.Units;
                if (units.IsDefaultOrEmpty || units.Length > spec.Count)
                {
                    return false;
                }

                for (int i = 0; i < units.Length; i++)
                {
                    if (units[i] == user.Id || s.FindUnit(units[i]) is not { Hp: > 0 } || units.IndexOf(units[i]) != i)
                    {
                        return false;
                    }
                }

                return true;

            case (TargetShape.Burst, PointTarget point):
                return s.Map.InBounds(point.Point) && s.Map[point.Point] != Tile.Wall;

            case (TargetShape.Cone or TargetShape.Line, AimTarget aim):
                return Enum.IsDefined(aim.Aim);

            default:
                return false;
        }
    }

    // The squares range and sight are measured to: each named unit's, or the point. None for an aim.
    private static ImmutableArray<GridPos> Aimed(GameState s, AbilityTarget target) => target switch
    {
        UnitsTarget listed => [.. listed.Units.Select(id => s.FindUnit(id)!.Pos)],
        PointTarget point => [point.Point],
        _ => [],
    };

    private static Direction Aim(AbilityDefinition ability, AbilityTarget target) =>
        (target as AimTarget ?? throw WrongShape(ability, target)).Aim;

    private static ArgumentException WrongShape(AbilityDefinition ability, AbilityTarget target) =>
        new($"Target {target?.GetType().Name ?? "null"} does not fit ability \"{ability.Id}\".", nameof(target));
}
