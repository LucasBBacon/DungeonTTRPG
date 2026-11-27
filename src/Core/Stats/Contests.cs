using System;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Stats;

using Effects = Dungeon.Core.Effects.Effects;

/// <summary>An attack ability and whether the attacker is trained in the weapon (SM 6).</summary>
public readonly record struct AttackSpec(Ability Ability, bool Trained);

/// <summary>What a skill contest against a unit meets: a save (10 + the save) or a passive number.</summary>
public enum Defence { Fortitude, Reflex, Will, PassivePerception, PassiveStealth }

/// <summary>The bonus added to the die, the number it must meet, and the roll mode folded before the draw.</summary>
public readonly record struct ContestSetup(long Bonus, long Target, RollMode Mode);

/// <summary>An attack outcome. <see cref="Crit"/> is kept apart from <see cref="Hit"/> so a downgrade can clear it (AR 8.1).</summary>
public readonly record struct AttackResult(D20Roll Roll, long Total, long Target, bool Hit, bool Crit);

/// <summary>A save or skill check outcome. A total that meets the target succeeds.</summary>
public readonly record struct CheckResult(D20Roll Roll, long Total, long Target, bool Success);

/// <summary>
/// Attack, save and skill contests (SM 6, W 7.5). Preparation and resolution are pure; the drawing methods join them
/// with one d20 from <c>Rng.Combat</c> and touch nothing else in the state.
/// </summary>
public static class Contests
{
    private const string Proficiency = "proficiency";

    // --- Preparation ---

    /// <summary>Bonus: the ability, proficiency if trained, and the flat <c>attack</c> adds. Target: the defender AC.</summary>
    public static ContestSetup PrepareAttack(RulesContext rules, GameState s, UnitId attacker, UnitId defender, AttackSpec spec)
    {
        var bonus = Effects.Number(rules, s, attacker, StatIds.Number(spec.Ability))
            + (spec.Trained ? Effects.Number(rules, s, attacker, Proficiency) : 0)
            + Effects.Number(rules, s, attacker, "attack");
        var target = Effects.Number(rules, s, defender, "armourClass");
        var mode = Effects.RollModeOf(rules, s, [(attacker, "attackRoll"), (defender, "attacksAgainst")]);
        return new ContestSetup(bonus, target, mode);
    }

    /// <summary>Bonus: the save number. The mode folds <c>saveRoll</c> with the save own target.</summary>
    public static ContestSetup PrepareSave(RulesContext rules, GameState s, UnitId defender, Save save, long difficulty) =>
        new(
            Effects.Number(rules, s, defender, StatIds.Number(save)),
            difficulty,
            Effects.RollModeOf(rules, s, [(defender, "saveRoll"), (defender, StatIds.RollTarget(save))]));

    /// <summary>Bonus: the skill number. The mode folds <c>skillCheck</c> with the skill own target.</summary>
    public static ContestSetup PrepareSkill(RulesContext rules, GameState s, UnitId user, Skill skill, long difficulty) =>
        new(
            Effects.Number(rules, s, user, StatIds.Number(skill)),
            difficulty,
            Effects.RollModeOf(rules, s, [(user, "skillCheck"), (user, StatIds.RollTarget(skill))]));

    /// <summary>A skill check against another unit: 10 + its save for a save defence, its passive number itself otherwise.</summary>
    public static ContestSetup PrepareSkillVsUnit(
        RulesContext rules, GameState s, UnitId user, Skill skill, UnitId target, Defence defence)
    {
        var difficulty = defence switch
        {
            Defence.Fortitude => 10 + Effects.Number(rules, s, target, StatIds.Number(Save.Fortitude)),
            Defence.Reflex => 10 + Effects.Number(rules, s, target, StatIds.Number(Save.Reflex)),
            Defence.Will => 10 + Effects.Number(rules, s, target, StatIds.Number(Save.Will)),
            Defence.PassivePerception => Effects.Number(rules, s, target, "passivePerception"),
            Defence.PassiveStealth => Effects.Number(rules, s, target, "passiveStealth"),
            _ => throw new ArgumentOutOfRangeException(nameof(defence)),
        };
        return PrepareSkill(rules, s, user, skill, difficulty);
    }

    // --- Resolution ---

    /// <summary>Natural 20 hits and crits; natural 1 misses; otherwise the total meeting the target hits.</summary>
    public static AttackResult ResolveAttack(ContestSetup setup, D20Roll roll)
    {
        var total = roll.Kept + setup.Bonus;
        return roll.Kept switch
        {
            20 => new AttackResult(roll, total, setup.Target, true, true),
            1 => new AttackResult(roll, total, setup.Target, false, false),
            _ => new AttackResult(roll, total, setup.Target, total >= setup.Target, false),
        };
    }

    /// <summary>A total that meets the target succeeds; the face of the die never decides it.</summary>
    public static CheckResult ResolveCheck(ContestSetup setup, D20Roll roll)
    {
        var total = roll.Kept + setup.Bonus;
        return new CheckResult(roll, total, setup.Target, total >= setup.Target);
    }

    // --- Drawing ---

    /// <summary>Prepares, draws one roll from the combat stream, resolves.</summary>
    public static (AttackResult Result, GameState Next) Attack(
        RulesContext rules, GameState s, UnitId attacker, UnitId defender, AttackSpec spec)
    {
        var setup = PrepareAttack(rules, s, attacker, defender, spec);
        var (roll, next) = Dice.D20(s.Rng.Combat, setup.Mode);
        return (ResolveAttack(setup, roll), s with { Rng = s.Rng with { Combat = next } });
    }

    /// <summary>A save against a difficulty.</summary>
    public static (CheckResult Result, GameState Next) SaveRoll(
        RulesContext rules, GameState s, UnitId defender, Save save, long difficulty) =>
        Draw(s, PrepareSave(rules, s, defender, save, difficulty));

    /// <summary>A skill check against a difficulty.</summary>
    public static (CheckResult Result, GameState Next) SkillCheck(
        RulesContext rules, GameState s, UnitId user, Skill skill, long difficulty) =>
        Draw(s, PrepareSkill(rules, s, user, skill, difficulty));

    /// <summary>A skill check against another unit save or passive.</summary>
    public static (CheckResult Result, GameState Next) SkillVsUnit(
        RulesContext rules, GameState s, UnitId user, Skill skill, UnitId target, Defence defence) =>
        Draw(s, PrepareSkillVsUnit(rules, s, user, skill, target, defence));

    private static (CheckResult Result, GameState Next) Draw(GameState s, ContestSetup setup)
    {
        var (roll, next) = Dice.D20(s.Rng.Combat, setup.Mode);
        return (ResolveCheck(setup, roll), s with { Rng = s.Rng with { Combat = next } });
    }
}
