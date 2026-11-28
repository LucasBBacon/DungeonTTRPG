using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Conditions;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.ConditionFixtures;

namespace Dungeon.Core.Tests;

// The classes share their names with their namespaces, so name them explicitly inside this namespace.
using Conditions = Dungeon.Core.Conditions.Conditions;

/// <summary>
/// The CO 5.5 combat-start condition step and the combat-end ends (M1-12a Task 5; plan calls 14 and 15; M1-10 rulings
/// record 5 step 2 and 6.3): <c>combatStartEnds</c> ends before initiative with no Steeled, their messages held after
/// <see cref="CombatStarted"/>, the out-of-combat duration switch, the per-fight counter reset, the initiative dice, the
/// opener path and its preview; and at combat end every <c>combatOnly</c> instance and gone-source window ends.
/// </summary>
public class CombatStartConditionTests
{
    private static readonly UnitId U1 = new(1);
    private static readonly UnitId U2 = new(2);
    private static readonly UnitId U3 = new(3);
    private static readonly UnitId U4 = new(4);

    /// <summary>Minor, <c>turns:3</c>, <c>combatStartEnds</c>, initiative −50.</summary>
    private const string Groggy = "test.cs.groggy";

    /// <summary>As <see cref="Groggy"/> without <c>combatStartEnds</c>.</summary>
    private const string Slowpoke = "test.cs.slowpoke";

    /// <summary>Hard, <c>turns:1</c>, <c>combatStartEnds</c>.</summary>
    private const string Stupor = "test.cs.stupor";

    /// <summary>Minor, <c>turns:2</c>, out of combat <c>turns:10</c>: it switches at combat start.</summary>
    private const string Switches = "test.cs.switches";

    /// <summary>Minor, <c>turns:3</c>, out of combat <c>turns:2</c>: it keeps its terms at combat start.</summary>
    private const string Keeps = "test.cs.keeps";

    /// <summary>Minor, <c>turns:3</c>, <c>combatStartEnds</c>, disadvantage on its holder's attack rolls.</summary>
    private const string Fumble = "test.cs.fumble";

    /// <summary>As <see cref="Fumble"/> without <c>combatStartEnds</c>.</summary>
    private const string Clumsy = "test.cs.clumsy";

    /// <summary>Hard, <c>turns:1</c>, <c>combatOnly</c>.</summary>
    private const string HardFight = "test.cs.hard_fight";

    /// <summary>Minor, <c>turns:3</c>, <c>combatOnly</c>.</summary>
    private const string SoftFight = "test.cs.soft_fight";

    /// <summary>Hard, <c>trigger {unitMoved, cap 2}</c> (enemy-facing only), out of combat <c>turns:5</c>.</summary>
    private const string Snare = "test.cs.snare";

    /// <summary>Minor, <c>turns:2</c>, out of combat <c>turns:3</c>.</summary>
    private const string Lag = "test.cs.lag";

    private static readonly string[] Extra =
    [
        $$"""{"id": "{{Groggy}}", "polarity": "harmful", "tier": "minor", "flags": {"combatStartEnds": true}, "duration": [{"turns": 3}], "modifiers": [{"target": "initiative", "add": -50}]}""",
        $$"""{"id": "{{Slowpoke}}", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 3}], "modifiers": [{"target": "initiative", "add": -50}]}""",
        $$"""{"id": "{{Stupor}}", "polarity": "harmful", "tier": "hard", "flags": {"combatStartEnds": true}, "duration": [{"turns": 1}]}""",
        $$"""{"id": "{{Switches}}", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 2}], "outOfCombat": [{"turns": 10}]}""",
        $$"""{"id": "{{Keeps}}", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 3}], "outOfCombat": [{"turns": 2}]}""",
        $$"""{"id": "{{Fumble}}", "polarity": "harmful", "tier": "minor", "flags": {"combatStartEnds": true}, "duration": [{"turns": 3}], "modifiers": [{"target": "attackRoll", "rollMode": "disadvantage"}]}""",
        $$"""{"id": "{{Clumsy}}", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 3}], "modifiers": [{"target": "attackRoll", "rollMode": "disadvantage"}]}""",
        $$"""{"id": "{{HardFight}}", "polarity": "harmful", "tier": "hard", "flags": {"combatOnly": true}, "duration": [{"turns": 1}]}""",
        $$$"""{"id": "{{{Snare}}}", "polarity": "harmful", "tier": "hard", "duration": [{"trigger": {"event": "unitMoved", "cap": 2}}], "outOfCombat": [{"turns": 5}]}""",
        $$"""{"id": "{{Lag}}", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 2}], "outOfCombat": [{"turns": 3}]}""",
        $$"""{"id": "{{SoftFight}}", "polarity": "harmful", "tier": "minor", "flags": {"combatOnly": true}, "duration": [{"turns": 3}]}""",
    ];

