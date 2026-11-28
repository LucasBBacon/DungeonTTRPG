using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The turn loop (M1-10 task 3; plan calls 4, 5, 6, 8, 9): combat start, turn start and turn end, EndTurn, moves in
/// combat, the out-of-combat tick resets (AB 6.8), and resuming a vacated turn.
/// </summary>
public class TurnTests
{
    private static readonly RulesContext Rules = RulesContext.Create(
        ContestKit.LoadEffects(ContestKit.Def("cond.slow", "condition", ContestKit.Add("speed", -2))),
        pools: PoolTests.FixturePools);

    // Two party units and an enemy (unit 3, table HP 12) on an open 9x3 map, seed 7, out of combat. The enemy keeps a
    // fight running past the settle loop's win check (M1-11 plan call 8).
    private static GameState Open() =>
        GameState.Create(
            GridMap.Parse(".........", ".........", "........."),
            [U(1, 0, 1), U(2, 4, 1), new Unit(new UnitId(3), new GridPos(8, 1), Faction.Enemy, [], EnemyStats, 12)],
            7);

    private static GameState InCombat() => Turns.StartCombat(Rules, Open()).Next;

    private static ImmutableArray<UnitId> OrderOf(GameState s) => s.Combat!.Order;

    private static StepResult Apply(GameState s, Command c) => Engine.Apply(s, c, Rules);

    private static void AssertRefused(GameState s, Command c, CommandError e)
    {
        var r = Apply(s, c);
        Assert.Equal(e, r.Error);
        Assert.Empty(r.Events);
        Assert.Same(s, r.State);
    }

    private static GameState WithBudget(GameState s, UnitId unit, TurnBudget budget)
    {
        var u = s.FindUnit(unit)!;
        return s with { Units = s.Units.SetItem(s.Units.IndexOf(u), u with { Budget = budget }) };
    }

    private static GameState Spend(GameState s, UnitId unit, string pool)
    {
        var (ok, next) = Pools.Spend(Rules, s, unit, pool, 1);
        Assert.True(ok);
        return next;
    }

    private static readonly TurnBudget Used = new(3, 1, true, true, true, true);

    [Fact]
    public void Starting_combat_rolls_initiative_and_starts_the_first_turn()
    {
        var s = Open();
        var (expected, _) = Initiative.Roll(Rules, s, s.Units.Select(u => u.Id));

        var (events, next) = Turns.StartCombat(Rules, s);

        Assert.True(next.CombatActive);
        Assert.Equal(1, next.Combat!.Round);
        Assert.Equal(0, next.Combat.Current);
        Assert.False(next.Combat.TurnVacated);
        Assert.True(next.Combat.Order.SequenceEqual(expected.Select(e => e.Unit)));
        Assert.Collection(
            events,
            e => Assert.True(Assert.IsType<CombatStarted>(e).Order.SequenceEqual(expected)),
            e => Assert.Equal(new TurnStarted(expected[0].Unit, 1), e));
        Assert.Equal(s.Rng.Combat.Counter + 3, next.Rng.Combat.Counter);
    }

    [Fact]
    public void Starting_combat_twice_after_a_run_end_or_with_no_units_throws()
    {
        var s = InCombat();
        Assert.Throws<InvalidOperationException>(() => Turns.StartCombat(Rules, s));
        var ended = Open() with { Ended = new RunEnd(RunEndCause.Victory, null) };
        Assert.Throws<InvalidOperationException>(() => Turns.StartCombat(Rules, ended));
        var empty = GameState.Create(GridMap.Parse("."), [], 7);
        Assert.Throws<ArgumentException>(() => Turns.StartCombat(Rules, empty));
    }

    [Fact]
    public void Combat_start_refills_fight_pools_and_freshens_every_budget()
    {
        var s = Open();
        foreach (var u in s.Units.Select(u => u.Id).ToArray())
        {
            s = Spend(Spend(Spend(s, u, "pool.fight"), u, "pool.rest"), u, "pool.turn");
            s = WithBudget(s, u, Used);
        }

        var next = Turns.StartCombat(Rules, s).Next;
        var first = next.Combat!.CurrentUnit;
        foreach (var u in next.Units)
        {
            Assert.Equal(default, u.Budget);
            Assert.Equal(0, Pools.Spent(next, u.Id, "pool.fight"));
            Assert.Equal(1, Pools.Spent(next, u.Id, "pool.rest"));

            // Only the first unit's turn has started, so only its turn pool has refilled.
            Assert.Equal(u.Id == first ? 0 : 1, Pools.Spent(next, u.Id, "pool.turn"));
        }
    }

