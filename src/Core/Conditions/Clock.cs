using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.Conditions;

/// <summary>
/// The wearer clock (M1-12a plan calls 1, 5, 9, 13 and 15; owner rulings 2 and 4; CO 5.2 to 5.7, 7.2): the counting test,
/// a unit's turn-start and turn-end condition items (in combat, and for every unit in an out-of-combat tick), the round
/// end's and the tick end's window ends, and combat end's ends. The caller advances
/// <see cref="GameState.Phase"/> and sets <see cref="GameState.Step"/> before each step, so at a turn-start step the
/// phase is that step's own, and at a turn-end step the turn's Act phase is <c>Phase - 1</c> and its turn-start phase
/// <c>Phase - 2</c>. Each numbered item runs over instances in seq order (CO 5.4); every end goes through
/// <see cref="Conditions.End"/>, so it emits one <see cref="ConditionEnded"/>, ends what it sustains and grants Steeled
/// after a hard one, before the next instance is processed.
/// </summary>
internal static class Clock
{
    /// <summary>
    /// The CO 5.2 counting test: a wearer turn whose Act phase is <paramref name="actPhase"/> counts toward
    /// <paramref name="c"/> only if the instance was applied before that Act phase.
    /// </summary>
    internal static bool Counts(ConditionState c, long actPhase) => c.AppliedPhase < actPhase;

