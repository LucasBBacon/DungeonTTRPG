using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// These classes share their names with their namespaces, so name them explicitly inside this namespace.
using Damage = Dungeon.Core.Damage.Damage;
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The damage function (M1-09 Task 4): dice on <c>combat</c> (plan call 8), crits rolling dice twice (DH 9), parts
/// resolved separately through the ladder and summed (SM 8.3, 9; owner ruling 1), HP lowered never below 0 (DH 20.3),
/// with Review Focus 1 and 3.
/// </summary>
public class DamageFunctionTests
{
    private const string EffectsFile = "effects/effects.json";

    private static readonly UnitId Foe = new(2);

    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("trait.resist-fire", "trait", Flag("resistant.fire")),
            Def("trait.immune-slashing", "trait", Flag("immune.slashing"))));

    // --- Dice ---

    [Fact]
    public void Die_faces_cover_one_to_sides()
    {
        var stream = RingState().Rng.Combat;   // seed 7
        var seen = new int[8];
        for (int i = 0; i < 2000; i++)
        {
            var (face, next) = Dice.Die(stream, 6);
            Assert.InRange(face, 1, 6);
            Assert.Equal(stream.Counter + 1, next.Counter);
            seen[face]++;
            stream = next;
        }

        Assert.All(Enumerable.Range(1, 6), f => Assert.True(seen[f] > 0, $"face {f} never appeared"));
    }

    [Fact]
    public void A_die_needs_at_least_one_side() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Dice.Die(RingState().Rng.Combat, 0));

    // --- Rolling (plan call 8) ---

    [Fact]
    public void A_normal_roll_draws_one_die_per_dice()
    {
        var s = RingState();
        var (roll, next) = Damage.Roll(s, [Part(DamageType.Slashing, 2, 6), Part(DamageType.Fire, 1, 4)], false);

        Assert.Equal(s.Rng.Combat.Counter + 3, next.Rng.Combat.Counter);
        Assert.Equal(s.Rng.Shrine, next.Rng.Shrine);
        Assert.Equal(Snapshot.Save(s with { Rng = s.Rng with { Combat = next.Rng.Combat } }), Snapshot.Save(next));
        Assert.False(roll.Crit);
        Assert.Equal([2, 1], roll.Parts.Select(p => p.Faces.Length).ToArray());
        Assert.All(roll.Parts, p => Assert.Equal(p.Faces.Sum() + p.Part.Flat, p.Raw));
    }

    // DH 9: "A crit rolls all damage dice twice"; flat terms do not double. SRD 5.2.1 p. 16 agrees (D-02).
    [Fact]
    public void A_crit_draws_every_part_twice_and_adds_flat_once()
    {
        var s = RingState();
        var part = Part(DamageType.Slashing, 1, 6, 3);

        var (crit, critNext) = Damage.Roll(s, [part], true);
        var (normal, _) = Damage.Roll(s, [part], false);

        Assert.True(crit.Crit);
        Assert.Equal(s.Rng.Combat.Counter + 2, critNext.Rng.Combat.Counter);
        var faces = crit.Parts[0].Faces;
        Assert.Equal(2, faces.Length);
        Assert.Equal(faces[0] + faces[1] + 3, crit.Parts[0].Raw);
        Assert.Equal(normal.Parts[0].Faces[0], faces[0]);   // same draw order
    }

    // Plan call 8 makes the draw order a contract: parts in list order, each part's dice in order, one combat draw per die.
    // The expected faces come from drawing Dice.Die straight off the combat stream, independent of Damage.Roll.
    private static (int[][] Faces, DrawStream End) DrawExpected(DrawStream stream, params (int Sides, int Count)[] perPart)
    {
        var all = new int[perPart.Length][];
        for (int p = 0; p < perPart.Length; p++)
        {
            all[p] = new int[perPart[p].Count];
            for (int d = 0; d < perPart[p].Count; d++)
            {
                var (face, next) = Dice.Die(stream, perPart[p].Sides);
                all[p][d] = face;
                stream = next;
            }
        }

        return (all, stream);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Parts_draw_in_list_order_with_the_negative_flat_kept(bool crit)
    {
        var s = RingState();
        int m = crit ? 2 : 1;
        var (roll, next) = Damage.Roll(s, [Part(DamageType.Slashing, 1, 6, 3), Part(DamageType.Fire, 1, 4, -5)], crit);
        var (expected, end) = DrawExpected(s.Rng.Combat, (6, m), (4, m));

        Assert.Equal(expected[0], roll.Parts[0].Faces.ToArray());
        Assert.Equal(expected[1], roll.Parts[1].Faces.ToArray());
        Assert.Equal(expected[0].Sum() + 3, roll.Parts[0].Raw);
        Assert.Equal(expected[1].Sum() - 5, roll.Parts[1].Raw);
        Assert.Equal(end, next.Rng.Combat);
    }

    [Fact]
    public void A_part_with_no_dice_draws_nothing()
    {
        var s = RingState();
        var (outcome, next) = Damage.Deal(Rules, s, Hero, [Part(DamageType.Bludgeoning, 0, 0, 4)], false);

        Assert.Equal(s.Rng.Combat, next.Rng.Combat);
        Assert.Equal(4, outcome.Total);
        Assert.Equal(FullHp - 4, next.FindUnit(Hero)!.Hp);
    }

    [Theory]
    [InlineData(-1, 6)]
    [InlineData(2, 0)]
    public void An_invalid_part_is_rejected(int dice, int sides)
    {
        var parts = ImmutableArray.Create(Part(DamageType.Fire, 1, 6), new DamagePart(DamageType.Cold, dice, sides, 0));
        var e = Assert.Throws<ArgumentException>(() => Damage.Roll(RingState(), parts, false));
        Assert.Contains("index 1", e.Message);
    }

    // --- Resolving (SM 8.3, 9; DH 20.3) ---

    [Fact]
    public void Parts_resolve_separately_and_sum()
    {
        var s = With("trait.resist-fire");
        var roll = Fixed(Rolled(DamageType.Slashing, 5), Rolled(DamageType.Fire, 6));

        var (outcome, next) = Damage.Apply(Rules, s, Hero, roll);

        Assert.Equal(
            [new PartOutcome(DamageType.Slashing, LadderStep.Normal, 5, 5), new PartOutcome(DamageType.Fire, LadderStep.Resistant, 6, 3)],
            outcome.Parts.ToArray());
        Assert.Equal(8, outcome.Total);
        Assert.Equal((17, 9, false), (outcome.HpBefore, outcome.HpAfter, outcome.ReachedZero));
        Assert.Equal(9, next.FindUnit(Hero)!.Hp);
    }

    // Owner ruling 1: the minimum of 1 applies to each part.
    [Fact]
    public void Each_part_deals_at_least_one()
    {
        var outcome = Damage.Resolve(
            Rules, With("trait.resist-fire"), Hero, Fixed(Rolled(DamageType.Fire, 1), Rolled(DamageType.Fire, 1)));

        Assert.All(outcome.Parts, p => Assert.Equal((LadderStep.Resistant, 1L), (p.Step, p.Dealt)));
        Assert.Equal(2, outcome.Total);
    }

    // Review Focus 1: 1d4 - 5 rolling 2 deals 1, not 0 or -3; against an immune target it deals 0.
    [Fact]
    public void A_flat_penalty_below_zero_still_deals_one_unless_immune()
    {
        var part = new PartRoll(Part(DamageType.Slashing, 1, 4, -5), [2], -3);

        var (plain, plainNext) = Damage.Apply(Rules, RingState(), Hero, Fixed(part));
        Assert.Equal(new PartOutcome(DamageType.Slashing, LadderStep.Normal, -3, 1), plain.Parts[0]);
        Assert.Equal(FullHp - 1, plainNext.FindUnit(Hero)!.Hp);

        var (immune, immuneNext) = Damage.Apply(Rules, With("trait.immune-slashing"), Hero, Fixed(part));
        Assert.Equal(new PartOutcome(DamageType.Slashing, LadderStep.Immune, -3, 0), immune.Parts[0]);
        Assert.Equal((0L, FullHp, FullHp, false), (immune.Total, immune.HpBefore, immune.HpAfter, immune.ReachedZero));
        Assert.Equal(FullHp, immuneNext.FindUnit(Hero)!.Hp);
    }

    // Review Focus 3: overkill leaves 0 and reports ReachedZero; a second hit still leaves 0 and reports its total.
    [Fact]
    public void Overkill_leaves_zero_and_repeat_damage_stays_at_zero()
    {
        var s = RingState(new Unit(Foe, new GridPos(3, 3), Faction.Enemy, [], EnemyStats, 12));
        var roll = Fixed(Rolled(DamageType.Bludgeoning, 30));

        var (first, afterFirst) = Damage.Apply(Rules, s, Foe, roll);
        Assert.Equal((30L, 12, 0, true), (first.Total, first.HpBefore, first.HpAfter, first.ReachedZero));
        Assert.Equal(0, afterFirst.FindUnit(Foe)!.Hp);

        var (second, afterSecond) = Damage.Apply(Rules, afterFirst, Foe, roll);
        Assert.Equal((30L, 0, 0, true), (second.Total, second.HpBefore, second.HpAfter, second.ReachedZero));
        Assert.Equal(0, afterSecond.FindUnit(Foe)!.Hp);
    }

    [Fact]
    public void Resolve_is_pure()
    {
        var s = With("trait.resist-fire");
        var before = Snapshot.Save(s);
        var roll = Fixed(Rolled(DamageType.Fire, 9), Rolled(DamageType.Cold, 2));

        var outcome = Damage.Resolve(Rules, s, Hero, roll);
        Assert.Equal(before, Snapshot.Save(s));

        var (applied, next) = Damage.Apply(Rules, s, Hero, roll);
        Assert.Equal(outcome with { Parts = [] }, applied with { Parts = [] });
        Assert.Equal(outcome.Parts.ToArray(), applied.Parts.ToArray());
        var hero = s.FindUnit(Hero)!;
        Assert.Equal(
            Snapshot.Save(s with { Units = s.Units.Replace(hero, hero with { Hp = outcome.HpAfter }) }),
            Snapshot.Save(next));
        Assert.Equal(FullHp - 6, outcome.HpAfter);
    }

    [Fact]
    public void An_unknown_target_is_rejected()
    {
        var roll = Fixed(Rolled(DamageType.Fire, 3));
        Assert.Throws<ArgumentException>(() => Damage.Resolve(Rules, RingState(), new UnitId(9), roll));
        Assert.Throws<ArgumentException>(() => Damage.Apply(Rules, RingState(), new UnitId(9), roll));
    }

    // --- Helpers ---

    private static DamagePart Part(DamageType type, int dice, int sides, int flat = 0) => new(type, dice, sides, flat);

    // A one-die part with a fixed face and no flat.
    private static PartRoll Rolled(DamageType type, int face) => new(Part(type, 1, face), [face], face);

    private static DamageRoll Fixed(params PartRoll[] parts) => new([.. parts], false);

    private static GameState With(params string[] effects)
    {
        var s = RingState();
        foreach (var id in effects)
        {
            s = Effects.Apply(s, id, Hero, new EffectSource(Hero));
        }

        return s;
    }

    private static string Flag(string name) => $$"""{"target": "unit", "flag": "{{name}}"}""";

    private static string Def(string id, string kind, params string[] modifiers) =>
        $$"""{"id": "{{id}}", "kind": "{{kind}}", "modifiers": [{{string.Join(", ", modifiers)}}]}""";

    private static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile(EffectsFile, "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}
