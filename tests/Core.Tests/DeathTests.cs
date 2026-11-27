using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// DH 20.3 step 7's consequences through <see cref="Harm.Deal"/> (M1-10 Task 4; plan calls 1, 2, 11; owner ruling 3;
/// DH 10.1, 10.3): sequenced damage events, a unit at 0 removed with its effects and its turn-order slot, and the
/// hero's death ending the run, with Review Focus 1.
/// </summary>
public class DeathTests
{
    private static readonly RulesContext Rules = RulesContext.Create(
        ContestKit.LoadEffects(
            ContestKit.Def("cond.held", "condition", ContestKit.Add("attack", 1)),
            ContestKit.Def("cond.sourced", "condition", ContestKit.Add("attack", -1)),
            ContestKit.Def("cond.own", "condition", ContestKit.Add("speed", 1))));

    private static readonly UnitId Foe = new(2);
    private static readonly UnitId A = new(1);
    private static readonly UnitId B = new(2);
    private static readonly UnitId C = new(3);

    private static readonly DamageRoll Lethal = Fixed(DamageType.Slashing, 30);

    // The hero (1, party) at (1,1) and a foe (2, enemy, HP 12) at (3,1), out of combat, seed 7.
    private static GameState HeroAndFoe(int foeHp = 12) =>
        GameState.Create(
            GridMap.Parse(".....", ".....", "....."),
            [
                new Unit(Hero, new GridPos(1, 1), Faction.Party, [], Fixtures.Stats, FullHp),
                new Unit(Foe, new GridPos(3, 1), Faction.Enemy, [], EnemyStats, foeHp),
            ],
            7,
            hero: Hero);

    // A (party, no hero), B and C (enemies) in combat with order [A, B, C], the given slot current, round 3.
    private static GameState ThreeInCombat(int current) =>
        GameState.Create(
            GridMap.Parse(".....", ".....", "....."),
            [
                new Unit(A, new GridPos(0, 1), Faction.Party, [], Fixtures.Stats, FullHp),
                new Unit(B, new GridPos(2, 1), Faction.Enemy, [], EnemyStats, 12),
                new Unit(C, new GridPos(4, 1), Faction.Enemy, [], EnemyStats, 12),
            ],
            7) with
        { Combat = new CombatState([A, B, C], current, 3) };

    [Fact]
    public void A_monster_at_zero_is_removed_with_its_effects()
    {
        var s = HeroAndFoe() with { Combat = new CombatState([Hero, Foe], 0, 1) };
        s = Effects.Apply(s, "cond.held", Foe, EffectSource.Environment);
        s = Effects.Apply(s, "cond.sourced", Hero, new EffectSource(Foe));
        s = Effects.Apply(s, "cond.own", Hero, new EffectSource(Hero));
        var pos = s.FindUnit(Foe)!.Pos;

        var (result, next) = Harm.Deal(Rules, s, Foe, Lethal);

        Assert.Equal(1, result.Seq);
        Assert.Equal<GameEvent>([new DamageDealt(1, Foe, 30, 0), new UnitDied(Foe, pos)], result.Events.ToArray());
        Assert.Null(next.FindUnit(Foe));
        Assert.Null(next.UnitAt(pos));
        Assert.Equal(["cond.own"], next.Effects.Select(e => e.EffectId).ToArray());
        Assert.Equal(s.NextEffectSeq, next.NextEffectSeq);
        Assert.Equal(2, next.NextEventSeq);

        // The foe held a later slot than the current one: the turn is the hero's still. Harm.Deal never ends combat.
        Assert.True(next.Combat!.Order.SequenceEqual([Hero]));
        Assert.Equal((0, 1L, false), (next.Combat.Current, next.Combat.Round, next.Combat.TurnVacated));
        Assert.Null(next.Ended);
    }

    [Fact]
    public void Each_damage_event_takes_the_next_sequence_id()
    {
        var (first, afterFirst) = Harm.Deal(Rules, HeroAndFoe(), Foe, Fixed(DamageType.Slashing, 5));
        var (second, afterSecond) = Harm.Deal(Rules, afterFirst, Foe, Fixed(DamageType.Slashing, 5));

        Assert.Equal((1L, 2L), (first.Seq, second.Seq));
        Assert.Equal<GameEvent>([new DamageDealt(1, Foe, 5, 7)], first.Events.ToArray());
        Assert.Equal<GameEvent>([new DamageDealt(2, Foe, 5, 2)], second.Events.ToArray());
        Assert.Equal(3, afterSecond.NextEventSeq);
    }

