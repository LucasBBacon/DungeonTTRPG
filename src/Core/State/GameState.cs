using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core;

public readonly record struct UnitId(int Value);

public enum Faction { Party, Enemy }

/// <summary>
/// A unit on the grid. <see cref="Boons"/> holds the boon ids the unit has taken; <see cref="Stats"/> is its stat block.
/// </summary>
public sealed record Unit(UnitId Id, GridPos Pos, Faction Faction, ImmutableArray<int> Boons, UnitStats Stats);

public readonly record struct DecisionId(int Value);

public enum DecisionKind { ShrineBoon }

/// <summary>A choice the game is waiting on. While one is pending, only an answer to it is valid.</summary>
public sealed record PendingDecision(
    DecisionId Id, UnitId Unit, DecisionKind Kind, ImmutableArray<int> Options, GridPos Source);

/// <summary>
/// The complete immutable state of a run. <see cref="RunSeed"/> is the 60-bit seed every
/// random stream is derived from; <see cref="Rng"/> holds the streams' positions.
/// <see cref="Units"/> is kept sorted by id so that
/// every walk over it is in a fixed order. <see cref="Effects"/> holds the active effect instances, sorted by
/// <see cref="EffectInstance.Seq"/>; <see cref="NextEffectSeq"/> is the seq the next one gets (never reused).
/// Two states with the same contents are not equal
/// with <c>==</c> or <c>Equals</c> (the map and the arrays compare by reference); compare
/// their <see cref="Snapshot.Save"/> text instead.
/// </summary>
public sealed record GameState(
    GridMap Map,
    ImmutableArray<Unit> Units,
    ulong RunSeed,
    RunConfig Config,
    UnlockSnapshot Unlocks,
    ImmutableArray<EffectInstance> Effects,
    long NextEffectSeq,
    RunStreams Rng,
    PendingDecision? Pending,
    long Tick,
    int NextDecisionId)
{
    /// <summary>
    /// Builds the starting state of a run. Units are sorted by id. Throws an
    /// <see cref="ArgumentException"/> naming the problem if two units share an id, a unit
    /// stands on a square that is not walkable, two units share a square, or a unit's
    /// <see cref="Unit.Boons"/> is an uninitialised array, or a unit's <see cref="Unit.Stats"/> is missing or
    /// outside the stat block's bounds (level 0 to 12, abilities -3 to +5, defined enum values, trained skills sorted and
    /// distinct), and an
    /// <see cref="ArgumentOutOfRangeException"/> if <paramref name="seed"/> is at least 2^60. The run
    /// configuration defaults to <see cref="RunConfig.M1"/> and the unlocks to <see cref="UnlockSnapshot.None"/>;
    /// both are validated. The run starts with no active effects and <see cref="NextEffectSeq"/> 1.
    /// </summary>
    public static GameState Create(
        GridMap map, IEnumerable<Unit> units, ulong seed, RunConfig? config = null, UnlockSnapshot? unlocks = null)
    {
        ArgumentNullException.ThrowIfNull(map);
        ArgumentNullException.ThrowIfNull(units);
        config = RunConfig.Validate(config ?? RunConfig.M1);
        unlocks = UnlockSnapshot.Validate(unlocks ?? UnlockSnapshot.None);
        var streams = RunStreams.FromSeed(Dungeon.Core.RunSeed.Validate(seed, nameof(seed)));

        var sorted = units
            .Select(u => u ?? throw new ArgumentException("Invalid units: a unit is null.", nameof(units)))
            .OrderBy(u => u.Id.Value)
            .ToImmutableArray();

        for (int i = 0; i < sorted.Length; i++)
        {
            var unit = sorted[i];
            if (i > 0 && sorted[i - 1].Id == unit.Id)
            {
                throw new ArgumentException(
                    $"Invalid units: duplicate unit id {unit.Id.Value}.", nameof(units));
            }

            if (unit.Boons.IsDefault)
            {
                throw new ArgumentException(
                    $"Invalid units: unit {unit.Id.Value} has uninitialised boons; use an empty array.",
                    nameof(units));
            }

            CheckStats(unit, nameof(units));

            if (!map.IsWalkable(unit.Pos))
            {
                throw new ArgumentException(
                    $"Invalid units: unit {unit.Id.Value} is on non-walkable square ({unit.Pos.X}, {unit.Pos.Y}).",
                    nameof(units));
            }

            for (int j = 0; j < i; j++)
            {
                if (sorted[j].Pos == unit.Pos)
                {
                    throw new ArgumentException(
                        $"Invalid units: units {sorted[j].Id.Value} and {unit.Id.Value} share square ({unit.Pos.X}, {unit.Pos.Y}).",
                        nameof(units));
                }
            }
        }

        return new GameState(map, sorted, seed, config, unlocks, ImmutableArray<EffectInstance>.Empty, 1, streams, null, 0, 1);
    }

    private static void CheckStats(Unit unit, string units)
    {
        string who = $"Invalid units: unit {unit.Id.Value} ";
        var st = unit.Stats ?? throw new ArgumentException(who + "has no stat block.", units);
        if (st.Level < UnitStats.MinLevel || st.Level > UnitStats.MaxLevel)
        {
            throw new ArgumentException(
                $"{who}has level {st.Level}; it must be {UnitStats.MinLevel} to {UnitStats.MaxLevel}.", units);
        }

        foreach (var a in Enum.GetValues<Ability>())
        {
            int bonus = st.Abilities[a];
            if (bonus < UnitStats.MinAbility || bonus > UnitStats.MaxAbility)
            {
                throw new ArgumentException(
                    $"{who}has {a} {bonus}; abilities must be {UnitStats.MinAbility} to {UnitStats.MaxAbility}.", units);
            }
        }

        if (!Enum.IsDefined(st.MainAbility))
        {
            throw new ArgumentException($"{who}has an unknown main ability.", units);
        }

        if (!Enum.IsDefined(st.TrainedSave))
        {
            throw new ArgumentException($"{who}has an unknown trained save.", units);
        }

        if (st.TrainedSkills.IsDefault)
        {
            throw new ArgumentException($"{who}has uninitialised trained skills; use an empty array.", units);
        }

        for (int k = 0; k < st.TrainedSkills.Length; k++)
        {
            var skill = st.TrainedSkills[k];
            if (!Enum.IsDefined(skill))
            {
                throw new ArgumentException($"{who}trains an unknown skill.", units);
            }

            if (k > 0 && st.TrainedSkills[k - 1] >= skill)
            {
                throw new ArgumentException(
                    $"{who}must list trained skills in order, each once ({st.TrainedSkills[k - 1]} then {skill}).", units);
            }
        }
    }

    /// <summary>
    /// The run's reproducibility key (SV 4.6): its seed, this build's generator version and manifest hash,
    /// and the hashes of its configuration and unlock snapshots.
    /// </summary>
    public RunKey Key => RunKey.For(RunSeed, Config, Unlocks);

    /// <summary>The unit with this id, or null if there is none.</summary>
    public Unit? FindUnit(UnitId id)
    {
        foreach (var unit in Units)
        {
            if (unit.Id == id)
            {
                return unit;
            }
        }

        return null;
    }

    /// <summary>The unit standing on <paramref name="p"/>, or null if the square is empty.</summary>
    public Unit? UnitAt(GridPos p)
    {
        foreach (var unit in Units)
        {
            if (unit.Pos == p)
            {
                return unit;
            }
        }

        return null;
    }
}
