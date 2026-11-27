using System.Collections.Immutable;
using Dungeon.Core.Combat;
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
/// DH 20.3 step 4 (M1-10 Task 4, plan call 10; DH 10.4, 15.2, 21.4; TL 6.5): the out-of-combat and undetected-trap
/// floors for party units, the mixed-event rule, and the <see cref="DamageClamped"/> event, with Review Focus 2 and 3.
/// </summary>
public class FloorTests
{
    private static readonly UnitId Foe = new(2);
    private static readonly RulesContext Rules = ContestKit.Rules;

    // A slashing resistance for the hero (Review Focus 3's resisted variant).
    private static readonly RulesContext Resistant = RulesContext.Create(
        ContestKit.LoadEffects(
            ContestKit.Def("trait.resist-slashing", "trait", """{"target": "unit", "flag": "resistant.slashing"}""")));

    // The hero (party, maximum 17) on heroHp and a foe (enemy, table HP 12) on full HP, out of combat, seed 7.
    private static GameState Out(int heroHp) =>
        GameState.Create(
            GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####"),
            [
                new Unit(Hero, new GridPos(1, 1), Faction.Party, [], Fixtures.Stats, heroHp),
                new Unit(Foe, new GridPos(3, 3), Faction.Enemy, [], EnemyStats, 12),
            ],
            7,
            hero: Hero);

    // As Out, with a fight in progress: the hero's turn, round 1.
    private static GameState In(int heroHp) => Out(heroHp) with { Combat = new CombatState([Hero, Foe], 0, 1) };

    // --- The out-of-combat floor (DH 10.4) ---

    [Fact]
    public void Out_of_combat_a_party_unit_stops_on_one()
    {
        var s = Out(17);
        var roll = Fixed(Plain(DamageType.Slashing, 30));

        var outcome = Damage.Resolve(Rules, s, Hero, roll);
        Assert.Equal((30L, 17, 1, false, (ClampReason?)ClampReason.OutOfCombat),
            (outcome.Total, outcome.HpBefore, outcome.HpAfter, outcome.ReachedZero, outcome.Clamp));

        var (result, next) = Harm.Deal(Rules, s, Hero, roll);
        Assert.Equal(outcome with { Parts = [] }, result.Outcome with { Parts = [] });
        Assert.Equal<GameEvent>(
            [new DamageDealt(1, Hero, 30, 1), new DamageClamped(1, Hero, ClampReason.OutOfCombat)], result.Events.ToArray());
        Assert.Equal(1, next.FindUnit(Hero)!.Hp);
        Assert.Null(next.Ended);

        var foe = Damage.Resolve(Rules, s, Foe, roll);
        Assert.Equal((0, true, (ClampReason?)null), (foe.HpAfter, foe.ReachedZero, foe.Clamp));
    }

    // Damage exactly equal to HP is lethal, so the floor binds.
    [Fact]
    public void An_exactly_lethal_hit_is_floored()
    {
        var (outOfCombat, afterOut) = Harm.Deal(Rules, Out(5), Hero, Fixed(Plain(DamageType.Slashing, 5)));
        Assert.Equal((5L, 1, false, (ClampReason?)ClampReason.OutOfCombat),
            (outOfCombat.Outcome.Total, outOfCombat.Outcome.HpAfter, outOfCombat.Outcome.ReachedZero, outOfCombat.Outcome.Clamp));
        Assert.Equal(1, afterOut.FindUnit(Hero)!.Hp);
        Assert.Null(afterOut.Ended);

        var trapped = Damage.Resolve(Rules, In(5), Hero, Fixed(Trap(Detection.Hidden, 5)));
        Assert.Equal((1, false, (ClampReason?)ClampReason.UndetectedTrap), (trapped.HpAfter, trapped.ReachedZero, trapped.Clamp));
    }

    // Plan call 10: out of combat the out-of-combat floor is checked first, so a hidden trap there is clamped for it.
    [Fact]
    public void Out_of_combat_the_out_of_combat_floor_comes_before_the_trap_floor()
    {
        var outcome = Damage.Resolve(Rules, Out(5), Hero, Fixed(Trap(Detection.Hidden, 10)));
        Assert.Equal((1, (ClampReason?)ClampReason.OutOfCombat), (outcome.HpAfter, outcome.Clamp));
    }

