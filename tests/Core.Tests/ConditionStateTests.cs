using System.Collections.Immutable;
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
/// Condition instance state (M1-12a Task 3; plan calls 1, 2 and 5 to 8; CO 5.2, 7.2, 8, 17.2): <see cref="Conditions.Insert"/>
/// stamps the clock fields; the phase clock, every instance field and the per-fight lost-turn count are saved, digested
/// and checked; a source's death ends its <c>sourceTied</c> conditions and keeps the rest.
/// </summary>
public class ConditionStateTests
{
    private static readonly UnitId P = new(1);
    private static readonly UnitId Q = new(2);
    private static readonly UnitId E = new(3);

    private static readonly DamageRoll Lethal =
        new([new PartRoll(new DamagePart(DamageType.Slashing, 0, 0, 30), [], 30)], false);

    // Party units 1 and 2 and an enemy 3 (table HP 12), out of combat, seed 7, phase 0.
    private static GameState Calm() =>
        GameState.Create(
            GridMap.Parse(".....", ".....", "....."),
            [
                new Unit(P, new GridPos(0, 1), Faction.Party, [], Fixtures.Stats, FullHp),
                new Unit(Q, new GridPos(2, 1), Faction.Party, [], Fixtures.Stats, FullHp),
                new Unit(E, new GridPos(4, 1), Faction.Enemy, [], EnemyStats, 12),
            ],
            7);

    // As Calm, in round 1 with the order [P, Q, E] and the given slot holding the turn, in the given step and phase.
    private static GameState Fight(int current = 0, ClockStep step = ClockStep.Act, long phase = 5) =>
        Calm() with { Combat = new CombatState([P, Q, E], current, 1), Step = step, Phase = phase };

    private static (ImmutableArray<GameEvent> Events, GameState Next) Insert(
        GameState s, string id, UnitId holder, UnitId? source, ConditionInsert? options = null) =>
        Conditions.Insert(Rules, s, id, holder, new EffectSource(source), options ?? ConditionInsert.None);

    private static EffectInstance Only(GameState s, string id) => Assert.Single(s.Effects, e => e.EffectId == id);

    private static EffectInstance Only(GameState s, string id, UnitId holder) =>
        Assert.Single(s.Effects, e => e.EffectId == id && e.Holder == holder);

    // --- Insert ---

