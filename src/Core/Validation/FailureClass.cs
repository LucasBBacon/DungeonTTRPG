using System;
using System.Diagnostics.CodeAnalysis;

namespace Dungeon.Core.Validation;

/// <summary>
/// The kind of a failure class: the eight SV 5.1 classes, plus <see cref="Build"/> for <c>s</c>/<c>t</c>
/// rows (plan call 1). Written with the first letter in lower case: <c>repair</c>, <c>stageRetry</c>,
/// <c>floorRetry</c>, <c>waivable</c>, <c>neverWaived</c>, <c>defect</c>, <c>runtimeAssert</c>, <c>report</c>, <c>build</c>.
/// </summary>
public enum FailureKind
{
    Repair,
    StageRetry,
    FloorRetry,
    Waivable,
    NeverWaived,
    Defect,
    RuntimeAssert,
    Report,
    Build,
}

/// <summary>
/// A check's failure class as SV writes it (plan call 2): a kind, with an argument in parentheses for
/// <c>stageRetry(stage)</c> and <c>waivable(fallback)</c>, such as <c>"stageRetry(traps)"</c>. Other kinds take no argument.
/// </summary>
public readonly record struct FailureClass(FailureKind Kind, string? Argument)
{
    private static readonly string[] Names =
        ["repair", "stageRetry", "floorRetry", "waivable", "neverWaived", "defect", "runtimeAssert", "report", "build"];

    /// <summary>True if <paramref name="kind"/> needs an argument (<c>stageRetry</c>, <c>waivable</c>).</summary>
    public static bool TakesArgument(FailureKind kind) => kind is FailureKind.StageRetry or FailureKind.Waivable;

    /// <summary>
    /// Parses <c>kind</c> or <c>kind(argument)</c>, where the argument is ASCII <c>[A-Za-z0-9_.:-]+</c>.
    /// The kind name must match exactly, and the argument must be present exactly when <see cref="TakesArgument"/>.
    /// </summary>
    public static bool TryParse(string? s, out FailureClass c)
    {
        c = default;
        if (s is null)
        {
            return false;
        }

        string name = s;
        string? argument = null;
        int open = s.IndexOf('(');
        if (open >= 0)
        {
            if (s[^1] != ')')
            {
                return false;
            }

            name = s.Substring(0, open);
            argument = s.Substring(open + 1, s.Length - open - 2);
            if (!IsArgument(argument))
            {
                return false;
            }
        }

        int index = Array.IndexOf(Names, name);
        if (index < 0)
        {
            return false;
        }

        var kind = (FailureKind)index;
        if (TakesArgument(kind) != (argument is not null))
        {
            return false;
        }

        c = new FailureClass(kind, argument);
        return true;
    }

    /// <summary>The written form, such as <c>"stageRetry(traps)"</c>.</summary>
    public override string ToString()
    {
        string name = Names[(int)Kind];
        return Argument is null ? name : name + "(" + Argument + ")";
    }

    private static bool IsArgument([NotNullWhen(true)] string? s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        foreach (var c in s)
        {
            bool ok = (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')
                || c == '_' || c == '.' || c == ':' || c == '-';
            if (!ok)
            {
                return false;
            }
        }

        return true;
    }
}
