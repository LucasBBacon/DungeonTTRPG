using System.Collections.Immutable;
using System.Text;
using Dungeon.Sim.Tools;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The name-list exporter over the real appendix (Review Focus 3) and the drift guard on its committed output.</summary>
public class NameListExporterTests
{
    private const string AppendixPath = "docs/superpowers/specs/2026-11-24-naming-pass-name-list.md";

    private static readonly Lazy<ImmutableArray<NameListEntry>> Entries =
        new(() => NameListExporter.Parse(ReadRepoText(AppendixPath)));

    [Fact]
    public void The_appendix_parses_into_every_entry_by_document()
    {
        var entries = Entries.Value;

        Assert.Equal(2893, entries.Length);
        Assert.Equal(34, entries.Select(e => e.Doc).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(
            new NameListEntry("dungeon-rpg", "run", "system term", "unclear", "final as written", "final", null),
            entries[0]);
    }

    [Fact]
    public void Trailing_text_after_the_outcome_is_the_note()
    {
        var held = Assert.Single(Entries.Value, e => e.Name == "Held Ground");
        Assert.NotNull(held.Note);
        Assert.StartsWith("(first Planted", held.Note, StringComparison.Ordinal);
        Assert.Single(Entries.Value, e => e.Note is not null);
    }

    [Fact]
    public void Outcome_kinds_have_the_exact_counts()
    {
        var counts = Entries.Value
            .GroupBy(e => e.OutcomeKind, StringComparer.Ordinal)
            .ToDictionary(g => g.Key, g => g.Count(), StringComparer.Ordinal);

        Assert.Equal(
            new Dictionary<string, int>(StringComparer.Ordinal)
            {
                ["final"] = 2510,
                ["renamed"] = 327,
                ["internal"] = 41,
                ["retired"] = 6,
                ["held"] = 3,
                ["never"] = 2,
                ["dropped"] = 1,
                ["deleted"] = 1,
                ["other"] = 2,
            },
            counts);

        var others = Entries.Value.Where(e => e.OutcomeKind == "other").Select(e => e.Outcome).ToArray();
        Assert.Contains(others, o => o.StartsWith("Sprawled final;", StringComparison.Ordinal));
        Assert.Contains(others, o => o.StartsWith("ruled:", StringComparison.Ordinal));
    }

    [Fact]
    public void Every_name_is_nfc()
    {
        Assert.All(Entries.Value, e => Assert.True(e.Name.IsNormalized(NormalizationForm.FormC), e.Name));
        Assert.Equal(14, Entries.Value.Count(e => e.Name.Any(c => c > '\u007F')));
    }

    [Fact]
    public void Retired_names_are_one_record_per_name_and_category()
    {
        var retired = NameListExporter.Retired(Entries.Value);

        var pairs = retired.Select(r => (r.Name, r.Category)).ToArray();
        Assert.Equal(pairs.Length, pairs.Distinct().Count());
        Assert.Equal(
            pairs,
            pairs.OrderBy(p => p.Name, StringComparer.Ordinal).ThenBy(p => p.Category, StringComparer.Ordinal).ToArray());

        Assert.Contains(retired, r => r.Name == "Underpillow" && r.Category == "heirloom");
        Assert.Contains(retired, r => r.Name == "Embercleaver of ?" && r.Category == "item");

        // Grouped names split into one record each.
        Assert.Contains(retired, r => r.Name == "Widow's Knot" && r.Category == "charm");
        Assert.Contains(retired, r => r.Name == "Scab Knot" && r.Category == "charm");
        Assert.Contains(retired, r => r.Name == "Vendor Chalk" && r.Category == "item");
        Assert.Contains(retired, r => r.Name == "Cartouche set piece" && r.Category == "item");

        // "renamed in part" is not retired: the part cannot be told without NP 5 (plan call 7).
        var inPart = Entries.Value.Where(e => e.Outcome.StartsWith("renamed in part", StringComparison.Ordinal)).ToArray();
        Assert.NotEmpty(inPart);
        Assert.All(inPart, e => Assert.DoesNotContain(retired, r => r.Name == e.Name && r.Category == e.Category));
    }

    [Fact]
    public void A_section_whose_count_disagrees_throws()
    {
        const string text = "#### B.1 doc-a (2)\n\n- run · system term · unclear · **final as written**\n";
        var ex = Assert.Throws<FormatException>(() => NameListExporter.Parse(text));
        Assert.Contains("doc-a", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void An_unparseable_entry_line_throws_naming_its_line()
    {
        const string text = "#### B.1 doc-a (2)\n\n- run · system term · unclear · **final as written**\n- not an entry\n";
        var ex = Assert.Throws<FormatException>(() => NameListExporter.Parse(text));
        Assert.Contains("line 4", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_name_that_is_not_nfc_throws()
    {
        const string text = "#### B.1 doc-a (1)\n\n- Ünder · item · unclear · **final as written**\n";
        var ex = Assert.Throws<FormatException>(() => NameListExporter.Parse(text));
        Assert.Contains("line 3", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Committed_files_match_the_appendix()
    {
        const string advice = "run `dotnet run --project src/Sim -- name-list`";
        var entries = Entries.Value;

        Assert.True(
            NameListExporter.ToNameListJson(entries) == ReadRepoText("content/naming/name_list.json"),
            "content/naming/name_list.json is out of date: " + advice);
        Assert.True(
            NameListExporter.ToRetiredNamesJson(entries) == ReadRepoText("content/naming/retired_names.json"),
            "content/naming/retired_names.json is out of date: " + advice);
    }

    [Fact]
    public void Output_is_lf_without_a_bom_with_a_trailing_newline_and_raw_utf8()
    {
        string json = NameListExporter.ToNameListJson(Entries.Value);

        Assert.DoesNotContain("\r", json, StringComparison.Ordinal);
        Assert.False(json.StartsWith('﻿'));
        Assert.EndsWith("}\n", json, StringComparison.Ordinal);
        Assert.StartsWith("{\n  \"entries\": [\n    {\n", json, StringComparison.Ordinal);
        Assert.DoesNotContain("\\u", json, StringComparison.Ordinal);
    }

    // Reads the file's exact text: UTF-8, no normalising of line endings or a BOM.
    private static string ReadRepoText(string relative)
    {
        var bytes = File.ReadAllBytes(Path.Combine(RepoRoot.Find(), relative));
        return new UTF8Encoding(false, throwOnInvalidBytes: true).GetString(bytes);
    }
}
