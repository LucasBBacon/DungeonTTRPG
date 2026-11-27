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
    [InlineData("foreach (var kv in frozenDict) { }")]
    [InlineData("foreach (var x in frozenSet) { }")]
    [InlineData("_ = frozenDict.Keys.ToList();")]
    [InlineData("_ = frozenDict.Values.ToList();")]
    [InlineData("foreach (var k in frozenDict.Keys) { }")]
    [InlineData("_ = frozenSet.ToList();")]
    [InlineData("var f = list.ToFrozenDictionary(x => x, x => x); foreach (var kv in f) { }")]
    [InlineData("foreach (var kv in dictBuilder) { }")]
    [InlineData("_ = dictBuilder.Keys.Sum();")]
    [InlineData("foreach (var x in setBuilder) { }")]
    [InlineData("foreach (var x in ImmutableHashSet.CreateBuilder<int>()) { }")]
    [InlineData("foreach (var kv in readOnlyDict) { }")]
    [InlineData("foreach (var kv in idict) { }")]
    [InlineData("foreach (var x in readOnlySet) { }")]
    [InlineData("foreach (var x in iset) { }")]
    [InlineData("foreach (var k in readOnlyDict.Keys) { }")]
    [InlineData("_ = readOnlySet.ToList();")]
    [InlineData("_ = readOnlyDict.Select(kv => kv.Key);")]
    [InlineData("_ = readOnlyDict.GetEnumerator();")]
    [InlineData("iset.CopyTo(array, 0);")]
    [InlineData("Take(readOnlySet);")]
    public void Unordered_enumeration_is_reported(string statement) =>
        Assert.Equal(new[] { "DUN006" }, Harness.Ids(Harness.Run(Fixture(statement))));

    [Theory]
    [InlineData("foreach (var k in immutableDict.Keys) { }")]
    [InlineData("foreach (var v in concurrentDict.Values) { }")]
    [InlineData("foreach (var kv in readOnlyDict) { }")]
    [InlineData("ICollection<int> c = set; c.CopyTo(array, 0);")]
    [InlineData("foreach (var kv in dict) { }")]
    [InlineData("foreach (var k in frozenDict.Keys) { }")]
    [InlineData("foreach (var x in setBuilder) { }")]
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
            foreach (var kv in sorted) { count += kv.Value; }
            readOnlyDict.TryGetValue(8, out var r);
            has = readOnlyDict.ContainsKey(9) && readOnlySet.Contains(10) && iset.Contains(11) && idict.ContainsKey(12);
            count += readOnlyDict[13] + readOnlyDict.Count + readOnlySet.Count + readOnlyDict.GetValueOrDefault(14) + r;
            has = has && frozenDict.TryGetValue(15, out _) && frozenSet.Contains(16) && dictBuilder.ContainsKey(17) && setBuilder.Contains(18);
            count += frozenDict[19] + frozenDict.Count + dictBuilder.Count;
            _ = (has, count);
            """));
        Assert.Empty(d);
    }

    [Fact]
    public void Enumerating_a_deserialized_interface_dictionary_is_reported()
    {
        var d = Harness.Run("""
            using System.Collections.Generic;
            using System.Text.Json;
            public sealed record MonsterDef(int Hp);
            public sealed record ContentFile(IReadOnlyDictionary<string, MonsterDef> Monsters);
            public static class Loader
            {
                public static int Total(string json)
                {
                    var c = JsonSerializer.Deserialize<ContentFile>(json)!;
                    int s = 0;
                    foreach (var kv in c.Monsters) s += kv.Value.Hp;
                    return s;
                }
            }
            """);
        Assert.Equal(new[] { "DUN006" }, Harness.Ids(d));
    }

    [Fact]
    public void Read_only_set_to_list_is_reported() =>
        Assert.Equal(
            new[] { "DUN006" },
            Harness.Ids(Harness.Run("""
                using System.Collections.Generic;
                using System.Linq;
                class C { public static List<int> M(IReadOnlySet<int> s) => s.ToList(); }
                """)));

    private static string Fixture(string statements) => $$"""
        using System.Collections.Concurrent;
        using System.Collections.Frozen;
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
                int[] array,
                FrozenDictionary<int, int> frozenDict,
                FrozenSet<int> frozenSet,
                ImmutableDictionary<int, int>.Builder dictBuilder,
                ImmutableHashSet<int>.Builder setBuilder,
                IReadOnlyDictionary<int, int> readOnlyDict,
                IDictionary<int, int> idict,
                IReadOnlySet<int> readOnlySet,
                ISet<int> iset,
                SortedDictionary<int, int> sorted)
            {
                {{statements}}
            }

            private static void Take(IEnumerable<int> items) { }
        }
        """;
}
