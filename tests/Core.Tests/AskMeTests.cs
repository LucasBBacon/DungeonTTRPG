using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// "Ask me" as a pending decision (M1-11 Task 8; plan call 12; Review Focus 4; M1-10 rulings record 5 step 8 and 6.3):
/// a reaction window that reaches an "ask me" candidate suspends the use on a <see cref="DecisionKind.Reaction"/>
/// decision, with the roll already drawn and the use held in <see cref="GameState.Resolution"/>; the answer fires or
/// declines the candidate, draws nothing before the window closes, and resolution continues from the frame.
/// </summary>
public class AskMeTests
{
    private static readonly UnitId Hero = new(1);
    private static readonly UnitId Two = new(2);
    private static readonly UnitId Three = new(3);

    private const string AskA = "ward.ask_a";
    private const string AskB = "ward.ask_b";
    private const string AskSave = "ward.ask_save";

    /// <summary>A two-target strike: single, range 1, up to 2 units, a Strength attack, 1d6 slashing.</summary>
    private const string Cleave = "test.cleave";

    /// <summary>Armour class +50: every attack misses but a natural 20.</summary>
    private const string Armoured = "test.armoured";

    private static readonly RulesContext Rules = RulesContext.Create(
        ContestKit.LoadEffects(
            ContestKit.Def(Clumsy, "condition", ContestKit.Add("reflex", -50)),
            ContestKit.Def(Exposed, "condition", ContestKit.Add("armourClass", -50)),
            ContestKit.Def(Armoured, "condition", ContestKit.Add("armourClass", 50))),
        pools: PoolTable,
        abilities:
        [
            .. Definitions,
            .. Wards,
            Reaction(AskA, ReactionTrigger.AttackRoll, null),
            Reaction(AskB, ReactionTrigger.AttackRoll, null),
            Reaction(AskSave, ReactionTrigger.OwnSave, null),
            new AbilityDefinition(
                AbilityFixtures.File, Cleave, AbilityKind.AtWill, new AbilityCost(CostKind.Action), null,
                new TargetSpec(TargetShape.Single, 1, 2, 0, false), new AttackResolve(new AttackSpec(Ability.Strength, true)),
                [new DamagePart(DamageType.Slashing, 1, 6, 0)], [], null),
        ]);

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......", ".......", ".......", ".......", ".......");

    private static ImmutableArray<string> Holding(params string[] ids) =>
        [.. ids.Concat(AllIds).Append(Cleave).Distinct().OrderBy(id => id, StringComparer.Ordinal)];

    private static ImmutableArray<ReactionSetting> Asking(params string[] ids) =>
        [.. ids.OrderBy(id => id, StringComparer.Ordinal).Select(id => new ReactionSetting(id, ReactionMode.AskMe))];

    // The hero (1,1) asking on the given wards, foe 2 (2,1) and companion 3 (1,2), both beside the hero and 3 beside foe 2.
    private static GameState Calm(ulong seed, params string[] asked) =>
        GameState.Create(
            Open,
            [
                Party(1, 1, 1) with { Abilities = Holding(asked), Reactions = Asking(asked) },
                Foe(2, 2, 1) with { Abilities = Holding() },
                Party(3, 1, 2) with { Abilities = Holding() },
            ],
            seed,
            hero: Hero);

    // As Calm, in round 1 with foe 2 holding the turn.
    private static GameState Fight(ulong seed, params string[] asked) => InCombat(Calm(seed, asked), 2, 1, 3);

    private static UseAbilityCommand CleaveBoth => new(Two, Cleave, new UnitsTarget([Hero, Three]));

    private static UseAbilityCommand StrikeHero => new(Two, Strike, new UnitsTarget([Hero]));

    private static StepResult Apply(GameState s, Command c) => Engine.Apply(s, c, Rules);

    private static StepResult Ok(GameState s, Command c)
    {
        var r = Apply(s, c);
        Assert.True(r.Ok, r.Error.ToString());
        return r;
    }

    private static AnswerDecisionCommand Answer(GameState s, bool take) => new(s.Pending!.Id, take ? 1 : 0);

    private static Type[] Types(StepResult r) => r.Events.Select(e => e.GetType()).ToArray();

    private static string Names(StepResult r) => string.Join(", ", r.Events.Select(e => e.GetType().Name));

    private static int Peek(GameState s) => Dice.D20(s.Rng.Combat, RollMode.Normal).Roll.Kept;

    private static GameState Edit(GameState s, UnitId id, Func<Unit, Unit> change)
    {
        var u = s.FindUnit(id)!;
        return s with { Units = s.Units.SetItem(s.Units.IndexOf(u), change(u)) };
    }

