using System.Collections.Immutable;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;

namespace Dungeon.Core.Effects;

/// <summary>
/// Where an effect comes from. The kind picks the stacking regime (M1-07 plan call 1): <see cref="Condition"/>
/// modifiers use the same-number rule (best bonus plus worst penalty, CO 6.7), every other kind sums (SM 9).
/// Written in lower case in JSON (<c>"condition"</c>, <c>"trait"</c>, ...).
/// </summary>
public enum EffectSourceKind
{
    /// <summary>A condition (CO): the same-number rule applies.</summary>
    Condition,

    /// <summary>A species, class or feat trait: stacks.</summary>
    Trait,

    /// <summary>Worn or wielded gear: stacks.</summary>
    Gear,

    /// <summary>A used or carried item: stacks.</summary>
    Item,

    /// <summary>An ability's lasting effect: stacks.</summary>
    Ability,

    /// <summary>The terrain or the room, with no source unit: stacks, and its triggers fire after every unit's.</summary>
    Environment,
}

/// <summary>What a <see cref="Modifier"/> does to its target (M1-07 plan call 2). <c>diceBonus</c> is reserved and refused at load.</summary>
public enum ModifierOp
{
    /// <summary>Adds the signed <see cref="Modifier.Value"/>.</summary>
    Add,

    /// <summary>Halves the number after the flat sum, rounding down; at most one halving applies.</summary>
    Halve,

    /// <summary>Sets the number to 0, overriding every other modifier.</summary>
    Zero,

    /// <summary>Gives a roll advantage or disadvantage (<see cref="Modifier.Mode"/>), folded by D-15.</summary>
    RollMode,

    /// <summary>Sets the named boolean <see cref="Modifier.FlagName"/>.</summary>
    Flag,
}

/// <summary>A roll's mode. A modifier gives <see cref="Advantage"/> or <see cref="Disadvantage"/>; <see cref="Normal"/> is the fold of none, or of one of each (D-15).</summary>
public enum RollMode
{
    /// <summary>One d20.</summary>
    Normal,

    /// <summary>Two d20, the higher counts.</summary>
    Advantage,

    /// <summary>Two d20, the lower counts.</summary>
    Disadvantage,
}

/// <summary>Which event subjects fire a trigger: <see cref="Holder"/> only when the subject is the effect's holder, <see cref="Any"/> for every subject.</summary>
public enum TriggerScope
{
    /// <summary>The event's subject unit is the effect's holder.</summary>
    Holder,

    /// <summary>Any subject.</summary>
    Any,
}

/// <summary>Who an <c>applyEffect</c> action gives the effect to: the triggering effect's holder, or the event's subject.</summary>
public enum TriggerTarget
{
    /// <summary>The holder of the effect whose trigger fired.</summary>
    Holder,

    /// <summary>The event's subject unit.</summary>
    Subject,
}

/// <summary>
/// One modifier: it changes a number, a roll's mode or a flag (MAIN 6.4). <see cref="Target"/> names a derived
/// number, a roll target or a flag target; it is checked against the code registries when the rules context is built.
/// <see cref="Value"/> is used by <see cref="ModifierOp.Add"/> only (0 otherwise), <see cref="Mode"/> by
/// <see cref="ModifierOp.RollMode"/> only (<see cref="RollMode.Normal"/> otherwise), and <see cref="FlagName"/> by
/// <see cref="ModifierOp.Flag"/> only (null otherwise).
/// </summary>
public sealed record Modifier(string Target, ModifierOp Op, long Value, RollMode Mode, string? FlagName) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(5);
        w.String(0, Target);
        w.Enum(1, Op);
        w.Int64(2, Value);
        w.Enum(3, Mode);
        w.OptionalString(4, FlagName);
        w.EndRecord();
    }
}

/// <summary>
/// One step of a trigger: exactly one of <see cref="ApplyEffect"/> (an effect id of <c>effects/effects.json</c>, given
/// to <see cref="To"/>) or <see cref="Hook"/> (a C# hook id, called with <see cref="Args"/>) is set. <see cref="To"/>
/// is <see cref="TriggerTarget.Holder"/> and <see cref="Args"/> is empty on the form that does not use them.
/// </summary>
public sealed record TriggerAction(string? ApplyEffect, TriggerTarget To, string? Hook, ImmutableArray<string> Args) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(4);
        w.OptionalString(0, ApplyEffect);
        w.Enum(1, To);
        w.OptionalString(2, Hook);
        w.List(3, Args, (iw, s) => iw.String(0, s));
        w.EndRecord();
    }
}

/// <summary>
/// One trigger: when the rule event <see cref="On"/> is raised (for a subject <see cref="Scope"/> admits), run
/// <see cref="Do"/> in order (MAIN 6.4; AB 14.4). Lower <see cref="Priority"/> fires first; ties go to source unit
/// order, then effect creation order (plan calls 3 and 4).
/// </summary>
public sealed record Trigger(string On, int Priority, TriggerScope Scope, ImmutableArray<TriggerAction> Do) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(4);
        w.String(0, On);
        w.Int32(1, Priority);
        w.Enum(2, Scope);
        w.List(3, Do, (iw, a) => iw.Record(0, a));
        w.EndRecord();
    }
}

/// <summary>
/// An effect definition from <c>effects/effects.json</c>: a content id, the source kind that sets its stacking regime,
/// its modifiers and its triggers, each in file order. See <c>docs/effects-vocabulary.md</c>.
/// </summary>
public sealed record EffectDefinition(
    string File,
    string Id,
    EffectSourceKind Kind,
    ImmutableArray<Modifier> Modifiers,
    ImmutableArray<Trigger> Triggers) : ContentRecord(File, Id)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(4);
        w.String(0, Id);
        w.Enum(1, Kind);
        w.List(2, Modifiers, (iw, m) => iw.Record(0, m));
        w.List(3, Triggers, (iw, t) => iw.Record(0, t));
        w.EndRecord();
    }
}
