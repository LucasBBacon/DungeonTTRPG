using System.Collections.Immutable;
using System.Text;
using System.Text.Json.Serialization;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Dungeon.Core.Validation.Checks;
using Xunit;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Tests;

/// <summary>V7.NameRegistry, V24 r1, r2 and r6, and the greyed-reason keys, over a fixture kind of named records.</summary>
public class NamingChecksTests
{
    private const string NamedFile = "test/named.json";

    private static readonly ImmutableArray<ContentKind> Kinds = [.. ContentKinds.All, new NamedKind()];

    // --- V7.NameRegistry ---

    [Fact]
    public void NameRegistry_passes_named_records_in_known_categories()
    {
        Assert.Empty(Run(new NameRegistryCheck(Kinds), Named(Rec("a", "Old Ragged", key: "T.n.a"))));
    }

    [Fact]
    public void NameRegistry_fails_an_empty_name()
    {
        var failure = Assert.Single(Run(new NameRegistryCheck(Kinds), Named(Rec("a", "", key: "T.n.a"))));
        Assert.Equal(new CheckId("V7.NameRegistry"), failure.Check);
        Assert.Equal(NamedFile, failure.File);
        Assert.Equal("$.named[0].name", failure.Field);
    }

    [Fact]
    public void NameRegistry_fails_a_control_character()
    {
        var failure = Assert.Single(Run(new NameRegistryCheck(Kinds), Named(Rec("a", "Old\\tRagged", key: "T.n.a"))));
        Assert.Equal("$.named[0].name", failure.Field);
    }

    [Fact]
    public void NameRegistry_fails_a_category_not_in_the_name_list()
    {
        var failure = Assert.Single(Run(
            new NameRegistryCheck(Kinds),
            Named(Rec("a", "Old Ragged", key: "T.n.a"), Rec("b", "Bobbin", category: "gizmo", key: "T.n.b"))));
        Assert.Equal("$.named[1].category", failure.Field);
    }

    // --- V24 r1 ---

    [Fact]
    public void R1_fails_two_equal_names_with_different_meanings()
    {
        var failure = Assert.Single(Run(
            new R1ExactEqualityCheck(Kinds),
            Named(
                Rec("a", "Old Ragged", meaningId: "boss.old_ragged", key: "T.n.a"),
                Rec("b", "Old Ragged", meaningId: "enemy.old_ragged", key: "T.n.b"))));
        Assert.Equal(new CheckId("V24.R1ExactEquality"), failure.Check);
        Assert.Equal(NamedFile, failure.File);
        Assert.Equal("$.named[1].name", failure.Field);
    }

    [Fact]
    public void R1_fails_two_equal_names_without_meanings()
    {
        Assert.Single(Run(
            new R1ExactEqualityCheck(Kinds),
            Named(Rec("a", "Old Ragged", key: "T.n.a"), Rec("b", "Old Ragged", key: "T.n.b"))));
    }

    [Fact]
    public void R1_passes_two_equal_names_with_the_same_meaning()
    {
        Assert.Empty(Run(
            new R1ExactEqualityCheck(Kinds),
            Named(
                Rec("a", "Old Ragged", meaningId: "boss.old_ragged", key: "T.n.a"),
                Rec("b", "Old Ragged", meaningId: "boss.old_ragged", key: "T.n.b"))));
    }

    // --- V24 r2 ---

    [Fact]
    public void R2_fails_a_retired_name_in_its_category()
    {
        var failure = Assert.Single(Run(
            new R2RetiredReusedCheck(Kinds),
            Named(Rec("a", "Underpillow", category: "heirloom", key: "T.n.a"))));
        Assert.Equal(new CheckId("V24.R2RetiredReused"), failure.Check);
        Assert.Equal("$.named[0].name", failure.Field);
    }

    [Fact]
    public void R2_passes_a_retired_name_in_another_category()
    {
        Assert.Empty(Run(
            new R2RetiredReusedCheck(Kinds),
            Named(Rec("a", "Underpillow", category: "place", key: "T.n.a"))));
    }

    // --- V24 r6 ---

    [Fact]
    public void R6_fails_an_internal_record_with_a_key()
    {
        var failure = Assert.Single(Run(
            new R6InternalByKeyCheck(Kinds),
            Named(Rec("a", "fx_ragged", isInternal: true, key: "T.n.a")),
            Strings(Str("T.n.a", "Ragged"))));
        Assert.Equal(new CheckId("V24.R6InternalByKey"), failure.Check);
        Assert.Equal("$.named[0].key", failure.Field);
    }

