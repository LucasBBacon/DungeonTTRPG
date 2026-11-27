using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json.Serialization;

namespace Dungeon.Core;

/// <summary>
/// The run seed and every command applied so far. Together they reproduce a run exactly.
/// Saved as single-line JSON in the same versioned style as <see cref="Snapshot"/>;
/// <see cref="Load"/> reports problems with the same exceptions, and refuses a seed of 2^60
/// or more, which no run can have (and <see cref="Save"/> refuses to write one). Two logs with the same
/// contents are not equal with <c>==</c> (the array compares by reference); compare their
/// <see cref="Save"/> text instead.
/// </summary>
public sealed record CommandLog(ulong Seed, ImmutableArray<Command> Commands)
{
    private const string MoveType = "move";
    private const string AnswerType = "answer";
    private const string EndTurnType = "endTurn";

    public string Save()
    {
        if (Commands.IsDefault)
        {
            throw new InvalidOperationException("Cannot save a log whose Commands array is uninitialised.");
        }

        // Load refuses such a seed, so writing it would give a log that cannot be read back.
        if (Seed >= RunSeed.Limit)
        {
            throw new InvalidOperationException($"Cannot save a log whose seed {Seed} is not below 2^60.");
        }

        return PersistenceJson.Write(new LogDto
        {
            Version = Snapshot.CurrentVersion,
            Seed = Seed,
            Commands = Commands.Select(ToDto).ToArray(),
        });
    }

    public static CommandLog Load(string json) => PersistenceJson.Read<LogDto, CommandLog>(json, FromDto);

    private static CommandLog FromDto(LogDto dto)
    {
        ulong seed = RunSeed.Validate(PersistenceJson.Need(dto.Seed, "seed"), "seed");
        var commands = ImmutableArray.CreateBuilder<Command>();
        foreach (var c in PersistenceJson.Need(dto.Commands, "commands"))
        {
            if (c is null)
            {
                throw new SnapshotFormatException("Invalid save: a command is null.");
            }

            commands.Add(FromDto(c));
        }

        return new CommandLog(seed, commands.ToImmutable());
    }

    private static CommandDto ToDto(Command command) => command switch
    {
        // Written as a number, an undefined direction would give a log that Load refuses.
        MoveCommand m when !Enum.IsDefined(m.Dir) =>
            throw new InvalidOperationException($"Cannot save a move with undefined direction {(int)m.Dir}."),
        MoveCommand m => new CommandDto { Type = MoveType, Unit = m.Unit.Value, Dir = m.Dir.ToString() },
        AnswerDecisionCommand a => new CommandDto
        {
            Type = AnswerType,
            Decision = a.Decision.Value,
            Option = a.OptionIndex,
        },
        EndTurnCommand e => new CommandDto { Type = EndTurnType, Unit = e.Unit.Value },
        null => throw new InvalidOperationException("Cannot save a null command."),
        _ => throw new NotSupportedException($"Unsupported command type {command.GetType().Name}."),
    };

    private static Command FromDto(CommandDto c)
    {
        string type = PersistenceJson.Need(c.Type, "command type");
        switch (type)
        {
            case MoveType:
                if (c.Decision is not null || c.Option is not null)
                {
                    throw new SnapshotFormatException("Invalid save: a move command carries answer fields.");
                }

                return new MoveCommand(
                    new UnitId(PersistenceJson.Need(c.Unit, "move unit")),
                    PersistenceJson.ParseEnum<Direction>(c.Dir, "move dir"));

            case AnswerType:
                if (c.Unit is not null || c.Dir is not null)
                {
                    throw new SnapshotFormatException("Invalid save: an answer command carries move fields.");
                }

                return new AnswerDecisionCommand(
                    new DecisionId(PersistenceJson.Need(c.Decision, "answer decision")),
                    PersistenceJson.Need(c.Option, "answer option"));

            case EndTurnType:
                if (c.Dir is not null || c.Decision is not null || c.Option is not null)
                {
                    throw new SnapshotFormatException("Invalid save: an endTurn command carries move or answer fields.");
                }

                return new EndTurnCommand(new UnitId(PersistenceJson.Need(c.Unit, "endTurn unit")));

            default:
                throw new SnapshotFormatException($"Invalid save: unknown command type '{type}'.");
        }
    }

    private sealed class LogDto
    {
        [JsonPropertyName("version"), JsonPropertyOrder(0)] public int? Version { get; set; }
        [JsonPropertyName("seed"), JsonPropertyOrder(1)] public ulong? Seed { get; set; }
        [JsonPropertyName("commands"), JsonPropertyOrder(2)] public CommandDto?[]? Commands { get; set; }
    }

    /// <summary>One command, tagged by <see cref="Type"/>. Fields the type does not use are left out.</summary>
    private sealed class CommandDto
    {
        [JsonPropertyName("type"), JsonPropertyOrder(0)] public string? Type { get; set; }

        [JsonPropertyName("unit"), JsonPropertyOrder(1)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Unit { get; set; }

        [JsonPropertyName("dir"), JsonPropertyOrder(2)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Dir { get; set; }

        [JsonPropertyName("decision"), JsonPropertyOrder(3)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Decision { get; set; }

        [JsonPropertyName("option"), JsonPropertyOrder(4)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Option { get; set; }
    }
}

/// <summary>A command in a replay was rejected by the rules.</summary>
public sealed class ReplayException : Exception
{
    public ReplayException(int commandIndex, CommandError error)
        : base($"Replay stopped: command {commandIndex} was rejected with {error}.")
    {
        CommandIndex = commandIndex;
        Error = error;
    }

    /// <summary>Zero-based position of the rejected command.</summary>
    public int CommandIndex { get; }

    public CommandError Error { get; }
}

/// <summary>Re-applies a sequence of commands to a state.</summary>
public static class Replay
{
    /// <summary>
    /// Applies <paramref name="commands"/> in order and returns the final state. Throws
    /// <see cref="ReplayException"/> at the first command the rules reject. It applies them with
    /// <see cref="Engine.Apply(GameState, Command)"/>, which has no effect rules, so a command that raises a rule event
    /// while the state holds effect instances throws <see cref="ArgumentException"/>.
    /// </summary>
    public static GameState Run(GameState initial, IEnumerable<Command> commands)
    {
        ArgumentNullException.ThrowIfNull(initial);
        ArgumentNullException.ThrowIfNull(commands);

        var state = initial;
        int index = 0;
        foreach (var command in commands)
        {
            var result = Engine.Apply(state, command);
            if (!result.Ok)
            {
                throw new ReplayException(index, result.Error);
            }

            state = result.State;
            index++;
        }

        return state;
    }
}
