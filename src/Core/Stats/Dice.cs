using System;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Stats;

/// <summary>
/// One d20 roll (SM 6, W 7.5). <see cref="Second"/> is null exactly when the mode is normal; <see cref="Kept"/> is the
/// die that counts: the first for normal, the higher for advantage, the lower for disadvantage.
/// </summary>
public readonly record struct D20Roll(RollMode Mode, int First, int? Second)
{
    /// <summary>The die the total is built on.</summary>
    public int Kept => Mode switch
    {
        RollMode.Advantage => Math.Max(First, Second ?? First),
        RollMode.Disadvantage => Math.Min(First, Second ?? First),
        _ => First,
    };
}

/// <summary>The d20 draw: one die normally, two with advantage or disadvantage, the first drawn being the plain die.</summary>
public static class Dice
{
    /// <summary>Draws the roll for <paramref name="mode"/> from <paramref name="stream"/> and returns the advanced stream.</summary>
    public static (D20Roll Roll, DrawStream Next) D20(DrawStream stream, RollMode mode)
    {
        var (u1, afterFirst) = stream.Next();
        var first = KeyAlgebra.D20(u1);
        if (mode == RollMode.Normal)
        {
            return (new D20Roll(mode, first, null), afterFirst);
        }

        var (u2, afterSecond) = afterFirst.Next();
        return (new D20Roll(mode, first, KeyAlgebra.D20(u2)), afterSecond);
    }
}
