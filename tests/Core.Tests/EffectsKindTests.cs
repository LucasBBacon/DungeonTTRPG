using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Sim.Tools;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// The <c>effects</c> content kind (M1-07 Task 1): every modifier operation and trigger form loads, and every
/// rejection names <c>effects/effects.json</c> and the exact JSON path (MAIN 6.5; Review Focus 5).
/// </summary>
public class EffectsKindTests
{
    private const string EffectsFile = "effects/effects.json";

    // --- Valid content ---

    [Fact]
    public void A_valid_file_loads_every_operation_and_both_action_forms()
    {
        var set = Load(Effects(
            """
            {"id": "test.all", "kind": "gear",
             "modifiers": [
               {"target": "speed", "add": -2},
               {"target": "speed", "halve": true},
               {"target": "speed", "zero": true},
               {"target": "attacksAgainst", "rollMode": "advantage"},
               {"target": "attackRoll", "rollMode": "disadvantage"},
               {"target": "unit", "flag": "hidden"}
             ],
             "triggers": [
               {"on": "unitMoved", "priority": -1, "scope": "any",
                "do": [{"applyEffect": "test.plain", "to": "subject"}, {"hook": "test.hook", "args": ["a", "b"]}]},
               {"on": "unitMoved", "scope": "holder", "do": [{"hook": "test.bare"}]}
             ]}
            """,
            """{"id": "test.plain", "kind": "condition"}"""));

        var all = set.Find<EffectDefinition>("test.all")!;
        Assert.Equal(EffectSourceKind.Gear, all.Kind);
        Assert.Equal<Modifier>(
            [
                new Modifier("speed", ModifierOp.Add, -2, RollMode.Normal, null),
                new Modifier("speed", ModifierOp.Halve, 0, RollMode.Normal, null),
                new Modifier("speed", ModifierOp.Zero, 0, RollMode.Normal, null),
                new Modifier("attacksAgainst", ModifierOp.RollMode, 0, RollMode.Advantage, null),
                new Modifier("attackRoll", ModifierOp.RollMode, 0, RollMode.Disadvantage, null),
                new Modifier("unit", ModifierOp.Flag, 0, RollMode.Normal, "hidden"),
            ],
            all.Modifiers.ToArray());

        Assert.Equal(2, all.Triggers.Length);
        var first = all.Triggers[0];
        Assert.Equal(("unitMoved", -1, TriggerScope.Any), (first.On, first.Priority, first.Scope));
        Assert.Equal(("test.plain", TriggerTarget.Subject, (string?)null), (first.Do[0].ApplyEffect, first.Do[0].To, first.Do[0].Hook));
        Assert.Empty(first.Do[0].Args);
        Assert.Equal(((string?)null, "test.hook"),(first.Do[1].ApplyEffect, first.Do[1].Hook));
        Assert.Equal<string>(["a", "b"], first.Do[1].Args.ToArray());

        var second = all.Triggers[1];
        Assert.Equal((0, TriggerScope.Holder), (second.Priority, second.Scope));
        Assert.Equal("test.bare", second.Do[0].Hook);
        Assert.Empty(second.Do[0].Args);

        var plain = set.Find<EffectDefinition>("test.plain")!;
        Assert.Equal(EffectSourceKind.Condition, plain.Kind);
        Assert.Empty(plain.Modifiers);
        Assert.Empty(plain.Triggers);
    }

    [Theory]
    [InlineData("condition", EffectSourceKind.Condition)]
    [InlineData("trait", EffectSourceKind.Trait)]
    [InlineData("gear", EffectSourceKind.Gear)]
    [InlineData("item", EffectSourceKind.Item)]
    [InlineData("ability", EffectSourceKind.Ability)]
    [InlineData("environment", EffectSourceKind.Environment)]
    public void Every_source_kind_is_written_in_lower_case(string token, EffectSourceKind kind)
    {
        var set = Load(Effects($$"""{"id": "test.a", "kind": "{{token}}"}"""));
        Assert.Equal(kind, set.Find<EffectDefinition>("test.a")!.Kind);
    }

    [Fact]
    public void Effect_ids_are_content_ids()
    {
        var kind = new EffectsKind();
        Assert.True(kind.IdsAreContentIds);
        Assert.False(kind.ReadByGeneration);
        Assert.Equal(EffectsFile, kind.FileName);
        Assert.Contains(ContentKinds.All, k => k is EffectsKind);
    }

