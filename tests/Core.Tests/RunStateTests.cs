using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// The run, combat and turn state (M1-10 task 1): the hero, the combat state, the run end, the event seq, each unit's
/// turn budget and pool spent counts; their validation, the save (version 7; version 8 from M1-11) and the run-end gate.
/// </summary>
public class RunStateTests
{
    private static Unit Foe(int id, int x, int y) =>
        new(new UnitId(id), new(x, y), Faction.Enemy, [], EnemyStats, 12);

    private static GameState Build(UnitId? hero, params Unit[] units) =>
        GameState.Create(GridMap.Parse("....."), units, 7, hero: hero);

    private static void AssertRejected(int unit, string field, UnitId? hero, params Unit[] units)
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(hero, units));
        Assert.Contains($"unit {unit}", ex.Message);
        Assert.Contains(field, ex.Message);
    }

    [Fact]
    public void Create_rejects_a_hero_that_is_missing_or_not_party()
    {
        AssertRejected(9, "hero", new UnitId(9), U(1, 0, 0), Foe(2, 1, 0));
        AssertRejected(2, "hero", new UnitId(2), U(1, 0, 0), Foe(2, 1, 0));
        Assert.Equal(Hero, Build(Hero, U(1, 0, 0), Foe(2, 1, 0)).Hero);
    }

    [Fact]
    public void Create_rejects_negative_budget_counts()
    {
        AssertRejected(4, "movement used", null, U(4, 0, 0) with { Budget = new TurnBudget(-1, 0, false, false, false, false) });
        AssertRejected(4, "actions used", null, U(4, 0, 0) with { Budget = new TurnBudget(0, -1, false, false, false, false) });
    }

    [Fact]
    public void Create_rejects_unsorted_repeated_negative_or_zero_spent_counts()
    {
        AssertRejected(4, "spent", null, U(4, 0, 0) with { Spent = [new("pool.b", 1), new("pool.a", 1)] });
        // Ordinal order: '.' (46) sorts before '_' (95), though a culture comparison puts '_' first.
        AssertRejected(4, "spent", null, U(4, 0, 0) with { Spent = [new("pool_a", 1), new("pool.z", 1)] });
        AssertRejected(4, "spent", null, U(4, 0, 0) with { Spent = [new("pool.a", 1), new("pool.a", 2)] });
        AssertRejected(4, "spent", null, U(4, 0, 0) with { Spent = [new("pool.a", 0)] });
        AssertRejected(4, "spent", null, U(4, 0, 0) with { Spent = [new("pool.a", -1)] });
        Build(null, U(4, 0, 0) with { Spent = [new("pool.z", 1), new("pool_a", 2)] });
    }

    // Pool ids are content ids ([a-z0-9_.:-]), so a bad one is a validation error, not a digest crash.
    [Theory]
    [InlineData("Pool.A")]
    [InlineData("pool a")]
    [InlineData("")]
    [InlineData(null)]
    public void Create_rejects_a_spent_count_whose_pool_id_is_not_a_content_id(string? pool) =>
        AssertRejected(4, "pool", null, U(4, 0, 0) with { Spent = [new(pool!, 1)] });

    [Fact]
    public void A_new_run_is_out_of_combat_and_not_ended()
    {
        var s = Build(Hero, U(1, 0, 0), Foe(2, 1, 0));
        Assert.Null(s.Combat);
        Assert.False(s.CombatActive);
        Assert.Null(s.Ended);
        Assert.Equal(1, s.NextEventSeq);
        Assert.All(s.Units, u => Assert.Equal(default, u.Budget));
        Assert.All(s.Units, u => Assert.Empty(u.Spent));
        Assert.False(new Unit(new UnitId(5), new(0, 0), Faction.Party, [], Fixtures.Stats, FullHp).Spent.IsDefault);
    }

    private static GameState InCombat()
    {
        var hero = U(1, 0, 0) with
        {
            Budget = new TurnBudget(2, 1, true, false, true, true),
            Spent = [new("pool.a", 1), new("pool.b", 3)],
        };
        var s = Build(Hero, hero, Foe(2, 1, 0), Foe(3, 2, 0) with { Budget = new TurnBudget(0, 0, false, true, false, false) });
        return s with { Combat = new CombatState([new(3), new(1), new(2)], 1, 2), NextEventSeq = 5 };
    }

    private static GameState Ended() =>
        Build(Hero, U(1, 0, 0) with { Hp = 0 }, Foe(2, 1, 0)) with { Ended = new RunEnd(RunEndCause.HeroDeath, 4), NextEventSeq = 5 };

    private static GameState Vacated() =>
        InCombat() with { Combat = new CombatState([new(3), new(1), new(2)], 3, 2, TurnVacated: true) };

    [Fact]
    public void The_run_state_survives_save_and_load()
    {
        foreach (var s in new[] { InCombat(), Ended(), Vacated() })
        {
            var json = Snapshot.Save(s);
            Assert.StartsWith("{\"version\":8,", json);
            var loaded = Snapshot.Load(json);
            Assert.Equal(json, Snapshot.Save(loaded));
            Assert.Equal(StateDigest.Of(s), StateDigest.Of(loaded));
            Assert.Equal(Hero, loaded.Hero);
            Assert.Equal(5, loaded.NextEventSeq);
        }

        var combat = Snapshot.Load(Snapshot.Save(InCombat()));
        Assert.Equal([3, 1, 2], combat.Combat!.Order.Select(id => id.Value));
        Assert.Equal((1, 2L, false), (combat.Combat.Current, combat.Combat.Round, combat.Combat.TurnVacated));
        Assert.Equal(new UnitId(1), combat.Combat.CurrentUnit);
        Assert.Equal(new TurnBudget(2, 1, true, false, true, true), combat.Units[0].Budget);
        Assert.Equal([new PoolSpend("pool.a", 1), new PoolSpend("pool.b", 3)], combat.Units[0].Spent.ToArray());
        Assert.Equal(new TurnBudget(0, 0, false, true, false, false), combat.Units[2].Budget);

        var ended = Snapshot.Load(Snapshot.Save(Ended()));
        Assert.Equal(new RunEnd(RunEndCause.HeroDeath, 4), ended.Ended);
        Assert.Null(ended.Combat);

        var vacated = Snapshot.Load(Snapshot.Save(Vacated()));
        Assert.Equal((3, true), (vacated.Combat!.Current, vacated.Combat.TurnVacated));
    }

    [Theory]
    [InlineData("\"order\":[3,1,2]", "\"order\":[3,1,9]")]
    [InlineData("\"order\":[3,1,2]", "\"order\":[3,1,1]")]
    [InlineData("\"current\":1", "\"current\":3")]
    [InlineData("\"current\":1", "\"current\":4")]
    [InlineData("\"current\":1", "\"current\":-1")]
    [InlineData("\"round\":2", "\"round\":0")]
    [InlineData("\"nextEventSeq\":5", "\"nextEventSeq\":0")]
    public void A_load_with_a_bad_combat_or_run_end_is_a_format_error(string from, string to)
    {
        var good = Snapshot.Save(InCombat());
        Assert.Contains(from, good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(from, to)));
    }

    [Theory]
    [InlineData("\"cause\":\"HeroDeath\"", "\"cause\":\"Victory\"")]
    [InlineData("\"cause\":\"HeroDeath\"", "\"cause\":\"Unravelled\"")]
    [InlineData("\"killingEventId\":4", "\"killingEventId\":null")]
    [InlineData("\"killingEventId\":4", "\"killingEventId\":0")]
    [InlineData("\"killingEventId\":4", "\"killingEventId\":-1")]
    [InlineData("\"killingEventId\":4", "\"killingEventId\":5")]
    [InlineData("\"cause\":\"HeroDeath\"", "\"cause\":\"heroDeath\"")]
    [InlineData("\"hero\":1", "\"hero\":null")]
    [InlineData("\"hp\":0", "\"hp\":12")]
    public void A_load_with_a_bad_run_end_is_a_format_error(string from, string to)
    {
        var good = Snapshot.Save(Ended());
        Assert.Contains(from, good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(from, to)));
    }

    [Fact]
    public void A_victory_or_unravelling_without_a_killing_event_loads()
    {
        var good = Snapshot.Save(Ended());
        foreach (var cause in new[] { "Victory", "Unravelled" })
        {
            var json = good.Replace("{\"cause\":\"HeroDeath\",\"killingEventId\":4}", $"{{\"cause\":\"{cause}\",\"killingEventId\":null}}");
            Assert.NotEqual(good, json);
            Assert.Null(Snapshot.Load(json).Ended!.KillingEventId);
        }
    }

    [Fact]
    public void A_vacated_turn_may_point_past_the_order_or_at_an_empty_one()
    {
        var good = Snapshot.Save(Vacated());
        Assert.Contains("\"current\":3,\"round\":2,\"turnVacated\":true", good);
        Assert.Equal(1, Snapshot.Load(good.Replace("\"current\":3", "\"current\":1")).Combat!.Current);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace("\"current\":3", "\"current\":4")));
        var empty = good.Replace("\"order\":[3,1,2],\"current\":3", "\"order\":[],\"current\":0");
        Assert.Empty(Snapshot.Load(empty).Combat!.Order);
        Assert.Throws<SnapshotFormatException>(
            () => Snapshot.Load(empty.Replace("\"turnVacated\":true", "\"turnVacated\":false")));
    }

    [Theory]
    [InlineData("\"hero\":1", "\"hero\":2")]
    [InlineData("\"hero\":1", "\"hero\":9")]
    [InlineData("\"movementUsed\":2", "\"movementUsed\":-1")]
    [InlineData("[{\"pool\":\"pool.a\",\"spent\":1},{\"pool\":\"pool.b\",\"spent\":3}]", "[{\"pool\":\"pool.b\",\"spent\":3},{\"pool\":\"pool.a\",\"spent\":1}]")]
    [InlineData("{\"pool\":\"pool.a\",\"spent\":1}", "{\"pool\":\"pool.a\",\"spent\":0}")]
    [InlineData("{\"pool\":\"pool.a\",", "{\"pool\":\"Pool.A\",")]
    [InlineData("{\"pool\":\"pool.a\",", "{\"pool\":\"\",")]
    [InlineData(",\"nextEventSeq\":5", "")]
    public void A_load_with_a_bad_hero_budget_or_spent_count_is_a_format_error(string from, string to)
    {
        var good = Snapshot.Save(InCombat());
        Assert.Contains(from, good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(from, to)));
    }

    // Save version 7 (M1-10) adds the run, combat and turn state; version 6 saves are refused, not half loaded.
    [Fact]
    public void A_v6_save_is_refused()
    {
        var v6 = Snapshot.Save(ShrineState()).Replace("\"version\":8", "\"version\":6");
        Assert.Equal(6, Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(v6)).Found);
    }

    // Review Focus 5: after the run ends every command is refused, the pending decision's answer included.
    [Fact]
    public void After_a_run_end_every_command_is_refused()
    {
        var atShrine = Engine.Apply(ShrineState(), new MoveCommand(Hero, Direction.E)).State;
        Assert.NotNull(atShrine.Pending);
        var ended = atShrine with { Ended = new RunEnd(RunEndCause.HeroDeath, 1), NextEventSeq = 2 };
        Fixtures.AssertRejected(ended, new MoveCommand(Hero, Direction.W), CommandError.RunEnded);
        Fixtures.AssertRejected(ended, new AnswerDecisionCommand(atShrine.Pending!.Id, 0), CommandError.RunEnded);
        var r = Engine.Apply(ended, new AnswerDecisionCommand(atShrine.Pending!.Id, 0), RulesContext.Empty);
        Assert.Equal(CommandError.RunEnded, r.Error);
        Assert.Same(ended, r.State);
    }
}
