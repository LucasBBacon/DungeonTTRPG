namespace Dungeon.Core;

/// <summary>Something that happened while applying a command, in the order it happened.</summary>
public abstract record GameEvent;

public sealed record UnitMoved(UnitId Unit, GridPos From, GridPos To) : GameEvent;

/// <summary>The game clock advanced to <paramref name="Tick"/>.</summary>
public sealed record TickAdvanced(long Tick) : GameEvent;
