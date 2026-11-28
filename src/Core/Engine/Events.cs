using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Conditions;
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

/// <summary>
/// The fight ended (DH 20.5 step 5; M1-10 plan call 12): <c>combatActive</c> is now false. It has no rule event (plan
/// call 14): a rule event needs a subject unit, and combat end has none.
/// </summary>
public sealed record CombatEnded(CombatEndReason Reason) : GameEvent;

/// <summary><paramref name="User"/> declared a use of <paramref name="AbilityId"/> and it passed validation (AB 14.3; M1-11 plan call 9).</summary>
public sealed record AbilityUsed(UnitId User, string AbilityId) : GameEvent;

/// <summary>
/// The use's cost was paid (AB 7.1): <paramref name="Charged"/> holds only the cost's own bits (an action, the bonus
/// action, the free interaction or movement), and <paramref name="Pools"/> the pool spend, if any, separate from the cost.
/// The record compares <paramref name="Pools"/> by reference; compare it with <c>SequenceEqual</c>.
/// </summary>
public sealed record AbilityCostPaid(UnitId User, string AbilityId, TurnBudget Charged, ImmutableArray<PoolSpend> Pools) : GameEvent;

/// <summary>
/// The units the use affects, locked in unit order (AB 14.3); an empty list is a legal use that resolves no target.
/// The record compares <paramref name="Targets"/> by reference; compare it with <c>SequenceEqual</c>.
/// </summary>
public sealed record TargetsLocked(UnitId User, ImmutableArray<UnitId> Targets) : GameEvent;

/// <summary><paramref name="Attacker"/>'s attack roll against <paramref name="Target"/> and its outcome.</summary>
public sealed record AttackRolled(UnitId Attacker, UnitId Target, AttackResult Result) : GameEvent;

/// <summary><paramref name="Target"/>'s save against an ability and its outcome.</summary>
public sealed record SaveRolled(UnitId Target, CheckResult Result) : GameEvent;

/// <summary>The damage dice rolled against <paramref name="Target"/>, before the ladder and the floors.</summary>
public sealed record DamageRolled(UnitId Target, DamageRoll Roll) : GameEvent;

/// <summary><paramref name="Reactor"/> took the reaction <paramref name="AbilityId"/> (AB 7.3).</summary>
public sealed record ReactionTaken(UnitId Reactor, string AbilityId) : GameEvent;

/// <summary>
/// The use of <paramref name="AbilityId"/> by <paramref name="User"/> has resolved every target (AB 14.3). It raises the
/// rule event <c>abilityResolved</c>, whose subject is the user. A use stopped by a run end has none.
/// </summary>
public sealed record AbilityResolved(UnitId User, string AbilityId) : GameEvent;

/// <summary>
/// A condition instance <paramref name="Seq"/> of <paramref name="ConditionId"/> was placed on <paramref name="Holder"/>
/// from <paramref name="Source"/> with <paramref name="Value"/> (M1-12a plan call 7). <paramref name="New"/> is false when
/// a second grant of a core definition (Steeled, Unyielding) re-stamped the existing instance instead (plan call 5).
/// </summary>
public sealed record ConditionApplied(long Seq, string ConditionId, UnitId Holder, EffectSource Source, int Value, bool New) : GameEvent;

/// <summary>
/// The condition instance <paramref name="Seq"/> of <paramref name="ConditionId"/> on <paramref name="Holder"/> ended for
/// <paramref name="Reason"/> (CO 8: every end on a living holder emits one; M1-12a plan call 7).
/// </summary>
public sealed record ConditionEnded(long Seq, string ConditionId, UnitId Holder, ConditionEndReason Reason) : GameEvent;

/// <summary>
/// <paramref name="Unit"/> lost its turn start to a hard condition (CO 5.7); <paramref name="ThisFight"/> is its count of
/// lost turn starts in this fight, this one included.
/// </summary>
public sealed record TurnStartLost(UnitId Unit, int ThisFight) : GameEvent;
