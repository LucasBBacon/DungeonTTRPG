using System.Collections.Immutable;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

using Effects = Dungeon.Core.Effects.Effects;

/// <summary>Initiative on the combat stream: d20 + Intelligence, ties by Intelligence then unit id (M1-08 Task 4).</summary>
public class InitiativeTests
{
    private static readonly UnitId Foe = ContestKit.Foe;

    private static GameState Duel(ulong seed, Abilities hero, Abilities foe, params (UnitId, string)[] effects) =>
        ContestKit.StateOf(ContestKit.Block(hero), ContestKit.Block(foe), seed, effects);

    // The two dice a state gives, hero first: read from a copy, so the state itself is untouched.
    private static (int Hero, int Foe) Dice2(GameState s)
    {
        var (a, afterA) = Dice.D20(s.Rng.Combat, RollMode.Normal);
        var (b, _) = Dice.D20(afterA, RollMode.Normal);
        return (a.First, b.First);
    }

    private static RulesContext RulesWith(params string[] defs) => RulesContext.Create(ContestKit.LoadEffects(defs));

    // Gives one unit an "eq" initiative bonus of the given size, so that the totals of the duel become equal.
    private static (GameState State, RulesContext Rules) Level(GameState s, Abilities hero, Abilities foe)
    {
        var (h, f) = Dice2(s);
        long gap = (h + hero.Intelligence) - (f + foe.Intelligence);
        var rules = RulesWith(ContestKit.Def("eq", "gear", ContestKit.Add("initiative", (int)Math.Abs(gap))));
        if (gap == 0)
        {
            return (s, rules);
        }

        var lower = gap < 0 ? Hero : Foe;
        return (Effects.Apply(s, "eq", lower, new EffectSource(lower)), rules);
    }

    private static UnitId[] Ids(ImmutableArray<InitiativeEntry> order) => [.. order.Select(e => e.Unit)];

    [Fact]
    public void Initiative_orders_by_total_then_intelligence_then_id()
    {
        // The total decides first.
        var s = Duel(7, ContestKit.Zero, ContestKit.Zero);
        var (h, f) = Dice2(s);
        var (order, _) = Initiative.Roll(ContestKit.Rules, s, [Hero, Foe]);
        Assert.NotEqual(h, f);
        Assert.Equal(h > f ? Hero : Foe, order[0].Unit);
        Assert.Equal(Math.Max(h, f), order[0].Total);
        Assert.Equal(h, order.Single(e => e.Unit == Hero).Natural);
        Assert.Equal(f, order.Single(e => e.Unit == Foe).Natural);

        // Equal totals: the higher Intelligence goes first, though the foe has the higher id.
        var heroLow = ContestKit.Zero;
        var foeHigh = ContestKit.Zero.With(Ability.Intelligence, 2);
        var (tied, rules) = Level(Duel(7, heroLow, foeHigh), heroLow, foeHigh);
        var (byInt, _) = Initiative.Roll(rules, tied, [Hero, Foe]);
        Assert.Equal(byInt[0].Total, byInt[1].Total);
        Assert.Equal([Foe, Hero], Ids(byInt));
        Assert.Equal(2, byInt[0].Intelligence);

        // Equal totals and Intelligence: the lower id goes first, whatever the input order.
        var (even, evenRules) = Level(Duel(7, ContestKit.Zero, ContestKit.Zero), ContestKit.Zero, ContestKit.Zero);
        var (byId, _) = Initiative.Roll(evenRules, even, [Foe, Hero]);
        Assert.Equal(byId[0].Total, byId[1].Total);
        Assert.Equal(byId[0].Intelligence, byId[1].Intelligence);
        Assert.Equal([Hero, Foe], Ids(byId));
    }

    // Review Focus 5.
    [Fact]
    public void Initiative_draws_in_id_order_whatever_the_input_order()
    {
        var s = Duel(7, ContestKit.Zero, ContestKit.Zero);
        var (h, f) = Dice2(s);

        var (forward, forwardState) = Initiative.Roll(ContestKit.Rules, s, [Hero, Foe]);
        var (backward, backwardState) = Initiative.Roll(ContestKit.Rules, s, [Foe, Hero]);

        Assert.Equal(forward.ToArray(), backward.ToArray());
        Assert.Equal(Snapshot.Save(forwardState), Snapshot.Save(backwardState));
        Assert.Equal(s.Rng.Combat.Counter + 2, forwardState.Rng.Combat.Counter);
        Assert.Equal(h, forward.Single(e => e.Unit == Hero).Natural);
        Assert.Equal(f, forward.Single(e => e.Unit == Foe).Natural);
        Assert.Equal(s.Rng.Shrine, forwardState.Rng.Shrine);
    }

    [Fact]
    public void Initiative_rejects_unknown_repeated_or_no_units()
    {
        var s = Duel(7, ContestKit.Zero, ContestKit.Zero);
        var before = Snapshot.Save(s);

        Assert.ThrowsAny<ArgumentException>(() => Initiative.Roll(ContestKit.Rules, s, null!));
        Assert.ThrowsAny<ArgumentException>(() => Initiative.Roll(ContestKit.Rules, s, []));
        Assert.ThrowsAny<ArgumentException>(() => Initiative.Roll(ContestKit.Rules, s, [Hero, new UnitId(99)]));
        Assert.ThrowsAny<ArgumentException>(() => Initiative.Roll(ContestKit.Rules, s, [Hero, Foe, Hero]));
        Assert.Equal(before, Snapshot.Save(s));
    }

    [Fact]
    public void Initiative_reads_effects_on_intelligence()
    {
        // +2 Intelligence on the foe raises both its total and its tie-break.
        var s = Duel(7, ContestKit.Zero, ContestKit.Zero, (Foe, "int.plus2"));
        var (h, f) = Dice2(s);
        long gap = f + 2 - h;
        var rules = RulesWith(
            ContestKit.Def("int.plus2", "gear", ContestKit.Add("intelligence", 2)),
            ContestKit.Def("eq", "gear", ContestKit.Add("initiative", (int)gap)));
        s = ContestKit.StateOf(ContestKit.Block(ContestKit.Zero), ContestKit.Block(ContestKit.Zero), 7, (Foe, "int.plus2"));

        var (plain, _) = Initiative.Roll(rules, s, [Hero, Foe]);
        var foeEntry = plain.Single(e => e.Unit == Foe);
        Assert.Equal(2, foeEntry.Intelligence);
        Assert.Equal(f + 2, foeEntry.Total);

        // Give the hero an initiative bonus that levels the totals: the foe wins on Intelligence, not on id.
        var tied = Effects.Apply(s, "eq", Hero, new EffectSource(Hero));
        var (order, _) = Initiative.Roll(rules, tied, [Hero, Foe]);
        Assert.Equal(order[0].Total, order[1].Total);
        Assert.Equal([Foe, Hero], Ids(order));
        Assert.Equal(0, order[1].Intelligence);
    }
}
