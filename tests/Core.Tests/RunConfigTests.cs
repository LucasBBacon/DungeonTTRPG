using System.Collections.Immutable;
using System.Reflection;
using Dungeon.Core.Canonical;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The run configuration snapshot, configHash and unlockHash (SV 4.6, RS 11.1, SV-12, HL-22).</summary>
public class RunConfigTests
{
    private static readonly RunConfig M1 = RunConfig.M1;

    private static readonly RunConfig TwoDoors = M1 with { RouteTwoDoorsEnabled = true };
    private static readonly RunConfig TwoFloors = M1 with { RouteFloorCount = 2 };

    // V19t ConfigHashCoversRunConfig (b) and (c): for each written field, valid configs that differ in exactly
    // that field. Set fields also get a same-size swap, so a serialiser that kept only the count would fail.
    private static readonly (string Field, RunConfig Base, RunConfig Changed)[] OneChangePerField =
    [
        (nameof(RunConfig.RouteFloorCount), M1, M1 with { RouteFloorCount = 2 }),
        (nameof(RunConfig.RouteTwoDoorsEnabled), M1, TwoDoors),
        (nameof(RunConfig.RouteLastHollowChoiceEnabled), TwoDoors, TwoDoors with { RouteLastHollowChoiceEnabled = true }),
        (nameof(RunConfig.RouteDoorCamps), M1, M1 with { RouteDoorCamps = [1] }),
        (nameof(RunConfig.RouteDoorCamps), TwoFloors with { RouteDoorCamps = [1] }, TwoFloors with { RouteDoorCamps = [2] }),
        (nameof(RunConfig.PacingTrimmedFloors), M1, M1 with { PacingTrimmedFloors = [1] }),
        (nameof(RunConfig.PacingTrimmedFloors), TwoFloors with { PacingTrimmedFloors = [1] }, TwoFloors with { PacingTrimmedFloors = [2] }),
        (nameof(RunConfig.RecruitCompanionCap), M1, M1 with { RecruitCompanionCap = 2 }),
        (nameof(RunConfig.DifficultyTier), M1, M1 with { DifficultyTier = 1 }),
        (nameof(RunConfig.Boons), M1, M1 with { Boons = ["boon.a"] }),
        (nameof(RunConfig.Boons), M1 with { Boons = ["boon.a"] }, M1 with { Boons = ["boon.b"] }),
        (nameof(RunConfig.NightmareTier), M1, M1 with { NightmareTier = 1 }),
        (nameof(RunConfig.RunModifierId), M1, M1 with { RunModifierId = "oath.a" }),
    ];

    private static object? ValueOf(RunConfig c, string property) => typeof(RunConfig).GetProperty(property)!.GetValue(c);

    // ImmutableArray compares by reference; compare elements instead.
    private static bool SameValue(object? a, object? b) => (a, b) switch
    {
        (ImmutableArray<int> x, ImmutableArray<int> y) => x.SequenceEqual(y),
        (ImmutableArray<string> x, ImmutableArray<string> y) => x.SequenceEqual(y, StringComparer.Ordinal),
        _ => Equals(a, b),
    };

    // V19t ConfigHashCoversRunConfig (a): every member of the snapshot type is written by the serialiser.
    // Every instance field must be the backing field of a written property; no non-public instance property
    // may hold state; the type has no base record; and the public properties are exactly the written ones.
    [Fact]
    public void ConfigHashCoversRunConfig_every_field_is_written()
    {
        const BindingFlags Instance = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var written = RunConfig.WrittenFields.OrderBy(n => n, StringComparer.Ordinal).ToArray();

        Assert.Equal(typeof(object), typeof(RunConfig).BaseType);
        Assert.Equal(
            written,
            typeof(RunConfig).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(p => p.Name)
                .Where(n => n != nameof(RunConfig.ConfigHash))
                .OrderBy(n => n, StringComparer.Ordinal)
                .ToArray());
        Assert.Equal(
            written.Select(n => $"<{n}>k__BackingField").OrderBy(n => n, StringComparer.Ordinal).ToArray(),
            typeof(RunConfig).GetFields(Instance).Select(f => f.Name).OrderBy(n => n, StringComparer.Ordinal).ToArray());
        Assert.Equal(
            ["EqualityContract"],
            typeof(RunConfig).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(p => p.Name)
                .ToArray());
    }

    // V19t ConfigHashCoversRunConfig (b): changing each field, and only that field, changes configHash.
    [Fact]
    public void ConfigHashCoversRunConfig_changing_each_field_changes_the_hash()
    {
        foreach (var (field, @base, changed) in OneChangePerField)
        {
            foreach (var other in RunConfig.WrittenFields)
            {
                bool same = SameValue(ValueOf(@base, other), ValueOf(changed, other));
                Assert.True(other == field ? !same : same, $"The {field} row must differ in {field} and nothing else; {other} breaks that.");
            }

            Assert.True(
                RunConfig.Validate(@base).ConfigHash != RunConfig.Validate(changed).ConfigHash,
                $"Changing {field} did not change configHash.");
        }
    }

    // V19t ConfigHashCoversRunConfig (c): the change table covers every written field.
    [Fact]
    public void ConfigHashCoversRunConfig_the_change_table_is_complete()
    {
        Assert.Equal(
            RunConfig.WrittenFields.OrderBy(n => n, StringComparer.Ordinal).ToArray(),
            OneChangePerField.Select(c => c.Field).Distinct().OrderBy(n => n, StringComparer.Ordinal).ToArray());
    }

