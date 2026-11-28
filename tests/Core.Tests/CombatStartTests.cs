using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// The split combat start (M1-11 Task 1; plan calls 6, 7, 16; M1-10 rulings record 5, steps 6 to 9): the
/// before-round-1 marker, the opener mark and the first-turn rule, removal and a win under the marker, and save
/// version 8.
/// </summary>
public class CombatStartTests
{
    private static readonly RulesContext Rules = RulesContext.Create(
        ContestKit.LoadEffects(),
        pools: [.. PoolTests.FixturePools, new PoolDefinition("pool.turn2", RefillTag.Turn, _ => 1)]);

    private static readonly UnitId A = new(1);
    private static readonly UnitId B = new(2);
    private static readonly UnitId C = new(3);

    private static readonly DamageRoll Lethal =
        new([new PartRoll(new DamagePart(DamageType.Slashing, 0, 0, 30), [], 30)], false);

    // The opener's charge: one action, and one of pool.turn.
    private static readonly OpenerCharge Charge =
        new(new TurnBudget(0, 1, false, false, false, false), [new PoolSpend("pool.turn", 1)]);

    private static Unit Foe(int id, int x, int y) => new(new UnitId(id), new(x, y), Faction.Enemy, [], EnemyStats, 12);

    // A and B (party) and C (enemy) on an open 9x3 map, seed 7, out of combat.
    private static GameState Open() =>
        GameState.Create(
            GridMap.Parse(".........", ".........", "........."),
            [U(1, 0, 1), U(2, 4, 1), Foe(3, 8, 1)],
            7);

    // Open, recorded as a fight held before round 1, with the given order.
    private static GameState Held(params UnitId[] order) =>
        Open() with { Combat = new CombatState([.. order], 0, 1, BeforeRound1: true) };

    private static GameState WithUnit(GameState s, Unit u) =>
        s with { Units = s.Units.SetItem(s.Units.IndexOf(s.FindUnit(u.Id)!), u) };

    private static GameState Spend(GameState s, UnitId unit, string pool)
    {
        var (ok, next) = Pools.Spend(Rules, s, unit, pool, 1);
        Assert.True(ok, $"spending {pool} was refused");
        return next;
    }

    // The striker pays for the opener by hand (an action and pool.turn), also spends pool.camp, 1 movement and its bonus
    // action, and is then marked.
    private static GameState Struck(GameState s, UnitId striker) => Turns.MarkOpener(Paid(s, striker), striker, Charge);

    private static GameState Paid(GameState s, UnitId striker)
    {
        var u = s.FindUnit(striker)!;
        s = WithUnit(s, u with { Budget = u.Budget with { ActionsUsed = 1, MovementUsed = 1, BonusUsed = true } });
        s = Spend(s, striker, "pool.turn");
        return Spend(s, striker, "pool.camp");
    }

    [Fact]
    public void StartCombat_keeps_its_events_and_dice()
    {
        var s = Open();
        var (expected, _) = Initiative.Roll(Rules, s, s.Units.Select(u => u.Id));

        var (events, next) = Turns.StartCombat(Rules, s);

        Assert.Collection(
            events,
            e => Assert.True(Assert.IsType<CombatStarted>(e).Order.SequenceEqual(expected)),
            e => Assert.Equal(new TurnStarted(expected[0].Unit, 1), e));
        Assert.Equal(s.Rng.Combat.Counter + 3, next.Rng.Combat.Counter);
        Assert.False(next.Combat!.BeforeRound1);
        Assert.Equal((0, 1L, false), (next.Combat.Current, next.Combat.Round, next.Combat.TurnVacated));
    }

    [Fact]
    public void BeginCombat_leaves_round_one_unstarted()
    {
        var s = Open();
        var used = new TurnBudget(3, 1, true, true, true, true);
        foreach (var u in s.Units)
        {
            s = WithUnit(s, u with { Budget = used });
        }

        var (expected, _) = Initiative.Roll(Rules, s, s.Units.Select(u => u.Id));

        var (events, next) = Turns.BeginCombat(Rules, s);

        var started = Assert.IsType<CombatStarted>(Assert.Single(events));
        Assert.True(started.Order.SequenceEqual(expected));
        Assert.True(next.Combat!.BeforeRound1);
        Assert.Equal((0, 1L, false), (next.Combat.Current, next.Combat.Round, next.Combat.TurnVacated));
        Assert.True(next.Combat.Order.SequenceEqual(expected.Select(e => e.Unit)));
        Assert.All(next.Units, u => Assert.Equal(default, u.Budget));
        Assert.All(next.Units, u => Assert.Null(u.Opener));
        Assert.Equal(s.Rng.Combat.Counter + 3, next.Rng.Combat.Counter);
    }

