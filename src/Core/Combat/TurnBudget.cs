namespace Dungeon.Core.Combat;

/// <summary>
/// A unit's turn budget (AB 14.2), stored as what has been used, never what is left (M1-10 plan call 5): movement
/// left is the folded speed minus <see cref="MovementUsed"/>, never below 0. <see cref="CampSpent"/> is the AB 6.4
/// "camp spend used" flag. The default value is a fresh turn.
/// </summary>
public readonly record struct TurnBudget(
    int MovementUsed, int ActionsUsed, bool BonusUsed, bool ReactionUsed, bool FreeUsed, bool CampSpent);

/// <summary>How much of one pool a unit has spent (AB 6.2: the state stores only spent counts).</summary>
public readonly record struct PoolSpend(string PoolId, int Spent);
