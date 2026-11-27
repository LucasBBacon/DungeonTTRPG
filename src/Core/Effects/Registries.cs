using System;
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

/// <summary>
/// A rule event a trigger's <c>on</c> may name: its id, the <see cref="GameEvent"/> type that raises it, and its
/// subject, the unit a <c>holder</c>-scope trigger compares with its holder and <c>"to": "subject"</c> gives an effect
/// to. Built with <see cref="Of{T}"/>.
/// </summary>
public sealed record RuleEvent
{
    private RuleEvent(string id, Type eventType, Func<GameEvent, UnitId> subject)
    {
        Id = id;
        EventType = eventType;
        Subject = subject;
    }

    /// <summary>The id content names in a trigger's <c>on</c>.</summary>
    public string Id { get; }

    /// <summary>The event type that raises it: an event raises it when its runtime type is exactly this.</summary>
    public Type EventType { get; }

    /// <summary>The event's subject unit.</summary>
    public Func<GameEvent, UnitId> Subject { get; }

    /// <summary>The rule event <paramref name="id"/>, raised by each <typeparamref name="T"/>, with the given subject.</summary>
    public static RuleEvent Of<T>(string id, Func<T, UnitId> subject)
        where T : GameEvent
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(subject);
        return new RuleEvent(id, typeof(T), e => subject((T)e));
    }
}

/// <summary>The production rule-event table: the events a trigger's <c>on</c> may name (plan call 5).</summary>
public static class RuleEvents
{
    /// <summary>
    /// The M1-07 rows, ordinal by id: <c>unitMoved</c>, raised by <see cref="UnitMoved"/> (a unit finished a move;
    /// the subject is that unit). <c>docs/effects-vocabulary.md</c> section 6 keeps the same table.
    /// </summary>
    public static ImmutableArray<RuleEvent> Core { get; } =
        [
            RuleEvent.Of<UnitMoved>("unitMoved", e => e.Unit),
        ];
}
