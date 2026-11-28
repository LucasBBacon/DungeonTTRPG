using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// The held events of a suspended use (M1-12a Task 1; plan call 12; review finding C1; Review Focus 4): while a use waits
/// on an "ask me" decision, the events that can fire triggers are held in <see cref="AbilityFrame.Held"/> and nothing is
/// dispatched; the command that completes the use dispatches the held events, then its own, so "ask me" ends where an
/// automatic policy does. The opener's combat-start phase is never held. Saves hold the held list (version 9).
/// </summary>
public class HeldEventsTests
{
    private static readonly UnitId A = new(1);
    private static readonly UnitId Two = new(2);
    private static readonly UnitId B = new(3);
    private static readonly UnitId Four = new(4);

    private static readonly GridMap Open = GridMap.Parse(".......", ".......", ".......", ".......", ".......", ".......", ".......");

    private static RulesContext Rules => HeldRules;

    private static ImmutableArray<string> Holding(params string[] ids) =>
        [.. ids.Concat(AllIds).Append(TwoStrike).Distinct().OrderBy(id => id, StringComparer.Ordinal)];

    private static ImmutableArray<ReactionSetting> Set(ReactionMode mode, params string[] ids) =>
        [.. ids.OrderBy(id => id, StringComparer.Ordinal).Select(id => new ReactionSetting(id, mode))];

    private static GameState On(GameState s, UnitId unit, string effect) =>
        Dungeon.Core.Effects.Effects.Apply(s, effect, unit, new EffectSource(unit));

    // A (1, party, 1 HP) at (1,1) and B (3, party) at (2,2), both exposed and beside foe 2 at (2,1); B holds the wards on
    // the given mode and Vengeful; the hero 4 stands far off. Out of combat.
    private static GameState Calm(ulong seed, ReactionMode mode, params string[] wards)
    {
        var s = GameState.Create(
            Open,
            [
                Party(1, 1, 1, hp: 1) with { Abilities = Holding() },
                Foe(2, 2, 1) with { Abilities = Holding() },
                Party(3, 2, 2) with { Abilities = Holding(wards), Reactions = Set(mode, wards) },
                Party(4, 5, 5) with { Abilities = Holding() },
            ],
            seed,
            hero: Four);
        return On(On(On(s, A, Exposed), B, Exposed), B, Vengeful);
    }

    // As Calm, in round 1 with foe 2 holding the turn.
    private static GameState Fight(ulong seed, ReactionMode mode, params string[] wards) =>
        InCombat(Calm(seed, mode, wards), 2, 1, 3, 4);

    private static UseAbilityCommand StrikeBoth => new(Two, TwoStrike, new UnitsTarget([A, B]));

    private static StepResult Ok(GameState s, Command c)
    {
        var r = Engine.Apply(s, c, Rules);
        Assert.True(r.Ok, r.Error.ToString());
        return r;
    }

    private static AnswerDecisionCommand Answer(GameState s, bool take) => new(s.Pending!.Id, take ? 1 : 0);

    private static string Names(StepResult r) => string.Join(", ", r.Events.Select(e => e.GetType().Name));

    // Under the automatic policy: A dies on the hit and B is hit.
    private static bool KillsAThenHitsB(StepResult r) =>
        r.Events.Contains(new UnitDied(A, new GridPos(1, 1)))
        && r.Events.OfType<AttackRolled>().Any(a => a.Target == B && a.Result.Hit);

    // The first seed under which the strike kills A and hits B with the ward on "never" (so with any ward policy).
    private static ulong Seed(Func<ulong, GameState> make)
    {
        for (ulong seed = 1; seed < 2000; seed++)
        {
            var r = Engine.Apply(make(seed), StrikeBoth, Rules);
            if (r.Ok && KillsAThenHitsB(r))
            {
                return seed;
            }
        }

        throw new InvalidOperationException("No seed kills A and hits B.");
    }

    private static readonly ulong FightSeed = Seed(seed => Fight(seed, ReactionMode.Never, AskWard));

    private static GameState AskFight(params string[] wards) => Fight(FightSeed, ReactionMode.AskMe, wards.Length == 0 ? [AskWard] : wards);

    private static int ResistsOnB(GameState s) => s.Effects.Count(e => e.EffectId == ResistSlashing && e.Holder == B);

    // The save with what the runs set up differently set aside: the ask consumes decision ids that the automatic run does
    // not, and B's wards and their policies are the runs' inputs. Units, effects, pools, HP and streams are all compared.
    private static string Outcome(GameState s)
    {
        var b = s.FindUnit(B)!;
        var units = s.Units.SetItem(s.Units.IndexOf(b), b with { Abilities = Holding(), Reactions = [] });
        return Snapshot.Save(s with { NextDecisionId = 1, Units = units });
    }

