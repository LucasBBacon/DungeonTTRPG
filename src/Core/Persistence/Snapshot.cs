using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dungeon.Core;

/// <summary>The text is not a readable save: not JSON, a field missing or mistyped, or a state that breaks the rules.</summary>
public sealed class SnapshotFormatException : Exception
{
    public SnapshotFormatException(string message)
        : base(message)
    {
    }

    public SnapshotFormatException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

/// <summary>The save was written by a newer version of the game than this one can read.</summary>
public sealed class SnapshotVersionException : Exception
{
    public SnapshotVersionException(int found)
        : base($"Save has version {found}, but this build reads up to version {Snapshot.CurrentVersion}.")
    {
        Found = found;
    }

    /// <summary>The version number the save carries, or <see cref="int.MaxValue"/> if it is larger than that.</summary>
    public int Found { get; }
}

/// <summary>
/// Saves and loads a complete <see cref="GameState"/> as single-line JSON. The output is
/// canonical: two equal states give identical text, so states can be compared by their saves.
/// </summary>
public static class Snapshot
{
    public const int CurrentVersion = 1;

    public static string Save(GameState s)
    {
        ArgumentNullException.ThrowIfNull(s);

        return PersistenceJson.Write(new StateDto
        {
            Version = CurrentVersion,
            Map = s.Map.ToRows().ToArray(),
            Units = s.Units.Select(u => new UnitDto
            {
                Id = u.Id.Value,
                X = u.Pos.X,
                Y = u.Pos.Y,
                Faction = u.Faction.ToString(),
                Boons = u.Boons.ToArray(),
            }).ToArray(),
            Rng = new RngStreamsDto
            {
                Generation = ToDto(s.Rng.Generation),
                Combat = ToDto(s.Rng.Combat),
                Loot = ToDto(s.Rng.Loot),
            },
            Pending = s.Pending is null ? null : new PendingDto
            {
                Id = s.Pending.Id.Value,
                Unit = s.Pending.Unit.Value,
                Kind = s.Pending.Kind.ToString(),
                Options = s.Pending.Options.ToArray(),
                X = s.Pending.Source.X,
                Y = s.Pending.Source.Y,
            },
            Tick = s.Tick,
            NextDecisionId = s.NextDecisionId,
        });
    }

    /// <summary>
    /// Rebuilds a state from <see cref="Save"/> output. Throws <see cref="SnapshotVersionException"/>
    /// for a save from a newer version and <see cref="SnapshotFormatException"/> for anything else
    /// that is wrong; it never returns a partly loaded state.
    /// </summary>
    public static GameState Load(string json) => PersistenceJson.Read<StateDto, GameState>(json, FromDto);

    private static GameState FromDto(StateDto dto)
    {
        var rows = PersistenceJson.Need(dto.Map, "map");
        var map = GridMap.Parse(rows);

        var units = new List<Unit>();
        foreach (var u in PersistenceJson.Need(dto.Units, "units"))
        {
            if (u is null)
            {
                throw new SnapshotFormatException("Invalid save: a unit is null.");
            }

            var unit = new Unit(
                new UnitId(PersistenceJson.Need(u.Id, "unit id")),
                new GridPos(PersistenceJson.Need(u.X, "unit x"), PersistenceJson.Need(u.Y, "unit y")),
                PersistenceJson.ParseEnum<Faction>(u.Faction, "faction"),
                [.. PersistenceJson.Need(u.Boons, "unit boons")]);
            NeedBoonIds(unit.Boons, $"unit {unit.Id.Value} boons");
            units.Add(unit);
        }

        // Create applies the same placement rules as a new run: unique ids, walkable squares,
        // one unit per square. Its fresh random streams are replaced by the saved ones below.
        var state = GameState.Create(map, units, 0);

        var rngDto = PersistenceJson.Need(dto.Rng, "rng");
        var rng = new RngStreams(
            FromDto(rngDto.Generation, "generation"),
            FromDto(rngDto.Combat, "combat"),
            FromDto(rngDto.Loot, "loot"));

        long tick = PersistenceJson.Need(dto.Tick, "tick");
        if (tick < 0)
        {
            throw new SnapshotFormatException($"Invalid save: tick {tick} is negative.");
        }

        int nextDecisionId = PersistenceJson.Need(dto.NextDecisionId, "nextDecisionId");
        if (nextDecisionId < 1)
        {
            throw new SnapshotFormatException($"Invalid save: nextDecisionId {nextDecisionId} is below 1.");
        }

        PendingDecision? pending = null;
        if (dto.Pending is { } p)
        {
            pending = new PendingDecision(
                new DecisionId(PersistenceJson.Need(p.Id, "pending id")),
                new UnitId(PersistenceJson.Need(p.Unit, "pending unit")),
                PersistenceJson.ParseEnum<DecisionKind>(p.Kind, "pending kind"),
                [.. PersistenceJson.Need(p.Options, "pending options")],
                new GridPos(PersistenceJson.Need(p.X, "pending x"), PersistenceJson.Need(p.Y, "pending y")));

            if (pending.Id.Value < 1 || pending.Id.Value >= nextDecisionId)
            {
                throw new SnapshotFormatException(
                    $"Invalid save: pending decision id {pending.Id.Value} was never issued (nextDecisionId is {nextDecisionId}).");
            }

            if (state.FindUnit(pending.Unit) is null)
            {
                throw new SnapshotFormatException(
                    $"Invalid save: pending decision names unknown unit {pending.Unit.Value}.");
            }

            if (pending.Options.IsEmpty)
            {
                throw new SnapshotFormatException("Invalid save: pending decision has no options.");
            }

            if (!map.InBounds(pending.Source))
            {
                throw new SnapshotFormatException(
                    $"Invalid save: pending decision source ({pending.Source.X}, {pending.Source.Y}) is outside the map.");
            }

            // DecisionKind has one value, so ParseEnum above has already limited this to a
            // shrine boon. Only a decision the rules can produce may be loaded, because
            // answering it changes the source square and the unit.
            if (map[pending.Source] != Tile.Shrine)
            {
                throw new SnapshotFormatException(
                    $"Invalid save: pending decision source ({pending.Source.X}, {pending.Source.Y}) is not a shrine.");
            }

            if (state.FindUnit(pending.Unit)!.Pos != pending.Source)
            {
                throw new SnapshotFormatException(
                    $"Invalid save: unit {pending.Unit.Value} is not standing on the pending decision's source.");
            }

            NeedBoonIds(pending.Options, "pending options");
            if (pending.Options.Length != Engine.ShrineBoonChoices
                || pending.Options.Distinct().Count() != pending.Options.Length)
            {
                throw new SnapshotFormatException(
                    $"Invalid save: pending options must be {Engine.ShrineBoonChoices} different boons.");
            }
        }

        return state with { Rng = rng, Pending = pending, Tick = tick, NextDecisionId = nextDecisionId };
    }

