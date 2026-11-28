using System.Collections.Immutable;
using System.Globalization;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// Openers (M1-11 Task 6; plan calls 4 to 8; owner rulings 2 and 3; M1-10 rulings record 4.1, 5 and 6.3): a hostile use
/// out of combat is validated on the step-4 view, runs combat start first, is charged to the striker's round 1, resolves
/// before any turn, and takes no tick.
/// </summary>
public class OpenerTests
{
    private static readonly UnitId Hero = new(1);
    private static readonly UnitId Two = new(2);
    private static readonly UnitId Three = new(3);

    /// <summary>An action strike that spends <c>pool.turn</c> (a once-per-round use): single, range 1, a flat 1.</summary>
    private const string TurnStrike = "test.turn_strike";

    /// <summary>An action strike that spends <c>pool.fight</c>: single, range 1, a flat 1.</summary>
    private const string FightStrike = "test.fight_strike";

    private static readonly HookRegistry Hooks = HookRegistry.Core
        .With("test.kill", Kill)
        .With("test.probe", Probe);

    private static readonly RulesContext Rules = RulesContext.Create(
        ContestKit.LoadEffects(
            ContestKit.Def(Clumsy, "condition", ContestKit.Add("reflex", -50)),
            Trait("t.probe_on_start", """{"on": "combatStarted", "scope": "any", "do": [{"hook": "test.probe"}]}"""),
            Trait("t.kill_hero_on_start", """{"on": "combatStarted", "scope": "any", "do": [{"hook": "test.kill", "args": ["1"]}]}"""),
            Trait("t.kill_three_on_start", """{"on": "combatStarted", "scope": "any", "do": [{"hook": "test.kill", "args": ["3"]}]}""")),
        hooks: Hooks,
        pools: PoolTable,
        abilities:
        [
            .. Definitions,
            Flat(TurnStrike, new PoolUse("pool.turn", 1)),
            Flat(FightStrike, new PoolUse("pool.fight", 1)),
        ]);

    private static readonly ImmutableArray<string> Held =
        [.. AllIds.Append(TurnStrike).Append(FightStrike).OrderBy(id => id, StringComparer.Ordinal)];

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......", ".......", ".......", ".......", ".......");

    private static AbilityDefinition Flat(string id, PoolUse pool) =>
        new(AbilityFixtures.File, id, AbilityKind.Limited, new AbilityCost(CostKind.Action), pool,
            new TargetSpec(TargetShape.Single, 1, 1, 0, false), null, [new DamagePart(DamageType.Bludgeoning, 0, 0, 1)], [], null);

    private static Unit P(int id, int x, int y, int hp = Fixtures.FullHp) => Party(id, x, y, hp) with { Abilities = Held };

    private static Unit F(int id, int x, int y, int hp = 12) => Foe(id, x, y, hp) with { Abilities = Held };

    // The hero (1,1) beside foe 2 (2,1), out of combat.
    private static GameState Duel(ulong seed) => GameState.Create(Open, [P(1, 1, 1), F(2, 2, 1)], seed, hero: Hero);

    // As Duel, with companion 3 (1,2) beside the hero.
    private static GameState Trio(ulong seed) =>
        GameState.Create(Open, [P(1, 1, 1), F(2, 2, 1), P(3, 1, 2)], seed, hero: Hero);

    // The first seed whose initiative order (as combat start would roll it) satisfies the predicate.
    private static ulong SeedWhere(Func<ulong, GameState> make, Func<ImmutableArray<UnitId>, bool> wanted)
    {
        for (ulong seed = 1; seed < 1000; seed++)
        {
            if (wanted(Turns.BeginCombat(Rules, make(seed)).Next.Combat!.Order))
            {
                return seed;
            }
        }

        throw new InvalidOperationException("No seed gives the wanted order.");
    }

    private static ulong HeroFirst(Func<ulong, GameState> make) => SeedWhere(make, o => o[0] == Hero);

