using System.Collections.Immutable;
using System.Globalization;
using Dungeon.Core.Combat;
using Dungeon.Core.Conditions;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.ConditionFixtures;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The classes share their names with their namespaces, so name them explicitly inside this namespace.
using Conditions = Dungeon.Core.Conditions.Conditions;
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The in-combat clock (M1-12a Task 4; plan calls 1, 5 and 9; owner rulings 2 and 4; CO 5.1 to 5.7, 7.2, 20.2): the
/// counting test at each boundary, the own-turn rule, the turn-start step (ticks, the zero-HP stop, in-step trigger
/// dispatch, the lost-turn check) and the turn-end step (repeat saves, decay, countdowns, trigger caps and fallbacks,
/// windows), round-end window ends, Steeled's window and the lost-turn cap.
/// </summary>
public class ClockTests
{
    private static readonly UnitId P1 = new(1);
    private static readonly UnitId P2 = new(2);
    private static readonly UnitId E3 = new(3);
    private static readonly UnitId E4 = new(4);

    private const long Unbeatable = 1000;
    private const long Trivial = -1000;

    private static readonly DamageRoll Lethal =
        new([new PartRoll(new DamagePart(DamageType.Slashing, 0, 0, 1000), [], 1000)], false);

    private static readonly HookRegistry Hooks = HookRegistry.Core
        .With("test.insert", InsertOnce)
        .With("test.kill_holder", ctx => Deal(ctx, ctx.Instance.Holder))
        .With("test.mark", ctx => ctx.State);

    // One-shot trigger effects: each inserts a condition (args: id, holder, source) and removes itself.
    private static readonly string[] TriggerEffects =
    [
        Trigger("t.start.stun", "turnStarted", "holder", "test.insert", Stun, "holder", "1"),
        Trigger("t.start.daze", "turnStarted", "holder", "test.insert", Daze, "holder", "1"),
        Trigger("t.end.daze", "turnEnded", "holder", "test.insert", Daze, "holder", "1"),
        Trigger("t.start.steeled", "turnStarted", "holder", "test.insert", Conditions.SteeledId, "holder", "holder"),
        Trigger("t.end.steeled", "turnEnded", "holder", "test.insert", Conditions.SteeledId, "holder", "holder"),
        Trigger("t.start.mark_end", "turnStarted", "holder", "test.insert", MarkEnd, "3", "holder"),
        Trigger("t.end.mark_end", "turnEnded", "holder", "test.insert", MarkEnd, "3", "holder"),
        Trigger("t.start.mark_start", "turnStarted", "holder", "test.insert", MarkStart, "3", "holder"),
        Trigger("t.end.mark_start", "turnEnded", "holder", "test.insert", MarkStart, "3", "holder"),
        Trigger("t.trap", "unitMoved", "any", "test.insert", Stun, "mover", "holder"),
        Trigger("t.start.kill", "turnStarted", "holder", "test.kill_holder"),
        Trigger("t.start.mark", "turnStarted", "holder", "test.mark"),
        Trigger("t.watch_death", "unitDied", "any", "test.mark"),
    ];

    /// <summary>A token, intensity decay (value cap 2), <c>charges:1 {on: unitMoved, fallback: decay}</c>.</summary>
    private const string Fading = "test.clock.fading";

    private static readonly RulesContext ClockRules = RulesContext.Create(
        Load(
            [.. EffectRecords, .. TriggerEffects],
            [.. Records, $$$$"""{"id": "{{{{Fading}}}}", "polarity": "token", "intensity": {"kind": "decay", "valueCap": 2}, "duration": [{"charges": {"count": 1, "on": "unitMoved", "fallback": {"decay": true}}}]}"""]),
        hooks: Hooks);

    // --- The driver ---

    // The hero P1 and companion P2 (party, full HP), enemies E3 and E4 (12 HP), seed 7, the fight held before round 1 in
    // the given order. Units can be given HP.
    private static GameState Held(UnitId[] order, int p1Hp = FullHp, int p2Hp = FullHp) =>
        GameState.Create(
            GridMap.Parse(".......", ".......", "......."),
            [
                new Unit(P1, new GridPos(0, 1), Faction.Party, [], Fixtures.Stats, p1Hp),
                new Unit(P2, new GridPos(2, 1), Faction.Party, [], Fixtures.Stats, p2Hp),
                new Unit(E3, new GridPos(4, 1), Faction.Enemy, [], EnemyStats, 12),
                new Unit(E4, new GridPos(6, 1), Faction.Enemy, [], EnemyStats, 12),
            ],
            7,
            hero: P1) with
        {
            Combat = new CombatState([.. order], 0, 1, BeforeRound1: true),
        };

    // A fight being played: round 1 starts on construction; End ends the current turn through the engine (whose settle
    // loop starts the next); every command's events are kept.
    private sealed class Play
    {
        public Play(GameState held)
        {
            var (events, s) = Turns.BeginRound1(ClockRules, held);
            S = s;
            Steps.Add(events);
        }

        public GameState S { get; private set; }

        public List<ImmutableArray<GameEvent>> Steps { get; } = [];

        public ImmutableArray<GameEvent> Last => Steps[^1];

        public IEnumerable<GameEvent> Log => Steps.SelectMany(e => e);