    private static GameState On(GameState s, UnitId unit, string effect) =>
        Dungeon.Core.Effects.Effects.Apply(s, effect, unit, new EffectSource(unit));

    private static long Ac(GameState s) => Dungeon.Core.Effects.Effects.Number(Rules, s, Hero, "armourClass");

    // The first seed whose state's next combat d20 satisfies the predicate.
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

    // Foe 2 cleaves the hero and companion 3, both exposed, with a first roll that is neither 1 nor 20.
    private static GameState ExposedFight(params string[] asked)
    {
        GameState Make(ulong seed) => On(On(Fight(seed, asked), Hero, Exposed), Three, Exposed);
        return Make(SeedWhere(Make, k => k is > 1 and < 20));
    }

    // --- Suspension ---

    [Fact]
    public void AskMe_suspends_with_the_roll_known()
    {
        var s = ExposedFight(AskA);
        int kept = Peek(s);
        var r = Ok(s, CleaveBoth);

        Assert.Equal(
            [typeof(AbilityUsed), typeof(AbilityCostPaid), typeof(TargetsLocked), typeof(DecisionRequested)],
            Types(r));
        Assert.Empty(r.Events.OfType<AttackRolled>());

        // The d20 is drawn: the counter has moved on by the one normal die.
        Assert.Equal(s.Rng.Combat.Counter + 1, r.State.Rng.Combat.Counter);

        var decision = r.Events.OfType<DecisionRequested>().Single().Decision;
        Assert.Same(decision, r.State.Pending);
        Assert.Equal(
            (new DecisionId(s.NextDecisionId), Hero, DecisionKind.Reaction, new GridPos(1, 1)),
            (decision.Id, decision.Unit, decision.Kind, decision.Source));
        Assert.Equal([0, 1], decision.Options.ToArray());

        var frame = Assert.IsType<AbilityFrame>(r.State.Resolution);
        Assert.Equal((Two, Cleave, 0, ReactionTrigger.AttackRoll, AskA, (long?)null), (frame.User, frame.AbilityId, frame.Index, frame.Window, frame.Candidate, frame.SaveDifficulty));
        Assert.Equal([Hero, Three], frame.Locked.ToArray());
        Assert.Equal([Hero, Three], Assert.IsType<UnitsTarget>(frame.Target).Units.ToArray());
        Assert.Equal(kept, frame.Roll.Kept);
        Assert.Equal(Contests.PrepareAttack(Rules, r.State, Two, Hero, new AttackSpec(Ability.Strength, true)), frame.Setup);

        // Nothing is spent yet: the cost is paid, the reaction is not.
        Assert.Equal(1, r.State.FindUnit(Two)!.Budget.ActionsUsed);
        Assert.False(r.State.FindUnit(Hero)!.Budget.ReactionUsed);
    }

    [Fact]
    public void Taking_fires_and_resolves_the_rest()
    {
        var s = ExposedFight(AskA);
        long ac = Ac(s);
        var asked = Ok(s, CleaveBoth).State;
        var frame = asked.Resolution!;
        var r = Ok(asked, Answer(asked, take: true));

        var types = Types(r).ToList();
        Assert.Equal(new DecisionResolved(asked.Pending!.Id, Hero, 1), r.Events[0]);
        Assert.Equal(new ReactionTaken(Hero, AskA), r.Events[1]);
        var rolls = r.Events.OfType<AttackRolled>().ToArray();
        Assert.Equal([Hero, Three], rolls.Select(a => a.Target).ToArray());
        Assert.Equal(typeof(AttackRolled), types[2]);
        Assert.Equal(ac + 2, rolls[0].Result.Target);
        Assert.Equal(frame.Roll, rolls[0].Result.Roll);
        Assert.True(rolls[0].Result.Hit, Names(r));
        Assert.Equal(typeof(DamageRolled), types[3]);
        Assert.Equal(Hero, ((DamageRolled)r.Events[3]).Target);
        Assert.True(types.IndexOf(typeof(DamageDealt)) < r.Events.IndexOf(rolls[1]), Names(r));
        Assert.Equal(new AbilityResolved(Two, Cleave), r.Events[^1]);

        Assert.Null(r.State.Pending);
        Assert.Null(r.State.Resolution);
        Assert.True(r.State.FindUnit(Hero)!.Budget.ReactionUsed);

        // The same use with the ward on its automatic "always" gives the same rolls, damage and HP.
        var auto = Ok(Edit(s, Hero, u => u with { Reactions = [] }), CleaveBoth);
        Assert.Equal(
            auto.Events.SkipWhile(e => e is not ReactionTaken).Select(e => e.GetType()),
            r.Events.Skip(1).Select(e => e.GetType()));
        Assert.Equal(auto.Events.OfType<AttackRolled>(), rolls);
        Assert.Equal(auto.Events.OfType<DamageDealt>(), r.Events.OfType<DamageDealt>());
        Assert.Equal(auto.State.Units.Select(u => u.Hp), r.State.Units.Select(u => u.Hp));
        Assert.Equal(auto.State.Rng.Combat.Counter, r.State.Rng.Combat.Counter);
    }

