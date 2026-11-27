using Dungeon.Core.Canonical;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation;

/// <summary>
/// A simulator-arm row, with exactly the SV 9.1 fields: <c>{id: "Axis.Value", owner, configDelta}</c>.
/// <see cref="ConfigDelta"/> is an ASCII string until its schema arrives (M1-06).
/// </summary>
public sealed record ArmRow(string File, string Id, string Owner, string ConfigDelta) : ContentRecord(File, Id)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(3);
        w.String(0, Id);
        w.String(1, Owner);
        w.String(2, ConfigDelta);
        w.EndRecord();
    }
}
