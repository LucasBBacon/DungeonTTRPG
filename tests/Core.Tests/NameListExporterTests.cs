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
    public void Plan_call_7_selects_312_entries_giving_344_retired_records()
    {
        // The selection, counted here from the four prefixes and the one "X retired" clause rather than through the exporter.
        int Starting(string prefix) => Entries.Value.Count(e => e.Outcome.StartsWith(prefix, StringComparison.Ordinal));
        Assert.Equal(303, Starting("renamed →"));
        Assert.Equal(6, Starting("retired"));
        Assert.Equal(1, Starting("dropped"));
        Assert.Equal(1, Starting("deleted"));

        var selected = NameListExporter.RetiringEntries(Entries.Value);
        Assert.Equal(312, selected.Length);
        Assert.Equal(
            Entries.Value.Where(e => new[] { "renamed →", "retired", "dropped", "deleted" }
                    .Any(p => e.Outcome.StartsWith(p, StringComparison.Ordinal))
                || e.Outcome == "Sprawled final; Upended retired (NP-6)"),
            selected);

        // Before the annotation fixes there were 348: less the two fragments of the charm-abilities
        // sentence (now one record), less the "unused since floor N" tag and coin ledger the outcome
        // keeps final, less "Second Wind (fighter-type rest tool)", whose bare name repeats a record,
        // gives 344; Upended from the clause repeats "Upended (former name)". Ready and Waiting (in the
        // rename target) and Gut spore-bloat and Silk mildew moth ("final as written") stay unretired.
        Assert.Equal(344, NameListExporter.Retired(Entries.Value).Length);
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
    public void Retired_names_are_the_bare_names_without_annotations()
    {
        var retired = NameListExporter.Retired(Entries.Value);

        // NP 9.1 and 14.2 item 5: the old names themselves, so V24 r2 blocks their return.
        Assert.Contains(retired, r => r.Name == "Upended" && r.Category == "condition");
        Assert.Contains(retired, r => r.Name == "Snare" && r.Category == "trap or lock");
        Assert.Contains(retired, r => r.Name == "Token" && r.Category == "system term");
        Assert.Contains(retired, r => r.Name == "talisman" && r.Category == "system term");
        Assert.Contains(retired, r => r.Name == "Doppel" && r.Category == "enemy type");
        Assert.Contains(retired, r => r.Name == "Hold" && r.Category == "verb or action");
        Assert.Contains(retired, r => r.Name == "Protector" && r.Category == "enemy affix");
        Assert.Contains(retired, r => r.Name == "ask me" && r.Category == "report or setting");
        Assert.Contains(retired, r => r.Name == "Weak: Will" && r.Category == "UI surface");

        // A parenthetical that is part of the name is kept: an option list or a (type)/(element) slot.
        Assert.Contains(retired, r => r.Name == "Complexity (Simple / Medium / Complex)" && r.Category == "UI surface");
        Assert.Contains(retired, r => r.Name == "Resisting (type)" && r.Category == "condition");
        Assert.Contains(retired, r => r.Name == "Resistant (element)" && r.Category == "weapon or armour property");

        Assert.DoesNotContain(retired, r => r.Name.EndsWith(" (former name)", StringComparison.Ordinal));
        Assert.DoesNotContain(retired, r => r.Name.StartsWith('"') && r.Name.EndsWith('"'));
    }

    [Fact]
    public void A_sentence_after_a_colon_is_one_retired_record_not_fragments()
    {
        var retired = NameListExporter.Retired(Entries.Value);

        Assert.Contains(retired, r => r.Name == "Charms catch moments: a token earned from a named moment in the fight, or one button you spend");
        Assert.DoesNotContain(retired, r => r.Name == "or one button you spend");
        Assert.DoesNotContain(retired, r => r.Name == "a token earned from a named moment in the fight");
    }

    [Fact]
    public void A_rename_that_keeps_named_parts_final_does_not_retire_them()
    {
        var retired = NameListExporter.Retired(Entries.Value);

        // "... / \"unused since floor N\" tag / coin ledger" renamed → "..."; the tag and ledger final as written.
        Assert.Contains(retired, r => r.Name == "the Hollow ahead sells rare" && r.Category == "UI surface");
        Assert.DoesNotContain(retired, r => r.Name.Contains("unused since floor N", StringComparison.Ordinal));
        Assert.DoesNotContain(retired, r => r.Name == "coin ledger" && r.Category == "UI surface");
    }

    [Fact]
    public void Annotations_are_stripped_from_retired_parts()
    {
        static NameListEntry E(string name, string outcome) => new("d", name, "c", "unclear", outcome, "x", null);
        static string[] Parts(NameListEntry e) => NameListExporter.RetiredParts(e).ToArray();

        Assert.Equal(["Upended"], Parts(E("Upended (former name)", "renamed → Sprawled")));
        Assert.Equal(["always", "ask me"], Parts(E("L: always / \"ask me\"", "renamed → Always / Ask me (ids internal)")));
        Assert.Equal(["seed (plays on)"], Parts(E("\"seed (plays on)\"", "renamed → Run code")));
        Assert.Equal(["Resisting (type)"], Parts(E("Resisting (type)", "renamed → Resistant (type) on screen")));
        Assert.Equal(["Pick (A / B)"], Parts(E("Pick (A / B)", "renamed → X / Y")));
        Assert.Equal(["L: a thing, or another"], Parts(E("L: a thing, or another", "renamed → M")));
        Assert.Equal(["\"a\" b"], Parts(E("\"a\" b / c tag / d ledger", "renamed → e; the tag and ledger final as written")));
        Assert.Equal(["Upended"], Parts(E("Upended / Sprawled", "Sprawled final; Upended retired (NP-6)")));
    }

    [Fact]
    public void A_retired_clause_inside_another_outcome_selects_the_entry()
    {
        var selected = NameListExporter.RetiringEntries(Entries.Value);
        Assert.Contains(selected, e => e.Name == "Upended / Sprawled" && e.Category == "condition");
        Assert.DoesNotContain(selected, e => e.OutcomeKind == "other" && !e.Name.StartsWith("Upended", StringComparison.Ordinal));
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