    [Fact]
    public void End_turn_passes_to_the_next_unit_and_wraps_to_a_new_round()
    {
        var s = InCombat();
        var o = OrderOf(s);

        var r1 = Apply(s, new EndTurnCommand(o[0]));
        Assert.True(r1.Ok);
        Assert.Equal(new GameEvent[] { new TurnEnded(o[0]), new TurnStarted(o[1], 1) }, r1.Events);
        Assert.Equal((1, 1L), (r1.State.Combat!.Current, r1.State.Combat.Round));

        var r2 = Apply(r1.State, new EndTurnCommand(o[1]));
        Assert.Equal(new GameEvent[] { new TurnEnded(o[1]), new TurnStarted(o[2], 1) }, r2.Events);
        Assert.Equal((2, 1L), (r2.State.Combat!.Current, r2.State.Combat.Round));

        var r3 = Apply(r2.State, new EndTurnCommand(o[2]));
        Assert.Equal(new GameEvent[] { new TurnEnded(o[2]), new TurnStarted(o[0], 2) }, r3.Events);
        Assert.Equal((0, 2L), (r3.State.Combat!.Current, r3.State.Combat.Round));
        Assert.True(r3.State.Combat.Order.SequenceEqual(o));
        Assert.Equal(0, r3.State.Tick);
    }

    [Fact]
    public void Turn_start_freshens_the_units_budget_refills_its_turn_pools_and_clears_every_camp_flag()
    {
        var s = InCombat();
        var o = OrderOf(s);
        foreach (var u in o)
        {
            s = Spend(Spend(s, u, "pool.turn"), u, "pool.fight");
        }

        s = WithBudget(s, o[0], new TurnBudget(4, 0, false, false, false, true));
        s = WithBudget(s, o[1], Used);
        s = WithBudget(s, o[2], new TurnBudget(0, 0, false, true, false, true));

        var next = Apply(s, new EndTurnCommand(o[0])).State;

        Assert.Equal(default, next.FindUnit(o[1])!.Budget);
        Assert.Equal(0, Pools.Spent(next, o[1], "pool.turn"));
        Assert.Equal(1, Pools.Spent(next, o[1], "pool.fight"));

        // Other units: only the camp flag clears; their turn pools wait for their own turns.
        Assert.Equal(new TurnBudget(4, 0, false, false, false, false), next.FindUnit(o[0])!.Budget);
        Assert.Equal(new TurnBudget(0, 0, false, true, false, false), next.FindUnit(o[2])!.Budget);
        Assert.Equal(1, Pools.Spent(next, o[0], "pool.turn"));
        Assert.Equal(1, Pools.Spent(next, o[2], "pool.turn"));
    }

    [Fact]
    public void End_turn_is_refused_out_of_combat_and_out_of_turn()
    {
        AssertRefused(Open(), new EndTurnCommand(Hero), CommandError.NotInCombat);
        var s = InCombat();
        var o = OrderOf(s);
        AssertRefused(s, new EndTurnCommand(o[1]), CommandError.NotYourTurn);
        AssertRefused(s, new EndTurnCommand(o[2]), CommandError.NotYourTurn);
        AssertRefused(s, new EndTurnCommand(new UnitId(99)), CommandError.UnknownUnit);

        // A vacated turn is held by no one until it resumes.
        var vacated = s with { Combat = s.Combat! with { TurnVacated = true } };
        AssertRefused(vacated, new EndTurnCommand(o[0]), CommandError.NotYourTurn);
    }

    // Review Focus 5: after a run end with a decision pending, an end turn is refused like every other command.
    [Fact]
    public void End_turn_is_refused_after_a_run_end()
    {
        var atShrine = Engine.Apply(ShrineState(), new MoveCommand(Hero, Direction.E)).State;
        Assert.NotNull(atShrine.Pending);
        var ended = atShrine with
        {
            Combat = new CombatState([Hero], 0, 1),
            Ended = new RunEnd(RunEndCause.HeroDeath, 1),
            NextEventSeq = 2,
        };
        AssertRefused(ended, new EndTurnCommand(Hero), CommandError.RunEnded);
    }

    [Fact]
    public void Resume_starts_the_vacated_slot_or_wraps()
    {
        var s = Open() with { Combat = new CombatState([new(3), new(1), new(2)], 1, 2, TurnVacated: true) };
        s = WithBudget(s, Hero, Used);

        var (events, next) = Turns.ResumeIfVacated(Rules, s);
        Assert.Equal(new GameEvent[] { new TurnStarted(Hero, 2) }, events);
        Assert.Equal((1, 2L, false), (next.Combat!.Current, next.Combat.Round, next.Combat.TurnVacated));
        Assert.Equal(default, next.FindUnit(Hero)!.Budget);

        var atEnd = s with { Combat = s.Combat! with { Current = 3 } };
        (events, next) = Turns.ResumeIfVacated(Rules, atEnd);
        Assert.Equal(new GameEvent[] { new TurnStarted(new UnitId(3), 3) }, events);
        Assert.Equal((0, 3L, false), (next.Combat!.Current, next.Combat.Round, next.Combat.TurnVacated));

        foreach (var idle in new[]
        {
            s with { Combat = s.Combat! with { TurnVacated = false } },
            Open(),
            s with { Ended = new RunEnd(RunEndCause.Victory, null) },
            s with { Combat = new CombatState([], 0, 2, TurnVacated: true) },
        })
        {
            var (none, same) = Turns.ResumeIfVacated(Rules, idle);
            Assert.Empty(none);
            Assert.Same(idle, same);
        }
    }

