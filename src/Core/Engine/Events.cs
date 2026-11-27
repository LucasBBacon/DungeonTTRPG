using Dungeon.Core.Effects;

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
