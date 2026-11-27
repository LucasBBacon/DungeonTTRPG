using System;
using Xunit;

namespace Dungeon.Core.Tests;

public class DrawStreamTests
{
    private const ulong K = 0x0123456789ABCDEFUL;

    [Fact]
    public void Open_next_returns_first_draw_and_advances_counter()
    {
        var (value, next) = DrawStream.Open(K).Next();
        Assert.Equal(KeyAlgebra.Draw(K, 0), value);
        Assert.Equal(new DrawStream(K, 1), next);
    }

    [Fact]
    public void Three_nexts_equal_draws_zero_to_two()
    {
        var s = DrawStream.Open(K);
        for (ulong i = 0; i < 3; i++)
        {
            var (v, n) = s.Next();
            Assert.Equal(KeyAlgebra.Draw(K, i), v);
            s = n;
        }

        Assert.Equal(3UL, s.Counter);
    }

    [Fact]
    public void Skip_is_a_dummy_draw()
        => Assert.Equal(KeyAlgebra.Draw(K, 2), DrawStream.Open(K).Skip(2).Next().Value);

    [Fact]
    public void NextBelow_bounds_the_next_draw()
    {
        var (v, n) = DrawStream.Open(K).NextBelow(10);
        Assert.Equal(KeyAlgebra.Bound(KeyAlgebra.Draw(K, 0), 10), v);
        Assert.Equal(1UL, n.Counter);
    }

    [Fact]
    public void NextBelow_zero_throws()
        => Assert.Throws<ArgumentOutOfRangeException>(() => DrawStream.Open(K).NextBelow(0));

    /// <summary>The counter is explicitly checked, so it throws in every build, not only Debug and CI.</summary>
    [Fact]
    public void Counter_overflow_throws()
        => Assert.Throws<OverflowException>(() => new DrawStream(K, ulong.MaxValue).Next());

    /// <summary>Skip is how a save restores a counter, so it must not wrap either.</summary>
    [Fact]
    public void Skip_overflow_throws()
        => Assert.Throws<OverflowException>(() => new DrawStream(K, ulong.MaxValue).Skip(1));

    [Fact]
    public void Skip_zero_leaves_the_stream_unchanged()
        => Assert.Equal(new DrawStream(K, 5), new DrawStream(K, 5).Skip(0));
}
