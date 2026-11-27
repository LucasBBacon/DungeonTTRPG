using System;

namespace Dungeon.Core;

/// <summary>
/// The sequential-saved draw streams of a run (SV 4.3), opened through the generated stream handles. Both keys
/// come from the run seed alone, so a save stores the seed and the two counters and re-derives the keys on load.
/// </summary>
/// <param name="Combat">Combat rolls, the <c>combat</c> row: <c>Derive(runSeed, "combat")</c>.</param>
/// <param name="Shrine">
/// The M0 shrine boons, the <c>loot/shrine</c> row (plan call 6): <c>Derive(Derive(runSeed, "loot"), "shrine")</c>;
/// it lives until real loot replaces shrines (M1-33).
/// </param>
public sealed record RunStreams(DrawStream Combat, DrawStream Shrine)
{
    /// <summary>Both streams of <paramref name="runSeed"/>, at counter 0.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="runSeed"/> is at least 2^60.</exception>
    public static RunStreams FromSeed(ulong runSeed)
    {
        RunSeed.Validate(runSeed, nameof(runSeed));
        return new RunStreams(
            Streams.Combat(runSeed).Open(),
            Streams.Loot(runSeed).Shrine.Open());
    }
}
