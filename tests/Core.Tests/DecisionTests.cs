using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

public class DecisionTests
{
    private static GameState State() => ShrineState();

    private static GameState AtShrine() =>
        Engine.Apply(State(), new MoveCommand(Hero, Direction.E)).State;

    [Fact]
    public void Stepping_on_shrine_requests_a_decision()
    {
        var r = Engine.Apply(State(), new MoveCommand(Hero, Direction.E));
        var d = Assert.IsType<DecisionRequested>(r.Events[^1]).Decision;
        Assert.Equal(d, r.State.Pending);
        Assert.Equal(new DecisionId(1), d.Id);
        Assert.Equal(3, d.Options.Distinct().Count());
        Assert.All(d.Options, o => Assert.InRange(o, 0, 9));
        Assert.Equal(State().Rng.Combat, r.State.Rng.Combat);
        Assert.Equal(State().Rng.Shrine.Skip(3), r.State.Rng.Shrine);
    }

    [Fact] public void Same_seed_offers_same_options() =>
        Assert.Equal(AtShrine().Pending!.Options.AsEnumerable(), AtShrine().Pending!.Options);

    [Fact] public void Move_is_rejected_while_decision_pending() =>
        AssertRejected(AtShrine(), new MoveCommand(Hero, Direction.E), CommandError.DecisionPending);

    [Fact]
    public void Answer_applies_choice_and_clears_decision()
    {
        var s = AtShrine(); var options = s.Pending!.Options;
        var r = Engine.Apply(s, new AnswerDecisionCommand(new DecisionId(1), 2));
        Assert.True(r.Ok); Assert.Null(r.State.Pending);
        Assert.Equal(new[] { options[2] }, r.State.FindUnit(Hero)!.Boons);
        Assert.Equal(Tile.Floor, r.State.Map[new(1, 0)]);
        Assert.Equal(s.Tick, r.State.Tick);
        Assert.Equal(new GameEvent[] {
            new DecisionResolved(new DecisionId(1), Hero, options[2]),
            new TileChanged(new(1, 0), Tile.Floor) }, r.Events);
    }

    [Fact] public void Answer_without_pending_decision_is_rejected() =>
        AssertRejected(State(), new AnswerDecisionCommand(new DecisionId(1), 0), CommandError.NoDecisionPending);
    [Fact] public void Answer_to_wrong_decision_is_rejected() =>
        AssertRejected(AtShrine(), new AnswerDecisionCommand(new DecisionId(5), 0), CommandError.WrongDecision);
    [Theory] [InlineData(-1)] [InlineData(3)]
    public void Answer_with_bad_option_is_rejected(int i) =>
        AssertRejected(AtShrine(), new AnswerDecisionCommand(new DecisionId(1), i), CommandError.InvalidOption);

    // Golden values: a change here alters every seeded run.
    [Fact]
    public void Shrine_step_for_seed_7_is_pinned()
    {
        var r = Engine.Apply(State(), new MoveCommand(Hero, Direction.E));
        Assert.Equal(new[] { 5, 9, 4 }, r.State.Pending!.Options);
        Assert.Equal(2, r.State.NextDecisionId);
        Assert.Equal(new GameEvent[] {
            new UnitMoved(Hero, new(0, 0), new(1, 0)),
            new TickAdvanced(1),
            new DecisionRequested(r.State.Pending) }, r.Events);
    }

    /// <summary>The Godot demo seed (<c>DemoLevel.Seed</c>): the shrine stream does not depend on the map.</summary>
    [Fact]
    public void Shrine_step_for_seed_12345_is_pinned()
    {
        var r = Engine.Apply(ShrineState(12345), new MoveCommand(Hero, Direction.E));
        Assert.Equal(new[] { 7, 5, 6 }, r.State.Pending!.Options);
    }

    /// <summary>Review Focus 5: a shrine step makes exactly three shrine draws and no combat draw, under every seed.</summary>
    [Fact]
    public void Shrine_draws_a_fixed_count_for_every_seed()
    {
        var seeds = Enumerable.Range(0, 10_000).Select(i => (ulong)i).Append(RunSeed.Limit - 1);
        foreach (var seed in seeds)
        {
            var r = Engine.Apply(ShrineState(seed), new MoveCommand(Hero, Direction.E)).State;
            Assert.Equal(3UL, r.Rng.Shrine.Counter);
            Assert.Equal(0UL, r.Rng.Combat.Counter);
            var options = r.Pending!.Options;
            Assert.Equal(3, options.Distinct().Count());
            Assert.All(options, o => Assert.InRange(o, 0, 9));
        }
    }

    [Fact]
    public void Answer_to_an_unsupported_decision_kind_throws()
    {
        var s = AtShrine();
        var odd = s with { Pending = s.Pending! with { Kind = (DecisionKind)99 } };
        Assert.Throws<NotSupportedException>(() => Engine.Apply(odd, new AnswerDecisionCommand(new DecisionId(1), 0)));
    }

    [Fact]
    public void Counters_at_their_limit_overflow_instead_of_wrapping()
    {
        var move = new MoveCommand(Hero, Direction.E);
        Assert.Throws<OverflowException>(() => Engine.Apply(State() with { Tick = long.MaxValue }, move));
        Assert.Throws<OverflowException>(() => Engine.Apply(State() with { NextDecisionId = int.MaxValue }, move));
    }
}
