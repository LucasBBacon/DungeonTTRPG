using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Dungeon.Core.Validation.Checks;
using Xunit;

namespace Dungeon.Core.Tests;

public class ValidationFrameworkTests
{
    private const string ChecksFile = "registry/checks.json";
    private const string ArmsFile = "registry/arms.json";

    // --- Failure classes (plan call 2) ---

    [Theory]
    [InlineData("repair", FailureKind.Repair, null)]
    [InlineData("stageRetry(traps)", FailureKind.StageRetry, "traps")]
    [InlineData("waivable(ArenaStampFallback)", FailureKind.Waivable, "ArenaStampFallback")]
    [InlineData("build", FailureKind.Build, null)]
    public void A_failure_class_parses(string text, FailureKind kind, string? argument)
    {
        Assert.True(FailureClass.TryParse(text, out var c));
        Assert.Equal(new FailureClass(kind, argument), c);
        Assert.Equal(text, c.ToString());
    }

    [Theory]
    [InlineData("stageRetry")]
    [InlineData("repair(x)")]
    [InlineData("waivable()")]
    [InlineData("Repair")]
    [InlineData("stageRetry(a b)")]
    public void A_bad_failure_class_is_rejected(string text)
    {
        Assert.False(FailureClass.TryParse(text, out _));
    }

    // --- V19.ClassDeclared ---

    [Fact]
    public void ClassDeclared_fails_a_build_phase_row_with_a_runtime_class()
    {
        var failure = Assert.Single(ClassDeclared(Row("V19.ClassDeclared", phase: "s", cls: "defect")));
        Assert.Equal(new CheckId("V19.ClassDeclared"), failure.Check);
        Assert.Equal(ChecksFile, failure.File);
        Assert.Equal("$.checks[0].class", failure.Field);
    }

    [Fact]
    public void ClassDeclared_fails_a_generation_row_with_build()
    {
        var failure = Assert.Single(ClassDeclared(Row("V19.ClassDeclared"), Row("V5.Gen", phase: "g", cls: "build")));
        Assert.Equal("$.checks[1].class", failure.Field);
    }

    [Fact]
    public void ClassDeclared_fails_a_floor_retry_row_without_a_proof()
    {
        var failure = Assert.Single(ClassDeclared(Row("V5.Gen", phase: "g", cls: "floorRetry")));
        Assert.Equal("$.checks[0].proofRef", failure.Field);
    }

    [Fact]
    public void ClassDeclared_fails_an_unparseable_class()
    {
        var failure = Assert.Single(ClassDeclared(Row("V5.Gen", phase: "r", cls: "stageRetry")));
        Assert.Equal("$.checks[0].class", failure.Field);
    }

    [Fact]
    public void ClassDeclared_passes_a_valid_mixed_set()
    {
        Assert.Empty(ClassDeclared(
            Row("V19.ClassDeclared"),
            Row("V19.Proof", phase: "t"),
            Row("V5.Gen", phase: "g", cls: "floorRetry", proofRef: "V19.Proof"),
            Row("V5.Traps", phase: "g", cls: "stageRetry(traps)"),
            Row("V8.Arena", phase: "r", cls: "waivable(ArenaStampFallback)"),
            Row("V9.Assert", phase: "r", cls: "runtimeAssert")));
    }

    // --- The runner ---

    [Fact]
    public void Checks_run_in_tier_order()
    {
        // Ordinal order alone gives V19, V24, V7, content.X.Load. Tier order (plan call 5) gives
        // V19 and content.X.Load at tier 0 (ordinal within the tier), then V7 at 2, then V24 at 3.
        var content = Load(Checks(Row("V24.Probe"), Row("V7.Probe"), Row("V19.Probe"), Row("content.X.Load")));
        var order = new List<string>();
        var checks = new IStaticCheck[]
        {
            new Probe("V24.Probe", order),
            new Probe("V7.Probe", order),
            new Probe("V19.Probe", order),
            new Probe("content.X.Load", order),
        };

        var report = ValidationRunner.Run(content, checks, new CheckCache());

        Assert.Equal(["V19.Probe", "content.X.Load", "V7.Probe", "V24.Probe"], order);
        Assert.True(report.Passed);
    }

