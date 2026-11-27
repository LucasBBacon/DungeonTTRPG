using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>DUN006: enumeration of unordered collections (DS11). Lookups stay legal; only iteration order is banned.</summary>
public class UnorderedEnumerationTests
{
    [Theory]
    [InlineData("foreach (var kv in dict) { }")]
    [InlineData("foreach (var k in dict.Keys) { }")]
    [InlineData("foreach (var v in dict.Values) { }")]
    [InlineData("foreach (var x in set) { }")]
    [InlineData("_ = set.ToList();")]
    [InlineData("_ = set.First();")]
    [InlineData("_ = dict.Select(kv => kv.Key);")]
    [InlineData("Take(set);")]
    [InlineData("set.CopyTo(array);")]
    [InlineData("_ = dict.GetEnumerator();")]
    [InlineData("int[] spread = [.. set];")]
    [InlineData("foreach (var kv in immutableDict) { }")]
    [InlineData("foreach (var x in immutableSet) { }")]
    public void Unordered_enumeration_is_reported(string statement) =>
        Assert.Equal(new[] { "DUN006" }, Harness.Ids(Harness.Run(Fixture(statement))));

    [Fact]
    public void Lookups_and_ordered_enumeration_are_clean()
    {
        var d = Harness.Run(Fixture("""
            dict.TryGetValue(1, out var found);
            dict[2] = found;
            bool has = dict.ContainsKey(3) && set.Contains(4);
            int count = set.Count;
            foreach (var a in immutableArray) { count += a; }
            foreach (var l in list) { count += l; }
            _ = (has, count);
            """));
        Assert.Empty(d);
    }

    private static string Fixture(string statements) => $$"""
        using System.Collections.Generic;
        using System.Collections.Immutable;
        using System.Linq;
        class C
        {
            public static void M(
                Dictionary<int, int> dict,
                HashSet<int> set,
                ImmutableDictionary<int, int> immutableDict,
                ImmutableHashSet<int> immutableSet,
                ImmutableArray<int> immutableArray,
                List<int> list,
                int[] array)
            {
                {{statements}}
            }

            private static void Take(IEnumerable<int> items) { }
        }
        """;
}
