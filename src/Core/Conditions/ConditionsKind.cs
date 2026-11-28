using System;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Conditions;

/// <summary>
/// The condition records, <c>conditions/conditions.json</c> (CO 17.1; M1-12a plan calls 3 and 4): content ids, strict
/// JSON. A record is <c>{id, polarity, tier?, tags?, family?, denies?, duration?, outOfCombat?, intensity?, flags?, cures?,
/// cureImmune?, tick?, modifiers?, triggers?}</c>.
/// <list type="bullet">
/// <item><c>polarity</c>: <c>harmful</c>, <c>beneficial</c> or <c>token</c>; <c>tier</c> (<c>token</c>, <c>minor</c>,
/// <c>major</c>, <c>hard</c>) is required on a harmful condition and refused on any other.</item>
/// <item><c>duration</c> and <c>outOfCombat</c>: lists of terms, each an object with exactly one key: <c>{"turns": N}</c>,
/// <c>{"untilSourceTurnStart": true}</c>, <c>{"untilSourceTurnEnd": true}</c>, <c>{"saveEnds": {save, cap, graceTurns?}}</c>,
/// <c>{"charges": {count, on, fallback}}</c>, <c>{"decay": true}</c>, <c>{"trigger": {event, cap?}}</c>,
/// <c>{"sustained": {max}}</c>, <c>{"staged": {stages, save, cap, then?}}</c>, <c>{"untilRest": true}</c>,
/// <c>{"untilCamp": true}</c>. Saves are written <c>fortitude</c>, <c>reflex</c>, <c>will</c>. A harmful condition needs
/// a duration; <c>rounds</c> is refused (CO 5.1).</item>
/// <item><c>intensity</c>: <c>{kind, valueCap?, levelCap?, stages?, noRefresh?}</c>, binary when absent.</item>
/// <item><c>denies</c>: <c>{action?, bonus?, oneOf?}</c>; <c>flags</c>: <c>{combatOnly?, sourceTied?, turnPartGroup?,
/// noProne?, combatStartEnds?}</c>; each flag is written <c>true</c> or left out.</item>
/// <item><c>tick</c>: <c>{damageType}</c>, on a harmful condition only; <c>cures</c> and <c>cureImmune</c>: rules strings.</item>
/// <item><c>modifiers</c> and <c>triggers</c>: the M1-07 shapes of <c>effects/effects.json</c>.</item>
/// </list>
/// Refused at load, with the path: <c>relations</c> and <c>implies</c> (M1-12b), <c>ai</c> and <c>controlRank</c> (M1-23),
/// <c>boss</c> (M1-24), <c>display</c> (M1-25), <c>extraSave</c> and <c>effects</c>; a <c>saveEnds</c> or <c>staged</c>
/// term without <c>combatOnly</c> (out-of-combat repeat saves are D-23's); and a breach of the CO 5.3 bands
/// (<see cref="ConditionRules"/>), checked on the enemy-facing side, the most lenient, and of the CO 5.5 out-of-combat
/// limit. <see cref="Resolve"/> checks that a trigger's <c>applyEffect</c> names an effect or a condition and a
/// <c>staged</c> term's <c>then</c> a condition; rule-event ids, modifier targets and hooks are checked when the rules
/// context is built, which also refuses any <c>applyEffect</c> naming a condition until M1-12b.
/// </summary>
public sealed class ConditionsKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "conditions/conditions.json";

    /// <inheritdoc />
    public override string ArrayName => "conditions";

    /// <inheritdoc />
    public override bool IdsAreContentIds => true;

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.conditions", ContentJson.Read<ConditionsDto>(file).Conditions);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.conditions", i);
            result.Add(ReadCondition(file, at, Need(file, at, rows[i])));
        }

        return result.MoveToImmutable();
    }

    /// <inheritdoc />
    public override void Resolve(ContentSet set)
    {
        var rows = set.RecordsIn(FileName);
        for (int i = 0; i < rows.Length; i++)
        {
            var condition = (ConditionDefinition)rows[i];
            string at = Item("$.conditions", i);
            for (int t = 0; t < condition.Triggers.Length; t++)
            {
                var actions = condition.Triggers[t].Do;
                string field = Member(Item(Member(at, "triggers"), t), "do");
                for (int a = 0; a < actions.Length; a++)
                {
                    if (actions[a].ApplyEffect is { } id && !EffectsKind.NamesEffectOrCondition(set, id))
                    {
                        throw new ContentLoadException(FileName, Member(Item(field, a), "applyEffect"), $"\"{id}\" names no effect.");
                    }
                }
            }

            CheckThen(set, condition.Duration, Member(at, "duration"));
            CheckThen(set, condition.OutOfCombat, Member(at, "outOfCombat"));
        }
    }

    // Every staged term's then names a condition record or a core definition.
    private void CheckThen(ContentSet set, ImmutableArray<DurationTerm> terms, string at)
    {
        for (int k = 0; k < terms.Length; k++)
        {
            string termAt = Item(at, k);
            var term = terms[k];
            if (term is DurationTerm.Charges charges)
            {
                term = charges.Fallback;
                termAt = Member(Member(termAt, "charges"), "fallback");
            }

            if (term is DurationTerm.Staged { Then: { } then }
                && set.Find<ConditionDefinition>(then) is null && !Conditions.IsCoreId(then))
            {
                throw new ContentLoadException(FileName, Member(Member(termAt, "staged"), "then"), $"\"{then}\" names no condition.");
            }
        }
    }

    private static ConditionDefinition ReadCondition(ContentFile file, string at, ConditionDto c)
    {
        string id = Id(file, Member(at, "id"), c.Id).Value;
        Reserved(file, at, "relations", c.Relations, "relations arrive with the pipeline (M1-12b).");
        Reserved(file, at, "implies", c.Implies, "implies is a relation; relations arrive with the pipeline (M1-12b).");
        Reserved(file, at, "ai", c.Ai, "ai is reserved until enemy AI (M1-23).");
        Reserved(file, at, "controlRank", c.ControlRank, "controlRank is reserved until enemy AI (M1-23).");
        Reserved(file, at, "boss", c.Boss, "boss is reserved until boss resolve (M1-24).");
        Reserved(file, at, "display", c.Display, "display is reserved until the combat display (M1-25).");
        Reserved(file, at, "extraSave", c.ExtraSave, "extraSave is reserved; no M1 condition has an extra save.");
        Reserved(file, at, "effects", c.Effects, "a condition's modifiers are written as modifiers, in the M1-07 shapes.");

        var polarity = ParseLowerEnum<ConditionPolarity>(file, Member(at, "polarity"), c.Polarity);
        ConditionTier? tier = null;
        if (polarity == ConditionPolarity.Harmful)
        {
            tier = ParseLowerEnum<ConditionTier>(file, Member(at, "tier"), c.Tier);
        }
        else if (c.Tier is not null)
        {
            throw new ContentLoadException(file.Name, Member(at, "tier"), "only a harmful condition has a tier.");
        }

        var definition = new ConditionDefinition(
            file.Name,
            id,
            polarity,
            tier,
            c.Tags is null ? ImmutableArray<string>.Empty : RuleStrings(file, Member(at, "tags"), c.Tags),
            OptionalRuleString(file, Member(at, "family"), c.Family),
            c.Denies is null ? Denies.None : ReadDenies(file, Member(at, "denies"), c.Denies),
            Terms(file, Member(at, "duration"), c.Duration),
            Terms(file, Member(at, "outOfCombat"), c.OutOfCombat),
            c.Intensity is null ? Intensity.Binary : ReadIntensity(file, Member(at, "intensity"), c.Intensity),
            c.Flags is null ? ConditionFlags.None : ReadFlags(file, Member(at, "flags"), c.Flags),
            c.Cures is null ? ImmutableArray<string>.Empty : RuleStrings(file, Member(at, "cures"), c.Cures),
            c.CureImmune is null ? ImmutableArray<string>.Empty : RuleStrings(file, Member(at, "cureImmune"), c.CureImmune),
            c.Tick is null ? null : new TickSpec(ParseLowerEnum<DamageType>(file, Member(Member(at, "tick"), "damageType"), c.Tick.DamageType)),
            EffectsKind.Modifiers(file, Member(at, "modifiers"), c.Modifiers),
            EffectsKind.Triggers(file, Member(at, "triggers"), c.Triggers));

        // The rules that do not depend on the JSON layout: structure, the combatOnly rule and the bands.
        return ConditionRules.CheckRecord(definition, at) is { } broken
            ? throw new ContentLoadException(file.Name, broken.Field, broken.Message)
            : definition;
    }

    private static void Reserved(ContentFile file, string at, string name, JsonElement value, string problem)
    {
        if (value.ValueKind != JsonValueKind.Undefined)
        {
            throw new ContentLoadException(file.Name, Member(at, name), problem);
        }
    }

    // A flag is written true or left out, as an effect's halve and zero are.
    private static bool Flag(ContentFile file, string field, bool? value) =>
        value switch
        {
            null => false,
            true => true,
            false => throw new ContentLoadException(file.Name, field, "the value is true; leave the key out instead of writing false."),
        };

    private static Denies ReadDenies(ContentFile file, string at, DeniesDto d) =>
        new(Flag(file, Member(at, "action"), d.Action), Flag(file, Member(at, "bonus"), d.Bonus), Flag(file, Member(at, "oneOf"), d.OneOf));

    private static ConditionFlags ReadFlags(ContentFile file, string at, FlagsDto f) =>
        new(
            Flag(file, Member(at, "combatOnly"), f.CombatOnly),
            Flag(file, Member(at, "sourceTied"), f.SourceTied),
            Flag(file, Member(at, "turnPartGroup"), f.TurnPartGroup),
            Flag(file, Member(at, "noProne"), f.NoProne),
            Flag(file, Member(at, "combatStartEnds"), f.CombatStartEnds));

    private static Intensity ReadIntensity(ContentFile file, string at, IntensityDto i) =>
        new(
            ParseLowerEnum<IntensityKind>(file, Member(at, "kind"), i.Kind),
            i.ValueCap,
            i.LevelCap,
            i.Stages,
            Flag(file, Member(at, "noRefresh"), i.NoRefresh));

    private static ImmutableArray<DurationTerm> Terms(ContentFile file, string field, TermDto?[]? items)
    {
        if (items is null)
        {
            return ImmutableArray<DurationTerm>.Empty;
        }

        var result = ImmutableArray.CreateBuilder<DurationTerm>(items.Length);
        for (int k = 0; k < items.Length; k++)
        {
            string at = Item(field, k);
            result.Add(Term(file, at, Need(file, at, items[k])));
        }

        return result.MoveToImmutable();
    }

    private static DurationTerm Term(ContentFile file, string at, TermDto t)
    {
        if (t.Rounds.ValueKind != JsonValueKind.Undefined)
        {
            throw new ContentLoadException(
                file.Name, Member(at, "rounds"), "rounds:N is not a condition duration (CO 5.1); a condition counts on the wearer's turns.");
        }

        int keys = (t.Turns is null ? 0 : 1) + (t.UntilSourceTurnStart is null ? 0 : 1) + (t.UntilSourceTurnEnd is null ? 0 : 1)
            + (t.SaveEnds is null ? 0 : 1) + (t.Charges is null ? 0 : 1) + (t.Decay is null ? 0 : 1) + (t.Trigger is null ? 0 : 1)
            + (t.Sustained is null ? 0 : 1) + (t.Staged is null ? 0 : 1) + (t.UntilRest is null ? 0 : 1) + (t.UntilCamp is null ? 0 : 1);
        if (keys != 1)
        {
            throw new ContentLoadException(
                file.Name,
                at,
                "a duration term has exactly one of turns, untilSourceTurnStart, untilSourceTurnEnd, saveEnds, charges, decay, "
                + $"trigger, sustained, staged, untilRest and untilCamp; this one has {keys}.");
        }

        switch (t)
        {
            case { Turns: { } n }:
                return new DurationTerm.Turns(n);
            case { UntilSourceTurnStart: { } start }:
                Flag(file, Member(at, "untilSourceTurnStart"), start);
                return new DurationTerm.UntilSourceTurnStart();
            case { UntilSourceTurnEnd: { } end }:
                Flag(file, Member(at, "untilSourceTurnEnd"), end);
                return new DurationTerm.UntilSourceTurnEnd();
            case { SaveEnds: { } s }:
                string saveAt = Member(at, "saveEnds");
                int grace = s.GraceTurns is { } g
                    ? (g >= 1 ? g : throw new ContentLoadException(file.Name, Member(saveAt, "graceTurns"), "graceTurns is at least 1; leave it out for none."))
                    : 0;
                return new DurationTerm.SaveEnds(
                    ParseLowerEnum<Save>(file, Member(saveAt, "save"), s.Save), Need(file, Member(saveAt, "cap"), s.Cap), grace);
            case { Charges: { } c }:
                string chargesAt = Member(at, "charges");
                return new DurationTerm.Charges(
                    Need(file, Member(chargesAt, "count"), c.Count),
                    RuleString(file, Member(chargesAt, "on"), c.On),
                    Term(file, Member(chargesAt, "fallback"), Need(file, Member(chargesAt, "fallback"), c.Fallback)));
            case { Decay: { } decay }:
                Flag(file, Member(at, "decay"), decay);
                return new DurationTerm.Decay();
            case { Trigger: { } e }:
                string triggerAt = Member(at, "trigger");
                return new DurationTerm.TriggerEnd(RuleString(file, Member(triggerAt, "event"), e.Event), e.Cap);
            case { Sustained: { } m }:
                return new DurationTerm.Sustained(Need(file, Member(Member(at, "sustained"), "max"), m.Max));
            case { Staged: { } st }:
                string stagedAt = Member(at, "staged");
                return new DurationTerm.Staged(
                    Need(file, Member(stagedAt, "stages"), st.Stages),
                    ParseLowerEnum<Save>(file, Member(stagedAt, "save"), st.Save),
                    Need(file, Member(stagedAt, "cap"), st.Cap),
                    st.Then is null ? null : Id(file, Member(stagedAt, "then"), st.Then).Value);
            case { UntilRest: { } rest }:
                Flag(file, Member(at, "untilRest"), rest);
                return new DurationTerm.UntilRest();
            default:
                Flag(file, Member(at, "untilCamp"), t.UntilCamp);
                return new DurationTerm.UntilCamp();
        }
    }

    private sealed class ConditionsDto
    {
        [JsonPropertyName("conditions")] public ConditionDto?[]? Conditions { get; set; }
    }

    private sealed class ConditionDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("polarity")] public string? Polarity { get; set; }
        [JsonPropertyName("tier")] public string? Tier { get; set; }
        [JsonPropertyName("tags")] public string?[]? Tags { get; set; }
        [JsonPropertyName("family")] public string? Family { get; set; }
        [JsonPropertyName("denies")] public DeniesDto? Denies { get; set; }
        [JsonPropertyName("duration")] public TermDto?[]? Duration { get; set; }
        [JsonPropertyName("outOfCombat")] public TermDto?[]? OutOfCombat { get; set; }
        [JsonPropertyName("intensity")] public IntensityDto? Intensity { get; set; }
        [JsonPropertyName("flags")] public FlagsDto? Flags { get; set; }
        [JsonPropertyName("cures")] public string?[]? Cures { get; set; }
        [JsonPropertyName("cureImmune")] public string?[]? CureImmune { get; set; }
        [JsonPropertyName("tick")] public TickDto? Tick { get; set; }
        [JsonPropertyName("modifiers")] public EffectsKind.ModifierDto?[]? Modifiers { get; set; }
        [JsonPropertyName("triggers")] public EffectsKind.TriggerDto?[]? Triggers { get; set; }

        // Reserved: any value, null included, is refused, so each key is captured whatever it holds.
        [JsonPropertyName("relations")] public JsonElement Relations { get; set; }
        [JsonPropertyName("implies")] public JsonElement Implies { get; set; }
        [JsonPropertyName("ai")] public JsonElement Ai { get; set; }
        [JsonPropertyName("controlRank")] public JsonElement ControlRank { get; set; }
        [JsonPropertyName("boss")] public JsonElement Boss { get; set; }
        [JsonPropertyName("display")] public JsonElement Display { get; set; }
        [JsonPropertyName("extraSave")] public JsonElement ExtraSave { get; set; }
        [JsonPropertyName("effects")] public JsonElement Effects { get; set; }
    }

    private sealed class DeniesDto
    {
        [JsonPropertyName("action")] public bool? Action { get; set; }
        [JsonPropertyName("bonus")] public bool? Bonus { get; set; }
        [JsonPropertyName("oneOf")] public bool? OneOf { get; set; }
    }

    private sealed class FlagsDto
    {
        [JsonPropertyName("combatOnly")] public bool? CombatOnly { get; set; }
        [JsonPropertyName("sourceTied")] public bool? SourceTied { get; set; }
        [JsonPropertyName("turnPartGroup")] public bool? TurnPartGroup { get; set; }
        [JsonPropertyName("noProne")] public bool? NoProne { get; set; }
        [JsonPropertyName("combatStartEnds")] public bool? CombatStartEnds { get; set; }
    }

    private sealed class IntensityDto
    {
        [JsonPropertyName("kind")] public string? Kind { get; set; }
        [JsonPropertyName("valueCap")] public int? ValueCap { get; set; }
        [JsonPropertyName("levelCap")] public int? LevelCap { get; set; }
        [JsonPropertyName("stages")] public int? Stages { get; set; }
        [JsonPropertyName("noRefresh")] public bool? NoRefresh { get; set; }
    }

    private sealed class TickDto
    {
        [JsonPropertyName("damageType")] public string? DamageType { get; set; }
    }

    private sealed class TermDto
    {
        [JsonPropertyName("turns")] public int? Turns { get; set; }
        [JsonPropertyName("untilSourceTurnStart")] public bool? UntilSourceTurnStart { get; set; }
        [JsonPropertyName("untilSourceTurnEnd")] public bool? UntilSourceTurnEnd { get; set; }
        [JsonPropertyName("saveEnds")] public SaveEndsDto? SaveEnds { get; set; }
        [JsonPropertyName("charges")] public ChargesDto? Charges { get; set; }
        [JsonPropertyName("decay")] public bool? Decay { get; set; }
        [JsonPropertyName("trigger")] public TriggerEndDto? Trigger { get; set; }
        [JsonPropertyName("sustained")] public SustainedDto? Sustained { get; set; }
        [JsonPropertyName("staged")] public StagedDto? Staged { get; set; }
        [JsonPropertyName("untilRest")] public bool? UntilRest { get; set; }
        [JsonPropertyName("untilCamp")] public bool? UntilCamp { get; set; }

        // Refused (CO 5.1): captured whatever it holds.
        [JsonPropertyName("rounds")] public JsonElement Rounds { get; set; }
    }

    private sealed class SaveEndsDto
    {
        [JsonPropertyName("save")] public string? Save { get; set; }
        [JsonPropertyName("cap")] public int? Cap { get; set; }
        [JsonPropertyName("graceTurns")] public int? GraceTurns { get; set; }
    }

    private sealed class ChargesDto
    {
        [JsonPropertyName("count")] public int? Count { get; set; }
        [JsonPropertyName("on")] public string? On { get; set; }
        [JsonPropertyName("fallback")] public TermDto? Fallback { get; set; }
    }

    private sealed class TriggerEndDto
    {
        [JsonPropertyName("event")] public string? Event { get; set; }
        [JsonPropertyName("cap")] public int? Cap { get; set; }
    }

    private sealed class SustainedDto
    {
        [JsonPropertyName("max")] public int? Max { get; set; }
    }

    private sealed class StagedDto
    {
        [JsonPropertyName("stages")] public int? Stages { get; set; }
        [JsonPropertyName("save")] public string? Save { get; set; }
        [JsonPropertyName("cap")] public int? Cap { get; set; }
        [JsonPropertyName("then")] public string? Then { get; set; }
    }
}

