using System.Collections.Immutable;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

using Effects = Dungeon.Core.Effects.Effects;

/// <summary>A scripted contest sequence replays identically, survives a save and load, and leaves exploration alone (M1-08 Task 4).</summary>
public class ContestReplayTests
{
    private static readonly UnitId Foe = ContestKit.Foe;
    private static readonly RulesContext Rules = ContestKit.Rules;

    // The hero has advantage on attacks; the foe has disadvantage on its own.
    private static GameState Start(ulong seed = 7, bool withEffects = true) =>
        ContestKit.StateOf(
            ContestKit.Block(ContestKit.Zero.With(Ability.Strength, 3).With(Ability.Intelligence, 2).With(Ability.Wisdom, 1), 3, Save.Will, Skill.Athletics),
            ContestKit.Block(ContestKit.Zero.With(Ability.Dexterity, 1).With(Ability.Intelligence, 1), 2, Save.Reflex),
            seed,
            withEffects ? [(Hero, "adv"), (Foe, "dis")] : []);

    private delegate (object Result, GameState Next) Step(GameState s);

    private static readonly Step[] Script =
    [
        s => { var (o, n) = Initiative.Roll(Rules, s, [Foe, Hero]); return (o, n); },
        s => { var (r, n) = Contests.Attack(Rules, s, Hero, Foe, new AttackSpec(Ability.Strength, true)); return (r, n); },
        s => { var (r, n) = Contests.SaveRoll(Rules, s, Foe, Save.Will, Effects.Number(Rules, s, Hero, "abilityDifficulty")); return (r, n); },
        s => { var (r, n) = Contests.SkillVsUnit(Rules, s, Hero, Skill.Athletics, Foe, Defence.Reflex); return (r, n); },
        s => { var (r, n) = Contests.SkillCheck(Rules, s, Hero, Skill.Athletics, 15); return (r, n); },
        s => { var (r, n) = Contests.Attack(Rules, s, Foe, Hero, new AttackSpec(Ability.Strength, false)); return (r, n); },
    ];

    private static string Canon(object result) => result switch
    {
        ImmutableArray<InitiativeEntry> order => string.Join("; ", order.Select(e => e.ToString())),
        _ => result.ToString()!,
    };

    private static IEnumerable<int> Naturals(object result) => result switch
    {
        // Drawn in ascending id order; the list is sorted by outcome.
        ImmutableArray<InitiativeEntry> order => order.OrderBy(e => e.Unit.Value).Select(e => e.Natural),
        AttackResult a => Dice(a.Roll),
        CheckResult c => Dice(c.Roll),
        _ => throw new InvalidOperationException(),
    };

    private static int[] Dice(D20Roll roll) => roll.Second is { } second ? [roll.First, second] : [roll.First];

    private static (List<object> Results, GameState Final) Run(GameState s, IEnumerable<Step> steps)
    {
        var results = new List<object>();
        foreach (var step in steps)
        {
            var (result, next) = step(s);
            results.Add(result);
            s = next;
        }

        return (results, s);
    }

    private static List<string> Canonical(IEnumerable<object> results) => [.. results.Select(Canon)];

    [Fact]
    public void A_scripted_contest_sequence_replays_identically()
    {
        var (a, finalA) = Run(Start(), Script);
        var (b, finalB) = Run(Start(), Script);

        Assert.Equal(Canonical(a), Canonical(b));
        Assert.Equal(Snapshot.Save(finalA), Snapshot.Save(finalB));
        Assert.Equal(StateDigest.Bytes(finalA), StateDigest.Bytes(finalB));
        Assert.Equal((ulong)a.SelectMany(Naturals).Count(), finalA.Rng.Combat.Counter);
        Assert.Equal(Start().Rng.Shrine, finalA.Rng.Shrine);
    }

    [Fact]
    public void A_contest_sequence_continues_identically_after_a_save_and_load()
    {
        var (whole, finalWhole) = Run(Start(), Script);

        var (head, mid) = Run(Start(), Script.Take(3));
        var (tail, finalSplit) = Run(Snapshot.Load(Snapshot.Save(mid)), Script.Skip(3));

        Assert.Equal(Canonical(whole), Canonical(head.Concat(tail)));
        Assert.Equal(Snapshot.Save(finalWhole), Snapshot.Save(finalSplit));
        Assert.Equal(StateDigest.Bytes(finalWhole), StateDigest.Bytes(finalSplit));
    }

    // The naturals the script draws on seed 7, in order: initiative (hero, foe), the advantage attack (two dice), the
    // save, the skill against Reflex, the skill check (one die each), the disadvantage attack (two dice).
    private static readonly int[] Seed7Naturals = [9, 4, 18, 3, 15, 1, 13, 10, 2];

    [Fact]
    public void The_seed_7_contest_dice_are_pinned()
    {
        var (results, _) = Run(Start(7), Script);
        Assert.Equal(Seed7Naturals, results.SelectMany(Naturals).ToArray());
    }

    [Fact]
    public void A_move_leaves_the_combat_counter_unchanged()
    {
        // The engine's own rules know no test effects, so the fight here runs without them.
        var (_, fought) = Run(Start(7, withEffects: false), Script);
        Assert.True(fought.Rng.Combat.Counter > 0);

        var moved = Engine.Apply(fought, new MoveCommand(Hero, Direction.E)).State;

        Assert.NotEqual(fought.FindUnit(Hero)!.Pos, moved.FindUnit(Hero)!.Pos);
        Assert.Equal(fought.Rng.Combat, moved.Rng.Combat);
    }
}
