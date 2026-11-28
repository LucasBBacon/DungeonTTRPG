using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Dungeon.Sim.Tools;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

// These classes share their names with their namespaces, so name them explicitly inside this namespace.
using Damage = Dungeon.Core.Damage.Damage;
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The UseAbility pipeline (M1-11 Task 4; plan calls 2 to 5, 9 and 10): the reason codes in AB 14.3's order, the two
/// validation stages, attack and save resolution, area target locking, the camp rule, the out-of-combat paths without
/// a hostile, and Review Focus 1, 2, 3 and 5.
/// </summary>
public class UseAbilityTests
{
    private static readonly UnitId Hero = new(1);
    private static readonly UnitId Two = new(2);
    private static readonly UnitId Three = new(3);
    private static readonly UnitId Four = new(4);

    // A wall at (4,2): from (2,2), the square (6,2) is 4 away and out of sight.
    private static readonly GridMap Walled = GridMap.Parse(
        ".........",
        ".........",
        "....#....",
        ".........",
        ".........");

    private static readonly GridMap Open = GridMap.Parse(
        ".........", ".........", ".........", ".........", ".........", ".........", ".........", ".........", ".........");

    private static readonly GridPos Far = new(6, 2);

    // The hero (2,2) beside foe 2 (3,2), and foe 3 (6,2) behind the wall; the hero's turn of round 1.
    private static GameState Fight(ulong seed = 7) =>
        InCombat(GameState.Create(Walled, [Party(1, 2, 2), Foe(2, 3, 2), Foe(3, 6, 2)], seed, hero: Hero), 1, 2, 3);

    private static UseAbilityCommand Use(UnitId user, string ability, AbilityTarget target) => new(user, ability, target);

    private static UnitsTarget Units(params UnitId[] units) => new([.. units]);

    private static StepResult Apply(GameState s, Command c) => Engine.Apply(s, c, Rules);

    private static GameState Edit(GameState s, UnitId id, Func<Unit, Unit> change)
    {
        var u = s.FindUnit(id)!;
        return s with { Units = s.Units.SetItem(s.Units.IndexOf(u), change(u)) };
    }

    private static GameState Budget(GameState s, UnitId id, TurnBudget budget) => Edit(s, id, u => u with { Budget = budget });

    private static GameState Turn(GameState s, UnitId id) =>
        s with { Combat = s.Combat! with { Current = s.Combat.Order.IndexOf(id) } };

    private static GameState CampEmpty(GameState s, UnitId id) =>
        Edit(s, id, u => u with { Spent = [new PoolSpend(CampPool, 3)] });

    private static void AssertRefused(GameState s, Command c, CommandError e)
    {
        var r = Apply(s, c);
        Assert.Equal(e, r.Error);
        Assert.Empty(r.Events);
        Assert.Same(s, r.State);
        Assert.Equal(s.Tick, r.State.Tick);
        Assert.Equal(s.Rng.Combat.Counter, r.State.Rng.Combat.Counter);
    }

    private static Type[] Types(StepResult r) => r.Events.Select(e => e.GetType()).ToArray();

    // --- The reason codes (plan calls 3 and 4) ---

