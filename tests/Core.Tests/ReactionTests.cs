using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// Reactions and their automatic policies (M1-11 Task 7; plan calls 10, 11 and 13; AB 7.3, 14.3): the attack-roll and
/// own-save windows, legality before policy, the first that fires closes the window, the per-unit policy settings and
/// the reaction an opener triggers before round 1.
/// </summary>
public class ReactionTests
{
    private static readonly UnitId Hero = new(1);
    private static readonly UnitId Two = new(2);
    private static readonly UnitId Three = new(3);

    private const string WardA = "ward.a";
    private const string WardB = "ward.b";
    private const string HalfWard = "ward.half";
    private const string MeasurePool = "pool.measure";

    private static readonly AttackSpec StrikeSpec = new(Ability.Strength, true);

    private static RulesContext RulesWith(ImmutableArray<PoolDefinition> pools, params AbilityDefinition[] extra) =>
        RulesContext.Create(
            ContestKit.LoadEffects(
            [
                .. Enumerable.Range(-40, 81).Where(n => n != 0).SelectMany(n => new[]
                {
                    ContestKit.Def(Tune("armourClass", n), "gear", ContestKit.Add("armourClass", n)),
                    ContestKit.Def(Tune("reflex", n), "gear", ContestKit.Add("reflex", n)),
                }),
            ]),
            pools: pools,
            abilities: [.. Definitions, .. Wards, .. extra]);

    private static readonly RulesContext Rules = RulesWith(
        PoolTable,
        Reaction(WardA, ReactionTrigger.AttackRoll, null),
        Reaction(WardB, ReactionTrigger.AttackRoll, new PoolUse("pool.turn", 1)));

    private static string Tune(string number, int n) => $"rx.{number.ToLowerInvariant()}.{n}";

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......", ".......", ".......", ".......", ".......");

    private static ImmutableArray<string> Holding(params string[] ids) =>
        [.. ids.Concat([Strike]).Distinct().OrderBy(id => id, StringComparer.Ordinal)];

