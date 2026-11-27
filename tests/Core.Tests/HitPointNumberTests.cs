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
/// Max HP and Recovery as fold queries (M1-09 Task 2): the DH 7.1 and DH 8 reference tables, the per-level minimum on
/// folded Constitution (Review Focus 5), table HP, and the Recovery floor.
/// </summary>
public class HitPointNumberTests
{
    private const string EffectsFile = "effects/effects.json";

    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("gear.con.plus1", "gear", Add("constitution", 1)),
            Def("cond.con.minus4", "condition", Add("constitution", -4))));

    // DH 7.1: Max HP = 11 + level × max(1, classHp + Con).
    [Theory]
    // Reference Slight, Con +1.
    [InlineData(HpTier.Slight, 1, 1, 18)]
    [InlineData(HpTier.Slight, 1, 2, 25)]
    [InlineData(HpTier.Slight, 1, 3, 32)]
    [InlineData(HpTier.Slight, 1, 5, 46)]
    [InlineData(HpTier.Slight, 1, 7, 60)]
    [InlineData(HpTier.Slight, 1, 10, 81)]
    // Lowest legal Slight, Con -1.
    [InlineData(HpTier.Slight, -1, 1, 16)]
    [InlineData(HpTier.Slight, -1, 2, 21)]
    [InlineData(HpTier.Slight, -1, 3, 26)]
    [InlineData(HpTier.Slight, -1, 5, 36)]
    [InlineData(HpTier.Slight, -1, 7, 46)]
    [InlineData(HpTier.Slight, -1, 10, 61)]
    // Sound, Con +2.
    [InlineData(HpTier.Sound, 2, 1, 20)]
    [InlineData(HpTier.Sound, 2, 2, 29)]
    [InlineData(HpTier.Sound, 2, 3, 38)]
    [InlineData(HpTier.Sound, 2, 5, 56)]
    [InlineData(HpTier.Sound, 2, 7, 74)]
    [InlineData(HpTier.Sound, 2, 10, 101)]
    // Sturdy, with the Constitution each row implies.
    [InlineData(HpTier.Sturdy, 3, 1, 22)]
    [InlineData(HpTier.Sturdy, 3, 2, 33)]
    [InlineData(HpTier.Sturdy, 3, 3, 44)]
    [InlineData(HpTier.Sturdy, 4, 5, 71)]
    [InlineData(HpTier.Sturdy, 4, 7, 95)]
    [InlineData(HpTier.Sturdy, 5, 10, 141)]
    public void The_DH_7_1_reference_table_is_reproduced(HpTier tier, int con, int level, long max) =>
        Assert.Equal(max, HitPoints.Max(Rules, StateOf(Block(level, con, tier)), Hero));

    // DH 8: Recovery = 4 + 2 × level + Con, minimum 1.
    [Theory]
    // Reference, Con +1.
    [InlineData(1, 1, 7)]
    [InlineData(1, 3, 11)]
    [InlineData(1, 5, 15)]
    [InlineData(1, 7, 19)]
    [InlineData(1, 10, 25)]
    // Con -1.
    [InlineData(-1, 1, 5)]
    [InlineData(-1, 3, 9)]
    [InlineData(-1, 5, 13)]
    [InlineData(-1, 7, 17)]
    [InlineData(-1, 10, 23)]
    // Con max: +3 at levels 1-3, +4 at 5-7, +5 at 10.
    [InlineData(3, 1, 9)]
    [InlineData(3, 3, 13)]
    [InlineData(4, 5, 18)]
    [InlineData(4, 7, 22)]
    [InlineData(5, 10, 29)]
    public void The_DH_8_recovery_table_is_reproduced(int con, int level, long recovery) =>
        Assert.Equal(recovery, HitPoints.Recovery(Rules, StateOf(Block(level, con, HpTier.Slight)), Hero));

    // DH 8: a rest with Medicine heals Recovery + Wisdom + proficiency (Wisdom +2, trained, Con +1).
    [Theory]
    [InlineData(1, 11)]
    [InlineData(3, 15)]
    [InlineData(5, 20)]
    [InlineData(7, 24)]
    [InlineData(10, 31)]
    public void Rest_with_Medicine_matches_DH_8(int level, long expected)
    {
        var stats = Block(level, 1, HpTier.Slight) with
        {
            Abilities = new Abilities(0, 0, 1, 0, 2, 0),
            TrainedSkills = [Skill.Medicine],
        };
        var s = StateOf(stats);
        Assert.Equal(expected, HitPoints.Recovery(Rules, s, Hero) + Effects.Number(Rules, s, Hero, "medicineRestBonus"));
    }

    // Review Focus 5: max(1, 6 + (-3 - 4)) is 1 per level, so 11 + 3 × 1, not 11 + 3 × (-1).
    [Fact]
    public void The_per_level_minimum_reads_folded_constitution()
    {
        var s = StateOf(Block(3, -3, HpTier.Slight), "cond.con.minus4");
        Assert.Equal(-7, Effects.Number(Rules, s, Hero, "constitution"));
        Assert.Equal(14, HitPoints.Max(Rules, s, Hero));
    }

    [Fact]
    public void An_effect_on_constitution_moves_max_hp_and_recovery()
    {
        var stats = Block(3, 1, HpTier.Slight);
        Assert.Equal(32, HitPoints.Max(Rules, StateOf(stats), Hero));
        Assert.Equal(11, HitPoints.Recovery(Rules, StateOf(stats), Hero));
        var geared = StateOf(stats, "gear.con.plus1");
        Assert.Equal(35, HitPoints.Max(Rules, geared, Hero));
        Assert.Equal(12, HitPoints.Recovery(Rules, geared, Hero));
    }

    // DH 7.1: "Monsters keep their table HP (enemies 6.2); no bonus."
    [Fact]
    public void Table_hp_ignores_level_and_constitution()
    {
        var stats = Block(3, 4, HpTier.Table) with { TableHp = 12 };
        var unit = new Unit(Hero, new GridPos(1, 1), Faction.Enemy, [], stats, 12);
        var s = GameState.Create(Room, [unit], 7);
        Assert.Equal(12, HitPoints.Max(Rules, s, Hero));
    }

    // Plan call 4: a level-0 tiered unit has the adventurer bonus only.
    [Fact]
    public void A_level_zero_party_unit_has_maximum_eleven() =>
        Assert.Equal(11, HitPoints.Max(Rules, StateOf(Block(0, 2, HpTier.Sturdy)), Hero));

    // DH 8: "minimum 1". 4 + 0 + (-3 - 4) = -3, floored to 1.
    [Fact]
    public void Recovery_has_a_floor_of_one() =>
        Assert.Equal(1, HitPoints.Recovery(Rules, StateOf(Block(0, -3, HpTier.Slight), "cond.con.minus4"), Hero));

    // DH 8: "Recovery reads neither the class HP tier nor Scarred."
    [Fact]
    public void Recovery_reads_no_hp_tier()
    {
        long slight = HitPoints.Recovery(Rules, StateOf(Block(3, 1, HpTier.Slight)), Hero);
        long sturdy = HitPoints.Recovery(Rules, StateOf(Block(3, 1, HpTier.Sturdy)), Hero);
        Assert.Equal(11, slight);
        Assert.Equal(slight, sturdy);
    }

    // Fixtures.FullHp is a literal; it must stay the derived maximum of Fixtures.Stats.
    [Fact]
    public void The_fixture_full_hp_is_the_derived_maximum() =>
        Assert.Equal(FullHp, HitPoints.Max(RulesContext.Empty, RingState(), Hero));

    // --- Helpers ---

    private static readonly GridMap Room = GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####");

    // A stat block: the given level, Constitution and HP tier, every other ability 0, no skills.
    private static UnitStats Block(int level, int con, HpTier tier) =>
        new(level, new Abilities(0, 0, con, 0, 0, 0), Ability.Strength, Save.Fortitude, ImmutableArray<Skill>.Empty, tier, 0);

    // The hero on the given stat block, at 1 HP (Create does not check HP against the maximum), with effects applied.
    private static GameState StateOf(UnitStats stats, params string[] effects)
    {
        var s = GameState.Create(Room, [new Unit(Hero, new GridPos(1, 1), Faction.Party, [], stats, 1)], 7);
        foreach (var id in effects)
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
