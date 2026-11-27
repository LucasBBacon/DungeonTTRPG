using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dungeon.Core.Canonical;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

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
    /// Version 7 (M1-10) adds the run, combat and turn state;
    /// version 6 (M1-09) added current HP and the HP basis;
    /// version 5 (M1-08) added each unit's stat block;
    /// version 4 (M1-07) added the active effects and the next effect seq;
    /// version 3 (M1-06) added the reproducibility-key header and the run configuration and unlock
    /// snapshots; version 2 (M1-02) stored the run seed and stream counters; version 1 held PCG32 states.
    /// </summary>
    public const int CurrentVersion = 7;

    /// <summary>
    /// Writes <paramref name="s"/>. Only the run seed and the stream counters are stored, so a state whose
    /// run seed is not below 2^60, or whose stream keys are not the ones that seed derives, cannot be saved
    /// (it would load as a different run); both throw <see cref="InvalidOperationException"/>. Active effects are
    /// written in seq order whatever order the state holds them in, so equal contents give equal text.
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

        var key = s.Key;
        return PersistenceJson.Write(new StateDto
        {
            Version = CurrentVersion,
            RulesVersion = Dungeon.Core.RulesVersion.Current,
            GenVersion = key.GenVersion,
            ManifestHash = key.ManifestHash.ToHex(),
            ConfigHash = key.ConfigHash.ToHex(),
            UnlockHash = key.UnlockHash.ToHex(),
            RunSeed = s.RunSeed,
            Config = ConfigDto.From(s.Config),
            Unlocks = new UnlocksDto { Ids = s.Unlocks.Ids.OrderBy(id => id, StringComparer.Ordinal).ToArray() },
            Map = s.Map.ToRows().ToArray(),
            Units = s.Units.Select(u => new UnitDto
            {
                Id = u.Id.Value,
                X = u.Pos.X,
                Y = u.Pos.Y,
                Faction = u.Faction.ToString(),
                Boons = u.Boons.ToArray(),
                Stats = StatsDto.From(u.Stats),
                Hp = u.Hp,
                Budget = BudgetDto.From(u.Budget),
                Spent = u.Spent.Select(p => new PoolSpendDto { Pool = p.PoolId, Spent = p.Spent }).ToArray(),
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
            Effects = s.Effects.OrderBy(e => e.Seq).Select(e => new EffectDto
            {
                Seq = e.Seq,
                Effect = e.EffectId,
                Holder = e.Holder.Value,
                Source = e.Source.Unit?.Value,
            }).ToArray(),
            NextEffectSeq = s.NextEffectSeq,
            Hero = s.Hero?.Value,
            Combat = s.Combat is null ? null : new CombatDto
            {
                Order = s.Combat.Order.Select(id => id.Value).ToArray(),
                Current = s.Combat.Current,
                Round = s.Combat.Round,
                TurnVacated = s.Combat.TurnVacated,
            },
            Ended = s.Ended is null ? null : new RunEndDto
            {
                Cause = s.Ended.Cause.ToString(),
                KillingEventId = s.Ended.KillingEventId,
            },
            NextEventSeq = s.NextEventSeq,
        });
    }

    /// <summary>
    /// Rebuilds a state from <see cref="Save"/> output. Throws <see cref="SnapshotVersionException"/>
    /// for a save of any version other than <see cref="CurrentVersion"/> and
    /// <see cref="SnapshotFormatException"/> for anything else that is wrong, including a run seed
    /// of 2^60 or more; it never returns a partly loaded state. Stream keys are re-derived from the
    /// run seed, so only the counters come from the file. Active effects must be sorted by seq, with positive,
    /// distinct seqs below <c>nextEffectSeq</c>, content ids, and holder and source units that are in the state.
    /// The hero, each unit's budget and spent counts are checked by <see cref="GameState.Create"/>, and the combat state,
    /// the run end and <c>nextEventSeq</c> by the same rules the core holds them to (M1-10 task 1).
    /// </summary>
    public static GameState Load(string json) => PersistenceJson.Read<StateDto, GameState>(json, FromDto);

    internal static SaveHeader ReadHeader(string json) => PersistenceJson.Read<StateDto, SaveHeader>(json, dto => dto.ToHeader());

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
                [.. PersistenceJson.Need(u.Boons, "unit boons")],
                PersistenceJson.Need(u.Stats, "unit stats").ToStats(),
                PersistenceJson.Need(u.Hp, "unit hp"),
                PersistenceJson.Need(u.Budget, "unit budget").ToBudget(),
                [.. PersistenceJson.Need(u.Spent, "unit spent").Select(p => PersistenceJson.Need(p, "unit spent count").ToSpend())]);
            NeedBoonIds(unit.Boons, $"unit {unit.Id.Value} boons");
            units.Add(unit);
        }

        // The snapshots must be the ones the stored hashes were taken over (SV 4.6). The build identity
        // (rulesVersion, genVersion, manifestHash) is checked for form but not compared with this build in M1:
        // comparability is M2 and the replay rule is M1-27 (M1-06 plan call 6). It stays readable in the file
        // through SaveHeader.Read; a loaded run saved again is stamped with this build's identity, because the
        // run now continues on this build (M1-06 final-review ruling).
        PersistenceJson.Need(dto.RulesVersion, "rulesVersion");
        PersistenceJson.Need(dto.GenVersion, "genVersion");
        Digest.Parse(PersistenceJson.Need(dto.ManifestHash, "manifestHash"));
        var config = RunConfig.Validate(PersistenceJson.Need(dto.Config, "config").ToConfig());
        var unlocks = UnlockSnapshot.Validate(
            new UnlockSnapshot([.. PersistenceJson.Need(PersistenceJson.Need(dto.Unlocks, "unlocks").Ids, "unlock ids")]));
        if (Digest.Parse(PersistenceJson.Need(dto.ConfigHash, "configHash")) != config.ConfigHash)
        {
            throw new SnapshotFormatException("Invalid save: configHash does not match the stored config.");
        }

        if (Digest.Parse(PersistenceJson.Need(dto.UnlockHash, "unlockHash")) != unlocks.UnlockHash)
        {
            throw new SnapshotFormatException("Invalid save: unlockHash does not match the stored unlocks.");
        }

        // Create applies the same rules as a new run: a run seed below 2^60, unique ids, walkable
        // squares, one unit per square. Its streams have the right keys; the saved counters are put back below.
        var state = GameState.Create(
            map,
            units,
            PersistenceJson.Need(dto.RunSeed, "runSeed"),
            config,
            unlocks,
            dto.Hero is { } hero ? new UnitId(hero) : null);

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

        long nextEffectSeq = PersistenceJson.Need(dto.NextEffectSeq, "nextEffectSeq");
        if (nextEffectSeq < 1)
        {
            throw new SnapshotFormatException($"Invalid save: nextEffectSeq {nextEffectSeq} is below 1.");
        }

        var effects = ReadEffects(PersistenceJson.Need(dto.Effects, "effects"), state, nextEffectSeq);

        long nextEventSeq = PersistenceJson.Need(dto.NextEventSeq, "nextEventSeq");
        var combat = dto.Combat is { } c
            ? new CombatState(
                [.. PersistenceJson.Need(c.Order, "combat order").Select(id => new UnitId(id))],
                PersistenceJson.Need(c.Current, "combat current"),
                PersistenceJson.Need(c.Round, "combat round"),
                PersistenceJson.Need(c.TurnVacated, "combat turnVacated"))
            : null;
        var ended = dto.Ended is { } e
            ? new RunEnd(PersistenceJson.ParseEnum<RunEndCause>(e.Cause, "run end cause"), e.KillingEventId)
            : null;

        // The same rules the core holds these fields to; a breach surfaces as a format error.
        GameState.CheckRunState(state.Units, combat, ended, nextEventSeq);

        return state with
        {
            Combat = combat,
            Ended = ended,
            NextEventSeq = nextEventSeq,
            Effects = effects,
            NextEffectSeq = nextEffectSeq,
            Rng = rng,
            Pending = pending,
            Tick = tick,
            NextDecisionId = nextDecisionId,
        };
    }

    /// <summary>The active effects, checked against the rules: Apply hands out seqs in order and never reuses one.</summary>
    private static ImmutableArray<EffectInstance> ReadEffects(EffectDto?[] dtos, GameState state, long nextEffectSeq)
    {
        var effects = ImmutableArray.CreateBuilder<EffectInstance>(dtos.Length);
        foreach (var e in dtos)
        {
            if (e is null)
            {
                throw new SnapshotFormatException("Invalid save: an effect is null.");
            }

            long seq = PersistenceJson.Need(e.Seq, "effect seq");
            string id = PersistenceJson.Need(e.Effect, "effect id");
            var holder = new UnitId(PersistenceJson.Need(e.Holder, "effect holder"));
            var source = new EffectSource(e.Source is { } unit ? new UnitId(unit) : null);
            if (seq < 1 || seq >= nextEffectSeq)
            {
                throw new SnapshotFormatException(
                    $"Invalid save: effect seq {seq} was never issued (nextEffectSeq is {nextEffectSeq}).");
            }

            if (effects.Count > 0 && effects[^1].Seq >= seq)
            {
                throw new SnapshotFormatException(effects[^1].Seq == seq
                    ? $"Invalid save: two effects have seq {seq}."
                    : $"Invalid save: effect seq {seq} follows seq {effects[^1].Seq}; effects are sorted by seq.");
            }

            if (!ContentId.IsValid(id))
            {
                throw new SnapshotFormatException($"Invalid save: effect {seq} id '{id}' is not a content id.");
            }

            if (state.FindUnit(holder) is null)
            {
                throw new SnapshotFormatException($"Invalid save: effect {seq} is held by unknown unit {holder.Value}.");
            }

            if (source.Unit is { } from && state.FindUnit(from) is null)
            {
                throw new SnapshotFormatException($"Invalid save: effect {seq} comes from unknown unit {from.Value}.");
            }

            effects.Add(new EffectInstance(seq, id, holder, source));
        }

        return effects.MoveToImmutable();
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
        [JsonPropertyName("rulesVersion"), JsonPropertyOrder(1)] public int? RulesVersion { get; set; }
        [JsonPropertyName("genVersion"), JsonPropertyOrder(2)] public int? GenVersion { get; set; }
        [JsonPropertyName("manifestHash"), JsonPropertyOrder(3)] public string? ManifestHash { get; set; }
        [JsonPropertyName("configHash"), JsonPropertyOrder(4)] public string? ConfigHash { get; set; }
        [JsonPropertyName("unlockHash"), JsonPropertyOrder(5)] public string? UnlockHash { get; set; }
        [JsonPropertyName("runSeed"), JsonPropertyOrder(6)] public ulong? RunSeed { get; set; }
        [JsonPropertyName("config"), JsonPropertyOrder(7)] public ConfigDto? Config { get; set; }
        [JsonPropertyName("unlocks"), JsonPropertyOrder(8)] public UnlocksDto? Unlocks { get; set; }
        [JsonPropertyName("map"), JsonPropertyOrder(9)] public string[]? Map { get; set; }
        [JsonPropertyName("units"), JsonPropertyOrder(10)] public UnitDto?[]? Units { get; set; }
        [JsonPropertyName("rng"), JsonPropertyOrder(11)] public RngStreamsDto? Rng { get; set; }
        // Required: Save always writes it (as null when nothing is pending), so its absence means a damaged file.
        [JsonPropertyName("pending"), JsonPropertyOrder(12), JsonRequired] public PendingDto? Pending { get; set; }
        [JsonPropertyName("tick"), JsonPropertyOrder(13)] public long? Tick { get; set; }
        [JsonPropertyName("nextDecisionId"), JsonPropertyOrder(14)] public int? NextDecisionId { get; set; }
        [JsonPropertyName("effects"), JsonPropertyOrder(15)] public EffectDto?[]? Effects { get; set; }
        [JsonPropertyName("nextEffectSeq"), JsonPropertyOrder(16)] public long? NextEffectSeq { get; set; }
        // Required: Save always writes these (hero, combat and ended as null when absent).
        [JsonPropertyName("hero"), JsonPropertyOrder(17), JsonRequired] public int? Hero { get; set; }
        [JsonPropertyName("combat"), JsonPropertyOrder(18), JsonRequired] public CombatDto? Combat { get; set; }
        [JsonPropertyName("ended"), JsonPropertyOrder(19), JsonRequired] public RunEndDto? Ended { get; set; }
        [JsonPropertyName("nextEventSeq"), JsonPropertyOrder(20), JsonRequired] public long? NextEventSeq { get; set; }

        public SaveHeader ToHeader() => new(
            PersistenceJson.Need(Version, "version"),
            PersistenceJson.Need(RulesVersion, "rulesVersion"),
            PersistenceJson.Need(GenVersion, "genVersion"),
            Digest.Parse(PersistenceJson.Need(ManifestHash, "manifestHash")),
            Digest.Parse(PersistenceJson.Need(ConfigHash, "configHash")),
            Digest.Parse(PersistenceJson.Need(UnlockHash, "unlockHash")),
            PersistenceJson.Need(RunSeed, "runSeed"));
    }

    /// <summary>The run configuration snapshot (RS 11.1, SV-12); sets are written sorted.</summary>
    private sealed class ConfigDto
    {
        [JsonPropertyName("routeFloorCount"), JsonPropertyOrder(0)] public int? RouteFloorCount { get; set; }
        [JsonPropertyName("routeTwoDoorsEnabled"), JsonPropertyOrder(1)] public bool? RouteTwoDoorsEnabled { get; set; }
        [JsonPropertyName("routeLastHollowChoiceEnabled"), JsonPropertyOrder(2)] public bool? RouteLastHollowChoiceEnabled { get; set; }
        [JsonPropertyName("routeDoorCamps"), JsonPropertyOrder(3)] public int[]? RouteDoorCamps { get; set; }
        [JsonPropertyName("pacingTrimmedFloors"), JsonPropertyOrder(4)] public int[]? PacingTrimmedFloors { get; set; }
        [JsonPropertyName("recruitCompanionCap"), JsonPropertyOrder(5)] public int? RecruitCompanionCap { get; set; }
        [JsonPropertyName("difficultyTier"), JsonPropertyOrder(6)] public int? DifficultyTier { get; set; }
        [JsonPropertyName("boons"), JsonPropertyOrder(7)] public string?[]? Boons { get; set; }
        [JsonPropertyName("nightmareTier"), JsonPropertyOrder(8)] public int? NightmareTier { get; set; }
        // Required: Save always writes it (as null when there is no run modifier).
        [JsonPropertyName("runModifierId"), JsonPropertyOrder(9), JsonRequired] public string? RunModifierId { get; set; }

        public static ConfigDto From(RunConfig c) => new()
        {
            RouteFloorCount = c.RouteFloorCount,
            RouteTwoDoorsEnabled = c.RouteTwoDoorsEnabled,
            RouteLastHollowChoiceEnabled = c.RouteLastHollowChoiceEnabled,
            RouteDoorCamps = c.RouteDoorCamps.OrderBy(n => n).ToArray(),
            PacingTrimmedFloors = c.PacingTrimmedFloors.OrderBy(n => n).ToArray(),
            RecruitCompanionCap = c.RecruitCompanionCap,
            DifficultyTier = c.DifficultyTier,
            Boons = c.Boons.OrderBy(b => b, StringComparer.Ordinal).ToArray(),
            NightmareTier = c.NightmareTier,
            RunModifierId = c.RunModifierId,
        };

        public RunConfig ToConfig() => new(
            PersistenceJson.Need(RouteFloorCount, "config routeFloorCount"),
            PersistenceJson.Need(RouteTwoDoorsEnabled, "config routeTwoDoorsEnabled"),
            PersistenceJson.Need(RouteLastHollowChoiceEnabled, "config routeLastHollowChoiceEnabled"),
            [.. PersistenceJson.Need(RouteDoorCamps, "config routeDoorCamps")],
            [.. PersistenceJson.Need(PacingTrimmedFloors, "config pacingTrimmedFloors")],
            PersistenceJson.Need(RecruitCompanionCap, "config recruitCompanionCap"),
            PersistenceJson.Need(DifficultyTier, "config difficultyTier"),
            [.. PersistenceJson.Need(Boons, "config boons").Select(b => PersistenceJson.Need(b, "config boon"))],
            PersistenceJson.Need(NightmareTier, "config nightmareTier"),
            RunModifierId);
    }

    /// <summary>The unlock snapshot: its ids, sorted.</summary>
    private sealed class UnlocksDto
    {
        [JsonPropertyName("ids"), JsonPropertyOrder(0)] public string[]? Ids { get; set; }
    }

    private sealed class UnitDto
    {
        [JsonPropertyName("id"), JsonPropertyOrder(0)] public int? Id { get; set; }
        [JsonPropertyName("x"), JsonPropertyOrder(1)] public int? X { get; set; }
        [JsonPropertyName("y"), JsonPropertyOrder(2)] public int? Y { get; set; }
        [JsonPropertyName("faction"), JsonPropertyOrder(3)] public string? Faction { get; set; }
        [JsonPropertyName("boons"), JsonPropertyOrder(4)] public int[]? Boons { get; set; }
        [JsonPropertyName("stats"), JsonPropertyOrder(5), JsonRequired] public StatsDto? Stats { get; set; }
        [JsonPropertyName("hp"), JsonPropertyOrder(6), JsonRequired] public int? Hp { get; set; }
        [JsonPropertyName("budget"), JsonPropertyOrder(7), JsonRequired] public BudgetDto? Budget { get; set; }
        [JsonPropertyName("spent"), JsonPropertyOrder(8), JsonRequired] public PoolSpendDto?[]? Spent { get; set; }
    }

    /// <summary>A unit's turn budget (AB 14.2), as used counts and flags.</summary>
    private sealed class BudgetDto
    {
        [JsonPropertyName("movementUsed"), JsonPropertyOrder(0), JsonRequired] public int? MovementUsed { get; set; }
        [JsonPropertyName("actionsUsed"), JsonPropertyOrder(1), JsonRequired] public int? ActionsUsed { get; set; }
        [JsonPropertyName("bonusUsed"), JsonPropertyOrder(2), JsonRequired] public bool? BonusUsed { get; set; }
        [JsonPropertyName("reactionUsed"), JsonPropertyOrder(3), JsonRequired] public bool? ReactionUsed { get; set; }
        [JsonPropertyName("freeUsed"), JsonPropertyOrder(4), JsonRequired] public bool? FreeUsed { get; set; }
        [JsonPropertyName("campSpent"), JsonPropertyOrder(5), JsonRequired] public bool? CampSpent { get; set; }

        public static BudgetDto From(TurnBudget b) => new()
        {
            MovementUsed = b.MovementUsed,
            ActionsUsed = b.ActionsUsed,
            BonusUsed = b.BonusUsed,
            ReactionUsed = b.ReactionUsed,
            FreeUsed = b.FreeUsed,
            CampSpent = b.CampSpent,
        };

        public TurnBudget ToBudget() => new(
            PersistenceJson.Need(MovementUsed, "budget movementUsed"),
            PersistenceJson.Need(ActionsUsed, "budget actionsUsed"),
            PersistenceJson.Need(BonusUsed, "budget bonusUsed"),
            PersistenceJson.Need(ReactionUsed, "budget reactionUsed"),
            PersistenceJson.Need(FreeUsed, "budget freeUsed"),
            PersistenceJson.Need(CampSpent, "budget campSpent"));
    }

    /// <summary>How much of one pool a unit has spent; the list is written in stored (pool id) order.</summary>
    private sealed class PoolSpendDto
    {
        [JsonPropertyName("pool"), JsonPropertyOrder(0), JsonRequired] public string? Pool { get; set; }
        [JsonPropertyName("spent"), JsonPropertyOrder(1), JsonRequired] public int? Spent { get; set; }

        public PoolSpend ToSpend() => new(
            PersistenceJson.Need(Pool, "spent pool"), PersistenceJson.Need(Spent, "spent count"));
    }

    /// <summary>The fight in progress: the initiative order as unit ids, the current slot, the round.</summary>
    private sealed class CombatDto
    {
        [JsonPropertyName("order"), JsonPropertyOrder(0), JsonRequired] public int[]? Order { get; set; }
        [JsonPropertyName("current"), JsonPropertyOrder(1), JsonRequired] public int? Current { get; set; }
        [JsonPropertyName("round"), JsonPropertyOrder(2), JsonRequired] public long? Round { get; set; }
        [JsonPropertyName("turnVacated"), JsonPropertyOrder(3), JsonRequired] public bool? TurnVacated { get; set; }
    }

    /// <summary>The run end; the cause by name, the killing event id null unless the hero died.</summary>
    private sealed class RunEndDto
    {
        [JsonPropertyName("cause"), JsonPropertyOrder(0), JsonRequired] public string? Cause { get; set; }
        // Required: Save always writes it (as null when the cause is not a hero death).
        [JsonPropertyName("killingEventId"), JsonPropertyOrder(1), JsonRequired] public long? KillingEventId { get; set; }
    }

    /// <summary>A unit's stat block; enums are written by name, trained skills in skill order.</summary>
    private sealed class StatsDto
    {
        [JsonPropertyName("level"), JsonPropertyOrder(0), JsonRequired] public int? Level { get; set; }
        [JsonPropertyName("abilities"), JsonPropertyOrder(1), JsonRequired] public AbilitiesDto? Abilities { get; set; }
        [JsonPropertyName("mainAbility"), JsonPropertyOrder(2), JsonRequired] public string? MainAbility { get; set; }
        [JsonPropertyName("trainedSave"), JsonPropertyOrder(3), JsonRequired] public string? TrainedSave { get; set; }
        [JsonPropertyName("trainedSkills"), JsonPropertyOrder(4), JsonRequired] public string?[]? TrainedSkills { get; set; }
        [JsonPropertyName("hpTier"), JsonPropertyOrder(5), JsonRequired] public string? HpTier { get; set; }
        [JsonPropertyName("tableHp"), JsonPropertyOrder(6), JsonRequired] public int? TableHp { get; set; }

        public static StatsDto From(UnitStats s) => new()
        {
            Level = s.Level,
            Abilities = new AbilitiesDto
            {
                Strength = s.Abilities.Strength,
                Dexterity = s.Abilities.Dexterity,
                Constitution = s.Abilities.Constitution,
                Intelligence = s.Abilities.Intelligence,
                Wisdom = s.Abilities.Wisdom,
                Presence = s.Abilities.Presence,
            },
            MainAbility = s.MainAbility.ToString(),
            TrainedSave = s.TrainedSave.ToString(),
            TrainedSkills = s.TrainedSkills.Select(k => (string?)k.ToString()).ToArray(),
            HpTier = s.HpTier.ToString(),
            TableHp = s.TableHp,
        };

        public UnitStats ToStats()
        {
            var a = PersistenceJson.Need(Abilities, "stats abilities");
            return new UnitStats(
                PersistenceJson.Need(Level, "stats level"),
                new Dungeon.Core.Stats.Abilities(
                    PersistenceJson.Need(a.Strength, "stats strength"),
                    PersistenceJson.Need(a.Dexterity, "stats dexterity"),
                    PersistenceJson.Need(a.Constitution, "stats constitution"),
                    PersistenceJson.Need(a.Intelligence, "stats intelligence"),
                    PersistenceJson.Need(a.Wisdom, "stats wisdom"),
                    PersistenceJson.Need(a.Presence, "stats presence")),
                PersistenceJson.ParseEnum<Ability>(MainAbility, "main ability"),
                PersistenceJson.ParseEnum<Save>(TrainedSave, "trained save"),
                [.. PersistenceJson.Need(TrainedSkills, "stats trainedSkills")
                    .Select(k => PersistenceJson.ParseEnum<Skill>(k, "trained skill"))],
                PersistenceJson.ParseEnum<HpTier>(HpTier, "hp tier"),
                PersistenceJson.Need(TableHp, "stats tableHp"));
        }
    }

    private sealed class AbilitiesDto
    {
        [JsonPropertyName("strength"), JsonPropertyOrder(0), JsonRequired] public int? Strength { get; set; }
        [JsonPropertyName("dexterity"), JsonPropertyOrder(1), JsonRequired] public int? Dexterity { get; set; }
        [JsonPropertyName("constitution"), JsonPropertyOrder(2), JsonRequired] public int? Constitution { get; set; }
        [JsonPropertyName("intelligence"), JsonPropertyOrder(3), JsonRequired] public int? Intelligence { get; set; }
        [JsonPropertyName("wisdom"), JsonPropertyOrder(4), JsonRequired] public int? Wisdom { get; set; }
        [JsonPropertyName("presence"), JsonPropertyOrder(5), JsonRequired] public int? Presence { get; set; }
    }

    /// <summary>The stream counters (the next draw index of each stream); keys are not stored.</summary>
    private sealed class RngStreamsDto
    {
        [JsonPropertyName("combat"), JsonPropertyOrder(0)] public ulong? Combat { get; set; }
        [JsonPropertyName("shrine"), JsonPropertyOrder(1)] public ulong? Shrine { get; set; }
    }

    /// <summary>An active effect; <c>source</c> is the source unit's id, or null for the environment.</summary>
    private sealed class EffectDto
    {
        [JsonPropertyName("seq"), JsonPropertyOrder(0)] public long? Seq { get; set; }
        [JsonPropertyName("effect"), JsonPropertyOrder(1)] public string? Effect { get; set; }
        [JsonPropertyName("holder"), JsonPropertyOrder(2)] public int? Holder { get; set; }
        // Required: Save always writes it (as null for the environment).
        [JsonPropertyName("source"), JsonPropertyOrder(3), JsonRequired] public int? Source { get; set; }
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

/// <summary>
/// The key header of a save (the M1 subset of SV 9.2 <c>SaveHeader</c>; <c>runId</c>, <c>seq</c> and the
/// comparability fields arrive with M1-27 and M2).
/// </summary>
public sealed record SaveHeader(
    int SaveVersion, int RulesVersion, int GenVersion, Digest ManifestHash, Digest ConfigHash, Digest UnlockHash, ulong RunSeed)
{
    /// <summary>
    /// Reads the header of a <see cref="Snapshot.Save"/> output without loading the state. Throws as
    /// <see cref="Snapshot.Load"/> does for an unread version or a malformed document.
    /// </summary>
    public static SaveHeader Read(string json) => Snapshot.ReadHeader(json);
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
