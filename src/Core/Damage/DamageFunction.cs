using System;
using System.Collections.Immutable;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.Damage;

/// <summary>
/// One part as rolled: the part, the faces drawn in order (twice its dice on a crit), and its raw amount, the faces'
/// sum plus the part's flat amount once (DH 9; M1-09 plan calls 7 and 8).
/// </summary>
public readonly record struct PartRoll(DamagePart Part, ImmutableArray<int> Faces, long Raw);

/// <summary>A hit's damage as rolled, part by part in list order, and whether it was a crit.</summary>
public sealed record DamageRoll(ImmutableArray<PartRoll> Parts, bool Crit);

/// <summary>
/// One part as resolved: its type, the target's ladder step for it, its raw amount and what it dealt. It is the place
/// of the M2 rider step (SM 9; M1-09 plan call 9): a target weak to a part's type gets that type's rider.
/// </summary>
public readonly record struct PartOutcome(DamageType Type, LadderStep Step, long Raw, long Dealt);

/// <summary>
/// A hit's damage as resolved against a unit: each part's outcome, the total dealt, the stored HP before, the HP after
/// (never below 0), and whether HP reached 0 (DH 20.3 step 7; what follows is M1-10's).
/// </summary>
public sealed record DamageOutcome(
    UnitId Target, ImmutableArray<PartOutcome> Parts, long Total, int HpBefore, int HpAfter, bool ReachedZero);

/// <summary>
/// The damage function (SM 9, AB 9.1, DH 20.3), like the M1-08 contests in three steps: <see cref="Roll"/> draws the
/// dice on <c>combat</c> (SV 4.3) and touches nothing else, <see cref="Resolve"/> is pure, and <see cref="Apply"/>
/// writes only the target's HP. It raises no events (M1-09 plan call 11): M1-11 does, when the ability pipeline calls it.
/// </summary>
public static class Damage
{
    /// <summary>
    /// Rolls <paramref name="parts"/> on the combat stream (M1-09 plan call 8): parts in list order, each part's dice in
    /// order, one draw per die; on a <paramref name="crit"/> each part draws twice its dice and adds its flat once (DH 9).
    /// Throws <see cref="ArgumentException"/> naming the index of a part with negative dice, or with dice and fewer than
    /// one side.
    /// </summary>
    public static (DamageRoll Roll, GameState Next) Roll(GameState s, ImmutableArray<DamagePart> parts, bool crit)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            if (part.Dice < 0)
            {
                throw new ArgumentException($"Damage part at index {i} has {part.Dice} dice; it needs at least 0.", nameof(parts));
            }

            if (part.Dice > 0 && part.Sides < 1)
            {
                throw new ArgumentException($"Damage part at index {i} has {part.Sides} sides; a die needs at least 1.", nameof(parts));
            }
        }

        var stream = s.Rng.Combat;
        var rolled = ImmutableArray.CreateBuilder<PartRoll>(parts.Length);
        foreach (var part in parts)
        {
            int count = crit ? checked(2 * part.Dice) : part.Dice;
            var faces = ImmutableArray.CreateBuilder<int>(count);
            long raw = part.Flat;
            for (int d = 0; d < count; d++)
            {
                var (face, next) = Dice.Die(stream, part.Sides);
                faces.Add(face);
                raw = checked(raw + face);
                stream = next;
            }

            rolled.Add(new PartRoll(part, faces.MoveToImmutable(), raw));
        }

        return (new DamageRoll(rolled.MoveToImmutable(), crit), s with { Rng = s.Rng with { Combat = stream } });
    }

    /// <summary>
    /// What <paramref name="roll"/> does to <paramref name="target"/>, in the DH 20.3 order, without changing anything.
    /// HP after = max(0, min(HP, maximum) - total). Throws <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static DamageOutcome Resolve(RulesContext rules, GameState s, UnitId target, DamageRoll roll)
    {
        var unit = HitPoints.Find(s, target);

        // DH 20.3, in order (M1-09 plan call 1):
        // 1. Downed: discard the damage. M2 (no Downed or companions in M1).
        // 2. Each part through the target's ladder step for its type (SM 8.3, 9; owner ruling 1). Shells and layers,
        //    transforms and temporary HP: M2 or later.
        var parts = ImmutableArray.CreateBuilder<PartOutcome>(roll.Parts.Length);
        foreach (var part in roll.Parts)
        {
            var step = Ladder.StepFor(rules, s, target, part.Part.Type);
            parts.Add(new PartOutcome(part.Part.Type, step, part.Raw, Ladder.Apply(step, part.Raw)));
        }

        // 3. Sum the parts.
        long total = 0;
        foreach (var part in parts)
        {
            total = checked(total + part.Dealt);
        }

        // 3a. Duck (Shade): M2.
        // 4. The out-of-combat and trap floors: M1-10.
        // 5. The save chain (Death-warded, the phial): M2.
        // 6. Lower HP, never below 0. HP above a lowered maximum counts as the maximum.
        long from = Math.Min(unit.Hp, HitPoints.Max(rules, s, target));
        int hpAfter = HitPoints.ToHp(Math.Max(0, from - total));

        // 7. At 0: report it. The consequences (death, RunEnded, removing a dead monster) are M1-10's.
        // 8. Riders and on-damage triggers: M2. PartOutcome is the rider's place (plan call 9).
        return new DamageOutcome(target, parts.MoveToImmutable(), total, unit.Hp, hpAfter, hpAfter == 0);
    }

    /// <summary><see cref="Resolve"/>, then writes the target's HP after; nothing else in the state changes.</summary>
    public static (DamageOutcome Outcome, GameState Next) Apply(RulesContext rules, GameState s, UnitId target, DamageRoll roll)
    {
        var outcome = Resolve(rules, s, target, roll);
        return (outcome, HitPoints.WithHp(s, HitPoints.Find(s, target), outcome.HpAfter));
    }

    /// <summary><see cref="Roll"/> then <see cref="Apply"/>: the combat stream advances by the dice drawn.</summary>
    public static (DamageOutcome Outcome, GameState Next) Deal(
        RulesContext rules, GameState s, UnitId target, ImmutableArray<DamagePart> parts, bool crit)
    {
        var (roll, rolled) = Roll(s, parts, crit);
        return Apply(rules, rolled, target, roll);
    }
}
