using System.Collections.Immutable;
using System.Globalization;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// The settle loop and dispatch phases (M1-11 Task 4; plan call 8; Review Focus 1): dispatch stops once the run has
/// ended, and after every accepted command the loop ends a won fight or starts a vacated turn, after the command's own
/// events and their triggers.
/// </summary>
public class SettleTests
{
    private static readonly UnitId Hero = new(1);
    private static readonly UnitId Two = new(2);
    private static readonly UnitId Three = new(3);

    // test.kill deals 1000 to the holder of the firing instance, or to the unit named by its one arg.
    private static readonly HookRegistry Hooks = HookRegistry.Core
        .With("test.kill", Kill)
        .With("test.mark", ctx => ctx.State);

    private static readonly RulesContext SettleRules = RulesContext.Create(
        LoadEffects(
            Def("t.kill_on_resolve", """{"on": "abilityResolved", "scope": "holder", "do": [{"hook": "test.kill"}]}"""),
            Def("t.kill_on_move", """{"on": "unitMoved", "priority": 0, "scope": "holder", "do": [{"hook": "test.kill"}]}"""),
            Def("t.mark_on_move", """{"on": "unitMoved", "priority": 1, "scope": "holder", "do": [{"hook": "test.mark"}]}"""),
            Def(
                "t.kill_then_give",
                """{"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.kill"}, {"applyEffect": "t.plain", "to": "holder"}]}"""),
            Def("t.kill_two_on_move", """{"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.kill", "args": ["2"]}]}"""),
            Def("t.plain")),
        hooks: Hooks,
        pools: PoolTable,
        abilities: Definitions);

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......", ".......", ".......", ".......", ".......");

    private static GameState Give(GameState s, UnitId unit, string effect) =>
        Dungeon.Core.Effects.Effects.Apply(s, effect, unit, new EffectSource(unit));

    // The hero (1,1) and foe 2 (5,5), in combat on the hero's turn.
    private static GameState HeroTurn() =>
        InCombat(GameState.Create(Open, [Party(1, 1, 1), Foe(2, 5, 5)], 7, hero: Hero), 1, 2);

    [Fact]
    public void Dispatch_stops_at_the_run_end()
    {
        var s = Give(Give(HeroTurn(), Hero, "t.mark_on_move"), Hero, "t.kill_on_move");

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), SettleRules);

        Assert.True(r.Ok);
        Assert.Equal(
            [typeof(UnitMoved), typeof(HookFired), typeof(DamageDealt), typeof(RunEnded)],
            r.Events.Select(e => e.GetType()).ToArray());
        Assert.Equal("test.kill", Assert.IsType<HookFired>(r.Events[1]).HookId);
        Assert.NotNull(r.State.Ended);
    }

    [Fact]
    public void Dispatch_stops_between_the_actions_of_one_firing()
    {
        var s = Give(HeroTurn(), Hero, "t.kill_then_give");

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), SettleRules);

        Assert.True(r.Ok);
        Assert.IsType<RunEnded>(r.Events[^1]);
        Assert.DoesNotContain(r.Events, e => e is EffectApplied);
        Assert.Single(r.State.Effects);
    }

    [Fact]
    public void A_kill_by_trigger_on_a_move_settles()
    {
        var s = Give(HeroTurn(), Hero, "t.kill_two_on_move");

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), SettleRules);

        Assert.True(r.Ok);
        Assert.Equal(
            [typeof(UnitMoved), typeof(HookFired), typeof(DamageDealt), typeof(UnitDied), typeof(TurnEnded), typeof(CombatEnded)],
            r.Events.Select(e => e.GetType()).ToArray());
        Assert.Equal(new UnitDied(Two, new GridPos(5, 5)), r.Events[3]);
        Assert.Equal(new TurnEnded(Hero), r.Events[4]);
        Assert.Null(r.State.Combat);
    }

    [Fact]
    public void A_trigger_that_kills_the_user_after_its_ability_settles_the_turn()
    {
        // Companion 2 holds the turn, then the hero, then foe 3.
        var s = InCombat(GameState.Create(Open, [Party(1, 5, 5), Party(2, 1, 1), Foe(3, 2, 1)], 7, hero: Hero), 2, 1, 3);
        s = Give(s, Two, "t.kill_on_resolve");

        var r = Engine.Apply(s, new UseAbilityCommand(Two, Sure, new UnitsTarget([Three])), SettleRules);

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Equal(
            [
                typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(DamageRolled), typeof(DamageDealt),
                typeof(AbilityResolved), typeof(HookFired), typeof(DamageDealt), typeof(UnitDied), typeof(TurnStarted),
            ],
            r.Events.Select(e => e.GetType()).ToArray());
        Assert.Equal(Three, Assert.IsType<DamageDealt>(r.Events[4]).Target);
        Assert.Equal(Two, Assert.IsType<DamageDealt>(r.Events[7]).Target);
        Assert.Equal(new UnitDied(Two, new GridPos(1, 1)), r.Events[8]);
        Assert.Equal(new TurnStarted(Hero, 1), r.Events[9]);
        Assert.Equal(Hero, r.State.Combat!.CurrentUnit);
        Assert.False(r.State.Combat.TurnVacated);
    }

    private static GameState Kill(HookContext ctx)
    {
        var target = ctx.Args.IsDefaultOrEmpty
            ? ctx.Instance.Holder
            : new UnitId(int.Parse(ctx.Args[0], CultureInfo.InvariantCulture));
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
