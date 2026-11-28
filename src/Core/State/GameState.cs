using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.AbilityModel;
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
/// an empty array, so every unit, however built, holds a real one. <see cref="Opener"/> is the "opener charged, first
/// turn not yet started" mark (M1-11 plan call 6), or null. <see cref="Abilities"/> holds the ids of the abilities the
/// unit may use and react with (M1-11 plan call 2), ordinal-sorted and distinct; an uninitialised array is held as an
/// empty one, as for <see cref="Spent"/>. <see cref="Reactions"/> holds the unit's reaction policy overrides (M1-11 plan
/// call 13), sorted by ability id in ordinal order, each once; an uninitialised array is held as an empty one.
/// </summary>
public sealed record Unit(
    UnitId Id,
    GridPos Pos,
    Faction Faction,
    ImmutableArray<int> Boons,
    UnitStats Stats,
    int Hp,
    TurnBudget Budget = default,
    ImmutableArray<PoolSpend> Spent = default,
    OpenerCharge? Opener = null,
    ImmutableArray<string> Abilities = default,
    ImmutableArray<ReactionSetting> Reactions = default)
{
    private readonly ImmutableArray<PoolSpend> _spent = Spent.IsDefault ? [] : Spent;
    private readonly ImmutableArray<string> _abilities = Abilities.IsDefault ? [] : Abilities;
    private readonly ImmutableArray<ReactionSetting> _reactions = Reactions.IsDefault ? [] : Reactions;

    /// <summary>The unit's pool spent counts; never an uninitialised array.</summary>
    public ImmutableArray<PoolSpend> Spent
    {
        get => _spent;
        init => _spent = value.IsDefault ? [] : value;
    }

    /// <summary>The ids of the abilities the unit holds; never an uninitialised array.</summary>
    public ImmutableArray<string> Abilities
    {
        get => _abilities;
        init => _abilities = value.IsDefault ? [] : value;
    }

    /// <summary>The unit's reaction policy overrides; never an uninitialised array.</summary>
    public ImmutableArray<ReactionSetting> Reactions
    {
        get => _reactions;
        init => _reactions = value.IsDefault ? [] : value;
    }
}

public readonly record struct DecisionId(int Value);