    // Each case passes every earlier check, fails its own, and fails as many later checks as the state allows.
    private static (GameState State, UseAbilityCommand Command) Case(CommandError code)
    {
        var all = new TurnBudget(5, 1, true, false, true, true);
        switch (code)
        {
            case CommandError.UnknownUnit:
                return (Budget(Turn(CampEmpty(Fight(), Hero), Two), Hero, all), Use(new UnitId(99), Strike, Units(Three)));
            case CommandError.UnknownAbility:
                return (Edit(Budget(Turn(Fight(), Two), Hero, all), Hero, u => u with { Abilities = [] }), Use(Hero, Strike, Units(Three)));
            case CommandError.ReactionOnly:
                return (Budget(Turn(Fight(), Two), Hero, all), Use(Hero, Ward, Units(Three)));
            case CommandError.NotYourTurn:
                return (Budget(Turn(CampEmpty(Fight(), Hero), Two), Hero, all), Use(Hero, CampBurst, new PointTarget(Far)));
            case CommandError.NoActionLeft:
                return (Budget(CampEmpty(Fight(), Hero), Hero, all), Use(Hero, CampBurst, new PointTarget(Far)));
            case CommandError.NoBonusActionLeft:
                return (Budget(Fight(), Hero, all), Use(Hero, Jab, Units(Three)));
            case CommandError.NoFreeInteraction:
                return (Budget(Fight(), Hero, all), Use(Hero, Poke, Units(Three)));
            case CommandError.NoMovementLeft:
                return (Budget(Fight(), Hero, all), Use(Hero, Dash, Units(Three)));
            case CommandError.NoUsesLeft:
                return (Budget(CampEmpty(Fight(), Hero), Hero, new TurnBudget(0, 0, false, false, false, true)), Use(Hero, CampBurst, new PointTarget(Far)));
            case CommandError.CampSpendUsed:
                return (Budget(Fight(), Hero, new TurnBudget(0, 0, false, false, false, true)), Use(Hero, CampBurst, new PointTarget(Far)));
            case CommandError.InvalidTarget:
                return (Fight(), Use(Hero, Strike, new PointTarget(Far)));
            case CommandError.OutOfRange:
                return (Fight(), Use(Hero, Strike, Units(Three)));
            case CommandError.NoLineOfSight:
                return (Fight(), Use(Hero, SaveBurst, new PointTarget(Far)));
            case CommandError.NotInCombat:
                // A drawing ability on an ally, out of combat: the last check (plan call 5, D-23).
                return (GameState.Create(Walled, [Party(1, 2, 2), Party(4, 2, 4)], 7, hero: Hero), Use(Hero, SaveBurst, new PointTarget(new GridPos(2, 4))));
            default:
                throw new ArgumentOutOfRangeException(nameof(code));
        }
    }

    /// <summary>Every code <see cref="AbilityUse.CheckUsable"/> or <see cref="AbilityUse.CheckTarget"/> can return, in AB 14.3's order.</summary>
    public static readonly CommandError[] Codes =
    [
        CommandError.UnknownUnit, CommandError.UnknownAbility, CommandError.ReactionOnly, CommandError.NotYourTurn,
        CommandError.NoActionLeft, CommandError.NoBonusActionLeft, CommandError.NoFreeInteraction, CommandError.NoMovementLeft,
        CommandError.NoUsesLeft, CommandError.CampSpendUsed, CommandError.InvalidTarget, CommandError.OutOfRange,
        CommandError.NoLineOfSight, CommandError.NotInCombat,
    ];

    public static TheoryData<CommandError> CodeData()
    {
        var data = new TheoryData<CommandError>();
        foreach (var code in Codes)
        {
            data.Add(code);
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(CodeData))]
    public void Each_reason_code_in_order(CommandError code)
    {
        var (s, c) = Case(code);
        Assert.Equal(code, AbilityUse.CheckTarget(Rules, s, c.User, c.AbilityId, c.Target));
        AssertRefused(s, c, code);
    }

    [Fact]
    public void No_action_and_out_of_range_reports_no_action()
    {
        var s = Budget(Fight(), Hero, new TurnBudget(0, 1, false, false, false, false));
        AssertRefused(s, Use(Hero, Strike, Units(Three)), CommandError.NoActionLeft);
    }

    [Fact]
    public void The_bar_stage_ignores_the_target()
    {
        var s = Fight();
        Assert.Equal(CommandError.None, AbilityUse.CheckUsable(Rules, s, Hero, Strike));
        Assert.Equal(CommandError.OutOfRange, AbilityUse.CheckTarget(Rules, s, Hero, Strike, Units(Three)));
        Assert.Equal(CommandError.None, AbilityUse.CheckTarget(Rules, s, Hero, Strike, Units(Two)));
    }