    [Fact]
    public void Declining_asks_the_next_candidate_then_resolves()
    {
        var s = ExposedFight(AskA, AskB);
        var asked = Ok(s, CleaveBoth).State;
        Assert.Equal(AskA, asked.Resolution!.Candidate);

        var declined = Ok(asked, Answer(asked, take: false));
        Assert.Equal(
            [typeof(DecisionResolved), typeof(DecisionRequested)],
            Types(declined));
        Assert.Equal(new DecisionResolved(asked.Pending!.Id, Hero, 0), declined.Events[0]);
        var next = declined.State.Pending!;
        Assert.Equal((new DecisionId(asked.Pending.Id.Value + 1), Hero, DecisionKind.Reaction), (next.Id, next.Unit, next.Kind));
        Assert.Equal(asked.Resolution with { Candidate = AskB }, declined.State.Resolution);
        Assert.False(declined.State.FindUnit(Hero)!.Budget.ReactionUsed);

        var taken = Ok(declined.State, Answer(declined.State, take: true));
        Assert.Equal(new DecisionResolved(next.Id, Hero, 1), taken.Events[0]);
        Assert.Equal(new ReactionTaken(Hero, AskB), taken.Events[1]);
        Assert.Equal(typeof(AttackRolled), taken.Events[2].GetType());
        Assert.Equal(new AbilityResolved(Two, Cleave), taken.Events[^1]);
        Assert.True(taken.State.FindUnit(Hero)!.Budget.ReactionUsed);

        // Declining the last candidate closes the window with no reaction.
        var none = Ok(declined.State, Answer(declined.State, take: false));
        Assert.Empty(none.Events.OfType<ReactionTaken>());
        Assert.Empty(none.Events.OfType<DecisionRequested>());
        Assert.Equal(typeof(AttackRolled), none.Events[1].GetType());
        Assert.Equal(new AbilityResolved(Two, Cleave), none.Events[^1]);
        Assert.False(none.State.FindUnit(Hero)!.Budget.ReactionUsed);
        Assert.Null(none.State.Resolution);
    }

    [Fact]
    public void A_later_automatic_ward_fires_after_a_decline()
    {
        // ward.ask_a asks; ward.shield (after it by id) is on "always" and fires once the hero declines.
        var s = ExposedFight(AskA);
        s = Edit(s, Hero, u => u with { Abilities = Holding(AskA, ShieldWard) });
        var asked = Ok(s, CleaveBoth).State;
        var r = Ok(asked, Answer(asked, take: false));
        Assert.Equal(new ReactionTaken(Hero, ShieldWard), r.Events[1]);
    }

    [Fact]
    public void The_answer_draws_nothing_before_the_window_closes()
    {
        // The hero is armoured, so with a roll below 20 the attack misses after the window and nothing more is drawn.
        GameState Make(ulong seed) => On(Fight(seed, AskA, AskB), Hero, Armoured);
        var s = Make(SeedWhere(Make, k => k is > 1 and < 20));
        var single = new UseAbilityCommand(Two, Cleave, new UnitsTarget([Hero]));
        var asked = Ok(s, single).State;
        ulong counter = asked.Rng.Combat.Counter;

        var declined = Ok(asked, Answer(asked, take: false)).State;
        Assert.Equal(asked.Rng, declined.Rng);

        var taken = Ok(declined, Answer(declined, take: true));
        Assert.False(taken.Events.OfType<AttackRolled>().Single().Result.Hit);
        Assert.Equal(asked.Resolution!.Roll, taken.Events.OfType<AttackRolled>().Single().Result.Roll);
        Assert.Equal(counter, taken.State.Rng.Combat.Counter);
        Assert.Equal(asked.Rng.Shrine, taken.State.Rng.Shrine);
    }

