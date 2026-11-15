using System.Collections.Immutable;
using Dungeon.Core;
using CoreGridMap = Dungeon.Core.GridMap;

namespace DungeonTTRPG;

/// <summary>The fixed M0 demo level: a 12 by 8 walled room with two shrines and one hero.</summary>
public static class DemoLevel
{
    public const ulong Seed = 12345;

    public static readonly UnitId Hero = new(1);

    private static readonly GridPos HeroStart = new(1, 1);

    // Row 0 is Y = 0. The floor square at (8, 6) is sealed off by walls on purpose.
    private static readonly string[] Rows =
    [
        "############",
        "#.....#....#",
        "#.*...#....#",
        "#.....#..*.#",
        "#..##......#",
        "#......###.#",
        "#......#.#.#",
        "############",
    ];

    /// <summary>The starting state of the demo run. Always the same.</summary>
    public static GameState CreateState() =>
        GameState.Create(
            CoreGridMap.Parse(Rows),
            [new Unit(Hero, HeroStart, Faction.Party, ImmutableArray<int>.Empty)],
            Seed);
}
