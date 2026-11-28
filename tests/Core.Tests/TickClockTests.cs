using System.Collections.Immutable;
using System.Globalization;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Conditions;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.ConditionFixtures;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The classes share their names with their namespaces, so name them explicitly inside this namespace.
using Conditions = Dungeon.Core.Conditions.Conditions;
using Damage = Dungeon.Core.Damage.Damage;
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The out-of-combat tick (M1-12a Task 5; plan call 13; Review Focus 5; CO 5.2, 5.5, 5.6): every unit's turn-start step,
/// then the command at the step phase, then every unit's turn-end step and the gone-source window ends, then
/// <see cref="TickAdvanced"/> and a shrine's decision; the 1 HP tick floor for every unit; windows that last one tick; no
/// lost turn starts; and a refused command that runs no tick step.
/// </summary>
public class TickClockTests
{
    private static readonly UnitId P1 = new(1);
    private static readonly UnitId P2 = new(2);
    private static readonly UnitId E3 = new(3);

    private static readonly DamageRoll Lethal =
        new([new PartRoll(new DamagePart(DamageType.Slashing, 0, 0, 1000), [], 1000)], false);

    private static readonly HookRegistry Hooks = HookRegistry.Core
        .With("test.insert", InsertOnce)
        .With("test.mark", ctx => ctx.State);

    // One-shot trigger effects on unitMoved (the step phase): each inserts a condition (args: id, holder, source).
    private static readonly string[] TriggerEffects =
    [
        Trigger("t.move.daze", "unitMoved", "holder", "test.insert", Daze, "holder", "3"),
        Trigger("t.move.stun", "unitMoved", "holder", "test.insert", Stun, "holder", "3"),
        Trigger("t.move.mark_end", "unitMoved", "holder", "test.insert", MarkEnd, "3", "holder"),
        Trigger("t.move.mark_start", "unitMoved", "holder", "test.insert", MarkStart, "3", "holder"),
        Trigger("t.start.mark", "turnStarted", "any", "test.mark"),
    ];

    private static readonly RulesContext TickRules = RulesContext.Create(
        Load([.. EffectRecords, .. TriggerEffects], Records),
        hooks: Hooks,
        pools: AbilityFixtures.PoolTable,
        abilities: AbilityFixtures.Definitions);

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......");

    // Out of combat: the hero P1 and companion P2 (party), enemy E3 (12 HP unless given), each holding the fixture
    // abilities.
    private static GameState Calm(int p1Hp = FullHp, int e3Hp = 12) =>
        GameState.Create(
            Open,
            [
                AbilityFixtures.Party(1, 0, 1, p1Hp),
                AbilityFixtures.Party(2, 1, 2),
                AbilityFixtures.Foe(3, 5, 1, e3Hp),
            ],
            7,
            hero: P1);

    private static GameState Insert(GameState s, string id, UnitId holder, UnitId? source, ConditionInsert? options = null) =>
        Conditions.Insert(TickRules, s, id, holder, new EffectSource(source), options ?? ConditionInsert.None).Next;

    private static GameState Give(GameState s, UnitId unit, string effect) => Effects.Apply(s, effect, unit, new EffectSource(unit));

    private static StepResult Ok(GameState s, Command c)
    {
        var r = Engine.Apply(s, c, TickRules);
        Assert.True(r.Ok, r.Error.ToString());
        return r;
    }

    private static readonly MoveCommand East = new(P1, Direction.E);

    private static readonly MoveCommand West = new(P1, Direction.W);

    private static bool Holds(GameState s, string id, UnitId holder) => s.Effects.Any(e => e.EffectId == id && e.Holder == holder);

    private static ConditionState State(GameState s, string id, UnitId holder) =>
        Assert.Single(s.Effects, e => e.EffectId == id && e.Holder == holder).Condition!;

    private static long Seq(GameState s, string id, UnitId holder) =>
        Assert.Single(s.Effects, e => e.EffectId == id && e.Holder == holder).Seq;

    // --- The tick order (Review Focus 5; plan call 13) ---