    // --- Review Focus 4 ---

    [Fact]
    public void Never_equals_ask_me_then_decline()
    {
        var never = Ok(Fight(FightSeed, ReactionMode.Never, AskWard), StrikeBoth);
        Assert.True(KillsAThenHitsB(never), Names(never));
        Assert.Equal(1, ResistsOnB(never.State));

        var asked = Ok(AskFight(), StrikeBoth).State;
        var declined = Ok(asked, Answer(asked, take: false));

        Assert.Equal(Outcome(never.State), Outcome(declined.State));
        Assert.Equal(never.State.FindUnit(B)!.Hp, declined.State.FindUnit(B)!.Hp);
        Assert.Equal(1, ResistsOnB(declined.State));
    }

    [Fact]
    public void Always_equals_ask_me_then_take()
    {
        var always = Ok(Fight(FightSeed, ReactionMode.Always, AskWard), StrikeBoth);
        Assert.Contains(new ReactionTaken(B, AskWard), always.Events);

        var asked = Ok(AskFight(), StrikeBoth).State;
        var taken = Ok(asked, Answer(asked, take: true));
        Assert.Contains(new ReactionTaken(B, AskWard), taken.Events);

        Assert.Equal(Outcome(always.State), Outcome(taken.State));
        Assert.Equal(1, ResistsOnB(taken.State));
    }

    // --- Dispatch ---

    [Fact]
    public void Nothing_dispatches_while_suspended()
    {
        var r = Ok(AskFight(), StrikeBoth);

        Assert.Contains(new UnitDied(A, new GridPos(1, 1)), r.Events);
        Assert.Empty(r.Events.OfType<HookFired>());
        Assert.Empty(r.Events.OfType<EffectApplied>());
        Assert.Equal(typeof(DecisionRequested), r.Events[^1].GetType());
        Assert.Equal(0, ResistsOnB(r.State));

        var frame = r.State.Resolution!;
        Assert.Equal([new UnitDied(A, new GridPos(1, 1))], frame.Held.ToArray());
    }

    [Fact]
    public void The_completing_command_dispatches_held_then_its_own()
    {
        var asked = Ok(AskFight(), StrikeBoth).State;
        long seq = asked.NextEffectSeq;
        var r = Ok(asked, Answer(asked, take: false));

        Assert.Equal(new DecisionResolved(asked.Pending!.Id, B, 0), r.Events[0]);
        Assert.Equal(typeof(AttackRolled), r.Events[1].GetType());
        Assert.DoesNotContain(r.Events, e => e is UnitDied);
        int resolved = r.Events.IndexOf(new AbilityResolved(Two, TwoStrike));
        Assert.True(resolved > 1, Names(r));
        Assert.Equal(new EffectApplied(seq, ResistSlashing, B, new EffectSource(B)), r.Events[resolved + 1]);

        // Nothing settles: the foe stands and the turn is still foe 2's.
        Assert.Equal(resolved + 2, r.Events.Length);
        Assert.Null(r.State.Resolution);
    }

    [Fact]
    public void A_re_ask_keeps_the_held_events()
    {
        var asked = Ok(AskFight(AskWard, AskWard2), StrikeBoth).State;
        var held = asked.Resolution!.Held;
        Assert.Equal([new UnitDied(A, new GridPos(1, 1))], held.ToArray());

        var again = Ok(asked, Answer(asked, take: false));
        Assert.Equal([typeof(DecisionResolved), typeof(DecisionRequested)], again.Events.Select(e => e.GetType()).ToArray());
        Assert.Equal(AskWard2, again.State.Resolution!.Candidate);
        Assert.Equal(held.ToArray(), again.State.Resolution.Held.ToArray());
        Assert.Equal(0, ResistsOnB(again.State));

        var done = Ok(again.State, Answer(again.State, take: false));
        Assert.Single(done.Events.OfType<EffectApplied>());
        Assert.Equal(1, ResistsOnB(done.State));

        // The same as with one ward declined.
        var once = Ok(AskFight(), StrikeBoth).State;
        Assert.Equal(Outcome(Ok(once, Answer(once, take: false)).State), Outcome(done.State));
    }

