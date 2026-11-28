using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Determinism;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Conditions;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// Condition instances (M1-12a plan calls 5 to 7): the core definitions every rules context holds, the one way an
/// instance appears (<see cref="Insert"/>), the one way one ends (<see cref="End"/>) and the rules-aware read of an
/// instance's condition part (<see cref="Read"/>). M1-12b puts the CO 9 pipeline in front of <see cref="Insert"/>.
/// </summary>
public static class Conditions
{
    /// <summary>Steeled's fixed id (CO 7.2).</summary>
    public const string SteeledId = "cond.steeled";

    /// <summary>Unyielding's fixed id (CO 5.7).</summary>
    public const string UnyieldingId = "cond.unyielding";

    /// <summary>The tag of an engine-granted state such as Surprised (CO 18.2): it grants Steeled when it ends (CO 7.2).</summary>
    public const string EngineStateTag = "engineState";

    /// <summary>The file name the core definitions carry.</summary>
    public const string CoreFile = "core/conditions";

    /// <summary>
    /// The core definitions (plan call 5), ordinal by id, held by every rules context whatever the content: Steeled and
    /// Unyielding, beneficial and <c>combatOnly</c>, with no duration terms (Steeled's end is an engine window) and no
    /// content. Their effects (immunity, downgrade) are M1-12b's; their display and tests M1-13's.
    /// </summary>
    public static ImmutableArray<ConditionDefinition> CoreDefinitions { get; } =
    [
        Core(SteeledId),
        Core(UnyieldingId),
    ];

    /// <summary>True if <paramref name="id"/> is a core definition's id.</summary>
    public static bool IsCoreId(string id) =>
        string.Equals(id, SteeledId, StringComparison.Ordinal) || string.Equals(id, UnyieldingId, StringComparison.Ordinal);

