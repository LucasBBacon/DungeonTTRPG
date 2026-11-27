using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>DUN005: culture-sensitive string operations (DS11).</summary>
public class CultureTests
{
    [Theory]
    [InlineData("s.ToUpper()")]
    [InlineData("s.ToLower()")]
    [InlineData("string.Compare(a, b)")]
    [InlineData("a.CompareTo(b)")]
    [InlineData("a.StartsWith(\"x\")")]
    [InlineData("a.EndsWith(\"x\")")]
    [InlineData("a.IndexOf(\"x\")")]
    [InlineData("string.Equals(a, b, StringComparison.CurrentCulture)")]
    [InlineData("a.StartsWith(\"x\", StringComparison.InvariantCultureIgnoreCase)")]
    [InlineData("StringComparer.InvariantCulture")]
    [InlineData("CultureInfo.CurrentCulture")]
    public void Culture_sensitive_operations_are_reported(string expression) =>
        Assert.Equal(new[] { "DUN005" }, Harness.Ids(Harness.Run(Fixture(expression))));

    [Fact]
    public void Non_constant_string_comparison_is_reported() =>
        Assert.Equal(
            new[] { "DUN005" },
            Harness.Ids(Harness.Run("""
                using System;
                class C { public static bool M(string a, string b, StringComparison c) => string.Equals(a, b, c); }
                """)));

    private static string Fixture(string expression) => $$"""
        using System;
        using System.Globalization;
        class C { public static object M(string a, string b, string s) => {{expression}}; }
        """;
}
