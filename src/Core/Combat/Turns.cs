using System;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.Combat;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The turn loop (M1-10 plan calls 4, 6, 8, 9): combat start, each turn's start and end, and resuming a turn its holder
/// vacated by dying. The turn-start and turn-end steps (CO 5.4) are empty seams that M1-12 fills.
/// </summary>
public static class Turns
{
    /// <summary>
    /// Starts a fight (plan call 6): rolls initiative (<see cref="Initiative.Roll"/>, one d20 per unit on
    /// <c>Rng.Combat</c>) over every unit in the state, stores the order with round 1 and slot 0, gives every unit a
    /// fresh turn budget and refills every <see cref="RefillTag.Fight"/> pool, emits <see cref="CombatStarted"/> and then
    /// starts the first unit's turn (<see cref="StartTurn"/>). Throws <see cref="InvalidOperationException"/> when
    /// already in combat or the run has ended, and <see cref="ArgumentException"/> when the state has no units.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) StartCombat(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        if (s.Ended is not null)
        {
            throw new InvalidOperationException("Cannot start combat: the run has ended.");
        }

        if (s.Combat is not null)
        {
            throw new InvalidOperationException("Cannot start combat: a fight is already in progress.");
        }

        if (s.Units.IsEmpty)
        {
            throw new ArgumentException("Cannot start combat with no units.", nameof(s));
        }

        // Who joins a fight is M1-35's; until then every unit in the state does, in id order.
        var (order, next) = Initiative.Roll(rules, s, s.Units.Select(u => u.Id));

        next = next with
        {
            Units = next.Units.Select(u => u.Budget == default ? u : u with { Budget = default }).ToImmutableArray(),
        };
        next = Pools.RefillAll(rules, next, RefillTag.Fight);

        // TL-19's combat-start seam (D-19, M1-30) goes here.
        next = next with { Combat = new CombatState(order.Select(e => e.Unit).ToImmutableArray(), 0, 1) };

        var (turn, started) = StartTurn(rules, next);
        return ([new CombatStarted(order), .. turn], started);
    }

    /// <summary>
    /// The current unit's turn start (plan call 6): its budget becomes fresh, its <see cref="RefillTag.Turn"/> pools
    /// refill, and every unit's <see cref="TurnBudget.CampSpent"/> clears (AB 6.4); then the turn-start step (empty
    /// until M1-12) and <see cref="TurnStarted"/>. Throws <see cref="InvalidOperationException"/> out of combat, on an
    /// ended run, or while the turn is vacated.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) StartTurn(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        var combat = LiveTurn(s);
        var unit = combat.CurrentUnit;

        var units = s.Units.ToBuilder();
        for (int i = 0; i < units.Count; i++)
        {
            var u = units[i];
            var budget = u.Id == unit ? default : u.Budget with { CampSpent = false };
            if (budget != u.Budget)
            {
                units[i] = u with { Budget = budget };
            }
        }

        var next = Pools.Refill(rules, s with { Units = units.ToImmutable() }, unit, RefillTag.Turn);

        // CO 5.4's turn-start step (conditions, durations, the zero-HP backstop) goes here: M1-12.
        return ([new TurnStarted(unit, combat.Round)], next);
    }

    /// <summary>
    /// Ends the current unit's turn and starts the next (plan call 9): emits <see cref="TurnEnded"/>, runs the turn-end
    /// step (empty until M1-12), moves to the next slot (passing the last slot wraps to slot 0 and round + 1), then
    /// <see cref="StartTurn"/>. Throws <see cref="InvalidOperationException"/> out of combat, on an ended run, or while
    /// the turn is vacated.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) Advance(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        var combat = LiveTurn(s);
        var ended = new TurnEnded(combat.CurrentUnit);

        // CO 5.4's turn-end step goes here: M1-12.
        int current = combat.Current + 1;
        long round = combat.Round;
        if (current == combat.Order.Length)
        {
            current = 0;
            round = checked(round + 1);
        }

        var (turn, next) = StartTurn(rules, s with { Combat = combat with { Current = current, Round = round } });
        return ([ended, .. turn], next);
    }

    /// <summary>
    /// Starts the turn a dead unit vacated (plan call 11; callers run it after <c>CombatEnd.EndIfWon</c>, once the
    /// command's queue has drained). It does nothing, returning <paramref name="s"/> itself, unless combat is active, the
    /// run has not ended, <see cref="CombatState.TurnVacated"/> is set and the order is non-empty. Otherwise it clears
    /// the flag, wraps <see cref="CombatState.Current"/> equal to the order's length to slot 0 in round + 1, and calls
    /// <see cref="StartTurn"/>.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) ResumeIfVacated(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var combat = s.Combat;
        if (combat is null || s.Ended is not null || !combat.TurnVacated || combat.Order.IsEmpty)
        {
            return (ImmutableArray<GameEvent>.Empty, s);
        }

        combat = combat.Current == combat.Order.Length
            ? combat with { Current = 0, Round = checked(combat.Round + 1), TurnVacated = false }
            : combat with { TurnVacated = false };
        return StartTurn(rules, s with { Combat = combat });
    }

    /// <summary>
    /// <paramref name="unit"/>'s movement left this turn (plan call 5): its folded <c>speed</c> minus its
    /// <see cref="TurnBudget.MovementUsed"/>, never below 0. Throws <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static long MovementLeft(RulesContext rules, GameState s, UnitId unit)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var holder = s.FindUnit(unit) ?? throw new ArgumentException($"Unknown unit {unit.Value}.", nameof(unit));
        return Math.Max(0, checked(Effects.Number(rules, s, unit, "speed") - holder.Budget.MovementUsed));
    }

    // The combat state of a turn some unit holds: in combat, not ended, not vacated.
    private static CombatState LiveTurn(GameState s)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.Ended is not null)
        {
            throw new InvalidOperationException("The run has ended.");
        }

        var combat = s.Combat ?? throw new InvalidOperationException("No fight is in progress.");
        if (combat.TurnVacated)
        {
            throw new InvalidOperationException("The turn is vacated; resume it with ResumeIfVacated.");
        }

        return combat;
    }
}