    [Fact]
    public void Fresh_budgets_come_before_initiative()
    {
        var s = Spend(Spend(Open(), A, "pool.fight"), C, "pool.fight");
        var (expected, _) = Initiative.Roll(Rules, s, s.Units.Select(u => u.Id));

        var (_, next) = Turns.BeginCombat(Rules, s);

        Assert.Equal(0, Pools.Spent(next, A, "pool.fight"));
        Assert.Equal(0, Pools.Spent(next, C, "pool.fight"));
        Assert.True(next.Combat!.Order.SequenceEqual(expected.Select(e => e.Unit)));
    }

    [Fact]
    public void BeginCombat_throws_as_StartCombat_does_and_BeginRound1_needs_the_marker()
    {
        var s = Open();
        var fight = Turns.StartCombat(Rules, s).Next;
        Assert.Throws<InvalidOperationException>(() => Turns.BeginCombat(Rules, fight));
        Assert.Throws<InvalidOperationException>(
            () => Turns.BeginCombat(Rules, s with { Ended = new RunEnd(RunEndCause.Victory, null) }));
        Assert.Throws<ArgumentException>(() => Turns.BeginCombat(Rules, s with { Units = [] }));

        Assert.Throws<InvalidOperationException>(() => Turns.BeginRound1(Rules, s));
        Assert.Throws<InvalidOperationException>(() => Turns.BeginRound1(Rules, fight));
        var held = Turns.BeginCombat(Rules, s).Next;
        Assert.Throws<InvalidOperationException>(
            () => Turns.BeginRound1(Rules, held with { Ended = new RunEnd(RunEndCause.Victory, null) }));

        var (events, round1) = Turns.BeginRound1(Rules, held);
        Assert.Equal<GameEvent>([new TurnStarted(held.Combat!.Order[0], 1)], events.ToArray());
        Assert.False(round1.Combat!.BeforeRound1);
        Assert.Equal(Snapshot.Save(fight), Snapshot.Save(round1));
    }

    [Fact]
    public void Under_the_marker_moves_end_turn_and_StartTurn_are_refused()
    {
        var s = Held(A, B, C);

        foreach (var command in new Command[] { new MoveCommand(A, Direction.E), new EndTurnCommand(A) })
        {
            var r = Engine.Apply(s, command, Rules);
            Assert.Equal(CommandError.NotYourTurn, r.Error);
            Assert.Empty(r.Events);
            Assert.Same(s, r.State);
        }

        Assert.Throws<InvalidOperationException>(() => Turns.Advance(Rules, s));
        Assert.Throws<InvalidOperationException>(() => Turns.StartTurn(Rules, s));
    }

    [Fact]
    public void MarkOpener_needs_the_marker_and_the_paid_charge()
    {
        var s = Held(A, B, C);
        Assert.Throws<InvalidOperationException>(() => Turns.MarkOpener(Paid(s, A) with { Combat = null }, A, Charge));
        Assert.Throws<InvalidOperationException>(
            () => Turns.MarkOpener(Paid(s, A) with { Combat = s.Combat! with { BeforeRound1 = false } }, A, Charge));

        // Nothing paid: neither the action nor pool.turn.
        Assert.Throws<InvalidOperationException>(() => Turns.MarkOpener(s, A, Charge));

        // The action paid, pool.turn not.
        var acted = WithUnit(s, s.FindUnit(A)! with { Budget = Charge.Charged });
        Assert.Throws<InvalidOperationException>(() => Turns.MarkOpener(acted, A, Charge));

        var marked = Turns.MarkOpener(Spend(acted, A, "pool.turn"), A, Charge);
        Assert.Equal(Charge, marked.FindUnit(A)!.Opener);

        // At most one opener per side.
        var other = WithUnit(marked, marked.FindUnit(B)! with { Budget = Charge.Charged });
        Assert.Throws<InvalidOperationException>(() => Turns.MarkOpener(Spend(other, B, "pool.turn"), B, Charge));
    }

    [Fact]
    public void The_first_turn_rule()
    {
        // The striker in slot 0.
        var s = Struck(Held(A, B, C), A);
        var (events, first) = Turns.BeginRound1(Rules, s);
        Assert.Equal<GameEvent>([new TurnStarted(A, 1)], events.ToArray());
        AssertFirstTurn(first, A);

        // The striker in slot 2: slot 0's turn start clears its camp flag but not its charge or mark.
        s = Struck(Held(B, C, A), A);
        (events, var round1) = Turns.BeginRound1(Rules, s);
        Assert.Equal<GameEvent>([new TurnStarted(B, 1)], events.ToArray());
        var waiting = round1.FindUnit(A)!;
        Assert.False(waiting.Budget.CampSpent);
        Assert.Equal((1, 1, true), (waiting.Budget.ActionsUsed, waiting.Budget.MovementUsed, waiting.Budget.BonusUsed));
        Assert.Equal(Charge, waiting.Opener);

        var (_, second) = Turns.Advance(Rules, round1);
        Assert.Equal(Charge, second.FindUnit(A)!.Opener);
        (events, var third) = Turns.Advance(Rules, second);
        Assert.Equal<GameEvent>([new TurnEnded(C), new TurnStarted(A, 1)], events.ToArray());
        AssertFirstTurn(third, A);
    }

