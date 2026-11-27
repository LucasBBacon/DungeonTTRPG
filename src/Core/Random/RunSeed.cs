using System;

namespace Dungeon.Core;

/// <summary>The run seed is a 60-bit integer (SV 4.3, 4.9); there is no salt option (SV-7).</summary>
public static class RunSeed
{
    /// <summary>Exclusive upper bound of a valid run seed, 2^60.</summary>
    public const ulong Limit = 1UL << 60;

    /// <summary>Returns <paramref name="seed"/> if it is below <see cref="Limit"/>.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="seed"/> is at least 2^60.</exception>
    public static ulong Validate(ulong seed, string paramName)
    {
        if (seed >= Limit)
            throw new ArgumentOutOfRangeException(paramName, seed, "A run seed must be below 2^60.");
        return seed;
    }
}
