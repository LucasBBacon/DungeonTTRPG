using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Content;

namespace Dungeon.Core.Effects;

/// <summary>
/// The fold queries over a state's active effects, and adding and removing instances. Nothing derived is stored: each
/// number is folded on demand from its base and the holder's instances (SM 6, 9). The folds are pure and make no draws,
/// so the display's "what would +1 change?" is the same fold on a hypothetical state from <see cref="Apply"/>. They read
/// the instances whose holder is the unit, in seq order, so the order of <see cref="GameState.Effects"/> never matters.
/// </summary>
public static class Effects
{
    /// <summary>
    /// The most triggers one command may fire (M1-07 plan call 7). Firing one more throws
    /// <see cref="TriggerLoopException"/>, so a trigger chain that does not end is a defect caught by tests.
    /// </summary>
    public const int MaxTriggerFirings = 64;

    /// <summary>
    /// The value of the derived number <paramref name="numberId"/> for <paramref name="unit"/> (<c>docs/effects-vocabulary.md</c>
    /// sections 4 and 5), from the number's base for that unit (<see cref="DerivedNumber.Base"/>).
    /// <see cref="NumberRule.Stack"/> and <see cref="NumberRule.Speed"/> add to the base the sum of every non-condition
    /// <c>add</c>, plus the best condition bonus and the worst condition penalty (CO 6.7).
    /// <see cref="NumberRule.AbilityScore"/> adds the best bonus across all sources, caps that, then adds the worst
    /// penalty across all sources (SM 5.1). <see cref="NumberRule.Speed"/> then halves once, rounding down, if any
    /// <c>halve</c> is active, is 0 if any <c>zero</c> is active, and is never below 0. Throws
    /// <see cref="ArgumentException"/> for an unknown number, unit or effect definition, and
    /// <see cref="InvalidOperationException"/> naming the path if the number's base reads itself through other bases.
    /// </summary>
    public static long Number(RulesContext rules, GameState s, UnitId unit, string numberId) =>
        Fold(rules, s, unit, numberId, ImmutableArray<string>.Empty);

    // Number, with the ids of the numbers whose bases are being computed, outermost first, to catch a cycle.
    internal static long Fold(RulesContext rules, GameState s, UnitId unit, string numberId, ImmutableArray<string> path)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var number = rules.Number(numberId);
        var holder = s.FindUnit(unit) ?? throw new ArgumentException($"Unit {unit.Value} is not in the state.", nameof(unit));
        var inner = path.Add(number.Id);
        if (path.Contains(number.Id, StringComparer.Ordinal))
        {
            throw new InvalidOperationException(
                $"The bases of the derived numbers read each other in a cycle: {string.Join(" -> ", inner)}.");
        }

        long numberBase = number.Base(new NumberBaseArgs(rules, s, holder, inner));
        long stackSum = 0;
        long conditionBonus = 0;
        long conditionPenalty = 0;
        long bonus = 0;
        long penalty = 0;
        bool halve = false;
        bool zero = false;
        foreach (var definition in Held(rules, s, unit))
        {
            foreach (var m in definition.Modifiers)
            {
                if (!string.Equals(m.Target, numberId, StringComparison.Ordinal))
                {
                    continue;
                }

                switch (m.Op)
                {
                    case ModifierOp.Add:
                        bonus = Math.Max(bonus, m.Value);
                        penalty = Math.Min(penalty, m.Value);
                        if (definition.Kind == EffectSourceKind.Condition)
                        {
                            conditionBonus = Math.Max(conditionBonus, m.Value);
                            conditionPenalty = Math.Min(conditionPenalty, m.Value);
                        }
                        else
                        {
                            stackSum += m.Value;
                        }

                        break;
                    case ModifierOp.Halve:
                        halve = true;
                        break;
                    case ModifierOp.Zero:
                        zero = true;
                        break;
                }
            }
        }

        if (number.Rule == NumberRule.AbilityScore)
        {
            long raised = numberBase + bonus;
            return (number.Cap is { } cap ? Math.Min(raised, cap) : raised) + penalty;
        }

        long value = numberBase + stackSum + conditionBonus + conditionPenalty;
        if (number.Rule == NumberRule.Speed)
        {
            value = zero ? 0 : Math.Max(0, halve ? IntMath.FloorDiv(value, 2) : value);
        }