    [Fact]
    public void R6_fails_a_shown_record_without_a_key()
    {
        var failure = Assert.Single(Run(new R6InternalByKeyCheck(Kinds), Named(Rec("a", "Old Ragged"))));
        Assert.Equal("$.named[0].key", failure.Field);
    }

    [Fact]
    public void R6_fails_a_key_missing_from_the_strings_table()
    {
        var failure = Assert.Single(Run(
            new R6InternalByKeyCheck(Kinds),
            Named(Rec("a", "Old Ragged", key: "T.n.a")),
            Strings(Str("T.n.b", "Bobbin"))));
        Assert.Equal("$.named[0].key", failure.Field);
    }

    [Fact]
    public void R6_passes_when_internal_records_have_no_key_and_shown_records_have_a_string()
    {
        Assert.Empty(Run(
            new R6InternalByKeyCheck(Kinds),
            Named(Rec("a", "fx_ragged", isInternal: true), Rec("b", "Old Ragged", key: "T.n.b")),
            Strings(Str("T.n.b", "Old Ragged"))));
    }

    // --- Greyed-reason keys ---

    [Fact]
    public void GreyedReasonKeys_fails_a_reason_without_its_string()
    {
        var failure = Assert.Single(Run(
            new GreyedReasonKeysCheck(),
            Greyed("{\"id\": \"notincombat\", \"doc\": \"ITEMS\"}"),
            Strings(Str("ITEMS.greyed.other", "Other"))));
        Assert.Equal(new CheckId("V24.GreyedReasonKeys"), failure.Check);
        Assert.Equal("naming/greyed_reasons.json", failure.File);
        Assert.Equal("$.reasons[0].id", failure.Field);
    }

    [Fact]
    public void GreyedReasonKeys_passes_once_the_string_exists()
    {
        Assert.Empty(Run(
            new GreyedReasonKeysCheck(),
            Greyed("{\"id\": \"notincombat\", \"doc\": \"ITEMS\"}"),
            Strings(Str("ITEMS.greyed.notincombat", "Not in combat"))));
    }

    // --- The naming kinds ---

    [Fact]
    public void A_strings_key_that_is_not_doc_registry_id_is_rejected()
    {
        var ex = Assert.Throws<ContentLoadException>(() => ContentLoader.Load([Strings(Str("ITEMS.greyed", "x"))]));
        Assert.Equal("strings/en.json", ex.File);
        Assert.Equal("$.strings[0].key", ex.Field);
    }

    [Fact]
    public void A_repeated_strings_key_is_named_by_its_key_field()
    {
        var ex = Assert.Throws<ContentLoadException>(
            () => ContentLoader.Load([Strings(Str("T.n.a", "x"), Str("T.n.a", "y"))]));
        Assert.Equal("$.strings[1].key", ex.Field);
    }

    [Fact]
    public void A_non_ascii_greyed_doc_is_rejected()
    {
        var ex = Assert.Throws<ContentLoadException>(
            () => ContentLoader.Load([Greyed("{\"id\": \"x\", \"doc\": \"É\"}")]));
        Assert.Equal("$.reasons[0].doc", ex.Field);
    }

    [Theory]
    [InlineData("a b")]
    [InlineData("")]
    [InlineData("IT.EMS")]
    [InlineData("ITEMS/x")]
    public void A_greyed_doc_that_is_not_one_key_part_is_rejected_at_doc(string doc)
    {
        var ex = Assert.Throws<ContentLoadException>(
            () => ContentLoader.Load([Greyed("{\"id\": \"x\", \"doc\": \"" + doc + "\"}")]));
        Assert.Equal("naming/greyed_reasons.json", ex.File);
        Assert.Equal("$.reasons[0].doc", ex.Field);
    }

    [Fact]
    public void Strings_table_text_is_nfc()
    {
        string path = Path.Combine(RepoRoot.Find(), "content", "strings", "en.json");
        var set = ContentLoader.Load([new ContentFile("strings/en.json", File.ReadAllText(path))]);
        Assert.All(set.All<StringEntry>(), s => Assert.True(s.Text.IsNormalized(NormalizationForm.FormC), s.Id));
    }

    // --- The read-set cache sees display fields (SV 5.3, Review Focus 5) ---

    [Fact]
    public void Editing_a_retired_name_reruns_r2()
    {
        var cache = new CheckCache();
        IStaticCheck[] checks = [new R2RetiredReusedCheck(Kinds)];
        var named = Named(Rec("a", "Underpillow", category: "heirloom", key: "T.n.a"));

        var first = ValidationRunner.Run(Load(ChecksJson(), named, RetiredFile("Underpillow", "heirloom")), checks, cache);
        Assert.Single(first.Failures);

        var second = ValidationRunner.Run(Load(ChecksJson(), named, RetiredFile("Overpillow", "heirloom")), checks, cache);
        Assert.Equal(2, cache.Runs);
        Assert.Equal(0, cache.Hits);
        Assert.Empty(second.Failures);

        ValidationRunner.Run(Load(ChecksJson(), named, RetiredFile("Overpillow", "heirloom")), checks, cache);
        Assert.Equal(2, cache.Runs);
        Assert.Equal(1, cache.Hits);
    }

