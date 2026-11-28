using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Damage = Dungeon.Core.Damage.Damage;

/// <summary>
/// M1-10's Done-when scenarios (Task 5; DH 10.3, 10.4, 20.5): fixture scripts that call the core functions in the order
/// M1-11's commands will, as the roadmap's fixture rule allows. The fixture opener, the fixture death burst and the
/// trap that starts combat stand in for content and trigger dispatch that later steps add.
/// </summary>
public class CombatScenarioTests
{
    private static readonly UnitId Foe = new(2);
    private static readonly RulesContext Rules = ContestKit.Rules;

    // The hero (1, party, HP 17) at (1,1) and a foe (2, enemy, HP 12) at (3,1), out of combat (exploration), seed 7.
    private static GameState Exploring() =>
        GameState.Create(
            GridMap.Parse(".....", ".....", "....."),
            [
                new Unit(Hero, new GridPos(1, 1), Faction.Party, [], Fixtures.Stats, FullHp),
                new Unit(Foe, new GridPos(3, 1), Faction.Enemy, [], EnemyStats, 12),
            ],
            7,
            hero: Hero);

    // DH 10.3 (Done-when): the hero dies in the same exchange that kills the last enemy. The hero's death wins: the run
    // ends, and combat end never runs.
    [Fact]
    public void The_hero_dies_alongside_the_last_enemy()
    {
        var s = Exploring() with { Combat = new CombatState([Hero, Foe], 0, 1) };
        var events = ImmutableArray.CreateBuilder<GameEvent>();

        var (kill, afterKill) = Harm.Deal(Rules, s, Foe, Fixed(Plain(DamageType.Slashing, 30)));
        events.AddRange(kill.Events);
        Assert.DoesNotContain(afterKill.Units, u => u.Faction == Faction.Enemy);

        // The fixture death burst: a plain second Harm.Deal on the hero, after the last enemy's lethal one and before
        // EndIfWon. It stands in for an on-death trigger; nothing dispatches triggers here.
        var (burst, afterBurst) = Harm.Deal(Rules, afterKill, Hero, Fixed(Plain(DamageType.Fire, FullHp + 10)));
        events.AddRange(burst.Events);

        // In combat, and no trap part: not floored.
        Assert.Null(burst.Outcome.Clamp);
        Assert.Equal((0, true), (burst.Outcome.HpAfter, burst.Outcome.ReachedZero));
        Assert.Equal(2, burst.Seq);

        var (ending, final) = CombatEnd.EndIfWon(Rules, afterBurst);
        events.AddRange(ending);

        Assert.Empty(ending);
        Assert.Same(afterBurst, final);
        Assert.Equal(new RunEnded(RunEndCause.HeroDeath, burst.Seq), events[^1]);
        Assert.DoesNotContain(events, e => e is CombatEnded);
        Assert.Equal(new RunEnd(RunEndCause.HeroDeath, 2), final.Ended);
        Assert.True(final.CombatActive);

        var refused = Engine.Apply(final, new MoveCommand(Hero, Direction.E), Rules);
        Assert.Equal(CommandError.RunEnded, refused.Error);
        Assert.Empty(refused.Events);
        Assert.Same(final, refused.State);
    }

    // The ambush (owner ruling 3): the hero (1,1) on 1 HP, and the foe beside it (2,1) holding the fixture sure strike.
    private static GameState Ambush() =>
        GameState.Create(
            GridMap.Parse(".....", ".....", "....."),
            [
                new Unit(Hero, new GridPos(1, 1), Faction.Party, [], Fixtures.Stats, 1),
                AbilityFixtures.Foe(2, 2, 1),
            ],
            7,
            hero: Hero);

