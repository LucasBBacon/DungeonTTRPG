using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// Pools (M1-10 task 2; AB 6.2, 6.4): derived maximums, stored spent counts, the five refill tags and the one camp
/// spend per turn.
/// </summary>
public class PoolTests
{
    // The fixture pools. The level pool is spelt pool.by_level: pool ids are content ids ([a-z0-9_.:-]).
    public static readonly ImmutableArray<PoolDefinition> FixturePools =
    [
        new("pool.turn", RefillTag.Turn, _ => 1),
        new("pool.fight", RefillTag.Fight, _ => 1),
        new("pool.rest", RefillTag.Rest, _ => 2),
        new("pool.camp", RefillTag.Camp, _ => 1),
        new("pool.never", RefillTag.Never, _ => 3),
        new("pool.by_level", RefillTag.Rest, a => a.Unit.Stats.Level < 5 ? 1 : 2),
    ];

    public static readonly RulesContext Rules = RulesContext.Create(ContestKit.LoadEffects(), pools: FixturePools);

    private static readonly string[] AllPools =
        ["pool.turn", "pool.fight", "pool.rest", "pool.camp", "pool.never", "pool.by_level"];

    private static readonly UnitId Second = new(2);

    private static GameState Start(int level = 1) =>
        GameState.Create(
            GridMap.Parse("....."),
            [U(1, 0, 0) with { Stats = Fixtures.Stats with { Level = level } }, U(2, 1, 0)],
            7);

    private static GameState SpendOk(GameState s, UnitId unit, string pool, int count = 1)
    {
        var (ok, next) = Pools.Spend(Rules, s, unit, pool, count);
        Assert.True(ok, $"spending {count} of {pool} was refused");
        return next;
    }

    private static void AssertRefused(GameState s, UnitId unit, string pool, int count = 1)
    {
        var (ok, next) = Pools.Spend(Rules, s, unit, pool, count);
        Assert.False(ok);
        Assert.Same(s, next);
    }

    private static GameState SpendOneOfEach(GameState s, UnitId unit)
    {
        foreach (var pool in AllPools)
        {
            s = SpendOk(s, unit, pool);
        }

        return s;
    }

    [Fact]
    public void The_maximum_is_derived_and_current_is_maximum_minus_spent()
    {
        var s = Start();
        Assert.Equal(2, Pools.Max(Rules, s, Hero, "pool.rest"));
        Assert.Equal(0, Pools.Spent(s, Hero, "pool.rest"));
        Assert.Equal(2, Pools.Current(Rules, s, Hero, "pool.rest"));
        s = SpendOk(s, Hero, "pool.rest");
        Assert.Equal(1, Pools.Spent(s, Hero, "pool.rest"));
        Assert.Equal(1, Pools.Current(Rules, s, Hero, "pool.rest"));
        Assert.Equal(1, Pools.Max(Rules, s, Hero, "pool.by_level"));
        Assert.Equal(2, Pools.Max(Rules, Start(level: 5), Hero, "pool.by_level"));
    }

    [Theory]
    [InlineData(RefillTag.Turn, new[] { "pool.turn" })]
    [InlineData(RefillTag.Fight, new[] { "pool.fight" })]
    [InlineData(RefillTag.Rest, new[] { "pool.rest", "pool.by_level" })]
    [InlineData(RefillTag.Camp, new[] { "pool.rest", "pool.by_level", "pool.camp" })]
    public void Each_refill_tag_resets_only_its_own_pools(RefillTag tag, string[] refilled)
    {
        var spent = SpendOneOfEach(SpendOneOfEach(Start(), Hero), Second);
        foreach (var next in new[] { Pools.Refill(Rules, spent, Hero, tag), Pools.RefillAll(Rules, spent, tag) })
        {
            foreach (var pool in AllPools)
            {
                Assert.Equal(refilled.Contains(pool) ? 0 : 1, Pools.Spent(next, Hero, pool));
            }

            Assert.Equal(1, Pools.Spent(next, Hero, "pool.never"));
        }

        // Refill touches only the unit named; RefillAll every unit.
        Assert.All(AllPools, p => Assert.Equal(1, Pools.Spent(Pools.Refill(Rules, spent, Hero, tag), Second, p)));
        var all = Pools.RefillAll(Rules, spent, tag);
        Assert.All(AllPools, p => Assert.Equal(refilled.Contains(p) ? 0 : 1, Pools.Spent(all, Second, p)));
    }

