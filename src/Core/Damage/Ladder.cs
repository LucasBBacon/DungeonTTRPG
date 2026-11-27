using System;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Damage;

/// <summary>
/// The damage ladder (SM 8.3): the step a unit's active effects give a damage type, and what a part deals at that step.
/// </summary>
public static class Ladder
{
    /// <summary>
    /// <paramref name="target"/>'s step for <paramref name="type"/>, from the ladder flags its active effects set
    /// (M1-09 plan call 6). Flags are booleans, so two sources of one resistance count once (SM 8.3). Immunity wins over
    /// everything, a weakness included; otherwise a resistance and a weakness to the same type cancel to Normal (SM 8.3);
    /// otherwise the one set gives the step; with neither, Normal.
    /// </summary>
    public static LadderStep StepFor(RulesContext rules, GameState s, UnitId target, DamageType type)
    {
        if (Effects.Effects.Flag(rules, s, target, "unit", DamageIds.Immune(type)))
        {
            return LadderStep.Immune;
        }

        bool resistant = Effects.Effects.Flag(rules, s, target, "unit", DamageIds.Resistant(type));
        bool weak = Effects.Effects.Flag(rules, s, target, "unit", DamageIds.Weak(type));
        return (resistant, weak) switch
        {
            (true, true) => LadderStep.Normal,
            (true, false) => LadderStep.Resistant,
            (false, true) => LadderStep.Weak,
            _ => LadderStep.Normal,
        };
    }

    /// <summary>
    /// What a part deals at <paramref name="step"/> (M1-09 plan call 7, owner ruling 1). <paramref name="raw"/> is the
    /// part's dice plus its flat amount, so "flat changes first" (SM 8.3); it may be zero or negative. Weak gives
    /// raw + half, Resistant half, both rounding down (SM 3); Normal raw; Immune 0. A part that is not immune deals at
    /// least 1 (SM 8.3: "Damage that is not immune is at least 1"), so a 1d4 - 5 that rolls 2 deals 1.
    /// </summary>
    public static long Apply(LadderStep step, long raw)
    {
        long dealt = step switch
        {
            LadderStep.Weak => checked(raw + IntMath.FloorDiv(raw, 2)),
            LadderStep.Normal => raw,
            LadderStep.Resistant => IntMath.FloorDiv(raw, 2),
            LadderStep.Immune => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(step), step, "Not a ladder step."),
        };
        return step == LadderStep.Immune ? 0 : Math.Max(dealt, 1);
    }
}
