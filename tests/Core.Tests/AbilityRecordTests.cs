using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Canonical;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// The <c>abilities</c> content kind (M1-11 Task 3; AB 6.1, 7.1, 7.3, 8.1): what loads, what is reserved and refused
/// with its JSON path, the reference checks of <see cref="RulesContext.Create"/>, and the canonical form.
/// </summary>
public class AbilityRecordTests
{
    private const string AbilitiesFile = "abilities/abilities.json";

    private const string Strike =
        """{"id": "test.strike", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1}}""";

    // --- Valid content ---

    [Fact]
    public void A_full_record_loads_every_field()
    {
        var set = Load(Abilities(
            """
            {"id": "test.full", "kind": "limited", "cost": "move:2",
             "pool": {"id": "pool.a", "count": 2},
             "target": {"shape": "burst", "range": 4, "radius": 2, "selective": true},
             "resolve": {"save": "reflex", "onSuccess": "none"},
             "damage": [{"type": "fire", "dice": "2d6", "flat": 3}, {"type": "cold", "flat": -1}, {"type": "acid", "dice": "1d4"}],
             "effects": ["test.fx"]}
            """), Effects("""{"id": "test.fx", "kind": "ability"}"""));

        var a = set.Find<AbilityDefinition>("test.full")!;
        Assert.Equal(AbilitiesFile, a.File);
        Assert.Equal(AbilityKind.Limited, a.Kind);
        Assert.Equal(new AbilityCost(CostKind.Move, 2), a.Cost);
        Assert.Equal(new PoolUse("pool.a", 2), a.Pool);
        Assert.Equal(new TargetSpec(TargetShape.Burst, 4, 0, 2, true), a.Target);
        Assert.Equal(new SaveResolve(Save.Reflex), a.Resolve);
        Assert.Equal<DamagePart>(
            [
                new DamagePart(DamageType.Fire, 2, 6, 3),
                new DamagePart(DamageType.Cold, 0, 0, -1),
                new DamagePart(DamageType.Acid, 1, 4, 0),
            ],
            a.Damage.ToArray());
        Assert.Equal<string>(["test.fx"], a.Effects.ToArray());
        Assert.Null(a.Reaction);
    }

    [Fact]
    public void A_minimal_record_has_no_optional_parts()
    {
        var a = Load(Abilities(Strike)).Find<AbilityDefinition>("test.strike")!;

        Assert.Equal(new AbilityCost(CostKind.Action), a.Cost);
        Assert.Null(a.Pool);
        Assert.Equal(new TargetSpec(TargetShape.Single, 1, 1, 0, false), a.Target);
        Assert.Null(a.Resolve);
        Assert.Empty(a.Damage);
        Assert.Empty(a.Effects);
        Assert.Null(a.Reaction);
    }

    [Theory]
    [InlineData("action", CostKind.Action, 0)]
    [InlineData("bonus", CostKind.Bonus, 0)]
    [InlineData("free", CostKind.Free, 0)]
    [InlineData("none", CostKind.None, 0)]
    [InlineData("move:1", CostKind.Move, 1)]
    [InlineData("move:12", CostKind.Move, 12)]
    public void Every_accepted_cost_loads(string token, CostKind kind, int move)
    {
        var a = Load(Abilities(Strike.Replace("\"action\"", $"\"{token}\"", StringComparison.Ordinal)))
            .Find<AbilityDefinition>("test.strike")!;
        Assert.Equal(new AbilityCost(kind, move), a.Cost);
    }

    [Theory]
    [InlineData("atWill", AbilityKind.AtWill)]
    [InlineData("limited", AbilityKind.Limited)]
    public void Every_accepted_kind_loads(string token, AbilityKind kind)
    {
        var a = Load(Abilities(Strike.Replace("atWill", token, StringComparison.Ordinal))).Find<AbilityDefinition>("test.strike")!;
        Assert.Equal(kind, a.Kind);
    }

