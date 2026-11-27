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
/// <see cref="Effects.MaxTriggerFirings"/> triggers.
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
        var queue = new List<GameEvent>(events);
        void Emit(GameEvent e) => queue.Add(e ?? throw new ArgumentNullException(nameof(e), "A hook emitted a null event."));

        int firings = 0;
        for (int i = 0; i < queue.Count; i++)
        {
            var e = queue[i];
            foreach (var ruleEvent in rules.EventsRaisedBy(e))
            {
                UnitId subject = ruleEvent.Subject(e);
                foreach (var firing in Due(rules, state, ruleEvent.Id, subject))
                {
                    if (firings == Effects.MaxTriggerFirings)
                    {
                        throw new TriggerLoopException(i, firings);
                    }

                    firings++;
                    state = Fire(rules, state, e, subject, firing, Emit);
                }
            }
        }

        return firings == 0 ? (state, events) : (state, queue.ToImmutableArray());
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
                    due.Add(new Firing(trigger.Priority, SourceRank(state, instance.Source), instance.Seq, t, instance, trigger));
                }
            }
        }

        due.Sort(FiringOrder.Instance);
        return due;
    }

    // The source unit's position in the unit list (sorted by id); the environment after every unit.
    private static int SourceRank(GameState state, EffectSource source)
    {
        if (source.Unit is not { } unit)
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

        throw new InvalidOperationException($"An effect's source unit {unit.Value} is not in the state.");
    }

    // Runs the trigger's actions in order.
    private static GameState Fire(
        RulesContext rules, GameState state, GameEvent e, UnitId subject, Firing firing, Action<GameEvent> emit)
    {
        var instance = firing.Instance;
        foreach (var action in firing.Trigger.Do)
        {
            if (action.ApplyEffect is { } effectId)
            {
                var holder = action.To == TriggerTarget.Holder ? instance.Holder : subject;
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
    private sealed record Firing(int Priority, int SourceRank, long Seq, int TriggerIndex, EffectInstance Instance, Trigger Trigger);

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