        public Play End()
        {
            var r = Engine.Apply(S, new EndTurnCommand(S.Combat!.CurrentUnit), ClockRules);
            Assert.True(r.Ok, r.Error.ToString());
            S = r.State;
            Steps.Add(r.Events);
            return this;
        }

        public Play Insert(string id, UnitId holder, UnitId? source, ConditionInsert? options = null)
        {
            var (events, s) = Conditions.Insert(ClockRules, S, id, holder, new EffectSource(source), options ?? ConditionInsert.None);
            S = s;
            Steps.Add(events);
            return this;
        }

        public Play Do(Func<GameState, GameState> change)
        {
            S = change(S);
            return this;
        }

        public UnitId Current => S.Combat!.CurrentUnit;

        public long Seq(string id, UnitId holder) => Assert.Single(S.Effects, e => e.EffectId == id && e.Holder == holder).Seq;

        public ConditionState State(string id, UnitId holder) =>
            Assert.Single(S.Effects, e => e.EffectId == id && e.Holder == holder).Condition!;

        public bool Holds(string id, UnitId holder) => S.Effects.Any(e => e.EffectId == id && e.Holder == holder);
    }

    private static GameState Insert(GameState s, string id, UnitId holder, UnitId? source, ConditionInsert? options = null) =>
        Conditions.Insert(ClockRules, s, id, holder, new EffectSource(source), options ?? ConditionInsert.None).Next;

    private static GameState Give(GameState s, UnitId unit, string effect) => Effects.Apply(s, effect, unit, new EffectSource(unit));

    private static ConditionInsert Difficulty(long d, params DurationTerm[] terms) =>
        new(RecordedDifficulty: d, Duration: terms.Length == 0 ? null : [.. terms]);

    // How many of the holder's turn ends the log holds up to and including the one whose step ended the instance (0: it
    // never ended).
    private static int EndsOnTurnEnd(Play play, UnitId holder, long seq)
    {
        int ends = 0;
        foreach (var e in play.Log)
        {
            if (e is TurnEnded t && t.Unit == holder)
            {
                ends++;
            }

            if (e is ConditionEnded c && c.Seq == seq)
            {
                return ends;
            }
        }

        return 0;
    }

    private static int Draws(GameState s) => (int)s.Rng.Combat.Counter;

    [Fact]
    public void A_charges_term_with_a_decay_fallback_decays_and_ends_at_0()
    {
        // Final review C5: no move is made, so the charge is never used; the decay fallback runs the instance down.
        var play = new Play(Held([P1, E3]));
        play.Insert(Fading, E3, P1, new ConditionInsert(Value: 2));
        long seq = play.Seq(Fading, E3);
        play.End().End();
        Assert.Equal(1, play.State(Fading, E3).Value);
        play.End().End();
        Assert.False(play.Holds(Fading, E3));
        Assert.Contains(new ConditionEnded(seq, Fading, E3, ConditionEndReason.Expired), play.Log);
        Assert.Equal(2, EndsOnTurnEnd(play, E3, seq));
    }

    // --- Phase mechanics (plan call 1) ---

    [Fact]
    public void Each_turn_advances_the_phase_at_its_turn_start_its_Act_phase_and_its_turn_end()
    {
        var play = new Play(Held([P1, E3]));
        Assert.Equal((2L, ClockStep.Act), (play.S.Phase, play.S.Step));
        var (ended, vacated) = Turns.EndTurn(ClockRules, play.S);
        Assert.Equal([new TurnEnded(P1)], ended.ToArray());
        Assert.Equal((3L, ClockStep.TurnEnd), (vacated.Phase, vacated.Step));
        play.End();
        Assert.Equal((5L, ClockStep.Act), (play.S.Phase, play.S.Step));
        Assert.Equal([new TurnEnded(P1), new TurnStarted(E3, 1)], play.Last.ToArray());
    }

    [Fact]
    public void A_won_fight_leaves_no_turn_step_so_nothing_after_it_is_own_turn()
    {
        // The last enemy dies in its own Act phase: its turn is vacated and the fight is won with no turn-end step.
        var play = new Play(Held([P1, E3])).End();
        Assert.Equal(ClockStep.Act, play.S.Step);
        var (_, dead) = Harm.Deal(ClockRules, Harm.Deal(ClockRules, play.S, E4, Lethal).Next, E3, Lethal);
        var (_, won) = CombatEnd.EndIfWon(ClockRules, dead);

        Assert.Null(won.Combat);
        Assert.Equal(ClockStep.None, won.Step);
        Assert.False(Assert.Single(Insert(won, Stun, P1, null).Effects).Condition!.OwnTurn);
    }

    // --- Review Focus 1: the counting test at each boundary ---

    // Where a turns:1 (Daze, a major) lands on E3: in its turn-start step (a turnStarted trigger), its Act phase, its
    // turn-end step (a turnEnded trigger), or another unit's turn (P1's Act).
    private static Play Placed(string condition, string point)
    {
        var held = Held([P1, E3]);
        string prefix = condition == Daze ? "daze" : "steeled";
        UnitId? source = condition == Daze ? P1 : E3;
        held = point switch
        {
            "turnStart" => Give(held, E3, $"t.start.{prefix}"),
            "turnEnd" => Give(held, E3, $"t.end.{prefix}"),
            _ => held,
        };

        var play = new Play(held);
        if (point == "other")
        {
            play.Insert(condition, E3, source);
        }

        play.End();
        if (point == "act")
        {
            Assert.Equal(E3, play.Current);
            play.Insert(condition, E3, source);
        }

        for (int i = 0; i < 6; i++)
        {
            play.End();
        }

        return play;
    }