        return value;
    }

    /// <summary>
    /// The mode of <paramref name="unit"/>'s rolls of <paramref name="rollTarget"/>, folded by D-15: any advantage and
    /// any disadvantage give <see cref="RollMode.Normal"/>, whatever the counts; otherwise whichever is present;
    /// otherwise <see cref="RollMode.Normal"/>. Throws <see cref="ArgumentException"/> for an unknown roll target, unit
    /// or effect definition.
    /// </summary>
    public static RollMode RollModeOf(RulesContext rules, GameState s, UnitId unit, string rollTarget)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(rollTarget);
        if (!rules.IsRollTarget(rollTarget))
        {
            throw new ArgumentException($"No roll target \"{rollTarget}\".", nameof(rollTarget));
        }

        return RollModeOf(rules, s, [(unit, rollTarget)]);
    }

    /// <summary>
    /// The mode of one roll that several (unit, roll target) pairs bear on, such as an attack, which reads the
    /// attacker's <c>attackRoll</c> and the defender's <c>attacksAgainst</c>: any advantage and any disadvantage over
    /// all the pairs, then D-15 once. Folding each pair first would be wrong, as the attacker's cancelled mode and the
    /// defender's disadvantage would then no longer cancel. Throws <see cref="ArgumentException"/> for no sources, an
    /// unknown roll target, unit or effect definition.
    /// </summary>
    public static RollMode RollModeOf(RulesContext rules, GameState s, IEnumerable<(UnitId Unit, string Target)> sources)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(sources);
        var pairs = sources.ToList();
        if (pairs.Count == 0)
        {
            throw new ArgumentException("A roll mode needs at least one source.", nameof(sources));
        }

        bool advantage = false;
        bool disadvantage = false;
        foreach (var (unit, rollTarget) in pairs)
        {
            if (rollTarget is null || !rules.IsRollTarget(rollTarget))
            {
                throw new ArgumentException($"No roll target \"{rollTarget}\".", nameof(sources));
            }

            foreach (var definition in Held(rules, s, unit))
            {
                foreach (var m in definition.Modifiers)
                {
                    if (m.Op == ModifierOp.RollMode && string.Equals(m.Target, rollTarget, StringComparison.Ordinal))
                    {
                        advantage |= m.Mode == RollMode.Advantage;
                        disadvantage |= m.Mode == RollMode.Disadvantage;
                    }
                }
            }
        }

        return advantage == disadvantage ? RollMode.Normal : advantage ? RollMode.Advantage : RollMode.Disadvantage;
    }

    /// <summary>
    /// True if one of <paramref name="unit"/>'s active effects sets the flag <paramref name="name"/> on
    /// <paramref name="target"/>. Throws <see cref="ArgumentException"/> for an unknown flag target or flag name, unit
    /// or effect definition.
    /// </summary>
    public static bool Flag(RulesContext rules, GameState s, UnitId unit, string target, string name)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(name);
        if (!rules.IsFlag(target, name))
        {
            throw new ArgumentException($"No flag \"{name}\" on target \"{target}\".", nameof(name));
        }

        foreach (var definition in Held(rules, s, unit))
        {
            foreach (var m in definition.Modifiers)
            {
                if (m.Op == ModifierOp.Flag
                    && string.Equals(m.Target, target, StringComparison.Ordinal)
                    && string.Equals(m.FlagName, name, StringComparison.Ordinal))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// <paramref name="s"/> with a new instance of <paramref name="effectId"/> held by <paramref name="holder"/>. Its seq
    /// is <see cref="GameState.NextEffectSeq"/>, which is incremented (checked, so it is never reused). Throws
    /// <see cref="ArgumentException"/> if <paramref name="effectId"/> is not a content id or the holder or source unit
    /// is not in the state. The definition is not looked up: a state does not carry the content.
    /// </summary>
    public static GameState Apply(GameState s, string effectId, UnitId holder, EffectSource source)
    {
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(effectId);
        if (!ContentId.IsValid(effectId))
        {
            throw new ArgumentException($"\"{effectId}\" is not a content id.", nameof(effectId));
        }

        if (s.FindUnit(holder) is null)
        {
            throw new ArgumentException($"Holder unit {holder.Value} is not in the state.", nameof(holder));
        }

        if (source.Unit is { } from && s.FindUnit(from) is null)
        {
            throw new ArgumentException($"Source unit {from.Value} is not in the state.", nameof(source));
        }

        long seq = s.NextEffectSeq;
        return s with
        {
            Effects = s.Effects.Add(new EffectInstance(seq, effectId, holder, source)),
            NextEffectSeq = checked(seq + 1),
        };
    }

    /// <summary>
    /// <paramref name="s"/> without the instance <paramref name="seq"/>. <see cref="GameState.NextEffectSeq"/> is kept,
    /// so the seq is never reused. Throws <see cref="ArgumentException"/> if no instance has that seq.
    /// </summary>
    public static GameState Remove(GameState s, long seq)
    {
        ArgumentNullException.ThrowIfNull(s);
        for (int i = 0; i < s.Effects.Length; i++)
        {
            if (s.Effects[i].Seq == seq)
            {
                return s with { Effects = s.Effects.RemoveAt(i) };
            }
        }

        throw new ArgumentException($"No active effect has seq {seq}.", nameof(seq));
    }

    // The definitions of the unit's instances, in seq order.
    private static IEnumerable<EffectDefinition> Held(RulesContext rules, GameState s, UnitId unit)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.FindUnit(unit) is null)
        {
            throw new ArgumentException($"Unit {unit.Value} is not in the state.", nameof(unit));
        }

        return s.Effects
            .Where(e => e.Holder == unit)
            .OrderBy(e => e.Seq)
            .Select(e => rules.Definition(e.EffectId))
            .ToList();
    }
}
