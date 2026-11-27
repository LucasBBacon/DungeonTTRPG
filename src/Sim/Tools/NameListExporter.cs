using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Dungeon.Sim.Tools;

/// <summary>
/// One harvested entry of the naming-pass appendix: <c>name · category · harvest status · **outcome**</c>,
/// with any text after the bold outcome as <see cref="Note"/>. <see cref="OutcomeKind"/> is
/// <c>final</c>, <c>renamed</c>, <c>internal</c>, <c>retired</c>, <c>held</c>, <c>deleted</c>,
/// <c>dropped</c>, <c>never</c> or <c>other</c>.
/// </summary>
public sealed record NameListEntry(
    string Doc, string Name, string Category, string Status, string Outcome, string OutcomeKind, string? Note);

/// <summary>A retired (name, category) pair and the entry it came from (plan call 7).</summary>
public sealed record RetiredName(string Name, string Category, string Doc, string Outcome);

/// <summary>
/// Turns the naming-pass appendix (<c>docs/superpowers/specs/2026-11-24-naming-pass-name-list.md</c>) into
/// <c>content/naming/name_list.json</c> and derives <c>content/naming/retired_names.json</c> (owner ruling 2).
/// The output is deterministic: LF, two-space indent, no BOM, a trailing newline and raw UTF-8.
/// </summary>
public static class NameListExporter
{
    private static readonly Regex Header = new(@"^#### B\.[0-9]+ (\S+) \(([0-9]+)\)$", RegexOptions.CultureInvariant);

    private static readonly Regex EntryLine =
        new(@"^- (.+) · (.+?) · (.+?) · \*\*(.+?)\*\*(.*)$", RegexOptions.CultureInvariant);

    private static readonly ImmutableArray<string> Kinds =
        ["final", "renamed", "internal", "retired", "held", "deleted", "dropped", "never"];

    private const string RenamedTo = "renamed →";

    private const string DeletedInPart = "deleted in part";

    private static readonly ImmutableArray<string> RetiringPrefixes = ["retired", "deleted", "dropped", RenamedTo];

    private static readonly JsonWriterOptions WriterOptions = new()
    {
        Indented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    /// <summary>
    /// Parses the appendix. A header <c>#### B.n doc (count)</c> starts a section, whose entry count must
    /// equal <c>count</c>. Any line starting <c>- </c> must be an entry with an NFC name, inside a section.
    /// Violations throw <see cref="FormatException"/> naming the line or section.
    /// </summary>
    public static ImmutableArray<NameListEntry> Parse(string markdown)
    {
        ArgumentNullException.ThrowIfNull(markdown);
        var entries = ImmutableArray.CreateBuilder<NameListEntry>();
        string? doc = null;
        int expected = 0;
        int inSection = 0;
        var lines = markdown.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].TrimEnd('\r');
            int lineNumber = i + 1;
            var header = Header.Match(line);
            if (header.Success)
            {
                CheckCount(doc, expected, inSection);
                doc = header.Groups[1].Value;
                expected = int.Parse(header.Groups[2].Value, NumberStyles.None, CultureInfo.InvariantCulture);
                inSection = 0;
                continue;
            }

            if (!line.StartsWith("- ", StringComparison.Ordinal))
            {
                continue;
            }

            var m = EntryLine.Match(line);
            if (!m.Success)
            {
                throw new FormatException($"line {lineNumber}: not a name-list entry (name · category · status · **outcome**).");
            }

            if (doc is null)
            {
                throw new FormatException($"line {lineNumber}: an entry before the first section header.");
            }

            string name = m.Groups[1].Value;
            if (!name.IsNormalized(NormalizationForm.FormC))
            {
                throw new FormatException($"line {lineNumber}: the name \"{name}\" is not NFC.");
            }

            string outcome = m.Groups[4].Value;
            string note = m.Groups[5].Value.Trim();
            entries.Add(new NameListEntry(
                doc, name, m.Groups[2].Value, m.Groups[3].Value, outcome, KindOf(outcome), note.Length == 0 ? null : note));
            inSection++;
        }

