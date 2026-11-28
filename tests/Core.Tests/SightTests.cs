using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// XI 4.1.2 sight, plan call 15. Unless a test says otherwise the map is an open 13×13 room and every
/// coordinate is an offset from O = (6, 6), where the viewer stands.
/// </summary>
public class SightTests
{
    private static readonly GridPos O = new(6, 6);

    private static GridPos At(int dx, int dy) => new(O.X + dx, O.Y + dy);

    internal static GridMap Room(params (int Dx, int Dy)[] walls)
    {
        var rows = new string[13];
        for (int y = 0; y < 13; y++) rows[y] = new string('.', 13);
        var map = GridMap.Parse(rows);
        foreach (var (dx, dy) in walls) map = map.With(At(dx, dy), Tile.Wall);
        return map;
    }

    private static bool Sees(GridMap map, int dx, int dy) => Sight.CanSee(map, O, At(dx, dy));

    [Fact]
    public void Walls_block_and_units_do_not()
    {
        // A wall two squares east hides the square behind it.
        Assert.False(Sees(Room((2, 0)), 4, 0));
        Assert.True(Sees(Room(), 4, 0)); // control: no wall

        // Units are not part of the sight input at all (CanSee reads only the map), so a unit
        // standing at (2, 0) cannot block: every parameter is shown here.
        var parameters = typeof(Sight).GetMethod(nameof(Sight.CanSee))!.GetParameters();
        Assert.Equal(new[] { typeof(GridMap), typeof(GridPos), typeof(GridPos) },
            parameters.Select(p => p.ParameterType).ToArray());
    }

    [Fact]
    public void Sight_glances_past_one_corner()
    {
        Assert.True(Sees(Room((1, 0)), 1, -1));
        Assert.True(Sees(Room((0, -1)), 1, -1));
    }

    [Fact]
    public void A_diagonal_crack_blocks_sight()
    {
        var crack = Room((1, 0), (0, -1));
        Assert.False(Sees(crack, 1, -1));
        Assert.False(Sees(crack, 2, -2));
        Assert.False(Sees(crack, 3, -3));

        // Controls: either wall alone lets the diagonal through.
        foreach (var one in new[] { Room((1, 0)), Room((0, -1)) })
        {
            Assert.True(Sees(one, 1, -1));
            Assert.True(Sees(one, 2, -2));
            Assert.True(Sees(one, 3, -3));
        }
    }

    [Fact]
    public void A_crack_off_the_diagonal_blocks_a_ray_through_its_corner()
    {
        Assert.False(Sees(Room((1, 1), (0, 2)), 1, 3));
        Assert.True(Sees(Room((1, 1)), 1, 3)); // control
        Assert.True(Sees(Room((0, 2)), 1, 3)); // control
    }

    [Fact]
    public void A_sealed_crack_hides_the_wedge_beyond_it()
    {
        // A room's NE inner corner with the corner square open: walls along the north row (x <= 6,
        // y = 5) and the east column (x = 7, y >= 6). (6, 6) meets (7, 5) only across the crack
        // between (6, 5) and (7, 6), so nothing past that crack is visible.
        var map = Room();
        for (int x = 0; x <= 6; x++) map = map.With(new GridPos(x, 5), Tile.Wall);
        for (int y = 6; y < 13; y++) map = map.With(new GridPos(7, y), Tile.Wall);

        Assert.False(Sight.CanSee(map, O, new GridPos(7, 5)));
        Assert.False(Sight.CanSee(map, O, new GridPos(8, 5)));
        Assert.False(Sight.CanSee(map, O, new GridPos(9, 4)));
        for (int y = 0; y <= 5; y++)
            for (int x = 7; x < 13; x++)
                Assert.False(Sight.CanSee(map, O, new GridPos(x, y)), $"({x}, {y}) is past the sealed crack");

        Assert.True(Sight.CanSee(map, O, new GridPos(2, 8))); // control: the room side stays visible
    }

    [Fact]
    public void A_diagonal_wall_line_of_corner_touching_walls_hides_its_far_side()
    {
        // Walls on x − y = 1 across a 13×13 map: movement and lines cannot cross, nor can sight.
        var map = Room();
        for (int x = 1; x < 13; x++) map = map.With(new GridPos(x, x - 1), Tile.Wall);

        for (int y = 0; y < 13; y++)
        {
            for (int x = 0; x < 13; x++)
            {
                if (x - y >= 2)
                    Assert.False(Sight.CanSee(map, O, new GridPos(x, y)), $"({x}, {y}) is on the far side");
            }
        }

        Assert.True(Sight.CanSee(map, O, new GridPos(0, 12))); // control: the near side stays visible
    }

    [Fact]
    public void CanSee_is_symmetric()
    {
        var rng = new System.Random(20261127);
        for (int m = 0; m < 200; m++)
        {
            var rows = new string[12];
            for (int y = 0; y < 12; y++)
            {
                var chars = new char[12];
                for (int x = 0; x < 12; x++) chars[x] = rng.Next(100) < 20 ? '#' : '.';
                rows[y] = new string(chars);
            }

            var map = GridMap.Parse(rows);
            var floors = new List<GridPos>();
            for (int y = 0; y < 12; y++)
                for (int x = 0; x < 12; x++)
                    if (map[new(x, y)] != Tile.Wall) floors.Add(new(x, y));

            for (int i = 0; i < floors.Count; i++)
            {
                for (int j = i + 1; j < floors.Count; j++)
                {
                    var a = floors[i];
                    var b = floors[j];
                    bool ab = Sight.CanSee(map, a, b);
                    bool ba = Sight.CanSee(map, b, a);
                    Assert.True(ab == ba, $"map {m}: {a}->{b} = {ab}, {b}->{a} = {ba}\n{string.Join("\n", rows)}");
                }
            }
        }
    }

    [Fact]
    public void Walls_see_nothing()
    {
        var map = Room((0, 0), (3, 0));
        Assert.False(Sight.CanSee(map, O, O));
        Assert.False(Sees(map, 1, 0));
        Assert.False(Sight.CanSee(map, At(1, 0), O));
        Assert.False(Sight.CanSee(map, At(1, 0), At(3, 0)));
        Assert.False(Sight.CanSee(map, O, new GridPos(-1, 0)));
        Assert.True(Sight.CanSee(Room(), O, O)); // control: a floor square sees itself
    }
}
