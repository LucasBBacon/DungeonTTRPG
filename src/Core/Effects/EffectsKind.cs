using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Effects;

/// <summary>
/// The effect definitions, <c>effects/effects.json</c> (MAIN 6.4; <c>docs/effects-vocabulary.md</c>): records
/// <c>{id, kind, modifiers?, triggers?}</c> with content ids. A modifier is <c>{target}</c> plus exactly one operation key
/// (<c>add</c>, <c>halve</c>, <c>zero</c>, <c>rollMode</c> or <c>flag</c>); <c>diceBonus</c> is reserved and refused. A
/// trigger is <c>{on, priority?, scope, do[]}</c>, and each action is <c>{applyEffect, to}</c> or <c>{hook, args?}</c>.
/// <see cref="Resolve"/> checks that every <c>applyEffect</c> names an effect of this file; number, roll-target, event
/// and hook ids are checked against the code registries when the rules context is built.
/// </summary>
public sealed class EffectsKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "effects/effects.json";

    /// <inheritdoc />
    public override string ArrayName => "effects";

    /// <inheritdoc />
    public override bool IdsAreContentIds => true;

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.effects", ContentJson.Read<EffectsDto>(file).Effects);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.effects", i);
            var e = Need(file, at, rows[i]);
            result.Add(new EffectDefinition(
                file.Name,
                Id(file, Member(at, "id"), e.Id).Value,
                ParseLowerEnum<EffectSourceKind>(file, Member(at, "kind"), e.Kind),
                Modifiers(file, Member(at, "modifiers"), e.Modifiers),
                Triggers(file, Member(at, "triggers"), e.Triggers)));
        }

        return result.MoveToImmutable();
    }

    /// <inheritdoc />
    public override void Resolve(ContentSet set)
    {
        var rows = set.RecordsIn(FileName);
        for (int i = 0; i < rows.Length; i++)
        {
            var effect = (EffectDefinition)rows[i];
            for (int t = 0; t < effect.Triggers.Length; t++)
            {
                var actions = effect.Triggers[t].Do;
                string field = Member(Item(Member(Item("$.effects", i), "triggers"), t), "do");
                for (int a = 0; a < actions.Length; a++)
                {
                    if (actions[a].ApplyEffect is { } id && set.Find<EffectDefinition>(id) is null)
                    {
                        throw new ContentLoadException(FileName, Member(Item(field, a), "applyEffect"), $"\"{id}\" names no effect.");
                    }
                }
            }
        }
    }

    private static ImmutableArray<Modifier> Modifiers(ContentFile file, string field, ModifierDto?[]? items)
    {
        if (items is null)
        {
            return ImmutableArray<Modifier>.Empty;
        }

        var result = ImmutableArray.CreateBuilder<Modifier>(items.Length);
        for (int j = 0; j < items.Length; j++)
        {
            string at = Item(field, j);
            var m = Need(file, at, items[j]);
            string target = RuleString(file, Member(at, "target"), m.Target);
            if (m.DiceBonus.ValueKind != JsonValueKind.Undefined)
            {
                throw new ContentLoadException(
                    file.Name, Member(at, "diceBonus"), "diceBonus is reserved until dice exist (M1-09 or later).");
            }

            int ops = (m.Add is null ? 0 : 1) + (m.Halve is null ? 0 : 1) + (m.Zero is null ? 0 : 1)
                + (m.RollMode is null ? 0 : 1) + (m.Flag is null ? 0 : 1);
            if (ops != 1)
            {
                throw new ContentLoadException(
                    file.Name, at, $"a modifier has exactly one of add, halve, zero, rollMode and flag; this one has {ops}.");
            }

            result.Add(m switch
            {
                { Add: { } add } => new Modifier(target, ModifierOp.Add, add, RollMode.Normal, null),
                { Halve: { } halve } => new Modifier(target, True(file, Member(at, "halve"), halve, ModifierOp.Halve), 0, RollMode.Normal, null),
                { Zero: { } zero } => new Modifier(target, True(file, Member(at, "zero"), zero, ModifierOp.Zero), 0, RollMode.Normal, null),
                { RollMode: { } mode } => new Modifier(target, ModifierOp.RollMode, 0, GivenMode(file, Member(at, "rollMode"), mode), null),
                _ => new Modifier(target, ModifierOp.Flag, 0, RollMode.Normal, RuleString(file, Member(at, "flag"), m.Flag)),
            });
        }

        return result.MoveToImmutable();
    }

    // halve and zero carry no amount: the key is written with the value true.
    private static ModifierOp True(ContentFile file, string field, bool value, ModifierOp op) =>
        value ? op : throw new ContentLoadException(file.Name, field, "the value is true; leave the key out instead of writing false.");

    // A modifier gives advantage or disadvantage; normal is only ever a fold result.
    private static RollMode GivenMode(ContentFile file, string field, string text)
    {
        var mode = ParseLowerEnum<RollMode>(file, field, text);
        return mode != RollMode.Normal
            ? mode
            : throw new ContentLoadException(file.Name, field, "a modifier gives \"advantage\" or \"disadvantage\".");
    }

    private static ImmutableArray<Trigger> Triggers(ContentFile file, string field, TriggerDto?[]? items)
    {
        if (items is null)
        {
            return ImmutableArray<Trigger>.Empty;
        }

        var result = ImmutableArray.CreateBuilder<Trigger>(items.Length);
        for (int j = 0; j < items.Length; j++)
        {
            string at = Item(field, j);
            var t = Need(file, at, items[j]);
            result.Add(new Trigger(
                RuleString(file, Member(at, "on"), t.On),
                t.Priority ?? 0,
                ParseLowerEnum<TriggerScope>(file, Member(at, "scope"), t.Scope),
                Actions(file, Member(at, "do"), t.Do)));
        }

        return result.MoveToImmutable();
    }

    private static ImmutableArray<TriggerAction> Actions(ContentFile file, string field, ActionDto?[]? items)
    {
        var array = Need(file, field, items);
        if (array.Length == 0)
        {
            throw new ContentLoadException(file.Name, field, "a trigger does at least one thing.");
        }

        var result = ImmutableArray.CreateBuilder<TriggerAction>(array.Length);
        for (int j = 0; j < array.Length; j++)
        {
            string at = Item(field, j);
            var a = Need(file, at, array[j]);
            if ((a.ApplyEffect is null) == (a.Hook is null))
            {
                throw new ContentLoadException(file.Name, at, "an action has exactly one of applyEffect and hook.");
            }

            if (a.ApplyEffect is not null)
            {
                if (a.Args is not null)
                {
                    throw new ContentLoadException(file.Name, Member(at, "args"), "args belong to a hook action.");
                }

                result.Add(new TriggerAction(
                    Id(file, Member(at, "applyEffect"), a.ApplyEffect).Value,
                    ParseLowerEnum<TriggerTarget>(file, Member(at, "to"), a.To),
                    null,
                    ImmutableArray<string>.Empty));
            }
            else
            {
                if (a.To is not null)
                {
                    throw new ContentLoadException(file.Name, Member(at, "to"), "to belongs to an applyEffect action.");
                }

                result.Add(new TriggerAction(
                    null,
                    TriggerTarget.Holder,
                    RuleString(file, Member(at, "hook"), a.Hook),
                    a.Args is null ? ImmutableArray<string>.Empty : RuleStrings(file, Member(at, "args"), a.Args)));
            }
        }

        return result.MoveToImmutable();
    }

    private sealed class EffectsDto
    {
        [JsonPropertyName("effects")] public EffectDto?[]? Effects { get; set; }
    }

    private sealed class EffectDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("kind")] public string? Kind { get; set; }
        [JsonPropertyName("modifiers")] public ModifierDto?[]? Modifiers { get; set; }
        [JsonPropertyName("triggers")] public TriggerDto?[]? Triggers { get; set; }
    }

    private sealed class ModifierDto
    {
        [JsonPropertyName("target")] public string? Target { get; set; }
        [JsonPropertyName("add")] public long? Add { get; set; }
        [JsonPropertyName("halve")] public bool? Halve { get; set; }
        [JsonPropertyName("zero")] public bool? Zero { get; set; }
        [JsonPropertyName("rollMode")] public string? RollMode { get; set; }
        [JsonPropertyName("flag")] public string? Flag { get; set; }

        // Reserved: any value, null included, is refused, so the key is captured whatever it holds.
        [JsonPropertyName("diceBonus")] public JsonElement DiceBonus { get; set; }
    }

    private sealed class TriggerDto
    {
        [JsonPropertyName("on")] public string? On { get; set; }
        [JsonPropertyName("priority")] public int? Priority { get; set; }
        [JsonPropertyName("scope")] public string? Scope { get; set; }
        [JsonPropertyName("do")] public ActionDto?[]? Do { get; set; }
    }

    private sealed class ActionDto
    {
        [JsonPropertyName("applyEffect")] public string? ApplyEffect { get; set; }
        [JsonPropertyName("to")] public string? To { get; set; }
        [JsonPropertyName("hook")] public string? Hook { get; set; }
        [JsonPropertyName("args")] public string?[]? Args { get; set; }
    }
}
