using System;
using System.Collections.Generic;
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
    /// The value of the derived number <paramref name="numberId"/> for <paramref name="unit"/> (<c>docs/effects-vocabulary.md</c>
    /// sections 4 and 5). <see cref="NumberRule.Stack"/> and <see cref="NumberRule.Speed"/> add to the base the sum of
    /// every non-condition <c>add</c>, plus the best condition bonus and the worst condition penalty (CO 6.7).
    /// <see cref="NumberRule.AbilityScore"/> adds the best bonus and the worst penalty across all sources (SM 5.1).
    /// <see cref="NumberRule.Speed"/> then halves once, rounding down, if any <c>halve</c> is active, is 0 if any
    /// <c>zero</c> is active, and is never below 0. A cap applies last. Throws <see cref="ArgumentException"/> for an
    /// unknown number, unit or effect definition.
    /// </summary>
    public static long Number(RulesContext rules, GameState s, UnitId unit, string numberId)
    {
        ArgumentNullException.ThrowIfNull(rules);
        var number = rules.Number(numberId);
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

        long value = number.Rule == NumberRule.AbilityScore
            ? number.Base + bonus + penalty
            : number.Base + stackSum + conditionBonus + conditionPenalty;

        if (number.Rule == NumberRule.Speed)
        {
            value = zero ? 0 : Math.Max(0, halve ? IntMath.FloorDiv(value, 2) : value);
        }

        return number.Cap is { } cap ? Math.Min(value, cap) : value;
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

        bool advantage = false;
        bool disadvantage = false;
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

        return advantage == disadvantage ? RollMode.Normal : advantage ? RollMode.Advantage : RollMode.Disadvantage;
    }

    /// <summary>
    /// True if one of <paramref name="unit"/>'s active effects sets the flag <paramref name="name"/> on
    /// <paramref name="target"/>. Throws <see cref="ArgumentException"/> for an unknown unit or effect definition.
    /// </summary>
    public static bool Flag(RulesContext rules, GameState s, UnitId unit, string target, string name)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(name);
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
