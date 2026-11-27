using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation;

/// <summary>
/// A record whose name reaches the screen, as the naming checks see it. <see cref="File"/> and
/// <see cref="Field"/> locate its name (a JSON path such as <c>$.items[3].name</c>); name failures are
/// named there. <see cref="CategoryField"/> locates its category (<c>V7.NameRegistry</c> category failures)
/// and <see cref="KeyField"/> its display key, or where the key belongs when it has none (V24 r6).
/// <see cref="MeaningId"/> is the meaning it shares with other records of the same name (V24 r1), and
/// <see cref="DisplayKey"/> is its key in <c>strings/en.json</c>, which an internal record does not have (V24 r6).
/// </summary>
public sealed record NamedRecord(
    string File,
    string Field,
    string DisplayName,
    string Category,
    string? MeaningId,
    bool Internal,
    string? DisplayKey,
    string CategoryField,
    string KeyField);

/// <summary>Collects the named records of the content kinds that are <see cref="INamedRecordSource"/>s.</summary>
internal static class NamedRecords
{
    /// <summary>The file names the sources among <paramref name="kinds"/> read, ordinal.</summary>
    public static ImmutableArray<string> SourceFiles(IEnumerable<ContentKind> kinds) =>
        kinds.Where(k => k is INamedRecordSource)
            .Select(k => k.FileName)
            .OrderBy(n => n, StringComparer.Ordinal)
            .ToImmutableArray();

    /// <summary>Every named record, by source file name (ordinal), then in each source's order.</summary>
    public static ImmutableArray<NamedRecord> Collect(ContentSet set, IEnumerable<ContentKind> kinds) =>
        kinds.Where(k => k is INamedRecordSource)
            .OrderBy(k => k.FileName, StringComparer.Ordinal)
            .SelectMany(k => ((INamedRecordSource)k).NamedRecords(set))
            .ToImmutableArray();

    /// <summary>The distinct values ordinal, for <see cref="Contains"/>.</summary>
    public static ImmutableArray<string> Sorted(IEnumerable<string> values) =>
        values.Distinct(StringComparer.Ordinal).OrderBy(v => v, StringComparer.Ordinal).ToImmutableArray();

    /// <summary>True if the ordinal-sorted <paramref name="sorted"/> holds <paramref name="value"/>.</summary>
    public static bool Contains(ImmutableArray<string> sorted, string value) =>
        ImmutableArray.BinarySearch(sorted, value, StringComparer.Ordinal) >= 0;
}
