using System;
using System.Collections.Immutable;

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
    public static StepResult Apply(GameState state, Command command)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(command);

        return command switch
        {
            MoveCommand move => ApplyMove(state, move),
            _ => throw new NotSupportedException($"Unsupported command type {command.GetType().Name}."),
        };
    }

    private static StepResult ApplyMove(GameState state, MoveCommand move)
    {
        int index = IndexOfUnit(state, move.Unit);
        if (index < 0)
        {
            return Reject(state, CommandError.UnknownUnit);
        }

        var unit = state.Units[index];
        var from = unit.Pos;
        var offset = move.Dir.Offset();
        var to = new GridPos(from.X + offset.X, from.Y + offset.Y);

        if (!state.Map.InBounds(to))
        {
            return Reject(state, CommandError.OutOfBounds);
        }

        if (!state.Map.IsWalkable(to))
        {
            return Reject(state, CommandError.Blocked);
        }

        // No corner cutting: a diagonal step needs both squares it passes between to be open.
        if (offset.X != 0 && offset.Y != 0
            && (!state.Map.IsWalkable(new GridPos(to.X, from.Y))
                || !state.Map.IsWalkable(new GridPos(from.X, to.Y))))
        {
            return Reject(state, CommandError.CornerCut);
        }

        if (state.UnitAt(to) is not null)
        {
            return Reject(state, CommandError.Occupied);
        }

        long tick = state.Tick + 1;
        var next = state with
        {
            Units = state.Units.SetItem(index, unit with { Pos = to }),
            Tick = tick,
        };
        return new StepResult(
            next,
            ImmutableArray.Create<GameEvent>(new UnitMoved(unit.Id, from, to), new TickAdvanced(tick)),
            CommandError.None);
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
