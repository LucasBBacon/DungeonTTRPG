using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

public class MoveTests
{
    private static GameState State(Unit? extra = null)
    {
        var map = GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####");
        var units = new List<Unit> { U(1, 1, 1) };
        if (extra is not null)
        {
            units.Add(extra);
        }

        return GameState.Create(map, units, 7);
    }

    [Fact]
    public void Move_onto_floor_succeeds()
    {
        var r = Engine.Apply(State(), new MoveCommand(Hero, Direction.E));
        Assert.True(r.Ok);
        Assert.Equal(new GridPos(2, 1), r.State.FindUnit(Hero)!.Pos);
        Assert.Equal(1, r.State.Tick);
        Assert.Equal(new GameEvent[] { new UnitMoved(Hero, new(1, 1), new(2, 1)), new TickAdvanced(1) }, r.Events);
    }

    [Theory]
    [InlineData(Direction.N, CommandError.Blocked)]
    [InlineData(Direction.SE, CommandError.Blocked)]      // (2,2) is a wall
    public void Move_into_wall_is_rejected(Direction d, CommandError e) => AssertRejected(State(), new MoveCommand(Hero, d), e);

    [Fact] public void Diagonal_past_wall_corner_is_rejected()
    {
        // hero at (2,1): SE to (3,2) is floor but (2,2) is a wall
        var s = Engine.Apply(State(), new MoveCommand(Hero, Direction.E)).State;
        AssertRejected(s, new MoveCommand(Hero, Direction.SE), CommandError.CornerCut);
    }

    [Fact] public void Unknown_unit_is_rejected() =>
        AssertRejected(State(), new MoveCommand(new UnitId(99), Direction.E), CommandError.UnknownUnit);

    [Fact] public void Move_off_map_is_rejected()
    {
        var s = GameState.Create(GridMap.Parse("."), new[] { new Unit(Hero, new(0, 0), Faction.Party, []) }, 7);
        AssertRejected(s, new MoveCommand(Hero, Direction.W), CommandError.OutOfBounds);
    }

    [Fact] public void Move_onto_another_unit_is_rejected()
    {
        var s = State(extra: new Unit(new UnitId(2), new(2, 1), Faction.Enemy, []));
        AssertRejected(s, new MoveCommand(Hero, Direction.E), CommandError.Occupied);
    }

    [Fact] public void Create_rejects_duplicate_ids_and_bad_squares()
    {
        var map = GridMap.Parse("#.");
        Assert.Throws<ArgumentException>(() => GameState.Create(map, new[] { U(1, 1, 0), U(1, 1, 0) }, 7));
        Assert.Throws<ArgumentException>(() => GameState.Create(map, new[] { U(1, 0, 0) }, 7));
    }
}
