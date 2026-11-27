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

    // The fixture numbers: a stacking number, a capped stacking number and a capped ability score, plus the core speed.
    private static readonly ImmutableArray<DerivedNumber> Numbers =
        [
            new DerivedNumber("test.stack", NumberRule.Stack, 10, null),
            new DerivedNumber("test.capped", NumberRule.Stack, 10, 12),
            new DerivedNumber("test.ability", NumberRule.AbilityScore, 4, 5),
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
            Def("cap.gear.plus2", "gear", Add("test.capped", 2)),
            Def("cap.gear.plus2b", "gear", Add("test.capped", 2)),
            Def("cap.cond.minus3", "condition", Add("test.capped", -3))),
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

    [Theory]
    [InlineData(3, "ab.trait.plus2", "ab.gear.plus1", "ab.gear.minus3")]   // highest bonus only (+2), not +3
    [InlineData(2, "ab.cond.minus1", "ab.gear.minus2")]                    // worst penalty only (-2), not -3
    [InlineData(2, "ab.trait.minus1", "ab.gear.minus2")]                   // across stacking kinds too
    [InlineData(5, "ab.trait.plus3")]                                      // 4 + 3 = 7, capped at 5
    [InlineData(5, "ab.trait.plus3", "ab.cond.minus1")]                    // cap after both: min(4 + 3 - 1, 5)
    [InlineData(4)]
    public void An_ability_score_takes_the_best_bonus_and_worst_penalty_then_the_cap(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "test.ability"));

    // The cap applies last for every number rule, after the bonuses and the penalties.
    [Theory]
    [InlineData(12, "cap.gear.plus2", "cap.gear.plus2b")]                     // 10 + 4 = 14, capped at 12
    [InlineData(11, "cap.gear.plus2", "cap.gear.plus2b", "cap.cond.minus3")]  // cap after the penalty: min(10 + 4 - 3, 12)
    [InlineData(10)]
    public void A_capped_stack_number_applies_the_cap_last(long expected, params string[] effects) =>
        Assert.Equal(expected, Effects.Number(Rules, With(effects), Hero, "test.capped"));

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
        Assert.Equal(6, RulesContext.Empty.Number("speed").Base);
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
