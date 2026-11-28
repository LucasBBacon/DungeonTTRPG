using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json.Serialization;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Effects;

namespace Dungeon.Core;

/// <summary>
/// The run seed and every command applied so far. Together, under the rules the run was played with, they reproduce a
/// run exactly.
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
    private const string UseAbilityType = "useAbility";

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
        UseAbilityCommand u => UseDto(u),
        null => throw new InvalidOperationException("Cannot save a null command."),
        _ => throw new NotSupportedException($"Unsupported command type {command.GetType().Name}."),
    };

    // A use's target is written as exactly one of targets (units), point or dir (an aim).
    private static CommandDto UseDto(UseAbilityCommand u)
    {
        if (u.AbilityId is null)
        {
            throw new InvalidOperationException("Cannot save an ability use with no ability id.");
        }

        var dto = new CommandDto { Type = UseAbilityType, Unit = u.User.Value, Ability = u.AbilityId };
        switch (u.Target)
        {
            case UnitsTarget { Units.IsDefault: false } units:
                dto.Targets = units.Units.Select(id => id.Value).ToArray();
                break;
            case PointTarget point:
                dto.Point = new PointDto { X = point.Point.X, Y = point.Point.Y };
                break;
            case AimTarget aim when Enum.IsDefined(aim.Aim):
                dto.Dir = aim.Aim.ToString();
                break;
            default:
                throw new InvalidOperationException("Cannot save an ability use whose target cannot be read back.");
        }

        return dto;
    }

    private static Command FromDto(CommandDto c)
    {
        string type = PersistenceJson.Need(c.Type, "command type");
        if (type != UseAbilityType && (c.Ability is not null || c.Targets is not null || c.Point is not null))
        {
            throw new SnapshotFormatException($"Invalid save: a {type} command carries ability-use fields.");
        }

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

            case UseAbilityType:
                return UseFromDto(c);

            default:
                throw new SnapshotFormatException($"Invalid save: unknown command type '{type}'.");
        }
    }

    private static UseAbilityCommand UseFromDto(CommandDto c)
    {
        if (c.Decision is not null || c.Option is not null)
        {
            throw new SnapshotFormatException("Invalid save: a useAbility command carries answer fields.");
        }

        int given = (c.Targets is null ? 0 : 1) + (c.Point is null ? 0 : 1) + (c.Dir is null ? 0 : 1);
        if (given != 1)
        {
            throw new SnapshotFormatException("Invalid save: a useAbility command needs exactly one of targets, point and dir.");
        }

        AbilityTarget target = c.Targets is { } targets
            ? new UnitsTarget([.. targets.Select(id => new UnitId(id))])
            : c.Point is { } point
                ? new PointTarget(new GridPos(PersistenceJson.Need(point.X, "useAbility point x"), PersistenceJson.Need(point.Y, "useAbility point y")))
                : new AimTarget(PersistenceJson.ParseEnum<Direction>(c.Dir, "useAbility dir"));
        return new UseAbilityCommand(
            new UnitId(PersistenceJson.Need(c.Unit, "useAbility unit")),
            PersistenceJson.Need(c.Ability, "useAbility ability"),
            target);
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

        [JsonPropertyName("ability"), JsonPropertyOrder(5)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Ability { get; set; }

        [JsonPropertyName("targets"), JsonPropertyOrder(6)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int[]? Targets { get; set; }

        [JsonPropertyName("point"), JsonPropertyOrder(7)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PointDto? Point { get; set; }
    }

    /// <summary>A burst's point.</summary>
    private sealed class PointDto
    {
        [JsonPropertyName("x"), JsonPropertyOrder(0), JsonRequired] public int? X { get; set; }
        [JsonPropertyName("y"), JsonPropertyOrder(1), JsonRequired] public int? Y { get; set; }
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
    /// <see cref="Run(GameState, IEnumerable{Command}, RulesContext)"/> with <see cref="RulesContext.Empty"/>, which
    /// holds no effect definitions and no pools. It reproduces a run only when the run was played with no rules: a
    /// command that raises a rule event while the state holds effect instances throws <see cref="ArgumentException"/>,
    /// and a state with pool spent counts never refills them, so the replay diverges from the live run with no error.
    /// Such a state needs the overload that takes the rules.
    /// </summary>
    public static GameState Run(GameState initial, IEnumerable<Command> commands) =>
        Run(initial, commands, RulesContext.Empty);

    /// <summary>
    /// Applies <paramref name="commands"/> in order with <see cref="Engine.Apply(GameState, Command, RulesContext)"/>
    /// and returns the final state. Throws <see cref="ReplayException"/> at the first command the rules reject. A
    /// command log reproduces a run only under the rules it was played with.
    /// </summary>
    public static GameState Run(GameState initial, IEnumerable<Command> commands, RulesContext rules)
    {
        ArgumentNullException.ThrowIfNull(initial);
        ArgumentNullException.ThrowIfNull(commands);
        ArgumentNullException.ThrowIfNull(rules);

        var state = initial;
        int index = 0;
        foreach (var command in commands)
        {
            var result = Engine.Apply(state, command, rules);
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
