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
/// Why a floor stopped a party unit on 1 HP (DH 20.3 step 4's <c>DamageClamped</c> reasons). M1-10 produces only
/// <see cref="OutOfCombat"/> and <see cref="UndetectedTrap"/>; the fall, Stir and lurker floors are M2 or later.
/// </summary>
public enum ClampReason
{
    OutOfCombat,
    UndetectedTrap,
    FallFloor,
    UnwarnedStir,
    LurkerOpener,
}

/// <summary>
/// A hit's damage as resolved against a unit: each part's outcome, the total dealt, the stored HP before, the HP after
/// (never below 0), whether HP reached 0 (DH 20.3 step 7; <c>Combat.Harm.Deal</c> applies the consequences), and the
/// floor that stopped it on 1, or null (step 4; M1-10 plan call 10).
/// </summary>
public sealed record DamageOutcome(
    UnitId Target,
    ImmutableArray<PartOutcome> Parts,
    long Total,
    int HpBefore,
    int HpAfter,
    bool ReachedZero,
    ClampReason? Clamp);

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

        // The combat stream; out of combat h:roll, added with D-22's halved input as one refactor (lead's call; M1-11
        // rulings record 8 item 12).
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
    /// HP after = max(0, min(HP, maximum) - total), unless a floor binds (step 4): then 1. The floors are the party's,
    /// plus, for a condition's <paramref name="tick"/> while no fight is on, the CO 5.5 tick floor for every unit, a
    /// monster included (<see cref="ClampReason.OutOfCombat"/>; M1-12a plan call 13). Throws
    /// <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static DamageOutcome Resolve(RulesContext rules, GameState s, UnitId target, DamageRoll roll, bool tick = false)
    {
        var unit = HitPoints.Find(s, target);

        // DH 20.3, in order (M1-09 plan call 1):
        // 1. Downed: discard the damage. M2 (no Downed or companions in M1).
        // 2. Each part through the target's ladder step for its type (SM 8.3, 9; owner ruling 1). Shells and layers,
        //    transforms: M2 or later. Temporary HP: Later (Bolstered, DH 14; test owed by roadmap D-17; order per
        //    DH 15.2 / 20.3). Halving by a save: FloorDiv per part before the ladder (D-22), in the D-22 step.
        var parts = ImmutableArray.CreateBuilder<PartOutcome>(roll.Parts.Length);
        foreach (var part in roll.Parts)
        {
            var step = Ladder.StepFor(rules, s, target, part.Part.Type);
            parts.Add(new PartOutcome(part.Part.Type, step, part.Raw, DealtByPart(rules, s, target, part.Part, part.Raw)));
        }

        // 3. Sum the parts.
        long total = 0;
        foreach (var part in parts)
        {
            total = checked(total + part.Dealt);
        }

        // 3a. Duck (Shade): M2; never on damage halved by a save or by Tumble Clear (D-22; lead's call).
        // 4. The floors (DH 10.4; M1-10 plan call 10), for a party target. HP above a lowered maximum counts as the
        //    maximum. A floor applies out of combat (OutOfCombat, checked first), or when a part comes from a Hidden or
        //    HunchFloored trap and the other parts alone deal less than that HP (UndetectedTrap: the floor protects
        //    against the trap parts only). A monster keeps only the CO 5.5 tick rule: a condition's tick out of combat
        //    cannot take it below 1 either (OutOfCombat; M1-12a plan call 13). An applying floor binds only on a hit
        //    that would take the unit from 1 or more to below 1, and then HP stops on 1. So a floor never raises HP,
        //    never fires on a hit that leaves 1 or more, and leaves a unit already on 0 on 0, unclamped.
        long from = Math.Min(unit.Hp, HitPoints.Max(rules, s, target));
        ClampReason? clamp = null;
        if (from >= 1 && from - total < 1)
        {
            if (!s.CombatActive && (unit.Faction == Faction.Party || tick))
            {
                clamp = ClampReason.OutOfCombat;
            }
            else if (unit.Faction == Faction.Party && FloorsTrap(roll, parts, from))
            {
                clamp = ClampReason.UndetectedTrap;
            }
        }

        // 5. The save chain (Death-warded, the phial): M2. It runs only on a total that still reaches 0, so a floored
        //    hit never reaches it (DH 21.4).
        // 6. Lower HP, never below 0; a bound floor stops it on 1.
        int hpAfter = clamp is null ? HitPoints.ToHp(Math.Max(0, from - total)) : 1;

        // 7. At 0: report it. Combat.Harm.Deal applies the consequences (death, RunEnded, removing a dead monster).
        // 8. Riders and on-damage triggers: M2. PartOutcome is the rider's place (plan call 9).
        return new DamageOutcome(target, parts.MoveToImmutable(), total, unit.Hp, hpAfter, hpAfter == 0, clamp);
    }

    /// <summary>
    /// What one part of <paramref name="raw"/> deals to <paramref name="target"/>: the target's ladder step for the
    /// part's type applied to the raw amount (DH 20.3 step 2). <see cref="Resolve"/> sums these; the preview weighs them
    /// (M1-11 plan call 14). Throws <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static long DealtByPart(RulesContext rules, GameState s, UnitId target, DamagePart part, long raw) =>
        Ladder.Apply(Ladder.StepFor(rules, s, target, part.Type), raw);

    // The undetected-trap floor applies (DH 10.4; TL 6.5): some part comes from a Hidden or HunchFloored trap, and the
    // parts that do not deal less than from.
    private static bool FloorsTrap(DamageRoll roll, ImmutableArray<PartOutcome>.Builder parts, long from)
    {
        bool trapped = false;
        long others = 0;
        for (int i = 0; i < roll.Parts.Length; i++)
        {
            if (roll.Parts[i].Part.Trap?.DetectionAtFire is Detection.Hidden or Detection.HunchFloored)
            {
                trapped = true;
            }
            else
            {
                others = checked(others + parts[i].Dealt);
            }
        }

        // D-17: once temporary HP absorbs per part, rule whether `others` counts before or after absorption, whether
        // part order may decide life or death (DH 10.4 vs CO 9.3), and what the log shows (M1-10 rulings record 1.1).
        // The question now covers parts halved by a save too: `others` sums the halved parts (M1-11 rulings record 1.6).
        return trapped && others < from;
    }

    /// <summary>
    /// <see cref="Resolve"/> (a condition's <paramref name="tick"/> included), then writes the target's HP after; nothing
    /// else in the state changes.
    /// </summary>
    public static (DamageOutcome Outcome, GameState Next) Apply(RulesContext rules, GameState s, UnitId target, DamageRoll roll, bool tick = false)
    {
        var outcome = Resolve(rules, s, target, roll, tick);
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
