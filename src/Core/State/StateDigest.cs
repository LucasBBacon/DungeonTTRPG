using System;
using Dungeon.Core.Canonical;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core;

/// <summary>
/// The replay core-state hash (SV 4.5): the <see cref="Digest"/> of a <see cref="GameState"/>'s
/// canonical bytes. Units are a set keyed by id, so their stored order does not matter; boons and
/// decision options are lists, because their order is data. Active effects are a set keyed by seq.
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

    private sealed class StateRecord(GameState s) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(11);
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
            w.EndRecord();
        }
    }

    private sealed class UnitRecord(Unit u) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(6);
            w.Int32(0, u.Id.Value);
            w.Int32(1, u.Pos.X);
            w.Int32(2, u.Pos.Y);
            w.Enum(3, u.Faction);
            w.List(4, u.Boons, (e, boon) => e.Int32(0, boon));
            w.Record(5, new StatsRecord(u.Stats));
            w.EndRecord();
        }
    }

    private sealed class StatsRecord(UnitStats st) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(10);
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
