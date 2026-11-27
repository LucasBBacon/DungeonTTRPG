using System;
using System.Linq;
using Xunit;

namespace Dungeon.Core.Tests;

public class KeyedSortTests
{
    private static string Id(string s) => s;

    [Fact]
    public void Keyed_sort_tie_breaks_by_ordinal_id()
        => Assert.Equal(new[] { "a.x", "a.y" }, KeyedSort.Order(new[] { "a.y", "a.x" }, Id, _ => 42UL));

    [Fact]
    public void Equal_hashes_sort_uppercase_before_lowercase()
        => Assert.Equal(new[] { "B", "a" }, KeyedSort.Order(new[] { "a", "B" }, Id, _ => 42UL));

    [Fact]
    public void Duplicate_ids_throw_naming_the_id()
    {
        var ex = Assert.Throws<ArgumentException>(() => KeyedSort.Order(new[] { "a", "b", "a" }, Id, _ => 1UL));
        Assert.Contains("\"a\"", ex.Message);
    }

    [Fact]
    public void Real_hash_overload_orders_by_H_ascending()
    {
        const ulong key = 12345;
        var ids = new[] { "alpha", "beta", "gamma", "delta", "eps.1", "eps.2" };
        var expected = ids
            .OrderBy(i => KeyAlgebra.H(key, "dom", KeyAlgebra.LabelId(i)))
            .ToArray();
        Assert.Equal(expected, KeyedSort.Order(ids, Id, key, "dom"));
    }

    [Fact]
    public void Input_order_does_not_change_output()
    {
        var ids = new[] { "alpha", "beta", "gamma", "delta", "eps" };
        var forward = KeyedSort.Order(ids, Id, 7UL, "dom");
        var reversed = KeyedSort.Order(ids.Reverse(), Id, 7UL, "dom");
        Assert.Equal(forward.ToArray(), reversed.ToArray());
    }

    [Fact]
    public void Empty_input_gives_empty()
        => Assert.Empty(KeyedSort.Order(Array.Empty<string>(), Id, 1UL, "dom"));
}
