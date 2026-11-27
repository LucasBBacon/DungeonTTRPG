namespace Dungeon.Core.Canonical;

/// <summary>
/// A value with a canonical byte form (SV 4.5). An implementation calls
/// <see cref="CanonicalWriter.BeginRecord"/> with its field count, writes fields 0, 1, 2 … in
/// that declared order, then calls <see cref="CanonicalWriter.EndRecord"/>. The ordinals are the
/// schema: reordering them is a format change, never a refactor.
/// </summary>
public interface ICanonical
{
    /// <summary>Writes this value as one record.</summary>
    void WriteCanonical(CanonicalWriter w);
}
