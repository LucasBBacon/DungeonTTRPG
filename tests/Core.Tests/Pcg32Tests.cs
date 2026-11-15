using Xunit;

namespace Dungeon.Core.Tests;

public class Pcg32Tests
{
    [Fact]
    public void Matches_reference_pcg32_output()
    {
        var s = Pcg32.Seed(42, 54);
        uint[] expected = { 0xa15c02b7, 0x7b47f409, 0xba1d3330, 0x83d2f293, 0xbfa4784b, 0xcbed606e };
        foreach (var e in expected) { var (v, n) = Pcg32.NextUInt(s); Assert.Equal(e, v); s = n; }
    }

    [Fact]
    public void NextInt_stays_in_range()
    {
        var s = Pcg32.Seed(1, 1);
        for (int i = 0; i < 10_000; i++) { var (v, n) = Pcg32.NextInt(s, 7); Assert.InRange(v, 0, 6); s = n; }
    }

    [Fact]
    public void Roll_covers_one_to_sides()
    {
        var s = Pcg32.Seed(1, 1); var seen = new bool[21];
        for (int i = 0; i < 10_000; i++) { var (v, n) = Pcg32.Roll(s, 20); seen[v] = true; s = n; }
        Assert.False(seen[0]); Assert.All(seen.Skip(1), Assert.True);
    }

    [Theory] [InlineData(0)] [InlineData(-3)]
    public void NextInt_rejects_non_positive_bound(int bound) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Pcg32.NextInt(Pcg32.Seed(1, 1), bound));

    // An even Inc (a zeroed or corrupt state) is not a full-period generator: without the
    // guard the rejection loop in NextInt never terminates. NextUInt is listed first so a
    // regression fails fast there rather than hanging the run.
    [Theory] [InlineData(0UL, 0UL)] [InlineData(1UL << 62, 0UL)] [InlineData(5UL, 4UL)]
    public void Draws_reject_state_with_even_inc(ulong state, ulong inc)
    {
        var bad = new RngState(state, inc);
        Assert.Equal("s", Assert.Throws<ArgumentException>(() => Pcg32.NextUInt(bad)).ParamName);
        Assert.Equal("s", Assert.Throws<ArgumentException>(() => Pcg32.NextInt(bad, 7)).ParamName);
        Assert.Equal("s", Assert.Throws<ArgumentException>(() => Pcg32.Roll(bad, 20)).ParamName);
    }

    [Fact]
    public void Draws_reject_default_state()
    {
        Assert.Throws<ArgumentException>(() => Pcg32.NextUInt(default));
        Assert.Throws<ArgumentException>(() => Pcg32.NextInt(default, 7));
    }

    [Fact]
    public void Streams_are_independent()
    {
        var r = RngStreams.FromSeed(99);
        Assert.NotEqual(Pcg32.NextUInt(r.Combat).Value, Pcg32.NextUInt(r.Loot).Value);
        Assert.Equal(r, RngStreams.FromSeed(99));
    }
}
