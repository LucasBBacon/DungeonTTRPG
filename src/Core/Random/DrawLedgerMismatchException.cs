using System;
using System.Globalization;

namespace Dungeon.Core;

/// <summary>
/// <c>DrawLedgerMismatch{path, expected, actual}</c> (SV 4.2): a stream row drew other than its count class allows
/// when its stage run closed. Class <c>defect</c> (LR5) in every build.
/// </summary>
public sealed class DrawLedgerMismatchException : Exception
{
    /// <summary>Creates the mismatch for <paramref name="path"/>.</summary>
    /// <param name="path">The stream row id (display only).</param>
    /// <param name="expected">What the row's count class allows.</param>
    /// <param name="actual">What the run drew.</param>
    public DrawLedgerMismatchException(string path, ulong expected, ulong actual)
        : base(string.Create(CultureInfo.InvariantCulture, $"DrawLedgerMismatch{{{path}, {expected}, {actual}}}"))
    {
        Path = path;
        Expected = expected;
        Actual = actual;
    }

    /// <summary>The stream row id.</summary>
    public string Path { get; }

    /// <summary>The count the row's class allows.</summary>
    public ulong Expected { get; }

    /// <summary>The count drawn.</summary>
    public ulong Actual { get; }
}

/// <summary>
/// A stage run closed on a stream row whose count formula (or cap) is <c>owed:OWNER</c> (plan call 4): the stage
/// cannot run until that owner publishes the formula.
/// </summary>
public sealed class DrawLedgerOwedException : Exception
{
    /// <summary>Creates the hold for <paramref name="path"/>.</summary>
    /// <param name="path">The stream row id (display only).</param>
    /// <param name="owner">The owner the formula waits on.</param>
    public DrawLedgerOwedException(string path, string owner)
        : base($"DrawLedgerOwed{{{path}, {owner}}}: the count formula is owed by {owner}, who has not published it.")
    {
        Path = path;
        Owner = owner;
    }

    /// <summary>The stream row id.</summary>
    public string Path { get; }

    /// <summary>The owner the formula waits on.</summary>
    public string Owner { get; }
}
