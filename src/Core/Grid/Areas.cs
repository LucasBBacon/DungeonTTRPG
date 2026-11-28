using System;
using System.Collections.Immutable;

namespace Dungeon.Core;

/// <summary>An area shape (AB 8.1). Distances are Chebyshev; every size is at least 1.</summary>
public abstract record AreaShape;

/// <summary>Every square within distance <see cref="Radius"/> of a point: (2r+1)² squares.</summary>
public sealed record Burst(int Radius) : AreaShape
{
    public int Radius { get; } = Radius >= 1
        ? Radius
        : throw new ArgumentOutOfRangeException(nameof(Radius), Radius, "A burst radius is at least 1.");
}

/// <summary>
/// A cone from the user in one of the 8 directions: orthogonal, squares 1..L forward and no further
/// sideways than forward; diagonal, both offsets 0..L, not the user's own square. (L+1)² − 1 squares.
/// </summary>
public sealed record Cone(int Length) : AreaShape
{
    public int Length { get; } = Length >= 1
        ? Length
        : throw new ArgumentOutOfRangeException(nameof(Length), Length, "A cone length is at least 1.");
}

/// <summary>A line 1 square wide from the user in one of the 8 directions, stopping at the first wall: L squares.</summary>
public sealed record Line(int Length) : AreaShape
{
    public int Length { get; } = Length >= 1
        ? Length
        : throw new ArgumentOutOfRangeException(nameof(Length), Length, "A line length is at least 1.");
}

