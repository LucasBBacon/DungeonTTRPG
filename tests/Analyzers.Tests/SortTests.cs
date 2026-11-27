using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>DUN007: sorts without a registered total comparer (DS11).</summary>
public class SortTests
{
    [Theory]
    [InlineData("list.Sort()")]
    [InlineData("list.Sort((x, y) => x - y)")]
    [InlineData("Array.Sort(arr)")]
    [InlineData("arr.AsSpan().Sort()")]
    [InlineData("immutable.Sort()")]
    [InlineData("names.OrderBy(n => n)")]
    [InlineData("items.OrderBy(i => i.Name)")]
    [InlineData("new SortedSet<string>()")]
    [InlineData("list.Sort(new UnmarkedComparer())")]
    public void Unregistered_sorts_are_reported(string statement) =>
        Assert.Equal(new[] { "DUN007" }, Harness.Ids(Harness.Run(Fixture(statement))));

    private static string Fixture(string statement) => $$"""
        using System;
        using System.Collections.Generic;
        using System.Collections.Immutable;
        using System.Linq;
        public sealed class Item { public string Name { get; init; } = ""; }
        public sealed class UnmarkedComparer : IComparer<int>
        {
            public int Compare(int x, int y) => x < y ? -1 : x > y ? 1 : 0;
        }
        class C
        {
            public static void M(List<int> list, int[] arr, ImmutableArray<int> immutable, List<string> names, List<Item> items)
            {
                {{statement}};
            }
        }
        """;
}
