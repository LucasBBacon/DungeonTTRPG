using System;
using System.Collections.Immutable;

namespace Dungeon.Core;

/// <summary>A square on the flat rules grid. X grows east, Y grows south.</summary>
public readonly record struct GridPos(int X, int Y)
{
    /// <summary>The neighbouring square one step in direction <paramref name="d"/>.</summary>
    public GridPos Step(Direction d)
    {
        var o = d.Offset();
        return new GridPos(X + o.X, Y + o.Y);
    }

    /// <summary>
    /// Number of steps between two squares when a diagonal step costs the same as an
    /// orthogonal one (Chebyshev distance): the larger of the two axis differences.
    /// </summary>
    public static int Distance(GridPos a, GridPos b) =>
        Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
}

/// <summary>The eight step directions, clockwise from north.</summary>
public enum Direction { N, NE, E, SE, S, SW, W, NW }

public static class DirectionExt
{
    /// <summary>All eight directions in enum order.</summary>
    public static readonly ImmutableArray<Direction> All = ImmutableArray.Create(
        Direction.N, Direction.NE, Direction.E, Direction.SE,
        Direction.S, Direction.SW, Direction.W, Direction.NW);

    /// <summary>The unit step for a direction. North is (0, -1), east is (1, 0).</summary>
    public static GridPos Offset(this Direction d) => d switch
    {
        Direction.N => new GridPos(0, -1),
        Direction.NE => new GridPos(1, -1),
        Direction.E => new GridPos(1, 0),
        Direction.SE => new GridPos(1, 1),
        Direction.S => new GridPos(0, 1),
        Direction.SW => new GridPos(-1, 1),
        Direction.W => new GridPos(-1, 0),
        Direction.NW => new GridPos(-1, -1),
        _ => throw new ArgumentOutOfRangeException(nameof(d), d, "Unknown direction."),
    };

    public static bool IsDiagonal(this Direction d)
    {
        var o = d.Offset();
        return o.X != 0 && o.Y != 0;
    }
}
