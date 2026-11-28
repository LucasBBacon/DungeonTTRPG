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
    /// Starts a fight with no opener: <see cref="BeginCombat"/>, then <see cref="BeginRound1"/> (M1-11 plan call 7), so
    /// its events are <see cref="CombatStarted"/> then the first unit's <see cref="TurnStarted"/>, and it draws one d20
    /// per unit. Throws as <see cref="BeginCombat"/> does.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) StartCombat(RulesContext rules, GameState s)
    {
        var (begun, held) = BeginCombat(rules, s);
        var (round1, next) = BeginRound1(rules, held);
        return ([.. begun, .. round1], next);
    }

    /// <summary>
    /// The combat-start step up to recording the fight (M1-11 plan call 7; M1-10 rulings record 5, steps 0 to 7): the
    /// checks; every unit joins, in id order; the held messages (none until M1-12 and M1-13); every unit gets a fresh
    /// turn budget and every <see cref="RefillTag.Fight"/> pool refills (step 4); initiative
    /// (<see cref="Initiative.Roll"/>, one d20 per unit on <c>Rng.Combat</c>; step 5); the fight is recorded with round 1,
    /// slot 0 and the <see cref="CombatState.BeforeRound1"/> marker, so no unit holds the turn yet; and
    /// <see cref="CombatStarted"/>, then the held messages. Throws <see cref="InvalidOperationException"/> when already
    /// in combat or the run has ended, and <see cref="ArgumentException"/> when the state has no units.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) BeginCombat(RulesContext rules, GameState s)
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

        // 1. Who joins a fight is M1-35's; until then every unit in the state does, in id order.
        // 2. CO 5.5's combat-start condition step: M1-12; TL-19's clause is M2 (D-19(a)). 3. Surprise: M1-13, M1-35.
        //    Their messages are held for step 7.
        var held = ImmutableArray<GameEvent>.Empty;

        // 4. Fresh turn budgets, then the per-fight use limits refill. 5. Initiative (draw-neutral after step 4).
        var next = Pools.RefillAll(rules, FreshBudgets(s), RefillTag.Fight);
        var (order, rolled) = Initiative.Roll(rules, next, next.Units.Select(u => u.Id));

        // 6. Record the fight with the before-round-1 marker.
        rolled = rolled with
        {
            Combat = new CombatState(order.Select(e => e.Unit).ToImmutableArray(), 0, 1, BeforeRound1: true),
        };

        // 7. CombatStarted, then the held messages.
        return ([new CombatStarted(order), .. held], rolled);
    }

    /// <summary>
    /// Clears the <see cref="CombatState.BeforeRound1"/> marker and starts slot 0's turn (<see cref="StartTurn"/>;
    /// M1-10 rulings record 5, step 9). Throws <see cref="InvalidOperationException"/> out of combat, on an ended run, or
    /// when the marker is not set.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) BeginRound1(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        if (s.Ended is not null)
        {
            throw new InvalidOperationException("The run has ended.");
        }

        var combat = s.Combat ?? throw new InvalidOperationException("No fight is in progress.");
        if (!combat.BeforeRound1)
        {
            throw new InvalidOperationException("Round 1 has already started.");
        }

        return StartTurn(rules, s with { Combat = combat with { BeforeRound1 = false } });
    }

    /// <summary>
    /// Marks <paramref name="striker"/> as "opener charged, first turn not yet started" (M1-11 plan call 6). It sets only
    /// the mark: the cost itself is paid beforehand. Throws <see cref="InvalidOperationException"/> unless the fight is
    /// held before round 1, the run has not ended, the striker's budget already includes
    /// <see cref="OpenerCharge.Charged"/>, its spent counts include <see cref="OpenerCharge.TurnPools"/>, and no unit of
    /// its faction is marked; <see cref="ArgumentException"/> for an unknown unit or a malformed charge.
    /// </summary>
    internal static GameState MarkOpener(GameState s, UnitId striker, OpenerCharge charge)
    {
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(charge);
        if (s.Ended is not null || s.Combat is not { BeforeRound1: true })
        {
            throw new InvalidOperationException("An opener is marked only while the fight is held before round 1.");
        }

        var unit = s.FindUnit(striker) ?? throw new ArgumentException($"Unknown unit {striker.Value}.", nameof(striker));
        if (charge.Charged.ReactionUsed || charge.Charged.CampSpent || charge.TurnPools.IsDefault)
        {
            throw new ArgumentException(
                "An opener charge sets no reaction or camp-spend flag and has initialised turn pools.", nameof(charge));
        }

        if (!GameState.Includes(unit.Budget, charge.Charged))
        {
            throw new InvalidOperationException($"Unit {striker.Value}'s budget does not include the opener's charge.");
        }

        foreach (var pool in charge.TurnPools)
        {
            if (GameState.SpentCount(unit.Spent, pool.PoolId) < pool.Spent)
            {
                throw new InvalidOperationException(
                    $"Unit {striker.Value} has not spent the {pool.Spent} of pool '{pool.PoolId}' the opener charges.");
            }
        }

        foreach (var other in s.Units)
        {
            if (other.Opener is not null && other.Faction == unit.Faction)
            {
                throw new InvalidOperationException(
                    $"Unit {other.Id.Value} already holds the {unit.Faction} side's opener.");
            }
        }

        return s with { Units = s.Units.SetItem(s.Units.IndexOf(unit), unit with { Opener = charge }) };
    }

    /// <summary>
    /// The current unit's turn start (plan call 6): its budget becomes fresh, its <see cref="RefillTag.Turn"/> pools
    /// refill, and every unit's <see cref="TurnBudget.CampSpent"/> clears (AB 6.4); then the turn-start step (empty
    /// until M1-12) and <see cref="TurnStarted"/>. A unit marked with an opener (M1-11 plan call 6; M1-10 rulings record
    /// 5, step 9) instead gets <see cref="OpenerCharge.Charged"/> as its budget and its charged turn-pool counts written
    /// back after the refill, and the mark clears. Throws <see cref="InvalidOperationException"/> out of combat, on an
    /// ended run, while the turn is vacated, or while the fight is held before round 1.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) StartTurn(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        var combat = LiveTurn(s);
        var unit = combat.CurrentUnit;
        var opener = s.FindUnit(unit)!.Opener;

        // The striker's first turn keeps what its opener charged; any other turn starts fresh.
        var fresh = opener?.Charged ?? default;
        var units = s.Units.ToBuilder();
        for (int i = 0; i < units.Count; i++)
        {
            var u = units[i];
            if (u.Id == unit)
            {
                if (u.Budget != fresh || opener is not null)
                {
                    units[i] = u with { Budget = fresh, Opener = null };
                }
            }
            else if (u.Budget.CampSpent)
            {
                units[i] = u with { Budget = u.Budget with { CampSpent = false } };
            }
        }

        var next = s with { Units = units.ToImmutable() };
        next = opener is null
            ? Pools.Refill(rules, next, unit, RefillTag.Turn)
            : Pools.RefillKeeping(rules, next, unit, RefillTag.Turn, opener.TurnPools);

        // CO 5.4's turn-start step (conditions, durations, the zero-HP backstop) goes here: M1-12.
        return ([new TurnStarted(unit, combat.Round)], next);
    }

    /// <summary>
    /// Ends the current unit's turn and starts the next (plan call 9): <see cref="EndCurrentTurn"/>
    /// (<see cref="TurnEnded"/>, then the turn-end step), then moves to the next slot (passing the last slot wraps to
    /// slot 0 and round + 1), then <see cref="StartTurn"/>. Throws <see cref="InvalidOperationException"/> out of combat,
    /// on an ended run, or while the turn is vacated.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) Advance(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        var combat = LiveTurn(s);
        var (ended, afterEnd) = EndCurrentTurn(rules, s);

        int current = combat.Current + 1;
        long round = combat.Round;
        if (current == combat.Order.Length)
        {
            current = 0;
            round = checked(round + 1);
        }

        var (turn, next) = StartTurn(rules, afterEnd with { Combat = combat with { Current = current, Round = round } });
        return ([.. ended, .. turn], next);
    }

    /// <summary>
    /// The current unit's turn end, shared by <see cref="Advance"/> and <c>CombatEnd.EndIfWon</c> so both give the same
    /// order: <see cref="TurnEnded"/>, then the turn-end step (CO 5.4; empty until M1-12, whose events then follow
    /// <see cref="TurnEnded"/> in both paths). The caller has checked that a live, unvacated turn exists.
    /// </summary>
    internal static (ImmutableArray<GameEvent> Events, GameState Next) EndCurrentTurn(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        var events = ImmutableArray.Create<GameEvent>(new TurnEnded(s.Combat!.CurrentUnit));

        // CO 5.4's turn-end step goes here, after TurnEnded: M1-12.
        return (events, s);
    }

    /// <summary>Every unit's turn budget made fresh, the camp flag included (combat start, combat end, each tick).</summary>
    internal static GameState FreshBudgets(GameState s) =>
        s with { Units = s.Units.Select(u => u.Budget == default ? u : u with { Budget = default }).ToImmutableArray() };

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
    /// <paramref name="unit"/>'s movement left this turn (plan call 5; D-18, subtract, not rescale: M1-10 rulings
    /// record 2.1): its folded <c>speed</c> minus its
    /// <see cref="TurnBudget.MovementUsed"/>, never below 0. Throws <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static long MovementLeft(RulesContext rules, GameState s, UnitId unit)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var holder = s.FindUnit(unit) ?? throw new ArgumentException($"Unknown unit {unit.Value}.", nameof(unit));
        return Math.Max(0, checked(Effects.Number(rules, s, unit, "speed") - holder.Budget.MovementUsed));
    }

    // The combat state of a turn some unit holds: in combat, not ended, not vacated, not held before round 1.
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

        if (combat.BeforeRound1)
        {
            throw new InvalidOperationException("Round 1 has not started; start it with BeginRound1.");
        }

        return combat;
    }
}
