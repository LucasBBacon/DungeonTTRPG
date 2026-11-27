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
/// Healing and the clamp to the maximum (M1-09 Task 4, plan call 10; DH 7.3, 8), with Review Focus 4.
/// </summary>
public class HealingTests
{
    private const string EffectsFile = "effects/effects.json";

    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(Def("cond.con.minus2", "condition", Add("constitution", -2))));

    [Fact]
    public void Heal_stops_at_the_maximum()
    {
        var s = StateOf(Fixtures.Stats, 10);

        var (five, afterFive) = HitPoints.Heal(Rules, s, Hero, 5);
        Assert.Equal(new HealOutcome(Hero, 5, 5, 0, 15), five);
        Assert.Equal(15, afterFive.FindUnit(Hero)!.Hp);

        var (nine, afterNine) = HitPoints.Heal(Rules, s, Hero, 9);
        Assert.Equal(new HealOutcome(Hero, 9, 7, 2, 17), nine);
        Assert.Equal(17, afterNine.FindUnit(Hero)!.Hp);
    }

    // Review Focus 4: 17 of 17, Constitution -2 through an effect: maximum 11 + 1 × (6 - 2) = 15. ClampToMax sets 15;
    // healing 5 then gives 15 with overheal 5. Healing before the clamp also stops at the lowered maximum.
    [Fact]
    public void Heal_and_clamp_read_a_lowered_maximum()
    {
        var lowered = Effects.Apply(StateOf(Fixtures.Stats, FullHp), "cond.con.minus2", Hero, new EffectSource(Hero));
        Assert.Equal(15, HitPoints.Max(Rules, lowered, Hero));

        var clamped = HitPoints.ClampToMax(Rules, lowered, Hero);
        Assert.Equal(15, clamped.FindUnit(Hero)!.Hp);
        var hero = lowered.FindUnit(Hero)!;
        Assert.Equal(
            Snapshot.Save(lowered with { Units = lowered.Units.Replace(hero, hero with { Hp = 15 }) }),
            Snapshot.Save(clamped));

        var (healed, afterHeal) = HitPoints.Heal(Rules, clamped, Hero, 5);
        Assert.Equal(new HealOutcome(Hero, 5, 0, 5, 15), healed);
        Assert.Equal(15, afterHeal.FindUnit(Hero)!.Hp);

        var (unclamped, afterUnclamped) = HitPoints.Heal(Rules, lowered, Hero, 5);
        Assert.Equal(new HealOutcome(Hero, 5, 0, 5, 15), unclamped);
        Assert.Equal(15, afterUnclamped.FindUnit(Hero)!.Hp);
    }

    [Fact]
    public void ClampToMax_leaves_a_unit_at_or_below_the_maximum_alone()
    {
        var s = StateOf(Fixtures.Stats, 10);
        Assert.Same(s, HitPoints.ClampToMax(Rules, s, Hero));
    }

    [Fact]
    public void A_negative_heal_is_rejected() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => HitPoints.Heal(Rules, StateOf(Fixtures.Stats, 10), Hero, -1));

    [Fact]
    public void An_unknown_target_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => HitPoints.Heal(Rules, StateOf(Fixtures.Stats, 10), new UnitId(9), 1));
        Assert.Throws<ArgumentException>(() => HitPoints.ClampToMax(Rules, StateOf(Fixtures.Stats, 10), new UnitId(9)));
    }

    // DH 8: the level-1 reference hero (Slight, Con +1) has maximum 18 and Recovery 4 + 2 + 1 = 7; 5 + 7 = 12.
    [Fact]
    public void Heal_with_Recovery_heals_DH_8_amounts()
    {
        var s = StateOf(Fixtures.Stats with { Abilities = new Abilities(0, 0, 1, 0, 0, 0) }, 5);
        Assert.Equal(18, HitPoints.Max(Rules, s, Hero));

        var (outcome, next) = HitPoints.Heal(Rules, s, Hero, HitPoints.Recovery(Rules, s, Hero));

        Assert.Equal(new HealOutcome(Hero, 7, 7, 0, 12), outcome);
        Assert.Equal(12, next.FindUnit(Hero)!.Hp);
    }

    // --- Helpers ---

    private static GameState StateOf(UnitStats stats, int hp) =>
        GameState.Create(
            GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####"),
            [new Unit(Hero, new GridPos(1, 1), Faction.Party, [], stats, hp)],
            7);

    private static string Add(string target, int value) => $$"""{"target": "{{target}}", "add": {{value}}}""";

    private static string Def(string id, string kind, params string[] modifiers) =>
        $$"""{"id": "{{id}}", "kind": "{{kind}}", "modifiers": [{{string.Join(", ", modifiers)}}]}""";

    private static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile(EffectsFile, "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}