        CheckCount(doc, expected, inSection);
        return entries.ToImmutable();
    }

    /// <summary>
    /// The entries plan call 7 selects: outcomes that start <c>retired</c>, <c>deleted</c> or
    /// <c>dropped</c>, and whole renames (<c>renamed →</c>; <c>renamed in part</c> is not selected).
    /// </summary>
    public static ImmutableArray<NameListEntry> RetiringEntries(ImmutableArray<NameListEntry> entries) =>
        entries.Where(e => RetiringPrefixes.Any(p => e.Outcome.StartsWith(p, StringComparison.Ordinal))).ToImmutableArray();

    /// <summary>
    /// The parts of a selected entry that are retired: each part of the grouped name, except that
    /// for <c>renamed →</c> a part that is also a part of the rename target (ordinal, also against the
    /// target part without a trailing parenthetical) keeps its name, and for <c>deleted in part</c> only
    /// the parts the outcome names (ordinal substring) are retired, since the rest stay as written.
    /// </summary>
    public static ImmutableArray<string> RetiredParts(NameListEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);
        var parts = SplitGroup(entry.Name);
        if (entry.Outcome.StartsWith(DeletedInPart, StringComparison.Ordinal))
        {
            string said = entry.Outcome.Substring(DeletedInPart.Length);
            return parts.Where(p => said.Contains(p, StringComparison.Ordinal)).ToImmutableArray();
        }

        if (entry.Outcome.StartsWith(RenamedTo, StringComparison.Ordinal))
        {
            var target = Split(entry.Outcome.Substring(RenamedTo.Length), [", ", " / ", "; "]);
            var survivors = target.Concat(target.Select(StripTrailingParenthetical)).ToHashSet(StringComparer.Ordinal);
            return parts.Where(p => !survivors.Contains(p)).ToImmutableArray();
        }

        return parts;
    }

    /// <summary>
    /// The retired pairs (plan call 7): one pair per <see cref="RetiredParts"/> part of each of the
    /// <see cref="RetiringEntries"/>; the first entry of a repeated pair is kept. Ordinal by (name, category).
    /// </summary>
    public static ImmutableArray<RetiredName> Retired(ImmutableArray<NameListEntry> entries)
    {
        var kept = new List<RetiredName>();
        foreach (var e in RetiringEntries(entries))
        {
            foreach (var name in RetiredParts(e))
            {
                if (!kept.Any(r => r.Name == name && r.Category == e.Category))
                {
                    kept.Add(new RetiredName(name, e.Category, e.Doc, e.Outcome));
                }
            }
        }

        return kept
            .OrderBy(r => r.Name, StringComparer.Ordinal)
            .ThenBy(r => r.Category, StringComparer.Ordinal)
            .ToImmutableArray();
    }

    /// <summary>The text of <c>content/naming/name_list.json</c>: <c>{"entries": [...]}</c> in file order, ids <c>doc:index</c>.</summary>
    public static string ToNameListJson(ImmutableArray<NameListEntry> entries) =>
        Write("entries", w =>
        {
            var indexByDoc = new List<(string Doc, int Next)>();
            foreach (var e in entries)
            {
                int slot = indexByDoc.FindIndex(d => d.Doc == e.Doc);
                if (slot < 0)
                {
                    indexByDoc.Add((e.Doc, 0));
                    slot = indexByDoc.Count - 1;
                }

                int index = indexByDoc[slot].Next;
                indexByDoc[slot] = (e.Doc, index + 1);

                w.WriteStartObject();
                w.WriteString("id", e.Doc + ":" + index.ToString(CultureInfo.InvariantCulture));
                w.WriteString("doc", e.Doc);
                w.WriteString("name", e.Name);
                w.WriteString("category", e.Category);
                w.WriteString("status", e.Status);
                w.WriteString("outcome", e.Outcome);
                w.WriteString("outcomeKind", e.OutcomeKind);
                if (e.Note is not null)
                {
                    w.WriteString("note", e.Note);
                }

                w.WriteEndObject();
            }
        });

    /// <summary>The text of <c>content/naming/retired_names.json</c>: <c>{"retired": [...]}</c>, ids <c>r0000</c> in (name, category) order.</summary>
    public static string ToRetiredNamesJson(ImmutableArray<NameListEntry> entries) =>
        Write("retired", w =>
        {
            var retired = Retired(entries);
            for (int i = 0; i < retired.Length; i++)
            {
                var r = retired[i];
                w.WriteStartObject();
                w.WriteString("id", "r" + i.ToString("D4", CultureInfo.InvariantCulture));
                w.WriteString("name", r.Name);
                w.WriteString("category", r.Category);
                w.WriteString("doc", r.Doc);
                w.WriteString("outcome", r.Outcome);
                w.WriteEndObject();
            }
        });

    /// <summary>
    /// The parts of a grouped name. A label before <c>": "</c> followed by a list (<c>Label: A, B</c>) is
    /// dropped and the list split on <c>", "</c> and <c>" / "</c>; otherwise the name splits on <c>" / "</c>.
    /// Separators inside parentheses or double quotes do not split, so <c>Complexity (Simple / Medium)</c>
    /// and <c>"Weak: Will"</c> stay whole.
    /// </summary>
    internal static ImmutableArray<string> SplitGroup(string name)
    {
        int colon = IndexAtTop(name, ": ", 0);
        if (colon >= 0)
        {
            string list = name.Substring(colon + 2);
            if (IndexAtTop(list, ", ", 0) >= 0 || IndexAtTop(list, " / ", 0) >= 0)
            {
                return Split(list, [", ", " / "]);
            }
        }

        return Split(name, [" / "]);
    }

    private static ImmutableArray<string> Split(string s, string[] separators)
    {
        var parts = ImmutableArray.CreateBuilder<string>();
        int start = 0;
        while (true)
        {
            int next = -1;
            string? found = null;
            foreach (var sep in separators)
            {
                int at = IndexAtTop(s, sep, start);
                if (at >= 0 && (next < 0 || at < next))
                {
                    next = at;
                    found = sep;
                }
            }

            string part = (next < 0 ? s.Substring(start) : s.Substring(start, next - start)).Trim();
            if (part.Length > 0)
            {
                parts.Add(part);
            }

            if (next < 0)
            {
                return parts.ToImmutable();
            }

            start = next + found!.Length;
        }
    }

    // "Focus hand (Totem kit; agreed, Q2)" gives "Focus hand"; a part without a trailing parenthetical is unchanged.
    private static string StripTrailingParenthetical(string part)
    {
        int open = part.LastIndexOf(" (", StringComparison.Ordinal);
        return part.EndsWith(')') && open > 0 ? part.Substring(0, open) : part;
    }

    // The first index of sep at or after start that is outside parentheses and double quotes, or -1.
    private static int IndexAtTop(string s, string sep, int start)
    {
        int depth = 0;
        bool quoted = false;
        for (int i = 0; i < s.Length; i++)
        {
            if (i >= start && depth == 0 && !quoted && string.CompareOrdinal(s, i, sep, 0, sep.Length) == 0)
            {
                return i;
            }

            char c = s[i];
            if (c == '"')
            {
                quoted = !quoted;
            }
            else if (!quoted && c == '(')
            {
                depth++;
            }
            else if (!quoted && c == ')' && depth > 0)
            {
                depth--;
            }
        }

        return -1;
    }

    // The leading run of ASCII letters (after any leading non-letters), lower-cased, if it is a kind.
    private static string KindOf(string outcome)
    {
        int i = 0;
        while (i < outcome.Length && !IsAsciiLetter(outcome[i]))
        {
            i++;
        }

        var word = new StringBuilder();
        while (i < outcome.Length && IsAsciiLetter(outcome[i]))
        {
            word.Append((char)(outcome[i] | 0x20));
            i++;
        }

        string kind = word.ToString();
        return Kinds.Contains(kind) ? kind : "other";
    }

    private static bool IsAsciiLetter(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');

    private static void CheckCount(string? doc, int expected, int actual)
    {
        if (doc is not null && expected != actual)
        {
            throw new FormatException($"section {doc} declares {expected} entries but holds {actual}.");
        }
    }

    private static string Write(string arrayName, Action<Utf8JsonWriter> writeItems)
    {
        var buffer = new MemoryStream();
        using (var w = new Utf8JsonWriter(buffer, WriterOptions))
        {
            w.WriteStartObject();
            w.WriteStartArray(arrayName);
            writeItems(w);
            w.WriteEndArray();
            w.WriteEndObject();
        }

        // Utf8JsonWriter indents with Environment.NewLine on .NET 8; string values never hold a raw CR.
        return new UTF8Encoding(false).GetString(buffer.ToArray()).Replace("\r\n", "\n", StringComparison.Ordinal) + "\n";
    }
}