    [Theory]
    [InlineData("""{"shape": "single", "range": 5, "count": 3}""", TargetShape.Single, 5, 3, 0)]
    [InlineData("""{"shape": "single", "range": 0}""", TargetShape.Single, 0, 1, 0)]
    [InlineData("""{"shape": "burst", "radius": 1}""", TargetShape.Burst, 0, 0, 1)]
    [InlineData("""{"shape": "burst", "range": 6, "radius": 2}""", TargetShape.Burst, 6, 0, 2)]
    [InlineData("""{"shape": "cone", "length": 4}""", TargetShape.Cone, 0, 0, 4)]
    [InlineData("""{"shape": "line", "length": 7}""", TargetShape.Line, 0, 0, 7)]
    public void Every_accepted_shape_loads(string target, TargetShape shape, int range, int count, int size)
    {
        var a = Load(Abilities($$"""{"id": "test.a", "kind": "atWill", "cost": "action", "target": {{target}}}"""))
            .Find<AbilityDefinition>("test.a")!;
        Assert.Equal(new TargetSpec(shape, range, count, size, false), a.Target);
    }

    [Fact]
    public void An_attack_resolve_loads()
    {
        var a = Load(Abilities("""
            {"id": "test.a", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1},
             "resolve": {"attack": "dexterity", "trained": false}}
            """)).Find<AbilityDefinition>("test.a")!;
        Assert.Equal(new AttackResolve(new AttackSpec(Ability.Dexterity, false)), a.Resolve);
    }

    [Fact]
    public void A_reaction_loads_with_every_policy_and_without_one()
    {
        foreach (string policy in new[] { "always", "ifItChangesOutcome", "whilePoolAtLeastHalf", "never", "askMe" })
        {
            var a = Load(Abilities(Reaction($$""", "policy": {"kind": "{{policy}}", "filter": "none"}"""))).Find<AbilityDefinition>("test.r")!;
            Assert.Equal(ReactionTrigger.AttackRoll, a.Reaction!.Trigger);
            Assert.Equal(2, a.Reaction.Bonus);
            Assert.Equal(Enum.Parse<ReactionMode>(policy, ignoreCase: true), a.Reaction.Policy);
        }

        var none = Load(Abilities(Reaction(""))).Find<AbilityDefinition>("test.r")!;
        Assert.Null(none.Reaction!.Policy);
        Assert.Null(none.Target);
        Assert.Equal(new AbilityCost(CostKind.Reaction), none.Cost);

        var own = Load(Abilities(Reaction("").Replace("attackRoll", "ownSave", StringComparison.Ordinal))).Find<AbilityDefinition>("test.r")!;
        Assert.Equal(ReactionTrigger.OwnSave, own.Reaction!.Trigger);
    }

    [Fact]
    public void The_kind_reads_content_ids()
    {
        var kind = new AbilitiesKind();
        Assert.True(kind.IdsAreContentIds);
        Assert.Equal(AbilitiesFile, kind.FileName);
        Assert.Equal("abilities", kind.ArrayName);
    }

    // --- Reserved and refused ---

