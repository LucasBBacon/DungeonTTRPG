using Dungeon.Core.Combat;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// Level-up HP (M1-10 task 2, plan call 13; DH 7.3): the increase in the maximum arrives as current HP, and a level-up
/// never refills anything (AB 6.2).
/// </summary>
public class LevelUpTests
{
    private static readonly TurnBudget UsedBudget = new(2, 1, true, false, true, true);

    // The reference hero (DH 7.1): Slight, Constitution +1, so the maximum is 18 at level 1 and 25 at level 2.
    private static GameState Reference(int level, int hp, int byLevelSpent) =>
        GameState.Create(
            GridMap.Parse("..."),
            [
                U(1, 0, 0) with
                {
                    Stats = Fixtures.Stats with { Level = level, Abilities = new Abilities(0, 0, 1, 0, 0, 0) },
                    Hp = hp,
                    Budget = UsedBudget,
                    Spent = [new PoolSpend("pool.by_level", byLevelSpent)],
                },
            ],
            7);

    [Fact]
    public void A_level_up_adds_the_max_hp_increase_and_leaves_spent_counts_alone()
    {
        var rules = PoolTests.Rules;
        var s = Reference(level: 1, hp: 10, byLevelSpent: 1);
        Assert.Equal(18, HitPoints.Max(rules, s, Hero));

        var up = HitPoints.RaiseLevel(rules, s, Hero, 2);
        var hero = up.FindUnit(Hero)!;
        Assert.Equal(2, hero.Stats.Level);
        Assert.Equal(25, HitPoints.Max(rules, up, Hero));
        Assert.Equal(17, hero.Hp);
        Assert.Equal(1, Pools.Spent(up, Hero, "pool.by_level"));
        Assert.Equal(UsedBudget, hero.Budget);
        Assert.Equal(1, s.FindUnit(Hero)!.Stats.Level);

        // Level 4 -> 5 raises pool.by_level's maximum from 1 to 2: current rises from 0 to 1 with nothing refilled.
        var four = Reference(level: 4, hp: 10, byLevelSpent: 1);
        Assert.Equal(0, Pools.Current(rules, four, Hero, "pool.by_level"));
        var five = HitPoints.RaiseLevel(rules, four, Hero, 5);
        Assert.Equal(1, Pools.Spent(five, Hero, "pool.by_level"));
        Assert.Equal(1, Pools.Current(rules, five, Hero, "pool.by_level"));
        Assert.Equal(UsedBudget, five.FindUnit(Hero)!.Budget);
    }

    [Fact]
    public void A_level_up_over_several_levels_adds_the_whole_increase()
    {
        // Level 1 (18) to 3 (32): Δ 14.
        var up = HitPoints.RaiseLevel(PoolTests.Rules, Reference(level: 1, hp: 10, byLevelSpent: 1), Hero, 3);
        Assert.Equal(24, up.FindUnit(Hero)!.Hp);
    }

    [Fact]
    public void A_level_up_to_a_bad_level_or_an_unknown_unit_throws()
    {
        var rules = PoolTests.Rules;
        var s = Reference(level: 4, hp: 10, byLevelSpent: 1);
        Assert.Throws<ArgumentOutOfRangeException>(() => HitPoints.RaiseLevel(rules, s, Hero, 3));
        Assert.Throws<ArgumentOutOfRangeException>(() => HitPoints.RaiseLevel(rules, s, Hero, 13));
        Assert.Throws<ArgumentOutOfRangeException>(() => HitPoints.RaiseLevel(rules, s, Hero, -1));
        Assert.Throws<ArgumentException>(() => HitPoints.RaiseLevel(rules, s, new UnitId(9), 5));
        Assert.Equal(10, HitPoints.RaiseLevel(rules, s, Hero, 4).FindUnit(Hero)!.Hp);
    }
}
