using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The SM 6 numbers M1-08 adds as fold queries (abilities, proficiency, saves, skills, passives, AC and the rest), the
/// ability rule on a stored value, and the roll mode folded once over several sources (M1-08 Task 2).
/// </summary>
public class DerivedNumberTests
{
    private const string EffectsFile = "effects/effects.json";

    private static readonly UnitId Foe = new(2);

    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("trait.str.plus2", "trait", Add("strength", 2)),
            Def("gear.str.plus1", "gear", Add("strength", 1)),
            Def("gear.str.minus2", "gear", Add("strength", -2)),
            Def("gear.str.minus1", "gear", Add("strength", -1)),
            Def("trait.str.plus3", "trait", Add("strength", 3)),
            Def("cond.str.minus2", "condition", Add("strength", -2)),
            Def("cond.con.minus3", "condition", Add("constitution", -3)),
            Def("cond.dex.minus2", "condition", Add("dexterity", -2)),
            Def("gear.perception.plus5", "gear", Add("perception", 5)),
            Def("adv", "condition", """{"target": "attackRoll", "rollMode": "advantage"}"""),
            Def("dis", "condition", """{"target": "attackRoll", "rollMode": "disadvantage"}"""),
            Def("dis.against", "condition", """{"target": "attacksAgainst", "rollMode": "disadvantage"}"""),
            Def("adv.against", "condition", """{"target": "attacksAgainst", "rollMode": "advantage"}""")));

    // --- Abilities and proficiency ---

    [Theory]
    [InlineData(Ability.Strength)]
    [InlineData(Ability.Dexterity)]
    [InlineData(Ability.Constitution)]
    [InlineData(Ability.Intelligence)]
    [InlineData(Ability.Wisdom)]
    [InlineData(Ability.Presence)]
    public void Each_ability_reads_its_stored_value(Ability ability)
    {
        var s = StateOf(new Abilities(-3, -2, -1, 1, 3, 5));
        long[] expected = [-3, -2, -1, 1, 3, 5];
        Assert.Equal(expected[(int)ability], Effects.Number(Rules, s, Hero, StatIds.Number(ability)));
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(4, 2)]
    [InlineData(5, 3)]
    [InlineData(8, 3)]
    [InlineData(9, 4)]
    [InlineData(12, 4)]
    public void Proficiency_follows_the_level_table(int level, long expected) =>
        Assert.Equal(expected, Effects.Number(Rules, StateOf(Zero, level: level), Hero, "proficiency"));

    // --- Saves ---

    [Theory]
    [InlineData(Save.Fortitude, 1, 2, 1, Save.Will, 2)]          // Str 1, Con 2, untrained
    [InlineData(Save.Fortitude, 1, 2, 5, Save.Fortitude, 5)]     // the same, trained at level 5
    [InlineData(Save.Reflex, -1, 3, 1, Save.Fortitude, 3)]       // Dex -1, Int 3
    [InlineData(Save.Will, 0, 0, 1, Save.Will, 2)]               // Wis 0, Presence 0, trained at level 1
    public void A_save_takes_the_better_ability_plus_proficiency_if_trained(
        Save save, int first, int second, int level, Save trained, long expected)
    {
        var (a, b) = StatIds.AbilitiesOf(save);
        var s = StateOf(Zero.With(a, first).With(b, second), level: level, trainedSave: trained);
        Assert.Equal(expected, Effects.Number(Rules, s, Hero, StatIds.Number(save)));
    }

    // Review Focus 2: the better of the folded values, not the stored ones.
    [Fact]
    public void A_save_reads_the_abilities_after_their_effects()
    {
        var abilities = Zero.With(Ability.Strength, 1).With(Ability.Constitution, 2);
        var s = StateOf(abilities, trainedSave: Save.Will, effects: ["cond.con.minus3"]);
        Assert.Equal(-1, Effects.Number(Rules, s, Hero, "constitution"));
        Assert.Equal(1, Effects.Number(Rules, s, Hero, "fortitude"));
    }

    // Review Focus 1: a stored +5 keeps its penalty, and the save reads it.
    [Fact]
    public void A_stored_plus_five_still_shows_its_penalty()
    {
        var s = StateOf(Zero.With(Ability.Strength, 5), trainedSave: Save.Will, effects: ["trait.str.plus2", "cond.str.minus2"]);
        Assert.Equal(3, Effects.Number(Rules, s, Hero, "strength"));
        Assert.Equal(3, Effects.Number(Rules, s, Hero, "fortitude"));
    }

    [Fact]
    public void Ability_difficulty_is_eight_plus_proficiency_plus_the_main_ability()
    {
        var s = StateOf(Zero.With(Ability.Wisdom, 3), level: 5, main: Ability.Wisdom);
        Assert.Equal(14, Effects.Number(Rules, s, Hero, "abilityDifficulty"));
    }

    // --- Skills ---

    [Theory]
    [InlineData(Skill.Athletics)]
    [InlineData(Skill.Acrobatics)]
    [InlineData(Skill.Stealth)]
    [InlineData(Skill.Mechanics)]
    [InlineData(Skill.Perception)]
    [InlineData(Skill.Intimidation)]
    [InlineData(Skill.Leadership)]
    [InlineData(Skill.Medicine)]
    [InlineData(Skill.Lore)]
    public void A_skill_is_its_ability_plus_proficiency_if_trained(Skill skill)
    {
        var abilities = new Abilities(1, 2, 3, 4, 5, -1);
        var s = StateOf(abilities, trained: [skill]);
        Assert.Equal(abilities[StatIds.AbilityOf(skill)] + 2, Effects.Number(Rules, s, Hero, StatIds.Number(skill)));
        foreach (var other in Enum.GetValues<Skill>())
        {
            if (other != skill)
            {
                Assert.Equal(abilities[StatIds.AbilityOf(other)], Effects.Number(Rules, s, Hero, StatIds.Number(other)));
            }
        }

        // Untrained, Stealth is just its ability.
        Assert.Equal(0, Effects.Number(Rules, StateOf(Zero), Hero, "stealth"));
    }

    [Fact]
    public void Passives_are_ten_plus_the_skill()
    {
        var trained = StateOf(Zero.With(Ability.Wisdom, 2), trained: [Skill.Perception]);
        Assert.Equal(14, Effects.Number(Rules, trained, Hero, "passivePerception"));
        Assert.Equal(10, Effects.Number(Rules, StateOf(Zero), Hero, "passiveStealth"));
        var geared = StateOf(Zero.With(Ability.Wisdom, 2), trained: [Skill.Perception], effects: ["gear.perception.plus5"]);
        Assert.Equal(19, Effects.Number(Rules, geared, Hero, "passivePerception"));
    }

    [Fact]
    public void Armour_class_unarmoured_is_ten_plus_dexterity()
    {
        var s = StateOf(Zero.With(Ability.Dexterity, 3));
        Assert.Equal(13, Effects.Number(Rules, s, Hero, "armourClass"));
        var slowed = StateOf(Zero.With(Ability.Dexterity, 3), effects: ["cond.dex.minus2"]);
        Assert.Equal(11, Effects.Number(Rules, slowed, Hero, "armourClass"));
    }

    [Fact]
    public void Initiative_is_intelligence() =>
        Assert.Equal(3, Effects.Number(Rules, StateOf(Zero.With(Ability.Intelligence, 3)), Hero, "initiative"));

    [Fact]
    public void Attack_has_base_zero() =>
        Assert.Equal(0, Effects.Number(Rules, StateOf(new Abilities(5, 5, 5, 5, 5, 5), level: 12), Hero, "attack"));

    [Fact]
    public void Medicine_rest_bonus_is_wisdom_plus_proficiency_only_if_trained()
    {
        var abilities = Zero.With(Ability.Wisdom, 2);
        Assert.Equal(5, Effects.Number(Rules, StateOf(abilities, level: 5, trained: [Skill.Medicine]), Hero, "medicineRestBonus"));
        Assert.Equal(0, Effects.Number(Rules, StateOf(abilities, level: 5), Hero, "medicineRestBonus"));
    }

    // --- The non-stacking rule on a stored ability ---

    [Theory]
    [InlineData(2, 0, "trait.str.plus2", "gear.str.plus1")]                 // trait +2 and gear +1: +2
    [InlineData(-2, 0, "gear.str.minus1", "gear.str.minus2")]               // penalties -1 and -2: -2
    [InlineData(3, 4, "trait.str.plus3", "gear.str.minus2")]                // base 4, +3 capped at 5, then -2
    public void Ability_non_stacking_across_kinds(long expected, int stored, params string[] effects) =>
        Assert.Equal(
            expected,
            Effects.Number(Rules, StateOf(Zero.With(Ability.Strength, stored), effects: effects), Hero, "strength"));

    // --- Several roll-mode sources (Review Focus 3) ---

    [Fact]
    public void Several_sources_fold_once()
    {
        var s = StateOf(Zero, effects: ["adv", "dis"], foe: true);
        s = Effects.Apply(s, "dis.against", Foe, new EffectSource(Foe));
        var sources = new[] { (Hero, "attackRoll"), (Foe, "attacksAgainst") };

        // Each side folded on its own would be normal and disadvantage; folded together it is one of each: normal.
        Assert.Equal(RollMode.Normal, Effects.RollModeOf(Rules, s, Hero, "attackRoll"));
        Assert.Equal(RollMode.Disadvantage, Effects.RollModeOf(Rules, s, Foe, "attacksAgainst"));
        Assert.Equal(RollMode.Normal, Effects.RollModeOf(Rules, s, sources));

        // Only the defender's disadvantage: disadvantage. Add the defender's advantage too: they cancel.
        var onlyDefender = Effects.Apply(StateOf(Zero, foe: true), "dis.against", Foe, new EffectSource(Foe));
        Assert.Equal(RollMode.Disadvantage, Effects.RollModeOf(Rules, onlyDefender, sources));
        var both = Effects.Apply(onlyDefender, "adv.against", Foe, new EffectSource(Foe));
        Assert.Equal(RollMode.Normal, Effects.RollModeOf(Rules, both, sources));
        Assert.Equal(RollMode.Normal, Effects.RollModeOf(Rules, StateOf(Zero, foe: true), sources));
    }

    [Fact]
    public void The_several_source_roll_mode_refuses_bad_sources()
    {
        var s = StateOf(Zero, foe: true);
        Assert.Throws<ArgumentException>(() => Effects.RollModeOf(Rules, s, Array.Empty<(UnitId, string)>()));
        Assert.Throws<ArgumentException>(() => Effects.RollModeOf(Rules, s, new[] { (Hero, "nope") }));
        Assert.Throws<ArgumentException>(() => Effects.RollModeOf(Rules, s, new[] { (new UnitId(9), "attackRoll") }));
        Assert.Throws<ArgumentNullException>(() => Effects.RollModeOf(Rules, s, (IEnumerable<(UnitId, string)>)null!));
    }

    // --- The tables ---

    [Fact]
    public void Every_roll_target_and_number_is_a_valid_modifier_target()
    {
        var modifiers = new List<string>();
        foreach (var row in StatNumbers.Rows)
        {
            modifiers.Add(Add(row.Id, 1));
        }

        foreach (var target in StatNumbers.RollTargets)
        {
            modifiers.Add($$"""{"target": "{{target}}", "rollMode": "advantage"}""");
        }

        var rules = RulesContext.Create(LoadEffects(Def("all", "gear", [.. modifiers])));
        var s = Effects.Apply(StateOf(Zero), "all", Hero, new EffectSource(Hero));
        Assert.Equal(RollMode.Advantage, Effects.RollModeOf(rules, s, Hero, "skillCheck"));
        Assert.Equal(RollMode.Advantage, Effects.RollModeOf(rules, s, Hero, "loreCheck"));
        Assert.Equal(StatNumbers.Rows.Length, StatNumbers.Rows.Select(r => r.Id).Distinct(StringComparer.Ordinal).Count());
    }

    // --- Helpers ---

    private static readonly Abilities Zero = new(0, 0, 0, 0, 0, 0);

    // The ring state with the hero holding a custom stat block, and the named effects applied to the hero.
    private static GameState StateOf(
        Abilities abilities,
        int level = 1,
        Ability main = Ability.Strength,
        Save trainedSave = Save.Fortitude,
        Skill[]? trained = null,
        string[]? effects = null,
        bool foe = false)
    {
        var stats = new UnitStats(
            level, abilities, main, trainedSave, [.. (trained ?? []).OrderBy(x => x)], HpTier.Slight, 0);
        var units = new List<Unit> { new(Hero, new GridPos(1, 1), Faction.Party, [], stats, FullHp) };
        if (foe)
        {
            units.Add(U(2, 3, 3));
        }

        var s = GameState.Create(GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####"), units, 7);
        foreach (var id in effects ?? [])
        {
            s = Effects.Apply(s, id, Hero, new EffectSource(Hero));
        }

        return s;
    }

    private static string Add(string target, int value) => $$"""{"target": "{{target}}", "add": {{value}}}""";

    private static string Def(string id, string kind, params string[] modifiers) =>
        $$"""{"id": "{{id}}", "kind": "{{kind}}", "modifiers": [{{string.Join(", ", modifiers)}}]}""";

    private static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile(EffectsFile, "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}

internal static class AbilitiesTestExtensions
{
    public static Abilities With(this Abilities a, Ability which, int value) => which switch
    {
        Ability.Strength => a with { Strength = value },
        Ability.Dexterity => a with { Dexterity = value },
        Ability.Constitution => a with { Constitution = value },
        Ability.Intelligence => a with { Intelligence = value },
        Ability.Wisdom => a with { Wisdom = value },
        _ => a with { Presence = value },
    };
}
