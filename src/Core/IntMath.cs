using System;

namespace Dungeon.Core;

/// <summary>
/// The only place in Core where signed <c>/</c> and <c>%</c> are allowed (DS11, analyzer rule DUN010).
/// Division rounds toward negative infinity and the remainder takes the sign of the divisor,
/// so <c>FloorDiv(a, b) * b + FloorMod(a, b) == a</c> for every non-zero <c>b</c>.
/// </summary>
public static class IntMath
{
    /// <summary>Floor division. Throws <see cref="DivideByZeroException"/> for zero and <see cref="OverflowException"/> for <see cref="int.MinValue"/> / -1.</summary>
    public static int FloorDiv(int a, int b)
    {
        if (b == 0) throw new DivideByZeroException();
        if (a == int.MinValue && b == -1) throw new OverflowException();
        var q = a / b;
        var r = a % b;
        return r != 0 && (r < 0) != (b < 0) ? q - 1 : q;
    }

    /// <summary>Floor modulo: zero or carrying the sign of <paramref name="b"/>. Throws <see cref="DivideByZeroException"/> for zero.</summary>
    public static int FloorMod(int a, int b)
    {
        if (b == 0) throw new DivideByZeroException();
        // x86 raises on int.MinValue % -1 even though the mathematical result is 0.
        if (b == -1) return 0;
        var r = a % b;
        return r != 0 && (r < 0) != (b < 0) ? r + b : r;
    }

    /// <summary>Floor division. Throws <see cref="DivideByZeroException"/> for zero and <see cref="OverflowException"/> for <see cref="long.MinValue"/> / -1.</summary>
    public static long FloorDiv(long a, long b)
    {
        if (b == 0) throw new DivideByZeroException();
        if (a == long.MinValue && b == -1) throw new OverflowException();
        var q = a / b;
        var r = a % b;
        return r != 0 && (r < 0) != (b < 0) ? q - 1 : q;
    }

    /// <summary>Floor modulo: zero or carrying the sign of <paramref name="b"/>. Throws <see cref="DivideByZeroException"/> for zero.</summary>
    public static long FloorMod(long a, long b)
    {
        if (b == 0) throw new DivideByZeroException();
        if (b == -1) return 0;
        var r = a % b;
        return r != 0 && (r < 0) != (b < 0) ? r + b : r;
    }
}
