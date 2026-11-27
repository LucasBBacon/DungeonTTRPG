using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>Natural 20 and natural 1 on attacks only; ties succeed (M1-08 Task 3, Review Focus 4).</summary>
public class NaturalRollTests
{
    private static readonly UnitId Foe = ContestKit.Foe;

    // Seeds whose combat stream gives these first d20 dice, the first match in seeds 1..10000; the guard test below re-derives them.
    private const ulong Natural20Seed = 6; // first d20 = 20
    private const ulong Natural1Seed = 15;  // first d20 = 1
    private const ulong TenSeed = 13;       // first d20 = 10

    private static int FirstDie(ulong seed) =>
        Dice.D20(ContestKit.StateOf(ContestKit.Block(ContestKit.Zero), ContestKit.Block(ContestKit.Zero), seed).Rng.Combat, RollMode.Normal).Roll.First;

    // Test-only: the first seed in 1..10000 whose combat stream yields the wanted first die.
    private static ulong SeedFor(int die)
    {
        for (ulong seed = 1; seed <= 10000; seed++)
        {
            if (FirstDie(seed) == die)
            {
                return seed;
            }
        }

        throw new InvalidOperationException($"no seed in 1..10000 gives {die}");
    }

    [Fact]
    public void The_pinned_seeds_still_give_their_dice()
    {
        Assert.Equal(20, FirstDie(Natural20Seed));
        Assert.Equal(1, FirstDie(Natural1Seed));
        Assert.Equal(10, FirstDie(TenSeed));
        Assert.Equal(Natural20Seed, SeedFor(20));
        Assert.Equal(Natural1Seed, SeedFor(1));
        Assert.Equal(TenSeed, SeedFor(10));
    }

    [Fact]
    public void The_kept_die_decides_naturals_not_the_discarded_one()
    {
        // A discarded natural 20 under disadvantage does not hit or crit.
        var miss = Contests.ResolveAttack(new ContestSetup(0, 30, RollMode.Disadvantage), new D20Roll(RollMode.Disadvantage, 20, 5));
        Assert.False(miss.Hit);
        Assert.False(miss.Crit);

        // A discarded natural 1 under advantage is not an automatic miss.
        var hit = Contests.ResolveAttack(new ContestSetup(0, 5, RollMode.Advantage), new D20Roll(RollMode.Advantage, 1, 12));
        Assert.True(hit.Hit);
        Assert.False(hit.Crit);

        // A kept natural 20 under advantage hits and crits whatever the target.
        var crit = Contests.ResolveAttack(new ContestSetup(0, 30, RollMode.Advantage), new D20Roll(RollMode.Advantage, 3, 20));
        Assert.True(crit.Hit);
        Assert.True(crit.Crit);
    }

    private static GameState Duel(ulong seed, Abilities hero, Abilities foe, params (UnitId, string)[] effects) =>
        ContestKit.StateOf(ContestKit.Block(hero), ContestKit.Block(foe), seed, effects);

    private static readonly AttackSpec Untrained = new(Ability.Strength, false);

    [Fact]
    public void A_natural_20_hits_and_crits_below_the_armour_class()
    {
        // Dex +5 gives AC 15; two -5 gear adds make the total 10, below it.
        var s = Duel(Natural20Seed, ContestKit.Zero, ContestKit.Zero.With(Ability.Dexterity, 5), (Hero, "gear.attack.minus5"), (Hero, "gear.attack.minus5"));
        var (r, _) = Contests.Attack(ContestKit.Rules, s, Hero, Foe, Untrained);
        Assert.Equal(20, r.Roll.Kept);
        Assert.True(r.Total < r.Target);
        Assert.True(r.Hit);
        Assert.True(r.Crit);
    }

    [Fact]
    public void A_natural_1_misses_above_the_armour_class()
    {
        var s = Duel(Natural1Seed, ContestKit.Zero.With(Ability.Strength, 5), ContestKit.Zero.With(Ability.Dexterity, -3), (Hero, "gear.attack.plus5"));
        var (r, _) = Contests.Attack(ContestKit.Rules, s, Hero, Foe, Untrained);
        Assert.Equal(1, r.Roll.Kept);
        Assert.True(r.Total > r.Target);
        Assert.False(r.Hit);
        Assert.False(r.Crit);
    }

    [Fact]
    public void A_save_with_a_natural_20_below_the_difficulty_fails()
    {
        var s = Duel(Natural20Seed, ContestKit.Zero, ContestKit.Zero);
        var (r, _) = Contests.SaveRoll(ContestKit.Rules, s, Hero, Save.Reflex, 30);
        Assert.Equal(20, r.Roll.Kept);
        Assert.True(r.Total < 30);
        Assert.False(r.Success);
    }

    [Fact]
    public void A_skill_check_with_a_natural_1_meeting_the_difficulty_succeeds()
    {
        var s = Duel(Natural1Seed, ContestKit.Zero.With(Ability.Strength, 5), ContestKit.Zero);
        var (r, _) = Contests.SkillCheck(ContestKit.Rules, s, Hero, Skill.Athletics, 6);
        Assert.Equal(1, r.Roll.Kept);
        Assert.Equal(6, r.Total);
        Assert.True(r.Success);
    }

    [Fact]
    public void A_total_equal_to_the_target_succeeds()
    {
        var s = Duel(TenSeed, ContestKit.Zero, ContestKit.Zero); // die 10, bonus 0, AC 10
        var (attack, _) = Contests.Attack(ContestKit.Rules, s, Hero, Foe, Untrained);
        Assert.Equal(attack.Total, attack.Target);
        Assert.True(attack.Hit);
        Assert.False(attack.Crit);

        var (check, _) = Contests.SkillCheck(ContestKit.Rules, s, Hero, Skill.Athletics, 10);
        Assert.Equal(check.Total, check.Target);
        Assert.True(check.Success);
    }

    [Fact]
    public void Crit_is_cleared_independently_of_hit()
    {
        var s = Duel(Natural20Seed, ContestKit.Zero, ContestKit.Zero);
        var (r, _) = Contests.Attack(ContestKit.Rules, s, Hero, Foe, Untrained);
        Assert.True(r.Crit);
        var downgraded = r with { Crit = false };
        Assert.True(downgraded.Hit);
        Assert.False(downgraded.Crit);
    }
}
