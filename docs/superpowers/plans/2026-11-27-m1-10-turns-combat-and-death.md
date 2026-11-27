# M1-10 Turns, Combat Lifecycle and Death Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** A fight starts, runs turn by turn and ends in the designs' fixed orders. Pools and the turn budget refill on their clocks. Party units are floored out of combat and against undetected traps. A monster at 0 is removed, and the hero at 0 ends the run, after which every command is refused.

**Architecture:**
- **Run state:** `GameState` gains `Hero`, `Combat` (null out of combat, so `combatActive` is `Combat is not null`), `Ended` (the run end) and `NextEventSeq`. Each `Unit` gains its turn budget and its pool spent counts (AB 14.2). Save version 7; `RulesVersion` 2.
- **Pools and budget** (`src/Core/Combat/Pools.cs`, `TurnBudget.cs`): pool definitions are a code table on `RulesContext` (content records arrive in M1-19), with a refill tag and a derived maximum. A unit stores spent counts only (AB 6.2).
- **The turn loop** (`src/Core/Combat/Turns.cs`): `Turns.StartCombat` rolls initiative with M1-08's `Initiative.Roll` and stores the order. `EndTurnCommand` advances the turn. A move in combat spends movement and is refused out of turn. The turn-start and turn-end steps are seams that M1-12 fills.
- **Floors and consequences:** DH 20.3 step 4 joins `Damage.Resolve`. A new `Harm.Deal` gives each damage event a sequence id, emits it, and applies step 7's consequences: a monster is removed, and the hero ends the run. `CombatEnd.EndIfWon` runs the DH 20.5 order once the command's queue has drained.

