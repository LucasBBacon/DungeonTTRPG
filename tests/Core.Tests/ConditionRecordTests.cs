using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Conditions;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.ConditionFixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Conditions = Dungeon.Core.Conditions.Conditions;

/// <summary>
/// The condition records, <c>conditions/conditions.json</c> (M1-12a Task 3; plan calls 3, 4 and 5; CO 5.1, 5.3, 17.1,
/// 18.1): every duration term, intensity kind and flag loads; each refusal names the file and the JSON path; the
/// synthesised effect definition folds; the core definitions exist whatever the content.
/// </summary>
public class ConditionRecordTests
{
    private const string File = "conditions/conditions.json";

    private static ContentSet LoadConditions(params string[] records) => Load([], records);

    private static ContentLoadException LoadFails(params string[] records) =>
        Assert.Throws<ContentLoadException>(() => LoadConditions(records));

    private static ConditionDefinition Def(string id) => ConditionFixtures.Content.Find<ConditionDefinition>(id)!;

    // --- The fixture set (CO 18.1) ---

    [Fact]
    public void Every_duration_term_and_combined_list_loads()
    {
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(1)], Def(Stun).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(3)], Def(Slow).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.UntilSourceTurnStart()], Def(MarkStart).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.UntilSourceTurnEnd()], Def(MarkEnd).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.SaveEnds(Save.Will, 2, 1)], Def(Held).Duration.ToArray());
        Assert.Equal<DurationTerm>(
            [new DurationTerm.Charges(1, "unitMoved", new DurationTerm.Turns(3))], Def(Token).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.Decay()], Def(Venom).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.TriggerEnd("unitMoved", 3)], Def(Trig).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.Sustained(2)], Def(Sustained).Duration.ToArray());
        Assert.Equal<DurationTerm>(
            [new DurationTerm.Staged(3, Save.Fortitude, 3, Stun)], Def(Calcify).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.UntilRest()], Def(Weary).Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.UntilCamp()], Def(Blessed).Duration.ToArray());
        Assert.Equal<DurationTerm>(
            [new DurationTerm.Sustained(2), new DurationTerm.SaveEnds(Save.Will, 2, 0)], Def(Combined).Duration.ToArray());

        // An uncapped trigger, and an outOfCombat list beside the in-combat one.
        var set = LoadConditions(
            """{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"trigger": {"event": "unitMoved"}}], "outOfCombat": [{"turns": 20}, {"untilRest": true}]}""");
        var a = set.Find<ConditionDefinition>("test.a")!;
        Assert.Equal<DurationTerm>([new DurationTerm.TriggerEnd("unitMoved", null)], a.Duration.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(20), new DurationTerm.UntilRest()], a.OutOfCombat.ToArray());
        Assert.Equal<DurationTerm>([new DurationTerm.Turns(10)], Def(Asleep).OutOfCombat.ToArray());
        Assert.Empty(Def(Stun).OutOfCombat);
    }

    [Fact]
    public void Every_intensity_kind_loads()
    {
        Assert.Equal(new Intensity(IntensityKind.Binary, null, null, null, false), Def(Stun).Intensity);
        Assert.Equal(new Intensity(IntensityKind.Valued, 5, null, null, false), Def(Burn).Intensity);
        Assert.Equal(new Intensity(IntensityKind.Valued, 5, null, null, true), Def(Blessed).Intensity);
        Assert.Equal(new Intensity(IntensityKind.Decay, 3, null, null, false), Def(Venom).Intensity);
        Assert.Equal(new Intensity(IntensityKind.Accumulating, null, 2, null, false), Def(Weary).Intensity);
        Assert.Equal(new Intensity(IntensityKind.Staged, null, null, 3, false), Def(Calcify).Intensity);
        Assert.Equal(new Intensity(IntensityKind.Charges, null, null, null, false), Def(Token).Intensity);
    }

    [Fact]
    public void Every_flag_and_field_loads()
    {
        Assert.Equal(new ConditionFlags(false, false, false, false, false), Def(Stun).Flags);
        Assert.True(Def(Held).Flags.CombatOnly);
        Assert.True(Def(Tied).Flags.SourceTied);
        Assert.True(Def(Daze).Flags.TurnPartGroup);
        Assert.Equal(new ConditionFlags(false, false, false, true, true), Def(Asleep).Flags);

        var set = LoadConditions(
            """
            {"id": "test.all", "polarity": "harmful", "tier": "major", "tags": ["fear", "engineState"], "family": "test.family",
             "denies": {"action": true, "bonus": true},
             "duration": [{"turns": 2}],
             "intensity": {"kind": "binary"},
             "flags": {"combatOnly": true, "sourceTied": true, "turnPartGroup": true, "noProne": true, "combatStartEnds": true},
             "cures": ["rally"], "cureImmune": ["tonic"], "tick": {"damageType": "cold"},
             "modifiers": [{"target": "speed", "add": -1}],
             "triggers": [{"on": "turnStarted", "scope": "holder", "do": [{"hook": "test.hook"}]}]}
            """);
        var all = set.Find<ConditionDefinition>("test.all")!;
        Assert.Equal((File, ConditionPolarity.Harmful, (ConditionTier?)ConditionTier.Major), (all.File, all.Polarity, all.Tier));
        Assert.Equal<string>(["fear", "engineState"], all.Tags.ToArray());
        Assert.Equal("test.family", all.Family);
        Assert.Equal(new Denies(true, true, false), all.Denies);
        Assert.Equal(new ConditionFlags(true, true, true, true, true), all.Flags);
        Assert.Equal<string>(["rally"], all.Cures.ToArray());
        Assert.Equal<string>(["tonic"], all.CureImmune.ToArray());
        Assert.Equal(new TickSpec(DamageType.Cold), all.Tick);
        Assert.Equal<Modifier>([new Modifier("speed", ModifierOp.Add, -1, RollMode.Normal, null)], all.Modifiers.ToArray());
        Assert.Equal("turnStarted", Assert.Single(all.Triggers).On);

        // Defaults: no tags, family, denies, flags, cures, tick, modifiers or triggers; binary intensity.
        var stun = Def(Stun);
        Assert.Empty(stun.Tags);
        Assert.Null(stun.Family);
        Assert.Equal(new Denies(false, false, false), stun.Denies);
        Assert.Empty(stun.Cures);
        Assert.Empty(stun.CureImmune);
        Assert.Null(stun.Tick);
        Assert.Empty(stun.Triggers);

        // Beneficial and token conditions carry no tier.
        Assert.Equal((ConditionPolarity.Beneficial, (ConditionTier?)null), (Def(Ward).Polarity, Def(Ward).Tier));
        Assert.Equal((ConditionPolarity.Token, (ConditionTier?)null), (Def(Token).Polarity, Def(Token).Tier));
    }

    [Fact]
    public void The_fixtures_cover_every_duration_model_intensity_kind_and_tier()
    {
        var all = ConditionFixtures.Content.All<ConditionDefinition>();
        var terms = all.SelectMany(d => d.Duration.Concat(d.OutOfCombat)).Select(t => t.GetType()).Distinct().ToHashSet();
        foreach (var type in typeof(DurationTerm).GetNestedTypes().Where(t => t.IsSubclassOf(typeof(DurationTerm))))
        {
            Assert.Contains(type, terms);
        }

        foreach (var kind in Enum.GetValues<IntensityKind>())
        {
            Assert.Contains(all, d => d.Intensity.Kind == kind);
        }

        foreach (var tier in new[] { ConditionTier.Minor, ConditionTier.Major, ConditionTier.Hard })
        {
            Assert.Contains(all, d => d.Tier == tier);
        }

        Assert.Contains(all, d => d.Polarity == ConditionPolarity.Token);
        Assert.Contains(all, d => d.Polarity == ConditionPolarity.Beneficial);
        Assert.Contains(all, d => d.Flags.SourceTied);
        Assert.Contains(all, d => d.Flags.CombatStartEnds);
        Assert.Contains(all, d => d.Tick is not null);
        Assert.Contains(all, d => d.Duration.Length > 1);
        Assert.Contains(all, d => !d.OutOfCombat.IsEmpty);
        Assert.Contains(all, d => d.Duration.Any(t => t is DurationTerm.SaveEnds { GraceTurns: > 0 }) && d.Flags.CombatOnly);
        Assert.Contains(all, d => d.Duration.Any(t => t is DurationTerm.Staged) && d.Flags.CombatOnly);
    }

    // --- Refusals at load (plan call 4) ---

    [Theory]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "relations": []}""", "$.conditions[0].relations", "M1-12b")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "implies": ["cond.x"]}""", "$.conditions[0].implies", "M1-12b")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "boss": {}}""", "$.conditions[0].boss", "M1-24")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "ai": {}}""", "$.conditions[0].ai", "M1-23")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "controlRank": 9}""", "$.conditions[0].controlRank", "M1-23")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "display": {}}""", "$.conditions[0].display", "M1-25")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "extraSave": []}""", "$.conditions[0].extraSave", "reserved")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"rounds": 2}]}""", "$.conditions[0].duration[0].rounds", "not a condition duration")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "hard", "duration": [{"saveEnds": {"save": "will", "cap": 2}}]}""", "$.conditions[0].duration[0].saveEnds", "combatOnly")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "major", "intensity": {"kind": "staged", "stages": 2}, "duration": [{"staged": {"stages": 2, "save": "will", "cap": 2}}]}""", "$.conditions[0].duration[0].staged", "combatOnly")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "duration": [{"turns": 1}]}""", "$.conditions[0].tier", "the field is missing")]
    [InlineData("""{"id": "test.a", "polarity": "beneficial", "tier": "minor", "duration": [{"turns": 1}]}""", "$.conditions[0].tier", "harmful")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor"}""", "$.conditions[0].duration", "harmful")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1, "decay": true}]}""", "$.conditions[0].duration[0]", "exactly one")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{}]}""", "$.conditions[0].duration[0]", "exactly one")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 0}]}""", "$.conditions[0].duration[0].turns", "at least 1")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 2}, {"turns": 2}]}""", "$.conditions[0].duration[1]", "twice")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"decay": false}]}""", "$.conditions[0].duration[0].decay", "true")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"decay": true}]}""", "$.conditions[0].duration[0].decay", "decay")]
    [InlineData("""{"id": "test.a", "polarity": "token", "duration": [{"charges": {"count": 1, "on": "unitMoved", "fallback": {"charges": {"count": 1, "on": "unitMoved", "fallback": {"turns": 1}}}}}]}""", "$.conditions[0].duration[0].charges.fallback.charges", "fallback")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"saveEnds": {"save": "will", "cap": 2, "graceTurns": 3}}], "flags": {"combatOnly": true}}""", "$.conditions[0].duration[0].saveEnds.graceTurns", "cap")]
    [InlineData("""{"id": "test.a", "polarity": "beneficial", "tick": {"damageType": "fire"}, "duration": [{"turns": 1}]}""", "$.conditions[0].tick", "harmful")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "intensity": {"kind": "binary", "valueCap": 2}, "duration": [{"turns": 1}]}""", "$.conditions[0].intensity.valueCap", "valued")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "flags": {"combatOnly": false}, "duration": [{"turns": 1}]}""", "$.conditions[0].flags.combatOnly", "true")]
    public void A_bad_record_is_refused_with_its_path(string record, string path, string problem)
    {
        var ex = LoadFails(record);
        Assert.Equal(File, ex.File);
        Assert.Equal(path, ex.Field);
        Assert.Contains(problem, ex.Message);
    }

    // The CO 5.3 bands, checked at load on the enemy-facing side (the most lenient); the party-facing side is checked on
    // an override at insert (ConditionStateTests).
    [Theory]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "hard", "duration": [{"turns": 2}]}""", "$.conditions[0].duration[0].turns")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "major", "duration": [{"turns": 3}]}""", "$.conditions[0].duration[0].turns")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 4}]}""", "$.conditions[0].duration[0].turns")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "hard", "flags": {"combatOnly": true}, "duration": [{"saveEnds": {"save": "will", "cap": 3}}]}""", "$.conditions[0].duration[0].saveEnds.cap")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "hard", "duration": [{"trigger": {"event": "unitMoved"}}]}""", "$.conditions[0].duration[0].trigger.cap")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "hard", "duration": [{"untilRest": true}]}""", "$.conditions[0].duration[0].untilRest")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "major", "duration": [{"sustained": {"max": 4}}]}""", "$.conditions[0].duration[0].sustained.max")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "hard", "intensity": {"kind": "decay", "valueCap": 2}, "duration": [{"decay": true}]}""", "$.conditions[0].intensity.valueCap")]
    [InlineData("""{"id": "test.a", "polarity": "beneficial", "duration": [{"turns": 11}]}""", "$.conditions[0].duration[0].turns")]
    [InlineData("""{"id": "test.a", "polarity": "token", "duration": [{"charges": {"count": 1, "on": "unitMoved", "fallback": {"turns": 4}}}]}""", "$.conditions[0].duration[0].charges.fallback.turns")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "outOfCombat": [{"turns": 21}]}""", "$.conditions[0].outOfCombat[0].turns")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "flags": {"combatOnly": true}, "duration": [{"turns": 1}], "outOfCombat": [{"saveEnds": {"save": "will", "cap": 2}}]}""", "$.conditions[0].outOfCombat[0].saveEnds")]
    public void A_band_violation_is_refused_with_its_path(string record, string path)
    {
        var ex = LoadFails(record);
        Assert.Equal(File, ex.File);
        Assert.Equal(path, ex.Field);
    }

    [Fact]
    public void A_staged_then_or_trigger_applyEffect_must_name_something()
    {
        var then = LoadFails(
            """{"id": "test.a", "polarity": "harmful", "tier": "major", "flags": {"combatOnly": true}, "intensity": {"kind": "staged", "stages": 2}, "duration": [{"staged": {"stages": 2, "save": "will", "cap": 2, "then": "test.none"}}]}""");
        Assert.Equal("$.conditions[0].duration[0].staged.then", then.Field);

        var apply = LoadFails(
            """{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.none", "to": "holder"}]}]}""");
        Assert.Equal("$.conditions[0].triggers[0].do[0].applyEffect", apply.Field);
    }

    // --- Refusals at rules build (plan call 3) ---

    [Fact]
    public void An_id_shared_with_an_effect_or_a_core_definition_is_refused()
    {
        var clash = Assert.Throws<ContentLoadException>(() => RulesContext.Create(Load(
            ["""{"id": "test.a", "kind": "trait"}"""],
            ["""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}]}"""])));
        Assert.Equal((File, "$.conditions[0].id"), (clash.File, clash.Field));

        var core = Assert.Throws<ContentLoadException>(() => RulesContext.Create(Load(
            [], [$$"""{"id": "{{Conditions.SteeledId}}", "polarity": "beneficial", "duration": [{"turns": 1}]}"""])));
        Assert.Equal((File, "$.conditions[0].id"), (core.File, core.Field));
    }

    [Fact]
    public void An_ability_effect_naming_a_condition_is_refused()
    {
        var ability = new AbilityDefinition(
            "test/abilities", "test.hex", AbilityKind.AtWill, new AbilityCost(CostKind.Action), null,
            new TargetSpec(TargetShape.Single, 1, 1, 0, false), null, [], [Slow], null);
        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(ConditionFixtures.Content, abilities: [ability]));
        Assert.Equal("$.abilities[test.hex].effects[0]", ex.Field);
        Assert.Contains("conditions are applied through the pipeline (M1-12b)", ex.Message);
    }

    [Fact]
    public void A_trigger_applying_a_condition_is_refused()
    {
        var effect = Assert.Throws<ContentLoadException>(() => RulesContext.Create(Load(
            [.. EffectRecords, $$"""{"id": "test.e", "kind": "trait", "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "{{Slow}}", "to": "holder"}]}]}"""],
            Records)));
        Assert.Equal(("effects/effects.json", "$.effects[2].triggers[0].do[0].applyEffect"), (effect.File, effect.Field));
        Assert.Contains("conditions are applied through the pipeline (M1-12b)", effect.Message);

        var own = Assert.Throws<ContentLoadException>(() => RulesContext.Create(Load(
            [],
            [
                $$"""{"id": "test.c", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "{{Conditions.SteeledId}}", "to": "holder"}]}]}""",
            ])));
        Assert.Equal((File, "$.conditions[0].triggers[0].do[0].applyEffect"), (own.File, own.Field));
        Assert.Contains("conditions are applied through the pipeline (M1-12b)", own.Message);
    }

    [Theory]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "major", "duration": [{"trigger": {"event": "test.nothing", "cap": 2}}]}""", "$.conditions[0].duration[0].trigger.event")]
    [InlineData("""{"id": "test.a", "polarity": "token", "duration": [{"charges": {"count": 1, "on": "test.nothing", "fallback": {"turns": 1}}}]}""", "$.conditions[0].duration[0].charges.on")]
    [InlineData("""{"id": "test.a", "polarity": "token", "duration": [{"turns": 1}], "outOfCombat": [{"charges": {"count": 1, "on": "unitMoved", "fallback": {"trigger": {"event": "test.nothing"}}}}]}""", "$.conditions[0].outOfCombat[0].charges.fallback.trigger.event")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "modifiers": [{"target": "test.nothing", "add": 1}]}""", "$.conditions[0].modifiers[0].target")]
    [InlineData("""{"id": "test.a", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 1}], "triggers": [{"on": "test.nothing", "scope": "holder", "do": [{"hook": "test.hook"}]}]}""", "$.conditions[0].triggers[0].on")]
    public void An_unknown_rule_id_is_refused_at_rules_build(string record, string path)
    {
        var set = LoadConditions(record);
        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(set));
        Assert.Equal((File, path), (ex.File, ex.Field));
    }

    // --- The synthesised effect definition and the core definitions ---

    [Fact]
    public void A_conditions_modifier_folds_through_its_synthesised_effect_definition()
    {
        var synthesised = Rules.Definition(Slow);
        Assert.Equal(EffectSourceKind.Condition, synthesised.Kind);
        Assert.Equal(Def(Slow).Modifiers.ToArray(), synthesised.Modifiers.ToArray());
        Assert.Same(Def(Slow), Rules.Condition(Slow));

        var s = GameState.Create(GridMap.Parse("..."), [Fixtures.U(1, 0, 0), Fixtures.U(2, 2, 0)], 7);
        long before = Dungeon.Core.Effects.Effects.Number(Rules, s, new UnitId(1), "speed");
        var (_, next) = Conditions.Insert(Rules, s, Slow, new UnitId(1), new EffectSource(new UnitId(2)), ConditionInsert.None);
        Assert.Equal(before - 2, Dungeon.Core.Effects.Effects.Number(Rules, next, new UnitId(1), "speed"));
    }

    [Fact]
    public void The_core_definitions_exist_with_no_content()
    {
        foreach (var rules in new[] { RulesContext.Empty, RulesContext.Create(ContentLoader.Load([], [new Dungeon.Core.Conditions.ConditionsKind()])), Rules })
        {
            var steeled = rules.Condition(Conditions.SteeledId);
            Assert.Equal("cond.steeled", steeled.Id);
            Assert.Equal((ConditionPolarity.Beneficial, (ConditionTier?)null, true), (steeled.Polarity, steeled.Tier, steeled.Flags.CombatOnly));
            Assert.Empty(steeled.Duration);
            Assert.Empty(steeled.Modifiers);

            var unyielding = rules.Condition(Conditions.UnyieldingId);
            Assert.Equal("cond.unyielding", unyielding.Id);
            Assert.True(unyielding.Flags.CombatOnly);

            Assert.Equal(EffectSourceKind.Condition, rules.Definition(Conditions.SteeledId).Kind);
            Assert.Equal(EffectSourceKind.Condition, rules.Definition(Conditions.UnyieldingId).Kind);
        }

        Assert.Throws<ArgumentException>(() => Rules.Condition(Plain));
        Assert.Equal(Records.Length + 2, Rules.Conditions.Length);
    }

    // --- The canonical form ---

    [Fact]
    public void The_canonical_form_is_stable()
    {
        const string record =
            """{"id": "test.a", "polarity": "harmful", "tier": "major", "flags": {"combatOnly": true}, "duration": [{"sustained": {"max": 2}}, {"saveEnds": {"save": "will", "cap": 2}}], "outOfCombat": [{"turns": 3}], "tags": ["x"], "modifiers": [{"target": "speed", "add": -1}]}""";
        const string reordered =
            """
            {"tags": ["x"], "modifiers": [{"add": -1, "target": "speed"}], "outOfCombat": [{"turns": 3}],
              "duration": [{"sustained": {"max": 2}}, {"saveEnds": {"cap": 2, "save": "will"}}],
              "flags": {"combatOnly": true}, "tier": "major", "polarity": "harmful", "id": "test.a"}
            """;
        Assert.Equal(Digest(record), Digest(reordered));
        Assert.Equal("8bc9be104c100cf8af0c8ff43552e225faae609876e075409cecfad5bf7645d4", Digest(record));

        // Every rules field reaches the bytes.
        Assert.NotEqual(Digest(record), Digest(record.Replace("\"max\": 2", "\"max\": 1")));
        Assert.NotEqual(Digest(record), Digest(record.Replace("\"turns\": 3", "\"turns\": 2")));
        Assert.NotEqual(Digest(record), Digest(record.Replace("\"tags\": [\"x\"]", "\"tags\": [\"y\"]")));
        Assert.NotEqual(Digest(record), Digest(record.Replace("\"add\": -1", "\"add\": -2")));
        Assert.NotEqual(Digest(record), Digest(record.Replace("\"combatOnly\": true", "\"combatOnly\": true, \"noProne\": true")));
    }

    private static string Digest(string record) => LoadConditions(record).DigestOf([File]).ToHex();
}
