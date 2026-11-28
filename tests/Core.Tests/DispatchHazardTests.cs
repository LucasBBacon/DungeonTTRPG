using System.Collections.Immutable;
using System.Globalization;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// Dispatch hazards (M1-12a Task 2; plan calls 10 and 11): a firing whose instance is gone is skipped and a live one fires
/// with the current instance; an <c>applyEffect</c> whose recipient is gone is skipped; an instance made by a firing
/// answers later events but not the one being processed; and an end-turn command dispatches the ending turn's events
/// before the next turn starts, so <c>turnEnded</c> triggers see the ending unit and a win there starts no next turn.
/// </summary>
public class DispatchHazardTests
{
    private static readonly UnitId Hero = new(1);
    private static readonly UnitId Two = new(2);
    private static readonly UnitId Three = new(3);

    // What the recording hooks saw, in call order. Tests in one class run one at a time; each clears these first.
    private static readonly List<EffectInstance> SeenInstances = [];
    private static readonly List<(UnitId Unit, bool Vacated, bool Present)> SeenEnds = [];

    private static readonly HookRegistry Hooks = HookRegistry.Core
        .With("test.kill", Kill)
        .With("test.mark", ctx => ctx.State)
        .With("test.mark_applied", ctx => ctx.State)
        .With("test.record", ctx =>
        {
            SeenInstances.Add(ctx.Instance);
            return ctx.State;
        })
        .With("test.remove_victims", ctx =>
        {
            var s = ctx.State;
            foreach (long seq in s.Effects.Where(e => e.EffectId == "t.victim").Select(e => e.Seq).ToArray())
            {
                s = Effects.Effects.Remove(s, seq);
            }

            return s;
        })
        .With("test.rewrite_victims", ctx => ctx.State with
        {
            Effects = [.. ctx.State.Effects.Select(e => e.EffectId == "t.victim" ? e with { Source = EffectSource.Environment } : e)],
        })
        .With("test.see_end", ctx =>
        {
            var unit = ((TurnEnded)ctx.Event).Unit;
            SeenEnds.Add((unit, ctx.State.Combat!.TurnVacated, ctx.State.FindUnit(unit) is not null));
            return ctx.State;
        });

    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("t.plain"),
            Def("t.remover", """{"on": "unitMoved", "priority": 0, "scope": "any", "do": [{"hook": "test.remove_victims"}]}"""),
            Def("t.rewriter", """{"on": "unitMoved", "priority": 0, "scope": "any", "do": [{"hook": "test.rewrite_victims"}]}"""),
            Def("t.victim", """{"on": "unitMoved", "priority": 1, "scope": "any", "do": [{"hook": "test.record"}]}"""),
            Def("t.kill_three", """{"on": "unitMoved", "priority": 0, "scope": "any", "do": [{"hook": "test.kill", "args": ["3"]}]}"""),
            Def("t.give_holder", """{"on": "unitMoved", "priority": 1, "scope": "any", "do": [{"applyEffect": "t.plain", "to": "holder"}]}"""),
            Def("t.give_subject", """{"on": "unitMoved", "priority": 1, "scope": "any", "do": [{"applyEffect": "t.plain", "to": "subject"}]}"""),
            Def("t.see_end", """{"on": "turnEnded", "scope": "any", "do": [{"hook": "test.see_end"}]}"""),
            Def("t.win_on_end", """{"on": "turnEnded", "scope": "holder", "do": [{"hook": "test.kill", "args": ["2"]}]}"""),
            Def("t.grant", """{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "t.granted", "to": "holder"}]}"""),
            Def(
                "t.granted",
                """{"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.mark"}]}""",
                """{"on": "test.effectApplied", "scope": "holder", "do": [{"hook": "test.mark_applied"}]}""")),
        events: [.. RuleEvents.Core, RuleEvent.Of<EffectApplied>("test.effectApplied", e => e.Holder)],
        hooks: Hooks,
        pools: PoolTable,
        abilities: Definitions);

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......", ".......", ".......", ".......", ".......");

    // The hero (1,1), foe 2 (5,5) and companion 3 (3,3), in the given order with slot `current` holding the turn.
    private static GameState Fight(int current, params int[] order) =>
        GameState.Create(Open, [Party(1, 1, 1), Foe(2, 5, 5), Party(3, 3, 3)], 7, hero: Hero) with
        {
            Combat = new CombatState([.. order.Select(i => new UnitId(i))], current, 1),
        };

    private static GameState Give(GameState s, UnitId unit, string effect) =>
        Effects.Effects.Apply(s, effect, unit, new EffectSource(unit));

    private static Type[] Types(StepResult r) => r.Events.Select(e => e.GetType()).ToArray();

    // --- Plan call 11: stale firings ---

    [Fact]
    public void A_firing_whose_instance_was_removed_is_skipped()
    {
        SeenInstances.Clear();
        var s = Give(Give(Fight(0, 1, 2, 3), Hero, "t.remover"), Three, "t.victim");

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal([new HookFired("test.remove_victims", 1)], r.Events.OfType<HookFired>().ToArray());
        Assert.Empty(SeenInstances);
        Assert.DoesNotContain(r.State.Effects, e => e.EffectId == "t.victim");
    }

    [Fact]
    public void A_firing_on_a_killed_holder_is_skipped()
    {
        var s = Give(Give(Fight(0, 1, 2, 3), Hero, "t.kill_three"), Three, "t.give_holder");

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal([typeof(UnitMoved), typeof(HookFired), typeof(DamageDealt), typeof(UnitDied)], Types(r));
        Assert.Equal(new UnitDied(Three, new GridPos(3, 3)), r.Events[3]);
        Assert.Empty(r.State.Effects.Where(e => e.EffectId == "t.plain"));
    }

    [Fact]
    public void An_applyEffect_to_a_removed_subject_is_skipped()
    {
        // Companion 3 holds the turn and moves; the hero's first trigger kills it, so the second has no subject.
        var s = Give(Give(Fight(0, 3, 1, 2), Hero, "t.kill_three"), Hero, "t.give_subject");

        var r = Engine.Apply(s, new MoveCommand(Three, Direction.E), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.DoesNotContain(r.Events, e => e is EffectApplied);
        Assert.Contains(new UnitDied(Three, new GridPos(4, 3)), r.Events);
        Assert.Equal(new TurnStarted(Hero, 1), r.Events[^1]);
        Assert.DoesNotContain(r.State.Effects, e => e.EffectId == "t.plain");
    }

    [Fact]
    public void A_firing_uses_the_current_instance()
    {
        SeenInstances.Clear();
        var s = Give(Give(Fight(0, 1, 2, 3), Hero, "t.rewriter"), Three, "t.victim");

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal([new EffectInstance(2, "t.victim", Three, EffectSource.Environment)], SeenInstances);
    }

    [Fact]
    public void A_new_instance_answers_later_queued_events_but_not_the_current_one()
    {
        var s = Give(Fight(0, 1, 2, 3), Hero, "t.grant");

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal(
            [
                new UnitMoved(Hero, new GridPos(1, 1), new GridPos(2, 1)),
                new EffectApplied(2, "t.granted", Hero, new EffectSource(Hero)),
                new HookFired("test.mark_applied", 2),
            ],
            r.Events.ToArray());
    }

    // --- Plan call 10: the EndTurn split ---

    [Theory]
    [InlineData(1)] // the hero in the middle slot of [3, 1, 2]; foe 2 is next, in round 1
    [InlineData(2)] // the hero in the last slot of [3, 2, 1]; companion 3 is next, in round 2
    public void TurnEnded_triggers_see_the_ending_unit(int current)
    {
        SeenEnds.Clear();
        var s = Give(current == 1 ? Fight(1, 3, 1, 2) : Fight(2, 3, 2, 1), Two, "t.see_end");

        var r = Engine.Apply(s, new EndTurnCommand(Hero), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal([(Hero, true, true)], SeenEnds);
        long round = current == 2 ? 2 : 1;
        var next = current == 2 ? Three : Two;
        Assert.Equal(
            [new TurnEnded(Hero), new HookFired("test.see_end", 1), new TurnStarted(next, round)],
            r.Events.ToArray());
        Assert.Equal(next, r.State.Combat!.CurrentUnit);
        Assert.Equal(round, r.State.Combat.Round);
        Assert.False(r.State.Combat.TurnVacated);
    }

    [Fact]
    public void A_turnEnded_trigger_fires_once()
    {
        var s = Give(Fight(0, 1, 2, 3), Two, "t.see_end");

        var r = Engine.Apply(s, new EndTurnCommand(Hero), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Single(r.Events.OfType<HookFired>());
    }

    [Fact]
    public void A_win_in_a_turnEnded_trigger_starts_no_next_turn()
    {
        var s = Give(Fight(0, 1, 2, 3), Hero, "t.win_on_end");

        var r = Engine.Apply(s, new EndTurnCommand(Hero), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal(
            [typeof(TurnEnded), typeof(HookFired), typeof(DamageDealt), typeof(UnitDied), typeof(CombatEnded)],
            Types(r));
        Assert.Equal(new TurnEnded(Hero), r.Events[0]);
        Assert.DoesNotContain(r.Events, e => e is TurnStarted);
        Assert.Null(r.State.Combat);
    }

    [Fact]
    public void End_turn_events_are_unchanged_without_triggers()
    {
        var s = Fight(2, 3, 2, 1);

        var r = Engine.Apply(s, new EndTurnCommand(Hero), Rules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal([new TurnEnded(Hero), new TurnStarted(Three, 2)], r.Events.ToArray());
        Assert.Equal(0, r.State.Combat!.Current);
        Assert.Equal(2, r.State.Combat.Round);
        Assert.False(r.State.Combat.TurnVacated);
    }

    [Fact]
    public void Advance_still_ends_the_turn_and_starts_the_next()
    {
        var (events, next) = Turns.Advance(Rules, Fight(2, 3, 2, 1));

        Assert.Equal([new TurnEnded(Hero), new TurnStarted(Three, 2)], events.ToArray());
        Assert.Equal(0, next.Combat!.Current);
        Assert.Equal(2, next.Combat.Round);
        Assert.False(next.Combat.TurnVacated);
    }

    [Fact]
    public void EndTurn_leaves_the_turn_vacated_on_the_next_slot_without_wrapping()
    {
        var (events, next) = Turns.EndTurn(Rules, Fight(2, 3, 2, 1));

        Assert.Equal([new TurnEnded(Hero)], events.ToArray());
        Assert.Equal(3, next.Combat!.Current);
        Assert.Equal(1, next.Combat.Round);
        Assert.True(next.Combat.TurnVacated);
    }

    private static GameState Kill(HookContext ctx)
    {
        var target = new UnitId(int.Parse(ctx.Args[0], CultureInfo.InvariantCulture));
        var part = new DamagePart(DamageType.Bludgeoning, 0, 0, 1000);
        var roll = new DamageRoll([new PartRoll(part, [], 1000)], false);
        var (harm, next) = Harm.Deal(ctx.Rules, ctx.State, target, roll);
        foreach (var e in harm.Events)
        {
            ctx.Emit(e);
        }

        return next;
    }

    private static string Def(string id, params string[] triggers) =>
        $$"""{"id": "{{id}}", "kind": "trait", "triggers": [{{string.Join(", ", triggers)}}]}""";

    private static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile("effects/effects.json", "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}