/// <summary>A broken rule: the field's path and the problem.</summary>
internal readonly record struct ConditionProblem(string Field, string Message);

/// <summary>
/// The condition rules that do not depend on the JSON layout (M1-12a plan calls 4 and 6), shared by the loader and
/// <see cref="Conditions.Insert"/>'s duration override: the structure of a term list, the term and intensity agreement,
/// the <c>combatOnly</c> rule for repeat saves, the CO 5.3 bands for a side, and the CO 5.5 out-of-combat limit.
/// Paths are built from the given list path: a term's key is <c>list[k].key</c>, its fields <c>list[k].key.field</c>,
/// and a fallback <c>list[k].charges.fallback.key</c>.
/// </summary>
internal static class ConditionRules
{
    /// <summary>The out-of-combat limit for a non-lingering harmful condition, in ticks (CO 5.5).</summary>
    public const int OutOfCombatLimit = 20;

    /// <summary>
    /// Every rule of a record: a harmful condition has a duration; a tick is harmful; the intensity's caps fit its kind;
    /// each list's structure, agreement and <c>combatOnly</c> rule; the duration within the enemy-facing CO 5.3 band and
    /// the out-of-combat list within CO 5.5. <paramref name="at"/> is the record's path.
    /// </summary>
    public static ConditionProblem? CheckRecord(ConditionDefinition d, string at)
    {
        string duration = Member(at, "duration");
        string outOfCombat = Member(at, "outOfCombat");
        if (d.Polarity == ConditionPolarity.Harmful && d.Duration.IsEmpty)
        {
            return new(duration, "a harmful condition needs a duration: at least one term.");
        }

        if (d.Tick is not null && d.Polarity != ConditionPolarity.Harmful)
        {
            return new(Member(at, "tick"), "a tick deals damage; only a harmful condition ticks.");
        }

        return CheckIntensity(d.Intensity, Member(at, "intensity"))
            ?? CheckTerms(d, d.Duration, duration, at)
            ?? CheckTerms(d, d.OutOfCombat, outOfCombat, at)
            ?? CheckBand(d, d.Duration, duration, at, partyFacing: false)
            ?? CheckOutOfCombat(d, d.OutOfCombat, outOfCombat);
    }

