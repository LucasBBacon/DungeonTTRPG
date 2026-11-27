using Dungeon.Core.Canonical;

namespace Dungeon.Core.Effects;

/// <summary>
/// Where an active effect came from: a unit, or the environment (terrain or the room) when <see cref="Unit"/> is
/// null. Triggers fire in source unit order, environment sources last (M1-07 plan call 4).
/// </summary>
public readonly record struct EffectSource(UnitId? Unit)
{
    /// <summary>The environment: no source unit.</summary>
    public static EffectSource Environment => new(null);
}

/// <summary>
/// An active effect: an instance of the definition <see cref="EffectId"/> held by <see cref="Holder"/>.
/// <see cref="Seq"/> is its creation number, taken from <see cref="GameState.NextEffectSeq"/>: positive, unique in the
/// run and never reused (plan call 4). The run state keeps its instances sorted by <see cref="Seq"/>.
/// </summary>
public sealed record EffectInstance(long Seq, string EffectId, UnitId Holder, EffectSource Source) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(4);
        w.Int64(0, Seq);
        w.String(1, EffectId);
        w.Int32(2, Holder.Value);
        w.OptionalInt32(3, Source.Unit?.Value);
        w.EndRecord();
    }
}
