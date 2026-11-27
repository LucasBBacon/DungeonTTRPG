using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The ten damage types, their ladder flags and the ladder (M1-09 Task 3): the step a unit's active effects give a type
/// (plan call 6) and the part arithmetic (plan call 7, owner ruling 1), with Review Focus 1 and 2.
/// </summary>
public class LadderTests
{
    private const string EffectsFile = "effects/effects.json";

    private static readonly UnitId Ally = new(2);

    // Built with the core flag table, so every ladder flag below must be a registered core flag.
    private static readonly RulesContext Rules = RulesContext.Create(
        LoadEffects(
            Def("trait.resist-fire", "trait", Flag("resistant.fire")),
            Def("trait.resist-fire-2", "trait", Flag("resistant.fire")),
            Def("cond.weak-fire", "condition", Flag("weak.fire")),
            Def("trait.weak-fire", "trait", Flag("weak.fire")),
            Def("trait.immune-psychic", "trait", Flag("immune.psychic")),
            Def("cond.weak-psychic", "condition", Flag("weak.psychic")),
            Def("trait.test-skeletal", "trait", Flag("resistant.piercing"), Flag("weak.bludgeoning"))));

    // --- The part arithmetic (plan call 7, owner ruling 1; Review Focus 1) ---

    [Theory]
    [InlineData(LadderStep.Weak, 7, 10)]          // damage + half, rounding down
    [InlineData(LadderStep.Weak, 1, 1)]
    [InlineData(LadderStep.Normal, 7, 7)]
    [InlineData(LadderStep.Resistant, 7, 3)]      // half, rounding down
    [InlineData(LadderStep.Resistant, 1, 1)]      // half of 1 is 0, raised to the minimum 1
    [InlineData(LadderStep.Immune, 7, 0)]
    [InlineData(LadderStep.Normal, -3, 1)]        // 1d4 - 5 rolling 2: at least 1, not 0 or -3
    [InlineData(LadderStep.Resistant, 0, 1)]
    [InlineData(LadderStep.Immune, -3, 0)]        // the same part against an immune target: 0
    public void Ladder_arithmetic_matches_SM_8_3(LadderStep step, long raw, long dealt) =>
        Assert.Equal(dealt, Ladder.Apply(step, raw));

    // --- The step from flags (plan call 6) ---

    [Fact]
    public void No_flag_reads_normal()
    {
        foreach (var type in Enum.GetValues<DamageType>())
        {
            Assert.Equal(LadderStep.Normal, Ladder.StepFor(Rules, Start(), Hero, type));
        }
    }

    [Fact]
    public void A_resistant_flag_reads_resistant() =>
        Assert.Equal(LadderStep.Resistant, Ladder.StepFor(Rules, With("trait.resist-fire"), Hero, DamageType.Fire));

    [Fact]
    public void A_weak_flag_reads_weak() =>
        Assert.Equal(LadderStep.Weak, Ladder.StepFor(Rules, With("trait.weak-fire"), Hero, DamageType.Fire));

    // SM 8.3: "Two sources of the same resistance count once." Flags are booleans, so this holds by construction.
    [Fact]
    public void Two_resistances_count_once()
    {
        var step = Ladder.StepFor(Rules, With("trait.resist-fire", "trait.resist-fire-2"), Hero, DamageType.Fire);
        Assert.Equal(LadderStep.Resistant, step);
        Assert.Equal(4, Ladder.Apply(step, 9));   // not 2
    }

    // SM 8.3: "A resistance and a weakness to the same type cancel to normal." Review Focus 2: two trait resistances
    // and a condition weakness read Normal, and 7 fire deals 7.
    [Fact]
    public void Resistance_and_weakness_cancel()
    {
        Assert.Equal(
            LadderStep.Normal, Ladder.StepFor(Rules, With("trait.resist-fire", "cond.weak-fire"), Hero, DamageType.Fire));

        var step = Ladder.StepFor(
            Rules, With("trait.resist-fire", "trait.resist-fire-2", "cond.weak-fire"), Hero, DamageType.Fire);
        Assert.Equal(LadderStep.Normal, step);
        Assert.Equal(7, Ladder.Apply(step, 7));
    }

    [Fact]
    public void Immunity_wins_over_weakness() =>
        Assert.Equal(
            LadderStep.Immune,
            Ladder.StepFor(Rules, With("trait.immune-psychic", "cond.weak-psychic"), Hero, DamageType.Psychic));

    [Fact]
    public void Steps_are_per_type()
    {
        var s = With("trait.resist-fire");
        Assert.Equal(LadderStep.Resistant, Ladder.StepFor(Rules, s, Hero, DamageType.Fire));
        Assert.Equal(LadderStep.Normal, Ladder.StepFor(Rules, s, Hero, DamageType.Cold));
        Assert.Equal(LadderStep.Normal, Ladder.StepFor(Rules, s, Ally, DamageType.Fire));   // and per unit
    }

    // SM 8.4: a trait "always means the same profile"; SM 9: a trait is a named bundle of effects.
    [Fact]
    public void A_trait_effect_carries_its_profile()
    {
        var s = With("trait.test-skeletal");
        foreach (var type in Enum.GetValues<DamageType>())
        {
            var expected = type switch
            {
                DamageType.Piercing => LadderStep.Resistant,
                DamageType.Bludgeoning => LadderStep.Weak,
                _ => LadderStep.Normal,
            };
            Assert.Equal(expected, Ladder.StepFor(Rules, s, Hero, type));
        }
    }

    // --- The ids ---

    [Fact]
    public void DamageIds_cover_every_type_once()
    {
        var types = Enum.GetValues<DamageType>();
        Assert.Equal(
            ["slashing", "piercing", "bludgeoning", "fire", "cold", "lightning", "acid", "psychic", "radiant", "necrotic"],
            types.Select(DamageIds.Of).ToArray());
        Assert.Equal(("resistant.fire", "weak.fire", "immune.fire"),
            (DamageIds.Resistant(DamageType.Fire), DamageIds.Weak(DamageType.Fire), DamageIds.Immune(DamageType.Fire)));

        var expected = types
            .SelectMany(t => new[] { DamageIds.Resistant(t), DamageIds.Weak(t), DamageIds.Immune(t) })
            .ToArray();
        Assert.Equal(30, expected.Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(30, DamageIds.LadderFlags.Length);
        Assert.Equal(
            expected.OrderBy(x => x, StringComparer.Ordinal).ToArray(),
            DamageIds.LadderFlags.ToArray());
        Assert.All(DamageIds.LadderFlags, f => Assert.Contains(f, Dungeon.Core.Effects.Flags.Core));
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

    private static string Flag(string name) => $$"""{"target": "unit", "flag": "{{name}}"}""";

    private static string Def(string id, string kind, params string[] modifiers) =>
        $$"""{"id": "{{id}}", "kind": "{{kind}}", "modifiers": [{{string.Join(", ", modifiers)}}]}""";

    private static ContentSet LoadEffects(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile(EffectsFile, "{\"effects\": [" + string.Join(", ", records) + "]}")],
            [new EffectsKind()]);
}