    [Fact]
    public void Swapping_one_unlock_for_another_changes_the_hash() =>
        Assert.NotEqual(new UnlockSnapshot(["unlock.a"]).UnlockHash, new UnlockSnapshot(["unlock.b"]).UnlockHash);

    // Sets are canonical in memory too, so two runs with equal keys hold them in the same order (B2).
    [Fact]
    public void Validate_puts_sets_in_canonical_order()
    {
        var c = RunConfig.Validate((M1 with { RouteFloorCount = 3 }) with
        {
            Boons = ["boon.b", "boon.a"], RouteDoorCamps = [3, 1], PacingTrimmedFloors = [2, 1],
        });
        Assert.Equal(new[] { "boon.a", "boon.b" }, c.Boons);
        Assert.Equal(new[] { 1, 3 }, c.RouteDoorCamps);
        Assert.Equal(new[] { 1, 2 }, c.PacingTrimmedFloors);
        Assert.Equal(new[] { "a.x", "b.x" }, UnlockSnapshot.Validate(new UnlockSnapshot(["b.x", "a.x"])).Ids);
    }

    // V22t (m) RunModifierIdInConfigHash (HL-14, HL-22).
    [Fact]
    public void RunModifierIdInConfigHash()
    {
        var a = (M1 with { RunModifierId = "oath.a" }).ConfigHash;
        var b = (M1 with { RunModifierId = "oath.b" }).ConfigHash;
        Assert.NotEqual(M1.ConfigHash, a);
        Assert.NotEqual(M1.ConfigHash, b);
        Assert.NotEqual(a, b);
    }

    [Fact]
    public void Boon_order_does_not_change_the_hash() =>
        Assert.Equal(
            (M1 with { Boons = ["boon.b", "boon.a"] }).ConfigHash,
            (M1 with { Boons = ["boon.a", "boon.b"] }).ConfigHash);

    [Fact]
    public void The_M1_defaults_follow_RS_11_1_and_D_08()
    {
        Assert.Equal(1, M1.RouteFloorCount);
        Assert.False(M1.RouteTwoDoorsEnabled);
        Assert.False(M1.RouteLastHollowChoiceEnabled);
        Assert.Empty(M1.RouteDoorCamps);
        Assert.Empty(M1.PacingTrimmedFloors);
        Assert.Equal(3, M1.RecruitCompanionCap);
        Assert.Equal(0, M1.DifficultyTier);
        Assert.Empty(M1.Boons);
        Assert.Equal(0, M1.NightmareTier);
        Assert.Null(M1.RunModifierId);
        Assert.Same(M1, RunConfig.Validate(M1));
    }

    public static TheoryData<string, RunConfig> Invalid => new()
    {
        { "RouteLastHollowChoiceEnabled", M1 with { RouteLastHollowChoiceEnabled = true } },
        { "RouteFloorCount", M1 with { RouteFloorCount = 0 } },
        { "RecruitCompanionCap", M1 with { RecruitCompanionCap = -1 } },
        { "DifficultyTier", M1 with { DifficultyTier = -1 } },
        { "NightmareTier", M1 with { NightmareTier = 4 } },
        { "NightmareTier", M1 with { NightmareTier = -1 } },
        { "Boons", M1 with { Boons = ["Boon A"] } },
        { "Boons", M1 with { Boons = ["boon.a", "boon.a"] } },
        { "RunModifierId", M1 with { RunModifierId = "Oath" } },
        { "RouteDoorCamps", M1 with { RouteDoorCamps = [2] } },
        { "RouteDoorCamps", M1 with { RouteDoorCamps = [1, 1] } },
        { "PacingTrimmedFloors", M1 with { PacingTrimmedFloors = [0] } },
        { "Boons", M1 with { Boons = default } },
    };

    [Theory]
    [MemberData(nameof(Invalid))]
    public void Validate_names_the_bad_field(string field, RunConfig config)
    {
        var ex = Assert.ThrowsAny<ArgumentException>(() => RunConfig.Validate(config));
        Assert.Equal(field, ex.ParamName);
    }

    [Fact]
    public void The_empty_unlock_snapshot_hashes_an_empty_set()
    {
        var w = new CanonicalWriter();
        w.BeginRecord(1);
        w.SetById(0, Array.Empty<string>(), s => s, (e, s) => e.String(0, s));
        w.EndRecord();
        Assert.Equal(Digest.Of(w.ToArray()), UnlockSnapshot.None.UnlockHash);
        Assert.NotEqual(UnlockSnapshot.None.UnlockHash, new UnlockSnapshot(["unlock.a"]).UnlockHash);
        Assert.Equal(new UnlockSnapshot(["b.x", "a.x"]).UnlockHash, new UnlockSnapshot(["a.x", "b.x"]).UnlockHash);
    }

    [Fact]
    public void Unlock_ids_must_be_content_ids()
    {
        Assert.ThrowsAny<ArgumentException>(() => UnlockSnapshot.Validate(new UnlockSnapshot(["Bad Id"])));
        Assert.ThrowsAny<ArgumentException>(() => UnlockSnapshot.Validate(new UnlockSnapshot(["a.x", "a.x"])));
    }

    [Fact]
    public void Gen_version_is_one_and_rules_version_is_four()
    {
        Assert.Equal(1, GenVersion.Current);
        Assert.Equal(4, RulesVersion.Current);
    }
}
