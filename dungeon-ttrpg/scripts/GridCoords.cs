using Dungeon.Core;
using Godot;

namespace DungeonTTRPG;

/// <summary>
/// The one place where screen and world positions become grid squares, and back.
/// One grid square is one world unit: grid X is world X, grid Y is world Z, the board lies on y = 0.
/// </summary>
public static class GridCoords
{
    /// <summary>The world position of the centre of square <paramref name="p"/>, on the board plane.</summary>
    public static Vector3 ToWorld(GridPos p) => new(p.X + 0.5f, 0, p.Y + 0.5f);

    /// <summary>The square containing world position <paramref name="v"/>. It may lie outside any map.</summary>
    public static GridPos FromWorld(Vector3 v) => new(Mathf.FloorToInt(v.X), Mathf.FloorToInt(v.Z));

    /// <summary>
    /// The square under a screen position: the ray from <paramref name="camera"/> through
    /// <paramref name="screen"/> is intersected with the board plane y = 0. Null when the ray
    /// never meets the plane. The square may lie outside any map.
    /// </summary>
    public static GridPos? Pick(Camera3D camera, Vector2 screen)
    {
        var origin = camera.ProjectRayOrigin(screen);
        var direction = camera.ProjectRayNormal(screen);
        var hit = new Plane(Vector3.Up, 0).IntersectsRay(origin, direction);
        return hit is null ? null : FromWorld(hit.Value);
    }
}