    // Review Focus 1: the current unit dies on its own turn. No TurnEnded or TurnStarted; the next turn starts only
    // when the caller resumes it.
    [Fact]
    public void Review_focus_1_the_current_unit_dies_on_its_turn()
    {
        var s = ThreeInCombat(1);
        var bPos = s.FindUnit(B)!.Pos;

        var (result, next) = Harm.Deal(Rules, s, B, Lethal);

        Assert.Equal<GameEvent>([new DamageDealt(1, B, 30, 0), new UnitDied(B, bPos)], result.Events.ToArray());
        Assert.True(next.Combat!.Order.SequenceEqual([A, C]));
        Assert.Equal((1, 3L, true), (next.Combat.Current, next.Combat.Round, next.Combat.TurnVacated));

        var (resumed, after) = Turns.ResumeIfVacated(Rules, next);
        Assert.Equal<GameEvent>([new TurnStarted(C, 3)], resumed.ToArray());
        Assert.Equal((1, 3L, false), (after.Combat!.Current, after.Combat.Round, after.Combat.TurnVacated));
    }

    [Fact]
    public void Review_focus_1_the_current_unit_dies_holding_the_last_slot()
    {
        var s = ThreeInCombat(2);

        var (result, next) = Harm.Deal(Rules, s, C, Lethal);

        Assert.Equal<GameEvent>([new DamageDealt(1, C, 30, 0), new UnitDied(C, new GridPos(4, 1))], result.Events.ToArray());
        Assert.True(next.Combat!.Order.SequenceEqual([A, B]));
        Assert.Equal((2, 3L, true), (next.Combat.Current, next.Combat.Round, next.Combat.TurnVacated));

        var (resumed, after) = Turns.ResumeIfVacated(Rules, next);
        Assert.Equal<GameEvent>([new TurnStarted(A, 4)], resumed.ToArray());
        Assert.Equal((0, 4L, false), (after.Combat!.Current, after.Combat.Round, after.Combat.TurnVacated));
    }

    // With no hero, a party unit at 0 is removed like a monster (plan call 2).
    [Fact]
    public void A_death_in_an_earlier_slot_keeps_the_current_turn()
    {
        var s = ThreeInCombat(2);

        var (result, next) = Harm.Deal(Rules, s, A, Lethal);

        Assert.Equal<GameEvent>([new DamageDealt(1, A, 30, 0), new UnitDied(A, new GridPos(0, 1))], result.Events.ToArray());
        Assert.DoesNotContain(result.Events, e => e is TurnStarted or TurnEnded);
        Assert.True(next.Combat!.Order.SequenceEqual([B, C]));
        Assert.Equal((1, 3L, false), (next.Combat.Current, next.Combat.Round, next.Combat.TurnVacated));
        Assert.Equal(C, next.Combat.CurrentUnit);
    }

    [Fact]
    public void Damage_to_a_unit_already_on_zero_has_no_consequence()
    {
        var s = HeroAndFoe(foeHp: 0) with { Combat = new CombatState([Hero, Foe], 1, 1) };

        var (result, next) = Harm.Deal(Rules, s, Foe, Fixed(DamageType.Slashing, 5));

        Assert.Equal<GameEvent>([new DamageDealt(1, Foe, 5, 0)], result.Events.ToArray());
        Assert.NotNull(next.FindUnit(Foe));
        Assert.True(next.Combat!.Order.SequenceEqual([Hero, Foe]));
        Assert.Equal((1, false), (next.Combat.Current, next.Combat.TurnVacated));
    }

    [Fact]
    public void The_hero_at_zero_ends_the_run()
    {
        var s = HeroAndFoe() with { Combat = new CombatState([Foe, Hero], 0, 1) };

        var (result, next) = Harm.Deal(Rules, s, Hero, Lethal);

        Assert.Equal(1, result.Seq);
        Assert.Equal<GameEvent>(
            [new DamageDealt(1, Hero, 30, 0), new RunEnded(RunEndCause.HeroDeath, 1)], result.Events.ToArray());
        Assert.Equal(new RunEnd(RunEndCause.HeroDeath, 1), next.Ended);
        Assert.Equal(0, next.FindUnit(Hero)!.Hp);
        Assert.True(next.Combat!.Order.SequenceEqual([Foe, Hero]));
        Assert.Equal(2, next.NextEventSeq);
    }

    [Fact]
    public void Harm_on_an_ended_run_throws()
    {
        var ended = HeroAndFoe() with { Ended = new RunEnd(RunEndCause.HeroDeath, 1), NextEventSeq = 2 };
        Assert.Throws<InvalidOperationException>(() => Harm.Deal(Rules, ended, Foe, Lethal));
    }

    [Fact]
    public void Harm_on_an_unknown_unit_throws() =>
        Assert.Throws<ArgumentException>(() => Harm.Deal(Rules, HeroAndFoe(), new UnitId(9), Lethal));

    // --- Helpers ---

    // One part with no dice and a fixed raw amount.
    private static DamageRoll Fixed(DamageType type, int raw) =>
        new([new PartRoll(new DamagePart(type, 0, 0, raw), [], raw)], false);
}
