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
    [InlineData("SortedSet<string> s = []")]
    [InlineData("SortedSet<string> s = [\"b\", \"a\"]")]
    [InlineData("SortedDictionary<string, int> d = []")]
    [InlineData("list.Sort(new UnmarkedComparer())")]
    [InlineData("builder.Sort()")]
    [InlineData("immutableList.Sort()")]
    [InlineData("_ = immutableList.Sort(new UnmarkedStringComparer())")]
    [InlineData("listBuilder.Sort()")]
    [InlineData("_ = ImmutableSortedDictionary<string, int>.Empty.Add(\"b\", 1)")]
    [InlineData("_ = ImmutableSortedSet<string>.Empty")]
    [InlineData("_ = ImmutableSortedDictionary.Create<string, int>()")]
    [InlineData("_ = ImmutableSortedDictionary.CreateBuilder<string, int>()")]
    [InlineData("_ = ImmutableSortedSet.Create<string>()")]
    [InlineData("_ = ImmutableSortedSet.Create(\"b\", \"a\")")]
    [InlineData("_ = ImmutableSortedSet.CreateRange(names)")]
    [InlineData("_ = ImmutableSortedSet.CreateBuilder<string>()")]
    [InlineData("_ = ImmutableSortedSet.Create<string>(new UnmarkedStringComparer())")]
    [InlineData("_ = names.ToImmutableSortedDictionary(n => n, n => 1)")]
    [InlineData("_ = names.ToImmutableSortedSet()")]
    [InlineData("ImmutableSortedSet<string> s = [\"b\", \"a\"]")]
    public void Unregistered_sorts_are_reported(string statement) =>
        Assert.Equal(new[] { "DUN007" }, Harness.Ids(Harness.Run(Fixture(statement))));

    [Theory]
    [InlineData("builder.Sort(StringComparer.Ordinal)")]
    [InlineData("_ = immutableList.Sort(StringComparer.Ordinal)")]
    [InlineData("listBuilder.Sort(StringComparer.Ordinal)")]
    [InlineData("_ = ImmutableSortedDictionary.Create<string, int>(StringComparer.Ordinal)")]
    [InlineData("_ = ImmutableSortedDictionary.CreateBuilder<string, int>(StringComparer.Ordinal)")]
    [InlineData("_ = ImmutableSortedSet.Create<string>(StringComparer.Ordinal)")]
    [InlineData("_ = ImmutableSortedSet.CreateRange(StringComparer.Ordinal, names)")]
    [InlineData("_ = ImmutableSortedSet.CreateBuilder<string>(StringComparer.Ordinal)")]
    [InlineData("_ = names.ToImmutableSortedDictionary(n => n, n => 1, StringComparer.Ordinal)")]
    [InlineData("_ = names.ToImmutableSortedSet(StringComparer.Ordinal)")]
    public void Registered_immutable_sorts_are_clean(string statement) =>
        Assert.Empty(Harness.Run(Fixture(statement)));

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
        public sealed class UnmarkedStringComparer : IComparer<string>
        {
            public int Compare(string? x, string? y) => string.CompareOrdinal(x, y);
        }
        class C
        {
            public static void M(List<int> list, int[] arr, ImmutableArray<int> immutable, List<string> names, List<Item> items,
                ImmutableArray<string>.Builder builder, ImmutableList<string> immutableList, ImmutableList<string>.Builder listBuilder)
            {
                {{statement}};
            }
        }
        """;
}
