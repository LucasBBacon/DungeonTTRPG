# Exploration and Information: Design (Dungeon Generation, Segment 10)

Date: 2026-11-23
Status: Approved, 2026-11-23
Names updated by the naming pass (naming pass design `2026-11-24-naming-pass-design.md`, approved 2026-11-24; NP rows listed): NP-25 (this document's row, covering every register rename whose sites include XI), NP-6 (Upended → Sprawled restated). Change ids, code identifiers, field names and history rows are unchanged; history rows carry "(renamed …, naming pass NP-n)" notes.
Parents (immediate): `2026-11-22-boss-rooms-design.md` (boss rooms design, "BR", segment 9, approved 2026-11-22, **the most recent template**), `2026-11-22-events-design.md` (events design, "EV", segment 8), `2026-11-21-floor-layout-design.md` ("FL", segment 2; FL 13 hands most hooks to segment 10), `2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change ids are RT-n), `2026-11-21-room-terrain-design.md` ("ROOM", segment 3; "RT" in section numbers such as RT 6.12; its change ids are TR-n), `2026-11-21-hazards-design.md` ("HZ", segment 4), `2026-11-21-traps-locks-design.md` ("TL", segment 5), `2026-11-21-encounter-placement-design.md` ("EP", segment 6), and the loot designs `2026-11-22-loot-economy-design.md` ("LE", segment 7a), `2026-11-22-item-rolls-design.md` ("IR", segment 7b) and `2026-11-22-loot-sources-design.md` ("LP", segment 7c).
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

Items marked **(agreed)** are the client's answers of 2026-11-23 to the five questions this design raised (Q1–Q5; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document. The client also said that everything else in the lead's proposal stands as written: the composite "R" (Model A's chassis, Model C's Chalk on entry and Trace on sight, the UI catalogue's surface rules, the numbers pass's fixes; the Dream map as an M3 skin); `PartyKnowledge` in the core, `CanSee`, explored = entered, Downed units contributing no knowledge; remembered state with Afterimages and fades; the map (minimap M1, full map M2, layers, marks, Waymarks, click-to-travel, accessibility); the incentives (loose ends on the map only, the Detour quote, the Behind-you line, no completion reward); the reveal items and requests (Lodestring, F-TREASURE at M3, Surveyor's Sketch, Tithe Bowl, Ear Trumpet, Walls Have Ears, Cased, Overlook writing chalk); the counts-only narrowing for paid pointers; travel and Next loose end, no auto-explore, stopping before known noticing triggers; stealth, hearing and patrols; the surface slot model, priority stack, regions, card queue, budgets, combat suppression, board digits for room clocks only, Inspect parity and staged disclosure; accessibility and input; determinism; save integrity; the full UI surface catalogue with every hook placed; art and audio; the new mechanics adopted, held and rejected; the numbers and fixes (the toy, XN1–XN15); data, V18 `Information`, reports and arms; staging; ownership and hooks; the binding requirements; XI-1 to XI-32; the told-plainly items; the Later client questions (units blocking line of sight, Scout Ahead, the Dream map skin); the risks; and the appendix resolutions and names. The client approved this document as a whole on 2026-11-23.

**The lead's six answers to the brief (0.1)** were accepted by the client by proceeding to the five questions. They are recorded here as the lead's answers; the client confirmed them on 2026-11-23, when it approved this document as a whole, **including their contested parts**: no "Shell on sight" and no one-square Pocket rule; no trap or secret reveal items; no completion reward and no off-path loot lean; no bought map; no separate special-treasure class.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-23, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-23. Items marked **(lead's call, after the check)** were made when the written document was checked by the builder, consistency, play and art-UX lenses; none changes a client answer; they are listed in 14.1 with the check's "Told plainly after the check" items, and the client confirmed them all on 2026-11-23 (explicitly including the per-command suspend-slot writes, Q4 covering Chalked squares, and the rooms tally exception, as written). Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change XI-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-23 (with Q1–Q5 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge, a critic or a draft, or state an unwelcome forecast, and were told to the client in plain words (14.1, including those told after the check); the client confirmed them all on 2026-11-23. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q5 of this document (2026-11-23), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal (the six answers to the brief included); confirmed by the client with this document on 2026-11-23 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-23 |
| **(lead's call, after the check)** | A lead's call made when the written document was checked by the builder, consistency, play and art-UX lenses; never overrides a client answer; listed in 14.1, with a "Told plainly after the check" list, and confirmed by the client on 2026-11-23 |
| **(change XI-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text are flagged ⚑ there |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a judge, a critic or a draft, or states an unwelcome forecast; told to the client in plain words (14.1) and confirmed by the client on 2026-11-23 |
| **(hook)** | Owned by another segment or design; segment 10 states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator (8, 9). Never binding |

**The client's brief.** Dungeon generation segment 10: **exploration and information**. It covers:
- fog of war: unknown, seen and remembered state; the reveal rules; symmetric line of sight; how much detail is remembered;
- the map: minimap, full map, accessibility, marks;
- exploration incentives beyond the main path, against the floor clock;
- information items, abilities, spells and traits (reveals, scouting, maps, hints);
- the consolidation of every UI and information surface that other segments handed over, with a surface budget, layering and priority;
- information determinism (the display never holds rules), accessibility, data, validator V18, reports, and hooks to segments 11–12, camp and hub.

The client asked six questions (0.1): (1) should distant points or small gaps in the fog be revealed; (2) should an item, ability, trait or spell reveal specific things in the fog; (3) how to incentivise exploring beyond the main path; (4) should the players have an easily accessible map; (5) an item that hints at special treasure, bought or found; (6) how detailed already-explored areas out of vision should be kept. In this document they are "brief questions 1–6", to keep them apart from this document's client decisions Q1–Q5.

Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; combat is key; contest the client or approved views where warranted.

The client's five answers (Q1–Q5, **agreed**, 2026-11-23) are in 0.2: **quitting mid-floor writes a single suspend slot that is deleted on load, and party knowledge is written forward-only (Q1). A known patrol's forecast is earned by watching, shown as a window, 20 ticks by default and one lap with a trained watcher or Cased, withdrawn by events, and protected by a `PatrolFightRate` gate (Q2). The Ear Trumpet is allowed at M3, stocked only for parties with no Perception-trained member, and only if the solo Listen click share misses (Q3). The Mild Plain auto-pick and auto-Probe read only known patrols and stand down when their hearing reach crosses unexplored squares (Q4). The Lodestring never points at secret caches (Q5).**

**Change prefix.** This design uses **XI-n**. A case-insensitive grep for `\bXI-[0-9]` across the 28 approved documents finds **0 hits** (re-run while writing). The research drafts, the three models, the UI catalogue and the incentives layer each used colliding local XI numbers (XI-1..26 three times, XI-24..30, XI-30..49). This document issues **one register, XI-1 to XI-32** (13), and no id has two meanings (A.6).

**Validator.** **V18 `Information`** is free. V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV and V17 BR. A grep for `\bV18` finds 0 hits (re-run while writing).

**Local id schemes** (each grep-checked as a whole word, 0 hits):
- tensions **XQ1–XQ16**;
- toy failures **XN1–XN15**;
- the information charter **IP1–IP10**;
- the incentive charter **IN1–IN6**;
- Appendix A findings **XP** (fun and play critic), **XB** (balance critic) and **XS** (consistency critic).

Rejected schemes: XT (20 hits), XF (35), XK (19), IT (32), IF (21). This document's client questions **Q1–Q5** are local, as in every parent.

**Names are placeholders** until the client naming pass. Every new name was grep-checked case-insensitively as a whole word across the 28 approved documents (A.5); the check was re-run while writing.

**Numbers in this document are illustrative** unless a rule says otherwise, and are marked *(illustr.)* where they first appear. Every rate comes from the segment 10 toy (8, Appendix B); the simulator owns every published figure. **Exceptions (new lead's call):** the values the client fixed in Q1–Q5 are client keys, not tuning: the forecast horizons of **20 ticks** and **one lap** (Q2), the single suspend slot (Q1), the Ear Trumpet's lane-gap stock rule (Q3), and the secret-cache exclusion (Q5). Changing them returns to the client, **except through the three Q2 cut steps in 4.8.1, which Q2 pre-approves** (14.2) **(lead's call, after the check)**.

**Waves:** **M1** slice, **M2** full run, **M3** between runs (profile, unlocks, ladder), **M4** breadth, **Later** **(settled, MAIN 9)**.

**Abbreviations:**
- F1–F5: floors 1–5.
- "Party knowledge": the core's `PartyKnowledge` (4.1.1); never the display's state.
- "Known patrol": a patrol group the party has seen, or heard through Listen "passing" or F-LISTEN (4.8.1). A heard-only patrol earns only the heard Portal's own "passing" window, never a square-by-square forecast (4.8.1).
- "Never Seen": a square in the Unknown or Chalked state (4.1.1); Q4's "unexplored squares" (4.8.2).
- "Space": a Room node, an Alcove or a corridor segment (FL room identity).
- "Walker" and "human arm": the FL 11.4 walker and the human-session arm of 9.4.
- "BASE": the approved surfaces plus the M1 minimap, the toy's and the simulator's baseline arm (8); qualified as **`XI.BASE`** in the simulator's arm catalogue (SV-22; seeds and validation design 6.2).

---

## 0. At a glance

### 0.1 The lead's answers to the brief (lead's call; accepted by the client by proceeding, confirmed by the client on 2026-11-23)

**The design in one line.** *Sight is symmetric and exact; the shape of a space you have entered is free; everything beyond sight is hearing, a located fact, or a single paid or earned pointer; the party's own map is never withheld; and nothing rewards completion itself.*

#### Brief question 1: should distant points or small gaps in the fog be revealed?
**Answer: yes for architecture inside spaces the party has entered; never for contents; never by distance** (XI-3, XI-4).
- When a party unit enters a Room node, Alcove or corridor segment (FL room identity), that space's walls, wall-bounded floor footprint and Portal kind glyphs become **Chalked** (drawn as chalk line-work). The 1–6-square holes behind pillars, Kegs and Tall pieces (FL 8.1 `CutawayPockets`) disappear, including multi-square gaps.
- Units, containers, pieces, traps, Hunch rims, tells, Spoor, hazard footprints and phases, square kinds, Voids and loot are never drawn as what they are: a piece's or a Void's square is chalked as plain footprint, the same as any floor square (4.1.4). An unfound Hairline is wall in every state, and dead-end stubs of 3 squares or fewer are known only by sight (they are FL's secret cue).
- Seeing into a room from outside **Traces** it: a deliberately gapped chalk node contour on the map only ("shape only; exits unknown"). Nothing farther is revealed by radius or proximity.
- Why: on a rotating isometric board, black holes read as glitches and players spend clicks "clearing" them. The toy measured this as the single largest click credit in the package (up to −9 human clicks per floor for completionists, −4 balanced). Filling contents would leak lurkers and Waylays (ENM 11.2, EP 6.8); filling shape does not (52% of Waylay rooms expose their pocket equally as a black hole). And because line of sight is symmetric, a Chalked square nobody currently sees is exactly a square from which nothing can see the party: a free stealth lesson.
- **Contested (told plainly; confirmed by the client on 2026-11-23):** Model A's "Shell on sight" spends the doorway's mystery and devalues F-ROOM and Overlook for no extra clock saving; Model B's one-square Pocket leaves about 70% of the hole-clearing tax in place. Both are rejected.

#### Brief question 2: should an item, ability, trait or spell reveal specific things in the fog?
**Answer: yes, but narrow, located and single-instance, and only in categories no skill lane owns** (XI-14, XI-22 to XI-27).
- The rule: *sight is symmetric; everything else is hearing, a located FactLedger fact, or chalk.* An item may point at one treasure room (the **Lodestring**), sell an outline of the room graph at camp (the **Surveyor's Sketch**), batch an existing verb (**Walls Have Ears**, Arcanist), stand in for a **missing** skill lane on the Wax Key precedent (the **Ear Trumpet**, only when nobody is Perception-trained; agreed, Q3), or reward watching (**Cased**, Shade).
- **Never (told plainly; confirmed by the client on 2026-11-23):** trap reveals or a trap total (Mechanics lane; TL KF7; Unfound traps are floored anyway); secret radar (FL-17, FL 19.1; the toy showed a stub-drawing map item identifies the secret on 100% of floors); telepathy, Clairvoyance, Arcane Eye or a Spyhole (MAIN 4.3, **decided**; FL 9.1); Detect Magic or Identify (CN 16.7); light or lamp items (no light model, RS 7.6); anything on a scroll (CN 13.7). The Wand of Outlining stays the only unit reveal.

#### Brief question 3: how do we properly incentivise exploring beyond the main path?
**Answer: the reasons already exist; make the decision legible, the trip cheap and the stopping point clean. Never reward completion** (XI-10, XI-11, XI-27).
- Already approved and well budgeted: side-room containers previewed from the threshold (LP 4.2), the 600‰ off-path healing lean, secret caches, Curiosities and Glimpses, the optional elite (+160 XP, intel line), Latch shortcuts, the Treasure count with "best remaining", `EmptyDeadEndRate` 0 **(settled)**.
- Segment 10 adds: **loose ends** marked on the map (no count anywhere, so it never becomes a completion meter); a 0-click **Detour quote** built only from party knowledge; **travel** and a **Next loose end** key that stops before crossing; one **Behind-you line** on the existing arena-entry confirm (the last cheap decision point); an **emptied field** when nothing worth chasing remains; one paid or earned **treasure pointer**; and information paid by combat (F-TREASURE through Yield).
- **Never (told plainly; confirmed by the client on 2026-11-23):** exploration XP; a completion percentage, badge or map-coverage stat (LP-29; Grave Goods "never a score"); a visible floor timer or alarm (STAT 7.2 "exploration has no clock", **agreed**; FL 9.1); a trap total; more off-path loot. **Contested:** Model B's 600‰ off-path loot-rank lean costs lean players 0.22 VP and the worst VP per minute in the toy; it is a push, not a pull. Rejected.

#### Brief question 4: should the players have an easily accessible map?
**Answer: yes, always, free, 0 ticks, never sold or found** (XI-7).
- Minimap from M1 (FL 8.1 data), full map on one remappable key from M2, with layers, core-derived marks, Waymarks and click-to-travel. Every hover read has a keyboard and controller path.
- Why: a 15–20-minute permadeath floor punishes disorientation with clock and death, not discovery, and "explored" is already a rules input (FL 6.8 flee anchors), so hiding it would be a hidden rule (RC1). **Contested (told plainly; confirmed by the client on 2026-11-23):** the Hollow Knight model (buy your own map) is a tax here. Paid information is only ever about the **unknown**.
- **Told plainly:** map-reading seconds are real floor time. The toy's lean arm passed the binding clock line only with about two full-map opens per floor, so the minimap must answer "where next" by itself.

#### Brief question 5: an item that hints at special treasure, bought or found?
**Answer: yes, the Lodestring, pointing at existing treasure only. A separate "special treasure" class is contested and rejected** (XI-22 to XI-25).
- A run-scoped party counter (cap 1, Wax Key model, never a scroll or a deck card), used out of combat on F2–F4 for 1 click, 0 ticks. It marks **one room** with a tier word and "plain cache" or "behind risk" (the cache's own socket class, never the room's traps or guards; 4.6.1), and a string runs along known walls toward it. Never the square, family or identity.
- Its target is the floor's best unfound container notable. **Secret caches are excluded (agreed, Q5)**, as are carried notables, boxed lurkers, Windfalls, the Parting Fare, the signature cache and all of F1.
- Sources: the Tithe Bowl at camp (one per camp), a Curiosity gain, or the fact itself through Yield's Pick the Question once per floor. "Nothing the string can find" does not spend it.
- Why not "special treasure" (told plainly; confirmed by the client on 2026-11-23): a treasure only the hint reveals would break the fixed deck and LootDoorInvariant (RS 6.4), be a CL 6.3 forbidden grant ("more loot"), and make the hint a must-buy tax. The *feeling* of special comes from presentation: a "the string ends here" frame on the loot card.
- It needs a flagged narrowing of "counts only, never locations" (FL-20, LE-15, DH 5.5 G6, RS 6.9, LP 4.12): free surfaces stay counts-only; a paid or earned pointer may name one room (XI-23 ⚑).

#### Brief question 6: how detailed should already-explored areas, out of vision, be kept?
**Answer: exact for what cannot change unseen; "as last seen" for what can, with the age in hover text; projected only from a public deterministic clock; never extrapolated** (XI-5).
- Walls, Portal kinds, open doors (monsters never close them, ENM 12.4), the party's own container and trap acts, Found traps and Hunch zones (monotonic), tells and facts: exact. Closed doors: drawn closed in the Remembered style with the hover "closed 14 ticks ago; monsters open doors".
- Last-seen monsters become **Afterimages**: flat role-glyph paper cut-outs (never models), never moved, keyed to what the unit was doing. A patrol leaves only a "last seen" pip on its drawn route (a patrol plate is 100% stale after one tick); Searching units are removed after about 5 ticks; Stationary and Dormant units persist until their square is seen empty or a disturbance is heard, and turn to a hollow "old" style after about one lap length. **Contested (told plainly):** a uniform 20-tick fade (every draft) fits no activity in the toy.
- Remembered squares are a "faded photograph" (value compression plus a seen-edge line), never a hatch (Rough hatching owns the hatch) and never a board digit (a digit on the board always means a room's clock, HZ 6).

### 0.2 The client's five decisions (agreed, 2026-11-23)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Save integrity: can a seeded floor be scouted by reloading? | **A.** Quitting mid-floor writes a single suspend slot of the current core state; loading it deletes the slot; `PartyKnowledge` is written forward-only; paid knowledge acts are atomic writes (4.12) | B: keep safe-point saves and accept reload-scouting | XI-28 ⚑ |
| Q2 | Patrol forecast strength | **A.** Earned by watching, shown as a window, 20 ticks by default and one lap with a Perception-trained watcher or Cased, withdrawn by events, protected by a `PatrolFightRate` gate (4.8.1) | B: route and last-seen memory only, no forecast. C: an uncapped forecast (cuts patrol contact about 6×) | XI-6 ⚑ |
| Q3 | The Ear Trumpet, which edits the agreed basic-vendor row (TL-25) | **A.** Allowed at M3, stocked only for parties with no Perception-trained member, and only if the solo Listen click share misses (4.5.2) | B: no Listen lane-gap item | XI-26 ⚑ |
| Q4 | Auto-actions and agreed TL-5 | **A.** The Mild Plain auto-pick and auto-Probe read only known patrols and stand down when their hearing reach crosses unexplored squares; closes a measured leak at a cost of at most 1 click in partly explored areas (4.8.2) | B: keep reading true state and accept the leak (0.13 per floor) | XI-6 ⚑ |
| Q5 | The Lodestring and secret caches | **A.** The Lodestring (and every F-TREASURE channel) never points at secret caches (4.6.1) | B: it may, but only while a living party member is Mechanics-trained | XI-22 |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Composite "R": Model A's chassis, Model C's Chalk on entry and Trace on sight, the UI catalogue's surface rules, the numbers pass's fixes; the Dream map as an M3 skin over a plain default (3, 6) | M1–M3 | lead's call; new lead's call for the M3/Later split of the skin (6) |
| 2 | `PartyKnowledge` in the core (Unknown, Chalked, Seen, Remembered; room flags `entered`, `traced`, `interiorChalked`); explored = entered; unmapped defined (4.1.1) | M1/M2 | lead's call; XI-1 ⚑ |
| 3 | One integer symmetric-shadowcasting `CanSee`; no range cap; units never block; the stated corner rule; Downed units contribute no knowledge (4.1.2) | M1 | lead's call; XI-2 ⚑ |
| 4 | Chalk on entry (the wall-bounded footprint, kinds never drawn); stubs by sight only; the sight-shadow reading (4.1.4) | M1 | lead's call; XI-3 ⚑ |
| 5 | Trace on sight, map only; F-ROOM drawn as Chalked; the FL 8.1 amendment in M2 (4.1.5) | M2 | lead's call; XI-4 |
| 6 | Remembered state; activity-keyed Afterimages; projected Stirs (4.2) | M1/M2 | lead's call; XI-5 ⚑ |
| 7 | Minimap (M1); full map, layers, auto-marks, Waymarks, Listen bubbles, fact drawings (M2) (4.3) | M1/M2 | lead's call; XI-7 ⚑ |
| 8 | Incentive charter IN1–IN6; loose ends on the map only; Detour quote; Behind-you line; rooms tally; emptied field (4.4) | M2 | lead's call; XI-10, XI-11 ⚑, XI-14 ⚑ |
| 9 | Information charter IP1–IP10; adopted and rejected reveal sources (4.5) | M2/M3 | lead's call; XI-14 ⚑, XI-32 ⚑ |
| 10 | F-TREASURE, the Lodestring, the Tithe Bowl merged, F-TREASURE through Yield; never a secret cache (4.6) | M3 | lead's call; **agreed, Q5**; XI-22 to XI-25 (XI-23 ⚑) |
| 11 | The Ear Trumpet (4.5.2) | M3, conditional | **agreed, Q3**; XI-26 ⚑ |
| 12 | Travel, Next loose end, To the Downstair, the noticing pre-stop, re-sight narrowing; no auto-explore (4.7) | M2 | lead's call; XI-8, XI-9 |
| 13 | Routes on known squares; `PatrolForecast` earned, windowed, event-withdrawn, gated (4.8.1) | M2 | **agreed, Q2**; XI-6 ⚑ |
| 14 | Auto-actions read known patrols and the never-Seen-crossing gate (4.8.2) | M2 | **agreed, Q4**; XI-6 ⚑ |
| 15 | In-sight and hidden-from pips, noticing bands (M2); watch lines and step tag (M3, conditional); hearing and noise preview (4.8.3, 4.8.4) | M2/M3 | lead's call; XI-12, XI-13 ⚑ |
| 16 | Regions, priority stack, slot model, modes, budgets, board digits, wall projection, card queue, naming (4.9) | M1/M2 | lead's call; XI-15 ⚑, XI-16, XI-17 ⚑, XI-18, XI-19 |
| 17 | Accessibility, Inspect parity, staged disclosure, `uiMemory` (4.10) | M1/M2 | lead's call; XI-20 ⚑, XI-21 ⚑ |
| 18 | Determinism: every rule-bearing pixel a core query; no new seeded stream (4.11) | M1 | lead's call |
| 19 | Suspend-save, the one slot (base plus journal) brought up to date after every committed command (cross-document comparison 2026-11-23, CC-121); forward-only knowledge; atomic paid writes (4.12) | M2 | **agreed, Q1**; lead's call, after the check, for the per-command write; XI-28 ⚑ |
| 20 | Floor Atlas, death-recap map, codex hosting, map paper (6, 7) | M3 | lead's call; XI-29 |
| 21 | V18 `Information`, reports, arms, the segment 10 clock line and self-cut order (9) | M1 tests, M2 | lead's call; XI-30, XI-31 ⚑ |
| 22 | Rejections register: magic mapping with contents, vendor maps, trap and secret radar, telepathy, lamps, divination scrolls, auto-explore, the off-path loot lean, Shell on sight, the Pocket and the rest (4.5.3, 7) | — | told plainly; XI-32 ⚑ |

### 0.4 Intent, goals and anti-goals (new lead's call: added in the template's shape)

**Intent.** Exploration is where the run's decisions are made between fights: which door, how far, when to stop. Segment 10 makes every one of those decisions on honest, legible information, so that a death from the unknown is a risk the player chose, never one the interface hid or invented.

**Goals.**
1. The board never lies: Seen, Remembered, Chalked and Unknown read apart at a glance, by shape and value, at every rotation.
2. The party's own map is always free, always to hand, and answers "where next?" from the minimap alone.
3. Every optional detour shows its known cost and value before commitment, and the player always knows when nothing worth chasing remains.
4. Information that reaches beyond sight is single, located and paid for (by coin, skill, watching or a fight), and never replaces a present skill lane.
5. Segment 10 nets no added minutes or clicks on F4–5 at party 4.
6. Every rule-bearing pixel is a core query; the same seed shows the same facts to every profile.

**Non-goals.** Room graphs, Portals and the minimap data (FL); noticing rolls and patrol behaviour (ENM, EP); trap grades and the trap-sense lane (TL); loot values and placement (7a–7c); fact content (EV); the seed tree, save file format and validator plumbing (segments 11–12); controller polish and screen-reader text (Later).

**Anti-goals.**
- A completion meter of any kind: percentages, badges, coverage stats, loose-end counts, exploration XP.
- A visible floor timer, alarm or pressure meter.
- Selling or withholding the party's own map.
- Category sweeps: radars for traps, secrets, containers or units.
- Auto-explore.
- Memory drawn as present truth, or a forecast drawn as a commitment.
- Hover-only information.

---

## 1. How games handle fog, maps and exploration

Claims marked † come from memory and were not web-verified (told plainly); none is load-bearing for a rule here.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Brogue | Unknown, visible and remembered cells; remembered items kept; magic mapping reveals layout (and, per its wiki, secret doors and traps); auto-explore | A distinct look for remembered state; layout-without-contents as a legitimate reveal | Any reveal that finds secrets or traps (our Mechanics lane) |
| DCSS | Map memory, travel commands, auto-explore that stops on monsters and items; magic mapping by radius; developers call auto-explore a fix for oversized levels | Travel; stop-on-new-thing logic; memory never overwritten by a reveal | Auto-explore (our floors are small and the decisions sit at doors) |
| NetHack† | Remembered glyphs persist; the labelled "remembered, unseen monster" marker | Labelled memory of units (the Afterimage) | Indefinite stale monster memory |
| Into the Breach | Full-information telegraphs | Our combat previews already do this | — |
| XCOM 2† | Concealment and detection tiles; pods activating out of fog criticised | A "this step is a noticing trigger" stop and readout | Surprise activations caused by the interface |
| Invisible Inc.† | Peek at doors; information as a spendable resource under an alarm clock; guard last-known ghosts | Listen as our peek; last-known positions | A visible alarm or timer |
| Darkest Dungeon | Room graph shown, contents hidden; chance-based scouting tied to torchlight | Graph without contents (the Surveyor's Sketch) | Random scouting; light meters (no light model) |
| Hades† | Doors preview their reward | Threshold reads (LP 4.2, the Detour quote) | — |
| Dead Cells† | Fast travel turns backtracking into a choice | Travel | — |
| Hollow Knight | Maps bought per area; Quill updates at benches; criticised as an access tax | — | Selling or withholding the player's own map |
| Slay the Spire† | Full route map; routing is the game | Legible routing between floors (camp, Two Doors) | — |
| Spelunky† | Udjat Eye beeps toward one hidden target | One pointer to one special thing (the Lodestring) | Pointers to everything |
| Binding of Isaac | Treasure Map (layout), Compass (icons), Blue Map (secret rooms) as separate items | Splitting "layout" from "what is where" | The Blue Map analogue (Isaac has no skills; we do) |
| Enter the Gungeon† | Secrets behind cracked walls; blank-spam criticised | One universal tell (FL-17, already approved) | Wall-combing |
| Risk of Rain 2† | Radar Scanner pings all interactables | — | Category sweeps |
| BG3, Pathfinder† | Passive Perception rolls reveal hidden objects; missed-roll FOMO | Our no-roll thresholds (TL CC1) | Hidden rolls on discovery |
| Shadow Tactics† | View cones (asymmetric sight) | — | View cones: our line of sight is symmetric, so "in sight of" is the whole truth |
| Cogmind | Strong map memory; terminals sell intel by category; essay on backtracking as a choice | Intel by category; "loose ends" visibility | — |
| Diablo series† | Always-available auto-map overlay | A free full map | — |
| Elden Ring† | Map fragments reveal terrain, not points of interest | Terrain without contents | — |
| Outer Wilds† | Ship's log surfaces open questions | Loose ends as open questions, not checklists | Completion percentages |
| D&D 5E (SRD) | Passive Perception; Find Traps (exists, not where); Locate Object (direction to a kind) | Locate Object as the hint model; passive thresholds | "Exists but not where" information (a tax); vague divinations a deterministic game cannot honour |
| OSR, The Alexandrian, Sly Flourish | Exploration turns; the Three Clue Rule; floating "secrets and clues" | The FactLedger already is floating clues; secrets already have several cues | Player-drawn maps on a rotating camera |
| Albert Ford's symmetric shadowcasting | A field-of-view algorithm that is exactly symmetric between floor squares | The `CanSee` algorithm (XI-2); its corner conventions must be checked against our corner rule before building | — |

Lessons:
1. Remembered state is exact for static things and visibly aged for mobile ones.
2. Every game that withholds your own map draws complaints; paid maps of the unknown are praised.
3. Pointers and threshold previews drive optional exploration better than percentages.
4. Auto-explore is a symptom fix for large, decision-thin maps.
5. A reveal item that duplicates a skill kills the skill.

---

## 2. The current-systems audit

### 2.1 What the approved documents fix for segment 10 (settled unless a flagged XI changes it)

**Architecture:**
- Rules never live in the display (MAIN 2 anti-goal, **decided**; MAIN 6.2, 6.8).
- Previews use party knowledge only (ABL 8.3, settled principle; DH 17.2; RT 6.12 `PreviewExactness`). Hovering changes nothing (ENM 11.3 invariant 10).
- Information draws nothing (TL CC1 "information is a threshold, never a roll", **agreed**).

**Fog is already rules state:**
- Routers flee to "the first anchor the party has not explored" (FL 6.8, ENM 14.5).
- EV 4.4.2 targets "the next unvisited room".
- The DH 17.13 undo window closes on "a move that revealed a square or a unit".
- TL stores `seenByParty`; EP has `ChipNoLeak`.

**Sight:**
- One symmetric line-of-sight and fog rule (MAIN 4.3, **decided**). No light model (ENM 5.1; RS 7.6 refuses light in M2).
- Portal sight semantics (FL 9.2, FL-7). Tall pieces block sight; Low pieces and Furnishings never do (RT). Spyhole/Peer rejected (FL 9.1).
- Corner cutting is blocked by every movement-blocking square (FL 3.1 `CornerCut`; RT 6.2.1, TR-5), and a Tall piece is a wall for lines.

**Noticing:** one roll per trigger (first line of sight, the 6 and 3 bands, noise, attack); band triggers with no member in line of sight are ignored (ENM 12.2, **settled**). Before this design, ENM 12.2 said nothing about Downed units: its lurker band read "the highest passive Perception among party units with line of sight" (change EP-3). The Downed exclusions that existed were ENM 11.4 Quarry ("non-downed party units") and TL 6.4 trap sense (TL-6); segment 10's Downed rule therefore **changed** the ENM 12.2 lurker-band candidates, now "living, non-Downed party units" (XI-2 ⚑, applied in ENM 12.2; corrected after the check; cross-document comparison 2026-11-23, CC-126).

**Patrols:** "Once seen, the route is drawn and feeds the rest preview" (ENM 12.1, **settled**). Monsters open unlocked doors and never close or lock them (ENM 12.4; FL 3.1).

**Listen:** out of combat, no roll, noise 0, once per door per floor, never Hidden lurkers, passive with a trained member at an auto-walk pause (FL-18).

**Secrets:** one universal tell, no decoys, Tap the wall only at a tell, a dead-end stub of 3 squares or fewer that is always a secret (`HairlineCue`), "never by minimap gaps" (FL-17, FL 19.1).

**Traps:** Found, Hunch, Unfound; no search-for-traps verb; no trap total (TL 6.4, KF7); Probe never makes a trap less safe (KB1).

**Hazards:** visible from reveal, act by a rule, never found by a check; a digit on the map always means the room's clock (HZ 6, 6.18).

**Loot:**
- Counts only, never locations (DH 5.5 G6, FL-20, LE-15, RS 6.9).
- Tier marks on hover only (LE 6.15); "best remaining" only once k ≥ n/2.
- The fixed deck and LootDoorInvariant (RS 6.4); "more loot or gold" forbidden (CL 6.3).
- The Tithe Bowl M3 hook (LE 8.1): "behind risk", never "behind a lock"; unavailable while the party holds a Wax Key; deduplicated through the FactLedger (EV-8).
- Tip-offs held for Later because they point at coin and double FOMO for low-Perception heroes (LP 3.3, 7; cross-document comparison 2026-11-23, CC-126).

**Items:** never knowledge on scrolls or wands (CN 13.7); Identify and Detect Magic rejected (CN 16.7); the Wax Key is the lane-gap precedent (TL-25, **agreed**).

**Readability:** RC1–RC9 and the M2 gates (RS 7.4, **agreed**); BX8 ≤ 3 telegraph layers; plates ≤ 2 marks; the glints-per-screen gate.

**Clock:**
- CMP 7.4 gate, median ≤ 20 min at party 4 on F4–5 (**agreed**).
- `ExploreSeconds` ≤ 3.5 min, `ExploreClicks` ≤ 40 (FL-16).
- TL-38, EP-33, LP-10 and EV-23 lines spent; BR-40's BossMinutes ≤ 7; no segment adds a CMP lever.
- "Exploration has no clock" (STAT 7.2, **agreed**).

**Saving:** the run save is a snapshot of the full core state written at safe points; resolving a Curiosity is a safe point (EV-21); profile memory sits outside the reproducibility key (MAIN 6.6, **decided**).

### 2.2 What the approved documents owe segment 10

| Source | Owed | Answered in |
|---|---|---|
| FL 13 | Fog, minimap UI and the rules-room overlay, Listen UI (occupancy, 9.2), Hairline display, the hearing outline (shut-door prompt, elite vantage risk), interrogation facts, auto-walk (pausing at doors, 6.10), Overlook (M3), the Stratum Card line | 4.1–4.3, 4.5, 4.7, 4.8, 5 |
| FL 6.9 | Travel-to-explored-square auto-walk in M2 | 4.7 |
| FL 8.1 | Minimap data and cutaway | 4.3.1, 4.9.7 |
| RS 13 | Camp card, Two Doors cards, Stratum Card, dream track, Lore reveal | 4.9.8, 5 |
| EP 13 | Formation chip, Read card, hold squares, marked tell, Spoor, rest-preview movers, Hedgehog badge | 5 |
| TL 6.23 | Trap glyphs, Hunch rim, previews, lock card, prompts, trap count line, codex | 5 |
| HZ 6.18 | Footprints, due overlay and digit, Dream card, rule cards | 4.9.6, 5 |
| RT 6.12 | Verb and cover previews, `PreviewExactness` | 5, 9.2 |
| DH 17 | Danger band, warnings, lethal tags, death recap, floor-entry card | 5 |
| LE 6.15 | Stratum Card order, floor strip, tier marks, plate loot badges, the Parting Fare offer UI, ledgers, stings | 4.9, 5 |
| LP 4.12 | Container hover, quiet summaries, wall-set mark, Windfall set | 5 |
| IR 6.1 | Item card, verdict, offer frames | 5 |
| EV 4.12 | Curiosity card, facts on the map, chips, Grace chip, codex; the worst-case card fit as a segment 10 acceptance item | 4.3.6, 4.10, 5 |
| BR 4.16, 4.17 | Two-tier plate, banner, Playbill, arena-entry confirm, Phase Score, Boss Ledger | 4.4.4, 5 |
| ENM 13, 15.8 | Turn log, intent read, morale overlay, loot summary, leftovers marker, Floor-info panel | 5 |
| ITEMS 10.9 | Trader marker | 4.3.3, 5 |
| CMP 7.x | Companion card, next-camp preview, coverage strip | 5 |
| C 15 | Conditions row, greyed reasons, Revealed outline | 5 |

### 2.3 Tensions and resolutions

| Id | Tension | Resolution |
|---|---|---|
| XQ1 | No doc specifies the line-of-sight algorithm, range, corner rule or unit blocking | XI-2 fixes all four; the corner rule is aligned with FL 3.1 and TR-5 as far as sight allows, and the difference is stated |
| XQ2 | FL 8.1 "never unexplored rooms" vs F-ROOM ("one unexplored room's layout", M2) and Overlook's mapped layer | XI-4 amends FL 8.1 in **M2** together with F-ROOM |
| XQ3 | "Explored" and "unmapped" are used three ways (FL 6.8, EV 4.4.2, FL 9.1, ENM 14.6) | XI-1: explored = entered (FL 6.8, ENM 14.5, EV 4.4.2); unmapped = not entered and interior not Chalked (FL 9.1 Yield, ENM 14.6 F-ROOM target) |
| XQ4 | Brief question 1's gap fill vs secrets, lurkers and symmetric line of sight | Chalk on entry: shape only (the footprint, never a square's kind, so a Void is plain chalk), stubs by sight only, contents never |
| XQ5 | ENM 12.1's settled "route drawn" would leak unexplored Circuit rooms if drawn through unknown space | XI-6: drawn on known squares only with "continues" stubs ⚑ |
| XQ6 | TL 6.6 and auto-Probe read "the patrol forecast" without saying whose | XI-6: known patrols only, plus the never-Seen-crossing gate on auto-actions ⚑ (TL-5; **agreed, Q4**) |
| XQ7 | Brief question 5's hint vs counts-only, LootDoorInvariant and the LP Tip-offs hold | XI-23: one room, tier word, notables only, class-blind; flagged narrowing |
| XQ8 | Treasure total shown, trap total refused | One principle: totals only for what is worth chasing (IN5, codex line) |
| XQ9 | "Exploration has no clock" vs a real floor clock | Pulls, never pushes; costs land on real minutes and clicks, policed by XI-31 |
| XQ10 | Auto-explore not mentioned anywhere | Rejected; travel and Next loose end instead |
| XQ11 | Remembered monsters vs RC1 "no lying UI" | Afterimages as labelled memory, keyed to activity |
| XQ12 | Surface overload: no global budget exists | XI-15 slot model, priority, budgets, combat suppression |
| XQ13 | Naming drift: count line, floor strip, Treasure strip | XI-19 canonical names |
| XQ14 | Hover-only surfaces vs remappable input and a later controller | XI-20 Inspect parity |
| XQ15 | Deterministic floors plus a rich knowledge display make reload-scouting trivial | XI-28 suspend-save (**agreed, Q1**) |
| XQ16 | Main-path identity must never leak, yet several surfaces want role splits | Tallies, ties, rankings and lines are never split or ordered by hidden role, nor by `roomId`, `portalId` or `socketId`, which follow FL's main-chain order; ties use a role-blind geometric key (XI-8, XI-11, XI-22; after the check) |

---

## 3. Information models

### 3.1 The models

- **Model A, "Clear Map".** Reveals a room's whole shape on sight, keeps a free map, and ships two narrow items. Best chassis; least mystery; most explore-mode clutter (27.6% of screens over 12 marks).
- **Model B, "Earned Knowledge".** Reveals only seen squares plus a one-square Pocket and sells information through five instruments and a set piece. Most mystery; worst on clock and value in the toy; about 15 flagged changes including one to decided MAIN 4.3.
- **Model C, "Dream Cartography".** Sketches on entry, traces on glimpse, and draws a diegetic dream map in nine themed styles. Best tone and best reveal rule; two drawing systems and every gate run twice (test cost 1.6–1.8×).
- Layers merged into all three: the UI surface catalogue, the incentives layer, the new-mechanics notes (N1–N13) and the numbers toy.

### 3.2 Judges' scores (mean of three judges, 1–10; judge ranges in brackets)

| Criterion | A "Clear Map" | B "Earned Knowledge" | C "Dream Cartography" |
|---|---|---|---|
| Exploration fun | 6.0 | 7.0 | 8.0 |
| Readability | 8.7 (8–9) | 6.0 | 5.3 (5–6) |
| Meaningful decisions | 7.0 | 7.3 (7–8) | 6.7 (6–7) |
| Pacing and clock | 8.0 | 4.0 | 7.0 |
| Theme expression | 6.0 | 6.0 | 9.0 |
| Determinism and testability | 9.0 | 8.0 | 7.7 (7–8) |
| Fit with approved docs | 8.0 | 5.0 | 6.7 (6–7) |
| Low build cost | 8.0 | 4.7 (4–5) | 3.0 |
| **Sum of averages** | **60.7** | **48.0** | **53.4** |

### 3.3 Verdict (lead's call; all three judges)

- **Composite "R" is the design.** Model A's chassis (knowledge model, `CanSee`, charter, surface register, V18 harness), Model C's Chalk on entry and Trace on sight, the UI catalogue's surface rules, and the numbers pass's fixes.
- R is the only configuration that passes the binding F4–5 clock line on the lean arm (8.2), keeps explore clutter inside the gate (5.5% over 12), has the lowest test cost (0.9×) and the fewest flags.
- **Told plainly (after the check):** R **fails the combat clutter gate in the toy** (8.9% over 12 against the 5% gate; BASE 2.6%). The combat suppression of 4.9.4 is the fix, but it was not re-measured (8.6). The combat gate is a **pre-M2 sign-off check**, and the levers of 8.4 apply in order on a miss.
- C's paper-and-chalk grammar is kept in one style. The Dream map is an M3 skin over a plain default, gated (6).
- **Told plainly:** Model A's own reveal rule (Shell on sight) and Model B's Pocket rule are both rejected.

---
## 4. Rules in full

### 4.1 The fog model

#### 4.1.1 `PartyKnowledge` (change XI-1; lead's call; M1 states, M2 traced and fact sources)

Held in the core, per floor, in the run snapshot and the suspend slot (MAIN 6.6; 4.12) and rebuilt by replay of the command log.

- **Square state**, one of:
  - **Unknown**;
  - **Chalked** (architecture known, not currently and never yet seen);
  - **Seen** (in the line of sight of a living, non-Downed party unit now; 4.1.2);
  - **Remembered** (seen before, not now).
  Precedence Seen > Remembered > Chalked > Unknown. "**Never Seen**" means Unknown or Chalked. Square knowledge is monotonic. **The one stated exception (lead's call, after the check):** the derived query `ChalkOutline(space)` closes a Chalked outline across the mouth of a dead-end stub of 3 squares or fewer (4.1.4). The closing line is a drawing, not a square state: the stub's squares stay Unknown, and the line is withdrawn when the stub is Seen.
- **Room flags** (Room node, Alcove, corridor segment): `entered`, `traced`, `interiorChalked`, each with a source (`entry`, `glimpse`, `F-ROOM`, `overlook`, `sketch`, `treasure`; `treasure` covers every F-TREASURE channel, the Lodestring included).
- **Memory records:** `portalLast{state, tick}`, `objectLast{kind, state, tick}`, `trapGrade` (TL, monotonic), `stirLast{phase, tick, projectable}`, `afterimage{unitId, groupId, square, activity, awareness, tick, state}` (the square is the unit's square at the last tick it was Seen; `state` per the 4.2 table), `listen{doorId, result, tick}`, `routeSeen{groupId, segments[], observedTiming[], earnedBy, trainedWatcher, earnedTick, horizon, withdrawnTick?, lastSeen{square, tick, activity}}` (4.8.1), `sketchEdges[]{roomA, roomB}` (the Surveyor's Sketch's node-to-node edges: never Portals, never loose ends, never travel edges), a FactLedger view.
- **Display-only, never read by rules:** Waymarks, camera, open panels, layer toggles (4.3.4, XI-7).
- **Events:** `KnowledgePass` (4.1.3), `SquaresSeen`, `SpaceEntered`, `SpaceTraced{source}`, `SpaceChalked{source}`, `MemoryUpdated`, `AfterimageSet`, `AfterimageCleared`, `HeardRecorded`, `FactDrawn`.
- **Entered (lead's call, after the check).** A space's `entered` is set when a living, non-Downed party unit **completes any step** (each square of travel counts), voluntary or forced, or stands up from Downed, on a **non-Portal square owned by that space** (FL room identity). Portal squares never set `entered` and never Chalk, whichever room FL-13 assigns them to; nor do the BR doorway-band squares that FL-13 assigns to the arena, so standing in a doorway never enters the room beyond, and the arena is entered only past the arena-entry confirm. A Downed unit shoved or carried into a space enters nothing. Tests in V18t: doorway standing, the arena doorway, a Downed unit shoved into a room.
- **Readings (owners confirm; no text change if confirmed):**
  - **Explored = `entered`:** FL 6.8 and ENM 14.5 flee anchors, and EV 4.4.2 "next unvisited", only. Chalked or Traced never counts, so no item, fact or glance moves a flee anchor.
  - **Unmapped** (FL 9.1 "Yield prefers unmapped rooms"; ENM 14.6 F-ROOM's "one unexplored room's layout") = not entered and interior not Chalked. **The F-ROOM target is an unmapped room** (corrected after the check: ENM 14.6 was listed under both readings), so F-ROOM never targets a room Overlook or an earlier fact has already Chalked. A Traced room or one outlined by the Surveyor's Sketch is still unmapped, so F-ROOM keeps its value.
  - **DH 17.13 "revealed a square"** (Later) = a `SquaresSeen` turning an Unknown or Chalked square to Seen, or a new unit sighting. Chalking alone does not close the undo window.
- **"Unexplored" in plain wording (lead's call, after the check; told plainly; replaces the draft's Unknown-only reading).** Q4's "unexplored squares" means **never-Seen** squares: Unknown **or Chalked**, but not Remembered. Chalked squares were by definition never seen (pockets behind pillars, rooms Chalked by F-ROOM or Overlook), and a group can stand there exactly as in Unknown space, so the Unknown-only reading would have narrowed the client's safety answer. The XI-1 term "explored" (= entered) stays a room-level rules term; the auto-action gate (4.8.2) is square-level and reads never Seen.

#### 4.1.2 Line of sight: `CanSee(a, b)` (change XI-2 ⚑; lead's call; M1)

- **One function** for fog, noticing (ENM 12.2), lurker rolls, trap detection (TL 6.4), Hairline auto-reveal (FL-17), targeting sight checks, Grille sight, the C 18.7 Revealed outline, flee-by-breaking-line-of-sight and the cutaway's "room holding a square in line of sight". Monsters use it per unit.
- **Algorithm:** integer symmetric shadowcasting (Albert Ford's formulation), square centre to square centre. Verify Ford's wall and corner conventions against the property tests before building (told plainly).
- **No range cap** (no light model; rooms and corridors of 12 or fewer squares bound it).
- **Blockers:** walls; closed Door and Locked; latched Latch; drawn Drape; unfound Hairline; the Bossgate; Tall pieces at full state; the Squeeze's 90° bend end to end.
- **Passes:** Arches, corridors, open Portals, Grilles (sight and noticing only, FL-7), Low pieces, Furnishings, Smash debris (RT; fog recomputed once).
- **Units never block sight** (lead's call; consistent with "units never give cover"). Recorded as a Later client question (14.2).
- **Corner rule (told plainly; flagged reading for the FL and RT owners).** Movement may not cut a corner past **one** blocker (FL 3.1, TR-5). Sight is blocked on a diagonal only when **both** orthogonal neighbours of that diagonal block sight (walls, closed Door-kinds, Tall pieces); sight may glance past a single corner, as shadowcasting naturally does. The difference is deliberate: a unit can see a square it cannot step to diagonally. Test `LosVsLineAgree`: anything RT treats as a wall for lines also blocks `CanSee`.
- **Symmetry and blockers (lead's call, after the check).** `CanSee(a, b)` is defined, and symmetric, between **transparent** squares. A blocking square (wall, closed Door-kind, Tall piece) sees nothing; it becomes **Seen** when Ford's algorithm lights it from a transparent origin square, so walls and closed doors are drawn and remembered by that rule. `LosSymmetric` tests pairs of transparent squares only.
- **Who contributes knowledge:** living, **non-Downed** party units, companions included. The precedents are ENM 11.4 Quarry ("non-downed party units") and TL 6.4 trap sense (TL-6); ENM 12.2 itself is silent, so this **changes** ENM 12.2's lurker-band candidates ("the highest passive Perception among party units with line of sight", change EP-3) to exclude Downed units (XI-2 ⚑; citation corrected after the check). This is a reading of MAIN 4.3 (**decided**, unchanged): symmetry is a property of the geometric `CanSee`; a monster can still see a Downed unit.
- **Recomputed** in every `KnowledgePass` (4.1.3).

**Queries over party knowledge (lead's call, after the check).** `CanSee` reads true geometry, so no "party knowledge only" query may call it across squares the party has never seen. Two functions over `PartyKnowledge` serve those queries:
- **`KnownCanSee(a, b, mode)`** is shadowcasting over known geometry. Known blockers block; a Remembered closed Portal blocks; never-Seen squares (Unknown, and Chalked squares whose pieces are not drawn) are treated by `mode`:
  - **pessimistic** (transparent and passable): used where a wrong "no" would be unsafe;
  - **optimistic** (blocking): used where a wrong "yes" would overclaim.
- **`KnownNoiseReach(source, n)`** is noise reach over known geometry, with the same two modes.

| Query (4.11) | Function and mode |
|---|---|
| `NoticingPreStop` (4.7) | `KnownCanSee` pessimistic |
| Auto-action gates (4.8.2) | `KnownCanSee` and `KnownNoiseReach` pessimistic |
| Forecast withdrawal on heard noise (4.8.1) | `KnownNoiseReach` pessimistic |
| Stationary Afterimage clear on a heard disturbance (4.2) | `KnownNoiseReach` pessimistic |
| Forecast windows (4.8.1) | `KnownCanSee` optimistic |
| Watch lines and noticing bands (4.8.3) | `KnownCanSee` optimistic |
| Noise preview (4.8.4) | `KnownNoiseReach` optimistic for "would draw", plus the fixed "unknown things may also hear" line whenever the pessimistic reach crosses a never-Seen square |

Both functions are in the `KnowledgeLeak` harness (9.2): changing an unseen wall, an unseen open door or an unseen piece leaves every output above unchanged.

#### 4.1.3 The per-square pass: `KnowledgePass` (lead's call; M1; scope and re-runs after the check)

**When it runs:** after **every** world change that can alter sight or knowledge: each party unit's step (the leader first, then followers in party order, one pass per unit step), each monster step, and each Portal or piece state change (whoever makes it, monsters included), Smash, `SecretFound`, `LatchLifted` and (M3) Overturn. In fixed order, integer and draw-free (noticing keeps its approved EP-3 keyed hashes):
1. FOV (`CanSee` from every living, non-Downed party unit);
2. knowledge update (Seen, Remembered, Chalk on entry, snapshots, Afterimage sets and clears);
3. noticing triggers;
4. trap detection;
5. Hairline auto-reveal;
6. interrupt evaluation (FL 6.9 and 6.10, TL-7, EP-17, XI-9, the noticing pre-stop of 4.7).

- **Re-run:** if step 5 changes geometry (a found Hairline, a lifted Latch), steps 1–3 run once more before step 6.
- **Combat start:** if step 3 starts combat, steps 4 and 5 still run, then the pass stops; combat's own order takes over.
- One pass, one order, so replay and display agree. `FogEqualsRules` and `ReplayView` include follower steps, monster steps, monster-opened doors, a Hairline found mid-pass and combat starting at step 3.

#### 4.1.4 Chalk on entry (change XI-3; lead's call; M1)

- **Trigger:** the space's `entered` flag is set (4.1.1): a living, non-Downed party unit completes a step on a non-Portal square of a Room node, Alcove or corridor segment (FL room identity). Standing in a doorway Chalks nothing.
- **Drawn:** walls; the space's **whole wall-bounded floor footprint as uniform chalk**, including the squares under pieces and Void squares (lead's call, after the check); every non-secret Portal frame with its **kind** glyph (state only where already known).
- **Never drawn (told plainly; narrower than Models A and C):** what stands on or fills a square: units; containers; Tall, Low and Furnishing pieces (an RT owner request may later add Tall "blocks sight" outlines); square kinds (water, web, Rough); hazard footprints and phases (HZ "visible from reveal" means sight); **Voids as Voids**; trap and Hunch marks; tells; Spoor; lock state; loot. A pillar's square, a pit and a plain floor square look the same in chalk. **Why the footprint, not the walkable outline (after the check):** a walkable outline would leave an Unknown island exactly where each piece, nook and Void is, which would give away lurker nooks and Void positions and make pillars and pits look alike as fog towers. Chalk claims shape only; travel confirms each square by sight before stepping on it (4.7).
- **Stub rule:** any dead end of 3 squares or fewer is known **only by sight**; its squares stay Unknown, and `ChalkOutline(space)` closes the outline across the stub mouth in the ordinary chalk wall line until the stub is Seen (the stated exception in 4.1.1; told plainly: chalk can show wall where a short stub is, and only sight is truth). This holds for every non-sight source (F-ROOM, Overlook, the Surveyor's Sketch). Without it, entering a space would draw FL's secret cue without sight; with a distinct "outline ends" mark, the mark itself would be the cue.
- An unfound Hairline is wall in every state. Nothing crosses a wall square, a Portal or a space boundary.
- **The sight-shadow reading.** A Chalked square nobody currently sees is exactly a square from which no unit can see the party (symmetric line of sight). Its hover reads **"out of sight (not out of hearing)"**. It is never called "safe" or "unwatched", because noise still alerts and a Hidden lurker may stand there (told plainly). It is drawn by the Chalked style itself, never by a new floor hatch.
- **Rejected:** corridor gap fill between two seen squares (the fill-or-not decision would leak hidden branches); radius or distance infill; display-side gap fill.

#### 4.1.5 Trace on sight (change XI-4; lead's call; M2)

- When any square of a Room node becomes Seen from outside it (through an Arch, open Door, Grille or drawn-back Drape), the room becomes **Traced**: its macro-grid node contour is drawn **on the map and minimap only**, never on the board.
- The contour is one style (6, line-style registry): a **solid chalk stroke with gaps at every wall-run end**, like an unfinished chalk sketch, never dotted, so it never reads as a sealed room. The gaps never correspond to real Portals. Hover: "shape only; exits unknown".
- Never interiors, Portals, pieces or units. A node reachable only through an unfound Hairline can never be glimpsed, and no other source may Trace or Chalk it (V18g `SketchNeverSecretNode`).
- F-TREASURE (4.6.1) Traces its target room if it is Unknown (source `treasure`).
- F-ROOM (M2) is drawn as interior Chalked. **FL 8.1** "never unexplored rooms" becomes "never Unknown rooms; Traced and Chalked rooms in their own styles". This must ship in **M2** with F-ROOM (told plainly; the drafts had it at M3).

### 4.2 Remembered state (change XI-5 ⚑; lead's call; M1 static rows, M2 the rest)

| Thing | Remembered as | Changes while unseen? Shown how |
|---|---|---|
| Walls, the floor footprint, Portal kinds, Landmarks | Exact | Only by party acts or seen events |
| Square kinds, Voids, hazard footprints (once Seen) | Exact | A Tide's level is a Stir: projected (below) |
| Portal **open** | Exact (monsters never close, ENM 12.4) | — |
| Portal **closed**, latched, drawn | Drawn as last seen in the Remembered style | Hover: "closed 14 ticks ago; monsters open doors". No "?" reachability logic (computing it from partial knowledge invites misreads) |
| Containers, locks, disarms | Exact for party acts; tier word only if hovered when seen | Windfall bundles: as last seen |
| Found traps, Hunch zones | Exact grade (monotonic, TL 6.4) | Spring state only if seen or heard by the reserved cue |
| Stir phase and its digit | **Projected** as a **solid** numeral in the Remembered grade with a "projected" pip as its only marker (never hollow: among segment 10's marks hollow means aged memory only, and on room-clock digits the approved Windfall count digit is hollow while Dormant), only when `projectable`; else "?". `projectable` is set at reveal from the hazard record alone: true when the schedule was announced at reveal and the room has no Cord or Stoke input (M3), whatever monsters are present (lead's call, after the check) | It stays the room's clock, so HZ 6's digit rule holds. Travel treats a Remembered hazard showing "?" as avoid-or-stop (4.7) |
| Units and groups | **Afterimage** (below) | Never moved by core or display |
| Patrol routes | Seen route segments on known squares, "continues" ends where they leave known space | Forecast per 4.8 |
| Listen results | Listen bubble on the door (4.3.5) | Turns to the hollow "old" style after 20 ticks *(illustr.; `listen.oldAfterTicks`)* |
| Facts, leftovers, the trader, a Curiosity left open | Permanent for the floor, fact marks with a "told" pip | No |

**Afterimages.**
- A flat paper cut-out of the unit's **role glyph** (never a figure) with a **dashed cut line** round its edge, on a torn-paper base, standing on the walkable square where the unit was last Seen; below unit value tier, with the last awareness and activity glyph. Never a unit model. **No numerals (after the check):** a group of 2–4 Afterimages on adjacent squares shows as 2 stacked, offset cut-outs, and 5 or more as 3 *(illustr.; `afterimage.groupCollapse`)*; the exact count is in hover and Inspect. A count digit would break XI-16.
- **Distinct from the EV M3 figure frame (after the check):** the figure frame is also "a flat, paper-cut figure below unit value tier" (EV 4.11), but it is a cropped figure in a niche and never stands on a walkable square. The Afterimage is a role glyph with a dashed cut line on a walkable square. "Afterimage vs figure frame" joins the role-ID and greyscale gates (5, 6).
- The Shade's False Trail "ghost marker" must use a different shape (request: a hollow double-footprint ring; Shade owner and art).
- **States (lead's call, after the check; one static look each, no alpha ramps, never animated):**

| State | When | Look |
|---|---|---|
| **Fresh** | Set this tick up to `afterimage.freshTicks` (5 ticks, *illustr.*) | Full cut-out in the Remembered grade |
| **Persisting** | Stationary, Posted or Dormant units after Fresh, until Old | The same cut-out, its base dimmed one value step |
| **Old** | Stationary, Posted or Dormant units after one lap length (`afterimage.oldAfterTicks`, 60 ticks, *illustr.*) | Hollow cut-out ("last seen 74 ticks ago" in hover) |
| **Short** | In combat, a combatant in the fight room that left line of sight within `afterimage.combatKeepRounds` (2 rounds, *illustr.*) | A half-height cut-out |

- **Keyed to activity (lead's call; the toy measured staleness):**
  - **Patrolling:** no plate; a "last seen" pip on the drawn route.
  - **Searching, Alerted, Pursuing:** Fresh, then **removed** at 5 ticks *(illustr.)*, with no in-between look.
  - **Stationary, Posted, Dormant:** persists (Fresh, Persisting, then Old) until its square is Seen empty, the unit is seen elsewhere, or a disturbance the party heard could have reached it (`KnownNoiseReach`, pessimistic; 4.1.2). One age step, no digit.
  - Dead, Yielded or routed-and-seen-gone units leave none.
- **In combat:** one round counts as one tick for these rules. Combatants in the fight room that left line of sight within the last 2 rounds keep a Short Afterimage (the memory most needed mid-fight: a unit that stepped behind a Tall piece or out a door); after 2 rounds the normal activity rule applies. Afterimages of groups outside the fight room drop to the map and hover; the hearing outline and arrival arrows already cover them.
- Age always in hover text, never a board digit (XI-16).
- An F-KEY carrier that is Patrolling gets its Keybearer pip on the route's "last seen" pip.
- An Afterimage always shows the **last-seen** activity and awareness and is never updated while unseen (IP5). The draft's "Cased groups keep exact activity" is withdrawn after the check: it was either redundant or telepathy.

**Visual grammar of the four states** (change XI-17; art leads; RS owner adds registry rows):
- **Seen:** full art and value; a thin crisp **seen-edge line** marks the boundary of the current union line of sight.
- **Remembered:** the same art, desaturated and contrast-compressed toward mid-value ("a faded photograph"). **The grade applies to tiers F and N only** (floor, architecture, knowledge state; 4.9.2). Hazard, trap, Hunch and Portal marks (tiers H, R, W) keep their full pattern and rim strength, compressed by at most one value step and **never desaturated**, because in the Law themes saturation is exactly what separates a hazard square from off-key dressing (RC4) (lead's call, after the check). Floor patterns (Rough, water, web) stay intact, only compressed. No added pattern: Rough hatching owns the hatch (HZ 8 registry). If a theme cannot meet the Seen-vs-Remembered value ratio, the fallback is a faint paper-grain overlay **on the walls only**, never a floor hatch.
- **Chalked:** chalk line-work on the dark floor plane, no floor texture.
- **Unknown:** a theme-tinted **fog bank at wall-top height**, opaque and static, never floor-plane darkness, so it never reads as the RC6 Void backdrop (a shaft below the floor). **Cutaway (after the check):** the fog bank takes part in the FL 8.1 cutaway: wherever it would hide a Seen or Remembered square or a unit from the camera, it drops to a low cap. Inside an entered space nothing stays Unknown except a stub behind its closing line, which draws as ordinary exterior fog (4.1.4), so no fog pillar ever stands in a room.
- Every state is distinct by shape and value, never hue. The greyscale and role-ID gates add the pairs "Unknown vs Void", "Afterimage vs unit", "Afterimage vs figure frame", "Seen vs Remembered" (with a minimum value-range ratio, *illustr.*), "Chalked vs Void backdrop", "Remembered vs Chalked" and, per Law theme, "Remembered hazard vs Seen hazard vs environment", each run per theme at 4 rotations; `TemplateReadability` checks the fog-bank cutaway at 4 rotations.

### 4.3 The map (change XI-7 ⚑; lead's call; minimap M1, the rest M2)

#### 4.3.1 Minimap (M1)
- HUD top-right, about 18% of screen width *(illustr.)*, north-up with the camera wedge (FL 8.1); a setting rotates it with the camera. Landmark icons, not words, so it reads at all rotations.
- **Form (lead's call, after the check):** FL 8.1's north-up **schematic room-node graph**, not a square-level plan (the plan is the full map's centre view, 4.3.2). Each node is filled by its room knowledge in one of three value fills: **entered** (solid), **interior Chalked** (chalk fill), **Traced** (the gapped contour only, M2).
- **Reduced mark set:** Portal state glyphs at loose ends only, the Downstair, the party, loose-end marks, Listen bubbles (as a door-side dot), Waymark tacks, fact marks (no told pips) and the Lodestring mark (M3); Precinct fill tints.
- **Glyph floor:** no minimap glyph below 10–12 px at 720p *(illustr.)*; the glance test and a `CardFit`-equivalent check run on the minimap at 720p and 1280×800.
- Never: Unknown rooms, unfound Hairlines (a wall leaves no gap), non-party units, **Afterimages in any state** (they live on the board and the full map).
- **No count of anything on its frame** (told plainly; contests the UI catalogue and Model A). A north arrow only.
- In combat it collapses to a one-room thumbnail; in arenas it hides behind the pull-back zoom (BR 4.16).
- **Duty:** it must answer "where next?" by itself (loose ends, the Downstair, marks), so the full map is optional by design.

#### 4.3.2 Full map (M2)
- One remappable action (default M; controller View, Later). 0 ticks, 0 draws: exploration is turn-stepped and nothing advances while it is open. The board dims behind it.
- **Centre:** a top-down plan of every Seen, Remembered and Chalked square plus Traced contours; a tab switches to FL's schematic room graph (also the controller-friendly view).
- **Left: layers** (each with a legend in board glyphs): **Rooms** and **Marks** on by default; Patrols, Hearing, Traps and Hunch, Loot (container presence and opened state; a tier word only on a container hover, which counts as LE 6.15's container hover, LE owner reading), Facts. Layer toggles persist across runs as a display preference.
- **Right: room inspector** for the hovered room: entered, Chalked or Traced (with source if told), Listen result and age, containers seen and opened, facts, the Detour quote, the CMP next-camp line where CMP places it.
- **Bottom: the Waymarks tray.**
- Interactions: click a known square, room or mark to travel (4.7); right-click places a Waymark; scroll zooms; controller D-pad jumps room to room, bumpers cycle layers (in map mode Inspect moves to the held left trigger; the binding table in 4.10).

#### 4.3.3 Auto-marks (core-derived)
Loose end; heard door (bubble); seen unopened container; leftovers and flee piles (ENM 15.8); the trader (ITEMS 10.9); Found trap; Hunch zone; found Hairline tell; padlock kind; F-KEY carrier and lock; a Curiosity left open; Landmarks; the Downstair; Forecourt; every fact (4.3.6).

#### 4.3.4 Waymarks (player marks)
- Up to 8 per floor *(illustr.)*, optional 12-character label. **Look (lead's call, after the check):** every Waymark stands on one reserved player **tack** base (a round-headed tack used by nothing else); on the board it shows as the bare tack, and its inner shape shows on the map and in hover. The 6 inner shapes are chosen by art from outside the pattern registry (V18s `WaymarkGlyphDistinct`; greyscale gate row). The drafts' star, cross, question mark, eye, skull and arrow are withdrawn: each already carries a rule (the notable's star, Hunch "?" ticks, the marked tell's eye tick, lethal tags, arrival and fall arrows).
- **Placement:** on the map by right-click; in the world by **modifier-click** (default Shift-click), never hold-click, which stays travel; by keyboard or controller with the Waymark binding on the focused square (4.10). Wiped at the stairs, kept in the Floor Atlas (XI-29).
- Stored in a **UI annex of the run save, outside the reproducibility key**, never read by rules, the simulator or validators (V18t `WaymarksOutsideKey`). **Told plainly:** this is a flagged **addition** to MAIN 6.6 (**decided**), not a reading; the `curiosityMemory` precedent is profile memory, and MAIN 6.6 defines the run save as a snapshot of core state.

#### 4.3.5 Listen bubbles (lead's call; M2)
- A Listen result (FL-18; Lockside Listen; F-LISTEN; joiner footsteps when heard through a door) is drawn as a small pencil-style bubble tethered by a **fine hairline** (6, line-style registry) to the **Portal it was heard through**, at a fixed outward offset, **never at the room's true position or size** (V18r `BubbleNeverPositioned`; cross-document comparison 2026-11-23, CC-122).
- **Contents (completed after the check):** the Listen glyphs (moving or still footsteps); size band and "passing" if trained; the **activity word** and the **watched Portal** when trained, if EP-9 and EP-12 are ruled (EP 13, pending the FL-18 requests); a **mechanism** glyph when Listen hears a device (TL 13); a **Stir-tell** glyph when Listen hears a hazard's tell, such as the Dare room's Stir (HZ 13); the tick in hover. An empty result is an empty bubble whose rule card says "Listen never hears Hidden things". At most one per Portal; the newest replaces the old.

#### 4.3.6 Fact drawings (lead's call; EV and ENM owners confirm the F-ROOM reading)

| Fact | Drawn as |
|---|---|
| F-TRAP | The Found trap glyph, as if seen |
| F-SECRET | The tell glyph on its wall face, plus a Portal glyph on the map |
| F-ROOM | Interior Chalked (the footprint, Voids included as plain chalk; stubs still need sight) |
| F-KEY | Keybearer pip on the carrier's plate, Afterimage or route pip, plus the lock's padlock badge |
| F-LISTEN | A Listen bubble only (never an Afterimage, which implies a seen position) |
| F-CACHE (pending EV-7) | Container hover card content |
| F-BOSS | Playbill "?" lines filled (BR 4.12) and a Floor-info row |
| F-TREASURE (M3) | Lodestring mark on the room node (the room Traced if Unknown), tier word and qualifier in hover; cleared when the target container is opened |

Every fact-sourced mark carries a small **"told" pip** (a speech dot) so learned is distinguishable from seen (RC1).

### 4.4 Exploration incentives

#### 4.4.1 The incentive charter (change XI-14 ⚑; lead's call; binding on later content)
- **IN1 Pulls, never pushes.** Leaving the path is rewarded by what is there, never punished by a timer (STAT 7.2; FL 9.1).
- **IN2 Visible before commitment.** Cost and value of a detour are legible first (FL 6.9 "detours are legible before commitment").
- **IN3 Single located instances, never category sweeps.** One room, one trap, one treasure.
- **IN4 Combat pays information.** Optional fights buy routing (Yield, the elite intel line, Spoor).
- **IN5 Count only what is worth chasing.** Treasures show k / n (bounded optional value; the count is a stopping signal); secrets show s from F2 (optional finds); traps show no total (floored dangers; a total invites sweeping, KF7). The codex states this in one line.
- **IN6 Nothing rewards completion, and nothing is a completion meter.** No exploration XP, no percentage, no badge, no map-coverage stat, no loose-end count. **One named exception (lead's call, after the check; told plainly; flagged with XI-11):** the approved FL-20 party-bar tally and LE 6.15's floor-strip rooms cell, given in 4.4.4 as "rooms e / t". Those approved documents fixed a rooms tally in the strip; segment 10 only defines it, keeps it unsplit, and adds no other coverage figure (the Floor Atlas carries none, 7). `NoCompletionStat` exempts it by name.

#### 4.4.2 Loose ends (change XI-10; lead's call; M2)
- A **loose end** is a known, non-secret Portal (Seen, Remembered or Chalked) whose **far-side space** (the space on the other side of the Portal, by FL-13 room ownership) has `entered == false` **and no square ever Seen** (no Seen or Remembered square) (restated after the check). So a door the party has looked through does not turn back into a loose end when its far squares leave sight (they are Remembered); a room Chalked by F-ROOM or Overlook but never seen or entered **stays a loose end**, because its contents are unexplored. V18t cases: peek-then-leave; an F-ROOM-Chalked room.
- A found Hairline becomes one; a Locked Portal keeps its padlock glyph; a Latch's blind face carries the "barred beyond" sub-mark. A dead-end stub is not a Portal and never becomes one. The Surveyor's Sketch's edges never create loose ends (4.1.1).
- Drawn as a **frayed thread tuft on the Portal** (lead's call, after the check; replaces the draft's chevron, which matched the patrol "continues" mark and the EP fall-back chevron) **on the map and minimap only**; in the world, FL's existing threshold seam is reused and nothing glows (RC6). When a patrol route's "continues" end (4.8.1) falls on the same Portal, the route end sits outward of the tuft.
- **No count is displayed anywhere** (IN6; told plainly; contests the lead's draft, Model A and the UI catalogue). `LooseEndsAtExit` is a report only.

#### 4.4.3 The Detour quote (change XI-10; lead's call; M2; 0 clicks, 0 ticks)
One core query `DetourQuote(target)` over party knowledge, for any loose end, Traced or Chalked unentered room, or map mark. Three lines by default; Inspect shows all five; each line six words or fewer where it can.
1. **Cost:** "≈ 14 steps · 28 there and back" over known squares, then "beyond: ?" where knowledge ends. **Never real minutes** (that would make the design budget a visible timer).
2. **Sound:** the Listen bubble ("heard: 2 still · 14 ticks ago") or "not listened"; "will Listen on arrival" if a trained member is present.
3. **Sight:** containers seen from the threshold or through a Grille (count and silhouettes; tier word if hovered), a Curiosity prop, the elite seen through its vantage; a Lodestring mark if any.
4. **Patrol** (Inspect): a known patrol's window from `PatrolForecast` (4.8.1), or nothing. (Already Inspect-only; the 9.5 cut order no longer lists it.)
5. **Route obstacles** (Inspect): known padlocks, Hunch zones, Found traps, known groups and hazard footprints on the route; a known Latch lever side.

Unknown is always "?", never "none" (RC1). Never shown: anything implying room role.

**Where it appears (lead's call, after the check).** On the board, in the single floating inspect card when the player hovers or Inspects a Portal, a Chalked room edge or a mark (Traced rooms are map-only); on the map, in the right-hand room inspector. The points-of-interest key steps to it, so a controller reaches it, and it shares the budget of at most 1 floating card (4.9.5).

#### 4.4.4 The Behind-you line and the rooms tally (change XI-11 ⚑; lead's call; M2)
- **One line on the existing BR 4.16 arena-entry confirm** (the last cheap point to detour before the 60–97-square post-boss walk): "Behind you: 2 rooms not entered · Treasures 5 / 7 · best remaining: Uncommon". 0 added clicks.
- Only approved fields: rooms not entered, the Treasure field, "best remaining" under its approved k ≥ n/2 rule. **No secrets found/total** (the approved count line shows only "secrets: s"; told plainly) and **no loose-end count**.
- **"Rooms not entered" (lead's call, after the check)** = t − e, less the arena and every room reachable only through the arena (none of them is behind the party). A full clear shows 0 at the confirm (V18t `BehindYouFullClearZero`).
- Hidden entirely in the emptied state (4.4.5).
- **If the player has turned the confirm off** (BR 4.2 allows it; lead's call, after the check): the same line shows once, as a non-modal status-ribbon toast, the first time a party unit enters the arena's doorway band.
- **No Downstair banner and no new Downstair confirm** (told plainly; contests N6 and the lead's draft: after the boss, going back is at its most expensive).
- **The rooms tally** (FL-20's "party-bar tally", undefined in FL; LE 6.15's floor-strip rooms cell; lead's call, told plainly): the floor strip's rooms cell reads **"rooms e / t"**, where t = n + k, the sum of the count line's "Main path: n rooms" and "side rooms: k" **(new lead's call: notation fixed)**, and **e counts Room nodes with `entered == true` from that same node set** (Alcoves and corridor segments never count; definition after the check). It is **never split by role**, because a role-split tally would mark each entered room as main path or side and leak the route to the boss. **Told plainly (after the check):** this tally is a room-coverage figure; it is kept only because FL-20 and LE 6.15 already fixed a rooms tally in the strip, and it is the one named exception to IN6 (4.4.1).

#### 4.4.5 The emptied field (change XI-11 ⚑; lead's call; M2)
When "best remaining" is Common or nothing countable is left, the Treasure field drops one RC6 value step and reads "nothing worth chasing" or "nothing countable left": a clean, honest signal to stop. (Renamed from the drafts' "quiet states" to avoid LP's "quiet summaries".) **Respecting LE's threshold (lead's call, after the check):** while k < n/2, "best remaining" is hidden, so the field can fire only on "nothing countable left"; "nothing worth chasing" fires only once "best remaining" is visible (V18t `SpentFieldRespectsBestRemaining`).

#### 4.4.6 Owner requests (hook; XI-27)
- **Beckoning Glimpses** (EV owner; recommended M2): on floors with side rooms, at least 1 Glimpse per floor *(illustr.)* sits on a wall visible from the main walk line inside or at a side room's threshold. A 0-click pull toward wonder; RC1 holds because `EmptyDeadEndRate` is 0. Report `BeckonFollowRate`.
- **`SideRoomPayoffMix`** (FL, LP and EV owners; M2): at most 60% *(illustr.)* of side rooms pay loot only; the rest also pay a fact, a shortcut, XP or a pointer, so detours still pull in a supply-heavy run. No VP change.

### 4.5 Reveal items, abilities, spells and traits (changes XI-14, XI-26, XI-27, XI-32)

#### 4.5.1 The information charter (lead's call; binding on later content)
- **IP1** Every information verb is free by default (passive Listen, auto-Probe) or a real decision; nothing exists only to avoid punishment.
- **IP2** Counts are free; **locations are earned** (sight, skills, facts, fights, coin).
- **IP3** Sight is symmetric; every other source is hearing, a located FactLedger fact, or chalk.
- **IP4** An item never stands in for a **present** skill lane; a lane-gap item exists only when the lane is absent (TL-25 precedent).
- **IP5** Nothing reveals a Hidden, unnoticed or unheard unit's position outside approved rules (line of sight, Listen, F-LISTEN, Revealed).
- **IP6** The party's own known map is never withheld or priced.
- **IP7** Every displayed value is a core query over `PartyKnowledge`; hovering changes nothing.
- **IP8** Remembered is never drawn as Seen; projected never as known; Chalked never as floor; told is marked.
- **IP9** Same seed, same facts, for every profile; disclosure changes explanation density only.
- **IP10** Information never changes loot value, n, identities or placement.

#### 4.5.2 Adopted

| Source | What it gives | Form and cost | Wave | Owner |
|---|---|---|---|---|
| **Lodestring** | One treasure room (4.6) | Party counter, 1 click, 0 ticks | M3 | Lead (brief question 5); LE, CN, EV |
| **Surveyor's Sketch** | The floor's room graph as node outlines and node-to-node edges (4.6.3) | Tithe Bowl purchase, applied at floor entry | M3 | LE |
| **Ear Trumpet** (agreed, Q3) | Untrained Listen becomes passive at auto-walk door pauses; never size band, "passing" or the activity word | Lane-gap counter (cap 1), stocked by the basic vendor only when no party unit (the hero included) is Perception-trained on the camp-entry snapshot, stable for the visit (the TL-25 Wax Key test); its effect is suspended while any living party unit is Perception-trained (IP4); lasts through the next floor; 15 coin *(illustr.)* | M3, **conditional** on the solo Listen click share missing (below) | CN, FL, ABL, ITEMS; ⚑ TL-25 vendor row and ITEMS 10.3 (client, Q3) |
| **Walls Have Ears** (Arcanist ritual request) | Listens at every closed, unlistened door of the current space at once, each counting as that door's once-per-floor Listen; trained detail if a Perception-trained member is in the party; never through Hairlines or the Bossgate; never Hidden units; results as Listen bubbles | Out of combat, 1 click, 4 ticks *(illustr.)*, no slot (owner's call) | M3 | Arcanist owner |
| **Cased** (Shade rider request) | When the Shade has had line of sight to **every member the party has seen this floor and that is not Hidden** of a known unaware group for 2 consecutive ticks *(illustr.; `cased.ticks`)* with no successful noticing roll by the group, the group is Cased for the floor; the 2-tick window restarts whenever a new member is seen. Cased gives: the one-lap forecast horizon (4.8.1), watched by sight; a "Cased" label, "from what you have seen"; and, **only if EP-41 is ruled and the ENM 13 flag lets Lore-style reads show Perception**, a noticing-odds **band** (Low / Even / High) on hover (lead's call, after the check: the band was a back door round EP-41) | 0 clicks | M3, **conditional on the Trap Eye re-scope ruling (TL-6)** so the Shade does not get two overlapping riders | Shade owner |
| **Overlook writes Chalked** | The vantage Chalks every room with a square in its line of sight (the footprint, Voids as plain chalk; stubs still by sight) | FL M3 | M3 | FL owner |
| F-ROOM (approved) | Interior Chalked | — | M2 | Reading |
| Wand of Outlining (approved) | The only unit reveal, aimed, in line of sight | — | — | Read, unchanged |

- Every item is a party counter or a class rider, never a scroll (CN 13.7 untouched). None writes a trap grade, a found Hairline, a Hunch zone or a unit position (V18s `InfoItemLaneSafe`, `RevealNeverUnits`).
- **Why "Cased" counts only seen members (told plainly).** If the trigger tested true membership, Cased firing or failing would reveal whether a Hidden member exists (ENM 11.2, `ChipNoLeak`, STAT 7.1). The `KnowledgeLeak` fuzz test includes this case.
- **The Ear Trumpet's condition (agreed, Q3; the trigger's terms are a new lead's call).** The Q3 wording governs: the item ships at M3 only if the M2 report **`SoloListenClickShare`** misses. That report is the share of `ExploreClicks` spent on Listen on F1–2, at party 1, among parties with no Perception-trained member; it misses above 15% *(illustr.)*, read at the M3 schedule review. `ExploreClicks` on F1–2 at party 1 is reported beside it but does not trigger the item by itself (the synthesis's alternative "or `ExploreClicks`" trigger is dropped to match Q3). The stock rule (no Perception-trained party unit) is a client key, not tuning. **Its test (lead's call, after the check; replaces the draft's "at the moment of purchase"):** read on the camp-entry snapshot, stable for the visit, exactly as TL-25 reads Mechanics for the Wax Key (TL 6.11; CMP 7.3), so hiring a Perception companion at the Muster never withdraws stock already shown. CMP 7.3's `gapOrder` has no Perception need, so the coverage strip gains **no** Ear Trumpet line; the vendor row alone shows it.

#### 4.5.3 Rejected (also in the register, XI-32; told plainly)
- Magic mapping with contents.
- Vendor map items (Chalk, Floor Rubbing, Folded Floorplan): no measured value on 2–4-side-room floors, and the stub-drawing Floorplan identified the secret on 100% of floors.
- Trap or secret radar; container radar.
- Telepathy, Clairvoyance, Arcane Eye, Spyhole.
- Detect Magic and Identify; divination scrolls.
- Lamps and light radius.
- The Cartographer's Loupe (needs a TK denylist exception).
- Augur's Knot (duplicates Pick the Question).
- Plumb Line and Hedge Bestiary (lane dilution; Later at most).
- Déjà Vu and Familiar Halls (erode Overlook and F-ROOM; Later).
- Plumb the Halls and Dream Survey (radius charting; outclasses facts).
- The Cartouche set piece (nine reliefs for no measured value).
- The off-path loot-rank lean.
- Range Ahead and Scout Ahead (need a change to decided MAIN 4.3 "companions follow"; Later client question).

### 4.6 Treasure hints

#### 4.6.1 F-TREASURE, the fact (change XI-22; lead's call; agreed, Q5 for the exclusion; M3; EV owner for EV 4.4.1, LE for the selector)
- An instance `{F-TREASURE, socketId}` in the per-floor FactLedger (EV-8), shared by every channel, deduplicated, with fall-through.
- **Candidates:** unfound field notables (never shown on a loot summary, the LE 8.2 k rule) at **container** sockets on the current floor, read draw-free from LootBind's bound placement and the LootLedger flags.
- **Excluded:**
  - **secret caches (agreed, Q5)**: a pointer naming the room holding a ≤ 3-square stub, which `HairlineCue` guarantees is a secret and which any adjacent unit can find the tell of, would turn the Mechanics lane into "walk to the short stub";
  - carried notables (they would locate a group, IP5);
  - boxed-lurker containers (TL 6.14 "never a free detector");
  - Windfalls; the Parting Fare; the signature cache; HE reservations;
  - all of F1 (FL-20: F1 teaches the floor).
- **Floors (new lead's call):** the fact channels (Yield, a Curiosity gain) are eligible on F2–F5; the Lodestring item is used on F2–F4 only (4.6.2).
- **Ranking (lead's call, after the check; the draft's "off-path before main path" is dropped):** tier descending, then the target room's macro-grid position (y, then x, in floor coordinates), then the container's square (y, then x) inside the room. Integer, draw-free and **role-blind**: neither room role nor `roomId`, `portalId` or `socketId` (which follow FL's main-chain order) ever orders candidates, so a pointed room says nothing about whether it is off the main path (XQ16). V18t `RoleBlindOrder`: permuting `roomId`s with geometry fixed gives identical targets.
- **Output:** the target room's node is Traced if Unknown (source `treasure`) and gets the mark "Rare · plain cache" or "Rare · behind risk". **The qualifier (lead's call, after the check)** is a fixed generation-time predicate over the **container's own socket class** as LootBind binds it, listed in data: "behind risk" (the approved Tithe Bowl wording) when the socket is padlocked or sits behind a locked Portal, is a guarded socket (LP's guard role), or is a Wager; otherwise "plain cache". It **never** reads whether the room holds a trap, a lurker, a patrol or a hazard, and it never says lock, trap or guard. The hover adds the fixed line **"says nothing about traps, lurkers, patrols or hazards"** (renamed from the draft's "in the open", which reassured; `PointerTextBounded`). The Detour quote lists the route's known obstacles as the party discovers them, so the cost is legible before commitment.
- **Cap:** at most 2 F-TREASURE instances per floor across all channels *(illustr.)*. An empty set reads **"Nothing the string can find"** in identical wording whether nothing is eligible, everything is excluded or the cap is reached; the counter is not spent and no fact is drawn (after the check). A later instance excludes sockets already pointed at this floor. The mark clears when the target container is opened.
- **Exemption from `KnowledgeLeak` (lead's call, after the check):** `TreasureTarget()` output for a paid or earned act is the one listed exemption, limited to {room node, tier word, qualifier}. Every other output stays byte-identical across states the party does not know (9.2).

#### 4.6.2 The Lodestring, the item (change XI-23 ⚑; lead's call; M3)
- **Form:** run-scoped party counter `party.lodestrings`, cap 1, size 0, no carrier (Wax Key model). Never a scroll, never a deck card or notable.
- **Use:** out of combat, any living unit, 1 click, 0 ticks, 0 draws; F2–F4 only.
- **Display:** a string drawn along **known** walls from the party to the known Portal nearest the target room, then a **long-dash bearing with an arrowhead** on the map. **The string is defined by pattern and value, not hue (lead's call, after the check):** a light-cored, twisted two-strand line that passes the greyscale and colour-blind gates on every theme's wall value; red is flavour only and is checked off the RC4 damage-hue key. Map line plus a world trace of 1 s or less *(illustr.)*, never a persistent floor decal (LE 6.15); where its wall is cut away, the world trace projects as the front-square half-rim decal (XI-18), so it reads at all 4 rotations. Re-anchors as new space is entered. Static under reduced motion.
- **Presentation as the "special":** when the pointed notable appears on a loot card, the card gets a "the string ends here" frame (display only, global UI skin).
- **Sources:** (a) the Tithe Bowl (4.6.3); (b) a Curiosity option gain (EV request); (c) the fact itself through Yield (4.6.4).
- **Price:** pegged at or above a healing potion's price at that camp *(illustr.)*, so it competes with healing and Wax Keys for the same coin. Price is the only knob.
- **Value claim, corrected (told plainly):** it never adds value to the floor, but it **can raise the share a hurried party collects** (toy: +0.35 VP for lean players at +0.05 min, inside LE's base-to-base+2 band; LE owner confirms).
- **Leaks accepted:** the tier gap between the pointer and "best remaining" hints that the best card is carried, boxed, walled or behind a Windfall; ambiguous by construction (about 0.8% of uses single out a boxed lurker in the toy).
- **Told plainly (new lead's call):** a Lodestring unspent after F4 has no further use; the item is run-scoped and F5 is outside its floors.

#### 4.6.3 The Tithe Bowl merged (change XI-24; lead's call; M3; LE owner)
- LE 8.1's "coin buys one Stratum fact" becomes one camp service, one purchase per camp, with two options:
  - **A Lodestring** (the carried counter; the player chooses when to use it);
  - **Surveyor's Sketch:** at the next floor's entry, every non-secret Room node and Alcove is outlined on the macro grid, with node-to-node edges and the Downstair. **Never** corridor squares, dead-end stubs, Portal-only fragments with no far node, interiors, square kinds, pieces, units, containers, traps, hazards, or any node reachable only through an unfound Hairline (V18r `ChalkNeverStub`, V18g `SketchNeverSecretNode`; cross-document comparison 2026-11-23, CC-122). Outlined rooms stay unmapped.
- Prices *(illustr.)*: Sketch 15 coin, Lodestring per 4.6.2.
- **Option availability (new lead's call):** the Lodestring option is shown greyed with its reason ("you already carry one") while the party holds one (cap 1), and is not offered at the camp before F5 (the fourth camp), where only the Sketch is offered, because the Lodestring is used on F2–F4 only.
- **Resolution timing:** both resolve at the entered floor's entry or use, never on camp or Two Doors cards (V18t `PointerNeverPreDoor`). The camp card says only "Bought: Surveyor's Sketch".
- The approved guards stay: never "behind a lock" wording; FactLedger dedup.
- **Request:** the LE owner relaxes "unavailable while holding a Wax Key" for both options; parties without Mechanics need routing most, and neither option is the lock card. Fallback in 14.2. Until the LE owner rules, the Bowl sits at the Pedlar's Cart shown greyed with its reason while a Wax Key is held (camp and hub design 4.3).

#### 4.6.4 F-TREASURE through Yield (change XI-25; lead's call; M3; enemies owner for ENM 14.6, Herald owner for HX-6)
- F-TREASURE joins the interrogation categories after F-KEY (default priority unchanged), choosable through Pick the Question, **at most once per floor through Yield** (told plainly; balance fix so the paid pointer is not dead content), inside the shared cap of 2.
- Kill switch: if `YieldCategoryMix` shows F-TREASURE above 50% *(illustr.)* of picks, restrict it to the optional elite.

#### 4.6.5 Fairness proof
- Reads only bound placement and LootLedger flags after generation; draws nothing; never adds, removes, moves, upgrades or re-rolls; n unchanged (LE 8.2); Ledgers A, B, C untouched.
- Resolved on the floor entered, from that floor's placement (identities equal through either door, RS 6.4; `LodestringDoorBlind`).
- Class-blind and skill-free, notables only (answers both LP Tip-offs objections).
- LE 6.9 exposure: recommend **not exposure** (no identity, weaker than F-CACHE); LE owner rules.

### 4.7 Auto-walk, travel and auto-explore (changes XI-8, XI-9; lead's call; M2)

- **Travel** answers FL 6.9's M2 request.
  - Click any Seen or Remembered walkable square, or a **Chalked square inside an entered space** (told plainly; contests the UI catalogue: TL-7 per-square detection and the step check below make this safe, and forbidding it makes paths round a pillar absurd), or a map node or mark. Never into Traced interiors.
  - 1 click, 1 tick per step, out of combat only, followers follow.
  - Paths round known triggers, Hunch zones, hazard footprints, Harm, Rough and web squares, Voids and blocking pieces (ABL 7.2, HZ), and round any Remembered hazard whose phase shows "?". Off inside Dare rooms.
  - **The step check (lead's call, after the check).** Chalk claims shape only (4.1.4), so travel never steps onto a square that is not **Seen at that moment**: after every `KnowledgePass`, if any newly Seen square on the remaining path is a hazard footprint, a Harm, Rough or web square, a Void, a trap, a Hunch zone or a blocking piece, travel re-plans round it; if no path over known-safe squares remains, it stops with the reason in the status ribbon. V18t `TravelNeverEntersKnownHazard`; a walker row.
- **Stops:** closed doors (FL 6.10; passive Listen resolves); a new Hunch zone (TL-7: the stop or auto-Probe); a new trap result; a newly Seen hazard with no safe re-plan; noticing and footsteps; EP-17 events; and:
  - **Noticing pre-stop (lead's call):** the last square **before** any step that is a noticing trigger for a known unaware group (entering its first line of sight, its 6 band or its 3 band), computed from party knowledge only (`KnownCanSee`, pessimistic, from the group's seen squares and Afterimages; 4.1.2). Toast: "Next step: in sight of Turnkeys". Noticing is rolled when the trigger fires, so stopping "on noticing" alone comes too late (told plainly; fixes an "auto-walk ambushed me" trap). `NoticingPreStop(path)` returns the first such step; unseen groups never cause the stop (a `KnowledgeLeak` case). Walker cost: 1 click per stop.
  - **Re-evaluation and release (lead's call, after the check).** `NoticingPreStop` is re-evaluated after every step's `KnowledgePass`. If the first step of a new travel command is itself the trigger, the toast asks for an explicit confirm, and the same click again takes that one step. The stop is not raised again for the same (group, trigger kind) until that group's known state changes (a new sighting, square, awareness or activity), so travel never locks at 0 steps.
  - **Re-sight narrowing (change XI-9; FL owner):** FL 6.9's "stopping on any sighting" becomes "on any **new** sighting or **changed** state" (new group or member, changed square, awareness or activity). A patrol coming into view always stops. A contradicted Afterimage updates silently. **XI-9 ships only together with the noticing pre-stop.** Pending with the FL owner: until it is ruled, `travel.stopOnResight` defaults to true (FL 6.9 as written) (cross-document comparison 2026-11-23, CC-124).
- **Next loose end** (one key, on by default after a profile's first run): travel to the nearest reachable loose end by known path length; stop on the near-side threshold; never open or cross. "No loose ends" when none. **Ties (after the check):** by the near-side threshold square's position (y, then x, in floor coordinates), **never by `portalId` or main-path order**, both of which follow FL's main-chain order and would leak role (V18t `RoleBlindOrder`).
  - **Cycling (lead's call, after the check):** repeated presses step through loose ends in that order, skipping the one the party stands on.
  - **Passability:** loose ends the party cannot currently pass (Locked with no key, Wax Key or Mechanics-trained member; a Latch's blind face; a Squeeze the party cannot use) rank after every passable one.
  - **Dismiss:** a "skip this loose end" action hides one from the key (not from the map) for the floor; it is display state in the UI annex (4.3.4), never read by rules.
  - Walker rows: standing on a loose end, a Locked loose end without Mechanics, a dismissed loose end.
- **To the Downstair** (once the Downstair has been Seen). There is **no "return to main path"** (it would reveal which rooms are main path; Model C's version rejected).
- **No auto-explore (told plainly; contests the DCSS default).** Floors have 9–14 Room nodes, and the decisions are at doors. In the toy, auto-explore pushed balanced players to full clears (side share 1.0, regret 0.81), added 0.85 unplanned engagements per floor and 5× the patrol exposure, and saved no more clicks than travel. Revisit only as a Later accessibility option if `ExploreClicks` misses with travel on.

### 4.8 Stealth, hearing and patrols (changes XI-6, XI-12, XI-13)

#### 4.8.1 Patrol routes and `PatrolForecast` (change XI-6 ⚑; agreed, Q2; M2)
- **Routes** are drawn only over known (Seen, Remembered, Chalked) squares, with an open "continues" end (a three-dot ellipsis; 6) where they leave known space. ⚑ Reading of ENM 12.1 (**settled**: "once seen, the route is drawn"); otherwise the drawing would leak unexplored Circuit rooms.
- **One query, `PatrolForecast(groupId, square, predicate)`, over known patrols only** (seen, or heard through Listen "passing" or F-LISTEN). **The core chooses the horizon** from the group's `routeSeen` record; no consumer passes one (after the check). `predicate` is the consumer's crossing test: `noticing` (the group within its first line of sight, by `KnownCanSee` optimistic, or its 6 band of the square) for the Patrols layer, the Detour quote, the rest preview and EV chips; `hearingReach` (the square inside the act's reach, 4.8.2) for the TL lock card, the Mild Plain auto-pick gate and auto-Probe suppression. It serves the Patrols layer, the Detour quote, the rest preview (ABL 15.1; EP-16 if ruled), the TL lock card, the Mild Plain auto-pick gate, auto-Probe suppression and EV 4.12 chips.
- **Earned (agreed, Q2; defined in observable terms after the check).** FL waypoints are hidden schedule data, so "earned" never refers to them. A forecast is earned when the party has **continuously watched** the group move at least 4 squares *(illustr.; `forecast.earnWatchSquares`)* of its route, or across one observed turn or Portal crossing; or the group is Cased. A single glimpse gives only the route pip.
- **Heard-only patrols (lead's call, after the check).** Listen "passing", F-LISTEN or Walls Have Ears with trained detail earns only the heard Portal's own window, worded as Listen's approved claim ("passing this door within 20 ticks"), and nothing else. A square-by-square window needs a watched segment or Cased. A `KnowledgeLeak` case.
- **Shown as a window, honestly (agreed, Q2):** "in this square's band from +4 to +8 ticks, if undisturbed": the interval during which the known patrol would meet the `noticing` predicate for the hovered square. **Windows are computed only over route squares the party has seen the group traverse with timing** (`observedTiming[]`). Beyond a "continues" end, no window is shown, and no return time out of unknown space is predicted, until the party has watched a **full timed lap**; otherwise the forecast would leak the unexplored route's length. Patrols layer only, never board pips by default. The look is in 6.
- **Horizon (agreed, Q2):** 20 ticks by default (Listen's approved "passing" horizon); **one lap** when a Perception-trained member was among the watchers **by sight** (Listen never earns it) or the group is Cased (a skill-lane payoff). **Data (after the check):** `trainedOrCasedHorizon: "lap"` with `lapCapTicks: 60` *(illustr.)*; the horizon is min(the observed lap, the cap), or the cap while no full lap has been observed. The horizon is fixed when the forecast is earned: the trained watcher's later death or Downing does not shorten it; a re-earned forecast follows the same rule.
- **Withdrawn by events, not age (agreed, Q2):** heard noise that could reach the route (`KnownNoiseReach`, pessimistic), an alert, a contradicting sighting, or the group last seen Alerted, Searching or Pursuing ("route unknown since tick 41"). `withdrawnTick` is recorded; the forecast can be re-earned by the same rule.
- **Gate (agreed, Q2; told plainly, "combat is key"):** the toy shows an uncapped forecast cuts accidental patrol contact about 6×. `PatrolFightRate` (defined in 9.3) must stay at or above 60% *(illustr.)* of the approved-surfaces baseline, owned by EP. On a miss, in order: (1) the horizon stays 20 ticks for everyone; (2) the forecast becomes hover-only; (3) route and memory only. Flagged to the EP and enemies owners as a pacing decision.
- **The baseline (new lead's call; floors corrected after the check):** the BASE arm (8) in the same simulator build, on **F1–5**, at party 4, with party 1 and 2 reported; the gate is read at M2 sign-off and on every balance pass after. Floor 1 has a Circuit: the settled Scavenger patrol on the Circuit through Q room 3 (EP 6.9, CL-10; EP's table "F1 | 1 (Circuit)"; ENM `F1NoiseClearOfCircuit`), so the draft's "Circuits start on F2" was false. The three cut steps are covered by Q2 and apply without a new question; a miss after step (3) returns to the client (14.2).

#### 4.8.2 Auto-actions read party knowledge (change XI-6 ⚑; agreed, Q4; TL-5 agreed row, TL 6.4 lead's call)
- The Mild Plain auto-pick gate (TL 6.6, TL-5: "only with no patrol due") and auto-Probe suppression (TL 6.4: "unless a patrol is due") read `PatrolForecast` over **known** patrols. Today's wording could read true state, and the toy measured the resulting leak at 0.13 events per floor (silent suppression reveals an unseen patrol).
- **The act's hearing reach (lead's call, after the check; makes Q4 computable).** Probe is a noise-0 act (TL glossary), so its own noise reach is empty and a noise-only reading would never fire. An act's **hearing reach** is therefore the union of its noise reach (`KnownNoiseReach`, pessimistic; empty for noise 0) and the worker's noticing reach at the act's square: its 6 band and every square that `KnownCanSee` (pessimistic) links to it. **Duration:** 2 ticks for auto-Probe (TL 6.4), the Take time ticks for an Mild Plain pick (TL 6.6).
- **When a patrol is "due":** a known patrol is due when its window under the `hearingReach` predicate overlaps [now, now + duration] (this maps TL's "crosses the hearing outline within N ticks"). **A known patrol with no earned or current forecast** (a single glimpse, a withdrawn forecast) is due whenever its drawn route or last-seen pip lies in the reach.
- **Stand down (agreed, Q4):** each auto-action is also suppressed when its hearing reach crosses any **never-Seen** square (Unknown or Chalked; 4.1.1 reading of Q4's "unexplored", changed after the check). Cost: the normal card or stop appears, at most 1 click per act; the per-floor cost in partly explored areas is re-measured with the never-Seen reading before M2 sign-off (8.6).
- **Told plainly:** an auto-action can still meet an unseen patrol arriving from known-empty space; Take time is channelled and ends with no result if combat interrupts (TL 6.6).
- `KnowledgeLeak` cases: a glimpsed-only patrol, a withdrawn forecast, an unseen patrol beyond a Chalked pocket.

#### 4.8.3 What the party knows versus what monsters know (change XI-12; lead's call)
- **M2:** an **"in sight of" pip** on a party portrait when a known unaware group's seen member has line of sight to that unit (exact by symmetry; hover names the groups); a **"hidden from: n"** pip. Out of combat, geometry only; not a danger band, so DH 17.3 stands (DH owner informed).
- **M2:** hovering a known unaware group draws its 6- and 3-square noticing bands on known squares, no odds.
- **M3:** **watch lines** on hover: known squares any seen member can see (`KnownCanSee`, optimistic; 4.1.2), captioned "from what you see".
- **M3, conditional:** a "noticing roll" tag on the move preview, with odds only when Lore has revealed the group's Perception bonus. Gated on **EP-41's ruling** (pending client) and a flag to ENM 13, which today does not let Lore reveal Perception bonuses.
- No view cones (line of sight is symmetric).

#### 4.8.4 Hearing (change XI-13 ⚑; lead's call; M2)
- The hearing outline (ENM 12.4 step 6; FL 6.6) is a board and map layer labelled **"known groups only"** (wording reading, enemies owner), with arrival-Portal arrows (EP-22), the self-teaching shut-door prompt, near-pair and elite-vantage risk marks (FL-21 (b) fallback).
- **Noise preview** on aiming any noisy verb (Force 6, Smash 6, Jam 4, Squeeze fail 4, Lure 4, trap noises): reach over known squares (`KnownNoiseReach`, optimistic), "would draw: <known groups>", and the fixed line "unknown things may also hear" whenever the pessimistic reach crosses a never-Seen square. It borrows the information-layer slot while aiming.

### 4.9 Surfaces: budget, layering and priority (changes XI-15 to XI-19)

#### 4.9.1 Fixed regions (at most 7; lead's call)
1. **Top-left, Stratum strip:** theme glyph, floor, boss name, Playbill button, dream-track pips. (C's folded-card minimap frame is an M3 art option only if a regions test frees space.)
2. **Top-centre:** the initiative bar in combat (portraits, boss pips and action count, Dream card, Rhythm strip, banner slot, joiner footsteps); the **status ribbon** in explore (travel, stop reason, Hold the Hide window).
3. **Top-right:** minimap.
4. **Bottom-centre, party bar:** portraits with the danger band (combat only), condition rows, in-sight and hidden-from pips, the Wax Key and information counters, the Grace chip and Strand line, and the **floor strip** on its top edge in LE 6.15's order (potion · rooms · traps · treasures), unchanged.
5. **Bottom-right:** action bar (verbs, greyed reasons, the one contextual chip).
6. **Right edge:** log drawer (clickable lines; LP quiet summaries as non-modal toasts).
7. **Left edge:** card dock for kept inspect cards (up to 2).

The centre is reserved for one modal card.

#### 4.9.2 Board priority (lead's call; extends RC6 without reordering it; RS owner adds rows)
**Units > K lethal > A active tool (drawn as lines) > D due/room clock > T telegraph > H hazard > R trap and Hunch > C cover and pieces > W Portal and wall marks > I information layer > M memory > F floor pattern > N knowledge state.**

#### 4.9.3 Square slot model (change XI-15; lead's call)
- Each square has 1 fill, 2 nested rims (higher tier outer, lower inset about 15% *(illustr.)*), 1 centre glyph, 2 screen-anchored corner pips (re-seated after each rotation), and line overlays from at most 2 families.
- A mark that loses its slot drops to a corner pip (a "+" pip if both are taken), and its text moves to the hover or Inspect card in tier order. **Nothing vanishes silently** (RC1). Rims nest rather than compete.
- **Mapping (lead's call, after the check).** Every `SurfaceRecord` (9.1) carries `slot: fill | rimOuter | rimInner | glyph | pip | line | sprite` and a `countsAsMark` flag, and every registry family is assigned one slot in the 6 registry table. **Contention:** the highest tier (4.9.2) keeps the slot; the losers demote to pips in tier order, ties broken by registry id. **Line-family cap:** the seen-edge line and the knowledge-state grade are part of tier N's drawing and do **not** use a line-family slot; the travel path, patrol routes, the Listen tether, the Lodestring string and bearing, trap links and threat outlines do. `BudgetWorstCase` computes from this mapping.

#### 4.9.4 Modes (lead's call)
- **Explore:** tiers H, R, C, W plus Afterimages plus **one** player-chosen board information layer. No danger band. Loose ends only on the map. **The single default rule (lead's call, after the check):** the default layer is **Patrols** on every floor, F1 included (it draws nothing until a patrol is known, and the F1 Scavenger Circuit is its teaching moment, 4.10); the player's last choice is remembered. The hearing outline is never a standing default: it appears as the shut-door prompt, the lock-card row and the vantage risk mark, and the noise preview **borrows** the layer slot only while aiming, so no two information layers are ever on by default.
- **Combat:** D and T added; threat reach on hover by default; at most 3 telegraph layers (BX8); the minimap collapses; the information layer off except arrival arrows and joiner footsteps; Afterimages per 4.2; Listen badges and told pips hover-only (always on in the Guided density setting); the sight shadow dims to 50% *(illustr.; `dim.sightShadow`)* while a tool is open, not hidden.
- **Tool mode (any tool open; renamed after the check from "Aim", the weapons rule):** the tool preview, lethal, due and hazard marks at full strength; every other T and I mark dims to 50% *(illustr.; `dim.toolMode`)*.
- **Plan (map open):** every layer togglable; the board dims.
- **Declutter** (hold): units and telegraphs only.

#### 4.9.5 Budgets (lead's call; *illustr.*; gated by V18 and the glance test)
- At most 7 HUD regions.
- Enemy plates unchanged (ENM 5.6: ≤ 2 marks plus the formation chip, ≤ 1 glint).
- **Pending request (charm abilities design CA-27; read; M2 for Trophy Tally's pips, M3):** a party unit's charms are **one** plate mark, a pair of pips, so they spend one of `plateMarks: 2` (which party plates already spend on the "hurt" and "low" marks, DH-20); no budget change. Not a rule until this owner rules (charm abilities design 4.7, 6).
- `MarksPerScreen` ≤ 12 at default zoom, with gate targets P(> 12) ≤ 10% in explore and ≤ 5% in combat. **Defined (after the check)** as the count of visible records with `countsAsMark` in the viewport at default zoom; the 6 registry table sets the flag (Afterimages, glints, Listen badges and told pips count; knowledge-state fills and the seen-edge line do not).
- ≤ 3 telegraph layers; 1 board information layer.
- 1 modal plus ≤ 1 floating and ≤ 2 kept inspect cards.
- Stratum Card ≤ 4 body rows; Detour quote ≤ 5 lines; event chips per EV 4.3.1; item cards per IR 6.1.

#### 4.9.6 Board digits are room clocks only (change XI-16; lead's call; M1)
A digit on the board means a room's one clock (the due digit, the Disturbance meter, the Windfall ring; HZ 6). Ages, counts and odds appear as pips, stacked shapes or hover text; Afterimages carry no numerals (4.2), and `BoardDigitIsClock` lists the Afterimage among its fixtures.

#### 4.9.7 Wall marks under cutaway (change XI-18; lead's call; M2)
Every rule mark on a wall (Hairline tell, Spoor, wall-set mark, Rathole, the vantage sight-risk mark) projects a front-square half-rim decal whenever its wall is cut away or faded (FL 8.1, MAIN 6.8), readable at all 4 rotations; the LP Rathole decal is the precedent. EP owner confirms for Spoor (a projection of a wall mark, not a floor mark).

#### 4.9.8 Card queue (change XI-19; lead's call; completed after the check)
Modal only, one at a time, in this order:
1. lethal, end-turn and ally confirms, including TL's ally-onto-trap and Bear-it prompts;
2. the arena-entry confirm with the Behind-you line;
3. the boss phase card;
4. the Hunch stop card (TL 6.4) and the lock card (TL 6.23);
5. reward, Parting Fare, loot summary;
6. Curiosity and Twice-Told;
7. TL's first-entry prompt (cross-document comparison 2026-11-23, CC-59);
8. the floor-entry card: one card, headed by the Stratum Card, that carries the DH floor-entry card (DH 17.12) (cross-document comparison 2026-11-23, CC-59) (dismissed by the first move command, 0 clicks; ≤ 4 body rows; overflow — Lore roster, F-BOSS lines, elite intel, Strand line — to the pause **Floor-info panel**).

- **Pre-emption (lead's call, after the check):** a displayed card is never pre-empted except by tier 1, which suspends it and resumes it afterwards. Otherwise ties and arrivals resolve first in, first out, in core event order.
- **First-sight callouts are not modal (after the check):** they are one-line, non-modal toasts in the status ribbon (explore) or the log drawer (combat), dismissed by the next move, at most 3 per floor *(illustr.)*, and counted in `FirstRunExplainerCount`.

Inspect cards never queue. On a profile's first two runs, a one-line ribbon "More in Floor info (F)" shows for one floor after the Stratum Card closes.

#### 4.9.9 Naming reconciliation (change XI-19; lead's call; FL, LE, LP owners confirm)
- **Count line** = the Stratum Card's line (FL-20, a lead's call in FL).
- **Floor strip** = the party-bar row (LE 6.15).
- **Treasure field** = the strip's "k / n · best remaining" cell ("Treasure strip", the term of LE 6.15 and LP 4.12, retired as a synonym; LE 6.15 and 8.2 wording follow, XI-19).
- **Floor-info panel** = the pause panel (LE 6.15, ENM 15.8; cross-document comparison 2026-11-23, CC-126).
- FL 9.2's "treasures: k / n" on the count line is read as the strip field.

### 4.10 Accessibility, input and disclosure (changes XI-20, XI-21)

- **Never hue alone:** every state and mark is shape plus pattern or value (RC4, RC5); all knowledge states, Afterimages, loose ends, projected pips, told pips and Waymarks join the registry and the greyscale and colour-blind gates.
- **Inspect parity (M2; lead's call):** no information is hover-only.
  - Hold **Inspect** labels the square, unit or Portal under the cursor.
  - **Keep card** docks it.
  - A **points-of-interest** key steps through known Portals, loose ends, containers, tells and marks, nearest first by known path length, ties by square position (y, then x; never by id or path role, XQ16).
  - V18s `NoHoverOnly`, which also checks that every segment 10 **action** has a non-mouse path.
- **Default bindings (lead's call, after the check; all remappable; defaults moved off Alt and Tab, because Alt+Tab is the operating system's window switcher and Alt toggles menu focus on some desktops):**

| Action | Keyboard / mouse | Controller | Mode |
|---|---|---|---|
| Inspect (hold) | V | LB (board); held left trigger (map) | All |
| Keep card | P | R3 | All |
| Points of interest, next / previous | ] / [ | D-pad (board) | Explore, combat |
| Full map | M | View (Later) | Explore |
| Map layers, next / previous | Page Down / Page Up | RB / LB | Map |
| Travel | Click | A on the focused square | Explore, map |
| Place Waymark | Shift-click (world); right-click (map) | Y on the focused square | Explore, map |
| Next loose end | N | Right stick click (L3) | Explore |
| Skip this loose end | Shift-N | Hold L3 | Explore |
| To the Downstair | B (shown once the Downstair is Seen) | Hold Y in the map | Explore, map |
| Declutter (hold) | Z | Hold right trigger with no tool open | All |
| Floor-info panel (cross-document comparison 2026-11-23, CC-125) | F | Menu (Start) | All |

  The M1 rebinding work adds a conflict check against operating-system-reserved chords (Alt+Tab, Alt+F4, the Windows key and its chords). Controller defaults are for the M2 focus model; controller polish stays Later.
- **Camp rows (camp and hub CH-22 ⚑; agreed there, Q3; M2):** the **Camp List**, a left-side list mirroring every camp station, on the bumpers (stations only) for controller and keyboard; ambient figures are never on the station ring but on a separate **Hear-out sub-list** on one face button ("Hear out" is the ambient verb, distinct from the floor Listen action); **Hold captions** (captions persist until the next input or focus change; the setting pins the last line in a corner tray, default on above 100% text scale; no timed text); **Legible captions** (one legible font, default on above 125% text scale); **controller Travel on** is a dedicated hold (0.6 s *(illustr.)*) reachable from any station, the door never default-focused, and the hold prompt shows any confirm lines, so **the hold is the confirm**. Inspect parity and `NoHoverOnly` apply at camp and hub (camp and hub design 4.1, 4.2, 4.13).
- **M1:** full rebinding; text scale to 150% with `CardFit` re-run (including EV's worst-case card, an acceptance item); glyph scale; hold-or-toggle for every overlay; reduced motion (no ink-in, static string, no veil drift, Afterimages never animate).
- **M2:** high-contrast overlay; sticky tooltips. **Later:** controller polish (the focus model is built in M2 so nothing is redesigned); screen-reader card text.
- **Audio:** no new gameplay cue; map, Waymark, travel and fact sounds use the global UI timbre (RC7); a travel stop is never a tick (the Dream tick is reserved).
- **Progressive disclosure (change XI-21 ⚑; lead's call; M2):**
  - `uiMemory`, a display-only profile field outside the reproducibility key (flagged addition to MAIN 6.6, EV-19 precedent): one-line first-sight callouts per mark family.
  - **Starting kits (starting kits SK-9 ⚑; settled; M2; M3 where marked):** on run 1 the class card shows the simplified **gear strip** (kit silhouette, role word, AC and the hand-off line; Shade and Herald show two strips with the default pre-selected; verbs, coin, supply, the given name and the Oddment line on inspect); from run 2 `uiMemory` shows the full strip (set icons with their verb text, never icon-only; armour and AC; potions, coin and supply generated from data; the "Also carried" heading with the Oddment's name). The **Oddment callout** is one-time and non-modal, the first time the hero inspect is opened at camp 1, never at F1 entry and never part of this section's staged vocabulary. **Creation warnings** for a kit item Strained or AC below the pick's `kit.acFloor` are one line of shape plus text, never hue, and never block. **M3:** the variant pick-order line on the Heirloom shelf and creation card. Text chips and labels, alt text, glyph and pattern, controller parity, whole-string keys; Inspect parity and `NoHoverOnly` apply (starting kits design 4.4, 4.6, 5.4).
  - **Staged by floor:** on a profile's first run, F1 shows Seen, Remembered, Chalked, Afterimages, Listen bubbles and loose-end marks on the map by default; the hearing outline first appears with the first shut-door prompt (as the prompt, never as a standing layer); patrol lines with the first known Circuit, on F1 the settled Scavenger Circuit (the cowable-group lesson); told and projected pips when a fact or Stir first appears. One information layer at a time, per 4.9.4 (aligned after the check). Every layer stays available by toggle and Inspect, so content is unchanged (IP9).
  - Density setting **Guided / Regular / Compact** moves items between always-on and hover only ("Regular" renamed after the check from "Standard", ENM's rank and TL's device grade).
  - **Veterans:** compact density, remembered layer toggles, Next loose end on by default.
  - Test `ProfileBlind`: the same seed gives byte-identical knowledge and previews on every profile. The codex is a library, never a HUD input (EV 4.4.2).

### 4.11 Determinism (lead's call; M1)

- Every rule-bearing pixel is a core query through `IKnowledgeQuery`: `Visible()`, `State(sq)`, `ChalkOutline(space)`, `Afterimages()`, `Marks()`, `Facts()`, `LooseEnds()`, `DetourQuote(t)`, `NoisePreview(verb)`, `TravelPath(dest)`, `NoticingPreStop(path)`, `PatrolForecast(g, sq, predicate)`, `InSightOf(unit)`, `TreasureTarget()`, `SketchGraph()`, and the two helpers over party knowledge, `KnownCanSee` and `KnownNoiseReach` (4.1.2, with each query's mode). The only AI-output places stay those the enemies owner rules (ENM 13.5 intent; EP-28 if ruled).
- The display holds only camera, panels, active layers, Waymarks and card state. Cosmetic randomness (ink wobble if the skin ships, paper grain) comes from the display or dressing stream (FL-2), which the core never reads. **Amended by SV-2 (M1):** it comes from the `display` key's `ink` domain (`DisplayKey`, outside every rules stream; Core assemblies cannot reference it), not the dressing stream; display interactions (dismissing a card or a Waymark) are UI-layer only, never core commands (SV-13, read) (seeds and validation design 4.3, 4.6).
- **Segment 10 adds no seeded stream and consumes no rules draw.** Items are core actions with events (`InstrumentUsed`, then `SpaceTraced` or `FactDrawn`; cross-document comparison 2026-11-23, CC-126). (Confirmed: segments 11–12 add no stream for segment 10; seeds and validation design 11.)
- **Event order:** the 4.1.3 `KnowledgePass`; paid knowledge acts resolve as a single core action (`InstrumentUsed` → the writes, counter and coin included → the suspend-slot write, 4.12, that command's journal record) whose result is shown only after the write (the outcome rule; seeds and validation design 4.8.3).

### 4.12 Save integrity (change XI-28 ⚑; agreed, Q1; M2)

- **Told plainly:** floors are seeded and every segment 10 rule is draw-free, so lurker squares, patrol schedules, traps and boxed lurkers come back identical on reload. MAIN 6.6 writes the run save at safe points; if a mid-floor quit restored an earlier snapshot, a player could walk in, look, quit and replay with perfect foresight, and segment 10's map would make that foresight easy to keep.
- **The rule (agreed, Q1):** a **suspend-save** model for permadeath.
  - Quitting mid-floor writes **one suspend slot** of the current core state; loading it deletes the slot.
  - `PartyKnowledge` is written **forward-only**: a resume never restores an earlier knowledge state than the one already written.
  - Every paid knowledge act (Lodestring use, the Tithe Bowl, a Yield fact pick) is an atomic write, like LE-14's camp commit.
- **The slot is continuous (lead's call, after the check; replaces the draft's knowledge journal).** The draft resumed a crashed run from the last safe point with knowledge laid over it from a journal. The check showed that this rewound deaths, Downs, sprung traps, spent counters and coin, broke replay (journaled `entered` flags drive flee anchors and EV 4.4.2), missed what was merely seen, and let a Lodestring or a Yield fact be used twice. So:
  - **While a run is on a floor, the one suspend slot is brought up to date after every committed command** (each command-log entry: every step, act, combat action, death, Down and paid act): one `suspend.journal` record per command over `suspend.base`, per the write order below (cross-document comparison 2026-11-23, CC-121). Quitting writes it too. Base plus journal hold the full core state, so the counter, the coin, the world and `PartyKnowledge` always agree, and replay from it reproduces every rule input.
  - **Write order (amended by SV-14 ⚑; agreed, SV Q4 (cross-document comparison 2026-11-23, CC-121); M2):** the slot is a snapshot plus a state-delta journal: `suspend.base` (the floor-entry or compacted snapshot, `PartyKnowledge` and the combat counter, written only by temp file, flush and atomic replace) and `suspend.journal` (one delta record per committed command, appended with a flush, compacted often). **No frame whose content depends on a draw, a reveal, a Down, a death, a spent counter or a paid act is shown until that command's journal record has landed** (the outcome rule, WO2); a paid act's result (the mark, the fact, the bought service) is one such frame (seeds and validation design 4.8.3, 4.8.4).
  - **Load order (amended by SV-14 ⚑; agreed, SV Q4; M2):** read the slot, validate it (matching `runId`, contiguous journal), write the re-validated state as a new `suspend.base` by atomic replace and truncate the journal to it, then accept input (WO7). The loaded slot no longer exists, which is how "deleted on load" is met (a reading of XI-28 and Q1; agreed, SV Q4: atomic replace instead of delete-then-write); the running game owns a fresh one, so a crash at any moment, the load path included, resumes at the latest committed command, never earlier, and never leaves the run without a valid slot (seeds and validation design 4.8.3).
  - **Precedence:** the suspend slot always wins over the safe-point snapshot whenever it exists. A mid-floor run with no slot can arise only at the floor's own safe point (before its first command), so resuming from the safe point never rewinds play.
  - **Told plainly:** this goes beyond Q1's "quitting writes" (the slot is also written per command) but keeps its single slot, its delete-on-load and its forward-only knowledge, and it closes the crash path Q1 was meant to close: killing the process can no longer undo a death, a bad fight, a spent counter or a look. It amends MAIN 6.6's "the run save is a snapshot of the full core state written at safe points" for mid-floor play (XI-28); MAIN 6.6's "the snapshot is the authority" holds, with the slot as that snapshot. Segments 11–12 own the file mechanics and the write cost (**answered:** seeds and validation design 4.8; agreed, Q4 there).
- Camp and other safe points (MAIN 6.6; EV-21) are unchanged: quitting at camp writes the normal run save, and no suspend slot exists outside a floor.
- V18t `KnowledgeReloadMonotone` (kill after a Lodestring use, after a Yield fact, after a Tithe Bowl purchase, after merely looking through a door: none is refunded or forgotten), `DeathNotRewindable` (kill after a death or Down: the resumed state has it), `SaveReloadKnowledge`; beside them, V19t `SlotEquivalence`, `ProbeResistance` and `LoadPathKillFuzz` (SV-14; seeds and validation design 4.8.3, 4.8.4).

### 4.13 Hooks to segments 11–12, camp and hub

- **Segments 11–12:** `PartyKnowledge` serialisation and the 4.1.3 event order in the run save and replay; the UI annex; the continuous suspend slot, its write and load order (agreed, Q1; per-command writes after the check); V18 in the validator set; the `KnowledgeLeak` fuzz harness as M1 infrastructure (it also protects ABL 8.3 and DH 17.2 for every later segment); release-seed coverage of `LosSymmetric` and treasure targets; walker rows for travel, the pre-stop, XI-9, Next loose end and the items; a human-session arm; no new stream. **Answered** by the seeds and validation design (segments 11–12, approved 2026-11-23; its 11): `PartyKnowledge` and the 4.1.3 event order in `run.save` and the slot (4.8); the UI annex as `run.annex` (4.8.1); the slot as `suspend.base` plus `suspend.journal` with write orders WO1–WO8 (4.8.3–4.8.4; agreed, Q4 there); V18 in the consolidated suite (5.2); `KnowledgeLeak` as M1 infrastructure and a PR-tier smoke (5.8); the release seed set covers `LosSymmetric` templates and treasure targets (4.11; SV-18, read); walker rows under the W instrument and the `XI.*` arms, the human arm as the H instrument (6.1, 6.2, 6.11); no new stream added.
  - **Pending request (segments 11–12; SV-22; M2):** walker equivalences, for example whether `XI.Completionist` equals `FL.Hunt` plus optional fights, are an owner question for the XI, FL and EV owners, not a silent mapping. Not a rule until they rule; if they do not, there is no mapping and each gate reads its own owner's arm (seeds and validation design 6.2, 14.2).
- **Camp:** the Tithe Bowl (Lodestring or Surveyor's Sketch); the conditional Ear Trumpet stock row (agreed, Q3); the next-floor card unchanged (RS 6.9); the Lore roster preview (RS, M3); the previous floor's Atlas page on the dream track (M3).
- **Hub and profile:** `uiMemory`; codex hosting (RS owns, segment 10 hosts, EV-37); the Floor Atlas in the run record (M3).
- **Answered** by the starting kits design (segment 14, approved 2026-11-23; its 4.6, 11, 13): the gear strip, the Oddment callout and the creation warnings under `uiMemory` (SK-9 ⚑, applied in 4.10; M2); codex hosting includes the Oddments page (RS owns, segment 10 hosts, with camp and hub design 4.15.8; M2).
- **Taken up** by the charm abilities design (segment 16, the TK 24.2 pass, approved 2026-11-24; its 4.7, 6, 11, 13): charm `forecastHint` computed only through `IKnowledgeQuery` and party knowledge, with its `KnowledgeLeak` cases (pending with this owner); `plateHint` never on a hostile's intent telegraph; plate states with Inspect text (`NoHoverOnly`); `uiMemory` callouts on the first charm pickup, first catch fire and first Keep fill (4.10, read). **Pending request (CA-27; M2 for Trophy Tally's pips, M3):** the charm pip pair, forecast line and fire beat as `SurfaceRecord` rows, the `plateMarks` read, the pip patterns and the output cues in the blind-ID gate (4.9.5, 5, 6). Not a rule until this owner rules.
- **Answered** by the camp and hub design (segment 13, approved 2026-11-23; its 2.2, 4.3, 5.2, 11): the Tithe Bowl at the Pedlar's Cart, camps 1–4, the Sketch only at camp 4, shown greyed with its reason while a Wax Key is held until the LE owner rules on 4.6.3's request (M3); the Ear Trumpet row in the vendor panel (M3, conditional); the next-floor card trimmed and unchanged at the door station, with the Lore roster preview in its detail tier and the previous floor's Atlas page pinned to the dream-track ribbon above the door (M3); camp chalk is pictorial, never this document's chalk grammar (its 4.7); `uiMemory` stages the hub stations and the ambient first-focus tooltip (its 4.12); codex hosting includes the "Residents of the Hollows" pages (M3; its 4.15.8); the Floor Atlas and fatal-room map in the Book of Nights and the recap (M3); the camp input rows in 4.10 (camp and hub CH-22 ⚑).

---
## 5. The UI surface catalogue (lead's call; every hook placed)

Tier codes as 4.9.2 (U = unit plate). Placement: **A** always on, **H** HUD, **V** hover or Inspect, **C** card, **M** map layer, **L** log, **P** Floor-info panel, **Meta** camp, record or codex. Every row has Inspect and Keep-card access (XI-20), a `SurfaceRecord` (9.1) and a registry row where it draws a mark (V18s `SurfaceRegistered`).

| Surface (source) | Placement · trigger | Tier | Shape | Conflict → resolution |
|---|---|---|---|---|
| Knowledge states, seen-edge line (FL 13; XI-17) | A board, M · always | N | 4.2 grammar | Unknown vs Void: fog bank at wall-top height |
| Minimap, rules-room overlay (FL 8.1, 13) | H top-right; overlay on M and Inspect | H / I | FL's schematic node graph with three knowledge fills (4.3.1); thin room-border line; glyph floor 10–12 px at 720p | Overlay vs hearing contour: line styles differ (6); one I layer |
| Portal glyphs, padlocks, Latch faces (FL 9.2, FL-7, TL-22) | A, M · reveal | W | Global set; padlock replaces Door glyph; state badge left corner, hazard or Dare signpost right corner | — |
| Listen results, heard doors (FL-18, EP-9, EP-12; TL 13 mechanism; HZ 13 tells) | Door-frame badge A; Listen bubble M · on Listen | W | Footsteps glyph, size pips, "passing" arrow, activity word and watched Portal if EP-9 and EP-12 are ruled, mechanism glyph, Stir-tell glyph (4.3.5); hollow when old | Footsteps also mean joiners: door frame out of combat vs arrival Portal in combat |
| Hairline tell (FL 9.2) | A wall, projected under cutaway · seen by party | W | Universal tell; Sound-the-wall chip | Spoor on same wall uses jambs and lintel |
| Count line (FL-20, RS 6.9, DH 17.12) | C Stratum Card | Card | Text | Naming per 4.9.9 |
| Hearing outline, arrival arrows, shut-door prompt, vantage risk (ENM 12.4, EP-22, FL 6.5, 7.2, FL-21) | I layer; lock-card row; chip | I / W | Double contour, ear badge, arrival arrow, sight-risk mark | Noise preview borrows the slot while aiming |
| Patrol routes, forecasts (ENM 12.1, FL 6.7, EP 6.9, EV 4.12 chips) | I layer, M, rest screen, lock card | I / M | Thin line with arrowheads every 3 squares *(illustr.)*, "continues" three-dot ellipsis, forecast band (6) | Vs LP scurry trail, EP fall-back and the loose-end tuft: distinct shape families; the route end sits outward of a tuft on the same Portal |
| Travel path, auto-walk stops (FL 6.9, 6.10, TL-7) | A while hovering, I while walking; status ribbon | A / I | Footprint-dot centre line, destination ring (never a flag or tack) | Hunch stop card replaces the toast |
| Landmarks, compass, Precinct tints, the Downstair (FL 8.1) | Art, minimap | — | As approved | Tints never on walkable fill |
| Overlook (FL 9.1, M3) | Produces Chalked | N | Chalk | — |
| Stratum Card, Stratum strip, dream track, Lore roster (RS 6.9, 9.5, ENM 10.6) | C at entry → H strip; P overflow | Card / H | LE 6.15 order, ≤ 4 rows | Overflow to P |
| Camp card, Two Doors cards, road not taken (RS 6.9) | Meta | — | As approved | No pointer result on door cards |
| M3 Mood, Omen, Nightmare lines (RS 6.9, HZ-20) | C announcements group | Card | ≤ 1 line each | Overflow to P |
| Enemy plate contract, awareness glyphs (ENM 5.6, 12.1) | A plate | U | As approved | Segment 10 adds nothing to enemy plates |
| Threat reach and ready tint (ENM 13.1) | T on hover in combat; toggle | T | Outline; ready tint adds a dash | Never hue alone |
| Turn log (ENM 13.1) | L | — | — | — |
| Signature banner (ENM 13.2, RC7) | Initiative bar | H / D | Universal banner | Boss signatures only |
| Joiner footsteps, reinforcement warnings (ENM 12.4) | Arrival Portal, initiative bar | T | Footsteps + arrow | — |
| Ready in N, phase marks, resolve pips (ENM 13.1) | Plate, initiative bar | U / H | As approved | N never on a board square |
| Challenged/Quarry lines, leader auras (ENM 13.1) | T | T | As approved | Aura vs Chainpost ring: dash types |
| Intent read (ENM 13.3, 13.5) | V plate | T | Text | EP-28 hold squares if ruled: same tier |
| Cowable glyph, morale overlay, pip bar (ENM 14.8, 14.10, HR 10.3) | Plate; toggle | U / T | As approved | Glints-per-screen gate |
| Loot summary, floor-fact slot, leftovers marker, Floor-info panel (ENM 15.8, LE 6.15; cross-document comparison 2026-11-23, CC-126) | C; W; P | — | As approved | Card queue |
| Formation chip, Line broken, Keystone, "Facing the door" (EP 6.20) | A plate once all noticed | U | Icons | ≤ 2 plate marks plus chip |
| Hold squares, fall-back (EP-28) | V from Read card | T | Brackets; chevron with dashed tether | Vs "continues" ellipsis and the loose-end tuft: different shape families |
| Marked tell, seen-tell band (EP 6.8.4) | A | W | Ring with eye tick; hollow ring | Vs sight-risk mark: distinct |
| Encounter Read card (EP 6.20) | C inspect | Card | As approved | One floating inspect card |
| Spoor, bait mark (EP 6.8.6, RC5) | A wall, projected | W | Three-stroke tally | XI-18 |
| Rest-preview movers (EP-16) | Rest screen | Card | Reads `PatrolForecast` | — |
| Hedgehog badge (EP 6.20) | Hearing outline | I | Badge beside ear badge | — |
| Trap glyph, sub-mark, gear, state badges, device area, trap link (TL 6.23) | A; V area and link | R | Approved RC5+ | Nested rims with hazards |
| Hunch rim and three-way stop (TL 6.4) | A rim; stop card | R | Dotted rim, "?" ticks | Never a hatch |
| Disturbance meter (TL 6.15, M3) | Wager glyph | D | 3 pips | The room's clock |
| Three-part preview, Hunch lethal tag, Botch line (TL 6.23) | Tool mode | K / A | As approved | — |
| Lock card (TL 6.23) | C modal | Card | Adds hearing per route and known-patrol window | `PatrolForecast` known only; queue slot 4 (4.9.8) |
| Prompts: first entry, ally onto trap, Bear-it (TL 6.23) | C modal | Card | — | Queue slots 1 and 7 (4.9.8) |
| Trap count line, Keybearer pip, wide-berth cue (TL-9, TL 6.4) | H strip; plate; one-shot cue | H / U / R | No total | — |
| Camp Wax Key line, codex pages, `detectionAtFire` (TL 6.23) | Meta | — | — | — |
| Latent footprint, due overlay and digit (HZ 6.18) | A | H / D | As approved | Only board digits |
| Dream card (HZ 6.18) | Initiative bar | H | As approved | Banner overlays ≤ 2 s *(illustr.)* |
| Hazard rule card, previews, Snag link, fall arrow, unit pips, Chainpost label, Void lip, Cord and vent pips (HZ 6.18, RC5) | V; aim; A | H | As approved | — |
| Supper menu card (HZ 13, M3) | C inspect | Card | As HZ places it; one floating inspect card | Never queued (inspect) |
| Plunge links (HZ 6.10, M3) | V; aim | H | As approved; a line family in the 6 line-style registry | Vs trap link and Snag link: stroke patterns differ |
| Push/Shunt/Kick/Smash, move and cover previews; cover shields; verb chip; "Needs a tall piece to hide" (RT 6.12, 13) | Tool mode; chip; greyed reason | A / C | As approved | One chip: lethal-relevant > shut-door > RT verb |
| Danger band (DH 17.3) | Portraits, combat only | H | Band + word | In-sight pip is not a band |
| End-turn warning, lethal tags, cracked heart, move/heal/get-up previews, rest screen (DH 17.4–17.11) | C; K; aim | K / A / Card | As approved | Top of queue |
| Death recap, Grave Goods, floor-entry card (DH 17.12) | Meta; C | — | Adds a fatal-room map thumbnail (M3) | — |
| Floor strip, Treasure field, best remaining, by-source hover, emptied field (LE 6.15, LE-15, LP-28) | H party bar; V | H | Icon counts | Order unchanged; no new field |
| Due line, tag, "paid" (LE 6.15) | C; H; summary | — | As approved | — |
| Tier marks (LE 6.15) | Container hover and map container hover only | Card | Diamonds + tier word | Never a board decal |
| Loot badges, Steal preview, salvage, elite intel, Parting Fare offer, camp ledger, camp-4 warning, Memento, Perilous badge (LE 6.15, 8.2) | Plate; aim; C; Meta | U / A | As approved | — |
| Due and dissolve stings (LE 12) | Audio | — | Global UI timbre | — |
| Threshold preview (LP 4.2) | Art | — | Silhouettes | CT2 art-blind |
| Container hover, Proprietor tags (LP 4.12) | V | Card | As approved | — |
| "Got away" log, quiet summaries, Glean (LP 4.12) | L; non-modal toast | — | — | Never blocks the queue |
| Wall-set mark, Rathole (LP RC5) | A wall + projection | W | Bracket | Precedent for XI-18 |
| Windfall set (LP 4.12, M3) | A, plate, H | U / D | As approved | Digit ring = room clock |
| Uncorked glint, twin-lid mark, the Washstand (LP 4.12) | Plate; container | U / W | As approved | — |
| Item card, verdict, mark line, Whence, offer frames, codex badge (IR 6.1, ITEMS 8.2, TK 15) | C | Card | As approved; "the string ends here" frame (M3) | `CardFit` at 150% |
| Carried-use floater, "Guards:" line, signature cache decal (IR 4.13, 4.19) | Floater; V; W | — | — | Never the banner slot |
| Over-capacity hatched slots (ITEMS) | Inventory only | Meta | Hatch | Never on board |
| Trader marker (ITEMS 10.9, Later) | M; W | W | As approved | — |
| Curiosity card, chips, First Telling, Twice-Told (EV 4.3.1, 4.5.2) | C modal | Card | As approved | Worst-case fit is an acceptance item |
| Facts on map, referent highlight, bound-tick chips (EV 4.12) | M Facts; board highlight while card open | I | 4.3.6; told pip | Chips read `PatrolForecast` |
| Grace chip, Strands (EV 4.12, 4.5.7) | Party bar; P overflow | H | — | — |
| Afterword, codex, Unwoken Roll (EV 4.12) | Meta | — | — | — |
| Glimpses, beckoning Glimpses (EV 4.11) | Art | — | Never a Portal or container silhouette | — |
| Two-tier boss plate, Rhythm strip, phase icons, Sprawled (renamed from Upended by boss rooms design 4.7.2; cross-document comparison 2026-11-23, CC-65; restated here by naming pass NP-6), Resolve Spent (BR 4.16) | Plate; initiative bar | U / H | As approved | Gates on plate |
| Banner, phase card, threshold preview, Playbill, Take Places, Counterpiece previews, reserves and wake chip, Gambit squares (BR 4.16) | As approved | D / T / Card | — | Phase card above reward |
| Leash ring, reward card, Memento, Boss Ledger and Grudge, Phase Score (BR 4.16, 4.17) | T; C; Meta | — | — | — |
| Pull-back zoom, intro card, Sanctum refusal, arena-entry confirm (BR 4.16) | Camera; C | Card | Confirm gains the Behind-you line | "Seal" never used |
| Conditions row, greyed reasons, Revealed outline (C 15.1, 15.3, 18.7) | Plate; action bar | U / H | As approved | Revealed is a live unit; Afterimage is a cut-out |
| Charm pip pair, forecast line, fire beat (charm abilities design 4.7, 6; TK 15) | A plate; move or attack forecast; over the unit on a catch | U / A | **Pending request (charm abilities design CA-27; M2 for Trophy Tally's pips, M3):** registered as `SurfaceRecord` rows (V18s `SurfaceRegistered`); one plate mark, five pip states by shape and pattern, never hue alone | One of `plateMarks: 2` (4.9.5); the forecast line through `IKnowledgeQuery` and party knowledge only (`KnowledgeLeak` cases); never on a hostile's intent telegraph; pips greyscale-gated against the Windfall mark and the Lodestring spool |
| Break-chain preview, combo glint, morale overlay (ABL 8.3, ARC 10.2, HR 10.3) | Tool mode; plate | A / U | As approved | Glints gate |
| Shade False Trail marker (SH) | Board, party side | M | Request: hollow double-footprint ring | Never the Afterimage shape |
| Hide state, Hold the Hide (SH; ABL 7.2; EP-17) | Portrait pip; status ribbon | H | Hidden-from count | — |
| Readings (M3, STAT/CN) | Fact marks | W | Told pip | Charter: "a fact" |
| Trap Eye re-scope (TL-6, pending) | Per Shade owner | — | — | Cased conditional on it |
| Lore-inscription Hairline reveal (M3, pending client) | F-SECRET drawing | W | — | — |
| CMP companion card, next-camp preview, Recruiter's Eye, coverage strip (CMP 7.x, 8.6, 9.2) | C; M footer; party bar | — | As approved; the coverage strip gains no Ear Trumpet line (CMP 7.3 `gapOrder` has no Perception need; 4.5.2) | — |
| Seams (RS 9.1, M3), the Clear Key (Later) | Art; per owner | — | — | Display only |
| Afterimage vs EV figure frame (EV 4.11, M3) | — | M | Afterimage: role glyph, dashed cut line, on a walkable square; figure frame: cropped figure in a niche, never on a walkable square | Role-ID and greyscale gate pair (4.2) |
| **Segment 10's own:** Afterimages, loose ends, Listen bubbles, Detour quote (floating inspect card on the board, room inspector on the map; 4.4.3), Behind-you line, emptied field, status ribbon, Waymarks, in-sight and hidden-from pips, noise preview, noticing bands (M2), watch lines and step tag (M3), Lodestring string and mark (M3), Declutter | Per 4.2–4.9 | M / W / I / H / C | Registry rows and line styles in 6 | Budgets 4.9.5 |

---

## 6. Art and audio (lead's call)

**Art (the eager team's list; M2 unless marked):**
1. **Knowledge-state style sheet, split by what it needs (after the check).**
   - **Before the first Tone Sheet (theme-neutral):** the Remembered "faded photograph" grade parameters (tiers F and N only, 4.2); chalk stroke weights (wall line, footprint, Portal glyph scale); the seen-edge line; the Traced contour (solid chalk with gaps); the fog bank's form, value ceiling and cutaway low cap. All through greyscale and colour-blind at 4 rotations, early, because these states are on screen every second.
   - **A required row in every Tone Sheet (per theme):** the fog-bank tint, checked against that theme's Void backdrop and Remembered grade, and the theme's gate pairs from 4.2 ("Seen vs Remembered", "Chalked vs Void backdrop", "Remembered vs Chalked"; in the Law themes "Remembered hazard vs Seen hazard vs environment").
2. **Afterimage kit:** 8 role-glyph paper cut-outs with a dashed cut line on a torn base, in the four states of 4.2 (Fresh, Persisting, Old hollow, Short half-height), one static look each; the route pip; the 2- and 3-layer stacked group cut-outs (no numerals). Distinct from unit silhouettes (RC2), hazard figments (RC1), the EV figure frame (EV 4.11) and the False Trail marker.
3. **New registry glyphs:** sight-risk mark (open and closed); ear badge; hearing double contour; noise single contour; patrol arrowhead line, "continues" three-dot ellipsis, forecast band and horizon bracket; loose-end thread tuft; told pip; projected pip; the Waymark tack and its 6 inner shapes (chosen outside the pattern registry); Listen bubble and its mechanism and Stir-tell glyphs; front-square wall projections; the Lodestring **spool** mark (M3; a spool with a trailing thread end, never a knot, which is the approved Windfall knotted-sack mark; greyscale-gated against it).
4. **Map presentation:** paper or vellum in the global UI skin with the theme compass, as the plain default.
5. **The Dream map skin (agreed as an M3 skin; the split is a new lead's call).** The **M3 skin** is an option over the plain default, gated by the same greyscale, colour-blind and glance gates: per-theme map paper (9 textures; the Grim Surreal identity carried by paper, not by board effects) and a pencil-to-ink transition of ≤ 0.3 s that never delays input. The **full Dream treatment** (veils, frontier motifs, ink wobble) is a **Later** client question over the M3 skin, never floor-level square-kind materials (RC1+), never idle motion on walkable squares.
6. **M3 showcases:** the Lodestring string along walls (pattern and value per 4.6.2); the "the string ends here" card frame; the **Floor Atlas** (each floor's final map in paper style with route, finds, death or boss mark and "Treasures found k of n"; **no rooms-entered fraction**, after the check: the drawn map already shows it, and a persistent coverage fraction is what IN6 bans; never a score); the death-recap fatal-room thumbnail with the killing chain's sources marked.
7. **No idle motion:** the referent-highlight pulse and the string become static under reduced motion; Afterimages never animate; the fog bank is static.

**Registry table (lead's call, after the check; the RS owner adds the rows; *illustr.* where a value appears).** Every segment 10 mark, with its slot (4.9.3), tier (4.9.2), modes and fallback. Pattern ids are assigned by the RS owner.

| Mark | Slot | Tier | Counts as mark | Modes | Fallback when the slot is taken |
|---|---|---|---|---|---|
| Knowledge-state grade, Chalked footprint, fog bank | fill | N | No | All | — (never displaced) |
| Seen-edge line | fill edge (no line-family slot) | N | No | All | — |
| Traced contour | map only | N | No | Map, minimap | — |
| Afterimage (and its awareness and activity glyphs) | sprite, on the square | M | Yes | Explore; Short in combat | Hover and Inspect |
| Told pip | pip | M | Yes | Explore; hover-only in combat (Guided: on) | "+" pip, hover |
| Projected pip | pip on the Stir decal | D | Yes | All | Never displaced (room clock) |
| Loose-end tuft | map only | W | — | Map, minimap | — |
| Listen badge (board) / bubble (map) | pip on the door frame / map | W | Yes | Explore; hover-only in combat | Hover |
| Waymark tack | glyph | M | Yes | Explore, map | Pip, hover |
| Lodestring spool mark | map only | M | — | Map, minimap | — |
| Lodestring string (world trace, ≤ 1 s) | line | A | No | Explore, while shown | — |
| Noticing bands, watch lines | line | I | No | Hover | The single I layer |
| Patrol route and forecast band | line | I | No | Explore (the I layer), map | Hover-only (Q2 cut step 2) |
| Hearing contour, noise contour | line | I | No | Prompt; aiming (borrow) | — |
| Travel path | line | A | No | While hovering or walking | — |

- **Pending request (charm abilities design CA-27; M2 for Trophy Tally's pips, M3):** the charm pip pair (one plate mark on a plain rounded backing, no sack, knot or spool outline) joins this registry as a non-segment-10 row, its five states (ready, used, waiting, stored, idle) in the RC5 pattern registry, greyscale-gated against the Windfall knotted-sack mark and the Lodestring spool. Not a rule until this owner and the RS owner rule (charm abilities design 6).

**Line-style registry (lead's call, after the check).** No two families differ only by hue or only by dot spacing; each is checked by the glance test (9.3).

| Family | Stroke | Weight | End | Layer |
|---|---|---|---|---|
| Seen-edge line | Solid, crisp | Thin | None | Board |
| Traced contour | Solid chalk, gaps at wall-run ends | Medium | Open | Map, minimap |
| Patrol route | Solid with arrowheads every 3 squares | Thin | Three-dot ellipsis ("continues") | Board (I), map |
| Forecast band | Segmented band along the route | Wide, translucent value step | Horizon bracket | Board (Patrols layer), map |
| Travel path | Footprint dots | Medium | Destination ring | Board, map |
| Listen tether | Fine hairline | Hairline | Bubble | Map |
| Lodestring bearing | Long dash | Medium | Arrowhead | Map |
| Lodestring string | Light-cored twisted two-strand | Medium | None | Board (≤ 1 s), map |
| Hearing contour | Double solid | Thin | Closed | Board (prompt), map |
| Noise contour | Single solid | Thin | Closed | Board (aiming) |
| Threat reach (approved) | Outline; ready tint adds a dash | — | Closed | Board |
| Hunch rim, trap link, EP fall-back tether, leader aura, leash ring (approved) | As approved (dotted rim; dashes) | — | — | Board |

**The forecast band (lead's call, after the check).** On the Patrols layer, the projected occupancy over the horizon is drawn as a segmented band along the known, timed route, ending in a horizon bracket; the one-lap band is visibly longer than the 20-tick one. No numerals on the board. The hovered or Inspected square shows the window as text ("in range +4 to +8 ticks, if undisturbed"). A withdrawn forecast removes the band and shows "route unknown since tick N" in hover.

**Art and tech budget (lead's call, after the check; counts *illustr.*):**

| Asset | Count | Wave | Owner | Gate |
|---|---|---|---|---|
| Remembered grade shader; seen-edge line shader (union line of sight); chalk set; fog volume with cutaway cap | 4 | M1 | Tech art | Greyscale, colour-blind, 4 rotations |
| Afterimage cut-outs × 2 states (Fresh, Persisting) | 16 | M1 | Art | Role-ID gate |
| Afterimage Old and Short states; stacked group cut-outs | 16 + 2 | M2 | Art | Role-ID gate |
| Registry glyphs (item 3) | about 20 | M2 | Art; RS owner rows | Greyscale, glance test |
| Map skin (plain), map UI, binding prompts | 1 set | M2 | UI art | Glance test |
| Per-theme fog tints | 9 | With each Tone Sheet | Art | Per-theme gate pairs |
| Lodestring string, spool mark, card frame | 3 | M3 | Art | Greyscale vs Windfall mark |
| Floor Atlas page, death-recap thumbnail | 2 | M3 | UI art | — |
| Dream map skin papers | 9 | M3 | Art | Same gates as the default |

**Audio:**
- No new reserved gameplay cue (RC7 unchanged).
- UI stings in the global timbre, **checked against the full RC7 reserved list and the HZ, TL, LP and BR timbre tables through `ReservedCues`, and each run through the 85% audio blind-ID gate** (after the check; replaces the draft's three-item exclusion list).
- **Segment 10 timbre row (lead's call, after the check):**

| Sting | Timbre | Kept clear of |
|---|---|---|
| Map open and close | A soft cloth or vellum slide | The Rattle Wire's paper or bead shaker |
| Fact gained | A soft ink-drop or blot | The Gramophone's needle-scratch |
| Waymark drop | A soft felt tap, no click | Jam's metallic click, the Lullaby Glyph's wind-up click |
| Travel start and stop | A soft breath of air, no click and never a tick | The Dream tick |
| Lodestring use (M3) | A short, soft swell in the global UI timbre, never a plucked or bowed string | The Windfall timbre row (plucked, dry string) |

- Request to the RS owner: whether a "something new seen" travel-stop sting counts as a gameplay cue; if refused, visuals only.
- **Pending request (charm abilities design CA-27; M3):** the two charm output cues (Warded, Helped; placeholder timbres) get RS timbre-table rows, are tagged for `ReservedCues` and run pairwise in the 85% audio blind-ID gate above; pending the RS and audio owners (charm abilities design 6).

---

## 7. New mechanics considered

| Mechanic | Source | Verdict | Where |
|---|---|---|---|
| Chalk on entry (room shell) | C, N1 | **Adopt** M1 | 4.1.4 |
| Trace on sight | C | **Adopt** M2, map only | 4.1.5 |
| Shell on sight | A | Reject (told plainly) | 0.1 brief question 1 |
| One-square Pocket | B | Reject (told plainly) | 8 |
| Sight-shadow reading | C, N1 | **Adopt**, worded "out of sight (not out of hearing)" | 4.1.4 |
| Pencil, chalk, ink map grammar | N2, C | **Adopt** in one style; Dream skin M3 option, full Dream Later | 4.2, 6 |
| Listen bubbles | N2 | **Adopt** M2 | 4.3.5 |
| Activity-keyed Afterimages | Numbers, UI | **Adopt** M1/M2 | 4.2 |
| `PatrolForecast`, earned, windowed | N3, numbers, critics | **Adopt** M2, gated (agreed, Q2) | 4.8.1 |
| Loose ends, travel, Next loose end | N4 | **Adopt** M2, no count | 4.4.2, 4.7 |
| Noticing pre-stop | Fun critic | **Adopt** M2 | 4.7 |
| Re-sight stop narrowing | Drafts | **Adopt** M2 with the pre-stop | 4.7 |
| Detour quote | N5, incentives | **Adopt** M2 | 4.4.3 |
| Behind-you line | N6, incentives | **Adopt** M2, arena confirm only | 4.4.4 |
| Emptied field; totals principle | Incentives | **Adopt** M2 | 4.4.5, 4.4.1 |
| Lodestring and F-TREASURE | A, B, C, N7, incentives | **Adopt** M3; never a secret cache (agreed, Q5) | 4.6 |
| Tithe Bowl: Lodestring or Surveyor's Sketch | N8 | **Adopt** M3 | 4.6.3 |
| F-TREASURE through Yield | Incentives | **Adopt** M3, once per floor | 4.6.4 |
| In-sight and hidden-from pips; bands | N9, A | **Adopt** M2 | 4.8.3 |
| Watch lines; noticing step tag | N9 | M3; tag conditional on EP-41 | 4.8.3 |
| Waymarks and auto-marks | N10 | **Adopt** M2 | 4.3.3–4.3.4 |
| Walls Have Ears (batched Listen) | N11, incentives | **Request** M3 | 4.5.2 |
| Cased | Incentives | **Request** M3, conditional on TL-6 | 4.5.2 |
| Ear Trumpet | B, incentives | M3, conditional (agreed, Q3) | 4.5.2 |
| Floor Atlas | N12 | **Adopt** M3 | 6, below |
| Dreambook pages | N13 | Later, cut first | — |
| Beckoning Glimpses, `SideRoomPayoffMix` | Incentives | **Request** M2 | 4.4.6 |
| Suspend-save | Balance critic | **Adopt** M2 (agreed, Q1) | 4.12 |
| Vendor Chalk, Floor Rubbing, Floorplan, Loupe, Cartouche, Augur's Knot, Plumb Line, Hedge Bestiary, Déjà Vu, Familiar Halls, Plumb the Halls, Dream Survey | B, C, A, incentives | Reject or Later | 4.5.3 |
| Range Ahead, Scout Ahead | B, incentives | Later client question (MAIN 4.3 decided) | 4.5.3 |
| Off-path loot-rank lean | B | Reject (told plainly) | 8 |
| Auto-explore | — | Reject; Later accessibility option only | 4.7 |
| Omen "Drawn in Chalk" (floor starts Sketched; "best remaining" never shows) | N8 | Later, RS Omens owner request | — |
| Units block line of sight | Drafts | Later client question | 4.1.2 |

**Adopted in full** where not already fully specified above:
- **Floor Atlas (M3).** At the stairs the core snapshots the floor's final `PartyKnowledge` into the run record as a static paper map with the walked route (from the command log), find locations, the boss or death mark and "Treasures found k of n" (no rooms-entered fraction; after the check). Profile-side display, never a score or achievement (LE 8.2), never read by rules. The camp dream track shows the previous floor's page.
- **Beckoning Glimpses (EV request, M2)** and **`SideRoomPayoffMix` (FL, LP, EV, M2)** as 4.4.6.
- **The noticing pre-stop (M2)** as 4.7.

---

## 8. Numbers with fixes (toy, not simulator; all illustr.)

Toy scripts (scratch, not project files): `seg10sim.py`, `ablate.py`, `analysis2.py`, `analysis3.py`, `analysis4.py`, kept in the design lead's scratch area (Appendix B). 1,200–1,500 seeds per cell; all figures *(illustr.)*. Calibration: 0.15 s per tick, 4 s per click. The toy has no fights, rests, Curiosities or loot summaries, so **read deltas, not absolutes**. BASE = approved surfaces plus the M1 minimap.

### 8.1 Feature deltas, each alone, F4–5 party 4 (means)

| Feature | Lean (walker / human clicks / min) | Balanced (human / min) | Completionist (human / min) |
|---|---|---|---|
| Chalk on entry | 0 / −1.1 / −0.09 | −3.7 / −0.28 | −9.5 / −0.73 |
| One-square Pocket | 0 / −0.3 / −0.02 | −1.1 / −0.08 | −2.9 / −0.21 |
| Travel | 0 / 0 / 0 | −1.4 / −0.09 | 0 / 0 |
| Re-sight narrowing | −1.1 / −1.1 / −0.08 | −1.8 / −0.12 | −2.0 / −0.13 |
| Pointer | +0.6 / +0.7 / +0.05 (VP +0.35) | −1.3 / −0.15 (chase 23 → 12 s) | ≈ 0 |
| Layout chart | 0 | +0.2 / 0.00 (VP 0) | +0.3 / +0.02 |
| Full map, 4 opens × 3 s | +0.20 min | +0.20 | +0.20 |

### 8.2 R against BASE, F4–5
- As specified (4 opens): lean **+0.09 min (fail)**, balanced −0.31.
- With 2 opens: lean **−0.01 (pass)**, balanced −0.41.
- Without re-sight narrowing: lean +0.16. Without chalk: lean +0.17.
- The completion premium falls from +64–71% (BASE) to +35–46% (R).
- `TallyChaseSeconds` mean 23 s (BASE) and 11 s (R), median 0; passes LE 8.2's ≤ 30 s.

### 8.3 Patrols
- P(collision per crossing): blind 0.418; route plus memory 0.312; forecast capped at 20 ticks 0.254; uncapped 0.050.
- Afterimage staleness at 20 ticks: Patrolling 100%, Searching 100%, Stationary 3%, Dormant 2%.
- Auto-Probe / Mild Plain leak under the true-state reading: 0.126 per floor.

### 8.4 Clutter, P(> 12 marks)
- Combat: BASE 2.6%, A 8.7%, B 12.3%, C 14.2%, R 8.9%. **R fails the 5% combat gate in the toy** (told plainly, after the check); the combat suppression (XN5) is unmeasured. Levers on a miss at the pre-M2 sign-off check, in order: told pips and Listen badges hover-only in every density; Short Afterimages only for units that left sight this round; arrival arrows only for known groups' next round; then the 14.2 clutter row.
- Explore: BASE 0.3%, A 27.6%, B 18.1%, C 20.5%, R 5.5%.

### 8.5 Failures and fixes

| Id | Failure | Fix |
|---|---|---|
| XN1 | Lean arm fails the binding line with 4 map opens | Minimap duty (4.3.1); `MapSecondsPerFloor` a report and cut-order trigger, not binding on player behaviour (told plainly); pointer minutes booked inside the line |
| XN2 | Drafts' "travel −3 to −6 clicks" not reproduced; the walker charges no move orders | Read `Seg10ClickDelta` on both arms; walker credits only re-sight narrowing; travel and chalk credits on the human arm |
| XN3 | Model B worst on clock and value | Chalk on entry replaces the Pocket; off-path loot lean rejected |
| XN4 | Model A explore clutter 27.6% | Loose ends map-only; forecast on the Patrols layer only |
| XN5 | Combat clutter 9–14% in every model (R 8.9% against the 5% gate) | Combat suppression (4.9.4), keeping in-room Short Afterimages; unmeasured, so the combat gate is a pre-M2 sign-off check (8.4) |
| XN6 | Uniform 20-tick fade fits nothing | Activity-keyed Afterimages |
| XN7 | 20-tick forecast cap discards most value; uncapped removes patrol fights | Earned, windowed, event-withdrawn forecast; 20 ticks default, one lap with a trained watcher or Cased; `PatrolFightRate` gate (agreed, Q2) |
| XN8 | TL 6.6 / auto-Probe leak 0.13 per floor | Known patrols only plus the never-Seen-crossing gate (agreed, Q4; read as Unknown or Chalked after the check) |
| XN9 | Folded Floorplan is a 100% secret radar | Charts are node-only; `ChalkNeverStub`; vendor chart items cut |
| XN10 | Layout charts show no measurable value | Only the Surveyor's Sketch survives, as a camp option |
| XN11 | Auto-explore forces full clears and 5× patrol exposure | Rejected |
| XN12 | Treasure count: passes (moderate pull, not completionism) | Kept; emptied field added |
| XN13 | Pointer: secrets found unchanged in the toy, but the toy did not model stub-plus-room inference | Secret caches excluded from F-TREASURE (agreed, Q5) |
| XN14 | Side-room band 55–85% is reachable only on a player mix (lean 0.08–0.12) | `SideRoomVisitRate` per policy, banded on human sessions only |
| XN15 | Detour quote value inconclusive in the toy | Measure `DetourRegret` and `DetourQuoteHoverRate` in playtest; 3 lines by default |

### 8.6 Told plainly
- The noticing pre-stop adds an estimated +0.3 to +0.8 walker stops per floor *(illustr., unsimulated)*, partly offsetting re-sight narrowing (−1.1). The binding line must be re-run with the pre-stop before M2 sign-off; the cut order (9.5) applies on a miss.
- Party-1 and party-2 lines are reported (information coin spent, `ExploreClicks` on F1–2), because small parties pay most of any lane-gap cost.
- **(Added while writing.)** The toy was not re-run after the client's answers or the new lead's calls; the camp-4 Lodestring rule (4.6.3) is unsimulated and changes no figure above.
- **(After the check.)** Also unsimulated, to re-run before M2 sign-off: the never-Seen reading of the auto-action gate (4.8.2; its per-floor click cost), the step check and hazard re-plans of travel (4.7), the replaced cut steps (9.5), Next loose end cycling, and R's combat clutter after suppression (8.4). The continuous suspend slot (4.12) changes no figure.

---
## 9. Data, validator V18 and simulator (changes XI-30, XI-31)

### 9.1 Records (`info.json` and records, *illustr.*)

```
fog{ chalkOnEntry:true, chalkFootprint:true, traceOnGlimpse:true, stubMaxSquares:3, unitsBlock:false, downedSee:false }
afterimage{ freshTicks:5, searchingRemoveTicks:5, oldAfterTicks:60, combatKeepRounds:2, groupCollapse:5, roundAsTicks:1 }
listen{ oldAfterTicks:20 }
forecast{ defaultHorizon:20, trainedOrCasedHorizon:"lap", lapCapTicks:60, earnWatchSquares:4, earnedBy:[segmentWatched, cased], listenEarns:"portalWindowOnly", gatePatrolFightRateVsBase:0.6 }
cased{ ticks:2 }
dim{ toolMode:0.5, sightShadow:0.5 }
travel{ nextLooseEnd:true, noticingPreStop:true, stopOnResight:true, chalkedInEnteredSpaces:true, stepMustBeSeen:true }   // stopOnResight true until the FL owner rules XI-9 (pending) (cross-document comparison 2026-11-23, CC-124)
waymarks{ maxPerFloor:8, shapes:6, labelChars:12 }
budgets{ hudRegions:7, plateMarks:2, telegraphLayers:3, infoLayers:1, marksPerScreen:12, exploreOverRate:0.10, combatOverRate:0.05, stratumRows:4, detourLines:5, firstSightToastsPerFloor:3 }
save{ suspendSlots:1, deleteOnLoad:true, knowledgeForwardOnly:true, writeEveryCommand:true }
lodestring{ floors:[2..4], cap:1 }
PartyKnowledge.roomFlag.source ∈ { entry, glimpse, F-ROOM, overlook, sketch, treasure }
PartyKnowledge.sketchEdges[]{ roomA, roomB }   // never Portals, loose ends or travel edges
PartyKnowledge.routeSeen{ groupId, segments[], observedTiming[], earnedBy, trainedWatcher, earnedTick, horizon, withdrawnTick?, lastSeen{square, tick, activity} }
SurfaceRecord{ id, ownerSegment, wave, region, tier, slot(fill|rimOuter|rimInner|glyph|pip|line|sprite), countsAsMark, modes[], trigger(always|hover|inspect|card|mapLayer|event), sourceQuery, patternId, access, disclosure }
RevealSource{ id, writes:[Traced|Chalked|Fact], scope, excludes[], cost{clicks,ticks,coin} }
InfoItemRecord{ id, form:counter|ritual|rider|campService|fact|eventGain, floors[], cap, stockRule(always|laneGap(skill, campEntrySnapshot)), suspendWhileLanePresent, excludes[] }
FactCategory{ F-TREASURE, selector, rank:[tierDesc, roomMacroYX, containerYX], qualifiers:["plain cache","behind risk"], qualifierSource:socketClass, perFloorCap:2, yieldPerFloorCap:1, floors:[2..5] }
```

- **Client keys, not tuning (new lead's call):** `forecast.defaultHorizon` 20 and `trainedOrCasedHorizon: "lap"` (Q2; `lapCapTicks` stays *illustr.*), `save.suspendSlots` 1, `deleteOnLoad`, `knowledgeForwardOnly` (Q1), the Ear Trumpet's `stockRule: laneGap(Perception)` (Q3), and the secret-cache entry in F-TREASURE's `excludes[]` (Q5). Changing any of them returns to the client, except through the three Q2 cut steps of 4.8.1, which Q2 pre-approves. `save.writeEveryCommand` is a lead's call after the check (4.12), not a client key.

### 9.2 V18 `Information` (change XI-30; split like V13 and V16)

- **V18s (static):** `SurfaceRegistered` (every row of 5 has a record with a core source query, a slot and a `countsAsMark` flag); `PatternNoHueOnly`; `NoHoverOnly` (information and every segment 10 action have a non-mouse path); `BoardDigitIsClock` (Afterimages among the fixtures); `WallMarkProjected`; `BudgetWorstCase` per mode, from the 6 registry table; `CardFit` at 150%; `LayerHasLegend`; `FactHasMapDraw` (F-KEY on route pips included); `RevealNeverUnits`; `InfoItemLaneSafe`; `InfoNeverWritesLoot`; `PointerTextBounded` (tier word plus qualifier and the fixed hover line only); `LaneGapStock` (camp-entry snapshot; suspended while the lane is present); `WaymarkGlyphDistinct`; `NoCompletionStat` (no field counts map coverage or loose ends for display, except the named FL-20 rooms tally, 4.4.4).
- **V18g (generation, per floor; amended by SV-20, M1 model, M2 complete):** the graph rows, asserted at AP2: `SketchNeverSecretNode`; `TreasureSelectorLegal` (exclusions incl. secret caches; role-blind ranking; qualifier from socket class); `TreasureFactCap`; `NoF1Pointer`. Failure class: this section's named action where it names one, otherwise `defect` (seeds and validation design 5.1, 5.2, 5.4).
- **V18r (runtime assertions, on every `KnowledgePass`; SV-20; lead's call; M1 model, M2 complete):** the knowledge rows, moved from V18g because they read `PartyKnowledge`: `MapNeverUnknown`; `ChalkNeverContents`; `ChalkNeverStub` (every non-sight source; the `ChalkOutline` closing line withdrawn once the stub is Seen); `RouteKnownOnly`; `ForecastKnownOnly` (timed segments only; nothing past a "continues" end without a timed lap; a heard-only patrol gets its Portal window only); `BubbleNeverPositioned`. Class `runtimeAssert`: a crash in dev and CI; in release fail closed (suppress the leaking surface, log `RuntimeAssert{check}` as P0, never block play) (seeds and validation design 5.1, 5.2).
- **Phase model (SV-20):** every check has a qualified id `V18.CheckName`; `s` and `t` parts fail the build, `g` and `r` parts act by their registered class (seeds and validation design 5.1).
- **V18t (tests):**
  - `LosSymmetric` (every pair of **transparent** squares, every template, 4 rotations, Portal states); `LosVsLineAgree`;
  - `FogEqualsRules`: every replay tick, the Seen squares equal the union over living, non-Downed party units u of {s : `CanSee`(u.square, s)} (plus blockers lit by the 4.1.2 rule), and a group's first-line-of-sight noticing trigger fires if and only if one of its member squares is in that set (restated after the check);
  - **`KnowledgeLeak`**: two states differing only in what the party does not know — a lurker's square, an unseen group, an unseen patrol's position, container contents, an unfound secret, a stub before sight, a boxed lurker, a Hidden member of a Cased group, an unseen wall, open door or piece, a glimpsed-only or heard-only patrol, a withdrawn forecast — give byte-identical view models and previews, including auto-Probe, Mild Plain, travel stops, the noticing pre-stop, `KnownCanSee` and `KnownNoiseReach`. **One listed exemption:** `TreasureTarget()` for a paid or earned act, limited to {room node, tier word, qualifier} (4.6.1);
  - `AfterimageHonesty` (no Afterimage changes while unseen); `ReplayView` (follower and monster steps, monster-opened doors, a mid-pass Hairline find, combat starting in the pass); `ProfileBlind`;
  - `SaveReloadKnowledge`; `KnowledgeReloadMonotone` and `DeathNotRewindable` (process kills after a Lodestring use, a Yield fact, a Tithe Bowl purchase, a look through a door, a death and a Down; 4.12); `WaymarksOutsideKey`;
  - `PointerNeverPreDoor`; `LodestringDoorBlind`; `RoleBlindOrder` (permuting `roomId`s with geometry fixed gives identical Next loose end, points-of-interest and F-TREASURE choices);
  - `TravelNeverEntersKnownHazard`; `BehindYouFullClearZero`; `SpentFieldRespectsBestRemaining`; entered-flag cases (doorway, arena doorway, Downed unit shoved); loose-end cases (peek-then-leave, an F-ROOM-Chalked room);
  - `PreviewExactness` extended to the noise preview and Detour quote; it is the structural partner of the zero gates `ForecastLethalSurprise` = 0 and `PreviewExactness` 100% (SV-25; M2; seeds and validation design 6.6).

### 9.3 Reports (M2 unless noted; targets *illustr.* except the stated gates; definitions after the check)

Every report is computed from the core's event log by the simulator's instruments: Seg10 rows on the W walker, `MarksPerScreen` on V, the glance test on H (SV-21, read; seeds and validation design 6.1, 6.12).

- **Binding:** `Seg10ClickDelta` (both arms), `Seg10MinutesDelta`.
- **Gates:**
  - `PatrolFightRate` (agreed, Q2; ≥ 60% of BASE) = combats per floor, F1–5, whose first noticing roll was made by a group whose activity was Patrolling, divided by the same count in the BASE arm (`XI.BASE`; EP owns the row; SV-22, SV-24; seeds and validation design 6.2, 6.10). **Walker forecast policy:** wait when an earned window overlaps the next 3 path squares; otherwise walk on.
  - `MarksPerScreen` (4.9.5) in explore and, as a pre-M2 sign-off check, in combat; the glance test (first-time testers name each visible line family ≥ 80%, on the board and the minimap).
- **Cut triggers:** `MapSecondsPerFloor`, with a plan of 6 s per floor *(illustr.)*.
- **Reads:** `MapOpenRate`, `TravelClicksSaved`, `NoticingPreStops`, `PocketClearClicks`, `LooseEndsAtExit`, `SideRoomVisitRate` per policy, `DetourRegret` (the share of quoted detours taken whose realised value fell below the quoted Sight line), `DetourQuoteHoverRate`, `BeckonFollowRate`, `TallyChaseSeconds`, `LostTime`, `AfterimageMisreadRate` (the share of player commands that target, or path around, a square holding an Afterimage while the true unit is elsewhere; combat included), `FirstRunExplainerCount`, `InfoCoinByPartySize`, `WaylaySurpriseRate`.
- **Q3 trigger (new lead's call):** `SoloListenClickShare` (4.5.2), read at the M3 schedule review.
- **M3:** `LodestringBuyRate`, `LodestringUseRate`, `LodestringRegret` (the share of uses whose target is uncollected at floor exit, plus the minutes spent on uncollected targets), `LodestringMinutesDelta`, `YieldCategoryMix`, `SketchBuyRate`, `SecretFoundRate{lodestring}` (renamed from `SecretFindRate`, SV-24, M2: one keyed row with FL's and LP's `SecretFoundRate`; seeds and validation design 6.10), `EarTrumpetClickSavings`.

### 9.4 Simulator arms
The FL 11.4 walker (lean = gate arm, balanced, completionist) plus a human-session arm that charges move orders and pocket clearing; per-feature ablation; party 1, 2 and 4; the BASE arm for the `PatrolFightRate` baseline. **Amended by SV-22 (M2):** qualified names `FL.Lean` (the gate arm), `XI.Balanced`, `XI.Completionist`, and the toggle baseline `XI.BASE`; the human-session arm runs under the H instrument and the human-session plan (SV-27, read) (seeds and validation design 6.1, 6.2, 6.11).

### 9.5 The segment 10 clock line (change XI-31 ⚑; lead's call; M2)
- Beside TL-38, EP-33, LP-10, EV-23 and BR-40: **net ≤ 0 minutes and ≤ 0 clicks on F4–5 at party 4**, on both arms; the companions' CMP 7.4 levers untouched (**agreed** gate kept). The line is a BIND row summed in `FloorClockLedger`, and a miss fires the self-cut order below before any CMP 7.4 lever (SV-26, read; seeds and validation design 6.3, 6.8, 6.9). **The human vertical slice is a read, not a gate (after the check):** an A/B with segment 10's features toggled where the slice allows it, otherwise reported beside the arms.
- **Self-cut order on a miss** (player-unrequested items first, map usability last; steps 1 and 3 replaced after the check, because the draft's steps were already the defaults):
  1. the Detour quote shows only the Cost line by default (the rest on Inspect);
  2. the Lodestring limited to F3–F4;
  3. the explore-mode board information layer off by default (patrol routes on the map only);
  4. the Stratum Card opens collapsed;
  5. the minimap absorbs the most-opened layer.
- If the FL owner refuses re-sight narrowing (XI-9), M2 ships with the new steps 1, 3 and 4 already applied, and the toy is re-run on that set before M2 sign-off (unsimulated; 8.6).
- The noticing pre-stop's cost (8.6) is re-run before M2 sign-off.

---

## 10. Staging by wave

| Wave | Contents |
|---|---|
| **M1** | XI-1 states (Unknown, Chalked, Seen, Remembered), the entered rule and readings; XI-2 `CanSee`, `KnownCanSee`, `KnownNoiseReach`, `LosSymmetric`, `LosVsLineAgree`; the 4.1.3 `KnowledgePass`; XI-3 Chalk on entry; XI-5 static memory rows and basic Afterimages; XI-7 minimap; Threat layer; XI-16 digits; XI-17 base grammar and fog bank; XI-20 accessibility base; the `KnowledgeLeak` and `FogEqualsRules` harness |
| **M2** | XI-4 Trace and the FL 8.1 amendment with F-ROOM; full Afterimages; XI-6 routes, `PatrolForecast`, auto-action gates, `PatrolFightRate` (agreed, Q2 and Q4); XI-7 full map, layers, marks, Waymarks, Listen bubbles, fact drawings; XI-8 travel, Next loose end, To the Downstair, noticing pre-stop; XI-9 re-sight narrowing; XI-10 loose ends, Detour quote; XI-11 Behind-you line, rooms tally, emptied field; XI-12 pips and bands; XI-13 hearing; XI-14 charters; XI-15–XI-19 surfaces; XI-20 Inspect parity; XI-21 disclosure; XI-27 owner requests (beckoning Glimpses, payoff mix, False Trail shape); XI-28 suspend-save with the continuous slot (agreed, Q1; per-command writes after the check); XI-30 V18; XI-31 reports and clock line |
| **M3** | XI-22–XI-25 F-TREASURE, Lodestring, Tithe Bowl, Yield (secret caches never, agreed Q5); XI-26 Ear Trumpet (conditional on `SoloListenClickShare`, agreed Q3); XI-27 Walls Have Ears, Cased (conditional on TL-6), Overlook writes Chalked; watch lines and the step tag (conditional on EP-41); XI-29 Floor Atlas, death-recap map, codex hosting; the Dream map skin (map paper, pencil-to-ink), gated |
| **Later** | The full Dream treatment (veils, wobble, frontier motifs) (client question); units blocking line of sight (client question); Scout Ahead (client question); auto-explore accessibility option; layout item only if `LostTime` misses; Dreambook pages; Omen "Drawn in Chalk"; controller polish; screen-reader text; DH 17.13 undo reading |

---

## 11. What segment 10 owns, and hooks handed on

| Owner | What |
|---|---|
| **Segment 10** | `PartyKnowledge` and `CanSee`; chalk, trace and memory rules; Afterimages; `PatrolForecast` and its consumers' reading; the minimap and full map; travel, Next loose end, stops; loose ends, the Detour quote, the Behind-you line, the emptied field; the stealth and hearing surfaces; the surface register, regions, priority, slot model, budgets, card queue, naming; Inspect parity and accessibility; progressive disclosure; F-TREASURE's drawing and the Lodestring's display; the suspend-save rules; V18 and the segment 10 reports; the clock line |
| **FL owner** | 8.1 amendment (XI-4), 6.9 stops (XI-9), the corner rule reading (XI-2), the rooms tally (XI-11), Overlook writes Chalked (XI-27), `SideRoomPayoffMix` |
| **Enemies owner** | 12.1 route reading (XI-6), 12.2 lurker-band candidates exclude Downed units (XI-2), 12.4 wording (XI-13), 13 Lore and odds (XI-12, and Cased's odds band, XI-27), 14.6 F-TREASURE (XI-25), the "explored" and "unmapped" readings (XI-1) |
| **EP owner** | EP-41 (via the client), EP-28, `PatrolFightRate` (XI-6), Spoor projection (XI-18) |
| **TL owner** | TL-5 and auto-Probe readings (XI-6; agreed, Q4), TL-25 vendor row (XI-26; agreed, Q3), TL-6 Trap Eye (Cased) |
| **LE owner** | Tithe Bowl (XI-24), exposure ruling, Wax Key guard, pointer VP band, tier marks on map hover (XI-7), "Treasure strip" wording in 6.15 and 8.2 (XI-19), the floor-strip rooms cell (XI-11) |
| **EV owner** | F-TREASURE category (XI-22), beckoning Glimpses, chips reading, codex EV-37 |
| **LP owner** | `SideRoomPayoffMix`, by-source hover narrowing (XI-23), Treasure strip naming (XI-19) |
| **BR owner** | One line on the arena-entry confirm (XI-11) |
| **RS owner** | Registry rows, RC6 tiers, gates (XI-15, XI-17, XI-20), codex hosting (XI-29), the travel-stop sting question (6) |
| **DH owner** | In-sight pip not a band (XI-12); DH 17.13 reading (Later); fatal-room thumbnail (XI-29) |
| **CN owner** | Roster +1 counter (XI-23), Ear Trumpet row (XI-26) |
| **ITEMS owner** | ITEMS 10.3 basic-vendor list and 18.1 vendor stock data: the Ear Trumpet stock row (XI-26) |
| **Arcanist, Shade, Herald owners** | Walls Have Ears; Cased and the False Trail shape; HX-6 Pick the Question (XI-25, XI-27) |
| **CMP owner** | Informed: no coverage-strip line for the Ear Trumpet (XI-26) |
| **Main design owner** | MAIN 6.6: the UI annex (XI-7), `uiMemory` (XI-21), the continuous suspend slot (XI-28); MAIN 4.3 reading (XI-2) |
| **Segments 11–12** | 4.13 (answered: seeds and validation design 11) |
| **Camp and hub** | 4.13 (answered: camp and hub design 11; CH-22 applied in 4.10) |
| **Starting kits** | 4.13 (answered: starting kits design 11; SK-9 applied in 4.10) |
| **Charm abilities (segment 16, TK 24.2)** | 4.13 (taken up: charm abilities design 11; CA-27 pending with this owner, with the RS and audio owners) |

**Hook answers:**

| Hook | Answer |
|---|---|
| FL 13 (fog, minimap, overlay, Listen UI, Hairline display, hearing outline, interrogation facts, auto-walk, Overlook, Stratum Card line) | 4.1–4.3, 4.3.6, 4.7, 4.8.4, 4.9.8, 5 |
| FL 6.9 travel request | 4.7 |
| RS 13 (camp card, Two Doors, Stratum Card, dream track, Lore reveal) | 4.9.8, 5, 6 |
| EP 13 | 5 |
| TL 6.23 | 5, 4.8.2 |
| HZ 6.18 | 4.9.6, 5 |
| RT 6.12 | 5, 9.2 `PreviewExactness` |
| DH 17 | 5; 4.1.1 (17.13 reading) |
| LE 6.15, LE 8.1 | 4.9, 4.6.3, 5 |
| LP 4.12 | 5; 4.9.9 |
| IR 6.1 | 5 |
| EV 4.12 and its worst-case card fit | 4.3.6, 4.10 `CardFit` (acceptance item), 5 |
| BR 4.16, 4.17 | 4.4.4, 5 |
| ENM 13, 15.8 | 5 |
| ITEMS 10.9 | 4.3.3, 5 |
| CMP 7.x | 5 |
| C 15 | 5 |

---

## 12. Binding requirements and how they are met

| Requirement | Met by |
|---|---|
| The brief: fog states, reveal rules, symmetric line of sight, remembered detail | 4.1, 4.2 |
| The brief: map, accessibility, marks | 4.3, 4.10 |
| The brief: incentives beyond the main path against the clock | 4.4, 4.7, 9.5 |
| The brief: information items, abilities, spells and traits | 4.5, 4.6 |
| The brief: every surface consolidated with budget, layering and priority | 4.9, 5 |
| The brief: determinism, accessibility, data, V18, reports, hooks | 4.10–4.13, 9, 11 |
| The six brief questions | 0.1 |
| MAIN 2 / 6.2: no rules in the display | `IKnowledgeQuery` for every rule pixel; `FogEqualsRules`, `KnowledgeLeak`, `ReplayView` |
| MAIN 4.3 symmetric line of sight (decided) | One `CanSee`; `LosSymmetric`; the Downed reading keeps geometry symmetric |
| No light model (ENM 5.1, RS 7.6) | Fog is geometry; no lamps; Unknown is backdrop, never a light radius |
| FL 9.1 Spyhole rejection | IP3, IP5; no sight at a distance |
| FL-17 / FL 19.1 secrets fair, never minimap gaps | Hairline is wall; stubs by sight only; `ChalkNeverStub`, `SketchNeverSecretNode`; secret caches never pointed at (agreed, Q5) |
| TL KF7 no trap total; KB1 | IN5; no item touches grades |
| HZ "visible from reveal"; one room clock digit | Chalk never draws hazards, and never a Void as a Void; travel steps only onto Seen squares; XI-16 |
| Counts only, never locations | Kept for free surfaces; one flagged narrowing for paid or earned pointers (XI-23) |
| LootDoorInvariant, fixed deck, CL 6.3 | IP10; 4.6.5 proof; `LodestringDoorBlind`, `PointerNeverPreDoor` |
| CN 13.7, 16.7 | Items are counters or riders; no identification |
| STAT 7.1 lanes; Wax Key precedent | IP4; lane-gap stock only when absent (agreed, Q3) |
| STAT 7.2 exploration has no clock | IN1; no visible timer |
| RC1–RC9 and the M2 gates | 4.2 grammar, told and projected pips, slot model, registry rows, gate pairs, `NoHoverOnly` |
| CMP 7.4 gate, no new lever | XI-31 clock line, both arms, self-cut order |
| EV 4.4.2 BC2 / profile memory | IP9, `ProfileBlind`, `uiMemory` display-only |
| Main-path identity never leaked | Rooms tally unsplit; ties and rankings by role-blind geometric keys, never by id (`RoleBlindOrder`); F-TREASURE has no off-path key; no "return to path"; Detour quote never implies role |
| Combat is key (standing guidance) | `PatrolFightRate` gate (agreed, Q2); no auto-explore |
| Permadeath not scoutable or rewindable by reload or crash | Suspend-save, forward-only knowledge (agreed, Q1); the slot (base plus journal) brought up to date after every command (4.12) (cross-document comparison 2026-11-23, CC-121); `DeathNotRewindable` |
| XI-n and V18 verified unused | Header |

---

## 13. Changes to approved documents

Applied on 2026-11-23, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; as in the LE, TL, EP, IR, LP, EV and BR precedents, the Flag column also marks rows that only *read* such text without changing it, so that every touch of it is visible **(new lead's call:** this adds read flags to XI-1, XI-3, XI-5, XI-7, XI-11, XI-13, XI-14, XI-15, XI-17, XI-20, XI-21, XI-31 and XI-32, which the proposal's register did not flag**)**. **The client's approval of 2026-11-23 covers the flagged rows** (with Q1–Q5 where a row names one). Told-plainly rows were told in 14.1 and confirmed by the client on 2026-11-23. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; each goes to its owner for confirmation):**
- Explored = entered (FL 6.8, ENM 14.5, EV 4.4.2); unmapped = not entered and interior not Chalked (FL 9.1 Yield; the ENM 14.6 F-ROOM target) (FL, ENM, EV owners; XI-1).
- DH 17.13 "revealed a square" = an Unknown or Chalked square turning Seen, or a new unit sighting (DH owner; Later; XI-1).
- Downed party units contribute no knowledge; `CanSee` stays geometrically symmetric (main design owner; XI-2). Its consequence for ENM 12.2's lurker band is a **change**, not a reading (XI-2 row).
- Sight glances past one corner; movement does not (FL, RT owners; XI-2).
- HZ "visible from reveal" means sight, so chalk never draws hazards (HZ owner; XI-3).
- A map container hover counts as LE 6.15's container hover for tier marks (LE owner; XI-7).
- F-ROOM is drawn as interior Chalked (EV, ENM owners; XI-4).
- The hearing outline shows known groups only (enemies owner; XI-13).
- Q4's "unexplored squares" means never-Seen squares, Unknown or Chalked (TL owner; XI-6; lead's call, after the check, replacing the Unknown-only reading).
- FL 9.2's "treasures: k / n" on the count line is the floor strip's Treasure field (FL, LE owners; XI-19).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| XI-1 | MAIN 2, 6.2 (read); **MAIN 6.6 (decided, read)**; FL 6.8, FL 9.1, ENM 14.5, 14.6, EV 4.4.2, DH 17.13, TL 6.4 `seenByParty` (readings) | `PartyKnowledge` in the core (never Seen; the `ChalkOutline` exception; extended records and sources); the entered rule; explored = entered (flee anchors, EV 4.4.2); unmapped defined (the F-ROOM target); Q4's "unexplored" = never Seen; DH 17.13 reading (Later) (4.1.1) | ⚑ **Decided** (MAIN 6.6, read) | Lead's call; FL, ENM, EV, DH owners confirm | M1/M2 |
| **XI-2** | **MAIN 4.3 (decided; implemented and read, not changed)**; FL 3.1 `CornerCut`, RT 6.2.1 / TR-5 (reading: stated difference); FL 9.2, FL-7; **ENM 12.2 lurker-band sentence (change EP-3): changed, Downed units excluded** (precedents ENM 11.4 Quarry and TL 6.4 / TL-6; citation corrected after the check); C 18.7 | `CanSee`: symmetric shadowcasting over transparent squares, the blocker-visibility rule, blockers, no range, units never block, corner rule (sight blocked by two orthogonal blockers; movement by one), non-Downed knowledge; `KnownCanSee` and `KnownNoiseReach` (4.1.2) | ⚑ **Decided** (MAIN 4.3, read); ⚑ ENM 12.2 / EP-3 text changed (treated as agreed or settled for flagging) | Lead's call (Downed units contributing no knowledge stood as written with the client's answers); told plainly; FL and RT owners confirm; main design owner confirms the reading; enemies owner applies the ENM 12.2 change | M1 |
| XI-3 | FL 8.1 `CutawayPockets`, FL 19.1, `HairlineCue` (read); **FL-17 secrets via RS RC1, RC5 (agreed, read)**; HZ "visible from reveal" (read; HZ owner informed); ABL 8.3; RC1 | Chalk on entry (the wall-bounded footprint, kinds never drawn; the entered rule); stubs by sight with `ChalkOutline`; the sight-shadow wording (4.1.4) | ⚑ **Agreed** (RC1, RC5, read) | Lead's call; told plainly | M1 |
| XI-4 | FL 8.1 minimap data bullet "never unexplored rooms"; ENM 14.6 F-ROOM; EV 4.4.1, 4.12 | Trace on glimpse (renamed Trace on sight, naming pass NP-25) (map only); F-ROOM drawn as Chalked; FL 8.1 → "never Unknown rooms; Traced and Chalked rooms in their own styles" (4.1.5) | — | Lead's call; told plainly (M2, not M3); FL owner applies | M2 |
| XI-5 | **ENM 12.1, 12.4 (settled, read)**; TL 6.4; HZ 6.18, HZ 6 digit rule; **RC1, RC5, RC6 (agreed; registry rows added by RS)** | Remembered table; activity-keyed Afterimages with the Fresh, Persisting, Old and Short states, no numerals, and the combat rule; projected Stirs (solid numeral, `projectable` from the hazard record); F-KEY on route pips (4.2) | ⚑ **Settled** (ENM 12.1, 12.4, read); ⚑ **Agreed** (RC rows) | Lead's call; RS owner adds rows | M1/M2 |
| **XI-6** | **ENM 12.1 "once seen, the route is drawn" (settled)**; **TL 6.6 auto-pick gate / TL-5 (agreed row)**; TL 6.6 lock card; TL 6.4 auto-Probe / TL-7 (lead's call); EV 4.12 chips; ABL 15.1, DH 17.11, EP-16; FL-18 "passing" (read) | Routes on known squares only with "continues" ends; `PatrolForecast` over known patrols, earned in observable terms, windowed over timed squares, heard-only patrols limited to the Portal window, event-withdrawn, 20 ticks / one lap; Easy Plain (Easy renamed Mild, naming pass NP-25) and auto-Probe read it plus the never-Seen-crossing gate, with the act's hearing reach and "due" defined; EV chips read it; the `PatrolFightRate` gate and its cut order (4.8.1, 4.8.2) | ⚑ **Settled** (ENM 12.1); ⚑ **Agreed** (TL-5); **Q2, Q4** | **Client (Q2, Q4, agreed 2026-11-23)**; enemies, TL, EV, EP owners apply | M2 |
| XI-7 | FL 8.1, FL 13; **MAIN 6.6 (decided): flagged addition of a run-save UI annex**; ITEMS 10.9; ENM 15.8; LE 6.15 tier marks (reading) | Minimap (FL 8.1's schematic graph with knowledge fills, reduced mark set, glyph floor) and full map, layers, auto-marks, Waymarks (tack base, modifier-click) in a run-save UI annex, Listen bubbles with the EP, TL and HZ contents, fact drawings, told pip, tier word on map container hover (4.3) | ⚑ **Decided** (MAIN 6.6, an addition) | Lead's call; told plainly; main design owner applies; LE owner confirms | M1/M2 |
| XI-8 | FL 6.9 request answered; FL 6.10, TL-7, EP-17, HZ pathing (read); FL 11.4 cost table | Travel (incl. Chalked squares in entered spaces, with the step check), noticing pre-stop with re-evaluation and release, Next loose end (nearest by known path length, ties by threshold position, never by id or main-path order; cycling, passability, dismissal), Back to Stairhead (renamed To the Downstair, naming pass NP-25); no auto-explore (4.7) | — | Lead's call; told plainly | M2 |
| XI-9 | FL 6.9 "stopping on any sighting" | Travel stops on new or changed sightings only (ships with the pre-stop) (4.7) | — | Lead's call; FL owner (request) | M2 |
| XI-10 | FL 6.9 "detours are legible before commitment" (extends); LP 4.2 item 3 (read) | Loose ends map-only, no count; Detour quote (4.4.2, 4.4.3) | — | Lead's call; told plainly | M2 |
| XI-11 | BR 4.16 arena-entry confirm (one line; BR 4.2's turned-off confirm gets a one-time toast); FL-20 party-bar tally and count line (**lead's call in FL, approved**; its F1 secret omission kept under FL Q3, read): tally defined; LE 6.15 floor-strip rooms cell and Treasure field (extends the Treasure field state) | Behind-you line on the arena confirm, rooms not entered excluding the arena side; rooms tally "rooms e / t" (Room nodes only), unsplit by role, the named IN6 exception; spent field (renamed emptied field, naming pass NP-25) gated by LE's k ≥ n/2 (4.4.4, 4.4.5) | ⚑ **Agreed** (FL Q3's F1 omission, read) | Lead's call; told plainly; BR, FL, LE owners apply | M2 |
| XI-12 | ENM 12.2, 13.5, 13.6, ENM 13 (Lore and Perception bonuses, flag); ABL 8.3; DH 17.2, 17.3 (DH owner informed); **EP-41 (pending client)** | In-sight and hidden-from pips; bands (M2); watch lines and noticing step tag (M3, conditional) (4.8.3) | — | Lead's call; enemies owner; client via EP-41 | M2/M3 |
| XI-13 | **ENM 12.3, 12.4 step 6 (settled; wording reading)**; FL 6.6; EP-22 | Noise preview; "known groups only" wording (4.8.4) | ⚑ **Settled** (ENM 12.4, read) | Lead's call; enemies owner confirms | M2 |
| XI-14 | RC1, RC9; EV BC2; CL 6.3; CN 16.7; FL 9.1; **TL CC1 (agreed; read)**; **STAT 7.2 (agreed; read)** | Information charter IP1–IP10 and incentive charter IN1–IN6 (4.4.1, 4.5.1) | ⚑ **Agreed** (TL CC1, STAT 7.2, read) | Lead's call | M2 |
| XI-15 | **RS 7.4 RC5/RC5+, RC6 (agreed; extended, not reordered)**; BR BX8; ENM glints gate (read) | Square slot model with the slot mapping and `countsAsMark`, priority stack, modes (one default information layer; Tool mode), budgets (`MarksPerScreen` defined), combat suppression (4.9.2–4.9.5; registry table in 6) | ⚑ **Agreed** (RS 7.4, extended) | Lead's call; RS owner adds rows | M1/M2 |
| XI-16 | HZ 6 (read) | Board digits are room clocks only (4.9.6) | — | Lead's call | M1 |
| XI-17 | **RS 7.4 RC4–RC6 and the M2 gates (agreed; rows added)** | Knowledge-state grammar (the Remembered grade on tiers F and N only); fog bank with the cutaway cap; seen-edge line; line-style registry; gate pairs incl. Seen vs Remembered, Chalked vs Void backdrop, Afterimage vs figure frame (4.2, 6) | ⚑ **Agreed** (RS 7.4, rows added) | Lead's call; RS owner; art | M1/M2 |
| XI-18 | FL 8.1 cutaway, MAIN 6.8; EP 6.8.6 Spoor anchors (EP owner confirms); FL 9.2; LP Rathole (precedent) | Wall-mark projection under cutaway (4.9.7) | — | Lead's call; EP owner confirms | M2 |
| XI-19 | RS 6.9, LE 6.15 (order kept; "Treasure strip" wording), LE 8.2 (wording), DH 17.12, BR 4.16, FL 9.2, LP 4.12 (wording), TL 6.23 prompts (queue slots) | Regions, card queue (completed: TL and DH cards, pre-emption, non-modal first-sight toasts), Stratum Card two-tier with Floor-info overflow and first-run ribbon, naming reconciliation (4.9.1, 4.9.8, 4.9.9) | — | Lead's call; FL, LE, LP owners confirm | M2 |
| XI-20 | MAIN 3 (read); **RS 7.4 gates (agreed; extended)** | Inspect parity, Keep card, points-of-interest key, Declutter, the default binding table (off Alt and Tab) with the OS-chord check, accessibility package (4.10) | ⚑ **Agreed** (RS 7.4, extended) | Lead's call | M1/M2 |
| XI-21 | **MAIN 6.6 (decided): flagged addition of a profile display field** (EV-19 precedent); EV 4.4.2 | `uiMemory`; staged first-run disclosure; density setting (4.10) | ⚑ **Decided** (MAIN 6.6, an addition) | Lead's call; main design owner applies | M2 |
| XI-22 | EV 4.4.1, 4.4.2; LE 6.9 (exposure ruling); TL 6.14; FL-20 (F1); STAT 7.1 | F-TREASURE category and selector; secret caches excluded; role-blind ranking (no off-path key); qualifier from the container's socket class ("plain cache" / "behind risk"); cap behaviour, socket exclusion and mark clearing; the `KnowledgeLeak` exemption; F2–F5 for fact channels (4.6.1) | **Q5** | **Client (Q5, agreed 2026-11-23)** for the exclusion; lead's call otherwise; EV, LE owners apply | M3 |
| **XI-23** | FL-20 / FL 9.2 count line (lead's call in FL, approved; its F1 secret omission kept under FL Q3); **LE-15 / LE 8.2**; **DH 5.5 G6 (agreed, DH Q3)**; RS 6.9 Stratum row; LP 4.12 by-source hover; LP Tip-offs hold (answered); LE 6.15 spoiler timing; CN 16.8 roster (+1 counter); LE 6.2 | The Lodestring; "counts only, never locations" narrowed for paid or earned pointers; value wording (4.6.2) | ⚑ **Agreed** (DH 5.5 G6) | Lead's call (brief question 5, confirmed by the client on 2026-11-23); told plainly; LE, FL, DH, LP, CN owners apply | M3 |
| XI-24 | LE 8.1 Tithe Bowl; LE 6.4; RS 6.9 camp card; RS 6.4 (read) | Tithe Bowl: a Lodestring or Surveyor's Sketch; resolution at entry; option availability; Wax Key guard relaxed (request) (4.6.3) | — | Lead's call; LE owner (request for the guard) | M3 |
| XI-25 | ENM 14.6 category list; HX-6 Pick the Question | F-TREASURE through Yield, once per floor, after F-KEY; kill switch (4.6.4) | — | Lead's call; told plainly; enemies, Herald owners (request) | M3 |
| **XI-26** | **TL-25 basic-vendor row (agreed, TL Q3)**; **ITEMS 10.3 basic-vendor list (agreed through TL-25)**; ITEMS 18.1 vendor stock data; CN vendor table; FL-18 passive condition; STAT 7.1 (kept); LE 6.2; CMP 7.3 (read: no coverage-strip line) | Ear Trumpet lane-gap counter in the basic vendor, only with no Perception-trained party unit on the camp-entry snapshot (stable for the visit, the TL-25 test), its effect suspended while the lane is present, only if `SoloListenClickShare` misses (4.5.2) | ⚑ **Agreed** (TL-25, ITEMS 10.3); **Q3** | **Client (Q3, agreed 2026-11-23)**; CN, FL, ABL, TL, ITEMS owners apply | M3 (conditional) |
| XI-27 | Arcanist list; SH 7.x, CL 6.4; FL 9.1; EV 4.11–4.12; FL `EmptyDeadEndRate`, LP roles; SH False Trail | Owner requests: Walls Have Ears, Cased (conditional on TL-6; its odds band also on EP-41 and the ENM 13 flag), Overlook writes Chalked, beckoning Glimpses, `SideRoomPayoffMix`, False Trail marker shape (4.4.6, 4.5.2) | — | Lead's call; Arcanist, Shade, FL, EV, LP owners (requests) | M2/M3 |
| **XI-28** | **MAIN 6.6 run save at safe points (decided)**: its sentence "the run save is a snapshot of the full core state written at safe points" amended for mid-floor play; "the snapshot is the authority" kept, with the slot as that snapshot; EV-21 (read); LE-14 (precedent) | Suspend-save (one slot, deleted on load and brought up to date after every committed command as base plus journal (cross-document comparison 2026-11-23, CC-121); temp-file-and-rename writes; load order; precedence over the safe point); forward-only knowledge; atomic paid knowledge writes shown only after the write (4.12) | ⚑ **Decided** (MAIN 6.6); **Q1** | **Client (Q1, agreed 2026-11-23)**; lead's call, after the check, for the per-command write (replaces the draft's knowledge journal); main design owner and segments 11–12 apply | M2 |
| XI-29 | LE 8.2, DH 17.12 (hooks); EV-37; RS 12 | Floor Atlas, death-recap map, codex hosting, map paper skin (6, 7) | — | Lead's call; RS owner | M3 |
| XI-30 | Segments 11–12 validator list | V18 `Information` (9.2) | — | Lead's call; segment 12 | M1 tests, M2 full |
| XI-31 | **CMP 7.4 (agreed gate kept; levers untouched)**; FL 6.10, FL-16 | Reports, the segment 10 clock line, the self-cut order (9.3, 9.5) | ⚑ **Agreed** (CMP 7.4, read) | Lead's call | M2 |
| XI-32 | **MAIN 4.3 (decided, kept)**, CN 13.7, 16.7, **TL KF7 (agreed, kept)**, FL 19.1, RS 7.6 | Rejections register (4.5.3, 4.7, 7) | ⚑ **Decided**, **Agreed** (read, all kept) | Lead's call; told plainly | — |

**Told plainly (no question; confirmed by the client on 2026-11-23):** XI-2 (corner rule difference; Downed units see nothing, which changes ENM 12.2's lurker band), XI-3 (chalk narrower than Models A and C; the sight-shadow wording), XI-4 (M2, not M3), XI-7 (the UI annex is an addition to MAIN 6.6), XI-8 (no auto-explore; Chalked travel allowed), XI-10 (no loose-end count), XI-11 (no secrets found/total, no Downstair banner, the rooms tally unsplit), XI-23 (the Lodestring can raise realised value), XI-25 (once per floor through Yield), XI-32 (every rejection).

---
## 14. Open items for the client

None. Approved by the client on 2026-11-23.

The client approved this document as a whole on 2026-11-23, including the six answers to the brief in 0.1 (which the client had accepted by proceeding), with their contested parts; every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the lead's calls after the check in 14.1); the told-plainly items (including "Told plainly after the check" in 14.1, specifically the per-command suspend-slot writes, Q4 covering Chalked squares, and the rooms tally exception, all as written); and the readings recorded in 13; and its approval covers the flagged rows of 13. The five questions raised before writing were answered on 2026-11-23 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. Writing raised **no new client question**, and neither did the check: every new lead's call and every lead's call after the check in 14.1 sits inside the five answers, keeps an approved rule, or is flagged against the text it touches. The Later client questions (units blocking line of sight, Scout Ahead, the full Dream map treatment), the future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them (including the owner-refusal fallbacks and the wave-gated items), and the naming pass remain future items.

### 14.1 This document

**The five answers (agreed, 2026-11-23), each the recommended option:**
- **Q1, save integrity (XI-28):** option A. Quitting mid-floor writes a single suspend slot that is deleted on load; knowledge is forward-only; paid knowledge acts are atomic writes. Rejected: B, safe-point saves with reload scouting.
- **Q2, patrol forecast strength (XI-6):** option A. Earned by watching, shown as a window, 20 ticks by default and one lap with a trained watcher or Cased, withdrawn by events, protected by a `PatrolFightRate` gate. Rejected: B, route and last-seen only; C, an uncapped forecast.
- **Q3, the Ear Trumpet (XI-26; edits the agreed TL-25 vendor row):** option A. Allowed at M3, stocked only for parties with no Perception-trained member, and only if the solo Listen click share misses. Rejected: B, no Listen lane-gap item.
- **Q4, auto-actions and agreed TL-5 (XI-6):** option A. The Mild Plain auto-pick and auto-Probe read only known patrols and stand down when their hearing reach crosses unexplored squares. Rejected: B, keep reading true state.
- **Q5, the Lodestring and secret caches (XI-22):** option A. The Lodestring never points at secret caches. Rejected: B, it may while a Mechanics-trained member lives.

**The six answers to the brief (lead's calls, accepted by the client by proceeding; confirmed by the client on 2026-11-23, with this document), with their contested parts:**
1. Fog gaps: Chalk on entry and Trace on sight; **no Shell on sight and no one-square Pocket rule** (told plainly).
2. Reveal items: narrow, located, single-instance, lane-safe; **no trap or secret reveal items** (told plainly).
3. Incentives: legible, cheap, clean stopping points; **no completion reward and no off-path loot lean** (told plainly).
4. The map: always free, 0 ticks; **no bought map** (told plainly).
5. Treasure hint: the Lodestring over existing treasure; **no separate special-treasure class** (told plainly).
6. Remembered detail: exact where static, "as last seen" with age, projected only from public clocks; activity-keyed Afterimages, **no uniform fade** (told plainly).

**New lead's calls (made while writing; confirmed by the client on 2026-11-23, with this document):**
1. **Document shape:** the proposal's section numbers 0–13 are kept so every cross-reference holds; the client decisions become 0.2 and the decisions in brief 0.3; intent, goals, non-goals and anti-goals are added as 0.4; the client questions become 14 (open items); the closing parts follow as 15 (risks), 16 (left to the builders), 17 (depends on), A.6 (XI cross-reference) and Appendix B (sources), as in BR. The data, validator, reports, arms and clock line are split into 9.1–9.5.
2. **Q values are client keys, not tuning:** the 20-tick and one-lap horizons (Q2), the single suspend slot, delete-on-load and forward-only knowledge (Q1), the lane-gap stock rule (Q3) and the secret-cache exclusion (Q5); changing them returns to the client, except through the three Q2 cut steps of 4.8.1, which Q2 pre-approves (header, 9.1, 14.2; the exception made explicit after the check).
3. **Q4's "unexplored squares" read at square level,** not as the room-level XI-1 term "explored" (4.1.1, 4.8.2; told plainly). *Changed after the check:* never-Seen squares (Unknown or Chalked), not Unknown only.
4. **The Ear Trumpet's trigger made computable:** the M2 report `SoloListenClickShare` (Listen clicks as a share of `ExploreClicks` on F1–2, party 1, no Perception-trained member) misses above 15% *(illustr.)*, read at the M3 schedule review; the proposal table's alternative "or `ExploreClicks`" trigger is dropped to match Q3's wording; *changed after the check:* the stock rule is read on the camp-entry snapshot, stable for the visit, as TL-25 reads Mechanics (4.5.2, 9.3).
5. **The `PatrolFightRate` baseline:** the BASE arm in the same simulator build, F1–5 (*corrected after the check:* floor 1 has the settled Scavenger Circuit), party 4 (parties 1 and 2 reported); read at M2 sign-off and every balance pass; Q2 covers the three cut steps, and a miss after the third returns to the client (4.8.1, 14.2).
6. **Abnormal exits under Q1:** *withdrawn after the check.* The draft's knowledge journal over the last safe point is replaced by the continuous suspend slot (lead's call after the check 1, below); camp and other safe points unchanged (4.12, 9.1, 9.2).
7. **F-TREASURE floors:** the fact channels (Yield, a Curiosity gain) are eligible on F2–F5; the Lodestring item stays F2–F4 (4.6.1, 9.1).
8. **Tithe Bowl option availability:** the Lodestring option is greyed with its reason while the party holds one, and not offered at the fourth camp (before F5), where only the Sketch is offered (4.6.3).
9. **The rooms tally's notation:** "rooms e / t", t = n + k from the count line; *after the check:* e counts Room nodes only, and the tally is the one named IN6 exception (4.4.4).
10. **The Dream map split:** the agreed M3 skin is the per-theme map paper and the pencil-to-ink transition, an option over the plain default under the same gates; the full Dream treatment (veils, wobble, frontier motifs) is the Later client question (6, 10).
11. **Read flags in 13:** XI-1, XI-3, XI-5, XI-7, XI-11, XI-13, XI-14, XI-15, XI-17, XI-20, XI-21, XI-31 and XI-32 flagged for reading or extending decided, agreed or settled text (0.3 now carries the same flags).
12. **Readings consolidated in 13**, including FL 9.2's "treasures: k / n" as the floor strip's Treasure field.
13. **Owner-refusal fallbacks** in 14.2, each stated so a refusal never blocks M2.
14. **Cased if TL-6 is unruled at M3:** Cased is held to Later; the one-lap horizon then comes only from a Perception-trained watcher (14.2).
15. **`WaylaySurpriseRate`** named as a read report for the balance critic's XB16 watch item (9.3).
16. **Intent, goals and anti-goals** added in the template's shape (0.4).

**Lead's calls, after the check (each sits inside Q1–Q5 or an approved rule; confirmed by the client on 2026-11-23, with this document):**
1. **The continuous suspend slot:** the one slot is overwritten after every committed command (temp file and rename), deleted on load and rewritten before input, and always wins over the safe point; the knowledge journal is withdrawn (4.12, 9.1).
2. **`KnownCanSee` and `KnownNoiseReach`** over party knowledge, pessimistic or optimistic per query (4.1.2).
3. **`KnowledgePass`:** runs after every party and monster step and every Portal or piece change, with the step-5 re-run and the combat-start rule (4.1.3).
4. **Entered:** set by completing any step (or standing up) on a non-Portal square of the space (cross-document comparison 2026-11-23, CC-123); doorways and the arena's doorway band never enter (4.1.1, 4.1.4).
5. **`CanSee` over transparent squares** and the blocker-visibility rule; `FogEqualsRules` restated (4.1.2, 9.2).
6. **Chalk draws the wall-bounded footprint,** pieces and Voids as plain chalk; `ChalkOutline` closes stub mouths, the one stated exception to monotonic drawing (4.1.1, 4.1.4).
7. **Q4's "unexplored" = never Seen** (Unknown or Chalked); the act's hearing reach includes its noticing reach; "due" defined, including known patrols with no forecast (4.1.1, 4.8.2).
8. **The forecast made computable:** earned in observable terms; windows only over timed squares; nothing past a "continues" end without a timed lap; heard-only patrols get the Portal window only; `trainedOrCasedHorizon: "lap"` with `lapCapTicks`; the horizon fixed when earned; `PatrolForecast(groupId, square, predicate)` (4.8.1, 9.1).
9. **Loose ends redefined** by far-side entry and ever-Seen squares; Next loose end ties by threshold position, cycles, ranks impassable ends last and can dismiss one (4.4.2, 4.7).
10. **Role-blind orders:** no tie-break or ranking by `roomId`, `portalId`, `socketId` or room role; F-TREASURE drops "off-path before main path" (4.6.1, 4.7, 4.10).
11. **F-TREASURE's qualifier** from the container's socket class only, "plain cache" or "behind risk", with a fixed "says nothing about traps, lurkers, patrols or hazards" line; cap, exclusion and clearing rules; the listed `KnowledgeLeak` exemption (4.6.1).
12. **Travel's step check:** never onto a square not Seen at that moment; re-plan round newly Seen hazards, Voids, traps and pieces; avoid-or-stop for "?" hazards (4.7).
13. **The noticing pre-stop re-evaluated each step,** with a one-step confirm and no re-raise until the group's known state changes (4.7).
14. **Behind-you line:** excludes the arena side; a one-time toast if the confirm is turned off (4.4.4).
15. **The emptied field** respects LE's k ≥ n/2 rule (4.4.5).
16. **The Ear Trumpet's stock** read on the camp-entry snapshot, its effect suspended while the lane is present, no coverage-strip line (4.5.2).
17. **Cased:** counts seen, non-Hidden members, restarts on a new member; its odds band also waits for EP-41 and the ENM 13 flag; "exact activity" withdrawn (4.2, 4.5.2).
18. **Afterimages:** Fresh, Persisting, Old and Short states; removal, not fading, for Searching units; stacked cut-outs, no numerals; dashed cut line, distinct from the EV figure frame (4.2).
19. **Projected Stirs solid,** "hollow" reserved for aged memory; `projectable` from the hazard record alone (4.2).
20. **Remembered grade on tiers F and N only;** the fog bank's cutaway cap; new gate pairs (4.2, 6).
21. **The minimap stays FL 8.1's schematic graph** with three knowledge fills, a reduced mark set and a glyph floor (4.3.1).
22. **One default information layer** (Patrols, F1 included); the hearing outline as a prompt only (4.9.4, 4.10).
23. **The slot mapping, registry table, line-style registry, forecast band look and art budget** (4.9.3, 6).
24. **The card queue completed,** with pre-emption and non-modal first-sight toasts (4.9.8).
25. **Bindings:** the default table, off Alt and Tab; Waymarks by modifier-click on a reserved tack base (4.3.4, 4.10).
26. **Names:** "Aim" mode renamed **Tool mode**; the density "Standard" renamed **Regular**; the Lodestring mark a **spool**; the loose end a thread tuft; "continues" an ellipsis (4.9.4, 4.10, 6).
27. **Audio:** the segment 10 timbre row and the full reserved-cue check (6).
28. **Reports defined:** `PatrolFightRate` with a walker policy, `AfterimageMisreadRate`, `LodestringRegret`, `DetourRegret`, the `MapSecondsPerFloor` plan; the human slice a read (9.3, 9.5).
29. **The cut order's steps 1 and 3 replaced** (9.5).
30. **The rooms tally kept as the named IN6 exception;** no rooms fraction in the Floor Atlas (4.4.1, 4.4.4, 6, 7).

**Told plainly (confirmed by the client on 2026-11-23):**
1. Model A's Shell on sight and Model B's Pocket are rejected (0.1).
2. Chalk is narrower than Models A and C: no pieces, square kinds or hazards, and a Void is plain chalk until seen (4.1.4).
3. The sight shadow is "out of sight (not out of hearing)", never "safe" (4.1.4).
4. The corner rule for sight differs from the movement rule (4.1.2).
5. The FL 8.1 amendment must ship in M2, not M3 (4.1.5).
6. A uniform 20-tick fade fits no activity (4.2).
7. The UI annex for Waymarks is an addition to MAIN 6.6, not a reading (4.3.4).
8. No count on the minimap; no loose-end count anywhere (4.3.1, 4.4.2).
9. No secrets found/total and no Downstair banner on the Behind-you line; the rooms tally is unsplit (4.4.4).
10. No auto-explore; Chalked squares in entered spaces are valid travel targets (4.7).
11. Stopping "on noticing" alone comes too late; hence the pre-stop (4.7).
12. An uncapped forecast cuts patrol contact about 6×; hence the gate (4.8.1).
13. An auto-action can still meet an unseen patrol from known-empty space (4.8.2).
14. Cased counts seen, non-Hidden members only, or it would leak Hidden members (4.5.2).
15. The Lodestring can raise the share a hurried party collects (4.6.2).
16. F-TREASURE through Yield once per floor (4.6.4).
17. Map-reading seconds are real floor time; the minimap must answer "where next" alone (0.1, 8).
18. The noticing pre-stop's cost is unsimulated and must be re-run before M2 sign-off (8.6).
19. Reload scouting is trivial without the suspend-save (4.12; answered by Q1).
20. **(Added while writing.)** The toy was not re-run after the client's answers or the new lead's calls (8.6).
21. **(Added while writing.)** A Lodestring unspent after F4 has no further use (4.6.2).
22. **(Added while writing; withdrawn after the check.)** The draft's "a deliberate process kill can still rewind world state" no longer holds: the continuous slot (4.12) closes it.
23. **(Added while writing.)** Several genre claims in 1 and Ford's corner conventions are unverified; none is load-bearing (1, 4.1.2).

**Told plainly after the check** (none overrides a client answer; confirmed by the client on 2026-11-23, with the per-command suspend-slot writes (1), Q4 covering Chalked squares (4) and the rooms tally exception (7) explicitly confirmed as written):
1. The draft's crash path rewound deaths, fights and spent counters; the suspend slot is now rewritten after every command, which goes beyond Q1's "quitting writes" but keeps its one slot, its delete-on-load and its forward-only knowledge (4.12).
2. Downed units seeing nothing **changes** ENM 12.2's lurker-band candidates; the draft wrongly called it settled text (2.1, 4.1.2, XI-2).
3. Floor 1 has a patrol Circuit (the settled Scavengers); the patrol layer and `PatrolFightRate` now include F1 (4.8.1, 4.9.4).
4. The draft's Unknown-only reading of Q4 narrowed the client's answer; Chalked squares were never seen, so they now trip the gate too, at a cost not yet re-measured (4.8.2, 8.6).
5. The F-TREASURE draft ranked off-path rooms first, leaking route role, and called rooms "in the open" from their trap and guard contents, a trap-absence reveal; both are gone (4.6.1).
6. R fails the combat clutter gate in the toy (8.9% against 5%); suppression is unmeasured, and the combat gate is a pre-M2 sign-off check (3.3, 8.4).
7. The rooms tally "rooms e / t" is a room-coverage figure, kept only because FL-20 and LE 6.15 fixed a rooms tally; it is the one named exception to IN6 (4.4.1, 4.4.4).
8. Chalk can show wall across a short stub's mouth until it is seen; only sight is truth (4.1.4).
9. A heard-only patrol gets only Listen's "passing this door" window; full forecasts need watching by sight (4.8.1).
10. Two of the draft's five self-cut steps were already the defaults and saved nothing; they are replaced, and the toy must be re-run on the new order (9.5, 8.6).
11. Cased's odds band would have shipped Perception information round EP-41; it now waits for that ruling and the ENM 13 flag (4.5.2).
12. Several draft names collided ("Aim", "Standard", the knot mark, the Waymark shapes, three chevrons) and are changed (4.3.4, 4.9.4, 4.10, 6).

**Later client questions (not asked now):** should units block line of sight (4.1.2); Scout Ahead and Range Ahead (would change decided MAIN 4.3 "companions follow"; 4.5.3); the full Dream map treatment over the M3 skin (6).

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a lead's call, a new lead's call or a lead's call after the check (confirmed by the client on 2026-11-23, with this document) unless marked.

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| Patrol fights fall | `PatrolFightRate` < 60% of BASE | Q2's cut steps in order: 20 ticks for everyone; hover-only; route and memory only. A miss after step 3 returns to the client | Pre-approved by Q2; client after step 3 |
| Ear Trumpet not needed | `SoloListenClickShare` ≤ 15% at the M3 review | The Ear Trumpet does not ship (agreed, Q3); a later miss re-opens it at the next schedule review | Pre-approved by Q3 |
| Segment 10 clock miss | `Seg10MinutesDelta` or `Seg10ClickDelta` > 0 on F4–5 at party 4 | The 9.5 self-cut order, steps 1–5 | Lead |
| Map time high | `MapSecondsPerFloor` above its plan (6 s per floor, *illustr.*) | The cut order's step 5 (the minimap absorbs the most-opened layer); never a map price | Lead |
| Pre-stop costs clicks | `NoticingPreStops` pushes the walker line over | The 9.5 cut order; the pre-stop itself is never cut while XI-9 ships | Lead |
| Yield pointer dominates | `YieldCategoryMix` F-TREASURE > 50% | F-TREASURE through Yield restricted to the optional elite | Lead |
| Lodestring is a tax | `LodestringBuyRate` > 90% among parties with a Rare-or-better unfound, or high `LodestringRegret` | Price first (pegged to healing); then the cut order's step 2 | Lead |
| Secrets found change | `SecretFoundRate{lodestring}` (SV-24) moves with Lodestring use | Review the selector; secret caches stay excluded (agreed, Q5) | Lead |
| Afterimages misread | `AfterimageMisreadRate` high, in combat or explore | Art first; then shorter persistence for Stationary units | Lead |
| Clutter | `MarksPerScreen` gate or the glance test misses | Density defaults, combat suppression, information layer defaults | Lead |
| Clicks still high | `ExploreClicks` misses with travel on | An auto-explore accessibility option goes to the client (Later) | Client |
| Players lost | `LostTime` misses | A layout item (Later) goes to the client | Client |
| Side rooms unvisited | `SideRoomVisitRate` below band on human sessions | `SideRoomPayoffMix` and beckoning Glimpses first; never a completion reward | Lead; FL, LP, EV owners |
| **Enemies owner refuses** the ENM 12.1 route reading | — | Returns to the client (Q2 was agreed); drawing routes through unknown space would leak Circuit rooms | Enemies owner; client |
| **TL owner refuses** the TL-5 / auto-Probe reading | — | Returns to the client (Q4 was agreed) | TL owner; client |
| **FL owner refuses** re-sight narrowing (XI-9) | — | M2 ships with the replaced cut steps 1, 3 and 4 applied (9.5), re-run in the toy first; the pre-stop still ships | FL owner |
| **FL owner refuses** the FL 8.1 amendment | — | Traced contours and F-ROOM's Chalked interior appear on the full map only, never the minimap | FL owner |
| **FL owner refuses** Overlook writing Chalked | — | Overlook Traces only | FL owner |
| **EP owner declines** owning `PatrolFightRate` | — | Segment 10 owns the report; the gate is unchanged | EP owner |
| **EP-41 refused** by the client | — | Watch lines ship; no noticing step tag with odds; Cased ships without its odds band | Client |
| **LE owner refuses** the Wax Key guard relaxation | — | The guard stays: both Tithe Bowl options are unavailable while the party holds a Wax Key | LE owner |
| **LE owner rules** the pointer is exposure (LE 6.9) | — | Exposure counted as LE rules; the price is re-pegged | LE owner |
| **Enemies or Herald owner refuses** F-TREASURE through Yield | — | The Lodestring's sources are the Tithe Bowl and a Curiosity gain only | Enemies, Herald owners |
| **EV owner refuses** the F-TREASURE category | — | The Lodestring resolves through its own selector outside the FactLedger, with the same rules, exclusions and cap | EV owner |
| **EV owner refuses** beckoning Glimpses | — | No Glimpse rule; `BeckonFollowRate` dropped | EV owner |
| **Shade owner refuses** Cased, or TL-6 is unruled at M3 | — | Cased held to Later; the one-lap horizon comes only from a Perception-trained watcher | Shade, TL owners |
| **Shade owner refuses** the False Trail shape | — | The Afterimage's base becomes a square paper tag with a punched hole (a feature it does not already have; the draft's torn-edge base was already its look), so the two never match | Shade owner; art |
| **Enemies owner refuses** the ENM 12.2 lurker-band change (Downed units excluded) | — | Returns to the client: Downed units contributing no knowledge stood as written with the client's answers | Enemies owner; client |
| Combat clutter | `MarksPerScreen` combat P(> 12) > 5% at the pre-M2 sign-off check | The 8.4 levers in order, then the clutter row above | Lead |
| **Arcanist owner refuses** Walls Have Ears | — | No batched Listen | Arcanist owner |
| **Main design owner refuses** the suspend slot or forward-only knowledge | — | Returns to the client (Q1 was agreed) | Main design owner; client |
| **Main design owner or segments 11–12 refuse** a write after every command (cost) | — | The slot is written at least at every `KnowledgePass` end, combat round end, death, Down and paid act, which keeps every rewindable outcome covered. **Segments 11–12 did not refuse:** a journal record after every command (seeds and validation design 4.8.4; agreed, Q4 there) | Main design owner; segments 11–12 |
| **Main design owner refuses** the run-save UI annex | — | Waymarks are display state only and are lost on suspend | Main design owner |
| **Main design owner refuses** `uiMemory` | — | Staged disclosure by floor runs on every run; first-sight callouts are always shown | Main design owner |
| **DH owner refuses** the in-sight pip beside the danger band | — | The pip becomes hover-only on the portrait | DH owner |
| **BR owner refuses** the Behind-you line | — | The line moves to the Floor-info panel | BR owner |
| **RS owner refuses** the registry rows or RC6 tier extension | — | Marks without rows do not ship; the lead re-scopes the surface set | RS owner; lead |
| **RS owner refuses** the travel-stop sting | — | Visuals only | RS owner |
| Later questions | A later design or playtest asks | Units blocking line of sight; Scout Ahead; the full Dream map treatment | Client |
| Naming pass | — | Every placeholder name here | Client |

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | **Afterimages and forecasts read as truth.** Stale memory will still kill someone (told plainly) | Role-glyph cut-outs, ages in hover, "if undisturbed" windows, event withdrawal; `AfterimageMisreadRate` in combat playtests |
| 2 | **Patrols solved.** Forecasts plus watch lines plus Cased could turn Circuits into timing puzzles | The `PatrolFightRate` gate (agreed, Q2) and its cut steps guard "combat is key" |
| 3 | **The pre-stop costs clicks.** Unsimulated | Re-run before M2 sign-off; the 9.5 cut order |
| 4 | **Lodestring as a tax** | Price pegged to healing; earned routes; `LodestringRegret`; a buy rate above 90% among parties with a Rare-or-better unfound as the alarm |
| 5 | **Surface overload.** About 11 new registry rows on top of segments 5–9 | Staged disclosure and the glance test (first-time testers name each visible line family ≥ 80%); caps stay illustrative until real screenshots run |
| 6 | **Traced contours read as dead ends** | Gapped contours and "exits unknown"; playtest the misread |
| 7 | **Reload scouting or rewinding** through abnormal exits | The suspend slot (agreed, Q1), brought up to date (base plus journal) after every command (cross-document comparison 2026-11-23, CC-121); `KnowledgeReloadMonotone`, `DeathNotRewindable` |
| 8 | **Owner refusals.** If ENM 12.1, TL-5, FL 6.9 or the FL 8.1 amendment are refused, leaks or extra stops return | The 14.2 fallbacks; the cut order and `KnowledgeLeak` flag them |
| 9 | **Unverified research.** Ford's corner conventions; several genre claims († in 1) | Property tests before building; none is load-bearing |
| 10 | **Art budget.** Knowledge states are on screen every second | The theme-neutral style sheet passes the gates before the first Tone Sheet; per-theme fog tints are Tone Sheet rows (6); the art and tech budget table (6) |
| 11 | **Small parties** pay most of any lane-gap cost | `InfoCoinByPartySize`, party-1 and party-2 lines; the Ear Trumpet trigger (agreed, Q3) |
| 12 | **Toy crudeness** | The simulator replaces every figure here |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the minimap width, fade and persistence ticks, the lap length, combat Afterimage rounds, Waymark counts, rim insets, dim levels, prices, the `PatrolFightRate` share, the `SoloListenClickShare` threshold, the F-TREASURE cap, report targets other than the stated gates.
- Godot scene structure for the board overlays, minimap, full map, cards and Inspect, with no physics; the shadowcasting implementation inside the property tests.
- Art and audio timings within 6.
- Names, until the naming pass.
- **Not tuning:** symmetric `CanSee` and its blockers; the `KnownCanSee` and `KnownNoiseReach` modes; Downed units see nothing; chalk never draws contents or stubs, and never draws a Void as a Void; travel never steps onto a square not Seen; ties and rankings never by id or role; the slot (base plus journal) brought up to date after every command (cross-document comparison 2026-11-23, CC-121); nothing reveals unit positions outside approved rules; no completion meter; no count on the minimap or of loose ends; no auto-explore; ties never by main-path order; **the 20-tick and one-lap horizons, the earned window and event withdrawal (agreed, Q2)**; **known-patrol reading and the never-Seen-crossing gate (agreed, Q4)**; **the lane-gap stock rule (agreed, Q3)**; **no pointer at secret caches (agreed, Q5)**; **one suspend slot, deleted on load, forward-only knowledge (agreed, Q1)**.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **The FL owner:** XI-2 (corner reading), XI-4 (FL 8.1), XI-9, XI-11 (rooms tally), XI-19 (naming), XI-27 (Overlook, `SideRoomPayoffMix`).
- **The enemies owner:** XI-1 readings, XI-2 (ENM 12.2 lurker band), XI-6 (ENM 12.1), XI-12 (ENM 13), XI-13, XI-25 (ENM 14.6), XI-27 (Cased's odds band and ENM 13).
- **The EP owner:** XI-6 (`PatrolFightRate`), XI-12 (EP-41 via the client), XI-18 (Spoor).
- **The TL owner:** XI-6 (TL-5, TL 6.4), XI-26 (TL-25), TL-6 for Cased.
- **The LE owner:** XI-7 (tier marks on map hover), XI-11, XI-19, XI-23, XI-24 (Tithe Bowl, Wax Key guard, exposure).
- **The LP owner:** XI-19, XI-23, XI-27.
- **The EV owner:** XI-4, XI-22, XI-27 (beckoning Glimpses), EV-37 codex.
- **The BR owner:** XI-11.
- **The RS owner:** XI-5, XI-15, XI-17, XI-20 (registry rows and gates), XI-29 (codex, Atlas page), the travel-stop sting.
- **The HZ owner:** XI-3 reading (informed).
- **The DH owner:** XI-1 (17.13, Later), XI-12, XI-23 (G6), XI-29.
- **The CN owner:** XI-23, XI-26.
- **The ITEMS owner:** XI-26 (ITEMS 10.3, 18.1).
- **The ABL owner:** XI-26 (FL-18 passive condition).
- **The Arcanist, Shade and Herald owners:** XI-25, XI-27.
- **The CMP owner:** XI-26 (informed: no coverage-strip line).
- **The room terrain owner (segment 3):** XI-2 (TR-5 reading).
- **The main design owner:** XI-2 (MAIN 4.3 reading), XI-7 (UI annex), XI-21 (`uiMemory`), XI-28 (the continuous suspend slot).
- **Segments 11 and 12 (seeds, validation, saves):** 4.13; V18 (XI-30); the suspend slot's write, load and precedence mechanics. **Designed:** the seeds and validation design (approved 2026-11-23; its 4.8, 5.2, 6, 11); pending there: walker equivalences (4.13).
- **Camp and hub (segment 13):** 4.13. **Designed:** the camp and hub design (approved 2026-11-23; its 11); its CH-22 is applied in 4.10.
- **Starting kits (segment 14):** 4.10, 4.13. **Designed:** the starting kits design (approved 2026-11-23; its 11, 17); its SK-9 is applied in 4.10; codex hosting of the Oddments page.
- **Charm abilities (segment 16, the TK 24.2 pass):** 4.9.5, 4.13, 5, 6. **Designed:** the charm abilities design (approved 2026-11-24; its 11, 17); pending with this owner: CA-27 (`SurfaceRecord` rows, plate-mark budget read) and the forecast's `KnowledgeLeak` cases.
- **The simulator:** every report of 9.3 and every arm of 9.4.
- **Art, audio and writing:** 4.2, 5, 6.
- **Naming pass:** every placeholder name here.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Fun and play critic (XP)

| Id | Finding | Resolution |
|---|---|---|
| XP1 | Travel and re-sight narrowing walk into noticing rolls | Noticing pre-stop; XI-9 ships only with it (4.7) |
| XP2 | Combat Afterimage suppression hides in-room combatants | Reversed: in-room Short kept, out-of-room dropped (4.2) |
| XP3 | Uncapped forecast removes patrol tension | Earned, windowed, gated by `PatrolFightRate` (4.8.1; agreed, Q2) |
| XP4 | Unfinished Business invents a secrets found/total; post-boss FOMO | Arena confirm only; approved fields only; no Stairhead banner (4.4.4; Stairhead renamed the Downstair, naming pass NP-25) |
| XP5 | Loose-end count is a completion meter | No count anywhere; report only (4.4.2) |
| XP6 | Pointer set up to trip its own tax alarm | Price pegged to healing; earned routes; decision-quality alarm (4.6.2, 15) |
| XP7 | "Behind risk" too vague | Detour quote lists known route obstacles (4.6.1) |
| XP8 | Too many vocabularies on a first run | Staged disclosure by floor; Traced map-only; glance gate (4.10) |
| XP9 | Traced contour reads as a dead end | Gapped contour, "exits unknown" (4.1.5) |
| XP10 | Map-seconds budget punishes map use | Report and cut trigger only; minimap duty; reordered cut list (9.5) |
| XP11 | Persistent Stationary Afterimages lie at length | Hollow "old" style after one lap (4.2) |
| XP12 | Travel refusing chalk makes odd paths | Chalked squares in entered spaces allowed (4.7) |
| XP13 | Sight shadow hidden while aiming | Dims to 50% instead (4.9.4) |
| XP14 | Combat info hover-only hurts newcomers | Guided density keeps them on (4.9.4, 4.10) |
| XP15 | Stratum Card dismissed before reading | First-run "More in Floor info" ribbon (4.9.8) |
| XP16 | Id and name chaos | One register XI-1..32; one name per surface (A.5, A.6) |
| XP17 | No moment of delight | Beckoning Glimpses recommended M2; "the string ends here" frame; Atlas (4.4.6, 6) |
| XP18 | Veterans lose speed | Compact density, persisted layers, Next loose end default (4.10) |

### A.2 Balance critic (XB)

| Id | Finding | Resolution |
|---|---|---|
| XB1 (H1) | Reload scouting | XI-28 suspend-save (agreed, Q1); after the check, the slot is rewritten after every command (the draft's knowledge journal withdrawn) |
| XB2 (H2) | Pointer plus stub = secret finder | Secret caches excluded (agreed, Q5) |
| XB3 (H3) | Cased leaks Hidden members | Trigger counts members the party has seen and that are not Hidden; `KnowledgeLeak` case |
| XB4 (H4) | Chalk draws stubs | Stubs by sight only; `ChalkNeverStub` for every non-sight source |
| XB5 (M1) | Uncapped forecast neutralises patrols | 20-tick default, one lap as skill payoff, fight-rate gate (agreed, Q2) |
| XB6 (M2) | Pointer before Two Doors selects doors | Resolve at entry or use; `PointerNeverPreDoor` |
| XB7 (M3) | Free Yield F-TREASURE kills the paid pointer | Once per floor through Yield; kill switch |
| XB8 (M4) | Pointer does raise realised value | Wording corrected; LE confirms band |
| XB9 (M5) | Clock line passes only on a behaviour assumption | XI-9 precondition with fallback defaults; pointer minutes inside; minimap duty |
| XB10 (M6) | Sight-shadow overclaims safety | "Out of sight (not out of hearing)" |
| XB11 (M7) | Three definitions of unmapped | One definition (4.1.1) |
| XB12 (M8) | Small parties pay the lane-gap tax; Downed split | `InfoCoinByPartySize`, party-1 checks; Downed do not see (an ENM 12.2 change, XI-2) |
| XB13 (M9) | Easy Plain (Easy renamed Mild, naming pass NP-25) walks blind into unseen patrols | Never-Seen-crossing gate on auto-actions (agreed, Q4) |
| XB14 (L1) | Batched Listen wastes trained Listen | Trained detail when a trained member is present |
| XB15 (L2) | Shell vs square kinds and Voids | Chalk draws no square kind; a Void's square is plain chalk, known as a Void only by sight (after the check) |
| XB16 (L3) | Wand of Outlining on chalk nooks | Not new (52% either way); after the check, chalk draws the whole footprint, so nooks no longer show as holes; `WaylaySurpriseRate` read (9.3) |
| XB17 (L4) | "Not spent on empty" as a probe | Identical wording for none-eligible and all-excluded; accepted (~0.8%) |
| XB18 (L5) | Stationary Afterimages near-exact | Honest and intended; report only |

### A.3 Consistency critic (XS)

| Id | Finding | Resolution |
|---|---|---|
| XS1 | XI-n collisions across drafts | One register XI-1..32 (13); draft mapping in A.6 |
| XS2 | Movement corner rule exists (FL 3.1, TR-5) | Cited; deliberate difference stated; `LosVsLineAgree` |
| XS3 | Main-path identity leaks (tie-break, treasure split, tally, return to path) | Nearest by known path length, ties by threshold position, never by id or main-path order; F-TREASURE ranking role-blind; split rejected; rooms tally unsplit; no return to path |
| XS4 | Pointer or Sketch can chart a secret room | `SketchNeverSecretNode`; secret caches excluded (agreed, Q5) |
| XS5 | Downed vision unresolved | Non-Downed only; reading of MAIN 4.3 |
| XS6 | Behaviour changes labelled "readings" | ENM 12.1, TL-5, TL 6.4, EV 4.12, MAIN 6.6 annex, FL 6.9 listed as changes with sections; ⚑ where settled, decided or agreed |
| XS7 | Counts-only narrowing missing homes | RS 6.9, LP 4.12, Tip-offs added to XI-23 |
| XS8 | Name collisions | A.5 |
| XS9 | Hooks not placed (Readings, Trap Eye, Lore inscription, Hold the Hide, coverage strip, Seams, Lucid Key (renamed the Clear Key, naming pass NP-25)) | Rows in 5; Cased conditional on Trap Eye |
| XS10 | Floor strip location | Party bar (4.9.1) |
| XS11 | Stratum strip vs folded card | Strip in M2; fold an M3 option |
| XS12 | Shell content inconsistent | Walls, the wall-bounded footprint, Portal kinds only |
| XS13 | DH 17.13 reading contradictory | Chalk alone does not close undo; Later |
| XS14 | Combat Afterimage suppression | As XP2 |
| XS15 | F-KEY pip vs no Patrolling plate | On the route pip |
| XS16 | F-LISTEN drawing | Bubbles only |
| XS17 | Ear Trumpet edits an agreed row | ⚑; agreed, Q3 |
| XS18 | F-TREASURE in Yield needs flags | XI-25 with sections and kill switch |
| XS19 | Noticing odds vs EP-41 and ENM 13 | Gated on EP-41; ENM 13 flagged; after the check, Cased's odds band too |
| XS20 | Scout Ahead must not survive in M3 | Later only |
| XS21 | MapSeconds cannot be a lever | Report and cut trigger |
| XS22 | Idle motion | Fog bank and Afterimages static; drift Later above wall-top only |
| XS23 | Ink-in must not delay input | Stated in 6 |
| XS24 | Waves ambiguous | One wave per row; FL 8.1 amendment M2; Lodestring M3 |
| XS25 | Cut items must be registered | XI-32 with failure reasons |

### A.4 Judges' grafts not taken, or changed
- Model A's Shell on sight (judge-scored as part of A) → replaced by C's Chalk on entry.
- Model B's one-square Pocket → rejected (XN3).
- Model B's off-path loot-rank lean → rejected.
- Model C's two drawing systems → one plain default; the Dream map an M3 skin, its full treatment Later (14.1 call 10).
- Model C's "return to main path" → rejected (leaks role).
- Model A's and the UI catalogue's counts on the minimap frame and loose-end count → rejected (IN6).

### A.5 Names checked (case-insensitive, whole word, 28 approved documents)

**Amended by SV-31 (M2):** "28 approved documents" is the count at writing; from M2 the name check is `V7.NameRegistry`, manifest-driven, which every segment feeds, so no document count is kept here (seeds and validation design 5.2).

**0 hits:** the `XI-` prefix; `V18`; Lodestring, F-TREASURE, Afterimage(s), Chalked, Traced, Waymark(s), Walls Have Ears, Cased, Ear Trumpet, Surveyor's Sketch, Behind-you, Detour quote, Keep card, Back to Stairhead (renamed To the Downstair, naming pass NP-25), loose end(s), Next loose end, Listen bubble, spent field (renamed emptied field, naming pass NP-25), Floor Atlas, `PartyKnowledge`, `CanSee`, `PatrolForecast`, `IKnowledgeQuery`, `uiMemory`, seen-edge, fog bank, Declutter, status ribbon, Stratum strip, Treasure field, told pip, sight shadow, noticing pre-stop, watch lines, Guided, Compact, frayed (now unused after the check); the events `SquaresSeen`, `SpaceEntered`, `SpaceTraced`, `SpaceChalked`, `AfterimageSet`, `HeardRecorded`, `FactDrawn`, `InstrumentUsed`; the queries `DetourQuote`, `NoticingPreStop`, `TreasureTarget`, `SketchGraph`, `LooseEnds`, `NoisePreview`, `InSightOf`; the checks and reports `LosSymmetric`, `FogEqualsRules`, `KnowledgeLeak`, `ProfileBlind`, `NoHoverOnly`, `ChalkNeverStub`, `SketchNeverSecretNode`, `BubbleNeverPositioned`, `LodestringDoorBlind`, `PointerNeverPreDoor`, `WaymarksOutsideKey`, `KnowledgeReloadMonotone`, `PatrolFightRate`, `MapSecondsPerFloor`, `Seg10ClickDelta`, `MarksPerScreen`, `CardFit`, `LostTime`, `YieldCategoryMix`, `SecretFindRate`, `SideRoomVisitRate`, `BeckonFollowRate`, `SideRoomPayoffMix`; "the string ends here"; "Dream map". Local prefixes XQ, XN, IP, IN, XP, XB, XS: 0 hits.

**Added while writing (0 hits, grep re-run):** `SoloListenClickShare`, `WaylaySurpriseRate`, the suspend slot (`suspendSlots`). (`knowledgeJournal` withdrawn after the check.)

**Added after the check (0 hits, grep re-run):** `KnownCanSee`, `KnownNoiseReach`, `KnowledgePass`, `ChalkOutline`, `DeathNotRewindable`, `TravelNeverEntersKnownHazard`, `WaymarkGlyphDistinct`, `SpentFieldRespectsBestRemaining`, `RoleBlindOrder`, `BehindYouFullClearZero`, Tool mode, Regular (as a density label), "plain cache", spool, tack, tuft, ellipsis, "outline ends".

- **Kept on purpose:** "Tithe Bowl" (11 hits, LE's own camp hook, the thing this design fills); "count line" and "floor strip" (approved FL and LE terms, reconciled in 4.9.9); "Floor-info panel" (3 hits, LE and ENM's own term); `TallyChaseSeconds`, `ExploreClicks`, `ExploreSeconds`, `PreviewExactness` (approved reports and checks, read); Pick the Question and the Wand of Outlining (approved, read); "Overlook" (FL's own M3 mechanic).
- **Descriptive only, not names:** "chalk" (3 hits: EV's Gaol dressing and Tally Board text; the state is **Chalked**, 0 hits); "Trace" (2 hits, the SRD spell name Pass without Trace; the state is **Traced**, 0 hits); "inspect" (9 hits as a lowercase verb; **Inspect** here is the UI action's name); "Standard" (*after the check:* withdrawn as the density label, because it is ENM's rank and TL's device grade; renamed **Regular**, 0 hits); "Aim" (*after the check:* the weapons rule's Aim attack; the mode is renamed **Tool mode**, 0 hits); "Shell" (capitalised 0; lowercase 14: Frozen's shell, Alms Crab (renamed Pew Crab, naming pass NP-25); used only descriptively); "Pocket" (1 lowercase hit in RT; the rejected rule is named only descriptively); "in sight of" and "hidden from" (common phrases; the pips are named by their captions).
- **Rejected after collision:** "Aim" and "Standard" as UI labels (after the check); the knot motif for the Lodestring mark (the Windfall knotted-sack, after the check); pennant for the Waymark base (Herald, after the check); star for a Waymark shape (the notable's star, after the check); Targeting (65 hits); Sleepwalker (enemy, IR skin, BR reserves); Thread (Arcanist, EV avoided list, BR lanes); Twine, Clew, Lodestone (retired with the drafts); F-HOARD (0 hits but LE's "hoard" vocabulary); Echo (41, EV avoided); Stake (EV avoided); Rumour (EP rejected meter); Last Turning (FL term); ghost (IR draws, SH False Trail); lens (CL term); Pull (11); Shape (42); Seek (11); pin (13); Field Notes; Marginalia; "quiet states" (LP quiet summaries; renamed **spent field**; renamed again **emptied field** by naming pass NP-25). Rejected local schemes: XT (20), XF (35), XK (19), IT (32), IF (21), ET (EV-rejected).

### A.6 XI cross-reference

The research drafts, the three models, the UI catalogue and the incentives layer each numbered from XI-1 with different contents. All are renumbered into this register by topic:
- knowledge model → XI-1; `CanSee` → XI-2; Chalk on entry → XI-3; Trace on sight → XI-4; remembered state → XI-5;
- patrols and auto-actions → XI-6; the map → XI-7; travel → XI-8; re-sight narrowing → XI-9; loose ends and the Detour quote → XI-10; the Behind-you line, rooms tally and emptied field → XI-11;
- party-side stealth pips → XI-12; hearing → XI-13; charters → XI-14;
- slot model → XI-15; digits → XI-16; knowledge grammar → XI-17; wall projection → XI-18; regions, queue and naming → XI-19; Inspect parity → XI-20; disclosure → XI-21;
- F-TREASURE → XI-22; the Lodestring → XI-23; the Tithe Bowl → XI-24; Yield → XI-25; the Ear Trumpet → XI-26; owner requests → XI-27;
- suspend-save → XI-28; the Atlas and codex → XI-29; V18 → XI-30; reports and the clock line → XI-31; rejections → XI-32.

**Draft id mapping (selected):** lead draft XI-8 / Model A XI-9 / N4 stop narrowing → **XI-9**; lead XI-13 Sleepwalker's Thread, A XI-14 Lodestring, B XI-15 Gilded Clew, C XI-13 Lodestone, incentives XI-36 Dowsing Twine → **XI-23**; incentives XI-35 F-TREASURE, A/C F-HOARD → **XI-22**; catalogue XI-24 slot model → **XI-15**; catalogue XI-25 wall projection → **XI-18**; catalogue XI-26 digits → **XI-16**; catalogue XI-27 fog bank → **XI-17**; catalogue XI-28 naming → **XI-19**; catalogue XI-29 Inspect parity → **XI-20**; catalogue XI-30 card queue and incentives XI-30 charter → **XI-19** and **XI-14**; all XI-33 and above retired.

The drafts' options the client rejected (Q1-B, Q2-B and C, Q3-B, Q4-B, Q5-B) are not register rows.

## Appendix B: sources

- Game details in section 1 are from memory and general design knowledge (Brogue, DCSS, NetHack, Into the Breach, XCOM 2, Invisible Inc., Darkest Dungeon, Hades, Dead Cells, Hollow Knight, Slay the Spire, Spelunky, The Binding of Isaac, Enter the Gungeon, Risk of Rain 2, Baldur's Gate 3, Pathfinder, Shadow Tactics, Cogmind, the Diablo series, Elden Ring, Outer Wilds, D&D 5E SRD, OSR writing, The Alexandrian, Sly Flourish, Albert Ford's symmetric shadowcasting); claims marked † were not web-verified; none is cited as a rule.
- The segment 10 proposal (the synthesis), its three models, the UI surface catalogue, the incentives layer, the new-mechanics notes N1–N13, the judges' and critics' reports, and its toy scripts (`seg10sim.py`, `ablate.py`, `analysis2.py`, `analysis3.py`, `analysis4.py`), illustrative only, kept in the design lead's scratch area, not in the repository.
- The 28 approved documents listed in the header.
