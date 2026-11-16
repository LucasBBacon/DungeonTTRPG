using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Dungeon.Core;

/// <summary>Read-only questions about a game state. Queries never change the state.</summary>
public static class Queries
{
    /// <summary>
    /// The shortest sequence of steps that takes <paramref name="unit"/> to <paramref name="target"/>,
    /// obeying the same rules as <see cref="MoveCommand"/> (walls, corner cutting, other units block).
    /// Returns an empty array if the unit is already there, and null if the unit is unknown or the
    /// target is off the map, not walkable, occupied or unreachable. Among equally short paths the
    /// result is fixed: squares are explored in <see cref="DirectionExt.All"/> order.
    /// <para>
    /// The path is a route, not a guarantee that every step will be accepted in one go: it may
    /// cross a square that opens a decision (a shrine), after which moves are rejected until the
    /// decision is answered, and a decision already pending in <paramref name="s"/> is not
    /// considered. Apply the steps one at a time and stop when a decision becomes pending.
    /// </para>
    /// </summary>
    public static ImmutableArray<Direction>? FindPath(GameState s, UnitId unit, GridPos target)
    {
        ArgumentNullException.ThrowIfNull(s);

        var mover = s.FindUnit(unit);
        if (mover is null || !s.Map.IsWalkable(target))
        {
            return null;
        }

        if (mover.Pos == target)
        {
            return ImmutableArray<Direction>.Empty;
        }

        if (s.UnitAt(target) is not null)
        {
            return null;
        }

        // Breadth-first search over squares, indexed Y * Width + X.
        int width = s.Map.Width;
        int start = mover.Pos.Y * width + mover.Pos.X;
        int goal = target.Y * width + target.X;
        var visited = new bool[width * s.Map.Height];
        var cameFrom = new int[visited.Length];
        var cameBy = new Direction[visited.Length];
        var frontier = new Queue<GridPos>();

        visited[start] = true;
        frontier.Enqueue(mover.Pos);
        while (frontier.Count > 0)
        {
            var here = frontier.Dequeue();
            var standing = mover with { Pos = here };
            foreach (var d in DirectionExt.All)
            {
                if (Engine.CheckStep(s, standing, d) != CommandError.None)
                {
                    continue;
                }

                var next = here.Step(d);
                int index = next.Y * width + next.X;
                if (visited[index])
                {
                    continue;
                }

                visited[index] = true;
                cameFrom[index] = here.Y * width + here.X;
                cameBy[index] = d;
                if (index == goal)
                {
                    return Backtrack(cameFrom, cameBy, start, goal);
                }

                frontier.Enqueue(next);
            }
        }

        return null;
    }

    private static ImmutableArray<Direction> Backtrack(int[] cameFrom, Direction[] cameBy, int start, int goal)
    {
        var steps = new List<Direction>();
        for (int at = goal; at != start; at = cameFrom[at])
        {
            steps.Add(cameBy[at]);
        }

        steps.Reverse();
        return steps.ToImmutableArray();
    }
}