    [Fact]
    public void A_never_pool_cannot_be_refilled()
    {
        var s = SpendOk(Start(), Hero, "pool.never");
        Assert.Throws<ArgumentException>(() => Pools.Refill(Rules, s, Hero, RefillTag.Never));
        Assert.Throws<ArgumentException>(() => Pools.RefillAll(Rules, s, RefillTag.Never));
        Assert.Throws<ArgumentOutOfRangeException>(() => Pools.Refill(Rules, s, Hero, (RefillTag)9));
        Assert.Throws<ArgumentOutOfRangeException>(() => Pools.RefillAll(Rules, s, (RefillTag)9));
        foreach (var tag in new[] { RefillTag.Turn, RefillTag.Fight, RefillTag.Rest, RefillTag.Camp })
        {
            Assert.Equal(1, Pools.Spent(Pools.RefillAll(Rules, s, tag), Hero, "pool.never"));
        }
    }

    [Fact]
    public void Spent_counts_are_stored_only_when_non_zero_and_sorted()
    {
        var s = Start();
        s = SpendOk(s, Hero, "pool.turn");
        s = SpendOk(s, Hero, "pool.rest", 2);
        s = SpendOk(s, Hero, "pool.by_level");
        s = SpendOk(s, Hero, "pool.camp");
        Assert.Equal(
            [new PoolSpend("pool.by_level", 1), new PoolSpend("pool.camp", 1), new PoolSpend("pool.rest", 2), new PoolSpend("pool.turn", 1)],
            s.FindUnit(Hero)!.Spent.ToArray());
        Assert.Empty(s.FindUnit(Second)!.Spent);

        s = Pools.Refill(Rules, s, Hero, RefillTag.Rest);
        Assert.Equal(
            [new PoolSpend("pool.camp", 1), new PoolSpend("pool.turn", 1)],
            s.FindUnit(Hero)!.Spent.ToArray());

        // What the pools write is what GameState.Create accepts: the save round-trips.
        var json = Snapshot.Save(s);
        Assert.Equal(json, Snapshot.Save(Snapshot.Load(json)));

        s = Pools.RefillAll(Rules, Pools.RefillAll(Rules, s, RefillTag.Turn), RefillTag.Camp);
        Assert.Empty(s.FindUnit(Hero)!.Spent);
    }