    private static void NeedBoonIds(ImmutableArray<int> boons, string name)
    {
        foreach (int boon in boons)
        {
            if (boon < 0 || boon >= Engine.BoonCount)
            {
                throw new SnapshotFormatException($"Invalid save: {name} holds unknown boon {boon}.");
            }
        }
    }

    private static RngDto ToDto(RngState s) => new() { State = s.State, Inc = s.Inc };

    private static RngState FromDto(RngDto? dto, string name)
    {
        var d = PersistenceJson.Need(dto, $"rng {name}");
        var s = new RngState(
            PersistenceJson.Need(d.State, $"rng {name} state"),
            PersistenceJson.Need(d.Inc, $"rng {name} inc"));

        // An even increment is never produced by seeding and would break the generator.
        if ((s.Inc & 1) == 0)
        {
            throw new SnapshotFormatException($"Invalid save: rng {name} inc {s.Inc} is not odd.");
        }

        return s;
    }

    // The file format. Kept apart from the domain records so that refactoring a record
    // does not silently change what is written to disk.

    private sealed class StateDto
    {
        [JsonPropertyName("version"), JsonPropertyOrder(0)] public int? Version { get; set; }
        [JsonPropertyName("map"), JsonPropertyOrder(1)] public string[]? Map { get; set; }
        [JsonPropertyName("units"), JsonPropertyOrder(2)] public UnitDto?[]? Units { get; set; }
        [JsonPropertyName("rng"), JsonPropertyOrder(3)] public RngStreamsDto? Rng { get; set; }
        // Required: Save always writes it (as null when nothing is pending), so its absence means a damaged file.
        [JsonPropertyName("pending"), JsonPropertyOrder(4), JsonRequired] public PendingDto? Pending { get; set; }
        [JsonPropertyName("tick"), JsonPropertyOrder(5)] public long? Tick { get; set; }
        [JsonPropertyName("nextDecisionId"), JsonPropertyOrder(6)] public int? NextDecisionId { get; set; }
    }

    private sealed class UnitDto
    {
        [JsonPropertyName("id"), JsonPropertyOrder(0)] public int? Id { get; set; }
        [JsonPropertyName("x"), JsonPropertyOrder(1)] public int? X { get; set; }
        [JsonPropertyName("y"), JsonPropertyOrder(2)] public int? Y { get; set; }
        [JsonPropertyName("faction"), JsonPropertyOrder(3)] public string? Faction { get; set; }
        [JsonPropertyName("boons"), JsonPropertyOrder(4)] public int[]? Boons { get; set; }
    }

    private sealed class RngStreamsDto
    {
        [JsonPropertyName("generation"), JsonPropertyOrder(0)] public RngDto? Generation { get; set; }
        [JsonPropertyName("combat"), JsonPropertyOrder(1)] public RngDto? Combat { get; set; }
        [JsonPropertyName("loot"), JsonPropertyOrder(2)] public RngDto? Loot { get; set; }
    }

