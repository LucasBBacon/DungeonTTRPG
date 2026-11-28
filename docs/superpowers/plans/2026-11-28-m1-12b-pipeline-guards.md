# M1-12b Condition Pipeline, Guards and Relations Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Every condition reaches a unit through one CO 9.2 pipeline with its entry table, re-application rules, control guards, Unyielding downgrade and relation kinds. Abilities and data triggers route conditions into it. Three property tests show that hard control can't lock a unit out.

**Architecture:**
- **This is the second of two plans for roadmap step M1-12** (M1-12a owner ruling 1). M1-12a built the records, the instance state, the clock and the internal `Conditions.Insert`. This plan adds everything that decides *whether* and *how* an instance is inserted.
- **One driver, `Conditions.Apply`, in a new `src/Core/Conditions/Pipeline.cs`.** It reads the entry table as data and runs the steps in CO order, stopping at the first that resists. `Conditions.Would` runs the same evaluation without committing. `Insert` keeps its throws as assertions: the pipeline settles every refusal before it calls `Insert`.
- **The guards live in `src/Core/Conditions/Guards.cs`** as pure predicates. The relation queries and steps live in `src/Core/Conditions/Relations.cs`. `Conditions.End` remains the only way an instance ends, and it now also runs `onEnd` and the composure grant.
- **Callers:**
  - `Resolution.Land` (ability uses);
  - `TriggerDispatch.Fire` (trigger actions);
  - `Clock` (the staged `then`, Unyielding);
  - `Conditions.End` (Steeled, composure, `onEnd`).

  Tests drive the rows that have no production caller yet: skill actions, terrain, aura pulses, Burning's spread and the talisman.
- **State:** save version 10 adds `ConditionState.MarkedHard`, `GameState.Floor` and `Unit.Rank`. Rules version 5.

**Tech Stack:** C# 12 / .NET 8; the M1-04 content framework; the M1-07 effects and trigger dispatch; the M1-11 ability pipeline; the M1-12a condition records, state and clock; xUnit.

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-12](../../roadmap/m1-vertical-slice.md#m1-12-conditions-core), the pipeline half, implementing:
- **CO 6.1, 6.3–6.6, 7.1–7.2, 9.1–9.2, 12.3 (Unyielding), 17.1 (`implies`, `relations`, `boss.downgrade`), 18.1 (test-only fixtures), 20.1 (the validators tied to this data) and 20.2 (the pipeline tests and the three property tests)** (`docs/superpowers/specs/2026-11-16-conditions-design.md`).
- **The M1-12a plan's "carried to M1-12b" items** (`docs/superpowers/plans/2026-11-28-m1-12a-condition-clock.md`, the PR #105 body, and the README's "M1-12a notes for owners").
- **M1-12a owner rulings 3 and 4**, which are built here.

## Owner rulings for this step (2026-11-28)

