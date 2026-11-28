using System;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Canonical;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core;

/// <summary>
/// The replay core-state hash (SV 4.5): the <see cref="Digest"/> of a <see cref="GameState"/>'s
/// canonical bytes. Units are a set keyed by id, so their stored order does not matter; boons and
/// decision options are lists, because their order is data. Active effects are a set keyed by seq. The initiative order,
/// a unit's spent counts, its opener mark's turn-pool counts, its abilities and its reaction settings are lists in stored
/// order.
/// Stream keys are derived from the run seed and are not written; a state whose keys disagree with its seed is refused.
/// </summary>
public static class StateDigest
{
    /// <summary>The digest of <paramref name="s"/>.</summary>
    /// <exception cref="ArgumentException">
    /// The streams do not belong to <see cref="GameState.RunSeed"/>.
    /// </exception>
    public static Digest Of(GameState s) => Digest.Of(Bytes(s));

    /// <summary>The canonical bytes <see cref="Of"/> hashes.</summary>
    public static byte[] Bytes(GameState s)
    {
        ArgumentNullException.ThrowIfNull(s);
        var expected = RunStreams.FromSeed(s.RunSeed);
        if (s.Rng.Combat.Key != expected.Combat.Key || s.Rng.Shrine.Key != expected.Shrine.Key)
        {
            throw new ArgumentException("The state's streams were not derived from its run seed.", nameof(s));
        }

        return CanonicalWriter.Serialize(new StateRecord(s));
    }

    // Maps every int id, negative ones included, to a ulong in the same order.
    private static ulong IdKey(UnitId id) => (ulong)((long)id.Value - int.MinValue);

    // Seqs are positive in every state the rules produce; any other is refused rather than wrapped.
    private static ulong EffectKey(EffectInstance e) =>
        e.Seq > 0 ? (ulong)e.Seq : throw new ArgumentException($"Effect seq {e.Seq} is not positive.", nameof(e));

    // Event sequence ids are positive in every state the rules produce; any other is refused rather than wrapped.
    private static ulong? EventKey(long? seq) => seq switch
    {
        null => null,
        > 0 => (ulong)seq.Value,
        _ => throw new ArgumentException($"Killing event id {seq} is not positive.", nameof(seq)),
    };

    private sealed class StateRecord(GameState s) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(15);
            w.UInt64(0, s.RunSeed);
            w.List(1, s.Map.ToRows(), (e, row) => e.String(0, row));
            w.SetByKey(2, s.Units, u => IdKey(u.Id), (e, u) => e.Record(0, new UnitRecord(u)));
            w.Record(3, new StreamsRecord(s.Rng));
            w.OptionalRecord(4, s.Pending is null ? null : new PendingRecord(s.Pending));
            w.Int64(5, s.Tick);
            w.Int32(6, s.NextDecisionId);
            w.Record(7, s.Config);
            w.Record(8, s.Unlocks);
            w.SetByKey(9, s.Effects, EffectKey, (e, effect) => e.Record(0, effect));
            w.Int64(10, s.NextEffectSeq);
            w.OptionalInt32(11, s.Hero?.Value);
            w.OptionalRecord(12, s.Combat is null ? null : new CombatRecord(s.Combat));
            w.OptionalRecord(13, s.Ended is null ? null : new RunEndRecord(s.Ended));
            w.Int64(14, s.NextEventSeq);
            w.EndRecord();
        }
    }

    private sealed class UnitRecord(Unit u) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(12);
            w.Int32(0, u.Id.Value);
            w.Int32(1, u.Pos.X);
            w.Int32(2, u.Pos.Y);
            w.Enum(3, u.Faction);
            w.List(4, u.Boons, (e, boon) => e.Int32(0, boon));
            w.Record(5, new StatsRecord(u.Stats));
            w.Int32(6, u.Hp);
            w.Record(7, new BudgetRecord(u.Budget));
            w.List(8, u.Spent, (e, spend) => e.Record(0, new SpendRecord(spend)));
            w.OptionalRecord(9, u.Opener is null ? null : new OpenerRecord(u.Opener));
            w.List(10, u.Abilities, (e, id) => e.String(0, id));
            w.List(11, u.Reactions, (e, r) => e.Record(0, new ReactionRecord(r)));
            w.EndRecord();
        }
    }

    private sealed class ReactionRecord(ReactionSetting r) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.String(0, r.AbilityId);
            w.Enum(1, r.Mode);
            w.EndRecord();
        }
    }

    private sealed class OpenerRecord(OpenerCharge o) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.Record(0, new BudgetRecord(o.Charged));
            w.List(1, o.TurnPools, (e, spend) => e.Record(0, new SpendRecord(spend)));
            w.EndRecord();
        }
    }

    private sealed class BudgetRecord(TurnBudget b) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(6);
            w.Int32(0, b.MovementUsed);
            w.Int32(1, b.ActionsUsed);
            w.Bool(2, b.BonusUsed);
            w.Bool(3, b.ReactionUsed);
            w.Bool(4, b.FreeUsed);
            w.Bool(5, b.CampSpent);
            w.EndRecord();
        }
    }

    private sealed class SpendRecord(PoolSpend p) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.String(0, p.PoolId);
            w.Int32(1, p.Spent);
            w.EndRecord();
        }
    }

    private sealed class CombatRecord(CombatState c) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(5);
            w.List(0, c.Order, (e, id) => e.Int32(0, id.Value));
            w.Int32(1, c.Current);
            w.Int64(2, c.Round);
            w.Bool(3, c.TurnVacated);
            w.Bool(4, c.BeforeRound1);
            w.EndRecord();
        }
    }

    private sealed class RunEndRecord(RunEnd r) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.Enum(0, r.Cause);
            w.OptionalUInt64(1, EventKey(r.KillingEventId));
            w.EndRecord();
        }
    }

    private sealed class StatsRecord(UnitStats st) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(12);
            w.Int32(0, st.Level);
            w.Int32(1, st.Abilities.Strength);
            w.Int32(2, st.Abilities.Dexterity);
            w.Int32(3, st.Abilities.Constitution);
            w.Int32(4, st.Abilities.Intelligence);
            w.Int32(5, st.Abilities.Wisdom);
            w.Int32(6, st.Abilities.Presence);
            w.Enum(7, st.MainAbility);
            w.Enum(8, st.TrainedSave);
            w.List(9, st.TrainedSkills, (e, skill) => e.Enum(0, skill));
            w.Enum(10, st.HpTier);
            w.Int32(11, st.TableHp);
            w.EndRecord();
        }
    }

    private sealed class StreamsRecord(RunStreams rng) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(2);
            w.UInt64(0, rng.Combat.Counter);
            w.UInt64(1, rng.Shrine.Counter);
            w.EndRecord();
        }
    }

    private sealed class PendingRecord(PendingDecision p) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(6);
            w.Int32(0, p.Id.Value);
            w.Int32(1, p.Unit.Value);
            w.Enum(2, p.Kind);
            w.List(3, p.Options, (e, option) => e.Int32(0, option));
            w.Int32(4, p.Source.X);
            w.Int32(5, p.Source.Y);
            w.EndRecord();
        }
    }
}