    private sealed class RngDto
    {
        [JsonPropertyName("state"), JsonPropertyOrder(0)] public ulong? State { get; set; }
        [JsonPropertyName("inc"), JsonPropertyOrder(1)] public ulong? Inc { get; set; }
    }

    private sealed class PendingDto
    {
        [JsonPropertyName("id"), JsonPropertyOrder(0)] public int? Id { get; set; }
        [JsonPropertyName("unit"), JsonPropertyOrder(1)] public int? Unit { get; set; }
        [JsonPropertyName("kind"), JsonPropertyOrder(2)] public string? Kind { get; set; }
        [JsonPropertyName("options"), JsonPropertyOrder(3)] public int[]? Options { get; set; }
        [JsonPropertyName("x"), JsonPropertyOrder(4)] public int? X { get; set; }
        [JsonPropertyName("y"), JsonPropertyOrder(5)] public int? Y { get; set; }
    }
}

/// <summary>JSON plumbing shared by <see cref="Snapshot"/> and <see cref="CommandLog"/>.</summary>
internal static class PersistenceJson
{
    // System.Text.Json always reads and writes numbers in the invariant culture.
    // Unknown properties are refused so that a damaged file is not half understood.
    private static readonly JsonSerializerOptions Options = new()
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
    };

    public static string Write<TDto>(TDto dto) => JsonSerializer.Serialize(dto, Options);

    /// <summary>
    /// Checks the version, parses <paramref name="json"/> into a DTO and builds the result.
    /// Every failure other than a newer version surfaces as <see cref="SnapshotFormatException"/>.
    /// </summary>
    public static TResult Read<TDto, TResult>(string json, Func<TDto, TResult> build)
        where TDto : class
    {
        ArgumentNullException.ThrowIfNull(json);

        try
        {
            CheckVersion(json);
            var dto = JsonSerializer.Deserialize<TDto>(json, Options)
                ?? throw new SnapshotFormatException("Invalid save: the document is null.");
            return build(dto);
        }
        catch (Exception ex) when (ex is JsonException or ArgumentException or InvalidOperationException
            or NotSupportedException or FormatException or OverflowException)
        {
            throw new SnapshotFormatException($"Invalid save: {ex.Message}", ex);
        }
    }

    public static T Need<T>(T? value, string name)
        where T : struct =>
        value ?? throw new SnapshotFormatException($"Invalid save: {name} is missing.");

    public static T Need<T>(T? value, string name)
        where T : class =>
        value ?? throw new SnapshotFormatException($"Invalid save: {name} is missing.");

    /// <summary>Reads an enum by its exact name. Numbers and unknown names are refused.</summary>
    public static T ParseEnum<T>(string? text, string name)
        where T : struct, Enum
    {
        string needed = Need(text, name);
        foreach (var value in Enum.GetValues<T>())
        {
            if (string.Equals(value.ToString(), needed, StringComparison.Ordinal))
            {
                return value;
            }
        }

        throw new SnapshotFormatException($"Invalid save: unknown {name} '{needed}'.");
    }

    // Runs before any content is looked at, so a save from a newer version is reported as
    // such even when its layout has changed beyond recognition.
    private static void CheckVersion(string json)
    {
        using var doc = JsonDocument.Parse(json);
        if (doc.RootElement.ValueKind != JsonValueKind.Object)
        {
            throw new SnapshotFormatException("Invalid save: the document is not a JSON object.");
        }

        if (!doc.RootElement.TryGetProperty("version", out var version)
            || version.ValueKind != JsonValueKind.Number
            || !version.TryGetInt64(out long wide))
        {
            throw new SnapshotFormatException("Invalid save: version is missing or not a whole number.");
        }

        // A version too large for an int is still a newer version, not a damaged file.
        int found = (int)Math.Clamp(wide, int.MinValue, int.MaxValue);

        if (found > Snapshot.CurrentVersion)
        {
            throw new SnapshotVersionException(found);
        }

        if (found < Snapshot.CurrentVersion)
        {
            throw new SnapshotFormatException($"Invalid save: unsupported version {found}.");
        }

        RejectDuplicateProperties(doc.RootElement);
    }

    // The JSON reader keeps the last of two properties with the same name. A save never
    // repeats one, so a repeat means the file was altered and must not be half understood.
    private static void RejectDuplicateProperties(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var seen = new List<string>();
            foreach (var property in element.EnumerateObject())
            {
                if (seen.Contains(property.Name))
                {
                    throw new SnapshotFormatException($"Invalid save: property '{property.Name}' appears twice.");
                }

                seen.Add(property.Name);
                RejectDuplicateProperties(property.Value);
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                RejectDuplicateProperties(item);
            }
        }
    }
}