    [Theory]
    [InlineData("turnStart", 1)]
    [InlineData("act", 2)]
    [InlineData("turnEnd", 2)]
    [InlineData("other", 1)]
    public void Turns_one_counts_from_the_right_turn(string point, int endsOn)
    {
        var play = Placed(Daze, point);
        var applied = Assert.Single(play.Log.OfType<ConditionApplied>(), a => a.ConditionId == Daze);
        Assert.Equal(endsOn, EndsOnTurnEnd(play, E3, applied.Seq));
        Assert.Contains(new ConditionEnded(applied.Seq, Daze, E3, ConditionEndReason.Expired), play.Log);
    }

    // --- Review Focus 2: the own-turn rule ---

    [Fact]
    public void An_own_turn_hard_condition_ends_that_turn_and_Steeled_follows()
    {
        // Held is hard save-ends (cap 2): it would run on, but lands in E3's own Act phase.
        var play = new Play(Held([P1, E3])).End();
        play.Insert(ConditionFixtures.Held, E3, P1, Difficulty(Unbeatable));
        long held = play.Seq(ConditionFixtures.Held, E3);
        Assert.True(play.State(ConditionFixtures.Held, E3).OwnTurn);
        int draws = Draws(play.S);
        long steeled = play.S.NextEffectSeq;

        play.End();

        Assert.Equal(
            [
                new TurnEnded(E3),
                new ConditionEnded(held, ConditionFixtures.Held, E3, ConditionEndReason.Expired),
                new ConditionApplied(steeled, Conditions.SteeledId, E3, new EffectSource(E3), 1, true),
                new TurnStarted(P1, 2),
            ],
            play.Last.ToArray());
        Assert.Equal(draws, Draws(play.S));

        // It costs no turn start.
        play.End();
        Assert.Equal([new TurnEnded(P1), new TurnStarted(E3, 2)], play.Last.ToArray());
        Assert.Equal(0, play.S.FindUnit(E3)!.HardTurnsLostThisFight);
    }

    [Fact]
    public void An_off_turn_hard_condition_costs_the_next_turn_start()
    {
        var play = new Play(Held([P1, E3]));
        play.Insert(Stun, E3, P1);
        Assert.False(play.State(Stun, E3).OwnTurn);
        long stun = play.Seq(Stun, E3);

        play.End();
        Assert.Equal([new TurnEnded(P1), new TurnStarted(E3, 1), new TurnStartLost(E3, 1)], play.Last.ToArray());
        Assert.Equal(1, play.S.FindUnit(E3)!.HardTurnsLostThisFight);

        long steeled = play.S.NextEffectSeq;
        play.End();
        Assert.Equal(
            [
                new TurnEnded(E3),
                new ConditionEnded(stun, Stun, E3, ConditionEndReason.Expired),
                new ConditionApplied(steeled, Conditions.SteeledId, E3, new EffectSource(E3), 1, true),
                new TurnStarted(P1, 2),
            ],
            play.Last.ToArray());
    }

    [Fact]
    public void A_reaction_during_the_wearers_Act_phase_is_own_turn()
    {
        // E3's trap answers P1's move in P1's own Act phase with a hard Stun on P1.
        var play = new Play(Give(Held([P1, E3]), E3, "t.trap"));
        var r = Engine.Apply(play.S, new MoveCommand(P1, Direction.E), ClockRules);
        Assert.True(r.Ok, r.Error.ToString());
        play.Do(_ => r.State);
        Assert.True(play.State(Stun, P1).OwnTurn);
        long stun = play.Seq(Stun, P1);

        play.End();
        Assert.Contains(new ConditionEnded(stun, Stun, P1, ConditionEndReason.Expired), play.Last);
        Assert.Contains(play.Last, e => e is ConditionApplied { ConditionId: Conditions.SteeledId } a && a.Holder == P1);

        play.End();
        Assert.Equal([new TurnEnded(E3), new TurnStarted(P1, 2)], play.Last.ToArray());
        Assert.Equal(0, play.S.FindUnit(P1)!.HardTurnsLostThisFight);
    }

    [Fact]
    public void An_insert_in_the_boss_action_gap_costs_the_heros_next_turn()
    {
        var play = new Play(Held([P1, E3]));
        var (_, gap) = Turns.EndTurn(ClockRules, play.S);
        Assert.True(gap.Combat!.TurnVacated);

        gap = Insert(gap, Stun, P1, E3);
        Assert.False(Assert.Single(gap.Effects, e => e.EffectId == Stun).Condition!.OwnTurn);

        var (resumed, next) = Turns.ResumeIfVacated(ClockRules, gap);
        Assert.Equal([new TurnStarted(E3, 1)], resumed.ToArray());
        play.Do(_ => next).End();
        Assert.Equal([new TurnEnded(E3), new TurnStarted(P1, 2), new TurnStartLost(P1, 1)], play.Last.ToArray());
    }

    // --- Source death ---