    [Fact]
    public void Insert_stamps_the_phase_the_source_faction_the_terms_and_own_turn()
    {
        // P's own Act phase: a hard condition is an own-turn one.
        var (events, s) = Insert(Fight(current: 0), Stun, P, E);
        var stun = Only(s, Stun);
        var state = stun.Condition!;
        Assert.Equal(1, stun.Seq);
        Assert.Equal(new EffectSource(E), stun.Source);
        Assert.Equal((1, 5L, 0, (int?)null, (long?)null, (Faction?)Faction.Enemy, (long?)null, true),
            (state.Value, state.AppliedPhase, state.TurnsCounted, state.Charges, state.RecordedDifficulty, state.SourceFaction, state.SustainedBy, state.OwnTurn));
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(1)], state.Terms.ToArray());
        Assert.Equal<GameEvent>([new ConditionApplied(1, Stun, P, new EffectSource(E), 1, true)], events.ToArray());
        Assert.Equal(2, s.NextEffectSeq);

        // Not own-turn: another unit's turn, the wearer's turn-start step, a vacated turn, or a non-hard condition.
        Assert.False(Only(Insert(Fight(current: 1), Stun, P, E).Next, Stun).Condition!.OwnTurn);
        Assert.False(Only(Insert(Fight(step: ClockStep.TurnStart), Stun, P, E).Next, Stun).Condition!.OwnTurn);
        Assert.False(Only(Insert(Fight(step: ClockStep.TurnEnd), Stun, P, E).Next, Stun).Condition!.OwnTurn);
        var vacated = Fight() with { Combat = new CombatState([P, Q, E], 0, 1, TurnVacated: true) };
        Assert.False(Only(Insert(vacated, Stun, P, E).Next, Stun).Condition!.OwnTurn);
        Assert.False(Only(Insert(Fight(), Slow, P, E).Next, Slow).Condition!.OwnTurn);

        // The environment has no faction; a valued condition takes the given value; charges come from the term.
        var env = Only(Insert(Fight(), Burn, P, null, new ConditionInsert(Value: 4, RecordedDifficulty: 13)).Next, Burn).Condition!;
        Assert.Equal((4, (Faction?)null, (long?)13), (env.Value, env.SourceFaction, env.RecordedDifficulty));
        Assert.Equal(1, Only(Insert(Fight(), Token, P, P).Next, Token).Condition!.Charges);
    }

    [Fact]
    public void A_duration_override_becomes_the_terms_and_is_checked_against_the_targets_side()
    {
        var (_, s) = Insert(Fight(), Slow, P, E, new ConditionInsert(Duration: [new DurationTerm.Turns(2), new DurationTerm.UntilSourceTurnEnd()]));
        Assert.Equal<DurationTerm>(
            [new DurationTerm.Turns(2), new DurationTerm.UntilSourceTurnEnd()], Only(s, Slow).Condition!.Terms.ToArray());

        // Terms are kept in their canonical order whatever order the override gives.
        var (_, r) = Insert(Fight(), Slow, P, E, new ConditionInsert(Duration: [new DurationTerm.UntilSourceTurnEnd(), new DurationTerm.Turns(2)]));
        Assert.Equal(Only(s, Slow).Condition!.Terms.ToArray(), Only(r, Slow).Condition!.Terms.ToArray());

        // A minor lasts at most 3 turns on either side.
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Slow, P, E, new ConditionInsert(Duration: [new DurationTerm.Turns(4)])));

        // Hard save-ends with cap 2 is enemy-facing only: refused on a party unit, accepted on an enemy.
        ImmutableArray<DurationTerm> saveEnds = [new DurationTerm.SaveEnds(Save.Will, 2, 0)];
        var held = new ConditionInsert(Duration: saveEnds);
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Held, P, E, held));
        Assert.Equal(saveEnds.ToArray(), Only(Insert(Fight(), Held, E, P, held).Next, Held).Condition!.Terms.ToArray());

        // A party-facing hard trigger has cap 1.
        Assert.Throws<ArgumentException>(
            () => Insert(Fight(), Stun, P, E, new ConditionInsert(Duration: [new DurationTerm.TriggerEnd("unitMoved", 2)])));
        Insert(Fight(), Stun, P, E, new ConditionInsert(Duration: [new DurationTerm.TriggerEnd("unitMoved", 1)]));

        // An override naming an unknown event, an empty one, or a repeated term is refused.
        Assert.Throws<ArgumentException>(
            () => Insert(Fight(), Slow, P, E, new ConditionInsert(Duration: [new DurationTerm.TriggerEnd("test.nothing", 1)])));
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Slow, P, E, new ConditionInsert(Duration: ImmutableArray<DurationTerm>.Empty)));
        Assert.Throws<ArgumentException>(
            () => Insert(Fight(), Slow, P, E, new ConditionInsert(Duration: [new DurationTerm.Turns(1), new DurationTerm.Turns(1)])));
    }

    [Fact]
    public void With_no_fight_the_terms_are_the_out_of_combat_list()
    {
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(10)], Only(Insert(Calm(), Asleep, P, E).Next, Asleep).Condition!.Terms.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(2)], Only(Insert(Fight(), Asleep, P, E).Next, Asleep).Condition!.Terms.ToArray());

        // A record with no out-of-combat list uses its duration out of combat too.
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(3)], Only(Insert(Calm(), Slow, P, E).Next, Slow).Condition!.Terms.ToArray());
    }

    [Fact]
    public void Sustained_by_must_name_a_live_instance()
    {
        var s = Effects.Apply(Fight(), Concentrating, E, new EffectSource(E));
        long sustainer = Only(s, Concentrating).Seq;
        var (_, next) = Insert(s, Sustained, P, E, new ConditionInsert(SustainedBy: sustainer));
        Assert.Equal(sustainer, Only(next, Sustained).Condition!.SustainedBy);

        Assert.Throws<ArgumentException>(() => Insert(s, Sustained, P, E, new ConditionInsert(SustainedBy: 99)));
        Assert.Throws<ArgumentException>(
            () => Insert(Effects.Remove(s, sustainer), Sustained, P, E, new ConditionInsert(SustainedBy: sustainer)));
    }

    [Fact]
    public void Insert_refuses_what_it_cannot_place()
    {
        Assert.Throws<ArgumentException>(() => Insert(Fight(), "test.cond.none", P, E));
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Plain, P, E));
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Slow, new UnitId(9), E));
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Slow, P, new UnitId(9)));
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Burn, P, E, new ConditionInsert(Value: 6)));
        Assert.Throws<ArgumentException>(() => Insert(Fight(), Stun, P, E, new ConditionInsert(Value: 2)));

        // One instance per condition id per holder (6.1 handling is M1-12b's).
        var (_, s) = Insert(Fight(), Slow, P, E);
        Assert.Throws<InvalidOperationException>(() => Insert(s, Slow, P, Q));
        Insert(s, Slow, Q, E);
    }

    [Fact]
    public void A_second_core_grant_re_stamps_the_instance()
    {
        var (first, s) = Insert(Fight(phase: 5), Conditions.SteeledId, P, P);
        var steeled = Only(s, Conditions.SteeledId);
        Assert.Equal<GameEvent>([new ConditionApplied(steeled.Seq, Conditions.SteeledId, P, new EffectSource(P), 1, true)], first.ToArray());

        var (second, again) = Insert(s with { Phase = 9 }, Conditions.SteeledId, P, E);
        var restamped = Only(again, Conditions.SteeledId);
        Assert.Equal(steeled.Seq, restamped.Seq);
        Assert.Equal(9, restamped.Condition!.AppliedPhase);
        Assert.Equal(new EffectSource(P), restamped.Source);
        Assert.Equal(s.NextEffectSeq, again.NextEffectSeq);
        Assert.Equal<GameEvent>([new ConditionApplied(steeled.Seq, Conditions.SteeledId, P, new EffectSource(P), 1, false)], second.ToArray());

        var (_, u) = Insert(s, Conditions.UnyieldingId, P, P);
        var (re, _) = Insert(u, Conditions.UnyieldingId, P, P);
        Assert.False(((ConditionApplied)Assert.Single(re)).New);
    }

    // --- Ends ---

    [Fact]
    public void End_emits_the_end_cascades_to_sustained_instances_and_grants_steeled_after_a_hard_one()
    {
        var s = Effects.Apply(Fight(), Concentrating, E, new EffectSource(E));
        (_, s) = Insert(s, Combined, P, E);
        long combined = Only(s, Combined).Seq;
        (_, s) = Insert(s, Sustained, P, E, new ConditionInsert(SustainedBy: combined));
        (_, s) = Insert(s, Stun, Q, E, new ConditionInsert(SustainedBy: combined));
        long sustained = Only(s, Sustained).Seq;
        long stun = Only(s, Stun).Seq;

        var (events, next) = Conditions.End(Rules, s, combined, ConditionEndReason.Saved);
        long steeled = s.NextEffectSeq;
        Assert.Equal<GameEvent>(
            [
                new ConditionEnded(combined, Combined, P, ConditionEndReason.Saved),
                new ConditionEnded(sustained, Sustained, P, ConditionEndReason.Ended),
                new ConditionEnded(stun, Stun, Q, ConditionEndReason.Ended),
                new ConditionApplied(steeled, Conditions.SteeledId, Q, new EffectSource(Q), 1, true),
            ],
            events.ToArray());
        Assert.DoesNotContain(next.Effects, e => e.Seq == combined || e.Seq == sustained || e.Seq == stun);

        // An engineState condition grants Steeled too.
        var (_, t) = Insert(Fight(), Surprised, P, E);
        var (ended, _) = Conditions.End(Rules, t, Only(t, Surprised).Seq, ConditionEndReason.Expired);
        Assert.IsType<ConditionApplied>(ended[1]);

        Assert.Throws<ArgumentException>(() => Conditions.End(Rules, s, 99, ConditionEndReason.Expired));
        Assert.Throws<ArgumentException>(() => Conditions.End(Rules, s, Only(s, Concentrating).Seq, ConditionEndReason.Expired));
    }

    [Fact]
    public void An_own_turn_instance_that_is_not_hard_throws_when_read()
    {
        var (_, s) = Insert(Fight(), Slow, P, E);
        var slow = Only(s, Slow);
        var bad = slow with { Condition = slow.Condition! with { OwnTurn = true } };
        Assert.Throws<InvalidOperationException>(() => Conditions.Read(Rules, bad));
        var broken = s with { Effects = [bad] };
        Assert.Throws<InvalidOperationException>(() => Conditions.End(Rules, broken, bad.Seq, ConditionEndReason.Expired));

        // A condition part on an id that names no condition definition throws too.
        var carrier = Effects.Apply(Fight(), Plain, P, new EffectSource(P));
        var plain = Only(carrier, Plain);
        Assert.Throws<InvalidOperationException>(() => Conditions.Read(Rules, plain with { Condition = slow.Condition }));

        var (_, h) = Insert(Fight(), Stun, P, E);
        var (definition, state) = Conditions.Read(Rules, Only(h, Stun));
        Assert.Equal((Stun, true), (definition.Id, state.OwnTurn));
    }

    // --- Source death (plan call 8) ---

    [Fact]
    public void A_source_death_ends_its_source_tied_conditions_and_steeled_follows_a_hard_one()
    {
        var (_, s) = Insert(Fight(current: 1), Tied, P, E);
        long tied = Only(s, Tied).Seq;
        var at = s.FindUnit(E)!.Pos;

        var (result, next) = Harm.Deal(Rules, s, E, Lethal);

        long steeled = s.NextEffectSeq;
        Assert.Equal<GameEvent>(
            [
                new DamageDealt(1, E, 30, 0),
                new UnitDied(E, at),
                new ConditionEnded(tied, Tied, P, ConditionEndReason.SourceGone),
                new ConditionApplied(steeled, Conditions.SteeledId, P, new EffectSource(P), 1, true),
            ],
            result.Events.ToArray());
        Assert.Equal(Conditions.SteeledId, Assert.Single(next.Effects).EffectId);
    }

    [Fact]
    public void A_condition_that_is_not_source_tied_outlives_its_source_and_the_save_loads()
    {
        var (_, s) = Insert(Fight(current: 1), Slow, P, E);
        (_, s) = Insert(s, MarkEnd, Q, E);
        s = Effects.Apply(s, Plain, P, new EffectSource(E));

        var (result, next) = Harm.Deal(Rules, s, E, Lethal);

        Assert.Equal(2, result.Events.Length);
        var slow = Only(next, Slow);
        Assert.Equal((new EffectSource(E), (Faction?)Faction.Enemy), (slow.Source, slow.Condition!.SourceFaction));
        Assert.Single(next.Effects, e => e.EffectId == MarkEnd);
        Assert.DoesNotContain(next.Effects, e => e.EffectId == Plain);

        // The gone source loads, and the slow still folds.
        var json = Snapshot.Save(next);
        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal(Effects.Number(Rules, next, P, "speed"), Effects.Number(Rules, loaded, P, "speed"));

        // A non-condition effect from a gone source is still refused.
        var plain = new EffectInstance(next.NextEffectSeq, Plain, P, new EffectSource(E));
        var bad = Snapshot.Save(next with { Effects = next.Effects.Add(plain), NextEffectSeq = next.NextEffectSeq + 1 });
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(bad));
    }

    [Fact]
    public void A_condition_from_a_gone_source_ranks_with_the_environment_in_trigger_order()
    {
        // Watch on P from E (to die), then Watch on Q from P; a plain environment effect fires last as ever.
        var (_, s) = Insert(Fight(current: 1), Watch, P, E);
        (_, s) = Insert(s, Watch, Q, P);
        (_, s) = Harm.Deal(Rules, s, E, Lethal);
        long gone = Only(s, Watch, P).Seq;
        long live = Only(s, Watch, Q).Seq;
        Assert.True(gone < live);

        var (after, events) = TriggerDispatch.Run(Rules, s, [new UnitMoved(Q, new GridPos(2, 1), new GridPos(3, 1))]);

        // The live source (P) fires before the gone one, which ranks as the environment: Q's plain first, then P's.
        var applied = events.OfType<EffectApplied>().ToArray();
        Assert.Equal([Q, P], applied.Select(a => a.Holder).ToArray());
        Assert.Equal(2, after.Effects.Count(e => e.EffectId == Plain));
    }

    [Fact]
    public void A_dead_holders_conditions_go_with_no_event()
    {
        var (_, s) = Insert(Fight(current: 1), Stun, E, P);
        (_, s) = Insert(s, Slow, E, null);

        var (result, next) = Harm.Deal(Rules, s, E, Lethal);

        Assert.Equal<GameEvent>([new DamageDealt(1, E, 30, 0), new UnitDied(E, s.FindUnit(E)!.Pos)], result.Events.ToArray());
        Assert.Empty(next.Effects);
    }

    // --- Persistence and the digest ---

    // Every instance field set away from its default, a phase and step, and a lost turn start.
    private static GameState Rich()
    {
        var s = Effects.Apply(Fight(phase: 7), Concentrating, E, new EffectSource(E));
        (_, s) = Insert(s, Stun, P, E);
        (_, s) = Insert(s, Token, Q, null, new ConditionInsert(SustainedBy: 1, RecordedDifficulty: 14));
        var token = Only(s, Token);
        var rich = token with
        {
            Condition = token.Condition! with
            {
                Value = 2,
                TurnsCounted = 1,
                Charges = 0,
                Terms = [new DurationTerm.Turns(1), new DurationTerm.SaveEnds(Save.Reflex, 2, 1), new DurationTerm.Charges(2, "unitMoved", new DurationTerm.TriggerEnd("turnEnded", null)), new DurationTerm.Staged(3, Save.Will, 3, Stun)],
            },
        };
        var p = s.FindUnit(P)!;
        return s with
        {
            Effects = s.Effects.SetItem(s.Effects.IndexOf(token), rich),
            Units = s.Units.SetItem(s.Units.IndexOf(p), p with { HardTurnsLostThisFight = 1 }),
        };
    }

    [Fact]
    public void The_phase_step_every_instance_field_and_lost_turns_round_trip_and_reach_the_digest()
    {
        var s = Rich();
        var json = Snapshot.Save(s);
        Assert.Contains("\"phase\":7,\"step\":\"Act\"", json);
        Assert.Contains("\"hardTurnsLostThisFight\":1", json);

        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal(StateDigest.Of(s), StateDigest.Of(loaded));
        Assert.Equal((7L, ClockStep.Act), (loaded.Phase, loaded.Step));
        Assert.Equal(1, loaded.FindUnit(P)!.HardTurnsLostThisFight);
        for (int i = 0; i < s.Effects.Length; i++)
        {
            var (a, b) = (s.Effects[i], loaded.Effects[i]);
            Assert.Equal((a.Seq, a.EffectId, a.Holder, a.Source), (b.Seq, b.EffectId, b.Holder, b.Source));
            Assert.Equal(a.Condition is null, b.Condition is null);
            if (a.Condition is { } c)
            {
                Assert.Equal(c with { Terms = [] }, b.Condition! with { Terms = [] });
                Assert.Equal(c.Terms.ToArray(), b.Condition!.Terms.ToArray());
            }
        }

        // Every field reaches the digest.
        var d = StateDigest.Of(s);
        var token = Only(s, Token);
        var c0 = token.Condition!;
        GameState With(ConditionState c) => s with { Effects = s.Effects.SetItem(s.Effects.IndexOf(token), token with { Condition = c }) };
        var p = s.FindUnit(P)!;
        var variants = new[]
        {
            s with { Phase = 8 },
            s with { Step = ClockStep.TurnEnd },
            s with { Units = s.Units.SetItem(s.Units.IndexOf(p), p with { HardTurnsLostThisFight = 2 }) },
            With(c0 with { Value = 3 }),
            With(c0 with { AppliedPhase = 6 }),
            With(c0 with { TurnsCounted = 2 }),
            With(c0 with { Terms = [new DurationTerm.Turns(1)] }),
            With(c0 with { Charges = 1 }),
            With(c0 with { Charges = null }),
            With(c0 with { RecordedDifficulty = 15 }),
            With(c0 with { SourceFaction = Faction.Party }),
            With(c0 with { SustainedBy = null }),
            With(c0 with { OwnTurn = true }),
            s with { Effects = s.Effects.SetItem(s.Effects.IndexOf(token), token with { Condition = null }) },
        };
        foreach (var v in variants)
        {
            Assert.NotEqual(d, StateDigest.Of(v));
        }
    }

    [Fact]
    public void Bad_clock_and_condition_values_are_format_errors()
    {
        var s = Rich();
        var json = Snapshot.Save(s);
        var bad = new (string Why, string From, string To)[]
        {
            ("a negative phase", "\"phase\":7", "\"phase\":-1"),
            ("an unknown step", "\"step\":\"Act\"", "\"step\":\"Lunch\""),
            ("a negative lost-turn count", "\"hardTurnsLostThisFight\":1", "\"hardTurnsLostThisFight\":-1"),
            ("a negative turn count", "\"turnsCounted\":1", "\"turnsCounted\":-1"),
            ("a negative charge count", "\"charges\":0", "\"charges\":-1"),
            ("a negative value", "\"value\":2", "\"value\":-2"),
            ("a phase after the clock", "\"appliedPhase\":7", "\"appliedPhase\":8"),
            ("a negative applied phase", "\"appliedPhase\":7", "\"appliedPhase\":-1"),
            ("an unknown term type", "{\"type\":\"turns\",\"n\":1}", "{\"type\":\"rounds\",\"n\":1}"),
            ("a term missing its count", "{\"type\":\"turns\",\"n\":1}", "{\"type\":\"turns\"}"),
            ("a term field its type does not use", "{\"type\":\"turns\",\"n\":1}", "{\"type\":\"turns\",\"n\":1,\"max\":2}"),
            ("unsorted terms", "[{\"type\":\"turns\",\"n\":1},{\"type\":\"saveEnds\",\"save\":\"Reflex\",\"cap\":2,\"graceTurns\":1}", "[{\"type\":\"saveEnds\",\"save\":\"Reflex\",\"cap\":2,\"graceTurns\":1},{\"type\":\"turns\",\"n\":1}"),
            ("a repeated term", "[{\"type\":\"turns\",\"n\":1}", "[{\"type\":\"turns\",\"n\":1},{\"type\":\"turns\",\"n\":1}"),
            ("a sustainer never issued", "\"sustainedBy\":1", "\"sustainedBy\":99"),
            ("an unknown source faction", "\"sourceFaction\":null", "\"sourceFaction\":\"Neutral\""),
        };
        foreach (var (why, from, to) in bad)
        {
            Assert.True(json.Contains(from, StringComparison.Ordinal), $"{why}: the save has no {from}");
            var ex = Record.Exception(() => Snapshot.Load(json.Replace(from, to)));
            Assert.True(ex is SnapshotFormatException, $"{why}: {ex?.GetType().Name ?? "loaded"}");
        }

        // Two instances with a condition part of one id on one holder.
        var stun = Only(s, Stun);
        var twice = s with
        {
            Effects = s.Effects.Add(stun with { Seq = s.NextEffectSeq }),
            NextEffectSeq = s.NextEffectSeq + 1,
        };
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(twice)));

        // A bare carrier of one id may still repeat.
        var carriers = Effects.Apply(Effects.Apply(Fight(), Plain, P, new EffectSource(P)), Plain, P, new EffectSource(P));
        Snapshot.Load(Snapshot.Save(carriers));
    }
}