    [Fact]
    public void Every_validation_code_has_a_greyed_reason()
    {
        var content = ContentLoader.Load(ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")));
        foreach (var code in Codes)
        {
            var reason = content.Find<GreyedReason>(code.ToString());
            Assert.True(reason is not null, $"No greyed reason {code}.");
            Assert.Equal("ab", reason!.Doc);
            Assert.True(content.Find<StringEntry>(reason.StringKey) is not null, $"No string {reason.StringKey}.");
        }
    }

    [Fact]
    public void Target_shapes_are_checked()
    {
        var s = Fight();
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, Strike, Units()));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, Strike, Units(Two, Two)));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, Strike, Units(Hero)));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, Strike, Units(new UnitId(99))));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, Strike, new AimTarget(Direction.E)));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, SaveBurst, Units(Two)));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, SaveBurst, new PointTarget(new GridPos(4, 2))));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, SaveBurst, new PointTarget(new GridPos(-1, 2))));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, AtWillCone, new PointTarget(new GridPos(3, 2))));
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, SaveLine, new AimTarget((Direction)99)));
        Assert.Equal(CommandError.None, AbilityUse.CheckTarget(Rules, s, Hero, SaveLine, new AimTarget(Direction.E)));
    }

    [Fact]
    public void A_unit_cannot_use_an_ability_it_does_not_hold()
    {
        var s = Edit(Fight(), Hero, u => u with { Abilities = [Sure] });
        AssertRefused(s, Use(Hero, Strike, Units(Two)), CommandError.UnknownAbility);
        Assert.True(Apply(s, Use(Hero, Sure, Units(Two))).Ok);

        // Held, but not in the rules.
        var unknown = Edit(Fight(), Hero, u => u with { Abilities = ["test.nothing"] });
        AssertRefused(unknown, Use(Hero, "test.nothing", Units(Two)), CommandError.UnknownAbility);
    }

    [Fact]
    public void Unit_abilities_are_normalized_and_validated()
    {
        Assert.Empty(Fixtures.U(1, 0, 0).Abilities);
        Assert.Throws<ArgumentException>(() => GameState.Create(Open, [Fixtures.U(1, 0, 0) with { Abilities = ["b", "a"] }], 7));
        Assert.Throws<ArgumentException>(() => GameState.Create(Open, [Fixtures.U(1, 0, 0) with { Abilities = ["a", "a"] }], 7));
        Assert.Throws<ArgumentException>(() => GameState.Create(Open, [Fixtures.U(1, 0, 0) with { Abilities = ["Bad Id"] }], 7));
        var s = Fight();
        var loaded = Snapshot.Load(Snapshot.Save(s));
        Assert.Equal(AllIds.ToArray(), loaded.FindUnit(Hero)!.Abilities.ToArray());
        Assert.Equal(StateDigest.Of(s), StateDigest.Of(loaded));
        Assert.NotEqual(StateDigest.Of(s), StateDigest.Of(Edit(s, Hero, u => u with { Abilities = [Sure] })));
    }

    // --- Resolution ---

    [Fact]
    public void An_attack_ability_in_combat()
    {
        var s = With(Fight(), 2, Exposed);
        var spec = new AttackSpec(Ability.Strength, true);
        var setup = Contests.PrepareAttack(Rules, s, Hero, Two, spec);
        var (d20, stream) = Dice.D20(s.Rng.Combat, setup.Mode);
        var attack = Contests.ResolveAttack(setup, d20);
        Assert.True(attack.Hit, "The fixture seed should hit.");
        var (roll, rolled) = Damage.Roll(s with { Rng = s.Rng with { Combat = stream } }, Rules.Ability(Strike).Damage, attack.Crit);

        var r = Apply(s, Use(Hero, Strike, Units(Two)));

        Assert.True(r.Ok);
        Assert.Equal(
            [typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(AttackRolled), typeof(DamageRolled), typeof(DamageDealt), typeof(AbilityResolved)],
            Types(r));
        Assert.Equal(new AbilityUsed(Hero, Strike), r.Events[0]);
        var paid = Assert.IsType<AbilityCostPaid>(r.Events[1]);
        Assert.Equal((Hero, Strike, new TurnBudget(0, 1, false, false, false, false)), (paid.User, paid.AbilityId, paid.Charged));
        Assert.Empty(paid.Pools);
        Assert.Equal<UnitId>([Two], Assert.IsType<TargetsLocked>(r.Events[2]).Targets.ToArray());
        Assert.Equal(new AttackRolled(Hero, Two, attack), r.Events[3]);
        AssertSameRoll(roll, Assert.IsType<DamageRolled>(r.Events[4]), Two);
        var dealt = Assert.IsType<DamageDealt>(r.Events[5]);
        Assert.Equal(roll.Parts[0].Raw, dealt.Total);
        Assert.Equal(new AbilityResolved(Hero, Strike), r.Events[6]);

        Assert.Equal(1, r.State.FindUnit(Hero)!.Budget.ActionsUsed);
        Assert.Equal(rolled.Rng.Combat.Counter, r.State.Rng.Combat.Counter);
        Assert.Equal(s.Tick, r.State.Tick);
    }

    [Fact]
    public void A_save_area_locks_targets_in_unit_order()
    {
        // Row order is 3 (4,4), 4 (5,5), 2 (6,6); lock order is by id.
        var s = InCombat(GameState.Create(Open, [Party(1, 1, 1), Foe(2, 6, 6), Foe(3, 4, 4), Foe(4, 5, 5)], 7, hero: Hero), 1, 2, 3, 4);
        s = With(With(With(s, 2, Nimble), 3, Nimble), 4, Nimble);
        long dc = Effects.Number(Rules, s, Hero, "abilityDifficulty");

        var r = Apply(s, Use(Hero, SaveBurst, new PointTarget(new GridPos(5, 5))));

        Assert.True(r.Ok);
        Assert.Equal(
            [typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(SaveRolled), typeof(SaveRolled), typeof(SaveRolled), typeof(AbilityResolved)],
            Types(r));
        Assert.Equal<UnitId>([Two, Three, Four], Assert.IsType<TargetsLocked>(r.Events[2]).Targets.ToArray());
        var saves = r.Events.OfType<SaveRolled>().ToArray();
        Assert.Equal<UnitId>([Two, Three, Four], saves.Select(x => x.Target).ToArray());
        Assert.All(saves, x => Assert.True(x.Result.Success));
        Assert.All(saves, x => Assert.Equal(dc, x.Result.Target));
        Assert.Equal(s.Rng.Combat.Counter + 3, r.State.Rng.Combat.Counter);
    }

    [Fact]
    public void Selective_and_at_will_areas_spare_allies()
    {
        // Ally 2 at (2,2) and foe 3 at (3,3): both in the SE cone from (1,1) and in a burst at (2,2).
        var s = InCombat(GameState.Create(Open, [Party(1, 1, 1), Party(2, 2, 2), Foe(3, 3, 3)], 7, hero: Hero), 1, 2, 3);
        var burst = new PointTarget(new GridPos(2, 2));

        Assert.Equal<UnitId>([Two, Three], AbilityUse.Affected(Rules, s, Hero, Rules.Ability(SaveBurst), burst).Units.ToArray());
        Assert.Equal<UnitId>([Three], AbilityUse.Affected(Rules, s, Hero, Rules.Ability(SelectiveBurst), burst).Units.ToArray());
        Assert.Equal<UnitId>([Three], AbilityUse.Affected(Rules, s, Hero, Rules.Ability(AtWillCone), new AimTarget(Direction.SE)).Units.ToArray());

        var r = Apply(s, Use(Hero, SelectiveBurst, burst));
        Assert.True(r.Ok);
        Assert.Equal<UnitId>([Three], r.Events.OfType<TargetsLocked>().Single().Targets.ToArray());
    }

    [Fact]
    public void The_user_is_never_in_its_own_area()
    {
        var s = InCombat(GameState.Create(Open, [Party(1, 1, 1), Foe(2, 2, 2)], 7, hero: Hero), 1, 2);
        var (squares, units) = AbilityUse.Affected(Rules, s, Hero, Rules.Ability(SaveBurst), new PointTarget(new GridPos(1, 1)));

        Assert.Contains(new GridPos(1, 1), squares);
        Assert.Equal<UnitId>([Two], units.ToArray());
        Assert.Equal(CommandError.InvalidTarget, AbilityUse.CheckTarget(Rules, s, Hero, Strike, Units(Hero)));
    }

    [Fact]
    public void A_second_camp_spend_in_the_same_turn_is_refused()
    {
        var s = Fight();
        // A camp-paid reaction taken earlier in this turn.
        var (ok, spent) = Pools.Spend(Rules, s, Hero, CampPool, 1);
        Assert.True(ok);
        var use = Use(Hero, CampBurst, new PointTarget(new GridPos(3, 2)));
        Assert.Equal(CommandError.None, AbilityUse.CheckUsable(Rules, s, Hero, CampBurst));

        AssertRefused(spent, use, CommandError.CampSpendUsed);

        var next = spent;
        for (int i = 0; i < 3; i++)
        {
            next = Turns.Advance(Rules, next).Next;
        }

        Assert.Equal(Hero, next.Combat!.CurrentUnit);
        var r = Apply(next, use);
        Assert.True(r.Ok);
        Assert.Equal(2, Pools.Spent(r.State, Hero, CampPool));
        Assert.True(r.State.FindUnit(Hero)!.Budget.CampSpent);
    }

    [Fact]
    public void A_party_area_out_of_combat_with_no_hostile_starts_nothing()
    {
        // The hero's stored budget is spent; the tick resets it before validation.
        var s = GameState.Create(Open, [Party(1, 1, 1), Party(2, 3, 1, hp: 1)], 7, hero: Hero);
        s = Budget(s, Hero, new TurnBudget(6, 1, true, true, true, true));

        var r = Apply(s, Use(Hero, CampBurst, new PointTarget(new GridPos(3, 1))));

        Assert.True(r.Ok, r.Error.ToString());
        Assert.Null(r.State.Combat);
        Assert.DoesNotContain(r.Events, e => e is CombatStarted);
        var clamp = Assert.Single(r.Events.OfType<DamageClamped>());
        Assert.Equal((Two, ClampReason.OutOfCombat), (clamp.Target, clamp.Reason));
        Assert.Equal(1, r.State.FindUnit(Two)!.Hp);
        Assert.Equal(s.Tick + 1, r.State.Tick);
        Assert.Equal(new TickAdvanced(s.Tick + 1), r.Events[^1]);
        Assert.Equal(s.Rng.Combat.Counter, r.State.Rng.Combat.Counter);
        Assert.Equal(new TurnBudget(0, 1, false, false, false, true), r.State.FindUnit(Hero)!.Budget);
    }

    [Fact]
    public void A_drawing_non_opener_out_of_combat_is_refused()
    {
        var s = GameState.Create(Open, [Party(1, 1, 1), Party(2, 2, 1)], 7, hero: Hero);
        AssertRefused(s, Use(Hero, SaveBurst, new PointTarget(new GridPos(3, 1))), CommandError.NotInCombat);
        AssertRefused(s, Use(Hero, Sure, Units(Two)), CommandError.NotInCombat);
    }

    // --- Review Focus 1, 2, 3 and 5 ---

    [Fact]
    public void An_area_kill_partway_through_still_resolves_the_rest()
    {
        var s = InCombat(GameState.Create(Open, [Party(1, 1, 1), Foe(2, 4, 4, hp: 1), Foe(3, 5, 5), Foe(4, 6, 6)], 7, hero: Hero), 1, 2, 3, 4);
        s = With(With(With(s, 2, Clumsy), 3, Clumsy), 4, Clumsy);

        var r = Apply(s, Use(Hero, SaveBurst, new PointTarget(new GridPos(5, 5))));

        Assert.True(r.Ok);
        Assert.Equal(new UnitDied(Two, new GridPos(4, 4)), r.Events.OfType<UnitDied>().Single());
        Assert.Equal<UnitId>([Two, Three, Four], r.Events.OfType<SaveRolled>().Select(x => x.Target).ToArray());
        Assert.Equal<UnitId>([Two, Three, Four], r.Events.OfType<DamageRolled>().Select(x => x.Target).ToArray());
        Assert.Equal(new AbilityResolved(Hero, SaveBurst), r.Events[^1]);
        Assert.Null(r.State.FindUnit(Two));
        Assert.True(r.State.CombatActive);
    }

    [Fact]
    public void The_hero_dying_mid_area_stops_resolution()
    {
        // An enemy's burst hits 1, then the hero (2, on 1 HP), then 3.
        var s = InCombat(
            GameState.Create(Open, [Party(1, 4, 4), Party(2, 5, 5, hp: 1), Party(3, 6, 6), Foe(4, 1, 1)], 7, hero: Two), 4, 1, 2, 3);
        s = With(With(With(s, 1, Clumsy), 2, Clumsy), 3, Clumsy);

        var r = Apply(s, Use(Four, SaveBurst, new PointTarget(new GridPos(5, 5))));

        Assert.True(r.Ok);
        Assert.Equal(
            [typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(SaveRolled), typeof(DamageRolled), typeof(DamageDealt),
             typeof(SaveRolled), typeof(DamageRolled), typeof(DamageDealt), typeof(RunEnded)],
            Types(r));
        Assert.Equal<UnitId>([Hero, Two, Three], r.Events.OfType<TargetsLocked>().Single().Targets.ToArray());
        Assert.DoesNotContain(r.Events, e => e is SaveRolled { Target.Value: 3 });
        Assert.NotNull(r.State.Ended);
        Assert.Equal(0, r.State.Combat!.Current);
        Assert.Equal(17, r.State.FindUnit(Three)!.Hp);
    }

    [Fact]
    public void Refusals_cost_nothing()
    {
        // In combat.
        AssertRefused(Fight(), Use(Hero, Strike, Units(Three)), CommandError.OutOfRange);

        // Out of combat, a non-opener: no tick resets, no tick.
        var s = GameState.Create(Open, [Party(1, 1, 1), Party(2, 3, 1)], 7, hero: Hero);
        s = Budget(s, Hero, new TurnBudget(6, 1, true, true, true, true)) with { Tick = 5 };
        AssertRefused(s, Use(Hero, SaveBurst, new PointTarget(new GridPos(3, 1))), CommandError.NotInCombat);
    }

    [Fact]
    public void Edge_and_wall_areas_resolve_no_target()
    {
        // A burst at the map corner covers the 4 in-bounds squares; only the hero stands there.
        var s = InCombat(GameState.Create(Open, [Party(1, 1, 1), Foe(2, 8, 8)], 7, hero: Hero), 1, 2);
        var corner = new PointTarget(new GridPos(0, 0));
        var (squares, units) = AbilityUse.Affected(Rules, s, Hero, Rules.Ability(SaveBurst), corner);
        Assert.Equal<GridPos>([new(0, 0), new(1, 0), new(0, 1), new(1, 1)], squares.ToArray());
        Assert.Empty(units);
        AssertNoTarget(s, Use(Hero, SaveBurst, corner), SaveBurst);

        // A line aimed straight into an adjacent wall covers nothing.
        var walled = InCombat(GameState.Create(Walled, [Party(1, 3, 2), Foe(2, 5, 2)], 7, hero: Hero), 1, 2);
        var aim = new AimTarget(Direction.E);
        Assert.Empty(AbilityUse.Affected(Rules, walled, Hero, Rules.Ability(SaveLine), aim).Squares);
        AssertNoTarget(walled, Use(Hero, SaveLine, aim), SaveLine);
    }

    private static void AssertNoTarget(GameState s, UseAbilityCommand c, string ability)
    {
        var r = Apply(s, c);
        Assert.True(r.Ok);
        Assert.Equal([typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(AbilityResolved)], Types(r));
        Assert.Empty(r.Events.OfType<TargetsLocked>().Single().Targets);
        Assert.Equal(new AbilityResolved(c.User, ability), r.Events[^1]);
        Assert.Equal(1, r.State.FindUnit(c.User)!.Budget.ActionsUsed);
        Assert.Equal(s.Rng.Combat.Counter, r.State.Rng.Combat.Counter);
    }

    // --- Persistence ---

    [Fact]
    public void The_use_replays()
    {
        var s = InCombat(GameState.Create(Open, [Party(1, 1, 1), Foe(2, 2, 1), Foe(3, 5, 5)], 7, hero: Hero), 1, 2, 3);
        Command[] commands =
        [
            Use(Hero, SaveBurst, new PointTarget(new GridPos(2, 1))),
            new EndTurnCommand(Hero),
            Use(Two, SaveLine, new AimTarget(Direction.W)),
            new EndTurnCommand(Two),
            new EndTurnCommand(Three),
            Use(Hero, Sure, Units(Two)),
        ];
        var live = s;
        foreach (var c in commands)
        {
            var r = Apply(live, c);
            Assert.True(r.Ok, r.Error.ToString());
            live = r.State;
        }

        var log = new CommandLog(7, [.. commands]);
        var loaded = CommandLog.Load(log.Save());
        Assert.Equal(log.Save(), loaded.Save());
        Assert.Equal(commands, loaded.Commands.ToArray(), CommandComparer.Instance);
        Assert.Equal(Snapshot.Save(live), Snapshot.Save(Replay.Run(s, loaded.Commands, Rules)));
    }

    [Fact]
    public void The_use_log_tag_has_its_fields()
    {
        var json = new CommandLog(7, [Use(Hero, Strike, Units(Two, Three)), Use(Hero, SaveBurst, new PointTarget(new GridPos(2, 3))), Use(Hero, SaveLine, new AimTarget(Direction.NE))]).Save();
        Assert.Contains("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.strike\",\"targets\":[2,3]}", json);
        Assert.Contains("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.save_burst\",\"point\":{\"x\":2,\"y\":3}}", json);
        Assert.Contains("{\"type\":\"useAbility\",\"unit\":1,\"dir\":\"NE\",\"ability\":\"test.line\"}", json);
    }

    [Theory]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.strike\"}")]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"targets\":[2]}")]
    [InlineData("{\"type\":\"useAbility\",\"ability\":\"test.strike\",\"targets\":[2]}")]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.strike\",\"targets\":[2],\"dir\":\"E\"}")]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.strike\",\"targets\":[2],\"point\":{\"x\":1,\"y\":1}}")]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.strike\",\"targets\":[2],\"option\":0}")]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.strike\",\"targets\":[2],\"decision\":1}")]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.burst\",\"point\":{\"x\":1}}")]
    [InlineData("{\"type\":\"move\",\"unit\":1,\"dir\":\"E\",\"ability\":\"test.strike\"}")]
    [InlineData("{\"type\":\"move\",\"unit\":1,\"dir\":\"E\",\"targets\":[2]}")]
    [InlineData("{\"type\":\"answer\",\"decision\":1,\"option\":0,\"point\":{\"x\":1,\"y\":1}}")]
    [InlineData("{\"type\":\"endTurn\",\"unit\":1,\"ability\":\"test.strike\"}")]
    public void Each_log_tag_rejects_foreign_fields(string command) =>
        Assert.Throws<SnapshotFormatException>(() => CommandLog.Load("{\"version\":8,\"seed\":7,\"commands\":[" + command + "]}"));

    private static void AssertSameRoll(DamageRoll expected, DamageRolled actual, UnitId target)
    {
        Assert.Equal(target, actual.Target);
        Assert.Equal(expected.Crit, actual.Roll.Crit);
        Assert.Equal(expected.Parts.Length, actual.Roll.Parts.Length);
        for (int i = 0; i < expected.Parts.Length; i++)
        {
            Assert.Equal(expected.Parts[i].Part, actual.Roll.Parts[i].Part);
            Assert.Equal(expected.Parts[i].Raw, actual.Roll.Parts[i].Raw);
            Assert.Equal(expected.Parts[i].Faces.ToArray(), actual.Roll.Parts[i].Faces.ToArray());
        }
    }

    // Commands compare by value, with a use's unit list compared by contents.
    private sealed class CommandComparer : IEqualityComparer<Command>
    {
        public static readonly CommandComparer Instance = new();

        public bool Equals(Command? x, Command? y) => (x, y) switch
        {
            (UseAbilityCommand a, UseAbilityCommand b) =>
                a.User == b.User && a.AbilityId == b.AbilityId && (a.Target, b.Target) switch
                {
                    (UnitsTarget p, UnitsTarget q) => p.Units.SequenceEqual(q.Units),
                    var (p, q) => object.Equals(p, q),
                },
            _ => object.Equals(x, y),
        };

        public int GetHashCode(Command obj) => 0;
    }
}
