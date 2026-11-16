using Xunit;

namespace Dungeon.Core.Tests;

public class GridTests
{
    [Fact] public void Diagonal_distance_equals_orthogonal() =>
        Assert.Equal(3, GridPos.Distance(new(0, 0), new(3, 3)));
    [Fact] public void Distance_is_max_of_axes() =>
        Assert.Equal(5, GridPos.Distance(new(1, 1), new(6, 3)));
    [Fact] public void North_is_negative_y() =>
        Assert.Equal(new GridPos(2, 1), new GridPos(2, 2).Step(Direction.N));
    [Fact] public void All_has_eight_distinct_offsets() =>
        Assert.Equal(8, DirectionExt.All.Select(d => d.Offset()).Distinct().Count());

    [Fact]
    public void Parse_reads_rows_top_down()
    {
        var m = GridMap.Parse("#.*", "...");
        Assert.Equal((3, 2), (m.Width, m.Height));
        Assert.Equal(Tile.Wall, m[new(0, 0)]);
        Assert.Equal(Tile.Shrine, m[new(2, 0)]);
        Assert.False(m.IsWalkable(new(0, 0)));
        Assert.True(m.IsWalkable(new(2, 0)));
        Assert.False(m.IsWalkable(new(3, 0)));
        Assert.Equal(new[] { "#.*", "..." }, m.ToRows());
    }

    [Fact]
    public void With_does_not_mutate_original()
    {
        var m = GridMap.Parse("*"); var n = m.With(new(0, 0), Tile.Floor);
        Assert.Equal(Tile.Shrine, m[new(0, 0)]); Assert.Equal(Tile.Floor, n[new(0, 0)]);
    }

    [Fact] public void Parse_rejects_empty() => Assert.Throws<ArgumentException>(() => GridMap.Parse());
    [Fact] public void Parse_rejects_ragged_rows() => Assert.Throws<ArgumentException>(() => GridMap.Parse("..", "."));
    [Fact] public void Parse_rejects_unknown_character() => Assert.Throws<ArgumentException>(() => GridMap.Parse(".x"));
    [Fact] public void Indexer_rejects_out_of_bounds() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => GridMap.Parse(".")[new(1, 0)]);

    // Extra coverage beyond the brief: conventions and error messages.

    [Fact]
    public void Directions_follow_the_grid_convention()
    {
        Assert.Equal(
            new[] { Direction.N, Direction.NE, Direction.E, Direction.SE, Direction.S, Direction.SW, Direction.W, Direction.NW },
            DirectionExt.All);
        Assert.Equal(new GridPos(0, -1), Direction.N.Offset());
        Assert.Equal(new GridPos(1, -1), Direction.NE.Offset());
        Assert.Equal(new GridPos(1, 0), Direction.E.Offset());
        Assert.Equal(new GridPos(1, 1), Direction.SE.Offset());
        Assert.Equal(new GridPos(0, 1), Direction.S.Offset());
        Assert.Equal(new GridPos(-1, 1), Direction.SW.Offset());
        Assert.Equal(new GridPos(-1, 0), Direction.W.Offset());
        Assert.Equal(new GridPos(-1, -1), Direction.NW.Offset());
    }

    [Fact]
    public void Only_two_axis_directions_are_diagonal()
    {
        foreach (var d in DirectionExt.All)
        {
            var o = d.Offset();
            Assert.Equal(o.X != 0 && o.Y != 0, d.IsDiagonal());
            Assert.Equal(1, GridPos.Distance(new(0, 0), o));
        }
    }

    [Fact]
    public void Diagonals_are_the_four_corner_directions() =>
        Assert.Equal(
            new[] { Direction.NE, Direction.SE, Direction.SW, Direction.NW },
            DirectionExt.All.Where(d => d.IsDiagonal()));

    [Fact]
    public void Distance_saturates_instead_of_overflowing()
    {
        Assert.Equal(int.MaxValue, GridPos.Distance(new(-2_000_000_000, 0), new(2_000_000_000, 0)));
        Assert.Equal(int.MaxValue, GridPos.Distance(new(0, int.MinValue), new(0, 0)));
    }

    [Fact]
    public void InBounds_covers_exactly_the_map()
    {
        var m = GridMap.Parse("..", "..", "..");
        Assert.True(m.InBounds(new(0, 0)));
        Assert.True(m.InBounds(new(1, 2)));
        Assert.False(m.InBounds(new(2, 0)));
        Assert.False(m.InBounds(new(0, 3)));
        Assert.False(m.InBounds(new(-1, 0)));
        Assert.False(m.InBounds(new(0, -1)));
        Assert.False(m.IsWalkable(new(-1, 0)));
    }

    [Fact]
    public void With_changes_only_the_named_square()
    {
        var n = GridMap.Parse("...", "...").With(new(1, 1), Tile.Wall);
        Assert.Equal(new[] { "...", ".#." }, n.ToRows());
    }

    [Fact] public void With_rejects_out_of_bounds() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => GridMap.Parse(".").With(new(0, 1), Tile.Wall));

    [Fact]
    public void Parse_errors_name_the_problem()
    {
        Assert.Contains("no rows", Assert.Throws<ArgumentException>(() => GridMap.Parse()).Message);
        Assert.Contains("empty", Assert.Throws<ArgumentException>(() => GridMap.Parse("")).Message);
        Assert.Contains("row 1", Assert.Throws<ArgumentException>(() => GridMap.Parse("..", ".")).Message);
        var unknown = Assert.Throws<ArgumentException>(() => GridMap.Parse("..", ".x")).Message;
        Assert.Contains("'x'", unknown);
        Assert.Contains("row 1", unknown);
        Assert.Contains("column 1", unknown);
    }

    [Fact] public void Parse_rejects_null_row() =>
        Assert.Throws<ArgumentException>(() => GridMap.Parse(".", null!));
}
