# Implementation Roadmap

The build order for the game, from the finished M0 skeleton to M4. Each
step is one testable capability with explicit completion conditions.

The approved designs in [`docs/superpowers/specs`](../superpowers/specs)
say **what** the game is. This roadmap says **in what order to build it
and when each piece is finished**. It adds no rules. Where the designs
leave a gap that blocks a step, it is listed under
[Open decisions](#open-decisions) instead of being decided here.

Design of this roadmap: [`2026-11-24-implementation-roadmap-design.md`](../superpowers/specs/2026-11-24-implementation-roadmap-design.md).

## Contents

| File | Milestone | Question it answers |
|---|---|---|
| [m0-skeleton.md](m0-skeleton.md) | M0 Skeleton (complete) | Does the core and display split work? |
| [m1-vertical-slice.md](m1-vertical-slice.md) | M1 Vertical slice | Is the loop fun? Does the effects system hold? |
| [m2-full-run.md](m2-full-run.md) | M2 Full run | Does a 1–2 hour run hold together? |
| [m3-between-runs.md](m3-between-runs.md) | M3 Between runs | Do players come back? |
| [m4-breadth.md](m4-breadth.md) | M4 Breadth | Is every run different? |

## Rules

1. **One step at a time.** Steps run strictly in the order listed below.
   No step starts until the step before it is done.
2. **Done means done.** A step is done only when every "Done when" box is
   ticked and the [definition of done](#definition-of-done) holds.
3. **Status lives in GitHub.** These files never record progress (except
   M0, kept as a record). Each step has one GitHub issue.
4. **Plan just in time.** A step's detailed task plan is written when the
   step starts, against the code as it then is.
5. **The designs win.** If a step here disagrees with an approved design,
   the design is right; fix this roadmap in the same pull request.

## Working a step

1. Check the step's **Depends on** steps are closed and any listed
   decision is resolved.
2. Open (or pick up) the step's GitHub issue.
3. Read every design section in **Implements**.
4. Write the task plan with the writing-plans skill, saved as
   `docs/superpowers/plans/YYYY-MM-DD-<step-id>-<name>.md`, and link it
   from the issue.
5. Build it on a short-lived branch; one pull request per step (or a few,
   if the plan splits it).
6. Tick each "Done when" box in the issue as it is proven, with the
   evidence (test name, CI run, screenshot).
7. Close the issue when all boxes are ticked and CI is green on `main`.

### GitHub conventions

- **Issue title:** `<step id> <step name>`, e.g. `M1-02 Key algebra`.
- **Issue body:** a link to the step's heading in this folder, then the
  "Done when" list copied as a task list, then a link to the task plan.
- **Labels:** the milestone (`M1` …) and `roadmap-step`.
- **GitHub milestones:** one per roadmap milestone, closed by its `-END`
  step.
- **Project board (optional):** columns Ready / In progress / Verifying /
  Done. Only one step is ever in "In progress".

## Definition of done

Every step, in addition to its own "Done when" list:

- [ ] `dotnet test Dungeon.sln` passes in CI on Ubuntu and Windows.
- [ ] `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` succeeds.
- [ ] No rule logic in the display layer (the CI grep, and later the V19s
  display rows).
- [ ] Every new rule has a unit test; every new random draw goes through
  a registered stream (from M1-03 onward).
- [ ] Content added by the step passes content validation.
- [ ] Changes visible in Godot have been checked by hand on Windows and
  Linux, noted in the issue.
- [ ] Reviewed by the other developer (MAIN 8).
- [ ] Any gap found in the designs is recorded in `docs/` or added to
  [Open decisions](#open-decisions).

## Step format

```
### <ID> <Name>
Goal:          one sentence
Implements:    design sections and change IDs
Depends on:    earlier steps and open decisions
Delivers:      what exists when it is done
Done when:     checkable conditions (tests, validators, CI, manual checks)
Out of scope:  what a reader might expect here but belongs elsewhere
```

IDs never change. If a step must be split later, keep its ID on the first
part and add a letter to the rest (`M1-27a`).

## Reading design references

References read `<doc> <section>`, e.g. `FL 6.1` is section 6.1 of the
floor layout design. Change IDs (`SV-1`, `CH-7`) are the rows each design
applied to the others; search the specs folder for the ID to find every
place it touches. All files are in `docs/superpowers/specs/` unless noted.

| Short | Document |
|---|---|
| MAIN | `2026-11-15-dungeon-rpg-design.md` |
| SM | `2026-11-16-character-stat-model-design.md` |
| AB | `2026-11-16-ability-resource-model-design.md` |
| CO | `2026-11-16-conditions-design.md` |
| ENM | `2026-11-16-enemies-design.md` |
| ITEMS | `2026-11-16-items-inventory-design.md` |
| AR | `2026-11-17-armour-design.md` |
| CL | `2026-11-17-classes-concepts-design.md` |
| CN | `2026-11-17-consumables-design.md` |
| DH | `2026-11-17-death-healing-design.md` |
| TK | `2026-11-17-trinkets-design.md` |
| W | `2026-11-17-weapons-design.md` |
| SH | `2026-11-18-shade-class-design.md` |
| VG | `2026-11-18-vanguard-class-design.md` |
| ARC | `2026-11-20-arcanist-class-design.md` |
| CMP | `2026-11-20-companions-design.md` |
| HR | `2026-11-20-herald-class-design.md` |
| EP | `2026-11-21-encounter-placement-design.md` |
| FL | `2026-11-21-floor-layout-design.md` |
| HZ | `2026-11-21-hazards-design.md` |
| ROOM | `2026-11-21-room-terrain-design.md` (change IDs `TR-`) |
| RS | `2026-11-21-run-structure-themes-design.md` (change IDs `RT-`) |
| TL | `2026-11-21-traps-locks-design.md` |
| BR | `2026-11-22-boss-rooms-design.md` |
| EV | `2026-11-22-events-design.md` |
| IR | `2026-11-22-item-rolls-design.md` |
| LE | `2026-11-22-loot-economy-design.md` |
| LP | `2026-11-22-loot-sources-design.md` |
| CH | `2026-11-23-camp-hub-design.md` |
| XI | `2026-11-23-exploration-information-design.md` |
| HL | `2026-11-23-heirlooms-curses-design.md` |
| SV | `2026-11-23-seeds-validation-design.md` |
| SK | `2026-11-23-starting-kits-design.md` |
| CA | `2026-11-24-charm-abilities-design.md` |
| NP | `2026-11-24-naming-pass-design.md` and `-name-list.md` |
| CC | `../reviews/2026-11-23-cross-document-comparison.md` |

Validators are named `Vn` (V1–V24) with phases `s` (static), `g`
(generation), `r` (runtime) and `t` (test); SV 5 defines them. Names in
code use the naming pass names (NP), not older names that may still
appear in a design's history.

## Open decisions

Gaps found while building this roadmap. Each blocks the step named. Resolved rows are struck through and point to their ruling. The
designs either say nothing, or two designs disagree. The owner rules; the
ruling is recorded in the owning design and the row is struck through
here.

| ID | Blocks | Question | Source |
|---|---|---|---|
| ~~D-01~~ | ~~M1-01~~ | **Resolved 2026-11-24 (client):** MAIN 6.1 amended to name `dungeon-ttrpg/`; `content/` and `src/Sim` are created by M1-01. See the [rulings record](../superpowers/specs/2026-11-24-m1-scope-rulings-design.md) 1. | MAIN 6.1 |
| D-02 | M1-09, M1-19 | SRD 5.2.1 check of class wording (Patch Up, Onslaught, Extra Attack, Shove, crit wording) before any class record is written. A required check, not a choice; recorded here so it is not skipped. | CL front matter; VG 18; DH header |
| ~~D-03~~ | ~~M1-21~~ | **Resolved 2026-11-24:** the floor-1 Forecourt Top-up (RS 9.4, RT-8) and its F1 guard (BR-3) move from M2 to M1, with level scaling; every M1 hero meets Old Ragged at level 3. No numbers change. Built in M1-21 (rule) and M1-35 (trigger and guard). See the [rulings record](../superpowers/specs/2026-11-24-m1-scope-rulings-design.md) 2. | RS 9.4; BR 4.2.1; DH 21.1; VG-24 |
| D-04 | M1-22, M1-31 | Base type of the M1 generic minion (EP names only "the ENM 21 minion template"). | EP 7.3; ENM 9.5 |
| D-05 | M1-27 | Arena stamp position: GS4 (before terrain) needs BR and ROOM to confirm; otherwise adopt the fallback (stamp after GS6). Changes floor-1 terrain draws, so it must be fixed before goldens. | SV-9; CC-68 |
| D-06 | M1-30 | Floor-1 traps, locks and keys in the slice. TL staging calls traps optional, but TL 6.20 gives floor 1 one or two traps and locks, and DH-32 needs trap ids. Does M1 ship the lesson trap, and any key? Two further questions from the M1-10 rulings ([record](../superpowers/specs/2026-11-27-m1-10-rulings-design.md) 7 item 3): (i) are known traps pathing hazards in M1, or only from M2 (TL 12 against CO 11.3 and TL 14)? (ii) how does a known lesson trap spring in M1, when every way to spring a known trap is M2 (TL 0.3 row 9) yet M1-30's goal names lesson traps? | TL 6.20, 12; DH-32 |
| D-07 | M1-31 | M1 patrol composition (the Kennel patrol uses M2 types) and plain start shapes for the lair and first fight (the designs use M2-only Revel and activities). | EP 6.17, 7.3, 12 |
| ~~D-08~~ | ~~M1-33~~ | **Resolved 2026-11-24:** no boss reward card in M1 (kind, field, ledger line and stream reserved and inert); coin drops as approved with no sink; LE-14 is one floor-commit routine (WO1 in M1, WO5 in M2); taking the floor-1 stairs runs the full warning and Scrap and is victory (`route.floorCount` = 1). See the [rulings record](../superpowers/specs/2026-11-24-m1-scope-rulings-design.md) 3. | LE 6.3, 6.10, 11; LP 4.1; ITEMS 9, 20; SV 4.8.3 |
| D-09 | M1-13 | Base collision damage for M1 (the 2/3/3 band is the M2 Wrecker's). | VG 11; CO 10.1 |
| D-10 | M1-30, M1-33, M1-39 | Owner deliverables before M1 goldens: HZ `hzTagMax`; TL `traps` and `trapsResolve` draw formulas; LP V15 M1 rows; phial pre-roll 3 draws confirmed; IR filter-first; DH reshuffle; SV-5 and SV-8 applied. Each has a fallback in SV 14.2 except as noted there. | SV 4.5, 7, 14.2 |
| D-11 | M1-34 | Which Afterimage states (Fresh, Persisting, Old, Short) are "basic" in M1. | XI 4.2 |
| ~~D-12~~ | ~~M1-04~~ | **Resolved 2026-11-28 (client):** content record ids are `[a-z0-9_.:-]`; stream and domain labels (code constants passed to `LabelId`) may also use capitals, `[A-Za-z0-9_.:-]`, so every SV 4.1 vector stays valid. Recorded in SV 4.1 K-rule 3. | SV 4.1 K-rule 3, 4.3 |
| D-13 | M1-33 | The `lineRoll:{source}` source kinds (IR) and the `loot/floor` boss-channel block label (LE) are not named; M1-05 registers neither. | SV 4.3; IR; LE |
| D-14 | M2-23 | V19s `CoreNoProfileRef` (SV 4.6): an analyzer rule that Core cannot reference Profile types. M1-06 builds no rule because no Profile type exists yet; the step that introduces the profile adds it. | SV 4.6, 5.2 V19s |
| ~~D-15~~ | ~~M1-07~~ | **Resolved 2026-11-28 (client):** advantage netting follows the 5E rule: any number of advantage sources give advantage, any number of disadvantage sources give disadvantage, and one of each cancels to a normal roll, whatever the counts. Weapons 7.5 ("net them (one cancels one)") is amended to fold them as conditions 6.7 does, and Overlap and Vex read the folded mode. Recorded in [`docs/effects-vocabulary.md`](../effects-vocabulary.md). | CO 6.7; W 7.5 |
| D-16 | M1-14 | Leadership and Rally: does Rally roll Leadership against a difficulty, need training in Leadership, or neither? No design gives Rally a roll (CO 18.1: "In M1, Rally applies the `rally` cure directly"; AB: "Rally: Leadership"). M1-08 adds Leadership as a skill only. | SM 7.1; CO 18.1; AB |
| ~~D-17~~ | ~~Later: Bolstered (temporary HP), [m4-breadth Later list](m4-breadth.md#later-not-scheduled)~~ | **Resolved 2026-11-27 (client): the deferral stands.** M1-10 keeps the reduced test (HP 5 + undetected trap 10 → HP 1, `DamageClamped{undetectedTrap}`, save chain not reached). The Later entry Bolstered owns the full test: HP 5 + temporary HP 3 + undetected trap 10 → temporary HP absorbs 3, total 7, the floor clamps to temporary HP 0 and HP 1, `DamageClamped{undetectedTrap}`, save chain not reached. Before Bolstered is built, the DH owner rules: (a) whether the mixed-event check should be independent of part order; (b) whether it counts temporary HP; (c) what the combat log shows. See the [rulings record](../superpowers/specs/2026-11-27-m1-10-rulings-design.md) 1. | DH 10.4, 14, 15.2, 21.4 |
| ~~D-18~~ | ~~M1-13~~ | **Resolved 2026-11-27 (client): subtract, not rescale.** Movement left = max(0, current speed + granted movement − movement used), rechecked before every step, and 0 while current speed is 0 (2.1). Dash (M1-14) adds granted movement equal to the current speed at the Dash; Bar the Way (M1-20) sets a per-turn stopped marker a later speed raise cannot undo (2.2). Stand gap: if movement left is less than the stand cost, standing costs the action, and with the action spent the stand is refused (M1-13; 2.3). The M1-10 code already subtracts. See the [rulings record](../superpowers/specs/2026-11-27-m1-10-rulings-design.md) 2. | AB 14.2; CO 6.7 |
| ~~D-19~~ | ~~M1-11, M1-35~~ | **Resolved 2026-11-27 (client).** (a) TL-19's condition end is M2; M1 ships no trap that can apply a condition, and M1-30 builds only the push stop before F1 trigger squares plus a guard check, so it no longer waits on D-19 ([record](../superpowers/specs/2026-11-27-m1-10-rulings-design.md) 3). (b) Declaring an opener runs the full combat start first, initiative included; the opener resolves before any turn starts and is charged to the striker's round-1 turn (action and once-per-round spends charged; movement and bonus action kept); its command takes no out-of-combat tick. Built in M1-11; M1-35 adds membership ([record](../superpowers/specs/2026-11-27-m1-10-rulings-design.md) 4 and 5). | CO 5.5, 11.6; TL-19; DH 10.4, 20.5; EP 6.8.2 |
| D-20 | M2-02 | Rulings RT-14 / HZ-6 (Teetering in the theme six), RT-16 / TL-39 (trap conditions from the theme six), EV-11 (Grace durations). | CO; RS; EV |
| D-21 | M2-10 | Rulings EP-6 (Billets), BR-6 (Rhythm), BR-7, TR-15, TL-16 (AI terms). | ENM; EP; BR |
| D-22 | The first content with a half-on-success save | **Owner: DH.** A successful save against `onSuccess: half`: halve each part, or the summed total, and how to round? M1-11 refuses `half` at load (owner ruling 2026-11-27). | AB 14.1; DH 20.3 |
| D-23 | M1-35 | **Owner: SV.** Out-of-combat ability rolls: an ability used out of combat that is not an opener and rolls (a save, an attack or damage dice): which stream? Drawing `combat` breaks V19t `ExplorationCombatIsolation` (SV 1001), and SV 4.3 names no other. M1-11 refuses such uses with `NotInCombat`, and flat-damage abilities still work (M1-11 plan call 5). | SV 4.3, V19t; AB 6.8; DH 10.4 |
| D-30 | M3-06 | How many class and aspect unlocks ship in M3. | CL 9.4 |
| D-40 | M4-02 | Multiclass model: (a) level split or (b) cross-training card. | CL 7.7 |

### M1-11 notes for owners

Plan calls, not decisions; none needs a ruling before its step.

- An opener that costs a bonus action or movement charges that cost, not the action (plan call 6; record 4.1 covers only action openers).
- "A hostile ability" for the own-save window means a user of another faction (plan call 10).
- The Vanguard's `ifKills` option policy (VG 6.4 rule 5) is not one of AB 7.3's four (for M1-20).
- The M1 Mastiff's bleeding claw must be an applied effect, not an AB 9 rider (for M1-22).
- Damage-part bound: each damage part is at most 20 dice, 1 to 100 sides and |flat| at most 9999, plus an exactness bound on the whole damage list. It is a content limit chosen so the exact preview never overflows; the largest design dice are 8d8.
- The greyed-reason English strings in `content/strings/en.json` are placeholder wording for the owner and naming pass.
- Out of combat, the action bar's `CheckUsable` accepts an ability usable on either the tick view or the opener view.

## All steps in order

**M0 Skeleton** (complete): M0-01, M0-END.

**M1 Vertical slice**

| Phase | Steps |
|---|---|
| A Foundations | [M1-01](m1-vertical-slice.md#m1-01-project-layout-and-build-guards) Project layout and build guards · [M1-02](m1-vertical-slice.md#m1-02-key-algebra) Key algebra · [M1-03](m1-vertical-slice.md#m1-03-canonical-writer-and-digests) Canonical writer · [M1-04](m1-vertical-slice.md#m1-04-content-pipeline-and-validator-framework) Content pipeline and validators · [M1-05](m1-vertical-slice.md#m1-05-stream-registry-and-draw-ledger) Stream registry · [M1-06](m1-vertical-slice.md#m1-06-reproducibility-key-and-run-configuration) Reproducibility key |
| B Rules core | [M1-07](m1-vertical-slice.md#m1-07-effects-system) Effects system · [M1-08](m1-vertical-slice.md#m1-08-stats-and-contests) Stats and contests · [M1-09](m1-vertical-slice.md#m1-09-damage-and-hp) Damage and HP · [M1-10](m1-vertical-slice.md#m1-10-turns-combat-lifecycle-and-death) Turns, combat lifecycle, death · [M1-11](m1-vertical-slice.md#m1-11-abilities-reactions-and-areas) Abilities, reactions, areas · [M1-12](m1-vertical-slice.md#m1-12-conditions-core) Conditions core · [M1-13](m1-vertical-slice.md#m1-13-m1-conditions-and-forced-movement) M1 conditions, forced movement · [M1-14](m1-vertical-slice.md#m1-14-universal-and-skill-actions) Universal and skill actions |
| C Equipment | [M1-15](m1-vertical-slice.md#m1-15-items-and-inventory) Items and inventory · [M1-16](m1-vertical-slice.md#m1-16-weapons-and-attacks) Weapons · [M1-17](m1-vertical-slice.md#m1-17-armour-ac-and-shields) Armour and shields · [M1-18](m1-vertical-slice.md#m1-18-healing-rests-and-supply) Healing, rests, supply |
| D Vanguard | [M1-19](m1-vertical-slice.md#m1-19-class-records-and-hero-creation) Class records, creation · [M1-20](m1-vertical-slice.md#m1-20-vanguard-level-1-kit) Vanguard level 1 · [M1-21](m1-vertical-slice.md#m1-21-experience-and-levels-23) Experience, levels 2–3 |
| E Enemies | [M1-22](m1-vertical-slice.md#m1-22-monsters-and-the-kennel-roster) Kennel roster · [M1-23](m1-vertical-slice.md#m1-23-enemy-ai) Enemy AI · [M1-24](m1-vertical-slice.md#m1-24-old-ragged) Old Ragged · [M1-25](m1-vertical-slice.md#m1-25-playable-combat-in-godot) Playable combat in Godot |
| F Generation | [M1-26](m1-vertical-slice.md#m1-26-den-arena-template) Den arena template · [M1-27](m1-vertical-slice.md#m1-27-generation-pipeline-and-route-stub) Pipeline and route stub · [M1-28](m1-vertical-slice.md#m1-28-floor-layout) Floor layout · [M1-29](m1-vertical-slice.md#m1-29-arena-stamp) Arena stamp · [M1-30](m1-vertical-slice.md#m1-30-terrain-traps-and-locks) Terrain, traps, locks · [M1-31](m1-vertical-slice.md#m1-31-encounter-placement) Encounter placement · [M1-32](m1-vertical-slice.md#m1-32-curiosity-stub) Curiosity stub · [M1-33](m1-vertical-slice.md#m1-33-floor-1-loot-and-floor-commit) Loot and floor commit |
| G Exploration | [M1-34](m1-vertical-slice.md#m1-34-sight-and-party-knowledge) Sight and knowledge · [M1-35](m1-vertical-slice.md#m1-35-awareness-noticing-and-the-explorecombat-loop) Awareness and the explore–combat loop |
| H Full slice | [M1-36](m1-vertical-slice.md#m1-36-floor-rendering) Floor rendering · [M1-37](m1-vertical-slice.md#m1-37-interface-flows-and-accessibility) Interface and flows |
| I Proof | [M1-38](m1-vertical-slice.md#m1-38-simulator-skeleton) Simulator skeleton · [M1-39](m1-vertical-slice.md#m1-39-golden-seeds-and-release-set) Goldens and release set · [M1-40](m1-vertical-slice.md#m1-40-ci-tiers) CI tiers · [M1-END](m1-vertical-slice.md#m1-end-sign-off-and-playtest) Sign-off and playtest |

**M2 Full run:** M2-01 Riders and layers · M2-02 M2 conditions, Downed ·
M2-03 Items, second wave · M2-04 Offers, levels 4–10 · M2-05 Shared
spellbook · M2-06 Shade · M2-07 Arcanist · M2-08 Herald · M2-09 Starting
kits · M2-10 Enemy roster and AI · M2-11 Themes, grammars, rosters ·
M2-12 Layout, terrain, Portals · M2-13 Condition terrain and hazards ·
M2-14 Traps and locks · M2-15 Curiosities · M2-16 Bosses and arenas ·
M2-17 Encounter placement · M2-18 Loot · M2-19 Five-floor run · M2-20
Camp and simple hub · M2-21 Companions · M2-22 Exploration · M2-23
Saves, profile, run codes · M2-24 Simulator and CI · M2-G1 Planning
gate · M2-G2 Cap decision · M2-G3 Floor-1 balance freeze · M2-G4 Full
balance pass · M2-END. See [m2-full-run.md](m2-full-run.md).

**M3 Between runs:** M3-01 Unlocks and grants · M3-02 Difficulty and
Peril · M3-03 Boons and kit variants · M3-04 Heirlooms · M3-05 Charms ·
M3-06 Class unlocks · M3-07 In-run additions · M3-08 Menu-room hub ·
M3-END. See [m3-between-runs.md](m3-between-runs.md).

**M4 Breadth:** M4-01 Species · M4-02 Multiclassing · M4-03 Curses and
Maladies · M4-04 Content breadth · M4-END. See
[m4-breadth.md](m4-breadth.md).

**Next step to start:** M1-12, once M1-11 is merged.
