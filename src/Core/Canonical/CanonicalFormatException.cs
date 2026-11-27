using System;

namespace Dungeon.Core.Canonical;

/// <summary>
/// The bytes are not a canonical encoding: truncated, a bad tag, a non-ASCII string, an unsorted
/// or duplicate set, a length beyond the input, or trailing bytes. The message names the offset.
/// </summary>
public sealed class CanonicalFormatException : Exception
{
    public CanonicalFormatException(int offset, string problem)
        : base($"Invalid canonical bytes at offset {offset}: {problem}")
    {
        Offset = offset;
    }

    /// <summary>The byte offset where reading failed.</summary>
    public int Offset { get; }
}
