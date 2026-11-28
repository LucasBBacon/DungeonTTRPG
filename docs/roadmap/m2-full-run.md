# M2 Full Run

**Question it answers:** Does a 1–2 hour run hold together? (MAIN 9)

**Scope:** four classes (Vanguard, Shade, Arcanist, Herald) to level 10;
pick-one-of-three offers; companions (cap 3, with the pre-authorised cap-2
fallback); 5 floors drawn from 8 themes with 15 bosses; camps between
floors and a simple hub screen; Curiosities; full exploration; save and
resume through the suspend slot; the profile file (CC-11 moves it here
from M3); the full simulator.

**Shape of this milestone:** build steps M2-01 to M2-24 first, then the
gated balance sequence SV 7 requires (M2-G1 to M2-G4), then sign-off.
SV 7 states that each gate must pass before the next starts. Checks that
need many full runs (offer variety, morale rates, simulator reports) are
read in M2-24 or the gates, not in the step that builds the feature.

**Sign-off checklist:** SV 7 "M2".

Steps in this file are coarser than M1's. Each is still one testable
capability; its task plan will split it further when it starts.

---

## Phase A: Rules breadth

### M2-01 Riders and layers

**Goal:** Damage riders and layers work for every unit.

**Implements:** SM 8.2, 8.5; AB 9.1–9.4; C8, C9.

**Depends on:** M1-END.

**Done when:**
- [ ] AB 16 "every rider bound" passes.
- [ ] One rider per target per use, and at-will riders once per turn
  (tests).

### M2-02 M2 conditions, Downed and movement states

**Goal:** The 22 M2 conditions, Downed, Flying and Jumping work, with
Treat and tonics.

**Implements:** CO 8 (C4), 18.1, 21; DH 11 (DH-11, DH-21); AB 7.2.

**Depends on:** M2-01; decision D-20.

**Done when:**
- [ ] CO 20.1 validators pass on the M2 catalogue.
- [ ] Scarred tests pass; DH 21.4 Downed inertness properties pass.
- [ ] `LiftsDownedUnapproved` passes.

### M2-03 Items, second wave

**Goal:** Rarity tiers, rolled lines, identification, and the M2
weapons, armour, trinkets and consumables exist.

**Implements:** ITEMS 6, 11, 12.4–12.5; W 22; AR 9, 10.2; TK 13–17, 19;
CN 7.3–7.4, 11–16; IR-4; TK-11 to TK-17; DH-41, DH-44.

**Depends on:** M2-02.

