# M1 Vertical Slice

**Question it answers:** Is the loop fun? Does the effects system hold?
(MAIN 9)

**Scope:** one class (the Vanguard, Weaponmaster subclass) at levels 1–3,
solo; one generated floor (floor 1, the Kennel Wood); three enemy types
(Kennel Mastiff, Huntsman, Poacher) plus the generic minion; one boss (Old
Ragged in the Den); basic loot; permadeath. No companions, camp, hub,
species, multiclassing, riders or mid-floor resume.

**Saves in M1:** safe-point run saves only. The mid-floor suspend slot is
M2 (SV-14, XI-28). Mid-floor reload is therefore possible in M1; this is
accepted for the slice and is not a bug.

**Sign-off checklist:** SV 7 "M1" (reproduced as the M1-END step).

**Fixtures:** where a step must be tested before the system that normally
feeds it exists, it uses a hand-built fixture (a test room, a test
monster). The real input replaces the fixture in the later step named.

Abbreviations and step format: see the [index](README.md).

---

## Phase A: Foundations

These steps rework M0 to meet the approved seeds and validation design
before any new rules are written. Everything later depends on them.

### M1-01 Project layout and build guards

**Goal:** The repository matches the agreed project layout and the build
enforces the determinism rules at compile time.

**Implements:** MAIN 6.1; SV DS11, 9.5; SV-28.

**Depends on:** M0-END. (D-01 ruled: MAIN 6.1 now names
`dungeon-ttrpg/`; this step creates `content/` and `src/Sim`.)

**Delivers:**
- Folder layout per D-01 (content folder, empty `Sim` console project in
  `Dungeon.sln`).
- A Roslyn analyzer in the build that bans, inside Core: `float`,
  `double`, `decimal`, `System.Random`, clocks, `GetHashCode`, enumeration
  of `Dictionary`/`HashSet`, unstable sorts, culture-sensitive
  comparisons, Godot references.
- `FloorDiv`/`FloorMod` helpers.
- Checked arithmetic (`CheckForOverflowUnderflow`) in Debug and CI builds,
  with a mechanism to exempt one named file (the key-algebra file).

**Done when:**
- [ ] One test fixture per banned construct fails the build with a
  message naming the rule.
- [ ] Core, tests and the Godot project build clean with the analyzer on.
- [ ] `ArchitectureTests` still pass; MAIN 6.1 and the repository agree.

**Out of scope:** the hostile-environment nightly (M1-40).

### M1-02 Key algebra

**Goal:** Replace PCG32 with the SplitMix64 key algebra every random draw
in the game is derived from.

**Implements:** SV 4.1, 4.2 (path grammar), K-rules; SV-1, SV-7, SV-8;
MAIN 6.3.

**Depends on:** M1-01.

**Delivers:**
- `Mix`, `LabelId` (FNV-1a-64), `Absorb`, `Derive`, `Draw`, `Bound`
  (multiply-shift, asserts n ≥ 1), `H`, `D20` in one `unchecked` file,
  unsigned indices only.
- Keyed sorts with an ordinal-id tiebreak.
- 60-bit `runSeed`; no salt option (SV-7).
- `Pcg32` removed; M0 callers moved to the new API.
- `Engine.DrawBoons` rewritten to a fixed draw count (no rejection loop).
- Snapshot `saveVersion` bumped; M0 snapshots rejected with the typed
  version error.

**Done when:**
- [ ] `KeyAlgebraVectors` (SV 4.1 vector table) pass on Windows and Linux.
- [ ] The same vectors pass under the checked-arithmetic debug build.
- [ ] A test proves `DrawBoons` makes the same number of draws for every
  seed.
- [ ] All M0 behaviour tests (movement, decisions, replay) pass on the
  new generator.

### M1-03 Canonical writer and digests

**Goal:** One binary serialiser produces every digest and journal delta.

**Implements:** SV 4.5.

**Depends on:** M1-02.

**Delivers:** `CanonicalWriter` (schema ordinals, sorted collections,
length prefixes) and SHA-256 digests over its bytes.

**Done when:**
- [ ] Round-trip and byte-stability tests pass on both OSs.
- [ ] Reordering an input collection does not change the digest (test).

**Out of scope:** the pinned `FloorStatic` vector (M1-33, once the floor
schema locks).

### M1-04 Content pipeline and validator framework

**Goal:** Game data loads from JSON with stable IDs, fails loudly when
wrong, and every check in the designs has a place to live.

**Implements:** MAIN 6.5; SV DS13, 5.1–5.3, 9.1, 4.6 (hashes); SV-19
(partial), SV-20 (model); NP staging ("M1 records").

**Depends on:** M1-03.

**Delivers:**
- Core content loader (System.Text.Json into integer types, ASCII rule
  strings, canonical p/q rationals).
- Load errors name the file and the field.
- `manifestHash`, `displayHash`, `genContentHash` computed at build time.
- Validator framework: qualified ids `Vn.Name`, phases s/g/r/t,
  `CheckRow`/`ReportRow`/`ArmRow` registries, build tiers 0–6 with
  caching keyed on each check's read set.
- `V7.NameRegistry` loaded from the naming pass name list.
- V24 static rows r1, r2, r6; the check that every greyed-reason id has a
  display key.

**Done when:**
- [ ] A missing field, a bad reference and a duplicate id each fail the
  build with file and field named (fixture per case).
- [ ] Content validation runs as part of `dotnet test`.
- [ ] V19s `ClassDeclared`: every registered check declares one failure
  class.
- [ ] V24 r1, r2, r6 pass.

### M1-05 Stream registry and draw ledger

**Goal:** Every random stream is a registered row, opened only through a
handle, with its draw count checked.

**Implements:** SV 4.2, 4.3, 9.1, DS1–DS3, DS15; SV-2, SV-31.

**Depends on:** M1-04.

**Delivers:**
- `content/registry/streams.json` (`StreamRow`: path, count class, count,
  stage, retry scope, owner, wave).
- Source-generated stream handles; an unregistered path does not compile.
- Fresh streams per stage run; `DrawLedger` closed at each stage end.
- M1 rows registered: `route` stub, `floor`, `attempt`, `layout`,
  `dressing` (display side), `terrain` (with `hzTagMax` reserved), `traps`
  (+`retry1`), `encounters/plan`, `encounters/place` (+`retry1`),
  `trapsResolve` (+`retry1`), `checks` (`h:object`, `h:save`, `h:notice`,
  `h:steal`, `h:lurkStealth` reserved and inert), `combat`,
  `loot/floor` blocks, `lootPlan` (`lineRoll` reserved and inert;
  `bossCard` registered with `wave: M2`, inert, per D-08), `loot/run`.
