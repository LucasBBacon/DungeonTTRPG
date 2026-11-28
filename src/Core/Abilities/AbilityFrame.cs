using System.Collections.Immutable;
using Dungeon.Core.Stats;

namespace Dungeon.Core.AbilityModel;

/// <summary>
/// An ability use suspended on an "ask me" reaction (M1-11 plan call 12; AB 7.3, 14.3), held in
/// <see cref="GameState.Resolution"/> while a <see cref="DecisionKind.Reaction"/> decision is pending.
/// <see cref="User"/> used <see cref="AbilityId"/> on <see cref="Target"/>; the cost is paid and <see cref="Locked"/> holds
/// the targets in unit order. <see cref="Index"/> is the current target, <c>Locked[Index]</c>, whose reaction window
/// <see cref="Window"/> is open: its d20 is drawn (<see cref="Roll"/>) and <see cref="Setup"/> is the contest it meets.
/// <see cref="Candidate"/> is the ability id of the ward being asked; declining moves on to the next candidate after it
/// in ability-id order. <see cref="SaveDifficulty"/> is the save DC captured when the targets locked, null for an attack.
/// <see cref="Held"/> holds the events of the suspended command(s) that can fire triggers (M1-12a plan call 12; review
/// finding C1): nothing is dispatched while the use is suspended, and the command that completes it dispatches them
/// first, then its own. <c>Resolution</c> creates it empty and <c>frame with { Candidate = … }</c> keeps it; the engine
/// fills it (<c>Engine.Dispatchable</c>). The record compares <see cref="Locked"/> and <see cref="Held"/> by reference;
/// compare frames by their saves.
/// </summary>
public sealed record AbilityFrame(
    UnitId User,
    string AbilityId,
    AbilityTarget Target,
    ImmutableArray<UnitId> Locked,
    int Index,
    ReactionTrigger Window,
    ContestSetup Setup,
    D20Roll Roll,
    string Candidate,
    long? SaveDifficulty,
    ImmutableArray<GameEvent> Held);
