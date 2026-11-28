using System;
using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.AbilityModel;

using Damage = Dungeon.Core.Damage.Damage;
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// An ability use after validation (AB 14.3; M1-11 plan call 10): the cost is paid, the targets are locked in unit order,
/// and each target resolves in turn. Triggers run only after it returns, so nothing between two targets but the
/// resolution itself changes the state.
/// </summary>
internal static class Resolution
{
    /// <summary>
    /// Resolves <paramref name="ability"/> used by <paramref name="user"/> on <paramref name="target"/>, which
    /// <see cref="AbilityUse.CheckTarget"/> has accepted on <paramref name="s"/>. Events: <see cref="AbilityUsed"/>;
    /// <see cref="AbilityCostPaid"/> (the cost's bits on the budget, then the pool spend through
    /// <see cref="Pools.Spend"/>); <see cref="TargetsLocked"/> (<see cref="AbilityUse.Affected"/>, in unit order; a save's
    /// DC is the user's <c>abilityDifficulty</c>, captured here); then per target, skipping units removed earlier in the
    /// same use: an attack (the attack-roll reaction window, then <see cref="AttackRolled"/>, and on a hit the damage, a
    /// crit doubling the dice), a save (the own-save window when the user is of another faction from the saver, then
    /// <see cref="SaveRolled"/>, and on a failure the damage); a fired reaction raises <see cref="ReactionTaken"/>
    /// before the roll's event (<see cref="Reactions.Window"/>); or with no <c>resolve</c> the damage alone; the damage is
    /// <see cref="DamageRolled"/> then <see cref="Harm.Deal"/>'s events, and the ability's effects follow on a target
    /// that still stands (<see cref="EffectApplied"/>, from the user). Last, <see cref="AbilityResolved"/>. When the run
    /// ends, resolution stops at once: <see cref="RunEnded"/> is the last event and there is no
    /// <see cref="AbilityResolved"/>. If the user itself is removed, the remaining targets are skipped and
    /// <see cref="AbilityResolved"/> still closes the use.
    /// <para>
    /// For an <paramref name="opener"/> (plan calls 6 and 8; M1-10 rulings record 5, step 8), <paramref name="s"/> is the
    /// fight held before round 1, and once the cost is paid the user is marked (<see cref="Turns.MarkOpener"/>) with what
    /// was paid: <see cref="OpenerCharge.Charged"/> is the cost's own bits, and <see cref="OpenerCharge.TurnPools"/> the
    /// spend if its pool is a <see cref="RefillTag.Turn"/> pool. The mark adds no event.
    /// </para>
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) Use(
        RulesContext rules, GameState s, UnitId user, AbilityDefinition ability, AbilityTarget target, bool opener = false)
    {
        var events = ImmutableArray.CreateBuilder<GameEvent>();
        events.Add(new AbilityUsed(user, ability.Id));

        var (charged, pools, paid) = Pay(rules, s, user, ability);
        events.Add(new AbilityCostPaid(user, ability.Id, charged, pools));
        s = opener ? Turns.MarkOpener(paid, user, new OpenerCharge(charged, TurnPools(rules, pools))) : paid;

        var locked = AbilityUse.Affected(rules, s, user, ability, target).Units;
        long difficulty = Difficulty(rules, s, user, ability);
        events.Add(new TargetsLocked(user, locked));

        foreach (var unit in locked)
        {
            // The user removed: its remaining targets are skipped (defensive; M1-11 cannot reach it, plan call 10).
            if (s.FindUnit(user) is null)
            {
                break;
            }

            if (s.FindUnit(unit) is null)
            {
                continue;
            }

            s = ResolveTarget(rules, s, user, ability, unit, difficulty, events);
            if (s.Ended is not null)
            {
                return (events.ToImmutable(), s);
            }
        }

        events.Add(new AbilityResolved(user, ability.Id));
        return (events.ToImmutable(), s);
    }

    // The spends from turn pools, in the paid order (one pool at most, so already sorted by pool id).
    private static ImmutableArray<PoolSpend> TurnPools(RulesContext rules, ImmutableArray<PoolSpend> pools)
    {
        var turn = ImmutableArray.CreateBuilder<PoolSpend>(pools.Length);
        foreach (var spend in pools)
        {
            if (rules.Pool(spend.PoolId).Refill == RefillTag.Turn)
            {
                turn.Add(spend);
            }
        }

        return turn.ToImmutable();
    }

    /// <summary>
    /// The save DC a use carries, captured when its targets lock: the user's <c>abilityDifficulty</c> for a save, 0
    /// otherwise (plan call 10). Resolution and the preview read it on the same paid state.
    /// </summary>
    internal static long Difficulty(RulesContext rules, GameState s, UnitId user, AbilityDefinition ability) =>
        ability.Resolve is SaveResolve ? Effects.Number(rules, s, user, "abilityDifficulty") : 0;

    /// <summary>
    /// The contest <paramref name="target"/> meets: <see cref="Contests.PrepareAttack"/> for an attack,
    /// <see cref="Contests.PrepareSave"/> against <paramref name="difficulty"/> for a save, null with no <c>resolve</c>.
    /// Resolution and the preview (plan call 14) both build their setups here.
    /// </summary>
    internal static ContestSetup? Prepare(
        RulesContext rules, GameState s, UnitId user, AbilityDefinition ability, UnitId target, long difficulty) =>
        ability.Resolve switch
        {
            AttackResolve attack => Contests.PrepareAttack(rules, s, user, target, attack.Attack),
            SaveResolve save => Contests.PrepareSave(rules, s, target, save.Save, difficulty),
            null => null,
            _ => throw new InvalidOperationException($"Ability \"{ability.Id}\" has an unknown resolve."),
        };

    /// <summary>
    /// The cost's own bits on the user's budget, then the pool spend, separate from the cost (AB 7.1). Pure: the preview
    /// reads the paid state the targets lock on.
    /// </summary>
    internal static (TurnBudget Charged, ImmutableArray<PoolSpend> Pools, GameState Next) Pay(
        RulesContext rules, GameState s, UnitId user, AbilityDefinition ability)
    {
        var cost = ability.Cost;
        var charged = cost.Kind switch
        {
            CostKind.Action => new TurnBudget(0, 1, false, false, false, false),
            CostKind.Bonus => new TurnBudget(0, 0, true, false, false, false),
            CostKind.Free => new TurnBudget(0, 0, false, false, true, false),
            CostKind.Move => new TurnBudget(cost.Move, 0, false, false, false, false),
            CostKind.None => default,
            _ => throw new InvalidOperationException($"Ability \"{ability.Id}\" has cost {cost.Kind}, which M1-11 cannot pay."),
        };

        var unit = s.FindUnit(user)!;
        var b = unit.Budget;
        var budget = new TurnBudget(
            checked(b.MovementUsed + charged.MovementUsed),
            checked(b.ActionsUsed + charged.ActionsUsed),
            b.BonusUsed || charged.BonusUsed,
            b.ReactionUsed,
            b.FreeUsed || charged.FreeUsed,
            b.CampSpent);
        s = s with { Units = s.Units.SetItem(s.Units.IndexOf(unit), unit with { Budget = budget }) };

        if (ability.Pool is not { } use)
        {
            return (charged, ImmutableArray<PoolSpend>.Empty, s);
        }

        var (ok, spent) = Pools.Spend(rules, s, user, use.PoolId, use.Count);
        if (!ok)
        {
            throw new InvalidOperationException($"Unit {user.Value} cannot pay the pool of \"{ability.Id}\"; validation should have refused it.");
        }

        return (charged, [new PoolSpend(use.PoolId, use.Count)], spent);
    }

    // A reaction window on the target (Reactions.Window); a fired reaction raises ReactionTaken.
    private static (ContestSetup Setup, GameState Next) Window(
        RulesContext rules, GameState s, UnitId target, ReactionTrigger trigger, ContestSetup setup, D20Roll roll,
        ImmutableArray<GameEvent>.Builder events)
    {
        var (fired, next, after) = Reactions.Window(rules, s, target, trigger, setup, roll);
        if (fired is not null)
        {
            events.Add(new ReactionTaken(target, fired));
        }

        return (next, after);
    }

    // One target: the roll, if any, then the attack-roll or own-save window, then the damage and the effects.
    private static GameState ResolveTarget(
        RulesContext rules, GameState s, UnitId user, AbilityDefinition ability, UnitId target, long difficulty,
        ImmutableArray<GameEvent>.Builder events)
    {
        bool crit = false;
        switch (ability.Resolve)
        {
            case AttackResolve:
            {
                var setup = Prepare(rules, s, user, ability, target, difficulty)!.Value;
                var (roll, stream) = Dice.D20(s.Rng.Combat, setup.Mode);
                s = s with { Rng = s.Rng with { Combat = stream } };

                // The attack-roll window (plan call 11): the defender's wards, judged on the roll already drawn.
                (setup, s) = Window(rules, s, target, ReactionTrigger.AttackRoll, setup, roll, events);
                var result = Contests.ResolveAttack(setup, roll);
                events.Add(new AttackRolled(user, target, result));
                if (!result.Hit)
                {
                    return s;
                }

                crit = result.Crit;
                break;
            }

            case SaveResolve:
            {
                var setup = Prepare(rules, s, user, ability, target, difficulty)!.Value;
                var (roll, stream) = Dice.D20(s.Rng.Combat, setup.Mode);
                s = s with { Rng = s.Rng with { Combat = stream } };

                // The own-save window opens only against a user of another faction (plan call 10: a hostile ability).
                if (s.FindUnit(user)!.Faction != s.FindUnit(target)!.Faction)
                {
                    (setup, s) = Window(rules, s, target, ReactionTrigger.OwnSave, setup, roll, events);
                }

                var result = Contests.ResolveCheck(setup, roll);
                events.Add(new SaveRolled(target, result));
                if (result.Success)
                {
                    return s;
                }

                break;
            }

            case null:
                break;

            default:
                throw new InvalidOperationException($"Ability \"{ability.Id}\" has an unknown resolve.");
        }

        if (!ability.Damage.IsDefaultOrEmpty)
        {
            var (damage, rolled) = Damage.Roll(s, ability.Damage, crit);
            events.Add(new DamageRolled(target, damage));
            var (harm, next) = Harm.Deal(rules, rolled, target, damage);
            events.AddRange(harm.Events);
            s = next;
            if (s.Ended is not null || s.FindUnit(target) is null)
            {
                return s;
            }
        }

        var source = new EffectSource(user);
        foreach (var effectId in ability.Effects.IsDefault ? [] : ability.Effects)
        {
            long seq = s.NextEffectSeq;
            s = Effects.Apply(s, effectId, target, source);
            events.Add(new EffectApplied(seq, effectId, target, source));
        }

        return s;
    }
}
