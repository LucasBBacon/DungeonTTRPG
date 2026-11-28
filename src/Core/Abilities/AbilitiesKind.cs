using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Stats;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.AbilityModel;

/// <summary>
/// The ability records, <c>abilities/abilities.json</c> (AB 6.1, 7.1, 7.3, 8.1; M1-11 plan call 1): content ids, strict JSON.
/// A record is <c>{id, kind, cost, pool?, target?, resolve?, damage?, effects?, reaction?}</c>.
/// <list type="bullet">
/// <item><c>kind</c>: <c>atWill</c>, <c>limited</c> or <c>reaction</c>; <c>passive</c>, <c>toggle</c> and <c>sustained</c> are reserved.</item>
/// <item><c>cost</c>: <c>action</c>, <c>bonus</c>, <c>free</c>, <c>none</c>, <c>reaction</c> or <c>move:N</c> (N at least 1); <c>attack</c> (M1-14), <c>onHit</c> (M1-16) and <c>onEvent:&lt;id&gt;</c> (M1-20) are reserved.</item>
/// <item><c>pool</c>: <c>{id, count}</c>, count at least 1.</item>
/// <item><c>target</c>: <c>{shape: single, range, count?}</c>, <c>{shape: burst, radius, range?}</c>, <c>{shape: cone, length}</c> or <c>{shape: line, length}</c>, each with <c>selective?</c>; required unless the kind is <c>reaction</c>, and absent for one.</item>
/// <item><c>resolve</c>: <c>{attack, trained}</c> or <c>{save, onSuccess: "none"}</c> (<c>half</c> is refused, D-22).</item>
/// <item><c>damage</c>: <c>[{type, dice?: "NdS", flat?}]</c>; <c>effects</c>: effect ids.</item>
/// <item><c>reaction</c>: <c>{trigger, bonus, policy?: {kind, filter?: "none"}}</c>, present exactly when the kind and the cost are <c>reaction</c>.</item>
/// </list>
/// Pool and effect references are checked when the rules context is built (<c>RulesContext.Create</c>).
/// </summary>
public sealed class AbilitiesKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "abilities/abilities.json";

    /// <inheritdoc />
    public override string ArrayName => "abilities";

    /// <inheritdoc />
    public override bool IdsAreContentIds => true;

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.abilities", ContentJson.Read<AbilitiesDto>(file).Abilities);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            result.Add(ReadAbility(file, Item("$.abilities", i), Need(file, Item("$.abilities", i), rows[i])));
        }

        return result.MoveToImmutable();
    }

    private static AbilityDefinition ReadAbility(ContentFile file, string at, AbilityDto a)
    {
        string id = Id(file, Member(at, "id"), a.Id).Value;
        var kind = ParseLowerEnum<AbilityKind>(file, Member(at, "kind"), a.Kind);
        if (kind is AbilityKind.Passive or AbilityKind.Toggle or AbilityKind.Sustained)
        {
            throw Fail(file, Member(at, "kind"), $"\"{a.Kind}\" is reserved; M1 abilities are atWill, limited or reaction.");
        }

        var cost = Cost(file, Member(at, "cost"), a.Cost);
        if ((kind == AbilityKind.Reaction) != (cost.Kind == CostKind.Reaction))
        {
            throw Fail(file, Member(at, "cost"), "a reaction ability, and only one, has the cost \"reaction\".");
        }

        if (kind == AbilityKind.Reaction)
        {
            if (a.Reaction is null)
            {
                throw Fail(file, Member(at, "reaction"), "a reaction ability has a reaction block.");
            }

            if (a.Target is not null)
            {
                throw Fail(file, Member(at, "target"), "a reaction ability has no target.");
            }
        }
        else
        {
            if (a.Reaction is not null)
            {
                throw Fail(file, Member(at, "reaction"), "only a reaction ability has a reaction block.");
            }

            if (a.Target is null)
            {
                throw Fail(file, Member(at, "target"), "the field is missing.");
            }
        }

        return new AbilityDefinition(
            file.Name,
            id,
            kind,
            cost,
            a.Pool is null ? null : Pool(file, Member(at, "pool"), a.Pool),
            a.Target is null ? null : Target(file, Member(at, "target"), a.Target),
            a.Resolve is null ? null : ReadResolve(file, Member(at, "resolve"), a.Resolve),
            Damage(file, Member(at, "damage"), a.Damage),
            Effects(file, Member(at, "effects"), a.Effects),
            a.Reaction is null ? null : Reaction(file, Member(at, "reaction"), a.Reaction));
    }

    private static AbilityCost Cost(ContentFile file, string field, string? text)
    {
        string s = RuleString(file, field, text);
        switch (s)
        {
            case "action": return new AbilityCost(CostKind.Action);
            case "bonus": return new AbilityCost(CostKind.Bonus);
            case "reaction": return new AbilityCost(CostKind.Reaction);
            case "free": return new AbilityCost(CostKind.Free);
            case "none": return new AbilityCost(CostKind.None);
            case "attack": throw Fail(file, field, "the \"attack\" cost is reserved to M1-14.");
            case "onHit": throw Fail(file, field, "the \"onHit\" cost is reserved to M1-16.");
        }

        if (s.StartsWith("move:", StringComparison.Ordinal))
        {
            string digits = s.Substring("move:".Length);
            if (Digits(digits, maxLength: 4) is int n && n >= 1)
            {
                return new AbilityCost(CostKind.Move, n);
            }

            throw Fail(file, field, $"\"{s}\" is not a movement cost; write move:N with N a whole number of at least 1.");
        }

        if (s.StartsWith("onEvent:", StringComparison.Ordinal) && s.Length > "onEvent:".Length)
        {
            throw Fail(file, field, "the \"onEvent:<id>\" cost is reserved to M1-20.");
        }

        throw Fail(file, field, $"unknown cost \"{s}\"; the costs are action, bonus, reaction, free, none and move:N.");
    }

    private static PoolUse Pool(ContentFile file, string at, PoolDto p)
    {
        string id = Id(file, Member(at, "id"), p.Id).Value;
        int count = Need(file, Member(at, "count"), p.Count);
        return count >= 1 ? new PoolUse(id, count) : throw Fail(file, Member(at, "count"), "a pool use spends at least 1.");
    }

    private static TargetSpec Target(ContentFile file, string at, TargetDto t)
    {
        var shape = ParseLowerEnum<TargetShape>(file, Member(at, "shape"), t.Shape);
        bool selective = t.Selective ?? false;
        switch (shape)
        {
            case TargetShape.Single:
                Forbid(file, at, "radius", t.Radius);
                Forbid(file, at, "length", t.Length);
                int range = AtLeast(file, Member(at, "range"), Need(file, Member(at, "range"), t.Range), 0);
                int count = t.Count is null ? 1 : AtLeast(file, Member(at, "count"), t.Count.Value, 1);
                return new TargetSpec(shape, range, count, 0, selective);
            case TargetShape.Burst:
                Forbid(file, at, "length", t.Length);
                Forbid(file, at, "count", t.Count);
                int radius = AtLeast(file, Member(at, "radius"), Need(file, Member(at, "radius"), t.Radius), 1);
                int burstRange = t.Range is null ? 0 : AtLeast(file, Member(at, "range"), t.Range.Value, 0);
                return new TargetSpec(shape, burstRange, 0, radius, selective);
            default:
                Forbid(file, at, "radius", t.Radius);
                Forbid(file, at, "range", t.Range);
                Forbid(file, at, "count", t.Count);
                int length = AtLeast(file, Member(at, "length"), Need(file, Member(at, "length"), t.Length), 1);
                return new TargetSpec(shape, 0, 0, length, selective);
        }
    }

    private static ResolveSpec ReadResolve(ContentFile file, string at, ResolveDto r)
    {
        if ((r.Attack is null) == (r.Save is null))
        {
            throw Fail(file, at, "a resolve has exactly one of attack and save.");
        }

        if (r.Attack is not null)
        {
            var ability = ParseLowerEnum<Ability>(file, Member(at, "attack"), r.Attack);
            bool trained = Need(file, Member(at, "trained"), r.Trained);
            if (r.OnSuccess is not null)
            {
                throw Fail(file, Member(at, "onSuccess"), "onSuccess belongs to a save.");
            }

            return new AttackResolve(new AttackSpec(ability, trained));
        }

        var save = ParseLowerEnum<Save>(file, Member(at, "save"), r.Save);
        if (r.Trained is not null)
        {
            throw Fail(file, Member(at, "trained"), "trained belongs to an attack.");
        }

        string onSuccess = RuleString(file, Member(at, "onSuccess"), r.OnSuccess);
        if (onSuccess == "half")
        {
            throw Fail(file, Member(at, "onSuccess"), "\"half\" is not supported; a save supports onSuccess \"none\" only (D-22).");
        }

        return onSuccess == "none"
            ? new SaveResolve(save)
            : throw Fail(file, Member(at, "onSuccess"), $"unknown onSuccess \"{onSuccess}\"; a save supports \"none\".");
    }

    private static ImmutableArray<DamagePart> Damage(ContentFile file, string field, DamageDto?[]? items)
    {
        if (items is null)
        {
            return ImmutableArray<DamagePart>.Empty;
        }

        var result = ImmutableArray.CreateBuilder<DamagePart>(items.Length);
        for (int i = 0; i < items.Length; i++)
        {
            string at = Item(field, i);
            var d = Need(file, at, items[i]);
            var type = ParseLowerEnum<DamageType>(file, Member(at, "type"), d.Type);
            if (d.Dice is null && d.Flat is null)
            {
                throw Fail(file, at, "a damage part has dice, a flat amount, or both.");
            }

            int dice = 0;
            int sides = 0;
            if (d.Dice is not null)
            {
                (dice, sides) = Dice(file, Member(at, "dice"), d.Dice);
            }

            result.Add(new DamagePart(type, dice, sides, d.Flat ?? 0));
        }

        return result.MoveToImmutable();
    }

    // "NdS": two whole numbers of at least 1 around a lower-case d.
    private static (int Dice, int Sides) Dice(ContentFile file, string field, string text)
    {
        string s = RuleString(file, field, text);
        int d = s.IndexOf('d', StringComparison.Ordinal);
        if (d > 0 && Digits(s.Substring(0, d), maxLength: 4) is int dice && dice >= 1
            && Digits(s.Substring(d + 1), maxLength: 4) is int sides && sides >= 1)
        {
            return (dice, sides);
        }

        throw Fail(file, field, $"\"{s}\" is not dice; write NdS, such as \"2d6\", with N and S whole numbers of at least 1.");
    }

    private static ImmutableArray<string> Effects(ContentFile file, string field, string?[]? items)
    {
        if (items is null)
        {
            return ImmutableArray<string>.Empty;
        }

        var result = ImmutableArray.CreateBuilder<string>(items.Length);
        for (int i = 0; i < items.Length; i++)
        {
            result.Add(Id(file, Item(field, i), items[i]).Value);
        }

        return result.MoveToImmutable();
    }

    private static ReactionSpec Reaction(ContentFile file, string at, ReactionDto r)
    {
        var trigger = ParseLowerEnum<ReactionTrigger>(file, Member(at, "trigger"), r.Trigger);
        int bonus = Need(file, Member(at, "bonus"), r.Bonus);
        ReactionMode? policy = null;
        if (r.Policy is { } p)
        {
            string pat = Member(at, "policy");
            policy = ParseLowerEnum<ReactionMode>(file, Member(pat, "kind"), p.Kind);
            if (p.Filter is not null && RuleString(file, Member(pat, "filter"), p.Filter) != "none")
            {
                throw Fail(file, Member(pat, "filter"), "reaction filters other than \"none\" arrive with M1-22.");
            }
        }

        return new ReactionSpec(trigger, bonus, policy);
    }

    private static void Forbid(ContentFile file, string at, string name, int? value)
    {
        if (value is not null)
        {
            throw Fail(file, Member(at, name), $"{name} does not apply to this shape.");
        }
    }

    private static int AtLeast(ContentFile file, string field, int value, int min) =>
        value >= min ? value : throw Fail(file, field, $"the value is at least {min}.");

    // A whole number written in ASCII digits only, or null.
    private static int? Digits(string s, int maxLength)
    {
        if (s.Length == 0 || s.Length > maxLength)
        {
            return null;
        }

        int n = 0;
        foreach (char c in s)
        {
            if (c is < '0' or > '9')
            {
                return null;
            }

            n = n * 10 + (c - '0');
        }

        return n;
    }

    private static ContentLoadException Fail(ContentFile file, string field, string problem) => new(file.Name, field, problem);

    private sealed class AbilitiesDto
    {
        [JsonPropertyName("abilities")] public AbilityDto?[]? Abilities { get; set; }
    }

    private sealed class AbilityDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("kind")] public string? Kind { get; set; }
        [JsonPropertyName("cost")] public string? Cost { get; set; }
        [JsonPropertyName("pool")] public PoolDto? Pool { get; set; }
        [JsonPropertyName("target")] public TargetDto? Target { get; set; }
        [JsonPropertyName("resolve")] public ResolveDto? Resolve { get; set; }
        [JsonPropertyName("damage")] public DamageDto?[]? Damage { get; set; }
        [JsonPropertyName("effects")] public string?[]? Effects { get; set; }
        [JsonPropertyName("reaction")] public ReactionDto? Reaction { get; set; }
    }

    private sealed class PoolDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("count")] public int? Count { get; set; }
    }

    private sealed class TargetDto
    {
        [JsonPropertyName("shape")] public string? Shape { get; set; }
        [JsonPropertyName("range")] public int? Range { get; set; }
        [JsonPropertyName("count")] public int? Count { get; set; }
        [JsonPropertyName("radius")] public int? Radius { get; set; }
        [JsonPropertyName("length")] public int? Length { get; set; }
        [JsonPropertyName("selective")] public bool? Selective { get; set; }
    }

    private sealed class ResolveDto
    {
        [JsonPropertyName("attack")] public string? Attack { get; set; }
        [JsonPropertyName("trained")] public bool? Trained { get; set; }
        [JsonPropertyName("save")] public string? Save { get; set; }
        [JsonPropertyName("onSuccess")] public string? OnSuccess { get; set; }
    }

    private sealed class DamageDto
    {
        [JsonPropertyName("type")] public string? Type { get; set; }
        [JsonPropertyName("dice")] public string? Dice { get; set; }
        [JsonPropertyName("flat")] public int? Flat { get; set; }
    }

    private sealed class ReactionDto
    {
        [JsonPropertyName("trigger")] public string? Trigger { get; set; }
        [JsonPropertyName("bonus")] public int? Bonus { get; set; }
        [JsonPropertyName("policy")] public PolicyDto? Policy { get; set; }
    }

    private sealed class PolicyDto
    {
        [JsonPropertyName("kind")] public string? Kind { get; set; }
        [JsonPropertyName("filter")] public string? Filter { get; set; }
    }
}