    [Fact]
    public void A_turns_condition_runs_on_after_its_source_dies()
    {
        var held = Insert(Held([P1, E3, E4]), Slow, P1, E4);
        (_, held) = Harm.Deal(ClockRules, held, E4, Lethal);
        var play = new Play(held);
        long slow = play.Seq(Slow, P1);
        for (int i = 0; i < 6; i++)
        {
            play.End();
        }

        Assert.Equal(3, EndsOnTurnEnd(play, P1, slow));
        Assert.Contains(new ConditionEnded(slow, Slow, P1, ConditionEndReason.Expired), play.Log);
    }

    [Fact]
    public void A_source_tied_condition_ends_at_once_when_its_source_dies()
    {
        var play = new Play(Held([P1, E3, E4]));
        play.Insert(Tied, P1, E4);
        long tied = play.Seq(Tied, P1);

        var (result, _) = Harm.Deal(ClockRules, play.S, E4, Lethal);

        Assert.Contains(new ConditionEnded(tied, Tied, P1, ConditionEndReason.SourceGone), result.Events);
    }

    [Fact]
    public void A_window_whose_source_died_ends_at_round_end()
    {
        var play = new Play(Held([P1, E3, E4]));
        play.Insert(MarkEnd, P1, E4);
        long mark = play.Seq(MarkEnd, P1);
        play.Do(s => Harm.Deal(ClockRules, s, E4, Lethal).Next);

        play.End();
        Assert.True(play.Holds(MarkEnd, P1));

        play.End();
        Assert.Equal(
            [
                new TurnEnded(E3),
                new ConditionEnded(mark, MarkEnd, P1, ConditionEndReason.SourceGone),
                new TurnStarted(P1, 2),
            ],
            play.Last.ToArray());
    }

    // --- CO 20.2: turn-start and turn-end applications ---

    [Fact]
    public void A_turn_start_trigger_application_counts_that_turn()
    {
        var play = new Play(Give(Held([P1, E3]), E3, "t.start.stun"));
        long trigger = play.Seq("t.start.stun", E3);
        long stun = play.S.NextEffectSeq;

        play.End();

        Assert.Equal(
            [
                new TurnEnded(P1),
                new TurnStarted(E3, 1),
                new HookFired("test.insert", trigger),
                new ConditionApplied(stun, Stun, E3, new EffectSource(P1), 1, true),
                new TurnStartLost(E3, 1),
            ],
            play.Last.ToArray());

        play.End();
        Assert.Contains(new ConditionEnded(stun, Stun, E3, ConditionEndReason.Expired), play.Last);
    }

    [Fact]
    public void A_turn_end_insert_does_not_count_that_turn()
    {
        var play = new Play(Give(Held([P1, E3]), E3, "t.end.daze")).End();
        play.End();
        long daze = play.Seq(Daze, E3);
        Assert.DoesNotContain(play.Last, e => e is ConditionEnded);
        // Applied at E3's turn-end phase; P1's turn start and Act phase have passed since.
        Assert.Equal(play.S.Phase - 2, play.State(Daze, E3).AppliedPhase);

        play.End();
        play.End();
        Assert.Contains(new ConditionEnded(daze, Daze, E3, ConditionEndReason.Expired), play.Last);
    }

    // --- Windows (CO 5.6) ---

    [Theory]
    [InlineData("turnStart", false)]
    [InlineData("act", false)]
    [InlineData("turnEnd", false)]
    [InlineData("turnStart", true)]
    [InlineData("act", true)]
    [InlineData("turnEnd", true)]
    public void A_window_opened_in_the_sources_turn_runs_to_its_following_turn(string point, bool atStart)
    {
        string mark = atStart ? MarkStart : MarkEnd;
        string suffix = atStart ? "mark_start" : "mark_end";
        var held = Held([P1, E3]);
        held = point switch
        {
            "turnStart" => Give(held, P1, $"t.start.{suffix}"),
            "turnEnd" => Give(held, P1, $"t.end.{suffix}"),
            _ => held,
        };

        var play = new Play(held);
        if (point == "act")
        {
            play.Insert(mark, E3, P1);
        }

        // P1's turn ends: the window opened in this turn stays.
        play.End();
        long seq = play.Seq(mark, E3);
        Assert.True(play.Holds(mark, E3));

        play.End();
        if (atStart)
        {
            // Item 0 of P1's next turn.
            Assert.Equal(
                [new TurnEnded(E3), new TurnStarted(P1, 2), new ConditionEnded(seq, mark, E3, ConditionEndReason.Expired)],
                play.Last.ToArray());
            return;
        }

        Assert.True(play.Holds(mark, E3));
        play.End();
        Assert.Equal(
            [new TurnEnded(P1), new ConditionEnded(seq, mark, E3, ConditionEndReason.Expired), new TurnStarted(E3, 2)],
            play.Last.ToArray());
    }

    [Fact]
    public void A_window_opened_on_another_units_turn_ends_at_the_sources_next_turn_end()
    {
        var play = new Play(Held([P1, E3])).End();
        play.Insert(MarkEnd, E3, P1);
        long seq = play.Seq(MarkEnd, E3);

        play.End();
        Assert.True(play.Holds(MarkEnd, E3));

        play.End();
        Assert.Equal(
            [new TurnEnded(P1), new ConditionEnded(seq, MarkEnd, E3, ConditionEndReason.Expired), new TurnStarted(E3, 2)],
            play.Last.ToArray());
    }

