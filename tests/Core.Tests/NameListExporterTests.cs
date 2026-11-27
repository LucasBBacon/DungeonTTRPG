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
    public void Plan_call_7_selects_311_entries_giving_348_retired_records()
    {
        // The selection, counted here from the four prefixes rather than through the exporter.
        int Starting(string prefix) => Entries.Value.Count(e => e.Outcome.StartsWith(prefix, StringComparison.Ordinal));
        Assert.Equal(303, Starting("renamed →"));
        Assert.Equal(6, Starting("retired"));
        Assert.Equal(1, Starting("dropped"));
        Assert.Equal(1, Starting("deleted"));

        var selected = NameListExporter.RetiringEntries(Entries.Value);
        Assert.Equal(311, selected.Length);
        Assert.Equal(
            Entries.Value.Where(e => new[] { "renamed →", "retired", "dropped", "deleted" }
                .Any(p => e.Outcome.StartsWith(p, StringComparison.Ordinal))),
            selected);

        // Splitting every part literally gives 352 distinct pairs; the 4 the source keeps (Ready and
        // Waiting in the rename target, Gut spore-bloat and Silk mildew moth "final as written") are not retired.
        Assert.Equal(348, NameListExporter.Retired(Entries.Value).Length);
    }

    [Fact]
    public void A_rename_does_not_retire_a_part_the_target_keeps()
    {
        var retired = NameListExporter.Retired(Entries.Value);

        // "Plate pip states: Ready, Spent, Waiting, Kept, Dormant" renamed → "Ready, Used, Waiting, Stored, Idle".
        Assert.DoesNotContain(retired, r => r.Name == "Ready" && r.Category == "UI surface");
        Assert.DoesNotContain(retired, r => r.Name == "Waiting" && r.Category == "UI surface");
        Assert.Contains(retired, r => r.Name == "Spent" && r.Category == "UI surface");
        Assert.Contains(retired, r => r.Name == "Kept" && r.Category == "UI surface");
        Assert.Contains(retired, r => r.Name == "Dormant" && r.Category == "UI surface");

        // "Shield kit / Totem kit" renamed → "Shield hand / Focus hand (Totem kit; agreed, Q2)": the
        // parenthetical records that Totem kit became Focus hand (herald-class 89), so both old names retire.
        Assert.Contains(retired, r => r.Name == "Totem kit" && r.Category == "UI surface");
        Assert.Contains(retired, r => r.Name == "Shield kit" && r.Category == "UI surface");
    }

    [Fact]
    public void Deleted_in_part_retires_only_the_named_part()
    {
        var retired = NameListExporter.Retired(Entries.Value);

        Assert.Contains(retired, r => r.Name == "Heart sleeping-draught nurse" && r.Category == "enemy type");
        Assert.DoesNotContain(retired, r => r.Name == "Gut spore-bloat" && r.Category == "enemy type");
        Assert.DoesNotContain(retired, r => r.Name == "Silk mildew moth" && r.Category == "enemy type");
    }

    [Fact]
    public void Retired_parts_follow_the_outcome_wording()
    {
        static NameListEntry E(string name, string outcome) => new("d", name, "c", "unclear", outcome, "x", null);
        static string[] Parts(NameListEntry e) => NameListExporter.RetiredParts(e).ToArray();

        Assert.Equal(["A", "C"], Parts(E("L: A, B, C", "renamed → B, D")));
        Assert.Equal(["A"], Parts(E("A / B", "renamed → X / B (A; agreed)")));
        Assert.Equal(["B"], Parts(E("L: A, B", "deleted in part: B is deleted; the rest final")));
        Assert.Equal(["A", "B"], Parts(E("A / B", "retired (rejected)")));
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
