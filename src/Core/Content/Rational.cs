using System;
using Dungeon.Core.Canonical;

namespace Dungeon.Core.Content;

/// <summary>
/// A rational in canonical form: reduced <c>P/Q</c> with <c>Q &gt; 0</c> (DS13). Zero is <c>0/1</c>.
/// Build one with <see cref="Of"/> or <see cref="TryParse"/>; both reject any other form.
/// </summary>
public readonly record struct Rational(long P, long Q) : ICanonical
{
    /// <summary>Makes a rational, throwing <see cref="ArgumentException"/> unless <c>q &gt; 0</c> and <c>gcd(|p|, q) = 1</c>.</summary>
    public static Rational Of(long p, long q)
    {
        if (q <= 0)
        {
            throw new ArgumentException($"The denominator must be positive, got {q}.", nameof(q));
        }

        if (Gcd(Magnitude(p), (ulong)q) != 1)
        {
            throw new ArgumentException($"{p}/{q} is not reduced.", nameof(p));
        }

        return new Rational(p, q);
    }

    /// <summary>
    /// Parses exactly <c>-?[0-9]+</c> (denominator 1) or <c>-?[0-9]+/[0-9]+</c> in reduced form with
    /// a positive denominator. A denominator of 1 is written as a plain integer, so <c>1/1</c> is rejected,
    /// and no number has a leading zero or is <c>-0</c>, so <see cref="ToString"/> round-trips every accepted input.
    /// </summary>
    public static bool TryParse(string s, out Rational r)
    {
        r = default;
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        var slash = s.IndexOf('/');
        var left = slash < 0 ? s : s.Substring(0, slash);

        var negative = left.Length > 0 && left[0] == '-';
        if (!TryDigits(negative ? left.Substring(1) : left, out var mag))
        {
            return false;
        }

        // "-0" is not canonical: zero is written "0".
        if (negative && mag == 0)
        {
            return false;
        }

        // |long.MinValue| = long.MaxValue + 1 is the only magnitude allowed beyond long.MaxValue.
        var limit = negative ? (ulong)long.MaxValue + 1 : (ulong)long.MaxValue;
        if (mag > limit)
        {
            return false;
        }

        long p;
        if (!negative)
        {
            p = (long)mag;
        }
        else if (mag > (ulong)long.MaxValue)
        {
            p = long.MinValue;
        }
        else
        {
            p = -(long)mag;
        }

        if (slash < 0)
        {
            r = new Rational(p, 1);
            return true;
        }

        if (!TryDigits(s.Substring(slash + 1), out var qMag) || qMag == 0 || qMag > (ulong)long.MaxValue || qMag == 1)
        {
            return false;
        }

        if (Gcd(mag, qMag) != 1)
        {
            return false;
        }

        r = new Rational(p, (long)qMag);
        return true;
    }

    /// <summary>Writes a record of two <c>Int64</c> fields: 0 is <see cref="P"/>, 1 is <see cref="Q"/>.</summary>
    public void WriteCanonical(CanonicalWriter w)
    {
        ArgumentNullException.ThrowIfNull(w);
        w.BeginRecord(2);
        w.Int64(0, P);
        w.Int64(1, Q);
        w.EndRecord();
    }

    /// <summary>Gives <c>"p/q"</c>, or <c>"p"</c> when q is 1.</summary>
    public override string ToString() =>
        Q == 1 ? P.ToString(System.Globalization.CultureInfo.InvariantCulture)
               : P.ToString(System.Globalization.CultureInfo.InvariantCulture) + "/" + Q.ToString(System.Globalization.CultureInfo.InvariantCulture);

    private static ulong Magnitude(long p) =>
        p == long.MinValue ? (ulong)long.MaxValue + 1 : p < 0 ? (ulong)(-p) : (ulong)p;

    /// <summary>Euclid on unsigned values, so no signed division is involved (DUN010). Gcd(0, n) = n.</summary>
    private static ulong Gcd(ulong a, ulong b)
    {
        while (b != 0)
        {
            var t = a % b;
            a = b;
            b = t;
        }

        return a;
    }

    /// <summary>
    /// Reads one or more ASCII digits into an unsigned value; false on any other character, a leading
    /// zero (<c>"007"</c>, <c>"00"</c>; only <c>"0"</c> itself starts with one) or overflow.
    /// </summary>
    private static bool TryDigits(string s, out ulong value)
    {
        value = 0;
        if (s.Length == 0 || (s.Length > 1 && s[0] == '0'))
        {
            return false;
        }

        foreach (var c in s)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }

            var d = (ulong)(c - '0');
            if (value > (ulong.MaxValue - d) / 10)
            {
                return false;
            }

            value = value * 10 + d;
        }

        return true;
    }
}
