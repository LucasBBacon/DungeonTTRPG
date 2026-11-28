using System;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Conditions;
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
    /// unit's turn ends as in <see cref="Turns.EndTurn"/> (<see cref="TurnEnded"/>, then the turn-end step), skipped
    /// when the turn is vacated (its holder is dead and has no turn-end step, CO 5.4, or its turn has already ended,
    /// M1-12a plan call 10) and under the
    /// <see cref="CombatState.BeforeRound1"/> marker (no turn has started: "If the fight is won, no <c>TurnStarted</c> or
    /// <c>TurnEnded</c> follows", M1-10 rulings record 5, step 8); 2, <c>combatOnly</c> instances and windows whose
    /// source is gone end (<see cref="Clock.CombatEnd"/>; M1-12a plan call 15: reasons
    /// <see cref="ConditionEndReason.CombatEnd"/> and <see cref="ConditionEndReason.SourceGone"/>, and a Steeled an end
    /// grants ends at once, so nothing <c>combatOnly</c> survives the fight); 3, Downed companions get up; 4, XP and
    /// level-ups; 5, the combat state is cleared, every unit gets a fresh turn budget (out-of-combat time, AB 6.8), loses
    /// any opener mark (M1-11 plan call 7), the clock leaves its turn step (<see cref="ClockStep.None"/>), and
    /// <see cref="CombatEnded"/> (<see cref="CombatEndReason.Win"/>) is emitted. Steps 3 and 4 are empty seams.
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

        // 1. The current unit's turn ends as in Turns.EndTurn: TurnEnded, then the turn-end step (CO 5.4; not dispatched
        //    here: the settle loop dispatches this pass). A vacated turn's holder is dead and has no turn-end step, or its
        //    turn has already ended (Turns.EndTurn); before round 1 no turn has started.
        if (!combat.TurnVacated && !combat.BeforeRound1)
        {
            var (ended, afterEnd) = Turns.EndCurrentTurn(rules, next);
            events.AddRange(ended);
            next = afterEnd;
        }

        // 2. combatOnly instances end, with every window whose source is gone; a Steeled an end grants ends at once
        //    (M1-12a plan call 15; CO 5.5).
        var (ended2, afterOnly) = Clock.CombatEnd(rules, next);
        events.AddRange(ended2);
        next = afterOnly;

        // 3. On a win, Downed companions get up: M2 (no Downed in M1).

        // 4. XP and level-ups: M1-21. Each levelled unit goes through HitPoints.RaiseLevel, which adds Δ (DH 7.3).

        // 5. combatActive becomes false, and no combat budget, opener mark or turn step survives into out-of-combat time
        //    (AB 6.8; M1-12a plan call 1: out of combat between ticks the clock is in no step, so nothing applied now is
        //    own-turn).
        next = Turns.FreshBudgets(next with
        {
            Combat = null,
            Step = ClockStep.None,
            Units = next.Units.Select(u => u.Opener is null ? u : u with { Opener = null }).ToImmutableArray(),
        });
        events.Add(new CombatEnded(CombatEndReason.Win));

        return (events.ToImmutable(), next);
    }
}
