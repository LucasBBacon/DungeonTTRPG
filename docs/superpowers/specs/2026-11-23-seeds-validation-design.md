# Seeds and Validation: Design (Dungeon Generation, Segments 11–12)

Date: 2026-11-23
Status: Approved, 2026-11-23
Parents (immediate): `2026-11-23-exploration-information-design.md` (exploration and information design, "XI", segment 10, approved 2026-11-23, **the most recent template**), `2026-11-22-boss-rooms-design.md` ("BR", segment 9), `2026-11-22-events-design.md` ("EV", segment 8), the loot designs `2026-11-22-loot-economy-design.md` ("LE", segment 7a), `2026-11-22-item-rolls-design.md` ("IR", segment 7b) and `2026-11-22-loot-sources-design.md` ("LP", segment 7c), `2026-11-21-encounter-placement-design.md` ("EP", segment 6), `2026-11-21-traps-locks-design.md` ("TL", segment 5), `2026-11-21-hazards-design.md` ("HZ", segment 4), `2026-11-21-room-terrain-design.md` ("ROOM", segment 3; "RT" in section numbers such as RT 6.12; its change ids are TR-n), `2026-11-21-floor-layout-design.md` ("FL", segment 2), `2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change ids are RT-n).
Other parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "C" in section numbers, "CON" in prose),
`2026-11-16-enemies-design.md` (enemies design, "ENM"),
`2026-11-16-items-inventory-design.md` (items design, "ITEMS"),
`2026-11-17-weapons-design.md` ("W"),
`2026-11-17-armour-design.md` ("AR"),
`2026-11-17-trinkets-design.md` ("TK"),
`2026-11-17-consumables-design.md` ("CN"),
`2026-11-17-death-healing-design.md` (death and healing design, "DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "CL"),
the stage-2 class designs `2026-11-18-vanguard-class-design.md` ("VG"),
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC")
and `2026-11-20-herald-class-design.md` ("HR"),
and `2026-11-20-companions-design.md` (companions design, "CMP").

Items marked **(agreed)** are the client's answers of 2026-11-23 to the four questions this design raised (Q1–Q4; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document; parent documents' questions are always named with their document ("XI Q1", "EV Q1", "DH Q11") so that they never read as this document's Q1–Q4 **(new lead's call)**. The client also said that everything else in the lead's proposal stands as written: composite R; the key algebra (SplitMix64, FNV-1a-64 labels as input only, counter-based draws, a child keyed from its parent's key, `Bound` by multiply-shift, reference vectors); the stream registry and the master stream table; keyed `attempt/{a}` and `retry1` retries replacing "seed + 1"; attempt-invariant streams; per-floor, per-entry loot and recruit keys; keyed checks with domains; the GS0–GS17 pipeline with AP1 and AP2 and act-then-assert; the draw-stability rules DS1–DS15; the outside-the-seed rules and `DisplayKey`; the reproducibility key; shared seeds, daily seeds (Later) and replays; saves with `FloorStatic` and `GenInputs` and the write orders WO1–WO8 (DS15 and WO8 added by the check inside those same rules; cross-document comparison 2026-11-23, CC-132); seed display and entry (a 13-character code with a check character and a build tag); versioning and no content deletion; golden seeds by set cover and the release seed set as generation fixtures; the validation phase model `s/g/r/t`, failure classes, qualified check ids and single owners; the consolidated suite V1–V19 with V19 `Conformance`; the build dependency graph; the ladder LR0–LR5; failure budgets; CI tiers and time budgets; the simulator with six instruments, the player-model and arm catalogue, 13 result classes, the statistical reading rules, minimum N, structural partners for zero-event gates, time-model calibration, `FloorClockLedger`, the precedence PR0–PR5 and lever discipline, the report-registry fixes and the human-session plan; the sign-off checklists for M1, M2, M3, M4 and Later; the numbers with fixes SN1–SN16; data and tooling; staging; ownership and the hook answers; the binding requirements; the conflict register SQ1–SQ46; SV-1 to SV-32; the requests with their fallbacks; the future client items; the risks; and the appendix resolutions and names. The client approved this document as a whole on 2026-11-23.

**The lead's answers (0.1) and composite R (3.3)** were accepted by the client by proceeding to the four questions. They are recorded here as the lead's answers; the client confirmed them on 2026-11-23, when it approved this document as a whole, **including every told-plainly item**:
1. out-of-combat noticing, Steal and hazard saves move off the combat stream (SV-6);
2. the difficulty tier, boons and Nightmare tier join the reproducibility key (SV-12);
3. shared codes match the dungeon's shape, but loot depends on the party from F2 (4.7);
4. no outcome is shown until its save lands (4.8.3);
5. codes survive patches at route and layout level only (4.10);
6. client numbers are never loosened by statistics (6.4);
7. four unmeasured numbers are measured on the M1 skeleton before the M2 budgets freeze (6.1, 8.7).

**Corrected after the check (told plainly, 14.3):** item 3 drew the boundary one floor too late. Floor-1 generation already reads the hero (the floor-1 Dream's Due is a weapon for the hero's arms, and IR's filter-first reads the party snapshot on every floor). The accurate statement is: **shared codes match the dungeon's shape (route, doors, layout graph); loot depends on the hero from F1 and on the whole party from F2**. Item 4 also gains camp commands (WO8), and item 7's measurements are completed at the M2 planning gate for the stages M1 does not have (7). The list above now names DS1–DS15 and WO1–WO8; the client accepted DS1–DS14 and WO1–WO7, and the check added DS15 and WO8 inside those same rules (14.3; CC-132).

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-23, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-23. Items marked **(lead's call, after the check)** were made in answer to the design check of this document; none overrides a client decision; they are listed in 14.3 with the check's "Told plainly after the check" items (T1–T11), and the client confirmed them all on 2026-11-23. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change SV-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-23 (with Q1–Q4 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest a draft, a judge, a critic or an approved view, or state an unwelcome forecast, and were told to the client in plain words (14.1; those found in the check, 14.3, T1–T11); the client confirmed them all on 2026-11-23. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q4 of this document (2026-11-23), or one recorded in an approved document (named with its document, for example "XI Q1"), restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal (the lead's answers of 0.1 and composite R included); confirmed by the client with this document on 2026-11-23 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-23 |
| **(lead's call, after the check)** | A lead's call made in answer to the design check; never overrides a client answer; listed in 14.3, with a "Told plainly after the check" list, and confirmed by the client on 2026-11-23 |
| **(change SV-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text are flagged ⚑ there |
| **(told plainly)** | Narrows or re-reads approved text, contests a draft, a judge, a critic or an approved view, or states an unwelcome forecast; told to the client in plain words (14.1; after the check, 14.3) and confirmed by the client on 2026-11-23 |
| **(request)** | Asks another segment's owner for a deliverable or a confirmation. Changes nothing until that owner rules. Every request names a fallback (14.2) |
| **(hook)** | Owned by another segment or design; segments 11–12 state only what they need |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator or the M1 measurements (6.1, 8). Never binding |

**The client's brief.** Dungeon generation segments 11 and 12: **seeds and validation**. They are consolidation segments and own **no gameplay**. They cover:
- **Segment 11, seeds:** the master seed, the stream table, the generation order, draw stability, the reproducibility key, shared and daily seeds and replays, how seeds interact with saves, seed display and entry, versioning and content updates, and golden seeds.
- **Segment 12, validation:** the validator suite V1–V19 (V1–V20 since the camp and hub design's V20 `Hollowfolk`, change CH-25; V1–V21 since the starting kits design's V21 `StartingKits`, change SK-10; V1–V22 since the heirlooms and curses design's V22 `HeirloomsCurses`, change HL-14; V1–V23 since the charm abilities design's V23 `CharmLane`, change CA-18), the retry and fallback ladder, failure budgets, the simulator with its reports and gates, sign-off checklists per wave, build and test time, and CI.

The client asked the lead to proceed with the usual research (four research drafts, two models, a simulator and sign-off layer, a numbers pass, three judges and three critics). Standing guidance: not an exact 5E port; design is not limited by quantity; combat is key; the display never holds rules; contest the client or approved views where warranted.

The client's four answers (Q1–Q4, **agreed**, 2026-11-23) are in 0.2: **seeded and shared runs appear in run history marked seeded and advance no unlocks, records, codex entries or launcher memory (Q1). A release build that cannot build a floor after its attempts ends the run cleanly ("the dream unravels"), keeps and marks its record and files a bug report, replacing the crash (Q2). A patch landing mid-run lets the run continue: floors already generated are kept exactly, future floors use the new version, and the run is marked not comparable (Q3). The suspend slot is a floor-entry snapshot plus a state-delta journal, load replaces it atomically, and no roll, reveal, Down or death is shown until its record is written (Q4).**

**What this document is.** The segments own the plumbing that every other segment's design depends on. **No rule here changes a probability or an outcome distribution on the normal (non-failure) path**; SV-6 changes which dice a roll uses, never its odds. Rewritten after the check (lead's call, after the check), because the earlier sentence missed several **player-visible** exceptions. Each is listed here with its owner:
- **Q1 (agreed):** seeded runs advance no progress; run history marks them (4.7). Owner: segments 11–12 with the main design owner (profile save).
- **Q2 (agreed):** a release run that cannot build a floor ends cleanly, with a marked record and a bug report; the progress credit of an unravelled run (5.5). Owners: segments 11–12 and DH.
- **Q3 (agreed):** the not-comparable mark, its notice and its exclusion from records (4.10.1). Owner: segments 11–12.
- **Q4 (agreed):** the outcome rule, a presentation-timing rule for floor and camp commands (4.8.3). Owner: segments 11–12 with the main design owner.
- **Seed display and entry:** the share code, where it is shown, the seeded-run entry line and the build-mismatch notices (4.9). Owner: segments 11–12; words in the naming pass.
- **`LootBindDegrade`:** on a failure path only (≤ 0.3% of floors, *illustr.*), a non-HE, non-Due item socket becomes a merge or coin. This is a loot substitution replacing LP's action-less "hard failure"; **it needs the LP owner's confirmation of the merge-or-coin rule** (request, 11; 5.5).

**Change prefix.** This design uses **SV-n**. A case-insensitive grep for `SV-[0-9]` across the 29 approved documents finds **0 hits** (re-run while writing). The research drafts and the two models each numbered SV from 1, and the simulator layer used SV-30 to SV-47, which collided with Model B's SV-30 to SV-32. This document issues **one register, SV-1 to SV-32** (13). No id has two meanings; the draft-to-register map is in A.6.

**Validator.** **V19 `Conformance`** is the only new validator id. V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV, V17 BR and V18 XI. A grep for `\bV19` and for `Conformance` finds 0 hits (re-run while writing). The drafts used four names for it (`SeedTree`, `SeedsAndValidation`, `Conformance`, `StreamsAndReplay`), and one draft spent it on a content-suite umbrella. This document keeps one name. The non-V content suites stay unnumbered.

**Local id schemes.** Each was grep-checked as a whole word with digits across the 29 approved documents, and each has 0 hits (re-run while writing):
- generation stages **GS0–GS17**;
- assertion points **AP1, AP2**;
- ladder rungs **LR0–LR5**;
- save write orders **WO1–WO8** (WO8 added after the check; 0 hits, re-run);
- draw-stability rules **DS1–DS15** (DS15 added after the check; 0 hits, re-run);
- gate precedence **PR0–PR5**;
- the conflict register **SQ1–SQ46**;
- numbers fixes **SN1–SN16**;
- Appendix A findings **SD** (determinism critic), **SX** (coverage critic) and **SY** (consistency critic).

**Rejected schemes, with their hit counts:** SR (184), SC (34), KR (34), KC (31), LB (22), CF (21), LK (21). Single letters with numbers (L, G, P, S, W, K, D, B, F) are rejected because the corpus already uses them for levels, the DH guarantee package G1–G6, the potion peg P1, and others. **Streams are named by path, never by a row id**, so "F1" always means floor 1. This document's client questions **Q1–Q4** are local, as in every parent.

**Names are placeholders** until the client naming pass. Every new name was grep-checked case-insensitively as a whole word across the 29 approved documents (A.5); the check was re-run while writing.

**Numbers in this document are illustrative** unless a rule says otherwise, and are marked *(illustr.)* where they first appear. Every figure in 8 comes from the numbers pass's toy and estimates, never the simulator. **Exceptions (new lead's call):** the rules the client fixed in Q1–Q4 are client keys, not tuning: seeded runs advance no progress (Q1); the clean end after the release attempts, its kept and marked record and the bug report (Q2); kept floors, new-version future floors and the not-comparable mark (Q3); the floor-entry snapshot plus state-delta journal, atomic replace on load and the outcome rule (Q4). Changing them returns to the client. The number of generation attempts (3), the compaction interval (50 commands) and the overlap budget (120 ms) stay *(illustr.)*. After the check, the attempt cap is a code constant, `GenMaxAttempts`, covered by `genVersion`, not a configuration key (5.5; lead's call, after the check).

**Waves:** **M1** slice, **M2** full run, **M3** between runs, **M4** breadth, **Later** **(settled, MAIN 9)**.

**Abbreviations.**
- F1–F5: floors 1–5.
- "Key": a 64-bit stream key (4.1). "Stream": a key plus a counter. "Keyed hash": a stateless draw addressed only by its key.
- "floorKey": the key of `dungeon/floor/{N}`. The approved docs' "floorSeed" means this.
- "Attempt": a generation attempt of one floor (`attempt/{a}`, 4.3).
- "Release set" and "golden set": defined in 4.11.
- "Instrument": a simulator harness (6.1).
- "Seeded run": a run whose seed did not come from the launcher (a typed or pasted code; daily seeds when they ship). "Personal run": a launcher run (R-SEED-6). Simulator and replay runs are neither (4.7).
- "XI Q1", "EV Q1", "DH Q11" and the like: a parent document's client question; "Q1–Q4" alone always means this document's.

---

## 0. At a glance

### 0.1 The lead's answers, in plain words (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

**The design in one line.** *One number makes the whole run; every job rolls its own named dice, so nothing a player does or a builder adds ever changes another job's dice; every generated floor is checked twice and stored; every failure has one ladder and a budget; and every report says in advance what a miss does.*

#### How seeds work
- Every run starts from **one number, the seed** (60 bits, shown to players as a 13-character code).
- From the seed the game grows a **tree of separate dice bags**, one per job: the route, each floor's layout, terrain, hazards, traps, monsters, loot, events and recruits, the fight dice, and the out-of-combat check dice.
- **Each bag is made from its job's name**, never from how many dice another bag has used. So adding a new feature later never changes what any other bag rolls, and **opening a chest, picking a lock or walking a different route never changes the next attack roll (decided, MAIN 6.3)**.
- **One new finding (told plainly).** Today, out-of-combat noticing rolls ("did the patrol spot you?"), out-of-combat Steal and out-of-combat hazard saves use the fight dice. Exploring differently therefore changes the next fight's rolls. We move those rolls to the check dice (SV-6).
- The floors are built in **one fixed order** (4.4). Each floor is checked at **two points**: once after its layout, terrain, hazards and traps exist, and once after monsters, loot and events are placed.

#### What is outside the seed
- **Launcher choices** before a run: re-rolling a seed so you do not get the same theme, the same floor-1 Curiosity or the same floor-1 boss three runs in a row.
- **Profile memory:** which Curiosities you have seen, recent bosses, display memory.
- **Purely visual content:** Retellings, the Twice-Told card, Afterword lines, art variants, map Waymarks.
- None of these can change a run's dungeon or its rolls. Tests prove it (6.1, V19).
- **Told plainly:** your difficulty tier, starting boons and Nightmare tier *do* change a run. They are therefore now part of the run's identity (SV-12; live in M3, field reserved from M1).

#### Shared seeds, daily seeds and replays
- **Shared seeds (M2).** Type or paste a code and you get the **same route, the same doors, the same floor 1 layout and the same layout graph on every later floor, and the same rolls for the same choices.**
- **Told plainly:** loot depends on the party you bring. **Corrected after the check:** this answer first said "from floor 2"; in fact floor 1's loot already depends on the hero (the floor-1 Due is a weapon for your hero's arms), and from floor 2 loot and room sizes depend on the whole party. Two players with different heroes see the same dungeon *shape* (route, doors, layout graph) but different loot (4.7, 14.3).
- Shared runs skip the launcher and use the standard unlock set, so they can show content you have not unlocked yet. They appear in your run history marked "seeded" and advance no unlocks, records, codex entries or launcher memory **(agreed, Q1)**.
- **Daily seeds** are Later. Fair comparison would need a fixed hero and a published seed, and leaderboards need a server.
- **Replays** are the seed plus the list of commands. They work only on the build that made them, and the game says so when they do not.

#### Saves
- **Safe points.** The run save is written at safe points: camp, the start of each floor, Curiosity resolves. **The generated floor is stored in the save**, so a patch can never change a floor you are standing on.
- **On a floor.** The single suspend slot is written after every command (agreed, MAIN 6.6, XI-28, XI Q1). It is a snapshot at floor entry plus a small journal of what changed after each command, compacted often; loading replaces the slot in one step instead of deleting and rewriting it **(agreed, Q4)**.
- **Told plainly:** we tighten one point. **No roll, reveal, Down or death is shown on screen until its save has landed** (agreed, Q4). Killing the game to undo a bad roll or a death therefore never works. After the check the same rule covers camp rerolls, purchases and recruiting (WO8).

#### Versioning
- A run's identity records the **generator version** as well as the content.
- **Adding content keeps the old order** of themes, bosses and cards, so old shared codes mostly still work after a patch.
- **Told plainly:** loot and room picks can still shift, so codes survive patches at route and layout level only.
- A code from another build shows a warning and starts anyway.
- A patch that lands during a run: the run continues, floors already built stay exactly as they were, later floors use the new version, and the run is marked not comparable for records and shared codes **(agreed, Q3)**.

#### How validation works
Checks run at three times:
1. **At build:** proofs over every room template, every boss arena and the content rules. These are the V1–V18 static parts.
2. **During generation:** every floor is checked at the two assertion points (AP1, AP2).
3. **In tests:** determinism, replays, saves, golden seeds, crash injection.

One new validator, **V19 `Conformance`**, checks the seed machinery itself: every dice bag is registered, every bag rolls exactly its declared count, and nothing reads the clock, the profile or the operating system.

#### The retry ladder and failure budgets
When a check fails, the floor climbs one ladder:
1. **LR1** repairs in place (no new dice).
2. **LR2** re-rolls only the failing step, once.
3. **LR3** regenerates the whole floor once, only for checks proven at build time.
4. **LR4** ships a plain, logged fallback, only where a check allows it.
5. **LR5** is a bug.

**Budgets** *(illustr.)*:
- Floor retries are never seen on the release seed set.
- Fewer than 3% of runs contain any degraded floor.
- Bugs are 0 over hundreds of thousands of test floors.

**If a released game still cannot build a floor** after its attempts (expected to be practically never), the run ends cleanly ("the dream unravels"), its record is kept and marked, and a bug report is filed. This replaces the crash in the death and healing design, because a crash would recur on every reload **(agreed, Q2)**.

#### The simulator and sign-off
- **One simulator** with six instruments: fights and runs, the explore walker, the launcher emulator, the screen-clutter counter, generation-only runs, and human playtests read through the same report code.
- **Every report row has one class** that says in advance what a miss does: fail the build, hold a theme, switch a feature off, slip content a wave, fire a segment's own clock cut, go to you for a decision, or just report.
- **Your numbers are never loosened by statistics (told plainly).** A gate on a client number passes only when its statistical bound (95% or stricter, 6.4) is inside the line.
- The DH 18.2 death targets stay calibration targets, reported to you. The same numbers also decide whether Two Doors switches on.
- **Sign-off lists** for M1, M2 and M3 are in 7. M2 has an internal order: planning, the party-cap decision, the floor-1 balance freeze, the full balance pass, then sign-off.

#### Build cost *(illustr.)*

| Step | Budget | Hardware |
|---|---|---|
| Pull request | ≤ 10 min | 8 cores |
| Nightly | ≤ 2 h | 16 cores |
| Weekly | ≤ 8 h | 32 cores |
| Full simulator sign-off | ≤ 24 h | 32 cores |
| A floor's generation, checks and retries included | p95 ≤ 250 ms, worst ≤ 1 s | speculatively in the background from camp entry; kept at Travel on only if the party's generation inputs did not change, otherwise rebuilt then behind a short loading beat (4.4; after the check) |

**Told plainly:** four numbers decide whether these budgets hold, and none is measured yet: simulated time per floor, generation stage times, how often a retry fails again, and disk write latency on the minimum machine. They are measured on the M1 skeleton before the M2 budgets are frozen, and completed for the M2 stages at the M2 planning gate (after the check).

### 0.2 The client's four decisions (agreed, 2026-11-23)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Should seeded and shared runs count toward progress? | **A.** They appear in run history marked seeded but advance no unlocks, records, codex entries or launcher memory; they write only the approved seen counts (`shownCount`, `fullShown`; EV-19) (4.7, 4.9) | B: unlock content but set no records. C: count exactly like normal runs | SV-16 |
| Q2 | What does a released game do when it cannot build a floor? | **A.** After the release path's attempts fail, the run ends cleanly ("the dream unravels"), its record is kept and marked, and a bug report is filed. This replaces the crash in DH 12.4 for release builds (5.5) | B: a hand-authored safe floor per theme (kept as a future item if release telemetry ever shows a defect, 14.2). C: crash | SV-10 ⚑ |
| Q3 | What happens to a run in progress when a patch lands? | **A.** The run continues; floors already generated are kept exactly; future floors use the new version; the run is marked not comparable for records and shared codes (4.10) | B: the run cannot be loaded on the new version | SV-15 ⚑ |
| Q4 | How is the suspend slot written? | **A.** A snapshot at floor entry plus a state-delta journal after each command, compacted often; load replaces the slot atomically instead of delete-then-write; no roll, reveal, Down or death is shown until its record is written. The agreed guarantee (MAIN 6.6, XI-28, XI Q1: one slot, removed or consumed on load, knowledge forward-only) is unchanged and strengthened (4.8) | B: a full rewrite after every command | SV-14 ⚑ |

### 0.3 Decisions in brief

| # | Topic | Decision | Where | Marking |
|---|---|---|---|---|
| 1 | Model | Composite **R**: Model A's content and registry files as the source of truth now (docs-first); Model B's code-side registrations checked against them by V19 from M1; Model B's review triggers; the simulator layer; the numbers pass's fixes | 3 | lead's call |
| 2 | Key algebra | SplitMix64 mix, FNV-1a-64 labels as input only, counter-based draws, child keyed from the parent's **key**, `Bound` (multiply-shift) everywhere; reference vectors published | 4.1 | lead's call; SV-1 ⚑ |
| 3 | Stream registry | Every stream has a registry row: path, parent, count class, count, stage, consumers, retry scope, owner, wave. An unregistered path does not compile | 4.2 | lead's call; SV-2 |
| 4 | Retries | Keyed `attempt/{a}` and `retry1` children replace "seed + 1" | 4.3 | lead's call; SV-4 ⚑ |
| 5 | Attempt-invariant streams | `curiosities`, `checks`, `trader`, every loot and recruit key; **`dressing` stays attempt-scoped** (FL "dressing included" kept) | 4.3 | lead's call; SV-4 ⚑ |
| 6 | Parent loot | Keyed per floor and per entry block; positions within a floor read as block addresses, order kept | 4.3 | lead's call; SV-5 ⚑ (MAIN 6.3, LE-14) |
| 7 | Keyed checks | One spec on `floorKey/checks` with domains; out-of-combat noticing, Steal and hazard saves move there | 4.3 | lead's call; told plainly; SV-6 ⚑ |
| 8 | Pipeline | GS0–GS17; AP1 after traps (the HE-7 point, "before the loot step"); AP2 after EventPlace; act-then-assert | 4.4, 5.4 | lead's call; SV-9 ⚑ |
| 9 | Ladder | LR0–LR5; HE-7 only for build-proven checks; one ladder in every build, ≤ 3 attempts (`GenMaxAttempts`), then the clean end in release or a hard failure in dev and CI | 5.5 | lead's call; **agreed, Q2**; SV-10 ⚑; parity after the check |
| 10 | Budgets | Per pass on LR4; retry rescue measured; per-run degraded share ≤ 3% *(illustr.)*; pooled reads | 5.6 | lead's call; SV-11 |
| 11 | Reproducibility key | (seed, `genVersion`, `manifestHash`, `configHash`, `unlockHash`); display fields excluded; profile grants in the config snapshot | 4.6 | lead's call; told plainly; SV-12 ⚑ |
| 12 | Saves | Generated floor stored; `GenInputs`; seq-numbered files bound by `runId`; write orders WO1–WO8; outcomes shown only after the write; load replaces the slot atomically; floor-entry snapshot (`suspend.base`) plus state-delta journal (`suspend.journal`) | 4.8 | lead's call; **agreed, Q4**; told plainly; SV-14 ⚑ |
| 13 | Seeded runs and progress | Marked seeded in run history; no unlocks, records, codex entries or launcher memory; approved seen counts only | 4.7, 4.9 | **agreed, Q1**; SV-16 |
| 14 | Patch during a run | Kept floors exact; future floors on the new version; not comparable for records and shared codes; stored snapshots and route state kept | 4.10 | **agreed, Q3**; new lead's call for the details; SV-15 ⚑ |
| 15 | Seed code | 12 base32 characters plus 1 check character (a weighted GF(32) sum over the seed and the preset), with a 5-character build tag | 4.9 | lead's call; SV-16; check symbol and tag after the check |
| 16 | Goldens and release set | Generation fixtures; goldens chosen by set cover; release set 2,000 runs per cap plus logged extension trials | 4.11 | lead's call; SV-18 ⚑ (read) |
| 17 | V19 `Conformance` | Registry, analyzer, DrawLedger, digests, the shared test harnesses | 5.2 | lead's call; SV-19 |
| 18 | Validator model | `s/g/r/t` phases, failure classes, qualified check ids, single owners | 5.1 | lead's call; SV-20 ⚑ |
| 19 | Simulator | Six instruments, event-sourced reports, common random numbers, `ToggleIsolation` on a draw trace | 6 | lead's call; SV-21 |
| 20 | Gates | 13 result classes; decision rule by class; gates bind at the shipped cap; DH 18.2 stays calibration | 6.3–6.4 | lead's call; told plainly; SV-23 |
| 21 | Spread gates | Read on the **observed** spread as the client numbers were set, at N large enough that noise adds ≤ 1 point, by the upper bound of a paired bootstrap (after the check); the de-noised figure is printed beside | 6.4 | lead's call; told plainly; SV-25 ⚑ |
| 22 | Precedence | PR0–PR5; one lever at a time; blast-radius reruns | 6.9 | lead's call; SV-26 |
| 23 | CI | PR ≤ 10 min, main ≤ 30, nightly ≤ 2 h, weekly ≤ 8 h, sign-off ≤ 24 h *(illustr.)*; four numbers measured on the M1 skeleton first | 5.8 | lead's call; told plainly; SV-28 |

### 0.4 Intent, goals and anti-goals (new lead's call: added in the template's shape)

**Intent.** Segments 11 and 12 make every other segment's promises checkable. A run is one number plus the player's choices; every floor is the same floor wherever and whenever it is rebuilt; every failure is caught, bounded and logged; and every balance verdict is read the same way, with its sample size and its consequence stated before it is read.

**Goals.**
1. One key gives one run: the same key and the same commands give the same run on every supported OS, CPU, culture and timezone.
2. No consumer moves another's draws: a chest, a lock, a route or a new feature never changes the next attack roll or another stream's values.
3. Every generated floor passes AP1 and AP2, or climbs one bounded, logged ladder whose rates are budgeted.
4. Nothing on a floor or at camp can be undone by killing the game, and no crash leaves a run without a save.
5. Every gate has an owner, an instrument, a threshold, a sample size and a class that says what a miss does; client numbers are read as the client set them.
6. CI stays inside its time budgets, and sign-off is a checklist, not a judgement call.

**Non-goals.** Any stream's consumers or counts (owners register them); any check's logic; any report's threshold; any lever; gameplay rules; the content of a fallback floor.

**Anti-goals.**
- "Seed + 1" or any arithmetic on seeds.
- A stream drawn by two stages, or a draw count that depends on draw results (other than a per-entry unit count or a leaf stream's declared formula over already-recorded choices, DS3).
- Generation that reads the profile, the clock, the culture or the display.
- A retry policy outside the one ladder, or an unlogged fallback.
- A gate passed by running until it passes, or by redefining a client number.
- A verdict from a toy.
- A save mechanism that shows an outcome before it is written.

---

## 1. How games handle seeds and procedural-generation validation

Confidence: "confirmed" means checked against a public source in research; "reported" means player reports or press; "memory" means general knowledge that was not re-checked.

| Game | What it does | Lesson for us | Confidence |
|---|---|---|---|
| Spelunky HD | One shared daily level set; one attempt per day | A daily mode needs one attempt and a published seed | confirmed |
| Spelunky 2 | Seeded runs block most journal progress; a guide says level seeds vary with sub-30-second level times | Seeded runs do not feed progress (agreed, Q1). Generation must never read player timing | reported / unverified |
| Slay the Spire | One RNG per purpose; seeded and custom runs disable achievements but reportedly still unlock content | Separate streams per purpose; a precedent for Q1 (agreed: our seeded runs unlock nothing either) | reported |
| Hades | No player-facing seeds | Seeds are optional; ours are worth it for testing alone | memory |
| DCSS | Before 0.23 one RNG drove both play and generation, so player actions changed later levels. 0.23 separated them; seeds are stable only within a version; adding or removing a vault breaks mappings; objstat/mapstat tooling reports placement failures | Cross-floor state may read only generation outputs (DS10). Tie compatibility to an explicit version (SV-12). A nightly generation-stats job (5.8) | confirmed |
| Brogue CE | A new gameplay version breaks replays and saves; "gameplay identical" releases keep them; seed catalogs per variant; connectivity holds by construction | `genVersion` in the key; constructive proofs before runtime checks | confirmed / memory |
| Noita, Balatro | Seeds as shareable strings | A readable, checked code (4.9) | memory |
| Dead Cells | A daily run | Daily stays Later | memory |
| Minecraft 1.18 | Terrain regenerates differently after major generation updates; existing chunks are kept | Store generated floors; future floors use the new generator (agreed, Q3) | memory |
| Factorio | Lockstep determinism with per-tick CRC desync detection; strict version matching | Rolling state hashes in replays (4.7) | memory |
| Cogmind | Generate-and-test: maps thrown away, sometimes dozens of times; validity checked right after layout, before content | Our AP1 is the same split; our ladder bounds retries and logs them | confirmed |
| Dwarf Fortress | World generation rejects whole worlds that fail parameter checks and shows rejection counts | The cautionary case: unbounded generate-and-test. We use proofs, budgets and a ladder | memory |
| Spelunky (generation), Unexplored | A solution path first, then templates; graph grammars that are valid by construction | Constructive first (LR0) | memory |
| Random123 (Philox, Threefry), NumPy SeedSequence | Counter-based RNGs and hash-derived child seeds instead of seed + 1 arithmetic | Our key algebra (4.1) | memory |

**.NET pitfalls** (memory; each one enforced by an analyzer, DS11):
- `string.GetHashCode` is randomised per process.
- `Dictionary` and `HashSet` iteration order is undocumented.
- `List.Sort` is unstable.
- Default string comparison is culture-sensitive.
- `System.Random`'s algorithm differs by constructor across versions.
- Integer division truncates toward zero.
- Godot's JSON parser reads numbers as `double`.

---

## 2. The audits

### 2.1 What the approved documents fix (settled unless a flagged SV changes it)

- **MAIN 6.3 (decided).**
  - One seed; separate streams for dungeon, combat and loot, plus `recruit`.
  - Opening a chest must not change the next attack roll.
  - Integers only; no unordered iteration; the core's own generator, "so results do not change between runtime versions".
  - Children added by CMP-3, RT-3, FL-2, TR-17, TL-3, EP-1, LE-14, EV-21 and BR-2.
- **R-SEED-1 to R-SEED-6 (RS 6.4; lead's calls).**
  - `route` is consumed only at run start and never moved by a retry, action or door.
  - Lists are sorted by stable id; one 64-bit draw per bounded integer by multiply-shift; fixed counts and dummy draws.
  - The walk uses no RNG.
  - The key is (seed, manifest hash, configuration-snapshot hash, unlock-snapshot hash); shared and daily seeds use the canonical unlock set.
  - The launcher uses OS entropy with at most 8 redraws, in the order theme, Fallow, F1 boss, onboarding. Shared seeds, daily seeds and the simulator bypass it.
- **MAIN 6.6 (decided).**
  - Run save at safe points; the snapshot is the authority.
  - The suspend slot is written after every committed command, deleted on load, with forward-only knowledge (XI-28, **agreed, XI Q1**).
  - Profile save, with curiosityMemory, uiMemory and bossMemory outside the key.
  - "The simulator writes nothing" (EV-19, **agreed, EV Q1**).
  - The command log is not the save.
- **DH 12.4 HE-7 (lead's call; FL-15 settled).**
  - Constructive reservation at LS11.
  - One floor assertion point "before the loot step".
  - One retry at "seed + 1"; a second failure is a crash bug.
  - "There is no other retry policy."
  - Only assertions proven at build time may trigger it.
- **Segment ladders (approved, binding):**
  - TL 6.22 per-pass retry, then `V11Fallback`;
  - EP 6.1 per-slot chain, 10g, `retry1`, then `V12FloorFallback` (< 1%);
  - LE 10.2 named actions;
  - LP LootBind fallbacks and `LootBindUnplaceable`;
  - BR `ArenaStampFallback` in release.
- **Validator ownership:** V1–V7 RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV, V17 BR, V18 XI.
- **LE-14 (M1).** The parent loot positions never move; count draws are dummies; the Due and arms-floor parent draws are ghosts; one atomic commit of floor N+1 and the camp-exit save. Floor N+1 is generated at camp exit from the party snapshot taken then (LE; FL 6.1 "Generation runs at camp step 11"; ITEMS 10.4).

### 2.2 What the approved documents owe segments 11–12 (hooks)

| From | Owed | Answered in |
|---|---|---|
| MAIN 6.6 / XI-28 | Suspend-slot file mechanics and write cost | 4.8, SV-14 |
| RS 13 | The `route` stream; R-SEED-1–6; `drawableSet` and the key; V1–V7; the signature-type assertion; RouteSample, fixed-arrival and chooser arms; LootDoorInvariant | 4.3, 4.6, 5.2, 5.7, 6.2 |
| FL 13 | `layout`, `dressing`, derivation, counts, V8, the assertion list, the shared floor assertion point; the stream tree, release seed set, retry logging | 4.3–4.4, 4.11, 5.4 |
| ROOM 13 | `terrain` and its counts, `terrainDigest`, V9 enumeration, `PreviewExactness` | 4.3, 5.2 |
| HZ 13 | `hazards`, V10, `hazardDigest`, save fields, reports and arms | 4.3, 5.2, 6 |
| TL 13 | Streams, V11, `trapDigest`, save fields, reports, `FloorTimeByTheme` segment-5 rows | 4.3, 5.2, 6.10 |
| EP 6.19, 13 | The `encounters` node, draw table, `retry1`, per-band `lurkStealth` keys; retry logging; **the CMP-8 tests extended to formations and the solo column** (EP 13, row "12 Validation"; added after the check) | 4.3, 5.5; the CMP-8 tests in 5.2 (`content.*` rows) |
| LE 13 | `lootPlan` derivation and grandchildren, the draw table, 3a and LootBind positions, ghost and dummy draws, the atomic commit; V13 and reports | 4.3–4.4, 4.8 |
| IR 4.17, 13 | The frozen generation order and keying; filter-first before golden seeds; the reserved-draws total ("segment 11 computes") | 4.3, 4.11, 8.1 |
| LP 13 | The `windfall` grandchild, SocketPlan, step 8, LootBind sub-order; V15 | 4.3–4.4 |
| EV 13 | `curiosities` and its draw map, EventDraw/EventPlace order, the resolve safe point, `curiosityMemory`, the launcher order, V16, arms | 4.3–4.4, 4.8, 6.2 |
| BR 4.17, 13 | `BossVariantKey` hash and counts; **template-local coordinates; the event order**; the launcher F1-boss rule's place; V17; **the reports and arms (BR 9)** (added after the check); release-seed coverage | 4.3, 4.6, 4.8, 4.11; reports and arms in 6.2, 6.10, 6.12 |
| XI 4.13 | `PartyKnowledge` serialisation and the 4.1.3 event order; the UI annex; the suspend slot; V18; `KnowledgeLeak` as M1 infrastructure; release-seed coverage of `LosSymmetric` and treasure targets; walker rows; the human-session arm; no new stream | 4.8, 4.11, 5.2, 6 |
| CMP 7.4 | The time model and the per-floor clock sum the gate implies | 6.7–6.8 |
| DH 18.2, 21.2 | "Over the release seed set" (undefined) | 4.11 |

### 2.3 Conflict register (streams, order, retries, validators, reports)

Every conflict the four audits, both models, the numbers pass and the three critics found, merged. Sites were verified in the corpus where line numbers are given.

| # | Conflict | Sites | Resolution |
|---|---|---|---|
| SQ1 | "Seed + 1" is additive, ambiguous (floor? pass? dungeon?), and can alias floor N+1. Read literally on "the dungeon stream", it would move `route` | DH 1026, 2340 (DH-32); FL 161, 421, 596, 679; ROOM 1112; HZ 2669; TL 2238, 3100; RS 594; ENM 3999 | Keyed `attempt/{a}` and `retry1` (SV-4) |
| SQ2 | One floor assertion point "before the loot step" vs the shared point after `trapsResolve` and LootBind | DH 1024–1032; FL 6.1 item 3 (677–681), FL 11.2 vs EP 6.1, LE 6.10 item 4, LP 4.3, EV 4.10, RS V13/V15 rows | AP1 and AP2 (SV-9) |
| SQ3 | FL's `ProtectedIntact` needs step-9 group moves, so it cannot run before loot | FL 11.2 | Split: structural part at AP1, group part in V12g (SV-9, SV-20) |
| SQ4 | "There is no other retry policy" vs the TL, EP, LE, LP and BR ladders | DH 1032 | One ladder (SV-10) |
| SQ5 | HE-7 re-derives every child, including `curiosities`, so the Fallow forecast breaks | DH 12.4, FL 6.1, EV 4.5.4, R-SEED-6 | `curiosities` keyed on floorKey (SV-4) |
| SQ6 | Parent loot is one sequential stream with data-dependent counts (DH 12.5 item 7, wand charges, phial 1 or 3 draws, vendor potions, trader). This conflicts with R-SEED-3's "never depends on the data", and retries shift later floors | ENM 10.5 step 3, DH 12.5, CN 14.7, ITEMS 10.4 | Per-floor keys with per-entry blocks; phial always 3 (SV-5) |
| SQ7 | `Derive` is written seven ways; FL's `parentState` makes children depend on parent position | FL 583–592; TR, EP, LE, IR, LP, BR `Hash(routeSeed,…)`, TL `H(floorSeed,…)`, CMP tuples, EV `H(sub,…)` | One key algebra (SV-1) |
| SQ8 | No PRNG or mixer is named; FNV-1a is used alone as a hash | all | SplitMix64; FNV labels as input only (SV-1) |
| SQ9 | `mod` vs multiply-shift | EV 908–909 (`mod 1000`), LP 774 (`mod 1000`), ENM 1611 and EP 1602 (`mod 20`) | `Bound` everywhere (SV-1, SV-6) |
| SQ10 | Keyed tuples differ; `floorSeed` is undefined; `lurkStealth` has no `die` and ET4 puts it "on the encounters stream"; Curiosity checks have a different root and no die or attempt | TL 6.22, LP 4.7.8, EV 4.3/4.10, EP 6.8.5/ET4, ENM 12.2 | One domain spec (SV-6) |
| SQ11 | Out-of-combat hazard saves are on the combat stream | HZ 929, 2663, 11.2 vs TL-3 principle | `h:envSave` (SV-6) |
| SQ12 | **New (critic):** out-of-combat noticing and Steal are on the combat stream, so exploration changes the next fight | ENM 1608 (lead's call); DH 188 "every command … can roll noticing" | `h:notice`, `h:steal` (SV-6) |
| SQ13 | TL builders' option to salt `checks` for shared or daily seeds breaks "one key, one run" | TL 2277, KB17 | Withdrawn (SV-7) |
| SQ14 | The pipeline is written 5–6 ways: EventDraw vs terrain; hazards after the reservation step vs after all terrain; SocketPlan before 2a's bait cache; no position for the arena stamp, with two sources for the arena interior; Windfall timing (LP 4.2 vs 4.7.10) | MAIN 6.3, ENM 10.5, FL 6.1, TR 6.2.5, TL 6.22, EP 6.1, LE 6.10.4, LP 2.1/4.2, EV 4.10, BR 4.1.3 | One stage table (SV-9) |
| SQ15 | **New (critic):** HZ's tag draws sit inside the terrain reservation, which is a prefix of the same counter. When hazards ship in M2, every M1 terrain draw shifts | ROOM 6.2.5 | Reserve the tag maximum from M1 (SV-3) |
| SQ16 | **New (critic):** `encounters` is drawn at two pipeline positions (steps 2/2a and 4–11) | EP 6.1 | Sub-keys `plan` and `place`, counts unchanged (SV-2) |
| SQ17 | MAIN 6.3 omits the `hazards` grandchild; EP calls the floor children "siblings"; stale "dungeon stream" and "parent substream" texts | MAIN 6.3; DH 1020, 1026; FL 6.1; ENM 8.2, 10.5 intro and steps 2, 5, 10; ITEMS 10.9, 18.4 | Text (SV-31) |
| SQ18 | The trader's two "dungeon-stream draws at step 2" have no home after EP-1 | ITEMS 10.9 / I-14, ENM 10.5 step 2 | `floor/{N}/trader` (SV-32) |
| SQ19 | Retry nesting is undefined; the Tattle and Wager bindings are owned by two ladders | TL, EP | Nesting rules; TL owns the actions (SV-10) |
| SQ20 | `LootBindUnplaceable` is a "hard failure" with no action; drafts routed it, and `GuaranteedItemUnreachable`, to HE-7 without a proof | LP 4.3; DH 21.2 | Build proof; then a defect, not a retry (SV-10) |
| SQ21 | V10, a build validator, carries "HE-7 single retry at seed + 1" | HZ 2669 | V10's runtime lookup is an AP1 floor-retry assert (SV-10) |
| SQ22 | The key has no generator version; the manifest hash is not canonical; BR's cutState is "logged in the key" | R-SEED-5; BR 4.10.1 | `genVersion`; canonical hash; cutState inside the manifest (SV-12) |
| SQ23 | **New (critic):** profile-held rules inputs (difficulty tier, boons, Nightmare tier) are not in the key | MAIN 490 vs RS 2637 | Into the config snapshot (SV-12) |
| SQ24 | The static floor is regenerated from "seed and configuration" without its real inputs, and breaks across generator patches. This conflicts with "full core state" | FL 11.1 line 2258 ("Save: the seed and configuration regenerate the static part, checked against layoutDigest"), TL 11.2 vs MAIN 6.6 | Store `FloorStatic` plus `GenInputs` (SV-14) |
| SQ25 | Transitions are unspecified; **new (critic):** the "read, validate, delete, write back" crash window rewinds the floor, and outcomes shown before the write allow probing | MAIN 6.6, XI 4.12, EV 4.10 step 5, LE 6.10.6 | WO1–WO7; atomic replace; outcome frames wait (SV-14; agreed, Q4) |
| SQ26 | The suspend write cost has no budget; the drafted 4 ms / 16 ms budget cannot be met off NVMe; a command-replay journal cannot survive a patch | XI-28 | Overlap budget plus a state-delta journal (SV-14; agreed, Q4) |
| SQ27 | Simulator vs launcher vs profile: "writes nothing" vs EV's Streak arm writing profile memory | MAIN 497, EV 1444, EV 1523, BR | LauncherEmu with sandbox profiles (SV-13) |
| SQ28 | The release seed set and golden seeds are undefined; floors 2–5 cannot be regenerated without their inputs | DH 21.2, 18.2; FL; CMP; IR; LE; LP; BR; XI | Generation fixtures (SV-18) |
| SQ29 | Illustrative draw constants are not locked before goldens; TL and HZ count formulas are missing | EV, TR, LE, CMP, EP, TL, HZ | Locks and owner deliverables (4.5, 4.11) |
| SQ30 | IR's "about 1,300 reserved draws" vs about 4,500 of loot address space | IR 571 | DrawLedger publishes two figures (SN1) |
| SQ31 | The V7 signature-twice assertion is homeless and duplicated | RS V7, EP 11.2 | V12g authority plus a V12t sample row (SV-20, request to EP) |
| SQ32 | The RS 11.2 wave column is wrong (V11 M1 skeleton per TL 484; V17 M1 subset per BR 1265; V12 parts; V15 M1 rows); its header "the build fails" contradicts shipped fallbacks | RS 2671, 2685 | SV-20 |
| SQ33 | The s/g/t split is inconsistent; simulator gates sit inside V12, V14 and V17s(k) | RS 11.2, EP, IR, BR | Phase model; gates move to the report registry (SV-20, SV-21) |
| SQ34 | V16g is "after V15g" yet `SlotLegal` runs "before V15g"; V17g's dependencies are incomplete; V18g rows read `PartyKnowledge` | RS, EV 9.2, BR 9.2, XI 9.2 | Split V16g; V17g after V15g and V16g; V18r (SV-20) |
| SQ35 | Check-name collisions and multi-owner checks (`TemplateReadability` V8/V9, `ContactLethalityBound` V9/V10, `NameCollision` ×7, `OneClockPerRoom`, `ProtectedIntact`, the theme's 6, …) | many | Qualified ids, single owners (SV-20) |
| SQ36 | Report-name collisions (`ClassThemeSpread` RS vs HZ; `EnvKillShare`/`EnvironmentKillShare`; `ContactHazardDowns` vs `EnvHeroDownShare`; `SecretFindRate`/`SecretFoundRate`; overloaded `RoundsPerFightDelta`, `FloorTimeByTheme`) | RS, HZ, ROOM, XI, TL, EP | SV-24 |
| SQ37 | Arm-name collisions ("Reference" ×3, "Oracle" ×2, "BASE" ×6, "balanced" ×2); `PatrolFightRate`'s BASE | RS, CMP, DH, FL, EV, XI, LP, EP | Qualified catalogue; `XI.BASE` (SV-22, SV-24) |
| SQ38 | Gate-status contradictions: DH 18.2's "not validation gates" vs BR-40, EV-3/EV-12 and RS 6.11 gate 5; FL's walker "gate" vs CMP 7.4 | DH 18.2, BR, EV, RS, FL, CMP | Result classes (SV-23) |
| SQ39 | Gates with no threshold or no instrument (`LauncherCheckMs`, `MarksPerScreen`, `V11Fallback`, `GenerationRetried`) | RS, XI, TL, FL | SV-23–SV-25 |
| SQ40 | A novice arm exists only in ARC 5.4, but EV, BR and HR gates need one | ARC 5.4, EV, BR, HR | Generalised skill arms (SV-22) |
| SQ41 | Unlock-set enumeration policy is inconsistent; V5 "every subset" looked expensive | V5, V13s, V16s | **Kept as written through M3** (11 core-s); revisit at M4 (8.3). Charm-family unlocks add subsets under the same policy (request CA-21 from the charm abilities design 4.10; pending, 4.6; M3) |
| SQ42 | `EventDrawRunBlind` vs Two Doors; and a retried F1 can lose a forecast Curiosity's host | EV 4.10, EV 4.5.4 | Flagged clarification and `F1HostAlways` (SV-30) |
| SQ43 | Two Doors has 6 gates plus gate 7 for the Last Hollow choice, not 5 | RS 1003–1050 | Corrected throughout |
| SQ44 | Floor clock lines are never summed; `MarksPerScreen` has no harness | CMP 7.4, XI 9.3 | `FloorClockLedger`; ViewModel instrument (SV-26, SV-21) |
| SQ45 | Minor text: EV 4.5.4 tuple omits the boss rule; LE 6.10.6 "Muster candidates"; CMP pledge keyed by floor; "RT" vs "ROOM" tag (TL 132, EP 141, HZ "RT 6.11"); "27 docs"/"28 documents"; the split ROOM 11.3 table; HZ VP1–VP10 vs LE VP | various | SV-31 |
| SQ46 | Recruit exhaustion has a variable count; `cosmetic` is display on a core child; the Oracle stream and display hashes are ad hoc | CMP 7.2, IR, LP, EV, XI | Per-entry keys; `display` and `sim` keys (SV-2) |

---

## 3. The models

### 3.1 The models

- **Model A, central registry.** Four JSON registries (streams, validators, reports, arms) owned by segments 11–12, with generated C# enums and V19 checking that code and registry agree. Its strengths are the most exact content: per-entry loot blocks, attempt-invariant host-blind streams, a `DisplayKey` type the core cannot reference, the AP1/AP2 equivalence rule, write orders with sequence numbers, and 71 conflicts mapped. Its weaknesses: the registry is a merge hotspot; 8 release attempts break the 1 s budget; inconsistent ladder budgets; 64 random goldens cannot give coverage; the slot budget is unachievable.
- **Model B, federated contracts.** Owners register their own streams, stages, checks, reports and arms next to their code; segments 11–12 own eight contracts; master tables are generated. Its strengths: computed reference vectors; a registry handle so an unregistered stream does not compile; one stage per stream; act-then-assert at the assertion points; golden and release items as generation fixtures; a display hash split out of the manifest hash; no content deletion after release. Its weaknesses: padded block bases that need maxima nobody has published; leaf-variable recruit lists; a safe floor per theme as new content; the generated table cannot exist before M1 code; owner drift.
- **Simulator and sign-off layer** (a graft source, not a full model): result classes separating metric from action; `ToggleIsolation`; instruments; three-way verdicts; structural partners for zero gates; `TimeModelError`; precedence and blast radius; the M2 internal sequence; an M1 skeleton.
- **Numbers pass:** draws, ladder rates, generation time, static and simulator cost, golden set cover, save cost, CI wall time. It found 16 budget failures, each with a fix (8).

### 3.2 Judges' scores (mean of three judges, 1–10; the simulator layer was scored by two)

| Model | Determinism safety | Clarity for builders | Maintainability | Low test cost | Fit with docs | Mean |
|---|---|---|---|---|---|---|
| A, central registry | 9.0 | 9.0 | 7.0 | 6.0 | 7.0 | **7.6** |
| B, federated contracts | 9.0 | 7.7 | 7.7 | 6.0 | 6.7 | **7.4** |
| Simulator and sign-off layer | 7.5 | 8.0 | 7.5 | 5.0 | 8.0 | 7.2 |

Two judges chose Model A as the frame with Model B grafts. One judge chose Model B's governance with Model A's content. All three adopted the simulator layer and the numbers fixes wholesale.

### 3.3 Composite R (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

All three judges point to the same hybrid:

1. **Now (docs-first), Model A's registry files are the source of truth.** `streams.json`, `validators.json`, `reports.json` and `arms.json` live under the content tree. Each row has an owner segment.
2. **From M1, Model B's code-side registrations** (`[Stage]`, `[Check]`, `[Report]`, `[Arm]`) are **checked against those files by V19s**. A mismatch fails the build. Once the generated tables are complete (M2), docs cite the generated table.
3. **Owners edit their own rows without review by segments 11–12** (Model B's autonomy). Review is needed only for changes to the stage skeleton, an assertion point, the ladder, the key algebra or the reproducibility key, and for anything that changes a golden digest (a `genVersion` bump). This removes Model A's bottleneck. **After the check (lead's call, after the check):** the AP2 act order is a code-fixed ordinal and counts as part of an assertion point, so reordering registry rows can never change it without that review (5.4).
4. **Model A's content where the models differ:**
   - per-entry loot and recruit keys over padding;
   - floor-level attempt-invariant trader;
   - `DisplayKey` as a compile-time type;
   - write orders with sequence numbers.
5. **Model B's mechanics:**
   - the exact key algebra and its reference vectors;
   - the registry handle;
   - one stage per stream;
   - act-then-assert;
   - fixtures;
   - the display hash;
   - no content deletion.
6. **The simulator layer and the numbers fixes**, amended by the three critics (Appendix A).

---

## 4. Seeds in full

### 4.1 Key algebra (change SV-1; lead's call; M1; frozen before the M1 golden seeds)

All arithmetic is on unsigned 64-bit integers, modulo 2^64.

**Wrapping arithmetic in every build (lead's call, after the check).** `Mix`, `Absorb`, `Draw`, the FNV-1a loop and `Bound` depend on wrapping multiplication and addition, while DS11 turns checked arithmetic on in debug and CI. The key-algebra and stream code therefore sits in **one file whose arithmetic is inside explicit `unchecked { }` blocks**; DS11's analyzer allows wrapping arithmetic only in that file. Every `Derive` and `H` index is a `ulong` (or a `uint` zero-extended), never a signed type: a negative `int` would throw in checked code and sign-extend in unchecked code, giving two keys. `StreamRow.indices[]` declares each index's unsigned type, and V19s rejects a signed index type at any call site.

```
γ            = 0x9E3779B97F4A7C15
Mix(z)       : z ^= z >> 30; z *= 0xBF58476D1CE4E5B9;
               z ^= z >> 27; z *= 0x94D049BB133111EB;
               z ^= z >> 31; return z                  // SplitMix64 finaliser (Stafford 13)
LabelId(s)   = FNV-1a-64 over the UTF-8 bytes of s     // offset 0xcbf29ce484222325, prime 0x100000001b3
Absorb(h, x) = Mix(h ^ Mix(x + γ))                      // order-sensitive
Derive(k, label, i1..in) =
               h = Absorb(k, LabelId(label));
               for each ij: h = Absorb(h, (ulong) ij);
               return Absorb(h, 0xD1B54A32D192ED03 ^ n) // arity-tagged terminator
Draw(k, i)   = Mix(k + (i + 1)·γ)                       // the i-th SplitMix64 output from state k
Bound(u, n)  = high 64 bits of u × n                    // Math.BigMul; multiply-shift (R-SEED-3); asserts n ≥ 1 in every build
H(k, domain, parts..) = Draw(Derive(k, domain, parts..), 0)
D20(u)       = 1 + Bound(u, 20)
```

**Rules:**
- **K-rule 1 (fixes SQ7).** A child derives from its parent's **key**, never from the parent's counter or state. FL 6.1's `Derive(parentState, …)` becomes "the parent's key" (change SV-1; FL-2 intent unchanged). BR's `Hash(routeSeed, "boss9", recordId)`, TL's, EV's, LP's and EP's `H(…)`, and CMP's tuples are all this one `Derive`/`H`.
- **K-rule 2.** A string index (record id, option id) enters `Derive` only as `LabelId(id)`.
- **K-rule 3.** Ids and labels are ASCII `[a-z0-9_.:-]` (V19s). `LabelId` arguments are constants or registry-id types, never culture-formatted strings.
- **K-rule 4 (fixes SQ9).** Every bounded integer uses `Bound`. `mod` on a raw draw is banned, with one exception: the approved Resistant draw is `r = Bound(u, 720)` and then `r mod n`, which is exact for n ≤ 6 because 720 is divisible by every n ≤ 6 (720 = 6! = 12·lcm(1..6); corrected after the check). V19s asserts n ≤ 6 for every theme. Weighted picks follow FL 6.1 as approved, over `Bound`. **`Bound(u, 0)` is never legal (lead's call, after the check):** it asserts in every build, and an empty or zero-weight legal set routes to its owner's LR1 fallback, or is a defect if the owner has none.
- **K-rule 5.** FNV-1a is only an input to `Mix`, never the hash itself. This answers the low-bit concern about FNV.
- **K-rule 6 (domain separation).** On any one parent, a stream child and a keyed-hash domain may never share a label (V19s `DomainDisjoint`). New keyed domains are prefixed `h:`. EV's approved in-slot labels `"rarity"`, `"pool"` and `"perm"` are kept as written; their parent, the slot sub-key, has no stream children.
- **K-rule 7 (keyed sorts; lead's call, after the check).** Every keyed sort orders by (H value, then the ordinal comparison of the stable id), so two equal 64-bit values still give one order (DS4).

**Reference vectors** (computed with the scratch reference implementation; pinned by V19t `KeyAlgebraVectors`; a C# port must reproduce them; the rows below the first eight were added after the check and recomputed with the same implementation under the 4.2 path grammar). `KeyAlgebraVectors` also runs under a checked-arithmetic debug build, which must give the same outputs:

| Input | Output |
|---|---|
| `Draw(0, 0)` | `e220a8397b1dcdaf` (the published first SplitMix64 output, which confirms `Mix`) |
| `LabelId("layout")` | `9c4f784aa5ce135f` |
| `LabelId("")` | `cbf29ce484222325` |
| runSeed `0x0123456789ABCDEF`: `dungeon` | `99b0558c8449420f` |
| `dungeon/floor/1` | `8224e58417674634` |
| `…/attempt/0` | `408b2970acba60f5` |
| `…/attempt/0/layout` | `ffc7031c11fad2b3`; its `Draw(·, 0)` = `5500e3c923d386c0`, d20 = 7 |
| `dungeon/floor/1/curiosities` | `2aad85054c0f15e0`; its `Draw(·, 0)` = `6cd955ee2b0e5b74` |
| The rejected reading `Derive(Derive(dungeon,"floor"),"1")` (shown so that a port can see it is wrong) | `770d8047d64bd714` (≠ `dungeon/floor/1`) |
| `dungeon/route` | `ccd4caa81e146b07` |
| `dungeon/route/boss9/{recordId:}` with the test id `boss.warden`, i.e. `Derive(route,"boss9",LabelId("boss.warden"))` | `feb6e74ee2462524`; draws 0 and 1 = `6d404c06a9d65b42`, `d9f923d397b2e462` |
| `recruit/camp/1/slot/0/entry/3` (three indexed segments) | `5937484078fa307a` |
| `loot/lootPlan/1/windfall/0` (arity 1) vs `loot/lootPlan/1/windfall` (arity 0, hypothetical) | `ef8041b38b01fd70` vs `5eae58fb0fa3e82c` |
| `H(dungeon/floor/1/checks, "h:notice", LabelId("unit.hero"), LabelId("unit.mastiff.1"), 0, 0)` | `ab0885a7f33fd7ad`, d20 = 14 (`checks` key `b8e7d03a7635ebca`) |
| `Bound(0xDEADBEEFCAFEBABE, 1)`; `Bound(2^64−1, 20)`; `Bound(0xDEADBEEFCAFEBABE, 2^63)` | `0`; `19`; `6f56df77e57f5d5f` |
| Keyed-sort tie | A stubbed `H` returning equal values for ids `a.x` and `a.y` sorts `a.x` first (K-rule 7) |

**Told plainly:** SplitMix64 counter streams are statistically sound for a game (they pass BigCrush; general knowledge, not re-verified) but are not cryptographic. A daily leaderboard would need anti-tamper beyond this.

### 4.2 Streams: naming, count classes, the registry (change SV-2; lead's call; M1)

- **A stream** is (key, counter). `Next()` returns `Draw(key, counter++)`. `Skip(n)` adds n to the counter, so **a dummy draw is a counter skip** and "fixed count" means the counter equals its declared count when the stage ends.
- **A stage run opens fresh streams (DS15; lead's call, after the check).** Every run of a stage, including an LR2 or LR3 rerun and an AP2 act's rerun span, opens fresh stream instances at counter 0, and the DrawLedger closes per stage run. A rerun from an unchanged key therefore reproduces the same draws; it never continues a counter.
- **A path** names every key: `dungeon/floor/3/attempt/0/encounters/place`. The key of a path is a left fold of `Derive` from `runSeed`. Doc aliases are stored beside the path (for example "floorSeed" for `dungeon/floor/{N}`).
- **Path grammar (lead's call, after the check; frozen with SV-1 before the M1 goldens).** `path = seg ("/" seg)*`; `seg = label ("/" idx)*`. **Each label together with the indices that follow it is one `Derive` call**: `dungeon/floor/1` is `Derive(dungeon, "floor", 1)`, never `Derive(Derive(dungeon,"floor"),"1")` (4.1 vectors). An `idx` is a decimal literal or a registry-typed id, passed as `LabelId(id)` (K-rule 2) and marked `{name:}` in the registry (for example `boss9/{recordId:}`, `gap/{needId:}`). A `label` is a constant, or a registry-enumerated composed constant such as IR's approved `lineRoll:{source}`, which expands to one registered label per source kind and is never built from data at run time; `{kind}` under `lootPlan` is likewise a label slot over the registry's kind list. Arity is part of the key: `windfall/0` and a bare `windfall` are different keys. **`StreamRow.label` and `indices[]` are normative; the path string is display only.**
- **Registry handle (fixes drift).** Core code opens a stream only through a source-generated registry handle. An unregistered path does not compile.
- **One stage per stream.** No counter stream is drawn by two stages. A second phase uses a child path. This is why `encounters` gains the sub-keys `plan` (step 2a) and `place` (steps 5, 5b, 10a, 10b). EP's per-step counts are unchanged (change SV-2 to EP 6.1, text only). A `vector` key may have several registered readers, because every read is by absolute index and nothing advances (after the check).
- **Count classes** (every row declares exactly one):

| Class | Meaning | Ledger rule (V19g) |
|---|---|---|
| `node` | Never drawn; only derives children | counter = 0 |
| `fixed` | Constant per manifest and configuration | counter = constant |
| `fixed-per-data` | A formula over manifest and configuration maxima, never over draw results | counter = formula |
| `leaf-formula` (after the check) | A formula over manifest and configuration data **and generation choices already recorded when the stream is drawn**: its own earlier draws or an earlier stage's recorded output (for example FL's layout count and ROOM's terrain count over the drawn Blueprint's #Sites, maxNodes and maxEdges). Allowed only on a leaf stream: keyed children never read its counter, so no other stream can move | counter = formula evaluated on the recorded choice (the Blueprint id in `GenerationLog`) |
| `vector` (after the check) | A fixed-length array read by absolute index `Draw(k, i)`, with no counter; several registered readers may read it at different times (the launcher, a stage, the Stratum Card). Indices are 0-based | every read index < the declared length; no counter |
| `per-entry` | Each unit (entry, node, slot) has its own key and a fixed count. The number of units may vary, because the parent takes no draws | each unit = its count; units ≤ declared cap |
| `keyed` | Stateless `H` addressing in registered domains | no counter |
| `sequential-saved` | A play-time counter saved in the run save and slot | saved and restored |
| `display` | Read only by the display layer; a `DisplayKey` type the core cannot reference | not ledgered; an analyzer proves the core never reads it |
| `sim` | Derived under `simRoot` only | must not derive from `runSeed` |

### 4.3 The master stream table (changes SV-2 to SV-6, SV-32; lead's call)

**The root** is `runSeed`, a 60-bit integer from one of three sources: the launcher (OS entropy masked to 60 bits, R-SEED-6), a typed share code (4.9), or the daily rule (Later). Paths are relative to `runSeed`. {N} is the floor index 1–5, {a} the attempt index, {k} the camp index 1–4.

#### Run level

| Path (alias) | Owner | Class | Count | Stage | Consumers | Retry scope | Guarantees | Wave |
|---|---|---|---|---|---|---|---|---|
| `dungeon` | MAIN | node, never drawn | 0 | — | — | — | MAIN 6.3 ⚑ | M1 |
| `dungeon/route` | RS | keyed sorts (SV-8): theme order by `H(route,"themePerm",themeId)`; boss order per cell by `H(route,"bossOrder",themeId,floor,bossId)`; final order by `H(route,"finalOrder",bossId)`; M3 `omen` by `H(route,"omen",themeId)`. Fallback if the RS owner refuses SV-8 (14.2): the approved Fisher–Yates, fixed per manifest | 0 sequential draws (with SV-8) | GS0.3 | ENM 10.5 steps 1 and 6, the walk (no RNG), door cards, Stratum Card, camp-4 preview, launcher forecast | **never** | R-SEED-1/3/4, V6 | M1 stub, M2 |
| `dungeon/route/boss9/{recordId:}` (`BossVariantKey`) | BR | `vector` (after the check) | length 4, 0-based: 0 Rendition, 1 mirror bit, 2–3 reserved (M3 Opening, Nightmare) | read by absolute index at GS4, by the launcher and by the Stratum Card | arena stamp, Stratum Card, launcher forecast | never | BR-2, V17g, V17t | M2 (indices 2–3 M3) |
| `loot` | MAIN | node | 0 | — | — | — | MAIN 6.3 ⚑ | M1 |
| `loot/run` | CN | `vector` (after the check) | **length 3, always** (0 offer, 1 floor, 2 channel; SV-5) | GS0.4 | Reprieve Phial pre-roll (CN-27) | never | V13t (m) | with CN-27; the key costs nothing before then |
| `recruit` | MAIN/CMP | node | 0 | — | — | — | CMP-3 | M2 |
| `recruit/camp/{k}/slot/{s}/entry/{e}` | CMP | per-entry | `RecruitDrawFixedCount` each; e < `candidateDrawMax` 32 *(illustr.)* | GS16 | Muster lists | never | per-entry stability | M2 |
| `recruit/camp/{k}/gap/{needId:}` | CMP | fixed | per CMP 7.2 | camp k entry | card A | never | CMP-3 | M2 |
| `…/slot/{s}/armReroll`, `…/gap/{needId:}/armReroll`, `…/slot/{s}/eyeReroll` | CMP | fixed per kit data | per CMP | camp, simulator | CL 6.6 arms, HR 5.4, Recruiter's Eye | never | CMP-3 | M2 |
| `recruit/camp/{k}/cosmetic` | CMP | fixed (core child kept as approved; its only consumer is the palette) | per CMP | camp | palette | never | text note only (SV-31) | M2 |
| `recruit/camp/{k}/pledge` | CMP | fixed, reserved | — | — | — | — | camp k has the same index as floor k (SV-31) | Later |
| `levelup/camp/{k}/unit/{unitId}/level/{L}` (change CH-2 ⚑, camp and hub design 4.2 A.2) | ABL (offer filter, ABL 10.2) | fixed | a fixed draw count per level offer; companion 3/6/9 choices draw nothing (CMP's fixed two-option grammar) | camp k: a further pending level's offer is drawn inside the WO8 write of the catch-up pick before it (4.8.3) | hero catch-up offers (ENM 16 multi-level catch-up) | never | V20t `CampProbeResistance` | M2 |
| `combat` | MAIN/ABL | sequential-saved, **run-wide as approved** | per command | play | in-combat attacks, saves, contests, initiative, in-combat noticing and Steal, in-combat hazard saves | saved counter | MAIN 6.3 ⚑, ABL 14.4 | M1 |

Model A's `combat/floor/{N}` re-keying is **dropped**. It fixed no conflict and would re-key a decided stream.

#### Per floor

| Path | Owner | Class | Count | Stage | Retry scope | Guarantees | Wave |
|---|---|---|---|---|---|---|---|
| `dungeon/floor/{N}` (floorKey, "floorSeed") | RS (RT-3) | node, never drawn | 0 | — | attempt-invariant | — | M1 |
| `…/curiosities` | EV | `vector` (after the check) | **length 12** *(illustr.; locked before the M2 goldens)*. Draw map 0–11 (0-based): 0 Curiosity-slot sub-key; 1 Interlude sub-key; 2 Sequel rarity (F2, M3); 3 F3 Reading (M3); 4–11 dummies. In a slot: `Bound(H(sub,"rarity"),1000)`, `Bound(H(sub,"pool"),1000)`, `perm` by keyed sort (SV-8) | GS3; the launcher reads F1 index 0 | **attempt-invariant** (SV-4) | EV-21, V16g `FixedDrawCount`, V19t `ForecastSurvivesRetry` | M2 |
| `…/checks` | TL (form), 11–12 (domains) | keyed | domains below | play | attempt-invariant | TL-3 ⚑ | M1 |
| `…/trader` | ITEMS | fixed | 2: presence, stock count | GS8 | attempt-invariant | SV-32 | **Later** |
| `…/attempt/{a}` (attemptKey) | DH / 11–12 | node | a = 0 normal; then each further attempt takes the next unused index, a ≤ `GenMaxAttempts` − 1 = 2 *(illustr.)*: a = 1 is the HE-7 retry (LR3) or the first defect-path attempt; a = 2 only the defect path. Every build runs the same ladder (5.5; after the check) | — | defines the retry scope | DH 12.4 ⚑ | M1 |
| `…/attempt/{a}/layout` | FL | `leaf-formula` (after the check; FL's approved formula unchanged) | 2+1+1+1+(2+#Sites)+3·maxNodes+2·maxEdges+3 (FL 6.1), over the drawn Blueprint | GS2 | attempt | V8 `DrawCount`, FL-2 | M1 |
| `…/attempt/{a}/dressing` | FL | display | per Blueprint | **display side, not run by Core** (after the check): `FloorStatic` stores `attemptUsed`; Core exposes the raw attempt key as a one-way `ulong`, from which the display assembly derives its own `DisplayKey` (DS12) | **attempt-scoped** ("dressing included", FL 6.1, kept) | FL-2 | M1 |
| `…/attempt/{a}/terrain` | ROOM | `leaf-formula` (after the check; ROOM's approved formula unchanged) | reservation 2+⌊0.3·maxFightSlots⌋+**`hzTagMax`** (reserved as dummies from M1, SV-3); then per node maxNodes×(1+2·maxSockets), maxSockets 16 *(illustr.)*; the arena node's slot is spent as dummies | GS5 | attempt | V9 `TerrainDrawCount`, TR-17 | M1 |
| `…/attempt/{a}/terrain/hazards` | HZ | fixed-per-data | **formula owed by HZ (request)**, before the M2 goldens; arena node dummies | GS6 | attempt | HZ-28, V10 | M2 |
| `…/attempt/{a}/traps` and `…/traps/retry1` | TL | fixed-per-data | **formula owed by TL (request)**, before the M1 goldens | GS7 | attempt; LR2 child | TL-3, V11 | M1 |
| `…/attempt/{a}/encounters` | EP | node | 0 | — | attempt | EP-1 | M1 |
| `…/encounters/plan` | EP | fixed-per-data | 1+2·floorCap+1 (step 2a) | GS8 | attempt | V12g | M1 |
| `…/encounters/place` | EP | fixed-per-data | 4 per slot × maxSlots (steps 5, 5b, 10a, 10b); the boss slot's draws spent as dummies (the escort comes from the Guise) | GS12 | attempt | V12g | M1 |
| `…/encounters/retry1/plan`, `…/retry1/place` | EP | as above | as above | GS8, GS12 | the LR2 child (EP's approved label) | EP 6.1 | M1 |
| `…/attempt/{a}/trapsResolve` and `…/trapsResolve/retry1` | TL | fixed-per-data | **formula owed by TL (request)** | GS13 | attempt; LR2 child | TL-3, V11 | M1 |

#### Loot (change SV-5)

| Path | Owner | Class | Count | Stage | Retry scope | Wave |
|---|---|---|---|---|---|---|
| `loot/lootPlan/{N}` (approved `Derive(lootStreamSeed,"lootPlan",N)`) | LE | node | 0 | GS10 | attempt-invariant | M1 |
| `loot/lootPlan/{N}/{kind}/{entryIndex}` | LE, IR, LP | fixed per kind (table below) | | GS10 | attempt-invariant | M1–M3 |
| `loot/lootPlan/{N}/lineRoll:{source}/{entry·16+option}` | IR | fixed 4 (line, alternate, element, wand charges); options 0–3, fallbacks 4–7, hero redraw 8 | | GS10 | attempt-invariant | reserved M1, used M2 (IR-14) |
| `loot/lootPlan/{N}/curseGate/0` | IR | fixed 2 | | GS10 | — | Later use; M4 by change HL-29 |
| `loot/lootPlan/{N}/windfall/0` | LP | fixed 4 (presence by `Bound(d,1000)`, host, pad, pad) | | GS10, at the start of step 8 inside 3a | — | M3 |
| `loot/lootPlan/{N}/dealShuffle/retry1` | LE | as `dealShuffle` | | GS10 | the LR2 child for V13g (e) (request: LE confirms the key) | M2 |
| `loot/floor/{N}` | LE | node | 0 | GS11 | attempt-invariant | M1 |
| `loot/floor/{N}/{block}/{entry}` | LE (ENM 10.5 step 3 order) | per-entry; **9 draws per identity entry** (filter-first) | blocks in approved order: `containers`, `carried`, the boss channel, `trader` (Later), then any returned trader slots on `containers` (Later), `dhBlock` (DH 12.5 items 2–7, item 7 per sundry entry), `resistant` (r ∈ [0,720) per authored Resistant notable; IR confirms the owning entry), `wandCharges` (per wand entry), `camp` (next camp's notables), `vendorPotions` (**always `max(table)` entries, a fixed draw count; Core trims the stock to the party table's count for `partyCountAtGeneration` and discards the rest**; change CH-16 ⚑, camp and hub design 7.2; V20t `VendorStockTrimmedInCore`), `traderService` (the trader's service draw, Later) (ITEMS 10.9, ENM 10.5, as SV-32 wrote them; cross-document comparison 2026-11-23, CC-45) | GS11 | attempt-invariant | M1 |

**Retired by SV-5 (told plainly):** the parent count dummies and the Due and arms-floor ghost draws (LE-14) existed only to hold positions on one sequential stream. With keyed blocks there are no positions to hold, so they are not drawn. The approved **order** of step 3 is kept as the order in which blocks are evaluated; "positions" become block addresses. *(Retired: the LE owner applied SV-5, approved by the client on 2026-11-23, so the refusal fallback (keep the sequential parent stream, pad every data-dependent block to a declared cap, and snapshot and restore the parent counter at GS1 on any retry) no longer applies; LE 6.10 item 2; cross-document comparison 2026-11-23, CC-108.)*

Per-kind counts under `lootPlan`:

| Kind | Draws |
|---|---|
| `variant` | 1 |
| `dealShuffle` | maxSocketsPerRank − 1 per rank (12 *(illustr.)*) |
| `crossRankSwap` | 1 |
| `dreamsDue`, `armsFloor` | 9 each |
| `fitPremium` | 9 per entry |
| `waxKeyIdentity` | 9 per cache |
| `bossCard`, `viaticum` | 9 × 4 |
| `bossCardFallback`, `viaticumFallback` | 9 × 5 |
| `wager` | 9 × 4 (M3) |
| `twinCoffer` | 9 (M3) |
| `campWaxKey` | 1 per camp |
| `trader` | 9 per entry (Later, SV-32) |

#### Keyed-check domains under `dungeon/floor/{N}/checks` (change SV-6)

`d = H(floorKey/checks, domain, parts…)`, `d20 = 1 + Bound(d, 20)`. Advantage and disadvantage use die 0 and die 1. Every index named below is part of the saved dynamic state.

| Domain | Key parts after the domain | Was | Owner | Wave |
|---|---|---|---|---|
| `h:object` | objectId, LabelId(checkKind), attemptIndex (TL's per-kind rules, settled), die | TL 6.22 | TL | M1 |
| `h:save` | objectId, unitId, springIndex, die | TL 6.22 | TL | M1 |
| `h:notice` | noticerId, targetId, noticeIndex (a per-noticer counter), die. **Out-of-combat noticing only** | ENM 12.2 "combat stream" | ENM | M1 (before the M1 goldens) |
| `h:steal` | unitId, stealIndex, die. Out-of-combat Steal only | ENM 12.2 | ENM | M1 |
| `h:lurkStealth` | groupId, positionIndex, band ∈ {placement, 6, 3}, die | EP 6.8.5 (no die; `mod 20`); ET4 | EP | **M2** (corrected after the check: EP's M1 staging row says "M1 has no lurkers", the Ghillie is not on EP 2234's M1 list, and ENM 12.2 dates EP-3's keyed lurker Stealth to M2). Registered in M1 as a reserved, inert domain, so the domain list is complete before the M1 goldens |
| `h:envSave` | hazardInstanceId, unitId, exposureIndex, die. **Out-of-combat only** (falls; M3 Plunge and Dare rooms) | HZ 929, 2663, 11.2 | HZ | M2 falls, M3 Plunge and Dare rooms (cross-document comparison 2026-11-23, CC-97) |
| `h:curiosity` | LabelId(curiosityId), LabelId(optionId), attemptIndex 0, die | EV 4.3, 4.10 (runSeed, floor, …) | EV | M2 |
| `h:windfallSteal` | windfallId, 0, die | LP-6 | LP | M3 |

In-combat noticing, Steal and hazard saves stay on `combat`. "Out of combat" means no fight is active when the roll is made, including a noticing roll that starts a fight. The approved TL attempt-index rules and ENM's "in the order the triggers occur" are kept, because the per-noticer `noticeIndex` preserves the order.

#### Display and simulator keys

| Key | Class | Notes |
|---|---|---|
| `display` | display (`DisplayKey`) | Domains: `itemPart` (IR: floor, instanceId), `containerArt` (LP `ContainerArtBlind`: floor, containerId), `glimpse` (EV DV14), `ink` (XI cosmetics), `lore` (ambient lines; change CH-8 ⚑, camp and hub design 4.16; M2) with two roots: **camp** `DisplayKey(run)` / `lore` / (nightId, campIndex, visitIndex, speaker); **hub** a profile-scoped root H(profileId, `lore`, nightIndex, visitIndex, speaker), created outside Core and never derived from a run key; and a **run-start** row for the Oddment under the camp root, `DisplayKey(run)` / `lore` / (campIndex 0, visitIndex 0, speaker "oddment", heroIndex, poolId), with **no `nightId`**, so two players of one code see the same Oddment (change SK-6 ⚑, starting kits design 4.5.2; M2). Core assemblies cannot reference `DisplayKey` (DS12) or the `lore` domain |
| `simRoot` | sim | Trial i: `Derive(simRoot,"trial",i)`; release run i: `Derive(simRoot,"release",cap,i)`; golden candidate i: `Derive(simRoot,"golden",i)`; generation fixture: `Derive(simRoot,"fixture",floor,partySize,cap,i)` (4.11); bootstrap resamples: `Derive(simRoot,"bootstrap",LabelId(reportId))` (6.4); each `runSeed = Draw(·,0) >> 4`. Launcher candidates: `Derive(simRoot,"launcher",profile,run,redraw)`. Oracle rollouts: `Derive(trialKey,"oracle",camp,rollout)` (CMP), which never read the real future |

#### Outside every stream

The launcher's OS-entropy candidates, profile memory, the UI annex, Retellings, Twice-Told and Afterword (4.6).

### 4.4 The canonical generation pipeline (change SV-9 ⚑; lead's call; M1)

#### Run start (R-SEED-2, settled)

| Stage | Work | Notes |
|---|---|---|
| GS0.0 | Launcher (personal runs only; outside the seed; R-SEED-6) | Candidate from OS entropy, masked to 60 bits. The forecast reads only `route`, the F1 boss's `boss9` and F1 `curiosities` index 0, **without generating a floor**. Order: theme rule, Fallow, F1-boss rule, onboarding; ≤ 8 redraws |
| GS0.1 | Hero creation (theme-blind) | Its inputs (class, species, kit) enter F1's `GenInputs`: F1 loot reads the hero (the F1 Due, filter-first `onParty`) (after the check). The kit is the default kit for the creation pick; a kit variant travels in `boons[]` and `configHash` (SV-12; starting kits design 4.7). The creation card's Oddment is selected from the run key the launcher has already drawn (or the entered code), display-only, and frozen into `run.save`'s display section at WO1 (change SK-6 ⚑, starting kits design 4.5.2; M2) |
| GS0.2 | Snapshots fixed: configuration (with profile grants, SV-12), unlock, **`drawableSet`** (computed from the unlock snapshot and stored in the run save; answers RS 13), the key | — |
| GS0.3 | Route keyed sorts; the walk for F1 and F2 (no RNG) | `RouteDrawn` |
| GS0.4 | Phial pre-roll | `loot/run` |
| GS0.5 | F1 = GS1–GS17, then WO1 (4.8) | — |

#### Floor N

F1 is generated at GS0.5. **F(N+1) is generated from the camp-exit state, as approved** (LE: "party snapshot at generation (camp exit)"; FL 6.1: "Generation runs at camp step 11"; ITEMS 10.4; LE-14's atomic camp-exit commit). Corrected after the check (lead's call, after the check): the draft froze `GenInputs` at camp entry, which would have built F(N+1) for a stale party, because Muster recruits, deaths, purchases, training and owned bases all change during camp.
- **Authoritative generation.** `GenInputs` for F(N+1) is frozen at **Travel on (camp step 11)** from the camp-exit state (GS1), and the floor is committed atomically with the camp-exit save (WO5).
- **Speculation, allowed only as a speed-up.** From camp entry, a background job may generate F(N+1) from a *candidate* `GenInputs` built from the current camp state. At Travel on, the authoritative `GenInputs` digest is computed. **If it equals the digest the speculation used, the speculative floor is committed; otherwise it is discarded** and F(N+1) is generated synchronously behind a short loading beat, which fits the 1 s worst case (5.8). A mismatch is not an error.
  - **Restart on change (reading; change CH-19 ⚑, camp and hub design 4.4, 7.5; M2).** Core recomputes the candidate `GenInputs` digest after each committed camp command and each non-WO8 change entering `GenInputs` (the door choice, the Perilous toggle (M3), a hire or dismissal); a job on a stale digest is cancelled or discarded and a new one starts after a debounce (0.5 s *(illustr.)*). A speculative result, an unravel (5.5) included, stays in Core memory only, never exposed to the display or to lore, until Travel on on a digest match; the "fire gutters" dressing shows only when a synchronous rebuild exceeds 300 ms. `SpeculationEquivalence` gains an unravel case.
- A floor's output never depends on when a background job started. V19t `SpeculationEquivalence` proves it: for the same camp-exit state, the speculative and synchronous paths give byte-identical `FloorStatic`.

| Stage | Work | Streams | Draw-free? |
|---|---|---|---|
| GS1 | Freeze `GenInputs` (4.8.2) and digest it: F1 at GS0.5; F(N+1) at Travel on from the camp-exit state (a speculative run uses a candidate and is kept only if the digests match) | — | yes |
| | **Attempt loop, a = 0; the next attempt on LR3 or on the defect path (5.5)** | | |
| GS2 | Layout LS1–LS13, with the HE-7 reservation at LS11 (3 draws) and the LS12 hearing repair (LR1). Dressing is display side, not a Core stage (4.3) | `layout` | — |
| GS3 | EventDraw (computed once per floor; reused on any retry) | `curiosities` | — |
| GS4 | Arena stamp geometry (BR 4.1.3 steps 1–3); reads the arenaCell and `boss9` indices 0–1 (Rendition, mirror; 0-based, corrected after the check). **Conditional: pending the BR and ROOM owners' confirmation of SV-9 (request); until both confirm, the 14.2 fallback holds and the stamp runs after GS6 and overwrites** (cross-document comparison 2026-11-23, CC-68) | — | yes, proven offline (V17s) |
| GS5 | Terrain: reservation (with `hzTagMax`), then per node; arena node dummies (conditional on the GS4 stamp, CC-68) | `terrain` | — |
| GS6 | Hazards, after **all** terrain | `terrain/hazards` | — |
| GS7 | Traps, TL pass 1 | `traps` | — |
| **AP1** | Structural assertion point (5.4) | — | — |
| GS8 | ENM step 2 (slot plan), then EP 2a (Mousetrap bait cache); Later: trader | `encounters/plan`; Later `trader` | — |
| GS9 | SocketPlan (LP); reads steps 2 and 2a only | — | yes |
| GS10 | LootPlan step 3a in IR 4.17's frozen order (signature form; Due, arms floor, fit premiums, caches, boss-card options, hero redraw, Viaticum, Wager, Twin Coffer); the Windfall at the start of step 8 | `lootPlan/*` | — |
| GS11 | Loot step 3, blocks in approved order | `loot/floor/{N}/*` | — |
| GS12 | ENM steps 4, 5, 5b, 6–8, 9, 9b, 10a–10g, 11; the boss escort as the arena slot (BR step 4); lurker Stealth by keyed hash | `encounters/place` | — |
| GS13 | trapsResolve, TL pass 2, with TL's failure actions | `trapsResolve` | — |
| GS14 | LootBind (LP's numbered order) | — | yes |
| GS15 | EventPlace | — | yes |
| **AP2** | Final assertion point, act then assert (5.4) | — | — |
| GS16 | Recruit lists for camp N | `recruit/camp/{N}/…` | — |
| GS17 | Stage digests, V19g DrawLedger close, `FloorStatic` written, commit (WO1 or WO5) | — | yes |

Card A (`recruit/camp/{k}/gap`) is drawn at camp k entry. It is keyed, so it has no pipeline position.

**Order decisions this table settles** (each reconciles approved fragments; SQ14):
- **EventDraw** runs after layout and before terrain. That satisfies MAIN 6.3's "after layout, before hazards" and EV's "right after layout". It reads nothing positional; one written place stops drift.
- **Hazards** run after **all** terrain, because hazard sockets live in terrain layers. That satisfies HZ's "after the reservation step".
- **SocketPlan** runs after 2a. EP's 10b activity props are read by LootBind, not SocketPlan.
- **The arena stamp** runs at GS4. The arena node's terrain and hazard slots are spent as dummies, so counts stay fixed and the interior has one source (request: BR and ROOM confirm; **pending**: until both confirm, this order is conditional and the 14.2 fallback applies; cross-document comparison 2026-11-23, CC-68). BR's stamp step 5 ("run V9, V10, V13g, then V17g") splits into `V17g.StampProven` at AP1 and the rest of V17g at AP2.
- **Windfall** draws are "at the start of step 8 inside 3a" (LP 4.7.10, LE). LP 4.2's "SocketPlan … apart from the Windfall" is a text fix; the stream is keyed, so values do not differ.
- **BR 4.17 (hook):** the stamp is stored in `FloorStatic` in **template-local coordinates plus its transform**, and BR's event order is a V17t replay row.

### 4.5 Draw-stability rules (DS1–DS15; changes SV-1 to SV-5; lead's call; DS15 and the DS3, DS11 and DS14 amendments are lead's calls, after the check)

| Rule | Statement |
|---|---|
| DS1 | A child key is a function of the parent key, its label and its indices only |
| DS2 | Every counter stream has a registered class and count. Each stage ends in a V19g ledger assertion |
| DS3 | No draw count depends on draw results, except a per-entry unit count and a `leaf-formula` stream's count over already-recorded generation choices (4.2). The parent of a per-entry family takes no draws. V8 `DrawCount` and the DrawLedger evaluate a leaf formula on the choice recorded in `GenerationLog` |
| DS4 | Lists are sorted by ordinal comparison of ASCII stable ids. Every sort has a total key; a keyed sort breaks equal H values by ordinal id (K-rule 7). No unordered iteration |
| DS5 | Whole-catalogue orders use keyed sorts (SV-8) |
| DS6 | A new consumer gets a new key. Changing an existing stream's use needs a `genVersion` bump and a golden rebaseline |
| DS7 | **A new keyed consumer moves no other draw** (told plainly: outputs downstream of it, in the same floor and in later floors through cross-floor state, may change). Reservations that already exist (`lineRoll`, `curseGate`, `windfall`, curiosities draws 4–11) are kept as declarations; they are no longer what guarantees stability |
| DS8 | Retries derive under the failing stage (`retry1`) or the floor (`attempt/{a}`), never by arithmetic |
| DS9 | Host-blind streams (`curiosities`, `checks`, `trader`, every loot and recruit key) are attempt-invariant. `dressing` is attempt-scoped |
| DS10 | Generation reads only `GenInputs` (4.8.2). Never the profile, the clock, the UI annex or the culture. Cross-floor state reads only generation outputs and the party snapshot at generation (4.8.2; the DCSS lesson, section 1) |
| DS11 | Core analyzer bans: `float`, `double`, `decimal`; `System.Random`; `DateTime.Now`/`UtcNow`; `Environment.TickCount`; `Guid.NewGuid`; `GetHashCode` and `HashCode`; culture-sensitive comparison or `ToUpper`/`ToLower`; `Dictionary`/`HashSet` enumeration; sorts without a registered total comparer; `Parallel.*` inside one generation; any reference to GodotSharp from Core. `/` and `%` with a possibly negative operand use `FloorDiv`/`FloorMod`. `CheckForOverflowUnderflow` is on in debug and CI, **except** inside the key-algebra file's explicit `unchecked { }` blocks, the only place the analyzer allows wrapping arithmetic (4.1) |
| DS12 | `DisplayKey` is a distinct type that Core cannot reference (compile-time) |
| DS13 | Content numbers read by generation or rules are integers or rationals. The Core loader (`System.Text.Json` into integer types, never Godot JSON) rejects non-integer values in those fields. A rational's canonical form is a reduced `p/q` with q > 0 |
| DS14 | Generation reads only its frozen `GenInputs` and no mutable shared state. A background job freezes a **candidate** `GenInputs` before it starts; **its floor is valid only if the candidate digest equals the authoritative digest frozen at GS1** (Travel on for F(N+1)). A mismatch is not an error: the speculative floor is discarded and generation runs synchronously (4.4) |
| DS15 | Every stage run opens fresh stream instances at counter 0, and the DrawLedger closes per stage run, so a rerun (LR2, LR3, an AP2 act's span) reproduces the same draws from the same key. `vector` keys are read by absolute index and have no counter. Draw indices are 0-based everywhere |

**Locks before the M1 goldens:** the SV-1 constants; the 4.2 path grammar; `CanonicalWriter` and the digest function (below); maxSockets 16; maxSocketsPerRank 12; floorCap and maxSlots; EP K = 4; the ambush cap; `hzTagMax` (request to HZ); TL's traps and trapsResolve formulas (request); IR filter-first (IR 4.1, settled); the DH seed reshuffle (approved); SV-5 and SV-8 applied by their owners, or their fallbacks adopted (14.2).

**The canonical byte serialisation and the digest function (lead's call, after the check).** One canonical binary writer, `CanonicalWriter`, is used for every digest (stage digests, the `GenInputs` digest, the static-file content hash, replay core-state hashes) and for journal deltas. It writes little-endian fixed-width integers, fields in a declared schema ordinal (never reflection order), collections in their DS4 sorted order with a length prefix, and an explicit tag for an absent or optional field. Every digest is SHA-256 over those bytes, stored in full and shown truncated to 128 bits. A vector for one small `FloorStatic` is pinned in `KeyAlgebraVectors` when the M1 schema locks. Without this, a refactor that reordered fields would "change generation" and force rebaselines with no real output change.

**Locks before the M2 goldens:** the curiosities count 12; `candidateListLength` 8; `candidateDrawMax` 32; HZ's hazards formula.

### 4.6 Outside the seed; the reproducibility key (changes SV-12, SV-13; lead's call)

**The reproducibility key (change SV-12 ⚑, contests R-SEED-5's tuple):** `(runSeed, genVersion, manifestHash, configHash, unlockHash)`.

- **`genVersion`** is an integer code constant, bumped whenever generator output changes for any key. CI fails any digest change on a golden or release fixture that has no bump.
- **`manifestHash`** is SHA-256 over **canonical rules-relevant content**: UTF-8 NFC, sorted keys, LF line endings, invariant integer formatting, comments stripped. Fields the schema tags `display` (text, art references, barks) are excluded and hashed separately as **`displayHash`**, which is outside the key. **A text or art edit therefore never moves a seed.**
- **Where the hashes are computed (lead's call, after the check).** `manifestHash`, `displayHash` and `genContentHash` are computed **in the build pipeline and embedded in the build**, never at run time, because NFC normalisation goes through ICU or NLS and behaves differently (or throws) with invariant globalisation on. In addition, every rules-relevant string field is ASCII (already true for ids, K-rule 3), so NFC is a no-op there, and the Core loader rejects non-ASCII in those fields. Rationals hash in their canonical `p/q` form (DS13).
- **`genContentHash`** is the subset of the manifest that generation reads. It is used for the build tag and shared-seed comparability (M2; brought forward from Model A's Later).
- `cutState` and `shipStatus` live in the manifest. BR 4.10.1's "content version … logged in the key" reads "inside `manifestHash`".
- **`configHash`** covers RS 11.1's configuration snapshot **plus the profile grants that change rules: `difficulty.tier`, `boons[]` and `nightmareTier`** (change SV-12; field reserved in M1, live in M3), **plus `runModifierId`** (the CH-24 run-modifier slot; field reserved in the snapshot schema at M1, unlit until Oaths ship, Later; change HL-22; heirlooms and curses design 9.1; guarded by V22t (m) `RunModifierIdInConfigHash` beside V19t `ConfigHashCoversRunConfig`), sorted canonically. The two tier fields are distinct (MAIN 4.7, decided: "kind, not stats"): **`difficulty.tier` drives the ladder%**; **`nightmareTier` (M3) drives the kind additions only and never changes the ladder%** (RS 11.1; cross-document comparison 2026-11-23, CC-84). Shared and daily runs use canonical values. Two checks guard it (replacing the draft's `KeyCoversProfileGrants`, which could only pass vacuously, because Core never reads the profile; lead's call, after the check): V19s **`CoreNoProfileRef`**, an analyzer rule that the Core assembly cannot reference Profile types; and V19t **`ConfigHashCoversRunConfig`**, a reflection test that every field of the configuration-snapshot type is written by the `configHash` canonical serialiser, plus a test that changing each field changes `configHash`.
  - **The Bobbin charm boon (request CA-20 from the charm abilities design 4.11; pending, the SV owner rules; M3).** Requested: the chosen charm family id, the receiving unit and the slot enter `configHash` as the boon choice in `boons[]`, as an heirloom template id does (the instance carries no rolled line, so nothing else enters the key); the launcher validates the family id against the run's `drawableSet` before WO1; the boon is off on shared, seeded and daily runs. Fallback (14.2).
  - **Charm forced-loadout sessions (request CA-18 from the charm abilities design 10; pending, the SV owner rules; M2).** Requested: the loadout assigned in a CG2 forced-loadout human session is a `configHash` field of the session's runs (6.2).
- **`unlockHash`** is as approved. The Sequel "seen" set it includes serialises as sorted ids.
  - **Charm-family unlocks (request CA-21 from the charm abilities design 4.10; pending, the SV owner rules; M3).** Requested: charm families enter as unlocks under MAIN 4.7 bullet 1 (two base families at M3, Hangnail and Jolt Knot); unlike heirloom unlocks, they change a generation pool, so they are `unlockHash` and `drawableSet` inputs, and the V5 every-subset cost note (SQ41) applies. They use the Library's generic loot-unlock object (CH 4.11), so V20s `UnlockHasHubObject` passes (the CH owner confirms the reading). Fallback (14.2).
  - **Heirloom unlocks (request HL-11 from the heirlooms and curses design 4.1.7; pending, the SV owner rules; M3).** Recommended: heirloom unlocks stay outside `unlockHash` (they change no generation pool; the template id travels in `boons[]`, inside `configHash`, and template content in `manifestHash`); a balance swap maps `supersededBy` and migrates the unlock and its Grip-marks, deleting nothing; the profile record carries `heirloomsSchemaVersion`. Fallback if refused (14.2).
- **The generation identity is per floor: (the reproducibility key, that floor's `GenInputs` digest)** (corrected after the check; lead's call, after the check). The key alone fixes the route and every layout graph (FL LS5 is party-agnostic); **F1's `GenInputs` includes the hero-creation inputs**, because F1's loot reads the hero (the F1 Due is a U weapon for the hero's arms, LE; IR's filter-first `onParty` reads the party snapshot on every floor). From F2, `GenInputs` carries the whole party snapshot. A replay's identity is the key plus `rulesVersion` (4.7); hero class and species are player inputs, recorded in the replay header. Golden F1 fixtures pin the hero inputs (4.11).

**Outside the seed and the key (register; read, not changed, unless marked):**

| Item | Rule | Guard |
|---|---|---|
| Launcher (R-SEED-6; Fallow EV-20; F1-boss rule BR-25; onboarding) | OS entropy, ≤ 8 redraws; bypassed for shared, daily and simulator runs. `LauncherCheckMs` ≤ 5 ms p95 per candidate *(illustr.)*, a REP row until measured | `LauncherLootNeutral`, `Floor1ThemeShare`, `F1PoolParity`, `LauncherRedrawRate`, EV's `LauncherBypass` (approved), new `LauncherF1Neutral` (6.10) |
| Client values (launcher-only) | `launcher.f1BossMemory` 2, `curiosity.fallowK` 3, `fallowFloors` [1]. `route.streakRedrawMax` 8 is **not** a client value: it is *(illustr.)* and launcher-only (RS 11.1; cross-document comparison 2026-11-23, CC-129) | — |
| Profile memory | `curiosityMemory`, `recentRuns[].f1BossId`, `recentRuns[].f1ThemeId` (written at run start for every launched personal run, outside the reproducibility key; read by R-SEED-6 for the last three personal runs; cross-document comparison 2026-11-23, CC-6), `runHistory[]` (4.7), `uiMemory`, `bossMemory`, codex, Deeds and Veteran, `loreMemory` (display only; checksum, a bad checksum reads empty; change CH-7 ⚑ with the main design owner, camp and hub design 9.1; M2), **Grip-marks** (`gripMarks{}`: Deed bynames and boss and theme marks only, never a given name or a count; display memory, outside `manifestHash` and `configHash`; written at WO6 for personal runs only; change HL-5 ⚑; heirlooms and curses design 4.1.5; M3). Never read by generation (DS10) | `ProfileBlind`, V19s `CoreNoProfileRef`, V19t `SharedSeedProfileBlind`; V22 (h), V22t (l) `GripMarksBynameOnly` (M3) |
| Display content | Retellings, Twice-Told, Afterword, Grudge barks, Glimpse rotation, the UI annex (Waymarks, dismissed loose ends), the `display` key | `RetellingParity`, `DisplayMemoryInert`, `WaymarksOutsideKey` |
| Ambient lines (camp and hub; change CH-8 ⚑, camp and hub design 4.16; M2) | The `lore` display domain with its camp and hub roots (4.3); outside `manifestHash`, in `displayHash`; selection reads the run-start `loreMemory` snapshot stored in `run.save` and the run's `loreRunState`, never the live profile | `DisplayMemoryInert` (covers `loreMemory`; V20t `LoreInert`), V20t `AmbientProfileBlind` (both roots); V19 `Conformance` lists the domain (`DomainDisjoint`, registry completeness) |
| Starting-kit display (changes SK-5 ⚑, SK-6 ⚑; starting kits design 4.5, 4.7; M2) | Given names, kit lines, Oddments and kit-strip text are display-tagged: outside `manifestHash`, their content in `displayHash`; the per-run sidecars (`displayOverlay`, `namedPieceLog`, the frozen Oddment) sit in `run.save`'s display section (4.8.1). Seeded runs read `loreMemory` and write nothing; replays read the Oddment from the save and never recompute it | V21s (h) `KitDisplayCoreBlind`, `KitDisplayInert`; V21t `SeededRunOddmentReadOnly`, `ReplayOddmentMatchesSave`, `OddmentStableOnReload` |
| **Display interactions (lead's call)** | Dismissing a card, a Retelling or a Waymark is **UI-layer only**: no tick, no command-log entry, no slot sequence number. Display-layer types cannot construct core commands (V19s). Otherwise profile memory could move patrols | V19t `SharedSeedProfileBlind`, run as **the same player-intent script under two profiles** with identical rules logs required |
| Simulator | Writes **no player profile**. Launcher-dependent arms write sandbox profiles in LauncherEmu only (change SV-13 ⚑ to MAIN 6.6 and EV 9.1; 6.1) | — |

### 4.7 Shared seeds, daily seeds and replays (change SV-17; lead's call)

**Shared seeds (M2):**
- Entered as a code (4.9).
- The launcher is bypassed.
- The canonical unlock set and canonical profile grants are used.
- The configuration comes from the code's suffix.
- Profile writes are limited to `shownCount` and `fullShown` (approved), plus the marked run-history entry (Q1).
- `SaveHeader.seedMode` records `shared` (9.2).
- **Before the run starts** (lead's call, after the check), the "Seeded run" entry screen says in one line that seeded runs use all content and standard difficulty and do not count toward unlocks, records or the codex (words in the naming pass). From M3, the profile grants stay canonical for seeded runs (4.6), and the same line says so.
- **Told plainly (corrected after the check):** the same code gives the same route, the same doors, the same layout graph on every floor and the same rolls for the same actions. **Loot depends on the hero from F1** (the F1 Due, filter-first `onParty`), so the same code with a different hero class gives a different F1 loot plan; and **from F2 generation reads the whole party snapshot** (size classes, filter-first loot, recruit fit), so one different pickup or recruit makes every later floor's contents different. The earlier statement, "the same floor 1; from floor 2 loot depends on the party", drew the boundary one floor too late (14.3).
- **Progress (agreed, Q1).** A seeded run appears in run history marked seeded and advances **no unlocks, records, codex entries or launcher memory** (`recentRuns`, `f1BossId`, the Fallow set, `bossMemory`). It writes only the approved seen counts, `shownCount` and `fullShown` (EV-19, as approved). Rejected: Q1 B (unlock content but set no records); Q1 C (count exactly like normal runs).
- **Where the seeded mark lives (lead's call, after the check).** The draft put the mark in EV's `seedMode` field, but that field exists only inside `curiosityMemory.recentRuns`, which a seeded run must never advance (EV 9.1 line 1444; MAIN 6.6). Run history therefore gets its own home: a profile **`runHistory[]`** record per finished run (personal or seeded), carrying `seedMode` copied from `SaveHeader.seedMode`, the result, the floor reached and the code. **`recentRuns` is never written for a seeded run.** This adds a profile field and a write that MAIN 6.6 and EV 9.1 describe as "`shownCount` and `fullShown` only", so it is a flagged change covered by Q1 (SV-16 ⚑), not a reading.
- **Scope (new lead's call).** "Seeded" covers every run whose seed was not drawn by the launcher, including a player's own earlier code typed again. Daily runs inherit the Q1 rule when they ship, unless the client rules otherwise with the daily-mode item (14.2). Replay and simulator runs write no player profile at all (SV-13).
- **Writes (new lead's call).** For a seeded run, WO1 and WO6 carry a `pendingProfile` limited to the marked `runHistory[]` entry and the seen counts. V19t **`SeededRunWritesSeenOnly`** (M2) plays a golden shared run to death and to victory under a fresh sandbox profile and asserts that only those fields changed. **Seeded runs, replays and the simulator write nothing to `loreMemory`**: their `loreSeen` marks and `loreRunState` are discarded at run end, and the hub after a seeded run writes nothing (the permitted seen counts stay `curiosityMemory.shownCount` and `fullShown` only; V19t unchanged; V20t `SeededRunLoreReadOnly`; change CH-9 ⚑, camp and hub design 4.16).
- **Shared seeds and kit variant boons (request SK-16 from the starting kits design 4.7; pending, the SV owner rules; M3).** Undecided. Recommended (and the fallback until ruled): on shared seeds, variant boons are replaced by the default kit for the chosen creation pick, and other boons follow the canonical-grant rule above; the alternative is a share code that carries `boons[]` and lends the variant. The seeded entry screen states the result either way (14.2).
- **Share codes and heirlooms (request HL-25 from the heirlooms and curses design 4.1.7; pending, the SV owner rules; Later).** Until ruled, shared and seeded runs use canonical grants, so no heirloom (SK-16's fallback). The request: Later share codes may carry one heirloom template id and one `runModifierId`, never Grip-marks. Fallback: share codes carry neither (14.2).

**Daily seeds (Later; RS 12 "already supported", LE 6.12):**
- **Told plainly:** a public formula such as `Derive(…"daily", date)` lets anyone compute tomorrow's seed and pre-play it as a shared seed. The daily seed must therefore come from a published or server value; otherwise the mode is told plainly as "local practice".
- Compatibility is checked on the full (`genVersion`, `genContentHash`, configuration), never on a major version number.
- A fixed hero class, species and configuration; one attempt per profile per UTC day (a local soft lock); profile memory read-only; the launcher bypassed.
- **Future item for the client:** whether a daily mode should generate loot and recruits against a fixed reference party snapshot, so that runs stay comparable after floor 1 (14.2).

**Replays (M1):**
- Header: the key, `rulesVersion`, `saveVersion`, hero-creation inputs.
- Body: the command log.
- Rolling core-state hashes every 64 commands *(illustr.)*, so a divergence can be located.
- Valid only when key and `rulesVersion` match; otherwise "not replayable on this build" (MAIN 6.6 already allows this).
- Bug reports attach the key, log, slot and `GenInputs`.

**TL's builders' option to salt `checks` for daily or shared seeds is withdrawn (change SV-7):** one key must give one run (R-SEED-5, V6, IP9).

### 4.8 Saves (change SV-14 ⚑; lead's call; agreed, Q4 for the slot mechanism; M1 run save and log, M2 the rest)

#### 4.8.1 Files

| File | Holds | Written |
|---|---|---|
| `run.save` | The safe-point snapshot (the authority, MAIN 6.6): key, `runId`, `seq`, the static-file manifest (each floor's file name and hash), route state (RS 11.1), per generated floor its `GenInputs`, stage digests, attempt used and `GenerationLog`, dynamic state (FL, TR, HZ, TL, LE, LP, EV, BR, CMP rows), `PartyKnowledge` and the XI 4.1.3 event order, the combat counter, and `pendingProfile[]` (with a display-tagged `loreSeen` kind), and, display-tagged and outside the key, the run-start `loreMemory` snapshot and the `loreRunState` record (every run kind; change CH-9 ⚑, camp and hub design 4.16; M2), and the starting-kit display sidecars `displayOverlay[instanceId]`, `namedPieceLog[]` and the Oddment frozen at WO1 (no field on the Core `ItemInstance`; changes SK-5 ⚑, SK-6 ⚑, starting kits design 4.5.1, 4.5.2; M2), and the charm per-fight state: `charmKept[(unit, prop)]`, the receiver-keyed caught flags, each catch's armed or missed state and the per-(unit, property) policy choice, reset only by the fight-resources reset event (BR 4.2.5), with a `saveVersion` bump (change CA-11; charm abilities design 9.1; M2 simulator-only for the shadow records, M3 live) | temp file, flush to disk, atomic replace |
| `floor-N-<hash>.static` | **New:** the generated static floor (GS17 output), immutable, named by content hash. Unreferenced files are garbage-collected | inside the commit, before `run.save` |
| The suspend slot: `suspend.base` + `suspend.journal` (two files after the check, 4.8.4) | On a floor only: `suspend.base` holds {`runId`, floor, `baseSeq`, the floor-entry or compacted snapshot of dynamic state, `PartyKnowledge` and the combat counter, CRC}; `suspend.journal` holds the state-delta records (agreed, Q4) | base: temp file, flush, atomic replace; journal: append with a flush per record, after every committed command (XI-28, agreed) |
| `run.annex` | The UI annex (XI-7) | display writes |
| `run.log` | The command log | append |
| `profile.save` | The profile (MAIN 6.6) | as approved, plus WO1/WO6 replays, plus the hub lore write (4.8.3; change CH-9 ⚑) |

**Contest (flagged; FL 11.1 line 2258, verbatim: "Save: the seed and configuration regenerate the static part, checked against layoutDigest"; told plainly):**
- Regenerating a floor needs every generation input: the party snapshot at generation, `mousetrapsThisRun`, `windfallCarry`, `LootLedger.generated[]`, EventDraw history, the committed theme and boss, the attempt used.
- It also breaks the moment a patch changes the generator.
- Storing `FloorStatic` makes the snapshot really "the full core state" (MAIN 6.6) and lets runs survive patches (agreed, Q3).
- Regeneration plus a digest comparison stays as V19t `SaveDigestMatch` and a debug-build check on load.
- Size: about 73 KB per floor, about 430 KB for 5 floors *(illustr.)*.

#### 4.8.2 `GenInputs` (DS10)

Contents:
- the key and N, and the `genVersion`, `manifestHash` and `genContentHash` it is generated under;
- the committed theme and boss record and the `boss9` outputs;
- for F1, the hero-creation inputs (class, species, kit) (after the check);
- the party snapshot at generation, **taken at camp exit** for F(N+1) (LE, as approved): lanes, usability, levels, training, owned bases, band, `partyCountAtGeneration`;
- `LootLedger.generated[]`, once-per-run and Scion-line memory;
- EventDraw prior draws;
- `mousetrapsThisRun`, `windfallCarry`;
- references to the configuration, unlock and `drawableSet` snapshots.

It is frozen and digested at GS1 (F1 at GS0.5; F(N+1) at Travel on). The commit asserts that the floor was built from that digest; a speculative floor built from a different candidate digest is discarded, not an error (DS14).

#### 4.8.3 Write orders (WO1–WO8) and the outcome rule

- **The outcome rule (lead's call; agreed, Q4; extends XI-28's guarantee).** **No frame whose content depends on a draw, a reveal, a Down, a death, a spent counter or a paid act is presented until that command's durable write has landed.** It covers floor commands (WO2) and, after the check, camp commands (WO8).
  - Draw-free wind-up (a swing starting, a step whose landing reveals nothing new) may overlap the write.
  - Previews (the move preview's noticing tag, `PreviewExactness`) show odds only, never a draw value.
  - **Only the command's own journal record gates the outcome frame** (lead's call, after the check). Compaction (4.8.4) runs after the frame and is crash-safe, because the old base plus its journal stays valid until the atomic replace.
  - **Over budget (lead's call, after the check).** If the gating write is slower than the overlap budget (an HDD flush stall, an antivirus scan), the last wind-up frame is held; no spinner appears below 300 ms *(illustr.)*. Every wait is logged as `OutcomeWaitMs` (REP; p99 ≤ 250 ms on the minimum-spec HDD *(illustr.)*, 5.8), and an M2 FELT session on the minimum-spec machine checks felt lag (6.11).
  - V19t `ProbeResistance` kills the process at the first outcome-bearing frame of every command type, **floor and camp**, on the goldens, reloads, and asserts the outcome is in the restored state.
- **Every file carries a monotone `seq` and the run's `runId`** (a 128-bit id drawn at run start outside the seed; lead's call, after the check). `seq` restarts at 1 for each run, so a file left behind by an earlier run (an interrupted removal or abandon) could otherwise outrank the new run's files. Load chooses the current `run.save`, then compares `seq` only among files whose `runId` matches it; files with another `runId` are garbage and are deleted. It then verifies the static hashes against the manifest in `run.save` and proceeds.

| Order | Event | Sequence |
|---|---|---|
| WO1 | Run start | GS0, F1 generation, `floor-1-<hash>.static`, `run.save` (seq 1) with `pendingProfile` = {for a **personal run only**, the `recentRuns` entry with `f1BossId`; for a seeded run nothing beyond the seen counts (4.7)}, then the profile write (idempotent by `runId`). A run is written before the profile remembers it |
| WO2 | On a floor | Each committed command appends its journal record (seq+1) to `suspend.journal` and flushes it before any outcome frame (4.8.4) |
| WO3 | Curiosity resolve | EV 4.10 step 5, "write the run snapshot", **is** that command's slot write while on a floor (reading of MAIN 6.6, "the snapshot is the one suspend slot"); step 6 writes the profile `last[id]`, idempotent (V16t `CrashBetweenWrites`) |
| WO4 | Floor exit to camp | `run.save` (seq+1), then the slot files are removed. After a crash in between, the highest `seq` of the matching `runId` wins. **The arrival write** carries get-up and **only the first pending catch-up offer per unit**, or none (reading; change CH-2 ⚑, camp and hub design 4.2 A.1, 4.4; M2) |
| WO5 | Travel on | `GenInputs` for F(N+1) frozen from the camp-exit state; the speculative floor kept if its digest matches, otherwise F(N+1) generated now (4.4, DS14); `floor-(N+1)-<hash>.static`, then `run.save` = camp-exit + `GenInputs` + digests (seq+1). **This single commit is F(N+1)'s own first safe point** (LE-14). The first slot write follows the first command |
| WO6 | Death, victory or unravel | Slot journal record of the result, then `run.save` with `pendingProfile` = {the `runHistory[]` entry; for a personal run also the run record, its progress writes and **the completion of the `recentRuns` entry pushed at WO1** (EV 9.1 `PendingProfileWrite`; an abandoned run completes it the same way; cross-document comparison 2026-11-23, CC-119)}, then the profile write, then the run files are removed. A crash in between finalises on load. **Pending request HL-6 (heirlooms and curses design 4.1.2, 4.1.5; the SV owner rules; M3):** for a personal run, a non-Core profile-progress step after the run's last Core event adds the `pendingProfile` kinds `bequestOffer` (at most one per run) and `gripMark`, by the heirlooms and curses design's run-end table; the profile keeps a pending queue, one entry per family, never overwritten; nothing is shown on the death path (fallback 14.2) |
| WO7 | Load | Read; validate (matching `runId`, contiguous journal, 4.8.4); flush `pendingProfile`; then **if the authoritative state is on a floor, write the re-validated state as a new `suspend.base` (seq+1) by atomic replace and truncate the journal to it; if it is at camp, rewrite `run.save` (seq+1) by atomic replace and write no slot** (MAIN 6.6, XI-28: no suspend slot exists outside a floor; corrected after the check). Atomic replace uses the platform shim (4.8.4). This meets "deleted on load" in substance: the loaded file no longer exists, and **no crash window leaves the run without a valid slot on a floor, or without `run.save` at camp** (told plainly; reading of XI-28 and XI Q1; agreed, Q4: load replaces the slot atomically instead of delete-then-write). Then accept input. Abandoning a run also flushes `pendingProfile` |
| WO8 | Camp command (after the check) | Each committed camp command that draws, spends or reveals (an arm, gap or Eye reroll, a purchase, recruiting, training) rewrites `run.save` (seq+1) by temp file, flush and atomic replace **before its outcome frame**. Without it, a camp reroll could be seen, the game killed, and the reroll reloaded unspent. If write cost bites, a camp journal shaped like the slot's (4.8.4) may replace the rewrite, with the same guarantee. **The camp WO8 writes (changes CH-2 ⚑, CH-3 ⚑, CH-18 ⚑; camp and hub design 4.4, 7.3; M2):** exactly the commands that draw, spend or reveal: each catch-up pick (inside the same write, Core resolves that level and draws the next pending level's offer from `levelup/camp/{k}/unit/{unitId}/level/{L}`, 4.3); the system command **`CampRest`**, issued by Core when no catch-up pick is pending, logged in `run.log` with its seq, with its own write before the rest-summary frame (a reload re-enters catch-up or re-issues `CampRest` deterministically); Buy and **`BuyBatch`** (one compound command with a Core-computed line list, rejected whole on `InsufficientCoin`, one write); Sell; hire, dismiss (with the top-up re-check), swap, undo; arm, gap and Eye rerolls; `ChooseReward` and the shop commit; the Tithe Bowl purchase (M3). **"Training" reads as level and skill picks.** Not WO8: equips, hand-overs, stance, spell and card swaps, Rethink, re-picks, the door choice, the Perilous toggle; they ride the next write |

**Lore marks and the hub lore write (change CH-9 ⚑; camp and hub design 4.4, 4.16; M2).** Camp seen marks (a `loreSeen` kind in `pendingProfile[]`) and `loreRunState` are display-tagged, ride each `run.save` rewrite and create no command, tick, seq or WO8 frame; they are flushed to `profile.save` for personal runs only, where SV already flushes (WO6, WO7, abandon), and discarded for seeded runs, replays and the simulator. **The hub lore write:** on leaving the hub (Begin, Continue, "Abandon and begin", quit; a hub skipped by Begin again has nothing to write), `profile.save` is rewritten by atomic replace with the hub's lore marks, idempotent by (nightIndex, visitIndex), personal Nights only (V20t `HubLoreWriteIdempotent`). No write on quitting at camp is added. **The Oddment mark (change SK-6 ⚑; starting kits design 4.5.2; M2):** a `loreSeen`-kind entry queued at the first camp-1 entry (WO4), or at WO6 if the run ends before camp 1, **never at WO1**, and flushed to `loreMemory.oddmentsSeen[]` only by these existing flushes, personal runs only; `AbandonWritesNoLore` gains "start, quit, reload, abandon on F1 → `oddmentsSeen` unchanged".

#### 4.8.4 The suspend-slot mechanism (agreed, Q4)

- **The floor-entry snapshot plus a journal of state deltas**, one record per committed command (a diff of core state written by `CanonicalWriter`, about 200 B *(illustr.)*).
- **Two files (lead's call, after the check):**
  - `suspend.base` = {`runId`, floor, `baseSeq`, `saveVersion`, snapshot, CRC}. Written **only** by temp file, flush and atomic replace.
  - `suspend.journal` = a header naming `runId` and `baseSeq`, then records {`runId`, `baseSeq`, `seq`, `saveVersion`, CRC, delta}. Append-only, with a flush per record.
  - **Compaction order:** write the new `suspend.base` (atomic replace), then atomically replace (or truncate) the journal with a fresh header naming the new `baseSeq`. A crash between the two leaves the new base with a journal whose `baseSeq` does not match it; load then ignores those records, which the new base already contains.
  - **Load** applies only journal records whose `runId` and `baseSeq` match the base and whose `seq` values are contiguous from `baseSeq` + 1; the journal is truncated at the first bad CRC or gap.
- **Charm state (change CA-11; charm abilities design 9.1; M2 simulator-only, M3 live).** The charm per-fight state of 4.8.1 travels in `suspend.base` and the journal deltas like any dynamic state; V23t round-trips it, the policy choice included.
- Applying a delta runs **no rules code**, so a journal written before a crash and read after an auto-update replays correctly within one `saveVersion` (and across versions as below).
- Compaction to a fresh base every 50 commands *(illustr.)* and at every Down, death, paid act and clean quit. Compaction never gates an outcome frame: it runs after the frame (4.8.3).
- **Rejected (Q4 B):** a full rewrite of the slot after every command. It works on NVMe, but every outcome frame would wait 8–50 ms on SATA and HDD (8.6), and a run would write about 206 MB.
- **Across a patch (new lead's call; corrected after the check).** Deltas are encoded against a `saveVersion`'s schema, so they cannot be applied to a snapshot already migrated to a new schema. On load with an older `saveVersion`, the journal is first applied to the old-schema base **by the old version's delta reader, kept in the migration layer for at least one release**, then the result is migrated once and written as the new `suspend.base` by atomic replace (WO7). Within one `saveVersion`, a journal applies unchanged across builds, because a delta is state, not rules.
- Tests: V19t `SlotEquivalence` (state equals the base plus the deltas after every command on the goldens), kill-fuzz at random byte offsets **inside writes, inside compaction and inside the load path** (`LoadPathKillFuzz`), and V18t `KnowledgeReloadMonotone` and `DeathNotRewindable` (approved). `SaveMigrationGoldens` includes previous-release slots with uncompacted journals of 1, 49 and 50+ records, one taken mid-combat, and asserts that the migrated state equals the migration of (old base + deltas) (4.10.1).
- **Budget (overlap, not latency):** p95 durable journal write ≤ the shortest outcome-gating animation, 120 ms *(illustr.)*. Bytes per run drop from about 206 MB (full rewrite) to about 3 MB.
- **Durability needs a platform shim (lead's call, after the check).** .NET has no managed directory flush, `FileStream.Flush(true)` flushes a file only, macOS `fsync` does not reach stable storage, and Windows `File.Replace` is not write-through. The save writer therefore carries a small platform shim, outside Core's determinism analyzer:
  - POSIX: `fsync` the file, rename, then `open(dir, O_RDONLY|O_DIRECTORY)` and `fsync` the directory;
  - macOS: `fcntl(F_FULLFSYNC)` on the file and the directory;
  - Windows: `MoveFileExW(MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH)` (or `ReplaceFileW` followed by `FlushFileBuffers`).
  - **Process-kill resistance** (the probing the outcome rule forbids) needs only that `write()` has completed; **power-loss durability** needs the shim. Both latencies are measured for SN16 (6.1).

#### 4.8.5 Forward-only knowledge

As approved (`PartyKnowledge` forward-only; the slot wins).

### 4.9 Seed display and entry (change SV-16; lead's call; agreed, Q1; M2)

The check character, the build tag, the mismatch notices and where the code is shown were revised after the check (lead's call, after the check); the 13-character code with a check character and a build tag stands as accepted.
- **Seed:** 60 bits, shown as 12 Crockford base32 characters in three groups, then **one check character drawn from the same 32-symbol Crockford alphabet**. The check symbol is a weighted sum over GF(32) of the 12 seed symbols **and the 2 preset symbols**, with 14 distinct non-zero position weights, so it catches every single-symbol substitution and every adjacent transposition (the draft's "5 bits of a CRC" guaranteed neither). Crockford's symbol checks `* ~ $ = U` are rejected, because chat markdown and URLs mangle them.
- **Format:** `7KQ2-9XHM-4TRA-C` *(illustr.)*, followed by a 2-character configuration preset (party cap and doors, for example `3D`) and a **5-character build tag**, uppercase Crockford, read case-insensitively: 4 characters of the **generation identity** (`genVersion`, `genContentHash`, `configHash`, `unlockHash` of the run the code would start) and 1 **rules character**, a hash of `rulesVersion` and the rules-relevant `manifestHash` (so a rules-only tuning patch, which bumps `manifestHash` but not `rulesVersion`, also moves it; 4.10; cross-document comparison 2026-11-23, CC-130). Example: `7KQ2-9XHM-4TRA-C · 3D · B3F9A`. The tag is not covered by the check character: a typo in it can only raise a mismatch notice, never change the dungeon.
- **Entry:** in the launcher's "Seeded run" field. Parsing uses an ASCII map (never culture `ToUpper`: Turkish dotted I). I and L read as 1, O as 0; hyphens and spaces are ignored; the check character is validated over the seed and the preset, and a mismatch shows "check the code", with no guess. Seeds ≥ 2^60 are rejected. The entry screen carries the one-line seeded-run notice (4.7).
- **A missing suffix:** "No settings or build tag in this code: using default settings; the run may not match the sharer's." The run may still start, on the defaults.
- **Display (where the code is shown):**
  - **A personal run shows its code only after it ends** (death, victory, abandon or unravel), on the run record and the death and victory screens, with a copy button. Never on the camp or pause menus during the run: otherwise a player could start the code as a seeded run on a second profile, scout the dungeon, and play the record-eligible personal run with foreknowledge.
  - A seeded run shows its code in the camp and pause menus too (the player already has it).
  - Never on the floor HUD.
  - **Shareability.** When a run's own generation identity differs from the one a typed code would produce (non-canonical unlocks or grants from M3, or a configuration the preset cannot encode), the copy button is labelled "seed (will play differently for others)" (words in the naming pass), and the copied tag lets the receiver's game warn (below).
- **Mismatch notices (two tiers):**
  - Generation character differs: "This code was made on another build or with different unlocks or settings; this dungeon may differ."
  - Only the rules character differs: "This dungeon matches; fights may play differently on this build."
  - Either way the run starts and is recorded as not comparable. No lockout.
- **Seeded runs and progress (agreed, Q1):** as 4.7. The run record and the death and victory screens show the seeded mark beside the code (new lead's call), and the version-crossed mark when it applies (4.10.1).
- V19t **`ShareCodeErrorDetect`** (M2) enumerates every single substitution and adjacent swap over sample codes and asserts that each is caught; a further row asserts that a code copied from a non-canonical personal run either raises the mismatch notice or reproduces the run's `routeDigest` and `layoutDigest`.

### 4.10 Versioning and content updates (change SV-15 ⚑; lead's call; agreed, Q3)

| Change | `genVersion` | `manifestHash` | Old shared codes | Saves | Replays |
|---|---|---|---|---|---|
| Text, art, barks (display-tagged) | — | — (`displayHash` moves) | identical | load | valid |
| Content record added | — | bump | **mostly identical under SV-8**: relative orders kept; changed only where the new record sorts first | load (4.10.1) | invalid if generation differs |
| A generation-relevant number tuned | — | bump | differ | load (4.10.1) | invalid |
| Rules-only tuning | — | bump (`genContentHash` unchanged) | dungeon identical; fights differ | load | invalid |
| Generator code changing a digest | bump | — | differ | load (4.10.1) | invalid |
| Rules-core code | — | — | dungeon identical | migrate | invalid (`rulesVersion`) |
| Save schema | — | — | — | migration required | — |

- **Ids are frozen after first release.** A stable id may get aliases, never a rename (V19s `IdFrozen`). A rename would move seeds and break stored floors.
- **No content deletion after release.** Records become `cut` or `held` and stay in the manifest (R-SEED-3 already keeps such records in the catalogue). A committed theme or boss therefore cannot vanish from a run.
- **Told plainly:** keyed sorts protect whole-catalogue orders only. Weighted picks (loot identity decks, template picks, EV rarity and pool) still remap on any deck change. Shared codes survive patches at **route and layout-graph level**, not loot level.
- **Golden rebaseline is the only way digests change:** the PR carries the bump, a `rebaseline: <reason>` tag, and a stage-diff report signed by the owning segment.

#### 4.10.1 A run in progress across a patch (agreed, Q3; the details are new lead's calls)

- **The run continues** on the new build. Rejected: Q3 B (the run cannot be loaded on the new version; its record kept).
- **Floors already generated are kept exactly.** Every generated floor is a stored `FloorStatic` with its `GenInputs` (4.8), so the floor the party stands on, and a floor committed at Travel on but not yet entered, load unchanged (a speculative floor is never stored, so it never crosses a patch). The debug-build `SaveDigestMatch` regeneration is skipped for a floor when **any** of the `genVersion`, `genContentHash` or the configuration and unlock snapshot hashes recorded in that floor's `GenInputs` differs from the current build's, because it cannot match by design; otherwise the floor is regenerated against its stored inputs and compared (widened after the check: an added record or a tuned generation number moves the content hashes without a `genVersion` bump).
- **Future floors use the new version.** Each floor's `GenInputs` records the `genVersion`, `manifestHash` and `genContentHash` it was generated under; the save header keeps the run's starting key and `genVersionsUsed[]`.
- **What stays fixed (new lead's call).** The configuration snapshot, the unlock snapshot, `drawableSet` and the route state were stored at GS0.2 and are never recomputed, so R-SEED-5's "a run never changes mode mid-run" holds. Content records added by the patch are not drawable in that run. The no-deletion rule above guarantees that every record the run already references still resolves.
- **The mark.** `comparable = false` in the save header. The run is excluded from records, and its code carries the build-mismatch notice when shared (4.9).
- **Which patches set the mark (lead's call, after the check; told plainly, 14.3).** Only a patch that changes `genVersion`, `genContentHash`, the rules-relevant `manifestHash` or `rulesVersion` relative to the run's. A display-only patch (text, art, barks: `displayHash` alone moves) changes nothing the run plays, so the run stays comparable. This reads "a patch lands" in Q3 as "a patch that can change the run".
- **Telling the player (lead's call, after the check).** When the mark is first set, a one-time notice shows on load: "This run began on an earlier version. It continues normally but will not count toward records." The run record and the death and victory screens show a version-crossed mark beside the code, as for the seeded mark (4.9). Words in the naming pass.
- **Still a personal run (new lead's call).** Unlocks, codex entries and launcher memory advance as usual, because Q3 names only records and shared codes.
- **Replays** of such a run are "not replayable on this build" on every build; the bug-report bundle still holds the log and every floor's `GenInputs`.
- **Save schema (new lead's call).** Every `saveVersion` bump ships a migration. BUILD test **`SaveMigrationGoldens`** loads the previous release's golden run saves and suspend slots (including slots with uncompacted journals of 1, 49 and 50+ records, one mid-combat), continues each for one floor on the new build, and asserts that the kept floors' digests are unchanged and that a migrated slot equals the migration of (old base + old deltas) (4.8.4). It also covers **a `stackMax` change** in `inventory.json`: each stack instance stores its `stackMax` at creation, so a saved stack's dose count (count = stackMax − spent) is unchanged across the patch (ITEMS 5.1; cross-document comparison 2026-11-23, CC-3). A save without a migration path is a release blocker, never a lost run.

### 4.11 Golden seeds and the release seed set (change SV-18; lead's call)

**The finding that shapes both sets:** from floor 1 generation reads the hero, and from floor 2 the whole party snapshot (4.6), so a bare seed is not enough. Every item is a **generation fixture**: (runSeed, floor, `GenInputs` fixture, configuration). Fixtures come from RS 11.3's fixed arrival states (theme and boss forced through the route) or from a whole run driven by the deterministic Reference simulator.

| Set | Purpose | Size *(illustr.)* | Content | Change rule | Wave |
|---|---|---|---|---|---|
| **Golden** | Exact-match regression, never statistics | M1: 16 F1 floor fixtures + 4 replays. M2: about **80 whole runs chosen by greedy set cover** (73 cover every stratum, including Blueprint × transform; random sampling needs about 408) + 16 replays + 8 suspend/resume scripts | key, `GenInputs`, per-stage digests (`routeDigest`, `layoutDigest`, `terrainDigest`, `hazardDigest`, `trapDigest`, `encounterDigest`, `lootPlanDigest`, `lootLedgerDigest`, `curiosityDigest`, `stampDigest`, `bindDigest`), stream counters, `GenerationLog`, final replay hash | Rebaseline only (4.10). **Any LR2–LR5 event on a golden fails PR CI** | M1 / M2 |
| **Release set** | Coverage, budgets and the DH 18.2 reading | Per wave: **2,000 whole runs per cap** (every run has an F1 at party 1, so this gives the 1,881 F1 trials per cap DH 18.2 needs at a true 4%); plus generation-only floor fixtures, **2,000 per (floor, party size, cap)** (about 80k floors) for budgets; plus a set-cover top-up | Must cover: every (theme, floor) cell; every (boss, Rendition, mirror) (BR 4.17); every Blueprint × transform; every F1 Curiosity; Windfall present and absent; trimmed configurations; doors on and off; XI treasure targets and `LosSymmetric` templates | Frozen at each milestone (named, for example `RS-M2.v1`). Coverage re-checked each build; lost coverage appends fixtures with a set version bump, never silently | M1 (F1 only) / M2 |
| **Logged extension trials** | Gates whose per-cell N exceeds the frozen set | Per gate (6.5) | `Derive(simRoot,"trial",i)` (a label distinct from the frozen runs' `"release"`), listed in the report so they replay | `simRoot` fixed per sign-off | M2 |
| **Named gate sets** | Fixed samples in owner docs | `IR-gate-1k`, `EV-launcher-1e5`, `V11-prop-50` | registry rows | as owners | M2 |

**Reading (flagged; DH 18.2 ⚑, agreed Q11 for the target):** "over the release seed set" means **the frozen release set plus its logged extension trials**.

**How the sets are chosen, reproducibly (lead's call, after the check):**
- **Golden candidates:** `Derive(simRoot, "golden", i)` for i < K (K = 4,096 *(illustr.)*). Greedy set cover over the strata of the table above; ties broken by the lowest i; **candidates with any LR2–LR5 event are excluded** (they would fail PR CI). The chosen i values are stored in the registry (`golden.m1`, `golden.m2`).
- **Release runs:** `Derive(simRoot, "release", cap, i)` for i < `releaseSet.runsPerCap`. **Extension trials** keep their own label, `Derive(simRoot, "trial", i)`, so no extension can duplicate a frozen run. Generation-only fixtures: `Derive(simRoot, "fixture", floor, partySize, cap, i)`.
- **Stored versus live:** the golden digests, the nightly digest manifest and every BUDGET read use **stored `GenInputs` fixtures**, captured once per set version from a Reference WholeRun (and, for F1, with the hero-creation inputs pinned). DH 18.2 and other WholeRun readings **regenerate F2+ from live play** off the same `runSeed`s. Stored fixtures are refreshed only at a set version bump (for example `RS-M2.v1` → `RS-M2.v2`), so a balance change never moves the digest manifest without a version bump.

**Golden preconditions:** the 4.5 locks; IR filter-first; the DH reshuffle; SV-1, SV-5 and SV-8 applied, or their fallbacks adopted (14.2); TL formulas published (M1) and HZ's (M2).

**Nightly release-set digest manifest:** a digest change between commits without a `genVersion` bump fails. This catches generator changes the goldens miss.

---

## 5. Validation in full

### 5.1 Phase model and failure classes (change SV-20 ⚑; lead's call; M1 model, M2 complete)

- Every check has a **qualified id** `Vn.CheckName`, owned by exactly one validator.
  - Each check has a phase: `s` (build: static and enumeration), `g` (generation, at a named stage or AP1/AP2), `r` (a runtime assertion in play), or `t` (a test).
  - Simulator gates are **not** validator parts. They move to the report registry and keep their ids as aliases (6.3).
- **Qualified ids resolve name collisions without renaming owners' checks:** `V8.TemplateReadability` and `V9.TemplateReadability`; `V9.ContactLethalityBound` and `V10.ContactLethalityBound`.
- **Every `g` and `r` check declares one failure class:**

| Class | Meaning |
|---|---|
| `repair` | Its owner's LR1 action |
| `stageRetry(stage)` | LR2, then a named waivable or never-waived end |
| `floorRetry` | LR3. Allowed only with a build-time proof reference (`proofRef`), FL-15 kept; V19s rejects a `floorRetry` check without one |
| `waivable(fallback)` | LR4 |
| `neverWaived` | LR5 after LR2 or LR3 |
| `defect` | LR5 directly |
| `runtimeAssert` | For `r` checks: a crash in dev and CI; in release **fail closed**: suppress the leaking surface (show less), log `RuntimeAssert{check}` as P0, never block play. BUDGET row: 0 on golden replays and kill-fuzz. Budget ≤ 0.5 ms per `KnowledgePass` *(illustr.)* |
| `report` | Logged only |

- **RS 11.2's header** "The build fails when one of these fails" (RS 2671) becomes: "`s` and `t` parts fail the build; `g` and `r` parts act by their registered class."

### 5.2 The consolidated suite

| Id | Owner | s (build) | g (stage) | r | t | Wave (corrected) |
|---|---|---|---|---|---|---|
| V1 ThemeBudget | RS | ✓ (owns `V1.ThemeSix`) | — | — | — | M2 |
| V2 BossCoverage | RS | ✓ coverage only (boss-kit rows owned by V17s) | — | — | — | M2 |
| V3 PoolRules | RS | ✓ | — | — | — | M2 |
| V4 ThemeFloorFill | RS | ✓ | — | — | — | M2 |
| V5 RouteEnumeration | RS | ✓ **as written** (every subset through M3; 8.3) | — | — | V5t random-subset sample (M4) | M2, grows M3 |
| V6 RouteDeterminism | RS | — | — | — | ✓ (calls the V19t harnesses) | M1 stub, M2 |
| V7 CharterChecks | RS | ✓ (owns `V7.NameRegistry`, manifest-driven; every segment feeds it) | signature-type row → V12g | — | — | M2 |
| V8 LayoutGrammar | FL | ✓ | AP1 structural asserts (`floorRetry`) | — | V8t | M1 core, M2 |
| V9 RoomTerrain | ROOM | ✓ | GS5 lookup (`floorRetry`) | — | — | M1 partial, M2 |
| V10 RoomHazards | HZ | ✓ after V9s | GS6 lookup (`floorRetry`) | — | — | M2 |
| V11 TrapsLocks | TL | ✓ (proofs, or a property test at N = 50 on generation truncated at AP1) | V11g1 at AP1; V11g2 at AP2 (`stageRetry`, then `waivable` `V11Fallback`) | — | ✓ | **M1 skeleton** (`LockNeverCritical`, `HeNoLock`, `KeyReachable`; TL 484), M2, M3 Trapwright |
| V12 EncounterPlacement | EP | ✓ worst case (about 1.1M, cached per layer) | GS12 geometry; AP2 floor assertions incl. **ProtectedIntact (group part)**, **signature ≥ 2**, `SeedHasTarget` | `ArrivalThroughPortal`, Billets | ✓ incl. `SignatureTypeTwice` over the release set (request: EP accepts) | M1 static + chain, M2 |
| V13 LootEconomy | LE | ✓ | AP2 (d)–(i) by named action; (f) → report | — | V13t (m) | M1 (a)(c), (g)(i) on F1, (m); M2; M3 (o) |
| V14 ItemRolls | IR | ✓ (a)–(m) | GS10/GS11 asserts with actions | — | ✓ | M1 (a)(b)(d)(h)(j) + draw test; M2; Later (m) |
| V15 LootSources | LP | ✓ incl. **an HE/Due socket-binding proof** (request) | AP2 after `V16g.SlotLegal` | — | ✓ | **M1 rows: owner deliverable (request to LP)**; M2; M3 Windfall |
| V16 Curiosities | EV | ✓ incl. `F1HostAlways` (SV-30) | `FixedDrawCount` at GS3; **`SlotLegal` before V15g; the rest after V15g** | — | ✓ | M1 stub, M2, Sequel M3, Ripening M3-late or M4 (the agreed "M3-late or M4", EV-28; RS 11.2; cross-document comparison 2026-11-23, CC-85) |
| V17 BossRooms | BR | ✓ ((k) guard rows → simulator tier) | `StampProven` at AP1; the rest at AP2 **after V15g and V16g** | — | ✓ incl. the event-order replay | **M1 subset** as listed in BR 10's M1 row: (a)–(c) on Old Ragged's kit, (f) root-column Deny, the Den Guise stamp proof, `V17g.StampProven` at AP1, start zones and lanes (BR 1265; cross-document comparison 2026-11-23, CC-70), M2, M3 Gambits |
| V18 Information | XI | ✓ | AP2 graph rows: `SketchNeverSecretNode`, `TreasureSelectorLegal`, `TreasureFactCap`, `NoF1Pointer` | **V18r:** `MapNeverUnknown`, `ChalkNeverContents`, `ChalkNeverStub`, `RouteKnownOnly`, `ForecastKnownOnly`, `BubbleNeverPositioned` on every `KnowledgePass` (`runtimeAssert`) | ✓ incl. `KnowledgeLeak` | M1 tests, M2 |
| **V19 Conformance (new)** | 11–12 | V19s | V19g at every stage end and GS17 | — | V19t | M1 partial, M2 |
| V20 Hollowfolk (change CH-25 ⚑; camp and hub design 9.2) | camp and hub (segment 13) | ✓ static, display-only (incl. `AmbientNoTrade`, `AmbientNoHire`, `AmbientNoEffect`, `AmbientCoreBlind`, `StationRingNoAmbient`, `AmbientNotServiceAnchor`) | — (no g phase) | V20r reports (simulator in `LoreEmu`; human from 6.11 sessions) | ✓ (incl. `LoreInert`, `SeededRunLoreReadOnly`, `CampProbeResistance`, `VendorStockTrimmedInCore`) | M2 |
| V21 StartingKits (change SK-10 ⚑; starting kits design 9.2) | starting kits (segment 14) | ✓ static and display-only parts as V20 ((a)–(l); (m) reserved, Later) | — (no g phase) | V21r reports (analytics, outside the gates: `KitSoldAtCamp1`, `VariantPickPerOffer`, the Oddment rates and others) | ✓ (incl. `KitIssueStable`, `SeededRunOddmentReadOnly`, `ReplayOddmentMatchesSave`, `OddmentStableOnReload`, `AbandonWritesNoLore` extended) | M1 (a)–(e), plus (f) if the Due is in the slice (else M2); M2 (g)–(j); M3 (k)–(l) |
| V22 HeirloomsCurses (change HL-14 ⚑; heirlooms and curses design 9.2) | heirlooms and curses (segment 15) | ✓ (a)–(j) | (k) (`EliteMaladyCapPerFloor`, `MaladyNotBeforeBoss`, `CuriosityMaladyOfferLegal`, `CuriosityMaladyPayerLegal`; `CrookedRoadGateLegal` Later) | — (the 9.3 reports are calibration class, beside DH 18.2, never a gate) | ✓ (l)–(m) | **M1** registration with only the (m) `RunModifierIdInConfigHash` field row live; M2 (e) rule rows; M3 (a)–(c), the (e) rows, (h), the (j) heirloom glyph, (l); M4 (d), (g), the other (j) glyphs, (k), (m); Later (f), (i), `CrookedRoadGateLegal` |
| V23 CharmLane (change CA-18 ⚑, a request; listed here, its RS 11.2 row pending with the RS owner; charm abilities design 9.3) | TK (charm abilities, segment 16) | ✓ V23s (ITEMS 19.1 rules 29–31, the `charmTriggers` and `charmOutputs` vocabularies, `simOnly` and `boonEligible` rules, the never-printed "catch" check, `iconSilhouette`, CardFit) | — (no g phase) | V23r (fire, missed, wasted and bind rates, precision ceiling, landing envelope under both CA-17 readings, the Bobbin win delta at M3; simulator, in the two shadow modes) | ✓ V23t (replay determinism of `charmKept`, the caught flags and deferred catches; save and suspend round-trip; the Bobbin choice, unit and slot in `configHash`) | M2 (V23s and V23r on the M2 roster plus the shadow records); M3 (the new limits live, the Bobbin rows) |

**V19 `Conformance` contents (change SV-19):**
- **V19s:**
  - registry completeness (every opened path, stage, check, report and arm has a row; every `Derive` call site maps to a row; the consolidated suite is V1–V23, with V20 `Hollowfolk` listed, change CH-25 ⚑, V21 `StartingKits` listed, change SK-10 ⚑, V22 `HeirloomsCurses` listed, change HL-14 ⚑, and V23 `CharmLane` listed, change CA-18 ⚑, its RS 11.2 row pending with the RS owner);
  - label and content-id collision scan (64-bit, across every registered label and manifest id); `DomainDisjoint`;
  - `IdFrozen`; ASCII ids;
  - every stage declares reads, writes and streams; draw-free stages open none (`DrawFreeProof`);
  - every act declares a rerun span covering every stage that reads its writes (`RerunSpanCovers`);
  - `floorRetry` checks carry a `proofRef`;
  - **`ClassDeclared`** (after the check): every `g` and `r` CheckRow has exactly one failure class;
  - report and arm ids unique, and every gate has a threshold, an instrument, an N and a sidedness;
  - the DS11 analyzer, with wrapping arithmetic allowed only in the key-algebra file (4.1); unsigned index types at every `Derive` and `H` call site;
  - `CoreNoProfileRef` (after the check; replaces `KeyCoversProfileGrants`, 4.6);
  - Resistant n ≤ 6;
  - a doc linter comparing MAIN 6.3's stream sentences with the registry.
- **V19g:**
  - the **DrawLedger** at every stage end. It subsumes, by calling, V8 `DrawCount`, V9 `TerrainDrawCount`, V13 (m), the V14 draw test, the V15 windfall 4, V16g `FixedDrawCount` and V17g `BossVariantKey`; owners keep their rows as citations;
  - stage digests (by `CanonicalWriter` and SHA-256, 4.5);
  - the floor was built from the `GenInputs` digest frozen at GS1 (a speculative floor with another digest is discarded, DS14);
  - the DrawLedger closes per stage run (DS15).
- **V19t** (the CI tier column was added after the check; tiers in 5.8, times in 8.7):

| Test | Wave | CI tier |
|---|---|---|
| `KeyAlgebraVectors` (also under a checked-arithmetic debug build, 4.1) | M1 | PR |
| Determinism (same key twice) | M1 | PR |
| Cross-OS digests (Windows, Linux, macOS; x64 and ARM64) | M1 two OSs; M2 all | nightly, as separate per-OS jobs |
| **Hostile environment:** goldens and `SaveDigestMatch` in a fresh process under tr-TR culture, a non-UTC timezone, 1 and N cores, invariant globalisation on and off | M1 | nightly |
| Goldens and replay checkpoints | M1 | PR |
| `StreamIndependence` (add a dummy child or consumer; nothing else moves) | M1 | PR |
| `RetryIsolation` (force each rung; **compare the set of keys opened and their raw draw values, not outputs: only keys under the declared retry path and its rerun span may differ**; corrected after the check, because later stages legitimately read the retried output) | M1 | main |
| `AP1EarlyEquivalence` | M1 | main |
| `ExplorationCombatIsolation` (**every out-of-combat command leaves the `combat` counter unchanged, and `Draw(combat, c..c+K)` at the next fight's start is identical** across scripts that differ only in out-of-combat commands; corrected after the check, because positions and HP legitimately change how many combat draws the fight then takes) | M1 | PR |
| Block independence (perturbing one loot block's entry count moves no other block) | M1 | PR |
| The shared **`LootDoorInvariant` harness** (V6, V14t, V15t, V16t and V17t call it) | M1 | main |
| `SaveDigestMatch` | M1 | PR (goldens); nightly (release set) |
| `DevReleaseParity` (after the check): force each defect-path event (LR5 at each attempt, `ArenaStampFallback`) on a debug and on a release build and compare the digests | M1 | nightly |
| `SpeculationEquivalence` (after the check): for the same camp-exit state, the speculative and synchronous paths give byte-identical `FloorStatic` (4.4) | M2 | main |
| `UnravelPath` (after the check), in a **release configuration**: force defects at F1 (personal and seeded runs) and at F(N+1) at Travel on (and in a speculative job, which must show nothing); assert the attempt count, the worst-case time, the WO6 record and its mark, the bug-bundle contents, that the F1 redraw does not count toward the 8-redraw cap, and that a seeded run writes no progress | M2 | nightly |
| `ForecastSurvivesRetry` (force an F1 LR3; the Curiosity's identity **and presence** are unchanged) | M2 | main |
| `ContentAddStability` (insert dummy theme, boss and card records; relative keyed-sort orders unchanged) | M2 | main |
| `ConfigHashCoversRunConfig` (after the check, 4.6) | M1 | PR |
| `ShareCodeErrorDetect` (after the check, 4.9) | M2 | PR |
| `ToggleIsolation` (6.1) | M2 | with each delta gate's own tier |
| `SharedSeedProfileBlind` (intent scripts) | M2 | main |
| `SlotEquivalence` | M2 | PR (goldens) |
| Kill-fuzz and `LoadPathKillFuzz` (inside writes, compaction and the load path) | M2 | nightly, 2,000 offsets *(illustr.)*; sign-off 20,000 |
| `ProbeResistance` (floor and camp command types; with V18t) | M2 | nightly |
| `SeededRunWritesSeenOnly` (Q1, agreed) | M2 | main |
| `SaveMigrationGoldens` (Q3, agreed; BUILD from the first release) | M2 | main |

**Pre-generation content suites** keep their own codes and have no V id. They are registered as `content.*` rows:
- MAIN 6.5 load validation;
- ABL 14.5; STAT 11; CON 20.1; ENM 20.1; ITEMS 19.1; AR 17.1; W 21.1; TK 18.1; **CN 19** (not 20.1);
- DH 21.2 and 21.3;
- CL 12.2 with the VG, SH, ARC and HR codes;
- CMP 11.2, **including the CMP-8 tests extended to formations and the solo column** (EP 13's hook to segment 12; answered after the check): `content.CMP8.Formations` and `content.CMP8.SoloColumn`, owned by CMP (CMP-8) over EP's formation and solo-column data, run with CMP 11.2 at tier 1 (request: CMP and EP confirm).

**Failure classes of the `g` checks** (added after the check; V19s `ClassDeclared` enforces one class per row; owners may refine a class only through their own row, never leave it blank):

| Check | Class |
|---|---|
| V8g AP1 structural asserts, V9g and V10g lookups | `floorRetry` (`proofRef`: V8s, V9s, V10s) |
| `V17g.StampProven` | `floorRetry` (`proofRef`: V17s); after the last attempt, `waivable(ArenaStampFallback)` in every build (5.5) |
| V11g1, V11g2 | `stageRetry(traps)` / `stageRetry(trapsResolve)`, then `waivable(V11Fallback)` |
| V12g geometry and floor assertions (incl. `ProtectedIntact` group part, signature ≥ 2, `SeedHasTarget`) | `stageRetry(encounters)`, then `waivable(V12FloorFallback)` for the waivable set, `neverWaived` for EP's never-waived list |
| V13g (d)–(i) | each by LE 10.2's named action: (e) `stageRetry(dealShuffle)`; (f) `report`; (h), (i) `repair`; any row LE names no action for, `defect` |
| V14 GS10/GS11 asserts | `repair` (IR's relaxation rungs and fallbacks), then `defect` |
| V15g | `repair` (LP fallbacks), then `waivable(LootBindDegrade)`; on an HE or Due socket, `defect` |
| `V16g.FixedDrawCount` | `defect` |
| `V16g.SlotLegal`, the rest of V16g | `repair` (EV's EventDraw and EventPlace fallbacks), then `defect` |
| V17g (the rest) | BR 9.2's named action where it names one; otherwise `defect` |
| V18g graph rows | XI 9.2's named action where it names one; otherwise `defect` |
| DH `GuaranteedItemUnreachable` | `defect` |
| V19g DrawLedger close, the `GenInputs` digest | `defect` (`DrawLedgerMismatch`) |
| V18r rows | `runtimeAssert` |

`Floor1BossSustain` (DH 21.1 step 6; CL 13, the Vanguard in M1) is an arithmetic **BUILD** content check, not a simulator row.

**One owner per shared check:**

| Check | Owner | Called by |
|---|---|---|
| `ProtectedIntact` | FL (structural part, `V8g` at AP1) | V9s proof; **group part is `V12g`** (EP owns; `stageRetry(encounters)` then `neverWaived`) |
| `OneClockPerRoom` | V10 | V15g Windfall rows, V17g. EV 4.10's "V15g (OneClockPerRoom)" is this check run at V15g |
| The theme's 6 | `V1.ThemeSix` | V9, V10 FallType, V11 HarmfulConditionsRecount, V16s, V17s (a) |
| Floor1BossMindReachable, F1EscortBreakable, signatures 1–2, counterType immunity | V17s | V2 checks coverage only |
| Carriage | V17s (d) | V14 (h) |
| `SeedHasTarget` | CL | executed in V12g |
| `NoFloor1Companion` | CMP | executed in V16g |
| KP5, CV8 / `Floor1NoCoveredStarts` | V9s | V12s |
| `NameCollision` | `V7.NameRegistry` | CL, the class docs, V16s, V17s (j). Fixes the stale "27 docs" and "28 documents" counts |
| Draw counts | V19g DrawLedger | V8, V9, V13 (m), V14t, V15g, V16g, V17g |

Where an owner's own list names a shared check, the entry is a **call to the owner in this table**, never a second owner: EV 9.2 (`NameCollision`), IR 9.2 (h) (carriage) and RS 11.2's V9 row (`ProtectedIntact`, `ThemeSix`) (SV-20; cross-document comparison 2026-11-23, CC-128).

### 5.3 Build dependency graph and caching

**Tiers** (each depends only on the tiers above it; unrelated branches run in parallel):

| Tier | Runs |
|---|---|
| 0 | Content load and schema; V19s |
| 1 | The content suites |
| 2 | V7s (`NameRegistry`); **V1s** (owns `V1.ThemeSix`, called by V9, V10, V11, V16s and V17s at tiers 3–5; moved here after the check) |
| 3 | V8s → V9s → V10s; V8s → V11s; V9s, V10s → V17s; V8s, V9s → V18s; V7s → V20s (static and display-only, no g phase; change CH-25 ⚑, camp and hub design 9.2); V7s → V21s (static and display-only parts as V20, no g phase; change SK-10 ⚑, starting kits design 9.2) |
| 4 | V9s, V10s, V11s → V12s |
| 5 | V12s → V13s → V14s → V15s → V16s |
| 6 | V1s (tier 2) → V3 → V5 → V6t; V17s → V2; V1s, V2 → V4 |
| — | All `t` parts after their `s` parts |

**Caching:** results are cached by content hash **keyed by each check's declared read set**, so invalidation fans out correctly. Example: an ENM base-stat edit invalidates V1, V4, all of V12s and V17s. A validator above 20% of the cold budget must declare a decomposition (FL risk 9's per-Site method is the template) or move rows to a sampled `t` part.

### 5.4 Generation-time order: the two assertion points (change SV-9 ⚑; lead's call; M1)

**Why two points (fixes SQ2).**
- DH 12.4 and FL-15 say one floor assertion point, "before the loot step". EP, LE, LP and EV put the shared point after LootBind.
- Both are kept: **AP1** is HE-7's point (before loot, as written), and **AP2** is the shared point after LootBind and EventPlace.
- **Equivalence rule:** stages after AP1 never mutate layout. Step-9 group moves affect only `ProtectedIntact`'s group part, which is at AP2. So running AP1's checks at AP1 or deferring them to AP2 gives identical outputs. V19t `AP1EarlyEquivalence` enforces this; any future stage that edits geometry must move its asserts to AP2.
- **This is a flagged change to DH 12.4 lines 1024–1032, FL 6.1 item 3 (677–681), FL 11.2 and the FL-15 row (13).**

**AP1** (after GS7):
1. **Act:** V11g1 per-rule failure actions (LR1); on failure `traps/retry1` (LR2); if that fails, TL removal of failing traps and Plain for failing locks (draw-free), then V11g1 again (TL 6.22, the approved SV-10 order; cross-document comparison 2026-11-23, CC-5).
2. **Assert:**
   - `HE7Reserved` (DH);
   - FL's structural runtime asserts: `MainPathConnected`, `NothingInWalls`, `AntechamberDominatesArena`, `ArenaEntrancesFromAntechamber`, `CircuitValid`, `BypassValid`, `SkippablePool` (structural), `IndexIntegrity`, `PortalMinimums`, `RoomCounts`, and **`ProtectedIntact` (structural part: terrain, hazards and traps against protected squares)**;
   - the V9g and V10g lookups ("cannot fail"; they only assert);
   - `V17g.StampProven`;
   - V11g1.
3. **Failure:** `floorRetry` checks → LR3; V11 → LR2, then TL removal and Plain with a re-check, then LR4 `V11Fallback` (cross-document comparison 2026-11-23, CC-5).

**AP2** (after GS15):
1. **Act, in pipeline-stage order, earliest stage first** (corrected after the check; lead's call, after the check). The draft said "registry order", which listed TL's GS13 act before EP's GS8 act, against nesting rule 1, and which owners could reorder without review. The order is now a **code-fixed ordinal**, part of the assertion point, so changing it needs segment 11–12 review and a `genVersion` bump (3.3 item 3). Each act declares its rerun span:
   1. EP's floor-assertion actions, `encounters/retry1` (GS8), re-run GS8–GS15 (SocketPlan, LootPlan and loot re-run from their unchanged keys; trapsResolve re-runs at its own attempt-0 key);
   2. LE V13g (e) `dealShuffle/retry1` (GS10) re-runs GS10 for that kind only, then GS14–GS15;
   3. TL's V11g2 per-rule failure actions (LR1); on failure `trapsResolve/retry1` (GS13; LR2) re-runs GS13–GS15; if that fails, TL removal of failing traps and Plain for failing locks (draw-free), then V11g2 again, then LR4 `V11Fallback` (cross-document comparison 2026-11-23, CC-5);
   4. V13g (h) 7c removal and (i) overflow rules (GS14) re-run GS14–GS15.
   - **Retry budgets on a rerun:** a rerun that re-enters a stage S resets S's LR2 budget **only if S's inputs changed**. Otherwise S's earlier retry outcome is kept and never redrawn, and its `retry1` key is never reopened a second time with the same inputs (DS15).
2. **Assert, in order, every check re-evaluated as a pure assertion:**
   1. V11g2
   2. V12g (incl. `ProtectedIntact` group part, signature ≥ 2, `SeedHasTarget`, and the Tattle and Wager bindings as assert-only; TL owns their actions)
   3. V13g
   4. `V16g.SlotLegal`
   5. V15g (incl. `V10.OneClockPerRoom`, the Windfall rows)
   6. V16g (the rest)
   7. V17g
   8. V18g (graph rows)
   9. DH `GuaranteedItemUnreachable` (`defect`; proven through the HE-7 reservation and SocketPlan)
   10. V19g ledger close
3. **Failure:** by class (5.1).

### 5.5 The retry and fallback ladder (change SV-10 ⚑; lead's call; agreed, Q2 for the release end; M1 for LR0–LR3 and build parity, M2 for the release clean end)

| Rung | What | Derivation | Logged as |
|---|---|---|---|
| **LR0** | Construction proven at build: V8s, V9s, V10s, V11s, V12s, V17s enumerations; HE-7's constructive reservation; the HE and Due socket-binding proof | — | — |
| **LR1** | In-stage draw-free repair: LS12 hearing repair; EP per-slot chain, cluster re-run 5b–8, 9b, 10g; TL failure actions (TP2, TP8, TP10, KeyReachable, Wager revert, boxed lurker → container); TL post-retry removal and Plain (draw-free, taken after LR2 and before LR4; cross-document comparison 2026-11-23, CC-5); LE V13g (f), (h), (i); LP square fallbacks, remaps, Windfall move or drop, ProprietorFallback, offer trim; IR relaxation rungs, per-option fallbacks, hero redraw; EV EventDraw and EventPlace fallbacks; CMP list exhaustion | inside fixed counts | owners' rate reports |
| **LR2** | **One stage retry** of the failing stage and every later stage that reads it, never an earlier one: `traps/retry1`, `encounters/retry1`, `trapsResolve/retry1`, `dealShuffle/retry1`. At most one per stage per attempt | `Derive(stageKey,"retry1")` | `StageRetried{floor, attempt, stage, check}` (answers EP's "every retry is logged for segment 12") |
| **LR3** | **Floor retry (HE-7), at most once per floor:** the next attempt (a = 1 from attempt 0) re-derives every attempt-scoped child; attempt-invariant streams are untouched, and loot re-runs from identical keys, so no rewind is needed. **Only `floorRetry` checks with a `proofRef`** (FL-15 kept) | `Derive(floorKey,"attempt",a+1)` | `GenerationRetried` (existing; FL risk 10 alarm, kept to FL's list) |
| **LR4** | **Terminal fallback, only for `waivable` checks:** `V11Fallback`; `V12FloorFallback` (waivable set only, never-waived list as approved); **`LootBindDegrade`** (a non-HE, non-Due socket → merge, else coin-only; replaces the action-less "hard failure"; the LP owner confirms the merge-or-coin rule, request); `ArenaStampFallback` (BR's approved fallback; after the check it runs in **every** build at the last attempt, with budget 0, so a dev or CI build records the same floor and then fails hard). **Designed terminal outcomes are not LR4:** LE (e)'s fit premium after the re-deal is approved behaviour, reported only | none | `GenerationFallback{floor, fallback, check}` |
| **LR5** | **Defect:** an unwaivable failure after LR2 or LR3, or any `defect`-class check (including `LootBindUnplaceable` on an HE or Due socket, and `GuaranteedItemUnreachable`, both of which the build proves impossible) | — | `GenerationDefect` |

**One ladder in every build (lead's call, after the check).** The draft let a dev or CI build crash after attempt 1 while a release build tried attempt 2, so a release floor could exist that no engineer's build could reproduce, and a player's bug report, replay or code could not be replayed. Now **every build configuration runs the same attempts and fallbacks and produces the same floor**; only what happens *after* the output is recorded differs.
- **The attempt rule.** Any LR5 condition at attempt a < `GenMaxAttempts` − 1 goes to attempt a + 1 (the next unused index, 4.3), with every LR2 counter reset (nesting rule 3). LR3 (`floorRetry`) also moves to the next attempt. So a = 1 is either the HE-7 retry or the first defect-path attempt, and **at most 3 attempts are made in total** *(illustr.)*, on any path. This keeps worst-case generation ≤ 1 s (8.2).
- **`GenMaxAttempts`** is a code constant covered by `genVersion`, never a configuration key: changing it changes outputs, so it needs a bump and a rebaseline.
- **After the output is recorded:**
  - **Development and CI:** any LR5 event, any attempt beyond the HE-7 retry, and any `ArenaStampFallback` is a hard failure (a CI failure, a dev assert) with a dump of key, `GenInputs`, stage and attempt. DH 12.4's "a second failure is a crash bug, never a silent fallback" is kept in substance: the second failure still fails the build hard and is never silent; it now fails after recording the floor every build would make.
  - **Release builds (agreed, Q2; replaces DH 12.4's crash in release builds only).** Generation is deterministic, so a crash would recur on every reload and the run could never continue. **When the last attempt fails, the clean end applies (agreed, Q2):** the run ends as "the dream unravels", its record is kept and marked, and a bug report is filed. Never silent: P0 telemetry fires (`RunUnravelled`), and CI holds LR5 at 0. Rejected: Q2 B, a hand-authored safe floor per theme (kept as a future item, 14.2); Q2 C, the crash.
  - **Where it happens (new lead's call; corrected after the check).** F(N+1)'s authoritative generation runs at Travel on from the camp-exit state (4.4), so the party is at camp. Travel on shows the unravel card instead of the next floor; the run is finalised by WO6 with the result `unravelled`, and the run files are removed. A failure seen by a *speculative* job earlier in camp is logged P0 but shows nothing, because the speculation's inputs are not the floor's real inputs. The card's words belong to the naming pass and writing.
  - **The record (new lead's call; credit changed after the check).** The run record is kept with the result `unravelled` and a defect mark, and is excluded from records (Q2). It is neither a death nor a victory. **A personal run's progress (unlocks, codex entries, Deeds counters) is credited as a death at camp N would earn, never less** (lead's call, after the check): the draft's "as an abandoned run" penalised the player for our bug, and nobody can choose to trigger a defect, so this cannot be farmed. A seeded run still advances nothing (Q1). *(Pending request HL-6, M3: an unravelled personal run also writes R1 for bosses already felled, the R2 offer for `BossFelled` already logged and the Grip-marks already earned; heirlooms and curses design 4.1.2.)*
  - **F1 at run start (new lead's call; refined after the check).** Nothing has been shown yet. A personal run silently discards the seed, and the launcher draws a fresh candidate that goes through the **full launcher order** (theme rule, Fallow, F1-boss rule, onboarding) **with a fresh 8-redraw budget**; the discarded candidate does not count against R-SEED-6's budget (flagged in SV-10), and the discarded key is logged P0 with its bug report. A seeded run shows "this code cannot be dreamed on this build" and starts nothing. Either way no run record is written, because no run began (told plainly after the check: a narrowing of Q2's "its record is kept", 14.3); the bug report is still filed.
  - **The bug report (new lead's call).** A local bundle: the key, `GenInputs`, `GenerationLog`, the failing stage, check and attempt, the command log and the run save. It is written beside the profile and sent only under the game's telemetry consent setting (hook to the main design owner).

**Nesting rules (fixes SQ19):**
1. The ladder of the earliest stage that owns a check runs first. The Tattle and Wager bindings are TL's at GS13; EP's floor assertions only assert them.
2. An LR2 never re-enters an earlier stage.
3. An LR3 resets every LR2 counter.
4. Each check declares exactly one class (V19s `ClassDeclared`), and a check's class never depends on the build configuration.
5. V10's "HE-7 single retry at seed + 1" (HZ 2669) reads: "V10g's runtime lookup is a `floorRetry` assert at AP1".

**Worst-case stage runs per floor:** every build, up to 3 attempts × (1 + traps retry + encounters retry, which re-runs trapsResolve, + trapsResolve retry, available once per set of trapsResolve inputs (5.4) + dealShuffle retry). Times in 8.2.

### 5.6 Failure budgets (change SV-11; lead's call; all *(illustr.)* unless marked)

- **Budget LR4 directly, per pass.** Measure the retry rescue rate nightly: new report `RetryRescueRate` = 1 − P(fail again | first fail) per stage.
- **ρ is the correlated-failure share** (corrected after the check): a failing pass fails again for certain with probability ρ, and otherwise independently with probability p. Then P(fail again | first fail) = ρ + (1−ρ)·p, the per-pass LR4 rate is p·(ρ + (1−ρ)·p), and ρ is derived as **ρ = (1 − rescue − p) ÷ (1 − p)**. The draft's "ρ = 1 − rescue" mixed the two models.
- The **LR2 ceiling per pass ≈ LR4 budget ÷ ρ**, with ρ **frozen at the M2 budget freeze** at the upper bound of ρ derived from the measured rescue rate (Wilson on the rate), so the threshold does not move night to night; it changes only through a logged re-freeze (lead's call, after the check).
- **Reading (corrected after the check; lead's call, after the check).** BUDGET rows **gate only on pooled reads, per (floor, cap)**, on the release set's stored fixtures, by the rule-2 one-sided bound (6.4, z = 2.18). Per-cell reads per (theme, floor, party size, cap) on the 10^5-floor nightly soak are **ALARM**: a cell alarms its owner only when its Wilson **lower** bound is above the budget. The draft gated per cell on the soak; at about 1,300 floors per cell a ≤ 0.3% budget could pass only with zero events, so it would have failed every night at a true rate of 0.1%. A zero budget observed without failures proves only ≤ 3/N (the rule of three); that is why LR3 and LR5 also require proofs.
- **What a pooled read can pass (told plainly after the check; *illustr.*, 80% chance of passing at z = 2.18):** F1 pools about 4,000 floors per cap (the fixtures plus the release runs' F1s), F2–F5 about 6,000–8,000 per (floor, cap), depending on how many party sizes the cap allows. A **≤ 0.3%** budget then passes at a true rate of about **0.08% on F1 and 0.11–0.14% on F2–5**; a **< 1%** budget at about **0.56% and 0.64–0.68%**. A rung whose true rate sits closer to its line needs an extension at its own N (6.5) or a lower true rate.

| Rung | Budget | Class |
|---|---|---|
| LR2 `encounters` | ≤ 3% of floors at ρ = 0.3 (recomputed from ρ frozen at the M2 budget freeze) | BUDGET |
| LR2 `traps`, `trapsResolve` | ≤ 1% each | BUDGET |
| LR4 `V12FloorFallback` | < 1%, **read pooled** (EP, approved; a per-cell reading would tighten an approved rule) | BUDGET |
| LR4 `V11Fallback` | ≤ 0.3% | BUDGET |
| LR4 `LootBindDegrade` | ≤ 0.3% | BUDGET |
| LR4 `ArenaStampFallback` | 0 (a content bug; BR) | BUDGET |
| **Per run:** runs with ≥ 1 LR4 floor | ≤ 3% | BUDGET |
| LR3 `GenerationRetried` | 0 on the release set (FL risk 10, as written); ≤ 1 per 10^4 on the soak, pooled | BUDGET |
| Per-cell soak reads of every row above | alarm when the cell's Wilson lower bound exceeds the budget | ALARM |
| LR5 `GenerationDefect` | 0 over 3×10^5 floors weekly (≤ 0.001%) and 10^6 at sign-off (every build runs the same ladder, so CI sees exactly what release would) | BUILD |
| `RuntimeAssert` | 0 on golden replays and kill-fuzz | BUILD |
| LR1 rates | owners' approved rates stay authoritative: `FormationFallbackRate` < 10% (< 5% F1); `ProprietorFallbackRate` ≤ 10%; `RelaxationStepRate` rung 1 ≤ 2% | as owners |

### 5.7 Hooks this answers

- **RS V7's signature-type assertion:** authority moves to `V12g` (signature ≥ 2). The "fixed seed sample per (theme, floor, party size)" becomes a V12t regression row over the release set (request: EP accepts; fallback V19t).
- **EP 769 "every retry is logged":** `StageRetried`.
- **FL and ROOM "retry logging":** `GenerationRetried`, `StageRetried`, `GenerationFallback`, `GenerationDefect`.

### 5.8 CI tiers and time budgets (change SV-28; lead's call; all *(illustr.)*)

| Tier | Trigger | Cores | Wall budget | Runs |
|---|---|---|---|---|
| Local | pre-push | dev machine | ≤ 2 min | affected unit tests, the analyzer |
| PR | every push (MAIN 8) | 8 | ≤ 10 min | compile + analyzer; V19s; content load; content suites; V*s **warm** (typical about 26 s; worst case an ENM-wide edit about 2 min); unit tests; all goldens; replays; FsCheck at 100 cases (seed printed; shrink configuration vectors, not seeds); DrawLedger on 1k fixtures; `KnowledgeLeak` smoke |
| Main | merge | 8 | ≤ 30 min | V*s **cold** (≤ 5 min after SN5); 2k release floors with every `g` |
| Nightly | daily | **16** | ≤ 2 h | the full release set GS1–GS17 + every `g` + pooled budgets (stored fixtures); release-set digest manifest; FsCheck at 10k cases; the hostile-environment run; the 10^5-floor soak (per-cell ALARM reads); GenStat and Walker reports; kill-fuzz, `LoadPathKillFuzz`, `ProbeResistance`, `DevReleaseParity`, `UnravelPath`. **No simulator drift sample** (moved to weekly after the check). Breakdown in 8.7 |
| Nightly, per-OS jobs (after the check) | daily, in parallel | own runners | ≤ 30 min each | cross-OS digests: Windows x64, macOS ARM64, Linux ARM64 (goldens, replays, the digest manifest on a 10k-floor sample). Not counted in the 16-core budget |
| Nightly, minimum-spec reference runner (after the check) | daily | 1 minimum-spec machine with the slowest supported disk (HDD) | ≤ 60 min | the runtime rows (BUDGET from M2; `OutcomeWaitMs` REP): generation p95 and worst case, load with verification, the save overlap budget, `KnowledgePass` asserts. CI servers never read these rows; a calibrated CI-to-minimum-spec factor may supplement, re-measured at each balance freeze |
| Weekly | weekly | 32 | ≤ 8 h | 3×10^5-floor LR5 soak; **the drift sample, 25% of the simulator matrix** (7); the shared zero-gate smoke at 3,000 runs (6.6); LauncherEmu; ViewModel; dashboards (each report a time series by commit, with intervals and owner alerts) |
| Sign-off | per wave | 32 | ≤ 24 h | the full simulator matrix with every gate at its N (6.5); the zero-gate smoke at 30,000 runs; 10^6-floor soak; kill-fuzz at 20,000 offsets; the checklist of 7 |
| Human | each playtest | — | — | ingest under report ids (6.11) |

**M1 stands up a minimal nightly (after the check):** cross-OS digests on two OSs, the hostile-environment run, the F1 release-set pooled budgets and `DevReleaseParity`, so that every M1 checklist row has a tier that runs it.

**Runtime budgets** (read on the minimum-spec reference runner only; REP at M1, BUDGET from M2):
- Floor generation, AP1, AP2 and retries: p95 ≤ 250 ms, worst ≤ 1 s, whether speculative during camp or synchronous at Travel on (4.4).
- `LauncherCheckMs` ≤ 5 ms p95.
- Load with verification ≤ 500 ms.
- Suspend write: the overlap budget (4.8.4); `OutcomeWaitMs` p99 ≤ 250 ms on the minimum-spec HDD (REP; after the check).
- `KnowledgePass` runtime asserts ≤ 0.5 ms.

Parallelise across seeds and fixtures, never inside one generation.

---

## 6. The simulator, reports and gates

### 6.1 One simulator, six instruments (change SV-21; lead's call; M1 skeleton, M2 full)

Everything runs headless under `dotnet test` (MAIN 7, settled).

| Instrument | Runs | Used for |
|---|---|---|
| **S: Sim** | The real rules core with scripted player models; modes FixedArrival (RS 11.3) and WholeRun (RouteSample, DH 18.2, EV and LE runs) | Death, win, rounds, parity, loot, boss and encounter rows |
| **W: Walker** | FL 11.4's walker over generated floors with the CMP time model; combat rounds imported from S | `ExploreSeconds`, clicks, clock lines, travel, Listen, Seg10 rows |
| **L: LauncherEmu** | **The real launcher code** against in-memory **sandbox profiles**; no combat; deaths drawn from history models | `CuriosityFreshShare`, `RunsToFirstRepeat`, `BossRepeatRate`, `LauncherRedrawRate`, `Floor1ThemeShare`, `LauncherCheckMs`, `StrandCashRate` (streak), `LauncherF1Neutral`, EV's scripted-memory arm |
| **V: ViewModel** | Core queries plus the RC5 registry over replays; no Godot | `MarksPerScreen`, `BudgetWorstCase`, `CardFit` spot checks |
| **G: GenStat** | GS1–GS17 per floor with every `g` check, no play | every BUDGET row, `F1CuriosityPresence`, `LootMixByCategory`, `UsableDropShare`, variety rows, DrawLedger |
| **H: Human** | Playtest telemetry through the same event schema | `FloorTimeByPartySize`, door pick rate, coverage, readability, `FormationRead`, glance test, audio blind-ID, `PhaseReadTime`, intent readability, `TimeModelError` |

**Shared rules:**
- **One report path.** Every report is computed from the core's event log (the payloads EV 9.3, BR 9.3 and CMP 7.4 define), by the same code for S, W, L, V, G and H. A human-versus-simulator disagreement is therefore a model difference, never a formula difference.
- **Common random numbers are mandatory.** Every comparison runs its arms on the same trial seeds (CMP 11.3 already says so).
- **`ToggleIsolation` (V19t), on a draw trace.** For each on/off arm pair:
  1. every (path, index) present in **both** arms' draw traces must have an identical value; and
  2. stage digests must be identical for every stage **outside the feature's influence closure**, computed from the declared reads and writes (the `StageRow` reads and writes of 9.1, checked by V19s).
  If either fails, the delta gate prints **INVALID**, not PASS or FAIL. **INVALID counts as FAIL for a one-sided class and as INCONCLUSIVE for a two-sided class, and blocks the related sign-off item until the isolation is fixed** (after the check). (Model A's draft required identical digests everywhere outside the subtree; that would have marked nearly every delta gate INVALID, because draw-free consumers and cross-floor state legitimately carry a feature's effect.)
- **Toys retired as sources of verdicts.** `gen.py`, `dh.py`, `seg10sim.py` and the EV 8 toy stay as regression fixtures. CMP's "gen.py re-run before M2 sign-off" becomes an S run of the same arm.
- **The simulator writes no player profile** (change SV-13 ⚑).
  - MAIN 6.6 (497) and EV 9.1 (1444) say "the simulator writes nothing, except the scripted-memory arm". EV 9.4 (1523, lead's call) has the Streak arm "write profile memory as a personal run".
  - Both are kept true by writing **sandbox profiles in LauncherEmu only**.
  - This is a change to client-agreed text (EV-19, EV Q1), covered by the client's approval of this document on 2026-11-23 (13).
- **M1 skeleton (SV-21):** F1, the Vanguard at levels 1–3, one boss, the M1 kit. It produces DH 21.5 rows as **reports only**; "verdicts read the M2 simulator only, never a toy" is kept (CL 5.6, CMP-29). It **measures** per-floor simulated time, generation stage times, ρ per retry and fsync latency on the minimum spec before the M2 budgets are frozen. **Completed at M2 (corrected after the check):** the M1 skeleton has only the F1 M1 stages (no hazards, curiosities, `dealShuffle` or M2 loot), so it measures what exists and files a **measurement report against the 8.7 assumptions** (its M1 output); the M2 stage times and ρ per M2 retry are measured at the M2 planning gate (7), which then recomputes 8.2 and 8.7, resizes the tiers and freezes `gen.budget.*` and the CI budgets. Both latencies of 4.8.4 (write completion and the platform shim's durable flush) are measured.

### 6.2 Player-model and arm catalogue (change SV-22; lead's call; M2)

Arms are vectors over orthogonal axes. Every name is qualified, so "Reference", "Oracle", "balanced" and "BASE" never collide. Each axis has a default equal to the gate arm the approved docs name.

| Axis | Values (owner) | Default |
|---|---|---|
| `Skill` (combat policy) | `Novice`, `Basic`, `Reference`, `Expert`, generalised from ARC 5.4 (**flagged extension; lead's call; fallback: each class owner writes a novice script by the M2 planning gate**); `WorstLegalShade` (EP, SH, VG) | `Reference` = DH 19.3 plus the ability 13 scorer (settled) |
| `Door` | `Reference`, `Random`, `Oracle` (RS 6.11, 11.3) | Door A when off; `Reference` when on |
| `Muster` | `Reference` (CMP's name kept; = MusterBestFit), `Oracle`, `DiversityFirst`, `AlwaysDecline`, `DeclineAt{k}`, `DeclineTwo`, `DeferThenDouble`, `ForceKit`, `SwapChurn`, `Without<Class>×Eye`, `SoloPerClass`, `SustainedRowNoShortfall` (CMP 11.3) | `Reference` |
| `Walk` | **Each owner's policies under its own namespace:** `FL.Lean` (the gate arm), `FL.Heavy`, `FL.Stealth`, `FL.Hunt`; `XI.Balanced`, `XI.Completionist`; `EV.First10` (and EV's light/median/heavy names); new **`Walk.Beeline`** (main path only; containers only when on-path; no Listen; no optional fights; report only; the lower bound of floor time) | `FL.Lean` |
| `LootPolicy` | `Greedy`, `Balanced`, `Smart`, `Ignore`, `Blocker`, `Pull` (LP 9.4) | LP's default (request: LP names it) |
| `Rest` | `Reference` (DH 19.3), `RestAdjacent` (EV), `MidBossRest` (BR) | `Reference` |
| `History` (L only) | `Streak`, `F1only`, `Weak`, `Learner`, `Average`, `ScriptedMemory`, `First10Runs` (EV 8). **Profile-history models, never skill levels** | `Streak` for the freshness gate |
| `Party` | forced 1–4; `Schedule` (1/2/3/4/4, CMP 7.1); `ScheduleDeclining` (LP M3); class mix = hero class × the canonical list (RS 11.3); `Without<Class>`; `Healer`/`NoHealer` (DH 18.1) | `Schedule`; F1 always party 1 |
| `Cap` | 3, 2 (**agreed**: every arm at both caps) | both |
| `Arrival` | `FixedArrival`, `WholeRun`, `RouteSample` | per report |
| `Toggles` (named baselines) | `EP.Base` (role-blind), `ROOM.NoTerrain`, `HZ.NoHazards` (segment 3 on, 4 off), `TL.NoTraps`, `TL.Baseline`, `LP.Loot7aOnly`, **`XI.BASE`** (XI 8, line 83: the approved surfaces plus the M1 minimap); cover variants; clutter 0/30/50; Stirs; Void density; Devices never/once/free; Renditions; cut states; Graces; Windfall states; concentrated guard posts; Two Doors; Last Hollow; Element Echo; Mousetrap; Waylay; `billetScale`; Wax-Key-always; no-Mechanics; skilled scout | shipped defaults |
| `AIAim` | toy one-step, lookahead (ROOM 11.4) | both, read as a band |
| `Con` | reference, lowest legal | where listed |

- **`PatrolFightRate`** (EP owns; the gate is agreed, XI Q2; its ≥ 60% of BASE is *(illustr.)*, owned by EP, **not a client number**; cross-document comparison 2026-11-23, CC-127) uses **`XI.BASE`**, as XI 713 and 1189 and EP 2873 say. Model B's "`Base.EP`, XI's BASE retired" contradicted an agreed gate and is rejected.
- **Walker equivalences are an owner question (request to XI, FL, EV), not a silent mapping.** For example: is `XI.Completionist` equal to `FL.Hunt` plus optional fights?
- **Charm arms (request CA-18 from the charm abilities design 9.1, 10; pending, the SV owner rules; M2, the boon arm M3).** Requested: the two shadow modes of `simOnly` charm records (an observer on cloned state that never mutates the run; a forced-loadout arm paired with a same-seed no-charm partner); the CG2 forced-loadout human-session arm (6.11; the loadout a `configHash` field, 4.6; no unlocks advanced, Q1; the Bobbin off); and the heirloom-boon comparison arm for the Bobbin's win delta. Fallback (14.2).

### 6.3 Result classes (change SV-23; lead's call; M1 skeleton, M2 complete)

Every report row carries exactly one class, which says what a miss does.

| Class | A miss does | Who acts | Examples |
|---|---|---|---|
| **BUILD** | Fails the build or PR | Owner | V*s, V*t, V19, LR5, `RuntimeAssert` |
| **BUDGET** | Fails nightly; blocks the release branch | 11–12 plus owner | 5.6 |
| **REL** | Blocks wave sign-off until levers pass or the client re-rules | Lead, then client | `Floor1ClassSpread`, `HealerGap`, `HealIsolation`, `FloorTimeByPartySize`, `RestsPerFloor`, `PatrolFightRate`, affix outliers, intent readability, AR 6.4, TK envelope; after the check: `Level10BeforeFinalBoss` (CMP lever 3 is reverted while it fails, 6.9) and `LauncherF1Neutral` (its lever is the reweighting route, 6.10) |
| **SW** | Keeps a feature flag off | Lead | Two Doors gates 1–6; gate 7 (BossParity) for the Last Hollow choice; after the check: `TimeModelError` (keeps simulator-time verdicts off, 6.7) |
| **HOLD** | Holds a theme or content item (`shipStatus = held`) | Lead | ThemeParity on random floors, ClassThemeSpread (ii), `FloorTimeByTheme`, readability |
| **SHIP** | Content moves a wave or falls back | Lead or client, pre-approved | F1 `BossDeathShare` per Rendition (BR-40); EV Graces (`EventDeathShareDelta`); HZ Void and Dream packages; Gambits; Windfall solo; the Wake |
| **BIND** | Fires that segment's own cut order **before any CMP 7.4 lever** | Segment owner | the TL-38, EP-33, LP-10, EV-23, BR-40, XI-31 lines; ROOM+HZ ≤ +1.6; FL's layout line (≤ 3.5 min, ≤ 40 clicks); EP `PartySurpriseShare` |
| **CUT** | Applies a named cut | Owner | `MapSecondsPerFloor` |
| **PB** | Blocks the next playtest build | Owner | IR `DeadRareRepeatRate`, `StepDownPerRun`, `LateFloorGearShare`, `BuildDiversity` |
| **ESC** | Lead applies the lever order, then the client decides | Lead | ARC novice row; HR reliance and desirability; CL reliance; `PreBossRestSafety`; CMP p75 > 23 min; `DistinctnessCheck` |
| **CAL** | Reported to the client with the evidence; never fails a build | Lead → client | DH 18.2 rows; BR F2–5 bands |
| **ALARM** | Lead investigates | Owner | LE-24 trip-wire; CN 19.3 25%; `MidBossRestRate` 25%; `ContainersOpened` −10%; `OpeningSpringRate` > 70%; `GraceExpiryRate` |
| **REP / WATCH / FELT** | Information | Owner | the rest; `EnvKillShare` 3–8% is FELT |

**DH 18.2, three roles, no text change (reading).** The DH 18.2 metrics are computed once. Their CAL verdict goes to the client ("calibration targets, not validation gates", kept true). The same metrics are read by RS 6.11 gate 5 (SW, Two Doors only), BR-40 (SHIP, a Rendition is retuned or cut) and EV-3/EV-12 (SHIP, Graces). None fails the build. **Told plainly:** one calibration figure now drives two switches.

**FL's walker "gate"** becomes a BIND commitment feeding CMP 7.4's REL gate, as CMP 7.4 already reads it.

**Gates bind at the shipped cap (told plainly; reading of CMP 7.4, agreed).** Every report runs at both caps, as agreed. After the M2 cap decision (7, step 2), REL, HOLD, SW and SHIP verdicts bind at the **shipped** cap; the other cap's rows are REP. Every REL gate is re-read at the shipped cap before the F1 balance freeze (step 2b).

### 6.4 Statistical reading rules (change SV-25; lead's call; M2)

1. **Approved bespoke rules stay verbatim:** DH 18.2 F1 by the 95% upper bound; RS 6.11 gate 2 with its 90% bound and sample floors; CMP 7.4's median of ≥ 12 sessions. The registry stores each one as written.
2. **The decision rule depends on the class (fixes the critic's loosening finding):**

| Class | Rule |
|---|---|
| REL, safety-related SHIP (death shares, push deaths), BUDGET, HOLD, SW | **One-sided:** PASS only when the bound on the failing side is inside the threshold. Every such gate is planned as a **Pocock two-look test at an overall one-sided α of 0.025: both looks use z = 2.18** (stated exactly after the check; the draft's sizes were computed at z = 1.96, and "one-sided 95%" would have meant z = 1.645). If the first look straddles, N is doubled once; **still straddling = FAIL** (the miss action applies) |
| CUT, PB (after the check) | One-sided, single look at z = 1.96: the action applies when the bound on the passing side is outside the threshold, so a cut or a playtest block is never fired on noise |
| BIND, REP, CAL, ESC | Two-sided 95% interval (z = 1.96), three-way verdict (PASS, FAIL, INCONCLUSIVE). The point estimate may decide only here, and the row is flagged "marginal" |
| ALARM (after the check) | Alarms only when the whole 95% interval lies on the failing side; never blocks |
| WATCH, FELT (after the check) | No decision rule; printed with N and interval |

   Every registry row declares one-sided or two-sided, and its z. Running until a gate passes (optional stopping) is not allowed. Rule 1's bespoke rules keep their own z (DH 18.2 F1 by the upper end of the two-sided 95% interval, z = 1.96, which is what 1,881 runs at a true 4% is sized for).
3. **Interval methods:** Wilson for rates; Newcombe for differences of independent rates, **paired** (McNemar-style) under common random numbers; bootstrap for medians, percentiles and ratios (`PatrolFightRate`); the rule of three for zeros.
4. **Spread gates are read on the observed spread, as the client numbers were set (told plainly).** This covers ThemeParity ≤ 5 and ≤ 8, ClassThemeSpread ≤ 10, BossParity ≤ 5 and `Floor1ClassSpread` ≤ 10.
   - Noise inflates a max-minus-min spread: +2.1 points at 4 classes with a standard error of 1, +2.5 at 6 themes.
   - So the gate runs **N large enough that noise adds ≤ 1 point** (standard error per cell ≈ 0.48, about 4.3× the earlier N).
   - **The interval method (lead's call, after the check):** a **paired percentile bootstrap of max-minus-min over cells under common random numbers**, B = 2,000 resamples of trials *(illustr.)*, with the resample seed `Derive(simRoot, "bootstrap", LabelId(reportId))`. A spread gate is in a one-sided class, so it **reads the bootstrap's upper percentile at the rule-2 level (98.5%, matching z = 2.18)** of the observed spread, never the point value. The 6.5 sizes (about 4,300 and 14,000) are *(illustr.)* starting points sized for the noise bias only; they are **recomputed at the M2 planning gate** from the measured per-cell variance, so that a true spread 2 points under its line passes with at least 80% probability.
   - The **cross-fitted, de-noised spread is printed beside it** as REP.
   - **Told plainly:** a perfectly fair game still reads about +1 point, and the compute cost is in 8.4.
   - Model A and the simulator layer proposed gating on the de-noised spread. That would quietly loosen agreed client numbers by 2–3 points, so it is rejected for gating.
   - The REP spreads `ClassBossContribution` and `ClassHpEqDelta` are read de-noised.
5. **Multiple splits.** `Floor1ClassSpread` "inside every split" (cover, Nightmare tier, Horn Post, subclass, pick) uses rule 2 per split. **A marginal split fails** (REL).
6. **Every report prints its N, interval, verdict and `simVersion`.** A row whose `ToggleIsolation` failed prints INVALID, which counts as FAIL for a one-sided class and blocks the related sign-off item until the isolation is fixed (6.1).

### 6.5 Minimum N (change SV-25; *(illustr.)*; sizes the release set and extensions)

Rule-2 rows were re-sized after the check at z = 2.18 (6.4); rule-1 and two-sided rows keep z = 1.96.

| Gate | Unit | N per cell | Basis |
|---|---|---|---|
| `F1DeathShare` < 5% (95% upper; CAL, rule 1, z = 1.96) | runs | **2,000 per cap** | at a true 4%: 1,881; at 3.5%: 815 |
| F1 `BossDeathShare` ≤ 2.5/100 per Rendition (SHIP safety, z = 2.18) | arenas engaged | **1,200**, extended to **4,700** if straddling | at a true 1.5: 1,164; at 2.0: 4,654. Pooled over hero class, per class reported (request: BR confirms) |
| `HealerGap`, `HealIsolation` ≤ 10 (REL, z = 2.18) | runs per arm | **2,500** paired | ±2 points at a win rate of 0.3 |
| `Floor1ClassSpread` ≤ 10 | F1 runs | **about 4,300 per class per split** (starting point; recomputed at the M2 planning gate, 6.4 rule 4) | observed-spread rule, bootstrap upper percentile |
| ThemeParity, ClassThemeSpread | floors | **about 14,000 per (theme, class, floor, cap)** (as above) | observed-spread rule |
| BossParity ≤ 5 | F5 floors | about 14,000 per (final boss, class, cap) (as above) | observed-spread rule |
| `EventDeathShareDelta` ≤ +0.5 pp (SHIP safety, z = 2.18) | paired runs | **3,100**, extended to **6,200** | discordance 1–4% |
| `CuriosityFreshShare` | profiles × runs | **200 profiles** × 10/30/50 runs (change SV-25 to EV 9.4, lead's call; approved 10 profiles gives [0.944, 0.984] against 0.95 and can never decide) | LauncherEmu, about 7 core-min |
| `PatrolFightRate` (REL, z = 2.18) | floors per arm | **2,500** paired | ratio bootstrap |
| `DoorCampShare` ≤ 15% | posted fights | 1,250 | ±2 points at z = 2.18 |
| `BossDeathShare` F2–5 bands | arrivals | 1,000 per (floor, party, class) | band width 4–13 points (CAL, z = 1.96) |
| Oracle chooser ≤ 333‰ (SW, z = 2.18) | runs | **1,000**, 8 single-floor rollouts per door, sign-off only | at a true 300: 975 (815 at z = 1.96) |
| BUDGET rows | floors | 2,000 per (floor, party size, cap), **read pooled per (floor, cap)** (about 4,000 on F1, 6,000–8,000 on F2–5; 5.6) | `V12FloorFallback` at a true 0.5%: upper 0.81% on F1, 0.70% at 8,000 (z = 2.18) |
| Zero gates | runs or floors | 3,000 → ≤ 0.1%; 30,000 → ≤ 0.01% | plus a structural partner (6.6); one shared smoke observes every zero gate (weekly at 3,000, sign-off at 30,000; 5.8) |
| `TimeModelError` ≤ 10% (SW, after the check) | human sessions | the CMP slice, ≥ 12 (agreed), **20 recommended** | bootstrap interval of the relative error, read one-sided on its upper bound (z = 2.18); an upper bound above 10% applies the suspended path (6.7) |
| `LauncherF1Neutral` (REL, after the check) | profiles × runs | 200 sandbox profiles × 50 runs per arm, paired launcher vs bypass | equivalence: PASS when the paired Newcombe interval of each F1 boss share and each Rendition share difference lies inside ±2 points (6.10) |
| `FloorTimeByPartySize` (H) | sessions | ≥ 12 per cap arm (agreed); **20 recommended** to narrow the median's interval (statistics 7–18 of 24) | median CI printed beside the gate |
| Door pick rate (H) | offers | ≥ 150 per theme pooled, ≥ 40 per cell (agreed rule) | the bound clause binds below about 246 offers |
| Readability, `FormationRead`, glance, audio (H) | reads | ≥ 100 per theme or family | 90% on 100 → lower bound 0.83 |
| Charm CG2 metrics (H; request CA-18 from the charm abilities design 10; pending, the SV owner rules; M2) | charm-holding runs in forced-loadout sessions | ≥ 30 per metric *(illustr.)* | a gate that cannot reach its N is "simulator-only, playtest-confirmatory" and cannot fail for lack of data |

### 6.6 Zero-event gates need a structural partner (change SV-25; lead's call; M2)

A simulated zero only bounds a rate at about 3/N. Each zero gate is restated as **"structurally impossible (a validator) and observed 0 in N (a simulator smoke)"**. **A simulated non-zero is a P0 bug, never a tuning item.** Partners marked "owed" are **blocking owner deliverables on the M2 checklist**. **Each zero gate is two registry rows under one class, BUILD** (after the check): its structural validator row, run in the build, and its simulator smoke row, run in the shared smoke (weekly at 3,000 runs, sign-off at 30,000; 5.8). The one generation-side zero gate, `ArenaStampFallback` = 0, keeps its 5.6 class, BUDGET, read on the release set and soak; its structural row (V17s) is BUILD.

| Zero gate | Structural partner |
|---|---|
| F1 environment deaths = 0 | V10 `F1NoHazards` (exists) **plus** V9 F1 push-lane rows and V11 `F1TrapRules` as co-partners (request: HZ states the scope of "environment" on F1) |
| `ForecastLethalSurprise` = 0; `PreviewExactness` 100% | V18t `PreviewExactness`; previews and the core share one function |
| `DeadRareRepeatRate` = 0; `OncePerRunGeneratedTwice` = 0 | V14 G asserts on the ledger |
| `DormantAtChoice` = 0 | V14 static check (**owed by IR**) |
| `WagerWalkRoundRate` = 0 | V11 Wager rows (M3) |
| F1 `SurpriseDeathShare` = 0 | V12 F1 package (EP-2) |
| `ArenaStampFallback` = 0 | V17s offline proof of every Guise × mirror × rotation |
| `EliteRemovedBeforeFirstTurn` = 0 | V10 Void geometry (**owed by HZ**) |
| `ZeroHpAfterDreamfall` = 0 solo (renamed from `DownedAfterDreamfall` and redefined as party units reaching 0 HP within one round of placement, because the hero has no Downed state; HZ 6.4, 11; cross-document comparison 2026-11-23, CC-96) | V10 `SnagProven` plus landing rules (**owed by HZ**) |
| `LootBindUnplaceable` on HE/Due = 0 | the V15s binding proof (**owed by LP**) |

### 6.7 Time-model calibration (change SV-25; lead's call; M2)

- **`TimeModelError`** = |simulator median − human median| ÷ human median floor time, on the M2 slice (F4–5, party 4), from the same seeds and prepared saves.
- At **≤ 10%** *(illustr.)*, simulator-time gates may give verdicts. The row is class **SW** (it switches simulator-time verdicts off), read one-sided on the upper bound of a bootstrap interval of the relative error over the slice's sessions (≥ 12, 20 recommended; 6.5), so a point read cannot pass by chance (after the check).
- Above 10%: the companions owner re-fits the per-action cost table (FL 11.4, CMP 7.4), and simulator-time verdicts are suspended until the error is back under 10%.
- **While suspended**, each segment's BIND line is **attributed from human-slice telemetry** through the same event path, so segment self-cuts still fire **before** any CMP lever (CMP 7.4 order kept). If attribution is impossible, **the cap decision is blocked** until the error passes. A CMP lever never fires first.
- Redone at each balance freeze, and whenever grouped playback or the UI turn time changes.

### 6.8 `FloorClockLedger` (change SV-26; REP; M2)

- One table per (cap, party 4, F4–5).
- It sums the lines, each against its own named baseline: FL layout walker 3.5 min; TL +0.3 min; ROOM+HZ ≤ +1.6; EP, LP, EV, XI ≤ 0 net; `BossMinutes` 7; fight minutes. All values are as owners wrote them *(illustr.)*.
- The sum is printed against the agreed 20 minutes, beside the human `FloorTimeByPartySize`.
- Its only job: show **which line is over** when the human gate fails, so that segment's self-cut fires first.
- **Camp and hub time (change CH-5 ⚑; camp and hub design 4.5, 9.2; M2).** RS 0.1's 12–16 min of camps sits outside the floor sum and is measured beside it by the reports `CampSeconds` and `CampClicks` (by camp index and party size; first-10-runs and veteran arms), `RunWallClock` (floors + camps + cards + end screens; new-player arm), `HubSeconds` and `DeathPathSeconds` (with `DeathNightHubLines`), sourced from 6.11 human sessions until telemetry consent exists. If a median misses, the camp and hub design's cut order applies; with no human data at M2 sign-off, the camp-time row is marked unmeasured.

### 6.9 Precedence and lever discipline (change SV-26; lead's call; M2)

| Rung | Gates | Rule |
|---|---|---|
| **PR0** Correctness | BUILD, BUDGET, zero gates | Never traded |
| **PR1** Client numbers and settled safety | `Floor1ClassSpread` 10, `HealerGap` 10, `HealIsolation` 10, `FloorTimeByPartySize` 20 min, ThemeParity 5 (choice floors), door pick 65%, `PatrolFightRate` (the gate agreed, XI Q2; its 60% *(illustr.)* and owned by EP, not a client number, so it is tuned only by EP; CC-127), `HazardPushDeaths` 10% (settled), AR, TK and ENM release gates | Fixed; misses go through owners' lever orders, then the client |
| **PR2** Content ship gates | HOLD, SW, SHIP | Content held, switched off or slipped; never fixed by moving a PR1 number |
| **PR3** Segment lines | BIND, CUT | Each segment's self-cut fires before any CMP 7.4 lever (approved) |
| **PR4** Calibration | CAL | Lever orders as approved; misses go to the client |
| **PR5** Reports | ESC, PB, ALARM, REP | Lead's discretion; PB blocks only playtest builds |

**Rules:**
- **One lever at a time** (HR already says so). After each lever, rerun every gate in its blast radius at its N, plus every PR0 and PR1 gate as a nightly sample. A lever that turns any higher-rung gate from PASS to FAIL is reverted and goes to the lead.
- **Inside PR1:** the cap decision is fixed first. After it, a lever for one PR1 gate may not turn another PR1 gate to FAIL. If no lever works, both reports go to the client.
- **A lever shared by two owners** (for example EP 11.3's and DH 18.2's F1 ambush and lair budgets) is applied once, by the owner higher on the ladder, and the blast radius is rerun before the second owner acts.
- **After any CMP lever,** the DH 18.2 rows are re-read and reported, never re-levered silently.

**Known opposing pairs:**

| Pair | Resolution |
|---|---|
| `FloorTimeByPartySize` vs DH 18.2 win rate ≤ 1/3 | PR1 wins; CMP lever 3 is guarded by `Level10BeforeFinalBoss`; DH goes to the client after its enemy-budget lever |
| `Floor1ClassSpread` vs `F1DeathShare` | Spread first (CL levers are class data); DH F1 levers only if the spread still passes |
| `HealerGap` potion weight vs DH 5.7 supply | DH 18.1 first; the P1 potion peg is never broken |
| Cap 2 vs `HealerGap`/`HealIsolation` | The intra-PR1 rule; re-read at the shipped cap |
| "Loot last" (LE-24) vs IR PB rows | IR fixes are content changes judged on IR rows; DH rows re-read; DH levers never use loot |
| ROOM fun floors vs `HazardPushDeaths` | Safety wins |
| F1 `BossDeathShare` vs `BossMinutes` | BR's lever order; never boss HP first |
| `PatrolFightRate` vs `Seg10ClickDelta` | A miss fires XI 9.5's cut order, map usability last |
| `LauncherRedrawRate` vs freshness | R-SEED-6 order and the 8-redraw cap |
| `TakeTimeShare` vs `TrapLockSeconds` | The clock line wins |
| Windfall nab band vs `WindfallGreedDowns` | Greed downs (agreed) win |
| Graces SHIP vs `CuriosityFreshShare` | SHIP first; freshness re-read on the fallback deck |
| ARC novice row vs `Floor1ClassSpread` | Spread re-read after every ARC lever |
| Two Doors gate 5 vs DH 18.2 CAL | Doors stay off until it passes; the CAL report is delivered either way |

**Blast radius by lever family:**

| Lever family | Rerun |
|---|---|
| Enemy budgets and boss rows | DH 18.2, `Floor1ClassSpread`, ThemeParity, ClassThemeSpread, BossParity, F1 `BossDeathShare`, `BossRoundsByFloor`, `FloorTimeByTheme`, `XpPerRisk`, `LevelAtDoorByFloor` |
| F1 ambush and lair budgets | `F1DeathShare`, `Floor1ClassSpread`, `SurpriseDeathShare`, `AmbushDeathShare`, `EventDeathShareDelta`, `PartySurpriseShare` |
| Class data | `Floor1ClassSpread`, `HealerGap`, `HealIsolation`, reliance rows, ClassThemeSpread, `ClassBossContribution`, the novice row, camp share |
| Potion weight and supply | `HealerGap`, `RestsPerFloor`, DH 18.2, `CampCoverage` |
| CMP levers 1–6 | `FloorTimeByPartySize`, DH 18.2, `Level10BeforeFinalBoss`, CMP 11.4, `HealerGap`, `HealIsolation` |
| Loot decks | IR PB rows, LE `kPC`, `HeroDreamsDueShare`, `LootTripWire`, DH 18.2 |
| Hazard and terrain cuts | `FloorMinutesDelta`, `HazardPushDeaths`, `EnvKillShare`, `VoidClassDeathGap`, `TeeteringUptimeOnBosses` |
| XI self-cuts | Seg10 rows, `PatrolFightRate`, `MapSecondsPerFloor`, `MarksPerScreen` |

### 6.10 Report-registry fixes (change SV-24; lead's call; M2)

| Problem | Fix |
|---|---|
| `ClassThemeSpread` means two things (RS 11.4 vs HZ) | HZ's becomes **`VoidClassDeathGap`** (≤ BASE + 2, unchanged) at **every** site: HZ 1390, 1403, 2732, 2748, 2871, 3303 and the HZ 12 gated row. RS keeps the name |
| `EnvKillShare` (HZ) vs `EnvironmentKillShare` (ROOM) | One id, `EnvKillShare` (HZ owns, FELT 3–8%); ROOM's name becomes an alias |
| `ContactHazardDowns` is measured against deaths | Renamed **`ContactHazardDeaths`** (threshold meaning kept; Model A's re-basing would have changed a gate) |
| `HazardPushDeaths` defined twice | One definition, HZ's all-environment (settled) |
| `SecretFindRate` (XI) vs `SecretFoundRate` (FL, LP) | `SecretFoundRate{lodestring}` |
| `RoundsPerFightDelta` | Keyed family `{segment}`: TL ≤ 0.1, EP ≤ +0.2 (EP's precedent) |
| `FloorTimeByTheme` overloaded | Keyed family `{arm, feature}`: RS ≤ +10% (HOLD); TL Wager taken ≤ 90 s; HZ Dare taken ≤ 60 s; EV first-10; FL normaliser |
| `TallyChaseSeconds`, `ChainPullRate`, `PatrolFightRate`, `ContainersOpened` | One row each; owners LE, FL, EP, LP; the others read |
| Missing thresholds | `LauncherCheckMs` ≤ 5 ms p95 (REP until measured); `V11Fallback`, `GenerationRetried`, `ArenaStampFallback` per 5.6 |
| Simulator gates inside validators | V12's simulator gate, V14's 1,000-seed gates and V17s (k) become registry rows (SHIP or REL) with their owners |
| V13g (f) `NoMechanicsLootLoss` is a by-class rate | TL's REP row, unless LE defines a per-floor quantity (request) |
| Launcher bias (new) | **`LauncherF1Neutral`** (L; REL; completed after the check): every F1 boss share and every Rendition share under the launcher lies within **ε = 2 points absolute** of bypass, read as a paired Newcombe equivalence interval (both bounds inside ±2) over 200 sandbox profiles × 50 runs per arm (6.5). **The fallback, applied only when the ε test fails:** the aggregate F1 gates are re-read weighted by the LauncherEmu marginals, and sign-off blocks only if the reweighted gates fail. The simulator bypasses the launcher, so F1 gates must not be measured on a different F1 mix from the one players get |
| Two Doors | **6 gates plus gate 7** for the Last Hollow choice (RS 1003–1050), corrected throughout |
| New rows | `RetryRescueRate`, `DegradedRunShare`, `TimeModelError`, `FloorClockLedger`, `DrawLedgerPerRun` (two figures, 8.1) |
| Charm reports (request CA-18 from the charm abilities design 9.4; pending, the SV owner rules; M2, Bobbin rows M3) | Register the charm abilities design 9.4 reports (catch, missed and wasted rates, bind rates per wearer-fight, the CG1–CG3 rows, the Bobbin win delta and pick share), each naming its source (sim, human, telemetry) and its comparison arm (6.2) |

### 6.11 Human-session plan and hours (change SV-27; lead's call; M2)

One playtest calendar covers every H gate. A session may serve several gates, but **never the CMP gate together with a suggestions-on arm** (CMP 7.4).

| Gate | Sessions or reads | Hours *(illustr.)* |
|---|---|---|
| CMP `FloorTimeByPartySize` | ≥ 12 per cap arm (20 recommended); forced party 4; prepared saves at F4; shipped defaults | 25–40 |
| Blind party-3 vs party-4 | within the CMP slice | — |
| Door pick rate (Two Doors behind a flag) | ≥ 150 offers per theme → about 225–300 runs reaching camp 2 | **150–300** |
| Per-build coverage | shared with door sessions | — |
| Readability | ≥ 100 reads per theme | 20–30 |
| `FormationRead`, glance test, audio blind-ID, `PhaseReadTime`, intent readability, creation pick rate | ≥ 100 reads per family | 20–40 |
| M1 "is the loop fun?" | as planned | — |
| Outcome-wait feel (FELT; after the check) | M2 sessions on the minimum-spec machine with an HDD; any felt lag at Downs, deaths and reveals is logged beside `OutcomeWaitMs` | within other sessions |
| Camp and hub time (change CH-5 ⚑; camp and hub design 4.5, 9.2) | camp 1 and the first hub visit join the M2 sessions; `CampSeconds`, `CampClicks`, `RunWallClock`, `HubSeconds`, `DeathPathSeconds` read there until telemetry consent exists (6.8) | within other sessions |
| Heirlooms and curses (request HL-28 from the heirlooms and curses design 4.8; pending, the CH owner rules; M3, M4, Later) | first-time lines (the inline content note, the pending offer in Begin again), the "Not yet read: may Cling" chip, Malady readability, `ClingRegret` (human) | within other sessions |

**Told plainly:** human hours, not compute, may be M2's critical path. If door-pick hours fall short, the Two Doors human gates move with the feature to M3, where Two Doors is re-tested at camps 1–2 (and gate 7), which RS 12 already allows; camp 3 is a separate M3 extension once the floor-4 pool is 3 or more (RS 6.11, RS 12; cross-document comparison 2026-11-23, CC-131). No new client question is needed.

### 6.12 Catalogue of gating rows (non-REP; owners' thresholds; full rows live in the registry)

| Area | Gating rows (class) |
|---|---|
| Death and danger | `F1DeathShare` (CAL; feeds SW gate 5); `DeathsWhere`, `WinRate` per healer arm (CAL); `HealerGap`, `HealIsolation`, `RestsPerFloor` (REL); F1 `BossDeathShare` per Rendition (SHIP); `BossDeathShare` F2–5 (CAL); `EventDeathShareDelta` (SHIP, Graces); `WindfallGreedDowns` (SHIP, M3); affix outliers, AR 6.4, TK envelope (REL); `DeathsFromSafe`, `LootTripWire`, CN 19.3 (ALARM); `F1EnvDeaths`, `ForecastLethalSurprise` (BUILD: a structural row and a smoke row each, 6.6) |
| Clock | `FloorTimeByPartySize` (REL, H); FL layout, TL, ROOM+HZ, EP, LP, EV, BR, XI lines (BIND); `MapSecondsPerFloor` (CUT); `FloorTimeByTheme{RS}` (HOLD); `TimeModelError` (SW, 6.7) |
| Loot | IR `DeadRareRepeatRate`, `StepDownPerRun`, `LateFloorGearShare`, `BuildDiversity` (PB); `ProprietorFallbackRate`, `RelaxationStepRate` (BUDGET); V14 1,000-seed gates (SHIP/REL); Windfall rows (SHIP, M3) |
| Encounters | `PartySurpriseShare` (BIND); `PatrolFightRate` (REL); `FormationFallbackRate`, `V12FloorFallback` (BUDGET); `FormationRead` (H); intent readability (REL); `Level10BeforeFinalBoss` (REL; CMP lever 3 is reverted while it fails) |
| Hazards, terrain, traps | `Floor1ClassSpread` with cover (REL); Void package rows incl. `VoidClassDeathGap` (SHIP); `V11Fallback` (BUDGET); Trapwright rows (M3 SHIP); `OpeningSpringRate` (ALARM) |
| Events | `CuriosityFreshShare`, `StrandCashRate` (SHIP, L); `CuriositySeconds/Clicks` (BIND); `GraceExpiryRate` (ALARM) |
| Bosses | F1 per Rendition (SHIP); `BossMinutes` (BIND); BossParity (SW, gate 7); `ArenaStampFallback` (BUDGET); Gambits (SHIP, M3); `MidBossRestRate` (ALARM) |
| Information | Seg10 deltas (BIND); `MarksPerScreen` (REL, V); glance test (H); `SoloListenClickShare` (ESC, M3) |
| Classes and companions | `Floor1ClassSpread` (REL); ARC skill-expression row, HR reliance, CL reliance (ESC) |
| Route and variety | ThemeParity, ClassThemeSpread, door pick, Oracle, coverage, gate 5 (SW); random-floor ThemeParity, ClassThemeSpread (ii), readability (HOLD); the Wake (SHIP); `DistinctnessCheck` (ESC); `LauncherLootNeutral` (BUILD test); `LauncherF1Neutral` (REL, L) |

---

## 7. Sign-off checklists (change SV-29; lead's call)

### M1: vertical slice ("Is the loop fun?")

**Seeds**
- [ ] SV-1 key algebra and the 4.2 path grammar frozen; `KeyAlgebraVectors` pass on two OSs and under a checked-arithmetic debug build; the analyzer (DS11, with the `unchecked` key-algebra file) and DS13 loader green; `CanonicalWriter` and the SHA-256 digest locked with their `FloorStatic` vector.
- [ ] Streams registered: `route` stub, `floor`, `attempt`, `layout`, `dressing` (display side), `terrain` (with `hzTagMax` reserved), `traps` (+`retry1`), `encounters/plan`, `encounters/place` (+`retry1`), `trapsResolve` (+`retry1`), `checks` (`h:object`, `h:save`, `h:notice`, `h:steal`; `h:lurkStealth` **reserved and inert**, live in M2), `combat`, `loot/floor` blocks, `lootPlan` (`lineRoll` reserved and inert), `loot/run` key.
- [ ] **Owner deliverables:** TL traps and trapsResolve formulas; HZ `hzTagMax`; LP's V15 M1 rows and the HE/Due binding proof.
- [ ] The 4.5 locks; IR filter-first; DH reshuffle; SV-5 and SV-8 applied by their owners, or their fallbacks adopted (14.2).
- [ ] Pipeline GS0–GS17 (M1 stages); AP1 and AP2 with act-then-assert; LE-14's atomic commit.
- [ ] Ladder LR0–LR3; LR4 for the V11 and V12 M1 rows; one ladder in every build (`GenMaxAttempts`), LR5 a hard failure in dev and CI after the output is recorded; `DevReleaseParity` green.
- [ ] Reproducibility key with `genVersion`, canonical `manifestHash`, `displayHash`, profile-grant fields reserved.

**Validators (BUILD)**
- [ ] V8 core; V9 partial; V11 skeleton; V12 static + chain; V13s (a)(c), V13g (g)(i) on F1, (m); V14 (a)(b)(d)(h)(j) + draw test; V15 M1 rows; V16 stub; V17 subset; V18 tests incl. `KnowledgeLeak` and `FogEqualsRules`; V19s, V19g and the M1 V19t rows (incl. `ExplorationCombatIsolation`, `AP1EarlyEquivalence`, `RetryIsolation`, hostile environment); V21 (a)–(e), plus (f) if the Due is in the slice (change SK-10; starting kits design 9.2); V22 registered with only its (m) `RunModifierIdInConfigHash` field row live (change HL-14; heirlooms and curses design 9.2).
- [ ] Content suites green; CL M1 checks; `Floor1BossSustain` (Vanguard, BUILD).

**Data and runs**
- [ ] M1 goldens (16 F1 fixtures + 4 replays) recorded after the preconditions; PR digest gate live.
- [ ] M1 release set (F1 only): `GenerationRetried` = 0; LR4 within budget.
- [ ] Run save with `runId`, `FloorStatic` and `GenInputs` (F1 with the hero-creation inputs); `SaveDigestMatch`; the log and replay checkpoints.

**Simulator**
- [ ] M1 skeleton runs F1 smoke; DH 21.5 rows printed (**reports only**); the four measurements taken on the M1 stages and **filed as a measurement report against the 8.7 assumptions** (6.1).
- [ ] CI PR and main tiers, and the minimal M1 nightly (5.8).

**Human**
- [ ] The M1 playtest.

**Not M1:** any REL, SW, HOLD or SHIP verdict.

### M2: full run ("Does a 1–2 hour run hold together?")

**Internal sequence.** Each step's gates must pass before the next starts.
1. **Planning gate:**
   - the Shade DEP-S1 call (done);
   - the graded cuts review (1a, 1b, Cut 2);
   - the EP clock re-measure plan;
   - **`Skill.Novice/Basic/Expert` adopted, or per-class novice scripts delivered** (blocks the Graces decision);
   - the zero-gate partners owed by IR, HZ and LP;
   - the human-session calendar (6.11);
   - **the measurements completed (after the check):** M2 stage times and ρ per M2 retry re-measured; ρ frozen (5.6); 8.2 and 8.7 recomputed from the measured values; the 6.5 spread N recomputed (6.4 rule 4); the tiers resized, with named fallbacks if a tier no longer fits (nightly: move FsCheck 10k to weekly; sign-off: add cores, or split the matrix over two runs on one frozen `simRoot`); `gen.budget.*` and the CI budgets frozen.
2. **Cap decision:** `TimeModelError` ≤ 10% or human attribution (6.7); `FloorTimeByPartySize` (the first M2 gate, human); the blind party-3/4 arm; then CMP levers in order, or cap 2 (pre-authorised).
   - **2b:** re-read every REL gate at the shipped cap.
3. **F1 balance freeze:**
   - SHIP: F1 `BossDeathShare` per Rendition; the Wake (read once); `EventDeathShareDelta` (the Graces decision).
   - REL: `Floor1ClassSpread` F1 rows in every split.
   - ESC: the Arcanist novice row.
   - CAL: `F1DeathShare` read as a report.
4. **Full balance pass:** the remaining REL, HOLD, SW, SHIP and BIND rows in precedence order (6.9).
5. **Sign-off.**

**Checklist**
- [ ] **Streams:** `hazards` (formula published), `curiosities` (12 locked, `vector`), `boss9` (`vector`), `recruit/*` (per entry), `h:lurkStealth` live, `h:envSave` (falls; Plunge is M3, CC-97), `h:curiosity`, display keys, `sim`.
- [ ] **Seeds:** SV-4 `ForecastSurvivesRetry` (identity and presence; `F1HostAlways`); SV-8 keyed sorts (in force from the M1 goldens); speculation at camp with `SpeculationEquivalence`; shared seeds and codes with the agreed Q1 progress rule, `runHistory[]` and `SeededRunWritesSeenOnly` (SV-16); `genContentHash`, the 5-character build tag, the two-tier notices and `ShareCodeErrorDetect`.
- [ ] **Saves:** WO1–WO8 with `runId`; the agreed Q4 slot mechanism (`suspend.base` plus `suspend.journal`, atomic replace) with the platform shim; `SlotEquivalence`, kill-fuzz, `LoadPathKillFuzz`, `ProbeResistance` (floor and camp commands), the overlap budget and `OutcomeWaitMs` on the minimum-spec runner; the agreed Q3 patch rule with its notice and marks, and `SaveMigrationGoldens` with uncompacted journals.
- [ ] **Launcher:** R-SEED-6, Fallow and the boss rule; LauncherEmu with sandbox profiles; `LauncherF1Neutral` at ε = 2 points (or its reweighting fallback).
- [ ] **Ladder:** the defect path (≤ 3 attempts in every build, then the agreed Q2 clean end in release with its record and bug report, and the F1 run-start case) with `UnravelPath` green; every 5.6 budget met on the frozen M2 release set; `ArenaStampFallback` = 0; `LootBindUnplaceable` on HE/Due = 0.
- [ ] **Validators:** V1–V18 full except M3 rows; V19 full; V20 M2 rows (change CH-25; camp and hub design 9.2); V21 M2 rows (g)–(j) (change SK-10; starting kits design 9.2); V22 (e) rule rows (change HL-14; heirlooms and curses design 9.2); V23 M2 rows, V23s and V23r on the M2 roster and the shadow records (change CA-18; charm abilities design 9.3); registries complete, with no duplicate ids, no gate missing a threshold, instrument, N or sidedness, and every `g` and `r` check classed (`ClassDeclared`).
- [ ] **Goldens and release set:** M2 goldens by set cover; the release set frozen (`RS-M2.v1`).
- [ ] **Gates:**
  - REL: `FloorTimeByPartySize` ≤ 20 min; `Floor1ClassSpread` ≤ 10 in every split; `HealerGap` ≤ 10; `HealIsolation` ≤ 10 per M2 Herald subclass; affix outliers; intent readability; AR 6.4; TK envelope; `RestsPerFloor` ≤ 3; `PatrolFightRate` ≥ 60% of `XI.BASE`.
  - HOLD: every shipped theme passes ThemeParity (random floors ≤ 8), ClassThemeSpread (ii) ≤ 10 (Heart and finals included), `FloorTimeByTheme` ≤ +10%, readability (85% / 75%); otherwise listed `held`.
  - SW: Two Doors gates 1–6; gate 7 for Last Hollow; each flag's state recorded.
  - SHIP: HZ Void and Dream packages; Graces; pilot Strands; F1 Renditions; the Wake; each state recorded.
  - BIND: every line inside, or its self-cut applied; `FloorClockLedger` published.
  - PB: IR rows green.
  - ESC: every escalation has a logged lead decision.
  - CAL: the DH 18.2 report, the BR F2–5 bands and the LE trip-wire delivered to the client.
- [ ] **XI pre-M2 re-runs:** pre-stop cost; never-Seen gate; travel; cut steps; `MarksPerScreen` (V).
- [ ] **CI:** nightly, weekly, the per-OS jobs on three OSs and two architectures; the minimum-spec reference runner with the runtime budgets as BUDGET rows.
- [ ] Every report printed with N, interval, verdict and `simVersion`, at both caps.

### M3: between runs ("Do players come back?")

- [ ] Unlock snapshot live; V5 enumeration as written; omen indices; `boss9` indices 2–3; Sequel drawable sets, and Ripening drawable sets **if Ripening ships in M3** (agreed "M3-late or M4"; otherwise at M4; cross-document comparison 2026-11-23, CC-85); profile grants (`difficulty.tier`, `boons[]`, `nightmareTier`) live in the key.
- [ ] `windfall`, `wager`, `twinCoffer` grandchildren; `h:envSave` Plunge and Dare (cross-document comparison 2026-11-23, CC-97); `h:windfallSteal`; bossMemory (display).
- [ ] SHIP: Trapwright gates; Gambits (`GambitDodgeRate` ≥ 70% on `Skill.Novice`, `GambitStayRate`, BX8), else Later; Windfall pilot with the solo arm; Wager and Dare taken arms; the V14 (c) Rare switch; the Dream map skin gates; Graces if they missed M2.
- [ ] SW: Two Doors re-tested at camps 1–2 (and gate 7) if it failed in M2, with the same 6 gates; camp 3 is a separate M3 extension once the floor-4 pool is 3 or more (RS 6.11, RS 12; cross-document comparison 2026-11-23, CC-131).
- [ ] REL extended: `Floor1ClassSpread` and F1 guard rows per Nightmare tier; F1 Rout rate with the Horn Post; heal isolation for Warcaller and Pennant.
- [ ] `CuriosityFreshShare` at 30 and 50 runs; `BossRepeatRate` with bossMemory; V13s (o); V15 Windfall rows; V16 Sequel rows, and the Ripening rows **if Ripening ships in M3** (else M4; CC-85); V17 Gambit rows; V21 (k)–(l) (change SK-10; starting kits design 9.2); V23 M3 rows (change CA-18; charm abilities design 9.3); V10 `TideStatesProven`; V9 `TippableExcluded`; `SoloListenClickShare` at the M3 review.
- [ ] DH 18.2 rows **reported** per difficulty tier (14.2).
- [ ] M3 goldens re-blessed with a `genVersion` bump; the release set re-frozen.

### M4 and Later

- **M4:** Cathedral validators and rows; tuning-only rows; species in `GenInputs`; V5 prefix policy plus a V5t sample if unlocks are confirmed prefix-ordered.
- **Later:** daily seeds; the trader (`floor/{N}/trader` and the loot `trader` block); `curseGate` in use (M4 with item curses by change HL-29; Later again if the conditions owner refuses its budget reading); `pledge`; leaderboards (a server); rendezvous picks for template lists.

**Drift watch (from M2; rewritten after the check; lead's call, after the check).** The draft re-sampled every passed gate nightly at 10% of N and marked a sign-off stale whenever the sample's interval crossed the line; at 10% of N the interval is about 3.2× wider, so a gate signed off just inside its line would have gone stale nearly every night. Now:
- Every passed gate is re-sampled **weekly at 25% of N** (no nightly simulator drift sample; 5.8).
- A weekly sample **alarms its owner (ALARM) only when its whole interval lies on the failing side** of the threshold.
- The weekly samples **pool on a rolling four-week window**; once the pool reaches full N, the gate is re-read by its own class rule (6.4). **Only a FAIL of that full-N re-read marks the milestone's sign-off stale.**
- **Stale clears** when a fresh full-N read (the rolling pool or a dedicated rerun) passes by the class rule.
- A change to player models bumps `simVersion` and restarts the pool.

---

## 8. Numbers with fixes (toy and estimates, not simulator; all *(illustr.)* or assumptions marked A)

### 8.1 Draws per floor and per run (padded maxima)

Assumptions: room nodes from FL 6.4; maxNodes = rooms + alcoves + 2 (A); maxEdges = maxNodes + 5 (A); Sites 6; maxSockets 16; TL and HZ formulas modelled (A).

| Floor | layout | terrain | hazards (A) | traps (A) | encounters | trapsResolve (A) | curiosities | lootPlan | lineRoll address space | M3 kinds + curseGate | parent blocks | Total |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| F1 | 111 | 569 | 80 | 280 | 44 | 112 | 12 | 270 | 432 | 51 | 242 | 2,203 |
| F3 | 126 | 668 | 92 | 325 | 52 | 130 | 12 | 270 | 432 | 51 | 242 | 2,400 |
| F5 | 101 | 503 | 72 | 250 | 50 | 100 | 12 | 270 | 432 | 51 | 242 | 2,083 |

- Per run: about 11,300 draws, or about 14,800 including the recruit address space.
- RNG CPU time is about 30 µs per run, which is negligible.
- **SN1:** IR's "about 1,300 reserved draws per run" is plausibly the **spent** count; the loot **address space** alone is about 4,500. `DrawLedgerPerRun` publishes both. Under DS7, neither is a stability requirement.
- **SN2:** the ledger cannot be computed until TL (M1) and HZ (M2) publish their formulas; both are checklist deliverables.

### 8.2 The ladder: rates and generation time

Per-pass terminal rate = p·(ρ + (1−ρ)·p), with ρ the correlated-failure share (5.6).

| Scenario | Floor LR4 | Runs with ≥ 1 degraded floor |
|---|---|---|
| Docs as stated (EP p = 10%, ρ = 0.3, A) | 5.1% | 23% |
| Drafted "LR2 ≤ 2% per stage", ρ = 0.3 | 2.9% | 13.6% |
| Independent retries, ρ = 0 | 0.16% | 0.8% |

- **SN3:** "≤ 2% per stage" and "total ≤ 1%" cannot both hold at ρ ≈ 0.3. Fixed by per-pass LR4 budgets, `RetryRescueRate` and the per-run budget (5.6).
- **SN4:** per-cell budgets cannot be read on the release set (about 110 floors per cell). Fixed by pooled reads on the release set. **Amended after the check:** they cannot be gated per cell on the 10^5 soak either (about 1,300 floors per cell, where a ≤ 0.3% budget passes only with zero events), so BUDGET gates read only the pooled figures per (floor, cap), and per-cell soak reads are ALARM (5.6).
- **Generation time** (stage costs A; 200k Monte Carlo floors, lognormal σ 0.35): base pipeline about 144 ms; p50 146 ms, p95 192–230 ms, p99.9 285–304 ms. Retry spans: traps 14 ms, encounters 86 ms, trapsResolve 36 ms, dealShuffle 36 ms.
- **Worst case:** 2 attempts 633 ms; 5 attempts 1.58 s (**fails 1 s**); at **3 attempts about 0.95 s** (passes). **SN3, second part:** attempts capped at 3 in every build (`GenMaxAttempts`, 5.5), with speculative generation during camp and a synchronous rebuild at Travel on when the party changed (4.4); the 0.95 s worst case is what a rebuild behind the loading beat costs. (The draft called this "SN5a"; folded into SN3 after the check so the scheme stays SN1–SN16.)

### 8.3 Static validators and build cost (single core; ms per case A)

| Validator | Cases | core-s |
|---|---|---|
| V17s stamp proofs | 447 | 3 |
| V17s (k) as approved | 134,400 fights | 3,360 |
| V17s (k) after BN14 | 18,278 | 457 |
| V8s per-Site | 1.59M | 477 |
| V8s interacting | 188k | 57 |
| V9s | 43,200 | 22 |
| V10s | 311k | 156 |
| V11 property test (N = 50, full generation) | 9,200 floors | 1,380 |
| V12s | 1.1M (EP) | 550 |
| V18t `LosSymmetric` | 30M pairs | 61 |
| V5 M3 every subset | 221k | 11 |
| V5 M4 every subset | 7.1M | 354 |

- M2 cold total about 3,170 core-s: **8.8 min on 8 cores (fails ≤ 5 min)**.
- **SN5:** V11 property rows run on generation truncated at AP1 and reuse the PR DrawLedger fixtures; V17s (k) moves to the simulator tier with BN14. The cold total falls to **3.7 min**. Warm PR: about 26 s typical, about 2 min for an ENM-wide edit.
- **SN6:** V5 "every subset" costs 11 core-s in M3, so the drafts' M3 contest of RS V5 is withdrawn; revisit at M4.

### 8.4 Simulator N, intervals and compute

| Item | Floors simulated |
|---|---|
| DH 18.2 + `HealerGap` | 40,000 |
| `HealIsolation` | 80,000 |
| `Floor1ClassSpread` (observed rule) | about 103,000 |
| ThemeParity / ClassThemeSpread (observed rule) | about 1.21M |
| BossParity (observed rule) | about 220,000 |
| F1 `BossDeathShare` | 9,000–36,000 |
| `BossDeathShare` F2–5 | 32,000 |
| `EventDeathShareDelta` | 30,000 |
| `PatrolFightRate` | 4,000 |
| CMP Muster arms | 150,000 |
| Feature baselines | 64,000 |
| Two Doors Reference/Random | 40,000 |
| Oracle door chooser (fixed: N 1,000, 8 single-floor rollouts) | about 64,000 |
| CMP Oracle Muster (8 rollouts) | about 80,000 |
| **Total** | **about 2.1M** |

- At 300 ms per floor (central A): about 175 core-h, **7.3 h on 32 cores**, inside the sign-off tier (≤ 24 h). At 1 s per floor: about 24 h, at the limit; the M2 planning gate's named fallbacks then apply (7).
- **After the check:** re-sizing the rule-2 rows at z = 2.18 (6.5) adds about 50,000 floors *(illustr.)*, inside this table's rounding; the spread rows are recomputed from measured variance at the M2 planning gate (6.4 rule 4).
- **SN7:** the drafted Oracle arms were 66% of compute; fixed as above.
- **SN8:** gating spreads on the observed value at noise ≤ 1 point costs about 1.2M extra floors over the draft. That is the price of keeping client numbers as set (6.4).
- **SN9:** EV's 10-profile streak arm can never decide its gate; 200 profiles (6.5).
- **SN10:** CMP's human median at 12 sessions has a wide interval; 20 sessions recommended, with the interval printed.

### 8.5 Goldens and the release set

- **SN11:** 64 random golden runs cannot cover Blueprint × transform (set cover needs 73; random sampling about 408). Fixed: about 80 by greedy set cover. Cost: about 1 MB of goldens and about 50 s per run of the suite.
- **SN12:** a 2,000-run release set split over party cells gave about 1,000 F1 trials per cap, under DH 18.2's 1,881. Fixed: 2,000 runs per cap, plus generation-only fixtures and logged extensions.

### 8.6 Saves

- Slot about 67 KB; `FloorStatic` about 73 KB per floor; `run.save` with 5 floors about 430 KB.
- Full write, flush and rename: NVMe 3.3 ms; SATA SSD about 8 ms; HDD 24–50 ms. About 3,000 writes and about 206 MB per run.
- **SN13:** the drafted 4 ms SSD / 16 ms HDD budget is unachievable off NVMe. Fixed by the overlap budget and the delta journal (4.8.4; agreed, Q4): about 3 MB per run.
- **SN14:** a command-replay journal breaks across an auto-update. Fixed by journalling state deltas.

### 8.7 CI wall time

| Tier | Cores | Wall | Budget | Result |
|---|---|---|---|---|
| PR | 8 | 6.6 min | ≤ 10 | pass |
| Main | 8 | 12.1 min | ≤ 30 | pass |
| Nightly | 16 | about 80 min (breakdown below; the draft's 88 min included a drift sample whose own cost was about 66 min, so the draft's sum was short) | ≤ 2 h | pass (**SN15:** on 8 cores it fails; use 16 cores, or move FsCheck 10k to weekly) |
| Nightly per-OS jobs | own runners | ≤ 30 min each | ≤ 30 min | parallel; not in the 16-core budget |
| Nightly minimum-spec runner | 1 | ≤ 60 min | ≤ 60 min | parallel |
| Weekly | 32 | about 3.6 h (3×10^5 soak + the 25% drift sample, about 1.4 h, + the 3,000-run zero-gate smoke, about 1.3 core-h) | ≤ 8 h | pass |
| Sign-off | 32 | about 7.3 h + 10^6 soak (about 1.8 h) + the 30,000-run zero-gate smoke (about 0.4 h) + kill-fuzz at 20,000 offsets | ≤ 24 h | pass |

**Nightly breakdown (16 cores; all A, added after the check):**

| Component | Wall |
|---|---|
| GS1–GS17 + every `g` over the release set's run floors (about 20k), the generation fixtures and the 10^5 soak (about 2×10^5 floors at about 144 ms) | about 30 min |
| Pooled budgets, the digest manifest, `SaveDigestMatch` on the release set | about 5 min |
| FsCheck at 10k cases | about 10 min |
| GenStat and Walker reports | about 15 min |
| Hostile-environment run (goldens in fresh processes) | about 8 min |
| Kill-fuzz (2,000 offsets), `LoadPathKillFuzz`, `ProbeResistance`, `DevReleaseParity`, `UnravelPath` | about 12 min |
| **Total** | **about 80 min** |

**SN16:** the four unmeasured numbers (simulated time per floor, stage times, ρ per retry, fsync latency) swing these estimates by up to 20×. They are measured on the M1 skeleton before the M2 budgets are frozen; the M2 stages' values are completed at the M2 planning gate, which recomputes this table and resizes the tiers (6.1, 7).

---

## 9. Data and tooling

### 9.1 Registries (source of truth under `content/registry/`; owner per row)

- **`StreamRow`** `{path (display only), alias[], parent, label (normative), indices[] (normative; each {name, type: ulong|uint|labelId}), class: node|fixed|fixed-per-data|leaf-formula|per-entry|vector|keyed|sequential-saved|display|sim, countFormula, length?, cap?, stage, readers[] (for a vector), consumers[], retryScope: never|floorKey|attempt|stage, rules[], keyType: core|display|sim, owner, source[], wave}`
- **`StageRow`** `{id: "GSn", owner, reads[], writes[], streams[], drawFree, digest, retrySpan}`
- **`CheckRow`** `{id: "Vn.Name", owner, phase: s|g|r|t, point?, deps[], uses[], class, action, rerunSpan?, proofRef?, wave, ciTier, budgetMs?}`
- **`ReportRow`** `{id, family?, keys[], owner, definition, unit, instrument: S|W|L|V|G|H, arms[], class, threshold, thresholdStatus: agreed|settled|lead|illustr, sided: one|two, z, stat, N, wave, ciTier, leverOrderRef?}`
- **The AP2 act order** is not a registry field: it is a code-fixed ordinal (5.4).
- **`ArmRow`** `{id: "Axis.Value", owner, configDelta}`
- **Charm rows (request CA-18 from the charm abilities design 9.3, 9.4; pending, the SV owner rules; M2).** The charm `ReportRow`s (6.10) and `ArmRow`s (6.2) join the registry once ruled; V23's `CheckRow`s register with V23 (5.2).

Source generators emit `StreamId`, `StageId`, `CheckId`, `ReportId` and `ArmId` handles. V19s compares the code registrations with the files.

### 9.2 Records

- `GenInputs` (4.8.2).
- `FloorStatic {floor, attemptUsed, static output, stage digests, GenerationLog}`.
- `SaveHeader {saveVersion, rulesVersion, genVersion, manifestHash, genContentHash, configHash, unlockHash, runSeed, runId, seedMode, comparable, genVersionsUsed[], result, seq}`.
- `SlotBaseHeader {runId, floor, baseSeq, saveVersion, crc}`; `JournalRecord {runId, baseSeq, seq, saveVersion, crc, delta}` (4.8.4).
- `ReplayHeader {key, rulesVersion, saveVersion, heroInputs}`.
- `ShareCode {seed60, check (GF(32), over seed and preset), configPreset, buildTag (4 generation characters + 1 rules character)}`.
- `RunHistoryEntry {runId, seedMode, result, floorReached, code, comparable}` (profile `runHistory[]`, 4.7).
- `PendingProfileWrite {runId, kind, payload}`.

### 9.3 Events

| Event | Status |
|---|---|
| `GenerationRetried{floor, attempt, check}` | existing |
| `StageRetried{floor, attempt, stage, check}` | new |
| `GenerationFallback{floor, fallback, check}` (V11Fallback, V12FloorFallback, LootBindDegrade, ArenaStampFallback) | new wrapper; existing names kept |
| `GenerationDefect{floor, attempt, check}` | new, P0 |
| `RunUnravelled{floor, attempt, check, seedMode}` | new, P0 (the Q2 clean end, agreed) |
| `RuntimeAssert{check}` | new, P0 |
| `DrawLedgerMismatch{path, expected, actual}` | new; `defect` class (LR5) in every build |
| `SpeculationDiscarded{floor, candidateDigest, finalDigest}` | new (after the check); REP, not an error |
| `OutcomeWait{commandType, ms}` | new (after the check); feeds `OutcomeWaitMs` |
| `SaveDigestMismatch{stage}` | new; CI and debug |
| `SeededRunStarted{seedMode, codeValid, buildMatch}` | new |

### 9.4 Configuration (builders' values unless marked)

| Key | Value | Notes |
|---|---|---|
| `seed.bits` | 60 | lead's call |
| `gen.version` | integer | a key element |
| (code constant, not configuration) `GenMaxAttempts` | 3 *(illustr.)* | the Q2 path (agreed); the count stays illustrative; covered by `genVersion` (replaces the draft's configuration key `gen.releaseMaxAttempts`, after the check) |
| `gen.budget.*` | 5.6 | CI |
| `hzTagMax` | HZ declares | before the M1 goldens |
| `curiosity.drawCount` | 12 *(illustr.)* | the `vector` length; locked before the M2 goldens |
| `save.overlapBudgetMs` | 120 *(illustr.)* | |
| `save.journalCompactEvery` | 50 *(illustr.)* | the Q4 journal (agreed); the interval stays illustrative |
| `replay.checkpointEvery` | 64 *(illustr.)* | |
| `golden.{m1, m2}` | 16 + 4 / about 80 + 16 + 8 *(illustr.)* | |
| `releaseSet.runsPerCap` | 2,000 *(illustr.)* | |
| `launcher.checkMsMax` | 5 *(illustr.)* | launcher-only |

**Client keys kept:** `launcher.f1BossMemory` 2, `curiosity.fallowK` 3, `fallowFloors` [1], `save.suspendSlots` 1, `deleteOnLoad` (met by atomic replace, agreed Q4), `knowledgeForwardOnly`.

**Client rules, not configuration (agreed, Q1–Q4; new lead's call):** seeded runs advance no progress; the release clean end; the mid-run patch rule; the snapshot-plus-journal slot, atomic replace and the outcome rule. No configuration key may switch them.

### 9.5 Tooling

- The Roslyn analyzer (DS11).
- The Core content loader (DS13).
- Source generators for registry handles.
- The DrawLedger and the stage-digest service.
- The draw tracer (for `ToggleIsolation` and divergence location).
- The save writer (.NET file APIs) with its platform shim for durable flush and atomic replace (4.8.4), outside Core's determinism analyzer.
- `CanonicalWriter`, the one canonical binary serialiser for digests and deltas (4.5).
- FsCheck generators over configuration vectors.
- The proof cache keyed by declared read sets.
- The dashboard.

---

## 10. Staging by wave

| Wave | Seeds | Validation | Simulator |
|---|---|---|---|
| **M1** | SV-1 (with the path grammar, `CanonicalWriter`), SV-2, SV-3, SV-4 (core), SV-5, SV-6 (object, save, notice, steal; lurkStealth reserved), SV-7, SV-8 (rule; route is a one-theme stub), SV-9, SV-12 (fields reserved), SV-14 (run save with `runId`, `FloorStatic`, log), SV-18 (M1 goldens, F1 release set) | SV-10 (LR0–LR3, LR4 M1 rows, one ladder in every build, LR5 hard failure in dev and CI), SV-11, SV-19 (partial), SV-20 (model), SV-28 (PR, main, minimal nightly) | SV-21 skeleton (reports only; four measurements on the M1 stages) |
| **M2** | SV-4 (curiosities), SV-6 (lurkStealth, curiosity, envSave falls), SV-13, SV-14 (slot files, WO1–WO8, agreed Q4), SV-15 (agreed Q3), SV-16 (codes, `runHistory[]`, agreed Q1), SV-17 (shared), SV-18 (M2), SV-30 | SV-10 (the release clean end, agreed Q2), SV-19 (full), SV-20 (complete), SV-28 (nightly, per-OS jobs, minimum-spec runner, weekly, sign-off) | SV-21–SV-27 in full; the measurements completed at the planning gate; the M2 internal sequence (SV-29) |
| **M3** | profile grants live; omen; boss9 indices 2–3; windfall, wager, twinCoffer; envSave Dare; windfallSteal | M3 validator rows | per-tier reporting; M3 SHIP gates |
| **M4** | species in `GenInputs` | V5 prefix policy (optional) | tuning rows |
| **Later** | daily seeds, SV-32 trader, curseGate use (M4 by change HL-29), pledge | — | leaderboards |

---

## 11. What segments 11–12 own, and the hook answers

**Own:**
- the key algebra;
- the stream registry and its rules;
- the pipeline skeleton and the two assertion points;
- the ladder and its budgets;
- the reproducibility key;
- what sits outside the seed;
- shared, daily and replay rules;
- save file mechanics;
- seed display and entry;
- versioning;
- golden seeds and the release set;
- V19 `Conformance`;
- the validator phase model and the check, report and arm registries' schemas;
- the simulator plumbing, its instruments, result classes and statistics rules;
- precedence;
- CI and time budgets;
- the sign-off checklists.

**Do not own:** any stream's consumers or counts (owners register them), any check's logic, any report's threshold, any lever.

| Hook source | Answer |
|---|---|
| MAIN 6.6 / XI-28 (file mechanics, write cost) | 4.8 (agreed, Q4) |
| RS 13 (`route`, R-SEED-1–6, `drawableSet`, the key, V1–V7, signature assertion, arms, LootDoorInvariant) | 4.3, 4.4 GS0.2, 4.6, 5.2, 5.7, 6.2, V19t harness |
| FL 13 (layout, dressing, derivation, counts, V8, the assertion list, the shared point, the stream tree, release set, retry logging) | 4.3, 4.4, 4.11, 5.4, 5.5 |
| ROOM 13 (terrain, counts, digest, V9, `PreviewExactness`) | 4.3, SV-3, 5.2 |
| HZ 13 (hazards, V10, digest, save fields, reports, arms) | 4.3, 5.2, 6.10 |
| TL 13 (streams, V11, digest, save fields, reports, `FloorTimeByTheme` rows) | 4.3, 5.2, 6.10 |
| EP 6.19, 13 (`encounters`, `retry1`, `lurkStealth`, retry logging; the CMP-8 tests extended to formations and the solo column) | 4.3, 5.5; `content.CMP8.*` rows (5.2) |
| LE 13 (`lootPlan`, the draw table, 3a and LootBind positions, ghost and dummy draws, atomic commit, V13) | 4.3, 4.4, 4.8 WO5 |
| IR 4.17, 13 (frozen order, keying, filter-first, reserved total) | 4.4 GS10, 4.3, 4.11, 8.1 |
| LP 13 (`windfall`, SocketPlan, step 8, LootBind sub-order, V15) | 4.3, 4.4 |
| EV 13 (`curiosities`, draw map and labels, pass order, resolve safe point, memory, launcher, V16, arms) | 4.3, 4.4, 4.8 WO3, 4.6, 6.2 |
| BR 4.17, 13 (`BossVariantKey`, **template-local coordinates, the event order**, launcher place, V17, the reports and arms (BR 9), release coverage) | 4.3, 4.4 (stamp in template-local coordinates plus transform; event order as a V17t replay row), 4.6, 4.11; reports and arms in 6.2, 6.10, 6.12 |
| XI 4.13 (`PartyKnowledge` and event order in save and replay, UI annex, slot, V18, `KnowledgeLeak`, `LosSymmetric` and treasure coverage, walker rows, human arm, no new stream) | 4.8, 4.11, 5.2, 6.1–6.2; no new stream added |
| CMP 7.4 (time model, clock sum) | 6.7, 6.8 |
| DH 18.2, 21.2 (release seed set) | 4.11 |
| Camp and hub (segment 13; **taken up by the camp and hub design**, approved 2026-11-23: catch-up draws, `CampRest` and the camp WO8 writes, `BuyBatch`, vendor potion stock, speculation restart, the `lore` display domain and `loreMemory`, lore marks and the hub lore write, camp and hub time reports, V20; changes CH-2, CH-3, CH-5, CH-7, CH-8, CH-9, CH-16, CH-18, CH-19, CH-25) | 4.3, 4.4, 4.6, 4.7, 4.8.1, 4.8.3, 5.2, 5.3, 6.8, 6.11; V19t `SeededRunWritesSeenOnly` unchanged |
| Starting kits (segment 14; **taken up by the starting kits design**, approved 2026-11-23: the `run.save` display sidecars, the Oddment's run-start display row and the creation-screen run key, the Oddment mark's write timing, V21; changes SK-5, SK-6, SK-10; SK-7 read (variants in `boons[]`, SV-12); SK-16 a pending request to this design) | 4.3, 4.4 GS0.1, 4.6, 4.7 (SK-16, pending), 4.8.1, 4.8.3, 5.2, 5.3, 7; 14.2 |
| Heirlooms and curses (segment 15; **taken up by the heirlooms and curses design**, approved 2026-11-23: `runModifierId` in `configHash`, Grip-marks in profile display memory, V22; changes HL-5, HL-14, HL-22; HL-6, HL-11 and HL-25 pending requests to this design; HL-28 a request to CH for 6.11 sessions; the `curseGate` reservation read, in use from M4 with item curses (HL-29, not an SV row; Later again if CON refuses the budget reading)) | 4.6, 4.7 (HL-25, pending), 4.8.3 WO6 (HL-6, pending), 5.2, 5.5 (HL-6, pending), 6.11 (HL-28, pending); 14.2 |
| Charm abilities (segment 16, the TK 24.2 pass; **taken up by the charm abilities design**, approved 2026-11-24: the charm per-fight state in the save and the suspend journal, V23; changes CA-11, CA-18 (V23 in 5.2); CA-18's report, arm and minimum-N rows, CA-20 (the Bobbin in `configHash`) and CA-21 (`unlockHash`, `drawableSet`, SQ41) pending requests to this design) | 2.3 SQ41 (CA-21, pending), 4.6 (CA-18, CA-20, CA-21, pending), 4.8.1, 4.8.4, 5.2, 6.2, 6.5, 6.10 (CA-18, pending), 7, 9.1 (CA-18, pending); 14.2 |

**Requests to owners** (request; each with a fallback, listed in 14.2; owed rows again in 13 and 17):
- **TL:** traps and trapsResolve formulas (M1). Fallback: TL's own padded maxima declared as a cap.
- **HZ:** the hazards formula (M2), `hzTagMax` (M1), the F1 environment-death scope, the Void zero-gate proofs, the VP id note.
- **LP:** the V15 M1 rows, the HE/Due binding proof, the default `LootPolicy`; confirm `LootBindDegrade`'s merge-or-coin rule (added after the check).
- **EP:** accept the signature-twice authority and the V12t sample row (fallback V19t); `ProtectedIntact`'s group part; with CMP, confirm the `content.CMP8.*` rows (added after the check).
- **LE:** the `dealShuffle/retry1` key; the V13g (f) quantity.
- **IR:** the Resistant draw's owning entry; the `DormantAtChoice` proof.
- **BR and ROOM:** the stamp before terrain, arena-node dummies.
- **RS:** the pending launcher-exhaustion reading, precisely the clause of R-SEED-6 "if no candidate passed the theme rule, it keeps the original seed" (RS 6.4, marked there as a reading the RS owner confirms).
- **XI, FL and EV:** walker equivalences.
- **ARC:** generalised skill arms (fallback: per-class scripts).
- **CN:** phial always 3 draws.

---

## 12. Binding requirements and how they are met

| Requirement | Source | How met |
|---|---|---|
| One seed; separate streams; a chest never moves the next attack roll | MAIN 6.3 (decided) | The key tree (4.1–4.3); out-of-combat checks, saves, **noticing and Steal** on `checks` (SV-6); V19t `ExplorationCombatIsolation` |
| The core's own generator; results stable across runtime versions | MAIN 6.3 (decided) | SplitMix64 in Core; DS11 bans `System.Random`; cross-OS and hostile-environment tests |
| Integers only; no unordered iteration | MAIN 6.3 (decided) | DS4, DS11, DS13 |
| Route consumed only at run start; never moved by retries, actions or doors | R-SEED-1 | `route` retry scope "never"; `RetryIsolation`; V6 |
| Fixed counts, dummy draws, multiply-shift, sorted lists | R-SEED-3 | Count classes, the DrawLedger, `Bound`, DS4 |
| The walk uses no RNG | R-SEED-4 | Unchanged |
| Reproducibility key; canonical unlocks for shared and daily seeds | R-SEED-5 | Extended (SV-12), canonical unlocks kept |
| Launcher order, ≤ 8 redraws, bypass for shared, daily and simulator runs | R-SEED-6 | Unchanged, except that the F1 defect redraw starts a fresh launcher order outside the 8-redraw budget (SV-10 ⚑); LauncherEmu runs the real code |
| Run save at safe points; the snapshot is the authority; the slot after every command, deleted on load, forward-only | MAIN 6.6, XI-28 (agreed) | `run.save` + `FloorStatic`; WO1–WO8 with `runId`; atomic replace on load (agreed, Q4); no slot outside a floor (WO7); the outcome rule strengthens the guarantee |
| The simulator writes nothing | MAIN 6.6, EV-19 (agreed) | No player profile written; sandbox profiles in LauncherEmu (flagged) |
| HE-7: constructive reservation, one retry, proven asserts only, crash on second failure | DH 12.4, FL-15 | AP1 = HE-7's point; LR3 only with `proofRef`; one ladder in every build, with the second failure a hard failure in dev and CI after the output is recorded (never silent); release builds end the run cleanly after ≤ 3 attempts (agreed, Q2) |
| LE-14: the atomic commit; loot consumed only in 3a and 3; floor N+1 generated at camp exit from the camp-exit party snapshot | LE-14, LE 6.10, FL 6.1, ITEMS 10.4 | WO5 with `GenInputs` frozen at Travel on; speculation kept only on a digest match (DS14); `loot/run` and `loot/floor` keys; order kept (positions flagged) |
| EP retry1 and V12FloorFallback < 1% | EP 6.1 | LR2 and LR4; read pooled |
| CMP 7.4: the floor-time gate on humans; both caps in every arm; segment cuts before CMP levers | CMP 7.4 (agreed) | H instrument; both caps; PR3 before CMP levers; the time-model suspension rule |
| DH 18.2: calibration targets, F1 by the 95% upper bound | DH 18.2 (agreed) | CAL class; read verbatim; release set sized for it |
| Client numbers (`Floor1ClassSpread` 10, `HealerGap` 10, `HealIsolation` 10, ThemeParity 5, door pick 65%), and the agreed `PatrolFightRate` gate (XI Q2; its 60% is *(illustr.)* and owned by EP, not a client number; cross-document comparison 2026-11-23, CC-127) | CL, DH, HR, RS, XI (agreed); EP (the 60%) | PR1; one-sided reading; observed spread; `XI.BASE` |
| `EventDrawRunBlind`; LootDoorInvariant; ProfileBlind | EV, RS, XI | SV-30; one shared harness; intent-script tests |
| Verdicts from the M2 simulator only, never a toy | CL 5.6, CMP-29 | The M1 skeleton gives reports only |
| All validators and the simulator run headless; build fails on content failure | MAIN 7, 8 | CI tiers (5.8) |
| Seeded and shared runs advance no progress; seen counts only | Q1 (agreed); EV-19 (agreed, EV Q1) | 4.7; `runHistory[]` with `seedMode`, `recentRuns` never written for seeded runs; limited `pendingProfile`; `SeededRunWritesSeenOnly` |
| A release generation defect ends the run cleanly, keeps and marks its record, files a bug report | Q2 (agreed) | 5.5 defect path (one ladder in every build) and release clean end; `RunUnravelled`; WO6; `UnravelPath`; LR5 = 0 budgets |
| A run continues across a patch; generated floors kept exactly; not comparable | Q3 (agreed) | `FloorStatic` + `GenInputs`; 4.10.1; `comparable`, set only by a patch that can change the run, with its notice and mark; `SaveMigrationGoldens` |
| Snapshot plus state-delta journal; atomic replace on load; no outcome before its write | Q4 (agreed); XI-28 (agreed, XI Q1) | 4.8.3–4.8.4; WO2, WO7, WO8; `suspend.base` plus `suspend.journal`; `SlotEquivalence`, `ProbeResistance`, `LoadPathKillFuzz` |

---

## 13. Changes to approved documents

Applied on 2026-11-23, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches, with line numbers where the line was verified against the corpus while writing. The DH 12.4 line numbers were re-verified after the check against the current DH file: **"re-derives every child stream" is line 1030 and "there is no other retry policy" is line 1032** (the draft's "corrected" 1031 and 1033 were each one line late). Rows that touch **decided**, **agreed** or **settled** text, or a lead's call, are flagged ⚑ in the "Flag" column; as in the LE, TL, EP, IR, LP, EV, BR and XI precedents, the Flag column also marks rows that only *read* such text without changing it, so that every touch of it is visible. **The client's approval of 2026-11-23 covers the flagged rows** (with Q1–Q4 where a row names one). Told-plainly rows were told in 14.1 and, for those found in the check, 14.3, and confirmed by the client on 2026-11-23. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; recorded as readings, approved by the client with this document on 2026-11-23; each owner confirms its own reading):**
- V10's "HE-7 single retry at seed + 1" (HZ 2669) = V10g's runtime lookup is an AP1 `floorRetry` assert.
- BR's "content version … logged in the key" (BR 4.10.1) = inside `manifestHash`.
- EV 4.10 resolve step 5 "write the run snapshot" = that command's slot write on a floor (MAIN 6.6).
- `OneClockPerRoom` in EV 4.10's V15g = V10's check run at V15g.
- "Over the release seed set" (DH 18.2) = the frozen set plus logged extension trials.
- DH 18.2's three roles (6.3).
- Gates bind at the shipped cap after the cap decision (CMP 7.4).
- `PatrolFightRate`'s BASE = `XI.BASE`.
- "Deleted on load" = removed by atomic replacement with the re-validated slot (XI-28, XI Q1; agreed, Q4).
- R-SEED-5's "a run never changes mode mid-run" = the configuration and unlock snapshots, `drawableSet` and route state never change; a mid-run patch changes only the generator version of future floors (agreed, Q3; 4.10.1).
- EV 9.1's "shared and daily runs write `shownCount` and `fullShown` only" = also the whole of a seeded run's *progress* writes (agreed, Q1; 4.7). The marked run-history entry is **not** a reading: it adds a profile record, so it is a flagged change in SV-16 (corrected after the check).
- Q3's "a patch lands" = a patch that changes `genVersion`, `genContentHash`, the rules-relevant `manifestHash` or `rulesVersion`; a display-only patch does not set the not-comparable mark (agreed, Q3; 4.10.1; after the check).

| # | Document / section (status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| **SV-1** | MAIN 6.3 "own seeded generator" (decided: specified, intent kept); FL 6.1 lines 583–592 `Derive(parentState…)` (FL-2, lead's call); TR 6.2.5; EP 6.1; LE 6.10 item 1; IR 4.17; LP 4.7.10 (`mod 1000` at 774); BR 4.10.1 `Hash(…)`; TL 6.22 `H(…)`; CMP 7.2 tuples; EV 4.10 lines 908–909; ENM 12.2 line 1611; EP line 1602; R-SEED-3 (lead's call; implemented) | Key algebra; child from parent key; `Bound` everywhere (asserting n ≥ 1); reference vectors; after the check: wrapping arithmetic in one `unchecked` file with unsigned indices, the path grammar, K-rule 7 and `CanonicalWriter` with SHA-256 | ⚑ | Lead's call; approved by the client, 2026-11-23 | M1 |
| **SV-2** | R-SEED-3 (read); EP 6.1 draw table (text: `encounters/plan` and `/place`, counts unchanged); every segment's draw table (now registry rows) | Registry, count classes, registry handle, one stage per stream; recruit per-entry keys (CMP 7.2); `display` and `sim` keys (IR, LP, EV, XI display hashes; CMP Oracle); after the check: DS15 (fresh streams per stage run), the `vector` class for `boss9` (BR 4.10.1, BR-2), `curiosities` (EV 4.10) and `loot/run` (CN 14.7), and the `leaf-formula` class for FL's layout and ROOM's terrain counts (classification only; every approved count and value unchanged) | ⚑ (read) | Lead's call | M1 |
| **SV-3** | ROOM 6.2.5 reservation; TR-17; HZ-28 | The terrain reservation reserves `hzTagMax` draws as dummies from M1 | ⚑ (lead's call texts) | Lead's call; ROOM and HZ apply (request: HZ declares `hzTagMax`) | M1 |
| **SV-4** | **DH 12.4 lines 1020, 1026, 1030** ("dungeon stream", "seed + 1", "re-derives every child") and **DH-32 (line 2340)** (lead's call); **FL 6.1 Retry bullet lines 594–596** and FL lines 161, 421, 679 (FL-15 settled); ROOM 1112; HZ 2669; ENM 3999; RS 594; TL 2238, 3100; R-SEED-1 (reading); R-SEED-6 and EV 4.5.4 (read; the forecast becomes exact) | `attempt/{a}` node; "seed + 1" → attempt 1; "every child" → "every attempt-scoped child"; `curiosities`, `checks`, `trader`, loot and recruit keys attempt-invariant; **`dressing` stays attempt-scoped** | ⚑ | Lead's call; approved by the client, 2026-11-23 | M1 (curiosities M2) |
| **SV-5** | **MAIN 6.3 lines 417–420** ("the approved loot draws, which keep their order and positions") (decided); **LE-14** and LE 6.10 items 2–3; ENM 10.5 step 3; DH 12.5 items 1, 5–7; CN 14.7 / CN-27 (outcome unchanged); ITEMS 10.4; AR 14.3 (position read; IR confirms) | Step 3 keyed per floor and per entry block, order kept; count dummies and ghosts retired; `loot/run` phial always 3 draws. Fallback: sequential stream, blocks padded to caps, counter restored on retry | ⚑ | **Lead's call**; approved by the client, 2026-11-23 | M1 |
| **SV-6** | **MAIN 6.3 lines 401–411** (TL-3 sentence widened to out-of-combat noticing, Steal and hazard saves) (decided); **ENM 12.2 line 1608** "Noticing and Steal rolls use the combat stream" (lead's call); ENM 10.5 step 10; EP 6.8.5, ET4, EP-3; HZ lines 929, 2663, 11.2; TL 6.22 (form kept); EV 4.3, 4.10; LP 4.7.8 | One keyed-check spec with domains; `h:` prefix; `die` added to `lurkStealth`; Curiosity checks re-keyed | ⚑ | Lead's call; approved by the client, 2026-11-23 | M1 / M2 / M3 |
| **SV-7** | TL 6.22 line 2277; TL KB17 | Builders' salt option withdrawn | ⚑ (lead's call text) | Lead's call | M1 |
| **SV-8** | **R-SEED-3 steps 1, 3, 4** (lead's call); EV 4.10 step 3; V6 (properties kept) | Keyed-sort catalogue orders; decided before the M1 goldens | ⚑ | **Lead's call** | M1 rule |
| **SV-9** | **DH 12.4 lines 1024, 1028–1032** ("one floor assertion point before the loot step") (lead's call); **FL 6.1 item 3, lines 677–681**, FL 11.2, **FL-15** (settled); MAIN 6.3 (order kept; a pointer sentence) (decided); EP 6.1; LE 6.10 item 4; LP 2.1, 4.2; EV 4.10; BR 4.1.3 step 5; TR 6.2.5; HZ 11.2; ENM 10.5 | Pipeline GS0–GS17; AP1 and AP2; act-then-assert with rerun spans, the AP2 acts in pipeline-stage order as a code-fixed ordinal (after the check); F(N+1)'s `GenInputs` frozen at Travel on with speculation kept only on a digest match (after the check); `ProtectedIntact` split (structural FL at AP1; group part V12g); the AP1 equivalence rule | ⚑ | **Lead's call**; approved by the client, 2026-11-23 | M1 |
| **SV-10** | **DH 12.4 lines 1026–1032**: "there is no other retry policy" (line 1032) and "a second failure is a crash bug" (line 1027; kept in substance as a hard dev and CI failure after the output is recorded, and replaced in release builds only by the clean end) (lead's call); DH-32 (line 2340, read); FL 11.4 risk 10 (read); TL 6.22, 11.2; EP 6.1 (kept); LE 10.2 (e); **LP 4.3** (`LootBindUnplaceable` action; `LootBindDegrade`, request); **BR 4.1.3 and BR's `ArenaStampFallback` "in release"** (now in every build, budget 0; lead's call text); HZ 11.3; DH 21.2 `GuaranteedItemUnreachable` (class); **RS 6.4 R-SEED-6** (the redraw budget: the F1 defect redraw does not count against "up to 8" and starts a fresh launcher order; lead's call; added after the check) | Ladder LR0–LR5; nesting; `floorRetry` only with `proofRef`; `LootBindDegrade`; HE/Due binding proven (request to LP); one ladder in every build, ≤ 3 attempts (`GenMaxAttempts`), then Q2 in release; `runtimeAssert` class | ⚑ | **Lead's call; agreed, Q2** (the clean end, its record, the bug report); new lead's calls for the camp and F1 cases; lead's calls, after the check, for build parity, the progress credit and the F1 redraw | M1 / M2 |
| **SV-11** | EP 11.3 `V12FloorFallback` < 1% (read pooled); TL 11.4 (`V11Fallback` threshold added); FL risk 10 (read) | Per-pass budgets, `RetryRescueRate`, per-run degraded share, pooled reads | ⚑ (read) | Lead's call | M1 |
| **SV-12** | **R-SEED-5** (lead's call); **RS 11.1 configuration snapshot, line 2637** (lead's call: profile grants added); BR 4.10.1; MAIN 6.6 "version number" (decided; specified); MAIN 6.6 line 490 (read: profile grants) | Key gains `genVersion`; canonical `manifestHash` (computed in the build pipeline); `displayHash`; `genContentHash`; cutState inside the manifest; difficulty tier, boons, Nightmare tier in the config snapshot; `CoreNoProfileRef` and `ConfigHashCoversRunConfig`; the per-floor generation identity (key, `GenInputs` digest) | ⚑ | **Lead's call** | M1 fields / M2 / M3 |
| **SV-13** | **MAIN 6.6 lines 494–497** "the simulator writes nothing" (decided; EV-19 **agreed, EV Q1**); **EV 9.1 line 1444**; EV 9.4 line 1523 (lead's call); BR 4.10.3, 9.3; R-SEED-6 (read); XI display surfaces (read) | "Writes no player profile"; LauncherEmu with sandbox profiles; display interactions never core commands; `LauncherF1Neutral` | ⚑ (agreed) | Approved by the client with this document, 2026-11-23 | M2 |
| **SV-14** | **MAIN 6.6 lines 470–482** (XI-28, **agreed, XI Q1**; mechanism); XI 4.12; **FL 11.1 line 2258** "Save: the seed and configuration regenerate the static part, checked against layoutDigest" (contested; quoted verbatim after the check); FL 6.1 "Generation runs at camp step 11" and LE's camp-exit party snapshot (read, kept: `GenInputs` frozen at Travel on); TL 11.2 (digests read); EV 4.10 resolve step 5 (reading); LE 6.10 item 6 (read) | `FloorStatic`, `GenInputs`, `seq` and `runId`, WO1–WO8, the outcome rule (floor and camp), atomic-replace load, `suspend.base` plus `suspend.journal`, the platform shim, delta migration, `pendingProfile` | ⚑ (agreed) | **Agreed, Q4** (the slot mechanism, atomic replace, the outcome rule); lead's call for the rest | M1 / M2 |
| **SV-15** | MAIN 6.6 "version number" (decided; specified) and "command log … may stop replaying" (read); R-SEED-3 (cut records kept, read); **R-SEED-5** "a run never changes mode mid-run" (new lead's call in RS; read: kept) | Id freeze; no deletion after release; the mid-run patch rule (kept floors exact, future floors on the new version, `comparable = false` only for a patch that can change the run, its notice and mark, snapshots and route state fixed, still a personal run); save migrations with `SaveMigrationGoldens` | ⚑ | **Agreed, Q3**; new lead's calls for the details | M2 |
| **SV-16** | R-SEED-5 (read); R-SEED-6 (the launcher masks to 60 bits); **MAIN 6.6 profile save, lines 494–498** and **EV 9.1 line 1444** (EV-19, agreed, EV Q1; **changed after the check**: a profile `runHistory[]` record is added for every finished run, seeded runs included, beside the seen counts; `recentRuns` is still never written for seeded runs); EV 9.1 line 1423 (`recentRuns.seedMode`, read: not used for the seeded mark); RS 12 "profile-side anti-streak outside seeded runs" (read) | 60-bit seed; code format, GF(32) check character over seed and preset, ASCII parse, the 5-character build tag and two-tier notices, display after the run for personal runs; seeded runs marked in `runHistory[]` and advancing no unlocks, records, codex entries or launcher memory; `SeededRunWritesSeenOnly`, `ShareCodeErrorDetect` | ⚑ (agreed: MAIN 6.6, EV 9.1; covered by Q1) | **Agreed, Q1**; lead's call; new lead's call for the scope; lead's calls, after the check, for `runHistory[]`, the check symbol, the tag and where the code is shown | M2 |
| **SV-17** | R-SEED-5 (read); RS 12 Later row; LE 6.12; MAIN 6.6 command log (read) | Shared and daily rules; replay validity and checkpoints | ⚑ (read) | Lead's call | M1 replays / M2 shared / Later daily |
| **SV-18** | **DH 18.2** ("over the release seed set"; agreed Q11 target, read) and DH 21.2; FL 11.4 risk 10; IR 4.1; LE 6.10; LP 9.2; BR 4.17; XI 4.13; CMP 7.4 (read) | Fixtures; goldens by set cover; release set 2,000 runs per cap; logged extensions | ⚑ (read) | Lead's call | M1 / M2 |
| **SV-19** | RS 11.2 V-table (lines 2669–2692; a row added); ENM 20.1 list | V19 `Conformance` | ⚑ (lead's call texts) | Lead's call; RS owner applies | M1 / M2 |
| **SV-20** | **RS 11.2 header line 2671** and wave column (**V11 row line 2685**: M1 skeleton per TL 484; V17 M1 subset per BR 1265; V12 parts; V15 M1 rows); RS V7 row; RS 13 hook row; FL 11.2; ROOM 11.2; HZ 11.3; TL 11.3; EP 11.2; LE 10.2; IR 9.2; LP 9.2; EV 9.2; BR 9.2; **XI 9.2** (V18g knowledge rows → V18r; XI-30, lead's call); CL 12.2 and the class docs (`NameRegistry` feeds); ENM 20.1 | Phase model, failure classes, qualified ids, single owners, V16g split, V17g dependencies, V7 → V12g (request to EP) | ⚑ (lead's call texts) | Lead's call; owners apply | M1 / M2 |
| **SV-21** | MAIN 7 (read); CL 5.6 / CMP-29 (kept); CMP 11.3; EV 9.3; BR 9.3; XI 9.3 | Six instruments; event-sourced reports; CRN; `ToggleIsolation`; M1 skeleton | ⚑ (read) | Lead's call | M1 / M2 |
| **SV-22** | **ARC 5.4** (novice, basic, expert arms generalised to every class; flagged extension); DH 19.3 (read); EV 8, 9.4; BR 9.4; XI 9.4; FL 11.4; LP 9.4; CMP 11.3; RS 11.3; EP 11.4; LE 10.4; TL 11.5; HZ 11.5; ROOM 11.4 | Qualified arm catalogue; `Skill.*`; `Walk.Beeline`; `XI.BASE` for `PatrolFightRate` | ⚑ | Lead's call; ARC owner (fallback: per-class scripts) | M2 |
| **SV-23** | RS 11.4; **DH 18.2** (read, three roles); CL 5.6; FL 6.10, 11.3 ("gate" → BIND); **CMP 7.4** (read: gates bind at the shipped cap); BR-40; EV-3/EV-12; RS 6.11 gate 5 | Result classes; class-dependent decision rule | ⚑ (read) | Lead's call | M1 / M2 |
| **SV-24** | **HZ lines 1390, 1403, 2732, 2748, 2871, 3303 and the HZ 12 gated row** (`ClassThemeSpread` → `VoidClassDeathGap`); ROOM 11.3 (`EnvironmentKillShare` alias; `ContactHazardDowns` → `ContactHazardDeaths`; the split table); XI 9.3 (`SecretFindRate`); TL 6.15, 11.4; EP 11.3; LP 9.3; LE 10.3; RS 11.4 (`LauncherCheckMs` threshold) | Report-name fixes, owners, families, `LauncherF1Neutral` | ⚑ (lead's call texts) | Lead's call; owners apply | M2 |
| **SV-25** | **EV 9.4** (streak arm 10 → 200 profiles; lead's call); DH 18.2 (kept); RS 6.11 gate 2 (kept); CMP 7.4 (kept; interval printed); HZ 11.4; IR 9.3; TL 11.4; EP 11.3; BR 9.3; ROOM/XI `PreviewExactness` | Statistics rules; observed-spread rule; minimum N; zero partners; `TimeModelError` | ⚑ | Lead's call | M2 |
| **SV-26** | CMP 7.4 (read; order kept); DH 18.1, 18.2, 5.7 lever orders (kept); EP 11.3; CL 15; ARC 11.8; HR; BR 9.4; XI 9.5; LE-24 | Precedence PR0–PR5; blast radius; one lever at a time; intra-PR1 rule; `FloorClockLedger` | ⚑ (read) | Lead's call | M2 |
| **SV-27** | CMP 7.4; RS 6.11, 7.4, 12 (camp-3 slip, read); EP; XI; HZ; BR; ENM 20.3 | Human-session plan and hours | ⚑ (read) | Lead's call | M2 |
| **SV-28** | MAIN 7, 8 (read; extended) | CI tiers, budgets, analyzer, loader, hostile-environment test; per-OS jobs and the minimum-spec runner | ⚑ (read) | Lead's call | M1 |
| **SV-29** | MAIN 9 waves (read); RS 12; CL 13 | Wave checklists; the M2 internal sequence; drift watch; `simVersion` | ⚑ (read) | Lead's call | M1–M3 |
| **SV-30** | **EV 4.10 `EventDrawRunBlind` / V16g** (EV-21, lead's call); EV 4.5.4 (read) | EventDraw reads the committed theme as a `GenInputs` field (no other player input); new V16s `F1HostAlways` (every F1 Blueprint offers a legal host for every F1 Curiosity pool, so a retried F1 keeps its forecast Curiosity) | ⚑ | Lead's call; EV owner applies | M2 |
| **SV-31** | **MAIN 6.3** (decided): sentences for the `hazards` grandchild of `terrain` (HZ-28), "`dungeon`, `loot` and each floor substream are never drawn", the `attempt` and `retry1` children, the `encounters` sub-keys, a registry pointer; **DH 12.4 lines 1020, 1026** (stale "dungeon stream"; lead's call); FL 6.1 ("steps 2–11 keep drawing from the parent substream"); ENM 8.2, 10.5 intro and steps 2, 5, 10; ITEMS 10.9, 18.4; EP "siblings" and ET4; EV 4.5.4 exhaustion tuple (add the boss rule); LE 6.10 item 6 ("the free cards"); CMP pledge (by camp; same index as floor); CMP `cosmetic` (note); BR "logged in the key"; LP 4.2 Windfall timing; "RT" → "ROOM" at TL 132, EP 141, HZ "RT 6.11" and LP 0.1, 2.1 and 13 ("RT 0.1 item 5 / RT Q5"; LP added by the cross-document comparison 2026-11-23, CC-113); BR V17s (j) "27 docs", XI A.5 "28 documents" → `NameRegistry`; ROOM 11.3 split table; HZ VP1–VP10 note (request) | Text hygiene | ⚑ (decided and lead's-call texts) | Lead's call; owners apply | M1–M2 |
| **SV-32** | ITEMS 10.9, 18.4, I-14; ENM 10.5 steps 2–3 (Later text) | `floor/{N}/trader` (2 draws, attempt-invariant) and the loot `trader` block; no parent positions inserted | ⚑ (lead's call texts) | Lead's call | Later |

---

## 14. Open items for the client

None. Approved by the client on 2026-11-23.

The client approved this document as a whole on 2026-11-23, including the lead's answers of 0.1 and composite R (which the client had accepted by proceeding); every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the 40 lead's calls after the check in 14.3); the told-plainly items (those in 14.1 and "Told plainly after the check" T1–T11 in 14.3, all as written); and the readings recorded in 13; and its approval covers the flagged rows of 13. The four questions raised before writing were answered on 2026-11-23 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. Writing raised **no new client question**, and neither did the design check: every new lead's call in 14.1 and every lead's call after the check in 14.3 sits inside the four answers, keeps an approved rule, or is flagged against the text it touches in 13. The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them (including the owner-refusal fallbacks, the report-triggered items and the wave-gated items), and the naming pass remain future items.

### 14.1 This document

**The four answers (agreed, 2026-11-23), each the recommended option:**
- **Q1, seeded and shared runs (SV-16):** option A. They appear in run history marked seeded but advance no unlocks, records, codex entries or launcher memory; they write only the approved seen counts. Rejected: B, unlock content but set no records; C, count like normal runs.
- **Q2, a release generation defect (SV-10):** option A. After the release path's attempts fail, the run ends cleanly ("the dream unravels"), its record is kept and marked, and a bug report is filed. This replaces the crash in the death and healing design (DH 12.4) for release builds. Rejected: B, a hand-authored safe floor per theme (kept as a future item, 14.2); C, crash.
- **Q3, a patch during a run (SV-15):** option A. The run continues; floors already generated are kept exactly; future floors use the new version; the run is marked not comparable for records and shared codes. Rejected: B, the run cannot be loaded on the new version.
- **Q4, the suspend-slot write mechanism (SV-14):** option A. A snapshot at floor entry plus a state-delta journal after each command, compacted often; load replaces the slot atomically instead of delete-then-write; no roll, reveal, Down or death is shown until its record is written. The agreed guarantee (MAIN 6.6, XI-28, XI Q1: one slot, removed or consumed on load, knowledge forward-only) is unchanged and strengthened. Rejected: B, a full rewrite after every command.

**The lead's answers (0.1) and composite R (3.3)** were accepted by the client by proceeding and confirmed by the client on 2026-11-23, with this document. Composite R: Model A's registry files as the source of truth now; Model B's code-side registrations checked against them by V19s from M1; owners edit their own rows, with review only for the stage skeleton, an assertion point, the ladder, the key algebra, the reproducibility key or a golden digest change; Model A's content and Model B's mechanics where they differ; the simulator layer and the numbers fixes as amended by the critics.

**Told plainly (confirmed by the client on 2026-11-23, with this document):**
1. Out-of-combat noticing, Steal and hazard saves move off the combat stream to keyed checks (SV-6); this changes a lead's-call sentence in ENM 12.2 and the decided MAIN 6.3 stream text.
2. The difficulty tier, boons and Nightmare tier change a run, so they join the reproducibility key (SV-12; live in M3, reserved from M1).
3. Shared codes match the dungeon's shape (route, doors, F1), but from F2 loot and room sizes depend on the party brought (4.7). **Corrected after the check (14.3, item T1):** loot depends on the hero from F1.
4. No roll, reveal, Down or death is shown until its save lands (4.8.3; agreed, Q4).
5. Codes survive patches at route and layout level only; weighted picks still remap (4.10).
6. Client numbers are never loosened by statistics: one-sided reading, straddling fails, spreads read as observed (6.4).
7. Four unmeasured numbers (simulated time per floor, stage times, ρ per retry, fsync latency) are measured on the M1 skeleton before the M2 budgets freeze (6.1, 8.7).
8. The rest, told where they stand: the retired loot ghost and count-dummy draws (4.3); DS7, "a new consumer moves no other draw" but downstream outputs may change (4.5); SplitMix64 is not cryptographic (4.1); daily seeds from a public formula can be pre-played (4.7); the FL 11.1 contest, stored floors (4.8.1); "deleted on load" met by atomic replace (WO7); one DH 18.2 calibration figure now drives two switches (6.3); gates bind at the shipped cap (6.3); a fair game still reads about +1 point on spread gates, at about 1.2M extra simulated floors (6.4, 8.4); human hours may be M2's critical path (6.11); an LP binding proof refusal has no M2 fallback (14.2).

**New lead's calls (made while writing; confirmed by the client on 2026-11-23, with this document):**
1. **Document shape.** The proposal's section numbers 0–15 are kept so every cross-reference holds; the client decisions become 0.2 and the decisions in brief 0.3 (with a marking column); intent, goals, non-goals and anti-goals are added as 0.4; the client questions become 14 (open items); 16 (left to the builders), 17 (depends on) and Appendix B (sources) are added.
2. **Parent questions named with their document** ("XI Q1", "EV Q1", "XI Q2", "DH Q11"), so that they never read as this document's Q1–Q4 (header, 2.1, 6.1, 6.2, 13).
3. **Q1–Q4 rules are client rules, not configuration:** changing them returns to the client; the attempt count (now the code constant `GenMaxAttempts`), the compaction interval and the overlap budget stay illustrative (header, 9.4).
4. **Q1 scope:** "seeded" covers every run whose seed was not drawn by the launcher, a player's own code included; daily runs inherit Q1 unless the client rules otherwise with the daily-mode item; replay and simulator runs write no player profile; WO1 and WO6 carry a `pendingProfile` limited to the marked history entry and the seen counts; V19t `SeededRunWritesSeenOnly`; the seeded mark beside the code on the run record and the death and victory screens (4.7, 4.9).
5. **Q2, where the clean end happens:** at camp, when F(N+1)'s generation fails at Travel on (corrected after the check: the authoritative generation runs at Travel on from the camp-exit state); Travel on shows the unravel card, and WO6 finalises the run with the result `unravelled` (5.5).
6. **Q2, the record:** neither a death nor a victory; excluded from records; a seeded run still advances nothing; P0 event `RunUnravelled` (5.5, 9.3). *(Changed after the check: progress is credited as a death at camp N, not as an abandoned run; 14.3.)*
7. **Q2 at F1 run start:** a personal run silently redraws outside R-SEED-6's 8-redraw budget, logging the discarded key P0; a seeded code shows "this code cannot be dreamed on this build"; no run record either way (5.5). *(Refined after the check: the fresh candidate goes through the full launcher order with a fresh budget; flagged in SV-10; told plainly as a narrowing of Q2, 14.3.)*
8. **Q2, the bug report:** a local bundle (key, `GenInputs`, `GenerationLog`, stage, check, attempt, command log, run save), sent only under the game's telemetry consent setting (a hook to the main design owner) (5.5).
9. **Q3, what stays fixed:** the configuration and unlock snapshots, `drawableSet` and the route state are never recomputed (R-SEED-5's "never changes mode mid-run" kept, recorded as a reading in 13); content added by the patch is not drawable in that run; `genVersionsUsed[]` in the save header; the debug regeneration check is skipped for floors whose recorded `genVersion`, `genContentHash`, configuration or unlock hashes differ from the build's (widened after the check) (4.10.1, 9.2).
10. **Q3, still a personal run:** unlocks, codex entries and launcher memory advance, because Q3 names only records and shared codes; replays of such a run are not replayable on any build (4.10.1).
11. **Save migrations are mandatory:** every `saveVersion` bump ships a migration; BUILD test `SaveMigrationGoldens`; a save without a migration path is a release blocker, never a lost run (4.10.1, 5.2).
12. **Q4 across a patch:** a journal written by one build applies unchanged on the next within one `saveVersion`, because deltas are state; across a `saveVersion` bump the journal is applied to the old base by the old reader, then migrated (corrected after the check; 4.8.4).
13. **Line citations corrected:** DH 12.4 "re-derives every child" is line 1030 and "there is no other retry policy" line 1032 (re-verified after the check; the draft's 1031 and 1033 were one line late) (SQ2, SQ4, SV-4, SV-9, SV-10, 5.4).
14. **Readings added to 13:** R-SEED-5's "never changes mode mid-run" under Q3; EV 9.1's seen-count rule as the whole of a seeded run's progress writes.
15. **Flags in 13:** SV-15 flagged ⚑ (it now carries the Q3 rule); SV-16 also touches MAIN 6.6's profile save, EV 9.1 line 1444 and RS 12's "outside seeded runs" row (after the check, SV-16 changes MAIN 6.6 and EV 9.1 for `runHistory[]`, under Q1; 14.3).
16. **Requirements and risks added:** four binding-requirement rows for Q1–Q4 (12); risks 16–18 (15).
17. **Owner-refusal fallbacks** stated for every request, including those the proposal left without one (14.2), so that a refusal never blocks M1 and only the LP binding proof can block M2 (told plainly).

### 14.2 Future items and owner-refusal fallbacks

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a lead's call, a new lead's call or a lead's call after the check (confirmed by the client on 2026-11-23, with this document) unless marked.

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| **Safe floors (Q2 B)** | Release telemetry ever shows a `GenerationDefect` or `RunUnravelled` | Revisit a hand-authored, proven safe floor per theme | Client |
| **Daily mode** | The Later wave | Should a daily run generate loot and recruits against a fixed reference party snapshot, so runs stay comparable after F1? Where does the daily seed come from (a published value or a server)? Does Q1 hold for daily runs (it does by default, 4.7)? | Client |
| **Shared seeds and kit variant boons** (SK-16, request from the starting kits design 4.7; pending) | M3, or the SV owner's ruling | Fallback until ruled: on shared seeds, variant boons are replaced by the default kit for the chosen creation pick; other boons follow the canonical-grant rule (4.7); the seeded entry screen states it | SV owner |
| **Heirloom bequest write** (HL-6, request from the heirlooms and curses design 4.1.2; pending) | M3, or the SV owner's ruling | Fallback if refused: R2 ships without the offer; the unlock applies directly at WO6, shown at the next shelf visit, still never on the death path | SV owner |
| **Heirloom unlocks and `unlockHash`** (HL-11, request from the heirlooms and curses design 4.1.7; pending) | M3, or the SV owner's ruling | Fallback if refused: unlocks enter `unlockHash`; the "will play differently" notice states "different shelves" plainly | SV owner |
| **Share codes and heirlooms** (HL-25, request from the heirlooms and curses design 4.1.7; pending) | Later, or the SV owner's ruling | Fallback: share codes carry neither the template id nor `runModifierId` | SV owner |
| **The Bobbin in `configHash`** (CA-20, request from the charm abilities design 4.11; pending) | M3, or the SV owner's ruling | Fallback if refused: the charm abilities design's "Bobbin refused" row (the boon is withdrawn or narrowed; charm abilities design 14.2) | SV owner; charm abilities lead |
| **Charm-family unlocks and `unlockHash`** (CA-21, request from the charm abilities design 4.10; pending) | M3, or the SV owner's ruling | The charm abilities design names no fallback; the lead rules | SV owner; CH owner (hub-object reading) |
| **Charm reports, arms and minimum N** (CA-18, request from the charm abilities design 9.1, 9.4, 10; pending) | M2, or the SV owner's ruling | The charm abilities design names no fallback; the lead rules | SV owner |
| **Difficulty tiers** | M3 | Should any DH 18.2 target hold per tier, or only at the base tier with the other tiers reported? (Reported per tier from M3 either way) | Client |
| **Spread gates** | The observed-spread rule's measured compute proves too high (6.4, 8.4) | Ask whether ≤ 5 / ≤ 10 may be read on the noise-corrected spread | Client |
| **Owner refusals** | Any request below | Its fallback applies; it returns to the client only if the fallback touches a client number or an agreed answer | Lead |
| TL refuses the traps and trapsResolve formulas | — | TL's own padded maxima declared as caps; the DrawLedger checks counter = cap | TL owner |
| HZ refuses the hazards formula or `hzTagMax` | — | Segments 11–12 reserve a padded maximum from HZ's approved tag table at the lock; it changes only with a `genVersion` bump and a rebaseline | HZ owner |
| HZ does not state the F1 environment scope | — | "Environment" reads as every non-unit damage source (hazards, traps, falls), so V10 `F1NoHazards`, the V9 push-lane rows and V11 `F1TrapRules` are all required partners | HZ owner |
| HZ refuses the Void zero-gate proofs | — | `EliteRemovedBeforeFirstTurn` and `ZeroHpAfterDreamfall` (renamed from `DownedAfterDreamfall`: party units reaching 0 HP within one round of placement; cross-document comparison 2026-11-23, CC-96) are read as SHIP rows on the Void package at 30,000 floors; any non-zero holds the package | HZ owner |
| LP refuses itemising the V15 M1 rows | — | Segments 11–12 itemise the M1 subset from LP 9.2 (the rows touching F1 binding); LP confirms on the M1 checklist | LP owner |
| LP refuses the HE/Due binding proof | — | M1 ships with V15g's assert as `defect` and the soak at 0; **M2 sign-off stays blocked until the proof exists (told plainly: no M2 fallback)** | LP owner |
| LP does not name a default `LootPolicy` | — | `LootPolicy.Balanced` | LP owner |
| EP refuses the signature-twice authority or the V12t sample row | — | V19t runs the sample row | EP owner |
| EP refuses `ProtectedIntact`'s group part | — | FL owns both parts; the group part runs at AP2 as `stageRetry(encounters)` then `neverWaived`, under V8g | EP, FL owners |
| LE refuses SV-5 (**closed**: SV-5 is applied, LE 6.10 item 2; cross-document comparison 2026-11-23, CC-108) | — | *(Retired: the sequential parent stream, every data-dependent block padded to a declared cap, the counter snapshotted and restored at GS1 on any retry (4.3).)* | LE owner |
| LE does not confirm the `dealShuffle/retry1` key or the V13g (f) quantity | — | The key as written in 4.3; (f) stays TL's REP row | LE owner |
| IR does not name the Resistant draw's owning entry | — | The authored Resistant notable's own identity entry | IR owner |
| IR refuses the `DormantAtChoice` proof | — | The PB row is read at 30,000 floors (≤ 0.01%) with no structural partner; told plainly that this bounds a rate, not impossibility | IR owner |
| BR or ROOM refuse the stamp before terrain and the arena-node dummies | — | The stamp runs after GS6 and overwrites, as BR 4.1.3 reads today; the arena node's slot draws stay fixed-per-data, so no count moves | BR, ROOM owners |
| RS rules the launcher-exhaustion reading otherwise | — | R-SEED-6's ranked best-candidate rule applies as approved (keep the best candidate by theme rule, Fallow, boss rule, onboarding, earliest); if the RS owner rejects the pending clause "keep the original seed when none passes the theme rule", the best-ranked candidate is kept in that case too (corrected after the check) | RS owner |
| CMP or EP do not confirm the `content.CMP8.*` rows (added after the check) | — | The rows run as EP 13 words them, under CMP 11.2's suite | CMP, EP owners |
| LP does not confirm `LootBindDegrade`'s merge-or-coin rule (added after the check) | — | LP's own failure order applies to the socket, ending in coin-only; the row stays a BUDGET ≤ 0.3% | LP owner |
| RS refuses SV-8 keyed sorts | — | Fisher–Yates fixed per manifest; shared codes survive content additions less often (told plainly) | RS owner |
| RS refuses the profile grants in `configHash` (SV-12) | — | A separate key field `grantsHash` beside `configHash`, with the same effect | RS owner |
| XI, FL and EV do not rule on walker equivalences | — | No mapping; each gate reads its own owner's arm | XI, FL, EV owners |
| ARC refuses the generalised `Skill.*` arms | — | Per-class novice scripts delivered by the M2 planning gate | ARC and class owners |
| CN refuses "phial always 3 draws" | — | `loot/run` becomes per-entry with 1 or 3 draws on its own key; nothing else moves | CN owner |
| ENM refuses SV-6 | — | The status quo; `ExplorationCombatIsolation` carries a named known exception (risk 15) | Enemies owner |
| EV refuses SV-30 | — | `ForecastSurvivesRetry` asserts identity only; the forecast's presence becomes a REP row (told plainly) | EV owner |
| **FL refuses storing `FloorStatic`** (SV-14) | — | **Returns to the client:** Q3 (agreed) needs stored floors | FL owner; client |
| **Main design owner refuses** SV-13 (no player profile written by the simulator) | — | **Returns to the client** (EV Q1 agreed text) | Main design owner; client |
| **Main design owner refuses** the journal, atomic replace or the outcome rule | — | **Returns to the client** (Q4 agreed) | Main design owner; client |
| **Main design owner refuses** the profile `runHistory[]` record (SV-16; added after the check) | — | **Returns to the client** (Q1 agreed: seeded runs appear in run history, which needs a home outside `recentRuns`) | Main design owner; client |
| **DH owner refuses** the release clean end (SV-10) | — | **Returns to the client** (Q2 agreed); AP1 stays DH's own point either way | DH owner; client |
| Main design owner gives no telemetry consent setting | — | Bug reports stay local; the player can attach them by hand | Main design owner |
| Naming pass | — | Every placeholder name here, the unravel card's words, the seeded mark and the build-mismatch notice; after the check also the two-tier mismatch notices, the missing-suffix notice, the "seed (will play differently for others)" label, the seeded-run entry line, the version-crossed mark and its one-time load notice | Client |

### 14.3 After the design check

The design check of this document returned 61 findings, 3 of them blockers: 21 on determinism (1 blocker), 14 on consistency, 16 on validation (2 blockers) and 10 on the player's view. Each was fixed, merged into another fix, or rejected with a reason; none overrides a client decision (Q1–Q4) or reopens the accepted lead's answers, and none raises a client question. The client confirmed every item below on 2026-11-23, when it approved this document as a whole.

**Told plainly after the check** (none overrides a client answer; confirmed by the client on 2026-11-23, all as written):
- **T1. The shared-seed boundary was one floor late.** The accepted told-plainly item 3 said "loot depends on the party from F2". In fact F1 already reads the hero: the F1 Due is a weapon for the hero's arms, and IR's filter-first reads the party snapshot on every floor. Corrected statement: **shared codes match the dungeon's shape (route, doors, layout graph); loot depends on the hero from F1 and on the whole party from F2** (0.1, 4.6, 4.7). A daily mode with a fixed hero (14.2) would still give an identical floor 1.
- **T2. Floor N+1 is built from the camp-exit party, as the loot design already says.** The draft built it from the camp-entry party, which would have used a stale party after recruiting, deaths or purchases. Background work at camp is now speculation only, kept when nothing generation-relevant changed; otherwise the floor is rebuilt at Travel on behind a short loading beat, within the 1 s worst case (4.4).
- **T3. Q2 is narrowed at F1 run start.** When a run's first floor cannot be built, no run has begun and nothing was shown, so no run record is written; the bug report is still filed, a personal run silently gets a fresh launcher seed (with a fresh redraw budget, flagged against R-SEED-6), and a seeded code says it cannot be dreamed on this build (5.5).
- **T4. An unravelled personal run now earns at least what a death at that camp would**, not the lower credit of an abandoned run; it is still excluded from records (Q2) (5.5).
- **T5. A display-only patch does not mark a run not comparable.** Q3's mark is set only by a patch that changes generation, generation content or rules; text and art patches leave the run eligible for records. When the mark is set, the player is told once on load, and the run record shows a version-crossed mark (4.10.1).
- **T6. Seed codes are shown for personal runs only after the run ends**, to stop scouting a record-eligible run on a second profile; seeded runs still show their code in camp (4.9).
- **T7. The no-outcome-before-save rule now covers camp commands** (rerolls, purchases, recruiting, training), which the draft had left open to kill-and-reload probing (WO8).
- **T8. Budgets on generation fallbacks are read pooled only.** Per-cell soak reads become alarms. Stated plainly: at the pooled sizes, a ≤ 0.3% budget passes reliably only when the true rate is about 0.08% (F1) to 0.11–0.14% (F2–5) (5.6).
- **T9. Statistics are a little stricter than the draft's numbers said.** The one-sided gates read at z = 2.18 at both looks (the two-look plan), which raises several minimum N by about 24%, and spread gates read a bootstrap upper bound, not a point value, with N recomputed at the M2 planning gate (6.4, 6.5). Client numbers are unchanged.
- **T10. The four unmeasured numbers cannot all be measured at M1**, because M1 lacks the M2 stages. M1 files a measurement report; the M2 planning gate completes the measurements, recomputes the CI estimates and freezes the budgets (6.1, 7).
- **T11. Every build now runs the same generation ladder.** A dev or CI build makes exactly the floor a release build would, and only then fails hard; DH 12.4's "a second failure is a crash bug, never silent" is kept in substance (5.5).

**Lead's calls, after the check (confirmed by the client on 2026-11-23, with this document):**
1. **Camp-exit generation** with speculation kept only on a digest match; DS14 restated; `SpeculationEquivalence` (4.4, 4.5, 4.8.2, WO5).
2. **Per-floor generation identity** (key, `GenInputs` digest); F1's `GenInputs` carries the hero-creation inputs; golden F1 fixtures pin them (4.4, 4.6, 4.11).
3. **Wrapping arithmetic** in one `unchecked` key-algebra file; unsigned index types; vectors under a checked build (4.1, DS11).
4. **The path grammar**; `StreamRow.label` and `indices[]` normative; nine added reference vectors (4.1, 4.2).
5. **One ladder in every build**; `GenMaxAttempts` a code constant; `ArenaStampFallback` in every build at budget 0; `DevReleaseParity`, `UnravelPath` (4.3, 5.5, 9.4).
6. **DS15** (fresh streams per stage run) and the **`vector`** count class for `boss9`, `curiosities` and `loot/run`; 0-based indices; `boss9` indices 0–1 at GS4 (4.2, 4.3).
7. **The AP2 act order** by pipeline stage, as a code-fixed ordinal under segment 11–12 review; a rerun keeps a stage's earlier retry unless its inputs changed (3.3, 5.4).
8. **`runId`** on every save file and journal record; load compares `seq` only within the current run (4.8.3).
9. **The slot's two files** (`suspend.base`, `suspend.journal`), the compaction order, the load rule, and no slot written at camp (4.8.1, 4.8.4, WO7).
10. **The platform shim** for durable flush and atomic replace (4.8.4, 9.5).
11. **Journal migration** across a `saveVersion` bump, and `SaveMigrationGoldens` with uncompacted journals (4.8.4, 4.10.1).
12. **WO8**, camp commands under the outcome rule; `ProbeResistance` extended to camp (4.8.3).
13. **The share code:** a GF(32) check symbol over the seed and the preset; a 5-character uppercase build tag (4 generation characters over `genVersion`, `genContentHash`, `configHash` and `unlockHash`, 1 rules character); two-tier mismatch notices; the missing-suffix notice; the "will play differently" label; personal-run codes shown only after the run; the seeded-run entry line; `ShareCodeErrorDetect` (4.7, 4.9).
14. **`CanonicalWriter` and SHA-256** for every digest and delta, locked before the M1 goldens (4.5).
15. **The `SaveDigestMatch` skip rule** widened to content and snapshot hashes (4.10.1).
16. **The `leaf-formula` count class** for FL's layout and ROOM's terrain counts, and DS3 amended to match (4.2, 4.3, DS3).
17. **`CoreNoProfileRef` and `ConfigHashCoversRunConfig`** replacing `KeyCoversProfileGrants` (4.6).
18. **Build-pipeline hashing**, ASCII rules-relevant strings, canonical rationals (4.6, DS13).
19. **`Bound` asserts n ≥ 1**; keyed sorts break ties by ordinal id (K-rule 7) (4.1).
20. **Dressing on the display side**, from `attemptUsed` (4.3, 4.4).
21. **The seeded mark's home**, a profile `runHistory[]` record; `recentRuns` never written for seeded runs; flagged in SV-16 (4.7, WO1, WO6).
22. **`h:lurkStealth` moved to M2**, reserved in M1 (4.3, 7, 10, A.3).
23. **V1s at build tier 2** (5.3).
24. **ρ as the correlated-failure share**, frozen at the M2 budget freeze (5.6, 8.2).
25. **Flags in 13** added for SV-3, SV-19, SV-24, SV-28, SV-29 and SV-32; SV-10 gains R-SEED-6 and BR's "in release"; SV-16 becomes a change to MAIN 6.6 and EV 9.1 under Q1.
26. **The CMP-8 hook** answered (`content.CMP8.*`) and BR's reports-and-arms hook listed (2.2, 5.2, 11).
27. **The pooled-only BUDGET reading**, per-cell soak reads as ALARM (5.6, 6.5, SN4).
28. **The drift watch** moved to weekly, alarming only on a whole interval past the line, with stale set only by a full-N re-read (7, 5.8).
29. **The z values stated** (Pocock z = 2.18 at both looks for one-sided classes; rule-1 rules at their own z) and the 6.5 sizes recomputed (6.4, 6.5).
30. **Spread gates' interval method** (a paired percentile bootstrap) and N recomputed at the M2 planning gate (6.4).
31. **Decision rules for CUT, PB, ALARM, WATCH and FELT**; zero gates as two BUILD rows; `Level10BeforeFinalBoss` REL; `TimeModelError` SW with its own N; INVALID = FAIL for one-sided classes (6.1, 6.3, 6.4, 6.5, 6.6, 6.12).
32. **The M2 planning gate completes the measurements** and resizes the tiers with named fallbacks (6.1, 7).
33. **The nightly breakdown**, the drift sample off nightly, per-OS jobs and the minimum-spec reference runner; a minimal M1 nightly (5.8, 8.7).
34. **Golden and release-set derivations** from `simRoot`, LR2+ candidates excluded, stored fixtures for digests and budgets (4.3, 4.11).
35. **`LauncherF1Neutral`** given ε = 2 points, a class, an N and its fallback (6.5, 6.10).
36. **A failure-class table** for the `g` checks and V19s `ClassDeclared`; V19t CI tiers (5.2).
37. **The outcome rule's edges:** only the journal record gates; compaction after the frame; the over-budget hold; `OutcomeWaitMs`; a FELT session (4.8.3, 5.8, 6.11).
38. **The unravel progress credit** (as a death at camp N) and the F1 redraw through the full launcher order (5.5).
39. **The "What this document is" paragraph** rewritten to list every player-visible exception (header).
40. **Text fixes:** the DH line numbers, the FL 11.1 quote, the lcm reason, the R-SEED-6 fallback, and the draft ids R-K4, SC-2, SN5a and "B S7" removed (2.3, 4.1, 4.5, 6.1, 8.2, 13, 14.2, A.6).

**Rejected or narrowed in the check, with reasons:**
- **Show the unravel card as soon as the defect is known at camp** (player view): rejected for the timing part. The floor's real inputs are fixed only at Travel on (T2), and a speculative failure on a candidate party does not mean the real floor fails, so the card shows at Travel on. A speculative failure is logged P0 at once. The credit part of the same finding is taken (T4).
- **Add hero class and species to the share code so F1 matches exactly** (an alternative offered by the check): not taken; the boundary is told plainly instead (T1), and the daily-mode future item already covers a fixed hero (14.2).
- **Size the soak for per-cell budgets (about 4.6×10^5 floors weekly)** (an alternative): not taken; pooled gating with per-cell alarms answers the same need at no compute cost.

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | Many flagged changes to settled or decided text (DH 12.4 three times, MAIN 6.3, MAIN 6.6, R-SEED-3, R-SEED-5, LE-14, FL 6.1, FL 11.1, ENM 12.2). If several are refused, robustness drops | Each has a named fallback (14.2): padding plus counter restore; Fisher–Yates; the status quo for SV-6. A refusal of stored floors returns to the client, because Q3 (agreed) depends on them |
| 2 | The four unmeasured numbers swing compute 20× and decide whether CI tiers fit | Measured on the M1 skeleton (a measurement report), completed for the M2 stages at the M2 planning gate, which recomputes 8.2 and 8.7, resizes the tiers with named fallbacks and freezes the budgets (7) |
| 3 | Human playtest hours (150–300 for door pick alone) may be M2's critical path | One calendar; Two Doors may slip to M3, re-tested at camps 1–2 (and gate 7) (approved); camp 3 is a separate M3 extension once the floor-4 pool is 3 or more (RS 6.11, RS 12; cross-document comparison 2026-11-23, CC-131) |
| 4 | Owner deliverables (TL and HZ formulas, `hzTagMax`, LP rows and proofs, the zero-gate proofs, the novice arms) gate the goldens and M2 | On the checklists as blocking items with fallbacks |
| 5 | The registry is a merge hotspot | Row ownership and Model B's review triggers; owners edit their own rows |
| 6 | The AP1 equivalence depends on later stages never editing layout | `AP1EarlyEquivalence`; any geometry-editing stage moves its asserts to AP2 |
| 7 | Storing `FloorStatic` duplicates truth and grows saves (about 430 KB per run); a regeneration-only generator bug is caught in CI, not the field | `SaveDigestMatch` in CI and debug builds |
| 8 | Keyed sorts keep relative orders, not first picks; weighted picks still remap; shared codes survive patches only at route and graph level | Told plainly (4.10) |
| 9 | Release defect handling (agreed, Q2) could hide defects behind clean endings if telemetry is ignored | P0 telemetry; LR5 = 0 soak gates; golden LR2–LR5 = 0 |
| 10 | Observed-spread gating is about 1 point stricter than the true spread and costs about 1.2M extra floors | Printed beside the de-noised figure; future item if cost bites |
| 11 | Generalising the novice arm extends ARC 5.4; per-class scripts may be needed | Fallback by the M2 planning gate |
| 12 | Label and id collisions are negligible but not zero; runtime-composed tuples are not scanned | 64-bit scan of registered labels and manifest ids; `Absorb` chaining; domain disjointness |
| 13 | Cross-OS determinism covers Core only; Godot display may differ | By design; display is outside the key |
| 14 | Daily seeds need a server for fairness and leaderboards | Later; told plainly |
| 15 | Moving noticing off `combat` (SV-6) changes ENM's lead's-call stream sentence late; if refused, exploration keeps moving fight rolls | Flagged; the fallback is the status quo, with `ExplorationCombatIsolation` marked as a known exception |
| 16 | Runs that span a patch (agreed, Q3) mix generator versions, so a version-boundary bug shows only in the field | `SaveMigrationGoldens` continues every previous-release golden run across the boundary; `genVersionsUsed[]` in every bug report |
| 17 | An unravelled run (agreed, Q2) still feels like a lost run to the player | LR5 = 0 budgets over 10^6 floors at sign-off; P0 `RunUnravelled`; Q2 B (safe floors) returns as a future item if release telemetry ever shows a defect (14.2) |
| 18 | The journal (agreed, Q4) adds a second save format to keep correct | `SlotEquivalence` after every command on the goldens; compaction at every Down, death, paid act and clean quit; CRC truncation; kill-fuzz inside writes, compaction and the load path; delta migration with `SaveMigrationGoldens` on uncompacted journals |
| 19 | Speculative generation at camp is often discarded (any recruit or purchase that touches generation inputs), so many players wait at Travel on (after the check) | The rebuild fits the 1 s worst case behind a loading beat; `SpeculationDiscarded` is logged; if the wait is felt, a later speculation restarts after each committed camp command |
| 20 | The platform shim is per-OS code outside Core's analyzer (after the check) | Kept tiny; covered by kill-fuzz on each per-OS nightly job; process-kill resistance does not depend on it |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the attempt count (3, the code constant `GenMaxAttempts`, changed only with a `genVersion` bump), the journal compaction interval (50), the overlap budget (120 ms), the replay checkpoint interval (64), the golden and release-set sizes, maxSockets, maxSocketsPerRank, `candidateDrawMax`, `candidateListLength`, the curiosities count (locked before the M2 goldens), the CI tier budgets and core counts, the runtime budgets, `LauncherCheckMs`, minimum N, human hours, and every report target other than the stated gates.
- The tooling of 9.5: the Roslyn analyzer, the Core loader, the source generators, the DrawLedger and digest service, the draw tracer, the save writer, the FsCheck generators, the proof cache and the dashboard; CI infrastructure and hardware.
- Godot scenes for the seed field, the copy button, the build-mismatch notices, the seeded-run entry line, the version-crossed notice and the unravel card, with no rules in them.
- Names, until the naming pass, including the unravel card's words.
- **Not tuning:** the key algebra and its reference vectors; a child keyed from its parent's key; `Bound` everywhere; the registry handle, count classes and one stage per stream; `attempt/{a}` and `retry1`, never arithmetic; the attempt-invariant set and `dressing` attempt-scoped; DS1–DS15; the path grammar; one ladder in every build; camp-exit `GenInputs`; the GS order, AP1 and AP2, act-then-assert and the AP2 act order; failure classes and `floorRetry` only with a `proofRef`; the reproducibility key's fields; display outside the key; `FloorStatic` and `GenInputs` stored; the outcome rule; the class-dependent decision rules, the observed-spread reading and no optional stopping; verdicts never from a toy; **seeded runs advance no progress (agreed, Q1)**; **the release clean end with its kept, marked record and bug report (agreed, Q2)**; **the mid-run patch rule (agreed, Q3)**; **the snapshot-plus-journal slot, atomic replace on load and one slot (agreed, Q4; XI Q1)**.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13 and the requests in 11; by owner:

- **The main design owner:** SV-1, SV-5, SV-6 (MAIN 6.3), SV-9 (pointer sentence), SV-12 and SV-15 ("version number"), SV-13, SV-14 (MAIN 6.6, agreed Q4), SV-16 (profile save and the `runHistory[]` record), SV-31; the telemetry consent setting for bug reports.
- **The RS owner:** SV-8, SV-10 (R-SEED-6: the F1 defect redraw), SV-12, SV-19 (the V19 row), SV-20 (the 11.2 header and wave column), SV-23, SV-24 (`LauncherCheckMs`); the launcher-exhaustion reading.
- **The FL owner:** SV-1, SV-4, SV-9, SV-14 (FL 11.1), SV-20, SV-23 (the walker "gate"), SV-31; walker equivalences.
- **The room terrain owner (segment 3):** SV-3, SV-4, SV-24; the stamp before terrain.
- **The HZ owner:** SV-3 (`hzTagMax`), SV-4, SV-6 (`h:envSave`), SV-24 (`VoidClassDeathGap`); the hazards formula, the F1 environment scope, the Void proofs, the VP note.
- **The TL owner:** SV-1, SV-4, SV-6 (form kept), SV-7, SV-10, SV-11; the traps and trapsResolve formulas.
- **The EP owner:** SV-1, SV-2 (`encounters/plan` and `/place`), SV-6 (`lurkStealth` die), SV-10, SV-11, SV-20 (V7 → V12g; `ProtectedIntact`'s group part); with CMP, the `content.CMP8.*` rows.
- **The LE owner:** SV-5, SV-10, SV-31; the `dealShuffle/retry1` key; the V13g (f) quantity.
- **The IR owner:** SV-1, SV-5 (the Resistant entry); the `DormantAtChoice` proof.
- **The LP owner:** SV-1, SV-6, SV-10 (`LootBindDegrade`), SV-20; the V15 M1 rows, the HE/Due binding proof, the default `LootPolicy`, the merge-or-coin rule of `LootBindDegrade`.
- **The EV owner:** SV-1, SV-4, SV-6 (`h:curiosity`), SV-8 (EV 4.10 step 3), SV-13, SV-25 (200 profiles), SV-30, SV-31.
- **The BR owner:** SV-1, SV-2 (`boss9` as a `vector`), SV-9 (stamp step 5), SV-10 (`ArenaStampFallback` in every build), SV-12 ("logged in the key"); the stamp before terrain; template-local coordinates.
- **The XI owner:** SV-20 (V18r), SV-24 (`SecretFoundRate`); walker equivalences.
- **The DH owner:** SV-4, SV-9, SV-10 (agreed Q2), SV-18, SV-23 (reading), SV-31.
- **The enemies owner:** SV-5, SV-6 (ENM 12.2), SV-31.
- **The ITEMS owner:** SV-5, SV-31, SV-32.
- **The CN owner:** SV-2 and SV-5 (`loot/run` as a 3-long `vector`; phial always 3 draws).
- **The CMP owner:** SV-2 (recruit keys), SV-25, SV-26, SV-27, SV-31; the `content.CMP8.*` rows.
- **The ARC owner and the class owners:** SV-22; SV-20 (`NameRegistry` feeds).
- **The AR owner:** SV-5 (position read).
- **The simulator:** every instrument, report and arm of 6.
- **Art, audio and writing:** the unravel card, the seeded mark, the version-crossed mark and notice, the build-mismatch notices, the seeded-run entry line (placed by the camp and hub design: the hub's seeded-night row with the mismatch notices, the unravel card over the camp diorama; camp and hub design 4.4, 4.8; hook taken up).
- **Naming pass:** every placeholder name here.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Determinism critic (SD)

| Id | Finding | Resolution |
|---|---|---|
| SD1 | Out-of-combat noticing and Steal use the combat stream (ENM 1608), so exploration changes fights | `h:notice`, `h:steal` domains; `ExplorationCombatIsolation` (SV-6; M1) |
| SD2 | Showing an outcome before its durable write allows probing and death-scumming; the "overlap" fix widens it | The outcome rule; `ProbeResistance`; overlap only for draw-free wind-up (4.8.3) |
| SD3 | The "read, validate, delete, write back" crash window rewinds the floor | Atomic replace with the re-validated slot; `LoadPathKillFuzz` (WO7; agreed, Q4) |
| SD4 | A command journal cannot survive a patch | A state-delta journal; compaction at Downs, deaths, paid acts, clean quits; CRC truncation (4.8.4) |
| SD5 | Profile-held rules inputs are missing from the key | Difficulty tier, boons, Nightmare tier in the config snapshot; `CoreNoProfileRef` and `ConfigHashCoversRunConfig` (replacing the draft's vacuous `KeyCoversProfileGrants` after the check; SV-12) |
| SD6 | `ToggleIsolation` as drafted marks nearly every delta INVALID; "no seed changes" overstated | Draw-trace definition plus an influence closure; DS7 reworded (6.1, 4.5) |
| SD7 | Display interactions could enter the log or tick | UI-only; analyzer ban; intent-script `SharedSeedProfileBlind` (4.6) |
| SD8 | Daily seeds can be pre-computed; major-version compatibility is wrong; code parsing and check symbols fragile | Published or server seed, or told plainly; full compatibility check; ASCII parse; a check character (a weighted GF(32) symbol over seed and preset after the check); reference-snapshot daily as a future item (4.7, 4.9, 14.2) |
| SD9 | C# and Godot pitfalls missing (Godot JSON doubles, fractional content, overflow, id hygiene, background generation, ambient state) | DS11, DS13, DS14, K-rule 3; hostile-environment test (4.5, 5.2) |
| SD10 | Id renames move seeds; weighted picks remap; `genContentHash` deferred | `IdFrozen`; told plainly; `genContentHash` at M2 (4.10, 4.6) |
| SD11 | A retried F1 can lose a forecast Curiosity's host; the simulator bypasses the launcher, so F1 gates see a different F1 mix | `F1HostAlways`; `ForecastSurvivesRetry` asserts presence; `LauncherF1Neutral` (SV-30, 6.10) |
| SD12 | Domain aliasing; static file naming; pending profile writes on abandoned runs; seed range | `h:` prefix and `DomainDisjoint`; content-hash file names with garbage collection; `pendingProfile` flushed on load, abandon and finalise; seeds ≥ 2^60 rejected |

### A.2 Coverage critic (SX)

| Id | Finding | Resolution |
|---|---|---|
| SX1 | Model B's `Base.EP` baseline contradicts the agreed `PatrolFightRate` | `XI.BASE` per XI line 83 (approved surfaces plus the M1 minimap); Model A's "segment 10 off" wording also corrected (6.2) |
| SX2 | "Point estimate decides" and "marginal splits listed" loosen client gates | One-sided rule by class; straddling = FAIL for REL, SHIP-safety, BUDGET, HOLD, SW (6.4) |
| SX3 | De-noising redefines agreed numbers; the doubled N was not costed | Gate on the observed spread at N with noise ≤ 1 point; de-noised printed beside; costed (6.4, 8.4). No client question needed now; a future item if the cost bites |
| SX4 | `ProtectedIntact` at AP2 triggers HE-7 before EP's cheaper retry, without proof | Split: structural at AP1 (`floorRetry`), group part in V12g (`stageRetry`) (5.4) |
| SX5 | Bind failures routed to LR3 without proof; `GenerationRetried` overloaded | HE/Due binding proven at build; runtime failure = `defect`; `GenerationRetried` kept to FL's list (5.5) |
| SX6 | Retry budgets inconsistent; per-cell reads impossible | Per-pass LR4, `RetryRescueRate`, per-run budget, pooled reads; EP's < 1% read pooled (5.6) |
| SX7 | The release set cannot satisfy DH 18.2 | 2,000 runs per cap plus logged extensions; flagged reading (4.11) |
| SX8 | "The simulator writes nothing" is a decided-text change, not a reading | Flagged change SV-13, MAIN 497 and EV 1444 named |
| SX9 | EV's streak gate cannot decide at 10 profiles | 200 profiles; flagged change to EV 9.4 (SV-25) |
| SX10 | Runtime assertions have no release behaviour; V18g move unflagged | `runtimeAssert` class (fail closed); XI 9.2 and RS 11.2 rows flagged (SV-20) |
| SX11 | The novice arm is missing and blocks agreed M2 gates | Generalised `Skill.*` on the M2 planning gate as a blocker, with fallback (SV-22, 7) |
| SX12 | A suspended time model inverts the cut order | Human attribution of BIND lines, else the cap decision is blocked (6.7) |
| SX13 | Which cap's verdict signs off is undefined | Gates bind at the shipped cap; step 2b re-read (6.3, 7) |
| SX14 | Human-session hours never estimated | Hours table; camp-3 slip allowed (6.11) |
| SX15 | PR1 has no internal order | Intra-PR1 rule; pairs added to the blast radius (6.9) |
| SX16 | Owed zero-gate partners not blocking; F1 environment scope incomplete | Blocking deliverables; V9 and V11 co-partners; scope request to HZ (6.6) |
| SX17 | Release attempt counts disagree; Model B's nesting count understated | ≤ 3 attempts; background generation; nesting stated; safe floor offered only as Q2 B, rejected by the client and kept as a future item (5.5, 14.2) |
| SX18 | The V5 contest is unnecessary for M3 | Withdrawn; RS V5 kept as written through M3 (8.3) |
| SX19 | Build-time targets disagree; caching under-modelled | ≤ 5 min cold after SN5; cache keyed by declared read sets; worst-case warm PR (5.3, 5.8) |
| SX20 | Act-then-assert rerun spans under-declared | Every act declares its span; V19s `RerunSpanCovers` (5.4) |
| SX21 | "Two Doors (5 gates)" | 6 gates plus gate 7, corrected (6.3, 6.10) |
| SX22 | `ContactHazardDowns` and `LauncherCheckMs` disagree across drafts | Renamed `ContactHazardDeaths`; 5 ms p95 as REP until measured (6.10) |
| SX23 | The HZ rename touches more lines than cited | All sites listed (SV-24) |
| SX24 | RS 11.2 header and V11 row contradict TL and BR | Lines 2671 and 2685 in SV-20 |
| SX25 | Wrong CN section; `Floor1BossSustain` misplaced as a simulator row | CN 19; BUILD content check (5.2) |
| SX26 | V15 M1 rows still unitemised | Blocking LP deliverable on the M1 checklist |
| SX27 | SV numbering collides | One register SV-1 to SV-32 (A.6) |
| SX28 | DrawLedger needs two figures; formulas owed | Address space and spent (SN1); TL and HZ deliverables (SN2) |
| SX29 | The slot budget is impossible as written | Overlap budget plus the delta journal (4.8.4, SN13) |

### A.3 Consistency critic (SY)

| Id | Finding | Resolution |
|---|---|---|
| SY1 | Decided and settled changes filed as readings: AP1/AP2, the HE-7 trigger set, attempt-invariance and dressing, per-floor loot, the slot mechanism | All flagged with exact lines (13: SV-4, SV-5, SV-9, SV-10, SV-14); HE-7's trigger set not widened (SX5); `dressing` stays attempt-scoped; the slot mechanism is Q4 (agreed, option A); the outcome rule covers every irrevocable result |
| SY2 | HZ tag draws on terrain shift M1 draws at M2 | `hzTagMax` reserved from M1 (SV-3); a golden precondition |
| SY3 | Ladder budgets inconsistent; EP's < 1% tightened; release attempts too many | 5.6; EP read pooled, not tightened; ≤ 3 attempts |
| SY4 | V19 spent four ways | One name, `Conformance`; content suites unnumbered (`content.*`) |
| SY5 | BR 4.17 and RS 13 hooks unanswered; the V7 row's owner; retry log name | Template-local coordinates plus transform; event order as V17t; `drawableSet` at GS0.2; V12t with EP's acceptance (fallback V19t); `StageRetried` |
| SY6 | Name disagreements between drafts | `XI.BASE`; `ContactHazardDeaths`; all HZ sites; 5 ms; walker equivalence as an owner question; `Muster.Reference`; qualified ids over renames; trader floor-level |
| SY7 | Wave conflicts | curiosities lock at M2; `lurkStealth` **M2**, reserved and inert in M1 (corrected after the check: the draft's "the Ghillie lurker is M1, EP 2234" was wrong; EP 2234 lists the Ghillie outside its M1 roster, EP's M1 staging row says "M1 has no lurkers", and ENM 12.2 dates the keyed lurker Stealth to M2; XI-2's M1 change is the line-of-sight band, not a lurker); the phial key follows CN-27; keyed sorts before the M1 goldens; V5 kept; V15 rows a deliverable |
| SY8 | `combat/floor/{N}` fixes nothing; `cosmetic` reclassification | Dropped; `cosmetic` a text note only |
| SY9 | Draft local ids collide with corpus ids | GS, AP, LR, WO, DS, PR, SQ, SN, SD, SX, SY, all 0 hits; streams named by path |
| SY10 | `encounters` drawn at two stages | `encounters/plan` and `encounters/place`, counts unchanged (SV-2) |
| SY11 | Stale-text anchors missing | All anchors listed in SV-4 and SV-31 (DH 1020/1026 flagged as lead's-call text; HZ 929 exact) |
| SY12 | `EventDrawRunBlind` narrowing needs a flagged amendment | SV-30, flagged |
| SY13 | Single-change classifications | Salt and `genVersion` correctly flagged; 60-bit masking noted under R-SEED-6 (SV-16) |
| SY14 | Readings to confirm with owners | "Writes nothing" became a flagged change (SV-13); the R-SEED-6 exhaustion reading carried as an open RS dependency |
| SY15 | The DH 18.2 three-role reading is sound | Kept; told plainly (6.3) |

### A.4 Judges' grafts not taken, or changed

| Graft | Disposition |
|---|---|
| Split-half de-noising as the gating statistic (all three judges) | **Changed:** reported beside the gate; gating stays on the observed spread (6.4; SX3) |
| Model A's `Root()` domain constant and length terminator (one judge) | Not taken: Model B's arity-tagged algebra is adopted verbatim so its computed vectors stand |
| Model B's `safeBlueprint` per theme (one judge as fallback) | Offered only as Q2 B; rejected by the client (agreed, Q2 A), kept as a future item (14.2) |
| Model A's `combat/floor/{N}` | Not taken (SY8) |
| Model B's `Base.EP` for `PatrolFightRate` | Rejected (SX1) |
| `PatrolFightRate` baseline renamed `Baseline.Seg10Off` (critic) | Not taken: XI's approved name "BASE" kept, qualified as `XI.BASE` |
| A's `dressing` attempt-invariance | Not taken (SY1) |
| A's `ContactHazardDowns` re-basing | Not taken; renamed instead (SX22) |
| V5 prefix contest in M3 (both models) | Withdrawn to M4 (SX18) |
| `BindRetried` event (critic option) | Not taken: release defects use `GenerationDefect` on the defect path |
| Simulator layer's `Walk.Rush` | Renamed **`Walk.Beeline`** ("Rush" has 17 hits) |
| LauncherCheckMs 50 ms (Model B, simulator layer) | 5 ms p95, REP until measured |

### A.5 Names checked (case-insensitive, whole word, 29 approved documents)

**0 hits:** the prefix `SV-`; `V19`; `Conformance`; local schemes GS, AP1/AP2, LR, WO, DS, PR, SQ, SN, SD, SX, SY, `Q-SV`; `GenInputs`, `FloorStatic`, `genVersion`, `manifestHash`, `displayHash`, `genContentHash`, `rulesVersion`, `saveVersion`, `floorKey`, `attemptKey`, `DisplayKey`, `StreamSpec`, `hzTagMax`, `pendingProfile`, `mixedVersion`, `nonComparable`, `buildTag`, `ShareCode`, `SeedCode`; `h:envSave` (`envSave`); the checks and tests `DrawLedger`, `DrawLedgerPerRun`, `DrawTrace`, `ToggleIsolation`, `KeyAlgebraVectors`, `StreamIndependence`, `ContentAddStability`, `RetryIsolation`, `AP1EarlyEquivalence`, `ForecastSurvivesRetry`, `ExplorationCombatIsolation`, `SaveDigestMatch`, `SlotEquivalence`, `KillFuzz`, `LoadPathKillFuzz`, `ProbeResistance`, `SharedSeedProfileBlind`, `KeyCoversProfileGrants`, `DomainDisjoint`, `RerunSpanCovers`, `DrawFreeProof`, `IdFrozen`, `F1HostAlways`, `StampProven`, `BlockIndependence`, `HostileEnvironment`; the reports `RetryRescueRate`, `DegradedRunShare`, `TimeModelError`, `FloorClockLedger`, `VoidClassDeathGap`, `ContactHazardDeaths`, `LauncherF1Neutral`; the events `StageRetried`, `GenerationFallback`, `GenerationDefect`, `RuntimeAssert`, `LootBindDegrade`, `BindRetried` (unused); harness names `LauncherEmu`, `safeBlueprint`, `simVersion`, `Beeline`; "dream unravels".

**Added while writing (0 hits, grep re-run):** `SeededRunWritesSeenOnly`, `SaveMigrationGoldens`, `RunUnravelled`, `unravelled` (a run result), `genVersionsUsed`, `grantsHash` (a fallback only).

**Added after the check (0 hits, grep re-run):** WO8, DS15; `runHistory`, `RunHistoryEntry`; `OutcomeWaitMs`, `OutcomeWait`; `ShareCodeErrorDetect`, `SpeculationEquivalence`, `SpeculationDiscarded`, `DevReleaseParity`, `UnravelPath`, `ClassDeclared`, `CoreNoProfileRef`, `ConfigHashCoversRunConfig`; `GenMaxAttempts`, `CanonicalWriter`; `suspend.base`, `suspend.journal`, `SlotBaseHeader`, `JournalRecord`, `baseSeq`, `attemptUsed`; the count classes `leaf-formula` and `vector` (the word "vector" has 3 prose hits, none a name); `content.CMP8.*`; "version-crossed" (a mark).

**Kept on purpose (existing approved names, read):** `LauncherBypass` (2 hits, EV V16t); `LauncherCheckMs` (2, RS); `seedMode` (2, EV's `recentRuns` field; after the check the name is reused as a `SaveHeader` and `runHistory[]` field, because a seeded run never writes `recentRuns`, 4.7); `runId` (5 hits, EV's `curiosityMemory` and events; reused as the run's save identity, 4.8.3); `lurkStealth`, `retry1`, `floorSeed` (EP, TL; aliases); `RecruitDrawFixedCount`, `candidateDrawMax`, `maxSocketsPerRank` (CMP, LE); "release seed set" (15 hits, the term this document defines); `GenerationRetried`, `V11Fallback`, `V12FloorFallback`, `LootBindUnplaceable`, `ArenaStampFallback` (approved events).

**Descriptive or field names reused on purpose (added while writing):** `comparable` (a save-header field; the word has 6 prose hits, none a name); "abandon" (EV's approved run outcome, read); "bug report" (8 prose hits, a common noun).

**Rejected after collision:** "Rush" (17 hits); "seeded" as a field value (46 hits, mostly "seeded test"); SR (184), SC (34), KR (34), KC (31), LB (22), CF (21), LK (21) as id schemes; single-letter numbered schemes L, G, P, S, W, K, D, B, F; `SeedTree`, `SeedsAndValidation`, `StreamsAndReplay`, `CoreContent` as V19 names (withdrawn for one name).

### A.6 SV cross-reference

The determinism-replay draft (SV-1 to SV-22), the procgen draft (SV-1 to SV-21), the stream audit (SV-1 to SV-18), the validator audit (SV-1 to SV-22), Model A (SV-1 to SV-28), Model B (SV-1 to SV-32) and the simulator layer (SV-30 to SV-47) all numbered independently. They are renumbered into this register by topic:

| This register | Main draft sources |
|---|---|
| SV-1 key algebra | A SV-2; B SV-2; det SV-1; procgen SV-2 |
| SV-2 registry | A SV-1, SV-3; B SV-1, SV-3 |
| SV-3 `hzTagMax` | consistency critic SY2 (new) |
| SV-4 attempt node | A SV-4; B SV-4; det SV-4 |
| SV-5 loot keys | A SV-5; B SV-5, SV-6; det SV-5 |
| SV-6 keyed checks | A SV-8; B SV-9, SV-12; determinism critic SD1 |
| SV-7 salt | A SV-9; B SV-10 |
| SV-8 keyed sorts | A SV-10; B SV-11; det SV-12 |
| SV-9 pipeline | A SV-6; B SV-7 |
| SV-10 ladder | A SV-7; B SV-8 |
| SV-11 budgets | numbers R1/R2 (new) |
| SV-12 key | A SV-13; B SV-13 |
| SV-13 outside the seed | A SV-14; B SV-27; sim SV-31 part |
| SV-14 saves | A SV-15; B SV-16, SV-17 |
| SV-15 versioning | A SV-16 (the draft's "B S7" was not an SV id and is removed after the check) |
| SV-16 codes | A SV-17; B SV-18 |
| SV-17 shared, daily, replays | A SV-18; B SV-19 |
| SV-18 goldens | A SV-19; B SV-20 |
| SV-19 V19 | A SV-20; B SV-21 |
| SV-20 validator model | A SV-21, SV-22; B SV-22–SV-24 |
| SV-21 simulator | sim SV-31, SV-42, SV-44; A SV-24 |
| SV-22 arms | sim SV-32; B SV-26 |
| SV-23 result classes | sim SV-30; A SV-23 part |
| SV-24 report names | sim SV-33, SV-41; B SV-25 |
| SV-25 statistics | sim SV-34–SV-37; B SV-28 |
| SV-26 precedence, ledger | sim SV-38–SV-40; B SV-29 |
| SV-27 human plan | sim SV-43 |
| SV-28 CI | A SV-26; B SV-31 |
| SV-29 checklists | A SV-27; B SV-32; sim SV-45, SV-46 |
| SV-30 `EventDrawRunBlind` | consistency critic SY12; determinism critic SD11 (new) |
| SV-31 text hygiene | A SV-28; B SV-14 |
| SV-32 trader | A SV-11; B SV-15 |

Not carried: A SV-12 (`combat/floor/{N}`); A SV-25 and B SV-30 (the V5 contest); sim SV-47 (folded into 7 M3 and 14.2).

The options the client rejected (Q1 B and C, Q2 B and C, Q3 B, Q4 B) are not register rows.

## Appendix B: sources

- Game and library details in section 1 carry their confidence (confirmed, reported, memory); none marked "memory" is load-bearing for a rule here.
- The segments 11–12 proposal (the synthesis), its four research drafts (determinism and replay, procedural generation, the stream audit, the validator audit), Model A and Model B, the simulator and sign-off layer, the numbers pass and its toy estimates, the scratch reference implementation that computed the 4.1 vectors, and the judges' and critics' reports; illustrative only, kept in the design lead's scratch area, not in the repository.
- The 29 approved documents listed in the header.