    [Fact]
    public void Editing_a_retired_category_reruns_r2()
    {
        var cache = new CheckCache();
        IStaticCheck[] checks = [new R2RetiredReusedCheck(Kinds)];
        var named = Named(Rec("a", "Underpillow", category: "heirloom", key: "T.n.a"));

        ValidationRunner.Run(Load(ChecksJson(), named, RetiredFile("Underpillow", "heirloom")), checks, cache);
        var second = ValidationRunner.Run(Load(ChecksJson(), named, RetiredFile("Underpillow", "place")), checks, cache);

        Assert.Equal(2, cache.Runs);
        Assert.Empty(second.Failures);
    }

    [Fact]
    public void Editing_a_name_list_category_reruns_the_name_registry()
    {
        var cache = new CheckCache();
        IStaticCheck[] checks = [new NameRegistryCheck(Kinds)];
        var named = Named(Rec("a", "Bobbin", category: "gizmo", key: "T.n.a"));

        var first = ValidationRunner.Run(Load(ChecksJson(), named, NameList("boss")), checks, cache);
        Assert.Single(first.Failures);

        var second = ValidationRunner.Run(Load(ChecksJson(), named, NameList("gizmo")), checks, cache);
        Assert.Equal(2, cache.Runs);
        Assert.Empty(second.Failures);
    }

    [Fact]
    public void Editing_a_display_name_reruns_r1()
    {
        var cache = new CheckCache();
        IStaticCheck[] checks = [new R1ExactEqualityCheck(Kinds)];

        var first = ValidationRunner.Run(
            Load(ChecksJson(), Named(Rec("a", "Old Ragged", key: "T.n.a"), Rec("b", "Old Ragged", key: "T.n.b"))), checks, cache);
        Assert.Single(first.Failures);

        var second = ValidationRunner.Run(
            Load(ChecksJson(), Named(Rec("a", "Old Ragged", key: "T.n.a"), Rec("b", "Bobbin", key: "T.n.b"))), checks, cache);
        Assert.Equal(2, cache.Runs);
        Assert.Empty(second.Failures);
    }

    [Fact]
    public void Editing_strings_text_changes_the_digest()
    {
        var a = Load(Strings(Str("T.n.a", "Old Ragged")));
        var b = Load(Strings(Str("T.n.a", "Old Raggéd")));
        Assert.NotEqual(a.DigestOf(["strings/en.json"]), b.DigestOf(["strings/en.json"]));
        Assert.Equal(a.DigestOf(["strings/en.json"]), Load(Strings(Str("T.n.a", "Old Ragged"))).DigestOf(["strings/en.json"]));
    }

    // --- Helpers ---

    private static ImmutableArray<CheckFailure> Run(IStaticCheck check, params ContentFile[] files) =>
        check.Run(Load(files));

    private static ContentSet Load(params ContentFile[] files) => ContentLoader.Load(WithNamingFiles(files), Kinds);

    // The given files plus the default name list and retired names, unless the caller gives its own.
    private static List<ContentFile> WithNamingFiles(ContentFile[] files)
    {
        var all = new List<ContentFile>(files);
        if (!files.Any(f => f.Name == "naming/name_list.json"))
        {
            all.Add(new ContentFile(
                "naming/name_list.json",
                "{\"entries\": [" + Entry("a:0", "heirloom") + ", " + Entry("a:1", "place") + ", " + Entry("a:2", "boss") + "]}"));
        }

        if (!files.Any(f => f.Name == "naming/retired_names.json"))
        {
            all.Add(new ContentFile(
                "naming/retired_names.json",
                "{\"retired\": [{\"id\": \"r0000\", \"name\": \"Underpillow\", \"category\": \"heirloom\", "
                + "\"doc\": \"heirlooms-curses\", \"outcome\": \"retired (renamed the Bobbin in CA T4)\"}]}"));
        }

        return all;
    }

    private static ContentFile NameList(params string[] categories) =>
        new("naming/name_list.json", "{\"entries\": [" + string.Join(", ", categories.Select((c, i) => Entry("a:" + i, c))) + "]}");

    private static ContentFile RetiredFile(string name, string category) =>
        new("naming/retired_names.json",
            $"{{\"retired\": [{{\"id\": \"r0000\", \"name\": \"{name}\", \"category\": \"{category}\", "
            + "\"doc\": \"heirlooms-curses\", \"outcome\": \"retired (renamed the Bobbin in CA T4)\"}]}");

