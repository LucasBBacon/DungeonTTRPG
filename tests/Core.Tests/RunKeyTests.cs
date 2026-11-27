using Dungeon.Core.Canonical;
using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The reproducibility key (runSeed, genVersion, manifestHash, configHash, unlockHash), SV 4.6.</summary>
public class RunKeyTests
{
    private static RunKey Key(ulong seed = 7, RunConfig? config = null, UnlockSnapshot? unlocks = null) =>
        RunKey.For(seed, config ?? RunConfig.M1, unlocks ?? UnlockSnapshot.None);

    [Fact]
    public void For_builds_the_tuple_in_order()
    {
        var k = Key();
        Assert.Equal(7UL, k.RunSeed);
        Assert.Equal(GenVersion.Current, k.GenVersion);
        Assert.Equal(ContentHashes.Manifest, k.ManifestHash.ToHex());
        Assert.Equal(RunConfig.M1.ConfigHash, k.ConfigHash);
        Assert.Equal(UnlockSnapshot.None.UnlockHash, k.UnlockHash);
    }

    [Fact]
    public void The_same_inputs_give_the_same_key_and_bytes()
    {
        Assert.Equal(Key(), Key());
        Assert.Equal(CanonicalWriter.Serialize(Key()), CanonicalWriter.Serialize(Key()));
        Assert.Equal(Key().Id, Key().Id);
    }

    [Fact]
    public void Each_input_moves_only_its_own_field()
    {
        var k = Key();
        var seed = Key(seed: 8);
        Assert.NotEqual(k.RunSeed, seed.RunSeed);
        Assert.Equal(k.ConfigHash, seed.ConfigHash);
        Assert.NotEqual(k.Id, seed.Id);

        var config = Key(config: RunConfig.M1 with { DifficultyTier = 1 });
        Assert.NotEqual(k.ConfigHash, config.ConfigHash);
        Assert.Equal(k.UnlockHash, config.UnlockHash);
        Assert.Equal(k.RunSeed, config.RunSeed);
        Assert.NotEqual(k.Id, config.Id);

        var unlocks = Key(unlocks: new UnlockSnapshot(["unlock.a"]));
        Assert.NotEqual(k.UnlockHash, unlocks.UnlockHash);
        Assert.Equal(k.ConfigHash, unlocks.ConfigHash);
        Assert.NotEqual(k.Id, unlocks.Id);
    }

    [Fact]
    public void A_seed_of_2_to_the_60_is_refused() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Key(seed: RunSeed.Limit));

    [Fact]
    public void An_invalid_config_is_refused() =>
        Assert.ThrowsAny<ArgumentException>(() => Key(config: RunConfig.M1 with { RouteFloorCount = 0 }));

    [Fact]
    public void V22_is_registered_at_tier_3() =>
        Assert.Equal(3, BuildTier.Of(new CheckId("V22.RunModifierIdInConfigHash")));

    [Fact]
    public void The_two_key_test_rows_are_registered()
    {
        var content = ContentLoader.Load(Dungeon.Sim.Tools.ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")));
        foreach (var id in new[] { "V19.ConfigHashCoversRunConfig", "V22.RunModifierIdInConfigHash" })
        {
            var row = content.Find<CheckRow>(id);
            Assert.NotNull(row);
            Assert.Equal(Phase.T, row!.Phase);
            Assert.Equal("M1", row.Wave);
        }
    }
}
