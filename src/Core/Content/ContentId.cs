using System;

namespace Dungeon.Core.Content;

/// <summary>
/// A stable content record id: non-empty ASCII <c>[a-z0-9_.:-]</c> (SV 4.1 K-rule 3). Stream and
/// domain labels may also use capitals; that rule belongs to <c>LabelId</c>, not to content ids.
/// </summary>
public readonly record struct ContentId(string Value)
{
    /// <summary>True if <paramref name="s"/> is non-empty and every character is in <c>[a-z0-9_.:-]</c>.</summary>
    public static bool IsValid(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        foreach (var c in s)
        {
            if (!IsIdChar(c))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>Parses an id, or throws <see cref="ArgumentException"/> naming the first bad character.</summary>
    public static ContentId Parse(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            throw new ArgumentException("A content id must not be empty.", nameof(s));
        }

        for (var i = 0; i < s.Length; i++)
        {
            if (!IsIdChar(s[i]))
            {
                throw new ArgumentException(
                    $"Invalid character '{s[i]}' (U+{(int)s[i]:X4}) at index {i} in content id \"{s}\"; ids use [a-z0-9_.:-].",
                    nameof(s));
            }
        }

        return new ContentId(s);
    }

    /// <inheritdoc />
    public override string ToString() => Value;

    private static bool IsIdChar(char c) =>
        (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '_' || c == '.' || c == ':' || c == '-';
}