    private static ulong HeroLater(Func<ulong, GameState> make) => SeedWhere(make, o => o[0] != Hero);

    private static UseAbilityCommand Use(UnitId user, string ability, params UnitId[] targets) =>
        new(user, ability, new UnitsTarget([.. targets]));

    private static StepResult Apply(GameState s, Command c) => Engine.Apply(s, c, Rules);

    private static StepResult Ok(GameState s, Command c)
    {
        var r = Apply(s, c);
        Assert.True(r.Ok, r.Error.ToString());
        return r;
    }

    private static Type[] Types(StepResult r) => r.Events.Select(e => e.GetType()).ToArray();

    private static GameState Give(GameState s, UnitId unit, string effect) => Effects.Apply(s, effect, unit, new EffectSource(unit));

    private static GameState Edit(GameState s, UnitId id, Func<Unit, Unit> change)
    {
        var u = s.FindUnit(id)!;
        return s with { Units = s.Units.SetItem(s.Units.IndexOf(u), change(u)) };
    }

    // Ends turns until the unit holds one.
    private static GameState UntilTurnOf(GameState s, UnitId unit)
    {
        while (s.Combat!.CurrentUnit != unit)
        {
            s = Ok(s, new EndTurnCommand(s.Combat.CurrentUnit)).State;
        }

        return s;
    }

    private static GameState Loaded(GameState s) => Snapshot.Load(Snapshot.Save(s));

    private static long Speed(GameState s, UnitId unit) => Effects.Number(Rules, s, unit, "speed");

    // --- The charge (tests 1 to 4) ---

    [Fact]
    public void The_opener_spends_the_action_wherever_the_striker_rolls()
    {
        foreach (var seed in new[] { HeroFirst(Duel), HeroLater(Duel) })
        {
            var r = Ok(Duel(seed), Use(Hero, Sure, Two));
            Assert.Equal(1, r.State.FindUnit(Hero)!.Budget.ActionsUsed);
            Assert.Equal(Snapshot.Save(r.State), Snapshot.Save(Loaded(r.State)));

            var turn = UntilTurnOf(r.State, Hero);
            var budget = turn.FindUnit(Hero)!.Budget;
            Assert.Equal((1, false, 0), (budget.ActionsUsed, budget.BonusUsed, budget.MovementUsed));
            Assert.Equal(Speed(turn, Hero), Turns.MovementLeft(Rules, turn, Hero));
            Assert.Null(turn.FindUnit(Hero)!.Opener);

            Assert.Equal(CommandError.NoActionLeft, Apply(turn, Use(Hero, Sure, Two)).Error);
            var jab = Ok(turn, Use(Hero, Jab, Two));
            Assert.True(jab.State.FindUnit(Hero)!.Budget.BonusUsed);
        }
    }

    [Fact]
    public void A_bonus_opener_charges_the_bonus_and_keeps_the_action()
    {
        var r = Ok(Duel(HeroLater(Duel)), Use(Hero, Jab, Two));
        Assert.Equal(new OpenerCharge(new TurnBudget(0, 0, true, false, false, false), []), r.State.FindUnit(Hero)!.Opener, OpenerComparer.Instance);

        var turn = UntilTurnOf(r.State, Hero);
        var budget = turn.FindUnit(Hero)!.Budget;
        Assert.Equal((true, 0), (budget.BonusUsed, budget.ActionsUsed));
        Assert.True(Ok(turn, Use(Hero, Sure, Two)).Ok);
    }

    [Fact]
    public void A_move_opener_charges_its_movement()
    {
        var r = Ok(Duel(HeroLater(Duel)), Use(Hero, Dash, Two));

        var turn = UntilTurnOf(r.State, Hero);
        var budget = turn.FindUnit(Hero)!.Budget;
        Assert.Equal((2, 0), (budget.MovementUsed, budget.ActionsUsed));
        Assert.Equal(Speed(turn, Hero) - 2, Turns.MovementLeft(Rules, turn, Hero));
    }

