using System.Collections.Immutable;

namespace Dungeon.Core.Combat;

/// <summary>
/// A fight in progress (M1-10 plan call 4): the initiative order fixed at combat start, the index of the slot whose turn
/// it is, and the round (from 1). <see cref="TurnVacated"/> is set when the unit holding the turn died: then
/// <see cref="Current"/> is the index the next turn takes in the shrunk order, and <see cref="Current"/> equal to the
/// order's length (an empty order included) means the next turn wraps to slot 0 in a new round. The record compares
/// <see cref="Order"/> by reference; compare orders with <c>SequenceEqual</c>.
/// </summary>
public sealed record CombatState(ImmutableArray<UnitId> Order, int Current, long Round, bool TurnVacated = false)
{
    /// <summary>The unit whose turn it is. Valid only when <see cref="TurnVacated"/> is false.</summary>
    public UnitId CurrentUnit => Order[Current];
}

/// <summary>Why a run ended (DH 10.1, D-08). Only <see cref="HeroDeath"/> is produced in M1-10.</summary>
public enum RunEndCause { HeroDeath, Victory, Unravelled }

/// <summary>
/// The end of a run (DH 10.1, D-08): its cause and, for <see cref="RunEndCause.HeroDeath"/> only, the sequence id of
/// the damage event that killed the hero. After it the core refuses every command.
/// </summary>
public sealed record RunEnd(RunEndCause Cause, long? KillingEventId);
