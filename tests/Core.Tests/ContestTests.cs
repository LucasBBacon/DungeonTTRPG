using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>Shared builders for the contest tests: a hero and a foe with chosen stats on a chosen seed.</summary>
internal static class ContestKit
{
    public static readonly UnitId Foe = new(2);

    public static readonly Abilities Zero = new(0, 0, 0, 0, 0, 0);

    public static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("adv", "condition", Roll("attackRoll", "advantage")),
            Def("dis", "condition", Roll("attackRoll", "disadvantage")),
            Def("dis.against", "condition", Roll("attacksAgainst", "disadvantage")),
            Def("save.adv", "condition", Roll("saveRoll", "advantage")),
            Def("will.dis", "condition", Roll("willSave", "disadvantage")),
            Def("skill.adv", "condition", Roll("skillCheck", "advantage")),
            Def("athletics.dis", "condition", Roll("athleticsCheck", "disadvantage")),
            Def("athletics.adv", "condition", Roll("athleticsCheck", "advantage")),
            Def("gear.attack.plus1", "gear", Add("attack", 1)),
            Def("gear.attack.plus5", "gear", Add("attack", 5)),
            Def("gear.attack.minus5", "gear", Add("attack", -5)),
            Def("gear.perception.plus5", "gear", Add("perception", 5))));

    public static UnitStats Block(Abilities abilities, int level = 1, Save trained = Save.Fortitude, params Skill[] skills) =>
        new(level, abilities, Ability.Strength, trained, [.. skills.OrderBy(x => x)], HpTier.Slight, 0);

    /// <summary>The hero (1) and the foe (2) with the given stat blocks on <paramref name="seed"/>; effects land on the unit named.</summary>
    public static GameState StateOf(
        UnitStats hero, UnitStats foe, ulong seed = 7, params (UnitId Unit, string Effect)[] effects)
    {
        var units = new List<Unit>
        {
            new(Hero, new GridPos(1, 1), Faction.Party, [], hero, FullHp),
            new(Foe, new GridPos(3, 3), Faction.Party, [], foe, FullHp),
        };
        var s = GameState.Create(GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####"), units, seed);
        foreach (var (unit, effect) in effects)
        {
            s = Effects.Apply(s, effect, unit, new EffectSource(unit));
        }

        return s;
    }

    public static string Add(string target, int value) => $$"""{"target": "{{target}}", "add": {{value}}}""";

    public static string Roll(string target, string mode) => $$"""{"target": "{{target}}", "rollMode": "{{mode}}"}""";

    public static string Def(string id, string kind, params string[] modifiers) =>
        $$"""{"id": "{{id}}", "kind": "{{kind}}", "modifiers": [{{string.Join(", ", modifiers)}}]}""";

    public static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile("effects/effects.json", "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}

/// <summary>Dice, contest preparation and resolution (M1-08 Task 3).</summary>
public class ContestTests
{
    private static readonly UnitId Foe = ContestKit.Foe;

    private static GameState Plain(ulong seed = 7, params (UnitId Unit, string Effect)[] effects) =>
        ContestKit.StateOf(ContestKit.Block(ContestKit.Zero), ContestKit.Block(ContestKit.Zero), seed, effects);

    [Theory]
    [InlineData(RollMode.Normal, 1)]
    [InlineData(RollMode.Advantage, 2)]
    [InlineData(RollMode.Disadvantage, 2)]
    public void Dice_draw_one_die_normal_and_two_with_a_mode(RollMode mode, int draws)
    {
        var stream = Plain().Rng.Combat;
        var (a, afterA) = stream.Next();
        var (b, _) = afterA.Next();
        int first = KeyAlgebra.D20(a), second = KeyAlgebra.D20(b);

        var (roll, next) = Dice.D20(stream, mode);

        Assert.Equal(stream.Counter + (ulong)draws, next.Counter);
        Assert.Equal(stream.Key, next.Key);
        Assert.Equal(mode, roll.Mode);
        Assert.Equal(first, roll.First);
        Assert.Equal(mode == RollMode.Normal ? null : second, roll.Second);
        Assert.Equal(
            mode switch { RollMode.Normal => first, RollMode.Advantage => Math.Max(first, second), _ => Math.Min(first, second) },
            roll.Kept);
        Assert.Equal(Dice.D20(stream, RollMode.Normal).Roll.First, roll.First);
    }

    [Fact]
    public void Each_contest_advances_only_the_combat_counter()
    {
        var s = Plain();
        var before = Snapshot.Save(s);
        var rules = ContestKit.Rules;
        var spec = new AttackSpec(Ability.Strength, true);

        var attack = Contests.Attack(rules, s, Hero, Foe, spec).Next;
        var save = Contests.SaveRoll(rules, s, Foe, Save.Will, 10).Next;
        var skill = Contests.SkillCheck(rules, s, Hero, Skill.Athletics, 10).Next;
        var versus = Contests.SkillVsUnit(rules, s, Hero, Skill.Athletics, Foe, Defence.Reflex).Next;

        foreach (var next in new[] { attack, save, skill, versus })
        {
            Assert.Equal(s.Rng.Shrine, next.Rng.Shrine);
            Assert.True(next.Rng.Combat.Counter > s.Rng.Combat.Counter);
            Assert.NotEqual(before, Snapshot.Save(next));
            Assert.Equal(before, Snapshot.Save(next with { Rng = next.Rng with { Combat = s.Rng.Combat } }));
        }
    }

    [Fact]
    public void Prepare_draws_nothing()
    {
        var s = Plain();
        var before = Snapshot.Save(s);
        var rules = ContestKit.Rules;

        _ = Contests.PrepareAttack(rules, s, Hero, Foe, new AttackSpec(Ability.Strength, true));
        _ = Contests.PrepareSave(rules, s, Hero, Save.Reflex, 12);
        _ = Contests.PrepareSkill(rules, s, Hero, Skill.Lore, 12);
        _ = Contests.PrepareSkillVsUnit(rules, s, Hero, Skill.Lore, Foe, Defence.PassiveStealth);

        Assert.Equal(before, Snapshot.Save(s));
    }

    [Fact]
    public void Prepare_attack_combines_ability_proficiency_and_flat_adds()
    {
        var hero = ContestKit.Block(ContestKit.Zero.With(Ability.Strength, 3), level: 5);
        var foe = ContestKit.Block(ContestKit.Zero.With(Ability.Dexterity, 2));
        var s = ContestKit.StateOf(hero, foe, 7, (Hero, "gear.attack.plus1"));

        var trained = Contests.PrepareAttack(ContestKit.Rules, s, Hero, Foe, new AttackSpec(Ability.Strength, true));
        Assert.Equal(new ContestSetup(7, 12, RollMode.Normal), trained);

        var untrained = Contests.PrepareAttack(ContestKit.Rules, s, Hero, Foe, new AttackSpec(Ability.Strength, false));
        Assert.Equal(new ContestSetup(4, 12, RollMode.Normal), untrained);
    }

    [Fact]
    public void Prepare_save_reads_the_save_and_both_roll_targets()
    {
        var hero = ContestKit.Block(ContestKit.Zero.With(Ability.Wisdom, 2), trained: Save.Will);
        var s = ContestKit.StateOf(hero, ContestKit.Block(ContestKit.Zero));

        Assert.Equal(new ContestSetup(4, 13, RollMode.Normal), Contests.PrepareSave(ContestKit.Rules, s, Hero, Save.Will, 13));

        var adv = Effects.Apply(s, "save.adv", Hero, new EffectSource(Hero));
        Assert.Equal(RollMode.Advantage, Contests.PrepareSave(ContestKit.Rules, adv, Hero, Save.Will, 13).Mode);

        var both = Effects.Apply(adv, "will.dis", Hero, new EffectSource(Hero));
        Assert.Equal(RollMode.Normal, Contests.PrepareSave(ContestKit.Rules, both, Hero, Save.Will, 13).Mode);
        Assert.Equal(RollMode.Advantage, Contests.PrepareSave(ContestKit.Rules, both, Hero, Save.Fortitude, 13).Mode);
    }

    [Fact]
    public void Prepare_skill_reads_the_skill_number_and_difficulty()
    {
        var hero = ContestKit.Block(ContestKit.Zero.With(Ability.Strength, 2), level: 5, skills: Skill.Athletics);
        var s = ContestKit.StateOf(hero, ContestKit.Block(ContestKit.Zero));
        Assert.Equal(new ContestSetup(5, 14, RollMode.Normal), Contests.PrepareSkill(ContestKit.Rules, s, Hero, Skill.Athletics, 14));
    }

    [Fact]
    public void Prepare_skill_reads_the_general_and_the_skill_roll_targets()
    {
        var s = Plain();
        var general = Effects.Apply(s, "skill.adv", Hero, new EffectSource(Hero));
        Assert.Equal(RollMode.Advantage, Contests.PrepareSkill(ContestKit.Rules, general, Hero, Skill.Athletics, 10).Mode);

        var both = Effects.Apply(general, "athletics.dis", Hero, new EffectSource(Hero));
        Assert.Equal(RollMode.Normal, Contests.PrepareSkill(ContestKit.Rules, both, Hero, Skill.Athletics, 10).Mode);

        var athleticsOnly = Effects.Apply(s, "athletics.adv", Hero, new EffectSource(Hero));
        Assert.Equal(RollMode.Advantage, Contests.PrepareSkill(ContestKit.Rules, athleticsOnly, Hero, Skill.Athletics, 10).Mode);
        Assert.Equal(RollMode.Normal, Contests.PrepareSkill(ContestKit.Rules, athleticsOnly, Hero, Skill.Lore, 10).Mode);
    }

    [Fact]
    public void Skill_checks_draw_two_dice_with_a_mode()
    {
        var plain = Plain();
        var adv = Effects.Apply(plain, "skill.adv", Hero, new EffectSource(Hero));

        Assert.Equal(plain.Rng.Combat.Counter + 1, Contests.SkillCheck(ContestKit.Rules, plain, Hero, Skill.Athletics, 10).Next.Rng.Combat.Counter);
        Assert.Equal(plain.Rng.Combat.Counter + 1, Contests.SkillVsUnit(ContestKit.Rules, plain, Hero, Skill.Athletics, Foe, Defence.Reflex).Next.Rng.Combat.Counter);
        Assert.Equal(adv.Rng.Combat.Counter + 2, Contests.SkillCheck(ContestKit.Rules, adv, Hero, Skill.Athletics, 10).Next.Rng.Combat.Counter);
        Assert.Equal(adv.Rng.Combat.Counter + 2, Contests.SkillVsUnit(ContestKit.Rules, adv, Hero, Skill.Athletics, Foe, Defence.Reflex).Next.Rng.Combat.Counter);
    }

    [Fact]
    public void Skill_against_a_unit_meets_ten_plus_the_matching_save_including_training()
    {
        var hero = ContestKit.Block(ContestKit.Zero.With(Ability.Strength, 1), skills: Skill.Athletics);
        var foe = ContestKit.Block(ContestKit.Zero.With(Ability.Strength, 2), level: 5, trained: Save.Fortitude);
        var s = ContestKit.StateOf(hero, foe);

        var setup = Contests.PrepareSkillVsUnit(ContestKit.Rules, s, Hero, Skill.Athletics, Foe, Defence.Fortitude);
        Assert.Equal(new ContestSetup(3, 15, RollMode.Normal), setup);
    }

    [Fact]
    public void Skill_against_a_passive_meets_the_passive_itself()
    {
        var foe = ContestKit.Block(ContestKit.Zero.With(Ability.Wisdom, 2), skills: Skill.Perception);
        var s = ContestKit.StateOf(ContestKit.Block(ContestKit.Zero), foe, 7, (Foe, "gear.perception.plus5"));

        var perception = Contests.PrepareSkillVsUnit(ContestKit.Rules, s, Hero, Skill.Stealth, Foe, Defence.PassivePerception);
        Assert.Equal(19, perception.Target);
        var stealth = Contests.PrepareSkillVsUnit(ContestKit.Rules, s, Hero, Skill.Stealth, Foe, Defence.PassiveStealth);
        Assert.Equal(10, stealth.Target);
    }

    // Review Focus 3.
    [Fact]
    public void Several_sources_fold_once_in_an_attack()
    {
        var s = Plain(7, (Hero, "adv"), (Hero, "dis"), (Foe, "dis.against"));

        Assert.Equal(RollMode.Normal, Contests.PrepareAttack(ContestKit.Rules, s, Hero, Foe, new AttackSpec(Ability.Strength, false)).Mode);

        var (result, next) = Contests.Attack(ContestKit.Rules, s, Hero, Foe, new AttackSpec(Ability.Strength, false));
        Assert.Equal(RollMode.Normal, result.Roll.Mode);
        Assert.Null(result.Roll.Second);
        Assert.Equal(s.Rng.Combat.Counter + 1, next.Rng.Combat.Counter);

        // The defender's disadvantage alone is disadvantage, with two draws.
        var only = Plain(7, (Foe, "dis.against"));
        var (drawn, afterDrawn) = Contests.Attack(ContestKit.Rules, only, Hero, Foe, new AttackSpec(Ability.Strength, false));
        Assert.Equal(RollMode.Disadvantage, drawn.Roll.Mode);
        Assert.Equal(only.Rng.Combat.Counter + 2, afterDrawn.Rng.Combat.Counter);
    }
}