    /// <summary>
    /// <paramref name="unit"/>'s turn-start condition step (CO 5.4; plan call 9), after the caller has advanced the
    /// phase into it and run item 1 (the budget reset, which changes no condition and emits nothing). In combat the
    /// events start with <see cref="TurnStarted"/>; then:
    /// <list type="bullet">
    /// <item>item 0: every <c>untilSourceTurnStart</c> window whose source is <paramref name="unit"/> and whose applied
    /// phase is below the current one ends (<see cref="ConditionEndReason.Expired"/>), in seq order across holders;</item>
    /// <item>item 2 (cooldowns, M1-23) is an empty seam;</item>
    /// <item>item 3: the uncapped tick batch: each harmful instance of the unit with a tick, in seq order, deals its
    /// value of its damage type through <see cref="Harm.Deal"/>. Before and after each, the batch stops if the run
    /// ended, the unit was removed or the instance is gone (M1-13 adds the 1/4-max-HP cap and keeps this stop);</item>
    /// <item>item 4: if the run ended, the events are returned undispatched; if the unit was removed, they are
    /// dispatched once and returned, and items 5 to 8 are skipped;</item>
    /// <item>items 5 (beneficial ticks) and 6 (aura pulses) are empty seams;</item>
    /// <item>item 7: the events so far, <see cref="TurnStarted"/> included, are dispatched on
    /// <paramref name="firings"/> (owner ruling 4), so <c>turnStarted</c> triggers fire inside the step;</item>
    /// <item>item 8, in combat only, and only if the run has not ended and the unit still holds the turn: the lost-turn
    /// check. A hard instance that is not <c>engineState</c> and not own-turn, whose source faction is not the unit's
    /// (or is the environment), costs the turn start: <see cref="TurnStartLost"/>, and
    /// <see cref="Unit.HardTurnsLostThisFight"/> + 1; when that reaches 2, Unyielding is granted at once, from the
    /// unit itself (CO 5.7). Its events are dispatched before the step returns.</item>
    /// </list>
    /// The result's events are already dispatched, with what they fired. Out of combat (a tick's turn-start step, plan
    /// call 13; CO 5.5) there is no <see cref="TurnStarted"/>, so no <c>turnStarted</c> trigger fires, item 8 (a per-fight
    /// count) is skipped, and the tick batch cannot take any unit, a monster included, below 1 hit point.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) TurnStart(
        RulesContext rules, GameState s, UnitId unit, ref int firings)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var events = ImmutableArray.CreateBuilder<GameEvent>();
        if (s.Combat is { } fight)
        {
            events.Add(new TurnStarted(unit, fight.Round));
        }

        // Item 0: the unit's untilSourceTurnStart windows opened before this step.
        long phase = s.Phase;
        s = EndWindows(rules, s, events, unit, start: true, before: phase);

        // Item 2: cooldowns count down: M1-23.

        // Item 3: the harmful tick batch.
        foreach (long seq in Ticks(rules, s, unit))
        {
            if (Gone(s, unit, seq))
            {
                break;
            }

            var instance = Find(s, seq)!;
            var (definition, state) = Conditions.Read(rules, instance);
            var part = new DamagePart(definition.Tick!.DamageType, 0, 0, state.Value);
            var (harm, next) = Harm.Deal(rules, s, unit, new DamageRoll([new PartRoll(part, [], state.Value)], false), tick: true);
            events.AddRange(harm.Events);
            s = next;
            if (Gone(s, unit, seq))
            {
                break;
            }
        }

        // Item 4: the zero-hit-point check. In M1 a unit at 0 has already died (or ended the run) in Harm.Deal.
        if (s.Ended is not null)
        {
            return (events.ToImmutable(), s);
        }

        if (s.FindUnit(unit) is null)
        {
            var (afterDeath, dead) = TriggerDispatch.Run(rules, s, events.ToImmutable(), ref firings);
            return (dead, afterDeath);
        }

        // Item 5: beneficial ticks (Regenerating): M1-13. Item 6: aura pulses: M2.

        // Item 7: start-of-turn triggers and checks, dispatched inside the step (owner ruling 4).
        var (dispatched, all) = TriggerDispatch.Run(rules, s, events.ToImmutable(), ref firings);
        s = dispatched;
        if (s.Ended is not null || s.Combat is null || !Holds(s, unit))
        {
            return (all, s);
        }

        // Item 8: the lost-turn check.
        var (lost, checkedState) = LostTurnCheck(rules, s, unit);
        if (lost.IsEmpty)
        {
            return (all, checkedState);
        }

        var (final, more) = TriggerDispatch.Run(rules, checkedState, lost, ref firings);
        return (all.AddRange(more), final);
    }

    /// <summary>
    /// <paramref name="unit"/>'s turn-end condition step (CO 5.4; plan call 9), after the caller has advanced the phase
    /// into it. It does not dispatch. First every condition instance of the unit that passes the counting test against
    /// this turn's Act phase (<c>Phase - 1</c>) counts the turn (<see cref="ConditionState.TurnsCounted"/> + 1). Then:
    /// <list type="bullet">
    /// <item>item 1, repeat saves: on a counted instance, each <c>saveEnds</c> or <c>staged</c> term (a <c>charges</c>
    /// fallback included) rolls the unit's save against <see cref="ConditionState.RecordedDifficulty"/>, one d20 on
    /// <c>Rng.Combat</c> (<see cref="Contests.SaveRoll"/>), emitting <see cref="SaveRolled"/>; a <c>saveEnds</c> term
    /// rolls from its <c>graceTurns</c>th counted turn. A success ends the instance
    /// (<see cref="ConditionEndReason.Saved"/>); a failed <c>staged</c> save moves <see cref="ConditionState.Value"/> on by
    /// 1; passing the stages, or reaching the cap in counted turns, ends it (<see cref="ConditionEndReason.Expired"/>);</item>
    /// <item>item 2, decay: a counted <c>decay</c> instance's value drops by 1;</item>
    /// <item>item 3, countdowns and expiry (<see cref="ConditionEndReason.Expired"/>): a hard instance held before an
    /// Unyielding granted at this turn's start (owner ruling 2); an own-turn instance (CO 5.2); Steeled, once the phase
    /// is past its applied phase (plan call 5); a counted <c>turns</c> or <c>sustained</c> term at its count; a
    /// <c>decay</c> instance at 0;</item>
    /// <item>item 4: a counted <c>trigger</c> term at its cap, or a <c>charges</c> fallback (<c>turns</c>,
    /// <c>trigger</c> or <c>sustained</c>) at its count (<see cref="ConditionEndReason.Expired"/>);</item>
    /// <item>item 5 (terrain, M2) is an empty seam;</item>
    /// <item>then every <c>untilSourceTurnEnd</c> window whose source is <paramref name="unit"/> ends
    /// (<see cref="ConditionEndReason.Expired"/>), in seq order across holders, unless it was opened in this turn (its
    /// applied phase at or after this turn's turn-start phase, <c>Phase - 2</c>; CO 5.6).</item>
    /// </list>
    /// Throws <see cref="InvalidOperationException"/> when a repeat save meets an instance with no recorded difficulty.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) TurnEnd(RulesContext rules, GameState s, UnitId unit)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        long act = s.Phase - 1;
        long turnStart = s.Phase - 2;
        var events = ImmutableArray.CreateBuilder<GameEvent>();

        // The counting test, once per instance for this turn end.
        var counted = new List<long>();
        var mine = new List<long>();
        foreach (var e in s.Effects)
        {
            if (e.Holder != unit || e.Condition is not { } c)
            {
                continue;
            }

            mine.Add(e.Seq);
            if (Counts(c, act))
            {
                counted.Add(e.Seq);
            }
        }

        foreach (long seq in counted)
        {
            s = Update(s, seq, c => c with { TurnsCounted = checked(c.TurnsCounted + 1) });
        }

        // Item 1: repeat saves.
        foreach (long seq in counted)
        {
            s = RepeatSaves(rules, s, events, unit, seq);
        }

        // Item 2: decay drops.
        foreach (long seq in counted)
        {
            if (Find(s, seq) is { Condition: { } c } && Has<DurationTerm.Decay>(Flatten(c.Terms)) && c.Value > 0)
            {
                s = Update(s, seq, d => d with { Value = d.Value - 1 });
            }
        }

        // Item 3: countdowns and expiry. Owner ruling 2: an Unyielding granted at this turn's start ends the hard
        // instances held before it.
        long? unyielding = null;
        foreach (var e in s.Effects)
        {
            if (e.Holder == unit && e.Condition is { } c && c.AppliedPhase == turnStart
                && string.Equals(e.EffectId, Conditions.UnyieldingId, StringComparison.Ordinal))
            {
                unyielding = e.Seq;
            }
        }

        foreach (long seq in mine)
        {
            if (Find(s, seq) is not { } instance)
            {
                continue;
            }

            var (definition, c) = Conditions.Read(rules, instance);
            bool isCounted = counted.Contains(seq);
            bool expired =
                (definition.IsHard && seq < unyielding)
                || c.OwnTurn
                || (string.Equals(instance.EffectId, Conditions.SteeledId, StringComparison.Ordinal) && s.Phase > c.AppliedPhase)
                || (isCounted && Reached(c, term => term switch
                {
                    DurationTerm.Turns t => t.N,
                    DurationTerm.Sustained t => t.Max,
                    _ => null,
                }))
                || (Has<DurationTerm.Decay>(Flatten(c.Terms)) && c.Value == 0);
            if (expired)
            {
                s = End(rules, s, events, seq, ConditionEndReason.Expired);
            }
        }

        // Item 4: trigger caps and the charges fallback.
        foreach (long seq in counted)
        {
            if (Find(s, seq) is not { Condition: { } c })
            {
                continue;
            }

            bool capped = Reached(c, term => term switch
            {
                DurationTerm.TriggerEnd t => t.Cap,
                DurationTerm.Charges { Fallback: DurationTerm.Turns t } => t.N,
                DurationTerm.Charges { Fallback: DurationTerm.TriggerEnd t } => t.Cap,
                DurationTerm.Charges { Fallback: DurationTerm.Sustained t } => t.Max,
                _ => null,
            });
            if (capped)
            {
                s = End(rules, s, events, seq, ConditionEndReason.Expired);
            }
        }

        // Item 5: terrain onTurnEnd applications: M2.

        // The unit's untilSourceTurnEnd windows, unless opened in this turn (CO 5.6).
        s = EndWindows(rules, s, events, unit, start: false, before: turnStart);
        return (events.ToImmutable(), s);
    }

    /// <summary>
    /// The round end (plan call 9; CO 5.6), run once per round wrap after the last turn's turn-end step and the boss
    /// actions that follow it: every window whose source is gone (the environment, no longer in the state, or not in
    /// the initiative order) ends with <see cref="ConditionEndReason.SourceGone"/>, in seq order. It does not dispatch.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) RoundEnd(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        return EndGoneWindows(rules, s);
    }

    /// <summary>
    /// The end of an out-of-combat tick (plan call 13; CO 5.6: out of combat a window lasts until the next tick), after
    /// every unit's turn-end step: every window whose source is gone (the environment, or no longer in the state) ends
    /// with <see cref="ConditionEndReason.SourceGone"/>, in seq order. It does not dispatch.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) TickEnd(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        return EndGoneWindows(rules, s);
    }

    /// <summary>
    /// Combat end's step 2 (plan call 15; CO 5.5, 7.2), after the current unit's turn-end step. The instances to end are
    /// collected first, holders in id order and each holder's in seq order: every window whose source is gone (as at a
    /// round end) with <see cref="ConditionEndReason.SourceGone"/>, and every other <c>combatOnly</c> instance with
    /// <see cref="ConditionEndReason.CombatEnd"/>. Each ends through <see cref="Conditions.End"/>. A <c>combatOnly</c>
    /// instance an end creates (the Steeled a hard instance grants) never joins the collected list: it ends at once, with
    /// <see cref="ConditionEndReason.CombatEnd"/>, before the next collected instance. So nothing <c>combatOnly</c>
    /// survives the fight. It does not dispatch.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) CombatEnd(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var due = new List<(long Seq, ConditionEndReason Reason)>();
        foreach (var unit in s.Units)
        {
            foreach (var e in s.Effects)
            {
                if (e.Holder != unit.Id || e.Condition is not { } c)
                {
                    continue;
                }

                if (IsWindow(c.Terms) && SourceGone(s, e))
                {
                    due.Add((e.Seq, ConditionEndReason.SourceGone));
                }
                else if (Conditions.Read(rules, e).Definition.Flags.CombatOnly)
                {
                    due.Add((e.Seq, ConditionEndReason.CombatEnd));
                }
            }
        }

        long fresh = s.NextEffectSeq;
        var events = ImmutableArray.CreateBuilder<GameEvent>();
        foreach (var (seq, reason) in due)
        {
            if (Find(s, seq) is null)
            {
                continue;
            }

            s = End(rules, s, events, seq, reason);

            // A combatOnly instance the end created ends at once, before the next collected one.
            while (FirstFreshCombatOnly(rules, s, fresh) is { } created)
            {
                s = End(rules, s, events, created, ConditionEndReason.CombatEnd);
            }
        }

        return (events.ToImmutable(), s);
    }

    // Every window whose source is gone ends with SourceGone, in seq order (a round end, a tick end).
    private static (ImmutableArray<GameEvent> Events, GameState Next) EndGoneWindows(RulesContext rules, GameState s)
    {
        var gone = new List<long>();
        foreach (var e in s.Effects)
        {
            if (e.Condition is { } c && IsWindow(c.Terms) && SourceGone(s, e))
            {
                gone.Add(e.Seq);
            }
        }

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        foreach (long seq in gone)
        {
            if (Find(s, seq) is not null)
            {
                s = End(rules, s, events, seq, ConditionEndReason.SourceGone);
            }
        }

        return (events.ToImmutable(), s);
    }

    // CO 5.6: the window's source is the environment; or, in combat, it is not in the initiative order (dead, gone or
    // never in it); or, out of combat, it is no longer in the state.
    private static bool SourceGone(GameState s, EffectInstance e) =>
        e.Source.Unit is not { } source
        || (s.Combat is { } combat ? !combat.Order.Contains(source) : s.FindUnit(source) is null);

    // The first combatOnly condition instance created at or after the seq `fresh`, or null.
    private static long? FirstFreshCombatOnly(RulesContext rules, GameState s, long fresh)
    {
        foreach (var e in s.Effects)
        {
            if (e.Seq >= fresh && e.Condition is not null && Conditions.Read(rules, e).Definition.Flags.CombatOnly)
            {
                return e.Seq;
            }
        }

        return null;
    }

    // Item 1 for one counted instance: each save term in canonical order, a charges fallback included, until one ends it.
    private static GameState RepeatSaves(RulesContext rules, GameState s, ImmutableArray<GameEvent>.Builder events, UnitId unit, long seq)
    {
        if (Find(s, seq) is not { Condition: { } first })
        {
            return s;
        }

        foreach (var term in Flatten(first.Terms))
        {
            if (Find(s, seq) is not { Condition: { } c } instance)
            {
                return s;
            }

            int n = c.TurnsCounted;
            (Save Save, int Cap, int From, int? Stages)? roll = term switch
            {
                DurationTerm.SaveEnds t => (t.Save, t.Cap, Math.Max(t.GraceTurns, 1), null),
                DurationTerm.Staged t => (t.Save, t.Cap, 1, t.Stages),
                _ => null,
            };
            if (roll is not { } r)
            {
                continue;
            }

            if (n < r.From)
            {
                if (n >= r.Cap)
                {
                    return End(rules, s, events, seq, ConditionEndReason.Expired);
                }

                continue;
            }

            // D-23: Conditions.Insert refuses a repeat save with no fight on, so none can roll out of combat here.
            if (s.Combat is null)
            {
                throw new InvalidOperationException(
                    $"Instance {seq} ({instance.EffectId}) would roll a repeat save with no fight on; out-of-combat repeat saves are not built (D-23).");
            }

            long difficulty = c.RecordedDifficulty
                ?? throw new InvalidOperationException(
                    $"Instance {seq} ({instance.EffectId}) has a repeat save but no recorded difficulty.");
            var (result, rolled) = Contests.SaveRoll(rules, s, unit, r.Save, difficulty);
            s = rolled;
            events.Add(new SaveRolled(unit, result));
            if (result.Success)
            {
                return End(rules, s, events, seq, ConditionEndReason.Saved);
            }

            int value = r.Stages is null ? c.Value : checked(c.Value + 1);
            if (value > r.Stages || n >= r.Cap)
            {
                return End(rules, s, events, seq, ConditionEndReason.Expired);
            }

            s = Update(s, seq, d => d with { Value = value });
        }

        return s;
    }

    // The lost-turn check (item 8; CO 5.7; owner ruling 2's grant). It does not dispatch.
    private static (ImmutableArray<GameEvent> Events, GameState Next) LostTurnCheck(RulesContext rules, GameState s, UnitId unit)
    {
        var holder = s.FindUnit(unit)!;
        bool lost = false;
        foreach (var e in s.Effects)
        {
            if (e.Holder != unit || e.Condition is null)
            {
                continue;
            }

            var (definition, c) = Conditions.Read(rules, e);
            if (definition.IsHard && !definition.HasTag(Conditions.EngineStateTag) && !c.OwnTurn
                && (c.SourceFaction is not { } faction || faction != holder.Faction))
            {
                lost = true;
                break;
            }
        }

        if (!lost)
        {
            return (ImmutableArray<GameEvent>.Empty, s);
        }

        int count = checked(holder.HardTurnsLostThisFight + 1);
        s = s with { Units = s.Units.SetItem(s.Units.IndexOf(holder), holder with { HardTurnsLostThisFight = count }) };
        var events = ImmutableArray.Create<GameEvent>(new TurnStartLost(unit, count));
        if (count == 2)
        {
            var (granted, next) = Conditions.Insert(rules, s, Conditions.UnyieldingId, unit, new EffectSource(unit), ConditionInsert.None);
            events = events.AddRange(granted);
            s = next;
        }

        return (events, s);
    }

    // Ends the windows whose source is `unit`, of the start or end kind, opened before the phase `before`, in seq order.
    private static GameState EndWindows(
        RulesContext rules, GameState s, ImmutableArray<GameEvent>.Builder events, UnitId unit, bool start, long before)
    {
        var due = new List<long>();
        foreach (var e in s.Effects)
        {
            if (e.Condition is { } c && e.Source.Unit == unit && c.AppliedPhase < before
                && (start ? Has<DurationTerm.UntilSourceTurnStart>(Flatten(c.Terms)) : Has<DurationTerm.UntilSourceTurnEnd>(Flatten(c.Terms))))
            {
                due.Add(e.Seq);
            }
        }

        foreach (long seq in due)
        {
            if (Find(s, seq) is not null)
            {
                s = End(rules, s, events, seq, ConditionEndReason.Expired);
            }
        }

        return s;
    }

    private static GameState End(RulesContext rules, GameState s, ImmutableArray<GameEvent>.Builder events, long seq, ConditionEndReason reason)
    {
        var (ended, next) = Conditions.End(rules, s, seq, reason);
        events.AddRange(ended);
        return next;
    }

    // The harmful tick instances of `unit`, in seq order.
    private static List<long> Ticks(RulesContext rules, GameState s, UnitId unit)
    {
        var ticks = new List<long>();
        foreach (var e in s.Effects)
        {
            if (e.Holder == unit && e.Condition is not null)
            {
                var (definition, _) = Conditions.Read(rules, e);
                if (definition.Polarity == ConditionPolarity.Harmful && definition.Tick is not null)
                {
                    ticks.Add(e.Seq);
                }
            }
        }

        return ticks;
    }

    // The batch stops: the run ended, the unit was removed, or the instance is gone.
    private static bool Gone(GameState s, UnitId unit, long seq) =>
        s.Ended is not null || s.FindUnit(unit) is null || Find(s, seq) is null;

    // The unit holds a live turn.
    private static bool Holds(GameState s, UnitId unit) =>
        s.Combat is { TurnVacated: false, BeforeRound1: false } c && c.Current < c.Order.Length && c.CurrentUnit == unit;

    // Some term gives a count (through `count`) that the instance's counted turns have reached.
    private static bool Reached(ConditionState c, Func<DurationTerm, int?> count)
    {
        foreach (var term in c.Terms)
        {
            if (count(term) is { } n && c.TurnsCounted >= n)
            {
                return true;
            }
        }

        return false;
    }

    // The terms, each followed by its charges fallback where it has one.
    private static IEnumerable<DurationTerm> Flatten(ImmutableArray<DurationTerm> terms)
    {
        foreach (var term in terms)
        {
            yield return term;
            if (term is DurationTerm.Charges charges)
            {
                yield return charges.Fallback;
            }
        }
    }

    private static bool Has<T>(IEnumerable<DurationTerm> terms)
        where T : DurationTerm
    {
        foreach (var term in terms)
        {
            if (term is T)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsWindow(ImmutableArray<DurationTerm> terms) =>
        Has<DurationTerm.UntilSourceTurnStart>(Flatten(terms)) || Has<DurationTerm.UntilSourceTurnEnd>(Flatten(terms));

    private static GameState Update(GameState s, long seq, Func<ConditionState, ConditionState> change)
    {
        for (int i = 0; i < s.Effects.Length; i++)
        {
            if (s.Effects[i].Seq == seq)
            {
                var e = s.Effects[i];
                return s with { Effects = s.Effects.SetItem(i, e with { Condition = change(e.Condition!) }) };
            }
        }

        throw new InvalidOperationException($"No active effect has seq {seq}.");
    }

    private static EffectInstance? Find(GameState s, long seq)
    {
        foreach (var e in s.Effects)
        {
            if (e.Seq == seq)
            {
                return e;
            }
        }

        return null;
    }
}
