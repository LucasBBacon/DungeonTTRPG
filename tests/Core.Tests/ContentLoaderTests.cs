using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Xunit;

namespace Dungeon.Core.Tests;

public class ContentLoaderTests
{
    private const string ChecksFile = "registry/checks.json";
    private const string ReportsFile = "registry/reports.json";
    private const string ArmsFile = "registry/arms.json";

    // --- The three done-when fixtures ---

    [Fact]
    public void A_check_missing_phase_names_the_file_and_field()
    {
        var ex = LoadFails(Checks(Check("V19.ClassDeclared", phase: null)));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[0].phase", ex.Field);
    }

    [Fact]
    public void A_check_whose_deps_name_a_missing_check_names_the_reference()
    {
        var ex = LoadFails(Checks(Check("V19.ClassDeclared", deps: "[\"V99.Missing\"]")));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[0].deps[0]", ex.Field);
    }

    [Fact]
    public void Two_checks_with_one_id_name_the_second()
    {
        var ex = LoadFails(Checks(Check("V19.ClassDeclared"), Check("V19.ClassDeclared")));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[1].id", ex.Field);
    }

    // --- Review Focus 1: every failure path names its file and field ---

    [Theory]
    [InlineData("\"5\"")]
    [InlineData("1.5")]
    [InlineData("99999999999")]
    public void A_bad_budget_names_the_budget_field(string budget)
    {
        var ex = LoadFails(Checks(Check("V19.ClassDeclared", budgetMs: budget)));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[0].budgetMs", ex.Field);
    }

    [Fact]
    public void A_non_ascii_rule_string_is_rejected()
    {
        var ex = LoadFails(Checks(Check("V19.ClassDeclared", owner: "\"Lé\"")));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[0].owner", ex.Field);
    }

    [Fact]
    public void An_unknown_property_is_rejected()
    {
        var ex = LoadFails(Checks(Check("V19.ClassDeclared", extra: ", \"colour\": \"red\"")));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[0].colour", ex.Field);
    }

    [Fact]
    public void A_duplicated_property_is_rejected()
    {
        var ex = LoadFails(Checks(Check("V19.ClassDeclared", extra: ", \"owner\": \"MAIN\"")));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[0].owner", ex.Field);
    }

    [Fact]
    public void A_report_naming_a_missing_arm_names_the_reference()
    {
        var ex = LoadFails(Checks(), Arms(Arm("Party.Solo")), Reports(Report("HealerGap", arms: "[\"Party.Missing\"]")));
        Assert.Equal(ReportsFile, ex.File);
        Assert.Equal("$.reports[0].arms[0]", ex.Field);
    }

    [Fact]
    public void An_unreduced_threshold_is_rejected()
    {
        var ex = LoadFails(Reports(Report("HealerGap", threshold: "\"6/8\"")));
        Assert.Equal(ReportsFile, ex.File);
        Assert.Equal("$.reports[0].threshold", ex.Field);
    }

    [Fact]
    public void A_check_id_outside_its_grammar_is_rejected()
    {
        var ex = LoadFails(Checks(Check("v19.classDeclared")));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks[0].id", ex.Field);
    }

    [Fact]
    public void A_missing_record_array_is_rejected()
    {
        var ex = LoadFails(new ContentFile(ChecksFile, "{}"));
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$.checks", ex.Field);
    }

    [Fact]
    public void Comments_are_rejected()
    {
        var ex = LoadFails(new ContentFile(ChecksFile, "{\"checks\": [] // none yet\n}"));
        Assert.Equal(ChecksFile, ex.File);
        Assert.StartsWith("$", ex.Field, StringComparison.Ordinal);
    }

    // --- File-level errors ---

    [Fact]
    public void An_unknown_file_is_rejected()
    {
        var ex = LoadFails(new ContentFile("registry/nope.json", "{}"));
        Assert.Equal("registry/nope.json", ex.File);
        Assert.Equal("$", ex.Field);
    }

    [Fact]
    public void A_file_given_twice_is_rejected()
    {
        var ex = LoadFails(Checks(), Checks());
        Assert.Equal(ChecksFile, ex.File);
        Assert.Equal("$", ex.Field);
    }

    [Fact]
    public void An_absent_file_gives_no_records()
    {
        var set = ContentLoader.Load([Checks(Check("V19.ClassDeclared")), Reports()]);
        Assert.Empty(set.All<ArmRow>());
        Assert.Single(set.All<CheckRow>());
    }

    // --- Review Focus 2: order independence ---

    [Fact]
    public void File_order_and_line_endings_do_not_change_the_set()
    {
        ContentFile[] files =
        [
            Checks(Check("V7.NameRegistry"), Check("V19.ClassDeclared", deps: "[\"V7.NameRegistry\"]"), Check("content.MAIN65.Load")),
            Reports(Report("HealerGap", arms: "[\"Party.Solo\"]")),
            Arms(Arm("Party.Solo")),
        ];

        var forward = ContentLoader.Load(files);
        var reversed = ContentLoader.Load(files.Reverse());
        var crlf = ContentLoader.Load(files.Select(f => f with { Text = f.Text.Replace(", ", ",\r\n", StringComparison.Ordinal) }));

        string[] expected = ["V19.ClassDeclared", "V7.NameRegistry", "content.MAIN65.Load"];
        Assert.Equal(expected, forward.All<CheckRow>().Select(c => c.Id));
        Assert.Equal(expected, reversed.All<CheckRow>().Select(c => c.Id));
        Assert.Equal(expected, crlf.All<CheckRow>().Select(c => c.Id));
        Assert.Equal(forward.Files.Select(f => f.Name), reversed.Files.Select(f => f.Name));

        string[] all = [ArmsFile, ChecksFile, ReportsFile];
        Assert.Equal(forward.DigestOf(all), reversed.DigestOf(all));
        Assert.Equal(forward.DigestOf(all), crlf.DigestOf(all));
    }

