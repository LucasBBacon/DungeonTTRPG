using System;
using System.Collections.Immutable;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Stats;

/// <summary>
/// The SM 6 numbers M1-08 adds, as rows of the derived-number table (<c>docs/effects-vocabulary.md</c> 5.3), and the
/// save and skill roll targets (5.4). Each base reads the unit's stored stat block and its other numbers through
/// <see cref="NumberBaseArgs.Number"/>, so effects on an ability flow into the saves and skills that read it.
/// </summary>
public static class StatNumbers
{
    private const string Proficiency = "proficiency";

    /// <summary>The rows: six abilities, proficiency, three saves, ability difficulty, nine skills, two passives, AC, initiative, attack, Medicine rest bonus, max HP, Recovery.</summary>
    public static ImmutableArray<DerivedNumber> Rows { get; } = BuildRows();

    /// <summary>The roll targets: <c>saveRoll</c>, one per save, <c>skillCheck</c>, one per skill.</summary>
    public static ImmutableArray<string> RollTargets { get; } = BuildTargets();

    private static ImmutableArray<DerivedNumber> BuildRows()
    {
        var rows = ImmutableArray.CreateBuilder<DerivedNumber>();
        foreach (var ability in Enum.GetValues<Ability>())
        {
            var which = ability;
            rows.Add(new DerivedNumber(
                StatIds.Number(which), NumberRule.AbilityScore, a => a.Unit.Stats.Abilities[which], UnitStats.MaxAbility));
        }

        rows.Add(new DerivedNumber(Proficiency, NumberRule.Stack, a => ProficiencyOf(a.Unit.Stats.Level), null));

        foreach (var save in Enum.GetValues<Save>())
        {
            var which = save;
            var (first, second) = StatIds.AbilitiesOf(which);
            rows.Add(new DerivedNumber(
                StatIds.Number(which),
                NumberRule.Stack,
                a => Math.Max(a.Number(StatIds.Number(first)), a.Number(StatIds.Number(second)))
                    + (a.Unit.Stats.TrainedSave == which ? a.Number(Proficiency) : 0),
                null));
        }

        rows.Add(new DerivedNumber(
            "abilityDifficulty",
            NumberRule.Stack,
            a => 8 + a.Number(Proficiency) + a.Number(StatIds.Number(a.Unit.Stats.MainAbility)),
            null));

        foreach (var skill in Enum.GetValues<Skill>())
        {
            var which = skill;
            var ability = StatIds.AbilityOf(which);
            rows.Add(new DerivedNumber(
                StatIds.Number(which),
                NumberRule.Stack,
                a => a.Number(StatIds.Number(ability)) + (a.Unit.Stats.IsTrained(which) ? a.Number(Proficiency) : 0),
                null));
        }

        rows.Add(new DerivedNumber(
            "passivePerception", NumberRule.Stack, a => 10 + a.Number(StatIds.Number(Skill.Perception)), null));
        rows.Add(new DerivedNumber(
            "passiveStealth", NumberRule.Stack, a => 10 + a.Number(StatIds.Number(Skill.Stealth)), null));
        rows.Add(new DerivedNumber(
            "armourClass", NumberRule.Stack, a => 10 + a.Number(StatIds.Number(Ability.Dexterity)), null));
        rows.Add(new DerivedNumber(
            "initiative", NumberRule.Stack, a => a.Number(StatIds.Number(Ability.Intelligence)), null));
        rows.Add(new DerivedNumber("attack", NumberRule.Stack, 0, null));
        rows.Add(new DerivedNumber(
            "medicineRestBonus",
            NumberRule.Stack,
            a => a.Unit.Stats.IsTrained(Skill.Medicine)
                ? a.Number(StatIds.Number(Ability.Wisdom)) + a.Number(Proficiency)
                : 0,
            null));

        // DH 7.1: 11 + level × max(1, classHp + Con) with a tier, the table HP without; floor 1 (plan call 4).
        rows.Add(new DerivedNumber(
            HitPoints.MaxHpId,
            NumberRule.Stack,
            a => a.Unit.Stats.HpTier == HpTier.Table
                ? a.Unit.Stats.TableHp
                : DeathNumbers.AdventurerBonus
                    + (a.Unit.Stats.Level
                        * Math.Max(1, (int)a.Unit.Stats.HpTier + a.Number(StatIds.Number(Ability.Constitution)))),
            null,
            1));

        // DH 8: 4 + 2 × level + Con, minimum 1, for every unit; it reads no HP tier.
        rows.Add(new DerivedNumber(
            HitPoints.RecoveryId,
            NumberRule.Stack,
            a => DeathNumbers.RecoveryBase
                + (DeathNumbers.RecoveryPerLevel * a.Unit.Stats.Level)
                + a.Number(StatIds.Number(Ability.Constitution)),
            null,
            1));
        return rows.ToImmutable();
    }

    private static ImmutableArray<string> BuildTargets()
    {
        var targets = ImmutableArray.CreateBuilder<string>();
        targets.Add("saveRoll");
        foreach (var save in Enum.GetValues<Save>())
        {
            targets.Add(StatIds.RollTarget(save));
        }

        targets.Add("skillCheck");
        foreach (var skill in Enum.GetValues<Skill>())
        {
            targets.Add(StatIds.RollTarget(skill));
        }

        return targets.ToImmutable();
    }

    // SM 6: +2 at levels 0-4, +3 at 5-8, +4 at 9-12.
    private static long ProficiencyOf(int level) => level <= 4 ? 2 : level <= 8 ? 3 : 4;
}
