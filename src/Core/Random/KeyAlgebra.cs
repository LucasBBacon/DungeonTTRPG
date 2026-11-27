using System;

namespace Dungeon.Core;

/// <summary>
/// The key algebra of SV 4.1: SplitMix64 mixing, FNV-1a-64 label ids, and the pure
/// <see cref="Derive"/> / <see cref="Draw"/> / <see cref="H"/> functions every random decision is built from.
/// All key arithmetic is unsigned 64-bit modulo 2^64. This is the only file allowed wrapping
/// arithmetic (DS11, DUN011); all wrapping arithmetic below sits inside an explicit <c>unchecked</c> block.
/// </summary>
public static class KeyAlgebra
{
    /// <summary>The SplitMix64 increment (golden-ratio constant).</summary>
    public const ulong Gamma = 0x9E3779B97F4A7C15UL;

    private const ulong FnvOffset = 0xcbf29ce484222325UL;
    private const ulong FnvPrime = 0x100000001b3UL;
    private const ulong ArityTag = 0xD1B54A32D192ED03UL;

    /// <summary>The SplitMix64 finalizer.</summary>
    public static ulong Mix(ulong z)
    {
        unchecked
        {
            z ^= z >> 30;
            z *= 0xBF58476D1CE4E5B9UL;
            z ^= z >> 27;
            z *= 0x94D049BB133111EBUL;
            z ^= z >> 31;
            return z;
        }
    }

    /// <summary>
    /// FNV-1a-64 over the UTF-8 bytes of <paramref name="s"/>, which must match ASCII <c>[A-Za-z0-9_.:-]</c>
    /// (so the bytes are the characters and no culture or encoding can change the result).
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="s"/> contains a character outside the grammar.</exception>
    public static ulong LabelId(string s)
    {
        ArgumentNullException.ThrowIfNull(s);
        foreach (var c in s)
        {
            var ok = c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or (>= '0' and <= '9') or '_' or '.' or ':' or '-';
            if (!ok)
                throw new ArgumentException($"Label contains invalid character U+{(int)c:X4}; allowed are ASCII [A-Za-z0-9_.:-].", nameof(s));
        }

        unchecked
        {
            var h = FnvOffset;
            foreach (var c in s)
            {
                h ^= (byte)c;
                h *= FnvPrime;
            }

            return h;
        }
    }

    /// <summary><c>Mix(h ^ Mix(x + γ))</c>.</summary>
    public static ulong Absorb(ulong h, ulong x)
    {
        unchecked
        {
            return Mix(h ^ Mix(x + Gamma));
        }
    }

    /// <summary>
    /// Derives a child key: one label plus the indices that follow it is a single call, and the index count is part of the key.
    /// </summary>
    public static ulong Derive(ulong key, string label, params ulong[] indices)
    {
        ArgumentNullException.ThrowIfNull(indices);
        unchecked
        {
            var h = Absorb(key, LabelId(label));
            foreach (var i in indices)
                h = Absorb(h, i);
            return Absorb(h, ArityTag ^ (ulong)indices.Length);
        }
    }

    /// <summary>The <paramref name="i"/>-th (0-based) draw of a stream keyed by <paramref name="key"/>.</summary>
    public static ulong Draw(ulong key, ulong i)
    {
        unchecked
        {
            return Mix(key + (i + 1) * Gamma);
        }
    }

    /// <summary>Maps <paramref name="u"/> onto <c>[0, n)</c> as the high 64 bits of <c>u × n</c>.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="n"/> is 0 (in every build configuration).</exception>
    public static ulong Bound(ulong u, ulong n)
    {
        if (n == 0)
            throw new ArgumentOutOfRangeException(nameof(n), n, "Bound requires n >= 1.");
        return Math.BigMul(u, n, out _);
    }

    /// <summary>A keyed hash: the first draw of the key derived from <paramref name="domain"/> and <paramref name="parts"/>.</summary>
    public static ulong H(ulong key, string domain, params ulong[] parts) => Draw(Derive(key, domain, parts), 0);

    /// <summary>A d20 roll, <c>1 + Bound(u, 20)</c>.</summary>
    public static int D20(ulong u)
    {
        unchecked
        {
            return 1 + (int)Bound(u, 20);
        }
    }
}

/// <summary>
/// A stream of draws: a key and a counter (SV 4.1). Immutable; every step returns the value and the next stream.
/// Counter arithmetic is explicitly <c>checked</c>, so an overflow throws <see cref="OverflowException"/> in every
/// build configuration instead of wrapping back to draw 0.
/// </summary>
/// <param name="Key">The stream's key.</param>
/// <param name="Counter">The 0-based index of the next draw.</param>
public readonly record struct DrawStream(ulong Key, ulong Counter)
{
    /// <summary>A fresh stream at counter 0.</summary>
    public static DrawStream Open(ulong key) => new(key, 0);

    /// <summary>The next draw, <c>Draw(Key, Counter)</c>, and the stream advanced by one.</summary>
    public (ulong Value, DrawStream Next) Next()
        => (KeyAlgebra.Draw(Key, Counter), new DrawStream(Key, checked(Counter + 1)));

    /// <summary>The next draw mapped onto <c>[0, n)</c> by <see cref="KeyAlgebra.Bound"/>.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="n"/> is 0.</exception>
    public (ulong Value, DrawStream Next) NextBelow(ulong n)
    {
        var (u, next) = Next();
        return (KeyAlgebra.Bound(u, n), next);
    }

    /// <summary>The stream advanced by <paramref name="n"/> draws without consuming values.</summary>
    /// <exception cref="OverflowException">The counter would pass 2^64 - 1.</exception>
    public DrawStream Skip(ulong n) => new(Key, checked(Counter + n));
}
