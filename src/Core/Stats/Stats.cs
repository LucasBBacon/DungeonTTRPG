using System;
using System.Collections.Immutable;

namespace Dungeon.Core.Stats;

/// <summary>The six abilities (SM 5.1).</summary>
public enum Ability { Strength, Dexterity, Constitution, Intelligence, Wisdom, Presence }

/// <summary>The three saves (SM 6).</summary>
public enum Save { Fortitude, Reflex, Will }

/// <summary>The nine skills (SM 7.1).</summary>
public enum Skill { Athletics, Acrobatics, Stealth, Mechanics, Perception, Intimidation, Leadership, Medicine, Lore }

/// <summary>A unit's six ability bonuses. A player's range is -3 to +5; a monster's the same (ED line 221).</summary>
public readonly record struct Abilities(
    int Strength, int Dexterity, int Constitution, int Intelligence, int Wisdom, int Presence)
{
    public int this[Ability a] => a switch
    {
        Ability.Strength => Strength,
        Ability.Dexterity => Dexterity,
        Ability.Constitution => Constitution,
        Ability.Intelligence => Intelligence,
        Ability.Wisdom => Wisdom,
        Ability.Presence => Presence,
        _ => throw new ArgumentOutOfRangeException(nameof(a), a, "Not an ability."),
    };
}

/// <summary>
/// A unit's stat block (SM 5.1, 5.5): level 0 to 12, six ability bonuses, the main ability, the one trained save
/// and the trained skills. <see cref="TrainedSkills"/> is sorted by skill, each at most once.
/// </summary>
public sealed record UnitStats(
    int Level, Abilities Abilities, Ability MainAbility, Save TrainedSave, ImmutableArray<Skill> TrainedSkills)
{
    public const int MinLevel = 0;
    public const int MaxLevel = 12;
    public const int MinAbility = -3;
    public const int MaxAbility = 5;

    public bool IsTrained(Skill s) => !TrainedSkills.IsDefault && TrainedSkills.Contains(s);
}

/// <summary>The content-id spellings of the stat numbers and roll targets, and the ability each skill and save reads.</summary>
public static class StatIds
{
    private static readonly string[] AbilityIds =
        ["strength", "dexterity", "constitution", "intelligence", "wisdom", "presence"];

    private static readonly string[] SaveIds = ["fortitude", "reflex", "will"];

    private static readonly string[] SkillIds =
        ["athletics", "acrobatics", "stealth", "mechanics", "perception", "intimidation", "leadership", "medicine", "lore"];

    private static readonly string[] SaveTargets = ["fortitudeSave", "reflexSave", "willSave"];

    private static readonly string[] SkillTargets =
    [
        "athleticsCheck", "acrobaticsCheck", "stealthCheck", "mechanicsCheck", "perceptionCheck",
        "intimidationCheck", "leadershipCheck", "medicineCheck", "loreCheck",
    ];

    public static string Number(Ability a) => Pick(AbilityIds, (int)a, a);

    public static string Number(Save s) => Pick(SaveIds, (int)s, s);

    public static string Number(Skill s) => Pick(SkillIds, (int)s, s);

    public static string RollTarget(Save s) => Pick(SaveTargets, (int)s, s);

    public static string RollTarget(Skill s) => Pick(SkillTargets, (int)s, s);

    /// <summary>The ability a skill check adds (SM 7.1).</summary>
    public static Ability AbilityOf(Skill s) => s switch
    {
        Skill.Athletics => Ability.Strength,
        Skill.Acrobatics or Skill.Stealth => Ability.Dexterity,
        Skill.Mechanics or Skill.Lore => Ability.Intelligence,
        Skill.Perception or Skill.Medicine => Ability.Wisdom,
        Skill.Intimidation or Skill.Leadership => Ability.Presence,
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, "Not a skill."),
    };

    /// <summary>The two abilities a save adds, the higher one counting (SM 6).</summary>
    public static (Ability, Ability) AbilitiesOf(Save s) => s switch
    {
        Save.Fortitude => (Ability.Strength, Ability.Constitution),
        Save.Reflex => (Ability.Dexterity, Ability.Intelligence),
        Save.Will => (Ability.Wisdom, Ability.Presence),
        _ => throw new ArgumentOutOfRangeException(nameof(s), s, "Not a save."),
    };

    private static string Pick<T>(string[] table, int index, T value)
        where T : struct, Enum =>
        (uint)index < (uint)table.Length ? table[index] : throw new ArgumentOutOfRangeException(nameof(value), value, null);
}