    /// <summary>
    /// The structure of <paramref name="terms"/> at <paramref name="at"/> for <paramref name="d"/>: each count at least 1,
    /// a grace within the cap, a fallback that is not <c>charges</c>, no term twice; a <c>decay</c> term needs a decay
    /// intensity and a <c>staged</c> term a staged one with the same stages; a <c>saveEnds</c> or <c>staged</c> term needs
    /// <c>combatOnly</c>. <paramref name="recordAt"/> is the record's path.
    /// </summary>
    public static ConditionProblem? CheckTerms(ConditionDefinition d, ImmutableArray<DurationTerm> terms, string at, string recordAt)
    {
        if (terms.IsDefault)
        {
            return new(at, "the duration is uninitialised.");
        }

        for (int k = 0; k < terms.Length; k++)
        {
            string termAt = Item(at, k);
            if (terms[k] is null)
            {
                return new(termAt, "a term is null.");
            }

            for (int j = 0; j < k; j++)
            {
                if (terms[j] == terms[k])
                {
                    return new(termAt, "the term appears twice.");
                }
            }

            if (CheckTerm(d, terms[k], termAt, recordAt, fallback: false) is { } broken)
            {
                return broken;
            }
        }

        return null;
    }

    // A term's shape (DurationTerm.CheckShape), then its agreement with the record.
    private static ConditionProblem? CheckTerm(ConditionDefinition d, DurationTerm term, string termAt, string recordAt, bool fallback) =>
        DurationTerm.CheckShape(term, termAt, fallback) ?? CheckAgreement(d, term, termAt, recordAt);

