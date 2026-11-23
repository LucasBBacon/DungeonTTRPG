# Run Structure and Floor Themes: Design (Dungeon Generation, Segment 1)

Date: 2026-11-21
Status: Approved, 2026-11-21
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design),
`2026-11-17-consumables-design.md` (consumables design),
`2026-11-17-death-healing-design.md` (death and healing design, "DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "classes"),
the stage-2 class designs `2026-11-18-vanguard-class-design.md`,
`2026-11-18-shade-class-design.md`, `2026-11-20-arcanist-class-design.md`
and `2026-11-20-herald-class-design.md` ("Vanguard design", "Shade
design", "Arcanist design", "Herald design"), and
`2026-11-20-companions-design.md` ("companions design", the most recent
template)

Items marked **(agreed)** were decided by the client on 2026-11-21 for
the four questions this design raised (Q1–Q4, sections 6, 7, 8 and
19.1), by choosing the lead's recommendation in each case, or restate a
client answer recorded in an approved document. The client also said
that everything else in the lead's proposal stands as written,
including the told-plainly list, the catalogue, the hooks, the binding
requirements, the RT-n changes, the risks and the critique resolutions.
The client approved this document as a whole on 2026-11-21.

Items marked **(lead's call)** were filled in by the design lead inside
approved rules; the client confirmed them all on 2026-11-21, when it
approved this document as a whole, and they are binding. Items marked
**new lead's call** were made while writing this document, beyond the
proposal the client answered; they are listed in 19.1 and the client
confirmed them all on 2026-11-21. Items marked **(settled)** restate a rule from an approved
document; **(decided)** marks a main-design client decision. Items
marked **(change RT-n)** amend an approved document and are listed, with
the exact section they touch, in section 17; rows that touch
**decided** or **agreed** text are flagged there, and the client's
approval of this document on 2026-11-21 (with Q1–Q4 where a row names
one) covers them. Items marked **(told plainly)** narrow or re-read
agreed or settled text, or state an unwelcome forecast, and were told to
the client in plain words with the questions or after the check (19.1);
the client confirmed them all on 2026-11-21. Every item here is binding
on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q4 of this document (2026-11-21), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules; confirmed by the client with this document on 2026-11-21 |
| **new lead's call** | A lead's call made while writing this document, beyond the answered proposal; listed in 19.1 and confirmed by the client on 2026-11-21 |
| **(change RT-n)** | Amends an approved document; listed with its exact section in 17 |
| **(told plainly)** | Narrows or re-reads agreed or settled text, or states an unwelcome forecast; told to the client in plain words (19.1) and confirmed by the client on 2026-11-21 |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (10, 11) |

**Design-check revision (2026-11-21).** After the client's answers, a
builder, consistency, play and tone check of this document was applied
by the lead. Its fixes are marked **(lead's call)** where they are new
judgement; none changes a Q1–Q4 answer. The id schemes were renamed so
they collide with no approved id: the charter is **RC1–RC9** (the
questions called it C1–C9), the pool rules **RP1–RP6**, the tensions
**RE1–RE18**, and the Appendix A.4 findings **CF-H**, **CF-M** and
**CF-L**. Changes told to the client are listed in 19.1 under
"Told plainly after the check"; the client confirmed them on 2026-11-21,
with this document.

This is **segment 1 of 12 of dungeon generation**: how a run is shaped
(floors, cadence, camps, length), how floor themes are chosen, what the
themes are, the tone they share, the numbers that hold it together, the
staging, and the hooks handed to the later segments. The later segments
get **hooks only** (section 13): floor layout; room shapes and terrain;
hazards; traps and locks; encounter, loot and event placement; boss
rooms; exploration and information; seeds and validation (the remaining
segments are scoped by those designs).

The client's brief:

> "Should the dungeon themes vary per floor or only after defeating a
> boss? Should we randomize the themes at each theme level (e.g. at the
> first theme we can have theme A or theme B be picked at random, then
> second theme will be C or D, etc). Our artists are happy to model
> environments, enemies, traps, etc for each theme proposed, so don't be
> shy in the visual variation... An idea that has been floating around
> for the encompassing theme of the game is absurdist dark fantasy,
> surrealist, though we are not fully committed to that yet... I don't
> have much input on run structure, this one I leave for your
> considerations and research."

The lead's answers, which the client accepted (Q1–Q4, **agreed**), are
in section 0: **a fixed 5-floor run with a new theme on every floor,
drawn from tiered random pools at run start; Two Doors route choice
built in M2 and switched on only if its gates pass; the "Grim Surreal"
tone adopted provisionally under a binding readability charter; and an
8-theme M2 core with pre-authorised cut lines.**

**Change prefix.** This design uses **RT-n** ("run and themes"). A
whole-word grep for `RT` and `RT-` across the 17 approved documents finds
no hits. The research and model drafts used five schemes (RS, CHG-B,
CHG-C, CHG-M, TC); every draft id is mapped once to its RT row in
Appendix A.1, and no draft id is used anywhere else in this document.

**Names are placeholders** until the client naming pass. They are frozen
here so the documents agree. Every new name was grep-checked as a whole
word against the 17 approved documents; 8.0 lists the names avoided
because they collide.

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every
minute, win rate and share comes from crude toy models (Appendix B); the
M2 simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile,
unlocks, ladder), **M4** breadth, **Later** **(settled, main design 9)**.

---

## 0. At a glance

### 0.1 The answers to the brief

**1. Per floor, or only after a boss? Per floor (agreed, Q1).**
- Every floor already ends in a boss **(decided, main 4.1)**, so "a new
  theme per floor" and "a new theme after each boss" are the same rule.
  Each floor has one theme, which is main 4.6 as written **(settled)**.
- *A theme spanning two floors* is rejected: one faction would hold the
  screen for 35–40 minutes, against the genre norm of 10–20-minute
  biomes, and a run would drop from 5 factions to 3.
- *A theme changing mid-floor* is rejected: the 6-condition budget, the
  single visual faction, the Lore reveal and the camp next-floor line are
  all per floor.
- **Floor 5 breaks the pattern.** It is always the same destination, the
  Dreamer's Heart, with a pool of final bosses.
- An act feeling survives as **presentation only**: Shallows, Middle
  Dream, Deep, Heart.

**2. Randomise per theme tier? Yes, with three refinements (agreed,
Q1).**
- **Tiers follow the approved floor groupings** (caps, boss pips,
  hard-control gates): floor 1 | floors 2–3 | floor 4 | floor 5.
- **Floors 2 and 3 share one band pool of 3**, so a theme can appear on
  floor 2 in one run and floor 3 in the next: 6 ordered pairs instead of
  the brief's 2 × 2 = 4, for one theme fewer than two pools of 2.
- **No theme repeats in a run.** The whole route is drawn once from the
  run seed, so a seed always reproduces its run.
- Pool partners differ in **kind** (terrain, damage type, morale read),
  not only in look, because a pool of 2 is learned in about 10 runs.
- **Two Doors**, a choice between two pre-drawn candidates at camps 1
  and 2 plus the final-boss pick at camp 4, is **built in M2 and
  switched on only if its gates pass**; otherwise the random draw ships
  and the doors arrive in M3, with no structural change and no seed
  change **(agreed, Q2)**.

**3. Visual variation: much more of it (agreed, Q4).**
- M2 gets **8 floor themes** instead of the approved "4 themes plus a
  deep overlay": 2 on floor 1, 3 on floors 2–3, 2 on floor 4, and the
  fixed finale. Each has its own faction, environment, music and bosses.
- Themes on the floors 2–3 band get a shallow and a deep dressing set.
- The most-seen and most-replayed floors get extra bosses.
- **Told plainly:** art is no longer the constraint, but design, data,
  content tests and simulator time still are. This is about **2.8 times**
  the approved M2 content cost *(illustr.)*. Cut 1 and Cut 2 are
  pre-authorised schedule fallbacks (12).

**4. The absurdist, surrealist idea: adopted provisionally, in a
disciplined form (agreed, Q3).**
- The form is **"Grim Surreal: a dream that obeys rules."** Places are
  surreal and motives absurd; rules, threats and deaths are played
  completely straight.
- Taken literally, absurdism is the riskiest option: a world that feels
  arbitrary makes deaths feel arbitrary, and comic deflation undercuts
  permadeath. The binding readability charter RC1–RC9 (7.4) buys
  readability back.
- It fits the morale system unusually well: petty, sincere obsessions
  honestly flee, yield and get cowed; empty things honestly carry the
  fearless and mindless share.
- It is reversible: every theme carries a one-line grim reading.

**5. Run structure.** Five fixed floors, about 104–115 minutes in all
*(illustr.)*: floor 1 solo about 15–17 min; floors 2–3 about 17–22 min;
floors 4–5 held to the 20-minute gate; four camps of 3–4 min. Two pacing
fixes are owned here:
- **(a) the Antechamber Catch-up** guarantees level 3 before the floor-1
  boss (the Shade and Arcanist requests);
- **(b) a concrete form for companions lever 3:** floors 4–5 drop to 2
  guard posts; the 100 experience the trim removes moves to floor 4's
  boss (300) and to floor 5's lair (240), so on floor 5 it is earned
  before the final boss; applied only in the lever order of companions
  7.4.

**Told plainly:** the companions toy predicts the floor-time gate fails
at a cap of 3 in every modelled arm, so the pre-authorised cap-2
fallback is the likely outcome. Theme selection cannot fix the clock.

### 0.2 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | A run is a fixed 5 floors | M2 | **agreed, Q1**; change RT-1 (decided text) |
| 2 | One theme per floor; floor 5 is the fixed final theme with a final-boss pool | M2 | settled (main 4.6) + **agreed, Q1**; RT-2 |
| 3 | Tiered pools: floor 1 (2), floors 2–3 band (3), floor 4 (2), final (1 theme, 2 bosses) | M2 | **agreed, Q1**; RT-2 |
| 4 | Whole route drawn at run start on a `route` child of the dungeon stream (fixed-count, whole-catalogue permutation) | M2 | **agreed, Q1**; RT-3 |
| 5 | Two Doors at camps 1–2 and the Last Hollow choice at camp 4: built in M2, switched on only if the gates pass; otherwise random, doors in M3 | M2 or M3 | **agreed, Q2**; RT-10, RT-11 (decided text) |
| 6 | Grim Surreal tone, charter RC1–RC9 with M2 gates, Tone Sheets, uncommitted Dreamer premise | M2 | **agreed, Q3**; RT-17 |
| 7 | M2 themes: Gaol, Kennel Wood (F1); Drowned Choir, Silk Archive, Carnival (F2–3); Long Supper, Gut (F4); Dreamer's Heart (F5). Endless Wake if it passes its floor-1 test. Cut 1 and Cut 2 pre-authorised. Build order Heart and Gut first | M2 | **agreed, Q4**; RT-5 |
| 8 | Antechamber Catch-up to level 3 before the floor-1 boss | M2 | lead's call; RT-8 |
| 9 | Lever 3: floors 4–5 at 2 guard posts; floor-4 boss experience 300, floor-5 lair experience 240; only in the companions 7.4 lever order | M2 | lead's call; RT-7 (told plainly) |
| 10 | Step-5 theme overlay stays in M2 as per-theme legal overlays; the floor-5 deep overlay retires to Nightmare tier 3 (M3) | M2 / M3 | lead's call; RT-5, RT-6 |

---

## 1. Intent

The main design fixed a run of "4–5 floors" of 15–20 minutes, each
ending in a boss, with a camp between floors; it fixed that each floor
has a theme that selects tiles, enemies and hazards, and that bosses
come from a pool **(decided, main 4.1, 4.6)**. The enemies design wrote
the theme rules (10.6) and staged "4 themes of 5–6 types plus a deep
overlay on floor 5" for M2 (21). Every later design calibrated on five
floors. None said how many floors a run actually has, how a floor's
theme is chosen, what the themes are, what tone they share, or what the
final floor is.

The answer in one sentence: **a run is five floors, each a different
place drawn from a depth-banded pool at run start, the last always the
Dreamer's Heart; the places are surreal and the rules are not.** The
route is pure seed; route choice is built and switched on only when it
is proven fair; and every theme is built from the conditions, squares
and roles the approved documents already hold.

## 2. Goals, non-goals, anti-goals

### Goals

- **"Vary per floor or only after a boss"** (the brief): one theme per
  floor, which is the same thing as after every boss (6.2).
- **"Randomise at each theme level"** (the brief): tiered random pools,
  with a band pool on floors 2–3 (6.3, 6.4) **(agreed, Q1)**.
- **"Don't be shy in the visual variation"** (the brief): 8 M2 themes,
  each a full faction, environment, music set and boss pair or single
  (8) **(agreed, Q4)**.
- **"Absurdist dark fantasy, surrealist"** (the brief): Grim Surreal,
  provisionally, with a readability charter that keeps every rule
  honest (7) **(agreed, Q3)**.
- **"Run structure ... your considerations and research"** (the brief):
  a survey (4), three models judged (5), and a run plan that fits the
  decided 1–2 hours (6.6, 6.7).
- **Seeds stay pure:** the same reproducibility key (seed, `genVersion`,
  content manifest, configuration snapshot, unlock snapshot; change
  SV-12) always gives the same route and the same run (6.4 R-SEED-5)
  (cross-document comparison 2026-11-23, CC-87).
- **Themes differ in kind, not amount:** budgets stay theme-agnostic
  **(settled, enemies 10.1)**; themes change which tools are good.

### Non-goals

- **Layouts, rooms, terrain placement, hazards, traps, locks, placement,
  boss rooms, exploration and seed validation in full.** Those are the
  later segments; this document hands them hooks (13).
- **The camp and hub screens in full.** This document fixes the camp
  card's contents and its place in the camp order (6.8, 6.9); layout is
  the camp and hub design's and the builders'.
- **Final names** (naming pass), **final numbers** (simulator) and
  **final art** (the art team, inside the charter).
- **A story campaign** **(settled, main 2)**. The premise explains the
  rules; it commits no plot, no Dreamer identity and no endings.

### Anti-goals

- **A dice tax:** a random draw that decides runs by matchup rather than
  play. Guarded by ThemeParity (a build gate on random-draw floors too),
  ClassThemeSpread and pool partners that differ in kind (6.3, 11.4).
- **A dominant door:** route choice that rewards avoidance or has one
  right answer. Guarded by the Two Doors gates (6.11) **(agreed, Q2)**.
- **Readability sold for strangeness:** fake tiles, lying UI, hidden
  rules, or units whose role or morale cannot be read (7.4).
- **Bring-the-key:** loot or recruits that read the next theme, which
  would turn foresight into a must-carry list (6.4, LootDoorInvariant).
- **Grind to win** **(decided, main 2)**: unlockable themes and final
  bosses are content, never power (RT-18).
- **New rules smuggled in as flavour:** every theme is built from
  existing condition ids, square kinds, riders, forced movement and
  morale; one new square kind (bile) reuses Corroded (RT-13).

## 3. Constraints

### 3.1 What the parents already fix

The 17 approved documents were grepped for theme, floor, camp, overlay,
boss pool, depth, `eligibleFloors`, `Floor1ClassSpread` and "next floor",
and each hit was read in context.

- **Main 4.1 (decided):** a run is "4–5 floors" of about 15–20 min, each
  ending in a boss; camps between floors with full rest, a basic vendor,
  a Muster and a notables shop or reward card; about level 10 at the end.
  **Main 2 (decided):** 1–2 h runs, permadeath, every run different,
  seed determinism, no story campaign, no protected content.
- **Main 4.6 (decided):** each floor has a theme that selects its tiles,
  enemies and hazards; bosses are hand-designed and drawn from a pool.
  **Main 6.3:** separate dungeon, combat, loot and recruit streams.
  **Main 10:** the generation algorithm is the builders'.
- **Five floors are already calibrated:** enemies 6.3's depth table and
  boss levels 3–4 / 6 / 8–9 / 10–11 / 12; depth% 80 / 90 / 100 / 105 /
  110 (10.1); level 10 at 540 "because floor 5 covers one level" (16);
  `expectedCompanions` 0 / 1 / 2 / 3 / 3 (companions 7.7); consumables
  arrays whose "length equals the floor count"; DH HE-6 and 18.2's
  "floors 3–5". A 4-floor run ends at level 9.
- **Enemies 10.4 (settled):** 4–6 fights plus the boss. Slots: patrol
  70, guard posts 3 × 100, lair 140, optional elite room 160, boss 200.
  Guarantees: a Stealth bypass, a patrol through cover, an awareness mix,
  a cowable group, a leader group from floor 2, a steal target. **20.1:**
  the experience pool must be 110–135% of need **after one bypass**.
- **Enemies 10.6 theme rules (settled):** a roster of 5–7 types over at
  least 4 roles, with types that can lead, be a minion, be promoted and
  be `armed`; at most 6 harmful conditions, counting riders and terrain;
  the water rule; weaknesses in at least 2 families; 2–3 at-will damage
  types; at least half non-fearless; one visual faction and one
  signature mechanic; Lore reveals the roster on entry; the camp
  next-floor line (HR-23).
- **Enemies 17.1 role taxes (settled):** brute: Shove (Prone) plus
  Grapple or Staggered on its limited ability; soldier: Shove; leader:
  Frighten; controller: an at-will minor from {Chilled, Poisoned,
  Corroded}; lurker: an opener from {Poisoned, Bleeding, Blinded}.
- **Implicit riders (settled, stat model 8.1):** slashing → Bleeding;
  bludgeoning → Staggered; fire → Burning + Smouldering (counts 2); cold
  → Chilled; acid → Corroded; psychic → Unfocused; necrotic → Withered;
  radiant → Blinded or Revealed. Piercing and lightning carry none.
- **Enemies 17.5 (settled):** bosses are hand-made, carry
  `eligibleFloors` (HR-21), and have no fallback; a floor-1 boss is never
  mindless and never psychic-immune (`Floor1BossMindReachable`,
  **agreed**, Herald Q3); resolve pips 2 / 2–3 / 3; phases at 50% on
  floor 1 and 66/33 deeper; no boss immunity to "the theme's counter
  type" (undefined until this design).
- **Conditions (settled):** 11: static squares, never spreading, minor or
  major only; the M2 squares are water, web, fire and oil. 11.6: traps
  belong to generation, with no trap hard control on floor 1. 12.2: no
  theme with every enemy immune to one tag. 16 and 18.1: the
  32-condition budget is full.
- **Floor-1 protections (settled):** no hard control; no lurkers in
  main-path rooms 0–2; no surprise at `mainPathIndex` 0–1 (DH 17.1); a
  cowable group before the boss (CL-10); `SeedHasTarget`;
  `Floor1ClassSpread` of at most 10 points inside the bands 50–74% and
  75–100% non-fearless (classes 5.6); floor-1 hero deaths under 5% (DH
  18.2).
- **Enemies 21 (lead's call there):** M2 is "4 themes of 5–6 types plus a
  deep overlay on floor 5"; enemies 22 names this the largest M2 cost.

### 3.2 What the approved documents owe this design

| Owed item | Source |
|---|---|
| Floor count, and what decides it | main 4.1's range |
| Theme assignment: pools, repetition, floor ranges | enemies 10.5 "theme × floor in its range" |
| The undefined "deep overlay on floor 5" | enemies 21, 22 |
| The undefined "theme's counter type" | enemies 17.5 |
| Theme data read by Volatile ("theme damage"), Hardened ("one theme layer"), Chilling ("frost themes") | enemies 17.2 |
| Floor pacing: level 3 at the floor-1 boss door | Shade design 18, Arcanist design lever 5 |
| Lever 3, "fewer or shorter fights on floors 4–5", in a concrete form | companions 7.4 (owed by the enemies owner; the form is supplied here) |
| The final floor and final boss | nobody names one |
| Tone, names, faction lore | main 10; enemies 2, 23 |

About 30 generation requirements are scattered across 9 documents;
section 14 consolidates them.

### 3.3 Tensions found and their resolutions

| # | Tension | Resolution |
|---|---|---|
| RE1 | "4–5 floors" against tables built for exactly 5 | Fixed 5 (RT-1) **(agreed, Q1)** |
| RE2 | Toy run of about 117–131 min; floors 4–5 at 27–29 min at party 4 | Levers 1–2 mandatory; RT-7 trim; cap-2 forecast **(told plainly)** |
| RE3 | 4 themes plus an overlay reads as a fixed sequence | 8 themes in tiered pools (RT-2, RT-5) **(agreed, Q1, Q4)** |
| RE4 | Level shift of ±2 (enemies 7.3) against themes valid at many depths | Eligibility bands of at most 2 adjacent floors |
| RE5 | Foresight against the bring-the-key anti-goal | Loot stays theme-blind; camp cards are class-blind; the Lore roster preview at camp waits for M3; a bring-the-key watch report |
| RE6, RE7 | Surreal visuals against silhouettes and a flat grid with no light model | The charter (7.4); rule-touching surreal ideas refused (7.6) |
| RE8 | The full condition budget | Every theme built from existing ids; one new square kind (bile) reuses Corroded (RT-13) |
| RE9 | Undead and construct themes against the non-fearless half | Every theme at 714‰ or more non-fearless, with at most 1 mindless type |
| RE10 | Floor 5 hard-coded as "deep" | Kept: floor 5 is the fixed final |
| RE11 | Mid-floor or two-theme floors | Rejected |
| RE12 | Unlockable themes are not in main 4.7 | RT-18 (M3, client) |
| RE13 | `Floor1ClassSpread` bands | The M2 floor-1 pool is all 75–100%; the Wake is conditional (8.2) |
| RE14 | Undefined counter type | Defined (8.0, RT-4) |
| RE15 | Armour's Resistant-element filter reads the theme | Kept as an M1 precedent; LootDoorInvariant stated around it (6.4) |
| RE16 | Level 3 at the floor-1 door | Antechamber Catch-up (RT-8) |
| RE17 | Arcanist DEP-A1 terrain density against theme budgets | Reported; floor 4 is dry when the Supper is drawn **(told plainly)** |
| RE18 | Requirements scattered over 9 documents | Section 14 |

### 3.4 Terms

| Term | Meaning |
|---|---|
| **Theme** | One floor's place: a faction (roster), environment, music, signature mechanic ("Law") and bosses, as one record (8.0) |
| **Tier** | A theme's home depth band, a pool and presentation tag only: Shallows (F1), Middle Dream (F2–3), Deep (F4), Heart (F5). The act shown on cards and the dream track comes from the **floor index**, never from `tier` |
| **Band pool** | A pool whose themes are eligible on two adjacent floors (in M2, the floors 2–3 pool) |
| **Drawable set** | `drawableSet(manifest, unlockSnapshot)`: the themes and bosses a run may use (6.4 R-SEED-5) |
| **Route** | The run's five themes and their bosses. The pre-roll fixes the permutation and boss orders at run start; each floor's theme is committed by the walk at run start (floors 1–2) or at the camp before it (6.4) |
| **Strata Draw** | The route pre-roll, its pool rules and its enumerator (6.4, 9.2) |
| **Door A / Door B** | The first and second surviving candidates of the candidate walk for a floor (6.4) |
| **Two Doors** | The camp choice between Door A and Door B (6.11) |
| **Last Hollow** | Camp 4; the **Last Hollow choice** is the pick between the two pooled final bosses |
| **Stratum Card** | The floor-entry card header (6.9; DH 17.12) |
| **Dream track** | The five-stratum route display on the HUD and pause screen (6.9) |
| **Keepsakes** | Floor-5 dressing and one arena prop drawn from the run's visited themes (9.3) |
| **Antechamber** | The main-path room immediately before the boss arena, the Forecourt, on every floor: empty on floor 1 (9.4); on floors 2–5 a patrol Circuit waypoint (floor layout design 6.11; change FL-5); the floor-5 Viaticum is never placed in it (read, not changed; change LE-5; loot economy design 6.11) |
| **Law / Obsession** | A theme's signature mechanic / its faction's absurd vow (7.3) |
| **Free 4** | A theme's harmful conditions other than the universal Prone and Frightened tax (8.0) |
| **Reference chooser** | The simulator's door policy used for calibration when Two Doors is on (6.11) |
| **Nightmare tiers** | M3 ladder tiers that add kind, not stats (9.1) |

---

## 4. How other games handle run structure

These come from memory plus light web checks in the research pass.
*(unsure)* marks uncertain details. Other games are cited as mechanism
references only.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Slay the Spire | 3 fixed acts on branching maps; act boss drawn from 3 and shown at act start; secret Act 4 | Showing the boss early turns information into planning; the final stage breaks the pattern | Fixed act order (Act 1 staleness is the classic complaint) |
| Hades | 4 fixed biomes of 10–14 chambers; Styx breaks the pattern | Biome length of 10–20 min is the genre norm; a known destination | Tartarus repetition on the most-seen stage |
| Hades II | Pick a route (Underworld or Surface) at run start | Route choice roughly doubles variety at low cost | — |
| Risk of Rain 2 | Each stage slot draws 1 of 2+ environments; no repeat in a loop | The brief's "A or B per tier" | A pool of 2 is learned fast; disliked maps become a dice tax |
| Dead Cells | A biome graph; harder exits pay more; boss tiers fixed | Choice tied to risk and build | Dominant routes at high difficulty |
| Binding of Isaac | 2-floor chapters with random alternates; secret routes; a ladder of endings | Unlocks drive return play | Two-floor themes (they work only with about 5-min floors); obscure secrets |
| Spelunky 2 | Branching worlds chosen at exits; a long secret route | A later secret route (the Lucid Key) | Obscurity |
| Enter the Gungeon | Fixed chambers, boss drawn from a pool per floor | A random boss is the cheapest variety per floor | — |
| Darkest Dungeon 1–2 | Regions change which tools are good; DD2's road with inns; a narrator's grim irony | Themes change the *kind* of tool needed; irony softens loss without mocking it | DD2's run length (1.5–2.5 h) |
| Into the Breach | Islands in any order; a telegraphed environment rule per island; fixed finale | One readable, deterministic environment law per theme | — |
| Monster Train, Balatro | Boss shown at the start of the stage; Balatro's ante 8 draws from a separate showdown pool | A separate final-boss pool | — |
| Inscryption, Loop Hero, Hylics | Surreal framing over readable rules | Surreal art and premise over ordinary, honest mechanics | Randomness as humour (the Hylics critique) |
| Cult of the Lamb | Cute plus cruel; serious mechanics | Contrast sells when mechanics stay serious | Cute *hostile* units |
| Shiren, DCSS, Brogue | Depth-banded pools; DCSS's Lair draws 2 of 4 sub-branches | Themes valid in a depth band | Hours-long runs |
| Noita | Holy Mountains between biomes (shop and heal) | Our camps' cadence | Fixed order |
| Mythic-underworld OSR (Philotomy; megadungeons) | The dungeon has its own laws; deeper is stranger; a level is a faction plus an ecology | A theme is a faction plus an ecology plus one law; surrealism rises with depth | — |
| Made in Abyss | Depth as transformation; wonder curdles into horror | The tone's depth dial | — |

**Combined lesson:**
- (a) variety matters most on the most-seen stage (floor 1) and on the
  stages where players lose most (floors 3–5);
- (b) random pools are cheap variety, but pool partners must differ
  mechanically;
- (c) route choice is praised when the options are balanced and
  criticised when one dominates;
- (d) a theme should last about one of our floors;
- (e) the final stage breaks the pattern;
- (f) unlocks and secret routes drive return play;
- (g) showing the next boss enables planning.

---

## 5. Structure models

### 5.1 The three models

Three complete models were built and judged by three independent
judges.

- **Model A, tiered pools.** The brief's A/B idea, refined: a band pool
  of 3 for floors 2–3, a run-start route pre-roll, and pool rules proven
  by enumeration. No player choice in M2.
- **Model B, acts.** Two-floor act themes, each with a "shallow face"
  and a "deep face", plus a finale.
- **Model C, route choice.** A door choice at camps 1–3, eligibility
  bands, a whole-catalogue permutation pre-roll, LootDoorInvariant and
  door bounties.

### 5.2 Verdict and judges' scores

Averaged over three judges (1–10, higher is better):

| Model | Variety and replay | Decision quality | Balance safety | Fit with docs | Pacing | Low build cost | Total |
|---|---|---|---|---|---|---|---|
| A, tiered pools | 6.7 | 5.0 | 8.0 | 8.3 | 7.0 | 5.3 | 40.3 |
| B, acts | 3.7 | 4.7 | 5.7 | 6.3 | 5.3 | 6.3 | 32.0 |
| C, route choice | 9.0 | 9.0 | 6.0 | 6.7 | 7.0 | 4.0 | 41.7 |

**Verdict (lead's call; agreed in substance, Q1 and Q2).** Use **Model
A's spine, built on Model C's pre-roll.** C plays best but is the
hardest to calibrate and test. A is the safest first build and already
produces the per-floor candidates C needs.

- The hybrid ships tiered pools.
- **Two Doors is restricted to the Middle Dream floors** (camps 1 and 2)
  in M2, plus the Last Hollow choice at camp 4, and is switched on only
  if the gates pass **(agreed, Q2)**. Floor 4 stays random until its
  pool reaches 3 (M3).
- Model B survives only as presentation (act palettes and music) and as
  per-tier dressing variants.

### 5.3 Grafts

- **From C:** the fixed-count, whole-catalogue permutation, so unlocks
  never reorder seeds; the candidate walk and its pair and contrast
  rules; LootDoorInvariant; Reference chooser calibration.
- **From A:** the band pool; stratum dressing variants; role-tax
  accounting; the floor-1 catch-up.
- **From the numbers check and the critiques:** parity gates measured
  from fixed arrival states; ClassThemeSpread; a plain trim with the
  removed experience moved to floor 4's boss (300) and floor 5's lair
  (240), in place of concentrated guard posts (10.2).

**Rejected:** door bounties, two-floor acts as a rule, a variable floor
count.

---

## 6. Run structure rules in full

### 6.1 Floors per run (agreed, Q1; change RT-1)

A run is **exactly 5 floors** **(agreed, Q1)**. Main 4.1's "4–5 floors"
is **decided** text; RT-1 amends it.

| Option | Verdict | Why |
|---|---|---|
| Fixed 5 | **Adopted (agreed, Q1)** | Every approved table assumes it; camp 4 exists; level 10 lands on floor 5 |
| Fixed 4 | Rejected | Ends at level 9; strands the third companion slot; still fails the floor-time gate on floor 4 (10.1), so it does not fix the clock |
| Variable 4–5 | **Rejected (Q1 option C)** | Two calibrations, two DH readings, nothing gained by the player |
| 6 or more, or optional extra floors | Rejected | The clock is already full |
| "Short Descent" mode | Later | A separate mode with its own curve |

**Rejected with Q1:** B, the brief's literal A/B per floor (two
separate pools of 2 on floors 2 and 3: 16 sequences instead of 24, one
more theme to build); C, variable 4–5 floors.

### 6.2 Cadence

- **One theme per floor (settled, main 4.6).** It changes at every
  floor, and so after every boss.
- **No theme spans two floors.** A theme on the floors 2–3 band appears
  on *one* of them per run.
- **No mid-floor transitions.** Later "seams" are display-only: the boss
  arena's far exit shows the next theme's palette, with no enemies or
  rules from it (M3). The far exit is the Bossgate to the Stairhead,
  which opens on `BossDefeated` (floors 1–4); floor 5 has none, and a
  waking-light window dresses its far wall (M3, display only) (change
  FL-6; floor layout design 6.11).
- **Acts are presentation only (lead's call).** One canonical set is
  used on the dream track, cards and music:

| Act / tier | Floors | Surrealism | Party (settled schedule) |
|---|---|---|---|
| **Shallows** | 1 | Near-grounded: one wrong detail per theme, repeated in every room | solo |
| **Middle Dream** | 2–3 | Uncanny, impossible architecture | duo → trio |
| **Deep** | 4 | Sublime, horrific; physics wrong in the backdrop only | four |
| **Heart** | 5 | Full dream logic in the dressing, played quiet | four |

### 6.3 Pools and depth ranges (agreed, Q1; change RT-2)

| Tier | Floors | M2 pool | How chosen (M2) | M3 | M4 |
|---|---|---|---|---|---|
| Shallows | 1 | Gaol of Small Hours, Kennel Wood (+ Endless Wake if it passes its gate, 8.2) | Random at run start | + Wake (if not M2), + Ministry (F1–2 window) | + Night Market, Orchard |
| Middle Dream | 2–3 band | Drowned Choir, Silk Archive, Carnival of the Last Evening | F2: 1 of 3; F3: 1 of the remaining 2. Random, or Two Doors when on | + Moth Wedding; the Long Supper widens to F3–4 | 5 |
| Deep | 4 | Long Supper, Gut of the World | Random | + Mirror Court (F3–4), + Ossuary Opera; Two Doors at camp 3 once the pool is 3 or more | 4–5 |
| Heart | 5 | The Dreamer's Heart (fixed); final-boss pool of 2 | Fixed; boss random, or picked at camp 4 when the Last Hollow choice is on | Final-boss pool 3 | + alternate final, the Waking City |

- **Band limits (settled, enemies 7.3).** A type's ±2 level shift covers
  two adjacent floors but not three. Bands are therefore at most 2
  floors, and every (theme, eligible floor) cell needs its own boss.
- **The Long Supper is floor-4-only in M2 (lead's call).** As a floors
  3–4 band theme, the enumeration (10.3) gives the Gut about 85% of
  floor-4 slots, exactly the staleness the fun critique flagged on the floors
  players replay most. Floor-4-only gives a true 50/50 pool. The band
  opens in M3, when Mirror Court and Opera dilute floor 4.
- **Pool partners differ in kind (lead's call).** The Middle Dream "fire
  triangle": the Choir punishes fire (Soaked resists it); the Archive
  rewards fire (it burns webs and paper); the Carnival cuts both ways
  (Oiled). The Deep pair is "endure or shatter": the Gut is attrition
  against healing and armour; the Supper is freeze-and-shatter against
  slow, clumped parties.
- **One home theme per square kind in any M2 set (lead's call):** water
  → Choir, web → Silk, oil and fire → Carnival, bile → Gut. The only
  exception is bedsheet webs on the Heart, because the Silk can never be
  adjacent to floor 5. The rule covers condition square kinds only: the
  condition-free square classes (harm, Rough, Plank) are theme-neutral
  (change TR-11; room terrain design 6.2). The Gaol, Kennel and Supper
  are deliberately dry, read as "no condition squares": condition-free
  squares appear there (the Supper's shattered-crystal harm squares;
  Rough and Plank in all three; change TR-22, told plainly; room terrain
  design 7.1; kept for the Kennel's Nightmare tier 2+ staked pits, which
  are condition-free harm squares, change HZ-25, hazards design 6.19);
  their identity comes from a visible
  `signatureFeature` (8.0).

### 6.4 Selection and seeding (agreed, Q1; change RT-3; seeds-segment hook)

- **R-SEED-1, the stream.** Main 6.3 gains a `route` child of the
  dungeon stream (the `recruit` substreams are the precedent). Each floor
  generates from a dungeon child keyed by **floor index**, and the theme
  is an input (enemies 10.5 step 1). The route stream is consumed only at
  run start, so no generation retry (HE-7's retry, the keyed
  `attempt/{a}` child of the floor key, attempt 1; change SV-4; M1;
  seeds and validation design 4.3, 5.5), player action or
  door choice can perturb it. Each floor's dungeon child has a `layout`
  child (under its `attempt/{a}` node; change SV-4), drawn with fixed
  counts per Blueprint; dressing draws from a
  display-side stream the core never reads (change FL-2; floor layout
  design 6.1).
- **R-SEED-2, when.** The draw happens at run start, after hero
  creation. Hero creation stays theme-blind, so the class pick is not
  made against a known floor-1 theme and `Floor1ClassSpread` stays
  honest. **The one exception is a launcher streak** (R-SEED-6): after
  three runs on the same floor-1 theme the next one is forecastable; the
  forecastable share is recorded in the `Floor1ClassSpread` notes
  **(told plainly after the check)**. **Launcher Fallow (change EV-20;
  agreed, events Q1; M2; events design 4.5.4):** a Fallow redraw
  (R-SEED-6) is recorded in the same notes; it reads the forecast F1
  Curiosity, not the class, and `Floor1ThemeShare` keeps each drawable
  floor-1 theme within 1/n ± 5 points.
- **R-SEED-3, fixed-count draw order (lead's call on the details).** All
  lists are sorted by stable id, with no unordered iteration
  **(settled, main 6.3)**. Every bounded integer is drawn with **one
  64-bit draw per index** by a fixed method (multiply-shift), never by
  rejection sampling, so the number of draws never depends on the data.
  **Key algebra (change SV-1; lead's call; M1; seeds and validation
  design 4.1):** a child derives from its parent's key; every bounded
  integer uses `Bound` (multiply-shift, asserting n ≥ 1), with reference
  vectors. **Stream registry (change SV-2; M1; seeds and validation
  design 4.2, 4.3):** every draw table here is a registry row with a
  count class (`boss9` is a 4-long `vector`); every approved count is
  unchanged.
  The floor `layout` child follows the same rule: a fixed draw count per
  Blueprint, every draw consumed even when one option is legal (change
  FL-2; floor layout design 6.1). So does the `hazards` child of the
  `terrain` stream, with fixed draw counts and no draws at act time
  except saves (change HZ-28; hazards design 11.2). So does the
  `encounters` child of the floor's dungeon substream, drawn after
  `traps` and before `trapsResolve`, with fixed draw counts and dummy
  draws, and its one retry on a keyed child of it (change EP-1; M1;
  encounter placement design 6.1, 6.19; segment 11 owns the tree). So
  does the `lootPlan` child of the loot stream, one per floor keyed by
  floor index, each consumer on its own keyed grandchild with a
  reserved draw count (9 per identity draw) consumed even when unused,
  and the approved enemies 10.5 step 3 order kept as the order of keyed
  per-floor, per-entry blocks, the dummy and ghost draws retired (change
  SV-5; M1; seeds and validation design 4.3); floor N+1's generation is committed with the camp-exit
  save (change LE-14; M1; loot economy design 6.10). So does the
  `curiosities` child of each floor's dungeon substream: a fixed 12
  draws per floor *(illustr.)* in a fixed draw map, every draw consumed
  whatever the floor hosts; EventDraw runs after layout and before
  hazards and reads no host, loot, party, run choice or profile memory,
  and EventPlace after LootBind is draw-free (change EV-21; M2; events
  design 4.10; segment 11 owns the tree). So does each boss record's
  `boss9` keyed child of the route seed (`BossVariantKey`): a fixed 4
  draws, never consumed on the route stream, picking the Rendition and
  its mirror bit after this pre-roll fixes the boss; arena stamping makes
  no draws (change BR-2, BR-23; M2; boss rooms design 4.10.1, 4.15). In
  order:
  1. **the permutation:** a keyed sort of the **whole theme
     catalogue in the manifest, locked, held and cut themes included**,
     by `H(route, "themePerm", themeId)`, ties by stable id (change SV-8;
     lead's call; M1 rule; seeds and validation design 4.3; replaces
     the Fisher–Yates permutation, so the route stream takes 0
     sequential draws);
     the drawable set filters it afterwards, so unlocking a theme never
     reorders the others;
  2. **the floor-1 theme** is the first floor-1-eligible theme of the
     drawable set in the step-1 permutation. There is no separate draw;
     a third floor-1 theme (the Wake) takes its 1/3 share automatically;
  3. **the boss orders:** for every (theme, floor) cell of the
     catalogue, sorted by (theme id, floor), with themes marked `final`
     skipped, a keyed sort by `H(route, "bossOrder", themeId, floor,
     bossId)` (change SV-8; M1 rule) over **every** boss record whose
     `themes[]` and `eligibleFloors` match the cell, locked ones
     included. The boss used is the first drawable entry; floor 1 also
     filters by `Floor1BossMindReachable`. This moves the boss draw out
     of enemies 10.5 step 6 into the pre-roll, so the door cards (Two
     Doors), the camp-4 preview and the Stratum Card can name the boss
     without consuming floor substreams;
  4. **the final-boss order:** one keyed sort by `H(route, "finalOrder",
     bossId)` (change SV-8; M1 rule) over every boss
     record marked `final`, locked ones included. It is the **only**
     source of the final boss: the preview (and the random build's final
     boss) is its first drawable entry; the Last Hollow choice offers its
     first two drawable entries, whatever the pool size;
  5. M3: one omen index per theme of the catalogue.
- **R-SEED-4, the candidate walk** for floors 2–4 (and the same walk
  gives floor 1, step 2). It uses **no RNG**: it reads the step-1
  permutation, the drawable set and the committed history.
  - Walk the permutation and keep themes eligible for the floor,
    drawable and unused (RP1).
  - **R4, soft:** skip a theme that shares the previous floor's
    `primaryDamageType`, or its `squareKind` when that is not `none`
    (`none` never matches), unless that leaves fewer than 2 survivors;
    then keep the unskipped list.
  - **Door A** is the first survivor. **Door B** is the next survivor
    that differs from Door A in `primaryDamageType` or in a non-`none`
    `squareKind` (R3, soft); if no later survivor differs, Door B is
    simply the next survivor.
  - **R3 and R4 use the same thresholds in both builds**, and the walk
    always computes both doors, so a doors-on run that takes Door A at
    every camp is identical to the doors-off run (asserted by V6).
  - In the random build the floor takes Door A, which is exactly the
    brief's A/B.
  - **When the walk runs:** floor 1 and floor 2 at run start; floor N
    (N = 3, 4) at camp N − 1, from the committed theme of floor N − 1.
    The walk is deterministic, so it gives the same result whenever it
    is recomputed (on load, for example).
  - Every rule is an **authoring-time property** checked over every
    reachable history by the enumerator (V5, 11.2); the draw never
    rejects.
- **R-SEED-5, drawable set, reproducibility key, save and sharing (lead's
  call on the details).**
  - **One function decides what is drawable:**
    `drawableSet(manifest, unlockSnapshot)`. **Manifest data:** each
    theme's and boss's `shipStatus` (`core`, `conditional-passed`,
    `conditional-failed`, `cut`, `held`), which records the cuts (12),
    the Wake verdict (8.2) and readability or parity holds (7.4, 11.4).
    **Profile data:** the unlock snapshot (M3). A theme is drawable when
    its `shipStatus` is `core` or `conditional-passed` and it is
    unlocked (M2: every shipped theme is unlocked).
  - **The reproducibility key** is (seed, `genVersion`, content-manifest
    hash, configuration-snapshot hash, unlock-snapshot hash) (change
    SV-12; lead's call; M1 fields, M2, M3; seeds and validation design
    4.6): `genVersion` is the generator's code version; the manifest
    hash is the canonical `manifestHash` over rules-relevant content,
    computed in the build pipeline (display text and art go to a
    separate `displayHash` outside the key; `cutState` and `shipStatus`
    sit inside the manifest); the per-floor generation identity is the
    key plus that floor's `GenInputs` digest. The
    **configuration snapshot** holds `route.twoDoorsEnabled`,
    `route.lastHollowChoiceEnabled`, `route.doorCamps[]`,
    `pacing.trimmedFloors[]` and `recruitSchedule.companionCap` (11.1;
    cross-document comparison 2026-11-23, CC-83), the `layout.*`
    fields of 11.1 (change FL-14), the `terrain.*` and `cover.*`
    tuning of 11.1 (change TR-17; room terrain design 11.1), and the
    profile grants that change rules (11.1; change SV-12). It is fixed at run
    start; **a run never changes mode mid-run (new lead's call)**. Across
    a patch this is kept: the configuration and unlock snapshots,
    `drawableSet` and the route state are never recomputed, and a
    mid-run patch changes only the generator version of future floors
    (read, kept; change SV-15; agreed, seeds and validation Q3; M2; seeds
    and validation design 4.10.1).
  - **Display memory (read, not changed; change EV-18; agreed, events
    Q1; M2, R3 in M3; events design 4.5.3, 4.10):** Curiosity
    Retellings, the Twice-Told card and Afterword lines are display only
    and sit outside the reproducibility key (`RetellingParity`,
    `DisplayMemoryInert`); M3 Sequel and M3-late or M4 Ripening stages
    (change EV-28; cross-document comparison 2026-11-23, CC-85) enter only
    through the unlock snapshot (9.1).
  - **Shared and daily seeds use the canonical unlock set** (all content
    of the current M-wave unlocked), never the sharing player's own
    profile, so two players with one shared seed get one route.
    **Seeded runs advance no progress (read; change SV-16, SV-17;
    agreed, seeds and validation Q1; M2 shared, Later daily; seeds and
    validation design 4.7, 4.9):** a run whose seed did not come from
    the launcher is marked seeded in run history and advances no
    unlocks, records, codex entries or launcher memory; it writes only
    the seen counts.
  - **The save** holds the route state of 11.1: the permutation, the
    boss orders, the final-boss order, both snapshots, the committed
    theme per floor so far and the `DoorChosen` log. The last
    `DoorChosen` before Travel on counts. `drawableSet` is computed
    from the unlock snapshot at run start and stored in the run save
    (seeds and validation design 4.4 GS0.2, answering 13).
- **R-SEED-6, launcher streak cap (lead's call; adopted for M2, new
  lead's call on adoption; streak length revised after the check).** The
  launcher, not the rules core, may redraw a fresh OS-entropy seed
  (masked to 60 bits; change SV-16; M2; seeds and validation design 4.3,
  4.9) (up to 8 times) if its floor-1 theme equals that of **each of the last
  three personal runs** (`recentRuns[].f1ThemeId`, written at run start
  for every launched personal run, outside the reproducibility key, as
  `f1BossId` below; cross-document comparison 2026-11-23, CC-6), **or if the forecast F1 Curiosity is in the
  profile's Fallow set (K = 3)** (change EV-20; agreed, events Q1; M2;
  events design 4.5.4), **or if the forecast F1 boss equals the F1 boss
  of both of the last two personal runs** (change BR-25; agreed, boss
  rooms Q1; M2; boss rooms design 4.10.3), so theme streaks cap at 3. Shared and daily seeds and the
  simulator bypass it. It never touches seed purity.
  - **Launcher Fallow (change EV-20; agreed, events Q1; M2; events
    design 4.5.4):** the forecast is the first draw on F1's
    `curiosities` child; the Fallow set is the cards displayed in the
    last 3 personal runs (`curiosityMemory.recentRuns`). Fallow shares
    the budget of 8 redraws and applies to the F1 Curiosity only. Per
    candidate the order is (1) the theme rule above, (2) Fallow, (3) the
    F1-boss streak rule (change BR-25), (4) the
    onboarding preference (a profile's first 2 runs prefer a MECH or
    short F1 card), which only scores candidates and never forces a
    redraw. On exhaustion the launcher keeps the best candidate by
    (passes the theme rule, passes Fallow, passes the boss rule, passes
    onboarding, earliest);
    if no candidate passed the theme rule, it keeps the original seed
    (a reading, since this rule states no such case; the RS owner
    confirms). **Pending request (seeds and validation design 11, 14.2;
    RS owner):** confirm this clause; if it is rejected, the
    best-ranked candidate is kept in that case too.
    Guards: `LauncherLootNeutral`, `Floor1ThemeShare`, `F1PoolParity`,
    `LauncherRedrawRate` and `LauncherCheckMs` (≤ 5 ms p95 per candidate
    *(illustr.)*, a report until measured; 11.4), and the new
    `LauncherF1Neutral` (change SV-13, SV-24; M2; seeds and validation
    design 6.10). This pulls 12's M3 row
    "profile-side anti-streak outside seeded runs" into M2.
  - **Launcher F1-boss streak rule (change BR-25; agreed, boss rooms
    Q1; M2; boss rooms design 4.10.3):** the forecast is the pre-rolled
    F1 boss record; the memory is `recentRuns[].f1BossId` on EV's
    `recentRuns` ring (MAIN 6.6 as changed by EV-19), written at run
    start for every launched personal run, outside the reproducibility
    key; the memory of 2 runs (`launcher.f1BossMemory`) is a client
    value, not tuning. It shares the budget of 8 and the bypasses above;
    `BossRepeatRate` is measured by a launcher emulation, since the
    simulator bypasses the launcher (LauncherEmu, which runs the real
    launcher code on sandbox profiles; the simulator writes no player
    profile; change SV-13; M2; seeds and validation design 4.6, 6.1).
  - **The F1 defect redraw (change SV-10; lead's call, after the check;
    M2; seeds and validation design 5.5):** when a personal run's first
    floor cannot be built at run start (the last attempt of the LR0–LR5
    ladder fails), the seed is silently discarded and the launcher
    draws a fresh candidate through the full launcher order with a
    fresh 8-redraw budget; the discarded candidate does not count
    against "up to 8". A seeded run shows that its code cannot be
    dreamed on this build.
- **Switching Two Doors on changes no seed (agreed, Q2).** The walk
  produces Door A and Door B in both builds; only who picks changes.
- **LootDoorInvariant (lead's call):**
  - for a fixed seed and party state, swapping the door gives the **same
    loot draw sequence and the same item identities**;
  - *placement* may differ (carrier or container);
  - the armour Resistant-element property reads the floor's drawn theme
    (armour 14.3, an M1 precedent) through **one always-consumed draw
    r in [0, 720)**, the same for every theme: with the theme's
    elemental damage types sorted by id (n of them), the element is
    entry r mod n; when n = 0 (the Gaol, the Kennel), the property uses
    a fixed non-theme fallback element with no extra draw (lead's call;
    V6 checks it); **this read serves authored Resistant records only
    (read, not changed; change IR-14, IR-15; M2; item rolls design 4.4,
    4.17):** a Resistant line **rolled** from a table takes its element
    theme-blind from its own `lineRoll` entry, and the parent draw r
    keeps its approved position;
  - loot stays theme-blind **(settled, enemies 15.3; trinkets R-23)**;
    **theme bias is 0‰ on identities, category weights, lane weights
    and rolled lines (read, not changed; change IR-15; agreed, item
    rolls Q4; item rolls design 4.4):** the theme reaches loot only as
    display, through `theme.loot` (8.0), rosters and signatures;
  - **signatures never feed exclusions (read, not changed; change IR-6;
    M2; item rolls design 4.7 item 7):** a boss signature writes no
    floor key, no Scion-line memory and no `generated[]` row (it lives
    in `LootLedger.bossContent[]`), so no deck identity depends on the
    door. **Pending request (change IR-13, IX21; RS owner; item rolls
    design 4.13):** confirm that signatures are boss content outside
    "the same item identities"; if refused, the question whether the
    boss's own signature may follow the door returns to the client
    (item rolls design 14.2);
  - **Element Echo (change IR-19; agreed, item rolls Q4: held for an M3
    client decision; M3 option; item rolls design 4.4):** offensive
    element-choice lines would read the found floor's `counterType`
    through the existing element draw, identities and draw counts
    unchanged; default off (`loot.elementEcho = false`), and a release
    build ships it off until the client adopts it (V14 (j));
  - the Muster's recruit draw never reads the next theme;
  - **Curiosities (read, not changed; change EV-7, EV-21, EV-22; M2;
    events design 4.4.1, 4.5.7, 4.10):** Curiosities grant no VP, coin,
    experience or draws; loot never reads them, EventPlace after
    LootBind is draw-free, and Strands read only committed run state at
    floor entry or at offer time, so the invariant holds;
  - **the Perilous Door (read, not changed; change LE-19; agreed, loot
    Q5; M3; loot economy design 6.12):** the fourth dreamed option of
    every width-3 offer is drawn on `lootPlan` from M2 on every seed,
    door or no door; the door only reveals it on that floor's boss
    reward card, so it changes what is shown, never what is drawn, and
    the invariant holds. **Pending request (change LE-19; RS owner):**
    confirm this reading; if refused, the door pays a profile mark and a
    memento-wall frame only (loot economy design 18.2).

**Pool rules (lead's call; validated by V3 and V5):**

| Rule | Statement |
|---|---|
| RP1 No repeat | No theme twice in a run |
| RP2 ContrastAdjacent | Two themes that can sit on adjacent floors share at most 2 of their free 4 conditions (Prone and Frightened excluded as the universal tax) |
| RP3 Morale floor | Floor-1 themes are 750‰ or more non-fearless in M2 (8.2); at most one theme per route below 700‰; never two consecutive floors below 700‰ |
| RP4 RouteSufficiency | Every reachable history has at least 1 candidate per floor, and at least 2 on floors offered as Two Doors |
| RP5 Weakness coverage | Each theme has 2 or more weakness families; each route covers all 3 |
| RP6 Damage spread | Every theme has 1 or more physical at-will; no damage type above 600‰ *(illustr.)* of the theme's at-will damage, computed over the roster **unweighted**: each type's at-will average damage counted once per type (a build-time figure; fill-weighted shares are a simulator report) **(lead's call)** |

### 6.5 The final floor and final boss (agreed, Q1)

- **The Dreamer's Heart** is always floor 5 (RT-2). It has its own
  roster (8.11), a morale shape that escalates room by room, and
  **Keepsakes** (9.3).
- **Final-boss pool:** 2 in M2 (the Night Governess, the Rousing
  Engine), 3 in M3. All are level 12, 3 pips, 2 boss actions, phases at
  66/33, and all their conditions come from the Heart's 6. Boss records
  gain `final` (RT-4).
- **Camp 4, the Last Hollow.** No theme choice. It previews the
  pre-rolled final boss (the first drawable entry of the final-boss
  order, R-SEED-3 step 4); when the Last Hollow choice is on (6.11), it
  instead offers the first two drawable entries of that order (RT-11).
  It is the deliberate spend-down camp: no camp follows floor 5, and
  coin left at its exit is lost (read, not changed; change LE-5; agreed,
  loot Q1; M2; loot economy design 6.11).
- **The approved floor-5 facts hold (settled):** the trader on floors
  2–4 only; Pledges on floors 2–4 (Later); the tier-4 scroll "before the
  floor-5 boss"; boss level 12.

### 6.6 Floor plan, length and room counts by depth

| Floor | Party (cap 3 / cap 2) | Fights + boss | Experience pool (after a bypass) | Main-path rooms *(illustr.; layout hook)* | Side rooms | Target active min *(illustr.)* |
|---|---|---|---|---|---|---|
| 1 | solo | patrol, 3 guard posts, lair, elite room (optional), boss | 710 (610) | 7, plus an empty **antechamber** before the arena | 2–3 | 15–17 |
| 2 | duo | same | 710 (610) | 8 | 2–4 | 17–20 |
| 3 | trio | same | 710 (610) | 8–9 | 2–4 | 19–22 |
| 4 | four / three | same; **with lever 3: 2 guard posts, boss experience 300** | 710 (610) | 8, or 7 trimmed | 2–4 | ≤ 20 (gate) |
| 5 | four / three | same; **with lever 3: 2 guard posts, lair experience 240** (boss 200) | 710 (610); before the boss 510 (410) | 7 | 2–3 | ≤ 20 (gate) |

- Floors 4–5 still hold "4–6 fights plus the boss" **(settled, enemies
  10.4)** and keep every 10.4 guarantee: the leader group (guard post or
  lair), the cowable group (patrol Scavengers; every floor-4 and floor-5
  theme has a skittish type for it), the Stealth bypass, the patrol
  through cover, the awareness mix and the steal target.
- **Why floor 5 differs (lead's call, after the check).** Floor 5's boss
  experience is paid when the run ends, so the hero never spends it. On
  floor 5 the trimmed 100 therefore moves to the lair (240), which keeps
  the experience earned **before** the final boss at the baseline 510
  (410 after a bypass). On floor 4 the boss pays 300, as the next floor
  uses it.
- **Lever 3 is applied only in the lever order of companions 7.4**
  (levers 1–2 first, then lever 3, then onward to the cap-2 fallback),
  never by default **(lead's call, companions 7.4; levers 5–6 and the
  cap-2 drop agreed, companions Q2; told plainly here)**.
- **The switch is configuration:** `pacing.trimmedFloors[]` (default
  empty; lever 3 sets [4, 5]; the 10.2 response may set [3, 4, 5], with
  floor 3 trimmed as floor 4) sits in the configuration snapshot (11.1).
  The floor-plan step reads guard-post count and boss or lair
  experience from it **(lead's call)**.
- Between 1 and 3 main-path rooms per floor are empty or special (the
  OSR stocking lesson; placement hook). On floors 2–5 the antechamber
  (the Forecourt, 9.4) counts as one, read as **special** (it is the
  patrol's homeRoom and fight host), so untrimmed floor 5 has no empty
  room **(told plainly; change FL-5)**. **Curiosities (change EV-2;
  M2; events design 0.1, 4.2):** about one Curiosity per floor on
  floors 1–3 (floor 1: a `curiosityHost` signature room, else an Alcove
  set piece, else a free leaf side room, never main-path rooms 0–2;
  floors 2–3: a Q room, one of these empty or special rooms; floor
  layout design 6.5), a Forecourt Interlude on floors 2–5, and Glimpses
  on walls; in rooms layout already rations, never an added room; never
  in an arena; one per room.
- **Counting (change FL-4; floor layout design 3.4):** n main-path rooms
  count entry (index 0) through arena (n−1); the floor-1 antechamber is
  extra to the table's 7 but inside n (n = 8, the antechamber at n−2);
  the floors 2–5 antechamber is inside the table's count. The elite room and
  Circuit side rooms count as side rooms; the Bypass arm, Alcoves,
  corridors and the Stairhead are not counted rooms; the Circuit counts
  Room nodes. "Room k" means `mainPathIndex` k everywhere, except
  enemies 10.2, whose "Room N" stays room level as defined there.

### 6.7 Run length

- Levers 1–2 plus the lever-3 trim: about **104–115 min** *(illustr.)*.
- Without levers, the toy default is 131 min, over the 2-hour ceiling.
  Levers 1–2 are therefore mandatory (10.1).
- Two Doors adds about 15 s per camp when on.

### 6.8 Camps (companions 7.11 order kept; change RT-10)

The approved camp order of companions 7.11 stands. This design inserts
one step **(lead's call; the step number is a new lead's call)**:

| Step | Camp order (companions 7.11, with step 6a added) |
|---|---|
| 1 | Downed companions get up |
| 2 | Camp catch-up |
| 3 | Restore |
| 4 | Lingering conditions end |
| 5 | +1 supply |
| 6 | The hero's potion top-up |
| **6a** | **The next-floor card** (random build), or **both door cards** (Two Doors on); at camp 4, **the final-boss card** or **the Last Hollow choice** |
| 7 | Snapshot and card A |
| 8 | Muster |
| 9 | Vendor |
| 10 | Shop or reward **(decided)** |
| 11 | Travel on, which commits the door; floor N + 1 is then generated (items 10.4). Its room graph is party-agnostic; the party band picks only room size classes **(told plainly; change FL-2; floor layout design 6.1)** |

Information comes before the Muster, so hires, swaps, purchases and
Herald card swaps can respond to it. The chosen door can be changed
until Travel on (the camp state `nextTheme`).

- Camps 1 and 3 are dressed in the current act's palette; camp 2
  carries a bigger act-change moment; camp 4 is the Last Hollow.
- A recurring wakeful camp keeper runs the vendor (display only).
  **Afterword (change EV-26; agreed, events Q1 for the memory model;
  M2 lean, M3; events design 4.5.10):** in M2 the run-start card shows
  at most one Afterword line keyed to how the last personal run ended
  and at most one Strand closure line; in M3 the keeper gains lines
  that name carried Strands, and the Unwoken Roll; all display only,
  never humour on death, never the Dreamer.
- A lullaby on a music box plays at every camp and degrades with depth.

### 6.9 What the player sees ahead (change RT-10, RT-12)

| Surface | Random build (no choice) | Two Doors on |
|---|---|---|
| Run-start card | Floor-1 theme name, vignette, HR-23 line | Same |
| Camp next-floor card | Name, vignette, the approved HR-23 line "Next floor: theme X, about Y% non-fearless"; **M3:** a Tithe Bowl Surveyor's Sketch shows only as "Bought: Surveyor's Sketch", and every pointer resolves at the entered floor's entry or use, never on a camp card (change XI-24; exploration and information design 4.6.3) | Per door: name, vignette, HR-23 line, signature line, at-will damage icons, square-kind icon, boss name and silhouette; **M3:** no pointer result on a door card (change XI-24) |
| Floor-entry **Stratum Card** (DH 17.12 header) | Name, act, signature line, damage types, non-fearless band, boss name; the floor count line "Main path: n rooms; side rooms: k; secrets: s" (s from floor 2; counts only, never locations; change FL-20, floor layout design 9.2; **M3:** narrowed for paid or earned pointers: this card and every free surface stay counts-only, and only a paid or earned pointer (the Lodestring, an F-TREASURE fact) may name one room, drawn on the map; change XI-23; exploration and information design 4.6.2); **M3:** the floor's Mood, Omen and Nightmare only, with its stage and every threshold visible from floor entry (change HZ-20; hazards design 6.9); **loot lines (M2; loot economy design 6.15),** at most 1 line each, after the RS facts and the DH guarantees: the count "Treasures: n" (change LE-15; loot economy design 8.2), then announcements: the Dream's Due line naming its target, never the item ("The dream owes Brask something."), turning to a "paid" state once shown (change LE-13; the Due itself on floor 1 from M1; loot economy design 6.6); on floors 2–4 "A heavy purse walks this floor" (change LE-16; loot economy design 8.3); on floor 5 "Before the Heart's chamber, the dream sets out provisions: choose one" (the Viaticum; change LE-5; loot economy design 6.11) | Same |
| Lore reveal on entry | Roster types and roles **(settled, enemies 10.6)** | Same; M3: roles shown at camp too |
| **Dream track** (HUD, pause) | 5 strata: visited floors named; the next floor named at its camp; later floors show only their tier glyph; floor 5 reads "The Dreamer's Heart" from minute one | Plus "the road not taken": the declined door, shown on the run summary too |

Why the random build shows less at camp: full foresight with no lever is
a "foretold dice tax" (fun critique). The signature and boss arrive on
the Stratum Card instead. Every displayed fact is a public rule;
nothing hides a rule change. All of it is class-blind and
information-only. **The elite intel line (change LE-18; M2; loot
economy design 6.9, 6.15):** when a floor's elite is resolved or its
notable is stolen, the party learns one non-power boss fact for that
floor (the boss signature item's card), shown in the loot summary's
floor-fact slot (enemies 15.8) and kept under the boss name on the
pause screen's Floor-info panel; it is not a Stratum Card line.

**Card queue and naming (change XI-19; M2; exploration and information
design 4.9.1, 4.9.8, 4.9.9).** The Stratum Card is two-tier: a modal
card at floor entry, last in the card queue, dismissed by the first move
command (0 clicks), with at most 4 body rows, then the Stratum strip
(top-left); its overflow (the Lore roster, F-BOSS lines, the elite intel
line, the Strand line) goes to the pause **Floor-info panel**, and on a
profile's first two runs a one-line ribbon "More in Floor info (F)"
shows for one floor after the card closes. Its floor count line is the
**count line**; floor layout design 9.2's "treasures: k / n" on it is
read as the floor strip's **Treasure field** (loot economy design 6.15).

### 6.10 How runs differ

Run-to-run variety comes from the floor-1 draw, the band order, the
floor-4 draw and the final boss; 15 bosses over 11 (theme, floor)
cells; shallow and deep dressing sets; and layouts, recruits and loot.
Counts are in 10.3. From M3, unlockable strata, omens and Nightmare
tiers add more (9.1).

### 6.11 Two Doors and the Last Hollow choice (agreed, Q2; change RT-10, RT-11)

**The rule (agreed, Q2):** Two Doors (the pick of the next floor from
two pre-drawn candidates at camps 1 and 2) and the Last Hollow choice
(the final-boss pick at camp 4) are **built in M2 and switched on only
if the gates pass**. Otherwise the random draw ships (each floor takes
Door A; camp 4 previews the pre-rolled final boss) and the doors arrive
in M3, with no structural change and no seed change. Calibration runs on
a **Reference chooser policy**. It amends the decided main 4.1 camp
bullet (RT-10, flagged). **Rejected with Q2:** B, random only until M3;
C, doors regardless of the gates; D, never.

**What it is:**
- the door camps are `route.doorCamps[]` in the configuration snapshot
  (M2: [1, 2]); they show Door A and Door B from the walk, with the full
  door card (6.9);
- camp 3 (floor 4) stays random until the floor-4 pool reaches 3 (M3,
  which adds 3 to `route.doorCamps[]`);
- **a non-door camp always shows the trimmed random-build card**
  (6.9), whatever the switch (lead's call);
- camp 4 offers the first two drawable entries of the final-boss order
  (R-SEED-3 step 4).

**Two switches (new lead's call).** `route.twoDoorsEnabled` (the door
camps) and `route.lastHollowChoiceEnabled` (camp 4) are separate
configuration switches, both off by default. The Last Hollow choice is
switched on only with Two Doors, and only when BossParity also passes;
Two Doors may ship with the Last Hollow choice still a preview. **A
configuration with `lastHollowChoiceEnabled` true and `twoDoorsEnabled`
false is rejected at load (lead's call).**

**Gates, all required (agreed, Q2; the first two named by the client,
the rest as proposed, with gate 6 added after the check; metrics defined
in 11.4; each gate blocks the
switch it names, never the build):**
1. **ThemeParity of 5 points or less** on the choice floors (2–3), read
   from **fixed arrival states** at both caps *(the threshold agreed;
   the method lead's call)*. Blocks Two Doors;
2. **no theme picked in more than 65% of the times it is offered**, read
   on human playtest sessions with the switch on behind a playtest flag
   *(the 65% agreed)*. **Decision rule (lead's call, after the check;
   illustr. sample):** per theme pooled over floors and partners, the
   point estimate is 65% or less **and** the upper 90% confidence bound
   is 70% or less, on about 150 or more offers per Middle Dream theme
   (40 offers leaves a margin of about ±15 points, too wide to judge
   65%); per (theme, floor, partner) cell, at least 40 offers and a
   point estimate of 75% or less, so a pooled rate cannot hide one
   lopsided pair. The Reference chooser's rate is reported beside it.
   Blocks Two Doors;
3. **ClassThemeSpread** (both measures of 11.4) of 10 points or less on
   the choice floors. Blocks Two Doors;
4. **the Oracle chooser's absolute run-win rate is 333‰ or less** (the
   DH 18.2 baseline is stated beside it). Blocks Two Doors;
5. DH 18.2 calibrated on the Reference chooser (a change to DH 19.3,
   RT-10). Blocks Two Doors (read: gate 5 reads DH 18.2's metrics as an
   SW gate, while their calibration verdict stays CAL; change SV-23;
   seeds and validation design 6.3);
6. **per-build theme coverage** (11.4) within about 20% *(illustr.)* of
   random-draw coverage for every hero class (lead's call, after the
   check: turned from a report into a gate, so the doors cannot become
   "which theme do I never see"). Blocks Two Doors;
7. for the Last Hollow choice only: BossParity of 5 points or less for
   the final-boss pair. Blocks the Last Hollow choice.

ThemeParity is a **maximum over hero classes** (11.4), so a theme that
is easier for one class by more than 5 points already fails gate 1;
gate 6 then catches avoidance by preference rather than strength. A
per-class human pick rate is reported, not gated (the samples are too
small); a per-class pick rate from the Reference chooser is not used,
because a deterministic chooser reads 0% or 100% for any gap.

**The Reference chooser (new lead's call on its definition; keyed after
the check).** At a door camp it picks the door whose theme has the
higher floor-clear rate (11.4) in the ThemeParity table for the key
(hero class, party size, floor), read at that floor's fixed arrival
state; ties go to Door A. Companions are not part of the key: the fixed
arrival states carry a canonical companion list per party size. It sits
inside DH 19.3's reference party policy. A random chooser and an Oracle
chooser (full rollouts) are reported beside it; the Oracle cap is gate
4.

**Positive pull** (so the doors reward more than avoidance):
- theme milestone marks (M2), which become unlocks in M3;
- the road-not-taken line;
- per-build theme coverage (themes seen per 10 runs per class), now
  gate 6. If it fails, the levers are signature or roster tuning, never
  budgets.

**Fallback:** the walk's Door A, which is the brief's own A/B, with the
trimmed camp card (6.9). If a gate fails after M2's balance pass, the
switch stays off and the lead reports it to the client; the doors are
re-tested in M3 **(lead's call)**.

---

## 7. Tone and art direction (agreed, Q3)

### 7.1 Comparison

Scored 1–5, the lead's judgement *(illustr.)*:

| Option | Readability | Stakes / permadeath | SRD + morale fit | Art range | Distinct in market | Notes |
|---|---|---|---|---|---|---|
| Absurdist surreal, unruled (the idea taken literally) | 2 | 2 | 4 | 5 | 4 | Highest ceiling, highest risk |
| Grounded grimdark | 5 | 5 | 5 | 2 | 2 | Crowded (Darkest Dungeon, Battle Brothers); wastes a willing art team |
| Mythic or folk horror | 4 | 5 | 4 | 3 | 3 | The fallback register for floor 1 |
| New Weird | 2–3 | 4 | 2 | 5 | 3–4 | Alien minds blur the fear and mindless reads; borrow for single deep themes only |
| Whimsical-macabre | 4 | 2–3 | 4 | 4 | 2–3 | Cute hostile units undercut fear; a crowded lane |
| Absurdist comedy | 3 | 1 | 3 | 4 | 4 | Deflates every stake |
| **Grim Surreal, a dream that obeys rules (adopted)** | **4 with the charter** | **4–5** | **5** | **5** | **5** | Readability bought back by 7.4 |

### 7.2 The decision

**Grim Surreal is adopted provisionally (agreed, Q3)**, with the binding
readability charter RC1–RC9 and its M2 gates (7.4), a Tone Sheet per theme
(7.7), and the **uncommitted** Dreamer premise (7.5). **Rejected with
Q3:** B, no premise; C, folk horror; D, full absurdism.

- Surreal places, absurd motives, sincere stakes.
- Philosophical absurdism is the spine (Sisyphus *is* the roguelike
  loop); comic absurdism is seasoning only: names, item text, camp
  lines, and Yield and rout barks.
- **Morale:** factions with petty, sincere obsessions are non-fearless
  and flee, yield and cower; empty things are honestly mindless and
  psychic-immune (enemies 14.2; stat model 8.4). The fiction explains the
  morale data, and the art makes it readable.
- **Permadeath:** repetition inside a dream frames the loop: "you did not
  wake."
- **SRD:** stat shapes stay SRD under original names, which also serves
  the protected-content rule **(decided, main 2)**.
- **Provisional means reversible:** if the client later drops surrealism,
  rosters and rules stand and only skins, names and the premise change.
  The Heart, whose Law, roster and bosses lean hardest on the dream,
  carries a grim-reading rename for every type and boss (8.11), so its
  reversal is shown, not asserted.

### 7.3 Pillars (binding for art, audio, writing and UI)

1. **The dream is strange; its laws are not.** Every surreal idea goes
   through existing conditions, squares, riders, forced movement and
   morale. If it looks like it matters, it matters.
2. **Absurd in premise, sincere in stakes.** No humour on telegraphs,
   hits, downings, deaths or the death recap (DH 17.12 stays neutral).
3. **Every place has a Law and an Obsession.** The Law is the theme's
   signature mechanic (enemies 10.6). The Obsession is the faction's
   absurd vow, which sets its morale mix and its Yield fiction.
4. **Deeper is dreamier, and humour falls as deaths rise** (DH 18.2
   puts most deaths on floors 3–5): comic register allowed on floors 1–2,
   dry on 3–4, absent on 5. The register follows the **floor**, not the
   theme: a band theme authors one bark set per eligible floor (7.7).
   The registers **(lead's call)**:
   - **comic:** a joke the character knows is funny ("Sentence served,
     sir. Well, mostly.");
   - **dry:** deadpan sincerity, no punchline ("The hymn continues. You
     will not.");
   - **absent:** no wordplay and no comic premise ("Sleep.").

   **Curiosities (read, not changed; change EV-24; M2; events design
   4.11):** they keep this register; punchlines live in Retellings and
   Glimpse art, and a card drawable on floors of two registers is
   written register-neutral, in the drier one.
5. **Read it at a glance.** One visual grammar across every faction.
6. **Obsessions differ in shape (lead's call).** Each Obsession is
   tagged by kind (vow, debt, duty, grief, craft, vanity, hunger, rest).
   No two themes in one pool, or able to sit on adjacent floors, share
   a kind; the tone-by-depth review checks it. M2 tags: Gaol debt,
   Kennel duty, Wake grief, Choir vow, Silk craft, Carnival vanity,
   Supper hunger, Gut debt (never adjacent to the Gaol), Heart rest.

### 7.4 Readability charter (agreed, Q3; change RT-17 for the gates; display only)

| # | Rule |
|---|---|
| RC1 | **Mechanics never joke:** no fake tiles, no lying UI, no hidden rules, no "wild" random effects for flavour. **Dressing never mimics a unit, a square kind or a rule (lead's call, after the check):** statues and props share no silhouette with a roster type; a square kind's material (water, web, sheet, oil, bile) appears only on its squares and on walls above head height; no floor-level particle implies a condition; frost, rain and liquid read only as backdrop. **RC1+ (change TR-20; room terrain design 8.1, 8.2):** every object on a walkable square is a rules piece; dressing lives only on walls, backdrop, non-walkable squares or above head height; liquid-holding pieces are drawn dry or lidded; residue rule: nothing remains on the floor after an event resolves, except debris as Rough. **M3:** Remnant volumes are the one floor-level particle allowed to imply a condition (TR-27). **Hazards (change HZ-29; hazards design 8):** every Void is a Void, no fake holes; backdrop depth never reads as walkable; a fall leaves no residue; motion on walkable squares happens only during an act animation (≤ 1.0 s), never at idle, and only for the Turning Ring's rotation, Peristalsis's band step, a Tide's fill change and the Dreamer's heave; figments (the Stampede herd, Counting Sheep) exist only during their act, flat, translucent, non-unit and below unit value; **M4 (change LP-34; agreed, loot sources Q3; loot sources design 4.7.11):** the Ebb's fade, one notch per tick as an act of ≤ 1.0 s, joins this closed motion list. **Traps and locks (change TL-10; M2; traps and locks design 6.3, 8):** every device body over walkable squares is rigged, with no decoys; Hunch zones always contain a trap; no fake plates, empty "trapped" chests or lying hovers; every trap carries a diegetic tell on a wall, above head height or on the device body, never on a fake floor tile; snare and catch tells render only from Revealed, through a Reveal transition that reads as the object settling, never as a spawn; a forced lock shows a hanging padlock and a Splinter an empty frame with a jagged edge, with no floor debris; no trap tell moves at idle (idle motion on containers and Furnishings stays the lurkers', boxed lurkers included; change TL-28, a reading, M3). **Encounters (read, not changed; change EP-9, EP-12; M2; encounter placement design 6.7, 6.8.6, 8):** activities and postures keep their props on the model and off walkable squares; the Waylay's Spoor mark sits only on door frames and walls, never on floor squares. **Loot sources (change LP-7; agreed, loot sources Q1, Q4; M3; loot sources design 4.7.1, 6):** the Windfall Truant is a pre-placed unit, present at reveal, never a spawn, with a Dormant breathing idle and motion only in its closed act table of ≤ 1.0 s acts that reuse approved states (dormant, stand, hop, held, wait-at-hole, leave-by-Rathole, fade-with-bundle, nabbed, stolen); a loss always takes the bundle and plays the got-away cue, a gain always leaves a pile, and nothing remains after it resolves except a gain's pile; it cowers only while awake or held, never while Dormant. **Keepsakes (change LP-24; M2; loot sources design 5.1):** Keepsake dressing never uses a container, bait-cache, Windfall or Bedside Stand silhouette (9.3). **Curiosities (change EV-25; M2; events design 4.2, 4.11):** Curiosity props and Glimpses sit on walls, above head height or in a niche, never on a walkable square; every physical act is staged wall-mounted, dry or lidded, and any wet, spilled, shattered or burning moment appears only in the card's diorama; a resolved Curiosity goes to an inert state (a dust sheet above head height) and leaves no residue; no Curiosity prop, Glimpse or resolved state uses a Portal kind's silhouette or skin (a door appears only inside a diorama); card chips are printed from rules data (read, not changed; change EV-4; events design 4.3.1). **Pending request (change BR-38; RS owner; M2; boss rooms design 6):** boss arena Device idle and act motion (Device and leash-ring acts ≤ 1.0 s) joins this closed motion list; overhead Devices (the Pendulum, the Icicle Chandelier, the Heavy Thing) hang over non-walkable squares or a wall slot. **Exploration and information (change XI-5, XI-14, XI-15; M1/M2; exploration and information design 4.2, 4.5.1, 4.9.3):** the information charter (IP1–IP10) and the incentive charter (IN1–IN6) apply this rule to what the party knows: Remembered is never drawn as Seen, projected never as known, Chalked never as floor, and a learned fact carries a told pip; an Afterimage is labelled memory in its own paper cut-out grammar (never a unit silhouette or a hazard figment), shows the last-seen activity and never changes while unseen; Unknown is always "?", never "none"; a mark that loses its slot drops to a pip and its text to the hover or Inspect card, so nothing vanishes silently. Chalk on entry leaves FL-17's secrets as approved (read, not changed; change XI-3; exploration and information design 4.1.4) |
| RC2 | **Role prop grammar** (extends enemies 8.1): leaders carry a vertical emblem; soldiers a tall flat object (shield, door, tray, coffin lid, chair); artillery points something forward; brutes are oversized mass inside one square; skirmishers lean low; controllers have an orbiting element; lurkers show a tell at rest; minions are small and share one palette. **Pieces (change TR-20; room terrain design 8.1):** no terrain piece uses a chair, tray, door, upright coffin lid, shield, tall staff, forward-pointing tube, orbiting element or unit silhouette; chairs appear only tucked into tables; Low pieces that could read as shields come only as multi-square runs; no Furnishing or Low piece shares a silhouette family with its theme's current or planned Kegs. **Boxed lurkers (change TL-28, a reading; agreed, traps and locks Q2; M3; traps and locks design 6.14, 8):** a boxed lurker shows its own lurker's rest tell, re-authored at container scale and readable from 6 squares, and a face once revealed. **Pending request (change TL-43; RS owner and art; M2; traps and locks design 6.3, 8):** trap device bodies follow the piece bans above (no chair, tray, door, upright coffin lid, shield, tall staff, forward-pointing tube, orbiting element or roster prop). **Truants (change LP-7; agreed, loot sources Q1, Q4; M3; loot sources design 4.7.1, 5.3):** every Truant is a small, faced, non-fearless animal (RC3), never an object and never humanoid, hauling the one cross-theme bundle (undyed sackcloth tied with the Windfall mark, larger than the animal, visible in every pose, never an item silhouette); its eyes and limbs stay out in every pose, Dormant included; a Truant, its bundle and its Rathole never use its theme's faction motif or minion palette, so theme identity goes on the animal only (M3 pilot skins: the Choir's Alms Crab, the Carnival's Conjuror's Rabbit, the Supper's Crumb Mouse). **Curiosities (change EV-25, EV-45; M2 implied figures, M3 figure frame; events design 4.11):** no interactive chairs and no Portal silhouette on a Curiosity prop; M2 dream figures are implied only (hands with a plain cuff, a lamp and glove above head height, a back in a high window, empty coats on a rail), with no face and no faction motif or minion palette; the M3 **figure frame** is one cross-theme non-unit map object in a niche, booth or behind glass, never behind a Drape or on a walkable square, flat and paper-cut below unit value tier, cropped at the shoulders, with no roster silhouette |
| RC3 | **Faces mean minds:** mindless types show one cross-theme mind tell (a hollow keyhole motif) and no face; fearless-but-minded types are masked or veiled; non-fearless types have faces and visibly cower, flee and kneel (animations duplicate approved states, never replace them). **(Lead's calls, after the check.)** Bosses are exempt: the boss plate and pips carry their read, and the face grammar applies to non-boss types only. Helms on non-fearless types are open-faced (raised visor or none); veils on non-fearless types are lifted or pinned back. The keyhole silhouette is reserved: no environment, prop or non-mindless unit uses it (locks read as padlocks; change FL-7, told plainly). A Routed or Cowed animation never moves a unit through a door or changes a door. **Locks (change TL-22; traps and locks design 6.11, 8):** every lock reads as a padlock base decal plus a kind glyph, and theme locks are skins on that base (webbing over a padlock, a padlock on a sphincter's rim, a frosted padlock); no lock glyph uses a face part, a ring or a clock face; Wager cages carry no padlock, only the Wager glyph (M3); no container carries a keyhole (V7). **Encounters (read, not changed; change EP-9; M2; encounter placement design 6.7, 8):** Busy and Dormant postures duplicate approved states (the Dormant glyph stays). **Curiosities (change EV-25, EV-45; M2; events design 4.11):** implied figures and the M3 figure frame show neither a face nor the keyhole tell, under their own non-unit grammar; no Curiosity uses a clock-dial object that opens locks |
| RC4 | **Damage language:** family by shape (physical streaks and impacts; elemental fluids, particles, crystals; spirit rings and ripples); type by hue *plus* pattern, so it survives colour-blind simulation; no theme uses an undesaturated damage hue as its dominant environment colour. **The damage-hue key** (10 types, hue plus pattern) is owned by art and due before the first Tone Sheet. A theme whose Law is a damage type (Gut, Supper, Carnival) dresses its environment in a desaturated, off-key variant and keeps the saturated key for its hazard squares and attacks (lead's call). **RC4+ (change TR-20; room terrain design 8.1):** the saturated damage hue is also allowed on contact hazards in every F2–5 theme; fire hue stays limited to fire squares, Hot pieces and attacks. **M3:** Miasma Remnants take their own hue and pattern (a desaturated violet with a stipple, never bile green; room terrain design 6.6). **Hazards (change HZ-29; hazards design 8):** Voids carry the drop rim but no damage-hue fill; bile green stays reserved (the Gullet backdrop uses flesh tones); due overlays and latent footprints use the telegraph-tier pattern and colour with the damage-type icon; the fire hue appears only in act VFX and on fire squares or Hot pieces |
| RC5 | **One glyph per square rule:** each square kind has one cross-theme rim and icon; a theme reskins only the fill (bedsheets read as web because they *are* web). **RC5+ (change TR-20; room terrain design 8.1):** one glyph set per piece class and square class across themes, built from four composable marks (base, motion, danger, cover); every hazard face projects a floor decal onto its one front square; the stub-glyph rule (the "blocks sight" cap on a Tall piece's stub state) applies from M1. **Hazards (change HZ-29; hazards design 8):** one Void lip glyph, one Snag glyph, one Cord glyph with used and unused pips and one vent glyph with trigger pips across themes; a Stir uses its host's glyph plus a countdown numeral on the host's floor decal; the four-mark table gains the hazards design's RC5+ rows (Void, Snag, Cord, vent, latent footprint, due square, Tide socket, Lurch lane, Handhold, Chainpost, staked pit) and one pattern registry, in which no two patterns differ only by hue. **Traps and locks (change TL-22; traps and locks design 8):** the four-mark table gains the traps and locks design's RC5+ rows (Revealed trap trigger with a `triggerKind` sub-mark, device area, mechanism gear, trap link, Hunch zone, Hunch catch, state badges, lock, Wager Portal) and the pattern registry gains the trap rim, the Hunch dotted rim with question-mark ticks (never a hatch) and the trap-link dash; one lock glyph set across themes: Plain, Crier, Barb, Tattle and Keyed in M2, Runed, Hour, Riddle, Fed and Ticket in M3, and the High-Striker's threshold glyph. **Encounters (change EP-12; M2; encounter placement design 6.8.1, 6.8.6):** the pattern registry gains the **Spoor base mark** (one cross-theme three-stroke tally, apart from the trap-tell, lock and Hunch families, skinned per theme in material only) and the cross-theme **bait-cache mark**. **Pending request (change EP-12, extended; RS owner; M2; encounter placement design 6.20; cross-document comparison 2026-11-23, CC-103):** the four-mark table and pattern registry gain the encounter placement design's board read surfaces (the formation chip, the "Line broken" split frame, the Keystone notched-crown pip, the hold-square corner brackets, the fall-back chevron with its dashed tether, the marked-tell eye-tick ring and the seen-tell band marker), each gated on the greyscale and colour-blind checks. **Loot (read, not changed; change IR-13; agreed, item rolls Q5; M1; item rolls design 4.13):** the **signature cache** is one pattern-registry mark (base: cache; a signature sub-mark shared with the boss plate's "Guards: <signature name>" line). **Loot sources (change LP-7, LP-12, LP-14, LP-26; loot sources design 4.3, 4.7.3, 4.10, 6):** the four-mark table and pattern registry gain the loot sources design's RC5 rows, each with a positive shape, a slot and an RC6 tier: the **wall-set mark** (M2; an inset container's solid base bracket on the wall face, projected as a solid bracket onto its open square, never a dashed rim); **M3 (agreed, loot sources Q1, Q4):** the **Windfall mark** (a knotted-sack glyph on the plate and bundle), the **scurry trail** (paired dots, one pair per route square, never above floor tier), the **next-hop outline** (a solid pawprint outline, only while awake), the **Rathole** (one cross-theme arched opening at skirting height with the Windfall mark on its lintel, plus a front-square half-rim floor decal drawn whatever the wall's cutaway state; per-theme dressing changes only its material), the **count digit** (with a clock-pip on a floor ring, hollow while Dormant, solid while awake) and the **"held" badge**, and the Twin Coffer's **twin-lid mark**; **M2 (agreed, loot sources Q5):** the **Bedside Stand** is one cross-theme silhouette that hosts only the Viaticum. **Pending request (change LP-27; enemies owner; M2 if agreed, else M3; loot sources design 7):** the **Uncorked** state, a shape sub-state of the carried-notable glint (an open stopper notch), never a hue change, joins the pattern registry. **Curiosities (change EV-25; M2; events design 4.11):** no Curiosity prop or Glimpse uses a container, bait-cache, Windfall or Bedside Stand silhouette (`ReservedSilhouette`); a prop that is a container by name is fused into a wall bracket, rack or wall slot with no box body; no clock-dial object that opens locks. **Pending request (change BR-38; RS owner; M2, the Gambit mark M3; boss rooms design 6, 4.3.1):** the four-mark table and pattern registry gain the boss rooms design's marks: the Rhythm strip's three marks (Off dot, Lead diamond, Down area-shape glyph), the five phase-type icons (new move, stance, ward, reserve, terrain), "Upended" (renamed **Sprawled** by boss rooms design 4.7.2; this restatement's rename is pending with the RS owner; cross-document comparison 2026-11-23, CC-65), "Resolve Broken", the Gambit "Next turn" mark and one cross-theme **leash ring** for every `chain(n)` unit, each gated on the greyscale and colour-blind checks. **Exploration and information (change XI-5, XI-15, XI-17; M1 the knowledge states and basic Afterimages, M2 the rest, the Lodestring marks M3; exploration and information design 4.2, 4.9.3, 6):** the four-mark table and pattern registry gain segment 10's rows, each with a slot (fill, rim, glyph, pip, line or sprite), an RC6 tier and a `countsAsMark` flag per that design's registry table: the knowledge-state grade, the Chalked footprint and the fog bank; the seen-edge line; the Traced contour (map only); the Afterimage cut-outs in four states (Fresh, Persisting, Old, Short) and the stacked group cut-outs, with no numerals; the told pip; the projected pip on a Stir decal; the F-KEY Keybearer pip on a patrol route's last-seen pip; the loose-end thread tuft (map only); the Listen badge and bubble; the Waymark tack (its 6 inner shapes chosen outside the pattern registry); the sight-risk mark, ear badge, hearing double contour, noise single contour, patrol arrowhead line, "continues" ellipsis, forecast band and horizon bracket; the front-square wall projections; and, M3, the Lodestring spool mark (never a knot, greyscale-gated against the Windfall mark) and the Lodestring string. A **line-style registry** joins it: no two line families differ only by hue or only by dot spacing. Pattern ids are assigned by the RS owner |
| RC6 | **Value hierarchy:** units > telegraph overlays > hazards > floor > walls, checked on a greyscale screenshot per theme; surreal texture only on walls, ceilings and skyboxes, never on walkable squares; scale distortion in architecture only, every unit true to its square. **Boss scale (lead's call):** a boss may stand up to about 1.5 times standard height, but its footprint and silhouette stay within its square at minimum zoom and never hide a unit on an adjacent square; height is shown by proportion, not size. **RC6+ (change TR-20; room terrain design 8.1):** units > telegraph overlays (including Keg footprints while a preview or intent would burst them) > hazards (including contact hazards, harm squares, Kegs and their idle footprints) > cover shields > Furnishings and Low pieces > Tall pieces > floor (Rough, Plank) > walls. Rules pieces count as architecture for the scale clause (room terrain design 7.10). **Hazards (change HZ-29; hazards design 8):** telegraph overlays include due overlays and push and fall previews; hazards include latent footprints; a new bottom tier, the **Void backdrop**, sits below walls, its value ceiling below the floor's darkest walkable tone, rendered as a sealed shaft below the floor plane so nothing shows through at any of the 4 camera rotations. **Exploration and information (change XI-15, XI-17; M1/M2; exploration and information design 4.2, 4.9.2):** board priority extends this hierarchy without reordering it: units > lethal (K) > active tool lines (A) > due and room clocks (D) > telegraphs (T) > hazards (H) > traps and Hunches (R) > cover and pieces (C) > Portal and wall marks (W) > the information layer (I) > memory (M) > floor pattern (F) > knowledge state (N); the Remembered grade (a faded photograph) applies to tiers F and N only, and hazard, trap, Hunch and Portal marks are never desaturated; Unknown is a static, theme-tinted fog bank at wall-top height, never floor-plane darkness, so it never reads as the Void backdrop, and it drops to a low cap under the FL 8.1 cutaway |
| RC7 | **Tone-free UI:** plates, threat overlays, signature banners, joiner footsteps and condition icons use one global skin; boss moves may have diegetic names but use the universal banner and sting. **Gameplay cue sounds** (door open and close, joiner footsteps and horns, alarm bells, trap triggers, and the Choir's fallen-bell struck-bronze cue, kept distinct from the alarm bell; change TR-20, room terrain design 7.5) are reserved and never appear in a music bed or ambience (lead's call). **Hazards (change HZ-29; hazards design 8):** the reserved cues gain the party fall, a monster-removal cue, Cord Operated, the theme-neutral **Dream tick**, two layers per Stir (an announcement sting and an act cue) and the floor-1 small-hour chime, Horn Post rattle and candle exhale, each in a timbre family no music bed uses (the hazards design's timbre table); skipping or batching cuts animation, never cues. **Traps and locks (change TL-22; traps and locks design 8):** the reserved cues gain the trap spring, Jam and Botch, the Rattle Wire's shaker, the Lullaby Glyph's wind-up click, the Gramophone's needle-scratch and the Wager pips, per the traps and locks design's timbre table; a Crier sounds the alarm cue. **Pending request (change TL-43; RS owner and art; M2):** alarm traps, the Belfry Wire, the tin-can tripwire, the High-Striker and the Crier share **one** reserved alarm-bell cue, the same pitch everywhere, no longer pitched per theme (this rule read, not changed). **Encounters (read, not changed; change EP-28; M2; encounter placement design 6.20, 8):** the formation chip set uses the global skin. **Pending request (change EP-27; RS owner; M3; encounter placement design 6.6.7):** the Choir Ritual's "last singer falls" sting joins the reserved cues. **Loot (read, not changed; change IR-12, IR-15; M2; item rolls design 4.12):** the signature banner stays reserved for boss signatures; a carried notable's use shows as a name floater in the unit-floater layer, never in the banner slot. **Pending request (change LP-7; RS owner; M3; loot sources design 6):** a **Windfall timbre row** (a plucked, dry string family that no music bed uses, kept out of chimes, bells, ticks and the Lullaby Glyph's wind-up click) and six reserved cues (wake, stand-up, hop after the Dream tick, held, nabbed, got-away), clock telegraphs, not reward stings; until the RS owner rules they read "pending RS owner", and if refused the Windfall telegraph relies on visuals and the global UI timbre (loot sources design 14.2). **Curiosities (change EV-25; M2; events design 4.11):** no horns, bells or wind-up or winding props on Curiosities, and no running hands or ticking on any clock or watch act; Curiosity audio tags are checked against the reserved cues (`ReservedCues`); card chips and the Twice-Told card use the global skin (read, not changed; change EV-4; events design 4.3.1, 4.5.2). **Boss rooms (read, not changed; change BR-36, BR-38; M2/M3; boss rooms design 6):** boss cues follow the boss rooms design's per-boss timbre table, in this table's shape (no struck bronze on the Carillon, no ringing alarm on the Engine, no horn on the View Halloo, no breath-family sample in any Heart bed or ambience stem); the Phase Score's per-boss stems and per-threshold layers stay out of the reserved cues, and every boss cue runs the 85% audio blind-ID gate. **Exploration and information (read, not changed; exploration and information design 4.10, 6):** segment 10 adds no reserved gameplay cue; its map, fact, Waymark, travel and Lodestring stings use the global UI timbre, are checked against the reserved cues (`ReservedCues`) and the 85% audio blind-ID gate, and a travel stop is never a tick. **Pending request (exploration and information design 6; RS owner; M2):** whether a "something new seen" travel-stop sting counts as a gameplay cue; until the RS owner rules this reads "pending RS owner", and if refused the travel stop uses visuals only (exploration and information design 14.2) |
| RC8 | **Barks** fire only on floor entry, idle, rout, Yield, Cowed, and boss phase entry after the banner; never on attack resolution, downings or deaths. A content test checks every bark trigger. **Diegetic ambience follows the same rule (lead's call):** comic or laughing ambience ducks to silence on any downing or death and during the death recap. **Hazards (change HZ-29; hazards design 6.16):** a companion may call a Stir's first due round, once per fight; never on telegraphs. **Boxed lurkers (read, not changed; change TL-28; traps and locks design 6.14, 8):** "feels teeth" is a UI callout over the opener plus the lurker's reveal cue, never a bark. **Encounters (read, not changed; change EP-9; M2; encounter placement design 6.7):** activity barks are idle barks. **Pending request (change BR-38; RS owner; M2; boss rooms design 6):** the boss signature banner joins the bark triggers, so a signature is voiced only as a non-verbal vocal sting under the universal banner when its forecast appears, never on resolution; until the RS owner rules this reads "pending RS owner", and if refused signatures are not voiced at all (SFX only; boss rooms design 14.2) |
| RC9 | **Names may be whimsical but never lie** about threat or role. **Encounters (read, not changed; change EP-12, EP-28; M2; encounter placement design 6.8.6, 6.20):** formation chip names follow this rule, and a Spoor mark is never a decoy. **Curiosities (change EV-25; M2; events design 4.11):** no silent bell and no purse (it reads as coin) on a Curiosity. **Exploration and information (read, not changed; change XI-14; M2; exploration and information design 4.4.1, 4.5.1):** the information and incentive charters hold every information label and field to this rule |

**M2 readability gates** *(illustr. thresholds)*:
- a greyscale value test per theme;
- a silhouette lineup of all factions × 8 roles at minimum zoom;
- colour-blind simulation of 10 damage types and 5 square kinds (and
  harm squares, Rough, Kegs and the terrain VFX; at M3, Miasma over harm
  squares and Rough; change TR-20, room terrain design 8.1);
- a role-ID playtest, read **per theme** on at least 30 rated UI-free
  screenshots per theme *(illustr.)*, the screenshots including
  dressing: 85% or more of roles identified and 75% or more of
  fearless, mindless and non-fearless reads correct (bosses read from
  the plate, RC3); the greyscale, colour-blind and role-ID gates run
  with terrain pieces in frame, and the role-ID gate includes "Keg or
  Furnishing" (change TR-20; room terrain design 8.1);
- a tone-by-depth review of boss and signature names and of the bark
  sets per (theme, floor) cell (7.7), and of the floor-5 Memento's
  premise-neutral caption ("carried out of the Heart"; change LE-5; M2;
  loot economy design 6.11), with its second, premise-neutral epitaph
  line and the flagged Reveries (change IR-13, IR-15; M2; item rolls
  design 4.13, 6.1);
- the loot additions (change IR-15; agreed, item rolls Q4; M2; item
  rolls design 6.2): each theme's Patina hue joins the greyscale and
  colour-blind gates against the RC4 damage-hue key (not V14), and the
  greyscale and role-ID gates confirm each boss signature reads as its
  family at minimum zoom;
- the hazard additions (change HZ-29; hazards design 8): "water square
  against Void" joins the greyscale gate; "herd or sheep figment
  against monster" joins the role-ID gate; the colour-blind and
  greyscale passes run with every hazard state in frame; an audio
  blind-ID test names each reserved cue from the bed mix at 85% or more;
- the loot sources additions (change LP-7, LP-15; loot sources design
  5.1, 6): the role-ID gate includes "container or Furnishing",
  "container or bait", "inset container or Latch, Door or Hairline",
  "Rathole or Portal skin" and "Bedside Stand or Latch, Furnishing"
  (M2), and "Truant or minion", "Truant or boxed lurker" and "Truant or
  faction minion" (M3); the greyscale and colour-blind passes run with
  the loot sources RC5 marks and every Windfall state in frame (Dormant,
  awake, held, nabbed; M3); once the RS owner approves the RC7 request,
  the six Windfall cues join the audio blind-ID test at 85% in the
  Choir, Carnival and Supper mixes (M3);
- the Curiosity additions (change EV-25, EV-45; events design 4.11):
  the role-ID gate includes "implied figure, not unit" (M2) and "figure
  frame, not mindless unit" (M3); the tone review signs off every rare,
  Sequel and Heart Curiosity under the premise test (7.5);
- the exploration and information additions (change XI-17, XI-20; M1
  the base grammar, M2 the rest; exploration and information design
  4.2, 4.10, 6): the greyscale and role-ID gates add "Unknown vs Void",
  "Afterimage vs unit", "Afterimage vs figure frame", "Seen vs
  Remembered" (with a minimum value-range ratio), "Chalked vs Void
  backdrop", "Remembered vs Chalked" and, per Law theme, "Remembered
  hazard vs Seen hazard vs environment", each per theme at 4 rotations;
  every knowledge state, Afterimage, loose end, projected pip, told pip
  and Waymark joins the greyscale and colour-blind gates; every Tone
  Sheet (7.7) carries a fog-bank tint row checked against its Void
  backdrop and Remembered grade; and the glance test (first-time
  testers name each visible line family at 80% or more, on the board
  and the minimap) runs beside them.

A theme that fails a gate is not shipped until its art passes; the gate
never changes a rule **(lead's call)**. **A held theme (lead's call,
after the check)** gets `shipStatus = held` in the manifest, so
`drawableSet` excludes it (6.4 R-SEED-5) and V5 must still pass. If V5
fails, or the hold outlasts the M2 balance freeze, the lead applies the
matching pre-authorised cut (holding the Supper is Cut 2); a hold that
matches no cut goes to the client (19.2).

### 7.5 Macro premise (minimal; not committed; agreed, Q3)

> Beneath the world a Dreamer sleeps, and its dream swallows what the
> waking world forgets: gaols, wakes, choirs, fairs, feasts. Each night
> the dream re-forms. Some go down; few wake.

| Question | Answer | Explains |
|---|---|---|
| Why the dungeon? | The dream digests forgotten places | Unrelated themes side by side, because dreams juxtapose |
| Why does it change per floor? | Each floor is a stratum: one swallowed place reshaped by its occupants' Obsession; deeper lies nearer the Dreamer | Theme per floor; the depth dial |
| Why a new dungeon each run? | The dream re-forms each night; the seed is the night | Seeds, determinism, daily seeds |
| What is death? | "You did not wake" | Total permadeath; never implies resurrection |
| Camps? | Lucid hollows where the dream thins | Rest, vendor, Muster |
| Unlocks? | "The dream remembers you" | Main 4.7 content, never power |

- **Open, not committed (agreed, Q3):** who the Dreamer is, its
  motives, whether it can wake, hero motives, endings. These come back to
  the client as future items (19.2).
- **Nothing shows or implies who the Dreamer is** (no face, portrait,
  name, age or biography) until the client answers the 19.2 premise
  items; every Tone Sheet's forbidden-imagery check carries this line
  (lead's call, after the check).
- Every theme carries a one-line **grim reading**. If surrealism is
  dropped, rosters and rules stand and only skins and names change.
- Fallback premises: the Bureau of the Dead; a dead god's sunken tomb.
- **Curiosities (read, not changed; change EV-25, EV-26; M2; events
  design 4.5.6, 4.5.10, 4.11):** every Curiosity premise names a
  resident in plain sight as whoever made, wrote, wound or watched it,
  never the sleeper and never no one (the premise test); `selfReference`
  cards are capped at 1 per run; the Heart takes at most one
  domestic-bedtime prop per visit (`HeartBedroomCap`); Afterword and
  memorial lines never show or imply the Dreamer.
- **Boss rooms (read, not changed; change BR-30; agreed, boss rooms Q5;
  M2, Later; boss rooms design 4.13):** final bosses are keepers of the
  sleep, never the Dreamer; the Dreamer appears only as visible
  environment during acts, never as a body or a sound attributable to a
  sleeper (`FinalBossNoDreamer`); the Later "Dreamer as a true-ending
  boss" is replaced by a bodiless set piece (8.11).

### 7.6 What to avoid

- **Rule-touching surreal ideas are refused for M2**, and may appear
  Later only as flagged changes: shifting rooms (conditions 11.1;
  **M3** exception: pre-proven Stir states, namely Tides held whole in
  2 states, Cord Flips, the Turning Ring's boards and
  environment-moved Furnishings such as the Returns Trolley, every
  state proven by V10; change HZ-18, agreed, hazards Q3; hazards
  design 6.3, 7); gravity or height (main 4.3, 6.8; **M4** exception:
  the Inverted Cathedral's reversal round, in which pushes resolve as
  pulls; the rules stay flat, only the push direction reverses; change
  HZ-18, agreed, hazards Q3; hazards design 7.11); darkness or light mechanics
  (enemies 5.1; kept, change XI-32, exploration and information design
  4.5.3: fog is geometry, never a light radius); breakable walls (Vanguard 8.3; kept refused: walls
  never break, while M2 Furnishings and Kegs may break and move, change
  TR-7, room terrain design 6.7; Portals never break either: from F2 a
  Splintered Door stands and connects the same rooms, a reading told
  plainly, change TL-21, M1, traps and locks design 6.11; no trap seals
  or splits the party, so the portcullis is damage-only in M2 and its
  Bar is an M3 item flagged against this line, change TL-31, traps and
  locks design 6.3, 7.2); spreading surfaces (main 2; **M3**
  exception: environment-made Remnant clouds with a fixed footprint and
  a short countdown, which never drift, spread or grow, change TR-27,
  room terrain design 6.6); multi-square units; fourth-wall rule changes. Each may appear
  as backdrop only.
- **Content watch-list:** no suicide imagery (a later "Hanged
  Marionette" becomes "Strung Marionette"); no asylum or
  madness-as-monster themes; no child victims (nursery imagery is toys,
  played sinister); invented liturgies only; no ethnic caricature; no
  meme parody; no D&D-specific names.
- **Church cap:** at most 2 church-dressed themes in any pool set. The
  Choir is one; the Tallow Abbey and the Inverted Cathedral never share a
  pool set with it.
- **Story creep:** final-boss concepts carry no premise twists. The draft
  "the alarm wants to wake the sleeper and end everything" is cut.

### 7.7 The Tone Sheet (M2, one page per theme)

Each sheet holds: the Law and the Obsession, with its kind (7.3 pillar
6); a dominant and secondary palette in a hue family distinct from every
theme it can sit beside, read against the damage-hue key (RC4); material
language; mind-tell placement; the role prop sheet for all 8 roles and
the faction silhouette motif (8.3–8.11); the theme's **one wrong
detail** (Shallows) or **dream device** (deeper tiers), repeated in
every room and held to backdrop (RC6); Yield, rout and Cowed lines,
**one set per eligible floor** for a band theme, keyed to the dressing
set (shallow dressing the comic set, deep dressing the dry set); the
music set and its lullaby quote, with the RC7 cue sounds kept out of it;
the grim reading; a forbidden-imagery check, including the Dreamer line
of 7.5. The sheet is a content artefact referenced from
the theme record (`toneSheet`) and reviewed in the tone-by-depth gate.

---

## 8. The theme catalogue (agreed, Q4)

**Q4 (agreed):** the full M2 core of 8 themes (Gaol of Small Hours and
Kennel Wood on floor 1; Drowned Choir, Silk Archive and Carnival of the
Last Evening on floors 2–3; Long Supper and Gut of the World on floor 4;
the Dreamer's Heart on floor 5), with the Endless Wake added if it
passes its floor-1 test (8.2, 8.5); Cut 1 and Cut 2 pre-authorised as
schedule fallbacks (12); build order Heart and Gut first. **Rejected
with Q4:** B, plan Cut 1; C, plan Cut 2; D, the approved scope (4
themes plus a deep overlay).

### 8.0 Conventions, the theme record and frozen names

**Theme record (change RT-4; lead's call on field names; completed
after the check):**
- `id`, `tier` (a pool and presentation tag only; the act shown comes
  from the floor index, 3.4), `eligibleFloors` (at most 2 adjacent),
  `final`, `unlock` (M3), `shipStatus` (`core`, `conditional-passed`,
  `conditional-failed`, `cut`, `held`; manifest data read by
  `drawableSet`, 6.4 R-SEED-5);
- **`pairKey`** = (`primaryDamageType`, `squareKind`), both authored:
  `primaryDamageType` is the at-will type the theme's Law is about;
  `squareKind` is the theme's home square kind or `none`, and `none`
  never counts as a match (R-SEED-4). There is **no `poolWeight` in M2**:
  the draw is a permutation and reads no weight (a weighted integer
  permutation would be a later, flagged change);
- `roster[]`, `signatureType`, `minionType`, `lessonAxis` (floor-1
  themes), `signatureFeature` (a visible map feature the party uses,
  within existing rules; handed to the hazards, locks and boss-room
  segments), `signatureFamily` (the kind of that feature: doors, cover
  logs (was cover gates; change TR-22), islands, stacks, slicks, tables,
  pools, beds; read by DistinctnessCheck);
- **`counterType`**: one declared damage type among the roster's type
  weaknesses, or a weakness the theme's own squares grant. No boss of the
  theme is immune to it. This defines enemies 17.5's undefined term;
- **`themeDamage`** (read by Volatile) = the `primaryDamageType`, so its
  rider is inside the theme's 6; **`hardenedLayer`** (read by Hardened) =
  a layer whose shown breaker is the theme's `counterType`; **`frost`**
  (Chilling legal from floor 3). V1 checks all three against the 6
  (lead's call: one rule instead of per-theme values);
- `legalOverlays[]` (enemies 8.2 step 5, kept in M2; each must keep the
  theme's 6);
- `damageTypes[]`, `harmfulConditions[]` (at most 6 entries; declared,
  then recomputed by the validator with role taxes, riders, boss kits,
  traps and terrain), `teeteringFallbackDrop` (the condition dropped if
  RT-14 is refused);
- `terrainSquares[]` plus density per floor, `trapFlavours[]` (TrapRecords,
  a superset of the Shade DEP-S1 record; change TL-10; traps and locks
  design 7, 11.1),
  `terrainKit` (was `coverProps[]`: Tall, Low, Furnishing, Keg, hazard
  piece and hazard face kinds, harm, Rough and Plank skins and the
  theme's terrain Law; change TR-22; room terrain design 7, 11.1),
  `hazardKit` (the theme's Tableaux, Void skin and drop-theme flag,
  Stirs, Cords, vent, Mood and Omen, with minimum tiers; change HZ-8;
  hazards design 6.2.2, 11.1),
  `layoutGrammar` (was `layoutArchetype`; change
  FL-14; floor layout design 7, 11.1), `dressingSets[floor]`;
- **`placementRules[]`**: typed constraints the placement and terrain
  segments read (entry-square exclusions, dry islands, start-square
  shares, per-encounter unit caps such as the Gut's acid cap, Frozen
  source caps, the Wake's dormant-bias exclusion, the Heart's morale
  shape by room); **fearless-anchor tags** (the Gut Knights, Wake
  Pallbearers, Choir Vergers, Silk Misprints and the Night-Mare are
  tagged "kill the anchor, then the rest Cow"; change EP-29; M2;
  encounter placement design 6.16);
- `keepsakeProps` and `keepsakeArenaDressing` (9.3; every non-final
  theme authors both), `vignette` (the camp and run-start card text);
- `nonFearlessPermille` and `mindlessPermille` (computed),
  `signatureLine`, `toneSheet`, `mindTell`, `musicSet`;
- **`theme.loot`** (change IR-15; agreed, item rolls Q4; M2; item rolls
  design 5.1; additive, display and validator only): `carriedFamilies[]`
  (derived from the 8.x kit columns of armed roster types, leaders and
  bosses), `answers[]`, `patina` (material set, trim palette, at most
  one charm; hue reviewed at the 7.4 gates), `stamp` (a pictogram),
  `reveries[]` (part-tagged), `provenance`, `signatures[]` (derived at
  load from the boss records) and `memento?` (floor 5). The rules core
  never reads it, except V14 and the Element Echo switch (6.4);
- **`curiosityKit`** (change EV-40; M1 stub (empty), M2 content; events
  design 9.1): `curiosities[]`, `interludes[]` and `glimpses[]`,
  `CuriosityRecord` ids drawn by the events segment; shared Curiosity
  pools reuse this section's tier names (Shallows, Middle, Deep, plus a
  shared Forecourt pool).

**Counting (lead's call):**
- every theme pays Prone (Shove) and Frightened (Frighten): the tax;
  "free 4" names the other four;
- Teetering from the universal hazard-edge rule is counted **pending
  ruling RT-14**. Known armed traps are hazards too (conditions 11.3), so
  any theme with forced movement and traps can grant it, squares or not;
  the fallback drop (`teeteringFallbackDrop`) is named for every theme
  with a full 6. **Pending request (change TR-14; room terrain design
  6.5.5):** the conditions owner is asked to rule RT-14 "does not count"
  before M2 rosters freeze. Contact hazards and harm squares never give
  Teetering on a save (a harm square reaches it only through Brink), so
  harm squares stay in every case; if RT-14 is refused, every M3 row
  that uses Poisoned in the Supper (its fallback drop) is conditional.
  **Pending request (change HZ-6; hazards design 6.8; conditions owner,
  before M2 rosters freeze):** the ruling offered for conditions 16
  makes conditions granted only by the universal positional rules (the
  10.2 hazard-edge success, 10.3 landing, the Void branch, Clutch and
  Brink) an uncounted class, with no monster, boss, trap, Stir, Cord or
  square record applying Teetering directly, plus a boss guard (no
  Teetering from a Void-edge success or Clutch). Nothing changes until
  the owner rules. If refused, a Void-edge success is a plain stop, the
  fallback drops apply, every drop theme names its
  `teeteringFallbackDrop` in V1 before it ships, and the Supper vent and
  the Heart's Toss and Turn are cut;
- no M2 theme deals radiant, so ruling RT-15 blocks only M3 themes;
- **Pending request (change TL-39; traps and locks design 7;
  conditions owner, before M2 rosters freeze):** RT-16 is asked to rule
  "trap conditions come from the theme's 6, otherwise the trap is
  damage only". Nothing changes until the owner rules; if it rules
  damage-only, every trap condition in the traps catalogue drops.
- **Pending request (change TL-43; RS owner and art; M2; traps and
  locks design 6.3, 7, 8):** each theme's `dressingSets` avoid the
  object families of that theme's own trap tells and device bodies (the
  per-theme "Reserved" lists of traps and locks design 7), as RT 7.1
  did for Kegs; trap tells never reuse an HZ Cord, vent or Snag prop
  family; V9 and V11 check the lists as content.

**Names avoided because they collide (lead's call; whole-word grep):**

| Draft name | Collides with | Used instead |
|---|---|---|
| Sleeper (premise, Heart, final boss) | Enemies 17.4 template **Sleeper** | **the Dreamer**, the Dreamer's Heart |
| Mourner (Wake minion), Chief Mourner | Herald **Mourners** (Dirge feature; morale report) | **Black-Crepe Guest** |
| Toll, Knell, Passing Bell; "Bell" head words (Bell-Ringer, Bell-Mother) | Herald spells Toll, Knell, Passing Bell | **Peal** (brute ability), **Clapper-Monk**, **the Drowned Carillon**, Undersong |
| Dirge, Keening, Lament, Hush, Kindle | Herald subclass and spells; retired Herald names | **Sob, Wail**; the Mouthless Librarian |
| Undertow (Tidecantor note, Mother Undertow) | Arcanist spell **Undertow** (the Soak combo) | **Riptide Note** |
| Warden (Warden-Sergeant, Warden of Hours, Shushing Warden, Net-Warden) | Warden's Coif; Corridor Warden | **Jailer-Sergeant, the Jailer of Hours, Shusher, Netcaster** |
| Fork (camp route choice) | Arcanist Spellcraft option **Fork** | **Two Doors** |
| Deepening, Deepened | Arcanist **Deepen** and its rider | **Nightmare tiers** |
| Threshold (tier and mechanic) | A generic term in 15 documents | **Shallows; Antechamber Catch-up** |
| Errata (Archive artillery) | A section heading word in armour and trinkets | **Misprint** |
| Glint (Mirror Court skirmisher) | Shade on-target display **Glint** | **Shardling** |
| Rime (Supper lurker), Grease | Conditions and Arcanist *Rime*; consumables *Grease* | Lurker cut; fat is "dripping" in dressing only |
| Heart's Echoes | Trinket **Ward Echo** | **Keepsakes** |
| Gatekeeper (Model B boss role) | Companions kit **Gatekeeper** | Not used |
| Oil Flask (Fire-Eater ability) | Consumables item **Oil Flask** | **Lamp-Oil Toss** |
| Engulf (Bile Ooze ability) | Conditions **Engulfed** (Later); the SRD cube's Engulf | **Gulp** |
| Host, Hostess, guest names (Supper) | The Wake's **Guest of Honour** and Guests (one run can hold both) | **the Head of the Table**, **the Seneschal**, "diners" |
| Black-Veil Cousin (Wake) | Charter RC3: a veil means fearless | **Black-Glove Cousin** |
| Dirge-priest (Heart grim reading) | Herald **Dirge** | **vigil-singer** |
| Inverted cathedral (fallback premise) | The catalogued **Inverted Cathedral** (8.12) | **a dead god's sunken tomb** |

Grep-checked with no hits: Shallows, Middle Dream, Strata Draw, Stratum
Card, Two Doors, Last Hollow, Antechamber, Keepsake(s), Lockdown,
Clapper, Riptide, Undersong, Carillon, Governess, Rousing, Dreamer,
Shardling, Misprint, Shusher, Netcaster, Jailer, Sob, Gaol, Turnkey,
Lamplighter, Precentor, Chorister, Tidecantor, Ringmaster, Landlord,
Figment, Lullabist, Tenant, Pylorus, Grim Surreal, ThemeParity,
ClassThemeSpread, FloorTimeByTheme, LevelAtDoorByFloor; after the check:
Lamp-Oil, Gulp, Smother, Seneschal, Head of the Table, Black-Glove,
Carver, tomb-wisp, vigil-singer, Shroud-Keeper, Tomb Clock, Oathbroken,
AntechamberDominatesArena, drawableSet.

### 8.1 Summary

| # | Theme | Tier / floors | Wave | Law (signature) | At-wills | Free 4 | Squares | Non-fearless | Mindless types |
|---|---|---|---|---|---|---|---|---|---|
| 1 | **Gaol of Small Hours** | Shallows, F1 | **M2 core** | Lockdown: doors, noise and keys | sl, bl, pi | Bleeding, Staggered, Grappled, Dazed | none | 857‰ | 0 |
| 2 | **Kennel Wood** | Shallows, F1 | **M1 subset, M2 core** | The pack and the horn | sl, pi, bl (RT-22) | Bleeding, Grappled, Dazed (5 total) | none | 1000‰ | 0 |
| 3 | Endless Wake | Shallows, F1 | **M2 conditional** (else the first M3 unlock) | The sleeping house: dormant guests, coffin tells | bl, pi, psy | Staggered, Unfocused, Bleeding, Dazed | none | 714‰ | 0 |
| 4 | **Drowned Choir** | Middle Dream, F2–3 | **M2 core** | The drowning line: Soaked plus cold and lightning | ltn, bl, cold | Soaked, Chilled, Staggered, Grappled | water | 857‰ | 0 |
| 5 | **Silk Archive** | Middle Dream, F2–3 | **M2 core** | Webs and silence; fire is the key | pi, psy | Restrained, Poisoned, Unfocused, Grappled | web | 714‰ | 1 |
| 6 | **Carnival of the Last Evening** | Middle Dream, F2–3 | **M2 core** | Oil and fire, both ways | fire, pi, bl | Burning, Smouldering, Oiled, Staggered | oil (+ fire) | 857‰ | 1 |
| 7 | **Long Supper** | Deep, F4 (F3–4 from M3) | **M2 core** | Freeze and shatter, both ways | cold, bl, pi | Chilled, Frozen, Staggered, Poisoned | none (tables) | 857‰ | 1 |
| 8 | **Gut of the World** | Deep, F4 | **M2 core** | Attrition: Corroded and Withered | acid, pi, nec | Corroded, Withered, Poisoned, Grappled | bile (RT-13) | 714‰ | 1 |
| 9 | **The Dreamer's Heart** | Heart, F5 fixed | **M2 core** | Waking: breakable dreaming groups; a capped war on concentration | psy, pi | Unfocused, Poisoned, Dazed, Restrained | web (bedsheets) | 714‰ | 1 |

**M2 core:** 8 themes, 56 types, 15 bosses, 11 (theme, floor) cells, 14
environment and dressing sets *(counts recomputed by the enumerator)*.
The Wake adds 7 types and 2 bosses if it passes its gate.

**`pairKey` per theme (lead's call; the at-will column lists the
`primaryDamageType` first):** Gaol (slashing, none); Kennel (slashing,
none); Wake (bludgeoning, none); Choir (lightning, water); Silk
(piercing, web); Carnival (fire, oil); Supper (cold, none); Gut (acid,
bile); Heart (psychic, web). No floor-1 theme shares a primary type
with a band theme, so R4 never bites between floors 1 and 2 (10.3).

**`signatureFamily` per theme:** Gaol doors; Kennel cover logs (was
cover gates; change TR-22, told plainly; room terrain design 7.3); Wake
coffins and curtains; Choir islands; Silk stacks; Carnival slicks;
Supper tables; Gut pools; Heart beds.

**Tactical matrix: themes differ in kind, not amount.** Budgets stay
theme-agnostic **(settled, enemies 10.1)**.

| Theme | Rewards | Punishes |
|---|---|---|
| Gaol | Stealth, closing doors, stealing keys, cowing | Noise |
| Kennel | Walls, chokepoints, killing the leader, area attacks against packs | Standing in the open |
| Choir | Lightning and cold, breaking grapples, dry ground, spacing | Fire reliance, clumping in water |
| Silk | Fire, slashing, stealth | Noise, open firing lines |
| Carnival | Cold, spacing, burning oil away | Standing on slicks, edges |
| Supper | Bludgeoning, the party's fire, spread formations | Slow, clumped parties |
| Gut | Antidote and Remedy, cold, light armour | Heavy armour, heal reliance |
| Heart | Morale play, cures, saved resources | Concentration casters, heal reliance |

### 8.2 Floor-1 rules (lead's call; change RT-9)

Every floor-1 theme meets all of these:
- **(a) Default entry: 750‰ or more non-fearless.** The binding
  `Floor1ClassSpread` row is unproven under the adopted Herald package:
  Herald 11.5 shows a number only for the binding row (85.5, about 14
  behind) and otherwise for the *non-adopted* Totem-lever column.
- **(b)** Every M2 floor-1 boss is authored with an **escort drawn from
  non-fearless types**.
- **(c)** A 50–74% theme joins the floor-1 pool only after the
  reference-arm `Floor1ClassSpread` passes in its split, with
  **`F1EscortBreakable`** binding for it: every escort and reserve group
  of that theme's floor-1 bosses is drawn from non-fearless types only.
  This narrows the **agreed** HR-21 text "escort and reserve groups are
  not constrained" for such themes (change RT-9; **told plainly**). It
  also needs per-theme rows for any psychic at-will against a solo
  concentrating Arcanist. The Herald's own lever list (Herald 7.16)
  stays owed.
- **(d)** 0 mindless types; no hard control; at most 1 controller, never
  in rooms 1–2; lurkers never in main-path rooms 0–2; ambushes only after
  room 2; no surprise at `mainPathIndex` 0–1 **(settled)**.
- **(e)** Content tests: Scavengers fills at the floor-1 solo patrol
  budget (7–8); `SeedHasTarget`; the cowable group before the boss.
- **(f) Distinct lesson axes (binding contrast rule):** Kennel teaches
  **morale and positioning**; Gaol teaches **doors, noise and keys**;
  Wake teaches **reading tells and the sleeping house** (Perception,
  awareness). **Curiosities (change EV-3; M2; events design 4.2,
  4.6):** each floor-1 theme's Curiosities follow its axis; no
  Curiosity adds a lesson tag, and main-path Q room 3 carries a wall
  Glimpse only. Floor-1 Curiosities never offer supply, theme
  conditions, riders, curses, rolled options, companions, hazards or
  Landmark ticks.
- **(g) Leaders on floor 1** exist only as a Picket's leader-role
  standard (Picket is "1–2 standards", so it can field one where the
  budget allows) or as a boss escort or Champion elite. The leader-group
  *guarantee* starts on floor 2 **(settled)**. A content test reports how
  often a Picket leader fills on floor 1.
- **Floor-1 terrain (change TR-29, told plainly; room terrain design
  6.5.6):** floor 1 has no hazards of any kind (RT-20 read as written);
  the Gaol warden's stove and the Wake candle rack are ordinary Low
  pieces whose collision damage is fire-typed, with a visible ember
  tell, and are not hazards (no edge save, no saturated hue).
  **M3 Landmark ticks (change HZ-19, told plainly; hazards design 6.1,
  7.2–7.4):** harmless rhythms and dials (doors, a mute round,
  countdowns, a startle; no damage, condition or unit movement) are not
  hazards and are legal on floor 1, at most 1 per room at main-path
  index 3 or more, so floor 1 stays dry in the base game.
  **Nightmare tier 2+ (change HZ-25; agreed, hazards Q4; M3; hazards
  design 6.19):** below tier 2 floor 1 stays hazard-free; from tier 2
  each floor-1 theme gains one damaging hazard package (the Gaol's
  Treadwheel, with the warden's stove promoted to a Hot piece; the
  Kennel's staked pits with the Painted Hunt, the Stampede replacing the
  Painted Hunt at tier 3; the Wake's Open Grave Void), in at most 2
  rooms per floor at main-path index 3 or more, on floor 1's own bands.
- **Floor-1 traps and locks (change TL-19; M2; traps and locks design
  6.19, 7.2–7.4):** 1–2 traps, the first on the main path being the
  `alwaysKnown`, labelled, noise-only **lesson trap** (Gaol bell
  tripwire, Kennel tin-can tripwire (pending TL-43, 8.4), Wake creaking
  board), which
  teaches each theme's lesson axis (f); no trap in main-path rooms 0–1
  and no damaging trap in rooms 0–2, as (d); no trap trigger on a patrol
  Circuit or within 6 of a fight host; pushes stop before F1 trigger
  squares; trap conditions end when combat starts; detect DC ≤ F1
  Standard; damage ≤ 3, with one off-path device ≤ 4 (the Gaol's
  Clock-Weight); no hard control. Locks: Plain, plus at most 1
  consequence lock after room 2 (the Gaol's Crier, or the Wake's Viewing
  Door, the one F1 Tattle); no Splinter, no mimic and no Wager room on
  F1.

Effect: the 50–74% split of `Floor1ClassSpread` has no floor-1 data in
M2 unless the Wake passes **(told plainly)**. The gate is read per split
that occurs.

**When the Wake is judged (new lead's call).** The Wake's gate is read
once, at the M2 floor-1 balance freeze: if (c) and its rows pass, it
joins the M2 floor-1 pool; otherwise it becomes the first M3 unlock and
is not re-tested inside M2. **The Wake's promotion (read, not changed;
change EV-44; agreed, events Q2; M2; events design 4.6):** if it joins,
its Curiosity kit is authored to parity in M2 and joins at
`F1PoolParity` (every F1 theme pool with the same standard, MECH and
rare counts); otherwise 8.5 stands.

### 8.3 Shallows: 1. The Gaol of Small Hours (F1; M2 core)

- **Look:** a prison tower lying on its side, where it is always 3 a.m.
  Cell doors are clock faces with still hands (the ticking lives only in
  the boss arena, where the Countdown is); locks are padlocks; bars
  appear only on Grilles; never keyholes (RC3; change FL-7, floor layout
  design 8.1). Palette: iron grey and whitewash with rust
  accents, under pale gaslight (moved off amber after the check, so the
  floor-1 pair and the Gaol–Silk adjacency differ). *One wrong detail,
  in every room:* every clock shows a different hour.
- **Obsession:** inmates serving time for crimes they have not yet
  committed. **Grim reading:** an oubliette for the forgotten.
- **Faction:** Jailers and Inmates.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Inmate | minion | skittish | shiv (slashing) + Help |
| Escapee | skirmisher | skittish | thrown spoon-shiv (piercing); Disengage; leads Packs of Inmates (one family) |
| Turnkey | soldier, `armed`, `promotable` | normal | key-club (bludgeoning) + Shove; limited Manacle (Grappled; Staggered if RT-14 is refused); carries the keyring (steal target); cell-door shield |
| Lamplighter | artillery | normal | crossbow (piercing); limited line 4 Lantern Bolt (piercing) |
| The Lifer | brute | normal | manacled fists (bludgeoning) + Shove; limited Chain Swing (Staggered) |
| Silent Penitent | lurker | fearless (hooded vow, minded) | Ambush opener with a shiv (Bleeding); hides on the cots; never in rooms 0–2 |
| Jailer-Sergeant | leader | steady | halberd (slashing), Frighten, Command |

- **The 6:** Bleeding, Staggered, Grappled, Dazed (boss only), plus
  Prone and Frightened.
- **Damage:** slashing, bludgeoning, piercing. **Weaknesses:** Lamplighter
  to cold (elemental); Penitent to psychic (spirit). `counterType`
  psychic. Legal overlay: Inmate shiv slashing → piercing.
- **Law, Lockdown:**
  - the densest closed-door network in the game, with path hearing −3
    per closed door (enemies 12.3), so sneaking is easy and noise is the
    risk;
  - bell tripwires (noise; floor-1 joining cap 1);
  - one shut-door pair (a guard post and a neighbour through a `pairDoor`
    that starts propped open) **pending the enemies owner's ruling on the
    `pairDoor` reading**; if refused, one near pair instead (change
    FL-11; floor layout design 6.6, 7.2); **or the hooded cot instead,
    one per seed** (change EP-13; M2; told plainly; encounter placement
    design 6.17): on hooded-cot seeds the floor has no shut-door pair,
    and Lockdown shows through the door network and the bell tripwires;
  - the Turnkey's keyring is the floor's steal target and opens a cell
    cache (locks hook); the Turnkey is the floor's **Keybearer**, and
    the Jailer's keyring opens every cell-tagged lock, gained by kill,
    Steal, Yield or Cow and used automatically on walk-in, its carrier
    within 1 room of its lock (change TL-23; M2; traps and locks design
    6.11, 7.2);
  - Cowed inmates step aside and kneel at the bars, facing the wall;
    they never pass through or change a door (RC3);
  - Escapee and Inmate Scavengers fill the cowable guarantee.
- **`signatureFeature`:** cell doors the party can shut against a
  joiner (the party close action, FL-8; shutting mutes and
  delays; it does not cut a path unless locked; floor layout design
  7.2, 9.2).
- **Hooks:** cell-block grid layout; traps: bell tripwire (noise),
  portcullis (damage only in M2; the Arch-to-Grille Bar is M3, flagged
  against 7.6; change TL-31; traps and locks design 7.2), "Appeals"
  trapdoor (Prone), no hard control; locks: Plain cell padlocks (M1), 1
  Crier after room 2 (M2) and the Hour lock (M3: hourglass-plated cell
  doors that open on the Small Hour's Landmark tick, with no clock face
  on the lock; change TL-22; traps and locks design 6.11, 7.2);
  cover: desks, benches and foot stocks (bars appear only on Grilles;
  change TR-22, told plainly; room terrain design 7.2).
- **Terrain Law (change TR-22; room terrain design 7.2):** loud verbs
  wake the block: noise is the terrain.
- **Bosses** (both pass `Floor1BossMindReachable`; 2 pips; 50% phase;
  escorts of Turnkeys and Inmates; **this escort line is read to admit
  the Lifer as the (d) reserve of "Cell Zero Opens" only**, with no roster
  change and V1 `ThemeBudget` unaffected; change BR-24; agreed, boss
  rooms Q1; boss rooms design 4.10.2. **Renditions (change BR-23;
  agreed, boss rooms Q1; M2; boss rooms design 4.10.1, 5.3):** each F1
  boss ships exactly 3 Renditions; Rendition 1 is the text below, and
  phase or ability alternates that vary it need RS owner sign-off):
  - **The Jailer of Hours:** halberd plus key-mace; announced Countdown
    (Frightened signature, cooldown 3); at 50%, option (a) Stop the Clock
    (Dazed). Arena counterplay: the pendulum, which can be stopped to
    cancel the Countdown.
  - **The Thing in Cell Zero:** a chained shape behind the oldest door;
    chain flail (bludgeoning), roar (Frightened signature); at 50%, (a)
    a Dazed slam. **Cut step 1b (change BR-24; agreed, boss rooms Q1;
    M2 conditional; boss rooms design 4.10.2):** if cut, it folds into
    the Jailer as the Rendition **"Cell Zero Opens"** (50% (d): the Cell
    Zero alcove becomes a reserve socket and a chained Lifer, on
    `chain(3)`, wakes), replacing the Jailer's R3, on the Lifer's roster
    rig; no new type.
- **Role props (RC2):** Turnkey, the cell-door shield; Jailer-Sergeant,
  a raised key-staff emblem; Penitent's rest tell, a hood that turns to
  follow; minions share striped cloth. **Faction motif:** manacles and
  striped cloth.
- **Sound:** irregular ticking in 5/4, music box, cello; no door slams
  or bells in the bed (RC7 cue sounds).
- **Checks:** 7 types and 7 roles; leader, minion, promotable and armed
  present; 2 weakness families; no universal tag. **Teetering:** the
  Gaol has Shove users, traps (known armed traps are hazards,
  conditions 11.3) and a full 6, so Teetering would make 7;
  `teeteringFallbackDrop` = Grappled (Manacle moves to Staggered).

### 8.4 Shallows: 2. The Kennel Wood (F1; M1 subset, M2 core)

- **Look:** an underground forest under a moon painted on the cavern
  roof, around a dead lord's hunting lodge. Bracken and roots; rain
  falls only in the backdrop, never on walkable squares (RC1). Palette:
  moss green, lantern amber, painted-moon silver. *One wrong detail, in
  every room:* the painted moon never moves, and shadows fall toward
  it (no Curiosity shows a moving moon; change EV-25; events design
  4.11).
- **Obsession:** hounds still hunting for a master who is now a
  portrait, carried on a pole as the pack's banner. **Grim reading:** a
  cursed lord's eternal hunt.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Whelp | minion | skittish | bite (piercing) + Help |
| Gaunt Hound | skirmisher | skittish | slashing bite; limited Worry (Grappled); Disengage |
| Kennel Mastiff | brute | skittish | **the settled M1 Bleeding claw** + Shove; Charge (exactly the settled M1 brute, enemies 17.1) |
| Huntsman | soldier, `armed`, `promotable` | normal | hunting maul and boar-board shield (the weapons M1 warhammer-and-shield soldier: bludgeoning, Push, Interpose) + Shove; a boar spear (reach, piercing) only on a promoted or F2+ pike variant, never at room level 1 (weapons 15.1) (change RT-22; cross-document comparison 2026-11-23, CC-2) |
| Poacher | artillery, `armed` | normal | hunting sling (the weapons M1 sling artillery: bludgeoning, Push) (change RT-22; cross-document comparison 2026-11-23, CC-2); limited burst 1 Flushing Shot (damage only) |
| Houndmaster | leader | steady | hunting knife (slashing), Frighten (the horn), Command; portrait-pole emblem |
| Ghillie | lurker | normal | hook-knife (slashing); Ambush opener (Bleeding); never in rooms 0–2 |

- **The 5:** Bleeding, Grappled (the Gaunt Hound's Worry and snares),
  Dazed (boss), plus Prone and Frightened. These are exactly the
  approved M1 kit space, so the M1 art and kit carry straight into M2.
  Teetering, if counted (RT-14), makes 6, so no drop is needed.
- **Damage:** piercing, slashing, bludgeoning (the M1 hunting maul and
  sling; change RT-22; cross-document comparison 2026-11-23, CC-2). **Weaknesses:** hounds to fire
  (elemental); Ghillie to radiant (spirit). `counterType` fire. Legal
  overlay: Gaunt Hound slashing → piercing.
- **Law, the pack and the horn:** Packs are one family (Mastiff or Gaunt
  Hound plus Whelps), and a first loss Demoralises the skittish (T3); the
  horn-leader leads the boss escort; the lesson is to put your back to a
  wall, use chokepoints and kill the leader.
- **`signatureFeature`:** fallen logs (cover) and kennel gates
  (Portals); `signatureFamily` cover logs (change TR-22, told plainly;
  room terrain design 7.3).
- **Terrain Law (change TR-22; room terrain design 7.3):** backs to
  logs; the pack flanks round cover.
- **Hooks:** glades joined by narrow trails (open rooms and corridors,
  as weapons 9.5 and 26 need), partitioned in the rules into ordinary
  rooms joined by Portals and read as one wood on screen (`beads`;
  change FL-1, floor layout design 7.3); one horn near pair at index ≥ 4
  (FL-11, floor layout design 6.6); traps: snares (Grappled), alarm horns
  (noise; **pending request**, change TL-43, RS owner and art, M2,
  traps and locks design 7.3: the horn tripwire is re-told as the
  **tin-can tripwire**, noise 10 at the cans with the one reserved alarm
  cue, because horns stay reserved for joiners and a horn on a post
  reads as the HZ Horn Post Cord); staked pits are a walkable harm square (piercing, damage
  only, no Teetering) at Nightmare tier 2+, M3 (re-read from "Later
  (drop hazard)"; change HZ-25, told plainly; hazards design 6.19).
- **Bosses** (3 Renditions each in M2, as 8.3; change BR-23; agreed,
  boss rooms Q1; boss rooms design 4.10.1, 5.3):
  - **Old Ragged:** a huge hound in the dead lord's coat ("huge" is
    proportion and plinth; every boss is rules-size ≤ Large; change
    BR-18, told plainly; boss rooms design 4.7.6). It is the
    approved M1 boss kit **(settled, enemies 17.5)**: Bleeding claw,
    bite, roar (Frightened signature), slam, Dazed swap at 50%.
    Unarmed, it **guards** its signature, Bellwarden's Mail, in a
    signature cache in its den; its plate reads "Guards: Bellwarden's
    Mail" (read, not changed; change IR-13; agreed, item rolls Q5; M1;
    item rolls design 4.13). **Pending request (change IR-13; the RS
    owner rules; segment 9 and art support; item rolls design 4.13;
    cross-document comparison 2026-11-23, CC-87):** the coat is a torn
    cloth **surcoat** that is never loot, so the coat and the mail never
    read as one item; if refused, the cache, plate line and decal carry
    the read alone (item rolls design 14.2). **Pending request (change
    BR-22; enemies owner; M2; boss rooms design 4.9):** M1 unchanged; in
    M2 the roar takes `readyAt` 2, with the M1 values re-run; if
    refused, Old Ragged is exempt and its Rhythm opens on the roar.
  - **The Master in the Frame (M2):** a lord stepped out of his
    portrait; boar spear; the View Halloo (Frightened signature;
    renamed from "horn", change BR-16; cross-document comparison 2026-11-23, CC-69); at 50%, (d)
    announced Whelps. Not mindless. **Cut step 1c, client-only, below
    Cut 2 (change BR-24; agreed, boss rooms Q1; M2 conditional; boss
    rooms design 4.10.2):** if cut, it folds into Old Ragged as the
    **Lord's Gallery** Guise (dressing only), not a Rendition.
  - Escorts: Whelps and a Gaunt Hound (breakable).
- **M1 subset:** Kennel Mastiff, Huntsman, Poacher and Old Ragged: the
  approved "3 types plus 1 boss" **(settled, enemies 21)**. In M1 the
  Huntsman is the weapons M1 warhammer-and-shield soldier and the
  Poacher the M1 sling artillery, dressed as above (weapons 15.3, 15.4;
  armour 12.3, 13.6) (change RT-22; cross-document comparison 2026-11-23, CC-2).
- **Role props (RC2):** Huntsman, a hide-covered hurdle; Houndmaster,
  the portrait-pole emblem; Ghillie's rest tell, a twitching leaf cape.
  **Faction motif:** collars and hunting green.
- **Sound:** baying as rhythm, folk fiddle, low drones; horn calls are
  reserved for joiners and never appear in the bed (RC7).

### 8.5 Shallows: 3. The Endless Wake (F1; M2 conditional, else the first M3 unlock)

- **Look:** a hundred-year funeral in a candlelit manor crypt. Coffins
  used as furniture, portraits with their eyes closed. Palette: candle-wax
  ivory and mourning black, with faded violet. *One wrong detail, in
  every room:* every clock in the house is stopped at the moment of
  death, and every portrait's eyes are closed (Curiosities keep both:
  no open-eyed portrait, no running clock; change EV-25; events design
  4.11, 5.3 W5). **Obsession:** a funeral
  that cannot end politely (grief). **Grim reading:** a plague manor's
  endless vigil.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Black-Crepe Guest | minion | skittish | hatpin (piercing) |
| Black-Glove Cousin | skirmisher | skittish | thrown hatpins (piercing) |
| Pallbearer | soldier, `armed`, `promotable` | fearless (veiled) | coffin pole (bludgeoning) + Shove |
| Gravedigger | brute | normal | spade (bludgeoning) + Shove; limited Clout (Staggered) |
| Hired Wailer | artillery | normal | Sob (psychic, ranged); limited burst 1 Wail (psychic) |
| The Late Uncle | lurker | fearless (dead, veiled, minded) | cane (bludgeoning); Ambush opener with a razor (Bleeding); never in rooms 0–2 |
| Undertaker | leader | steady | black cane-sword (piercing), Frighten, Command |

- **The 6:** Staggered, Unfocused, Bleeding, Dazed (boss), plus Prone
  and Frightened.
- **Damage:** bludgeoning, piercing, psychic. **Weaknesses:** the Late
  Uncle to radiant (spirit), the lacquered Pallbearer to fire
  (elemental). `counterType` radiant. Legal overlay: Black-Crepe Guest
  hatpin piercing → psychic. **Teetering:** Shove users and traps with a
  full 6; `teeteringFallbackDrop` = Dazed (the Guest of Honour's 50%
  phase becomes (d), announced Guests).
- **Morale:** 714‰ (the 50–74% band).
- **Veils (RC3):** only the Pallbearer and the Late Uncle wear dropped
  veils; every non-fearless Wake type wears its veil lifted or pinned
  back, face fully visible; the Guests wear crepe armbands, not veils.
- **Law, the sleeping house:** a dormant bias with coffin ambush tells
  within 6; **no dormant bias in groups within hearing of main-path rooms
  0–2** (balance critique; a `placementRules` entry); the floor-1 death
  share is reported per theme.
- **`signatureFeature`:** coffins as fixed Low cover (units never
  occupy a piece; hiding comes from Tall canopies and Drapes; change
  TR-22, told plainly; room terrain design 7.4) and
  drawn curtains (line-of-sight blockers the party can open or close:
  the Drape Portal kind; drawing one in combat costs the action; change
  FL-7, floor layout design 9.2).
- **Hooks:** manor-and-crypt layout (galleries with niches); traps: a
  creaking floorboard (noise), a falling coffin lid (damage only); cover:
  coffins, pews and catafalques.
- **Terrain Law (change TR-22; room terrain design 7.4):** canopies and
  Drapes as sight breaks; Overturn candelabra (M3).
- **Role props (RC2):** Pallbearer, the coffin lid; Undertaker, a tall
  black-plumed staff; the Late Uncle's rest tell, a cane tapping in
  time; minions share black crepe. **Faction motif:** armbands and
  lilies.
- **Bosses:**
  - **The Guest of Honour:** Eulogy (Frightened signature); at 50%, (a)
    Last Rites (Dazed).
  - **The Executor of the Will:** reads the will (Frightened signature);
    at 50%, (d) announced Guests.
  - Escorts are Guests, Cousins and the Undertaker only; **never** the
    Pallbearer or the Late Uncle (this is `F1EscortBreakable`).
- **Sound:** a funeral march slowed to a waltz; muffled sobbing as
  rhythm; no bells (RC7).
- **Gate to join the M2 floor-1 pool:** 8.2 (c), plus the
  Wailer-against-a-concentrating-Arcanist row.

### 8.6 Middle Dream: 4. The Drowned Choir (F2–3; M2 core)

- **Look:** a half-flooded nave, organ pipes of storm-glass, choristers
  with water pouring from their mouths. Standing water appears only on
  water squares; the black water beyond the walls reads as backdrop
  (RC1). Palette: drowned teal-black and storm-glass blue-grey, with
  tarnished silver. *Dream device:* behind the pipes, water falls
  upward. *Deep dressing (F3):* higher water lines on the walls, darker
  vault.
- **Obsession:** the hymn must never stop or the building drowns, so
  they sing in shifts, forever. **Grim reading:** a drowned fishing
  village's cult.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Chorister | minion | normal | hymnal (bludgeoning) + Help |
| Eel-Acolyte | skirmisher | skittish | cold bite |
| Drowned Verger | soldier, `armed`, `promotable` | fearless (drowned, minded) | mace (bludgeoning) + Shove |
| Clapper-Monk | brute | normal | bell-hammer (bludgeoning) + Shove; limited Peal (Staggered) |
| Storm Organist | artillery | normal | lightning; limited line 4 Crescendo |
| Tidecantor | controller | normal | Riptide Note (cold, Chilled minor); limited Pull Under (Grappled) |
| Deacon | leader | steady | cold-iron crozier (cold), Frighten, Command |

- **The 6:** Soaked (water squares), Chilled, Staggered, Grappled, plus
  Prone and Frightened. **The water rule is met:** cold and lightning
  threats, no fire or acid.
- **Damage:** bludgeoning, cold, lightning. **Weaknesses:** Verger to
  radiant (spirit), Organist to bludgeoning (physical); anyone in water
  is Soaked. `counterType` lightning. `frost` true. Legal overlay:
  Chorister bludgeoning → cold. Teetering fallback: drop Grappled.
- **Law, the drowning line** (the approved Soaked row and ARC-8 only):
  Soaked targets are weak to cold and lightning, so riders land
  automatically; lightning arcs reach every adjacent unit; fire ends
  Soaked, but Soaked resists fire. The Arcanist can turn the floor
  around.
- **Placement rules (balance critique; lead's call):** no water square
  under or adjacent to a fight room's party entry squares; at least 1
  dry island within 2 of each entrance; at most half of an encounter's
  enemy start squares in water; the enemies 6.4 round-1 guards computed
  with the party Soaked and the arc fan-out counted, and Conduct (K3)
  counted (change TR-26; room terrain design 6.8). These caps hold in
  every hazard state, Tide states included (change HZ-3; hazards design
  6.2.4, 11.3 `EntryCapsAllStates`).
- **`signatureFeature`:** dry pews and dais islands; sluice gates.
- **Hooks:** nave layout with side chapels; traps: sluice (one-shot
  Soak), falling chandelier (damage); cover: pillars; Wrecker: shoving
  into water; the Hymn Riddle lock (M3; change TL-32; traps and locks
  design 7.5).
- **Terrain Law (change TR-22; room terrain design 7.5):** Conduct
  across water (K3); the bell.
- **Bosses:**
  - **The Drowned Carillon** (F2, level 6): Undersong (Frightened
    signature); at 66%, (e) floods squares (never under party units); at
    33%, (d) Choristers.
  - **The Precentor** (F3, level 8–9): Crescendo lightning line
    signature; at 66%, (b) profile swap; at 33%, (d).
- **Role props (RC2):** Drowned Verger, a pew-back carried as a shield;
  Deacon, the crozier emblem; Tidecantor, an orbit of water droplets.
  **Faction motif:** wet surplices and hymnals.
- **Sound:** a submerged organ, a choir a quarter-tone flat, muffled
  low chimes (no alarm-bell timbre, RC7).

### 8.7 Middle Dream: 5. The Silk Archive (F2–3; M2 core)

- **Look:** stacks rising out of sight, webs strung between them above
  head height only (floor-level web is always a web square, RC1),
  spider-scribes copying one book whose last page is missing. Vellum,
  ink black, lamp gold. *Dream device:* the stacks have no top. *Deep
  dressing:* stacks leaning inward.
- **Obsession:** finish the book; overdue borrowers are shelved. **Grim
  reading:** a spider-haunted scriptorium.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Inkling | minion | mindless (animated ink) | nib (piercing) |
| Scribe-Spider | controller | normal | bite (piercing, Poisoned minor); limited Web Shot (Restrained) |
| Margin-Crawler | lurker | skittish | piercing; Ambush opener (Poisoned); hides in shelves |
| Spine-Binder | brute, `promotable` | normal | awl-pike (piercing) + Shove; limited Stitch (Grappled) |
| Shusher | soldier, `armed` | normal | Keen; glaive (reach, piercing) + Shove; Interpose |
| Misprint | artillery | fearless (a misprint ghost, minded) | psychic bolt; limited burst Redaction |
| Head Archivist | leader | steady | quill-sword (piercing), Frighten, Command |

- **The 6:** Restrained (web squares, Web Shot), Poisoned, Unfocused,
  Grappled, plus Prone and Frightened.
- **Damage:** piercing, psychic. **Weaknesses:** spiders and paper to
  fire (elemental), Misprint to radiant (spirit). `counterType` fire.
  Legal overlay: Inkling piercing → psychic. Teetering fallback: drop
  Grappled.
- **Law, webs and silence:** webs choke the aisles; a Keen-heavy
  awareness mix makes noise draw joiners within the caps (near pairs at
  the top of the band, +1 to the cap; FL-11, floor layout design 6.6,
  7.6); the Shade
  shines and loud parties pay. **Fire is the key** (web breakers and the
  paper weakness): the mirror of the Choir. **Paper burns** (change
  TL-32; M2; traps and locks design 7.6): fire disarms the theme's
  `paper` traps (the ink-pot plate, the Paper-Cut Tray) with contents
  untouched, and any fire opens the web seal lock (noise 4); the Index
  Riddle lock is M3.
- **`signatureFeature`:** full-height stacks (line-of-sight walls,
  narrow aisles for Skewer and reach, Acrobatics gaps for armour 7.3).
- **Hooks:** traps: a toppling shelf (damage only), an ink-pot plate
  (Poisoned), a whispering catalogue (noise). **Boxed lurker (change
  TL-28; agreed, traps and locks Q2; M3; traps and locks design 6.14,
  7.6):** the Margin-Crawler in a catalogue drawer, its rest tell (a
  page that turns by itself) re-authored at container scale; the drawer
  never shares a host with a catch.
- **Terrain Law (change TR-22; room terrain design 7.6):** burn webs off
  the aisles (K2); lamps against fire-weak spiders.
- **Bosses:**
  - **The Mouthless Librarian** (F2): "Shh" (Frightened signature);
    limited Index Card (Restrained); at 66%, (e) webs; at 33%, (d)
    Inklings.
  - **The Weaver of the Index** (F3): Cross-Reference (a limited
    Restrained line, signature); at 66%, (b); at 33%, (e) webs.
- **Role props (RC2):** Shusher, a lectern board; Head Archivist, a
  raised quill-standard; Scribe-Spider, an orbit of loose pages;
  Margin-Crawler's rest tell, a page that turns by itself. **Faction
  motif:** ink-stained vellum and spectacles.
- **Pending request (change IR-12; the RS owner rules; segment 9
  supports; M2; item rolls design 4.12; cross-document comparison 2026-11-23, CC-87):** flag the Spine-Binder `armed` (it already wields
  an awl-pike), so the floor's elite notable can be carried; until the
  owner rules, the roster stands and the elite notable falls to the lair
  cache as approved (item rolls design 14.2).
- **Sound:** scratching quills, page turns as percussion, harpsichord;
  the bed keeps a steady floor level, with no sudden silences that
  could be read as a stealth cue (RC7).

### 8.8 Middle Dream: 6. The Carnival of the Last Evening (F2–3; M2 core)

- **Look:** a funfair at permanent dusk; bruise-purple and faded
  red-and-cream tents pegged to stone; carousel horses fixed on poles
  that never move or breathe (the living Carousel Horse is a unit, RC1;
  at M3 the F3 Turning Ring rotates only the ring boards, a pre-proven
  Stir state, with the horses on their poles, change HZ-18, told
  plainly; hazards design 7.7).
  Fire's saturated hue appears only on fire squares, Hot pieces and
  attacks (RC4; change TR-20, room terrain design 8.1).
  *Dream device:* the dusk sky's sun is a painted backdrop that never
  sets. *Deep dressing:* under the big top.
- **Obsession:** the show goes on for an audience long dead; tickets
  cost a memory. **Grim reading:** a plague-town fair that never closed.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Roustabout | minion | skittish | tent stake (piercing) |
| Knife-Thrower | skirmisher | normal | knives (piercing) |
| Lion-Tamer | soldier, `armed`, `promotable` | normal | prod-fork (piercing) + Shove; the chair silhouette |
| Strongman | brute | normal | barbell (bludgeoning) + Shove; limited Bend the Bar (Staggered) |
| Carousel Horse | brute | mindless construct | trample (bludgeoning) + Shove; limited Rear (Staggered) |
| Fire-Eater | artillery | normal | thrown torch (fire); limited line 4 breath; limited burst 1 Lamp-Oil Toss (Oiled) |
| Ringmaster | leader | steady | cane-sword (piercing), Frighten, Command |

- **The 6:** Burning, Smouldering (fire counts 2), Oiled, Staggered,
  plus Prone and Frightened.
- **Damage:** piercing, bludgeoning, fire. **Weaknesses:** Fire-Eater to
  cold and the painted Horse to fire (elemental), Ringmaster to psychic
  (spirit). `counterType` cold. Legal overlay: Roustabout piercing →
  fire. Teetering fallback: drop Staggered.
- **The only M2 fire theme.** It keeps Emberward live, which is why it
  is core.
- **Law, oil and fire both ways** (the Oiled row; conditions 11.4
  burn-away): Fire-Eaters Oil heroes then ignite them; party fire pays
  out on Oiled carnies or burns slicks away; Strongmen shove toward fire
  squares. Locks (change TL-32; traps and locks design 7.7): the
  **High-Striker** (M2), a no-roll Strength threshold on a padlocked
  prize cage that sounds the reserved alarm cue; the Ticket lock (M3;
  coin buys access, never loot).
- **Placement:** no oil or fire squares under or adjacent to party entry
  squares; the 6.4 guards computed with the party Oiled; the two-turn
  flask-then-breath combo counted as a spike; the entry caps hold in
  every hazard state (change HZ-3; hazards design 6.2.4, 11.3
  `EntryCapsAllStates`).
- **`signatureFeature`:** oil slicks and fire pits; a Wheel of Knives
  set piece.
- **Hooks:** ring-plaza layout (open rooms for Cleave and reach), the
  plaza partitioned in the rules into 2–3 ordinary rooms by full-height
  wagon and booth rows and read as one fairground on screen (`hubRim`;
  change FL-1, floor layout design 7.7); one plaza near pair (FL-11,
  floor layout design 6.6); cover from wagons, and beast crates drawn
  solid-sided, never as barred cages (change TR-22; room terrain design
  7.1); trapdoor stages (Prone).
- **Terrain Law (change TR-22; room terrain design 7.7):** the oil keg
  into oil-then-fire.
- **Bosses:**
  - **The Strongest Man Who Ever Lived** (F2): Hold Aloft throw (Prone
    signature; the name never appears in the banner, RC7); at 66%, (e)
    oil; at 33%, (d).
  - **Madame Ashes, the Final Act** (F3): Curtain of Flame (a fire line,
    signature); at 66%, (e) fire squares; at 33%, (d) the Menagerie
    Wagon opens (wagon bolt), announced (change BR-16; cross-document comparison 2026-11-23, CC-69).
- **Role props (RC2):** Lion-Tamer, the chair; Ringmaster, a tall
  plumed baton; Knife-Thrower leans low behind a target board.
  **Faction motif:** stripes and greasepaint.
- **Sound:** a slowed, detuned calliope; laughter loops with dropouts,
  which duck to silence on any downing or death (RC8).

### 8.9 Deep: 7. The Long Supper (F4 in M2; F3–4 band from M3)

- **Look:** a banquet hall miles long, frozen mid-toast. Frosted swans,
  icicle candle-flames, ice sculptures of diners with no faces, in poses
  no unit uses (RC1). Frost lies on walls, tables and ceilings only,
  never on walkable floor (the Supper has no ice square). Palette: frost
  white over oxblood and old silver, desaturated; the saturated cold hue
  is kept for attacks (RC4). *Dream device:* the far end of the table
  shows the same feast again.
- **Obsession:** no one may eat until the empty chair at the head of the
  table is filled, and the seating chart is law (hunger; re-pointed after
  the check, away from the Wake's etiquette). **Grim reading:** a winter
  famine feast.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Frozen Footman | minion | mindless | iced decanter (bludgeoning) |
| Glutton | brute, `promotable` | normal | belly slam (bludgeoning) + Shove; limited Second Helping (Staggered) |
| Butler-at-Arms | soldier, `armed` | normal | rapier (piercing) + Shove; Parry; silver salver shield |
| Sommelier | controller | normal | iced wine (cold; Poisoned minor, a corked vintage); limited `freezing` Chilled Pour → **Frozen** (hard; `readyAt` 2, cooldown 3; at most 1 source per encounter, enemies 10.3) |
| Ice Sculptor | artillery | normal | cold shards; limited line 4 |
| Carver | skirmisher | skittish | carving fork (piercing) |
| The Seneschal | leader | steady | silver ladle (bludgeoning), Frighten, Command |

- **The 6:** Chilled, Frozen, Staggered, Poisoned, plus Prone and
  Frightened. **Teetering:** Shove users and traps (icicles, the
  dumbwaiter) with a full 6; `teeteringFallbackDrop` = Poisoned (the
  Sommelier's at-will minor becomes Chilled).
- **Damage:** cold, bludgeoning, piercing. **Weaknesses:** Footmen to
  fire (elemental), Glutton to piercing (physical). `counterType` fire.
  `frost` true (Chilling legal). Legal overlay: Carver piercing → cold.
- **Cowable group:** the Carver is the Supper's skittish type, so
  Scavengers (2–3 skittish standards or minions, enemies 17.4) fill on a
  Supper floor 4; 6 of 7 types stay non-fearless (857‰).
- **Law, freeze and shatter both ways:** cold riders Chill, the
  Sommelier's pour Freezes, bludgeoning shatters, and fire ends Chilled
  (and Frozen; change TR-22, room terrain design 7.8).
  The party's bludgeoning and cold users get the same combo against the
  diners. The **Iced padlock** (change TL-32; M2; traps and locks design
  7.8): picking it has disadvantage, any fire thaws it to normal, and a
  bludgeoning Smash shatters it at Athletics Easy (noise 6); the
  Place-Setting Riddle lock is M3.
- **Deliberately dry** (no condition squares; no ice), as the Deep
  contrast to the Gut; condition-free squares appear (shattered-crystal
  harm squares, Rough, Plank; change TR-22, told plainly; room terrain
  design 7.1, 7.8).
- **`signatureFeature`:** the long tables, split into Tall high tables
  (line-of-sight walls, corridor play, Skewer) and Low dining tables
  (cover), and braziers as Hot hazard pieces that give cover and hurt;
  fire ends Chilled **and Frozen** (change TR-22, told plainly; room
  terrain design 7.8).
- **Hooks:** traps: falling icicles (damage) and a dumbwaiter drop
  (Prone).
- **Terrain Law (change TR-22; room terrain design 7.8):** braziers end
  Chilled and Frozen, both ways.
- **M3:** the Supper's floor-3 window waits for M3; its layout (`axis`,
  ring Blueprints around a kitchen hub) already reads apart from the
  Choir under `LayoutDistinct` (floor layout design 7.8, 12).
- **Role props (RC2):** Butler-at-Arms, the salver shield; the
  Seneschal, a tall white steward's rod; Sommelier, an orbit of frosted
  glasses; minions carry decanters, never trays. **Faction motif:**
  napkins and place cards.
- **Pending request (change IR-12; the RS owner rules; segment 9
  supports; M2; item rolls design 4.12; cross-document comparison 2026-11-23, CC-87):** flag the Butler-at-Arms `promotable`, so the
  floor's elite notable can be carried; until the owner rules, the
  roster stands and the elite notable falls to the lair cache as
  approved (item rolls design 14.2).
- **Boss: the Head of the Table** (F4, level 10–11): fills the empty
  chair and still forbids the meal. Signatures: **Toast** (announced,
  Frightened) and **Frozen Toast** (the one hard-control Frozen); at
  66%, (b); at 33%, (d) Footmen. M3 adds **the Last Course** (F3) when
  the band opens.
- **Sound:** a string quartet frozen into held chords; clinking glass.

### 8.10 Deep: 8. The Gut of the World (F4; M2 core)

- **Look:** a gut the size of the world, with no owner shown (7.5).
  Ribbed, breathing corridors; tenants' houses and chapels built inside.
  Flesh pink and bone with a desaturated bile olive; the saturated bile
  green is kept for bile squares and acid (RC4); wet claymation
  materials. *Dream device:* the chapels in the backdrop are slowly
  being digested.
- **Obsession:** domestic life inside a monster; tenants complain about
  your noise and the rent. **Grim reading:** a leviathan's belly.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Tenant | minion | skittish | fork (piercing) |
| Gutworm | lurker | skittish | bite (piercing); Ambush opener (Poisoned) |
| Antibody Knight | soldier, `armed`, `promotable` | fearless (masked) | spear (piercing) + Shove |
| Bile Ooze | brute | mindless (bleed immune; with mindless's `mind`, 2 tags, 1 control) | acid slam + Shove; limited Gulp (Grappled) |
| Acid-Spitter | artillery | normal | acid; limited burst 1 |
| Spore-Priest | controller | normal | acid (Poisoned minor); limited Sporecloud (Poisoned) |
| The Landlord | leader | steady | cleaver (necrotic), Frighten, Command |

- **The 6:** Corroded (acid; bile squares), Withered, Poisoned,
  Grappled, plus Prone and Frightened.
- **Damage:** acid, piercing, necrotic. **Weaknesses:** Ooze to cold
  (elemental), Tenants to radiant (spirit). `counterType` cold. Legal
  overlay: Tenant piercing → acid. Teetering fallback: drop Grappled.
- **Law, attrition before the final:** Corroded strips layers and
  armour, Withered blocks healing, poison makes Antidote and Remedy
  matter. The **Fed sphincter** (change TL-32; M3; traps and locks
  design 7.9): 1 supply opens it with no check.
- **Balance caps (balance critique; illustr.):** at most 2 acid
  sources per encounter (a source is an acid-dealing unit, a swollen
  sac, Brim or the Gullet; bile squares, and so the Acid Tide, are never
  counted; change HZ-5, agreed text, told plainly; hazards design 6.5),
  the Tenants' acid overlay counted (`AcidSourcesAfterOverlay`; change
  EP-31; M2; encounter placement design 6.4);
  armour
  `verbDefencePermille` measured on this theme first; ClassThemeSpread
  must hold for the Vanguard.
- **Bile squares** (Corroded on entry; change RT-13, accepted by the
  client on 2026-11-21; cross-document comparison 2026-11-23, CC-82).
- **`signatureFeature`:** bile pools and sphincter doors (locks hook).
- **Hooks:** looping tube layout; one resonance near pair (FL-11, floor
  layout design 6.6, 7.1); rib-pillar cover; acid-geyser trap (one-shot
  Corroded; named the Acid Gland, traps and locks design 7.9, A.5).
  **Keys (change TL-23; M2; traps and locks design 6.11, 7.9):** the
  house keys on strings are a wire whose roll-only disarm gains the
  Tenant's key for the Tenant's strongbox, and the Landlord's ledger
  lock has a Keybearer. **Boxed lurker (change
  TL-28; agreed, traps and locks Q2; M3; traps and locks design 6.14,
  7.9):** the Gutworm in a swallowed strongbox, organ-swollen, always
  unlocked and with no hasp, its rest tell (a pulsing bulge)
  re-authored at container scale.
- **Terrain Law (change TR-22; room terrain design 7.9):** sacs burst;
  Oozes shove toward molars.
- **Bosses (two, because the Gut carries floor 4 in half of runs):**
  - **The Pylorus**, a door that is a mouth: Swallow (Grappled
    signature); at 66%, (e) bile; at 33%, (d) Tenants.
  - **The Appetite:** Digest (Withered signature); at 66%, (b); at 33%,
    (d).
- **Role props (RC2):** Antibody Knight, a rib-plate shield; the
  Landlord, a tall rent-ledger standard; Spore-Priest, an orbit of
  spores; Gutworm's rest tell, a pulsing wall bulge. **Faction motif:**
  aprons and house keys on strings.
- **Sound:** wet drones, a heartbeat that quickens near the boss.

### 8.11 Heart: 9. The Dreamer's Heart (F5 fixed; M2 core)

- **Look:** a bedchamber the size of a cavern, dreamt at a child's
  scale, with no age or identity shown (7.5). Bedposts like towers, the
  moon through the curtains; the carpet-ocean lives on walls and the
  skybox only, never on walkable squares (RC6); loose bedsheets appear
  only as bedsheet squares or above head height (RC1). Palette:
  moonlight blue-grey and faded nursery rose over sheet off-white. Toys
  are played sinister, never cute. *Dream device:* the room's far wall
  is always the same distance away. Heart Curiosities are architectural
  or giant-furniture images, with at most one domestic-bedtime prop per
  visit (`HeartBedroomCap`) and no wind-up or winding prop (change
  EV-25; M2; events design 4.11).
- **Keepsakes** (9.3) dress it with the visited themes' props.
- **Hooks:** the single bedchamber is partitioned in the rules into
  ordinary rooms by full-height giant furniture and read as one place on
  screen; the main path spirals inward to the bed arena (`spiral`;
  change FL-1, floor layout design 7.10).
- **Terrain Law (change TR-22; room terrain design 7.10):** kick the
  giant ball.
- **Obsession:** to keep sleeping (rest). **Grim reading:** the tomb of a
  sleeping god.

| Type | Role | Profile | Kit |
|---|---|---|---|
| Figment | minion | mindless | psychic touch |
| Sleepwalker | soldier, `armed`, `promotable` | skittish | fire-poker (piercing) + Shove; headboard shield |
| Storybook Knight | soldier | skittish (a hero who failed, still re-enacting the tale; cowering reads as shame) | lance (piercing) + Shove; Interpose; storybook-cover shield; visor up (RC3) |
| Night-Mare | skirmisher | fearless (minded beast) | dread hoof (psychic) |
| Insomniac | artillery | normal | alarm-bolt crossbow (piercing); limited burst Clangour (psychic) |
| Lullabist | controller | normal | psychic (Poisoned minor: a sleeping draught); limited Lullaby (Dazed) |
| Mother-Shape | leader | steady | knitting-spear (piercing), Frighten, Command |

- **The 6:** Unfocused, Poisoned, Dazed, Restrained (bedsheet web
  squares), plus Prone and Frightened. No hard control.
- **Damage:** psychic, piercing. **Weaknesses:** Figments to radiant
  (spirit), Insomniacs' moth-wool to fire (elemental), Night-Mare to
  bludgeoning (physical). `counterType` radiant. Legal overlay:
  Sleepwalker piercing → psychic. Teetering fallback: drop the bedsheet
  squares.
- **Poisoned keeps Antidote live in every run.**
- **Psychic cap (lead's call, after the check):** the Heart is on every
  route, so its war on concentration is capped: at most 2 types with a
  psychic at-will per encounter, and Figments (psychic touch) as at most
  one minion pack per encounter. ClassThemeSpread is gated on the Heart
  and on each final boss (11.4), so the Arcanist and the Herald are not
  tuned against on every run's last floor.
- **Law, waking:** skittish dreaming groups break early (Paper burns
  applies to story-page devices, and the Story Riddle lock is M3; change
  TL-32; traps and locks design 7.10). A Routed unit
  uses the standard flee animation while it is on the map (it can still
  be attacked and cornered, enemies 14.5); the waking fade plays only
  when it leaves the floor under 14.5 (it ends a turn unseen). The same
  holds for any theme's rout fiction (RC1). The psychic war on
  concentration tests casters, within the cap above.
- **Morale shape by room (fun critique; a `placementRules` entry):**
  early main-path rooms are skittish (fast; "waking"); the elite room and
  the last guard post before the boss are led by the Mother-Shape or
  anchored by a fearless Night-Mare (enemies 14.4, 17.2: only a
  leader-role type or a Commanding soldier or controller leads). The
  floor escalates instead of deflating.
- **Role props (RC2):** Sleepwalker, a headboard; Storybook Knight, a
  storybook-cover shield; Mother-Shape, a raised knitting-spear with
  yarn pennant; Lullabist, an orbit of moths. **Faction motif:**
  nightgowns and yarn.
- **Tone (RC8; pillar 4, floor 5 absent):** no comic timing in
  animation; cower and flee animations hold, never pratfall.
- **Final-boss pool** (level 12, 3 pips, 2 boss actions, phases at
  66/33, no hard control, names played sincere; **keepers of the sleep,
  never the Dreamer, its avatar, body or relation**, checked by
  `FinalBossNoDreamer`; change BR-30; M2; boss rooms design 4.13):
  - **The Night Governess:** long-limbed, stooped under the ceiling (its
    height shown by proportion, RC6; rules-size ≤ Large, change BR-18,
    told plainly; boss rooms design 4.7.6); she tucks intruders in. Tuck In
    (Restrained signature; renamed **Smother** if its banner does not
    read sinister in the tone-by-depth review); at 66%, (e) bedsheets;
    at 33%, (d) Sleepwalkers.
  - **The Rousing Engine:** a clockwork alarm with a bell-and-hammer
    head, no animal caricature. Reveille (Dazed signature), Clangour; at
    66%, (b) profile swap; at 33%, (d) Insomniacs.
  - M3 adds **the Lullaby** (a song with a body). **Later adds a
    bodiless true-ending set piece: after the last keeper falls, the
    room wakes (every arena act at scale, in sequence, as display) and
    the party walks into the waking light** (replaces "Later adds the
    Dreamer as a true-ending boss (premise permitting, 7.5)"; change
    BR-30; agreed, boss rooms Q5; Later; boss rooms design 4.13). It has
    no unit, HP bar, plate or named banner; it cannot harm, move or
    condition any unit; victory is recorded at the last keeper's
    `BossDefeated`, before it begins; the walk is player-paced and
    cannot be failed; it obeys `FinalBossNoDreamer`. A harmful set piece
    would be a new client question.
- **Grim-reading renames** (used only if surrealism is dropped, 7.2;
  lead's call): Figment → tomb-wisp; Sleepwalker → entranced votary;
  Storybook Knight → Oathbroken knight; Night-Mare → tomb-steed;
  Insomniac → vigil-keeper; Lullabist → vigil-singer; Mother-Shape →
  tomb-matron; the Night Governess → the Shroud-Keeper; the Rousing
  Engine → the Tomb Clock; Law "waking" → "the god's slumber: its
  servants break when disturbed".
- **Sound:** the lullaby leitmotif heard whole for the first time, then
  broken; distorted quotes of every visited floor's score, stripped of
  every reserved cue timbre (RC7; change HZ-29; hazards design 8).

### 8.12 Later themes and unlock order (lead's call)

The unlock order spends breadth on the deep floors first (fun critique).

| Order | Theme | Tier / floors | Wave | Law | Blocker / note |
|---|---|---|---|---|---|
| 1 | **the Lullaby** (third final boss) | F5 | M3 | Unfocused and Frightened kit | — |
| 2 | **Mirror Court** | Deep, F3–4 | M3 | Hidden against radiant | Radiant ruling RT-15. Shade fixes: party-facing radiant on at most 2 types; the Doppel opener Blinded only; ClassThemeSpread for the Shade. 667‰, the one route-allowed theme below 700‰. Skirmisher named Shardling |
| 3 | **Ossuary Opera** | Deep, F4 | M3 | The aria: anti-caster, dry | Raises necrotic presence (Graveward) |
| 4 | Endless Wake (if not M2) | F1 | M3 | Tells and the sleeping house | 8.2 gate |
| 5 | Moth Wedding | Middle Dream, F2–3 | M3 | Light against stealth | Radiant ruling; dark side routes keep the Stealth guarantee |
| 6 | Ministry of Small Mercies | Shallows, F1–2 | M3 | Due process (morale-maximal, Weary attrition) | Psychic on floor 1 needs its own `Floor1ClassSpread` row |
| 7 | Tallow Abbey | Shallows, F1–2 | M3–M4 | Oil and censer fire | Church cap: never in a pool set with the Choir; overlaps the Carnival's square kind |
| — | Inverted Cathedral | Deep, F3–4 | M4 | Edges and light | The lethal-safe drop hazard is supplied (change HZ-26; hazards design 6.4, 7.11): rose-window Voids you fall upward from to the chandelier Snag, up to 50% of rooms, Plank catwalks, the censer-rope Cord, and the Chimes Ring Upward reversal round; its "light" Law waits for NM-9; church cap |
| — | Orchard of Teeth, Night Market, Marionette Grove ("Strung Marionette"), Ledger Halls | various | M4 | — | Each needs a compliant floor-1 boss where floor-1 eligible |
| — | The Waking City (alternate final) | F5 | M4 | — | Offered at camp 4 after a first win |

Unlockable themes and final bosses are main 4.7 content, never power
(RT-18, client, M3).

---

## 9. New mechanics considered

### 9.1 Table

| Mechanic | Adds | Time cost | Verdict |
|---|---|---|---|
| **Strata Draw** (route pre-roll, bands, pool rules, enumerator) | Deterministic variety, shareable seeds | 0 | **Adopted, M2** |
| **Antechamber Catch-up** | Level 3 before the floor-1 boss | 0 | **Adopted, M2** |
| **Stratum Card, camp card, dream track** | Theme readability; the act moment; the road not taken | 0 | **Adopted, M2** |
| **Keepsakes** (finale dressing plus one prop) | A personal finale | 0 | **Adopted, M2** (echo *types* M4) |
| **Two Doors** (camps 1–2) | One real decision per camp | about 15 s per camp | **Built M2; on only if the gates pass; else M3 (agreed, Q2)** |
| **Last Hollow choice** (final boss at camp 4) | A final decision | about 10 s | **With Two Doors, plus BossParity (agreed, Q2)** |
| Door bounties | Reward bias by door | 0 | **Rejected** (stream complexity; no gain once milestones give pull). The Perilous Door's reveal is not one: it adds no draw (read, not changed; change LE-19; loot economy design 6.12) |
| Theme milestones (profile marks) | A positive pull into hard themes | 0 | M2 marks; M3 unlocks |
| Dream Fragments (off-path Lore checks for boss intel) | Lore gets a dungeon use; a tone outlet | 0.5–1 min per floor | **M3** (it counts against the failing floor-time gate). Accepted (change TL-34; change EV-30; segment 8; M3; traps and locks design 6.12; cross-document comparison 2026-11-23, CC-87): readings, Dream Fragments included, become no-roll thresholds that each yield a floor fact. Segment 8 folds Dream Fragments into F-BOSS options on Lore thresholds, 0 VP and 0 coin, through the FactLedger's one boss-fact queue (change EV-29; M3; events design 4.9) |
| Omens (per-theme generator mutators inside the 6) | Variety inside a theme | 0 | M3 (test matrix × 4); a theme's Omen may switch on its one hazard Mood (an integer stage 0–2, capped at +1 per two rooms; change HZ-20; hazards design 6.9); Omens never touch the spoils list (LootDoorInvariant; read, not changed; change LE-19; loot economy design 6.12); an Omen floor may swap a Curiosity for its Omen Retelling (kind, not stats), and a Curiosity cost may be the named, visible event that advances a Mood within its cap (change EV-31; M3; events design 4.9) |
| Nightmare tiers (ladder tiers that add kind, not stats) | Harder modes; the old deep overlay returns at tier 3 | 0 | M3 tiers 1–3; Later 4–5 (RT-21, client); hazards escalate in kind only (a kind added, a period shortened by at most 1, or a Mood stage higher; never a band), and each floor-1 theme gains its hazard package from tier 2 (change HZ-25; agreed, hazards Q4; hazards design 6.19); Curiosities gain **Provisos**, extra printed costs, never better gains, never on the free option, ticks or noise only on floor 1 (change EV-31; M3; events design 4.9) |
| Perilous Door (+1 ladder on one floor) | Risk for choice | 0 | M3 with full Two Doors; client (a per-floor ladder, RT-21). Loot (agreed, loot Q5; change LE-19; M3; loot economy design 6.12): on its floor the boss reward card (enemies 17.5) reveals the pre-drawn fourth dreamed option, choose 1 of 4; no extra draw (6.4 LootDoorInvariant) |
| Unlockable strata | Return play through content | 0 | M3 (client, main 4.7; RT-18) |
| Alternate final | Variety at the finale | 0 | M4 |
| Lucid Key secret route | A secret floor-4 door | 0 (replaces a floor) | Later (needs a quest-item slot from items) |
| Other Dreamers (a rival party as a visitor) | A cross-theme set piece | 0 | Later (enemies 18) |
| Seams (cosmetic next-theme preview) | Foreshadowing | 0 | M3 |
| Concentrated guard posts | A lever-3 alternative | small | Simulator arm only (10.2) |
| Element Echo (offensive element-choice lines read the found floor's `counterType`) | A theme read on rolled lines, no identity or draw change | 0 | **Held for an M3 client decision, default off** (change IR-19; agreed, item rolls Q4; item rolls design 4.4; 6.4 LootDoorInvariant) |
| Mid-floor theme transitions; two-floor acts; a run-wide curse track; theme-keyed loot; an epilogue floor; cross-run weighting inside the seed | — | — | **Rejected** (theme-keyed loot stays rejected: theme bias 0‰, read, not changed; change IR-15; agreed, item rolls Q4; item rolls design 4.4). **Cross-run weighting inside the seed stays rejected (read, not changed; agreed, events Q1; change EV-20, EV-27, EV-28; events design 4.5.4, 4.5.8, 4.5.9):** Launcher Fallow acts outside the seed (R-SEED-6); M3 Curiosity Sequels (stage k+1 drawable once stage k is seen) and M3-late or M4 Ripening enter only through `drawableSet`'s unlock snapshot under `SuccessorParity`, a reading of this row, not weighting inside the seed; shared, daily and simulator seeds stay canonical |

### 9.2 Adopted in full: Strata Draw (M2; RT-2, RT-3)

As 6.3–6.4. Validated by V3, V5 and V6 (11.2): route enumeration over
every reachable history, for every drawable set the manifest can
produce; RP1–RP6; RouteSufficiency; at least 1 boss per (theme, floor);
compliant floor-1 bosses; determinism.

### 9.3 Adopted in full: Keepsakes (M2; display plus one prop)

- Floor-5 dressing uses prop sets from the 4 visited themes, chosen
  deterministically from the route history, with no random draw. No
  rule effect. **The rule (lead's call, after the check):** floor-5
  dressing slot k (numbered from 0 by the layout segment) takes its
  props from the `keepsakeProps` of the theme of floor (k mod 4) + 1.
- The boss arena's **counterplay feature** keeps one fixed rule per
  arena, defined by the boss-room segment: a "drop the heavy thing"
  one-shot within the Heart's 6. It is **dressed** from the **floor-4
  theme's** `keepsakeArenaDressing` (the Supper's chandelier, the Gut's
  hanging rib-cage), so the player recognises their route in the final
  fight. The rule never varies with the dressing. **Filled by the boss
  rooms design (change BR-30; M2; boss rooms design 4.13):** **the
  Heavy Thing**, in both final arenas: interacted at its cleat or by a
  ranged trigger, a fixed 3×3 drops at once (TrapRecord format; not a
  Cord, not a clock), dealing piercing at the harm band with Reflex for
  half and Prone on a failure, both ways, once per fight, party-only;
  dressed in neutral materials, never ice, frost or blue (the Supper's
  dark iron hoop with unlit candle cups, or the Gut's rib-cage; under
  Cut 2 only the rib-cage). Each final boss also has its own Device, of
  different kinds (the Governess's Lamp Snuffer, the Engine's
  Mainspring).
- **Keepsake dressing carries no damage-type cue (RC4):** no ice, flame,
  acid or other fluid; only the visited theme's neutral materials (wood,
  brass, cloth, glass, bone). The tone-by-depth review checks it, and
  V7 checks that every non-final theme authors both Keepsake fields.
- **Keepsake dressing never uses a container, bait-cache or Windfall
  silhouette, nor the Bedside Stand's** (change LP-24; M2; loot sources
  design 5.1, 4.10; RC1).
- M4 adds echo types (at most 1 per encounter, at most 2 encounters,
  curated to the Heart's 6).

### 9.4 Adopted in full: Antechamber Catch-up (M2; RT-8)

- **Trigger (lead's call on the details, after the check):** the event
  `AntechamberCatchUp` fires on the earlier of (a) the hero's first
  out-of-combat turn with its square in the floor-1 **antechamber**, the
  empty main-path room immediately before the boss arena (the Forecourt;
  (a) fires on squares whose `roomId` is the Forecourt), and (b) any
  attempt to enter an arena square, which covers the arena-owned
  doorway squares (change FL-5, read with FL-13; floor layout design
  6.11). The
  hero's experience becomes max(current, 540). It is **idempotent**: the
  floor-1 run state holds `antechamberCatchUpApplied`, and it never
  fires twice.
- **If combat is active, it defers** to the combat-end level-up screen
  (ability 6.9: never during combat), including when a fight that began
  elsewhere ends with the hero already in the antechamber. The arena
  cannot be entered until it resolves.
- The level-up resolves as an ordinary one, including the level-3
  subclass pick (classes 7.3). The antechamber gives a short safe walk to
  read the new kit before the boss.
- **Overflow** on a full clear is shown on the level-up screen ("ahead
  of the dream"), so carry-over feels earned.
- **Every floor-1 layout has an antechamber that dominates the arena**
  (`AntechamberDominatesArena`, a generation assertion owned by the
  layout segment) **(new lead's call; strengthened after the check)**:
  in the floor-1 room graph the antechamber is a cut vertex, so every
  path from the start to the arena passes through it, with no side
  room, second entrance or forced-movement route around it.
- **Every floor has the antechamber (change FL-5, agreed in the floor
  layout design Q2; floor layout design 6.11):** `AntechamberDominatesArena`
  holds on every floor, judged on the pre-boss graph; both arena
  entrances are dog-leg Arches from it, at least 5 apart, with the
  symmetric `AntechamberScreened`. On floor 1 it stays empty,
  single-inbound and off the Circuit, and stays near-safe by this
  section's approved exception (the "short safe walk"); its walls may
  carry Glimpses, which have no rules, no square and no click (a
  reading of "empty", not a change; change EV-2; M2; events design
  4.2). On floors 2–5 it
  is always a patrol Circuit waypoint, the patrol's homeRoom and fight
  host at the floor's fight-room size, with at most two Circuit side
  rooms; it is never guaranteed safe while the patrol lives (told
  plainly). The Catch-up stays floor 1 only.
- **Reports:** `AntechamberCatchUpRate` per class *(illustr.: about 100%
  of bypass runs, about 5% of full clears)*; the camp catch-up's "under
  20%" target (enemies 16) applies to **camps 2–4**.
- **Reruns at level 3:** the enemies 6.4 floor-1 boss guards and
  `Floor1ClassSpread`. `Floor1BossSustain` is already at level 3 (DH 21.1
  step 6), so DH needs no recalibration.
- **Told plainly:** in effect the first boss always meets a level-3
  hero.
- **Boss rooms (read, not changed; change BR-3, BR-4; M2; boss rooms
  design 4.2.1, 4.2.2):** from M2, while the Catch-up is enabled and
  `antechamberCatchUpApplied` is false, the boss and escort cannot
  notice the party, so the hero meets the F1 boss at level 3 (M1 has
  no guard); the Catch-up resolves before `ArenaEngaged` on the same
  tick. A party unit entering an arena square counts as noticed, so
  the boss fight starts as a notice, with Take Places as its placement
  step. The floor-1 Forecourt is near-safe only before engagement: from
  the first `ArenaEngaged` until the boss falls, no short rest is taken
  while any party unit stands in the Sanctum (Forecourt, arena,
  Stairhead) (change BR-31; agreed, boss rooms Q2; boss rooms design
  4.2.5).

### 9.5 Adopted in full: camp card, Stratum Card and dream track (M2; RT-10, RT-12)

As 6.9. All class-blind and information-only.

### 9.6 Built in M2, switched by gates: Two Doors and the Last Hollow choice (agreed, Q2)

As 6.11.

---

## 10. Numbers (illustrative; toy, not simulator)

All model numbers are *(illustr.)*. They come from toy scripts
(Appendix B), not from the simulator, and are a direction only.

### 10.1 Run length (companions 10.4 toy)

| Arm | Cap | Floors 4–5 lever | F4 / F5 min | Run | Gate (≤ 20) |
|---|---|---|---|---|---|
| Toy default | 3 | none | 28.5 / 28.8 | 131 | fail; over 2 h |
| Levers 1–2 | 3 | none | 23.0 / 24.2 | 114 | fail |
| Levers 1–2 | 3 | 2 guard posts | 20.2 / 21.3 | 109 | fail, narrowly |
| Levers 1–2 | **2** | 2 guard posts | 18.4 / 18.4 | 104 | **pass** |
| 4 floors, levers | 3 | none | 23.0 | 87 | fail |

- Levers 1–2 are mandatory.
- A 4-floor run is not a clock fix.
- **The cap-2 fallback is the predicted outcome (told plainly).**
- Because cap 2 is likely, **party size 3 is the reference row** for
  tuning floor 4–5 signatures, run at both caps (companions 7.4 item 3).
- `FloorTimeByTheme` gates each theme **relative** to a median (at most
  +10%) rather than against an absolute figure, which would flag every
  theme while the cap-3 gate fails. It is measured in simulator time
  under the time model of the companions 7.4 floor-time gate (seconds
  per hero action, companion turn, enemy turn and movement step, owned
  there), from fixed arrival states at both caps, and each theme is
  compared with the median over **all themes on floors 2–5 at the same
  party size**, so the fixed Heart and the floor-4 pair are actually
  gated (11.4).
- The human gate slice uses the slowest floor-4 theme by simulator turn
  count.
- **Layout commitments (change FL-16; floor layout design 6.10, 11.4):**
  at party 4 on floors 4–5, `ExploreSeconds` ≤ 3.5 min and
  `ExploreClicks` ≤ 40, as medians of the layout walker's lean policy,
  plus Circuit isolation. The walker's terms and cost table feed the
  companions 7.4 time model; layout adds **no** lever to the lever
  order.

### 10.2 Experience

| Pool | Before bypass | After one bypass | Before the boss (after a bypass) | Enemies 20.1 |
|---|---|---|---|---|
| Baseline (3 guard posts, boss 200) | 710 (131%) | 610 (113%) | 510 (410) | pass |
| Plain trim (2 guard posts, boss 200) | 610 | 510 (94%) | 410 (310) | **fail** |
| **RT-7, floor 4: 2 guard posts, boss *experience* 300** | 710 (131%) | 610 (113%) | 410 (310); the boss's 300 is spent on floor 5 | **pass** |
| **RT-7, floor 5: 2 guard posts, lair *experience* 240** | 710 (131%) | 610 (113%) | 510 (410), as baseline | **pass**, counting only experience earned before the final boss |
| Floor 5 with boss 300 (the earlier form, replaced) | 710 | 610 | 410 (310) | pass on paper only: the boss's experience arrives when the run ends |
| Drop the patrol | 640 | 540 (100%) | 440 (340) | fail |

**Lever 3's form (lead's call; change RT-7; told plainly).**
- Enemies 10.1's budget band (boss 200, "authored, including escort and
  reserves"; lair 140) and enemies 16's experience list (boss 200)
  **are already separate lists**. RT-7 edits only the 16 list for
  trimmed floors (floor-4 boss 300; floor-5 lair 240), plus the 20.1
  pool check, which on floor 5 counts only experience earned before the
  final boss. Escort and lair budgets do not rise.
- **Concentrated guard posts are rejected as the form** (balance and
  consistency critiques). The same budget filled with fewer,
  higher-level or elite units barely cuts HP-equivalents and rounds:
  about 27 s per floor at the toy's 1.5 s per enemy turn, not the assumed
  10%. It pushes elites into guard posts against DH 18.2, strips cowable
  bodies, and changes enemies 10.3.
- It stays a simulator arm and needs a measured saving of at least 1.5
  min at party 4 to be reconsidered. If ever chosen: at most 1 elite per
  guard post, and at least 1 skittish or minion slot.

**Floor 1:** a full clear reaches 539–557 at the door (level 3 in 95% of
room orders); a bypass reaches 455–482 (level 3 never); the Antechamber
Catch-up covers it.

**Carry-over:** full clears run about 1 level ahead of the enemies 6.3
table from floor 2. Trimming floors 4–5 pulls the hero back toward the
table, a step up in relative difficulty at the end.
`LevelAtDoorByFloor` reports it **(definition lead's call, after the
check)**: Δ(N) is the mean over runs of the hero's fractional level at
the floor-N boss door (level plus experience into the level ÷ 270, or
÷ 540 for level 10) minus the enemies 6.3 expected level for floor N;
the step is Δ(3) − Δ(4), and Δ(4) − Δ(5), read on RouteSample at both
caps. If a step exceeds about 0.7 level, the trim also applies from
floor 3 (`pacing.trimmedFloors` [3, 4, 5]), or the step is documented.

**Level 10:** with typical carry-over, every modelled arm reaches level
10 before the final boss. A minimum-arrival hero (level 9) who bypasses
one fight needs the elite room: 410 + 160 = 570 against the 540 of
enemies 16. **Whenever lever 3 applies, `Level10BeforeFinalBoss` is a
gate (lead's call, after the check):** level 10 before the final boss in
about 95% *(illustr.)* or more of runs at minimum arrival with one
bypass. Without lever 3 it stays a report.

### 10.3 Variety (hybrid M2 pool, recounted by the route enumerator)

| Build | Floor 1–4 sequences | With 2 final bosses | Next run repeats sequence | Floor-1 repeat | Floor-4 exposure |
|---|---|---|---|---|---|
| Approved (4 + overlay) | 1 | 1 | 100% | 100% | — |
| The brief's pure A/B per floor (Q1 option B) | 16 | 16 | 6.3% | 50% | — |
| **Hybrid M2 (Gaol, Kennel) (agreed, Q1)** | **24** | **48** | **4.2%** | 50% (R-SEED-6 caps streaks at 3) | Gut 50%, Supper 50% |
| Hybrid + Wake | 36 | 72 | 2.8% | 33% | same |
| Supper as an F3–4 band (rejected for M2) | 30 | 60 | 5.4% | 50% | **Gut about 85%** |

- With the authored `pairKey`s of 8.1 (Gaol and Kennel slashing/none,
  Wake bludgeoning/none, Choir lightning/water, Silk piercing/web,
  Carnival fire/oil, Supper cold/none, Gut acid/bile; `none` never
  matching), the soft R3 and R4 rules never bite in M2. Two Doors always
  has 2 candidates (minimum 2, exhaustive check). Recounted after the
  check: had Kennel been coded piercing (its first listed type), R4
  would skip the Silk after Kennel, giving 20 sequences and the Silk
  only about 17% of floor-2 slots; the authored primary avoids it. The
  rejected Supper band row is recounted under the same walk and coding
  (it had been computed from an older coding).
- Draft counts corrected: Model C had 44 sequences, not 52; Model A had
  11 cells, not 10. The enumerator owns every published figure.

### 10.4 Content cost

| Build | Types | Bosses | Cells | Test cells (× 4 sizes × 3 bands) | Owner reports (× 8) | (types + 8 × bosses) vs approved |
|---|---|---|---|---|---|---|
| Approved | 22 | 5 | 5 | 60 | 40 | 1.0× |
| **Hybrid M2 core (agreed, Q4)** | **56** | **15** | **11** | **132** | **64** | **2.8×** |
| + Wake | 63 | 17 | 12 | 144 | 72 | 3.2× |
| Cut 1 (drop the extra bosses of Gaol, Kennel and Gut) | 56 | 12 | 11 | 132 | 64 | 2.4× |
| Cut 2 (Cut 1, and drop the Supper; F4 fixed Gut) | 49 | 11 | 10 | 120 | 56 | 2.2× |

**Graded cut lines (change BR-26; agreed, boss rooms Q1; M2; boss rooms
design 4.10.4, 8):** the Cut 1 and Cut 2 rows above are the approved
cuts, kept for comparison. In the boss rooms toy, with Renditions
counted: M2 core 2.84×; graded 1a 2.71×; graded 1b 2.58×; Cut 2 with
Cut 2R 2.34× (a built Appetite counted inside Cut 2); core + Wake 3.21×
*(illustr.)*. The boss asset manifest is totalled against these units
by the RS owner (boss rooms design 6).

**Simulator volume (balance critique):** ThemeParity at 5 points with p
≈ 0.7 needs about 650–700 runs per cell; 11 cells × 4 classes × 2 caps
× 700 is about **62k floor simulations per balance pass**, from fixed
arrival states. Route-sampled rates are reported, not gated.

### 10.5 Floor 1 and the Herald (Herald 11.5)

With the adopted package, only the binding row (50–74%, fearless escort)
has a number: 85.5 against about 99, a spread of about 14. **Fail.** The
3.8, 6.6 and 10.2 figures belong to the non-adopted Totem-lever column.
**No floor-1 theme is proven yet.** Hence 8.2.

### 10.6 Route choice risk (logit model; base clear 0.97 / 0.90 / 0.80 / 0.70 / 0.58; run win 28.4%)

| Theme spread | Matchup sd | Random | Heuristic chooser | Oracle | Best theme's pick rate |
|---|---|---|---|---|---|
| 5 | 0.3 | 27.7 | 29.5 | 31.2 | 64% |
| 10 | 0.0 | 28.1 | 29.3 | 30.3 | **92%** |
| 10 | 0.3 | 27.6 | 29.7 | 31.4 | **74%** |
| 15 | 0.3 | 27.3 | 30.3 | 31.8 | 81% |

- Route choice adds 2–4 points of win rate, and up to 6 for an Oracle.
  That is why Reference-chooser calibration and the Oracle cap of 1/3
  apply **(told plainly with Q2)**.
- A 10-point parity gate is too loose for choice floors, so 5 points or
  less is required there **(agreed, Q2)**; 8 points or less is required
  under a random draw, as a build gate (11.4; lead's call, after the
  check).
- The dice tax with no choice, at a 5-point spread, is a 25.7–31.0% run
  win rate between the worst and best sequences.
- The 5-point row's best-theme pick rate (64%) sits just under the
  agreed 65% cap: parity and pick rate are two views of one property.

### 10.7 Coverage reports

**Neck trinkets per route (M2):**

| Trinket | Source | Live in |
|---|---|---|
| Antidote | the Heart (Poisoned) | every run |
| Emberward | the Carnival (only fire theme) | 2 runs in 3; **told plainly** to the trinkets owner |
| Graveward | the Gut (Withered) | 1 run in 2 |

`NeckLineByRun` is read under the Reference chooser when Two Doors is
on. A pool rule forcing a fire theme into every route was considered and
**rejected**: it would fix the Carnival into every run. M3's Opera
raises necrotic presence.

**Terrain (Arcanist DEP-A1):** floors 2–3 always have squares; floor 4
has squares only with the Gut (50%); floor 5 has bedsheets. **Told
plainly:** the about-40% target is likely missed on floor 4 when the
Supper is drawn. No card relies on terrain until the report passes
**(settled, Arcanist 16)**. **Pending request (change TR-21; room
terrain design 7.8, 11.3):** the Arcanist owner is asked to count
interactive features (condition squares, contact hazards, harm squares,
Kegs) for DEP-A1, with the 3–6 ring rule, and to rule which "start" is
meant; with features counted, the Supper's braziers, tureen and carving
face likely close the floor-4 miss. **Told plainly:** Arcanist E3 combos
still need condition squares, which the Supper never has.

---

## 11. Data, validation and simulator

### 11.1 Records and configuration

- **Theme records** (8.0), one per theme, in `content/` **(settled,
  main 6.5)**; boss records gain `themes[]` and `final` (RT-4).
- **Configuration snapshot** (hashed into the reproducibility key and
  fixed at run start, 6.4 R-SEED-5; lead's call on the fields; it also
  holds the profile grants that change rules, `difficulty.tier`,
  `boons[]` and `nightmareTier`, sorted canonically, with canonical
  values for shared and daily runs (change SV-12; lead's call; field
  reserved in M1, live in M3; seeds and validation design 4.6);
  `difficulty.tier` drives ladder% (enemies 10.1), and `nightmareTier`
  (M3) drives the Nightmare kind additions only and never changes
  ladder% (main 4.7 **(decided)**, "kind, not stats"; RT-21)
  (cross-document comparison 2026-11-23, CC-84)):
  `route.twoDoorsEnabled`, `route.lastHollowChoiceEnabled` (both false
  by default; 6.11), `route.doorCamps[]` (M2: [1, 2]),
  `pacing.trimmedFloors[]` (default empty; lever 3: [4, 5]; 10.2
  response: [3, 4, 5]), `recruitSchedule.companionCap` (3, or 2 under
  the fallback; cross-document comparison 2026-11-23, CC-83);
  `layout.mainPathRooms`, `layout.mainPathRoomsTrimmed`,
  `layout.sideRooms`, `layout.sideRoomsTrimmed`, `layout.secrets`,
  `layout.hearingGapMin`, `layout.nearPairsMax`,
  `layout.lastTurningWeight` and `layout.wayBack` (default off, M3), all
  hashed into the reproducibility key (change FL-14; floor layout design
  11.1); the `terrain.*` tuning (including `terrain.maxSockets`) and
  `cover.*` (`cover.effect`, `cover.penalty`), also hashed into the
  reproducibility key (change TR-17; room terrain design 11.1, 6.2.5).
  Loading rejects `lastHollowChoiceEnabled` true with `twoDoorsEnabled`
  false. `route.streakRedrawMax` 8 *(illustr.)* is launcher-only and not
  in the snapshot, as are `curiosity.fallowK` = 3 and
  `curiosity.fallowFloors` = [1], client values, not tuning (change
  EV-20, EV-40; agreed, events Q1; M2; events design 9.1).
- **Route state** in the run save (lead's call, after the check): the
  catalogue permutation (stable ids), the boss order per (theme, floor)
  cell, the final-boss order, the unlock snapshot, the configuration
  snapshot, the committed theme and boss per floor so far, and the
  `DoorChosen` log (the last choice before Travel on counts). Later
  floors are recomputed by the walk (R-SEED-4), which uses no RNG.
  Floor-1 run state adds `antechamberCatchUpApplied` (9.4).
- **Core events:** `RouteDrawn` (payload: the permutation, the boss
  orders and the final-boss order, not five themes, since door floors
  are not known at run start), `DoorOffered`, `DoorChosen`,
  `StratumEntered`, `AntechamberCatchUp`, beside the companions
  telemetry events (`FloorEntered`, `BossDefeated`).

### 11.2 Content validation (change RT-17; definitions new lead's call)

`s` and `t` parts fail the build; `g` and `r` parts act by their
registered class (change SV-20; lead's call; M1 model, M2 complete;
seeds and validation design 5.1: every check has a qualified id
`Vn.CheckName`, one owner, a phase `s`, `g`, `r` or `t`, and every `g`
and `r` check one failure class; simulator gates move to the report
registry, 11.4):

| Id | Validator | Checks |
|---|---|---|
| V1 | ThemeBudget (owns `V1.ThemeSix`, called by V9, V10, V11, V16s and V17s; change SV-20) | Every theme meets enemies 10.6 and conditions 12.2 and 16, with its 6 recomputed from role taxes, riders, boss kits, traps, terrain and every legal overlay; `counterType` is a declared weakness or square weakness; `themeDamage` and `hardenedLayer` follow the 8.0 rule and stay inside the 6; per-type conditions at most 2; tag immunities within the budget (2 per non-boss trait set, at most 1 control, mindless counting `mind`); every floor-4 and floor-5 theme has a skittish type; a Teetering fallback drop on every theme with a full 6 |
| V2 | BossCoverage | At least 1 boss per (theme, eligible floor); floor-1 bosses pass `Floor1BossMindReachable` and 8.2 (b), and (c) where it binds (`F1EscortBreakable`); no boss immune to its theme's `counterType`; every boss has 1–2 named signatures; final bosses use only the Heart's 6. **Single owner (change SV-20; seeds and validation design 5.2):** V2 checks coverage only; the boss-kit rows (`Floor1BossMindReachable`, `F1EscortBreakable`, signatures 1–2, `counterType` immunity) are owned by V17s |
| V3 | PoolRules | RP1–RP6 (6.4) over every theme and every pair that can sit on adjacent floors (RP6 unweighted over the roster); the church cap (7.6); one home theme per square kind (6.3); Obsession kinds (7.3 pillar 6) |
| V4 | ThemeFloorFill | Enemies 10.5's content test over every (theme, floor) cell × party size × band × ladder tier, trimmed floors included |
| V5 | RouteEnumeration | Enumerated over **histories**, not permutations: for floor 1, each drawable floor-1 theme; for floors 2–4, each ordered choice of survivors the walk can produce (Door A and Door B where doors apply). Run once for **every drawable set the manifest can produce**: every subset of unlockable themes, crossed with the Wake verdict, the cut states and any held theme. The build fails if any history breaks RP1–RP5 or RouteSufficiency (RP4), or a Two Doors floor has fewer than 2 candidates. The 10.3 counts are published from the all-unlocked set |
| V6 | RouteDeterminism (a `t` part calling the V19t harnesses; M1 stub, M2; change SV-20) | The same reproducibility key (seed, `genVersion`, manifest hash, configuration-snapshot hash, unlock-snapshot hash; change SV-12) gives the same route; shared and daily seeds use the canonical unlock set; perturbing any floor substream or a generation retry never changes the route; unlocking a theme never changes another theme's position or the boss orders; a doors-on run choosing Door A at every camp is identical to the doors-off run; LootDoorInvariant holds for both doors, including the Resistant-element draw and its n = 0 fallback |
| V7 | CharterChecks | Every bark trigger is in RC8's list; one glyph per square kind (RC5); a Tone Sheet referenced; every non-final theme authors `keepsakeProps` and `keepsakeArenaDressing`; no frozen-name collision; no keyhole silhouette outside mindless tells (RC3); no keyhole on any container (change TL-28; traps and locks design 8, 11.3 `NoKeyholeOnContainers`). The "signature type placed at least twice per floor" check (enemies 10.4) is a **generation assertion** over a fixed seed sample per (theme, floor, party size), owned by the seeds and validation segment. **Pending request (change SV-20; request to EP; M2; seeds and validation design 5.2, 5.7):** its authority moves to `V12g` (signature ≥ 2) and the fixed sample becomes a V12t regression row over the release set; if EP refuses, V19t runs the sample row. V7 owns `V7.NameRegistry` (manifest-driven; every segment feeds it) |
| V8 | LayoutGrammar (change FL-14; floor layout design 11.2) | Every theme's `layoutGrammar`, Blueprints, rules and templates, with the named sub-checks of floor layout design 11.2 (LG1–LG20, `HearingGap`, Circuit isolation, `LayoutDistinct`, config nesting and others) |
| V9 | RoomTerrain (change TR-18; room terrain design 11.2) | Every template × orientation × layer × floor band × theme, plus the floor-level capacity rows per Blueprint and rule choice, with the named checks of room terrain design 11.2 (`ClearwayConnected`, `ProtectedIntact`, `KitCoversRulesKeys`, `EntryCaps`, `ThemeSix`, `HomeTheme`, `PhaseSocketsBoth`, `TerrainDrawCount` and others); partial in M1, full in M2 |
| V10 | RoomHazards (change HZ-28; hazards design 11.3) | After V9, every template × orientation × layer × state × floor band × theme × Mood stage × tier class, with the named checks of hazards design 11.3 (`HazardPointsWithinBudget`, `OneClockPerRoom`, `F1NoHazards`, `EntryCapsAllStates`, `VoidQuotasAndGeometry`, `SnagProven`, `PushLaneExists` and others); `EnvRoundBound` is a report, not pass/fail |
| V11 | TrapsLocks (change TL-20; M1 skeleton (`LockNeverCritical`, `HeNoLock`, `KeyReachable`), M2, M3 Trapwright (wave corrected, change SV-20); traps and locks design 6.20, 6.22, 11.3) | In two passes (`traps`, then `trapsResolve` after encounter placement), the named checks of traps and locks design 11.3: TP1–TP12, `HunchTrue`, `WagerNonSkillLine` and the Tattle host rule, `HiddenTrapBound`, `TrapLethalityBound`, `EveryDamagingTrapHasSave`, `F1TrapRules`, `MimicPlacement`, `RiskRoomShare`, `HardControlGates`, `HarmfulConditionsRecount` (with V1), `NoKeyholeOnContainers` (with V7) and `ReservedTellSilhouettes` (with V9); with the retry and terminal fallback of traps and locks design 6.20 |
| V12 | EncounterPlacement (change EP-32; static parts and the fallback chain M1, the rest M2 (wave corrected, change SV-20); encounter placement design 6.1, 11.2) | In three parts (build time on the worst case, generation time per seed, runtime assertions), the named checks of encounter placement design 11.2, with the per-slot fallback chain, one segment-6 retry and the terminal floor fallback of encounter placement design 6.1 |
| V13 | LootEconomy (change LE-23; V13s (a), (c), the floor-1 V13g (g), (i) and the (m) test M1, (o) M3, the rest M2; loot economy design 10.2; cross-document comparison 2026-11-23, CC-85) | In three parts: V13s, static over the data (every floor × party size × cap × variant × unlock set); V13g, at generation at the final floor assertion point AP2 after LootBind (change SV-20; seeds and validation design 5.2, 5.4), each check with its named action; and the (m) integration test (fixed, identical draw counts across seeds, doors, waves and opening methods); the named checks of loot economy design 10.2 |
| V14 | ItemRolls (change IR-21; (a), (b), (d), (h), (j) and the draw-count test M1, (m) Later, the rest M2; item rolls design 9.2; cross-document comparison 2026-11-23, CC-85) | Beside V13: static checks (a)–(m) of item rolls design 9.2, among them lanes, values and rolled lines, signature forms and carriage, display isolation (no rules code reads `theme.loot`; a release configuration with Element Echo on fails) and a complete `theme.loot` for every drawable theme (8.0); generation-time asserts; and the constant-draw-count and LootDoorInvariant tests |
| V15 | LootSources (change LP-31; the V15 M1 rows M1 (an owner deliverable, request to LP; change SV-20), V15s and V15g M2, the Windfall rows M3; loot sources design 9.2) | Beside V13 and V14, split like V13 and run at AP2 after V13g and `V16g.SlotLegal` (change SV-20): V15s, static (container skins per kind, `ContainerFamilyDistinct` with V7, `ContainerArtBlind`, the Proprietor checks, `TruantHasFace`, `WindfallSkinLegal`, `BedsideStandOnly`); V15g, at generation (socket binding, the role table, container caps, offer pacing, the Windfall rules including `OneClockPerRoom`, `WindfallRouteSafe`, `WindfallCatchable`, `WindfallOffGuarantees` and `WindfallBudgetInside`, `GleanNoLeak`); and V15t tests; the named checks of loot sources design 9.2 |
| V16 | Curiosities (change EV-38; M1 stub, M2, the Sequel rows M3, the Ripening rows M3-late or M4 (change EV-28; agreed, events Q1); events design 9.2; cross-document comparison 2026-11-23, CC-85) | In three parts: V16s, static over the records (option shape and prices, `PaletteWhitelist`, `StateBlind`, `PaymentFeasibility`, `RegisterByFloor`, `DreamerLine`, `HeartBedroomCap`, `PremiseTagCap`, `ReservedCues`, `ReservedSilhouette`, `RetellingParity`, `SuccessorParity`, `PoolCoverage`, `F1PoolParity`, `NameCollision` against 8.0's avoided list and others); V16g, at generation, split (change SV-20; seeds and validation design 5.2): `FixedDrawCount` at EventDraw, `SlotLegal` before V15g, the rest after V13g and V15g (`EventDrawRunBlind`, the within-run caps, `ReferentResolved`, `NoEventLoot`, `NoEventUnit`, `GraphUnchanged` and others); and V16t tests (determinism replay, `SnapshotCanonical`, LootDoorInvariant over door × memory, `DisplayMemoryInert`, `LauncherLootNeutral`, `LauncherBypass`, `ResolveIsSafePoint`); the named checks of events design 9.2 |
| V17 | BossRooms (change BR-39; M1 subset (wave corrected, change SV-20; the M1 rows are the boss rooms design 10 M1 list: (a)–(c) on Old Ragged's kit, (f) root-column Deny, the Den Guise stamp proof, `V17g.StampProven` at AP1, start zones and lanes; cross-document comparison 2026-11-23, CC-70), M2, the Gambit rows M3; boss rooms design 9.2) | In three parts, after V9, V10 and V13g: V17s, static over every boss record, Rendition, Guise and formation (kits and caps on the effective kit, `NoUntelegraphedSpike`, the floor-1 package with exactly 3 Renditions per F1 boss, size ≤ Large, carriage, the last-threshold rule, the Device contract, reserves and AddCap, `final` immunities, answer windows and `BossCourtBreakable`, facts and `FinalBossNoDreamer`, guard rows per Rendition × formation × size, cut consistency against `cutState`); V17g, at generation on each stamped arena (`V17g.StampProven` at AP1, the rest at AP2 after V15g and V16g; change SV-20) (a proven stamp, start zones, one clock and one Void, lanes, phase sockets in both states, the OneDueThing enumeration, the `BossVariantKey` draw count); and V17t, tests (determinism, mirror equivalence, event order at the arena) |
| V18 | Information (change XI-30; the tests M1, full M2; exploration and information design 9.2) | Split like V13 and V16: V18s, static (`SurfaceRegistered`, `PatternNoHueOnly`, `NoHoverOnly`, `BoardDigitIsClock`, `WallMarkProjected`, `BudgetWorstCase` per mode from the registry rows of 7.4 RC5, `CardFit` at 150%, `LayerHasLegend`, `FactHasMapDraw`, `RevealNeverUnits`, `InfoItemLaneSafe`, `InfoNeverWritesLoot`, `PointerTextBounded`, `LaneGapStock`, `WaymarkGlyphDistinct`, `NoCompletionStat` and others); V18g, at generation per floor (`SketchNeverSecretNode`, `TreasureSelectorLegal`, `TreasureFactCap`, `NoF1Pointer`); V18r, runtime assertions on every `KnowledgePass` (`MapNeverUnknown`, `ChalkNeverContents`, `ChalkNeverStub`, `RouteKnownOnly`, `ForecastKnownOnly`, `BubbleNeverPositioned`; class `runtimeAssert`; change SV-20, XI-30); and V18t tests (`LosSymmetric`, `FogEqualsRules`, `KnowledgeLeak`, `ProfileBlind`, `PointerNeverPreDoor`, `LodestringDoorBlind`, `RoleBlindOrder` and others); the named checks of exploration and information design 9.2 |
| V19 | Conformance (change SV-19; lead's call; owned by segments 11–12; M1 partial, M2; seeds and validation design 5.2) | Split like V13: V19s, static (registry completeness, label and id collisions, `DomainDisjoint`, `IdFrozen`, `DrawFreeProof`, `RerunSpanCovers`, `ClassDeclared`, the Core analyzer, `CoreNoProfileRef` and others); V19g, at every stage end and at commit (the DrawLedger, which calls the owners' draw-count rows, stage digests and the frozen `GenInputs` digest); and V19t tests (`KeyAlgebraVectors`, determinism, cross-OS and hostile-environment digests, goldens, `RetryIsolation`, `ExplorationCombatIsolation`, the shared `LootDoorInvariant` harness that V6 calls, `ForecastSurvivesRetry`, `ContentAddStability` and others); the named checks of seeds and validation design 5.2 |

### 11.3 Simulator arms

- **Fixed arrival states** (lead's call on the definition, after the
  check): authored data, one per {floor, party size} at each cap: the
  hero level (the enemies 6.3 expected level for the floor), the hero's
  starter kit by class plus the floor's expected item upgrades, and a
  canonical companion list per party size. Every ThemeParity,
  ClassThemeSpread, BossParity and FloorTimeByTheme gate reads them.
- **RouteSample**: whole runs over sampled routes; reported, not gated.
- **Choosers:** Reference (6.11), Random, Oracle; all three whenever Two
  Doors is on.
- **Both caps** (3 and 2) from the start (companions 7.4 item 3).
- **Concentrated guard posts** (10.2): an arm only.
- **Qualified names (change SV-22; lead's call; M2; seeds and
  validation design 6.2):** in the shared arm catalogue the choosers are
  `Door.Reference`, `Door.Random` and `Door.Oracle` (default Door A when
  Two Doors is off, `Door.Reference` when on); fixed arrival states and
  RouteSample are values of the `Arrival` axis (`FixedArrival`,
  `RouteSample`); the canonical companion list is the `Party` axis's
  class mix.

### 11.4 Reports owed (M2)

**Metrics (lead's call on the definitions, after the check).** Rates are
read in the simulator from the fixed arrival states of 11.3, at both
caps, unless a row says otherwise.
- **Floor-clear rate** of a theme on a floor: the share of floors on
  which the floor's boss is defeated and the hero is alive.
- **ThemeParity(floor, cap)** = the maximum over hero classes of (the
  highest minus the lowest floor-clear rate among the themes drawable on
  that floor), in percentage points.
- **ClassThemeSpread**, two measures: (i) for one hero class, the
  spread of its floor-clear rate across the themes of a pool (the
  per-class term inside ThemeParity); (ii) for one theme, the highest
  minus the lowest floor-clear rate across hero classes. Measure (ii)
  is gated for every theme, **including the fixed Heart and each final
  boss**, so a one-theme floor is not left unchecked.
- **BossParity** = the spread of the floor-5 clear rate between final
  bosses, as a maximum over hero classes.
- **Oracle gate:** the Oracle chooser's absolute run-win rate, with the
  DH 18.2 baseline (random draw, Reference party) stated beside it.
- **Door pick rate:** per theme pooled over floors and partners, and per
  (theme, floor, partner) cell (6.11 gate 2).
- **Per-build theme coverage:** themes seen per 10 runs per hero class,
  on human playtest sessions, against the random-draw expectation.
- **FloorTimeByTheme:** median floor time in simulator time (10.1), per
  theme and party size, against the median over all themes on floors
  2–5 at that party size.
- **LevelAtDoorByFloor:** Δ(N) and its steps as defined in 10.2.

**What a failing gate blocks:** a Two Doors gate keeps
`route.twoDoorsEnabled` off; the BossParity gate keeps
`route.lastHollowChoiceEnabled` off; a theme gate (ThemeParity on a
random-draw floor, ClassThemeSpread (ii), FloorTimeByTheme, readability)
holds that theme (`shipStatus = held`, 7.4) until tuning passes;
`Level10BeforeFinalBoss` blocks lever 3's form until fixed.

**Result classes (change SV-23; lead's call; M1 skeleton, M2 complete;
seeds and validation design 6.3):** every row below carries exactly one
class, which says what a miss does: the Two Doors gates 1–6 and gate 7
(BossParity) are **SW** (keep a switch off); ThemeParity on random-draw
floors, ClassThemeSpread (ii), `FloorTimeByTheme` and the readability
gates are **HOLD** (`shipStatus = held`); `Level10BeforeFinalBoss` is
**REL**; `DistinctnessCheck` is **ESC**; the floor-1 death share read
against DH 18.2 is **CAL**; the rest are REP. Every report runs at both
caps; after the M2 cap decision the gating verdicts bind at the shipped
cap and the other cap's rows are REP (a reading of companions 7.4).
**Report registry (change SV-24; M2; seeds and validation design
6.10):** `ClassThemeSpread` keeps its name here (the hazards design's
row is renamed `VoidClassDeathGap`); `FloorTimeByTheme` is registered as
the keyed family member `FloorTimeByTheme{RS}`.

| Report | Gate or report | Threshold *(illustr. unless agreed)* |
|---|---|---|
| ThemeParity (per floor and cap) | Gate on choice floors (Two Doors); **build gate on random-draw floors** (theme hold) | ≤ 5 points on choice floors **(agreed, Q2)**; ≤ 8 on random-draw floors (1, 4, and 2–3 when doors are off) |
| Door pick rate (humans) | Gate for Two Doors | No theme above 65% of its offers **(agreed, Q2)**, with the 6.11 decision rule |
| ClassThemeSpread | Gate: (i) for Two Doors on choice floors; (ii) theme hold on every theme, the Heart and each final boss included | ≤ 10 points |
| BossParity (final pair) | Gate for the Last Hollow choice | ≤ 5 points |
| Oracle chooser win rate | Gate for Two Doors | ≤ 333‰ absolute |
| Per-build theme coverage | Gate for Two Doors | Within about 20% of random-draw coverage, per class |
| `FloorTimeByTheme` | Gate (theme hold) | ≤ +10% of the floors 2–5 median at the same party size |
| `LevelAtDoorByFloor` | Report, with a stated response | Step ≤ 0.7 level |
| `ExploreSeconds`, `ExploreClicks` (layout walker, lean policy; change FL-16, floor layout design 6.10, 11.4) | Layout commitment feeding the companions 7.4 floor-time gate; the heavy and hunt figures reported | ≤ 3.5 min and ≤ 40 clicks, medians at party 4 on floors 4–5 |
| `Level10BeforeFinalBoss` | Gate whenever lever 3 applies; report otherwise | ≥ 95% of runs at minimum arrival with one bypass |
| `AntechamberCatchUpRate` per class | Report | — |
| Floor-1 death share per theme | Report against DH 18.2 | — |
| `NeckLineByRun`; terrain presence per theme (DEP-A1) | Report | — |
| DistinctnessCheck | Report | Score = shared free-4 conditions + 1 if the same `squareKind` (not `none`) + 1 if the same `signatureFamily` + 1 if the same `topologyClass` (`LayoutDistinct` feeds it; change FL-14, floor layout design 7.1), over every pair of themes that share a pool or can sit on adjacent floors; pairs scoring 3 or more go to the lead |
| Carrier-fallback rate per theme | Report | — |
| Per-class door pick rate (humans) | Report | — |
| Bring-the-key watch (purchases and hires by next theme) | Report | — |
| Picket leader fill on floor 1 | Report | — |
| Armour `verbDefencePermille`, Resistant-target share, single-type builds | Report per theme | — |
| Readability gates (7.4) | Gate (art; theme hold) | 85% roles, 75% morale reads, per theme |
| `LauncherCheckMs` (6.4 R-SEED-6; change SV-24; M2) | Report until measured | ≤ 5 ms p95 per candidate |

## 12. Staging

**(lead's call; the M2 scope, cut lines and build order agreed, Q4; the
Two Doors switch agreed, Q2)**

| Wave | Content | Structure |
|---|---|---|
| **M1** | Kennel Wood subset: Kennel Mastiff (the settled Bleeding-claw brute), Huntsman (the M1 warhammer-and-shield soldier), Poacher (the M1 sling artillery), and Old Ragged (the approved M1 boss kit) (change RT-22; cross-document comparison 2026-11-23, CC-2). Terrain: Tall and Low pieces as blockers only, per room terrain design 12 M1 (change TR-31; cross-document comparison 2026-11-23, CC-86); Old Ragged's architectural root-columns are template walls that are not terrain pieces, the Den's line-of-sight blockers (change BR-1, told plainly; boss rooms design 4.1, 5.3) | One floor. The route pre-roll code ships returning the single theme, so determinism tests start early. The M1 art and kit carry into M2 |
| **M2, first** | Build order: **the Heart and the Gut first** (they carry the companions "M2 first" floors 4–5 slice), then Kennel, Gaol, the Middle Dream themes, the Supper. The Wake only if gated in (8.2) | The floor-time slice (companions 7.4) runs on the Heart and the slowest floor-4 theme |
| **M2** | 8 themes (8.1) **(agreed, Q4)** | Strata Draw; Antechamber Catch-up; cards and dream track; Keepsakes; the RT-7 lever form (only in lever order); V1–V7; ThemeParity, ClassThemeSpread, FloorTimeByTheme, LevelAtDoorByFloor; readability gates; **Two Doors and the Last Hollow choice built, switched on only if their gates pass (agreed, Q2)** |
| **M2 prerequisites owed by other owners, before rosters freeze** | RT-3 (enemies 10.5 steps 1 and 6), RT-4, RT-6, RT-7, RT-8 (enemies 16), the RT-9 rows in enemies 17.5 and 20.1, RT-17's enemies rows (enemies owner); RT-8 (classes 7.3: classes owner; companions 7.1 and 7.11: companions owner); RT-9's Herald 7.16 sentence (Herald owner); RT-13, RT-14, RT-16 (conditions owner); RT-12 and the DH 19.3 row of RT-10 (DH owner); RT-20 (trinkets owner). **Applied by their owners:** every row listed here except RT-14 and RT-16, which stay pending rulings (cross-document comparison 2026-11-23, CC-87) | — |
| **M2 cut lines** | **Cut 1, graded (change BR-26; agreed, boss rooms Q1; M2; boss rooms design 4.10.4), in this order:** **1a** drops the Appetite **only if it is not yet built** at that review (a no-op otherwise); **1b** drops the Thing in Cell Zero, folded into the Jailer as the Rendition "Cell Zero Opens". **Cut 2:** 1a and 1b, and drop the Supper (floor 4 fixed Gut), **with the Cut 2R rider: Cut 2 never drops a built Appetite**. 1a, 1b and Cut 2 are **pre-authorised (agreed, Q4; graded, boss rooms Q1)**. **1c** drops the Master in the Frame, folded into Old Ragged as the Lord's Gallery Guise; it sits below Cut 2 and is **client-only**. The applied cuts are stamped as `cutState` into the content version (boss rooms design 9.1). Below Cut 2, the approved-scope floor (Gaol and Kennel on F1, Choir and Silk on F2–3, Gut on F4, floor 5 as a step-5 deep overlay of the floor-4 theme not drawn, re-passing its 6) needs the client | Under Cut 2, floor 4 has no choice anyway; under the approved-scope floor, random draws only |
| **M3** | Unlocks in 8.12's order; the Supper's F3–4 band and its F3 boss; a second boss for every M2 (theme, floor) cell that has only one; the third final boss; the Lore roster preview at camp; Dream Fragments plus codex (Fragments folded into Curiosity F-BOSS options; change EV-29; events design 4.9). **Pending request (change EV-37; RS owner; M3; events design 4.9):** segment 8 co-owns the codex's Curiosity pages (Retellings collected and options seen, never power or gating rule text, write-only from the run's view); the RS owner owns the codex and segment 10 hosts it. **Exploration and information (change XI-29; M3; exploration and information design 6, 7):** segment 10 hosts the codex (the RS owner owns it); the **Floor Atlas** (each floor's final map in the run record, with "Treasures found k of n" and no rooms-entered fraction, never a score), whose previous-floor page shows on the camp dream track; the death-recap map; and the Dream map skin, an option over the plain map gated by the greyscale, colour-blind and glance gates (7.4) | Two Doors (if its gates failed in M2) and at camp 3 once the floor-4 pool is 3 or more; Omens; Nightmare tiers 1–3; Perilous Door; Seams; profile-side anti-streak outside seeded runs (Launcher Fallow on the F1 Curiosity is pulled into M2: change EV-20; agreed, events Q1; 6.4 R-SEED-6; the launcher F1-boss streak rule is pulled into M2: change BR-25; agreed, boss rooms Q1; 6.4 R-SEED-6) |
| **M4** | Pools of 3–4 per tier; the Waking City alternate final; Keepsake echo types; the Inverted Cathedral with its hazard package (the drop hazard now exists; change HZ-26; hazards design 7.11, 12) | Three-door camps (optional) |
| **Later** | The Lucid Key; Other Dreamers; Nightmare tiers 4–5; daily seeds (already supported; their rules, a seed from a published or server value, and replays in the seeds and validation design 4.7; change SV-17; Later); seams that bleed the chosen door's palette | — |

**How a cut is applied (new lead's call).** The lead applies 1a, then
1b, then Cut 2 with Cut 2R (change BR-26; agreed, boss rooms Q1; boss
rooms design 4.10.4), when the M2 schedule review shows the core will
not fit, and reports it to the client; none is asked again. "Built"
(1a) means the boss's kit, arena template and rig have passed V17s at
that review (`cutState.appetiteBuilt`). Any cut below Cut 2, 1c
included, goes back to the client. A cut never removes the Heart, never leaves a
tier without its pool, and never removes `F1EscortBreakable`, the
charter or the Two Doors gates. The Heart and Gut are never cut because
they are built first.

**Floor-1 variety (read, not changed; change EV-44; agreed, events Q2;
events design 4.6, 14.2).** Floor-1 Blueprint and boss variety is
recorded as a production priority for the RS, FL and segment 9 owners;
no cut line changed then. Cut 1 as approved still dropped the Gaol's
and the Kennel's extra bosses first; the F1-boss cut order was carried
to segment 9 as a future item for the client (19.2), and any reorder is
an RS change that returns to the client. **Answered by the boss rooms design (change
BR-23, BR-25, BR-26; agreed, boss rooms Q1; M2; boss rooms design
4.10):** the cut lines above are graded; every F1 boss ships exactly 3
Renditions in M2 (a folded Rendition counts toward its survivor's 3),
the Pylorus's Renditions ship in M2, the launcher F1-boss streak rule
ships in M2 (6.4 R-SEED-6), and **the Wake's gate (8.2) is a production
priority for the RS owner**. Told plainly with Q1: the Gut is built
first, so 1a may save nothing; graded steps relieve less schedule per
cut, so Cut 2 may arrive sooner.

---

## 13. Hooks handed to later generation segments

| Segment | Hook |
|---|---|
| Floor layout | Room counts (6.6); the floor-1 **antechamber** (empty, main path, immediately before the arena, a cut vertex of the room graph; `AntechamberDominatesArena`); layout archetype per theme (cell grid, glades and trails, galleries with niches, nave with chapels, stacks and aisles, ring plaza, long hall, looping tubes, bedchamber); per-floor dressing sets; 1–3 empty or special main-path rooms. **Filled by the floor layout design (change FL-1, FL-5, FL-14, FL-22):** rooms joined by Portals, open-plan themes partitioned into ordinary rooms (FL-1); the antechamber (the Forecourt) on every floor, on the patrol Circuit on floors 2–5 (FL-5); the layout archetype is the theme's `layoutGrammar` (FL-14); layout supplies sockets and minimum counts, and the named segments below keep their rules (FL-22) |
| Room shapes and terrain | `terrainSquares` and density per floor; one home theme per square kind; water and oil placement caps (8.6, 8.8); cover props per theme (the weapons 9.5 decision); wall and edge density for the Wrecker; at least 3 Acrobatics-gated choices per floor (armour 7.3); this segment keeps the ≥ 3 total, of which layout supplies 2 Squeezes (change FL-22). **Filled by the room terrain design (change TR-1; room terrain design 13):** segment 3 owns the room-terrain system (piece and square classes, layers and sockets, the Clearway, the cover rule, Furnishings, Kegs and their verbs, Rough and Plank, arena terrain rules and caps, the Remnant system at M3) and the knock-into system (contact hazards and harm squares: rules, bands, caps and in-room placement); cover props per theme become the theme's `terrainKit` (change TR-22) |
| Hazards | The bile square (RT-13); the drop hazard needed by the Inverted Cathedral and staked pits; Teetering counting (RT-14). **Ownership (change TR-1; room terrain design 13):** segment 4 owns per-theme hazard catalogues and floor-scale density built from segment 3's classes inside its caps, drops, bile, Teetering counting and vents; segment 5 owns traps. **Filled by the hazards design (change HZ-1, a reading of this hook; hazards design 6.1, 13):** segment 4 owns hazard points, roles and quotas, VP1–VP10, Voids and falls (the drop hazard), bile density, Teetering counting (as a request, HZ-6), vents, Stirs, Cords, Tides, Lurch and the Dream step, Landmark ticks (not hazards), Moods, Dare rooms and Plunge, per-theme catalogues and Tableaux (`hazardKit`); a hazard is visible from reveal and never disarmed; boss counterplay features stay the boss rooms' (segment 9), which take only the Stir and Cord machinery; the staked pits are re-read as a walkable harm square at Nightmare tier 2+, not a drop hazard (change HZ-25, told plainly; hazards design 6.19) |
| Traps and locks | `trapFlavours` per theme (Shade DEP-S1 records with `mechanismSquare`); trap conditions drawn from the theme's 6 or damage-only (RT-16); no trap hard control on F1, set pieces only on F2; the Gaol keyring and cell caches; sphincter doors; closable cell doors. Keeps the door, lock and secret rules, including the party door verbs (open in M1, close in M2; change FL-8) and Hairline secrets with Sound the wall (change FL-17); layout supplies the slots and minimum counts (change FL-22; floor layout design 9.2, 13). **The line with hazards (change HZ-1; hazards design 6.1):** traps are hidden until detected, or known armed devices, and are sprung; the sprung twins of hazard Stirs stay traps (the toppling-shelf tripwire, the dumbwaiter drop, the sluice one-shot Soak, the icicle chandelier, the acid geyser, trapdoor stages, the bell tripwire, the portcullis, the Appeals trapdoor, the Kennel horn tripwires); an open trapdoor or shaft is a Void (a hazard), a closed one that springs is a trap; Cords never arm traps; known armed traps stay hazards for conditions 10.1–10.4 (conditions 11.3) without moving ownership. **Filled by the traps and locks design (change TL-10, a reading of this agreed hook; M2; traps and locks design 6.3, 11.1, 13):** segment 5 owns the check charter, the DC ladder and the `checks` stream, trap records and rules (the sprung twins included), detection, locks, keys and the Wax Key, the door verbs, Hairline variants, boxed lurkers, Wager rooms and V11; traps come in three families (Device, Snare, Catch); device bodies and mechanisms are always visible and always rigged; every trap carries a diegetic tell (RC1); `trapFlavours` hold TrapRecords, a superset of the DEP-S1 record; trap conditions still wait on RT-16 (pending request, TL-39); the sprung twins keep their traps-catalogue names (the acid geyser is the Acid Gland; the Kennel horn tripwire is re-told as the tin-can tripwire, a pending request, TL-43) |
| Encounter, loot and event placement | Each theme's `placementRules[]` (8.0); lesson axes per floor-1 theme; the Pack one-family rule; Scavengers at the F1 patrol budget; the Wake's dormant bias kept away from rooms 0–2; the Heart's morale shape by room and psychic cap; the signature type at least twice (a generation assertion, V7); Corroded caps; Frozen source caps; Keepsake slot dressing (9.3); loot stays theme-blind (LootDoorInvariant). Keeps the Wake dormant rule, read from layout's `nearStart` tag, and Keepsake dressing (change FL-22). **Filled by the encounter placement design (segment 6; encounter placement design 6.4, 6.16, 13):** segment 6 owns `placementRules[]` application (the fearless-anchor tags, EP-29; the Gut acid overlay count, EP-31) and ENM 10.5 steps 2 and 4–11; loot and events keep their segments. **Loot placement taken up by the loot sources design (segment 7c, approved 2026-11-22; its 4, 5, 11):** sources, sockets and per-theme container art; loot stays theme-blind (Proprietor tags are display only); the charter entries of changes LP-7, LP-15 and LP-24 (7.4, 9.3). **Event placement filled by the events design (segment 8, approved 2026-11-22; its 4.2, 4.10, 4.13, 11):** segment 8 owns Curiosity content and forms (Curiosity, Interlude, Glimpse), their hosts (6.6; floor-1 Alcove set pieces and side rooms, Q rooms, Forecourts, `curiosityHost` signature rooms; never arenas), each theme's `curiosityKit` (8.0) and the shared pools, the cost and gain palette, Strands, Retellings, the Twice-Told card, Launcher Fallow (R-SEED-6), Sequels, Ripening, the Afterword and V16; EventDraw runs after layout and EventPlace after LootBind, draw-free, so loot stays theme-blind and never reads Curiosities (LootDoorInvariant) |
| Boss rooms | One counterplay feature per boss (pendulum, the pen bar (renamed from "kennel gate": a two-state piece on the Whelping Pen, not a Portal, so TR-22's kennel gates are untouched; change BR-16; boss rooms design 4.7.1), sluice and bell rope, the Menagerie Wagon (wagon bolt; renamed from "cage", change BR-16; cross-document comparison 2026-11-23, CC-69), dunk tank, chandelier of icicles, the Pylorus mouth, the bed); Keepsake dressing; phase option (e) never on party squares. Arena terrain caps are segment 3's; (e) uses pre-placed phase sockets of existing kinds only, announced a round ahead and proven in both states; the counterplay identity is unchanged (change TR-16, told plainly; room terrain design 6.11). **Traps (change TL-40; with segment 9; traps and locks design 7.8, 13):** arena devices such as the chandelier of icicles are segment-9 instances in the TrapRecord format; no boss room has guaranteed traps. **Filled by the boss rooms design (segment 9, approved 2026-11-22; change BR-16, BR-23 to BR-26, BR-30; its 4, 5, 11):** the list is extended to every boss as the Device contract (one class-blind Device per boss, an `arenaDevice` TrapRecord instance that is not a trap, with a ranged trigger; F1 Devices never harm; F5 adds the Heavy Thing, 9.3) (change BR-16; M1 for the Den, M2); segment 9 owns arenas and Guises, Rhythm, phases, reserves, Renditions and `BossVariantKey`, folded bosses for the graded cuts (12), the Playbill and V17 `BossRooms`; the RS owner signs off Rendition phase and ability alternates that vary 8.x text (change BR-23) |
| Exploration and information | Camp card, Two Doors cards, Stratum Card, dream track, Lore reveal, road not taken; Dream Fragments (M3); the Lucid Key secret (Later). Keeps the Stratum Card, including the floor count line (change FL-20), the Listen and Hairline displays (FL-18, FL-17), and the Lucid Key, whose Site layout reserves (change FL-22; floor layout design 9.2, 13). Dream Fragments are folded into Curiosity F-BOSS options (change EV-29; M3; events design 4.9); the Stratum Card carries no Curiosity line in M2 (events design 4.12). **Filled by the exploration and information design (segment 10, approved 2026-11-23; change XI-15, XI-17, XI-19, XI-20, XI-23, XI-24, XI-29; its 4.9, 5, 6, 11):** segment 10 places the camp card, door cards, Stratum Card, dream track and Lore reveal in its regions and card queue (6.9), adds its registry rows and gates to the charter (7.4), hosts the codex, and keeps the next-floor card unchanged, with no pointer result on camp or door cards |
| Seeds and validation | The `route` stream and R-SEED-1 to 6; `drawableSet` and the reproducibility key; V1–V7; the signature-type placement assertion; the RouteSample, fixed-arrival and chooser arms; LootDoorInvariant; the reports in 10 and 11. **Events (change EV-21, EV-38; events design 4.10, 4.13, 9):** the `curiosities` stream and its draw map, the EventDraw and EventPlace order, the resolve safe point, `curiosityMemory`, the launcher order against R-SEED-6, and V16. **Filled by the seeds and validation design (segments 11–12, approved 2026-11-23; change SV-1, SV-2, SV-4, SV-8, SV-10, SV-12, SV-19, SV-20, SV-22; its 4.3, 4.4 GS0.2, 4.6, 5.2, 5.7, 6.2, 11):** segments 11–12 own the key algebra, the stream registry (`dungeon/route` by keyed sorts, retry scope "never"), the pipeline and its two assertion points AP1 and AP2, the LR0–LR5 retry ladder, the reproducibility key (with `genVersion`), saves, shared, daily and replay rules, golden seeds and the release set, V19 `Conformance` and the report and arm registries; `drawableSet` is computed at GS0.2 and stored in the run save; V1–V7 keep their RS owner and phases (5.2); the signature-type assertion moves to `V12g` with a V12t sample row (pending EP; fallback V19t); the arms are qualified (11.3); LootDoorInvariant is one shared V19t harness that V6 calls; the `curiosities` stream is attempt-invariant (change SV-4). Owners keep every stream's consumers and counts, every check's logic and every report's threshold |

---

## 14. Binding requirements and how they are met

| Requirement | Source | How it is met / owner |
|---|---|---|
| Theme selects tiles, enemies, hazards; one per floor | main 4.6 **(decided)** | 6.2–6.3 |
| Seed determinism; separate streams | main 2, 6.3 | 6.4 (RT-3); the reproducibility key (seed, `genVersion`, manifest, configuration, unlock; change SV-12) (cross-document comparison 2026-11-23, CC-87); V6 |
| Theme rules (roster, 6, water rule, at least half non-fearless, …) | enemies 10.6; conditions 12.2, 16 | 8.1–8.11 checks; V1. Pending request: RT-14 ruled "does not count" before M2 rosters freeze (change TR-14; room terrain design 6.5.5) |
| Tag-immunity budget (2 per non-boss trait set, at most 1 control; mindless grants `mind`) | conditions 12.2; enemies 8.3; stat model 8.4 | 8.10 (the Bile Ooze); V1 |
| Role taxes and per-type condition cap (at most 2) | enemies 9.1, 9.2, 17.1 | Counted in every theme (the Kennel Mastiff stays the settled M1 brute); V1 |
| Boss signatures (1–2 per boss) | enemies 17.5, 20.1 | Named for every boss (8.3–8.11); V2 |
| A boss per eligible floor; a compliant F1 boss | enemies 10.5, 17.5, 20.1; HR-21 **(agreed)** | 15 bosses over 11 cells; V2 |
| Theme × floor content test | enemies 10.5 | V4 over 11 cells |
| Floor plan and guarantees | enemies 10.4 | 6.6; kept on trimmed floors; a skittish type on every floor-4 and floor-5 theme for Scavengers |
| Experience check after a bypass | enemies 16, 20.1 | RT-7 (floor 5 counts only experience earned before the final boss) |
| Floor-1 protections | DH 17.1; enemies 10.3, 11.3; CL-10 | 8.2 |
| `Floor1ClassSpread`, `SeedHasTarget` | classes 5.6 | 8.2 (RT-9) |
| Level 3 at the F1 boss door | Shade 18; Arcanist lever 5 | 9.4 (RT-8); `AntechamberDominatesArena` |
| Lever 3 on floors 4–5 | companions 7.4 | RT-7 |
| Floor-time gate at both caps | companions 7.4 **(agreed)** | 10.1; `FloorTimeByTheme` |
| Next-floor line (HR-23) | enemies 10.6; Herald 5.7 | 6.9 |
| Floor-entry card | DH 17.12 | Stratum Card (RT-12) |
| HE-7 containers; generation retry | DH 12.4 | Seeds and placement segments; the route is unaffected by retries (the LR0–LR5 ladder, with the release clean end, agreed Q2; seeds and validation design 5.5) |
| Death share by floor; win rate | DH 18.2 **(agreed)** | Per theme; Reference chooser when Two Doors is on (RT-10) |
| Loot theme-blind; the Resistant filter | enemies 15.3; trinkets R-23; armour 14.3 | 6.4 LootDoorInvariant |
| Carrier legality inside the 6 | enemies 15 | Carrier-fallback report per theme |
| Rare and very rare by floor; trader F2–4 | items 12.5, 10.9 | Unchanged |
| Hazard edges per floor | trinkets 23 | Floors 2–5; floor 1 exempt (RT-20, **told plainly**); the floor-1 stove and candle rack are typed Low pieces, not hazards (change TR-29, told plainly; room terrain design 6.5.6); floor 1 gains hazard edges at Nightmare tier 2+ only (change HZ-25; hazards design 6.19) |
| Neck-line trigger rates by theme | trinkets 18.3, 20 | 10.7 |
| Terrain presence | Arcanist DEP-A1 | Report; 10.7 |
| Wrecker hazard and wall density | Vanguard 8.3, 18 | Hook; per-theme report |
| Trap records; `TrapwrightHasMechanism` | Shade DEP-S1 | Traps segment; `trapFlavours` |
| Cover decision | weapons 9.5, 26 | Cover props per theme (now `terrainKit`, change TR-22); decided by the room terrain design 6.1 (change TR-2) |
| Acrobatics choices | armour 7.3 | Terrain segment; stacks and tables help |
| `verbDefencePermille` per theme | armour 8.1 | Report; the Gut first |
| Herald trap-room and hearth reports | Herald 5.4, 5.7 | Per theme; RP3 |
| Resistant-target share | Arcanist 9.4 | Per theme |
| Single-type builds "slowed, not stopped" | stat model 11 | Per theme, with Soaked modelled |
| Pledge rooms F2–4 (Later) | companions 7.12 | Slot reserved; placement segment |
| Stairs, left-behind warning, Scrap | items 24 | Layout segment: the Stairhead behind the arena, through a Bossgate that opens on `BossDefeated`; none on floor 5 (change FL-6; floor layout design 6.11) |
| Non-combat content on most floors | stat model 10 | Placement segment; Dream Fragments M3 |
| Arcana-trained non-caster in recruit pools | consumables DEP-1 | Unaffected; the Muster stays theme-blind |
| Bring-the-key anti-goal | stat model 2; items 2; trinkets 2 | Class-blind cards; vendor pre-rolled; bring-the-key watch report |
| No protected content | main 2 **(decided)** | Original names; the frozen-names check (V7) |
| 1–2 hour runs | main 2 **(decided)** | 6.7; levers 1–2 mandatory |

---

## 15. Risks

| # | Risk | Signal | Response |
|---|---|---|---|
| 1 | The floor-time gate fails at cap 3 | `FloorTimeByPartySize` over 20 min after the levers | Levers 1–2 mandatory; RT-7; a morale-forward Heart; party-3 reference tuning; the cap-2 fallback, pre-authorised (companions Q2) |
| 2 | Content cost about 2.8× approved; design, test and simulator time | M2 schedule review | Cut 1 and Cut 2, pre-authorised (Q4); Heart and Gut first; 62k-run passes budgeted from fixed arrival states |
| 3 | Deep staleness (Gut and Heart replayed after losses) | Playtest notes; coverage report | The Supper floor-4-only for a true 50/50; 2 Gut bosses; room-by-room Heart escalation; Keepsakes; deep breadth first in the M3 unlock order |
| 4 | A dice tax under a random draw | ThemeParity over 8 points | A build gate: the theme is held until signature or roster tuning passes (11.4); themes differ in kind; ClassThemeSpread; Two Doors when its gates pass |
| 5 | Two Doors rewards avoidance or has a dominant door | Pick rate over 65%; ThemeParity over 5; Oracle over 1/3; coverage under the band | The switch stays off (Q2); milestones; road not taken; the coverage gate |
| 6 | Readability under surreal art | Readability gates fail | Charter RC1–RC9 and M2 gates (Q3) |
| 7 | Tone drift with a large art team | Tone-by-depth review | Tone Sheets; bark-timing test (V7); watch-list |
| 8 | Themes blur (shared tax conditions, reused squares) | DistinctnessCheck pairs at 3 or more | `signatureFeature`; one home theme per square kind; RP2 |
| 9 | Pending rulings (Teetering, radiant, traps) | RT-14 to RT-16 refused or late | Fallback drops per theme; radiant blocks only M3 |
| 10 | Class-polar matchups (Shade vs Mirror Court, Vanguard vs Gut, fire Arcanist vs Choir, Herald vs low bands, **Arcanist and Herald vs the Heart**, on every route) | ClassThemeSpread over 10 | Mirror Court in M3 with radiant limits; Corroded caps; water placement caps; the Heart's psychic cap and ClassThemeSpread (ii) gated on the Heart and each final boss; RP3 |
| 11 | Water and oil spikes | Round-1 downs by theme | The 6.4 guards computed with the party Soaked or Oiled; entry-square rules |
| 12 | Unlock manifests complicate shared seeds | V6 fails across manifests or unlock states | Whole-catalogue permutation; `drawableSet`; the (seed, `genVersion`, manifest, configuration, unlock) key (change SV-12); the canonical unlock set for shared and daily seeds |
| 13 | Name drift between documents | Naming pass | Frozen names (8.0); one canonical act set |
| 14 | The premise hardens into a story by accretion | Writing review | Uncommitted questions stay open (7.5); no premise twists in boss concepts; Curiosities carry premise tags, the `selfReference` cap, the premise test and a tone review on every Sequel chain (change EV-25; events design 4.5.6, 4.11) |
| 15 | **All evidence is toy** | — | Every number is owed a simulator rerun before it settles |
| 16 | **Recall-based survey** | — | Mechanisms only; no names or text taken |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: room counts, minute targets, report
  thresholds other than the agreed ones, the RP6 600‰ cap, the pick-rate
  sample and confidence rule, the coverage band, the role-ID screenshot
  count, the streak redraw count, the caps on acid units and psychic
  at-will types, the `Level10BeforeFinalBoss` share, and every dice, HP
  and damage value of every type and boss within the enemies bands.
- Theme rosters' exact kits within the budgets and the frozen roles,
  profiles, conditions and damage types of 8.3–8.11.
- Layout of the camp card, door cards, Stratum Card and dream track.
- Art, audio and writing inside the charter and the Tone Sheets,
  including final palettes against the damage-hue key (RC4).
- Wording of signature lines and barks within RC8 and RC9, until the
  naming pass.
- **Not tuning:** five floors **(agreed, Q1)**; one theme per floor; the
  tier pools and no repeats **(agreed, Q1)**; the run-start route draw
  and its fixed-count order; Two Doors on only if its gates pass, with
  the agreed thresholds **(agreed, Q2)**; the charter RC1–RC9 **(agreed,
  Q3)**; the 8-theme core and the cut order **(agreed, Q4)**; the 750‰
  floor-1 default and `F1EscortBreakable`; the lever order; loot and the
  Muster theme-blind.

---

## 17. Changes to approved documents

Applied on 2026-11-21, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and rows owed by
other owners are listed again in 18 and recorded as such. Every row
names the exact section it touches. Rows that touch **decided** or
**agreed** text are flagged in the "Flag" column; **the client's
approval of 2026-11-21 covers them** (with Q1–Q4 where a row names one).
Told-plainly rows were told with the questions or after the check
(19.1) and confirmed by the client on 2026-11-21. The rulings requested
from other owners (RT-14, RT-15, RT-16) are recorded as requests and
change nothing until those owners rule. Conditional rows are recorded
now and apply only when their condition is met: RT-11 and the door parts
of RT-10 only when the switches go on (6.11). RT-13 (bile) was accepted
by the client with this document on 2026-11-21; the conditions owner
applies it. RT-18, RT-19 and RT-21 are recorded as M3 rows and apply at
M3. RT-22 (added 2026-11-23, cross-document comparison 2026-11-23,
CC-2) is an M1 row.

**Narrowings recorded inside this design (no approved text changes):**
- the floor-1 pool's 750‰ default sits inside the settled 50–100%
  bands (classes 5.6);
- the Long Supper's floor-4-only window in M2 sits inside enemies 10.5's
  "theme × floor in its range";
- no M2 theme deals radiant;
- the Lore roster preview at camp waits for M3.

| # | Document / section (current status) | Change | Flag | Approved by |
|---|---|---|---|---|
| **RT-1** | Main 4.1 bullet 1, "A run is 4–5 floors." (**decided**); main 9 M2 row "4–5 floors" (**decided**); ability model 3 "Levels 1–10 over 4–5 floors"; classes 3.1 constraint table row "4–5 floors"; armour 5.1 item 1 "across 4–5 floors"; companions 3.1 main 4.1 row, 7.1 table row "Camp 4 (5-floor runs)" and the stranded-slot bullet's "on a 4-floor run" sentence and forecast text, 7.6 swap forecast "on a 4-floor run", 18.1 stranded-slot bullet | "4–5 floors" → "5 floors"; "(5-floor runs)" deleted; the 4-floor stranded-slot sentence and forecast lines deleted (with 5 floors the third slot is always reachable through camp 4) | **Decided** (main 4.1, 9) | **Client (agreed 2026-11-21, Q1)** |
| **RT-2** | Main 4.6 bullet "Each floor has a theme that selects its tiles, enemies and hazards." (**decided**); enemies 10.6 | Add: "Themes are drawn per floor from tiered pools (floor 1; floors 2–3 band; floor 4), or picked from two drawn candidates when route choice is on (RT-10), with no repeat in a run; floor 5 is always the fixed final theme with a final-boss pool (run structure design 6.3–6.5)". Enemies 10.6 gains `tier`, `eligibleFloors` (at most 2 adjacent), `pairKey`, `final`, `shipStatus` and pool rules RP1–RP6 | **Decided** (main 4.6) | **Client (agreed 2026-11-21, Q1)** |
| **RT-3** | Main 6.3 bullet "Separate random streams ..." (**decided**, as extended by CMP-3); enemies 10.5 steps 1 and 6 and its content-test text | Add a `route` child of the dungeon stream, consumed only at run start; per-floor dungeon substreams keyed by floor index; the boss draw of 10.5 step 6 moves to the route pre-roll (the boss is an input, like the theme) | **Decided** (main 6.3) **(new lead's call: flagged)** | **Client (agreed 2026-11-21, Q1)**; enemies owner applies 10.5 |
| RT-4 | Enemies 10.6 (record fields); 17.2 (Volatile reads `themeDamage` = the `primaryDamageType`; Hardened reads `hardenedLayer`, whose breaker is the `counterType`; Chilling reads `frost`); 17.5 ("the theme's counter type" defined as `counterType`; boss records gain `themes[]`, `final` and `shipStatus`) | The theme record of 8.0, `placementRules[]` and the Keepsake fields included | — | Lead; enemies owner applies |
| **RT-5** | Enemies 21 M2 bullet "themes (4 themes plus a deep overlay on floor 5)"; 21 "Content volume" paragraph ("4 themes of 5–6 types (about 22 types) plus a deep overlay"; lead's call there); 22 risk "M2 content volume" ("The deep overlay is the lever") | M2 = 8 themes of 5–7 types (56 types), 15 bosses, with Cut 1 and Cut 2 as the lever; the floor-5 deep overlay retires (returns as Nightmare tier 3, M3) | Replaces a lead's call the client saw | **Client (agreed 2026-11-21, Q4)** |
| RT-6 | Enemies 8.2 step 5; 8.3 content tests | Step 5 **stays in M2**; each theme lists `legalOverlays` that keep its 6. Restated, no rule change; the 17.2 affix cut stands | — | Enemies owner |
| RT-7 | Enemies 10.4 slot table (floors 4–5: guard posts 2 when lever 3 applies, read from `pacing.trimmedFloors`); enemies 16 experience list (floor-4 boss 300 and floor-5 lair 240 on trimmed floors; the 10.1 budget bands of 200 and 140 unchanged) and its floor pool check table; 20.1 pool check (on floor 5, only experience earned before the final boss counts); companions 7.4 lever 3 wording | The concrete lever-3 form, applied only in the lever order of companions 7.4 | Touches lead's-call lever text next to agreed levers 5–6 (companions Q2) | Enemies owner; **client told plainly** (the floor-5 form told again after the check, 19.1) |
| RT-8 | Enemies 16 "Camp catch-up" bullet and its target ("under 20% of runs" → "under 20% of runs at camps 2–4"); classes 7.3 subclass timing ("at the floor-1 antechamber, before the boss"); companions 7.1 camp-1 row; companions 7.11 camp-1 catch-up paragraph ("A hero who reaches camp 1 at level 2 is raised to 3 at step 2 …" → "the hero already reached level 3 at the floor-1 antechamber") | Antechamber Catch-up (9.4) | — | Lead; classes, Shade and Arcanist owners told; **client told plainly** |
| **RT-9** | Enemies 17.5 floor-1 boss bullet ("Escort and reserve groups are not constrained", **agreed**, Herald Q3); enemies 20.1 floor-1 boss rule; Herald 7.16 content-rule bullet "escort and reserve groups are not constrained by it"; classes 5.6 | Add: "except that a theme below 75% non-fearless joins the floor-1 pool only with `F1EscortBreakable` (every escort and reserve group of its floor-1 bosses drawn from non-fearless types) and a passing reference-arm `Floor1ClassSpread` in its split"; the floor-1 pool's 750‰ default | **Agreed** (HR-21, Herald Q3) | **Client, with this document (approved 2026-11-21); told plainly and confirmed** |
| **RT-10** | Main 4.1 camp bullet "Between floors the party reaches a camp: full rest, a shop or reward, and a **Muster** ..." (**decided**, as amended by CMP-1); enemies 10.6 next-floor line (HR-23: one line per door when Two Doors is on); companions 7.11 camp order (new step 6a; step 11 Travel on commits the door); DH 18.2 "competent player" reading and **DH 19.3** (the Reference chooser inside the reference party policy when Two Doors is on); stat model 7.1 (Lore roster preview at camp, M3) | Add to main 4.1: "Each camp shows the next floor's theme; when route choice is on, camps 1–2 offer a choice of two next floors (run structure design 6.8–6.11)". The camp card, door cards and their place in the camp order | **Decided** (main 4.1 camp bullet) | **Client (agreed 2026-11-21, Q2)**; the door parts apply only when `route.twoDoorsEnabled` is on |
| **RT-11** | Main 4.1 camp bullet (**decided**); main 4.6 boss bullet "Which boss appears on a floor is drawn from a pool." (**decided**); enemies 17.5 (a final-boss pool); companions 7.11 camp 4 | The Last Hollow choice at camp 4; main 4.6 reads "... drawn from a pool; the final boss may be picked at camp 4 when the Last Hollow choice is on" | **Decided** (main 4.1, 4.6) | **Client (agreed 2026-11-21, Q2)**; applies only when `route.lastHollowChoiceEnabled` is on |
| RT-12 | DH 17.12 floor-entry card | The Stratum Card header (6.9) above the guaranteed counts | — | Lead; DH owner applies |
| **RT-13** | Conditions 11.2, 11.5 (the M2 square set "water, web, fire and oil-slick squares", **agreed**) | The bile square (Corroded on entry) added to the M2 square set; no new condition id | **Agreed** (conditions 11.5) | **Client, with this document (approved 2026-11-21); conditions owner applies**; told plainly and confirmed (19.1) |
| RT-14 | Conditions 16 | Ruling requested: "conditions granted by the universal hazard-edge and landing rules (10.2, 10.3) do not count toward a theme's 6". Fallback drops listed per theme | — | Conditions owner (needed before M2 rosters freeze) |
| RT-15 | Enemies 9.2; conditions 16; stat model 8.1 | Ruling requested: whether radiant counts 2, and whether Revealed is "harmful" at all. Blocks the M3 Mirror Court and Moth Wedding only | — | Conditions owner |
| RT-16 | Conditions 11.6, 16 | Ruling requested: trap conditions come from the theme's 6, or the trap is damage-only | — | Conditions owner |
| RT-17 | Enemies 8.1 (role prop grammar, extending AR-23), 5.6 UI test, 20.1, 20.3 | Charter gates (7.4); V1–V7; ThemeParity; ClassThemeSpread; FloorTimeByTheme; LevelAtDoorByFloor; the reports of 11.4 | — | Lead; **client (agreed 2026-11-21, Q3)** for the charter |
| **RT-18** | Main 4.7 "Content unlocks lead" (**decided**) | Themes and final bosses join the unlockable content (M3), never power | **Decided** | **Client (M3)** |
| **RT-19** | Main 9 M3 row (**decided**) | M3 also carries in-run content: Two Doors at camp 3, Omens, Seams, Nightmare tiers | **Decided** | **Client, told with this document (approved 2026-11-21)**; applies at M3 |
| RT-20 | Trinkets 23 | The hazard-edge ask applies from floor 2 (floor 1 is dry by design) | — | Trinkets owner; **told plainly** |
| **RT-21** | Main 4.7 "Difficulty ladder" (**decided**); enemies 10.1 ladder% ("never raises caps or raw stats" kept) | Nightmare tiers add kind, not stats; the Perilous Door makes the ladder per-floor | **Decided** | **Client (M3)** |
| RT-22 | This design 8.4 Kennel roster, damage line and M1 subset, 8.1 at-will cell, 12 M1 row; item rolls 5 Kennel row (base families); encounter placement 7.3 K1 (re-check that the Huntsman's level after the −1 shift lies in armour 12.3's legal L1–4, else raise the K1 room or drop the shift); armour 13.6 (types named); weapons unchanged | The M1 Huntsman is re-dressed onto the weapons M1 warhammer-and-shield soldier (a hunting maul and boar-board shield, Push and Interpose, plus Shove) and the Poacher onto the M1 sling artillery (a hunting sling, Push), so both teach their verb against a solo hero (weapons 15.3, 15.4; armour 12.3, 13.6, 10.5); "boar spear" only on a promoted or F2+ pike variant, never at room level 1 (weapons 15.1) (cross-document comparison 2026-11-23, CC-2) | — | Lead's call; RS owner applies 8.4 and 12; item rolls, encounter placement and armour owners apply their rows; M1; added 2026-11-23 |

---

## 18. Depends on systems not yet designed

The owed rows are the single list in 17; by owner (rows marked applied:
cross-document comparison 2026-11-23, CC-87):

- **The enemies owner:** RT-3 (10.5 steps 1 and 6), RT-4, RT-6, RT-7,
  RT-8's enemies 16 row, the RT-9 rows in 17.5 and 20.1, RT-11's enemies
  17.5 row, RT-17's enemies rows (all applied); the 56 type records and
  15 boss records within the budgets.
- **The conditions owner:** RT-13 (bile; applied), RT-14 (Teetering),
  RT-15 (radiant, M3), RT-16 (traps); before M2 rosters freeze.
- **The DH owner:** RT-12; the DH 18.2 and 19.3 rows of RT-10 (the
  Reference chooser) (applied).
- **The companions owner:** RT-1's companions rows; RT-7's lever wording;
  RT-8's 7.1 and 7.11 rows; RT-10's step 6a; RT-11's camp-4 row
  (applied).
- **The classes, Shade and Arcanist owners:** RT-8 (subclass at the
  antechamber; applied); the reruns of 9.4.
- **The Herald owner:** RT-9's 7.16 sentence (applied); the lever list
  of 7.16 for a 50–74% floor-1 theme.
- **The trinkets owner:** RT-20 (applied); the neck-line coverage of
  10.7.
- **The item rolls, encounter placement and armour owners:** RT-22's
  rows (item rolls 5 Kennel row; encounter placement 7.3 K1; armour
  13.6) (cross-document comparison 2026-11-23, CC-2).
- **Dungeon generation segments 2–12:** every hook in 13.
- **The camp and hub design:** the card layouts inside the camp order of
  6.8.
- **The simulator:** the arms of 11.3 and the reports of 11.4 at both
  caps; the route enumerator.
- **Playtests:** the human door pick rate (6.11, gate 2); the
  readability role-ID playtest (7.4); the floor-time slice on the Heart
  and the slowest floor-4 theme.
- **The profile (M3):** milestone marks to unlocks; unlockable strata;
  Nightmare tiers; the Perilous Door.
- **The art and audio teams:** Tone Sheets and the readability gates.
- **Naming pass:** every placeholder name here.

---

## 19. Open items for the client

None. Approved by the client on 2026-11-21.

The client approved this document as a whole on 2026-11-21, including
every lead's call, the told-plainly items (including "Told plainly after
the check") and the new lead's calls in 19.1. The four questions were
answered on 2026-11-21 **(agreed)**. The future items in 19.2, which
come back only if a report or a later design triggers them, and the
naming pass remain future items.

### 19.1 This document

**The four answers (agreed, 2026-11-21), each the recommended option:**

- **Q1, run shape and theme selection:** option A. Fixed 5-floor runs
  (RT-1 changes the decided "4–5 floors"); one theme per floor; tiered
  random pools: floor 1 draws from 2, floors 2–3 share a band pool of 3,
  floor 4 draws from 2, floor 5 is always the Dreamer's Heart with a
  final-boss pool; no theme repeats in a run; the whole route drawn at
  run start from the seed (the `route` child stream, a fixed-count,
  whole-catalogue permutation). Rejected: B, the literal A/B per floor
  (16 sequences); C, variable 4–5 floors.
- **Q2, route choice:** option A. Two Doors (the next floor picked from
  two pre-drawn candidates at camps 1 and 2, plus the Last Hollow
  final-boss pick at camp 4) is built in M2 but switched on only if the
  gates pass (ThemeParity of 5 points or less on choice floors, and no
  theme above a 65% pick rate); otherwise the random draw ships and the
  doors arrive in M3, with no structural change and no seed change.
  Calibration on a Reference chooser policy. It amends the decided main
  4.1 camp bullet (RT-10, flagged). Rejected: B, random only until M3; C,
  doors regardless; D, never.
- **Q3, tone:** option A. "Grim Surreal: a dream that obeys rules",
  provisionally, with the binding readability charter C1–C9 (now
  numbered RC1–RC9) and its M2
  gates, a Tone Sheet per theme, and the uncommitted Dreamer premise
  (open: who the Dreamer is, motives, endings). Rejected: B, no premise;
  C, folk horror; D, full absurdism.
- **Q4, M2 content:** option A. The full M2 core of 8 themes (Gaol and
  Kennel Wood on F1; Drowned Choir, Silk Archive and Carnival on F2–3;
  Long Supper and Gut on F4; the Dreamer's Heart on F5), the Endless Wake
  added if it passes its floor-1 test; Cut 1 and Cut 2 pre-authorised as
  schedule fallbacks; build order Heart and Gut first. Rejected: B, plan
  Cut 1; C, plan Cut 2; D, the approved scope.

**Told plainly with the questions; confirmed by the client on
2026-11-21:**
- the companions toy predicts the floor-time gate fails at a cap of 3,
  so the cap-2 fallback is likely; levers 1–2 are mandatory; theme
  choice cannot fix the clock (10.1);
- lever 3 takes the RT-7 form (2 guard posts, boss experience 300),
  applied only in the companions 7.4 lever order (the floor-5 form was
  revised after the check, below);
- the Antechamber Catch-up fires in nearly every bypass run: in effect,
  the first boss always meets a level-3 hero (9.4);
- the 50–74% `Floor1ClassSpread` split has no floor-1 data in M2 unless
  the Wake passes; `F1EscortBreakable` narrows agreed HR-21 text (RT-9);
- the Gut appears on floor 4 in half of runs; floor 5 differs only by
  boss and Keepsakes until M3; Emberward is dead in a third of runs and
  Graveward in half; floor 4 is dry when the Supper is drawn (10.7);
- the trinkets hazard-edge ask starts on floor 2 (RT-20);
- route choice raises win rates by 2–4 points, so calibration runs on
  the Reference chooser (10.6);
- the M2 core is about 2.8× the approved content cost (10.4).

**Told plainly after the check** (fixes the lead applied after the
answers; none changes a Q1–Q4 answer); confirmed by the client on
2026-11-21, with this document:
- **lever 3 on floor 5:** the final boss's experience is paid when the
  run ends, so on floor 5 the trimmed 100 moves to the lair (240)
  instead of the boss; floor 4 keeps boss 300. `Level10BeforeFinalBoss`
  becomes a gate whenever lever 3 applies (6.6, 10.2, RT-7);
- **bile squares touch agreed text:** the M2 square set of conditions
  11.5 is agreed, so RT-13 is flagged and covered by the client's
  approval of this document on 2026-11-21 (17);
- **ThemeParity is a build gate under the random draw too** (8 points),
  so a matchup gap cannot ship as a dice tax; a theme that fails is held
  until tuning passes, and a hold that matches no pre-authorised cut
  comes back to the client (7.4, 11.4);
- **Two Doors has two more gates:** the per-class coverage band and a
  confidence rule on the 65% pick rate, which needs about 150 human
  offers per Middle Dream theme instead of 40 (6.11). This makes the
  switch harder to turn on, never easier;
- **the launcher streak cap is now 3 runs**, so after three runs on one
  floor-1 theme the next is forecastable (R-SEED-2, R-SEED-6);
- **the Heart's psychic pressure is capped** (at most 2 psychic at-will
  types per encounter) and ClassThemeSpread is gated on the Heart and
  each final boss (8.11, 11.4);
- **the premise text was trimmed** to stay uncommitted, as Q3 answered:
  "Some go down; few wake."; the Gut and the Heart no longer imply who
  or what the Dreamer is (7.5, 8.10, 8.11);
- **renames:** the Supper's Hostess and Host become the Seneschal and
  the Head of the Table (the Supper's Obsession is now hunger, distinct
  from the Wake); Oil Flask, Engulf and Black-Veil Cousin are renamed
  (8.0); the id schemes became RC, RP, RE and CF (front matter).

**New lead's calls made while writing this document** (beyond the
answered proposal; confirmed by the client with the document on
2026-11-21):
1. **Two switches:** `route.twoDoorsEnabled` and
   `route.lastHollowChoiceEnabled` are separate; the Last Hollow choice
   goes on only with Two Doors and only if BossParity also passes, so
   Two Doors may ship with the final boss still a preview (6.11).
2. **Switches in the seed key:** the switch state is part of the run's
   configuration snapshot, hashed into the reproducibility key (seed,
   manifest, configuration snapshot, unlock snapshot) and fixed at run
   start; a run never changes mode mid-run (6.4 R-SEED-5).
3. **The Reference chooser's definition:** the door with the higher
   floor-clear rate in the ThemeParity table for (hero class, party
   size, floor), at that floor's fixed arrival state; ties to Door A;
   Random and Oracle choosers reported beside it (6.11).
4. **The pick-rate sample:** read on human playtest sessions with the
   switch on behind a playtest flag, with the 6.11 decision rule (about
   150 offers per Middle Dream theme, 40 per (theme, floor, partner)
   cell) *(illustr.)*, the Reference chooser's rate beside it (6.11).
5. **A failed gate after M2's balance pass** leaves the switch off, is
   reported to the client, and the doors are re-tested in M3 (6.11).
6. **The camp-order slot:** the next-floor or door card is step 6a of
   companions 7.11, after the potion top-up and before card A and the
   Muster (6.8).
7. **R-SEED-6 adopted for M2** (the launcher-only streak redraw), rather
   than left optional, with streaks capped at 3 (6.4).
8. **When the Wake is judged:** once, at the M2 floor-1 balance freeze;
   if it fails, it is the first M3 unlock and is not re-tested in M2
   (8.2).
9. **`F1EscortBreakable` defined** as: every escort and reserve group
   of a 50–74% theme's floor-1 bosses is drawn from non-fearless types
   only (8.2).
10. **How a cut is applied:** the lead applies Cut 1, then Cut 2, on the
    M2 schedule review and reports it; anything below Cut 2 returns to
    the client; a cut never removes the Heart, a tier's pool,
    `F1EscortBreakable`, the charter or the Two Doors gates (12).
11. **Validators V1–V7 defined** (11.2), with the route record, the
    configuration snapshot and the route events of 11.1.
12. **`AntechamberDominatesArena`,** a layout assertion that every
    floor-1 layout has an antechamber every path to the arena passes
    through (9.4); the catch-up report is named
    `AntechamberCatchUpRate`, keeping "Threshold" out of new names (8.0).
13. **RT-3 flagged** as touching decided text (main 6.3's streams
    bullet, following the CMP-3 precedent), covered by Q1.
14. **Readability gate failures** hold a theme's art back and never
    change a rule (7.4).
15. **The design-check fixes** marked (lead's call) across the document:
    `drawableSet`, `shipStatus` and the canonical unlock set (6.4); the
    fixed-count draw details and the walk timing (6.4); authored
    `pairKey`s (8.1); the configuration snapshot fields (11.1); the
    metric definitions and what each gate blocks (11.4); the floor-5
    lair form of lever 3 (6.6); the extended charter clauses inside
    RC1–RC8 (7.4); pillar 6, Obsession kinds (7.3); the Keepsake rule
    (9.3); the roster fixes (the Mastiff, the Bile Ooze, the skittish
    Carver, Teetering drops, boss signatures, role props, the completed
    Wake).

### 19.2 Future items triggered by reports or later designs

These come back to the client only if a report or a later design
triggers them:
- a Two Doors or Last Hollow gate failing after M2's balance pass (the
  switch stays off; the client is told);
- any cut below Cut 2 (1c, the Master in the Frame, which is
  client-only; the approved-scope floor; change BR-26; agreed, boss
  rooms Q1);
- the floor-time gate failing after every lever (the cap drops to 2,
  pre-authorised by companions Q2; the client is told);
- a theme held by a gate (ThemeParity over 8 points under the random
  draw, readability, floor time) past the M2 balance freeze, where no
  pre-authorised cut matches the hold;
- `LevelAtDoorByFloor` showing a step above about 0.7 level that the
  floor-3 trim does not fix;
- the premise questions left open by Q3: who the Dreamer is, its
  motives, whether it can wake, hero motives, endings;
- dropping or replacing Grim Surreal (every theme's grim reading is the
  fallback);
- the M3 rows: unlockable strata (RT-18), the M3 charter (RT-19),
  Nightmare tiers and the Perilous Door (RT-21);
- an owner declining RT-14 to RT-16 in a way the catalogue cannot absorb
  (RT-13 was accepted by the client; cross-document comparison 2026-11-23, CC-82);
- from the events design (its 14.2): the RS owner refusing the Launcher
  Fallow extension of R-SEED-6 (change EV-20; agreed, events Q1); the
  F1-boss cut order against Cut 1, carried to segment 9 (change EV-44;
  agreed, events Q2; any reorder is an RS change); **answered by the
  boss rooms design (change BR-26; agreed, boss rooms Q1; 12)**;
- from the boss rooms design (its 14.2): the RS owner refusing the
  graded cuts, the launcher F1-boss streak rule or the replaced 8.11
  Later line (returns to the client); the Wake's gate failing (the Wake
  stays the first M3 unlock; F1 variety rests on Renditions and the
  launcher rule); an F1 boss that cannot field 3 passing Renditions;
- from the exploration and information design (its 14.2): the RS owner
  refusing the travel-stop sting (pending request, 7.4 RC7; the travel
  stop uses visuals only);
- the naming pass.

---

## Appendix A: how the critique findings were resolved

### A.1 Judges

| Finding | Resolution |
|---|---|
| Winner: A's spine with C's pre-roll and route choice | Adopted (5.2); route choice as Two Doors (**agreed**, Q2) |
| The plain trim fails 20.1 | Confirmed; RT-7 moves the experience to floor 4's boss (300) and floor 5's lair (240) instead (A.3 #1) |
| ThemeParity of 5 points or less for choice | Adopted (6.11) (**agreed**, Q2) |
| A breakable-escort rule instead of the 750‰ default | Partly: both kept (A.3 #2; 8.2) |
| Mirror Court against the Long Supper on floor 4 | The Supper in M2 (floor 4 only); Mirror Court to M3 |
| Draft id mapping | RS-1/B1/C1 → RT-1. RS-2/RS-5/C2/M1/TC-2 → RT-2, RT-4. RS-6/C7 → RT-3. RS-3/RS-5 (A)/C8/M3 → RT-8. RS-4/B7/C9 → RT-7. RS-6 (A)/B4/C6/TC-1 → RT-5, RT-6. RS-7/B8/C10/TC-3 → RT-15. TC-4 → RT-14. TC-5/RS-13 → RT-13. TC-6 → RT-16. RS-9/C3/C4/M2 → RT-10. RS-10/C12 → RT-17. RS-11/B10/C13/M10 → RT-18. RS-12/TC-7 → RT-9. M8/M7 → RT-21. M4/C14/B12 → RT-12. (Draft ids are recorded here only.) |

### A.2 Fun critique

| # | Finding | Resolution |
|---|---|---|
| 1 | Deep floors least varied | The Supper in M2 as floor-4-only (a true 50/50); 2 Gut bosses; M3 unlocks lead with the third final boss, Mirror Court and Opera |
| 2 | A foretold dice tax without a choice | Middle-Dream-only Two Doors as the M2 build (**agreed**, Q2); a trimmed camp card in the random build (6.9) |
| 3 | Route choice rewards avoidance | Milestone marks; road not taken; per-build coverage report; the pick-rate gate. The Perilous Door stays M3 (it needs the ladder; a client change) |
| 4 | A soft finale | Morale shape by room; the Last Hollow choice; a mechanical Keepsake prop; sincere final-boss names; the premise twist cut |
| 5 | Invisible signatures | `signatureFeature` per theme; one home theme per square kind; bile in M2 (RT-13); DistinctnessCheck |
| 6 | A catch-up at the door | Moved to the antechamber, with a safe walk and the overflow shown |
| 7 | A thin floor 1, overlapping lessons | Binding lesson axes; 2 bosses per floor-1 theme; Kennel promoted to M2 core |
| 8 | Barks and names | The bark-timing content test (V7); the tone-by-depth review; the church cap; "Strung Marionette" |
| 9 | Party size at depth | The party-3 reference row; told early |
| 10 | Inconsistent act and theme names | One canonical act set; frozen names |

### A.3 Balance critique

| # | Finding | Resolution |
|---|---|---|
| 1 | Concentrated guard posts barely save time and push elites into guard posts | Rejected as the form; RT-7's plain trim with floor-4 boss experience 300 and floor-5 lair experience 240 (separate lists confirmed in enemies 10.1 and 16); concentrated posts kept as a simulator arm with a proof bar |
| 2 | Class-spread evidence overclaimed | Floor-1 default 750‰; the Wake gated; Kennel is the second floor-1 theme (8.2) |
| 3 | ThemeParity measures the wrong thing | Fixed arrival states; the ClassThemeSpread gate; sample sizes at both caps costed (10.4) |
| 4 | Class-polar matchups | Mirror Court to M3 with radiant limits; the Gut's Corroded cap and armour report; the Choir's water caps; RP3 never two consecutive floors below 700‰ |
| 5 | Water and oil spikes | 6.4 guards with party terrain states; entry-square rules |
| 6 | Roster rule violations | The Gaol Pack led by the Escapee (one family); no Sergeant-group signature on F1; the Wailer is artillery; the Model B Heart not used; every M2 theme keeps a leader-role type (no Commanding dependence); Wake Guests skittish; the Scavengers content test |
| 7 | A Reference policy understates experts | The Oracle cap of 1/3; class reports rerun under the Reference chooser; the pick-rate gate on humans |
| 8 | An absolute FloorTimeByTheme is noise | A relative gate; the slowest-theme human slice |
| 9 | Non-monotone difficulty after the trim | The `LevelAtDoorByFloor` report and a stated response |
| 10 | Catch-up side effects | Reruns listed; per-class rate reported |
| 11 | Wake dormant groups on a solo floor 1 | No dormant bias within hearing of rooms 0–2; per-theme death share |
| 12 | Count nits; neck lines | Recounted (10.3); `NeckLineByRun`; the forced-fire pool rule rejected with its reason; the trinkets owner told |

### A.4 Consistency critique

| # | Finding | Resolution |
|---|---|---|
| CF-H1 | The breakable-escort rule overturns agreed text | Flagged RT-9 with every section; narrowed to 50–74% themes; the Herald lever list still owed; the client told |
| CF-H2 | Teetering unruled with no headroom | RT-14 ruling requested before rosters freeze; a fallback drop named per theme |
| CF-H3 | Removing step 5 from M2 is unflagged | Step 5 kept in M2 with legal overlays (RT-6); only the floor-5 deep overlay retires; all three enemies 21/22 passages edited (RT-5) |
| CF-H4 | Route choice needs the client and DH 19.3 | The client agreed (Q2); DH 19.3 added to RT-10 |
| CF-H5 | Missing record fields; three counter-type definitions | `themeDamage`, `hardenedLayer`, `frost` added; one `counterType` definition (8.0, RT-4) |
| CF-M1 | Name collisions | All renamed (8.0 table) |
| CF-M2 | The RT-1 list is incomplete | Every location listed (17) |
| CF-M3 | Catch-up: unflagged text, a mid-combat edge case | RT-8 edits classes 7.3 and companions 7.1; deferral to combat end; DH already at level 3 |
| CF-M4 | Concentrated guard posts are an enemies 10.3 change with an assumed saving | Not adopted (A.3 #1) |
| CF-M5 | LootDoorInvariant overclaims | Restated around placement and the Resistant filter; a carrier-fallback report |
| CF-M6 | Dream Fragments add time where the gate fails | Moved to M3 |
| CF-M7 | No owed-requirements table; trinkets 23 against a dry floor 1 | Section 14; RT-20 |
| CF-M8 | Merged theme versions | One frozen version per theme (8.3–8.11); cross-theme checks rerun (10.3, 10.7) |
| CF-L1 | A Picket can field a leader | Corrected (8.2 g) |
| CF-L2 | Wave charter; Perilous owner; Lucid Key slot | RT-19; RT-21 client; the Lucid Key Later, pending items |
| CF-L3 | 5–6 against 5–7 types | RT-5 (enemies 10.6 already says 5–7) |
| CF-L4 | A draft raised the budget band | RT-7 edits the experience list only |
| CF-L5 | Bring-the-key | A watch report; class-blind cards |
| CF-L6 | The radiant ruling's scope | RT-15 includes stat model 8.1 and "is Revealed harmful" |

## Appendix B: sources

**Recall, with light web checks during research** (mechanisms only; no
names or text taken): Slay the Spire, Hades, Hades II, Risk of Rain 2,
Dead Cells, The Binding of Isaac, Spelunky 2, Enter the Gungeon,
Darkest Dungeon 1–2, Into the Breach, Monster Train, Balatro,
Inscryption, Loop Hero, Hylics, Cult of the Lamb, Shiren, DCSS, Brogue,
Noita, the mythic-underworld OSR essays (Philotomy) and megadungeon
practice, Made in Abyss. Details marked *(unsure)* in section 4 were not
confirmed.

Toy models and outputs (scratch, not project files):
`/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/seg1_numbers.py`,
`.../scratchpad/seg1_xp_extra.py`, `.../scratchpad/hybrid_routes.py`
(full prefix as on the first line), and the companions toy
(`.../scratchpad/comp_toy.py`).
