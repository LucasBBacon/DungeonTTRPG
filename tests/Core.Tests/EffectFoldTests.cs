using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Sim.Tools;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The active effects in the run state and the fold queries (M1-07 Task 2): the stacking regimes per source kind,
/// the ability rule and its cap, the speed fold, D-15 roll modes, flags, purity, and saving and digesting the
/// instances (Review Focus 1-3).
/// </summary>
public class EffectFoldTests
{
    private const string EffectsFile = "effects/effects.json";

    private static readonly UnitId Ally = new(2);

    // The fixture numbers: a stacking number, two capped ability scores (base 4 and base 5), a number whose base is
    // per unit, and a number whose base reads another number, plus the core speed.
    private static readonly ImmutableArray<DerivedNumber> Numbers =
        [
            new DerivedNumber("test.stack", NumberRule.Stack, 10, null),
            new DerivedNumber("test.ability", NumberRule.AbilityScore, 4, 5),
            new DerivedNumber("test.ability5", NumberRule.AbilityScore, 5, 5),
            new DerivedNumber("test.perUnit", NumberRule.Stack, a => a.Unit.Id.Value * 10, null),
            new DerivedNumber("test.reads", NumberRule.Stack, a => a.Number("test.ability") + 10, null),
            .. DerivedNumbers.Core,
        ];

