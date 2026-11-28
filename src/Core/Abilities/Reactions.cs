using System;
using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.AbilityModel;

/// <summary>
/// A unit's policy override for one of its reactions (AB 7.3: "Policies are set per unit, per reaction, and last for the
/// run"; M1-11 plan call 13). A unit's settings are held sorted by <see cref="AbilityId"/> in ordinal order, each once.
/// </summary>
public sealed record ReactionSetting(string AbilityId, ReactionMode Mode);

/// <summary>
/// Reactions in M1-11 (plan call 11; AB 7.3, 14.3): bonus wards on an attack roll against the holder or on the holder's
/// own save. A window walks the defender's own reaction abilities with the window's trigger in ability-id order; an
/// illegal candidate is skipped, a legal one is judged by its policy, and the first that fires closes the window. A ward
/// resolves no pipeline, so reactions cannot nest.
/// </summary>
public static class Reactions
{
    /// <summary>
    /// The policy <paramref name="unit"/>'s reaction <paramref name="abilityId"/> follows: the unit's override if it has
    /// one, else the data default (a record with no policy is treated as <see cref="ReactionMode.Never"/>; content cannot
    /// load one, <c>content.ABL145.ReactionHasPolicy</c>). Throws <see cref="ArgumentException"/> for an unknown unit, an
    /// unknown ability, or an ability that is not a reaction.
    /// </summary>
    public static ReactionMode Mode(RulesContext rules, GameState s, UnitId unit, string abilityId)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(abilityId);
        var holder = s.FindUnit(unit) ?? throw new ArgumentException($"Unit {unit.Value} is not in the state.", nameof(unit));
        var spec = rules.Ability(abilityId).Reaction
            ?? throw new ArgumentException($"Ability \"{abilityId}\" is not a reaction.", nameof(abilityId));
        return ModeOf(holder, abilityId, spec);
    }

    /// <summary>
    /// The window for <paramref name="trigger"/> on <paramref name="defender"/>, opened after the d20 is drawn and before
    /// the outcome is decided (plan calls 10 to 12). Candidates are the abilities the defender holds that are reactions
    /// with that trigger, in ability-id order, starting after <paramref name="after"/> when it is given (the candidate an
    /// "ask me" answer declined). Each is checked for legality first: the reaction unused this turn, and its pool, if
    /// any, spendable (<see cref="Pools.CheckSpend"/>: enough left, and the camp rule); an illegal candidate is skipped,
    /// not judged and not spent. A legal one on <c>askMe</c> (a party unit's only; AB 7.3) stops the walk and is returned
    /// as <c>Ask</c>, with the state and setup untouched: the caller suspends the use on it. Otherwise it fires by its
    /// policy (<see cref="Mode"/>): <c>always</c>; <c>never</c> does not; <c>whilePoolAtLeastHalf</c> while the pool's
    /// current is at least <c>ceil(max / 2)</c>; <c>ifItChangesOutcome</c> when the bonus, judged on
    /// <paramref name="roll"/> already drawn, turns a hit into a miss or a failed save into a success. Firing is
    /// <see cref="Take"/> and closes the window. Draws nothing and raises no event: the caller raises
    /// <see cref="ReactionTaken"/> for the fired id, or the decision for the asked one.
    /// </summary>
    internal static (string? Fired, string? Ask, ContestSetup Setup, GameState Next) Window(
        RulesContext rules, GameState s, UnitId defender, ReactionTrigger trigger, ContestSetup setup, D20Roll roll,
        string? after = null)
    {
        var holder = s.FindUnit(defender)
            ?? throw new InvalidOperationException($"Unit {defender.Value} is not in the state; no window can open on it.");
        foreach (var id in holder.Abilities)
        {
            if (after is not null && string.CompareOrdinal(id, after) <= 0)
            {
                continue;
            }

            if (rules.FindAbility(id) is not { Reaction: { } spec } ability || spec.Trigger != trigger)
            {
                continue;
            }

            if (!Legal(rules, s, holder, ability))
            {
                continue;
            }

            if (ModeOf(holder, id, spec) == ReactionMode.AskMe && holder.Faction == Faction.Party)
            {
                return (null, id, setup, s);
            }

            if (!Fires(rules, s, holder, ability, spec, setup, roll))
            {
                continue;
            }

            var (fired, next) = Take(rules, s, defender, id, trigger, setup);
            return (id, null, fired, next);
        }

        return (null, null, setup, s);
    }

    /// <summary>
    /// Fires <paramref name="defender"/>'s reaction <paramref name="abilityId"/> at the <paramref name="trigger"/> window
    /// (plan calls 11 and 12): sets <see cref="TurnBudget.ReactionUsed"/>, pays the pool through <see cref="Pools.Spend"/>
    /// (which sets <see cref="TurnBudget.CampSpent"/> for a camp pool) and adds the bonus to <paramref name="setup"/> (the
    /// attack's target, or the save's bonus). Throws <see cref="InvalidOperationException"/> when the defender is gone,
    /// does not hold the ability, the ability is not a reaction with that trigger, or it is not legal now. Draws nothing
    /// and raises no event.
    /// </summary>
    internal static (ContestSetup Setup, GameState Next) Take(
        RulesContext rules, GameState s, UnitId defender, string abilityId, ReactionTrigger trigger, ContestSetup setup)
    {
        var unit = s.FindUnit(defender)
            ?? throw new InvalidOperationException($"Unit {defender.Value} is not in the state; it cannot react.");
        if (unit.Abilities.IndexOf(abilityId, StringComparer.Ordinal) < 0
            || rules.FindAbility(abilityId) is not { Reaction: { } spec } ability
            || spec.Trigger != trigger)
        {
            throw new InvalidOperationException($"Unit {defender.Value} holds no {trigger} reaction \"{abilityId}\".");
        }

        if (!Legal(rules, s, unit, ability))
        {
            throw new InvalidOperationException($"Unit {defender.Value} cannot take \"{abilityId}\" now: it is used or its pool cannot pay.");
        }

        s = s with
        {
            Units = s.Units.SetItem(s.Units.IndexOf(unit), unit with { Budget = unit.Budget with { ReactionUsed = true } }),
        };
        if (ability.Pool is { } pool)
        {
            var (ok, spent) = Pools.Spend(rules, s, defender, pool.PoolId, pool.Count);
            if (!ok)
            {
                throw new InvalidOperationException($"Unit {defender.Value} cannot pay the pool of \"{abilityId}\"; legality should have skipped it.");
            }

            s = spent;
        }

        return (WithBonus(setup, trigger, spec.Bonus), s);
    }

    // AB 7.3 and armour 10.4: the reaction unused, and its pool spendable under the camp rule.
    private static bool Legal(RulesContext rules, GameState s, Unit holder, AbilityDefinition ability) =>
        !holder.Budget.ReactionUsed
        && (ability.Pool is not { } pool || Pools.CheckSpend(rules, s, holder.Id, pool.PoolId, pool.Count) == CommandError.None);

    private static bool Fires(
        RulesContext rules, GameState s, Unit holder, AbilityDefinition ability, ReactionSpec spec, ContestSetup setup, D20Roll roll)
    {
        switch (ModeOf(holder, ability.Id, spec))
        {
            case ReactionMode.Always:
                return true;

            case ReactionMode.WhilePoolAtLeastHalf:
                if (ability.Pool is not { } pool)
                {
                    return false;
                }

                long max = Pools.Max(rules, s, holder.Id, pool.PoolId);
                return Pools.Current(rules, s, holder.Id, pool.PoolId) >= IntMath.FloorDiv(checked(max + 1), 2);

            case ReactionMode.IfItChangesOutcome:
                var with = WithBonus(setup, spec.Trigger, spec.Bonus);
                return spec.Trigger == ReactionTrigger.AttackRoll
                    ? Contests.ResolveAttack(setup, roll).Hit && !Contests.ResolveAttack(with, roll).Hit
                    : !Contests.ResolveCheck(setup, roll).Success && Contests.ResolveCheck(with, roll).Success;

            // "Ask me" never fires on its own: Window asks a party unit first, and a non-party unit has no prompt.
            case ReactionMode.AskMe:
            case ReactionMode.Never:
            default:
                return false;
        }
    }

    private static ContestSetup WithBonus(ContestSetup setup, ReactionTrigger trigger, int bonus) =>
        trigger == ReactionTrigger.AttackRoll
            ? setup with { Target = checked(setup.Target + bonus) }
            : setup with { Bonus = checked(setup.Bonus + bonus) };

    private static ReactionMode ModeOf(Unit holder, string abilityId, ReactionSpec spec)
    {
        foreach (var setting in holder.Reactions)
        {
            if (string.Equals(setting.AbilityId, abilityId, StringComparison.Ordinal))
            {
                return setting.Mode;
            }
        }

        return spec.Policy ?? ReactionMode.Never;
    }

    /// <summary>
    /// <paramref name="settings"/> with <paramref name="abilityId"/>'s mode set to <paramref name="mode"/>: an existing
    /// setting replaced, a new one placed in ordinal order.
    /// </summary>
    internal static ImmutableArray<ReactionSetting> With(ImmutableArray<ReactionSetting> settings, string abilityId, ReactionMode mode)
    {
        var builder = ImmutableArray.CreateBuilder<ReactionSetting>(settings.Length + 1);
        bool placed = false;
        foreach (var setting in settings)
        {
            int c = string.CompareOrdinal(setting.AbilityId, abilityId);
            if (c == 0)
            {
                continue;
            }

            if (c > 0 && !placed)
            {
                builder.Add(new ReactionSetting(abilityId, mode));
                placed = true;
            }

            builder.Add(setting);
        }

        if (!placed)
        {
            builder.Add(new ReactionSetting(abilityId, mode));
        }

        return builder.ToImmutable();
    }

    /// <summary>
    /// Whether <paramref name="unit"/> may set <paramref name="abilityId"/>'s policy to <paramref name="mode"/> (plan call
    /// 13): <see cref="CommandError.UnknownUnit"/>, <see cref="CommandError.UnknownAbility"/> (the unit does not hold it,
    /// or the rules have no such ability), <see cref="CommandError.NotAReaction"/>, and
    /// <see cref="CommandError.AskMeIsPartyOnly"/> for <see cref="ReactionMode.AskMe"/> on a non-party unit (AB 7.3:
    /// monsters have no "ask me"), and <see cref="CommandError.PolicyNeedsPool"/> for
    /// <see cref="ReactionMode.WhilePoolAtLeastHalf"/> on a reaction with no pool (it could never fire), in that order.
    /// A snapshot cannot refuse that last pair on load, since it does not know an ability's pool without the rules.
    /// </summary>
    internal static CommandError CheckSetting(RulesContext rules, GameState s, UnitId unit, string abilityId, ReactionMode mode)
    {
        if (s.FindUnit(unit) is not { } holder)
        {
            return CommandError.UnknownUnit;
        }

        if (abilityId is null || holder.Abilities.IndexOf(abilityId, StringComparer.Ordinal) < 0 || rules.FindAbility(abilityId) is not { } ability)
        {
            return CommandError.UnknownAbility;
        }

        if (ability.Reaction is null)
        {
            return CommandError.NotAReaction;
        }

        if (mode == ReactionMode.AskMe && holder.Faction != Faction.Party)
        {
            return CommandError.AskMeIsPartyOnly;
        }

        return mode == ReactionMode.WhilePoolAtLeastHalf && ability.Pool is null ? CommandError.PolicyNeedsPool : CommandError.None;
    }
}