    [Fact]
    public void An_own_save_window_asks_and_carries_the_difficulty()
    {
        // Foe 2 bursts the hero and companion 3; the hero asks on its save ward.
        GameState Make(ulong seed) => On(On(Fight(seed, AskSave), Hero, Clumsy), Three, Clumsy);
        var s = Make(SeedWhere(Make, k => k is > 1 and < 20));
        var r = Ok(s, new UseAbilityCommand(Two, SaveBurst, new PointTarget(new GridPos(1, 1))));
        var frame = r.State.Resolution!;
        Assert.Equal((ReactionTrigger.OwnSave, AskSave), (frame.Window, frame.Candidate));
        Assert.Equal(Resolution.Difficulty(Rules, r.State, Two, Rules.Ability(SaveBurst)), frame.SaveDifficulty);
        Assert.Contains(Hero, frame.Locked);

        var taken = Ok(r.State, Answer(r.State, take: true));
        Assert.Equal(new ReactionTaken(Hero, AskSave), taken.Events[1]);
        var save = Assert.IsType<SaveRolled>(taken.Events[2]);
        Assert.Equal(frame.Setup.Bonus + 2, save.Result.Total - save.Result.Roll.Kept);
        Assert.Equal(new AbilityResolved(Two, SaveBurst), taken.Events[^1]);
    }

    // --- Record test 9, the ask-me half ---

    [Fact]
    public void A_suspended_opener_holds_round_1()
    {
        var s = Calm(7, AskA);
        var r = Ok(s, StrikeHero);

        Assert.Equal(typeof(CombatStarted), r.Events[0].GetType());
        Assert.Equal(typeof(DecisionRequested), r.Events[^1].GetType());
        Assert.Empty(r.Events.OfType<TurnStarted>());
        Assert.Empty(r.Events.OfType<AttackRolled>());
        var held = r.State;
        Assert.True(held.Combat!.BeforeRound1);
        Assert.Equal(DecisionKind.Reaction, held.Pending!.Kind);
        Assert.NotNull(held.FindUnit(Two)!.Opener);

        // While the decision is pending no unit may move or end a turn.
        var slot0 = held.Combat.Order[0];
        foreach (var command in new Command[] { new MoveCommand(Hero, Direction.W), new MoveCommand(slot0, Direction.S), new EndTurnCommand(slot0) })
        {
            var refused = Apply(held, command);
            Assert.Equal(CommandError.DecisionPending, refused.Error);
            Assert.Same(held, refused.State);
        }

        var answered = Ok(held, Answer(held, take: true));
        var types = Types(answered).ToList();
        Assert.Equal(typeof(DecisionResolved), types[0]);
        Assert.Equal(new ReactionTaken(Hero, AskA), answered.Events[1]);
        Assert.Equal(typeof(AttackRolled), types[2]);
        int resolved = types.IndexOf(typeof(AbilityResolved));
        Assert.True(resolved > 2, Names(answered));
        Assert.Equal(new TurnStarted(slot0, 1), answered.Events[resolved + 1]);
        Assert.Equal(answered.Events.Length, resolved + 2);
        Assert.False(answered.State.Combat!.BeforeRound1);
        Assert.Null(answered.State.Pending);
        Assert.Null(answered.State.Resolution);
    }

    [Fact]
    public void The_answer_takes_no_tick()
    {
        // An opener out of combat: neither the declaring command nor the answer ticks.
        var calm = Calm(7, AskA) with { Tick = 5 };
        var held = Ok(calm, StrikeHero).State;
        Assert.Equal(5, held.Tick);
        var answered = Ok(held, Answer(held, take: false));
        Assert.Equal(5, answered.State.Tick);
        Assert.Empty(answered.Events.OfType<TickAdvanced>());

        // In combat.
        var fight = ExposedFight(AskA) with { Tick = 9 };
        var asked = Ok(fight, CleaveBoth).State;
        var r = Ok(asked, Answer(asked, take: true));
        Assert.Equal(9, r.State.Tick);
        Assert.Empty(r.Events.OfType<TickAdvanced>());
    }

    // --- Save and load (Review Focus 4) ---

    [Fact]
    public void Save_and_load_across_a_pending_reaction()
    {
        // The hero sets "ask me" on its shield ward by command, so the whole run is in the log.
        var start = Edit(ExposedFight(), Hero, u => u with { Abilities = Holding(ShieldWard) });
        var set = Ok(start, new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.AskMe)).State;
        var asked = Ok(set, CleaveBoth).State;

        var json = Snapshot.Save(asked);
        Assert.Contains("\"resolution\":{", json);
        Assert.Contains("\"candidate\":\"ward.shield\"", json);
        Assert.Contains("\"kind\":\"Reaction\"", json);

        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal(StateDigest.Of(asked), StateDigest.Of(loaded));
        Assert.NotEqual(StateDigest.Of(asked), StateDigest.Of(asked with { Resolution = asked.Resolution! with { Index = 1 } }));