    private static readonly RulesContext StartRules = RulesContext.Create(
        Load(EffectRecords, [.. Records, .. Extra]),
        pools: AbilityFixtures.PoolTable,
        abilities: AbilityFixtures.Definitions);

    private static readonly DamageRoll Lethal =
        new([new PartRoll(new DamagePart(DamageType.Slashing, 0, 0, 1000), [], 1000)], false);

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......");

    // Out of combat: party U1 (the hero) and U2, enemy U3, each holding the fixture abilities; U1 is beside U3.
    private static GameState Calm(ulong seed = 7) =>
        GameState.Create(
            Open,
            [AbilityFixtures.Party(1, 1, 1), AbilityFixtures.Party(2, 0, 2), AbilityFixtures.Foe(3, 2, 1)],
            seed,
            hero: U1);

    private static GameState Insert(GameState s, string id, UnitId holder, UnitId? source, ConditionInsert? options = null) =>
        Conditions.Insert(StartRules, s, id, holder, new EffectSource(source), options ?? ConditionInsert.None).Next;

    private static bool Holds(GameState s, string id, UnitId holder) => s.Effects.Any(e => e.EffectId == id && e.Holder == holder);

    private static ConditionState State(GameState s, string id, UnitId holder) =>
        Assert.Single(s.Effects, e => e.EffectId == id && e.Holder == holder).Condition!;

    private static long Seq(GameState s, string id, UnitId holder) =>
        Assert.Single(s.Effects, e => e.EffectId == id && e.Holder == holder).Seq;

    private static InitiativeEntry Entry(ImmutableArray<GameEvent> events, UnitId unit) =>
        Assert.Single(Assert.IsType<CombatStarted>(events[0]).Order, e => e.Unit == unit);

    // --- Before initiative (M1-10 rulings record 6.3, M1-12) ---

    [Fact]
    public void The_combat_start_step_runs_before_initiative()
    {
        var plain = Turns.BeginCombat(StartRules, Calm());
        var ended = Turns.BeginCombat(StartRules, Insert(Calm(), Groggy, U1, U3));
        var kept = Turns.BeginCombat(StartRules, Insert(Calm(), Slowpoke, U1, U3));

        // The fixture would change U1's initiative number; it is gone before the roll, so the roll ignores it.
        Assert.Equal(Entry(plain.Events, U1).Total - 50, Entry(kept.Events, U1).Total);
        Assert.Equal(Entry(plain.Events, U1), Entry(ended.Events, U1));
        Assert.Equal(plain.Next.Combat!.Order.AsEnumerable(), ended.Next.Combat!.Order);
        Assert.False(Holds(ended.Next, Groggy, U1));
    }

    [Fact]
    public void Its_ConditionEnded_messages_follow_CombatStarted()
    {
        var s = Insert(Calm(), Asleep, U3, U1);
        long a = Seq(s, Asleep, U3);
        s = Insert(s, Asleep, U1, U3);
        long b = Seq(s, Asleep, U1);
        s = Insert(s, Groggy, U1, U3);
        long c = Seq(s, Groggy, U1);
        Assert.True(a < b && b < c);

        var (events, next) = Turns.StartCombat(StartRules, s);

        Assert.Equal(
            [typeof(CombatStarted), typeof(ConditionEnded), typeof(ConditionEnded), typeof(ConditionEnded), typeof(TurnStarted)],
            events.Select(e => e.GetType()).ToArray());
        Assert.Equal(
            [
                new ConditionEnded(b, Asleep, U1, ConditionEndReason.CombatStart),
                new ConditionEnded(c, Groggy, U1, ConditionEndReason.CombatStart),
                new ConditionEnded(a, Asleep, U3, ConditionEndReason.CombatStart),
            ],
            events.OfType<ConditionEnded>().ToArray());
        Assert.Equal([b, c, a], events.OfType<ConditionEnded>().Select(e => e.Seq).ToArray());
        Assert.Equal([U1, U1, U3], events.OfType<ConditionEnded>().Select(e => e.Holder).ToArray());
        Assert.Empty(next.Effects);
    }