    [Fact]
    public void A_once_per_round_spend_survives_to_the_first_turn()
    {
        var r = Ok(Duel(HeroLater(Duel)), Use(Hero, TurnStrike, Two));
        Assert.Equal(1, Pools.Spent(r.State, Hero, "pool.turn"));
        var mark = r.State.FindUnit(Hero)!.Opener!;
        Assert.Equal<PoolSpend>([new PoolSpend("pool.turn", 1)], mark.TurnPools.ToArray());

        var turn = UntilTurnOf(r.State, Hero);
        Assert.Equal(1, Pools.Spent(turn, Hero, "pool.turn"));
        Assert.Equal(CommandError.NoUsesLeft, Pools.CheckSpend(Rules, turn, Hero, "pool.turn", 1));
        var budget = turn.FindUnit(Hero)!.Budget;
        Assert.Equal((1, false, 0), (budget.ActionsUsed, budget.BonusUsed, budget.MovementUsed));

        // The hero's round-2 turn refills it.
        var round2 = UntilTurnOf(Ok(turn, new EndTurnCommand(Hero)).State, Hero);
        Assert.Equal(2L, round2.Combat!.Round);
        Assert.Equal(0, Pools.Spent(round2, Hero, "pool.turn"));
    }

    [Fact]
    public void Once_per_turn_flags_clear_when_slot_zero_starts()
    {
        // The camp burst at (3,1) covers foe 2 and not the hero at (1,1).
        GameState Camp(ulong seed) => GameState.Create(Open, [P(1, 1, 1), F(2, 3, 1)], seed, hero: Hero);
        foreach (var seed in new[] { HeroFirst(Camp), HeroLater(Camp) })
        {
            var r = Ok(Camp(seed), new UseAbilityCommand(Hero, CampBurst, new PointTarget(new GridPos(3, 1))));
            var paid = r.Events.OfType<AbilityCostPaid>().Single();
            Assert.Equal<PoolSpend>([new PoolSpend(CampPool, 1)], paid.Pools.ToArray());

            // Slot 0 has started: every camp flag is clear, the camp pool stays spent, and the mark carries no camp.
            Assert.IsType<TurnStarted>(r.Events[^1]);
            Assert.False(r.State.FindUnit(Hero)!.Budget.CampSpent);
            Assert.Equal(1, Pools.Spent(r.State, Hero, CampPool));
            Assert.Empty(r.State.FindUnit(Hero)!.Opener?.TurnPools ?? []);
            Assert.False(r.State.FindUnit(Hero)!.Opener?.Charged.CampSpent ?? false);

            var turn = UntilTurnOf(r.State, Hero);
            Assert.False(turn.FindUnit(Hero)!.Budget.CampSpent);
            Assert.Equal(CommandError.None, Pools.CheckSpend(Rules, turn, Hero, CampPool, 1));
        }
    }

    // --- Order and dice (tests 5 and 6) ---

    [Fact]
    public void Opener_messages_come_between_CombatStarted_and_the_first_turn()
    {
        var s = Duel(HeroLater(Duel));
        var r = Ok(s, Use(Hero, Sure, Two));

        Assert.Equal(
            [
                typeof(CombatStarted), typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(DamageRolled),
                typeof(DamageDealt), typeof(AbilityResolved), typeof(TurnStarted),
            ],
            Types(r));
        var order = Assert.IsType<CombatStarted>(r.Events[0]).Order;
        Assert.Equal(new TurnStarted(order[0].Unit, 1), r.Events[^1]);
        Assert.False(r.State.Combat!.BeforeRound1);
        Assert.Equal(order[0].Unit, r.State.Combat.CurrentUnit);
    }

    // Pinned from the first green run (a golden): the opener's natural d20 on seed 7 with three fighters.
    private const int Seed7OpenerD20 = 3;

