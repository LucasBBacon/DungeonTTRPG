using System;
using System.Collections.Immutable;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;

namespace Dungeon.Core;

/// <summary>How a stream row's draws are counted and checked by the DrawLedger (SV 4.2, SV 9.1).</summary>
public enum CountClass
{
    /// <summary><c>node</c>: a parent only; counter 0.</summary>
    Node,

    /// <summary><c>fixed</c>: counter = a constant.</summary>
    Fixed,

    /// <summary><c>fixed-per-data</c>: counter = a formula over content data.</summary>
    FixedPerData,

    /// <summary><c>leaf-formula</c>: counter = a formula on the recorded choice.</summary>
    LeafFormula,

    /// <summary><c>per-entry</c>: each unit draws its count, and units are at most the cap.</summary>
    PerEntry,

    /// <summary><c>vector</c>: indexed reads below the length; no counter.</summary>
    Vector,

    /// <summary><c>keyed</c>: a keyed node or keyed-hash domain; no counter.</summary>
    Keyed,

    /// <summary><c>sequential-saved</c>: saved and restored, not ledgered per stage.</summary>
    SequentialSaved,

    /// <summary><c>display</c>: not ledgered.</summary>
    Display,

    /// <summary><c>sim</c>: must not derive from <c>runSeed</c>.</summary>
    Sim,
}

/// <summary>What a retry re-keys a stream row on (SV 9.1 <c>retryScope</c>).</summary>
public enum RetryScope
{
    /// <summary><c>never</c>.</summary>
    Never,

    /// <summary><c>floorKey</c>.</summary>
    FloorKey,

    /// <summary><c>attempt</c>.</summary>
    Attempt,

    /// <summary><c>stage</c>.</summary>
    Stage,
}

/// <summary>Which key family a stream row belongs to (SV 9.1 <c>keyType</c>).</summary>
public enum KeyType
{
    /// <summary><c>core</c>.</summary>
    Core,

    /// <summary><c>display</c>.</summary>
    Display,

    /// <summary><c>sim</c>.</summary>
    Sim,
}

/// <summary>The type of a stream index; every index is unsigned (SV 4.1).</summary>
public enum IndexType
{
    /// <summary><c>ulong</c>: absorbed as is.</summary>
    ULong,

    /// <summary><c>uint</c>: zero-extended.</summary>
    UInt,

    /// <summary><c>labelId</c>: absorbed as <c>LabelId(id)</c>.</summary>
    LabelId,
}

/// <summary>One index of a stream row: its name in the path and its type (SV 4.2: normative).</summary>
public sealed record StreamIndex(string Name, IndexType Type) : ICanonical
{
    /// <summary>Writes a record of the name (field 0) and the type (field 1).</summary>
    public void WriteCanonical(CanonicalWriter w)
    {
        ArgumentNullException.ThrowIfNull(w);
        w.BeginRecord(2);
        w.String(0, Name);
        w.Enum(1, Type);
        w.EndRecord();
    }
}

/// <summary>
/// A stream-registry row, with exactly the SV 9.1 fields: <c>{path, alias[], parent, label, indices[], class,
/// countFormula, length?, cap?, stage, readers[], consumers[], retryScope, rules[], keyType, owner, source[], wave}</c>.
/// <see cref="ContentRecord.Id"/> is the <c>path</c> with index placeholders (plan call 2), used for lookup and display
/// only; keys come from <see cref="Label"/> and <see cref="Indices"/>. <see cref="Count"/> is <c>countFormula</c>.
/// </summary>
public sealed record StreamRow(
    string File,
    string Id,
    string? Parent,
    string Label,
    ImmutableArray<StreamIndex> Indices,
    CountClass Class,
    CountFormula? Count,
    ulong? Length,
    CountFormula? Cap,
    string? Stage,
    RetryScope RetryScope,
    KeyType KeyType,
    string Owner,
    Wave Wave,
    ImmutableArray<string> Alias,
    ImmutableArray<string> Readers,
    ImmutableArray<string> Consumers,
    ImmutableArray<string> Rules,
    ImmutableArray<string> Source) : ContentRecord(File, Id)
{
    /// <summary>True if the row's wave is later than the build's, so it expects 0 draws (D-08).</summary>
    public bool IsInert => Waves.IsInert(Wave);

    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        ArgumentNullException.ThrowIfNull(w);
        w.BeginRecord(18);
        w.String(0, Id);
        w.OptionalString(1, Parent);
        w.String(2, Label);
        w.List(3, Indices, (iw, x) => iw.Record(0, x));
        w.Enum(4, Class);
        w.OptionalString(5, Count?.ToString());
        w.OptionalUInt64(6, Length);
        w.OptionalString(7, Cap?.ToString());
        w.OptionalString(8, Stage);
        w.Enum(9, RetryScope);
        w.Enum(10, KeyType);
        w.String(11, Owner);
        w.Enum(12, Wave);
        w.List(13, Alias, (iw, s) => iw.String(0, s));
        w.List(14, Readers, (iw, s) => iw.String(0, s));
        w.List(15, Consumers, (iw, s) => iw.String(0, s));
        w.List(16, Rules, (iw, s) => iw.String(0, s));
        w.List(17, Source, (iw, s) => iw.String(0, s));
        w.EndRecord();
    }
}
