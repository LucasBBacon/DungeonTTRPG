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
    /// The ability rule, whatever the source kind: the base plus the highest bonus, capped, plus the worst penalty
    /// (SM 5.1: "+5 after all bonuses"; "a bonus and a penalty both apply").
    /// </summary>
    AbilityScore,

    /// <summary>
    /// <see cref="Stack"/> for the flat changes, then at most one halving rounding down, and any <c>zero</c>
    /// overrides everything (CO 6.7).
    /// </summary>
    Speed,
}

/// <summary>
/// A derived number's base for one unit, before the unit's effects fold in. Nothing derived is stored (SM 9): the base
/// reads what the unit stores and, through <see cref="NumberBaseArgs.Number"/>, other derived numbers of the same unit
/// after their effects (max HP reads Constitution, a save reads two abilities). It must be pure and make no draws.
/// </summary>
public delegate long NumberBase(NumberBaseArgs args);

/// <summary>What a <see cref="NumberBase"/> reads: the state, the unit, and the unit's other derived numbers.</summary>
public sealed class NumberBaseArgs
{
    private readonly RulesContext _rules;
    private readonly ImmutableArray<string> _path;

    internal NumberBaseArgs(RulesContext rules, GameState state, Unit unit, ImmutableArray<string> path)
    {
        _rules = rules;
        _path = path;
        State = state;
        Unit = unit;
    }

    /// <summary>The state the number is folded on.</summary>
    public GameState State { get; }

    /// <summary>The unit whose number it is.</summary>
    public Unit Unit { get; }

    /// <summary>
    /// The unit's derived number <paramref name="numberId"/>, folded with its effects, as
    /// <see cref="Effects.Number"/> gives it. Throws <see cref="ArgumentException"/> for an unknown number, and
    /// <see cref="InvalidOperationException"/> naming the path if the read closes a cycle of bases.
    /// </summary>
    public long Number(string numberId) => Effects.Fold(_rules, State, Unit.Id, numberId, _path);
}

/// <summary>
/// A derived number effects can change: its id (a modifier's <c>target</c>), its fold rule, its base per unit, a cap
/// and a floor. Only an <see cref="NumberRule.AbilityScore"/> number may have a cap (SM 5.1, the only cap the designs
/// give); <see cref="RulesContext"/> rejects a cap on any other rule. The floor binds after the rule's whole result,
/// cap included: the value is max(result, floor) (M1-09 plan call 5; Recovery "minimum 1", DH 8). The context rejects a
/// floor on an <see cref="NumberRule.AbilityScore"/> number (SM 12 leaves ability floors open) and on a
/// <see cref="NumberRule.Speed"/> number (speed 0 overrides everything, CO 6.7). Numbers are integers (SM 3).
/// </summary>
public sealed record DerivedNumber(string Id, NumberRule Rule, NumberBase Base, long? Cap, long? Floor = null)
{
    /// <summary>A number whose base is the same constant for every unit.</summary>
    public DerivedNumber(string id, NumberRule rule, long constantBase, long? cap, long? floor = null)
        : this(id, rule, _ => constantBase, cap, floor)
    {
    }
}

/// <summary>The production derived-number table (plan call 5). Later steps add rows; tests pass their own tables.</summary>
public static class DerivedNumbers
{
    /// <summary>
    /// <c>speed</c> (SM 6, base 6 squares), then the M1-08 stat rows (<see cref="Dungeon.Core.Stats.StatNumbers.Rows"/>).
    /// The context sorts them by id.
    /// </summary>
    public static ImmutableArray<DerivedNumber> Core { get; } =
        [
            new DerivedNumber("speed", NumberRule.Speed, 6, null),
            .. Dungeon.Core.Stats.StatNumbers.Rows,
        ];
}

/// <summary>The production roll-target table: the targets a <c>rollMode</c> modifier may name (plan call 5).</summary>
public static class RollTargets
{
    /// <summary>
    /// <c>attackRoll</c> (the holder's own attack rolls) and <c>attacksAgainst</c> (attack rolls made against the
    /// holder), then the M1-08 save and skill targets (<see cref="Dungeon.Core.Stats.StatNumbers.RollTargets"/>).
    /// </summary>
    public static ImmutableArray<string> Core { get; } =
        ["attackRoll", "attacksAgainst", .. Dungeon.Core.Stats.StatNumbers.RollTargets];
}

/// <summary>
/// The production flag tables: the targets and names a <c>flag</c> modifier may give (plan call 5). A flag is a
/// boolean a unit's active effects set on it, read with <see cref="Effects.Flag"/>.
/// </summary>
public static class Flags
{
    /// <summary>The flag targets, ordinal: <c>unit</c> (the effect's holder). Fixed in code; not a registry parameter.</summary>
    public static ImmutableArray<string> Targets { get; } = ["unit"];

    /// <summary>
    /// The flag names: the 30 M1-09 ladder flags (<see cref="Dungeon.Core.Damage.DamageIds.LadderFlags"/>:
    /// <c>resistant.&lt;type&gt;</c>, <c>weak.&lt;type&gt;</c> and <c>immune.&lt;type&gt;</c> per damage type; M1-09
    /// plan call 6). The other steps that read a flag (M1-13 Incapacitated, M1-14 Dodge and Hide, M1-17 Raise) add
    /// their rows; tests pass their own table. The context sorts them.
    /// </summary>
    public static ImmutableArray<string> Core { get; } = [.. Dungeon.Core.Damage.DamageIds.LadderFlags];
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
    /// The rows, ordinal by id: <c>combatStarted</c>, raised by <see cref="CombatStarted"/> (the subject is the first
    /// unit in the order); <c>turnEnded</c> and <c>turnStarted</c>, raised by <see cref="TurnEnded"/> and
    /// <see cref="TurnStarted"/> (the subject is the turn's unit; M1-10 plan call 14); <c>unitDied</c>, raised by
    /// <see cref="UnitDied"/> (the subject is the dead unit, whose effects are already gone, so only other units'
    /// triggers answer it; plan call 14); <c>unitMoved</c>, raised by
    /// <see cref="UnitMoved"/> (a unit finished a move; the subject is that unit). <c>docs/effects-vocabulary.md</c>
    /// section 6 keeps the same table.
    /// </summary>
    public static ImmutableArray<RuleEvent> Core { get; } =
        [
            RuleEvent.Of<CombatStarted>("combatStarted", e => e.Order[0].Unit),
            RuleEvent.Of<TurnEnded>("turnEnded", e => e.Unit),
            RuleEvent.Of<TurnStarted>("turnStarted", e => e.Unit),
            RuleEvent.Of<UnitDied>("unitDied", e => e.Unit),
            RuleEvent.Of<UnitMoved>("unitMoved", e => e.Unit),
        ];
}