    // The record-dependent half of a term's rules, a charges fallback included: a decay term needs a decay intensity, a
    // staged term a staged one with the same stages, and a repeat save combatOnly.
    private static ConditionProblem? CheckAgreement(ConditionDefinition d, DurationTerm term, string termAt, string recordAt)
    {
        string key = Member(termAt, term.Tag);
        switch (term)
        {
            case DurationTerm.Charges c:
                return CheckAgreement(d, c.Fallback, Member(key, "fallback"), recordAt);
            case DurationTerm.Decay when d.Intensity.Kind != IntensityKind.Decay:
                return new(key, "a decay duration needs intensity kind decay.");
            case DurationTerm.Staged st when d.Intensity.Kind != IntensityKind.Staged || d.Intensity.Stages != st.Stages:
                return new(Member(key, "stages"), "a staged duration needs intensity kind staged with the same stages.");
        }

        // Out-of-combat repeat saves are D-23's: a repeat save lives only on a combatOnly condition, and
        // Conditions.Insert refuses to place one with no fight on.
        if (term is DurationTerm.SaveEnds or DurationTerm.Staged && !d.Flags.CombatOnly)
        {
            return new(key, "a saveEnds or staged duration needs combatOnly: out-of-combat repeat saves are not built (D-23).");
        }

        return null;
    }

