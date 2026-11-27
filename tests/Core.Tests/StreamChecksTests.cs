using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Dungeon.Core.Validation.Checks;
using Dungeon.Sim.Tools;
using Xunit;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Tests;

/// <summary>The V19s stream checks: <c>V19.StreamRows</c>, <c>V19.DomainDisjoint</c> and <c>V19.LabelCollision</c>.</summary>
public class StreamChecksTests
{
    private const string StreamsFile = "registry/streams.json";
    private const string IdsFile = "test/ids.json";

    // --- V19.StreamRows ---

    [Fact]
    public void StreamRows_passes_well_formed_rows()
    {
        var set = Load(
            Row("a", cls: "node"),
            Row("a/b/{n}/{id:}", parent: "a", label: "b", indices: Idx(("n", "ulong"), ("id", "labelId")), cls: "fixed-per-data", count: "2*n"),
            Row("a/v", parent: "a", label: "v", cls: "vector", extra: "\"length\": 3"),
            Row("a/e/{entry}", parent: "a", label: "e", indices: Idx(("entry", "ulong")), cls: "per-entry", count: "9", extra: "\"cap\": \"owed:LE\""),
            Row("a/k", parent: "a", label: "k", cls: "keyed"),
            Row("a/k/h:x/{u:}/{i}", parent: "a/k", label: "h:x", indices: Idx(("u", "labelId"), ("i", "uint")), cls: "keyed"),
            Row("a/d", parent: "a", label: "d", cls: "display", keyType: "display"),
            Row("a/s", parent: "a", label: "s", cls: "sequential-saved"),
            Row("simRoot", cls: "sim", keyType: "sim"),
            Row("simRoot/trial/{i}", parent: "simRoot", label: "trial", indices: Idx(("i", "ulong")), cls: "sim", keyType: "sim"));

        Assert.Empty(new StreamRowsCheck().Run(set));
    }

    [Theory]
    [InlineData("a/x")] // the label is "b"
    [InlineData("a/b")] // the declared index is missing
    [InlineData("a/b/{m}")] // the wrong index name
    [InlineData("a/b/{n:}")] // a ulong index marked as a labelId
    [InlineData("a/b/{n}/{n}")] // one index too many
    [InlineData("z/b/{n}")] // not under the parent
    public void StreamRows_fails_an_id_that_is_not_parent_label_and_indices(string path)
    {
        var set = Load(Row("a", cls: "node"), Row("z", cls: "node"), Row(path, parent: "a", label: "b", indices: Idx(("n", "ulong"))));

        var failure = Assert.Single(new StreamRowsCheck().Run(set));
        Assert.Equal(new CheckId("V19.StreamRows"), failure.Check);
        Assert.Equal(StreamsFile, failure.File);
        Assert.Equal("$.streams[2].path", failure.Field);
    }

    [Fact]
    public void StreamRows_fails_a_labelId_index_not_marked_with_a_colon()
    {
        var set = Load(Row("a/{id}", label: "a", indices: Idx(("id", "labelId"))));

        Assert.Equal("$.streams[0].path", Assert.Single(new StreamRowsCheck().Run(set)).Field);
    }

    [Fact]
    public void StreamRows_fails_a_root_id_that_is_not_its_label()
    {
        var set = Load(Row("a", label: "b"));

        Assert.Equal("$.streams[0].path", Assert.Single(new StreamRowsCheck().Run(set)).Field);
    }

