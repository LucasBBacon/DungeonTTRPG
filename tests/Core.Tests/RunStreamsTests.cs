using Xunit;

namespace Dungeon.Core.Tests;

public class RunStreamsTests
{
    // Golden keys: combat = Derive(7, "combat"); shrine = Derive(Derive(7, "loot"), "shrine").
    [Fact]
    public void Seed_7_derives_the_pinned_keys_at_counter_zero()
    {
        var r = RunStreams.FromSeed(7);
        Assert.Equal(new DrawStream(0xb1fbfb392345d2f0UL, 0), r.Combat);
        Assert.Equal(new DrawStream(0xcd9099bee98dba22UL, 0), r.Shrine);
    }

    [Fact]
    public void Keys_follow_the_path_grammar()
    {
        var r = RunStreams.FromSeed(12345);
        Assert.Equal(KeyAlgebra.Derive(12345, "combat"), r.Combat.Key);
        Assert.Equal(KeyAlgebra.Derive(KeyAlgebra.Derive(12345, "loot"), "shrine"), r.Shrine.Key);
    }

    [Fact]
    public void Seed_at_the_limit_is_rejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RunStreams.FromSeed(RunSeed.Limit));
        RunStreams.FromSeed(RunSeed.Limit - 1);
    }

    [Fact]
    public void Game_state_rejects_a_seed_at_the_limit() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Fixtures.ShrineState(RunSeed.Limit));

    [Fact]
    public void Game_state_keeps_its_run_seed() =>
        Assert.Equal(12345UL, Fixtures.ShrineState(12345).RunSeed);
}
