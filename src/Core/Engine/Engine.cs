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
    /// <summary>How many boons a shrine offers.</summary>
    internal const int ShrineBoonChoices = 3;

    /// <summary>Boon ids run from 0 to one below this.</summary>
    internal const int BoonCount = 10;

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
        var error = CheckStep(state, unit, move.Dir);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        var from = unit.Pos;
        var to = from.Step(move.Dir);

        // Checked: at the limit the step fails loudly rather than wrap to a state no save can hold.
        long tick = checked(state.Tick + 1);
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
