using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

public class PathfindingTests
{
    private static GameState State(Unit? extra = null) => RingState(extra);

    [Fact]
    public void Path_goes_around_the_pillar()
    {
        var path = Queries.FindPath(State(), Hero, new(3, 3))!.Value;
        Assert.Equal(4, path.Length);                      // no diagonal past the pillar corners
        Assert.Equal(new GridPos(3, 3), Replay.Run(State(), path.Select(d => (Command)new MoveCommand(Hero, d))).FindUnit(Hero)!.Pos);
    }

    [Fact] public void Path_to_self_is_empty() => Assert.Empty(Queries.FindPath(State(), Hero, new(1, 1))!.Value);
    [Fact] public void Same_query_gives_same_path() =>
        Assert.Equal(Queries.FindPath(State(), Hero, new(3, 3))!.Value.AsEnumerable(), Queries.FindPath(State(), Hero, new(3, 3))!.Value);

    [Fact] public void Tie_break_between_equal_paths_is_pinned() =>
        Assert.Equal(
            new[] { Direction.E, Direction.E, Direction.S, Direction.S },
            Queries.FindPath(State(), Hero, new(3, 3))!.Value);

    [Fact]
    public void Path_may_cross_a_shrine_and_ignores_a_pending_decision()
    {
        var s = ShrineState();
        Assert.Equal(new[] { Direction.E, Direction.E }, Queries.FindPath(s, Hero, new(2, 0))!.Value);

        var waiting = Engine.Apply(s, new MoveCommand(Hero, Direction.E)).State;
        Assert.NotNull(waiting.Pending);
        Assert.Equal(new[] { Direction.E }, Queries.FindPath(waiting, Hero, new(2, 0))!.Value);
    }

    [Theory]
    [InlineData(2, 2)]    // wall
    [InlineData(9, 9)]    // off the map
    [InlineData(-1, 0)]
    public void No_path_to_invalid_target(int x, int y) => Assert.Null(Queries.FindPath(State(), Hero, new(x, y)));

    [Fact] public void No_path_for_unknown_unit() => Assert.Null(Queries.FindPath(State(), new UnitId(99), new(3, 3)));
    [Fact] public void No_path_to_occupied_square() =>
        Assert.Null(Queries.FindPath(State(extra: new Unit(new UnitId(2), new(3, 3), Faction.Enemy, [])), Hero, new(3, 3)));
    [Fact] public void No_path_to_sealed_room()
    {
        var s = GameState.Create(GridMap.Parse(".#."), new[] { new Unit(Hero, new(0, 0), Faction.Party, []) }, 7);
        Assert.Null(Queries.FindPath(s, Hero, new(2, 0)));
    }
}
