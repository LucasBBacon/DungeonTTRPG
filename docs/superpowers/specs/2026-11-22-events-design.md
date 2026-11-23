# Events and Curiosities: Design (Dungeon Generation, Segment 8)

Date: 2026-11-22
Status: Approved, 2026-11-22
Parents (immediate): `2026-11-22-loot-sources-design.md` (loot sources design, "LP" in change ids, "7c" in prose, approved 2026-11-22, **the most recent template**), `2026-11-22-loot-economy-design.md` (loot economy design, "LE", segment 7a, approved 2026-11-22) and `2026-11-22-item-rolls-design.md` (item rolls design, "IR", segment 7b, approved 2026-11-22).
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
`2026-11-20-companions-design.md` (companions design, "CMP"),
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change ids are RT-n),
`2026-11-21-floor-layout-design.md` ("FL", segment 2),
`2026-11-21-room-terrain-design.md` ("ROOM", segment 3; its change ids are TR-n),
`2026-11-21-hazards-design.md` ("HZ", segment 4),
`2026-11-21-traps-locks-design.md` ("TL", segment 5) and
`2026-11-21-encounter-placement-design.md` ("EP", segment 6).

Items marked **(agreed)** are the client's answers of 2026-11-22 to the four questions this design raised (Q1–Q4; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document. The client also said that everything else in the lead's proposal stands as written: the rules noun Curiosity (card, Interlude, Glimpse; MECH and rare tags); Model A as the chassis with the lead catalogue's content and grafts; the placement table per floor; the card shape with printed chips, a free Leave and a lit sidegrade option; checks as no-roll thresholds; the gains (the FactLedger, Graces, route reveals, priced routes to unstepped ordinary locks, Strands, the codex in M3); the costs; the Never list; the loot touches; the within-run caps; the Twice-Told card; Retellings; the profile memory `curiosityMemory`; Launcher Fallow; determinism (the `curiosities` stream, EventDraw, EventPlace); Strands; the clock lines and the self-cut order; the register by floor; the tone guards and premise tags; the Afterword, Sequels and the Unwoken Roll; the M3 meta; the Later items; the per-theme catalogue for all themes with its content budget; writing, art and audio; the new mechanics adopted, held and rejected; the numbers and the losing-streak table (toy); data, V16 `Curiosities`, reports and arms; staging; ownership and hooks; the binding requirements; EV-1 to EV-45; the told-plainly items; the risks; and the appendix resolutions and names. The client approved this document as a whole on 2026-11-22.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-22, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-22. Items marked **(lead's call, after the check)** were made when the written document was checked by the builder, consistency, play and art-and-writing lenses; they are listed in 14.1 with the check's "told plainly" items, the client confirmed them all on 2026-11-22, and none changes a client answer. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change EV-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-22 (with Q1–Q4 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge, a critic or a draft, or state an unwelcome forecast, and were told to the client in plain words (14.1, including those told after the check); the client confirmed them all on 2026-11-22. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q4 of this document (2026-11-22), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-22 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(lead's call, after the check)** | A lead's call made when the written document was checked; never overrides a client answer; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(change EV-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a judge, a critic or a draft, or states an unwelcome forecast; told to the client in plain words (14.1) and confirmed by the client on 2026-11-22 |
| **(hook)** | Owned by another segment or design; segment 8 states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator (8, 9). Never binding |

**The client's brief.** Dungeon generation segment 8: **events**. These are the non-combat rooms and encounters on a floor: shrines, altars, curios, captives, wanderers and dream figures, choices and bargains, lore and codex moments, vignettes, puzzles and social beats. The brief covers their outcomes inside the fixed loot deck, placement, pacing against the floor clock, checks, floor-1 rules, determinism, data, validator V16, reports, and hooks to segments 9, 10, 11–12 and to camp and hub.

The client also raised a concern directly: *events can be interesting and memorable if done properly, but they can be quite repetitive for a player going through many runs on a losing streak; watch out for that.* Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; combat is key; contest the client or approved views where warranted.

The client's four answers (Q1–Q4, **agreed**, 2026-11-22) are in 0.2: **M2 cross-run memory is seed-pure, with display-only Retellings outside the reproducibility key, the Twice-Told card and Launcher Fallow (K = 3) on the F1 Curiosity inside R-SEED-6's budget; M3 adds a third Retelling and Sequels advancing on sight; Ripening is M3-late; RS 9.1 stays closed (Q1). Floor-1 variety is funded in parallel: the Endless Wake joins the M2 F1 pool when its gate passes, at F1PoolParity, and F1 Blueprint and boss variety are a production priority flagged to the RS, FL and segment 9 owners (Q2). Graces ship in M2 only if the conditions owner rules the `eventBoon` form and the F1/F2 death-share gates pass, otherwise in M3 (Q3). Losing streaks get novelty and memory relief only, never easier odds or richer outcomes; difficulty help belongs in the visible ladder (Q4).**

**Change prefix.** This design uses **EV-n**. A case-insensitive grep for `\bEV-[0-9]` across the 26 approved documents finds 0 hits (re-run while writing). The bare word "EV" appears in the weapons design and means expected value, so the number is always written. Every draft used colliding local EV numbers. This document issues one register, **EV-1 to EV-45** (13), and no id has two meanings (A.6).

**Validator.** **V16 `Curiosities`** is free. V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR and V15 LP. A grep for `\bV16` finds 0 hits (re-run while writing).

**Local id schemes** (each grep-checked as a whole word, 0 hits):
- tensions **XT1–XT18**;
- toy failures **NF1–NF11**;
- catalogue rules **KR1–KR16**;
- anti-repetition devices **DV1–DV14**;
- Appendix A findings **FC** (fun critic), **BC** (balance critic) and **KC** (consistency critic).

This document's client questions **Q1–Q4** are local, as in every parent.

