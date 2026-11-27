using System;
using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Effects;

namespace Dungeon.Core;

public enum CommandError
{
    None,
    UnknownUnit,
    OutOfBounds,
    Blocked,
    Occupied,
    CornerCut,
    DecisionPending,
    NoDecisionPending,
    WrongDecision,
    InvalidOption,

    /// <summary>The run has ended (DH 10.1, D-08); every command is refused.</summary>
    RunEnded,

    /// <summary>In combat, the command names a unit whose turn it is not.</summary>
    NotYourTurn,

    /// <summary>In combat, the moving unit has no movement left this turn.</summary>
    NoMovementLeft,

    /// <summary>The command is valid only in combat.</summary>
    NotInCombat,
}

/// <summary>
/// The outcome of applying a command. On rejection <see cref="State"/> is the same instance
/// that was passed in, <see cref="Events"/> is empty and <see cref="Error"/> says why.
/// </summary>
public sealed record StepResult(GameState State, ImmutableArray<GameEvent> Events, CommandError Error)
{
    public bool Ok => Error == CommandError.None;
}

/// <summary>The rules: a pure function from a state and a command to the next state and its events.</summary>
public static class Engine
{
    /// <summary>How many boons a shrine offers.</summary>
    internal const int ShrineBoonChoices = 3;

    /// <summary>Boon ids run from 0 to one below this.</summary>
    internal const int BoonCount = 10;

    /// <summary>
    /// Applies <paramref name="command"/> with no effect rules: <see cref="Apply(GameState, Command, RulesContext)"/>
    /// with <see cref="RulesContext.Empty"/> (M1-07 plan call 6), so a state without effects behaves as it always has.
    /// <see cref="RulesContext.Empty"/> holds no effect definitions, so on a state that holds effect instances a
    /// command that raises a rule event (a move raises <c>unitMoved</c>) throws <see cref="ArgumentException"/>. It holds
    /// no pools either, so on a state with pool spent counts a tick or a turn start refills nothing, and the result
    /// differs from the live run's with no error. Such states need the overload that takes the rules.
    /// </summary>
    public static StepResult Apply(GameState state, Command command) => Apply(state, command, RulesContext.Empty);

    /// <summary>
    /// Applies <paramref name="command"/>, then fires the triggers of the active effects that answer its events
    /// (MAIN 6.4; AB 14.4). <see cref="StepResult.Events"/> holds the command's own events, then the trigger-produced
    /// events in firing order. A rejected command fires nothing. Throws <see cref="TriggerLoopException"/> if the
    /// command fires more than <see cref="Effects.Effects.MaxTriggerFirings"/> triggers.
    /// </summary>
    public static StepResult Apply(GameState state, Command command, RulesContext rules)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(rules);

        // After the run ends the core refuses every command (DH 10.1), checked before anything else (M1-10 plan call 3).
        if (state.Ended is not null)
        {
            return Reject(state, CommandError.RunEnded);
        }

        // While a decision is pending, the only valid command is an answer to it.
        if (state.Pending is not null && command is not AnswerDecisionCommand)
        {
            return Reject(state, CommandError.DecisionPending);
        }

        var result = command switch
        {
            MoveCommand move => ApplyMove(state, move, rules),
            EndTurnCommand end => ApplyEndTurn(state, end, rules),
            AnswerDecisionCommand answer => ApplyAnswer(state, answer),
            _ => throw new NotSupportedException($"Unsupported command type {command.GetType().Name}."),
        };
        if (!result.Ok)
        {
            return result;
        }

