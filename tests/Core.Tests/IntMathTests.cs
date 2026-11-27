using Xunit;

namespace Dungeon.Core.Tests;

public class IntMathTests
{
    [Theory]
    [InlineData(7, 2, 3)]
    [InlineData(-7, 2, -4)]
    [InlineData(7, -2, -4)]
    [InlineData(-7, -2, 3)]
    [InlineData(6, 3, 2)]
    [InlineData(-6, 3, -2)]
    [InlineData(0, 5, 0)]
    public void FloorDiv_int_rounds_toward_negative_infinity(int a, int b, int expected) =>
        Assert.Equal(expected, IntMath.FloorDiv(a, b));

    [Theory]
    [InlineData(7, 2, 1)]
    [InlineData(-7, 2, 1)]
    [InlineData(7, -2, -1)]
    [InlineData(-7, -2, -1)]
    [InlineData(6, 3, 0)]
    [InlineData(-6, 3, 0)]
    [InlineData(0, 5, 0)]
    public void FloorMod_int_takes_the_sign_of_the_divisor(int a, int b, int expected) =>
        Assert.Equal(expected, IntMath.FloorMod(a, b));

    [Theory]
    [InlineData(7L, 2L, 3L)]
    [InlineData(-7L, 2L, -4L)]
    [InlineData(7L, -2L, -4L)]
    [InlineData(-7L, -2L, 3L)]
    [InlineData(6L, 3L, 2L)]
    [InlineData(-6L, 3L, -2L)]
    [InlineData(0L, 5L, 0L)]
    public void FloorDiv_long_rounds_toward_negative_infinity(long a, long b, long expected) =>
        Assert.Equal(expected, IntMath.FloorDiv(a, b));

    [Theory]
    [InlineData(7L, 2L, 1L)]
    [InlineData(-7L, 2L, 1L)]
    [InlineData(7L, -2L, -1L)]
    [InlineData(-7L, -2L, -1L)]
    [InlineData(6L, 3L, 0L)]
    [InlineData(-6L, 3L, 0L)]
    [InlineData(0L, 5L, 0L)]
    public void FloorMod_long_takes_the_sign_of_the_divisor(long a, long b, long expected) =>
        Assert.Equal(expected, IntMath.FloorMod(a, b));

    [Fact]
    public void FloorDiv_of_MinValue_by_minus_one_overflows()
    {
        Assert.Throws<OverflowException>(() => IntMath.FloorDiv(int.MinValue, -1));
        Assert.Throws<OverflowException>(() => IntMath.FloorDiv(long.MinValue, -1L));
    }

    [Fact]
    public void FloorMod_of_MinValue_by_minus_one_is_zero()
    {
        Assert.Equal(0, IntMath.FloorMod(int.MinValue, -1));
        Assert.Equal(0L, IntMath.FloorMod(long.MinValue, -1L));
    }

    [Fact]
    public void Division_by_zero_throws()
    {
        Assert.Throws<DivideByZeroException>(() => IntMath.FloorDiv(1, 0));
        Assert.Throws<DivideByZeroException>(() => IntMath.FloorMod(1, 0));
        Assert.Throws<DivideByZeroException>(() => IntMath.FloorDiv(1L, 0L));
        Assert.Throws<DivideByZeroException>(() => IntMath.FloorMod(1L, 0L));
    }

    [Fact]
    public void Contract_holds_over_a_grid()
    {
        for (var a = -20; a <= 20; a++)
        {
            for (var b = -6; b <= 6; b++)
            {
                if (b == 0) continue;

                var q = IntMath.FloorDiv(a, b);
                var r = IntMath.FloorMod(a, b);
                Assert.Equal(a, q * b + r);
                Assert.InRange(Math.Abs(r), 0, Math.Abs(b) - 1);
                Assert.True(r == 0 || (r < 0) == (b < 0), $"FloorMod({a},{b})={r} has the wrong sign");

                var ql = IntMath.FloorDiv((long)a, b);
                var rl = IntMath.FloorMod((long)a, b);
                Assert.Equal(q, ql);
                Assert.Equal(r, rl);
            }
        }
    }
}
