namespace Dungeon.Core;

/// <summary>A request to change the game state. Applied by <see cref="Engine.Apply(GameState, Command)"/>.</summary>
public abstract record Command;

/// <summary>
/// Move a unit one square in a direction. <see cref="Dir"/> must be one of the eight named
/// directions; any other value is a programming error and <see cref="Engine.Apply(GameState, Command)"/> throws
/// <see cref="System.ArgumentOutOfRangeException"/> instead of returning a rejection.
/// </summary>
public sealed record MoveCommand(UnitId Unit, Direction Dir) : Command;

/// <summary>Answer the pending decision by picking one of its options.</summary>
public sealed record AnswerDecisionCommand(DecisionId Decision, int OptionIndex) : Command;

/// <summary>
/// End <see cref="Unit"/>'s turn in combat (M1-10 plan call 9): refused out of combat, out of turn, and while the turn
/// is vacated (no unit holds it).
/// </summary>
public sealed record EndTurnCommand(UnitId Unit) : Command;

/// <summary>
/// <see cref="User"/> uses the ability <see cref="AbilityId"/> on <see cref="Target"/> (AB 14.3; M1-11 plan calls 3 to 5
/// and 10). In combat only the unit holding a live turn may use one; out of combat a use that affects no unit of another
/// faction takes one tick, as a step does (AB 6.8). The first failing check, in AB 14.3's order, is the refusal's
/// <see cref="CommandError"/> (<see cref="AbilityModel.AbilityUse.CheckTarget"/>).
/// </summary>
public sealed record UseAbilityCommand(UnitId User, string AbilityId, AbilityModel.AbilityTarget Target) : Command;
