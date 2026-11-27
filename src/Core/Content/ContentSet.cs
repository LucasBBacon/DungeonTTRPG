using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Canonical;

namespace Dungeon.Core.Content;

/// <summary>
/// The loaded content: the files, ordinal by name, and their records. Lookups are by ordinal id
/// over sorted arrays (DS4), so nothing here depends on hash order. Build one with <see cref="ContentLoader"/>.
/// </summary>
public sealed class ContentSet
{
    // Each file's records in file order, so a kind can name $.<array>[i] when it resolves references.
    private readonly ImmutableArray<(string File, ImmutableArray<ContentRecord> Records)> _byFile;

    // Every record, ordinal by id then file.
    private readonly ImmutableArray<ContentRecord> _all;

    internal ContentSet(
        ImmutableArray<ContentFile> files,
        ImmutableArray<(string File, ImmutableArray<ContentRecord> Records)> byFile,
        ImmutableArray<string> kindFiles)
    {
        Files = files;
        KindFiles = kindFiles;
        _byFile = byFile;
        _all = byFile
            .SelectMany(f => f.Records)
            .OrderBy(r => r.Id, StringComparer.Ordinal)
            .ThenBy(r => r.File, StringComparer.Ordinal)
            .ToImmutableArray();
    }

    /// <summary>The loaded files, ordinal by name.</summary>
    public ImmutableArray<ContentFile> Files { get; }

    /// <summary>The file names the set's content kinds read, ordinal: every name <see cref="DigestOf"/> accepts.</summary>
    public ImmutableArray<string> KindFiles { get; }

    /// <summary>True if one of the set's content kinds reads <paramref name="fileName"/>.</summary>
    public bool IsKindFile(string fileName) =>
        ImmutableArray.BinarySearch(KindFiles, fileName, StringComparer.Ordinal) >= 0;

    /// <summary>Every record of type <typeparamref name="T"/>, ordinal by id.</summary>
    public ImmutableArray<T> All<T>()
        where T : ContentRecord =>
        _all.OfType<T>().ToImmutableArray();

    /// <summary>The record of type <typeparamref name="T"/> with id <paramref name="id"/>, or null.</summary>
    public T? Find<T>(string id)
        where T : ContentRecord
    {
        ArgumentNullException.ThrowIfNull(id);

        // Lower bound of id in the id-sorted array, then the run of records with that id.
        int lo = 0;
        int hi = _all.Length;
        while (lo < hi)
        {
            int mid = lo + ((hi - lo) >> 1);
            if (string.CompareOrdinal(_all[mid].Id, id) < 0)
            {
                lo = mid + 1;
            }
            else
            {
                hi = mid;
            }
        }

        for (int i = lo; i < _all.Length && string.Equals(_all[i].Id, id, StringComparison.Ordinal); i++)
        {
            if (_all[i] is T found)
            {
                return found;
            }
        }

        return null;
    }

    /// <summary>The records read from <paramref name="fileName"/> in file order; empty if the file is absent.</summary>
    public ImmutableArray<ContentRecord> RecordsIn(string fileName)
    {
        ArgumentNullException.ThrowIfNull(fileName);
        foreach (var (file, records) in _byFile)
        {
            if (string.Equals(file, fileName, StringComparison.Ordinal))
            {
                return records;
            }
        }

        return ImmutableArray<ContentRecord>.Empty;
    }

    /// <summary>
    /// SHA-256 over the canonical bytes of the named files' records: the distinct names in ordinal
    /// order, each with its records ordinal by id. Each record contributes its rules bytes
    /// (<see cref="ContentRecord.WriteCanonical"/>) and, when it is <see cref="IDisplayCanonical"/>, its
    /// display bytes, so editing any field a check can read changes the digest (SV 5.3 cache). An absent
    /// file counts as one with no records, so the digest depends on the records alone, never on JSON
    /// layout, line endings or file order. A name no kind of the set reads throws
    /// <see cref="ArgumentException"/>: as an always-empty file it would give a digest that never changes.
    /// </summary>
    public Digest DigestOf(IEnumerable<string> fileNames)
    {
        ArgumentNullException.ThrowIfNull(fileNames);
        var names = fileNames
            .Select(n => n ?? throw new ArgumentException("A file name is null.", nameof(fileNames)))
            .Select(n => IsKindFile(n) ? n : throw new ArgumentException($"No content kind reads {n}.", nameof(fileNames)))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(n => n, StringComparer.Ordinal)
            .Select(n => new FileRecords(n, RecordsIn(n)))
            .ToImmutableArray();
        return Digest.Of(new Manifest(names));
    }

    private sealed record FileRecords(string Name, ImmutableArray<ContentRecord> Records) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.String(0, Name);
            w.SetById(1, Records, r => r.Id, (iw, r) => iw.Record(0, new RecordBytes(r)));
            w.EndRecord();
        }
    }

    // A record's rules bytes, then its display bytes when it has display fields.
    private sealed record RecordBytes(ContentRecord Record) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.Record(0, Record);
            w.OptionalRecord(1, Record is IDisplayCanonical display ? new DisplayBytes(display) : null);
            w.EndRecord();
        }
    }

    private sealed record DisplayBytes(IDisplayCanonical Display) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w) => Display.WriteDisplay(w);
    }

    private sealed record Manifest(ImmutableArray<FileRecords> Files) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(1);
            w.List(0, Files, (iw, f) => iw.Record(0, f));
            w.EndRecord();
        }
    }
}
