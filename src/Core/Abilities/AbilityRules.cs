using System;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.AbilityModel;

/// <summary>
/// The structural rules every ability definition meets, whether loaded from <c>abilities/abilities.json</c> or built
/// in code (M1-11 final review C5). <see cref="AbilitiesKind"/> runs them on each record it reads, and
/// <c>RulesContext.Create</c> runs them on every definition it holds, so a code-built definition is refused at
/// <c>$.abilities[id]</c> just as a record is at <c>$.abilities[i]</c>:
/// <list type="bullet">
/// <item>the kind is <c>atWill</c>, <c>limited</c> or <c>reaction</c> (the rest are reserved);</item>
/// <item>the cost is an M1 cost (<c>attack</c>, <c>onHit</c> and <c>onEvent</c> are reserved), <c>move:N</c> has N at
/// least 1, and no other cost carries a move count or an event id;</item>
/// <item>a reaction ability, and only one, has the <c>reaction</c> cost and a reaction block; a target is required
/// unless the ability is a reaction, and absent for one;</item>
/// <item>a pool use spends at least 1;</item>
/// <item>a single target has range at least 0, count at least 1 and no size; a burst has radius at least 1, range at
/// least 0 and no count; a cone or line has length at least 1 and no range or count;</item>
/// <item>a resolve is an attack or a save with <c>onSuccess: "none"</c> (the only save form a
/// <see cref="SaveResolve"/> can express; <c>onSuccess: half</c> is ruled (D-22, M1-11 rulings record 1) and built in
/// the D-22 step, written to the canonical form only when not <c>none</c> (lead's call));</item>
/// <item>the damage and effects lists are not default; a damage part has dice and sides both at least 1 or both 0.</item>
/// </list>
/// Pool and effect references, the preview bound and the AB 14.5 static checks are separate.
/// </summary>
internal static class AbilityRules
{
    /// <summary>The first rule <paramref name="ability"/> breaks, as a JSON path under <paramref name="at"/> and a problem; or null.</summary>
    internal static (string Field, string Problem)? Check(AbilityDefinition ability, string at)
    {
        ArgumentNullException.ThrowIfNull(ability);
        string kindAt = Member(at, "kind");
        if (!Enum.IsDefined(ability.Kind))
        {
            return (kindAt, $"unknown kind {(int)ability.Kind}; M1 abilities are atWill, limited or reaction.");
        }

        if (ability.Kind is AbilityKind.Passive or AbilityKind.Toggle or AbilityKind.Sustained)
        {
            return (kindAt, $"\"{LowerFirst(ability.Kind.ToString())}\" is reserved; M1 abilities are atWill, limited or reaction.");
        }

        if (CheckCost(ability.Cost) is { } costProblem)
        {
            return (Member(at, "cost"), costProblem);
        }

        bool reaction = ability.Kind == AbilityKind.Reaction;
        if (reaction != (ability.Cost.Kind == CostKind.Reaction))
        {
            return (Member(at, "cost"), "a reaction ability, and only one, has the cost \"reaction\".");
        }

        if (reaction)
        {
            if (ability.Reaction is null)
            {
                return (Member(at, "reaction"), "a reaction ability has a reaction block.");
            }

            if (ability.Target is not null)
            {
                return (Member(at, "target"), "a reaction ability has no target.");
            }
        }
        else
        {
            if (ability.Reaction is not null)
            {
                return (Member(at, "reaction"), "only a reaction ability has a reaction block.");
            }

            if (ability.Target is null)
            {
                return (Member(at, "target"), "the field is missing.");
            }
        }

        if (ability.Pool is { } pool && pool.Count < 1)
        {
            return (Member(Member(at, "pool"), "count"), "a pool use spends at least 1.");
        }

        if (ability.Target is { } target && CheckTarget(target, Member(at, "target")) is { } targetProblem)
        {
            return targetProblem;
        }

        if (ability.Resolve is not (null or AttackResolve or SaveResolve))
        {
            return (Member(at, "resolve"), "a resolve is an attack roll or a save with onSuccess \"none\".");
        }

        if (ability.Damage.IsDefault)
        {
            return (Member(at, "damage"), "the damage list is default; give an empty list instead.");
        }

        if (ability.Effects.IsDefault)
        {
            return (Member(at, "effects"), "the effects list is default; give an empty list instead.");
        }

        for (int i = 0; i < ability.Damage.Length; i++)
        {
            var part = ability.Damage[i];
            if (!(part.Dice == 0 && part.Sides == 0) && !(part.Dice >= 1 && part.Sides >= 1))
            {
                return (Member(Item(Member(at, "damage"), i), "dice"), "dice and sides are both at least 1, or both 0 for a flat part.");
            }
        }

        if (ability.Reaction is { } block && (!Enum.IsDefined(block.Trigger) || (block.Policy is { } p && !Enum.IsDefined(p))))
        {
            return (Member(at, "reaction"), "the reaction's trigger or policy is not a defined value.");
        }

        return null;
    }

    private static string? CheckCost(AbilityCost cost)
    {
        switch (cost.Kind)
        {
            case CostKind.Attack: return "the \"attack\" cost is reserved to M1-14.";
            case CostKind.OnHit: return "the \"onHit\" cost is reserved to M1-16.";
            case CostKind.OnEvent: return "the \"onEvent:<id>\" cost is reserved to M1-20.";
            case CostKind.Move:
                return cost.Move >= 1 && cost.Event is null ? null : "a movement cost is move:N with N a whole number of at least 1.";
            case CostKind.Action or CostKind.Bonus or CostKind.Reaction or CostKind.Free or CostKind.None:
                return cost.Move == 0 && cost.Event is null ? null : "only move:N carries a count, and only onEvent an event id.";
            default:
                return $"unknown cost {(int)cost.Kind}; the costs are action, bonus, reaction, free, none and move:N.";
        }
    }

    private static (string Field, string Problem)? CheckTarget(TargetSpec t, string at)
    {
        switch (t.Shape)
        {
            case TargetShape.Single:
                if (t.Size != 0)
                {
                    return (Member(at, "radius"), "radius and length do not apply to a single target.");
                }

                if (t.Range < 0)
                {
                    return (Member(at, "range"), "the value is at least 0.");
                }

                return t.Count < 1 ? (Member(at, "count"), "the value is at least 1.") : null;
            case TargetShape.Burst:
                if (t.Count != 0)
                {
                    return (Member(at, "count"), "count does not apply to this shape.");
                }

                if (t.Size < 1)
                {
                    return (Member(at, "radius"), "the value is at least 1.");
                }

                return t.Range < 0 ? (Member(at, "range"), "the value is at least 0.") : null;
            case TargetShape.Cone or TargetShape.Line:
                if (t.Range != 0)
                {
                    return (Member(at, "range"), "range does not apply to this shape.");
                }

                if (t.Count != 0)
                {
                    return (Member(at, "count"), "count does not apply to this shape.");
                }

                return t.Size < 1 ? (Member(at, "length"), "the value is at least 1.") : null;
            default:
                return (Member(at, "shape"), $"unknown shape {(int)t.Shape}; the shapes are single, burst, cone and line.");
        }
    }

    private static string LowerFirst(string s) => s.Length == 0 ? s : char.ToLowerInvariant(s[0]) + s.Substring(1);
}
