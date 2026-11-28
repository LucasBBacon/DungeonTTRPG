# M1-12a Condition Clock and Carried Fixes Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Conditions exist as records and as instances that count down on one clock: a global phase counter, the wearer clock, the own-turn rule, the CO 5.4 turn-start and turn-end steps in and out of combat, combat start and combat end, the lost-turn count and its Unyielding cap. The two M1-11 carry-overs are fixed: a suspended ability use holds its events until it completes, and trigger dispatch no longer misfires.

**Architecture:**
- **This is the first of two plans for roadmap step M1-12** (owner ruling 1). M1-12b, planned after this one merges, adds the CO 9 application pipeline, the relation kinds, the CO 7.2 guards (Steeled's immunity, composite lockout, depth gates, party composure, hard exclusivity) and the property tests.
- **Until then, conditions reach a unit only through an internal direct insert** (`Conditions.Insert`), which tests use for fixtures. Ability `effects` naming a condition keep today's M1-07 path until M1-12b.
- **Conditions are a new content kind** (`conditions/conditions.json`, `ConditionsKind`). Each record also produces a `condition`-kind `EffectDefinition`, so the M1-07 fold and trigger dispatch keep working off `EffectInstance.EffectId`.
- **Instances stay in `GameState.Effects`.** Each gains an optional `ConditionState` part (value, applied phase, turns counted and so on).
- **`GameState` gains the phase counter and the current clock step.**
- **The turn steps:** `Turns.StartTurn` and `EndCurrentTurn` run the CO 5.4 items. An out-of-combat tick runs every unit's turn-start step, then the command, then every unit's turn-end step. `BeginCombat` runs the CO 5.5 combat-start condition step, and `EndIfWon` ends `combatOnly` instances.

**Tech Stack:** C# 12 / .NET 8; the M1-04 content framework; the M1-07 effects and trigger dispatch; the M1-10 turn loop, `Harm.Deal` and combat end; the M1-11 ability pipeline, settle loop and "ask me" frame; xUnit.

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-12](../../roadmap/m1-vertical-slice.md#m1-12-conditions-core), the clock half, implementing:
- **CO 5.1–5.7, change C1 (the wearer clock, CO 24 row C1), CO 8 (the end events), CO 17.1–17.2 (the data and state M1-12a executes), CO 18.1 (test-only fixtures)** (`2026-11-16-conditions-design.md`, lines 126–386, 588–627, 1557–1626, 1711–1743, 3356).
- **AB 9.6** (`2026-11-16-ability-resource-model-design.md`, lines 834–861): the wearer clock for conditions on units.
- **DH 20.5 step 2** (`2026-11-17-death-healing-design.md`): `combatOnly` instances end at combat end.
- **The [M1-10 rulings record](../specs/2026-11-27-m1-10-rulings-design.md)**: section 5 step 2 and the section 6.3 M1-12 tests.
- **The M1-12 roadmap carry-overs:**
  - hold a suspended use's events (from the M1-11 final review, finding C1 — the "held events" finding, not change C1);
  - the opener preview after combat start.
- **Change C1** is the wearer clock. **Review finding C1** is the held-events finding. The plan names them apart.

## Owner rulings for this step (2026-11-28)

1. **M1-12 is two plans and two PRs on one roadmap step.** M1-12a is this plan (clock and carry-overs; save version 9, rules version 4). M1-12b covers the pipeline, relations, guards and property tests (rules version 5).
2. **A hard instance already held when Unyielding lands ends at the end of the turn in which Unyielding is granted, and Steeled follows** (a change to CO 5.7's "runs its course"). Built here, because the lost-turn cap is part of this plan's turn-start step. Task 6 records it as a CO change row.
3. **An ability's own hit, or the target's failed save, is the contest for a non-rider condition it carries.** No extra roll. Built in M1-12b; recorded here.
4. **A data trigger's `applyEffect` may apply a condition, entering the CO 9.2 pipeline at step 2.** A hard condition on a `combatStarted` trigger is refused at load; CO 9.2 gains a "Trigger action" row. The routing is M1-12b's. **The dispatch timing is built here:** `turnStarted` triggers fire inside the turn-start step as CO 5.4 item 7, before the lost-turn check (item 8).

## Global Constraints

- **CO 5.2:** "The core keeps one global phase counter, which advances by 1 at the start of every phase of every unit, in combat and out. Each instance stores the counter's value when it was applied (`appliedPhase`)." "A wearer turn counts toward an instance only if the instance's `appliedPhase` is earlier than that turn's Act phase."
  - Own-turn rule: "a hard condition applied in the own-turn window ... ends at the end of that turn, whatever its duration model, and Steeled follows."
  - "Out of combat it is the step phase of the tick (5.5)."
- **CO 5.1:** `turns:N`, `untilSourceTurnStart`, `untilSourceTurnEnd`, `saveEnds {save, cap, graceTurns?}`, `charges:N {on, fallback}`, `decay`, `trigger {event, cap}`, `sustained {max}`, `staged {stages, save, cap}`, `untilRest`, `untilCamp`.
  - `duration` may be a list: "whichever terminator comes first".
  - `rounds:N` "is not a condition duration".
  - Flags: `combatOnly` (ends at combat end), `sourceTied` (ends when the source dies or leaves).
- **CO 5.4:** each item runs over instances in `seq` order.
  - **Turn-start:** 1 budget reset; 2 cooldowns; 3 harmful ticks as one batch; 4 zero-HP check ("the rest of its turn-start step is skipped"); 5 beneficial ticks; 6 aura pulses; 7 start-of-turn triggers and checks; 8 lost-turn check.
  - **Turn-end:** 1 repeat saves; 2 decay drops; 3 countdowns and expiry (each end "fires ... the Steeled rule (7.2), immediately, before the next instance is processed"); 4 trigger-end checks; 5 terrain (M2).
  - "After the turn-end step, boss actions resolve. A round ends after the last initiative slot's turn-end step and the boss actions that follow it."
- **CO 5.5:**
  - "A tick runs every unit's turn-start step in unit order, then the step phase ..., then every unit's turn-end step in unit order. The step phase is every unit's Act phase for 5.2."
  - "Harmful ticks out of combat cannot take a unit below 1 hit point."
  - Combat start: an `outOfCombat` duration "switches to its in-combat duration, counted from then, if that is shorter"; `hardTurnsLostThisFight` resets. The step "runs before the surprise grant and before initiative, and draws no randomness". Its messages "are held and emitted after `CombatStarted` and before the first `TurnStarted`, listed by unit and then in the order the conditions were applied".
  - Combat end: "the current unit's turn-end step completes, then `combatOnly` instances end (reason `combatEnd`)."
- **CO 5.6:** "`untilSourceTurnStart` ends when the source's next turn-start step begins; `untilSourceTurnEnd` ends when the source's next turn-end step completes. A window opened during the source's own turn runs to its following turn." "If the source is dead, downed, gone or not in the initiative order, the window ends at the end of the current round." "Out of combat ... a window lasts until the next tick."
- **CO 5.7:**
  - At the end of the turn-start step, a unit loses its turn start if it holds a hard instance "whose source is hostile to it or is the environment". Never for Surprised, own-turn hard conditions, or self or ally sources.
  - Each lost turn start adds 1 to `hardTurnsLostThisFight`; at 2, Unyielding (`combatOnly`) is applied at once (owner ruling 2 for the held instance).
  - "The counter resets at combat start."
- **CO 7.2 (Steeled, the grant only):** gained when a hard instance or Surprised ends "for any reason except `combatStart`". It lasts "until the end of its next turn-end step that begins after it is gained". Its immunity is M1-12b's.
- **CO 8:** "Every end emits `ConditionEnded {reason}`." "Turns go on for everyone." "Suppression ... never stops countdowns."
- **CO 17.2:** "Implied flags, suppression, blocks and combined modifiers are queries, never stored."
- **CO 18.1:** M1 content uses no `saveEnds`, `charges`, `decay` or `staged`; "test-only fixture conditions, never loaded by the game" cover each duration model.
- **SV 4.3 / D-23:** out-of-combat rolls are refused in M1. Rolls in combat draw `combat`. V19t `ExplorationCombatIsolation` stays green.
- **Determinism:** the DUN001–DUN012 analyzers stay green: no floats, `System.Random` or clocks; no Dictionary/HashSet enumeration; sorts only with `[TotalComparer]`; `IntMath` for signed `/` and `%`.
- **Out of scope:**
  - **M1-12b:** the CO 9 pipeline and its entry points; `ConditionApplied`/`ConditionResisted` from real applications; relation kinds (`relations` refused at load until then); Steeled's immunity; hard exclusivity; depth gates; composite lockout and `markedHard`; party composure; Unyielding's downgrade and resistance; routing ability `effects` and trigger `applyEffect` into the pipeline; the three property tests; the CO 9.2 "Trigger action" row.
  - **M1-13:** the ten M1 conditions as content; the tick batch's 1/4-max-HP cap; the concentration event; Surprised's grant.
  - **M1-23:** cooldowns (turn-start item 2), `ai`, `controlRank`.
  - **M1-24:** boss resolve and pips; boss actions as content (the seam lands here).
  - **M2:** riders, terrain, auras and `rounds:N`, the out-of-combat repeat-save stream (D-23), Downed.

## Plan-level calls (mine, flagged for review)

1. **Phase mechanics.**
   - `GameState.Phase` (`long`, starts at 0) and `GameState.Step` (`ClockStep { None, TurnStart, Act, TurnEnd }`).
   - **In combat:** each turn advances `Phase` by 1 at its turn-start step, at its Act phase and at its turn-end step.
   - **Out of combat:** each tick advances `Phase` three times, shared by every unit (all turn-start steps, then the step phase, then all turn-end steps). This is the CO 5.2 header's "counting mechanics lead's call".
   - So a turn's Act phase is always the turn-start phase + 1. At the turn-end step, the Act phase is `Phase − 1` and the turn-start phase is `Phase − 2`.
   - Combat start, the opener and anything before round 1 advance nothing, and `Step` is `None` there. Anything applied then is off-turn, so it counts the wearer's first turn.
   - CO 5.2's counting test decides which turn ends count for every count: `turns` countdowns, `saveEnds` and `staged` rolls and caps, `trigger` caps, `sustained` maxima, fallbacks and `decay` drops.
2. **The instance part.** `EffectInstance` gains `ConditionState? Condition`:

   `ConditionState(int Value, long AppliedPhase, int TurnsCounted, ImmutableArray<DurationTerm> Terms, int? Charges, long? RecordedDifficulty, Faction? SourceFaction, long? SustainedBy, bool OwnTurn)`.
   - **`Terms`** is the active terminator list, resolved at insert from the first of: `ConditionInsert.Duration` (the CO 17.1 override), then the record's `OutOfCombat` when there is no fight and that list is not empty, then the record's `Duration`. Every clock item reads `Terms`, never the record or `s.Combat`.
   - **`TurnsCounted`** counts the wearer turn ends that passed the counting test. Every terminator's "remaining" is derived from it, standing in for CO 17.2's illustrative `remaining`.
   - **`Value`** holds the value, level or, for `staged`, the current stage (1 at insert).
   - **`SourceFaction`** is recorded at insert, so hostility (5.7) and "the source is dead" (5.6) still work after the source dies.
   - **`OwnTurn`** is set when a hard instance is inserted in its wearer's own-turn window.
   - A load checks this part for structure only: a non-negative phase and counts, sorted terms, and one instance with a `Condition` per (holder, id). A bare carrier from `effects.json` may still repeat. Where rules are present (`Conditions.Insert`, the clock), an `OwnTurn` instance whose definition is not hard, or a `Condition` whose id names no condition definition, throws `InvalidOperationException`.
   - `markedHard`, `binding` and the M2 fields are M1-12b's or later.
3. **Conditions are a content kind** (`ConditionsKind`, file `conditions/conditions.json`, `IdsAreContentIds`, not `ReadByGeneration`).
   - **No production file in M1-12a.** Registering the kind still changes the manifest and display hashes, so Task 3 regenerates `ContentHashes.g.cs` once. M1-13 adds the file.
   - Each `ConditionDefinition` contributes a synthesised `EffectDefinition(Kind: Condition, Id, Modifiers, Triggers)`. Ids must be unique across effects and conditions.
   - Bare `condition`-kind entries in `effects.json` stay legal as modifier carriers, which keeps the M1-07 to M1-11 tests unchanged.
   - **Until M1-12b routes them through the pipeline, conditions can't be applied by ability or trigger.** An ability `effects` entry, or an `applyEffect` in any trigger (including a condition record's own triggers), that names a condition definition is refused at `RulesContext` build, with the path and the message "conditions are applied through the pipeline (M1-12b)".
4. **The record fields M1-12a parses:**
   - `id`, `polarity` (`harmful | beneficial | token`), `tier` (`token | minor | major | hard`, harmful only), `tags`, `family`, `denies` (stored for M1-12b);
   - `duration` (a list), `outOfCombat` (a list), `intensity` (`kind`, `valueCap`, `levelCap`, `stages`, `noRefresh`);
   - `flags`:
     - `combatOnly`, `sourceTied`, `turnPartGroup`, `noProne`;
     - `combatStartEnds`: this names CO 8's unnamed "By flag" `combatStart` end (conditions-design.md:609);
   - `cures`, `cureImmune`;
   - `tick` (`{damageType}`; the amount is the instance's `Value`);
   - `modifiers` and `triggers` in the M1-07 shapes.

   **The `engineState` tag** marks an engine-granted state such as Surprised (CO 18.2). M1-13 authors Surprised as a hard condition with this tag. It grants Steeled when it ends (CO 7.2), and it never counts as a lost turn start (CO 5.7).

   **The terms:** `charges.on` and `trigger.event` must be rule-event ids the rules know. `staged` carries `then` (a condition id), stored for M1-12b. M1-12a builds only the clock paths of these terms: the `charges` fallback, and the `trigger` and `staged` caps. Consuming a charge, ending on a trigger event, and `staged`'s final application are M1-12b's.

   **Refused at load:**
   - `relations` (naming M1-12b);
   - `boss`, `ai`, `controlRank`, `display` and `extraSave` (naming M1-23, M1-24 and M1-25);
   - `rounds` (CO 5.1);
   - an unknown `charges.on` or `trigger.event`;
   - a `saveEnds` or `staged` terminator on a condition without `combatOnly`, because out-of-combat repeat saves are D-23's;
   - band violations against CO 5.3, checked per side.

   CO 6.5's "staged only on Calcifying" check waits for M1-13. CO 17.1's illustrative `"effects": [{"mod": ...}]` maps onto the M1-07 modifier vocabulary.
5. **Steeled and Unyielding are core definitions** with fixed ids (`cond.steeled`, `cond.unyielding`) that every `RulesContext` holds, whatever the content. M1-13 ships their display and tests, not their records.
   - **Steeled:** beneficial and `combatOnly`. It ends at the end of the first turn-end step of its wearer that begins after the grant. That is the first turn-end item 3 at which `Phase > AppliedPhase`; it is an engine window, not `turns:1`. Out of combat, the turn-end phase is shared, so a Steeled granted during an earlier unit's turn-end step in the same tick ends at the next tick. That is a reading, listed in Task 6.
   - **Unyielding:** `combatOnly`.
   - **A second grant** to a holder that already has Steeled or Unyielding re-stamps the existing instance's `AppliedPhase`. It emits `ConditionApplied(..., New: false)` and creates no second instance.
   - Their effects (immunity, downgrade) are M1-12b's.
6. **`Conditions.Insert(rules, s, string conditionId, UnitId holder, EffectSource source, ConditionInsert options) -> (ImmutableArray<GameEvent>, GameState)`** is internal and visible to tests.
   - `ConditionInsert(int? Value, long? RecordedDifficulty, ImmutableArray<DurationTerm>? Duration, long? SustainedBy)`. A `Duration` override is checked against the CO 5.3 band for the target's side, and `SustainedBy` must name a live instance.
   - It stamps `AppliedPhase = Phase`, resolves `Terms`, sets `OwnTurn` when it applies and `SourceFaction`, and emits `ConditionApplied(Seq, ConditionId, Holder, Source, Value, New: true)`.
   - It is the only way a condition instance appears in M1-12a; M1-12b puts its pipeline in front of it.
   - It asserts one instance per condition id per holder (6.1 handling is M1-12b's), except for the core re-stamp of call 5.
7. **Ends and events.** Every end, except a `CombatStart` end and a dead holder's own instances, goes through one internal `Conditions.End(rules, s, long seq, ConditionEndReason reason) -> (events, state)`:
   - it emits `ConditionEnded`;
   - then, at once and before the next instance is processed, it ends every instance whose `SustainedBy` is this seq, with reason `Ended`, in seq order;
   - then, if the ended instance was hard or carries `engineState`, it grants Steeled (call 5).

   Events:
   - `ConditionApplied(long Seq, string ConditionId, UnitId Holder, EffectSource Source, int Value, bool New)`.
   - `ConditionEnded(long Seq, string ConditionId, UnitId Holder, ConditionEndReason Reason)`, where `ConditionEndReason` is `Expired, Saved, CombatStart, CombatEnd, SourceGone, Consumed, Ended`. `Consumed` is emitted from M1-12b. The cure and relation reasons arrive with M1-12b and M1-13.
   - `TurnStartLost(UnitId Unit, int ThisFight)`.
   - A dead holder's own instances go silently with the unit: `UnitDied` covers them. This reads CO 8's "every end emits" as "every end on a living holder", and Task 6 records the reading.
8. **Source death.** `Harm.Deal` passes the rules to `Harm.Remove`, and `Remove` now returns events. In order:
   1. the dead unit's own instances go, with no event;
   2. after `UnitDied`, each `sourceTied` condition instance it was the source of, held by a living unit, ends through `Conditions.End` with `SourceGone`, in seq order (Steeled follows a hard one);
   3. every other condition instance keeps its source id and recorded `SourceFaction`, and a window on it ends at the next round end, at combat end, or at the end of the current tick.

   `Snapshot.Load`, `Effects` and `TriggerDispatch.SourceRank` accept a missing source unit for a condition instance; it ranks with the environment, last. Non-condition effects keep M1-10's behaviour.
9. **The turn steps** (`Turns.StartTurn`, `EndCurrentTurn`; `Clock.TurnStart`, `Clock.TurnEnd`).
   - **Who dispatches.** Only the turn-start entry points dispatch inside the step: `StartTurn`, `BeginRound1` and `ResumeIfVacated` take `ref int firings`. Every exit dispatches the events it returns exactly once, unless the run has ended. `Settle` does not re-dispatch their passes.
     - `EndCurrentTurn`, `EndTurn` and `EndIfWon` do not dispatch: `Engine.Apply` dispatches `ApplyEndTurn`'s result, and `Settle` dispatches an `EndIfWon` pass.
     - `Turns.StartCombat`, `Turns.Advance` and the two-argument overloads use a fresh counter.
   - **The turn-start step:**
     - **`TurnStarted` comes first, then the step's events.** It mirrors `TurnEnded`, which comes before the turn-end step. A unit killed by its own tick has already emitted `TurnStarted`.
     - **Item 0:** every `untilSourceTurnStart` window whose source is this unit ends (`Expired`) when its `AppliedPhase` is below the current phase. Out of combat, that keeps a window opened earlier in the same shared turn-start phase.
     - **Item 3:** the uncapped tick batch. Each harmful instance with a `tick`, in seq order, deals `Value` of its damage type through `Harm.Deal`. Before and after each `Harm.Deal`, the batch stops if the run ended, the holder was removed, or the instance is gone. M1-13 adds the 1/4-max-HP cap and Bleeding, and must keep this early stop.
     - **Item 4:** after the batch, if the holder was removed or the run ended, items 5–8 are skipped. The events so far are still dispatched once before the step returns, unless the run ended.
     - **Item 7:** the step's events so far, `TurnStarted` included, are dispatched inside the step on the command's counter (owner ruling 4). Afterwards, if the run ended, the holder was removed, or it no longer holds the turn, item 8 is skipped.
     - **Item 8:** the lost-turn check.
       - **What costs a turn start:** a hard instance (not `engineState`, not `OwnTurn`) whose `SourceFaction` is hostile to the holder, or is null (the environment).
       - **What follows:** `TurnStartLost`, then `HardTurnsLostThisFight` + 1. At 2, Unyielding is applied at once (call 5).
       - **Owner ruling 2, derived with no new state:** at this unit's turn-end item 3, if it holds Unyielding with `AppliedPhase` equal to this turn's turn-start phase, every other hard instance it holds ends with `Expired`, in seq order, and Steeled follows. A hard instance inserted after Unyielding landed is untouched.
       - The events item 8 emits are dispatched before the step returns.
     - **Items 2, 5 and 6** are empty seams, named in comments.
   - **The turn-end step:**
     - item 1, repeat saves for `saveEnds` and `staged`:
       - a roll happens only at a turn end that passes the counting test: one d20 on `combat` against `RecordedDifficulty` with the wearer's save;
       - `graceTurns` and the cap are measured in `TurnsCounted`;
       - a success ends the instance with `Saved`; a failed `staged` save raises `Value` by 1;
       - passing `Stages` or the cap ends it with `Expired`;
     - item 2, decay: counted turn ends only;
     - item 3, countdowns and expiry, including the owner-ruling-2 ends, Steeled's own end, the own-turn rule (an `OwnTurn` instance ends at its wearer's turn end) and `sustained` maxima;
     - item 4, `trigger` caps and the `charges` fallback;
     - then every `untilSourceTurnEnd` window whose source is this unit ends, unless its `AppliedPhase` is at or after this turn's turn-start phase. A window opened during the source's own turn runs to its following turn (CO 5.6).
   - **The boss-action seam** is the gap between `EndTurn` and `ResumeIfVacated`; M1-12a adds no hook there, and M1-24 fills it.
   - **`Turns.RoundEnd`** runs once per round wrap, in `ResumeIfVacated`. It ends, with `SourceGone`, each window whose source is gone.
10. **The `EndTurn` split** (dispatch hazard, roadmap carry-over).
    - `Turns.EndTurn(rules, s)` runs `EndCurrentTurn` (`TurnEnded`, then the turn-end step), then moves `Current` + 1 with `TurnVacated` set. It does not wrap; `ResumeIfVacated` wraps and runs `RoundEnd`.
    - `ApplyEndTurn` returns that; `Engine.Apply` dispatches it, then `Settle` runs `EndIfWon`, then `ResumeIfVacated`.
    - A `turnEnded` trigger identifies the ending unit through `TurnEnded.Unit`, and that unit is still in the state. During that dispatch `TurnVacated` is set, and `Combat.CurrentUnit` must not be read.
    - A win in a `turnEnded` trigger stops the next `TurnStarted`.
    - `TurnVacated`'s documented meaning widens to "no unit holds the turn: its holder died, or its turn ended and the next has not started".
    - `Turns.Advance` stays as `EndTurn` + `ResumeIfVacated` for direct callers.
11. **Stale firings.** Before each firing, `TriggerDispatch` looks its instance up by seq in the current state.
    - If the instance is gone, the firing is skipped: no count, no event. Otherwise it fires with the current instance.
    - An `applyEffect` whose recipient is gone is skipped.
    - An instance created by a firing does not join the due list of the event being processed, which is computed once per queued event and rule event, as today. It does answer later events in the queue, its own `EffectApplied` included.
12. **Held events of a suspended use** (review finding C1).
    - **What is held:** while a use is suspended, `AbilityFrame.Held` holds the command's events that raise at least one rule event under the rules. Those are the only events that can fire triggers. Nothing is dispatched in the suspending command.
    - **The completing command** dispatches `held ++ its own events` in one `TriggerDispatch.Run`, and reports only its own events.
    - **The opener's combat-start phase** is never held: it dispatches first, as today.
    - **Persistence:** `Held` is saved, digested and validated (structure only). The held event types are a closed set; any other type throws on save:
      - Task 1: the payloads of `RuleEvents.Core` that can occur mid-use;
      - Task 3 adds the condition events.
13. **The out-of-combat tick** (`Engine`). For a non-opener move or use out of combat, `Engine.Apply` drives the tick:
    1. validate on the stored state and refuse before anything else, so a refused command runs no tick step;
    2. open `firings = 0`, then run every unit's turn-start step in id order (item 1 is `TickResets`), each dispatching its own events. No `TurnStarted` is emitted, so data `turnStarted` triggers do not fire on a tick, and item 8 (a per-fight count) is skipped;
    3. resolve the command at the step phase. `AbilityUse.View` validates a non-opener on the state after the turn-start steps. If that view refuses the command, it is refused after all and the original state is returned;
    4. dispatch the command's events on the same counter;
    5. run every unit's turn-end step in id order and dispatch its events. Windows whose source is gone end now, with `SourceGone`;
    6. `Tick + 1` and `TickAdvanced`;
    7. a step onto a shrine raises its `DecisionRequested` after `TickAdvanced`, which keeps the shrine goldens;
    8. `Settle`.

    A command that starts a fight skips steps 5–6. The tick batch is floored at 1 HP out of combat for every unit: `Damage.Resolve`, `Damage.Apply` and `Harm.Deal` gain an optional `bool tick`, and with no fight, `tick` floors a monster the way the party is floored (`ClampReason.OutOfCombat`, with `DamageClamped`).
14. **The combat-start condition step** (`Conditions.CombatStart`, M1-10 record 5, step 2).
    - It draws nothing, and runs between the checks and `FightReady`.
    - **The duration switch:** an instance running on its record's `OutOfCombat` terms switches to the record's in-combat `Duration` when that list's effective end, counted from now, comes sooner than what it has left. Effective end follows CO 6.3: turns for `turns`; the cap for `saveEnds`, `trigger` and `sustained`; infinity for `untilRest` and `untilCamp`. The switch replaces `Terms` and resets `TurnsCounted` to 0, with no event. An instance that does not switch keeps its terms and count.
    - Instances flagged `combatStartEnds` end with reason `CombatStart`. They bypass `Conditions.End`, so no Steeled follows.
    - `HardTurnsLostThisFight` resets.
    - Its messages are held and sorted by `(holder id, instance seq, emission index)` with a `[TotalComparer]`, then emitted after `CombatStarted`.
    - **The opener view becomes `Turns.CombatStartView`** (step 2, step 3 when M1-13 adds it, then step 4), used by `AbilityUse.View`, `CheckUsable`, `Preview` and `BeginCombat`. The remaining gap is `combatStarted` triggers; the preview doc says so.
15. **Combat end.** `EndIfWon` step 2 collects the `combatOnly` instances (holders in id order, each holder's in seq order), plus every window whose source is gone. For each instance X:
    - X ends through `Conditions.End`, with `CombatEnd` (a gone-source window: `SourceGone`).
    - If X was hard, the Steeled it grants is itself `combatOnly`: its `ConditionApplied` is followed at once by its own `ConditionEnded(CombatEnd)`, before the next instance.

    The new Steeled never joins the collected list. Nothing `combatOnly` survives the fight.
16. **Versions.** Save version 9 and rules version 4 bump once, in Task 1, where v9 holds `Held`. Task 3 extends the same v9 format, with no second bump, adding `Phase`, `Step`, `ConditionState`, `HardTurnsLostThisFight` and the condition events to the held set.

## Review Focus

1. **The counting test at each boundary:** a `turns:1` fixture inserted in the wearer's turn-start step ends at that turn's end. Inserted in its Act phase or turn-end step, it ends at the next turn's end. Inserted in another unit's turn, it ends at the wearer's next turn end (Task 4).
2. **The own-turn rule:** a hard fixture inserted in the wearer's Act phase ends at that turn's end, whatever its duration, Steeled follows, and no turn start is counted as lost. Off-turn, it costs the wearer's next turn start (Task 4).
3. **A death mid-step:** a unit killed by its own turn-start tick, or by a `turnStarted` trigger, skips the rest of its step, and its `UnitDied` is still dispatched once. The settle loop starts the next unit, and the hero's death ends the run with nothing after `RunEnded` (Task 4).
4. **"Never" equals "ask me then decline":** with a `unitDied` trigger that changes a later target's resistance, both runs end in the same units, effects and pools (Task 1).
5. **Out of combat:** a fixture tick never takes any unit below 1 HP, a window lasts exactly one tick, and a refused command runs no tick step (Task 5).

---

### Task 1: Held events of a suspended use; save version 9, rules version 4

**Files:**
- Modify: `src/Core/Abilities/AbilityFrame.cs` (`Held`), `src/Core/Engine/Engine.cs` (`Apply`, `ApplyOpener`), `src/Core/State/GameState.cs` (`CheckResolution`), `src/Core/Persistence/Snapshot.cs` (v9; held-event DTOs), `src/Core/State/StateDigest.cs`, `src/Core/Run/Versions.cs`, `src/Core/Abilities/Resolution.cs` (doc)
- Modify tests: the version literals (`"version":8` → 9, "future" → 10; `RunConfigTests` rules 4)
- Test: `tests/Core.Tests/HeldEventsTests.cs`

**Interfaces:**
- `AbilityFrame` gains `ImmutableArray<GameEvent> Held` (last; non-default). `Resolution.Suspend` creates it empty, and `frame with { Candidate = … }` keeps it.
- `internal static ImmutableArray<GameEvent> Engine.Dispatchable(RulesContext, ImmutableArray<GameEvent>)` returns the events for which `rules.EventsRaisedBy(e)` is not empty.

- [ ] **Step 1: Write the failing tests** (fixtures in `AbilityFixtures`: foe 2 uses a two-target strike on [A, B]; A dies; B holds an ask-me ward; B holds an effect with a `unitDied` (scope any) trigger that `applyEffect`s a `resistant.slashing` flag effect to its holder):
  - `Never_equals_ask_me_then_decline` (Review Focus 4): units, effects and pools are equal. Also `Always_equals_ask_me_then_take`.
  - `Nothing_dispatches_while_suspended`: the suspending command's events include `UnitDied(A)` but no `HookFired` or trigger `EffectApplied`, and `Resolution.Held` is `[UnitDied(A)]`.
  - `The_completing_command_dispatches_held_then_its_own`: the events are `[DecisionResolved, …, AbilityResolved, <unitDied trigger output>, …]`, then the settle events.
  - `A_re_ask_keeps_the_held_events`.
  - `A_suspended_opener_dispatches_combat_start_at_once_and_holds_the_rest`.
  - `A_held_last_foe_death_ends_the_fight_only_after_the_use`.
  - `Held_events_round_trip_and_reach_the_digest`, `A_v8_save_is_refused`, `An_unsupported_held_event_is_a_format_error`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter HeldEventsTests`. Expected: build fails.
- [ ] **Step 3: Implement** plan call 12.
  - `Snapshot.CurrentVersion` = 9, with the doc line "v9 (M1-12a) adds held events, the phase clock and condition state".
  - `RulesVersion.Current` = 4, with the doc line "suspended uses hold their events; the condition clock runs in the turn steps and ticks".
  - Update `Resolution.cs`'s class doc (the early-dispatch exception is gone), plus the `Engine.Apply` and `ApplyOpener` docs.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12a: a suspended use holds its events until it completes; save version 9, rules version 4`.

### Task 2: Dispatch hazards — stale firings and the EndTurn split

**Files:**
- Modify: `src/Core/Effects/TriggerDispatch.cs`, `src/Core/Combat/Turns.cs` (`EndTurn`, `Advance`), `src/Core/Combat/CombatState.cs` (doc), `src/Core/Engine/Engine.cs` (`ApplyEndTurn`)
- Test: `tests/Core.Tests/DispatchHazardTests.cs`

**Interfaces:**
- `Turns.EndTurn(RulesContext, GameState) -> (ImmutableArray<GameEvent>, GameState)` (plan call 10).

- [ ] **Step 1: Write the failing tests:**
  - `A_firing_whose_instance_was_removed_is_skipped`: two instances answer one event; the first's hook removes the second. The second does not fire, and nothing throws.
  - `A_firing_on_a_killed_holder_is_skipped`.
  - `An_applyEffect_to_a_removed_subject_is_skipped`.
  - `A_firing_uses_the_current_instance`.
  - `turnEnded_triggers_see_the_ending_unit`: the hook records the `TurnEnded.Unit` it answers and asserts that `Combat.TurnVacated` is set and the unit is still present. Covered for a middle slot and for the last slot in the order.
  - `A_turnEnded_trigger_fires_once`: one `HookFired` on an end-turn command.
  - `A_new_instance_answers_later_queued_events_but_not_the_current_one`: a trigger on event X applies effect E, and E has triggers on X and on `effectApplied`. E's `effectApplied` trigger fires; its X trigger does not fire for the X being processed.
  - `A_win_in_a_turnEnded_trigger_starts_no_next_turn`: the events are `[TurnEnded, …, CombatEnded]`.
  - `End_turn_events_are_unchanged_without_triggers`: `[TurnEnded, TurnStarted]`, and the round wraps exactly once.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter DispatchHazardTests`. Expected: fail.
- [ ] **Step 3: Implement** plan calls 10 and 11.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass, and the goldens are unchanged.
- [ ] **Step 5: Commit** `M1-12a: trigger dispatch skips stale firings; ending a turn settles before the next starts`.

### Task 3: Condition records, instance state and the clock fields

**Files:**
- Create: `src/Core/Conditions/ConditionDefinition.cs`, `src/Core/Conditions/ConditionsKind.cs`, `src/Core/Conditions/ConditionState.cs`, `src/Core/Conditions/Conditions.cs` (`Insert`, the core definitions)
- Modify: `src/Core/Effects/EffectInstance.cs` (`Condition`), `src/Core/Effects/RulesContext.cs` (`Conditions`, `Condition(id)`, synthesised effect definitions, the core definitions), `src/Core/Content/ContentKind.cs`, `src/Core/State/GameState.cs` (`Phase`, `Step`, `Unit.HardTurnsLostThisFight`; validation), `src/Core/Combat/Harm.cs` (source death, plan call 8), `src/Core/Effects/Effects.cs`, `src/Core/Effects/TriggerDispatch.cs` (`SourceRank`), `Snapshot.cs`, `StateDigest.cs`, `src/Core/Engine/Events.cs`
- Test: `tests/Core.Tests/ConditionRecordTests.cs`, `tests/Core.Tests/ConditionStateTests.cs`, `tests/Core.Tests/ConditionFixtures.cs` (the shared test-only fixture set)

**Interfaces (namespace `Dungeon.Core.Conditions`):**
- `enum ConditionPolarity { Harmful, Beneficial, Token }`; `enum ConditionTier { Token, Minor, Major, Hard }`.
- `abstract record DurationTerm`, with `Turns(int N)`, `UntilSourceTurnStart`, `UntilSourceTurnEnd`, `SaveEnds(Save Save, int Cap, int GraceTurns)`, `Charges(int N, string On, DurationTerm Fallback)`, `Decay`, `TriggerEnd(string Event, int? Cap)`, `Sustained(int Max)`, `Staged(int Stages, Save Save, int Cap)`, `UntilRest`, `UntilCamp`.
- `sealed record ConditionDefinition(string File, string Id, ConditionPolarity Polarity, ConditionTier? Tier, ImmutableArray<string> Tags, string? Family, Denies Denies, ImmutableArray<DurationTerm> Duration, ImmutableArray<DurationTerm> OutOfCombat, Intensity Intensity, ConditionFlags Flags, ImmutableArray<string> Cures, ImmutableArray<string> CureImmune, TickSpec? Tick, ImmutableArray<Modifier> Modifiers, ImmutableArray<Trigger> Triggers) : ContentRecord`.
- `ConditionState` per plan call 2; `GameState.Phase`, `GameState.Step` per plan call 1.
- The events of plan call 7.
- `Conditions.Insert` per plan call 6; `Conditions.SteeledId = "cond.steeled"`, `Conditions.UnyieldingId = "cond.unyielding"`.

- [ ] **Step 1: Write the failing tests:**
  - **The record loads:**
    - every duration term, combined lists, `outOfCombat`, every intensity kind and every flag (`ConditionRecordTests`);
    - each refusal of plan call 4 (with its path), and a band violation per side;
    - an id that clashes with an effect is refused;
    - the synthesised effect definition folds (a condition's modifier shows in `Effects.Number`);
    - the canonical form is stable.
  - **The core definitions** `cond.steeled` and `cond.unyielding` exist with no content.
  - **`ConditionFixtures`:** one test-only definition per duration model and intensity kind (CO 18.1). It covers:
    - hard, major, minor and token tiers;
    - both windows and a `sourceTied`;
    - `saveEnds` (+ `graceTurns`, `combatOnly`);
    - `charges` with a fallback, `decay`, `trigger` with a cap, `sustained` with a test sustainer instance, `staged` (`combatOnly`);
    - a `tick`, a `combatStartEnds`, an `outOfCombat` term and a combined list.
  - **Refusals at `RulesContext` build** (call 3): an ability `effects` entry naming a condition, a trigger `applyEffect` naming a condition, and a condition record's own trigger naming one. Also an unknown `charges.on` or `trigger.event`.
  - **State (`ConditionStateTests`):**
    - `Insert` stamps the phase, the source faction, `Terms` and `OwnTurn`;
    - a `Duration` override becomes `Terms`, and an override outside the band is refused;
    - with no fight, `Terms` is the record's `OutOfCombat` list;
    - `SustainedBy` must name a live instance;
    - insert emits `ConditionApplied(New: true)`, and a second Steeled grant re-stamps with `New: false`;
    - `Phase`, `Step`, every instance field and `HardTurnsLostThisFight` round-trip and reach the digest;
    - `A_held_condition_event_round_trips`;
    - bad values are format errors: a negative phase or count, two instances with a `Condition` of one id on one holder;
    - an `OwnTurn` instance that is not hard throws `InvalidOperationException` when the clock reads it.
  - **Source death:**
    - a hard `sourceTied` instance on a living holder: the events are `[DamageDealt, UnitDied, ConditionEnded(SourceGone), ConditionApplied(cond.steeled)]`;
    - a `turns:3` instance keeps running;
    - a dead holder's instances go with no event;
    - a save with a gone source loads.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "ConditionRecordTests|ConditionStateTests"`. Expected: build fails.
- [ ] **Step 3: Implement** plan calls 2–8, plus the held-set part of call 12, copying `EffectsKind`'s style.
  - No content file is added.
  - Run `dotnet run --project src/Sim -- content-hashes` once after registering the kind: the manifest and display hashes change. Commit `ContentHashes.g.cs`, after which `ContentHashTests` passes.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12a: condition records, instance state and the phase clock fields`.

### Task 4: The in-combat clock

**Files:**
- Create: `src/Core/Conditions/Clock.cs` (the counting test, the turn-start and turn-end items, windows, `RoundEnd`)
- Modify: `src/Core/Combat/Turns.cs` (`StartTurn`, `BeginRound1` and `ResumeIfVacated` gain `ref int firings` and return events already dispatched; `EndCurrentTurn` and `EndTurn` do not dispatch; `RoundEnd`), `src/Core/Engine/Engine.cs` (`Settle` dispatches an `EndIfWon` pass but not a `BeginRound1` or `ResumeIfVacated` pass; `StartCombat` keeps a local counter), `src/Core/Conditions/Conditions.cs` (`End`)
- Test: `tests/Core.Tests/ClockTests.cs`

**Interfaces:**
- `internal static bool Clock.Counts(ConditionState c, long actPhase)` returns `c.AppliedPhase < actPhase`.
- `internal static (ImmutableArray<GameEvent>, GameState) Clock.TurnStart(RulesContext, GameState, UnitId, ref int firings)` and `Clock.TurnEnd(RulesContext, GameState, UnitId)`.
- `Turns.StartTurn(RulesContext, GameState, ref int firings)`. The two-argument overload uses a fresh counter.

- [ ] **Step 1: Write the failing tests:**
  - **Review Focus 1:** `Turns_one_counts_from_the_right_turn`, a theory over the insert point.
  - **Review Focus 2:**
    - `An_own_turn_hard_condition_ends_that_turn_and_Steeled_follows`: it costs no lost turn start;
    - `An_off_turn_hard_condition_costs_the_next_turn_start`: `TurnStartLost(unit, 1)`.
  - **A reaction during the wearer's Act phase is own-turn.**
  - `An_insert_in_the_boss_action_gap_costs_the_heros_next_turn`: `Turns.EndTurn` on the hero's turn, then `Conditions.Insert` of a hostile hard fixture on the hero while `TurnVacated` is set, then `ResumeIfVacated` (or the settle loop). `OwnTurn` is false, and the hero's next turn start emits `TurnStartLost(hero, 1)`.
  - **Source died:** `turns` runs on; `sourceTied` ends at once; a window with a dead source ends at round end.
  - `A_turn_start_trigger_application_counts_that_turn`: a `turnStarted` trigger hook inserts a hard fixture. It counts this turn and costs this turn start (owner ruling 4, CO 20.2 "an aura's turn-start reapplication").
  - `A_turn_end_insert_does_not_count_that_turn` (CO 20.2, staged/terrain analogue).
  - **Windows**, a theory over the open point (the source's turn-start step through a `turnStarted` trigger, its Act phase, its turn-end step):
    - an `untilSourceTurnEnd` window is still present after this turn's turn-end step, and ends `Expired` at the source's next turn-end step;
    - an `untilSourceTurnStart` window ends at item 0 of the source's next turn;
    - control: an `untilSourceTurnEnd` window opened on another unit's turn ends at the source's next turn-end step.
  - **`saveEnds`:**
    - a repeat save at turn end against the recorded difficulty, one d20 on `combat`;
    - `A_saveEnds_inserted_in_the_wearers_Act_phase_rolls_nothing_that_turn_end`: the `combat` position is unchanged, and the next turn end draws exactly one d20;
    - `graceTurns` delays the first save;
    - the cap ends it.
  - **`decay`** drops by 1 per counted turn and ends at 0.
  - **`charges` ends by its fallback.** **`trigger` ends at its cap.**
  - **`sustained`:** removing the sustainer emits `ConditionEnded(sustainer)` and then `ConditionEnded(sustained, Ended)` (with Steeled after a hard one). It also ends at `max`.
  - **`staged`:** after each failed save, `Value` is asserted. A success ends it with `Saved`; passing the cap ends it with `Expired`.
  - **Combined terms** end on the first terminator.
  - **Steeled** lasts to the first turn-end step after it is gained, including a gain in the wearer's own Act phase.
  - **The lost-turn cap (owner ruling 2):** an enemy-facing unit U.
    1. Insert Y (hard, `turns:1`) before U's turn 1. Turn 1 is lost (count 1); Y expires and Steeled follows.
    2. After turn 1, insert X (hard, `saveEnds` cap 2, a difficulty it cannot beat).
    3. At turn 2's start: `TurnStartLost(U, 2)`, then `ConditionApplied(cond.unyielding)` within the step.
    4. At turn 2's end: `ConditionEnded(X, Expired)`, then Steeled, although X's cap would have run on. X is absent at turn 3.

    A hard instance inserted after Unyielding is not ended early. An `engineState` fixture, and self-sourced and ally-sourced hard instances, are never counted.
  - **Review Focus 3.** Each holder has two tick instances, and the first is lethal.
    - `A_unit_killed_by_its_own_tick_skips_the_rest_of_its_step`: one `DamageDealt` from the batch; `TurnStarted` before `UnitDied`; no event from items 5–8. Another unit's `unitDied` trigger fires exactly once, and the next unit's `TurnStarted` follows.
    - `The_hero_killed_by_a_tick_ends_the_run`: `RunEnded` is last.
    - `A_turnStarted_trigger_that_kills_the_holder_skips_the_lost_turn_check`: no `TurnStartLost`, no Unyielding, and nothing throws. The hero variant ends with `RunEnded`.
  - **Every end emits one `ConditionEnded`**, in seq order within an item.
  - **`turnStarted` triggers** fire inside the step, before the lost-turn check.
  - **The existing turn and opener goldens** are unchanged when no condition is present.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter ClockTests`. Expected: fail.
- [ ] **Step 3: Implement** plan calls 1, 5, 9 and owner rulings 2 and 4. `StartTurn`'s doc comment is rewritten for the new order.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12a: the wearer clock, the turn-start and turn-end steps, the lost-turn cap`.

### Task 5: Out-of-combat ticks, combat start and combat end

**Files:**
- Modify: `src/Core/Engine/Engine.cs` (`ApplyMove`, `ApplyUseAbility`; the tick of plan call 13), `src/Core/Abilities/AbilityUse.cs` (`View`), `src/Core/Combat/Turns.cs` (`BeginCombat`, `CombatStartView`), `src/Core/Combat/CombatEnd.cs` (step 2), `src/Core/Abilities/Preview.cs` (doc), `src/Core/Conditions/Clock.cs`, `src/Core/Damage/DamageFunction.cs` (the tick floor for monsters, plan call 13)
- Test: `tests/Core.Tests/TickClockTests.cs`, `tests/Core.Tests/CombatStartConditionTests.cs`

**Interfaces:**
- `internal static (ImmutableArray<GameEvent> Held, GameState Next) Conditions.CombatStart(RulesContext, GameState)`.
- `internal static GameState Turns.CombatStartView(RulesContext, GameState)`.

- [ ] **Step 1: Write the failing tests:**
  - **Review Focus 5:**
    - a tick runs every unit's turn-start step, then the command, then every turn-end step, then `TickAdvanced`;
    - a fixture tick floors any unit, monsters included, at 1 HP out of combat. A monster's event list is `[DamageDealt, DamageClamped(OutOfCombat)]`;
    - out of combat, a hostile hard instance held across two ticks causes no `TurnStartLost`, no Unyielding and no count change;
    - `A_shrine_step_raises_its_decision_after_the_tick`: `[UnitMoved, ConditionEnded…, TickAdvanced, DecisionRequested]`. The shrine goldens in `DecisionTests` and `TriggerTests` are unchanged;
    - a window whose source died ends `SourceGone` at the end of the tick;
    - a window lasts one tick;
    - a refused move or use runs no tick step: same instance, no events;
    - the counting test out of combat (the step phase is the Act phase).
  - **The M1-10 record's 6.3 M1-12 tests:**
    - `The_combat_start_step_runs_before_initiative`: a `combatStartEnds` fixture that changes the initiative number is gone before the roll;
    - `Its_ConditionEnded_messages_follow_CombatStarted`: insert a `combatStartEnds` fixture on u3 first (seq a), then two on u1 (seq b < c). The events are `[CombatStarted, ConditionEnded(u1, b), ConditionEnded(u1, c), ConditionEnded(u3, a), TurnStarted]`; each event's `Seq` and `Holder` is asserted;
    - `Out_of_combat_durations_switch_when_shorter`: one switches (new `Terms`, `TurnsCounted` 0) and one keeps its terms and count;
    - `Per_fight_counters_reset`;
    - `The_number_of_initiative_dice_is_unchanged`.
  - **Opener path:**
    - the same messages sit between `CombatStarted` and `AbilityUsed`;
    - `The_opener_preview_reads_the_combat_start_view`: a `combatStartEnds` fixture giving the striker disadvantage → the preview `Chance` equals resolution's.
  - **Combat end** (u1 holds hard H at seq 1 and soft S at seq 2; u2 holds soft T). The events are `[…turn-end step…, ConditionEnded(H, u1, CombatEnd), ConditionApplied(cond.steeled, u1), ConditionEnded(steeled, u1, CombatEnd), ConditionEnded(S, u1, CombatEnd), ConditionEnded(T, u2, CombatEnd), CombatEnded]`. A window whose source died ends `SourceGone` in the same batch, and nothing `combatOnly` survives the fight.
  - **No Steeled on a `combatStart` end.**
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "TickClockTests|CombatStartConditionTests"`. Expected: fail.
- [ ] **Step 3: Implement** plan calls 13–15.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln` and `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`. Expected: all pass, 0 warnings.
- [ ] **Step 5: Commit** `M1-12a: out-of-combat ticks, the combat-start condition step and combat-end ends`.

### Task 6: Docs, roadmap and the CO change row

**Files:**
- Modify: `docs/effects-vocabulary.md`, `docs/roadmap/m1-vertical-slice.md`, `docs/roadmap/README.md`, `README.md` (a `Conditions/` line), `docs/superpowers/specs/2026-11-16-conditions-design.md` (CO 24: a change row for owner ruling 2, marked "agreed 2026-11-28 (owner)")

- [ ] **Step 1: The effects vocabulary.**
  - Section 8: "Durations and clocks" arrived in M1-12a. "Condition application pipeline, relations and exclusivity" is M1-12b.
  - Section 9: an M1-12a row (condition records, the clock, `ConditionApplied`/`ConditionEnded`/`TurnStartLost`, held events).
- [ ] **Step 2: The roadmap.**
  - **M1-12:** add "Planned as two PRs: M1-12a (clock and carry-overs) and M1-12b (pipeline, relations, guards, property tests) (owner ruling 2026-11-28)". Mark the held-events and opener-preview bullets as done in M1-12a.
  - **M1-13's Delivers** gain: "the tick batch's 1/4-max-HP cap, the concentration event and Bleeding on M1-12a's uncapped batch; Steeled and Unyielding are core definitions (M1-12a), so M1-13 ships their display and tests, not their records". The M1-14 held-events fallback bullet is marked done.
  - **README notes for owners:**
    - owner rulings 2–4;
    - CO 5.4 item 7 is read as including data `turnStarted` triggers (owner ruling 4);
    - out of combat, no `TurnStarted` is emitted and the lost-turn check is skipped;
    - the 3-per-tick phase reading (plan call 1), and the out-of-combat Steeled extra-tick reading (call 5);
    - CO 8's "every end emits" excludes a dead holder's own instances (call 7);
    - the new data terms: the `combatStartEnds` flag (CO 8's "by flag") and the `engineState` tag (M1-13's Surprised and Asleep must carry them);
    - the monster out-of-combat tick floor reuses `ClampReason.OutOfCombat`.
  - **The CO 24 change row** records owner ruling 2.
- [ ] **Step 3: Run:**
  - `dotnet test Dungeon.sln`;
  - `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`;
  - `dotnet build tests/AnalyzerSmoke/AnalyzerSmoke.csproj` (expected: `error DUN001`);
  - `dotnet run --project src/Sim -- stream-handles` and `content-hashes` (expected: no diff).
- [ ] **Step 4: Commit** `M1-12a: vocabulary, roadmap and the CO change row`.

## Done when → evidence (M1-12a's share of the roadmap boxes)

- The M1-10 record's 6.3 M1-12 combat-start tests: `CombatStartConditionTests`.
- The clock half of "CO 20.2 clock edge-case … tests": `ClockTests`, `TickClockTests`.
- The carry-overs: `HeldEventsTests`, `DispatchHazardTests`, and the opener preview test.
- Left for M1-12b: the pipeline-order tests, the entry-point skips and the property tests.