- Inert rows: a row whose registry wave is later than the build's wave
  is expected to draw 0 (R-SEED-3 as amended by D-08).
- The M0 shrine stream `loot/shrine` (sequential-saved, added by M1-02 for
  the demo boons and not in SV 4.3) is either registered or removed with
  the shrine; it must not stay an unregistered path.

**Done when:**
- [ ] V19s stream-row completeness, `DomainDisjoint` and the 64-bit
  collision scan pass.
- [ ] The DrawLedger expects 0 draws on `loot/lootPlan/{N}/bossCard/*`
  in an M1 build, and a draw on it fails (test).
- [ ] A fixture stage that draws a different count from its row raises
  `DrawLedgerMismatch` (test).
- [ ] V19t `StreamIndependence` passes.

**Out of scope:** V19s completeness over stages, checks and reports
(M1-27, once the stages exist); stream counts owed by other owners
(`hzTagMax` in M1-30; traps formulas in M1-30).

### M1-06 Reproducibility key and run configuration

**Goal:** Every run is identified by a key that fixes everything that can
change its outcome.

**Implements:** SV 4.6; MAIN 6.6; SV-12 (fields only), HL-14.

**Depends on:** M1-05.

**Delivers:**
- Key (`runSeed`, `genVersion`, `manifestHash`, `configHash`,
  `unlockHash`); `rulesVersion` and `saveVersion` in saves.
- Run config snapshot type with reserved `difficulty.tier`, `boons[]`,
  `nightmareTier`, `runModifierId`; `route.floorCount` = 1 (in the
  snapshot and so in `configHash`; D-08).
- V22 registered with only row (m) live.

**Done when:**
- [ ] V19t `ConfigHashCoversRunConfig` passes.
- [ ] V22t (m) `RunModifierIdInConfigHash` passes.
- [ ] The same key twice gives byte-identical state (test).

**Out of scope:** the GS0.2 snapshot stage itself (M1-27).

---

## Phase B: Rules core

The combat rules, built and tested headless. The effects system is the
milestone's main technical risk (MAIN 6.4); it comes first.

### M1-07 Effects system

**Goal:** Every rule exception is a modifier or a trigger, folded and
fired the same way whatever its source.

**Implements:** MAIN 6.4; SM 3, 9; AB 6.1, 6.2, 14.3–14.4; CO 6.7,
17.1–17.2; DH 20.3–20.4.

**Depends on:** M1-06.

