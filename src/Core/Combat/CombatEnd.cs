using System;
using System.Collections.Immutable;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Combat;

/// <summary>Why a fight ended (DH 20.5). The designs name no reason but a win yet.</summary>
public enum CombatEndReason { Win }

/// <summary>The combat-end procedure (M1-10 plan call 12; DH 20.5).</summary>
public static class CombatEnd
{
    /// <summary>
    /// Ends a won fight in the DH 20.5 order. Callers run it once the command's queue has drained, and then
    /// <see cref="Turns.ResumeIfVacated"/> (plan call 11), so a fight won on an enemy's turn starts no further turn. It
    /// does nothing, returning <paramref name="s"/> itself with no events, out of combat, on an ended run ("If the hero
    /// reached 0, none of this runs"), or while any <see cref="Faction.Enemy"/> unit remains. Otherwise: 1, the current
    /// unit's turn ends as in <see cref="Turns.Advance"/> (<see cref="TurnEnded"/>, then the turn-end step), skipped
    /// when the turn is vacated (its holder is dead and has no turn-end step, CO 5.4); 2, <c>combatOnly</c> instances
    /// end; 3, Downed companions get up; 4, XP and level-ups; 5, the combat state is cleared, every unit gets a fresh
    /// turn budget (out-of-combat time, AB 6.8) and <see cref="CombatEnded"/> (<see cref="CombatEndReason.Win"/>) is
    /// emitted. Steps 2 to 4 are empty seams in M1-10.
    /// </summary>
    public static (ImmutableArray<GameEvent> Events, GameState Next) EndIfWon(RulesContext rules, GameState s)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var combat = s.Combat;
        if (combat is null || s.Ended is not null)
        {
            return (ImmutableArray<GameEvent>.Empty, s);
        }

        foreach (var unit in s.Units)
        {
            if (unit.Faction == Faction.Enemy)
            {
                return (ImmutableArray<GameEvent>.Empty, s);
            }
        }

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        var next = s;

        // 1. The current unit's turn ends as in Turns.Advance: TurnEnded, then the turn-end step (CO 5.4's contents:
        //    M1-12). A vacated turn's holder is dead and has no turn-end step.
        if (!combat.TurnVacated)
        {
            var (ended, afterEnd) = Turns.EndCurrentTurn(rules, next);
            events.AddRange(ended);
            next = afterEnd;
        }

        // 2. combatOnly instances end: M1-12.

        // 3. On a win, Downed companions get up: M2 (no Downed in M1).

        // 4. XP and level-ups: M1-21. Each levelled unit goes through HitPoints.RaiseLevel, which adds Δ (DH 7.3).

        // 5. combatActive becomes false, and no combat budget survives into out-of-combat time (AB 6.8).
        next = Turns.FreshBudgets(next with { Combat = null });
        events.Add(new CombatEnded(CombatEndReason.Win));

        return (events.ToImmutable(), next);
    }
}