    [Fact]
    public void Golden_N_initiative_dice_then_the_opener_roll()
    {
        var s = Trio(7);
        const int n = 3;
        var (_, begun) = Turns.BeginCombat(Rules, s);
        Assert.Equal(s.Rng.Combat.Counter + n, begun.Rng.Combat.Counter);
        var setup = Contests.PrepareAttack(Rules, begun, Hero, Two, new AttackSpec(Ability.Strength, true));
        var (expected, _) = Dice.D20(begun.Rng.Combat, setup.Mode);

        var r = Ok(s, Use(Hero, Strike, Two));

        var attack = r.Events.OfType<AttackRolled>().Single();
        Assert.Equal(expected, attack.Result.Roll);
        Assert.Equal(Seed7OpenerD20, attack.Result.Roll.First);
        Assert.Equal(
            Assert.IsType<CombatStarted>(r.Events[0]).Order.ToArray(),
            Assert.IsType<CombatStarted>(Turns.BeginCombat(Rules, s).Events[0]).Order.ToArray());
    }

    // --- Kills and wins (tests 7 and 8; Review Focus 1) ---

    [Fact]
    public void An_opener_that_kills_slot_zero_starts_the_next_unit()
    {
        GameState Make(ulong seed) =>
            GameState.Create(Open, [P(1, 1, 1), F(2, 4, 4, hp: 1), F(3, 5, 5)], seed, hero: Hero);
        var seed = SeedWhere(Make, o => o[0] == Two);
        var s = Give(Give(Make(seed), Two, Clumsy), Three, Clumsy);
        var order = Turns.BeginCombat(Rules, s).Next.Combat!.Order;

        var r = Ok(s, new UseAbilityCommand(Hero, SaveBurst, new PointTarget(new GridPos(5, 5))));

        Assert.Equal<UnitId>([Two, Three], r.Events.OfType<TargetsLocked>().Single().Targets.ToArray());
        Assert.Equal<UnitId>([Two, Three], r.Events.OfType<SaveRolled>().Select(e => e.Target).ToArray());
        int died = r.Events.IndexOf(new UnitDied(Two, new GridPos(4, 4)));
        int resolved = r.Events.IndexOf(new AbilityResolved(Hero, SaveBurst));
        Assert.True(died >= 0 && died < r.Events.IndexOf(r.Events.OfType<SaveRolled>().Last()));
        Assert.True(resolved >= 0);
        Assert.Equal(new TurnStarted(order[1], 1), r.Events[^1]);
        Assert.Equal(resolved + 1, r.Events.Length - 1);
        Assert.Single(r.Events.OfType<TurnStarted>());
        Assert.Equal(order[1], r.State.Combat!.CurrentUnit);
        Assert.False(r.State.Combat.TurnVacated);
        Assert.Equal(Snapshot.Save(r.State), Snapshot.Save(Loaded(r.State)));
    }

    [Fact]
    public void An_opener_that_wins_emits_no_turn_events()
    {
        var s = GameState.Create(Open, [P(1, 1, 1), F(2, 2, 1, hp: 1)], 7, hero: Hero) with { Tick = 4 };

        var r = Ok(s, Use(Hero, Sure, Two));

        Assert.Equal(
            [
                typeof(CombatStarted), typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(DamageRolled),
                typeof(DamageDealt), typeof(UnitDied), typeof(AbilityResolved), typeof(CombatEnded),
            ],
            Types(r));
        Assert.DoesNotContain(r.Events, e => e is TurnStarted or TurnEnded or TickAdvanced);
        Assert.Null(r.State.Combat);
        Assert.All(r.State.Units, u => Assert.Null(u.Opener));
        Assert.Equal(4, r.State.Tick);
    }

    // --- One per side, refusals, ticks (tests 10 to 12; owner ruling 2; Review Focus 3) ---