    [Fact]
    public void A_suspended_opener_dispatches_combat_start_at_once_and_holds_the_rest()
    {
        // Out of combat, foe 2's strike opens the fight. The hero carries Rally, a combatStarted trigger.
        GameState Make(ulong seed) => On(Calm(seed, ReactionMode.AskMe, AskWard), Four, Rally);
        ulong seed = 0;
        for (ulong k = 1; k < 2000 && seed == 0; k++)
        {
            var r0 = Engine.Apply(Make(k), StrikeBoth, Rules);
            if (r0.Ok && r0.State.Resolution is not null && r0.Events.Contains(new UnitDied(A, new GridPos(1, 1))))
            {
                var done = Engine.Apply(r0.State, Answer(r0.State, take: false), Rules);
                seed = done.Events.OfType<AttackRolled>().Any(a => a.Target == B && a.Result.Hit) ? k : 0;
            }
        }

        Assert.NotEqual(0UL, seed);
        var s = Make(seed);
        var r = Ok(s, StrikeBoth);

        Assert.Equal(typeof(CombatStarted), r.Events[0].GetType());
        var rallied = r.Events.OfType<EffectApplied>().Single();
        Assert.Equal((Rallied, Four), (rallied.EffectId, rallied.Holder));
        Assert.True(r.Events.IndexOf(rallied) < r.Events.IndexOf(new AbilityUsed(Two, TwoStrike)), Names(r));
        Assert.Contains(new UnitDied(A, new GridPos(1, 1)), r.Events);
        Assert.Equal(typeof(DecisionRequested), r.Events[^1].GetType());
        Assert.Equal(0, ResistsOnB(r.State));
        Assert.True(r.State.Combat!.BeforeRound1);
        Assert.Equal([new UnitDied(A, new GridPos(1, 1))], r.State.Resolution!.Held.ToArray());

        long seq = r.State.NextEffectSeq;
        var answered = Ok(r.State, Answer(r.State, take: false));
        int resolved = answered.Events.IndexOf(new AbilityResolved(Two, TwoStrike));
        Assert.True(resolved > 0, Names(answered));
        Assert.Equal(new EffectApplied(seq, ResistSlashing, B, new EffectSource(B)), answered.Events[resolved + 1]);
        Assert.Equal(new TurnStarted(answered.State.Combat!.Order[0], 1), answered.Events[resolved + 2]);
        Assert.Single(answered.Events.OfType<TurnStarted>());
        Assert.DoesNotContain(answered.Events, e => e is EffectApplied { EffectId: Rallied });
    }

    [Fact]
    public void A_held_last_foe_death_ends_the_fight_only_after_the_use()
    {
        // Foe 1 (1 HP) at (1,1); the hero 2 at (2,1) strikes foe 1 and its companion 3 at (1,2), which asks.
        GameState Make(ulong seed)
        {
            var s = GameState.Create(
                Open,
                [
                    Foe(1, 1, 1, hp: 1) with { Abilities = Holding() },
                    Party(2, 2, 1) with { Abilities = Holding() },
                    Party(3, 1, 2) with { Abilities = Holding(AskWard), Reactions = Set(ReactionMode.AskMe, AskWard) },
                ],
                seed,
                hero: Two);
            return InCombat(On(On(On(s, A, Exposed), B, Exposed), B, Vengeful), 2, 1, 3);
        }

        var strike = new UseAbilityCommand(Two, TwoStrike, new UnitsTarget([A, B]));
        ulong seed = 0;
        for (ulong k = 1; k < 2000 && seed == 0; k++)
        {
            var r0 = Engine.Apply(Make(k), strike, Rules);
            seed = r0.Ok && r0.State.Resolution is not null && r0.Events.Contains(new UnitDied(A, new GridPos(1, 1))) ? k : 0;
        }

        Assert.NotEqual(0UL, seed);
        var r = Ok(Make(seed), strike);
        Assert.Empty(r.Events.OfType<CombatEnded>());
        Assert.NotNull(r.State.Combat);
        Assert.Equal([new UnitDied(A, new GridPos(1, 1))], r.State.Resolution!.Held.ToArray());

        var answered = Ok(r.State, Answer(r.State, take: false));
        var events = answered.Events.ToList();
        int resolved = events.IndexOf(new AbilityResolved(Two, TwoStrike));
        int resisted = events.FindIndex(e => e is EffectApplied { EffectId: ResistSlashing });
        int ended = events.FindIndex(e => e is CombatEnded);
        Assert.True(resolved > 0 && resisted == resolved + 1 && ended > resisted, Names(answered));
        Assert.Null(answered.State.Combat);
        Assert.Empty(answered.Events.OfType<TurnStarted>());
    }

    // --- Save and load ---

