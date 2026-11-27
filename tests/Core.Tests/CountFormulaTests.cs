using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The count-formula grammar and its exact evaluation (plan call 4, Review Focus 4).</summary>
public class CountFormulaTests
{
    private const string Terrain = "2+floor(3/10*maxFightSlots)+hzTagMax+maxNodes*(1+2*maxSockets)";

    [Fact]
    public void A_literal_evaluates_to_itself()
    {
        Assert.Equal(9UL, Parse("9").Evaluate([]));
    }

    [Fact]
    public void Multiplication_binds_tighter_than_addition()
    {
        Assert.Equal(8UL, Parse("1+2*floorCap+1").Evaluate([P("floorCap", 3)]));
    }

    [Fact]
    public void The_terrain_formula_evaluates_exactly()
    {
        // Parameters in formula order (maxFightSlots, hzTagMax, maxNodes, maxSockets) = (7, 2, 3, 16):
        // 2 + floor(21/10) + 2 + 3 * (1 + 32) = 2 + 2 + 2 + 99.
        var f = Parse(Terrain);
        ulong value = f.Evaluate([P("maxFightSlots", 7), P("hzTagMax", 2), P("maxNodes", 3), P("maxSockets", 16)]);
        Assert.Equal(105UL, value);
    }

    [Fact]
    public void Floor_rounds_an_exact_rational_down()
    {
        Assert.Equal(2UL, Parse("floor(3/10*7)").Evaluate([]));
        Assert.Equal(3UL, Parse("floor(3/10*10)").Evaluate([]));
        Assert.Equal(0UL, Parse("floor(1/2)").Evaluate([]));
    }

    [Fact]
    public void Parentheses_group()
    {
        Assert.Equal(14UL, Parse("2*(3+4)").Evaluate([]));
    }

    [Fact]
    public void Whitespace_is_ignored_and_the_source_text_is_kept()
    {
        var f = Parse(" 2 * ( 3 + 4 ) + floor ( 3 / 10 * n ) ");
        Assert.Equal(16UL, f.Evaluate([P("n", 7)]));
        Assert.Equal(" 2 * ( 3 + 4 ) + floor ( 3 / 10 * n ) ", f.ToString());
    }

    [Fact]
    public void Parameters_are_ordinal_and_distinct()
    {
        Assert.Equal(["hzTagMax", "maxFightSlots", "maxNodes", "maxSockets"], Parse(Terrain).Parameters.ToArray());
        Assert.Equal(["a", "b"], Parse("b+a*b+a").Parameters.ToArray());
        Assert.Empty(Parse("9").Parameters);
    }

    [Fact]
    public void A_missing_parameter_throws_naming_it()
    {
        var ex = Assert.Throws<ArgumentException>(() => Parse("1+2*floorCap+1").Evaluate([P("other", 1)]));
        Assert.Contains("floorCap", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_non_integer_result_throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Parse("3/10").Evaluate([]));
        Assert.Contains("3/10", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void An_owed_formula_names_its_owner_and_cannot_be_evaluated()
    {
        var f = Parse("owed:TL");
        Assert.True(f.IsOwed);
        Assert.Equal("TL", f.OwedBy);
        Assert.Empty(f.Parameters);
        Assert.Equal("owed:TL", f.ToString());
        var ex = Assert.Throws<InvalidOperationException>(() => f.Evaluate([]));
        Assert.Contains("TL", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_published_formula_is_not_owed()
    {
        var f = Parse("9");
        Assert.False(f.IsOwed);
        Assert.Null(f.OwedBy);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("1.5")]
    [InlineData("a-b")]
    [InlineData("floor(")]
    [InlineData("owed:")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("3/0")]
    [InlineData("6/8")]
    [InlineData("3/n")]
    [InlineData("1 2")]
    [InlineData("1+")]
    [InlineData("(1")]
    [InlineData("1)")]
    [InlineData("floor")]
    [InlineData("007")]
    [InlineData("2/3/4")]
    [InlineData("owed:T L")]
    public void Bad_formulas_are_rejected(string s)
    {
        Assert.False(CountFormula.TryParse(s, out _, out var error));
        Assert.False(string.IsNullOrEmpty(error));
    }

    private static CountFormula Parse(string s)
    {
        Assert.True(CountFormula.TryParse(s, out var f, out var error), error);
        return f;
    }

    private static KeyValuePair<string, ulong> P(string name, ulong value) => new(name, value);
}
