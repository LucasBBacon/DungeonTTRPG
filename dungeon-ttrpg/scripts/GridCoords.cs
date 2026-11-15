using System;
using Dungeon.Core;
using Godot;

namespace DungeonTTRPG;

/// <summary>
/// The one place where screen and world positions become grid squares, and back.
/// One grid square is one world unit: grid X is world X, grid Y is world Z, the board lies on y = 0.
/// </summary>
public static class GridCoords
{
    // A ray must sink this far into a block to count as meeting it. At the isometric angle the
    // ray through a square's centre passes exactly through the top corner of the block in front
    // of it; without this, rounding would decide between the two.
    private const float GrazeTolerance = 0.001f;

    /// <summary>The world position of the centre of square <paramref name="p"/>, on the board plane.</summary>
    public static Vector3 ToWorld(GridPos p) => new(p.X + 0.5f, 0, p.Y + 0.5f);

    /// <summary>The square containing world position <paramref name="v"/>. It may lie outside any map.</summary>
    public static GridPos FromWorld(Vector3 v) => new(Mathf.FloorToInt(v.X), Mathf.FloorToInt(v.Z));

    /// <summary>
    /// The square under a screen position: the first square whose drawn block the ray from
    /// <paramref name="camera"/> through <paramref name="screen"/> meets on its way down to the
    /// board plane y = 0, or the square where it meets that plane. Each square is taken as a
    /// block filling its footprint from y = 0 up to <paramref name="drawnHeight"/> of it, which
    /// is never above <paramref name="maxHeight"/>. Null when the ray never comes down to the
    /// plane. The square may lie outside any map.
    /// </summary>
    public static GridPos? Pick(Camera3D camera, Vector2 screen, Func<GridPos, float> drawnHeight, float maxHeight)
    {
        var origin = camera.ProjectRayOrigin(screen);
        var direction = camera.ProjectRayNormal(screen);
        if (direction.Y >= 0)
        {
            return null;
        }

        float end = -origin.Y / direction.Y;
        if (end < 0)
        {
            return null;
        }

        // Walk the squares under the ray from where it drops below maxHeight to where it lands.
        float t = Mathf.Max(0, (maxHeight - origin.Y) / direction.Y);
        var start = origin + direction * t;
        var square = FromWorld(start);
        int x = square.X;
        int z = square.Y;
        int stepX = Math.Sign(direction.X);
        int stepZ = Math.Sign(direction.Z);
        float nextX = stepX == 0 ? float.PositiveInfinity : t + ((stepX > 0 ? x + 1 : x) - start.X) / direction.X;
        float nextZ = stepZ == 0 ? float.PositiveInfinity : t + ((stepZ > 0 ? z + 1 : z) - start.Z) / direction.Z;
        float perX = stepX == 0 ? float.PositiveInfinity : stepX / direction.X;
        float perZ = stepZ == 0 ? float.PositiveInfinity : stepZ / direction.Z;

        while (true)
        {
            var here = new GridPos(x, z);
            float exit = Mathf.Min(nextX, nextZ);
            if (exit >= end || drawnHeight(here) >= origin.Y + direction.Y * exit + GrazeTolerance)
            {
                return here;
            }

            if (nextX < nextZ)
            {
                x += stepX;
                nextX += perX;
            }
            else
            {
                z += stepZ;
                nextZ += perZ;
            }
        }
    }
}