    // The fixture flag names.
    private static readonly ImmutableArray<string> Flags = ["dodging", "hidden"];

    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("cond.plus2", "condition", Add("speed", 2), Add("test.stack", 2)),
            Def("cond.plus2b", "condition", Add("speed", 2), Add("test.stack", 2)),
            Def("cond.minus1", "condition", Add("speed", -1), Add("test.stack", -1)),
            Def("cond.minus3", "condition", Add("speed", -3), Add("test.stack", -3)),
            Def("gear.plus2", "gear", Add("speed", 2), Add("test.stack", 2)),
            Def("gear.plus2b", "gear", Add("speed", 2), Add("test.stack", 2)),
            Def("trait.plus1", "trait", Add("speed", 1)),
            Def("gear.plus5", "gear", Add("speed", 5)),
            Def("gear.minus10", "gear", Add("speed", -10)),
            Def("cond.halve", "condition", """{"target": "speed", "halve": true}"""),
            Def("cond.halve2", "condition", """{"target": "speed", "halve": true}"""),
            Def("cond.zero", "condition", """{"target": "speed", "zero": true}"""),
            Def("ab.trait.plus2", "trait", Add("test.ability", 2)),
            Def("ab.trait.plus3", "trait", Add("test.ability", 3)),
            Def("ab.gear.plus1", "gear", Add("test.ability", 1)),
            Def("ab.gear.minus3", "gear", Add("test.ability", -3)),
            Def("ab.cond.minus1", "condition", Add("test.ability", -1)),
            Def("ab.gear.minus2", "gear", Add("test.ability", -2)),
            Def("ab.trait.minus1", "trait", Add("test.ability", -1)),
            Def("adv", "condition", """{"target": "attackRoll", "rollMode": "advantage"}"""),
            Def("adv2", "ability", """{"target": "attackRoll", "rollMode": "advantage"}"""),
            Def("dis", "condition", """{"target": "attackRoll", "rollMode": "disadvantage"}"""),
            Def("hidden", "ability", """{"target": "unit", "flag": "hidden"}"""),
            Def("ab5.trait.plus2", "trait", Add("test.ability5", 2)),
            Def("ab5.gear.minus2", "gear", Add("test.ability5", -2)),
            Def("reads.gear.plus1", "gear", Add("test.reads", 1))),
        Numbers,
        flags: Flags);

    // --- Review Focus 1: stacking per source kind ---

    [Theory]
    [InlineData(8, "cond.plus2", "cond.plus2b")]                  // two condition +2: best bonus only
    [InlineData(10, "gear.plus2", "gear.plus2b")]                 // two gear +2: sums
    [InlineData(5, "cond.plus2", "cond.minus3")]                  // condition +2 and -3: -1
    [InlineData(9, "gear.plus2", "cond.plus2", "cond.minus1")]    // gear +2, condition +2, condition -1: +3
    [InlineData(6)]                                               // nothing: the base
    public void Speed_stacks_by_source_kind(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "speed"));

    [Theory]
    [InlineData(12, "cond.plus2", "cond.plus2b")]
    [InlineData(14, "gear.plus2", "gear.plus2b")]
    [InlineData(9, "cond.plus2", "cond.minus3")]
    [InlineData(13, "gear.plus2", "cond.plus2", "cond.minus1")]
    [InlineData(7, "cond.minus1", "cond.minus3")]                 // only the worst condition penalty
    public void A_stack_number_folds_conditions_and_sums_the_rest(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "test.stack"));

    // --- Review Focus 2: the ability rule and the cap ---

    // SM 5.1: the cap binds the base plus the best bonus ("+5 after all bonuses"); the worst penalty applies after it
    // ("a bonus and a penalty both apply"): min(base + bonus, cap) + penalty.
    [Theory]
    [InlineData(2, "ab.trait.plus2", "ab.gear.plus1", "ab.gear.minus3")]   // highest bonus only (+2): min(4 + 2, 5) - 3
    [InlineData(2, "ab.cond.minus1", "ab.gear.minus2")]                    // worst penalty only (-2), not -3
    [InlineData(2, "ab.trait.minus1", "ab.gear.minus2")]                   // across stacking kinds too
    [InlineData(5, "ab.trait.plus3")]                                      // 4 + 3 = 7, capped at 5
    [InlineData(4, "ab.trait.plus3", "ab.cond.minus1")]                    // the penalty after the cap: min(7, 5) - 1
    [InlineData(4)]
    public void An_ability_score_caps_the_best_bonus_then_takes_the_worst_penalty(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "test.ability"));

    [Theory]
    [InlineData(3, "ab5.trait.plus2", "ab5.gear.minus2")]                 // min(5 + 2, 5) - 2: the penalty shows
    [InlineData(5, "ab5.trait.plus2")]                                    // a bonus at the cap is wasted
    [InlineData(3, "ab5.gear.minus2")]
    [InlineData(5)]
    public void An_ability_at_the_cap_still_loses_its_penalty(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "test.ability5"));

    // Only the ability rule has a spec'd cap (SM 5.1); a cap on any other rule is a registry defect, caught at startup.
    [Theory]
    [InlineData(NumberRule.Stack)]
    [InlineData(NumberRule.Speed)]
    public void A_cap_on_a_number_that_is_not_an_ability_is_rejected(NumberRule rule)
    {
        var ex = Assert.Throws<ArgumentException>(() => RulesContext.Create(
            LoadEffects(), [new DerivedNumber("test.capped", rule, 10, 12)]));
        Assert.Contains("test.capped", ex.Message, StringComparison.Ordinal);
    }

    // M1-09 plan call 5: the floor binds after the rule's whole result (Recovery "minimum 1", DH 8).
    [Fact]
    public void A_floor_binds_after_every_modifier()
    {
        var rules = RulesContext.Create(
            LoadEffects(
                Def("g.minus5", "gear", Add("test.floored", -5)),
                Def("g.plus3", "gear", Add("test.floored", 3))),
            [new DerivedNumber("test.floored", NumberRule.Stack, 2, null, 1)]);
        var s = Start();
        Assert.Equal(2, Effects.Number(rules, s, Hero, "test.floored"));
        Assert.Equal(1, Effects.Number(rules, Effects.Apply(s, "g.minus5", Hero, new EffectSource(Hero)), Hero, "test.floored"));
        Assert.Equal(5, Effects.Number(rules, Effects.Apply(s, "g.plus3", Hero, new EffectSource(Hero)), Hero, "test.floored"));
        Assert.Equal(1L, new DerivedNumber("test.f", NumberRule.Stack, a => 0, null, 1).Floor);
    }

    // M1-09 plan call 5: SM 12 leaves ability floors open, and speed 0 must override everything (CO 6.7).
    [Theory]
    [InlineData(NumberRule.AbilityScore)]
    [InlineData(NumberRule.Speed)]
    public void A_floor_on_an_ability_score_or_speed_number_is_rejected(NumberRule rule)
    {
        var ex = Assert.Throws<ArgumentException>(() => RulesContext.Create(
            LoadEffects(), [new DerivedNumber("test.floored", rule, 10, null, 1)]));
        Assert.Contains("test.floored", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_derived_number_with_no_id_or_no_base_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => RulesContext.Create(
            LoadEffects(), [new DerivedNumber("test.x", NumberRule.Stack, (NumberBase)null!, null)]));
        Assert.Throws<ArgumentException>(() => RulesContext.Create(
            LoadEffects(), [new DerivedNumber(null!, NumberRule.Stack, 1, null)]));
    }

    // An ability with no cap still takes only the best bonus and the worst penalty: 4 + 2 - 2, not the Stack sum.
    [Fact]
    public void An_uncapped_ability_score_takes_the_best_bonus_and_the_worst_penalty()
    {
        var rules = RulesContext.Create(
            LoadEffects(
                Def("t.plus2", "trait", Add("test.free", 2)),
                Def("g.plus1", "gear", Add("test.free", 1)),
                Def("g.minus1", "gear", Add("test.free", -1)),
                Def("g.minus2", "gear", Add("test.free", -2))),
            [new DerivedNumber("test.free", NumberRule.AbilityScore, 4, null)]);
        GameState Holding(params string[] effects)
        {
            var s = Start();
            foreach (var id in effects)
            {
                s = Effects.Apply(s, id, Hero, new EffectSource(Hero));
            }

            return s;
        }

        Assert.Equal(6, Effects.Number(rules, Holding("t.plus2", "g.plus1"), Hero, "test.free"));                         // not 7
        Assert.Equal(4, Effects.Number(rules, Holding("t.plus2", "g.plus1", "g.minus1", "g.minus2"), Hero, "test.free")); // 4 + 2 - 2
    }

    // --- Per-unit bases ---

    // Two bases reading the same number is not a cycle (a save reads two abilities; max HP and Recovery both read Con).
    [Fact]
    public void Two_bases_may_read_the_same_number()
    {
        var rules = RulesContext.Create(
            LoadEffects(),
            [
                new DerivedNumber("d.leaf", NumberRule.Stack, 3, null),
                new DerivedNumber("d.left", NumberRule.Stack, a => a.Number("d.leaf") + 1, null),
                new DerivedNumber("d.right", NumberRule.Stack, a => a.Number("d.leaf") + 2, null),
                new DerivedNumber("d.top", NumberRule.Stack, a => a.Number("d.left") + a.Number("d.right") + a.Number("d.leaf"), null),
            ]);
        Assert.Equal(12, Effects.Number(rules, Start(), Hero, "d.top"));
    }

    [Fact]
    public void A_base_can_differ_per_unit() =>
        Assert.Equal(
            (10L, 20L),
            (Effects.Number(Rules, Start(), Hero, "test.perUnit"), Effects.Number(Rules, Start(), Ally, "test.perUnit")));

    [Theory]
    [InlineData(14)]                                                          // 4 + 10
    [InlineData(15, "ab.trait.plus2")]                                        // the read number's effects and cap count: 5 + 10
    [InlineData(16, "ab.trait.plus2", "reads.gear.plus1")]                    // and the number's own effects add on top
    [InlineData(15, "ab.trait.plus3", "ab.cond.minus1", "reads.gear.plus1")]  // the read number after its fold: 4 + 10 + 1
    public void A_base_reads_another_number_after_its_effects(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "test.reads"));

    [Fact]
    public void A_base_reads_the_numbers_of_its_own_unit()
    {
        var s = Effects.Apply(Start(), "ab.trait.plus2", Ally, new EffectSource(Ally));
        Assert.Equal((14L, 15L), (Effects.Number(Rules, s, Hero, "test.reads"), Effects.Number(Rules, s, Ally, "test.reads")));
    }

    [Theory]
    [InlineData("cycle.a", "cycle.a -> cycle.b -> cycle.a")]
    [InlineData("cycle.b", "cycle.b -> cycle.a -> cycle.b")]
    [InlineData("cycle.self", "cycle.self -> cycle.self")]
    public void A_base_that_reads_itself_through_a_cycle_throws_with_the_path(string number, string path)
    {
        var rules = RulesContext.Create(
            LoadEffects(),
            [
                new DerivedNumber("cycle.a", NumberRule.Stack, a => a.Number("cycle.b"), null),
                new DerivedNumber("cycle.b", NumberRule.Stack, a => a.Number("cycle.a"), null),
                new DerivedNumber("cycle.self", NumberRule.Stack, a => a.Number("cycle.self") + 1, null),
            ]);
        var ex = Assert.Throws<InvalidOperationException>(() => Effects.Number(rules, Start(), Hero, number));
        Assert.Contains(path, ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void A_base_that_reads_an_unknown_number_throws()
    {
        var rules = RulesContext.Create(
            LoadEffects(), [new DerivedNumber("test.bad", NumberRule.Stack, a => a.Number("nope"), null)]);
        Assert.Throws<ArgumentException>(() => Effects.Number(rules, Start(), Hero, "test.bad"));
    }

    // --- Review Focus 3: speed ---

    [Theory]
    [InlineData(3, "trait.plus1", "cond.halve")]                  // 7 halves to 3, rounding down
    [InlineData(3, "cond.halve", "cond.halve2")]                  // two halvings halve once
    [InlineData(4, "gear.plus2", "cond.halve")]                   // flat changes first: (6 + 2) / 2
    [InlineData(0, "cond.zero", "gear.plus5")]                    // zero beats a later add
    [InlineData(0, "gear.plus5", "cond.zero", "cond.halve")]
    [InlineData(0, "gear.minus10")]                               // never below 0
    [InlineData(0, "gear.minus10", "cond.halve")]
    public void Speed_applies_flat_changes_then_one_halving_and_zero_overrides(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "speed"));

    // --- Roll mode (D-15) and flags ---

    [Theory]
    [InlineData(RollMode.Normal)]
    [InlineData(RollMode.Advantage, "adv", "adv2")]
    [InlineData(RollMode.Normal, "adv", "adv2", "dis")]           // D-15: one of each cancels, whatever the counts
    [InlineData(RollMode.Disadvantage, "dis")]
    public void Roll_mode_folds_by_d15(RollMode expected, params string[] effects) =>
        Assert.Equal(expected, Effects.RollModeOf(Rules, With(effects), Hero, "attackRoll"));

    [Fact]
    public void A_roll_mode_reads_only_its_target_and_its_holder()
    {
        var s = With("adv");
        Assert.Equal(RollMode.Normal, Effects.RollModeOf(Rules, s, Hero, "attacksAgainst"));
        Assert.Equal(RollMode.Normal, Effects.RollModeOf(Rules, s, Ally, "attackRoll"));
    }

    [Fact]
    public void A_flag_is_set_while_its_effect_is_active()
    {
        var s = With("hidden");
        Assert.True(Effects.Flag(Rules, s, Hero, "unit", "hidden"));
        Assert.False(Effects.Flag(Rules, s, Hero, "unit", "dodging"));
        Assert.False(Effects.Flag(Rules, s, Ally, "unit", "hidden"));
        Assert.False(Effects.Flag(Rules, Start(), Hero, "unit", "hidden"));
    }

    [Fact]
    public void A_flag_query_refuses_an_unknown_target_or_name()
    {
        var s = With("hidden");
        Assert.Throws<ArgumentException>(() => Effects.Flag(Rules, s, Hero, "unti", "hidden"));
        Assert.Throws<ArgumentException>(() => Effects.Flag(Rules, s, Hero, "unit", "hiden"));
    }

    [Fact]
    public void Only_the_holders_instances_count()
    {
        var s = Effects.Apply(Start(), "gear.plus2", Ally, new EffectSource(Hero));
        Assert.Equal(6, Effects.Number(Rules, s, Hero, "speed"));
        Assert.Equal(8, Effects.Number(Rules, s, Ally, "speed"));
    }

    // --- Purity and order independence ---

    [Fact]
    public void Folding_never_changes_the_state()
    {
        var s = With("gear.plus2", "cond.halve", "adv", "hidden");
        var before = Snapshot.Save(s);
        _ = Effects.Number(Rules, s, Hero, "speed");
        _ = Effects.Number(Rules, s, Hero, "test.ability");
        _ = Effects.RollModeOf(Rules, s, Hero, "attackRoll");
        _ = Effects.Flag(Rules, s, Hero, "unit", "hidden");
        Assert.Equal(before, Snapshot.Save(s));
    }

    [Fact]
    public void The_fold_does_not_depend_on_the_order_of_the_instances()
    {
        var s = With("gear.plus2", "cond.plus2", "cond.minus1", "cond.halve", "adv", "dis", "ab.trait.plus2", "ab.gear.minus2");
        var reversed = s with { Effects = [.. s.Effects.Reverse()] };
        var rotated = s with { Effects = [.. s.Effects.Skip(3), .. s.Effects.Take(3)] };
        foreach (var shuffled in new[] { reversed, rotated })
        {
            Assert.Equal(Effects.Number(Rules, s, Hero, "speed"), Effects.Number(Rules, shuffled, Hero, "speed"));
            Assert.Equal(Effects.Number(Rules, s, Hero, "test.ability"), Effects.Number(Rules, shuffled, Hero, "test.ability"));
            Assert.Equal(Effects.RollModeOf(Rules, s, Hero, "attackRoll"), Effects.RollModeOf(Rules, shuffled, Hero, "attackRoll"));
        }
    }

    // The display's "what would +1 change?" is the same fold on a hypothetical state; the real one is untouched.
    [Fact]
    public void What_would_one_more_change_is_the_fold_of_a_hypothetical_state()
    {
        var s = With("cond.plus2");
        var hypothetical = Effects.Apply(s, "trait.plus1", Hero, new EffectSource(Hero));
        Assert.Equal(9, Effects.Number(Rules, hypothetical, Hero, "speed"));
        Assert.Equal(8, Effects.Number(Rules, s, Hero, "speed"));
        Assert.Single(s.Effects);
    }

    // --- Apply and Remove ---

    [Fact]
    public void Apply_assigns_sequence_numbers_from_one_and_remove_keeps_the_counter()
    {
        var s = Start();
        Assert.Empty(s.Effects);
        Assert.Equal(1, s.NextEffectSeq);

        s = Effects.Apply(s, "gear.plus2", Hero, new EffectSource(Hero));
        s = Effects.Apply(s, "cond.plus2", Hero, new EffectSource(Ally));
        s = Effects.Apply(s, "cond.minus1", Ally, EffectSource.Environment);
        Assert.Equal<long>([1, 2, 3], s.Effects.Select(e => e.Seq).ToArray());
        Assert.Equal(4, s.NextEffectSeq);
        Assert.Equal(new EffectInstance(2, "cond.plus2", Hero, new EffectSource(Ally)), s.Effects[1]);
        Assert.Null(s.Effects[2].Source.Unit);

        s = Effects.Remove(s, 2);
        Assert.Equal<long>([1, 3], s.Effects.Select(e => e.Seq).ToArray());
        Assert.Equal(4, s.NextEffectSeq);
        s = Effects.Apply(s, "cond.plus2", Hero, new EffectSource(Hero));
        Assert.Equal(4, s.Effects[^1].Seq);
    }

    [Fact]
    public void Apply_and_remove_refuse_what_the_state_does_not_hold()
    {
        var s = Start();
        Assert.Throws<ArgumentException>(() => Effects.Apply(s, "gear.plus2", new UnitId(9), EffectSource.Environment));
        Assert.Throws<ArgumentException>(() => Effects.Apply(s, "gear.plus2", Hero, new EffectSource(new UnitId(9))));
        Assert.Throws<ArgumentException>(() => Effects.Apply(s, "Not An Id", Hero, EffectSource.Environment));
        Assert.Throws<ArgumentException>(() => Effects.Remove(s, 1));
        Assert.Throws<OverflowException>(
            () => Effects.Apply(s with { NextEffectSeq = long.MaxValue }, "gear.plus2", Hero, EffectSource.Environment));
    }

    // --- The rules context ---

    [Fact]
    public void The_empty_context_has_the_core_tables_and_no_definitions()
    {
        Assert.Equal(NumberRule.Speed, RulesContext.Empty.Number("speed").Rule);
        Assert.Equal(6, Effects.Number(RulesContext.Empty, Start(), Hero, "speed"));
        Assert.Throws<ArgumentException>(() => RulesContext.Empty.Definition("gear.plus2"));
        Assert.Throws<ArgumentException>(() => RulesContext.Empty.Number("test.stack"));
        Assert.Equal(EffectSourceKind.Gear, Rules.Definition("gear.plus2").Kind);
    }

    [Fact]
    public void The_repository_content_builds_with_the_core_tables() =>
        Assert.NotNull(RulesContext.Create(ContentLoader.Load(
            ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")))));

    [Theory]
    [InlineData("""{"target": "nope", "add": 1}""", "$.effects[1].modifiers[0].target")]
    [InlineData("""{"target": "attackRoll", "add": 1}""", "$.effects[1].modifiers[0].target")]
    [InlineData("""{"target": "nope", "rollMode": "advantage"}""", "$.effects[1].modifiers[0].target")]
    [InlineData("""{"target": "speed", "rollMode": "advantage"}""", "$.effects[1].modifiers[0].target")]
    [InlineData("""{"target": "test.stack", "halve": true}""", "$.effects[1].modifiers[0].halve")]
    [InlineData("""{"target": "test.stack", "zero": true}""", "$.effects[1].modifiers[0].zero")]
    [InlineData("""{"target": "unti", "flag": "hidden"}""", "$.effects[1].modifiers[0].target")]
    [InlineData("""{"target": "speed", "flag": "hidden"}""", "$.effects[1].modifiers[0].target")]
    [InlineData("""{"target": "unit", "flag": "hiden"}""", "$.effects[1].modifiers[0].flag")]
    public void A_bad_modifier_target_fails_with_the_file_and_field(string modifier, string field)
    {
        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(
            LoadEffects(Def("ok", "gear", Add("speed", 1)), Def("x.bad", "gear", modifier)), Numbers, flags: Flags));
        Assert.Equal((EffectsFile, field), (ex.File, ex.Field));
    }

    [Theory]
    [InlineData("""{"on": "nope", "scope": "holder", "do": [{"applyEffect": "ok", "to": "holder"}]}""", "$.effects[1].triggers[0].on")]
    [InlineData("""{"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "ok", "to": "holder"}, {"hook": "nope"}]}""", "$.effects[1].triggers[0].do[1].hook")]
    public void A_bad_trigger_reference_fails_with_the_file_and_field(string trigger, string field)
    {
        var ex = Assert.Throws<ContentLoadException>(() => RulesContext.Create(LoadEffects(
            Def("ok", "gear"),
            $$"""{"id": "x.bad", "kind": "gear", "triggers": [{{trigger}}]}""")));
        Assert.Equal((EffectsFile, field), (ex.File, ex.Field));
    }

    // --- Persistence ---

    [Fact]
    public void Effects_round_trip_through_a_save()
    {
        var s = Effects.Apply(Start(), "gear.plus2", Hero, new EffectSource(Ally));
        s = Effects.Apply(s, "cond.halve", Ally, EffectSource.Environment);
        s = Effects.Apply(s, "cond.zero", Hero, EffectSource.Environment);
        s = Effects.Remove(s, 3);
        var json = Snapshot.Save(s);
        Assert.Contains(
            "\"effects\":[{\"seq\":1,\"effect\":\"gear.plus2\",\"holder\":1,\"source\":2},"
            + "{\"seq\":2,\"effect\":\"cond.halve\",\"holder\":2,\"source\":null}],\"nextEffectSeq\":4",
            json);
        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal(s.Effects.AsEnumerable(), loaded.Effects);
        Assert.Equal(4, loaded.NextEffectSeq);
        Assert.Equal(StateDigest.Of(s), StateDigest.Of(loaded));
    }

    [Fact]
    public void Load_rejects_effects_the_rules_cannot_produce()
    {
        var s = Effects.Apply(Effects.Apply(Start(), "gear.plus2", Hero, new EffectSource(Ally)), "cond.halve", Ally, EffectSource.Environment);
        var e = s.Effects;
        Assert.Equal(Snapshot.Save(s), Snapshot.Save(Snapshot.Load(Snapshot.Save(s))));
        var bad = new[]
        {
            s with { Effects = [e[0], e[1] with { Seq = 1 }] },                                 // duplicate seq
            s with { Effects = [e[0] with { Seq = 0 }, e[1]] },                                 // non-positive seq
            s with { Effects = [e[0] with { Seq = -4 }, e[1]] },
            s with { Effects = [e[0], e[1] with { Seq = 3 }] },                                 // seq >= nextEffectSeq
            s with { NextEffectSeq = 2 },
            s with { Effects = [e[0], e[1] with { Holder = new UnitId(9) }] },                  // unknown holder
            s with { Effects = [e[0] with { Source = new EffectSource(new UnitId(9)) }, e[1]] }, // unknown source
            s with { Effects = [e[0] with { EffectId = "Not An Id" }, e[1]] },                  // not a content id
            s with { Effects = [], NextEffectSeq = 0 },                                         // counter below 1
        };
        for (int i = 0; i < bad.Length; i++)
        {
            var json = Snapshot.Save(bad[i]);
            Assert.True(
                Record.Exception(() => Snapshot.Load(json)) is SnapshotFormatException,
                $"Bad state {i} loaded.");
        }
    }

    [Fact]
    public void Save_writes_the_effects_in_seq_order_whatever_the_stored_order()
    {
        var s = With("gear.plus2", "cond.halve", "adv");
        var shuffled = s with { Effects = [s.Effects[2], s.Effects[0], s.Effects[1]] };
        var json = Snapshot.Save(shuffled);
        Assert.Equal(Snapshot.Save(s), json);
        Assert.Equal(s.Effects.AsEnumerable(), Snapshot.Load(json).Effects);
    }

    [Fact]
    public void Load_rejects_effects_out_of_seq_order()
    {
        var s = Effects.Apply(Effects.Apply(Start(), "gear.plus2", Hero, new EffectSource(Ally)), "cond.halve", Ally, EffectSource.Environment);
        const string First = "{\"seq\":1,\"effect\":\"gear.plus2\",\"holder\":1,\"source\":2}";
        const string Second = "{\"seq\":2,\"effect\":\"cond.halve\",\"holder\":2,\"source\":null}";
        var good = Snapshot.Save(s);
        Assert.Contains(First + "," + Second, good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(First + "," + Second, Second + "," + First)));
    }

    [Theory]
    [InlineData("\"source\":null", "")]                                  // a field Save always writes
    [InlineData("\"effects\":[", "\"effects\":[null,")]
    [InlineData("\"nextEffectSeq\":3", "\"nextEffectSeq\":\"3\"")]
    public void Load_rejects_damaged_effect_fields(string find, string replace)
    {
        var s = Effects.Apply(Effects.Apply(Start(), "gear.plus2", Hero, new EffectSource(Ally)), "cond.halve", Ally, EffectSource.Environment);
        var good = Snapshot.Save(s);
        Assert.Contains(find, good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(find, replace)));
    }

    // --- Digest coverage ---

    [Fact]
    public void Effects_reach_the_digest_as_a_set_keyed_by_seq()
    {
        var s = With("gear.plus2", "cond.halve");
        Assert.Equal(StateDigest.Of(s), StateDigest.Of(s with { Effects = [.. s.Effects.Reverse()] }));
        Assert.NotEqual(StateDigest.Of(s), StateDigest.Of(s with { Effects = [s.Effects[0]] }));
        Assert.NotEqual(StateDigest.Of(s), StateDigest.Of(s with { NextEffectSeq = s.NextEffectSeq + 1 }));
    }

    // --- Helpers ---

    // Two units: the hero at (1,1) and an ally at (3,3) in the ring room.
    private static GameState Start() => RingState(U(2, 3, 3));

    // The start state with the named effects applied to the hero, in order, from the hero.
    private static GameState With(params string[] effects)
    {
        var s = Start();
        foreach (var id in effects)
        {
            s = Effects.Apply(s, id, Hero, new EffectSource(Hero));
        }

        return s;
    }

    private static string Add(string target, int value) => $$"""{"target": "{{target}}", "add": {{value}}}""";

    private static string Def(string id, string kind, params string[] modifiers) =>
        $$"""{"id": "{{id}}", "kind": "{{kind}}", "modifiers": [{{string.Join(", ", modifiers)}}]}""";

    private static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile(EffectsFile, "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}