/// <summary>
/// What a pending decision asks: <see cref="ShrineBoon"/>, a boon from a shrine; <see cref="Reaction"/>, whether to take
/// an "ask me" reaction (M1-11 plan call 12; options <c>[0 = decline, 1 = take]</c>), with the suspended use held in
/// <see cref="GameState.Resolution"/>.
/// </summary>
public enum DecisionKind { ShrineBoon, Reaction }

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
/// the sequence id the next sequenced event gets (M1-10 plan call 1); <see cref="Resolution"/> is the ability use
/// suspended on an "ask me" reaction, set exactly while a <see cref="DecisionKind.Reaction"/> decision is pending (M1-11
/// plan call 12).
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
    long NextEventSeq,
    AbilityFrame? Resolution)
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
    /// content id, sorted in ordinal order, each pool once, each count at least 1; its abilities must be content ids, sorted
    /// in ordinal order, each once; its reaction settings must name content ids in ordinal order, each once, with a defined
    /// mode, and "ask me" only on a party unit. <paramref name="hero"/>, when given, must name a
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
            map, sorted, seed, config, unlocks, ImmutableArray<EffectInstance>.Empty, 1, streams, null, 0, 1, hero, null, null, 1, null);
    }

    /// <summary>
    /// Checks the run fields <see cref="Create"/> does not receive, against <paramref name="units"/>; the save loader
    /// applies it (M1-10 task 1). <paramref name="nextEventSeq"/> is at least 1. A combat state has an initialised order
    /// of distinct units in the state, a round of at least 1, and <c>Current</c> in [0, order length), or equal to the
    /// order length only when its turn is vacated. The before-round-1 marker (M1-11 plan call 7) needs round 1, current
    /// 0, no vacated turn and a non-empty order, and a pending <see cref="DecisionKind.Reaction"/> decision (an opener's
    /// suspended use, M1-11 plan call 12) or a run end (with neither, round 1 would already have started). The suspended
    /// use <paramref name="resolution"/> is checked for structure only, as <see cref="CheckResolution"/> says. An opener mark (plan call 6) needs combat in round 1, its unit in the order
    /// and that unit's round-1 turn not yet started (the marker is set, its slot is after the current one, or its slot is
    /// the current one and vacated); each faction has at most one. A run end has a defined cause and a killing event id
    /// exactly when the cause is <see cref="RunEndCause.HeroDeath"/>, from 1 to below <paramref name="nextEventSeq"/>; a
    /// hero death also needs a <paramref name="hero"/>, on 0 HP. Throws <see cref="ArgumentException"/> naming the
    /// problem.
    /// </summary>
    internal static void CheckRunState(
        ImmutableArray<Unit> units, UnitId? hero, CombatState? combat, RunEnd? ended, long nextEventSeq,
        PendingDecision? pending, AbilityFrame? resolution)
    {
        if (nextEventSeq < 1)
        {
            throw new ArgumentException($"Invalid run: nextEventSeq {nextEventSeq} is below 1.", nameof(nextEventSeq));
        }

        if (combat is not null)
        {
            CheckCombat(units, combat);

            // Only a reaction an opener triggered can hold the fight before round 1 (M1-11 plan call 12; record 5 step 8).
            if (combat.BeforeRound1 && pending?.Kind != DecisionKind.Reaction && ended is null)
            {
                throw new ArgumentException(
                    "Invalid combat: the before-round-1 marker is set with no pending reaction decision and no run end.",
                    nameof(combat));
            }
        }

        CheckOpenerMarks(units, combat);
        CheckResolution(units, combat, pending, resolution);

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

        if (combat.BeforeRound1 && (combat.Round != 1 || combat.Current != 0 || combat.TurnVacated || length == 0))
        {
            throw new ArgumentException(
                "Invalid combat: the before-round-1 marker needs round 1, current 0, no vacated turn and a non-empty "
                + $"order (round {combat.Round}, current {combat.Current}, turnVacated {combat.TurnVacated}, order of {length}).",
                nameof(combat));
        }
    }

    /// <summary>
    /// The structure of a suspended use (M1-11 plan call 12), the part of the check that needs no rules (the rest is
    /// <c>Resolution.Continue</c>'s): <paramref name="resolution"/> is set exactly when a <see cref="DecisionKind.Reaction"/>
    /// decision is pending, whose options are <c>[0, 1]</c>; a frame needs combat; its ability and candidate are content
    /// ids; its target is one of the three shapes, well formed; its user is in the state; its locked units are in unit
    /// order, each once, and <see cref="AbilityFrame.Index"/> is within them; the window is defined, and a save difficulty
    /// is held exactly for the own-save window; the roll is well formed (faces 1 to 20, a second die exactly when the
    /// mode is not normal) in the setup's mode; the reactor <c>Locked[Index]</c> is a party unit in the state (AB 7.3:
    /// only the party is asked), its reaction unused, holding the candidate; the decision names the reactor, on its
    /// square; and the held events (M1-12a plan call 12) are initialised, each one of the closed set a suspended use can
    /// hold (<see cref="UnitDied"/>, <see cref="UnitMoved"/>, <see cref="AbilityResolved"/> with a content id), whose
    /// units need not be in the state (a held <see cref="UnitDied"/>'s unit is gone). Throws
    /// <see cref="ArgumentException"/> naming the problem.
    /// </summary>
    private static void CheckResolution(
        ImmutableArray<Unit> units, CombatState? combat, PendingDecision? pending, AbilityFrame? resolution)
    {
        const string param = "resolution";
        bool asking = pending?.Kind == DecisionKind.Reaction;
        if (asking && (pending!.Options.IsDefault || pending.Options.Length != 2 || pending.Options[0] != 0 || pending.Options[1] != 1))
        {
            throw new ArgumentException("Invalid pending decision: a reaction decision's options are [0, 1] (decline, take).", nameof(pending));
        }

        if (resolution is null)
        {
            if (asking)
            {
                throw new ArgumentException("Invalid run: a reaction decision is pending with no suspended ability use.", param);
            }

            return;
        }

        if (!asking)
        {
            throw new ArgumentException("Invalid run: an ability use is suspended with no reaction decision pending.", param);
        }

        if (combat is null)
        {
            throw new ArgumentException("Invalid run: an ability use is suspended out of combat; only a combat roll can ask.", param);
        }

        var f = resolution;
        if (f.AbilityId is null || !ContentId.IsValid(f.AbilityId))
        {
            throw new ArgumentException($"Invalid frame: ability '{f.AbilityId}' is not a content id ([a-z0-9_.:-]).", param);
        }

        if (f.Candidate is null || !ContentId.IsValid(f.Candidate))
        {
            throw new ArgumentException($"Invalid frame: candidate '{f.Candidate}' is not a content id ([a-z0-9_.:-]).", param);
        }

        switch (f.Target)
        {
            case UnitsTarget { Units.IsDefault: false }:
            case PointTarget:
            case AimTarget aim when Enum.IsDefined(aim.Aim):
                break;
            default:
                throw new ArgumentException("Invalid frame: the target is missing or malformed.", param);
        }

        if (!units.Any(u => u.Id == f.User))
        {
            throw new ArgumentException($"Invalid frame: the user {f.User.Value} is not in the state.", param);
        }

        if (f.Locked.IsDefault)
        {
            throw new ArgumentException("Invalid frame: the locked units are uninitialised.", param);
        }

        for (int k = 1; k < f.Locked.Length; k++)
        {
            if (f.Locked[k - 1].Value >= f.Locked[k].Value)
            {
                throw new ArgumentException(
                    $"Invalid frame: locked units must be in unit order, each once ({f.Locked[k - 1].Value} then {f.Locked[k].Value}).", param);
            }
        }

        if (f.Index < 0 || f.Index >= f.Locked.Length)
        {
            throw new ArgumentException($"Invalid frame: index {f.Index} is outside the {f.Locked.Length} locked units.", param);
        }

        if (!Enum.IsDefined(f.Window))
        {
            throw new ArgumentException("Invalid frame: unknown reaction window.", param);
        }

        if ((f.Window == ReactionTrigger.OwnSave) != f.SaveDifficulty.HasValue)
        {
            throw new ArgumentException("Invalid frame: a save difficulty is held exactly for an own-save window.", param);
        }

        var roll = f.Roll;
        static bool Face(int n) => n >= 1 && n <= 20;
        if (!Enum.IsDefined(roll.Mode) || !Enum.IsDefined(f.Setup.Mode) || roll.Mode != f.Setup.Mode
            || !Face(roll.First)
            || (roll.Mode == RollMode.Normal) != (roll.Second is null)
            || (roll.Second is { } second && !Face(second)))
        {
            throw new ArgumentException("Invalid frame: the roll is malformed or not in the setup's mode.", param);
        }

        var reactorId = f.Locked[f.Index];
        var reactor = units.FirstOrDefault(u => u.Id == reactorId)
            ?? throw new ArgumentException($"Invalid frame: the reactor {reactorId.Value} is not in the state.", param);
        if (reactor.Faction != Faction.Party)
        {
            throw new ArgumentException($"Invalid frame: the reactor {reactorId.Value} is a {reactor.Faction} unit; only the party is asked.", param);
        }

        if (reactor.Budget.ReactionUsed)
        {
            throw new ArgumentException($"Invalid frame: the reactor {reactorId.Value} has used its reaction.", param);
        }

        if (reactor.Abilities.IndexOf(f.Candidate, StringComparer.Ordinal) < 0)
        {
            throw new ArgumentException($"Invalid frame: the reactor {reactorId.Value} does not hold '{f.Candidate}'.", param);
        }

        if (pending!.Unit != reactorId || pending.Source != reactor.Pos)
        {
            throw new ArgumentException(
                $"Invalid frame: the reaction decision must name the reactor {reactorId.Value} on its square ({reactor.Pos.X}, {reactor.Pos.Y}).", param);
        }

        if (f.Held.IsDefault)
        {
            throw new ArgumentException("Invalid frame: the held events are uninitialised.", param);
        }

        foreach (var held in f.Held)
        {
            switch (held)
            {
                case UnitDied:
                case UnitMoved:
                case AbilityResolved { AbilityId: { } id } when ContentId.IsValid(id):
                    break;
                case null:
                    throw new ArgumentException("Invalid frame: a held event is null.", param);
                default:
                    throw new ArgumentException($"Invalid frame: a suspended use does not hold a {held.GetType().Name} event like this one.", param);
            }
        }
    }

    // M1-11 plan call 6: a mark lives only on a unit whose round-1 turn has not started, one per faction.
    private static void CheckOpenerMarks(ImmutableArray<Unit> units, CombatState? combat)
    {
        for (int i = 0; i < units.Length; i++)
        {
            var unit = units[i];
            if (unit.Opener is null)
            {
                continue;
            }

            string who = $"Invalid units: unit {unit.Id.Value} has an opener mark";
            if (combat is null)
            {
                throw new ArgumentException($"{who} out of combat.", nameof(units));
            }

            if (combat.Round != 1)
            {
                throw new ArgumentException($"{who} in round {combat.Round}; a mark lasts only until round 1.", nameof(units));
            }

            int index = combat.Order.IndexOf(unit.Id);
            if (index < 0)
            {
                throw new ArgumentException($"{who} but is not in the turn order.", nameof(units));
            }

            bool unstarted = combat.BeforeRound1
                || index > combat.Current
                || (index == combat.Current && combat.TurnVacated);
            if (!unstarted)
            {
                throw new ArgumentException($"{who} but its round-1 turn has already started.", nameof(units));
            }

            for (int j = 0; j < i; j++)
            {
                if (units[j].Opener is not null && units[j].Faction == unit.Faction)
                {
                    throw new ArgumentException(
                        $"{who}, and so does unit {units[j].Id.Value}; a {unit.Faction} side has at most one opener.",
                        nameof(units));
                }
            }
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

        if (unit.Opener is { } opener)
        {
            CheckOpener(unit, opener, who, units);
        }

        // M1-11 plan call 2: the abilities the unit holds are content ids, ordinal-sorted and distinct.
        var abilities = unit.Abilities;
        for (int k = 0; k < abilities.Length; k++)
        {
            if (abilities[k] is not { } id || !ContentId.IsValid(id))
            {
                throw new ArgumentException($"{who}holds ability '{abilities[k]}', which is not a content id ([a-z0-9_.:-]).", units);
            }

            if (k > 0 && string.CompareOrdinal(abilities[k - 1], id) >= 0)
            {
                throw new ArgumentException(
                    $"{who}must list its abilities in ordinal order, each once ('{abilities[k - 1]}' then '{id}').", units);
            }
        }

        // M1-11 plan call 13: reaction settings name content ids, ordinal-sorted and distinct, with a defined mode; "ask
        // me" only on a party unit (AB 7.3: monsters have no "ask me").
        var reactions = unit.Reactions;
        for (int k = 0; k < reactions.Length; k++)
        {
            var setting = reactions[k] ?? throw new ArgumentException($"{who}has a null reaction setting.", units);
            if (setting.AbilityId is not { } id || !ContentId.IsValid(id))
            {
                throw new ArgumentException(
                    $"{who}has a reaction setting for '{setting.AbilityId}', which is not a content id ([a-z0-9_.:-]).", units);
            }

            if (!Enum.IsDefined(setting.Mode))
            {
                throw new ArgumentException($"{who}has an unknown reaction mode for '{id}'.", units);
            }

            if (setting.Mode == ReactionMode.AskMe && unit.Faction != Faction.Party)
            {
                throw new ArgumentException($"{who}is a {unit.Faction} unit with \"ask me\" on '{id}'; only a party unit may be asked.", units);
            }

            if (k > 0 && string.CompareOrdinal(reactions[k - 1].AbilityId, id) >= 0)
            {
                throw new ArgumentException(
                    $"{who}must list its reaction settings by ability id in ordinal order, each once ('{reactions[k - 1].AbilityId}' then '{id}').",
                    units);
            }
        }
    }

    // M1-11 plan call 6: the charge is well formed (CheckOpenerCharge), the budget includes it, and each turn-pool count is
    // within the unit's spent count.
    private static void CheckOpener(Unit unit, OpenerCharge opener, string who, string units)
    {
        CheckOpenerCharge(opener, who, units);
        if (!Includes(unit.Budget, opener.Charged))
        {
            throw new ArgumentException($"{who}has a turn budget that does not include its opener charge.", units);
        }

        foreach (var pool in opener.TurnPools)
        {
            int spent = SpentCount(unit.Spent, pool.PoolId);
            if (spent < pool.Spent)
            {
                throw new ArgumentException(
                    $"{who}charges {pool.Spent} of pool '{pool.PoolId}' to its opener, but its spent count is {spent}.", units);
            }
        }
    }

    /// <summary>
    /// The shape of an opener charge, the part of the load check that needs no unit (M1-11 plan call 6): no negative
    /// count; no reaction or camp-spend flag; initialised turn pools, sorted by pool id in ordinal order, each pool once,
    /// each id a content id and each count at least 1. Throws <see cref="ArgumentException"/> naming
    /// <paramref name="param"/>, its message starting with <paramref name="who"/>. <c>Turns.MarkOpener</c> runs the same
    /// check, so a marked state always loads.
    /// </summary>
    internal static void CheckOpenerCharge(OpenerCharge opener, string who, string param)
    {
        var charged = opener.Charged;
        if (charged.MovementUsed < 0 || charged.ActionsUsed < 0)
        {
            throw new ArgumentException($"{who}has an opener charge with a negative count.", param);
        }

        if (charged.ReactionUsed || charged.CampSpent)
        {
            throw new ArgumentException($"{who}has an opener charge that sets the reaction or camp-spend flag.", param);
        }

        var pools = opener.TurnPools;
        if (pools.IsDefault)
        {
            throw new ArgumentException($"{who}has uninitialised opener turn pools; use an empty array.", param);
        }

        for (int k = 0; k < pools.Length; k++)
        {
            var pool = pools[k];
            if (pool.PoolId is null || !ContentId.IsValid(pool.PoolId))
            {
                throw new ArgumentException(
                    $"{who}has an opener turn pool '{pool.PoolId}', which is not a content id ([a-z0-9_.:-]).", param);
            }

            if (pool.Spent < 1)
            {
                throw new ArgumentException(
                    $"{who}has an opener turn pool '{pool.PoolId}' count of {pool.Spent}; it must be at least 1.", param);
            }

            if (k > 0 && string.CompareOrdinal(pools[k - 1].PoolId, pool.PoolId) >= 0)
            {
                throw new ArgumentException(
                    $"{who}must list opener turn pools by pool id in ordinal order, each pool once ('{pools[k - 1].PoolId}' then '{pool.PoolId}').",
                    param);
            }
        }
    }

    /// <summary>
    /// Whether <paramref name="budget"/> includes <paramref name="charged"/>: at least its movement and actions used, and
    /// every flag it sets.
    /// </summary>
    internal static bool Includes(TurnBudget budget, TurnBudget charged) =>
        budget.MovementUsed >= charged.MovementUsed
        && budget.ActionsUsed >= charged.ActionsUsed
        && (!charged.BonusUsed || budget.BonusUsed)
        && (!charged.ReactionUsed || budget.ReactionUsed)
        && (!charged.FreeUsed || budget.FreeUsed)
        && (!charged.CampSpent || budget.CampSpent);

    /// <summary>The count of <paramref name="poolId"/> in <paramref name="spent"/>, or 0.</summary>
    internal static int SpentCount(ImmutableArray<PoolSpend> spent, string poolId)
    {
        foreach (var s in spent)
        {
            if (string.Equals(s.PoolId, poolId, StringComparison.Ordinal))
            {
                return s.Spent;
            }
        }

        return 0;
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