    /// <summary>
    /// Inserts an instance of <paramref name="conditionId"/> on <paramref name="holder"/> from <paramref name="source"/>
    /// (plan call 6), emitting <see cref="ConditionApplied"/> with <c>New: true</c>. It stamps
    /// <see cref="ConditionState.AppliedPhase"/> with <see cref="GameState.Phase"/>; resolves the terms from the first of
    /// the options' duration override, the record's out-of-combat list when no fight is on and the list is not empty, and
    /// the record's duration, in canonical order; takes the charges from a <c>charges</c> term; records the source's
    /// faction; and sets <see cref="ConditionState.OwnTurn"/> for a hard condition inserted in the wearer's own-turn
    /// window (the Act phase of its own turn in combat, the step phase out of combat; CO 5.2). A second grant of a core
    /// definition re-stamps the existing instance's applied phase instead, keeping its seq and source, and emits
    /// <see cref="ConditionApplied"/> with <c>New: false</c>. Throws <see cref="ArgumentException"/> for an unknown
    /// condition, holder or source unit, a value outside the intensity (1 for binary, charges and staged; up to the cap
    /// for valued, decay and accumulating), a sustainer that is no live instance, or an override that is empty, malformed,
    /// names an unknown rule event, or breaks the CO 5.3 band for the holder's side; and
    /// <see cref="InvalidOperationException"/> if the holder already holds an instance of the condition (6.1 handling is
    /// M1-12b's), or, with no override, the record's in-combat duration breaks the party-facing band on a party holder.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) Insert(
        RulesContext rules, GameState s, string conditionId, UnitId holder, EffectSource source, ConditionInsert options)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(conditionId);
        ArgumentNullException.ThrowIfNull(options);
        var definition = rules.Condition(conditionId);
        var wearer = s.FindUnit(holder) ?? throw new ArgumentException($"Holder unit {holder.Value} is not in the state.", nameof(holder));
        Faction? faction = null;
        if (source.Unit is { } from)
        {
            faction = (s.FindUnit(from) ?? throw new ArgumentException($"Source unit {from.Value} is not in the state.", nameof(source))).Faction;
        }

        if (Held(s, conditionId, holder) is { } existing)
        {
            if (!IsCoreId(conditionId))
            {
                throw new InvalidOperationException(
                    $"Unit {holder.Value} already holds {conditionId} (instance {existing.Seq}); re-application is the pipeline's (M1-12b).");
            }

            // Plan call 5: a second grant re-stamps the existing instance.
            var (_, held) = Read(rules, existing);
            var restamped = existing with { Condition = held with { AppliedPhase = s.Phase } };
            return (
                [new ConditionApplied(existing.Seq, conditionId, holder, existing.Source, held.Value, false)],
                s with { Effects = s.Effects.SetItem(s.Effects.IndexOf(existing), restamped) });
        }

        int value = Value(definition, options.Value);
        if (options.SustainedBy is { } sustainer && !s.Effects.Any(e => e.Seq == sustainer))
        {
            throw new ArgumentException($"The sustainer {sustainer} is not a live instance.", nameof(options));
        }

        ImmutableArray<DurationTerm> terms;
        if (options.Duration is { } given)
        {
            CheckOverride(rules, definition, given, wearer.Faction == Faction.Party);
            terms = given;
        }
        else if (s.Combat is null && !definition.OutOfCombat.IsEmpty)
        {
            terms = definition.OutOfCombat;
        }
        else
        {
            // The record's own duration was checked enemy-facing at load; on a party holder it meets the party-facing band
            // too (CO 5.3: party-facing hard control is turns:1, or a trigger with cap 1).
            terms = definition.Duration;
            if (wearer.Faction == Faction.Party
                && ConditionRules.CheckBand(definition, terms, "duration", "condition", partyFacing: true) is { } broken)
            {
                throw new InvalidOperationException(
                    $"{definition.Id} cannot be placed on party unit {holder.Value} without a duration override: "
                    + $"{broken.Field}: {broken.Message}");
            }
        }

        var sorted = new List<DurationTerm>(terms);
        sorted.Sort(DurationTermOrder.Instance);
        int? charges = null;
        foreach (var term in sorted)
        {
            if (term is DurationTerm.Charges c)
            {
                charges = c.N;
                break;
            }
        }

        var state = new ConditionState(
            value,
            s.Phase,
            0,
            [.. sorted],
            charges,
            options.RecordedDifficulty,
            faction,
            options.SustainedBy,
            definition.IsHard && InOwnTurnWindow(s, holder));
        long seq = s.NextEffectSeq;
        var next = Effects.Apply(s, conditionId, holder, source, state);
        return ([new ConditionApplied(seq, conditionId, holder, source, value, true)], next);
    }

    /// <summary>
    /// Ends the condition instance <paramref name="seq"/> for <paramref name="reason"/> (plan call 7): removes it and emits
    /// <see cref="ConditionEnded"/>; then, at once, ends every instance it sustains (<see cref="ConditionState.SustainedBy"/>)
    /// with <see cref="ConditionEndReason.Ended"/> (with <see cref="ConditionEndReason.CombatStart"/> when this end is a
    /// combat-start end, so the whole cascade is), in seq order, each through this method; then, if the ended instance
    /// was hard or carries <see cref="EngineStateTag"/>, grants Steeled to its holder (CO 7.2), from the holder itself,
    /// unless the reason is <see cref="ConditionEndReason.CombatStart"/>. So no Steeled follows any end inside the
    /// combat-start step, the cascade included (CO 5.5, 7.2). Every end except a dead holder's own instances comes
    /// through here; the holder must be in the state. Throws
    /// <see cref="ArgumentException"/> if no instance has the seq or it has no condition part, and
    /// <see cref="InvalidOperationException"/> as <see cref="Read"/> does.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) End(
        RulesContext rules, GameState s, long seq, ConditionEndReason reason)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var instance = Find(s, seq) ?? throw new ArgumentException($"No active effect has seq {seq}.", nameof(seq));
        if (instance.Condition is null)
        {
            throw new ArgumentException($"Instance {seq} ({instance.EffectId}) is not a condition instance.", nameof(seq));
        }

        var (definition, _) = Read(rules, instance);
        if (s.FindUnit(instance.Holder) is null)
        {
            throw new InvalidOperationException($"Instance {seq}'s holder {instance.Holder.Value} is not in the state.");
        }

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        s = Effects.Remove(s, seq);
        events.Add(new ConditionEnded(seq, instance.EffectId, instance.Holder, reason));

        // The sustained instances, in seq order; each may already be gone, ended by an earlier one's cascade.
        var sustained = s.Effects.Where(e => e.Condition?.SustainedBy == seq).Select(e => e.Seq).ToList();
        foreach (long dependent in sustained)
        {
            if (Find(s, dependent) is not null)
            {
                // A combat-start end carries its reason down the cascade, so no dependent grants Steeled (CO 7.2).
                var cascade = reason == ConditionEndReason.CombatStart ? ConditionEndReason.CombatStart : ConditionEndReason.Ended;
                var (more, next) = End(rules, s, dependent, cascade);
                events.AddRange(more);
                s = next;
            }
        }

        if ((definition.IsHard || definition.HasTag(EngineStateTag)) && reason != ConditionEndReason.CombatStart)
        {
            var (granted, next) = Insert(rules, s, SteeledId, instance.Holder, new EffectSource(instance.Holder), ConditionInsert.None);
            events.AddRange(granted);
            s = next;
        }

        return (events.ToImmutable(), s);
    }

    /// <summary>
    /// The CO 5.5 combat-start condition step (plan call 14; M1-10 rulings record 5, step 2), run between combat start's
    /// checks and its step 4 (<see cref="Dungeon.Core.Combat.Turns.FightReady"/>). It draws nothing and advances no phase. Over the
    /// condition instances in seq order:
    /// <list type="bullet">
    /// <item>an instance whose definition is flagged <c>combatStartEnds</c> ends with
    /// <see cref="ConditionEndReason.CombatStart"/> through <see cref="End"/>; what it sustains ends with it, also with
    /// <see cref="ConditionEndReason.CombatStart"/>, and no Steeled follows any of these ends (CO 7.2 excepts
    /// <c>combatStart</c>);</item>
    /// <item>an instance running on its record's out-of-combat terms switches to the record's in-combat
    /// <see cref="ConditionDefinition.Duration"/>, counted from now, when that list's effective end comes sooner than what
    /// it has left (CO 5.5, 6.3): its <see cref="ConditionState.Terms"/> are replaced, its
    /// <see cref="ConditionState.TurnsCounted"/> goes to 0 and its charges follow the new terms, with no event. An
    /// instance that does not switch keeps its terms and count.</item>
    /// </list>
    /// Then every unit's <see cref="Unit.HardTurnsLostThisFight"/> resets to 0 (CO 5.7). The messages are returned to
    /// be held; combat start sorts them (<see cref="SortHeld"/>) and emits them after <see cref="CombatStarted"/>. Throws
    /// <see cref="InvalidOperationException"/> as <see cref="Read"/> does, or when a switch would put a record's in-combat
    /// duration that breaks the party-facing band on a party holder (as <see cref="Insert"/> does).
    /// </summary>
    internal static (ImmutableArray<GameEvent> Held, GameState Next) CombatStart(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var held = ImmutableArray.CreateBuilder<GameEvent>();
        var seqs = s.Effects.Where(e => e.Condition is not null).Select(e => e.Seq).ToList();
        foreach (long seq in seqs)
        {
            if (Find(s, seq) is not { } instance)
            {
                continue;
            }

            var (definition, state) = Read(rules, instance);
            if (definition.Flags.CombatStartEnds)
            {
                var (ended, next) = End(rules, s, seq, ConditionEndReason.CombatStart);
                held.AddRange(ended);
                s = next;
                continue;
            }

            if (Switches(definition, state) is not { } terms)
            {
                continue;
            }

            if (s.FindUnit(instance.Holder)!.Faction == Faction.Party
                && ConditionRules.CheckBand(definition, terms, "duration", "condition", partyFacing: true) is { } broken)
            {
                throw new InvalidOperationException(
                    $"{definition.Id} (instance {seq}) cannot switch to its in-combat duration on party unit "
                    + $"{instance.Holder.Value}: {broken.Field}: {broken.Message}");
            }

            var switched = state with { Terms = terms, TurnsCounted = 0, Charges = ChargesOf(terms) };
            s = s with { Effects = s.Effects.SetItem(s.Effects.IndexOf(instance), instance with { Condition = switched }) };
        }

        s = s with
        {
            Units = s.Units.Select(u => u.HardTurnsLostThisFight == 0 ? u : u with { HardTurnsLostThisFight = 0 }).ToImmutableArray(),
        };
        return (held.ToImmutable(), s);
    }

    /// <summary>
    /// Combat start's held condition messages in their CO 5.5 order (plan call 14; M1-10 rulings record 5, step 7):
    /// listed by holder, by unit id, then in the order the conditions were applied (the seq of the instance each message
    /// is about), then in emission order. Throws <see cref="ArgumentException"/> for a message that is not a
    /// <see cref="ConditionEnded"/> or <see cref="ConditionApplied"/>.
    /// </summary>
    internal static ImmutableArray<GameEvent> SortHeld(ImmutableArray<GameEvent> held)
    {
        var keyed = new List<HeldMessage>(held.Length);
        for (int i = 0; i < held.Length; i++)
        {
            var (holder, seq) = held[i] switch
            {
                ConditionEnded e => (e.Holder, e.Seq),
                ConditionApplied a => (a.Holder, a.Seq),
                _ => throw new ArgumentException($"{held[i].GetType().Name} is not a held condition message.", nameof(held)),
            };
            keyed.Add(new HeldMessage(holder.Value, seq, i, held[i]));
        }

        keyed.Sort(HeldOrder.Instance);
        return [.. keyed.Select(k => k.Message)];
    }

    // The in-combat terms an instance on its record's out-of-combat terms switches to, or null when it keeps its own
    // (CO 5.5: "if that is shorter"; plan call 14).
    private static ImmutableArray<DurationTerm>? Switches(ConditionDefinition d, ConditionState c)
    {
        if (d.OutOfCombat.IsEmpty || d.Duration.IsEmpty || !c.Terms.SequenceEqual(Sorted(d.OutOfCombat)))
        {
            return null;
        }

        var terms = Sorted(d.Duration);
        long? left = Left(c.Terms, c);
        long? fresh = Left(terms, c with { TurnsCounted = 0 });
        return fresh is { } shorter && (left is null || shorter < left) ? terms : null;
    }

    // The counted turns an instance on `terms` has left before its first terminator's effective end (CO 6.3): turns for
    // turns; the cap for saveEnds, trigger, staged and sustained; a charges term's fallback; the value for decay; 1 for
    // a window (it ends by the source's next turn); null (never) for untilRest, untilCamp and a trigger without a cap.
    private static long? Left(ImmutableArray<DurationTerm> terms, ConditionState c)
    {
        long? least = null;
        foreach (var term in terms)
        {
            long? left = term is DurationTerm.Decay ? c.Value : EffectiveEnd(term) is { } end ? Math.Max(0, end - c.TurnsCounted) : null;
            if (left is { } l && (least is null || l < least))
            {
                least = l;
            }
        }

        return least;
    }

    private static long? EffectiveEnd(DurationTerm term) => term switch
    {
        DurationTerm.Turns t => t.N,
        DurationTerm.SaveEnds t => t.Cap,
        DurationTerm.TriggerEnd t => t.Cap,
        DurationTerm.Sustained t => t.Max,
        DurationTerm.Staged t => t.Cap,
        DurationTerm.Charges t => EffectiveEnd(t.Fallback),
        DurationTerm.UntilSourceTurnStart or DurationTerm.UntilSourceTurnEnd => 1,
        _ => null,
    };

    private static ImmutableArray<DurationTerm> Sorted(ImmutableArray<DurationTerm> terms)
    {
        var sorted = new List<DurationTerm>(terms);
        sorted.Sort(DurationTermOrder.Instance);
        return [.. sorted];
    }

    private static int? ChargesOf(ImmutableArray<DurationTerm> terms)
    {
        foreach (var term in terms)
        {
            if (term is DurationTerm.Charges c)
            {
                return c.N;
            }
        }

        return null;
    }

    // One held message with its sort key: holder id, the seq it is about, its emission index.
    private sealed record HeldMessage(int Holder, long Seq, int Index, GameEvent Message);

    // Holder, then seq, then emission index: a total order, since emission indexes are unique.
    [TotalComparer]
    private sealed class HeldOrder : IComparer<HeldMessage>
    {
        public static readonly HeldOrder Instance = new();

        public int Compare(HeldMessage? x, HeldMessage? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);
            int c = x.Holder.CompareTo(y.Holder);
            if (c == 0)
            {
                c = x.Seq.CompareTo(y.Seq);
            }

            return c != 0 ? c : x.Index.CompareTo(y.Index);
        }
    }

    /// <summary>
    /// The definition and condition part of <paramref name="instance"/>, where rules are present (plan call 2). Throws
    /// <see cref="ArgumentException"/> if it has no condition part, and <see cref="InvalidOperationException"/> if its id
    /// names no condition definition, or it is marked own-turn but its definition is not hard.
    /// </summary>
    internal static (ConditionDefinition Definition, ConditionState State) Read(RulesContext rules, EffectInstance instance)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(instance);
        var state = instance.Condition
            ?? throw new ArgumentException($"Instance {instance.Seq} ({instance.EffectId}) is not a condition instance.", nameof(instance));
        var definition = rules.FindCondition(instance.EffectId)
            ?? throw new InvalidOperationException(
                $"Instance {instance.Seq} has a condition part, but \"{instance.EffectId}\" names no condition definition.");
        if (state.OwnTurn && !definition.IsHard)
        {
            throw new InvalidOperationException(
                $"Instance {instance.Seq} ({instance.EffectId}) is marked own-turn, but only a hard condition can be.");
        }

        return (definition, state);
    }

    // CO 5.2: the Act phase of the wearer's own turn in combat; out of combat, the tick's step phase.
    private static bool InOwnTurnWindow(GameState s, UnitId holder)
    {
        if (s.Step != ClockStep.Act)
        {
            return false;
        }

        if (s.Combat is not { } combat)
        {
            return true;
        }

        return !combat.TurnVacated && !combat.BeforeRound1 && combat.Current < combat.Order.Length && combat.CurrentUnit == holder;
    }

    private static int Value(ConditionDefinition d, int? given)
    {
        int value = given ?? 1;
        int max = d.Intensity.Kind switch
        {
            IntensityKind.Valued or IntensityKind.Decay => d.Intensity.ValueCap ?? 1,
            IntensityKind.Accumulating => d.Intensity.LevelCap ?? 1,
            _ => 1,
        };
        return value >= 1 && value <= max
            ? value
            : throw new ArgumentException($"{d.Id} takes a value from 1 to {max}; {value} was given.", nameof(given));
    }

    // CO 17.1: an override is validated against the CO 5.3 band for the side it targets, and its events must be known.
    private static void CheckOverride(RulesContext rules, ConditionDefinition d, ImmutableArray<DurationTerm> terms, bool partyFacing)
    {
        const string at = "duration";
        if (terms.IsDefaultOrEmpty)
        {
            throw new ArgumentException($"A duration override for {d.Id} needs at least one term.", nameof(terms));
        }

        var broken = ConditionRules.CheckTerms(d, terms, at, "condition")
            ?? ConditionRules.CheckBand(d, terms, at, "condition", partyFacing)
            ?? rules.CheckTermEvents(terms, at);
        if (broken is { } problem)
        {
            throw new ArgumentException($"The duration override for {d.Id} is refused at {problem.Field}: {problem.Message}", nameof(terms));
        }
    }

    private static EffectInstance? Held(GameState s, string conditionId, UnitId holder)
    {
        foreach (var e in s.Effects)
        {
            if (e.Condition is not null && e.Holder == holder && string.Equals(e.EffectId, conditionId, StringComparison.Ordinal))
            {
                return e;
            }
        }

        return null;
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

    private static ConditionDefinition Core(string id) =>
        new(
            CoreFile,
            id,
            ConditionPolarity.Beneficial,
            null,
            ImmutableArray<string>.Empty,
            null,
            Denies.None,
            ImmutableArray<DurationTerm>.Empty,
            ImmutableArray<DurationTerm>.Empty,
            Intensity.Binary,
            ConditionFlags.None with { CombatOnly = true },
            ImmutableArray<string>.Empty,
            ImmutableArray<string>.Empty,
            null,
            ImmutableArray<Modifier>.Empty,
            ImmutableArray<Trigger>.Empty);
}
