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

/// <summary>
/// The save carries a version this build does not read: a newer one, or an older one whose format
/// was retired (each save version is read only by the builds that write it).
/// </summary>
public sealed class SnapshotVersionException : Exception
{
    public SnapshotVersionException(int found)
        : base(found > Snapshot.CurrentVersion
            ? $"Save has version {found}, but this build reads up to version {Snapshot.CurrentVersion}."
            : $"Save has version {found}, and this build no longer reads it (it reads version {Snapshot.CurrentVersion}).")
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
    /// <summary>
    /// The only version this build reads and writes, shared with <see cref="CommandLog"/>.
    /// Version 2 (M1-02) stores the run seed and stream counters; version 1 held PCG32 states.
    /// </summary>
    public const int CurrentVersion = 2;

    /// <summary>
    /// Writes <paramref name="s"/>. Only the run seed and the stream counters are stored, so a state whose
    /// run seed is not below 2^60, or whose stream keys are not the ones that seed derives, cannot be saved
    /// (it would load as a different run); both throw <see cref="InvalidOperationException"/>.
    /// </summary>
    public static string Save(GameState s)
    {
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(s.Rng);
        if (s.RunSeed >= RunSeed.Limit)
        {
            throw new InvalidOperationException($"Cannot save a state whose run seed {s.RunSeed} is not below 2^60.");
        }

        var keys = RunStreams.FromSeed(s.RunSeed);
        if (s.Rng.Combat.Key != keys.Combat.Key || s.Rng.Shrine.Key != keys.Shrine.Key)
        {
            throw new InvalidOperationException(
                $"Cannot save a state whose stream keys are not derived from its run seed {s.RunSeed}.");
        }

        return PersistenceJson.Write(new StateDto
        {
            Version = CurrentVersion,
            RunSeed = s.RunSeed,
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
                Combat = s.Rng.Combat.Counter,
                Shrine = s.Rng.Shrine.Counter,
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
    /// for a save of any version other than <see cref="CurrentVersion"/> and
    /// <see cref="SnapshotFormatException"/> for anything else that is wrong, including a run seed
    /// of 2^60 or more; it never returns a partly loaded state. Stream keys are re-derived from the
    /// run seed, so only the counters come from the file.
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

        // Create applies the same rules as a new run: a run seed below 2^60, unique ids, walkable
        // squares, one unit per square. Its streams have the right keys; the saved counters are put back below.
        var state = GameState.Create(map, units, PersistenceJson.Need(dto.RunSeed, "runSeed"));

        var rngDto = PersistenceJson.Need(dto.Rng, "rng");
        var rng = new RunStreams(
            state.Rng.Combat.Skip(NeedCounter(rngDto.Combat, "rng combat")),
            state.Rng.Shrine.Skip(NeedCounter(rngDto.Shrine, "rng shrine")));

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

    /// <summary>A saved stream counter. A stream at 2^64 - 1 can never draw again, so that value is refused here, at load.</summary>
    private static ulong NeedCounter(ulong? value, string name)
    {
        ulong counter = PersistenceJson.Need(value, name);
        if (counter == ulong.MaxValue)
        {
            throw new SnapshotFormatException($"Invalid save: {name} {counter} leaves the stream no draws.");
        }

        return counter;
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

    // The file format. Kept apart from the domain records so that refactoring a record
    // does not silently change what is written to disk.

    private sealed class StateDto
    {
        [JsonPropertyName("version"), JsonPropertyOrder(0)] public int? Version { get; set; }
        [JsonPropertyName("runSeed"), JsonPropertyOrder(1)] public ulong? RunSeed { get; set; }
        [JsonPropertyName("map"), JsonPropertyOrder(2)] public string[]? Map { get; set; }
        [JsonPropertyName("units"), JsonPropertyOrder(3)] public UnitDto?[]? Units { get; set; }
        [JsonPropertyName("rng"), JsonPropertyOrder(4)] public RngStreamsDto? Rng { get; set; }
        // Required: Save always writes it (as null when nothing is pending), so its absence means a damaged file.
        [JsonPropertyName("pending"), JsonPropertyOrder(5), JsonRequired] public PendingDto? Pending { get; set; }
        [JsonPropertyName("tick"), JsonPropertyOrder(6)] public long? Tick { get; set; }
        [JsonPropertyName("nextDecisionId"), JsonPropertyOrder(7)] public int? NextDecisionId { get; set; }
    }

    private sealed class UnitDto
    {
        [JsonPropertyName("id"), JsonPropertyOrder(0)] public int? Id { get; set; }
        [JsonPropertyName("x"), JsonPropertyOrder(1)] public int? X { get; set; }
        [JsonPropertyName("y"), JsonPropertyOrder(2)] public int? Y { get; set; }
        [JsonPropertyName("faction"), JsonPropertyOrder(3)] public string? Faction { get; set; }
        [JsonPropertyName("boons"), JsonPropertyOrder(4)] public int[]? Boons { get; set; }
    }

    /// <summary>The stream counters (the next draw index of each stream); keys are not stored.</summary>
    private sealed class RngStreamsDto
    {
        [JsonPropertyName("combat"), JsonPropertyOrder(0)] public ulong? Combat { get; set; }
        [JsonPropertyName("shrine"), JsonPropertyOrder(1)] public ulong? Shrine { get; set; }
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
    /// Every failure other than an unread version surfaces as <see cref="SnapshotFormatException"/>.
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

    // Runs before any content is looked at, so a save from another version is reported as
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

        if (found != Snapshot.CurrentVersion)
        {
            throw new SnapshotVersionException(found);
        }

        // A save never repeats a property, so a repeat means the file was altered (JsonStrict).
        if (JsonStrict.FirstDuplicate(doc.RootElement, out string? repeated) is not null)
        {
            throw new SnapshotFormatException($"Invalid save: property '{repeated}' appears twice.");
        }
    }
}
