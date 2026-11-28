using System;
using System.Collections.Immutable;
using Dungeon.Core.Canonical;

namespace Dungeon.Core.Conditions;

/// <summary>
/// Which step of a turn the phase clock is in (M1-12a plan call 1; CO 5.2): <see cref="None"/> out of any turn (out of
/// combat between ticks, combat start, the opener, before round 1), else a turn's turn-start step, Act phase or
/// turn-end step. Out of combat a tick's three phases are shared by every unit.
/// </summary>
public enum ClockStep
{
    /// <summary>No turn step: nothing applied now is in an own-turn window.</summary>
    None,

    /// <summary>A turn-start step (CO 5.4).</summary>
    TurnStart,

    /// <summary>An Act phase: the own-turn window (CO 5.2).</summary>
    Act,

    /// <summary>A turn-end step (CO 5.4).</summary>
    TurnEnd,
}

/// <summary>
/// Why a condition instance ended (CO 8; M1-12a plan call 7). <see cref="Consumed"/> is emitted from M1-12b; the cure and
/// relation reasons arrive with M1-12b and M1-13.
/// </summary>
public enum ConditionEndReason
{
    /// <summary>Its duration ran out.</summary>
    Expired,

    /// <summary>A repeat save succeeded.</summary>
    Saved,

    /// <summary>Combat started (the <c>combatStartEnds</c> flag); no Steeled follows.</summary>
    CombatStart,

    /// <summary>Combat ended (<c>combatOnly</c>).</summary>
    CombatEnd,

    /// <summary>Its source died or left (<c>sourceTied</c>, or a window whose source is gone).</summary>
    SourceGone,

    /// <summary>Its charges were used up.</summary>
    Consumed,

    /// <summary>Its sustainer ended.</summary>
    Ended,
}

/// <summary>
/// The condition part of an effect instance (M1-12a plan call 2; CO 17.2). <see cref="Value"/> is the value, level or
/// stage; <see cref="AppliedPhase"/> the phase clock at insert (CO 5.2); <see cref="TurnsCounted"/> the wearer turn ends
/// that passed the counting test, from which every terminator's remainder is derived; <see cref="Terms"/> the active
/// terminator list, in canonical order (<see cref="DurationTermOrder"/>); <see cref="Charges"/> the charges left, for a
/// <c>charges</c> term; <see cref="RecordedDifficulty"/> the difficulty recorded at application;
/// <see cref="SourceFaction"/> the source's faction at insert (null: the environment), kept after the source dies;
/// <see cref="SustainedBy"/> the seq of the instance that sustains it; <see cref="OwnTurn"/> set when a hard instance was
/// inserted in its wearer's own-turn window; <see cref="OnOutOfCombatTerms"/> set when <see cref="Conditions.Insert"/>
/// chose the record's out-of-combat list (no override, no fight on), the one case the combat-start step may switch
/// (CO 5.5). The record compares <see cref="Terms"/> by reference; compare terms with
/// <c>SequenceEqual</c>.
/// </summary>
public sealed record ConditionState(
    int Value,
    long AppliedPhase,
    int TurnsCounted,
    ImmutableArray<DurationTerm> Terms,
    int? Charges,
    long? RecordedDifficulty,
    Faction? SourceFaction,
    long? SustainedBy,
    bool OwnTurn,
    bool OnOutOfCombatTerms) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        if (Terms.IsDefault)
        {
            throw new ArgumentException("A condition's terms are uninitialised.");
        }

        w.BeginRecord(10);
        w.Int32(0, Value);
        w.Int64(1, AppliedPhase);
        w.Int32(2, TurnsCounted);
        w.List(3, Terms, (iw, t) => iw.Record(0, t));
        w.OptionalInt32(4, Charges);
        w.OptionalRecord(5, RecordedDifficulty is { } d ? new Long(d) : null);
        w.OptionalString(6, SourceFaction is { } f ? Enum.GetName(f) ?? throw new ArgumentException($"Unknown faction {f}.") : null);
        w.OptionalRecord(7, SustainedBy is { } seq ? new Long(seq) : null);
        w.Bool(8, OwnTurn);
        w.Bool(9, OnOutOfCombatTerms);
        w.EndRecord();
    }

    // An optional long (the writer has no optional Int64).
    private sealed record Long(long Value) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(1);
            w.Int64(0, Value);
            w.EndRecord();
        }
    }
}

/// <summary>
/// The options of <see cref="Conditions.Insert"/> (M1-12a plan call 6): the value (null: 1), the recorded difficulty,
/// a duration override (CO 17.1; checked against the CO 5.3 band for the target's side) and the sustaining instance's seq.
/// </summary>
public sealed record ConditionInsert(
    int? Value = null,
    long? RecordedDifficulty = null,
    ImmutableArray<DurationTerm>? Duration = null,
    long? SustainedBy = null)
{
    /// <summary>No options.</summary>
    public static ConditionInsert None { get; } = new();
}
