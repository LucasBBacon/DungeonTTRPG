using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Dungeon.Core.Determinism;

namespace Dungeon.Core;

/// <summary>K-rule 7: a keyed sort orders by (H value, then ordinal comparison of the stable id).</summary>
public static class KeyedSort
{
    /// <summary>Orders by <c>H(key, domain, LabelId(id))</c>, ties broken by ordinal id.</summary>
    /// <exception cref="ArgumentException">Two items share an id.</exception>
    public static ImmutableArray<T> Order<T>(IEnumerable<T> items, Func<T, string> id, ulong key, string domain)
        => Order(items, id, s => KeyAlgebra.H(key, domain, KeyAlgebra.LabelId(s)));

    /// <summary>Orders by <paramref name="hash"/> of the id, ties broken by ordinal id.</summary>
    /// <exception cref="ArgumentException">Two items share an id.</exception>
    public static ImmutableArray<T> Order<T>(IEnumerable<T> items, Func<T, string> id, Func<string, ulong> hash)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(hash);

        var entries = new List<Entry<T>>();
        foreach (var item in items)
        {
            var itemId = id(item);
            entries.Add(new Entry<T>(hash(itemId), itemId, item));
        }

        entries.Sort(new EntryComparer<T>());

        // Equal ids have equal hashes (hash is a pure function of the id), so duplicates are adjacent after the sort.
        for (var i = 1; i < entries.Count; i++)
        {
            if (string.Equals(entries[i - 1].Id, entries[i].Id, StringComparison.Ordinal))
                throw new ArgumentException($"Duplicate id \"{entries[i].Id}\": a keyed order over duplicates is not total.", nameof(items));
        }

        var result = ImmutableArray.CreateBuilder<T>(entries.Count);
        foreach (var e in entries)
            result.Add(e.Item);
        return result.MoveToImmutable();
    }

    private readonly record struct Entry<T>(ulong Hash, string Id, T Item);

    [TotalComparer]
    private sealed class EntryComparer<T> : IComparer<Entry<T>>
    {
        public int Compare(Entry<T> x, Entry<T> y)
        {
            var c = x.Hash.CompareTo(y.Hash);
            return c != 0 ? c : string.CompareOrdinal(x.Id, y.Id);
        }
    }
}
