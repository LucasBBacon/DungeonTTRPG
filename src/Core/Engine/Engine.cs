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
    private const int ShrineBoonChoices = 3;
    private const int BoonCount = 10;

    public static StepResult Apply(GameState state, Command command)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(command);

        // While a decision is pending, the only valid command is an answer to it.
        if (state.Pending is not null && command is not AnswerDecisionCommand)
        {
            return Reject(state, CommandError.DecisionPending);
        }

        return command switch
        {
            MoveCommand move => ApplyMove(state, move),
            AnswerDecisionCommand answer => ApplyAnswer(state, answer),
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
        var events = ImmutableArray.CreateBuilder<GameEvent>();
        events.Add(new UnitMoved(unit.Id, from, to));
        events.Add(new TickAdvanced(tick));

        if (state.Map[to] == Tile.Shrine)
        {
            var (options, loot) = DrawBoons(state.Rng.Loot);
            var decision = new PendingDecision(
                new DecisionId(state.NextDecisionId), unit.Id, DecisionKind.ShrineBoon, options, to);
            next = next with
            {
                Rng = state.Rng with { Loot = loot },
                Pending = decision,
                NextDecisionId = state.NextDecisionId + 1,
            };
            events.Add(new DecisionRequested(decision));
        }

        return new StepResult(next, events.ToImmutable(), CommandError.None);
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

        var unit = state.Units[index];
        int boon = pending.Options[answer.OptionIndex];
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

    /// <summary>Draws distinct boon ids from the loot stream, in draw order, skipping repeats.</summary>
    private static (ImmutableArray<int> Options, RngState Next) DrawBoons(RngState loot)
    {
        var options = ImmutableArray.CreateBuilder<int>(ShrineBoonChoices);
        while (options.Count < ShrineBoonChoices)
        {
            (int boon, loot) = Pcg32.NextInt(loot, BoonCount);
            if (!options.Contains(boon))
            {
                options.Add(boon);
            }
        }

        return (options.MoveToImmutable(), loot);
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