    private static ConditionProblem? CheckIntensity(Intensity i, string at)
    {
        if (!Enum.IsDefined(i.Kind))
        {
            return new(Member(at, "kind"), "unknown intensity kind.");
        }

        bool valued = i.Kind is IntensityKind.Valued or IntensityKind.Decay;
        if (i.ValueCap is { } v ? !valued || v < 1 : valued)
        {
            return new(Member(at, "valueCap"), "valueCap, at least 1, belongs to a valued or decay intensity, and only to one.");
        }

        if (i.LevelCap is { } l ? i.Kind != IntensityKind.Accumulating || l < 1 : i.Kind == IntensityKind.Accumulating)
        {
            return new(Member(at, "levelCap"), "levelCap, at least 1, belongs to an accumulating intensity, and only to one.");
        }

        if (i.Stages is { } s ? i.Kind != IntensityKind.Staged || s < 1 : i.Kind == IntensityKind.Staged)
        {
            return new(Member(at, "stages"), "stages, at least 1, belongs to a staged intensity, and only to one.");
        }

        return null;
    }

    /// <summary>
    /// The CO 5.3 band of <paramref name="d"/> on one side, term by term (a <c>charges</c> fallback against this band and
    /// the token band). Party-facing hard control is <c>turns:1</c> or a trigger with cap 1; a party-facing turn-part
    /// major is <c>turns:1</c>. Windows fit every band.
    /// </summary>
    public static ConditionProblem? CheckBand(
        ConditionDefinition d, ImmutableArray<DurationTerm> terms, string at, string recordAt, bool partyFacing)
    {
        var band = Band.For(d, partyFacing);
        for (int k = 0; k < terms.Length; k++)
        {
            if (CheckBandTerm(d, band, terms[k], Item(at, k), recordAt) is { } broken)
            {
                return broken;
            }
        }

        return null;
    }