    // --- saveEnds ---

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void A_repeat_save_rolls_one_d20_on_combat_against_the_recorded_difficulty(bool saves)
    {
        // Held: hard, saveEnds {will, cap 2, graceTurns 1}, placed off-turn on E3.
        var play = new Play(Held([P1, E3]));
        play.Insert(ConditionFixtures.Held, E3, P1, Difficulty(saves ? Trivial : Unbeatable));
        long held = play.Seq(ConditionFixtures.Held, E3);
        play.End();
        int draws = Draws(play.S);

        play.End();

        Assert.Equal(draws + 1, Draws(play.S));
        var rolled = Assert.Single(play.Last.OfType<SaveRolled>());
        Assert.Equal((E3, saves ? Trivial : Unbeatable, saves), (rolled.Target, rolled.Result.Target, rolled.Result.Success));
        if (saves)
        {
            Assert.Equal(
                [
                    new TurnEnded(E3),
                    rolled,
                    new ConditionEnded(held, ConditionFixtures.Held, E3, ConditionEndReason.Saved),
                    new ConditionApplied(held + 1, Conditions.SteeledId, E3, new EffectSource(E3), 1, true),
                    new TurnStarted(P1, 2),
                ],
                play.Last.ToArray());
            return;
        }

        Assert.Equal([new TurnEnded(E3), rolled, new TurnStarted(P1, 2)], play.Last.ToArray());
        Assert.Equal(1, play.State(ConditionFixtures.Held, E3).TurnsCounted);

        // The cap (2) ends it at the second counted turn end, after the roll.
        play.End().End();
        Assert.Single(play.Last.OfType<SaveRolled>());
        Assert.Contains(new ConditionEnded(held, ConditionFixtures.Held, E3, ConditionEndReason.Expired), play.Last);
    }

    [Fact]
    public void A_saveEnds_inserted_in_the_wearers_Act_phase_rolls_nothing_that_turn_end()
    {
        // Combined (a combatOnly major, so no own-turn rule) as saveEnds {will, cap 3}.
        var play = new Play(Held([P1, E3])).End();
        play.Insert(Combined, E3, P1, Difficulty(Unbeatable, new DurationTerm.SaveEnds(Save.Will, 3, 0)));
        int draws = Draws(play.S);

        play.End();
        Assert.Equal(draws, Draws(play.S));
        Assert.DoesNotContain(play.Last, e => e is SaveRolled);

        play.End().End();
        Assert.Equal(draws + 1, Draws(play.S));
        Assert.Single(play.Last.OfType<SaveRolled>());
    }

    [Fact]
    public void GraceTurns_delays_the_first_save_and_the_cap_ends_it()
    {
        var play = new Play(Held([P1, E3]));
        play.Insert(Combined, E3, P1, Difficulty(Unbeatable, new DurationTerm.SaveEnds(Save.Will, 3, 2)));
        long daze = play.Seq(Combined, E3);

        play.End().End();
        Assert.DoesNotContain(play.Last, e => e is SaveRolled);

        play.End().End();
        Assert.Single(play.Last.OfType<SaveRolled>());
        Assert.True(play.Holds(Combined, E3));

        play.End().End();
        Assert.Single(play.Last.OfType<SaveRolled>());
        Assert.Contains(new ConditionEnded(daze, Combined, E3, ConditionEndReason.Expired), play.Last);
    }

    // --- decay, charges, trigger, sustained, staged, combined ---

    [Fact]
    public void Decay_drops_by_one_per_counted_turn_and_ends_at_zero()
    {
        var play = new Play(Held([P1, E3]));
        play.Insert(Venom, E3, P1, new ConditionInsert(Value: 3));
        long venom = play.Seq(Venom, E3);

        play.End();
        Assert.Contains(play.Last, e => e is DamageDealt { Total: 3, HpAfter: 9 } d && d.Target == E3);
        play.End();
        Assert.Equal(2, play.State(Venom, E3).Value);

        play.End();
        Assert.Contains(play.Last, e => e is DamageDealt { Total: 2, HpAfter: 7 });
        play.End();
        Assert.Equal(1, play.State(Venom, E3).Value);

        play.End();
        Assert.Contains(play.Last, e => e is DamageDealt { Total: 1, HpAfter: 6 });
        play.End();
        Assert.Contains(new ConditionEnded(venom, Venom, E3, ConditionEndReason.Expired), play.Last);
        Assert.False(play.Holds(Venom, E3));
    }

    [Theory]
    [InlineData(Token)] // charges:1, fallback turns:3
    [InlineData(Trig)] // trigger {unitMoved, cap 3}
    public void A_fallback_or_a_trigger_cap_ends_it_at_the_third_counted_turn(string id)
    {
        var play = new Play(Held([P1, E3]));
        play.Insert(id, E3, P1);
        long seq = play.Seq(id, E3);
        for (int i = 0; i < 8; i++)
        {
            play.End();
        }

        Assert.Equal(3, EndsOnTurnEnd(play, E3, seq));
        Assert.Contains(new ConditionEnded(seq, id, E3, ConditionEndReason.Expired), play.Log);
    }

