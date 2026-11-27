using System;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace Dungeon.Core.Canonical;

/// <summary>
/// A SHA-256 digest over canonical bytes (SV 4.5): stored in full as four 64-bit words (big-endian
/// from the hash bytes, so <see cref="ToHex"/> follows byte order) and shown truncated to 128 bits.
/// Value equality compares all 256 bits.
/// </summary>
public readonly record struct Digest(ulong W0, ulong W1, ulong W2, ulong W3)
{
    /// <summary>The SHA-256 of <paramref name="bytes"/>.</summary>
    public static Digest Of(ReadOnlySpan<byte> bytes)
    {
        Span<byte> hash = stackalloc byte[32];
        SHA256.HashData(bytes, hash);
        return new Digest(
            BinaryPrimitives.ReadUInt64BigEndian(hash[..8]),
            BinaryPrimitives.ReadUInt64BigEndian(hash[8..16]),
            BinaryPrimitives.ReadUInt64BigEndian(hash[16..24]),
            BinaryPrimitives.ReadUInt64BigEndian(hash[24..]));
    }

    /// <summary>The SHA-256 of <paramref name="value"/>'s canonical bytes.</summary>
    public static Digest Of(ICanonical value) => Of(CanonicalWriter.Serialize(value));

    /// <summary>All 256 bits as 64 lowercase hex digits.</summary>
    public string ToHex() => Hex(4);

    /// <summary>The first 128 bits as 32 lowercase hex digits, the form shown to people.</summary>
    public string ToShortHex() => Hex(2);

    /// <summary>The 128-bit display form.</summary>
    public override string ToString() => ToShortHex();

    private string Hex(int words)
    {
        Span<byte> bytes = stackalloc byte[32];
        BinaryPrimitives.WriteUInt64BigEndian(bytes[..8], W0);
        BinaryPrimitives.WriteUInt64BigEndian(bytes[8..16], W1);
        BinaryPrimitives.WriteUInt64BigEndian(bytes[16..24], W2);
        BinaryPrimitives.WriteUInt64BigEndian(bytes[24..], W3);
        return Convert.ToHexString(bytes[..(words * 8)]).ToLowerInvariant();
    }
}