    private static void AssertFirstTurn(GameState s, UnitId striker)
    {
        var u = s.FindUnit(striker)!;
        Assert.Equal(1, u.Budget.ActionsUsed);
        Assert.Equal(0, u.Budget.MovementUsed);
        Assert.False(u.Budget.BonusUsed);
        Assert.False(u.Budget.CampSpent);
        Assert.Equal(1, Pools.Spent(s, striker, "pool.turn"));
        Assert.Equal(1, Pools.Spent(s, striker, "pool.camp"));
        Assert.Null(u.Opener);
    }

    [Fact]
    public void A_reaction_spend_between_the_opener_and_the_first_turn_still_refills()
    {
        var s = Spend(Struck(Held(A, B, C), A), A, "pool.turn2");
        Assert.Equal(1, Pools.Spent(s, A, "pool.turn2"));

        var (_, next) = Turns.BeginRound1(Rules, s);

        Assert.Equal(0, Pools.Spent(next, A, "pool.turn2"));
        Assert.Equal(1, Pools.Spent(next, A, "pool.turn"));
    }

    [Fact]
    public void Removing_slot_zero_before_round_one_promotes_the_next_unit()
    {
        var foes = GameState.Create(
            GridMap.Parse(".........", ".........", "........."),
            [U(1, 0, 1), U(2, 4, 1), Foe(3, 8, 1), Foe(4, 6, 0)],
            7) with
        { Combat = new CombatState([C, A, B, new UnitId(4)], 0, 1, BeforeRound1: true) };

        var (harm, next) = Harm.Deal(Rules, foes, C, Lethal);

        Assert.Contains(new UnitDied(C, new GridPos(8, 1)), harm.Events);
        Assert.Equal([A, B, new UnitId(4)], next.Combat!.Order.ToArray());
        Assert.Equal((0, false, true), (next.Combat.Current, next.Combat.TurnVacated, next.Combat.BeforeRound1));

        var (events, round1) = Turns.BeginRound1(Rules, next);
        Assert.Equal<GameEvent>([new TurnStarted(A, 1)], events.ToArray());
        Assert.Equal((0, 1L), (round1.Combat!.Current, round1.Combat.Round));
    }

    [Fact]
    public void A_win_before_round_one_emits_no_turn_events()
    {
        var s = Struck(Held(A, C, B), A);
        var (_, dead) = Harm.Deal(Rules, s, C, Lethal);
        Assert.Equal(Charge, dead.FindUnit(A)!.Opener);

        var (events, next) = CombatEnd.EndIfWon(Rules, dead);

        Assert.Equal<GameEvent>([new CombatEnded(CombatEndReason.Win)], events.ToArray());
        Assert.Null(next.Combat);
        Assert.All(next.Units, u => Assert.Null(u.Opener));
        Assert.All(next.Units, u => Assert.Equal(default, u.Budget));
    }

    [Fact]
    public void A_win_after_round_one_starts_clears_a_waiting_mark()
    {
        var s = Struck(Held(B, C, A), A);
        var (_, round1) = Turns.BeginRound1(Rules, s);
        var (_, dead) = Harm.Deal(Rules, round1, C, Lethal);

        var (events, next) = CombatEnd.EndIfWon(Rules, dead);

        Assert.Equal<GameEvent>([new TurnEnded(B), new CombatEnded(CombatEndReason.Win)], events.ToArray());
        Assert.All(next.Units, u => Assert.Null(u.Opener));
    }

    // --- Save version 8 ---

    // A (party) stepped onto the shrine at (1,0) and holds its decision; B (party) and C (enemy) stand by. The fight is
    // held before round 1 with order [C, A, B], and B struck the opener.
    private static GameState Saved()
    {
        var open = GameState.Create(
            GridMap.Parse(".*.......", ".........", "........."),
            [U(1, 0, 0), U(2, 4, 1), Foe(3, 8, 1)],
            7);
        var atShrine = Engine.Apply(open, new MoveCommand(A, Direction.E), Rules).State;
        Assert.NotNull(atShrine.Pending);
        var held = atShrine with { Combat = new CombatState([C, A, B], 0, 1, BeforeRound1: true) };
        return Struck(held, B);
    }

