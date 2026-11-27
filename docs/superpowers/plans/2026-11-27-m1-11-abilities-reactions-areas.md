# M1-11 Abilities, Reactions and Areas Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Every ability use goes through one `UseAbility` pipeline: declared, validated with one reason code, cost paid, targets locked, resolved per target. Reactions hook into it with automatic policies or an "ask me" decision. Bursts, cones and lines pick their squares by one shared function, and a draw-free preview reuses the resolution code. An out-of-combat hostile ability is an opener: combat start runs first, the opener is charged to the striker's round-1 turn, and only then does round 1 begin.

**Architecture:**
- **Combat start split** (`src/Core/Combat/Turns.cs`): `BeginCombat` runs rulings-record steps 0–7 and leaves a "before round 1" marker. `BeginRound1` is step 9. `StartCombat` runs both, so the no-opener path and its goldens are unchanged. The striker carries an `OpenerCharge` mark until its first turn.
- **Geometry** (`src/Core/Grid/Sight.cs`, `Areas.cs`): one symmetric `CanSee` and the area shapes, in row-major order.
- **Abilities** (new `src/Core/Abilities/`): the ability record and its content kind, validation (`CheckUsable`, `CheckTarget`), the resolution pipeline with reaction windows, a suspended-resolution frame for "ask me", and the preview.
- **Engine:** `UseAbilityCommand` and `SetReactionPolicyCommand`. Trigger dispatch stops at a run end, and every command ends with a settle loop: `EndIfWon`, then `BeginRound1` or `ResumeIfVacated`.

