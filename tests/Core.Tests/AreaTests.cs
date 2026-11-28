using System.Collections.Immutable;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// AB 8.1 areas. <c>ShapeSquares</c> tests use raw coordinates around (0, 0); <c>Covered</c> tests use
/// the open 13×13 room with coordinates as offsets from O = (6, 6).
/// </summary>
public class AreaTests
{
    private static readonly GridPos O = new(6, 6);

    private static GridPos At(int dx, int dy) => new(O.X + dx, O.Y + dy);

    private static GridPos[] Offsets(ImmutableArray<GridPos> squares) =>
        squares.Select(p => new GridPos(p.X - O.X, p.Y - O.Y)).ToArray();

    private static readonly GridPos Zero = new(0, 0);

    public static TheoryData<Direction, int> ConeCases()
    {
        var data = new TheoryData<Direction, int>();
        foreach (var d in DirectionExt.All)
            for (int l = 1; l <= 4; l++) data.Add(d, l);
        return data;
    }

    public static TheoryData<Direction, int> BurstCases()
    {
        var data = new TheoryData<Direction, int>();
        foreach (var d in DirectionExt.All)
            for (int r = 1; r <= 2; r++) data.Add(d, r);
        return data;
    }

    /// <summary>The brief's cone membership rule, written independently of the implementation.</summary>
    private static bool InCone(Direction d, int l, int dx, int dy)
    {
        var u = d.Offset();
        if (!d.IsDiagonal())
        {
            int f = dx * u.X + dy * u.Y;
            int s = dx * u.Y - dy * u.X;
            return 1 <= f && f <= l && Math.Abs(s) <= f;
        }

        return (dx == 0 || Math.Sign(dx) == u.X)
            && (dy == 0 || Math.Sign(dy) == u.Y)
            && Math.Max(Math.Abs(dx), Math.Abs(dy)) <= l
            && !(dx == 0 && dy == 0);
    }

    private static GridPos[] RuleCone(Direction d, int l, GridPos origin)
    {
        var list = new List<GridPos>();
        for (int dy = -l; dy <= l; dy++)
            for (int dx = -l; dx <= l; dx++)
                if (InCone(d, l, dx, dy)) list.Add(new(origin.X + dx, origin.Y + dy));
        return list.ToArray();
    }

    [Fact]
    public void The_cone_rule_matches_the_literal_L2_sets()
    {
        GridPos[] P(params (int, int)[] xs) => xs.Select(t => new GridPos(t.Item1, t.Item2)).ToArray();
        Assert.Equal(P((2, -2), (1, -1), (2, -1), (1, 0), (2, 0), (1, 1), (2, 1), (2, 2)),
            RuleCone(Direction.E, 2, Zero));
        Assert.Equal(P((-2, -2), (-1, -2), (0, -2), (1, -2), (2, -2), (-1, -1), (0, -1), (1, -1)),
            RuleCone(Direction.N, 2, Zero));
        Assert.Equal(P((1, 0), (2, 0), (0, 1), (1, 1), (2, 1), (0, 2), (1, 2), (2, 2)),
            RuleCone(Direction.SE, 2, Zero));
        Assert.Equal(P((-2, -2), (-1, -2), (0, -2), (-2, -1), (-1, -1), (0, -1), (-2, 0), (-1, 0)),
            RuleCone(Direction.NW, 2, Zero));
    }

    [Theory]
    [MemberData(nameof(ConeCases))]
    public void Cones_in_all_8_directions(Direction d, int l)
    {
        var squares = Areas.ShapeSquares(new Cone(l), Zero, d);
        Assert.Equal((l + 1) * (l + 1) - 1, squares.Length);
        Assert.DoesNotContain(Zero, squares);
        Assert.Equal(RuleCone(d, l, Zero), squares.ToArray());
    }