    private static ConditionProblem? CheckBandTerm(ConditionDefinition d, Band band, DurationTerm term, string termAt, string recordAt)
    {
        string key = Member(termAt, term.Tag);
        string who = $"a {band.Name} condition";
        if (band.OnlyTurns && term is not (DurationTerm.Turns or DurationTerm.UntilSourceTurnStart or DurationTerm.UntilSourceTurnEnd))
        {
            return new(key, $"{who} lasts turns:{band.TurnsMax} (CO 5.3).");
        }

        switch (term)
        {
            case DurationTerm.Turns t when t.N > band.TurnsMax:
                return new(key, $"{who} lasts at most {band.TurnsMax} turns (CO 5.3).");
            case DurationTerm.SaveEnds or DurationTerm.Staged when !band.RepeatSaves:
                return new(key, $"{who} takes no repeat save (CO 5.3).");
            case DurationTerm.SaveEnds s when s.Cap > band.CapMax:
                return new(Member(key, "cap"), $"{who} has a cap of at most {band.CapMax} (CO 5.3).");
            case DurationTerm.Staged st when st.Cap > band.CapMax:
                return new(Member(key, "cap"), $"{who} has a cap of at most {band.CapMax} (CO 5.3).");
            case DurationTerm.TriggerEnd { Cap: null } when band.TriggerNeedsCap:
                return new(Member(key, "cap"), $"{who} needs a trigger cap (CO 5.3).");
            case DurationTerm.TriggerEnd { Cap: { } cap } when cap > band.CapMax:
                return new(Member(key, "cap"), $"{who} has a cap of at most {band.CapMax} (CO 5.3).");
            case DurationTerm.Sustained s when band.SustainedMax is { } max && s.Max > max:
                return new(Member(key, "max"), $"{who} is sustained for at most {max} turns (CO 5.3).");
            case DurationTerm.Decay when d.Intensity.ValueCap is { } cap && cap > band.TurnsMax:
                return new(Member(Member(recordAt, "intensity"), "valueCap"), $"{who} decays from at most {band.TurnsMax} (CO 5.1, 5.3).");
            case DurationTerm.UntilRest or DurationTerm.UntilCamp when !band.Lingering:
                return new(key, $"{who} is never lingering (CO 5.3).");
            case DurationTerm.Charges c:
                string fallbackAt = Member(key, "fallback");
                return CheckBandTerm(d, band, c.Fallback, fallbackAt, recordAt)
                    ?? (band.Name == Band.Token.Name ? null : CheckBandTerm(d, Band.Token, c.Fallback, fallbackAt, recordAt));
        }

        return null;
    }

