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
/// resolution itself changes the state. A reaction window that reaches an "ask me" candidate suspends the use on a
/// <see cref="DecisionKind.Reaction"/> decision, holding it in <see cref="GameState.Resolution"/>, and
/// <see cref="Continue"/> takes it up again from there (plan call 12).
/// </summary>
internal static class Resolution
{
    /// <summary>The option of a <see cref="DecisionKind.Reaction"/> decision that declines the asked reaction.</summary>
    internal const int Decline = 0;

    /// <summary>The option of a <see cref="DecisionKind.Reaction"/> decision that takes the asked reaction.</summary>
    internal const int Take = 1;

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
    /// When a window reaches an "ask me" candidate (plan call 12), the use suspends with the d20 drawn: the state holds
    /// the <see cref="AbilityFrame"/> and a <see cref="DecisionKind.Reaction"/> decision for the reactor (options
    /// <c>[0 = decline, 1 = take]</c>, source the reactor's square), <see cref="DecisionRequested"/> is the last event, and
    /// the target's roll event and everything after it wait for <see cref="Continue"/>.
    /// </para>
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

        s = Targets(rules, s, new InFlight(user, ability, target, locked, difficulty), 0, events);
        return (events.ToImmutable(), s);
    }

    /// <summary>
    /// Answers the pending <see cref="DecisionKind.Reaction"/> decision and takes up the suspended use in
    /// <see cref="GameState.Resolution"/> (plan call 12). The decision and the frame are cleared. Taking fires the asked
    /// candidate (<see cref="Reactions.Take"/>, then <see cref="ReactionTaken"/>); declining walks on to the candidates
    /// after it in ability-id order, which may fire one automatically or ask again (a new decision and frame, and
    /// <see cref="DecisionRequested"/> last). Nothing is drawn before the window closes: the roll is the frame's. Then the
    /// target's roll event, its damage and effects, the remaining targets and <see cref="AbilityResolved"/> follow as in
    /// <see cref="Use"/>. The caller raises <see cref="DecisionResolved"/> first.
    /// <para>
    /// Loading checks the frame's structure only. The rules-dependent part is checked here, and a mismatch throws
    /// <see cref="InvalidOperationException"/>: the ability and the candidate are known, the candidate is a reaction with
    /// the frame's trigger, the trigger is the ability's window (an attack's or a save's), and a save difficulty is held
    /// exactly for a save.
    /// </para>
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) Continue(RulesContext rules, GameState s, bool take)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var frame = s.Resolution ?? throw new InvalidOperationException("No ability use is suspended.");
        if (s.Pending is not { Kind: DecisionKind.Reaction })
        {
            throw new InvalidOperationException("A suspended ability use needs a pending reaction decision.");
        }

        var ability = rules.FindAbility(frame.AbilityId)
            ?? throw new InvalidOperationException($"The suspended ability \"{frame.AbilityId}\" is not in the rules.");
        var window = ability.Resolve switch
        {
            AttackResolve => ReactionTrigger.AttackRoll,
            SaveResolve => ReactionTrigger.OwnSave,
            _ => throw new InvalidOperationException($"The suspended ability \"{ability.Id}\" rolls nothing; it has no reaction window."),
        };
        if (frame.Window != window)
        {
            throw new InvalidOperationException($"The suspended ability \"{ability.Id}\" opens a {window} window, not {frame.Window}.");
        }

        if ((ability.Resolve is SaveResolve) != frame.SaveDifficulty.HasValue)
        {
            throw new InvalidOperationException($"The suspended ability \"{ability.Id}\" holds a save difficulty exactly when it is a save.");
        }

        if (rules.FindAbility(frame.Candidate) is not { Reaction: { } spec } || spec.Trigger != frame.Window)
        {
            throw new InvalidOperationException($"The asked candidate \"{frame.Candidate}\" is not a {frame.Window} reaction.");
        }

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        var reactor = frame.Locked[frame.Index];
        var setup = frame.Setup;
        s = s with { Pending = null, Resolution = null };
        if (take)
        {
            (setup, s) = Reactions.Take(rules, s, reactor, frame.Candidate, frame.Window, setup);
            events.Add(new ReactionTaken(reactor, frame.Candidate));
        }
        else
        {
            var (fired, ask, next, after) = Reactions.Window(rules, s, reactor, frame.Window, setup, frame.Roll, frame.Candidate);
            if (ask is not null)
            {
                s = Suspend(s, frame with { Candidate = ask }, events);
                return (events.ToImmutable(), s);
            }

            if (fired is not null)
            {
                events.Add(new ReactionTaken(reactor, fired));
            }

            (setup, s) = (next, after);
        }

        var use = new InFlight(frame.User, ability, frame.Target, frame.Locked, frame.SaveDifficulty ?? 0);
        s = Decide(rules, s, use, reactor, setup, frame.Roll, events);
        if (s.Ended is null)
        {
            s = Targets(rules, s, use, frame.Index + 1, events);
        }

        return (events.ToImmutable(), s);
    }

    // One use in flight: who, what, aimed where, the locked targets and the save DC captured at the lock (0 for none).
    private sealed record InFlight(UnitId User, AbilityDefinition Ability, AbilityTarget Target, ImmutableArray<UnitId> Locked, long Difficulty);

    // The locked targets from `from` on, then AbilityResolved; stops at a suspension or a run end.
    private static GameState Targets(
        RulesContext rules, GameState s, InFlight use, int from, ImmutableArray<GameEvent>.Builder events)
    {
        for (int i = from; i < use.Locked.Length; i++)
        {
            // The user removed: its remaining targets are skipped (defensive; M1-11 cannot reach it, plan call 10).
            if (s.FindUnit(use.User) is null)
            {
                break;
            }

            if (s.FindUnit(use.Locked[i]) is null)
            {
                continue;
            }

            s = ResolveTarget(rules, s, use, i, events);
            if (s.Ended is not null || s.Resolution is not null)
            {
                return s;
            }
        }

        events.Add(new AbilityResolved(use.User, use.Ability.Id));
        return s;
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

    // One target: the roll, if any, then the attack-roll or own-save window, then the outcome, the damage and the
    // effects. A window that asks suspends the use instead (the state then holds the frame).
    private static GameState ResolveTarget(
        RulesContext rules, GameState s, InFlight use, int index, ImmutableArray<GameEvent>.Builder events)
    {
        var ability = use.Ability;
        var target = use.Locked[index];
        if (Prepare(rules, s, use.User, ability, target, use.Difficulty) is not { } setup)
        {
            return Land(rules, s, use, target, crit: false, events);
        }

        var (roll, stream) = Dice.D20(s.Rng.Combat, setup.Mode);
        s = s with { Rng = s.Rng with { Combat = stream } };

        // The attack-roll window (plan call 11): the defender's wards, judged on the roll already drawn. The own-save
        // window opens only against a user of another faction (plan call 10: a hostile ability).
        ReactionTrigger? window = ability.Resolve switch
        {
            AttackResolve => ReactionTrigger.AttackRoll,
            SaveResolve when s.FindUnit(use.User)!.Faction != s.FindUnit(target)!.Faction => ReactionTrigger.OwnSave,
            _ => null,
        };
        if (window is { } trigger)
        {
            var (fired, ask, next, after) = Reactions.Window(rules, s, target, trigger, setup, roll);
            if (ask is not null)
            {
                long? difficulty = ability.Resolve is SaveResolve ? use.Difficulty : null;
                var frame = new AbilityFrame(use.User, ability.Id, use.Target, use.Locked, index, trigger, setup, roll, ask, difficulty);
                return Suspend(s, frame, events);
            }

            if (fired is not null)
            {
                events.Add(new ReactionTaken(target, fired));
            }

            (setup, s) = (next, after);
        }

        return Decide(rules, s, use, target, setup, roll, events);
    }

    // The use held on an "ask me" candidate (plan call 12): the frame, and a reaction decision for the reactor.
    private static GameState Suspend(GameState s, AbilityFrame frame, ImmutableArray<GameEvent>.Builder events)
    {
        var reactor = s.FindUnit(frame.Locked[frame.Index])!;
        var decision = new PendingDecision(
            new DecisionId(s.NextDecisionId), reactor.Id, DecisionKind.Reaction, [Decline, Take], reactor.Pos);
        events.Add(new DecisionRequested(decision));
        return s with { Pending = decision, Resolution = frame, NextDecisionId = checked(s.NextDecisionId + 1) };
    }

    // The window closed: the outcome on the drawn roll, its event, and on a hit or a failed save the damage and effects.
    private static GameState Decide(
        RulesContext rules, GameState s, InFlight use, UnitId target, ContestSetup setup, D20Roll roll,
        ImmutableArray<GameEvent>.Builder events)
    {
        switch (use.Ability.Resolve)
        {
            case AttackResolve:
            {
                var result = Contests.ResolveAttack(setup, roll);
                events.Add(new AttackRolled(use.User, target, result));
                return result.Hit ? Land(rules, s, use, target, result.Crit, events) : s;
            }

            case SaveResolve:
            {
                var result = Contests.ResolveCheck(setup, roll);
                events.Add(new SaveRolled(target, result));
                return result.Success ? s : Land(rules, s, use, target, crit: false, events);
            }

            default:
                throw new InvalidOperationException($"Ability \"{use.Ability.Id}\" has an unknown resolve.");
        }
    }

    // The damage (a crit doubling the dice), then the ability's effects on a target that still stands.
    private static GameState Land(
        RulesContext rules, GameState s, InFlight use, UnitId target, bool crit, ImmutableArray<GameEvent>.Builder events)
    {
        var ability = use.Ability;
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

        var source = new EffectSource(use.User);
        foreach (var effectId in ability.Effects.IsDefault ? [] : ability.Effects)
        {
            long seq = s.NextEffectSeq;
            s = Effects.Apply(s, effectId, target, source);
            events.Add(new EffectApplied(seq, effectId, target, source));
        }

        return s;
    }
}
