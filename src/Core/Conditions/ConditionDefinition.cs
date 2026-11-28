using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Determinism;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.Conditions;

/// <summary>A condition's polarity (CO 17.1): written <c>harmful</c>, <c>beneficial</c> or <c>token</c>.</summary>
public enum ConditionPolarity
{
    /// <summary>Harmful: it has a tier and its CO 5.3 band.</summary>
    Harmful,

    /// <summary>Beneficial: 1 to 10 turns (CO 5.3).</summary>
    Beneficial,

    /// <summary>A token: until used, with a fallback of at most 3 turns (CO 5.3).</summary>
    Token,
}

/// <summary>A harmful condition's tier (CO 5.3): written <c>token</c>, <c>minor</c>, <c>major</c> or <c>hard</c>.</summary>
public enum ConditionTier
{
    /// <summary>The token band.</summary>
    Token,

    /// <summary>At most 3 turns.</summary>
    Minor,

    /// <summary>At most 2 turns, or save-ends or a trigger with cap 3.</summary>
    Major,

    /// <summary>1 turn, or save-ends or a trigger with cap 2 enemy-facing only; costs turn starts (CO 5.7).</summary>
    Hard,
}

/// <summary>How a condition's value behaves (CO 6; CO 17.1 <c>intensity.kind</c>).</summary>
public enum IntensityKind
{
    /// <summary>On or off; the value is 1.</summary>
    Binary,

    /// <summary>A value up to <see cref="Intensity.ValueCap"/>.</summary>
    Valued,

    /// <summary>A value that drops by 1 at each counted turn end (the <c>decay</c> duration).</summary>
    Decay,

    /// <summary>A level up to <see cref="Intensity.LevelCap"/>.</summary>
    Accumulating,

    /// <summary>A stage up to <see cref="Intensity.Stages"/> (the <c>staged</c> duration).</summary>
    Staged,

    /// <summary>Charges (the <c>charges</c> duration).</summary>
    Charges,
}

/// <summary>
/// One terminator of a condition's duration (CO 5.1). A duration is a list of terminators, and the instance ends on
/// whichever comes first. Each term counts on the wearer's turns by the CO 5.2 counting test, except the windows, which
/// run on the source's (CO 5.6), and the lingering terms, which count only rest and camp events. Terms have a canonical
/// order (<see cref="DurationTermOrder"/>): the declaration order of the term types, then their fields.
/// </summary>
public abstract record DurationTerm : ICanonical
{
    // Only the nested records derive from it.
    private DurationTerm()
    {
    }

    /// <summary>The term's key in JSON and its tag in the canonical bytes.</summary>
    public abstract string Tag { get; }

    /// <summary>The term type's place in the canonical order.</summary>
    internal abstract int Rank { get; }

    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        // One tagged layout for every term: the tag, then each field (absent or 0 where the type has none).
        var (n, save, cap, grace, on, fallback, max, stages, then) = this switch
        {
            Turns t => (t.N, (Save?)null, 0, 0, (string?)null, (DurationTerm?)null, 0, 0, (string?)null),
            SaveEnds t => (0, t.Save, t.Cap, t.GraceTurns, null, null, 0, 0, null),
            Charges t => (t.N, null, 0, 0, t.On, t.Fallback, 0, 0, null),
            TriggerEnd t => (0, null, t.Cap ?? 0, 0, t.Event, null, 0, 0, null),
            Sustained t => (0, null, 0, 0, null, null, t.Max, 0, null),
            Staged t => (0, t.Save, t.Cap, 0, null, null, 0, t.Stages, t.Then),
            _ => (0, null, 0, 0, null, null, 0, 0, null),
        };
        // A cap of 0 is an absent one: every given cap is at least 1.
        w.BeginRecord(10);
        w.String(0, Tag);
        w.Int32(1, n);
        w.OptionalString(2, save is { } sv ? Enum.GetName(sv) : null);
        w.Int32(3, cap);
        w.Int32(4, grace);
        w.OptionalString(5, on);
        w.OptionalRecord(6, fallback);
        w.Int32(7, max);
        w.Int32(8, stages);
        w.OptionalString(9, then);
        w.EndRecord();
    }

    /// <summary><c>turns:N</c>: N counted wearer turns.</summary>
    public sealed record Turns(int N) : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "turns";

        internal override int Rank => 0;
    }

    /// <summary><c>untilSourceTurnStart</c>: a window that ends when the source's next turn-start step begins.</summary>
    public sealed record UntilSourceTurnStart : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "untilSourceTurnStart";

        internal override int Rank => 1;
    }

    /// <summary><c>untilSourceTurnEnd</c>: a window that ends when the source's next turn-end step completes.</summary>
    public sealed record UntilSourceTurnEnd : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "untilSourceTurnEnd";

        internal override int Rank => 2;
    }

    /// <summary>
    /// <c>saveEnds {save, cap, graceTurns?}</c>: a repeat <paramref name="Save"/> at counted turn ends, first at the
    /// <paramref name="GraceTurns"/>th (0: none), ending after <paramref name="Cap"/> counted turns at most.
    /// </summary>
    public sealed record SaveEnds(Save Save, int Cap, int GraceTurns) : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "saveEnds";

        internal override int Rank => 3;
    }

    /// <summary>
    /// <c>charges:N {on, fallback}</c>: used up by the rule event <paramref name="On"/>, or ended by
    /// <paramref name="Fallback"/>, which is any term but <see cref="Charges"/>.
    /// </summary>
    public sealed record Charges(int N, string On, DurationTerm Fallback) : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "charges";

        internal override int Rank => 4;
    }

    /// <summary><c>decay</c>: the value drops by 1 at each counted turn end, and the instance ends at 0.</summary>
    public sealed record Decay : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "decay";

        internal override int Rank => 5;
    }

    /// <summary><c>trigger {event, cap?}</c>: ends on the rule event <paramref name="Event"/>, or after <paramref name="Cap"/> counted turns.</summary>
    public sealed record TriggerEnd(string Event, int? Cap) : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "trigger";

        internal override int Rank => 6;
    }

    /// <summary><c>sustained {max}</c>: ends with the sustainer, or after <paramref name="Max"/> counted turns.</summary>
    public sealed record Sustained(int Max) : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "sustained";

        internal override int Rank => 7;
    }

    /// <summary>
    /// <c>staged {stages, save, cap, then?}</c> (CO 6.5): a repeat <paramref name="Save"/> each counted turn end, a failure
    /// moving one stage on; past <paramref name="Stages"/> it applies <paramref name="Then"/> (stored for M1-12b).
    /// </summary>
    public sealed record Staged(int Stages, Save Save, int Cap, string? Then = null) : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "staged";

        internal override int Rank => 8;
    }

    /// <summary><c>untilRest</c>: lingering, ended by a rest.</summary>
    public sealed record UntilRest : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "untilRest";

        internal override int Rank => 9;
    }

    /// <summary><c>untilCamp</c>: lingering, ended by a camp.</summary>
    public sealed record UntilCamp : DurationTerm
    {
        /// <inheritdoc />
        public override string Tag => "untilCamp";

        internal override int Rank => 10;
    }
}

