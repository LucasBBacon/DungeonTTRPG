using System.Collections.Generic;
using System.Collections.Immutable;
using Dungeon.Core.Canonical;

namespace Dungeon.Core.Validation;

/// <summary>
/// Check results cached by content hash keyed by each check's declared read set (SV 5.3). In memory,
/// for one process (plan call 4). It holds each check's latest result, ordinal by id, with the
/// read-set digest it was computed over; a lookup hits only when that digest matches.
/// </summary>
public sealed class CheckCache
{
    private readonly List<Entry> _entries = new();

    /// <summary>How many lookups found a cached result.</summary>
    public int Hits { get; private set; }

    /// <summary>How many results were stored, one per check actually run.</summary>
    public int Runs { get; private set; }

    /// <summary>The cached result of <paramref name="id"/> over the read-set digest <paramref name="readSet"/>, if any.</summary>
    public bool TryGet(CheckId id, Digest readSet, out ImmutableArray<CheckFailure> failures)
    {
        int at = IndexOf(id);
        if (at >= 0 && _entries[at].ReadSet == readSet)
        {
            failures = _entries[at].Failures;
            Hits++;
            return true;
        }

        failures = default;
        return false;
    }

    /// <summary>Stores the result of running <paramref name="id"/> over the read-set digest <paramref name="readSet"/>, replacing any older one.</summary>
    public void Put(CheckId id, Digest readSet, ImmutableArray<CheckFailure> failures)
    {
        var entry = new Entry(id, readSet, failures);
        int at = IndexOf(id);
        if (at >= 0)
        {
            _entries[at] = entry;
        }
        else
        {
            _entries.Insert(~at, entry);
        }

        Runs++;
    }

    // Binary search by ordinal id: the index if present, else the complement of the insertion point.
    private int IndexOf(CheckId id)
    {
        int lo = 0;
        int hi = _entries.Count - 1;
        while (lo <= hi)
        {
            int mid = lo + ((hi - lo) >> 1);
            int cmp = string.CompareOrdinal(_entries[mid].Id.Value, id.Value);
            if (cmp == 0)
            {
                return mid;
            }

            if (cmp < 0)
            {
                lo = mid + 1;
            }
            else
            {
                hi = mid - 1;
            }
        }

        return ~lo;
    }

    private sealed record Entry(CheckId Id, Digest ReadSet, ImmutableArray<CheckFailure> Failures);
}
