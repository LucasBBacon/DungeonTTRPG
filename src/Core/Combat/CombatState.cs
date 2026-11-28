using System.Collections.Immutable;

namespace Dungeon.Core.Combat;

/// <summary>
/// A fight in progress (M1-10 plan call 4): the initiative order fixed at combat start, the index of the slot whose turn
/// it is, and the round (from 1). <see cref="TurnVacated"/> is set when no unit holds the turn: its holder died, or its
/// turn ended and the next has not started (<c>Turns.EndTurn</c>; M1-12a plan call 10, which widens the M1-10 meaning).
/// Then <see cref="Current"/> is the index the next turn takes (in the shrunk order, after a death), and
/// <see cref="Current"/> equal to the order's length (an empty order included) means the next turn wraps to slot 0 in a
/// new round.
/// <see cref="BeforeRound1"/> is the "before round 1" marker (M1-11 plan call 7; M1-10 rulings record 5, step 6): the
/// fight is recorded but round 1 has not started, so no unit holds the turn; it is valid only with round 1, current 0,
/// the turn not vacated and a non-empty order. The record compares <see cref="Order"/> by reference; compare orders
/// with <c>SequenceEqual</c>.
/// </summary>
public sealed record CombatState(
    ImmutableArray<UnitId> Order, int Current, long Round, bool TurnVacated = false, bool BeforeRound1 = false)
{
    /// <summary>
    /// The unit in the current slot: the unit whose turn it is, unless <see cref="TurnVacated"/> or
    /// <see cref="BeforeRound1"/> is set (then no unit holds the turn).
    /// </summary>
    public UnitId CurrentUnit => Order[Current];
}

/// <summary>Why a run ended (DH 10.1, D-08). Only <see cref="HeroDeath"/> is produced in M1-10.</summary>
public enum RunEndCause { HeroDeath, Victory, Unravelled }

/// <summary>
/// The end of a run (DH 10.1, D-08): its cause and, for <see cref="RunEndCause.HeroDeath"/> only, the sequence id of
/// the damage event that killed the hero. After it the core refuses every command.
/// </summary>
public sealed record RunEnd(RunEndCause Cause, long? KillingEventId);