    [Theory]
    [InlineData("attack", "M1-14")]
    [InlineData("onHit", "M1-16")]
    [InlineData("onEvent:ally.fell", "M1-20")]
    public void A_reserved_cost_is_refused_naming_its_step(string token, string step)
    {
        var ex = LoadFails(Abilities(Strike.Replace("\"action\"", $"\"{token}\"", StringComparison.Ordinal)));
        Assert.Equal(AbilitiesFile, ex.File);
        Assert.Equal("$.abilities[0].cost", ex.Field);
        Assert.Contains(step, ex.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("move:0")]
    [InlineData("move:-1")]
    [InlineData("move:")]
    [InlineData("move:x")]
    [InlineData("onEvent:")]
    [InlineData("Action")]
    [InlineData("sprint")]
    public void A_malformed_cost_is_refused(string token)
    {
        var ex = LoadFails(Abilities(Strike.Replace("\"action\"", $"\"{token}\"", StringComparison.Ordinal)));
        Assert.Equal("$.abilities[0].cost", ex.Field);
    }

    [Theory]
    [InlineData("passive")]
    [InlineData("toggle")]
    [InlineData("sustained")]
    public void A_reserved_kind_is_refused(string token)
    {
        var ex = LoadFails(Abilities(Strike.Replace("atWill", token, StringComparison.Ordinal)));
        Assert.Equal("$.abilities[0].kind", ex.Field);
        Assert.Contains("reserved", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void An_unknown_kind_is_refused()
    {
        Assert.Equal("$.abilities[0].kind", LoadFails(Abilities(Strike.Replace("atWill", "often", StringComparison.Ordinal))).Field);
    }

    [Fact]
    public void Half_on_a_save_is_refused_citing_D_22()
    {
        var ex = LoadFails(Abilities("""
            {"id": "test.a", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1},
             "resolve": {"save": "will", "onSuccess": "half"}}
            """));
        Assert.Equal("$.abilities[0].resolve.onSuccess", ex.Field);
        Assert.Contains("D-22", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_reaction_filter_other_than_none_is_refused()
    {
        var ex = LoadFails(Abilities(Reaction(""", "policy": {"kind": "always", "filter": "nonMinion"}""")));
        Assert.Equal("$.abilities[0].reaction.policy.filter", ex.Field);
        Assert.Contains("M1-22", ex.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("""{"id": "test.a", "kind": "atWill", "cost": "reaction", "target": {"shape": "single", "range": 1}}""", "$.abilities[0].cost")]
    [InlineData("""{"id": "test.a", "kind": "reaction", "cost": "action", "reaction": {"trigger": "attackRoll", "bonus": 1}}""", "$.abilities[0].cost")]
    [InlineData("""{"id": "test.a", "kind": "reaction", "cost": "reaction"}""", "$.abilities[0].reaction")]
    [InlineData("""{"id": "test.a", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1}, "reaction": {"trigger": "attackRoll", "bonus": 1}}""", "$.abilities[0].reaction")]
    [InlineData("""{"id": "test.a", "kind": "reaction", "cost": "reaction", "target": {"shape": "single", "range": 1}, "reaction": {"trigger": "attackRoll", "bonus": 1}}""", "$.abilities[0].target")]
    [InlineData("""{"id": "test.a", "kind": "atWill", "cost": "action"}""", "$.abilities[0].target")]
    public void The_reaction_kind_cost_and_block_must_agree(string record, string path)
    {
        Assert.Equal(path, LoadFails(Abilities(record)).Field);
    }

    [Theory]
    [InlineData("""{"shape": "single"}""", "$.abilities[0].target.range")]
    [InlineData("""{"shape": "single", "range": -1}""", "$.abilities[0].target.range")]
    [InlineData("""{"shape": "single", "range": 1, "count": 0}""", "$.abilities[0].target.count")]
    [InlineData("""{"shape": "single", "range": 1, "radius": 1}""", "$.abilities[0].target.radius")]
    [InlineData("""{"shape": "single", "range": 1, "length": 1}""", "$.abilities[0].target.length")]
    [InlineData("""{"shape": "burst"}""", "$.abilities[0].target.radius")]
    [InlineData("""{"shape": "burst", "radius": 0}""", "$.abilities[0].target.radius")]
    [InlineData("""{"shape": "burst", "radius": 1, "length": 1}""", "$.abilities[0].target.length")]
    [InlineData("""{"shape": "burst", "radius": 1, "count": 2}""", "$.abilities[0].target.count")]
    [InlineData("""{"shape": "cone"}""", "$.abilities[0].target.length")]
    [InlineData("""{"shape": "cone", "length": 0}""", "$.abilities[0].target.length")]
    [InlineData("""{"shape": "cone", "radius": 2, "length": 2}""", "$.abilities[0].target.radius")]
    [InlineData("""{"shape": "cone", "length": 2, "range": 3}""", "$.abilities[0].target.range")]
    [InlineData("""{"shape": "line", "length": 2, "count": 2}""", "$.abilities[0].target.count")]
    [InlineData("""{"shape": "ring", "radius": 2}""", "$.abilities[0].target.shape")]
    [InlineData("""{"shape": "burst", "radius": 1, "extra": 1}""", "$.abilities[0].target.extra")]
    public void A_malformed_target_is_refused(string target, string path)
    {
        var ex = LoadFails(Abilities($$"""{"id": "test.a", "kind": "atWill", "cost": "action", "target": {{target}}}"""));
        Assert.Equal(AbilitiesFile, ex.File);
        Assert.Equal(path, ex.Field);
    }

    [Theory]
    [InlineData("""{"attack": "strength"}""", "$.abilities[0].resolve.trained")]
    [InlineData("""{"attack": "luck", "trained": true}""", "$.abilities[0].resolve.attack")]
    [InlineData("""{"attack": "strength", "trained": true, "onSuccess": "none"}""", "$.abilities[0].resolve.onSuccess")]
    [InlineData("""{"save": "will"}""", "$.abilities[0].resolve.onSuccess")]
    [InlineData("""{"save": "will", "onSuccess": "none", "trained": true}""", "$.abilities[0].resolve.trained")]
    [InlineData("""{"save": "luck", "onSuccess": "none"}""", "$.abilities[0].resolve.save")]
    [InlineData("""{"save": "will", "onSuccess": "full"}""", "$.abilities[0].resolve.onSuccess")]
    [InlineData("""{"attack": "strength", "trained": true, "save": "will", "onSuccess": "none"}""", "$.abilities[0].resolve")]
    [InlineData("{}", "$.abilities[0].resolve")]
    public void A_malformed_resolve_is_refused(string resolve, string path)
    {
        var ex = LoadFails(Abilities($$"""
            {"id": "test.a", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1}, "resolve": {{resolve}}}
            """));
        Assert.Equal(path, ex.Field);
    }

    [Theory]
    [InlineData("""{"type": "sonic", "flat": 1}""", "$.abilities[0].damage[0].type")]
    [InlineData("""{"type": "fire"}""", "$.abilities[0].damage[0]")]
    [InlineData("""{"type": "fire", "dice": "0d6"}""", "$.abilities[0].damage[0].dice")]
    [InlineData("""{"type": "fire", "dice": "2d0"}""", "$.abilities[0].damage[0].dice")]
    [InlineData("""{"type": "fire", "dice": "2d"}""", "$.abilities[0].damage[0].dice")]
    [InlineData("""{"type": "fire", "dice": "d6"}""", "$.abilities[0].damage[0].dice")]
    [InlineData("""{"type": "fire", "dice": "2D6"}""", "$.abilities[0].damage[0].dice")]
    [InlineData("""{"type": "fire", "dice": "-2d6"}""", "$.abilities[0].damage[0].dice")]
    [InlineData("""{"type": "fire", "dice": "99999999999d6"}""", "$.abilities[0].damage[0].dice")]
    public void A_malformed_damage_part_is_refused(string part, string path)
    {
        var ex = LoadFails(Abilities($$"""
            {"id": "test.a", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1}, "damage": [{{part}}]}
            """));
        Assert.Equal(path, ex.Field);
    }

    [Theory]
    [InlineData(""", "pool": {"id": "pool.a", "count": 0}""", "$.abilities[0].pool.count")]
    [InlineData(""", "pool": {"id": "Pool A", "count": 1}""", "$.abilities[0].pool.id")]
    [InlineData(""", "pool": {"id": "pool.a"}""", "$.abilities[0].pool.count")]
    [InlineData(""", "effects": ["Bad Id"]""", "$.abilities[0].effects[0]")]
    [InlineData(""", "effects": [null]""", "$.abilities[0].effects[0]")]
    [InlineData(""", "extra": 1""", "$.abilities[0].extra")]
    public void A_malformed_pool_or_effect_list_is_refused(string extra, string path)
    {
        var ex = LoadFails(Abilities(
            """{"id": "test.a", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1}""" + extra + "}"));
        Assert.Equal(path, ex.Field);
    }

    [Theory]
    [InlineData("""{"trigger": "hit", "bonus": 1}""", "$.abilities[0].reaction.trigger")]
    [InlineData("""{"trigger": "attackRoll"}""", "$.abilities[0].reaction.bonus")]
    [InlineData("""{"trigger": "attackRoll", "bonus": 1, "policy": {"filter": "none"}}""", "$.abilities[0].reaction.policy.kind")]
    [InlineData("""{"trigger": "attackRoll", "bonus": 1, "policy": {"kind": "sometimes"}}""", "$.abilities[0].reaction.policy.kind")]
    public void A_malformed_reaction_block_is_refused(string block, string path)
    {
        var ex = LoadFails(Abilities($$"""{"id": "test.a", "kind": "reaction", "cost": "reaction", "reaction": {{block}}}"""));
        Assert.Equal(path, ex.Field);
    }

    [Fact]
    public void A_repeated_id_is_refused()
    {
        var ex = LoadFails(Abilities(Strike, Strike));
        Assert.Equal(AbilitiesFile, ex.File);
    }

    // --- References, checked by the rules context ---

    [Fact]
    public void An_unknown_pool_is_refused_by_the_rules_context()
    {
        var set = Load(Abilities(
            """{"id": "test.a", "kind": "limited", "cost": "action", "pool": {"id": "pool.nope", "count": 1}, "target": {"shape": "single", "range": 1}}"""));

        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(set));
        Assert.Equal(AbilitiesFile, ex.File);
        Assert.Equal("$.abilities[0].pool.id", ex.Field);
    }

    [Fact]
    public void An_unknown_effect_is_refused_by_the_rules_context()
    {
        var set = Load(Abilities(
            Strike,
            """{"id": "test.b", "kind": "atWill", "cost": "action", "target": {"shape": "single", "range": 1}, "effects": ["test.fx", "test.missing"]}"""),
            Effects("""{"id": "test.fx", "kind": "ability"}"""));

        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(set));
        Assert.Equal(AbilitiesFile, ex.File);
        Assert.Equal("$.abilities[1].effects[1]", ex.Field);
    }

    [Fact]
    public void Known_references_pass_and_the_context_serves_abilities_ordinal_by_id()
    {
        var set = Load(Abilities(
            """{"id": "test.b", "kind": "limited", "cost": "action", "pool": {"id": "pool.a", "count": 1}, "target": {"shape": "single", "range": 1}, "effects": ["test.fx"]}""",
            Strike.Replace("test.strike", "test.a", StringComparison.Ordinal)),
            Effects("""{"id": "test.fx", "kind": "ability"}"""));

        var rules = RulesContext.Create(set, pools: [new PoolDefinition("pool.a", RefillTag.Rest, _ => 1)]);

        Assert.Equal<string>(["test.a", "test.b"], rules.Abilities.Select(a => a.Id).ToArray());
        Assert.Equal("test.b", rules.Ability("test.b").Id);
        Assert.Throws<ArgumentException>(() => rules.Ability("test.none"));
        Assert.Empty(RulesContext.Empty.Abilities);
    }

    [Fact]
    public void A_code_built_definition_merges_with_content()
    {
        var set = Load(Abilities(Strike));
        var built = new AbilityDefinition(
            "code", "test.built", AbilityKind.AtWill, new AbilityCost(CostKind.Action), null,
            new TargetSpec(TargetShape.Single, 1, 1, 0, false), new AttackResolve(new AttackSpec(Ability.Strength, true)),
            [new DamagePart(DamageType.Slashing, 1, 8, 0)], [], null);

        var rules = RulesContext.Create(set, abilities: [built]);

        Assert.Equal<string>(["test.built", "test.strike"], rules.Abilities.Select(a => a.Id).ToArray());
        Assert.Same(built, rules.Ability("test.built"));
    }

    [Fact]
    public void A_code_built_duplicate_of_a_content_id_is_refused()
    {
        var set = Load(Abilities(Strike));
        var built = new AbilityDefinition(
            "code", "test.strike", AbilityKind.AtWill, new AbilityCost(CostKind.Free), null,
            new TargetSpec(TargetShape.Single, 1, 1, 0, false), null, [], [], null);

        Assert.Throws<ArgumentException>(() => RulesContext.Create(set, abilities: [built]));
        Assert.Throws<ArgumentException>(() => RulesContext.Create(set, abilities: [built with { Id = "test.x" }, built with { Id = "test.x" }]));
        Assert.Throws<ArgumentException>(() => RulesContext.Create(set, abilities: [null!]));
    }

    [Fact]
    public void A_code_built_definition_has_its_references_checked()
    {
        var built = new AbilityDefinition(
            "code", "test.built", AbilityKind.Limited, new AbilityCost(CostKind.Action), new PoolUse("pool.nope", 1),
            new TargetSpec(TargetShape.Single, 1, 1, 0, false), null, [], [], null);

        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(Load(), abilities: [built]));
        Assert.Equal("code", ex.File);
    }

    // --- Canonical form ---

    [Fact]
    public void The_canonical_form_is_stable_and_starts_with_the_id()
    {
        string record = """
            {"id": "test.full", "kind": "limited", "cost": "move:2", "pool": {"id": "pool.a", "count": 2},
             "target": {"shape": "cone", "length": 3},
             "resolve": {"attack": "strength", "trained": true},
             "damage": [{"type": "fire", "dice": "2d6", "flat": 3}], "effects": ["test.fx"]}
            """;
        byte[] bytes = CanonicalWriter.Serialize(Load(Abilities(record)).Find<AbilityDefinition>("test.full")!);
        byte[] again = CanonicalWriter.Serialize(Load(Abilities(record)).Find<AbilityDefinition>("test.full")!);

        Assert.Equal(bytes, again);
        // A string field 0 is a length then ASCII; the record has no header.
        Assert.Equal(9, bytes[0]);
        Assert.Equal("test.full", System.Text.Encoding.ASCII.GetString(bytes, 4, 9));
    }

    [Fact]
    public void Every_field_changes_the_digest()
    {
        string baseRecord = """
            {"id": "test.a", "kind": "limited", "cost": "move:2", "pool": {"id": "pool.a", "count": 2},
             "target": {"shape": "single", "range": 3, "count": 1},
             "resolve": {"attack": "strength", "trained": true},
             "damage": [{"type": "fire", "dice": "2d6", "flat": 3}], "effects": ["test.fx"]}
            """;
        string Digest(string r) => Load(Abilities(r)).DigestOf([AbilitiesFile]).ToHex();
        string reference = Digest(baseRecord);

        string[] variants =
        [
            baseRecord.Replace("test.a", "test.b", StringComparison.Ordinal),
            baseRecord.Replace("limited", "atWill", StringComparison.Ordinal),
            baseRecord.Replace("move:2", "move:3", StringComparison.Ordinal),
            baseRecord.Replace("\"count\": 2", "\"count\": 3", StringComparison.Ordinal),
            baseRecord.Replace("\"pool.a\"", "\"pool.b\"", StringComparison.Ordinal),
            baseRecord.Replace("\"range\": 3", "\"range\": 4", StringComparison.Ordinal),
            baseRecord.Replace("\"strength\"", "\"dexterity\"", StringComparison.Ordinal),
            baseRecord.Replace("\"trained\": true", "\"trained\": false", StringComparison.Ordinal),
            baseRecord.Replace("2d6", "2d8", StringComparison.Ordinal),
            baseRecord.Replace("\"flat\": 3", "\"flat\": 4", StringComparison.Ordinal),
            baseRecord.Replace("\"fire\"", "\"cold\"", StringComparison.Ordinal),
            baseRecord.Replace("test.fx", "test.fy", StringComparison.Ordinal),
            baseRecord.Replace("\"single\", \"range\": 3, \"count\": 1", "\"single\", \"range\": 3, \"count\": 1, \"selective\": true", StringComparison.Ordinal),
        ];
        foreach (string v in variants)
        {
            Assert.NotEqual(reference, Digest(v));
        }

        Assert.Equal(reference, Digest(baseRecord));
    }

    // --- Helpers ---

    private static string Reaction(string policy) =>
        $$$"""{"id": "test.r", "kind": "reaction", "cost": "reaction", "reaction": {"trigger": "attackRoll", "bonus": 2{{{policy}}}}}""";

    private static ContentFile Abilities(params string[] records) =>
        new(AbilitiesFile, "{\"abilities\": [" + string.Join(", ", records) + "]}");

    private static ContentFile Effects(params string[] records) =>
        new("effects/effects.json", "{\"effects\": [" + string.Join(", ", records) + "]}");

    private static ContentSet Load(params ContentFile[] files) =>
        ContentLoader.Load(files, [new AbilitiesKind(), new EffectsKind()]);

    private static ContentLoadException LoadFails(params ContentFile[] files) =>
        Assert.Throws<ContentLoadException>(() => Load(files));
}
