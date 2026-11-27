using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core;

/// <summary>Something that happened while applying a command, in the order it happened.</summary>
public abstract record GameEvent;

public sealed record UnitMoved(UnitId Unit, GridPos From, GridPos To) : GameEvent;

/// <summary>The game clock advanced to <paramref name="Tick"/>.</summary>
public sealed record TickAdvanced(long Tick) : GameEvent;

/// <summary>The game now waits for an answer to <paramref name="Decision"/>.</summary>
public sealed record DecisionRequested(PendingDecision Decision) : GameEvent;

/// <summary>A pending decision was answered; <paramref name="ChosenOption"/> is the option's value, not its index.</summary>
public sealed record DecisionResolved(DecisionId Decision, UnitId Unit, int ChosenOption) : GameEvent;

/// <summary>The square at <paramref name="Pos"/> became <paramref name="Tile"/>.</summary>
public sealed record TileChanged(GridPos Pos, Tile Tile) : GameEvent;

/// <summary>
/// A trigger's <c>applyEffect</c> gave <paramref name="Holder"/> a new instance <paramref name="Seq"/> of
/// <paramref name="EffectId"/>, from <paramref name="Source"/> (the triggering effect's holder).
/// </summary>
public sealed record EffectApplied(long Seq, string EffectId, UnitId Holder, EffectSource Source) : GameEvent;

/// <summary>A trigger of the instance <paramref name="Seq"/> called the C# hook <paramref name="HookId"/>; the events the hook raises follow.</summary>
public sealed record HookFired(string HookId, long Seq) : GameEvent;

/// <summary>
/// A fight began (M1-10 plan call 6): <paramref name="Order"/> is the initiative rolled for it, in turn order. The record
/// compares <paramref name="Order"/> by reference; compare it with <c>SequenceEqual</c>.
/// </summary>
public sealed record CombatStarted(ImmutableArray<InitiativeEntry> Order) : GameEvent;

/// <summary><paramref name="Unit"/>'s turn began, in round <paramref name="Round"/> (M1-10 plan call 6).</summary>
public sealed record TurnStarted(UnitId Unit, long Round) : GameEvent;

/// <summary><paramref name="Unit"/>'s turn ended (M1-10 plan call 9).</summary>
public sealed record TurnEnded(UnitId Unit) : GameEvent;

/// <summary>
/// The damage event <paramref name="Seq"/> (M1-10 plan calls 1 and 11): <paramref name="Target"/> took
/// <paramref name="Total"/> and is now on <paramref name="HpAfter"/>.
/// </summary>
public sealed record DamageDealt(long Seq, UnitId Target, long Total, int HpAfter) : GameEvent;

/// <summary>A floor stopped the damage event <paramref name="Seq"/> on 1 HP (DH 20.3 step 4; M1-10 plan call 10).</summary>
public sealed record DamageClamped(long Seq, UnitId Target, ClampReason Reason) : GameEvent;

/// <summary>
/// <paramref name="Unit"/> reached 0 and was removed (M1-10 owner ruling 3); <paramref name="At"/> is its last square.
/// </summary>
public sealed record UnitDied(UnitId Unit, GridPos At) : GameEvent;

/// <summary>
/// The run ended (DH 10.1, D-08): <paramref name="KillingEventId"/> is the killing damage event's sequence id, present
/// only for <see cref="RunEndCause.HeroDeath"/>. It is the last event of its command.
/// </summary>
public sealed record RunEnded(RunEndCause Cause, long? KillingEventId) : GameEvent;
