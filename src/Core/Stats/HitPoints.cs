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
}
