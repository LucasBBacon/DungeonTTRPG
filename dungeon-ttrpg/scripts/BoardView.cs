using System.Collections.Generic;
using Dungeon.Core;
using Godot;
using CoreGridMap = Dungeon.Core.GridMap;

namespace DungeonTTRPG;

/// <summary>Draws the map as placeholder boxes, one node per square. The board's top lies on y = 0.</summary>
public partial class BoardView : Node3D
{
    private const float FloorThickness = 0.05f;
    private const float WallHeight = 0.5f;
    private const float ShrineHeight = 0.2f;

    private readonly Dictionary<GridPos, Node3D> _squares = new();

    private readonly BoxMesh _floorMesh = Box(new Vector3(0.96f, FloorThickness, 0.96f), new Color(0.55f, 0.55f, 0.55f));
    private readonly BoxMesh _wallMesh = Box(new Vector3(1, WallHeight, 1), new Color(0.16f, 0.16f, 0.2f));
    private readonly BoxMesh _shrineMesh = Box(new Vector3(0.6f, ShrineHeight, 0.6f), new Color(1.0f, 0.8f, 0.1f));

    /// <summary>Discards whatever is drawn and draws every square of <paramref name="map"/>.</summary>
    public void Build(CoreGridMap map)
    {
        foreach (var square in _squares.Values)
        {
            square.QueueFree();
        }

        _squares.Clear();
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                var pos = new GridPos(x, y);
                SetTile(pos, map[pos]);
            }
        }
    }

    /// <summary>Redraws the one square at <paramref name="pos"/> as <paramref name="tile"/>.</summary>
    public void SetTile(GridPos pos, Tile tile)
    {
        if (_squares.Remove(pos, out var old))
        {
            old.QueueFree();
        }

        var square = new Node3D { Position = GridCoords.ToWorld(pos) };
        switch (tile)
        {
            case Tile.Wall:
                AddBox(square, _wallMesh, WallHeight / 2);
                break;
            case Tile.Shrine:
                AddBox(square, _floorMesh, -FloorThickness / 2);
                AddBox(square, _shrineMesh, ShrineHeight / 2);
                break;
            default:
                AddBox(square, _floorMesh, -FloorThickness / 2);
                break;
        }

        AddChild(square);
        _squares[pos] = square;
    }

    private static void AddBox(Node3D square, BoxMesh mesh, float centreY) =>
        square.AddChild(new MeshInstance3D { Mesh = mesh, Position = new Vector3(0, centreY, 0) });

    private static BoxMesh Box(Vector3 size, Color colour) =>
        new() { Size = size, Material = new StandardMaterial3D { AlbedoColor = colour } };
}
