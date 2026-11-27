using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core;

public readonly record struct UnitId(int Value);

public enum Faction { Party, Enemy }

/// <summary>
/// A unit on the grid. <see cref="Boons"/> holds the boon ids the unit has taken; <see cref="Stats"/> is its stat block.
/// <see cref="Hp"/> is its current HP (DH 7.3: "Each unit stores current HP; the maximum is derived on demand").
/// <see cref="Budget"/> is its turn budget (AB 14.2) and <see cref="Spent"/> its pool spent counts (AB 6.2), sorted by
/// pool id in ordinal order, each pool once and each count at least 1. An uninitialised <see cref="Spent"/> is held as
/// an empty array, so every unit, however built, holds a real one.
/// </summary>
public sealed record Unit(
    UnitId Id,
    GridPos Pos,
    Faction Faction,
    ImmutableArray<int> Boons,
    UnitStats Stats,
    int Hp,
    TurnBudget Budget = default,
    ImmutableArray<PoolSpend> Spent = default)
{
    private readonly ImmutableArray<PoolSpend> _spent = Spent.IsDefault ? [] : Spent;

    /// <summary>The unit's pool spent counts; never an uninitialised array.</summary>
    public ImmutableArray<PoolSpend> Spent
    {
        get => _spent;
        init => _spent = value.IsDefault ? [] : value;
    }
}

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
/// <see cref="Hero"/> is the unit whose death ends the run, or null; <see cref="Combat"/> is the fight in progress, null
/// out of combat; <see cref="Ended"/> is the run end, after which every command is refused; <see cref="NextEventSeq"/> is
/// the sequence id the next sequenced event gets (M1-10 plan call 1).
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
    int NextDecisionId,
    UnitId? Hero,
    CombatState? Combat,
    RunEnd? Ended,
    long NextEventSeq)
{
    /// <summary>True while a fight is in progress (DH 10.3's <c>combatActive</c>).</summary>
    public bool CombatActive => Combat is not null;

    /// <summary>
    /// Builds the starting state of a run. Units are sorted by id. Throws an
    /// <see cref="ArgumentException"/> naming the problem if two units share an id, a unit
    /// stands on a square that is not walkable, two units share a square, or a unit's
    /// <see cref="Unit.Boons"/> is an uninitialised array, or a unit's <see cref="Unit.Stats"/> is missing or
    /// outside the stat block's bounds (level 0 to 12, abilities -3 to +5, defined enum values, trained skills sorted and
    /// distinct), or a unit's HP basis or HP is invalid (a party unit needs an HP tier other than
    /// <see cref="HpTier.Table"/> and table HP 0, an enemy the <see cref="HpTier.Table"/> tier and table HP of at least 1;
    /// HP is never negative), and an
    /// <see cref="ArgumentOutOfRangeException"/> if <paramref name="seed"/> is at least 2^60. The run
    /// configuration defaults to <see cref="RunConfig.M1"/> and the unlocks to <see cref="UnlockSnapshot.None"/>;
    /// both are validated. The run starts with no active effects and <see cref="NextEffectSeq"/> 1. HP is not checked
    /// against the maximum (M1-09 plan call 2): a save loads through here before its effects are back, and an effect
    /// may raise the maximum. A unit's turn budget counts must not be negative, and its spent counts must name pools by
    /// content id, sorted in ordinal order, each pool once, each count at least 1. <paramref name="hero"/>, when given, must name a
    /// party unit in the state. The run starts out of combat, not ended, with <see cref="NextEventSeq"/> 1.
    /// </summary>
    public static GameState Create(
        GridMap map, IEnumerable<Unit> units, ulong seed, RunConfig? config = null, UnlockSnapshot? unlocks = null,
        UnitId? hero = null)
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

        if (hero is { } h)
        {
            var heroUnit = sorted.FirstOrDefault(u => u.Id == h)
                ?? throw new ArgumentException($"Invalid hero: unit {h.Value} is not in the state.", nameof(hero));
            if (heroUnit.Faction != Faction.Party)
            {
                throw new ArgumentException(
                    $"Invalid hero: unit {h.Value} is a {heroUnit.Faction} unit; the hero must be a party unit.", nameof(hero));
            }
        }

        return new GameState(
            map, sorted, seed, config, unlocks, ImmutableArray<EffectInstance>.Empty, 1, streams, null, 0, 1, hero, null, null, 1);
    }

    /// <summary>
    /// Checks the run fields <see cref="Create"/> does not receive, against <paramref name="units"/>; the save loader
    /// applies it (M1-10 task 1). <paramref name="nextEventSeq"/> is at least 1. A combat state has an initialised order
    /// of distinct units in the state, a round of at least 1, and <c>Current</c> in [0, order length), or equal to the
    /// order length only when its turn is vacated. A run end has a defined cause and a killing event id exactly when the
    /// cause is <see cref="RunEndCause.HeroDeath"/>, from 1 to below <paramref name="nextEventSeq"/>; a hero death also
    /// needs a <paramref name="hero"/>, on 0 HP. Throws <see cref="ArgumentException"/> naming the problem.
    /// </summary>
    internal static void CheckRunState(
        ImmutableArray<Unit> units, UnitId? hero, CombatState? combat, RunEnd? ended, long nextEventSeq)
    {
        if (nextEventSeq < 1)
        {
            throw new ArgumentException($"Invalid run: nextEventSeq {nextEventSeq} is below 1.", nameof(nextEventSeq));
        }

        if (combat is not null)
        {
            CheckCombat(units, combat);
        }

        if (ended is not null)
        {
            if (!Enum.IsDefined(ended.Cause))
            {
                throw new ArgumentException("Invalid run end: unknown cause.", nameof(ended));
            }

            if (ended.Cause == RunEndCause.HeroDeath)
            {
                long id = ended.KillingEventId
                    ?? throw new ArgumentException("Invalid run end: a hero death needs a killingEventId.", nameof(ended));
                if (id < 1 || id >= nextEventSeq)
                {
                    throw new ArgumentException(
                        $"Invalid run end: killingEventId {id} was never issued (nextEventSeq is {nextEventSeq}).",
                        nameof(ended));
                }

                var heroUnit = hero is { } h ? units.FirstOrDefault(u => u.Id == h) : null;
                if (heroUnit is null)
                {
                    throw new ArgumentException("Invalid run end: a hero death needs a hero.", nameof(ended));
                }

                if (heroUnit.Hp != 0)
                {
                    throw new ArgumentException(
                        $"Invalid run end: a hero death needs the hero on 0 HP, but unit {heroUnit.Id.Value} has {heroUnit.Hp}.",
                        nameof(ended));
                }
            }
            else if (ended.KillingEventId is not null)
            {
                throw new ArgumentException(
                    $"Invalid run end: a {ended.Cause} end has no killingEventId.", nameof(ended));
            }
        }
    }

    private static void CheckCombat(ImmutableArray<Unit> units, CombatState combat)
    {
        if (combat.Order.IsDefault)
        {
            throw new ArgumentException("Invalid combat: the order is uninitialised; use an empty array.", nameof(combat));
        }

        if (combat.Round < 1)
        {
            throw new ArgumentException($"Invalid combat: round {combat.Round} is below 1.", nameof(combat));
        }

        for (int i = 0; i < combat.Order.Length; i++)
        {
            var id = combat.Order[i];
            if (!units.Any(u => u.Id == id))
            {
                throw new ArgumentException($"Invalid combat: the order names unknown unit {id.Value}.", nameof(combat));
            }

            for (int j = 0; j < i; j++)
            {
                if (combat.Order[j] == id)
                {
                    throw new ArgumentException($"Invalid combat: the order names unit {id.Value} twice.", nameof(combat));
                }
            }
        }

        int length = combat.Order.Length;
        bool inOrder = combat.Current >= 0 && combat.Current < length;
        bool pastEnd = combat.Current == length && combat.TurnVacated;
        if (!inOrder && !pastEnd)
        {
            throw new ArgumentException(
                $"Invalid combat: current {combat.Current} is outside the order of {length}"
                + (combat.Current == length ? " and the turn is not vacated." : "."),
                nameof(combat));
        }
    }

    // M1-10 plan calls 5 and 7: the budget stores used counts; spent counts are sorted by pool id, positive, each pool once.
    private static void CheckTurnState(Unit unit, string who, string units)
    {
        if (unit.Budget.MovementUsed < 0)
        {
            throw new ArgumentException($"{who}has movement used {unit.Budget.MovementUsed}; it must not be negative.", units);
        }

        if (unit.Budget.ActionsUsed < 0)
        {
            throw new ArgumentException($"{who}has actions used {unit.Budget.ActionsUsed}; it must not be negative.", units);
        }

        var spent = unit.Spent;
        for (int k = 0; k < spent.Length; k++)
        {
            var pool = spent[k];
            if (pool.PoolId is null)
            {
                throw new ArgumentException($"{who}has a spent count with no pool id.", units);
            }

            if (!ContentId.IsValid(pool.PoolId))
            {
                throw new ArgumentException(
                    $"{who}has a spent count for pool '{pool.PoolId}', which is not a content id ([a-z0-9_.:-]).", units);
            }

            if (pool.Spent < 1)
            {
                throw new ArgumentException(
                    $"{who}has spent {pool.Spent} of pool '{pool.PoolId}'; a stored spent count must be at least 1.", units);
            }

            if (k > 0 && string.CompareOrdinal(spent[k - 1].PoolId, pool.PoolId) >= 0)
            {
                throw new ArgumentException(
                    $"{who}must list spent counts by pool id in ordinal order, each pool once ('{spent[k - 1].PoolId}' then '{pool.PoolId}').",
                    units);
            }
        }
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

        CheckHp(unit, st, who, units);
        CheckTurnState(unit, who, units);
    }

    // M1-09 plan call 2: the HP basis matches the faction, and current HP is not negative.
    private static void CheckHp(Unit unit, UnitStats st, string who, string units)
    {
        if (!Enum.IsDefined(st.HpTier))
        {
            throw new ArgumentException($"{who}has an unknown HP tier.", units);
        }

        if (unit.Faction == Faction.Party && st.HpTier == HpTier.Table)
        {
            throw new ArgumentException($"{who}is a party unit on HP tier Table; it needs a class HP tier.", units);
        }

        if (unit.Faction != Faction.Party && st.HpTier != HpTier.Table)
        {
            throw new ArgumentException($"{who}is an enemy on HP tier {st.HpTier}; it must use HP tier Table.", units);
        }

        if (st.HpTier != HpTier.Table && st.TableHp != 0)
        {
            throw new ArgumentException($"{who}has table HP {st.TableHp} beside HP tier {st.HpTier}; it must be 0.", units);
        }

        if (st.HpTier == HpTier.Table && st.TableHp < 1)
        {
            throw new ArgumentException($"{who}has table HP {st.TableHp}; it must be at least 1.", units);
        }

        if (unit.Hp < 0)
        {
            throw new ArgumentException($"{who}has HP {unit.Hp}; it must not be negative.", units);
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
