# Boss Rooms: Design (Dungeon Generation, Segment 9)

Date: 2026-11-22
Status: Approved, 2026-11-22
Parents (immediate): `2026-11-22-events-design.md` (events design, "EV", segment 8, approved 2026-11-22, **the most recent template**), `2026-11-16-enemies-design.md` (enemies design, "ENM"; 13.2, 13.6, 17.3–17.5 and 21 are the boss frame), `2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change ids are RT-n), `2026-11-21-floor-layout-design.md` ("FL", segment 2), `2026-11-21-room-terrain-design.md` ("ROOM", segment 3; "RT" in section numbers such as RT 6.11; its change ids are TR-n), `2026-11-21-hazards-design.md` ("HZ", segment 4), `2026-11-21-traps-locks-design.md` ("TL", segment 5), `2026-11-21-encounter-placement-design.md` ("EP", segment 6), and the loot designs `2026-11-22-loot-economy-design.md` ("LE", segment 7a), `2026-11-22-item-rolls-design.md` ("IR", segment 7b) and `2026-11-22-loot-sources-design.md` ("LP", segment 7c).
Other parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "C" in section numbers, "CON" in prose),
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

Items marked **(agreed)** are the client's answers of 2026-11-22 to the five questions this design raised (Q1–Q5; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document. The client also said that everything else in the lead's proposal stands as written: Model A as the chassis (authored ArenaTemplates with Guises, draw-free stamping proven offline) with its grafts (Renditions, OneDueThing, the Dream contract; Model C as the M3 layer); `ArenaEngaged` replacing "seal"; the Rhythm (Offbeats, Lead beats, Downbeats) and telegraphs; phases with the two authoring rules; escorts, reserves and adds; boss AI; counterplay with one class-blind Device per boss, answer windows per class, Upended and the immunity rules; the action-economy narrowings (BR-7) and HP-and-bodies scaling, with Shorthanded rejected; the floor-1 safety additions (BR-22); variety and Renditions; signatures, the reward card, the Viaticum and the Memento; F-BOSS facts; the final bosses as keepers of the sleep and the Heavy Thing; M3 Stirs, Omens and Nightmare; determinism; UI and segment hooks; the full per-boss catalogue for all 17 bosses; readability, art, animation and audio; the new mechanics adopted, held and rejected (Prompts as an M3 option); the numbers and fixes (the toy, BN1–BN14); data, V17 `BossRooms`, reports and arms; staging; ownership and hooks; the binding requirements; BR-1 to BR-41; the told-plainly items; the risks; and the appendix resolutions and names. The client approved this document as a whole on 2026-11-22.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-22, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-22. Items marked **(lead's call, after the check)** were made when the written document was checked by the builder, consistency, play and art-and-audio lenses; none changes a client answer; they are listed in 14.1 with the check's "Told plainly after the check" items, the client confirmed them all on 2026-11-22 (explicitly including A13, the one-fight rule, and A28, the small-party upper bound, as written). Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change BR-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-22 (with Q1–Q5 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge, a critic or a draft, or state an unwelcome forecast, and were told to the client in plain words (14.1, including those told after the check); the client confirmed them all on 2026-11-22. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q5 of this document (2026-11-22), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-22 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(lead's call, after the check)** | A lead's call made when the written document was checked; never overrides a client answer; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(change BR-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text are flagged ⚑ there |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a judge, a critic or a draft, or states an unwelcome forecast; told to the client in plain words (14.1) and confirmed by the client on 2026-11-22 |
| **(hook)** | Owned by another segment or design; segment 9 states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator (8, 9). Never binding |

**The client's brief.** Dungeon generation segment 9: **boss rooms**. It covers:
- the boss arena and the boss fight as a generated and authored set piece: arena generation, the seal, the Forecourt and Stairhead interfaces;
- fight structure: phases, rhythms and signature moves telegraphed under the Dream step and countdown rules, escorts and adds, boss AI inside its one-step limits, enrage or not, and the boss counterplay that segment 5 left to segment 9;
- authored rhythms for every boss in every theme, the F5 final-boss pool, and variety and repetition across runs, including **the carried RS 12 Cut 1 question** (EV-44; RS 12, 19.2) and segment 8's request to prioritise F1 boss variety (agreed, events Q2);
- signatures and their carriage, the reward card and the Viaticum, boss facts, party-size scaling, floor-1 boss safety, the final boss and the Dreamer premise, Omens and Nightmare tiers;
- determinism, data, validator V17, reports, and hooks to segments 10 and 11–12.

Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; combat is key; contest the client or approved views where warranted.

The client's five answers (Q1–Q5, **agreed**, 2026-11-22) are in 0.2: **RS 12's Cut 1 becomes graded: 1a drops the Appetite only if it is not yet built, 1b drops the Thing in Cell Zero and folds it into the Jailer as a Rendition, Cut 2 gains the Cut 2R rider (keep the Appetite), and 1c (the Master in the Frame) moves below Cut 2 and is client-only; every F1 boss gets 3 Renditions in M2, the launcher F1-boss streak rule ships in M2 and the Wake's gate is prioritised (Q1). No short rest inside the Sanctum from first engagement until the boss falls; rests elsewhere on the floor stay legal (Q2). Gambits move from Later into M3, gated, never on F1, with the Frozen Toast exception (Q3). Every F2–5 Boss Court includes at least one non-fearless escort or reserve group (Q4). RS 8.11's Later "Dreamer as a true-ending boss" is replaced by a bodiless set piece (Q5).**

**Change prefix.** This design uses **BR-n**. A case-insensitive grep for `\bBR-[0-9]` across the 27 approved documents finds one hit, inside a URL in the classes design (`.../br-2024/...`), which is not an id; there are no other hits (re-run while writing). Every draft used colliding local BR numbers. This document issues one register, **BR-1 to BR-41** (13), and no id has two meanings (A.6).

**Validator.** **V17 `BossRooms`** is free. V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP and V16 EV. A grep for `\bV17` finds 0 hits, and `BossRooms` 0 hits (re-run while writing).

**Local id schemes** (each grep-checked as a whole word, 0 hits):
- tensions **BT1–BT24**;
- toy failures **BN1–BN14**;
- catalogue rules **BK1–BK12**;
- the arena charter **BX1–BX10**;
- Appendix A findings **FN** (fun critic), **BL** (balance critic) and **CS** (consistency critic).

This document's client questions **Q1–Q5** are local, as in every parent.

**Names are placeholders** until the client naming pass. Every new name was grep-checked case-insensitively as a whole word across the 27 approved documents (A.5); the check was re-run while writing. Kept on purpose: "the Heavy Thing" echoes RS 9.3's own phrase "drop the heavy thing"; "Pendulum", "Bell Rope" and "sluice" are RS 13's own counterplay words. Two names are changed while writing **(new lead's call)**: the Master in the Frame's R2 Device "the Empty Frame" becomes **the Turned Portrait**, because "an empty frame" is RS RC1's and TL's render of a Splintered lock; and the Drowned Carillon's second handle is **the sluice**, never "Sluice Wheel", which names HZ's non-arena Cord (HZ keeps non-arena look-alikes distinct). **Renamed after the check (lead's call, after the check; A.5):** the Night Governess's at-will "Hush" (the Herald's heal kind) becomes **There, There**; Old Ragged's R3 "Empty Chair" (the Supper's Obsession) becomes **Drawn Shutters**; the Precentor's "Spark" (a withdrawn Arcanist name) becomes **Arc Note**; Old Ragged's 50% "the Maul" (a weapon) becomes **the Pin-Down**; the Master's "Horn" becomes **the View Halloo** (horns are reserved for joiners); the drafts' formation names "Retinue variants (Court, Screen, Wings)" are dropped, because Retinue is EP's own formation and Wings names FL Blueprints (4.5.1); and the folded "Chained Thing" is replaced by a chained **Lifer**, the Gaol's approved brute (4.10.2).

**Numbers in this document are illustrative** unless a rule says otherwise, and are marked *(illustr.)* where they first appear. Every rate comes from the segment 9 toy (8, Appendix B); the simulator owns every published figure. **Exceptions (new lead's call):** the launcher rule's memory of **2** personal runs (`launcher.f1BossMemory`), the **3** Renditions per F1 boss in M2 and the Cut 1 step order are the client's agreed values (Q1), not tuning; a gate miss that would move them returns to the client (14.2).

**Waves:** **M1** slice, **M2** full run, **M3** between runs (profile, unlocks, ladder), **M4** breadth, **Later** **(settled, MAIN 9)**. **M2g** means M2 if its gate passes, else M3.

**Abbreviations:**
- F1–F5: floors 1–5.
- "Lowest legal hero": the hero at the lowest legal Con for its class (VG-24).
- "Party units": non-Downed party units at round start (BR-21); used for the boss-action count only.
- "Arena party size": the number of party members when the floor is generated; it fixes formation fill, AddCap and reserve trimming for the whole floor **(lead's call, after the check)**.
- pp: percentage points.
- "Commit": a committed boss move (a Gambit, M3).
- "Headline": an announced but unresolved event (a Gambit, a due Stir act, a due reserve, a due (e) change).
- "The Sanctum": the Forecourt, the arena, the Bossgate and the Stairhead (FL 6.11).
- "Built" (Cut 1a): a boss whose kit, `ArenaTemplate` and rig have passed V17s at the M2 schedule review that applies the cut **(new lead's call)**; recorded in data as `cutState.appetiteBuilt` (9.1) **(lead's call, after the check)**.
- "Effective kit": a boss record with its Rendition's overrides applied (9.1); every V17s row reads the effective kit **(lead's call, after the check)**.

---

## 0. At a glance

### 0.1 The lead's answers to the brief

**The design in one line.** *Every boss is a hand-authored fight in a hand-authored room: a visible Rhythm tells you when its big move comes, one class-blind Device in the room lets you answer it, and seeded Renditions of the same boss change the question from run to run, all inside the approved boss frame and drawn without a single runtime arena draw.*

#### What a boss fight is here
- One authored boss in one authored arena, at the end of every floor **(settled, ENM 17.5; RS)**.
- The fight asks **one question per boss**: "when do you stop time?" (the Jailer), "can you keep out of its sight?" (Old Ragged), "will you dunk the showman?" (the Strongest Man). The question is answered through the arena's **Device** (4.7) and the boss's **Rhythm** (4.3).
- It is fair before it is hard: nothing that can kill is unknown before it lands. Every big hit is a named signature on the Rhythm strip with a banner behind it; every add has a visible socket and a round of warning; every rule change at a threshold is announced by type from turn 1.
- No enrage and no damage cap (BR-15). Pressure comes from the Rhythm, the reserve, the arena clock where one exists (M2g/M3) and the party's own resources.

#### Arenas
- **Model A chassis (BR-1, BR-2).** Each boss names one authored `ArenaTemplate`, with **3 Guises on F1 and 2 elsewhere**, each mirrorable, each at one authored size per floor band. Generation stamps it into FL's 2×2-cell `arenaCell`, rotated to face the Forecourt. **Stamping makes no draws.** Every Guise × mirror × 4 rotations is proven offline by V17s, so a stamp cannot fail at runtime.
- All approved caps hold: FL 6.3 sizes and the long axis of 21; FL 6.11 Arches, start zones ≥ 9 and `AntechamberScreened`; RT 6.11 terrain caps; HZ 6.17's at most 1 Stir and 1 Void **(settled)**.
- The events design's "arena seal" is defined as **`ArenaEngaged`** (BR-3). Nothing is ever sealed, and no UI says "seal".

#### Phases and rhythms
- Phases are read as approved: 50% on F1, 66/33 on F2–5, damage carries through, exactly one authored change on entry **(settled)**.
- Two authoring rules make them land (BR-9): the **last threshold** must change the board or the kit ((a), (d) or (e)); and any **(b)** entry must change the plate's stance line **and** the Rhythm's shape, so it is never invisible.
- Each phase band has a **Rhythm** (BR-6): a short loop of Offbeats, Lead beats and **Downbeats** (signatures), shown as marks on the plate. It is a preference order for the settled one-step scorer, not a wind-up and not a map clock. Its pointer rules (waiting, pending signatures, phase carry, resets) are exact (4.3.1).
- Every signature lands on the boss's **own** turn, after at least one full round of banner, including after a reset.

#### Action economy against parties of 1–4
- **No legendary actions, no lair initiative slot, no extra boss turns** (told plainly). The approved A-3 count stands: min(1 on F1–2 or 2 on F3–5, party units), at most one per unit turn end, never after the boss's own turn **(settled)**.
- Narrowings, in one flagged bundle (BR-7): **Signature is never a boss action**; on **F1** the menu is **{Reposition, Shed, Command}** (the toy shows any Strike or Signature boss action breaks the F1 75% spike row); **Shed** removes only a condition present at the start of the round; no two boss actions on one unit in a round; no boss action on a unit standing in a live committed footprint (M3).
- "Party units" means non-Downed units at round start, so a companion cascade lowers the boss's action count (BR-21).
- Scale **HP and bodies, never attacks or tempo**. The drafts' "Shorthanded" kit downgrade for parties of 2 or fewer is **rejected** as an empty-slot bonus the companions design forbids (told plainly; BR-21).

#### Class counterplay and immunities
- Every boss authors an **answer window per launch class** (BR-19), checked by V17 and measured by `ClassBossContribution`.
- Vanguard: every boss is ≤ Large (BR-18), and Expose Devices, catch squares that a shoved boss triggers, and **Upended** (BR-17) give pushes a payoff that `noDrop` bosses otherwise deny.
- Shade: Tall flank pairs; the Offbeat after a Downbeat.
- Arcanist: reserves arrive clustered at a known socket and never act on arrival; every Device has a ranged trigger.
- Herald: fear spends pips; a visible "Resolve Broken" beat when the pips run out; F1 formations that start a breakable group outside the boss's aura; and **every F2–5 Boss Court includes at least one non-fearless escort or reserve group** (narrows HR-21; **agreed, Q4**), placed in the escort at fight start wherever the budget allows (lead's call, after the check).
- Immunities (BR-20): **final bosses take no damage immunity** (the toy shows +14 to +28 death points when one blanks a party member's damage type); F1 bosses take none either; no boss resists its theme's `counterType`.

#### Floor-1 safety
- Every approved row is kept: ≤ 25% expected per round; lowest-legal spike ≤ 75%; no one-round kill even with a crit; the F1 cumulative 2.5%; `Floor1BossSustain` at L3 **(settled)**.
- The toy shows those rows **do not bound F1 deaths** (12–45 per 100 arrivals at the authored Jailer dice). So segment 9 adds (BR-22): an F1 fight-level row that counts the escort; a **simulated `BossDeathShare` F1 gate ≤ 2.5 per 100 arrivals**; `readyAt` ≥ 2 on every F1 limited ability; F1 Devices that never harm; an F1 fight-length target (median 4–6, p90 ≤ 8 rounds); and, from M2, a Catch-up guard so the hero can never meet the F1 boss below level 3.

#### Variety across runs, and the carried RS 12 Cut 1 question
- The true F1 same-boss repeat between consecutive runs, with the approved R-SEED-6 theme cap, is **22% with the M2 core and 44% under approved Cut 1** (not 25% and 50%).
- **Renditions** (BR-23) give each F1 boss **3** seeded variants in M2 **(agreed, Q1)**. Each must change the fight's question (a phase option, the Rhythm's shape, the signature or its condition, or the Device), never only the floor plan, the formation or the opening beat; each carries a visible tell on the rig.
- **Cut 1 is graded (agreed, Q1; BR-24 to BR-26).** 1a drops the Appetite **only if it is not yet built** (the Gut is built first, so its saving may already be spent); 1b drops the Thing in Cell Zero and folds it back as a Jailer Rendition; Cut 2 gains the **Cut 2R** rider (keep the Appetite, free if built); 1c, the Master in the Frame, moves below Cut 2 and is client-only. The launcher F1-boss streak rule ships in M2 and the Wake's gate is prioritised. Measured: same F1 boss 26.5%, same fight 8.7%, three-in-a-row ≈ 0%, at about 2.58× against approved Cut 1's 2.45×.
- **Told plainly:** because the Gut is built first, 1a may save nothing; graded steps relieve less schedule per cut, so under real slippage Cut 2 (the Supper) may arrive sooner.

#### The final boss and the Dreamer
- Final bosses are **keepers of the sleep, never the sleeper** (BR-30). The Dreamer appears only as visible environment (the mattress heave during acts, the sheets and bedframe shifting, the waking-light window), never as sound attributable to a sleeper, checked by `FinalBossNoDreamer`.
- Both F5 arenas share **the Heavy Thing**, RS 9.3's fixed "drop the heavy thing" rule: a 3×3 drop dealing **piercing** (inside the Heart's damage set) plus Prone, dressed by the floor-4 Keepsake in neutral materials, never ice. Each final boss also has its own Device, so the last fight is the most distinctive one.
- **RS 8.11's Later "Dreamer as a true-ending boss" is replaced by a bodiless set piece (agreed, Q5):** after the last keeper falls, the room wakes and the party walks into the waking light. It is display only and cannot be failed: victory is recorded at the last keeper's `BossDefeated`. A unit with a model is a body; RS 7.5 cannot survive a Dreamer boss.

#### Signatures and rewards
- Carriage follows IR's approved per-form rule; segment 9 authors `armed` and issue gear so that a carried signature is always the weapon the boss visibly swings (BR-27).
- On the boss, the item supplies **only its chassis** (die and reach); its lines are display only, so items never enlarge boss kits. The hit's damage type follows the theme.
- The reward card, the Perilous 1-of-4 card, the Viaticum and the Memento are read unchanged **(agreed)**. The F5 signature never takes a guarded second form (told plainly; a flagged IR row).

#### The rest loophole
- Both critics found that a party can burst a boss past a threshold, step out and rest in the now-restable Forecourt. A-4 **deliberately** allows retreat-rest-return at a supply cost **(settled)**, so segment 9 does not ban it. It closes the cheap form: **no short rest inside the Sanctum while an engaged boss lives** (BR-31; **agreed, Q2**). Rests elsewhere on the floor stay legal, so A-4 is intact. The same loophole without a rest, stepping out to reset party cooldowns, is closed by treating the boss fight as one fight for the party's per-fight resources too (4.2.5; lead's call, after the check; told plainly). A floor-wide ban was rejected by the client; if `MidBossRestRate` fails its gate, the lead may raise it again (14.2) **(new lead's call)**.

### 0.2 The client's five decisions (agreed, 2026-11-22)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | The RS 12 Cut 1 order (carried from EV-44) | **A, graded.** 1a drops the Appetite only if not yet built; 1b drops the Thing in Cell Zero and folds it into the Jailer as a Rendition ("Cell Zero Opens"); Cut 2 gains the Cut 2R rider (keep the Appetite, free if built); 1c, the Master in the Frame, moves below Cut 2 and is client-only. Plus 3 Renditions per F1 boss in M2, the launcher F1-boss streak rule in M2, and the Wake's gate prioritised. **Told plainly:** the Gut is built first, so its saving may already be spent; graded steps relieve less per cut, so Cut 2 may come sooner | B: the approved order plus F1 Renditions and the launcher rule (same boss 34.7%, same fight 14.5%). C: F1 extras cut last of all, behind the Supper | BR-23 ⚑, BR-24 ⚑, BR-25 ⚑, BR-26 ⚑ |
| Q2 | Resting while an engaged boss lives | **A.** No short rest inside the Sanctum (Forecourt, arena, Stairhead) from first engagement until the boss falls; rests elsewhere on the floor stay legal (A-4 intact) | B: no rest anywhere on the floor between first engagement and the boss's defeat (narrows A-4); not pre-approved as a fallback. C: keep the Forecourt restable throughout | BR-31 ⚑ |
| Q3 | Gambits (committed boss signatures) | **A.** Pulled from Later into M3, gated, never on F1, with the Frozen Toast exception (the Head of the Table's Frozen Toast may be its Gambit) | B: M3 without the exception. C: keep them Later | BR-32 ⚑ |
| Q4 | A breakable group in every F2–5 boss fight | **A.** Every F2–5 Boss Court includes at least one non-fearless escort or reserve group, so the Herald's fear and T7's Rout always have a target (narrows HR-21, agreed 2026-11-20) | B: keep HR-21 as agreed and measure the gap with `ClassBossContribution` | BR-19 ⚑ |
| Q5 | The Later "Dreamer as a true-ending boss" | **A.** Replace RS 8.11's Later line with a bodiless set piece: after the last keeper falls, the room wakes and the party walks into the waking light | B: keep the Later line as a premise item for RS 19.2 | BR-30 ⚑ |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Model A as the chassis, with grafts from B, C, the rhythm layer, the catalogue, the new-mechanics doc and the toy (3) | M1–M3 | lead's call |
| 2 | Authored arenas, Guises, draw-free stamping, offline proof, template-local AI tie-breaks (4.1) | M1/M2 | lead's call; BR-1 ⚑, BR-2 ⚑ |
| 3 | `ArenaEngaged` defines the arena seal; once per floor; arena entry as a notice; behind the F1 Catch-up from M2 (4.2.1) | M1/M2 | lead's call; BR-3 ⚑ |
| 4 | Take Places at first engagement; the Playbill boss card (4.2.2, 4.12) | M2 | lead's call; BR-4 ⚑, BR-5 ⚑ |
| 5 | Rhythm and the Rhythm strip (4.3.1) | M2 | lead's call; BR-6 ⚑ |
| 6 | Boss-action bundle: no Signature boss action; F1 menu without Strike; Shed timing; focus rules (4.8.1) | M2 | lead's call; BR-7 ⚑ |
| 7 | Telegraph and spike budget on the settled rows' own crit-inclusive basis (4.3.3) | M2 | lead's call; BR-8 ⚑ |
| 8 | Phase authoring rules; OneDueThing; the exact Dream contract for (d) and (e) (4.3.4, 4.4) | M2 | lead's call; BR-9 ⚑, BR-10 ⚑ |
| 9 | Reserve sockets; AddCap; Few-and-Strong; patrol joiner counted (4.5) | M2 | lead's call; BR-11 ⚑, BR-12 ⚑, BR-13 |
| 10 | One class-blind Device per boss; Upended; denial budget (4.7) | M1/M2 | lead's call; BR-16 ⚑, BR-17 ⚑ |
| 11 | Sizes ≤ Large; answer windows; immunity rules (4.7) | M2 | lead's call; BR-18 ⚑, BR-19 ⚑, BR-20 ⚑ |
| 12 | F1 package with a simulated death gate (4.9) | M1/M2 | lead's call; BR-22 ⚑ |
| 13 | Renditions (3 per F1 boss in M2), folded bosses, launcher streak rule, graded Cut 1 with Cut 2R (4.10) | M2 | **agreed, Q1**; BR-23 ⚑ to BR-26 ⚑ |
| 14 | Sanctum rest rule (4.2.5) | M2 | **agreed, Q2**; BR-31 ⚑ |
| 15 | Gambits (committed signatures) from Later to M3, gated, with the Frozen Toast exception (4.14.3) | M3 gated, else Later | **agreed, Q3**; BR-32 ⚑ |
| 16 | A non-fearless group in every F2–5 Boss Court (4.7.4) | M2 | **agreed, Q4**; BR-19 ⚑ |
| 17 | Keepers not sleeper; the Heavy Thing; final-boss Devices; a bodiless true ending (4.13) | M2 / Later | lead's call; **agreed, Q5** for the Later line; BR-30 ⚑ |
| 18 | Arena Stirs: M2g pilots in two arenas; M3 Keynote layer; HZ 6.17 rows completed (4.14.1) | M2g / M3 | lead's call; BR-33 ⚑ |
| 19 | V17 `BossRooms`, reports, corrected death bands, BossMinutes ≤ 7 (9) | M2 | lead's call; BR-38 ⚑, BR-39, BR-40 ⚑ |
| 20 | Rejected: enrage, damage caps, legendary actions, lair slot, Shorthanded kit downgrades, generated arena fill, Downbeat-cued clocks, Break pip strip in M2 (7) | — | told plainly |
| 21 | The check's fixes: exact Rhythm, Device, headline, reserve, engagement and Gambit rules; Renditions re-authored; the one-fight rule; the small-party upper bound; readability, asset manifest and audio rules (14.1, A1–A33) | M1–M3 | lead's call, after the check; BR-3 ⚑, BR-14 ⚑, BR-31 ⚑, BR-38 ⚑, BR-40 ⚑ |

### 0.4 Intent, goals and anti-goals (new lead's call: added in the template's shape)

**Intent.** The boss is the floor's exam and the run's most remembered fight. Segment 9 makes each one a hand-built question with a visible answer, so that a death at a boss is a lesson the player can name, and so that the fifth meeting with the same F1 boss still asks something new.

**Goals.**
1. Every boss has one rule-changing idea (its Device and Rhythm), readable in a glance.
2. Nothing that can kill lands unannounced: signatures are bannered Downbeats, adds have visible sockets, threshold changes are typed from turn 1.
3. F1 bosses are safe enough to teach (≤ 2.5 deaths per 100 arrivals, simulated) and short enough to enjoy.
4. Every launch class has an authored window in every boss fight.
5. The F1 boss changes its question across consecutive runs; a same-fight repeat is rare.
6. Arena generation is draw-free and proven offline; boss variants never move another seed.

**Non-goals.** Boss identity and rosters (RS); the Forecourt and Sanctum layout (FL); terrain, hazard and trap rules (segments 3–5); escort budgets and joiner rules (EP, ENM); loot value and item identities (7a, 7b); the screens themselves (segment 10); the seed tree and validator plumbing (segments 11–12).

**Anti-goals.**
- Enrage timers, damage caps, refilled bars and HP sponges.
- Legendary actions, lair initiative slots and off-turn signature spikes.
- Ambush spawns, adds that act on arrival, and summon spam.
- Puzzles with one answer, and Devices that are mandatory.
- Small-party bonuses that reward empty slots.
- A body, face or voice for the Dreamer.

---

## 1. How games design bosses

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Into the Breach | Every attack committed to exact squares a turn ahead; pushes are the core verb | Committed boss moves scored at present value (Gambits, M3); push payoffs (Upended) | Bosses that are stat bumps of ordinary units |
| Slay the Spire | Act boss shown at act start; exact intents; visible counters (Time Eater); boss relic 1 of 3 | The Playbill; the Rhythm strip; the reward card (read) | Act-1 staleness from 3 bosses on the most-replayed act |
| Hades | Three Fury variants on one arena slot; Extreme Measures add kind | Renditions on one rig; Nightmare Renditions (kind, not stats) | Long unskippable intros |
| XCOM 2 | Chosen with seeded strengths; Alien Rulers react to every action | Seeded, revealable Renditions; the strict A-3 cap | Ruler-style reactions after every action |
| Fire Emblem | Throne bosses; reinforcements that sometimes act on arrival | Reinforcements never act on arrival; leash and screening | Ambush spawns; chip-and-retreat throne farming |
| Darkest Dungeon | Puzzle bosses (the Hag's pot, the Prophet's pews) | One rule-changing gimmick per boss (the Device) | Summon spam; repeat fights that are only stat bumps |
| Inscryption | Each Act 1 boss rewrites one board rule per phase | Last threshold changes the board or the kit (BR-9) | — |
| Wildfrost | Countdown digits on units | The Rhythm strip (marks on the plate, not a map clock) | Snowballing difficulty |
| Monster Train | Boss and its floor modifiers shown before commitment | The Playbill; the Stratum Card names the Rendition | — |
| Dead Cells / Isaac | Route-tied boss alternatives; champion variants | Renditions as the cheap variety multiplier, by shape not hue (RC5) | Tier-1 repetition; spongy bosses |
| Enter the Gungeon | Boss intro card; a DPS cap per window | The intro card (skippable) | Damage caps that hide information |
| Gloomhaven | Boss HP × character count; scenario special rules | `hpByPartySize` (read); per-boss authored sheets | Blanket condition immunity |
| D&D 5E / 2024 MM | Legendary actions and resistance; lair actions | Resolve pips (read) as the stunlock answer | Initiative-20 lair slots; damage-creep legendary actions |
| MCDM Flee, Mortals! | Ordered once-per-fight villain actions | The authored Rhythm's three-beat shape | Off-turn signature spikes |
| PF2E | Complex hazards with routines as co-antagonists | The arena Stir as the boss's second actor (M2g/M3) | +4 level bosses that crit-kill |
| 4E solos | Stunlock and sponges, fixed by bloodied triggers | Phases with Steeled (read) | Long HP pools |
| 13th Age | Party-side escalation die | Later option on F5 only, if fights stall | Boss enrage as its mirror |
| Baldur's Gate 3 | Set pieces solved with the environment (Grym's hammer) | Visible party-operated Devices that save HP | Ledge and barrel cheese (`noDrop` answers it) |
| Dark Souls / Elden Ring | Phase changes with arena changes; dual bosses | A phase change that reads instantly | Adds in a cramped arena; unpreviewed pits |
| Final Fantasy Tactics | A solo duel that could soft-lock saves | Exits never sealed; F1 solo rows | Unwinnable solo states under permadeath |

Lessons:
1. Identity comes from one rule-changing idea per boss, not HP.
2. The bigger the spike, the more exact its telegraph must be.
3. Adds must be announced, capped, tied to a phase, and never act on arrival.
4. Variety matters most on the most-seen floor, and variants on a fixed body are the cheapest way to buy it.
5. Difficulty tiers add kind, never stats.
6. Scale HP and bodies with party size, never attacks.
7. No enrage and no damage cap under a deterministic core.

---

## 2. The current-systems audit

### 2.1 What the approved documents fix for segment 9 (settled unless a flagged BR changes it)

**Boss frame (ENM 17.5):**
- Hand-designed, from a pool, no fallback; Unyielding; `fearless`; never Death-warded or affixed; guile +2; starts Wary **(settled)**.
- Resolve pips 2 / 2–3 / 2–3 / 3 / 3; fear spends a pip (HR-16a).
- Phases on the HP bar from the start: 50% on F1, 66/33 on F2–5; damage carries through; double crossings resolve in order; entry ends harmful conditions, grants Steeled and makes exactly one change from (a)–(e); never heals, restores pips or refreshes uses **(settled)**.
- Kit caps: 2 at-wills; 2–4 limited, 1–2 `signature`; ≤ 1 reaction; ≤ 1 bonus action; 1–3 boss-action options; ≤ 2 conditions per phase kit and 3 across phases; ≤ 1 hard control (cooldown ≥ 3, `readyAt` 2, 1 turn), none on F1.
- Immunities: ≤ 1 damage immunity, never the `counterType`; ≥ 1 weakness; ≤ 2 resistances; ≤ 2 tag immunities, never `goad`.
- Boss actions (A-3, M2): the count, timing and menu above; never hard control; "Hampered never blocks boss actions" **(settled)**.
- Life-long state (A-4): pips, phases and uses persist; cooldowns reset at combat end; a reset returns the boss Wary to the top of its current band. "Retreat-rest-return therefore costs the party supply and keeps the phase progress it earned" **(settled)**.
- Exits never sealed; the Bossgate opens on `BossDefeated` (none on F5).
- Arena: authored template; ≥ 2 entrances (dog-leg Arches); ≥ 2 Tall line-of-sight blockers; ≥ 1 counterplay feature.
- T7: boss death Routs the non-fearless escort and reserves; mindless servants go inert. `mindless` implies `fearless` (ENM).
- HP from `hpByPartySize[1..4]` of the actual party; a content row per boss per size (CMP-7).
- Escort and reserve groups are not constrained by the Herald content rule, except through `F1EscortBreakable` on F1 (HR-21, **agreed 2026-11-20**; RT-9). Narrowed for F2–5 by BR-19 (**agreed, Q4**).
- Signature carried by an armed boss or guarded by an unarmed one; the reward card C/U/R/R choose 1 of 3, never VR; F5 Memento plus Viaticum; Perilous 1 of 4 (M3) **(agreed)**.
- M1 boss Old Ragged: settled kit; the roar announced "from the first turn" (`readyAt` defaults to 1, ENM 9.3).

**Telegraphs:** signatures listed from the boss's first turn with a live forecast banner, "a forecast, not a commitment" (13.2); committed intents rejected (13.6); committed boss signatures are Later (21) until BR-32 (**agreed, Q3**); scripted wind-ups deferred (ABL 5); lair actions a non-goal (ABL 2).

**Guards (ENM 6.4, DH-26):** boss ≤ 35% expected per round; worst spike (dice at max, a crit, one boss action) ≤ 70%; no one-round kill of a full-HP hero without a crit. F1 solo: ≤ 25%; lowest-legal spike ≤ 75%; no one-round kill even with a crit; F1 cumulative round-1 budget ≤ 2.5%. `Floor1BossSustain` at L3 (DH 21.1 step 6), 6 rounds / 700‰ / 3 potions.

**F1:** `Floor1BossMindReachable`; non-fearless escorts (RT-9); `F1EscortBreakable` (reserves included); no elite; no hard control; no hazards below Nightmare tier 2 (RT-20); Landmark ticks are a non-hazard class allowed on F1 from M3, ≤ 1 per room at index ≥ 3 (HZ 6.19, HT7); no traps in the Sanctum (TL 6.20); always solo (CMP 7.10); Antechamber Catch-up to level 3, which defers if combat is active, and "the arena cannot be entered until it resolves" (RS 9.4).

**Sanctum (FL 6.11):** Forecourt (n−2), arena, Bossgate, Stairhead about 5×5 (none on F5); two dog-leg Arches ≥ 5 apart; start zones ≥ 9; symmetric `AntechamberScreened`; the arena's only other Portal is the Bossgate (LG4); sizes F1 15×15–19×19, F2–3 15×17, F4 17×19–21×21, F5 the bed about 17×19; long axis ≤ 21, w + h ≤ 42. F2–5 Forecourt restable once the patrol is dead (client reading, 2026-11-21); a short rest needs "no hostile unit aware of the party" (ABL 15.1 **(settled)**).

**Arena caps:** RT 6.11 (TR-16) and HZ 6.17 as listed in 4.1. Bosses `noDrop`. One clock per room, never waived; a clock is any countdown digit or meter **on the map**. M2 Stirs are only the Organ Swells and Pit Breathes, gated, each in its theme's signature room, at most one per floor **(agreed, HZ Q1)**.

**Rosters (RS 8.3–8.11):** 15 M2 bosses over 11 cells, plus the Wake's 2 if gated (5.1). RS 12 Cut 1 drops the Gaol's, Kennel's and Gut's extras; Cut 2 adds the Supper **(agreed, RS Q4)**. "The Heart and Gut are never cut because they are built first." The lead applies Cut 1, then Cut 2, on the M2 schedule review; any cut below Cut 2 returns to the client (RS 12, new lead's call there). The F1-boss cut order was the carried client question (EV-44), answered here (**agreed, Q1**). "Profile-side anti-streak outside seeded runs" is an M3 row in RS 12 (Launcher Fallow for the F1 Curiosity alone was pulled into M2 by EV-20).

**Premise:** RS 7.5: nothing shows or implies who the Dreamer is; "never a body" (HZ 7.10). RC6 scale ≤ 1.5× within one square; RC7 universal banner and sting; RC8 barks after the banner. RS 8.11: "Later adds the Dreamer as a true-ending boss (premise permitting, 7.5)", replaced by BR-30 (**agreed, Q5**).

**Loot:** IR carriage per form (IR 4.13: carried only if armed and the form's family is in the boss's issue gear, or a visible worn armour, shield or focus); unarmed bosses guard in a signature cache in the arena; IR's signature tables (Warming Pan, mace; Wind-Down Hammer, warhammer; and the rest); second forms required in M2 for specialist chassis; the Heart's damage is psychic and piercing, its counter radiant.

**Events:** F-BOSS run-specific where segment 9 exposes 2–3 seeded variables; one `bossQueue` shared with LE-18; no Curiosities in arenas; Graces end at "the arena seal" before arena combat start (EV-11; the conditions doc carries `arenaSeal` in `endsOn`); the Ca7 poster Glimpse; Coat Hooks on F5.

### 2.2 What the approved documents owe segment 9

| Source | Owed | Answered in |
|---|---|---|
| FL 13 "9 Boss rooms" | Interior, counterplay, phases, Keepsake dressing, tuning with the patrol joiner (FL-21(e)) | 4.1, 4.5, 4.7, BR-13 |
| FL 11.4 | `BossDamageFromOutsideLeash`, boss arm with joiner, `BossFightJoinRate`, `PreBossRestSafety`, `PostBossBacktrack` | 9.3 |
| RS 13 | One counterplay feature per boss (8 named for 17 bosses) | 4.7, 5 |
| RS 9.3 | The Heart's "drop the heavy thing" rule | 4.13 |
| RS 19.2 / EV-44 | The F1-boss cut order | 4.10, Q1 (agreed) |
| RT 13 / 6.11 | Interiors, counterplay identity, each boss's one authored terrain move | 4.1, 5 |
| HZ 13 / 6.17 | Missing arena rows; `noDrop`; Teetering guard; Mood hooks | BR-33, 4.14 |
| TL 13 | Arena devices in TrapRecord format; Bossgate never by Wax Key | 4.7 |
| EP 13 / 6.14 | Arena formation set; interior tagging; boss reserves | 4.5 |
| ENM 17.4 / 17.5 / 23 / 25 | Arena set; reserves; signature flags per boss; M1 arena row | 4, 5 |
| IR 4.13 / 4.18 | `signatureForms`, armed flags, second forms, surcoat, Headsman's Greataxe | BR-27 |
| IR-12 | Spine-Binder `armed`, Butler-at-Arms `promotable` | supported (11) |
| LP 4.10 / 4.13 | Cache square; "lair = arena"; card screen; no Windfall | 4.11 |
| LE 6.15, LE-5 | Boss-card screen; Viaticum room | 4.11 |
| EV 4.13 | Seeded variables; seal; `bossQueue`; Ca7 poster; Interludes; Coat Hooks; F1 variety; Cut 1 | 4.2, 4.12, 4.10 |
| CMP 7.9 | `hpByPartySize` row per boss per size | BR-21 |
| CL 5.6 / VG / SH / ARC / HR 7.16 | Solo boss breakdown, answer rows, Herald rows | 4.7, 9.3 |
| DH 21.5 | F1 solo boss reported separately | 9.3 |
| CN 19.3 | Scroll-and-bomb share of boss damage, alarm > 25%; `ViaticumBossShare` | 9.3 |

### 2.3 Tensions and resolutions

| Id | Tension | Resolution |
|---|---|---|
| BT1 | Cut 1 removes the only multi-boss cells below F5; F1 repeat 22% → 44% | Graded Cut 1 and Renditions (BR-23–BR-26; agreed, Q1) |
| BT2 | The Gut is built first, so dropping the Appetite may save nothing | Cut 1a conditional; the real choice was the F1 extras (agreed, Q1; told plainly) |
| BT3 | RS 13 names 8 features for 17 bosses | A Device authored for every boss (5) |
| BT4 | The kennel gate is a Portal skin (TR-22), but LG4 allows no other arena Portal | Renamed the **pen bar**, a two-state piece; RS 13 and the HZ 6.17 Master row flagged (BR-16) |
| BT5 | F1 hazard ban versus "a chasm edge for shoves" | F1 Devices never harm (Cancel, Deny, Divide only) |
| BT6 | The Jailer's "announced Countdown" versus 13.2 forecasts and the one-clock rule | The Countdown is a Downbeat on the plate's Rhythm strip: plate marks, no map digit, no wind-up (BR-6) |
| BT7 | "Arena seal" versus "exits are never sealed" | `ArenaEngaged`; data alias kept (BR-3) |
| BT8 | ArenaEngaged by "noticing" could start the F1 fight before the Catch-up | From M2, while the Catch-up is enabled, noticing is suppressed on F1 until it resolves (BR-3) |
| BT9 | Phase (d) versus "no off-map reinforcements" and `ArrivalThroughPortal` | Reserve sockets inside the arena (BR-11) |
| BT10 | AddCap trims reserves that 17.5 (d) excludes from caps | Flagged (BR-12) |
| BT11 | The Heavy Thing's drafted bludgeoning is outside the Heart's damage set | Piercing (BR-30) |
| BT12 | Heart bosses carry bludgeoning signature weapons | Chassis only; the hit's type follows the theme (BR-27) |
| BT13 | Size undefined; "a huge hound" | Every M1–M2 boss ≤ Large (BR-18) |
| BT14 | Old Ragged's settled roar is usable on turn 1; F1 `readyAt` ≥ 2 changes it | Flagged for M2; M1 unchanged (BR-22) |
| BT15 | Spike caps "without a crit" versus the settled crit-inclusive rows | One crit-inclusive spike budget (BR-8) |
| BT16 | Shed at the end of the controller's turn erases control before it matters | Shed only removes a condition present at round start (BR-7) |
| BT17 | Retreat-rest-return in a restable Forecourt makes the boss three short fights | Sanctum rest rule (BR-31; agreed, Q2); A-4 kept |
| BT18 | 5–7 round boss fights versus the F4–5 20-minute floor gate | Floor-clock line, BossMinutes ≤ 7 (BR-40) |
| BT19 | The patrol joiner unruled (FL-21(e)) | Counted in AddCap and the guard enumeration (BR-13) |
| BT20 | A final boss's damage immunity can kill a Viaticum tool | No damage immunity on `final` records (BR-20) |
| BT21 | Gut "bile phase at 75%" versus fixed 66/33 | Pre-armed socket set; thresholds kept (BR-34) |
| BT22 | "The Dreamer Turns Over" and RS 8.11's Later Dreamer boss versus RS 7.5 | Tone review; bodiless ending (BR-30; agreed, Q5) |
| BT23 | 8 of 15 signatures are fear roars | Condition spread in Renditions (BK4) |
| BT24 | `BossDefeated` versus `BossResolved` | One event, alias kept (BR-37) |

---

## 3. Boss models

### 3.1 The models
- **Model A, authored arenas and rhythms.** One authored template per boss with Guises; a fixed Rhythm per phase band; one class-blind Device; reserve sockets; draw-free stamping.
- **Model B, modular bosses.** A Core kit plus one drawn Facet combo from an authored list (Phase, Court, Arena, Rhythm slots); parametric templates with 48 generated fill draws and a proven fallback fill.
- **Model C, the arena as a character.** One Keynote per arena (a Dial or an acting Stir); a signature bound to the Keynote's off-beat; Overtaken; Arena States; Downbeat (the boss cues its own clock).
- Layers merged into all three: the **rhythm and phase layer** (Prompts, Decrees, Jarred, OneDueThing, the Dream contract), the **boss catalogue**, the **new-mechanics doc** (Playbill, Take Places, Signature in Hand, Phase Score, Boss Ledger) and the **numbers toy**.

### 3.2 Judges' scores (mean of three judges, 1–10)

| Model | Memorable | Fair | Readable | Variety | Class counterplay | Determinism and tests | Fit with docs | Low build cost | Total |
|---|---|---|---|---|---|---|---|---|---|
| A authored arenas | 7.0 | 8.7 | 8.7 | 6.0 | 8.0 | 9.0 | 8.3 | 4.0 | **59.7** |
| B modular bosses | 6.3 | 6.7 | 6.7 | 9.0 | 7.7 | 6.3 | 6.3 | 5.0 | 54.0 |
| C arena as character | 9.0 | 5.7 | 5.7 | 6.0 | 8.3 | 7.3 | 5.0 | 4.0 | 51.0 |

### 3.3 Verdict (lead's call; agreed, Q1 for the variety grafts)
- **Model A is the chassis.** It is the only model whose F1 package passes the settled spike and crit-kill rows at the authored dice (69% against 108–115%), the most readable, and deterministic and provable with the fewest reopened rulings.
- **Its weakness, predictability across runs, is what the grafts fix:** B's authored-list discipline and folded bosses become Renditions (BR-23, BR-24); the rhythm layer's OneDueThing and Dream contract govern headlines (BR-10).
- **C's vision is A's M3 layer,** not the M2 chassis (told plainly). In M2 C would be Dial-only in 13 of 17 arenas, would spend the one clock without acting, and would reopen five approved texts at once. Its charter rules become BX1–BX10, and its Keynote binding and Overtaken arrive with the M3 Stir catalogue (BR-33).
- **Contests of the judges (told plainly):**
  - Judges 1 and 2 wanted the F1 arena clock-free at every tier. That would narrow approved HZ text (F1 Landmark ticks from M3). This design keeps HZ as written and authors one harmless Landmark tick for the Master's arena at M3 (CS2).
  - Judge 1 wanted Prompts in M2; judges 2 and 3 and the fun critic wanted M3. Prompts are an **M3 option** gated on `ThreatMix` (BR-41).
  - All judges grafted the "Shorthanded" lever. The balance critic is right that it is a Lone-Wolf bonus the companions design rejects. It is **rejected** (BR-21).
  - Model A's verification load (400 enumerations, about 217% of a 62k pass) is cut: mirror and rotation are proven statically, AI ties break in template-local coordinates so mirrored stamps behave identically by construction, and only Rendition × formation × size is simulated (BR-2).

---

## 4. Rules in full

### 4.1 Arena generation (changes BR-1, BR-2)

**4.1.1 The template (change BR-1; lead's call; M1 for Old Ragged's Den, M2 for all).** Every boss names one `ArenaTemplate`:

| Field | Rule |
|---|---|
| `shapeKit` | Hall (Gaol, Supper, Wake), Den (Kennel), Nave (Choir, Wake), Stacks (Silk), Ring (Carnival), Antrum (Gut), Bed (Heart). One flat rules level: no shape implies elevation |
| `sizeByBand` | **One authored size per (Guise, floor band)**, inside FL 6.3 for the boss's floors; long axis ≤ 21; w + h ≤ 42. FL's `arenaCell` is sized to contain it (an FL hook, BR-1); the stamp chooses nothing **(lead's call, after the check)** |
| Arch sockets | Two, ≥ 5 apart, each with an authored dog-leg baffle (`AntechamberScreened`); the arena owns its doorway squares (FL-13); each lists its `takePlacesSquares[]` (arena squares at path distance ≤ 2 from that Arch's doorway squares, 4.2.2) |
| `startZone` | Boss post plus escort squares, ≥ 9 path squares from both Arches |
| `partyApproach` | Squares within 3 of each Arch: no contact hazard, Void, phase socket, reserve socket or Device face; ≥ 2 cover squares per Arch *(illustr.)* |
| `bossLanes[]` | 1–3 named lanes from the post; no contact hazard or Void within 3 (extends VP7 `BossLaneClear` to contact hazards) |
| Tall clusters | ≥ 2, placed so no square of `partyApproach` sees the boss post; and so every square beyond the boss's threat radius either lacks sight of the boss or lies in a reserve wake fan (anti-kiting, FN6) |
| Cover | ≥ 4 cover-giving squares per half; clutter 5–12% **(settled caps)** |
| Shade pair | ≥ 1 flank pair beside a lane, each square with Tall cover from the other Arch |
| `deviceSockets[]` | Exactly 1 (F5: 2, the boss's own Device and the Heavy Thing, 4.13), reachable from both Arches without crossing a start zone; each Interact or catch square ≥ 3 from the boss post. An overhead Device hangs over non-walkable squares or a wall slot; its walkable catch square carries the RC5 Device glyph (6) |
| `reserveSockets[]` | 0–1 (one reserve group per boss), footprint ≤ 3×3 |
| `phaseSockets[]` | (e) only; proven in both states (V9 `PhaseSocketsBoth`) |
| `stirSocket?` | 0–1; M2g for two arenas only, else M3 (BR-33) |
| `voidRegion?` | 0–1, F2+ only, M2 gated else M3; ≥ 3 from Arches, start zones and lanes; never with a Lurch Stir |
| `kegSocket?` | Carnival and Gut only, ≤ 1 |
| `cacheCandidates[]` | ≥ 2, LP 4.4 filters: ≥ 3 from both Arches; outside start zone, clock footprints, phase sockets, reserve sockets, Device faces and committed-move candidate areas; visible from both Arches |
| Wall slots | Keepsakes (F5), the Ca7 poster Glimpse (Carnival), the boss's dressing; never walkable (RC1) |

**4.1.2 Guises and orientation (lead's call).**
- Each boss authors **3 Guises on F1, 2 elsewhere** (plus the named extra Guises of 4.10 and 4.14.2). A Guise is a complete authored interior inside its template's shape kit, at one authored size per floor band: Device positions, Tall layout, reserve and phase sockets, cache candidates, and (M3) the Stir socket. Guises never change kit, stats, Rhythm or caps. Every Guise is referenced by at least one Rendition (V17s), so no Guise is dead content **(lead's call, after the check)**.
- Every Guise may be mirrored on the axis perpendicular to the entrance wall. Rotation is fixed by FL's placement, not drawn.
- Each template names one **safe Guise** for the release fallback (4.1.3).
- **Generated arena fill is rejected** (Model B's 48 draws; told plainly): it adds a runtime failure path and makes the most important room unprovable offline.

**4.1.3 The stamp (change BR-2; deterministic; M2).**
1. Inputs: the boss (route pre-roll), its `BossVariantKey` (Rendition, mirror bit; BR-23), the arena party size, FL's `arenaCell` and Forecourt side.
2. Take the Rendition's Guise at its authored size for the floor band; rotate so the entrance faces the Forecourt; mirror if the Rendition's `Mirror` is true, or if it is null and the drawn bit is set (4.10.1).
3. Stamp walls, Tall, cover, clutter, Devices, sockets, cache candidates, wall slots.
4. Fill the escort from the Rendition's formation at the arena party size, then apply AddCap and trimming once (4.5.3).
5. Run V9, V10, V13g, then V17g.
- Every Guise × mirror × 4 rotations, at its authored size, is proven offline by V17s. A runtime failure is a bug: hard error in development; in release, stamp the Rendition's own Guise unmirrored, else the template's safe Guise with the Rendition's kit and Device kept; log `ArenaStampFallback` as a content bug. Facts already shown (Stratum Card, door card, Playbill) are never re-derived; facts not yet shown are derived from what was stamped **(lead's call, after the check)**.
- **AI tie-breaks use template-local coordinates before the stamp transform** (BL14), so a mirrored or rotated stamp behaves identically; V17t asserts mirror-equivalence on replays.

**4.1.4 Caps summary (V17g).**

| Item | Cap | Source |
|---|---|---|
| Tall | ≥ 2 | ENM 17.5, RT 6.11 |
| Cover | ≥ 4 per half; clutter 5–12% | RT 6.11 |
| Contact hazards | ≤ 4, none within 3 of Arches, entrances, start zones or lanes; **0 on F1** | RT 6.11, RT-20, BR-1 |
| Furnishings / Kegs | ≤ 4 / ≤ 1 (Carnival, Gut) | RT 6.11 |
| Void | ≤ 1, F2+, never with Lurch | HZ 6.17 |
| Clocks | ≤ 1 on the map (a Stir or an F1 Landmark tick); the Rhythm strip is not a clock | HZ 6.2.1 (BR-6 reading) |
| Devices | 1 (F5: 2) | BR-16 |
| Reserve sockets | ≤ 1 | BR-11 |
| Hard-control sources | ≤ 1, or ≤ 2 in an F4–5 boss encounter, counting boss, escort, reserve, Device, and Few-and-Strong substitutes | ENM 9.4/10.3; BR-12 |
| Acid sources | ≤ 2 after overlay (Gut) | RS 8.10, EP-31 |

### 4.2 The seal, the Forecourt and the Stairhead

**4.2.1 `ArenaEngaged` (change BR-3 ⚑; lead's call; M1).**
- Fires **once per floor**, at the first of: (a) a party unit enters an arena square, doorway squares included (FL-13); (b) the boss or its escort notices a party unit.
- **Trigger (a) is an automatic notice (lead's call, after the check):** a party unit on an arena square counts as noticed by the boss's group, so the fight starts by noticing, as MAIN 4.3 **(decided)** says; Take Places is the placement step of the combat that this notice starts (BR-3, BR-4, flagged ⚑ Decided, read).
- **F1 guard (M2+):** while the Antechamber Catch-up is enabled (M2+; RS 9.4, RT-8) and `antechamberCatchUpApplied` is false, trigger (b) is suppressed and the boss and escort cannot notice (CS5). The hero therefore always meets the F1 boss at level 3 from M2. In M1 there is no Catch-up, and `ArenaEngaged` fires on (a) or (b) with no guard **(lead's call, after the check)**.
- Order on the same tick: Antechamber Catch-up (F1, M2+) → `ArenaEngaged` (Graces and event riders end, EV-11 order kept) → Take Places (BR-4) → combat start. **If combat is already active** (an F2–5 Forecourt patrol fight), the boss group joins it as joiners under ENM 12.4 item 4 (initiative rolled at the start of its entry round) and Take Places is skipped **(lead's call, after the check)**.
- A re-engagement after a reset fires no new `ArenaEngaged`, but **every arena combat start while `sanctumEngaged` is set processes `endsOn: arenaSeal` again**, so a Grace or event rider taken elsewhere on the floor between engagements ends before the arena fight, as EV 4.4.3 requires ("a Grace never fires in the arena"). Graces cannot be regained in the Sanctum **(lead's call, after the check; told to the EV and conditions owners under BR-3)**.
- **Entry confirm (UI only, lead's call, after the check):** the first move of a party unit into a doorway band asks "Enter the arena? The Sanctum will not let you sleep until <boss> falls." The confirm can be turned off in settings; the Playbill and the door card carry the same line. No rule changes. **The confirm also carries the Behind-you line** ("Behind you: 2 rooms not entered · Treasures 5 / 7 · best remaining: Uncommon"; rooms not entered exclude the arena and every room reachable only through it; hidden in the spent state; 0 added clicks); **with the confirm turned off**, the same line shows once, as a non-modal status-ribbon toast, the first time a party unit enters the arena's doorway band **(change XI-11; lead's call; told plainly; M2; exploration and information design 4.4.4)**.
- The conditions doc's data id `arenaSeal` stays as an alias in `endsOn`. Display never says "seal" and never shows a door closing.
- EV's BC4 hook ("segment 9 may declare Grace exceptions") is answered: **no Grace exceptions in M2**.
- `ArenaEngaged` also starts the Sanctum rest rule (4.2.5) **(new lead's call: the rule's start event is this one, so the F1 Catch-up order holds for it too)**.

**4.2.2 Take Places (change BR-4 ⚑; lead's call; M2).**
- At `ArenaEngaged`, when combat was not already active, before initiative, the player may place each party unit that is **out of combat and in the Forecourt or on an arena square** (the triggering unit included) on any free square of either Arch's doorway band or that Arch's `takePlacesSquares[]` (4.1.1; *(illustr.)* about 6 per Arch). Units elsewhere on the floor join combat normally.
- No actions, abilities, items or swaps. A unit's destination must lie within a path distance ≤ its Speed from where it stands **(lead's call, after the check)**.
- Fires only at the first engagement. On F1 it waits behind the Catch-up.
- The Playbill (BR-5) shows the opening Rhythm and the first forecast during placement, so the choice of Arch is informed (FN11).
- Why: in turn-based exploration the hero otherwise steps in alone while companions trickle in. The two dog-leg Arches become a real choice.

**4.2.3 The Forecourt** (read: FL 6.11). F1 empty, near-safe, hosting the Catch-up. F2–5 a Circuit and patrol host. Segment 9 adds only: arena baffles; no socket or Device face within 3 of an Arch; Interludes keep `AntechamberScreened`; and the Sanctum rest rule (4.2.5).

**4.2.4 The Bossgate and the Stairhead** (read): far wall; opens on `BossDefeated`; Stairhead about 5×5; the waking-light window dresses the F5 far wall. The cache opens on the same event. The reward card is offered in the arena through the offer UI. `BossResolved` (LP) is an alias of `BossDefeated` (BR-37): bosses cannot rout, yield, be cowed or be removed.

**4.2.5 The Sanctum rest rule (change BR-31 ⚑; agreed, Q2; M2).**
- **Told plainly:** A-4 deliberately allows retreat-rest-return at a supply cost, and segment 9 keeps that. The critics' cheap form is resting **next door** in the restable Forecourt: burst the boss just past a threshold (where a reset costs it nothing), step out, rest for 1 supply, return with every Downed companion up.
- **The rule (agreed, Q2):** from the first `ArenaEngaged` until `BossDefeated`, **no short rest anywhere in the Sanctum** (Forecourt, arena, Stairhead). Rests before first engagement stay legal (`PreBossRestSafety`). Display: "The Sanctum will not let you sleep."
- **It is a location ban, not an awareness reading (lead's call, after the check):** a short rest is refused while `sanctumEngaged` is set, the boss lives, and **any party unit stands on a Sanctum square** (rest is party-wide, so one unit in the Sanctum blocks it). It is added to ABL 15.1 and DH 11.3 as a rest condition (BR-31). Awareness is untouched: combat ends normally when no hostile is aware of the party (MAIN 4.3, **decided**), and a boss that loses the party resets under A-4 as before.
- **One fight for both sides (lead's call, after the check; told plainly):** while `sanctumEngaged` is set and the boss lives, a combat end in the Sanctum does **not** reset party cooldowns or once-per-fight counters (Medicine, Tainted Blade and the like); they reset at `BossDefeated`, or when the party ends a combat outside the Sanctum. This closes the "threshold dip" (burst past a threshold, step out through an Arch, come back with every per-fight resource ready), which needs no rest at all. It mirrors A-4's boss life state; flagged ⚑ against ABL's settled cooldown reset and DH's once-per-fight Medicine (BR-31). Report `ThresholdDipRate` (resets within 2 rounds after a threshold crossing). Fallback if the ABL or DH owner refuses: no rule, and `ThresholdDipRate` is reported beside `BossCheeseRate` (14.2).
- Rests elsewhere on the floor stay legal: the walk and the floor clock are the cost, as A-4 intends.
- The Rest button in the Sanctum shows the rule as its unusable reason (ABL 15.4 unusable buttons) and is never hidden **(new lead's call)**.
- The rule is the same on F1, where the Forecourt is near-safe only before engagement (RS 9.4 read) **(new lead's call: stated so F1 has no exception)**.
- Persisting state when the party returns: pips, phases, spent uses, Device charges, announced reserves, OneDueThing state and the Rhythm pointer (4.3.4).
- **Rejected (Q2):** B, no rest anywhere on the floor between first engagement and the boss's defeat (narrows A-4); C, keep the Forecourt restable throughout.
- Reports: `MidBossRestRate` and `RestBetweenPhasesRate` (defined in 9.3 from the `RestTaken` event). If more than 25% *(illustr.)* of boss wins on any floor include a mid-boss rest, the lead reports it to the client and may raise a floor-wide rule again; because B was rejected, it is **not** a pre-approved fallback (14.2) **(new lead's call)**.

### 4.3 Fight structure, Rhythm and telegraphs

**4.3.1 The Rhythm (change BR-6 ⚑; lead's call; M2).**
- Each boss record holds `rhythm[band]`: a loop of 2–5 beats *(illustr.)* per phase band, plus `entryBeat` 0.

| Beat | Meaning |
|---|---|
| **Off** | The scorer chooses freely among at-wills, movement and limited abilities not reserved by a Lead or Down beat |
| **Lead** | A named non-signature limited ability, used if ready and legal, else played as Off |
| **Down** | A named signature, used if ready and with ≥ 1 legal target or area scoring at or above the invariant-7 floor; the scorer picks target, area and movement one-step |

Rules (made exact after the check; lead's call, after the check, where marked †):
1. **Advance.** The pointer advances at the end of **every** boss turn, whether or not the boss acted (a turn lost to Dazed or other control still advances it) †. Boss actions never advance it.
2. **Waiting.** An unready or illegal Downbeat plays an Off turn and the pointer holds, at most once (`maxWait` 1 *(illustr.)*). Then the pointer moves on, **but the signature becomes pending** (BL10). Kiting delays a signature; it never deletes it.
3. **Pending** †. A pending signature is used on the next boss turn where it is ready and legal, as an absolute override of an Off or a Lead beat, but never of another signature's Downbeat. When it fires off-beat, the pointer re-phases to the beat after that signature's own Downbeat, so its next Downbeat is a full loop later and its cooldown is always ready by then. Pending clears when the signature fires, and at phase entry under rule 5.
4. **Cooldowns agree with the loop** (V17 `RhythmMatchesCooldown`): a signature's cooldown ≤ the loop distance to its next Downbeat; its `readyAt` ≤ its first Downbeat's turn number (counted from `rhythmStart`, rule 6). Rule 3's re-phase keeps this true after an off-beat use.
5. **Phase entry.** The next boss turn starts the new band at `entryBeat`, which is always Off; a phase change never adds a signature in the same breath. **Owed-beat carry** †: if, at entry, the old band's next beat was a Downbeat or a signature was pending, the new band instead starts on the beat just before its own first Downbeat, so the phase change delays the owed Downbeat by one boss turn at most (entry Off, then Down; a swapped ability's `readyAt` 2 after entry, 4.4, is met). A band's Rhythm is the previous band's unless its entry is (b) (a new shape) or (a) (the new ability takes the replaced ability's beat, shape unchanged); a loop's shape changes only at an (a) or (b) entry (V17s (e)).
6. **`rhythmStart`** † (a Rendition's opening offset) applies to the first band only. V17s rejects a start whose beat is Down or whose first Downbeat would fall before `readyAt` plus one full banner round. **On F1, every loop is ≥ 3 beats and the first Downbeat is at least 2 beats after the start**, so the first signature lands on boss turn ≥ 3. A Rendition that "opens with X" inserts X as a Lead beat (a Rhythm override, 9.1); it is never a `rhythmStart` offset.
7. **Resets** †. The pointer is life-long state (A-4): after a reset it resumes where it stopped, so retreating cannot skip a Downbeat. `readyAt` keeps counting the boss's turns in the current fight **(settled, ENM 9.3)**. On re-engagement, if the pointer is on a Downbeat or a signature is pending, the first boss turn of the new combat plays Off without advancing the pointer, and the banner is lit from combat start; a Downbeat therefore lands after a reset only after one full banner round (V17t).
8. The Rhythm reads only the boss's own turn count and move legality, never "is the hero" (ENM 11.1 invariants).
9. **Banner.** The 13.2 banner is lit when the next beat is a Downbeat, while a signature is pending, and during a rule 7 hold.
10. **Display: the Rhythm strip** on the plate and initiative entry: a timeline track built from three shapes only, **Off a small dot, Lead a diamond, Down the signature's area-shape glyph** (burst, cone, line, single, pull) with the existing condition icon; never a per-boss glyph; the current mark lit. Resolve pips keep their approved shape on a separate row **(lead's call, after the check; registered in the RC5+ table, 6)**. It counts the boss's own turns, not rounds, and sits on the plate, not the map, so it is **not a clock** under HZ 6.2.1 (reading, flagged to the HZ owner). The strip alone carries timing: no Device or arena prop shows a Downbeat approaching.
- **Why flagged:** it constrains the settled scorer to a per-turn preference order (ENM 11.1, 17.3); adds the pointer to the life-state list (17.5 A-4); and reads ABL 5 as permitting it (an order is not a wind-up: nothing is committed to squares). No new search depth; p95 stays under 50 ms (ENM 11.5).
- **Fallback if refused:** cooldowns only, with ready-state pips (13.2 already shows ready state).

**4.3.2 Tempo variety (lead's call; V17s; FN3).**
- Every theme has ≥ 1 boss whose loop length is not 3, counting every band and Rendition.
- Each floor band (F1, F2–3, F4, F5) has ≥ 1 boss with a Lead beat or two Downbeats per loop.
- Every boss authors `rhythm[band]` for its first band (5); later bands follow rule 5 of 4.3.1.
- Every (b) entry changes the loop's shape (length or order), together with its stance line (BR-9).
- F1 Renditions must differ in Rhythm shape or another question-changing field, never only `rhythmStart`, formation, Guise or mirror (BK2).

**4.3.3 Telegraph and spike budget (change BR-8; lead's call; adds rows under ENM 6.4; M2).**
- **`NoUntelegraphedSpike`:** any boss or boss-action effect whose maximum without a crit exceeds **30% of the floor's reference hero** *(illustr.)* must be a Downbeat (bannered signature), a Gambit (M3) or an announced arena act.
- **Forecast Downbeat cap:** maximum without a crit ≤ 45% of the lowest legal hero *(illustr.)*; signatures are preferably save-based (no crit).
- **Spike budget on the settled rows' own basis (BL1):** worst own-turn effect **with a crit** + the worst boss action that can hit the same unit that round ≤ 70% (F1: ≤ 75% at the lowest legal hero). The F1 menu has no Strike (BR-7), so the F1 budget is the boss's own turn.
- **Two boss actions (F3–5):** focus spread is read to forbid two boss actions on the same unit in one round (flagged reading of ENM 6.4 and 11.1, `freeFocus`). V17 then enumerates one boss action per target.
- **Practical consequence:** wherever Strike is on the menu, attack-roll at-wills are capped near 22% without a crit *(illustr.)*.
- The round in which a phase entry occurs is enumerated with the new kit.

**4.3.4 OneDueThing and the Dream contract (change BR-10; lead's call; M2).**
- At most **one headline** is announced and unresolved at a time: a live Gambit (M3), a due Stir act, a due (d) arrival, a due (e) change.
- **Ordering (made exact after the check; lead's call, after the check):**
  - The **newer** headline always slips, whatever its precedence. On a double crossing (one hit through two thresholds), the deeper threshold's headline is the newer one. Precedence (Gambit, then Stir, then (d), then (e)) only breaks ties between headlines that become due in the same round from different sources.
  - **Every delay, from any source** (a slip, Divide, an occupied exit), re-checks OneDueThing at the new due round. A headline takes **at most 2 rounds of delay in total**, after which it resolves regardless and every other headline due that round slips instead (BL11).
  - When a Gambit commit would be blocked by a due headline, the Gambit's Downbeat waits under 4.3.1 rule 2.
- **Exact timing of (d) and (e):** entry in round R → announced at once on the phase card, sockets at due tier at step 4 of round R's arena round-end step → resolves at **step 1 of round R+1's arena round-end step** → reserve units first act in **round R+2**. Every party unit gets at least one turn between announcement and arrival (the HZ 6.3 contract, reused).
- **The arena round-end step (lead's call, after the check):** it is HZ 6.3's round-end Dream step. HZ-12 brought that step into M2 "for Stirs only", gated; segment 9 asks that it also run in every boss arena for phase headlines, independent of the Stir gates (BR-10, ⚑ Agreed, HZ Q1). If the Dream step does not ship in a wave, the arena round-end step is the step after the last turn-end step of the round, with the same step 1 (resolve) and step 4 (announce) slots.
- **V17g proof (lead's call, after the check):** an enumeration, per Rendition, over the crossing round of each threshold (rounds 1..N, including same-hit double crossings) × the Stir's phase offset × Divide used or not, asserting at most one headline is due per arena round-end step. A single dry run is not a proof, because crossings are driven by the player's damage.

### 4.4 Phases (change BR-9 ⚑; lead's call; M2)

- **Triggers stay HP-only** (told plainly): no escort-death, arena-state or time triggers. Those would hide information or be enrage by another name.
- **Last-threshold rule:** the last threshold (50% on F1; 33% on F2–5) uses (a), (d) or (e). (b) and (c) are allowed only at 66%. Every RS 8.x roster entry already complies; the rule binds Renditions (FN5).
- **(b) visibility:** a (b) entry changes the plate's stance line ("Rises from the chair") **and** the Rhythm's shape (4.3.2).
- **Phase marks:** each mark on the HP bar shows its change **type** from turn 1 (new move, stance, ward, reserve, terrain). The specific new ability is revealed at entry, and stays a "?" line on the Playbill until seen (FN5). A swapped ability has `readyAt` 2 after entry, so a reveal at entry is fair.
- **Phase card:** ≤ 1.2 s *(illustr.)*, skippable, blocking no input, ≤ 8 words: "The Jailer stops the clock: Dazed swing." From M3, the first-ever sighting (via `bossMemory`) may run about 2.5 s, still skippable. Barks follow the banner (RC8). Double crossings show one combined card.
- **Threshold preview:** the attack preview warns "This hit crosses 66%: the boss shrugs off <conditions>", so control spent at a threshold is a choice, not a surprise.
- **Flag:** the last-threshold and (b) rules narrow which authored option ENM 17.5 permits at a given threshold, for Renditions; they change no approved roster entry.

### 4.5 Escorts and adds

**4.5.1 Formations (lead's call; EP 6.14 formation set owned; change BR-19 for EP 6.14).** 2–3 `ArenaFormation`s per boss, each an authored shape on the start zone (for example a close guard, a screen in front of the post, or a flank split; these are descriptions, not names), filled in authored order from the boss's approved escort list, inside the approved escort budget **(settled, CMP-7)**. EP 6.14's "Boss Court escorts use Retinue" becomes "Boss Court escorts use segment 9's `ArenaFormation`s; EP's Retinue is one allowed shape on F2–5", a request to the EP owner (BR-19) **(lead's call, after the check)**.
- **Each Rendition pins exactly one formation** (4.10.1); a boss's formations are the union of its Renditions' formations, and every formation is referenced by at least one Rendition (V17s).
- **F1:** at least one formation per boss starts its non-fearless group **≥ 3 from the boss post**, outside the Emboldened aura (BL7, Herald); where the solo minion cap allows, a 2-minion pack starts clusterable inside a 3×3 (Arcanist, FN7).
- **F2–5 (agreed, Q4; BR-19):** every formation, at every party size, holds at least one **non-fearless group** (4.7.4), as escort or as the boss's (d) reserve. Wherever the escort budget allows, that group is in the **escort at fight start**, so the Herald has a fear target before 33%; the reserve-only cases are listed by V17s (i) and told to the client (4.7.4; lead's call, after the check).
- **The boss's Command never moves an escort back within 2 of the boss while the party has exactly one party unit** (a position and count rule, not "is the hero").

**4.5.2 Reserve sockets (change BR-11 ⚑; lead's call; M2).**
- Every (d) reserve group waits from reveal in a visible, inert **reserve socket** inside the arena: a pen, the choir loft, the menagerie wagon, family pews, a stomach fold, the nursery linen press. Occupants show as silhouettes behind a barrier piece, untargetable and outside initiative, with a "Wakes at 33%" chip. **Silhouettes keep their type's RC3 mind read** (face outline, veil, or the keyhole tell for mindless types), so a player can see before the wake whether the reserve is breakable; V17s (i) checks it **(lead's call, after the check)**. No reserve socket is in or behind a bed element. The socket is not a Portal (LG4 holds); the units are already in the room, so "no off-map reinforcements" (EP 6.12) and `ArrivalThroughPortal` (EP-16) hold (reading, told plainly).
- Timing is the Dream contract (4.3.4). **Reserves never act in their arrival round** (an addition to "announced a round early", flagged).
- Exit squares ≥ 3 from party start squares and the Arches *(illustr.)*. If an exit is occupied, arrival may be delayed **at most once per fight**, inside the headline's 2-round delay cap (4.3.4); after that the group arrives on free squares in **rings of increasing radius around the socket**, in template-local order, until each unit's footprint fits (Large units included), units placed in stable-id order (BL8: no Downed-companion body-blocking) **(lead's call, after the check)**.
- **Initiative:** arrived units roll initiative at the start of round R+2 as joiners under ENM 12.4 item 4; they are outside initiative until then **(lead's call, after the check)**.
- **Anti spawn-camping:** arriving units ignore party-created persistent effects on the arrival step only; they take on-entry effects at their own turn start if they remain. The Arcanist's free area turn on a known cluster is kept, because it is one turn of value, not a pre-armed kill.
- **Boss death:** pending (announced, not created) reserves are cancelled and their silhouettes fade; created reserves Rout under T7, or go inert if mindless **(settled)**.

**4.5.3 AddCap and Few-and-Strong (change BR-12 ⚑; lead's call; M2).**
- **One definition** (CS9): non-boss hostile units in the arena, counted in ENM 10.3 units (a minion counts ½), ≤ **arena party size + 1** *(illustr.)*, and ≤ the floor's 10.3 unit cap. It counts escort, reserve and the patrol joiner (the joiner at its caps, 4.5.4).
- **One snapshot (lead's call, after the check):** AddCap, formation fill and reserve trimming are computed once, at floor generation, against the arena party size; they never change at arrival time, so a Downed cascade never trims a reserve and the Playbill's formation fact never goes stale. The boss-action count alone reads live party units (4.8.2).
- **Solo:** escort plus reserve minions ≤ 2 **(settled, ENM 7.1)**, enforced with reserves (V17s).
- **Escort bodies at fight start** ≤ 3 at party 4 *(illustr.)*.
- A reserve that would exceed the cap is trimmed. **Trim order (lead's call, after the check):** fearless members in authored order, then the rest in authored order, skipping the members of the last Q4 group on F2–5 or of any `F1EscortBreakable` group on F1. The trimmed cost buys **Few-and-Strong**: the next escort member at most floor level +1. Never an elite or a tier jump on F1–2; **never a hard-control source**; any remainder is unspent (BL13). Never boss HP.
- **Trimming never removes the last non-fearless group** on F2–5 (Q4) or any `F1EscortBreakable` group on F1 **(new lead's call: AddCap made consistent with Q4)**. **Few-and-Strong may only pick an upgrade type whose fearless and mindless status equal the replaced member's**; if no such type exists within floor level +1, the cost is unspent. V17s (g) and (i) check it per formation and size **(lead's call, after the check:** fearless is a type property, so a substitute cannot "keep" a status**)**.
- **A (d) reserve is never trimmed to zero** at any party size its Rendition allows; a Rendition whose (d) change would be trimmed away pins a lighter formation instead (V17s (g); lead's call, after the check).
- **Flag:** ENM 17.5 (d) says reserves are "excluded from the cluster and joining caps". AddCap is a new arena headcount that trims them.

**4.5.4 The patrol joiner (change BR-13; a proposal answering FL-21(e); ENM owner rules).** The F2–5 Forecourt patrol joins as an ordinary joiner under the 1/1/2/2/2 caps, through an Arch after footsteps; it counts in AddCap; the 35% row is enumerated with it present at its worst arrival round; a (d) reserve due in the same round slips one round, a delay counted under 4.3.4's cap. T7 does not Rout ordinary joiners **(settled)**, so the patrol never counts as the Q4 group.

### 4.6 Boss AI (change BR-14 ⚑; lead's call; M2)

- The Boss profile (`leash: room`) plus per-boss **data weights**: Rhythm obedience; Device-face avoidance (the boss treats an armed Expose face or the Heavy Thing area as a hazard, so the party must push, pin or lure it there); lane preference; escort Command toward screening the Device. No new search depth.
- **`deviceBait` (lead's call, after the check):** a boss whose BK7 signature post stands on or under an Expose face (the Strongest Man's stage over the Dunk Tank; the Head of the Table's chair under the Icicle Chandelier) ignores Device-face avoidance **on a Downbeat turn and while a signature is pending**, so it steps onto the bait to perform and the party gets its window. Avoidance applies on Off and Lead turns only. V17t: every BK7 signature is legal on its Downbeat in a dry run with the Device armed.
- **Chain leash data:** `leash: chain(n)` (a narrower leash value in the Boss profile). It limits the unit's **own** movement only; forced movement is unaffected, and a unit pushed beyond its chain returns toward the anchor on its next move. Used by the Thing in Cell Zero (n 5) and the Pylorus (n 2), and by the chained Lifer of "Cell Zero Opens" (n 3). This replaces Model A's `fixedInPlace` exception (CS20, BL15): pushes still pay on the Pylorus. Every `chain(n)` unit shows one cross-theme **leash ring** mark (6), never painted dressing.
- **Kiting guard (lead's call, after the check):** `RangedOnlyWinShare` (alarm above 40%) is reported for **every** `chain(n)` or post-bound boss (the Thing in Cell Zero, the Pylorus, and every boss with a BK7 post). Fallbacks: the Thing's *The Chain Snaps* (5.3); for the rest, in order, a reserve socket or (e) set placed in the ranged band, then a Lead beat that reaches it (14.2). The Pylorus is built against kiting from the start (5.5).
- **Doorway band:** the boss avoids ending its turn in its doorway band unless it can reach a party unit from there; after ending one turn in the band, the band takes a −∞ end-square weight until it ends a turn outside it *(illustr.)*. Target `BossDamageFromOutsideLeash` 0 for melee-capable bosses (FL 11.4). FL-21(a): segment 9 asks for the narrow form (pursuit to the Arch squares, never into the Forecourt).
- **Shed priority:** the highest-`controlRank` eligible condition (4.8.1 Shed rule), then one blocking the next Downbeat.
- Focus spread, never targeting the Downed and invariant-7 floors apply to boss actions **(settled)**.
- **Reset (A-4):** the boss walks to its post Wary, heals to the band top; the pointer, Device charges, reserve state and Rendition persist.

### 4.7 Counterplay, Devices and immunities

**4.7.1 The Device contract (change BR-16 ⚑; lead's call; M2; M1 for the Den).**
- **One Device per boss** (F5: plus the Heavy Thing). A segment-9 TrapRecord instance of kind `arenaDevice` (TL-40 format for data and preview only). It is not a trap: no trigger plate, no hiding, no Botch, never armed by monsters (reading for TL 6.20, told plainly).
- Visible and previewed from reveal at Into the Breach grade: exact squares, effect per unit, uses left as pips, what a forced entry would do.
- **Operated by any class:** every Device has one or more **handles**; each handle has one payoff kind, its own charges and its trigger modes (9.1): **Interact** (an action) from adjacent; **RangedAttackOnPart** (an attack against the Device's marked part, auto-hit, from range); **AreaEffectTag(tag)** (an area effect with that tag that includes the marked part, for example fire on a thread anchor); and **ForcedEntry(boss)** (the boss forced onto the handle's catch square; any kind may list it). Every Device has a ranged trigger; area effects and misses trigger a handle only if it lists the matching mode. Never Shove-only, so Small heroes are never locked out (A-10) **(lead's call, after the check)**.
- **One primary handle per Device, one payoff kind per handle.** An F2+ Device may declare **one second handle** with its own kind and charges (the Carillon's sluice, the Pylorus's rescue); F1 Devices have one handle **(lead's call, after the check)**.
- **Payoff kinds:**

| Kind | Payoff | F1 legal |
|---|---|---|
| Cancel | Sets `pendingCancel`. When the pointer next reaches a Downbeat, the boss plays an Off turn instead and the pointer advances normally: that Downbeat is **skipped**, and the signature's next Downbeat is a full loop later. A pending Cancel carries across a threshold and is consumed by the next Downbeat in whichever band it falls; it counts against the denial budget of the phase in which it was used | Yes |
| Deny | An authored two-state piece or square set (V9-proven) blocks a lane, a line of sight or a line of effect, removes a terrain set, or tightens a chain leash | Yes |
| Divide | A pending reserve arrival is delayed one round, once per fight, before or after its announcement; the delay counts toward the headline's 2-round cap (4.3.4) | Yes |
| Divert | The operator becomes the signature's target until the end of the next round; an area signature is placed at the scorer's best legal placement that includes the operator | Yes |
| Sate | Spend a resource (1 supply); otherwise exactly as Cancel | No |
| Expose | Prone and **Upended** (4.7.2), plus device damage ≤ CapRound where authored, both ways | **No** |
| Release (second handle only) | Ends one Grapple or Restrained that the boss imposed on a unit | No |

- **F1 Devices never deal damage, impose a condition or move a unit,** so they stay non-hazards (RT-20, HT7).
- **Charges:** once per phase *(illustr.)* unless stated, **reset to the maximum** (never accumulated) at phase entry (re-arming the party's Device is not refreshing the boss); charges are life-long state, so a reset re-arms nothing. **F1 Cancel Devices have one charge per fight**, so a burst to the threshold plus a re-armed Cancel cannot delete the F1 signature **(lead's call, after the check)**.
- **When the denial budget (4.7.3) is spent**, the affected handle shows as unusable with its reason ("The Jailer will not be stopped again this phase") and cannot be operated; it is never hidden **(lead's call, after the check)**.
- **Never a brazier**, never a hazard Cord **(settled)**.
- **Never voids a committed move** (BN9): Cancel acts on a forecast Downbeat only.
- **RS 13 list (flagged ⚑, agreed RS Q4):** extended to every boss (5), and the "kennel gate" renamed **the pen bar**, a two-state piece on the Whelping Pen (BT4). TR-22's "kennel gates are Portal skins" is untouched, because the arena object is no longer a gate.

**4.7.2 Upended (change BR-17 ⚑; lead's call; M2).** The Expose payoff: the boss is Prone and takes **no boss actions until the end of its next turn**; plate icon "Upended". It is a boss state, not a condition and not hard control; the boss still takes its turn. **Once per phase.** It never cancels a signature or a committed move, and strips no pip. (It sits in the BR-7 boss-action bundle for the Hampered clause.)
- **Saves and immunity (lead's call, after the check):** Upended applies whenever the boss is in an Expose area or forced onto its catch square, whatever its save and even if it holds a Prone tag immunity (Upended is a boss state, not Prone). A second Expose in a phase where Upended is already used deals its damage and Prone only. V17t covers both cases.

**4.7.3 The denial budget (lead's call; BL9, FN2).** Per phase, from all party sources combined, a boss loses **at most one signature use** (Cancel or Sate) **and one round of boss actions** (Upended). Divide delays at most one reserve, once per fight. Reports: `SignatureLandRate` (target 0.5–0.9 hits per use for experts *(illustr.)*; below the band, raise the signature's value, never hide it) and `DeviceUseRate` (alarm below 30% and above 85%: ignored or mandatory; defined in 9.3). The "Device never used" arm must stay inside the floor's death band, at most one band step worse, where a band step is the width of that floor's death band (9.3) **(lead's call, after the check)**.

**4.7.4 Class answer windows (change BR-19 ⚑; lead's call; agreed, Q4 for the Herald F2–5 row; report-gated).** Every boss authors one window per launch class in `BossAnswerMatrix`; V17 checks every row exists.

| Class | Window | Rules leaned on |
|---|---|---|
| Vanguard | Every boss ≤ Large, so Medium heroes Shove and Grapple it (A-10). F2+: ≥ 2 push payoffs reachable from a lane (Expose face, contact hazard, Void edge: contact band plus Prone, never Teetering from Void or Clutch). Brink (b) beside a contact hazard stays legal on bosses; `TeeteringUptimeOnBosses` ≤ 25%. F1: wall and Tall faces within 2 of lanes, and pushes that strip the boss off its escort | `noDrop` **(settled)**, VG |
| Shade | ≥ 2 Tall clusters with a flank pair; the Offbeat after each Downbeat; Upended gives melee advantage; Tainted Blade once per fight; Draw Out against guile +2; never Surprises a boss | SH **(settled)** |
| Arcanist | Clustered reserve arrival with a free turn; F1 clustered minion pack; ranged Device triggers; "bind the guard"; hard control once pips are gone (Restrained ≥ Dazed) | ARC-17 |
| Herald | Fear spends pips; a **"Resolve Broken"** display beat when the pip strip empties; Scatter's second clause Frightens the boss for a turn; T7 Routs the escort; F1 formation outside the aura. **F2–5 (agreed, Q4; narrows HR-21 ⚑):** every Boss Court includes ≥ 1 non-fearless escort or reserve group | HR-16a, HR-5, ENM 14.4, 14.8 |

- **The Q4 group defined (new lead's call):** a "non-fearless group" is an escort group or the (d) reserve group **all of whose members are non-fearless types** (the per-group definition `F1EscortBreakable` already uses); `mindless` types are fearless (ENM) and never count; the patrol joiner never counts (T7 does not Rout ordinary joiners). It must exist in every formation at every party size after AddCap (4.5.3). Where a boss's reserve is mindless (the Mouthless Librarian's Inklings, the Head of the Table's Frozen Footmen), the escort supplies the group. V17s (i) checks it; BK3 checks each type's status. **The escort is the preferred home (lead's call, after the check):** a reserve-only group leaves the Herald without a fear target until 33%, or for the whole fight on a burst kill, so the group sits in the escort at fight start wherever the escort budget allows; V17s (i) lists every (boss, formation, size) where the reserve is the only group, and that list is told to the client with the content report. `ClassBossContribution` reports Herald fear-target availability by round.
- **Rejected (Q4):** B, keep HR-21 as agreed and measure the gap with `ClassBossContribution`.
- The ENM owner is asked to rule HX-2 Waver before M2 boss tuning.

**4.7.5 Immunities (change BR-20 ⚑; lead's call; M2).**
- **`final` records take no damage-type immunity** (narrows "at most 1"). Loot cannot read the final boss (`LootDoorInvariant`; the Last Hollow choice can change it), so the Viaticum fix must sit on the boss side.
- **F1 bosses take no damage immunity** (lead's call, within the cap): the level-3 hero's one damage type always works.
- **No boss resists its theme's `counterType`** (narrows the resistance choice; CS15): RT-4 makes the counterType the theme's shown breaker.
- **BX5:** a boss is never immune or resistant to its own arena clock's damage type.
- **Preview honesty:** a party effect blocked by Unyielding, a pip or a tag immunity is shown blocked before commit; the plate lists "Resists: <tag> (pips n)".

**4.7.6 Sizes (change BR-18; lead's call; told plainly).** Every M1–M2 boss is rules-size **≤ Large**; prose like "a huge hound" or the Governess's height is proportion and plinth (RC6). Each F1 theme has ≥ 1 Medium boss (Jailer, Master in the Frame, Guest, Executor). Unsure: which Vanguard verbs beyond Shove are size-gated; the Vanguard owner confirms. **Under graded Cut 1c the Kennel keeps only Old Ragged (Large); the one-Medium-boss row then holds for every theme that still has two bosses, and the Kennel is exempt while the Master is cut (new lead's call; V17s (c) reads the shipped roster).**

### 4.8 Action economy and party size

**4.8.1 The boss-action bundle (change BR-7 ⚑; lead's call; M2).**
1. **Signature is never a boss action** on any floor. Every boss authors its 1–3 options from {Strike, Reposition, Shed, Command}. Every signature lands on the boss's own turn behind a banner.
2. **F1 menu: {Reposition, Shed, Command}** (BN1: any Strike or Signature boss action breaks the F1 75% spike row).
3. **Shed** removes only a condition that was on the boss at the start of the current round (BL3). Freshly applied control therefore always costs the boss at least one turn under it. This still meets "a tool against sustained effects" **(settled)**.
4. No two boss actions on the same unit in one round (4.3.3).
5. No boss action targets a unit standing in a live Gambit footprint (M3, BR-32).
6. Upended denies boss actions (4.7.2); together with the denial budget (4.7.3), this is the complete list of boss-action denial sources.
7. Timing stays one-step: take the best action if its score ≥ the floor *(illustr.)*; ties by stable id. **Forced spend (lead's call, after the check):** at each eligible party-unit turn end, if the boss's unspent actions ≥ the eligible turn ends left before the boss's turn (counting only non-Downed party units still to act, and only units not yet targeted by a boss action this round), it spends one now on the best-scoring legal target, even below the floor, subject to focus spread. Unspent actions are lost, never banked. V17t covers it.
- The flag covers the A-3 menu and timing, the settled Shed and the "Hampered never blocks boss actions" clause (Upended is not Hampered, but it does deny boss actions; told plainly).

**4.8.2 Party size (change BR-21 ⚑; lead's call; M2).**

| Scales with party size | Never scales |
|---|---|
| HP (`hpByPartySize`), boss-action count min(cap, party units); escort budget, formation fill and AddCap at the arena party size (4.5.3) | Attacks per turn, damage per hit, Rhythm, Device and its uses, the Stir, phases |

- **"Party units" means non-Downed party units at round start** (flagged reading of A-3; BL5). A companion cascade lowers the count; report `HeroAloneRounds` and deaths following a cascade.
- **Shorthanded is rejected** (told plainly; BL6): companions are Downed, never killed, and solo on F2–5 is a choice the companions design refuses to reward with in-run power. Small-party relief goes **only** through approved per-size data: `hpByPartySize[1..2]`, the solo composition table and escort fill. Gates: `BossDeathShare` at party size ≤ 2 is never below party 4's on the same floor; and **an upper bound (lead's call, after the check):** on F2–5, `BossDeathShare(n ≤ 2)` ≤ 1.5 × `BossDeathShare(n = 4)`, with a hard cap on F5 of ≤ 45 per 100 *(illustr.)*, reported per class. A lower `hpByPartySize[1]` on F2–5 is the first lever to test. **Told plainly after the check:** the toy's expert deaths (F3 36 / 30 against 3 at party 4; F5 70 / 67 against 31) fail this bound today by a wide margin; if per-size data cannot meet it, it returns to the client rather than reopening Shorthanded (14.2).
- **F1 HP rows [2..4]** are filled by the formula and not tuned, because F1 is always solo (CMP 7.10) (flagged reading of CMP-7's content test; CS14).
- Content rule: on F1–2, signature areas are ≤ 3×3 or a line ≤ 5 *(illustr.)*, so a solo hero can always leave them in one move.

### 4.9 Floor 1 (change BR-22 ⚑; lead's call; M1 rows kept, M2 additions)

**Kept (settled):** the DH-26 rows; `Floor1BossSustain`; `Floor1BossMindReachable`; RT-9; `F1EscortBreakable`; no elite; no hard control; never Surprised; the near-safe Forecourt and Catch-up; the cowable group before the boss (CL-10); no hazards; the boss-potion lever never applies.

**Added:**
1. **`readyAt` ≥ 2** for every F1 limited ability (flagged ENM 9.3 for F1). With F1 loops of ≥ 3 beats and the first Downbeat at least 2 beats after `rhythmStart` (4.3.1 rule 6, a V17s (c) row), the first signature lands on boss turn ≥ 3 **(corrected after the check: the rule is enforced, not assumed)**.
2. **Old Ragged (⚑, settled M1 kit):** M1 unchanged. In M2 the roar takes `readyAt` 2 (flagged ENM 17.5 "M1 boss" and RS 8.4), with M1 values re-run (VG: P(death) 1–15%). Fallback if refused: Old Ragged is exempt, from this row and from 4.3.1 rule 6 (BK15), and its Rhythm opens on the roar.
3. F1 menu without Strike or Signature (BR-7); F1 Devices never harm (4.7.1).
4. **An F1 fight-level row** that counts escort damage at its worst arrival round *(illustr. ≤ 30% expected per round)*.
5. **`guard.floor1BossRounds`** set to the measured p90 fight length, bounded by the F1 length target at 8 (flagged DH 21.1 step 6 ⚑; BL2). The Sustain check then reads the fight that actually happens.
6. **F1 fight length:** median 4–6, p90 ≤ 8 rounds *(illustr.)*. A shorter fight is both safer and more fun (FN4).
7. **Signature share:** the telegraphed signature carries ≥ 40% *(illustr.)* of boss-side damage in rounds 2+ in the "Device used once" arm (CL 5.6 expects about half) **(arm narrowed after the check)**. Damage moves into readable beats, not untelegraphed at-wills.
8. **`BossDeathShare` F1 ≤ 2.5 per 100 arrivals is a simulated gate,** per class at the reference and lowest legal Con, with `Floor1ClassSpread` ≤ 10 points.
9. Every Rendition × mirror × formation is enumerated separately; a Rendition that fails any row is deleted, never tuned by HP.
10. The F1 arena takes no hazard at any tier: HZ 6.19's tier-2+ packages never pick the arena (flagged reading of HZ-25 ⚑, agreed HZ Q4). It may hold its one approved Landmark tick from M3 (the Master's pen bar only, BR-33).

**Lever order** if F1 deaths exceed the gate: (1) escort; (2) at-will dice; (3) HP (shortens the fight); (4) signature timing (`readyAt`, loop length); (5) phase option. Never loosen a guard row. **Deleting a Rendition under item 9 may never take an F1 boss below the 3 agreed in Q1: a failing Rendition is replaced by a re-authored one before M2 ships, and a boss that cannot field 3 passing Renditions returns to the client (new lead's call; 14.2).**

### 4.10 Variety and repetition (changes BR-23 to BR-26; agreed, Q1)

**4.10.1 Renditions and `BossVariantKey` (change BR-23 ⚑; agreed, Q1 for 3 per F1 boss in M2; lead's call otherwise; M2 for F1 bosses and the Pylorus; M3 for the rest).**
- A **Rendition** is one authored record and **the only drawn unit** (lead's call, after the check): it fixes one Guise, one formation and, optionally, the mirror (`Mirror` true, false, or null for "use the drawn bit"), plus `rhythmStart` and optional overrides: Rhythm per band, abilities (swap a signature or promote a limited ability, with its signature and limited flags), phase options per threshold, Devices, stance lines and the plate name (9.1). V17s rows (a), (b) and (e) run on the **effective kit**, the base record with the Rendition's overrides applied. Every boss has an explicit Rendition list in M2, never a Guise × formation cross product: exactly 3 on F1, ≥ 2 elsewhere, each Guise and formation referenced by at least one; phase, ability and Device alternates are M2 for F1 bosses and the Pylorus, M3 for the rest.
- **Every F1 boss ships exactly 3 Renditions in M2 (agreed, Q1).**
- **Rendition 1 is the RS 8.x text.** Other phase alternates vary RS text and need RS sign-off.
- **Never varied:** stats, HP rows, pips, thresholds, caps, guards, the theme's 6 (kind, not stats; `RenditionStatInert`, `RenditionLootInert`).
- **Must change the question (BK2, FN10):** each F1 Rendition beyond R1 differs from R1 and from each other in at least one of: phase option, Rhythm shape (loop length or beat order, including an inserted Lead beat), signature or signature condition, Device. **`rhythmStart`, formation, Guise and mirror never count toward the 3** (lead's call, after the check). V17s (c) compares these fields of the effective kits.
- **Condition spread (BK4):** at least one of each F1 boss's R2/R3 adds a non-Frightened signature, phase option or **Lead beat** within the theme's 6 (a Lead beat counted after the check, because it is a scheduled use shown on the Rhythm strip). **Old Ragged is exempt by name:** its settled kit cannot change, and its R1 already carries the Dazed Pin-Down (lead's call, after the check). In M3 the rule extends to F2–5.
- **Visible tell:** every Rendition beyond R1 carries one silhouette-level prop or stance change on the boss rig plus a Device or socket dressing change, listed in 5 (V17s and art check) **(lead's call, after the check)**.
- **Display:** the plate names it ("The Jailer of Hours, Counter-Clockwise"); the Stratum Card, door card and Playbill can name it.
- **Draw (BL14, CS):** a per-record child stream `Hash(routeSeed, "boss9", recordId)` with a **fixed count of 4 draws**: (1) the Rendition, uniform over the record's drawable Renditions (from M3 under Nightmare tier 1, over those flagged `Hard` plus the Nightmare Rendition); (2) the mirror bit, used only when the Rendition's `Mirror` is null; (3–4) reserved (M3 Opening and Nightmare). **No draws on the `route` stream itself**, so adding or cutting records, door swaps, unlocks, the Last Hollow choice and the Wake verdict never shift any other seed (V6 spirit). This is a new keyed child of the route seed, added to MAIN 6.3 (BR-2) **(corrected after the check)**. The content version, which stamps `cutState` (9.1), is logged in the reproducibility key.
- **Parity:** `RenditionParity` ±10% of simulated threat per class *(illustr.)*.
- **Folded Renditions count toward the survivor's 3 (new lead's call):** under Cut 1b the Jailer's "Cell Zero Opens" replaces its R3 *Lock-In* (both are 50% (d) cell-reserve alternates; keeping both would leave two Renditions asking the same question), so the Jailer still ships 3. Under Cut 1c the Lord's Gallery is a Guise of Old Ragged, not a Rendition (dressing only), and does not count. **In data (lead's call, after the check):** a folded Rendition carries `FoldedFromBossId` and `ReplacesRenditionId`; a folded Guise sits in `BossRecord.FoldedGuises[]` with its `FoldedFromBossId` and the `ReplacesGuiseId` it stands in for. The drawable list is the base list with each replacement applied when its `FoldedFromBossId` is cut under the content's `cutState` (9.1).

**4.10.2 Folded bosses (change BR-24 ⚑; agreed, Q1; M2, conditional on an F1 cut).** A cut F1 boss returns as a Rendition of its theme's survivor on the survivor's rig, at about 2 cost units instead of 8 *(illustr.)*:
- the Thing in Cell Zero (Cut 1b) → the Jailer's **"Cell Zero Opens"** (50% (d): the Cell Zero alcove becomes a reserve socket, and **a chained Lifer**, the Gaol's approved brute at its roster level, non-fearless, on `chain(3)` from the alcove's ring-bolt, wakes). It replaces the drafts' new "Chained Thing" type **(lead's call, after the check):** the Lifer is already in RS 8.3's 7-type Gaol roster, so no new type, no V1 `ThemeBudget` re-run and no new rig; it reuses the Lifer's roster rig with its RC3 face, cower, flee and kneel set, dressed with Cell Zero's chain. RS 8.3's boss line ("escorts of Turnkeys and Inmates") is read to admit the Lifer as this Rendition's (d) reserve only, flagged in BR-24 (⚑ Agreed). The Rendition pins a lone Turnkey as its formation, so AddCap and the solo minion cap leave the Lifer untrimmed (1 + 1 = 2 units; 5.3);
- the Master in the Frame (only under Cut 1c, client-only) → Old Ragged's **"Lord's Gallery"** Guise (portraits as Tall clusters; dressing only, because Old Ragged's settled kit cannot take phase alternates), replacing the Guise of R2 *Pack Mother*.

**4.10.3 The launcher F1-boss streak rule (change BR-25 ⚑; agreed, Q1; M2).** For personal seeds only, the launcher also redraws when the forecast F1 boss equals the F1 boss of **both** of the last two personal runs. Order: theme rule, Fallow (EV-20), boss rule, onboarding; shares R-SEED-6's budget of 8. **On exhaustion,** R-SEED-6's keep-the-best tie-break gains the boss rule in the same place: (theme, Fallow, boss rule, onboarding, earliest) **(lead's call, after the check)**. Bypassed for shared and daily seeds and the simulator; seed purity is untouched.
- **Memory (lead's call, after the check):** each entry of EV's `recentRuns` ring (MAIN 6.6 as changed by EV-19) gains `f1BossId`, written at run start from the pre-rolled F1 boss record for **every launched personal run**, whether it is later abandoned or ends before the F1 boss; shared, daily and simulator runs never write it (V17t). The ring already holds 3 personal runs, so it covers the memory of 2. The field is outside the reproducibility key. Flagged ⚑ Decided (MAIN 6.6 profile save) in BR-25, as EV-19 and BR-35 were.
- **Measuring it:** the fight simulator keeps bypassing the launcher; `BossRepeatRate` is measured by a separate **launcher emulation**, a Monte Carlo over seeds through the launcher's own code path (9.3, 9.4). Under approved Cut 1 it would be equivalent to a theme-streak cap of 2 (told plainly). It pulls an RS 12 M3 row ("profile-side anti-streak") into M2. The memory of 2 runs (`launcher.f1BossMemory`) is a client value, not tuning **(new lead's call)**.

**4.10.4 RS 12 Cut 1, graded (change BR-26 ⚑; agreed, Q1).**

Exposure *(illustr.; RS 10.6 clears 0.97 / 0.90 / 0.80 / 0.70)*: the F1 boss is met in about 98% of runs, including every losing-streak run; the Appetite in about 17%; the Head of the Table in about 35%.

**The cut lines (agreed, Q1), in the order the lead applies them:**

| Step | What it drops | Applied by | Fold |
|---|---|---|---|
| **1a** | The Appetite, **only if not yet built** at that review; a no-op otherwise | The lead, on the M2 schedule review, reported to the client | None (the Gut keeps the Pylorus) |
| **1b** | The Thing in Cell Zero | The lead, as 1a | The Jailer's "Cell Zero Opens" (4.10.2) |
| **Cut 2, with Cut 2R** | The Supper (F4 fixed Gut). **Cut 2R:** Cut 2 never drops a built Appetite | The lead, as 1a | None |
| **1c** | The Master in the Frame | **Client only**, below Cut 2 | Old Ragged's Lord's Gallery Guise |

- A cut still never removes the Heart, never leaves a tier without its pool, and never removes `F1EscortBreakable`, the charter or the Two Doors gates **(settled, RS 12)**. Anything below Cut 2 other than 1c, as before, returns to the client.
- **Cut 2R reconciled with 1a (new lead's call):** 1a is always applied before Cut 2, so an Appetite that is not built is already dropped when Cut 2 arrives; Cut 2R therefore only ever keeps a built Appetite, at no new cost. The toy's 2.34× (against approved Cut 2's 2.21×) is the built Appetite's cost counted inside Cut 2.
- **In every case:** 3 Renditions per F1 boss in M2; Pylorus Renditions in M2 (it is the only F4 boss in every run under Cut 2); **the Wake's gate is prioritised** (the largest single lever: core + Wake gives 15.6% same-boss), a production priority for the RS owner **(agreed, Q1)**.

| Option | Order | Same F1 boss (true, with R-SEED-6) | Same F1 fight | 3 in a row | Cost |
|---|---|---|---|---|---|
| **A (agreed): graded** | 1a, 1b, Cut 2 with Cut 2R, then 1c client-only | 26.5% (with Renditions and the launcher rule) | 8.7% | ≈ 0% | 2.58× (2.44× with F1 solo costing) |
| B (rejected): approved order | Cut 1 drops all three extras; fund F1 Renditions and the launcher rule | 34.7% with the launcher rule (44% without) | 14.5% | ≈ 0% | 2.45× (2.35×) |
| C (rejected): F1 extras last of all | Below the Supper | 22% | ≈ 4% | ≈ 0% | highest |

- **Why A:** approved Cut 1 leaves the floor every run sees at 44% same-boss repeat. Renditions fix the identical fight, not the same face.
- **Why the Thing before the Master:** its Dazed (a) duplicates the Jailer's; its specialist flail is the costlier signature; and Old Ragged's settled kit cannot take phase alternates, so losing the Master would leave the Kennel invariant.
- **Told plainly (with Q1):** the Gut is built first, so dropping the Appetite may save nothing by the time a cut is needed; the realistic Cut 1 saving comes from 1b. Graded steps relieve less schedule per cut, so under real slippage Cut 2 (the Supper) may arrive sooner.

### 4.11 Signatures, reward card, Viaticum, Memento (changes BR-27, BR-28)

**4.11.1 Carriage (change BR-27 ⚑; lead's call; M2).**
- IR's per-form carriage rule is read unchanged. Segment 9 authors each boss's `armed` flag and issue gear so that a carried form is always the weapon or worn piece the boss visibly uses: an `armed` boss has an at-will that uses its first form's family (`showsInUse`).

| Carried (armed) | Guarded (unarmed) |
|---|---|
| Jailer (Stopclock Mace); Thing in Cell Zero (Cell Zero Flail); Master in the Frame (Frame-Master's Spear); Executor (Executor's Letter-Knife); Drowned Carillon (Sunken Carillon Hammer); Precentor (Precentor's Baton); Weaver (Index Crossbow); Strongest Man (Ever-Lifted Barbell); Madame Ashes (Curtain-Fall Rod); Head of the Table (Oxblood Brigandine, worn, with a carving-knife at-will); Appetite (Appetite's Pestle); Night Governess (Warming Pan); Rousing Engine (Wind-Down Hammer) | Old Ragged (Bellwarden's Mail); Guest of Honour (Lying-in-State Leathers); Mouthless Librarian (Vellum Gambeson); Pylorus (Swallowed Splint) |

- Names are IR's approved names; the drafts' renames (Long Snuffer, Long Needle, Reveille Hammer) are withdrawn (CS4).
- **Chassis only on the boss (BL12):** a carried item supplies its damage die and reach; its lines are display only. Items never enlarge boss kits or the guard enumeration.
- **Damage type follows the theme's set** where the chassis differs (the Heart: psychic and piercing). The Warming Pan's at-will *There, There* deals psychic; the carried-use floater still names the item (told plainly to the IR owner).
- **F5:** the final boss always carries its first form; **no second form is chosen on F5**, because the Memento is 0 VP and usability is moot (flagged IR 5.2 second-form row for the Wind-Down Hammer).
- **Second forms owed in M2** (IR): Cell Zero Flail, Bellwarden's Mail, Sunken Carillon Hammer, Curtain-Fall Rod, Swallowed Splint. Under graded Cut 1 (agreed, Q1), the Gaol keeps a universal-chassis signature through the Jailer's mace, and the Kennel through the Master's spear until 1c, then Old Ragged's required second form. A cut boss's second form is not owed **(new lead's call)**. **Flagged after the check:** counting a draw-free second form as the theme's universal-chassis signature is a reading of IR's "≥ 1 universal-chassis signature per theme (M2+)" row, sent to the IR owner in BR-27; if refused, Old Ragged's second form is authored on a universal chassis under 1c (14.2).
- **Owed to IR:** budget-complete lines and value-1 echo lines per form (for example the Stopclock Mace's echo naming "the Countdown"); `signatureAbilityTag`. "Lair = arena" for boss caches is confirmed. Segment 9 supports the never-loot surcoat on Old Ragged.

**4.11.2 Reward card and F5 (change BR-28; lead's call; read).** The card is unchanged **(agreed)**: C/U/R/R choose 1 of 3, Yours/Theirs/Wayward, the F1 tier-1 scroll, 1 of 4 behind the Perilous Door (M3), offered in the arena after `BossDefeated` beside the opened cache. F5: no card; the carried signature drops as the 0-VP Memento with the premise-neutral caption; no loot summary. The **Viaticum** is chosen before the F5 Forecourt (LE-5): every final Rendition is simulated with each of the three Rare tools held in turn; each final boss's 33% (d) arrival is clustered so area tools score; no final boss is damage-immune (BR-20). Reports: `ViaticumBossShare`, CN 19.3's scroll-and-bomb share (alarm above 25%).

### 4.12 F-BOSS facts and the Playbill (changes BR-29, BR-5; lead's call; M2)

- **Run-specific variables per boss** (EV 4.13 asks 2–3): two seeded values, the **Rendition** and the **mirror** (where the Rendition leaves it open), which yield three fact lines: the Rendition and its phase change; the **Guise** (Device and reserve-socket side, mirror-aware, worded relative to the entrance); and the **formation** (fixed by the Rendition, 4.10.1). All draw-free from `BossVariantKey` **(restated after the check)**.
- **`bossQueue` order per floor** (no repeats; information only, never power):
  1. the signature card (the elite intel line, LE-18, unchanged, first);
  2. the Rendition fact ("The Jailer runs Counter-Clockwise: at half, he calls the cells");
  3. the Guise fact ("The pendulum hangs to your left as you enter");
  4. the formation fact ("Two Turnkeys stand before it");
  5. fixed Lore facts (Rhythm, Device rule, weakness).
- Each boss authors ≥ 4 facts plus mirror-aware variants (V17s).
- **Channels (corrected after the check):** F1–3 Curiosity F-BOSS options (EV 5.3: G5, K5, W3, C1, S1, Ca6; the only channel on F1, where the Forecourt is empty); F2–3 Forecourt Interludes; F4–5 Interludes substitutive only **(settled)**; F5 through Strands cashed at the Coat Hooks, which read the pre-rolled final boss's keys (both Last Hollow candidates' keys exist in the pre-roll); Yield interrogation and the elite intel line; Testimony (Later, EV-41).
- **The Playbill (change BR-5 ⚑; M2)** is their home: a boss card opened from the Stratum Card, the door card and the Forecourt's arena-facing edge. Always lines: name, silhouette, level band, pips, boss-action count, phase marks with type icons, signature list with shapes, the Device's one-line rule, "Carries/Guards". "?" lines (Rendition, opening Rhythm, Guise, formation, weakness) fill only from the `bossQueue`. It folds in Model C's arena reveal card. The flag: 13.2 lists signatures "from the boss's first turn"; the Playbill lists them from the Stratum Card.

### 4.13 The final boss and the Dreamer (change BR-30 ⚑; lead's call; agreed, Q5 for the Later line; M2 rules, Later set piece)

- **Keepers of the sleep.** Final bosses (the Governess, the Engine, the M3 Lullaby) are the dream's keepers, never the Dreamer, its avatar, body or relation. The Dreamer appears only as visible environment, display only: the mattress heaves **during** an act only, the sheets and bedframe shift, the waking-light window brightens one step per phase (at 33% visibly so, tied to the last band). **No sound attributable to a sleeper:** the dry-breath family is HZ's reserved Heart Stir cue, played only when an act is announced or fires, never in a bed or ambience stem **(lead's call, after the check:** the earlier "room-wide breath cue" is removed; it contradicted section 6 and HZ's RC7 table**)**. Never a body, face, silhouette under the sheets, voice or age.
- **`FinalBossNoDreamer`** (V17s) is defined in data: every Heart arena asset (mesh, animation, bark, audio stem) carries a required `dreamerDepiction` enum (`none` or `environment`), no humanoid silhouette may sit in or behind any bed element, no Heart bed or ambience stem may contain a breath-family sample, and a banned-term list is checked over names, barks, stance lines, phase cards, Coat Hooks payoffs, Memento captions and victory text **(lead's call, after the check)**.
- **Tone flag:** "The Dreamer Turns Over" points at where a sleeper is, not who; it goes to the tone-by-depth review with the rule card's fiction line "The bed heaves." Suggested fallback display name: **"The Bed Turns Over"** (HZ owner decides).
- **The Heavy Thing (lead's call; RS 9.3 delegates it).** The one fixed rule in both final arenas:
  - Interact at its cleat (or the ranged trigger): a fixed 3×3 drops **at once**, inside the operator's action (TrapRecord format; not a Cord, not a clock).
  - **Piercing** at the harm band ≤ CapRound, Reflex for half; Prone on a failure. Both ways. **Once per fight**, party-only, an Expose (Upended on the boss).
  - Dressed from the floor-4 theme's `keepsakeArenaDressing` in neutral brass, glass, wood and bone: the Supper's dark iron hoop with unlit candle cups, or the Gut's hanging rib-cage. **No ice, frost or blue**, never Frozen, so it never reads as the F4 Icicle Chandelier (RC1, RC4). The rule never varies with the dressing. Under Cut 2 (agreed, Q1) floor 4 is always the Gut, so the rib-cage is the only M2 dressing **(new lead's call: stated)**.
- **Each final boss has its own Device too** (FN8), so the last fight is the most distinctive, and the two are of different kinds **(lead's call, after the check)**: the Governess's **Lamp Snuffer** (Deny: snuffing the night-lamp darkens the bed lanes, blocking sight for Tuck In, which needs sight, until the end of the next round; both ways) and the Engine's **Mainspring** (Cancel, with a ranged trigger on the exposed escapement; it becomes the Alarm Clock's Wind Back at M3).
- **The bodiless true ending (agreed, Q5; Later).** RS 8.11's "Later adds the Dreamer as a true-ending boss (premise permitting, 7.5)" is replaced by: **Later adds a bodiless true-ending set piece: after the last keeper falls, the room wakes (every arena act at scale, in sequence, as display) and the party walks into the waking light.** A unit with a model is a body; RS 7.5 cannot survive a Dreamer boss. **Bounds, bound now inside Q5 (new lead's call, tightened after the check):** the set piece has no unit, no HP bar, no plate and no banner with a name; it **cannot harm, move or condition any unit** (display-only acts at scale); **victory is recorded at the last keeper's `BossDefeated`**, before it begins; the walk into the light is player-paced and cannot be failed; it obeys `FinalBossNoDreamer`. A harmful set piece would be a new client question, not a Later design choice. Rejected (Q5): B, keep the Later line as a premise item for RS 19.2.

### 4.14 M3: arena Stirs, Omens, Nightmare and Gambits (changes BR-32 to BR-34)

**4.14.1 Arena Stirs (change BR-33 ⚑; lead's call).**
- **M2g:** the Precentor's arena may host the **Organ Swells**, and Madame Ashes's arena **Pit Breathes**, as that floor's **one** signature Stir **instead of** the signature room (flagged ⚑ HZ 6.3, agreed HZ Q1). "Never more than one new rhythm per floor" and the pilots' ship gates are unchanged in wording. Also gated by BX8's glance test and `StirRoundsDelta` ≤ 0. Fallback: the signature room keeps it.
- **M3, the Keynote layer (Model C's vision):** where an HZ 6.17 Stir ships in an arena, one non-hard-control signature may be **Keynote-bound**: ready only in the round after each strike (the off-beat rule; flagged ENM 9.3 readiness source). **Overtaken:** a boss on its own due squares takes the act (both ways **(settled)**) and its bound signature misses its next window, once per phase, no pip loss.
- **Rejected:** Downbeat (the boss cueing its own clock), because it moves toward the lair-action territory ABL 2 excludes; Model C's M2 Dials, because they spend the one clock without acting and put map digits on F1 two waves early (CS2).
- **Heart Act Keynotes stay M3** (the Dreamer Turns Over keeps its HZ Prone-ratio gate; the Alarm Clock its own) (CS13). The Pit Breathes row for the Strongest Man is not adopted.
- **Missing HZ 6.17 rows, completed:**

| Boss | Arena clock | Wave |
|---|---|---|
| Thing in Cell Zero | None (the leash ring is a rule, not a clock) | — |
| Old Ragged | None | — |
| Master in the Frame | The pen bar as a **Landmark tick** (a door on a 3-round dial: the pen bar can be dropped only while the pen stands open), the arena's one clock; harmless (HZ HT7) | M3 |
| Weaver of the Index | None (its Cross-Reference line is the read) | — |
| Strongest Man | None (the dunk tank is the rhythm) | — |
| Appetite | None | — |
| Guest of Honour / Executor | None | — |

**4.14.2 Omens, Nightmare tiers and the Perilous Door (change BR-34 ⚑; lead's call; M3).**
- **Kind, not stats** **(decided, RT-21):** each boss authors one **Nightmare Rendition**. Tier 1: draws are restricted to the boss's Renditions flagged `Hard` (9.1) or its Nightmare Rendition. Tier 2: F2+ arenas gain their HZ 6.17 clock (or a Gambit, BR-32); F1 uses a Rhythm or formation change only. Tier 3: phase (e) may leave a Remnant **(agreed, ROOM Q5)**.
- HP, damage, pips and thresholds never change; the ladder's escort budget goes through AddCap.
- **Perilous Door:** the boss uses its Nightmare Rendition one tier up (cap 3), plus the 1-of-4 card **(agreed, LE-19)**; never an extra boss action; the Stratum Card and Playbill show it before commitment.
- **Mood readings (told plainly):** Gut stage 2, "Pylorus bile phase at 75%" → the Pylorus's first (e) bile set is pre-armed and announced at `ArenaEngaged` and fires at round 2's arena round-end step (4.3.4); 66/33 stay. A true 75% threshold would reopen ENM 17.5 ⚑ and is refused. Carnival "Act III sets the arena" → selects the Carnival boss's Act III Guise. Heart "Nearer Waking" → HZ's own Dreamer period 3 → 2, still under OneDueThing.

**4.14.3 Gambits: committed boss signatures (change BR-32 ⚑; agreed, Q3; M3 gated, else Later).**
- One `gambit`-flagged signature per boss: **never on F1**, never a boss action, never hard control **except** the Head of the Table's Frozen Toast **(agreed, Q3)**: Frozen then lands only on units that chose to stay, the fairest form of the game's one hard control.
- **Commit (order made exact after the check; lead's call, after the check):** at the end of the boss's own turn, after its action, the pointer advances (4.3.1 rule 1), and then the commit check reads the **new** beat: if it is that Downbeat (or the Gambit signature is pending), the boss commits. If no legal footprint scores at the floor, nothing is committed and the Downbeat waits under 4.3.1 rule 2. The existing area scorer picks the squares one-step on present occupants × a dodge discount 0.5 *(illustr.)*, so ENM 13.6's objection does not apply. Squares show at RC6 telegraph tier with a "Next turn" glyph (no digit).
- **Resolve:** at the start of the boss's next turn, **as that turn's action**, on exactly those squares; then the boss may move. One effect per round per unit. A committed Gambit resolves **even if the boss is controlled** (Dazed, or control after its pips are gone) and is then the only thing the boss does that turn **(lead's call, after the check)**.
- **Commitment:** never voided by Cancel, Upended or anything except the boss's death or combat end. A Cancel Device used before the commit still skips the Downbeat.
- **Escape guarantee:** for every unit inside, a free exit reachable with its **current** effective movement (standing from Prone and speed conditions included); a unit that cannot leave, or is held by the boss's group, counts as saving: half damage, no rider (the no-pin rule). Due squares never include an Arch, the doorway band or the Clearway. **It is a runtime constraint on the scorer** (candidate footprints are filtered by that reachability at commit), backed by the no-pin rule; V17s (l) checks the authored footprint shape and the no-pin rule's presence, and V17t asserts it on replays **(lead's call, after the check:** a position-dependent property cannot be proven statically**)**.
- **Spike:** maximum ≤ 60% of the lowest legal hero, save-based (no crit); no boss action may target a unit in a live footprint.
- **Not a clock** (flagged reading of HZ 6.2.1: no digit or meter on the map); bounded by OneDueThing.
- Gates: `GambitDodgeRate` ≥ 70% in the novice arm; `GambitStayRate` 10–40% *(illustr.)*; BX8 glance test. If the gates fail, Gambits stay Later **(agreed, Q3)**. Candidates: Precentor (Crescendo), Weaver (Cross-Reference), Madame Ashes (Curtain of Flame), Governess (Tuck In), Engine (Reveille), and the Head of the Table (Frozen Toast, the exception).
- **The Frozen Toast Gambit in full (new lead's call):** its footprint is the single square of the unit it targets at commit; it resolves Frozen (1 turn) only on a unit still standing on that square; the no-pin rule means a unit that could not leave takes no Frozen. It still counts as the boss's one hard-control ability (cooldown ≥ 3, `readyAt` 2) and as one of the F4 encounter's two hard-control sources with the chandelier; it is not a Downbeat spike (no damage), so the 60% row does not apply to it. Its Gambit form ships only with the rest of BR-32; until then it stays the forecast signature of 5.5.
- **Rejected (Q3):** B, M3 without the Frozen Toast exception; C, keep Gambits Later.

### 4.15 Determinism (lead's call; M2)

- Boss identity stays the R-SEED-3 / final-order pre-roll **(settled)**; segment 9 never redraws it. Graded Cut 1 (Q1) changes which records and Renditions are drawable (`cutState`, 9.1), never the draw (`BossVariantKey` is per record).
- `BossVariantKey`: per-record keyed child of the route seed (`boss9`), fixed 4 draws, never consumed on the route stream (4.10.1); a new MAIN 6.3 sentence (BR-2). Arena stamping makes no draws. Escort identities use EP's existing streams.
- Fights use the combat stream only (saves). Rhythm, OneDueThing, reserve placement, Upended and Gambit squares are pure functions of state, with template-local tie-breaks.
- **Event order at the arena:** Catch-up (F1, M2+) → `ArenaEngaged` → Take Places (or, if combat is active, the boss group joins) → combat start. Per round: unit turns (boss actions at turn ends) → turn-end steps → the arena round-end step (4.3.4; HZ order **(settled)**: acts at step 1, (d)/(e) headlines resolving at step 1 under OneDueThing, announcements at step 4).
- **The launcher rule** (4.10.3) acts before the seed, on personal seeds only; it never enters the reproducibility key **(agreed, Q1; settled R-SEED-6 pattern)**.
- **Save:** route state holds nothing new beyond the hash inputs; boss life state gains {rhythm pointer, pending signature, `pendingCancel`, Device charges, reserve status, headline delay rounds, live Gambit}; the floor state gains `sanctumEngaged` for the rest rule and the party's held per-fight state (4.2.5) **(new lead's call; extended after the check)**; the profile gains `recentRuns[].f1BossId` (4.10.3).
- Telemetry: `ArenaEngaged`, `BossPhaseEntered{rendition}`, `DownbeatUsed`, `DownbeatWaited`, `DeviceUsed`, `Upended`, `ReserveArrived`, `HeadlineSlipped`, `GambitCommitted` (M3), `BossReset`, `SanctumRestRefused` **(new lead's call)**, and `RestTaken{floor, sanctumEngaged, bossPhaseBand, inSanctum}` **(lead's call, after the check: feeds `MidBossRestRate` and `RestBetweenPhasesRate`)**.

### 4.16 UI hooks (segment 10)
- **Two-tier plate** (FN9): always visible are name, Rhythm strip, pips and phase marks; on hover the Rendition, stance line, boss-action count, "Carries/Guards", "Resists", Upended. BX8's glance test, the greyscale check and the colour-blind check run on the plate as well as the map.
- The 13.2 banner with the universal sting (RC7); the phase card; the threshold preview; the Playbill; Take Places; Device previews; reserve silhouettes and wake chip; Gambit squares (M3).
- The reward card (1 of 3 / 1 of 4 with the Perilous badge); the Memento victory screen; the F-BOSS floor panel; the carried-use floater; pull-back zoom (w + h ≤ 42); an optional skippable intro card ≤ 2 s.
- The Sanctum rest refusal ("The Sanctum will not let you sleep.") on the Rest button, and the arena-entry confirm (4.2.1, 4.2.5).
- **The arena-entry confirm gains one Behind-you line** (rooms not entered, the Treasure field, "best remaining" under LE's k ≥ n/2 rule; no secrets found/total, no loose-end count); with the confirm turned off, a one-time non-modal status-ribbon toast at the doorway band (4.2.1) **(change XI-11; lead's call; M2; exploration and information design 4.4.4)**.
- **Card queue slots (change XI-19; lead's call; M2):** the arena-entry confirm with the Behind-you line is queue slot 2 (after lethal, end-turn and ally confirms) and the boss phase card slot 3, above reward, Viaticum and loot summary (slot 5); a displayed card is pre-empted only by slot 1 (exploration and information design 4.9.8).
- Never the word "seal".

### 4.17 Hooks to segments 10–12
- **Segment 10:** as 4.16, plus the Phase Score (BR-36), the Boss Ledger and Grudge pages (M3, BR-35), grouped escort playback. **Answered** (XI-11, XI-19; exploration and information design 4.4.4, 4.9.8, 5).
- **Segments 11–12:** `BossVariantKey` hash and draw counts; template-local coordinates; the event order; the launcher F1-boss rule's place in the launcher order (BR-25); V17s/g/t; the reports and arms (9); release-seed coverage of every (boss, Rendition, mirror).

---

## 5. Per-theme boss catalogue (lead's call; agreed, Q1 for F1 Renditions and the folds, Q3 for Gambit candidates; all illustrative content)

### 5.1 Catalogue rules (BK)

- **BK1** R1 is always the RS 8.x text; R2/R3 phase alternates need RS sign-off.
- **BK2** Every F1 Rendition beyond R1 changes the question (4.10.1): phase option, Rhythm shape, signature or its condition, or Device, compared on effective kits; never `rhythmStart`, formation, Guise or mirror alone. Every F1 boss ships exactly 3 in M2 (agreed, Q1).
- **BK3** Escort and reserve types come from the theme roster and the RS 8.x boss escort line (a reserve outside that line is flagged, as the chained Lifer is in BR-24); V17s checks each type's fearless status, and on F2–5 that every formation holds a non-fearless group (agreed, Q4), in the escort wherever the budget allows.
- **BK4** Condition spread: at least one non-Frightened signature, phase option or Lead beat among each F1 boss's R2/R3; Old Ragged exempt by name (4.10.1).
- **BK5** Every F1 at-will maximum ≤ 30% of the reference hero; the Jailer's halberd is 1d6+2.
- **BK6** Every Device has a ranged trigger and is class-blind.
- **BK7** Authored position rules open Device windows (Toasts only from the head chair, in the first band; Hold Aloft only from the stage; Eulogy only from the lectern). A post on or under an Expose face takes `deviceBait` (4.6).
- **BK8** No arena name collides with an existing whole-word name (A.5).
- **BK9** Every damage type sits in the theme's damage set; carried chassis follow 4.11.1.
- **BK10** Rhythms are written `O` Off, `L:` Lead, `D:` Down; all loops, shapes and dice are *(illustr.)*.
- **BK11** No Device in the F1 arena harms, imposes a condition or moves a unit.
- **BK12** At most one reserve group per boss, at most one reserve socket per arena.
- **BK13** Every Rendition beyond R1 has a visible tell on the rig and a Device or socket dressing change (written "Tell:" below) **(lead's call, after the check)**.
- **BK14** Every Rendition pins one formation; a (d) Rendition's formation leaves its reserve untrimmed at every allowed party size (4.5.3) **(lead's call, after the check)**.
- **BK15** F1 loops are ≥ 3 beats with the first Downbeat ≥ 2 beats after `rhythmStart`; a loop changes shape only at an (a) or (b) entry (4.3.1) **(lead's call, after the check)**.

### 5.2 Summary

| Boss | Theme, floor | Size, armed | Question | Device (kind) | Last threshold | Clock (wave) |
|---|---|---|---|---|---|---|
| Jailer of Hours | Gaol F1 | Medium, armed | When do you stop time? | Pendulum (Cancel) | 50% (a) | — |
| Thing in Cell Zero | Gaol F1 (Cut 1b; folds into the Jailer) | Large, armed | How far can it reach? | Windlass (Deny) | 50% (a) | — |
| Old Ragged | Kennel F1 (M1) | Large, unarmed | Can you keep out of its sight? | M1 root-columns; M2 Heel Whistle (Cancel); R3 Kennel Shutter (Deny) | 50% (a) | — |
| Master in the Frame | Kennel F1 (Cut 1c, client-only, below Cut 2) | Medium, armed | The lord, or the whelps he calls? | Pen bar (Divide) | 50% (d) | Landmark tick (M3) |
| Guest of Honour | Wake F1 (gated) | Medium, unarmed | Can you silence the eulogy? | Mourning Curtains (Deny) | 50% (a) | — |
| Executor of the Will | Wake F1 (gated) | Medium, armed | Will you let him finish reading? | Probate Desk (Cancel) | 50% (d) | — |
| Drowned Carillon | Choir F2 | Large, armed | Where are you standing when it sings? | Bell Rope (Cancel); second handle the sluice (Deny) | 33% (d) | Tide High (M3) |
| Precentor | Choir F3 | Medium, armed | Do you dare use the water? | Grounding Chain (Deny) | 33% (d) | Organ Swells (M2g) |
| Mouthless Librarian | Silk F2 | Medium, unarmed | Who is the loudest? | Returns Bell (Divert) | 33% (d) | Leaning index (M3) |
| Weaver of the Index | Silk F3 | Large, armed | Which threads do you burn? | Thread Anchors (Deny) | 33% (e) | — |
| Strongest Man | Carnival F2 | Large, armed | Will you dunk the showman? | Dunk Tank (Expose) | 33% (d) | — |
| Madame Ashes | Carnival F3 | Medium, armed | Will you bolt the wagon before the finale? | Wagon bolt (Divide) | 33% (d) | Pit Breathes (M2g) / Turning Ring (M3) |
| Head of the Table | Supper F4 (Cut 2) | Medium, armed | Will you drop the chandelier when he toasts? | Icicle Chandelier (Expose) | 33% (d) | Courses (M3) |
| Pylorus | Gut F4 | Large, unarmed, `chain(2)` | Can you keep out of its mouth? | Jaw Prop (Cancel); second handle the rescue (Release) | 33% (d) | Brim (M3) |
| Appetite | Gut F4 (Cut 1a, only if not built; kept by Cut 2R) | Large, armed | Will you feed it? | Feeding Hatch (Sate) | 33% (d) | — |
| Night Governess | Heart F5 | Large, armed | Will you lie still? | Lamp Snuffer (Deny) + Heavy Thing | 33% (d) | The Dreamer Turns Over (M3) |
| Rousing Engine | Heart F5 | Large, armed | Will you wind it back? | Mainspring (Cancel) + Heavy Thing | 33% (d) | Alarm Clock (M3) |

### 5.3 Floor 1 (one 50% phase; 2 pips; no hazards; no hard control; menu {Reposition, Shed, Command}; 3 Renditions each in M2)

**The Jailer of Hours** (Gaol; Medium; armed; carries the Stopclock Mace).
- **Arena: the Pendulum Hall.** Hall, 15×17 *(illustr.)*; a clock-face floor mosaic (dressing); two Tall clock-case clusters; foot-stocks as Low cover. Guises: G1 pendulum on the left wall; G2 on a back dais between cell blocks; G3 on the right with a gallery.
- **Kit:** Halberd Sweep (at-will, reach 2, 1d6+2 slashing); key-mace (at-will, adjacent); **Countdown** (signature, burst 2, Will or Frightened, cooldown 3); Chain of Keys (limited, line 3, Staggered).
- **Rhythm P1:** `O · O · D: Countdown` (loop 3; first use turn 3). **50% (a):** Stop the Clock (signature, adjacent, Fortitude or Dazed) takes the Countdown's beat: `O · O · D: Stop the Clock`.
- **Device: the Pendulum (Cancel; one charge per fight on F1).** Party-only, never a hazard at any tier **(settled, HZ 6.17)**. The bob hangs at wall height over non-walkable squares beside its wall slot; its walkable **catch square** carries the RC5 Device glyph. One verb and one look in both phases: **catch the bob** (Interact on the catch square; ranged trigger: strike the bob; ForcedEntry(boss): a boss shoved onto the catch square triggers it). At phase entry the bob is released again as an act animation ≤ 1.0 s. Its state shows only by its uses pip and a ready or spent badge, never by swing speed; the Rhythm strip alone carries timing **(lead's call, after the check:** the drafts' "swing slows as the Downbeat nears" was a map meter and could show a beat that waits**)**.
- **Reading:** the "announced Countdown" is the Rhythm strip and its banner; no map digit, no committed wind-up (BT6). The arena's 5/4 ticking is the Gaol's bed texture only, never tied to the Rhythm (6).
- **Renditions** (each pins one formation; corrected after the check):
  - R1 *Ten to Three* (RS text). Formation: Turnkey + 2 Inmates (minions), the Inmates clustered in a 3×3 starting ≥ 3 from the post, outside the aura (the F1 Herald and Arcanist formation; 1 + ½ + ½ = 2 units, 2 minions).
  - R2 *Counter-Clockwise*: G2, `Mirror` true; loop 4 `O · L: Chain of Keys · O · D: Countdown` (Staggered as the non-Frightened Lead). Formation: 2 Turnkeys, one starting outside the aura (escort kept inside RS 8.3's Turnkeys and Inmates; the drafts' Lamplighter is withdrawn). Tell: the halberd carried reversed and a ring of keys at the belt; the clock-face mosaic runs backwards.
  - R3 *Lock-In* (RS sign-off): 50% (d) 2 Inmates from the bunk-row reserve socket **instead of** (a); the Countdown stays all fight (one change per entry). Formation: a lone Turnkey (1 + ½ + ½ = 2 units and 2 minions with the reserve, so nothing is trimmed). Tell: the Jailer's keys in hand, the bunk-row grille chained shut. **Under Cut 1b, R3 becomes *Cell Zero Opens*** (4.10.2; formation a lone Turnkey; reserve the chained Lifer; tell: the Cell Zero door ajar, the Jailer's lantern lowered toward it).
- **Answers:** Vanguard: shove the Jailer onto the catch square (ForcedEntry triggers the Cancel); Herald: Countdown is fear, the Inmates cowable; Arcanist: the Inmate pair; Shade: the cell-front flank pair.

**The Thing in Cell Zero** (Gaol; Large; armed; carries the Cell Zero Flail; second form guarded). **First F1 cut (Cut 1b, agreed Q1).**
- **Arena: Cell Zero.** Hall, 17×17. Ring-bolt anchor at the open cell; its `chain(5)` radius shown by the cross-theme leash ring mark (6; not a clock, never painted dressing). Tall cell-fronts outside the ring break long sightlines (anti-kiting).
- **Kit:** chain flail (at-will, reach 2); **Chain Lash** (at-will, line 3, so standing off is not free); roar (signature, cone 4, needs sight, Frightened); Chain Crack (limited, line 4, Prone).
- **Rhythm P1:** `O · O · D: roar`. **50% (a):** the Cell Zero Slam (Dazed, adjacent).
- **Device: the Windlass (Deny).** Once per phase the radius shrinks by 1 (minimum 3), shown as a ≤ 1.0 s act on the leash ring. It moves no one (BK11).
- **Renditions:** R1 RS. R2 *The Chain Snaps* (RS sign-off): the last-threshold rule forbids (b) at 50%, so R2 keeps (a) at 50% and the snap is the stance: **told plainly, R2 is offered only if `RangedOnlyWinShare` > 40%, as a replacement R1 (a) "Dazed slam and the chain snaps"**, with RS sign-off; otherwise R2 is *Long Reach*: Guise 2, the Windlass on the far wall, loop 4 `O · L: Chain Crack · O · D: roar` **(new lead's call: a default R2 so the Thing ships 3 Renditions while it is in M2)**; tell: a second, longer chain coiled at the ring-bolt. R3 *Old Iron*: Guise 3, `Mirror` true; loop 3 `O · O · D: Chain Crack`, with Chain Crack (Prone) **replacing the roar as the single P1 signature** (an ability override; RS sign-off; non-Frightened) **(corrected after the check:** the drafts' loop of 2 left the roar a signature with no Downbeat and put the first signature on turn 2**)**; tell: rust-scaled hide and a broken roar-plate on the muzzle.
- **Formations:** R1 and R3 Turnkey + 2 Inmates (clustered, outside the reach of the ring at start); R2 2 Turnkeys.
- Report `RangedOnlyWinShare`, alarm > 40% (as for every `chain(n)` or post-bound boss, 4.6).

**Old Ragged** (Kennel; Large; unarmed; guards Bellwarden's Mail; settled M1 kit).
- **Arena: the Den.** Den, 17×17, under the painted moon. **M1:** architectural root-columns (three Tall clusters, template walls, not RT pieces) as the line-of-sight blockers and the Deny counterplay (the roar needs sight) (BR-1 reading of RS 12's M1 "No terrain"; told plainly). **M2:** fallen-log pieces and a bone heap added. Signature cache at the den's back wall.
- **Kit (settled):** Bleeding claw, bite, roar (Frightened, signature), damage-only slam; 50% (a) the roar becomes a limited Dazed (**the Pin-Down**, signature; renamed from "the Maul", a weapon, after the check).
- **Rhythm (M2):** `O · O · D: roar · L: slam` (loop 4); at 50% the Pin-Down takes the roar's beat. **M1:** no boss actions **(settled)**. **M2:** options {Reposition, Shed} (A-3 applies; kit text untouched).
- **Device (M2): the Heel Whistle** on a wall hook beside the dead lord's leash rack, a wall slot (Cancel; one charge per fight; noise 4). Ranged trigger: knock the whistle from its hook **(moved after the check:** a free-standing chair as a Device body breaks RC2 and TL-43**)**.
- **Renditions (M2; re-authored after the check so each changes the question without touching the settled kit):**
  - R1 the settled kit; G1; formation 2 Whelps clustered, starting outside the aura.
  - R2 *Pack Mother*: G2; a Rhythm shape change, loop 3 `O · L: slam · D: roar` (the pack presses: the slam leads into every roar); formation Gaunt Hound + 1 Whelp. Tell: a Whelp at heel on the plate art and a scarred flank; the den strewn with bedding.
  - R3 *Drawn Shutters* (renamed from "Empty Chair", the Supper's Obsession): G3, `Mirror` true; an **alternate Device, the Kennel Shutter** (Deny: a two-state Tall line of hunting shutters that, lowered, cuts the roar's sight lanes across the den; one charge per phase) in place of the Heel Whistle; formation 2 Whelps clustered. Tell: shutters along the den's flank and a torn hunting coat on Old Ragged's back.
  - Old Ragged is exempt from BK4 by name (its R1 carries the Dazed Pin-Down). Under Cut 1c, the **Lord's Gallery** Guise replaces R2's Guise (dressing only; 4.10.2).
- **Formations:** M1 the boss alone **(settled)**; M2 as each Rendition pins.

**The Master in the Frame** (Kennel; Medium; armed; carries the Frame-Master's Spear). **Cut 1c only, below Cut 2, client-only (agreed, Q1).**
- **Arena: the Portrait Kennel.** Hall, 15×19; the lord's empty gilt frame on the far wall; portrait easels as Tall; the **Whelping Pen** reserve socket on a side wall ≥ 6 from the start zones.
- **Kit:** boar spear (at-will, reach 2); hunting knife (at-will); **the View Halloo** (signature, burst 5, by hearing, Frightened; renamed from "the Horn" after the check, because horns are reserved for joiners); Spear Lunge (limited, line 3, Prone).
- **Rhythm:** `O · O · D: View Halloo`. **50% (d):** 2 Whelps from the pen (formation: a Gaunt Hound only; 1 + ½ + ½ = 2 units, 2 minions).
- **Device: the pen bar (Divide).** Drop it: the Whelps' arrival slips one round, once per fight. **M3:** the pen bar becomes the arena's one Landmark tick (4.14.1).
- **Renditions:** R1 RS; G1. R2 *Gone to Ground* (RS sign-off): G2; 50% (a) the View Halloo is swapped for the Spear Lunge as a signature (Prone line; non-Frightened); no reserve; Device swapped to **the Turned Portrait** (Cancel: turn the portrait to the wall; renamed from "the Empty Frame", new lead's call); formation a Gaunt Hound and 1 Whelp. Tell: the spear held low for the charge; the gilt frame turned half to the wall. R3 *Kennel Row*: **G3, a Hall Guise** of pews and portrait rows with the pen near the Bossgate (corrected after the check: a Nave Guise would be a second shape kit); a Rhythm shape change, loop 4 `O · L: Spear Lunge · O · D: View Halloo`; formation a Gaunt Hound only. Tell: a hunting coat and a brace of leashes; kennel tags hung along the portrait rows.
- **Audio check:** the View Halloo is a hoarse hunting cry in the voice family, never a horn (reserved for joiners) and never the wooden rattle of HZ's Horn Post (RC7 audio-blind test with both, 6).

**The Guest of Honour** (Wake, conditional; Medium; unarmed; guards the Lying-in-State Leathers).
- **Arena: the Viewing Room** (renamed from the drafts' "the Lying-in-State", which is IR's item name; new lead's call). Nave, 15×17; canopies (Tall), coffins and pews (Low), a lectern. **Eulogy only from the lectern** (BK7).
- **Kit:** cold hand; lily pin (ranged 4); **Eulogy** (signature, all with sight of the lectern within 6, Frightened); Mourner's Embrace (limited, Staggered). **50% (a):** Last Rites (Dazed).
- **Rhythm P1 (authored after the check):** `O · O · D: Eulogy` (loop 3); at 50% Last Rites takes the Eulogy's beat.
- **Device: the Mourning Curtains (Deny):** a two-state soft Tall line blocking the lectern's sight lanes.
- **Renditions:** R1 RS; G1. R2 the RS fallback (d) Guests (Teetering-ruling form); G2; its formation leaves the Guests' headroom (BK14). Tell: a black-edged invitation pinned to the Guest's breast; the family pews uncovered. R3: G3, `Mirror` true, Undertaker-led formation; a Rhythm shape change, loop 4 `O · L: Mourner's Embrace · O · D: Eulogy`. Tell: the Guest's veil drawn back; the lectern draped in crepe.
- Escorts never the Pallbearer or the Late Uncle **(settled)**.