**Names are placeholders** until the client naming pass. Every new name was grep-checked case-insensitively as a whole word across the 26 approved documents. Twenty-six collisions forced renames in the proposal (A.5). The check was re-run while writing: the only whole-word hits among the new names are "Seating Chart" and "Last Page", which echo the Long Supper's and the Silk Archive's own approved Obsession prose (RS) and are kept on purpose as theme callbacks, and "strands" as an ordinary verb in two parents, which names no rules piece **(new lead's call)**.

**Numbers in this document are illustrative** unless a rule says otherwise, and are marked *(illustr.)* where they first appear. Every rate and share comes from the segment 8 toy (8, Appendix B); the simulator owns every published figure. **One exception (new lead's call):** the Fallow window **K = 3** and its restriction to the F1 Curiosity are the client's agreed values (Q1) and are not tuning; a gate miss that would move them returns to the client (14.2).

**Waves:** **M1** slice, **M2** full run, **M3** between runs (profile, unlocks, ladder), **M4** breadth, **Later** **(settled, MAIN 9)**.

**Abbreviations:**
- F1–F5: floors 1–5.
- Q: empty or special room. G: guard post. L: lair (FL 6.5; cross-document comparison 2026-11-23, CC-120).
- HE-n: DH healing-economy reservations. "The Due": the Dream's Due (LE 6.6).
- pp: percentage points. ‰: per mille.
- "Streak arm": the toy's losing-streak player, who dies on F1 in 40% of runs and on F2 in 60%.
- A **text-card meeting** is a `CuriosityShown` event of form curiosity that is not MECH. Interludes, MECH cards and Glimpses are not text-card meetings **(lead's call, after the check)**.
- txtNew: share of text-card meetings that show a card never displayed before.
- txtFresh: share of text-card meetings that display **in full** a presentation never displayed in full before (a new card, a Retelling not yet read in full, or a successor). A Twice-Told display is never fresh (4.5.2) **(lead's call, after the check)**.
- near3: share of text-card meetings with a card displayed in the previous 3 personal runs.
- "Q room 3" is floor 1's main-path Q room (room 3); "Q3" always means this document's Graces question **(lead's call, after the check)**.
- EventDraw, EventPlace, `NoEventLoot`, `EventDeathShareDelta` and the other `Event*` identifiers are internal names that keep "event" on purpose, as the pass and report family of segment 8; the rules noun is still Curiosity (4.1) **(told plainly, after the check)**.
- "M2 lean": the M2 subset of a device that is completed in M3; each one is defined where it stands (4.5.7, 4.5.10, 10) **(new lead's call)**.

---

## 0. At a glance

### 0.1 The lead's answers to the brief

**The design in one line:** *about one short, priced decision per floor, written into the floor's own fiction and tied to this floor's rooms. Its text changes each time you meet it again; it never pays loot, power or mercy; and losing moves the dream's memory forward, not its odds.*

#### The client's concern: repetition on a losing streak

**Verdict: the client is right. With no memory, the concern is measurable. The cure is shape and memory, not volume, and never mercy (agreed, Q1, Q4).**

**The problem, measured.** On the lead's first catalogue as written, with plain seeded draws and no memory:
- a player dying on F1–2 meets a card they have never seen on only **66%** of text-event meetings over 10 runs, and **44%** over 30 runs;
- one card is seen **3.4 times in 10 runs**;
- about **20%** of meetings repeat something from the previous 3 runs (toy, 8; *illustr.*).

**Three corrections to the premise (told plainly).**
1. **The hot spot is floor 1 and the theme count, not only floors 1–2.**
   - From RS 10.6's base clears (0.97 / 0.90 / 0.80 / 0.70 / 0.58), floor reach is **100 / 97 / 87 / 70 / 49%** *(illustr.)*.
   - DH 18.2 **(agreed)** aims most deaths at F3–5, so even competent players see F1–3 almost every run.
   - M2 ships two floor-1 themes, the Gaol and the Kennel. Each one returns about every second run, and R-SEED-6 still allows three in a row.
2. **Meeting an event again is not what tires players.** Three things do:
   - seconds of forced re-reading;
   - an answer that is already solved;
   - hidden outcomes that send players to a wiki.

   Slay the Spire repeats events constantly and players tolerate it, because each event is short and its best answer depends on the deck. FTL text and Darkest Dungeon curios wear out because they are long, solved or opaque.
3. **Events are the cheapest variety lever, not the biggest.**
   - A streaking player repeats the two floor-1 themes' fights, Blueprints and bosses far more than any event.
   - Promoting the Endless Wake into the M2 floor-1 pool moves the event metrics by only about 3 points, but it is the largest single lever for fights and themes.
   - The Wake's gate and floor-1 Blueprint and boss variety are funded in parallel **(agreed, Q2)**.

**The losing-streak model** (toy, 10 and 30 runs; full table in 8):

| Setup | 10 runs: txtNew / txtFresh / near3 | 30 runs: txtNew / txtFresh / near3 |
|---|---|---|
| First catalogue as written, no memory (FTL-like) | 0.66 / 0.66 / 0.20 | 0.44 / 0.44 / 0.23 |
| **Adopted M2 (agreed, Q1):** M2 pools + 2 Retellings + Launcher Fallow on F1 | **0.92 / 1.00 / 0.04** | **0.70 / 0.93 / 0.04** |
| **Adopted M3 (agreed, Q1):** + a third Retelling (+ Ripening, M3-late) | 0.94 / 1.00 / 0.03 | 0.75 / 1.00 / 0.04 |
| Closest measured to the rejected Q1-B: Fallow F1 + a seeded Fallow on F2–3 inside the key (RS 9.1 change) | 0.93 / 1.00 / 0.00 | 0.69 / 0.94 / 0.00 |
| Bound, not an option: least-seen inside the seed on every floor from M2 | 1.00 / 1.00 / 0.00 | 0.95 / 1.00 / 0.00 |

**The adopted devices (lead's call; agreed, Q1 for the memory devices):**
1. **Few events.** About one decision card (a **Curiosity**) per floor on F1–3. A 1-click **Interlude** in each F2–5 Forecourt when one is drawn. 0-click **Glimpses** on walls and backdrops. All of these use slots the layout already rations; no room is added.
2. **Short, open decisions.**
   - A first view is at most 40 words on F1 and 60 deeper.
   - Every card has 2–3 options plus a free **Leave**, and every cost and gain is printed from rules data. Every option but Leave carries a price.
   - A skill-gated **lit option** gives a *different* gain at a *different* price, not a discount, so the best answer depends on this party.
   - Each theme also has 2–3 near-wordless **tool cards** tied to a real room, lock or group on this floor.
3. **Cheap repeats.** Each Retelling is read in full once. After that, a **Twice-Told card** takes about 6 s and 1 click. It leads with *what is different this time*, and shows last time's choice in a footer.
4. **Fresh text.** Each card has 2 text-and-art **Retellings** in M2 and 3 in M3, on one rules chassis. They are display only, so the seed is untouched.
5. **No recent repeat on floor 1.** **Launcher Fallow** redraws the seed outside the rules core when the forecast F1 card was shown in the last 3 runs. It shares R-SEED-6's budget, and shared and daily seeds are unaffected.
6. **Losing moves the dream forward.**
   - **Strands** let a choice pay off on the next floor, the moment you enter it.
   - **Sequels** (M3) advance when a stage is *seen*, win or lose.
   - **Afterword** lines on the run-start card react to how the last run ended.
   - One sincere memorial, the **Unwoken Roll** (M3).

   All of this is content, never power.
7. **No mercy (agreed, Q4).** Nothing reads losses, streaks, HP, downs or deaths to decide which card is drawn, which options it offers or what they give (DH 5.4(f) **(agreed)**, MAIN 4.7 **(decided)**). Payment feasibility may disable a shown option, and display-only lines may react to how the last run ended (4.4.7, 4.5.10) **(lead's call, after the check:** reworded to match 4.4.7**)**. Help for struggling players belongs in a visible difficulty setting, never in events; the approved ladder only gets harder today (14.1, told plainly after the check).

**The content budget (M2, illustr.; restated after the check).**
- Pools:
  - each F1 theme: 6 standard Curiosities, 2 tool cards and 1 rare (the Wake at parity, authored in M2, agreed, Q2);
  - the shared Shallows pool: 6 F1-eligible cards;
  - each band theme: 4 standard, 2 tool cards, 1 rare and 2 Forecourt Interludes;
  - the shared Middle pool: 4;
  - Supper and Gut: 3 Interludes each;
  - the Heart: 5 Interludes plus the Coat Hooks;
  - shared Interludes: 4;
  - Glimpses: 8 per theme;
  - Afterword: about 60–90 short lines (4.5.10).
- Totals **without the Wake:** about **39 text Curiosities, 32 near-wordless cards, 78 text presentations**. **With the Wake** (the M2 plan, agreed, Q2): about **46 text Curiosities, 34 near-wordless cards, 92 text presentations, about 11–12k words of all kinds and about 400–450 art states of all kinds** (6, asset table), and about 130 human tone reviews. `F1PoolParity` binds the Wake's kit.
- **Told plainly after the check:** the proposal's "about 7k words and about 105 art states" counted card bodies and one diorama per presentation only. It left out the Wake's kit, world props in their active and inert states, per-class and per-theme variants, Glimpses, the band Interludes added after the check, Afterword and closure lines, footers and captions (6).
- If the budget slips, cut per-class and per-theme art variants first (6), then band rares, then the band's second Retelling. **Never cut F1 or Shallows pool depth, or their R2**: that is where the streaking player lives.

**Where we contest the drafts and the judges (told plainly).**
- RS 9.1 stays closed (agreed, Q1). The launcher matches an in-seed draw on the streak arm.
- Retellings sit outside the reproducibility key: text is not a rule.
- Every horn, bell, chair, clock-faced lock object and moving moon is cut.
- Cow advantage and Lure are out of event rewards. Cow advantage is a class verb (ENM 14.8; HR-22). Lure is a universal 1-tick exploration action (SH NM-4, pending the enemies owner, SH-21), and furniture would duplicate it at no cost (BC7) **(lead's call, after the check:** re-grounded; Lure is not the Shade's**)**.
- "The dream already knows your name" premises are capped at 1 per run.

#### What an event is

The rules noun is **Curiosity**, because "event" already means core rule events, telemetry, IR generation events, Mood events and Wager pip events. Curiosities come in three forms:
- the **Curiosity** card: 2–3 options plus Leave;
- the **Interlude**: 1 click, Forecourt;
- the **Glimpse**: 0 clicks, no rules.

Tags mark the near-wordless **tool** cards (MECH) and **rare** cards. Dream figures are only *implied* in M2: hands at a speaking slot, a back seen in a high window. In M3 they get one **figure frame** with its own non-unit grammar (4.11).

**Rejected:**
- random "?" rooms;
- hidden-outcome curios;
- item-key curios;
- riddles that need outside knowledge;
- a parley or dialogue system beside Cow and Yield;
- event healing in M2;
- experience from events.

#### How many per floor

| Floor | Curiosity | Interlude | Glimpses | Host |
|---|---|---|---|---|
| F1 | 1 | — | 2–4 | A `curiosityHost` signature room if any; else an Alcove set piece, else a free leaf side room. Never main-path rooms 0–2. Q room 3 gets a wall Glimpse only. |
| F2 | 1 | 1 when drawn | 2–4 | Q room; Forecourt |
| F3 | 1 | 1 when drawn | 2–4 | The Q room with the lowest main-path index; the other Q gets a Glimpse (M3: a Reading) |
| F4 | M2: 0 (Glimpse); M3: 1 in Twice-Told format if the line holds | 1, substitutive | 2–3 | Q; Forecourt |
| F5 | — | 1 (Heart; facts only) | 1–2 | Forecourt |

#### What they may give or cost

**May give (one rules gain per option, plus at most one codex page in M3; lead's call, after the check):**
- a **floor fact**, through one shared per-floor FactLedger, drawn on the map;
- a **Grace**: one existing boon on one unit, at most 1 per floor, ending at the arena seal, from Curiosity cards only (M2 only if gated through, agreed, Q3);
- a **route reveal** (a Hairline or a Latch's far side);
- a priced extra route to an existing ordinary lock that carries no value step;
- a **Strand**, cashed on entering the next floor (or at the F5 Coat Hooks);
- M3: a codex page, beside the option's one rules gain.

**May cost:**
- ticks, bound to a named patrol forecast;
- noise *here*, under the hearing rules;
- hero HP, floored and payable only above the cost;
- Weary 1;
- one of the theme's own lingering conditions;
- an item offered up;
- M3 only: carried riders (M2 if EV-11 is ruled), Mood steps and a map-colour cost.

**Never:**
- VP, notables, sockets or redraws;
- experience or coin;
- healing;
- units, or companions before camp 1;
- cross-run power;
- hidden outcomes;
- supply costs (M2);
- any draw, option set or gain that reads HP, downs, deaths, losses or streaks (agreed, Q4); only payment feasibility reads current HP, and only to disable a shown option (4.4.7).

#### Tone

Every Curiosity is written into its theme's Law and Obsession. The Dreamer is never shown or implied (RS 7.5 **(agreed)**). The register follows the floor (RS 7.3 pillar 4 **(agreed)**): comic on F1–2, with punchlines kept in Retellings read in full once and in Glimpse art; dry on F3–4; absent on F5 (≤ 8 words). A card drawable on floors of two registers is written in the drier one, and its F2 comic beat lives in the theme's F2 Glimpse art (4.11). Mechanics never joke (RC1).

#### Determinism

- A `curiosities` child stream of each floor's dungeon substream.
- **EventDraw** picks identities with no host, no loot and no run choices as input (fixed draws).
- **EventPlace** binds them to hosts after LootBind, draw-free, before V13g and V15g.
- Retellings, the Twice-Told card and Afterword lines are display only and sit outside the reproducibility key.
- The launcher reads profile memory outside the seed, as R-SEED-6 already does.

### 0.2 The client's four decisions (agreed, 2026-11-22)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Cross-run memory model | **A.** M2 is seed-pure. Display-only Retellings (R2 in M2, R3 in M3) outside the reproducibility key; the Twice-Told card; Launcher Fallow (K = 3) on the F1 Curiosity only, inside R-SEED-6's budget. M3 adds R3 and Sequels advancing on sight. Ripening is M3-late. RS 9.1 stays closed | B: A plus a least-seen draw inside the key for the F2–3 band in M3 (reopens RS 9.1; shared and personal runs would differ). C: no memory (0.44 new at 30 runs) | EV-17 ⚑ to EV-20 ⚑, EV-26 ⚑ to EV-28 ⚑ |
| Q2 | Floor-1 variety in parallel | **A.** Promote the Endless Wake into the M2 F1 pool when its gate passes, at F1PoolParity; prioritise F1 Blueprint and boss variety, a production priority flagged to the RS, FL and segment 9 owners | B: events only | EV-44 ⚑ |
| Q3 | Graces | **A.** M2 only if the conditions owner rules the `eventBoon` form and the F1/F2 death-share gates pass; otherwise M3 | B: M3 regardless | EV-11, EV-12 ⚑ |
| Q4 | No mercy | **A.** Confirmed: losing streaks get novelty and memory relief only, never easier odds or richer outcomes (DH 5.4(f) **(agreed)**; MAIN 4.7 **(decided)**). Difficulty help belongs in the visible ladder | B: visible opt-in streak help through events | EV-10 ⚑ |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | **Rules noun and forms:** Curiosity (card, Interlude, Glimpse; MECH and rare tags) (4.1) | M2 | lead's call; EV-1 |
| 2 | **Budget:** about one decision per floor; no added rooms; F1 hosted by an Alcove set piece first; Q room 3 and the F1 Forecourt get wall Glimpses only; draw-free host and referent tie-breaks (4.2) | M2 | lead's call; EV-2 ⚑ |
| 3 | **Card shape:** printed chips; a free Leave; every other option priced; at most 1 lit option, which must be a **sidegrade** in gain and in price (a different gain at a different price, never more value); rolled options rare, with odds shown (4.3.1) | M2 | lead's call; EV-4 ⚑ |
| 4 | **Checks:** no-roll thresholds. Party-best by default; the worst member for movement Stealth; the hero for Leadership and Intimidation. This defines STAT 7.1's "event outcomes" (4.3.2) | M2 | lead's call; EV-5 ⚑, EV-6 ⚑ |
| 5 | **Gains:** one rules gain per option; one fact family list and one per-floor FactLedger of fact instances. F-BOSS is run-specific where segment 9 allows. Fact-only options are at most 40% of a deck, 50% on the Graces-off fallback deck (4.4.1, 4.4.2) | M2 | lead's call; EV-7 ⚑, EV-8 |
| 6 | **Costs:** bound ticks; noise here (F1 ≤ 4); hero HP with payment feasibility; Weary; theme conditions; offered items. No supply cost and no coin (4.4.4, 4.4.7) | M2 | lead's call; EV-9 ⚑, EV-10 ⚑ |
| 7 | **Graces:** existing ids through one new deferred-boon form; single unit; 1 per floor; end at the arena seal; never free and never priced in ticks alone (4.4.3) | M2 if ruled and gated, else M3 | **agreed, Q3**; EV-11, EV-12 ⚑ |
| 8 | **No verb giveaways:** no Cow-advantage Grace, and no Lure from furniture (Lure is a universal exploration action) (4.8) | M2 | lead's call; EV-13, EV-14 ⚑ |
| 9 | **Loot:** skin only, plus priced routes to unstepped ordinary locks. Never a price on a free socket. Never HE or the Due (4.4.5) | M2 | lead's call; EV-15 ⚑ |
| 10 | **Within-run caps:** no repeats; prop-family and chassis caps; one noise card per floor; at most one self-reference premise per run; shared shares capped by pool size; caps count draws (4.2, 4.5) | M2 | lead's call; EV-16 |
| 11 | **Repeat card:** each Retelling read in full once, then the Twice-Told card, delta first; auto-answer only for no-roll information options (4.5.2) | M2 | **agreed, Q1**; EV-17 ⚑ |
| 12 | **Retellings:** display only, outside the key; R2 in M2, R3 in M3 (4.5.3) | M2 / M3 | **agreed, Q1**; EV-18 ⚑ |
| 13 | **Profile memory:** `curiosityMemory` in the MAIN 6.6 profile save (4.10, 9.1) | M2 | **agreed, Q1**; EV-19 ⚑ |
| 14 | **Launcher Fallow:** K = 3, on the F1 Curiosity only, inside R-SEED-6's budget. RS 9.1 stays closed (4.5.4) | M2 | **agreed, Q1**; EV-20 ⚑ |
| 15 | **Determinism:** EventDraw and EventPlace; a fixed draw map with named hash labels; resolving a Curiosity is a safe point with a fixed write order (4.10) | M2 | lead's call (draw map: new lead's call; draw order and write order: lead's call, after the check); EV-21 ⚑ |
| 16 | **Strands:** committed run flags cashed on entering the next floor (or at the F5 Coat Hooks). Every F1 Strand pays off on entering F2. Unpaid Strands get a closure line (4.5.7) | M2 lean / M3 | lead's call (cash points: lead's call, after the check); EV-22 ⚑ |
| 17 | **Clock:** F1–2 ≤ +0.5 min; F3 ≤ +0.5 on light and median arms; F4–5 net ≤ 0. Segment 8 cuts itself first (4.7) | M2 | lead's call; EV-23 ⚑ |
| 18 | **Register:** the comic register is kept on F1–2. The drafts' "deadpan reading" is withdrawn (4.11) | M2 | told plainly; EV-24 ⚑ |
| 19 | **Tone guards:** premise tags and the premise test; a cap on domestic props in the Heart; reserved silhouettes and cues; no Portal or container reads; staging (4.5.6, 4.11) | M2 | lead's call (test, reads and staging: lead's call, after the check); EV-25 ⚑ |
| 20 | **Losing moves the dream:** Afterword lines (M2 lean); Sequels advancing on sight (M3); the Unwoken Roll (M3); never cumulative counts (4.5.8, 4.5.10) | M2 lean / M3 | **agreed, Q1** (Sequels on sight); EV-26 ⚑, EV-27 ⚑ |
| 21 | **Ripening:** M3-late or M4, under SuccessorParity (4.5.9) | M3-late / M4 | **agreed, Q1**; EV-28 ⚑ |
| 22 | **M3 meta:** Fragments folded into F-BOSS options; Readings; Omen Retellings; Nightmare Provisos (costs only); Overtures; Easing (4.9) | M3 | lead's call; EV-29 to EV-33 |
| 23 | **Floor-1 variety in parallel:** the Wake at F1PoolParity when its gate passes; F1 Blueprint and boss variety as a production priority (0.1, 5.3) | M2 | **agreed, Q2**; EV-44 ⚑ |
| 24 | **No mercy:** novelty and memory relief only (4.4.7, 4.4.8) | All | **agreed, Q4**; EV-10 ⚑ |
| 25 | **Later:** Pledge sources and Testimony; the Hawker's Hour (4.8, 10). Curse cures and bargains **moved to M4** (EV-43; agreed, heirlooms and curses Q5; HL-29; heirlooms and curses design 4.2, 10) | Later; M4 (EV-43) | lead's call; EV-41 to EV-43 |

### 0.4 Intent, goals and anti-goals (new lead's call: added in the template's shape)

**Intent.** A floor should hold about one moment that is not a fight and not a container: a short, priced, honest decision written into this theme's dream, tied to this floor's rooms, that reads differently the next time you meet it. Segment 8 adds **no value, no units and no hidden information**.

**Goals.**
1. Every Curiosity is a real decision whose best answer depends on this party and this floor.
2. Every cost and gain is printed from rules data; mystery lives only in text and art.
3. A player on a losing streak meets fresh text on nearly every meeting and almost never meets the same F1 card twice within 3 runs.
4. Curiosities stay inside the floor clock and cut themselves first.
5. Shared and daily seeds stay canonical; profile memory never enters the rules core.

**Non-goals.** The value economy and the fixed deck (7a); item identities (7b); loot sockets (7c); boss arenas (segment 9); the screens themselves (segment 10); the seed tree and validator plumbing (segments 11–12); Pledges and companions (CMP, Later).

**Anti-goals.**
- Random "?" rooms and hidden outcomes.
- A wall of text met a hundred times.
- Event mercy, pity or streak help.
- Event loot, event healing, event experience.
- Dialogue trees and riddle homework.
- A dream figure that reads as a unit, or anything that shows or implies the Dreamer.

---

## 1. How games handle events and repetition

Details are from memory unless sourced (Appendix B). "Unsure" marks uncertain recollection.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Slay the Spire | "?" rooms (unsure of exact odds); one screen with 2–3 explicit trades; act pools plus a shared pool; a seen event leaves the run | Short, explicit, state-dependent trades; no repeat within a run | Solved events (some always-take answers); random room roles |
| FTL | Text beacons; blue options gated by crew and systems; many hidden outcomes | Lit options | Identical long text seen 100 times; hidden odds; wiki homework |
| Darkest Dungeon 1 | Curios; the right provision gives a known result, otherwise a random one | — | Item-key checklists; random punishment (RC1; TL and STAT anti-goals) |
| Darkest Dungeon 2 | Road events; hero shrines unlock backstory chapters in sequence (unsure on details) | Sequential versions that cost no time on repeat (Retellings, Sequels) | Repeated road events at full length |
| Hades | Priority dialogue keyed to run state and the last death; death advances the story; Chaos gates are explicit curse-then-boon trades | Afterword lines; Sequels that advance on sight; printed trades | Any mechanical mercy |
| Binding of Isaac, Spelunky, Brogue | Systemic, legible trades: devil rooms, Kali altars, vault cages | MECH tool cards tied to a real room, lock or group | Captives as mid-floor allies (rejected, CMP 7.12) |
| Gloomhaven / Frosthaven | A persistent event deck: cards return or are removed, new cards are added (rules.dized.com) | The idea survives as launcher Fallow and Ripening | An in-key deck snapshot (RS 9.1) |
| Fallen London, storylets | Content gated by qualities | Strands; party-keyed lit options | Time-costly repeats |
| Griftlands, Cobalt Core | Mechanical social play; character memories | Text compresses on repeat | Long repeat dialogue |
| Rogue Legacy, Inscryption | Journals and lines unlocked across runs | Sequels, the codex | Gating base pools by run count (it starves new players) |
| Angry GM, the Alexandrian | "No decision, no encounter"; the three-clue rule | Glimpses never take a click; facts arrive by many routes | — |
| Blades in the Dark devil's bargain | An optional boon for a printed complication | Priced options | — |
| Banner Saga | Opaque choices; heroes can die | — | Death by information the player never had (DH 2) |
| Balatro, Monster Train | Sparse or no events | Combat-first games keep events small | — |

**Lessons (lead's call).**
1. Fatigue tracks seconds of reading × times seen × whether the answer is solved.
2. A context-free dominant answer is a tax. OptionTakeRate above 850‰ triggers a rework.
3. Mystery belongs in text and art; rules are always shown.
4. Cross-run memory should change presentation and content, never odds or power.
5. One chassis with N Retellings is the cheapest variety per design hour.
6. Systemic cards survive repetition; written ones go stale. Each theme gets both.

---

## 2. The current-systems audit

### 2.1 What the approved documents fix for segment 8 (settled unless a flagged EV changes it)

**Hosts**
- FL 6.5: "segment 8 fills Q rooms; floors 2–5 antechambers may host non-encounter micro-events". Untrimmed F5 has no Q.
- Role orders give Q rooms of:
  - F1: 1 (room 3);
  - F2: 1;
  - F3: 2, also 2 when trimmed;
  - F4: 1, also 1 when trimmed;
  - F5 trimmed: 1.
- FL 13 row 8 hands over Q rooms, the Forecourt micro socket, trader and Pledge tags, and signature rooms.
- F1 Q room 3 hosts the CL-10 cowable Scavengers on the Circuit and, in the Gaol, a F1LessonLadder lesson.
- The F1 Forecourt is empty, near-safe, single-inbound, has no Alcove (LG5) and hosts the Catch-up (RS 9.4; FL 6.11 **(agreed)**).
- LT16: side-room claimants already exceed F1's 2–3 side rooms.
- LP-11 (pending): an Alcove's default occupant is a set piece where segment 3 offers one.

**Loot**
- Ledgers A/B/C never grow (LE 6.1). Everything is fixed on `lootPlan` (LE-14).
- LootDoorInvariant holds; theme bias is 0‰ (RS 6.4; IR-15 **(agreed)**).
- LP 4.13: skin a socket, never add one; no event spawns or moves units; no clock Landmark in a `windfallHost` room; V15g runs after segment 8.
- The Q socket is SR0 and often HE-3, HE-4 or the Due: no check, no lock, never behind a choice (LP 4.2; DH 12.4; LE-13).
- Coin is spent only at camp, at a Ticket lock (M3), and, Later, at a wandering trader (ITEMS 10.1, as amended; **agreed 2026-11-23, cross-document comparison Q-1**). Curiosities still never spend or give coin (4.4.4). Fragments are 0 VP and 0 coin (LE 6.12).

**Healing and death**
- No pity timers (DH 5.4(f) **(agreed)**).
- No free healing between fights and no passive regeneration (DH 2; MAIN 4.4 **(decided)**).
- Out-of-combat damage is floored at 1 HP (DH 10.4 **(agreed)**).
- Heal sources are closed (DH 12.1; the HZ-30 path).
- F1 hero deaths under 5% (DH 18.2 **(agreed)**). `Floor1BossSustain` is calibrated on the M1 package (DH 21.1).

**Units**
- No event spawns or moves units (EP 13).
- `NoFloor1Companion` (CMP 7.1).
- Pledges are Later: F2–4, at most 1 per floor, met at the next camp (CMP 7.12). `captiveSocket` is reserved (EP-39).

**Checks**
- CC1–CC8 (TL 6.1): information is a threshold; roll only with the cost printed and exact odds; the best member is chosen automatically, companions included.
- No free retries (STAT 7.2).
- Party rule (STAT 5.4 **(agreed)**): best member; worst for Stealth; Presence is the hero's.
- TL-34 (Readings) is pending and waits on segment 8.

**Morale and class verbs**
- No spare, rob or execute menu; "a choice with no mechanical difference is noise" (ENM 14.6).
- Cow (ENM 14.8). Show of Force is a Herald tier-1 slot spell (HR-22). Lure (SH NM-4) is adopted by the Shade design for M2 as a **universal** 1-tick exploration action, pending the enemies owner (SH-21), and is a universal baseline for Shade leans (SH "Lean, never require"); `luredThisFloor` is a per-group flag, not a Shade resource.

**Conditions**
- C5: only event sources and the starting boon grant until-rest boons without concentration; one lingering boon per unit; a new one replaces the old.
- The 32-id budget is full (C16). Harmful lingering cap of 2 (C14). Each theme has at most 6 harmful conditions (C16; RT-16 open).

**Tone and readability**
- RC1–RC9, including RC1+ (every walkable object is a rules piece; nothing remains after an event resolves).
- RC2: chairs only tucked into tables. RC3: no lock glyph uses a clock face. RC5: the hourglass is the Hour lock's. RC7: horns, alarm bells and the Choir's struck-bronze cue are reserved.
- RS 7.5: never show or imply the Dreamer, including age. RS 7.6 watch-list. RS risk 14: story by accretion.
- Theme "wrong details": the Kennel's painted moon never moves; every Wake portrait's eyes are closed; every Wake clock is stopped at the moment of death (RS 8.5).
- The Bedside Stand is a reserved Viaticum silhouette (LP-14, LP-24).

**Seeds**
- Key = (seed, manifest, configuration, unlock snapshot). Shared and daily seeds use the canonical set (R-SEED-5).
- RS 9.1 **rejects** "cross-run weighting inside the seed".
- R-SEED-6 is the launcher streak cap: up to 8 redraws.

**Clock**
- The CMP 7.4 gate (party 4, F4–5 median ≤ 20 min) gets no new lever **(agreed)**.
- Each segment funds its own line: FL-16, TL-38, HZ-27, EP-33, LP-10.
- Dream Fragments moved to M3 for the clock (CF-M6).

**Saving**
- MAIN 6.6 **(decided)**: the run save is a snapshot written at safe points; the profile save holds "unlocks, boons and difficulty tier".

### 2.2 What the approved documents owe segment 8

- STAT 7.1: Leadership's "event outcomes" (still undefined after CMP-14). STAT 10: non-combat content on most floors.
- TL-34 Readings and TL 6.12: segment 8 owns the content. TL 13: Riddle-lock clues (Hymn, Index, Place-Setting, Story) and Hour locks.
- HZ 13: unguarded Dare forms, Moods named on the Stratum Card, empty Tide naves.
- LE 12 and LE-18: the shared boss-fact family. LP 4.13 and LP 17: the Hawker's Hour, plus Lost Property with camp and hub.
- CMP 17: captive rooms and shrines for Pledges. EP 13: activity vignettes in Q rooms.
- RS 12: the codex (no owner) and "profile-side anti-streak outside seeded runs" (M3).
- Each hook is answered or handed on, with an owner and a wave, in the hook-answer table of 4.13 **(lead's call, after the check)**.

### 2.3 Tensions and resolutions

| Id | Tension | Resolution |
|---|---|---|
| XT1 | The client's "floors 1–2" against the reach data | The hot spot is F1 plus theme frequency; depth goes there (told plainly) |
| XT2 | Cross-run memory against RS 9.1 and seed purity | Display-only Retellings, launcher Fallow (R-SEED-6 precedent), and content unlocks only through the snapshot (M3, flagged readings). No in-key weighting (agreed, Q1) |
| XT3 | Streak relief against DH 5.4(f) and MAIN "grind to win" | Novelty only; StateBlind; SuccessorParity (agreed, Q4) |
| XT4 | STAT 2 non-goal (social dialogue checks) against Leadership's "event outcomes" | One hero-only threshold on one option; no dialogue (EV-6) |
| XT5 | "Skin, never add" against a reward vocabulary | Facts, Graces, routes, Strands; priced routes to unstepped locks only |
| XT6 | Full condition budget against Graces | Existing ids through one deferred-boon form (EV-11; agreed, Q3) |
| XT7 | No event time budget | A segment 8 line and a self-cutting order (EV-23) |
| XT8 | Floor 1 seen 100% but with the least room | Alcove set piece first; Glimpses only in Q room 3 and the Forecourt (EV-2) |
| XT9 | EP 13 against figures and noise | Figures are framed map objects; noise moves units only through the hearing rules (EV-14, EV-45 readings) |
| XT10 | Landmark ownership (HZ 13, LP 4.13, FL 13) | Segment 8 places none (EV-35) |
| XT11 | Mystery against RC1 and CC3 | Mystery in text and art; outcomes always printed |
| XT12 | Experience pool | Events pay 0 XP (ENM 16; the Annex precedent) |
| XT13 | Companions are Later and next-camp only | Pledge sources and Testimony are Later (EV-41) |
| XT14 | Stratum Card line budget; "event" overloaded | No M2 line; the noun Curiosity (EV-1) |
| XT15 | Theme-keyed loot rejected | Theme fiction only; loot-blind |
| XT16 | Comic register on the most-repeated floors | Punchlines live in Retellings and Glimpse art; the register is unchanged (EV-24) |
| XT17 | Verbs (the Herald's Show of Force; the universal Lure) duplicated by furniture | Removed from event rewards; a Herald-lit variant offered for M3 (EV-13); no Lure variant at all (EV-14) |
| XT18 | Self-reference premises read as an author (RS 7.5, risk 14) | Premise tag cap (EV-25) |

---

## 3. Event models

### 3.1 The models

- **Model A, curated-few (Cards, Interludes, Glimpses).** Uses only the rationed slots. Printed chips, free and lit options, StateBlind, CuriosityLootBlind, Graces that end at the arena, an honest clock cut order. Least-seen inside the key in M3 (that last part rejected, Q1).
- **Model B, storylets (Signs, a salience ladder, Stakes).** A seed-pure Stake band drawn before memory; T1–T5 salience tiers; Sequels; a shared FactLedger. Recollection inside the key from M2.
- **Model C, systemic vignettes (Curios with Referents, the draft's "Bindings", and Overtures).** A card tied to a real group, trap, lock or Latch. Conditions applied to a tied group at its fight start. Faceless figures.
- **The lead's catalogue** (reference). MECH tools, a rare tier, Ripening and the richest themed content.

### 3.2 Judges' scores (mean of three judges, 1–10; the lead catalogue was scored by one judge)

| Model | Memorable | Low repetition | Decisions | Theme and tone | Pacing and clock | Determinism and testability | Fit with docs | Build cost (higher = cheaper) | Mean |
|---|---|---|---|---|---|---|---|---|---|
| A curated-few | 6.7 | 7.3 | 7.0 | 8.0 | 7.7 | 8.0 | 7.3 | 6.0 | **7.25** |
| B storylets | 8.0 | 9.0 | 7.0 | 7.7 | 6.7 | 5.0 | 5.0 | 3.0 | 6.4 |
| C systemic | 5.7 | 6.0 | 9.0 | 6.0 | 8.0 | 7.3 | 6.0 | 5.3 | 6.7 |
| Lead catalogue | 8 | 6 | 7 | 9 | 7 | 8 | 6 | 5 | 7.0 |

### 3.3 Verdict (lead's call; agreed, Q1 for memory)

All three judges independently chose the same winner:
- **Model A as the chassis,** filled with the lead catalogue's content and shape, patched by the numbers check;
- **Launcher Fallow and display-only Retellings** for memory **(agreed, Q1)**;
- **RS 9.1 closed** **(agreed, Q1)**.

**Grafts taken:**
- From B: the shared FactLedger; value parity as a *static check* (DeckValueParity, SuccessorParity), not as a draw filter, because the Stake filter doubles near-repeats; and the forbidden-input list.
- From C: Referents on tool cards, and the rejection of priced access to free sockets. Overtures move to M3 behind the enemies owner.
- From the anti-repetition and new-mechanics branches: the repeat card, First Telling, Afterword lines and Sequels on sight.

**Declined:**
- B's salience engine and its Recollection inside the key;
- A's least-seen draw in M3 (client option Q1-B, **rejected**, Q1);
- the strict F1 + F2 Freshness check, which exhausts the redraw budget by runs 30–50;
- Late Leaves, which can be farmed by abandoning runs;
- C's acting-unit Stealth for party movement.

---

## 4. Rules in full

### 4.1 Taxonomy (lead's call; change EV-1)

| Term | Definition | Clicks | Wave |
|---|---|---|---|
| **Curiosity** | The umbrella rules noun, and the main card form: one modal, 2–3 options plus Leave | 1–2 | M2 |
| **Interlude** | A Forecourt card: 1–2 options plus Leave, ≤ 12 words (Heart ≤ 8). Gains are **arena-facing** facts only: F-BOSS (F2–3 only), F-LISTEN on the arena, arena-path F-TRAP, or arena F-ROOM (`ForecourtGainFacesArena`; lead's call, after the check). One presentation, no Retellings | 1 | M2 |
| **Glimpse** | A wall, backdrop or out-of-reach scene. No rules and no decision. Optional hover caption ≤ 12 words | 0 | M2 |
| **MECH** tag | A near-wordless tool card (title, one line, one priced option plus Leave), tied to a named referent on this floor, with a fallback gain when none exists (4.5.5). One presentation, no Retellings | 1 | M2 |
| **Rare** tag | A set piece allowed up to 90 words; at most 1 per theme pool; drawn when the slot's rarity roll is under about 30‰ *(illustr.)* (4.10) | 2 | M2 / M3 |
| **Grace** | An event boon on one unit, built from an existing id (4.4.3) | — | M2 if ruled and gated, else M3 (agreed, Q3) |
| **Strand** | A run flag set by an option, cashed on entering the next floor, or at the F5 Coat Hooks (4.5.7) | — | M2 lean / M3 |
| **Referent** | The named room, lock, Latch, Hairline or group a tied option acts on, chosen draw-free at EventPlace (4.5.5). Renamed from the drafts' "Binding", which EP uses (A.5) | — | M2 |
| **Retelling** | A text-and-art variant of one card on an identical rules chassis | — | M2: 2; M3: 3 (agreed, Q1) |
| **Twice-Told card** | The repeat presentation, once each Retelling has been read in full (4.5.2) | 1 | M2 (agreed, Q1) |
| **First Telling** | The first full display of a card's R1 to a profile: R1's diorama in a First Telling treatment, shown once; a treatment, not a separate asset | — | M2 |
| **Fallow** | The launcher rule against recent F1 repeats (4.5.4) | — | M2 (agreed, Q1) |
| **Sequel** | A cross-run chain stage that unlocks on sight (4.5.8) | — | M3 (agreed, Q1) |
| **Ripening** | A successor that replaces a much-seen card (4.5.9) | — | M3-late / M4 (agreed, Q1) |
| **Afterword** | A display-only line keyed to the last run's end (4.5.10) | — | M2 lean / M3 |
| **Figure frame** | The one cross-theme, non-unit treatment for dream figures, with its own grammar (4.11) | — | M3 |

"Lucid frame" and "Known card" from the drafts are renamed. "Lucid" is on the hazards avoided list; "Known" means known traps (A.5).

### 4.2 Placement (lead's call; change EV-2)

- **Per floor:** as in 0.1. A signature room marked `curiosityHost` is **always** the floor's Curiosity host when the floor has one, *instead of* the Q or leaf host, never as an extra one **(lead's call, after the check:** "may" made a rule**)**.
- **F1 host order:**
  1. **An Alcove set piece.** Wall-mounted, RC1+. It takes the set-piece slot LP-11 already prefers. It never empties a leaf and never displaces a container that 7c's cap needs (flagged to FL, segment 3 and 7c).
  2. **A free leaf side room,** as the **lowest-priority** claimant (LT16, flagged).
  3. **Else none,** logged as `CuriosityAbsent{slot: curiosity, reason: noHost, blueprintId}`.
- **Tie-break (lead's call, after the check).** Wherever several hosts or referents qualify (which Alcove, which leaf, which Q room, which lock or group), EventPlace takes the lowest main-path index, then the lowest room id. Side rooms take the main-path index of the room they hang from. "The first Q room" on F3 means the Q room this rule picks. No draw is used.

  Q room 3 carries a wall Glimpse only. It keeps the CL-10 Circuit and its one lesson, and no Curiosity adds a lesson tag. The F1 Forecourt's walls may carry Glimpses: a flagged reading of RS 9.4 and FL 6.11 "empty", since a Glimpse has no rules, no square and no click. Report `F1CuriosityPresence` per Blueprint before F1 pool sizes are frozen: it is 1000‰ under V16s `F1HostAlways` (9.2), and any shortfall is a defect, not a tuning miss (SV-30) (cross-document comparison 2026-11-23, CC-117).
- **Never:**
  - in arenas, the Sanctum, the Annex, a Dare or Wager room, the elite room, a boxed-lurker host, or F1 main-path rooms 0–2;
  - on a walkable square (RC1+);
  - a clock Landmark anywhere (EV-35).
- **One Curiosity per room.** **Closed while combat is active anywhere** (the ITEMS 10.9 NotInCombat precedent). **Gone after it resolves:** the prop goes to its inert state, for example a dust sheet over it above head height, never a Portal skin (the residue rule; 4.11) **(lead's call, after the check:** "the Drape is drawn" reused the Drape Portal's skin**)**.
- **Pools:** each theme's `curiosityKit`, plus shared pools by tier, reusing RS 8.0's tier names: Shallows (F1–2 eligible), Middle (F2–3), Deep (F4) and Forecourt (shared Interludes).
  - The shared share is the **per-slot probability that the shared pool is chosen over the theme pool**: **min(400‰, shared ÷ (shared + theme))** for Curiosity slots *(illustr.)*, which fixes NF1, and **at most 200‰** for Interlude slots (NF3). The pool roll is defined in 4.10 **(lead's call, after the check)**.
  - Each shared Interlude is legal on at most 2 floors, with at most 1 shared Interlude per run (NF3).
  - Every theme drawable on F2–5 has its own Forecourt Interludes: 2 per band theme (added after the check), 3 for the Supper and the Gut, 5 for the Heart. V16s `PoolCoverage(interlude, floor)` fails while any theme drawable on that floor has an empty Interlude pool **(lead's call, after the check)**.
  - **An empty slot.** When EventDraw's fallbacks are spent (4.10), the slot is empty and logged as `CuriosityAbsent{slot, reason: poolEmpty | capsExhausted}`. An empty Forecourt Interlude slot shows a Glimpse. Strands no longer depend on Interludes (4.5.7).
- **Pending request (XI-27; recommended M2): beckoning Glimpses.** On floors with side rooms, at least 1 Glimpse per floor *(illustr.)* on a wall visible from the main walk line inside or at a side room's threshold; never a Portal or container silhouette; report `BeckonFollowRate`. Not adopted until the EV owner rules; if refused, no Glimpse rule and `BeckonFollowRate` is dropped (exploration and information design 4.4.6).

### 4.3 Structure, choices and checks

#### 4.3.1 The card (lead's call; change EV-4)
- **Options:** 2–3 plus **Leave**, which is always free. **Every other option carries a printed price** (lead's call, after the check). At most 1 lit, at most 1 rolled.
- **Leave (lead's call, after the check).** Leave closes the card without resolving it. The prop stays active, and the card can be reopened while the party is on the floor; reopening shows the same presentation and is not a new sighting (4.5.1). The card resolves only when an option is taken.
- **Chips:** every option ends in rules chips generated from data, for example `−2 HP (hero; not below 1)`, `noise 4 here (who hears: the Turnkey patrol)`, `+6 ticks (patrol moves: see map)`, `Fact: the floor key's carrier`. Prose never states a rule (RC1, RC7). Spatial facts are **drawn on the map** the moment they are gained (hook, segment 10).
  - **Layout budget (lead's call, after the check).** At most 2 chips per option and at most 6 words per chip. Patrol and route detail lives in the map highlight (4.12), never in chip text. A lit option is marked by a shape icon, never by hue alone (RC4 and colour-blind grammar). The worst-case card (the longest rare, three options, a delta line and a footer, at maximum text scale and minimum resolution) must fit on one screen; this is a segment 10 acceptance item, and if it fails, the rare body cap is lowered. Segment 10 checks it as V18s `CardFit` at 150% text scale (exploration and information design 4.10).
- **Lit option rules:**
  - **`LitIsSidegrade`:** the lit option's gain differs from the priced option's in kind or target.
  - **`LitHasPrice` (lead's call, after the check):** the lit option carries a printed cost that shares no cost kind with the priced option's (for example ticks against noise, or HP against an item). A met threshold is never the whole price. A teaching rung (K2) is a priced lit option like any other.
  - **`LitNoExtraGain`:** its value is no more than the priced option's band, and never "the priced gain plus something", read from `curiosityValueTable` (4.4.9).
  - A pure cost waiver is never allowed: the lit option always gives a different gain **(lead's call, after the check;** it replaces the third-option exception, which no Interlude could meet**)**.
  - Unmet lit options collapse to one line: "Also possible with: Lore 13".
  - Report `LitDominanceShare`: above **850‰** means rework, the same line as `OptionTakeRate` **(lead's call, after the check;** was 900‰**)**. This settles the fun critic (FC1) and the balance critic (BC5) together: the lit option is a different decision at a different price, and a bigger party gets more choice, not more value.
- **ENM 14.6 applied:** no two options share an effect set (`MechanicalDifference`). One attempt only.
- **Text caps:**

  | Element | F1 | F2–3 | F4 | F5 |
  |---|---|---|---|---|
  | Curiosity body, first view | ≤ 40 words | ≤ 60 | ≤ 45 (M3) | — |
  | Rare body | ≤ 75 | ≤ 90 | ≤ 60 | — |
  | Interlude body | — | ≤ 12 | ≤ 12 | ≤ 8 |
  | Option label | ≤ 10 | ≤ 10 | ≤ 10 | ≤ 5 |
  | Twice-Told body | ≤ 15 | ≤ 15 | ≤ 15 | ≤ 5 |

#### 4.3.2 Checks (changes EV-5, EV-6)
- **No-roll thresholds by default** (CC1). This accepts TL-34's threshold principle for options. Passive = 10 + modifier (STAT 6).
- **Threshold ladder** (TL 6.2's middle rung, *illustr.*): F1 12, F2–3 13, F4 14, F5 15. DCs never scale with party size (CC6).
  - **F1 teaching rung 9 (new lead's call; illustr.).** The proposal's "9 for MECH lit lines" is corrected: MECH cards carry one priced option and no lit option (KR3), so the rung 9 is instead allowed on **at most one lit option per F1 theme pool**, as a teaching rung. In M2 it is the Kennel's K2 only.
  - **Shared pools read the ladder of the floor they are drawn on (new lead's call).** A Shallows or Middle card's catalogue number is its lowest eligible floor's value; `thresholdByFloor[]` carries the rest (D2 and D5 read 12 on F1 and 13 on F2).
- **Who:**
  - dungeon skills (Arcana, Lore, Mechanics, Athletics, Acrobatics, Perception, Medicine) use the best eligible member, companions included (CC8);
  - **party-movement** Stealth ("slip past", "tiptoe") uses the **worst** member (STAT 5.4);
  - a **single-actor manipulation** ("lift the key off its string") uses the acting unit, a flagged reading under the Steal precedent ("any party member's Stealth", STAT);
  - **Leadership and Intimidation use the hero** (Presence).

  The card shows who meets the threshold: "Met by Wren, Lore 15".
- **Leadership's "event outcomes" (EV-6, flagged reading of STAT 7.1 Leadership row and STAT 2 non-goals):** one hero-only threshold on one option. No dialogue tree and no roll.
- **Rolled options** are capped at 1 per band theme pool. They print exact odds and the failure cost (CC2, CC3), roll on `checks` in the keyed-check domain `h:curiosity`: `H(floorKey/checks, "h:curiosity", LabelId(curiosityId), LabelId(optionId), 0, die)`, attempt index 0 (SV-6; seeds and validation design 4.3; M2), and allow one attempt.
- **`LitOptionSpread`** target: each launch class meets a lit option on ≥ 500‰ of the Curiosities it meets *(illustr.)*, **measured at party 1 with each class's creation-default skills** from the stage-2 class designs **(lead's call, after the check;** the proposal's class-to-skill list did not match those defaults**)**:
  - Arcanist: Lore, Medicine (ARC);
  - Shade: Stealth, Medicine, Perception (SH 7.14);
  - Vanguard: Athletics, Medicine (VG);
  - Herald: Intimidation, Medicine (HR).

  Leadership, Arcana, Mechanics and Acrobatics are optional picks and count for the classes that take them. **`LitSpreadPerPool`** (V16s, static): in each F1 theme pool, every launch class meets the lit option of at least 3 of the 6 standard cards with its default skills. Each F1 pool meets this with one lit option each in Lore, Perception or Stealth, Athletics and Intimidation, and two in Medicine (5.3).

### 4.4 Outcomes

#### 4.4.1 Gains (change EV-7)
- **One rules gain per option (lead's call, after the check).** An option gives exactly one of: a fact, a Grace, a route, or a Strand. In M3 it may add one codex page beside that gain. A Strand is a gain in its own right and is never stacked on another (`OptionShape`). The proposal's double-gain rows (G1, G9, W6, C7, Su4) are split in 5.3.
- **Facts.** The ENM 14.6 five: F-TRAP, F-SECRET, F-BOSS, F-ROOM, F-KEY. Plus **F-LISTEN** (one room's Listen result, as if listened at its door now) and **F-CACHE** (the contents of one optional, unstepped, non-HE, non-Due container; counted as exposure under LE 6.9; respects CT2 `ContainerArtBlind` and the `known` field; LE and 7c owners).
  - One fact per option, naming its **target** by a selector: the option's referent, the next unvisited room by main-path index, the player's choice, the arena path, or the arena (4.4.2).
  - **`GainMix` (restated after the check):** a **deck** is a theme's `curiosityKit` Curiosity cards (standard, MECH and rare), or one shared Curiosity pool (Shallows, Middle). Its options are every priced, lit and rolled option; Leave is excluded, and so are Interludes, which are fact-only by definition. An option is fact-only when its one rules gain is a fact (an M3 codex page does not count). Fact-only options are **at most 40%** of a deck's options *(illustr.)*. On the **Graces-off fallback deck** (4.4.3) the ceiling is **50%** *(illustr.)*, a separate threshold **(lead's call, after the check;** told plainly in 14.1**)**. 5.2 lists the per-pool counts, so V16s and the catalogue agree.
  - **F-BOSS is run-specific** where segment 9 exposes 2–3 seeded boss variables (phase variant, move order, escort, arena hazard placement). Otherwise it is the boss's Lore entry (hook). A veteran still gains something, and it is information, not power.
  - **Answered by segment 9 (BR-29; M2):** two seeded values per boss, the **Rendition** and the **mirror** (where the Rendition leaves it open), yield three run-specific fact lines: the Rendition and its phase change, the **Guise** (Device and reserve-socket side, worded relative to the entrance) and the **formation**, all draw-free from `BossVariantKey`; each boss authors ≥ 4 facts plus mirror-aware variants (boss rooms design 4.12, 4.10.1).
  - **F-TREASURE (XI-22; agreed, exploration and information Q5, for the exclusion; M3):** an eighth fact category, one instance `{F-TREASURE, socketId}` in the FactLedger: an unfound field notable at a container socket on the current floor, chosen by its own role-blind selector (tier descending, then the room's macro-grid position, then the container's square; never room role, `roomId`, `portalId` or `socketId`). **Never a secret cache (agreed, exploration and information Q5; cross-document comparison 2026-11-23, CC-120)**, a carried notable, a boxed-lurker container, a Windfall, the Viaticum, the signature cache or an HE reservation, and never on F1; fact channels (Yield, a Curiosity gain) on F2–F5. Output: the room Traced if Unknown, the tier word and the qualifier "plain cache" or "behind risk" from the container's socket class, with the fixed line "says nothing about traps, lurkers, patrols or hazards". At most 2 instances per floor across all channels *(illustr.)*; an empty set reads "Nothing the string can find" and spends nothing. LE owns the selector (exploration and information design 4.6.1).
  - **Pending request (XI-23; M3):** a Curiosity option gain of a **Lodestring** (the run-scoped `party.lodestrings` counter, cap 1) as one of its sources; not adopted until the EV owner rules (exploration and information design 4.6.2).
  - **Pending request (XI-27; M2):** `SideRoomPayoffMix` (FL, LP and EV owners): at most 60% *(illustr.)* of side rooms pay loot only; the rest also pay a fact, a shortcut, XP or a pointer (exploration and information design 4.4.6).
- **Route.** Reveal an existing Hairline or a Latch's far side. A priced extra route to an existing lock (4.4.5). M3: skin an FL-generated Chute (EV-36).
- **Grace** (4.4.3). From Curiosity cards only, never Interludes.
- **Strand** (4.5.7). M3: a codex page beside the one rules gain. Later: a Pledge. **M4: a curse ended**, a Malady's Cast-off included (wave moved by HL-29; agreed, heirlooms and curses Q5; heirlooms and curses design 4.3.2).

#### 4.4.2 The FactLedger (change EV-8)
- One **per-floor, in-run** ledger shared by Yield interrogation, the elite intel line (LE-18), Curiosities, Fragments, Parting Words and the Tithe Bowl.
- **It records fact instances, not categories (lead's call, after the check):** `known[{category, targetId}]` plus the floor's `bossQueue[]`. Learning one room's Listen never blocks another room's.
- **`bossQueue` order per floor (BR-29; M2):** no repeats, information only: (1) the signature card (the elite intel line, LE-18, unchanged, first); (2) the Rendition fact; (3) the Guise fact; (4) the formation fact; (5) fixed Lore facts (Rhythm, Device rule, weakness). The Playbill is their home (boss rooms design 4.12).
- **Fall-through.** Each fact gain carries `fallThrough: [category…]`, by default its own category followed by F-ROOM, F-SECRET, F-TRAP. When the named target is already known, the option takes the next **unknown instance**: first other targets of the same category under the same selector (by main-path index, then room id), then the next category in the list. The preview shows which. If nothing is left, the option shows "Nothing left to learn" and is disabled (honest).
- **F-TREASURE in the ledger (XI-22; M3):** deduplicated with fall-through like every category; a later instance excludes sockets already pointed at this floor; the mark clears when the target container is opened. Its `TreasureTarget()` output for a paid or earned act is the one listed `KnowledgeLeak` exemption, limited to {room node, tier word, qualifier} (exploration and information design 4.6.1).
- **Targets are draw-free.** "The next unvisited room" is the lowest main-path index not yet entered (reading, XI-1: "entered" and "unvisited" mean the room's `entered` flag; Chalked or Traced never counts; exploration and information design 4.1.1); a referent comes from EventPlace (4.5.5); "of your choice" lets the player pick among unknown instances at resolve time.
- **The codex is write-only from the run's point of view.** Dedup never reads profile memory, so the same seed gives the same facts to a veteran and a new player (BC2). Flagged to the enemies owner (HX-6, TL-23). Segment 10 keeps this (reading, XI-14, XI-21): IP9 and `ProfileBlind`; its `uiMemory` profile field is display-only and the codex is never a HUD input (exploration and information design 4.5.1, 4.10).

#### 4.4.3 Graces (changes EV-11, EV-12; agreed, Q3: M2 only if the conditions owner rules EV-11 and the F1/F2 death-share gates pass, else M3)

**List (existing M2 ids only; one unit):**

| Grace | Built from | Fires | Floors |
|---|---|---|---|
| G-Ward | Warded, 1 charge | Its next harmful non-lingering condition | All |
| G-Help | Helped, 1 charge | Its next attack roll or check | All |
| G-Spur | Spurred | At its next combat start | All |
| G-Steady | Rallied | At its next combat start | F2+ (protects `Floor1BossSustain`) |

**Rules:**
- **Carrier.** A Grace is the unit's C5 lingering event boon, carried by a new duration form (EV-11, flagged to conditions 13 and 17), specified after the check **(lead's call, after the check)**:
  `eventBoon{payload, trigger: nextCombatStart | nextAttackOrCheck | nextCheck | nextHarm, endsOn: [fired, shortRest, floorExit, arenaSeal]}`. G-Help uses `nextAttackOrCheck`.
- **Order at the arena.** The arena seal is processed **before** the arena's combat start, so a Grace never fires in the arena: a G-Spur or G-Steady carried to the boss door expires there.
  - **The arena seal is `ArenaEngaged` (BR-3; M1/M2):** it fires once per floor, at the first of a party unit entering an arena square (doorway squares included) or the boss group noticing a party unit; order on the tick: Antechamber Catch-up (F1, M2+) → `ArenaEngaged` (Graces and event riders end) → Take Places → combat start. A re-engagement fires no new `ArenaEngaged`, but **every arena combat start while `sanctumEngaged` is set processes `endsOn: arenaSeal` again**, so a Grace or rider taken elsewhere on the floor between engagements still ends before the arena fight; Graces cannot be regained in the Sanctum. `arenaSeal` stays as the data id in `endsOn` (an alias); display never says "seal" (boss rooms design 4.2.1).
- **Target.** Each Grace option names `graceTarget: player | hero | worstStealth`; "player" lets the player pick the unit at resolve time, defaulting to a unit with an empty boon slot (KC4).
- **Replacement.** Per C5, a new boon **replaces** the old. The card names what it would replace ("replaces: <starting boon>").
- **Telemetry.** `GraceGranted{id, unit, replaced}` and `GraceEnded{id, reason: fired | shortRest | floorExit | arenaSeal | replaced}`.
- **Cap.** At most 1 Grace per party per floor. Once the cap is reached, a card's Grace option **converts** to its authored `graceFallbackGain` instead of greying out (FC13). The fallback prefers a route or a Strand wherever a fact would push the deck past its fallback `GainMix` ceiling; 5.3 prints every fallback as "[fb: …]".
- **Ends** at the first of `endsOn`. No exceptions in M2; segment 9 may declare some later (BC4). **Answered: no Grace exceptions in M2** (BR-3; boss rooms design 4.2.1).
- **Prices (restated after the check).** A Grace is **never free and never priced in ticks alone**, on a priced or a lit option. Its price includes at least one of: an offered item, noise of 4 or more bound to a forecast, HP, Weary, or a theme condition or carried rider; ticks may be added. On the rest-adjacent policy arm, `GraceValue` checks that Weary and HP costs are not paid for free by a planned rest (BC6). A Weary "kept through your next short rest" form is offered to the conditions owner as an option (EV-34). V16s `GracePriceNotFree` checks the whole rule.
- **Never** heals, prevents a drop (DH 15), adds actions or grants Cow advantage. HUD chip: "ends at the boss door".
- **Gates:** `EventDeathShareDelta` ≤ +0.5 pp on the **novice** arm, **on F1 at party 1, and on F2 at party 1 and party 2**, the only party sizes CMP 7.1 allows there (NoFloor1Companion; at most 2 on F2) **(lead's call, after the check:** the proposal's "party 1 and party 4" cannot occur on F1–2**)**. `Floor1BossSustain` is re-run.
- **If Graces wait for M3 (new lead's call).** The M2 cards still ship. Every Grace option shows its authored `graceFallbackGain` as its gain, at the same price, exactly as if the cap were reached; V16s re-runs `MechanicalDifference`, `LitIsSidegrade` and `GainMix` (at the fallback ceiling) on the fallback deck, and where two options would then share an effect set, the fallback is authored to differ (a different fact category or a route). No card is cut for want of Graces. **After the check** every fallback is authored in 5.3, and each M2 pool's fallback deck meets the 50% ceiling (5.2).

#### 4.4.4 Costs (change EV-9)

| Cost | Rule | F1 |
|---|---|---|
| Ticks | 2–8. The chip names the patrol that moves; where it goes is drawn on the map (bound cost, FC3). Never the only price of a Grace | Yes |
| Noise | **Here only**, at the card's square. Generic ENM 12.3 noise resolved by the hearing rules; joining caps hold. Plays only the generic noise cue (6). Never a Lure and never sets the group flag `luredThisFloor` | ≤ 4; `F1NoiseClearOfCircuit`: the printed radius may not reach a Circuit square |
| HP | **The hero pays by default.** Companions pay only on options that print "any member". **`PaymentFeasibility`:** payable only by a unit whose current HP exceeds the cost. DH 10.4's floor still covers incidental damage (BC3) | ≤ 2 (hero) |
| Weary 1 | Lingering. Unavailable if the cap of 2 would block it. Universal use needs EV-34 | One card per F1 theme |
| Theme condition | Lingering Poisoned (Silk, Supper, Gut) or the theme's own riders (4.4.6). Only from the theme's declared `harmfulConditions[]`. Never Scarred | No |
| Item offered | The player picks any unequipped carried item meeting a printed family ("a scroll", "any consumable"). Never healing items, the HE-3/4 items, the Due, quest items, curses or **heirlooms** (an heirloom is never a price; HL-4, M3; heirlooms and curses design 4.1.6). New reason **`ItemDestroyed{offered}`**, not LE-4's `dreamedAway` (KC21) | Non-healing consumable only |
| Malady (**pending request, HL-19; M4**) | Not adopted until the EV owner rules (agreed, heirlooms and curses Q4; heirlooms and curses design 4.3.3): F2–F4 only; a named Malady drawn StateBlind from `event/{floor}/{site}/malady`, its rule and Cast-off deed printed; **the hero pays by default**, a companion only on an option that prints "any member" (priced higher in `curiosityValueTable`), the cost row carrying `payer`; payable only by a unit with no Malady for which the preset is eligible, else greyed with its reason; never on Leave or the free option; never stacked with a Proviso on F2; `registerLock: sincere`; F2 Malady costs inside the `EventDeathShareDelta` gate, else they start on F3 | No |
| Supply | **Not used in M2.** Options priced in supply were dead (below 50‰) and moved healing supply into buffs (NF8) | No |
| Coin | Never. Coin owed at camp is held, off (14.1) | No |

#### 4.4.5 Loot touches (change EV-15)
- **`CuriosityLootBlind`:** a card may frame its room's existing container with wall or backdrop dressing (a decal, a cloth or a plate around it, never a Drape skin), never by reshaping it: the container keeps its standard silhouette and its LP CT2 `ContainerArtBlind` art **(lead's call, after the check;** the proposal's "the offering bowl *is* the chest" would have changed a loot silhouette**)**. That container opens with no check whatever is chosen, and its contents are never an option outcome. V16s `ReservedSilhouette` checks it.
- **Priced extra route:** a card may open an existing **ordinary** locked container early, only if the lock carries no value step. This needs `stepFunded = false`, a LootBind flag requested from 7c. The lock must also not be HE, the Due or a boxed lurker.
  - The price is never ticks alone.
  - The contents are unchanged. This adds a route under CC4 (BC9).
- **No priced access to a socket that is otherwise free:** that is a tax (Model C's contest, adopted).

#### 4.4.6 Carried riders (M2 only if EV-11 is ruled; else M3)
- A cost may start the hero's next fight Soaked (Choir), Oiled (Carnival) or Chilled (Supper).
- **Carrier (lead's call, after the check).** A rider rides in its own `eventRider{condition, trigger: nextCombatStart, endsOn: [fired, shortRest, floorExit, arenaSeal]}` carrier, **outside** the C5 lingering boon slot, so taking a rider cost never replaces the hero's starting boon or a Grace. When it fires, it is a harmful lingering condition like any other, under C14's cap. Part of the EV-11 request.
- Under a pipeline step 4 exemption, a cost-sourced instance is not blocked by Warded (EV-11).
- **If EV-11 is not ruled for M2 (new lead's call),** each rider cost falls back as the catalogue states: the Choir's C2 is priced hero 3 HP; the Carnival's Ca4 moves to M3 and the Carnival ships one MECH card (Ca2) in M2, its pool checked by `PoolCoverage` as usual (band pools carry no parity rule); the Supper's rider card Su1 is M3 already.

#### 4.4.7 StateBlind and payment feasibility (change EV-10; agreed, Q4)
- **(a) Value predicates** (which card is drawn, which options appear, what they give) may read only: floor, theme, slot, the run's prior draws, Strands, party skills (for lit display), carried item families, the last floor's theme, unlock-snapshot data, **the floor's FactLedger** (for fall-through and "Nothing left to learn") and **Graces granted this floor** (for the cap and conversion) **(lead's call, after the check)**. **Never** HP, downs, deaths, profile wins or losses, streaks or dry spells.
- **(b) Payment feasibility** (HP above the cost, the item held, the condition cap) is allowed and shown, following the existing supply = 0 precedent.
- V16s `StateBlind` checks (a); `PaymentFeasibility` whitelists (b).
- **Q4 (agreed):** this is the whole of segment 8's answer to struggling players. Losing streaks get novelty and memory relief only (4.5); never easier odds, cheaper prices or richer outcomes. Visible opt-in streak help through events was rejected (Q4-B); difficulty help belongs in the visible ladder.

#### 4.4.8 The Never list (V16s `PaletteWhitelist`)
- No VP, notables, rarity steps, offer widths, sockets or redraws.
- No experience. No coin.
- No healing. No units, and nothing before camp 1.
- No cross-run power. No hidden outcomes.
- No clock Landmark. No graph edge. No change to terrain squares.
- No draw, option set or gain that reads losses, streaks, HP, downs or deaths (agreed, Q4); payment feasibility and display-only lines excepted (4.4.7, 4.5.10).

#### 4.4.9 The value table (lead's call, after the check)
- Segment 8 keeps one config table, **`curiosityValueTable`** *(illustr.)*, giving every gain kind (each fact category, a route, each Grace, a Strand) and every cost kind and amount (ticks, noise, HP, Weary, an item family, a condition or rider) a value in hero-HP equivalents (HPe) from `dh.py`, grouped into bands.
- Every value check reads only this table: `LitNoExtraGain` (the lit gain's band is at most the priced option's), `DeckValueParity` (each option's net band within one band of its deck's median), `SuccessorParity`, `StrandValue` (a Strand's payoff band at most a fact's) and KR12 (a rare's options inside its deck's bands).
- The table is builder-owned tuning (16); the simulator's `GraceValue` arm calibrates it.

### 4.5 Anti-repetition in full

| Id | Device | Seed effect | Wave |
|---|---|---|---|
| DV1 | No repeat within a run; prop-family cap 1 per floor; a chassis at most twice per run and never on consecutive floors; 1 noise card per floor (4.5.11) | Inside the seed | M2 |
| DV2 | **Premise tag cap:** at most 1 `selfReference` card per run at the draw | Inside the seed | M2 |
| DV3 | Shared share capped by pool size; shared Interlude caps | Inside the seed | M2 |
| DV4 | **Twice-Told card**, delta first | Display only | M2 (agreed, Q1) |
| DV5 | **First Telling** diorama | Display only | M2 |
| DV6 | **Retellings**, R2 then R3 | Display only, outside the key | M2 / M3 (agreed, Q1) |
| DV7 | **Launcher Fallow** on the F1 Curiosity | Outside the seed | M2 (agreed, Q1) |
| DV8 | **Referents** on MECH and other tied cards, and bound costs | Inside the seed | M2 |
| DV9 | **Strands** cashed on entering the next floor | Inside the run (committed state) | M2 lean / M3 |
| DV10 | Rare tier at about 30‰ | Inside the seed (authored weight) | M2 / M3 |
| DV11 | **Afterword** lines and chain beats | Display only | M2 lean / M3 |
| DV12 | **Sequels** advancing on sight | Unlock snapshot (flagged reading) | M3 (agreed, Q1) |
| DV13 | **Ripening** under SuccessorParity | Unlock snapshot (flagged reading) | M3-late / M4 (agreed, Q1) |
| DV14 | Glimpse rotation: the least-seen variant on the display stream (the `display` key's `glimpse` domain; SV-2; seeds and validation design 4.3); text grammar tokens | Display only | M2 / M3 |

#### 4.5.1 What counts as "seen"
A card counts as seen when it is **displayed** (the card opens; `CuriosityShown`). Drawn-but-unreached cards never advance memory (FC15).
- **Seen is written at display (lead's call, after the check).** `CuriosityShown` is written to profile memory when the card first opens, **independent of resolution**, idempotent per (runId, curiosityId). A card that is displayed and then Left, closed, or cut off by a crash or quit has still been seen; Fallow and Retellings advance all the same, so closing a card cannot re-roll its presentation.
- Reopening a Left card in the same run is not a new sighting (4.3.1).
- The resolve safe point (4.10) commits the cost, the gain and the `last[id]` record, not the sighting.

#### 4.5.2 The presentation sequence and the Twice-Told card (change EV-17; agreed, Q1)

**The presentation sequence (lead's call, after the check).** Every display decision reads the **display snapshot** of `curiosityMemory` taken at run start, so reopening a card mid-run never changes it. For a text card with n Retellings (2 in M2, 3 in M3), `fullShown[id]` records which Retellings have been displayed in full:

| Meeting | What the player sees |
|---|---|
| R1 not yet shown in full | **First Telling:** R1 in full, with R1's diorama in the First Telling treatment |
| Some Rk not yet shown in full (lowest such k ≤ n) | **Rk in full,** with its diorama |
| Every Retelling shown in full | **The Twice-Told card:** body = Rn's Twice-Told line, a delta line, a footer; "Read in full" opens Rn |
| A clock rule forces the format (4.7: the F3 heavy arm, cut step 2; the M3 F4 Curiosity) | The Twice-Told format with the would-be Rk's Twice-Told line. It does not mark Rk as shown in full, so the full display waits for the next meeting. On a first sight, the delta line reads "First time here" and there is no footer **(new lead's call)** |

So a second meeting shows R2 in full, as the toy assumed when it counted R2 as fresh; the Twice-Told card starts once every Retelling has been read. MECH cards and Interludes have one presentation and no Retellings (KR9): they show their one line every time.

**The Twice-Told card:**
- **Top line: what is different this time**, generated from rules data. At most one line is shown, in this priority **(lead's call, after the check)**:
  1. a price now unaffordable (payment feasibility);
  2. a lit option newly met or newly missed;
  3. the option's referent or seeded target differs from last time ("the hatch now opens onto the guard post");
  4. what a Grace option would replace now differs from last time ("replaces: your Spur");
  5. a Strand-setting option now disabled (2 Strands carried).
- **An authored delta source on every standard F1 card.** Each has at least one option whose target is a referent or a seeded target (4.4.2), or a Grace option, so a delta of kind 3 or 4 is usually available; V16s `F1DeltaSource` checks it **(lead's call, after the check)**.
- **Footer:** "Last time: you signed." It is pre-highlighted only when no delta exists and **never pre-committed**.
- **Auto-answer** is opt-in per card, only for no-roll information options, and is suppressed whenever a delta exists.
- Every path issues the same core command (MAIN 2 anti-goal). "Read in full" and skip are always available.
- **Punchlines.** Twice-Told lines never carry a punchline. A full body's punchline is displayed in full at most once per profile, except through "Read in full" (4.11).

#### 4.5.3 Retellings (change EV-18; agreed, Q1)
- The variant shown follows the presentation sequence of 4.5.2, read from the run-start display snapshot **(lead's call, after the check:** replaces "min(seen count + 1, n)"**)**. **Retellings are outside the reproducibility key**, because text is not a rule (told plainly against Model A, Model C and the first catalogue, which put this in the unlock snapshot).
- n = 2 in M2 and 3 in M3 **(agreed, Q1)**.
- Shared and daily seeds may show any Retelling: the rules are identical.
- **`RetellingParity`:** one rules hash across variants.
- **`DisplayMemoryInert`:** wiping or randomising memory, the codex included, gives the same core hash.

#### 4.5.4 Launcher Fallow (change EV-20 ⚑; agreed, Q1)
- **Forecast.** The F1 Curiosity's identity is fixed by the **first draw** on F1's `curiosities` child (the draw map, 4.10), filtered only by manifest, unlock snapshot, theme, floor, wave and rarity: `F1MajorForecastable`, part of `EventDrawRunBlind`. No Sequel draw touches F1 (4.5.8). **Every F1-eligible card is host-agnostic**, authored to fit an Alcove, a side room or a wall. The host decides presence only, and the referent never changes identity: where no referent exists, only the fallback gain differs (4.5.5) (BC6; KC8). The forecast ignores host presence: a forecast card whose floor turns out to have no host still counts **(lead's call, after the check)**. **The forecast is exact (SV-4, read; SV-30; M2):** `curiosities` is attempt-invariant, so an F1 floor retry never moves it, and V16s `F1HostAlways` (9.2) gives every F1 Blueprint a legal host for every F1 Curiosity pool, so a retried F1 keeps its forecast Curiosity (V19t `ForecastSurvivesRetry`; seeds and validation design 4.3, 5.2). Under `F1HostAlways`, `F1CuriosityPresence` is 1000‰ and a shortfall is a defect (SV-30) (cross-document comparison 2026-11-23, CC-117).
- **Rule.** The launcher, not the rules core, redraws an OS-entropy seed when the forecast F1 Curiosity is in the profile's **Fallow set** (cards displayed in the last **K = 3** personal runs **(agreed, Q1)**; `recentRuns`, 9.1).
  - It shares R-SEED-6's budget of at most 8 redraws.
  - **Order per candidate (lead's call, after the check):** (1) the R-SEED-6 theme rule; (2) Fallow; (3) the onboarding preference, which only scores candidates and never forces a redraw that Fallow does not need.
  - **Segment 9's F1-boss streak rule (BR-25; agreed, boss rooms Q1; M2)** sits between Fallow and onboarding: (theme, Fallow, boss rule, onboarding), inside the same budget of 8, and in the same place in the exhaustion tie-break (theme, Fallow, boss rule, onboarding, earliest) (boss rooms design 4.10.3). RS owner applies.
  - **On exhaustion** the launcher keeps the best candidate by (passes the theme rule, passes Fallow, passes the F1-boss rule (M2), passes onboarding, earliest) (boss rule added, SV-31). If no candidate passed the theme rule, it keeps the original seed; R-SEED-6 states no rule for that case, so this reading goes to the RS owner with EV-20.
  - Bypassed for shared seeds, daily seeds and the simulator.
  - It applies to the **F1 Curiosity only** **(agreed, Q1)**; no other floor's slot is forecast or redrawn.
- **Rares are not exempt.** A rare is fresh if and only if it is not in the Fallow set. An exemption would raise the rare share about 1.5× (BC6).
- **Onboarding.** On a profile's first 2 runs, the launcher prefers a seed whose F1 Curiosity is MECH or ≤ 25 words *(lead's call; illustr.)*. This is a presentation preference inside the same budget, not a gate on base pools (FC14).
- **Guards:**
  - `LauncherLootNeutral`: over 10⁵ accepted seeds, the spoils, route and theme distributions match a no-Fallow arm;
  - `Floor1ThemeShare`: each drawable F1 theme within 1/n ± 5 points, n the number of drawable F1 themes (50% with the Gaol and the Kennel, 33% with the Wake; more themes in M3) **(lead's call, after the check;** the toy measured only n = 2**)**;
  - `F1PoolParity`: every F1 theme pool in the manifest has the same standard / MECH / rare counts (6 / 2 / 1 in M2), so the Wake's promotion never thins one theme's freshness (agreed, Q2);
  - `LauncherRedrawRate` ≤ 0.5 per run (measured 0.10–0.15);
  - `LauncherCheckMs`;
  - `LauncherF1Neutral` (new, REL; SV-13; seeds and validation design 6.10; M2).
- This also pulls RS 12's M3 row "profile-side anti-streak outside seeded runs" into M2 (flagged).
- **Rejected (Q1):** a least-seen or seeded Fallow draw inside the key (Q1-B), and no memory at all (Q1-C).

#### 4.5.5 Referents and bound costs
A MECH card, and any option marked as tied, is tied at generation to a named **referent** on this floor (a lock, room, Latch, Hairline, or leader or patrol group). Tick costs name the patrol that moves. The board highlights the referent while the card is open, and the full map shows the facts gained (exploration and information design 5) (cross-document comparison 2026-11-23, CC-118). The same card is then a different decision on different seeds (FC3, Model C).
- **Choosing the referent.** EventPlace chooses it draw-free, by the tie-break of 4.2 (lowest main-path index, then lowest room id), among the referents of the option's `referentKinds[]` legal on this floor (for an unstepped lock: `stepFunded = false` from LootBind).
- **No referent (lead's call, after the check).** EventDraw is host-blind and loot-blind, so a drawn card can find no legal referent. Every referent kind carries a `referentFallbackGain` (V16s `ReferentFallbackPresent`); EventPlace then gives the option that gain instead and logs `CuriosityReferentFallback`. The card's identity and presence never change, so the F1 forecast holds. V16g `ReferentResolved` checks that every placed tied option has a referent or its fallback; `ReferentAvailability` reports, per Blueprint, how often each kind falls back (9.3).

**`ReflexTakeRate`** replaces the MECH exemption from OptionTakeRate. A MECH card taken above 950‰ on the streak arm gets a real bound cost, or (information gains only) becomes a 0-click map mark.

#### 4.5.6 Premise tags (change EV-25)
- Every card carries one tag: `selfReference`, `lostObject`, `ritual`, `mechanism`, `residentWant` or `place`.
- `selfReference` cards ("your name in the ledger") are capped at **1 per theme pool, 2 in Shallows, and 1 per run** at the draw.
- The rest are written around the residents' Obsessions. This guards RS 7.5 and RS risk 14, and keeps the uncanny beat rare (FC4).

#### 4.5.7 Strands (change EV-22)
- An option may set one Strand. At most 2 are carried at a time, and at most 1 is cashed per floor. While 2 are carried, every option that would set a Strand is disabled under payment feasibility, with the reason shown.
- **Where a Strand cashes (lead's call, after the check).** The proposal cashed Strands as overlays on the next Forecourt Interlude. The Forecourt is main-path room n−2 (FL LG1), so a fact about the floor arrived just before the arena, about rooms already passed, and often fell through to "Nothing left to learn". Strands now cash in one of two places, recorded as `cashAt`:
  - **`floorEntry` (the default):** on entering the next floor, as a 0-click line on the floor-entry display, with the fact drawn on the map at once. This was already D1's rule (KC13). It adds no stop and no click.
  - **`coatHooks`:** on F5 at the Heart's **Coat Hooks by the Door**, for payoffs about the final arena (F-BOSS, arena-path F-TRAP). The Coat Hooks **replace** the drawn F5 Interlude whenever a Strand can be cashed there, and are never shown beside it.
- **Generation stays run-blind** (KC12, BC12): the cash reads committed run state at floor entry or at offer time, and picks its target draw-free (4.4.2).
- **Order.** When two Strands could cash, the oldest cashes first; the other waits for the next cash point. A `floorEntry` cash counts toward the 1-per-floor cap.
- **Every F1 Strand pays off on entering F2** (FC6b).
- The setup option prints "pays later, if you get there". An unpaid Strand gets a closure line on the next run-start card ("The letter stayed sealed."; 4.5.10).
- Payoffs are facts or routes within the band (`StrandValue`, read from `curiosityValueTable`). Never loot or units.
- Report `StrandCashRate` ≥ 400‰ on the streak arm *(illustr.)*, counting only cashes whose fact was not already known (`StrandCashed{fellThrough}`).
- **M2 lean and M3 (new lead's call).** M2 ships every catalogued Strand option with a single named payoff, cashed at floor entry or at the Coat Hooks, plus the one closure line. M3 completes the device: camp display of carried Strands (knotted cords over the camp fire; camp and hub design 4.7), keeper lines that name them (the camp keeper's only M3 lines; CH-14, camp and hub design 5.1), and Strands named by Sequel stages. In M2 each F1 theme's **pilot Strand** (the Gaol's *Signed In*, the Kennel's *Collared*, the Wake's *In Mourning*) carries the `StrandCashRate` gate; every other Strand reports only. After the check, each pilot Strand sits on a priced option facing a priced lit option, so no free alternative beats it.
- **Default payoffs (new lead's call; illustr.).** Strands the proposal listed without a payoff take the payoff named in the catalogue rows of 5.3 (each marked there), so every Strand names its payoff (KR10).
- **Cuts never strand a payoff.** Floor-entry cashes are untouched by every step of the clock cut order, and the Coat Hooks survive cut step 1 (4.7; V16t `StrandSurvivesCut`) **(lead's call, after the check:** replaces the proposal-era overlay-only Interludes, which floor-entry cashing makes unnecessary**)**.

#### 4.5.8 Sequels (M3; agreed, Q1; change EV-27, a flagged reading of RS 9.1)
- **Structure.** 2–4 stages. Stage k+1 enters `drawableSet` through the unlock snapshot once stage k has been **seen**, win or lose.
- **Reaching the player.**
  - A due stage gets a dedicated seed-pure rarity draw on the **F2** Curiosity slot only, at ≤ 200‰ *(illustr.)* (FC6c). It never touches F1, so the F1 forecast stays draw 1 alone and Fallow holds in M3 **(lead's call, after the check;** the proposal allowed the F1 slot**)**.
  - Its *text* beat also advances on the run-start card each run after a stage is seen, display only (FC6a).
- **Ending.** The last stage gives a codex page and a cosmetic mark.
- **Limits.**
  - `SuccessorParity`.
  - Residents and Obsessions only; endings are images, never premise reveals.
  - One tone review per chain.
  - Shared, daily and simulator seeds stay canonical (stage 1).
- **First chain:** the Night Usher.

#### 4.5.9 Ripening (M3-late or M4; agreed, Q1; change EV-28, flagged reading)
- After about 4 sightings *(illustr.)*, a card is replaced from the next run by an authored successor that inherits its permutation slot.
- **`SuccessorParity`:** the same option shape and an equal-or-lower value band. Variety comes from swapping a cost type at equal value, never from added gains.
- This closes the abandon-farming route to cross-run power (BC1). `SightingsPerRunMinute` watches for farming anyway.

#### 4.5.10 Afterword and memorials (change EV-26)
- **M2 lean:** one run-start line keyed to how the last **personal** run ended, plus the Strand closure line (4.5.7). Example: "The Kennel remembers your scent." **(new lead's call:** M2 lean is exactly these two line kinds; camp-keeper lines and chain beats are M3**)**. **Reading (camp and hub design 13, CH-14; RS and EV owners):** the camp keeper's M2 strings are service strings (UI text), not keeper lines (camp and hub design 5.1).
- **Selection (lead's call, after the check).**
  - After a death: the first line, in authored priority, not yet seen on this profile, whose key matches the Grave Goods record (floor, theme, killer category).
  - After a win or an abandon: the first unseen line keyed to that outcome, or no keyed line.
  - When the keyed lines for a key are spent: a line from a **generic tier** of theme-neutral lines, which may recur once it has not been shown for K = 3 personal runs.
  - The run-start card shows **at most 1 Afterword line and at most 1 closure line** (for the oldest pending Strand). A shown closure clears its `pendingClosure` entry.
  - Never silence by exhaustion: the fallback order is the keyed line, then the closure line, then a generic line.
- **Budget (illustr.).** At least 3 keyed lines per (F1–F2 theme × killer category) and at least 12 generic lines: about 60–90 short lines, about 0.7k words, each through tone review (pillar 2). Report `AfterwordExhaustion`: on the streak arm, 10 runs never reach a silent run-start card and use no generic line before run 6 *(illustr.)*. This keeps 0.1's "losing moves the dream" true of M2 as budgeted.
- **M3:** camp-keeper lines (Strand lines only: lines that name carried Strands, never a lore chain; CH-14, camp and hub design 5.1), Sequel chain beats, plus the **Unwoken Roll** as the single fallen-hero device:
  - at most once per run, only after the profile's first death;
  - the last 3 names plus "and others";
  - marks, never a count;
  - forward-looking, never cumulative;
  - a setting to hide memorials (FC11).
  - **Hub home (CH-14; M3):** the Roll is a hub object at the Stairtop with the **Sitter**, who owns the hub death reaction as the Roll's caption, from a small sincere pool; every limit above is kept, and no figure names the fallen (camp and hub design 4.10, 5.1). The Sitter's selector calls this section's Afterword selector and excludes that line's key family (`AfterwordNoEcho`; camp and hub design 4.15.6).
- **The Afterword stays on the run-start card, unchanged** (CH-14; `AfterwordExhaustion` untouched); at most one figure reacts to a death per Night: none in M2, the Sitter in M3 (camp and hub design 4.15.6).
- Never humour on death (pillar 2). Never the Dreamer.
- Afterword lines never change what is drawn or offered (agreed, Q4).

#### 4.5.11 Within-run caps defined (change EV-16; lead's call, after the check)
- **Prop family.** Each record names a `propFamily` (an authored grouping of like props: ledgers, clocks, mirrors, letters, coats…). At most 1 per floor across the Curiosity and the Interlude.
- **Chassis.** The rules chassis shared by a card's Retellings; at most twice per run and never on consecutive floors.
- **Noise card.** A card with any option carrying a noise cost; at most 1 per floor.
- **What the caps count.** Every cap, and the no-repeat rule, counts **EventDraw results**, whether or not EventPlace finds a host, so generation never depends on hosts.
- **Exhaustion.** When the caps empty a pool, EventDraw applies the fallback order of 4.10 (the other rarity class, then the other pool), then leaves the slot empty with `CuriosityAbsent{reason: capsExhausted}`.

### 4.6 Floor 1 (change EV-3)
- **Host:** per 4.2. One Curiosity, optional. Glimpses only in Q room 3 and on the Forecourt walls.
- **Costs:** ticks; noise ≤ 4, clear of the Circuit; hero HP ≤ 2; Weary 1 (one card per theme); a non-healing consumable offered.
- **Gains:** facts, routes, Strands, and G-Ward, G-Help and G-Spur (gated, agreed, Q3).
- **Never on F1:** supply, theme conditions, riders, curses, rolled options, companions or Pledges, hazards, Landmark ticks.
- **Lesson axis** (RS 8.2(f)): the Gaol teaches doors, noise and keys; the Kennel teaches the leader, morale and positioning; the Wake teaches tells.
- **Comic register,** with punchlines in Retellings and Glimpse art.
- **Nightmare tier 2+ (M3)** may add a cost, never a gain.
- **Gate:** F1 hero deaths stay under 5%. `EventDeathShareDelta` (F1, party 1) ≤ +0.5 pp on the novice arm. `Floor1BossSustain` is unchanged.
- **Floor-1 variety in parallel (agreed, Q2).** The Endless Wake joins the M2 F1 pool when its RS 8.2 gate passes, at `F1PoolParity`; its catalogue (5.3) is authored to parity in M2 so promotion costs no event work late. F1 Blueprint and boss variety are a production priority flagged to the RS, FL and segment 9 owners (EV-44); segment 8 changes none of their text. **Told plainly after the check:** RS 12's pre-authorised Cut 1 **(agreed, RS Q4)** drops the extra bosses of the Gaol and the Kennel first, which is floor-1 boss variety. If the schedule slips, that cut still applies first unless the client reorders the RS cut lines, and any reorder is an RS change that returns to the client (14.1, 14.2). The client did not reorder the cut lines when it approved this document: the F1-boss cut-order question is carried to segment 9 (boss rooms) as a future item for the client, with no RS change now (14.2). **Answered (agreed, boss rooms Q1, 2026-11-22; BR-26, RS owner applies; M2):** Cut 1 is graded (1a, 1b, Cut 2 with Cut 2R, 1c client-only; boss rooms design 4.10.4), so the question is closed (cross-document comparison 2026-11-23, CC-116).

### 4.7 Clock and reading load (change EV-23)

**Toy costs:**

| Form | First Telling | Repeat |
|---|---|---|
| Curiosity | 25–40 s, 2 clicks (+10 s side-room detour) | each further Retelling's first full read: as a First Telling; after that about 6 s, 1–2 clicks |
| MECH | about 9 s | about 4 s |
| Interlude | about 9 s | about 3 s |
| Glimpse | 0 | 0 |

**Lines** (measured like LP-10 on `FloorTimeByTheme`, **on a first-10-runs arm**, because playtesters are first-sight readers):

| Floor | Allowance per floor |
|---|---|
| F1–2 | ≤ +0.5 min and ≤ +4 clicks |
| F3, light and median arms | ≤ +0.5 min and ≤ +4 clicks |
| F3, heavy arm | The Curiosity uses the Twice-Told format on first sight (about 11 s with the Interlude; 4.5.2) |
| F4–5 at party 4 | **Net ≤ 0** and ≤ +1 click. Interludes only from **substitutive** families that replace a Listen or Sound at the arena door: F-LISTEN on the arena, arena-path F-TRAP, arena F-ROOM. F-BOSS is not substitutive and is not offered by F4–5 Interludes; it reaches F5 only through a Strand cashed at the Coat Hooks, which replace the drawn Interlude and add no stop. Each F4–5 row in 5.3 is marked "substitutive" or "exempt (reason)" **(lead's call, after the check)**. A measured net is required before any ships |

**Binding cut order.** Segment 8 cuts itself before any CMP 7.4 lever:
1. F4–5 Interludes become Glimpses.
2. The F3 Curiosity uses the Twice-Told format on first sight on every arm.
3. The F2 Forecourt Interlude becomes a Glimpse.
4. M3 F4 Curiosities are dropped.

Floor-entry Strand cashes are untouched by every step, and the Coat Hooks survive step 1 (4.5.7). Dream Fragments **replace** F-BOSS options; they never add stops (EV-29).

**Told plainly after the check: the repeat cost.** The toy priced every repeat at about 6 s, the Twice-Told card. Under the presentation sequence (4.5.2) a second meeting reads R2 in full, so each card costs one more full read per profile before its repeats get cheap. On the first-10-runs arm, where most meetings are first sights anyway, the lead judges the F1–2 lines still hold (a full R2 read costs what a first sight costs, and the runs 1–3 column already sits at the line), but the toy was not re-run and the simulator must confirm it.

### 4.8 Class, companion and captive interplay

- **Herald:**
  - Intimidation and Medicine lit options at creation defaults; Leadership (hero) where taken.
  - **No Cow-advantage Grace.** Show of Force stays the Herald's slot spell. A Herald-lit variant ("your Show of Force costs no slot this floor") is offered to the Herald owner for M3 (EV-13).
  - Curiosities never offer cow, spare or interrogate menus.
- **Shade:**
  - Stealth (movement: worst member; single-actor manipulation: the acting unit), Perception and Medicine lit options at creation defaults; Mechanics and Acrobatics where taken.
  - **No furniture Lures.** Lure is a universal exploration action (SH NM-4, pending SH-21) and `luredThisFloor` is a per-group flag. Curiosity noise is never a Lure, because furniture would duplicate a universal 1-tick verb at no cost (BC7). The proposal's Shade-lit "Lure from here at no tick cost" is **dropped**: it was a class-locked discount on a universal verb **(lead's call, after the check)** (EV-14).
- **Arcanist:**
  - Lore reads inscriptions, and Medicine lit options suit its creation defaults; Arcana reads runes and shrines where taken.
  - M3 Readings are a second Q's content (EV-30). Soaked and Oiled riders pay off its element kit (M3).
- **Vanguard:** Athletics and Medicine lit options at creation defaults; Intimidation where taken.
- **Companions:**
  - They count for party-best thresholds and lit display (CC8). They never pay HP unless the option prints "any member".
  - They are never a cost. Nothing joins mid-floor or before camp 1.
  - A fallen companion's name may appear in Retelling and Glimpse text only. It is never a selection Sign (BC12).
- **Captives and wanderers:**
  - Later only, as Pledges (CMP 7.12, EP-39). Segment 8 hosts the wanderer and shrine sources; the Night Usher is the wanderer (EV-41).
  - A freed captive may offer **Testimony** (one boss fact through the FactLedger) beside Meet and Send home. Flagged to CMP.
  - Model C's M3 Parting-Word Captive is declined. Pledges stay one Later package (told plainly).

### 4.9 M3 meta: codex, Fragments, Readings, Omens, Nightmare

- **Codex (EV-37):** segment 8 co-owns "Curiosities" pages: Retellings collected and options seen, with the exact outcomes already printed on the card. Never power, never gating rule text. Write-only from the run's view. Proposed: RS owns the codex and segment 10 hosts it. **Answered by segment 10 (XI-29; M3):** segment 10 hosts the codex, RS owns it (exploration and information design 4.13, 7).
- **Dream Fragments (EV-29):** folded into F-BOSS options on Lore thresholds. 0 VP and 0 coin. One boss-fact queue in the FactLedger. Recovers most of the 0.5–1 min per floor.
- **Readings (EV-30):** TL-34 accepted. One Reading per room, a no-roll threshold, a floor fact. The second F3 Q room's M3 use.
- **Omens and Moods (EV-31):** an Omen floor may swap a card for its Omen Retelling (kind, not stats). A cost may be the named, visible event that advances a Mood, within HZ 6.9's cap.
- **Nightmare tiers** (read field: the profile's `nightmareTier`, M3; it drives kind additions only and never changes ladder%, which `difficulty.tier` drives; RS 11.1, MAIN 4.7 (decided)) (cross-document comparison 2026-11-23, CC-84) add **Provisos:** extra printed costs, never better gains ("harder is not richer", LE 6.12). Never on the free option. Ticks or noise only on F1.
- **Overtures (EV-32):** a priced option may apply Soaked, Oiled or Revealed to the tied group at its fight's first round.
  - Never on the elite, the Annex, the boss, or a lurker host (for Revealed).
  - F1: Revealed only.
  - At most 1 per floor. Priced in noise plus an item. Enemies owner.
- **Easing (EV-33):** a priced Weary-relief or rest-tick service. Never HP. Banned on F5 and in pre-arena Forecourts. Counted in `HealSupplyPerFloor` (the HZ-30 path). In conditions terms it is a `cleanse`-tagged source (DH and conditions owners). Easing is priced like any option and never reads streaks (agreed, Q4).

### 4.10 Determinism (change EV-21)

- **Stream:** `curiosities`, a keyed child of each floor's dungeon substream (segment 11 assigns it). A fixed **12 draws per floor** *(illustr.)*, with dummy draws (R-SEED-3). **Assigned (SV-2, SV-4; M2):** `dungeon/floor/{N}/curiosities`, count class `vector` of length 12, read by absolute index (0-based: index 0 is draw 1 below), the length locked before the M2 goldens; **attempt-invariant**, so a floor retry never moves it; the launcher reads F1 index 0 (seeds and validation design 4.2, 4.3).
- **The draw map (new lead's call).** The proposal's "fixed 12 draws" and its "whole-catalogue Fisher–Yates permutation" are reconciled: a slot consumes **one** draw, its slot sub-seed, and the rarity roll and the whole-catalogue permutation are derived from that sub-seed by keyed hash, draw-free. The draws are taken in a fixed order on every floor, whatever the floor hosts:

  | Draw | Use |
  |---|---|
  | 1 | The Curiosity slot's sub-seed (rarity, pool and permutation). On F1 this alone fixes the forecast (4.5.4) |
  | 2 | The Interlude slot's sub-seed |
  | 3 | The Sequel stage rarity draw, used on F2 only (M3; a dummy in M2 and on every other floor) |
  | 4 | The F3 second-Q Reading sub-seed (M3; a dummy in M2) |
  | 5–12 | Reserved, consumed as dummies |

  A floor with no slot of a kind still consumes its draw.
- **EventDraw** (order inside one slot specified after the check; lead's call, after the check):
  - runs right after layout, before hazards, so no earlier pass reads its result;
  - for each slot, with `sub` the slot's draw and H segment 11's keyed hash (`H` and `Bound` of seeds and validation design 4.1; SV-1):
    1. `rar = Bound(H(sub, "rarity"), 1000)` (SV-1: `mod` on a raw draw is banned); the slot seeks a rare if `rar < curiosity.rarePermille` (about 30 *(illustr.)*), else a standard card;
    2. `pool = Bound(H(sub, "pool"), 1000)` (SV-1); the slot seeks the shared pool if `pool <` the slot's capped shared share (4.2), else the theme pool;
    3. `perm` = the whole catalogue in keyed-sort order, by (`H(sub, "perm", LabelId(id))`, then the ordinal comparison of the id) (SV-8; seeds and validation design 4.1 K-rule 7), locked entries included;
    4. the first entry of `perm` that passes every filter wins: form, pool, rarity class, theme, floor, wave, drawable, the run's prior draws (no repeat; prop-family, chassis, noise and premise caps; 4.5.11) and F1 rules;
    5. if none passes: the other rarity class, then the other pool (with each rarity class), then the slot is empty with `CuriosityAbsent{slot, reason}`;
  - the label strings `"rarity"`, `"pool"` and `"perm"` are fixed and listed in the segment 11 draw table (kept as written; their parent, the slot sub-key, has no stream children; seeds and validation design 4.1 K-rule 6);
  - reads **no host, no loot, no party, no run choices and no profile memory** (`EventDrawRunBlind`); its one player input is the committed theme, read as a `GenInputs` field (SV-30; seeds and validation design 4.8.2; M2).
- **Pass order** (aligned after the check with LP 2.1 and LE 6.10 item 4, the approved order):
  ```
  layout → EventDraw → hazards → traps → ENM 10.5 step 2 → LootPlan
  → (ENM 10.5 steps 3–11) → trapsResolve → LootBind →
  EventPlace (draw-free: bind identities to legal hosts by the 4.2
              tie-break, or log absence; frame existing containers;
              choose referents or fall back, 4.5.5)
  → V13g / V15g (OneClockPerRoom) → V16g Curiosities
  ```
  **Pipeline positions (SV-9; seeds and validation design 4.4, 5.4; M1 pipeline, M2 curiosities):** EventDraw is GS3, computed once per floor and reused on any retry; EventPlace is GS15; the shared point is **AP2**, after EventPlace (AP1, HE-7's structural point, holds no V16 check). V16g is split (SV-20): `V16g.FixedDrawCount` at GS3; at AP2 `V16g.SlotLegal` before V15g, then the rest of V16g after V15g. `OneClockPerRoom` here is V10's check run at V15g (`V10.OneClockPerRoom`; reading recorded in seeds and validation design 13).
- **Strands** are read at floor entry or at offer time from committed run state. Loot never reads Curiosities, so **LootDoorInvariant holds**.
- **Play-time rolls** use `checks`, in the keyed-check domain `h:curiosity` (curiosityId, optionId, attempt index 0, die) (SV-6; 4.3.2; seeds and validation design 4.3; M2).
- **Resolving a Curiosity is a safe point** (a reading of MAIN 6.6, flagged), so a reload cannot undo a cost or re-roll (BC12). Unchanged by segment 10's mid-floor suspend slot (XI-28; agreed, exploration and information Q1; M2), which is written after every committed command and wins over the safe-point snapshot (exploration and information design 4.12). **Resolve order (lead's call, after the check):**
  1. validate payment feasibility;
  2. apply the costs;
  3. apply the gain (FactLedger, map, Grace, Strand);
  4. advance ticks and noise through the hearing rules (patrols may move or join; combat, if it starts, starts after the resolve);
  5. write the run snapshot, carrying the pending memory record (on a floor this **is** that command's slot write: its `suspend.journal` record, flushed before any outcome frame; reading, seeds and validation design 4.8.3 WO2, WO3; agreed, seeds and validation Q4);
  6. write the profile's `last[id]`, idempotent on replay from the snapshot. A crash between 5 and 6 replays step 6 on load (V16t `CrashBetweenWrites`).
- **Profile memory** feeds Retellings, the Twice-Told card, Afterword lines and the launcher. It sits outside the key. A profile whose `curiosityMemory` fails its version or checksum reads as empty: First Tellings again, and no Fallow redraws (9.1).
- **M3 snapshot:** Sequel and Ripening stages enter `drawableSet`. Shared, daily and simulator runs use the canonical snapshot.

### 4.11 Tone (changes EV-24, EV-25)

- **The Dreamer line.** No figure, voice, portrait, name, age, handwriting, signature, letter-writer or occupied bed of the Dreamer. Only effects: breathing walls, the heave, bedsheets, the lullaby heard and never seen played.
  - **The premise test (lead's call, after the check).** `DreamerLine`'s tone review asks of every premise: *who made, wrote, wound or watched this?* The answer must be a resident in plain sight (the Turnkey, the Night Usher, the choirmaster…), never the sleeper and never no one. A banned-noun check alone cannot catch an implied author. Under this test D1's letter is franked by the Night Usher's lamp-office, the Printed Itinerary leaves the Heart and lists floors, not choices, and the Heart's "Unread Page" becomes a pinned notice (5.3).
- **The Heart.** The approved Look is "dreamt at a child's scale, with no age or identity shown". So the Heart takes **at most one domestic-bedtime prop per visit** (`HeartBedroomCap`), and the night light / glass of water / turned-down sheet cluster is banned (FC5). Heart cards are architectural or giant-furniture images.
  - **Checkable after the check (lead's call, after the check):** every Curiosity and Glimpse record carries `domesticProp: bool`, and each Heart card is tagged in 5.3. `HeartBedroomCap` counts the Interlude (or the Coat Hooks, which replace it), and the Glimpses, together for each visit. F5 Glimpses are never domestic props, and the Coat Hooks are re-skinned as giant pegs on a tower-sized wall rail, with no door shown (non-domestic).
  - **No winding, no music box (lead's call, after the check).** Winding is the Lullaby Glyph's reserved cue (TL: "the mobile's dry wind-up click"), so no Heart card or Glimpse uses a wind-up or winding prop, and none is played by the player (the lullaby is heard, never seen played). The proposal's H1 "The Turning Key" is replaced (5.3); `ReservedCues` and KR8 carry the ban.
- **Figures.**
  - M2 figures are implied only: hands with a plain cuff at a speaking slot, a lamp and a glove at a booth window above head height, a back in a class-neutral coat seen in a high window, empty hats and coats on a wall rail. No face (RC3). **No faction motif or minion palette** on an implied figure (the LP Truant rule): no striped cuff (the Inmates' cloth) and no queue of guests (the Black-Crepe Guest) **(lead's call, after the check)**. M2 implied figures pass the role-ID gate as "implied figure, not unit".
  - M3 brings the **figure frame:** a non-unit map object in a niche, booth or behind glass, never behind a Drape; never on a walkable square; no roster silhouette. It has its **own grammar**, distinct from RC3's faceless mindless units: a flat, paper-cut figure below unit value tier, cropped at the shoulders by its niche, so it shows neither a face nor the keyhole tell. It passes the role-ID gate as "figure frame, not mindless unit" (EV-45) **(lead's call, after the check)**.
- **Reserved items, never used on Curiosities:**
  - no horns and no bells (RC7, RC9: a silent bell would lie);
  - no interactive chairs (RC2);
  - no clock-dial object that opens locks (RC3, RC5);
  - no moving painted moon (the Kennel's wrong detail);
  - no open-eyed Wake portrait and no running Wake clock (the Wake's wrong details; see W5 in 5.3) **(new lead's call** for the clock**)**;
  - no Bedside Stand, container, bait-cache or Windfall silhouette (LP-14, LP-24; `ReservedSilhouette`). A prop that is a container by name (a jar, luggage, a slot, a trough, a bowl) is drawn so it does not read as one: fused into a wall bracket, a rack, or a wall slot with no box body; a purse never appears (RC9: it reads as coin) **(lead's call, after the check)**;
  - **no Portal kind's silhouette or skin** (Door, Grille, Drape, Latch, Squeeze, Hairline) on any Curiosity prop, Glimpse or resolved state (RC1, RC2) **(lead's call, after the check)**. A door may appear only inside a card's diorama;
  - no wind-up or winding prop on the Heart, and no running hands or ticking on any clock or watch act on any floor: the act is the price chip only (the W5 treatment) **(lead's call, after the check)**.
- **Staging (RC1, RC1+, RC4; lead's call, after the check).** Every physical act in 5.3 is staged in-world as wall-mounted, dry or lidded, and leaves no residue. Any wet, oily, spilled, shattered or burning moment appears only in the card's diorama. Nothing in the world shows water off its squares, the Oil material, or the fire hue off fire squares, Hot pieces and attacks. KR16 and V16s `StagingRC1` carry this.
- **Register (told plainly).** The drafts called F1–2 "deadpan" a reading. By RS 7.3's definitions (dry = "deadpan sincerity, no punchline"), that would be a change. **We keep the comic register.** Punchlines live in Retellings and Glimpse art: a full body's punchline is displayed in full at most once per profile, and Twice-Told lines never carry one (4.5.2). Chips and the Twice-Told card use the global skin (RC7).
  - **Cards that straddle registers (lead's call, after the check).** RS 7.3 sets the register by floor; comic is *allowed* on F1–2, so a dry text is legal on F2 but a comic one is not legal on F3. A card drawable on floors of two registers (every band and Middle card, and the F2–3, F3–4 and F4–5 shared and band Interludes) is written **register-neutral**, in the driest register of its floors (dry for F2–3, absent for F4–5), with no punchline. Its F2 comic beat lives in the theme's F2 Glimpse art. The record carries `registerNeutral: bool`, so `RegisterByFloor` can be checked. The F1 pools and the Shallows (F1–2) keep the comic register in their Retellings. Told plainly: F2 band cards are dry in text.
- **Watch-list (RS 7.6).** Death imagery is never suicide imagery; the Wake rare is rewritten (FC12). Invented liturgies only. The church cap is unaffected. Tone review signs off every rare, every Sequel and every Heart card.

### 4.12 UI hooks (segment 10)

- **Cards:** the Curiosity card with chips (at most 2 per option, 6 words each); lit-option display with "met by" and a shape icon; the Twice-Told card with the delta line; First Telling; the worst-case card layout test (4.3.1) as an acceptance item.
- **Map:** facts drawn on the map; the referent highlight; bound-tick patrol forecasts.
  - **Answered by segment 10 (exploration and information design 4.3.6, 4.8.1):** every fact-sourced mark carries a "told" pip; **F-ROOM is drawn as interior Chalked** (the footprint; stubs still need sight) (reading, XI-4; M2); F-TREASURE (M3) as the Lodestring mark on the room node, the room Traced if Unknown, tier word and qualifier in hover (XI-22). **Chips and bound-tick forecasts read `PatrolForecast`** over known patrols only, with the `noticing` predicate, shown as a window, 20 ticks or one lap, withdrawn by events (XI-6; agreed, exploration and information Q2; M2).
- **HUD and state:** the Grace chip with its expiry; Strand display; the floor-entry Strand line (4.5.7).
- **Profile:** Afterword slot on the run-start card; codex pages (segment 10 hosts, RS owns; XI-29, M3); the Unwoken Roll and its hide setting; auto-answer and "read in full".
- **Accessibility:** text scale and skip.
- **Stratum Card:** **no line in M2**. M3 option: "Curiosities: n", counts only.

### 4.13 Hooks to other segments

- **Segment 9 (bosses):**
  - expose 2–3 seeded variables per boss for run-specific F-BOSS (answered: Rendition and mirror, three fact lines; BR-29, boss rooms design 4.12);
  - no Curiosities in arenas; Graces end at the seal (answered: the seal is `ArenaEngaged`, `arenaSeal` processed at every arena combat start while `sanctumEngaged`; no Grace exceptions in M2; BR-3, boss rooms design 4.2.1);
  - the boss-fact queue in the FactLedger (answered: the `bossQueue` order, 4.4.2; BR-29);
  - the Carnival rare's poster as an arena Glimpse (answered: the Ca7 poster on a Carnival wall slot, a wall decal, not a Curiosity; boss rooms design 4.1);
  - Forecourt Interludes keep `AntechamberScreened` (answered: boss rooms design 4.2);
  - the Coat Hooks payoffs on F5 (answered: Strands cashed at the Coat Hooks read the pre-rolled final boss's keys; boss rooms design 4.12) (cross-document comparison 2026-11-23, CC-120);
  - floor-1 boss variety as a production priority (agreed, Q2) (answered: 3 Renditions per F1 boss in M2, the launcher F1-boss streak rule and RS 12 Cut 1 graded; agreed, boss rooms Q1; BR-23 to BR-26, boss rooms design 4.10).
- **Segment 10:** 4.12 (answered: every hook placed in the UI surface catalogue; XI-4, XI-6, XI-22, XI-27, XI-29; exploration and information design 5).
- **Segments 11–12:**
  - the `curiosities` stream, its draw count and draw map; EventDraw and EventPlace order;
  - the resolve safe point; `curiosityMemory`; the launcher order against R-SEED-6;
  - V16; the reports and arms (9).
  - (answered: `curiosities` a `vector` of 12, attempt-invariant, with the labels kept; EventDraw GS3, EventPlace GS15, V16g at GS3 and AP2; the resolve write is that command's slot write (WO3); memory outside the key, simulator and seeded writes limited; launcher order with the boss rule; V16 phases and classes; reports event-sourced and classed; arms qualified; SV-1, SV-2, SV-4, SV-6, SV-8, SV-9, SV-13, SV-16, SV-20 to SV-23, SV-25, SV-30, SV-31; seeds and validation design 4.3, 4.4, 4.6, 4.8.3 WO3, 5.4, 6.2, 11).
- **Camp and hub:**
  - Afterword and keeper lines; Strand display (answered: the Afterword unchanged on the run-start card; keeper M2 service strings, M3 Strand lines only; Strands as cords over the camp fire, M3; CH-14; camp and hub design 2.2, 4.15.6, 5.1);
  - Pledges, Testimony and the Hawker's Hour (Later) (answered: Pledges and Testimony on Muster cards only, never ambient; the Hawker's Hour stays a floor event; Lost Property held; camp and hub design 2.2);
  - Tithe Bowl fact dedup through the FactLedger (answered: at the Keeper's Cart, dedup unchanged; M3; camp and hub design 2.2, 4.3);
  - coin owed (held, off) (answered: stays off; camp and hub design 2.2);
  - the Unwoken Roll and Sequel beats (answered: the Roll's hub home with the Sitter, all limits kept, CH-14, M3; Sequel beats on the run-start card, unchanged; camp and hub design 2.2, 4.10).

**Hooks the parents hand to segment 8, answered (lead's call, after the check).** One row per parent hook (2.2):

| Parent hook | Answer | Owner / wave |
|---|---|---|
| STAT 7.1 Leadership's "event outcomes" | One hero-only threshold on one option (4.3.2; EV-6) | Segment 8; stat owner; M2 |
| STAT 10 non-combat content on most floors | About one Curiosity per floor F1–3, Interludes F2–5 (4.2) | Segment 8; M2 |
| TL-34 Readings; TL 6.12 | Accepted; the second F3 Q room's M3 use (4.9; EV-30) | Segment 8; TL owner; M3 |
| TL 13 Riddle-lock clues (Hymn, Index, Place-Setting, Story) | **A guaranteed, check-free clue placement whenever the Riddle lock exists on the floor**: a fixed clue host (a wall Glimpse or a clue prop that any party can read, as TL's Hymn rule says a Lore-trained party reads at once), placed by EventPlace by the 4.2 tie-break and never a Curiosity option. Covers all four, the Place-Setting clue included. The proposal's lit-option clues on C1, S4 and H6 are withdrawn | Segment 8 places; TL owns the locks; M3 |
| TL 13 Hour locks on Landmark ticks | Not segment 8's: TL and HZ own them; segment 8 places no Landmark (EV-35) | TL, HZ |
| TL 13 Dream Fragments read the threshold rule | Folded into F-BOSS options on Lore thresholds (4.9; EV-29) | Segment 8; M3 |
| HZ 13 unguarded Dare forms | Not Curiosities: HZ owns Dare rooms, and no Curiosity is placed in one (4.2) | HZ |
| HZ 13 Moods on the Stratum Card | None in M2 (4.12); handed to segment 10 and HZ; M3 Omen costs may advance a named Mood (EV-31) | Segment 10, HZ; M3 |
| HZ 13 empty Tide naves as non-combat puzzles | Not M2 Curiosities: HZ owns them; an empty nave may be an M3 Curiosity host if HZ offers it | HZ; M3 option |
| HZ 13 the Gaol's small hours | A Glimpse and Retelling motif only (5.3), no rule | Segment 8; M2 |
| EP 13 activity vignettes in Q rooms ("remains of a revel") | Glimpse dressing beside the Q room's Curiosity, under RC1+, never a second decision | Segment 8; M2 |
| EP 13 no event spawns or moves units | Held: noise moves units only through the hearing rules (EV-14 reading) | Segment 8; enemies and EP owners |
| LE 12, LE-18 the shared boss-fact family | The FactLedger's `bossQueue` (4.4.2; EV-8) | Segment 8; LE, enemies; M2 |
| LP 4.13, LP 17 the Hawker's Hour; Lost Property | Later (EV-42); Lost Property stays with camp and hub | Items owner, camp and hub; Later |
| CMP 17 captive rooms and shrines for Pledges | Later (EV-41) | Companions owner; Later |
| RS 12 the codex; profile-side anti-streak | Codex pages co-owned (EV-37, M3); Launcher Fallow pulled into M2 (EV-20) | RS owner |

---

## 5. Per-theme catalogue (lead's call; agreed, Q2 for the Wake's parity; all illustrative content)

### 5.1 Catalogue rules (KR)

- **KR1.** Every card obeys 4.3–4.6. Facts go through the FactLedger.
- **KR2.** Every F1-eligible card is host-agnostic.
- **KR3.** At least 1 lit option on ≥ 800‰ of standard Curiosities. MECH cards are single-option.
- **KR4.** `GainMix`: fact-only options ≤ 40% per deck, ≤ 50% on the Graces-off fallback deck (4.4.1). 5.2 lists the counts.
- **KR5.** Premise tags as in 4.5.6. The tag is listed per card.
- **KR6.** No supply costs. A Grace is never free and never priced in ticks alone, on a priced or a lit option (4.4.3).
- **KR7.** F1 noise ≤ 4. F2–3 noise ≤ 6.
- **KR8.** No horns, bells, chairs, clock-dial lock objects, moving moon, open-eyed Wake portraits, running clocks or ticking, wind-up or winding props on the Heart, Portal silhouettes or skins, or reserved silhouettes; container-type props drawn so they do not read as containers (4.11).
- **KR9.** Each **text** card carries 2 Retellings in M2 (3 in M3), each with a Twice-Told line ≤ 15 words and no punchline. MECH cards and Interludes have **one presentation and no Retellings** (they are counted once in the budget) **(lead's call, after the check)**.
- **KR10.** Each Strand names its payoff and where it cashes: on entering the next floor, or at the Coat Hooks (4.5.7).
- **KR11.** Heart cards are facts only, each tagged `domesticProp`, with at most one domestic prop per visit.
- **KR12.** Rares stay inside DeckValueParity's band, read from `curiosityValueTable`: more words, never more value.
- **KR13.** Notation:
  - **(a)** is a priced option; **(b)** is lit, with its price; Leave is always present and free;
  - "→" separates cost from gain;
  - **[fb: …]** is a Grace option's `graceFallbackGain` (4.4.3); **{fb: …}** is a tied option's `referentFallbackGain` (4.5.5);
  - "tied" marks an option acting on a referent chosen at EventPlace;
  - *R2* and *R3* give the Twice-Told line or the Retelling beat.
  - **(new lead's call)** Two proposal rows listed their lit option first (W6, Gu3); their labels are swapped here so (a) is always the priced option, with no change of content. Strand payoffs added while writing are marked "[payoff added]" (4.5.7).
  - **(lead's call, after the check)** The rows below were re-authored after the check so that each pool meets `GainMix`, `LitHasPrice`, `GracePriceNotFree`, one rules gain per option and `LitSpreadPerPool`, and so that every prop meets the Portal, container, figure, staging and cue rules of 4.11. Each re-authored row keeps its card's premise unless its note says otherwise.
- **KR14.** Default art: wall-mounted or Furnishing-class (RC1+); a world prop with an active and an inert state; two Retelling art states in M2 (three in M3), First Telling being a treatment of R1's diorama; rule-bearing sounds on their approved cues; the global UI card-open sound; a silent Twice-Told card.
- **KR15.** One rules gain per option (plus an M3 codex page). Every option but Leave carries a price. A lit option's cost kinds share nothing with its card's priced option **(lead's call, after the check)**.
- **KR16.** Staging: every physical act is wall-mounted, dry or lidded, and leaves no residue; wet, oily, spilled, shattered or burning moments appear only in the card's diorama (4.11; `StagingRC1`) **(lead's call, after the check)**.

### 5.2 Summary

| Pool | Floors | M2 standard / MECH / rare | M2 Interludes | Fact-only options, M2 (Graces on / Graces-off fallback) | M3 target | selfReference card |
|---|---|---|---|---|---|---|
| Gaol of Small Hours | F1 | 6 / 2 / 1 | — | 6 / 16 (37.5%) / 8 / 16 (50%) | + successors | Your Name on the Charge Sheet |
| Kennel Wood | F1 | 6 / 2 / 1 | — | 6 / 16 / 8 / 16 | + successors | none |
| Endless Wake | F1 (authored in M2; drawn when its gate passes; agreed, Q2) | 6 / 2 / 1 (parity) | — | 6 / 16 / 8 / 16 | + successors | The Wake for You |
| Drowned Choir | F2–3 | 4 / 2 / 1 | 2 | 4 / 12 (33%) / 6 / 12 (50%) | 6 | none |
| Silk Archive | F2–3 | 4 / 2 / 1 | 2 | 4 / 12 / 6 / 12 | 6 | The Missing Last Page |
| Carnival of the Last Evening | F2–3 | 4 / 2 / 1 (1 MECH if EV-11 is not ruled, 4.4.6) | 2 | 4 / 12 / 5 / 12 (4 / 11 / 5 / 11 without Ca4) | 6 | Curtain Call |
| Long Supper | F4 (F3–4 from M3) | — | 3 | — | + 4 Curiosities | none |
| Gut of the World | F4 | — | 3 | — | + 3 Curiosities | none |
| Dreamer's Heart | F5 | — | 5 + the Coat Hooks | — | (the Printed Itinerary moves to the Middle pool as D-R) | none |
| Shallows shared | F1–2 | 6 | — | 4 / 12 (33%) / 6 / 12 (50%) | 10 | Letter, Coat (2) |
| Middle shared | F2–3 | 4 | — | 3 / 8 (37.5%) / 4 / 8 (50%) | 6 + the Printed Itinerary (rare) | none |
| Forecourt shared Interludes | F2–5 (≤ 2 floors each) | — | 4 | — | 6 | none |
| Inverted Cathedral | F3–4 | — | — | — | M4: 5 | — |

Counts follow 4.4.1's definition of a deck; V16s `GainMix` and `GraceFallbackDeck` recompute them from data.

### 5.3 Each theme

**The Gaol of Small Hours** (F1, M2). Law: Lockdown, where noise is the risk. Lesson: doors, noise and keys. Obsession: serving time for crimes not yet committed.

| # | Card | Form · tag | Options | Notes |
|---|---|---|---|---|
| G1 | The Turnkey's Daybook | Curiosity · residentWant | (a) Countersign: noise 4 here → Strand *Signed In* (on entering F2: F-ROOM, next unvisited). (b) Lore 12, 2 ticks: read back a week → F-TRAP on the Keybearer's route | The noise lesson. The Gaol's pilot Strand, now its option's one gain. *R2* "The column for visitors has grown." *R3* "Every visitor signed in tomorrow." Generic noise cue only. Renamed from "Ledger" (LE) |
| G2 | The Visitors' Slot | Curiosity · residentWant | (a) Pass a non-healing consumable through (offered) → G-Help (your choice of unit) [fb: reveal a Hairline]. (b) Intimidation 12 (hero), 2 ticks: demand the right answer → F-ROOM (next unvisited) | Hands with a plain cuff at a speaking slot, no bars, no face. *R3*: chalk marks on the cell wall, uncounted. Renamed from "the Visitors' Grille" (Grille is a Portal kind) |
| G3 | The Tally Board | MECH · mechanism | "CHALK OFF A DAY? 6 ticks, noise 2" → the tied unstepped cell padlock opens {fb: F-SECRET} | Replaces Time Served (no clock dial) |
| G4 | The Slop Hatch | MECH · mechanism | "LIFT THE FLAP? noise 2" → F-LISTEN on the tied adjacent room {fb: F-LISTEN, next unvisited} | A wall-mounted food flap with no opening into the next room |
| G5 | The Petitions Slot | Curiosity · ritual | (a) Post a non-healing consumable (offered) → Strand *Petitioned* (on entering F2: F-SECRET). (b) Perception 12, 2 ticks: read the petitions through the slot → F-BOSS (run-specific) | Inmates petition against crimes not yet done. A wall slot with no box body |
| G6 | The Borrowed Hour | Curiosity · lostObject | (a) Wind the backwards watch: Weary 1 (hero) → G-Ward (hero) [fb: reveal a Hairline]. (b) Medicine 12, 2 ticks: time your pulse against it → F-SECRET | The Gaol's one Weary option. Silent: no running hands, no ticking; the act is the chip |
| G7 | The Warders' Rota | Curiosity · place | (a) Rattle the rota board: noise 4 here → Strand *Off the Rota* (on entering F2: F-TRAP). (b) Medicine 12, 2 ticks: read the sick list → F-LISTEN on two Circuit rooms | Every hour on the rota is 3 a.m. |
| G8 | The Yard Wall | Curiosity · place | (a) Reach over the spikes: hero 2 HP → reveal a Hairline {fb: F-SECRET}. (b) Athletics 12, noise 4 here: vault it → G-Spur (hero) [fb: F-ROOM] | Renamed from "the Exercise Yard Line" (a line on the floor would be a walkable square) |
| G9 | Your Name on the Charge Sheet | Rare · **selfReference** | (a) Serve the sentence: 8 ticks + noise 2 → Strand *Time Already Served* (on entering F2: F-KEY [payoff added]). (b) Lore 12, hero 1 HP: read the charge sheet → G-Ward (hero) + codex page (M3) [fb: F-BOSS] | A charge-sheet board, not a cell door. Per-class aged gear in the diorama (first to cut: a class-neutral bundle). *R3*: the gear is gone. Renamed from "The Cell With Your Name" |

Gaol Glimpses: chalk calendars counting down to tomorrow; wanted posters dated tomorrow; a coat on a hook with the keyring outline missing; clock faces with still hands painted on the corridor wall (approved dressing, never on a lock or a door); the small hours as a motif.

**The Kennel Wood** (F1, M2). Law: the pack and the horn. Lesson: kill the leader; backs to the logs. Obsession: hounds hunting for a master who is now a portrait.

| # | Card | Form · tag | Options | Notes |
|---|---|---|---|---|
| K1 | The Fallen Portrait | Curiosity · residentWant | (a) Straighten the frame: hero 1 HP (splinters) → G-Help (hero) [fb: reveal a Hairline]. (b) Perception 12, 2 ticks: read the claw marks under it → F-LISTEN on the leader-led group's room | A wall frame, empty or turned to the wall, so the master's portrait stays the leader's cue; never the portrait-pole (RC2). No Cow Grace |
| K2 | The Collar With No Dog | Curiosity · lostObject | (a) Write a name on the tag: hero 1 HP (the buckle bites) → Strand *Collared* (on entering F2: F-TRAP). (b) Lore 9, 2 ticks: copy a name off the kennel nameplates → F-LISTEN on one Pack room | Not your name. The Kennel's pilot Strand. The Kennel's one F1 teaching rung, priced (4.3.2) |
| K3 | The Kennel Book | MECH · mechanism | "READ THE LAST PAGE? 4 ticks, noise 2" → F-LISTEN on the tied room {fb: F-LISTEN, next unvisited} | — |
| K4 | The Lodge Weathervane | MECH · mechanism | "TURN THE VANE? 4 ticks, noise 2" → reveal the tied Hairline {fb: F-SECRET} | Replaces the Moon Crank. The moon never moves |
| K5 | The Hunt Card | Curiosity · residentWant | (a) Sign on as a beater: noise 4 here → G-Spur (your choice of unit; choose your ground) [fb: reveal a Hairline]. (b) Intimidation 12 (hero), 2 ticks: strike out the Houndmaster's name → F-BOSS | The quarry is "the one that got away", not you |
| K6 | The Gamekeeper's Board | Curiosity · place | (a) Pin on a rosette: offer a non-healing consumable → G-Ward [fb: F-TRAP]. (b) Medicine 12, 2 ticks: follow the blood trail drawn on the board → reveal a Hairline {fb: F-ROOM} | — |
| K7 | The Unfinished Pedigree | Curiosity · ritual | (a) Add a line: 6 ticks + noise 2 → Strand *Pedigreed* (on entering F2: F-ROOM). (b) Athletics 12, hero 1 HP: drag the stud book off the high shelf → the tied Latch's far side {fb: F-ROOM} | — |
| K8 | The Feeding Trough | Curiosity · place | (a) Tip it into the wall drain: noise 4 here → G-Help (your choice of unit) [fb: reveal a Hairline]. (b) Medicine 12, 2 ticks: smell what is off → F-TRAP (next unvisited) | Wall-mounted; it tips into a wall drain, no spill |
| K9 | The Last Hound | Rare · residentWant | (a) Kneel by the coat: hero 2 HP → F-BOSS + codex page (M3). (b) Mechanics 12, noise 4 here: read the brass tag → G-Ward (hero) [fb: F-ROOM] | A coat on a hook and a still shadow, no face, no bowl; never Old Ragged's surcoat (IR-13); fiddle drone, no horns |

Kennel Glimpses: antlers and empty frames (no portraits); leashes coiled and hanging, never pulled taut into a wall (the Horn Post Cord); rain only in the backdrop; muddy pawprints climbing toward the painted moon. The "Horn Left Hanging" is cut (RC7).

**The Endless Wake** (F1; authored in M2, drawn when its gate passes, at F1PoolParity; agreed, Q2). Law: the sleeping house; tells. Obsession: a funeral that cannot end politely.

| # | Card | Form · tag | Options |
|---|---|---|---|
| W1 | The Guest Book | Curiosity · residentWant | (a) Add a condolence: 4 ticks + noise 2 → F-LISTEN on the tied lurker room {fb: F-TRAP, next unvisited}. (b) Perception 12, hero 1 HP (a paper cut): find the odd signature, a servant's → reveal a Hairline {fb: F-ROOM} |
| W2 | The Uncovered Mirror | MECH · ritual | "COVER IT? Weary 1 (hero)" → G-Ward (hero) [fb: F-SECRET]. Replaces "Open Eyes" (every portrait's eyes stay closed). The Wake's one Weary option |
| W3 | The Codicil | Curiosity · lostObject | (a) Read it aloud: noise 4 here → F-BOSS (run-specific). (b) Lore 12, 2 ticks: read it to yourself → F-KEY. Renamed from "The Will" (the Executor's signature) |
| W4 | The Black Armband | Curiosity · ritual | Hung on a wall hook, not a chair. (a) Wear it: 4 ticks + noise 2 → Strand *In Mourning* (on entering F2: F-TRAP [payoff added]). (b) Intimidation 12 (hero), leave a non-healing consumable for the dead: hush the room → G-Spur (hero) [fb: F-ROOM]. The Wake's pilot Strand |
| W5 | Start the Clocks | MECH · mechanism | "SET THEM GOING? 4 ticks, noise 2" → reveal the tied Hairline (the hands point the way) {fb: F-ROOM}. **(new lead's call)** The hands shudder forward and settle back: every Wake clock stays stopped at the moment of death (RS 8.5's wrong detail); the act is the noise and the tick cost, never a running clock |
| W6 | The Wake for You | Rare · **selfReference** | (a) Sit with the guests for the hour: 8 ticks + noise 2 → Strand *Rehearsed* (on entering F2: F-ROOM [payoff added]). (b) Leadership 12 (hero), leave a non-healing consumable on the lectern: correct the eulogy → F-BOSS + codex page (M3). Lying in the coffin is cut (RS 7.6). The proposal's G-Ward is dropped (one gain per option) |
| W7 | The Lily Clock | Curiosity · place | A flower clock with no hands; nothing moves. (a) Rearrange the lilies: 6 ticks + noise 2 → F-SECRET. (b) Medicine 12, hero 1 HP (the pollen): name the poison in them → G-Help (your choice of unit) [fb: reveal a Hairline] |
| W8 | The Receiving Line | Curiosity · residentWant | Empty hats and coats on a wall rail. (a) Shake every sleeve: 8 ticks + noise 2 → Strand *Shook Hands* (on entering F2: F-LISTEN [payoff added]). (b) Athletics 12, hero 1 HP: shoulder past the rail → the tied Latch's far side {fb: F-ROOM} |
| W9 | The Funeral Programme | Curiosity · lostObject | (a) Follow the order of service: 4 ticks + noise 2 → reveal a Hairline (the procession's way) {fb: F-ROOM}. (b) Medicine 12, leave a non-healing consumable as a wreath: read the cause of death → F-TRAP (next unvisited) |

Wake Glimpses: portraits with closed eyes; clocks stopped at the hour; empty hats and coats on a wall rail, never a queue of guests (the Black-Crepe Guest's read).

**The Drowned Choir** (F2–3, M2; register-neutral, dry). Law: the drowning line (Soaked, cold, lightning). Obsession: the hymn must never stop.

| # | Card | Form · tag | Options |
|---|---|---|---|
| C1 | Take a Shift | Curiosity · ritual | (a) Sing one verse (silent beyond the generic noise cue): 6 ticks + noise 4 here → G-Steady (your choice of unit) [fb: reveal a Hairline]. (b) Lore 13, Weary 1 (hero): read the hymnboard to the end → F-BOSS. The Hymn Riddle clue is no longer an option here (4.13) |
| C2 | The Black Basin | Curiosity · ritual | A black, dry, lidded wall basin; the soak is shown only in the diorama. (a) Bow your head: the hero starts the next fight Soaked (rider; EV-11, else hero 3 HP) → F-BOSS. (b) Arcana 13, noise 4 here: read the rim runes aloud → G-Help [fb: F-SECRET]. Renamed from "The Font" (the dry-font Snag) |
| C3 | The Dry Pew | MECH · place | A pew-back fixed to the wall; "sit out" is the price chip only (RC2). "SIT OUT A VERSE? hero 2 HP (the damp)" → G-Ward [fb: reveal a Hairline]. Repriced away from ticks alone |
| C4 | The Waterlogged Pipe | MECH · mechanism | "BANG THE PIPE? noise 4 here" → F-LISTEN on the two tied adjacent rooms {fb: F-LISTEN on one adjacent room, else F-ROOM}. Replaces the Drowned Bell; "blow it" is cut (the horn family is reserved) |
| C5 | The Unsung Verse | Curiosity · residentWant | (a) Offer an item to the choirmaster's stand → Strand *Settled Debt* (on entering the next floor: F-ROOM). (b) Perception 13, 2 ticks → F-SECRET |
| C6 | The Rising Waterline | Curiosity · place | A waterline painted on the wall above head height. (a) Climb to the high mark: hero 3 HP → the tied Latch's far side {fb: F-ROOM}. (b) Athletics 13, 4 ticks: climb the gallery → reveal a Hairline {fb: F-SECRET} |
| C7 | The Hymn Stops | Rare · ritual | (a) Let the silence hold: 8 ticks → Strand *The Silence Held* (on entering the next floor: F-SECRET [payoff added]). (b) Leadership 13 (hero), noise 4 here: start it again → G-Steady + codex page (M3) [fb: F-BOSS]. An organ overtone stays in the bed, so silence is never a stealth cue. The proposal's F-BOSS on (a) is dropped (one gain per option) |
| C-I1 | The Damp Order of Service | Interlude, F2–3 | "Read the last verse? 2 ticks" → arena-path F-TRAP |
| C-I2 | The Drip Count | Interlude, F2–3 | "Count the drips? 2 ticks" → F-LISTEN on the arena |

**The Silk Archive** (F2–3, M2; register-neutral, dry). Law: webs and silence; fire is the key. Obsession: finish the book; overdue borrowers are shelved.

| # | Card | Form · tag | Options |
|---|---|---|---|
| S1 | Second Notice | Curiosity · residentWant | (a) Return a scroll (offered) → F-BOSS. (b) Stealth 13, worst member (movement), 2 ticks: tiptoe past the desk → reveal a Hairline {fb: F-ROOM}. Renamed from "Overdue Notice" (Silk Mood) |
| S2 | The Card Catalogue | MECH · mechanism | "PULL ONE CARD? 4 ticks, noise 2" → F-CACHE (else F-ROOM until the LE and 7c owners rule) |
| S3 | Silence, Please | MECH · ritual | "OBEY? Weary 1 (hero)" → G-Help (`graceTarget: worstStealth`) [fb: reveal a Hairline] |
| S4 | Finish the Sentence | Curiosity · ritual | (a) Write anything: 4 ticks + noise 2 → Strand *In the Book* (on entering the next floor: F-ROOM [payoff added]). (b) Lore 13, offer a scroll: complete it properly → F-SECRET. The proposal's "(c) Burn the page" is cut (the fire hue, RC4), and the Index Riddle clue is no longer an option here (4.13) |
| S5 | The Lamp Wick | Curiosity · lostObject | (a) Carry its light a while: Weary 1 (hero) → G-Help (your choice of unit) [fb: F-SECRET]. (b) Arcana 13, 2 ticks: read the wick-rune → reveal a Hairline {fb: F-ROOM} |
| S6 | The Missing Last Page | Rare · **selfReference** | (a) Return it: 6 ticks + noise 2 → F-BOSS + codex page (M3). (b) Keep it: Weary 1 → Strand *The Last Page* (cashes at the Coat Hooks: the final boss's F-BOSS) |
| S7 | The Spider's Copy | Curiosity · residentWant | (a) Correct their copy, a shelf map: 6 ticks + noise 2 → the tied Latch's far side {fb: F-ROOM}. (b) Perception 13, hero 2 HP: find the error by touch → G-Ward [fb: F-SECRET] |
| S-I1 | The Returns Slip | Interlude, F2–3 | "Read the slip? 2 ticks" → arena F-ROOM |
| S-I2 | The Shelf Mark | Interlude, F2–3 | "Follow the mark? 2 ticks" → arena-path F-TRAP |

**The Carnival of the Last Evening** (F2–3, M2; register-neutral, dry). Law: oil and fire both ways. Obsession: the show goes on; a ticket costs a memory.

| # | Card | Form · tag | Options |
|---|---|---|---|
| Ca1 | Admission: One Memory | Curiosity · ritual | (a) M2: pay a non-healing consumable (offered) → G-Spur [fb: reveal a Hairline]. M3: "your map loses its colour until camp" + 4 ticks; never navigation data (FC10). (b) Leadership 13 (hero), 2 ticks: talk your way in → the tied Latch's far side, the staff way {fb: F-ROOM}. Renamed from "Ticket Booth" (the Ticket lock) |
| Ca2 | Ring Toss | MECH · rolled | "ONE THROW. noise 4 here. Acrobatics vs 13: n% (shown)" → success G-Help; failure nothing more [fb: reveal a Hairline] |
| Ca3 | The High Mirror | Curiosity · place | The mirror clouds and never shatters. (a) Rap on the glass: noise 4 here → reveal a Hairline (a reflected route) {fb: F-ROOM}. (b) Leadership 13 (hero), Weary 1: take a bow → G-Steady (hero) [fb: the tied Latch's far side {fb: F-SECRET}]. The proposal's "the mirror was a door" is withdrawn (the prop would lie about a passage) |
| Ca4 | The Fire-Eater's Tip Jar | MECH · ritual | A sealed wall dispenser fused into a bracket; the oil is shown only in the diorama. "OIL YOUR HANDS FOR LUCK?" The hero starts the next fight Oiled (rider; EV-11) → G-Ward (hero) [fb: reveal a Hairline]. M3 if EV-11 is not ruled |
| Ca5 | The Applause Machine | Curiosity · mechanism | (a) Take a bow: noise 4 here (the generic noise cue, no applause foley) → Strand *Took a Bow* (on entering the next floor: F-LISTEN, next unvisited). (b) Perception 13, 2 ticks: see who is cranking it → F-LISTEN on the next room |
| Ca6 | The Fortune Machine | Curiosity · mechanism | (a) Feed it a consumable → F-BOSS. (b) Arcana 13, 2 ticks → F-TRAP |
| Ca7 | Curtain Call | Rare · **selfReference** | A stub with your name. (a) Stand in the spotlight: 8 ticks + noise 2 → F-BOSS + codex page (M3); the poster, a silhouette, appears as an arena Glimpse (hook, segment 9). (b) Leadership 13 (hero), offer an item as your prop: refuse the role with style → G-Steady (hero) [fb: F-ROOM]. Renamed from "The Last Act" (Wager room) |
| Ca-I1 | The Running Order | Interlude, F2–3 | "Read the bill? 2 ticks" → F-BOSS |
| Ca-I2 | The Prompter's Cue | Interlude, F2–3 | "Peek at the cue? 2 ticks" → F-LISTEN on the arena |

**The Long Supper** (F4 in M2 as Interludes; F3–4 Curiosities from M3). Law: freeze and shatter. Obsession: the empty chair; the seating chart is law.

| # | Card | Form | Options |
|---|---|---|---|
| Su-I1 | The Seating Chart | Interlude, substitutive | "Swap two place cards? 2 ticks" → arena F-ROOM |
| Su-I2 | The Warming Dish | Interlude, substitutive | "Lift the lid? noise 2" → F-LISTEN on the arena. Renamed from "Brazier's Due" (brazier is a terrain piece; Due is LE) |
| Su-I3 | The Bill of Fare | Interlude, substitutive | "Read the one course? 2 ticks" → arena-path F-TRAP. Renamed from "Menu Card" (HZ) |
| Su1 (M3) | The Laid Place | Curiosity | (a) Take the place card: the hero starts the next fight Chilled → F-BOSS. (b) Leadership 14 (hero), noise 4 here: decline by the chart → G-Steady [fb: F-ROOM]. The chair stays tucked in (RC2) |
| Su2 (M3) | Pass the Salt | Curiosity | (a) Pass an item (offered) → F-BOSS. (b) Perception 14, 2 ticks → F-TRAP |
| Su3 (M3) | Every Glass Rises | Rare | (a) Drink without a toast: lingering Poisoned → F-BOSS. (b) Leadership 14 (hero), Weary 1: give the toast → G-Steady + codex page [fb: F-ROOM]. Renamed from "The Toast" (boss signature) |
| Su4 (M3) | The Folded Napkin | Curiosity | (a) Unfold it at the empty place: lingering Poisoned → Strand *Sat Down* (cashes at the Coat Hooks: arena-path F-TRAP [payoff added]). (b) Perception 14, 2 ticks → F-SECRET. The proposal's G-Spur is dropped (one gain per option). Coordinate with HZ-30; never a heal |

**The Gut of the World** (F4). Law: attrition. Obsession: tenants complain about noise and rent.

| # | Card | Form | Options |
|---|---|---|---|
| Gu-I1 | Noise Complaint | Interlude, substitutive | "KEEP IT DOWN. Knock anyway? noise 4" (the generic noise cue, no knock foley) → F-LISTEN on the arena |
| Gu-I2 | The To-Let Sign | Interlude, substitutive | "Read the particulars? 2 ticks" → arena F-ROOM |
| Gu-I3 | Undigested Letters | Interlude, substitutive | 4 ticks → arena-path F-TRAP; or Lore 14, noise 2 → F-LISTEN on the arena. The proposal's F-BOSS is withdrawn (not substitutive, 4.7) |
| Gu1 (M3) | Rent Due | Curiosity | (a) Pay in kind (offered item) → F-KEY. (b) Intimidation 14 (hero), noise 4 here → G-Ward (hero) [fb: F-ROOM]. Supply price removed |
| Gu2 (M3) | The Knitted Scar | Curiosity | A purse-shaped scar in the flesh wall. (a) Reach in: hero 4 HP → the tied unstepped strongbox opens early {fb: F-SECRET}. Never HE, the Due or a boxed Gutworm. Renamed from "The Swallowed Purse" (a purse reads as coin, RC9) |
| Gu3 (M3) | The Chapel Being Digested | Rare | (a) Call into it: noise 6 here → G-Steady [fb: F-ROOM]. (b) Athletics 14, hero 2 HP: climb in → F-SECRET + codex page. No bell rope |

**The Dreamer's Heart** (F5). Forecourt Interludes, facts only, absent register, at most one domestic prop per visit, no winding props.

| # | Card | Card text | Gain | Clock · domesticProp |
|---|---|---|---|---|
| H1 | The Skirting Board | "Ear to it?" 2 ticks | F-LISTEN on the arena. Architectural; the generic UI cue, no click, no melody. Replaces "The Turning Key" (a winding prop: the Lullaby Glyph's reserved cue, a lullaby played, and an implied prior winder) | Substitutive · no |
| H2 | The High Window | "Look out?" 2 ticks | Arena-path F-TRAP. A giant window seen above head height, no curtain (the Drape skin). Renamed from "The Curtain Gap" | Substitutive · no |
| H3 | The Bedpost Ladder | "Climb?" 4 ticks | Arena F-ROOM (the arena's layout) | Substitutive · **yes** |
| H4 | The Coat Hooks by the Door | "Your things, from earlier." | Cash one carried Strand for its named payoff. Shown only when a Strand can be cashed here, and then **instead of** the drawn Interlude. Re-skinned as giant pegs on a tower-sized wall rail, no door shown. Replaces the "Bedside Shelf" (Bedside Stand silhouette) | Exempt (Strand cash; replaces the Interlude, adds no stop) · no |
| H5 | The Wall That Keeps Its Distance | "Walk toward it?" 4 ticks | Arena-path F-TRAP (a second trap; dedup falls through to arena F-ROOM). The proposal's F-BOSS is withdrawn (not substitutive, 4.7) | Substitutive · no |
| H6 | The Pinned Notice | "Read it?" 2 ticks | Arena F-ROOM (dedup falls through to arena-path F-TRAP). A giant notice pinned to a bedpost: no book, no story, no fire. Renamed from "The Unread Page" (a bedtime story implies a reader); its lit path is dropped (a pure cost waiver) | Substitutive · **yes** |

Cut: the Glass of Water and the Turned-Down Corner (RS 7.5; FC5, KC19). Moved: the Printed Itinerary (H-R) leaves the Heart for the Middle pool (below).

**Shared pools**

| # | Card | Pool · tag | Options |
|---|---|---|---|
| D1 | The Letter Addressed to You | Shallows · **selfReference** | An envelope franked by the Night Usher's lamp-office (a resident sender in plain sight). (a) Sign for it and keep it sealed: 4 ticks → Strand *Sealed Letter* (on entering the next floor: that floor's F-BOSS) (KC13). (b) Open it now: noise 2 here → F-TRAP (next unvisited) |
| D2 | Someone Wearing Your Coat | Shallows · **selfReference** | A back in a class-neutral coat, seen in a high window. (a) Follow: 6 ticks → reveal a Hairline (where it went) {fb: F-SECRET}. (b) Leadership 12 on F1, 13 on F2 (hero), noise 4 here: call out → G-Help (hero) [fb: F-ROOM] |
| D3 | The Stopped Mantel | Shallows · mechanism | (a) Wind it: hero 1 HP (the key bites) → G-Spur (your choice of unit) [fb: F-ROOM]. (b) Mechanics 12, 2 ticks: find the false back → reveal a Hairline {fb: F-SECRET}. Silent; the hands shudder and settle back (the W5 treatment), so it is legal on a Wake or Gaol floor |
| D4 | The Spare Key on a String | Shallows · lostObject | (a) Take it: noise 4 here → F-KEY. (b) Stealth 12 (acting unit; single-actor manipulation), 2 ticks: lift it off its string → the tied unstepped ordinary lock opens {fb: F-SECRET} |
| D5 | The Night Usher | Shallows · residentWant | A lamp and a glove at a booth window above head height. (a) Leave him luggage (an offered non-healing consumable) → G-Ward (your choice of unit) [fb: reveal a Hairline]. (b) Leadership 12 (hero), 2 ticks: ask which floor → F-ROOM (next main path). M3: Sequel stage 1. Later: the wanderer Pledge source |
| D6 | The Lantern Left Burning | Shallows · lostObject | (a) Take the light: hero 1 HP → F-LISTEN on the next lurker room. (b) Perception 12, 2 ticks: spot the draught → reveal a Hairline {fb: F-SECRET} |
| D7 | The Door You Already Opened | Middle · place | A knocker and a nameplate in the last floor's theme on a wall panel; the full door appears only in the diorama. (a) Knock: noise 4 here → F-SECRET. (b) Lore 13, 2 ticks: read the old floor's plate → Strand *Shut Behind You* (on entering the next floor: F-TRAP [payoff added]). M3: Chute skin only per EV-36 |
| D8 | The Umbrella Stand | Middle · lostObject | No drip. (a) Take an umbrella: noise 4 here (the stand rattles) → G-Ward [fb: reveal a Hairline]. (b) Perception 13, 2 ticks: find the dry one → F-ROOM |
| D9 | Left Luggage | Middle · lostObject | A left-luggage ticket rack, not a case. (a) Hand in an item (offered) → Strand *Left Luggage* (on entering the next floor: F-ROOM). (b) Leadership 13 (hero), noise 4 here: call the attendant → G-Help [fb: F-SECRET] |
| D10 | The Wrong Floor Sign | Middle · place | (a) Follow it: 6 ticks → the tied Latch's far side {fb: F-ROOM}. (b) Lore 13, noise 2 here: read the small print → F-SECRET |
| D-R (M3) | The Printed Itinerary | Middle · rare | Printed type, no hand; it lists the floors of this run by theme, never the player's choices. (a) Read it through: 6 ticks + noise 2 → F-BOSS + codex page. Moved from the Heart (it implied a watcher at the sleeper's bedside) |
| I-1 | The Clock With No Hands | Interlude, F2–3 | "Study the face? 4 ticks" → arena F-ROOM. No ticking |
| I-2 | The Late Mirror | Interlude, F2–3 | "Look? 2 ticks" → arena-path F-TRAP |
| I-3 | The Lost Glove | Interlude, F3–4 (substitutive on F4) | "Take it down from the hook? 2 ticks" → F-LISTEN on the arena |
| I-4 | The Stair Count | Interlude, F4–5 (substitutive) | "Count them? 2 ticks" → arena-path F-TRAP |

**M3 and M4 themes.** Mirror Court, Ossuary Opera, Moth Wedding, the Ministry of Small Mercies and Tallow Abbey each ship at unlock with at least 4 standard, 2 MECH and 1 rare, plus 2 Forecourt Interludes for each theme drawable on F2–5. Ministry and Tallow are F1-eligible and need the F1 counts (6 / 2 / 1, under `F1PoolParity`). The Inverted Cathedral (M4) gets 5 under the church cap, avoiding the censer-rope Cord and the Collection Plate trap.

---

## 6. Writing, art and audio (lead's call; restated after the check)

**Writing**
- **Strings by kind, M2 with the Wake** *(illustr.)*:

  | Kind | Count | Words |
  |---|---|---|
  | Card bodies (46 text cards × 2 Retellings) | 92 | about 6.5–7.5k |
  | Twice-Told lines (one per Retelling) | 92 | about 1.1k |
  | Option labels and past-tense footers ("Last time: …") | about 210 options | about 1.2k |
  | Near-wordless lines (12 MECH, 22 Interludes incl. the Coat Hooks) | 34 | about 0.3k |
  | Glimpse hover captions (8 per theme, 9 themes) | about 72 | about 0.7k |
  | Afterword lines (4.5.10) | about 60–90 | about 0.7k |
  | Strand closure lines | about 20 | about 0.2k |
  | **Total** | | **about 11–12k** |

- About 17k in M3 with R3; about 27k with Ripening *(illustr.; re-estimated after the check)*.
- Comic Retellings carry the punchlines on F1–2 (the F1 pools and the Shallows); band, Middle and straddling cards are register-neutral (4.11).
- Tone review for every rare, Sequel and Heart card and every Afterword line, against RS 7.5 and 7.6 and the premise test (4.11).
- **Briefs before M2 content lock (lead's call, after the check).** The catalogue in 5.3 is a rules sheet, not a writing brief. Before content lock, each text card gets a brief: the premise in one line; the R1 hook; the R2 twist, which changes the image and not just the wording; where the punchline sits (an R2 line or Glimpse art); a Twice-Told line per Retelling; the register for each eligible floor; banned motifs. Each theme gets a Glimpse brief: 8 motifs, its wrong detail or dream device, banned motifs, and a caption register. These are writing deliverables, owned by segment 8 and listed in 16.

**Art (the eager team's showcase)**
- **Assets by kind, M2 with the Wake** *(illustr.)*:

  | Kind | Count | States |
  |---|---|---|
  | Card dioramas, one per Retelling of a text card (First Telling is a treatment of R1's, not a separate asset) | 92 | 92 |
  | Card art for MECH cards and Interludes | 34 | 34 |
  | World props, active and inert (80 Curiosities and Interludes) | 80 | about 160 |
  | Per-class and per-theme variants (G9's aged gear; D7's nameplates; Ca7's poster and its arena Glimpse) | — | about 16 |
  | Glimpses (8 per theme, 9 themes) and their DV14 rotation variants | about 72 | about 100–140 |
  | **Total** | | **about 400–450** |

- **Cut order when capacity runs short:** per-class and per-theme variants first (G9 shows a class-neutral bundle; D2 already uses a class-neutral coat; Ca7's poster is a silhouette), then Glimpse rotation variants, then band rares; never F1 or Shallows depth or their R2 (0.1).
- The figure frame (M3), and Unwoken Roll skins per theme except the Heart (M3).
- Every new figure or prop passes the M2 readability gates: greyscale, silhouette lineup, tone by depth, and the role-ID rows, which add **"Curiosity prop or Portal"**, **"Curiosity prop or container"**, **"Curiosity prop or bait-cache"**, **"implied figure or unit"** (M2), **"figure frame or mindless unit"** (M3) and, for the Kennel, **"Glimpse or Cord"** **(lead's call, after the check)**. **Camp and hub (CH-12; M3):** a role-ID row "hub coat display vs the Coat Hooks and implied-figure silhouettes"; the hub's coat rack hangs each mantle on its own newel peg, never hooks by a door (camp and hub design 4.8, 6).

**Audio (restated after the check)**
- **Card open and First Telling:** the RC7 global UI timbre, a soft "paper turn" on open and a "soft swell" on a First Telling. Neither is a chime (the floor-1 small-hour chime is a reserved RC7 cue) nor a sting (stings are reserved for bosses, Stir announcements and the pending Choir Ritual). Both join the audio blind-ID test at ≥ 85% *(illustr.)*. Twice-Told cards are silent.
- **Rule-bearing sounds** use their approved cues: every noise option plays **only the generic noise cue**, with no diegetic flavour foley (no drawer slam, knock, pipe, applause or singing). Watch, clock and mantel acts are silent: no ticking (the Gaol keeps ticking for its boss arena; the Wake's clocks are stopped).
- **No reserved cue in any bed:** horns, alarm bells, the struck-bronze cue, the Lullaby Glyph's wind-up click and the Heart's music box stay out (RC7; TL). **Camp music box (CH-12; M2):** the camp box has its own timbre, degraded by filtering, detune and dropped notes only, and passes the blind-ID gate against the Heart's box and the Gaol bed's box; no figure handles it (camp and hub design 6).
- `ReservedCues` is an **audio-tag check** against the RC7 and TL timbre tables, as well as the horn and bell prop check (9.2).

---

## 7. New mechanics considered

| Mechanic | Source | Verdict | Wave |
|---|---|---|---|
| Twice-Told card (delta-first repeat card) | NM, AR, FC7 | **Adopted** (4.5.2; agreed, Q1) | M2 |
| First Telling flourish | NM | **Adopted** | M2 |
| Retellings, display-only | All; key placement per NM | **Adopted** (4.5.3; agreed, Q1) | M2 / M3 |
| Launcher Fallow (F1, K = 3) | NM, AR | **Adopted** (4.5.4; agreed, Q1) | M2 |
| Strict F1 + F2 Freshness check | AR | **Rejected**: 4–5 redraws per run by runs 30–50 exhausts the budget | — |
| Least-seen or Seeded Fallow inside the key | A, B, NM | **Rejected** (client option Q1-B; RS 9.1 stays closed) | — |
| Referents and bound costs | C, FC3 | **Adopted** (4.5.5) | M2 |
| Graces | A, B, NM | **Adopted, gated** (4.4.3; agreed, Q3) | M2 / M3 |
| Overtures | C, NM | **Adopted for M3** with exclusions (4.9) | M3 |
| Strands / Bookmarks | A, NM, lead | **Adopted**; cashed on entering the next floor after the check, no longer as Interlude overlays (4.5.7) | M2 lean / M3 |
| Sequels on sight | B, NM, lead | **Adopted for M3** (4.5.8; agreed, Q1) | M3 |
| Ripening | Lead | **Adopted late**, with SuccessorParity (agreed, Q1) | M3-late / M4 |
| Late Leaves (by runs started) | A | **Rejected**: farmable by abandoning runs | — |
| Afterword lines | NM | **Adopted** (4.5.10) | M2 lean / M3 |
| Memorial plaque / Unwoken Roll | NM, lead | **Merged** into one device | M3 |
| Omen Retellings and Nightmare Provisos | NM, A | **Adopted** (4.9) | M3 |
| The Usher / figure frame | NM, C | **Adopted** as the Night Usher in the figure frame | M3 |
| Testimony | NM | **Adopted Later** with Pledges | Later |
| Salience engine with Stakes | B | **Rejected as engine**; parity kept as a check | — |
| Model C's M3 Captive Parting Word | C | **Rejected**: stays with Pledges | Later |
| Codex-gated better options | — | **Rejected** (MAIN 4.7; HZ N13) | — |
| Corpse runs | — | **Rejected** (LE 6.1; DH 5.4(f)) | — |
| Visible opt-in streak help through events | Client option Q4-B | **Rejected** (agreed, Q4): difficulty help belongs in the visible ladder | — |
| Coin owed at camp | A, B, C | **Held, off**; offered to the camp owner | Later option |
| Run-count or depth gating of base pools | AR | **Rejected**: starves streaking players | — |

---

## 8. Numbers with fixes (toy, not simulator; all illustr.)

**Method.** 2,000 trials per arm. Arms:
- streak: dies on F1 40% / F2 60%;
- f1only: always dies on F1;
- weak: clears × 0.8;
- learner: clears × 0.8 rising to × 1.0 by run 20;
- average: RS 10.6 clears.

Slots as in 4.2; shared share 400‰ unless capped; rares at 30‰. Pools:
- **LEAD_M2:** the first catalogue as written.
- **REC_M2:** F1 6 / 2 / 1 per theme; Shallows 6; band 4 / 2 / 1; Middle 4; Interludes 4 shared + 3 per deep theme + 6 Heart; R2. (The band Interludes added after the check are not in the toy; Interludes are not text-card meetings, so txtNew, txtFresh and near3 do not move, but `SharedInterludeMostSeen` will.)
- **REC_M3:** F1 7 / 2 / 1; Shallows 8 + 1 rare; band 5 / 2 / 1; Middle 6 + 1; R3; Ripening at the 4th sighting. **Told plainly after the check:** these pool sizes differ from 5.2's M3 targets (Shallows 10, band 6, Middle 6 + the Printed Itinerary); 5.2's targets are larger, so the M3 rows below are, if anything, a floor.

**Table A. Streak arm** (txtNew / txtFresh / near3; "most" = most sightings of one card)

| Setup | 10 runs | 30 runs |
|---|---|---|
| LEAD_M2, uniform (FTL-like) | 0.66 / 0.66 / 0.20; most 3.4 | 0.44 / 0.44 / 0.23; most 9.1 |
| LEAD_M2, union draw | 0.80 / 0.80 / 0.14 | 0.53 / 0.53 / 0.16 |
| REC_M2 uniform + R2 | 0.87 / 0.99 / 0.08 | 0.67 / 0.91 / 0.09 |
| **REC_M2 + Fallow F1 (adopted M2; agreed, Q1)** | **0.92 / 1.00 / 0.04** | **0.70 / 0.93 / 0.04** |
| REC_M2 + Fallow F1 + seeded Fallow F2–3 (RS 9.1 change; not adopted) | 0.93 / 1.00 / 0.00 | 0.69 / 0.94 / 0.00 |
| REC_M2 + strict F1+F2 launcher (rejected) | 1.00 / 1.00 / 0.02 | 0.88 / 0.99 / 0.03 |
| Bound, not an option: REC_M2 + least-seen in key on every floor from M2 (not Q1-B; cross-document comparison 2026-11-23, CC-120) | 1.00 / 1.00 / 0.00; most 1.0 | 0.95 / 1.00 / 0.00; most 1.9 |
| REC_M3 + Fallow F1 (adopted M3; agreed, Q1) | 0.94 / 1.00 / 0.03 | 0.75 / 1.00 / 0.04 |

**Table B. 50 runs** (txtNew / txtFresh)

| Setup | streak | learner | average |
|---|---|---|---|
| REC_M2 uniform R2 | 0.53 / 0.80 | 0.35 / 0.62 | 0.35 / 0.61 |
| M3 R3, no Ripening | 0.58 / 0.96 | 0.40 / 0.85 | 0.40 / 0.84 |
| M3 R3 + Ripening + Fallow F1 | 0.63 / 1.00 | 0.49 / 0.99 | 0.49 / 0.99 |

**Reading:**
- For the streak, selection policy matters as much as pool size.
- Retellings carry fresh text far past the point where cards repeat.
- Only Ripening keeps the long tail fresh past run 30. It helps average players at 50 runs (0.84 → 0.99), not the streak arm. Hence M3-late (agreed, Q1).

**Launcher cost:**

| Launcher | Redraws per run |
|---|---|
| Fallow K = 3, F1 only | 0.10–0.15 throughout |
| Strict F1 + F2, average arm | 1.5 at run 10, 4.4 at run 30, 5.4 at run 50 |

Neither skewed the F1 theme share (Gaol 0.50, with two F1 themes; `Floor1ThemeShare` reads 1/n, 4.5.4).

**Reading load and clock** (average arm, REC_M2; seconds per floor visit):

| Floor | Line | Runs 1–3 | Runs 4–10 | Runs 11–30 |
|---|---|---|---|---|
| F1 | 30 s, 4 clicks | 29 | 27 | 23 |
| F2 | 30 s | 30 | 25 | 17 |
| F3 | 18 s (old line) | 30 | 24 | 17–24 |
| F4–5 | 6 s | 6.9–7.6 | 5.0–7.4 | 3.5–4.5 |

- About 38 s per run for the streak arm and 80–100 s for the average arm (1.5–2.5% of play).
- Clicks pass everywhere.

**Power** (`dh.py`; hero-HP equivalents at the boss door; base F1 hero death 3.0%):

| Policy | Mean shift | F1 hero death | Worst single card |
|---|---|---|---|
| Rational | +0.44 to +0.46 HPe | 2.52–2.76% | 3.01% |
| Typical | +0.05 | 2.92–2.98% | 3.65–4.41% |
| Novice | −0.2 | 3.11–3.25% | 3.65–4.41% |

The worst cards were noise 6 and Lure options, now removed.

**Content cost:**

| Package | Text cards | Presentations | Words | Art states |
|---|---|---|---|---|
| REC_M2 as the toy costed it (Gaol and Kennel only; bodies and one diorama per presentation) | about 39 (+ 26 near-wordless) | about 78 | about 7k | about 105 |
| **M2 as restated after the check** (with the Wake and the band Interludes; every string and asset kind, 6) | about 46 (+ 34 near-wordless) | about 92 | about 11–12k | about 400–450 |
| M3, R3, no Ripening (bodies only, as costed by the toy) | about 50 | about 150 | about 12k | about 150 |
| M3, R3 + Ripening (bodies only, as costed by the toy) | about 93 | about 279 | about 21.6k | about 360 |

**Failures found, with fixes:**

| Id | Failure | Fix (where) |
|---|---|---|
| NF1 | One F1-eligible shared card at 400‰ fills about 40% of F1 slots | Share capped by size; Shallows ≥ 6; PoolCoverage per eligible slot (4.2, 9) |
| NF2 | Uniform draws hold near3 at 0.06–0.09 at any pool size | Launcher Fallow (4.5.4) |
| NF3 | Shared Interludes seen about 20× in 50 runs | ≤ 2 floors each, ≤ 200‰, ≤ 1 per run (4.2) |
| NF4 | The F1 side-room host is fragile (LT16) | Alcove set piece first; presence report (4.2) |
| NF5 | F3 fails its line | +0.5 on light and median arms; Twice-Told format on the heavy arm (4.7) |
| NF6 | F4–5 fail for new players | Substitutive families only; net ≤ 0; cut order (4.7) |
| NF7 | F1 noise 6 adds about 1 pp for a novice | F1 noise ≤ 4, clear of the Circuit (4.4.4) |
| NF8 | Tick-priced Graces dominant (> 850‰); supply options dead (< 50‰) | No tick-only Grace prices; no supply costs (KR6) |
| NF9 | The strict Freshness check exhausts the budget | Rejected (7) |
| NF10 | Model C's 11% exact-repeat figure needs 3 referent kinds; F1 has about 2 (12% at 10 runs, 34% at 30) | Referents plus Retellings, not Referents alone (4.5.5) |
| NF11 | Model B's Stake filter doubles near-repeats in small pools | Parity as a static check only (3.3) |

**Told plainly:** the critic fixes since the toy (rares not exempt from Fallow, premise caps, host-agnostic F1 cards) were not re-run, nor were the changes made while writing (the default Strand payoffs, the Grace fallbacks) or after the check (Strands cashed at floor entry, priced lit options, the re-authored catalogue rows, the band Interludes, the full R2 read on a second meeting). The consistency critic judged that the critic fixes do not move the measured figures; the lead judges the same of the writing changes and of the txtNew, txtFresh and near3 figures after the check, which keep every pool size and the toy's own counting of R2 as fresh. Priced lit options will move `OptionTakeRate` and `LitDominanceShare`, which the toy never measured. The simulator must confirm all of this before the gates bind.

---

## 9. Data, validator V16 and simulator (changes EV-38 to EV-40)

### 9.1 Records (change EV-40; M1 stub, M2 content)

```text
CuriosityRecord {
  id, form: curiosity|interlude|glimpse, tags[mech|rare], wave, chassis, propFamily,
  premiseTag: selfReference|lostObject|ritual|mechanism|residentWant|place,
  pools: [themeId | shared.shallows | shared.middle | shared.deep | shared.forecourt],
  floors[], hostKinds[alcove|sideRoom|q|forecourt|signature|wall], hostAgnostic: bool,
  registerNeutral: bool, domesticProp: bool,
  rarityClass: standard|rare,
  options[{ id, kind: priced|lit|rolled, labelId,
            costs[{kind, amount, payer: hero|anyMember|unit}],
            gains[{kind, param, target: referent|nextUnvisited|playerChoice|arenaPath|arena,
                   fallThrough[category…]}],        // exactly one rules gain (+ codexPageId in M3)
            graceTarget?: player|hero|worstStealth, graceFallbackGain?,
            referentKinds[]?, referentFallbackGain?,
            gate?{skill, thresholdByFloor[], who: best|worst|hero|actor, teachingRung?: bool},
            roll?{chancePermille, failCosts[]},
            setsStrand?{id, payoff{gain, target}, cashAt: floorEntry|coatHooks,
                        floorsRelative[+1], pilot?: bool} }],
  riderFallback?,                       // EV-11 not ruled (4.4.6)
  retellings[{textId, artId, words, twiceToldLineId}],   // text cards only (KR9)
  successorId?, sequel?{chainId, stage}, codexPageId?, toneFlags[], figureFrame?
}
Theme record += curiosityKit { curiosities[], interludes[], glimpses[] }
Run save   += curiosityDigest[floor] = {slot → (curiosityId, referentRef, optionId|left)},
              strands[{id, setOnFloor, payoff}],
              pendingProfile[] += PendingProfileWrite{curiosityLast}   // replayed to the profile on load (seeds and validation design 4.8.3; cross-document comparison 2026-11-23, CC-119)
Profile (MAIN 6.6 change) curiosityMemory {
  version, checksum,
  shownCount[id], fullShown[id] (bitmask of Retellings displayed in full; bit 1 = First Telling),
  recentRuns: ring[K]{runId, seedMode, shownIds[], f1BossId, f1ThemeId},   // f1BossId: BR-25, M2 (boss rooms design 4.10.3); f1ThemeId: R-SEED-6, M2 (cross-document comparison 2026-11-23, CC-6); seedMode here is not the seeded-run mark (runHistory[], seeds and validation design 4.7; SV-16)
  last[id]{optionId|left, targetRole, replacedBoon, litMet[], unaffordable[], runId},
  autoAnswer[id], afterwordSeen[], genericAfterwordLastRun[], pendingClosure[strandId],
  chainStage[]  // chainStage enters the unlock snapshot in M3 only
}
FactLedger (per floor, run state) { known[{category, targetId}], bossQueue[] }
Config keys (client values, not tuning): curiosity.fallowK = 3; curiosity.fallowFloors = [1]   // agreed, Q1
Config (tuning): curiosity.rarePermille; curiosityValueTable (4.4.9)
```

**(new lead's call)** `teachingRung`, `pilot`, `riderFallback` and the two client config keys are added while writing (4.3.2, 4.5.7, 4.4.6, 4.5.4). The draw map (4.10) is a segment 11 table, not record data.

**(lead's call, after the check)** The rest of the changes:
- `propFamily`, `registerNeutral`, `domesticProp`, gain `target` and `fallThrough`, `graceTarget`, `referentKinds` with `referentFallbackGain` (renamed from `bindingKinds`), and `setsStrand.cashAt` (4.4.2, 4.4.3, 4.5.5, 4.5.7, 4.5.11, 4.11).
- Per-Retelling `register` is dropped for `registerNeutral` (4.11).
- **`curiosityMemory` is restated** so its readers can be built:
  - `fallowSet[]` becomes `recentRuns`, a ring of the last K = 3 personal runs; **Fallow = the union of `recentRuns.shownIds`**;
  - `shown` becomes `shownCount`, and `retellingShown` is dropped for `fullShown` (4.5.2);
  - `lastOption` becomes `last`, which also keeps what the Twice-Told delta needs;
  - `pendingClosure` feeds the closure line; `version` and `checksum` detect a bad file, which then reads as empty (4.10).
- **`f1BossId` (BR-25; agreed, boss rooms Q1; M2):** written at run start from the pre-rolled F1 boss record for every launched personal run, abandoned or not; shared, daily and simulator runs never write it; outside the reproducibility key (boss rooms design 4.10.3).
- **`f1ThemeId` (M2; cross-document comparison 2026-11-23, CC-6):** written at run start for every launched personal run, beside `f1BossId`, and read by R-SEED-6's floor-1 theme redraw (each of the last three personal runs, `recentRuns[].f1ThemeId`; RS 6.4, MAIN 6.6, seeds and validation design 4.6); shared, daily, seeded and simulator runs never write it; outside the reproducibility key.
- **Which runs write memory.** Personal runs write every field. The `recentRuns` entry is pushed at run start (WO1, with `f1BossId` and `f1ThemeId`) and completed at run end or abandon (WO6); the run save carries the curiosity write as a `PendingProfileWrite{curiosityLast}` in `pendingProfile[]`, replacing the old `pendingMemory` (seeds and validation design 4.8.3) (cross-document comparison 2026-11-23, CC-119). Shared and daily runs write `shownCount` and `fullShown` only (their display may change; their draws never do) and never advance `recentRuns`. **Seeded runs (agreed, seeds and validation Q1; SV-16; M2):** every run whose seed the launcher did not draw writes only those seen counts plus a marked profile `runHistory[]` record (every finished run, personal or seeded, gets one, with `seedMode` from `SaveHeader.seedMode`), and advances no unlocks, records, codex entries or launcher memory; `recentRuns` and `f1BossId` are never written for a seeded run (V19t `SeededRunWritesSeenOnly`; seeds and validation design 4.7). The simulator writes **no player profile**; the launcher-dependent arms (the scripted-memory and streak arms, 9.4) write sandbox profiles in LauncherEmu only (SV-13; seeds and validation design 4.6, 6.1; M2). Replay runs write no player profile either.

### 9.2 V16 `Curiosities` (change EV-38)

**Phases and classes (SV-20; seeds and validation design 5.1, 5.2, 5.4):** checks carry qualified ids `V16.CheckName` and a phase (`s`, `g`, `t`); `s` and `t` parts fail the build, `g` parts act by their class: `V16g.FixedDrawCount` at GS3, `defect`; `V16g.SlotLegal` at AP2 before V15g, then the rest of V16g after V15g, each `repair` (the EventDraw and EventPlace fallbacks), then `defect`. Wave: M1 stub, M2, M3 Sequel and Ripening rows.

**V16s (static, M2):**
- `OutcomeShown`; `FreeLeave`; `OptionShape` (one rules gain per option, plus an M3 codex page; every option but Leave priced); `MechanicalDifference`.
- `LitIsSidegrade`; `LitHasPrice`; `LitNoExtraGain` (against `curiosityValueTable`).
- `PaletteWhitelist`; `StateBlind` (with the 4.4.7(a) whitelist, the FactLedger and Graces this floor included); `PaymentFeasibility`.
- `GraceShape` (C5, the `eventBoon` fields and the buff test); `GracePriceNotFree` (never free, never ticks alone, lit options included; replaces `GracePriceNotTicksOnly`); `GraceFallbackDeck` (the M2 deck with every Grace replaced by its fallback still passes `MechanicalDifference`, `LitIsSidegrade`, and `GainMix` at the fallback ceiling; new lead's call, ceiling after the check).
- `GainMix` (4.4.1's deck definition); `TextCaps`; `RegisterByFloor` (with `registerNeutral`).
- `DreamerLine` (with the premise test, 4.11); `HeartBedroomCap` (with `domesticProp`); `PremiseTagCap`.
- `ReservedCues` (no horn or bell props, no winding props on the Heart, and an audio-tag check against the RC7 and TL timbre tables); `ReservedSilhouette` (container, bait-cache, Windfall and Bedside Stand reads, container framing under `CuriosityLootBlind`, and Portal silhouettes and skins); `ThemeWrongDetail` (no running hands or ticking on any clock or watch act on any floor (4.11; cross-document comparison 2026-11-23, CC-120); the Kennel's painted moon; new lead's call, widened after the check); `StagingRC1` (KR16).
- `EventConditionsInThemeSix`; `F1Rules`; `HostAgnosticF1`; `F1HostAlways` (every F1 Blueprint offers a legal host for every F1 Curiosity pool, so a retried F1 keeps its forecast Curiosity; SV-30; M2); `TeachingRungCap` (≤ 1 rung-9 lit option per F1 theme pool; new lead's call).
- `RetellingParity`; `SuccessorParity` and `DeckValueParity` (against `curiosityValueTable`); `SequelGraphAcyclic` (M3).
- `PoolCoverage` per (floor, slot class), counted over eligible entries under the canonical snapshot; in M3 also under each Sequel and Ripening unlock snapshot. `PoolCoverage(interlude, F2|F3)` fails while any theme drawable there has an empty Interlude pool.
- `ForecourtGainFacesArena` (Interlude gains are arena-facing; F4–5 substitutive only, or marked exempt with a reason).
- `F1PoolParity` (agreed, Q2).
- `LitSpread` and `LitSpreadPerPool` (4.3.2); `F1DeltaSource` (4.5.2).
- `StrandPayoffNamed` (KR10; new lead's call); `ReferentFallbackPresent` (4.5.5).
- `NameCollision` (whole-word against RS 8.0's avoided list, boss signatures, Moods, lock, room and condition names).
- **Pending request (HL-19; M4):** the Curiosity Malady cost row (4.4.4) enters `PaletteWhitelist`, `PaymentFeasibility` (the payer and eligibility rows), `StateBlind` and `RegisterByFloor` (`registerLock`); V22 (k) adds `CuriosityMaladyOfferLegal` and `CuriosityMaladyPayerLegal` (heirlooms and curses design 4.3.3, 9.2).

**V16g (generation, M2):**
- `FixedDrawCount` (against the draw map, 4.10); `EventDrawRunBlind`.
- `WithinRunNoRepeat`; `FamilyCap` (prop family); `ChassisCap`; `NoiseCardCap`; `PremiseRunCap`; `SharedShareCap`; `SharedInterludeCap`.
- `SlotLegal`: never arena, Sanctum, Annex, Dare or Wager, elite, boxed-lurker host, F1 rooms 0–2 or a walkable square. Runs before V15g.
- `ReferentResolved` (every placed tied option has a referent or its fallback gain).
- `F1NoiseClearOfCircuit`.
- `SkinOnly`; `NoStepLockBypass`; `EventNeverTaxesHE`.
- `NoEventLoot`, an **ablation** (restated after the check): for a seed, the `lootPlan`, LootBind and spoils hashes are identical with segment 8's passes enabled and disabled, and identical before and after EventPlace; `NoEventUnit`; `GraphUnchanged`; `XpBandAfterEvents` (M3).
- `NoClockLandmark`; `NoFloor1Companion`; `GraceCapPerFloor`; `StrandCaps`.

**V16t (tests):**
- Determinism replay.
- `SnapshotCanonical` for shared, daily and simulator seeds.
- The LootDoorInvariant property test over door × memory.
- `DisplayMemoryInert`, codex included.
- `LauncherLootNeutral`; `LauncherBypass`; `ResolveIsSafePoint`; `CrashBetweenWrites` (a crash between the run-snapshot write and the profile write replays the profile write once, 4.10); `SeenAtDisplay` (a displayed, Left or crashed card advances `shownCount` once).
- `StrandSurvivesCut` (a carried Strand stays cashable under every cut-order step; replaces `OverlaySurvivesCut` after the check).

### 9.3 Reports (change EV-39; targets illustr.)

**Event payloads (lead's call, after the check).** The core emits, from M1 (stubs) and M2 (full):
- `CuriosityShown{runId, seedMode, floor, curiosityId, form, retellingK, presentation: firstTelling|full|twiceTold|twiceToldForced, availableOptionIds[], affordableOptionIds[], litMet[], deltaKind}`;
- `CuriosityResolved{runId, curiosityId, optionId|left, autoAnswered, readInFull, seconds, clicks, factResults[{category, targetId, fellThrough, disabled}]}`;
- `CuriosityAbsent{slot, reason: noHost|poolEmpty|capsExhausted, blueprintId}`; `CuriosityReferentFallback{curiosityId, optionId, referentKind}`;
- `GraceGranted{id, unit, replaced}`, `GraceEnded{id, reason}`;
- `StrandSet{id, floor}`, `StrandCashed{id, floor, cashAt, fellThrough}`, `StrandClosed{id}`.

**Definitions.** A *meeting* is a text-card meeting (0). Rates are per mille of the denominator named.

**Report path and result classes (SV-21, SV-23; seeds and validation design 6.1, 6.3, 6.12; M2):** every report below is computed from these payloads by the one report path, the same code for simulator and human sessions; `CuriosityFreshShare` and `StrandCashRate` run on LauncherEmu, `F1CuriosityPresence` on generation-only runs. Gate classes: `CuriosityFreshShare` and `StrandCashRate` **SHIP**; `EventDeathShareDelta` **SHIP** (Graces; it reads the DH 18.2 metrics, one of their three roles); `CuriositySeconds` and `CuriosityClicks` **BIND** (the EV-23 line: the 4.7 cut order fires before any CMP 7.4 lever); `GraceExpiryRate` **ALARM**; the rest **REP**. SHIP verdicts bind at the shipped cap after the cap decision.

| Report | Formula and gate |
|---|---|
| `CuriosityFreshShare` | Over `CuriosityShown` text-card meetings on the streak arm (launcher on): txtNew, txtFresh, near3 as defined in 0. Gate: txtFresh ≥ 0.95 at 10 runs, ≥ 0.90 at 30; near3 ≤ 0.05 (**gated, M2**) |
| `RunsToFirstRepeat`, `MostSeenCount`, `SharedInterludeMostSeen` | From `CuriosityShown`, per profile |
| `CuriositySeconds`, `CuriosityClicks` | Sums of `CuriosityResolved.seconds` and `.clicks` per floor visit, first-10-runs arm; against 4.7 (**gated**) |
| `LauncherRedrawRate` | Redraws per personal run start; ≤ 0.5 |
| `Floor1ThemeShare` | Each drawable F1 theme within 1/n ± 5 points |
| `F1CuriosityPresence` | Placed F1 Curiosities ÷ F1 floors generated, per Blueprint (`CuriosityAbsent` gives the reasons); = 1000‰ under V16s `F1HostAlways`, and a shortfall is a defect (SV-30) (cross-document comparison 2026-11-23, CC-117) |
| `ReferentAvailability` | `CuriosityReferentFallback` ÷ placed tied options, per Blueprint and referent kind |
| `OptionTakeRate` | Takes of an option ÷ displays where it was available and affordable; above 850‰ dominant, below 50‰ dead. Leave is reported separately (`LeaveRate`) |
| `LitDominanceShare` | Takes of a lit option ÷ displays where it was met and affordable; ≤ 850‰ |
| `ReflexTakeRate` (MECH) | `OptionTakeRate` of a MECH card's option on the streak arm; ≤ 950‰ |
| `LitMetRate` by party size | Displays with a lit option met ÷ displays; solo ≥ 60% of party 4 |
| `LitOptionSpread` | Per class at party 1 with creation defaults: displays with a lit option met ÷ displays; ≥ 500‰ |
| `EventDeathShareDelta` | Novice arm, events on minus events off: hero death share on F1 at party 1, and on F2 at party 1 and party 2; ≤ +0.5 pp (**gated**; gates Graces in M2, agreed, Q3) |
| `GraceValue` | Rest-adjacent arm, from `curiosityValueTable` and `dh.py` |
| `GraceExpiryRate` | `GraceEnded` with a reason other than `fired` or `replaced` ÷ `GraceGranted`; > 600‰ means re-price |
| `StrandCashRate` | `StrandCashed` with `fellThrough = false` ÷ `StrandSet`, streak arm; ≥ 400‰ (gated on the three pilot Strands; others report only) |
| `FactRedundancy` | `factResults` with `fellThrough` ÷ fact results |
| `FactLedgerExhaustion` | `factResults` with `disabled` ÷ fact options displayed |
| `TwiceToldSkipRate` | Twice-Told displays resolved in under 2 s without "Read in full" ÷ Twice-Told displays |
| `AutoAnswerRate` | `autoAnswered` ÷ Twice-Told displays where auto-answer was enabled |
| `AfterwordExhaustion` | Streak arm: run-start cards with no line, and the first run using a generic line (4.5.10) |
| `SightingsPerRunMinute` | Farm watch |
| `F1RareShare` | ≤ 1.1× base |
| `OvertureFightDelta` (M3) | — |

### 9.4 Simulator arms

- **Streak** (**200 profiles** dying on F1–2, over 10, 30 and 50 runs; was 10, which can never decide the gate; SV-25; seeds and validation design 6.5; M2). **Runs the launcher** and writes profile memory as a personal run (lead's call, after the check), in a **sandbox profile in LauncherEmu**, never a player profile (SV-13).
- **f1only**, **learner**, **average**, **novice**.
- **First-10-runs** time arm.
- **Rest-adjacent** policy arm.
- **Party 1, party 2 and party 4** (party 2 for the F2 gate).
- **Scripted profile memory** for the launcher (runs the launcher; sandbox profiles in LauncherEmu, SV-13).
- **Canonical and determinism runs** bypass the launcher and write no memory.
- **Graces off** (the M2 fallback deck), so the Q3 decision can be taken on measured figures **(new lead's call)**.
- **Qualified names (SV-22; seeds and validation design 6.2; M2):** the `History` axis (LauncherEmu only; profile-history models, never skill levels): `Streak` (the freshness gate's default), `F1only`, `Learner`, `Average`, `ScriptedMemory`, `First10Runs`; the `Walk` axis `EV.First10` (and this design's light, median and heavy walks under `EV.`); the `Rest` axis `RestAdjacent`; novice = `Skill.Novice`; party 1, 2 and 4 on the `Party` axis; Graces off on `Toggles`.
- **Pending request (from the seeds and validation design, 6.2; request to the XI, FL and EV owners):** walker equivalences, for example whether `XI.Completionist` equals `FL.Hunt` plus optional fights. Until this design's owner rules, there is no mapping: each gate reads its own owner's arm (seeds and validation design 14.2).

---

## 10. Staging by wave

| Wave | Contents |
|---|---|
| **M1** | Data stub only: `CuriosityRecord`; empty `curiosityKit`; the core emits the 9.3 event payloads as stubs (`CuriosityShown`, `CuriosityResolved`, `CuriosityAbsent`, `CuriosityReferentFallback`, the Grace and Strand events) |
| **M2** | Curiosities F1–3; arena-facing Interludes F2–3 (band and shared) and substitutive Interludes F4–5; Glimpses; the catalogue of 5 as re-authored after the check, with the Wake's kit authored at parity and drawn when its gate passes (agreed, Q2); priced lit thresholds; the FactLedger of fact instances; costs; the skin rule and priced unstepped lock routes; the presentation sequence, Twice-Told and First Telling; R2; `curiosityMemory`; Launcher Fallow (K = 3, F1 only; agreed, Q1); Referents with fallback gains; the M2 lean Strands cashed at floor entry and the Coat Hooks, with the three pilot Strands gated (4.5.7); the Afterword run-start line and closure line with the generic tier; V16s, V16g and V16t; reports. Graces and carried riders **only if** EV-11 is ruled and the gates pass (agreed, Q3); otherwise the fallback deck |
| **M3** | R3 everywhere (agreed, Q1); full Strands; Sequels advancing on sight (the Night Usher first; agreed, Q1); the Unwoken Roll; keeper lines; codex pages; Fragments folded into F-BOSS; Readings; F4 Curiosities in Twice-Told format if the line holds; Omen Retellings and Provisos; Overtures; Easing; the figure frame; Chute skins (if EV-36); M3 theme kits; Graces and riders if they missed M2; heirlooms never an "Item offered" price (HL-4; heirlooms and curses design 4.1.6) |
| **M3-late / M4** | Ripening (agreed, Q1); the Inverted Cathedral deck (M4) |
| **M4** (HL-29) | EV-43's shrine curse cures and curse bargains and the desecrated-shrine source ("Kneel / Pass by", the Malady printed), moved from Later (agreed, heirlooms and curses Q5); cure text generated from the hosts in the build; the Strand "a curse ended". Pending request (HL-19): the Curiosity "Malady" cost row and its V16s rows (heirlooms and curses design 4.2, 4.3.2, 4.3.3, 10) |
| **Later** | Pledge sources (Night Usher, shrines) and Testimony; the Hawker's Hour; coin owed (held, off). Curse cures and bargains moved to M4 (HL-29) |

No wave carries a least-seen draw inside the key (rejected, Q1).

---

## 11. What segment 8 owns, and hooks handed on

| Owner | What |
|---|---|
| **Segment 8** | Curiosity content and forms; their placement in Q rooms, Forecourts, F1 Alcove set pieces and side rooms, and signature rooms; the cost and gain palette; `curiosityValueTable`; the FactLedger rule; Graces (content), Strands, Retellings, Twice-Told, Fallow, Sequels, Ripening, Afterword and the Unwoken Roll; the segment 8 clock line; V16; the reports; Readings content (TL-34); Fragments content; guaranteed Riddle-clue hosts (4.13); the writing and Glimpse briefs (6); co-ownership of the codex's Curiosity pages |
| **Segment 9** | Seeded boss variables; the arena seal; the F-BOSS queue; poster Glimpses; floor-1 boss variety as a production priority (agreed, Q2); the RS Cut 1 question once carried for the client (14.2), now answered as graded (BR-26) (cross-document comparison 2026-11-23, CC-116). **Answered by the boss rooms design (segment 9, approved 2026-11-22; BR-3, BR-23 to BR-26, BR-29):** the seal is `ArenaEngaged`; F-BOSS facts from the Rendition and mirror in the `bossQueue` order; 3 Renditions per F1 boss and the graded RS Cut 1 (agreed, boss rooms Q1); the Ca7 poster Glimpse on a Carnival wall slot, Interludes keeping `AntechamberScreened` and the F5 Coat Hooks (boss rooms design 4.1, 4.2, 4.12) (cross-document comparison 2026-11-23, CC-120) |
| **Segment 10** | 4.12. **Answered by the exploration and information design (segment 10, approved 2026-11-23; XI-4, XI-6, XI-22, XI-27, XI-29):** facts and F-ROOM drawn per 4.3.6; chips read `PatrolForecast` (agreed, exploration and information Q2; cross-document comparison 2026-11-23, CC-120); `CardFit`; codex hosted (M3); F-TREASURE (M3) and beckoning Glimpses (request) |
| **Segments 11–12** | The stream, its draw map, pass order, safe point, memory field, launcher, V16 and arms. **Answered by the seeds and validation design (segments 11–12, approved 2026-11-23; SV-1, SV-2, SV-4, SV-6, SV-8, SV-9, SV-13, SV-16, SV-20 to SV-23, SV-25, SV-30, SV-31):** the `curiosities` `vector` key and draw map (4.10); keyed checks `h:curiosity` (4.3.2); the pipeline and AP2 (4.10); WO3 for the resolve write; seeded and simulator writes (9.1); V16 phases and classes (9.2); report classes (9.3); qualified arms and 200 streak profiles (9.4); pending request to this design's owner: walker equivalences (9.4) |
| **Camp and hub** | Afterword and keeper lines; Pledges; Testimony; the Hawker's Hour; Tithe Bowl dedup; coin owed. **Answered by the camp and hub design (segment 13, approved 2026-11-23; CH-12, CH-14):** the Afterword unchanged on the run-start card; keeper M2 service strings, M3 Strand lines only; the Unwoken Roll's hub home with the Sitter and `AfterwordNoEcho` (M3); Pledges and Testimony on Muster cards only; the Hawker's Hour a floor event; Tithe Bowl dedup unchanged; coin owed off; camp box and coat-display gates (6) (camp and hub design 2.2, 4.10, 4.15.6, 5.1, 6) |
| **Heirlooms and curses** | EV-43; the Strand "a curse ended"; "Item offered" exclusions. **Taken up by the heirlooms and curses design (segment 15, approved 2026-11-23; HL-4, HL-19, HL-29):** heirlooms never an "Item offered" price (M3); EV-43's shrine curse cures and bargains, the desecrated-shrine source and the Strand "a curse ended" moved to M4 (agreed, heirlooms and curses Q5); pending requests to this design's owner: the Curiosity Malady cost row with `payer`, its V16s rows and the shrine Malady sources (HL-19; agreed, heirlooms and curses Q4; M4) (heirlooms and curses design 4.1.6, 4.2, 4.3.2, 4.3.3, 10) |
| **Conditions owner** | The `eventBoon` form (agreed, Q3 makes M2 Graces depend on it), Weary's universality, Easing |
| **Enemies owner** | The noise reading, the FactLedger, Overtures |
| **EP owner (segment 6)** | The noise reading (EV-14, with the enemies owner); the EP 13 reading for Overtures (EV-32) |
| **Herald owner** | The EV-13 variant (M3). The Shade-lit Lure variant is dropped after the check (4.8), so no Shade request remains |
| **LE and 7c** | `stepFunded`, F-CACHE, Alcove occupancy |
| **FL** | Host tags; LT16; Chutes; floor-1 Blueprint variety as a production priority (agreed, Q2) |
| **RS** | Fallow, the profile field, the RS 9.1 readings, the figure frame, the codex owner; the Wake's gate and promotion at F1PoolParity (agreed, Q2); the cut lines, if the client ever reorders them (14.2) |

---

## 12. Binding requirements and how they are met

| Requirement | Met by |
|---|---|
| No extra VP, coin, XP or draws; LootDoorInvariant | Skin only; NoEventLoot; EventPlace after LootBind is draw-free; loot never reads Curiosities |
| No units; NoFloor1Companion | NoEventUnit; noise moves units only through the hearing rules; Pledges Later |
| No hidden pity (DH 5.4(f)); no mercy (agreed, Q4) | StateBlind; PaymentFeasibility split; SuccessorParity; the launcher reads display memory only; codex never read in-run |
| DH healing closed; F1 deaths < 5% | No heals; Easing M3 via the DH owner; Graces end at the seal; death-share gates |
| RC1–RC9; RS 7.5 | Chips from data; figure rules; reserved cues and silhouettes; DreamerLine; HeartBedroomCap; PremiseTagCap |
| Combat is key; clock | About one decision per floor; F4–5 net ≤ 0; self-cutting order; no new CMP 7.4 lever |
| Determinism and shared seeds | EventDraw run-blind with a fixed draw map; display memory outside the key; canonical snapshots; launcher bypass; RS 9.1 closed (agreed, Q1) |
| Check charter | Thresholds; party rule; printed odds; one attempt |
| The client's repetition concern | Section 0.1 devices (agreed, Q1); floor-1 variety in parallel (agreed, Q2); streak gates in 9.3 |
| Change discipline | EV-1 to EV-45 with exact sections; ⚑ on decided, agreed and settled rows |

---

## 13. Changes to approved documents

Applied on 2026-11-22, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; **the client's approval of 2026-11-22 covers them** (with Q1–Q4 where a row names one). As in the LE, TL, EP, IR and LP precedents, the Flag column also marks rows that only *read* agreed, decided or settled text without changing it, so that every touch of such text is visible **(new lead's call:** this adds flags to EV-3, EV-7, EV-15, EV-18, EV-22, EV-25, EV-26 and EV-44, which the proposal's register did not flag; **after the check** it also adds them to EV-4, EV-6, EV-12, EV-14, EV-17 and EV-45, which the written register had missed**)**. Told-plainly rows were told in 14.1 and confirmed by the client on 2026-11-22. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; each goes to its owner for confirmation):**
- The F1 Forecourt's walls may carry Glimpses under RS 9.4 and FL 6.11's "empty" (a Glimpse has no rules, no square and no click; RS and FL owners).
- Curiosity noise is generic noise *here* under EP 13 and ENM 12.3, never a Lure (enemies and EP owners).
- A single-actor manipulation uses the acting unit's Stealth, under the Steal precedent (STAT owner).
- Leadership's "event outcomes" (STAT 7.1) are one hero-only threshold on one option (STAT owner).
- Resolving a Curiosity is a safe point under MAIN 6.6 (main design owner).
- Sequels and Ripening enter `drawableSet` through the unlock snapshot, a reading of RS 9.1's rejected row, not cross-run weighting inside the seed (RS owner; agreed, Q1 for the waves).
- The Wake's promotion reads RS 8.5 "M2 conditional, else the first M3 unlock" unchanged (RS owner; agreed, Q2).
- DH 10.4's 1 HP floor covers incidental damage; a printed HP price is a separate payment rule (DH owner).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| EV-1 | New vocabulary | Terms: Curiosity, Interlude, Glimpse, MECH, rare, Grace, Strand, Retelling, Twice-Told, Fallow, Sequel, Ripening, Afterword, figure frame (4.1) | — | Lead's call | M2 |
| **EV-2** | FL 6.5 "Empty or special" row; FL 6.9; FL 13 row 8; FL LT16; LP-11 (pending, FL 6.x leaf note); RS 6.6; reads **RS 9.4 and FL 6.11 (agreed)**; LG5 (read) | Budget and hosts; F1 host order (Alcove set piece, then the lowest-priority side room); the `curiosityHost` rule; the draw-free tie-break; Q room 3 wall Glimpse only; F1 Forecourt wall Glimpses as a reading of "empty" (4.2) | ⚑ **Agreed** (RS 9.4, FL 6.11, read) | Lead's call; FL, RS, 7c owners apply; segment 3 (request) | M2 |
| EV-3 | RS 8.2(f); RT-20; TL-19 analogue; reads **DH 18.2 (agreed)**; DH 21.1; CMP 7.1; ITEMS 13 | Floor-1 rules (4.6) | ⚑ **Agreed** (DH 18.2, read) | Lead's call; DH owner applies | M2 |
| **EV-4** | **RS 7.4 RC1, RC7 (agreed, read)**; TL 6.1; **STAT 7.2 (agreed, read)**; ENM 14.6 | Card shape; every option but Leave priced; Leave closes without resolving; chip layout budget; `LitIsSidegrade`, `LitHasPrice`, `LitNoExtraGain` (4.3.1) | ⚑ **Agreed** (RS 7.4, STAT 7.2, read) | Lead's call | M2 |
| **EV-5** | **STAT 5.4 (agreed)**; STAT 6; TL 6.1, 6.2, 6.12; TL-34 | Checks; accepts TL-34 for options; movement Stealth by the worst member; single-actor Stealth by the acting unit (reading); the F1 teaching rung (4.3.2) | ⚑ **Agreed** (STAT 5.4) | Lead's call; stat and TL owners apply | M2 |
| **EV-6** | **STAT 7.1 Leadership row (agreed, read)**; STAT 2 non-goals | Leadership "event outcomes" = one hero-only threshold; no dialogue (4.3.2); a flagged reading | ⚑ **Agreed** (STAT 7.1, read) | Lead's call; stat owner applies | M2 |
| EV-7 | LE 6.1; LE-14; LP 4.13; RS 6.4; ENM 14.6, 15.8, 16; LE 6.9 (exposure); CT2; DH 12.1; reads **MAIN 4.7 (decided)** | Gains whitelist; F-LISTEN and F-CACHE families; `GainMix`; run-specific F-BOSS (4.4.1) | ⚑ **Decided** (MAIN 4.7, read); ⚑ **Settled** (LE 6.1, LP 4.13, read) | Lead's call; LE, 7c, enemies owners; segment 9 (request) | M2 |
| EV-8 | ENM 14.6; HX-6; TL-23; LE-18; LE 6.9 item 6; ENM 15.8; CMP 7.12; LE 8.1 Tithe Bowl | One per-floor FactLedger; codex write-only (4.4.2) | — | Lead's call; enemies, LE owners apply | M2 |
| **EV-9** | **DH 10.4 (agreed)** (a reading: price floor ≠ incidental floor); C14; MAIN 4.3; ITEMS event reasons; LE-4 (not reused) | Costs: bound ticks; noise here; hero HP with `PaymentFeasibility`; Weary; theme conditions; `ItemDestroyed{offered}`; no supply, no coin (4.4.4) | ⚑ **Agreed** (DH 10.4) | Lead's call; DH, items owners apply | M2 |
| **EV-10** | **DH 5.4(f) (agreed)**; DH 2; MAIN 2, **4.7 (decided)** | StateBlind split into value predicates and payment feasibility; no mercy through events (4.4.7) | ⚑ **Agreed** (DH 5.4(f)); ⚑ **Decided** (MAIN 4.7, read); **Q4** | **Client (Q4, agreed 2026-11-22)**, with this document; DH owner applies | M2 |
| EV-11 | Conditions 13 (C5 bullet); conditions 17 (duration vocabulary); conditions pipeline step 4 | Deferred duration form `eventBoon{payload, trigger, endsOn}`, with the arena seal processed before combat start; carried riders in a separate `eventRider` carrier outside the C5 slot; Warded does not block cost-sourced instances (4.4.3, 4.4.6) | **Q3** | **Client (Q3, agreed 2026-11-22)** for the wave rule; conditions owner rules (request) | M2 if ruled, else M3 |
| **EV-12** | C5; conditions 13, 16; DH 15; **DH 18.2 (agreed, read)**; DH 21.1 | Graces: single unit; `graceTarget`; 1 per floor; end at the arena seal; replacement named per C5; never free and never priced in ticks alone; conversion at cap; fallback deck if M3, with its own `GainMix` ceiling; gates on F1 at party 1 and F2 at parties 1 and 2 (4.4.3) | ⚑ **Agreed** (DH 18.2, read); **Q3** | **Client (Q3, agreed 2026-11-22)**; conditions, DH owners apply | M2 gated / M3 |
| EV-13 | HR-22; ENM 14.8; Herald 9.1 table | No Cow Grace; Herald-lit "Show of Force costs no slot this floor" offered (4.8) | — | Lead's call; Herald, enemies owners (request) | M3 if ruled |
| **EV-14** | **EP 13 "no event spawns or moves units" (settled, read)**; ENM 12.3; SH NM-4 and SH-21 (read); CL-10 | Curiosity noise is generic noise here (EP 13 reading); never a Lure, because Lure is a universal exploration action furniture would duplicate (BC7); never sets the group flag `luredThisFloor`; `F1NoiseClearOfCircuit`. The proposal's Shade-lit Lure variant is dropped after the check (4.4.4, 4.8) | ⚑ **Settled** (EP 13, read) | Lead's call; enemies owner applies; EP owner (reading) | M2 |
| EV-15 | LP 4.13; LP 4.2; LE 6.9; TL 6.11 / CC4; TL-28; DH 12.4; LE-13 | `CuriosityLootBlind`; priced extra route only to unstepped, non-HE, non-Due, non-boxed ordinary locks; `stepFunded` flag (4.4.5) | ⚑ **Settled** (LP 4.13, LP 4.2, read) | Lead's call; LE, 7c, TL owners (`stepFunded` a request) | M2 |
| EV-16 | — (no approved text) | Within-run caps defined (prop family, chassis, noise card; caps count draws); premise run cap; shared shares as per-slot pool probabilities; shared Interlude caps; empty slots logged (4.2, 4.5.11) | — | Lead's call | M2 |
| **EV-17** | **MAIN 2 anti-goal (decided, read)**; **RC7 (agreed, read)** | The presentation sequence; Twice-Told card (delta first, one line by priority); auto-answer limits; First Telling; first sight in Twice-Told format (4.5.2) | ⚑ **Decided** (MAIN 2, read); ⚑ **Agreed** (RC7, read); **Q1** | **Client (Q1, agreed 2026-11-22)**; segment 10 | M2 |
| **EV-18** | RS 6.4; reads **R-SEED-5 (settled)** | Retellings display-only, outside the key; R2 in M2, R3 in M3; `RetellingParity`; `DisplayMemoryInert` (4.5.3) | ⚑ **Settled** (R-SEED-5, read); **Q1** | **Client (Q1, agreed 2026-11-22)**; RS owner applies | M2 / M3 |
| **EV-19** | **MAIN 6.6 "Profile save" bullet (decided)**; **MAIN 9 M3 "Profile" row (decided; a read wave pull, on the R-SEED-6 M2 precedent)** | Profile field `curiosityMemory`, restated after the check (version, checksum, `recentRuns`, `fullShown`, `last`, `pendingClosure`; which runs write it) (4.5.1, 4.10, 9.1) | ⚑ **Decided** (MAIN 6.6; MAIN 9 staging, a wave pull); **Q1** | **Client (Q1, agreed 2026-11-22)**, with this document; main design and RS owners apply | M2 |
| **EV-20** | **RS 6.4 R-SEED-6 (settled lead's call)**; R-SEED-2 (`Floor1ClassSpread` notes); **RS 12 M3 row "profile-side anti-streak outside seeded runs"** | Launcher Fallow: add "or if the forecast F1 Curiosity is in the profile's Fallow set (K = 3)" to R-SEED-6; shared budget; per-candidate order (theme rule, Fallow, onboarding as a score); on exhaustion the best candidate, or the original seed if none passed the theme rule (a reading for the RS owner); forecast notes; onboarding preference; `Floor1ThemeShare` at 1/n; pull RS 12's M3 row into M2 (4.5.4) | ⚑ **Settled** (R-SEED-6; RS 12 staging row, a wave pull); **Q1** | **Client (Q1, agreed 2026-11-22)**, with this document; RS owner applies | M2 |
| EV-21 | Segment 11 tree; RS 6.4; R-SEED-3; LP 4.13 (V15g order); LP 2.1 and LE 6.10 item 4 (the approved pass order, read); MAIN 6.6 "safe points" (reading) | `curiosities` stream; the draw map with hash labels; EventDraw's in-slot order and fallbacks; EventPlace after LootBind; fixed draws; checks keying; **resolving is a safe point**, with a fixed write order (4.10) | ⚑ **Decided** (MAIN 6.6, read) | Lead's call; segment 11, main design owner | M2 |
| EV-22 | Reads RS 6.4 inputs and **LootDoorInvariant (settled)**; FL LG1 (the Forecourt is room n−2, read) | Strands cashed on entering the next floor or at the Coat Hooks; oldest first; F2 payoffs at floor entry; closure line; M2 lean definition; pilot Strands; cuts never strand a payoff (4.5.7) | ⚑ **Settled** (LootDoorInvariant, read) | Lead's call; RS owner | M2 lean / M3 |
| **EV-23** | FL 6.10; FL-16; reads **CMP 7.4 (agreed; no lever)** | Segment 8 clock line and cut order; F4–5 Interludes from substitutive families only (4.7) | ⚑ **Agreed** (CMP 7.4, read) | Lead's call; FL owner applies | M2 |
| **EV-24** | Reads **RS 7.3 pillar 4 (agreed; not changed)** | Register kept; punchlines in Retellings and Glimpses; drafts' "deadpan reading" withdrawn (4.11) | ⚑ **Agreed** (RS 7.3, read) | Lead's call (told plainly) | M2 |
| EV-25 | RS 7.4 RC1, RC2, RC3, RC5, RC7, RC9; reads **RS 7.5 (agreed)**, 7.6, risk 14; RS 8.4, 8.5, 8.11 Look rows; LP-14, LP-24 | Tone guards: premise tags and the premise test; `HeartBedroomCap` with `domesticProp`; reserved cues (audio-tag check) and silhouettes, Portal silhouettes and container reads; staging; register-neutral straddling cards; wrong details kept, including the Wake's stopped clocks (4.5.6, 4.11) | ⚑ **Agreed** (RS 7.4, RS 7.5, read) | Lead's call; RS owner applies | M2 |
| EV-26 | RS 6.8 keeper (display); reads **RS 7.5 (agreed)**; pillar 2; DH 17.12; LE-15 | Afterword (M2 lean defined); Unwoken Roll; memorial tone rules (4.5.10) | ⚑ **Agreed** (RS 7.5, read); **Q1** | **Client (Q1, agreed 2026-11-22)** for the memory model; RS owner, camp and hub | M2 lean / M3 |
| **EV-27** | **RS 9.1 table, rejected row "cross-run weighting inside the seed" (agreed)**; R-SEED-5; MAIN 4.7 | Sequels through `drawableSet` on sight: **a flagged reading of RS 9.1's rejected row**; dedicated rarity draw; `SuccessorParity`; canonical seeds (4.5.8) | ⚑ **Agreed** (RS 9.1, read); ⚑ **Decided** (MAIN 4.7, read); **Q1** | **Client (Q1, agreed 2026-11-22)**; RS owner confirms the reading | M3 |
| **EV-28** | As EV-27 | Ripening, same reading and parity (4.5.9) | ⚑ As EV-27; **Q1** | **Client (Q1, agreed 2026-11-22: M3-late)**; RS owner | M3-late / M4 |
| EV-29 | RS 9.1 Fragments row; CF-M6; LE 6.12; LE-18 | Fragments folded into F-BOSS options (4.9) | — | Lead's call; RS owner | M3 |
| EV-30 | TL 6.12; STAT 6 | Readings content (TL-34 accepted) (4.9) | — | Lead's call; TL owner | M3 |
| EV-31 | HZ 6.9; LE 6.12; RS 9.1 | Omen Retellings; Nightmare Provisos; Mood steps (4.9) | — | Lead's call; HZ, RS owners | M3 |
| EV-32 | EP 13 (reading); conditions entries | Overtures with exclusions (4.9) | — | Lead's call; enemies owner (request) | M3 |
| EV-33 | DH 5.7, 12.1; C14; HZ-30 precedent | Easing (priced Weary or rest-tick relief) (4.9) | — | Lead's call; DH, conditions owners (request) | M3 |
| EV-34 | Conditions 16 (beside RT-16) | Event-sourced conditions count toward the theme's 6; Weary universal or not; optional "kept through the next short rest" Weary (4.4.3, 4.4.4) | — | Conditions owner rules (request) | M2 ruling |
| EV-35 | HZ 13; LP 4.13; FL 13 | Segment 8 places no Landmarks (FL owns the sockets, segment 4 the ticks) (4.2) | — | Lead's call | M2 |
| EV-36 | FL M3 Chute; HZ-23 (LG14 exception, pending); ENM 20.1; LE 6.9 | Bypass routes only as skins of FL-generated Chutes; `GraphUnchanged`; `XpBandAfterEvents`; waits on HZ-23 (4.4.1) | — | Lead's call; FL, enemies owners | M3 if HZ-23 |
| EV-37 | RS 12 | Codex Curiosity pages; RS owns the codex, segment 10 hosts it (4.9) | — | Lead's call; RS owner (request) | M3 |
| EV-38 | Validator list (RS 11.2) | V16 `Curiosities` (9.2) | — | Lead's call; segment 12; RS owner applies | M2 / M3 |
| EV-39 | Segment 12 | Reports and arms (9.3, 9.4) | — | Lead's call | M2 / M3 |
| EV-40 | RS 8.0 theme record | Records and save fields (9.1) | — | Lead's call; RS owner, segment 11 | M1 / M2 |
| EV-41 | CMP 7.12; EP-39 | Pledge sources hosted by segment 8; Testimony (4.8) | — | Lead's call; companions owner (request) | Later |
| EV-42 | ITEMS 10.9; LP 4.7.12 | The Hawker's Hour as a skin of the ITEMS 10.9 trader | — | Lead's call; items owner, camp and hub | Later |
| EV-43 | ITEMS 13; C14; LE 8.5; IR 4.15 | Shrine curse cures and curse bargains | — | Lead's call; conditions, items owners | **M4** (was Later; HL-29, agreed, heirlooms and curses Q5; heirlooms and curses design 4.2, 10) |
| EV-44 | Reads **RS 8.2, 8.5 (agreed, RS Q4 catalogue)**; R-SEED-6 | The Wake's promotion: **read, not changed** (RS 8.5 already says "M2 conditional, else the first M3 unlock"); `F1PoolParity`; floor-1 Blueprint and boss variety recorded as a production priority to the RS, FL and segment 9 owners, changing none of their text (0.1, 4.6) | ⚑ **Agreed** (RS 8.5, read); **Q2** | **Client (Q2, agreed 2026-11-22)**; RS, FL, segment 9 owners (request) | M2 |
| **EV-45** | **RS 7.4 RC1, RC1+, RC2, RC3 (agreed, read)**; EP 13; ITEMS 10.9; the role-ID gate | Figure frame with its own non-unit grammar; M2 implied figures only, without faction motifs, under the role-ID gate (4.11) | ⚑ **Agreed** (RS 7.4, read) | Lead's call; RS owner, art | M2 (implied figures) / M3 (frame) |

Waves: EV-40's stub in M1; EV-1 to EV-10, EV-14 to EV-25, EV-26 (M2 lean), EV-34, EV-35, EV-38 to EV-40, EV-44 and EV-45 (implied figures) in M2 (EV-11 and EV-12 in M2 only if ruled and gated, agreed, Q3); EV-13, EV-26 (full), EV-27, EV-29 to EV-33, EV-36, EV-37 and EV-45 (the figure frame) in M3; EV-28 in M3-late or M4; EV-43 in M4 (HL-29); EV-41 and EV-42 Later.

---

## 14. Open items for the client

None. Approved by the client on 2026-11-22.

The client approved this document as a whole on 2026-11-22, including every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the lead's calls after the check in 14.1), the told-plainly items (including "Told plainly after the check" in 14.1: among them the re-authored catalogue, the 50% ceiling on the Graces-off fallback deck, Strands paying on entering the next floor, the priced lit options, the M2 budget of about 11–12k words and 400–450 art states, the full read on a second meeting with a card, RS Cut 1 still cutting floor-1 bosses first, help in the visible ladder being a future decision, and F2 band cards being dry in text), the readings recorded in 13, and its approval covers the flagged rows of 13. The four questions raised before writing were answered on 2026-11-22 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. Writing raised no new client question, and neither did the check: every lead's call after the check sits inside the four answers, keeps an approved rule, or is flagged against the text it touches. The client did not choose to reorder RS 12's Cut 1 now: the F1-boss cut-order question is carried to segment 9 (boss rooms) as a future item for the client, with no RS change now (14.2). That question is now answered: Cut 1 is graded (agreed, boss rooms Q1, 2026-11-22; BR-26; 14.2) (cross-document comparison 2026-11-23, CC-116). The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them, and the naming pass remain future items.

### 14.1 This document

**The four answers (agreed, 2026-11-22), each the recommended option:**

- **Q1, cross-run memory (EV-17 to EV-20, EV-26 to EV-28):** option A. M2 seed-pure; display-only Retellings (R2 in M2, R3 in M3) outside the reproducibility key; the Twice-Told card; Launcher Fallow (K = 3) on the F1 Curiosity only, inside R-SEED-6's budget; M3 adds R3 and Sequels advancing on sight; Ripening M3-late; RS 9.1 stays closed. Rejected: B, a least-seen draw inside the key in M3; C, no memory.
- **Q2, floor-1 variety in parallel (EV-44):** option A. Promote the Endless Wake into the M2 F1 pool when its gate passes, at F1PoolParity; prioritise F1 Blueprint and boss variety, a production priority flagged to the RS, FL and segment 9 owners. Rejected: B, events only.
- **Q3, Graces (EV-11, EV-12):** option A. M2 only if the conditions owner rules the `eventBoon` form and the F1/F2 death-share gates pass; otherwise M3. Rejected: B, M3 regardless.
- **Q4, no mercy (EV-10):** option A confirmed. Losing streaks get novelty and memory relief only, never easier odds or richer outcomes (DH 5.4(f) **(agreed)**; MAIN 4.7 **(decided)**); difficulty help belongs in the visible ladder. Rejected: B, visible opt-in streak help through events.

**New lead's calls (made while writing; confirmed by the client on 2026-11-22, with this document):**
1. **Document shape:** the proposal's section numbers 0–13 are kept so every cross-reference holds; intent, goals, non-goals and anti-goals sit in 0 as 0.4; the client questions become 14 (open items); the closing parts follow as 15 (risks), 16 (left to the builders), 17 (depends on) and Appendix B (sources), as in LP.
2. **Name re-check:** "Seating Chart" and "Last Page" hit the Supper's and the Silk Archive's own Obsession prose and are kept as theme callbacks; "strands" hits only as an ordinary verb (header).
3. **K = 3 and F1-only Fallow are client keys, not tuning** (`curiosity.fallowK`, `curiosity.fallowFloors`); a gate miss that would move them returns to the client (header, 9.1, 14.2).
4. **"M2 lean" is defined** for Strands and the Afterword (4.5.7, 4.5.10).
5. **The F1 teaching rung 9** replaces the proposal's "9 for MECH lit lines", which contradicted KR3; at most one rung-9 lit option per F1 theme pool, the Kennel's K2 in M2 (`TeachingRungCap`) (4.3.2, 5.3).
6. **Shared pools read the threshold ladder of the floor they are drawn on**; D2's missing number is filled as 12 on F1, 13 on F2 (4.3.2, 5.3).
7. **The Grace fallback deck:** if Graces wait for M3, every Grace option ships with its `graceFallbackGain` at the same price; `GraceFallbackDeck` validates it; a "Graces off" simulator arm informs the Q3 decision (4.4.3, 9.2, 9.4).
8. **Rider fallbacks** if EV-11 is not ruled: C2 priced hero 3 HP; Ca4 to M3, so the Carnival ships one MECH card in M2 (4.4.6, 5.2).
9. **First sight in Twice-Told format** is defined (R1 Twice-Told line, "First time here", no footer; counts as seen; the diorama waits) (4.5.2).
10. **Pilot Strands:** *Signed In* (Gaol), *Collared* (Kennel) and *In Mourning* (Wake) carry the `StrandCashRate` gate; other Strands report only (4.5.7, 9.3).
11. **Default Strand payoffs** for the eight Strands the proposal listed without one: *Time Already Served* (F2: F-KEY), *In Mourning* (F2: F-TRAP), *Rehearsed* (F2: F-ROOM), *Shook Hands* (F2: F-LISTEN), *The Silence Held* (next floor: F-SECRET), *In the Book* (next floor: F-ROOM), *Shut Behind You* (next floor: F-TRAP), *Sat Down* (Coat Hooks: arena-path F-TRAP) (5.3; `StrandPayoffNamed`).
12. **Overlay-only Interludes** survive cut-order steps 1 and 3 (and keep the Coat Hooks on F5), so a clock cut never strands a payoff (4.5.7, 4.7; `OverlaySurvivesCut`). **Superseded after the check:** Strands now cash at floor entry, which no cut touches, and the Coat Hooks survive step 1 (`StrandSurvivesCut`; after-the-check call 6).
13. **The draw map:** one draw per slot as a sub-seed, the rarity roll and the whole-catalogue permutation derived from it by keyed hash; draws 1–4 named, 5–12 dummies, consumed on every floor. This reconciles the proposal's "fixed 12 draws" with its Fisher–Yates permutation and makes `F1MajorForecastable` exact (4.10).
14. **Start the Clocks (W5) never runs a clock:** the hands shudder and settle back, keeping RS 8.5's wrong detail; `ThemeWrongDetail` covers running Wake clocks (4.11, 5.3).
15. **Option labels normalised:** W6 and Gu3 had their lit option listed first; the labels are swapped so (a) is always priced, with no change of content (KR13).
16. **Read flags in 13:** EV-3, EV-7, EV-15, EV-18, EV-22, EV-25, EV-26 and EV-44 are flagged for reading agreed, decided or settled text (13); after the check, EV-4, EV-6, EV-12, EV-14, EV-17 and EV-45 too (after-the-check call 22).
17. **Critic ids normalised:** the proposal's partial "BC… S2-5"-style cross-references are written as their Appendix A.2 ids (BC6, BC9, BC12) (4.4.5, 4.5.4, 4.5.7, 4.8, 4.10).
18. **Owner-refusal fallbacks** in 14.2, each stated so a refusal never blocks M2.

**Told plainly (confirmed by the client on 2026-11-22):**
1. The hot spot is F1 and theme frequency. Fights and floor-1 themes repeat more than events do (0.1).
2. "Done properly" does not mean more events (0.1).
3. Display-only Retellings sit outside the key, against three drafts (4.5.3).
4. RS 9.1 stays closed, against Models A and B's in-key memory (agreed, Q1).
5. Sequels and Ripening are flagged *readings* of RS 9.1, not automatically legal (4.5.8, 4.5.9, 13).
6. The register is kept, and the "deadpan reading" is withdrawn (4.11).
7. No event healing; no supply costs; no coin (4.4).
8. Cow advantage and Lures are removed from rewards (4.8).
9. Self-reference premises are capped (4.5.6).
10. The Heart's bedroom imagery is capped, because its approved child scale already sits near RS 7.5's "age" (4.11).
11. Model C's M3 Captive is declined (4.8).
12. The toy was not re-run after the critic fixes or the writing changes (8).
13. Coin owed at camp is held, off (4.4.4, 7).
14. **(Added while writing.)** The proposal's "9 for MECH lit lines" contradicted its own KR3 and is corrected (14.1 call 5).
15. **(Added while writing.)** The proposal's cut order would have stranded every F1 Strand payoff on F2; overlay-only Interludes closed that gap (14.1 call 12), and floor-entry cashing now closes it for good (after the check).
16. **(Added while writing.)** "Start the Clocks" as written broke the Wake's approved wrong detail (14.1 call 14).
17. **(Added while writing.)** If Graces miss M2, the M2 decks lean further on facts; `GainMix` and `OptionTakeRate` are the watch (4.4.3, risk 1).

**Lead's calls after the check (confirmed by the client on 2026-11-22, with this document; none changes a client answer):**
1. **Interlude coverage:** each band theme gets 2 F2–3 Forecourt Interludes (6 near-wordless cards); the shared share is a per-slot pool probability (≤ 400‰ for Curiosities, ≤ 200‰ for Interludes); an empty slot is logged as `CuriosityAbsent` and shows a Glimpse; `PoolCoverage(interlude, floor)` (4.2, 5.3).
2. **Forecourt Interludes face the arena:** F-BOSS (F2–3 only), F-LISTEN on the arena, arena-path F-TRAP, arena F-ROOM; on F4–5 only the substitutive three, each row marked; Gu-I3, H1, H5 and H6 re-authored (4.1, 4.7, 5.3; `ForecourtGainFacesArena`).
3. **`GainMix` defined and met:** a deck is a theme's Curiosity cards or a shared Curiosity pool, Leave and Interludes excluded; ≤ 40% fact-only, ≤ 50% on the Graces-off fallback deck; the catalogue re-authored to meet both, with per-pool counts in 5.2 (4.4.1).
4. **One rules gain per option**, plus an M3 codex page; the double-gain rows split (G1, G9, W6, C7, Su4) (4.4.1, 5.3).
5. **Every option but Leave is priced;** lit options pay a price that shares no cost kind with the priced option (`LitHasPrice`); pure cost waivers are gone; `LitDominanceShare` at 850‰ (4.3.1).
6. **Strands cash on entering the next floor** (0 clicks), or at the Coat Hooks on F5, which replace the drawn Interlude; oldest first; floor-entry cashes count toward the cap; Strand-setting options disabled while 2 are carried; `StrandCashRate` counts only new facts (4.5.7).
7. **Grace prices:** never free and never ticks alone, lit options included; a theme condition or rider is an allowed price; D3, D8, W6 and Ca2 re-priced (`GracePriceNotFree`; 4.4.3, 5.3).
8. **`eventBoon` specified** (triggers, `endsOn`, the arena seal before combat start), riders in a separate `eventRider` carrier, `graceTarget`, and the Grace events (4.4.3, 4.4.6; EV-11).
9. **Graces gate at the party sizes that exist:** F1 at party 1, F2 at parties 1 and 2 (4.4.3, 9.3, 14.2).
10. **The FactLedger holds fact instances,** with `fallThrough` lists and draw-free target selectors; the ledger and Graces this floor join `StateBlind`'s whitelist (4.4.2, 4.4.7).
11. **Referents** (renamed from "Bindings", which EP uses): draw-free choice by the lowest main-path index then room id; a fallback gain for every referent kind; `ReferentResolved`, `ReferentAvailability` (4.5.5).
12. **Host tie-break and `curiosityHost`:** lowest main-path index, then room id; a `curiosityHost` signature room is always preferred (4.2).
13. **EventDraw's order inside a slot,** with hash labels, fallbacks and empty slots; the pass order aligned with LP 2.1 and LE 6.10; the Sequel draw on F2 only (4.10, 4.5.8).
14. **Seen is written at display;** Leave closes without resolving and can be reopened; the resolve write order and `CrashBetweenWrites` (4.3.1, 4.5.1, 4.10).
15. **`curiosityMemory` restated** (version, checksum, `recentRuns`, `fullShown`, `last`, `pendingClosure`) and which runs write it (9.1).
16. **The presentation sequence:** each Retelling is read in full once, then the Twice-Told card; display reads a run-start snapshot; one delta line by priority; an authored delta source on every standard F1 card; txtFresh counts full displays only (4.5.2, 0).
17. **Launcher order** per candidate and on exhaustion; the forecast ignores host presence; `Floor1ThemeShare` at 1/n ± 5 (4.5.4).
18. **Within-run caps defined:** `propFamily`, noise card, caps counting draws, exhaustion (4.5.11).
19. **`curiosityValueTable`** for every value check (4.4.9).
20. **Report payloads and formulas;** the streak and scripted-memory arms run the launcher; party 2 added (9.3, 9.4).
21. **`NoEventLoot` as an ablation;** `PoolCoverage`'s memory case limited to M3 unlock snapshots (9.2).
22. **Flags added** to EV-4, EV-6, EV-12, EV-14, EV-17 and EV-45; EV-19 also reads MAIN 9's M3 "Profile" row as a wave pull (13).
23. **Lure re-grounded:** Lure is a universal exploration action and `luredThisFloor` a group flag; the Shade-lit Lure variant is dropped (2.1, 4.8; EV-14).
24. **Afterword selection and budget:** keyed lines in priority, a recurring generic tier, at most one Afterword and one closure line, never silence; `AfterwordExhaustion` (4.5.10).
25. **Lit spread on creation defaults** (Medicine is every class's default); `LitSpreadPerPool`: every class meets at least 3 of 6 standard F1 cards (4.3.2, 5.3).
26. **Register-neutral straddling cards:** band, Middle and straddling Interludes are written in the driest register of their floors; the F2 comic beat lives in F2 Glimpse art (4.11).
27. **Tone and readability rules:** the premise test; `domesticProp`; no winding props on the Heart; no Portal silhouettes or skins; container-type props drawn not to read as containers; implied figures without faction motifs; the figure frame's own grammar; staging (KR16); the chip layout budget (4.3.1, 4.11).
28. **Catalogue re-skins and renames:** H1 The Skirting Board (for The Turning Key), H2 The High Window, H6 The Pinned Notice, the Printed Itinerary moved to the Middle pool, D1 franked by the Night Usher, G2 The Visitors' Slot, G8 The Yard Wall, G9 Your Name on the Charge Sheet, Gu2 The Knitted Scar; staging notes on C2, C3, C6, K8, Ca3, Ca4, D8; K1's portrait empty; Kennel and Wake Glimpses (5.3). The art finding's suggested "Keyhole-less Wardrobe" was not used: the wardrobe door is the Heart's Door Portal skin (FL).
29. **Audio:** the card-open and First Telling sounds use the RC7 global UI timbre, not a chime or a sting; noise options play only the generic noise cue; `ReservedCues` becomes an audio-tag check (6).
30. **The content budget and asset table restated** with the Wake, the band Interludes and every string and asset kind; writing and Glimpse briefs before M2 content lock; KR9 says MECH cards and Interludes have one presentation (0.1, 6, 8, KR9).
31. **Riddle clues** are guaranteed, check-free placements whenever the Riddle lock exists, the Place-Setting clue included, never lit options; a hook-answer table answers every parent hook (4.13).
32. **Naming notes:** "Q room 3" is written out; the `Event*` identifiers keep "event" on purpose (0).

**Told plainly after the check (confirmed by the client on 2026-11-22):**
1. **The catalogue broke its own rules as written.** `GainMix` failed in most pools, several Grace prices broke 4.4.3, lit options were free, some options paid two gains, and many props mimicked Portals, containers or reserved cues. The rows were re-authored (5.3); the toy was not re-run on them (8).
2. **The Graces-off fallback deck cannot meet 40%.** It gets its own ceiling of 50% *(illustr.)*: if Graces miss M2, the F1 decks are half facts (4.4.1).
3. **Strands no longer pay at the Forecourt.** A fact about the floor given at room n−2 was nearly worthless; Strands now cash on entering the next floor, which the proposal's D1 already did (4.5.7).
4. **Lit options now cost something.** A free lit option beat its priced twin on most cards and made the pilot Strands dead options (4.3.1).
5. **The budget was well short of the real one.** About 11–12k words and 400–450 art states in M2 with the Wake, not about 7k and 105: the words about 60% of the real figure, the art about a quarter (0.1, 6).
6. **The second meeting with a card costs a full read** of R2 before repeats get cheap; the toy priced every repeat at 6 s and was not re-run (4.7).
7. **RS Cut 1 still removes floor-1 boss variety first.** Q2 made F1 boss variety a production priority, but RS 12's pre-authorised Cut 1 **(agreed, RS Q4)** drops the Gaol's and the Kennel's extra bosses first. Only a client reorder of the RS cut lines changes that, and that would be an RS change. The client did not reorder them on 2026-11-22: the question is carried to segment 9 (boss rooms) as a future item for the client, with no RS change now (14.2). **Answered by segment 9 (agreed, boss rooms Q1, 2026-11-22; BR-26, RS owner applies; M2):** Cut 1 is graded, 1a (the Appetite, only if not yet built), 1b (the Thing in Cell Zero, folded into the Jailer's "Cell Zero Opens"), Cut 2 with Cut 2R, then 1c (the Master in the Frame) client-only below Cut 2 (boss rooms design 4.10.4).
8. **"Help in the visible ladder" is a future decision.** MAIN 4.7 **(decided)** has only harder tiers; no approved document has an easier rung or assist setting. Q4's "difficulty help belongs in the visible ladder" therefore points at a future ladder or difficulty decision owned by the main design and RS, not segment 8; nothing changes now (14.2).
9. **F2 band cards are dry in text.** Cards drawable on F2 and F3 are written in the dry register; the F2 joke lives in Glimpse art (4.11).
10. **The rejected Q1-B's figures were overstated** in 0.1: the row labelled Q1-B was an all-floors, from-M2 bound; the closest measured case is Fallow F1 plus a seeded Fallow on F2–3 (0.93 / 1.00 / 0.00 at 10 runs) (0.1).
11. **The Shade's Lure was misdescribed.** Lure is a universal action pending the enemies owner; the Shade-lit variant is dropped (4.8).
12. **`Event*` names keep "event"** on purpose, as internal identifiers (0).

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a new lead's call or a lead's call after the check (confirmed by the client on 2026-11-22, with this document) unless marked:

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| Graces in M2 | The conditions owner rules EV-11 and `EventDeathShareDelta` ≤ +0.5 pp (novice; F1 at party 1; F2 at parties 1 and 2) | Graces ship in M2; otherwise M3 with the fallback deck | Pre-approved by Q3 (agreed) |
| Freshness gate miss | `CuriosityFreshShare` below its M2 gate | Content first: Shallows and F1 pool depth, then R3 brought forward for F1; any change to K, to the Fallow floors or an in-key draw returns to the client (Q1) | Lead for content; client otherwise |
| Clock line miss | `CuriositySeconds` or `CuriosityClicks` over 4.7 | The 4.7 cut order, before any CMP 7.4 lever | Lead |
| F1 presence low | `F1CuriosityPresence` < 1000‰ on a Blueprint | A defect, not a tuning miss: under V16s `F1HostAlways` every F1 Blueprint has a legal host (SV-30). Fix the Blueprint or the pool before pool sizes freeze; host order unchanged (cross-document comparison 2026-11-23, CC-117) | FL, segment 3 |
| Dominant or dead option | `OptionTakeRate`, `LitDominanceShare`, `ReflexTakeRate` | Card rework inside the palette | Lead |
| Wake gate fails | RS 8.2's gate | The Wake stays the first M3 unlock (RS 8.5); its kit ships with it at parity | RS owner (agreed, Q2) |
| **Conditions owner refuses** `eventBoon` | — | Graces and riders M3 (agreed, Q3); the fallback deck and rider fallbacks ship (4.4.3, 4.4.6) | Conditions owner |
| **Conditions owner refuses** Weary universality (EV-34) | — | Weary only on the catalogued cards (one per F1 theme, plus S3, S5, S6) | Conditions owner |
| **Enemies or EP owner refuses** the noise reading (EV-14) | — | Noise prices become bound tick costs; any Grace then priced in noise is re-priced in HP, Weary or an item (never ticks alone), and any lit option whose price would then share a kind with its priced option is re-priced to keep them disjoint | Enemies, EP owners |
| **7c or LE refuses** `stepFunded` | — | No priced lock routes: G3 gains F-SECRET (else F-ROOM); Gu2 (M3) is cut | 7c, LE owners |
| **LE or 7c refuses** F-CACHE | — | S2 gains F-ROOM (as catalogued) | LE, 7c owners |
| **Segment 9 exposes no seeded boss variables** | — | F-BOSS is the boss's Lore entry (4.4.1) ("H5 becomes F-ROOM" dropped: H5 no longer gains F-BOSS, 5.3; cross-document comparison 2026-11-23, CC-120). **Not triggered:** segment 9 exposes the Rendition and mirror (BR-29; boss rooms design 4.12) | Segment 9 |
| **Segment 3 offers no Alcove set pieces** | — | F1 host order falls to a free leaf side room, then none, logged | Segment 3 |
| **RS owner refuses** the Fallow extension | — | Returns to the client (Q1 was agreed) | RS owner; client |
| **Main design owner refuses** the safe-point reading | — | Returns to the client: without it a reload can undo a cost (BC12) | Main design owner; client |
| **Herald owner refuses** EV-13's variant | — | The variant is not shipped; nothing else changes | Herald owner |
| **The F1-boss cut order (carried to segment 9)** | Segment 9 (boss rooms) is designed, or the schedule slips into RS Cut 1 (RS 12 cut lines, **agreed, RS Q4**) | The client did not reorder Cut 1 on 2026-11-22; RS 12 is unchanged now. Cut 1 still drops the Gaol's and the Kennel's extra bosses first, against Q2's priority. The question returns to the client with segment 9; the lead may then recommend moving F1 bosses below Cut 1, and any reorder is an RS change. **Answered (agreed, boss rooms Q1, 2026-11-22):** graded Cut 1 (1a, 1b, Cut 2 with Cut 2R, 1c client-only), an RS change applied by the RS owner (BR-26; boss rooms design 4.10.4) | Client; segment 9, RS owner |
| **A future ladder or difficulty decision** | The main design or RS takes up the M3 ladder | Q4's "difficulty help in the visible ladder" is handed to the main design owner and the client; segment 8 changes nothing | Main design owner; client |
| **The worst-case card does not fit** | Segment 10's layout test (4.3.1) | The rare body cap is lowered before any chip rule changes | Lead; segment 10 |
| **Referents fall back often** | `ReferentAvailability` high on a Blueprint | Re-author the tied option's referent kind, or ask FL for the referent | Lead; FL |
| **Walker equivalences** (pending request from the seeds and validation design 6.2, to the XI, FL and EV owners) | This design's owner does not rule | No mapping; each gate reads its own owner's arm (9.4; seeds and validation design 14.2) | EV, XI, FL owners |
| **Curiosity Malady costs** (pending request HL-19 from the heirlooms and curses design 4.3.3, to this design's owner; M4) | This design's owner does not rule, or `EventDeathShareDelta` fails on F2 | No Curiosity Malady row; on a gate miss, Malady costs start on F3 (heirlooms and curses design 14.2) | EV owner; lead |
| **Maladies at the boss** (heirlooms and curses design 14.2; M4) | `MaladyAtBossShare` high, or `MaladyDeathShare` above baseline +2 pp at boss fights | Every Malady also ends at the arena seal (`ArenaEngaged`, the Grace precedent, 4.4.3) | Lead; EV, BR owners |
| **Companions owner refuses** Testimony | — | Pledges (Later) ship without Testimony | Companions owner |
| Naming pass | — | Every placeholder name here | Client |

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | **A narrow reward vocabulary.** Facts and small Graces may feel low-stakes next to genre relics; more so if Graces wait for M3 | `OptionTakeRate`, `GraceExpiryRate`, `GainMix`; the "Graces off" arm |
| 2 | **Facts competing with other sources.** The FactLedger may often fall through to "nothing left" | `FactRedundancy` |
| 3 | **F1 host fragility.** Some Blueprints may have no F1 Curiosity | V16s `F1HostAlways`; `F1CuriosityPresence` is 1000‰ and a shortfall is a defect (SV-30) (cross-document comparison 2026-11-23, CC-117) |
| 4 | **Dependencies.** EV-11 (Graces), EV-14 (noise reading), EV-15 (`stepFunded`) and segment 9's seeded boss variables | Each falls back as stated in 14.2 |
| 5 | **The Twice-Told card becoming a skip tax** | Delta-first presentation; `TwiceToldSkipRate` |
| 6 | **Content cost.** About 11–12k words and 400–450 art states in M2 with the Wake (restated after the check). Ripening doubles M3 | Ripening scheduled late (agreed, Q1); the art and content cut order in 0.1 and 6 |
| 7 | **Personal and shared seeds differ** (launcher, Retellings) | Personal runs only; replays carry memory |
| 8 | **Story creep from Sequels** (RS risk 14) | Tone review on every chain |
| 9 | **The comic register on the most-repeated floors** | Mitigated by Retellings; not cured |
| 10 | **Toy crudeness** | The simulator replaces every figure here |
| 11 | **Floor-1 variety outside segment 8.** The streak arm's biggest repeat is fights, Blueprints and bosses, which segment 8 does not own; RS Cut 1 removes F1 bosses first (answered: Cut 1 is graded and every F1 boss ships 3 Renditions; agreed, boss rooms Q1; BR-23, BR-26) | Flagged as a production priority (agreed, Q2); the cut-order conflict told plainly (14.1) and carried to segment 9 as a future item for the client (14.2) |
| 12 | **The re-authored catalogue is untested.** Priced lit options, floor-entry Strands and the new rows change take rates the toy never measured | `OptionTakeRate`, `LitDominanceShare`, `StrandCashRate`; the simulator before pools freeze |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: thresholds, tick and noise amounts, the rare weight, the shared share, the draw count, the Sequel rarity, the Ripening sighting count, every report target other than the stated gates.
- Retelling text and art within the caps, the tags and the tone guards; Glimpse sets per theme; Twice-Told lines; the writing and Glimpse briefs of 6.
- `curiosityValueTable` and `curiosity.rarePermille` (4.4.9, 4.10).
- Godot scene structure for the card, the Twice-Told card, wall Glimpses and the figure frame, with no physics.
- Names, until the naming pass.
- **Not tuning:** the Never list; StateBlind and no mercy **(agreed, Q4)**; Retellings outside the key and RS 9.1 closed **(agreed, Q1)**; Fallow K = 3 on the F1 Curiosity only **(agreed, Q1)**; the Grace wave rule **(agreed, Q3)**; `F1PoolParity` **(agreed, Q2)**; the draw map's fixed order and count; EventDraw run-blind; EventPlace draw-free after LootBind; resolving is a safe point; one Curiosity per room; the free Leave; printed chips; `LitIsSidegrade`, `LitHasPrice` and `LitNoExtraGain`; one rules gain per option; the cut order; the EventDraw in-slot order and the pass order; the host and referent tie-break; the resolve write order; the presentation sequence; Strands cashed at floor entry or the Coat Hooks.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **The conditions owner:** EV-11, EV-12, EV-33, EV-34.
- **The DH owner:** EV-3, EV-9, EV-10, EV-12, EV-33.
- **The enemies owner:** EV-7, EV-8, EV-13, EV-14, EV-32, EV-36.
- **The EP owner (segment 6):** EV-14 (the noise reading), EV-32 (the EP 13 reading).
- **The stat and TL owners:** EV-5, EV-6, EV-15, EV-30.
- **The FL owner:** EV-2, EV-23, EV-35, EV-36, EV-44 (Blueprint variety).
- **The room terrain owner (segment 3):** Alcove set pieces (EV-2).
- **The RS owner:** EV-2, EV-18, EV-20, EV-22, EV-24 to EV-29, EV-31, EV-37, EV-38, EV-40, EV-44, EV-45.
- **The main design owner:** EV-19 (with the MAIN 9 wave pull), EV-21 (the safe-point reading); a future ladder or difficulty decision (14.2).
- **Dungeon generation 7a (LE) and 7c (LP):** EV-7, EV-8, EV-15; F-CACHE; Alcove occupancy.
- **The Herald owner:** EV-13. (No Shade request remains after the check: the Shade-lit Lure variant is dropped.)
- **The companions owner:** EV-41.
- **The items owner:** EV-9, EV-42, EV-43.
- **The heirlooms and curses design:** EV-43 and the Strand "a curse ended". Taken up by the heirlooms and curses design (segment 15, approved 2026-11-23; HL-4 (M3) and HL-29 (EV-43 to M4) applied; pending request to this design's owner: HL-19, the Curiosity Malady cost row, `payer`, feasibility and `registerLock` (M4); its 4.1.6, 4.2, 4.3.2, 4.3.3, 10).
- **The HZ owner:** EV-31, EV-35, EV-36 (HZ-23).
- **Segment 9 (bosses):** seeded boss variables; the arena seal; floor-1 boss variety (EV-44); the RS Cut 1 question once carried for the client (14.2), now answered as graded (BR-26) (cross-document comparison 2026-11-23, CC-116). Answered by the boss rooms design (approved 2026-11-22; BR-3, BR-23 to BR-26, BR-29; its 4.2.1, 4.10, 4.12).
- **Segment 10 (UI):** 4.12; EV-17. Answered by the exploration and information design (approved 2026-11-23; XI-4, XI-6, XI-22, XI-27, XI-29; its 4.3.6, 4.6.1, 4.8.1, 4.10, 5); pending requests to this design's owner: beckoning Glimpses, `SideRoomPayoffMix`, the Curiosity Lodestring gain (XI-23, XI-27).
- **Segment 11 (seeds):** the `curiosities` stream and its draw map, the pass order, the launcher order (EV-20, EV-21, EV-40). Answered by the seeds and validation design (approved 2026-11-23; SV-1, SV-2, SV-4, SV-6, SV-8, SV-9, SV-13, SV-16, SV-30, SV-31; its 4.1–4.4, 4.6–4.8, 11).
- **Segment 12 (validation):** V16 and the reports (EV-38, EV-39). Answered by the seeds and validation design (approved 2026-11-23; SV-20 to SV-23, SV-25, SV-30; its 5.1–5.4, 6.1–6.5, 6.12); pending request to this design's owner: walker equivalences (SV-22; 9.4, 14.2).
- **The camp and hub design:** Afterword and keeper lines, Pledges, Testimony, the Hawker's Hour, coin owed. Answered by the camp and hub design (segment 13, approved 2026-11-23; CH-12 (EV 6), CH-14 (EV 4.5.10); its 2.2, 4.10, 4.15.6, 5.1, 6).
- **The simulator:** every report of 9.3 and every arm of 9.4.
- **Art, audio and writing:** 5, 6.
- **Naming pass:** every placeholder name here.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Fun critic (FC)

| Id | Finding | Resolution |
|---|---|---|
| FC1 | Lit options are cost waivers, which solves the card | LitIsSidegrade + LitDominanceShare (4.3.1) |
| FC2 | Fact vending machine; static F-BOSS | Run-specific F-BOSS; GainMix; facts drawn on the map (4.4.1) |
| FC3 | Near-free prices; MECH reflex clicks | Bound costs; ReflexTakeRate replaces the exemption (4.5.5) |
| FC4 | The "it knows you" meta-repetition | PremiseTagCap; catalogue rewritten (4.5.6, 5) |
| FC5 | Heart props implying the sleeper's age | HeartBedroomCap; cluster cut; letter replaced (4.11, 5.3) |
| FC6 | Progress-on-loss never arrives | Chain beats on the run-start card; Strand payoffs on entering F2 and closure lines; Sequel rarity draw; a never-silent Afterword (4.5.7–4.5.10) |
| FC7 | Repeat card anchors on the old answer | Delta first; footer; auto-answer suppressed on a delta (4.5.2) |
| FC8 | "Deadpan" is a change, not a reading | Register kept; punchlines in Retellings and Glimpses (4.11; told plainly) |
| FC9 | Lures give away a verb | Noise here only; Lure is universal, so no Lure variant at all (EV-14, after the check) |
| FC10 | Map-memory price is anti-fun | Colour-only cost plus ticks, M3 (Ca1) |
| FC11 | Memorials demoralise | Last 3 names, no counts, forward-looking lines, hide setting (4.5.10) |
| FC12 | Wake rare near suicide imagery | Rewritten as "sit with the guests" (W6) |
| FC13 | Graces expire or grey out | Conversion at cap; HUD expiry; GraceExpiryRate (4.4.3) |
| FC14 | Onboarding load | Fallow prefers MECH or short cards in runs 1–2 (4.5.4) |
| FC15 | Definitions and names | "Seen" = displayed; rares handled as in BC6; renames (A.5) |

### A.2 Balance critic (BC)

| Id | Finding | Resolution |
|---|---|---|
| BC1 (S1-1) | Successors strictly better, farmable | SuccessorParity; committed sightings; farm report; Late Leaves rejected (4.5.9) |
| BC2 (S1-2) | Codex dedup leaks power | Dedup is in-run only; codex write-only (4.4.2) |
| BC3 (S1-3) | HP costs free at low HP | PaymentFeasibility; the hero pays by default (4.4.4, 4.4.7) |
| BC4 (S1-4) | Grace durations leak into arenas; party-wide scaling | End at the seal; single unit; Heart and F4–5 facts only (4.4.3) |
| BC5 (S2-1) | Party-size advantage | LitNoExtraGain; LitMetRate by party size (4.3.1) |
| BC6 (S2-2) | Fallow inflates rares; forecast not layout-blind; loot neutrality; budget order | Rares not exempt; host-agnostic F1 cards; LauncherLootNeutral; theme-passing seed kept (4.5.4) |
| BC7 (S2-3) | Curiosity Lures beat the Shade | No Lures; noise here; Circuit clearance (EV-14) |
| BC8 (S2-4) | New bypass edges | Chute skins only; GraphUnchanged; XpBandAfterEvents (EV-36) |
| BC9 (S2-5) | Lock premium eroded | `stepFunded = false` only; never ticks alone (4.4.5) |
| BC10 (S2-6) | Pre-rest pricing makes costs fake | Rest-adjacent arm; rest-surviving Weary offered (EV-34) |
| BC11 (S2-7) | Overtures trivialise elites | Exclusions; M3 (4.9) |
| BC12 (S3-1–S3-10) | Safe point; Strand overlays; Memorial Signs; file tampering; F-CACHE exposure; coin owed; supply; heal-adjacent relief; clock measured; farming | ResolveIsSafePoint; overlays (floor-entry cashes after the check); display tokens only; invalid = empty; exposure counted; off; removed; Easing banned on F5 and pre-arena; first-10-runs arm; report |

### A.3 Consistency critic (KC)

| Id | Finding | Resolution |
|---|---|---|
| KC1 | Name collisions | Renamed (A.5) |
| KC2 | Bedside Shelf vs the Bedside Stand | Coat Hooks by the Door; ReservedSilhouette |
| KC3 | Moving moon, open eyes, clock-dial lock object | Weathervane, Uncovered Mirror, Tally Board |
| KC4 | C5 replacement; durations | Replacement named; `eventBoon` form (EV-11) |
| KC5 | Show of Force and Lure giveaways | EV-13, EV-14 |
| KC6 | Bells break RC7 and RC9 | No bell props; drawer, pipe |
| KC7 | RC2 chairs | Wall hooks, place cards, napkins |
| KC8 | Fallow vs pass order | EventDraw / EventPlace; the draw map (4.10) |
| KC9 | R-SEED-6 extension; RS 12 wave pull | EV-20 ⚑ (agreed, Q1) |
| KC10 | MAIN 6.6 profile | EV-19 ⚑; one store |
| KC11 | Ripening and Sequels are readings | EV-27, EV-28 ⚑ |
| KC12 | Strand slot-taking | Overlays; after the check, floor-entry cashes and the Coat Hooks (4.5.7) |
| KC13 | Sealed Letter timing | Opens on entering the next floor, now every Strand's default (4.5.7) |
| KC14 | Pending dependencies | Waves conditioned (10, 13, 14.2) |
| KC15 | Theme 6 | EV-34; Scarred dropped |
| KC16 | Heart Graces | Facts only |
| KC17 | Stealth "who" | Movement uses the worst member; manipulation uses the actor (reading) |
| KC18 | Leadership | EV-6 flagged reading |
| KC19 | RS 7.5 Heart cases | Letter, glass and corner cut |
| KC20 | Numbers as V16 rows; Alcove vs 7c | Rows added; LT16 and LP-11 flagged |
| KC21 | `dreamedAway` reuse | `ItemDestroyed{offered}` |
| KC22 | EV renumbering | One register (A.6) |
| KC23 | "Stake" near "Staked pits" | Dropped |
| KC24 | Hook sections | Updated (4.13) |

### A.4 Judges' grafts taken

All three judges' grafts are taken except where a critic or the client superseded them:
- rares are no longer exempt from Fallow;
- Show of Force is no longer granted as a Grace;
- "deadpan" is withdrawn;
- the Bedside Shelf is renamed;
- the least-seen draw inside the key is rejected (Q1).

### A.5 Names checked (case-insensitive, whole word, 26 approved documents)

**Free (0 hits; re-run while writing):** Curiosity, Curiosities, Interlude, Glimpse, Grace, Strand, Retelling, Twice-Told, First Telling, Fallow, Afterword, Reprise, Ripening, Sequel, Unwoken Roll, Night Usher, FactLedger, Testimony, Easing, figure frame, `curiosityMemory`, `curiosityKit`, `CuriosityRecord`, `stepFunded`, `PaymentFeasibility`, EventDraw, EventPlace, Turnkey's Daybook, Second Notice, Admission: One Memory, Curtain Call, Every Glass Rises, Coat Hooks by the Door, Tally Board, Slop Hatch, Lodge Weathervane, Uncovered Mirror, Waterlogged Pipe, Black Basin, Codicil, Warming Dish, Bill of Fare, Lamp Wick, Unsung Verse, Start the Clocks, Turning Key, Printed Itinerary, Curtain Gap, Bedpost Ladder, Unread Page, Wall That Keeps Its Distance, and the Strand names (Signed In, Settled Debt, Sat Down, Left Luggage, Shook Hands, Pedigreed, Off the Rota, Petitioned, Collared, In Mourning, Rehearsed, Time Already Served, The Silence Held, In the Book, Took a Bow, Sealed Letter, Shut Behind You). Also free (after the check): Referent, `referentKinds`, `ReferentResolved`, `ReferentFallbackPresent`, `ReferentAvailability`, `CuriosityReferentFallback`, `CuriosityAbsent`, `GraceGranted`, `GraceEnded`, `StrandSet`, `StrandCashed`, `StrandClosed`, `eventRider`, `curiosityValueTable`, `propFamily`, `registerNeutral`, `domesticProp`, `graceTarget`, `fullShown`, `recentRuns`, `pendingClosure`, `LitHasPrice`, `GracePriceNotFree`, `LitSpreadPerPool`, `F1DeltaSource`, `ForecourtGainFacesArena`, `StrandSurvivesCut`, `CrashBetweenWrites`, `StagingRC1`, `AfterwordExhaustion`, `LeaveRate`, The Visitors' Slot, The Yard Wall, Your Name on the Charge Sheet, The Skirting Board, The High Window, The Pinned Notice, The Knitted Scar, The Damp Order of Service, The Drip Count, The Returns Slip, The Shelf Mark, The Running Order, The Prompter's Cue. Also free: every other card title in 5.3, the V16 row names, `teachingRung`, `F1PoolParity`, `StateBlind`, `CuriosityLootBlind`, `GainMix`, `eventBoon`, `graceFallbackGain`, F-LISTEN, F-CACHE, `curiosityHost`.

**Kept on purpose (whole-word hits; new lead's call):** "Seating Chart" (Su-I1) and "The Last Page" (S6's Strand) echo their themes' approved Obsession prose in RS; "strands" appears only as an ordinary verb (CMP, RS).

**Renamed because taken:**

| Draft name | Taken by | Now |
|---|---|---|
| Known card | Known traps | Twice-Told card |
| Lucid frame | Lucid Key | Figure frame |
| Turnkey's Ledger | LE Ledgers | The Turnkey's Daybook |
| Overdue Notice | Silk Mood | Second Notice |
| Ticket Booth | Ticket lock | Admission: One Memory |
| The Last Act | Wager room | Curtain Call |
| The Toast | Supper boss signature | Every Glass Rises |
| Appeal Hearing | Appeals trapdoor | Cut |
| Remedy | Vendor consumable | Easing |
| Bedside Shelf | Bedside Stand silhouette | Coat Hooks by the Door |
| The Will | Executor of the Will | The Codicil |
| The Font | Dry font Snag | The Black Basin |
| Brazier's Due | Brazier piece; Dues | The Warming Dish |
| Menu Card | HZ | The Bill of Fare |
| Reading Lamp | HZ NM-9 | The Lamp Wick |
| Collection Plate | TL trap | The Unsung Verse |
| Stopped Clocks | ROOM, LP | Start the Clocks |
| Wind-Up Key | Wind-Up | The Turning Key |
| Night Porter | Porter's Satchel | The Night Usher |
| Paid In, Seated, Received, Kin, Checked In | Various prose | Settled Debt, Sat Down, Shook Hands, Pedigreed, Left Luggage |
| Mourners' Chairs | Herald feature; RC2 | Cut |
| Visiting Hour | EP G5 | The Visitors' Grille, renamed the Visitors' Slot after the check |
| The Far Wall | Layout term | The Wall That Keeps Its Distance |
| Bindings | EP's generation term (the Annex, Wager and boxed-lurker bindings) | Referents (after the check) |
| The Visitors' Grille | Grille Portal kind | The Visitors' Slot (after the check) |
| The Turning Key | Lullaby Glyph wind-up cue | Cut; The Skirting Board (after the check) |
| Keyhole-less Wardrobe (a check suggestion) | The Heart's wardrobe-door Portal skin (FL) | Not used; The Skirting Board |
| Returns Trolley | Taken (4 hits) | The Returns Slip (after the check) |

**Also avoided:** Aside (Shade), Thread (Arcanist), Parley (Dirge Herald), Stake (Staked pits), Bearing, Echo, Toll, Lost Property, Hawker, Pledge, Reading(s), Omen, Mood, Tend, Keepsake, Vigil, Dues, Ledger, the hourglass glyph, horns, bells. **Local ids:** XT, NF, KR, DV, FC, BC and KC all have 0 hits. ET (27 hits) was rejected. **EV-n** and **V16** have 0 hits (re-run while writing).

### A.6 EV cross-reference

Every draft numbered from EV-1 with different contents (curated-few EV-1–42, storylets EV-1–36, systemic EV-1–33, the lead's catalogue EV-1–33, anti-repetition EV-1–14, new mechanics EV-1–21). All are renumbered into this register by topic:
- terms → EV-1; hosts → EV-2; floor 1 → EV-3; card → EV-4; checks → EV-5, EV-6;
- palette → EV-7 to EV-10; Graces → EV-11, EV-12; class verbs → EV-13, EV-14; loot → EV-15;
- caps → EV-16; repeat card → EV-17; Retellings → EV-18; memory → EV-19; launcher → EV-20;
- determinism → EV-21; Strands → EV-22; clock → EV-23; tone → EV-24, EV-25; Afterword → EV-26;
- Sequels and Ripening → EV-27, EV-28; M3 meta → EV-29 to EV-33; conditions ruling → EV-34;
- Landmarks → EV-35; Chutes → EV-36; codex → EV-37; V16, reports and data → EV-38 to EV-40;
- Later → EV-41 to EV-43; Wake → EV-44; figures → EV-45.

The drafts' in-key least-seen proposals (curated-few EV-19, storylets EV-5, new mechanics EV-11) mapped to client option Q1-B, which the client rejected (Q1); they are not register rows.

## Appendix B: sources

- Gloomhaven event card placement and adding cards (rules.dized.com); the Slay the Spire events wiki; "Correlated Randomness in Slay the Spire" (forgottenarbiter.github.io); the Darkest Dungeon curio wiki; PC Gamer on Darkest Dungeon curios. Other game details are from memory and marked "unsure" where uncertain.
- The segment 8 proposal and its toy scripts (illustrative only, kept in the design lead's scratch area, not in the repository): the streak, Fallow, launcher, Stake, Binding (now Referent) and power (`dh.py`) toys behind section 8.
