using System.Collections.Immutable;

namespace Dungeon.Core.Effects;

/// <summary>How a derived number folds its modifiers (M1-07 plan call 1; <c>docs/effects-vocabulary.md</c>).</summary>
public enum NumberRule
{
    /// <summary>
    /// The source-kind regime: <c>condition</c> modifiers give the best bonus plus the worst penalty (CO 6.7), every
    /// other kind sums (SM 9), and the two groups add.
    /// </summary>
    Stack,

    /// <summary>
    /// The ability rule, whatever the source kind: the highest bonus plus the worst penalty, then the cap (SM 5.1).
    /// </summary>
    AbilityScore,

    /// <summary>
    /// <see cref="Stack"/> for the flat changes, then at most one halving rounding down, and any <c>zero</c>
    /// overrides everything (CO 6.7).
    /// </summary>
    Speed,
}

/// <summary>
/// A derived number effects can change: its id (a modifier's <c>target</c>), its fold rule, its base value and an
/// optional cap applied last. Numbers are integers (SM 3).
/// </summary>
public sealed record DerivedNumber(string Id, NumberRule Rule, long Base, long? Cap);

/// <summary>The production derived-number table (plan call 5). Later steps add rows; tests pass their own tables.</summary>
public static class DerivedNumbers
{
    /// <summary>The M1-07 rows, ordinal by id: <c>speed</c> (SM 6, base 6 squares).</summary>
    public static ImmutableArray<DerivedNumber> Core { get; } =
        [
            new DerivedNumber("speed", NumberRule.Speed, 6, null),
        ];
}

/// <summary>The production roll-target table: the targets a <c>rollMode</c> modifier may name (plan call 5).</summary>
public static class RollTargets
{
    /// <summary>
    /// The M1-07 rows, ordinal: <c>attackRoll</c> (the holder's own attack rolls) and <c>attacksAgainst</c>
    /// (attack rolls made against the holder).
    /// </summary>
    public static ImmutableArray<string> Core { get; } = ["attackRoll", "attacksAgainst"];
}

/// <summary>The production rule-event table: the events a trigger's <c>on</c> may name (plan call 5).</summary>
public static class RuleEvents
{
    /// <summary>The M1-07 rows, ordinal: <c>unitMoved</c> (a unit finished a move; the subject is that unit).</summary>
    public static ImmutableArray<string> Core { get; } = ["unitMoved"];
}