    [Fact]
    public void Held_events_round_trip_and_reach_the_digest()
    {
        var start = AskFight();
        var asked = Ok(start, StrikeBoth).State;

        var json = Snapshot.Save(asked);
        Assert.StartsWith("{\"version\":9,", json);
        Assert.Contains("\"held\":[{\"type\":\"unitDied\",\"unit\":1,\"at\":{\"x\":1,\"y\":1}}]", json);

        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal(asked.Resolution!.Held.ToArray(), loaded.Resolution!.Held.ToArray());
        Assert.Equal(StateDigest.Of(asked), StateDigest.Of(loaded));

        var frame = asked.Resolution;
        Assert.NotEqual(StateDigest.Of(asked), StateDigest.Of(asked with { Resolution = frame with { Held = [] } }));
        Assert.NotEqual(
            StateDigest.Of(asked),
            StateDigest.Of(asked with { Resolution = frame with { Held = [new UnitDied(A, new GridPos(0, 1))] } }));

        // Every supported held event type round-trips, in order, and its order is data.
        ImmutableArray<GameEvent> all =
        [
            new UnitDied(A, new GridPos(1, 1)),
            new UnitMoved(Two, new GridPos(2, 1), new GridPos(3, 1)),
            new AbilityResolved(Two, TwoStrike),
        ];
        var many = asked with { Resolution = frame with { Held = all } };
        var manyJson = Snapshot.Save(many);
        var manyLoaded = Snapshot.Load(manyJson);
        Assert.Equal(all.ToArray(), manyLoaded.Resolution!.Held.ToArray());
        Assert.Equal(manyJson, Snapshot.Save(manyLoaded));
        Assert.Equal(StateDigest.Of(many), StateDigest.Of(manyLoaded));
        Assert.NotEqual(
            StateDigest.Of(many),
            StateDigest.Of(asked with { Resolution = frame with { Held = [all[1], all[0], all[2]] } }));

        // The answer on the loaded state, and a replay of the log, end where the live run does.
        var answer = Answer(asked, take: false);
        var live = Ok(asked, answer).State;
        Assert.Equal(Snapshot.Save(live), Snapshot.Save(Ok(loaded, answer).State));
        var log = CommandLog.Load(new CommandLog(start.RunSeed, [StrikeBoth, answer]).Save());
        Assert.Equal(Snapshot.Save(live), Snapshot.Save(Replay.Run(start, log.Commands, Rules)));
    }

    [Fact]
    public void A_v8_save_is_refused()
    {
        var asked = Ok(AskFight(), StrikeBoth).State;
        var v8 = Snapshot.Save(asked).Replace("{\"version\":9,", "{\"version\":8,");
        Assert.Equal(8, Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(v8)).Found);
    }

    [Fact]
    public void An_unsupported_held_event_is_a_format_error()
    {
        var asked = Ok(AskFight(), StrikeBoth).State;
        var frame = asked.Resolution!;
        var json = Snapshot.Save(asked);
        const string held = "\"held\":[{\"type\":\"unitDied\",\"unit\":1,\"at\":{\"x\":1,\"y\":1}}]";
        Assert.Contains(held, json);

        var bad = new (string Why, string Held)[]
        {
            ("an unknown type", "\"held\":[{\"type\":\"tileChanged\",\"unit\":1,\"at\":{\"x\":1,\"y\":1}}]"),
            ("a missing list", "\"held\":null"),
            ("a null entry", "\"held\":[null]"),
            ("a missing unit", "\"held\":[{\"type\":\"unitDied\",\"at\":{\"x\":1,\"y\":1}}]"),
            ("a missing square", "\"held\":[{\"type\":\"unitDied\",\"unit\":1}]"),
            ("a field the type does not use", "\"held\":[{\"type\":\"unitDied\",\"unit\":1,\"ability\":\"test.strike\",\"at\":{\"x\":1,\"y\":1}}]"),
            ("a move with no destination", "\"held\":[{\"type\":\"unitMoved\",\"unit\":1,\"from\":{\"x\":1,\"y\":1}}]"),
            ("an ability that is not a content id", "\"held\":[{\"type\":\"abilityResolved\",\"unit\":2,\"ability\":\"Not An Id\"}]"),
        };
        foreach (var (why, text) in bad)
        {
            var ex = Record.Exception(() => Snapshot.Load(json.Replace(held, text)));
            Assert.True(ex is SnapshotFormatException, $"{why}: {ex?.GetType().Name ?? "loaded"}");
        }

        // A type outside the closed set, or an uninitialised list, cannot be saved.
        Assert.Throws<InvalidOperationException>(
            () => Snapshot.Save(asked with { Resolution = frame with { Held = [new TileChanged(new GridPos(0, 0), Tile.Wall)] } }));
        Assert.Throws<InvalidOperationException>(() => Snapshot.Save(asked with { Resolution = frame with { Held = default } }));
    }
}
