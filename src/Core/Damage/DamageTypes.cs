using System;
using System.Collections.Immutable;
using System.Linq;

namespace Dungeon.Core.Damage;

/// <summary>The ten damage types (SM 8.1), in the design's order.</summary>
public enum DamageType
{
    Slashing,
    Piercing,
    Bludgeoning,
    Fire,
    Cold,
    Lightning,
    Acid,
    Psychic,
    Radiant,
    Necrotic,
}

/// <summary>
/// A unit's step on the damage ladder for one type (SM 8.3): Weak "Damage + half", Normal, Resistant "Half damage",
/// Immune "No damage". <see cref="Ladder.StepFor"/> reads it from the unit's flags; <see cref="Ladder.Apply"/> applies it.
/// </summary>
public enum LadderStep
{
    Weak,
    Normal,
    Resistant,
    Immune,
}

/// <summary>
/// The content ids of the damage types and their ladder flags (M1-09 plan call 6): <c>resistant.&lt;type&gt;</c>,
/// <c>weak.&lt;type&gt;</c> and <c>immune.&lt;type&gt;</c>, one of each per type. They are rows of
/// <see cref="Dungeon.Core.Effects.Flags.Core"/>, so an effect sets them with a <c>flag</c> modifier on <c>unit</c>.
/// </summary>
public static class DamageIds
{
    /// <summary>The prefix of a resistance flag.</summary>
    public const string ResistantPrefix = "resistant.";

    /// <summary>The prefix of a weakness flag.</summary>
    public const string WeakPrefix = "weak.";

    /// <summary>The prefix of an immunity flag.</summary>
    public const string ImmunePrefix = "immune.";

    // SM 8.1 order, as the enum declares it.
    private static readonly ImmutableArray<DamageType> AllTypes =
        [
            DamageType.Slashing, DamageType.Piercing, DamageType.Bludgeoning, DamageType.Fire, DamageType.Cold,
            DamageType.Lightning, DamageType.Acid, DamageType.Psychic, DamageType.Radiant, DamageType.Necrotic,
        ];

    /// <summary>The 30 ladder flags, three per type, ordinal-sorted.</summary>
    public static ImmutableArray<string> LadderFlags { get; } = AllTypes
        .SelectMany(t => new[] { Resistant(t), Weak(t), Immune(t) })
        .OrderBy(f => f, StringComparer.Ordinal)
        .ToImmutableArray();

    /// <summary>The content id of <paramref name="type"/>: its lowercase name, such as <c>fire</c>.</summary>
    public static string Of(DamageType type) => type switch
    {
        DamageType.Slashing => "slashing",
        DamageType.Piercing => "piercing",
        DamageType.Bludgeoning => "bludgeoning",
        DamageType.Fire => "fire",
        DamageType.Cold => "cold",
        DamageType.Lightning => "lightning",
        DamageType.Acid => "acid",
        DamageType.Psychic => "psychic",
        DamageType.Radiant => "radiant",
        DamageType.Necrotic => "necrotic",
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Not a damage type."),
    };

    /// <summary>The resistance flag of <paramref name="type"/>, such as <c>resistant.fire</c>.</summary>
    public static string Resistant(DamageType type) => ResistantPrefix + Of(type);

    /// <summary>The weakness flag of <paramref name="type"/>, such as <c>weak.fire</c>.</summary>
    public static string Weak(DamageType type) => WeakPrefix + Of(type);

    /// <summary>The immunity flag of <paramref name="type"/>, such as <c>immune.fire</c>.</summary>
    public static string Immune(DamageType type) => ImmunePrefix + Of(type);
}

/// <summary>
/// One typed part of a hit's damage (SM 9: "Damage travels as a list of typed parts"; AB 9.1): <paramref name="Dice"/>
/// dice of <paramref name="Sides"/> sides plus a signed <paramref name="Flat"/> amount. <c>Dice</c> must be at least 0,
/// and <c>Sides</c> at least 1 when <c>Dice</c> is above 0; the damage function refuses a part that breaks this,
/// naming its index. M1 parts carry no rider (AB 9.1; M1-09 plan call 9).
/// </summary>
public readonly record struct DamagePart(DamageType Type, int Dice, int Sides, int Flat);
