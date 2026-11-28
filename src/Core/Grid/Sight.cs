using System;
using System.Collections.Generic;

namespace Dungeon.Core;

/// <summary>
/// Line of sight (XI 4.1.2, plan call 15). <see cref="CanSee"/> is the conjunction of two symmetric
/// conditions, so it is symmetric:
/// (a) Albert Ford's integer symmetric shadowcasting from <c>a</c> lights <c>b</c>;
/// (b) the segment from <c>a</c>'s centre to <c>b</c>'s centre passes through no square corner whose
/// two orthogonal neighbours across the crossing are both walls (the corner-crack rule: sight is
/// blocked on a diagonal only when both orthogonal neighbours block sight).
/// Only walls block sight in M1; squares off the map count as walls. Units never block sight.
/// Integer arithmetic only: Ford's slopes are kept as fractions.
/// </summary>
public static class Sight
{
    /// <summary>
    /// True when <paramref name="a"/> can see <paramref name="b"/>. False if either square is off the
    /// map or a wall (a blocking square sees nothing and is not "seen" by this function); true if
    /// <paramref name="a"/> equals <paramref name="b"/>.
    /// </summary>
    public static bool CanSee(GridMap map, GridPos a, GridPos b)
    {
        ArgumentNullException.ThrowIfNull(map);
        if (Opaque(map, a) || Opaque(map, b))
        {
            return false;
        }

        if (a == b)
        {
            return true;
        }

        return FordLights(map, a, b) && !CrossesCrack(map, a, b);
    }

    private static bool Opaque(GridMap map, GridPos p) => !map.InBounds(p) || map[p] == Tile.Wall;

    // ---- (a) Ford's symmetric shadowcasting, restricted to the quadrant(s) holding the target ----

    /// <summary>Ford's quadrants: depth runs away from the origin, column across.</summary>
    private enum Quadrant { North, East, South, West }

    private static GridPos Transform(GridPos o, Quadrant q, long depth, long col) => q switch
    {
        Quadrant.North => new GridPos(checked((int)(o.X + col)), checked((int)(o.Y - depth))),
        Quadrant.South => new GridPos(checked((int)(o.X + col)), checked((int)(o.Y + depth))),
        Quadrant.East => new GridPos(checked((int)(o.X + depth)), checked((int)(o.Y + col))),
        Quadrant.West => new GridPos(checked((int)(o.X - depth)), checked((int)(o.Y + col))),
        _ => throw new ArgumentOutOfRangeException(nameof(q), q, "Unknown quadrant."),
    };

    /// <summary>
    /// Ford's full algorithm lights a square if any quadrant lights it. A target on a diagonal lies in
    /// two quadrants, so both are tried; every other target lies in exactly one.
    /// </summary>
    private static bool FordLights(GridMap map, GridPos a, GridPos b)
    {
        long dx = (long)b.X - a.X;
        long dy = (long)b.Y - a.Y;
        return (-dy >= Math.Abs(dx) && ScanQuadrant(map, a, Quadrant.North, -dy, dx))
            || (dx >= Math.Abs(dy) && ScanQuadrant(map, a, Quadrant.East, dx, dy))
            || (dy >= Math.Abs(dx) && ScanQuadrant(map, a, Quadrant.South, dy, dx))
            || (-dx >= Math.Abs(dy) && ScanQuadrant(map, a, Quadrant.West, -dx, dy));
    }

    /// <summary>A row of Ford's scan: slopes are fractions with positive denominators.</summary>
    private readonly record struct Row(long Depth, long StartNum, long StartDen, long EndNum, long EndDen);