/// <summary>
/// Area geometry (AB 8.1, plan call 15). Square lists are in row-major order (Y ascending, then X
/// ascending), generated in that order directly, never sorted.
/// </summary>
public static class Areas
{
    /// <summary>The geometric size of a shape: (2r+1)², (L+1)² − 1 or L. The area cap reads this.</summary>
    public static int ShapeSize(AreaShape shape) => shape switch
    {
        Burst b => (2 * b.Radius + 1) * (2 * b.Radius + 1),
        Cone c => (c.Length + 1) * (c.Length + 1) - 1,
        Line l => l.Length,
        null => throw new ArgumentNullException(nameof(shape)),
        _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, "Unknown area shape."),
    };

    /// <summary>
    /// The geometric squares of <paramref name="shape"/> at <paramref name="origin"/>, ignoring the map,
    /// in row-major order. A burst takes <paramref name="aim"/> null; cones and lines need it.
    /// </summary>
    public static ImmutableArray<GridPos> ShapeSquares(AreaShape shape, GridPos origin, Direction? aim)
    {
        CheckAim(shape, aim);
        return shape switch
        {
            Burst b => BurstSquares(b.Radius, origin),
            Cone c => ConeSquares(c.Length, origin, aim!.Value),
            Line l => LineSquares(l.Length, origin, aim!.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, "Unknown area shape."),
        };
    }

    /// <summary>
    /// The squares the area covers on <paramref name="map"/>: shape squares that are in bounds, not
    /// walls and seen from the origin (<see cref="Sight.CanSee"/>), in row-major order. A line is first
    /// cut at the first wall or blocked diagonal step: the movement corner rule, a diagonal step with
    /// a wall (or the map edge) on either orthogonal neighbour stops it.
    /// </summary>
    public static ImmutableArray<GridPos> Covered(GridMap map, AreaShape shape, GridPos origin, Direction? aim)
    {
        ArgumentNullException.ThrowIfNull(map);
        CheckAim(shape, aim);
        if (shape is Line line)
        {
            return CoveredLine(map, line.Length, origin, aim!.Value);
        }

        var squares = ShapeSquares(shape, origin, aim);
        var result = ImmutableArray.CreateBuilder<GridPos>(squares.Length);
        foreach (var p in squares)
        {
            // CanSee is false for squares off the map or walls, and for every square when the origin is.
            if (Sight.CanSee(map, origin, p))
            {
                result.Add(p);
            }
        }

        return result.ToImmutable();
    }

    private static void CheckAim(AreaShape shape, Direction? aim)
    {
        ArgumentNullException.ThrowIfNull(shape);
        if (shape is Burst && aim is not null)
        {
            throw new ArgumentException("A burst takes no aim.", nameof(aim));
        }

        if (shape is not Burst && aim is null)
        {
            throw new ArgumentException("Cones and lines need an aim.", nameof(aim));
        }

        if (aim is { } d && !Enum.IsDefined(d))
        {
            throw new ArgumentOutOfRangeException(nameof(aim), d, "Unknown direction.");
        }
    }

    private static ImmutableArray<GridPos> BurstSquares(int r, GridPos o)
    {
        var result = ImmutableArray.CreateBuilder<GridPos>((2 * r + 1) * (2 * r + 1));
        for (int y = o.Y - r; y <= o.Y + r; y++)
        {
            for (int x = o.X - r; x <= o.X + r; x++)
            {
                result.Add(new GridPos(x, y));
            }
        }

        return result.MoveToImmutable();
    }

    /// <summary>Filters the (2L+1)² box around the origin, row by row, with the cone rule.</summary>
    private static ImmutableArray<GridPos> ConeSquares(int l, GridPos o, Direction d)
    {
        var u = d.Offset();
        var result = ImmutableArray.CreateBuilder<GridPos>((l + 1) * (l + 1) - 1);
        for (int dy = -l; dy <= l; dy++)
        {
            for (int dx = -l; dx <= l; dx++)
            {
                if (InCone(l, u, d.IsDiagonal(), dx, dy))
                {
                    result.Add(new GridPos(o.X + dx, o.Y + dy));
                }
            }
        }

        return result.MoveToImmutable();
    }

    private static bool InCone(int l, GridPos u, bool diagonal, int dx, int dy)
    {
        if (!diagonal)
        {
            int forward = dx * u.X + dy * u.Y;
            int side = dx * u.Y - dy * u.X;
            return forward >= 1 && forward <= l && Math.Abs(side) <= forward;
        }

        // Both offsets 0..L in the aimed signs, not the user's own square.
        int fx = dx * u.X;
        int fy = dy * u.Y;
        return fx >= 0 && fx <= l && fy >= 0 && fy <= l && (dx != 0 || dy != 0);
    }

    /// <summary>
    /// origin + k·aim for k = 1..L, listed row by row: lines aimed north, or due west, run against
    /// row-major order, so they are listed from the far end.
    /// </summary>
    private static ImmutableArray<GridPos> LineSquares(int l, GridPos o, Direction d)
    {
        var result = ImmutableArray.CreateBuilder<GridPos>(l);
        for (int k = 1; k <= l; k++)
        {
            result.Add(At(o, d, k));
        }

        if (RunsBackwards(d))
        {
            result.Reverse();
        }

        return result.MoveToImmutable();
    }

    private static ImmutableArray<GridPos> CoveredLine(GridMap map, int l, GridPos o, Direction d)
    {
        var result = ImmutableArray.CreateBuilder<GridPos>(l);
        var prev = o;
        for (int k = 1; k <= l; k++)
        {
            var next = At(o, d, k);
            if (!map.IsWalkable(next))
            {
                break;
            }

            // The movement corner rule (as Engine.CheckStep): a diagonal step needs both squares it
            // passes between to be open.
            if (d.IsDiagonal()
                && (!map.IsWalkable(new GridPos(next.X, prev.Y)) || !map.IsWalkable(new GridPos(prev.X, next.Y))))
            {
                break;
            }

            if (Sight.CanSee(map, o, next))
            {
                result.Add(next);
            }

            prev = next;
        }

        if (RunsBackwards(d))
        {
            result.Reverse();
        }

        return result.ToImmutable();
    }

    private static bool RunsBackwards(Direction d)
    {
        var u = d.Offset();
        return u.Y < 0 || (u.Y == 0 && u.X < 0);
    }

    private static GridPos At(GridPos o, Direction d, int k)
    {
        var u = d.Offset();
        return new GridPos(o.X + k * u.X, o.Y + k * u.Y);
    }
}