1. **Save version 10.** It adds `ConditionState.MarkedHard`, a saved `GameState.Floor` (default 1) and a saved `Unit.Rank` (`Minion | Standard | Elite | Boss`, default `Standard`). M1-22 fills rank from monster data, and the floor commit advances the floor. The property tests run on floors 2 and 3.
2. **Damage-side relations are deferred to M2.** These are per-part `damageType` and `damage` cure ends, and transforms triggered by a damage part. M1-12b builds the condition-incoming transform and the one-transform rule through a per-damage-event counter that `Land` creates. A `transform.by.damageType` is refused at load, with the path and the message "damage-triggered transforms arrive with M2".
3. **The composure property is tested as "a party unit gains no hostile turn-part major in its next turn"** (CO 20.2's third property, weakened). A party unit can still carry a turn-part major applied in its own Act phase into the next turn. Task 9 records the change as a CO change row.
4. **The lockout readings:**
   - **`oneOf` never completes a lockout.** A lockout needs one part that denies the action and one that denies the bonus.
   - **"Completes"** means both turn parts are denied with the incoming condition and not without it.
   - **Hostility, the depth gate and the per-fight cap** key on the incoming condition's source.
   - **CO 5.2's own-turn rule applies to a marked lockout.**
   - **A trigger-applied condition is sourced from the trigger's holder.**

## Global Constraints

- **CO 9.2 order:** 1 rider gate; 2 blocks ("No roll is made"); 3 contest; the gear step; 4 ward; 5 boss resolve, then the Unyielding downgrade; 6 transform; 7 re-check; 8 cancel, then override ("A cancel ends processing"); 9 "Stacking (6.3), then the instance is applied (a consumed transform source ends now), then `onApply` runs, then engine grants." "Each step that stops the pipeline emits `ConditionResisted {reason}`."
- **CO 9.2 entry table**, plus M1-12a ruling 4's row:

  | Origin | Enters at | Skips | Gear step |
  |---|---|---|---|
  | Ability use | Step 1 | — | Runs |
  | Skill action, aura pulse | Step 2 | — | Runs |
  | Terrain square or trap; Burning's spread | Step 2 | Contest as its data says | Runs |
  | `onApply` and `onEnd` relations | Step 2 | Contest and ward | Skipped |
  | Staged last stage | Step 2 | Contest and ward | Skipped |
  | Engine grants (Steeled, Unyielding, composure) | Step 9 | Everything before it | Skipped |
  | Talisman | Applied at once | Everything | Skipped |
  | Trigger action | Step 2 | Contest, unless the action names one | Runs |
- **Step 7, the re-check:** if step 5 or 6 changed the condition, "steps 2, the gear step, 4 and 5 run once more against the new condition". "A second change is not re-checked: the fallback is used, or the condition is resisted if there is none."
- **CO 6.3:** one instance per condition per unit. A re-application "replaces it only if its value is higher, or its value is equal and its effective end is later"; the replacement "takes everything". **CO 6.4:** `worsen` raises the value up to `valueCap` and leaves the duration alone. **CO 6.5:** accumulating adds a level up to `levelCap`; re-applying a staged condition "has no effect". `noRefresh`: a re-application has no effect.
- **CO 6.6 / 7.2:**
  - a unit holds at most one hard control (hard, marked hard, or `engineState`);
  - Steeled resists hard conditions and lockout completers;
  - depth gates: on floor 1 a monster or environment can't put hard control on a party unit; on floor 2 only an Elite or Boss source can; from floor 3 the gate is open;
  - party-facing hard and turn-part majors keep the party-facing band (5.3);
  - "after a turn-part major ... ends on a party unit, it is immune to hostile turn-part majors until the end of its next turn-end step";
  - a composite lockout is "marked hard for its lifetime" and counts toward the per-fight cap.
- **CO 12.3, Unyielding:**
  - a hard condition becomes its `boss.downgrade`;
  - it "holds at most one turn-part major at a time", and after one ends "it is immune to that group for 1 turn";
  - "it resists any condition that would complete a composite lockout".
- **CO 9.1 relation kinds:** `implies`, `onApply`, `onEnd`, `cancel`, `override`, `suppress`, `blocks`, `transform`. "At most one transform per damage event"; "a transform never triggers another". `implies` and `suppress` are queries, never stored (17.2). Suppression "never stops countdowns" (CO 8), and Steeled, Unyielding and `engineState` are never suppressed.
- **CO 5.4 item 3 order for an end:** `ConditionEnded`, the sustained cascade, `onEnd`, then Steeled.
- **CO 7.1 / 20.1:** the build fails when "a hard condition comes from an at-will, a reaction or a terrain square", a hard condition has other than exactly one downgrade, a cancel is one-sided, a relation partner does not resolve, a transform has no fallback, two relations of one record cover the same pair, or a lingering condition denies a turn part.
- **CO 18.1:** M1 content uses no `saveEnds`, `charges`, `decay`, `staged` or relations. Test-only fixtures, loaded through the real loader, cover each relation kind, each duration model and the step-7 re-check.
- **SV 4.3 / D-23:** no roll out of combat. The pipeline draws nothing in M1-12b: the contest is the ability's own roll (M1-12a ruling 3), and gear, ward and resolve are seams.
- **Determinism:** the DUN001–DUN012 analyzers stay green: no floats, `System.Random` or clocks in Core; no Dictionary/HashSet enumeration; sorts only with `[TotalComparer]`; `IntMath` for signed `/` and `%`. Every walk over instances is over `s.Effects` in seq order.
- **Out of scope:**
  - **M1-13:** the ten M1 conditions as content; the ability `effects` object form (value, duration override, `worsen` field), so routed conditions land at value 1 until then; Surprised's grant; ability properties.
  - **M1-17:** the gear step. **M1-24:** boss resolve and pips, and the other `boss` keys. **M1-25:** the preview field (the internal `Would` query is built here). **M1-20:** the VG 12.1 turn-part query beyond what `Payable` reads.
  - **M2:** riders (step 1 is a pass-through seam); the ward; auras; terrain content; damage-side relations (owner ruling 2); suppressing cures; Challenged immunity; Downed.

## Plan-level calls (mine, flagged for review)

1. **The driver.**
   - `Conditions.Apply(RulesContext, GameState, ConditionApplication) -> (ImmutableArray<GameEvent> Events, GameState Next)`.
   - `Conditions.Would(RulesContext, GameState, ConditionApplication) -> ConditionOutcome`, which is pure and draw-free. `Apply` is `Would` followed by the commit, so the two cannot drift.
   - One internal overload, `Conditions.Run(...) -> (ImmutableArray<GameEvent> Events, GameState Next, ConditionOutcome Outcome, ImmutableArray<PipelineStep> Ran)`, backs both. `Ran` lists the steps the driver entered, for the entry-skip test. `Land` reads `Outcome.TransformUsed` from it (call 9).
   - The step-7 re-check is recorded only when it runs. A seam step (rider gate, gear, ward, resolve) records that it ran and does nothing.
2. **The entry table is data** (`EntryTable.Of(ConditionEntry) -> (PipelineStep EntersAt, ImmutableArray<PipelineStep> Skips)`):

   | Entry | Enters at | Skips |
   |---|---|---|
   | `AbilityUse` | `RiderGate` | none |
   | `SkillAction`, `AuraPulse`, `Terrain`, `BurningSpread` | `Blocks` | none (the caller supplies the contest) |
   | `TriggerAction` | `Blocks` | `Contest` |
   | `Relation`, `StagedLastStage` | `Blocks` | `Contest`, `Gear`, `Ward` |
   | `EngineGrant` | `Stacking` | everything before it |
   | `Talisman` | none | every step: inserted at once |

   `enum PipelineStep { RiderGate, Blocks, Contest, Gear, Ward, Resolve, Transform, Recheck, CancelOverride, Stacking }`.
3. **The application:**

   ```
   ConditionApplication(string ConditionId, UnitId Target, EffectSource Source, ConditionEntry Entry,
       bool ContestPassed = true, int? Value = null, int Worsen = 0,
       ImmutableArray<DurationTerm>? Duration = null, long? RecordedDifficulty = null,
       long? SustainedBy = null, Faction? SourceFaction = null, DamageEvent? DamageEvent = null)
   ```

   - **`readonly record struct DamageEvent(bool TransformUsed)`** lives in `Pipeline.cs` beside it (Task 2). A null `DamageEvent` means the application belongs to no damage event, and its transform step may run.

   - **`SourceFaction`** comes from the source unit when it is present, from this field otherwise, and is null for an environment source (`EffectSource.Unit` null).
   - **A gone source:** `ConditionInsert` gains `Faction? SourceFaction`, and `Insert` accepts a source unit that is gone only when a faction is supplied. A `sourceTied` condition with a gone source is resisted with `SourceGone`.
   - **A gone source in the depth gate** counts as rank `Standard`.
4. **`ConditionResisted(string ConditionId, UnitId Holder, EffectSource Source, ConditionResistReason Reason)`** (`Events.cs`).
   - The live reasons, in this order: `SavePassed`, `AlreadyPresent`, `Steeled`, `AlreadyControlled`, `DepthGate`, `PartyFacing`, `Unyielding`, `Composure`, `Blocked`, `Cancelled`, `OutOfCombat`, `SourceGone`.
   - Reserved for later steps and never emitted now: `WardBlocked`, `Immune`, `ArmourVerb`, `TrinketProperty`.
   - The names follow the charm design (`2026-11-24-charm-abilities-design.md`, `ConditionResisted{reason}`).
   - It raises no rule event, so the held-event set does not change.
   - `ConditionEndReason` gains `Triggered`, `Staged`, `Cancelled`, `Overridden` and `Transformed`; `Consumed` becomes live. Their data spelling is lower camel.
5. **Step 9 stacking**, when the target already holds an instance of the id (`Held`):
   1. a core id: M1-12a's re-stamp;
   2. `noRefresh` or staged: `AlreadyPresent`;
   3. accumulating: the value rises by 1, keeping the seq, terms and `AppliedPhase`, with `ConditionApplied(seq, …, New: false)`; at `levelCap` it is `AlreadyPresent`;
   4. `Worsen > 0` (CO 6.4): `Value = min(Value + Worsen, valueCap)` with the duration and seq unchanged, and `ConditionApplied(New: false)`; already at the cap it is `AlreadyPresent`. With no instance, `Worsen` is ignored and the base value lands;
   5. otherwise CO 6.3 (call 6).
6. **The 6.3 refresh is not an end.**
   - **The comparison:** a higher value replaces. An equal value replaces only when the new effective end is later. Anything else is `AlreadyPresent`.
   - **The effective end** is "which future wearer turn end": `Left(terms, c) − 1`, plus 1 when the holder's next unprocessed turn end is the current one and the instance's `AppliedPhase` is at or after that turn's Act phase (the counting test). `untilRest`, `untilCamp` and an uncapped `trigger` are infinite, and two infinities tie. Windows count as 1.
   - **The replacement** removes the old instance with no `ConditionEnded`, no Steeled and no `onEnd`. It inserts the new one: a fresh seq, `AppliedPhase = Phase`, the new source and difficulty, and `OwnTurn` and `MarkedHard` recomputed. It re-points every `SustainedBy == old seq` to the new seq and emits `ConditionApplied(newSeq, …, New: false)`. This is `internal Conditions.Replace`.
   - A hard instance never reaches the replacement, because exclusivity blocks the second hard application at step 2.
7. **Step-2 refusals that protect `Insert`'s assertions**, at their places in call 13's list:
   - **`OutOfCombat` (D-23):** with no fight, the resolved terms (the override, else the record's `OutOfCombat` list, else its `Duration`, flattened) roll a repeat save.
   - **`PartyFacing`:** a party holder with no override, where the record's in-combat `Duration` breaks the party band.
   - **Repeat saves need a difficulty:** a resolved term that rolls one, or an out-of-combat list whose in-combat `Duration` does (the M1-12a carry), needs a recorded difficulty. Every caller supplies one (call 8), so a missing one throws `ArgumentException` as a caller bug.
8. **The recorded difficulty (CO 5.1 "recorded at application", M1-12a ruling 3).**
   - A save ability records the use's save difficulty, captured at the lock and already kept on the frame.
   - Any other ability records the user's `abilityDifficulty` at `Land`.
   - A trigger action records its holder's `abilityDifficulty` at the firing.
   - `then`, `onApply` and `onEnd` inherit the parent instance's value.
   - Engine grants record none.
   - This changes no frame shape.
9. **Routing.**
   - **`Resolution.Land`:** each `ability.Effects` id that names a condition goes through `Apply` with `Entry: AbilityUse`, the user as source, and a `DamageEvent` created once per (use, target). Other ids keep today's path.
     - After each application, stop if `s.Ended` or the target is gone.
     - On a passed save, each condition id goes through `Apply` with `ContestPassed: false`. Steps 1–2 can still stop it first, with their own reason. Otherwise it stops at step 3 with `SavePassed`.
     - A miss applies nothing and emits nothing.
   - **`TriggerDispatch.Fire`:** an `applyEffect` naming a condition goes through `Apply` with `Entry: TriggerAction`, sourced from the trigger's holder (owner ruling 4). The recipient and source checks stay first.
10. **Load refusals (`RulesContext`):**
    - the two M1-12a "conditions are applied through the pipeline (M1-12b)" refusals and their constant are removed;
    - an ability `effects` entry or an `applyEffect` naming `cond.steeled`, `cond.unyielding` or `cond.composed` is refused with the message "engine grants only (CO 7.2)";
    - a hard condition in a `combatStarted` trigger's `applyEffect` is refused (M1-12a ruling 4);
    - **CO 7.1:** a hard condition in the `effects` of an ability whose cost spends no limited pool, or of a reaction, is refused. M1-23's cooldowns widen "limited" later.
11. **Event-driven ends (M1-12a plan call 4's leftovers), in `TriggerDispatch.Run`, per queued event and raised rule event.**
    1. Snapshot the seqs of the subject's instances that have a `Charges { On: id }` or `TriggerEnd { Event: id }` term (flattened).
    2. Fire the due triggers as today.
    3. For each snapshotted seq still alive, in seq order, unless `s.Ended`:
       - a `TriggerEnd` term ends it with `Triggered`;
       - otherwise one charge is spent, and at 0 it ends with `Consumed`.

    The subject is the holder: `ruleEvent.Subject(e) == instance.Holder`.

    **The birth rule:** an instance whose own `ConditionApplied` sits in the current queue answers only events queued after it. An instance with no `ConditionApplied` in the queue answers every event.

    Ends are not firings and don't count against `MaxTriggerFirings`. A suppressed instance's terms still count (suppression never stops countdowns).
12. **Rule events `conditionApplied` and `conditionEnded`**, each with the holder as subject (`RuleEvents.Core`, kept ordinal).
    - A condition's own `conditionEnded` trigger never fires, because the instance is gone. Its own `conditionApplied` trigger does fire.
    - A Steeled re-stamp raises `conditionApplied` too.
    - The held DTO, the digest and load validation already carry both events.
13. **The step-2 guards (`Guards.cs`)**, computed with `hardLike = definition.IsHard || CompletesLockout(...)`. The first that applies stops the pipeline:
    1. **Steeled:** the holder holds `cond.steeled` and the application is `hardLike` → `Steeled`.
    2. **Exclusivity:** `hardLike` and the holder already holds hard control (hard, `MarkedHard` or `engineState`) → `AlreadyControlled`. A non-hard application never trips it, so a Daze still lands on a Stunned unit.
    3. **The depth gate:** the holder is party, the application is `hardLike`, the source is an enemy or the environment, and `DepthGateBlocks(s.Floor, rank)` → `DepthGate`. Floor 1 blocks. Floor 2 blocks unless the rank is `Elite` or `Boss`; the environment is blocked there (set pieces are M2). Floor 3 and above is open.
    4. **The party-facing limit** (call 7) → `PartyFacing`. CO 9.2 step 2 lists it with the depth gates.
    5. **The lockout under Unyielding:** the application completes a lockout and the holder holds `cond.unyielding` → `Unyielding`. Otherwise a completing application is remembered as `markLockout` for step 9.
    6. **Unyielding's one-at-a-time rule:** the holder holds `cond.unyielding`, the application has `turnPartGroup`, and the holder already holds a different turn-part major → `Unyielding`.
    7. **Composure:** the application has `turnPartGroup`, the holder holds `cond.composed`, and either the holder holds `cond.unyielding` (any source; 12.3 has no "hostile") or the holder is party and the source is hostile or the environment → `Composure`. So CO 20.2's "an Unyielding downgrade is re-checked against composure" reports `Composure`.
    8. **Relation `blocks`** (call 17) → `Blocked`.
    9. **Call 7's other refusals:** `OutOfCombat`, `SourceGone`.

    The re-check (step 7) runs the whole list again, recomputing `hardLike`.
14. **The composite lockout (owner ruling 4).**
    - **The lockout set:** the incoming instance plus each live instance on the holder whose effective end (call 6) is at or after the wearer's next turn end. In combat, windows are checked exactly from `CombatState.Order` and `Current`; out of combat, every live instance counts.
    - **Completion:** the set denies the action and the bonus (`Denies.Action` and `Denies.Bonus`; `OneOf` never counts), and without the incoming instance it does not.
    - **A marked instance** has `MarkedHard = true` for life. Every reader of `IsHard` now reads `IsHard || MarkedHard`: the Steeled grant, the lost-turn check, the ruling-2 end at Unyielding, the `OwnTurn` stamp and its assertion, and exclusivity.
15. **Composure is a third core definition, `cond.composed`** (beneficial, `combatOnly`, no terms, no effects), handled like Steeled.
    - **The grant:** `Conditions.End` grants it through the `EngineGrant` entry when a fight is on (`s.Combat` is not null), the ended definition has `turnPartGroup`, the holder is party or holds `cond.unyielding`, and the reason is neither `CombatStart` nor `CombatEnd`. Out of combat no composure is granted: CO 7.2 guards fights, and the instance is `combatOnly`.
    - **The end:** it ends at the first turn-end step that begins after it is gained (the Steeled clause in `Clock` is extended to cover it).
    - **The block** is call 13 guard 7.
    - All five CO turn-part majors share the one group (the boolean flag).
    - `Conditions.IsCoreId` covers `cond.composed`, so the existing id checks in `RulesContext` treat it like Steeled.
16. **Unyielding's downgrade.**
    - **Data:** `boss.downgrade` opens in the record (the other `boss` keys stay refused until M1-24). It is a condition id or the literal `"none"`.
    - **Validation:** required on every hard record except an `engineState` one, and refused on a non-hard one. It must name a non-hard condition. The check runs after the duration and band checks, so an existing refusal keeps its path.
    - **Step 5:** the holder holds `cond.unyielding` and the application `IsHard`. The resolve seam does nothing, then the application becomes its downgrade. `"none"` is resisted with `Unyielding`. A change sends the application to step 7.
    - **Steeled, Unyielding and composure** now insert through `Apply` with `Entry: EngineGrant`.
17. **Relation data.** A top-level `implies` list, and a `relations` list of objects tagged by `kind`:
    - `onApply {apply, to: holder|source, duration?, value?}`;
    - `onEnd {apply, reasons | except, duration?, value?}`;
    - `cancel {with}`;
    - `override {target}`;
    - `suppress {target, parts?: [tick, modifiers, triggers]}`;
    - `blocks {target}`;
    - `transform {by: {condition}, into, fallback: id | "none", consumes?: true}`.

    A selector `target` is `{condition}`, `{tag}`, or `{all: true}`, and `all` is allowed in `suppress` only. A selector never matches its own record, a core definition or an `engineState` record.

    **Refused at load, each with its path and owner:**
    - `by.damageType` (M2, owner ruling 2);
    - `by.property` (abilities have no properties yet);
    - `parts: [cures]` (M2);
    - `kind: implies` (use the top-level list);
    - `transformed` and `combatStart` in `onEnd` reasons.

    **Validated at load:**
    - every partner resolves to a condition;
    - `cancel` is symmetric, and nothing cancels itself;
    - a `transform` has a `fallback`;
    - no two relations of one record share a kind and partner;
    - `suppress` never targets Steeled, Unyielding or an `engineState` record;
    - `implies` is depth 1: an implied record implies nothing and is not a core id;
    - the `onApply`/`onEnd` graph has no cycle, and no hard `apply` target;
    - two records may not suppress each other;
    - a lingering record (`untilRest`/`untilCamp`) has no `denies`.

    `ConditionDefinition` gains `ImmutableArray<string> Implies`, `ImmutableArray<Relation> Relations` and `string? Downgrade`. These change its canonical bytes.
18. **The relation queries and hooks** (`Relations.cs`):
    - `Suppressed(rules, s, instance, part)` is one level: a suppressor works whatever its own suppression, and a suppressed instance's `implies` and `blocks` stop;
    - `Implied(rules, s, unit)` and `Has(rules, s, unit, id)`;
    - `Effects.Held` skips instances whose modifiers are suppressed and appends implied definitions after their parent, first occurrence kept;
    - `TriggerDispatch.Due` skips suppressed triggers;
    - `Clock.Ticks` skips suppressed ticks.
19. **The relation steps:**
    - **Step 6, transform:** this runs only when the application's `DamageEvent` has not used its transform, and the application is not itself a transform result.
      - The first instance on the target, in seq order, with `transform.by.condition == incoming` wins. The application becomes `into`.
      - If `into` is "a tier the source may not apply to this target" (CO 9.2 step 6), the `fallback` is used instead. This dry run checks only Steeled, exclusivity, the depth gate and the party-facing limit (guards 1–4). Every other guard acts at the step-7 re-check, where it resists. A `"none"` fallback is resisted with the refusing guard's reason.
      - The event's transform is marked used. Nothing is consumed yet.
      - A transform result's `onApply` applications carry a used event.
    - **Step 7:** the call 13 list runs once more, then step 5 runs. A change during the re-check is the "second change": the transform's fallback if one is in play and differs, else resisted with the reason that refused it.
    - **Step 8, cancel:** the first instance B, in seq order, that the incoming record cancels ends with `Cancelled`, through `End`, so B's `onEnd` and a Steeled can follow. Then `ConditionResisted(incoming, Cancelled)`, and processing stops.
    - **Step 8, override:** every instance matched by an `override` selector ends with `Overridden`, in seq order, skipping a pending transform source. Then step 9 runs.
    - **Step 9, after the insert:**
      1. a consumed transform source ends with `Transformed`, no `onEnd`, and Steeled if it was hard. It is consumed only when the condition applied is the transform's `into` (CO 9.1: "when R is applied"). A fallback, a downgrade or a re-check change leaves the source in place;
      2. each `onApply` runs in data order (`Entry: Relation`, the applied instance's source, the recipient by `to`);
      3. engine grants.
    - **`onEnd` lives in `Conditions.End`**, in CO 5.4 order: `ConditionEnded`, the sustained cascade, each admitted `onEnd` (`Entry: Relation`, on the ended instance's holder), then Steeled and composure. There is no `onEnd` for `CombatStart`, `Transformed`, or a dead holder.
20. **The staged `then`.** A failed staged save that would move `Value` to `Stages` on a term with `Then`:
    1. applies `Then` through `Apply` (`Entry: StagedLastStage`, the staged instance's source, faction and difficulty);
    2. if it lands, the staged instance ends with `Staged`;
    3. if it is resisted, `Value` stays at `Stages − 1` and the instance runs on.

    Without `Then`, M1-12a's behaviour is unchanged. `then` must name a hard condition (CO 6.5).
21. **The round-end win guard (M1-12a carry).**
    - After its round-end dispatch, `Turns.ResumeIfVacated` returns unwrapped when `CombatEnd.IsWon(s)`. `Settle`'s next pass then ends the fight.
    - The turn-start step's lost-turn check (item 8) is skipped when the fight was won at item 7.
22. **`denies` is enforced (CO 7.1).** `AbilityUse.Payable` refuses:
    - an action-costing use while a held instance denies the action (`CommandError.ActionDenied`);
    - a bonus-action use while one denies the bonus (`BonusActionDenied`);
    - either use when a held `oneOf` instance has already seen the other part spent.

    Movement is not denied here.
23. **Stale context:** `TriggerDispatch` re-reads `ctx.Instance` from the state before each action of a firing (an M1-12a deferred minor).
24. **Versions.**
    - **Rules version 5** in Task 1, the first task that changes what a command does. Doc line: "conditions go through the CO 9.2 pipeline; condition events raise rule events; event-driven ends".
    - **Save version 10** in Task 4. Doc line: "v10 (M1-12b) adds markedHard, the floor and unit rank".
    - `Floor` is validated to `1 ≤ Floor ≤ Config.RouteFloorCount`. `Rank` is written by name.

## Review Focus

1. **A refresh is not an end:** replacing an instance under CO 6.3 emits no `ConditionEnded`, grants no Steeled and runs no `onEnd`, and a `sustained` dependent survives, pointing at the new seq (Task 2).
2. **An event-driven end and the instance that event created:** a `charges {on unitMoved}` condition applied by a `unitMoved` firing, or by an ability whose push moved the target earlier in the command, keeps its charge (Task 1).
3. **A staged chain blocked at its last stage:** with the holder Steeled, `then` is resisted and the staged instance stays at stage `Stages − 1` until its next save or cap; nothing throws when its source is dead (Tasks 3 and 5).
4. **Guard order on several hits:** a lockout completer on a Steeled unit reports `Steeled`; on an Unyielding unit it reports `Unyielding`; a Daze on a Stunned unit lands (Task 5).
5. **A cancel ends processing:** the incoming condition is not inserted, the override and stacking do not run, and the order is `ConditionEnded(B, Cancelled)`, B's `onEnd`, Steeled, `ConditionResisted(A, Cancelled)` (Task 8).

---

### Task 1: Carries, rule events and event-driven ends; rules version 5

**Files:**
- Modify: `src/Core/Conditions/Conditions.cs` (`Insert`, `CombatStart`, `Read`), `src/Core/Conditions/ConditionState.cs` (`Triggered`), `src/Core/Effects/Registries.cs` (rule events), `src/Core/Effects/TriggerDispatch.cs` (calls 11, 23), `src/Core/Combat/Turns.cs` (`ResumeIfVacated`), `src/Core/Combat/CombatEnd.cs` (extract `internal static bool IsWon(GameState s)`: in combat, not ended, no `Enemy` unit; `EndIfWon` uses it), `src/Core/Conditions/Clock.cs` (item 8 after a win), `src/Core/Run/Versions.cs`
- Modify tests: `RunConfigTests` (rules 5), `EffectsKindTests` (the `RuleEvents.Core` list becomes the eight ids in ordinal order, with `EventType` asserts for the two new rows)
- Test: `tests/Core.Tests/ConditionCarryTests.cs`, `tests/Core.Tests/EventEndTests.cs`; fixture `test.cond.switch` in `ConditionFixtures.cs` (hard, `combatOnly`, `duration [saveEnds {will, cap 2}]`, `outOfCombat [turns:10]`)

**Interfaces:**
- `RuleEvent.Of<ConditionApplied>("conditionApplied", e => e.Holder)`, `RuleEvent.Of<ConditionEnded>("conditionEnded", e => e.Holder)`.
- `ConditionEndReason.Triggered`.

- [ ] **Step 1: Write the failing tests:**
  - **The difficulty carry** (on a monster holder):
    - `Insert_on_out_of_combat_terms_needs_a_difficulty_for_the_combat_duration`: `ArgumentException`;
    - `The_switched_repeat_save_rolls_against_the_recorded_difficulty`;
    - `CombatStart_throws_on_a_switch_target_with_no_difficulty` (a hand-built state).
  - **Rule events:**
    - `A_conditionApplied_trigger_fires_for_its_holder_only`;
    - `A_conditionEnded_trigger_fires_and_its_own_never_does`;
    - `A_self_reapplying_pair_hits_the_firing_bound`: `TriggerLoopException`.
  - **The win guards:**
    - `A_win_in_a_round_end_trigger_starts_no_next_turn`: a window whose source died ends `SourceGone` at the round end, and a `conditionEnded` (scope any) hook kills the last enemy. The events are `[…, TurnEnded(last), …, ConditionEnded(SourceGone), HookFired, …, UnitDied, CombatEnded]`, with no `TurnStarted` after `TurnEnded(last)`.
    - The same through `Turns.Advance`, asserting the vacated, unwrapped state.
    - `A_win_in_a_turnStarted_trigger_costs_no_turn_start`.
  - **Event-driven ends:**
    - `A_charge_is_spent_on_its_holders_event_only`: token `charges:1 {on unitMoved}` ends `Consumed` on its holder's move, not on another unit's;
    - `A_trigger_term_ends_Triggered`;
    - `The_tokens_own_payoff_trigger_fires_before_its_charge_is_spent`;
    - `An_instance_applied_by_a_unitMoved_firing_keeps_its_charge` (Review Focus 2);
    - `An_instance_born_later_in_the_queue_ignores_earlier_events`.
  - **Stale context:** `A_second_action_reads_the_current_instance`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "ConditionCarryTests|EventEndTests"`. Expected: fail.
- [ ] **Step 3: Implement** plan calls 11, 12, 21 and 23, the carry assertions in `Insert`, `CombatStart` and `Read`, and rules version 5.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass; existing goldens unchanged.
- [ ] **Step 5: Commit** `M1-12b: condition rule events, event-driven ends, the round-end win guard and the difficulty carry; rules version 5`.

### Task 2: The pipeline driver and step 9

**Files:**
- Create: `src/Core/Conditions/Pipeline.cs` (`ConditionEntry`, `PipelineStep`, `EntryTable`, `ConditionApplication`, `DamageEvent`, `ConditionOutcome`, `Conditions.Apply`/`Would`/`Run`, `Conditions.Replace`)
- Modify: `src/Core/Engine/Events.cs` (`ConditionResisted`, `ConditionResistReason`), `src/Core/Conditions/ConditionState.cs` (`ConditionInsert.SourceFaction`), `Conditions.cs` (`Insert` accepts a gone source with a faction; Steeled and Unyielding through `EngineGrant`), `src/Core/Conditions/Clock.cs` (Unyielding grant)
- Test: `tests/Core.Tests/PipelineTests.cs`

**Interfaces:**
- Plan calls 1–7. `ConditionOutcome(ConditionOutcomeKind Kind, string ConditionId, ConditionResistReason? Reason, int Value, ImmutableArray<DurationTerm> Terms, bool TransformUsed)`, `enum ConditionOutcomeKind { Applied, Replaced, Raised, Resisted }`. Step 2 holds only call 7's refusals (`OutOfCombat`, `PartyFacing`, `SourceGone`) in this task; steps 5–8 are pass-throughs. Later tasks fill them.

- [ ] **Step 1: Write the failing tests:**
  - **First application:**
    - `A_first_application_inserts_New_true`;
    - `Apply_draws_nothing`: `s.Rng` is unchanged;
    - `Would_equals_Apply`: on every case in this file, `Would`'s outcome matches what `Apply` committed.
  - **6.3 (Review Focus 1):**
    - `A_higher_value_replaces`: new seq, fresh `AppliedPhase`, new source, `New: false`, no `ConditionEnded`;
    - `An_equal_value_with_a_later_end_replaces`;
    - `An_equal_or_earlier_end_is_AlreadyPresent_and_changes_nothing`;
    - `A_lower_value_with_a_later_end_is_AlreadyPresent`;
    - `The_counting_test_breaks_the_tie`: a `turns:2` applied in the wearer's Act phase against an off-turn `turns:2` with 2 left replaces;
    - `A_sustained_dependent_follows_the_replacement`.
  - **6.4 and 6.5:**
    - `Worsen_raises_to_the_cap_keeping_seq_and_terms` (burn), and `Worsen_with_no_instance_lands_the_base_value`;
    - `Accumulating_adds_a_level_then_is_AlreadyPresent_at_the_cap` (weary);
    - `Staged_and_noRefresh_reapplication_is_AlreadyPresent` (calcify, blessed).
  - **Call 7's refusals:**
    - `Out_of_combat_a_repeat_save_is_resisted_OutOfCombat`;
    - `A_party_holder_with_a_band_breaking_record_is_resisted_PartyFacing`: no throw;
    - `A_sourceTied_application_with_a_gone_source_is_resisted_SourceGone`;
    - `A_gone_source_with_a_faction_inserts`.
  - **Engine grants:** `Steeled_goes_through_EngineGrant_and_restamps` (events unchanged from M1-12a).
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter PipelineTests`. Expected: build fails.
- [ ] **Step 3: Implement** plan calls 1–7, and the `EngineGrant` part of call 16.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass; `ClockTests` and `CombatStartConditionTests` goldens unchanged.
- [ ] **Step 5: Commit** `M1-12b: the CO 9.2 pipeline driver, its entry table and step-9 stacking`.

### Task 3: Routing, load refusals and the staged `then`

**Files:**
- Modify: `src/Core/Abilities/Resolution.cs` (`Land`, `Decide`), `src/Core/Effects/TriggerDispatch.cs` (`Fire`), `src/Core/Effects/RulesContext.cs` (call 10), `src/Core/Conditions/ConditionsKind.cs` (`then` must be hard), `Clock.cs` (call 20), `ConditionState.cs` (`Staged`)
- Modify tests: `ClockTests.Staged_moves_a_stage_on_each_failed_save_and_ends_past_its_stages` (flips to call 20), the `ConditionRecordTests` "(M1-12b)" refusal cases (flip to the new refusals)
- Test: `tests/Core.Tests/ConditionRoutingTests.cs`; ability fixtures in `AbilityFixtures.cs`

**Interfaces:**
- `DamageEvent` (Task 2): `Land` starts one per (use, target) with `TransformUsed: false`, calls `Conditions.Run`, and passes `Outcome.TransformUsed` on to the next application for the same target.

- [ ] **Step 1: Write the failing tests:**
  - **Routing:**
    - `An_ability_condition_lands_from_the_user_with_its_difficulty`: `ConditionApplied`, not `EffectApplied`. An attack records the user's `abilityDifficulty`; a save ability records the use's save difficulty.
    - `A_miss_applies_and_emits_nothing`.
    - `A_passed_save_emits_SavePassed`.
    - `A_trigger_applyEffect_of_a_condition_goes_through_the_pipeline`: sourced from the trigger's holder, recording that holder's `abilityDifficulty`. It lands on a target that would pass a save (contest skipped).
  - **Load refusals:**
    - `Core_ids_are_refused_in_effects_and_applyEffect`;
    - `A_hard_condition_on_a_combatStarted_trigger_is_refused`;
    - `A_hard_condition_on_an_at_will_or_a_reaction_is_refused`;
    - `The_through_the_pipeline_refusals_are_gone`;
    - `A_then_naming_a_non_hard_condition_is_refused`.
  - **The staged `then` (Review Focus 3):**
    - `Failing_into_the_last_stage_applies_then_and_ends_Staged`: the stun is applied in the turn-end step, so it is not counted that turn and costs the next turn start (CO 20.2's clock bullet).
    - `Then_lands_after_the_source_died`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter ConditionRoutingTests`. Expected: fail.
- [ ] **Step 3: Implement** plan calls 8, 9, 10 and 20.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12b: abilities and triggers route conditions into the pipeline; the staged then`.

### Task 4: Lockout state, floor and rank; save version 10

**Files:**
- Create: `src/Core/Conditions/Guards.cs` (`IsHardLike` only; Task 5 adds the rest)
- Modify: `ConditionState.cs` (`MarkedHard`, canonical field 10; `ConditionInsert.MarkedHard`), `src/Core/State/GameState.cs` (`Floor`, `Unit.Rank`, `enum UnitRank { Minion, Standard, Elite, Boss }`, validation), `Snapshot.cs` (v10, DTOs), `StateDigest.cs`, `Conditions.cs` (the Steeled grant, the `OwnTurn` stamp and assertion), `Clock.cs` (the lost-turn check, the ruling-2 end), `Versions.cs`
- Modify tests: the version literals (`"version":9` → 10, "future" → 11)
- Fixtures (moved here from Task 5 so Task 4 has a deny-action major): `test.cond.nausea` (major, `denies.action`, `turnPartGroup`, `turns:1`) and `test.cond.distract` (major, `denies.bonus`, `turnPartGroup`, `turns:1`)
- Test: `tests/Core.Tests/LockoutStateTests.cs`

**Interfaces:**
- `ConditionState(..., bool OnOutOfCombatTerms, bool MarkedHard = false)`; `GameState.Floor` (`int`, default 1); `Unit.Rank` (`UnitRank`, default `Standard`, last parameter).
- `internal static bool Guards.IsHardLike(ConditionDefinition d, ConditionState c) => d.IsHard || c.MarkedHard`.

- [ ] **Step 1: Write the failing tests:**
  - **Round trips and the digest:**
    - `MarkedHard_floor_and_rank_round_trip_and_reach_the_digest`;
    - `A_v9_save_is_refused`.
  - **Format errors:**
    - `A_floor_outside_the_route_is_a_format_error`;
    - `Read_throws_on_MarkedHard_on_a_hard_or_non_harmful_instance`: `InvalidOperationException` from `Conditions.Read`, beside the `OwnTurn` assertion (a save carries the id, not the tier, so `Snapshot.Load` can't check it).
  - **`MarkedHard` behaves as hard** (each test inserts `test.cond.nausea` with `ConditionInsert.MarkedHard = true`; step 9 sets that field from `markLockout`):
    - `A_marked_instance_costs_a_turn_start_and_counts_toward_the_cap`;
    - `Steeled_follows_a_marked_instance_end`;
    - `A_marked_instance_inserted_in_the_own_turn_window_is_OwnTurn`.
  - **The deferred ruling-2 test:** `A_hard_instance_inserted_after_Unyielding_is_not_ended_early` (the `seq < unyielding` guard).
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter LockoutStateTests`. Expected: build fails.
- [ ] **Step 3: Implement** plan call 14's state part and call 24's save version.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12b: markedHard, the floor and unit rank; save version 10`.

### Task 5: The step-2 control guards and composure

**Files:**
- Modify: `src/Core/Conditions/Guards.cs`, `Pipeline.cs` (step 2), `Conditions.cs` (`cond.composed`; the composure grant in `End`), `Clock.cs` (composure's end beside Steeled's), `ConditionsKind.cs` (the `denies` and `turnPartGroup` checks), `src/Core/Abilities/AbilityUse.cs` (`Payable`, call 22), `src/Core/Engine/Engine.cs` (`CommandError.ActionDenied`, `BonusActionDenied`)
- Fixtures: `test.cond.grip` (enemy-facing major, `denies.action`, `turnPartGroup`, `saveEnds {fortitude, cap 3}`)
- Modify tests:
  - `ConditionRecordTests`: `test.all`'s `denies` becomes `action` only (it keeps `turnPartGroup`); the core-definition count becomes `Records.Length + 3`;
  - `ClockTests` (`Removing_the_sustainer_…`) and any other in-combat golden where a `turnPartGroup` instance ends on a party unit: the composure `ConditionApplied` joins the expected events. Out-of-combat goldens (`TickClockTests`) are unchanged, because no composure is granted out of combat.
- Test: `tests/Core.Tests/ControlGuardTests.cs`

**Interfaces:**
- `Guards.CompletesLockout(RulesContext, GameState, UnitId holder, ConditionDefinition incoming) -> bool`
- `Guards.HoldsControl(RulesContext, GameState, UnitId) -> bool`
- `Guards.DepthGateBlocks(int floor, UnitRank? sourceRank) -> bool` (a null rank means the environment)
- `Conditions.ComposedId = "cond.composed"`

- [ ] **Step 1: Write the failing tests:**
  - **Steeled:**
    - `Steeled_resists_Stun_and_a_lockout_completer_but_not_Daze`;
    - `The_Steeled_window_holds_at_the_next_turn_start_and_lapses_after_its_turn_end`.
  - **Exclusivity:** `Stun_on_a_Stunned_Surprised_or_marked_unit_is_AlreadyControlled`; `Daze_on_a_Stunned_unit_lands`.
  - **The depth gate:**
    - `The_depth_gate_by_floor_and_rank`: floor 1 → `DepthGate`; floor 2 Standard → gated, Elite and Boss → land; floor 3 → lands; the environment is gated on floors 1 and 2;
    - `Enemy_on_enemy_and_ally_on_party_are_ungated`.
  - **The lockout:**
    - `Nausea_plus_distract_is_a_lockout_marked_for_life`;
    - `Daze_plus_distract_is_no_lockout`: `oneOf` never completes;
    - `A_window_ending_before_the_next_turn_end_is_not_counted`.
  - **CO 20.2, verbatim cases:**
    - `A_composite_lockout_counts_toward_the_per_fight_cap_and_is_resisted_from_a_floor_1_monster`;
    - `The_per_fight_cap_counts_a_lockout_but_not_own_turn_hard_Surprised_or_an_allys_hard_condition` (an ally's `test.cond.stun` stands in for CO's "an ally's freeze");
    - `Party_composure_after_Dazed`: the next hostile Daze and nausea are resisted `Composure` until after the next turn-end step; an ally's Daze lands; an enemy unit gains no composure; a Daze ending out of combat grants none.
  - **Guard order (Review Focus 4):** `A_lockout_completer_on_a_Steeled_unit_reports_Steeled`; `A_band_breaking_application_on_a_composed_party_unit_reports_PartyFacing`.
  - **Moved from Task 3, needing the Steeled guard:**
    - `A_Steeled_target_reports_Steeled_before_the_save`: a save ability whose save the target would pass;
    - `A_Steeled_holder_resists_then_and_stays_at_the_previous_stage` (Review Focus 3).
  - **Load checks:**
    - `oneOf_with_action_or_bonus_is_refused`;
    - `A_non_hard_condition_denying_both_parts_is_refused`;
    - `turnPartGroup_needs_a_harmful_major_with_denies`;
    - `A_harmful_major_with_denies_needs_turnPartGroup`.
  - **`Payable`:** `A_denied_action_is_refused_ActionDenied`, `A_denied_bonus_is_refused_BonusActionDenied`, `oneOf_refuses_the_second_part`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter ControlGuardTests`. Expected: fail.
- [ ] **Step 3: Implement** plan calls 13, 14, 15 and 22.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12b: Steeled immunity, exclusivity, depth gates, composite lockout and composure`.

### Task 6: Unyielding

**Files:**
- Modify: `ConditionDefinition.cs` (`Downgrade`), `ConditionsKind.cs` (open `boss.downgrade`; validate it), `src/Core/Effects/RulesContext.cs` (the cross-record check), `Pipeline.cs` (steps 5 and 7, guards only), `Guards.cs` (Unyielding's turn-part rule)
- Fixtures: downgrades on every hard fixture (`test.cond.stun → test.cond.daze`, `test.cond.tied`, `test.cond.held` and Task 1's `test.cond.switch` to a non-hard fixture, `test.cond.sleep` (new, hard, `turns:1`) → `"none"`); `test.cond.surprised` keeps none
- Modify tests: `CombatStartConditionTests` (a downgrade on the inline hard records Stupor, HardFight and Snare; no expected events change); `ConditionRecordTests` (a downgrade on each hard inline record, so every refusal keeps its asserted path; re-pin the canonical digest)
- Test: `tests/Core.Tests/UnyieldingTests.cs`

- [ ] **Step 1: Write the failing tests:**
  - **The downgrade:**
    - `Unyielding_turns_Stun_into_its_downgrade`;
    - `A_none_downgrade_is_resisted_Unyielding`.
  - **CO 20.2 verbatim:** `An_Unyielding_downgrade_is_rechecked_against_composure`: the Daze from a Stun is resisted `Composure` after a turn-part major ended.
  - **Unyielding's turn-part rules:**
    - `Unyielding_holds_one_turn_part_major_at_a_time`;
    - `Unyielding_is_immune_to_the_group_after_one_ends`;
    - `Unyielding_resists_a_lockout_completer`.
  - **Validation:**
    - `A_hard_record_without_a_downgrade_is_refused`;
    - `A_downgrade_on_a_non_hard_record_is_refused`;
    - `A_downgrade_naming_a_hard_condition_is_refused`;
    - `Other_boss_keys_stay_refused`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter UnyieldingTests`. Expected: fail.
- [ ] **Step 3: Implement** plan call 16 (steps 5 and 7, with the guards as the re-check set).
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12b: Unyielding's downgrade, its turn-part rule and the re-check`.

### Task 7: Relation data and the query hooks

**Files:**
- Create: `src/Core/Conditions/Relations.cs` (`Relation` records, `ConditionSelector`, `RelationKind`, `SuppressPart`, `RelationTo`; `Suppressed`, `Implied`, `Has`)
- Modify: `ConditionDefinition.cs` (`Implies`, `Relations`, canonical bytes), `ConditionsKind.cs` (parse; lift the `relations`/`implies` refusal), `RulesContext.cs` (call 17's cross-record checks), `src/Core/Effects/Effects.cs` (`Held`), `TriggerDispatch.cs` (`Due`), `Clock.cs` (`Ticks`), `Pipeline.cs` (`blocks` at step 2)
- Modify tests: `ConditionRecordTests` (the `relations`/`implies` refusal cases; re-pin the canonical digest)
- Fixtures (research-relations §4, minus damage-side): `test.cond.incap`, `test.cond.soak`, `test.cond.scorch`, `test.cond.grime`, `test.cond.chill`, `test.cond.freeze`, `test.cond.haste`, `test.cond.knock`, `test.cond.prone`, `test.cond.float`, `test.cond.stone`. Every hard one has a `boss.downgrade`. No fixture uses `by.damageType` or `cures` on damage.
- Test: `tests/Core.Tests/RelationQueryTests.cs`

**Interfaces:**
- `internal static bool Relations.Suppressed(RulesContext, GameState, EffectInstance, SuppressPart)`
- `internal static ImmutableArray<string> Relations.Implied(RulesContext, GameState, UnitId)`
- `public static bool Conditions.Has(RulesContext, GameState, UnitId, string conditionId)`

- [ ] **Step 1: Write the failing tests:**
  - **`implies`:** `Freeze_implies_incap_as_a_query`: speed includes incap's −3 and `Has` is true; no incap instance or event; ending freeze emits one `ConditionEnded`.
  - **`suppress`:**
    - `Suppressing_a_tick_keeps_its_decay_and_end`;
    - `Full_suppression_stops_modifiers_and_triggers`;
    - `Steeled_is_never_suppressed`.
  - **`blocks`:** `Blocks_resists_Blocked_and_draws_nothing`.
  - **Load refusals, one per rule in call 17:** each refusal names its path.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter RelationQueryTests`. Expected: build fails.
- [ ] **Step 3: Implement** plan calls 17 and 18, and the `blocks` guard.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12b: relation data, implies, suppress and blocks`.

### Task 8: The relation steps

**Files:**
- Modify: `Pipeline.cs` (steps 6, 7, 8 and 9's relation parts), `Conditions.cs` (`End`: `onEnd`, in call 19's order), `Relations.cs`
- Test: `tests/Core.Tests/RelationStepTests.cs`, `tests/Core.Tests/PipelineOrderTests.cs`

- [ ] **Step 1: Write the failing tests:**
  - **`onApply`:**
    - `Knock_applies_prone_after_itself`: `[ConditionApplied(knock), ConditionApplied(prone)]`, and prone outlives knock;
    - `A_held_float_blocks_the_onApply_but_knock_lands`.
  - **`onEnd`:**
    - `Haste_expiring_applies_daze_after_its_end`;
    - `A_cancelled_haste_applies_nothing`.
  - **`cancel` (Review Focus 5):** `Scorch_on_soak_cancels_both_and_stops`: `[ConditionEnded(soak, Cancelled), ConditionResisted(scorch, Cancelled)]`.
  - **`override`:** `Soak_overrides_grime`: `[ConditionEnded(grime, Overridden), ConditionApplied(soak)]`.
  - **`transform`:**
    - `Chill_on_soak_transforms_into_freeze_and_consumes_soak`: `ConditionApplied(freeze)`, then `ConditionEnded(soak, Transformed)`, with no `onEnd`;
    - `consumes_false_keeps_the_source`.
  - **The one-transform rule:** `One_ability_use_transforms_once`. The target holds two transform sources, and an ability carries two conditions that each meet one. Only the first transforms, and the second source is not consumed. A transform result's `onApply` never transforms.
  - **Pipeline order** (`PipelineOrderTests`, CO 20.2 verbatim, non-pip cases):
    - `Freezing_on_a_Soaked_Steeled_unit_gives_Chilled`: Soaked is still held;
    - `A_floor_1_monsters_freezing_attack_on_a_Soaked_hero_gives_Chilled`;
    - `A_second_change_in_the_recheck_uses_the_fallback`: soak + chill on an Unyielding unit; Soaked is still held;
    - `A_fallback_none_is_resisted_with_the_refusing_guards_reason`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "RelationStepTests|PipelineOrderTests"`. Expected: fail.
- [ ] **Step 3: Implement** plan call 19.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-12b: transform, re-check, cancel, override, onApply and onEnd`.

### Task 9: Property tests, the entry-skip test and docs

**Files:**
- Test: `tests/Core.Tests/ControlPropertyTests.cs`, `tests/Core.Tests/EntrySkipTests.cs`
- Modify: `docs/superpowers/specs/2026-11-16-conditions-design.md` (the CO 9.2 Trigger action row; CO 24 rows C17–C20), `docs/effects-vocabulary.md`, `docs/roadmap/README.md`, `docs/roadmap/m1-vertical-slice.md`

- [ ] **Step 1: Write the tests:**
  - **`EntrySkipTests`:**
    - `Each_entry_runs_exactly_its_steps`: a theory over every `ConditionEntry`. An application that is not resisted is driven through the `Ran` overload, and the trace equals `EntryTable` minus `Recheck`.
    - `A_trigger_action_skips_the_contest`.
    - `Step_2_runs_for_every_entry_but_EngineGrant_and_Talisman`: a Steeled target resists them all.
    - The PR states that the ward, gear and resolve skips are proven by trace until those steps exist.
  - **`ControlPropertyTests`:**
    - **Randomness** is test-side only, from `DrawStream` over `KeyAlgebra` with seeds 1..400.
    - **The fight:** a hero, a party ally and three enemies ranked Standard, Elite and Boss (no pips), on floors 2 and 3 (`RouteFloorCount = 5`), up to 10 rounds.
    - **Timing points:** applications are drawn at four points: off-turn; the wearer's Act phase; the turn-start step, through a `turnStarted` trigger; right after `EndTurn`.
    - **Each application** draws a fixture (Stun, Held with a party override `turns:1`, Daze, nausea, distract, grip), a source (an enemy by rank, an ally, self, or the environment) and a target. Every application goes through `Conditions.Apply`.
    - **Cures:** an optional cure (`Conditions.End(…, Ended)`) is drawn only off-turn and never at the Act phase.
    - **Turns end** through `Engine.Apply(EndTurn)`.
    - The tests:
      - `No_party_unit_loses_two_consecutive_turn_starts`;
      - `No_unit_loses_more_than_two_turn_starts_per_fight`;
      - `A_party_unit_gains_no_hostile_turn_part_major_in_its_next_turn` (owner ruling 3).
    - **A failure** prints the seed, the round and the drawn script.
    - **An exhaustive layer:** every sequence of length ≤ 3 over (timing point × {Stun, Daze, nausea, distract} × {Elite enemy, environment}) on one party target over 3 rounds.
    - **`The_generator_bites`:** over the run, at least one `TurnStartLost`, one Unyielding grant, one lockout mark, and one `ConditionResisted` of each of `Steeled`, `AlreadyControlled`, `DepthGate`, `PartyFacing` (grip with no override on a party unit), `Unyielding` and `Composure`. The other reasons are reached by the unit tests.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "ControlPropertyTests|EntrySkipTests"`. Expected: pass, in under 10 s. A failure is a defect in Tasks 2–8: fix it there, with the failing seed as a regression test.
- [ ] **Step 3: Docs:**
  - **CO 9.2** gains the Trigger action row, and a step-3 note for M1-12a ruling 3.
  - **CO 24 change rows:**
    - C17: the Trigger action row;
    - C18: the ability's own roll is the contest;
    - C19: owner ruling 3's property reading;
    - C20: owner ruling 4's lockout readings and the trigger-applied source.
  - **`docs/effects-vocabulary.md`:** the "cannot yet be applied" row and the M1-12 rows become M1-12b rows: routing, the rule events, event-driven ends, relations.
  - **README:**
    - "M1-12b notes for owners": the four owner rulings and plan calls 6, 8, 11, 13 (order), 15 and 22;
    - the D-23 resistance, the reserved resist reasons, the `damageType` deferral, and the gap between `Would` and resolution (resolution evaluates after the damage and earlier targets);
    - strike the closed party-band carry, and update the next-step line.
  - **Roadmap M1-12:** tick the boxes with evidence, including the M1-10 record's 6.3 combat-start box (`CombatStartConditionTests`).
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`, the analyzer smoke build (`dotnet build tests/AnalyzerSmoke/AnalyzerSmoke.csproj`, which reports `error DUN001`), `dotnet run --project src/Sim -- content-hashes` and `-- stream-handles` (no drift). Expected: all pass.
- [ ] **Step 5: Commit** `M1-12b: control property tests, the entry-skip test, docs and roadmap`.

## Done when → evidence (M1-12b's share of the roadmap boxes)

- [ ] The pipeline-order tests: `PipelineOrderTests`, `PipelineTests`, `ControlGuardTests`, `UnyieldingTests`, `RelationStepTests`.
- [ ] Each entry point skips exactly its listed steps: `EntrySkipTests`.
- [ ] The property tests: `ControlPropertyTests`.
- [ ] The M1-10 record's 6.3 combat-start tests: `CombatStartConditionTests` (built in M1-12a, ticked here).
- [ ] The M1-12a carries: `ConditionCarryTests`, `EventEndTests`.
