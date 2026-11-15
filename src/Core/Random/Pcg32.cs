using System;

namespace Dungeon.Core;

/// <summary>Immutable state of one PCG32 stream. <see cref="Inc"/> selects the stream and is always odd.</summary>
public readonly record struct RngState(ulong State, ulong Inc);

/// <summary>
/// PCG32 (XSH-RR 64/32) as pure functions: every draw returns the value and the next state.
/// This is the only random source the rules core may use.
/// </summary>
public static class Pcg32
{
    private const ulong Multiplier = 6364136223846793005UL;

    public static RngState Seed(ulong seed, ulong stream)
    {
        unchecked
        {
            var s = new RngState(0, (stream << 1) | 1);
            s = Step(s);
            s = s with { State = s.State + seed };
            return Step(s);
        }
    }

    public static (uint Value, RngState Next) NextUInt(RngState s)
    {
        unchecked
        {
            // The output is computed from the state before stepping.
            ulong old = s.State;
            uint x = (uint)(((old >> 18) ^ old) >> 27);
            int rot = (int)(old >> 59);
            uint value = (x >> rot) | (x << ((-rot) & 31));
            return (value, Step(s));
        }
    }

    /// <summary>Uniform integer in <c>0..exclusiveMax-1</c>, without modulo bias.</summary>
    public static (int Value, RngState Next) NextInt(RngState s, int exclusiveMax)
    {
        if (exclusiveMax <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(exclusiveMax), exclusiveMax, "Random bound must be positive.");
        }

        unchecked
        {
            uint b = (uint)exclusiveMax;
            uint threshold = (0u - b) % b;
            while (true)
            {
                var (r, next) = NextUInt(s);
                s = next;
                if (r >= threshold)
                {
                    return ((int)(r % b), s);
                }
            }
        }
    }

    /// <summary>Die roll in <c>1..sides</c>.</summary>
    public static (int Value, RngState Next) Roll(RngState s, int sides)
    {
        if (sides <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sides), sides, "Die must have a positive number of sides.");
        }

        var (v, next) = NextInt(s, sides);
        return (v + 1, next);
    }

    private static RngState Step(RngState s) =>
        unchecked(new RngState(s.State * Multiplier + s.Inc, s.Inc));
}

/// <summary>The three independent random streams of a run: dungeon generation, combat rolls and loot.</summary>
public sealed record RngStreams(RngState Generation, RngState Combat, RngState Loot)
{
    public static RngStreams FromSeed(ulong runSeed) =>
        new(Pcg32.Seed(runSeed, 1), Pcg32.Seed(runSeed, 2), Pcg32.Seed(runSeed, 3));
}
