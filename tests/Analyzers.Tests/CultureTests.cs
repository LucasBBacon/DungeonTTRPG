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
    [InlineData("a.ToUpper(CultureInfo.GetCultureInfo(\"tr-TR\"))")]
    [InlineData("Comparer<string>.Default")]
    [InlineData("Comparer<string>.Default.Compare(a, b)")]
    [InlineData("Comparer<(string, int)>.Default")]
    [InlineData("(a, 1).CompareTo((b, 2))")]
    [InlineData("((IComparable<string>)a).CompareTo(b)")]
    [InlineData("((IComparable)a).CompareTo(b)")]
    [InlineData("((IComparable)Tuple.Create(a, 1)).CompareTo(Tuple.Create(b, 2))")]
    [InlineData("Cmp(a, b)")]
    [InlineData("l.Min()")]
    [InlineData("l.Max()")]
    [InlineData("l.Min(x => x)")]
    [InlineData("l.MinBy(x => x)")]
    [InlineData("l.MaxBy(x => x)")]
    [InlineData("l.BinarySearch(a)")]
    [InlineData("Array.BinarySearch(arr, a)")]
    [InlineData("Array.BinarySearch((Array)arr, a)")]
    public void Culture_sensitive_operations_are_reported(string expression) =>
        Assert.Equal(new[] { "DUN005" }, Harness.Ids(Harness.Run(Fixture(expression))));

    [Theory]
    [InlineData("a.ToUpper(CultureInfo.InvariantCulture)")]
    [InlineData("a.ToLower(CultureInfo.InvariantCulture)")]
    [InlineData("char.ToUpper('x', CultureInfo.InvariantCulture)")]
    [InlineData("(1, 2).CompareTo((3, 4))")]
    [InlineData("Comparer<int>.Default.Compare(1, 2)")]
    [InlineData("Cmp(1, 2)")]
    [InlineData("l.Min(StringComparer.Ordinal)")]
    [InlineData("l.MaxBy(x => x, StringComparer.Ordinal)")]
    [InlineData("l.Max(x => x.Length)")]
    [InlineData("l.BinarySearch(a, StringComparer.Ordinal)")]
    [InlineData("Array.BinarySearch(arr, a, StringComparer.Ordinal)")]
    [InlineData("new List<int>().Min()")]
    public void Culture_neutral_operations_are_clean(string expression) =>
        Assert.Empty(Harness.Run(Fixture(expression)));

    [Fact]
    public void Culture_ordered_total_comparers_are_reported()
    {
        var d = Harness.Run("""
            using System.Collections.Generic;
            using Dungeon.Core.Determinism;
            public sealed class Room { public string Id { get; init; } = ""; public int Rank { get; init; } }
            [TotalComparer]
            public sealed class RoomCmp : IComparer<Room>
            {
                public int Compare(Room? a, Room? b) => (a!.Id, a.Rank).CompareTo((b!.Id, b.Rank));
            }
            [TotalComparer]
            public sealed class IdCmp : IComparer<string>
            {
                public int Compare(string? a, string? b) => Comparer<string>.Default.Compare(a, b);
            }
            """);
        Assert.Equal(new[] { "DUN005" }, Harness.Ids(d));
        Assert.Equal(2, d.Length);
    }

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
        using System.Collections.Generic;
        using System.Globalization;
        using System.Linq;
        class C
        {
            public static object M(string a, string b, string s, List<string> l, string[] arr) => {{expression}};

            private static int Cmp<T>(T x, T y) where T : IComparable<T> => x.CompareTo(y);
        }
        """;
}