        // With nothing fired the result is returned as the handler made it, so M0 behaviour is unchanged.
        var (next, events) = TriggerDispatch.Run(rules, result.State, result.Events);
        return ReferenceEquals(next, result.State) && events == result.Events
            ? result
            : new StepResult(next, events, CommandError.None);
    }

    /// <summary>
    /// A one-square move. Out of combat it advances <see cref="GameState.Tick"/> and, as each tick does (AB 6.8; M1-10
    /// plan call 6), first gives every unit a fresh turn budget and refills every <see cref="RefillTag.Turn"/> pool, then
    /// makes the move (the tick's start steps come first, CO 5.5). In combat
    /// (plan call 8) only the current unit may move, and only with movement left (checked before the square); the move
    /// spends 1 movement and does not tick.
    /// </summary>
    private static StepResult ApplyMove(GameState state, MoveCommand move, RulesContext rules)
    {
        // Checked before anything else so the outcome does not depend on the rest of the command.
        if (!Enum.IsDefined(move.Dir))
        {
            throw new ArgumentOutOfRangeException(nameof(move), move.Dir, "Unknown direction.");
        }

        int index = IndexOfUnit(state, move.Unit);
        if (index < 0)
        {
            return Reject(state, CommandError.UnknownUnit);
        }

        var unit = state.Units[index];
        var combat = state.Combat;
        if (combat is not null)
        {
            // A vacated turn is held by no one until it resumes.
            if (combat.TurnVacated || combat.CurrentUnit != unit.Id)
            {
                return Reject(state, CommandError.NotYourTurn);
            }

            if (Turns.MovementLeft(rules, state, unit.Id) < 1)
            {
                return Reject(state, CommandError.NoMovementLeft);
            }
        }

        var error = CheckStep(state, unit, move.Dir);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        var from = unit.Pos;
        var to = from.Step(move.Dir);

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        events.Add(new UnitMoved(unit.Id, from, to));
        GameState next;
        if (combat is not null)
        {
            // In combat the move spends movement; ticks are out-of-combat time (CO 5.5).
            var budget = unit.Budget with { MovementUsed = checked(unit.Budget.MovementUsed + 1) };
            next = state with { Units = state.Units.SetItem(index, unit with { Pos = to, Budget = budget }) };
        }
        else
        {
            // Checked: at the limit the step fails loudly rather than wrap to a state no save can hold.
            long tick = checked(state.Tick + 1);
            next = TickResets(rules, state);
            next = next with
            {
                Units = next.Units.SetItem(index, next.Units[index] with { Pos = to }),
                Tick = tick,
            };
            events.Add(new TickAdvanced(tick));
        }

        if (state.Map[to] == Tile.Shrine)
        {
            var (options, shrine) = DrawBoons(state.Rng.Shrine);
            var decision = new PendingDecision(
                new DecisionId(state.NextDecisionId), unit.Id, DecisionKind.ShrineBoon, options, to);
            next = next with
            {
                Rng = state.Rng with { Shrine = shrine },
                Pending = decision,
                NextDecisionId = checked(state.NextDecisionId + 1),
            };
            events.Add(new DecisionRequested(decision));
        }

        return new StepResult(next, events.ToImmutable(), CommandError.None);
    }

    // AB 6.8, at the start of each tick, before the command's own step: every unit's turn budget becomes fresh (the
    // camp flag included) and every turn pool refills.
    private static GameState TickResets(RulesContext rules, GameState s) =>
        Pools.RefillAll(rules, Turns.FreshBudgets(s), RefillTag.Turn);

    /// <summary>
    /// Ends the current unit's turn (M1-10 plan call 9): refused out of combat, for an unknown unit, and out of turn
    /// (a vacated turn included); otherwise <see cref="Turns.Advance"/>.
    /// </summary>
    private static StepResult ApplyEndTurn(GameState state, EndTurnCommand end, RulesContext rules)
    {
        var combat = state.Combat;
        if (combat is null)
        {
            return Reject(state, CommandError.NotInCombat);
        }

        if (IndexOfUnit(state, end.Unit) < 0)
        {
            return Reject(state, CommandError.UnknownUnit);
        }

        if (combat.TurnVacated || combat.CurrentUnit != end.Unit)
        {
            return Reject(state, CommandError.NotYourTurn);
        }

        var (events, next) = Turns.Advance(rules, state);
        return new StepResult(next, events, CommandError.None);
    }

    /// <summary>
    /// Whether <paramref name="u"/> may step one square in direction <paramref name="d"/> from
    /// where it stands: <see cref="CommandError.None"/> if so, otherwise the reason it may not.
    /// Shared by <see cref="MoveCommand"/> and the path query so both obey the same rules.
    /// </summary>
    internal static CommandError CheckStep(GameState s, Unit u, Direction d)
    {
        var from = u.Pos;
        var to = from.Step(d);

        if (!s.Map.InBounds(to))
        {
            return CommandError.OutOfBounds;
        }

        if (!s.Map.IsWalkable(to))
        {
            return CommandError.Blocked;
        }

        // No corner cutting: a diagonal step needs both squares it passes between to be open.
        if (d.IsDiagonal()
            && (!s.Map.IsWalkable(new GridPos(to.X, from.Y))
                || !s.Map.IsWalkable(new GridPos(from.X, to.Y))))
        {
            return CommandError.CornerCut;
        }

        if (s.UnitAt(to) is not null)
        {
            return CommandError.Occupied;
        }

        return CommandError.None;
    }

    private static StepResult ApplyAnswer(GameState state, AnswerDecisionCommand answer)
    {
        var pending = state.Pending;
        if (pending is null)
        {
            return Reject(state, CommandError.NoDecisionPending);
        }

        if (answer.Decision != pending.Id)
        {
            return Reject(state, CommandError.WrongDecision);
        }

        if (answer.OptionIndex < 0 || answer.OptionIndex >= pending.Options.Length)
        {
            return Reject(state, CommandError.InvalidOption);
        }

        int index = IndexOfUnit(state, pending.Unit);
        if (index < 0)
        {
            return Reject(state, CommandError.UnknownUnit);
        }

        return pending.Kind switch
        {
            DecisionKind.ShrineBoon => ResolveShrineBoon(state, pending, index, answer.OptionIndex),
            _ => throw new NotSupportedException($"Unsupported decision kind {pending.Kind}."),
        };
    }

    /// <summary>The unit takes the chosen boon and the shrine it stood on becomes plain floor.</summary>
    private static StepResult ResolveShrineBoon(GameState state, PendingDecision pending, int index, int optionIndex)
    {
        var unit = state.Units[index];
        int boon = pending.Options[optionIndex];
        var next = state with
        {
            Map = state.Map.With(pending.Source, Tile.Floor),
            Units = state.Units.SetItem(index, unit with { Boons = unit.Boons.Add(boon) }),
            Pending = null,
        };
        return new StepResult(
            next,
            ImmutableArray.Create<GameEvent>(
                new DecisionResolved(pending.Id, unit.Id, boon),
                new TileChanged(pending.Source, Tile.Floor)),
            CommandError.None);
    }

    /// <summary>
    /// Draws <see cref="ShrineBoonChoices"/> distinct boon ids from the shrine stream: a partial
    /// Fisher-Yates shuffle of <c>[0, BoonCount)</c> that makes exactly one draw per option, so the
    /// stream advances by the same count under every seed.
    /// </summary>
    private static (ImmutableArray<int> Options, DrawStream Next) DrawBoons(DrawStream shrine)
    {
        var boons = new int[BoonCount];
        for (int i = 0; i < BoonCount; i++)
        {
            boons[i] = i;
        }

        for (int i = 0; i < ShrineBoonChoices; i++)
        {
            (ulong offset, shrine) = shrine.NextBelow(checked((ulong)(BoonCount - i)));
            int j = checked(i + (int)offset);
            (boons[i], boons[j]) = (boons[j], boons[i]);
        }

        return (ImmutableArray.Create(boons, 0, ShrineBoonChoices), shrine);
    }

    private static int IndexOfUnit(GameState state, UnitId id)
    {
        for (int i = 0; i < state.Units.Length; i++)
        {
            if (state.Units[i].Id == id)
            {
                return i;
            }
        }

        return -1;
    }

    private static StepResult Reject(GameState state, CommandError error) =>
        new(state, ImmutableArray<GameEvent>.Empty, error);
}