        var answer = Answer(asked, take: true);
        var fromOriginal = Ok(asked, answer).State;
        var fromLoaded = Ok(loaded, answer).State;
        Assert.Equal(Snapshot.Save(fromOriginal), Snapshot.Save(fromLoaded));

        var log = new CommandLog(start.RunSeed, [new SetReactionPolicyCommand(Hero, ShieldWard, ReactionMode.AskMe), CleaveBoth, answer]);
        var reloaded = CommandLog.Load(log.Save());
        Assert.Equal(Snapshot.Save(fromOriginal), Snapshot.Save(Replay.Run(start, reloaded.Commands, Rules)));
    }

    [Fact]
    public void Bad_frames_are_format_errors()
    {
        var asked = Ok(ExposedFight(AskA), CleaveBoth).State;
        var frame = asked.Resolution!;

        // The state as the rules leave it loads.
        Snapshot.Load(Snapshot.Save(asked));

        var bad = new (string Why, GameState State)[]
        {
            ("a frame without a reaction decision", asked with { Pending = null }),
            ("a reaction decision without a frame", asked with { Resolution = null }),
            ("a reactor whose reaction is used", Edit(asked, Hero, u => u with { Budget = u.Budget with { ReactionUsed = true } })),
            ("a candidate the reactor does not hold", asked with { Resolution = frame with { Candidate = AskB } }),
            ("a frame out of combat", asked with { Combat = null }),
            ("an index past the locked units", asked with { Resolution = frame with { Index = 2 } }),
            ("a decision unit that is not the reactor", asked with { Pending = asked.Pending! with { Unit = Three } }),
            ("a decision source off the reactor's square", asked with { Pending = asked.Pending! with { Source = new GridPos(0, 0) } }),
            ("options other than decline and take", asked with { Pending = asked.Pending! with { Options = [0, 1, 2] } }),
            ("a roll whose mode is not the setup's", asked with { Resolution = frame with { Roll = frame.Roll with { Mode = RollMode.Advantage, Second = 3 } } }),
            ("locked units out of order", asked with { Resolution = frame with { Locked = [Three, Hero], Index = 1 } }),
        };
        foreach (var (why, state) in bad)
        {
            var json = Snapshot.Save(state);
            var ex = Record.Exception(() => Snapshot.Load(json));
            Assert.True(ex is SnapshotFormatException, $"{why}: {ex?.GetType().Name ?? "loaded"}");
        }

        // The marker set with a shrine decision pending, the unit standing on the shrine: only the marker rule refuses it.
        var shrine = GameState.Create(
            Open.With(new GridPos(1, 1), Tile.Shrine),
            [Party(1, 1, 1) with { Abilities = Holding() }, Foe(2, 2, 1) with { Abilities = Holding() }],
            7,
            hero: Hero);
        var pending = new PendingDecision(new DecisionId(1), Hero, DecisionKind.ShrineBoon, [0, 1, 2], new GridPos(1, 1));
        var plain = shrine with { Pending = pending, NextDecisionId = 2, Combat = new CombatState([Two, Hero], 0, 1) };
        Snapshot.Load(Snapshot.Save(plain));
        var marked = plain with { Combat = plain.Combat! with { BeforeRound1 = true } };
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(marked)));
    }

    [Fact]
    public void A_v8_shrine_decision_still_loads()
    {
        var s = GameState.Create(
            GridMap.Parse(".*.", "..."), [Party(1, 0, 0) with { Abilities = Holding() }, Foe(2, 2, 1)], 7, hero: Hero);
        var r = Ok(s, new MoveCommand(Hero, Direction.E));
        Assert.Equal(DecisionKind.ShrineBoon, r.State.Pending!.Kind);
        Assert.Null(r.State.Resolution);

        var json = Snapshot.Save(r.State);
        Assert.StartsWith("{\"version\":8,", json);
        Assert.Contains("\"kind\":\"ShrineBoon\"", json);
        Assert.Contains("\"resolution\":null", json);
        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));

        var answered = Ok(loaded, new AnswerDecisionCommand(loaded.Pending!.Id, 0));
        Assert.Equal(typeof(DecisionResolved), answered.Events[0].GetType());
        Assert.Equal(Tile.Floor, answered.State.Map[new GridPos(1, 0)]);

        // A shrine decision is still held to the shrine rules.
        var off = r.State with { Pending = r.State.Pending with { Options = [0, 1] } };
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(off)));
    }
}
