using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

public class DecisionTests
{
    private static GameState State() =>
        GameState.Create(GridMap.Parse(".*."), new[] { U(1, 0, 0) }, 7);

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
        Assert.Equal(State().Rng.Generation, r.State.Rng.Generation);
        Assert.NotEqual(State().Rng.Loot, r.State.Rng.Loot);
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
}