    [Fact]
    public void A_move_in_combat_spends_movement_and_does_not_tick()
    {
        var s = InCombat();
        var o = OrderOf(s);
        var mover = o[0];
        Assert.Equal(6, Turns.MovementLeft(Rules, s, mover));

        for (int i = 0; i < 6; i++)
        {
            var from = s.FindUnit(mover)!.Pos;
            var r = Apply(s, new MoveCommand(mover, i % 2 == 0 ? Direction.N : Direction.S));
            Assert.True(r.Ok, $"move {i + 1} was refused with {r.Error}");
            Assert.Equal(new GameEvent[] { new UnitMoved(mover, from, r.State.FindUnit(mover)!.Pos) }, r.Events);
            Assert.Equal(0, r.State.Tick);
            s = r.State;
        }

        Assert.Equal(6, s.FindUnit(mover)!.Budget.MovementUsed);
        Assert.Equal(0, Turns.MovementLeft(Rules, s, mover));
        AssertRefused(s, new MoveCommand(mover, Direction.N), CommandError.NoMovementLeft);

        // Refused for want of movement before the square is checked: from row 0, north is off the map.
        var fresh = InCombat();
        var top = Apply(fresh, new MoveCommand(mover, Direction.N)).State;
        top = WithBudget(top, mover, new TurnBudget(6, 0, false, false, false, false));
        AssertRefused(top, new MoveCommand(mover, Direction.N), CommandError.NoMovementLeft);

        AssertRefused(fresh, new MoveCommand(o[1], Direction.N), CommandError.NotYourTurn);
        var vacated = fresh with { Combat = fresh.Combat! with { TurnVacated = true } };
        AssertRefused(vacated, new MoveCommand(o[0], Direction.N), CommandError.NotYourTurn);
    }

    [Fact]
    public void A_move_in_combat_off_the_map_is_still_refused_by_the_square()
    {
        var s = InCombat();
        var mover = OrderOf(s)[0];
        s = Apply(s, new MoveCommand(mover, Direction.N)).State;
        AssertRefused(s, new MoveCommand(mover, Direction.N), CommandError.OutOfBounds);
    }

    // Plan call 5: movement left is the folded speed minus movement used, so a penalty mid-turn takes effect at once.
    [Fact]
    public void A_speed_penalty_mid_turn_shrinks_the_movement_left()
    {
        var s = InCombat();
        var mover = OrderOf(s)[0];
        for (int i = 0; i < 3; i++)
        {
            s = Apply(s, new MoveCommand(mover, i % 2 == 0 ? Direction.N : Direction.S)).State;
        }

        Assert.Equal(3, Turns.MovementLeft(Rules, s, mover));
        s = Effects.Apply(s, "cond.slow", mover, new EffectSource(mover));
        Assert.Equal(1, Turns.MovementLeft(Rules, s, mover));

        var r = Apply(s, new MoveCommand(mover, Direction.S));
        Assert.True(r.Ok);
        AssertRefused(r.State, new MoveCommand(mover, Direction.N), CommandError.NoMovementLeft);

        // Never below zero: a bigger penalty than the movement left leaves 0.
        var slower = Effects.Apply(r.State, "cond.slow", mover, new EffectSource(new UnitId(2)));
        Assert.Equal(0, Turns.MovementLeft(Rules, slower, mover));
        var used = WithBudget(s, mover, new TurnBudget(9, 0, false, false, false, false));
        Assert.Equal(0, Turns.MovementLeft(Rules, used, mover));
    }

    // AB 6.8: out of combat each tick resets every unit's turn pools and turn budget, the camp flag included.
    [Fact]
    public void Out_of_combat_each_move_freshens_budgets_and_turn_pools()
    {
        var s = Open();
        foreach (var u in s.Units.Select(u => u.Id).ToArray())
        {
            s = WithBudget(Spend(Spend(s, u, "pool.turn"), u, "pool.fight"), u, Used);
        }

        var r = Apply(s, new MoveCommand(Hero, Direction.E));
        Assert.True(r.Ok);
        Assert.Equal(new GameEvent[] { new UnitMoved(Hero, new(0, 1), new(1, 1)), new TickAdvanced(1) }, r.Events);
        foreach (var u in r.State.Units)
        {
            Assert.Equal(default, u.Budget);
            Assert.Equal(0, Pools.Spent(r.State, u.Id, "pool.turn"));
            Assert.Equal(1, Pools.Spent(r.State, u.Id, "pool.fight"));
        }
    }

