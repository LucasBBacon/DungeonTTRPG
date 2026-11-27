using Dungeon.Core.Content;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The stream registry kind, <c>registry/streams.json</c> (SV 9.1 <c>StreamRow</c>, plan calls 1-4).</summary>
public class StreamRowTests
{
    private const string StreamsFile = "registry/streams.json";

    [Fact]
    public void A_valid_two_row_file_loads()
    {
        var set = ContentLoader.Load([Streams(Dungeon(), Route())]);

        var root = Assert.IsType<StreamRow>(set.Find<StreamRow>("dungeon"));
        Assert.Equal(StreamsFile, root.File);
        Assert.Null(root.Parent);
        Assert.Equal("dungeon", root.Label);
        Assert.Equal(CountClass.Node, root.Class);
        Assert.Equal("0", root.Count?.ToString());
        Assert.Equal(RetryScope.Never, root.RetryScope);
        Assert.Equal(KeyType.Core, root.KeyType);
        Assert.Equal(Wave.M1, root.Wave);
        Assert.False(root.IsInert);

        var route = Assert.IsType<StreamRow>(set.Find<StreamRow>("dungeon/route"));
        Assert.Equal("dungeon", route.Parent);
        Assert.Equal(CountClass.Keyed, route.Class);
        Assert.Null(route.Count);
        Assert.Empty(route.Indices);
        Assert.Equal(["GS route"], route.Consumers.ToArray());
        Assert.Equal(["SV 4.3"], route.Source.ToArray());
    }

    [Fact]
    public void Every_class_index_type_retry_scope_and_key_type_token_parses()
    {
        string[] classes = ["node", "fixed", "fixed-per-data", "leaf-formula", "per-entry", "vector", "keyed", "sequential-saved", "display", "sim"];
        CountClass[] expected =
        [
            CountClass.Node, CountClass.Fixed, CountClass.FixedPerData, CountClass.LeafFormula, CountClass.PerEntry,
            CountClass.Vector, CountClass.Keyed, CountClass.SequentialSaved, CountClass.Display, CountClass.Sim,
        ];
        for (int i = 0; i < classes.Length; i++)
        {
            var row = Single(Row("s", cls: classes[i]));
            Assert.Equal(expected[i], row.Class);
        }

        var typed = Single(Row(
            "s/{a}/{b}/{c:}",
            indices: """[{"name": "a", "type": "ulong"}, {"name": "b", "type": "uint"}, {"name": "c", "type": "labelId"}]"""));
        Assert.Equal([IndexType.ULong, IndexType.UInt, IndexType.LabelId], typed.Indices.Select(x => x.Type));

        string[] scopes = ["never", "floorKey", "attempt", "stage"];
        RetryScope[] scopeValues = [RetryScope.Never, RetryScope.FloorKey, RetryScope.Attempt, RetryScope.Stage];
        for (int i = 0; i < scopes.Length; i++)
        {
            Assert.Equal(scopeValues[i], Single(Row("s", retryScope: scopes[i])).RetryScope);
        }

        Assert.Equal(KeyType.Display, Single(Row("s", keyType: "display")).KeyType);
        Assert.Equal(KeyType.Sim, Single(Row("s", keyType: "sim")).KeyType);
    }

    [Fact]
    public void A_vector_row_has_a_length_and_a_per_entry_row_has_a_cap()
    {
        Assert.Equal(12UL, Single(Row("s", cls: "vector", count: null, extra: "\"length\": 12")).Length);
        var perEntry = Single(Row("s", cls: "per-entry", count: "\"9\"", extra: "\"cap\": \"owed:LE\""));
        Assert.Equal("owed:LE", perEntry.Cap?.ToString());
        Assert.True(perEntry.Cap?.IsOwed);
    }

    [Fact]
    public void A_later_wave_row_is_inert()
    {
        Assert.True(Waves.IsInert(Wave.M2));
        Assert.False(Waves.IsInert(Wave.M1));
        Assert.Equal(Wave.M1, Waves.Build);
        Assert.True(Single(Row("s", wave: "M2")).IsInert);
        Assert.True(Single(Row("s", wave: "Later")).IsInert);
    }

