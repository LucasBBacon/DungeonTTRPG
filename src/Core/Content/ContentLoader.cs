using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Dungeon.Core.Content;

/// <summary>
/// Loads content at startup and fails loudly, naming the file and field (MAIN 6.5). Files are taken
/// in ordinal name order, each read by the one kind whose <see cref="ContentKind.FileName"/> it has;
/// references are resolved after every file has loaded.
/// </summary>
public static class ContentLoader
{
    /// <summary>Loads <paramref name="files"/> with <see cref="ContentKinds.All"/>.</summary>
    public static ContentSet Load(IEnumerable<ContentFile> files) => Load(files, ContentKinds.All);

    /// <summary>
    /// Loads <paramref name="files"/> with the given kinds. An unknown or repeated file name, a bad field,
    /// a duplicate id within a kind and a bad reference each throw <see cref="ContentLoadException"/>.
    /// Every kind is optional: one whose file is absent has no records.
    /// </summary>
    public static ContentSet Load(IEnumerable<ContentFile> files, IReadOnlyList<ContentKind> kinds)
    {
        ArgumentNullException.ThrowIfNull(files);
        ArgumentNullException.ThrowIfNull(kinds);

        var sortedKinds = kinds
            .Select(k => k ?? throw new ArgumentException("A content kind is null.", nameof(kinds)))
            .OrderBy(k => k.FileName, StringComparer.Ordinal)
            .ToImmutableArray();
        for (int i = 1; i < sortedKinds.Length; i++)
        {
            if (string.Equals(sortedKinds[i - 1].FileName, sortedKinds[i].FileName, StringComparison.Ordinal))
            {
                throw new ArgumentException($"Two content kinds read {sortedKinds[i].FileName}.", nameof(kinds));
            }
        }

        var sortedFiles = files
            .Select(f => f ?? throw new ArgumentException("A content file is null.", nameof(files)))
            .OrderBy(f => f.Name, StringComparer.Ordinal)
            .ToImmutableArray();

        var byFile = ImmutableArray.CreateBuilder<(string File, ImmutableArray<ContentRecord> Records)>(sortedFiles.Length);
        for (int i = 0; i < sortedFiles.Length; i++)
        {
            var file = sortedFiles[i];
            if (i > 0 && string.Equals(sortedFiles[i - 1].Name, file.Name, StringComparison.Ordinal))
            {
                throw new ContentLoadException(file.Name, "$", "the file is given twice.");
            }

            var kind = KindFor(sortedKinds, file.Name)
                ?? throw new ContentLoadException(file.Name, "$", "no content kind reads this file.");
            var records = kind.Read(file);
            RejectDuplicateIds(kind, file.Name, records);
            byFile.Add((file.Name, records));
        }

        var set = new ContentSet(sortedFiles, byFile.MoveToImmutable());
        foreach (var kind in sortedKinds)
        {
            kind.Resolve(set);
        }

        return set;
    }

    private static ContentKind? KindFor(ImmutableArray<ContentKind> kinds, string fileName)
    {
        foreach (var kind in kinds)
        {
            if (string.Equals(kind.FileName, fileName, StringComparison.Ordinal))
            {
                return kind;
            }
        }

        return null;
    }

    // A kind reads one file, so a repeat within a kind is a repeat within that file. The second
    // record in file order is the one named.
    private static void RejectDuplicateIds(ContentKind kind, string fileName, ImmutableArray<ContentRecord> records)
    {
        var seen = new List<string>(records.Length);
        for (int i = 0; i < records.Length; i++)
        {
            int at = seen.BinarySearch(records[i].Id, StringComparer.Ordinal);
            if (at >= 0)
            {
                throw new ContentLoadException(
                    fileName,
                    ContentJson.Member(ContentJson.Item("$." + kind.ArrayName, i), "id"),
                    $"id \"{records[i].Id}\" is used twice.");
            }

            seen.Insert(~at, records[i].Id);
        }
    }
}