    [Fact]
    public void End_turn_round_trips_through_the_command_log()
    {
        Command[] commands = [new MoveCommand(Hero, Direction.E), new EndTurnCommand(new UnitId(2)), new AnswerDecisionCommand(new DecisionId(1), 0)];
        var log = new CommandLog(7, [.. commands]);
        string json = log.Save();
        Assert.Contains("{\"type\":\"endTurn\",\"unit\":2}", json);
        var loaded = CommandLog.Load(json);
        Assert.Equal(commands, loaded.Commands);
        Assert.Equal(json, loaded.Save());

        // The replay of an end turn under the same rules reaches the same state as applying it, the next unit's turn
        // pool refilled included.
        var s = InCombat();
        s = Spend(s, OrderOf(s)[1], "pool.turn");
        var end = new EndTurnCommand(OrderOf(s)[0]);
        var live = Apply(s, end).State;
        Assert.Equal(0, Pools.Spent(live, OrderOf(s)[1], "pool.turn"));
        Assert.Equal(Snapshot.Save(live), Snapshot.Save(Replay.Run(s, [end], Rules)));
    }

    // A command log reproduces a run only under the rules it was played with: without them a turn pool never refills,
    // and the replay diverges with no error.
    [Fact]
    public void A_replay_matches_the_live_run_only_under_its_rules()
    {
        var s = Spend(Open(), Hero, "pool.turn");
        var move = new MoveCommand(Hero, Direction.E);

        var live = Apply(s, move).State;
        Assert.Equal(0, Pools.Spent(live, Hero, "pool.turn"));
        Assert.Equal(Snapshot.Save(live), Snapshot.Save(Replay.Run(s, [move], Rules)));

        var bare = Replay.Run(s, [move]);
        Assert.Equal(1, Pools.Spent(bare, Hero, "pool.turn"));
        Assert.NotEqual(Snapshot.Save(live), Snapshot.Save(bare));
    }

    [Theory]
    [InlineData("{\"version\":9,\"seed\":7,\"commands\":[{\"type\":\"endTurn\"}]}")]
    [InlineData("{\"version\":9,\"seed\":7,\"commands\":[{\"type\":\"endTurn\",\"unit\":1,\"dir\":\"E\"}]}")]
    [InlineData("{\"version\":9,\"seed\":7,\"commands\":[{\"type\":\"endTurn\",\"unit\":1,\"decision\":1}]}")]
    [InlineData("{\"version\":9,\"seed\":7,\"commands\":[{\"type\":\"endTurn\",\"unit\":1,\"option\":0}]}")]
    public void An_end_turn_with_missing_or_foreign_fields_is_refused(string json) =>
        Assert.Throws<SnapshotFormatException>(() => CommandLog.Load(json));

    // Plan call 14: the subjects of the new rule events.
    [Fact]
    public void The_turn_rule_events_name_their_subjects()
    {
        var (events, _) = Turns.StartCombat(Rules, Open());
        var started = Assert.IsType<CombatStarted>(events[0]);
        RuleEvent Row(string id) => RuleEvents.Core.Single(e => e.Id == id);
        Assert.Equal(started.Order[0].Unit, Row("combatStarted").Subject(started));
        Assert.Equal(new UnitId(2), Row("turnStarted").Subject(new TurnStarted(new UnitId(2), 4)));
        Assert.Equal(new UnitId(3), Row("turnEnded").Subject(new TurnEnded(new UnitId(3))));
    }

    [Fact]
    public void The_turn_steps_throw_outside_a_live_turn()
    {
        Assert.Throws<InvalidOperationException>(() => Turns.StartTurn(Rules, Open()));
        Assert.Throws<InvalidOperationException>(() => Turns.Advance(Rules, Open()));
        var s = InCombat();
        var vacated = s with { Combat = s.Combat! with { TurnVacated = true } };
        Assert.Throws<InvalidOperationException>(() => Turns.StartTurn(Rules, vacated));
        Assert.Throws<InvalidOperationException>(() => Turns.Advance(Rules, vacated));
        var ended = s with { Ended = new RunEnd(RunEndCause.Victory, null) };
        Assert.Throws<InvalidOperationException>(() => Turns.Advance(Rules, ended));
        Assert.Throws<ArgumentException>(() => Turns.MovementLeft(Rules, s, new UnitId(99)));
    }
}