    // DH 10.4 (Done-when): the foe's opener runs combat start before its strike (M1-11 Task 6), so the strike is
    // in-combat damage: no floor, and the lethal strike ends the run before any turn starts.
    [Fact]
    public void An_opening_ambush_strike_is_in_combat_damage()
    {
        var r = Engine.Apply(
            Ambush(), new UseAbilityCommand(Foe, AbilityFixtures.Sure, new UnitsTarget([Hero])), AbilityFixtures.Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.IsType<CombatStarted>(r.Events[0]);
        var dealt = Assert.Single(r.Events.OfType<DamageDealt>());
        Assert.Equal((Hero, 0), (dealt.Target, dealt.HpAfter));
        Assert.DoesNotContain(r.Events, e => e is DamageClamped);
        Assert.Equal(new RunEnded(RunEndCause.HeroDeath, dealt.Seq), r.Events[^1]);
        Assert.DoesNotContain(r.Events, e => e is TurnStarted);
        Assert.Equal(new RunEnd(RunEndCause.HeroDeath, dealt.Seq), r.State.Ended);
        Assert.True(r.State.CombatActive);
    }

    // The control case: the same strike with no combat start is out-of-combat damage, and the floor holds.
    [Fact]
    public void Without_combat_start_the_same_strike_is_floored()
    {
        var (strike, next) = Harm.Deal(Rules, Exploring(), Hero, Fixed(Trap(Detection.Revealed, 30)));

        Assert.Equal((1, (ClampReason?)ClampReason.OutOfCombat), (strike.Outcome.HpAfter, strike.Outcome.Clamp));
        Assert.Equal<GameEvent>(
            [new DamageDealt(1, Hero, 30, 1), new DamageClamped(1, Hero, ClampReason.OutOfCombat)], strike.Events.ToArray());
        Assert.Equal(1, next.FindUnit(Hero)!.Hp);
        Assert.Null(next.Ended);
    }

    // An undetected trap that starts combat is still floored: the trap floor holds in or out of combat (DH 10.4).
    [Fact]
    public void An_undetected_trap_that_starts_combat_is_floored()
    {
        var (_, s) = Turns.StartCombat(Rules, Exploring());

        var (trap, next) = Harm.Deal(Rules, s, Hero, Fixed(Trap(Detection.Hidden, 30)));

        Assert.Equal((1, (ClampReason?)ClampReason.UndetectedTrap), (trap.Outcome.HpAfter, trap.Outcome.Clamp));
        Assert.Equal<GameEvent>(
            [new DamageDealt(1, Hero, 30, 1), new DamageClamped(1, Hero, ClampReason.UndetectedTrap)], trap.Events.ToArray());
        Assert.Equal(1, next.FindUnit(Hero)!.Hp);
        Assert.Null(next.Ended);
        Assert.True(next.CombatActive);
    }

    // --- A scripted fight replays identically ---

    // 1d4 + 1 piercing: at most 2 × 4 + 1 = 9 on a crit (DH 9 doubles the dice, not the flat).
    private static readonly ImmutableArray<DamagePart> Dagger = [new DamagePart(DamageType.Piercing, 1, 4, 1)];

    private const int FightTurns = 6;

    // The hero: level 3, Strength +3, HP tier Sturdy, so maximum 11 + 3 × 8 = 35. The foe: an enemy on table HP 40.
    // Each unit takes at most three hits in three rounds, at most 3 × 9 = 27, so neither can reach 0.
    private static GameState FightStart()
    {
        var hero = new UnitStats(
            3, ContestKit.Zero.With(Ability.Strength, 3).With(Ability.Wisdom, 1), Ability.Strength, Save.Will,
            [Skill.Athletics], HpTier.Sturdy, 0);
        var foe = new UnitStats(
            2, ContestKit.Zero.With(Ability.Dexterity, 1).With(Ability.Strength, 2), Ability.Strength, Save.Reflex,
            [], HpTier.Table, 40);
        var s = GameState.Create(
            GridMap.Parse(".......", ".......", "......."),
            [
                new Unit(Hero, new GridPos(2, 1), Faction.Party, [], hero, 35),
                new Unit(Foe, new GridPos(4, 1), Faction.Enemy, [], foe, 40),
            ],
            7,
            hero: Hero);
        Assert.Equal((35L, 40L), (HitPoints.Max(Rules, s, Hero), HitPoints.Max(Rules, s, Foe)));
        return Turns.StartCombat(Rules, s).Next;
    }

    // count turns: the current unit attacks the other, a hit deals the dagger through Harm.Deal, and the attacker ends
    // its turn through the engine. A guard stops the script if the run ended or a unit is gone (the HP choice means it
    // should not).
    private static GameState Fight(GameState s, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (s.Ended is not null || s.Units.Length < 2)
            {
                break;
            }

            var attacker = s.Combat!.CurrentUnit;
            var defender = attacker == Hero ? Foe : Hero;
            var (attack, next) = Contests.Attack(Rules, s, attacker, defender, new AttackSpec(Ability.Strength, true));
            if (attack.Hit)
            {
                var (roll, rolled) = Damage.Roll(next, Dagger, attack.Crit);
                next = Harm.Deal(Rules, rolled, defender, roll).Next;
            }

            var ended = Engine.Apply(next, new EndTurnCommand(attacker), Rules);
            Assert.True(ended.Ok, $"turn {i}: EndTurn refused with {ended.Error}");
            s = ended.State;
        }

        return s;
    }

    // Combat end after the script: a no-op, since the foe survives.
    private static GameState Finish(GameState s)
    {
        var (events, next) = CombatEnd.EndIfWon(Rules, s);
        Assert.Empty(events);
        Assert.Same(s, next);
        return next;
    }

    // Pinned from the first green run on seed 7 (a golden, not a hand-derived value): the hero's and the foe's HP and
    // NextEventSeq after six turns. A change here means the draw order, the turn loop or the damage arithmetic moved.
    private static readonly (int Hero, int Foe, long NextEventSeq) Seed7Final = (29, 31, 6);

    [Fact]
    public void A_scripted_fight_replays_identically()
    {
        var a = Finish(Fight(FightStart(), FightTurns));
        var b = Finish(Fight(FightStart(), FightTurns));
        Assert.Equal(Snapshot.Save(a), Snapshot.Save(b));

        // A save and load after round 1 (two turns).
        var mid = Fight(FightStart(), 2);
        Assert.Equal(2L, mid.Combat!.Round);
        var split = Finish(Fight(Snapshot.Load(Snapshot.Save(mid)), FightTurns - 2));
        Assert.Equal(Snapshot.Save(a), Snapshot.Save(split));

        Assert.Null(a.Ended);
        Assert.Equal(2, a.Units.Length);
        Assert.Equal(4L, a.Combat!.Round);
        Assert.Equal(Seed7Final, (a.FindUnit(Hero)!.Hp, a.FindUnit(Foe)!.Hp, a.NextEventSeq));
    }

    // --- Helpers ---

    // A part with no dice and a fixed raw amount.
    private static PartRoll Plain(DamageType type, int raw) => new(new DamagePart(type, 0, 0, raw), [], raw);

    private static PartRoll Trap(Detection detection, int raw) =>
        new(new DamagePart(DamageType.Piercing, 0, 0, raw, new TrapSource("trap.dart", detection)), [], raw);

    private static DamageRoll Fixed(params PartRoll[] parts) => new([.. parts], false);
}