    [Fact]
    public void A_combatStart_end_grants_no_Steeled()
    {
        var s = Insert(Calm(), Stupor, U3, U1);
        var (events, next) = Turns.BeginCombat(StartRules, s);

        Assert.Equal(
            [new ConditionEnded(1, Stupor, U3, ConditionEndReason.CombatStart)],
            events.OfType<ConditionEnded>().ToArray());
        Assert.DoesNotContain(events, e => e is ConditionApplied);
        Assert.Empty(next.Effects);
    }

    [Fact]
    public void A_combatStart_end_ends_what_it_sustains_with_combatStart_and_no_Steeled()
    {
        // Asleep (combatStartEnds) on U1 sustains a hard Stun on U3: both end with CombatStart, and neither grants Steeled.
        var s = Insert(Calm(), Asleep, U1, U3);
        long sustainer = Seq(s, Asleep, U1);
        s = Insert(s, Stun, U3, U1, new ConditionInsert(SustainedBy: sustainer));
        long dependent = Seq(s, Stun, U3);

        var (events, next) = Turns.BeginCombat(StartRules, s);

        Assert.Equal<GameEvent>(
            [
                new ConditionEnded(sustainer, Asleep, U1, ConditionEndReason.CombatStart),
                new ConditionEnded(dependent, Stun, U3, ConditionEndReason.CombatStart),
            ],
            events.Skip(1).ToArray());
        Assert.IsType<CombatStarted>(events[0]);
        Assert.DoesNotContain(events, e => e is ConditionApplied);
        Assert.Empty(next.Effects);
        Assert.Empty(Turns.CombatStartView(StartRules, s).Effects);
    }

    [Fact]
    public void Out_of_combat_durations_switch_when_shorter()
    {
        // Both land out of combat on their out-of-combat terms, and one tick counts a turn on each.
        var s = Insert(Insert(Calm(), Switches, U1, U3), Keeps, U1, U3);
        Assert.Equal([new DurationTerm.Turns(10)], State(s, Switches, U1).Terms.ToArray());
        s = Engine.Apply(s, new MoveCommand(U2, Direction.N), StartRules).State;
        Assert.Equal(1, State(s, Switches, U1).TurnsCounted);
        Assert.Equal(1, State(s, Keeps, U1).TurnsCounted);

        var (events, next) = Turns.BeginCombat(StartRules, s);

        // Switches had 9 ticks left; its in-combat turns:2 is shorter, so it switches, counted from now.
        var switched = State(next, Switches, U1);
        Assert.Equal([new DurationTerm.Turns(2)], switched.Terms.ToArray());
        Assert.Equal(0, switched.TurnsCounted);
        Assert.Equal(State(s, Switches, U1).AppliedPhase, switched.AppliedPhase);

        // Keeps had 1 tick left; its in-combat turns:3 is longer, so it keeps its terms and count.
        Assert.Equal(State(s, Keeps, U1), State(next, Keeps, U1));

        // A switch is no message.
        Assert.Single(events);
    }

    [Fact]
    public void A_party_holder_is_refused_a_record_whose_in_combat_duration_breaks_the_party_band_out_of_combat_too()
    {
        // Final review C1/C7: the switch target would break the party band at the next fight start, so the insert is
        // refused where it is caused, in either branch.
        var ex = Assert.Throws<InvalidOperationException>(() => Insert(Calm(), Snare, U1, U3));
        Assert.Contains("party-facing hard", ex.Message);
        Assert.Throws<InvalidOperationException>(() => Insert(Turns.BeginCombat(StartRules, Calm()).Next, Snare, U1, U3));

        // An enemy holder of the same record takes its out-of-combat terms and switches at the fight start.
        var s = Insert(Calm(), Snare, U3, U1);
        Assert.Equal([new DurationTerm.Turns(5)], State(s, Snare, U3).Terms.ToArray());
        Assert.True(State(s, Snare, U3).OnOutOfCombatTerms);
        var (_, next) = Turns.BeginCombat(StartRules, s);
        Assert.Equal([new DurationTerm.TriggerEnd("unitMoved", 2)], State(next, Snare, U3).Terms.ToArray());
    }

