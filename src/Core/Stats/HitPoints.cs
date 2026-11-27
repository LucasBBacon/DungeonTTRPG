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