/// <summary>
/// The canonical order of duration terms: by type (<see cref="DurationTerm.Rank"/>), then by every field in declaration
/// order (strings ordinal, an absent cap or <c>then</c> first, a fallback by this order). Total: two terms compare equal
/// only when they are equal.
/// </summary>
[TotalComparer]
public sealed class DurationTermOrder : IComparer<DurationTerm>
{
    /// <summary>The one instance.</summary>
    public static readonly DurationTermOrder Instance = new();

    private DurationTermOrder()
    {
    }

    /// <inheritdoc />
    public int Compare(DurationTerm? x, DurationTerm? y)
    {
        ArgumentNullException.ThrowIfNull(x);
        ArgumentNullException.ThrowIfNull(y);
        int c = x.Rank.CompareTo(y.Rank);
        if (c != 0)
        {
            return c;
        }

        return (x, y) switch
        {
            (DurationTerm.Turns a, DurationTerm.Turns b) => a.N.CompareTo(b.N),
            (DurationTerm.SaveEnds a, DurationTerm.SaveEnds b) => Then(Then(((int)a.Save).CompareTo((int)b.Save), a.Cap, b.Cap), a.GraceTurns, b.GraceTurns),
            (DurationTerm.Charges a, DurationTerm.Charges b) => Chain(Then(0, a.N, b.N), string.CompareOrdinal(a.On, b.On), () => Compare(a.Fallback, b.Fallback)),
            (DurationTerm.TriggerEnd a, DurationTerm.TriggerEnd b) => Chain(string.CompareOrdinal(a.Event, b.Event), Nullable(a.Cap, b.Cap), () => 0),
            (DurationTerm.Sustained a, DurationTerm.Sustained b) => a.Max.CompareTo(b.Max),
            (DurationTerm.Staged a, DurationTerm.Staged b) => Chain(Then(Then(a.Stages.CompareTo(b.Stages), (int)a.Save, (int)b.Save), a.Cap, b.Cap), NullableText(a.Then, b.Then), () => 0),
            _ => 0,
        };
    }

    private static int Then(int c, int a, int b) => c != 0 ? c : a.CompareTo(b);

    private static int Chain(int c, int d, Func<int> e) => c != 0 ? c : d != 0 ? d : e();

    private static int Nullable(int? a, int? b) =>
        a is { } x ? (b is { } y ? x.CompareTo(y) : 1) : (b is null ? 0 : -1);

    private static int NullableText(string? a, string? b) =>
        a is null ? (b is null ? 0 : -1) : (b is null ? 1 : string.CompareOrdinal(a, b));
}

