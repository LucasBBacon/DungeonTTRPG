using System.Collections.Immutable;
using System.Reflection;
using Dungeon.Core.Canonical;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The run configuration snapshot, configHash and unlockHash (SV 4.6, RS 11.1, SV-12, HL-22).</summary>
public class RunConfigTests
{
    private static readonly RunConfig M1 = RunConfig.M1;

    // One valid change per written field. V19t ConfigHashCoversRunConfig (b) and (c).
    private static readonly (string Field, RunConfig Changed)[] OneChangePerField =
    [
        (nameof(RunConfig.RouteFloorCount), M1 with { RouteFloorCount = 2 }),
        (nameof(RunConfig.RouteTwoDoorsEnabled), M1 with { RouteTwoDoorsEnabled = true }),
        (nameof(RunConfig.RouteLastHollowChoiceEnabled), M1 with { RouteTwoDoorsEnabled = true, RouteLastHollowChoiceEnabled = true }),
        (nameof(RunConfig.RouteDoorCamps), M1 with { RouteDoorCamps = [1] }),
        (nameof(RunConfig.PacingTrimmedFloors), M1 with { PacingTrimmedFloors = [1] }),
        (nameof(RunConfig.RecruitCompanionCap), M1 with { RecruitCompanionCap = 2 }),
        (nameof(RunConfig.DifficultyTier), M1 with { DifficultyTier = 1 }),
        (nameof(RunConfig.Boons), M1 with { Boons = ["boon.a"] }),
        (nameof(RunConfig.NightmareTier), M1 with { NightmareTier = 1 }),
        (nameof(RunConfig.RunModifierId), M1 with { RunModifierId = "oath.a" }),
    ];

    private static string[] DeclaredProperties() =>
        typeof(RunConfig)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Select(p => p.Name)
            .Where(n => n != nameof(RunConfig.ConfigHash))
            .OrderBy(n => n, StringComparer.Ordinal)
            .ToArray();

    // V19t ConfigHashCoversRunConfig (a): every field of the snapshot type is written by the serialiser.
    [Fact]
    public void ConfigHashCoversRunConfig_every_field_is_written()
    {
        Assert.Equal(DeclaredProperties(), RunConfig.WrittenFields.OrderBy(n => n, StringComparer.Ordinal).ToArray());
    }

    // V19t ConfigHashCoversRunConfig (b): changing each field changes configHash.
    [Fact]
    public void ConfigHashCoversRunConfig_changing_each_field_changes_the_hash()
    {
        foreach (var (field, changed) in OneChangePerField)
        {
            Assert.True(RunConfig.Validate(changed).ConfigHash != M1.ConfigHash, $"Changing {field} did not change configHash.");
        }
    }

    // V19t ConfigHashCoversRunConfig (c): the change table covers every written field.
    [Fact]
    public void ConfigHashCoversRunConfig_the_change_table_is_complete()
    {
        Assert.Equal(
            RunConfig.WrittenFields.OrderBy(n => n, StringComparer.Ordinal).ToArray(),
            OneChangePerField.Select(c => c.Field).OrderBy(n => n, StringComparer.Ordinal).ToArray());
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
    public void Version_constants_start_at_one()
    {
        Assert.Equal(1, GenVersion.Current);
        Assert.Equal(1, RulesVersion.Current);
    }
}
