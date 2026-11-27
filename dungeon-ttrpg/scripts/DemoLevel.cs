using System.Collections.Immutable;
using Dungeon.Core;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
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

    private static readonly UnitStats HeroStats =
        new(1, new Abilities(0, 0, 0, 0, 0, 0), Ability.Strength, Save.Fortitude, ImmutableArray<Skill>.Empty,
            HpTier.Slight, 0);

    /// <summary>
    /// The starting state of the demo run, the hero at full HP. Always the same. Full HP is the derived maximum
    /// (<see cref="HitPoints.Max"/>, DH 7.1), so the formula lives in one place: the state is built once with a
    /// placeholder HP of 0 to read the maximum, then built again with it.
    /// </summary>
    public static GameState CreateState()
    {
        var provisional = Build(0);
        return Build((int)HitPoints.Max(RulesContext.Empty, provisional, Hero));
    }

    private static GameState Build(int heroHp) =>
        GameState.Create(
            CoreGridMap.Parse(Rows),
            [new Unit(Hero, HeroStart, Faction.Party, ImmutableArray<int>.Empty, HeroStats, heroHp)],
            Seed,
            hero: Hero);
}