    // --- Happy path ---

    [Fact]
    public void A_valid_set_loads_and_finds_by_id()
    {
        var set = ContentLoader.Load([Checks(Check("V7.NameRegistry"), Check("V19.ClassDeclared", deps: "[\"V7.NameRegistry\"]"))]);

        var check = set.Find<CheckRow>("V19.ClassDeclared");
        Assert.NotNull(check);
        Assert.Equal(ChecksFile, check.File);
        Assert.Equal("the lead", check.Owner);
        Assert.Equal<string>(["V7.NameRegistry"], check.Deps);
        Assert.Null(check.BudgetMs);
        Assert.Null(set.Find<CheckRow>("V99.Missing"));
        Assert.Null(set.Find<ArmRow>("V19.ClassDeclared"));
    }

    [Fact]
    public void A_report_reads_its_rationals_and_count()
    {
        var set = ContentLoader.Load([Arms(Arm("Party.Solo")), Reports(Report("HealerGap", arms: "[\"Party.Solo\"]"))]);

        var report = set.Find<ReportRow>("HealerGap")!;
        Assert.Equal(Rational.Of(3, 4), report.Threshold);
        Assert.Equal(Rational.Of(49, 25), report.Z);
        Assert.Equal(4300, report.N);
        Assert.Equal<string>(["Party.Solo"], report.Arms);
    }

    [Fact]
    public void The_digest_follows_the_records_of_the_named_files()
    {
        var a = ContentLoader.Load([Checks(Check("V19.ClassDeclared")), Arms(Arm("Party.Solo"))]);
        var b = ContentLoader.Load([Checks(Check("V19.ClassDeclared", owner: "\"MAIN\"")), Arms(Arm("Party.Solo"))]);

        Assert.NotEqual(a.DigestOf([ChecksFile]), b.DigestOf([ChecksFile]));
        Assert.Equal(a.DigestOf([ArmsFile]), b.DigestOf([ArmsFile]));
    }

    // --- Builders ---

    private static ContentLoadException LoadFails(params ContentFile[] files) =>
        Assert.Throws<ContentLoadException>(() => ContentLoader.Load(files));

    private static ContentFile Checks(params string[] rows) =>
        new(ChecksFile, "{\"checks\": [" + string.Join(", ", rows) + "]}");

    private static ContentFile Reports(params string[] rows) =>
        new(ReportsFile, "{\"reports\": [" + string.Join(", ", rows) + "]}");

    private static ContentFile Arms(params string[] rows) =>
        new(ArmsFile, "{\"arms\": [" + string.Join(", ", rows) + "]}");

    /// <summary>One check-row JSON object; each argument is raw JSON, and a null phase is left out.</summary>
    private static string Check(
        string id,
        string deps = "[]",
        string? phase = "\"s\"",
        string owner = "\"the lead\"",
        string? budgetMs = null,
        string extra = "")
    {
        var parts = new List<string>
        {
            $"\"id\": \"{id}\"",
            $"\"owner\": {owner}",
        };
        if (phase is not null)
        {
            parts.Add($"\"phase\": {phase}");
        }

        parts.Add($"\"deps\": {deps}");
        parts.Add("\"uses\": []");
        parts.Add("\"class\": \"build\"");
        parts.Add("\"action\": \"fail the build\"");
        parts.Add("\"wave\": \"M1\"");
        parts.Add("\"ciTier\": \"pr\"");
        if (budgetMs is not null)
        {
            parts.Add($"\"budgetMs\": {budgetMs}");
        }

        return "{" + string.Join(", ", parts) + extra + "}";
    }

    private static string Report(string id, string arms = "[]", string threshold = "\"3/4\"") =>
        "{" + string.Join(", ",
            $"\"id\": \"{id}\"",
            "\"keys\": [\"class\"]",
            "\"owner\": \"the lead\"",
            "\"definition\": \"healer gap\"",
            "\"unit\": \"percent\"",
            "\"instrument\": \"S\"",
            $"\"arms\": {arms}",
            "\"class\": \"REL\"",
            $"\"threshold\": {threshold}",
            "\"thresholdStatus\": \"illustr\"",
            "\"sided\": \"one\"",
            "\"z\": \"49/25\"",
            "\"stat\": \"mean\"",
            "\"N\": 4300",
            "\"wave\": \"M1\"",
            "\"ciTier\": \"nightly\"") + "}";

    private static string Arm(string id) =>
        $"{{\"id\": \"{id}\", \"owner\": \"the lead\", \"configDelta\": \"party=1\"}}";
}