    [Fact]
    public void Removing_the_sustainer_ends_what_it_sustains_and_Steeled_follows_a_hard_one()
    {
        // Daze (turns:1) on P1 sustains Sustained on E3 and a hard Stun on E4; Daze ends at P1's first turn end.
        var held = Insert(Held([P1, E3, E4]), Daze, P1, E3);
        long daze = held.Effects[^1].Seq;
        held = Insert(held, Sustained, E3, P1, new ConditionInsert(SustainedBy: daze));
        held = Insert(held, Stun, E4, P1, new ConditionInsert(SustainedBy: daze));
        long sustained = daze + 1;
        long stun = daze + 2;
        long steeled = daze + 3;

        var play = new Play(held).End();

        Assert.Equal(
            [
                new TurnEnded(P1),
                new ConditionEnded(daze, Daze, P1, ConditionEndReason.Expired),
                new ConditionEnded(sustained, Sustained, E3, ConditionEndReason.Ended),
                new ConditionEnded(stun, Stun, E4, ConditionEndReason.Ended),
                new ConditionApplied(steeled, Conditions.SteeledId, E4, new EffectSource(E4), 1, true),
                new TurnStarted(E3, 1),
            ],
            play.Last.ToArray());
    }

    [Fact]
    public void Sustained_ends_at_its_max()
    {
        var play = new Play(Held([P1, E3]));
        play.Insert(Sustained, E3, P1);
        long seq = play.Seq(Sustained, E3);
        for (int i = 0; i < 6; i++)
        {
            play.End();
        }

        Assert.Equal(2, EndsOnTurnEnd(play, E3, seq));
        Assert.Contains(new ConditionEnded(seq, Sustained, E3, ConditionEndReason.Expired), play.Log);
    }

    [Fact]
    public void Staged_moves_a_stage_on_each_failed_save_and_ends_past_its_stages()
    {
        var play = new Play(Held([P1, E3]));
        play.Insert(Calcify, E3, P1, Difficulty(Unbeatable));
        long seq = play.Seq(Calcify, E3);

        play.End().End();
        Assert.Equal(2, play.State(Calcify, E3).Value);
        play.End().End();
        Assert.Equal(3, play.State(Calcify, E3).Value);
        play.End().End();
        Assert.Single(play.Last.OfType<SaveRolled>());
        Assert.Contains(new ConditionEnded(seq, Calcify, E3, ConditionEndReason.Expired), play.Last);
    }