**Tech Stack:** C# 12 / .NET 8; the M1-07 effects and trigger dispatch; the M1-08 initiative; the M1-09 damage function and HP; the M1-04 validator framework; xUnit.

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-10](../../roadmap/m1-vertical-slice.md#m1-10-turns-combat-lifecycle-and-death), implementing:
- **AB 6.2, 6.4, 6.8, 6.9, 7.1, 14.2, 16** (`2026-11-16-ability-resource-model-design.md`, lines 217–249, 286–304, 383–410, 414–435, 1151–1159, 1341–1343).
- **DH 10, 15.2, 20.3 steps 4 and 7, 20.4, 20.5, 21.4**, and DH-15 and DH-18 (`2026-11-17-death-healing-design.md`, lines 664–820, 1260–1295, 2003–2030, 2034–2070, 2230–2250, 2479, 2481).
- **CO 5.4–5.5** (`2026-11-16-conditions-design.md`, lines 260–301): turns and ticks.
- **TL-8** and TL 6.5 (`2026-11-21-traps-locks-design.md`, lines 1186–1192, 3471): `detectionAtFire`.
- **D-08** (`docs/roadmap/README.md`, line 162; `2026-11-24-m1-scope-rulings-design.md`, lines 265–280): the `RunEnded` shape.

## Owner rulings for this step (2026-11-27)

1. **M1-10 owns the turn loop.** It stores the initiative order, the current turn and the round, and adds `EndTurnCommand`. In combat a move spends the mover's movement and is refused out of turn. The turn-start and turn-end steps stay empty for M1-12. Task 6 amends the roadmap's Delivers line.
2. **The temporary-HP test is reduced.** The Done-when test becomes "HP 5 + undetected trap 10 → HP 1, `DamageClamped{undetectedTrap}`, the save chain never reached". The temporary-HP half of DH 15.2's example is recorded as **D-17**, owned by the step that adds Bolstered.
3. **A monster at 0 is removed.** One routine removes the unit and every effect instance it holds or is the source of, takes it out of the turn order, and frees its square. `UnitDied` carries its id and its last square, for M1-33's drops.

## Global Constraints

- AB 6.2: "Its maximum is derived on demand"; "The state stores only how much of it has been spent"; "Current = maximum − spent, never below zero"; "Every pool has exactly one refill tag": `turn` (the unit's turn start), `fight` (combat start), `rest` (a completed short rest, and camp), `camp` (camp only), `never`. "A level-up never refills anything."
- AB 6.4: "A unit may spend from a `camp` pool at most once per turn." "Each unit's 'camp spend used' flag clears at the start of every unit's turn. Out of combat it clears every tick."
- AB 6.8: "Each tick, every unit's `turn` pools and turn budget reset, including the camp-spend flag."
- AB 14.2: "The turn budget: movement left, action used, bonus used, reaction used, free interaction used, and the camp-spend flag".
- DH 10.1 / D-08: "`RunEnded{cause, killingEventId?}`, cause one of `heroDeath | victory | unravelled`. `killingEventId` is present only for `heroDeath`." After it, the core "rejects every later command".
- DH 10.3: "If the hero reaches 0 in the same event that kills the last enemy ... the hero's death wins and the run ends." `combatActive` "stays true until the combat-end procedure has run".
- DH 10.4: "Damage to a party unit that resolves while `combatActive` is false (20.5) cannot take it below 1 HP." Undetected traps: "cannot take a party unit below 1 HP, in or out of combat." Mixed events: "the floor protects against the trap parts only. If the other parts alone would take the unit to 0, the event is not clamped; otherwise it is clamped so HP stays ≥ 1." "The new floor is party-only."
- DH 20.3 step 4: `DamageClamped{outOfCombat | undetectedTrap | fallFloor | unwarnedStir | lurkerOpener}`. Step 7: the hero → "emits `RunEnded{cause: heroDeath, killingEventId}` as its final event"; "No later target of an area, rider, phase change (conditions 9.3), XP or combat-end step is processed. `killingEventId` is the damage event's sequence id."
- DH 20.5: combat end, in order: "1. the current unit's turn-end step; 2. `combatOnly` instances end; 3. on a win, Downed companions get up ...; 4. XP and level-ups ...: each levelled unit gains Δ (7.3) ...; 5. `combatActive` becomes false (`CombatEnded{reason}`)." "If the hero reached 0, none of this runs."
- DH 7.3: "A level-up ... adds the increase in maximum, Δ = derived maximum after − derived maximum before ... to current HP."
- TL 6.5: "`detectionAtFire {hidden | hunchFloored | hunchWarned | revealed}`"; the floor covers `hidden` and `hunchFloored`.
- SV 4.3: initiative draws on `combat`. SM 3: integer arithmetic only. The determinism analyzer stays green.
- Out of scope:
  - **M1-11:** UseAbility, its combat-start step before cost, and reactions; the area "party area out of combat" scenario's ability.
  - **M1-12:** turn-start and turn-end step contents (CO 5.4), `combatOnly` durations, the zero-HP turn-start backstop, condition ticks.
  - **M1-13:** Surprised, and `detectionAtFire` stored on condition instances a trap applied.
  - **M1-18:** short rests (wiring the `rest` refill). Camp is not in M1.
  - **M1-19, M1-20:** pool content records and the one-refill-tag validator; Onslaught's extra action; Bar the Way's "movement to 0".
  - **M1-21:** XP, level-up cards and the Top-up order (they fill combat-end step 4).
  - **M1-22:** the minion shared initiative slot. **M1-35:** who joins a fight, noticing, the floor-1 surprise rule, fight end back to exploration. **M1-30:** trap records. **M1-33:** `RunEnded{victory}`. **M1-37:** WO6 save-slot handling.
  - **M2 or later:** Downed and get-ups, the save chain (step 5), temporary HP, Hunch grades, the fall, Stir and lurker floors, concentration, toggles.

## Plan-level calls (mine, flagged for review)

1. **Event identity.** `GameState.NextEventSeq` (a `long`, starting at 1, saved and digested) numbers sequenced events. In M1-10 only damage events take a number (`DamageDealt.Seq`); `RunEnded.KillingEventId` is that number. Other events gain numbers when a step needs them.
2. **The hero.** `GameState.Hero` is a `UnitId?` passed to `GameState.Create` (`hero:` parameter, default null). When set, it must name a `Party` unit. With no hero, a party unit at 0 is removed like a monster (no companions in M1; Downed is M2).
3. **Run end.** `GameState.Ended` is a `RunEnd(RunEndCause Cause, long? KillingEventId)?`. `Engine.Apply` checks it before the pending-decision gate and refuses every command with the new `CommandError.RunEnded`. `RunEndCause` declares `HeroDeath`, `Victory` and `Unravelled`; only `HeroDeath` is produced in M1-10.
4. **The combat state.** `CombatState(ImmutableArray<UnitId> Order, int Current, long Round, bool TurnVacated = false)`. The order is fixed at combat start from `Initiative.Roll` over every unit in the state (who joins a fight is M1-35's). The initiative entries are reported in `CombatStarted`, not stored. `TurnVacated` is set when the unit holding the turn died: then `Current` is the index the next turn takes in the shrunk order, and `Current == Order.Length` means the next turn wraps to slot 0 in a new round. Records holding an `ImmutableArray` compare it by reference (as `GameState`'s doc says), so tests compare `Order` with `SequenceEqual`, never with record equality.
5. **The budget stores used counts, never "left".** `TurnBudget(int MovementUsed, int ActionsUsed, bool BonusUsed, bool ReactionUsed, bool FreeUsed, bool CampSpent)`. The default value is a fresh turn. Movement left is the folded `speed` − `MovementUsed`, never below 0, so a speed change mid-turn takes effect at once and speed 0 stops movement (CO: "stops there"). The action allowance is 1; M1-20 adds Onslaught's grant. Recorded as **D-18** (Task 6) for the owner to confirm, since no design says whether a mid-turn speed change rescales the movement left.
6. **When budgets reset.** At a unit's turn start: its budget becomes fresh, its `turn` pools refill, and every unit's `CampSpent` clears (AB 6.4). At combat start: every unit's budget becomes fresh and every `fight` pool refills, then the first unit's turn starts. Out of combat, each tick (each `MoveCommand`) gives every unit a fresh budget and refills every `turn` pool (AB 6.8). `rest` and `camp` refills are functions with no trigger in M1-10; M1-18 calls the `rest` one.
7. **Pools.** `PoolDefinition(string Id, RefillTag Refill, NumberBase Max)` rows on `RulesContext` (a new optional `pools` parameter; production table empty until M1-19). A unit stores `ImmutableArray<PoolSpend>` sorted by pool id, holding only non-zero counts. A camp pool spend sets `CampSpent` and is refused while it is set.
8. **A move in combat.** Allowed only for the current unit (`CommandError.NotYourTurn`) with movement left (`CommandError.NoMovementLeft`). It spends 1 movement and does not advance `Tick` (ticks are out-of-combat time, CO 5.5). Its events are `UnitMoved` and, as before, a shrine `DecisionRequested`. Out of combat the move is unchanged apart from the resets of call 6.
9. **`EndTurnCommand(UnitId Unit)`.** Refused out of combat (`CommandError.NotInCombat`) or out of turn. It emits `TurnEnded(unit)`, runs the empty turn-end seam, moves to the next slot, and emits `TurnStarted(next, round)`. Passing the last slot starts round + 1. Its CommandLog tag is `"endTurn"`.
10. **Step 4 in `Resolve`.** `DamagePart` gains an optional `TrapSource? Trap` (`TrapSource(string TrapId, Detection DetectionAtFire)`), so the mixed rule can tell trap parts from the rest. For a `Party` target, with `from` = min(HP, max):
    - a floor *applies* when combat is not active (`OutOfCombat`, checked first), or when any part's detection is `Hidden` or `HunchFloored` and the dealt total of the other parts alone is below `from` (`UndetectedTrap`);
    - an applying floor *binds* only when `from` ≥ 1 and `from − total` < 1: then `HpAfter = 1` and `Clamp` is that reason;
    - in every other case `Clamp` is null and `HpAfter = max(0, from − total)`. So a non-lethal hit is never a clamp, a clamp never raises HP, and a unit already on 0 stays on 0 unclamped.
    - `DamageOutcome` gains `ClampReason? Clamp`. `Harm.Deal` emits `DamageClamped` only when `Clamp` is set.
    - `ClampReason` and `Detection` declare all of the designs' values; M1-10 produces only `OutOfCombat` and `UndetectedTrap`.
11. **`Harm.Deal` is the only path to death.** `Harm.Deal(RulesContext rules, GameState s, UnitId target, DamageRoll roll) -> (HarmResult, GameState)`, where `HarmResult(DamageOutcome Outcome, long Seq, ImmutableArray<GameEvent> Events)`. It applies the resolved damage, takes `Seq` from `NextEventSeq`, and emits `DamageDealt(Seq, Target, Total, HpAfter)`, then `DamageClamped(Seq, Target, Reason)` if clamped. Then, when the hit took the unit from ≥ 1 to 0: the hero → `RunEnded(HeroDeath, Seq)`, set `Ended`, and nothing after it; any other unit → `UnitDied(id, pos)` and the removal of ruling 3. Damage to a unit already on 0 emits `DamageDealt` and nothing else (the zero-HP backstop is M1-12's). `Harm.Deal` never starts a turn and never ends combat. On an ended run it throws `InvalidOperationException`. M1-09's `Damage.Deal` stays a library call with no consequences.
    - **Removal from the order.** Let d be the dead unit's index in `Combat.Order`; remove it. If d < `Current`, `Current` decreases by 1. If d == `Current`, `Current` stays d (now the successor's slot, or `Order.Length` when it held the last slot) and `TurnVacated` is set, with no `TurnEnded` (a dead unit has no turn-end step, CO 5.4 step 4) and no `TurnStarted`. If d > `Current`, nothing changes. An emptied order keeps `Current` 0 with `TurnVacated` set.
    - **After the queue drains**, callers run `CombatEnd.EndIfWon` and then `Turns.ResumeIfVacated`, in that order (M1-11 wires both). So the next turn starts only once the killing event has finished, and never after a won fight.
12. **`CombatEnd.EndIfWon(RulesContext rules, GameState s) -> (ImmutableArray<GameEvent>, GameState)`.** It does nothing out of combat, on an ended run, or while any `Enemy` unit remains. Otherwise it runs DH 20.5 in order: 1, the current unit's turn-end seam and `TurnEnded` (skipped when `TurnVacated` is set: the turn's holder is dead); 2, the `combatOnly` seam (M1-12); 3, the get-up seam (M2); 4, the level-up seam (M1-21); 5, `Combat = null` and `CombatEnded(CombatEndReason.Win)`. `CombatEndReason` has `Win` only; the designs name no other reason yet.
13. **Level-up HP.** `HitPoints.RaiseLevel(RulesContext rules, GameState s, UnitId unit, int newLevel) -> GameState` sets the level and adds Δ = max after − max before to current HP. It leaves spent counts and the budget alone. M1-21 calls it in combat-end step 4.
14. **New rule events:** `combatStarted` (subject: the first unit in the order), `turnStarted` and `turnEnded` (the unit), `unitDied` (the dead unit; its effects are already gone, so only other units' triggers can answer it). Each is inserted into `RuleEvents.Core` in ordinal id order, mirrored in effects-vocabulary section 6, and `EffectsKindTests`' registry test is updated in the same task. `combatEnded` gets no rule event: a rule event needs a subject unit, and combat end has none. It is deferred until a subject-less rule event exists.
15. **`PartyInstantDeath` (DH 10.2, 21.2)** is a registered static check, `content.DH21.PartyInstantDeath`. The effects vocabulary has no operation that sets or zeroes current HP. The check fails if `ModifierOp` gains a value outside {Add, Halve, Zero, RollMode, Flag} that has not been added to its allow-list, or if `maxHp` loses its floor of 1. It is a tripwire for the step that adds such an operation. Shape: `public sealed class PartyInstantDeathCheck(ImmutableArray<DerivedNumber>? numbers = null, IEnumerable<ModifierOp>? ops = null) : IStaticCheck`, defaulting to `DerivedNumbers.Core` and `Enum.GetValues<ModifierOp>()`. `Reads` is empty, it ignores its `ContentSet`, and its failures name the code: `src/Core/Effects/EffectDefinition.cs` with field `ModifierOp.<value>`, and `src/Core/Effects/Registries.cs` with field `DerivedNumbers.Core[maxHp].Floor`. Effects-vocabulary section 8 reserves "instant death, save-or-die and set-HP-to-0" as refused for party targets.
16. **Versions.** Save version 7. `RulesVersion` becomes 2: from now on commands are refused after a run end, and moves in combat change (Versions.cs: "Bump it whenever rules-core code changes what a command does").

## Review Focus

1. **The current unit dies on its own turn:** `Harm.Deal`'s events are `[DamageDealt, UnitDied]` with no `TurnStarted`, and `TurnVacated` is set. With an enemy left, `EndIfWon` emits nothing and `ResumeIfVacated` starts the very next unit's turn (the round advances when the dead unit held the last slot). When it was the last enemy, `EndIfWon` emits only `CombatEnded` and no party turn starts. A death in an earlier slot keeps the current unit (Task 4, Task 5).
2. **A floor never heals and never fires needlessly:** out of combat, a party unit on 1 HP takes 5 and stays on 1 (`Clamp` `OutOfCombat`); a party unit on 17 takes 3 and goes to 14 with `Clamp` null; a party unit created on 0 HP takes 5 and stays on 0 with `Clamp` null and no death (Task 4).
3. **Mixed trap event in combat:** a hidden-trap part of raw 3 plus a slashing part of raw 4. On 4 HP the slashing alone reaches 0, so there is no clamp and the hero dies. On 5 HP the event is clamped and HP is 1 (Task 4).
4. **A pool spent above a lowered maximum:** spent 2, maximum now 1 → current 0, never −1. Its refill clears it to 0 spent (Task 2).
5. **After `RunEnded` with a decision pending:** `AnswerDecisionCommand`, `EndTurnCommand` and `MoveCommand` are all refused with `RunEnded`, and the state is unchanged (Task 1 for move and answer, Task 3 for end turn).

---

### Task 1: Run state, the run-end gate, save version 7

**Files:**
- Create: `src/Core/Combat/CombatState.cs` (`CombatState`, `RunEnd`, `RunEndCause`), `src/Core/Combat/TurnBudget.cs` (`TurnBudget`, `PoolSpend`)
- Modify: `src/Core/State/GameState.cs` (four state fields, two unit fields, `Create`'s `hero` parameter and checks), `src/Core/Engine/Engine.cs` (`CommandError.RunEnded`, `NotYourTurn`, `NoMovementLeft`, `NotInCombat`; the run-end gate first), `src/Core/Persistence/Snapshot.cs` (v7), `src/Core/State/StateDigest.cs`, `src/Core/Run/Versions.cs` (`RulesVersion` 2)
- Modify tests: `PersistenceTests.cs` (version literals; v6 retired), `RunConfigTests.cs` (rules version 2), `StateDigestTests.cs`, `HitPointStateTests.cs` (the v6 refusal), every `new Unit(` site only if the optional fields need it
- Test: `tests/Core.Tests/RunStateTests.cs`

**Interfaces (namespace `Dungeon.Core.Combat`):**
- `public sealed record CombatState(ImmutableArray<UnitId> Order, int Current, long Round, bool TurnVacated = false)` (plan call 4); `public UnitId CurrentUnit => Order[Current]` (valid only when `TurnVacated` is false). Valid states: `Round` ≥ 1; ids distinct and existing; `Current` in [0, `Order.Length`), or equal to `Order.Length` (including an empty order) only when `TurnVacated` is set.
- `public enum RunEndCause { HeroDeath, Victory, Unravelled }`; `public sealed record RunEnd(RunEndCause Cause, long? KillingEventId)`.
- `public readonly record struct TurnBudget(int MovementUsed, int ActionsUsed, bool BonusUsed, bool ReactionUsed, bool FreeUsed, bool CampSpent)`; `default` is a fresh turn.
- `public readonly record struct PoolSpend(string PoolId, int Spent)`.
- `Unit(UnitId Id, GridPos Pos, Faction Faction, ImmutableArray<int> Boons, UnitStats Stats, int Hp, TurnBudget Budget = default, ImmutableArray<PoolSpend> Spent = default)`. The record normalizes a default `Spent` itself (`public ImmutableArray<PoolSpend> Spent { get; init; } = Spent.IsDefault ? [] : Spent;`), so every unit, however built, holds a real array. `Create` checks it (ordinal-sorted by pool id, no repeats, every count ≥ 1), and Load gets the check through `Create`.
- `GameState` gains, after `NextDecisionId`: `UnitId? Hero, CombatState? Combat, RunEnd? Ended, long NextEventSeq`. `Create(..., UnitId? hero = null)` starts with `Combat` null, `Ended` null and `NextEventSeq` 1.
- `public bool CombatActive => Combat is not null;` on `GameState`.

- [ ] **Step 1: Write the failing tests** in `RunStateTests`:
  - `Create_rejects_a_hero_that_is_missing_or_not_party`, `Create_rejects_negative_budget_counts`, `Create_rejects_unsorted_repeated_negative_or_zero_spent_counts`: each `ArgumentException` naming the unit and field.
  - `A_new_run_is_out_of_combat_and_not_ended`: `Combat` null, `Ended` null, `NextEventSeq` 1, every budget fresh.
  - `The_run_state_survives_save_and_load`: a state with a hero, a combat state (order [3, 1, 2], current 1, round 2), a used budget, spent counts and `NextEventSeq` 5 round-trips byte for byte; another with `Ended` = (HeroDeath, 4) and `NextEventSeq` 5; a third with a vacated turn (`Current == Order.Length`, `TurnVacated` true). The text has `"version":7`.
  - `A_load_with_a_bad_combat_or_run_end_is_a_format_error`: an order naming an unknown or repeated unit; `current` out of range (or equal to the order's length without `turnVacated`); `round` below 1; a `killingEventId` without `heroDeath`, `heroDeath` without a `killingEventId`, or a `killingEventId` below 1 or not below `nextEventSeq` → `SnapshotFormatException`. `Create` applies the same rules to the fields it receives.
  - `A_v6_save_is_refused` (`SnapshotVersionException`).
  - `After_a_run_end_every_command_is_refused` (Review Focus 5): a state with `Ended` set and a pending decision. A move and an answer each return `CommandError.RunEnded`, no events, and the same state instance. (`EndTurnCommand` arrives in Task 3, which adds its case.)
  - In `StateDigestTests`: each new state field and each budget and spent field reaches the digest.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "RunStateTests|StateDigestTests"`. Expected: build fails.
- [ ] **Step 3: Implement.** The DTOs follow the M1-09 pattern (`JsonRequired`, enums by name, the next property orders). Bump `CurrentVersion` to 7 (doc line "v7 (M1-10) adds the run, combat and turn state") and `RulesVersion.Current` to 2. The digest uses existing `CanonicalWriter` primitives only (add no writer or reader method): the state record grows from `BeginRecord(11)` to 15, with field 11 `Hero` as `OptionalInt32`; 12 `Combat` as an `OptionalRecord` (order as a list of `Int32` ids, `Current` and `Round` as integers, `TurnVacated` as a bool or 0/1 integer, whichever the writer already has); 13 `Ended` as an `OptionalRecord` (`Enum` cause, then the killing id through `OptionalUInt64`, refusing a non-positive id the way the effect-seq key does); 14 `NextEventSeq` as `Int64`. The unit record grows to 9 fields: 7 the budget record, 8 the spent list (pool id string, count).
- [ ] **Step 4: Run** `dotnet test Dungeon.sln` and `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`. Expected: all pass, 0 warnings.
- [ ] **Step 5: Commit** `M1-10: run, combat and turn state; run-end gate; save version 7, rules version 2`.

### Task 2: Pools and the camp spend

**Files:**
- Create: `src/Core/Combat/Pools.cs` (`RefillTag`, `PoolDefinition`, `Pools`)
- Modify: `src/Core/Effects/RulesContext.cs` (optional `pools` parameter, sorted and checked like the other tables), `src/Core/Stats/HitPoints.cs` (`RaiseLevel`)
- Test: `tests/Core.Tests/PoolTests.cs`, `tests/Core.Tests/LevelUpTests.cs`

**Interfaces:**
- `public enum RefillTag { Turn, Fight, Rest, Camp, Never }`.
- `public sealed record PoolDefinition(string Id, RefillTag Refill, NumberBase Max)`; `RulesContext` rejects a null or repeated id and an undefined tag, and exposes `PoolDefinition Pool(string id)` (throws `ArgumentException` for an unknown id) and `ImmutableArray<PoolDefinition> Pools`.
- `public static class Pools`:
  - `long Max(RulesContext rules, GameState s, UnitId unit, string poolId)`; `int Spent(GameState s, UnitId unit, string poolId)`; `long Current(...)` = max(0, Max − Spent).
  - `(bool Ok, GameState Next) Spend(RulesContext rules, GameState s, UnitId unit, string poolId, int count)`: refused when `count` < 1 (throws), when current < count, or for a `Camp` pool when `Budget.CampSpent` is set. A camp spend sets `CampSpent`.
  - `GameState Refill(RulesContext rules, GameState s, UnitId unit, RefillTag tag)` and `GameState RefillAll(RulesContext rules, GameState s, RefillTag tag)`: zero the spent counts of the pools with that tag, except that `Camp` zeroes both `Rest` and `Camp` pools (AB 6.2: `rest` refills at "a completed short rest, and camp"). `Never` throws: nothing refills it.
- `HitPoints.RaiseLevel(RulesContext rules, GameState s, UnitId unit, int newLevel) -> GameState` (plan call 13); throws for a level outside 0–12 or below the current level.

- [ ] **Step 1: Write the failing tests** (fixture pools in the test's own `RulesContext`: `pool.turn` (Turn, max 1), `pool.fight` (Fight, 1), `pool.rest` (Rest, 2), `pool.camp` (Camp, 1), `pool.never` (Never, 3), `pool.byLevel` (Rest, max 1 below level 5, else 2)):
  - `Each_refill_tag_resets_only_its_own_pools`: a theory over the four refillable tags. After spending one of every pool, each refill zeroes exactly: `Turn` → {`pool.turn`}; `Fight` → {`pool.fight`}; `Rest` → {`pool.rest`, `pool.byLevel`}; `Camp` → {`pool.rest`, `pool.byLevel`, `pool.camp`}. `pool.never` stays spent in every row; `Never` is covered by `A_never_pool_cannot_be_refilled` (for both `Refill` and `RefillAll`).
  - `Spent_counts_are_stored_only_when_non_zero_and_sorted`.
  - Review Focus 4: spent 2 on `pool.byLevel` at level 5, then the level drops to 4 through state rebuilt for the test → current 0; refill → spent 0.
  - `A_spend_beyond_current_is_refused` and `A_never_pool_cannot_be_refilled`.
  - `One_camp_spend_per_turn`: the first camp spend succeeds and sets `CampSpent`; a second is refused; a `Rest` spend is not affected by the flag.
  - `A_camp_spend_on_another_units_turn_uses_the_same_flag` (AB 16 "including reactions", with a fixture off-turn spend: unit 2 spends while unit 1 holds the turn, then cannot spend again before the next turn start). The flag's clearing at turn start is tested in Task 3.
  - `LevelUpTests.A_level_up_adds_the_max_hp_increase_and_leaves_spent_counts_alone`: a reference hero (Slight, Con +1) at level 1 on 10 of 18 HP with `pool.byLevel` spent 1. `RaiseLevel` to 2 → HP 17 (Δ 7), spent still 1, budget unchanged. At level 4 → 5, `pool.byLevel` current rises from 0 to 1 with nothing refilled.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "PoolTests|LevelUpTests"`. Expected: build fails.
- [ ] **Step 3: Implement.** Writing the spent counts keeps the array sorted by ordinal pool id and drops zeros.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-10: pools store spent counts; five refill tags; one camp spend per turn; level-ups add the HP increase`.

### Task 3: The turn loop

**Files:**
- Create: `src/Core/Combat/Turns.cs`
- Modify: `src/Core/Engine/Commands.cs` (`EndTurnCommand`), `src/Core/Engine/Engine.cs` (the end-turn handler; the in-combat move; the out-of-combat tick resets), `src/Core/Engine/Events.cs` (`CombatStarted`, `TurnStarted`, `TurnEnded`), `src/Core/Persistence/CommandLog.cs` (`"endTurn"`), `src/Core/Effects/Registries.cs` (rule events `combatStarted`, `turnStarted`, `turnEnded`)
- Modify tests: `MoveTests.cs` if needed; `EffectsKindTests.cs` (its registry test pins `RuleEvents.Core`: the ids become [combatStarted, turnEnded, turnStarted, unitMoved], each looked up by id with its event type)
- Test: `tests/Core.Tests/TurnTests.cs`

**Interfaces:**
- `public sealed record CombatStarted(ImmutableArray<InitiativeEntry> Order) : GameEvent`; `TurnStarted(UnitId Unit, long Round)`; `TurnEnded(UnitId Unit)`.
- `public sealed record EndTurnCommand(UnitId Unit) : Command`.
- `public static class Turns`:
  - `(ImmutableArray<GameEvent> Events, GameState Next) StartCombat(RulesContext rules, GameState s)`: throws when already in combat or ended. Rolls `Initiative.Roll` over every unit, sets `Combat` (round 1, current 0), applies plan call 6's combat-start resets, emits `CombatStarted` and then starts the first turn.
  - `(ImmutableArray<GameEvent> Events, GameState Next) StartTurn(RulesContext rules, GameState s)`: the current unit's turn start (plan call 6), the empty CO 5.4 seam, then `TurnStarted`.
  - `(ImmutableArray<GameEvent> Events, GameState Next) Advance(RulesContext rules, GameState s)`: moves to the next slot (round + 1 on wrap) and calls `StartTurn`. `EndTurnCommand` uses it.
  - `(ImmutableArray<GameEvent> Events, GameState Next) ResumeIfVacated(RulesContext rules, GameState s)`: does nothing unless combat is active, the run has not ended, `TurnVacated` is set and the order is non-empty. Otherwise it clears the flag, wraps `Current == Order.Length` to slot 0 with round + 1, and calls `StartTurn` (plan call 11; Task 4 creates the vacated state).
  - `long MovementLeft(RulesContext rules, GameState s, UnitId unit)` = max(0, folded `speed` − `MovementUsed`).

- [ ] **Step 1: Write the failing tests** in `TurnTests` (three party units on an open fixture map, so initiative order comes from seed 7):
  - `Starting_combat_rolls_initiative_and_starts_the_first_turn`: `CombatActive`, round 1, `Combat.Order` `SequenceEqual` to the units of `Initiative.Roll`'s order on the same state, events checked with `Assert.Collection` (`CombatStarted` by type and its `Order` by `SequenceEqual`; then `TurnStarted(first, 1)`), `Rng.Combat` advanced by 3. Never compare `CombatStarted` or `CombatState` with record equality (plan call 4).
  - `Combat_start_refills_fight_pools_and_freshens_every_budget`.
  - `End_turn_passes_to_the_next_unit_and_wraps_to_a_new_round`: three end turns → `TurnStarted` for slots 2, 3, then slot 1 in round 2; each emits `TurnEnded` first.
  - `Turn_start_freshens_the_units_budget_refills_its_turn_pools_and_clears_every_camp_flag`.
  - `End_turn_is_refused_out_of_combat_and_out_of_turn` (`NotInCombat`, `NotYourTurn`).
  - `End_turn_is_refused_after_a_run_end` (Review Focus 5): `Ended` set and a decision pending → `CommandError.RunEnded`, no events, the same state instance.
  - `Resume_starts_the_vacated_slot_or_wraps` (fixture states with `TurnVacated` set): `Current` 1 of 3 → `TurnStarted(Order[1], same round)`; `Current == Order.Length` → `TurnStarted(Order[0], round + 1)`; flag cleared. Not vacated, out of combat, ended, or an empty order → no events, the same state.
  - `A_move_in_combat_spends_movement_and_does_not_tick`: six moves by the current unit succeed (speed 6) with no `TickAdvanced`; the seventh is refused `NoMovementLeft`; a move by another unit is refused `NotYourTurn`.
  - `A_speed_penalty_mid_turn_shrinks_the_movement_left` (plan call 5): after 3 moves, a −2 effect on `speed` leaves 1.
  - `Out_of_combat_each_move_freshens_budgets_and_turn_pools`: AB 6.8.
  - `End_turn_round_trips_through_the_command_log`.
  - The existing `MoveTests` still pass unchanged (out of combat).
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "TurnTests|MoveTests"`. Expected: build fails.
- [ ] **Step 3: Implement.** The end-turn handler and the in-combat move go through `Engine.Apply`, so their events reach `TriggerDispatch` as before. Add the three rule-event rows (subjects: `TurnStarted.Unit`, `TurnEnded.Unit`, and for `CombatStarted` the first unit in its order).
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-10: the turn loop; EndTurn; moves in combat spend movement; budgets reset on their clocks`.

### Task 4: Floors, damage events and death

**Files:**
- Create: `src/Core/Combat/Harm.cs` (`Harm`, `HarmResult`)
- Modify: `src/Core/Damage/DamageTypes.cs` (`Detection`, `TrapSource`, `DamagePart.Trap`), `src/Core/Damage/DamageFunction.cs` (step 4; `ClampReason`; `DamageOutcome.Clamp`), `src/Core/Engine/Events.cs` (`DamageDealt`, `DamageClamped`, `UnitDied`, `RunEnded`), `src/Core/Effects/Registries.cs` (rule event `unitDied`)
- Modify tests: M1-09 damage tests whose party target now meets the out-of-combat floor take an `Enemy` target or start combat first; `DamageReplayTests` starts combat first and re-pins its golden (say so in its comment); M1-09 tests comparing whole `DamageOutcome` values gain `Clamp` null; `EffectsKindTests` (rule-event ids gain `unitDied`, mapped to `UnitDied`).
- Test: `tests/Core.Tests/FloorTests.cs`, `tests/Core.Tests/DeathTests.cs`

**Interfaces:**
- `public enum Detection { Hidden, HunchFloored, HunchWarned, Revealed }`; `public sealed record TrapSource(string TrapId, Detection DetectionAtFire)`.
- `public readonly record struct DamagePart(DamageType Type, int Dice, int Sides, int Flat, TrapSource? Trap = null)`.
- `public enum ClampReason { OutOfCombat, UndetectedTrap, FallFloor, UnwarnedStir, LurkerOpener }`; `DamageOutcome` gains `ClampReason? Clamp` as its last field.
- Events: `DamageDealt(long Seq, UnitId Target, long Total, int HpAfter)`, `DamageClamped(long Seq, UnitId Target, ClampReason Reason)`, `UnitDied(UnitId Unit, GridPos At)`, `RunEnded(RunEndCause Cause, long? KillingEventId)`.
- `public sealed record HarmResult(DamageOutcome Outcome, long Seq, ImmutableArray<GameEvent> Events)`; `Harm.Deal` per plan call 11.

- [ ] **Step 1: Write the failing tests:**
  - `FloorTests`:
    - `Out_of_combat_a_party_unit_stops_on_one`: 30 to a party unit on 17 → 1, `Clamp` `OutOfCombat`. The same to an `Enemy` → 0, no clamp.
    - `A_floor_that_changes_nothing_is_not_a_clamp`: out of combat, 3 to a party unit on 17 → 14, `Clamp` null, and `Harm.Deal` emits no `DamageClamped`; in combat, a `Hidden` part of raw 2 on 10 → 8, `Clamp` null.
    - `The_reduced_DH_15_2_example` (owner ruling 2): in combat, HP 5, an undetected-trap part (`Hidden`) of raw 10 → HP 1, `DamageClamped{UndetectedTrap}`, `ReachedZero` false.
    - `A_revealed_trap_is_ordinary_damage`: the same with `Revealed` in combat → 0.
    - Review Focus 2 and Review Focus 3.
    - `Floors_come_before_the_save_chain` (property, DH 21.4): over seeds 1–200, random HP 1–30 and random parts (a third of them hidden-trap parts), every clamped outcome has `HpAfter` 1, `ReachedZero` false and `from − Total` < 1, so step 5 (which runs only on a total that still reaches 0) is never reached by a floored event.
  - `DeathTests`:
    - `A_monster_at_zero_is_removed_with_its_effects`: an enemy holding one effect and sourcing another on a party unit; a lethal `Harm.Deal` → events `[DamageDealt(1, ...), UnitDied(id, pos)]`, the unit is gone, both effect instances are gone, its square is free, `NextEventSeq` 2.
    - Review Focus 1, order [A, B, C]: B, the current unit, dies → events `[DamageDealt, UnitDied]`, order [A, C], `Current` 1, `TurnVacated` set, no `TurnStarted`; then `ResumeIfVacated` → `TurnStarted(C, same round)`. When C dies holding the last slot, `ResumeIfVacated` → `TurnStarted(A, round + 1)`.
    - `A_death_in_an_earlier_slot_keeps_the_current_turn`: order [A, B, C] with C current; A dies → `CurrentUnit` still C, `Current` 1, round unchanged, no `TurnStarted`, `TurnVacated` false.
    - `Damage_to_a_unit_already_on_zero_has_no_consequence`: emits only `DamageDealt`.
    - `The_hero_at_zero_ends_the_run`: in combat, a lethal hit on the hero → the last event is `RunEnded(HeroDeath, seq)` where seq is the `DamageDealt`'s; `Ended` set; the hero is not removed.
    - `Harm_on_an_ended_run_throws`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "FloorTests|DeathTests"`. Expected: build fails.
- [ ] **Step 3: Implement** step 4 in `Resolve` (its numbered comment becomes code; the step-5 comment stays) and `Harm.Deal`. The removal reuses `Effects.Remove` for each instance whose holder or source is the dead unit, in seq order, then removes the unit and fixes `Combat` by plan call 11's removal rule. Inside `namespace Dungeon.Core.Combat` the simple names `Effects` and `Damage` bind to namespaces; use the codebase's existing aliasing pattern (as `Ladder.cs` does).
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-10: out-of-combat and undetected-trap floors; damage events with ids; monsters removed at 0; the hero's death ends the run`.

### Task 5: Combat end and the Done-when scenarios

**Files:**
- Create: `src/Core/Combat/CombatEnd.cs` (`CombatEnd`, `CombatEndReason`)
- Modify: `src/Core/Engine/Events.cs` (`CombatEnded`; no rule event, plan call 14)
- Test: `tests/Core.Tests/CombatEndTests.cs`, `tests/Core.Tests/CombatScenarioTests.cs`

**Interfaces:**
- `public enum CombatEndReason { Win }`; `public sealed record CombatEnded(CombatEndReason Reason) : GameEvent`.
- `CombatEnd.EndIfWon` per plan call 12.

- [ ] **Step 1: Write the failing tests:**
  - `CombatEndTests`:
    - `Combat_ends_when_no_enemy_remains`: events `[TurnEnded(current), CombatEnded(Win)]`, `Combat` null.
    - `Combat_does_not_end_while_an_enemy_remains`, `out_of_combat`, or `on_an_ended_run`: no events, the same state.
    - `After_combat_ends_party_damage_is_floored_again`.
    - `The_last_enemy_dying_on_its_own_turn_starts_no_party_turn` (Review Focus 1): `Harm.Deal` gives `[DamageDealt, UnitDied]`; `EndIfWon` gives `[CombatEnded(Win)]` with no `TurnEnded` (the turn was vacated); `ResumeIfVacated` then does nothing.
  - `CombatScenarioTests` (fixture scripts calling the core functions, as the roadmap's fixture rule allows):
    - `The_hero_dies_alongside_the_last_enemy` (DH 10.3, Done-when): in combat, a lethal `Harm.Deal` on the last enemy, then a fixture death burst (`Harm.Deal` on the hero for more than its HP) before `EndIfWon`, then `EndIfWon`. The burst is not floored, the last event is `RunEnded(HeroDeath, burst seq)`, no `CombatEnded` is emitted, and a following `MoveCommand` is refused `RunEnded`.
    - `An_opening_ambush_strike_is_in_combat_damage` (DH 10.4, Done-when): from exploration, the fixture opener runs `Turns.StartCombat` before its strike, then `Harm.Deal` on the hero → no `DamageClamped`, and a lethal strike ends the run.
    - `Without_combat_start_the_same_strike_is_floored`: the control case, `DamageClamped{OutOfCombat}`.
    - `An_undetected_trap_that_starts_combat_is_floored`: the trap starts combat, then deals a lethal `Hidden` part → HP 1, `UndetectedTrap`.
    - `A_scripted_fight_replays_identically`: a hero and one `Enemy` on an open fixture map, with HP too high for either to reach 0 in three rounds (for example enemy table HP 40 and a level-3 `Sturdy` hero; check against the largest possible roll). `Turns.StartCombat`, then six turns: the attacker is `s.Combat.CurrentUnit`, the defender is the other unit; `Contests.Attack`, then on a hit `Damage.Roll` (crit from the attack) and `Harm.Deal`; then `Engine.Apply(new EndTurnCommand(attacker), rules)`, asserted accepted. The script stops early if `Ended` is set or a unit is gone (a guard; the HP choice means it should not). Then `EndIfWon` (a no-op here). Run it twice, and once with a save and load after round 1: the same final save text. Pin the final HP pair and `NextEventSeq` as a golden from the first green run, and say so in a comment.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "CombatEndTests|CombatScenarioTests"`. Expected: build fails.
- [ ] **Step 3: Implement** `CombatEnd.EndIfWon` with the five DH 20.5 steps in order as numbered comments, the seams naming their owners.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-10: combat end in the DH 20.5 order; the Done-when scenarios`.

### Task 6: PartyInstantDeath, docs and the roadmap

**Files:**
- Create: `src/Core/Validation/Checks/PartyInstantDeathCheck.cs`
- Modify: `src/Core/Validation/StaticChecks.cs` (adds `new PartyInstantDeathCheck()`), `content/registry/checks.json` (the row: owner "death and healing", phase `s`, class `build`, wave `M1`, ciTier `pr`), `src/Core/Content/ContentHashes.g.cs` (regenerated), `docs/effects-vocabulary.md` (section 6 rule events; section 8 reserved rows for durations now "M1-12", instant death per plan call 15; section 9 M1-10 row), `docs/roadmap/m1-vertical-slice.md` (M1-10 Delivers: the turn loop and the reduced Done-when, owner rulings 1–2), `docs/roadmap/README.md` (D-17, D-18, D-19 rows; next step M1-11), `README.md` (a `Combat/` layout line), `dungeon-ttrpg/scripts/` (the demo sets the hero, and the controller shows a run end by refusing input; no combat in the demo)
- Test: `tests/Core.Tests/PartyInstantDeathCheckTests.cs`

**Decision rows to add (docs/roadmap/README.md):**
- **D-17** (blocks the step that adds Bolstered): the temporary-HP half of the DH 15.2 / 21.4 property test (temporary HP absorbs before the floors), deferred by owner ruling 2.
- **D-18** (blocks M1-13, whose Slowed and Prone change speed mid-turn): whether movement left is rescaled by a mid-turn speed change (plan call 5 rescales it, since movement left is speed − used).
- **D-19** (blocks M1-30 and M1-35): (a) TL-19 staging. CO 1156 says "Trap conditions on floor 1 end when combat starts ... M2", but the roadmap puts TL-19 in M1-30. M1-10 leaves a combat-start seam either way. (b) Where initiative falls relative to an opening strike, and whether the opener uses the striker's round-1 turn (DH 10.4 against EP 6.8.2). M1-10 rolls initiative inside `StartCombat`.

- [ ] **Step 1: Write the failing tests** (the check's shape is plan call 15): `The_check_passes_on_the_current_vocabulary` (`new PartyInstantDeathCheck()`, no failures); `The_check_fails_when_max_hp_loses_its_floor` (`DerivedNumbers.Core` with the `maxHp` row replaced by `row with { Floor = null }`, and again with the row missing → one failure each, naming `maxHp`); `The_check_names_an_unreviewed_modifier_op` (`ops: [.. Enum.GetValues<ModifierOp>(), (ModifierOp)99]` → one failure naming 99). Then run `dotnet test tests/Core.Tests --filter PartyInstantDeathCheckTests` and expect a build failure.
- [ ] **Step 2: Implement** the check and the registry row; run `dotnet run --project src/Sim -- content-hashes` (and `stream-handles`, which must not change). Update the docs, the roadmap, the decision rows and the demo.
- [ ] **Step 3: Run** `dotnet test Dungeon.sln` (the validators run inside it, README "The validators run as part of `dotnet test`"), `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` and the analyzer smoke build. Expected: all pass, 0 warnings, no generator drift, and the validation-runner test lists the new check as passing.
- [ ] **Step 4: Commit** `M1-10: the PartyInstantDeath check; vocabulary, roadmap and decision rows`.

## Done when → evidence

- AB 16 tests for each refill tag and the camp-spend rule pass: `PoolTests` (`Each_refill_tag_resets_only_its_own_pools`, `One_camp_spend_per_turn`, `A_camp_spend_on_another_units_turn_uses_the_same_flag`), `TurnTests` (the flag clears at every turn start), `LevelUpTests`.
- DH 21.4 property tests pass: `FloorTests.Floors_come_before_the_save_chain` and `The_reduced_DH_15_2_example` (owner ruling 2; D-17 records the temporary-HP half).
- "Hero dies alongside the last enemy" ends the run: `CombatScenarioTests.The_hero_dies_alongside_the_last_enemy`.
- An opening ambush strike counts as in-combat damage: `CombatScenarioTests.An_opening_ambush_strike_is_in_combat_damage`.
