using System.Collections.Immutable;
using System.Globalization;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// Trigger dispatch, C# hooks and the effect-aware engine (M1-07 Task 3): firing order by priority, source unit and
/// creation order (Review Focus 4), scopes and targets, hooks and their args, the chain bound, and the load-time hook
/// check (Review Focus 5).
/// </summary>
public class TriggerTests
{
    private const string EffectsFile = "effects/effects.json";

    private static readonly UnitId Ally = new(2);
    private static readonly UnitId Third = new(3);

    // Hook args seen by test.args, in call order. Only the hook test reads it.
    private static readonly List<ImmutableArray<string>> SeenArgs = [];

    private static readonly HookRegistry Hooks = HookRegistry.Core
        .With("test.mark", ctx => ctx.State)
        .With("test.args", ctx =>
        {
            SeenArgs.Add(ctx.Args);
            ctx.Emit(new TileChanged(new GridPos(0, 0), Tile.Wall));
            return Effects.Apply(ctx.State, "t.plain", ctx.Instance.Holder, EffectSource.Environment);
        })
        .With("test.chain", Chain);

    // A fixture event: an applied effect, whose subject is the new instance's holder.
    private static readonly ImmutableArray<RuleEvent> Events =
        [.. RuleEvents.Core, RuleEvent.Of<EffectApplied>("test.effectApplied", e => e.Holder)];

    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("t.p0", Hook(0, "holder", "test.mark")),
            Def("t.p1", Hook(1, "holder", "test.mark")),
            Def("t.any", Hook(0, "any", "test.mark")),
            Def("t.args", """{"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.args", "args": ["a", "b"]}]}"""),
            Def("t.plain"),
            Def("t.give_subject", """{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "t.plain", "to": "subject"}]}"""),
            Def("t.give_holder", """{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "t.plain", "to": "holder"}]}"""),
            Def(
                "t.loop",
                """{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "t.loop", "to": "holder"}]}""",
                """{"on": "test.effectApplied", "scope": "holder", "do": [{"applyEffect": "t.loop", "to": "holder"}]}"""),
            Def("t.chain63", """{"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.chain", "args": ["63"]}]}"""),
            Def("t.chain64", """{"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.chain", "args": ["64"]}]}"""),
            Def("t.on_applied", """{"on": "test.effectApplied", "scope": "any", "do": [{"hook": "test.mark"}]}""")),
        events: Events,
        hooks: Hooks);

    private static readonly MoveCommand HeroEast = new(Hero, Direction.E);

    // --- Review Focus 4: trigger order ---

    [Fact]
    public void Triggers_fire_by_priority_then_source_unit_then_creation_order()
    {
        var r = Engine.Apply(FourOnTheHero(), HeroEast, Rules);
        Assert.True(r.Ok);
        Assert.Equal<long>([4, 3, 2, 1], FiredSeqs(r));
    }

    [Fact]
    public void Shuffling_the_instances_cannot_change_the_firing_order()
    {
        var s = FourOnTheHero();
        var shuffles = new[]
        {
            s with { Effects = [.. s.Effects.Reverse()] },
            s with { Effects = [s.Effects[2], s.Effects[0], s.Effects[3], s.Effects[1]] },
            s with { Effects = [s.Effects[1], s.Effects[3], s.Effects[0], s.Effects[2]] },
        };
        foreach (var shuffled in shuffles)
        {
            Assert.Equal<long>([4, 3, 2, 1], FiredSeqs(Engine.Apply(shuffled, HeroEast, Rules)));
        }
    }

    // --- Scope and targets ---

    [Fact]
    public void A_holder_scope_fires_only_for_its_holder_and_any_fires_for_every_subject()
    {
        var s = Effects.Apply(Start(), "t.p0", Ally, new EffectSource(Ally));
        s = Effects.Apply(s, "t.any", Ally, new EffectSource(Ally));
        var r = Engine.Apply(s, HeroEast, Rules);
        Assert.Equal<long>([2], FiredSeqs(r));
    }

    [Fact]
    public void ApplyEffect_gives_the_effect_to_the_subject_or_to_the_holder()
    {
        var s = Effects.Apply(Start(), "t.give_subject", Ally, new EffectSource(Third));
        s = Effects.Apply(s, "t.give_holder", Ally, EffectSource.Environment);
        var r = Engine.Apply(s, HeroEast, Rules);
        Assert.Equal<GameEvent>(
            [
                new UnitMoved(Hero, new GridPos(1, 1), new GridPos(2, 1)),
                new TickAdvanced(1),
                new EffectApplied(3, "t.plain", Hero, new EffectSource(Ally)),
                new EffectApplied(4, "t.plain", Ally, new EffectSource(Ally)),
            ],
            r.Events.ToArray());
        Assert.Equal(new EffectInstance(3, "t.plain", Hero, new EffectSource(Ally)), r.State.Effects[2]);
        Assert.Equal(new EffectInstance(4, "t.plain", Ally, new EffectSource(Ally)), r.State.Effects[3]);
        Assert.Equal(5, r.State.NextEffectSeq);
    }

    // --- Hooks ---

    [Fact]
    public void A_hook_runs_with_its_args_and_its_state_and_events_are_kept()
    {
        SeenArgs.Clear();
        var s = Effects.Apply(Start(), "t.args", Hero, new EffectSource(Hero));
        var r = Engine.Apply(s, HeroEast, Rules);
        Assert.Single(SeenArgs);
        Assert.Equal<string>(["a", "b"], SeenArgs[0].ToArray());
        Assert.Equal<GameEvent>(
            [
                new UnitMoved(Hero, new GridPos(1, 1), new GridPos(2, 1)),
                new TickAdvanced(1),
                new HookFired("test.args", 1),
                new TileChanged(new GridPos(0, 0), Tile.Wall),
            ],
            r.Events.ToArray());
        Assert.Equal(new EffectInstance(2, "t.plain", Hero, EffectSource.Environment), r.State.Effects[1]);
    }

    [Fact]
    public void Events_raised_by_actions_join_the_queue_and_fire_their_own_triggers()
    {
        var s = Effects.Apply(Start(), "t.give_subject", Ally, new EffectSource(Ally));
        s = Effects.Apply(s, "t.on_applied", Third, new EffectSource(Third));
        var r = Engine.Apply(s, HeroEast, Rules);
        Assert.Equal<GameEvent>(
            [
                new UnitMoved(Hero, new GridPos(1, 1), new GridPos(2, 1)),
                new TickAdvanced(1),
                new EffectApplied(3, "t.plain", Hero, new EffectSource(Ally)),
                new HookFired("test.mark", 2),
            ],
            r.Events.ToArray());
    }

    // --- The chain bound ---

    [Fact]
    public void A_self_reapplying_trigger_stops_at_the_firing_bound()
    {
        var s = Effects.Apply(Start(), "t.loop", Hero, new EffectSource(Hero));
        var ex = Assert.Throws<TriggerLoopException>(() => Engine.Apply(s, HeroEast, Rules));
        Assert.Equal(Effects.MaxTriggerFirings, ex.Firings);
        Assert.Equal(64, Effects.MaxTriggerFirings);
    }

    [Fact]
    public void A_chain_of_exactly_the_bound_completes_and_one_more_throws()
    {
        var ok = Engine.Apply(Effects.Apply(Start(), "t.chain63", Hero, new EffectSource(Hero)), HeroEast, Rules);
        Assert.Equal(64, ok.Events.Count(e => e is HookFired));

        var ex = Assert.Throws<TriggerLoopException>(
            () => Engine.Apply(Effects.Apply(Start(), "t.chain64", Hero, new EffectSource(Hero)), HeroEast, Rules));
        Assert.Equal(64, ex.Firings);

        // The move raises 2 events; firing k then adds HookFired at 2k and its re-raised UnitMoved at 2k + 1. The 65th
        // firing would answer the UnitMoved the 64th raised, at 129.
        Assert.Equal(129, ex.EventIndex);
    }

    // --- No listener, no rules ---

    [Fact]
    public void An_event_no_trigger_listens_to_changes_nothing()
    {
        var s = Effects.Apply(Start(), "t.on_applied", Hero, new EffectSource(Hero));
        s = Effects.Apply(s, "t.plain", Hero, new EffectSource(Hero));
        var with = Engine.Apply(s, HeroEast, Rules);
        var without = Engine.Apply(s with { Effects = [] }, HeroEast);
        Assert.Equal(without.Events.AsEnumerable(), with.Events);
        Assert.Equal(StateDigest.Of(without.State with { Effects = s.Effects }), StateDigest.Of(with.State));
    }

    // Engine.Apply(state, command) on a state without effects does what M0 did: the move, the tick and the shrine
    // decision, with no trigger events and the effect counter untouched.
    [Fact]
    public void Apply_without_rules_on_a_state_without_effects_is_the_m0_move()
    {
        var s = ShrineState();
        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E));
        Assert.True(r.Ok);
        var pending = Assert.IsType<PendingDecision>(r.State.Pending);
        Assert.Equal(
            (new DecisionId(1), Hero, DecisionKind.ShrineBoon, new GridPos(1, 0)),
            (pending.Id, pending.Unit, pending.Kind, pending.Source));
        Assert.Equal(3, pending.Options.Distinct().Count());
        Assert.Equal<GameEvent>(
            [new UnitMoved(Hero, new GridPos(0, 0), new GridPos(1, 0)), new TickAdvanced(1), new DecisionRequested(pending)],
            r.Events.ToArray());
        Assert.Equal(new GridPos(1, 0), r.State.Units[0].Pos);
        Assert.Equal(1, r.State.Tick);
        Assert.Equal(s.Rng.Combat, r.State.Rng.Combat);
        Assert.Equal(s.Rng.Shrine.Skip(3), r.State.Rng.Shrine);
        Assert.Empty(r.State.Effects);
        Assert.Equal(1, r.State.NextEffectSeq);
    }

    // The old overload has no effect definitions, so a state holding instances cannot answer a rule event with it:
    // replaying such a state needs Engine.Apply(state, command, rules). Pinned until replay is effect-aware.
    [Fact]
    public void Apply_without_rules_throws_when_a_rule_event_meets_an_effect_instance()
    {
        var s = Effects.Apply(Start(), "t.plain", Hero, EffectSource.Environment);
        Assert.Throws<ArgumentException>(() => Engine.Apply(s, HeroEast));
        Assert.Throws<ArgumentException>(() => Replay.Run(s, [HeroEast]));
        Assert.True(Engine.Apply(s, HeroEast, Rules).Ok);
    }

    [Fact]
    public void A_rejected_command_fires_nothing()
    {
        var s = Effects.Apply(Start(), "t.p0", Hero, new EffectSource(Hero));
        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.W), Rules);
        Assert.Equal(CommandError.Blocked, r.Error);
        Assert.Empty(r.Events);
        Assert.Same(s, r.State);
    }

    // --- The hook registry ---

    [Fact]
    public void The_hook_registry_refuses_duplicates_and_bad_ids()
    {
        Assert.False(HookRegistry.Core.Contains("test.mark"));
        Assert.True(Hooks.Contains("test.mark"));
        Assert.False(Hooks.Contains("test.nope"));
        Assert.Throws<ArgumentException>(() => Hooks.With("test.mark", ctx => ctx.State));
        Assert.Throws<ArgumentException>(() => HookRegistry.Core.With("Not An Id", ctx => ctx.State));
        Assert.Throws<ArgumentNullException>(() => HookRegistry.Core.With("test.x", null!));
    }

    // --- Review Focus 5: load-time references ---

    [Theory]
    [InlineData("""{"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.mark"}, {"hook": "test.nope"}]}""", "$.effects[1].triggers[0].do[1].hook")]
    [InlineData("""{"on": "test.nope", "scope": "holder", "do": [{"hook": "test.mark"}]}""", "$.effects[1].triggers[0].on")]
    public void An_unknown_hook_or_event_fails_with_the_file_and_field(string trigger, string field)
    {
        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(
            LoadEffects(Def("ok"), $$"""{"id": "x.bad", "kind": "gear", "triggers": [{{trigger}}]}"""),
            events: Events,
            hooks: Hooks));
        Assert.Equal((EffectsFile, field), (ex.File, ex.Field));
    }

    [Fact]
    public void An_unknown_number_target_fails_with_the_file_and_field()
    {
        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(
            LoadEffects(Def("ok"), """{"id": "x.bad", "kind": "gear", "modifiers": [{"target": "nope", "add": 1}]}"""),
            hooks: Hooks));
        Assert.Equal((EffectsFile, "$.effects[1].modifiers[0].target"), (ex.File, ex.Field));
    }

    [Fact]
    public void A_registered_hook_loads()
    {
        var rules = RulesContext.Create(LoadEffects(Def("t.p0", Hook(0, "holder", "test.mark"))), hooks: Hooks);
        Assert.Equal(EffectSourceKind.Trait, rules.Definition("t.p0").Kind);
    }

    // --- Helpers ---

    // Three units: the hero at (1,1), an ally at (3,3) and a third at (1,3) in the ring room. The hero can step east.
    private static GameState Start() =>
        GameState.Create(GridMap.Parse("#####", "#...#", "#.#.#", "#...#", "#####"), [U(1, 1, 1), U(2, 3, 3), U(3, 1, 3)], 7);

    // A (priority 1, unit 1), B (priority 0, environment), C (priority 0, unit 2), D (priority 0, unit 1).
    private static GameState FourOnTheHero()
    {
        var s = Effects.Apply(Start(), "t.p1", Hero, new EffectSource(Hero));
        s = Effects.Apply(s, "t.p0", Hero, EffectSource.Environment);
        s = Effects.Apply(s, "t.p0", Hero, new EffectSource(Ally));
        return Effects.Apply(s, "t.p0", Hero, new EffectSource(Hero));
    }

    // An event with value equality: a decision's options are an ImmutableArray, which compares by reference.
    private static long[] FiredSeqs(StepResult r) => r.Events.OfType<HookFired>().Select(h => h.Seq).ToArray();

    // Re-raises the holder's move until it has fired args[0] + 1 times, keeping count with t.plain instances.
    private static GameState Chain(HookContext ctx)
    {
        int limit = int.Parse(ctx.Args[0], CultureInfo.InvariantCulture);
        int count = ctx.State.Effects.Count(e => e.EffectId == "t.plain");
        if (count >= limit)
        {
            return ctx.State;
        }

        var pos = ctx.State.FindUnit(ctx.Instance.Holder)!.Pos;
        ctx.Emit(new UnitMoved(ctx.Instance.Holder, pos, pos));
        return Effects.Apply(ctx.State, "t.plain", ctx.Instance.Holder, EffectSource.Environment);
    }

    private static string Hook(int priority, string scope, string hook) =>
        $$"""{"on": "unitMoved", "priority": {{priority}}, "scope": "{{scope}}", "do": [{"hook": "{{hook}}"}]}""";

    private static string Def(string id, params string[] triggers) =>
        $$"""{"id": "{{id}}", "kind": "trait", "triggers": [{{string.Join(", ", triggers)}}]}""";

    private static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile(EffectsFile, "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}