    [Fact]
    public void Snapshot_v8_round_trips_the_marker_and_the_mark()
    {
        var s = Saved();
        var json = Snapshot.Save(s);
        Assert.StartsWith("{\"version\":8,", json);
        Assert.Contains("\"turnVacated\":false,\"beforeRound1\":true}", json);
        Assert.Contains(
            "\"opener\":{\"charged\":{\"movementUsed\":0,\"actionsUsed\":1,\"bonusUsed\":false,\"reactionUsed\":false,"
            + "\"freeUsed\":false,\"campSpent\":false},\"turnPools\":[{\"pool\":\"pool.turn\",\"spent\":1}]}",
            json);
        Assert.Contains("\"opener\":null", json);

        var loaded = Snapshot.Load(json);

        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal(StateDigest.Of(s), StateDigest.Of(loaded));
        Assert.True(loaded.Combat!.BeforeRound1);
        Assert.Equal(Charge.Charged, loaded.FindUnit(B)!.Opener!.Charged);
        Assert.Equal(Charge.TurnPools.ToArray(), loaded.FindUnit(B)!.Opener!.TurnPools.ToArray());
        Assert.Null(loaded.FindUnit(A)!.Opener);
    }

    [Fact]
    public void A_v7_save_is_refused()
    {
        var v7 = Snapshot.Save(ShrineState()).Replace("\"version\":8", "\"version\":7");
        Assert.Equal(7, Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(v7)).Found);
    }

    [Fact]
    public void A_save_missing_the_marker_or_the_opener_is_a_format_error()
    {
        var json = Snapshot.Save(Saved());
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(json.Replace(",\"beforeRound1\":true", "")));
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(json.Replace(",\"opener\":null", "")));
    }

    public static TheoryData<string> BadStates => new()
    {
        "marker in round 2",
        "marker with current 1",
        "marker with a vacated turn",
        "marker with nothing pending and no run end",
        "two marks on one faction",
        "a mark on a unit whose turn started",
        "a mark out of combat",
        "a mark whose budget lacks the charge",
        "a mark whose spent counts lack its turn pools",
        "a mark with unsorted turn pools",
        "a mark with a zero turn pool count",
        "a mark charging a reaction",
    };

    [Theory]
    [MemberData(nameof(BadStates))]
    public void Bad_markers_and_marks_are_format_errors(string bad)
    {
        var s = Saved();
        var combat = s.Combat!;
        var b = s.FindUnit(B)!;
        var a = s.FindUnit(A)!;
        var broken = bad switch
        {
            "marker in round 2" => s with { Combat = combat with { Round = 2 } },
            "marker with current 1" => s with { Combat = combat with { Current = 1 } },
            "marker with a vacated turn" => s with { Combat = combat with { TurnVacated = true } },
            "marker with nothing pending and no run end" => s with { Pending = null },
            "two marks on one faction" => WithUnit(s, a with { Budget = Charge.Charged, Spent = Charge.TurnPools, Opener = Charge }),
            "a mark on a unit whose turn started" =>
                s with { Combat = new CombatState([B, A, C], 1, 1) },
            "a mark out of combat" => s with { Combat = null },
            "a mark whose budget lacks the charge" => WithUnit(s, b with { Budget = default }),
            "a mark whose spent counts lack its turn pools" => WithUnit(s, b with { Spent = [new PoolSpend("pool.camp", 1)] }),
            "a mark with unsorted turn pools" => WithUnit(
                s,
                b with
                {
                    Spent = [new PoolSpend("pool.turn", 1), new PoolSpend("pool.turn2", 1)],
                    Opener = Charge with { TurnPools = [new PoolSpend("pool.turn2", 1), new PoolSpend("pool.turn", 1)] },
                }),
            "a mark with a zero turn pool count" =>
                WithUnit(s, b with { Opener = Charge with { TurnPools = [new PoolSpend("pool.turn", 0)] } }),
            "a mark charging a reaction" => WithUnit(
                s,
                b with
                {
                    Budget = b.Budget with { ReactionUsed = true },
                    Opener = Charge with { Charged = Charge.Charged with { ReactionUsed = true } },
                }),
            _ => throw new ArgumentException(bad),
        };

        // The unbroken state loads, so each failure is the breach's own.
        Snapshot.Load(Snapshot.Save(s));
        var ex = Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(broken)));
        Assert.DoesNotContain("is missing", ex.Message);
    }

    // A mark on the unit whose round-1 turn is current but vacated, or on a later slot, is legal without the marker.
    [Fact]
    public void A_mark_on_a_later_or_vacated_slot_loads()
    {
        var s = Saved();
        var later = s with { Combat = new CombatState([C, A, B], 1, 1) };
        Assert.NotNull(Snapshot.Load(Snapshot.Save(later)).FindUnit(B)!.Opener);
        var vacated = s with { Combat = new CombatState([C, A, B], 2, 1, TurnVacated: true) };
        Assert.NotNull(Snapshot.Load(Snapshot.Save(vacated)).FindUnit(B)!.Opener);
    }
}
