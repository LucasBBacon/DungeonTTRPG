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
    /// same use: an attack (<see cref="AttackRolled"/>, and on a hit the damage, a crit doubling the dice), a save
    /// (<see cref="SaveRolled"/>, and on a failure the damage), or with no <c>resolve</c> the damage alone; the damage is
    /// <see cref="DamageRolled"/> then <see cref="Harm.Deal"/>'s events, and the ability's effects follow on a target
    /// that still stands (<see cref="EffectApplied"/>, from the user). Last, <see cref="AbilityResolved"/>. When the run
    /// ends, resolution stops at once: <see cref="RunEnded"/> is the last event and there is no
    /// <see cref="AbilityResolved"/>. If the user itself is removed, the remaining targets are skipped and
    /// <see cref="AbilityResolved"/> still closes the use.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) Use(
        RulesContext rules, GameState s, UnitId user, AbilityDefinition ability, AbilityTarget target)
    {
        var events = ImmutableArray.CreateBuilder<GameEvent>();
        events.Add(new AbilityUsed(user, ability.Id));

        var (charged, pools, paid) = Pay(rules, s, user, ability);
        events.Add(new AbilityCostPaid(user, ability.Id, charged, pools));
        s = paid;

        var locked = AbilityUse.Affected(rules, s, user, ability, target).Units;
        long difficulty = ability.Resolve is SaveResolve ? Effects.Number(rules, s, user, "abilityDifficulty") : 0;
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

    // The cost's own bits on the user's budget, then the pool spend, separate from the cost (AB 7.1).
    private static (TurnBudget Charged, ImmutableArray<PoolSpend> Pools, GameState Next) Pay(
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

    // One target: the roll, if any, then the damage and the effects.
    private static GameState ResolveTarget(
        RulesContext rules, GameState s, UnitId user, AbilityDefinition ability, UnitId target, long difficulty,
        ImmutableArray<GameEvent>.Builder events)
    {
        bool crit = false;
        switch (ability.Resolve)
        {
            case AttackResolve attack:
            {
                var setup = Contests.PrepareAttack(rules, s, user, target, attack.Attack);
                var (roll, stream) = Dice.D20(s.Rng.Combat, setup.Mode);
                s = s with { Rng = s.Rng with { Combat = stream } };

                // The attack-roll reaction window opens here (M1-11 Task 7).
                var result = Contests.ResolveAttack(setup, roll);
                events.Add(new AttackRolled(user, target, result));
                if (!result.Hit)
                {
                    return s;
                }

                crit = result.Crit;
                break;
            }

            case SaveResolve save:
            {
                var setup = Contests.PrepareSave(rules, s, target, save.Save, difficulty);
                var (roll, stream) = Dice.D20(s.Rng.Combat, setup.Mode);
                s = s with { Rng = s.Rng with { Combat = stream } };

                // The own-save reaction window opens here (M1-11 Task 7).
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