    [Fact]
    public void An_override_equal_to_the_out_of_combat_list_never_switches()
    {
        // Without an override, Lag runs on its out-of-combat turns:3 and switches to its shorter turns:2.
        var plain = Insert(Calm(), Lag, U1, U3);
        Assert.True(State(plain, Lag, U1).OnOutOfCombatTerms);
        Assert.Equal([new DurationTerm.Turns(2)], State(Turns.BeginCombat(StartRules, plain).Next, Lag, U1).Terms.ToArray());

        // An override that happens to equal the list is the caller's duration: it is kept.
        var given = Insert(Calm(), Lag, U1, U3, new ConditionInsert(Duration: [new DurationTerm.Turns(3)]));
        Assert.False(State(given, Lag, U1).OnOutOfCombatTerms);
        var (_, next) = Turns.BeginCombat(StartRules, given);
        Assert.Equal(State(given, Lag, U1), State(next, Lag, U1));
        Assert.Equal([new DurationTerm.Turns(3)], State(next, Lag, U1).Terms.ToArray());

        // In a fight the record's own duration is never on its out-of-combat terms.
        var fight = Turns.BeginCombat(StartRules, Calm()).Next;
        Assert.False(State(Insert(fight, Lag, U1, U3), Lag, U1).OnOutOfCombatTerms);
    }

    [Fact]
    public void Per_fight_counters_reset()
    {
        var s = Calm();
        s = s with { Units = [.. s.Units.Select(u => u with { HardTurnsLostThisFight = 1 })] };
        var (_, next) = Turns.BeginCombat(StartRules, s);
        Assert.All(next.Units, u => Assert.Equal(0, u.HardTurnsLostThisFight));
    }

    [Fact]
    public void The_number_of_initiative_dice_is_unchanged()
    {
        var plain = Calm();
        var busy = Insert(Insert(Insert(Calm(), Asleep, U3, U1), Groggy, U1, U3), Switches, U2, U3);

        var (_, a) = Turns.BeginCombat(StartRules, plain);
        var (_, b) = Turns.BeginCombat(StartRules, busy);

        Assert.Equal(plain.Rng.Combat.Skip(3), a.Rng.Combat);
        Assert.Equal(a.Rng.Combat, b.Rng.Combat);
        Assert.Equal(plain.Rng, Turns.CombatStartView(StartRules, busy).Rng);
    }

    [Fact]
    public void The_combat_start_view_is_the_state_before_initiative()
    {
        var busy = Insert(Insert(Calm(), Groggy, U1, U3), Switches, U2, U3);
        var view = Turns.CombatStartView(StartRules, busy);

        Assert.Null(view.Combat);
        Assert.False(Holds(view, Groggy, U1));
        Assert.Equal([new DurationTerm.Turns(2)], State(view, Switches, U2).Terms.ToArray());
        Assert.Equal(StateDigest.Of(view with { Combat = null }), StateDigest.Of(Turns.FightReady(StartRules, Conditions.CombatStart(StartRules, busy).Next)));
    }

    // --- The opener path ---

    [Fact]
    public void An_openers_held_messages_sit_between_CombatStarted_and_AbilityUsed()
    {
        var s = Insert(Insert(Calm(), Asleep, U3, U1), Groggy, U1, U3);
        var r = Engine.Apply(s, new UseAbilityCommand(U1, AbilityFixtures.Sure, new UnitsTarget([U3])), StartRules);

        Assert.True(r.Ok, r.Error.ToString());
        var types = r.Events.Select(e => e.GetType()).ToList();
        Assert.Equal(typeof(CombatStarted), types[0]);
        Assert.Equal([typeof(ConditionEnded), typeof(ConditionEnded), typeof(AbilityUsed)], types.Skip(1).Take(3).ToArray());
        Assert.Equal([U1, U3], r.Events.OfType<ConditionEnded>().Select(e => e.Holder).ToArray());
    }

    [Fact]
    public void The_opener_preview_reads_the_combat_start_view()
    {
        var target = new UnitsTarget([U3]);
        var plain = Assert.Single(AbilityUse.Preview(StartRules, Calm(), U1, AbilityFixtures.Strike, target).Targets);
        var fumbling = Assert.Single(AbilityUse.Preview(StartRules, Insert(Calm(), Fumble, U1, U3), U1, AbilityFixtures.Strike, target).Targets);
        var clumsy = Assert.Single(AbilityUse.Preview(StartRules, Insert(Calm(), Clumsy, U1, U3), U1, AbilityFixtures.Strike, target).Targets);

        // Fumble ends at combat start, before the opener resolves: the preview's chance is resolution's.
        Assert.Equal(plain.Chance, fumbling.Chance);
        Assert.Equal(RollMode.Normal, fumbling.Setup?.Mode);
        Assert.NotEqual(plain.Chance, clumsy.Chance);
    }

