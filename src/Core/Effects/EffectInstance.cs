using Dungeon.Core.Canonical;
using Dungeon.Core.Conditions;

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
/// <see cref="Condition"/> is the condition part (M1-12a plan call 2), set exactly on an instance of a condition record
/// placed by <c>Conditions.Insert</c>; a bare <c>condition</c>-kind carrier from <c>effects.json</c> has none. A condition
/// instance keeps its source id after the source dies (plan call 8).
/// </summary>
public sealed record EffectInstance(long Seq, string EffectId, UnitId Holder, EffectSource Source, ConditionState? Condition = null) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(5);
        w.Int64(0, Seq);
        w.String(1, EffectId);
        w.Int32(2, Holder.Value);
        w.OptionalInt32(3, Source.Unit?.Value);
        w.OptionalRecord(4, Condition);
        w.EndRecord();
    }
}