    [Theory]
    [InlineData("M1", Wave.M1)]
    [InlineData("M2", Wave.M2)]
    [InlineData("M3", Wave.M3)]
    [InlineData("M4", Wave.M4)]
    [InlineData("Later", Wave.Later)]
    public void A_wave_parses_by_its_exact_name(string s, Wave wave)
    {
        Assert.True(Waves.TryParse(s, out var parsed));
        Assert.Equal(wave, parsed);
    }

    [Theory]
    [InlineData("m1")]
    [InlineData("later")]
    [InlineData("0")]
    [InlineData("M5")]
    [InlineData("")]
    public void A_bad_wave_is_rejected(string s)
    {
        Assert.False(Waves.TryParse(s, out _));
    }

    // --- Load-time rejections ---

    [Theory]
    [InlineData("dungeon/Floor/{n-1}")]
    [InlineData("dungeon//route")]
    [InlineData("/dungeon")]
    [InlineData("dungeon/")]
    [InlineData("dungeon/{}")]
    [InlineData("dungeon/{1n}")]
    [InlineData("dungeon/{n")]
    [InlineData("dungeon floor")]
    public void A_bad_path_is_rejected(string path)
    {
        var ex = LoadFails(Streams(Row(path)));
        Assert.Equal(StreamsFile, ex.File);
        Assert.Equal("$.streams[0].path", ex.Field);
    }

    [Theory]
    [InlineData("h object")]
    [InlineData("{n}")]
    [InlineData("a/b")]
    public void A_bad_label_is_rejected(string label)
    {
        var ex = LoadFails(Streams(Row("s", label: label)));
        Assert.Equal(StreamsFile, ex.File);
        Assert.Equal("$.streams[0].label", ex.Field);
    }

    [Fact]
    public void A_bad_index_name_is_rejected()
    {
        var ex = LoadFails(Streams(Row("s", indices: """[{"name": "n-1", "type": "uint"}]""")));
        Assert.Equal("$.streams[0].indices[0].name", ex.Field);
    }

    [Theory]
    [InlineData("class", "\"Node\"")]
    [InlineData("class", "\"fixedPerData\"")]
    [InlineData("retryScope", "\"floorkey\"")]
    [InlineData("keyType", "\"Core\"")]
    [InlineData("wave", "\"m1\"")]
    public void A_bad_enum_is_rejected(string field, string json)
    {
        var ex = LoadFails(Streams(Row("s", override_: (field, json))));
        Assert.Equal(StreamsFile, ex.File);
        Assert.Equal("$.streams[0]." + field, ex.Field);
    }

    [Fact]
    public void A_bad_index_type_is_rejected()
    {
        var ex = LoadFails(Streams(Row("s", indices: """[{"name": "n", "type": "uLong"}]""")));
        Assert.Equal("$.streams[0].indices[0].type", ex.Field);
    }

    [Fact]
    public void An_unparseable_count_formula_is_rejected()
    {
        var ex = LoadFails(Streams(Row("s", cls: "fixed", count: "\"1.5\"")));
        Assert.Equal(StreamsFile, ex.File);
        Assert.Equal("$.streams[0].countFormula", ex.Field);
    }

    [Fact]
    public void An_unparseable_cap_is_rejected()
    {
        var ex = LoadFails(Streams(Row("s", cls: "per-entry", extra: "\"cap\": \"owed:\"")));
        Assert.Equal("$.streams[0].cap", ex.Field);
    }

    [Fact]
    public void A_length_on_a_non_vector_row_is_rejected()
    {
        var ex = LoadFails(Streams(Row("s", cls: "fixed", extra: "\"length\": 4")));
        Assert.Equal(StreamsFile, ex.File);
        Assert.Equal("$.streams[0].length", ex.Field);
    }

    [Fact]
    public void A_cap_on_a_non_per_entry_row_is_rejected()
    {
        var ex = LoadFails(Streams(Row("s", cls: "vector", count: null, extra: "\"cap\": \"4\"")));
        Assert.Equal(StreamsFile, ex.File);
        Assert.Equal("$.streams[0].cap", ex.Field);
    }