/// <summary>What a condition denies its holder (CO 7.2): the action, the bonus action, or one of the two (<see cref="OneOf"/>).</summary>
public sealed record Denies(bool Action, bool Bonus, bool OneOf) : ICanonical
{
    /// <summary>Denies nothing.</summary>
    public static Denies None { get; } = new(false, false, false);

    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(3);
        w.Bool(0, Action);
        w.Bool(1, Bonus);
        w.Bool(2, OneOf);
        w.EndRecord();
    }
}

/// <summary>A condition's intensity (CO 17.1): its kind and the caps the kind uses (null where unused).</summary>
public sealed record Intensity(IntensityKind Kind, int? ValueCap, int? LevelCap, int? Stages, bool NoRefresh) : ICanonical
{
    /// <summary>Binary, no caps, refreshable.</summary>
    public static Intensity Binary { get; } = new(IntensityKind.Binary, null, null, null, false);

    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(5);
        w.Enum(0, Kind);
        w.OptionalInt32(1, ValueCap);
        w.OptionalInt32(2, LevelCap);
        w.OptionalInt32(3, Stages);
        w.Bool(4, NoRefresh);
        w.EndRecord();
    }
}

/// <summary>
/// A condition's flags (CO 5.1, 17.1): <see cref="CombatOnly"/> ends at combat end; <see cref="SourceTied"/> ends when
/// the source dies or leaves; <see cref="TurnPartGroup"/> marks a turn-part major (party composure, CO 7.2);
/// <see cref="NoProne"/>; <see cref="CombatStartEnds"/> ends at combat start (CO 8's "by flag" <c>combatStart</c> end).
/// </summary>
public sealed record ConditionFlags(bool CombatOnly, bool SourceTied, bool TurnPartGroup, bool NoProne, bool CombatStartEnds) : ICanonical
{
    /// <summary>No flag set.</summary>
    public static ConditionFlags None { get; } = new(false, false, false, false, false);

    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(5);
        w.Bool(0, CombatOnly);
        w.Bool(1, SourceTied);
        w.Bool(2, TurnPartGroup);
        w.Bool(3, NoProne);
        w.Bool(4, CombatStartEnds);
        w.EndRecord();
    }
}

/// <summary>A harmful condition's tick: damage of <see cref="DamageType"/>, the amount being the instance's value.</summary>
public sealed record TickSpec(DamageType DamageType) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(1);
        w.Enum(0, DamageType);
        w.EndRecord();
    }
}

/// <summary>
/// A condition record from <c>conditions/conditions.json</c> (CO 17.1; M1-12a plan call 4), or one of the core
/// definitions (<see cref="Conditions.CoreDefinitions"/>). <see cref="Tier"/> is set exactly for a harmful condition.
/// <see cref="Duration"/> is the in-combat terminator list and <see cref="OutOfCombat"/> the out-of-combat one (empty:
/// <see cref="Duration"/> applies out of combat too), each in file order. <see cref="Modifiers"/> and
/// <see cref="Triggers"/> are in the M1-07 shapes; the rules context folds them through a synthesised
/// <see cref="EffectSourceKind.Condition"/> effect definition with the same id.
/// </summary>
public sealed record ConditionDefinition(
    string File,
    string Id,
    ConditionPolarity Polarity,
    ConditionTier? Tier,
    ImmutableArray<string> Tags,
    string? Family,
    Denies Denies,
    ImmutableArray<DurationTerm> Duration,
    ImmutableArray<DurationTerm> OutOfCombat,
    Intensity Intensity,
    ConditionFlags Flags,
    ImmutableArray<string> Cures,
    ImmutableArray<string> CureImmune,
    TickSpec? Tick,
    ImmutableArray<Modifier> Modifiers,
    ImmutableArray<Trigger> Triggers) : ContentRecord(File, Id)
{
    /// <summary>True for a hard condition.</summary>
    public bool IsHard => Tier == ConditionTier.Hard;

    /// <summary>True if <see cref="Tags"/> holds <paramref name="tag"/>.</summary>
    public bool HasTag(string tag) => Tags.IndexOf(tag, StringComparer.Ordinal) >= 0;

    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(15);
        w.String(0, Id);
        w.Enum(1, Polarity);
        w.OptionalString(2, Tier is { } tier ? Enum.GetName(tier) : null);
        w.List(3, Tags, (iw, t) => iw.String(0, t));
        w.OptionalString(4, Family);
        w.Record(5, Denies);
        w.List(6, Duration, (iw, t) => iw.Record(0, t));
        w.List(7, OutOfCombat, (iw, t) => iw.Record(0, t));
        w.Record(8, Intensity);
        w.Record(9, Flags);
        w.List(10, Cures, (iw, t) => iw.String(0, t));
        w.List(11, CureImmune, (iw, t) => iw.String(0, t));
        w.OptionalRecord(12, Tick);
        w.List(13, Modifiers, (iw, m) => iw.Record(0, m));
        w.List(14, Triggers, (iw, t) => iw.Record(0, t));
        w.EndRecord();
    }
}
