using System;
using System.Collections.Immutable;
using Dungeon.Core.Canonical;

namespace Dungeon.Core;

/// <summary>
/// The unlock snapshot fixed at run start (SV 4.6): the profile's unlocked ids, as a sorted set. Empty
/// in M1; the unlock set goes live in M3.
/// </summary>
public sealed record UnlockSnapshot(ImmutableArray<string> Ids) : ICanonical
{
    /// <summary>No unlocks: the M1 snapshot.</summary>
    public static UnlockSnapshot None { get; } = new(ImmutableArray<string>.Empty);

    /// <summary>SHA-256 over this snapshot's canonical bytes (SV 4.6). Validates the snapshot first.</summary>
    public Digest UnlockHash => Digest.Of(Validate(this));

    /// <summary>Returns <paramref name="u"/> if its ids are distinct content ids; otherwise throws.</summary>
    public static UnlockSnapshot Validate(UnlockSnapshot u)
    {
        ArgumentNullException.ThrowIfNull(u);
        RunConfig.RequireIdSet(u.Ids, nameof(Ids));
        return u;
    }

    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(1);
        w.SetById(0, Ids, s => s, (e, s) => e.String(0, s));
        w.EndRecord();
    }
}