**Delivers:**
- The modifier and trigger vocabulary (builders' call, AB 18, CO 23),
  written up in `docs/`.
- Effect-folding queries: nothing derived is stored.
- Non-stacking and same-number rules.
- Trigger order: priority, then source unit order, then creation order.
- C# hooks referenced from data by ID.

**Done when:**
- [ ] Unit tests for folding, non-stacking, the same-number rule and
  trigger ordering pass.
- [ ] A test effect defined only in JSON (no new C#) changes a derived
  number and fires a trigger.
- [ ] The vocabulary document is approved by the other developer in the
  step's pull request.

### M1-08 Stats and contests

**Goal:** Units have abilities, derived numbers, saves and skills, and
every contest is one d20 roll on the combat stream.

**Implements:** SM 5.1, 6, 7.1–7.2 (M1 skills), 9; DH 9 (crits).

**Depends on:** M1-07.

**Delivers:**
- Six abilities as single modifiers; every SM 6 derived number except
  trap sense.
- Three saves; attack, save and skill-vs-unit contests.
- Natural 20 and natural 1 on attack rolls only.
- Initiative d20 + Int, ties by Int then unit order.
- M1 skills: Athletics, Acrobatics, Stealth, Mechanics, Perception,
  Intimidation, Leadership (`rally` cure only), Medicine (rest bonus
  only), Lore.
- `combat` stream as a saved sequential counter.

**Done when:**
- [ ] SM 11 unit tests for each derived number and the non-stacking rule
  pass.
- [ ] Natural 1 and 20 tests pass.
- [ ] A replay of a scripted contest sequence is identical.

### M1-09 Damage and HP

**Goal:** Damage and healing follow the single normative damage function.

**Implements:** SM 8.3–8.4, 9; AB 9.1 (M1, no riders); DH 7, 8, 9, 15.2,
20.3; DH-4, DH-5, DH-6.

**Depends on:** M1-08; precondition D-02 (crit wording).

**Delivers:**
- Typed damage parts; the ladder (weak, normal, resistant, immune) and
  traits.
- The DH 20.3 damage function in its normative order.
- Max HP = 11 + L × max(1, tier + Con); Recovery = 4 + 2L + Con (min 1).
- Crits roll all damage dice twice.

**Done when:**
- [ ] SM 11 ladder and order-of-operations tests pass.
- [ ] The DH 7.1 reference table (18/25/32) is reproduced by test.
- [ ] Crit wording checked against SRD 5.2.1 (recorded in the issue).

### M1-10 Turns, combat lifecycle and death

**Goal:** Fights start, run turn by turn and end in the fixed orders the
designs give, and the hero's death ends the run.

**Implements:** AB 6.2, 6.4, 6.9, 7.1, 14.2; DH 10, 20.5; CO 5.5;
DH-15, DH-18; TL-8.

**Depends on:** M1-09.

**Delivers:**
- Turn budget: move, action, bonus, reaction, free interaction.
- Pools store spent count only; refill tags `turn`/`fight`/`rest`/
  `camp`/`never`; one camp spend per unit per turn.
- `combatActive`; combat-start resets; combat-end order (turn end →
  `combatOnly` ends → get-ups → XP and level-ups → flag false).
- Level-ups banked to combat end; level-ups never refill.
- `RunEnded{cause, killingEventId?}`, cause `heroDeath` (with
  `killingEventId`), `victory`, or `unravelled` (reserved, M2); every
  command rejected after it (DH 10.1 as amended by D-08).
- Out-of-combat 1-HP floor; undetected-trap floor with
  `detectionAtFire`; the `PartyInstantDeath` denylist.

**Done when:**
- [ ] AB 16 tests for each refill tag and the camp-spend rule pass.
- [ ] DH 21.4 property tests pass (floors before the save chain; HP 5 +
  temp 3 + undetected trap 10 → HP 1).
- [ ] Scenario "hero dies alongside the last enemy" ends the run (test).
- [ ] An opening ambush strike counts as in-combat damage (test).

### M1-11 Abilities, reactions and areas

**Goal:** Every action goes through one UseAbility pipeline, with
reactions and area targeting.

**Implements:** AB 7.3, 8.1–8.2, 14.3, 14.6, 15.4; DH 20.5.

**Depends on:** M1-10.

**Delivers:**
- Declared → validated (one reason code) → cost paid → targets locked →
  per target → resolved; combat-start step before cost.
- Reaction policies `always`, `ifItChangesOutcome`,
  `whilePoolAtLeastHalf`, `never`, plus "ask me" (a pending decision).
- Burst, cone and line areas (Chebyshev, equal cones in 8 directions,
  row-by-row square order, area-origin line of sight, friendly fire
  unless `selective`, 25-square cap at levels 1–6).
- Preview query that shares the resolution code.

**Done when:**
- [ ] Property test: preview equals resolution (AB 16).
- [ ] Cone and burst tests in all 8 directions pass.
- [ ] The AB 14.5 validator "reaction has no automatic policy" passes,
  and an area over 25 squares at level ≤ 6 fails content validation
  (fixture).

### M1-12 Conditions core

**Goal:** Conditions apply, tick and end on one clock through one
pipeline.

**Implements:** CO 5, 6, 7.2, 9, 17; C1; CO 5.7, 6.6.

**Depends on:** M1-11.

**Delivers:**
- Global phase counter, `appliedPhase`, the wearer clock (C1), own-turn
  rule, turn-start and turn-end step order (CO 5.4).
- One instance per condition; re-application by effective end.
- The 9-step application pipeline and entry-point table; all relation
  kinds; `ConditionEnded`/`ConditionResisted`.
- Control guards: Steeled, lost-turn-start cap leading to Unyielding,
  composite lockout, depth gates, party composure, hard exclusivity.
- Test-only fixture conditions covering every relation kind and duration
  model (CO 18.1).

**Done when:**
- [ ] CO 20.2 clock edge-case and pipeline-order tests pass.
- [ ] Each entry point skips exactly its listed steps (test).
- [ ] Property tests: no party unit loses more than 1 consecutive turn
  start; no unit loses more than 2 per fight.

### M1-13 M1 conditions and forced movement

**Goal:** The ten M1 conditions and forced movement work in play.

**Implements:** CO 10.1, 18.1–18.9, 21; W-8, VG-8.

**Depends on:** M1-12; decision D-09 (collision damage).

**Delivers:**
- Surprised, Prone, Grappled, Frightened, Hidden, Dodging, Stunned,
  Steeled, Dazed, Bleeding; engine states Incapacitated, Grappling,
  Unyielding.
- Bleeding ends on any HP gain; tick batches capped at 1/4 max HP.
- Push, pull, shift: at most 3 squares per hit, collision damage at the
  D-09 value, never provokes, stops before hazard squares, grapple drags.
- A generic "stop-short square" predicate, used by hazards now and by
  the Forecourt lock in M1-35 (D-03): the moved unit stops before it
  with no collision and no collision damage.

**Done when:**
- [ ] Tick batch tests pass (seq order, cap, one concentration event).
- [ ] CO 20.1 validator list passes on the M1 data.
- [ ] "M1 pushes and drags stop before hazard squares" (CO 20.2) passes.
- [ ] A collision deals exactly the D-09 value (test).

### M1-14 Universal and skill actions

**Goal:** Every M1 action a unit can take exists and is tested.

**Implements:** AB 7.2; SM 7.1–7.2; W 8 (threatened zone, opportunity
attacks).

**Depends on:** M1-13.

**Delivers:** Attack, Dash, Disengage, Dodge, Help, Shove and grapple,
Escape, Hide, Search, Frighten, Rally; opportunity attacks and threatened
zones; trained-only skill uses.

**Done when:**
- [ ] Grapple and shove size-rule tests pass.
- [ ] Each action has a test for success, failure and its greyed reason.

---

## Phase C: Equipment

### M1-15 Items and inventory

**Goal:** Items exist as data and instances, live in exactly one place,
and move only through logged events.

**Implements:** ITEMS 4, 5.1–5.4, 7.1–7.3, 12.1–12.3, 12.8, 18.1–18.4;
I-1, I-5, I-7; W 11.1–11.2; SV-15.

**Depends on:** M1-14.

**Delivers:**
- `inventory.json`, family and record schema, `ItemInstance` with a
  single `location`, `instanceId` counter, `everHeld`, `stackMax`.
- Item events (`ItemCreated` … `ItemDestroyed`, `CoinChanged`,
  `MarkerCreated`) and the transition table.
- Active and spare weapon sets, body armour, belt of 2, pack = max(2, 3 +
  Strength).
- Combat costs: set swap (bonus action), Retrieve (action), free pickup,
  no armour change in combat; greyed reasons.
- Coin purse counter.

**Done when:**
- [ ] ITEMS 19.2 invariants pass (one location per instance; belt rules;
  over capacity only from a capacity loss).
- [ ] No inventory command draws a random number (test).
- [ ] Replay rebuilds inventory from events.
- [ ] Capacity reads Strength without conditions (test).

### M1-16 Weapons and attacks

**Goal:** Weapon attacks run through the full pipeline with the M1
families and verbs.

**Implements:** W 6.1–6.4, 6.6, 7.1–7.6, 8, 9.1–9.3, 10, 11, 13, 15.1,
20.1–20.3; W-17.

**Depends on:** M1-15.

**Delivers:**
- 15 M1 families (dagger, rapier, longsword, greatsword, handaxe,
  greataxe, mace, greatclub, warhammer, flail, pike, spear, longbow, light
  crossbow, sling) plus improvised and unarmed.
- Exploit (1 die at levels 1–3); ranged disadvantage in melee; thrown;
  Loading; Strained; soft training; dual wielding.
- Verbs: Push, Topple, Cleave, Skewer, Nick, Lunge, Aim, Parry, Receive,
  Graze, Pry.

**Done when:**
- [ ] W 21.2 unit tests pass (including Skewer in 8 directions).
- [ ] W 21.1 content rules 1–16 pass.
- [ ] Preview equals resolution for every family (property test).

### M1-17 Armour, AC and shields

**Goal:** Armour class is a query, and armour and shields behave as
designed.

**Implements:** AR 6, 7, 8, 10.1, 10.3–10.5, 12.1–12.3, 16.1–16.3;
AR-8, AR-40, AR-41, AR-51.

**Depends on:** M1-16.

**Delivers:** `AC()` and `VisibleAC()`; leather, hide, chain shirt,
chain mail; unarmoured and natural armour; training and `CanWear`;
Clatter, Weighty, Stiff; verbs Supple, Sure-Footed, Ringed; shield bash,
Raise, Interpose; roll-mode and crit caps.

**Done when:**
- [ ] AR 17.2 tests pass (AC query, caps, gear prevention, shield in
  active vs spare set).
- [ ] AR 17.1 content rules pass on M1 data.

### M1-18 Healing, rests and supply

**Goal:** The hero can heal with potions and short rests, limited by
supply.

**Implements:** CN 7.2, 8, 10, 18.1; DH 8, 11.3, 12.1; AB 15;
DH-37, DH-39, DH-40, HE-1.

**Depends on:** M1-17.

**Delivers:**
- Healing Potion: bonus action from your own belt, heals Recovery;
  stacks of 3.
- Short rest: 20 ticks, no aware hostile, interrupted rests gain nothing;
  completion order (Recovery + Medicine bonus, then `rest` pools).
- Supply counter (start 2, cap 3).

**Done when:**
- [ ] CN 19.2: heal equals Recovery without the Medicine bonus (test).
- [ ] Replay across a rest is identical (AB 16).
- [ ] An interrupted rest changes nothing (test).

---

## Phase D: The Vanguard

### M1-19 Class records and hero creation

**Goal:** A Vanguard can be created with its kit, ready to play.

**Implements:** CL 7.3, 7.4, 12.1, 12.2; VG 5.5, 7.1, 7.10, 7.14, 9.1,
13.1; SK 4.1, 4.2, 5.1, 10; SK-1, SK-3, SK-10; VG-10, VG-11.

**Depends on:** M1-18; precondition D-02 (SRD check).

**Delivers:**
- Class record shape and loader; option records with ranks; `stance`,
  `turnAllowance`, `perTargetRoundCount` kinds.
- Creation: default ability array, 2 of 5 skills, stance pick, ladder
  preview.
- Kit issue at run start (WO1): chain mail; set A warhammer and shield;
  set B greataxe; 2 Healing Potions; coin 15; supply 2.

**Done when:**
- [ ] `content.*` class rows pass (`MissingRestDefensiveTool`,
  `ForbiddenClassGrant`, `ClassGatedContent`, `NameCollision`, cap of 8).
- [ ] V21 (a)–(e) pass; V21t `KitIssueStable` (byte-identical across
  reload and replay).
- [ ] `CoinChanged{kit, 15}` is emitted.

**Out of scope:** kit issue before floor generation is wired in M1-27.

### M1-20 Vanguard level-1 kit

**Goal:** The Vanguard's level-1 mechanics work in combat.

**Implements:** VG 6.4–6.7, 7.2–7.4, 9.2, 11, 12.1; VG-2, VG-6, VG-7,
VG-16, VG-23; CL-6.

**Depends on:** M1-19.

**Delivers:** Patch Up; Techniques (Press I, Hew I, Empower I on the M1
verb lines); Onslaught; Momentum; Bar the Way stance;
`conditions.equiv.json`.

**Done when:**
- [ ] `HewCeiling` and `TechniquePickerSize` pass.
- [ ] Every Momentum clause has a unit test (VG 6.7); `FlagNotCondition`
  passes.
- [ ] Onslaught cannot be combined with another camp spend (test).
- [ ] The composite-lockout invariant holds (VG 12.1).

### M1-21 Experience and levels 2–3

**Goal:** The hero earns experience and levels to 3 with real choices.

**Implements:** ENM 16 (with level scaling); VG 7.6, 8.1; CL 7.3, 7.4;
AB 6.2, 6.9, 10.1; RS 9.4 (the Top-up rule, M1 by D-03).

**Depends on:** M1-20. (D-03 ruled: the floor-1 Forecourt Top-up ships
in M1; see the [rulings record](../superpowers/specs/2026-11-24-m1-scope-rulings-design.md) 2.)

**Delivers:**
- Experience on kill: band values split by frozen cost, each share
  level-scaled (ENM 16, clamped 50–150%, FloorDiv per share), paid once.
- Level 2 talent card: Drive Through, Pivot, Grim Resolve.
- Level 3 subclass card (Weaponmaster, the only M1 subclass): Practised
  Hand.
- Level-up as a pending decision at combat end or, out of combat, at
  once; several levels resolve in level order; HP 21/31/41.
- Core rule `AntechamberCatchUp`: experience := max(current, 540);
  floor-1 run-state flag `antechamberCatchUpApplied`; fires once; no
  draws; overflow recorded. Combat-end order when deferred: kill shares
  at the pre-Top-up level, then the Top-up, then one level-up screen.
  (Its trigger is wired in M1-35.)

**Done when:**
- [ ] Level-scaled shares match a fixture table at hero levels 1–3 on
  floor-1 expected levels.
- [ ] Top-up fixtures: heroes at 200, 455 and 560 experience end at
  max(xp, 540) with one event and no second firing; the 200 hero
  resolves the talent card, then the subclass card.
- [ ] Combat-end order fixture: a hero at 500 with a Forecourt kill gets
  the kill share at level 2 first, then the Top-up.
- [ ] `SubclassCopiesBaseLean`, `SubclassAddsPool` pass.
- [ ] Level-up choices and the flag persist through save and replay.

**Out of scope:** the floor-wide experience check (M1-31).

---

## Phase E: Enemies and the boss

### M1-22 Monsters and the Kennel roster

**Goal:** Monsters are data, checked against permadeath guards, and the
three Kennel types plus the minion exist.

**Implements:** ENM 5, 6.4, 6.5, 7.1, 9.1–9.5, 10.1, 17.1, 19.1; RS 8.4,
12; RT-22; CC-2; DH-26.

**Depends on:** M1-21; decision D-04 (minion base type).

**Delivers:** monster record and derived values; ranks; `readyAt`;
guard checks against the reference hero; Kennel Mastiff, Huntsman,
Poacher and the minion template as content.

**Done when:**
- [ ] ENM 20.1 build-fail list passes.
- [ ] Every guard row passes at the lowest legal party AC (AR-20).
- [ ] V1 `ThemeSix` (as called by V17s (a)) passes for the Kennel's M1
  kit space.

### M1-23 Enemy AI

**Goal:** Enemies choose actions with one integer scorer, fairly and
fast.

**Implements:** ENM 11.1, 11.3, 11.5, 17.3; AB 12.1, 13; CO 17.3; VG-16.

**Depends on:** M1-22.

**Delivers:** the one-step scorer with M1 terms (approach, exposure,
opportunity cost including known triggers, range, hazard path, target
preference, kill bonus); profiles Charger, Holder, Kiter, Mob, Boss;
`uses` and `cooldown`.

**Done when:**
- [ ] Golden scenarios per profile pass.
- [ ] Each ENM 11.3 fairness invariant (1–10) is a passing property test.
- [ ] The AI rolls no dice (test); stall detector passes.
- [ ] p95 decision time < 50 ms over the golden scenarios, measured in a
  Release build on the CI Windows runner.

### M1-24 Old Ragged

**Goal:** The floor-1 boss fights to its approved kit and phases.

**Implements:** ENM 13.2, 17.5; BR 4.9, 5.3, 6; CO 12.3 (C2); AB 12.2.

**Depends on:** M1-23.

**Delivers:** resolve pips; Unyielding; one phase at 50%; phase 1 claw,
bite, roar, slam; phase 2 Pin-Down; life-long state; HP from
`hpByPartySize[1]`.

**Done when:**
- [ ] V17s (a)–(c) pass on Old Ragged.
- [ ] `Floor1BossMindReachable` passes.
- [ ] "Resolve by each route" test passes.
- [ ] DH-26 floor-1 solo boss rows pass with the reference hero and the
  lowest legal hero (tier 6, Con −1, AR-20 lowest legal AC) both at
  level 3 (boss term only; D-03).

### M1-25 Playable combat in Godot

**Goal:** A person can play the Vanguard against the Kennel enemies and
Old Ragged in a hand-built room.

**Implements:** MAIN 6.8; ENM 13.1–13.2; DH 17.3–17.5, 17.8, 17.12, 22
(end-turn warning); XI 5 (threat layer).

**Depends on:** M1-24.

**Delivers:**
- Godot combat scene on a hand-authored test room, with four fixed
  scenario seeds: one per enemy type and one for Old Ragged.
- Turn order, action bar with greyed reasons, previews, threat reach on
  hover, HP plates, combat log.
- Decision panels for reactions ("ask me"), Techniques and level-ups.
- Danger band, lethal tags, end-turn warning, basic death recap.
- Death screen.

**Done when:**
- [ ] Each of the four scenarios has been played to a win and to a loss
  on Windows and Linux, noted in the issue.
- [ ] Hovering changes no state and draws no random number (test).
- [ ] The CI display-layer rule grep passes.

---

## Phase F: Floor generation

Stages are built in pipeline order. The arena is stamped early (GS4);
its fairness checks that run after loot are completed in M1-33.

### M1-26 Den arena template

**Goal:** Old Ragged's arena exists as a proven template ready to stamp.

**Implements:** BR 4.1.1, 4.2.1, 10; BR-1, BR-4; CC-70.

**Depends on:** M1-25.

**Delivers:** Den Guise template; root-columns as sight blockers; two
Arches; start zone; the signature-cache socket.

**Done when:**
- [ ] V17s (f) root-column Deny passes.
- [ ] The Den Guise stamp proof (V17s) passes.

### M1-27 Generation pipeline and route stub

**Goal:** A floor is generated by a fixed sequence of stages with
assertion points and a retry ladder.

**Implements:** SV 4.4, 5.4, 5.5, 5.6; RS 6.4 (R-SEED-1/2/3/5), 11.1;
SV-4, SV-9, SV-10, SV-11, SV-20; RT-3.

**Depends on:** M1-26; decision D-05 (arena stamp position).

**Delivers:**
- Stages GS0–GS17 in canonical order (stubs until their step);
  GS0.2 config, unlock and `drawableSet` snapshot; kit issue before
  generation.
- `attempt/{a}` and `retry1` keys; AP1 and AP2 hooks (act-then-assert).
- Retry ladder LR0–LR4; `GenMaxAttempts` = 3; LR5 hard failure in dev
  and CI; events `StageRetried`, `GenerationRetried`,
  `GenerationFallback`, `GenerationDefect`.
- Route code returning the single Kennel Wood theme; route drawn after
  hero creation; `RouteDrawn` event.
- Draw-ledger gaps handed over from M1-05 (PR #93; the owner deferred
  them here, 2026-11-28):
  - The generated `Open()` on every drawn key bypasses the ledger.
    Emit it only for `sequential-saved` rows (`combat`, `loot/shrine`),
    so counted, inert, vector and display rows open only through
    `StageRun`.
  - A draw on an inert row (D-08) fails only at `Close`; it should fail
    at the draw.
  - `leaf-formula` rows must be checked on their recorded choice (SV 4.2,
    DS3): `StageRun` fixes parameters at construction, so a stage needs
    a way to record a choice (layout's `#Sites`) before `Close`.
  - `Close` checks only the rows a stage opened. With stage rows
    (`StageRow.streams[]`), every live counted row a stage owns must be
    checked, even if never opened ("fixed count" means the counter equals
    its declared count when the stage ends, SV 4.2).

**Done when:**
- [ ] V19t `RetryIsolation` passes.
- [ ] `DevReleaseParity` passes.
- [ ] V6 `RouteDeterminism` stub passes.
- [ ] V19s completeness over every opened path and stage passes.

### M1-28 Floor layout

**Goal:** Floor 1 of the Kennel Wood is laid out as designed.

**Implements:** FL 6.1 (LS1–LS13), 6.3–6.8, 6.11, 7.3, 11.1, 11.2, 12;
FL-2 to FL-6, FL-8 to FL-11, FL-13 to FL-15; DH 12.4 (HE-4, HE-7).

**Depends on:** M1-27.

**Delivers:**
- Layout records: LayoutGrammar, Blueprint, SiteRule, RoomTemplate; the
  Kennel's two Blueprints (Hunting Circle, Lodge Grounds).
- A Godot editor plugin that authors room templates as scenes, exports
  them to core data and runs the template checks on save.
- Socket router, rewrite and side budget, structure and indices
  (`mainPathIndex`, `roomId`, `equivalentHosts`, `fleeExit`).
- Hearing and movement graphs with repair; Circuit; Bypass; Sanctum
  (Forecourt, arena socket, Bossgate, Downstair).
- HE-7 and HE-4 reservations; `layoutDigest`.

**Done when:**
- [ ] V8 M1 rows pass (LG1–LG10, LG12, LG15, LG16, LG18–LG20 and named
  checks: `DrawCount`, `SideBudget`, `HearingGapBound`,
  `CircuitIsolation`, `SkippablePoolProof`, `HE7Sockets`).
- [ ] Runtime asserts pass on 1,000 seeds: `MainPathConnected`,
  `NothingInWalls`, `PortalMinimums`, `IndexIntegrity`, `HE7Reserved`.
- [ ] On 1,000 seeds, arena squares are reachable out of combat only
  through the floor-1 Forecourt (the premise of D-03), and the Downstair
  stairs square is reachable through the Bossgate once it opens (D-08).

### M1-29 Arena stamp

**Goal:** The Den is stamped into every floor 1 at its pipeline position.

**Implements:** BR 4.1.1; SV-9; CC-68.

**Depends on:** M1-28.

**Delivers:** the stamp at the position fixed by D-05; AP1 stamp check.

**Done when:**
- [ ] `V17g.StampProven` passes at AP1.
- [ ] `ArenaStampFallback` = 0 over 1,000 seeds.

### M1-30 Terrain, traps and locks

**Goal:** Floor 1 has its blocking terrain, locks and lesson traps, with
hazard draws reserved.

**Implements:** ROOM 6.2.1–6.2.5, 7.3, 12; TR-3, TR-5, TR-17, TR-19;
HZ 11.2, 12; SV-3; TL 6.1, 6.2, 6.6, 6.11, 6.19, 6.22, 7.3, 12; TL-1 to
TL-5, TL-19 to TL-21; DH-32.

**Depends on:** M1-29; decisions D-06 (floor-1 traps and keys) and D-10
(`hzTagMax`, traps formulas).

**Delivers:**
- Tall and Low pieces as blockers; Clearway; clutter bands; reservation
  tags with `hzTagMax` dummy draws.
- Keyed object checks on `checks` (`h:object`); floor-1 DC ladder.
- Plain Locked Portals: Pick, Take time, Force (third try opens);
  party door open-by-moving.
- Trap records with `detectionAtFire`; `traps` and `trapsResolve` passes
  with fallbacks.

**Done when:**
- [ ] V9 M1 rows pass: `ClearwayConnected`, `ProtectedIntact`,
  `FreeSpaceAfterClutter`, `RoomMixAfterClutter`, `ClutterBand`,
  `ReservationCapacity`, `WayRound`, `CutawayWithTall`,
  `TerrainDrawCount`.
- [ ] V11 skeleton passes (`LockNeverCritical`, `HeNoLock`,
  `KeyReachable`); `F1TrapRules` passes.
- [ ] Swapping party members gives the same check roll (test).
- [ ] The solo Vanguard can always force every lock (test).

### M1-31 Encounter placement

**Goal:** Enemy groups are budgeted and placed on floor 1 as designed.

**Implements:** ENM 10.1–10.5, 16, 17.4; EP 6.1, 6.2, 6.6.1, 6.6.6,
6.17, 6.19, 7.3, 12; EP-1, EP-4, EP-11, EP-13, EP-32.

**Depends on:** M1-30; decisions D-04 and D-07 (M1 patrol and start
shapes).

**Delivers:**
- Party weight 15; floor-1 budgets (patrol 8, guard post 12, lair 16);
  slot plan.
- Templates Picket, Pack, Line and Bow, Boss Court; formations Picket
  Line, Door Line, Crescent; Patrol File start shape; the solo table
  with its formation column.
- The floor-1 placement package; Keybearer candidate guarantee.
- `encounters/plan` and `encounters/place` with K = 4 tries and
  fallbacks.

**Done when:**
- [ ] ENM 10.2 worked examples pass as content tests.
- [ ] V12 static checks pass, including `FirstSight`, `LandingShooters`
  and `Floor1NoCoveredStarts`; V12g floor-1 assertions pass.
- [ ] "The M1 floor fills from M1 templates alone" passes.
- [ ] A retry never changes layout, terrain or traps (test).
- [ ] `FormationFallbackRate` < 5% on floor 1 over 1,000 seeds.
- [ ] Floor experience is 110–135% of need after one bypass, over 1,000
  seeds. (A pacing check only: the level-3 guarantee is the Forecourt
  Top-up, D-03, so the patrol outcome of D-07 cannot change the door
  level.)

### M1-32 Curiosity stub

**Goal:** The Curiosity hooks exist so later stages and checks run in
their final order.

**Implements:** EV 9.1, 9.3, 10; EV-40; SV 5.2 (V16 M1 stub).

**Depends on:** M1-31.

**Delivers:** `CuriosityRecord`; empty `curiosityKit` on the theme;
EventDraw and EventPlace stages as stubs; Curiosity event payloads; V16
registered as a stub.

**Done when:**
- [ ] Records load and every payload serialises (test).
- [ ] V16 stub runs at its AP2 position and passes.

### M1-33 Floor-1 loot and floor commit

**Goal:** Floor 1 deals its loot, coin and guaranteed healing exactly as
designed, and the finished floor is committed and saved.

**Implements:** LE 6.3, 6.4, 6.6, 6.10, 10.1, 11; IR 4.1–4.3, 4.9, 4.13;
LP 4.1–4.4, 4.9, 10; ENM 15; DH 12.4, 12.5; ITEMS 9; BR 10 ("Guards:"
line); SV 4.7, 4.8.1, 4.8.2, 4.8.3 (WO1 M1 form), 5.2; MAIN 6.6;
ITEMS 10.5 steps 1–2 and item 6 (Scrap); LE-1, LE-2, LE-7, LE-8,
LE-13, LE-14 (WO1 form); IR-1, IR-10, IR-13, IR-14; LP-1 (`bossCard`
kind enumerated, inert); LP-11, LP-12; SV-5, SV-14 (M1 part), SV-17;
DH-27; HE-3; rulings record 3 (D-08).

**Depends on:** M1-32; decision D-10 (LP V15 rows, IR filter-first, DH
reshuffle, phial draws). (D-08 ruled: no boss card in M1; coin drops
with no sink; Descend from floor 1 is victory.)

**Delivers:**
- Loot keys; SocketPlan, LootPlan, step 3, LootBind.
- Plain containers and the pen box; rarity deck C C U U U; item pipeline
  (filter-first identity draw).
- Dream's Due for the hero; coin pool 35 (salvage 4, found 31, main-path
  floor 17).
- Old Ragged's guarded Hound-Collar Mail with its "Guards:" line;
  `BossDefeated` opens the cache and the Bossgate and does not end the
  run.
- `LootLedger` with kit coin on its own `kit` line outside Ledger C and
  a `bossCard` line fixed at 0; the LootPlan `bossCard` field serialised
  empty.
- The GS17 floor-commit routine in SV 4.8.3 order (static file written
  and flushed, then `run.save` seq 1 by atomic replace), called at WO1;
  M2-19 reuses it unchanged at WO5.
- Core descent pipeline: left-behind warning data (leftovers markers,
  flee piles, seen-but-unopened containers by name only), Descend or
  Stay, per-instance Scrap (`ItemScrapped`, `CoinChanged{scrap}`),
  `ItemDestroyed{lost}`, then `RunEnded{victory}` when the floor is
  `route.floorCount`.
- A placeholder `basePrice` > 0 on every family that can be scrapped in
  M1 (SK 8.4 scale).
- Guaranteed main-path potion; leftovers and flee piles.
- M1 weapon and armour notables as content.
- AP2 in its full order; `FloorStatic` named by content hash;
  `GenInputs` (hero class, species field reserved, kit).
- `run.save` with temp file, flush and atomic replace; `SaveHeader`
  (`runId`, `seq`, versions); replay header with key, `rulesVersion`,
  hero inputs and rolling state hashes every 64 commands.
- Pinned `FloorStatic` vector in `KeyAlgebraVectors`.

**Done when:**
- [ ] V13 (a)(c), (g)(i) on F1 pass; V13 (m) passes with its WO1 clause
  and the inert `bossCard` (its WO5 clause is M2-19).
- [ ] `LootLedger` F1: Ledger A 6, Floor VP 11, `bossCard` line 0,
  Ledger C = 35 (salvage 4 + found 31), kit 15 on its own line (test).
- [ ] No `loot/lootPlan/1/bossCard` key is drawn; the field round-trips
  empty (test).
- [ ] A crash injected after the static file but before `run.save`
  seq 1 leaves no run; load deletes the orphan (test).
- [ ] Descend scraps the sum, per instance, of FloorDiv(base × count ×
  scrapPermille, 1000) (test); a family with no `basePrice` fails content
  validation (fixture).
- [ ] `RunEnded{victory}` is the last event, after the descent events,
  only on Descend from floor `route.floorCount`, never at `BossDefeated`
  (test).
- [ ] V14 (a)(b)(d)(h)(j) and the draw test pass.
- [ ] V15 M1 rows listed in LP 9.2 (or the D-10 fallback) pass.
- [ ] V17g start zones and lanes pass.
- [ ] V19t `AP1EarlyEquivalence`, block independence and the
  `LootDoorInvariant` harness pass.
- [ ] V21 (f) `KitDueStrictUpgrade` passes.
- [ ] `GuaranteedItemUnreachable` = 0 and `HealingFloorUnmet` = 0 over
  1,000 seeds.
- [ ] `SaveDigestMatch` passes; the same key gives identical
  `FloorStatic` bytes on Windows and Linux.
- [ ] A replay with a mismatched `rulesVersion` is rejected with a typed
  error; loading a save never regenerates a floor (test).

---

## Phase G: Exploration

### M1-34 Sight and party knowledge

**Goal:** What the party knows about the floor is core state, derived
only from what it has seen and entered.

**Implements:** XI 4.1.1–4.1.4, 4.2, 4.11, 4.13; XI-1, XI-2, XI-3, XI-5.

**Depends on:** M1-33; decision D-11 (M1 Afterimage states).

**Delivers:**
- `CanSee` (integer symmetric shadowcasting), `KnownCanSee`,
  `KnownNoiseReach`.
- `PartyKnowledge`: Unknown, Chalked, Remembered, Seen; `entered`.
- `KnowledgePass` in fixed order (Hairline step stubbed).
- Chalk on entry; remembered rows; the D-11 Afterimage states.
- `KnowledgeLeak` fuzz harness; `IKnowledgeQuery` as the only route for
  display.

**Done when:**
- [ ] V18t `LosSymmetric`, `LosVsLineAgree`, `FogEqualsRules`,
  `ReplayView`, `AfterimageHonesty`, `SaveReloadKnowledge` pass.
- [ ] V18r `MapNeverUnknown`, `ChalkNeverContents`, `ChalkNeverStub`
  pass.
- [ ] `KnowledgeLeak` smoke runs in the PR tier, including a case for
  the left-behind warning data (it never names the contents of an
  unopened container or a living monster; D-08).

### M1-35 Awareness, noticing and the explore–combat loop

**Goal:** Exploring the floor leads into fights the way the designs
describe, and back out again.

**Implements:** ENM 11.2, 12.1–12.5, 10.4; EP-2 (recorded only), EP-16
(static parts); DH 17.1 (DH-2); SV-6; RS 9.4 and BR 4.2.1 (Forecourt
Top-up trigger and F1 guard, M1 by D-03); BR-3.

**Depends on:** M1-34.

**Delivers:**
- Group awareness records; noticing triggers; out-of-combat `h:notice`
  checks; Hidden rule; noise as path distance; joining (max 1 on floor
  1) with footsteps warning; pursuit; floor-1 surprise rule; boss arena
  counts as noticed; fight end back to exploration.
- Forecourt Top-up trigger (D-03): an out-of-combat move ends on its
  first Forecourt square and, after that step's `KnowledgePass`, fires
  `AntechamberCatchUp` (or defers it to combat end if the pass started
  combat); arena squares refused at no cost while the lock holds; party
  forced movement stops short before arena squares.
- F1 guard (BR-3): while the flag is false the boss group cannot notice
  the party, ignores noise, cannot join a fight and stays in the arena.

**Done when:**
- [ ] V19t `ExplorationCombatIsolation` passes.
- [ ] V17t M1 row: `AntechamberCatchUp` precedes `ArenaEngaged` in the
  command log; no arena entry is accepted and no `ArenaEngaged` fires
  while the flag is false or its level-up is pending.
- [ ] V19t `TopUpNoDraw`: the `combat` counter is unchanged and the
  DrawLedger attributes no draw to the Top-up or its level-up.
- [ ] Fixtures: an enemy chases the hero into the Forecourt (Top-up
  applies at combat end, arena stays locked until it resolves); a
  Shove toward an Arch doorway leaves the hero in the Forecourt with no
  collision damage; with the flag false, Old Ragged neither notices a
  hero in Arch view nor answers noise.
- [ ] The Hidden-rule test passes; the `h:notice` reference vector
  (d20 = 14) matches.
- [ ] "No party surprise on floor-1 squares 0–1" passes.
- [ ] Scenario tests: a joining group's footsteps marker appears at least
  1 round before it arrives, in each joining case of ENM 12.4.

---

## Phase H: The full slice in Godot

### M1-36 Floor rendering

**Goal:** The generated floor is drawn from theme kit pieces, readable
from every camera angle.

**Implements:** MAIN 6.8; FL 8.0, 8.1; ROOM TR-19, TR-20; XI 4.3.1,
4.9.4, XI-7, XI-16, XI-17.

**Depends on:** M1-35.

**Delivers:**
- Kennel greybox kit (walls, Portals, Tall and Low pieces, root-columns)
  assembled from `FloorStatic`.
- Cutaway and stub walls at 4 rotations.
- Knowledge-state looks (fog bank, chalk, remembered, seen edge);
  Afterimage cut-outs.
- Minimap; dressing from the display-side `dressing` stream only.

**Done when:**
- [ ] `CutawayPockets` and `TemplateReadability` pass.
- [ ] Greyscale pair checks (RS 7.4) pass at all 4 rotations
  (screenshots attached to the issue).
- [ ] Clicks map to the right square at all 4 rotations on a generated
  floor.

### M1-37 Interface, flows and accessibility

**Goal:** A whole run can be played start to finish without developer
tools.

**Implements:** CH 10; XI 4.9.4, 4.10, XI-20; ITEMS 6.3, 8, 9; CL 7.4;
RS 9.4 (D-03); SV 4.8.3 WO6 (M1 form, D-08); BR 4.13 (M1 reading).

**Depends on:** M1-36.

**Delivers:**
- Explore and combat HUD modes; loot summary; inventory and equip
  screens; level-up cards, including the out-of-combat level-up that
  blocks every other command until resolved.
- The greyed arena move preview (known squares only) with the Forecourt
  Top-up reason line and its overflow number.
- The left-behind warning screen (Descend or Stay; Stay changes
  nothing).
- Run end, M1 form of WO6: `run.save` atomically replaced with a copy
  carrying the result record; victory or death screen; run record;
  `run.save` and the floor static file removed; Begin. A save carrying a
  result record loads straight to the run record. The victory screen
  shows no coin.
- Hero creation screen.
- Rebinding (with OS-reserved chord check), text scale to 150%, glyph
  scale, hold-or-toggle overlays, reduced motion.

**Done when:**
- [ ] The developer who did not build this step creates a hero, plays
  floor 1 to death and to victory, and starts again, on Windows and
  Linux, using only the game's own screens (noted in the issue).
- [ ] `CardFit` passes at 150% text scale, including the Forecourt
  Top-up line and the two-card level-up sequence.
- [ ] Death and victory each remove `run.save` and the floor static
  file; Begin starts a fresh seed (test).
- [ ] A crash injected after the result record is written and before the
  files are removed reloads to the run record, never to a live run
  (test).
- [ ] The signature cache can be looted after `BossDefeated` and before
  Descend (scenario test).

---

## Phase I: Proof

### M1-38 Simulator skeleton

**Goal:** The balance simulator plays floor 1 headless and reports.

**Implements:** MAIN 7; SV 6.1, 6.3; SV-21, SV-23; DH 21.1, 21.5;
VG 11; AB 14.6.

**Depends on:** M1-37.

**Delivers:** `Sim` console app with the S instrument on floor 1;
reports with declared result classes; DH 21.5 rows except those that
need a camp, companions or a party; the VG 11 rows; measurements of
floor time, stage times, retry correlation and fsync latency. The bot
descends once the boss is dead and its cache looted; outcomes are
"cleared" (no placed group has a living member at Descend) and "left
early"; win rate = Descend from floor 1 (D-08).

**Done when:**
- [ ] `Floor1BossSustain` passes (BUILD).
- [ ] `AntechamberCatchUpRate` (Vanguard; bypass and full clear) and F1
  `BossDeathShare` are printed as reports (D-03).
- [ ] The F1 smoke run completes on CI.
- [ ] The four measurements are filed as a report against SV 8.7.

### M1-39 Golden seeds and release set

**Goal:** Floor-1 generation is frozen by recorded fixtures and a
release seed set.

**Implements:** SV 4.11, 5.6; SV-18.

**Depends on:** M1-38; every SV 4.5 lock applied or its fallback adopted
(D-10).

**Delivers:** 16 floor-1 golden fixtures and 4 replays chosen by set
cover; the F1 release set (2,000 runs per cap); the digest manifest.

**Done when:**
- [ ] A digest change without a `genVersion` bump fails CI.
- [ ] Digests match on two OSs.
- [ ] Release set: `GenerationRetried` = 0; the LR4 rate is within the
  SV 5.6 budget, with the budget value used recorded in the issue.
- [ ] Of the 4 replays, one includes a bypass and the Forecourt
  level-up, and one a Top-up deferred to combat end (D-03).
- [ ] Runtime assert over the release set: every arena arrival has
  `antechamberCatchUpApplied` true and the hero at level 3 or higher.

### M1-40 CI tiers

**Goal:** Every M1 check runs in a defined CI tier within its time
budget.

**Implements:** SV 5.8; SV-28.

**Depends on:** M1-39.

**Delivers:** PR tier (≤ 10 min), main tier (≤ 30 min), minimal nightly
(cross-OS × 2, hostile environment, F1 pooled budgets,
`DevReleaseParity`); the PR digest gate.

**Done when:**
- [ ] Every row of the M1 sign-off checklist names the tier that runs
  it.
- [ ] The hostile-environment run (tr-TR culture, non-UTC timezone, 1 and
  N cores, invariant globalisation on and off) is green.
- [ ] One full nightly run is green.

### M1-END Sign-off and playtest

**Goal:** M1 meets its sign-off checklist and the slice answers "is the
loop fun?".

**Implements:** SV 7 "M1"; MAIN 8.

**Depends on:** M1-40.

**Done when** (SV 7 M1, condensed; the full list is in SV 7):
- [ ] Seeds: SV-1 frozen; streams registered; owner deliverables in or
  fallbacks adopted; GS0–GS17; AP1 and AP2; LR0–LR3 and LR4 rows;
  reproducibility key.
- [ ] Validators: V8 core; V9 partial; V11 skeleton; V12 static + chain;
  V13s (a)(c), V13g (g)(i) on F1, (m); V14 (a)(b)(d)(h)(j); V15 M1 rows;
  V16 stub; V17 subset (with the D-03 V17t row); V18 tests; V19s/g/t
  M1 rows (with `TopUpNoDraw`); V21 (a)–(f); V22 (m); V24 r1, r2, r6.
- [ ] Rulings D-03 and D-08 are in force as built: every arena arrival at
  level 3 or higher; victory on Descend; no boss card; the client has
  been told plainly of the RT-8 and BR-3 restaging.
- [ ] Content suites green; `Floor1BossSustain` green.
- [ ] Goldens recorded; release set clean; run save with `FloorStatic`
  and `GenInputs`; `SaveDigestMatch`.
- [ ] Simulator measurement report filed.
- [ ] CI PR, main and minimal nightly tiers green.
- [ ] The M1 playtest is held with at least one player who did not build
  the game. Its notes answer the two M1 questions ("is the loop fun?",
  "does the effects system hold?") and are filed in `docs/`.
- [ ] No REL, SW, HOLD or SHIP verdict is claimed (not M1).