    /// <summary>
    /// Ford's <c>scan</c> for one quadrant, iterative instead of recursive, stopping below the target
    /// row (deeper rows never affect shallower ones). Returns true when the target square
    /// (<paramref name="targetDepth"/>, <paramref name="targetCol"/>) is revealed.
    /// </summary>
    private static bool ScanQuadrant(GridMap map, GridPos o, Quadrant q, long targetDepth, long targetCol)
    {
        var pending = new Stack<Row>();
        pending.Push(new Row(1, -1, 1, 1, 1));
        while (pending.Count > 0)
        {
            var row = pending.Pop();
            long depth = row.Depth;
            long startNum = row.StartNum;
            long startDen = row.StartDen;

            // min_col = round_ties_up(depth * start), max_col = round_ties_down(depth * end).
            long minCol = RoundTiesUp(depth * startNum, startDen);
            long maxCol = RoundTiesDown(depth * row.EndNum, row.EndDen);

            int prev = None;
            for (long col = minCol; col <= maxCol; col++)
            {
                bool wall = Opaque(map, Transform(o, q, depth, col));

                // Ford reveals a square when it is a wall or is_symmetric(row, square). Walls are
                // never targets here, so only the symmetric test matters for the target.
                if (!wall && depth == targetDepth && col == targetCol
                    && col * startDen >= depth * startNum
                    && col * row.EndDen <= depth * row.EndNum)
                {
                    return true;
                }

                // slope(tile) = (2·col − 1) / (2·depth).
                if (prev == WallTile && !wall)
                {
                    startNum = 2 * col - 1;
                    startDen = 2 * depth;
                }

                if (prev == FloorTile && wall && depth < targetDepth)
                {
                    pending.Push(new Row(depth + 1, startNum, startDen, 2 * col - 1, 2 * depth));
                }

                prev = wall ? WallTile : FloorTile;
            }

            if (prev == FloorTile && depth < targetDepth)
            {
                pending.Push(new Row(depth + 1, startNum, startDen, row.EndNum, row.EndDen));
            }
        }

        return false;
    }

    private const int None = 0;
    private const int WallTile = 1;
    private const int FloorTile = 2;

    /// <summary>n/d rounded to the nearest integer, ties up: floor((2n + d) / 2d). d is positive.</summary>
    private static long RoundTiesUp(long n, long d) => IntMath.FloorDiv(2 * n + d, 2 * d);

    /// <summary>n/d rounded to the nearest integer, ties down: ceil((2n − d) / 2d). d is positive.</summary>
    private static long RoundTiesDown(long n, long d) => -IntMath.FloorDiv(d - 2 * n, 2 * d);

    // ---- (b) the corner-crack rule over the centre-to-centre segment ----

    /// <summary>
    /// In doubled coordinates square centres are even and square corners are odd on both axes. The
    /// lattice points on the segment from 2a to 2b are 2a + k·(D / g), k = 0..g, with D = 2(b − a)
    /// and g = gcd(|Dx|, |Dy|); the corners among them are those odd on both axes. At a corner the
    /// segment crosses diagonally from one square to the opposite one, and the other two squares
    /// touching the corner are the orthogonal neighbours across the crossing. The pair is the same
    /// in both directions, so the rule is symmetric.
    /// </summary>
    private static bool CrossesCrack(GridMap map, GridPos a, GridPos b)
    {
        long dx = 2 * ((long)b.X - a.X);
        long dy = 2 * ((long)b.Y - a.Y);
        if (dx == 0 || dy == 0)
        {
            return false; // an axis-aligned segment between centres never meets a corner
        }

        long g = Gcd(Math.Abs(dx), Math.Abs(dy));
        long stepX = IntMath.FloorDiv(dx, g);
        long stepY = IntMath.FloorDiv(dy, g);
        long sx = Math.Sign(dx);
        long sy = Math.Sign(dy);
        for (long k = 1; k < g; k++)
        {
            long cx = 2L * a.X + k * stepX;
            long cy = 2L * a.Y + k * stepY;
            if (IntMath.FloorMod(cx, 2) == 1 && IntMath.FloorMod(cy, 2) == 1)
            {
                var side1 = new GridPos(
                    checked((int)IntMath.FloorDiv(cx + sx, 2)), checked((int)IntMath.FloorDiv(cy - sy, 2)));
                var side2 = new GridPos(
                    checked((int)IntMath.FloorDiv(cx - sx, 2)), checked((int)IntMath.FloorDiv(cy + sy, 2)));
                if (Opaque(map, side1) && Opaque(map, side2))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static long Gcd(long x, long y)
    {
        while (y != 0)
        {
            long t = IntMath.FloorMod(x, y);
            x = y;
            y = t;
        }

        return x;
    }
}