    // The hero (1,1) holding the given abilities, foe 2 (2,1) beside it; foe 2's turn of round 1.
    private static GameState Duel(ulong seed, params string[] held) =>
        InCombat(GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Holding(held) }, Foe(2, 2, 1)], seed, hero: Hero), 2, 1);

    private static StepResult Apply(GameState s, Command c, RulesContext? rules = null) => Engine.Apply(s, c, rules ?? Rules);

    private static StepResult Ok(GameState s, Command c, RulesContext? rules = null)
    {
        var r = Apply(s, c, rules);
        Assert.True(r.Ok, r.Error.ToString());
        return r;
    }

    private static UseAbilityCommand Attack(UnitId user, UnitId target) => new(user, Strike, new UnitsTarget([target]));

    private static Type[] Types(StepResult r) => r.Events.Select(e => e.GetType()).ToArray();

    private static ReactionTaken[] Taken(StepResult r) => r.Events.OfType<ReactionTaken>().ToArray();

    private static GameState Edit(GameState s, UnitId id, Func<Unit, Unit> change)
    {
        var u = s.FindUnit(id)!;
        return s with { Units = s.Units.SetItem(s.Units.IndexOf(u), change(u)) };
    }

    private static GameState Policy(GameState s, UnitId unit, string ability, ReactionMode mode) =>
        Edit(s, unit, u => u with { Reactions = [new ReactionSetting(ability, mode)] });

    private static GameState On(GameState s, UnitId unit, string effect) =>
        Effects.Apply(s, effect, unit, new EffectSource(unit));

    private static GameState Set(RulesContext rules, GameState s, UnitId unit, string number, long value)
    {
        long delta = value - Effects.Number(rules, s, unit, number);
        return delta == 0 ? s : On(s, unit, Tune(number, (int)delta));
    }

    private static int Peek(GameState s) => Dice.D20(s.Rng.Combat, RollMode.Normal).Roll.Kept;

    // The first seed whose next combat d20 satisfies the predicate.
    private static ulong SeedWhere(Func<ulong, GameState> make, Func<int, bool> wanted)
    {
        for (ulong seed = 1; seed < 2000; seed++)
        {
            if (wanted(Peek(make(seed))))
            {
                return seed;
            }
        }

        throw new InvalidOperationException("No seed gives the wanted roll.");
    }

    // Foe 2's strike on the hero, with the hero's armour class set so the roll's total is the target plus `over`.
    private static GameState AttackOver(GameState s, int over, RulesContext? rules = null)
    {
        rules ??= Rules;
        var setup = Contests.PrepareAttack(rules, s, Two, Hero, StrikeSpec);
        long total = Peek(s) + setup.Bonus;
        return Set(rules, s, Hero, "armourClass", total - over);
    }

    private static ulong Plain(Func<ulong, GameState> make) => SeedWhere(make, k => k is > 1 and < 20);

    private static long Ac(GameState s) => Effects.Number(Rules, s, Hero, "armourClass");

    // --- The attack-roll window ---

    [Fact]
    public void Always_fires_and_spends_the_reaction()
    {
        var s = Duel(7, ShieldWard);
        long ac = Ac(s);
        var r = Ok(s, Attack(Two, Hero));

        var types = Types(r);
        Assert.Equal(
            [typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(ReactionTaken), typeof(AttackRolled)],
            types.Take(5).ToArray());
        Assert.Equal(new ReactionTaken(Hero, ShieldWard), Taken(r).Single());
        Assert.Equal(ac + 2, r.Events.OfType<AttackRolled>().Single().Result.Target);
        Assert.True(r.State.FindUnit(Hero)!.Budget.ReactionUsed);
    }

    [Fact]
    public void Never_does_not_fire()
    {
        var s = Policy(Duel(7, ShieldWard), Hero, ShieldWard, ReactionMode.Never);
        long ac = Ac(s);
        var r = Ok(s, Attack(Two, Hero));

        Assert.Empty(Taken(r));
        Assert.Equal(ac, r.Events.OfType<AttackRolled>().Single().Result.Target);
        Assert.False(r.State.FindUnit(Hero)!.Budget.ReactionUsed);
    }

    [Theory]
    [InlineData(-1, false, false)]
    [InlineData(0, true, false)]
    [InlineData(2, false, true)]
    public void IfItChangesOutcome_fires_only_when_a_hit_becomes_a_miss(int over, bool fires, bool hit)
    {
        GameState Make(ulong seed) => Policy(Duel(seed, ShieldWard), Hero, ShieldWard, ReactionMode.IfItChangesOutcome);
        var s = AttackOver(Make(Plain(Make)), over);
        long ac = Ac(s);
        var r = Ok(s, Attack(Two, Hero));

        var rolled = r.Events.OfType<AttackRolled>().Single().Result;
        Assert.Equal(Peek(s), rolled.Roll.Kept);
        Assert.Equal(fires, Taken(r).Length == 1);
        Assert.Equal(fires, r.State.FindUnit(Hero)!.Budget.ReactionUsed);
        Assert.Equal(fires ? ac + 2 : ac, rolled.Target);
        Assert.Equal(hit, rolled.Hit);
    }

    [Fact]
    public void IfItChangesOutcome_does_not_fire_on_a_natural_20()
    {
        GameState Make(ulong seed) => Policy(Duel(seed, ShieldWard), Hero, ShieldWard, ReactionMode.IfItChangesOutcome);
        var s = AttackOver(Make(SeedWhere(Make, k => k == 20)), 0);
        var r = Ok(s, Attack(Two, Hero));

        var rolled = r.Events.OfType<AttackRolled>().Single().Result;
        Assert.Empty(Taken(r));
        Assert.True(rolled.Hit);
        Assert.True(rolled.Crit);
        Assert.False(r.State.FindUnit(Hero)!.Budget.ReactionUsed);
    }

    [Fact]
    public void The_preview_leaves_reactions_out()
    {
        // AB 8.3: the preview shows the odds with reactions off, even against an "always" ward; and it spends nothing.
        var s = Duel(7, ShieldWard);
        var p = AbilityUse.Preview(Rules, s, Two, Strike, new UnitsTarget([Hero]));

        Assert.Equal(CommandError.None, p.Error);
        Assert.Equal(Ac(s), p.Targets.Single().Setup!.Value.Target);
        Assert.False(s.FindUnit(Hero)!.Budget.ReactionUsed);
    }

    // --- The own-save window ---

    // Foe 2 (4,4) bursts on the hero (1,1), who holds the save ward; foe 2's turn of round 1.
    private static GameState SaveDuel(ulong seed) =>
        InCombat(GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Holding(SaveWard) }, Foe(2, 4, 4)], seed, hero: Hero), 2, 1);

    private static UseAbilityCommand Burst(UnitId user, GridPos at) => new(user, SaveBurst, new PointTarget(at));

    [Theory]
    [InlineData(-1, true, true)]
    [InlineData(-3, false, false)]
    [InlineData(0, false, true)]
    public void IfItChangesOutcome_on_a_save(int over, bool fires, bool success)
    {
        GameState Make(ulong seed) => Policy(SaveDuel(seed), Hero, SaveWard, ReactionMode.IfItChangesOutcome);
        var s = Make(Plain(Make));
        long dc = Effects.Number(Rules, s, Two, "abilityDifficulty");
        s = Set(Rules, s, Hero, "reflex", dc + over - Peek(s));
        long reflex = Effects.Number(Rules, s, Hero, "reflex");
        var r = Ok(s, Burst(Two, new GridPos(1, 1)));

        var saved = r.Events.OfType<SaveRolled>().Single().Result;
        Assert.Equal(fires, Taken(r).Length == 1);
        Assert.Equal(fires, r.State.FindUnit(Hero)!.Budget.ReactionUsed);
        Assert.Equal(success, saved.Success);
        Assert.Equal(saved.Roll.Kept + reflex + (fires ? 2 : 0), saved.Total);
        if (fires)
        {
            int taken = Array.FindIndex(r.Events.ToArray(), e => e is ReactionTaken);
            Assert.IsType<SaveRolled>(r.Events[taken + 1]);
        }
    }

    [Fact]
    public void No_own_save_ward_against_an_ally()
    {
        // Companion 3 (3,1) bursts on the hero (1,1), who holds an always save ward; foe 2 stands far off.
        var s = InCombat(
            GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Holding(SaveWard) }, Foe(2, 6, 6), Party(3, 3, 1)], 7, hero: Hero),
            3, 1, 2);
        var r = Ok(s, Burst(Three, new GridPos(1, 1)));

        Assert.Contains(r.Events, e => e is SaveRolled { Target.Value: 1 });
        Assert.Empty(Taken(r));
        Assert.False(r.State.FindUnit(Hero)!.Budget.ReactionUsed);
        Assert.Null(r.State.Pending);
    }

    // --- Pools and the policies that read them ---

    [Theory]
    [InlineData(3, 2, true)]
    [InlineData(3, 1, false)]
    [InlineData(4, 2, true)]
    [InlineData(4, 1, false)]
    [InlineData(1, 1, true)]
    public void WhilePoolAtLeastHalf_uses_ceil_half(int max, int remaining, bool fires)
    {
        var rules = RulesWith(
            [.. PoolTable, new PoolDefinition(MeasurePool, RefillTag.Fight, _ => max)],
            Reaction(HalfWard, ReactionTrigger.AttackRoll, new PoolUse(MeasurePool, 1), ReactionMode.WhilePoolAtLeastHalf));
        var s = Duel(7, HalfWard);
        if (max > remaining)
        {
            s = Edit(s, Hero, u => u with { Spent = [new PoolSpend(MeasurePool, max - remaining)] });
        }

        var r = Ok(s, Attack(Two, Hero), rules);

        Assert.Equal(fires, Taken(r).Length == 1);
        Assert.Equal(fires ? remaining - 1 : remaining, Pools.Current(rules, r.State, Hero, MeasurePool));
    }

    [Fact]
    public void A_camp_paid_reaction_sets_the_camp_flag()
    {
        // Foe 2 (2,1), foe 3 (1,2) and the hero (1,1); foe 2's turn, then foe 3's, then the hero's.
        var s = InCombat(
            GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Holding(CampWard) }, Foe(2, 2, 1), Foe(3, 1, 2)], 7, hero: Hero),
            2, 3, 1);
        var r = Ok(s, Attack(Two, Hero));

        Assert.Equal(new ReactionTaken(Hero, CampWard), Taken(r).Single());
        var hero = r.State.FindUnit(Hero)!;
        Assert.Equal(1, Pools.Spent(r.State, Hero, CampPool));
        Assert.True(hero.Budget.CampSpent);
        Assert.True(hero.Budget.ReactionUsed);

        // The flag clears at the next turn start, foe 3's; the reaction stays spent until the hero's own.
        var next = Ok(r.State, new EndTurnCommand(Two)).State;
        Assert.Equal(Three, next.Combat!.CurrentUnit);
        Assert.False(next.FindUnit(Hero)!.Budget.CampSpent);
        Assert.True(next.FindUnit(Hero)!.Budget.ReactionUsed);
        Assert.Equal(1, Pools.Spent(next, Hero, CampPool));
    }

    // --- Legality and the window ---

    public static TheoryData<string> Illegal => new() { "reactionUsed", "poolEmpty", "campSpent" };

    [Theory]
    [MemberData(nameof(Illegal))]
    public void An_illegal_reaction_is_skipped_and_not_spent(string why)
    {
        var s = why switch
        {
            "reactionUsed" => Edit(Duel(7, ShieldWard), Hero, u => u with { Budget = u.Budget with { ReactionUsed = true } }),
            "poolEmpty" => Edit(Duel(7, TurnWard), Hero, u => u with { Spent = [new PoolSpend("pool.turn", 1)] }),
            "campSpent" => Edit(Duel(7, CampWard), Hero, u => u with { Budget = u.Budget with { CampSpent = true } }),
            _ => throw new ArgumentOutOfRangeException(nameof(why)),
        };
        long ac = Ac(s);
        var before = s.FindUnit(Hero)!;
        var r = Ok(s, Attack(Two, Hero));

        Assert.Empty(Taken(r));
        Assert.Equal(ac, r.Events.OfType<AttackRolled>().Single().Result.Target);
        var after = r.State.FindUnit(Hero)!;
        Assert.Equal(before.Budget, after.Budget);
        Assert.Equal(before.Spent.ToArray(), after.Spent.ToArray());
    }

    [Fact]
    public void An_illegal_candidate_passes_the_window_to_the_next()
    {
        // The camp ward is first by id but the camp spend is used; the shield ward fires instead.
        var s = Edit(Duel(7, CampWard, ShieldWard), Hero, u => u with { Budget = u.Budget with { CampSpent = true } });
        var r = Ok(s, Attack(Two, Hero));

        Assert.Equal(new ReactionTaken(Hero, ShieldWard), Taken(r).Single());
        Assert.Equal(0, Pools.Spent(r.State, Hero, CampPool));
    }

    [Fact]
    public void The_first_that_fires_closes_the_window()
    {
        var s = Duel(7, WardA, WardB);
        long ac = Ac(s);
        var r = Ok(s, Attack(Two, Hero));

        Assert.Equal(new ReactionTaken(Hero, WardA), Taken(r).Single());
        Assert.Equal(ac + 2, r.Events.OfType<AttackRolled>().Single().Result.Target);
        Assert.Equal(0, Pools.Spent(r.State, Hero, "pool.turn"));
    }

    [Fact]
    public void A_reaction_resets_only_at_its_owners_turn_start()
    {
        // Foe 2 (2,1), foe 3 (1,2) and the hero (1,1); foe 2's turn, then foe 3's, then the hero's.
        var s = InCombat(
            GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Holding(ShieldWard) }, Foe(2, 2, 1), Foe(3, 1, 2)], 7, hero: Hero),
            2, 3, 1);
        var first = Ok(s, Attack(Two, Hero));
        Assert.Single(Taken(first));

        var third = Ok(first.State, new EndTurnCommand(Two)).State;
        Assert.True(third.FindUnit(Hero)!.Budget.ReactionUsed);
        var second = Ok(third, Attack(Three, Hero));
        Assert.Empty(Taken(second));

        var own = Ok(second.State, new EndTurnCommand(Three)).State;
        Assert.Equal(Hero, own.Combat!.CurrentUnit);
        Assert.False(own.FindUnit(Hero)!.Budget.ReactionUsed);
    }

    // --- The policy settings (plan call 13) ---

    [Fact]
    public void SetReactionPolicy_overrides_and_survives_save_and_load()
    {
        var s = Duel(7, ShieldWard, SaveWard);
        Assert.Equal(ReactionMode.Always, Reactions.Mode(Rules, s, Hero, ShieldWard));

        var set = Ok(s, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.Never));
        Assert.Empty(set.Events);
        Assert.Equal(ReactionMode.Never, Reactions.Mode(Rules, set.State, Hero, ShieldWard));
        Assert.Equal(ReactionMode.Always, Reactions.Mode(Rules, set.State, Hero, SaveWard));

        // Setting a value replaces it; settings stay sorted by ability id.
        var both = Ok(set.State, new SetReactionPolicyCommand(Hero, SaveWard, ReactionMode.IfItChangesOutcome)).State;
        both = Ok(both, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.IfItChangesOutcome)).State;
        both = Ok(both, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.Never)).State;
        Assert.Equal(
            [new ReactionSetting(SaveWard, ReactionMode.IfItChangesOutcome), new ReactionSetting(ShieldWard, ReactionMode.Never)],
            both.FindUnit(Hero)!.Reactions.ToArray());

        var loaded = Snapshot.Load(Snapshot.Save(both));
        Assert.Equal(Snapshot.Save(both), Snapshot.Save(loaded));
        Assert.Equal(both.FindUnit(Hero)!.Reactions.ToArray(), loaded.FindUnit(Hero)!.Reactions.ToArray());
        Assert.Equal(StateDigest.Of(both), StateDigest.Of(loaded));
        Assert.NotEqual(StateDigest.Of(s), StateDigest.Of(both));

        // The override holds after the load: the shield ward does not fire.
        Assert.Empty(Taken(Ok(loaded, Attack(Two, Hero))));

        // The log carries the command and replays it.
        var commands = new Command[]
        {
            new SetReactionPolicyCommand(Hero, SaveWard, ReactionMode.IfItChangesOutcome),
            new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.Never),
        };
        var log = new CommandLog(7, [.. commands]);
        var json = log.Save();
        Assert.Contains("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\",\"mode\":\"Never\"}", json);
        var reloaded = CommandLog.Load(json);
        Assert.Equal(json, reloaded.Save());
        Assert.Equal(commands, reloaded.Commands.ToArray());
        Assert.Equal(Snapshot.Save(both), Snapshot.Save(Replay.Run(s, reloaded.Commands, Rules)));
    }

    [Theory]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\"}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"mode\":\"Never\"}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"ability\":\"ward.shield\",\"mode\":\"Never\"}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\",\"mode\":\"never\"}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\",\"mode\":\"Never\",\"dir\":\"E\"}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\",\"mode\":\"Never\",\"targets\":[2]}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\",\"mode\":\"Never\",\"point\":{\"x\":1,\"y\":1}}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\",\"mode\":\"Never\",\"decision\":1}")]
    [InlineData("{\"type\":\"setReactionPolicy\",\"unit\":1,\"ability\":\"ward.shield\",\"mode\":\"Never\",\"option\":0}")]
    [InlineData("{\"type\":\"useAbility\",\"unit\":1,\"ability\":\"test.strike\",\"targets\":[2],\"mode\":\"Never\"}")]
    [InlineData("{\"type\":\"move\",\"unit\":1,\"dir\":\"E\",\"mode\":\"Never\"}")]
    [InlineData("{\"type\":\"answer\",\"decision\":1,\"option\":0,\"mode\":\"Never\"}")]
    [InlineData("{\"type\":\"endTurn\",\"unit\":1,\"mode\":\"Never\"}")]
    [InlineData("{\"type\":\"move\",\"unit\":1,\"dir\":\"E\",\"ability\":\"ward.shield\"}")]
    public void The_policy_log_tag_has_only_its_fields(string command) =>
        Assert.Throws<SnapshotFormatException>(() => CommandLog.Load("{\"version\":8,\"seed\":7,\"commands\":[" + command + "]}"));

    [Fact]
    public void AskMe_is_party_only()
    {
        var s = Edit(Duel(7, ShieldWard), Two, u => u with { Abilities = Holding(ShieldWard) });
        AssertRefused(s, new SetReactionPolicyCommand(Two, ShieldWard, ReactionMode.AskMe), CommandError.AskMeIsPartyOnly);

        var r = Ok(s, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.AskMe));
        Assert.Equal(ReactionMode.AskMe, Reactions.Mode(Rules, r.State, Hero, ShieldWard));

        // A foe may take any automatic policy.
        var foe = Ok(s, new SetReactionPolicyCommand(Two, ShieldWard, ReactionMode.Never));
        Assert.Equal(ReactionMode.Never, Reactions.Mode(Rules, foe.State, Two, ShieldWard));

        // A save holding "ask me" on a non-party unit does not load.
        var bad = Edit(s, Two, u => u with { Reactions = [new ReactionSetting(ShieldWard, ReactionMode.AskMe)] });
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(bad)));
    }

    [Fact]
    public void WhilePoolAtLeastHalf_needs_a_pool()
    {
        // The shield ward has no pool, so "while the pool is at least half" could never fire.
        var s = Duel(7, ShieldWard, TurnWard);
        AssertRefused(s, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.WhilePoolAtLeastHalf), CommandError.PolicyNeedsPool);

        // Control: a pooled ward takes it.
        var r = Ok(s, new SetReactionPolicyCommand(Hero, TurnWard, ReactionMode.WhilePoolAtLeastHalf));
        Assert.Equal(ReactionMode.WhilePoolAtLeastHalf, Reactions.Mode(Rules, r.State, Hero, TurnWard));
    }

    [Fact]
    public void NotAReaction()
    {
        var s = Duel(7, ShieldWard);
        AssertRefused(s, new SetReactionPolicyCommand(Hero, Strike, ReactionMode.Always), CommandError.NotAReaction);
        AssertRefused(s, new SetReactionPolicyCommand(Hero, SaveWard, ReactionMode.Always), CommandError.UnknownAbility);
        AssertRefused(s, new SetReactionPolicyCommand(Hero, "test.nothing", ReactionMode.Always), CommandError.UnknownAbility);
        AssertRefused(s, new SetReactionPolicyCommand(new UnitId(99), ShieldWard, ReactionMode.Always), CommandError.UnknownUnit);
    }

    [Fact]
    public void A_policy_change_takes_no_tick()
    {
        // Out of combat, a step would tick; the policy change does not, and touches no budget, pool or stream.
        var calm = GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Holding(ShieldWard) }, Foe(2, 5, 5)], 7, hero: Hero);
        calm = Edit(calm, Hero, u => u with { Budget = u.Budget with { ActionsUsed = 1, ReactionUsed = true } });
        var r = Ok(calm, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.Never));
        Assert.Empty(r.Events);
        Assert.Equal(calm.Tick, r.State.Tick);
        Assert.Equal(calm.Rng.Combat.Counter, r.State.Rng.Combat.Counter);
        Assert.Equal(calm.FindUnit(Hero)!.Budget, r.State.FindUnit(Hero)!.Budget);

        // In combat, on another unit's turn.
        var fight = Duel(7, ShieldWard);
        var f = Ok(fight, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.Never));
        Assert.Empty(f.Events);
        Assert.Equal(fight.Tick, f.State.Tick);
        Assert.Equal(fight.Combat, f.State.Combat);
        Assert.Equal(fight.FindUnit(Hero)!.Budget, f.State.FindUnit(Hero)!.Budget);
    }

    private static void AssertRefused(GameState s, Command c, CommandError e)
    {
        var r = Apply(s, c);
        Assert.Equal(e, r.Error);
        Assert.Empty(r.Events);
        Assert.Same(s, r.State);
    }

    // --- The opener (record test 9, the automatic half) ---

    [Fact]
    public void A_reaction_triggered_by_the_opener_resolves_before_round_1()
    {
        var s = GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Holding(ShieldWard) }, Foe(2, 2, 1)], 7, hero: Hero);
        long ac = Ac(s);
        var r = Ok(s, Attack(Two, Hero));

        var types = Types(r).ToList();
        int started = types.IndexOf(typeof(CombatStarted));
        int taken = types.IndexOf(typeof(ReactionTaken));
        int rolled = types.IndexOf(typeof(AttackRolled));
        int resolved = types.IndexOf(typeof(AbilityResolved));
        Assert.True(started == 0 && started < taken && taken < rolled && rolled < resolved, string.Join(", ", types.Select(t => t.Name)));
        Assert.Equal(typeof(TurnStarted), types[resolved + 1]);
        Assert.Equal(taken + 1, rolled);
        Assert.Equal(new ReactionTaken(Hero, ShieldWard), Taken(r).Single());
        Assert.Equal(ac + 2, r.Events.OfType<AttackRolled>().Single().Result.Target);
    }
}