**Delivers:** the D-22 step ([M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 1, 6.2 MS-42):
- D-22's halving in the shared damage function (DH 20.3 step 2), lifting
  the load refusal of `half`.
- The per-part save shape (AB 14.1) and Frost Shard (CN 16.4).
- The single D-22 and D-23 refactor of the damage function, and the
  D-23 lift too if this step's consumables can be used out of combat
  without starting a fight (**lead's call**; record 8 items 12 and 17).

**Preconditions:** the AB owner answers whether an area rolls damage
once or per target (record 7 item 14), and whether a Minion that
succeeds attempts a flagged rider (record 7 item 15).

**Done when:**
- [ ] ITEMS 19.1 rules 14–31, AR 17.2 M2 tests, TK 18.2,
  CN 19.1–19.2 pass.
- [ ] Identification tests pass.
- [ ] Reprieve Phial tests pass.
- [ ] The D-22 tests of the [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 6.3 pass.

### M2-04 Offers and levels 4–10

**Goal:** Pick-one-of-three offers and level-up to 10 work, proven on
the Vanguard.

**Implements:** CL 7.5, 12.3; AB 10.2–10.3; VG 14 (all M2 rows),
VG-4, VG-12, VG-17, VG-19, VG-20; SM 7.2.

**Depends on:** M2-03.

**Done when:**
- [ ] CL-2 cap validator passes; `CampOptionBound` passes.
- [ ] Offer `requires` tags resolve; the offer draw never makes a fifth
  read (test).

### M2-05 Shared spellbook

**Goal:** Slots, concentration and spell-card draws work for any caster.

**Implements:** CL 7.4; ARC 7.7; HR 7.5–7.7; AB 6.5.

**Depends on:** M2-04.

**Done when:**
- [ ] `DrawSatisfiable`, `StarterDefaultsHaveAnswer`, `SpellTierBound`,
  `SlotlessCastBound` pass.

### M2-06 The Shade

**Goal:** The Shade is playable to level 10 (Cutthroat and Swordsworn).

**Implements:** SH 11, 14; SH-4, SH-9; NM-1, NM-2.

**Depends on:** M2-05.

**Done when:**
- [ ] SH 12.1 named tests pass; `OpeningCeiling`, `HideLimit`,
  `TricksPickerSize` pass.
- [ ] The Shade tests of the [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 6.3 pass: Tumble Clear halves
  per part; Duck III never applies to damage halved by a save (**lead's
  call**).

### M2-07 The Arcanist

**Goal:** The Arcanist is playable to level 10 (Binder, Alembic).

**Implements:** ARC 12, 15; prerequisites EL-1, LM-0, DEP-A1.

**Depends on:** M2-06.

**Done when:**
- [ ] ARC 14.2 validators and 13.1 tests pass.
- [ ] `KnownVariantInPair` property test passes.
- [ ] The Arcanist marks count the halved parts (ARC 10.3; D-22,
  [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 1.4).

### M2-08 The Herald

**Goal:** The Herald is playable to level 10.

**Implements:** HR 12, 15; CL-1; prerequisites HR-14, HR-16 to HR-19,
HR-21, HR-22, HR-24.

**Depends on:** M2-07.

**Done when:**
- [ ] HR 14.2 validators and HR 8.9 spell tests pass.
- [ ] `NoHealInDefaults` passes.
- [ ] Toll shows its "on any outcome" marker; Pronouncement (8d8) passes
  the per-record preview check (D-22, [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 1.3, 1.6).

### M2-09 Starting kits for every class

**Goal:** All six class picks start with their designed kits.

**Implements:** SK 4.5, 10; SK-2, SK-11.

**Depends on:** M2-08.

**Done when:**
- [ ] V21 (g)–(j) pass; `KitDisplayInert` passes; every kit family has a
  `basePrice`.

---

## Phase B: Enemies and themes

### M2-10 Enemy roster and AI, full

**Goal:** All enemy roles, elites, affixes, morale and intent reading
exist.

**Implements:** ENM 7, 10.6, 11.1, 11.4, 12.4, 13.3–13.4, 14, 17.1–17.2,
21; RT-5.

**Depends on:** M2-09; decision D-21.

**Done when:**
- [ ] ENM 20.2 morale and guile tests pass.
- [ ] Banned affix pairs fail content validation (fixtures).
- [ ] Volatile's death burst halves per part on a success (ENM 17.2;
  D-22, [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 1.1).

### M2-11 Themes, grammars and rosters

**Goal:** Eight themes exist with their rosters, Blueprints and room
templates.

**Implements:** RS 6.3, 8, 12; RT-1, RT-2; FL 7 (all grammars), 12.

**Depends on:** M2-10.

**Done when:**
- [ ] V1–V5 and V7 pass; V5 `RouteEnumeration` covers every drawable
  set.
- [ ] Every theme roster is at least half non-fearless.

### M2-12 Layout, terrain and Portals, full

**Goal:** Every M2 layout feature, Portal kind and terrain rule
generates and validates for every theme.

**Implements:** FL 6.6–6.11, 12; FL-7, FL-8, FL-17 to FL-20; ROOM (M2
rows: cover, furnishings, Kegs, Rough, Plank, KP1–KP9, arena phase
sockets).

**Depends on:** M2-11.

**Done when:**
- [ ] V8 full passes, including `LayoutDistinct`, `NoSealedPocket`,
  `HairlineCue`, `SecretNeverCritical`.
- [ ] V9 full passes.
- [ ] Keg `reflexHalf` uses the shared halving routine: 4/6/8 → 2/3/4 on
  a success (ROOM 11.1; D-22, [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 1.2).

### M2-13 Condition terrain and hazards

**Goal:** Hazards and condition terrain generate on floors 2–5 within
their budgets.

**Implements:** CO 10.2–11.5; HZ 1–12 (M2 rows); ROOM TR-10, TR-11,
TR-13, TR-25, TR-26; HZ-1, HZ-28; SV-31.

**Depends on:** M2-12; the HZ hazards draw formula published.

**Done when:**
- [ ] V10 `RoomHazards` rows pass (`HazardPointsWithinBudget`,
  `OneClockPerRoom`, `F1NoHazards`, …).
- [ ] `onEnter` fires once per entry; `TrapLethalityBound` passes.
- [ ] Floor-1 golden digests are unchanged (the `hzTagMax` reservation
  held).
- [ ] Gated items (Void and Dream packages) are recorded with their gate
  state.
- [ ] A hazard Strike halves per part in combat (HZ 6.3; D-22,
  [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 1.1); its Dare room form is M3.

### M2-14 Traps and locks, full

**Goal:** Trap detection, the full lock set, keys and the Wax Key work.

**Implements:** TL (M2 rows), TP1–TP12, 6.20; TL-8.

**Depends on:** M2-13.

**Done when:**
- [ ] V11 full passes; `trapDigest` recorded.
- [ ] The Barb halves per part, with its condition on a failure only
  (TL 6.11). A trap's damage dice, and a Botch's, come from the combat
  stream in combat and from `h:roll` out of combat once the D-23 wave
  has lit it (TL 6.22, 6.23; [M1-11 rulings record](../superpowers/specs/2026-11-28-m1-11-rulings-design.md) 1.1, 2.2).

### M2-15 Curiosities

**Goal:** Curiosities draw, place and resolve on every floor.

**Implements:** EV 4, 9, 10; EV-19, EV-20, EV-21.

**Depends on:** M2-14.

**Done when:**
- [ ] V16 full passes (`FixedDrawCount`, `SlotLegal`, `F1HostAlways`,
  `CrashBetweenWrites`, …).

### M2-16 Bosses and arenas

**Goal:** All 15 bosses fight in their arenas with Renditions.

**Implements:** BR 4, 5, 10; BR-1 to BR-40; LE-3.

**Depends on:** M2-15.

**Done when:**
- [ ] V17 full passes; `BossCourtBreakable`, `RenditionParity` pass.
- [ ] `boss9` vector test passes.

### M2-17 Encounter placement, full

**Goal:** Encounters place across all themes, floors and party sizes.

**Implements:** EP 6.6–6.12, 12; EP-9, EP-29, EP-31.

**Depends on:** M2-16.

**Done when:**
- [ ] V12 full passes; `V12FloorFallback` < 1% over the release set.
- [ ] Every theme × floor × party size × band × tier fills every slot.

### M2-18 Loot, full

**Goal:** The loot economy runs across five floors with every channel.

**Implements:** LE 6, 8.2, 8.3; IR 4.2–4.12; LP 4.2–4.6, 4.10, 5;
LE-3 to LE-20; IR-2 to IR-16, IR-23; LP-8 to LP-28.

**Depends on:** M2-17; the LP HE/Due binding proof (no fallback).

**Done when:**
- [ ] V13, V14, V15 full pass.
- [ ] `LootBindUnplaceable` on HE/Due = 0; `OfferUsableRate` = 100%.
- [ ] The boss reward card goes live (LE-3, LE-4, IR-8, IR-14): the
  `bossCard` stream row moves to the live wave and draws its 9 × 4;
  `bossCardFallback` registered; F1 Floor VP 12 (D-08).
- [ ] V19t block independence: enabling the `bossCard` block moves no
  other block's draw positions; the F1 golden re-bake is labelled
  expected.

---

## Phase C: The run

### M2-19 Five-floor run

**Goal:** A run draws five floors and moves between them.

**Implements:** RS 6.3–6.4, 9.2, 9.4 (floors 2–5 and the same-tick
order; the floor-1 Top-up shipped in M1, D-03), 9.5; RT-3, RT-7, RT-8,
RT-10, RT-12; SV 4.8 (camp-exit commit); LE-14 (WO5 form); DS14.

**Depends on:** M2-18.

**Done when:**
- [ ] V6 full passes.
- [ ] Floor N+1 is generated at Travel on and committed by the M1 GS17
  floor-commit routine, unchanged, at WO5 (test; V13 (m) WO5 clause).
- [ ] `route.floorCount` = 5; floor `route.floorCount` has no Downstair
  (validator); Descend on floors 1–4 goes through WO4 to camp, reusing
  the M1 warning and Scrap pipeline (D-08).
- [ ] `AntechamberCatchUpRate` reported per class; the RS 9.4 F1 guard
  and `Floor1ClassSpread` reruns at level 3 per class.

### M2-20 Camp and simple hub

**Goal:** Camps between floors and the simple Stairtop screen work.

**Implements:** CH 4.1–4.8, 4.14–4.16, 7, 10; CH-2, CH-7, CH-13, CH-15,
CH-16, CH-18, CH-19; ITEMS 10.3–10.6.

**Depends on:** M2-19.

**Done when:**
- [ ] V20 M2 rows pass (`CampProbeResistance`, `LoreInert`, …).
- [ ] V19t `SpeculationEquivalence` passes.
- [ ] `DeathPathSeconds` ≤ 20 s (measured by hand, three runs).

### M2-21 Companions

**Goal:** Companions are recruited at camp and fight beside the hero.

**Implements:** CMP 7.1–7.11, 8.1–8.6; CL 7.8; AB 11; ITEMS 6, 8.2–8.3;
CMP-1 to CMP-11.

**Depends on:** M2-20.

**Done when:**
- [ ] CMP 11.2 validators and 11.5 tests pass.
- [ ] `NoFloor1Companion`, `SoloBudgetMonotone` pass.

### M2-22 Exploration, full

**Goal:** Trace, patrols, travel, the full map and the rest of the XI
M2 set work.

**Implements:** XI 10 (M2 rows); XI-4, XI-6, XI-7, XI-9, XI-18, XI-21.

**Depends on:** M2-21.

**Done when:**
- [ ] V18 complete passes; `TravelNeverEntersKnownHazard` passes.

### M2-23 Saves, profile and run codes

**Goal:** Runs suspend and resume mid-floor, the profile file exists,
and runs can be shared by code.

**Implements:** SV 4.7–4.10.1; MAIN 6.6; SV-14, SV-15, SV-16, SV-17;
XI-28; CC-11.

**Depends on:** M2-22.

**Done when:**
- [ ] `SlotEquivalence`, kill-fuzz, `LoadPathKillFuzz`,
  `ProbeResistance` pass.
- [ ] `SaveMigrationGoldens` pass; `ShareCodeErrorDetect` passes.
- [ ] `SeededRunWritesSeenOnly` passes.
- [ ] WO1 and WO6 reach their full forms (`pendingProfile`, slot
  journal result record, `runHistory`, profile write); the
  `RunEnded{cause: unravelled}` path is live with `RunUnravelled` before
  it (SV-10); Grave Goods wording on the run record (LE-15).

### M2-24 Simulator and CI, full

**Goal:** The full simulator and CI tiers exist, and the run-level
reports the earlier steps deferred are produced.

**Implements:** SV 5.8, 6 (all), SV-21 to SV-27; CA 9, 10 (shadow
records); CL 12.3; ENM 14; XI 10.

**Depends on:** M2-23.

**Done when:**
- [ ] Nightly, weekly and per-OS jobs green on three OSs and two
  architectures; minimum-spec runner live.
- [ ] Report registry complete: no gate missing threshold, instrument,
  N or sidedness.
- [ ] M2 goldens by set cover; release set `RS-M2.v1` frozen.
- [ ] Deferred reports produced: offer Jaccard distance over 100 runs
  (CL 12.3, target ≥ 0.5); Pack fights ended by morale before round 2
  (target < 10%); "saves rolled per round"; `MarksPerScreen` (target
  ≤ 12); TL 11.4 reports. Misses go to the M2-G4 balance pass.

---

## Phase D: Balance gates (SV 7 internal sequence)

### M2-G1 Planning gate

**Done when:** every SV 7 M2 step-1 item is done (graded cuts review,
EP clock re-measure plan, `Skill.Novice/Basic/Expert` adopted or novice
scripts delivered, zero-gate partners from IR/HZ/LP, human-session
calendar, measurements re-taken and budgets frozen).

### M2-G2 Companion cap decision

**Done when:** `TimeModelError` ≤ 10% or human attribution;
`FloorTimeByPartySize` ≤ 20 min; blind party-3/4 arm read; cap set to 3,
or cap 2 adopted; every REL gate re-read at the shipped cap.

### M2-G3 Floor-1 balance freeze

**Done when:** SHIP F1 `BossDeathShare` per Rendition, the Wake,
`EventDeathShareDelta`; REL `Floor1ClassSpread` ≤ 10 in every split;
ESC Arcanist novice row; CAL `F1DeathShare` reported.

### M2-G4 Full balance pass

**Done when:** every remaining REL, HOLD, SW, SHIP and BIND row is read
in precedence order (SV 6.9) and has a recorded verdict, including
Vanguard parity ≤ 110% at the worst legal build (VG-12's parity
measure).

### M2-END Sign-off and playtest

**Done when:**
- [ ] Every SV 7 "M2" checklist row is ticked (streams, seeds, saves,
  launcher, ladder, validators, goldens, gates, XI re-runs, CI, reports
  at both caps).
- [ ] The M2 playtest, with at least one player who did not build the
  game, answers "does a 1–2 hour run hold together?"; notes filed in
  `docs/`.
