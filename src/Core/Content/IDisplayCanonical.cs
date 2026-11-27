using Dungeon.Core.Canonical;

namespace Dungeon.Core.Content;

/// <summary>
/// A record with display fields (SV 4.6). <see cref="WriteDisplay"/> writes them as one record, with
/// <see cref="ContentRecord.Id"/> as field 0 and display text through <see cref="CanonicalWriter.Text"/>.
/// <see cref="ContentSet.DigestOf"/> includes these bytes, so editing a display field invalidates the
/// read-set cache of every check that reads the file.
/// </summary>
public interface IDisplayCanonical
{
    /// <summary>Writes the display fields as one complete record.</summary>
    void WriteDisplay(CanonicalWriter w);
}
