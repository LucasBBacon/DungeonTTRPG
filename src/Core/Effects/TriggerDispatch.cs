using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Dungeon.Core.Determinism;

namespace Dungeon.Core.Effects;

/// <summary>
/// Fires the triggers that answer a command's events (MAIN 6.4; AB 14.4; M1-07 plan calls 3, 4 and 7). The command's
/// events are queued in order; each queued event with a registered rule-event id fires the triggers listening for it,
/// in order of priority (lower first), then source unit order (environment last), then creation order. Events the
/// actions raise join the end of the queue, which is also the command's event list. A command fires at most
/// <see cref="Effects.MaxTriggerFirings"/> triggers, across all its dispatch phases (M1-11 plan call 8). Nothing fires
/// once the run has ended.
/// <para>
/// Stale firings (M1-12a plan call 11): the due list is computed once per queued event and rule event, so an instance a
/// firing creates does not answer the event being processed; it answers later events in the queue, its own
/// <see cref="EffectApplied"/> included. Before each firing its instance is looked up by seq in the current state: a
/// removed instance is skipped (no count, no event), and a live one fires as it is now. An <c>applyEffect</c> whose
/// recipient is no longer in the state is skipped.
/// </para>
/// </summary>
internal static class TriggerDispatch
{
    /// <summary>
    /// The state and events after every trigger answering <paramref name="events"/> has fired, starting from
    /// <paramref name="state"/>. The result's events are <paramref name="events"/> followed by the trigger-produced
    /// events in firing order. Throws <see cref="TriggerLoopException"/> past the firing bound.
    /// </summary>
    public static (GameState State, ImmutableArray<GameEvent> Events) Run(
        RulesContext rules, GameState state, ImmutableArray<GameEvent> events)
    {
        int firings = 0;
        return Run(rules, state, events, ref firings);
    }

    /// <summary>
    /// <see cref="Run(RulesContext, GameState, ImmutableArray{GameEvent})"/> with a firing counter shared by every
    /// dispatch phase of one command (M1-11 plan call 8), so <see cref="Effects.MaxTriggerFirings"/> stays a bound per
    /// command. Once <see cref="GameState.Ended"/> is set, dispatch stops: before the next queued event, before the next
    /// firing, and before the next action of the firing in progress (DH 20.3 step 7: nothing later is processed).
    /// </summary>
    internal static (GameState State, ImmutableArray<GameEvent> Events) Run(
        RulesContext rules, GameState state, ImmutableArray<GameEvent> events, ref int firings)
    {
        var queue = new List<GameEvent>(events);
        void Emit(GameEvent e) => queue.Add(e ?? throw new ArgumentNullException(nameof(e), "A hook emitted a null event."));

        bool fired = false;
        for (int i = 0; i < queue.Count && state.Ended is null; i++)
        {
            var e = queue[i];
            foreach (var ruleEvent in rules.EventsRaisedBy(e))
            {
                UnitId subject = ruleEvent.Subject(e);
                foreach (var firing in Due(rules, state, ruleEvent.Id, subject))
                {
                    if (state.Ended is not null)
                    {
                        break;
                    }

                    // An earlier firing may have removed or changed the instance (M1-12a plan call 11): a removed one is
                    // skipped, uncounted; a live one fires as it is now.
                    if (Current(state, firing.Seq) is not { } instance)
                    {
                        continue;
                    }

                    if (firings == Effects.MaxTriggerFirings)
                    {
                        throw new TriggerLoopException(i, firings);
                    }

                    firings++;
                    fired = true;
                    state = Fire(rules, state, e, subject, instance, firing.Trigger, Emit);
                }
            }
        }

        return fired ? (state, queue.ToImmutableArray()) : (state, events);
    }

    // The triggers listening for eventId whose scope admits subject, in firing order.
    private static List<Firing> Due(RulesContext rules, GameState state, string eventId, UnitId subject)
    {
        var due = new List<Firing>();
        foreach (var instance in state.Effects)
        {
            var triggers = rules.Definition(instance.EffectId).Triggers;
            for (int t = 0; t < triggers.Length; t++)
            {
                var trigger = triggers[t];
                if (string.Equals(trigger.On, eventId, StringComparison.Ordinal)
                    && (trigger.Scope == TriggerScope.Any || instance.Holder == subject))
                {
                    due.Add(new Firing(trigger.Priority, SourceRank(state, instance), instance.Seq, t, trigger));
                }
            }
        }

        due.Sort(FiringOrder.Instance);
        return due;
    }

    // The source unit's position in the unit list (sorted by id); the environment after every unit. A condition instance
    // whose source unit is gone ranks with the environment (M1-12a plan call 8); any other effect's source must be present.
    private static int SourceRank(GameState state, EffectInstance instance)
    {
        if (instance.Source.Unit is not { } unit)
        {
            return state.Units.Length;
        }

        for (int i = 0; i < state.Units.Length; i++)
        {
            if (state.Units[i].Id == unit)
            {
                return i;
            }
        }

        return instance.Condition is not null
            ? state.Units.Length
            : throw new InvalidOperationException($"An effect's source unit {unit.Value} is not in the state.");
    }

    // The instance with this seq in the state, or null when it is gone. A linear walk: it assumes nothing of the order.
    private static EffectInstance? Current(GameState state, long seq)
    {
        foreach (var instance in state.Effects)
        {
            if (instance.Seq == seq)
            {
                return instance;
            }
        }

        return null;
    }

    // Runs the trigger's actions in order, for the instance as it is in the state when the firing starts.
    private static GameState Fire(
        RulesContext rules, GameState state, GameEvent e, UnitId subject, EffectInstance instance, Trigger trigger,
        Action<GameEvent> emit)
    {
        foreach (var action in trigger.Do)
        {
            // A run end stops the firing between its actions.
            if (state.Ended is not null)
            {
                break;
            }

            if (action.ApplyEffect is { } effectId)
            {
                // A recipient or source an earlier action removed gets nothing (plan call 11): the action is skipped.
                var holder = action.To == TriggerTarget.Holder ? instance.Holder : subject;
                if (state.FindUnit(holder) is null || state.FindUnit(instance.Holder) is null)
                {
                    continue;
                }

                var source = new EffectSource(instance.Holder);
                long seq = state.NextEffectSeq;
                state = Effects.Apply(state, effectId, holder, source);
                emit(new EffectApplied(seq, effectId, holder, source));
            }
            else
            {
                string hookId = action.Hook!;
                emit(new HookFired(hookId, instance.Seq));
                state = rules.Hooks.Get(hookId)(new HookContext(rules, state, e, instance, action.Args, emit))
                    ?? throw new InvalidOperationException($"Hook \"{hookId}\" returned no state.");
            }
        }

        return state;
    }

    // One trigger due to fire. Trigger is the trigger's index in its definition, so two triggers of one instance on
    // one event still have a total order.
    private sealed record Firing(int Priority, int SourceRank, long Seq, int TriggerIndex, Trigger Trigger);

    // Priority, then source rank, then seq, then trigger index: a total order, since seqs are unique.
    [TotalComparer]
    private sealed class FiringOrder : IComparer<Firing>
    {
        public static readonly FiringOrder Instance = new();

        public int Compare(Firing? x, Firing? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);
            int c = x.Priority.CompareTo(y.Priority);
            if (c == 0)
            {
                c = x.SourceRank.CompareTo(y.SourceRank);
            }

            if (c == 0)
            {
                c = x.Seq.CompareTo(y.Seq);
            }

            return c != 0 ? c : x.TriggerIndex.CompareTo(y.TriggerIndex);
        }
    }
}
