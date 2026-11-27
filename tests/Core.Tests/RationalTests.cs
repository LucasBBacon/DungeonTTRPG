using Dungeon.Core.Content;
using Xunit;

namespace Dungeon.Core.Tests;

public class RationalTests
{
    [Theory]
    [InlineData("3/4", 3, 4)]
    [InlineData("-3/4", -3, 4)]
    [InlineData("5", 5, 1)]
    [InlineData("0", 0, 1)]
    [InlineData("-7", -7, 1)]
    [InlineData("9223372036854775807", long.MaxValue, 1)]
    [InlineData("-9223372036854775808", long.MinValue, 1)]
    [InlineData("-9223372036854775808/3", long.MinValue, 3)]
    public void Accepted_inputs_parse_and_round_trip(string s, long p, long q)
    {
        Assert.True(Rational.TryParse(s, out var r));
        Assert.Equal(new Rational(p, q), r);
        Assert.Equal(s, r.ToString());
    }

    [Theory]
    [InlineData("6/8")]
    [InlineData("3/0")]
    [InlineData("-3/-4")]
    [InlineData("3/-4")]
    [InlineData("1/1")]
    [InlineData(" 3/4")]
    [InlineData("3/4 ")]
    [InlineData("3/4/5")]
    [InlineData("")]
    [InlineData("+3/4")]
    [InlineData("0/5")]
    [InlineData("9223372036854775808")]
    [InlineData("-9223372036854775809")]
    [InlineData("1/9223372036854775808")]
    [InlineData("-")]
    [InlineData("/4")]
    [InlineData("3/")]
    [InlineData("1.5")]
    [InlineData("٣/4")]
    [InlineData("-0")]
    [InlineData("007")]
    [InlineData("00")]
    [InlineData("03/4")]
    [InlineData("3/04")]
    [InlineData("-03/4")]
    public void Rejected_inputs_fail(string s)
    {
        Assert.False(Rational.TryParse(s, out var r));
        Assert.Equal(default, r);
    }

    [Fact]
    public void Null_is_rejected()
    {
        Assert.False(Rational.TryParse(null!, out _));
    }

    [Fact]
    public void Of_requires_canonical_form()
    {
        Assert.Equal(new Rational(1, 2), Rational.Of(1, 2));
        Assert.Equal(new Rational(0, 1), Rational.Of(0, 1));
        Assert.Equal(new Rational(long.MinValue, 1), Rational.Of(long.MinValue, 1));
        Assert.Throws<ArgumentException>(() => Rational.Of(2, 4));
        Assert.Throws<ArgumentException>(() => Rational.Of(1, 0));
        Assert.Throws<ArgumentException>(() => Rational.Of(1, -2));
        Assert.Throws<ArgumentException>(() => Rational.Of(0, 5));
        Assert.Throws<ArgumentException>(() => Rational.Of(long.MinValue, 2));
    }
    // --- Checked arithmetic (M1-05 count formulas) ---

    [Fact]
    public void Add_reduces()
    {
        Assert.Equal(Rational.Of(5, 6), Rational.Of(1, 2).Add(Rational.Of(1, 3)));
        Assert.Equal(Rational.Of(1, 1), Rational.Of(1, 2).Add(Rational.Of(1, 2)));
        Assert.Equal(Rational.Of(0, 1), Rational.Of(-3, 4).Add(Rational.Of(3, 4)));
    }

    [Fact]
    public void Multiply_reduces()
    {
        Assert.Equal(Rational.Of(21, 10), Rational.Of(3, 10).Multiply(Rational.Of(7, 1)));
        Assert.Equal(Rational.Of(3, 1), Rational.Of(3, 10).Multiply(Rational.Of(10, 1)));
        Assert.Equal(Rational.Of(0, 1), Rational.Of(0, 1).Multiply(Rational.Of(-5, 7)));
        Assert.Equal(Rational.Of(-1, 2), Rational.Of(-3, 4).Multiply(Rational.Of(2, 3)));
    }

    [Fact]
    public void Floor_rounds_toward_negative_infinity()
    {
        Assert.Equal(2L, Rational.Of(21, 10).Floor());
        Assert.Equal(3L, Rational.Of(3, 1).Floor());
        Assert.Equal(0L, Rational.Of(1, 2).Floor());
        Assert.Equal(-1L, Rational.Of(-1, 2).Floor());
    }

    [Fact]
    public void Arithmetic_overflow_throws()
    {
        Assert.Throws<OverflowException>(() => Rational.Of(long.MaxValue, 1).Add(Rational.Of(1, 1)));
        Assert.Throws<OverflowException>(() => Rational.Of(long.MaxValue, 1).Multiply(Rational.Of(2, 1)));
    }
}
