using System;

namespace Dungeon.Core;

/// <summary>A delivery wave, ordered <c>M1 &lt; M2 &lt; M3 &lt; M4 &lt; Later</c> (plan call 1).</summary>
public enum Wave
{
    /// <summary>Milestone 1.</summary>
    M1,

    /// <summary>Milestone 2.</summary>
    M2,

    /// <summary>Milestone 3.</summary>
    M3,

    /// <summary>Milestone 4.</summary>
    M4,

    /// <summary>After milestone 4.</summary>
    Later,
}

/// <summary>The build's wave and the D-08 inert rule.</summary>
public static class Waves
{
    /// <summary>
    /// The wave this build delivers. A Core constant, not run configuration (plan call 1); M1-06 may move it
    /// into the reproducibility key as its own decision.
    /// </summary>
    public static Wave Build => Wave.M1;

    /// <summary>Parses a wave by its exact member name (<c>"M1"</c> to <c>"M4"</c>, <c>"Later"</c>); any other spelling is refused.</summary>
    public static bool TryParse(string s, out Wave wave)
    {
        foreach (var value in Enum.GetValues<Wave>())
        {
            if (string.Equals(value.ToString(), s, StringComparison.Ordinal))
            {
                wave = value;
                return true;
            }
        }

        wave = default;
        return false;
    }

    /// <summary>True if a row of <paramref name="rowWave"/> is later than <see cref="Build"/>, so the DrawLedger expects 0 draws on it (D-08).</summary>
    public static bool IsInert(Wave rowWave) => rowWave > Build;
}
