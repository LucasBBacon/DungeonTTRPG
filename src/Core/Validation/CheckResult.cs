using System.Collections.Immutable;

namespace Dungeon.Core.Validation;

/// <summary>One failure of a check, naming the file and field (a JSON path such as <c>$.checks[0].class</c>).</summary>
public sealed record CheckFailure(CheckId Check, string File, string Field, string Problem);

/// <summary>The result of a validation run: every failure, ordinal by check, file, field and problem.</summary>
public sealed record ValidationReport(ImmutableArray<CheckFailure> Failures)
{
    /// <summary>True when no check failed.</summary>
    public bool Passed => Failures.IsEmpty;
}