    [Fact]
    public void A_tick_runs_every_turn_start_step_then_the_command_then_every_turn_end_step_then_TickAdvanced()
    {
        // Burn ticks at each turn start; Daze (turns:1, applied before the tick) ends at each turn end.
        var s = Insert(Insert(Calm(), Burn, P1, null, new ConditionInsert(Value: 2)), Burn, E3, null, new ConditionInsert(Value: 3));
        s = Insert(Insert(s, Daze, P1, null), Daze, E3, null);
        long dazeP1 = Seq(s, Daze, P1), dazeE3 = Seq(s, Daze, E3);

        var r = Ok(s, East);

        Assert.Equal(
            [typeof(DamageDealt), typeof(DamageDealt), typeof(UnitMoved), typeof(ConditionEnded), typeof(ConditionEnded), typeof(TickAdvanced)],
            r.Events.Select(e => e.GetType()).ToArray());
        Assert.Equal([P1, E3], r.Events.OfType<DamageDealt>().Select(d => d.Target).ToArray());
        Assert.Equal([2L, 3L], r.Events.OfType<DamageDealt>().Select(d => d.Total).ToArray());
        Assert.Equal(
            [new ConditionEnded(dazeP1, Daze, P1, ConditionEndReason.Expired), new ConditionEnded(dazeE3, Daze, E3, ConditionEndReason.Expired)],
            r.Events.OfType<ConditionEnded>().ToArray());
        Assert.Equal(new TickAdvanced(1), r.Events[^1]);
        Assert.Empty(r.Events.OfType<TurnStarted>());

        // Three phases a tick, shared by every unit; between ticks the clock is in no step.
        Assert.Equal((3L, ClockStep.None, 1L), (r.State.Phase, r.State.Step, r.State.Tick));
        Assert.Equal(1, State(r.State, Burn, P1).TurnsCounted);
    }

    [Fact]
    public void An_ability_use_out_of_combat_resolves_at_the_step_phase_between_the_steps()
    {
        var s = Insert(Insert(Calm(), Burn, P1, null), Daze, P2, null);
        var r = Ok(s, new UseAbilityCommand(P1, AbilityFixtures.Jab, new UnitsTarget([P2])));

        var types = r.Events.Select(e => e.GetType()).ToList();
        Assert.Equal(typeof(DamageDealt), types[0]);
        int used = types.IndexOf(typeof(AbilityUsed));
        int ended = types.IndexOf(typeof(ConditionEnded));
        Assert.True(used > 0 && ended > used, string.Join(", ", types.Select(t => t.Name)));
        Assert.Equal(typeof(TickAdvanced), types[^1]);
        Assert.Equal(3L, r.State.Phase);
    }

    [Fact]
    public void A_tick_fires_no_turnStarted_trigger()
    {
        var r = Ok(Give(Calm(), P2, "t.start.mark"), East);
        Assert.DoesNotContain(r.Events, e => e is HookFired);
    }

    // --- The 1 HP tick floor (Review Focus 5; plan call 13) ---

    [Fact]
    public void A_fixture_tick_floors_any_unit_monsters_included_at_1_HP_out_of_combat()
    {
        var s = Insert(Insert(Calm(p1Hp: 3, e3Hp: 3), Burn, P1, null, new ConditionInsert(Value: 5)), Burn, E3, null, new ConditionInsert(Value: 5));
        var r = Ok(s, East);

        Assert.Equal(1, r.State.FindUnit(P1)!.Hp);
        Assert.Equal(1, r.State.FindUnit(E3)!.Hp);
        var monster = r.Events.Where(e => e is DamageDealt d && d.Target == E3 || e is DamageClamped c && c.Target == E3).ToArray();
        Assert.Equal([typeof(DamageDealt), typeof(DamageClamped)], monster.Select(e => e.GetType()).ToArray());
        Assert.Equal(ClampReason.OutOfCombat, ((DamageClamped)monster[1]).Reason);
        Assert.Equal(1, ((DamageDealt)monster[0]).HpAfter);
        Assert.Contains(r.Events, e => e is DamageClamped { Reason: ClampReason.OutOfCombat } c && c.Target == P1);
        Assert.DoesNotContain(r.Events, e => e is UnitDied);
    }

    [Fact]
    public void The_tick_flag_floors_a_monster_only_out_of_combat_and_other_damage_never()
    {
        var calm = Calm(e3Hp: 3);
        var five = new DamageRoll([new PartRoll(new DamagePart(DamageType.Fire, 0, 0, 5), [], 5)], false);

        Assert.Equal(ClampReason.OutOfCombat, Damage.Resolve(TickRules, calm, E3, five, tick: true).Clamp);
        Assert.Null(Damage.Resolve(TickRules, calm, E3, five).Clamp);
        Assert.Equal(0, Damage.Resolve(TickRules, calm, E3, five).HpAfter);

        var fight = calm with { Combat = new CombatState([P1, P2, E3], 0, 1) };
        Assert.Null(Damage.Resolve(TickRules, fight, E3, five, tick: true).Clamp);

        var (harm, _) = Harm.Deal(TickRules, calm, E3, five, tick: true);
        Assert.Equal([typeof(DamageDealt), typeof(DamageClamped)], harm.Events.Select(e => e.GetType()).ToArray());
    }

