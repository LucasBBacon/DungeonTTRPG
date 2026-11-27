using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Canonical;

namespace Dungeon.Core.Content;

/// <summary>The three content hashes of SV 4.6.</summary>
/// <param name="Manifest"><c>manifestHash</c>: every record's rules fields.</param>
/// <param name="Display"><c>displayHash</c>: the display fields of the records that have them.</param>
/// <param name="GenContent"><c>genContentHash</c>: the manifest over the kinds generation reads.</param>
public readonly record struct ContentHashSet(Digest Manifest, Digest Display, Digest GenContent);

/// <summary>
/// Computes the content hashes over canonical bytes (SV 4.6, owner ruling 4). Each hash is one root record
/// whose field 0 is a set of kinds by file name; each kind is <c>{0 file name, 1 its records by id}</c>.
/// The manifest writes each record's <see cref="ContentRecord.WriteCanonical"/>, the display hash each
/// <see cref="IDisplayCanonical.WriteDisplay"/>, and the generation hash is the manifest structure over the
/// kinds with <see cref="ContentKind.ReadByGeneration"/>. Nothing depends on JSON layout, line endings or
/// file order. The build embeds the result as <c>ContentHashes</c>; it is never computed at run time.
/// </summary>
public static class ContentHashing
{
    /// <summary>The hashes of <paramref name="set"/> over <see cref="ContentKinds.All"/>.</summary>
    public static ContentHashSet Compute(ContentSet set)
    {
        ArgumentNullException.ThrowIfNull(set);
        var kinds = ContentKinds.All;
        return new ContentHashSet(
            Digest.Of(new Root(Kinds(set, kinds, display: false))),
            Digest.Of(new Root(Kinds(set, kinds, display: true))),
            Digest.Of(new Root(Kinds(set, kinds.Where(k => k.ReadByGeneration), display: false))));
    }

    private static ImmutableArray<KindRecords> Kinds(
        ContentSet set, IEnumerable<ContentKind> kinds, bool display) =>
        kinds.Select(k => new KindRecords(k.FileName, set.RecordsIn(k.FileName), display)).ToImmutableArray();

    private sealed record Root(ImmutableArray<KindRecords> Kinds) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(1);
            w.SetById(0, Kinds, k => k.FileName, (iw, k) => iw.Record(0, k));
            w.EndRecord();
        }
    }

    private sealed record KindRecords(string FileName, ImmutableArray<ContentRecord> Records, bool Display) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.String(0, FileName);
            if (Display)
            {
                w.SetById(
                    1,
                    Records.Where(r => r is IDisplayCanonical),
                    r => r.Id,
                    (iw, r) => iw.Record(0, new DisplayBytes((IDisplayCanonical)r)));
            }
            else
            {
                w.SetById(1, Records, r => r.Id, (iw, r) => iw.Record(0, r));
            }

            w.EndRecord();
        }
    }

    private sealed record DisplayBytes(IDisplayCanonical Display) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w) => Display.WriteDisplay(w);
    }
}
