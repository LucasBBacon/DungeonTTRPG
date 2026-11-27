using Dungeon.Core.Content;
using Dungeon.Sim.Tools;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// <see cref="StageRun"/> and the DrawLedger (SV 4.2, DS15, D-08): every count class at close, inert rows, vectors,
/// owed formulas and misuse (Review Focus 3).
/// </summary>
public class DrawLedgerTests
{
    private const string StreamsFile = "registry/streams.json";
    private const string FixtureStage = "GS.fixture";
    private const ulong Rs = 0x0123456789ABCDEFUL;

    private static readonly KeyValuePair<string, ulong>[] NoParameters = [];

    private static readonly Lazy<ContentSet> Repository = new(() =>
        ContentLoader.Load(ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content"))));

    // --- Done-when 2: bossCard is inert in M1 ---

    [Fact]
    public void Inert_boss_card_drawn_once_mismatches()
    {
        var run = new StageRun(Repository.Value, "GS10", NoParameters);
        var stream = run.Open(Streams.Loot(Rs).LootPlan(1).BossCard(0).Handle);
        stream.Next();

        var ex = Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Equal("loot/lootPlan/{N}/bossCard/{entryIndex}", ex.Path);
        Assert.Equal(0UL, ex.Expected);
        Assert.Equal(1UL, ex.Actual);
        Assert.Equal("DrawLedgerMismatch{loot/lootPlan/{N}/bossCard/{entryIndex}, 0, 1}", ex.Message);
    }

    [Fact]
    public void Inert_boss_card_not_drawn_closes()
    {
        var run = new StageRun(Repository.Value, "GS10", NoParameters);
        run.Open(Streams.Loot(Rs).LootPlan(1).BossCard(0).Handle);
        run.Open(Streams.Loot(Rs).LootPlan(1).BossCard(1).Handle);

        run.Close();
    }

    // --- Done-when 3: a fixed row ---

    [Fact]
    public void Fixed_row_drawn_its_count_closes()
    {
        var (run, stream) = OpenFixed();
        stream.Next();
        stream.Next();
        stream.Next();

        Assert.Equal(3UL, stream.Counter);
        run.Close();
    }

    [Theory]
    [InlineData(2UL)]
    [InlineData(4UL)]
    public void Fixed_row_drawn_otherwise_mismatches(ulong draws)
    {
        var (run, stream) = OpenFixed();
        for (ulong i = 0; i < draws; i++)
        {
            stream.NextBelow(6);
        }

        var ex = Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Equal("f", ex.Path);
        Assert.Equal(3UL, ex.Expected);
        Assert.Equal(draws, ex.Actual);
    }

    [Fact]
    public void Skip_counts_as_draws()
    {
        var (run, stream) = OpenFixed();
        stream.Skip(3);

        Assert.Equal(3UL, stream.Counter);
        run.Close();
    }

    [Fact]
    public void Draws_are_the_key_algebra_draws_from_counter_0()
    {
        var (_, stream) = OpenFixed(key: 42);

        Assert.Equal(KeyAlgebra.Draw(42, 0), stream.Next());
        stream.Skip(1);
        Assert.Equal(KeyAlgebra.Bound(KeyAlgebra.Draw(42, 2), 6), stream.NextBelow(6));
        Assert.Equal(3UL, stream.Counter);
    }

    // --- Formulas ---

    [Fact]
    public void Fixed_per_data_formula_reads_the_stage_parameters()
    {
        var set = Load(Row("p", cls: "fixed-per-data", count: "1+2*floorCap+1"));

        var good = new StageRun(set, FixtureStage, [new("floorCap", 2)]);
        good.Open(new StreamHandle("p", 1)).Skip(6);
        good.Close();

        var short1 = new StageRun(set, FixtureStage, [new("floorCap", 2)]);
        short1.Open(new StreamHandle("p", 1)).Skip(5);
        var ex = Assert.Throws<DrawLedgerMismatchException>(short1.Close);
        Assert.Equal(6UL, ex.Expected);
        Assert.Equal(5UL, ex.Actual);
    }

    [Fact]
    public void Missing_parameter_throws_naming_it()
    {
        var set = Load(Row("p", cls: "fixed-per-data", count: "1+2*floorCap+1"));
        var run = new StageRun(set, FixtureStage, NoParameters);
        run.Open(new StreamHandle("p", 1));

        var ex = Assert.Throws<ArgumentException>(run.Close);

        Assert.Contains("floorCap", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Leaf_formula_row_expects_its_formula()
    {
        var set = Load(Row("l", cls: "leaf-formula", count: "choice+1"));
        var run = new StageRun(set, FixtureStage, [new("choice", 1)]);
        run.Open(new StreamHandle("l", 1)).Next();

        var ex = Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Equal(2UL, ex.Expected);
        Assert.Equal(1UL, ex.Actual);
    }

    // --- Owed ---

    [Fact]
    public void Owed_traps_formula_throws_naming_the_owner()
    {
        var run = new StageRun(Repository.Value, "GS7", NoParameters);
        run.Open(Streams.Dungeon(Rs).Floor(1).Attempt(0).Traps.Handle);

        var ex = Assert.Throws<DrawLedgerOwedException>(run.Close);

        Assert.Equal("dungeon/floor/{N}/attempt/{a}/traps", ex.Path);
        Assert.Equal("TL", ex.Owner);
    }

    [Fact]
    public void Owed_cap_throws_naming_the_owner()
    {
        var set = Load(Row("e/{entry}", label: "e", indices: Idx("entry"), cls: "per-entry", count: "1", extra: "\"cap\": \"owed:LE\""));
        var run = new StageRun(set, FixtureStage, NoParameters);
        run.Open(new StreamHandle("e/{entry}", 1)).Next();

        var ex = Assert.Throws<DrawLedgerOwedException>(run.Close);

        Assert.Equal("e/{entry}", ex.Path);
        Assert.Equal("LE", ex.Owner);
    }

    // --- Vectors ---

    [Fact]
    public void Vector_reads_below_its_length()
    {
        var run = new StageRun(Repository.Value, "GS0.4", NoParameters);
        var handle = Streams.Loot(Rs).Run.Handle;

        Assert.Equal(KeyAlgebra.Draw(handle.Key, 2), run.ReadVector(handle, 2));
        Assert.Throws<ArgumentOutOfRangeException>(() => run.ReadVector(handle, 3));
        run.Close();
    }

    [Fact]
    public void Vector_read_on_another_class_throws()
    {
        var (run, _) = OpenFixed();

        Assert.Throws<InvalidOperationException>(() => run.ReadVector(new StreamHandle("f", 1), 0));
    }

    [Fact]
    public void Vector_is_read_not_opened()
    {
        var run = new StageRun(Repository.Value, "GS0.4", NoParameters);

        Assert.Throws<InvalidOperationException>(() => run.Open(Streams.Loot(Rs).Run.Handle));
    }

    // --- Node ---

    [Fact]
    public void Node_drawn_mismatches()
    {
        var set = Load(Row("n"));
        var run = new StageRun(set, FixtureStage, NoParameters);
        run.Open(new StreamHandle("n", 1)).Next();

        var ex = Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Equal("n", ex.Path);
        Assert.Equal(0UL, ex.Expected);
        Assert.Equal(1UL, ex.Actual);
    }

    // --- Misuse ---

    [Fact]
    public void Opening_the_same_handle_twice_throws()
    {
        var (run, _) = OpenFixed();

        Assert.Throws<InvalidOperationException>(() => run.Open(new StreamHandle("f", 1)));
    }

    [Fact]
    public void Opening_a_row_of_another_stage_throws()
    {
        var run = new StageRun(Repository.Value, "GS7", NoParameters);

        Assert.Throws<InvalidOperationException>(() => run.Open(Streams.Loot(Rs).LootPlan(1).BossCard(0).Handle));
        Assert.Throws<InvalidOperationException>(() => run.ReadVector(Streams.Loot(Rs).Run.Handle, 0));
    }

    [Fact]
    public void Opening_an_unknown_row_throws()
    {
        var (run, _) = OpenFixed();

        Assert.Throws<InvalidOperationException>(() => run.Open(new StreamHandle("nope", 1)));
    }

    [Fact]
    public void Opening_combat_throws()
    {
        var run = new StageRun(Repository.Value, "play", NoParameters);

        Assert.Throws<InvalidOperationException>(() => run.Open(Streams.Combat(Rs).Handle));
    }

    [Theory]
    [InlineData("keyed", "core")]
    [InlineData("display", "display")]
    [InlineData("sequential-saved", "core")]
    [InlineData("sim", "sim")]
    public void Opening_an_unledgered_class_throws(string cls, string keyType)
    {
        var set = Load(Row("x", cls: cls, keyType: keyType));
        var run = new StageRun(set, FixtureStage, NoParameters);

        Assert.Throws<InvalidOperationException>(() => run.Open(new StreamHandle("x", 1)));
    }

    [Fact]
    public void Use_after_close_throws()
    {
        var (run, stream) = OpenFixed();
        stream.Skip(3);
        run.Close();

        Assert.Throws<InvalidOperationException>(() => stream.Next());
        Assert.Throws<InvalidOperationException>(() => stream.NextBelow(2));
        Assert.Throws<InvalidOperationException>(() => stream.Skip(1));
        Assert.Throws<InvalidOperationException>(() => stream.Counter);
        Assert.Throws<InvalidOperationException>(() => run.Open(new StreamHandle("f", 2)));
        Assert.Throws<InvalidOperationException>(run.Close);
    }

    [Fact]
    public void Closing_after_a_failed_close_throws()
    {
        var (run, _) = OpenFixed();
        Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Throws<InvalidOperationException>(run.Close);
    }

    // --- Per-entry ---

    [Fact]
    public void Per_entry_units_within_the_cap_close()
    {
        var (run, a, b) = OpenTwoEntries();
        a.Skip(9);
        b.Skip(9);

        run.Close();
    }

    [Fact]
    public void Per_entry_units_over_the_cap_mismatch()
    {
        var (run, a, b) = OpenTwoEntries();
        var c = run.Open(new StreamHandle("e/{entry}", 3));
        a.Skip(9);
        b.Skip(9);
        c.Skip(9);

        var ex = Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Equal("e/{entry}", ex.Path);
        Assert.Equal(2UL, ex.Expected);
        Assert.Equal(3UL, ex.Actual);
    }

    [Fact]
    public void Per_entry_unit_short_of_its_count_mismatches()
    {
        var (run, a, b) = OpenTwoEntries();
        a.Skip(9);
        b.Skip(8);

        var ex = Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Equal("e/{entry}", ex.Path);
        Assert.Equal(9UL, ex.Expected);
        Assert.Equal(8UL, ex.Actual);
    }

    [Fact]
    public void Rows_are_checked_in_ordinal_row_id_order()
    {
        var set = Load(Row("b", cls: "fixed", count: "1"), Row("a", cls: "fixed", count: "2"));
        var run = new StageRun(set, FixtureStage, NoParameters);
        run.Open(new StreamHandle("b", 1));
        run.Open(new StreamHandle("a", 2));

        var ex = Assert.Throws<DrawLedgerMismatchException>(run.Close);

        Assert.Equal("a", ex.Path);
    }

    // --- Helpers ---

    private static (StageRun Run, LedgeredStream Stream) OpenFixed(ulong key = 1)
    {
        var run = new StageRun(Load(Row("f", cls: "fixed", count: "3")), FixtureStage, NoParameters);
        return (run, run.Open(new StreamHandle("f", key)));
    }

    private static (StageRun Run, LedgeredStream A, LedgeredStream B) OpenTwoEntries()
    {
        var set = Load(Row("e/{entry}", label: "e", indices: Idx("entry"), cls: "per-entry", count: "9", extra: "\"cap\": \"2\""));
        var run = new StageRun(set, FixtureStage, NoParameters);
        return (run, run.Open(new StreamHandle("e/{entry}", 1)), run.Open(new StreamHandle("e/{entry}", 2)));
    }

    private static ContentSet Load(params string[] rows) =>
        ContentLoader.Load([new ContentFile(StreamsFile, "{\"streams\": [" + string.Join(", ", rows) + "]}")]);

    private static string Idx(string name) => $"[{{\"name\": \"{name}\", \"type\": \"ulong\"}}]";

    /// <summary>One stream-row JSON object at <see cref="FixtureStage"/>, wave M1; <paramref name="extra"/> is raw JSON appended.</summary>
    private static string Row(
        string path,
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
            "\"parent\": null",
            $"\"label\": \"{label ?? path}\"",
            $"\"indices\": {indices}",
            $"\"class\": \"{cls}\"",
            $"\"stage\": \"{FixtureStage}\"",
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
}