**The Executor of the Will** (Wake, conditional; Medium; armed; carries the Executor's Letter-Knife).
- **Arena: the Probate Room.** Hall, 15×15; the Probate Desk; **the family pews** reserve socket.
- **Kit:** letter-knife (adjacent or thrown 4); sealing wax (ranged 3); **Read the Will** (signature, sight within 6, Frightened); Disinherit (limited, ranged 4, a non-Frightened condition inside the Wake's 6, V17-checked).
- **Rhythm P1 (authored after the check):** `O · O · D: Read the Will` (loop 3).
- **50% (d):** 2 Guests from the pews; R1's formation leaves their headroom (BK14). **Device: the Probate Desk** (Cancel only: tear the page; the next Read the Will is skipped). The drafts' Divert clause is dropped after the check: one kind per handle, and Divert means nothing on F1, which is always solo.
- **Renditions:** R1 RS; G1. R2 (a) Disinherit becomes the signature, no reserve (RS sign-off); G2. Tell: the will rolled and sealed in black wax; the desk's inkwell overturned. R3: G3, `Mirror` true, Cousin-led formation; a Rhythm shape change, loop 4 `O · L: Disinherit · O · D: Read the Will` (corrected after the check: mirror and leader alone do not change the question). Tell: spectacles pushed up and a second codicil in hand; the pews' family crests turned out.

### 5.4 Floors 2–3 (66/33; F2 1 boss action, F3 2; every formation holds a non-fearless group, agreed Q4)

**The Drowned Carillon** (Choir F2, L6; Large construct; armed; carries the Sunken Carillon Hammer; second form guarded).
- **Arena: the Sunken Belfry.** Nave 15×17; flooded aisle band with dry pew islands (RS 8.6 placement caps); choir-loft reserve socket; flood phase sockets A and B.
- **Kit:** Clapper Swing; Cold Ring (burst 1); **Undersong** (signature, units standing in water within 8, Frightened); Fermata (limited, line 4, Staggered). Weak to lightning (the `counterType`); resists cold.
- **Rhythm:** `O · O · D: Undersong`. **66% (e)** flood set; **33% (d)** Choristers.
- **Device: the Bell Rope (Cancel) with the sluice** (one RS 13 feature, two handles). The rope is the primary handle (Cancel). **The sluice is a declared second handle of kind Deny** (after 66%, it drains one flood set, a V9-proven two-state set, once per fight) **(kind stated after the check)**. The sluice is a lever on the arena wall, skinned distinctly from HZ's non-arena Sluice Wheel Cord **(new lead's call)**.
- **Q4 group:** the Choristers are the reserve; the escort carries a non-fearless group at fight start where the budget allows (4.7.4).
- **M3:** Tide High (HZ 6.17).

**The Precentor** (Choir F3, L8–9; Medium; armed; carries the Precentor's Baton).
- **Arena: the Choir Stalls.** Nave; tiered stalls (Low, drawn as floor-level decals at most one step high); the pulpit as the 66% stance post, a floor-level dais with the RC5 cover and base mark; organ face; water bands.
- **Kit:** baton; Arc Note (ranged 6; renamed from "Spark" after the check); **Crescendo** (signature, lightning line 6 with Conduct arcs, Reflex half; Gambit candidate); Hold the Note (limited, burst 2, Staggered). Resists **cold** (never lightning, its `counterType`, BR-20).
- **Rhythm:** `O · O · D: Crescendo`. **66% (b):** stance "Takes the pulpit"; loop becomes `O · D` (shape change). **33% (d):** Storm Organist and Choristers from the loft.
- **Device: the Grounding Chain (Deny, both ways; rewritten after the check as a true Deny piece):** a two-state chain curtain hung across the nave; lowered, it blocks line of effect for lightning lines and Conduct arcs crossing it until the end of the next round, so Crescendo stops at the chain; party lightning across it is grounded too.
- **Clock:** the Organ Swells (M2g, BR-33), else the Organ (M3).

**The Mouthless Librarian** (Silk F2; Medium; unarmed; guards the Vellum Gambeson).
- **Arena: the Reading Hall.** Stacks 15×17; reading tables; the returns desk; web phase sockets in two aisle sets; the inkwell reserve socket.
- **Kit:** Inkblot (ranged 5); Marginalia (adjacent); **"Shh"** (signature, the loudest unit last round in sight, Frightened; a non-verbal hiss in the voice family, never the dry-breath family HZ reserves; 6); Index Card (limited, Restrained). Weak to fire.
- **Rhythm:** `O · L: Index Card · O · D: Shh` (loop 4). **66% (e)** webs; **33% (d)** Inklings (mindless; inert on boss death; so the escort carries the Q4 group).
- **Device: the Returns Bell (Divert):** the ringer becomes the loudest unit (noise 6) until the end of the next round. A natural Herald window. Its sound is a damped single desk-bell tap with its own timbre-table entry, pitched apart from the reserved alarm bell (RC7, TL-43; 6).
- **M3:** the leaning index (Overturn). Fire burns webs both ways (K2).

**The Weaver of the Index** (Silk F3; Large; armed; carries the Index Crossbow).
- **Arena: the Index Well.** Ring 15×17 *(corrected after the check from 17×17, outside FL 6.3's F2–3 size)* around a stacked well; three thread lanes, each anchored beside a lamp sconce, drawn as floor-level web-glyph lanes (threads above head height are dressing only; no shape implies height).
- **Kit:** crossbow bolt (ranged 8); Spinneret (adjacent, minor Poisoned); **Cross-Reference** (signature, Restrained line 6, Reflex; Gambit candidate); See Also (limited, pull 2 along a lane); **Threadwalk** (its one bonus action: move up to its Speed along a thread lane without provoking; counted against ENM 17.5's bonus-action cap, BR-14) **(restated after the check:** the drafts' double-speed trait was outside the kit caps**)**.
- **Rhythm P1:** `O · D: Cross-Reference`. **66% (b):** stance "Drops from the threads", shown as a floor-level stance change (legs spread to legs folded), never a vertical drop; loop → `O · O · L: See Also · D: Cross-Reference`. **33% (e):** webs.
- **Device: the Thread Anchors (Deny):** one handle per anchor (triggers: Interact at its sconce, a ranged attack on the anchor, or AreaEffectTag(fire) on the anchor square); each cuts its lane for the fight.
- No arena clock (told plainly).

**The Strongest Man Who Ever Lived** (Carnival F2; Large; armed; carries the Ever-Lifted Barbell).
- **Arena: the Strongman's Ring.** Ring 15×17; a central 3×3 stage over the dunk tank, drawn as a floor-level dais at most one step high with the RC5 base mark; wagon cover; ≤ 1 Keg ≥ 3 from entry; oil phase sockets; the sideshow-booth reserve socket.
- **Kit:** barbell (1-square push); Showstopper (burst 1); **Hold Aloft** (signature; lift an adjacent Medium-or-smaller target and throw up to 3 to a forecast square, Prone; the banner shows the shape glyph only, never the name, RC7). **Hold Aloft only from the stage** (BK7), with `deviceBait` (4.6): on its Downbeat he steps onto the stage whether or not the tank is armed. Flex and Stamp (limited, burst 2, Staggered). Weak to cold.
- **Rhythm P1 (authored after the check):** `O · O · D: Hold Aloft` (loop 3, the stage beat).
- **66% (e)** oil; **33% (d)** Fire-Eater and Roustabouts.
- **Device: the Dunk Tank (Expose, both ways).** Hit the bullseye (adjacent Interact, or RangedAttackOnPart on the bullseye): everyone on the stage drops in, Prone, with Burning, Smouldering and Oiled ended; the boss is Upended. A throw onto fire is a forecast spike (RT 7.7 guard).

**Madame Ashes, the Final Act** (Carnival F3; Medium; armed; carries the Curtain-Fall Rod; second form guarded).
- **Arena: the Proscenium.** Ring 15×17 *(corrected after the check from 17×17)*; the stage at the far side; fire phase sockets; the **Menagerie Wagon** reserve socket, drawn solid-sided, never a barred cage, with a backlit canvas side panel that shows the silhouettes and their RC3 mind read; the Ca7 poster on a wall slot.
- **Kit:** fire bolt (Rod); smoke flourish; **Curtain of Flame** (signature, fire line 6, Burning; Gambit candidate); Final Bow (limited, burst 2, Frightened).
- **Rhythm:** `O · O · O · D: Curtain of Flame` (loop 4, a slow build) in every band **(corrected after the check:** the drafts' acceleration at 66% made two changes at one (e) entry, against ENM 17.5's one authored change; a loop changes shape only at (a) or (b)**)**. **66% (e)** fire squares; **33% (d)** the wagon opens.
- **Device: the wagon bolt (Divide):** the reserve arrives one round late, used before or after the announcement; once per fight (the drafts' two-round form is withdrawn after the check).
- **Clock:** Pit Breathes (M2g) or the Turning Ring (F3, M3; no Void, no Keg in the ring). Omen "Act III" selects the Act III Guise.

### 5.5 Floor 4 (3 pips; 2 boss actions)

**The Head of the Table** (Supper, L10–11; Medium; armed; carries the Oxblood Brigandine, worn, with a carving-knife at-will). Dropped by Cut 2.
- **Arena: the Head of the Hall.** Long hall 19×21; Tall high-table spine; Low dining tables; plate-warmers (Hot, ≤ 4 contact hazards); braziers as pieces, never the counterplay; the head chair under the **Icicle Chandelier**; the service-door alcove reserve socket (a piece, not a Portal).
- **Kit:** Carving Knife; Cold Shoulder (ranged 6, cold); **Toast** (signature, all who see the head chair within 8, Frightened); **Frozen Toast** (signature, the one hard control, cooldown ≥ 3, `readyAt` 2, one target, 1 turn). **Toasts only from the head chair in the first band** (BK7), with `deviceBait` (4.6): on a Downbeat he takes the chair whether or not the chandelier is armed.
- **Rhythm:** `O · D: Toast · O · D: Frozen Toast` (two Downbeats; first Frozen Toast on turn 4). **66% (b):** stance "Rises from the chair"; the chair lock lifts, so he Toasts from anywhere and the chandelier window is lost, as the stance change intends (he can still be pushed under it); loop `O · D: Toast · D: Frozen Toast` (loop 3) **(authored after the check)**. **33% (d):** Frozen Footmen (mindless; so the escort carries the Q4 group).
- **Device: the Icicle Chandelier** (TL-40 record, read): 3×3, ≤ CapRound cold and piercing, Chilled; Frozen on the centre once per fight; both ways; Upended if the boss is under it.
- Hard-control sources: Frozen Toast + chandelier = 2 (the F4 cap). **No Sommelier** in any formation; Few-and-Strong never adds one.
- **M3:** Courses; Frozen Toast as a Gambit (agreed, Q3; 4.14.3).

**The Pylorus** (Gut; Large; unarmed; guards the Swallowed Splint; `chain(2)` from its mouth alcove beside the Bossgate).
- **Arena: the Antrum.** 17×19 tube bend; rib pillars (Tall); molar faces (contact hazards); bile phase sockets, the 66% set lying across the 6–10 band from the mouth so standing off costs footing; the stomach-fold reserve socket on the far side of that band, so the 33% Tenants arrive among the kiters **(lead's call, after the check:** anti-turret placement**)**. Acid sources ≤ 2: the Pylorus is one; **no Acid-Spitter, and the Tenant acid overlay is off** in this encounter.
- **Kit:** Gnash; Bile Spit (ranged **10**, reaching the far third, anti-kiting); **Swallow** (signature, **line 8** pull to adjacent, Grappled; the mouth reaches the ranged band, lengthened from line 5 after the check); Retch (limited, cone 3, Corroded).
- **Rhythm P1 (authored after the check):** `O · D: Swallow · L: Retch` (loop 3).
- **66% (e)** bile set; **33% (d)** Tenants and the Landlord.
- **Device: the Jaw Prop:** a rib strut inside its reach with two handles and separate charges: the prop (Cancel, once per phase) and the rescue (Release, a second handle: ends a Swallow's Grapple, once per phase) **(split after the check)**.
- **Renditions in M2** (it is the only F4 boss under Cut 2): R1 RS, G1; R2 bile set B (an alternate (e) set) and loop 4 `O · L: Retch · O · D: Swallow`, G2; R3 the Gullet Guise (M3 Void in its (e) set).
- Report `RangedOnlyWinShare` (4.6).
- **M3:** Brim, with the Gullet. Omen Gut stage 2 pre-arms bile set A.

**The Appetite** (Gut; Large; armed; carries the Appetite's Pestle). Dropped by Cut 1a only if not yet built; kept by Cut 2R (agreed, Q1).
- **Arena: the Larder of the World.** Hall 19×21; larder shelving (Tall); a sac Keg (the Gut's ≤ 1); the larder reserve socket.
- **Kit:** pestle; Gorge (necrotic); **Digest** (signature, adjacent, Withered); Mouthful (limited, Grappled).
- **Rhythm P1:** `O · O · D: Digest`. **66% (b):** stance "Still hungry", loop → `O · D: Digest`. **33% (d):** Spore-Priest and Tenants.
- **Device: the Feeding Hatch (Sate):** pay 1 supply; it spends its next Downbeat eating, so that Digest is skipped (Sate works exactly as Cancel, 4.7.1). Attrition both ways (the Gut's Law).

### 5.6 Floor 5 (L12; 3 pips; 2 boss actions; 66/33; no hard control; no damage immunity; the Heart's 6; damage psychic and piercing; counter radiant)

**The Night Governess** (Large, very tall by proportion; armed; carries the Warming Pan, the Memento: "Warming Pan, carried out of the Heart. Still warm." on tone review).
- **Arena: the Turned-Down Bed.** The bed, about 17×19; bedposts as Tall Handholds; bedsheet phase sockets; the **nursery linen press** reserve socket against a wall, exits toward open floor (moved off the bed after the check, so no humanoid silhouette stands in the bed); the Heavy Thing; the waking-light window.
- **Kit:** *There, There* (the pan, psychic; renamed from "Hush" after the check); Lights Out (psychic, ranged); **Tuck In** (signature, needs sight, Restrained 2×2; renamed Smother if the tone review asks; Gambit candidate; VFX a sheet band across legs and torso with head and arms always visible, the bedsheet square glyph, never a full cover); a limited Unfocused hum.
- **Rhythm P1 (authored after the check):** `O · L: hum · O · D: Tuck In` (loop 4).
- **66% (e)** bedsheets (fallback if RT-14 drops them: (a) Tuck In → Lights Out as an Unfocused cone). **33% (d)** Sleepwalkers, clustered.
- **Device: the Lamp Snuffer (Deny)**, plus the Heavy Thing: snuffing the night-lamp darkens the bed lanes until the end of the next round, blocking sight for Tuck In (both ways; 4.13).
- **M3:** the Dreamer Turns Over ("lie still and it passes" applies to her too).

**The Rousing Engine** (Large clockwork; armed; carries the Wind-Down Hammer, the Memento: "Wind-Down Hammer, carried out of the Heart. It has stopped.").
- **Arena: the Mantel Nursery.** The bed variant with the mantel-clock plinth; the same no-silhouette-in-the-bed rule as the Turned-Down Bed.
- **Kit:** hammer (piercing gear-teeth by theme set); gear-shard (piercing, ranged); **Reveille** (signature, Dazed cone 3; Gambit candidate); **Clangour** (limited, psychic burst 2).
- **Rhythm:** `O · L: Clangour · O · D: Reveille` (corrected after the check: Clangour is a limited ability, so it is a Lead beat, not a Downbeat). **66% (b):** stance "Wound up", loop → `O · L: Clangour · D: Reveille` (loop 3). **33% (d):** Insomniacs, clustered.
- **Device: the Mainspring (Cancel)**, ranged trigger on the exposed escapement, plus the Heavy Thing. **M3:** the Alarm Clock made boss (Wind Back and Ring Now; a Herald may ring it; its alarm timbre is a reserved cue).

**F5 and Q4 (new lead's call):** both final bosses' reserves (Sleepwalkers, Insomniacs) are checked by BK3 for fearless status; if either is fearless, the F5 formation carries a non-fearless escort group, so Q4 holds on F5 without a roster change. The escort is the preferred home in any case (4.7.4).

**The two finales differ (lead's call, after the check):** different Device kinds (the Governess's Deny darkness against the Engine's Cancel at the escapement), different loops (a Lead hum before Tuck In; Clangour leading Reveille), and different 66% changes ((e) bedsheets against (b) "Wound up"). Their 33% bands keep RS's (d) text and their loops; the last band is escalated by the waking-light window's visible brightening and the Phase Score's third layer, never by a second change at the (d) entry (ENM 17.5).

**Later, the true ending (agreed, Q5):** a bodiless set piece after the last keeper falls (4.13).

### 5.7 M3 and later
- **The Lullaby** (third final): a song with a body of moths and yarn, never the sleeper or a child-figure; Unfocused and Frightened; a music-box device; the Heavy Thing.
- **The Last Course** (Supper F3) and **a second boss for every single-boss cell** (RS 12): authored on this sheet format, first as Renditions where the rig allows. Requests to the RS owner, no names invented here.
- **The Headsman's Greataxe** waits for an M3 boss (the Ministry of Small Mercies, F1–2).
- **Later:** the Rival Party slot (needs its own arena rules); multi-square bosses; the bodiless true ending (agreed, Q5).

---

## 6. Readability, art, animation and audio (lead's call)

**The arena charter (change BR-38; V17 and playtest gates):**
- **BX1** One clock on the map; Devices, the Rhythm strip and Gambits are not clocks.
- **BX2** Seen before it matters: Devices, reserve sockets, phase sockets and any Stir footprint carry a latent outline from reveal.
- **BX3** One full round of warning for every arena change, reserve and (M3) commit.
- **BX4** Warned or floored holds in every state **(settled, HZ 6.3.2)**.
- **BX5** A boss is never immune or resistant to its own arena clock's type.
- **BX6** (M3) The party always owns a verb on the arena clock.
- **BX7** Safe footing: in every socket and clock state, ≥ 2 × party size walkable non-footprint squares within 3 of each start zone and Arch.
- **BX8** At most 3 telegraph layers at once (due overlay, signature banner, reserve or (e) mark); a 3-second glance test ("what happens at round end?"), run on the map and the plate. (Read, unchanged, by segment 10's combat mode and budgets: ≤ 3 telegraph layers; change XI-15, exploration and information design 4.9.4, 4.9.5.)
- **BX9** No clock escalates with round count.
- **BX10** F1: no harmful arena act at any tier.

**Art (the team is eager):**
- Per boss: one rig; one Arena State dressing per threshold (1 on F1, 2 on F2–5: light, sound bed, Keepsake animation, display only), so every threshold, the last included, changes sound and light **(lead's call, after the check)**; the Device animation with two V9-proven states; per Guise a dressing pass; reserve-socket props with silhouettes.
- Renditions reuse the rig with prop and stance changes; each Rendition's visible tell is listed in 5 (BK13).
- Shape, never hue, distinguishes variants (RC5). Saturated element hue only on squares and attacks (RC4).
- Bosses ≤ 1.5× height within one square, on plinths (RC6); RC3's boss exemption from the face grammar holds. **Prone and Upended poses for bosses are a kneeling or folded collapse inside the boss's square** at every camera rotation, never a full-length fall over adjacent squares **(lead's call, after the check)**.
- **No boss pose or arena shape implies height** (one flat rules level): pulpits, stages and daises are floor-level decals at most one step high with the RC5 cover and base mark; thread lanes and other "high" routes are floor-level glyph lanes with dressing above head height only **(lead's call, after the check)**.
- **Device bodies obey RC2 and TL-43:** no free-standing chair, staff or soldier role prop as a Device body; overhead Devices (the Pendulum, the Icicle Chandelier, the Heavy Thing) hang over non-walkable squares or a wall slot, and their catch squares carry the RC5 Device glyph. An RC2 check runs over all Device bodies **(lead's call, after the check)**.
- The Heart: no body, face or limb shape under the sheets; heave animations run edge to edge only during acts; no humanoid silhouette in or behind any bed element (`FinalBossNoDreamer`).
- **New rules marks, registered in the RC5+ pattern table (lead's call, after the check):** the Rhythm strip's three marks (Off dot, Lead diamond, Down area-shape glyph; 4.3.1); the five phase-type icons (new move, stance, ward, reserve, terrain); "Upended"; "Resolve Broken"; the Gambit "Next turn" mark; and one cross-theme **leash ring** (latent outline style, telegraph tier, shape-patterned) drawn for every `chain(n)` unit, chains and ring-bolts staying as dressing at the anchor only. Each is gated on the greyscale and colour-blind checks and the BX8 glance test.
- **Camera rotation gate (V17 art gate, beside BX8; lead's call, after the check):** every Guise × mirror is captured at all 4 camera rotations at minimum zoom; the boss post, every Device catch square, the reserve socket and every Arch stay visible through cutaway or fade, and an overhead Device fades to an outline when a unit stands beneath it (RC6: nothing hides a unit). The greyscale and colour-blind checks run on the same captures.
- **Art order under graded Cut 1 (new lead's call):** the F1 survivors' 3 Renditions (Jailer, Old Ragged) and the Pylorus's come before the Thing in Cell Zero's and the Master's rigs, so a cut at 1b or 1c never strands Rendition art. "Cell Zero Opens" needs no Thing rig: its chained Lifer reuses the Gaol roster's Lifer rig and is built with the Jailer's Rendition art **(corrected after the check)**.

**Asset manifest (lead's call, after the check; counts *(illustr.)*, totalled against RS 10.4 cost units by the RS owner).** Reuse rule: Guises are built from the theme's RT piece kit; only the Device, the reserve socket and the Rendition tells are bespoke.

| Boss | Rigs | Clips per rig | Guises | Arena States | Device states | Socket props | Wave | Cut step |
|---|---|---|---|---|---|---|---|---|
| Jailer of Hours | 1 | 16 | 3 | 1 | 2 | 1 (+ Cell Zero alcove under 1b) | M2 | — |
| Thing in Cell Zero | 1 | 15 | 3 | 1 | 2 | 0 | M2 | 1b |
| Old Ragged | 1 | 16 (M1 subset 10) | 3 (M1: 1) | 1 | 2 + Kennel Shutter 2 | 0 | M1/M2 | — |
| Master in the Frame | 1 | 15 | 3 | 1 | 2 + Turned Portrait 2 | 1 | M2 | 1c |
| Guest of Honour, Executor | 1 each | 15 | 3 each | 1 each | 2 each | 1 each | M2 (Wake gated) | — |
| Carillon, Precentor, Librarian, Weaver, Strongest Man, Madame Ashes | 1 each | 16–18 | 2 each (+ Act III for Madame Ashes, M3) | 2 each | 2 (Carillon 3) | 1 each | M2 | — |
| Head of the Table | 1 | 17 | 2 | 2 | 2 | 1 | M2 | Cut 2 |
| Pylorus | 1 | 16 | 2 (+ Gullet) | 2 | 3 | 1 | M2 | — |
| Appetite | 1 | 16 | 2 | 2 | 2 | 1 | M2 | 1a (if not built) |
| Night Governess, Rousing Engine | 1 each | 18 | 2 each | 2 each | 2 + the Heavy Thing 2 | 1 each | M2 | — |
| Shared | — | Reaction set (below) | Lord's Gallery (1c only) | — | — | — | M2 | — |

- **Clip list per boss rig** (about 14–18): idle, walk, 2 at-wills, 1–2 signatures, 2–4 limited abilities, boss-action playback, Upended or Prone collapse and recovery, (b) stance change where authored, phase entry, death, reset walk. About 43 interiors in all (6 F1 bosses × 3, 11 × 2, plus Act III, the Gullet and the Lord's Gallery).
- **No paired or synced boss-to-hero clips (lead's call, after the check):** the boss plays a solo clip and the target plays one universal reaction clip from a shared set (lifted, thrown arc, pulled, grappled-held, restrained); displacement is shown by a VFX arc or tether and a position tween. The shared reaction set is one global animation deliverable.
- **Throughput gate (risk 14):** if rig and clip throughput by mid-M2 is below plan, the Wake bosses and second Guises on F2–5 slip before any F1 Rendition does.

**Animation:** phase card ≤ 1.2 s; Upended collapse ≤ 0.8 s; reserve wake ≤ 1 s; Device and leash-ring acts ≤ 1.0 s; grouped escort playback; boss-action playback ≤ 1.5 s each *(illustr.)*. Device idle and act motion joins RC1's closed motion list (BR-38).

**Barks and voice (RC8; lead's call, after the check):** a signature is voiced only as a non-verbal vocal sting under the universal 13.2 banner when its forecast appears, if the RS owner agrees to add the signature banner as a bark trigger (BR-38); otherwise not at all. Resolution plays SFX only; spoken acts (the Eulogy, Read the Will, the Toast, "Shh") are never voiced on resolution. A content test checks that no signature has VO on resolution.

**Audio — the Phase Score (change BR-36; M2 per-boss stems, M3 motifs):** a **per-boss base stem**, built from the theme's palette but distinct from the floor bed, so two bosses of one theme on adjacent floors never sound alike; plus **one added layer per threshold** (1 on F1, 2 on F2–5), added after the sting **(lead's call, after the check)**; a one-bar motif for the next Downbeat, always mirrored by the strip (sound never carries unique information). A playtest blind-ID of the phase from audio alone runs on the F2–5 bosses.

**Theme checks:** the Gaol's bed ticking continues in the arena as bed texture only, never tied to the Rhythm, and the Gaol blind-ID test includes it with the Dream tick; the Master's View Halloo is a voice-family hunting cry, never a horn or a rattle; "Shh" is a voice-family hiss; the Carillon's music line carries no chimes and no struck bronze; no voice, breath or heartbeat attributable to a sleeper in the Heart.

**Per-boss timbre table (lead's call, after the check; in the shape of HZ's RC7 table):**

| Cue | Timbre family | Kept out of |
|---|---|---|
| Bell Rope pulled (Carillon) | Rope creak and water surge; the bell heard only as a muffled thud under water | Struck bronze (the Choir's fallen-bell cue) |
| Clapper Swing, Cold Ring (Carillon) | Dull waterlogged clank | Struck bronze; low chimes (Small Hour Strikes) |
| Returns Bell (Librarian) | Damped single desk-bell tap, its own entry | The reserved alarm bell (RC7, TL-43) |
| Reveille, Clangour (Engine) | Gear grind and spring twang | Any ringing alarm (the Alarm Clock's reserved cue) |
| View Halloo (Master) | Hoarse voice-family hunting cry | Horns (joiners); the Horn Post's wooden rattle |
| Heel Whistle (Old Ragged) | Thin bone whistle, pitched apart | The View Halloo; joiner horns |
| "Shh" (Librarian) | Voice-family hiss | The dry-breath family (the Heart Stir) |
| Heart beds and ambience | Theme palette, no breath-family sample | The dry-breath family, played only at an announced or firing Heart act |

Every boss cue runs through the 85% audio blind-ID gate with its theme's reserved cues.

---

## 7. New mechanics considered

| Mechanic | Source | Verdict |
|---|---|---|
| Rhythm and strip | A, NM | **Adopted** M2 (BR-6) |
| Device contract with Upended | A, catalogue, NM | **Adopted** M2 (BR-16, BR-17) |
| Reserve sockets with Green Room presentation | A, C | **Adopted** M2 (BR-11) |
| AddCap and Few-and-Strong | A, B, RL | **Adopted** M2, one definition (BR-12) |
| OneDueThing and the Dream contract | RL | **Adopted** M2 (BR-10) |
| Playbill, Take Places | NM | **Adopted** M2 (BR-5, BR-4) |
| Renditions (authored list, folded bosses) | A, B, catalogue, NM | **Adopted** M2 F1 / M3 (BR-23, BR-24; agreed, Q1) |
| Launcher F1-boss streak rule | A, NM | **Adopted** M2 (BR-25; agreed, Q1) |
| Phase Score | NM | **Adopted** (BR-36) |
| Signature in Hand | NM | **Adopted** as authoring under IR's rule (BR-27) |
| Gambits / Decrees | A3, RL, NM | **Adopted** M3 gated, with the Frozen Toast exception (BR-32; agreed, Q3) |
| Prompts on boss actions | RL | **Held** as an M3 option gated on `ThreatMix` (BR-41) |
| Keynote binding and Overtaken | C | **M3** with the Stir catalogue (BR-33) |
| Boss Ledger and Grudge | NM | **M3**, display only (BR-35) |
| Sanctum rest rule | critics | **Adopted** M2 (BR-31; agreed, Q2) |
| Floor-wide mid-boss rest ban | critics | **Rejected** (Q2 option B); may return to the client only through 14.2 |
| Bodiless true-ending set piece | lead | **Adopted** Later (BR-30; agreed, Q5) |
| Shorthanded lever | toy, judges | **Rejected** (empty-slot bonus; BR-21) |
| Break / pip strip by Device or Jarred | A, B, RL | **Rejected for M2**; Later only if `ClassBossContribution` asks (told plainly) |
| Jarred cancelling a committed move | RL | **Rejected** (two outs on one move; BN9) |
| Generated arena fill | B | **Rejected** (unprovable offline) |
| Downbeat (boss cues its clock); M2 Dials | C | **Rejected** |
| Enrage, damage cap, legendary actions, lair slot, refilled bars, Ruler reactions | research | **Rejected** (BR-15) |
| Weak-point squares | NM | **Rejected** (no facing; multi-square Later; name collides) |
| A boss that counters learned facts | NM | **Rejected** (facts are information, never negative power) |
| Perilous boss swap; fresh-face Fisher–Yates | NM, research | **Rejected** (erodes the preview and seed comparability) |
| Device as an alternative win line | research | **Rejected** (the Bossgate needs `BossDefeated`) |
| A Dreamer boss with a body | RS 8.11 Later | **Rejected** (agreed, Q5; RS 7.5) |

**Adopted in full, beyond section 4:**

**Boss Ledger and Grudge (change BR-35 ⚑; lead's call; M3).** A codex page per boss records Renditions seen ("?" for unseen), Devices used, phases reached, wins and runs it ended. If a boss killed the hero the last time the profile met it, it next carries a trophy shaped like the fallen hero's class emblem, plays a bark variant on phase entry, and its plate adds "It remembers." It reads only a display snapshot at run start (`bossMemory {bossId: lastOutcome, renditionsSeen[]}` in the profile, the `curiosityMemory` pattern); never draws, stats, Rhythm, Rendition or AI. Playbill badges show "new Rendition" to returning players (FN10).

**Prompts (change BR-41 ⚑; lead's call; M3 option).** Each boss-action option carries one Prompt on the unit whose turn just ended: Strike/`InReach`, Reposition/`Far(n)`, Command/`StruckEscort`, Shed/`Any`, Reposition or Strike/`NearDevice(n)`. Trigger: `ThreatMix` shows boss actions above 35% of boss-side damage at party 4; band `PromptDenialRate` 15–45%. If denial runs high, widen a Prompt; never add a boss action.

---

## 8. Numbers with fixes (toy, not simulator; all illustr.)

The toy (`bossfight.py` and runners, Appendix B) was calibrated so the BASE expert arm meets a median of 6 rounds and target death rates; only deltas between models mean anything. The toy party is weaker than real classes (one heal, simple AI). The toy was not re-run after the client's answers or the new lead's calls (told plainly).

**Deaths per 100 boss arrivals, default party per floor (expert / novice):**

| Model | F1 | F2 | F3 | F4 | F5 |
|---|---|---|---|---|---|
| BASE (approved rules) | 1.2 / 1.0 | 4.2 / 3.6 | 8.4 / 12.9 | 13.7 / 22.9 | 33.7 / 42.7 |
| A | 0.2 / 0.1 | 3.0 / 3.3 | 7.7 / 10.9 | 14.0 / 19.5 | 29.8 / 40.3 |
| B | 0.4 / 0.1 | 3.6 / 3.2 | 7.1 / 10.3 | 15.0 / 22.3 | 31.0 / 41.6 |
| C | 1.7 / 1.2 | 1.4 / 2.0 | 4.3 / 7.7 | 10.1 / 12.8 | 23.1 / 31.1 |
| RL | 0.2 / 0.0 | 1.5 / 1.4 | 1.6 / 4.7 | 4.0 / 9.9 | 12.4 / 23.6 |

**Failures and fixes:**

| Id | Failure | Fix |
|---|---|---|
| BN1 | F1 lowest-legal spike 108–115% with a Strike or Signature boss action | F1 menu {Reposition, Shed, Command} (BR-7); A passes at 69% |
| BN2 | Jailer halberd 1d8+2 max is 31% of the reference hero | 1d6+2 (BK5) |
| BN3 | F1 rows pass but deaths are 12–45 per 100 at authored dice (0.52× needed) | Simulated F1 gate; fight-level row with escort; `floor1BossRounds` = measured p90 (BR-22) |
| BN4 | F1 p90 rounds 10–11 | F1 length target median 4–6, p90 ≤ 8; HP early in the lever order |
| BN5 | `BossMinutes` 6.7–7.2 at party 4 on F4–5 (party turns alone ≈ 4.4 min) | Gate median ≤ 7 min, declared as the floor-clock line (≈ 35% of the floor); grouped playback, UI turn time; never HP (BR-40) |
| BN6 | Drafted F5 band 35–55 infeasible above ≈ 40 | Bands from a boss share 0.3–0.7: F1 ≤ 2.5; F2 3–7; F3 7–15; F4 11–23; F5 27–40 |
| BN7 | Solo/duo 3–10× deadlier on F2–5 | Per-size data only; n ≤ 2 gate; test lower `hpByPartySize[1]`; no Shorthanded |
| BN8 | A final damage immunity adds +10 F5 deaths overall, +14 to +28 when it blanks a party type | No damage immunity on `final` (BR-20) |
| BN9 | Jarred cancelling committed Decrees: 0.05 hits per use at F3 | Nothing voids a commit; denial budget (4.7.3) |
| BN10 | Gambit 60% + Strike boss action = 82% | No boss action on footprint units; Gambit resolves as the turn's action |
| BN11 | Σp² repeat figures ignore R-SEED-6 | True 22% core, 44% Cut 1 |
| BN12 | RL/NM experts below band; novice gap +11/+15 | Retune dice up 15–20% where adopted; novice F4/F5 gap ≤ +10 points |
| BN13 | Arcanist binds F1 at the lowest legal Con (9–24 per 100) | `Floor1ClassSpread` gate; clustered pack; ranged Device triggers |
| BN14 | A's verification ≈ 217% of a 62k pass | Static mirror/rotation proofs; template-local tie-breaks; simulate Rendition × formation × size only |

**Party-size fairness, expert deaths for n = 1 / 2 / 3 / 4:** F3 A 36 / 30 / 9 / 3, RL 29 / 17 / 2 / 0.6; F5 A 70 / 67 / 45 / 31.

**Telegraph actionability, signature hits per use, F3 expert / novice:** BASE 1.04 / 1.67; A 0.88 / 1.56; A + Gambits 0.44 / 0.84; RL 0.05 / 0.22 (soft Jarred 0.38).

**Variety, Monte Carlo of 20,000 players × 10 runs with R-SEED-6:**

| Build | Same boss as last run | Same fight | Same boss 3 in a row | Distinct in 5 runs |
|---|---|---|---|---|
| M2 core (4) | 22.1% | 22.1% | 3.8% | 3.16 |
| Approved Cut 1 (2) | 44.1% | 44.1% | 15.4% | 2.00 |
| Graded 1b (3) | 33.1% | 33.1% | 9.7% | 2.58 |
| Core + Wake (6) | 15.6% | 15.6% | 2.0% | 3.63 |
| Cut 1 + 3 Renditions | 44.2% | 14.5% | 15.4% | 2.00 |
| Cut 1 + launcher rule | 34.7% | 34.7% | 0.1% | 2.00 |
| **Graded 1b + 3 Renditions + launcher (agreed, Q1)** | **26.5%** | **8.7%** | **0.0%** | 2.66 |

**Told plainly after the check:** the "same fight" column assumes that every Rendition changes the question. As first written, Old Ragged's R2 and R3 did not (5.3), so for the boss that survives every cut a streak player would have met one fight three ways; they are re-authored here, and the Monte Carlo is re-run by the simulator counting only Renditions that pass BK2 as distinct fights.

**Cost (RS 10.4 units relative to approved):** M2 core 2.84×; Cut 1 2.45×; Cut 2 2.21×; graded 1a 2.71×; graded 1b 2.58×; Cut 2R 2.34×; core + Wake 3.21×. With F1 bosses recounted solo-only (about 5 units): core 2.65×, Cut 1 2.35×, graded 1b 2.44×. Verification: A 400 enumerations (≈ 217%, falling with BN14's fix), B 288 (156%), C 288 (108%).

---

## 9. Data, validator V17 and simulator (changes BR-39, BR-40)

### 9.1 Records (illustr. C#)

```csharp
record BossRecord : existing (eligibleFloors, themes[], final, shipStatus, hpByPartySize[4], signatureForms) {
  BossSize Size;                    // ≤ Large (BR-18)
  bool Armed; GearFamily[] IssueGear;
  Leash Leash;                      // room | chain(n)
  AbilityId? BonusAction;           // e.g. the Weaver's Threadwalk, inside the ENM 17.5 cap (BR-14)
  BossRhythm[] Rhythms;             // first band authored; later bands per 4.3.1 rule 5 (BR-6)
  BossActionOption[] BossActions;   // ⊆ {Strike, Reposition, Shed, Command}; F1 ⊆ {Reposition, Shed, Command}
  ArenaTemplateId Template;
  RenditionRecord[] Renditions;     // exactly 3 drawable on F1 (M2; agreed, Q1), ≥ 2 elsewhere; [0] = RS text;
                                    // folded ones carry FoldedFromBossId and are drawable only when that boss is cut
  FoldedGuise[] FoldedGuises;       // e.g. the Lord's Gallery (1c)
  NightmareRendition? Nightmare;    // M3
  ArenaDeviceId[] Devices;          // 1 (F5: own + HeavyThing)
  ReserveSocketId? Reserve;
  PostRule? SignaturePost; bool DeviceBait;  // BK7 posts; 4.6
  string[] StanceLines;             // per (b) entry
  AnswerWindow[] Answers;           // Vanguard, Shade, Arcanist, Herald
  FactTemplate[] Facts;             // ≥ 4, mirror-aware
  AbilityId? GambitAbility;         // M3 (agreed, Q3)
  CutStep? CutStep;                 // the drop step only: 1a, 1b, 1c or Cut 2 (agreed, Q1); null if never cut
}
record BossRhythm(int Band, Beat[] Loop, int EntryBeat = 0, int MaxWait = 1);
record Beat(BeatKind Kind /*Off, Lead, Down*/, AbilityId? Ability);
record RenditionRecord(string Id, string PlateName, GuiseId Guise, FormationId Formation,
  bool? Mirror /*null: use the drawn bit*/, int RhythmStart /*first band only*/, bool Hard /*Nightmare tier 1*/,
  BossRhythm[]? RhythmOverride /*per band*/, AbilityOverride[]? Abilities /*swap or promote; signature and limited flags*/,
  PhaseOptionOverride[]? Phases /*keyed by threshold index; RS sign-off*/, ArenaDeviceId[]? DeviceOverride,
  string[]? StanceLines, string Tell /*BK13*/, FactText Fact,
  BossRecordId? FoldedFromBossId, RenditionId? ReplacesRenditionId);
record FoldedGuise(GuiseId Guise, BossRecordId FoldedFromBossId, GuiseId ReplacesGuiseId);
record ArenaTemplate(string Id, ShapeKit Shape, ArchSocketPair Arches /*with takePlacesSquares[]*/, StartZone Start,
  Square[] PartyApproach, Lane[] Lanes, Guise[] Guises, GuiseId SafeGuise);
record Guise(string Id, SizeByBand Sizes /*one size per floor band*/, Layer[] Pieces, Socket[] DeviceSockets /*count checked by V17s (f)*/,
  ReserveSocket? Reserve, PhaseSocket[] Phase, StirSocket? Stir, VoidRegion? Void, Square[] CacheCandidates,
  WallSlot[] Slots, FactText MirrorAwareFact);
record ArenaDevice : TrapRecord(kind = arenaDevice) {
  Handle[] Handles /*1 primary; F2+ may add 1 second handle*/; bool F1Safe; bool BothWays; }
record Handle(DeviceKind Kind /*Cancel, Deny, Divide, Divert, Sate, Expose, Release (second handle only)*/,
  TriggerMode[] Triggers /*Interact, RangedAttackOnPart, AreaEffectTag(tag), ForcedEntry(boss)*/, UsesPer Per);
record ReserveSocket(string Id, Square[] Footprint /*≤ 3×3*/, Square[] Exits, Silhouette Skin /*keeps RC3 mind read*/);
record CutState(CutStep[] Applied, bool AppetiteBuilt);  // stamped into the content version (BR-26)
```

Runtime life state: `{rhythmPointer, pendingSignature?, pendingCancel, deviceCharges, reserveStatus, headlineDelayRounds, liveGambit?}`; floor state `sanctumEngaged` and the party's held per-fight state (4.2.5); profile `recentRuns[].f1BossId` (4.10.3). **Cut 2R as a rule:** Cut 2 never drops a record while `cutState.appetiteBuilt` is true. Config keys *(illustr.)*: `boss.addCapOverParty` 1; `boss.noUntelegraphedSpikePct` 30; `boss.forecastSpikePct` 45; `boss.gambitSpikePct` 60; `boss.gambitDodgeDiscountPermille` 500; `boss.doorwayDwellTurns` 1; `boss.f1ReadyAtMin` 2; `boss.variantDrawsPerRecord` 4; `boss.midBossRestAlarmPct` 25; `boss.smallPartyDeathRatio` 1.5; `boss.f5SmallPartyDeathCap` 45; `ui.turnSecondsParty` and the playback times of 6 (for `BossMinutes`). **Client values, not tuning (agreed, Q1; new lead's call):** `launcher.f1BossMemory` 2; `boss.f1RenditionCount` 3; the cut-step order.

### 9.2 V17 `BossRooms` (change BR-39)

**V17s (static):**
- (a) Kit, immunity and condition caps per Rendition's **effective kit**; 1–2 signatures; every signature is a Downbeat; `RhythmMatchesCooldown`; entry beat Off; legal `rhythmStart` (4.3.1 rule 6); tempo variety (4.3.2); every Guise and formation referenced by a Rendition.
- (b) `NoUntelegraphedSpike`; forecast cap; crit-inclusive spike budget per party size; two-boss-action focus rule.
- (c) F1: menu, `readyAt` ≥ 2, Device non-harming, `Floor1BossMindReachable`, `F1EscortBreakable` with reserves, solo minion cap, one Medium boss per two-boss theme of the shipped roster (4.7.6), formation outside the aura, **exactly 3 Renditions per F1 boss** (agreed, Q1), each beyond R1 passing BK2 on the effective-kit fields and BK4 (Old Ragged exempt) and carrying a tell (BK13); F1 loops ≥ 3 with the first Downbeat ≥ 2 beats after `rhythmStart`; F1 Cancel Devices one charge per fight.
- (d) Size ≤ Large; carriage consistent with `Armed`, issue gear and IR (V14 (h) cross-check); chassis-only.
- (e) Last-threshold rule; (b) stance and loop change; a loop changes shape only at an (a) or (b) entry.
- (f) Exactly one Device (F5: two), matching the Guise's `DeviceSockets` count; class-blind, with a ranged trigger; one kind per handle, at most one second handle and none on F1; never a brazier; RC2 body check.
- (g) One reserve group; AddCap feasible at sizes 1–4; a (d) reserve never trimmed to zero at any allowed party size; Few-and-Strong picks only types of equal fearless and mindless status; hard-control and acid sources after Few-and-Strong.
- (h) `final`: no damage immunity; no resistance to `counterType`; BX5.
- (i) Answer windows for 4 classes; **F2–5 `BossCourtBreakable`:** every formation at every party size, after AddCap, holds ≥ 1 non-fearless escort or reserve group as defined in 4.7.4 (agreed, Q4); reserve-only cases listed; reserve silhouettes keep their RC3 mind read.
- (j) ≥ 4 facts with mirror-aware text; `FinalBossNoDreamer` as defined in 4.13 (`dreamerDepiction` on every Heart asset, no silhouette in a bed element, no breath-family sample in a Heart bed or ambience stem, banned-term list); frozen-name collision check across the 27 docs.
- (k) Guard rows enumerated per Rendition × formation × size, with the patrol arm; `RenditionParity` declared; `RenditionStatInert`, `RenditionLootInert`.
- (l) Gambits (M3; agreed, Q3): never on an F1-eligible record; never hard control except the Frozen Toast record; the authored footprint shape and the no-pin rule present (the escape guarantee itself is a runtime scorer constraint, tested in V17t).
- (m) Cut consistency (agreed, Q1), evaluated against the content's `cutState`: a record's `CutStep` matches 4.10.4; a folded Rendition exists for 1b (with `ReplacesRenditionId`) and a folded Guise for 1c; under the applied cuts every surviving F1 boss has exactly 3 drawable Renditions; Cut 2 drops no record while `appetiteBuilt` is true.

**V17g (generation, after V9, V10, V13g):** a proven stamp; start zones ≥ 9; ≤ 1 clock and ≤ 1 Void; lanes clear; phase sockets in both states; Device and reserve exits reachable; cache square legal; LG4 Portals only; no Curiosity, Windfall, trap or loot clock; Interludes keep `AntechamberScreened`; BX7 safe footing; `takePlacesSquares[]` within path distance 2 of each Arch; the OneDueThing enumeration of 4.3.4 (crossing rounds × Stir offset × Divide); `BossVariantKey` draw count fixed.

**V17t (tests):** determinism replay; mirror-equivalence; `BossVariantKey` stable across door swaps, cuts, unlocks and the Last Hollow choice; banner equals the AI's decision; event order (Catch-up before `ArenaEngaged` from M2; no guard in M1; boss group joins an active combat); `endsOn: arenaSeal` processed at every arena combat start while `sanctumEngaged`; reserves never act on arrival; the Rhythm's pending, carry and reset rules (a Downbeat after a reset lands only after one full banner round); Cancel skip and carry; Upended through a save and a Prone immunity, and a second Expose in a phase; `deviceBait` (every BK7 signature legal on its Downbeat with the Device armed); the boss-action forced spend; pending reserves cancelled on boss death; Shed timing; reset keeps the pointer and charges; **the Sanctum rest rule (no rest in the Forecourt, arena or Stairhead from `ArenaEngaged` to `BossDefeated`; rests elsewhere legal; pre-engagement Forecourt rest legal)** (agreed, Q2); the one-fight rule for party per-fight resources; the launcher F1-boss rule bypassed for shared, daily and simulator seeds, which never write `recentRuns[].f1BossId` (agreed, Q1); Gambit order, resolve under control and escape on replays (M3); `LootDoorInvariant` unchanged.

### 9.3 Reports (change BR-40; targets illustr.)
Definitions made computable after the check **(lead's call, after the check)**: an **arrival** is an `ArenaEngaged`; a **band step** is the width of the floor's death band below.
- `BossDeathShare` per floor, party size, class, and "companions Downed at death", per 100 arrivals: F1 ≤ 2.5 (gate); F2 3–7; F3 7–15; F4 11–23; F5 27–40. The F1 gate applies **per Rendition** (a hard gate) and to the draw-weighted aggregate. Small parties: on F2–5, n ≤ 2 never below n = 4, and ≤ 1.5 × n = 4; F5 n ≤ 2 ≤ 45 (4.8.2). The F1 solo boss is reported separately (DH 21.5).
- `BossRoundsByFloor`: median 5–7, p90 ≤ 9; F1 median 4–6, p90 ≤ 8.
- `BossMinutes` = Σ party turns × the configured UI turn time + boss, escort and reserve playback at section 6's times + card times, constants in config: median ≤ 7 min at party 4 on F4–5; p75 reported.
- `DeviceUseRate` = fights with ≥ 1 Device use ÷ fights reaching round 2 (band 30–85%), reported with HP saved.
- `ThreatMix`: boss-side damage by source {boss at-will, boss limited, signature, boss action, escort, reserve, Device both ways, Stir}.
- `MidBossRestRate` = boss wins with ≥ 1 `RestTaken` while `sanctumEngaged` ÷ boss wins, per floor (alarm 25%, 14.2); `RestBetweenPhasesRate` = its subset with `bossPhaseBand` > 0; `ThresholdDipRate` = boss resets within 2 rounds after a threshold crossing ÷ crossings.
- `SignatureLandRate`, `DownbeatWaited`, `RangedOnlyWinShare` (every `chain(n)` or post-bound boss), `HeroAloneRounds`, `ReserveFirstTurnSurvival`, phase-skip rate, retreat-reset rate, `BossCheeseRate` (wins with ≥ 3 resets; doorway kills).
- `ClassBossContribution`: the clear rate of the fixed arrival state with the hero of class X and the standard companion set, spread ≤ 10 points across classes, beside `ClassThemeSpread`; the Herald row also reports **fear-target availability** (the share of Herald turns with a non-fearless hostile in range), by round, on F2–5 (Q4).
- `BossRepeatRate` and distinct bosses by run 10, measured by the **launcher emulation** (4.10.3), never the fight simulator; same-fight repeat (counting only BK2-passing Renditions as distinct); `RenditionParity`; `PhaseReadTime` (playtest); `ArenaStampFallback`.
- `GambitDodgeRate`, `GambitStayRate` (M3; Q3 gates).
- Read with their existing meanings: `BossFightJoinRate`, `BossDamageFromOutsideLeash`, `PreBossRestSafety`, `PostBossBacktrack`, `TeeteringUptimeOnBosses`, `ViaticumBossShare`, `BossParity`, potions per boss fight, CN 19.3's scroll-and-bomb share, HR 7.16's fear-immune F1 boss and escort-aura-answered-solo rows.

### 9.4 Simulator arms
Boss arm (fixed arrival states per class × size × floor × Rendition, both Con values), Device never used versus used once versus used freely, with and without the patrol joiner, mid-boss rest arm (rests outside the Sanctum only, Q2), threshold-dip arm, Viaticum tool arm (F5), Renditions on/off, each cut state (1a, 1b, Cut 2, 1c), Gambits on/off (M3), novice arm. The launcher rule on/off is measured by the separate launcher emulation (4.10.3), not by a simulator arm.

**The segment-9 floor-clock line (beside TL-38, EP-33, LP-10, EV-23):** levers in order: AddCap (party + 0 at party 4 on F4–5, never trimming the Q4 group), grouped escort playback, reserve size, phase option (c) → (a), the arena Stir moved to M3; never boss HP first.

---

## 10. Staging by wave

| Wave | Contents |
|---|---|
| **M1** | Old Ragged's Den template (1 Guise), root-columns as the line-of-sight Deny, the signature cache and "Guards:" line, `ArenaEngaged` (no Catch-up guard in M1, because the Catch-up is M2), V17 subset; the M1 kit unchanged |
| **M2** | All 15 boss templates (+2 Wake if gated; the Wake's gate prioritised, agreed Q1) with Guises and formations; Rhythm; the boss-action bundle; spike budget; phase rules; OneDueThing; reserve sockets; AddCap; Devices and Upended; sizes; answer windows with the F2–5 non-fearless group (agreed, Q4); immunity rules; F1 package and gate; **3 Renditions per F1 boss** and the Pylorus's Renditions (agreed, Q1); folded Renditions for any applied cut; the launcher F1-boss streak rule (agreed, Q1); the Sanctum rest rule (agreed, Q2); Playbill; Take Places; carriage; F-BOSS; the Heavy Thing and final-boss Devices; Phase Score (per-boss stems, a layer per threshold); the asset manifest and camera rotation gate; V17; reports; the launcher emulation; floor-clock line |
| **M2g** | The Organ Swells and Pit Breathes hosted in the Precentor's and Madame Ashes's arenas |
| **M3** | The arena Stir layer with Keynote binding and Overtaken; the Master's Landmark tick; **Gambits, gated, with the Frozen Toast exception** (agreed, Q3); Renditions with phase alternates for every boss; Nightmare Renditions, Omens, Perilous one tier up; Remnants; the Lullaby, the Last Course, second bosses; Boss Ledger and Grudge; Prompts (option); first-sighting phase cards; condition spread on F2–5 |
| **M4** | The Waking City final on this sheet format |
| **Later** | Multi-square bosses; the Rival Party slot; **the bodiless true-ending set piece** (agreed, Q5); Gambits if their M3 gates fail (agreed, Q3); an F5 party-side escalation only if p90 rounds exceed 9; a Device pip strip only if `ClassBossContribution` asks |

**Cut lines (agreed, Q1):** 1a (the Appetite, if not built) → 1b (the Thing in Cell Zero, folded) → Cut 2 with Cut 2R (the Supper) → 1c (the Master, folded; client only).

---

## 11. What segment 9 owns, and hooks handed on

| Owner | What |
|---|---|
| **Segment 9** | Arena templates, Guises, formations and interiors; Devices (as TrapRecord instances); reserve sockets; Rhythms; boss-action options; Renditions, folded Renditions and `BossVariantKey`; boss sizes, armed flags and issue gear; per-boss kits where RS is silent; F-BOSS fact templates; the Heavy Thing; the Sanctum rest rule's content; V17; the reports in 9.3 |
| **RS owner** | The graded cut lines and Cut 2R in RS 12 (BR-26); the launcher rule in R-SEED-6 and the wave row (BR-25); the RS 8.11 Later line (BR-30); the RS 13 rename (BR-16); RS sign-off on Rendition phase and ability alternates (BR-23, BR-24) and the Lifer reading (BR-24); RC1, RC5+ and RC8 rows (BR-38); the Wake's gate as a priority (Q1) |
| **Enemies owner** | BR-3, BR-4 (joining an active combat), BR-6, BR-7, BR-9, BR-11, BR-12, BR-14 leash data (including the folded chained Lifer) and the Weaver's Threadwalk, BR-17, BR-20, BR-21, BR-22, BR-32; FL-21(a)/(e); HX-2 Waver |
| **Herald owner** | BR-19's narrowing of HR-21 (agreed, Q4) |
| **ABL owner** | BR-6 reading; BR-31's rest condition (agreed, Q2) and the one-fight rule for party cooldowns |
| **HZ owner** | BR-6 strip not a clock; BR-10 arena round-end step; BR-33; BR-32 reading; HZ-25 arena reading; the tone line; BR-30 breath family kept as the Heart Stir cue |
| **EV and conditions owners** | BR-3 (including `endsOn: arenaSeal` at every arena combat start while `sanctumEngaged`) |
| **IR owner** | BR-27 |
| **CMP owner** | BR-21 |
| **DH owner** | BR-22, BR-31 (rest condition; once-per-fight Medicine under the one-fight rule) |
| **FL owner** | BR-1 (`arenaCell` sized to the authored arena), BR-31 (Forecourt restability), FL-21(a) |
| **EP owner** | BR-11 readings; BR-19 (EP 6.14 formations, `answers{}`) |
| **Main design owner** | BR-2 (MAIN 6.3 sentence), BR-3 and BR-4 (MAIN 4.3, read), BR-25 (`recentRuns[].f1BossId`), BR-35 |
| **Segment 10** | 4.16. **Answered by the exploration and information design (segment 10, approved 2026-11-23; XI-11, XI-19; its 4.4.4, 4.9.8, 5):** the Behind-you line on the arena-entry confirm; the card-queue slots; every 4.16 and 4.17 surface placed in its catalogue |
| **Segments 11–12** | 4.17 |

**Hook answers:**

| Hook | Answer |
|---|---|
| FL 13, FL-21(a), (e) | 4.1, 4.6 narrow leash, BR-13 |
| RS 13, RS 9.3 | 4.7, 4.13 |
| RS 19.2 / EV-44 | Q1 (agreed): graded Cut 1 |
| RT 13 / 6.11 | 4.1; one authored terrain move per boss in 5 |
| HZ 13 / 6.17 | 4.14.1 rows; `noDrop` read; Mood readings |
| TL 13 / TL-40 | `arenaDevice` is not a trap (reading for TL 6.20); the Bossgate never by Wax Key **(settled)** |
| EP 13 / 6.14 | 4.5 |
| LE / LP 4.13 | 4.11; "lair = arena" confirmed; no Windfall in arenas |
| IR 4.13 / IR-12 | BR-27; Spine-Binder `armed` and Butler-at-Arms `promotable` supported; the surcoat supported; the Headsman's Greataxe owed to an M3 boss |
| EV 4.13 | `ArenaEngaged`; no Grace exceptions; 3 variables; `bossQueue`; the Ca7 poster on a Carnival wall slot (a Glimpse is a wall decal, not a Curiosity; RS 6.6 holds); Coat Hooks; F1 boss variety (agreed, Q1) |
| CMP 7.9 | Rows per size, F1 [2..4] by formula |
| Class docs, DH, CN | 4.7.4, 9.3 |

---

## 12. Binding requirements and how they are met

| Brief item | Met by |
|---|---|
| Arena generation within HZ/RT caps, Portals, seal, Forecourt and Stairhead | 4.1, 4.2 |
| Phases, triggers, rhythms and signatures under the Dream step and countdown rules | 4.3, 4.4, 4.14.3 |
| Escorts and adds | 4.5 |
| Boss AI within one-step limits | 4.6 |
| Enrage or not | BR-15: no |
| Boss counterplay left by segment 5 | 4.7 |
| Per-boss rhythms for every boss and the F5 pool | 5 |
| Variety across runs; the Cut 1 question; F1 variety priority | 4.10 (agreed, Q1) |
| Signatures, carriage, Memento | 4.11 |
| Reward card and Viaticum | 4.11.2 |
| F-BOSS and the elite intel line | 4.12 |
| Party-size scaling | 4.8.2 |
| Floor-1 boss safety (DH 17.1 read with DH-26 and DH 21.1) | 4.9 |
| The final boss and the Dreamer premise | 4.13 (agreed, Q5) |
| Omens and Nightmare (M3) | 4.14.2 |
| Determinism, data, V17, reports | 4.15, 9 |
| Hooks to segments 10–12 | 4.16, 4.17, 11 |
| Mark numbers, waves and approved rules touched | throughout; 13 |
| BR-n and V17 verified unused | header |
| The rest loophole (critics) | 4.2.5 (agreed, Q2) |
| Herald target on F2–5 (critics) | 4.7.4 (agreed, Q4) |

---

## 13. Changes to approved documents

Applied on 2026-11-22, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; as in the LE, TL, EP, IR, LP and EV precedents, the Flag column also marks rows that only *read* such text without changing it, so that every touch of it is visible **(new lead's call:** this adds read flags to BR-1, BR-8, BR-10, BR-15, BR-18, BR-28, BR-29, BR-38 and BR-40, which the proposal's register did not flag; **after the check** BR-14, BR-17, BR-24 and BR-36 are flagged too, and BR-2, BR-3, BR-4, BR-10, BR-25 and BR-31 gain the decided, agreed or settled text they were found to touch**)**. **The client's approval of 2026-11-22 covers the flagged rows** (with Q1–Q5 where a row names one). Told-plainly rows were told in 14.1 and confirmed by the client on 2026-11-22. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; each goes to its owner for confirmation):**
- The Rhythm strip is not a clock under HZ 6.2.1: it sits on the plate and counts the boss's own turns (HZ owner).
- A Rhythm is a preference order, not a wind-up, under ABL 5 (ABL owner).
- Reserve sockets keep "no off-map reinforcements" (EP 6.12) and `ArrivalThroughPortal` (EP-16): the units are already in the room (EP owner).
- An `arenaDevice` is not a trap under TL 6.20 (TL owner).
- RS 8.3's Gaol boss line "escorts of Turnkeys and Inmates" admits the Lifer as Cell Zero Opens' (d) reserve (RS owner; BR-24, after the check).
- A required second form on a universal chassis satisfies IR's "≥ 1 universal-chassis signature per theme" for the Kennel under Cut 1c (IR owner; BR-27, after the check).
- A party unit on an arena square counts as noticed by the boss's group, so the fight starts by noticing under MAIN 4.3 (main design owner; flagged in BR-3 and BR-4).
- A Gambit footprint is not a clock under HZ 6.2.1 (HZ owner; agreed, Q3).
- RS 12's M1 "No terrain" admits Old Ragged's architectural root-columns (RS owner).
- Two boss actions may not target one unit in a round, a reading of ENM 6.4 and 11.1 focus spread (enemies owner).
- "Party units" in A-3 are non-Downed party units at round start (enemies owner).
- F1 HP rows [2..4] are filled by formula under CMP-7's content test (CMP owner).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| BR-1 | ENM 17.5 arena (read); FL 6.3, 6.11 (read; `arenaCell` sized to contain the authored arena, an FL hook); RT 6.11; HZ 6.17; VP7 `BossLaneClear` extended to contact hazards near lanes | Arena template grammar and Guises; one authored size per (Guise, band); M1 root-columns reading (4.1) | ⚑ **Settled** (ENM 17.5 arena, read) | Lead's call; FL, ROOM owners confirm | M1/M2 |
| **BR-2** | FL 13 hook; **MAIN 6.3 (decided)**: one sentence added, "a per-record `boss9` keyed child of the route seed, 4 fixed draws, never consumed on the route stream", as in the FL-2, TR-17, TL-3, EP-1, LE-14 and EV-21 precedents; R-SEED-3 (read) | Draw-free stamp; offline proof; template-local tie-breaks; generated fill rejected; the `boss9` keyed child (4.1.3, 4.10.1) | ⚑ **Decided** (MAIN 6.3, a sentence added) | Lead's call; told plainly; main design owner, segment 11 | M2 |
| **BR-3** | **EV 4.4.3, EV-11 (agreed, events Q3)**; conditions C5 Graces paragraph and the pending EV-11 `endsOn` row; **RS 9.4 (agreed, read)**; **MAIN 4.3 "combat starts in place when enemies notice the party" (decided, read)**; ENM 12.2, 12.4 item 4 (read) | `ArenaEngaged` defines the arena seal; once per floor; arena entry as an automatic notice; F1 Catch-up guard from M2 only; joining an active combat; `endsOn: arenaSeal` processed at every arena combat start while `sanctumEngaged`; `arenaSeal` alias; no Grace exceptions; entry confirm; starts the Sanctum rest rule (4.2.1) | ⚑ **Agreed** (EV-11, RS 9.4); ⚑ **Decided** (MAIN 4.3, read) | Lead's call; EV and conditions owners apply; main design and enemies owners confirm the reading | M1/M2 |
| **BR-4** | ENM 12.2 and the combat-start order; **RS 9.4 gate (agreed, read)**; FL `AntechamberScreened` (read); **MAIN 4.3 (decided, read)**: Take Places is the placement step of the combat that the arena-entry notice starts | Take Places (4.2.2) | ⚑ **Agreed** (RS 9.4, read); ⚑ **Decided** (MAIN 4.3, read) | Lead's call; enemies owner (request); main design owner confirms the reading | M2 |
| **BR-5** | **ENM 13.2 "from the boss's first turn" (agreed)** | The Playbill lists signatures from the Stratum Card (4.12) | ⚑ **Agreed** (ENM 13.2) | Lead's call; enemies owner applies | M2 |
| **BR-6** | **ENM 11.1 (settled scorer)**, 17.3 Boss profile, **17.5 A-4 life-state list (settled)**; **ABL 5 (settled deferral, reading)**; HZ 6.2.1 reading | Rhythm; strip; pointer in life state; fallback: cooldown pips (4.3.1) | ⚑ **Settled** (ENM 11.1, A-4, ABL 5 read) | Lead's call; enemies (request), ABL and HZ owners confirm readings | M2 |
| **BR-7** | **ENM 17.5 boss actions: menu, timing, Shed (settled), "Hampered never blocks boss actions" (settled)**; A-3 | Boss-action bundle (4.8.1) | ⚑ **Settled** (Shed, Hampered clause) | Lead's call; told plainly; enemies owner (request) | M2 |
| BR-8 | ENM 6.4 rows (read, rows added; F3–5 reading); ENM 11.1 focus spread; DH-26 (read) | `NoUntelegraphedSpike`; forecast cap; crit-inclusive spike budget; two-action focus reading (4.3.3) | ⚑ **Settled** (DH-26, read) | Lead's call; enemies owner applies | M2 |
| **BR-9** | **ENM 17.5 phases (settled)**, 13.1 marks; RC7, RC8 (read) | Last-threshold rule, (b) visibility, type icons, phase card, threshold preview, HP-only triggers (4.4) | ⚑ **Settled** (ENM 17.5 phases) | Lead's call; told plainly; enemies owner (request) | M2 |
| **BR-10** | ENM 17.5 (d)/(e) (timing made exact); **HZ 6.3 and HZ-12 (agreed, HZ Q1)**: "the round-end Dream step also runs in boss arenas for phase headlines, independent of the Stir gates" | OneDueThing; the newer headline slips; a 2-round delay cap; exact (d)/(e) timing on the arena round-end step; the enumeration proof (4.3.4) | ⚑ **Agreed** (HZ 6.3, HZ-12) | Lead's call; enemies owner applies; HZ owner (request) | M2 |
| **BR-11** | **ENM 17.5 (d) (settled)**, 14.2; EP 6.12, EP-16 (readings); FL LG4 (read) | Reserve sockets; never act on arrival; delay once; arrival-step immunity (4.5.2) | ⚑ **Settled** (ENM 17.5 (d)) | Lead's call; told plainly; enemies, EP owners (request) | M2 |
| **BR-12** | **ENM 17.5 (d) "excluded from the cluster and joining caps" (settled)**; 17.4 Boss Court; 10.3; 7.1; 9.4 | AddCap; Few-and-Strong; never trims the Q4 group (4.5.3) | ⚑ **Settled** (ENM 17.5 (d)) | Lead's call; enemies owner (request) | M2 |
| BR-13 | FL-21(e) proposal; ENM 12.4 (read) | Patrol joiner tuning; never the Q4 group (4.5.4) | — | Lead's call; enemies owner rules (request) | M2 |
| **BR-14** | ENM 11.1, 17.3, **17.5 A-4 (settled)**; ENM 17.5 kit caps (read: the Weaver's Threadwalk counted as its one bonus action); FL-21(a) request; replaces A's `fixedInPlace` (no RT 6.5.3 exception) | AI weights; `deviceBait`; doorway band; `chain(n)` leash data (bosses and the folded chained Lifer); reset with the pointer, Device charges and Rendition in life state (4.6) | ⚑ **Settled** (A-4) | Lead's call; told plainly; enemies owner (request) | M2 |
| BR-15 | **ENM 17.5 tuning (settled, read)** | No enrage, no damage cap (0.1, 7) | ⚑ **Settled** (read) | Lead's call; told plainly | M2 |
| **BR-16** | **RS 13 hook list (agreed, RS Q4)**; HZ 6.17 Master row; TL-40; TL 6.20 reading; RT 6.11 braziers (read) | Device contract; every boss; kennel gate renamed the pen bar (4.7.1) | ⚑ **Agreed** (RS 13) | Lead's call; RS, HZ, TL owners apply | M1/M2 |
| **BR-17** | **ENM 17.5 boss actions, "Hampered never blocks boss actions" (settled)** (within BR-7) | Upended; its save and Prone-immunity rule (4.7.2) | ⚑ **Settled** (ENM 17.5 boss actions, Hampered clause) | Lead's call; enemies owner (request) | M2 |
| BR-18 | ENM 5.1 `size`, **A-10 (settled, read)**; RS 8.4, 8.11 prose (display) | Size ≤ Large; Medium F1 boss per two-boss theme (4.7.6) | ⚑ **Settled** (A-10, read) | Lead's call; told plainly; VG owner confirms verbs | M2 |
| **BR-19** | **HR-21 "escort and reserve groups are not constrained" (agreed 2026-11-20, HR Q3)**; **RT-9 (agreed, read)**; EP 6.16 `answers{}` extended; EP 6.14 "Boss Court escorts use Retinue" → segment 9's `ArenaFormation`s, Retinue one allowed shape on F2–5 (request to the EP owner, after the check) | Answer windows; F1 formation outside the aura; Resolve Broken beat; **every F2–5 Boss Court holds ≥ 1 non-fearless group** (4.7.4) | ⚑ **Agreed** (HR-21, narrowed; RT-9, read); **Q4** | **Client (Q4, agreed 2026-11-22)**, with this document; Herald, enemies, EP owners apply | M2 |
| **BR-20** | **ENM 17.5 immunities (settled)** | No damage immunity on `final` and F1; no resistance to `counterType`; BX5 (4.7.5) | ⚑ **Settled** | Lead's call; enemies owner (request) | M2 |
| **BR-21** | **ENM 17.5 A-3 count**; **CMP-7 content test (companions 7.9, agreed)** | "Party units" non-Downed; Shorthanded rejected; F1 rows [2..4] by formula (4.8.2) | ⚑ **Agreed** (CMP-7) | Lead's call; told plainly; enemies, CMP owners (request) | M2 |
| **BR-22** | ENM 9.3; **ENM 17.5 "M1 boss" (settled kit)**; RS 8.4; DH 21.1 step 6; **DH-26 (agreed, read)**; **HZ-25 / 6.19 (agreed, HZ Q4)** | F1 package: `readyAt` ≥ 2; Old Ragged in M2; fight-level row; `floor1BossRounds` = p90; signature share; F1 gate; lever order; F1 arena never a Nightmare package room (4.9) | ⚑ **Settled** (M1 kit); ⚑ **Agreed** (DH-26 read, HZ-25) | Lead's call; enemies, DH, HZ owners (request) | M1/M2 |
| **BR-23** | **R-SEED-3 (agreed)** (adds a draw-free derivation); **RS 8.3–8.11 text for alternates (agreed RS Q4 catalogue)** | Renditions; 3 per F1 boss in M2; `BossVariantKey` per-record hash; folded Renditions count toward 3 (4.10.1) | ⚑ **Agreed** (R-SEED-3, RS 8.x); **Q1** | **Client (Q1, agreed 2026-11-22)** for the count; lead's call otherwise; RS owner signs off alternates | M2/M3 |
| **BR-24** | **RS 8.3, 8.4 catalogue text (agreed, RS Q4)**; RS 8.3's boss escort line "escorts of Turnkeys and Inmates" read to admit the Lifer as Cell Zero Opens' (d) reserve only (no roster change; V1 `ThemeBudget` unaffected) | Folded bosses: Cell Zero Opens with a chained Lifer (1b); Lord's Gallery (1c) (4.10.2) | ⚑ **Agreed** (RS 8.3, 8.4); **Q1** | **Client (Q1, agreed 2026-11-22)**; RS owner applies | M2 conditional |
| **BR-25** | **R-SEED-6 (agreed)**, its exhaustion tie-break gaining the boss rule; **RS 12 wave table "profile-side anti-streak" M3 → M2 (agreed RS Q4)**; EV-20 (shared budget); **MAIN 6.6 profile save (decided)**: `recentRuns[].f1BossId` added to EV-19's ring | Launcher F1-boss streak rule, memory 2; the profile field; the launcher emulation (4.10.3) | ⚑ **Agreed** (R-SEED-6, RS 12); ⚑ **Decided** (MAIN 6.6); **Q1** | **Client (Q1, agreed 2026-11-22)**; RS owner applies | M2 |
| **BR-26** | **RS 12 cut lines and the Cut 2 definition (agreed, RS Q4)**; RS 12 "How a cut is applied"; RS 10.4; RS 19.2; EV-44 | Graded Cut 1 (1a conditional, 1b folded, 1c client-only below Cut 2); Cut 2R; `cutState` stamped into the content version (4.10.4, 9.1) | ⚑ **Agreed** (RS 12); **Q1** | **Client (Q1, agreed 2026-11-22)**; RS owner applies | M2 |
| **BR-27** | IR 4.13 (read), IR 5.2 Wind-Down Hammer second-form row, IR 5.3 Heart rows; IR "≥ 1 universal-chassis signature per theme (M2+)" (read: a required second form on a universal chassis satisfies it under 1c); ENM 15.2 (read); LP 4.4, 4.10 | Armed flags and issue gear; chassis only; theme damage type; no F5 second form; a cut boss's second form not owed; the Kennel's universal-chassis reading (4.11.1) | ⚑ **Agreed** (IR 5.2 row) | Lead's call; told plainly; IR owner applies | M2 |
| BR-28 | **LE-3, LE-5, LE 6.11, LE-19 (agreed, read)** | Card, Viaticum, Memento interplay (4.11.2) | ⚑ **Agreed** (read) | Lead's call | M2 |
| BR-29 | EV 4.4.1–4.4.2, 4.13; **LE-18 (agreed, read)** | F-BOSS variables and queue order (4.12) | ⚑ **Agreed** (LE-18, read) | Lead's call; EV owner applies | M2 |
| **BR-30** | **RS 7.5 (agreed RS Q3, read)**; **RS 8.11 Later line**; RS 9.3; HZ 6.17 Governess row (tone); HZ 7.10 (read); HZ RC7 timbre table (read: the dry-breath family stays the Heart Stir cue) | Keepers not sleeper; `FinalBossNoDreamer` defined in data; no sleeper sound; the Heavy Thing (piercing); final-boss Devices of different kinds; tone flag; **RS 8.11's Later line replaced by the bodiless, display-only set piece, victory recorded at `BossDefeated`** (4.13) | ⚑ **Agreed** (RS 7.5, read); **Q5** | **Client (Q5, agreed 2026-11-22)** for the Later line; lead's call otherwise; RS, HZ owners apply | M2 / Later |
| **BR-31** | **FL 6.11 Forecourt restability (client reading 2026-11-21)**; **ABL 15.1 (settled)**: a rest condition added, "no short rest while `sanctumEngaged` is set, the boss lives and any party unit stands on a Sanctum square"; DH 11.3 (the same condition); **ABL cooldowns reset at combat end (settled)** and **DH once-per-fight Medicine (settled)**: not reset by a combat end in the Sanctum while the boss lives (the one-fight rule, after the check); **ENM 17.5 A-4 (settled, read)**; MAIN 4.3 combat end (decided, read, untouched) | Sanctum rest rule as a location ban; the one-fight rule; `RestTaken`, `ThresholdDipRate` (4.2.5) | ⚑ **Agreed** (FL reading); ⚑ **Settled** (ABL 15.1, ABL cooldown reset, DH Medicine, A-4 read); **Q2** for the rest rule | **Client (Q2, agreed 2026-11-22)** for the rest rule; lead's call, after the check, for the one-fight rule; FL, ABL, DH owners apply | M2 |
| **BR-32** | ENM 13.2, 13.6, **21 (Later row)**; **ABL 5 (settled deferral)**; ABL 2 reading; HZ 6.2.1 reading; **ENM 17.5 hard control (settled, exception)** | Gambits M3 gated, never on F1; Frozen Toast exception (4.14.3) | ⚑ **Settled** (ABL 5, ENM 17.5); **Q3** | **Client (Q3, agreed 2026-11-22)**; enemies, ABL, HZ owners apply | M3 gated, else Later |
| **BR-33** | **HZ 6.3 pilot placement (agreed HZ Q1)**; HZ 6.17; ENM 9.3 readiness | M2g arena Stir hosting; M3 Keynote binding and Overtaken; HZ 6.17 rows; Master Landmark tick (4.14.1) | ⚑ **Agreed** (HZ Q1) | Lead's call; HZ owner (request) | M2g/M3 |
| **BR-34** | **MAIN 4.7 / RT-21 (decided)**; **HZ-25 (agreed)**; HZ Mood table; **LE-19 (agreed, read)**; ENM 17.5 thresholds (kept) | Nightmare Renditions; Perilous one tier up; Mood readings; 75% refused (4.14.2) | ⚑ **Decided** (MAIN 4.7); ⚑ **Agreed** (HZ-25, LE-19) | Lead's call; told plainly; HZ, LE owners apply | M3 |
| **BR-35** | **MAIN 6.6 profile save (decided)** | Boss Ledger and Grudge; `bossMemory` (7) | ⚑ **Decided** (MAIN 6.6) | Lead's call; main design owner applies | M3 |
| BR-36 | **RS 7.3/7.4 (agreed, RS Q3, read)** | Phase Score: per-boss stems, a layer per threshold; the per-boss timbre table (6) | ⚑ **Agreed** (RS 7.3/7.4, read) | Lead's call | M2/M3 |
| BR-37 | LP 4.4; FL; CMP naming | `BossDefeated` canonical; `BossResolved` alias (4.2.4) | — | Lead's call; LP owner applies | M2 |
| BR-38 | **HZ 6.3.2 (agreed, read)**; **RC1–RC9 (agreed, read)**; RC1 closed motion list (Device idle and act motion added); RC5+ pattern table (new marks registered); RC8 (request: the signature banner as a bark trigger) | Arena charter BX1–BX10; the camera rotation gate; the asset manifest; animation and voice rules (6) | ⚑ **Agreed** (read; RC1, RC5+ and RC8 rows requested) | Lead's call; RS owner (RC rows) | M2 |
| BR-39 | New | V17 `BossRooms` (9.2) | — | Lead's call; segment 12 | M2 |
| BR-40 | **DH 18.2 (agreed, feeds)**; **CMP 7.4 (agreed, read)**; CL 5.6 | Reports (made computable), arms, death bands, the small-party upper bound, BossMinutes, floor-clock line (9.3, 9.4) | ⚑ **Agreed** (DH 18.2, CMP 7.4, read) | Lead's call; DH owner confirms bands | M2 |
| **BR-41** | **ENM 17.5 A-3 timing** | Prompts (M3 option) (7) | ⚑ (A-3, an approved change row) | Lead's call; enemies owner (request) | M3 option |

**Told plainly (no question; confirmed by the client on 2026-11-22):** BR-2 (no generated fill), BR-3 (arena entry as a notice), BR-31 (the one-fight rule), BR-6 (strip not a clock), BR-11 readings, BR-14 (`chain(n)` instead of `fixedInPlace`), BR-15, BR-18, BR-21 (Shorthanded rejected), BR-27 (theme damage type on carried chassis), BR-34 (75% refused), the M1 "No terrain" reading, the F1 Landmark tick kept against two judges, the Break pip strip rejected for M2.

---

## 14. Open items for the client

None. Approved by the client on 2026-11-22.

The client approved this document as a whole on 2026-11-22, including every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the lead's calls after the check A1–A33 in 14.1, specifically including A13, the one-fight rule for party per-fight resources, and A28, the small-party upper bound that the toy fails today, both as written), the told-plainly items (including "Told plainly after the check" in 14.1), the readings recorded in 13, and its approval covers the flagged rows of 13. The five questions raised before writing were answered on 2026-11-22 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. Writing raised **no new client question**, and neither did the design check that followed (builder, consistency, play and art-and-audio lenses): every new lead's call and every lead's call after the check in 14.1 sits inside the five answers, keeps an approved rule, or is flagged against the text it touches. The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them (including the owner-refusal fallbacks and the wave-gated items), and the naming pass remain future items.

### 14.1 This document

**The five answers (agreed, 2026-11-22), each the recommended option:**

- **Q1, the RS 12 Cut 1 order (BR-23 to BR-26):** option A, graded. 1a drops the Appetite only if not yet built; 1b drops the Thing in Cell Zero and folds it into the Jailer as a Rendition; Cut 2 gains the Cut 2R rider (keep the Appetite, free if built); 1c, the Master in the Frame, moves below Cut 2 and is client-only. Plus 3 Renditions per F1 boss in M2, the launcher F1-boss streak rule in M2, and the Wake's gate prioritised. Told plainly: the Gut is built first, so its saving may already be spent; graded steps relieve less per cut, so Cut 2 may come sooner. Rejected: B, the approved order plus Renditions and the launcher rule; C, F1 extras cut last.
- **Q2, resting while an engaged boss lives (BR-31):** option A. No short rest inside the Sanctum (Forecourt, arena, Stairhead) from first engagement until the boss falls; rests elsewhere on the floor stay legal (A-4 intact). Rejected: B, a floor-wide ban (not kept as a pre-approved fallback; the lead may raise it again if `MidBossRestRate` fails, 14.2); C, the Forecourt restable throughout.
- **Q3, Gambits (BR-32):** option A. Committed boss signatures move from Later into M3, gated, never on F1, with the Frozen Toast exception. Rejected: B, without the exception; C, keep Later.
- **Q4, a breakable group in every F2–5 boss fight (BR-19):** option A. Every F2–5 Boss Court includes at least one non-fearless escort or reserve group (narrows HR-21, agreed 2026-11-20). Rejected: B, keep HR-21 and measure.
- **Q5, the Later Dreamer true-ending boss (BR-30):** option A. RS 8.11's Later "Dreamer as a true-ending boss" is replaced by a bodiless set piece: after the last keeper falls, the room wakes and the party walks into the waking light. Rejected: B, keep the Later line.

**New lead's calls (made while writing; confirmed by the client on 2026-11-22, with this document):**
1. **Document shape:** the proposal's section numbers 0–13 are kept so every cross-reference holds; the client decisions become 0.2 and the decisions in brief 0.3; intent, goals, non-goals and anti-goals are added as 0.4; the client questions become 14 (open items); the closing parts follow as 15 (risks), 16 (left to the builders), 17 (depends on), A.6 (BR cross-reference) and Appendix B (sources), as in EV.
2. **Q1 values are client keys, not tuning:** `launcher.f1BossMemory` 2, `boss.f1RenditionCount` 3 and the cut-step order; a gate miss that would move them returns to the client (header, 9.1, 14.2).
3. **"Built" defined for Cut 1a:** kit, `ArenaTemplate` and rig have passed V17s at the schedule review that applies the cut (header).
4. **Cut 2R reconciled with 1a:** 1a always precedes Cut 2, so Cut 2R only ever keeps a built Appetite, at no new cost; the toy's 2.34× counts the built Appetite inside Cut 2 (4.10.4).
5. **Folded Renditions count toward the survivor's 3:** under Cut 1b "Cell Zero Opens" replaces the Jailer's R3 *Lock-In*; the Lord's Gallery is a Guise and does not count (4.10.1, 5.3).
6. **A failing F1 Rendition is replaced, not dropped below 3;** a boss that cannot field 3 passing Renditions returns to the client (4.9, 14.2).
7. **A default R2 for the Thing in Cell Zero,** *Long Reach* (Guise 2, loop 4 with Chain Crack as Lead), because *The Chain Snaps* is offered only if `RangedOnlyWinShare` alarms; the Thing then ships 3 Renditions while it is in M2 (5.3).
8. **The Q4 group defined:** an escort group or the (d) reserve group all of whose members are non-fearless types; mindless types and the patrol joiner never count; required in every formation at every party size after AddCap; where a reserve is mindless the escort supplies it; checked by V17s (i) `BossCourtBreakable` (4.5.1, 4.7.4, 9.2).
9. **AddCap never trims the last Q4 group** (or an F1 `F1EscortBreakable` group); fearless members are trimmed first; the floor-clock lever respects it (4.5.3, 9.4). (After the check, Few-and-Strong picks only a type of equal fearless and mindless status: call A10 below.)
10. **F5 and Q4:** if either final boss's reserve type is fearless, the F5 formation carries a non-fearless escort group, with no roster change (5.6).
11. **The Sanctum rest rule's mechanics:** `ArenaEngaged` starts it (so the F1 Catch-up order holds); the Rest button shows the rule as its unusable reason and is never hidden; F1 has no exception; floor state `sanctumEngaged`; telemetry `SanctumRestRefused`; V17t test; the mid-boss rest arm rests outside the Sanctum only (4.2.1, 4.2.5, 4.15, 9). (After the check it is a location ban, not an awareness reading: call A12.)
12. **`MidBossRestRate` alarm** no longer applies a fallback by itself: Q2's option B was rejected, so a miss returns to the client as a future item (4.2.5, 14.2).
13. **The Frozen Toast Gambit in full:** a one-square footprint on the target at commit; Frozen only on a unit still there; no Frozen on a unit that could not leave (no-pin rule); still the boss's one hard-control ability and one of the F4 encounter's two sources; not subject to the 60% spike row; ships only with BR-32 (4.14.3).
14. **Bounds for the Later bodiless set piece:** no unit, HP bar, plate or named banner; obeys `FinalBossNoDreamer` (4.13). (Tightened after the check: display only, cannot be failed, victory at `BossDefeated`: call A30.)
15. **The Heavy Thing's M2 dressing** is the Gut's rib-cage, because under Cut 2 floor 4 is always the Gut (4.13).
16. **Medium-boss row under Cut 1c:** the Kennel is exempt while the Master is cut; V17s (c) reads the shipped roster (4.7.6, 9.2).
17. **A cut boss's IR second form is not owed;** the Kennel's universal-chassis signature passes from the Master's spear to Old Ragged's required second form at 1c (4.11.1).
18. **Art order under graded Cut 1:** survivor Renditions (Jailer, Old Ragged) and the Pylorus's before the Thing's and the Master's rigs (6).
19. **`chain(n)` leash data** also covers the folded chained unit, a non-boss unit, as part of the BR-14 request (4.6, 11). (After the check that unit is a chained Lifer: call A20.)
20. **Renames for collisions:** the Master's R2 Device "the Empty Frame" → **the Turned Portrait** (RS RC1 and TL use "an empty frame" for a Splintered lock); the Guest's arena "the Lying-in-State" → **the Viewing Room** (IR's item name); the Carillon's second handle is **the sluice**, skinned distinctly from HZ's non-arena Sluice Wheel Cord (header, 5.3, 5.4).
21. **The Old Ragged M1 reading's id corrected:** the proposal cited it under BR-28 (the reward card); it belongs to BR-1 (the template) (5.3).
22. **Read flags in 13:** BR-1, BR-8, BR-10, BR-15, BR-18, BR-28, BR-29, BR-38 and BR-40 are flagged for reading agreed or settled text (13).
23. **New V17 rows:** V17s (c) Rendition count, (i) `BossCourtBreakable`, (l) Gambit rules, (m) cut consistency; V17t Sanctum rest and launcher bypass tests (9.2).
24. **Owner-refusal fallbacks** in 14.2, each stated so a refusal never blocks M2.

**Told plainly (confirmed by the client on 2026-11-22):**
1. No legendary actions, no lair slot, no extra boss turns (0.1).
2. Shorthanded is rejected, against all three judges (BR-21).
3. Model C is the M3 layer, not the M2 chassis (3.3).
4. Generated arena fill is rejected (BR-2).
5. The Rhythm strip is not a clock (BR-6).
6. Phase triggers stay HP-only (4.4).
7. Upended is not Hampered but does deny boss actions (BR-7).
8. Every boss is ≤ Large; "huge" is proportion (BR-18).
9. The approved F1 rows do not bound F1 deaths; a simulated gate is added (4.9).
10. The true repeat figures are 22% and 44%, not 25% and 50% (BN11).
11. The Gut is built first, so Cut 1a may save nothing; graded steps relieve less per cut, so Cut 2 may come sooner (Q1).
12. A-4's retreat-rest-return is kept; only the Sanctum is closed to rest (Q2).
13. A true 75% Gut threshold is refused (BR-34).
14. The F1 Landmark tick is kept against two judges (3.3).
15. Carried chassis take the theme's damage type (BR-27); the F5 signature takes no second form.
16. The Break pip strip is rejected for M2 (7).
17. **(Added while writing.)** The toy was not re-run after the client's answers or the new lead's calls (8).
18. **(Added while writing.)** The proposal's Old Ragged M1 reading cited the wrong BR id (14.1 call 21).
19. **(Added while writing.)** Under graded Cut 1c, the Kennel would have no Medium boss (14.1 call 16).

**Lead's calls after the check** (each marked **(lead's call, after the check)** in the text; none changes a client answer; confirmed by the client on 2026-11-22, with this document):
- A1. **The Rendition is the only drawn unit:** it fixes Guise, formation and (optionally) mirror; draw 1 picks it uniformly (from M3, from the `Hard` subset under Nightmare tier 1), draw 2 is the mirror bit used only when `Mirror` is null; explicit Rendition lists, every Guise and formation referenced (4.10.1, 4.5.1, 4.12).
- A2. **`RenditionRecord` extended** to carry everything the catalogue changes: Rhythm per band, ability swaps and promotions, phase options per threshold, Devices, stance lines, `Mirror`, `Hard`, `Tell`, `FoldedFromBossId`, `ReplacesRenditionId`; V17s reads the effective kit (9.1).
- A3. **Renditions re-authored to change the question:** Old Ragged's R2 (a loop-3 shape) and R3 (the Kennel Shutter, a Deny Device); the Master's R3 (a Lead beat); the Executor's R3 (a Lead beat); the Guest's R3 loop stated; the Thing's R3 at loop 3 with Chain Crack as its single signature; `rhythmStart`, formation, Guise and mirror never count; BK4 counts Lead beats and exempts Old Ragged by name; every Rendition has a visible tell (BK13) (4.10.1, 5.3).
- A4. **The Jailer's formations pinned per Rendition** so Lock-In's and Cell Zero Opens' (d) reserves survive AddCap and the solo minion cap; the drafts' Lamplighter escort withdrawn; a (d) reserve is never trimmed to zero (BK14) (4.5.3, 5.3).
- A5. **Rhythm pointer rules made exact:** advance on every boss turn; pending signatures override Off and Lead beats but never another Downbeat, and re-phase the pointer; owed-beat carry across a threshold (at most one turn of delay); `rhythmStart` limits; F1 loops ≥ 3 with the first Downbeat ≥ 2 beats in; the reset hold; the banner while pending; a loop changes shape only at (a) or (b) (4.3.1, BK15).
- A6. **Device semantics:** Cancel skips the next Downbeat and carries across a threshold; Sate as Cancel; charges reset to max at phase entry; **F1 Cancel Devices one charge per fight**; a handle blocked by the denial budget is shown unusable (4.7.1).
- A7. **Device data and triggers:** handles carry kind, charges and trigger modes (Interact, RangedAttackOnPart, AreaEffectTag, ForcedEntry); one optional second handle on F2+ (the sluice as Deny, the Pylorus's rescue as Release); Divert's area placement; the Probate Desk is Cancel only; the Grounding Chain rewritten as a Deny curtain; the wagon bolt one round (4.7.1, 5).
- A8. **Headline ordering:** the newer headline always slips; double crossings; every delay re-checks OneDueThing; a 2-round total delay cap; a blocked Gambit commit waits; the V17g proof is an enumeration, not a dry run (4.3.4).
- A9. **Reserve arrival:** rings of increasing radius for the fan; initiative as ENM 12.4 joiners at round R+2; the arena party size fixes formation fill, AddCap and trimming at floor generation (4.5.2, 4.5.3).
- A10. **Few-and-Strong** picks only an upgrade type of equal fearless and mindless status, else the cost is unspent (4.5.3).
- A11. **Upended** applies through a save and a Prone immunity; a second Expose in a phase deals damage and Prone only (4.7.2).
- A12. **The Sanctum rest rule is a location ban** (any party unit on a Sanctum square), added to ABL 15.1 and DH 11.3; awareness, combat end and the A-4 reset are untouched; `RestTaken` telemetry and computable rates (4.2.5, 9.3).
- A13. **One fight for both sides:** while the boss lives and `sanctumEngaged` is set, a combat end in the Sanctum does not reset party cooldowns or once-per-fight counters; `ThresholdDipRate` (4.2.5).
- A14. **Engagement:** arena entry is an automatic notice (MAIN 4.3 kept); the boss group joins an active combat as joiners and Take Places is skipped; Take Places by path distance ≤ Speed onto per-Arch `takePlacesSquares[]` (4.2.1, 4.2.2).
- A15. **Graces at re-engagement:** `endsOn: arenaSeal` is processed at every arena combat start while `sanctumEngaged` (4.2.1).
- A16. **The F1 Catch-up guard applies from M2 only;** M1 has no guard (4.2.1, 10).
- A17. **Launcher rule:** the exhaustion tuple gains the boss rule; `recentRuns[].f1BossId` for every launched personal run; the repeat report by launcher emulation (4.10.3).
- A18. **Cut data:** `cutState { applied, appetiteBuilt }` stamped into the content version; folded Renditions and Guises carry what they replace (9.1, 4.10.1).
- A19. **Stamp inputs:** one authored size per (Guise, band), FL's `arenaCell` sized to contain it; Guise × mirror × 4 rotations proven; the fallback keeps the Rendition's kit and never re-derives shown facts (4.1).
- A20. **The fold uses a chained Lifer,** the Gaol's approved brute and rig, not a new "Chained Thing" type; RS 8.3's escort line read and flagged in BR-24 (4.10.2).
- A21. **Gambit order:** advance the pointer, then commit; no legal footprint waits; a committed Gambit resolves even under control; the escape guarantee is a runtime scorer constraint (4.14.3).
- A22. **Reports defined:** arrival, band step, `BossMinutes`, `DeviceUseRate`, `ThreatMix`, `ClassBossContribution` and fear-target availability, `MidBossRestRate`, the F1 gate per Rendition, `FinalBossNoDreamer` in data (9.3, 4.13).
- A23. **Boss-action forced spend** defined (4.8.1).
- A24. **`deviceBait`:** a boss whose post is on or under an Expose face steps onto it on Downbeat turns; the Head of the Table's chair lock lifts at 66% (b) (4.6, 5.5).
- A25. **Kiting guard on every `chain(n)` or post-bound boss;** the Pylorus's Swallow at line 8, its 66% bile set across the ranged band and its reserve socket beyond it (4.6, 5.5).
- A26. **Rhythms authored for every boss's first band** (the Guest, the Executor, the Strongest Man, the Pylorus, the Governess); later bands follow the band rule; the Engine's Clangour as a Lead beat; Madame Ashes keeps one loop (one change per entry) (5).
- A27. **The Q4 group sits in the escort wherever the budget allows;** reserve-only cases are listed and told (4.7.4).
- A28. **Small-party upper bound:** on F2–5, n ≤ 2 deaths ≤ 1.5 × n = 4, F5 n ≤ 2 ≤ 45 per 100, tuned only through per-size data (4.8.2).
- A29. **Arena-entry confirm** (UI only) (4.2.1).
- A30. **The bodiless set piece is display only,** cannot be failed, and victory is recorded at the last keeper's `BossDefeated` (4.13).
- A31. **The two finales' Devices differ in kind** (the Lamp Snuffer as Deny, the Mainspring as Cancel at the escapement); the last band escalates by display and score, not by a second change (4.13, 5.6).
- A32. **Readability, art and audio:** no sleeper breath; the Pendulum hung over non-walkable squares with one verb and no swing meter; the Rhythm strip's three marks and the new marks registered in RC5+; the Governess's reserve socket off the bed and Tuck In's VFX; the asset manifest and throughput gate; no paired animations; per-boss base stems and a layer and Arena State per threshold; the camera rotation gate; RC8 voice rule; Rendition tells; no pose implies height; reserve silhouettes keep their RC3 read; one leash ring mark; RC2 Device bodies; Upended poses inside the square; the per-boss timbre table; the Gaol's ticking as bed texture (6, 5).
- A33. **Name and data fixes:** There, There; Drawn Shutters and the Kennel Shutter; Arc Note; the Pin-Down; the View Halloo; the formation names; Threadwalk as the Weaver's bonus action; F3 arenas at 15×17; the Master's R3 as a Hall Guise; the menagerie wagon in 4.5.2; F-BOSS channels corrected; MAIN 6.3 sentence; EP 6.14 request; IR universal-chassis reading; read flags on BR-14, BR-17, BR-24, BR-36 (header, 4, 5, 13, A.5).

**Told plainly after the check** (none overrides a client answer; confirmed by the client on 2026-11-22, with A13 and A28 explicitly confirmed as written):
1. Several Renditions as first written did not change the question by the document's own rule (Old Ragged's R2 and R3, the Master's R3, the Executor's R3, the Thing's R3); they are re-authored, and the variety figures are re-run counting only qualifying Renditions (8).
2. As first written, the Jailer's Lock-In and the folded Cell Zero Opens would have been trimmed to nothing at F1 solo; each now pins a lighter formation (5.3).
3. The fold no longer adds a new unit type: the chained Lifer is an approved Gaol brute (4.10.2).
4. **The one-fight rule** (A13) narrows settled ABL and DH text: inside the Sanctum, while the boss lives, stepping out no longer resets party cooldowns or once-per-fight abilities. It closes a loophole Q2 did not reach (the threshold dip needs no rest). The client explicitly confirmed it as written on 2026-11-22; if the ABL or DH owner refuses it, the fallback is report-only (14.2).
5. **The small-party upper bound** (A28) is failed by the toy today by a wide margin (F5 expert solo 70 per 100 against a cap of 45). The client explicitly confirmed the bound as written on 2026-11-22. If per-size data cannot meet it, it returns to the client, not to Shorthanded (14.2).
6. The F1 Device now has one charge per fight, so the Device can no longer delete the F1 signature with the Rhythm's help (4.7.1).
7. Combat still starts by noticing (MAIN 4.3): stepping into the arena counts as being noticed (4.2.1).
8. The Pylorus was a turret against ranged parties; its pull now reaches the ranged band (5.5).
9. The bodiless set piece is now bound: display only, cannot be failed, victory recorded first (4.13).
10. The Dreamer's breath cue is removed: the breath family is the reserved Heart Stir cue (4.13, 6).
11. Bells, horns and alarms in boss kits are mapped against the reserved timbres; the Master's Horn becomes a voice-family cry (6).
12. Art volume is now stated (about 43 interiors, 14–18 clips per rig), with a throughput gate that slips Wake bosses and second F2–5 Guises before any F1 Rendition (6).

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a lead's call, a new lead's call or a lead's call after the check (confirmed by the client on 2026-11-22, with this document) unless marked:

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| Mid-boss rests outside the Sanctum | `MidBossRestRate` > 25% *(illustr.)* of boss wins on any floor | The lead reports to the client and may raise a floor-wide rule again; Q2's option B was rejected, so nothing changes without the client | Client |
| F1 death gate miss | `BossDeathShare` F1 > 2.5 per 100 | The 4.9 lever order; never a guard row | Lead |
| An F1 boss cannot field 3 passing Renditions | V17s or the F1 gate | Re-author before M2 ships; if impossible, back to the client (Q1 fixed 3) | Lead; client |
| Same-fight repeat misses | `BossRepeatRate` well above 8.7% / 26.5% | Content first (Rendition parity, the Wake's gate); any change to the launcher memory or Rendition count returns to the client | Lead; client |
| The Wake's gate fails | RS 8.2's gate | The Wake stays the first M3 unlock (RS 8.5); F1 variety rests on Renditions and the launcher rule | RS owner (agreed, Q1 priority) |
| Gambit gates fail | `GambitDodgeRate` < 70% novice or `GambitStayRate` out of band | Gambits stay Later (agreed, Q3) | Pre-approved by Q3 |
| `ClassBossContribution` Herald gap persists | Herald spread > 10 points on F2–5 | Content inside the Q4 rule first; a Device pip strip (Later) only then | Lead; client for the strip |
| Device dominance | `DeviceUseRate` > 85% or < 30% | Rework the Device's payoff | Lead |
| Floor clock miss | `BossMinutes` > 7 min median on F4–5 | The 9.4 floor-clock line, never boss HP first | Lead |
| `RangedOnlyWinShare` > 40% | Report, any `chain(n)` or post-bound boss | The Thing: *The Chain Snaps* replaces its R1 (a), with RS sign-off. Others: a reserve socket or (e) set in the ranged band, then a Lead beat that reaches it (4.6) | Lead; RS owner where RS text moves |
| Small parties too deadly | `BossDeathShare(n ≤ 2)` > 1.5 × n = 4 on F2–5, or > 45 per 100 on F5 | `hpByPartySize[1..2]` and the solo composition table first; if per-size data cannot meet it, back to the client (Shorthanded stays rejected) | Lead; client |
| Threshold dips | `ThresholdDipRate` high where the one-fight rule is refused | The lead reports to the client | Lead; client |
| **Enemies owner refuses** the Rhythm (BR-6) | — | Cooldowns only with ready-state pips (4.3.1) | Enemies owner |
| **Enemies owner refuses** Old Ragged's M2 `readyAt` 2 (BR-22) | — | Old Ragged exempt; its Rhythm opens on the roar | Enemies owner |
| **Enemies owner refuses** AddCap (BR-12) | — | Reserves stay outside caps; the F2–5 35% row is enumerated with the full reserve; trimming moves into authoring | Enemies owner |
| **Herald owner refuses** BR-19's F2–5 rule | — | Returns to the client (Q4 was agreed) | Herald owner; client |
| **RS owner refuses** the graded cuts, the launcher rule or the Later line | — | Returns to the client (Q1, Q5 were agreed) | RS owner; client |
| **ABL or DH owner refuses** the Sanctum rest condition | — | Returns to the client (Q2 was agreed) | ABL, DH owners; client |
| **ABL or DH owner refuses** the one-fight rule | — | No rule; `ThresholdDipRate` reported beside `BossCheeseRate` (4.2.5) | ABL, DH owners |
| **Main design owner refuses** the arena-entry notice reading | — | `ArenaEngaged` on (a) starts only the rest rule and Take Places; combat starts at the boss's first notice, which the Wary boss's post makes on its first turn of sight | Main design owner |
| **HZ owner refuses** the arena round-end step outside the Stir gates | — | The arena round-end step is the step after the last turn-end step of the round, with the same slots (4.3.4) | HZ owner |
| **RS owner refuses** the Lifer reading for Cell Zero Opens | — | The reserve is 2 Inmates and the Lifer is dropped; the Rendition keeps the Cell Zero alcove socket | RS owner |
| **IR owner refuses** the Kennel universal-chassis reading | — | Old Ragged's second form is authored on a universal chassis under 1c | IR owner |
| **RS owner refuses** the RC8 signature-banner bark trigger | — | Signatures are not voiced at all; SFX only | RS owner |
| **EP owner refuses** the EP 6.14 formation change | — | Arena formations are authored as Retinue variants inside EP's shape on F2–5 | EP owner |
| **HZ owner refuses** the strip-not-a-clock reading | — | The Rhythm strip moves off the initiative entry onto the plate's hover tier only; no map element | HZ owner |
| **HZ owner refuses** the M2g arena Stir hosting | — | The signature room keeps the pilot (4.14.1) | HZ owner |
| **IR owner refuses** chassis-only or the F5 no-second-form row | — | Carried lines stay display-only on the boss; the F5 second form is authored at 0 VP | IR owner |
| **TL owner refuses** the `arenaDevice` reading | — | Devices are authored as segment-9 pieces with their own preview record, same rules | TL owner |
| **Tone review** renames "The Dreamer Turns Over" or Tuck In | — | "The Bed Turns Over"; Smother | HZ owner; tone review |
| A harmful bodiless set piece | Its Later design asks for one | A new client question; bound as display only until then (4.13) | Client |
| Art throughput | Rig and clip throughput below plan by mid-M2 | Wake bosses and second F2–5 Guises slip before any F1 Rendition (6) | Lead; RS owner |
| Naming pass | — | Every placeholder name here | Client |

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | **Predictability.** Fixed Rhythms are learned fast; Renditions fix the identical fight, not the same face | Renditions that change the question; the Wake gate (prioritised, Q1); Nightmare Renditions (M3) |
| 2 | **Verification and simulator load.** Even with BN14's fix, Rendition × formation × size enumeration grows the RS 10.4 balance pass; each cut state adds an arm | Static mirror/rotation proofs; simulate only Rendition × formation × size |
| 3 | **F1 safety.** The toy needed the authored F1 dice at about 0.52×; the F1 gate may force bland F1 bosses | The lever order keeps the danger in fewer, louder beats |
| 4 | **Floor clock.** `BossMinutes` ≤ 7 min is about 35% of the F4–5 floor; with Stirs and joiners the 20-minute median may fail | The floor-clock line (9.4) |
| 5 | **Device dominance.** A Device used in over 85% of fights is a puzzle with one answer | `DeviceUseRate` band; the never-used arm |
| 6 | **Readability load.** Rhythm, banner, Device, reserve and (M3) Stirs and Gambits | BX8 is a hard gate |
| 7 | **Approved-text changes.** BR-6, BR-7, BR-9, BR-11, BR-12, BR-22 and BR-32 reopen settled text | Refusals have stated fallbacks (14.2) |
| 8 | **Small parties.** Solo and duo F2–5 stay much deadlier; the toy fails the upper bound today | Per-size data only; the n ≤ 2 gates, both ways (4.8.2); back to the client if data cannot meet them |
| 9 | **Tone.** The Heart walks the RS 7.5 line; one animation could imply a sleeper; the Later set piece must too | `FinalBossNoDreamer`; tone review; the set piece is bodiless (Q5) |
| 10 | **Cut 1.** Graded steps relieve less schedule per cut, and the Appetite saving may already be spent (agreed, Q1, told plainly) | Survivor Rendition art first (6); the Wake gate |
| 11 | **Escort fearless status** of several roster types is unverified until V17s runs (BK3); Q4 may force escort changes on F2–5 | `BossCourtBreakable` early in M2 content |
| 12 | **Rest displacement.** The Sanctum rule may push parties to walk back to a mid-floor room and rest, lengthening floors | `MidBossRestRate`, `PostBossBacktrack`; 14.2 |
| 13 | **Toy crudeness** | The simulator replaces every figure here |
| 14 | **Art volume.** About 43 interiors and 14–18 clips per rig across 17 rigs (6) | Guises from RT piece kits; no paired animations; the throughput gate slips Wake bosses and second F2–5 Guises before any F1 Rendition |
| 15 | **Rhythm exactness.** Pending, carry and reset rules add state the AI and UI must agree on | V17t replays: the banner equals the AI's decision in every rule case |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: loop lengths and beat orders, dice, sizes, AddCap's +1, the spike percentages other than the settled rows, the doorway-band weight, Device charges, the report targets other than the stated gates.
- Guise interiors, formations and fact text within the caps; the per-boss dressing passes; reserve-socket props.
- Godot scene structure for the plate, Rhythm strip, Playbill, Take Places and Device previews, with no physics.
- Animation and audio timings within 6.
- Names, until the naming pass.
- **Not tuning:** the F1 menu without Strike; Signature never a boss action; no enrage or damage cap; HP-only phase triggers; the last-threshold rule; reserves never act on arrival; one Device per boss with a ranged trigger; F1 Devices never harm; no damage immunity on `final` or F1 bosses; ≤ Large; draw-free stamping and the fixed 4 variant draws; template-local tie-breaks; the F1 death gate; **3 Renditions per F1 boss, the launcher memory of 2 and the graded cut order (agreed, Q1)**; **the Sanctum rest rule (agreed, Q2)**; **Gambits never on F1 and the Frozen Toast exception (agreed, Q3)**; **the F2–5 non-fearless group (agreed, Q4)**; **no Dreamer body, ever (agreed, Q5; RS 7.5)**.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **The enemies owner:** BR-3 (joining an active combat), BR-4 to BR-12, BR-14, BR-17, BR-19, BR-20, BR-21, BR-22, BR-32, BR-41; FL-21(a)/(e); HX-2 Waver.
- **The RS owner:** BR-16, BR-23 to BR-26 (BR-24's Lifer reading), BR-30, BR-38 (RC rows); the Wake's gate as a priority (Q1); the asset manifest's cost units.
- **The Herald owner:** BR-19 (Q4).
- **The ABL owner:** BR-6 and BR-32 readings; BR-31 (Q2's rest condition; the one-fight rule).
- **The HZ owner:** BR-6 reading, BR-10 (the arena round-end step), BR-16, BR-22 (HZ-25 reading), BR-30 (tone line, the breath family), BR-32 reading, BR-33, BR-34.
- **The DH owner:** BR-22, BR-31, BR-40.
- **The FL owner:** BR-1, BR-31; FL-21(a).
- **The room terrain owner (segment 3):** BR-1.
- **The TL owner:** BR-16 (TL 6.20 reading, TL-40).
- **The EP owner (segment 6):** BR-11 readings, BR-19 (`answers{}`, EP 6.14 formations).
- **The IR owner:** BR-27.
- **The LE and LP owners:** BR-28, BR-37.
- **The EV and conditions owners:** BR-3, BR-29.
- **The CMP owner:** BR-21.
- **The VG owner:** BR-18 (size-gated verbs).
- **The main design owner:** BR-2 (MAIN 6.3 sentence), BR-3 and BR-4 (MAIN 4.3 reading), BR-25 (`recentRuns[].f1BossId`), BR-35 (`bossMemory` in the profile save).
- **Segment 10 (UI):** 4.16 (answered: XI-11, XI-19; exploration and information design 4.4.4, 4.9.8, 5).
- **Segments 11 and 12 (seeds, validation):** 4.17; V17 (BR-39); the launcher order (BR-25).
- **The simulator:** every report of 9.3 and every arm of 9.4.
- **Art, audio and writing:** 5, 6.
- **Naming pass:** every placeholder name here.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Fun critic (FN)

| Id | Finding | Resolution |
|---|---|---|
| FN1 | Phase-and-rest cheese | BR-31 (agreed, Q2); A-4 kept |
| FN2 | Stacked outs make signatures irrelevant | Upended never cancels signatures; denial budget; `SignatureLandRate`; DeviceUseRate band; commits never voided |
| FN3 | Every boss on an A-A-S loop of 3 | Tempo variety rules (4.3.2); loops 2, 4 and two-Downbeat loops in 5 |
| FN4 | F1 slogs | F1 length target; HP third in the lever order; signature share floor |
| FN5 | Phase drama designed out | Last-threshold rule; type icons with reveal at entry; first-sighting cards (M3); threshold preview |
| FN6 | Ranged kiting of leashed or immobile bosses | Tall sightline rule; Chain Lash; Bile Spit range 10; `chain(2)` Pylorus; `RangedOnlyWinShare` |
| FN7 | Uneven F1 class showcases | Clustered pack; ranged Device triggers; Resolve Broken beat |
| FN8 | Plain final boss | Own Device per final boss; the Heavy Thing; Heart Act Keynotes stay M3 (CS13) |
| FN9 | Overloaded plate; Prompts in M2 | Two-tier plate; Prompts M3; BX8 on the plate |
| FN10 | Same face on F1 | Renditions must change the question; graded Cut 1, the launcher rule and the Wake gate (agreed, Q1); Ledger badges (M3) |
| FN11 | Fear monotony; Take Places blind; Jailer R3; names | BK4; Playbill during placement; R3 rewritten; one name each (Heavy Thing, Upended) |

### A.2 Balance critic (BL)

| Id | Finding | Resolution |
|---|---|---|
| BL1 | Caps on a different basis from the 70/75% rows; two boss actions | Crit-inclusive spike budget; two-action focus reading (BR-8) |
| BL2 | F1 rows do not bound deaths; fixes push damage untelegraphed | F1 gate; fight-level row; p90 rounds; signature share floor; lever order |
| BL3 | Shed erases fresh control | Shed only conditions present at round start (BR-7) |
| BL4 | Phase-hopping with rests; per-fight resets | BR-31 (agreed, Q2); Take Places once; charges and reserves persist; C's per-fight Hold reset rejected |
| BL5 | Downed cascade | "Party units" non-Downed; `HeroAloneRounds` |
| BL6 | Shorthanded is a Lone-Wolf bonus | Rejected; per-size data; n ≤ 2 gate |
| BL7 | Solo Herald versus an Emboldened escort | F1 formation outside the aura; Command restriction; Waver request; F2–5 group (agreed, Q4) |
| BL8 | Spawn camping and body-blocking reserves | Delay once; fixed fan; arrival-step immunity; `ReserveFirstTurnSurvival` |
| BL9 | Soft-denial stacking widens the skill gap | Denial budget; never-used arm; novice gap ≤ +10 |
| BL10 | Kiting deletes adjacent Downbeats | The signature becomes pending and overrides the next Off or Lead beat (4.3.1 rule 3) |
| BL11 | Escape ignores speed loss; endless slips | Current movement; the newer headline slips, at most 2 rounds of delay in total (4.3.4) |
| BL12 | Carried lines enlarge kits | Chassis only |
| BL13 | Few-and-Strong spikes | ≤ floor level +1; no elite or hard control; remainder unspent |
| BL14 | Seed breaks: route draws; mirror-variant tie-breaks | Per-record hash; template-local tie-breaks; mirror-equivalence test |
| BL15 | Pylorus `fixedInPlace` kills push payoff; bands; minutes; repeat figures; Rendition on door card | `chain(2)`; corrected bands, ≤ 7 min, true repeat figures; per-class `RenditionParity` |

### A.3 Consistency critic (CS)

| Id | Finding | Resolution |
|---|---|---|
| CS1 | Kennel gate as a piece contradicts TR-22 | Renamed the pen bar; RS 13 and HZ 6.17 Master row flagged; TR-22 untouched |
| CS2 | "F1 arena clock-free" narrows HZ; C's F1 Dials early | HZ kept: one M3 Landmark tick (the Master); Dials dropped; HZ-25 arena reading flagged |
| CS3 | Heavy Thing bludgeoning; four names | Piercing; one name |
| CS4 | IR names, F5 second form, carriage tables | IR names kept; no F5 second form (flagged); one table; armed flags as authoring under IR's rule |
| CS5 | ArenaEngaged before the Catch-up | F1 noticing suppressed; V17t test |
| CS6 | Gut built first; launcher rule M3; Cut 2R edits Cut 2 | Cut 1a conditional; honest question, answered (agreed, Q1); RS 12 wave row and Cut 2 definition flagged; Cut 2R reconciled with 1a (14.1 call 4) |
| CS7 | Old Ragged's kit changed quietly | Flagged; M1 unchanged; fallback exemption |
| CS8 | Boss-action denial needs flags | One bundle BR-7; authoring versus narrowing stated |
| CS9 | AddCap narrows (d); three definitions; Few-and-Strong elites | Flagged; one definition; no hard-control source; never trims the Q4 group |
| CS10 | `arenaSeal` in conditions; re-fire; BC4 | Alias kept; once per floor; no Grace exceptions in M2 |
| CS11 | Take Places unflagged | Flagged BR-4; restricted; Catch-up gated |
| CS12 | Name collisions; the false "Guise has 3 hits" | hymn bellows, the Winch, Mourners' Pews, Pull Under, Overdue, Returns Trolley avoided; Guise and Rendition confirmed 0 hits and kept; Empty Frame and Lying-in-State renamed while writing |
| CS13 | Heart Act Keynotes and a second Carnival arena host | Heart stays M3; Strongman's Pit Breathes not adopted |
| CS14 | F1 solo cost recount versus CMP-7 | Flagged reading; costs shown both ways |
| CS15 | Precentor resists its `counterType` | No resistance to `counterType` (flagged); Precentor resists cold |
| CS16 | Clashing BR numbers | One register BR-1–BR-41 |
| CS17 | Gambit clock readings disagree | Not a clock; one flagged reading |
| CS18 | Unanswered hooks | Hook table in 11 |
| CS19 | Herald rule narrows HR-21 | Q4 (agreed) |
| CS20 | `fixedInPlace` unflagged in the catalogue | Replaced by `chain(n)` |
| CS21 | Catalogue internal errors | Jailer R3 rewritten; Old Ragged's escort line cleaned; Carillon's weakness stated; "Long Chain" dropped; Old Ragged's BR id corrected while writing |
| CS22 | Horn audio | Named RC7 audio-blind test |
| CS23 | Pilot wording | "Never more than one new rhythm per floor" kept verbatim |

### A.4 Judges' grafts not taken, or changed
- Shorthanded (all three judges) → rejected (BL6).
- F1 arena clock-free (judges 1, 2) → HZ kept (CS2).
- Prompts in M2 (judge 1) → M3 option.
- Catalogue's plus-shaped Canopy Weight dropped at the Dream step → the Heavy Thing, 3×3, at once.
- The Thing's "Chain Snaps" as default (fun critic) → offered only if `RangedOnlyWinShare` alarms, because the last-threshold rule bars (b) at 50%; *Long Reach* is the default R2 (14.1 call 7).

### A.5 Names checked (case-insensitive, whole word, 27 approved documents)

**0 hits** unless noted: Guise, Rendition, Upended, ArenaEngaged, Take Places, Playbill, ReserveSocket, AddCap, Few-and-Strong, OneDueThing, Gambit, Downbeat, Offbeat, Lead beat, Rhythm strip, BossVariantKey, ThreatMix, Keynote, Overtaken, FinalBossNoDreamer, Bed Turns Over, Grudge, Boss Ledger, `bossMemory`, Phase Score, Arena State, Whelping Pen, pen bar, Windlass, Grounding Chain, Returns Bell, Feeding Hatch, Jaw Prop, Thread Anchors, Menagerie Wagon, Probate Desk, family pews, Lamp Snuffer, Mainspring, Heel Whistle, Mourning Curtains, Resolve Broken, Chain Lash, Chain Crack, Chain of Keys, Halberd Sweep, Fermata, Hold the Note, Marginalia, Spinneret, Showstopper, Final Bow, Gnash, Retch, Gorge, Lights Out, Cold Shoulder, Carving Knife, Disinherit, Spear Lunge, Gone to Ground, Ten to Three, Counter-Clockwise, Lock-In, Old Iron, Chain Snaps, Kennel Row, Pack Mother, Cell Zero Opens, Lord's Gallery, Wound Up, Cut 2R, `ArenaTemplate`, `arenaDevice`, `BossRooms`, `BossAnswerMatrix`, Dream contract, Waking Light; and the arena names Pendulum Hall, Portrait Kennel, Sunken Belfry, Choir Stalls, Index Well, Strongman's Ring, Proscenium, Head of the Hall, Antrum, Larder of the World, Turned-Down Bed, Mantel Nursery; the reports and checks NoUntelegraphedSpike, RestBetweenPhasesRate, MidBossRestRate, SignatureLandRate, RangedOnlyWinShare, HeroAloneRounds, ReserveFirstTurnSurvival, BossDeathShare, BossRoundsByFloor, BossMinutes, ClassBossContribution, DeviceUseRate, BossRepeatRate, RenditionParity, BossCheeseRate, PhaseReadTime, PromptDenialRate, GambitDodgeRate, GambitStayRate. Local prefixes BT, BN, BK, BX, FN, BL, CS: 0 hits.

**Added while writing (0 hits):** the Turned Portrait, the Viewing Room, Long Reach, `sanctumEngaged`, `SanctumRestRefused`, `BossCourtBreakable`, `launcher.f1BossMemory`, `boss.f1RenditionCount`.

**Added after the check (0 hits, grep re-run):** There, There; Drawn Shutters; Kennel Shutter; Arc Note; Pin-Down; View Halloo; Threadwalk; leash ring; linen press; `deviceBait`; `takePlacesSquares`; `ThresholdDipRate`; `RestTaken`; `cutState`; `dreamerDepiction`. "Lifer" is the approved Gaol roster brute (RS 8.3), used as that type. "Release" and the other payoff kinds (Cancel, Deny, Divide, Divert, Sate, Expose) are rule words, not names.

- **Kept on purpose:** "the Heavy Thing" (RS 9.3's phrase, 1 hit); Pendulum (5) and Bell Rope (8), RS 13's and HZ's names for these very features; "sluice" as RS 13's word for the Carillon's feature (never "Sluice Wheel", HZ's non-arena Cord); "Cell Zero" (9 hits, all the approved boss name); "the Den" for Old Ragged's arena and shape kit (RS 8.4 already puts its signature cache "in its den"; ENM's Den is an encounter template, a different kind of id, so data writes the kit as `shapeKit: Den` and the template as `encounterTemplate: Den`); IR's signature names.
- **Avoided after collision:** Run Through (1), Serving Hatch (5), Skewer (57), Nightlight (4), Composure (24), Prompt as a bare word (9; the mechanic is named "Prompts on boss actions" and its data field `prompt`), hymn bellows (4, HZ reserves it for non-arena look-alikes), Winch (6), Mourners (32), Pull Under (1), Overdue (13), Returns Trolley (13, an existing HZ Stir), Dumbwaiter (24), Reading Room (5), Lying-in-State as an arena name (4, an IR item; now the Viewing Room), Cage Lock (1), Reading Desk (1), the Empty Frame (2; now the Turned Portrait), "The Waking" (4, part of "the Waking City"; the set piece is named only by description). **After the check:** Hush (the Herald's heal kind; RS 8.0 and HZ avoid it; now There, There), Empty Chair (the Supper's Obsession; TL avoided it; now Drawn Shutters), Spark (a withdrawn Arcanist name; now Arc Note), Maul (a weapon; now the Pin-Down), the Horn (horns reserved for joiners; now the View Halloo), Retinue as a segment-9 formation set (EP's formation) and Wings (FL Blueprints), Chained Thing (withdrawn: the fold uses the Lifer). Candidates rejected for hits: Lull (HZ), Far Side (FL, EV, LP), Static (common rules word), Inhale (HZ).
- **"Prompts"** remains a new rules sense of a common word; rename at the naming pass if it confuses.

### A.6 BR cross-reference

Every draft numbered from BR-1 with different contents (Model A, Model B, Model C, the rhythm layer, the catalogue and the new-mechanics doc). All are renumbered into this register by topic:
- arenas → BR-1, BR-2; seal → BR-3; Take Places → BR-4; Playbill → BR-5;
- Rhythm → BR-6; boss actions → BR-7; telegraph budget → BR-8; phases → BR-9; OneDueThing → BR-10;
- reserves → BR-11; AddCap → BR-12; patrol joiner → BR-13; AI → BR-14; no enrage → BR-15;
- Devices → BR-16; Upended → BR-17; sizes → BR-18; answer windows and the Q4 group → BR-19; immunities → BR-20;
- party size → BR-21; floor 1 → BR-22; Renditions → BR-23; folded bosses → BR-24; launcher rule → BR-25; graded Cut 1 → BR-26;
- carriage → BR-27; card and Viaticum → BR-28; F-BOSS → BR-29; final boss and the bodiless ending → BR-30;
- Sanctum rest → BR-31; Gambits → BR-32; arena Stirs → BR-33; Nightmare → BR-34; Ledger → BR-35; Phase Score → BR-36;
- `BossDefeated` → BR-37; charter → BR-38; V17 → BR-39; reports → BR-40; Prompts → BR-41.

The drafts' options the client rejected (Q1-B and C, Q2-B and C, Q3-B and C, Q4-B, Q5-B) are not register rows.

## Appendix B: sources

- Game details in section 1 are from memory and general design knowledge (Into the Breach, Slay the Spire, Hades, XCOM 2, Fire Emblem, Darkest Dungeon, Inscryption, Wildfrost, Monster Train, Dead Cells, The Binding of Isaac, Enter the Gungeon, Gloomhaven, D&D 5E and the 2024 Monster Manual, MCDM's Flee, Mortals!, Pathfinder 2E, D&D 4E, 13th Age, Baldur's Gate 3, Dark Souls, Elden Ring, Final Fantasy Tactics); none is cited as a rule.
- The segment 9 proposal, its judges' and critics' reports, and its toy scripts (`bossfight.py` and runners: deaths per model, party-size fairness, telegraph actionability, the variety Monte Carlo and the cost table), illustrative only, kept in the design lead's scratch area, not in the repository.
- The 27 approved documents listed in the header.