    // --- Rejections: each names the file and the exact path ---

    [Theory]
    // An unknown key, at every level.
    [InlineData("""{"id": "test.a", "kind": "gear", "colour": 1}""", "$.effects[0].colour")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2, "colour": 1}]}""", "$.effects[0].modifiers[0].colour")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "colour": 1, "do": [{"hook": "h"}]}]}""", "$.effects[0].triggers[0].colour")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"hook": "h", "colour": 1}]}]}""", "$.effects[0].triggers[0].do[0].colour")]
    // Zero or two operation keys.
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed"}]}""", "$.effects[0].modifiers[0]")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2, "halve": true}]}""", "$.effects[0].modifiers[0]")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "zero": true, "flag": "x"}]}""", "$.effects[0].modifiers[0]")]
    // diceBonus is reserved.
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "attackRoll", "diceBonus": "1d4"}]}""", "$.effects[0].modifiers[0].diceBonus")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "attackRoll", "diceBonus": null}]}""", "$.effects[0].modifiers[0].diceBonus")]
    // An empty do.
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": []}]}""", "$.effects[0].triggers[0].do")]
    // Both or neither of applyEffect and hook.
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "test.a", "to": "holder", "hook": "h"}]}]}""", "$.effects[0].triggers[0].do[0]")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"to": "holder"}]}]}""", "$.effects[0].triggers[0].do[0]")]
    // A field of the other action form.
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"hook": "h", "to": "holder"}]}]}""", "$.effects[0].triggers[0].do[0].to")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "test.a", "to": "holder", "args": []}]}]}""", "$.effects[0].triggers[0].do[0].args")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "test.a"}]}]}""", "$.effects[0].triggers[0].do[0].to")]
    // Bad enum values.
    [InlineData("""{"id": "test.a", "kind": "Gear"}""", "$.effects[0].kind")]
    [InlineData("""{"id": "test.a", "kind": "spell"}""", "$.effects[0].kind")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "attackRoll", "rollMode": "Advantage"}]}""", "$.effects[0].modifiers[0].rollMode")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "attackRoll", "rollMode": "normal"}]}""", "$.effects[0].modifiers[0].rollMode")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "everyone", "do": [{"hook": "h"}]}]}""", "$.effects[0].triggers[0].scope")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "test.a", "to": "target"}]}]}""", "$.effects[0].triggers[0].do[0].to")]
    // A halve or zero that is not true.
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "halve": false}]}""", "$.effects[0].modifiers[0].halve")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "zero": false}]}""", "$.effects[0].modifiers[0].zero")]
    // Missing or malformed required fields.
    [InlineData("""{"kind": "gear"}""", "$.effects[0].id")]
    [InlineData("""{"id": "Test.A", "kind": "gear"}""", "$.effects[0].id")]
    [InlineData("""{"id": "test.a"}""", "$.effects[0].kind")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"add": 2}]}""", "$.effects[0].modifiers[0].target")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"scope": "any", "do": [{"hook": "h"}]}]}""", "$.effects[0].triggers[0].on")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "do": [{"hook": "h"}]}]}""", "$.effects[0].triggers[0].scope")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any"}]}""", "$.effects[0].triggers[0].do")]
    [InlineData("""{"id": "test.a", "kind": "gear", "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"hook": "h", "args": [""]}]}]}""", "$.effects[0].triggers[0].do[0].args[0]")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 1.5}]}""", "$.effects[0].modifiers[0].add")]
    public void A_bad_effect_names_the_file_and_path(string record, string path)
    {
        var ex = LoadFails(Effects(record));
        Assert.Equal(EffectsFile, ex.File);
        Assert.Equal(path, ex.Field);
    }

    [Fact]
    public void An_apply_effect_naming_a_missing_effect_names_the_reference()
    {
        var ex = LoadFails(Effects(
            """{"id": "test.a", "kind": "trait"}""",
            """
            {"id": "test.b", "kind": "trait", "triggers": [
              {"on": "unitMoved", "scope": "holder", "do": [{"hook": "h"}]},
              {"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"applyEffect": "test.missing", "to": "holder"}]}
            ]}
            """));
        Assert.Equal(EffectsFile, ex.File);
        Assert.Equal("$.effects[1].triggers[1].do[1].applyEffect", ex.Field);
        Assert.Contains("test.missing", ex.Message);
    }

    [Fact]
    public void Dice_bonus_is_rejected_as_reserved()
    {
        var ex = LoadFails(Effects("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "attackRoll", "diceBonus": "1d4"}]}"""));
        Assert.Contains("reserved", ex.Message);
    }

    [Fact]
    public void Two_effects_with_one_id_name_the_second()
    {
        var ex = LoadFails(Effects("""{"id": "test.a", "kind": "gear"}""", """{"id": "test.a", "kind": "trait"}"""));
        Assert.Equal(EffectsFile, ex.File);
        Assert.Equal("$.effects[1].id", ex.Field);
    }

    // --- Canonical bytes: every field is written ---

    [Theory]
    [InlineData("""{"id": "test.b", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.b", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "trait", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "reach", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 3}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "halve": true}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitHit", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "priority": 1, "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "subject"}, {"hook": "h", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "g", "args": ["x"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["y"]}]}]}""")]
    [InlineData("""{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"hook": "h", "args": ["x"]}, {"applyEffect": "test.a", "to": "holder"}]}]}""")]
    public void Every_field_changes_the_digest(string changed)
    {
        const string baseline =
            """{"id": "test.a", "kind": "gear", "modifiers": [{"target": "speed", "add": 2}], "triggers": [{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.a", "to": "holder"}, {"hook": "h", "args": ["x"]}]}]}""";

        Assert.NotEqual(Digest(baseline), Digest(changed));
        Assert.Equal(Digest(baseline), Digest(baseline));
    }

    [Theory]
    [InlineData("""{"target": "speed", "zero": true}""", """{"target": "speed", "halve": true}""")]
    [InlineData("""{"target": "attackRoll", "rollMode": "advantage"}""", """{"target": "attackRoll", "rollMode": "disadvantage"}""")]
    [InlineData("""{"target": "unit", "flag": "hidden"}""", """{"target": "unit", "flag": "prone"}""")]
    public void Every_modifier_field_changes_the_digest(string a, string b)
    {
        Assert.NotEqual(
            Digest($$"""{"id": "test.a", "kind": "gear", "modifiers": [{{a}}]}"""),
            Digest($$"""{"id": "test.a", "kind": "gear", "modifiers": [{{b}}]}"""));
    }

    // --- The repository file and the code registries ---

    [Fact]
    public void The_repository_effects_file_loads_empty()
    {
        string path = Path.Combine(RepoRoot.Find(), "content", "effects", "effects.json");
        Assert.True(File.Exists(path), "content/effects/effects.json is missing.");

        var content = ContentLoader.Load(ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")));

        Assert.True(content.IsKindFile(EffectsFile));
        Assert.Contains(content.Files, f => f.Name == EffectsFile);
        Assert.Empty(content.RecordsIn(EffectsFile));
    }

    [Fact]
    public void The_core_registries_hold_the_m1_07_rows()
    {
        var speed = Assert.Single(DerivedNumbers.Core);
        Assert.Equal(("speed", NumberRule.Speed, (long?)null), (speed.Id, speed.Rule, speed.Cap));
        Assert.Equal(6, Dungeon.Core.Effects.Effects.Number(RulesContext.Empty, Fixtures.RingState(), Fixtures.Hero, "speed"));
        Assert.Equal<string>(["attackRoll", "attacksAgainst"], RollTargets.Core.ToArray());
        Assert.Equal<string>(["unitMoved"], RuleEvents.Core.Select(e => e.Id).ToArray());
        Assert.Equal(typeof(UnitMoved), RuleEvents.Core[0].EventType);
    }

    // --- Helpers ---

    private static ContentFile Effects(params string[] records) =>
        new(EffectsFile, "{\"effects\": [" + string.Join(", ", records) + "]}");

    private static ContentSet Load(params ContentFile[] files) => ContentLoader.Load(files, [new EffectsKind()]);

    private static ContentLoadException LoadFails(params ContentFile[] files) =>
        Assert.Throws<ContentLoadException>(() => Load(files));

    private static string Digest(string record) => Load(Effects(record)).DigestOf([EffectsFile]).ToHex();
}