    [Fact]
    public void A_missing_parent_is_rejected()
    {
        var ex = LoadFails(Streams(Dungeon(), Row("dungeon/route", parent: "\"dungeons\"", label: "route")));
        Assert.Equal(StreamsFile, ex.File);
        Assert.Equal("$.streams[1].parent", ex.Field);
        Assert.Contains("dungeons", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_missing_required_field_is_rejected()
    {
        var ex = LoadFails(Streams(Row("s", override_: ("owner", "null"))));
        Assert.Equal("$.streams[0].owner", ex.Field);
    }

    [Fact]
    public void A_repeated_path_is_rejected()
    {
        var ex = LoadFails(Streams(Dungeon(), Dungeon()));
        Assert.Equal("$.streams[1].path", ex.Field);
    }

    // --- Canonical form ---

    [Fact]
    public void Every_field_is_in_the_manifest()
    {
        var baseline = ContentHashing.Compute(ContentLoader.Load([Streams(Dungeon(), Route())]));
        string[] edits =
        [
            Route(alias: """["r"]"""),
            Route(owner: "RS"),
            Route(wave: "M2"),
            Route(consumers: """["x"]"""),
        ];
        foreach (var edit in edits)
        {
            var edited = ContentHashing.Compute(ContentLoader.Load([Streams(Dungeon(), edit)]));
            Assert.NotEqual(baseline.Manifest, edited.Manifest);
            Assert.NotEqual(baseline.GenContent, edited.GenContent);
        }
    }

    // --- Builders ---

    private static StreamRow Single(string row) =>
        Assert.IsType<StreamRow>(Assert.Single(ContentLoader.Load([Streams(row)]).All<StreamRow>()));

    private static ContentLoadException LoadFails(params ContentFile[] files) =>
        Assert.Throws<ContentLoadException>(() => ContentLoader.Load(files));

    private static ContentFile Streams(params string[] rows) =>
        new(StreamsFile, "{\"streams\": [" + string.Join(", ", rows) + "]}");

    private static string Dungeon() => Row("dungeon", cls: "node", count: "\"0\"");

    private static string Route(string alias = "[]", string owner = "MAIN", string wave = "M1", string consumers = "[\"GS route\"]") =>
        Row(
            "dungeon/route",
            parent: "\"dungeon\"",
            label: "route",
            cls: "keyed",
            count: null,
            alias: alias,
            owner: owner,
            wave: wave,
            consumers: consumers,
            source: "[\"SV 4.3\"]");

    /// <summary>
    /// One stream-row JSON object. String arguments are raw JSON except <paramref name="path"/>, <paramref name="label"/>,
    /// <paramref name="cls"/>, <paramref name="retryScope"/>, <paramref name="keyType"/>, <paramref name="owner"/> and
    /// <paramref name="wave"/>, which are quoted here. A null <paramref name="count"/> leaves the formula out;
    /// <paramref name="override_"/> replaces one field's raw JSON.
    /// </summary>
    private static string Row(
        string path,
        string parent = "null",
        string? label = null,
        string indices = "[]",
        string cls = "fixed",
        string? count = "\"1\"",
        string retryScope = "never",
        string keyType = "core",
        string owner = "MAIN",
        string wave = "M1",
        string alias = "[]",
        string consumers = "[]",
        string source = "[]",
        string? extra = null,
        (string Field, string Json)? override_ = null)
    {
        label ??= path.Split('/')[0];
        var fields = new List<(string Name, string Json)>
        {
            ("path", Quote(path)),
            ("alias", alias),
            ("parent", parent),
            ("label", Quote(label)),
            ("indices", indices),
            ("class", Quote(cls)),
            ("stage", "\"GS1\""),
            ("readers", "[]"),
            ("consumers", consumers),
            ("retryScope", Quote(retryScope)),
            ("rules", "[]"),
            ("keyType", Quote(keyType)),
            ("owner", Quote(owner)),
            ("source", source),
            ("wave", Quote(wave)),
        };
        if (count is not null)
        {
            fields.Add(("countFormula", count));
        }

        if (override_ is { } o)
        {
            int at = fields.FindIndex(f => f.Name == o.Field);
            fields[at] = (o.Field, o.Json);
        }

        var parts = fields.Select(f => $"\"{f.Name}\": {f.Json}").ToList();
        if (extra is not null)
        {
            parts.Add(extra);
        }

        return "{" + string.Join(", ", parts) + "}";
    }

    private static string Quote(string s) => "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
}