    // The repository's checks.json, so ValidationRunner finds the rows of the real checks.
    private static ContentFile ChecksJson() =>
        new("registry/checks.json", File.ReadAllText(Path.Combine(RepoRoot.Find(), "content", "registry", "checks.json")));

    private static string Entry(string id, string category) =>
        $"{{\"id\": \"{id}\", \"doc\": \"a\", \"name\": \"n\", \"category\": \"{category}\", \"status\": \"unclear\", "
        + "\"outcome\": \"final as written\", \"outcomeKind\": \"final\"}";

    private static ContentFile Named(params string[] records) =>
        new(NamedFile, "{\"named\": [" + string.Join(", ", records) + "]}");

    private static ContentFile Strings(params string[] records) =>
        new("strings/en.json", "{\"strings\": [" + string.Join(", ", records) + "]}");

    private static ContentFile Greyed(params string[] records) =>
        new("naming/greyed_reasons.json", "{\"reasons\": [" + string.Join(", ", records) + "]}");

    private static string Str(string key, string text) =>
        $"{{\"key\": \"{key}\", \"text\": \"{text}\", \"field\": \"name\"}}";

    private static string Rec(
        string id, string name, string category = "boss", string? meaningId = null, bool isInternal = false, string? key = null) =>
        "{" + string.Join(", ",
            $"\"id\": \"{id}\"",
            $"\"name\": \"{name}\"",
            $"\"category\": \"{category}\"",
            $"\"internal\": {(isInternal ? "true" : "false")}")
        + (meaningId is null ? "" : $", \"meaningId\": \"{meaningId}\"")
        + (key is null ? "" : $", \"key\": \"{key}\"")
        + "}";

    /// <summary>A fixture kind whose records reach the screen: <c>{id, name, category, meaningId?, internal, key?}</c>.</summary>
    private sealed class NamedKind : ContentKind, INamedRecordSource
    {
        public override string FileName => NamedFile;

        public override string ArrayName => "named";

        public override ImmutableArray<ContentRecord> Read(ContentFile file)
        {
            var rows = Need(file, "$.named", ContentJson.Read<NamedDto>(file).Named);
            var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
            for (int i = 0; i < rows.Length; i++)
            {
                string at = Item("$.named", i);
                var r = Need(file, at, rows[i]);
                result.Add(new NamedFixture(
                    file.Name,
                    Id(file, Member(at, "id"), r.Id).Value,
                    i,
                    Need(file, Member(at, "name"), r.Name),
                    Need(file, Member(at, "category"), r.Category),
                    OptionalRuleString(file, Member(at, "meaningId"), r.MeaningId),
                    Need(file, Member(at, "internal"), r.Internal),
                    OptionalRuleString(file, Member(at, "key"), r.Key)));
            }

            return result.MoveToImmutable();
        }

        public ImmutableArray<NamedRecord> NamedRecords(ContentSet set) =>
            set.RecordsIn(FileName)
                .Cast<NamedFixture>()
                .Select(r => new NamedRecord(
                    r.File,
                    Member(Item("$.named", r.Index), "name"),
                    r.Name,
                    r.Category,
                    r.MeaningId,
                    r.Internal,
                    r.Key,
                    Member(Item("$.named", r.Index), "category"),
                    Member(Item("$.named", r.Index), "key")))
                .ToImmutableArray();

        private sealed class NamedDto
        {
            [JsonPropertyName("named")] public RecordDto?[]? Named { get; set; }
        }

        private sealed class RecordDto
        {
            [JsonPropertyName("id")] public string? Id { get; set; }
            [JsonPropertyName("name")] public string? Name { get; set; }
            [JsonPropertyName("category")] public string? Category { get; set; }
            [JsonPropertyName("meaningId")] public string? MeaningId { get; set; }
            [JsonPropertyName("internal")] public bool? Internal { get; set; }
            [JsonPropertyName("key")] public string? Key { get; set; }
        }
    }

    private sealed record NamedFixture(
        string File, string Id, int Index, string Name, string Category, string? MeaningId, bool Internal, string? Key)
        : ContentRecord(File, Id), IDisplayCanonical
    {
        public override void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(6);
            w.String(0, Id);
            w.String(1, Category);
            w.OptionalString(2, MeaningId);
            w.Bool(3, Internal);
            w.OptionalString(4, Key);
            w.Int32(5, Index);
            w.EndRecord();
        }

        public void WriteDisplay(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.String(0, Id);
            w.Text(1, Name);
            w.EndRecord();
        }
    }
}
