using System;
using System.Collections.Generic;

namespace Dungeon.Core;

/// <summary>
/// Line of sight (XI 4.1.2, plan call 15). <see cref="CanSee"/> is the conjunction of two symmetric
/// conditions, so it is symmetric:
/// (a) Albert Ford's integer symmetric shadowcasting from <c>a</c> lights <c>b</c>;
/// (b) the segment from <c>a</c>'s centre to <c>b</c>'s centre meets no sealed crack: a crack is two
/// walls touching only at a corner, sealed by the closed segment joining their centres (so sight is
/// blocked on a diagonal when both orthogonal neighbours block sight, and a ray cannot slip through
/// the crack off its exact corner either; one wall alone is no crack).
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

    // ---- (b) the crack-seal rule over the centre-to-centre segment ----

    /// <summary>
    /// A crack is two blocking squares p and q that touch only at a corner, q = p + (±1, ±1); the
    /// closed segment joining their centres seals it. In doubled coordinates (square centres even,
    /// corners odd) the segment from 2a to 2b is blocked when it meets the closed segment 2p→2q of
    /// any crack. Every such crack lies in the bounding box of a..b expanded by 1 (any point of 2p→2q
    /// is within one square of p and of q on each axis), so only that box is scanned. Segment
    /// intersection does not depend on the order of a and b, so the rule is symmetric; it includes the
    /// exact-corner case because 2p→2q passes through the shared corner. One wall alone is no crack,
    /// so sight still glances past a single corner.
    /// </summary>
    private static bool CrossesCrack(GridMap map, GridPos a, GridPos b)
    {
        long ax = 2L * a.X;
        long ay = 2L * a.Y;
        long bx = 2L * b.X;
        long by = 2L * b.Y;
        long minX = Math.Min(a.X, b.X) - 1L;
        long maxX = Math.Max(a.X, b.X) + 1L;
        long minY = Math.Min(a.Y, b.Y) - 1L;
        long maxY = Math.Max(a.Y, b.Y) + 1L;
        for (long py = minY; py < maxY; py++)
        {
            for (long px = minX; px <= maxX; px++)
            {
                var p = new GridPos(checked((int)px), checked((int)py));
                if (!Opaque(map, p))
                {
                    continue;
                }

                // q one row below p, one column either side: each crack is visited once, from its
                // upper square.
                for (long sx = -1; sx <= 1; sx += 2)
                {
                    long qx = px + sx;
                    if (qx < minX || qx > maxX)
                    {
                        continue;
                    }

                    var q = new GridPos(checked((int)qx), checked((int)(py + 1)));
                    if (Opaque(map, q)
                        && SegmentsMeet(ax, ay, bx, by, 2 * px, 2 * py, 2 * qx, 2 * (py + 1)))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>Closed segments p1→p2 and q1→q2 share a point (touching and collinear cases included).</summary>
    private static bool SegmentsMeet(long p1x, long p1y, long p2x, long p2y, long q1x, long q1y, long q2x, long q2y)
    {
        int d1 = Orientation(q1x, q1y, q2x, q2y, p1x, p1y);
        int d2 = Orientation(q1x, q1y, q2x, q2y, p2x, p2y);
        int d3 = Orientation(p1x, p1y, p2x, p2y, q1x, q1y);
        int d4 = Orientation(p1x, p1y, p2x, p2y, q2x, q2y);
        if (d1 != d2 && d3 != d4 && d1 != 0 && d2 != 0 && d3 != 0 && d4 != 0)
        {
            return true;
        }

        return (d1 == 0 && OnSegment(q1x, q1y, q2x, q2y, p1x, p1y))
            || (d2 == 0 && OnSegment(q1x, q1y, q2x, q2y, p2x, p2y))
            || (d3 == 0 && OnSegment(p1x, p1y, p2x, p2y, q1x, q1y))
            || (d4 == 0 && OnSegment(p1x, p1y, p2x, p2y, q2x, q2y));
    }

    /// <summary>Sign of the cross product (b − a) × (c − a).</summary>
    private static int Orientation(long ax, long ay, long bx, long by, long cx, long cy) =>
        Math.Sign(((bx - ax) * (cy - ay)) - ((by - ay) * (cx - ax)));

    /// <summary>c, known collinear with a→b, lies within the segment's bounding box.</summary>
    private static bool OnSegment(long ax, long ay, long bx, long by, long cx, long cy) =>
        cx >= Math.Min(ax, bx) && cx <= Math.Max(ax, bx) && cy >= Math.Min(ay, by) && cy <= Math.Max(ay, by);
}