    /// <summary>
    /// The CO 5.5 out-of-combat list: no repeat save (D-23), and for a harmful condition every count at most
    /// <see cref="OutOfCombatLimit"/> ticks (lingering terms are exempt).
    /// </summary>
    public static ConditionProblem? CheckOutOfCombat(ConditionDefinition d, ImmutableArray<DurationTerm> terms, string at)
    {
        int limit = d.Polarity == ConditionPolarity.Harmful ? OutOfCombatLimit : int.MaxValue;
        for (int k = 0; k < terms.Length; k++)
        {
            var term = terms[k] is DurationTerm.Charges c ? c.Fallback : terms[k];
            string key = terms[k] is DurationTerm.Charges
                ? Member(Member(Member(Item(at, k), "charges"), "fallback"), term.Tag)
                : Member(Item(at, k), term.Tag);
            string over = $"a harmful condition lasts at most {OutOfCombatLimit} ticks out of combat (CO 5.5).";
            switch (term)
            {
                case DurationTerm.SaveEnds or DurationTerm.Staged:
                    return new(key, "out-of-combat repeat saves are not built (D-23).");
                case DurationTerm.Turns t when t.N > limit:
                    return new(key, over);
                case DurationTerm.TriggerEnd { Cap: { } cap } when cap > limit:
                    return new(Member(key, "cap"), over);
                case DurationTerm.Sustained s when s.Max > limit:
                    return new(Member(key, "max"), over);
            }
        }

        return null;
    }

