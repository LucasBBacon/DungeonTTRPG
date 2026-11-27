using Dungeon.Core.Canonical;

namespace Dungeon.Core.Content;

/// <summary>
/// One record read from a content file. <see cref="Id"/> is its ASCII id, whose grammar its
/// <see cref="ContentKind"/> checks. <see cref="WriteCanonical"/> writes the rules fields with
/// <see cref="Id"/> as field 0; <see cref="File"/> is where it came from and is not written.
/// </summary>
public abstract record ContentRecord(string File, string Id) : ICanonical
{
    /// <inheritdoc />
    public abstract void WriteCanonical(CanonicalWriter w);
}
