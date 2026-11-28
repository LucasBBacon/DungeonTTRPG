using System;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Conditions;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Combat;

// These classes share their names with their namespaces, so name them explicitly inside this namespace.
using Conditions = Dungeon.Core.Conditions.Conditions;
using Damage = Dungeon.Core.Damage.Damage;
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// What <see cref="Harm.Deal"/> did: the resolved damage, the damage event's sequence id, and the events it emitted, in
/// order. The record compares <see cref="Events"/> by reference; compare it with <c>SequenceEqual</c>.
/// </summary>
public sealed record HarmResult(DamageOutcome Outcome, long Seq, ImmutableArray<GameEvent> Events);

/// <summary>
/// Damage with its consequences (M1-10 plan call 11; DH 20.3 step 7; owner ruling 3): the only path to death. M1-09's
/// <see cref="Damage.Deal"/> stays a library call with none.
/// </summary>
public static class Harm
{
    /// <summary>
    /// Applies <paramref name="roll"/> to <paramref name="target"/> (<see cref="Damage.Apply"/>, floors included), gives
    /// the damage event the sequence id <see cref="GameState.NextEventSeq"/> (plan call 1), and emits
    /// <see cref="DamageDealt"/>, then <see cref="DamageClamped"/> when a floor bound. When the hit took the unit from
    /// 1 or more to 0: the hero emits <see cref="RunEnded"/> (<see cref="RunEndCause.HeroDeath"/>, this event's id) as
    /// the final event and sets <see cref="GameState.Ended"/>, and is not removed; any other unit emits
    /// <see cref="UnitDied"/> with its last square and is removed (<see cref="Remove"/>), and the ends of the
    /// <c>sourceTied</c> conditions it was the source of follow (M1-12a plan call 8). Damage to a unit already on 0
    /// emits <see cref="DamageDealt"/> and nothing else. It never starts a turn and never ends combat: once the
    /// command's queue has drained, callers run <c>CombatEnd.EndIfWon</c> and then <see cref="Turns.ResumeIfVacated"/>.
    /// A condition's <paramref name="tick"/> is floored at 1 HP for every unit while no fight is on (CO 5.5; M1-12a plan
    /// call 13; <see cref="Damage.Resolve"/>).
    /// Throws <see cref="InvalidOperationException"/> on an ended run and <see cref="ArgumentException"/> for an
    /// unknown unit.
    /// </summary>
    public static (HarmResult Result, GameState Next) Deal(RulesContext rules, GameState s, UnitId target, DamageRoll roll, bool tick = false)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(roll);
        if (s.Ended is not null)
        {
            throw new InvalidOperationException("Cannot deal damage: the run has ended.");
        }

        var (outcome, next) = Damage.Apply(rules, s, target, roll, tick);
        long seq = s.NextEventSeq;
        next = next with { NextEventSeq = checked(seq + 1) };

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        events.Add(new DamageDealt(seq, target, outcome.Total, outcome.HpAfter));
        if (outcome.Clamp is { } reason)
        {
            events.Add(new DamageClamped(seq, target, reason));
        }

        // From 1 or more to 0. The maximum is at least 1, so min(HP, maximum) >= 1 exactly when the stored HP is.
        if (outcome.HpBefore >= 1 && outcome.ReachedZero)
        {
            if (s.Hero == target)
            {
                // DH 20.3 step 7: the final event; nothing later is processed.
                events.Add(new RunEnded(RunEndCause.HeroDeath, seq));
                next = next with { Ended = new RunEnd(RunEndCause.HeroDeath, seq) };
            }
            else
            {
                events.Add(new UnitDied(target, next.FindUnit(target)!.Pos));
                (next, var ends) = Remove(rules, next, target);
                events.AddRange(ends);
            }
        }