    // A side's band: the most turns, the most cap, whether repeat saves and lingering terms are allowed, whether a trigger
    // needs a cap, the most sustained turns (null: no bound), and whether only turns (and windows) are allowed.
    private sealed record Band(
        string Name, int TurnsMax, int CapMax, bool RepeatSaves, bool TriggerNeedsCap, bool Lingering, int? SustainedMax, bool OnlyTurns)
    {
        public static readonly Band Token = new("token", 3, 3, true, false, true, 3, false);

        public static Band For(ConditionDefinition d, bool partyFacing) => (d.Polarity, d.Tier) switch
        {
            (ConditionPolarity.Token, _) or (_, ConditionTier.Token) => Token,
            (ConditionPolarity.Beneficial, _) => new("beneficial", 10, 10, true, false, true, null, false),
            (_, ConditionTier.Minor) => new("minor", 3, 3, true, false, true, 3, false),
            (_, ConditionTier.Major) when partyFacing && d.Flags.TurnPartGroup =>
                new("party-facing turn-part major", 1, 1, false, true, false, 1, true),
            (_, ConditionTier.Major) => new("major", 2, 3, true, false, true, 3, false),
            (_, ConditionTier.Hard) when partyFacing => new("party-facing hard", 1, 1, false, true, false, 1, false),
            _ => new("hard", 1, 2, true, true, false, 2, false),
        };
    }
}