    [Fact]
    public void A_floor_that_changes_nothing_is_not_a_clamp()
    {
        var (result, next) = Harm.Deal(Rules, Out(17), Hero, Fixed(Plain(DamageType.Slashing, 3)));
        Assert.Equal((14, (ClampReason?)null), (result.Outcome.HpAfter, result.Outcome.Clamp));
        Assert.Equal<GameEvent>([new DamageDealt(1, Hero, 3, 14)], result.Events.ToArray());
        Assert.Equal(14, next.FindUnit(Hero)!.Hp);

        var trapped = Damage.Resolve(Rules, In(10), Hero, Fixed(Trap(Detection.Hidden, 2)));
        Assert.Equal((8, (ClampReason?)null), (trapped.HpAfter, trapped.Clamp));
    }

    // --- The undetected-trap floor (DH 10.4, 15.2; TL 6.5) ---

    // Owner ruling 2: HP 5 + an undetected trap of 10 → HP 1, DamageClamped{undetectedTrap}, the save chain never reached.
    [Theory]
    [InlineData(Detection.Hidden)]
    [InlineData(Detection.HunchFloored)]
    public void The_reduced_DH_15_2_example(Detection detection)
    {
        var (result, next) = Harm.Deal(Rules, In(5), Hero, Fixed(Trap(detection, 10)));

        Assert.Equal((10L, 5, 1, false, (ClampReason?)ClampReason.UndetectedTrap),
            (result.Outcome.Total, result.Outcome.HpBefore, result.Outcome.HpAfter, result.Outcome.ReachedZero, result.Outcome.Clamp));
        Assert.Equal<GameEvent>(
            [new DamageDealt(1, Hero, 10, 1), new DamageClamped(1, Hero, ClampReason.UndetectedTrap)], result.Events.ToArray());
        Assert.Equal(1, next.FindUnit(Hero)!.Hp);
        Assert.Null(next.Ended);
    }

    [Theory]
    [InlineData(Detection.Revealed)]
    [InlineData(Detection.HunchWarned)]
    public void A_revealed_trap_is_ordinary_damage(Detection detection)
    {
        var outcome = Damage.Resolve(Rules, In(5), Hero, Fixed(Trap(detection, 10)));
        Assert.Equal((0, true, (ClampReason?)null), (outcome.HpAfter, outcome.ReachedZero, outcome.Clamp));
    }

    [Fact]
    public void The_floors_are_party_only()
    {
        var outcome = Damage.Resolve(Rules, In(5), Foe, Fixed(Trap(Detection.Hidden, 30)));
        Assert.Equal((0, true, (ClampReason?)null), (outcome.HpAfter, outcome.ReachedZero, outcome.Clamp));
    }

    // --- Review Focus 2: a floor never heals and never fires needlessly ---

    [Fact]
    public void Review_focus_2_a_floor_never_heals_and_never_fires_needlessly()
    {
        var (onOne, afterOne) = Harm.Deal(Rules, Out(1), Hero, Fixed(Plain(DamageType.Slashing, 5)));
        Assert.Equal((1, 1, (ClampReason?)ClampReason.OutOfCombat), (onOne.Outcome.HpBefore, onOne.Outcome.HpAfter, onOne.Outcome.Clamp));
        Assert.Equal(1, afterOne.FindUnit(Hero)!.Hp);

        var (onSeventeen, _) = Harm.Deal(Rules, Out(17), Hero, Fixed(Plain(DamageType.Slashing, 3)));
        Assert.Equal((14, (ClampReason?)null), (onSeventeen.Outcome.HpAfter, onSeventeen.Outcome.Clamp));

        var zero = Out(0);
        var (onZero, afterZero) = Harm.Deal(Rules, zero, Hero, Fixed(Plain(DamageType.Slashing, 5)));
        Assert.Equal((0, 0, (ClampReason?)null), (onZero.Outcome.HpBefore, onZero.Outcome.HpAfter, onZero.Outcome.Clamp));
        Assert.Equal<GameEvent>([new DamageDealt(1, Hero, 5, 0)], onZero.Events.ToArray());
        Assert.Null(afterZero.Ended);
        Assert.Equal(0, afterZero.FindUnit(Hero)!.Hp);
    }

    // --- Review Focus 3: a mixed trap event in combat (DH 10.4) ---

