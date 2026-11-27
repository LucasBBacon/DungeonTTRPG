using System.Collections.Immutable;
using Dungeon.Core.Stats;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>Helpers shared by more than one test file.</summary>
internal static class Fixtures
{
    public static readonly UnitId Hero = new(1);

    /// <summary>
    /// Level 1, all abilities 0, main Strength, trained Fortitude, no skills, HP tier Slight (maximum 11 + 1 × 6 = 17).
    /// </summary>
    public static readonly UnitStats Stats =
        new(1, new Abilities(0, 0, 0, 0, 0, 0), Ability.Strength, Save.Fortitude, ImmutableArray<Skill>.Empty, HpTier.Slight, 0);

    /// <summary>The maximum HP of a unit on <see cref="Stats"/>: 11 + 1 × max(1, 6 + 0) = 17 (DH 7.1).</summary>
    public const int FullHp = 17;

    /// <summary>As <see cref="Stats"/>, but a monster's HP basis: HP tier Table, table HP 12.</summary>
    public static readonly UnitStats EnemyStats = Stats with { HpTier = HpTier.Table, TableHp = 12 };

    /// <summary>A party unit on <see cref="Stats"/> and full HP.</summary>
    public static Unit U(int id, int x, int y) => new(new UnitId(id), new(x, y), Faction.Party, [], Stats, FullHp);

    /// <summary>A one-row map with a shrine east of the hero: <c>.*.</c>, hero at (0,0), seed 7 unless given.</summary>
    public static GameState ShrineState(ulong seed = 7) =>
        GameState.Create(GridMap.Parse(".*."), new[] { U(1, 0, 0) }, seed);

    /// <summary>
    /// A 5x5 walled room with a pillar in the middle, hero at (1,1), seed 7, plus an optional extra unit.
    /// </summary>
    public static GameState RingState(Unit? extra = null)
    {
        var map = GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####");
        var units = new List<Unit> { U(1, 1, 1) };
        if (extra is not null)
        {
            units.Add(extra);
        }

        return GameState.Create(map, units, 7);
    }

    public static void AssertRejected(GameState s, Command c, CommandError e)
    {
        var r = Engine.Apply(s, c);
        Assert.Equal(e, r.Error); Assert.Empty(r.Events); Assert.Same(s, r.State);
    }
}
