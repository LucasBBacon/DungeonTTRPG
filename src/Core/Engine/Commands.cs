namespace Dungeon.Core;

/// <summary>A request to change the game state. Applied by <see cref="Engine.Apply"/>.</summary>
public abstract record Command;

/// <summary>Move a unit one square in a direction.</summary>
public sealed record MoveCommand(UnitId Unit, Direction Dir) : Command;

/// <summary>Answer the pending decision by picking one of its options.</summary>
public sealed record AnswerDecisionCommand(DecisionId Decision, int OptionIndex) : Command;
