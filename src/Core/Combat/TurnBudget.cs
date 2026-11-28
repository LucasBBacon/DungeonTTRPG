using System.Collections.Immutable;

namespace Dungeon.Core.Combat;

/// <summary>
/// A unit's turn budget (AB 14.2), stored as what has been used, never what is left (M1-10 plan call 5; D-18,
/// subtract, not rescale: M1-10 rulings record 2.1): movement left is the folded speed minus
/// <see cref="MovementUsed"/>, never below 0. <see cref="CampSpent"/> is the AB 6.4
/// "camp spend used" flag. The default value is a fresh turn.
/// </summary>
public readonly record struct TurnBudget(
    int MovementUsed, int ActionsUsed, bool BonusUsed, bool ReactionUsed, bool FreeUsed, bool CampSpent);

/// <summary>How much of one pool a unit has spent (AB 6.2: the state stores only spent counts).</summary>
public readonly record struct PoolSpend(string PoolId, int Spent);

/// <summary>
/// An opener's charge to its striker's round-1 turn (M1-11 plan call 6; M1-10 rulings record 5, steps 8 and 9), held
/// on <see cref="Unit.Opener"/> from the opener until the striker's first turn starts. <see cref="Charged"/> holds only
/// the opener's own cost bits (action, bonus, free or movement); its <see cref="TurnBudget.ReactionUsed"/> and
/// <see cref="TurnBudget.CampSpent"/> are never set. <see cref="TurnPools"/> holds the counts the opener spent from
/// <c>turn</c> pools, sorted by pool id in ordinal order, each pool once and each count at least 1. At the striker's
/// first turn start its budget becomes <see cref="Charged"/> and those counts are written back after the turn refill.
/// The record compares <see cref="TurnPools"/> by reference; compare it with <c>SequenceEqual</c>.
/// </summary>
public sealed record OpenerCharge(TurnBudget Charged, ImmutableArray<PoolSpend> TurnPools);