        return (new HarmResult(outcome, seq, events.ToImmutable()), next);
    }

    /// <summary>
    /// Removes a dead unit (owner ruling 3; plan call 11; M1-12a plan call 8) and returns the events that follow its
    /// <see cref="UnitDied"/>. First every effect instance it holds goes, with no event (a dead holder's conditions go
    /// with it: <see cref="UnitDied"/> covers them), and every non-condition instance it is the source of, in seq order
    /// through <see cref="Effects.Remove"/>; then the unit, which frees its square; then its slot in the turn order;
    /// then one pass in seq order over the living holders' condition instances: each <c>sourceTied</c> one it was the
    /// source of ends through <c>Conditions.End</c> with <see cref="ConditionEndReason.SourceGone"/>, and each one
    /// sustained by an instance removed with it (CO 5.1: a sustained condition ends with its sustainer) with
    /// <see cref="ConditionEndReason.Ended"/>, Steeled following a hard one. Every other condition instance keeps its
    /// source id and recorded faction. In the turn order, a slot before
    /// the current one moves the current index back by 1. The current slot itself leaves the index on its successor
    /// (or on the order's length when it was the last slot) and sets <see cref="CombatState.TurnVacated"/>, with no <see cref="TurnEnded"/> (a dead unit has no turn-end step, CO 5.4)
    /// and no <see cref="TurnStarted"/>. A later slot changes nothing. Under the <see cref="CombatState.BeforeRound1"/>
    /// marker no unit holds the turn: removing slot 0 leaves the index at 0, on the next unit, and vacates nothing
    /// (M1-11 plan call 7). Out of combat there is no order to change. A
    /// pending decision the dead unit holds is cleared: its choice is moot and the draw behind it has happened, and left
    /// pending it would refuse every command and make the state unloadable.
    /// </summary>
    private static (GameState Next, ImmutableArray<GameEvent> Events) Remove(RulesContext rules, GameState s, UnitId dead)
    {
        // s.Effects is kept sorted by seq, so these walks are in seq order.
        var seqs = ImmutableArray.CreateBuilder<long>();
        foreach (var instance in s.Effects)
        {
            if (instance.Holder == dead || (instance.Source.Unit == dead && instance.Condition is null))
            {
                seqs.Add(instance.Seq);
            }
        }

        // The living holders' condition instances to end after UnitDied, in seq order, each with its reason.
        var ends = ImmutableArray.CreateBuilder<(long Seq, ConditionEndReason Reason)>();
        foreach (var instance in s.Effects)
        {
            if (instance.Holder == dead || instance.Condition is not { } condition)
            {
                continue;
            }

            if (instance.Source.Unit == dead && rules.FindCondition(instance.EffectId) is { Flags.SourceTied: true })
            {
                ends.Add((instance.Seq, ConditionEndReason.SourceGone));
            }
            else if (condition.SustainedBy is { } sustainer && seqs.Contains(sustainer))
            {
                ends.Add((instance.Seq, ConditionEndReason.Ended));
            }
        }

        foreach (long seq in seqs)
        {
            s = Effects.Remove(s, seq);
        }

        int index = -1;
        for (int i = 0; i < s.Units.Length; i++)
        {
            if (s.Units[i].Id == dead)
            {
                index = i;
            }
        }

        s = s with { Units = s.Units.RemoveAt(index) };
        if (s.Pending?.Unit == dead)
        {
            s = s with { Pending = null };
        }

        if (s.Combat is { } combat)
        {
            int d = combat.Order.IndexOf(dead);
            if (d >= 0)
            {
                // Under the before-round-1 marker no unit holds the turn, so slot 0 leaves the index on its successor
                // without vacating anything (M1-11 plan call 7).
                var order = combat.Order.RemoveAt(d);
                combat = combat.BeforeRound1
                    ? combat with { Order = order }
                    : d < combat.Current
                        ? combat with { Order = order, Current = combat.Current - 1 }
                        : d == combat.Current
                            ? combat with { Order = order, TurnVacated = true }
                            : combat with { Order = order };
                s = s with { Combat = combat };
            }
        }

        // Each may already be gone, ended by an earlier one's sustained cascade.
        var events = ImmutableArray.CreateBuilder<GameEvent>();
        foreach (var (seq, reason) in ends)
        {
            if (s.Effects.Any(e => e.Seq == seq))
            {
                var (ended, next) = Conditions.End(rules, s, seq, reason);
                events.AddRange(ended);
                s = next;
            }
        }

        return (s, events.ToImmutable());
    }
}