    // --- Combat end (plan call 15) ---

    // A fight held before round 1 in the order [U4, U1, U2, U3]: U4 (party) holds the turn once round 1 starts; U3 is
    // the enemy.
    private static GameState Fight()
    {
        var calm = GameState.Create(
            Open,
            [
                AbilityFixtures.Party(1, 1, 1),
                AbilityFixtures.Party(2, 0, 2),
                AbilityFixtures.Foe(3, 2, 1),
                AbilityFixtures.Party(4, 5, 2),
            ],
            7,
            hero: U1);
        var held = calm with { Combat = new CombatState([U4, U1, U2, U3], 0, 1, BeforeRound1: true) };
        return Turns.BeginRound1(StartRules, held).Next;
    }

    [Fact]
    public void At_combat_end_combatOnly_instances_end_and_a_fresh_Steeled_ends_at_once()
    {
        var s = Insert(Fight(), HardFight, U1, U3);
        s = Insert(s, SoftFight, U1, U1);
        s = Insert(s, SoftFight, U2, U2);
        Assert.Equal([1L, 2L, 3L], s.Effects.Select(e => e.Seq).ToArray());
        s = Harm.Deal(StartRules, s, U3, Lethal).Next;

        var (events, next) = CombatEnd.EndIfWon(StartRules, s);

        Assert.Equal<GameEvent>(
            [
                new TurnEnded(U4),
                new ConditionEnded(1, HardFight, U1, ConditionEndReason.CombatEnd),
                new ConditionApplied(4, Conditions.SteeledId, U1, new EffectSource(U1), 1, true),
                new ConditionEnded(4, Conditions.SteeledId, U1, ConditionEndReason.CombatEnd),
                new ConditionEnded(2, SoftFight, U1, ConditionEndReason.CombatEnd),
                new ConditionEnded(3, SoftFight, U2, ConditionEndReason.CombatEnd),
                new CombatEnded(CombatEndReason.Win),
            ],
            events.ToArray());
        Assert.Empty(next.Effects);
    }

    [Fact]
    public void At_combat_end_a_window_whose_source_died_ends_SourceGone_and_nothing_combatOnly_survives()
    {
        var s = Insert(Fight(), MarkEnd, U2, U3);
        s = Insert(s, SoftFight, U1, U1);
        s = Insert(s, Ward, U2, U2);
        s = Insert(s, Slow, U2, U3);
        s = Harm.Deal(StartRules, s, U3, Lethal).Next;
        Assert.True(Holds(s, MarkEnd, U2));

        var (events, next) = CombatEnd.EndIfWon(StartRules, s);

        Assert.Equal<GameEvent>(
            [
                new TurnEnded(U4),
                new ConditionEnded(2, SoftFight, U1, ConditionEndReason.CombatEnd),
                new ConditionEnded(1, MarkEnd, U2, ConditionEndReason.SourceGone),
                new ConditionEnded(3, Ward, U2, ConditionEndReason.CombatEnd),
                new CombatEnded(CombatEndReason.Win),
            ],
            events.ToArray());
        Assert.DoesNotContain(next.Effects, e => StartRules.Condition(e.EffectId).Flags.CombatOnly);
        Assert.True(Holds(next, Slow, U2));
    }

    [Fact]
    public void A_Steeled_from_the_last_turn_end_step_does_not_survive_the_fight()
    {
        // U4's own hard instance ends at its turn-end step, which combat end completes; the Steeled it grants is
        // combatOnly and ends in step 2.
        var s = Insert(Fight(), HardFight, U4, U3);
        Assert.True(State(s, HardFight, U4).OwnTurn);
        s = Harm.Deal(StartRules, s, U3, Lethal).Next;

        var (events, next) = CombatEnd.EndIfWon(StartRules, s);

        Assert.Equal<GameEvent>(
            [
                new TurnEnded(U4),
                new ConditionEnded(1, HardFight, U4, ConditionEndReason.Expired),
                new ConditionApplied(2, Conditions.SteeledId, U4, new EffectSource(U4), 1, true),
                new ConditionEnded(2, Conditions.SteeledId, U4, ConditionEndReason.CombatEnd),
                new CombatEnded(CombatEndReason.Win),
            ],
            events.ToArray());
        Assert.Empty(next.Effects);
    }
}