    [Fact]
    public void Staged_ends_with_Saved_on_a_success_and_Expired_at_its_cap()
    {
        var saved = new Play(Held([P1, E3]));
        saved.Insert(Calcify, E3, P1, Difficulty(Trivial));
        long seq = saved.Seq(Calcify, E3);
        saved.End().End();
        Assert.Contains(new ConditionEnded(seq, Calcify, E3, ConditionEndReason.Saved), saved.Last);

        var capped = new Play(Held([P1, E3]));
        capped.Insert(Calcify, E3, P1, Difficulty(Unbeatable, new DurationTerm.Staged(3, Save.Fortitude, 2, Stun)));
        seq = capped.Seq(Calcify, E3);
        capped.End().End();
        Assert.Equal(2, capped.State(Calcify, E3).Value);
        capped.End().End();
        Assert.Contains(new ConditionEnded(seq, Calcify, E3, ConditionEndReason.Expired), capped.Last);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Combined_terms_end_on_the_first_terminator(bool saves)
    {
        // turns:1 with saveEnds {will, cap 3}: the roll comes first (item 1), then the countdown (item 3).
        var play = new Play(Held([P1, E3]));
        play.Insert(Combined, E3, P1, Difficulty(saves ? Trivial : Unbeatable, new DurationTerm.Turns(1), new DurationTerm.SaveEnds(Save.Will, 3, 0)));
        long seq = play.Seq(Combined, E3);

        play.End().End();

        var rolled = Assert.Single(play.Last.OfType<SaveRolled>());
        var reason = saves ? ConditionEndReason.Saved : ConditionEndReason.Expired;
        Assert.Equal(
            [new TurnEnded(E3), rolled, new ConditionEnded(seq, Combined, E3, reason), new TurnStarted(P1, 2)],
            play.Last.ToArray());
    }

    // --- Steeled (plan call 5; CO 7.2) ---

    [Theory]
    [InlineData("turnStart", 1)]
    [InlineData("act", 1)]
    [InlineData("turnEnd", 2)]
    [InlineData("other", 1)]
    public void Steeled_lasts_to_the_first_turn_end_step_after_it_is_gained(string point, int endsOn)
    {
        var play = Placed(Conditions.SteeledId, point);
        var applied = Assert.Single(play.Log.OfType<ConditionApplied>(), a => a.ConditionId == Conditions.SteeledId);
        Assert.Equal(endsOn, EndsOnTurnEnd(play, E3, applied.Seq));
    }

    // --- The lost-turn cap (owner ruling 2; CO 5.7) ---

    [Fact]
    public void The_second_lost_turn_start_grants_Unyielding_and_the_held_hard_instance_ends_that_turn()
    {
        // 1. Y (hard, turns:1) before E3's turn 1: turn 1 is lost; Y expires and Steeled follows.
        var play = new Play(Insert(Held([P1, E3]), Stun, E3, P1));
        long y = play.Seq(Stun, E3);
        play.End();
        Assert.Equal([new TurnEnded(P1), new TurnStarted(E3, 1), new TurnStartLost(E3, 1)], play.Last.ToArray());
        long steeled = play.S.NextEffectSeq;
        play.End();
        Assert.Equal(
            [
                new TurnEnded(E3),
                new ConditionEnded(y, Stun, E3, ConditionEndReason.Expired),
                new ConditionApplied(steeled, Conditions.SteeledId, E3, new EffectSource(E3), 1, true),
                new TurnStarted(P1, 2),
            ],
            play.Last.ToArray());

        // 2. X (hard, saveEnds cap 2, unbeatable) after turn 1.
        play.Insert(ConditionFixtures.Held, E3, P1, Difficulty(Unbeatable));
        long x = play.Seq(ConditionFixtures.Held, E3);
        long unyielding = play.S.NextEffectSeq;

        // 3. Turn 2's start: the second lost turn start, then Unyielding within the step.
        play.End();
        Assert.Equal(
            [
                new TurnEnded(P1),
                new TurnStarted(E3, 2),
                new TurnStartLost(E3, 2),
                new ConditionApplied(unyielding, Conditions.UnyieldingId, E3, new EffectSource(E3), 1, true),
            ],
            play.Last.ToArray());

        // 4. Turn 2's end: X ends although its cap would run on, and Steeled follows (the first Steeled ends too).
        play.End();
        var rolled = Assert.Single(play.Last.OfType<SaveRolled>());
        Assert.Equal(
            [
                new TurnEnded(E3),
                rolled,
                new ConditionEnded(steeled, Conditions.SteeledId, E3, ConditionEndReason.Expired),
                new ConditionEnded(x, ConditionFixtures.Held, E3, ConditionEndReason.Expired),
                new ConditionApplied(unyielding + 1, Conditions.SteeledId, E3, new EffectSource(E3), 1, true),
                new TurnStarted(P1, 3),
            ],
            play.Last.ToArray());
        Assert.False(play.Holds(ConditionFixtures.Held, E3));

        // Turn 3 starts with no hard instance held.
        play.Insert(ConditionFixtures.Held, E3, P1, Difficulty(Unbeatable));
        long z = play.Seq(ConditionFixtures.Held, E3);
        play.End();
        Assert.Equal([new TurnEnded(P1), new TurnStarted(E3, 3), new TurnStartLost(E3, 3)], play.Last.ToArray());

        // A hard instance inserted after Unyielding landed is not ended early.
        play.End();
        Assert.DoesNotContain(new ConditionEnded(z, ConditionFixtures.Held, E3, ConditionEndReason.Expired), play.Last);
        Assert.True(play.Holds(ConditionFixtures.Held, E3));
        Assert.True(play.Holds(Conditions.UnyieldingId, E3));
    }

    [Theory]
    [InlineData(Surprised, 1, false)] // engineState
    [InlineData(Stun, 3, false)] // self
    [InlineData(Stun, 4, false)] // an ally
    [InlineData(Stun, 1, true)] // a hostile source
    [InlineData(Stun, 0, true)] // the environment
    public void Only_a_hostile_or_environment_hard_instance_costs_a_turn_start(string id, int source, bool lost)
    {
        var play = new Play(Insert(Held([P1, E3, E4]), id, E3, source == 0 ? null : new UnitId(source))).End();

        Assert.Equal(lost, play.Last.Contains(new TurnStartLost(E3, 1)));
        Assert.Equal(lost ? 1 : 0, play.S.FindUnit(E3)!.HardTurnsLostThisFight);
    }

    // --- Review Focus 3: a death mid-step ---

    [Fact]
    public void A_unit_killed_by_its_own_tick_skips_the_rest_of_its_step()
    {
        // P2 (4 HP) holds Burn 5 (lethal) and Venom 3, and a hostile Stun; the hero watches for deaths.
        var held = Give(Held([P1, P2, E3], p2Hp: 4), P1, "t.watch_death");
        long watch = held.Effects[^1].Seq;
        held = Insert(held, Burn, P2, E3, new ConditionInsert(Value: 5));
        held = Insert(held, Venom, P2, E3, new ConditionInsert(Value: 3));
        held = Insert(held, Stun, P2, E3);
        var at = held.FindUnit(P2)!.Pos;

        var play = new Play(held).End();

        Assert.Equal(
            [
                new TurnEnded(P1),
                new TurnStarted(P2, 1),
                new DamageDealt(held.NextEventSeq, P2, 5, 0),
                new UnitDied(P2, at),
                new HookFired("test.mark", watch),
                new TurnStarted(E3, 1),
            ],
            play.Last.ToArray());
    }

    [Fact]
    public void The_hero_killed_by_a_tick_ends_the_run()
    {
        var held = Insert(Held([E3, P1], p1Hp: 4), Burn, P1, E3, new ConditionInsert(Value: 5));
        held = Insert(held, Venom, P1, E3, new ConditionInsert(Value: 3));

        var play = new Play(held).End();

        Assert.Equal(
            [
                new TurnEnded(E3),
                new TurnStarted(P1, 1),
                new DamageDealt(held.NextEventSeq, P1, 5, 0),
                new RunEnded(RunEndCause.HeroDeath, held.NextEventSeq),
            ],
            play.Last.ToArray());
        Assert.NotNull(play.S.Ended);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void A_turnStarted_trigger_that_kills_the_holder_skips_the_lost_turn_check(bool hero)
    {
        // The victim holds a hostile Stun and has already lost one turn start: the check would grant Unyielding.
        var victim = hero ? P1 : P2;
        var held = Held(hero ? [E3, P1] : [P1, P2, E3]);
        held = Insert(held, Stun, victim, E3);
        held = Give(held, victim, "t.start.kill");
        var unit = held.FindUnit(victim)!;
        held = held with { Units = held.Units.SetItem(held.Units.IndexOf(unit), unit with { HardTurnsLostThisFight = 1 }) };

        var play = new Play(held).End();

        Assert.DoesNotContain(play.Last, e => e is TurnStartLost or ConditionApplied);
        Assert.Contains(play.Last, e => e is HookFired { HookId: "test.kill_holder" });
        if (hero)
        {
            Assert.IsType<RunEnded>(play.Last[^1]);
        }
        else
        {
            Assert.Contains(play.Last, e => e is UnitDied d && d.Unit == P2);
            Assert.Equal(new TurnStarted(E3, 1), play.Last[^1]);
        }
    }

    // --- Ends and dispatch order ---

    [Fact]
    public void Every_end_emits_one_ConditionEnded_in_seq_order_within_an_item()
    {
        // Item 3: Daze and Slow (turns:1) on E3. The window step: P1's marks on E4 then E3.
        var held = Insert(Held([P1, E3, E4]), MarkEnd, E4, P1);
        held = Insert(held, MarkEnd, E3, P1);
        held = Insert(held, Daze, E3, P1);
        held = Insert(held, Slow, E3, P1, new ConditionInsert(Duration: [new DurationTerm.Turns(1)]));

        var play = new Play(held).End();
        Assert.Equal(
            [
                new TurnEnded(P1),
                new ConditionEnded(1, MarkEnd, E4, ConditionEndReason.Expired),
                new ConditionEnded(2, MarkEnd, E3, ConditionEndReason.Expired),
                new TurnStarted(E3, 1),
            ],
            play.Last.ToArray());

        play.End();
        Assert.Equal(
            [
                new TurnEnded(E3),
                new ConditionEnded(3, Daze, E3, ConditionEndReason.Expired),
                new ConditionEnded(4, Slow, E3, ConditionEndReason.Expired),
                new TurnStarted(E4, 1),
            ],
            play.Last.ToArray());
    }

    [Fact]
    public void TurnStarted_triggers_fire_inside_the_step_before_the_lost_turn_check()
    {
        var held = Give(Insert(Held([P1, E3]), Stun, E3, P1), E3, "t.start.mark");
        long mark = held.Effects[^1].Seq;

        var play = new Play(held).End();

        Assert.Equal(
            [new TurnEnded(P1), new TurnStarted(E3, 1), new HookFired("test.mark", mark), new TurnStartLost(E3, 1)],
            play.Last.ToArray());
    }

    [Fact]
    public void A_turnStarted_trigger_fires_once_when_the_engine_starts_the_turn()
    {
        var play = new Play(Give(Held([P1, E3]), E3, "t.start.mark")).End();
        Assert.Single(play.Last.OfType<HookFired>());

        // The direct entry points dispatch too, each on a fresh counter.
        var (events, _) = Turns.StartCombat(ClockRules, Give(Give(Held([E3, P1]), E3, "t.start.mark"), P1, "t.start.mark") with { Combat = null });
        Assert.Contains(events, e => e is HookFired { HookId: "test.mark" });
    }

    // --- Helpers ---

    // A trait with one trigger on `on` (scope `scope`) running `hook` with `args`.
    private static string Trigger(string id, string on, string scope, string hook, params string[] args) =>
        $$"""{"id": "{{id}}", "kind": "trait", "triggers": [{"on": "{{on}}", "scope": "{{scope}}", "do": [{"hook": "{{hook}}", "args": [{{string.Join(", ", args.Select(a => "\"" + a + "\""))}}]}]}]}""";

    // test.insert: args (condition, holder, source), each unit "holder", "mover" (a moved unit) or an id; then the
    // trigger's own instance goes, so it fires once.
    private static GameState InsertOnce(HookContext ctx)
    {
        UnitId Who(string spec) => spec switch
        {
            "holder" => ctx.Instance.Holder,
            "mover" => ((UnitMoved)ctx.Event).Unit,
            _ => new UnitId(int.Parse(spec, CultureInfo.InvariantCulture)),
        };

        var s = Effects.Remove(ctx.State, ctx.Instance.Seq);
        var (events, next) = Conditions.Insert(
            ctx.Rules, s, ctx.Args[0], Who(ctx.Args[1]), new EffectSource(Who(ctx.Args[2])), ConditionInsert.None);
        foreach (var e in events)
        {
            ctx.Emit(e);
        }

        return next;
    }

    private static GameState Deal(HookContext ctx, UnitId target)
    {
        var (harm, next) = Harm.Deal(ctx.Rules, ctx.State, target, Lethal);
        foreach (var e in harm.Events)
        {
            ctx.Emit(e);
        }

        return next;
    }
}
