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
    [InlineData("foreach (var k in immutableDict.Keys) { }")]
    [InlineData("_ = immutableDict.Values.ToList();")]
    [InlineData("Take(immutableDict.Keys);")]
    [InlineData("foreach (var kv in concurrentDict) { }")]
    [InlineData("foreach (var k in concurrentDict.Keys) { }")]
    [InlineData("foreach (var v in concurrentDict.Values) { }")]
    [InlineData("IReadOnlyDictionary<int, int> r = dict; foreach (var kv in r) { }")]
    [InlineData("ICollection<int> c = set; c.CopyTo(array, 0);")]
    [InlineData("ISet<int> s = set; _ = s;")]
    [InlineData("IReadOnlyCollection<int> rc = set; _ = rc;")]
    [InlineData("IDictionary<int, int> id = dict; _ = id;")]
    [InlineData("ICollection<int> keys = dict.Keys; _ = keys;")]
    public void Unordered_enumeration_is_reported(string statement) =>
        Assert.Equal(new[] { "DUN006" }, Harness.Ids(Harness.Run(Fixture(statement))));

    [Theory]
    [InlineData("foreach (var k in immutableDict.Keys) { }")]
    [InlineData("foreach (var v in concurrentDict.Values) { }")]
    [InlineData("IReadOnlyDictionary<int, int> r = dict; foreach (var kv in r) { }")]
    [InlineData("ICollection<int> c = set; c.CopyTo(array, 0);")]
    [InlineData("foreach (var kv in dict) { }")]
    public void Each_construct_is_reported_once(string statement) =>
        Assert.Single(Harness.Run(Fixture(statement)));

    [Fact]
    public void Property_exposing_a_dictionary_as_an_interface_is_reported()
    {
        var d = Harness.Run("""
            using System.Collections.Generic;
            class Store
            {
                private readonly Dictionary<int, int> _items = new();
                public IReadOnlyDictionary<int, int> Items => _items;
            }
            """);
        Assert.Equal(new[] { "DUN006" }, Harness.Ids(d));
    }

    [Fact]
    public void Lookups_and_ordered_enumeration_are_clean()
    {
        var d = Harness.Run(Fixture("""
            dict.TryGetValue(1, out var found);
            dict[2] = found;
            bool has = dict.ContainsKey(3) && set.Contains(4);
            int count = set.Count + dict.GetValueOrDefault(5) + concurrentDict.Count;
            _ = immutableDict.ContainsKey(6) && concurrentDict.TryGetValue(7, out _);
            foreach (var a in immutableArray) { count += a; }
            foreach (var l in list) { count += l; }
            _ = (has, count);
            """));
        Assert.Empty(d);
    }

    private static string Fixture(string statements) => $$"""
        using System.Collections.Concurrent;
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
                ConcurrentDictionary<int, int> concurrentDict,
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