    // A hidden-trap part of raw 3 plus a slashing part of raw 4. On 4 HP the slashing alone reaches 0: no clamp, and
    // the hero dies. On 5 HP the event is clamped and HP is 1.
    [Fact]
    public void Review_focus_3_a_mixed_trap_event()
    {
        var roll = Fixed(Trap(Detection.Hidden, 3), Plain(DamageType.Slashing, 4));

        var (onFour, afterFour) = Harm.Deal(Rules, In(4), Hero, roll);
        Assert.Equal((7L, 0, true, (ClampReason?)null),
            (onFour.Outcome.Total, onFour.Outcome.HpAfter, onFour.Outcome.ReachedZero, onFour.Outcome.Clamp));
        Assert.Equal<GameEvent>(
            [new DamageDealt(1, Hero, 7, 0), new RunEnded(RunEndCause.HeroDeath, 1)], onFour.Events.ToArray());
        Assert.Equal(new RunEnd(RunEndCause.HeroDeath, 1), afterFour.Ended);

        var (onFive, afterFive) = Harm.Deal(Rules, In(5), Hero, roll);
        Assert.Equal((7L, 1, false, (ClampReason?)ClampReason.UndetectedTrap),
            (onFive.Outcome.Total, onFive.Outcome.HpAfter, onFive.Outcome.ReachedZero, onFive.Outcome.Clamp));
        Assert.Equal<GameEvent>(
            [new DamageDealt(1, Hero, 7, 1), new DamageClamped(1, Hero, ClampReason.UndetectedTrap)], onFive.Events.ToArray());
        Assert.Null(afterFive.Ended);
    }

    // The other parts are counted as dealt, after the ladder: a resisted slashing part of raw 8 deals 4, which alone does
    // not take 5 HP to 0, so the event is clamped even though its raw amount would.
    [Fact]
    public void Review_focus_3_the_other_parts_count_as_dealt()
    {
        var s = Effects.Apply(In(5), "trait.resist-slashing", Hero, new EffectSource(Hero));

        var outcome = Damage.Resolve(Resistant, s, Hero, Fixed(Trap(Detection.Hidden, 3), Plain(DamageType.Slashing, 8)));

        Assert.Equal((7L, 1, false, (ClampReason?)ClampReason.UndetectedTrap),
            (outcome.Total, outcome.HpAfter, outcome.ReachedZero, outcome.Clamp));
    }

    // --- DH 21.4: floors come before the save chain ---

    // Step 5 (the save chain, M2) runs only on a total that still reaches 0, so a floored event never reaches it: every
    // clamped outcome stops on 1 without reaching 0, and only where the unfloored result would have been below 1.
    [Fact]
    public void Floors_come_before_the_save_chain()
    {
        int clamped = 0;
        for (int seed = 1; seed <= 200; seed++)
        {
            var random = new System.Random(seed);
            int hp = random.Next(1, 31);
            var s = random.Next(2) == 0 ? Out(hp) : In(hp);
            int count = random.Next(1, 4);
            var parts = new PartRoll[count];
            for (int i = 0; i < count; i++)
            {
                int raw = random.Next(1, 16);
                parts[i] = random.Next(3) == 0
                    ? Trap(random.Next(2) == 0 ? Detection.Hidden : Detection.HunchFloored, raw)
                    : Plain((DamageType)random.Next(10), raw);
            }

            var outcome = Damage.Resolve(Rules, s, Hero, Fixed(parts));
            long from = Math.Min(hp, HitPoints.Max(Rules, s, Hero));

            // Out of combat a party unit from 1 or more never drops below 1, and a lethal hit is clamped for that floor.
            if (!s.CombatActive)
            {
                Assert.True(outcome.HpAfter >= 1, $"seed {seed}: out of combat the hero reached {outcome.HpAfter}");
                Assert.Equal(from - outcome.Total < 1 ? (ClampReason?)ClampReason.OutOfCombat : null, outcome.Clamp);
            }

            if (outcome.Clamp is not null)
            {
                clamped++;
                Assert.Equal(1, outcome.HpAfter);
                Assert.False(outcome.ReachedZero);
                Assert.True(from - outcome.Total < 1, $"seed {seed}: a clamp where the hit was not lethal");
            }
            else
            {
                Assert.Equal(Math.Max(0, from - outcome.Total), outcome.HpAfter);
            }
        }

        Assert.True(clamped > 0, "no seed produced a clamp");
    }

    // --- Helpers ---

    // A part with no dice and a fixed raw amount.
    private static PartRoll Plain(DamageType type, int raw) => new(new DamagePart(type, 0, 0, raw), [], raw);

    private static PartRoll Trap(Detection detection, int raw) =>
        new(new DamagePart(DamageType.Piercing, 0, 0, raw, new TrapSource("trap.dart", detection)), [], raw);

    private static DamageRoll Fixed(params PartRoll[] parts) => new([.. parts], false);
}