    [Fact]
    public void One_opener_per_side()
    {
        var r = Ok(Trio(SeedWhere(Trio, o => o[0] == Two)), Use(Hero, Sure, Two));
        var off = r.State;
        Assert.Equal(Two, off.Combat!.CurrentUnit);
        Assert.NotNull(off.FindUnit(Hero)!.Opener);

        // Off its turn the side's next hostile use is an ordinary in-combat use: refused, at no cost.
        var refused = Apply(off, Use(Three, Sure, Two));
        Assert.Equal(CommandError.NotYourTurn, refused.Error);
        Assert.Same(off, refused.State);
        Assert.Empty(refused.Events);

        // On its turn it is charged there, starts nothing and marks no one.
        var turn = UntilTurnOf(off, Three);
        var used = Ok(turn, Use(Three, Sure, Two));
        Assert.Equal(typeof(AbilityUsed), used.Events[0].GetType());
        Assert.DoesNotContain(used.Events, e => e is CombatStarted);
        Assert.Equal(1, used.State.FindUnit(Three)!.Budget.ActionsUsed);
        Assert.Null(used.State.FindUnit(Three)!.Opener);
        Assert.Equal(
            turn.Units.Select(u => u.Opener is not null).ToArray(), used.State.Units.Select(u => u.Opener is not null).ToArray());

        // The state check (Task 1): a second mark on the party side does not load.
        var action = new TurnBudget(0, 1, false, false, false, false);
        var twice = Edit(off, Three, u => u with { Budget = action, Opener = new OpenerCharge(action, []) });
        Loaded(off);
        Assert.Throws<SnapshotFormatException>(() => Loaded(twice));
    }

    [Fact]
    public void A_refused_opener_starts_no_fight()
    {
        var s = GameState.Create(Open, [P(1, 1, 1), F(2, 4, 1)], 7, hero: Hero) with { Tick = 3 };

        var r = Apply(s, Use(Hero, Sure, Two));

        Assert.Equal(CommandError.OutOfRange, r.Error);
        Assert.Same(s, r.State);
        Assert.Empty(r.Events);
        Assert.Null(r.State.Combat);
        Assert.Equal(3, r.State.Tick);
        Assert.Equal(s.Rng.Combat.Counter, r.State.Rng.Combat.Counter);
    }

    [Fact]
    public void The_opener_takes_no_tick()
    {
        var s = Trio(HeroLater(Trio));

        // The previous command, a non-opener on the companion, spends pool.turn and ticks.
        var before = Ok(s, Use(Hero, TurnStrike, Three)).State;
        Assert.Equal(s.Tick + 1, before.Tick);
        Assert.Equal(1, Pools.Spent(before, Hero, "pool.turn"));

        // Turn pools are not refilled at combat start, so the same pool cannot open.
        Assert.Equal(CommandError.NoUsesLeft, Apply(before, Use(Hero, TurnStrike, Two)).Error);

        var r = Ok(before, Use(Hero, Sure, Two));

        Assert.Equal(before.Tick, r.State.Tick);
        Assert.DoesNotContain(r.Events, e => e is TickAdvanced);
        Assert.Equal(new OpenerCharge(new TurnBudget(0, 1, false, false, false, false), []), r.State.FindUnit(Hero)!.Opener, OpenerComparer.Instance);
        Assert.Equal(1, Pools.Spent(r.State, Hero, "pool.turn"));
    }

