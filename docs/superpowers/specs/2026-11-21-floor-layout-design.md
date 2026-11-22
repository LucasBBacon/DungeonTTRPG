# Floor Layout: Design (Dungeon Generation, Segment 2)

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
design", "Arcanist design", "Herald design"),
`2026-11-20-companions-design.md` ("companions design") and
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1, the most
recent template)

Items marked **(agreed)** were decided by the client on 2026-11-21 for
the three questions this design raised (Q1–Q3, sections 0, 6, 9 and
19.1), by choosing the lead's recommendation (option A) in each case, or
restate a client answer recorded in an approved document. The client
also said that everything else in the lead's proposal stands as
written: the method (Model A with its grafts), the sizes, the slot
rules, hearing, the Circuit, stealth, flee and retreat, the per-theme
grammars, the art kit rules, the adopted mechanics and rejections, the
clock commitments, the told-plainly items, the FL-n changes, the
ownership split and hooks, the risks and the critique resolutions.
The client approved this document as a whole on 2026-11-21.

Items marked **(lead's call)** were filled in by the design lead inside
approved rules; the client confirmed them all on 2026-11-21, when it
approved this document as a whole, and they are binding. Items marked
**new lead's call** were made while writing this document, beyond the
proposal the client answered; they are listed in 19.1 and the client
confirmed them all on 2026-11-21. Items marked **(lead's call, after
the check)** were made in the revision that answered the design check;
they too are listed in 19.1 and the client confirmed them all on
2026-11-21. Items marked **(settled)** restate a rule from an approved
document; **(decided)** marks a main-design client decision. Items
marked **(change FL-n)** amend an approved document and are listed,
with the exact section they touch, in section 17; rows that touch
**decided** or **agreed** text are flagged there, and the client's
approval of this document on 2026-11-21 (with Q1–Q3 where a row names
one) covers them. Items marked **(told plainly)** narrow or re-read
agreed or settled text, or state an unwelcome forecast, and were told
to the client in plain words (19.1); the client confirmed them all on
2026-11-21. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q3 of this document (2026-11-21), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules; confirmed by the client with this document on 2026-11-21 |
| **new lead's call** | A lead's call made while writing this document, beyond the answered proposal; listed in 19.1 and confirmed by the client on 2026-11-21 |
| **(lead's call, after the check)** | A lead's call made when revising the document after the design check; listed in 19.1 and confirmed by the client on 2026-11-21 |
| **(change FL-n)** | Amends an approved document; listed with its exact section in 17 |
| **(told plainly)** | Narrows or re-reads agreed or settled text, or states an unwelcome forecast; told to the client in plain words (19.1) and confirmed by the client on 2026-11-21 |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (10, 11) |

The client's brief: dungeon generation segment 2 of 12, floor layout.
The client gave no specific input beyond wanting manageable segments,
and welcomed research and a recommendation.

The lead's answers, which the client accepted (Q1–Q3, **agreed**), are
in section 0: **floors are built from authored Blueprints rewritten by a
cyclic grammar and proven at build time, so the runtime cannot fail; open
themes are partitioned into ordinary rooms (Q1); every boss stands behind
a Forecourt antechamber with a Stairhead beyond the arena, near-safe only
on floor 1 (Q2); and fair Hairline secrets appear from floor 1 in two
themes (Q3).**

**Change prefix.** This design uses **FL-n** ("floor layout"). A grep for
`FL-[0-9]` and for the whole word `FL-` across the 18 approved documents
finds no hits. The research drafts reused ids with different meanings;
this document issues **one** register, FL-1 to FL-22 (17), and no draft
id is used anywhere in it. Local id schemes were also chosen to collide
with no approved id **(new lead's call)**: the graph invariants are
**LG1–LG20** (DH 5.5 already uses G-ids), the generation steps
**LS1–LS13**, the tensions **LT1–LT19**, the socket rule **SK1**, and the
Appendix A findings **FF** (fun), **FB** (balance) and **FC**
(consistency).

**Names are placeholders** until the client naming pass. Every new name
was grep-checked as a whole word against the 18 approved documents and
has 0 hits: Blueprint, Site, Portal, Arch, Circuit, Forecourt, Sanctum,
Stairhead, Bossgate, Grille, Drape, Squeeze, Latch, Hairline, Alcove,
Landmark, Precinct, Hinge, Postern, Nexus, Bypass (capitalised; "Stealth
bypass" stays the enemies word), Last Turning, near pair, Overlook,
Chute, Strongroom, Drop-bar, Way Back, `SkippablePool`, `topologyClass`,
`layoutGrammar`, `LayoutGrammar` (V8), `HearingGap`, `PortalMinimums`,
`LayoutDistinct` and every report and record name in 11.

**Names avoided because they are taken:**

| Avoided | Taken by | Used instead |
|---|---|---|
| Opening (a Portal kind) | The Shade's **Opening** precision feature (classes 8.4; Shade design) | **Arch**: any open doorway, whatever its skin **(new lead's call)** |
| Fork | Arcanist Spellcraft option **Fork** (RS 8.0 already avoided it) | "split" in prose |
| Curtain | "Curtain of Flame" | Drape |
| Descent | "Short Descent"; the items `descent` event | Stairhead |
| Search | Ability 7.2 action | Sound the wall |
| Spur (as a name) | Herald | lower-case "spur" in prose only |
| Spine, Threshold, Kit, Zone, Lane, Hub | Approved names or RS's avoided list | Blueprint, Forecourt, Precinct, `hubRim` as a class tag only |
| Bolt | Lantern, Lightning Bolt | Latch |
| Sealed | "exits are never sealed" (enemies 17.5) | Bossgate |
| Slot (for a rewrite hole) | Enemies 10.4 slots | Site |
| Rule (as a record name) | Generic use across the documents | `SiteRule` |

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every
count, minute and share comes from crude toy models (Appendix B); the M2
simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile,
unlocks, ladder), **M4** breadth, **Later** **(settled, main design 9)**.

---

## 0. At a glance

### 0.1 The answers to the brief

**1. How floors are built: Blueprint-first cyclic grammar, proven at
build time (Model A), with grafts from Models B and C (lead's call;
stands as written).**

1. **Blueprints.** Each theme authors a small set of macro graphs on a
   coarse grid of 12-square cells *(illustr.)*.
   - A Blueprint holds the main chain, typed **Sites** (holes for cycles
     and branches) and the cell footprint of every node.
   - The Precinct-and-Hinge shape from Model C is one Blueprint *family*
     any theme may author, not a runtime generator.
   - The draw count is fixed per Blueprint: every per-node and per-edge
     step spends a draw on every slot up to the Blueprint's `maxNodes`
     and `maxEdges` (6.1).
2. **Rewrite.** One fixed-count draw per Site picks a theme-legal
   `SiteRule`: Bypass, Circuit, loop-back side room, elite spur,
   Squeeze, Latch shortcut, secret, lock-key cache. Every rule carries
   its own cell footprint.
3. **Embed.** Authored room templates from the theme kit are stamped
   into the reserved cells by socket match. Corridor routes come from a
   fixed router's sorted list. Organic themes pick among pre-baked mask
   variants, each validated like a template.
4. **Annotate.** `mainPathIndex`, room identity, slot hosts, the
   Circuit, hearing matrices, anchors, HE-7 sockets and Landmarks are
   computed with no randomness.
5. **Prove.** A build-time validator, **V8 `LayoutGrammar`**, proves
   that every combination embeds and meets every invariant: each Site
   exhaustively over its rules, template classes and orientations, and
   the floor-wide invariants from composed per-Site bounds (11.2). The
   runtime is a lookup that cannot fail. It keeps only structurally
   proven assertions, checked once with DH's HE-7 check and under its
   existing single seed + 1 retry **(settled)**.

Why, on the toy:
- Model A failed at runtime on 0% of seeds.
- Pure graph-first generation failed on 29–78% of seeds.
- Brogue-style free accretion passed within HE-7's two tries on only
  0.2–0.6% of seeds.
- Model C's runtime accretion needed its authored fallback on 15–39% of
  seeds, against its own 2% alarm.
- Model B was as safe as A but gave 1–8 graph shapes per theme cell
  against A's 5–41 at a comparable piece count (about 370–450 room
  templates across nine themes for either, 8.2), so A buys far more
  variety per piece.

**2. Open-plan themes are partitioned into rooms (agreed, Q1; change
FL-1).** The Kennel woods, the Carnival plaza and the Heart's single
bedchamber are partitioned in the rules into ordinary rooms joined by
doorways, Arches, flaps and corridors of any length, including 0, while
reading as one place on screen. Without it, `mainPathIndex`, path
hearing, morale by room and line-of-sight escape stop working in those
themes. *Rejected:* B, true open maps; C, partitioning only the Kennel
and the Carnival.

**3. Every boss stands behind a Forecourt (agreed, Q2; change FL-5,
FL-6).** Every floor ends in the same **Sanctum**: a Forecourt
antechamber that every path to the arena passes through and that both
arena entrances open from, the arena, and a **Stairhead** behind the
arena through a **Bossgate** that opens on `BossDefeated`.
- **Floor 1:** empty and near-safe, as RS 9.4 approved.
- **Floors 2–5:** always on the patrol **Circuit**, never guaranteed
  safe while the patrol lives, which keeps the ability model's rejection
  of "a guaranteed safe room before the boss" intact. It is also the
  patrol's fight host, so it takes the floor's fight-room size.
  **Told plainly (after the check):** once the patrol is defeated, the
  Forecourt is as restable as any cleared room (6.8).
- *Rejected:* B, an antechamber on floor 1 only; C, a guaranteed safe
  antechamber everywhere.

**4. Fair secrets (agreed, Q3; change FL-17).** Hairline secrets: floor 1
0–1, in the Gaol and the Wake only (the Kennel has none); floors 2–3 1;
floors 4–5 1–2 *(illustr.)*. Optional content only
(`SecretNeverCritical`); one universal tell and no decoys; untrained
parties **Sound the wall**. *Rejected:* B, no secrets on floor 1; C,
exactly 1 on every floor-1 theme.

**5. Floor size by depth** *(illustr.)*. n counts `mainPathIndex` 0
(entry) to n−1 (arena) **(change FL-4)**.

| Floor | Party | n | Side rooms | Walkable squares | Critical path | Fight rooms |
|---|---|---|---|---|---|---|
| 1 | solo | 8 (7 + antechamber, RS 6.6) | 2–3 | 1,100–1,400 | 100–140 | 9×10–11×13, at least 10 deep |
| 2 | 2 | 8 | 2–4 | 1,300–1,600 | 110–150 | 10×10–12×13 |
| 3 | 3 | 8–9 (8 trimmed) | 2–4 (2 + a visible Alcove when trimmed) | 1,450–1,800 | 115–160 | at least 11×11 |
| 4 | 3–4 | 8 (7 trimmed) | 2–4 (2 + a visible Alcove when trimmed) | 1,550–1,850 | 120–160 (100–130) | 11×13–13×16 |
| 5 | 3–4 | 7 | 2–3 | 1,450–1,800 | 110–150 | 11×13–13×16 |

**6. Key choices (lead's call; stand as written):**
- **Exactly one route-avoidable fight per floor**, always a guard post.
  The lair is a cut vertex. `SkippablePool` asserts it (FL-9), which
  keeps enemies 20.1 at 610 **(settled)**.
- **Acoustic separation is derived, not invented.** The 10.1 bands under
  the 10.4 cluster budget force every pair of slot homes out of combat
  hearing (guard post + guard post = 200 > 150; guard post + patrol =
  170 > 150). Layout builds that separation in. Joins become
  player-caused, through **near pairs** at effective path 9–10 that loud
  acts or a drifting fight can pull (FL-11).
- **Layout is a clock input, not a clock lever.** On the toy,
  exploration time and patrol chain-pulls are each worth about 1
  minute, the margin by which trimmed floors 4–5 pass at cap 3. Layout
  commits to `ExploreSeconds` ≤ 3.5 min and `ExploreClicks` ≤ 40 at
  party 4 on floors 4–5, and stays out of the companions 7.4 lever order
  (FL-16) **(agreed levers untouched)**.
- **Kept, not contested:** main 6.8 **(decided)**, one flat rules level
  per floor. All "vertical stacking" is backdrop. Split-level sub-grids
  are not proposed.
- **New low-cost mechanics:** M1: the party open verb. M2: Grille,
  Squeeze, Latch, Drape, Hairline secrets, Listen, the party close verb,
  Last Turning, the floor count line. M3: Overlook, Chute, Strongroom,
  Drop-bar doors, Way Back. Rejected: Spyhole/Peer, collapsing routes,
  floor alarms, shifting rooms, camp-exit choice, split levels.

**Told plainly** (19.1): floor 3 runs over its target at heavy
exploration; floors 4–5 at party 4 pass the clock gate only narrowly, so
cap 2 stays likely; untrimmed floor 5 has one legal role order; the
floor-1 antechamber is near-safe by approved exception; FL-13 touches
agreed DH 17.1 text. **After the check:** on floors 2–5 the patrol lives
in the post-lair tail, so mid-floor rests are uncontested and the rests
gate will likely fire; the floors 2–5 Forecourt is safe once its patrol
is dead; dodging the patrol there usually means meeting it in the boss
fight; and the rest of the list in 19.1.

### 0.2 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Rooms joined by Portals; a corridor may have length 0; open themes partitioned into rooms | M1 | **agreed, Q1**; change FL-1 (decided text) |
| 2 | A `layout` child of the floor's dungeon substream with fixed draws; the party-agnostic graph drawn first; dressing on a display stream | M1 | lead's call; FL-2 (decided text) |
| 3 | Layout runs as enemies 10.5 step 1a and outputs slot hosts | M1 | lead's call; FL-3 |
| 4 | n counts entry through arena; floor-1 antechamber extra; "room k" means `mainPathIndex` k (except enemies 10.2's room level) | M1 | lead's call; FL-4 |
| 5 | A Forecourt antechamber on every floor; Arches into the arena; on the Circuit on floors 2–5; near-safe on floor 1 | M1 / M2 | **agreed, Q2**; FL-5 |
| 6 | A Stairhead behind the arena through a Bossgate | M1 | **agreed, Q2**; FL-6 |
| 7 | Portal kinds; hearing and movement graphs | M1 / M2 | lead's call; FL-7 |
| 8 | Party door verbs: open in M1, close in M2 | M1 / M2 | lead's call; FL-8 |
| 9 | One avoidable fight (a guard post); lair a cut vertex; `SkippablePool`; Last Turning | M1 / M2 | lead's call; FL-9 |
| 10 | Circuit definition and isolation | M1 | lead's call; FL-10 |
| 11 | Hearing conventions, `HearingGap`, near pairs, draw-free repair | M1 / M2 | lead's call; FL-11 |
| 12 | Flee exits, anchors, entry rules; RC3 kept literal | M1 (flee exits, entry rules) / M2 (anchors) | lead's call; FL-12 (told plainly) |
| 13 | Index and room-identity rules for Portals and Alcoves; the arena owns its doorways | M1 | lead's call; FL-13 (agreed text; told plainly) |
| 14 | `layoutGrammar` record, V8 `LayoutGrammar`, `topologyClass`, `LayoutDistinct` | M1 / M2 | lead's call; FL-14 |
| 15 | Runtime asserts only when proven at build time; HE-7 retry shared | M1 | lead's call; FL-15 |
| 16 | Clock commitments and reports; no lever | M2 | lead's call; FL-16 |
| 17 | Hairline secrets and Sound the wall | M2 | **agreed, Q3**; FL-17 |
| 18 | Listen at a closed door | M2 | lead's call; FL-18 |
| 19 | Latch and the return-Portal principle | M2 | lead's call; FL-19 |
| 20 | Floor count line | M2 | lead's call; FL-20 |
| 21 | Requests to the enemies owner (boss and Champion leash, doorway-hold AI, multi-bypass) | M1 / M2 | flags; FL-21 |
| 22 | RS 13 ownership rows amended: layout supplies sockets and minimums; named segments keep the rules | M1 | lead's call; FL-22 |

---

## 1. Intent

The main design fixed that each floor is "generated from the run seed as
rooms and corridors, with a guaranteed path from entrance to boss", on
one flat square grid shared by exploration and combat, under an
orthographic isometric camera that rotates in 90° steps **(decided, main
4.3, 4.6, 6.8)**. The enemies design placed slots on rooms, measured
hearing as path distance through doors and asked generation for a
Stealth bypass, a patrol through cover, retreat anchors and an elite
"visible before commitment" (10.4, 12, 14.5, 25). DH asked for a
`mainPathIndex` on every square and constructive HE-7 container
reservation (12.4, 17.1). RS fixed room counts, the floor-1 antechamber
and nine theme layout archetypes (6.6, 9.4, 8). None said what a room,
a corridor or a door is in the data, how a floor is drawn from the seed,
or how all of those owed properties are guaranteed on every seed.

The answer in one sentence: **a floor is an authored Blueprint whose
holes are rewritten by a fixed number of seeded draws and filled with
authored rooms, every combination proven legal at build time, so every
seed yields a floor that meets every rule, reads as its theme and keeps
fights, stealth, flight and the clock in balance.**

## 2. Goals, non-goals, anti-goals

### Goals

- **Every seed works.** No runtime search; a floor is a lookup over
  combinations proven at build time (V8). The runtime keeps only
  assertions with a structural proof (FL-15).
- **Every owed property holds by construction:** `mainPathIndex`, HE-7
  and HE-4 sockets, slot hosts, a Bypass, a patrol Circuit through
  cover, retreat anchors, the elite vantage, the antechamber, stairs
  (14).
- **Floors play differently by theme:** a `topologyClass` per theme
  (grid, beads, courtyard, comb, lattice, hubRim, axis, braid, spiral),
  each with its own grammar, door share and near pairs (7).
- **Fights are chosen, not stumbled into:** doors, Listen, Grilles,
  flank pairs and the hearing outline make the next fight a decision.
- **Flight and retreat always exist:** a line-of-sight break within 6
  of every fight entry; non-door flee exits; a cleared room behind.
- **Seeds stay pure:** a fixed draw count per Blueprint (R-SEED-3);
  dressing on a display stream; the route untouched (R-SEED-1).
- **The clock is respected:** layout commitments on floors 4–5 at party
  4 (6.10).

### Non-goals

- **Room interiors, terrain, hazards, traps, locks, checks, encounter
  fill, loot contents, non-combat rooms, boss rooms and exploration UI
  in full.** Those are segments 3–12; this document hands them sockets,
  tags and minimums (13).
- **Multi-level rules geometry.** Main 6.8 **(decided)** keeps one flat
  rules level per floor.
- **Final names, numbers and art:** the naming pass, the simulator and
  the art team.

### Anti-goals

- **A warren of identical rooms:** guarded by topology classes,
  `LayoutDistinct`, signature rooms, Landmarks and `TraversalFeel`.
- **Rejection sampling at runtime:** a floor that may fail and reroll
  silently.
- **Loops that skip fights:** only one route-avoidable fight per floor,
  so enemies 20.1 holds (`SkippablePool`).
- **Mandatory backtracking:** `BacktrackBound`, loop-back Sites,
  Latches.
- **A guaranteed safe room on floors 2–5:** the antechamber is always on
  the Circuit **(agreed, ability 15.1; ability 5 lead's call kept)**, and
  no space is sealed against patrols (`NoSealedPocket`).
- **Fake geometry:** painted bars on solid walls, walkable-looking
  backdrops, unit-like props (RC1).

## 3. Constraints

### 3.1 What the parents already fix

The 18 approved documents were grepped for room, door, corridor,
`mainPathIndex`, antechamber, arena, guard post, patrol, hearing, flee,
stealth and cover, and each hit was read in context.

- **Grid (main 4.3, decided):**
  - flat square grid, 8 directions, diagonal costs 1, no corner cutting
    (M0 plan; its `CornerCut` covered walls only and now covers every
    movement-blocking square, forced moves and a piece's own travel
    included: room terrain design 6.2.1, TR-5);
  - one map for exploration and combat, "no separate battle arenas";
  - turn-stepped exploration; symmetric line of sight and fog;
  - no high ground;
  - joiners 1 per fight on floors 1–2, 2 deeper; fleeing by breaking line
    of sight.
- **Generation (main 4.6, decided):** "rooms and corridors, with a
  guaranteed path from entrance to boss".
- **Camera (main 6.8, decided):** orthographic isometric, 90° rotation,
  walls cut away or faded, "one level per floor, no height in the
  rules", theme tile pieces.
- **Determinism:** main 6.3 **(decided)**, RT-3, RS 6.4 R-SEED-1 (no
  retry perturbs the route) and R-SEED-3 (fixed counts). **Main 7:**
  property tests that every floor is connected, the boss reachable and
  nothing in a wall.
- **RS 6.6:**
  - room counts and 1–3 empty or special main-path rooms;
  - `pacing.trimmedFloors` (floor 4 "8, or 7 trimmed" is already
    written; RS 10.2 may extend the trim to floor 3, [3, 4, 5]);
  - floor 5 under lever 3 has 2 guard posts and lair 240.
- **RS 9.4:**
  - the floor-1 antechamber, a cut vertex, empty, "a short safe walk";
  - `AntechamberDominatesArena` is owned by this segment.
- **RS 8.0–8.11** theme records and hooks; RS 13 and 14 hooks (stairs
  to layout); RS 7.6 refusals; charter RC1–RC9 **(agreed, RS Q3)**; the
  catalogue **(agreed, RS Q4)**.
- **Enemies 10.4 (settled):**
  - slots: patrol loops through 2–4 rooms, never the entry or boss room,
    clear of main-path rooms 1 and 2 for 20 ticks (DH-28; read with
    DH 17.1 as rooms 0–2); 3 guard posts (2 trimmed) on the main path at
    chokepoints and doors; lair on the main path past the midpoint;
    optional elite room off the path and visible before commitment;
    boss in an authored arena;
  - guarantees: a Stealth bypass, a patrol through cover, the cluster
    budget.
- **Enemies 12.3–12.5:**
  - path-distance noise, never through walls; combat 8, loud 6, failed
    quiet 4, loud trap or shattered door 10; −3 per closed door; Dormant
    groups hear at half radius;
  - monsters open unlocked doors and never close or lock them; locked
    doors block monster paths;
  - pursuit 0–3 rounds.
- **Enemies 14.5:** Routed units Dash to generation-marked anchors.
- **Enemies 17.5 (settled):** an arena with at least 2 entrances; exits
  never sealed; the boss Wary and leashed to its room.
- **RS 6.6 (enemies 20.1 band 110–135%):** baseline 710; 610 after one
  guard-post bypass; pre-boss 510 on floor 5 (410 after one bypass).
- **DH:**
  - 12.4 HE-7: 2 or more no-check main-path containers before the boss
    room, one in the first half; one retry at seed + 1, then a crash;
  - 12.5: "first half" means index < FloorDiv(n−1, 2);
  - 17.1 **(agreed Q9)**: `mainPathIndex`, corridors take the lower
    index, no Surprise at floor-1 index 0–1.
- **Ability 15.1 (agreed):** no guaranteed safe spot. **Ability 5
  (lead's call):** a guaranteed safe room before the boss was rejected.
- **Companions 7.4 (agreed Q2):** the floor-time gate, median ≤ 20 min
  at party 4 on floors 4–5.

### 3.2 What the approved documents owe this design

Each is met in section 14:
- `mainPathIndex` on every square (met as `mainPathIndex` where DH 17.1
  defines one and `effectiveIndex` everywhere, FL-13); `expectedTenths`
  inputs;
- HE-7 and HE-4 sockets;
- the Stealth bypass / second route; a cover room on the patrol route;
- retreat anchors (M2); the elite "visible before commitment";
- stairs, left-behind warning and Scrap;
- at least 3 Acrobatics choices (shared with segment 3);
- DEP-S1's 2 closed unlocked doors from floor 2;
- secrets and unexplored rooms (enemies 14.6 facts);
- Keepsake slot numbering;
- the Wake and Heart `placementRules` inputs;
- the trader side-room tag (Later);
- room identity for leftovers.

### 3.3 Tensions found and their resolutions

| # | Tension | Resolution |
|---|---|---|
| LT1 | Main 4.6 "rooms and corridors" against open-plan themes | Rooms joined by Portals; corridors may have length 0; open themes partitioned (FL-1) **(agreed, Q1)** |
| LT2 | Two or more arena entrances (17.5) against the antechamber as a cut vertex (RS 9.4) | Both entrances open from the Forecourt (FL-5) **(agreed, Q2)** |
| LT3 | Floor-1 "short safe walk" against "no guaranteed safe spot" (agreed) and ability 5 | Floor 1 is the approved exception, told plainly. Floors 2–5 put the antechamber on the Circuit every time (FL-5) **(agreed, Q2)** |
| LT4 | "Visible before commitment" against first sight being a noticing trigger (12.2; symmetric line of sight decided) | A sight-only Grille vantage at ≥ 7 squares, behind an opt-in vantage Door; the Champion holds its room (FL-7; FL-21 request, with a layout fallback) |
| LT5 | DEP-S1 needs 2 closed doors; Carnival, Gut and Heart are door-poor | Every theme authors door skins; `PortalMinimums` |
| LT6 | Stairs owed but placed nowhere | Stairhead behind the arena (FL-6) **(agreed, Q2)** |
| LT7 | Patrol "loops" has no geometry | The Circuit, counted in Room nodes (FL-10); on floors 2–5 it lives in the post-lair tail (told plainly) |
| LT8 | A bypassed lair fails 20.1 (570, 106%) | The lair is a cut vertex; only guard posts are route-avoidable; `SkippablePool` (FL-9) |
| LT9 | Den and Sleeper lairs are "Stealth-bypass candidates", so they can be sneaked past in-room | Layout closes only the route case; flagged to the enemies owner (FL-21, told plainly) |
| LT10 | The cluster budget puts every pair of slot homes out of hearing; authored hearing pairs would break it | Near pairs at effective path 9–10, outside combat radius 8, inside the loud-trap / shattered-door radius 10. The Gaol `pairDoor` reading needs the enemies owner (FL-11) |
| LT11 | Hearing changes as doors open and never re-close | `hearingState(k)` for the main path, with every non-forced door counted open in proofs; Circuit-internal doors treated as open (FL-11) |
| LT12 | "Room k" read both as index and as room level | `mainPathIndex`, except enemies 10.2's "Room N", which stays room level as defined there (FL-4) |
| LT13 | DH 17.1's corridor index rule would give the floor-1 Bypass arm index 1, widening Surprise immunity | Only Portals between consecutive main-path rooms take a `mainPathIndex`; every square has an `effectiveIndex` (FL-13, told plainly) |
| LT14 | RC3: Routed units never go "through a door", but they flee through doors | Every fight host gets a non-door flee exit; RC3 stays literal (FL-12) |
| LT15 | The Gaol `signatureFeature` "existing door rules" has no rule behind it | The party close verb (FL-8); RS 8.3 text amended. Shutting mutes and delays; it does not cut a path unless locked (told plainly) |
| LT16 | Side-room claimants exceed 2–3 side rooms on floors 1 and 5 | Alcoves carry caches and do not count; the two-room Circuit ring |
| LT17 | Floor time | Commitments, not a lever (FL-16) |
| LT18 | Main 6.8 flat rules against "vertical stacking" | Backdrop only; walkable squares exactly coplanar (8.1) |
| LT19 | RS 13 gives door rules, secrets, Acrobatics counts and the Stratum Card to other segments | Layout supplies sockets and minimums only (FL-22) |

### 3.4 Terms

| Term | Meaning |
|---|---|
| **Blueprint** | An authored macro graph on 12-square cells: main chain, Sites, node footprints, role orders, signature room (6.1) |
| **Site** | A typed hole in a Blueprint, rewritten by one `SiteRule` draw (6.1) |
| **Room node** | A room in the graph: the entry, a main-path room, a side room, the antechamber, the arena or the Stairhead. Corridors, Alcoves and the Bypass arm are not Room nodes. Room nodes = n + k + the Stairhead; the elite room and Circuit side rooms count among the k side rooms (6.3) |
| **Portal** | Any connection between Room nodes or a Room node and an Alcove: Arch, corridor, Door, Locked, Bossgate, Drape, Grille, Squeeze, Latch, Hairline secret (9.2) |
| **Arch** | An open doorway of any skin (archway, gap, tent mouth); hearing and line of sight pass |
| **Door kind** | Door, Locked, Latch, Hairline secret and Drape. A **non-door** Portal is an Arch or a corridor; Squeezes, Grilles and the Bossgate are neither and are never flee exits (9.2) **(lead's call, after the check)** |
| **Alcove** | A sub-room of at most 5×5 attached to one Room node; shares its parent's identity and index; never counted as a room (FL-4, FL-13) |
| **Main chain / main path** | The Blueprint-designated chain of Room nodes 0..n−1, entry to arena, with their joining Portals (LG1) |
| **`mainPathIndex`** | The index of a main-chain room, and of the squares of a Portal joining consecutive main-chain rooms (the lower index; arena-adjacent squares take the arena's), and of a main-chain room's Alcoves. Null elsewhere (FL-13) |
| **`branchIndex`** | For a side room: (attachment index, depth, sibling order). Attachment index = the `mainPathIndex` of the main-chain room the branch hangs from; depth = Room nodes from the chain (1 for a spur off the chain); sibling order = Site order. "Deeper" means a higher (attachment index, depth), compared lexicographically **(lead's call, after the check)** |
| **`effectiveIndex`** | Defined on every square: the `mainPathIndex` where there is one; otherwise the attachment index of the room or branch the square belongs to (Alcoves take their parent's) **(lead's call, after the check)** |
| **Socket** | (cell, side, offset, width): offsets 3, 6 or 9, width 1–3, covering offset to offset + width − 1 along the cell side (SK1, 6.4) |
| **Template class** | (role, size class, socket mask); V8 checks the worst case over its member templates (11.2) |
| **Forecourt** | The antechamber: main-path room n−2, which every path to the arena passes through (6.11) |
| **Sanctum** | The Forecourt, the arena and the Stairhead together (6.11) |
| **Stairhead** | A small room behind the arena holding the stairs; none on floor 5 (6.11) |
| **Bossgate** | The Portal between arena and Stairhead; impassable until `BossDefeated`, then an Arch (6.11) |
| **Circuit** | The patrol's cycle of 2–4 Room nodes (6.7) |
| **Bypass** | The one cycle that lets a party route around one guard post (6.8) |
| **Near pair** | A fight host and a neighbouring group at effective hearing path 9–10 (6.6) |
| **Precinct / Hinge / Postern / Nexus** | The Model C Blueprint family: precincts of rooms joined by a doored Hinge, with a Postern back route and a Nexus junction room |
| **Last Turning** | The Bypass placed around the last guard post before the antechamber (6.8) |
| **Grille, Drape, Squeeze, Latch, Hairline** | Portal kinds (9.2) |
| **Landmark** | One unique set dressing per room that orients the player (8.1) |

---

## 4. How other games handle floor layout

Entries marked *(unsure)* are from recall. boristhebrave.com could not be
fetched (network allowlist), so the Unexplored and Gungeon details are
unverified.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Rogue / NetHack | 3×3 room grid with corridors; NetHack adds special levels and vaults | Fixed room count by depth | Few loops; samey shapes |
| Brogue | Room accretion tree, then loops added for "pillar dancing", lakes that never disconnect, secret doors likelier with depth | Tree, then loops, then organic fill as the order of operations; secrets scaled by depth | Unbounded placement and validation retries (toy: 0.2–0.6% pass within two tries) |
| DCSS | Many layout algorithms plus a large vault catalogue per branch | Authored set pieces as variety | Layouts that favour ranged or open-ground swarms |
| Spelunky | Macro-grid solution path with typed room templates | Path-first guarantee; typed templates by socket | — |
| Binding of Isaac | Room tree on a grid; specials at dead ends; secrets placed by deducible rules | Dead ends as reward slots; fair, deducible secrets | Almost no loops |
| Enter the Gungeon | Authored per-floor flows plus prefab rooms *(unsure on detail)* | Flow position equals pacing | Late-game room repetition |
| Dead Cells | Per-biome concept graph filled with hand-made chunks; "as restrained as possible" | Procedural but designed: a graph per biome | — |
| Unexplored (Dormans) | Cyclic generation: lock-key, hidden shortcut, dangerous-short / safe-long cycles | The Bypass is the dangerous-short / quiet-long cycle; the Latch is the valve | Runtime graph search (toy: 29–78% failure without reserved cells) |
| XCOM 2 | Plot and parcel: authored plots, hand-built parcels in 3 sizes, thin walls | Blueprint = plot, template = parcel, a shared socket standard | Fully random maps, which the developers found "not fun" |
| Hades | Authored chambers; exits preview rewards; no backtracking | One room per screen; preview before commitment | — |
| Darkest Dungeon | Rooms and corridors on a schematic node map | The schematic room-node minimap | Long backtracking |
| Slay the Spire | Row rules: no elites early, rest row before the boss | Pacing rules by graph position | — |
| Diablo 1–4 | D1 per-level generators plus set pieces; D4 backtracking objectives | Set pieces | Mandatory backtracking objectives (widely disliked) |
| Invisible Inc | Grid of rooms, doors, peeking, readable patrols | Doors as pacing valves; readable patrol routes; Listen as peeking | "A warren of identical rooms": hence per-theme topology classes |
| Gloomhaven | Opening a door reveals the room | The door is the moment the party chooses the next fight | — |
| Into the Breach | Small boards where every square matters | Fight rooms small enough for terrain to matter | — |
| FFT, Tactics Ogre, BG3, Mordheim | Rules height plus a rotating camera; occlusion and picking pain | Evidence for keeping the decided flat rules | Rules verticality |
| Cogmind | Validate-and-reject; some maps fail dozens of times | — | Rejection sampling |
| Monster Hunter (pre-World) | Numbered zone graph | A legible zone graph; Precinct tints | — |
| Jaquays / Xandering | Multiple entrances, loops, secret paths | Flank entries; loops for retreat | Loops that skip fights (that would break enemies 20.1) |

## 5. Layout models

### 5.1 The models

Five models were built as toys (150 seeds × 15 floor/theme cells each):

- **A, graph-first cyclic:** a Blueprint plus a footprint-carrying
  rewrite, embedded into reserved cells.
- **B, template stitch:** authored Plots plus Parcels on Lots.
- **C, Precincts and Nexuses:** runtime accretion with a repair ladder.
- **R, pure graph-first with free embedding** (baseline).
- **N, Brogue-like accretion and reject** (baseline).

### 5.2 Verdict and judges' scores

Three judges scored the three candidates; the scores are their means.

| Model | Tactical variety | Theme expression | Pacing and clock | Readability | Determinism and validation | Fit with docs | Low build cost |
|---|---|---|---|---|---|---|---|
| **A: graph-first cyclic** | 7.3 | 7.7 | 7.0 | 7.7 | **9.3** | **8.7** | 5.7 |
| B: template stitch | 5.7 | 6.7 | 7.0 | **8.0** | **9.7** | 8.0 | 4.0 |
| C: Precincts and Nexuses | **8.3** | 7.7 | 6.0 | 6.7 | 4.3 | 6.7 | 3.0 |
| R: pure graph-first (baseline) | 7 | 5 | 6 | 5 | 2 | 3 | 6 |
| N: Brogue-like (baseline) | 7 | 4 | 5 | 5 | 1 | 2 | 7 |

**Model A is adopted**, unanimous across the three judges **(lead's
call)**.
- *B rejected as the method:* it buys variety with content; 48+ pieces
  per theme still leaves 86% of Heart rooms already seen by run 5.
- *C rejected as a runtime method:* once its Plats reserve Postern and
  Circuit cells, it *is* Model A.

### 5.3 Grafts

| From | Graft |
|---|---|
| C | The Precinct / Nexus / Hinge / Postern Blueprint family; the derived acoustic separation; flank pairs; Precinct tints; `branchIndex` |
| B | Deep fight rooms; the centre-socket rule SK1; the marker contract; `hearingState(k)`; the free-space rule; party door verbs |
| Theme pass | `topologyClass` and `LayoutDistinct`; nine grammars |
| Mechanics pass | The return-Portal principle; Grille, Squeeze, Latch, Listen, Hairline |
| Numbers pass | Circuit isolation, the Squeeze Site, entry rules, size fixes and bounds (10) |

---

## 6. Floor layout rules in full

### 6.1 Generation procedure (change FL-2, FL-3, FL-15)

**Streams (lead's call; the derivation, retry and weighted-pick rules are
lead's calls after the check):**
- The floor's dungeon substream (RT-3, keyed by floor index) gains a
  child keyed `layout`, derived as `Derive(parentState,
  FNV-1a("layout"))`, the same function that derives the `route` and
  `recruit` children. Only layout draws from it.
- Enemies 10.5 steps 2–11 keep drawing from the parent substream as
  approved. The `route` child is untouched, so R-SEED-1 and V6 hold.
- Dressing (art variants and props with no rules effect) draws from a
  display-side child, `Derive(parentState, FNV-1a("dressing"))`, that
  the core never reads. Art growth never moves a rules seed.
- **Terrain (TR-17):** segment 3's rules pieces draw from a sibling
  child, `terrain = Derive(parentState, FNV-1a("terrain"))`, after LS13
  and before enemies 10.5 step 2; rules pieces never come from the
  `dressing` child (room terrain design 6.2.5).
- **Retry:** HE-7's retry reseeds the floor's dungeon substream at
  seed + 1, and every child, `layout`, `terrain` and `dressing`
  included, is re-derived from it, with nothing carried over.
- **Weighted picks** (rule weights, `bypassTargets[]`, `closedShare`)
  use one 64-bit draw, multiply-shifted into [0, the sum of the integer
  per-mille weights), scanning the cumulative list sorted by id. This is
  R-SEED-3's bounded-integer method applied to weights.
- **Told plainly:** adding templates *does* change layouts. That is
  covered by the manifest hash in the reproducibility key (R-SEED-5).

**Conventions:**
- Integer maths only; every candidate list sorted by a fixed key; no
  unordered iteration; every draw consumed even when only one option is
  legal.
- Per-node and per-edge steps iterate a fixed slot list of the
  Blueprint's `maxNodes` or `maxEdges` and spend a dummy draw on each
  unused slot, as LS5 does for forced-`None` Sites.
- **Canonical node order (lead's call, after the check):** the main
  chain by `mainPathIndex`; then Site nodes by (Site index, the rule's
  node index); then the Stairhead; then Alcoves by (parent `roomId`,
  socket index). `roomId` is the position in that order, and every draw,
  tie-break and Keepsake number uses it.
- Generation runs at camp step 11 for floor N+1 (RS 6.8).

**Inputs:** theme, floor index, actual party band (fixed at generation),
the configuration snapshot (`layout.*`, `pacing.trimmedFloors[]`) and the
theme's `layoutGrammar`. No run history is read: a theme appears at most
once per run (RS RP1), so a Blueprint cannot repeat within a run and no
rule is needed to forbid it.

| Step | Work | Draws (`layout` child, always consumed) |
|---|---|---|
| LS1 Length | n from `layout.mainPathRooms[floor]`, or `layout.mainPathRoomsTrimmed[floor]` when the floor is in `pacing.trimmedFloors`; k (side rooms) from `layout.sideRooms[floor]` or `layout.sideRoomsTrimmed[floor]`. Both draws are spent even when a range holds one value | 2 |
| LS2 Blueprint | Among the theme's Blueprints eligible for (floor, n, trimmed); V8 proves each one reaches every k in the range | 1 |
| LS3 Transform | One of the Blueprint's `allowedTransforms[]` (up to 8 dihedral variants) | 1 |
| LS4 Order | One of the Blueprint's authored role orders for n. Orders carry G only; the bypassed guard post is marked in LS5 | 1 |
| LS5 Rewrite | (a) The Bypass target: a weighted pick over `bypassTargets[]` (6.8, Last Turning), marking that guard post Gb. (b) The secret count s from `secretRange[floor]`. (c) One `SiteRule` per Site, in Site order, under the side budget below | 2 + one per Site (8–12) |
| — | **The graph is complete here and is party-agnostic.** Shared and daily seeds get the same macro graph at any party size (lead's call; told plainly against RS 6.8: the party band changes only room size classes) | — |
| LS6 Templates | Per node slot in canonical order: a template matching role, size class for the party band, socket mask (6.4) and `lessonTag` (consecutive main-path fights never share a tag). No template repeats on a floor while candidates remain | `maxNodes` |
| LS7 Orientation | Per node slot, among orientations whose sockets match the node's edges at both ends | `maxNodes` |
| LS8 Portals | Per edge slot: an index into the edge's sorted list of legal corridor routes (6.4), then the initial-state draw (spent even for fixed-state kinds) | 2 × `maxEdges` |
| LS9 Masks | Per node slot: organic themes pick one of the template's pre-baked mask variants, each validated by V8 like a template; other themes spend the draw. There is no runtime cellular pass **(lead's call, after the check)** | `maxNodes` |
| LS10 Structure | `roomId`, `mainPathIndex`, `effectiveIndex`, `branchIndex`, Portal and square ownership (FL-13), role tags, slot hosts, `equivalentHosts`, each fight host's `fleeExit` | 0 |
| LS11 Reserve | HE-7 first-half socket, HE-7 second socket, HE-4 supply socket: one draw each over a candidate list sorted by (`effectiveIndex`, `roomId`, socket index) (6.5) | 3 |
| LS12 Hearing repair | For each `HearingGap` shortfall, in pair order (slot-home `roomId` pairs ascending), upgrade the first repairable Portal on the shortest hearing path to a forced-closed Door; ties broken by (path length, `portalId`). **Repairable:** an Arch or Drape that is not an arena entrance, a fight host's `fleeExit`, a Bypass-arm Portal or the Bossgate. Each Portal at most once; bounded by the Portal count | 0 |
| LS13 Annotate | The final hearing and movement graphs and static matrices; Circuit waypoints and phase; the sight checks (`PartyEscape`, the Grille vantage, the Bypass sight window, `AntechamberScreened`); anchor rankings; door share; Landmarks; Keepsake slots; minimap nodes; `protectedSquares` and `protectedSightLines`; `layoutDigest` | 0 |

**Side budget in LS5 (lead's call, after the check):**
- Each Blueprint Site is `mandatory` or optional, with a `priority`.
  Each `SiteRule` states its `sideRoomCount`: 0 for a Bypass, a Squeeze
  or an Alcove cache; 1 for a loop-back side room or the elite spur; 1–2
  for a Circuit ring.
- Mandatory Sites (the Bypass, the Circuit, the elite spur and, from M2,
  the Squeeze Site) never take `None`.
- Optional side-producing Sites are filled in priority order until the
  running side total reaches k; every later one is forced to `None` but
  still draws. A rule draw is restricted to rules legal on this floor
  whose `sideRoomCount` fits the remaining budget.
- Loop-back Sites come first in priority, so LG10's ⌈k/2⌉ loop-backs are
  met for every k.
- **Secret Sites:** the Blueprint carries `secretSites[]`, as long as
  the top of its range. The first s draw real rules; the rest are forced
  to `None` and still draw.
- V8 proves every Blueprint reaches exactly every k in the floor's range,
  trimmed and untrimmed, with LG10's loop-back share.

**After layout (lead's call, after the check):**
1. **Segment 3 terrain** runs after LS13 and before enemies 10.5 step 2.
   It never blocks `protectedSquares` and never stands on a
   `protectedSightLines` square. Segment 3 may move its step with the
   lead, as long as it stays before the assertion point. **(TR-17:)**
   it draws from the `terrain` child (reservation step, then per node
   slot a layer pick, on/off and skin draws), places one of the
   template's terrain layers, and keeps each layer's **Clearway**, a
   connected superset of `protectedSquares` at least 2 wide in fight
   rooms, free of anything that blocks movement (room terrain design
   6.2.2, 6.2.5).
2. **Enemies 10.5 step 2** fixes homeRooms from the slot hosts and draws
   the trader (Later); steps 3–11 follow as approved. Step 9's cluster
   pass may move a group only within its slot host, or to a host that
   layout lists as equivalent for that role (`equivalentHosts`, proven by
   V8), never off a proven host (FL-3).
3. **The floor assertion point:** DH 12.4's HE-7 assertion and the
   layout runtime assertions (11.2) run together, before the loot step.
   On a failure, HE-7's single retry at seed + 1 (`GenerationRetried`)
   reruns everything; a second failure is a crash bug **(settled,
   DH 12.4; FL-15)**.

**The draw count is fixed per Blueprint:** 2 + 1 + 1 + 1 + (2 + the
number of Sites) + 3 × `maxNodes` + 2 × `maxEdges` + 3. `maxNodes` and
`maxEdges` are the largest totals over all the Blueprint's rule choices,
authored on the Blueprint and recomputed by V8 `DrawCount`. LS1 and LS2
draw before the Blueprint is known, but their counts are constant. The
number of draws therefore never depends on the data, which meets
R-SEED-3.

### 6.2 Graph invariants

All are proven at build time (V8, 11.2). Those marked † are re-asserted
at runtime at the floor assertion point (6.1, 11.2).

| # | Invariant | Source |
|---|---|---|
| LG1† | Main chain 0..n−1, designated in the Blueprint, not a computed shortest path. 0 = entry, n−2 = antechamber (Forecourt), n−1 = arena | enemies 6.3; FL-4 |
| LG2† | Connected; every walkable square reachable from the entry. Required content (main path, HE-7 and HE-4 sockets, elite room, anchors, Stairhead after the boss) is reachable without Locked, unfound secret, Squeeze or unlifted Latch Portals. Nothing in walls. No lock, check or trap gates it (`LockNeverCritical`, `TrapNeverSeals`; TL-20, traps and locks design 6.1 CC5, 6.20) | main 4.6, 7 |
| LG3† | **`AntechamberDominatesArena` on every floor**, judged on the pre-boss graph. No walkable square of another room lies within 3 squares across a wall of the arena or Stairhead; no crossable hazard bridges them (no Void or crossable hazard region between the antechamber and the Sanctum; V10 `NoSanctumBridge`, HZ-3, hazards design 6.2.2) | RS 9.4 extended (FL-5) |
| LG4† | Both arena entrances open from the antechamber, ≥ 5 apart, and are **dog-leg Arches, never Doors**, screened both ways (`AntechamberScreened`, 6.11). The arena's only other Portal is the Bossgate; on floor 5 there is none **(new lead's call; the dog-leg is a lead's call after the check)** | enemies 17.5; FL-5, FL-6 |
| LG5 | Floor 1: the antechamber has exactly 1 inbound Portal and the 2 arena Portals; no side room, no Alcove, not on the Circuit. Floors 2–5: 1–2 inbound main Portals (a Bypass arm landing in the Forecourt counts as one); **always a Circuit waypoint**; at most **two** Circuit side rooms attach, and neither touches the arena **(two: lead's call, after the check)** | RS 9.4; FL-5 |
| LG6 | The lair is a cut vertex between the entry and the antechamber; lair index in [FloorDiv(n−1, 2) + 1, n−3] | enemies 10.4; FL-9 |
| LG7† | Exactly one route-avoidable fight host per floor, a guard post (`SkippablePool`, structural form in 6.8). The optional elite room and the patrol's Circuit are excluded by definition. Bypass and Circuit traps are alarms at Easy detect or `alwaysKnown` (TP7 `BypassClean`; TL-20, traps and locks design 6.20) | enemies 10.4, 20.1; FL-9 |
| LG8† | The Circuit (6.7) | enemies 10.4; FL-10 |
| LG9 | Circuit isolation and `HearingGap` (6.6), proven with worst-case interior path bounds per socket pair | FL-10, FL-11 |
| LG10 | Branches: side rooms at depth ≤ 2; at most 1 depth-2 spur (none in M1), which ends in a Latch back toward the chain. At least ⌈k / 2⌉ loop-back side rooms, filled first by the side budget (6.1). Leaf dead ends 1–3 (Alcoves excluded); no empty leaf | numbers pass; fun critique |
| LG11 | Elite spur (6.5) | enemies 10.4, 17.4 |
| LG12 | Fight-host exits: guard posts, the lair and Circuit rooms (the floors 2–5 Forecourt included) have ≥ 2 Portals, including a **non-door flee exit** (an Arch or a corridor, 3.4) away from the party-entry side, recorded as the host's `fleeExit`. The elite room may be a leaf | enemies 14.5; RC3; FL-12 |
| LG13 | Flank entry: on ≥ 50% of main-path fight rooms *(illustr.)*, the approach side has a flank pair (two entry Portals ≥ 5 apart, joined outside the room within ≤ 12 squares) | Model C; FF-3; FB-6 |
| LG14 | Locked, secret, Squeeze (other than the Bypass arm), Latch and Chute Portals sit only on optional content and obey the **return-Portal principle**: the far end lies at a lower (attachment index, depth) than the near end (3.4 `branchIndex`). **`NoSealedPocket`:** every Room node and Alcove has a Portal on the monster movement graph once Locked Portals, Latches and secrets are opened, so no space is reachable only through Squeezes or seen only through Grilles. **`HairlineCue`:** every Hairline has its dead-end stub (9.2), and no other dead-end stub of ≤ 3 squares exists **(both lead's calls, after the check)**. **Pending request (HZ-23; M3):** an explicit exception to the return-Portal principle for Plunge, a Chute skin whose far end lies deeper on the main path (hazards design 6.10); not a rule until the FL owner rules. **`LockNeverCritical`** extends the optional-content rule to padlocked containers (TP9; TL-20, traps and locks design 6.11) | mechanics pass; FL-19; ability 15.1 |
| LG15 | Floor 1: rooms 0–2 form a linear chain. Only Alcoves and the Bypass arm (leaving room 1) attach to rooms 0–1. No Circuit room among 0–2 on any floor | DH 17.1; RS 8.2(d); DH-28 |
| LG16† | HE-7 and HE-4 sockets reserved (6.5) | DH 12.4 |
| LG17† | `PortalMinimums` (6.4), structural through `forcedClosed` edges | DEP-S1; armour 7.3 |
| LG18 | Flat: one walkable square per (x, y); walkable squares exactly coplanar (8.1) | main 6.8; RS 7.6; RC1 |
| LG19† | Exactly n main-path rooms and k side rooms (LS1), counted as in 3.4 | RS 6.6; FL-4 |
| LG20 | Entry rules: each fight host's party-entry Portal is a door kind or has a bend within 6; a 3×3 landing on the corridor side; start zones ≥ 7 from the entry mouth; no width-1 corridor longer than 2 ends at a fight-host Portal, and a width-1 chokepoint mouth (≤ 2 deep) opens onto the landing | numbers pass; FB-13 |

**Pending request (HZ-23; M3):** the hazards design asks the FL owner to
admit Plunge, a one-way, out-of-combat link from a side room or Dare
room to a cleared or non-combat room deeper on the main path (never the
Sanctum, never past the antechamber, ≤ 1 per floor), into the graph
invariants, with `NoSealedPocket` and `PartyEscape` holding (hazards
design 6.10; LG14 above). It changes nothing until the FL owner rules.

### 6.3 Sizes by depth *(illustr.)*

**Macro cells** are 12 squares with **per-cell sockets**: only
Portal-bearing cells need socket coverage, which fixes the toy's 0%
screen fit on floors 4–5. Walls are one square thick and lie inside a
node's footprint, so a footprint holds the room's interior plus one wall
square on each side (8.0). Room sizes below are interior walkable
squares. Fight-room long axis ≤ 18 and arena ≤ 21 stay as authoring
caps; fight rooms and arenas also meet the projected screen-fit bound of
8.1.

**Counting (lead's call, after the check):** Room nodes = n + k (side
rooms) + the Stairhead. The elite room and Circuit side rooms count as
side rooms; the Bypass arm is Portals only, never a Room node; Alcoves
and corridors are not rooms (FL-4).

| Floor (party) | n | Side rooms (k) | Alcoves | Room nodes | Walkable squares | Macro grid | Critical path | Fight rooms | Arena |
|---|---|---|---|---|---|---|---|---|---|
| F1 (solo) | 8 (7 + antechamber) | 2–3 | 1–3 | 11–12 | 1,100–1,400 | 5×6 cells | 100–140 | 9×10–11×13, ≥ 10 deep along the entry axis | 15×15–19×19 |
| F2 (duo) | 8 | 2–4 | 1–3 | 11–13 | 1,300–1,600 | 6×6 | 110–150 | 10×10–12×13, ≥ 10 deep | 15×17 |
| F3 (trio) | 8–9 (8 trimmed) | 2–4 (trimmed: 2 + a visible Alcove reward) | 2–4 | 11–14 (trimmed 11) | 1,450–1,800 (1,350–1,650) | 6×7 | 115–160 (105–140) | ≥ 11×11, up to 13×15 | 15×17 |
| F4 (3–4) | 8 (7 trimmed) | 2–4 (trimmed: 2 + a visible Alcove reward) | 2–3 | 11–13 (trimmed 10) | 1,550–1,850 (1,400–1,650) | 6×7 | 120–160 (100–130) | 11×13–13×16 | 17×19–21×21 |
| F5 (3–4) | 7 | 2–3 (trimmed: 2 + a visible Alcove reward) | 1–3 | 9–10 (no Stairhead) | 1,450–1,800 | 6×6 | 110–150 (90–125) | 11×13–13×16 | the bed, about 17×19 |

The trimmed floor-3 row serves RS 10.2's floor-3 extension
(`pacing.trimmedFloors` [3, 4, 5]) **(lead's call, after the check)**.

**Room roles:**

| Role | Size |
|---|---|
| Entry | 7×7–9×9 |
| Empty or special | 5×5–9×9 (7×7–9×11 at party 3–4) |
| Side room | 5×5–9×9 |
| Alcove | ≤ 5×5 |
| Antechamber (Forecourt), floor 1 | 5×9–7×11 |
| Antechamber (Forecourt), floors 2–5 | The floor's fight-room size, because it is the patrol's fight host (6.11) **(lead's call, after the check)** |
| Stairhead | about 5×5 |
| Elite room | 10×10–13×15, ≥ 10 deep |
| Dare room (M3; HZ-21) | ≤ 9×9; a side-room Site on floors 2–5, ≤ 1 Dare-or-Wager per floor (the slot shared with segment 5's Wager rooms; TL-29, M3; traps and locks design 6.15), never critical, signposted on its Portal (hazards design 6.10) |

**Free-space rule:** a fight room keeps ≥ 6 free squares per unit at
party size + the floor's unit cap + one 3-unit joiner group. That is why
floor-3 fight rooms are at least 11×11. It applies to the floors 2–5
Forecourt. Every terrain piece square is non-free (TR-19; room terrain
design 6.2.1, V9 `FreeSpaceAfterClutter`).

**Other bounds:**
- Corridors ≤ 30% of walkable squares (35–40% in the Silk and the Gut).
- Floor area scales with depth. The party band picks only the template
  size class. Hazards never scale with party size (count, footprint,
  band or period); their footprints scale with room size only, and
  their hazard points, quotas and `clock` and `void` reservation tags
  sit on top of segment 3's layers (HZ-2; hazards design 6.2).

**Trimmed floors stay inside the RS 6.6 bands.** The third reward becomes
a main-path-visible Alcove, so the content stays and the detour goes.
This is not a count change.

### 6.4 Corridors and doors

**Sockets (SK1; the definition is a lead's call after the check):**
- A socket is (cell, side, offset, width). It covers the squares offset
  to offset + width − 1 along that cell side; offsets are 3, 6 or 9 and
  widths 1–3.
- The edge's kind constraint fixes its width. LS6 and LS7 choose
  templates and orientations so both ends of every edge carry matching
  sockets.
- A fixed router lists every legal corridor route between the two
  sockets inside the edge's reserved corridor cells (axis-aligned, at
  most 2 bends, the edge's width), sorted by (length, bends, squares in
  row-major order). LS8 draws an index into that list. Length 0 arises
  only when the two sockets abut across a shared cell edge. V8 proves
  no list is empty.

**Corridors:**
- Length 0 (a doorway) to 12; at most 2 bends; axis-aligned.
- Width 2 by default; width 3 for galleries, halls, plazas and tubes.
  Width 1 only on tagged chokepoints and Squeezes, ≤ 4 long on any floor
  with followers. Floor-1 Kennel trails may run longer, because floor 1
  is solo, but reach a fight host only through a mouth ≤ 2 deep (LG20,
  7.3).
- Every corridor is a legal fight space **(decided, main 4.3)**.
- **`RoomMix`** (weapons 26): every floor has ≥ 1 open fight room
  ≥ 11×11 (Cleave, reach) and ≥ 1 guard post whose forward Portal is a
  door kind or ≤ 2 wide (Skewer, spears, Lightning Line). Terrain pieces
  are non-free, and the open room keeps a clear 5×5 core and a lane
  (3×6 *illustr.*) after clutter (TR-19; room terrain design 6.2.3, V9
  `RoomMixAfterClutter`).

**Doors:**
- Door-share bands per theme are in 7.1, measured after LS12 repair.
- Initial states **(lead's call; the exceptions are lead's calls after
  the check)**: Hinge and Postern doors, fight-room doors and every
  `forcedClosed` Door start closed, **except a Gaol `pairDoor`, which
  starts propped open** (6.6). Drapes start drawn and Latches latched.
  Other Doors draw from the theme's `closedShare`. Every Portal spends
  its state draw, fixed or not.

**`PortalMinimums` (structural; lead's call after the check):**
- From floor 2: every Blueprint carries ≥ 2 `forcedClosed` unlocked Door
  edges on the main chain or the Bypass (DEP-S1); on floor 1, ≥ 1. V8
  checks it, so the minimum holds whatever the draws and the runtime
  assertion follows from it.
- From M2: 2 Squeezes per floor via a dedicated Site. With segment 3's
  narrow ground this reaches armour 7.3's ≥ 3.
- From floor 2: a trap socket before the first fight, in room 0 or on the
  0→1 Portal, for Shade DEP-S1(a), beside the trap sockets within 6 of
  fight hosts **(lead's call, after the check)**. Segment 5 fills them
  (TP12: from floor 2, ≥ 2 main-path traps, ≥ 1 before the first fight,
  ≥ 1 within 6 of a fight host); the 0→1 Portal socket takes a Door
  catch or a threshold wire outside the Portal square (TP1; TL-20, M2;
  traps and locks design 6.20).

### 6.5 Slot placement (enemies 10.4 settled; realised by layout under FL-3)

**Legal main-chain role orders.** Codes (local to this table): E entry;
G guard post; Q empty or special; L lair; F Forecourt (antechamber);
B arena. Orders carry G only. **Gb** below marks the canonical Bypass
target, which LS5 draws from the Blueprint's `bypassTargets[]` (6.8).

| Floor | n | Legal orders (toy enumeration) | Canonical (Bypass target as Gb) |
|---|---|---|---|
| 1 | 8 | 2 | E G Gb Q L G F B, or E G Gb Q G L F B |
| 2 | 8 | 8 | E G Gb Q L G F B |
| 3 | 9 / 8 trimmed | 20 / builders enumerate | E G Gb Q G L Q F B / E G Gb Q L Q F B |
| 4 | 8 / 7 | 8 / 3 | E G Gb Q L G F B / E G Gb Q L F B |
| 5 | 7 / 7 | **1** / 3 | E G G G L F B (Bypass around the first or second G) / E Gb G Q L F B |

- "Gb before the lair" is dropped. Last Turning (6.8) may put Gb after
  the lair; `SkippablePool` is the real guard. The builders re-run the
  enumeration with that change.
- **On floor 5, in both forms, the Bypass never goes around the
  highest-index guard post,** which carries the Heart's `ledShape` (RS
  8.11 morale showcase). `bypassTargets[]` on Heart Blueprints never
  lists it **(new lead's call, extended to trimmed floor 5 after the
  check)**.
- **Told plainly:**
  - untrimmed floor 5 has one rhythm, and floor 1 two;
  - variety there comes from topology, signature rooms and templates,
    not from rhythm;
  - untrimmed floor 5 has no Q other than the antechamber, so segment 8
    gets no special room there, and the Circuit is the antechamber with
    one or two cover side rooms.

**Where each slot goes:**

| Slot | Host rule |
|---|---|
| Patrol | The Circuit (6.7). Floor 1's cowable Scavengers (CL-10) sit on main-path Q room 3. On floors 2–5 the patrol's homeRoom is the Forecourt, since enemies 6.3 homes a patrol in its route room with the highest `expectedTenths` and Circuit side rooms inherit the Forecourt's; the patrol is levelled and paid at room n−2 **(told plainly, after the check)** |
| Guard posts | Chokepoint rooms: one forward Portal that is a door kind or ≤ 2 wide, plus an entry side of at most a flank pair |
| Lair | The L position, a cut vertex. May be `deadEndAllowed` only for a Den or Sleeper template, where Cornered is the intended payoff |
| Elite room | A depth-1 spur from a main-path room with index ≥ FloorDiv(n−1, 2), not the antechamber or arena (floor 1: second half, enemies 17.4). Entered by a Door. Its **Grille vantage** sits in a vantage Alcove of the parent room, behind a Door the party opens only when it wants to look, so a peek is always a choice; from it the party sees ≥ half of the elite start zone at distance ≥ 7, and the two sides are walkably joined within ≤ 12 (`GrilleReach`). No square on the parent's main walk line sees the start zone. **If the enemies owner refuses FL-21 (b),** a noticed Champion may come, so the hearing outline marks the vantage Door as a noticing risk before the party opens it; the opt-in Door is the layout fallback and needs no enemies rule **(vantage Alcove and fallback: lead's calls, after the check)**. Templates also fit the floor-3+ Rival Party (2–3 units, enemies 18.1). Inherits `expectedTenths` through `branchIndex` |
| Boss | Arena socket: the Blueprint's 2×2-cell `arenaCell`; two dog-leg Forecourt Arches; the Bossgate on the far wall (none on floor 5). Arena start zones sit ≥ 9 path squares from both entrances (segment 9 hook) |
| Empty or special | Q positions, and the antechamber on floors 2–5, which is also the patrol's fight host and homeRoom (RS 6.6's "empty or special" reads it as special; told plainly, FL-5). Segment 8 fills Q rooms; floors 2–5 antechambers may host non-encounter micro-events |
| Side rewards | Loop-back Sites first, then spurs and Alcoves. Off-path potions (600‰ lean) and secret caches |
| HE-7 / HE-4 | Socket 1: a no-check socket in a traversed room with index < FloorDiv(n−1, 2), not in the Gb room. Socket 2: a traversed room with FloorDiv(n−1, 2) ≤ index ≤ n−2 (the antechamber allowed; on floor 1 "empty" means no encounter, so a container is legal). HE-4: a traversed no-check socket before the arena, not in the Gb room and not in a Circuit room (the floors 2–5 Forecourt included), so no optional fight stands in front of it. Entry templates, every Q template and **every Forecourt template** carry ≥ 1 no-check socket, so reservation is constructive on every role order, floor 5 included (V8 `HE7Sockets`). DH 12.5 step 4 draws the supply among these HE-4-eligible containers only (FL-3) **(lead's calls, after the check)** |
| Trader (Later), Pledge (Later) | A `traderEligible` / `pledgeEligible` loop-back side room on floors 2–4, not on the Circuit, not the elite room |
| Keepsake slots | Numbered from 0: main path by index, then side rooms by `branchIndex` (3.4), then `roomId` (RS 9.3) |
| Signature room | Each Blueprint authors one theme set piece unique to that Blueprint (e.g. the Gaol's Cell Zero door, the Heart's music-box clearing). It can host any role. `SignatureRoomRepeatRate` report |

### 6.6 Hearing (change FL-11)

**Graphs:**
- **Hearing graph:** every walkable connection, including Squeezes. Not
  Grilles, the Bossgate, or unfound secrets.
- **Movement graph (monsters):** excludes Squeeze, Locked, unfound
  secret, unlifted Latch, Grille and Bossgate.
- Joiner arrival and footsteps timing use the movement path; noise reach
  uses the hearing path.
- **At runtime, noise and pathing read the live graphs** (each Portal's
  current state). The static matrices are build-check, report and
  hearing-outline inputs only, recomputed on `SecretFound` and
  `LatchLifted` **(lead's call, after the check)**.
- **Terrain (TR-17):** the static movement matrices, joiner timing,
  `HearingGap`, Circuit isolation, `AntechamberScreened` and the Grille
  vantage stay proven on walls and Portals only. Terrain pieces only add
  blocking and no proof relies on one; hearing ignores pieces (room
  terrain design 6.2.1).
- **Landmark ticks (M3; HZ-19; told plainly):** the Small Hour Strikes'
  mute round stops party-verb noise from propagating until the end of
  the next round; combat noise and the graphs are unaffected (hazards
  design 7.2).

**Static conventions:**
- `hearingState(k)`: for a fight in main-path room k, main-path Doors with
  index < k are open. **Proofs** (`HearingGap`, isolation, near pairs)
  also count every Door that is not `forcedClosed` as open, whatever its
  drawn state, so they hold on every seed; `forcedClosed` Doors at index
  ≥ k count closed, and a Gaol `pairDoor` counts closed (below)
  **(lead's call, after the check)**.
- Circuit checks treat Circuit-internal Doors as open, because the
  patrol opens them and never closes them.
- A generated-state variant and an all-doors-open variant are reported.

**`HearingGap`:**
- The effective path (+3 per closed door-like Portal) from any square of
  slot home A to the start zone of slot home B must be **> 8** for every
  pair of slot homes, under the proof state above. This is derived from
  the 10.1 bands under the 10.4 cluster budget: every pair fails the
  budget if they hear each other.
- Proven at build time with authored worst-case socket-to-start-zone
  bounds per template class (11.2).
- LS12 repair upgrades Portals without draws.
- Enemies 10.5 step 9 stays as the backstop **(settled)**, restricted to
  moves within a slot host or to an equivalent host (6.1).

**Near pairs:**
- The theme's authored joiner set pieces (the Kennel horn, the Carnival
  plaza, the Silk stacks, the Gut's resonance).
- A fight host and a neighbouring group at **effective** path **9–10**
  under the proof state: outside combat radius 8, inside the loud-trap /
  shattered-door radius 10 (enemies 12.3).
- A horn call, a shattered door, a loud trap, or a fight that drifts
  toward the pair pulls them. The M2 hearing outline shows the risk.
- Legal under the cluster budget as written; no change.
- Caps: floor 1 ≤ 1 and none at index ≤ 3 (`nearPairMinIndex[1]` = 4);
  floor 2 ≤ 1; floors 3–5 ≤ 2; the Silk and the Carnival +1. Each
  grammar's `nearPairsMax[floor]` lies inside `layout.nearPairsMax[floor]`
  (V8).
- **Pending requests (EP-22, M2; encounter placement design 6.12):**
  near-pair partners at the far end (10) of the 9–10 band, behind a
  closed door where layout allows; and, only if `ChainPullRate` still
  misses after segment 6's own levers, dropping the Silk and Carnival
  +1 on floors 4–5 or the floors 3–5 cap. Not rules until the FL owner
  rules.
- A Dormant-partner variant (half radius) is also allowed.

**Gaol shut-door pairs (the enemies owner's call):**
- A guard post and a neighbour at open path 6–8 through one `pairDoor`,
  which starts propped open (the one exception to 6.4's closed fight-room
  doors).
- The static check counts the `pairDoor` as closed (6 + 3 ≥ 9), on the
  reading that the hearing outline shows the pull before entry and the
  party closes it out of combat.
- On floor 1 the Gaol has 1 shut-door pair, or, under the fallback, 1
  near pair; never both, so floor 1's cap of 1 holds.
- **Told plainly:** this loosens the static cluster check. A party that
  ignores it gets one joiner (cap 1). Its footsteps warning comes ≥ 1
  round ahead, so DH 6.4's floor-1 round-1 death bound is unaffected.
- **Fallback if refused:** the Gaol uses its 1 near pair, and shutting
  the door protects against loud noise.

**`ChainPullRate` target:** 0.3–0.6 joins per floor *(illustr.)*, not
zero. Joiners stay a real, player-caused experience.

### 6.7 The Circuit (change FL-10)

- A cycle through **2–4 Room nodes** (corridors do not count), or 2 rooms
  joined by 2 distinct Portals. A lap is 30–60 squares.
- Never the entry, the arena or rooms 0–2. On floor 1 never the
  antechamber; on floors 2–5 **always** the antechamber **(agreed, Q2)**.
- Includes ≥ 1 `coverRoom` (segment 3 gives it cover) and no fight host
  other than the patrol's own.
- **Pending request (EP-17, M2; encounter placement design 6.9):** a
  **cover-room window**: the Circuit `coverRoom` holds ≥ 2 Hide spots
  (RT KP4) with line of sight to a lane square within 4, out of the
  lane's sight from the previous waypoint; on floors 2–5 in a Circuit
  side room, never the Forecourt. Segment 6's V12 checks it (failure
  action: the next Circuit side room, else the floor is logged and the
  patrol is a Picket); not a layout rule until the FL owner rules.
- Floor 1: includes main-path Q room 3 (CL-10).
- **Floors 2–5: the Circuit lives in the post-lair tail (told plainly,
  after the check).** It must hold the Forecourt and no other fight host,
  the lair is a cut vertex, and only one fight is route-avoidable, so no
  lap can reach back past the lair or room n−3 when that room is a G.
  The Circuit is the Forecourt with one or two Circuit side rooms (LG5),
  plus room n−3 when it is a Q. Every room cleared before the lair is
  patrol-free; the rest consequence is in 6.8.
- Layout emits waypoints and a start phase that keeps the first 20 ticks
  outside rooms 0–2 (DH-28). The patrol's waypoints and pauses in the
  Forecourt sit on its far side from the arena Arches **(lead's call,
  after the check)**. The patrol's marching order on the route is
  segment 6's Patrol File (EP-18, a reading; M1; encounter placement
  design 6.9).
- **Isolation:** for each fight host k, under `hearingState(k)` and the
  proof state of 6.6, every Circuit square is at effective path > 8 from
  that host's start zone, the arena's included. This needs at least one
  corridor cell, or ≥ 6 squares plus a `forcedClosed` door, between a
  Circuit room and a fight host. The dog-leg Arches (6.11) keep every
  Circuit square out of sight of arena squares more than 2 beyond an
  entrance **(sight clause: lead's call, after the check)**.
- Without isolation, the toy's patrol sat within hearing of a fight host
  on 80–100% of floors and cost about 1 minute at party 4.
- The Circuit may pass a fight host's closed door at path 6–8 (+3 =
  9–11) only if that door is `forcedClosed`, is not the host's party-entry
  Portal and has no main-path index < k (a side or flank door). If the
  party opens it mid-fight, the outline shows the patrol risk.
- On Last Turning floors (6.8), the Bypass arm rejoins at the Forecourt
  or passes through a Circuit side room, so taking the stealth route means
  meeting the patrol's route. Elsewhere the Bypass and the Circuit do not
  meet, because the Circuit lives in the tail **(narrowed after the
  check)**.

### 6.8 Stealth, flee and retreat (change FL-9, FL-12)

**Bypass:**
- One cycle whose arm leaves room Gb−1 and rejoins at Gb+1. Never
  secret. The arm is Portals only (corridors, Doors, a Squeeze), never a
  Room node; its squares belong to room Gb−1 and carry no
  `mainPathIndex` (FL-13).
- Floor 1: an ungated quiet corridor with ≥ 1 closed Door, wholly out of
  line of sight of Gb's start zone. Any solo class can use it.
- Floors 2–5: a Squeeze or a quiet two-door corridor (the Blueprint
  chooses), so heavy-armour parties keep a route. The arm crosses Gb's
  line of sight once, in a window of ≤ 3 squares at distance 7–10. First
  sight is a noticing trigger, so Stealth matters there (enemies 12.2).
- Every arm square is at effective hearing ≥ 7 from Gb's start zone, so
  a failed Squeeze (noise 4, from the mouth square nearest the party)
  never reaches the guard.
- Arm length ≤ 1.5 × the replaced route + 10.

**Last Turning:**
- The Bypass target is drawn in LS5 over the Blueprint's
  `bypassTargets[]`, integer per-mille weights. The weight on the last
  guard post before the antechamber is the grammar's `lastTurningWeight`
  (300‰ *(illustr.)* on floors 2–4), capped by `layout.lastTurningWeight`
  **(per-grammar weight and draw: lead's calls, after the check)**.
- Weight 0 on floor 1 (the floor-1 antechamber has one inbound Portal)
  and on the Heart (its highest-index guard post is the morale showcase,
  RS 8.11).
- An arm landing in the Forecourt counts toward LG5's two inbound main
  Portals.

**`SkippablePool`:**
- **Structural form (proven by V8, asserted at runtime):** on the
  movement graph with Squeezes, picked Locked Portals and found secrets
  traversable and unlifted Latches blocked, every main-chain fight host
  except exactly one guard post (Gb) is a cut vertex between the entry
  and the Forecourt. The elite room and the patrol are excluded by
  definition.
- **Experience reading:** a guard post's share is 100 (the RS 6.6 /
  enemies 20.1 table: 710 → 610), so every entry→antechamber path passes
  ≥ `preBossPool(floor, trimmed)` − 100: 410 standard, 310 on trimmed
  floor 4, 410 on trimmed floor 5 and, on trimmed floor 3, the enemies
  owner's preBossPool − 100 (310 *(illustr.)*). The boss and the
  optional elite are excluded.
- The patrol counts as present: it can be dodged by timing, not by
  route.
- **Told plainly:** a party that takes the Bypass **and** times the
  patrol reaches 540 counting the boss (100%), below the 20.1 band.
  `MultiBypassRate` is reported and flagged to the enemies owner
  (FL-21).
- **Told plainly (after the check):** on floors 2–5 the patrol's lap runs
  through the Forecourt, so a party that times its way past it usually
  meets it as an ordinary joiner in the boss fight (enemies 17.5 allows
  ordinary joiners there; the joiner cap applies). `BossFightJoinRate` is
  reported, the simulator's boss arm includes the case, and FL-21 (e)
  asks the enemies owner to tune the boss with it.

**Party flight:**
- `PartyEscape`: a line-of-sight break (closable door or bend) within 6
  squares of every fight host's entry.
- The cleared room behind is always a retreat. This serves the settled
  1–3-round escape valve (enemies 12.5).

**Monster flight:**
- Layout ranks anchors per (fight host, party-entry Portal): rooms
  reachable without crossing that Portal, through **non-door** Portals
  first.
- Rooms that are the homeRoom of an un-engaged group are excluded unless
  nothing else qualifies. Side rooms and Circuit rooms are preferred, so
  routers do not run forward into the next fight.
- Runtime: the first anchor the party has not explored, else the
  farthest, else Cornered (enemies 14.5's "nearest" anchor is replaced by
  this ranking, FL-12; Cornered's definition is unchanged).
- Every non-elite fight host has a non-door flee exit (LG12). RC3
  ("never moves a unit through a door") therefore stays literal, with
  "door" read as door kinds (3.4; told plainly).
- **Deferred (EP-35, M3; encounter placement design 9.1):** segment 6's
  Messenger (a skittish runner who runs for help) would need a flagged
  exception to this anchor ranking; nothing changes until it is taken
  up.
- `CorneredRate` and `FrightenFleeSuccess` are reported by theme. If the
  Gaol or the Heart runs more than 15 points below the median, add flee
  exits there.

**Rests:**
- Layout designates no safe room and keeps none patrol-free by rule.
- Floors 2–5 antechambers are always on the Circuit **(agreed, Q2)**.
- `NoSealedPocket` (LG14) means no Squeeze-only or Grille-only space can
  shelter a rest from patrols.
- **Dare rooms (M3; HZ-21):** an unguarded Dare room's Stir runs per
  out-of-combat tick (C 5.5) with auto-walk off inside; no short rest
  inside the room; a taker reduced to 1 HP is returned to the threshold
  and the dare resets; two attempts per floor, then the container seals
  for the floor (anti-brute-force; hazards design 6.10).
- **Told plainly (after the check):**
  - On floors 2–5 the patrol lives in the post-lair tail (6.7), so every
    room cleared before the lair is patrol-free and restable. DH counts
    patrol risk as what holds rests down, so the rests gate (median ≤ 3
    per floor) is **likely to fire**. The DH owner is handed that
    forecast now, so the DH 5.7 lever order is queued rather than found
    by the report: found supply 1 per floor first; limited vendor supply
    stock back to the client; supply cap 2 last.
  - The Forecourt is never guaranteed safe while the patrol lives. Once
    the party has defeated the patrol, the Forecourt is as restable as
    any cleared room, one step from the arena (the reading of Q2
    confirmed by the client on 2026-11-21, 19.1). `PreBossRestSafety` is
    therefore a **gated report**: if parties rest in a cleared Forecourt
    on more than 50% *(illustr.)* of floors 2–5, it comes back to the
    client with the options in 19.2.
- `RestSpotPatrolCoverage` and the median rests per floor are handed to
  the DH owner with the forecast above.

### 6.9 Backtracking

- **Definitions (lead's calls, after the check):**
  - The **critical path** is the shortest movement-graph path from the
    entry start square to the nearer arena Arch.
  - The **full-clear tour** is the walker's deterministic
    nearest-unvisited-room tour (11.4) visiting every Room node and
    Alcove reachable without Locked or unfound secret Portals, ending at
    the arena Arch. The Stairhead is excluded (it follows the boss).
- `BacktrackBound`: full-clear tour ≤ 1.7 × critical path as the
  *target* median, computed over the V8 enumeration per (theme, floor,
  party band). The build fails at a median of 1.9 or a p90 of 2.2.
- The structural fixes are the loop-back Sites (LG10) and a Latch at the
  end of the one deep spur.
- `EmptyDeadEndRate` target 0: every leaf holds a container, a cache or
  a set piece.
- Detours are legible before commitment (floor count line, Listen,
  containers seen through Grilles and Arches).
- Travel-to-explored-square auto-walk is requested of segment 10 **in
  M2**: 1 tick per step, stopping on any sighting, noticing or
  footsteps, and at doors as 6.10 says.
- The post-boss walk is 60–97 squares (11–17 s) on the toy. The Way Back
  Latch is built in M3 and off by default (`PostBossBacktrack`
  decides).
- **Pending request (HZ-23; M3):** Plunge, a one-way out-of-combat link
  forward (a Chute skin), the only hazard that saves clock (hazards
  design 6.10). Nothing here changes until the FL owner rules.

### 6.10 Clock (change FL-16)

- Walking is under 40 s per floor. Clicks, containers, reveals and rests
  dominate.
- **Layout commitments at party 4 on floors 4–5**, measured by the
  walker arm defined in 11.4 (median over the release seed set, per
  theme and floor):
  - `ExploreSeconds` ≤ 3.5 min;
  - `ExploreClicks` ≤ 40;
  - Circuit isolation.
- **Segment 5's allowance (TL-38, M2; told plainly; traps and locks
  design 6.21):** a new budget line of ≤ +0.3 min `ExploreSeconds` and
  ≤ +5 `ExploreClicks` per floor at party 4 on floors 4–5, reported
  separately on top of the commitments above, which stay for layout,
  terrain and hazard content. The floors 4–5 party-4 total may reach
  3.8 min and 45 clicks for segment-5 content only; the whole floor
  still answers to the companions floor-time gate, with segment 5's
  binding cut order on a miss.
- **Segment 6's line (EP-33, M2; told plainly; encounter placement
  design 6.18):** beside TL-38's, ≤ +0.2 rounds per fight on average
  and no net minutes at party 4 on floors 4–5 *(illustr.)*; activities,
  chips and the Read card add 0 clicks, Listen costs are unchanged and
  Hold the Hide is 1 click. On a miss, segment 6's cut order applies
  before release: (1) no Crossfire or Kill Box on floors 4–5 guard
  posts at party 4; (2) `maxRounds` 3 → 2; (3) halve the Waylay share;
  (4) Revel and Bout start in Posted formation. The companions 7.4
  levers stay untouched.
- **Auto-walk at doors (lead's call, after the check):** auto-walk
  pauses at each closed door on its path. A trained party's passive
  Listen resolves there (FL-18). Auto-walk then opens the door and walks
  on, unless a group was heard, the party itself closed that door, or
  the far side holds a known un-engaged group; in those cases it stops.
  Each stop counts in `ExploreClicks`.
- **Pending request (EP-17, M2; encounter placement design 6.9): Hold
  the Hide** as an auto-walk entry: in the cover-room window only, once
  per floor, 1 click; ticks pass until a patrol unit enters a party
  unit's line of sight, any noticing roll, any group turns Alerted, or
  20 ticks *(illustr.)*; the ticks count toward the clock, not as a
  rest. Not a rule until the FL and ability 7.2 owners rule.
- **Auto-walk and traps (TL-7, M2; traps and locks design 6.4):**
  detection runs after every square of party movement, and a new
  Revealed or Hunch result interrupts auto-walk. Auto-walk stops on the
  last square before a new Hunch zone; out of combat it auto-Probes at 0
  clicks unless the patrol forecast shows a crossing within 2 ticks. It
  defaults to walk-round (traps and locks design 6.21).
- Trimmed floors use 2 side rooms plus a visible Alcove reward, no
  Overlook and no second Latch. Door share on floors 4–5 themes ≤ 0.5.
- `FloorTimeByTheme` normaliser: each grammar's walker exploration time
  (11.4) sits within ±0.5 min of the floors 2–5 median (RS 11.4 +10%
  hold).
- **No layout lever** is added to the companions 7.4 lever order
  **(agreed Q2 levers untouched)**.

### 6.11 The Sanctum (agreed, Q2; change FL-5, FL-6)

The Q2 answer gathered in one place; every clause is also an invariant
or a slot rule above.

| Piece | Rule | Floors |
|---|---|---|
| **Forecourt** (antechamber) | Main-path room n−2, always a Room node, never a corridor **(new lead's call)**; every path to the arena passes through it (LG3); themed per Blueprint | All |
| Floor-1 Forecourt | Empty (no encounter), 5×9–7×11, one inbound Portal, no side room, no Alcove, off the Circuit; near-safe by RS 9.4's approved exception; hosts the `AntechamberCatchUp` (RS 9.4) | 1 |
| Floors 2–5 Forecourt | Counted as an empty or special room; 1–2 inbound main Portals; **always a Circuit waypoint** and the patrol's homeRoom and fight host, never guaranteed safe while the patrol lives; at most two Circuit side rooms attach, never touching the arena; the floor's fight-room size, with the free-space rule, LG12 flee exits and LG20 entry rules; may host a segment-8 micro-event **(size and fight-host rules: lead's calls, after the check)** | 2–5 |
| **Arena entrances** | Two **dog-leg** Arches from the Forecourt, ≥ 5 apart, never Doors; arena start zones ≥ 9 path squares from both. **`AntechamberScreened`, symmetric:** no Forecourt square more than 2 beyond an entrance has line of sight to any arena square more than 2 beyond that entrance, in either direction; a baffle or bend inside each Arch does it. The boss can therefore be shot from the Forecourt only while it stands in its own doorway band, where its reach covers the Arch **(symmetric form: lead's call, after the check)** | All |
| **Bossgate** | On the arena's far wall; blocks movement and sight and is not a hearing path until `BossDefeated`, then an Arch. Not an entrance: "the exits are never sealed" refers to the entrances (enemies 17.5, clarified) | 1–4 |
| **Stairhead** | About 5×5 behind the Bossgate; the stairs, the left-behind warning and Scrap (items 9, 24); the floor fact "path to the stairs" means the Stairhead (enemies 14.6) | 1–4 |
| Floor 5 | No Stairhead and no Bossgate: the run ends; a waking-light window dresses the far wall (display only, M3 seams). The floor fact "path to the stairs" reads as the main path to the arena | 5 |

**Catch-up trigger, read with FL-13 (new lead's call):** RS 9.4's
trigger (a) fires on squares whose `roomId` is the Forecourt; the inbound
Portal's squares belong to room n−3 and the arena-adjacent doorway
squares belong to the arena, so trigger (b) ("any attempt to enter an
arena square") covers the arena doorways. RS 3.4's glossary wording
"room or corridor" becomes "room" (FL-5).

---

## 7. Per-theme layout grammars (change FL-14)

`layoutArchetype` becomes `layoutGrammar` (RS 8.0). The partitioning of
open-plan themes into ordinary rooms is **agreed, Q1** (FL-1). **Common
to every theme:**
- LG1–LG20.
- 4–6 Blueprints per theme in M2. **Floor-1 themes and the Heart get
  6–8**, because they are seen every run.
- One Blueprint per theme in the Precinct family, where it suits the
  theme.
- A signature room per Blueprint.
- A `secretRange[floor]` per theme within `layout.secrets[floor]` (9.2):
  floor 1 0–1 in the Gaol and the Wake, 0 in the Kennel **(agreed, Q3)**.
- A `lastTurningWeight` and `nearPairsMax[floor]` per theme within their
  configuration bounds (6.6, 6.8).
- On floors 2–5 the Circuit is the Forecourt ring of 6.7; each theme
  below skins it.

### 7.1 Summary

| Theme (floor) | `topologyClass` | Door share *(illustr.)* | Open-room share | Mean corridor | Cycles | Near pairs | Compass toward the boss |
|---|---|---|---|---|---|---|---|
| Gaol (F1) | grid | 0.75–0.85 | 10% | 2 | 3–4 | 1 shut-door pair (or 1 near pair under the 6.6 fallback); none on hooded-cot seeds (EP-13, M2; encounter placement design 6.17) | The Landmark clock of each room shows a later hour along the main path |
| Kennel (F1; M1 subset) | beads | 0.10–0.20 | 50% | 8 | 2 | 1 horn | The painted moon over the lodge; shadows point to it |
| Wake (F1, conditional) | courtyard | 0.45–0.60 incl. Drapes | 20% | 4 | 2 | 0 | Sconces lit along the main path |
| Choir (F2–3) | comb | 0.33–0.43 | 30% | 1 | 2–3 | 1 | The water line rises toward the quire |
| Silk (F2–3) | lattice | 0.20–0.30 | 15% | 8 (aisles) | 3–5 | 1–2 (+1) | Lamp-gold main aisle |
| Carnival (F2–3) | hubRim | 0.20–0.30 + flap Drapes | 50% | 3 | 2–3 | 1 (+1) | Lantern strings converge on the big top |
| Supper (F4; F3 from M3) | axis | 0.45–0.50 | 5% | 3 | 2–3 | 0 | Place-card numbers count down to the empty chair |
| Gut (F4) | braid | 0.25–0.35 | 35% | 7 | 3–5 | 1 | Wall ridges lean with the flow; the heartbeat quickens |
| Heart (F5) | spiral | 0.20–0.30 + Drapes | 40% | 2 | 1–2 | 0 | The bed and canopy, always in view |

**`LayoutDistinct` (computable form: lead's call, after the check):**
- Every pair that shares a pool or can sit on adjacent floors differs in
  `topologyClass` and in ≥ 2 metric bins.
- **Bins (global):** door share in steps of 0.15 (0–0.15, 0.15–0.30,
  0.30–0.45, 0.45–0.60, 0.60–0.75, 0.75+); open-room share 0–15%,
  16–35%, 36%+; mean corridor 0–2, 3–5, 6+; cycles 1–2, 3–4, 5+.
- Each grammar's metric is its median over the V8-enumerated floors; two
  grammars **differ** in a metric when their medians fall in different
  bins.
- An **open room** is a Room node whose Portals are all Arches or
  Drapes.
- It feeds RS 11.4 `DistinctnessCheck` (+1 for a shared class).
- **The Choir (floor 3) and the Supper (floor 4) are adjacent in M2**, so
  the check applies to them now: they differ in door share, open-room
  share and mean corridor (7.8). From M3 the Supper can also sit on
  floor 3, beside the Choir in the same pool; the same bins apply.

**`TraversalFeel` report:** corridor share, open-room share, door
clicks, mean straight run (maximal axis-aligned segments on the
critical path), bends per 20 squares. The Kennel and the Gut use 1×2 or
L-shaped footprints in ≥ 50% of rooms, so organic themes do not read as
boxes; V8 checks that share.

### 7.2 The Gaol of Small Hours (F1): `grid`

**Macro shape.** A prison tower lying on its side: parallel cell-block
wings joined through junction guard rooms. Blueprints:
- Two Wings (H)
- Three Wings (E)
- The Yard
- The Stairwell Tube
- Cell Zero Wing (signature)
- a Precinct variant: outer block → inner block → the warden's clock
  office

**Rooms:**
- Admissions (entry, 7×9, HE-7 socket).
- Cell galleries (5–7 × 12–16) with 2–6 cell **Alcoves**. Each cell
  front is a clock-face cell Door set in a Grille panel (a Locked Door
  for padlocked caches): the Door is the Alcove's Portal and the Grille
  panel only gives sight in. Cells hold caches, Hide spots and, at
  index ≥ 3, Penitent cots. Cell Doors count in the Gaol door share
  **(lead's call, after the check)**.
- Junction guard rooms (9×9–11×11, chokepoints).
- At most 1 yard (13×13–15×15, lair or elite).
- Records or chapel (Q); the Last Hour (Forecourt).

**Law in layout (Lockdown):**
- Consecutive fight rooms are usually 2 closed doors apart (−6). Short
  corridors pay back the door-opening time.
- One shut-door pair (or one near pair; 6.6) makes the `signatureFeature`
  playable with the close verb. **Told plainly:** shutting mutes and
  delays; it does not cut a path unless locked (segment 5). **On
  hooded-cot seeds (EP-13, M2; told plainly; encounter placement design
  6.17)** the floor has no shut-door pair, and the Law shows through the
  door network and the bell tripwires.
- The keyring cell cache sits within 1 room of the Turnkey's homeRoom;
  it never gates the main path.
- Tripwire-eligible thresholds on the Circuit and the Bypass go to
  segment 5. **Answered (TL-20; told plainly):** on floor 1 the Gaol's
  Circuit thresholds take no trap; the request is met only by Bypass
  alarms (TP7) (traps and locks design 6.19).
- Cowed inmates kneel at Grille squares (RC3).
- Secrets: 0–1 on floor 1 **(agreed, Q3)**.
- **The compass clock:** one designated Landmark clock per room, larger
  and lit, whose hour advances along the main path. Every other clock
  shows a random hour from the display stream (RS 8.3) **(lead's call,
  after the check)**.

**Floor-1 lesson ladder** (`F1LessonLadder`: one primary lesson per
room, no repeats), written by role so both floor-1 orders pass it (V8
checks both) **(lead's call, after the check)**:

| Room | Lesson |
|---|---|
| 1 (the first G) | Doors isolate |
| 2 (Gb) | The quiet bypass |
| 3 (Q) | A padlocked cache seen through a Grille (keys) |
| The guard post adjacent to the lair | The shut-door pair **or** the hooded cot, one per seed (EP-13, M2; encounter placement design 6.17) |
| The lair | The lair |

The shut-door pair is self-teaching: its door starts propped, and the
hearing outline shows the pull before entry (segment-10 hook).

**Risks:**
- Grid sameness (2 block shapes per Blueprint, the yard, clocks).
- Door clicks (auto-open on path; counted in `ExploreClicks`).
- Painted bars on solid wall would lie (RC1), so bars appear only on
  Grilles (8.1).
- Padlocks only (RC3, as amended by FL-7).
- The 5/4 ticking never masks door cues (RC7).

### 7.3 The Kennel Wood (F1; M1 subset): `beads`

**Macro shape.** Glades (pre-baked mask variants in authored bounds,
long axis ≤ 18) strung on trails around an impassable,
line-of-sight-blocking thicket. Partitioned into rooms in the rules, read
as one wood on screen **(agreed, Q1)**. The lodge interior is the
Forecourt cluster. Blueprints:
- Hunting Ring (M1)
- Lodge Grounds (M1)
- The Ride
- Twin Coverts
- The Drive
- (M2) a Precinct variant: hunt's edge → lodge grounds

**Law in layout (the pack and the horn):**
- Every glade has ≥ 2 trail mouths. One may be 1 wide, so the solo hero
  can meet a pack one at a time. That mouth is a tagged chokepoint, at
  most 2 squares deep, opening onto the LG20 3×3 landing, and the
  glade's other mouth reaches the back of its trail within the pack's
  pursuit window (a flank pair of ≤ 12), so the hold costs something.
  The doorway-hold arm runs on the Kennel at floor-1 solo **(lead's
  call, after the check)**.
- Leader start squares are visible from the entry mouth at ≥ 7.
- One horn near pair at index ≥ 4 (6.6).
- Trails 6–12 long *are* the hearing spacing.
- Every trail bends within 6 of a glade mouth (`PartyEscape`).
- Routed hounds flee visibly down a far trail.
- Bypass: a parallel trail with a boarded-hurdle Door (M1), or a
  deer-run Squeeze (M2).
- Circuit: the ring trail through a fallen-log cover glade (floor 1).
- No secrets on floor 1 **(agreed, Q3)**.

**Risks:**
- **Told plainly:** open glades on the solo floor raise ranged dominance
  while there is no cover rule (M1; the M2 cover rule is room terrain
  design 6.1, TR-2). The thicket and trail bends give
  line-of-sight breaks, and the per-grammar `DamageShareByRangeBand` arm
  runs with segment 3 with the cover arm (room terrain design 11.4).
- Mask variants snap to the grid with no single-square notches; the edge
  tile set covers exactly the allowed configurations (8.2).
- No walkable hunting stands (RC1).

### 7.4 The Endless Wake (F1, conditional): `courtyard`

**Macro shape.** Manor galleries ring a dead courtyard; a crypt wing lies
toward the boss, "below" in architecture only (lowering vaults and floor
material on a flat floor, 8.1). Blueprints:
- Manor Court
- Two Wings and a Chapel
- The Long Gallery
- The Undercroft

**Law in layout (the sleeping house):**
- Niche Alcoves (8–14) grow denser toward the crypt.
- No niche within hearing of rooms 0–2: layout emits `nearStart`, which
  makes the RS 8.5 placement rule geometric.
- Coffin tells are visible at ≥ 7 on approach.
- Drapes are the drawn curtains.
- The servants' passage is the Bypass; the gallery ring is the Circuit
  (floor 1).
- Secrets: 0–1 on floor 1 **(agreed, Q3)**. The theme dressing beside
  the universal tell (lead's call): the one portrait whose eyes are
  open. It is dressing only; the universal glyph is the rule.

**Risks:**
- Upright coffin lids mimicking the Pallbearer prop (RC2) are forbidden.
- Sealed coffins must read as unable to hold a unit.

### 7.5 The Drowned Choir (F2–3): `comb`

**Macro shape.** A nave of bays (the main path, 11–13 × 12–16, Arches
between bays) with a parallel side aisle (the Bypass) and chapel teeth
(side rooms). The Circuit is the quire ambulatory: the Forecourt (the
quire screen) and one or two chapels behind it **(Circuit placement
restated after the check, 6.7)**. Blueprints:
- The Nave
- Cruciform
- Twin Naves
- Drowned Cloister
- Quire and Crypt (suits n = 9)

**Law in layout (the drowning line):**
- Per-room wetness bands (dry / wet edge / flooded) for segment 3.
- Every fight room's party entry opens from a dry edge, which supports
  RS 8.6's entry-square and dry-island caps.
- Sluice gates are Door skins between wet and dry (segment 5 owns the
  Soak). A sluice *wheel* is a hazard piece (a Cord), not this Door skin
  **(told plainly; HZ-1, hazards design 6.1)**.

**Risks:**
- No wet sheen on dry squares (RC1).
- Dais lips are floor material and value only, never a step (8.1), and
  sit outside fight footprints.

### 7.6 The Silk Archive (F2–3): `lattice`

**Macro shape.** Full-height stack blocks carved by axis-aligned 1–2-wide
aisles; reading rooms (9×11–13×15) are the fight hosts. The Circuit is
the catalogue ring: the Forecourt and one or two stack rooms behind it
(6.7). Blueprints:
- Stack Hall
- Scriptorium Court
- The Carrels
- Catalogue Cross
- The Missing Page (deep set)

**Law in layout (webs and silence):**
- Web-eligible aisle mouths.
- Long straight aisles for Skewer and reach. Each reading room also has
  a dogleg entry.
- Near pairs at the top of the band (**told plainly:** "loud parties
  pay", within joiner caps).
- Shelf-gap Squeezes, built with the Squeeze kit read so they never look
  like the 1-wide aisles beside them (8.1).
- At most 2 maze rooms per floor, never consecutive, never on the
  critical path when the Bypass also runs through aisles.
- Every aisle longer than 8 ends at a Landmark.
- Width-1 aisles on the main path ≤ 4 long.

**Risks:**
- `CutawayPockets` behind stacks.
- Fixed library ladders must never look climbable.

### 7.7 The Carnival of the Last Evening (F2–3): `hubRim`

**Macro shape.** Entry gate → midway → central plaza (13×13–16×16) →
spokes → a rim of tents and wagons; the big top (arena) sits on the far
rim behind a backstage Forecourt. The plaza is partitioned into 2–3
rooms by full-height wagon and booth rows around a central Arch (8.1),
and reads as one fairground on screen **(agreed, Q1)**. Blueprints:
- The Midway
- The Ring
- Two Rings
- Fairground Gate
- Under the Big Top (deep set)

**Law in layout (oil and fire both ways):**
- Open rooms carry slick and fire-pit region tags. Party entries open on
  stall-edge tags (RS 8.8 caps).
- Stage sockets for trapdoors and hazard edges; stages are floor
  material only (8.1).
- Wagon and booth Doors meet DEP-S1; tent flaps are Drapes (hearing 0).
- The back lot is the Bypass; the rim is a flank route. The Circuit is
  the backstage ring: the Forecourt and one or two dressing-wagon rooms
  behind it **(restated after the check, 6.7)**.
- One plaza near pair.

**Risks:**
- Saturated fire hue only on fire squares (RC4).
- Open plazas need wagon cover (segment 3).
- Carousel horses always on poles. **M3 (HZ-18):** the letter holds; the
  Turning Ring turns boards only, never the horses, and its spirit is
  contested **(told plainly; hazards design 7.7)**.

### 7.8 The Long Supper (F4): `axis`

**Macro shape (lead's call, revised after the check so it reads apart
from the Choir).** The default Blueprints are rings: **the Horseshoe**
and **Two Sittings** wrap hall bays (11–13 × 12–16, each a Room) around a
**kitchen hub**, from which service passages (width 2–3, ≤ 12 per run)
radiate to the bays. Bays are joined to each other and to the passages
only by service-screen Doors, never by Arches. One service passage is
the Bypass; kitchens and cellars are side rooms. The Circuit is the
servery ring: the Forecourt and one or two pantries behind it.
Blueprints:
- The Horseshoe (default)
- Two Sittings (default)
- The Long Table
- The Cellar Run
- High Table
- each with a trimmed variant

**Rules:**
- **At most 5 hall bays on the main path**, with a kitchen or cellar
  interleaved between bays 2–4.
- No two consecutive bays share a table layout (segment-3 hook).
- A dumbwaiter Latch (a backward shortcut).
- Door share 0.45–0.50, inside the floors 4–5 ≤ 0.5 cap; open-room share
  about 5%; mean corridor about 3. Against the Choir (0.33–0.43, 30%,
  1) that is three different bins.
- Deliberately dry, read as "no condition squares": condition-free harm,
  Rough and Plank squares appear (TR-22; room terrain design 7.8).
  **Told plainly:** floor 4 misses DEP-A1 when the Supper is drawn,
  likely closed if DEP-A1 counts interactive features (TR-21, pending
  the Arcanist owner).

**Risks:**
- Chandeliers over fight squares must be the icicle trap (pillar 1).
- The far-end "feast again" backdrop must read as a picture (RC1, RC6).
- Door clicks at 0.45–0.50: counted in `ExploreClicks`, auto-open on
  path.

### 7.9 The Gut of the World (F4): `braid`

**Macro shape.** Tubes (2–4 wide, fight-legal) split and rejoin around
chambers (pre-baked mask variants) on an authored loop skeleton. Tenant
houses are doored sub-rooms and Landmarks. Blueprints:
- The Coil
- Two Stomachs
- The Villi (spurs capped by `DeadEndDepth`)
- The Parish
- Pylorus Approach
- Appetite Chamber

**Law in layout (attrition):**
- Many loops, but LG7 keeps one avoidable fight. **Told plainly:**
  "choose your fights" is mainly choosing your *approach* (flank pairs)
  and an arm of a split.
- The loops are flank pairs and the Bypass. The Circuit is the pylorus
  ring: the Forecourt and one or two folds behind it (6.7). **Told
  plainly (after the check):** rests before the lair are uncontested here
  as on every floor 2–5 theme (6.8).
- Peristaltic valve Latches flow one way back up the gut.
- Bile region tags (segment 4) mark the tubes' lows in floor material
  only; no walkable sag (8.1).
- Unlocked sphincter Doors meet DEP-S1; locked sphincter caches are
  optional.
- Main-path tubes ≥ 2 wide.

**Risks:**
- Bile green is reserved (RC4).
- Disorientation (flow compass, Landmarks).
- The wall pulse must never read as shifting rooms (RS 7.6).

### 7.10 The Dreamer's Heart (F5): `spiral`

**Macro shape.** One cavern-scale bedchamber in display, partitioned
into ordinary rooms by full-height giant furniture (wardrobes, toy
forts, blanket hills with near-vertical faces, 8.1) **(agreed, Q1)**. The
main path spirals inward to the bed arena. The Forecourt is the hem of
the blanket with two dog-leg gaps into the bed clearing. There is no
Stairhead: the run ends, and a waking-light window dresses the far wall
(M3 seams, display only). Blueprints:
- The Spiral
- Four Posts
- The Nursery Sprawl
- Wardrobe Way
- Foot of the Bed
- Moonward
- (6–8 in M2)

**Law in layout (waking):**
- Rooms at index 1–2 are tagged `skittishShape`; the elite room and the
  highest-index guard post are tagged `ledShape` (RS 8.11). The Bypass
  never goes around that guard post (6.5).
- Early rooms have ≥ 2 Portals, so routs read as waking.
- No near pairs.
- The spiral critical path is capped at the floor-4 median.
- The Bypass is an outer-ring crawl Squeeze (under the wardrobe or
  through a toy-fort tunnel) around an outer-turn guard post; under-bed
  dressing is backdrop only **(re-skinned after the check)**.
- The Circuit is the hem ring: the Forecourt and one or two pillow
  rooms beside it.

**Risks:**
- Scale distortion in architecture only (RC6).
- Toy soldiers must not share unit silhouettes (RC1).
- Partition walls must read as full height.
- No Dreamer identity (RS 7.5).

---

## 8. Isometric readability and art kit rules

### 8.0 Kit metrics (lead's call, after the check)

The physical contract every template, Portal piece and greybox shares
from the first M1 blockout. Values marked *(illustr.)* are the art
team's to calibrate in M1; the wall model, the projection and the
greybox code are binding.

| Metric | Standard |
|---|---|
| Square | 1.5 m *(illustr.)*, the rules square of main 4.3 |
| Wall model | **Thick walls:** a wall fills whole squares, 1 square deep, inside the node's footprint (6.3). A door, Latch or Hairline frame fills its wall square, which is the Portal's square ("open 1 square", "whose square is empty"). Nothing walkable lies inside a wall square |
| Wall height | Full: 2 squares (3 m) *(illustr.)*. Stub: 0.25 square. A full-height partition is at least as tall as a full wall, which stands above a 1.5×-scale boss at the fixed pitch |
| Camera | Orthographic dimetric, elevation 30° *(illustr.)*, yaw in 90° steps **(decided, main 6.8)** |
| Zoom | Default: 28 square-widths across a 16:9 reference screen. Minimum (furthest out): 36. The arena pull-back zoom: up to 34, never below the unit silhouette size RC6 sets at minimum zoom *(all illustr.)* |
| Template origin | The footprint's north-west corner on integer square coordinates, north-up, before the LS3 transform |
| Greybox colour code | Walkable: light grey. Wall: dark grey. Portal by kind: Arch white, Door brown, Locked brown with a padlock decal, Drape purple, Grille cyan, Squeeze orange, Latch red (bar side) and red-hatched (blind side), Bossgate black, Hairline wall-grey with the tell decal. Sockets: yellow marks on the cell edge. Start zones: blue outlines |

`TemplateReadability` checks these on save (11.2).

### 8.1 Rules for the kit

**Flat (lead's call; the coplanar rule is restated after the check):**
- One walkable plane per (x, y). **Walkable squares are exactly
  coplanar.** A dais, stage, sag, low, "below" or blanket hill is
  expressed only by floor material and value and by wall and backdrop
  height, never by a geometry offset on a walkable square. Corridors and
  Hinges are flat too (the drafts' ramps and terraces are struck).
- Partitions (thicket, wagon rows, giant furniture, blanket hills) have
  near-vertical faces at least full-wall height, so they never read as
  walkable slopes (RC1).
- The Stairhead's stairs are a non-walkable backdrop piece: a descent
  into a void beyond the room edge, used through an interact square.
- Height lives in walls, backdrop and skybox (RS 7.6, RC1, RC6).

**Cutaway (lead's call; the visible-space rule is a lead's call after the
check):**
- Every wall and tall piece ships full and stub states (4-way rotation
  makes every wall a front wall). Segment 3's Tall pieces stand at full
  wall height, and their stub state carries the "blocks sight" stub
  glyph, from M1 (TR-19; room terrain design 6.2.1, 8.1).
- Camera-side walls render as stubs for the party's own space **and for
  every room or corridor holding a square in the party's current line of
  sight**. A corridor counts as its own space. Far walls stay full.
- A Grille panel is always drawn see-through at full height.
- Partition materials have their own stub treatment: trees keep their
  trunks and fade the canopy; furniture, wagons and stacks fade rather
  than slice.
- No ceiling over walkable squares. Door frames never cut below stub
  height.

**`CutawayPockets`:** no walkable square more than 3 squares behind a
wall corner in its own room at any of 4 rotations. Templates are
near-convex. In an L-shaped room the leg depth behind the re-entrant
corner is capped so no square lies more than 3 behind it, and the
re-entrant corner wall always renders at stub height. `TemplateReadability`
also checks, at each rotation, that every start-zone and vantage-visible
square is unoccluded under these stub states.

**Screen fit (projected; lead's call after the check):** at the worst
rotation a w × h room spans about (w + h) × 0.71 square-widths across and
(w + h) × 0.35 + 1.7 down (wall height included) at the fixed pitch
*(illustr. factors)*. At default zoom a fight room needs w + h ≤ 34 (a
13×16 room is 29). Arenas use the pull-back zoom and need w + h ≤ 42
(21×21 is 42). Long spaces become bay chains.

**Partitioned open themes (agreed, Q1; the recipe is a lead's call after
the check):**
- Partition walls of the Kennel, Carnival and Heart read as full height
  (thicket, wagon and booth rows, giant furniture).
- Arches between partitioned rooms are 3–6 squares wide; a wide opening
  is what makes the place read as one. Where LS12 turns one into a Door,
  the Door is 2 wide.
- One shared ceiling or backdrop per open theme spans every partition:
  the painted moon and canopy (Kennel), lantern strings toward the big
  top (Carnival), the bed canopy (Heart).
- Floor art runs continuously across Arches, with one subtle universal
  threshold seam on the floor at Arch squares, so rules rooms can be
  found without breaking the one-place read. The minimap and a rules
  overlay outline rooms (morale by room, the Catch-up trigger, Listen's
  "room beyond" and the elite's "visible before commitment" all depend
  on room membership).
- Recipes: the Kennel splits glades by thicket bands; the Carnival
  splits the plaza into 2–3 rooms with wagon and booth rows around a
  central Arch; the Heart splits the bedchamber with wardrobes, toy forts
  and blanket hills.

**Portal grammar (lead's call, after the check; FL-7):**
- Closed Doors and Latches are always opaque solid panels. No Door skin
  is see-through (no barred gate, no wattle hurdle).
- Locked = a Door with the universal padlock decal; no bars. Every lock
  reads as that padlock base plus a kind glyph: Crier, Barb and Tattle
  (Portal hosts only) in M2, Runed and Hour in M3; no lock glyph uses a
  face part, a ring or a clock face (TL-22; traps and locks design 6.11,
  8).
- **The Grille is the only see-through Portal:** a dense mesh or
  bars-over-glass panel with one universal rim glyph (RC5-style), echoed
  by the targeting UI as a "blocked" line. Bars appear nowhere else; the
  Gaol's portcullis trap uses a different silhouette (a spiked drop
  frame).
- **The Squeeze has one cross-theme read:** a low-lintel or crawl-frame
  piece at both mouths and a universal Squeeze glyph on its squares
  (RC5). Themes reskin only the fill. Width-1 corridors and trail mouths
  never use the lintel or crawl silhouette and keep full-height
  clearance.
- **The Latch:** its bar side shows a visible bar; its blind side
  carries a universal "no handle, barred beyond" glyph.

**Portal skins by theme** *(illustr.; every cell filled or marked "not
used", checked by V8 `PortalSkinMatrix`)*:

| Theme | Arch | Door | Drape | Grille | Squeeze fill | Latch | Bossgate |
|---|---|---|---|---|---|---|---|
| Gaol | archway | clock-face cell door, ward door | not used | cell bars over glass | drain crawl | hatch | iron leaf |
| Kennel | thicket gap | boarded hurdle, lodge door | not used | kennel mesh | deer-run | trap hatch | lodge doors |
| Wake | gallery arch | panelled door | drawn curtain | crypt lattice | servants' crawl | hatch | crypt slab |
| Choir | bay arch | sluice door | not used | choir-screen mesh | flooded culvert | hatch | quire doors |
| Silk | aisle mouth | reading-room door | not used | catalogue mesh | shelf gap | book-lift | archive leaf |
| Carnival | tent mouth | wagon door, booth door | tent flap | ticket-window mesh | under-stage crawl | trap hatch | big-top curtain wall |
| Supper | bay arch | service-screen door | not used | serving-hatch mesh | under-table crawl | dumbwaiter | dining-hall doors |
| Gut | tube mouth | sphincter | not used | villus mesh | fold crawl | valve | pylorus |
| Heart | furniture gap | wardrobe door | bed hanging | crib bars over glass | toy-fort tunnel | drawer slide | not used (floor 5) |

The Hairline tell is one universal glyph in every theme. No Latch skin
shares a word or silhouette with a Door or Drape skin.

**Landmarks:**
- One per room from the theme set, unique per floor
  (`LandmarkUniqueness`). Never unit-like, never keyhole-shaped (RC1,
  RC3).
- Each theme's set holds at least the largest Room-node count of any of
  its Blueprints, plus its aisle-end and set-piece extras (about 18 for
  the Silk, the Choir and the Carnival) *(illustr.)*; V8 checks the
  size.
- Precinct-family Blueprints add a Precinct tint.
- The boss Landmark is backdrop visible from about half the floor.

**Precinct tints:** only on wall trim, lighting and minimap fill, never
on walkable-square fill; drawn from a palette checked against the RC4
damage-hue key and colour-blind simulation, with no fully saturated hue;
included in the per-theme greyscale screenshot check.

**Compass:** one directional cue per theme that survives rotation (7.1).

**Minimap data** (UI to segment 10):
- a north-up schematic room-node graph on the macro grid, with explored
  rooms' outlines drawn over it;
- Portal glyphs by kind and state (one global set, in the RC5/RC7
  spirit);
- Landmark icons, Precinct tints, the Stairhead glyph;
- never unexplored rooms;
- a camera wedge.

**Engine note** *(verify on the shipped Godot 4 version)*: baked
occluders suit runtime layouts poorly. Use frustum culling plus
room-chunk visibility tied to fog, and MultiMesh or GridMap per kit
piece. A floor of 9–14 Room nodes needs no streaming.

### 8.2 Art kit budget *(illustr.; recomputed after the check)*

A template carries a superset of sockets; unused sockets render as wall,
and orientations and transforms reuse it, so one template serves several
socket classes. Budgets count templates, then size classes.

| Item | Per theme, M2 |
|---|---|
| Socket standard | A shared 12-square cell with per-cell sockets at offsets 3/6/9 and widths 1–3; an openable edge always supports its centre socket (**SK1**) |
| Guard-post and lair templates | 12–16, covering the toy's 16–37 socket classes through superset sockets and orientations, each in the 1–2 size classes of the theme's party bands |
| Other room templates | Entry 2, elite 3, Q 5, side 6, Alcove 5, Forecourt 3, Stairhead 1–2, signature 1 per Blueprint (4–8) |
| Total room templates | About 41–50 per theme; about 370–450 across nine themes |
| Mask variants (organic themes) | 3–4 pre-baked per organic template, validated like templates |
| Edge tiles (organic themes) | One marching-squares edge set covering exactly the configurations the mask rules allow (no single-square notches) |
| Corridor pieces | About 12 |
| Portal pieces | The skin matrix above |
| Landmarks | Per the Landmark rule above |
| Other | Compass dressing; dressing sets per floor on the display stream |

- **M1 Kennel budget:** 2 Blueprints; about 18 room templates (entry 2,
  guard post and lair 6, Q 2, side 2, Alcove 2, Forecourt 1, Stairhead 1,
  signature 2), 3 mask variants each for glades, the edge tile set,
  about 8 corridor pieces, Arch, Door, Locked and Bossgate skins, and 12
  Landmarks.
- **The Model B comparison, like for like:** both models need room
  pieces on the order of 370–450 across nine themes. Model A's pieces
  combine into 5–41 graph shapes per theme cell against B's 1–8, so A
  buys far more variety per piece (0.1).
- Each template has 2 dressing variants and 2–3 segment-3 terrain
  layers (`TerrainLayer`, each with its Clearway and sockets; room
  terrain design 6.2.5, TR-17).
- Tooling (M1): a Godot editor plugin authors templates as scenes,
  exports integer outlines to core data, and runs the template checks
  and a 4-angle cutaway preview on save. A Blueprint editor shows live V8
  results.

---

## 9. New mechanics considered

### 9.1 Table

| Mechanic | What it adds | Verdict |
|---|---|---|
| Party door verbs (open, close) | Passing closed Doors (the M1 Bypass has one); the Gaol `signatureFeature`; line-of-sight control | **M1** open, **M2** close (FL-8) |
| Grille (sight only) | Elite vantage; Gaol bars | **M2** (FL-7) |
| Squeeze | Armour 7.3; Bypass; a party escape route | **M2** (FL-7) |
| Drape | Wake curtains, tent flaps, bed hangings | **M2** (FL-7) |
| Latch (one-way backward) | Loops instead of backtracking | **M2** (FL-19) |
| Hairline secrets + Sound the wall | Fair secrets; a Mechanics payoff | **M2** (FL-17) **(agreed, Q3)** |
| Listen at a door | Peeking without breaking symmetric line of sight | **M2** (FL-18) |
| Last Turning | Route choice before the boss | **M2** (FL-9) |
| Floor count line | Pacing information | **M2** (FL-20) |
| Overlook (mapped layer) | A planning aid | **M3** (needs segment 10's mapped layer; Yield prefers unmapped rooms) |
| Chute (directed edge) | Spurs with no walk back | **M3** (pathfinder testing cost). **Pending request (HZ-23):** Plunge as a Chute skin (hazards design 6.10). **Pending request (TL-41; M3):** segment 5's relocation traps (in combat, same room only; the Supper's Dumbwaiter Drop out of combat to its linked scullery) wait for the ability model's teleport rule (ABL 8); fallback Prone in place (traps and locks design 6.3, 7.8) |
| Strongroom spur | Fight, steal or pick for one reward | **M3**, floors 2–3 only. **Pending (TL-29; M3):** segment 5's Wager room may take this Site (traps and locks design 6.15); the FL owner's call, not yet ruled |
| Drop-bar doors (the party bars a door; locked for monsters) | Real joiner cuts | **M3**, Gaol only, after SH-18 and the Shade owner. They create safe rest cells against ability 15.1, so they return to the client before they ship **(new lead's call)**. If adopted, segment 5 recommends M3 with the anti-rest guard: monsters force it at 10 + the dropper's Athletics (loud 6), a patrol whose Circuit hits it forces it, and it never satisfies a short rest's "no aware hostile" by itself (TL-33; traps and locks design 6.12) |
| Way Back (Stairhead Latch) | A cheap post-boss return | **M3**, built off by default |
| Spyhole / Peer | Unit information without line of sight | **Rejected:** strains decided symmetric fog; Grille + Listen cover it |
| Collapsing routes; floor-wide alarm; shifting rooms | — | **Rejected** (RS 7.6, conditions 11.1, joiner caps). An "On Watch" Omen is possible Later. Hazard Moods (M3, Omen and Nightmare only) are not a floor-wide alarm: they change hazard states only, never awareness, joiners, hearing, Portals or monster stats (HZ-20; hazards design 6.9) |
| Camp-exit choice | — | **Rejected** (main 4.1 decided; Two Doors owns it) |
| Split-level sub-grids | — | **Rejected** (main 6.8 decided); the client may refuse it explicitly Later |

### 9.2 Adopted in full

**Party door verbs (FL-8).**
- **Open (M1):** a party unit opens an unlocked door by moving into it (1
  square of movement), as monsters do. M1 needs it: its floor-1 Bypass
  carries a closed Door **(staging: lead's call, after the check)**.
- **Close (M2):** a party unit adjacent to an open, unlocked door whose
  square is empty may close it.
  - Out of combat: 1 tick.
  - In combat: the unit's action, once per door per round, and never
    while a hostile is adjacent to the doorway or threatens it with
    reach.
  - This stops door-dancing and keeps Wedge (which locks) distinct.
  - Refused on a Splintered Door (TL-33; traps and locks design 6.11,
    6.12).
- Monster door rules are unchanged **(settled, enemies 12.4)**.
- **Stuck doors (TL-33; traps and locks design 6.12):** rejected as a
  generic type. The **Swollen door** (M3, Choir and Wake, optional
  content only) is a no-roll threshold: if passive Athletics ≥ Easy it
  opens quietly; otherwise noise 6 and +2 ticks.
- **Landmark ticks (M3; HZ-19; told plainly):** the Gaol's Small Hour
  Strikes is a flagged non-party Portal source: every 4 rounds it swings
  shut every open, unoccupied, unlocked cell door in its room; never
  locked, never the `fleeExit`, held while occupied. Landmark ticks are
  not hazards (no damage, condition or unit movement) and are legal on
  floor 1. The Lockdown Lever and the Mourning Cord stay party-only;
  monsters changing Portals remains an FL-8 change (hazards design 7.2,
  7.4).
- The rule is owned by segment 5; layout consumes it.

**Portal kinds (FL-7):**

| Kind | Party movement | Monster movement | Line of sight | Hearing | Wave |
|---|---|---|---|---|---|
| Arch (any open doorway; skins in 8.1) | yes | yes | passes | path, 0 | M1 |
| Corridor (width 1–3, length 1–12) | yes | yes | passes | path length | M1 |
| Door (opaque solid panel; skins in 8.1) | open 1 square; verbs above | open, never close | closed blocks | closed −3 | M1 |
| Locked (optional content only; a Door with the padlock decal) | pick (Mechanics, quiet) or force (Athletics, loud 6 per try); DCs on segment 5's ladder, force DC = lock DC + 2 (TL-2, TL-21; traps and locks design 6.2, 6.11) | blocked | blocks | −3 | M1 |
| Bossgate (arena to Stairhead only) | none until `BossDefeated`, then an Arch | — | blocks | not a path | M1 |
| Drape | out of combat free to draw or open; in combat, drawing costs the unit's action, once per Drape per round, never while a hostile is adjacent to it or threatens it with reach **(lead's call, after the check)** | pass, leave open | drawn blocks | 0 | M2 |
| Grille | impassable | impassable | **sight and noticing pass; nothing else does:** no attack, ability, spell, skill action, aura or other effect may target, originate or extend through it (symmetric, so main 4.3 is kept) | not a path | M2 |
| Squeeze | Acrobatics (below) | never | 90° bend: no line of sight end to end | path, 0 | M2 |
| Latch | lift from the bar side: 1 tick out of combat, 1 square in combat, no check, silent; then a Door | blocked until lifted | latched blocks | −3 | M2 |
| Hairline secret | once found, a Door | never (until opened) | wall | wall until found | M2 |

**Door kind (3.4):** Door, Locked, Latch, Hairline secret and Drape.
Arches and corridors are not door kinds; Squeezes, Grilles and the
Bossgate are neither and never serve as flee exits.

**Locks (TL-21, M1; TL-4 read; traps and locks design 6.6, 6.11):**
- One pick roll per lock per floor (the Margin rule; a Jam holds to
  floor end). Force is retryable: each failure lowers the DC by 2, and
  the third try always opens.
- A picked or forced Locked Portal becomes an ordinary unlocked Door,
  which monsters path through from then on.
- From floor 2, a force Botch on a Door Splinters it: the lock opens,
  noise 10, the Door can never close again and loses its −3 hearing;
  the Portal stands and connects the same rooms **(told plainly)**.
- No party relock verb.
- A Locked Portal's force point sits at path distance ≥ 7 from every
  Dormant group home and every Circuit room, except intended Tattle
  locks (TP10).

**Squeeze (resolution: lead's calls, after the check):**
- 1–3 narrow squares, built with the Squeeze kit read (8.1).
- **Out of combat, one check, no retry:** the leading unit (the selected
  unit; the party may pick it) makes one Acrobatics check on the
  keyed `checks` stream (segment 5; TL-3, traps and locks design 6.12,
  6.22), keyed by the Squeeze's `traversalIndex`, so a backtrack
  re-rolls. On a pass the party goes through
  ("rigged"). On a failure the party still goes through, after +4 ticks
  *(illustr.)*, and makes noise 4 at the Squeeze mouth square nearest the
  party.
- **In combat:** each unit checks as it enters. On a pass it moves into
  the Squeeze and its movement ends there; it continues next turn
  without a new check. On a failure its movement ends at the mouth and
  it may not try that Squeeze again this round.
- Squeeze mouths sit ≥ 8 path squares from any fight host's start zone.
- 2 per floor from M2 via a dedicated Site. On floor 1, one is a
  forgiving practice Squeeze to an optional room that also has a Door
  (`NoSealedPocket`, LG14).
- The action and its costs go into ability 7.2's universal actions table
  (FL-7).

**Listen at a closed door (FL-18; reporting rules: lead's calls, after
the check):**
- Out of combat; no roll, no stream draw; noise 0.
- Active: 2 ticks for untrained parties. Passive and free for a party
  with a member trained in Perception when auto-walk pauses at the door
  (6.10).
- The **room beyond** is the Room node on the far side, following
  corridor Portals.
- Shows a moving or still footsteps glyph for each non-Hidden group
  **physically in the room beyond at that moment**, not by homeRoom.
  Trained Perception adds the size band, plus "passing" if the patrol's
  deterministic waypoint schedule brings it into that room within the
  next 20 ticks.
- **Pending requests (EP-9, EP-12; M2; encounter placement design 6.7,
  6.8.6):** trained Perception would also add the group's activity word
  ("dice", "snoring", "chanting") beside the size band, and, when a
  Waylay lies beyond, which Portal it watches; not rules until the FL
  owner rules (the activity word also with ability 7.2 and segment 10).
- Once per door per floor (heard doors are marked).
- Hidden lurkers are never reported.
- Feeds the enemies 12.4 step-6 hearing outline. Information only.
- **Lockside Listen (TL-23, M2; traps and locks design 6.11):** a pick,
  or opening any door lock, also Listens through that door.
- Owned by segment 10; the action goes into ability 7.2's table. It
  makes stat model 7.1's Perception row ("hear what is behind a door")
  exact.

**Hairline secrets (FL-17; agreed, Q3; the reveal rules are lead's calls
after the check):**
- A secret Portal sits at the end of a dead-end stub of ≤ 3 squares on
  the side the party approaches from, and no other stub that short
  exists on the floor (`HairlineCue`, LG14). A stub is always a secret.
- Its wall face carries one universal tell glyph, shown to party units
  only:
  - a party member trained in Mechanics within 3 squares (Chebyshev) with
    line of sight to the wall face sees it and reveals the secret
    automatically;
  - any party unit adjacent to the wall face sees the tell.
- **Sound the wall** targets only a wall square showing the tell (or
  8-adjacent to the Portal's squares) and opens it: out of combat, 6
  ticks, no roll. There is nothing to sound anywhere else, so wall
  combing is never a strategy.
- There are no decoy tells and no decoy stubs (RC1).
- Counts *(illustr.)*: floor 1 0–1, in the Gaol and the Wake only (the
  Kennel none); floors 2–3 1; floors 4–5 1–2.
- Optional content only (`SecretNeverCritical`). Secret caches count as
  containers in the "containers and secrets" channel.
- The Lucid Key secret (Later) reuses a reserved floor-4 Site.
- Check variants belong to segment 5; detection display to segment 10;
  Sound the wall goes into ability 7.2's table (FL-17).
- **M3 variants (TL-33; traps and locks design 6.12):** a locked
  Hairline cache (Plain or Crier only) and a trapped Hairline cache (a
  catch); the agreed no-roll rules are kept. **Open item (M3):** the
  Lore-inscription reveal in the Silk and the Choir widens agreed Q3 and
  goes to the client at M3; not a rule until then (19.2).

**Latch (FL-19):**
- 1–2 per floor; the deep spur's end. Skins per theme in 8.1 (hatch,
  dumbwaiter, valve and the like), none sharing a word or silhouette with
  a Door or Drape skin.
- The Portal record carries `barSideRoomId`, which must be the deeper
  end: the higher (attachment index, depth) by `branchIndex` (3.4). The
  blind side is the shallower end (the return-Portal principle), so it
  can never open a forward bypass.
- Never into the antechamber, the arena or floor-1 rooms 0–2.
- Monsters never lift it. Lifting goes into ability 7.2's table.

**Last Turning (FL-9):** as 6.8.

**Floor count line (FL-20):** the floor-entry card gains "Main path: n
rooms; side rooms: k; secrets: s" (s from floor 2), with a party-bar
tally. Counts only, never locations (the DH 5.5 G6 pattern; RS 6.9
public-rule display). The floor-1 card omits the secret count, so the
floor-1 Gaol or Wake secret is a find, not a chore (lead's call, as
proposed; Q3 kept it). From floor 2 segment 5 adds "traps found: k ·
open Hunch zones: h", with no total (TL-9, M2; traps and locks design
6.4).

---

## 10. Numbers (illustrative; toy, not simulator)

Every figure here comes from the scratch toys (Appendix B) and is owed a
simulator rerun before it settles.

### 10.1 Findings and fixes

| Finding | Toy value | Fix adopted |
|---|---|---|
| Runtime failure | A 0%; B 0%; C 15–39% anchor fill; R 29–78%; N 0.2–0.6% pass within 2 tries | Model A with reserved Site cells; V8 proof |
| Patrol Circuit within hearing of a fight host | 80–100% of floors | Circuit isolation (6.7), proven in V8 |
| Full-clear ratio | A median 1.77–1.89, p90 2.0–2.2 | Reserved loop-back Sites; a Latch at the deep spur; target 1.7, fail at 1.9 / 2.2 |
| Squeezes per floor | 0–0.8 | A dedicated Site, 2 per floor |
| Line-of-sight break within 6 of entry | 47–92% | LG20 entry rule |
| Peek depth on floors 1–3 | 58–77% | Fight rooms ≥ 10 deep along the entry axis |
| Elite vantage | 69–100% | Elite template rule: start zone ≥ 7 from the Grille wall |
| Screen fit on floors 4–5 | 0% with 12-square cells | Per-cell sockets; long-axis cap 18; the projected bound (8.1) |
| Floor-3 crowding | < 6 free squares per unit with a joiner | Fight rooms ≥ 11×11 |
| Post-boss walk | 60–97 squares (11–17 s) | Way Back off by default |
| Legal role orders | F1 2, F5 untrimmed 1 | Told plainly; variety from topology, signature rooms, templates |
| Model B repetition | 86% of Heart rooms already seen by run 5 at 48 pieces | Model A plus orientation, terrain layers, dressing |

### 10.2 Exploration time and the clock gate

**Exploration time:** lean 3.0–3.9 min, heavy 4.6–6.4 min.

**The gate on the toy** (trimmed F4–5, levers 1–2, cap 3; minutes):

| Exploration | No joins | With joins |
|---|---|---|
| Lean | 19.0–19.2 (pass) | 20.1–20.2 (fail by about 0.2) |
| Heavy | 21.1–21.4 | 22.2–22.4 |

Cap 2 passes in every case.

**Told plainly:** layout is a gate input. The commitments in 6.10 are
about the 1-minute margin; floors 4–5 at party 4 pass only narrowly, so
the pre-authorised cap-2 fallback (companions Q2) stays likely.

### 10.3 Per-floor totals (Model A, lean–heavy, levers 1–2)

| Floor | Total (min) | Target (RS 6.6) | Result |
|---|---|---|---|
| F1 | 15.3–17.0 | 15–17 | ✓ |
| F2 | 17.8–19.7 | 17–20 | ✓ |
| F3 | 20.5–22.9 | 19–22 | **Over at heavy (told plainly)** |

---

## 11. Data, validation and simulator

### 11.1 Records and configuration

The record fields added after the check are lead's calls.

```text
LayoutGrammar (theme field; replaces layoutArchetype)        [FL-14]
  topologyClass, blueprintSet[], rules[siteType][], portalMix{}, portalSkins{kind: skin},
  doorShare, closedShare, corridorBand{minLen,maxLen,widths[]}, roomSizeBand[role][partyBand],
  nearPairsMax[floor], nearPairMinIndex[floor], secretRange[floor], lastTurningWeight,
  alcoveSockets, regionTags, landmarkSet[], bossLandmark, compassCue, organicMask, precinctTints?
Blueprint
  id, theme, floors[], nRange, trimmedMode (untrimmed | trimmed | both), family (plain | precinct),
  allowedTransforms[], cells[] {cell, footprint, chainIndex?, allowedRoles[]},
  chainEdges[] {fromCell, toCell, side, kindConstraint, forcedClosed},
  arenaCell (2x2), stairheadCell?, roleOrders[][],
  sites[] {type, mandatory, priority, anchorNodes[], reservedCells[]}, secretSites[],
  bypassTargets[] {guardPostChainIndex, weightPermille}, nearPairSpecs[],
  maxNodes, maxEdges, signatureRoomCell, landmarkCells[]
SiteRule
  id, theme, siteType, weightPermille, floors[], sideRoomCount, loopBack,
  nodes[] {role, footprint, offsetCell, slotHostRole?, circuit?}, corridorCells[],
  edges[] {from, to, kindConstraint, socketSide, forcedClosed, pairDoor?}
RoomTemplate
  id, theme, role, sizeClass, partyBands, lessonTag, footprintShape,
  sockets[] {cell, side, offset, width}, orientations[], startZone, partyEntrySquares[socket],
  postSquares, patrolWaypoints, containerSockets[] {noCheck}, trapSockets, landmarkSocket,
  socketToStartZoneMin[socket], fleeExit, vantageSquares?, maskVariants[]?
FloorLayout (output)
  static (regenerable from the seed; covered by layoutDigest):
    blueprintId, transform, roleOrder, bypassTarget, siteRules[], secretCount
    rooms[] {roomId, role, mainPathIndex?, effectiveIndex, branchIndex {attachIndex, depth,
      sibling}, parentRoomId?, templateId, orientation, maskVariant?, tags[] (nearStart,
      skittishShape, ledShape, coverRoom, ...)}
    portals[] {portalId, a, b, kind, skin, initialState, forcedClosed, pairDoor, barSideRoomId?,
      hidden, fleeExitFor[], squares[], width, length, ownerRoomId, mainPathIndex?}
    per-square {roomId, mainPathIndex?, effectiveIndex}; mainChain[n]; slotHosts;
    equivalentHosts; circuit {rooms, waypoints, phase}; bypass; nearPairs[]; flankPairs[];
    grilleVantage[]; hearingMatrix[k] + circuitOpen + generatedState + allOpen;
    anchors[fightRoom][entryPortal][]; reserved {he7First, he7Second, supply}; landmarks[];
    keepsakeSlots[]; minimapNodes[]; protectedSquares[]; protectedSightLines[]; report{}
  layoutDigest: a 64-bit hash (FNV-1a) of the canonical serialisation of the static part,
    fields in the order above and arrays in roomId / portalId / square order
Save: the seed and configuration regenerate the static part, checked against layoutDigest;
  the save stores only dynamic per-Portal state (open, closed, drawn, found, lifted)
  and, beside it, dynamic Furnishing state {id, square, state: intact|debris|gone,
  burstSpent}; terrainDigest sits beside layoutDigest (TR-17; room terrain design 6.2.5);
  hazardDigest sits beside terrainDigest, and the save adds FloorHazardState (Mood stage
  and counter, menu index, Stir phases, Tides, Cord uses, vent triggers) and UnitFallState
  (HZ-28, HZ-20; hazards design 11.1, 11.2)
Configuration snapshot (RS 11.1; hashed into the reproducibility key, RS 6.4 R-SEED-5; FL-14)
  layout.mainPathRooms[floor], layout.mainPathRoomsTrimmed[floor], layout.sideRooms[floor],
  layout.sideRoomsTrimmed[floor], layout.secrets[floor], layout.hearingGapMin,
  layout.nearPairsMax[floor], layout.lastTurningWeight (ceiling), layout.wayBack (default off)
Events: GenerationRetried (existing); DoorClosed; DrapeDrawn; LatchLifted; ListenHeard; SecretFound
```

- `layout.secrets[floor]` is the global bound *(illustr.: [0–1, 1, 1,
  1–2, 1–2])*; each theme's `secretRange[floor]` sits inside it, which is
  how the Kennel's 0 on floor 1 is expressed **(new lead's call; Q3
  counts agreed)**. `nearPairsMax[floor]` and `lastTurningWeight` nest the
  same way: the grammar value lies within the configuration bound, and V8
  checks it.
- **Index fields (FL-13):** `mainPathIndex` is null off the main chain.
  `effectiveIndex` is defined for every square (3.4). The Surprise rule
  reads `mainPathIndex` (null means "not 0–1"); `expectedTenths`,
  Keepsake order and leftovers read `effectiveIndex`.

### 11.2 Validators and assertions (change FL-14, FL-15)

**One new build validator, V8 `LayoutGrammar`** (RS 11.2 ends at V7),
with named sub-checks:
- `BlueprintEmbeds`, `DrawCount` (6.1), `SideBudget` (every k, trimmed
  and untrimmed), `RouteListsNonEmpty` (6.4).
- `LayoutGraphInvariants`: LG1–LG20.
- `HearingGapBound`, `CircuitIsolation`, `DoorShareAfterRepair` and LG12
  after LS12 repair.
- `SkippablePoolProof`, `PortalMinimums`, `PortalKindsLegal` (the
  return-Portal principle, `barSideRoomId`), `NoSealedPocket`,
  `HairlineCue`, `GrilleReach`, `SecretNeverCritical`,
  `RoleSlotsAvailable` at the minimum side count (including the
  pre-first-fight trap socket), `HE7Sockets` (non-empty LS11 lists on
  every role order, floor 5 included), `EquivalentHosts`.
- `TemplateReadability`: `CutawayPockets`, the projected screen fit, the
  8.0 kit metrics, the stub-state visibility check, start-zone margin,
  Landmark socket, flee exit, entry landing, no keyhole silhouette, the
  Squeeze and width-1 reads.
- `F1LessonLadder`, `LayoutDistinct`, `TraversalFeel` footprint share,
  `PortalSkinMatrix`, `LandmarkSetSize`, `NearPairMinIndex`, config
  nesting.

**What V8 enumerates (lead's call, after the check):**
1. **Per Site, exhaustively:** every legal rule × every template class ×
   every orientation, checked locally (embedding, routes, sockets, local
   invariants). A **template class** is (role, size class, socket mask);
   its metrics are the worst case over its member templates, and over
   their mask variants for organic themes.
2. **Floor-wide, by composed bounds:** each Site and each main-chain node
   contributes interval bounds (min/max door count, walkable, corridor
   and dead-end squares, leaf count, flank pairs, worst-case
   socket-to-socket hearing and sight paths). Global invariants
   (`HearingGap` across Sites, `SkippablePool`, Circuit isolation,
   `PortalMinimums`, door-share bands, the corridor ≤ 30% bound, the
   walkable and critical-path bands, LG10 leaf counts, LG13's flank
   share) are proven from those bounds summed or composed over the
   Blueprint. **A check fails when its bound cannot be proven**, even if
   no real floor would break it; the author then tightens the Blueprint.
3. **Full product only over Sites a Blueprint declares `interacting`**
   (for example a Bypass arm that lands in a Circuit side room), with a
   build-time cap on the product size.
4. **Other dimensions:** the LS3 transforms are enumerated; party bands
   are enumerated (size classes differ); trimmed and untrimmed are
   enumerated per `trimmedMode` and every legal `pacing.trimmedFloors`
   value ([], [4, 5], [3, 4, 5]); corridor routes are bounded by their
   longest and shortest entries; door states are covered by the proof
   state of 6.6, so no drawn state is enumerated; the s secret Sites and
   the Bypass target are enumerated; template no-repeat and the
   `lessonTag` adjacency rule are proven by a per-order count check (each
   role and tag has more candidates than the order needs in a row).

Rules per Site type are capped at about 4 in M2.

**V9 `RoomTerrain` (TR-18):** segment 3's build validator joins RS
11.2's list beside V8, per template × orientation × layer × floor band ×
theme plus floor-level capacity rows per Blueprint and rule choice
(room terrain design 11.2). Its runtime is a lookup that cannot fail
(FL-15).

**V10 `RoomHazards` (HZ-28):** segment 4's build validator joins RS
11.2's list beside V8 and V9 and runs after V9, including
`HingePosternClear` and `NoSanctumBridge` (LG3); its draws come from a
`hazards` child of the `terrain` stream with fixed counts. Its runtime
is a lookup that cannot fail (FL-15; hazards design 11.2, 11.3).

**Runtime assertions** at the floor assertion point (6.1), each with a
build-time structural proof; anything else is a report:
- `MainPathConnected`, `NothingInWalls`, `AntechamberDominatesArena`,
  `ArenaEntrancesFromAntechamber`;
- `HE7Reserved` (with DH's HE-7 check), `CircuitValid`, `BypassValid`,
  `SkippablePool` (structural form), `IndexIntegrity`, `PortalMinimums`,
  `RoomCounts` (LG19);
- `ProtectedIntact`: no terrain or moved group blocks `protectedSquares`
  or a `protectedSightLines` square, and every slot-hosted group stands
  on its host or an equivalent host. Segment 3's Clearway extends it:
  nothing that blocks movement is placed on, or moved onto, a Clearway
  square, and V9 proves the no-piece part at build time (TR-17; room
  terrain design 6.2.2, 11.2). Segment 6 reads it as written: no unit
  starts on a Clearway square (EP-24, a reading; M1; encounter placement
  design 6.6.5).

Failure triggers HE-7's single retry **(settled, DH 12.4; FL-15)**.

### 11.3 Reports (M2, at both party caps)

- **Paths:** `CriticalPathSquares`, `FullClearWalkRatio`,
  `DeadEndDepth`, `EmptyDeadEndRate`, `PostBossBacktrack`.
- **Time:** `ExploreSeconds`, `ExploreClicks`, `ExploreTimeByTheme`,
  `SecretSearchSeconds` (beside `ExploreSeconds`).
- **Doors:** `DoorOpsPerFloor`, `DoorCloseInCombat` (Drapes included),
  `CycleCount`.
- **Hearing:** `ChainPullRate`, `HearingGapOpenDoors`.
- **Flee:** `CorneredRate`, `FrightenFleeSuccess`, `AnchorFallbackRate`.
- **Rests:** `RestSpotPatrolCoverage`; `PreBossRestSafety`, gated
  (6.8).
- **Stealth and the boss:** `MultiBypassRate`, `BypassUseRate` by class,
  `ElitePeekNoticeRate`, `BossFightJoinRate`.
- **Mix and variety:** `RoomMix`, `AcrobaticsChoicesPerFloor` (with
  segment 3), `LandmarkUniqueness`, `BlueprintRepeatRate`,
  `SignatureRoomRepeatRate`, `TraversalFeel`.
- **Secrets:** `SecretFoundRate` by floor and by Mechanics training
  **(new lead's call: the report that tells whether Q3's floor-1 count
  is worth its cost)**.
- **Shared:** the DEP-S1 dead-room rate, with segment 5.

### 11.4 Simulator arms

1. **Walker (the clock measure; definition a lead's call after the
   check).**
   - Policies: **lean** (the main path plus every reward visible from
     it, Listen at each closed door on the way) and **heavy** (the
     full-clear tour of 6.9), plus **stealth via the Bypass** and
     **hunt** (heavy, plus walking to every dead-end stub until the
     count line's s secrets are found).
   - Seconds per tick and a cost table per action *(illustr., the
     companions owner calibrates)*: a step 1 tick; open a door 1 click;
     a container N clicks by type; Listen 1 click and 2 ticks (0 ticks
     when passive); Sound the wall 1 click and 6 ticks; a Squeeze 1
     click, +4 ticks on a failure; a Latch 1 click and 1 tick; a reveal
     1 click; each auto-walk stop 1 click.
   - Statistic: the median over the release seed set at party 4, per
     (theme, floor). The 6.10 gate passes when the lean median is ≤ 3.5
     min and ≤ 40 clicks. The heavy and hunt figures are reported.
   - Supplies the companions 7.4 time model with these terms.
2. **`DamageShareByRangeBand` per grammar**, with segment 3, for the
   weapons 9.5 cover decision.
3. **Chain-pull arm:** doors opened mid-fight; near pairs.
4. **Flee arm.**
5. **Doorway-hold arm:** damage per unit when the party holds the entry
   doorway against an open engagement, including the Kennel's 1-wide
   trail mouths at floor-1 solo. If the hold wins by more than X
   *(illustr., builders)*, flank reach is too long.
6. **`BossDamageFromOutsideLeash`:** target 0 for melee-capable bosses,
   which the symmetric `AntechamberScreened` delivers without FL-21 (a).
7. **Boss arm with a patrol joiner** on floors 2–5 (`BossFightJoinRate`,
   FL-21 (e)).
8. **Rest arm** (DH 5.7).
9. **Bypass-experience arm** (enemies 20.1, multi-bypass, camp
   catch-up).

---

## 12. Staging

**(lead's call; the Q1–Q3 content agreed)**

| Wave | Content |
|---|---|
| **M1** | Kennel subset: 2 Blueprints, a small rule library, the M1 kit budget and the 8.0 kit metrics. Graph rules LG1–LG10 (LG3 and LG5 in their floor-1 form; LG10 with no depth-2 spur, since the Latch is M2), LG12 (non-door flee exits, FL-12's M1 clause), LG15–LG16, LG18–LG20. Portal kinds Arch, Door, Locked, Bossgate; the party **open** verb (FL-8). Corridors and the socket router; `mainPathIndex`, `effectiveIndex` and the index rules (FL-13); the `expectedTenths` order; slot hosts (FL-3). The Bypass as a quiet corridor with a Door; the Circuit with a cover room and isolation; hearing graph and matrices. HE-7 and HE-4 reservation at the shared assertion point with the retry. The arena socket with dog-leg Forecourt Arches (`AntechamberScreened`). The Stairhead with plain stairs, warning and Scrap (items 24 is M1). V8 core sub-checks; runtime assertions; template and Blueprint editor tools. Enemies 25 M1 rows; DH-32 |
| **M2** | All 8 core grammars plus the Wake if it passes; 4–6 Blueprints per theme (6–8 for floor-1 themes and the Heart); the Precinct family; the Portal skin matrix. Grille, Drape, Squeeze, Latch, Hairline (Q3 counts). The party close verb; Listen; the floor count line. Elite vantage; the antechamber on every floor (floors 2–5 on the Circuit, Q2); retreat anchors (enemies 25 M2 row). Near pairs; Gaol shut-door pairs (if the owner agrees). Last Turning; Landmarks, compass, tints and minimap data; trimmed Blueprints; signature rooms. Full V8; all reports and arms; travel auto-walk (segment 10) |
| **M3** | More Blueprints per unlock; seams at the Stairhead; Nightmare-tier layout variants (to be designed); Overlook; Chute; Strongroom (floors 2–3); Drop-bar doors (Gaol, gated, back to the client); Way Back (off by default); the Supper on floor 3 (`LayoutDistinct` already applies to the Choir–Supper pair from M2) |
| **M4** | Breadth: rules and templates |
| **Later** | The Lucid Key secret door (reserved Site); trader and Pledge rooms (tags reserved from M2). Split levels: not recommended; listed only so the client can refuse them explicitly |

---

## 13. What segment 2 owns, and hooks handed to later segments

**Segment 2 owns:**
- the room graph: nodes, roles, Sites, Portals and initial states,
  cycles, branches, Alcoves, secret and lock-key *placement*;
- counts and sizes by depth; the macro grid and embedding; corridors;
  door placement and density;
- `mainPathIndex`, `branchIndex`, room identity;
- slot hosts, the Circuit, the Bypass, flank entries, near pairs;
- hearing and movement graphs, static matrices; anchor rankings;
- HE-7 and HE-4 reservations; the Forecourt, arena socket, Stairhead and
  Bossgate; `AntechamberDominatesArena`;
- Landmark sockets, compass, minimap data, Keepsake numbering, signature
  rooms;
- the 8.0 kit metrics, the Portal grammar and the skin matrix (with
  the art team);
- the layout stream, V8, the runtime assertions and the reports.

**Handed off:**

| Segment | Receives from layout | Owns |
|---|---|---|
| 3 Room shapes and terrain | Template footprints, start zones, `partyEntrySquares`, the `coverRoom` tag, wet/dry/web/slick/bile region tags, `lessonTag`, `protectedSquares` and `protectedSightLines`; the terrain step's place in the order (after LS13, before enemies 10.5 step 2; 6.1) | The room-terrain system and the knock-into system (TR-1; room terrain design 13): interiors, in-room terrain squares and placement, cover props and **the weapons 9.5 cover decision** (with the per-grammar range-band arm, including cover through Grilles if sight-only is reopened), DEP-A1, Wrecker walls and edges, narrow ground toward armour 7.3's ≥ 3, the Choir and Carnival entry caps |
| 4 Hazards | Region tags; "no hazard" Hinge and Postern squares (radius filled: no hazard on the square or its 8 neighbours, no Void within 2; V10 `HingePosternClear`; HZ-3, hazards design 6.2.2); no crossable bridge into the Sanctum (no Void or crossable hazard region; V10 `NoSanctumBridge`; HZ-3) | Floor-scale hazard density (inside segment 3's caps), bile, drop hazards, Teetering counting, vents (TR-1; room terrain design 13); the hazard / trap / terrain line: an open trapdoor is a Void, a closed one that springs is a trap; a horn post is a Cord, a horn tripwire a trap; the sprung twins stay segment 5's and boss counterplay segment 9's (HZ-1; hazards design 6.1) |
| 5 Traps, locks, checks | Locked, Hairline, Squeeze, Latch and Door slots; `PortalMinimums`; trap sockets within 6 of fight hosts, and from floor 2 one before the first fight (room 0 or the 0→1 Portal, Shade DEP-S1(a)), the 0→1 Portal socket filled by a Door catch or a threshold wire outside the Portal square (TL-20; traps and locks design 6.20) | DCs (the ladder, TL-2); traps and trap records (DEP-S1; TR-1; taxonomy, tells and records, TL-10; traps and locks design 6.2, 6.3, 11.1); keyring and cell caches; sphincters; **the party door verbs as rules (FL-8)**; the Squeeze check on the keyed `checks` stream (TL-3) and its failure cost; Drop-bar (M3); Wedge (SH-18); Hairline check variants |
| 6 Encounter placement | Slot hosts, `equivalentHosts`, the Circuit with phase, the Bypass, near pairs, matrices, start zones, `nearStart`, `skittishShape` / `ledShape` | Enemies 10.5 steps 2–11 (step 9 moves only within a host or to an equivalent host, 6.1), `placementRules`, the reinforcement and Dormant partners for near pairs (encounter placement design 6.12), Rival Party fit in elite templates (a Later stub, EP-34; encounter placement design 6.14) |
| 7 Loot | Container, Alcove and secret-cache sockets; HE-7 and HE-4 reservations | Contents and channel counts |
| 8 Non-combat rooms | Q rooms, the floors 2–5 antechamber micro-event socket, trader and Pledge tags, signature rooms | Fills, trader, shrines, Dream Fragments |
| 9 Boss rooms | The 2×2-cell socket, two dog-leg Forecourt Arches ≥ 5 apart, the Bossgate, start-zone depth ≥ 9, the symmetric `AntechamberScreened` (6.11), the arena pull-back zoom bound (8.1) | Interior, counterplay feature, phases, Keepsake arena dressing; boss tuning with a patrol joiner (FL-21 (e)) |
| 10 Exploration and information | Minimap nodes and explored-room outlines, Portal glyph kinds, Landmarks, tints, travel paths, count-line data, the patrol waypoint schedule (Listen's "passing") | Fog, minimap UI and the rules-room overlay, Listen UI (occupancy, 9.2), Hairline display, the hearing outline (including the self-teaching shut-door prompt and the elite vantage risk), interrogation facts, auto-walk (pausing at doors, 6.10), Overlook (M3), the Stratum Card line |
| 11–12 Seeds and validation | The `layout` and `dressing` children and their derivation, draw counts, V8 and its enumeration, the assertion list and the shared floor assertion point | The stream tree, release seed set, retry logging |

---

## 14. Binding requirements and how they are met

| Requirement | Source | How it is met |
|---|---|---|
| Rooms and corridors; a guaranteed path entrance to boss | main 4.6 **(decided)** | Portals with corridors of length ≥ 0 (FL-1, **agreed Q1**); LG2 |
| Room counts; 1–3 empty or special | RS 6.6 | FL-4 counting; role orders (6.5) |
| Floor-1 antechamber, cut vertex | RS 9.4 | LG3, LG5; extended to every floor (FL-5, **agreed Q2**) |
| Layout archetype per theme; dressing sets | RS 13, 8.0 | `layoutGrammar` (7); the display stream |
| Stairs, left-behind warning, Scrap | RS 14; items 9, 24 | The Stairhead (FL-6, **agreed Q2**), M1 |
| Keepsake numbering | RS 9.3 | 6.5 |
| `mainPathIndex`; corridors take the lower index | DH 17.1 **(agreed Q9)**, DH-32 | FL-13: `mainPathIndex` where defined, `effectiveIndex` on every square |
| HE-7 containers with retry | DH 12.4 | LS11; LG16; FL-15 |
| `expectedTenths`; off-path inheritance | enemies 6.3 | Main-chain order; `branchIndex` |
| Slots (patrol, guard posts, lair, elite, boss) | enemies 10.4 | 6.5, 6.7 |
| Stealth bypass / second route | enemies 10.4, 25; classes 6.4; Shade design | The Bypass (6.8) |
| Patrol through cover | enemies 10.4, 25 | The Circuit's `coverRoom` |
| Cluster budget | enemies 10.4 | `HearingGap`, isolation, near pairs |
| Path hearing; locked doors block | enemies 12.3–12.4, 25 | Hearing and movement graphs (FL-7) |
| Retreat anchors | enemies 14.5, 25 (M2) | 6.8 (FL-12) |
| Arena: 2+ entrances, never sealed | enemies 17.5 | LG4; Arches; the Bossgate is not an entrance (FL-6) |
| Experience after one bypass | enemies 20.1 | LG6, LG7, `SkippablePool` |
| Floor-1 protections | DH 17.1; RS 8.2(d); enemies 10.3, 11.3 | LG15; segment 6 on slot hosts |
| Cowable group on the floor-1 main path before the boss | CL-10 | The Circuit through Q room 3 |
| `SeedHasTarget` ≥ 2 main-path encounters on floor 1 | classes 5.6 | 4 main-path fight hosts on floor 1 |
| ≥ 3 Acrobatics choices from M2 | armour 7.3 | 2 Squeezes + segment 3 narrow ground |
| ≥ 2 closed unlocked doors from floor 2; traps near fights; a trap before the first fight | Shade DEP-S1 | `PortalMinimums` through `forcedClosed` edges; trap sockets, one before the first fight from floor 2 |
| An interactive square near the start in about 40% of rooms | Arcanist DEP-A1 | Region tags for segment 3; the Supper miss told plainly |
| Walls and edges near fights | Vanguard design | Chokepoints and flank pairs; segment 3 density |
| Corridors and open rooms | weapons 26 | `RoomMix` |
| Cover decision in M2 | weapons 9.5 | Segment 3, with the per-grammar arm; decided by room terrain design 6.1 (TR-2) |
| Secrets and unexplored rooms exist | enemies 14.6; stat model 7.1, 10 | Hairline counts (**agreed Q3**); the floor count line |
| Trader in an off-path side room | items 10.9 | The `traderEligible` tag |
| Leftovers by room | items 9 | Room identity (FL-13) |
| No guaranteed safe spot | ability 15.1 **(agreed)**; ability 5 | Floors 2–5 antechamber on the Circuit (**agreed Q2**); `NoSealedPocket`; the floor-1 exception and the cleared-Forecourt case told plainly; `PreBossRestSafety` gated |
| Floor-time gate | companions 7.4 **(agreed)** | 6.10 commitments measured by the 11.4 walker; reports |
| `FloorTimeByTheme` ≤ +10% | RS 10.1, 11.4 | Per-grammar normaliser |
| Wake dormant bias; Heart morale by room | RS 8.5, 8.11 | `nearStart`; `skittishShape` / `ledShape` |
| Flat rules; camera; tile pieces | main 6.8 **(decided)** | LG18; section 8 |
| Connected; boss reachable; nothing in walls | main 7 | LG2; runtime asserts |
| Seed determinism; separate streams | main 6.3 **(decided)**; RS 6.4 | The `layout` child; fixed counts; display stream (FL-2) |
| Symmetric line of sight and fog | main 4.3 **(decided)** | Grille sight-only and symmetric; Listen gives no line of sight |

---

## 15. Risks

| # | Risk | Signal | Response |
|---|---|---|---|
| 1 | Blueprint recognisability: graph shapes learnable in about 20 runs per theme; floors 1 and 5 have 1–2 rhythms | `BlueprintRepeatRate`; playtest notes | 6–8 Blueprints for floor-1 themes and the Heart; signature rooms; transforms; the Precinct family; more from M3; the floor-5 n = 8 option (19.2) |
| 2 | The macro grid shows ("a warren of boxes") | `TraversalFeel` | Per-theme topology classes; 1×2 and L footprints; organic masks |
| 3 | Joiners become rare | `ChainPullRate` below 0.3 | Near pairs, Circuit near-passes, the 0.3–0.6 band |
| 4 | Floor-clock overrun (the gate margin is about 1 min) | `ExploreSeconds`, `ExploreClicks`, the companions gate | 6.10 commitments; auto-open; passive Listen; trimmed floors keep rewards as Alcoves; the cap-2 fallback (companions Q2) |
| 5 | Rest economy too generous off the Circuit (forecast likely: the patrol lives in the post-lair tail) | `RestSpotPatrolCoverage`; median rests > 3 | Floors 2–5 antechamber on the Circuit; `NoSealedPocket`; the forecast handed to the DH owner now; the DH 5.7 lever order (found supply 1 per floor first; vendor stock back to the client; supply cap 2 last) |
| 6 | The boss shot from outside its leash | `BossDamageFromOutsideLeash` > 0 | Dog-leg Arches, arena-owned doorways, the symmetric `AntechamberScreened` (holds without FL-21 (a)); FL-21 (a) as a second guard |
| 7 | Door dancing and doorway holds | `DoorCloseInCombat`; the doorway-hold arm | Close costs an action, barred when a hostile is adjacent; flank pairs; FL-21(c) |
| 8 | Multi-bypass experience below 20.1 | `MultiBypassRate` | `SkippablePool`; the Bypass meets the Circuit; FL-21(d) |
| 9 | V8 combinatorial blow-up | Build time | Per-Site enumeration, composed floor-wide bounds, a capped product only over declared `interacting` Sites (11.2); ≤ 4 rules per Site type in M2 |
| 10 | Runtime crash from unprovable asserts | Any `GenerationRetried` in the release seed set | FL-15: only structurally proven asserts at runtime |
| 11 | Kennel ranged dominance on the solo floor | `DamageShareByRangeBand` | Thicket and trail bends; the joint range-band arm with the cover arm (cover decided: room terrain design 6.1, TR-2) |
| 12 | Cutaway failures in tall themes | `CutawayPockets` | The stub kit |
| 13 | Authoring and tooling cost | M2 schedule review | A shared socket standard; editor tooling from M1; the eager art team is the enabling assumption |
| 14 | Partitioned open themes read as boxes | Playtest notes | Continuous floor art across Arches; full-height partitions; organic masks **(agreed Q1)** **(new lead's call: row added)** |
| 15 | Floor-1 secrets go unfound or feel like a chore | `SecretFoundRate`; `SecretSearchSeconds` | Universal tell; the dead-end stub cue; Sound the wall only at a tell; the floor-1 count line omits secrets **(new lead's call: row added)** |
| 16 | Parties dodge the Forecourt patrol and meet it in the boss fight | `BossFightJoinRate` | Waypoints on the far side of the Forecourt; dog-leg Arches; the boss arm; FL-21 (e) **(lead's call, after the check: row added)** |
| 17 | Patrol-free mid-floor rests; the cleared Forecourt used as a pre-boss camp | Median rests; `PreBossRestSafety` (gated) | The DH 5.7 lever order; the 19.2 options for the Forecourt **(lead's call, after the check: row added)** |
| 18 | The kit contract slips during blockout | `TemplateReadability` failures | The 8.0 kit metrics fixed before M1 blockout; greybox colour code **(lead's call, after the check: row added)** |
| 19 | **All evidence is toy** | — | Every number is owed a simulator rerun before it settles |
| 20 | **Research partly from recall** | — | Marked *(unsure)*; boristhebrave.com was blocked by the network allowlist |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: cell size, sizes and areas, door shares,
  corridor bands, near-pair caps, the Last Turning weight, flank share,
  secret counts within the agreed ranges, report thresholds other than
  the stated gates, and Blueprint counts within the ranges.
- The Blueprints, `SiteRule`s and templates themselves, within the
  grammars of 7 and the invariants of 6.2.
- Template authoring tools, editor UI and the engine culling approach
  (8).
- Art, dressing and Landmark sets inside the charter.
- Names, until the naming pass.
- **Not tuning:** Model A with build-time proof; runtime assertions only
  when proven; the `layout` child stream and fixed counts; partitioned
  open themes **(agreed, Q1)**; the Sanctum on every floor and the
  floors 2–5 Circuit rule **(agreed, Q2)**; the symmetric
  `AntechamberScreened`; `NoSealedPocket`; the 8.0 wall model and
  projection; secrets as optional content
  with one universal tell and no decoys **(agreed, Q3)**; exactly one
  route-avoidable fight; the lair as a cut vertex; `HearingGap` > 8;
  Circuit isolation; flat walkable floors; the clock commitments; no
  layout lever.

---

## 17. Changes to approved documents

Applied on 2026-11-21, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and rows owed by
other owners are listed again in 18 and recorded as such. Every row
names the exact section it touches. Rows that touch **decided** or
**agreed** text are flagged in the "Flag" column; **the client's
approval of 2026-11-21 covers them** (with Q1–Q3 where a row names
one). Rows that touch the RS catalogue (agreed, RS Q4) or charter
(agreed, RS Q3) are flagged too **(new lead's call)**, although each
only renames a field, fills a hook or restates a reading. Told-plainly
rows were told in 19.1 and confirmed by the client on 2026-11-21.
FL-21 is a set of requests to the enemies owner, recorded as requests;
it changes nothing until that owner rules. Conditional rows are
recorded now and apply only when their condition is met: FL-11's Gaol
`pairDoor` reading only if the enemies owner accepts it. M3 items are
recorded as M3 items and apply at M3: FL-6's Way Back (built off by
default) and the Supper's floor-3 window (RS 8.9).

**Narrowings recorded inside this design (no approved text changes):**
- the floor-1 Forecourt's "short safe walk" stays near-safe by RS 9.4's
  approved exception; ability 15.1 and ability 5 are met on floors 2–5;
- the Kennel has no floor-1 secret, inside Q3's 0–1;
- the floors 2–5 Forecourt is never guaranteed safe while its patrol
  lives; once the patrol is dead it is a cleared room like any other
  (told plainly, after the check);
- the floor-1 count line omits secrets;
- the Supper's floor-3 window waits for M3 (RS 8.9).

| # | Document / section (current status) | Change | Flag | Approved by |
|---|---|---|---|---|
| **FL-1** | Main 4.6 bullet 1, "Each floor is generated from the run seed as rooms and corridors, with a guaranteed path from entrance to boss." (**decided**); RS 13 Floor-layout row; RS 8.4, 8.8, 8.11 hooks (**agreed**, RS Q4) | Add: "Rooms are joined by Portals (doorways, doors, Arches, flaps and corridors of length 0 or more); open-plan themes are partitioned into ordinary rooms that read as one place on screen (floor layout design 6, 7)" | **Decided** (main 4.6); **Agreed** (RS Q4, hooks filled) | **Client (agreed 2026-11-21, Q1)** |
| **FL-2** | Main 6.3 bullet "Separate random streams ..." (**decided**, as extended by CMP-3 and RT-3); RS 6.4 R-SEED-1, R-SEED-3; RS 6.8 reading (told plainly) | Add: "a `layout` child of each floor's dungeon substream, drawn with fixed counts per Blueprint; dressing draws from a display-side stream the core never reads (floor layout design 6.1)". The party-agnostic graph is drawn first; the party band picks only size classes | **Decided** (main 6.3; an addition consistent with its text) | **Client, with this document**; told plainly |
| FL-3 | Enemies 10.5 steps 1–2 and step 9; enemies 10.4 Placement column (placements unchanged); DH 12.4 HE-7 wording "(enemies 10.5 step 2)" → "(enemies 10.5 step 1a)"; DH 12.5 step 4 | Layout runs as enemies 10.5 step 1a and outputs slot hosts and `equivalentHosts`; step 2 keeps the homeRoom and trader draws; step 9 moves a group only within its slot host or to an equivalent host **(after the check)**; DH 12.5 step 4 draws the guaranteed supply only among HE-4-eligible containers (never in the Gb room or a Circuit room), so G2's "no optional fight" holds on every seed **(after the check)** | — (DH 12.4's placement detail is a lead's call inside the agreed DH Q3 package; wording only; the step-4 restriction narrows a draw inside agreed G2, which it serves) | Lead; enemies and DH owners apply |
| **FL-4** | RS 6.6 table and last bullet; enemies 6.3; enemies 10.3 floor-1 caps; DH 12.5 first half; DH 3.3 PF9, 17.1 Scope and 23 ("lair in room 2" read as an example) | n counts entry (0) through arena (n−1); the floor-1 antechamber is extra; the floors 2–5 antechamber counts as an empty or special room; the elite room and Circuit side rooms count as side rooms; the Bypass arm, Alcoves, corridors and the Stairhead are not counted rooms; the Circuit counts Room nodes; "room k" means `mainPathIndex` k everywhere **except enemies 10.2, whose "Room N" stays room level as defined there**; the DH 17.1 Scope and PF9 rows are read with the 10.2 lair at room level 2, not index 2 **(narrowed after the check)** | **Agreed** (DH 17.1, Q9: a reading only, no text narrowed) | Lead; enemies and DH owners apply |
| **FL-5** | RS 9.4 trigger definition and last bullets (new lead's call there); RS 3.4 glossary "Antechamber" row ("room or corridor" → "room"); RS 13 Floor-layout row; RS 6.6 last bullet (**reading flagged, told plainly**); enemies 17.5 arena bullets (**settled; kept**); ability 15.1 (**agreed; kept**); ability 5 (kept, now met while the patrol lives) | A Forecourt antechamber on every floor; `AntechamberDominatesArena` on every floor on the pre-boss graph; arena entrances are dog-leg Arches ≥ 5 apart with the symmetric `AntechamberScreened`; floor 1 stays empty, single-inbound, off the Circuit; floors 2–5 always a Circuit waypoint, the patrol's homeRoom and fight host at the floor's fight-room size, with at most two Circuit side rooms; RS 6.6's "empty or special" reads that Forecourt as special, so untrimmed floor 5 has no empty room; the Catch-up stays floor 1 only (6.11) | **Agreed** (ability 15.1, kept and stated beside it; the cleared-Forecourt case told plainly) | **Client (agreed 2026-11-21, Q2)**; the after-check details with this document |
| FL-6 | Items 9, 24; RS 14 stairs row; RS 6.2 seams; enemies 17.5 "exits are never sealed" (**settled; clarification**); enemies 14.6 | A Stairhead behind the arena via a Bossgate that opens on `BossDefeated`; none on floor 5; "exits are never sealed" refers to the entrances; the floor fact "path to the stairs" means the Stairhead (on floor 5, the main path to the arena); Way Back built off by default (M3) | — (settled text clarified, not changed) | **Client (agreed 2026-11-21, Q2)**; lead; enemies and items owners apply |
| **FL-7** | Enemies 12.3 modifiers; 12.4 Doors paragraph; 10.4 elite row; 17.4 Champion "visible from the approach"; ability 8.1 targeting (clarification); ability 7.2 universal actions table (Squeeze, Drape in combat); stat model 7.1 Acrobatics and Mechanics rows; armour 7.3; RS 8.3 ("locks are padlocks and bars" → "locks are padlocks; bars appear only on Grilles") and RS 8.5 (**agreed**, RS Q4); RS 7.4 RC3 parenthesis "(locks read as padlocks and bars)" → "(locks read as padlocks)" (**agreed**, RS Q3; told plainly); main 4.3 symmetric line of sight (**decided; kept**) | The Portal kinds table (9.2) and the Portal grammar (8.1); the hearing graph and the monster movement graph; the Grille passes sight and noticing only: no attack, ability, spell, skill action, aura or other effect targets, originates or extends through it; drawing a Drape in combat costs the action; monsters never use Squeeze, unfound secret or unlifted Latch; the Squeeze resolution (9.2) | **Decided** (main 4.3, kept); **Agreed** (RS Q4, hooks filled; RS Q3 charter, one parenthesis narrowed, told plainly) | Lead + enemies owner; ability owner (7.2, 8.1); **client, with this document** |
| **FL-8** | Ability 7.2 universal actions table; enemies 12.4 Doors paragraph; RS 8.3 Gaol `signatureFeature` text ("doors segment, existing door rules" → "the party close action, FL-8; shutting mutes and delays") (**agreed**, RS Q4); RS 13 Traps-and-locks row | The party verbs (9.2): **open in M1** (the M1 Bypass needs it), **close in M2** **(split after the check)** | **Agreed** (RS Q4: one clause re-pointed; told plainly) | Segment 5 owner via the lead; **client, with this document** |
| FL-9 | Enemies 10.4 Stealth guarantee and Placement column; enemies 20.1; enemies 25 "second route" row; the Shade second route; CL-10 | Exactly one route-avoidable fight (a guard post); the lair a cut vertex; `SkippablePool` in its structural form (every other main-chain fight host a cut vertex), read as ≥ preBossPool − 100 with the patrol counted present; Bypass forms by floor; Last Turning drawn in LS5 over `bypassTargets[]` with a per-grammar weight (300‰ on floors 2–4; 0 on floor 1 and the Heart), never around the Heart's highest-index guard post; the "Gb before the lair" order rule dropped | — | Lead + enemies owner |
| FL-10 | Enemies 10.4 Patrol row (DH-28 reading) and cover guarantee; enemies 25 rows; CL-10 | The Circuit (Room nodes 2–4; never rooms 0–2, the entry, the arena or the floor-1 antechamber; always the floors 2–5 antechamber, where it lives in the post-lair tail; a cover room; the CL-10 host on floor 1) and Circuit isolation under `hearingState(k)`, with the sight clause and the narrowed near-pass rule (6.7) | — | Lead |
| FL-11 | Enemies 10.4 Cluster budget (kept; the `pairDoor` reading is a loosening, told plainly); enemies 10.5 step 9 (kept as backstop, moves restricted, FL-3); enemies 12.3 (consumer); RS 8.3–8.11 joiner hooks; DH 6.4 (unaffected, stated) | Static conventions (`hearingState(k)` with every non-forced door counted open in proofs, Circuit-open, generated-state and all-open reports); the live graphs at runtime; `HearingGap` > 8 between slot homes; near pairs at effective path 9–10 with `nearPairMinIndex`; the Gaol `pairDoor` reading (if the owner accepts); LS12 draw-free repair with its repairable set; the `ChainPullRate` band | — | Enemies owner |
| **FL-12** | Enemies 14.5 (Routed: "the nearest retreat anchor" replaced by the ranking; Cornered unchanged); enemies 25 retreat-anchor row (M2); RS 7.4 RC3 (**agreed**, RS Q3 charter; reading told plainly, text unchanged) | Anchor ranking (non-door paths first; un-engaged homeRooms excluded); the runtime unexplored filter and fallbacks; a non-door flee exit (an Arch or a corridor) per fight host, recorded as `fleeExit` and never repaired into a Door; `PartyEscape`; the entry landing; RC3 kept literal, "door" read as door kinds (3.4) | **Agreed** (RC3, RS Q3; reading only) | Lead + enemies owner; **client, with this document** |
| **FL-13** | DH 17.1 `mainPathIndex` bullet (**agreed**, Q9); items 9 `leftoversScope: room`; enemies 6.3; enemies 25 `mainPathIndex` row | A Portal takes a `mainPathIndex` only if it joins consecutive main-path rooms (taking the lower); other Portals have none (so the floor-1 Bypass arm has none, and its squares belong to room Gb−1); a main-chain room's Alcoves share its id and index; Portal squares belong to the lower-index adjacent room (off-path: the room with the lower `branchIndex`), except arena-adjacent squares, which take the arena's id and index; every square carries an `effectiveIndex`, which `expectedTenths`, Keepsakes and leftovers read, while the Surprise rule reads `mainPathIndex` (null = not 0–1) | **Agreed** (DH 17.1, Q9: the corridor reading narrowed for the Bypass arm and the arena doorways, and widened to Alcoves of rooms 0–1; told plainly) | Lead + DH owner; **client, with this document** |
| **FL-14** | RS 8.0 record field list (RT-4) `layoutArchetype` (**agreed**, RS Q4); RS 11.1 Configuration snapshot field list; RS 6.4 R-SEED-5 configuration snapshot; RS 11.2; RS 11.4; RS 13 | `layoutArchetype` → `layoutGrammar` (11.1); the snapshot gains `layout.mainPathRooms`, `layout.mainPathRoomsTrimmed`, `layout.sideRooms`, `layout.sideRoomsTrimmed`, `layout.secrets`, `layout.hearingGapMin`, `layout.nearPairsMax`, `layout.lastTurningWeight` and `layout.wayBack`, all hashed into the reproducibility key; RS 11.2 adds one validator, V8 `LayoutGrammar`, with sub-checks; `topologyClass`; `LayoutDistinct` into `DistinctnessCheck` | **Agreed** (RS Q4: a field renamed and filled) | Lead |
| FL-15 | DH 12.4 HE-7 (lead's call; the trigger list widened, the policy unchanged) | Layout runtime assertions run with HE-7's check at one floor assertion point before the loot step and share its single retry; the retry re-derives every child stream; only structurally proven assertions may trigger it; no new retry policy | — | DH owner |
| **FL-16** | Companions 7.4 time model (lever order **agreed**, Q2, untouched); RS 10.1, 11.4 reports | Layout commitments (`ExploreSeconds` ≤ 3.5 min, `ExploreClicks` ≤ 40 at party 4 on floors 4–5, as medians of the 11.4 walker's lean policy; Circuit isolation); the walker's terms and cost table in the time model; reports; **no** layout lever | **Agreed** (companions Q2; untouched) | Lead; companions owner adds the terms |
| **FL-17** | Stat model 7.1 Mechanics row; ability 7.2 universal actions table (Sound the wall); enemies 14.6, 15.3; items 10.4 channels; RS 7.4 RC1, RC5 (**agreed**, RS Q3; consistent); RS 13 Traps-and-locks and Exploration rows | Hairline secrets and Sound the wall (only at a tell); counts by depth (floor 1 0–1 in the Gaol and the Wake only; floors 2–3 1; floors 4–5 1–2); `SecretNeverCritical`; `HairlineCue`; one universal tell, no decoys; secret caches as containers | **Agreed** (RS Q3 charter, consistent) | **Client (agreed 2026-11-21, Q3)**; segments 5 and 10; ability owner (7.2) |
| **FL-18** | Stat model 7.1 Perception row; ability 7.2 universal actions table (Listen); enemies 12.4 step 6 (consumer); main 4.3 (**decided; untouched**); RS 13 Exploration row | Listen at a closed door (9.2), reporting the groups in the room beyond at that moment | **Decided** (main 4.3, untouched) | Segment 10 via the lead; ability owner (7.2) |
| **FL-19** | Enemies 12.4; enemies 20.1 (protected); ability 7.2 universal actions table (lifting a Latch); RS 7.4 RC5 (**agreed**, RS Q3; consistent) | The Latch (with `barSideRoomId`) and the return-Portal principle; `PortalKindsLegal` | **Agreed** (RC5, consistent) | Lead; ability owner (7.2) |
| FL-20 | DH 17.12; the DH 5.5 G6 pattern; RS 6.9; RS Stratum Card (Exploration row) | The floor count line (9.2) | — | Lead; segment 10; DH owner applies 17.12 |
| FL-21 | Enemies Boss and Sentinel profile rows (17.3); 17.4; 17.5; 11.1 scorer terms; 16; 20.1; Den and Sleeper template rows | Requests (flags, not decisions): (a) a Boss-profile Provoked analogue extending the leash to the Sanctum when damaged from outside reach; (b) the Champion's elite uses a Sentinel-style hold, so noticing through a Grille leaves it Alerted but holding; (c) a doorway-hold AI term; (d) the 540 multi-bypass case and Den/Sleeper in-room sneaks against 20.1 (options: drop "Stealth-bypass candidate" from lair templates, re-read 20.1 as "one guard post or one Dormant lair", or rely on the camp catch-up measured by `MultiBypassRate`); (e) boss tuning (5–7 rounds at half camp resources) that includes the floors 2–5 patrol arriving as an ordinary joiner, measured by `BossFightJoinRate` **(after the check)**. Layout no longer depends on (a) (the symmetric screen) or (b) (the opt-in vantage Door) | — (requests) | Enemies owner (M1 for (a), M2 for the rest) |
| FL-22 | RS 13 table | RS 13 rows amended: layout supplies sockets and minimum counts; Traps and locks keeps door, lock and secret rules (including FL-8); Room shapes keeps the ≥ 3 Acrobatics total (layout supplies 2 Squeezes); Encounter placement keeps the Wake dormant rule (reads `nearStart`) and Keepsake dressing; Exploration keeps the Stratum Card and the Lucid Key (layout reserves the Site) | — | Lead |

Waves: FL-1 to FL-4, FL-6, FL-10, FL-13, FL-15 and FL-22 in M1; FL-5 in
M1 (floor 1) and M2 (floors 2–5); FL-7, FL-9, FL-11 and FL-14 in M1 and
M2; FL-8 open in M1 and close in M2; FL-12's flee-exit clause in M1 and
its anchors in M2; FL-16 to FL-20 in M2; FL-21 (a) in M1, the rest M2.

---

## 18. Depends on systems not yet designed

The owed rows are the single list in 17; by owner:

- **The enemies owner:** FL-3 (10.5 step 1a and the step-9 restriction), FL-4's enemies rows,
  FL-6's 17.5 clarification, FL-7, FL-9, FL-10, FL-11 (including the
  Gaol `pairDoor` reading), FL-12, FL-13's 6.3 and 25 rows, and the
  FL-21 requests (a) to (e).
- **The DH owner:** FL-3's HE-7 wording and the 12.5 step-4 supply
  restriction, FL-4's DH rows, FL-13 (17.1), FL-15, FL-20's 17.12 line;
  the `RestSpotPatrolCoverage` report, and the forecast that the rests
  gate will likely fire, with the DH 5.7 lever order queued (6.8).
- **The companions owner:** FL-16's walker terms in the 7.4 time model.
- **The items owner:** FL-6 (items 9, 24), FL-13 (`leftoversScope`),
  FL-17 (items 10.4 channels).
- **The stat model and ability owners:** FL-7, FL-17, FL-18 (stat model
  7.1 rows); ability 7.2's universal actions table for FL-8 (door verbs),
  FL-7 (Squeeze, Drape in combat), FL-17 (Sound the wall), FL-18
  (Listen) and FL-19 (Latch); FL-7's ability 8.1 targeting clause.
- **Dungeon generation segments 3–12:** every hook in 13, including
  segment 5's door verbs (FL-8) and Hairline checks, segment 9's arena
  start-zone depth and `AntechamberScreened`, and segment 10's Listen,
  Hairline display, auto-walk and count line.
- **The simulator:** the arms of 11.4 and the reports of 11.3 at both
  caps.
- **The art team:** the 8.0 kit metrics, the kit and budget of 8.2, the
  Portal skin matrix, the stub states and the readability of partitioned
  open themes.
- **Naming pass:** every placeholder name here.

---

## 19. Open items for the client

None. Approved by the client on 2026-11-21.

The client approved this document as a whole on 2026-11-21, including
every lead's call (the new lead's calls and the lead's calls after the
check in 19.1), the told-plainly items (including "Told plainly after
the check") and the lead's reading of Q2. The three questions were
answered on 2026-11-21 **(agreed)**. The future items in 19.2, which
come back only if a report or a later design triggers them, and the
naming pass remain future items.

### 19.1 This document

**The three answers (agreed, 2026-11-21), each the recommended
option:**

- **Q1, open-plan themes:** option A. The Kennel woods, the Carnival
  plaza and the Heart's single bedchamber are partitioned in the rules
  into ordinary rooms joined by doorways, Arches, flaps and corridors of
  any length, including 0, while reading as one place on screen (FL-1
  changes decided main 4.6). Rejected: B, true open maps; C, partition
  only the Kennel and the Carnival.
- **Q2, the antechamber:** option A. A Forecourt antechamber before
  every boss; every path to the arena passes through it; both arena
  entrances open from it; a Stairhead behind the arena through a
  Bossgate that opens on `BossDefeated`. On floor 1 empty and near-safe
  (as RS 9.4 approved); on floors 2–5 always on the patrol Circuit,
  never guaranteed safe, keeping the ability model's rejection of a
  guaranteed safe room before the boss (FL-5, FL-6). Rejected: B, an
  antechamber on floor 1 only; C, a guaranteed safe antechamber
  everywhere.
- **Q3, secrets:** option A. Hairline secrets on floor 1: 0–1, in the
  Gaol and the Wake only (the Kennel none); floors 2–3 1; floors 4–5
  1–2 *(illustr.)*; optional content only (`SecretNeverCritical`); one
  universal tell, no decoys; Sound the wall (FL-17). Rejected: B, none on
  floor 1; C, exactly 1 on every floor-1 theme.

The client also said everything else in the proposal stands as written
(front matter).

**Told plainly; confirmed by the client on 2026-11-21:**
- **floor 3 runs over its 19–22 min target at heavy exploration**
  (20.5–22.9 on the toy, 10.3);
- **floors 4–5 at party 4 pass the clock gate only narrowly** (lean
  exploration with joins fails by about 0.2 min on the toy), so the
  pre-authorised cap-2 fallback stays likely; layout adds no lever (6.10,
  10.2);
- **untrimmed floor 5 has one legal role order** (floor 1 two); variety
  there comes from topology, signature rooms and templates; floor 5
  untrimmed has no special room for segment 8 (6.5);
- **the floor-1 antechamber is near-safe** by RS 9.4's approved
  exception, beside agreed ability 15.1 (6.11);
- **FL-13 touches agreed DH 17.1 text:** only Portals between
  consecutive main-path rooms take an index, so the floor-1 Bypass arm
  has none and Surprise immunity does not widen there (see also the
  after-check list below);
- the party-agnostic graph: the party band changes only room size
  classes, against a wider reading of RS 6.8 (6.1);
- adding templates changes layouts, covered by the manifest hash (6.1);
- a Bypass taken **and** a timed patrol reach 540 experience (100%),
  below the 20.1 band; flagged (FL-21 (d));
- Den and Sleeper lairs can still be sneaked past in-room; layout
  closes only the route case (LT9);
- the Gaol `pairDoor` reading loosens the static cluster check, if the
  enemies owner accepts it (6.6);
- shutting a door mutes and delays; it does not cut a path unless locked
  (7.2, FL-8);
- RC3 stays literal with "door" read as door kinds (FL-12);
- cleared fight rooms off the Circuit are restable: the approved rest
  economy (6.8), sharpened in the after-check list below;
- the Supper misses DEP-A1 on floor 4; the Kennel's open glades raise
  ranged dominance until the cover decision; the Gut's "choose your
  fights" is mainly choosing your approach (7);
- the Silk's near pairs sit at the top of the band: loud parties pay
  (7.6).

**New lead's calls made while writing this document** (beyond the
answered proposal; confirmed by the client on 2026-11-21, with this
document):
1. **Arch replaces "Opening"** as the name of the open-doorway Portal
   kind, because "Opening" is the Shade's precision feature; the
   proposal's Opening and Arch are one kind (3.4, 9.2).
2. **Collision-free local ids:** LG1–LG20 (DH 5.5 already uses G-ids),
   LS1–LS13, LT1–LT19, SK1, and FF / FB / FC for the critique findings;
   "Fork" becomes "split" in prose; the rewrite record is `SiteRule`
   (front matter).
3. **The Forecourt is always a Room node, never a corridor;** RS 3.4's
   glossary "room or corridor" becomes "room" (6.11, FL-5).
4. **The Catch-up trigger read with FL-13:** RS 9.4 (a) fires on
   Forecourt squares; the arena-owned doorway squares fall under (b)
   (6.11).
5. **Floor 5's Bypass** goes around a guard post other than the
   highest-index one, in both forms (untrimmed: the first or second G;
   trimmed: canonical E Gb G Q L F B), since that guard post is the
   Heart's `ledShape` showcase and the Heart's Last Turning weight is 0
   (6.5; extended to trimmed floor 5 after the check).
6. **Secret counts per theme:** `secretRange[floor]` in each
   `layoutGrammar`, inside the global `layout.secrets[floor]`; this is
   how the Kennel's floor-1 0 is expressed (7, 11.1).
7. **A `SecretFoundRate` report** by floor and Mechanics training (11.3).
8. **Drop-bar doors return to the client** before they ship in M3,
   because barred cells are safe rest spots against agreed ability 15.1
   (9.1, 19.2).
9. **The Bossgate and Stairhead are absent on floor 5,** so the floor-5
   arena has only its two Arches (LG4, 6.11).
10. **Flags on RS catalogue and charter rows** (FL-1, FL-7, FL-8, FL-12,
    FL-14, FL-17, FL-19), although each only fills a hook, renames a
    field or restates a reading (17).
11. **Two risk rows added:** partitioned open themes reading as boxes,
    and floor-1 secrets going unfound (15).

**Told plainly after the check** (the design check of 2026-11-21 found
these; none overrides a client answer); confirmed by the client on
2026-11-21, with this document:
- **On floors 2–5 the patrol always lives in the post-lair tail.** The
  Circuit must hold the Forecourt and no other fight host, the lair is a
  cut vertex and only one fight is route-avoidable, so no lap can reach
  back past the lair. The Circuit is the Forecourt with one or two side
  rooms (6.7). Every room cleared before the lair is patrol-free, every
  floor 2–5 ends with the same beat in every theme (the lair or a guard
  post, the patrol's ring, the boss), and the Bypass meets the patrol's
  route only on Last Turning floors.
- **The rests gate is likely to fire.** DH counts patrol risk as what
  holds rests down, and mid-floor rooms are now patrol-free, so the
  median rests per floor will likely exceed 3. The DH owner gets the
  forecast now, with the approved DH 5.7 lever order queued: found
  supply 1 per floor first; limited vendor supply stock (back to the
  client); supply cap 2 last (6.8).
- **"Never guaranteed safe" holds while the patrol lives.** Once the
  party defeats the patrol, the floors 2–5 Forecourt is as restable as
  any cleared room, one step from the arena. `PreBossRestSafety` is a
  gated report; the options are in 19.2.
- **The floors 2–5 patrol is homed in the Forecourt** (enemies 6.3), so
  it is levelled and paid at room n−2, and RS 6.6's "empty or special"
  reads that Forecourt as special: untrimmed floor 5 has no empty room
  (FL-5).
- **Dodging the patrol by timing usually means meeting it in the boss
  fight,** as an ordinary joiner under the cap (enemies 17.5).
  `BossFightJoinRate` is reported and FL-21 (e) asks the enemies owner to
  tune the boss with it (6.8).
- **The Forecourt on floors 2–5 is a fight room** at the floor's
  fight-room size, since the patrol fight happens there (6.3, 6.11).
- **FL-13 also widens and moves index text:** arena-adjacent doorway
  squares take the arena's index (DH: "the lower index"), and Alcoves of
  main-chain rooms share their parent's index (DH: "a square off the
  main path has none"), which extends floor-1 Surprise immunity to the
  Alcoves of rooms 0–1, a small widening.
- **RC3's "locks read as padlocks and bars" becomes "padlocks"** (agreed
  charter text, FL-7): bars now mean only the Grille, the one see-through
  Portal, so a barred thing never opens (8.1).
- **Without FL-21 (b), peeking at the elite can start the fight;** the
  peek is opt-in behind a vantage Door, and the hearing outline warns
  first (6.5).
- **The Supper changes shape** so the Choir (floor 3) and the Supper
  (floor 4) do not read as the same floor twice: ring Blueprints around a
  kitchen hub, bays joined only by Doors (7.8).
- **Kennel chokepoint mouths are kept but priced:** at most 2 deep, with
  a flank pair behind, so backing into a 1-wide mouth is not free every
  fight (7.3).
- **Secrets are cued by a dead-end stub, never by minimap gaps;** Sound
  the wall works only on a tell, so wall-combing is never a strategy
  (9.2).
- **A failed out-of-combat Squeeze costs time, not a retry loop** (+4
  ticks, then the party is through) (9.2).

**The lead's reading of Q2, confirmed by the client on 2026-11-21:**
"never guaranteed safe" means "never safe while its patrol lives", so a
party that kills the patrol may rest in the Forecourt, as in any
cleared room. The remedies in 19.2 (options (a) to (c) under
`PreBossRestSafety`; (c) needs the enemies owner) stay future items,
triggered only by a report.

**Lead's calls after the check** (confirmed by the client on 2026-11-21,
with this document; each is marked in the text):
1. Stream derivation, retry re-derivation and the weighted-pick method;
   fixed slot lists and the per-Blueprint draw count; the canonical node
   order (6.1).
2. Pre-baked mask variants instead of a runtime cellular pass (6.1, 8.2).
3. The step order LS10 Structure → LS11 Reserve → LS12 repair → LS13
   Annotate, the repairable set and the tie-break (6.1).
4. The LS5 side budget, mandatory and priority Sites, `secretSites[]`
   and the drawn secret count (6.1).
5. Segment 3 terrain before enemies 10.5 step 2; step 9's moves
   restricted to equivalent hosts; one floor assertion point shared with
   HE-7 (6.1, FL-3, FL-15).
6. The socket definition and the corridor router (6.4).
7. Structural `PortalMinimums` through `forcedClosed` edges; fixed
   initial states for Drapes and Latches; the `pairDoor` exception;
   the pre-first-fight trap socket (6.4).
8. Proofs count every non-forced door open (6.6); the live graphs at
   runtime.
9. Up to two Circuit side rooms on the Forecourt (LG5); the sight clause
   and narrowed near-pass rule for the Circuit (6.7).
10. The floors 2–5 Forecourt as a fight host at fight-room size (6.3,
    6.11).
11. Dog-leg arena Arches and the symmetric `AntechamberScreened` (LG4,
    6.11).
12. The elite vantage Alcove behind a Door as the FL-21 (b) fallback
    (6.5).
13. `branchIndex`, `effectiveIndex`, door kind, socket and template class
    definitions (3.4); `barSideRoomId` for Latches (9.2).
14. Last Turning as an LS5 draw over `bypassTargets[]` with a
    per-grammar weight (6.8).
15. The structural `SkippablePool`; the trimmed floor-3 row and
    preBossPool reading (6.3, 6.5, 6.8).
16. HE-4 excludes the Gb room and Circuit rooms; Forecourt templates
    carry a no-check socket; DH 12.5 step 4 restricted (6.5, FL-3).
17. `NoSealedPocket` and `HairlineCue` (LG14).
18. The Drape's in-combat cost; the Grille's full sight-only definition;
    the Squeeze resolution; Listen by occupancy; the Hairline reveal
    rules (9.2).
19. Auto-walk pausing at doors (6.10); the walker's policies, cost table
    and statistic (11.4); the critical-path and full-clear definitions
    (6.9).
20. `LayoutDistinct` bins and the Choir–Supper check from M2; the
    Supper's ring shape (7.1, 7.8).
21. The Kennel chokepoint-mouth rule; the Gaol cell Doors, compass clock
    and role-based lesson ladder; the Heart's outer-ring crawl (7).
22. The 8.0 kit metrics (thick walls, projection, zoom, greybox code);
    the coplanar rule; the visible-space cutaway rule; the projected
    screen fit; the partition recipe; the Portal grammar and skin
    matrix; Landmark set size; tint placement; the recomputed kit budget
    (8).
23. The split of FL-8 (open in M1) and FL-12's flee-exit clause into M1
    (12, 17).
24. The record, configuration and save changes, `layoutDigest` and the
    V8 enumeration (11).

### 19.2 Future items triggered by reports or later designs

These come back to the client only if a report or a later design
triggers them:
- the enemies owner refusing the Gaol `pairDoor` reading (the Gaol
  falls back to near pairs; the client is told);
- the enemies owner's answer to FL-21 (d) re-reading enemies 20.1;
- a floor-5 n = 8 option if `BlueprintRepeatRate` or sessions show
  sameness (it costs 0.3–0.5 min on a gate floor);
- the floor-time gate failing after every lever (the cap drops to 2,
  pre-authorised by companions Q2; the client is told);
- `RestSpotPatrolCoverage` or median rests breaching the DH report (the
  DH 5.7 lever order: found supply 1 per floor first; vendor stock back
  to the client; supply cap 2 last);
- `PreBossRestSafety` showing parties camping in a cleared Forecourt on
  floors 2–5: the options are (a) a rest preview warning that the
  Forecourt is within hearing of a Wary boss, (b) the DH lever order, or
  (c) a second pressure that survives the patrol's death (for example
  the boss's reserve group arriving on a Forecourt rest), which needs
  the enemies owner;
- the enemies owner refusing FL-21 (b) (the opt-in vantage Door
  stands; the client is told);
- Drop-bar doors (M3) and turning Way Back on;
- `SecretFoundRate` showing floor-1 secrets unused or resented (option
  B of Q3 is the fallback);
- the Lore-inscription Hairline reveal in the Silk and the Choir, which
  widens agreed Q3 (M3; TL-33, traps and locks design 6.12);
- split-level sub-grids, listed only so the client can refuse them
  explicitly;
- the naming pass.

---

## Appendix A: how the critique findings were resolved

Draft ids map once: fun S1–S13 → FF-1 to FF-13; balance S1-1 to S3-15 →
FB-1 to FB-15; consistency 1–22 → FC-1 to FC-22.

### A.1 Fun critique

| # | Finding | Resolution |
|---|---|---|
| FF-1 | F1 has 2 role orders and F5 1 | Contested on the count change: adding a room to floor 5 costs 0.3–0.5 min on the gate floor against a 1-minute margin, and lever 3 is predicted to trim floor 5 anyway (3 orders). Adopted instead: a signature room per Blueprint, 6–8 Blueprints for floor-1 themes and the Heart, `lessonTag` variety between consecutive fights. Told plainly (6.5). A floor-5 n = 8 option stays open (19.2) |
| FF-2 | Isolation turns floors into sealed arenas | Near pairs at path 9–10; the Circuit passes a fight host's closed door; Gaol shut-door pairs (owner's call); the `ChainPullRate` band 0.3–0.6 (6.6, 6.7) |
| FF-3 | Thin route choice | Flank entry on ≥ 50% of fight rooms (LG13); Last Turning lowered to 30%. A second patrol bypass was **not** adopted: it pushes 20.1 lower (FB-4); flagged instead (FL-21 (d)) |
| FF-4 | Backtracking | Reserved loop-back Sites; a Latch at the deep spur; no empty leaves; auto-walk in M2; target 1.7 with fail at 1.9 (6.9) |
| FF-5 | Floor-1 teaching overload | `F1LessonLadder`; a self-teaching shut-door pair; a practice Squeeze on floor 1. "Q at index 2" rejected: the Circuit must touch a Q at index ≥ 3 for CL-10 and LG8. Room 2 is the *optional* bypassed guard post, which softens the back-to-back start (7.2) |
| FF-6 | Five identical antechambers | A themed Forecourt per Blueprint; the boss Landmark seen as backdrop; floors 2–5 segment-8 micro-events. Full line-of-sight preview into the arena rejected: it conflicts with FB-1, so `AntechamberScreened` applies |
| FF-7 | Theme traversal monotony | The Supper ≤ 5 bays with interleaving; Silk maze spacing and aisle-end Landmarks; Gaol auto-open; the Heart path cap (7) |
| FF-8 | Elite peek forces the fight | FL-21 (b) Sentinel-style hold; the Grille sight-only; after the check, the opt-in vantage Door as the layout fallback (6.5) |
| FF-9 | Listen busywork | Once per door; passive when trained; counted in `ExploreClicks` |
| FF-10 | Secrets too easy | Near-range Hairline tell for trained parties; the dead-end stub cue and Sound the wall at the tell for untrained ones (revised after the check) |
| FF-11 | Squeeze at party 4 | A single leading check, followers pass; mouths ≥ 8 from fights |
| FF-12 | Trimmed floors lose discovery | Rewards kept as visible Alcoves within the 6.6 bands |
| FF-13 | Organic themes look boxy | `TraversalFeel`; non-rectangular footprints ≥ 50% |

### A.2 Balance critique

| # | Finding | Resolution |
|---|---|---|
| FB-1 | Boss shooting gallery | Arena-owned doorways (FL-13); dog-leg Arches only (LG4); the symmetric `AntechamberScreened` (after the check, so it holds without FL-21 (a)); FL-21 (a) Provoked analogue; the `BossDamageFromOutsideLeash` arm |
| FB-2 | Rests effectively guaranteed safe | Floors 2–5 antechamber always on the Circuit (FL-5, **agreed Q2**); `RestSpotPatrolCoverage` and median rests handed to the DH owner with the DH 5.7 lever order and, after the check, a forecast that the gate will likely fire; `NoSealedPocket`; Drop-bar deferred to M3. A RestPressure *build rule* was not adopted, because patrols interrupt rests by sight, not by hearing; the report covers it |
| FB-3 | Last Turning against the floor-1 antechamber rule | Weight 0 on floor 1 (6.8); LG5 states one inbound Portal |
| FB-4 | Patrol undercounted; the 540 case | The patrol counted present in `SkippablePool`; the Bypass meets the Circuit on Last Turning floors; `MultiBypassRate`; FL-21 (d) |
| FB-5 | Door dancing | Close costs an action in combat, once per round, barred when a hostile is adjacent (FL-8) |
| FB-6 | Chokepoint cheese | Flank pairs ≤ 12 (LG13); the doorway-hold AI flag (FL-21 (c)); the doorway-hold arm |
| FB-7 | The Grille as a firing window | The Grille passes sight and noticing only; no attack, ability, spell, skill action, aura or effect passes (FL-7, widened after the check) |
| FB-8 | Squeeze as a free disengage; a split at party 4 | Mouths ≥ 8 from fights; in combat movement ends; a single leading check out of combat; the Bypass offered as a Squeeze or a two-door corridor; hearing margin ≥ 7 |
| FB-9 | Routs run forward | Anchors exclude un-engaged homeRooms; `CorneredRate` and `FrightenFleeSuccess` reports with a theme remedy (FL-12) |
| FB-10 | New interactions eat the clock margin | `ExploreClicks`; passive Listen; auto-open; trimmed-floor limits; door share ≤ 0.5 on floors 4–5 |
| FB-11 | Unprovable runtime asserts | FL-15; V8's per-Site enumeration and composed bounds; door share measured after repair |
| FB-12 | Isolation decays as the patrol opens doors | The Circuit-open convention (6.6) |
| FB-13 | Crowding at the doorway | The entry landing; no width-1 corridor ends at a fight door (LG20) |
| FB-14 | Shut-door pairs always free to cancel | Accepted and told plainly: the beat teaches shutting on the solo floor. A Drape pair (hearing 0) is the authored alternative where a real join is wanted |
| FB-15 | Class spread | The floors 2–5 Bypass crosses line of sight once (Stealth matters); Wedge distinct from close; Arcanist corridor queues watched by the arm; the Supper DEP-A1 miss told plainly; Vanguard runners sent sideways by the anchor rule |

### A.3 Consistency critique

| # | Finding | Resolution |
|---|---|---|
| FC-1 | `SkippablePool ≥ 610` cannot hold (610 includes the boss) | Restated as ≥ preBossPool − 100 (410 / 310 / 410), the patrol counted present (6.8). Verified against the enemies 20.1 table |
| FC-2 | Hearing pairs break the cluster budget | Near pairs at 9–10 (legal as written); a Dormant-partner option; the Gaol `pairDoor` reading only with the enemies owner, told plainly as a loosening; DH 6.4 stated unaffected (FL-11) |
| FC-3 | The floor-1 Bypass against LG16 and DH 17.1 | LG15 allows the Bypass arm from room 1; FL-13 gives the arm no index (told plainly; DH owner; Q9 text touched) |
| FC-4 | An antechamber on every floor recreates the ability 5 item; RS text incomplete | Floors 2–5 always on the Circuit (**agreed Q2**); FL-5 lists the glossary row, the trigger definition, the 9.4 bullets and RS 13; the floors 2–5 form is written explicitly (LG5) |
| FC-5 | "LairNotBypassable" overclaims | Told plainly: route case only; FL-21 (d) |
| FC-6 | Several FL and V registers | One FL-1–22 register; one V8 with sub-checks |
| FC-7 | Ownership taken without amending RS 13; no party door action exists | FL-22; FL-8 written on ability 7.2 and enemies 12.4, owned by segment 5; RS 8.3 text corrected |
| FC-8 | Name collisions | Drape, Stairhead, Sound the wall, Site, Drop-bar, Bossgate; no "gate" Door skin after the check (8.1); Arch for Opening (new lead's call) |
| FC-9 | Wave errors | The Stairhead in M1; LG3's every-floor form in M2 (the floor-1 form M1) |
| FC-10 | Portal semantics conflict | One table (9.2) and one Portal grammar (8.1): Doors are opaque panels; the Grille is not a hearing path; the Squeeze is a hearing path but off the monster graph; Alcoves share parent identity; corridors owned by the lower-index room |
| FC-11 | RC3 against flee through doors | Non-door flee exits; RC3 text unchanged; the Cornered rate reported |
| FC-12 | The Circuit counted corridors as rooms | Room nodes only (FL-4, FL-10) |
| FC-13 | Last Turning against "Gb before the lair" | Rule dropped; the enumeration re-run by the builders |
| FC-14 | Hairline and Listen pre-empt check ownership | Presented as FL-17 and FL-18 touching stat model 7.1, owned by segments 5 and 10 |
| FC-15 | The elite slot hosts the Rival Party | Elite templates fit 2–3 units; segment 6 and 9 hooks |
| FC-16 | Visual height on walkable squares | Struck: walkable floors are one visual plane (8) |
| FC-17 | Determinism details | Pre-baked mask variants with a fixed draw per node slot (revised after the check); the party-agnostic graph first, told plainly against RS 6.8 |
| FC-18 | Overlook and the count line touch unlisted facts | Overlook M3, Yield prefers unmapped rooms; FL-20 lists the Stratum Card |
| FC-19 | The HE-7 second socket | The antechamber is allowed (Model A's reading) |
| FC-20 | Floor 5 has no special room | Told plainly (6.5) |
| FC-21 | Floor 4 "7 trimmed" called unrecorded | Corrected: it is in RS 6.6; no change row needed |
| FC-22 | Circuit isolation against the arena | Arena start zones ≥ 9 from the entrances (segment 9 hook); part of `CircuitIsolation` |

### A.4 Judges' grafts and numbers-pass fixes

All adopted as listed in sections 5, 6 and 10, except where a critique
above overrode them:
- the Grille is sight-only, not a line-of-sight-and-areas window;
- Overlook moves to M3;
- Last Turning drops from 50% to 30%, and to 0 on floor 1.

### A.5 Design check (2026-11-21)

The design check's builder, consistency, play and art findings were each
fixed, merged into another fix, or (none) rejected. Every fix is in the
text above, marked **(lead's call, after the check)** or **(told
plainly)** where it is new, and listed in 19.1. No client answer (Q1–Q3)
was changed.

## Appendix B: sources

**Recall, with light web checks during research** (mechanisms only; no
names or text taken): Rogue, NetHack, Brogue, DCSS, Spelunky, The
Binding of Isaac, Enter the Gungeon, Dead Cells, Unexplored (Dormans'
cyclic generation), XCOM 2, Hades, Darkest Dungeon, Slay the Spire,
Diablo 1–4, Invisible Inc, Gloomhaven, Into the Breach, Final Fantasy
Tactics, Tactics Ogre, Baldur's Gate 3, Mordheim, Cogmind, Monster
Hunter, and the Jaquays / "Xandering" level-design essays. Details marked
*(unsure)* in section 4 were not confirmed; boristhebrave.com was blocked
by the network allowlist.

Toy models and outputs (scratch, not project files):
`/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/fl/`
(the three-model generator toy), `.../scratchpad/fl_clock.py` (the clock
toy) and `.../scratchpad/precinct_toy3.py` (the Model C toy), full
prefix as on the first line.
