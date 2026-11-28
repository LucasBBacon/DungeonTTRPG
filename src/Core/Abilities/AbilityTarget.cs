using System.Collections.Immutable;

namespace Dungeon.Core.AbilityModel;

/// <summary>
/// What a <see cref="UseAbilityCommand"/> aims at (AB 8.1): named units for a <see cref="TargetShape.Single"/> ability, a
/// point for a <see cref="TargetShape.Burst"/>, a direction for a <see cref="TargetShape.Cone"/> or
/// <see cref="TargetShape.Line"/>. Any other pairing is <see cref="CommandError.InvalidTarget"/>.
/// </summary>
public abstract record AbilityTarget;

/// <summary>
/// The units a single-target ability names: 1 to the ability's count, distinct, living, never the user. The record
/// compares <see cref="Units"/> by reference; compare it with <c>SequenceEqual</c>.
/// </summary>
public sealed record UnitsTarget(ImmutableArray<UnitId> Units) : AbilityTarget;

/// <summary>A burst's centre: in bounds and not a wall.</summary>
public sealed record PointTarget(GridPos Point) : AbilityTarget;

/// <summary>A cone's or line's direction, one of the eight.</summary>
public sealed record AimTarget(Direction Aim) : AbilityTarget;
