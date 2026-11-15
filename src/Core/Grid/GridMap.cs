using System;
using System.Collections.Immutable;

namespace Dungeon.Core;

public enum Tile { Floor, Wall, Shrine }

/// <summary>
/// An immutable rectangular tile map. Row 0 is Y = 0 (the top), X grows east, Y grows south.
/// Tiles are stored flat, indexed <c>Y * Width + X</c>.
/// </summary>
public sealed class GridMap
{
    private const char FloorChar = '.';
    private const char WallChar = '#';
    private const char ShrineChar = '*';

    private readonly ImmutableArray<Tile> _tiles;

    private GridMap(int width, int height, ImmutableArray<Tile> tiles)
    {
        Width = width;
        Height = height;
        _tiles = tiles;
    }

    public int Width { get; }

    public int Height { get; }

    /// <summary>The tile at <paramref name="p"/>. Throws if <paramref name="p"/> is outside the map.</summary>
    public Tile this[GridPos p] => _tiles[IndexOf(p)];

    public bool InBounds(GridPos p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;

    /// <summary>True when <paramref name="p"/> is on the map and is not a wall.</summary>
    public bool IsWalkable(GridPos p) => InBounds(p) && _tiles[p.Y * Width + p.X] != Tile.Wall;

    /// <summary>A new map equal to this one except that <paramref name="p"/> holds <paramref name="t"/>.</summary>
    public GridMap With(GridPos p, Tile t)
    {
        if (t is not (Tile.Floor or Tile.Wall or Tile.Shrine))
        {
            throw new ArgumentOutOfRangeException(nameof(t), t, "Unknown tile.");
        }

        return new GridMap(Width, Height, _tiles.SetItem(IndexOf(p), t));
    }

    /// <summary>
    /// Builds a map from text rows, top row first: <c>#</c> wall, <c>.</c> floor, <c>*</c> shrine.
    /// Throws an <see cref="ArgumentException"/> naming the problem if there are no rows, a row is
    /// empty, rows differ in length, or a character is not a map character.
    /// </summary>
    public static GridMap Parse(params string[] rows)
    {
        ArgumentNullException.ThrowIfNull(rows);
        if (rows.Length == 0)
        {
            throw new ArgumentException("Malformed map: no rows.", nameof(rows));
        }

        if (rows[0] is null)
        {
            throw new ArgumentException("Malformed map: row 0 is null.", nameof(rows));
        }

        int width = rows[0].Length;
        if (width == 0)
        {
            throw new ArgumentException("Malformed map: row 0 is empty.", nameof(rows));
        }

        var tiles = ImmutableArray.CreateBuilder<Tile>(width * rows.Length);
        for (int y = 0; y < rows.Length; y++)
        {
            string row = rows[y] ?? throw new ArgumentException(
                $"Malformed map: row {y} is null.", nameof(rows));
            if (row.Length != width)
            {
                throw new ArgumentException(
                    $"Malformed map: ragged rows, row {y} has {row.Length} characters but row 0 has {width}.",
                    nameof(rows));
            }

            for (int x = 0; x < width; x++)
            {
                tiles.Add(row[x] switch
                {
                    FloorChar => Tile.Floor,
                    WallChar => Tile.Wall,
                    ShrineChar => Tile.Shrine,
                    _ => throw new ArgumentException(
                        $"Malformed map: unknown character '{row[x]}' at row {y}, column {x}.",
                        nameof(rows)),
                });
            }
        }

        return new GridMap(width, rows.Length, tiles.MoveToImmutable());
    }

    /// <summary>The map as text rows, top row first. Round-trips with <see cref="Parse"/>.</summary>
    public ImmutableArray<string> ToRows()
    {
        var rows = ImmutableArray.CreateBuilder<string>(Height);
        var buffer = new char[Width];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                buffer[x] = _tiles[y * Width + x] switch
                {
                    Tile.Floor => FloorChar,
                    Tile.Wall => WallChar,
                    Tile.Shrine => ShrineChar,
                    var t => throw new InvalidOperationException($"Unknown tile {t}."),
                };
            }

            rows.Add(new string(buffer));
        }

        return rows.MoveToImmutable();
    }

    private int IndexOf(GridPos p)
    {
        if (!InBounds(p))
        {
            throw new ArgumentOutOfRangeException(
                nameof(p), p, $"Position is outside the {Width}x{Height} map.");
        }

        return p.Y * Width + p.X;
    }
}