    [Fact]
    public void Validated_against_a_fresh_budget()
    {
        var s = Trio(HeroLater(Trio));

        // The hero used its action last command (a non-opener on the companion); the stored budget keeps it.
        var before = Ok(s, Use(Hero, TurnStrike, Three)).State;
        Assert.Equal(1, before.FindUnit(Hero)!.Budget.ActionsUsed);

        // pool.fight was spent in an earlier fight: the step-4 refill makes it available to an opener only.
        before = Edit(before, Hero, u => u with { Spent = [new PoolSpend("pool.fight", 1), .. u.Spent] });
        Assert.Equal(CommandError.NoUsesLeft, AbilityUse.CheckTarget(Rules, before, Hero, FightStrike, new UnitsTarget([Three])));
        Assert.Equal(CommandError.None, AbilityUse.CheckTarget(Rules, before, Hero, FightStrike, new UnitsTarget([Two])));
        Assert.Equal(CommandError.None, AbilityUse.CheckUsable(Rules, before, Hero, FightStrike));
        var preview = AbilityUse.Preview(Rules, before, Hero, FightStrike, new UnitsTarget([Two]));
        Assert.Equal(CommandError.None, preview.Error);
        Assert.Equal<UnitId>([Two], preview.Affected.ToArray());

        var r = Ok(before, Use(Hero, FightStrike, Two));
        Assert.IsType<CombatStarted>(r.Events[0]);
        Assert.Equal(1, Pools.Spent(r.State, Hero, "pool.fight"));
        Assert.Equal(new TurnBudget(0, 1, false, false, false, false), r.State.FindUnit(Hero)!.Opener!.Charged);
        Assert.Empty(r.State.FindUnit(Hero)!.Opener!.TurnPools);
    }

    // --- Combat-start triggers (plan call 8) ---

    [Fact]
    public void CombatStarted_triggers_see_the_state_before_the_opener()
    {
        var s = Give(Duel(HeroLater(Duel)), Hero, "t.probe_on_start");

        var r = Ok(s, Use(Hero, Sure, Two));

        Assert.Equal(
            [
                typeof(CombatStarted), typeof(HookFired), typeof(Probed), typeof(AbilityUsed), typeof(AbilityCostPaid),
                typeof(TargetsLocked), typeof(DamageRolled), typeof(DamageDealt), typeof(AbilityResolved), typeof(TurnStarted),
            ],
            Types(r));
        Assert.Equal(new Probed(12, 0, true, false), r.Events[2]);
    }

    [Fact]
    public void A_combatStarted_hook_that_kills_the_hero_stops_the_opener()
    {
        var s = Give(Duel(HeroLater(Duel)), Hero, "t.kill_hero_on_start");

        var r = Ok(s, Use(Hero, Sure, Two));

        Assert.IsType<CombatStarted>(r.Events[0]);
        Assert.IsType<RunEnded>(r.Events[^1]);
        Assert.DoesNotContain(r.Events, e => e is AbilityUsed or AbilityCostPaid or TurnStarted);
        Assert.NotNull(r.State.Ended);
        Assert.All(r.State.Units, u => Assert.Null(u.Opener));
        Assert.Equal(12, r.State.FindUnit(Two)!.Hp);
    }

    [Fact]
    public void A_combatStarted_hook_that_removes_the_striker_drops_the_opener()
    {
        // Companion 3 strikes; the hook kills it before its opener resolves.
        var s = Give(Trio(7), Hero, "t.kill_three_on_start");
        var counter = Turns.BeginCombat(Rules, s).Next.Rng.Combat.Counter;

        var r = Ok(s, Use(Three, Sure, Two));

        Assert.Equal(
            [typeof(CombatStarted), typeof(HookFired), typeof(DamageDealt), typeof(UnitDied), typeof(TurnStarted)],
            Types(r));
        Assert.Null(r.State.FindUnit(Three));
        Assert.All(r.State.Units, u => Assert.Null(u.Opener));
        Assert.Equal(12, r.State.FindUnit(Two)!.Hp);
        Assert.Equal(counter, r.State.Rng.Combat.Counter);
        Assert.False(r.State.Combat!.BeforeRound1);
        Assert.Equal(s.Tick, r.State.Tick);
    }

    // --- MarkOpener enforces the load check (Task 1 review) ---