    [Theory]
    [InlineData("fixed")]
    [InlineData("fixed-per-data")]
    [InlineData("leaf-formula")]
    [InlineData("per-entry")]
    public void StreamRows_fails_a_counted_class_with_no_formula(string cls)
    {
        string? extra = cls == "per-entry" ? "\"cap\": \"4\"" : null;
        var set = Load(Row("a", cls: cls, count: null, extra: extra));

        var failure = Assert.Single(new StreamRowsCheck().Run(set));
        Assert.Equal("$.streams[0].countFormula", failure.Field);
        Assert.Contains($"a {cls} row needs", failure.Problem, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("node", null)]
    [InlineData("keyed", null)]
    [InlineData("display", "display")]
    [InlineData("sequential-saved", null)]
    [InlineData("vector", null)]
    public void StreamRows_fails_a_count_on_a_class_that_takes_none(string cls, string? keyType)
    {
        string? extra = cls == "vector" ? "\"length\": 3" : null;
        var set = Load(Row("a", cls: cls, count: "1", keyType: keyType ?? "core", extra: extra));

        var failure = Assert.Single(new StreamRowsCheck().Run(set));
        Assert.Equal("$.streams[0].countFormula", failure.Field);
        Assert.Contains($"a {cls} row takes no", failure.Problem, StringComparison.Ordinal);
    }

    [Fact]
    public void StreamRows_fails_a_vector_with_no_length()
    {
        var set = Load(Row("a", cls: "vector"));

        Assert.Equal("$.streams[0].length", Assert.Single(new StreamRowsCheck().Run(set)).Field);
    }

    [Fact]
    public void StreamRows_fails_a_per_entry_row_with_no_cap()
    {
        var set = Load(Row("a", cls: "per-entry", count: "9"));

        Assert.Equal("$.streams[0].cap", Assert.Single(new StreamRowsCheck().Run(set)).Field);
    }

    [Fact]
    public void StreamRows_fails_a_fixed_formula_with_parameters()
    {
        var set = Load(Row("a", cls: "fixed", count: "2+n"));

        Assert.Equal("$.streams[0].countFormula", Assert.Single(new StreamRowsCheck().Run(set)).Field);
    }

    [Fact]
    public void StreamRows_passes_a_fixed_constant_and_an_owed_fixed_formula()
    {
        Assert.Empty(new StreamRowsCheck().Run(Load(Row("a", cls: "fixed", count: "2*(3+4)"), Row("b", cls: "fixed", count: "owed:TL"))));
    }

    [Fact]
    public void StreamRows_fails_a_display_row_whose_key_type_is_not_display()
    {
        var set = Load(Row("a", cls: "display", keyType: "core"));

        Assert.Equal("$.streams[0].keyType", Assert.Single(new StreamRowsCheck().Run(set)).Field);
    }

    [Fact]
    public void StreamRows_fails_a_sim_row_under_a_root_other_than_simRoot()
    {
        var set = Load(
            Row("dungeon", cls: "node"),
            Row("dungeon/trial", parent: "dungeon", label: "trial", cls: "sim", keyType: "sim"),
            Row("other", cls: "sim", keyType: "sim"));

        var failures = new StreamRowsCheck().Run(set);
        Assert.Equal(["$.streams[1].parent", "$.streams[2].parent"], failures.Select(f => f.Field));
    }

    [Fact]
    public void StreamRows_fails_a_stream_child_under_a_keyed_domain()
    {
        var set = Load(
            Row("k", cls: "keyed"),
            Row("k/h:x", parent: "k", label: "h:x", cls: "keyed"),
            Row("k/h:x/c", parent: "k/h:x", label: "c", cls: "fixed", count: "1"));

        Assert.Equal("$.streams[2].parent", Assert.Single(new StreamRowsCheck().Run(set)).Field);
    }

    [Fact]
    public void StreamRows_passes_a_stream_child_under_a_keyed_node()
    {
        var set = Load(Row("k", cls: "keyed"), Row("k/c", parent: "k", label: "c", cls: "fixed", count: "1"));

        Assert.Empty(new StreamRowsCheck().Run(set));
    }

    // --- V19.DomainDisjoint ---

    [Fact]
    public void DomainDisjoint_fails_a_stream_child_and_a_domain_sharing_a_label()
    {
        var set = Load(
            Row("p", cls: "keyed"),
            Row("p/foo", parent: "p", label: "foo", cls: "fixed", count: "1"),
            Row("p/foo/{u:}", parent: "p", label: "foo", indices: Idx(("u", "labelId")), cls: "keyed"));

        var failures = new DomainDisjointCheck().Run(set);
        Assert.Contains(failures, f => f.Field == "$.streams[2].label" && f.Problem.Contains("stream child", StringComparison.Ordinal));
        Assert.All(failures, f => Assert.Equal(new CheckId("V19.DomainDisjoint"), f.Check));
    }

    [Fact]
    public void DomainDisjoint_passes_a_prefixed_domain_beside_a_stream_child()
    {
        var set = Load(
            Row("p", cls: "keyed"),
            Row("p/foo", parent: "p", label: "foo", cls: "fixed", count: "1"),
            Row("p/h:foo/{u:}", parent: "p", label: "h:foo", indices: Idx(("u", "labelId")), cls: "keyed"));

        Assert.Empty(new DomainDisjointCheck().Run(set));
    }

    [Fact]
    public void DomainDisjoint_fails_an_unprefixed_domain_under_checks()
    {
        var set = Load(
            Row("checks", cls: "keyed"),
            Row("checks/h:ok", parent: "checks", label: "h:ok", cls: "keyed"),
            Row("checks/bar", parent: "checks", label: "bar", cls: "keyed"));

        var failure = Assert.Single(new DomainDisjointCheck().Run(set));
        Assert.Equal("$.streams[2].label", failure.Field);
    }

    [Fact]
    public void DomainDisjoint_allows_only_the_approved_route_domains_unprefixed()
    {
        var set = Load(
            Row("dungeon", cls: "node"),
            Row("dungeon/route", parent: "dungeon", label: "route", cls: "keyed"),
            Row("dungeon/route/themePerm/{themeId:}", parent: "dungeon/route", label: "themePerm", indices: Idx(("themeId", "labelId")), cls: "keyed"),
            Row("dungeon/route/finalOrder", parent: "dungeon/route", label: "finalOrder", cls: "keyed"),
            Row("dungeon/route/omen", parent: "dungeon/route", label: "omen", cls: "keyed"),
            Row("dungeon/route/bossOrder", parent: "dungeon/route", label: "bossOrder", cls: "keyed"),
            Row("other", cls: "keyed"),
            Row("other/themePerm", parent: "other", label: "themePerm", cls: "keyed"));

        var failure = Assert.Single(new DomainDisjointCheck().Run(set));
        Assert.Equal("$.streams[7].label", failure.Field);
    }

    // --- V19.LabelCollision ---

    [Fact]
    public void LabelCollision_fails_two_labels_with_one_64_bit_value()
    {
        var set = Load(Row("a", cls: "node"), Row("a/b", parent: "a", label: "b", cls: "node"));

        var failure = Assert.Single(new LabelCollisionCheck(AEqualsB).Run(set));
        Assert.Equal(new CheckId("V19.LabelCollision"), failure.Check);
        Assert.Equal(StreamsFile, failure.File);
        Assert.Equal("$.streams[1].label", failure.Field);
        Assert.Contains("\"a\"", failure.Problem, StringComparison.Ordinal);
    }

    [Fact]
    public void LabelCollision_passes_one_label_used_twice()
    {
        var set = Load(Row("a", cls: "node"), Row("a/a", parent: "a", label: "a", cls: "node"), Row("c", cls: "node"));

        Assert.Empty(new LabelCollisionCheck(AEqualsB).Run(set));
        Assert.Empty(new LabelCollisionCheck().Run(set));
    }

    [Fact]
    public void LabelCollision_scans_content_ids_against_labels()
    {
        ContentKind[] kinds = [.. ContentKinds.All, new IdKind()];
        var set = ContentLoader.Load(
            [Streams(Row("a", cls: "node")), new ContentFile(IdsFile, "{\"ids\": [{\"id\": \"a\"}, {\"id\": \"b\"}]}")],
            kinds);

        var failure = Assert.Single(new LabelCollisionCheck(kinds, AEqualsB).Run(set));
        Assert.Equal(IdsFile, failure.File);
        Assert.Equal("$.ids[1].id", failure.Field);
    }

    [Fact]
    public void LabelCollision_reads_the_streams_and_every_kind_file()
    {
        var reads = new LabelCollisionCheck().Reads;

        Assert.Equal(ContentKinds.All.Select(k => k.FileName).Order(StringComparer.Ordinal), reads.Order(StringComparer.Ordinal));
        Assert.Contains(StreamsFile, reads);
    }

    [Fact]
    public void The_repository_streams_pass_every_stream_check()
    {
        var set = ContentLoader.Load(ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")));

        Assert.NotEmpty(set.RecordsIn(StreamsFile));
        Assert.Empty(new StreamRowsCheck().Run(set));
        Assert.Empty(new DomainDisjointCheck().Run(set));
        Assert.Empty(new LabelCollisionCheck().Run(set));
    }

    [Fact]
    public void The_repository_registers_h_notice_with_the_SV_4_1_arity()
    {
        var set = ContentLoader.Load(ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")));

        var notice = Assert.Single(set.All<StreamRow>(), r => r.Label == "h:notice");
        Assert.Equal("dungeon/floor/{N}/checks", notice.Parent);
        Assert.Equal([IndexType.LabelId, IndexType.LabelId, IndexType.ULong, IndexType.ULong], notice.Indices.Select(x => x.Type));
        Assert.Equal(CountClass.Keyed, notice.Class);
        Assert.Equal(Wave.M1, notice.Wave);
    }

    [Fact]
    public void The_repository_keeps_the_inert_rows_at_M2()
    {
        var set = ContentLoader.Load(ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")));

        var inert = set.All<StreamRow>().Where(r => r.IsInert).Select(r => r.Id).Order(StringComparer.Ordinal);
        Assert.Equal(
            ["dungeon/floor/{N}/checks/h:lurkStealth/{groupId:}/{positionIndex}/{band:}/{die}", "dungeon/route/omen/{themeId:}", "loot/lootPlan/{N}/bossCard/{entryIndex}"],
            inert);
    }

    // --- Helpers ---

    private static ulong AEqualsB(string s) => s is "a" or "b" ? 1UL : KeyAlgebra.LabelId(s);

    private static ContentSet Load(params string[] rows) => ContentLoader.Load([Streams(rows)]);

    private static ContentFile Streams(params string[] rows) => new(StreamsFile, "{\"streams\": [" + string.Join(", ", rows) + "]}");

    private static string Idx(params (string Name, string Type)[] indices) =>
        "[" + string.Join(", ", indices.Select(x => $"{{\"name\": \"{x.Name}\", \"type\": \"{x.Type}\"}}")) + "]";

    /// <summary>
    /// One stream-row JSON object. A null <paramref name="parent"/> or <paramref name="count"/> leaves the field null or out; the class defaults to <c>node</c>;
    /// <paramref name="label"/> defaults to the path; <paramref name="extra"/> is raw JSON appended to the object.
    /// </summary>
    private static string Row(
        string path,
        string? parent = null,
        string? label = null,
        string indices = "[]",
        string cls = "node",
        string? count = null,
        string keyType = "core",
        string? extra = null)
    {
        var parts = new List<string>
        {
            $"\"path\": \"{path}\"",
            "\"alias\": []",
            $"\"parent\": {(parent is null ? "null" : $"\"{parent}\"")}",
            $"\"label\": \"{label ?? path}\"",
            $"\"indices\": {indices}",
            $"\"class\": \"{cls}\"",
            "\"stage\": \"GS1\"",
            "\"readers\": []",
            "\"consumers\": []",
            "\"retryScope\": \"never\"",
            "\"rules\": []",
            $"\"keyType\": \"{keyType}\"",
            "\"owner\": \"MAIN\"",
            "\"source\": []",
            "\"wave\": \"M1\"",
        };
        if (count is not null)
        {
            parts.Add($"\"countFormula\": \"{count}\"");
        }

        if (extra is not null)
        {
            parts.Add(extra);
        }

        return "{" + string.Join(", ", parts) + "}";
    }

    /// <summary>A fixture kind whose record ids follow the <see cref="ContentId"/> grammar: <c>{"ids": [{id}]}</c>.</summary>
    private sealed class IdKind : ContentKind
    {
        public override string FileName => IdsFile;

        public override string ArrayName => "ids";

        public override bool IdsAreContentIds => true;

        public override ImmutableArray<ContentRecord> Read(ContentFile file)
        {
            var rows = Need(file, "$.ids", ContentJson.Read<IdsDto>(file).Ids);
            var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
            for (int i = 0; i < rows.Length; i++)
            {
                string at = Item("$.ids", i);
                var r = Need(file, at, rows[i]);
                result.Add(new IdRecord(file.Name, Id(file, Member(at, "id"), r.Id).Value));
            }

            return result.MoveToImmutable();
        }

        private sealed class IdsDto
        {
            [JsonPropertyName("ids")] public IdDto?[]? Ids { get; set; }
        }

        private sealed class IdDto
        {
            [JsonPropertyName("id")] public string? Id { get; set; }
        }
    }

    private sealed record IdRecord(string File, string Id) : ContentRecord(File, Id)
    {
        public override void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(1);
            w.String(0, Id);
            w.EndRecord();
        }
    }
}