    // --- No lost turn starts out of combat (Review Focus 5) ---

    [Fact]
    public void A_hostile_hard_instance_held_across_two_ticks_costs_no_turn_start()
    {
        // Stun from the hero on E3, made to last two counted turns (a trigger cap 2, enemy-facing); E3 has already lost
        // one turn start this fight, so a second would grant Unyielding.
        var s = Insert(Calm(), Stun, E3, P1, new ConditionInsert(Duration: [new DurationTerm.TriggerEnd("unitMoved", 2)]));
        s = s with { Units = s.Units.SetItem(2, s.Units[2] with { HardTurnsLostThisFight = 1 }) };

        var first = Ok(s, East);
        Assert.True(Holds(first.State, Stun, E3));
        var second = Ok(first.State, West);

        var log = first.Events.Concat(second.Events).ToList();
        Assert.DoesNotContain(log, e => e is TurnStartLost);
        Assert.DoesNotContain(log, e => e is ConditionApplied { ConditionId: Conditions.UnyieldingId });
        Assert.Equal(1, second.State.FindUnit(E3)!.HardTurnsLostThisFight);
        Assert.False(Holds(second.State, Conditions.UnyieldingId, E3));
    }

    // --- The shrine (plan call 13 step 7) ---

    [Fact]
    public void A_shrine_step_raises_its_decision_after_the_tick()
    {
        var s = Insert(ShrineState(), Daze, Hero, null);
        long daze = Seq(s, Daze, Hero);
        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), TickRules);

        Assert.True(r.Ok, r.Error.ToString());
        var pending = Assert.IsType<PendingDecision>(r.State.Pending);
        Assert.Equal<GameEvent>(
            [
                new UnitMoved(Hero, new GridPos(0, 0), new GridPos(1, 0)),
                new ConditionEnded(daze, Daze, Hero, ConditionEndReason.Expired),
                new TickAdvanced(1),
                new DecisionRequested(pending),
            ],
            r.Events.ToArray());
        Assert.Equal(ShrineState().Rng.Shrine.Skip(3), r.State.Rng.Shrine);
    }

    // --- Windows (Review Focus 5; CO 5.6) ---

    [Fact]
    public void A_window_whose_source_died_ends_SourceGone_at_the_end_of_the_tick()
    {
        var s = Insert(Insert(Calm(), MarkEnd, P1, E3), MarkStart, P2, E3);
        long end = Seq(s, MarkEnd, P1), start = Seq(s, MarkStart, P2);
        s = Harm.Deal(TickRules, s, E3, Lethal).Next;
        Assert.Null(s.FindUnit(E3));
        Assert.True(Holds(s, MarkEnd, P1) && Holds(s, MarkStart, P2));

        var r = Ok(s, East);

        Assert.Equal<GameEvent>(
            [
                new UnitMoved(P1, new GridPos(0, 1), new GridPos(1, 1)),
                new ConditionEnded(end, MarkEnd, P1, ConditionEndReason.SourceGone),
                new ConditionEnded(start, MarkStart, P2, ConditionEndReason.SourceGone),
                new TickAdvanced(1),
            ],
            r.Events.ToArray());
    }

    [Fact]
    public void A_window_opened_in_the_step_phase_lasts_until_the_next_tick()
    {
        // P1's move opens two windows on E3, sourced by P1, in the step phase of tick 1.
        var s = Give(Give(Calm(), P1, "t.move.mark_end"), P1, "t.move.mark_start");
        var first = Ok(s, East);
        Assert.True(Holds(first.State, MarkEnd, E3) && Holds(first.State, MarkStart, E3));
        Assert.DoesNotContain(first.Events, e => e is ConditionEnded);
        long end = Seq(first.State, MarkEnd, E3), start = Seq(first.State, MarkStart, E3);

        // Tick 2: the start window ends as P1's turn-start step begins, the end window as its turn-end step completes.
        var second = Ok(first.State, West);
        Assert.Equal<GameEvent>(
            [
                new ConditionEnded(start, MarkStart, E3, ConditionEndReason.Expired),
                new UnitMoved(P1, new GridPos(1, 1), new GridPos(0, 1)),
                new ConditionEnded(end, MarkEnd, E3, ConditionEndReason.Expired),
                new TickAdvanced(2),
            ],
            second.Events.ToArray());
    }

    [Fact]
    public void A_window_opened_between_ticks_lasts_one_tick()
    {
        var s = Insert(Insert(Calm(), MarkEnd, E3, P1), MarkStart, E3, P1);
        long end = Seq(s, MarkEnd, E3), start = Seq(s, MarkStart, E3);

        var r = Ok(s, East);

        Assert.Equal<GameEvent>(
            [
                new ConditionEnded(start, MarkStart, E3, ConditionEndReason.Expired),
                new UnitMoved(P1, new GridPos(0, 1), new GridPos(1, 1)),
                new ConditionEnded(end, MarkEnd, E3, ConditionEndReason.Expired),
                new TickAdvanced(1),
            ],
            r.Events.ToArray());
    }

    // --- A refused command (Review Focus 5) ---

    [Fact]
    public void A_refused_move_or_use_runs_no_tick_step()
    {
        var s = Insert(Insert(Calm(), Burn, P1, null), Daze, E3, null);

        var wall = Engine.Apply(s, new MoveCommand(P1, Direction.W), TickRules);
        Assert.Equal(CommandError.OutOfBounds, wall.Error);
        Assert.Same(s, wall.State);
        Assert.Empty(wall.Events);

        var unknown = Engine.Apply(s, new UseAbilityCommand(P1, "test.nope", new UnitsTarget([P2])), TickRules);
        Assert.Equal(CommandError.UnknownAbility, unknown.Error);
        Assert.Same(s, unknown.State);
        Assert.Empty(unknown.Events);

        var drawing = Engine.Apply(s, new UseAbilityCommand(P1, AbilityFixtures.Strike, new UnitsTarget([P2])), TickRules);
        Assert.Equal(CommandError.NotInCombat, drawing.Error);
        Assert.Same(s, drawing.State);
        Assert.Empty(drawing.Events);
    }

    // --- The counting test out of combat (CO 5.2: the step phase is the Act phase) ---

    [Fact]
    public void Out_of_combat_an_instance_applied_in_the_step_phase_counts_from_the_next_tick()
    {
        // Daze (turns:1) lands on P1 in tick 1's step phase: tick 1's turn end does not count it; tick 2's does.
        var s = Give(Calm(), P1, "t.move.daze");
        var first = Ok(s, East);
        Assert.Equal(0, State(first.State, Daze, P1).TurnsCounted);
        Assert.Equal(2L, State(first.State, Daze, P1).AppliedPhase);

        var second = Ok(first.State, West);
        Assert.Contains(second.Events, e => e is ConditionEnded { ConditionId: Daze, Reason: ConditionEndReason.Expired });
        Assert.False(Holds(second.State, Daze, P1));
    }

    [Fact]
    public void Out_of_combat_an_instance_applied_between_ticks_counts_at_the_next_tick()
    {
        var s = Insert(Calm(), Daze, P1, E3);
        var r = Ok(s, East);
        Assert.Contains(r.Events, e => e is ConditionEnded { ConditionId: Daze, Reason: ConditionEndReason.Expired });
    }

    [Fact]
    public void Out_of_combat_a_hard_instance_applied_in_the_step_phase_is_own_turn_and_ends_at_that_tick()
    {
        var s = Give(Calm(), P1, "t.move.stun");
        var r = Ok(s, East);

        var applied = Assert.Single(r.Events.OfType<ConditionApplied>(), a => a.ConditionId == Stun);
        Assert.Contains(r.Events, e => e is ConditionEnded { ConditionId: Stun, Reason: ConditionEndReason.Expired } c && c.Seq == applied.Seq);
        Assert.Contains(r.Events, e => e is ConditionApplied { ConditionId: Conditions.SteeledId } a && a.Holder == P1);
    }

    // --- Helpers ---

    // A trait with one trigger on `on` (scope `scope`) running `hook` with `args`.
    private static string Trigger(string id, string on, string scope, string hook, params string[] args) =>
        $$"""{"id": "{{id}}", "kind": "trait", "triggers": [{"on": "{{on}}", "scope": "{{scope}}", "do": [{"hook": "{{hook}}", "args": [{{string.Join(", ", args.Select(a => "\"" + a + "\""))}}]}]}]}""";

    // test.insert: args (condition, holder, source), each unit "holder" or an id; then the trigger's own instance goes, so
    // it fires once.
    private static GameState InsertOnce(HookContext ctx)
    {
        UnitId Who(string spec) => spec == "holder" ? ctx.Instance.Holder : new UnitId(int.Parse(spec, CultureInfo.InvariantCulture));

        var s = Effects.Remove(ctx.State, ctx.Instance.Seq);
        var (events, next) = Conditions.Insert(
            ctx.Rules, s, ctx.Args[0], Who(ctx.Args[1]), new EffectSource(Who(ctx.Args[2])), ConditionInsert.None);
        foreach (var e in events)
        {
            ctx.Emit(e);
        }

        return next;
    }
}