    [Fact]
    public void Failures_are_ordinal_and_fail_the_report()
    {
        var content = Load(Checks(Row("V24.Probe"), Row("V19.Probe")));
        var checks = new IStaticCheck[]
        {
            new Probe("V24.Probe", [], ("b.json", "$.y"), ("a.json", "$.x")),
            new Probe("V19.Probe", [], ("c.json", "$.z")),
        };

        var report = ValidationRunner.Run(content, checks, new CheckCache());

        Assert.False(report.Passed);
        Assert.Equal(
            ["V19.Probe c.json", "V24.Probe a.json", "V24.Probe b.json"],
            report.Failures.Select(f => f.Check.Value + " " + f.File));
    }

    [Fact]
    public void A_check_with_no_row_throws()
    {
        var content = Load(Checks(Row("V19.Probe")));
        var ex = Assert.Throws<InvalidOperationException>(
            () => ValidationRunner.Run(content, [new Probe("V24.Probe", [])], new CheckCache()));
        Assert.Contains("V24.Probe", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_check_whose_row_is_not_phase_s_throws()
    {
        var content = Load(Checks(Row("V24.Probe", phase: "g", cls: "defect")));
        var ex = Assert.Throws<InvalidOperationException>(
            () => ValidationRunner.Run(content, [new Probe("V24.Probe", [])], new CheckCache()));
        Assert.Contains("V24.Probe", ex.Message, StringComparison.Ordinal);
    }

    // --- Review Focus 5: the read-set cache ---

    [Fact]
    public void The_cache_skips_a_check_whose_read_set_is_unchanged()
    {
        var cache = new CheckCache();
        var check = new Probe("V19.Probe", []) { Reads = [ChecksFile] };
        var content = Load(Checks(Row("V19.Probe")), Arms(Arm("Party.Solo")));

        ValidationRunner.Run(content, [check], cache);
        Assert.Equal(1, cache.Runs);
        Assert.Equal(0, cache.Hits);

        ValidationRunner.Run(content, [check], cache);
        Assert.Equal(1, cache.Runs);
        Assert.Equal(1, cache.Hits);
        Assert.Equal(1, check.Calls);
    }

    [Fact]
    public void A_change_in_the_read_set_reruns_the_check()
    {
        var cache = new CheckCache();
        var check = new Probe("V19.Probe", []) { Reads = [ChecksFile] };

        ValidationRunner.Run(Load(Checks(Row("V19.Probe"))), [check], cache);
        ValidationRunner.Run(Load(Checks(Row("V19.Probe", owner: "MAIN"))), [check], cache);

        Assert.Equal(2, cache.Runs);
        Assert.Equal(0, cache.Hits);
        Assert.Equal(2, check.Calls);
    }

    [Fact]
    public void A_change_outside_the_read_set_does_not_rerun_the_check()
    {
        var cache = new CheckCache();
        var check = new Probe("V19.Probe", []) { Reads = [ChecksFile] };

        ValidationRunner.Run(Load(Checks(Row("V19.Probe")), Arms(Arm("Party.Solo"))), [check], cache);
        ValidationRunner.Run(Load(Checks(Row("V19.Probe")), Arms(Arm("Party.Duo"))), [check], cache);

        Assert.Equal(1, cache.Runs);
        Assert.Equal(1, cache.Hits);
        Assert.Equal(1, check.Calls);
    }

    [Fact]
    public void A_read_set_naming_a_file_no_kind_reads_throws()
    {
        var check = new Probe("V19.Probe", []) { Reads = ["registry/check.json"] };

        var ex = Assert.Throws<InvalidOperationException>(
            () => ValidationRunner.Run(Load(Checks(Row("V19.Probe"))), [check], new CheckCache()));
        Assert.Contains("V19.Probe", ex.Message, StringComparison.Ordinal);
        Assert.Contains("registry/check.json", ex.Message, StringComparison.Ordinal);
        Assert.Equal(0, check.Calls);
    }

    [Fact]
    public void Every_static_check_reads_only_files_a_kind_reads()
    {
        var files = ContentKinds.All.Select(k => k.FileName).ToHashSet(StringComparer.Ordinal);
        Assert.All(StaticChecks.All, c => Assert.All(c.Reads, r => Assert.Contains(r, files)));
    }

    // --- Tiers (SV 5.3, plan call 5) ---

    [Theory]
    [InlineData("content.MAIN65.Load", 0)]
    [InlineData("content.Naming.Suite", 1)]
    [InlineData("V19.ClassDeclared", 0)]
    [InlineData("V7.NameRegistry", 2)]
    [InlineData("V1.X", 2)]
    [InlineData("V24.R1ExactEquality", 3)]
    [InlineData("V8.X", 3)]
    [InlineData("V21.X", 3)]
    [InlineData("V12.X", 4)]
    [InlineData("V16.X", 5)]
    [InlineData("V2.X", 6)]
    [InlineData("V6.X", 6)]
    public void A_check_has_its_tier(string id, int tier)
    {
        Assert.Equal(tier, BuildTier.Of(new CheckId(id)));
    }

    [Fact]
    public void An_unknown_validator_has_no_tier()
    {
        Assert.Throws<ArgumentException>(() => BuildTier.Of(new CheckId("V99.X")));
    }

    [Theory]
    [InlineData("V7.NameRegistry", "V7")]
    [InlineData("V24.GreyedReasonKeys", "V24")]
    [InlineData("content.MAIN65.Load", "content")]
    public void A_check_id_names_its_validator(string id, string validator)
    {
        Assert.True(CheckId.IsValid(id));
        Assert.Equal(validator, new CheckId(id).Validator);
    }

    [Theory]
    [InlineData("V0.X")]
    [InlineData("v7.X")]
    [InlineData("V7.1X")]
    [InlineData("content")]
    [InlineData("content..X")]
    public void A_bad_check_id_is_invalid(string id)
    {
        Assert.False(CheckId.IsValid(id));
        Assert.Throws<ArgumentException>(() => new CheckId(id));
    }

    // --- Helpers ---

    private static ImmutableArray<CheckFailure> ClassDeclared(params string[] rows) =>
        new ClassDeclaredCheck().Run(Load(Checks(rows)));

    private static ContentSet Load(params ContentFile[] files) => ContentLoader.Load(files);

    private static ContentFile Checks(params string[] rows) =>
        new(ChecksFile, "{\"checks\": [" + string.Join(", ", rows) + "]}");

    private static ContentFile Arms(params string[] rows) =>
        new(ArmsFile, "{\"arms\": [" + string.Join(", ", rows) + "]}");

    private static string Arm(string id) =>
        $"{{\"id\": \"{id}\", \"owner\": \"the lead\", \"configDelta\": \"party=1\"}}";

    private static string Row(string id, string phase = "s", string cls = "build", string? proofRef = null, string owner = "the lead") =>
        "{" + string.Join(", ",
            $"\"id\": \"{id}\"",
            $"\"owner\": \"{owner}\"",
            $"\"phase\": \"{phase}\"",
            "\"deps\": []",
            "\"uses\": []",
            $"\"class\": \"{cls}\"",
            "\"action\": \"fail the build\"",
            "\"wave\": \"M1\"",
            "\"ciTier\": \"pr\"") + (proofRef is null ? "" : $", \"proofRef\": \"{proofRef}\"") + "}";

    /// <summary>A test check that records when it runs and returns the given failures.</summary>
    private sealed class Probe(string id, List<string> order, params (string File, string Field)[] failures) : IStaticCheck
    {
        public CheckId Id { get; } = new(id);

        public ImmutableArray<string> Reads { get; init; } = [];

        public int Calls { get; private set; }

        public ImmutableArray<CheckFailure> Run(ContentSet content)
        {
            Calls++;
            order.Add(Id.Value);
            return failures.Select(f => new CheckFailure(Id, f.File, f.Field, "probe")).ToImmutableArray();
        }
    }
}