    [Theory]
    [MemberData(nameof(BurstCases))]
    public void Bursts_in_all_8_directions(Direction d, int r)
    {
        var u = d.Offset();
        var point = new GridPos(O.X + 2 * u.X, O.Y + 2 * u.Y);
        var expected = new List<GridPos>();
        for (int y = point.Y - r; y <= point.Y + r; y++)
            for (int x = point.X - r; x <= point.X + r; x++)
                expected.Add(new(x, y));

        Assert.Equal(expected.ToArray(), Areas.ShapeSquares(new Burst(r), point, null).ToArray());
        Assert.Equal(expected.ToArray(), Areas.Covered(SightTests.Room(), new Burst(r), point, null).ToArray());
    }

    [Fact]
    public void Lines_stop_at_the_first_wall()
    {
        Assert.Equal(new GridPos[] { new(1, 0), new(2, 0), new(3, 0), new(4, 0) },
            Offsets(Areas.Covered(SightTests.Room(), new Line(4), O, Direction.E)));
        Assert.Equal(new GridPos[] { new(1, 0), new(2, 0) },
            Offsets(Areas.Covered(SightTests.Room((3, 0)), new Line(4), O, Direction.E)));
    }

    [Fact]
    public void A_diagonal_line_stops_at_a_corner()
    {
        Assert.Empty(Areas.Covered(SightTests.Room((1, 0)), new Line(3), O, Direction.NE));
        // Control: without the wall the line runs, listed row by row (north first).
        Assert.Equal(new GridPos[] { new(3, -3), new(2, -2), new(1, -1) },
            Offsets(Areas.Covered(SightTests.Room(), new Line(3), O, Direction.NE)));
    }

    [Fact]
    public void A_burst_behind_a_wall_is_cut_by_origin_sight()
    {
        var open = Offsets(Areas.Covered(SightTests.Room(), new Burst(2), O, null));
        var cut = Offsets(Areas.Covered(SightTests.Room((1, 0)), new Burst(2), O, null));
        Assert.Equal(25, open.Length); // control
        Assert.Equal(23, cut.Length);
        Assert.Equal(open.Where(p => p != new GridPos(1, 0) && p != new GridPos(2, 0)).ToArray(), cut);
    }

    [Fact]
    public void Covered_clips_to_the_map()
    {
        var map = SightTests.Room();
        Assert.Equal(new GridPos[] { new(0, 0), new(1, 0), new(0, 1), new(1, 1) },
            Areas.Covered(map, new Burst(1), new GridPos(0, 0), null).ToArray());
        Assert.Equal(new GridPos[] { new(0, 0), new(1, 0) },
            Areas.Covered(map, new Line(5), new GridPos(2, 0), Direction.W).ToArray());
        Assert.Empty(Areas.Covered(map, new Line(3), new GridPos(0, 0), Direction.N));
        Assert.Equal(3, Areas.Covered(map, new Cone(1), new GridPos(0, 0), Direction.SE).Length);
    }

    [Fact]
    public void ShapeSize_matches_ShapeSquares()
    {
        for (int r = 1; r <= 3; r++)
            Assert.Equal(Areas.ShapeSize(new Burst(r)), Areas.ShapeSquares(new Burst(r), Zero, null).Length);
        for (int l = 1; l <= 6; l++)
        {
            foreach (var d in DirectionExt.All)
            {
                Assert.Equal(Areas.ShapeSize(new Cone(l)), Areas.ShapeSquares(new Cone(l), Zero, d).Length);
                Assert.Equal(Areas.ShapeSize(new Line(l)), Areas.ShapeSquares(new Line(l), Zero, d).Length);
            }
        }

        Assert.Equal(new[] { 9, 25, 49 }, new[] { 1, 2, 3 }.Select(r => Areas.ShapeSize(new Burst(r))));
        Assert.Equal(new[] { 3, 8, 15, 24, 35, 48 }, Enumerable.Range(1, 6).Select(l => Areas.ShapeSize(new Cone(l))));
    }

    [Fact]
    public void Shapes_reject_bad_sizes_and_aims()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Burst(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Cone(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Line(0));
        Assert.Throws<ArgumentException>(() => Areas.ShapeSquares(new Burst(1), Zero, Direction.N));
        Assert.Throws<ArgumentException>(() => Areas.ShapeSquares(new Cone(1), Zero, null));
        Assert.Throws<ArgumentException>(() => Areas.ShapeSquares(new Line(1), Zero, null));
    }
}
