using System;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Stats;

/// <summary>
/// The HP basis of a unit (DH 7.1): a party unit's class HP tier, whose value is the class amount per level
/// ("slight 6, sound 7, sturdy 8"), or <see cref="Table"/> for a monster, which keeps its table HP (enemies 6.2).
/// </summary>
public enum HpTier
{
    Table = 0,
    Slight = 6,
    Sound = 7,
    Sturdy = 8,
}

/// <summary>
/// The HP and Recovery numbers (DH 7.1, 8). They are client and lead numbers, not builder tuning, so they live in code
/// (M1-09 owner ruling 2) until <c>death.json</c> (DH 20.1) arrives with the first real tuning lever.
/// </summary>
public static class DeathNumbers
{
    /// <summary><c>hp.adventurerBonus</c> (DH 7.1): Max HP = 11 + level × max(1, classHp + Con). Monsters get no bonus.</summary>
    public const int AdventurerBonus = 11;

    /// <summary><c>recovery.base</c> (DH 8): Recovery = 4 + 2 × level + Con, minimum 1.</summary>
    public const int RecoveryBase = 4;

    /// <summary><c>recovery.perLevel</c> (DH 8): Recovery = 4 + 2 × level + Con, minimum 1.</summary>
    public const int RecoveryPerLevel = 2;
}

/// <summary>
/// A unit's HP numbers as fold queries (M1-09 plan call 4): the maximum and Recovery are rows of the derived-number
/// table (<c>docs/effects-vocabulary.md</c> 5.3), so effects on Constitution flow into both. Nothing here is stored.
/// </summary>
public static class HitPoints
{
    /// <summary>The derived-number id of the maximum HP.</summary>
    public const string MaxHpId = "maxHp";

    /// <summary>The derived-number id of Recovery.</summary>
    public const string RecoveryId = "recovery";

    /// <summary>
    /// <paramref name="unit"/>'s maximum HP (DH 7.1): with a tier, 11 + level × max(1, tier + folded Constitution);
    /// with <see cref="HpTier.Table"/>, its table HP; then its effects; at least 1.
    /// </summary>
    public static long Max(RulesContext rules, GameState s, UnitId unit) => Effects.Effects.Number(rules, s, unit, MaxHpId);

    /// <summary>
    /// <paramref name="unit"/>'s Recovery (DH 8): 4 + 2 × level + folded Constitution, then its effects; at least 1.
    /// It reads no HP tier.
    /// </summary>
    public static long Recovery(RulesContext rules, GameState s, UnitId unit) =>
        Effects.Effects.Number(rules, s, unit, RecoveryId);

    /// <summary>
    /// Heals <paramref name="target"/> by <paramref name="amount"/> up to its current maximum (DH 7.3; M1-09 plan call 10)
    /// and reports the overheal. HP above a lowered maximum counts as the maximum, so such a unit heals nothing. It does
    /// not check Downed (none in M1) or the necrotic heal block (M2), and it does not refuse a unit at 0 HP: M1-10 owns
    /// death. Throws <see cref="ArgumentOutOfRangeException"/> for a negative amount and <see cref="ArgumentException"/>
    /// for an unknown unit.
    /// </summary>
    public static (HealOutcome Outcome, GameState Next) Heal(RulesContext rules, GameState s, UnitId target, long amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), amount, "A heal amount is never negative.");
        }

        var unit = Find(s, target);
        long max = Max(rules, s, target);
        long from = Math.Min(unit.Hp, max);
        long to = Math.Min(max, checked(from + amount));
        int hpAfter = ToHp(to);
        return (new HealOutcome(target, amount, to - from, amount - (to - from), hpAfter), WithHp(s, unit, hpAfter));
    }

    /// <summary>
    /// Lowers <paramref name="target"/>'s HP to its current maximum if it is above it (DH 7.3: "Every other change to
    /// the maximum only clamps current HP"); otherwise returns <paramref name="s"/> itself. The M1-12 application
    /// pipeline calls it when an effect lowers the maximum. Throws <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static GameState ClampToMax(RulesContext rules, GameState s, UnitId target)
    {
        var unit = Find(s, target);
        long max = Max(rules, s, target);
        return unit.Hp > max ? WithHp(s, unit, ToHp(max)) : s;
    }

    /// <summary>
    /// Raises <paramref name="target"/> to <paramref name="newLevel"/> (M1-10 plan call 13; DH 7.3): current HP gains
    /// Δ = maximum after − maximum before, both folded with the unit's current effects. Should Δ ever be negative (no
    /// HP rule lowers the maximum on a level-up), the change only clamps current HP, as every other change to the
    /// maximum does. Spent counts and the turn budget are left alone (AB 6.2: "A level-up never refills anything").
    /// The same level is allowed and changes nothing. Throws <see cref="ArgumentOutOfRangeException"/> for a level
    /// outside <see cref="UnitStats.MinLevel"/> to <see cref="UnitStats.MaxLevel"/> or below the unit's level, and
    /// <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static GameState RaiseLevel(RulesContext rules, GameState s, UnitId target, int newLevel)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        if (newLevel < UnitStats.MinLevel || newLevel > UnitStats.MaxLevel)
        {
            throw new ArgumentOutOfRangeException(
                nameof(newLevel), newLevel, $"A level is {UnitStats.MinLevel} to {UnitStats.MaxLevel}.");
        }

        var unit = Find(s, target);
        if (newLevel < unit.Stats.Level)
        {
            throw new ArgumentOutOfRangeException(
                nameof(newLevel), newLevel, $"Unit {target.Value} is level {unit.Stats.Level}; a level-up never lowers it.");
        }

        long before = Max(rules, s, target);
        var raised = unit with { Stats = unit.Stats with { Level = newLevel } };
        var next = s with { Units = s.Units.SetItem(s.Units.IndexOf(unit), raised) };
        long delta = checked(Max(rules, next, target) - before);
        return delta > 0 ? WithHp(next, raised, ToHp(checked(raised.Hp + delta))) : ClampToMax(rules, next, target);
    }

    /// <summary>The unit, or <see cref="ArgumentException"/> as the contests and fold queries report one.</summary>
    internal static Unit Find(GameState s, UnitId id) =>
        s.FindUnit(id) ?? throw new ArgumentException($"Unit {id.Value} is not in the state.", nameof(id));

    /// <summary>
    /// The one place a derived HP number narrows to the stored <see cref="Unit.Hp"/>: the maximum is a <c>long</c> fold,
    /// current HP an <c>int</c>. A value out of range throws rather than wraps.
    /// </summary>
    internal static int ToHp(long value) => checked((int)value);

    /// <summary><paramref name="s"/> with <paramref name="unit"/>'s HP set to <paramref name="hp"/>, nothing else changed.</summary>
    internal static GameState WithHp(GameState s, Unit unit, int hp) =>
        s with { Units = s.Units.SetItem(s.Units.IndexOf(unit), unit with { Hp = hp }) };
}

/// <summary>
/// A heal's outcome (M1-09 plan call 10): the amount offered, the HP gained, the overheal (the amount past the maximum)
/// and the HP after. <c>Healed + Overheal == Amount</c>.
/// </summary>
public readonly record struct HealOutcome(UnitId Target, long Amount, long Healed, long Overheal, int HpAfter);