    [Fact]
    public void MarkOpener_refuses_what_the_load_check_refuses()
    {
        var held = Turns.BeginCombat(Rules, Duel(7)).Next;
        var paid = Edit(held, Hero, u => u with
        {
            Budget = new TurnBudget(0, 1, false, false, false, false),
            Spent = [new PoolSpend("pool.fight", 1), new PoolSpend("pool.turn", 1)],
        });
        var action = new TurnBudget(0, 1, false, false, false, false);

        Assert.Throws<ArgumentException>(() => Turns.MarkOpener(paid, Hero, new OpenerCharge(action, [new PoolSpend("pool.turn", 1), new PoolSpend("pool.fight", 1)])));
        Assert.Throws<ArgumentException>(() => Turns.MarkOpener(paid, Hero, new OpenerCharge(action, [new PoolSpend("pool.turn", 1), new PoolSpend("pool.turn", 1)])));
        Assert.Throws<ArgumentException>(() => Turns.MarkOpener(paid, Hero, new OpenerCharge(action, [new PoolSpend("Bad Id", 1)])));
        Assert.Throws<ArgumentException>(() => Turns.MarkOpener(paid, Hero, new OpenerCharge(action, [new PoolSpend("pool.turn", 0)])));
        Assert.Throws<ArgumentException>(() => Turns.MarkOpener(paid, Hero, new OpenerCharge(action with { MovementUsed = -1 }, [])));
        Assert.Throws<ArgumentException>(() => Turns.MarkOpener(paid, Hero, new OpenerCharge(action with { ActionsUsed = -1 }, [])));

        // A striker outside the turn order.
        var outside = paid with { Combat = paid.Combat! with { Order = [Two] } };
        Assert.Throws<InvalidOperationException>(() => Turns.MarkOpener(outside, Hero, new OpenerCharge(action, [])));

        var marked = Turns.MarkOpener(paid, Hero, new OpenerCharge(action, [new PoolSpend("pool.fight", 1), new PoolSpend("pool.turn", 1)]));

        // A saved state holds the marker only with a decision pending or the run ended; with the hero's slot later and
        // the marker cleared, the mark itself loads.
        var later = marked with { Combat = new CombatState([Two, Hero], 0, 1) };
        Assert.Equal(Snapshot.Save(later), Snapshot.Save(Loaded(later)));
    }

    // --- Helpers ---

    private sealed record Probed(int FoeHp, int StrikerActions, bool BeforeRound1, bool Marked) : GameEvent;

    private static GameState Probe(HookContext ctx)
    {
        var s = ctx.State;
        ctx.Emit(new Probed(
            s.FindUnit(Two)!.Hp, s.FindUnit(Hero)!.Budget.ActionsUsed, s.Combat!.BeforeRound1, s.Units.Any(u => u.Opener is not null)));
        return s;
    }

    private static GameState Kill(HookContext ctx)
    {
        var target = new UnitId(int.Parse(ctx.Args[0], CultureInfo.InvariantCulture));
        var part = new DamagePart(DamageType.Bludgeoning, 0, 0, 1000);
        var (harm, next) = Harm.Deal(ctx.Rules, ctx.State, target, new DamageRoll([new PartRoll(part, [], 1000)], false));
        foreach (var e in harm.Events)
        {
            ctx.Emit(e);
        }

        return next;
    }

    private static string Trait(string id, string trigger) =>
        $$"""{"id": "{{id}}", "kind": "trait", "triggers": [{{trigger}}]}""";

    // Opener charges compare their pool lists by contents.
    private sealed class OpenerComparer : IEqualityComparer<OpenerCharge?>
    {
        public static readonly OpenerComparer Instance = new();

        public bool Equals(OpenerCharge? x, OpenerCharge? y) =>
            x is null || y is null ? x is null && y is null : x.Charged == y.Charged && x.TurnPools.SequenceEqual(y.TurnPools);

        public int GetHashCode(OpenerCharge? obj) => 0;
    }
}