**Tech Stack:** C# 12 / .NET 8; the M1-04 content and validator framework; the M1-07 effects and trigger dispatch; the M1-08 contests; the M1-09 damage function; the M1-10 turn loop, `Harm.Deal` and combat end; xUnit.

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-11](../../roadmap/m1-vertical-slice.md#m1-11-abilities-reactions-and-areas), implementing:
- **AB 6.8, 7.1, 7.3, 8.1–8.3, 14.1–14.6, 15.4, 16** (`2026-11-16-ability-resource-model-design.md`, lines 383–401, 421–442, 545–610, 614–712, 1144–1253, 1335–1356).
- **DH 10.4 (openers, the party area out of combat), 20.3 step 7, 20.5** (`2026-11-17-death-healing-design.md`, lines 710–740, 2038–2046, 2071–2100).
- **The [M1-10 rulings record](../specs/2026-11-27-m1-10-rulings-design.md)** sections 4.1, 5 (steps 0 and 6–9), 6.3 (the M1-11 tests) and 8 (lead's calls 4, 10 and 11).
- **XI 4.1.2** (`2026-11-23-exploration-information-design.md`, lines 389–400): `CanSee`, per owner ruling 1.
- **CL 3.4** (`2026-11-17-classes-concepts-design.md`, lines 190–205): "once per round" is a `turn` pool of 1; "once per turn" is a per-unit flag.

> The rulings record and the specs it amends are on branch `ccr-8c482fd4-mk1d5i` (861e586), not yet on main. This plan reads them there; the links resolve once that branch is merged.

## Owner rulings for this step (2026-11-27)

1. **`CanSee` is built in M1-11.** It is the real symmetric function over walls only, with its symmetry and corner tests. M1-34 keeps `KnownCanSee`, `KnownNoiseReach` and the party-knowledge layer. Task 9 amends M1-34's Delivers.
2. **An opener is validated against a fresh budget and the pools as they stand after the `fight` refill.** That is the state record step 4 produces before the charge. `turn` pools are not refilled at combat start.
3. **Any faction may be a striker.** No M1 content or AI declares an enemy opener, but the core allows one; the design is symmetric. `CombatScenarioTests`' ambush becomes a Foe opener against the hero.
4. **Saves support only `onSuccess: none`.** `"half"` is refused at load and recorded as **D-22** for the DH owner (Task 9). No M1 content uses it.

## Global Constraints

- AB 14.3: "`UseAbility` passes through fixed steps, each raising an event in the effects system": Declared; Validated: "cost, pools, the camp-spend rule, range, line of sight. The first failure returns one reason code"; Cost paid; "Targets locked, in unit order"; per target "attack roll or save, then damage ... then effects applied"; Resolved. "Reactions hook in at named points, such as between an attack roll and the hit being decided."
- AB 7.1: "Every ability declares exactly one cost": `action`, `bonus`, `reaction`, `attack`, `move:N`, `free`, `onHit`, `onEvent:<id>`, `none`. "A pool spend is separate from the cost."
- AB 7.3: the automatic policies are `always`, `ifItChangesOutcome`, `whilePoolAtLeastHalf` and `never`. "Policies are set per unit, per reaction, and last for the run. Their defaults come from data." "Monsters use their reactions' automatic policies; they have no 'ask me'." "Reactions do not nest." `whilePoolAtLeastHalf`: "`remaining ≥ ceil(max / 2)`".
- AB 8.1: distance is Chebyshev. Burst r: "Every square within distance r of a point". Cone L: "Orthogonal: squares 1..L forward, no further sideways than forward. Diagonal: both offsets 0..L, not the user's own square", (L+1)² − 1 squares. Line L: "1 square wide in one of the 8 directions, stopping at the first wall". "An area includes a square only if the area's origin can see it." "Corner cutting is blocked by every movement-blocking square." "Squares are enumerated in a fixed order (row by row)." "No area covers more than 25 squares at levels 1–6, or more than 49 at levels 7–10."
- AB 8.2: "Areas hit allies unless the ability is tagged `selective`. At-wills never hit allies." "A caster is never affected by its own casts' areas."
- AB 8.3: the preview gives "which units are affected, the odds for each, expected damage". It is compared with resolution "with full knowledge and with reactions switched off".
- AB 14.5: "The build fails when ... an area exceeds 25 squares at levels 1–6, or 49 at levels 7–10; a reaction has no automatic policy".
- AB 15.4: the action bar shows reasons that "do not depend on a target"; out of range and no line of sight show on the targeting cursor. "The reason is the validation code from 14.3."
- AB 6.8: "Each command (a step by the hero, or an ability by anyone) advances the world one tick." "The command that declares an opener ... runs combat start first and takes no out-of-combat tick."
- DH 10.4: "An ability use whose targets or area include a hostile unit ... runs the combat-start step before it pays its cost and resolves." "A party area used out of combat that includes no hostile unit ... starts nothing: its damage to a party unit is floored."
- DH 20.3 step 7: after the hero reaches 0, "No later target of an area ... or combat-end step is processed."
- Rulings record 5, steps 6–9: record the fight with a "before round 1" marker that is not `TurnVacated`. `CombatStarted` comes first, then the held messages. The opener is charged ("the action and any once-per-round use limits it spent, not once-per-turn flags"), its targets are locked and it resolves, then the win check; "If the fight is won, no `TurnStarted` or `TurnEnded` follows". Then the marker clears and slot 0 starts. At the striker's first turn, "turn start neither resets nor refills what the opener spent"; "once-per-turn flags and the camp-spend flag clear". "The mark lives on the unit". Save version 8, rules version 3.
- XI 4.1.2: "integer symmetric shadowcasting (Albert Ford's formulation), square centre to square centre"; "Sight is blocked on a diagonal only when both orthogonal neighbours of that diagonal block sight"; "defined, and symmetric, between transparent squares"; "Units never block sight".
- SV 4.3 / AB 14.4: rolls in combat, openers included, draw `combat`. Out-of-combat rolls are D-23 (plan call 5): V19t `ExplorationCombatIsolation` (SV 1001) forbids exploration drawing `combat`. SM 3: integer arithmetic only. The determinism analyzers (DUN001–DUN012) stay green. Previews draw nothing.
- Out of scope:
  - **M1-12:** condition contents of combat-start step 2; the turn-start and turn-end step contents.
  - **M1-13:** Surprised (step 3), per-step movement costs.
  - **M1-14:** universal actions; the `attack` cost; opportunity-attack and movement reaction triggers.
  - **M1-16:** weapon attacks (W 7.5), `onHit`, `missedBy`, `attackAgainstOther`.
  - **M1-17:** the AR 10.4 AC window across allies, and window fills.
  - **M1-19:** class records, pool records, `Unit.Abilities` from classes.
  - **M1-20:** `onEvent` costs, the Vanguard's `ifKills` option policy.
  - **M1-22, M1-23:** monster records, `uses`, `cooldown` and `readyAt`; reaction filters other than `none` (`nonMinion` needs ranks).
  - **M1-25:** the display, including the spent-action pip.
  - **M1-34:** party knowledge in the preview.
  - **M1-35:** noticing, fight membership, the boss-group refusal.
  - **M2 or later:** riders, concentration, auras, monster-side opener content, DH 20.5's companion and carrying resets.

## Plan-level calls (mine, flagged for review)

1. **The ability record** (`abilities/abilities.json`, kind `AbilitiesKind`, ids are content ids). Every field is in Task 3's interfaces.
   - `kind` parses all six AB 6.1 values but accepts only `atWill`, `limited` and `reaction`.
   - `cost` parses all nine AB 7.1 forms but accepts only `action`, `bonus`, `free`, `move:N` (N ≥ 1), `none` and `reaction`. `attack`, `onHit` and `onEvent:<id>` are load errors naming M1-14, M1-16 and M1-20.
   - `kind: reaction` ⇔ `cost: reaction` ⇔ a `reaction` block; anything else is a load error.
   - A reaction's `policy` may be missing, and `askMe` loads, so that the AB 14.5 check (not the loader) is what fails them.
   - `AbilityDefinition` can also be built in code, because W 20.1's attack ability is derived, never stored.
2. **Who knows an ability.** `Unit.Abilities` holds ability ids, ordinal-sorted and distinct, with the default normalized to empty like `Spent`. M1-19 and M1-22 fill it from records; tests set it directly. A unit may use, and react with, only abilities it holds.
3. **Reason codes are `CommandError` members, named as the greyed-reason ids** (labels may be capitalised, D-12). Each code `CheckUsable` or `CheckTarget` can return gets a `naming/greyed_reasons.json` row (`doc` `"ab"`) and an `ab.greyed.<Id>` string.
   - New codes: `UnknownAbility`, `ReactionOnly`, `NoActionLeft`, `NoBonusActionLeft`, `NoFreeInteraction`, `NoUsesLeft`, `CampSpendUsed`, `InvalidTarget`, `OutOfRange`, `NoLineOfSight`, `NotAReaction`, `AskMeIsPartyOnly`.
   - Existing codes reused: `UnknownUnit`, `NotYourTurn`, `NoMovementLeft`, and `NotInCombat` (a drawing non-opener out of combat, plan call 5).
   - The order is AB 14.3's: user → ability → turn → cost → pools → camp spend → target shape → range → line of sight → `NotInCombat` (decided once the affected units show whether the use is an opener).
4. **Two validation stages, one reason.** `CheckUsable` covers the target-independent part (the action bar); `CheckTarget` calls it first, then the target-dependent part. The command uses `CheckTarget`.
   - **Out of combat**, both stages run on the *view* the command will act on: the tick-reset view (fresh budgets, `turn` pools refilled) for a non-opener, and the step-4 view (fresh budgets, `fight` pools refilled) for an opener (owner ruling 2).
   - **In combat** they run on the stored state. The user must hold a live turn: not `BeforeRound1`, not `TurnVacated`, and `CurrentUnit == user`; otherwise `NotYourTurn`.
   - **Exception:** a reaction-cost ability is never usable by command (`ReactionOnly`).
5. **Openers.** Out of combat, a use is an opener when its affected units include one of another faction. The affected units are computed by Task 4's `Abilities.Affected`, the same function preview and resolution use. Noticing is M1-35's.
   - **Non-opener out of combat:** tick resets first, then the pipeline, then `Tick + 1` and `TickAdvanced` last, as `ApplyMove` does.
   - **A non-opener out of combat may not draw.** An ability with a `resolve` or with dice in its damage is refused with `NotInCombat`, because no stream is ruled for it: drawing `combat` would break V19t, and SV 4.3 names no other. This is recorded as **D-23** (Task 9). Flat-damage and effect-only abilities work, which is enough for DH 10.4's party-area floor. Since reaction windows open only on rolls, no out-of-combat use can suspend on "ask me".
   - **Opener:** no tick at all (owner ruling 2; AB 6.8).
6. **The opener's charge.** `OpenerCharge(TurnBudget Charged, ImmutableArray<PoolSpend> TurnPools)` lives on `Unit.Opener`.
   - `Charged` holds only the opener's own cost bits (action, bonus, free or movement); its `ReactionUsed` and `CampSpent` are never set.
   - `TurnPools` holds the counts the opener spent from `turn` pools, sorted by pool id.
   - **At the striker's first turn start:** the budget becomes `Charged`; `turn` pools refill and then the charged counts are written back; the mark clears. Every unit's `CampSpent` clears, as at every turn start.
   - An opener that costs a bonus action charges the bonus and keeps the action. This is a reading of record 4.1, which assumes an action opener.
7. **Combat start split.** `Turns.BeginCombat(rules, s)` runs steps 0–7:
   - the existing checks; every unit joins in id order;
   - an empty held-messages list (M1-12 and M1-13 fill it);
   - step 4 **before** step 5 (fresh budgets and the `fight` refill, then initiative; draw-neutral);
   - `Combat = new CombatState(order, 0, 1, BeforeRound1: true)`;
   - events `[CombatStarted, .. held]`.

   `Turns.BeginRound1(rules, s)` clears the marker and starts slot 0's turn. `StartCombat` = `BeginCombat` + `BeginRound1`, so its event list and dice stay as they are today.
   - While the marker is set, `Current` is 0 and no unit holds the turn: `LiveTurn` throws, and moves, End Turn and abilities are refused with `NotYourTurn`.
   - `Harm.Remove` of slot 0 under the marker leaves `Current` 0 and does **not** set `TurnVacated`.
   - `EndIfWon` skips its turn-end step under the marker and clears every `Opener` mark.
8. **The settle loop and dispatch phases.** `TriggerDispatch` gains an internal overload with a firing counter shared across one command (`Effects.MaxTriggerFirings` stays per command). It stops before each queued event, before each firing, and before each action within a firing once `state.Ended` is set. With the settle loop in place, a fight with no `Enemy` unit ends at the first accepted command, so test fixtures that need a running fight must include an enemy.
   - **The loop** (`Engine.Settle`) runs after every accepted command's dispatch: while not ended and no decision is pending, run `EndIfWon`; otherwise `BeginRound1` when the marker is set; otherwise `ResumeIfVacated`. Each pass's events are dispatched, and the loop stops when a pass emits nothing.
   - **An opener dispatches in phases:** the combat-start events, then the opener's resolution, then the settle loop. So `combatStarted` triggers see the state before the opener resolves.
9. **Events and rule events.**
   - New events: `AbilityUsed(UnitId User, string AbilityId)`, `AbilityCostPaid(UnitId User, string AbilityId, TurnBudget Charged, ImmutableArray<PoolSpend> Pools)`, `TargetsLocked(UnitId User, ImmutableArray<UnitId> Targets)`, `AttackRolled(UnitId Attacker, UnitId Target, AttackResult Result)`, `SaveRolled(UnitId Target, CheckResult Result)`, `DamageRolled(UnitId Target, DamageRoll Roll)`, `ReactionTaken(UnitId Reactor, string AbilityId)` and `AbilityResolved(UnitId User, string AbilityId)`.
   - Only accepted commands emit them; a refusal emits nothing.
   - One new rule event: `abilityResolved` (subject: the user), which is AB's `ownActionResolved` for M1-20. AB's `combatStart` event kind is the existing `combatStarted`.
10. **Resolution.** For each locked target in unit order, skipping units removed earlier in the same use:
    - **attack:** `PrepareAttack`, `Dice.D20`, the attack-roll window, `ResolveAttack`, `AttackRolled`; on a hit, `Damage.Roll` (crit doubles the dice), `DamageRolled`, `Harm.Deal`, then the effects;
    - **save:** the save DC is the user's `abilityDifficulty`, captured when targets lock. `PrepareSave`, `Dice.D20`, the own-save window (opened only when the user is of another faction from the saving unit: AB 14.3's "against a hostile ability", with hostile read as in plan call 5), `ResolveCheck`, `SaveRolled`; on a failure, damage and effects as above;
    - **no `resolve`:** damage and effects with no roll.
    - Effects are applied with `Effects.Apply(..., new EffectSource(user))` and emit `EffectApplied`.
    - When `Ended` is set, resolution stops at once; `RunEnded` stays the last event, with no `AbilityResolved`. If the user itself is removed, the remaining targets are skipped and `AbilityResolved` still closes the use. This branch is defensive: M1-11 cannot reach it, because no reaction deals damage and triggers run only after resolution. No test is owed until M1-14 or M1-16 adds attacking reactions.
    - Zero affected units is a legal use: the cost is paid, then `TargetsLocked([])` and `AbilityResolved`.
11. **Reactions in M1-11 are bonus wards**, because no attacking reaction exists until M1-14 and M1-16. A reaction record names `trigger` (`attackRoll` or `ownSave`) and a `bonus`.
    - **Candidates** at a window are the defender's own reaction abilities with that trigger, in ability-id order. Allies' wards (AR 10.4) are M1-17's.
    - **Legality comes first**, before any policy or prompt: the reaction must be unused, its pool affordable and the camp rule satisfied. An illegal candidate is skipped, not offered and not spent.
    - **The policy**, the unit's override if it has one, else the data default:
      - `always` fires;
      - `never` does not;
      - `whilePoolAtLeastHalf` fires while pool current ≥ `IntMath.FloorDiv(max + 1, 2)`;
      - `ifItChangesOutcome` fires only if the bonus turns a hit into a miss (`Target + bonus`) or a failed save into a success (`Bonus + bonus`), judged on the roll already drawn;
      - `askMe` suspends.
    - **Firing** pays the reaction (`ReactionUsed`, and the pool spend if any, through `Pools.Spend`, which sets `CampSpent` for a camp pool), emits `ReactionTaken`, adds the bonus to the setup and closes the window.
    - Nesting is impossible by construction: a ward resolves no pipeline.
12. **"Ask me".**
    - `DecisionKind.Reaction`; the decision's `Unit` is the reactor, `Options` are `[0 = decline, 1 = take]`, and `Source` is the reactor's square.
    - The suspended use is a new `GameState.Resolution` (`AbilityFrame`). It is saved, digested and validated: a frame exists exactly when a `Reaction` decision is pending, and only in combat (plan call 5: out of combat, nothing can suspend).
    - The frame names the candidate being asked by its **ability id**. Loading checks the frame's structure only, and needs no rules. `Resolution.Continue` rebuilds the candidate list from the rules and throws `InvalidOperationException` on a mismatch.
    - **Answering** draws nothing: take fires the reaction; decline moves on to the next candidate by id, which may ask again. Then resolution continues from the frame, and the settle loop runs, which starts round 1 for a suspended opener. The answer takes no tick.
13. **`SetReactionPolicyCommand(UnitId Unit, string AbilityId, ReactionMode Mode)`** stores an override in `Unit.Reactions` (`ReactionSetting(string AbilityId, ReactionMode Mode)`, sorted by id; setting a value replaces it).
    - It takes no tick and no budget, and works in or out of combat.
    - It is refused for an unknown unit, an ability the unit lacks, a non-reaction ability (`NotAReaction`), and `AskMe` on a non-`Party` unit (`AskMeIsPartyOnly`).
14. **Preview.** `Abilities.Preview(rules, s, user, abilityId, target) -> AbilityPreview` draws nothing and returns the same state.
    - It returns `CheckTarget`'s code, the area squares and the affected units in lock order.
    - Per target it gives the `ContestSetup`, the chance and the expected damage:
      - **Chance:** the number of d20 faces (or face pairs, under advantage or disadvantage) for which `ResolveAttack` hits, or `ResolveCheck` fails the save, over 20 or 400, as a `Rational`.
      - **Expected damage:** the exact mean of `DamageOutcome.Total` before the DH HP floors (`ClampReason`). The total already includes the ladder's minimum of 1 per part. Each part's dice-sum distribution goes through the same per-part ladder step `Damage.Resolve` uses (extracted as `Damage.DealtByPart`), weighted by the normal-hit and crit chances.
    - Reactions are left out. Out of combat, the preview reads the view of call 4.
15. **Geometry.** `Sight.CanSee` is the conjunction of two symmetric conditions, so it is symmetric:
    - (a) Ford's integer symmetric shadowcasting from `a` lights `b`;
    - (b) the segment from `a`'s centre to `b`'s centre passes through no square corner whose two orthogonal neighbours across the crossing are both walls.

    Lines use the *movement* corner rule: a diagonal step with a wall on either orthogonal stops the line (AB 8.1 puts TR-5 under lines). Lines are then filtered by sight like every area.
16. **Versions.** Save version 8 carries more than the roadmap's "marker and mark": also `Unit.Abilities`, `Unit.Reactions` and the frame. `RulesVersion` 3. One bump each, in Task 1; later tasks add fields under v8 before the step merges.

## Review Focus

1. **A kill partway through, and a turn holder killed after its own use:**
   - An area kills its first target; the remaining targets still roll and `AbilityResolved` closes the use (Task 4).
   - A trigger on `abilityResolved` kills the user, who holds the turn. The trigger's `DamageDealt` and `UnitDied` come after the ability's events, and the settle loop's `TurnStarted` for the next slot comes last, never inside them (Task 4).
   - An opener area that kills slot 0 still resolves its later targets before `BeginRound1` starts the next unit (Task 6).
2. **The hero is hit by an area before later targets:** the hero dies on target 2 of 3 (an enemy's burst in combat). Target 3 takes no roll, `RunEnded` is the last event, there is no `AbilityResolved` and nothing settles (Task 4).
3. **Every refusal is free:** for a refused opener, a refused non-opener out of combat and a refused in-combat use, the state is the same instance, there are no events, and `Tick` and `Rng.Combat.Counter` are unchanged (Tasks 4 and 6).
4. **Save and load across "ask me":** a state suspended on a reaction decision round-trips byte for byte. Answering the loaded copy gives the same final save as answering the original, and `Replay.Run` of the full log reproduces it (Task 8).
5. **Edges and walls:** a burst whose point is at the map corner covers only in-bounds squares; a line aimed straight into an adjacent wall covers nothing; both are legal uses that resolve no target (Task 4).

---

### Task 1: Combat start split, the marker, the opener mark, save version 8

**Files:**
- Modify: `src/Core/Combat/CombatState.cs` (`BeforeRound1`), `src/Core/Combat/TurnBudget.cs` (`OpenerCharge`), `src/Core/Combat/Turns.cs`, `src/Core/Combat/Harm.cs`, `src/Core/Combat/CombatEnd.cs`, `src/Core/Combat/Pools.cs` (a refill that writes back charged counts), `src/Core/State/GameState.cs` (`Unit.Opener`, validation), `src/Core/Engine/Engine.cs` (marker refusals), `src/Core/Persistence/Snapshot.cs` (v8), `src/Core/State/StateDigest.cs`, `src/Core/Run/Versions.cs` (`RulesVersion` 3)
- Modify tests: the `"version":7` literals (`PersistenceTests`, `HitPointStateTests`, `RunStateTests`, `TurnTests`; the "future version" becomes 9), `RunConfigTests` (rules version 3), `StateDigestTests`
- Test: `tests/Core.Tests/CombatStartTests.cs`

**Interfaces:**
- `CombatState(ImmutableArray<UnitId> Order, int Current, long Round, bool TurnVacated = false, bool BeforeRound1 = false)`. Valid only with `Round == 1`, `Current == 0`, `!TurnVacated` and a non-empty order.
- `public sealed record OpenerCharge(TurnBudget Charged, ImmutableArray<PoolSpend> TurnPools)` (plan call 6). `Unit` gains `OpenerCharge? Opener = null` after `Spent`.
- `Turns.BeginCombat(RulesContext, GameState) -> (ImmutableArray<GameEvent>, GameState)`, `Turns.BeginRound1(RulesContext, GameState) -> (ImmutableArray<GameEvent>, GameState)`. `Turns.StartCombat` composes them. `BeginRound1` throws unless in combat, not ended and the marker is set.
- `internal static GameState Turns.MarkOpener(GameState s, UnitId striker, OpenerCharge charge)`: requires the marker and throws unless the striker's budget already includes `Charged` and its spent counts include `TurnPools`. It sets only the mark: the cost itself is paid by the pipeline (Task 6) or, in this task's tests, by `with` and `Pools.Spend`.
- `GameState` validation adds:
  - the marker rules above;
  - marker ⇒ `Pending` is not null or `Ended` is not null;
  - at most one marked unit per `Faction`;
  - a mark ⇒ in combat, `Round == 1`, the unit is in the order, and its round-1 turn has not started (`BeforeRound1`, or its index > `Current`, or index == `Current` with `TurnVacated`);
  - the budget includes `Charged`;
  - `TurnPools` is sorted and distinct, with counts ≥ 1.
- Snapshot v8: `CombatDto.beforeRound1` (required), `UnitDto.opener` (required, null when absent). Digest: the combat record grows to 5 fields, and the unit record gains the opener as an optional record.

- [ ] **Step 1: Write the failing tests** in `CombatStartTests`. The file builds its own rules: `PoolTests.FixturePools` plus `new PoolDefinition("pool.turn2", RefillTag.Turn, …max 1)`. Leave `FixturePools` itself unchanged, because `PoolTests` asserts its exact id list. Every fight fixture includes an `Enemy` unit.
  - `StartCombat_keeps_its_events_and_dice`: `[CombatStarted, TurnStarted]` and the same `Rng.Combat.Counter` as on main for a three-unit fight. `CombatScenarioTests`' `Seed7Final` stays green.
  - `BeginCombat_leaves_round_one_unstarted`: the marker is set, `Current` 0, no `TurnStarted`, every budget fresh and the `fight` pools refilled, exactly N combat draws for N units.
  - `Fresh_budgets_come_before_initiative`: a `fight` pool spent before the fight is refilled.
  - `Under_the_marker_moves_end_turn_and_StartTurn_are_refused`: on a state built with `with`, `MoveCommand` and `EndTurnCommand` give `NotYourTurn`, and `Turns.Advance` throws.
  - `The_first_turn_rule` (record tests 2–4, driven through `MarkOpener` after paying by hand). The charge is (`ActionsUsed` 1) plus `pool.turn` spent 1, and the striker also spent `pool.camp` and 1 movement.
    - At the striker's first `TurnStarted` (striker in slot 0, and again in slot 2): `ActionsUsed` 1, `MovementUsed` 0, `BonusUsed` false, `pool.turn` spent 1, `CampSpent` false, `Opener` null.
    - The camp flag clears at slot 0's turn start even when the striker is in slot 2.
  - `A_reaction_spend_between_the_opener_and_the_first_turn_still_refills`: `pool.turn2` is spent 1 after `MarkOpener` and before `BeginRound1`. At the striker's turn start, `pool.turn2` is spent 0 and the charged `pool.turn` is still 1.
  - `Removing_slot_zero_before_round_one_promotes_the_next_unit`: `Harm.Deal` kills slot 0 under the marker. `TurnVacated` stays false; `BeginRound1` starts the old slot 1 in round 1.
  - `A_win_before_round_one_emits_no_turn_events`: the only enemy is killed under the marker. `EndIfWon` emits only `CombatEnded(Win)`, and every `Opener` is null.
  - `Snapshot_v8_round_trips_the_marker_and_the_mark`, `A_v7_save_is_refused`, `Bad_markers_and_marks_are_format_errors`. The format errors cover: marker with round 2; marker with `TurnVacated`; marker with no pending decision and no run end; two marks on one faction; a mark on a unit whose turn already started; a mark whose budget lacks the charge.
  - In `StateDigestTests`: `BeforeRound1` and each `OpenerCharge` field reach the digest.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "CombatStartTests|StateDigestTests|PersistenceTests"`. Expected: build fails.
- [ ] **Step 3: Implement** plan calls 6 and 7. `Snapshot.CurrentVersion` = 8 (doc line "v8 (M1-11) adds the before-round-1 marker and the opener mark; later M1-11 tasks add abilities, reaction settings and the resolution frame"). `RulesVersion.Current` = 3 (doc line "abilities run through UseAbility; combat start holds round 1 for an opener; an opener is charged to its striker's round-1 turn and takes no tick"). Pools stay the only writer of `Spent`.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln` and `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`. Expected: all pass, 0 warnings.
- [ ] **Step 5: Commit** `M1-11: combat start holds round 1; the opener mark and the first-turn rule; save version 8, rules version 3`.

### Task 2: Sight and area geometry

**Files:**
- Create: `src/Core/Grid/Sight.cs`, `src/Core/Grid/Areas.cs`
- Test: `tests/Core.Tests/SightTests.cs`, `tests/Core.Tests/AreaTests.cs`

**Interfaces:**
- `public static class Sight { public static bool CanSee(GridMap map, GridPos a, GridPos b); }`. It returns false if either square is out of bounds or a wall, true if `a == b`, and otherwise plan call 15. Integer arithmetic only (`IntMath`).
- `public abstract record AreaShape`; `public sealed record Burst(int Radius) : AreaShape`, `Cone(int Length)`, `Line(int Length)`. Each size is ≥ 1.
- `public static class Areas`:
  - `int ShapeSize(AreaShape shape)`: (2r+1)², (L+1)² − 1 and L.
  - `ImmutableArray<GridPos> ShapeSquares(AreaShape shape, GridPos origin, Direction? aim)`: the geometric set, generated in row-major order (Y ascending, then X), with no sort and no hash set. A burst takes `aim` null; cones and lines need it.
  - `ImmutableArray<GridPos> Covered(GridMap map, AreaShape shape, GridPos origin, Direction? aim)`: the shape squares that are in bounds, not walls and seen from the origin. A line is first cut at the first wall or blocked diagonal step (plan call 15).

- [ ] **Step 1: Write the failing tests.** Unless a test says otherwise, the `Sight` and `Covered` tests run on an open 13×13 map, and every listed coordinate is an **offset from O = (6,6)**: the user stands on O, and a wall "at (1,0)" is at (7,6). The pure `ShapeSquares` tests keep raw coordinates around (0,0), since they read no map. `Covered_clips_to_the_map` puts its origin at the corner (0,0) on purpose; r1 there covers (0,0), (1,0), (0,1), (1,1). Every negative sight or line assertion also has a control: with one of the two walls removed, the same target is visible or the line is not empty.
  - `SightTests`:
    - `Walls_block_and_units_do_not`;
    - `Sight_glances_past_one_corner`: a wall at (1,0) only, so (0,0) sees (1,−1);
    - `A_diagonal_crack_blocks_sight`: walls at (1,0) and (0,−1); from (0,0), (1,−1), (2,−2) and (3,−3) are not visible;
    - `A_crack_off_the_diagonal_blocks_a_ray_through_its_corner`: walls at (1,1) and (0,2); (0,0) does not see (1,3), whose centre line passes the corner (0.5, 1.5) between them;
    - `CanSee_is_symmetric`: 200 seeded random 12×12 maps with 20% walls, all pairs of floor squares;
    - `Walls_see_nothing`.
  - `AreaTests`:
    - `Cones_in_all_8_directions`: a theory over `DirectionExt.All` × L ∈ {1, 2, 3, 4}. Size is (L+1)² − 1, and the origin is never included. For each case, filter the (2L+1)² box around the origin, in row-major order, with the membership rule, and assert the result is exactly `ShapeSquares`. Let (dx, dy) = square − origin and (ux, uy) = the direction's offset:
      - orthogonal: f = dx·ux + dy·uy and s = dx·uy − dy·ux; a member iff 1 ≤ f ≤ L and |s| ≤ f;
      - diagonal: (dx = 0 or sign(dx) = ux), (dy = 0 or sign(dy) = uy), max(|dx|, |dy|) ≤ L, and not the origin.

      Check the rule itself against these literal L2 sets: E = (2,−2); (1,−1), (2,−1); (1,0), (2,0); (1,1), (2,1); (2,2). N = (−2,−2) … (2,−2); (−1,−1), (0,−1), (1,−1). SE = (1,0), (2,0); (0,1), (1,1), (2,1); (0,2), (1,2), (2,2). NW = (−2,−2), (−1,−2), (0,−2); (−2,−1), (−1,−1), (0,−1); (−2,0), (−1,0).
    - `Bursts_in_all_8_directions`: a theory over `DirectionExt.All` × r ∈ {1, 2}. The burst point is origin + 2 × offset on an open map; the result is the exact (2r+1)² block in row-major order.
    - `Lines_stop_at_the_first_wall`: L4 E is (1,0) … (4,0); with a wall at (3,0) it is (1,0), (2,0).
    - `A_diagonal_line_stops_at_a_corner`: L3 NE with a wall at (1,0) covers nothing.
    - `A_burst_behind_a_wall_is_cut_by_origin_sight`: r2 at (0,0) with a wall at (1,0) drops exactly (1,0) and (2,0), giving 23 squares.
    - `Covered_clips_to_the_map`.
    - `ShapeSize_matches_ShapeSquares` for r 1–3 and L 1–6.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "SightTests|AreaTests"`. Expected: build fails.
- [ ] **Step 3: Implement.** Write the crack tests green before the symmetry property. If (a) ∧ (b) fails `CanSee_is_symmetric`, stop and report BLOCKED: XI says to verify Ford's conventions before building, and no non-symmetric rule may ship.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass, analyzers green.
- [ ] **Step 5: Commit** `M1-11: symmetric CanSee with the corner rule; burst, cone and line squares in row order`.

### Task 3: Ability records and the two AB 14.5 checks

**Files:**
- Create: `src/Core/Abilities/AbilityDefinition.cs`, `src/Core/Abilities/AbilitiesKind.cs`, `src/Core/Validation/Checks/ReactionHasPolicyCheck.cs`, `src/Core/Validation/Checks/AreaCapByLevelCheck.cs`, `content/abilities/abilities.json` (`{"abilities": []}`)
- Modify: `src/Core/Content/ContentKind.cs` (register the kind), `src/Core/Effects/RulesContext.cs` (`Abilities`, `Ability(id)`, reference checks), `src/Core/Validation/StaticChecks.cs`, `content/registry/checks.json`, `content/README.md`, `src/Core/Content/ContentHashes.g.cs` (regenerated)
- Test: `tests/Core.Tests/AbilityRecordTests.cs`, `tests/Core.Tests/AbilityChecksTests.cs`

**Interfaces (namespace `Dungeon.Core.Abilities`):**
- `enum AbilityKind { AtWill, Limited, Passive, Toggle, Sustained, Reaction }`; `enum CostKind { Action, Bonus, Reaction, Attack, Move, Free, OnHit, OnEvent, None }`; `readonly record struct AbilityCost(CostKind Kind, int Move = 0, string? Event = null)`.
- `sealed record PoolUse(string PoolId, int Count)` (count ≥ 1).
- `enum TargetShape { Single, Burst, Cone, Line }`; `sealed record TargetSpec(TargetShape Shape, int Range, int Count, int Size, bool Selective)`. `Size` is the radius or length; `Count` applies only to `Single`; `Range` applies to `Single` and `Burst`, and burst range 0 means centred on the user.
- `abstract record ResolveSpec`; `sealed record AttackResolve(AttackSpec Attack)`; `sealed record SaveResolve(Save Save)` (success = no effect, owner ruling 4).
- `enum ReactionMode { Always, IfItChangesOutcome, WhilePoolAtLeastHalf, Never, AskMe }`; `enum ReactionTrigger { AttackRoll, OwnSave }`; `sealed record ReactionSpec(ReactionTrigger Trigger, int Bonus, ReactionMode? Policy)`.
- `sealed record AbilityDefinition(string File, string Id, AbilityKind Kind, AbilityCost Cost, PoolUse? Pool, TargetSpec? Target, ResolveSpec? Resolve, ImmutableArray<DamagePart> Damage, ImmutableArray<string> Effects, ReactionSpec? Reaction) : ContentRecord`, with `WriteCanonical`. `Target` is required unless the kind is `Reaction`, and must be null for one.
- **JSON** (strict, lower-camel enum names):
  - `{"id","kind","cost","pool":{"id","count"},"target":{"shape","range","count","radius"|"length","selective"},"resolve":{"attack":"strength","trained":true}|{"save":"reflex","onSuccess":"none"},"damage":[{"type","dice":"2d6","flat"}],"effects":[ids],"reaction":{"trigger","bonus","policy":{"kind","filter":"none"}}}`.
  - A filter other than `none` is a load error (M1-22). `onSuccess: half` is a load error citing D-22.
- `RulesContext.Create` builds `ImmutableArray<AbilityDefinition> Abilities` (sorted by ordinal id) and exposes `AbilityDefinition Ability(string id)`, which throws `ArgumentException` for an unknown id. Unknown pool or effect references are refused in `Validate`, naming the file and path. An optional `abilities` parameter takes code-built definitions (merged with content; duplicate ids refused).
- `public static int AreaCap(int level)`: 25 for 1–6, 49 for 7–10; throws otherwise.
- **The checks** (registered, with `checks.json` rows: owner "ability model", phase `s`, class `build`, wave `M1`, ciTier `pr`):
  - `content.ABL145.ReactionHasPolicy` fails a reaction record whose policy is missing or `askMe`, `whilePoolAtLeastHalf` with no `pool`, and `ifItChangesOutcome` with a zero bonus.
  - `content.ABL145.AreaCapByLevel` fails an area record whose `ShapeSize` exceeds `AreaCap(1)`. Every M1 record counts as level 1–6; M1-19 and M1-22 supply real levels.

- [ ] **Step 1: Write the failing tests:**
  - **`AbilityRecordTests` (inline `ContentFile`s):**
    - each accepted cost, kind and shape loads;
    - each reserved cost, kind and `half` is refused with a path;
    - `reaction` kind, cost and block must agree;
    - a missing policy and `askMe` load;
    - unknown pool or effect ids are refused by `RulesContext.Create`;
    - a code-built definition merges;
    - the canonical form is stable.
  - **`AbilityChecksTests`:**
    - `A_reaction_without_an_automatic_policy_fails` (missing; `askMe`), `Never_passes`;
    - `An_area_over_25_squares_fails`: burst r3 (49), cone L5 (35) and line L26 fail; burst r2, cone L4 and line L25 pass;
    - `AreaCap_is_25_to_level_6_and_49_from_7`.
  - **`RepositoryContentTests`** stays green (code ids equal the phase-`s` rows).
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "AbilityRecordTests|AbilityChecksTests"`. Expected: build fails.
- [ ] **Step 3: Implement**, copying `EffectsKind`'s style (`ContentJson`, `Need`, `Item`, `Member`). Then run `dotnet run --project src/Sim -- content-hashes`.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass, and `ContentHashTests` shows no drift.
- [ ] **Step 5: Commit** `M1-11: ability records; ReactionHasPolicy and AreaCapByLevel checks`.

### Task 4: The UseAbility pipeline (in combat, and out of combat without a hostile)

**Files:**
- Create: `src/Core/Abilities/AbilityTarget.cs`, `src/Core/Abilities/Abilities.cs` (`Affected`, `CheckUsable`, `CheckTarget`), `src/Core/Abilities/Resolution.cs`
- Modify: `src/Core/Engine/Commands.cs`, `Events.cs`, `Engine.cs` (`UseAbilityCommand`, the new `CommandError` members, the settle loop), `src/Core/Effects/TriggerDispatch.cs` (shared counter; stop on `Ended`), `src/Core/Effects/Registries.cs` (`abilityResolved`), `src/Core/Combat/Pools.cs` (`CheckSpend`), `src/Core/Damage/DamageFunction.cs` (extract `DealtByPart`), `src/Core/State/GameState.cs` (`Unit.Abilities`), `Snapshot.cs`, `StateDigest.cs`, `src/Core/Persistence/CommandLog.cs` (`"useAbility"`), `content/naming/greyed_reasons.json`, `content/strings/en.json`, `ContentHashes.g.cs`, `tests/Core.Tests/EffectsKindTests.cs` (the rule-event list)
- Modify tests: `TurnTests.cs` (`Open()` makes unit 3 an `Enemy`, `EnemyStats`, HP 12, keeping three units and seed 7 so its counter and order assertions hold). Before Step 4, grep `tests/Core.Tests` for other running-fight fixtures with no `Enemy` unit (`Turns.StartCombat` callers, `CombatState` built with `with`), and fix them the same way (plan call 8).
- Create: `tests/Core.Tests/AbilityFixtures.cs`, a shared `public static` fixture set (definitions and `Rules`) used by Tasks 4–8 and `CombatScenarioTests`
- Test: `tests/Core.Tests/UseAbilityTests.cs`, `tests/Core.Tests/SettleTests.cs`

**Interfaces:**
- `public abstract record AbilityTarget`; `sealed record UnitsTarget(ImmutableArray<UnitId> Units)`, `PointTarget(GridPos Point)`, `AimTarget(Direction Aim)`.
- `public sealed record UseAbilityCommand(UnitId User, string AbilityId, AbilityTarget Target) : Command`. Its CommandLog tag is `"useAbility"`, with fields `ability`, `targets`, `point {x,y}` and `dir`; each tag rejects foreign fields.
- `Abilities.Affected(RulesContext, GameState, UnitId user, AbilityDefinition, AbilityTarget) -> (ImmutableArray<GridPos> Squares, ImmutableArray<UnitId> Units)`:
  - single: the listed units;
  - areas: the units on `Covered` squares, minus the user, minus same-faction units when `Selective` or `AtWill`;
  - units are returned in id order.
- `Abilities.CheckUsable(RulesContext, GameState, UnitId user, string abilityId) -> CommandError` and `Abilities.CheckTarget(..., AbilityTarget) -> CommandError` (plan calls 3 and 4).
  - **Target shape:** `UnitsTarget` only for `Single`, with 1..`Count` distinct living units that are not the user; `PointTarget` only for `Burst`, in bounds and not a wall; `AimTarget` only for `Cone` and `Line`. Anything else is `InvalidTarget`.
  - **Range** is Chebyshev from the user to each unit or to the point; **sight** is `CanSee` from the user's square.
- `Pools.CheckSpend(rules, s, unit, poolId, count) -> CommandError` (`None`, `NoUsesLeft`, `CampSpendUsed`); `Spend` uses it.
- `Damage.DealtByPart(RulesContext, GameState, UnitId target, DamagePart part, long raw) -> long`, called by `Resolve` (no behaviour change).
- `internal static class Resolution`: `Use(...)` pays the cost, locks targets and resolves (plan call 10), returning `(ImmutableArray<GameEvent>, GameState)`.
- `Unit` gains `ImmutableArray<string> Abilities = default` (plan call 2), saved, digested and validated (sorted, distinct).

- [ ] **Step 1: Write the failing tests.** `AbilityFixtures` holds:
  - a melee strike (`single`, range 1, attack strength, 1d6);
  - a sure strike (`single`, range 1, no `resolve`, damage 1d4+1, so it always deals at least 2);
  - a `bonus` jab and a `move:2` dash-strike (both `single`, range 1, no `resolve`, flat 1);
  - a `free` poke;
  - a camp-pool burst (`burst`, range 2, radius 1, no `resolve`, flat 3);
  - a save burst (`burst`, range 6, radius 1, save reflex, 1d6);
  - a `selective` save burst;
  - an at-will cone (L2, save reflex, 1d4);
  - a line (L4, save reflex, 1d4).
  - **`Each_reason_code_in_order`:** one case per code. Each case passes every earlier check, fails its own, and also fails as many later checks as the state allows, and the expected result is the earliest code. For example, the `NoActionLeft` case also has an empty pool, the camp flag set, and a target out of range and out of sight. Then `No_action_and_out_of_range_reports_no_action`, and `CheckUsable` returns `None` where `CheckTarget` returns `OutOfRange`.
  - **`Every_validation_code_has_a_greyed_reason`:** every code either stage can return has a row and a string.
  - **`An_attack_ability_in_combat`:**
    - events `AbilityUsed, AbilityCostPaid, TargetsLocked, AttackRolled, DamageRolled, DamageDealt, AbilityResolved`;
    - the action is spent;
    - the roll and damage match `Contests` and `Damage` run directly on the same stream.
  - **`A_save_area_locks_targets_in_unit_order`.**
  - **`Selective_and_at_will_areas_spare_allies` and `The_user_is_never_in_its_own_area`.**
  - **`A_second_camp_spend_in_the_same_turn_is_refused`** (AB 16, "including reactions"): in combat, on the user's own live turn, a direct `Pools.Spend` of `pool.camp` stands for a camp-paid reaction taken earlier in that turn. Its camp-paid burst in the same turn, with action and pool uses still available, gets `CampSpendUsed`, the same state and no events. After `Turns.Advance` brings its next turn, the same use is accepted. (A camp spend on another unit's turn is cleared by the user's next turn start, so it can never block the user's own ability; the reaction side is Task 7's.)
  - **`A_party_area_out_of_combat_with_no_hostile_starts_nothing`** (DH 10.4): the hero uses the flat camp-pool burst on a second party unit on 1 HP. Expected: no `CombatStarted`, a `DamageClamped(OutOfCombat)`, HP 1, `Tick` + 1 with `TickAdvanced` last, `Rng.Combat.Counter` unchanged, and the hero's budget reset before validation.
  - **`A_drawing_non_opener_out_of_combat_is_refused`** (D-23): the save burst on an ally out of combat gets `NotInCombat`, the same state and no events.
  - **Review Focus 1:** `An_area_kill_partway_through_still_resolves_the_rest` (a burst kills target 1 of 3; targets 2 and 3 still roll). In `SettleTests`, `A_trigger_that_kills_the_user_after_its_ability_settles_the_turn`: an `abilityResolved` trigger's hook kills the user, who holds the turn. The ability's events come first, up to `AbilityResolved`, then the trigger's `DamageDealt` and `UnitDied`, then `TurnStarted` for the next slot.
  - **Review Focus 2:** `The_hero_dying_mid_area_stops_resolution`.
  - **Review Focus 3, part:** `Refusals_cost_nothing` for the in-combat case and the out-of-combat non-opener.
  - **Review Focus 5:** `Edge_and_wall_areas_resolve_no_target`.
  - **`A_unit_cannot_use_an_ability_it_does_not_hold`** (`UnknownAbility`).
  - **`The_use_replays`:** `CommandLog` round-trip and `Replay.Run` give the same final save.
  - **`SettleTests`:**
    - `Dispatch_stops_at_the_run_end`: two `unitMoved` triggers; the first's hook kills the hero; the second does not fire, and `RunEnded` is last. A sibling case uses one trigger with two actions, a killing hook and then an `applyEffect`: there is no `EffectApplied` after `RunEnded`, and nothing throws.
    - `A_kill_by_trigger_on_a_move_settles`: an enemy killed by a trigger during a move ends the fight (`CombatEnded`), through the settle loop.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "UseAbilityTests|SettleTests"`. Expected: build fails.
- [ ] **Step 3: Implement** plan calls 2–5 (the non-opener half), 8, 9 and 10. In this task an out-of-combat use whose affected units include another faction is refused with `InvalidTarget`; Task 6 replaces that branch. The new `CommandError` members (plan call 3) get their greyed-reason rows here. Add `abilityResolved` to `RuleEvents.Core` in ordinal order, and to effects-vocabulary section 6.4 in Task 9. Regenerate the content hashes.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln` and the Godot build. Expected: all pass, 0 warnings.
- [ ] **Step 5: Commit** `M1-11: the UseAbility pipeline, reason codes and the settle loop; dispatch stops at a run end`.

### Task 5: The preview

**Files:**
- Create: `src/Core/Abilities/Preview.cs`
- Test: `tests/Core.Tests/PreviewTests.cs`

**Interfaces:**
- `public enum ContestKind { Attack, Save, Automatic }`.
- `public sealed record TargetPreview(UnitId Target, ContestKind Kind, ContestSetup? Setup, Rational Chance, ImmutableArray<DamagePart> Parts, Rational ExpectedDamage)`. For an attack, `Chance` is P(hit); for a save, P(the save fails); for automatic, 1.
- `public sealed record AbilityPreview(CommandError Error, ImmutableArray<GridPos> Squares, ImmutableArray<UnitId> Affected, ImmutableArray<TargetPreview> Targets)`.
- `Abilities.Preview(RulesContext, GameState, UnitId user, string abilityId, AbilityTarget target) -> AbilityPreview` (plan call 14). The setups come from the same `Contests.Prepare*` calls resolution makes, and the affected units from `Abilities.Affected`.

- [ ] **Step 1: Write the failing tests:**
  - `Preview_draws_nothing`: the same `Snapshot.Save` before and after, and the same `Rng.Combat.Counter`.
  - `Hit_chance_counts_faces`: bonus +3 against AC 13 is 11/20; advantage is (400 − 9²)/400 = 319/400.
  - `Expected_damage_is_exact`: a 1d6+1 attack whose setup hits on 11–20 (chance 10/20, crit 1/20) gives exactly 97/40 = (9/20)·(7/2) + (1/20)·8. Against a target resistant to its type it gives 87/80, because each roll is halved and rounded down before averaging (normal mean 2, crit mean 15/4). 87/80 is not half of 97/40.
  - `Preview_equals_resolution` (AB 16 property): seeds 1–200, in combat, reactions off, over single, burst, cone and line fixtures on random small maps.
    - The preview's `Affected` equals `TargetsLocked`.
    - Each target's `Setup` equals the one rebuilt from its `AttackRolled`/`SaveRolled` event's bonus and target.
    - `ResolveAttack`/`ResolveCheck` applied to the preview's setup and the event's roll equals the event's result.
    - `Damage.Resolve` on the pre-command state, applied to the `DamageRolled` roll, gives that target's `DamageDealt` total.
    - A refused use previews the same `Error`.
    - The preview's `Parts` equal the parts in that target's `DamageRolled`.
  - `Preview_odds_are_exact` (same generator, fixture dice at most 2d6 per part, and covering a resistant target, a weak target, advantage and disadvantage). For each previewed target:
    - counting the d20 faces (or 400 face pairs) for which `ResolveAttack`/`ResolveCheck` on the preview's `Setup` hits or fails the save equals `Chance`;
    - enumerating every dice outcome for the normal and crit cases, building each `DamageRoll`, calling `Damage.Resolve` on the pre-command state and weighting by the face-derived chances equals `ExpectedDamage` exactly.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter PreviewTests`. Expected: build fails.
- [ ] **Step 3: Implement.** Dice-sum distributions are exact integer convolutions per part; there are no floats.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-11: a draw-free preview sharing the resolution code`.

### Task 6: Openers

**Files:**
- Modify: `src/Core/Engine/Engine.cs` (the opener branch, phased dispatch), `src/Core/Abilities/Resolution.cs` (charge and mark), `tests/Core.Tests/CombatScenarioTests.cs` (the test at lines 76–92 and a setup helper for it)
- Test: `tests/Core.Tests/OpenerTests.cs`

**Interfaces:**
- The opener branch of `UseAbilityCommand` (plan calls 4–8):
  1. validate on the step-4 view (a refusal is free);
  2. `BeginCombat`, then dispatch. The opener goes on only if `Ended` is still null and the striker is still in `Units` and can still pay the step-4 cost. If the run ended, return the events so far (`RunEnded` last; no `AbilityUsed`, no charge, no settle). If the striker is gone or can no longer pay, drop the opener (no `AbilityUsed`, cost or mark) and go to step 5. Targets removed meanwhile are skipped by plan call 10;
  3. pay the cost against the step-4 budget, then `MarkOpener` with what was paid (`Charged`, plus the `turn`-pool spends), then lock targets and resolve;
  4. dispatch;
  5. settle (`EndIfWon`, then `BeginRound1`).

  There is no tick and no tick reset. The expected event order is `CombatStarted, [held], AbilityUsed, AbilityCostPaid, TargetsLocked, …, AbilityResolved, then CombatEnded | TurnStarted(slot 0, 1)`.

- [ ] **Step 1: Write the failing tests**, the record's 6.3 M1-11 list:
  - `The_opener_spends_the_action_wherever_the_striker_rolls`: two seeds, striker in slot 0 and in a later slot. After the command, `ActionsUsed` 1. At its `TurnStarted`, still 1, `BonusUsed` false, `MovementUsed` 0 and `MovementLeft` = speed, and a bonus-cost jab is accepted on that turn (tests 1 and 4).
  - `A_bonus_opener_charges_the_bonus_and_keeps_the_action` and `A_move_opener_charges_its_movement` (plan call 6's reading): at the striker's `TurnStarted`, `BonusUsed` true and `ActionsUsed` 0; and `MovementUsed` 2 and `ActionsUsed` 0.
  - `A_once_per_round_spend_survives_to_the_first_turn` (test 2).
  - `Once_per_turn_flags_clear_when_slot_zero_starts`: the opener spends `pool.camp` (test 3).
  - `Opener_messages_come_between_CombatStarted_and_the_first_turn` (test 5).
  - `Golden_N_initiative_dice_then_the_opener_roll`: `Rng.Combat.Counter` after `CombatStarted` = N, and the `AttackRolled` d20 equals `Dice.D20` on the combat stream advanced N times (test 6).
  - `An_opener_that_kills_slot_zero_starts_the_next_unit` (test 7, Review Focus 1): an opener burst kills slot 0 as its first target; a later target is still rolled, and `TurnStarted` for the next unit comes after `AbilityResolved`.
  - `An_opener_that_wins_emits_no_turn_events` (test 8).
  - `One_opener_per_side`: after the opener, a second hostile ability by the striker's side is an ordinary in-combat use (refused off-turn, charged on its turn, no new mark), plus the state check from Task 1 (test 10).
  - `A_refused_opener_starts_no_fight`: out of range. Same instance, no events, `Combat` null, `Tick` and `Rng.Combat.Counter` unchanged (test 11, Review Focus 3).
  - `The_opener_takes_no_tick`: `Tick` unchanged, no `TickAdvanced`, the charge present after the command, and a `turn` pool spent by the previous command still spent (test 12, owner ruling 2).
  - `Validated_against_a_fresh_budget`: a hero who used their action last command can still open (owner ruling 2).
  - `combatStarted_triggers_see_the_state_before_the_opener`.
  - `A_combatStarted_hook_that_kills_the_hero_stops_the_opener` (`RunEnded` last, no `AbilityUsed`) and `A_combatStarted_hook_that_removes_the_striker_drops_the_opener` (no `AbilityUsed`; round 1 starts). These test robustness: no production hook reaches this path.
  - **Reshape** `An_opening_ambush_strike_is_in_combat_damage` (owner ruling 3). It gets its own setup instead of `Exploring()`: hero at (1,1) on 1 HP, Foe adjacent at (2,1) holding `AbilityFixtures`' sure strike, and rules from `AbilityFixtures`. `UseAbilityCommand(Foe, sure strike, UnitsTarget([Hero]))`. Expected: `CombatStarted`, the strike's `DamageDealt` with no `DamageClamped`, `RunEnded(HeroDeath)` last, and no `TurnStarted`. The control case (`Without_combat_start_the_same_strike_is_floored`) keeps `Exploring()`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "OpenerTests|CombatScenarioTests"`. Expected: fail.
- [ ] **Step 3: Implement.**
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass, goldens unchanged.
- [ ] **Step 5: Commit** `M1-11: openers run combat start first, are charged to round 1 and take no tick`.

### Task 7: Reactions and their automatic policies

**Files:**
- Create: `src/Core/Abilities/Reactions.cs`
- Modify: `src/Core/Abilities/Resolution.cs` (the two windows), `src/Core/Engine/Commands.cs` and `Engine.cs` (`SetReactionPolicyCommand`), `src/Core/State/GameState.cs` (`Unit.Reactions`), `Snapshot.cs`, `StateDigest.cs`, `CommandLog.cs` (`"setReactionPolicy"`, fields `ability` and `mode`)
- Test: `tests/Core.Tests/ReactionTests.cs`

**Interfaces:**
- `public sealed record ReactionSetting(string AbilityId, ReactionMode Mode)`; `Unit` gains `ImmutableArray<ReactionSetting> Reactions = default` (normalized, sorted, distinct).
- `public sealed record SetReactionPolicyCommand(UnitId Unit, string AbilityId, ReactionMode Mode) : Command` (plan call 13).
- `Reactions.Mode(RulesContext, GameState, UnitId unit, string abilityId) -> ReactionMode`: the override, else the data default.
- `internal static class Reactions`: `Window(...)` walks the candidates (plan call 11) and returns the fired reaction, if any, plus the new setup and state. The `AskMe` case is Task 8's: here it is treated as `Never` and marked with a `TODO(Task 8)`, which Task 8 removes.

- [ ] **Step 1: Write the failing tests.** Fixture wards in `AbilityFixtures`: `ward.shield` (trigger `attackRoll`, bonus 2); a ward costing `pool.turn`; `ward.camp` (trigger `attackRoll`, bonus 2, `pool.camp` 1, `always`); a save ward (trigger `ownSave`, bonus 2). Every fight fixture includes an `Enemy`.
  - `Always_fires_and_spends_the_reaction`: `ReactionTaken` comes between `AbilityUsed`… and `AttackRolled`, and the result's target is +2.
  - `Never_does_not_fire`.
  - `IfItChangesOutcome_fires_only_when_a_hit_becomes_a_miss`, four rolls with totals stated against the attack's `Target`, bonus 2:
    - total = Target − 1, a miss: does not fire;
    - total = Target, the smallest hit: fires and becomes a miss;
    - total = Target + 2: does not fire, stays a hit, and the reaction is not spent;
    - a natural 20: does not fire.
  - `IfItChangesOutcome_on_a_save`: a failure the bonus fixes fires; a failure by more than the bonus does not; a success does not.
  - `WhilePoolAtLeastHalf_uses_ceil_half`: a theory over (max, remaining, fires): (3,2,true), (3,1,false), (4,2,true), (4,1,false), (1,1,true).
  - `A_camp_paid_reaction_sets_the_camp_flag`: a Foe attacks the hero on the Foe's turn and `ward.camp` fires. `pool.camp` spent 1, `CampSpent` and `ReactionUsed` true, and the flag clears at the next turn start.
  - `No_own_save_ward_against_an_ally`: a party unit's non-selective save burst catches a party ally holding an `always` `ownSave` ward. Expect no `ReactionTaken`, the ward unused and no decision pending.
  - `An_illegal_reaction_is_skipped_and_not_spent`: reaction used, pool empty, or camp flag set.
  - `The_first_that_fires_closes_the_window`: two wards, ids `ward.a` and `ward.b`; only `ward.a` fires.
  - `A_reaction_resets_only_at_its_owners_turn_start`.
  - `SetReactionPolicy_overrides_and_survives_save_and_load`, `AskMe_is_party_only`, `NotAReaction`, and `A_policy_change_takes_no_tick`.
  - `A_reaction_triggered_by_the_opener_resolves_before_round_1` (record test 9, the automatic half): a Foe opener on the hero's `always` ward. Order: `CombatStarted … ReactionTaken … AttackRolled … AbilityResolved, TurnStarted`; the ward's effect counts.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter ReactionTests`. Expected: build fails.
- [ ] **Step 3: Implement** plan calls 11 and 13.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-11: reaction windows with automatic policies; per-unit policy settings`.

### Task 8: "Ask me" as a pending decision

**Files:**
- Create: `src/Core/Abilities/AbilityFrame.cs`
- Modify: `src/Core/State/GameState.cs` (`DecisionKind.Reaction`, `Resolution`, validation), `src/Core/Abilities/Reactions.cs`, `Resolution.cs` (suspend and continue), `src/Core/Engine/Engine.cs` (`ApplyAnswer` for `Reaction`), `Snapshot.cs` (a kind-aware decision loader; the frame), `StateDigest.cs`
- Test: `tests/Core.Tests/AskMeTests.cs`

**Interfaces:**
- `public sealed record AbilityFrame(UnitId User, string AbilityId, AbilityTarget Target, ImmutableArray<UnitId> Locked, int Index, ReactionTrigger Window, ContestSetup Setup, D20Roll Roll, string Candidate, long? SaveDifficulty)`. `Index` is the current target; `Candidate` is the ability id of the ward being asked. Declining moves on to the next candidate after it in ability-id order.
- `GameState` gains `AbilityFrame? Resolution` (last parameter). Validation, structural only (Snapshot.Load has no rules):
  - `Resolution` is not null ⇔ `Pending?.Kind == Reaction`;
  - a frame needs `Combat` not null;
  - `Index` is in `[0, Locked.Length)`; the reactor `Locked[Index]` exists, has `ReactionUsed` false and holds `Candidate` in its `Abilities`;
  - the decision's `Unit` is the reactor and its `Source` is the reactor's square;
  - replacing Task 1's rule: the marker ⇒ `Pending?.Kind == Reaction` or `Ended` is not null.
- `Resolution.Continue` checks the rules-dependent part (the ability ids are known, and `Candidate` is a reaction with that trigger) and throws `InvalidOperationException` on a mismatch.
- `Resolution.Continue(RulesContext, GameState, bool take) -> (ImmutableArray<GameEvent>, GameState)`, called by `ApplyAnswer`; it draws nothing before the window closes.

- [ ] **Step 1: Write the failing tests:**
  - `AskMe_suspends_with_the_roll_known`: the use stops after the d20 (`Rng.Combat.Counter` already advanced), with `DecisionRequested(Reaction)`, a frame set, and no `AttackRolled` yet.
  - `Taking_fires_and_resolves_the_rest`: then `ReactionTaken`, `AttackRolled`, damage, the other targets, `AbilityResolved`.
  - `Declining_asks_the_next_candidate_then_resolves`: two ask-me wards; decline, then take.
  - `The_answer_draws_nothing_before_the_window_closes`.
  - `A_suspended_opener_holds_round_1` (record test 9, the ask-me half): a Foe opener on the hero's ask-me ward. While pending:
    - the marker is set and there is no `TurnStarted`;
    - `MoveCommand` and `EndTurnCommand` get `DecisionPending`.

    After the answer: the reaction, the rest of the opener, then `TurnStarted(slot 0, 1)`.
  - `Save_and_load_across_a_pending_reaction` (Review Focus 4): the save text holds the frame. Answering the loaded copy and the original gives the same `Snapshot.Save`, and `Replay.Run` over the full log gives the same.
  - `Bad_frames_are_format_errors`: a frame without a reaction decision; a reaction decision without a frame; a reactor whose reaction is used; a candidate the reactor does not hold; a frame out of combat; the marker set with a shrine decision pending (standing on a shrine, so only the new rule rejects it).
  - `A_v8_shrine_decision_still_loads`.
  - `The_answer_takes_no_tick`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter AskMeTests`. Expected: build fails.
- [ ] **Step 3: Implement** plan call 12, removing Task 7's `TODO(Task 8)`.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln` and the Godot build. Expected: all pass, 0 warnings.
- [ ] **Step 5: Commit** `M1-11: "ask me" suspends the ability on a reaction decision and resumes on the answer`.

### Task 9: Docs, roadmap and decision rows

**Files:**
- Modify: `docs/effects-vocabulary.md`, `docs/roadmap/m1-vertical-slice.md`, `docs/roadmap/README.md`, `README.md` (an `Abilities/` layout line)

- [ ] **Step 1: The effects vocabulary:**
  - section 6.4: the `abilityResolved` row;
  - section 8: reserved rows for the costs `attack`, `onHit` and `onEvent` (M1-14, M1-16, M1-20), `onSuccess: half` (D-22), out-of-combat rolls by non-openers (D-23), reaction filters other than `none` (M1-22), and the M1-17 AC window across allies;
  - section 9: the M1-11 row restated as built.
- [ ] **Step 2: The roadmap:**
  - **M1-11's Delivers:**
    - "Snapshot v8 … for the marker and the mark" gains "unit abilities, reaction settings and the suspended-resolution frame";
    - add "`CanSee` (integer symmetric shadowcasting, walls only) (owner ruling 2026-11-27)".
  - **M1-34's Delivers:** "`CanSee` (integer symmetric shadowcasting), `KnownCanSee`, `KnownNoiseReach`" → "`KnownCanSee`, `KnownNoiseReach` (`CanSee` is built in M1-11; M1-34 extends its blockers to doors and pieces)". `LosSymmetric` stays in M1-34's Done-when as the registered V18t row.
  - **README:** add the row **D-22** (blocks the first content with a half-on-success save; owner: DH): "A successful save against `onSuccess: half`: halve each part, or the summed total, and how to round? M1-11 refuses `half` at load (owner ruling 2026-11-27)." Source: AB 14.1; DH 20.3.
  - **README:** add the row **D-23** (blocks M1-35; owner: SV): "Out-of-combat ability rolls: an ability used out of combat that is not an opener and rolls (a save, an attack or damage dice): which stream? Drawing `combat` breaks V19t `ExplorationCombatIsolation` (SV 1001), and SV 4.3 names no other. M1-11 refuses such uses with `NotInCombat`, and flat-damage abilities still work (M1-11 plan call 5)." Source: SV 4.3, V19t; AB 6.8; DH 10.4.
  - **Owner notes in the README's M1-11 row or the PR**, recorded as plan calls, not decisions:
    - an opener that costs a bonus action or movement charges that cost, not the action (plan call 6; record 4.1 covers only action openers);
    - "a hostile ability" for the own-save window means a user of another faction (plan call 10);
    - the Vanguard's `ifKills` option policy (VG 6.4 rule 5) is not one of AB 7.3's four (for M1-20);
    - the M1 Mastiff's bleeding claw must be an applied effect, not an AB 9 rider (for M1-22).
- [ ] **Step 3: Run:**
  - `dotnet test Dungeon.sln`;
  - `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`;
  - `dotnet build tests/AnalyzerSmoke/AnalyzerSmoke.csproj` (expected: `error DUN001`);
  - `dotnet run --project src/Sim -- stream-handles` and `content-hashes` (expected: no diff).
- [ ] **Step 4: Commit** `M1-11: vocabulary, roadmap and decision rows`.

## Done when → evidence

- Property test, preview equals resolution (AB 16): `PreviewTests.Preview_equals_resolution`.
- Cone and burst tests in all 8 directions: `AreaTests.Cones_in_all_8_directions`, `Bursts_in_all_8_directions`.
- The AB 14.5 "reaction has no automatic policy" check, and an area over 25 squares failing: `AbilityChecksTests`.
- The record's 6.3 M1-11 tests: `OpenerTests` (tests 1–8 and 10–12), `ReactionTests` and `AskMeTests` (test 9), and `CombatScenarioTests` through the new path (test 13).
