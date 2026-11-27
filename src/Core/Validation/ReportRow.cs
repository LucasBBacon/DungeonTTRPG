using System.Collections.Immutable;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation;

/// <summary>The simulator instrument a report reads (SV 9.1 <c>instrument: S|W|L|V|G|H</c>).</summary>
public enum Instrument
{
    S,
    W,
    L,
    V,
    G,
    H,
}

/// <summary>How settled a report's threshold is (SV 9.1 <c>thresholdStatus: agreed|settled|lead|illustr</c>).</summary>
public enum ThresholdStatus
{
    Agreed,
    Settled,
    Lead,
    Illustr,
}

/// <summary>Whether a report's test is one- or two-sided (SV 9.1 <c>sided: one|two</c>).</summary>
public enum Sided
{
    One,
    Two,
}

/// <summary>
/// A report-registry row, with exactly the SV 9.1 fields:
/// <c>{id, family?, keys[], owner, definition, unit, instrument: S|W|L|V|G|H, arms[], class, threshold, thresholdStatus: agreed|settled|lead|illustr, sided: one|two, z, stat, N, wave, ciTier, leverOrderRef?}</c>.
/// <see cref="Threshold"/> and <see cref="Z"/> are rationals (DS13); <see cref="Arms"/> names arm ids and is resolved at load.
/// </summary>
public sealed record ReportRow(
    string File,
    string Id,
    string? Family,
    ImmutableArray<string> Keys,
    string Owner,
    string Definition,
    string Unit,
    Instrument Instrument,
    ImmutableArray<string> Arms,
    string Class,
    Rational Threshold,
    ThresholdStatus ThresholdStatus,
    Sided Sided,
    Rational Z,
    string Stat,
    int N,
    string Wave,
    string CiTier,
    string? LeverOrderRef) : ContentRecord(File, Id)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(18);
        w.String(0, Id);
        w.OptionalString(1, Family);
        w.List(2, Keys, (iw, s) => iw.String(0, s));
        w.String(3, Owner);
        w.String(4, Definition);
        w.String(5, Unit);
        w.Enum(6, Instrument);
        w.List(7, Arms, (iw, s) => iw.String(0, s));
        w.String(8, Class);
        w.Record(9, Threshold);
        w.Enum(10, ThresholdStatus);
        w.Enum(11, Sided);
        w.Record(12, Z);
        w.String(13, Stat);
        w.Int32(14, N);
        w.String(15, Wave);
        w.String(16, CiTier);
        w.OptionalString(17, LeverOrderRef);
        w.EndRecord();
    }
}