    [Fact]
    public void A_spend_beyond_current_is_refused()
    {
        var s = Start();
        AssertRefused(s, Hero, "pool.rest", 3);
        s = SpendOk(s, Hero, "pool.rest");
        AssertRefused(s, Hero, "pool.rest", 2);
        s = SpendOk(s, Hero, "pool.rest");
        AssertRefused(s, Hero, "pool.rest");
        Assert.Equal(0, Pools.Current(Rules, s, Hero, "pool.rest"));

        Assert.Throws<ArgumentOutOfRangeException>(() => Pools.Spend(Rules, s, Hero, "pool.never", 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Pools.Spend(Rules, s, Hero, "pool.never", -1));
        Assert.Throws<ArgumentException>(() => Pools.Spend(Rules, s, Hero, "pool.unknown", 1));
        Assert.Throws<ArgumentException>(() => Pools.Spend(Rules, s, new UnitId(9), "pool.never", 1));
        Assert.Throws<ArgumentException>(() => Pools.Max(Rules, s, Hero, "pool.unknown"));
        Assert.Throws<ArgumentException>(() => Pools.Spent(s, new UnitId(9), "pool.rest"));
    }

    // Review Focus 4: a pool spent above a lowered maximum reads 0, never -1, and its refill clears it.
    [Fact]
    public void A_pool_spent_above_a_lowered_maximum_reads_zero_and_its_refill_clears_it()
    {
        var s = SpendOk(Start(level: 5), Hero, "pool.by_level", 2);
        Assert.Equal(0, Pools.Current(Rules, s, Hero, "pool.by_level"));

        // The level drops to 4 through state rebuilt for the test: no rule lowers a level.
        var hero = s.FindUnit(Hero)!;
        s = s with { Units = s.Units.SetItem(s.Units.IndexOf(hero), hero with { Stats = hero.Stats with { Level = 4 } }) };
        Assert.Equal(1, Pools.Max(Rules, s, Hero, "pool.by_level"));
        Assert.Equal(2, Pools.Spent(s, Hero, "pool.by_level"));
        Assert.Equal(0, Pools.Current(Rules, s, Hero, "pool.by_level"));
        AssertRefused(s, Hero, "pool.by_level");

        s = Pools.Refill(Rules, s, Hero, RefillTag.Rest);
        Assert.Equal(0, Pools.Spent(s, Hero, "pool.by_level"));
        Assert.Equal(1, Pools.Current(Rules, s, Hero, "pool.by_level"));
    }

    // AB 6.4: "A unit may spend from a camp pool at most once per turn."
    [Fact]
    public void One_camp_spend_per_turn()
    {
        var s = SpendOk(Start(), Hero, "pool.camp");
        Assert.True(s.FindUnit(Hero)!.Budget.CampSpent);

        // Refilling the pool leaves the flag set, so the second spend is refused by the flag, not by the maximum.
        s = Pools.Refill(Rules, s, Hero, RefillTag.Camp);
        Assert.Equal(1, Pools.Current(Rules, s, Hero, "pool.camp"));
        AssertRefused(s, Hero, "pool.camp");

        // The flag does not touch other pools.
        var budget = s.FindUnit(Hero)!.Budget;
        s = SpendOk(s, Hero, "pool.rest");
        Assert.Equal(budget, s.FindUnit(Hero)!.Budget);
    }

    [Fact]
    public void A_spend_from_a_pool_that_is_not_camp_leaves_the_budget_alone()
    {
        var s = Start();
        foreach (var pool in new[] { "pool.turn", "pool.fight", "pool.rest", "pool.never", "pool.by_level" })
        {
            s = SpendOk(s, Hero, pool);
        }

        Assert.Equal(default, s.FindUnit(Hero)!.Budget);
    }

    // AB 16, "including reactions": the flag is the spender's, whoever holds the turn.
    [Fact]
    public void A_camp_spend_on_another_units_turn_uses_the_same_flag()
    {
        var s = Start() with { Combat = new CombatState([Hero, Second], 0, 1) };
        Assert.Equal(Hero, s.Combat!.CurrentUnit);

        s = SpendOk(s, Second, "pool.camp");
        Assert.True(s.FindUnit(Second)!.Budget.CampSpent);
        Assert.False(s.FindUnit(Hero)!.Budget.CampSpent);

        // Unit 2 cannot spend again before the next turn start, even with the pool refilled.
        s = Pools.Refill(Rules, s, Second, RefillTag.Camp);
        AssertRefused(s, Second, "pool.camp");

        // Unit 1's own flag is untouched by unit 2's spend.
        s = SpendOk(s, Hero, "pool.camp");
        Assert.True(s.FindUnit(Hero)!.Budget.CampSpent);
    }

    [Fact]
    public void The_rules_context_holds_the_pools_sorted_and_finds_them_by_id()
    {
        Assert.Equal(
            ["pool.by_level", "pool.camp", "pool.fight", "pool.never", "pool.rest", "pool.turn"],
            Rules.Pools.Select(p => p.Id).ToArray());
        Assert.Equal(RefillTag.Camp, Rules.Pool("pool.camp").Refill);
        Assert.Throws<ArgumentException>(() => Rules.Pool("pool.unknown"));
        Assert.Empty(RulesContext.Empty.Pools);
        Assert.Empty(RulesContext.Create(ContestKit.LoadEffects()).Pools);
        Assert.Throws<ArgumentException>(() => RulesContext.Empty.Pool("pool.camp"));
    }

    public static TheoryData<PoolDefinition?[]> BadPoolTables() => new()
    {
        new PoolDefinition?[] { null },
        new PoolDefinition?[] { new(null!, RefillTag.Rest, _ => 1) },
        new PoolDefinition?[] { new("pool.a", RefillTag.Rest, null!) },
        new PoolDefinition?[] { new("pool.a", RefillTag.Rest, _ => 1), new("pool.a", RefillTag.Camp, _ => 1) },
        new PoolDefinition?[] { new("pool.a", (RefillTag)9, _ => 1) },
        new PoolDefinition?[] { new("Pool.A", RefillTag.Rest, _ => 1) },
        new PoolDefinition?[] { new("pool a", RefillTag.Rest, _ => 1) },
        new PoolDefinition?[] { new(string.Empty, RefillTag.Rest, _ => 1) },
    };

    [Theory]
    [MemberData(nameof(BadPoolTables))]
    public void A_bad_pool_table_is_rejected(PoolDefinition?[] pools)
    {
        var ex = Assert.Throws<ArgumentException>(
            () => RulesContext.Create(ContestKit.LoadEffects(), pools: [.. pools.Select(p => p!)]));
        Assert.Equal("pools", ex.ParamName);
    }
}
