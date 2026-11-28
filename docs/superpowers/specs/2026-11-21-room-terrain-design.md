# Room Shapes and Tactical Terrain: Design (Dungeon Generation, Segment 3)

Date: 2026-11-21
Status: Approved, 2026-11-21
Names updated by the naming pass (naming pass design `2026-11-24-naming-pass-design.md`, approved 2026-11-24; NP-19 and the name-register rows of its section 5 that cite this document). Internal ids, record and report names are unchanged.
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
`2026-11-18-shade-class-design.md`,
`2026-11-20-arcanist-class-design.md`
and `2026-11-20-herald-class-design.md` ("Vanguard design", "Shade
design", "Arcanist design", "Herald design"),
`2026-11-20-companions-design.md` ("companions design"),
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1) and
`2026-11-21-floor-layout-design.md` ("FL", segment 2, the most recent
template)

Items marked **(agreed)** were decided by the client on 2026-11-21 for
the five questions this design raised (Q1–Q5, sections 0, 6 and 19.1),
by choosing the lead's recommendation (option A) in each case, or
restate a client answer recorded in an approved document. The client
also said that everything else in the lead's proposal stands as written:
the method (Model A with its grafts), the room interior vocabulary (Tall
and Low pieces, Furnishings, Kegs, hazard pieces and faces, Rough,
Plank), terrain layers and the `terrain` stream, V9 `RoomTerrain`, the
clutter bands and placement rules, collisions, the limits on
destructible terrain, the AI requests, the class interplay, the boss
arena rules, previews and UI, the nine terrain kits and their signature
interactions, the readability and art kit rules, the adopted, deferred
and rejected mechanics, the numbers, the staging, the ownership split
with segments 4 and 5, the binding requirements, the TR-n changes, the
told-plainly items, the future items, the risks and the critique
resolutions. The client approved this document as a whole on 2026-11-21.

Items marked **(lead's call)** were filled in by the design lead inside
approved rules and were in the proposal the client answered; the client
confirmed them all on 2026-11-21, when it approved this document as a
whole, and they are binding. Items marked **new lead's call** were made
while writing this document, beyond that proposal; they are listed in
19.1 and the client confirmed them all on 2026-11-21. Items marked
**(lead's call, after the check)** were made while revising this
document after the builder, consistency, play and art check of
2026-11-21; they are listed in 19.3 and the client confirmed them all on
2026-11-21. Items marked **(settled)** restate a rule from an approved document;
**(decided)** marks a main-design client decision. Items marked
**(change TR-n)** amend an approved document and are listed, with the
exact section they touch, in section 17; rows that touch **decided** or
**agreed** text are flagged there, and the client's approval of this
document on 2026-11-21 (with Q1–Q5 where a row names one) covers them.
Items marked **(told plainly)** narrow or re-read agreed or settled
text, or state an unwelcome forecast, and were told to the client in
plain words (19.1, and 19.3 for those told after the check); the client
confirmed them all on 2026-11-21. Every item here is binding on the
builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q5 of this document (2026-11-21), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-21 |
| **new lead's call** | A lead's call made while writing this document, beyond the answered proposal; listed in 19.1 and confirmed by the client on 2026-11-21 |
| **(lead's call, after the check)** | A lead's call made while revising this document after the 2026-11-21 check; listed in 19.3 and confirmed by the client on 2026-11-21; never overrides a client answer |
| **(change TR-n)** | Amends an approved document; listed with its exact section in 17 |
| **(told plainly)** | Narrows or re-reads agreed or settled text, or states an unwelcome forecast; told to the client in plain words (19.1) and confirmed by the client on 2026-11-21 |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (10, 11) |

The client's brief: dungeon generation segment 3 of 12, room shapes and
tactical terrain, that is, what is inside a room on the grid. The
approved documents owe this segment the **cover decision** (weapons
9.5). The client added three ideas and asked for the usual research:
1. **movable terrain:** crates, barrels, carts and furniture that the
   party or enemies can move to block paths and provide cover;
2. **knock-into hazards** that are not traps: things a unit can be
   shoved or knocked into by attacks or forces;
3. **lingering terrain effects:** toxic gas, thorny vines on a cell, and
   the like.

The lead's answers, which the client accepted (Q1–Q5, **agreed**), are
in section 0: **room interiors are authored terrain layers drawn by a
fixed-count `terrain` stream and proven at build time; from M2 cover is
one directional tier worth −2 to ranged attack rolls (Q1); Furnishings
can be Shunted, Kicked and Smashed, and Kegs burst once, but walls never
break (Q2); contact hazards and harm squares from floor 2 always give a
save and deal flat, previewed damage (Q3); fire burns webs away and
lightning charges connected water (Charged Water; Q4); and in M3 only, the environment
may leave a fixed, short-lived Miasma cloud that never drifts or spreads
(Q5).**

**Change prefix.** This design uses **TR-n** ("terrain"). A grep for
`TR-[0-9]` across the 19 approved documents finds 0 hits. The research
drafts and model briefs each numbered their own TR rows; this document
issues **one** register, TR-1 to TR-31 (17; TR-30 added after the
check; TR-31 added 2026-11-23, cross-document comparison 2026-11-23,
CC-86), and no draft id is used
anywhere in it. Local id schemes, each grep-checked with 0 hits:
tensions **TT1–TT16**, cover rules **CV1–CV8**, kit placement rules
**KP1–KP9**, combos **K1–K5**, numbers findings **TN1–TN17** **(new
lead's call:** the proposal's N1–N17 collide with the Vanguard design's
N1–N14 mechanics ids**)**, and the Appendix A findings **TF** (fun),
**TB** (balance) and **TC** (consistency).

**Names are placeholders** until the client naming pass. Each was
grep-checked as a whole word across the 19 approved documents. These
have 0 hits: Clearway, Furnishing, Shunt, Smash, Kick, Keg, Overturn,
Rough, Plank, Briar, Miasma, Remnant, Glaze, Haze, Conduct (renamed Charged Water, naming pass NP-19), "harm
square", "hazard face", "hazard piece", "contact hazard", "Low piece",
"cover-giver", "interactive feature", "contact band", "harm band", "Keg
band", heft, `terrainDigest`, `NoCheapKill`, `causedBy`,
`usesFurnishings`, `FurnishingNeverSeals`, `terrainKit`, `RoomTerrain`
(V9) and every record and report name in 11. "Tall piece" has 1 hit: FL
8.1 already uses it as a generic term, and this document matches that
meaning. "Haul" has 1 hit (the noun "haul" on the Thief), which is
harmless. "Covered" appears only as the ordinary English word; this
document uses it, capitalised, as a rules term (3.4). Added after the
check, with 0 hits: Bladed, "Shoot an object", `rulesKey`,
`contactRate`, `PreviewHiddenDelta`, `FloorMinutesDelta`, "thorn-rose".

**Names avoided because they are taken:**

| Avoided | Taken by | Used instead |
|---|---|---|
| Fixture | Conditions 18.1 "fixture conditions" | Tall piece, Low piece |
| Volatile | Enemies 17.2 elite affix | Keg |
| Roll | Dice | Kick |
| "burst" as a class name | The area-shape keyword | Keg (the event is still "a burst") |
| Topple, Upend | Weapon verb; Wrecker Technique | Overturn |
| Brink | Wrecker clause | hazard edge (prose) |
| Ram, Heave, Jostle, Barge | Wrecker; companion kits | Shunt |
| Shift | Forced-movement kind (W-8) | Haul |
| Spine, Lane (as names) | FL and RS vocabulary | Clearway; "lane" stays lower-case prose |
| Flanked | Shade "Flank Sense" | "flank square" (prose) |
| Smoke | Smoke Pellet | Haze (Later) |
| Spill, Rime, Shard, Pall | Approved spell, flask and condition names | Miasma, Remnant |
| Strike (an object) | Enemies 17.5 boss menu **Strike** (renamed Attack by the naming pass); the Shade `strike` option and "Strike limit" | Shoot an object (M3) |
| Spiked | Armour "Spiked Boss" property; the dropped weapons "Spiked" property | Bladed (hazard kind) |

The draft names "Clear Path", "Throughway" and "spine" are merged into
**Clearway**. "Thorns" is merged into **harm square**; "Briar" is kept
only for the M3 Bleeding variant.

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every
rate and share comes from crude toy models (Appendix B); the M2
simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile,
unlocks, ladder), **M4** breadth, **Later** **(settled, main design
9)**.

---

## 0. At a glance

### 0.1 The answers to the brief

**1. Cover, the decision owed by weapons 9.5 (agreed, Q1; change TR-2,
TR-3, TR-4): yes from M2, none in M1.**
- **One tier, directional by geometry.** A target is Covered against a
  ranged, thrown or spell attack roll from 2 or more squares away when a
  cover-giver stands on a square next to the target that faces the
  attacker, within 45 degrees of the line to it (the integer dot-product
  facing test, CV2; the 45-degree bound is a lead's call after the
  check, told plainly and confirmed by the client on 2026-11-21).
- **No three-quarter tier.** "Full cover" stays what it is today: no
  line of sight.
- **The effect is −2 to the attack roll** *(illustr., a lever)*:
  - ignored when the attack already has disadvantage;
  - suppressed against a target whose charged AC bonus is active;
  - replaced, not added to, when the defender uses a reaction AC bonus:
    the larger of the two applies, never both (CV4).
- **Walls, partitions, units and Grilles never give cover. A unit on a
  Portal square never has it.**
- **Melee, reach, saves and areas ignore cover.** Areas are the counter,
  and flanking emerges from geometry with no bonus.
- **Unchanged:** symmetric line of sight (main 4.3, **decided**). Hide
  still needs a broken line of sight; "hide behind cover" is rejected
  for M2, because it would hand every class a free Vanish.
- **AI:** no new term. Monsters price cover through the existing
  Exposure term.
- **Told plainly:** −2 to the roll and +2 AC give the same hit chance.
  Both fail the armour 11 sustained row as written (18 against +4: 35%
  becomes 25%), so armour 11 item 1 gains one positional row (TR-4). The
  −2 form is chosen because the shown AC never changes, not because it
  passes the gate. Cover is symmetric: it helps enemy marksmen too.
- *Rejected:* B, cover as disadvantage; C, no cover rule.

**2. Movable terrain (agreed, Q2; change TR-6, TR-7, TR-8, TR-9): yes in
M2, as Furnishings, objects on squares, never terrain squares.**
- **Kit.** Each theme gets a family of light and heavy Low pieces
  (crates, carts, bales, trolleys, toy blocks); some are round.
- **Shunt.** A light piece Shunted for 2 movement repositions it.
  Shunting a piece as an attack rams it into a unit for the collision
  band, with a Prone save; a light piece breaks doing so.
- **Kick** sends a round piece rolling up to 4 squares along a previewed
  line.
- **Smash** breaks a piece. Breaking is binary: no hit points.
- **Kegs** (the explosive barrels) are the Carnival lamp-oil keg and the
  Gut swollen sac in M2. They are sturdy, burst once and leave nothing
  on the ground; a burst never triggers another burst.
- **Never sealing.** No piece may ever end on the **Clearway**, the
  authored path joining every Portal, landing, start zone and flee exit,
  or narrow it below 2 wide in a fight room (`FurnishingNeverSeals`). A
  piece can never seal a room.
- **Monsters** Smash light pieces that stand in their way; a few kits
  use Furnishings as weapons.
- **Told plainly:** this reverses the Vanguard design's "breakable
  terrain" and "destructible terrain" rejections, **for objects only**.
  Walls never break (RS 7.6 stands).
- **Rejected outright:** carrying, stacking and throwing props (the
  Baldur's Gate 3 "barrelmancy" exploit; weapons 9.3; no weights);
  breakable walls; loot inside crates.
- *Rejected:* B, static cover pieces only.

**3. Knock-into hazards (agreed, Q3; change TR-10, TR-11): yes, from
floor 2 in M2, as a universal damage-only system with themed skins.**
- **Contact hazards** are non-walkable: Bladed or Hot **hazard pieces**
  (spike racks, knife wheels, braziers, cauldrons) and **hazard faces**
  on walls (knife-wheel faces, grinding molars, fireplace grates).
- **Harm squares** are walkable: thorny vines, glass, nibs and bone
  shards. The client's "thorny vines on a cell" are the Heart's
  thorn-rose vines (7.1). They join the agreed M2 square set (conditions
  11.5).
- **On forced movement into one, the unit saves:**
  - failure: typed contact damage **instead of** collision damage;
  - success: an ordinary obstacle stop (a contact hazard) or a stop on
    the last safe square treated as one (a harm square), **with no
    Teetering**.
- **Always a save, always previewed, flat damage**, at most 20% of the
  lowest legal hero's maximum HP; **one environment damage instance per
  unit per action**; monsters never prefer an environment kill on a hero
  (`NoCheapKill`).
- **Contested, two ways:** damage-only hazards give no Teetering, which
  removes this segment's dependency on the pending RT-14 ruling (a harm
  square's one Teetering route is Brink, which RS 8.0 already counts in
  every theme); and Brink is **not** extended to contact hazards.
- **Ownership:** drops, chasms and deep water stay segment 4's (FL 13).
- *Rejected:* B, harm squares only in M3; C, condition squares only.

**4. Instant combos (agreed, Q4; change TR-25, TR-26): fire burns webs
and bedsheets away (K2), and lightning charges connected water (Charged Water, K3), in
M2.** No timers; the combo table is closed at 5 rows (6.8). *Rejected:*
B, burn-away only; C, neither.

**5. Lingering effects (agreed, Q5; change TR-27): nothing timed in M2;
environment-only Miasma Remnants in M3.**
- **M2 delivers the fantasy legally:** static harm squares, one-shot Keg
  bursts, gas-aura monsters (legal under ability 8.4), boss phase
  sockets, and the Q4 combos.
- **M3 Remnants**, only after the M2 rider and layer reports:
  - Miasma clouds only; only in themes whose 6 holds Poisoned;
  - fixed footprint ≤ 9 squares; 2-round countdown;
  - **never drift, spread or grow**; at most 1 per room;
  - **ignitable once** into a single fire event;
  - made only by the environment (Kegs, boss phases, segment-4 vents).
- **Told plainly:** this changes decided main 2 (flagged, TR-27), and a
  party-triggered cloud is player-made ground effect in substance.
- **Narrowed by segment 4 (change HZ-12; agreed, hazards Q1; M2
  (gated), else M3):** "nothing timed in M2" no longer covers one
  signature Stir per F2–5 floor at most (the Choir's Organ Swells, the
  Carnival's Pit Breathes). It acts in this round-end environment step
  (the Dream step), brought to M2 for Stirs only, announced a full
  round ahead and never in round 1; if its gates fail it falls back to
  M3 (hazards design 6.3).
- **Read by segment 7c (change LP-4; reading, no change):** "nothing
  timed in M2" holds. The Windfall's count runs in the Dream step from
  M3 only, as its room's one clock, and reopens nothing agreed (loot
  sources design 0.1, 4.7.4).
- **Rejected outright:** drifting gas, spreading fire, blood slicks,
  player-made surfaces, and ice in the Supper.
- *Rejected:* B, never.

**6. Room interiors (lead's call; stand as written; change TR-5, TR-17,
TR-18, TR-19).**
- **Piece and square classes:** Tall pieces block movement and sight and
  are the Hide spots; Low pieces block movement only and give cover;
  Furnishings; Kegs; hazard pieces and faces; Rough squares (2 movement
  to enter); Planks (Acrobatics shortcuts, owed to armour 7.3).
- **Placement.** Each template authors 2–3 terrain layers (FL 8.2). A
  new fixed-draw `terrain` stream picks them after LS13. A build-time
  validator, **V9 `RoomTerrain`**, proves every layer, so the runtime
  cannot fail (FL-15 pattern).
- **Clutter bands:** open 3–8%, standard 8–15%, cluttered 15–22% of
  interior squares *(illustr.)*. Cluttered fight rooms are at most 30%
  of a floor's fight rooms; a reservation step makes this and every
  other floor-level quota hold on every seed (6.2.5).

**Told plainly** (19.1; confirmed by the client on 2026-11-21): cover
is symmetric and can raise enemy
marksman damage; the terrain packages cost about +0.8 to +1.6 min per
floor on floors already over the clock gate, held by a net rounds gate;
the Vanguard rejection is reversed for objects; decided main 2 changes
in M3; and the rest of the list in 19.1.

### 0.2 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Cover: one tier, dot-product facing test, −2 to the roll, with the ignore, suppress and replace clauses; no cover on Portal squares; walls and units never give it; armour 11 gains a positional row | M2 | **agreed, Q1**; change TR-2, TR-4 |
| 2 | M1: Tall pieces block sight and movement, Low pieces block movement; no cover rule; nothing movable; no hazards | M1 | lead's call; TR-3 |
| 3 | Furnishings (light and heavy, all Low in M2) with Shunt (movement or attack mode), Kick (round pieces) and Smash; debris is object state | M2 | **agreed, Q2**; TR-6, TR-7, TR-8 |
| 4 | Kegs: Carnival lamp-oil keg and Gut swollen sac in M2; sturdy in M2; a burst never triggers a burst in M2 | M2 | **agreed, Q2**; TR-9 |
| 5 | Contact hazards (hazard pieces and faces) on floors 2–5. A failed save deals typed contact damage instead of collision; a success is an ordinary obstacle stop with **no Teetering** | M2 | **agreed, Q3**; TR-10 |
| 6 | Harm squares (walkable, damage on entry, edge save without Teetering) on floors 2–5; in the M2 square set | M2 | **agreed, Q3**; TR-11 |
| 7 | One environment damage instance per unit per action (the top-up rule, 6.4) | M2 | **agreed, Q3** (the cap); the top-up wording is a lead's call after the check; TR-13 |
| 8 | Clearway at least 2 wide in fight rooms; `FurnishingNeverSeals` reachability rule against the initial state | M1 / M2 | **agreed, Q2**; TR-17 |
| 9 | Rough (2 movement per square; costs never stack above 2) and Plank narrow ground (Acrobatics; failure stops you at the mouth) | M2 | lead's call; TR-11, TR-12 |
| 10 | AI: `NoCheapKill` (a scorer term, 6.9); destination square value; `usesFurnishings`; universal Smash of light pieces scored by path gain | M2 | **agreed, Q3** (`NoCheapKill`) and **Q2** (monster Smash); the rest lead's call; TR-15 (requests) |
| 11 | Boss phase (e) through phase sockets proven in both states; arena caps here, counterplay identity stays RS 13 and segment 9 | M2 | lead's call; TR-16, TR-8 |
| 12 | Web and bedsheet burn-away; instant Charged Water; closed 5-row combo table | M2 | **agreed, Q4**; TR-25, TR-26 |
| 13 | Remnants (Miasma only), environment-only, fixed footprint, countdown, ignite once; the round-end environment step comes to M2 for one signature Stir per F2–5 floor at most (hazards design 6.3) | M3; the Stir step M2 (gated), else M3 | **agreed, Q5**; TR-27 (decided text); narrowed by HZ-12 (**agreed**, hazards Q1) |
| 14 | Overturn (tippable Tall Furnishings), Haul, Shoot an object, Kegs in the other themes, depth-1 chains | M3 | lead's call; TR-6, TR-28 |
| 15 | Ownership: segment 3 owns the room-terrain system; segment 4 owns floor-scale density, drops, gall, Teetering counting and vents; segment 5 owns traps | — | lead's call; TR-1 |
| 16 | Floor-level quotas made constructive by a reservation step; sockets carry a rules key; contact and harm saves defined; cover placed in the attack pipeline | M1 / M2 | lead's call, after the check (19.3) |

---

## 1. Intent

The main design fixed one flat square grid shared by exploration and
combat, symmetric line of sight, no high ground and one flat rules level
per floor **(decided, main 4.3, 6.8)**, and refused spreading, evolving
or player-placed ground surfaces while allowing fixed condition squares
placed by floor generation **(decided, main 2)**. The conditions design
built those squares (11), the ability model kept abilities off the
ground (8.4), the weapons design deferred cover to this segment with a
simulator arm (9.5), and the floor layout design handed this segment
template footprints, start zones, protected squares and sight lines, and
2–3 terrain layers per template (FL 6.1, 8.2, 13). None said what stands
inside a room, what gives cover, what can be pushed or broken, or how
any of it is proven on every seed.

The answer in one sentence: **a room's interior is an authored terrain
layer of a few readable piece and square classes, picked by a fixed
number of seeded draws and proven at build time, so every room gives
fights a shape, cover and a place to push someone, without ever sealing
a path, surprising the player or spreading across the floor.**

## 2. Goals, non-goals, anti-goals

### Goals

- **Rooms have tactical shape:** cover lines, pillars, chokes, lanes and
  nooks that change where units stand (6.2).
- **The cover decision is made** and fits the armour envelope, the AI
  and symmetric sight (6.1).
- **The room is a weapon for everyone:** pushes have something to hit,
  props can move, and a few monsters use them too (6.3–6.5, 6.9).
- **Every result is previewed before commit** (6.12), and no single
  failed save or entry is lethal to the hero (conditions 10.4).
- **Every seed works:** terrain is a lookup over layers proven at build
  time; legality of moved objects is a rule, not an assertion (6.2.5,
  6.3.6).
- **Themes play differently:** one kit and one signature interaction per
  theme (7).
- **The clock is respected:** verbs are combat-only, cheap to read, and
  held by a net rounds gate (6.12, 10).

### Non-goals

- **Drop hazards, floor-wide hazard density, gall, Teetering counting,
  vents, traps, locks and checks, encounter fill, loot and boss
  interiors.** Those are segments 4–12; this document hands them
  classes, caps and hooks (13).
- **Height, stand-on pieces, multi-level rules geometry.** Main 6.8
  **(decided)**.
- **Anything timed or spreading on the ground in M2.** Main 2
  **(decided)**; conditions 11.1 and ability 8.4 **(agreed)**. Narrowed
  by segment 4: one gated signature Stir per F2–5 floor at most, which
  changes no square (change HZ-12; M2 (gated), else M3; 6.6).
- **Final names, numbers and art:** the naming pass, the simulator and
  the art team.

### Anti-goals

- **Surface soup:** combos and overlaps no one can read (the Divinity:
  Original Sin 2 failure). The combo table is closed (6.8).
- **Barrelmancy:** carrying, stacking and throwing props.
- **Murder by pinball:** long chains, caroms, unforecast environment
  kills of heroes.
- **A sealed room:** any arrangement of moved objects that cuts a path,
  a flee exit or a start zone (`FurnishingNeverSeals`).
- **Fake tiles:** dressing that looks like a rule, or a rule that looks
  like dressing (RC1).
- **A smash-everything chore:** Furnishings never hold loot.

## 3. Constraints

### 3.1 What the parents already fix

The 19 approved documents were grepped for cover, push, shove, forced
movement, hazard, square, Teetering, collision, obstacle, terrain and
Hide, and each hit was read in context.

- **Grid and geometry:**
  - main 4.3 **(decided)**: 8 directions, diagonal cost 1, symmetric
    line of sight and fog, no high ground; Jumping and Flying cross
    hazards and obstacles tagged crossable;
  - no corner cutting: the M0 skeleton plan's `CornerCut` rule, walls
    only (FL 3.1 cites it beside main 4.3; it is not decided text);
  - main 6.8 **(decided)**: one flat rules level per floor;
  - FL LG18: walkable squares exactly coplanar;
  - FL 8.0: square 1.5 m, full wall 2 squares, stub 0.25;
  - FL 8.1: tall pieces fade or stub, never slice; `CutawayPockets` ≤ 3.
- **Surfaces** (main 2 Non-goals, **decided**): "Spreading, evolving or
  player-placed ground surfaces. Fixed condition squares placed by floor
  generation are allowed." The same ban appears in:
  - stat model 2 Non-goals (agreed): "Spreading or evolving terrain
    surfaces, such as burning oil that spreads or conducting water. Held
    back until riders and layers are proven." Fixed condition squares
    are allowed; "they never spread or change, except that fire burns an
    oil square to plain floor";
  - ability model 2 and 8.4 (agreed: "Nothing is left on the ground");
  - conditions 2 and 11.1 (agreed: "Squares never spread or change",
    with the single exception of fire on oil in 11.4, which "keeps every
    square free of timers");
  - consumables 12 (flasks are "never terrain");
  - the Arcanist design: "Never: ground zones (the one terrain change is
    the settled fire on oil)";
  - RS 7.6 (agreed: breakable walls and spreading surfaces refused for
    M2).
- **Rejections:**
  - Vanguard 8.3: "breakable terrain (rejected: the fixed-terrain
    non-goal)"; lethal chasms, Over the Edge auto-entry, carom pushes
    and Throw are Later;
  - Vanguard 9 table, "Rejected" row: "destructible terrain";
  - Shade 9 table: flanking, facing, vision cones, smoke clouds;
  - weapons 9.3: no improvised throw;
  - stat model 6 and items 5: no weights.
- **Forced movement:**
  - conditions 10.1: stops at walls, obstacles and units. Collision
    deals band bludgeoning once, only when a non-verb part contributed
    (weapons 7.2 rule 4; Battering Force is the one exception).
    "Stopping before a hazard is not stopping early and deals no
    damage." The 3-square sum per hit. M1 pushes stop before hazards;
  - 10.2: hazard-edge Reflex save; success stops on the last safe square
    with Teetering;
  - 10.3 landing; 10.4 no single failed save or entry lethal to the
    hero;
  - verb-sourced movement (weapons 7.2 rule 4): the weapon verbs, verb
    signatures, the shield bash's Push, Bowl Over (AR-49, AR-52) and
    Empower; the Arcanist's Gust and Reel count as verb-sourced for
    collisions (ARC-7). Upheaval, Squall (Arcanist 8.2, 8.3) and Give
    Ground (Herald) are non-verb.
- **Hazards:**
  - conditions 11.2: squares apply minor or major conditions only;
  - 11.3: every harmful-condition square, and every known armed trap, is
    a hazard;
  - 11.5: the M2 square set is water, web, fire, oil, gall (agreed);
  - 11.6: source `environment`.
- **Budget:**
  - conditions 16 and 18.1: 32 conditions in M1 + M2, full;
  - at most 6 harmful conditions per theme, counting terrain;
  - 8 of 9 themes at 6/6, the Kennel at 5;
  - RT-14 (Teetering counting) pending, with named fallback drops (RS
    8.0; the Supper drops Poisoned, the Heart drops its bedsheet
    squares).
- **Weapons 9.5 (lead's call):** "No cover rule in M1. Obstacles block
  line of sight only, and units never give cover. Revisit in M2 … using
  the simulator's party damage share by range band (21.3)."
- **Armour 11 item 1 (lead's call, confirmed to stand):** sustained
  maximum party AC leaves ≥ 35% hit chance; with the one gear roll-mode
  effect ≥ 10%; charged ≥ 15%. Armour 8.1 rule 6: one roll-mode effect
  per attack, with the charged exclusion.
- **Hide** (stat model 7.1, agreed): needs a broken line of sight;
  Vanish (CL-12c) waives it. Hidden ends in sight within 3 at turn end
  (conditions 18.7). Shade parity rides on the Hide rate (Shade D4,
  `hideSuccess` 700‰).
- **AI:** enemies 11.1, a one-step integer scorer with Exposure and
  Hazard path terms. Conditions 17.3 values terrain by forced movement
  and keeps unvaluable content out of kits. Stupidity floors in enemies
  11.3. The Holder "shoves toward hazards (M2)" (enemies 17.3).
- **Bosses:** enemies 17.5 (settled): pillars or cover that block line
  of sight; at least one counterplay feature (a brazier against an ice
  layer, a chasm edge for shoves); optional set-piece hazards within the
  theme's 6; phase (e) "change arena terrain within the theme's 6, never
  on a party unit's square".
- **Lethality floors:** DH 10.4 (out-of-combat party floor), DH 11.4
  (Downed bodies are inert), DH 17.6 (lethal tags), DH 17.7 (ally
  confirmation).
- **Readability charter** (RS 7.4, agreed):
  - RC1 no fake tiles; dressing never mimics a rule; no floor particle
    implies a condition;
  - RC2 reserved role-prop silhouettes (shield, door, tray, coffin lid,
    chair);
  - RC3 keyhole; RC4 saturated hue only on hazard squares and attacks;
    RC5 one glyph per square kind; RC6 units > telegraphs > hazards >
    floor > walls;
  - RS 6.3 (lead's call): one home theme per square kind; "the Gaol,
    Kennel and Supper are deliberately dry". RS 8.9 (agreed, RS Q4): the
    Supper is "Deliberately dry (no squares)" and has no ice square. RS
    8.4: the Kennel's `signatureFeature` is "kennel gates and fallen logs
    (cover …)", its `signatureFamily` "cover gates". RT-20: floor 1 is
    dry.
- **Floor layout:**
  - FL 6.1: terrain runs after LS13 and before enemies 10.5 step 2,
    never on `protectedSquares` or `protectedSightLines`;
  - FL 11.2 `ProtectedIntact`; FL-15: the runtime asserts only what is
    proven at build time;
  - FL 6.3: free space ≥ 6 squares per unit;
  - FL 6.4 `RoomMix`: ≥ 1 open fight room ≥ 11×11; ≥ 1 narrow guard
    post;
  - LG20: 3×3 landing, start zones ≥ 7 from the entry mouth;
  - LG12 `fleeExit`; `PartyEscape`; `NoSealedPocket`;
  - FL 8.2: 2–3 segment-3 terrain layers per template;
  - FL 11.1: the save stores only Portal state;
  - FL 9.1: shifting rooms rejected.
- **Clock** (FL-16): `ExploreClicks` ≤ 40 and `ExploreSeconds` ≤ 3.5 min
  at party 4 on floors 4–5. The companions toy shows floors at about 27
  min against the 20-minute gate (companions 7.4, agreed).

### 3.2 What the approved documents owe this design

Each is met in section 14.

| # | Owed | Source |
|---|---|---|
| O1 | The cover decision, with the per-grammar range-band arm | weapons 9.5, 26; FL 11.4 arm 2, 13, 14 |
| O2 | Interiors, terrain squares and density per theme and floor | RS 8.0, 13; FL 13 |
| O3 | `coverProps[]` per theme | RS 8.0, 8.3–8.11 |
| O4 | Cover for the Circuit's `coverRoom` (M1) | enemies 10.4; FL 6.7 |
| O5 | DEP-A1: an interactive square within 6 of the start in ~40% of non-boss rooms on floors 2–5 | Arcanist 16, 19; RS 10.7 |
| O6 | ≥ 3 Acrobatics-gated choices per floor from M2 (layout gives 2 Squeezes; segment 3 gives "narrow ground") | armour 7.3; FL 13; FL-22 |
| O7 | Wrecker walls and edges near main-path fights; value by theme hazard density | Vanguard 8.3, 18; FL 13 |
| O8 | Hazard edges and crossable hazards from floor 2 (Ledgewalker, Leaper) | trinkets 23 |
| O9 | Boss arena line-of-sight blockers and the counterplay-feature rules | enemies 17.5, 25 |
| O10 | Lurker hide spots (Gaol cots, Archive shelves, Wake coffins) | enemies 25; RS 8.3, 8.7; FL 7.2 |
| O11 | Choir and Carnival entry caps | RS 8.6, 8.8; FL 13 |
| O12 | Supper bays with no two consecutive table layouts alike | FL 7.8 |
| O13 | Open-room Cleave and reach space; corridor and guard-post lines | weapons 26; FL 6.4 |
| O14 | The terrain step's stream and draw count | FL 6.1; R-SEED-3 |
| O15 | The Kennel solo-floor ranged risk, through the joint range-band arm | FL 7.3; FL 15 risk 11 |
| O16 | `AcrobaticsChoicesPerFloor` report | FL 11.3 |

### 3.3 Tensions found and their resolutions

| Id | Tension | Resolution |
|---|---|---|
| TT1 | The client's lingering, spreading gas and fire against main 2 (decided) and four agreed texts | M2 has no timed effects. M3 Remnants, environment-only and non-spreading **(agreed, Q5)**. Drift and spread rejected (6.6) |
| TT2 | Movable and breakable props against "breakable terrain (rejected)", conditions 11.1 and the static FL proofs | Furnishings are objects, not squares. Walls never break. Clearway plus flood fill keep every proof valid. One consolidated 11.1 change (TR-8) lists debris as a change, not a clarification **(agreed, Q2)** |
| TT3 | Knock-into hazards have no schema for damage-only hazards and would run up against `HewCeiling` | A new obstacle-hazard branch of 10.2 (TR-10). Contact band = collision band + 1 step. Verb-only pushes take only the collision band. `HewCeiling` gains an explicit contact term (6.10; TR-30) |
| TT4 | Full theme 6s (8 of 9) and pending RT-14 | Every piece is damage-only or reuses a condition in the theme's 6. Damage-only hazards give no Teetering. Harm squares reach Teetering only through Brink, which RS 8.0 already counts in every theme, so they do not depend on RT-14 (6.5.5) |
| TT5 | Gaol "cell bars" cover against FL 8.1 "bars only on Grilles" | Gaol cover is desks, benches and foot stocks (TR-22, told plainly) |
| TT6 | Wake "closable coffins (hiding spots)" against FL 7.4 "sealed coffins never hold a unit" | Coffins are fixed Low cover. Hiding comes from Tall canopies and Drapes (TR-22, told plainly) |
| TT7 | Supper "braziers as cover props" with no Burning in its 6 and RC4's hue rule | Braziers are Hot hazard pieces: cover plus fire contact damage, rider-free. Fire ends Chilled and Frozen (told plainly). RC4 extended to contact hazards (TR-20) |
| TT8 | Line-of-sight blockers move the Shade Hide rate, Arcanist areas, ranged dominance and Skewer lines all at once | Hide-spot band in V9; way-round ≤ 6; `RoomMix` open core and lane; per-grammar range-band arm |
| TT9 | Obstacle density raises the Wrecker's `collisionRate` above `HewCeiling`'s 400‰ | Cluttered fight rooms ≤ 30% per floor (KP3, a rule made constructive by the reservation step, 6.2.5); `HewCeiling` re-run with the contact term (6.10) |
| TT10 | Clutter against free space, `RoomMix`, LG20 and `CutawayPockets` | V9 proves each after clutter; the open room keeps a clear 5×5 core |
| TT11 | Boss phase (e) "change arena terrain" (settled) against conditions 11.1 "never change" and the Arcanist's "one terrain change" | Named a sanctioned exception through phase sockets proven in both states (TR-16, TR-8) |
| TT12 | RT-14 refusal strips the Heart of bedsheets, so floor 5 loses its terrain | Floor 5 keeps contact hazards, Furnishings, Plank and harm squares. DEP-A1 holds at 0.39–0.51 (10), and KP2 now holds in every F2–5 fight room by construction |
| TT13 | DEP-A1 against the deliberately dry Supper | Interactive features count (TR-21). Supper braziers, tureen and carving face close the floor-4 gap |
| TT14 | Prop interactions against the clock | Combat only; verbs on hover, or as a chip only when they pay off; a net rounds gate |
| TT15 | Dressing props draw from the display stream with no rules effect | Every object on a walkable square is a rules piece from the core `terrain` stream (TR-20, TR-17) |
| TT16 | Verb-only pushes (Gust, Reel, Push, Hook, Bowl Over) deal no collision damage, so terrain gives them no payoff | A failed save against a contact hazard deals the collision band, typed, even for verb-only pushes. Capped per action (TR-10) |

### 3.4 Terms

| Term | Meaning |
|---|---|
| **Piece** | A rules object that occupies one square of a room: a Tall piece, a Low piece, a Furnishing, a Keg or a hazard piece. A 2×2 cluster is four pieces in one group. Never a unit (6.2.1) |
| **Tall piece** | A piece at full wall height that blocks movement and line of sight (FL 8.1's generic term, kept) |
| **Low piece** | A fixed piece ≤ 0.6 squares high that blocks movement only and gives cover from M2; it never moves or breaks |
| **Furnishing** | A movable, breakable piece of Low height, a class of its own beside Low pieces: light or heavy, sometimes `round`, sometimes `sturdy` (6.3) |
| **Keg** | A heavy, sturdy Furnishing that bursts once (6.3.5) |
| **Contact hazard** | A hazard piece or a hazard face: non-walkable, damage on a failed save when forced into it (6.5) |
| **Hazard piece** | A fixed, sturdy Bladed or Hot piece; also a cover-giver. It never moves or breaks |
| **Hazard face** | A tagged side of a wall or Tall piece; adjacent only to the single square directly in front of it |
| **Harm square** | A walkable, condition-free square that deals the harm band on entry; a hazard (6.5) |
| **Rough** | A walkable square that costs 2 movement to enter (6.2.6) |
| **Plank** | A 1-wide line of narrow-ground squares gated by Acrobatics in combat (6.2.6) |
| **Obstacle** | In conditions 10.1: a wall, a partition, a Tall or Low piece, a Furnishing, a Keg or a hazard piece (TR-5) |
| **Cover-giver** | A square holding a Low piece, an intact Furnishing or Keg, a hazard piece, or a Tall piece at a peek (CV3) |
| **Covered** | The state of a target against one attacker under CV2: the attack takes −2 |
| **Clearway** | The authored, connected set of floor squares joining every Portal, landing, start zone, `fleeExit` and mouth; ≥ 2 wide in fight rooms; never blocked (6.2.2) |
| **Terrain layer** | An authored interior for one room template: its Clearway, sockets and phase sockets (6.2.5) |
| **Socket** (terrain) | A square or group in a layer that a piece or patch of a fixed class may occupy; distinct from FL's Portal sockets and enemies 10.4 slots |
| **Phase socket** | An arena socket that changes state on a boss phase (e), proven in both states (6.11) |
| **Interactive feature** | For DEP-A1: a condition square, a contact hazard, a harm square or a Keg (TR-21). **Pending request (segment 4 asks; change HZ-31; M2; hazards design 6.15, O13, 18):** segment 4's Stirs, Cords, Voids and vents also count as interactive features for DEP-A1 and KP2. This document owns the definition; nothing changes until this owner rules (cross-document comparison 2026-11-23, CC-7) |
| **Environment damage** | Damage from a collision, a contact hazard, a harm square, a Keg burst, Charged Water or (M3) an ignition; source `environment`; rider-free (6.5.2) |
| **Collision band / contact band / harm band / Keg band** | The flat damage values of 11.1: collision 2/3/3 by the pusher's level band (the Vanguard design's indexing, kept); contact 3/4/4 and harm 2/3/3 by the level band of the floor's lowest expected level; Keg 4/6/8 by floor (3.4 indexing is a lead's call after the check). A band halved by a successful save (Reflex for half, the Keg's `reflexHalf`) is halved per part, rounding down, with a minimum of 1 unless immune (death and healing design 20.3 step 2): harm 2/3/3 → 1/1/1, Keg 4/6/8 → 2/3/4. If the low bands then feel too strong, the band values are retuned, not the rounding **(agreed 2026-11-28, roadmap D-22; M1-11 rulings 1.2)** |
| **Hazard-edge save** | Any Reflex save against forced movement into a hazard: the 10.2 save, the contact save and the harm-square save (6.5.2). Every rule that reads "hazard-edge save" reads all three |
| **Heft** | A Furnishing's Athletics difficulty: light 8, heavy 12 *(illustr.)* |
| **Remnant** | An M3 environment cloud with a fixed footprint and a countdown; Miasma is the only kind (6.6) |
| **`causedBy`** | The unit whose action began an environment damage event (9.2) |
| **Signature interaction / terrain Law** | One authored terrain beat per theme (7.1) |

---

## 4. How other games handle cover, props and environmental hazards

Details recalled rather than web-checked are marked *(recalled)*. Web
checks covered Baldur's Gate 3 Shove, the Frosthaven icy and hazardous
terrain rules, XCOM cover and the Into the Breach postmortem.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| XCOM: EU / XCOM 2 | Directional half/full cover; flanking removes it; shield icons on move hover; destructible cover; explosive cars and barrels | Directional cover by geometry; the shield shown at move time; areas as the counter | Two tiers on a d20; "95% miss" noise; grenade cover-spam |
| XCOM: Chimera Squad | Authored breach rooms with authored cover; deterministic timeline | Authored layers over procedural clutter (proofs) | — |
| Into the Breach | Push 1, bump damage 1; environment kills (water, chasm, mountains); every intent telegraphed; no combat randomness | Exact push previews; flat bump damage; hazards as a symmetric tool | Small boards that feel solved |
| Hoplite | Deterministic bash pushes into lava; perfect information | Knock-into as the main verb of a class (the Wrecker) | — |
| Gloomhaven / Frosthaven | Obstacles block movement only; difficult terrain costs 2; hazardous terrain damages on entry; icy terrain slides +1 and re-triggers; pushes chosen by the pusher; jumping exempt | Rough at 2; damage-only harm squares; Glaze (future frost theme); the jumping exemption (already main 4.3) | — |
| D&D 4E | Push, pull, slide; a save to fall Prone at the edge of hindering terrain; terrain powers as one-action verbs | The edge save; verbs that cost one existing action slot | "Murder by pinball" chains (we keep the 3-square cap and no carom) |
| D&D 5E / 5.2 SRD | Half, three-quarter and total cover; difficult terrain ×2; object AC and HP by material; clouds such as Cloudkill and Stinking Cloud | Binary cover; breaker types by material | Three tiers; object HP bookkeeping; moving clouds argued at the table |
| Pathfinder 2E *(partly recalled)* | Lesser, standard and greater cover; Take Cover action; forced movement into hazards only if the effect says so | Explicit rules on what may push into hazards | Cover from creatures; a Take Cover action (the budget is full) |
| Lancer *(recalled)* | Soft and hard cover by adjacency; melee ignores cover | Adjacency-based; melee exempt | Object HP |
| Warhammer 40k / Kill Team *(recalled)* | One keyword per terrain piece, one rule per keyword | One keyword and one glyph per piece class | — |
| Divinity: Original Sin 2 | Surfaces and clouds that combine and spread | A closed, small combo vocabulary | Surface spam, unreadable overlaps, "the environment decides the fight"; confirms the approved non-goal |
| Baldur's Gate 3 | Shove (a bonus action) into chasms; explosive barrels; carrying and stacking barrels | Shove into hazards, priced at one attack (as approved) | Barrelmancy; AI shoving heroes off ledges every fight (`NoCheapKill`) |
| Wasteland 3, Jagged Alliance 3 | Explosive and toxic barrels, destructible cover | Barrel moments | Barrel cheese (caps, spacing, no carrying) |
| Gears Tactics, Mario + Rabbids | Directional cover; destructible cover; 0/50/100% pips | Binary readable pips; breaking cover as counterplay (Smash) | — |
| Enter the Gungeon | Flipping a table makes directional, breakable cover | An action converts or moves cover (Shunt) | — |
| Phoenix Point | Physical cover with destructible parts | — | Physics simulation; fiddly and slow |
| DCSS | Clouds with a fixed footprint and a timer that mostly don't spread | The Remnant model | — |
| Brogue, Caves of Qud | Gases diffuse by concentration; fire spreads through grass | — | Cellular-automaton chaos; deaths players cannot predict |
| Disgaea | Lift-and-throw; Geo panel chain transforms | — | Chain transforms; throwing |
| Invisible Inc., Shadow Tactics | Deterministic hide spots; environmental kills | Hide needs a visible, explicit rule (Tall pieces) | Vision cones (rejected by the Shade design) |
| Fire Emblem, Battle Brothers | Fixed terrain bonuses as roll changes | Cover as a roll change | Terrain-bonus randomness that goes stale |
| Tactics Ogre, FFT, Solasta, Mordheim | Height, ledges, knockback off cliffs | — | Height (main 6.8 decided: one flat rules level) |
| Triangle Strategy | Fire melts ice, lightning on water | Each combo gets a teaching beat | Many transforms |
| Wildermyth *(recalled)* | Props as spell fuel; authored per object | Per-theme authored kits | — |
| Sly Flourish (DM advice) *(recalled)* | "Three fantastic terrain features per fight" | Few, meaningful features per room; one obvious verb each | Dense flavour clutter |

**Lessons, in one line each:**
1. Cover reads only when it is binary, directional by geometry and shown
   on the move preview.
2. Knock-into play is fair only with exact previews, flat damage, a save
   and hazards as a symmetric tool.
3. Surfaces and gas fail on readability the moment they spread or
   combine.
4. Barrel play needs caps on chains, carrying and stacking.
5. Destructible maps fight proof-based generation; breakable objects on
   top of proven walls are the safe middle.

---

## 5. Terrain models

### 5.1 The models

- **Model A, light touch.** One flat cover tier by line crossing; one
  push-only Furnishing kind per theme; a small universal knock-into set;
  Remnants in M3 only.
- **Model B, full tactical (XCOM + Into the Breach).** Two-tier
  directional cover with a flank bonus; objects with hit points; carry
  and hurl; Surfaces and Clouds with deterministic spread; a combo
  table.
- **Model C, surfaces and objects first.** +2 AC cover; Furnishing
  families; Outpour (breaking a Furnishing creates its home square);
  drifting Vapour clouds; a six-row combo table.
- **Theme kits synthesis.** A per-theme catalogue on a universal kit.
- **New-mechanics package.** Fourteen low-cost mechanics.

### 5.2 Verdict and judges' scores

Three judges scored A, B and C; two scored the kits and the
new-mechanics package. The table shows means (10 = best; "build cost" is
scored high when the cost is low).

| Model | Tactical depth | Readability | Fairness | Theme expression | Determinism and AI | Fit with docs | Pacing | Build cost (low) | Sum |
|---|---|---|---|---|---|---|---|---|---|
| A, light touch | 6 | 9 | 8.7 | 6 | 9 | 9 | 8 | 8 | **63.7** |
| Theme kits | 7 | 8 | 8 | 9 | 8 | 8 | 7 | 6 | 61.0 |
| New-mechanics | 7 | 8 | 7.5 | 7 | 8 | 7.5 | 7 | 7 | 59.0 |
| C, surfaces first | 8 | 6 | 7 | 8 | 6.3 | 5 | 6 | 5 | 51.3 |
| B, full tactical | 9 | 4 | 6 | 8.7 | 5 | 3 | 4 | 2.7 | 42.4 |

All three judges picked **Model A as the chassis**, with the kits as its
content layer and grafts from C and the new-mechanics package **(lead's
call; stands as written)**.
- B fails a hard check: its promise that every timed effect ends within
  4 rounds is false (fire burns 5–17 rounds).
- C's +2 AC fails armour 11 as written, its Outpour reverses decided
  main 2 in M2, and its drift needs a new AI term.
- **Model B is a long-term catalogue**, drawn from item by item after
  the M3 reports, never as a package.

### 5.3 Grafts

**(lead's call; stand as written)**

| Graft | From | Why |
|---|---|---|
| Dot-product facing cover test in place of line crossing | B, C, kits | Cover visible on ~15–35% of ranged attacks instead of 1–10% |
| Cover suppressed against charged AC; replaced by reaction AC | new-mechanics; balance critique | AC envelope; correct timing |
| No cover on a Portal square | C | Damps doorway holds (FL-21 (c)) |
| Light and heavy Furnishing families per theme; Smash (binary) | kits, C | A's single push-only kind is thin; Smash counters entrenched cover |
| Shunt in two modes (movement or attack); Kick in M2 | fun critique | Props otherwise never beat an attack |
| Kegs in the Carnival and Gut in M2; sturdy in M2; no burst triggers a burst | kits; balance critique | Barrel moments without movable-keg chains |
| Hazard faces on walls and Tall pieces | kits, C | Theme expression; Wrecker targets |
| Contact-hazard sockets near enemy starts and lanes | numbers pass | Otherwise knock-ins are too rare to be felt |
| Obstacle-hazard branch: failure = contact damage; success = ordinary obstacle stop, no Teetering | balance and consistency critiques | Honest change text; RT-14 decoupled; Brink not inflated |
| `NoCheapKill` only, phrased as "does not prefer" | B; numbers pass | Removed every residual environment hero down |
| One environment damage instance per unit per action | balance critique | One cap instead of many |
| Web burn-away and Charged Water | C, B | Archive and Choir Laws on the map; cheap and bounded **(agreed, Q4)** |
| `causedBy` kill credit; boss phase sockets | new-mechanics; A | Fair credit; reconciles enemies 17.5 |
| Clearway ≥ 2 wide in fight rooms; universal monster Smash of light pieces | balance critique | No party-built single-file funnels the AI cannot answer |
| Cluttered rooms ≤ 30%; DEP-A1 3–6 ring rule | numbers pass | Wrecker parity; DEP-A1 |
| Per-theme signature interaction and one terrain-using enemy per theme | fun critique | Themes do not play the same |
| Floor-1 teaching room; typed Low pieces on floor 1 | fun critique | Solo players learn terrain matters |

---

## 6. Terrain rules in full

### 6.1 Cover (agreed, Q1; change TR-2, TR-3, TR-4)

**CV1. M1.** No cover rule (the weapons 2 non-goal is kept for M1). Tall
pieces block sight and movement; Low pieces block movement only; nothing
gives cover; units never give cover. TR-3 rewords weapons 9.5 bullet 1,
because FL 7.3's Circuit glade names fallen logs. The Kennel `coverRoom`
gets its M1 "cover" from Tall trunks and root plates, as line-of-sight
breaks and Hide spots **(told plainly)**.

**CV2. Who is Covered (M2).** Target T is Covered against attacker A
when all of these hold:
1. the attack is a **ranged attack roll**: a ranged or thrown weapon
   attack, or a ranged spell attack. Melee, reach, opportunity attacks,
   saves, areas, flasks, bursts, auras and no-roll effects (Needles)
   never face cover;
2. A is at Chebyshev distance ≥ 2 from T and has line of sight to T;
3. some square C, 8-adjacent to T, holds a **cover-giver**, and C
   **faces A**: with c = C − T and a = A − T, the integer dot product
   d = c·a > 0 **and** 2·d² ≥ |c|²·|a|², so C lies within 45 degrees of
   the line from T to A (45 degrees exactly counts) **(lead's call, after
   the check:** the half-plane test d > 0 alone covered a target against
   about 270 degrees behind the middle of a Low line, including shots
   along the line and from the flank, so getting round to the side
   never removed cover**)**;
4. C is not 8-adjacent to A (no cover from the attacker's own crate);
5. T is not standing on a Portal square.

**Test vectors** (T at the origin, x east, y north; the unit tests and
`PreviewExactness` use them; the move-hover shields of 6.12 are drawn
from the same function):

| Attacker offset A − T | Facing squares C − T (each Covered if it holds a cover-giver) |
|---|---|
| (6, 0) | (1, 0), (1, 1), (1, −1) |
| (6, 1) | (1, 0), (1, 1) |
| (6, 3) | (1, 0), (1, 1) |
| (6, 6) | (1, 1), (1, 0), (0, 1) |
| (3, 1) | (1, 0), (1, 1) |
| (2, 0) | none (every facing square is adjacent to A, item 4) |
| (−1, 8) against (1, 1) | not facing (d = 7, 2·49 < 2·65) |

The other directions follow by symmetry. At most 3 squares ever face one
attacker, and at least 1 does from every direction at range ≥ 3.
**Told plainly:** TN1's 11–37% was measured on the half-plane test; the
narrowed test is owed a re-run and will show fewer covered attacks.

**CV3. Cover-givers:** Low pieces, intact Furnishings and Kegs, hazard
pieces, and Tall pieces (only when the line of sight to T still exists:
a "peek"). **Never** walls or partitions, units (weapons 9.5 bullet
kept), Grilles (a Grille still blocks every attack, FL-7), debris, or
squares of any kind.

**CV4. Effect.** −2 to the attack roll *(illustr.; lever `cover.effect`
= off | minus2 | disadvantage; default minus2)*.
- Cover never stacks with itself.
- **Where it sits (lead's call, after the check).** Cover is computed at
  a new **step 2b** of the weapon pipeline (weapons 7.5), right after
  step 2 fixes the **net** roll mode, and at the matching point of
  ability model 14.3 for spell attacks (after the roll mode, before the
  roll). It is recorded with the mode in `AttackDeclared` (TR-2).
- **Ignored when the attack already has disadvantage:** the **net** mode
  after step 2 (an attack with both advantage and disadvantage is
  normal, so cover applies). The preview shows the disadvantage icon or
  the cover pip, never both.
- **Suppressed against a target whose charged AC bonus is active**
  (Raise, Fortified), which is known when the attack is declared.
- **Reaction AC: the larger applies, never both.** In the reaction
  window (weapons 7.5 step 5, armour 10.4), the defender's protection
  against a Covered attack is max(cover 2, the reaction AC bonus), never
  their sum:
  - the window evaluates every offer against the roll **with cover
    already applied**, so `ifItChangesOutcome` fires only if the
    reaction's bonus minus 2 turns that roll from a hit into a miss;
  - a reaction or step-0 window fill whose bonus is ≤ 2 is **not
    offered** against a Covered attack: it is not spent and does not
    close the window, so the next candidate in the window order is
    considered;
  - a bonus above 2 replaces cover for that attack.

  The attack preview reads "Cover −2 (a reaction can only replace it)".
  **Told plainly:** a +2 Parry does nothing against a Covered attack and
  is never spent on one; this is the agreed "replaced, not added" made
  exact.
- Cover is symmetric: monsters are Covered by the same rule.

**CV5. Why the roll, told plainly.** −2 to the roll and +2 AC give the
same hit chance. Both fail the armour 11 sustained row as written (level
1: AC 18 against +4 gives 35%, and 25% in cover). The roll form is
chosen because:
- the shown AC never changes;
- the enemies 6.4 guards, Puncture's "visible AC − R" and the charged-AC
  rows keep reading gear and class AC;
- cover is positional, like Prone and Dodge.

Armour 11 item 1 therefore gains a **positional row**, "sustained
maximum AC + cover leaves ≥ 25%", met exactly at level 1. Armour 8.1
rule 6's charged exclusion is extended to cover (TR-4, armour owner;
**agreed, Q1**).
- With the ignore clause, the gear roll-mode row stays at 12.25%
  (passes).
- With the suppress clause, the charged row stays at 15% (passes).
- The judges' notes saying that the −2 form "passes armour 11 as
  written" are corrected here.

**CV6. Why not disadvantage, and why not two tiers.**
- Disadvantage nearly doubles the floor-1 solo Arcanist's down rate
  (7.2% to 13.4% on the toy; Mote is a spell attack) and costs the most
  fight rounds. It stays a simulator arm (option B of Q1, rejected).
- A three-quarter tier is a near-dead number on a d20, and full cover
  already exists as "no line of sight".
- A flank bonus is not added: flanking emerges from geometry. The
  rejection of "flanking, facing" is in the **Shade 9 table**, not the
  Vanguard design, as some drafts cited. **Told plainly:** directional
  cover makes flanking positionally important without any flanking rule.

**CV7. What cover does to other systems.**

| System | Effect |
|---|---|
| Symmetric line of sight (main 4.3, decided) | Untouched: cover is roll maths, never sight |
| Ranged balance | Every toy arm keeps range-band share within ±25%. Launcher damage stays ≥ 1.01× the greatsword reference, far from the 75% Precision line (10). Cover is symmetric: it can **raise** enemy marksman damage by 5–15%, because covered marksmen live longer. **Told plainly:** cover is not a party buff by default |
| Shade | Hide unchanged: it still needs a line-of-sight break. "Hide behind cover" rejected for M2 (a free Vanish for every class, CL-12c; it would also help lurkers); Later with NM-9 Gloom. A Hide attempt next to Low cover greys out with "Needs a tall piece to hide" |
| AI | No new term. Exposure reads the resolve preview, so Kiters and marksmen seek cover and Raiders flank |
| Arcanist and Herald | Areas and saves ignore cover: the anti-marksman role, with no new rule |
| Pavise | Becomes designable (Later, armour owner) |
| Shieldwall and reaction AC | The larger of cover and the reaction bonus applies (CV4); never both |

**CV8. Placement rules that keep cover fair** (V9; change TR-18):
- every fight room has ≥ 3 cover-giving squares on the party half and ≥
  3 on the enemy half *(illustr.)*. **The halves (lead's call, after the
  check):** the room is split by the perpendicular bisector of the
  segment from the centroid of `partyEntrySquares` to the centroid of
  the enemy start zones; a square on the bisector belongs to the party
  half;
- **every enemy start socket that is Covered from any party-half square
  has a flank square** that is not Covered against it, reachable in ≤ 6
  path squares from the Clearway *(illustr.)*. Every start socket is
  treated as possibly a marksman or a Kiter, because who stands on it is
  decided after terrain (enemies 10.5, segment 6). **Start sockets
  (reading, change EP-5):** this rule and the floor-1 rule below range
  over the start sockets segment 6 derives from FL `startZone` squares
  (walkable, off the Clearway, never a socket piece's square)
  (encounter placement design 6.6.1). **Pending request (change EP-6;
  the enemies owner rules; M2):** the rule extends to segment 6's
  fall-back squares, each with a flank square within 6 (encounter
  placement design 6.6.3). Nothing changes until that owner rules;
- **floor 1: no monster start socket is Covered against the landing:**
  for every landing square L and every start socket S with line of sight
  from L and Chebyshev distance ≥ 2, CV2 with A = L and T = S is false.
  This keeps the solo floor fair; it is a hook for segment 6.
  **Reading (change EP-6; the RT owner confirms):** it governs start
  sockets only; an F1 fall-back square may be Covered (encounter
  placement design 6.6.3).
- **These are initial-state guarantees.** Furnishings move in play; the
  runtime keeps the rules of 6.3.6, not these counts.

### 6.2 Room interior vocabulary and template placement (change TR-5, TR-11, TR-12, TR-17, TR-19)

**(lead's call; stands as written)**

#### 6.2.1 Piece and square classes

| Class | Blocks movement | Blocks sight | Gives cover (M2) | Moves or breaks | Crossable by Jumping or Flying (main 4.3) | Kit height | Wave |
|---|---|---|---|---|---|---|---|
| **Tall piece** (pillar, 2×2 pillar cluster, book column, wardrobe, high table, rib pillar, thicket trunk) | yes | yes | yes, at a peek (CV3) | never in M2; tippable Tall Furnishings in M3 | no | full wall height (2 squares), with stub and cutaway states | M1 |
| **Low piece** (pew row, desk, log, bench, coffin on catafalque, dining table) | yes | no | yes | never | yes where tagged `crossable` | ≤ 0.6 squares | M1 (as a blocker); cover from M2 |
| **Furnishing** (light or heavy; some `round`) | yes | no (Low only in M2) | yes while intact | Shunt, Kick, Smash (6.3) | yes | ≤ 0.6 | M2 |
| **Keg** (exploding Furnishing class) | yes | no | yes | bursts once (6.3.5); sturdy in M2 | yes | ≤ 0.6 | M2 (Carnival, Gut) |
| **Hazard piece** (Bladed or Hot; fixed, sturdy) | yes | no | yes | never | yes (landing on it is never possible) | ≤ 0.6 | M2, floors 2–5 |
| **Hazard face** (tagged side of a wall or Tall piece) | (its host) | (its host) | no | never | — | — | M2, floors 2–5 |
| **Harm square** (walkable; damage on entry) | no | no | no | never | ignored while crossing | floor fill | M2, floors 2–5 **(agreed, Q3)** |
| **Rough square** | costs 2 to enter | no | no | — | ignored | floor fill | M2 |
| **Plank** (narrow ground) | Acrobatics on entry | no | no | — | — | floor fill | M2 |
| Existing condition squares (water, web, fire, oil, gall, bedsheet web) | no | no | no | per conditions 11 | per 11.2 | existing | M2 (conditions 11.5) |
| **Remnant** (Miasma) | no | no | no | countdown, ignition | ignored while crossing | low translucent volume | M3 **(agreed, Q5)** |

**Rules for every class:**
- **"Obstacle" in conditions 10.1** means a wall, a partition, a Tall or
  Low piece, a Furnishing, a Keg or a hazard piece (TR-5).
- **Corner cutting:** every square that blocks movement blocks a
  diagonal step exactly as a wall does, for ordinary **and forced**
  movement, and for a Furnishing's own travel (Shunt, Kick). This
  extends the M0 skeleton plan's `CornerCut` rule, which covered walls
  only, to every movement-blocking square (TR-5); it is not decided
  text. **Sight reads differently (reading, XI-2; approved
  2026-11-23):** `CanSee` is blocked on a diagonal only when **both**
  orthogonal neighbours block sight, so a unit can see a square it
  cannot step to diagonally; this rule is unchanged, and anything
  treated here as a wall for lines also blocks `CanSee`
  (`LosVsLineAgree`) (exploration and information design 4.1.2).
- **Lines** (TR-5): a Tall piece is a wall for lines, Skewer, reach and
  area origin sight (ability 8.1, weapons 7.6). Low pieces, Furnishings
  and hazard pieces never stop a line or reach, and their squares are
  included in areas (for breakers).
- **Free space:** every piece square is non-free for FL 6.3 and
  `RoomMix` (TR-19).
- **Hearing** ignores pieces (sound passes over a crate), so V8's
  hearing proofs are untouched.
- **Static matrices:** FL 6.6 movement matrices, joiner timing,
  `HearingGap`, Circuit isolation, `AntechamberScreened` and the Grille
  vantage stay proven on walls and Portals only. Pieces only ever
  **add** blocking, and no proof relies on a piece (TR-17).
- **No piece is ever a unit:** not for targeting, riders, conditions,
  auras, morale or experience. There are no attacks against objects in
  M2 (Shoot an object arrives in M3, 6.3.7).
- **Rough costs never stack:** Rough, Prone crawl and drag do not
  multiply. A square never costs more than 2 movement (TR-11; the
  conditions 18.4 Prone-crawl precedent).

#### 6.2.2 The Clearway (change TR-17)

Each terrain layer marks a **Clearway**: a connected set of floor
squares, a superset of `protectedSquares`, joining:
- every Portal square;
- the LG20 3×3 landing;
- every start zone;
- the `fleeExit`;
- every Squeeze, Plank and Alcove mouth;
- Circuit waypoints and anchors.

Rules:
- **In fight rooms the Clearway is at least 2 wide**, except where it
  passes through a Portal or a width-1 chokepoint mouth that layout
  itself authored (LG20).
- **Nothing that blocks movement may be placed on, or moved onto, a
  Clearway square.** Condition squares and harm squares may lie on it
  only where the RS entry caps allow, never on the landing.
- This extends FL's `ProtectedIntact`. LG2, LG12, `NoSealedPocket`,
  `PartyEscape`, the Circuit, Squeeze mouths and FL 9.1's rejection of
  shifting rooms then hold whatever moves.

#### 6.2.3 Shape vocabulary

Inside FL footprints (rectangle, 1×2 bay, L; near-convex;
`CutawayPockets` ≤ 3):

| Feature | Rule |
|---|---|
| Pillared hall | A ring of Tall pieces with gaps ≥ 2; line-of-sight breaks and peek cover |
| Low line | 2–5 Low pieces in a row with a gap ≥ 1 at least every 4 squares |
| Split room | A Low or Tall line with 2 gaps of 1–2 |
| Island | A dry group in water (Choir; the RS 8.6 dry-island rule) |
| In-room choke | A gap of width 1–2 between pieces. At most 1 per fight room; never within 3 of the landing or on the party-entry axis within 3; never on the Clearway |
| lane | A clear strip ≥ 3 wide and ≥ 6 long *(illustr.)*. Required in every `RoomMix` open room (Cleave, reach, Skewer; weapons 26) |
| Nook | A floor square with Tall pieces or walls on ≥ 3 sides: a Hide spot and a lurker socket |
| Dais or stage | Floor material only (FL 8.1); never a piece |
| Way round | Every Tall cluster and every Low line leaves a route round it of ≤ 6 squares, so shooters can always be reached |

#### 6.2.4 Clutter and density *(all illustr.)*

Clutter share = blocked interior squares ÷ interior squares.

| Room role | Clutter | Tall | Low + Furnishings | Furnishings | Kegs | Contact hazards | Rough squares | Notes |
|---|---|---|---|---|---|---|---|---|
| Fight, open (`RoomMix` ≥ 11×11) | 3–8% | 0–2 | 2–4 | 1–2 | ≤ 1 | by depth | ≤ 2 | Clear 5×5 core and one lane |
| Fight, standard | 8–15% | 1–3 | 3–6 | 2–3 | ≤ 1 (Carnival ≤ 2) | by depth | ≤ 4 | — |
| Fight, cluttered (Archive, Supper, Heart lean) | 15–22% | 2–5 | 4–8 | 2–3 | ≤ 1 (Carnival ≤ 2) | by depth | ≤ 4 | ≤ 30% of a floor's fight rooms |
| Guard post | standard band | ≥ 1 | ≥ 2 on the defenders' side | 0–1 | 0, never within 3 of the Portal | by depth | ≤ 2 | — |
| Elite room | standard band | ≥ 1 per half | ≥ 2 per half | 1–2 | ≤ 1 | by depth | ≤ 2 | Never on Grille-vantage sight lines (FL 6.5) |
| Forecourt (F2–5) | 3–8% | 0–2 | 2–4 | 0–1 | 0 | 0 | 0 | The approach stays calm |
| Circuit `coverRoom` | 10–18% | ≥ 4 Tall | any | 0–1 | 0 | 0 | 0 | M1: Tall line-of-sight breaks only |
| Alcove (≤ 5×5) | 0–10% | 0–1 | 0–2 | 0 | 0 | 0 | 0 | — |
| Corridors | 0 | 0 | 0 | 0 | 0 | 0 | 0 | Proof-clean, fight-legal (main 4.3) |
| Non-combat rooms | dressing | any | any | 0 | 0 | 0 | 0 | — |
| Arena | 6.11 | | | | | | | |

**Contact hazards per fight room, by floor:** F1 0 (typed Low pieces
only, 6.5.6); F2 1–2; F3 1–2; F4 1–3; F5 1–3 (F2–3 raised from 0–2 by
KP1, below).

**Hazard points (change HZ-2):** segment 4 lays hazard points per
fight room, room roles, floor quotas and a party-size rule (hazards
never scale with party size) on top of this table; the zero-hazard
roles here stay at 0, and KP1–KP9 and the class caps of this document
still bind (hazards design 6.2).

**Placement rules (V9).** Every floor-level quota is **constructive**
**(lead's call, after the check):** KP1 and KP2 hold in every eligible
layer on its own, and KP3, KP8, KP9 and the Plank minimum are met by the
reservation step of 6.2.5, so no seed can miss them and the runtime
asserts nothing it has not proven (FL-15).
- **KP1.** **Every** F2–5 fight-room layer has at least 1 **mandatory**
  contact-hazard socket within 2 of an enemy start zone or a fight lane
  (was "≥ 50% of fight rooms"; the share is now 100%, told plainly).
  Perimeter faces alone are poorly placed for mid-room fights (TN6).
- **KP2.** **Every** F2–5 fight-room layer has at least 1 **mandatory**
  interactive feature at path distance 3–6 from some
  `partyEntrySquares` square (DEP-A1; TR-21; was "≥ 50%"). One piece
  may meet KP1 and KP2 at once. **Pending request (change HZ-31; M2;
  hazards design 6.15, O13):** Stirs, Cords, Voids and vents count for
  KP2 as interactive features (3.4); nothing changes until this owner
  rules (cross-document comparison 2026-11-23, CC-7).
- **KP3.** Cluttered fight rooms are ≤ 30% of a floor's fight rooms
  (Wrecker parity, TN7): a cluttered layer can be picked only for a slot
  the reservation step tagged `cluttered` (6.2.5). A rule, not a
  report.
- **KP4.** Hide spots (Tall-piece nooks and peeks that break sight from
  the room's enemy half): 1–3 per standard room, ≤ 4 cluttered (Shade
  parity). **Pending request (change EP-17; with FL 6.7; M2):** the
  Circuit `coverRoom` holds ≥ 2 Hide spots with line of sight to a
  patrol-lane square within 4, out of the lane's sight from the previous
  waypoint; on F2–5 in a Circuit side room, never the Forecourt; failure
  action: the next Circuit side room, else the patrol is a Picket
  (encounter placement design 6.9). Nothing changes until this owner
  rules.
- **KP5.** No contact hazard, harm square or Keg within 2 of
  `partyEntrySquares` or on the landing; Kegs ≥ 3 from party entry and ≥
  2 from enemy start zones; none, and no trap trigger, adjacent to the
  entry lane or, for lurkers, the open zone in ambush templates (change
  EP-8; M2; encounter placement design 6.6.1, 6.8.3). Segment 6's
  Terrain Pairings also keep Kegs ≥ 2 from the entry lane (change EP-21;
  M2; encounter placement design 6.6.8). **Extended by VP1 (change HZ-3):**
  no condition patch or gall within 2 of `partyEntrySquares`; no Void,
  vent mouth, Stir footprint or Cord within 3 (hazards design 6.2.4).
- **KP6.** Kegs ≥ 2 from each other.
- **KP7.** Plank and harm-square shortcuts end within 2 of an enemy
  cover line or a flank square (6.2.6).
- **KP8.** Floor 1: one teaching room per floor (6.5.6), by a
  reserved `teaching` slot.
- **KP9.** Every F2–5 floor has its theme's signature interaction (7) in
  ≥ 1 fight room, by a reserved `signature` slot. In M2 a segment-4
  **Tableau** fills this slot (change HZ-8; hazards design 6.2.2).

Segment 4's placement rules VP1–VP10 (V10) bind beside KP1–KP9 (change
HZ-3; hazards design 6.2.4).

#### 6.2.5 Layers, sockets and the stream (change TR-17)

```
TerrainLayer {
  id, templateId, wetness?: dry|wetEdge|flooded,       // Choir bands (FL 7.5)
  minFloor, maxFloor,
  clutterRole: open|standard|cluttered|guardPost|elite|forecourt|coverRoom|alcove|nonCombat|arena,
  tags: (teaching|signature|plank|clock|void)[],        // what the layer can serve (6.2.4); clock, void: segment 4 (HZ-2; CC-91)
  lessonTag?,                                           // floor-1 teaching layers (6.5.6)
  clearway: Square[],                                   // 6.2.2
  sockets: Socket[],                                    // sorted by socketId
  phaseSockets?: PhaseSocket[]                          // arenas only (6.11)
}
Socket {
  socketId, square, groupId?,                           // a 2x2 cluster = 4 sockets, one group
  class: tall|low|furnishing|keg|hazardPiece|hazardFace|harmPatch|roughPatch|plank|conditionPatch,
  rulesKey?: {weight: light|heavy, sturdy, round, breakers[], debris, contactType?, typedCollision?},
  side?: N|E|S|W,                                       // hazardFace: the host side, rotated with the orientation
  regionTag?,                                           // conditionPatch: FL 13 wet/dry/web/slick/bile/fire-pit tag
  patchShape?: Square[],                                // patches 2–8 squares, never single
  minFloor, optional: bool, onPermille?: int
}
PhaseSocket { socket: Socket, phase, off: PieceKind|SquareClass|none, on: PieceKind|SquareClass|none }
```

- A socket's **class and rules key** are fixed by the layer; only the
  **skin** comes from the theme's kit, and the skin draw is restricted
  to PieceKinds whose rules fields match the socket's `rulesKey`
  exactly, so a skin never changes a rule **(lead's call, after the
  check:** a bare class let the skin draw change weight, roundness,
  breakers, debris and contact type**)**. V9 `KitCoversRulesKeys` proves
  that every theme kit offers ≥ 1 PieceKind for every rules key its
  layers use, so properties such as ≤ 1 round piece per room, the
  Supper's one light trolley, Keg type, debris counts and the fire hue
  on Hot pieces are fixed by the layer, not by a runtime draw.
- A `conditionPatch` socket may occupy only squares carrying the
  matching FL 13 region tag (wet, dry, web, slick, bile, fire-pit), and
  a `hazardFace` socket names its host side, so the face's one front
  square is derived from the rotated template.
- **Pending request (change EP-5; M3):** an optional `roleHint` on
  `Socket`, used by segment 6 only to break ties between its derived
  start sockets; no other authored start-socket field (encounter
  placement design 6.6.1). Nothing changes until this owner rules.
- **Stream:** `terrain` is the key `dungeon/floor/{N}/attempt/{a}/terrain`,
  derived from its parent's **key** by the one key algebra, with every
  bounded draw by `Bound` (change SV-1; M1; seeds and validation design
  4.1, 4.3); it is registered as a `leaf-formula` row, its approved
  count unchanged (change SV-2; M1; seeds and validation design 4.2).
  It runs at stage GS5, after LS13, EventDraw (GS3) and, **conditional
  on the pending stamp request below**, the arena stamp (GS4)
  (cross-document comparison 2026-11-23, CC-68), and before hazards (GS6), traps (GS7), AP1 and enemies 10.5
  step 2 (change SV-9; M1; seeds and validation design 4.4). It
  draws, in this order **(lead's call, after the check)**:
  1. **Reservation:** fight-room slots, sorted by (`effectiveIndex`,
     `roomId`), receive tags in the fixed order `teaching` (floor 1) or
     `signature` (floors 2–5), then `plank`, then `cluttered` × ⌊0.3 ×
     the floor's fight-room count⌋, then segment 4's `clock` and `void`
     tags (change HZ-2; hazards design 6.2.3, 11.2). Each tag takes one
     R-SEED-3 bounded draw over the still-untagged slots whose template
     has a candidate layer carrying that tag (any cluttered layer, for `cluttered`); if
     no such slot is left, the draw is a dummy. A slot takes at most one
     tag. The count is fixed at 2 + ⌊0.3 ×
     `maxFightSlots`⌋ draws per floor, plus **`hzTagMax`** draws for
     segment 4's tags, reserved and spent as dummies **from M1**, so no
     M1 terrain draw shifts when hazards ship in M2 (change SV-3; M1;
     seeds and validation design 4.3; **pending request:** HZ declares
     `hzTagMax` before the M1 goldens; fallback, a padded maximum from
     HZ's approved tag table, seeds and validation design 14.2); unused ones are dummies.
  2. **Per node slot up to `maxNodes`:** 1 layer pick; `maxSockets`
     on/off draws; `maxSockets` skin draws. Unused slots and sockets
     spend dummy draws (R-SEED-3).
     **Pending request (segments 11–12 ask; BR and ROOM confirm; M1;
     seeds and validation design 4.4, 11, 14.2):** the arena stamp runs
     at GS4, before terrain, and the arena node's terrain slot is spent
     as dummies, so counts stay fixed and the arena interior has one
     source. Nothing changes until this owner rules; if refused, the
     stamp runs after GS6 and overwrites (BR 4.1.3 as it reads today),
     and the arena node's slot draws stay fixed. Until the BR and ROOM
     owners confirm SV-9, every GS4 stamp reading in this document is
     conditional on this request (cross-document comparison 2026-11-23,
     CC-68).
- **Layer candidates:** the layers for (`templateId`, orientation) with
  `minFloor` ≤ floor ≤ `maxFloor`, the room's wetness band, and the
  slot's reserved tag if it has one; an untagged slot never takes a
  `cluttered` layer. **Segment 4's tags (change HZ-2; hazards design
  6.2.2, 6.2.3):** a layer may carry `clock` or `void` only where
  segment 4's room-role and quota rules allow it (no Void or Stir in
  the roles 6.2.2 bars; `void` only in drop themes; at most one clock
  and, in M2, one Void region per room); segment 4 supplies the rule
  and this step reads it. A slot tagged `clock` or `void`, like every
  slot not tagged `cluttered` (KP3), never takes a cluttered layer
  (cross-document comparison 2026-11-23, CC-91). Sorted by layer id and picked by the bounded draw.
  For a Supper bay template, the previous main-path bay's layer id is
  excluded (FL 7.8), and the draw is still spent; V9 proves ≥ 2
  candidates for every bay template.
- **On/off draws:** one per socket. A group uses the draw of its lowest
  `socketId`; its other sockets spend a dummy. A socket withheld by a
  rule (for example `ThemeSix` for a Keg or Remnant condition) is forced
  off and still draws. Phase sockets take no draws: their states are
  authored.
- **Skin draws:** candidates are the theme kit's PieceKinds matching the
  socket's `rulesKey`, sorted by id.
- **`maxSockets`** *(illustr. 16)* is one global constant in the
  `terrain.*` snapshot; V9 `TerrainDrawCount` rejects any layer above
  it. HE-7's retry (LR3) takes the next attempt key, `attempt/{a+1}`
  (a = 1 from attempt 0), which re-derives every attempt-scoped child,
  `terrain` included, and nothing is carried over from the failed
  attempt (change SV-4; M1; seeds and validation design 4.3, 5.5). `terrain.*` tuning is hashed
  into the reproducibility key (R-SEED-5).
- **Rules pieces never come from the display `dressing` stream.**
- **Proofs.** Quantities that only grow with placed sockets (blocking,
  clutter, cover counts) are proven on the all-optional-on and the
  all-optional-off configurations, and both ends of a two-sided band
  are checked on both. A quantity that is not monotone (`HideSpotBand`:
  a Tall piece can make a nook or fill one) is proven by enumerating
  every on/off combination of the optional sockets on or 8-adjacent to a
  nook candidate square; at most 8 such sockets per layer *(illustr.)*,
  otherwise they are mandatory. The proof space (template × orientation
  × layer × floor band × theme, plus the reservation capacity per
  Blueprint) is finite. V9 `ReservationCapacity` proves, for every
  Blueprint and rule choice, that the floor has enough fight slots and
  eligible layers for every reserved tag.
- **Save:** dynamic Furnishing state `{id, square, state:
  intact|debris|gone, burstSpent}` is saved beside Portal state (FL
  11.1, TR-17). `terrainDigest` sits beside `layoutDigest`.

#### 6.2.6 Rough and Plank

**Rough (M2; TR-11):**
- Entering costs 2 movement. Forced movement, Jumping and Flying ignore
  the cost.
- No condition, not a hazard. One cross-theme hatched glyph. Not a
  `squareKind` for `pairKey` or the home-theme rule.
- Sources: authored patches (≤ 10% of a room, ≤ 4 squares) and debris
  from broken heavy Furnishings.
- It slows the melee approach, so it is measured in the range-band arm.

**Plank (M2; TR-12):**
- A 1-wide line of 2–5 floor squares, drawn coplanar (LG18), flanked by
  hazard squares, harm squares, Rough or Low pieces: a pew-back walk, a
  plank over gall, a dry causeway, a ruler laid flush.
- It shortens a route by ≥ 4 *(illustr.)*, always beside a free route,
  and ends within 2 of an enemy cover line or flank square (KP7), so it
  is a tactical choice, not a pathing tax.
- **In combat:** entering the first Plank square needs Acrobatics
  against the segment-5 ladder (DC 12, *illustr.*; the flat Plank row,
  12 on every floor, change TL-2; traps and locks design 6.2).
  - Pass: continue.
  - Fail: movement ends on the square before; no retry that round; no
    fall and no damage (10.4-safe).
- **Out of combat:** one check by the leading unit, with the Squeeze
  row's failure cost (segment 5).
- **Rolls (change TL-3; decided):** Plank checks roll on the keyed
  `checks` stream by the Plank's `traversalIndex`, once per traversal,
  in and out of combat (traps and locks design 6.12, 6.22).
- Stiff gives disadvantage (armour 7.3). Forced movement onto a Plank
  needs no check. Monsters may use Planks; the AI costs the path by its
  fail chance.
- **≥ 1 per floor on floors 1–5 from M2**, by a reserved `plank` slot
  (6.2.5). With layout's 2 Squeezes this meets armour 7.3's ≥ 3
  (`AcrobaticsChoices`). If the report misses,
  armour 7.3's named fallback applies.

### 6.3 Movable props: Furnishings (agreed, Q2; change TR-6, TR-7, TR-9)

#### 6.3.1 Model

A Furnishing is an **object with a position**. It has:
- one square, a stable `furnishingId` in canonical socket order, and no
  turn, initiative, side, awareness, conditions or hit points;
- a `weight` of light (size Small, heft 8) or heavy (size Medium, heft
  12) *(illustr.)*; size and heft follow the weight;
- `sturdy: bool` beside the weight: a sturdy piece is never Shunted or
  Kicked (Kegs in M2, which are heavy; Carnival beast crates). It can
  still be Smashed at its weight's heft **(lead's call, after the
  check)**;
- `round?` (Kick), `breakers[]` (breaker damage types),
  `fragileToCollision: bool` (it breaks when a unit is forced into it),
  `debris` (none or Rough), `crossable[]`, `noise` per verb, and
  `tippable?` (M3).

**All Furnishings are Low in M2**, so moving one never changes sight or
fog.

**Furnishings move only by verbs, with one M3 exception (change HZ-18;
agreed, hazards Q3; M3):** the environment may move a Furnishing along
a pre-proven rail as an announced Stir act, held while occupied (the
Returns Trolley; a bound Cord's Knock), and a Stir's Overturn part may
tip a tippable Tall Furnishing along a pre-proven strip (6.3.7; hazards
design 6.3, 6.3.4, 7.6).

**Cords (change HZ-15; with Stirs, M2 (gated), else M3):** a Cord is a
Low piece face or wall socket with exactly one verb, used by
**Operate**, a new ability 7.2 object action (one action, from an
adjacent square, no check, noise 4 *(illustr.)*). In M2 bound Cords
only re-time their Stir (Now, Sustain); Reverse, Flip, Knock and the
standalone Cords are M3. **Pending request (the ability owner rules):**
a unit that starts its turn adjacent to a Cord may Operate it as its
bonus action (hazards design 6.3.4).

It is not a ground surface and leaves nothing on the ground, so moving
one engages none of the surface non-goals. Breaking one is a map change
in substance, listed in TR-8.

#### 6.3.2 Shunt (new ability 7.2 row)

| Mode | Cost | Rule |
|---|---|---|
| **Move Shunt** | 2 movement *(illustr.)* | Light, non-sturdy pieces only, adjacent, no check. Moves the piece 1 square straight away from the user. **Never collides:** if that square holds a unit, Move Shunt cannot be chosen. Repositions cover or closes a flank cheaply |
| **Attack Shunt** | Replaces one attack | Any non-sturdy piece, adjacent. Light: automatic. Heavy: Athletics against heft. Moves 1 square straight away. **If the destination holds a unit**, the piece stops, the unit takes the collision band once (Shove is non-verb, weapons 7.2 rule 4) and is **not moved** (no carom; Vanguard Later kept). The unit then makes a Reflex save against 8 + the Shunter's Athletics bonus *(illustr.)* or falls Prone (Prone is every theme's tax, so the 6 is unchanged). **A light piece breaks** (gone) when it hits a unit **(lead's call, after the check:** otherwise the same crate is a no-roll Prone anvil every round**)**. A heavy piece survives |

**Resolution, for Shunt, Kick and Smash (lead's call, after the
check):**

| Point | Rule |
|---|---|
| Direction | Straight away from the user: the one square in line with the user and the piece (an adjacent user fixes it uniquely). There is no tie fallback and no choice. If that square is blocked (a wall, a piece, a Portal, a hazard or harm square) or, for a Shunt, is an end square 6.3.6 forbids, the verb cannot be chosen and the preview greys it out with the reason. A Kick may cross squares it may not end on; its end square follows 6.3.3 |
| Travel | A piece's travel obeys the corner rule of 6.2.1 on diagonals, exactly as a unit's move does |
| Hitting a unit | Collision band once, then the Reflex save against Prone. **Size:** a piece cannot Prone a unit more than one size larger than the piece (light Small, heavy Medium; the A-10 rule applied to the piece); the collision still applies. A light piece breaks after an Attack Shunt hit; a Kicked piece survives and stops on the square before the unit |
| Hidden and noise | Attack Shunt, Kick and Smash end Hidden like an attack (conditions 18.7). Move Shunt does not; it makes noise 4. Noise: Attack Shunt 4, Kick 4, Smash 6 *(illustr.)* |
| Breakage on collision | A piece breaks from a unit forced into it only if its `fragileToCollision` is true |
| Sturdy pieces | Never Shunted or Kicked; Smash at the weight's heft (a Keg at heft 12) |

**Shared limits:**
- **Once per piece per round**, across both modes.
- **Combat only** in M2 (protects `ExploreClicks`).
- Noise 4 *(illustr.)*.
- **An Attack Shunt into a unit is legal even when the square the unit
  stands on would be an illegal end square under 6.3.6**, because the
  piece does not move **(new lead's call)**. A Shunt into an empty
  illegal square cannot be chosen.

#### 6.3.3 Kick (M2; round pieces)

- **Cost:** replaces one attack; adjacent to a `round`, non-sturdy
  Furnishing; Athletics against heft (light automatic).
- **Travel:** the piece travels straight away from the user up to 4
  squares *(illustr.)*, stopping before walls, pieces, Portals, hazard
  squares and harm squares, or at the first unit in its line, and
  obeying the corner rule.
- **Hitting a unit:** the first unit in its line takes the collision
  band, makes a Reflex save (as Attack Shunt, with the size rule) or
  falls Prone, and the piece stops on the square before it.
- **Where it ends (new lead's call):** the travel may cross Clearway
  squares but the piece never ends on one, or on any other square 6.3.6
  forbids. If the square where its travel stops is illegal, it ends
  instead on the last legal square of its travel; a unit it hit still
  takes the collision. If no square of its travel is legal, Kick cannot
  be chosen, and the preview greys it out with "Would block the way".
- **Preview:** the whole line, the end square and every hit are
  previewed.
- **Limits:** at most 1 round piece per room *(illustr.)*. Kegs are
  never round in M2.

**Why M2, not M3:** Low pieces never block sight, so Kick needs no fog
recompute. It is the most satisfying prop verb (fun critique) at small
core cost.

#### 6.3.4 Smash (M2)

- **Cost:** replaces one attack; adjacent. Light: breaks automatically.
  Heavy, sturdy included: Athletics against heft. Noise 6 *(illustr.)*;
  it ends Hidden like an attack.
- **Other ways to break a piece:**
  - an area whose damage type is in its `breakers[]` (fire burns wood
    and cloth; bludgeoning and force break pottery, glass and casks);
  - a unit forced into it with collision damage, if it is
    `fragileToCollision`;
  - a light piece's own Attack Shunt hit on a unit (6.3.2).
- **Binary:** no hit points and no AC.
- **Results:**
  - a broken light piece is **gone**;
  - a broken heavy piece becomes **debris**: walkable, Rough, no cover,
    no blocking. Debris is the Furnishing's own saved state, a map
    change in substance (TR-8, not a clarification).
- **Never breaks:** walls, partitions, Tall and Low pieces, hazard
  pieces and Portals (RS 7.6 kept). Door shattering stays segment 5's
  (force and Splinter, read against RS 7.6: change TL-21; traps and
  locks design 6.11).
- **Furnishings never hold loot.** Containers stay segment 7's and HE-7
  sockets, so there is no smash-everything chore. **Reaffirmed by
  segment 7c (change LP-12; settled, read; M2):** loot set into a room's
  walls uses **inset containers**, never a Furnishing or a hidden stash
  (loot sources design 4.3).

#### 6.3.5 Kegs (M2: Carnival, Gut; change TR-9)

- **Kegs are heavy and sturdy** (heft 12 for Smash; never Shunted or
  Kicked).
- **Trigger:** a Keg bursts **once**, when:
  - it is broken (Smash, a unit forced into it, an area of a breaker
    type);
  - a fire area includes its square.

  A Hot contact hazard, a Burning unit or a fire square next to it does
  not ignite it. There is no "shoot the keg" in M2 (M3, 6.3.7, which
  adds "a fire part dealt to it by Shoot an object"). **(lead's call,
  after the check:** the proposal's third trigger, "a fire part resolves
  on its square", could never fire, because no unit stands on a Keg**)**
- **Burst:** radius 1 around the Keg, source `environment`, then the Keg
  is gone. Its footprint uses area line of effect from the Keg's square
  (walls and Tall pieces block it, with the corner rule). **It never
  creates a square** (ability 8.4 and conditions 11.1 kept).
- **The Smasher is outside its own burst (lead's call, after the
  check).** Smash needs adjacency and the burst has radius 1, so
  otherwise every Smash would burst the Keg on its user. The Smasher
  takes no damage and no condition from a Keg it Smashed; everyone else
  in the footprint does. `BurstSelfDamageShare` reports it.

| Keg | Broken without fire | Ignited by fire |
|---|---|---|
| Carnival lamp-oil keg | No damage; Reflex or Oiled | Fire, Keg band *(illustr. F2–3 4, F4 6, F5 8)*, Reflex for half (per part, death and healing design 20.3 step 2) **(agreed 2026-11-28, roadmap D-22; M1-11 rulings 1.1)**; Oiled units in it take Oiled's own payout. That payout is Oiled's own rule, not a Keg effect; whether it applies to a unit that succeeds on the Keg's save is open for this design's owner (M1-11 rulings 7 item 2). It also burns away oil squares under 11.4 unchanged; never a fire square |
| Gut swollen sac | Acid, Keg band, Reflex for half (per part, death and healing design 20.3 step 2) **(agreed 2026-11-28, roadmap D-22; M1-11 rulings 1.1)**; Poisoned on a failure | Same |

- **Caps:**
  - **a burst never triggers another burst in M2**;
  - Kegs are sturdy in M2, so they cannot be walked into a pack;
  - ≤ 2 per Carnival room, ≤ 1 per Gut room (the sac counts toward the
    Gut's "≤ 2 acid sources per encounter"; gall squares never count,
    change HZ-5, hazards design 6.5);
  - spacing and distances per KP5–KP6;
  - none on floor 1.
- **Lethality:** `BurstLethalityBound` (V9): the Keg band is ≤ 25% of
  the lowest legal hero maximum HP at the floor's lowest expected level.
- **Readability:** **the footprint is always drawn** as a permanent
  ring at **hazard** tier, not only on hover. It rises to telegraph
  tier only while a preview or a visible intent would burst it.
  Overlapping footprints merge into one outline, and each Keg keeps its
  type pattern on its own squares (8.1).
- **Enemy guards:** every round in which a monster-triggerable Keg is in
  range of party-reachable squares counts in the enemies 6.4 guards, not
  round 1 only.
- **Out of combat:** party damage is floored at 1 HP (DH 10.4).

#### 6.3.6 Legality: `FurnishingNeverSeals` (a core rule, not an assertion)

A Furnishing never ends on:
- a Clearway square;
- a Portal, corridor or Plank square;
- a hazard square or harm square;
- a `protectedSightLines` square (a Low piece would not block the line,
  but this keeps `ProtectedIntact` true at runtime);
- a Downed body;
- a square outside its room;
- a square whose occupation would leave any walkable square of the room
  **unreachable** from the Clearway (8-connected, with the corner rule);
- a square whose occupation would lengthen the path from any walkable
  square of the room to the Clearway by more than 6 *(illustr.)*
  **compared with the layer's initial state** (every socket as the
  `terrain` stream generated it, re-derivable from the seed, so nothing
  extra is saved), so repeated Shunts, in one fight or across fights in
  the same room, can never add up past the bound.

**(lead's call, after the check)** The proposal's "2-wide flood fill on
a thickened grid" is dropped as vacuous: the Clearway is itself proven ≥
2 wide and nothing may stand on it, so no off-Clearway piece can cut it.
What a moved piece can do is seal a pocket off the Clearway or lengthen
the way to it, and the two rules above forbid exactly that: one
breadth-first search from the Clearway per candidate move, against the
initial-state distance table, computed once per room (about 0.15 ms on
the toy). The
static `WayRound` (≤ 6) and the CV8 flank reachability are initial-state
guarantees proven by V9; at runtime these two rules hold their place.

A Furnishing may end on a trap's trigger square, known or not, and
never hides a known trap's marker. **Narrowed by segment 5 (change
TL-14; M2): pressure slabs.** A Furnishing whose Shunt, Kick or
Overturn **ends** on a plate or trapdoor springs it, with the mover as
causer; squares it only passes over spring nothing; wires and glyphs
ignore pieces; Kegs stay sturdy. So one act springs at most one trap
(traps and locks design 6.7; 13).

An illegal destination means the move does not happen. The preview greys
the option out with "Would block the way" or "Hazard".

#### 6.3.7 Later verbs

- **M3 Overturn:**
  - action; Athletics against heft 15 *(illustr.)*; orthogonally
    adjacent to a `tippable` **Tall** Furnishing (bookshelf,
    card-catalogue cabinet, wardrobe, candelabrum, block tower, crockery
    dresser);
  - it falls into the 1×2 strip straight away from the user; the strip
    may never include a Clearway square (V9 per socket);
  - units there make a Reflex save: failure means 2× collision band plus
    Prone (tag `collision`); success means half damage;
  - the piece becomes debris; line of sight opens, and fog is recomputed
    once;
  - a Hidden unit behind it is re-tested at its own turn end under
    conditions 18.7, not immediately;
  - ≤ 1 tippable piece per room; V9 proves `coverRoom` cover,
    `PartyEscape` and Hide minimums **without** tippable pieces;
  - **environment Overturn (change HZ-18; agreed, hazards Q3; M3):** a
    Stir's Overturn part rides this verb along a pre-proven strip
    (hazards design 6.3).
- **M3 Haul:** drag an adjacent light Furnishing into the square you
  left, at 2 movement per square. It provokes as normal movement.
- **M3 Shoot an object (TR-28):**
  - a ranged or thrown weapon attack may target a Furnishing or Keg
    against AC 12 *(illustr.)*; any hit breaks a light piece or bursts a
    Keg (a fire part dealt this way ignites a Keg);
  - no riders, Prime, arcs or forks on object hits; cover does not apply
    to objects; it uses the attack roll stream;
  - it ends Hidden (it is an attack); noise 6;
  - monsters triggering Kegs this way fall under `NoCheapKill` (6.9).
- **M3:** Kegs in the Choir, Archive, Supper and Heart; chains of depth 1
  in `furnishingId` order.
- **Rejected:** carrying, stacking, pulling into a pack, and party
  throwing (weapons 9.3; stat model 6; BG3). Hurl is Later at most, as
  the turn's one offensive flask. Pre-fight barricading. Barricading
  Portals (enemies 17.5 "exits are never sealed").

### 6.4 Collisions and forced movement (change TR-5, TR-10, TR-13)

- **Unchanged from conditions 10.1** (settled):
  - stops at walls, obstacles and units;
  - collision band once, only with a non-verb part (Battering Force
    excepted);
  - the 3-square sum per hit; never provokes;
  - Tangled, Restrained and Anchored are not moved.
- **A unit forced into a Furnishing or Low piece:** it stops and
  collides as with any obstacle. A Furnishing with `fragileToCollision`
  breaks (a Keg bursts); the piece is never moved (no carom).
- **A Furnishing driven into a unit:** see Attack Shunt and Kick.
- **Contact hazards:** 6.5.
- **Stalwart and similar shortenings** (armour, Splint): the shortened
  move is computed first; the edge save happens only if the shortened
  move would still reach the hazard.
- **Downed bodies** are inert (DH 11.4): not obstacles for
  forced-movement stops, never moved except by an ally's grapple-drag,
  and no piece may end on one.
- **One environment damage instance per unit per action** (TR-13;
  **agreed, Q3**). A unit takes no more environment damage from a single
  action than its largest single instance would deal: collision,
  contact, harm-square entry, Keg burst, ignition (M3) and Charged Water.
  - **The top-up rule (lead's call, after the check).** Instances arise
    in sequence (a collision at step 10, then a broken piece, a Keg
    burst, Charged Water), so damage is never deferred to the end of the
    action. The first environment instance on a unit in an action
    applies in full. Each later one deals max(0, its final amount − the
    environment damage that unit has already taken this action), where
    the final amount is after the save, weakness and resistance; if the
    later one deals more than 0, its type is the later instance's type.
    Downs, concentration checks, "on a kill" credit and every hook fire
    where the damage lands. The preview shows each instance and the
    top-up it deals.
  - **"Action" means one use** in the ability model 9.3 sense: one
    attack (each attack of Extra Attack is its own use), one spell cast,
    one reaction, one Shunt, Kick or Smash **(new lead's call)**. Every
    forced move inside a use shares that use's cap.
  - **Voluntary movement is outside the cap (lead's call, after the
    check).** Each harm square a unit enters by its own movement deals
    the harm band, square by square; the move preview sums them and
    shows the lethal tag. A forced move takes harm damage under the cap,
    so at most once per use.
  - **Also outside the cap:** `onTurnEnd` effects, M3 Remnant
    applications and trap damage (traps are segment 5's and keep their
    own lethality rules, conditions 11.6; kept by TL-18). A piece's own
    collision instance is unaffected by a spring; when one act both
    collides and springs a pressure slab, both go through EnvRound in a
    fixed order, collision first, then the spring (change TL-14; M2;
    traps and locks design 6.7).
  - **Setup payouts are not environment instances.** Oiled's fire
    payout, triggered by a Keg or by K1, resolves under its own rule and
    is not capped here (6.5.2).
  - **The cap limits damage only.** Non-damage effects of every instance
    (an Attack Shunt's Prone save, an unlit lamp-oil keg's Oiled, a
    swollen sac's Poisoned) still resolve, each under its own rules and
    the per-fight condition cap **(new lead's call)**.
  - The triggering attack's or spell's own damage is never environment
    damage and is not capped by this rule.
  - **The EnvRound cap (change HZ-7):** for party units, environment
    damage within one round is also capped at `CapRound`, 30%
    *(illustr.)* of the lowest legal hero's maximum HP for the floor's
    level band, by this top-up mechanic at round scope, applied after
    the per-use top-up; Stir acts and falls are included, monsters are
    uncapped (hazards design 6.11 item 2). **Extended by segment 5
    (change TL-18; M2):** involuntary trap damage to a party unit is
    included; self-caused trap damage (Bear it, a party-caused spring on
    oneself, a Botch the unit itself rolled, a chosen in-combat entry
    into a shown Hunch zone) is counted in full outside the cap and does
    not fill it (traps and locks design 6.10).
  - **Dream-step acts (change HZ-15; M2 (gated), else M3):** each Stir act, and every combo
    it triggers, is one use for this cap. An act set by an Operated Cord
    credits the operator for experience and morale, but on-kill hooks
    never fire from a Dream-step act: they fire only inside an action
    (hazards design 6.3.4, 6.11 item 1).

### 6.5 Knock-into hazards and fairness (agreed, Q3; change TR-10, TR-11)

#### 6.5.1 The universal set

One glyph per class; themes reskin the fill.

| Kind | Form | Typical skins |
|---|---|---|
| **Bladed hazard piece / face** | Non-walkable obstacle; piercing or slashing (bludgeoning for the Choir's fallen bell) | Spike rack, knife wheel, grinding gears, glass rack, molar face, nib rack, pincushion (needles flat) |
| **Hot hazard piece / face** | Non-walkable obstacle; fire (lightning for the storm-glass pipe, acid for the Gut's acid-scald fold) | Brazier, plate-warmer, boiling tureen or cauldron, stove, lamp sconce, fireplace grate |
| **Harm square** | Walkable; damage on entry; a hazard | Thorny vines (the client's ask: the Heart's thorn-rose vines), broken glass and bottles, shattered crystal, bone shards, spilled nibs |
| Existing condition squares | Walkable; conditions 11.2 | Water, web, fire, oil, gall, bedsheets |
| Drop hazards | Segment 4: **Void** squares, non-walkable, crossable by Jumping and Flying (change HZ-9; M2 (gated), else M3; hazards design 6.4) | Chasm, shaft, open trapdoor; deep water as the Drowned Crypt Void |

**Contact hazard** means a hazard piece or a hazard face.

**Face adjacency:** a face is "adjacent" only to the single square
directly in front of it. That matters for VG-22 ally swap (pending),
follower pathing and Teetering's end rule.

#### 6.5.2 Resolution (all M2)

**The save (lead's call, after the check).** Conditions 10.2 sets no
difficulty, and many push sources have none, so:
- **difficulty:** the pusher's ability difficulty when the push comes
  from an ability that has one; otherwise 8 + the pusher's Athletics
  bonus (as Attack Shunt). A drag uses the holder's;
- **it is a hazard-edge save** for every rule keyed on that name:
  Ledgewalker's advantage (trinkets TK-2), Drag Line's disadvantage, the
  armour 7.3 Stiff fallback, Teetering's later-push disadvantage, Brink's
  ordering and Flare II's exclusion;
- **timing:** rolled at weapons 7.5 step 10, square by square as the
  move resolves, before step 11's reserved saves (Ram's collision
  Prone); it never uses the step-9 rider slot. Non-weapon pushes roll it
  at the same point of their own resolution (ability model 14.3);
- the difficulty rule is offered to the conditions owner for every 10.2
  save (TR-10).

**Into a contact hazard.** When the next square of a forced move holds a
contact hazard, the unit stops on its current square and makes the
save. TR-10 adds this as a new obstacle-hazard branch of 10.2; it is
**not** "10.2 unchanged".
- **Success:** an ordinary obstacle stop under 10.1. If a non-verb part
  contributed, the unit takes the collision band; otherwise nothing.
  **No Teetering.**
- **Failure:** typed contact damage **instead of** collision damage,
  never both:
  - a push with a non-verb part deals the **contact band**: collision
    band + 1 step, 3 / 4 / 4 by the level band of the floor's lowest
    expected level *(illustr.)*, and never less than the collision band
    the success would have dealt.
    Non-verb sources include Shove, kit and Technique pushes, Ram,
    Domino, Give Ground, Upheaval, Squall, Jostle, Heave and Barge;
    Battering Force counts its move as non-verb for this choice, as it
    does for collisions;
  - a purely verb-sourced push (the weapon verbs Push and Hook and their
    signatures, the shield bash's Push, Bowl Over, Empower effects, and
    the Arcanist's Gust and Reel) deals the **collision band**, typed: 2
    / 3 / 3. This is the TT16 payoff, a second named exception class
    beside Battering Force (weapons 7.2 rule 4; TR-10).
  - **New lead's call:** the proposal listed Upheaval and Squall among
    the verb-only pushes. The Arcanist design (8.2, 8.3) makes both
    non-verb ("collisions, hazard edges, Teetering"), so they take the
    contact band, under the area cap below.
- **Area cap:** an area-shaped forced movement (Give Ground, Upheaval,
  Squall, an M3 blast) gives the save to at most **2 units per action**
  **(lead's call, after the check, on the order):** in the fixed square
  order of the moved units' **starting** squares, the first 2 units
  whose move reaches a contact hazard or a harm square roll the save.
  Every later unit rolls nothing and stops as against an ordinary
  obstacle (the collision band if a non-verb part contributed). **The
  cap is one shared counter per use** across contact hazards, harm
  squares and (segment 4) Void squares; a unit beyond it stops before a
  Void as a non-early stop (change HZ-9; hazards design 6.4;
  cross-document comparison 2026-11-23, CC-90).
- The hazard piece takes nothing.
- **The preview headline shows the difference** a failed save makes
  over a plain obstacle ("Spikes: 55% for +1"), not the raw band.

**Into a harm square.** The harm-square save (a hazard-edge save):
- success: stop on the last safe square, **with no Teetering**
  (damage-only hazards never give it; TR-10). **The stop counts as an
  ordinary obstacle stop (lead's call, after the check):** a push with a
  non-verb part deals the collision band, exactly as a wall would, so a
  harm square is never worse to push toward than a wall;
- failure: enter, and take the harm band on entry: 2 / 3 / 3
  *(illustr.)*. The push then **continues**; each further harm square on
  its path gives a new save, and the damage stays under the one-instance
  cap (6.4), so it is dealt at most once per use.

Voluntary entry has no save, deals the harm band per square entered
(outside the cap, 6.4) and shows a lethal tag where relevant. Jumping
and Flying ignore harm squares (main 4.3); the landing rule (10.3)
applies.

**Typed, rider-free damage.** Environment damage is not a hit. It
carries no riders and no weak-target automatic riders, and never arcs or
forks. Damage weaknesses (Soaked against lightning), the **damage-end
row** (fire ends Chilled, Frozen, Soaked, Oiled) and **setup payouts**
(Oiled's fire payout) do apply (TR-13).
- **Told plainly:** a Supper brazier thaws Frozen units of either side.
  That cuts both ways, as the Supper's Law ("freeze and shatter, both
  ways") intends.

**Why no Teetering from damage-only hazards:**
- it removes this segment's dependency on the pending RT-14 ruling;
- it stops contact hazards from feeding Brink;
- the push still always does something on a success (a collision for
  non-verb pushes).

**Brink (Vanguard 8.3) is read literally:** "adjacent to a hazard
square". Contact hazards are not squares, so Brink does not read them.
Harm squares are squares, so it does.

**M1:** none of these exist; pushes still stop before hazards
(conditions 10.1).

#### 6.5.3 Who exploits them

| Side | Means |
|---|---|
| Party | Shove; weapon Push and Hook; shield bash; Bowl Over; Wrecker Ram, Domino, Battering Force; Arcanist Gust, Reel, Upheaval, Squall; Herald Give Ground; companion Jostle, Heave, Barge; Attack Shunt and Kick (collision; Prone) |
| Enemies | Kit Shove users; the Holder ("shoves toward hazards", M2, now including contact hazards); Carnival Strongmen toward fire; one visible terrain-using enemy per F2–5 theme (6.9) |
| Bosses | Authored, forecast signatures only, inside the 3-square cap and the edge save. Bosses are pushed into hazards like anyone else and are `noDrop` (segment 4); no `fixedInPlace` exception: a `chain(n)` leash limits a unit's own movement only, never forced movement (change BR-14; boss rooms design 4.6) |
| Environment | M3 powder-keg blast push 1 (non-verb), if adopted from Model B's later catalogue |

#### 6.5.4 Fairness

1. **Always a save.** No auto-entry (Over the Edge stays Later).
2. **Bounded damage.** Flat bands, previewed exactly.
   `ContactLethalityBound` (V9): contact band ≤ 20% of the lowest legal
   hero maximum HP at the floor's lowest expected level *(illustr.)*.
3. **Placement:** KP5; none on floor 1 (typed Low pieces only); none on
   Hinge or Postern squares (FL 13).
4. **`NoCheapKill`** (6.9): a scorer term. Environment damage that
   could finish a hero scores 0 for that hero, and the candidate gets no
   kill or down bonus for them; the action stays legal.
5. **Information:** enemy shove intents are visible through the existing
   intent read; a hover highlight shows "aligned to hazard" for the
   player's Push and Ram users; lethal tags (DH 17.6) and ally
   confirmations (DH 17.7) extend to contact hazards, harm squares,
   Kegs, Kick lines and Overturn strips (TR-23).
6. **Downed units are inert** (DH 11.4).
7. **Gates:** `HazardPushDeaths` ≤ 10% of hero deaths *(illustr.)*;
   `ContactHazardDeaths` (renamed from `ContactHazardDowns`, change
   SV-24) by theme and floor. The per-round push caps
   (`hazardHitThisRound`, `hazardPushPerRound`) are kept as **switchable
   simulator arms**, not rules. The toy AI aims badly, so human-like
   aiming may exceed its rates.

#### 6.5.5 RT-14

Contact hazards and harm squares no longer need RT-14: neither gives
Teetering on a save. A harm square reaches Teetering only **through
Brink**, and RS 8.0 already counts Teetering in every theme with forced
movement and traps, with a named fallback drop for every full-6 theme.
So a refusal costs the same fallback drop with or without harm squares.
**Condition squares** still give Teetering through 10.2, as today.
- TR-14 asks the conditions owner to rule "does not count" before M2
  rosters freeze (RS 14).
- **Pending request (change HZ-6; the conditions owner rules, before M2
  rosters freeze):** segment 4 asks for the same ruling with two
  guards: no monster, boss, trap, Stir, Cord or square record may apply
  Teetering directly (party abilities such as Brink excepted), and
  bosses gain no Teetering from a Void-edge success or from Clutch
  (hazards design 6.8). Nothing changes until that owner rules.
- **(lead's call, after the check)** The proposal's `ThemeSix`
  withholding of harm squares under a refusal is dropped: it bought
  nothing for the budget and only stripped content (the Supper's
  shattered crystal, DEP-A1 features). If RT-14 is refused, every M3 row
  that uses Poisoned in the Supper (whose fallback drop is Poisoned)
  becomes conditional, as before.

#### 6.5.6 Floor 1 (RT-20; enemies 6.4 guard rows, DH-26; DH 17.1)

- **No hazards of any kind** in the base game. M3 Landmark ticks are
  not hazards (no damage, condition or unit movement), so this stands
  (change HZ-19; hazards design 6.1). **Exception at Peril tier 2+
  only (change HZ-25; agreed, hazards Q4; M3):** each floor-1 theme
  gains one damaging hazard package (the Gaol Treadwheel; the Kennel's
  staked pits with the Painted Hunt, the Stampede replacing the Painted
  Hunt at tier 3; the Wake's Open Grave), with DH 17.1 and the floor-1
  guard rows still holding (hazards design 6.19).
- **Typed Low pieces:** the Gaol warden's stove and the Wake candle rack
  are ordinary Low pieces whose collision damage is fire-typed. They are
  not hazards, have no edge save and no saturated hue, but carry a
  visible ember tell. This satisfies RS pillar 1 ("if it looks like it
  matters, it matters") without breaking RT-20 **(told plainly** to the
  trinkets owner: floor 1 still has no hazard edges; TR-29**)**. At
  Peril tier 2+ only, the stove is promoted to a Hot piece for the
  Treadwheel and floor 1 gains hazard edges (HZ-25; 7.2).
- **Teaching room (KP8):** one `lessonTag` fight room per floor-1 theme
  authors a light piece in line with a lone enemy, so that an Attack
  Shunt (or a Kick, where the piece is round) drives the piece into the
  enemy for the collision band and a Prone save. It is legal, previewed
  and obviously the intended play **(lead's call, after the check:** the
  proposal's "drives a lone enemy into a wall" described a Shove, but
  Attack Shunt never moves the unit**)**.
- **Traps on floor 1 (read by segment 5; no change here):** RT-20 reads
  "traps are devices, not terrain hazards" on F1; pushes stop before F1
  trigger squares, so TR-29 stays true, and F1 trapdoors reclose
  (changes TL-19, TL-31; traps and locks design 6.19, 6.3).

#### 6.5.7 Chasms and drops (segment 4)

Legal under one flat rules level as non-walkable **Void** squares,
crossable by Jumping and Flying. Ruled by segment 4 (change HZ-9,
HZ-10; agreed, hazards Q2; M2 (gated), else M3; hazards design 6.4),
replacing this section's earlier suggestion:
- a non-elite monster that falls is removed and defeated (`voidFall`,
  paid as a kill); elites and group leaders **Clutch** the edge once
  per fight; bosses are `noDrop`;
- a party unit **Freefalls**: it takes the fall band per hazards
  design 6.4 (the contact band, or the harm band where its 6.11 item 5
  requires it, typed by the Void's skin; cross-document comparison
  2026-11-23, CC-93), which can never take it below 1 HP (the FallFloor), and is
  placed Prone on the room's **Snag**, a proven landing on the party
  side, chosen by the Snag rule (primary, secondary, overflow), not on
  the nearest safe square; it is Groggy until the end of its first turn
  that starts after the placement;
- Teetering's disadvantage does not apply to the Void-fall save.

Enemies 17.5's "a chasm edge for shoves" is met by a Void from M2
(gated), and by a contact hazard otherwise (hazards design 6.4).

### 6.6 Lingering effects and their (absent) spread (change TR-25, TR-26, TR-27)

**M2: nothing timed and nothing spreads**, except one gated signature
Stir (below). The decided main 2 non-goal
and agreed conditions 11.1, 11.4 and ability 8.4 stand for M2.
**Narrowed (change HZ-12; agreed, hazards Q1; M2 (gated), else M3):**
at most one signature Stir per F2–5 floor (the Choir's Organ Swells,
the Carnival's Pit Breathes) acts in M2, in the round-end environment
step below (the Dream step), brought from M3 to M2 for Stirs only. It
changes no square, is announced a full round ahead on exact squares and
never acts in round 1. It ships only if the reading-time playtest, the
TR-27 written report pass (brought forward for the Dream step alone),
and the clock and fight-length gates pass; otherwise it falls back to
M3 (hazards design 6.3).

Otherwise the legal M2 lingering experience is **one package**:
- static harm squares (thorny vines on a cell: the Heart's thorn-rose
  vines, 7.1; never growing) **(agreed, Q3)**;
- one-shot Kegs (Carnival oil, Gut gas sac with Poisoned) **(agreed,
  Q2)**;
- **one gas-aura monster each in the Gut, Archive and Heart** (a Gut
  spore-bloat and an Archive mildew moth; the Heart example is left to the enemies owner). The
  Supper, whose 6 also holds Poisoned, is left out because Poisoned is
  its RT-14 fallback drop. Unit
  auras are legal under ability 8.4; this is a request to the enemies
  owner (TR-15);
- boss phase sockets (6.11);
- web and bedsheet burn-away, and Charged Water (6.8) **(agreed, Q4)**.

**M3 Remnants (agreed, Q5).** Only after the M2 rider and layer reports,
the stat model 2 gate "until riders and layers are proven". **New lead's
call:** that gate is read as these M2 reports passing: the at-will rider
bound and the per-fight condition cap usage by theme (riders); V9 green
on the release seed set, `PreviewExactness` 100%, `HazardPushDeaths`,
`BurstSelfDamageShare` and net `FightRoundsByTheme` (layers) (11.3). The
lead records the pass in writing before M3 Remnant work starts; a failed
report holds Remnants back and the client is told.

| Rule | Value |
|---|---|
| Sources | Environment only: a gas Keg's burst (M3 Archive ink cask, Gut sac upgrade, Heart dust pillow), boss phase (e), segment-4 vents (struck vents emit when struck, `causedBy` the triggering unit; a breathing vent is its room's one Stir, `causedBy` `environment` or the Cord's operator; change HZ-16, hazards design 6.7). Never party abilities, monster at-wills or flasks |
| Kind | **Miasma only** (gas, spores, ink fumes, sleeping dust): `onEnter` and `onTurnEnd`, Fortitude save or Poisoned. Only in themes whose 6 holds Poisoned (Archive, Gut, Heart; the Supper only if RT-14 is ruled "does not count") |
| Footprint | Fixed at creation, ≤ radius 1 (9 squares), **clipped** off walls, pieces, condition squares, harm squares, Portal squares, the Clearway, `fleeExit` and in-room chokes. **Never drifts, spreads or grows** |
| Duration | Integer countdown: `roundsLeft` = 2 at creation *(illustr.)*. A new **round-end environment step** (from M2 (gated) for Stirs only, HZ-12) runs after the last turn's turn-end step (conditions 5.4) and before the next round starts; it decrements every Remnant in `createdSeq` order, skips Remnants created this round, and removes any at 0. Shown as a number on the glyph. All Remnants end when combat ends **(lead's call, after the check)** |
| Stacking | ≤ 1 per room. A new one from a **different** `sourceId` fails if one exists, and the preview says so; one from the **same** `sourceId` refreshes `roundsLeft` to full and never past it |
| Ignition (the headline) | A fire area overlapping it, or a fire part dealt to a unit inside, turns the whole cloud into **one** fire damage event (Keg band, Reflex for half; per part, death and healing design 20.3 step 2) **(agreed 2026-11-28, roadmap D-22; M1-11 rulings 1.1)** on every unit on its squares; then it ends. No fire squares; Burning only where it is in the theme's 6. The preview shows "Ignite: N damage to M units" whenever a fire area touches Miasma |
| Status | A hazard (11.3) for pathing and stupidity floors. **Routed units treat a non-lethal Remnant as passable** |
| Credit | `RemnantState.causedBy` is the unit whose action created it, used only for the condition cap: a party-triggered Remnant counts as party-sourced for the per-fight condition cap, which changes conditions 11.6 (TR-27). Ignition damage's `causedBy` is the unit whose action dealt the igniting fire (9.2). **Told plainly:** a party-triggered cloud is player-made ground effect in substance |
| Readability | A low translucent volume ≤ 35% opacity *(illustr.)* under units, in its own RC4 hue and pattern (a desaturated violet with a stipple, never bile green), edged by a hazard-tier rim; harm-square and Rough rims draw above it. RC1 amended for Remnant volumes only (TR-20, TR-27) |

**Deterministic evolution, stated:** a Remnant changes in exactly two
ways, its countdown and its ignition. Both are integer, with no draws,
in creation order. The numbers pass proved the model: ≤ 9 cells with
this cap, reach 1, exactly its duration, no cascades (TN8).

**Rejected or deferred:**

| Idea | Verdict | Reason |
|---|---|---|
| Drifting gas | Rejected | The one-step scorer cannot value drift; it needs a forecast term and a validator distance ≥ 6; a misread risk at 4 rotations; the DOS2 and Brogue failure (TN9) |
| Spreading fire | Rejected | Conditions 11.4 chose that fire never makes fire squares; Burning already spreads unit to unit (18.5); Model B's own bound fails (5–17 rounds, TN8) |
| Blood slicks | Rejected | An evolving surface; RC1; tone; Slick was cut (18.11) |
| Growing vines | Rejected | Static harm squares instead |
| Party abilities or flasks that create terrain | Rejected | Ability 8.4; consumables 12, 16; the Arcanist's "Never" |
| Outpour (breaking a piece creates squares) | Rejected | Reverses decided main 2 in M2 for little gain over Kegs |
| Fog (Chilled cloud) | Deferred | Low fun, readability cost; revisit if the Choir or Supper reports need it |
| Haze (smoke that blocks sight) | Later, with NM-9 Gloom | Moves the Shade's Hide-rate parity; the Shade rejected smoke clouds; RS 7.6 refuses light mechanics in M2 |
| Briar (harm square applying Bleeding) | M3, Kennel; its own client question then | Touches RS 6.3, 8.4 "deliberately dry", RT-20 and the 11.2 table (19.2) |
| Ice | Never in the Supper (RS 8.9) | **Glaze** (forced movement entering it slides 1 more square, re-triggering, inside the 3-square cap; no condition) is offered for a future frost theme, M3+ |

### 6.7 Destructible terrain (agreed, Q2; change TR-7, TR-8)

- **Breaks:** only Furnishings and Kegs (Smash, areas of a breaker type,
  collisions, fire for Kegs), and M3 tippable Tall Furnishings
  (Overturn).
- **Never breaks:** walls, partitions, Tall and Low pieces, hazard
  pieces and faces, and Portals. RS 7.6's refusal of breakable walls
  stands, so the V8 proofs, hearing, arena sight blockers and LG3 stay
  static.
- **Told plainly:** this reverses the Vanguard 8.3 rejected-list entry
  "breakable terrain" and the Vanguard 9 table "Rejected" row
  "destructible terrain" for objects only. That row rested on the main 2
  non-goal reading; the client agreed the reversal (Q2).

### 6.8 Combos (closed table)

| Id | Trigger | On | Result | Wave | Status |
|---|---|---|---|---|---|
| K1 | Fire area or fire part ≥ 1 | Oil square | Burns to plain floor; Oiled payout on the unit | existing | settled (11.4) |
| K2 | Same fire trigger | Web or bedsheet square | Burns to plain floor; a unit Restrained by that web is freed. No timer, existing code | M2 | **agreed, Q4**; TR-25 |
| K3 | Lightning part on a unit standing in water | Connected water | **Charged Water:** every other unit on water squares orthogonally connected to it within path 3 takes the lightning part, rider-free, cap 6 extra units. Instantaneous; no square changes. **Never starts from a monster at-will.** It follows ability 8.2: an at-will's or a selective cast's Charged Water skips the caster's allies, and it **never reaches the caster**. Counts in the enemies 6.4 guards and the Choir lightning spike row. Details below the table | M2 | **agreed, Q4**; TR-26 |
| K4 | Fire area | Keg | Bursts it (6.3.5) | M2 | **agreed, Q2**; TR-9 |
| K5 | Fire area or fire part | Miasma | Ignition (6.6), vent Remnants included (HZ-16; hazards design 6.7) | M3 | **agreed, Q5**; TR-27 |

**Charged Water in detail (lead's call, after the check):**
- one Charged Water event per action per lightning source; a lightning area that
  strikes several units in water starts it from each struck unit, in
  unit order, as one event;
- recipients are units on connected water that the triggering area did
  **not** already damage;
- the base is the struck unit's lightning part **before** its own
  weakness; each recipient applies its own weakness (Soaked), then
  halves, rounding down;
- a unit already hit by the same event's Soaked arc (conditions 18.6) is
  not reached by Charged Water as well; the arc resolves first;
- **told plainly:** Charged Water is the "conducting water" that stat model 2
  held back "until riders and layers are proven"; it arrives in M2,
  before that gate, because it is instantaneous and makes no square
  (TR-26).

**Order inside one action (lead's call, after the check):**
1. the triggering event's own damage to units;
2. square combos K1 and K2, in square order;
3. Keg bursts, in `furnishingId` order; each is a separate area whose
   own K1 and K2 effects apply to squares not yet processed this action
   (each square changes at most once per action);
4. Charged Water (K3), as above;
5. setup payouts (Oiled) with the instance that triggered them.

Bounds:
- **The table is closed at 5 rows. Any new row needs a client
  question.** Segment 4's Stir acts are new sources for K1, K2, K4 and
  K5 only, never K3 (hazards design 6.11 item 6).
- Each unit is still capped by the one-instance top-up rule (6.4,
  TR-13).

Rejected combos: water extinguishing fire squares, freezing water,
steam, poison plus fire spread, electrified surfaces, anything that
creates a square kind outside its theme's home set.

### 6.9 AI (requests to the enemies owner; change TR-15)

- **Cover:** no new term. The Exposure term reads the resolve preview
  (enemies 11.1).
- **Destination value:** the scorer adds the destination square's
  `onEnter` and `onTurnEnd` value (extends conditions 17.3). The toy
  showed a ranged AI ending 0.4% of turns on harm squares without it
  (TN16).
- **`NoCheapKill` (agreed, Q3; exact text for TR-15, lead's call after
  the check):** a term in the enemies 11.1 scorer. For each hero a
  candidate action would damage, if the hero's current HP minus the
  action's non-environment expected damage is ≤ the candidate's maximum
  environment damage to that hero (the failed-save branch), that
  environment damage scores 0 and the candidate gets no kill or down
  bonus for that hero. The candidate stays in the list and is still
  chosen if it wins on its other terms. It applies to non-boss monsters
  and to every trigger they cause: push, Shunt, Kick, Keg and Charged Water.
  **Pending request (change TL-16; the enemies owner rules; M2):**
  segment 5 extends it to trap springs a monster causes, with monster
  pushes and Shunts scoring only traps the party has Found (traps
  and locks design 6.8). Nothing changes until that owner rules.
- **`usesFurnishings` kit tag** (Weightlifter, Fire-Eater, Glutton, a Gaol
  warden brute, Gut Ooze *(illustr.)*): Attack Shunt, Kick and Smash of
  heavy pieces, valued by the existing forced-movement, collision and
  area terms. Monsters never Move-Shunt to barricade: a one-step scorer
  cannot value it (conditions 17.3). **Extended by `usesCords` (change
  HZ-15; with Stirs; the enemies owner applies):** one type per F2–5
  theme Operates Cords as a telegraphed intent, valued through the
  preview, under `NoCheapKill` (hazards design 6.3.4).
- **Universal light Smash:** every monster with a melee attack may Smash
  an adjacent light Furnishing, scored by the reduction in its next-turn
  path length to its preferred target (a one-step quantity). This stops
  party-built funnels from stalling the AI.
- **Kegs:** triggering one is valued as an area under the "no
  net-negative friendly fire" floor and `NoCheapKill`. A monster's Keg
  trigger counts as an attack in the intent read and in focus spread.
- **Holder:** "shoves toward hazards" includes contact hazards.
- **One visible terrain-using enemy per F2–5 theme**, with a telegraphed
  intent (a Weightlifter Shunts a crate at the backline; a Glutton Kicks a
  trolley; a Fire-Eater breathes on a Keg; a Gut Ooze shoves toward
  molars). **M3 (change EP-21; on the TR-15 ruling):** segment 6's
  Terrain Pairings seat the terrain-using enemy by its Furnishing; the
  Call Post (a bell rope or gong) is deferred to M3 until these
  Furnishings exist (change EP-36) (encounter placement design 6.6.8,
  9.1).
- **Tie-breaks:** Shunt and Kick ties use the fixed square order
  (ability 8.1).
- **Stupidity floors hold:** never enter a hazard that could kill;
  routed units never enter hazards, except non-lethal Remnants (6.6).
- **Told plainly:** players will out-think monsters with Furnishings.
  That is accepted as player power, and watched in `FurnishingUseRate`.

### 6.10 Class interplay

| Class | What terrain gives it | Watch |
|---|---|---|
| Vanguard: Wrecker | Contact hazards and Furnishings multiply collision partners; Ram into a brazier; Brink beside harm and condition squares; Battering Force against faces | `HewCeiling` with the contact term below, run with observed `collisionRate` and `contactRate` per theme (Ram at level 5+ reaches 450–496‰ in cluttered rooms on the toy); KP3 cap; `TeeteringUptimeOnBosses` ≤ 25% of boss turns *(illustr.)*, with the lever "bosses steady at turn start" held in reserve (19.2) |
| Vanguard: others | Low lines to hold; Shieldwall's reaction AC replaces cover (CV4) | Doorway holds gain no cover (Portal squares) |
| Shade | Tall nooks as Hide spots; cover at range; lurker sockets; "Needs a tall piece to hide" | Hide-spot band KP4; `HideSpotsPerRoom` against `hideSuccess` 700‰; the Shade yardstick re-run |
| Arcanist | Areas and saves ignore cover (the anti-marksman role); Gust and Reel pay the typed collision band against contact hazards, Upheaval and Squall the contact band; Keg ignition; K2 burn-away; Charged Water | Gust contact counted in the at-will rider bound (engine); area cap 2, one shared counter across contact, harm and Void squares (HZ-9; cross-document comparison 2026-11-23, CC-90); DEP-A1 by "interactive feature"; the solo floor-1 down rate with the adopted cover arm; `ArcanistComboShare` |
| Herald | Give Ground into hazards (capped at 2 contacts per action); saves ignore cover; Frighten near hazards pins routers | Give Ground's "tool, not a lean" budget |
| Companions | Shunt like any unit; Jostle, Heave and Barge are non-verb and collide | Prop verbs never offered on companion turns unless the player opens the target |
| Launcher users | Take cover against monster marksmen; lose ~13–25% against covered targets | Precision 75% line; damage-taken veto ≥ 50%; launcher family floor ≥ 60% |

**`HewCeiling` with contact (lead's call, after the check; change
TR-30).** Vanguard 6.4 rule 8 is a static formula, so "re-run counting
contact" needs an explicit term. For a line whose payoff is a
collision, the per-use damage for each hostile it can move becomes:

    collision × (collisionRate − contactRate)
      + contactRate × (collision × P(save) + contact × (1 − P(save)))

- `contactRate` *(illustr. 100‰)* is the share of uses whose move meets
  a contact hazard or a harm square; it sits in the tuning file beside
  `collisionRate` and is reported per theme.
- P(save) is the reference target's Reflex save against the reference
  difficulty of 6.5.2 *(illustr. 500‰)*. For a harm square, "contact"
  in the formula reads the harm band.
- **The per-unit clause** ("no single unit takes more from one use than
  Hew's expected damage") **reads the collision band only.** The contact
  surplus (+1) is bounded per unit by `ContactLethalityBound` and the
  one-instance top-up rule instead. Without this a Ram into a contact
  hazard fails the per-unit clause at every checked level (3 > 2.7 at
  levels 1–4; 4 > 3.8 at levels 5 and 10). This amends a lead's call in
  the Vanguard design, so it is a change row for its owner (TR-30), and
  told plainly.

### 6.11 Boss arena terrain rules (segment 9 inherits; change TR-16)

Segment 3 sets **caps and kind rules only**. The counterplay identity of
each boss stays with RS 13 and segment 9; the Supper's is the chandelier
of icicles, a segment-5 trap. Braziers may be extra arena pieces, never
the counterplay (read by change BR-16: a boss Counterpiece is never a brazier;
boss rooms design 4.7.1). Arena Counterpieces are segment-9 instances in
segment 5's TrapRecord format (change TL-40; with segment 9; traps and
locks design 7.8), of kind `arenaDevice`, one per boss (F5: two)
(change BR-16; M1 for the Den, M2; boss rooms design 4.7.1).

- ≥ 2 Tall pieces or clusters that block line of sight (enemies 17.5); ≥
  4 cover-giving squares per half *(illustr.)*; clutter 5–12%.
- Contact hazards ≤ 4; none within 3 of the Forecourt Arches, either
  arena entrance or the arena start zones, nor within 3 of a boss lane
  (VP7 `BossLaneClear` extended to contact hazards); 0 on F1 (RT-20)
  (change BR-1; boss rooms design 4.1.1, 4.1.4).
- Furnishings ≤ 4; Kegs ≤ 1 (Carnival or Gut only in M2).
- **Voids (change HZ-11; M2 (gated), else M3):** at most 1 Void region,
  ≥ 3 from the Arches, either entrance, the start zones and boss lanes;
  its own line, not counted as a contact hazard (hazards design 6.4).
- **Phase (e)** "change arena terrain within the theme's 6, never on a
  party unit's square" (settled) uses **phase sockets**:
  - pre-placed piece or square sockets that V9 proves in both states;
  - announced one round ahead;
  - never on a party unit's square or the Clearway;
  - existing kinds only.

  This is named a sanctioned exception in the consolidated conditions
  11.1 change (TR-8), which reconciles RS 8.6–8.11 bosses that flood or
  add webs, oil, fire, gall or bedsheets mid-fight.
- M3: phase (e) may also create a Remnant **(agreed, Q5)**.
- Phase sockets are the precedent for segment 4's M3 pre-proven Stir
  states (Tides, Cord Flips, Turning Ring boards), under the same rules
  (change HZ-18; agreed, hazards Q3; hazards design 6.3).
- Bosses use authored, forecast terrain moves. Each arena should give
  its boss one, authored by segment 9 (boss rooms design 4.1, 5).
- **Arena templates (change BR-1; lead's call; M1 for Old Ragged's
  Den, M2 for all; approved 2026-11-22):** segment 9 stamps each arena
  from an authored `ArenaTemplate` Guise at one authored size per
  (Guise, floor band), inside these caps; generated arena fill is
  rejected; the stamp runs V9 (with `PhaseSocketsBoth`) before V17g
  (boss rooms design 4.1). **Pending request (segments 11–12; BR and
  ROOM confirm):** the stamp runs before terrain, with the arena node's
  terrain slot spent as dummies (6.2.5; seeds and validation design
  4.4). **M1 reading (told plainly):** Old Ragged's
  root-columns are template walls, not RT pieces, and count as its Tall
  clusters and line-of-sight Deny; RS 12's M1 terrain is Tall and Low
  pieces as blockers only, per 12 (change TR-31; cross-document
  comparison 2026-11-23, CC-86), and the root-columns sit beside those
  blockers (boss rooms design 5.3).

### 6.12 Previews and UI (no rules in display; change TR-23)

- **Push, Shunt, Kick and Smash preview (Into the Breach grade),**
  returned by the core before commit:
  - every path and end square;
  - each collision star with its exact damage;
  - contact and harm results with damage type;
  - **edge-save odds as the headline number, with the difference a
    failure makes** over a plain obstacle ("Spikes: 55% for +1");
  - Teetering (from condition squares);
  - Keg footprints and per-unit damage;
  - cover changes;
  - lethal tags and kill marks.
- **Attack preview:** cover pip and "Cover −2" (or the replacement note
  under CV4).
- **Move preview** (only while the move tool is open, never on the
  companion card): path cost with Rough; harm-square damage summed per
  square entered, with a lethal tag; Plank odds; "In cover from N of M
  known enemies" from party knowledge only (ability 8.3).
- **Cover shields (lead's call, after the check).** While the move tool
  is open, the hovered destination shows a shield marker on each of its
  8 edges or corners where a cover-giver stands that faces at least one
  known enemy (CV2, from the same function as the test vectors). Each
  known enemy's plate gets a small shield tick if the square is Covered
  against it, or a broken-shield tick if the square is in its line of
  sight and not Covered (it flanks the square). Outside the move tool,
  a selected ranged unit's targetable enemies that are Covered show the
  shield pip on their plates. Markers are anchored in screen space and
  re-derived after each camera rotation. All of it uses the global UI
  skin (RC7).
- **Contextual verb chip:** when a selected unit is adjacent to a
  Furnishing **and** the action pays off (a collision with a unit, an
  aligned hazard, or removing a known enemy's cover), one chip shows the
  outcome ("Shunt → 3 to Archer"). Otherwise the verbs are hover-only.
  No extra clicks.
- **Ally confirmation** (DH 17.7) for contact hazards, harm squares, Keg
  footprints, Kick lines and Overturn strips.
- **Pending request (change HZ-13; the DH and enemies owners rule):** a
  Stir's **due squares** join these previews and are hazards for
  pathing, and the DH 17.3 danger band would include environment acts
  due before the hero's next turn (hazards design 6.3.1, 6.18).
- **`PreviewExactness`** **(lead's call, after the check):** a preview
  is a function of the **party-knowledge state** plus one branch per
  save outcome. Squares of a Kick line, push path or Keg footprint that
  the party cannot see are marked "unknown" and never show a unit, so no
  information leaks (ability 8.3). The test compares, branch by branch,
  the preview with resolution on the same knowledge state over N seeds:
  100%. Mismatches caused only by units the party could not see are
  logged separately as `PreviewHiddenDelta` (expected to be nonzero, not
  a failure).

---

## 7. Per-theme terrain kits (change TR-22)

**(lead's call; stand as written)**

### 7.1 Summary

| Theme (floor) | Tall | Low | Furnishings (light / heavy; round) | Keg | Contact hazards | Harm skin | Rough | Plank | Squares | Signature interaction (terrain Law) |
|---|---|---|---|---|---|---|---|---|---|---|
| Gaol (F1) | stone pillars, records cabinets, Landmark clock-case | ledger desks, bolted benches, foot stocks | filing crates, lidded pails / tally desk; laundry cart (round) | — | — (warden's stove = typed Low piece) | — | straw, spilled ledgers | gap beside the desk rank | — | Loud verbs wake the block: noise is the terrain |
| Kennel (F1; M1 subset) | oak trunks, root plates, lodge chimney | fallen logs (crossable), hurdle fence runs, hay mangers | hay bales, feed barrels (round) / meat cart | — | — | (M3 Briar) | bracken, root tangles | deer-run between log lines | — | Backs to logs; the pack flanks round cover |
| Wake (F1) | catafalque canopies, stopped clocks, draped easels | pew rows, lidded coffins on catafalques, lily urns | wreath trestles, draped side tables / lectern | — | — (candle rack = typed Low piece) | — | fallen wreaths, strewn lilies | aisle between pews and trestles | — | Canopies and Drapes as sight breaks; Overturn candelabra (M3) |
| Choir (F2–3) | nave pillars, organ-pipe clusters | pews on islands, dry font plinths | hymnal crates, kneeler benches / cantor's desk; hymn cart (round) | M3 cistern butt (Soaked, no damage) | storm-glass pipe face (lightning), fallen-bell face (bludgeoning, noise 6) | broken storm glass | fallen hymnals, dry vault rubble | dry causeway between pools | water (home) | Charged Water across water (K3); the bell |
| Archive (F2–3) | book columns, catalogue towers | reading desks, carrels, folio heaps | returns trolleys, paper bales (fire-fragile) / book cart (round) | M3 ink cask (Poisoned) | lamp-sconce face (fire), nib-rack face (piercing) | spilled nibs | ink-dark paper drifts | clear path through a web field | web (home) | Burn webs off the aisles (K2); lamps against fire-weak spiders |
| Carnival (F2–3) | wagons, booths, tent poles, Wheel of Knives (always with its knife-wheel face) | ring curbs, ticket crates, bale lines | crates / costume trunks, beast crates (sturdy, solid-sided); balancing ball (round) | **lamp-oil keg (M2)** | braziers (Hot), knife-wheel face | broken bottles | trampled bunting, collapsed canvas | duckboards between oil slicks | oil, fire (home) | Oil keg into oil-then-fire |
| Supper (F4) | high tables (sight walls), swan towers, dumbwaiter shaft | dining tables with chairs tucked in, sideboards | serving trolley (light, round) / linen presses | M3 frost-wine cask (cold, Chilled) | plate-warmer braziers and boiling tureen (Hot), carving-station face (piercing) | shattered crystal | fallen place settings | service gap beside a warmer line | none (dry) | Braziers end Chilled and Frozen, both ways |
| Gut (F4) | rib pillars, cartilage knots, tenant-house corners | gristle mounds, tenant sofas, villus stubs | tenant stools, lidded crocks / rent chests | **swollen sac (M2)** | grinding-molar face (piercing), acid-scald fold (acid) | bone shards | half-digested furniture | plank flush across gall | gall (segment 4) | Sacs burst; Oozes shove toward molars |
| Heart (F5) | bedposts, wardrobe sides, toy-fort walls, block towers | hatboxes, flat storybooks, slippers | toy blocks / toy chest; toy cart and giant ball (round) | M3 dust pillow (psychic, Poisoned) | fireplace-grate face (fire), pincushion face (piercing, needles flat) | thorn-rose vines (the client's thorny vines) | heaped storybooks, crayons | ruler laid flush | bedsheet web (home; RT-14) | Kick the giant ball |

- **New lead's call:** the Supper's serving trolley is its one **light**
  Furnishing, and it is round. The proposal listed no light Supper
  piece, which left Move Shunt and monster light Smash with nothing to
  act on in the cluttered Supper; chairs stay tucked in (RC2) and are
  never pieces.
- The signature lines are proposed as a one-line "terrain Law" per theme
  in RS 8.x (TR-22), so the art team and segment 4 build toward them.
- **(lead's call, after the check)** The Wake and the Gut have **no
  round piece in M2** (no Kick there). The Heart's harm skin is
  **thorn-rose vines**, the briar-rose of the sleeping house: the
  client's "thorny vines on a cell" in M2, condition-free and
  damage-only; Briar with Bleeding stays the separate M3 Kennel
  question. The Carnival's round piece is a balancing ball, not a
  barrel, and the Supper's heavy pieces are linen presses, not casks, so
  no inert piece shares a silhouette with a current or planned Keg
  (8.1). Wreath trestles, foot stocks and ticket crates are low forms;
  liquid-holding pieces are dry or lidded (dry font plinths, hay
  mangers, lidded pails, lidded crocks).
- **Told plainly (TR-22):** the Supper is "deliberately dry (no
  squares)" (RS 8.9) and the Gaol and Kennel "deliberately dry" (RS
  6.3). This document reads **dry as "no condition squares"**: the
  condition-free harm, Rough and Plank squares appear there (the
  Supper's shattered crystal; Rough and Plank in all three).

### 7.2 The Gaol of Small Hours (F1)

- **Law:** Lockdown (doors, noise, keys). **The 6:** full. **Kit
  condition cost:** 0.
- Smash (noise 6), Shunt (noise 4) and Kick are loud, so a careless
  party wakes the block. Desk lines let a patient party slip a patrol.
- **Teaching room:** the laundry cart (round) in line with a lone
  warder, for a Kick or an Attack Shunt into him (6.5.6).
- **Told plainly (TR-22):** RS 8.3 "cover: cell bars and desks" becomes
  "desks, benches and foot stocks", because bars appear only on
  Grilles (FL 8.1).
- Cell cots stay fixed Low pieces in cell Alcoves (the Penitent's hide
  spot and lurker socket). A moved cot could reach a cell Door, which
  the Clearway forbids.
- **Readability:** no loose door panels (Turnkey, RC2); only the
  Landmark clock is a piece; straw never reads as the Penitent's hood
  tell.
- **M3:** the stove may become a Hot hazard piece if floor-1
  `HazardPushDeaths` passes and RT-20 is reopened (19.2). **Applied at
  Peril tier 2+ only (change HZ-25; agreed, hazards Q4; M3):** the
  stove is promoted to a Hot piece, typed bludgeoning ("scalding iron"),
  for the Treadwheel package, under its floor-1 `HazardPushDeaths` gate;
  below tier 2 it stays a typed Low piece (hazards design 6.19).

### 7.3 The Kennel Wood (F1; M1 subset)

- **Law:** the pack and the horn. **The 6:** 5.
- **M1:** trunks and root plates are the Circuit `coverRoom`'s
  line-of-sight cover; logs block movement only (CV1).
- **M2:** logs give cover; hay bales and feed barrels move; bracken
  Rough.
- **Rulings:** the impassable thicket around glades is layout's sight
  boundary, never a hazard. Kennel gates are Portal skins, not cover
  pieces. **Told plainly (TR-22):** RS 8.4's `signatureFeature`
  "kennel gates and fallen logs (cover; an arena gate can cut the
  escort's approach)" and `signatureFamily` "cover gates" become "fallen
  logs (cover) and kennel gates (Portals)", family "cover logs".
- **M3:** a thorn-hedge contact hazard and Briar with Bleeding (Bleeding
  is in the 6), each behind its own question (19.2).
- **Readability:** fence pieces only as runs, never a lone panel
  (Huntsman's hurdle, RC2); bracken stays flat (the Ghillie's rest
  tell).

### 7.4 The Endless Wake (F1, conditional)

- **Law:** the sleeping house. **The 6:** full.
- **Told plainly (TR-22):** RS 8.5's "closable coffins (cover and hiding
  spots)" against FL 7.4's "sealed coffins never hold a unit". Coffins
  are fixed Low cover; units never occupy a piece; hiding comes from
  Tall canopies and Drapes; coffin ambush tells are segment-6 lurker
  sockets on fixed pieces.
- **M3:** candelabra Overturn (bludgeoning and Prone; the candles snuff
  out, no fire).
- **Readability:** no upright coffin lid anywhere (Pallbearer, RC2); no
  veiled figures (RC3); no plumed staffs (Undertaker).
- **Loot (change LP-18; lead's call; M2 cond.):** loot sits in
  knee-high burial caskets with a horizontal lid; **full coffins
  never hold loot**, so a coffin reads only as cover or a lurker tell.
  **Pending request (traps owner, TL 7.4 opening paragraph):** the
  same wording there (loot sources design 5.3).

### 7.5 The Drowned Choir (F2–3)

- **Law:** the drowning line. **The 6:** full; the kit uses Soaked
  (existing water) and lightning damage only.
- Water placed in pools and rims by wetness band. RS 8.6 caps encoded:
  - no water under or next to party entry;
  - a dry island within 2 of each entrance;
  - ≤ half of enemy starts in water.
- **Contact hazards:** storm-glass pipe face (lightning; the Soaked
  weakness applies to the amount, never arcs); fallen-bell face
  (bludgeoning; a noise-6 strike with a reserved struck-bronze gameplay
  cue, kept distinct from the alarm bell, added to RC7's reserved cues,
  TR-20).
- **K3 Charged Water (agreed, Q4)** makes the Law a combo, both ways: the
  Organist can use it through slot or recharge abilities, never
  at-wills.
- **Rulings:** deep water as drowning is rejected (10.4; segment 4).
  Floating pews are rejected: Furnishings stop before hazard squares, so
  pews never enter water.
- **Readability:** no lone upright pew-back (Drowned Verger, RC2); no
  wet sheen on dry squares (FL 7.5).

### 7.6 The Cobweb Archive (F2–3)

- **Law:** webs and silence; fire is the key. **The 6:** full; the kit
  uses Restrained (webs) and, in M3, Poisoned.
- Webs as lanes at aisle mouths, never on the Clearway. Lamp-sconce
  faces against fire-weak spiders and paper. **K2 burn-away (agreed,
  Q4)** finally shows the Law on the map.
- **Rulings:** ladders are backdrop only; the toppling-shelf **trap**
  stays segment 5's, while a visible, chosen Overturn of a cabinet (M3)
  is segment 3's.
- **Readability:** paper drifts are ink-dark, never white (they must not
  read as web, RC1); no hovering pages (the Scribe-Spider orbit, RC2);
  reading desks and carrels have flat tops, never a sloped or upright
  board (the Shusher's lectern board, RC2).

### 7.7 The Carnival of the Last Evening (F2–3)

- **Law:** oil and fire both ways. **The 6:** full; the kit uses Oiled
  and fire damage.
- The busiest kit, so the hardest caps:
  - ≤ 2 Kegs per room;
  - standard band;
  - plaza 5×5 core clear;
  - RS 8.8 entry caps;
  - Keg bursts counted in the flask-then-breath spike guard every round.
- Fire-eater pits are existing fire squares, which Strongmen shove
  toward.
- **Rulings:** carousel horses stay on poles and never move (FL 7.7); no
  circus cannon (an artillery silhouette); no spreading fire.
  **Told plainly (change HZ-18; agreed, hazards Q3; M3):** segment 4's
  Turning Ring (F3) moves units on ring boards around the hub while the
  horses stay on their poles, so FL 7.7's letter holds and the
  carousel's spirit is contested (hazards design 7.7).
- **Readability:** saturated fire hue only on fire squares, braziers and
  attacks (RC4); no loose chairs (Lion-Tamer, RC2).

### 7.8 The Long Supper (F4)

- **Law:** freeze and shatter both ways. **The 6:** full. Dry; no ice
  (RS 8.9).
- **Told plainly (TR-22):**
  - long tables split into Tall high tables (sight walls) and Low dining
    tables (cover);
  - FL 7.8's "no two consecutive bays share a table layout" is met by
    2–3 table layers per bay template;
  - RS 8.9's "braziers as cover props" become Hot hazard pieces: they
    give cover and hurt; fire ends Chilled **and Frozen**.
- These pieces likely close the told-plainly floor-4 DEP-A1 miss (TR-21;
  TN14). Arcanist E3 combos still need condition squares, which the
  Supper never has.
- **Readability:** no trays (soldiers, RC2); chairs only tucked in; ice
  sculptures as swans and towers only; chandeliers over fight squares
  are always the icicle trap (FL 7.8).

### 7.9 The Gut of the World (F4)

- **Law:** attrition. **The 6:** full; the kit uses Corroded (gall) and
  Poisoned.
- Gall rules and density are segment 4's (RT-13); segment 3 shapes gall
  pools in rooms. **Set by segment 4 (change HZ-4):** 6–12% of a Gut
  fight room's walkable squares *(illustr.)* in pools of 3–8, Corroded
  on entry, a Fortitude save negates it, with a dry route at most 2
  squares longer (hazards design 6.5). The swollen sac counts toward "≤
  2 acid sources per encounter"; gall squares are never a source
  (change HZ-5; hazards design 6.5).
- **Readability:** saturated bile green only on gall and acid; sacs in
  off-key olive with the hazard ring; wall pulses never read as shifting
  rooms (RS 7.6); no orbiting spores (Spore-Priest, RC2).

### 7.10 The Dreamer's Heart (F5)

- **Law:** waking. **The 6:** full; the kit uses Restrained (bedsheets)
  and, in M3, Poisoned.
- **If RT-14 is refused** and the bedsheets go, floor 5 keeps contact
  hazards, Furnishings, Kick, Plank and its thorn-rose harm squares.
- **Rulings:** loose marbles rejected (a chaotic slide surface); toy
  soldiers, rocking horses and dolls never pieces (FL 7.10, the
  Pale Mare); needles only flat.
- **Readability:** toys sinister, never cute; scale distortion in
  architecture only, and **rules pieces count as architecture** for that
  clause (RC6): the giant ball, slippers and hatboxes are giant pieces
  of a giant room, while units stay true to their square. Storybooks lie
  flat or spine-up, never upright or angled (the Storybook Knight's
  shield, RC2).

### 7.11 M2 build table (lead's call, after the check)

One row per Furnishing, Keg, hazard and harm skin, so blockouts can
start. Tall and Low pieces are fixed, never break, and (Low) give cover
from M2; they are not repeated here. Breakers: F fire, B bludgeoning and
force. "Broken" is the state after breaking: gone (break VFX only) or
debris (debris mesh with the Rough hatch). Kit height ≤ 0.6 squares for
every row except faces, which take their host's height.

| Theme | Skin | Class and weight | Round | Breakers | Fragile to collision | Broken | Cover | Damage type | Wave |
|---|---|---|---|---|---|---|---|---|---|
| Gaol | filing crates | Furnishing, light | — | F, B | yes | gone | yes | — | M2 |
| Gaol | lidded pails | Furnishing, light | — | B | yes | gone | yes | — | M2 |
| Gaol | tally desk | Furnishing, heavy | — | F | no | debris | yes | — | M2 |
| Gaol | laundry cart | Furnishing, light | yes | B | no | gone | yes | — | M2 |
| Kennel | hay bales | Furnishing, light | — | F | no | gone | yes | — | M2 |
| Kennel | feed barrels | Furnishing, light | yes | B | no | gone | yes | — | M2 |
| Kennel | meat cart | Furnishing, heavy | — | F | no | debris | yes | — | M2 |
| Wake | wreath trestles | Furnishing, light | — | F | yes | gone | yes | — | M2 |
| Wake | draped side tables | Furnishing, light | — | F | no | gone | yes | — | M2 |
| Wake | lectern | Furnishing, heavy | — | F | no | debris | yes | — | M2 |
| Choir | hymnal crates | Furnishing, light | — | F, B | yes | gone | yes | — | M2 |
| Choir | kneeler benches | Furnishing, light | — | F | no | gone | yes | — | M2 |
| Choir | cantor's desk | Furnishing, heavy | — | F | no | debris | yes | — | M2 |
| Choir | hymn cart | Furnishing, light | yes | B | no | gone | yes | — | M2 |
| Choir | storm-glass pipe face | Hot face | — | never | — | — | no | lightning | M2 |
| Choir | fallen-bell face | Bladed face | — | never | — | — | no | bludgeoning | M2 |
| Choir | broken storm glass | harm square | — | — | — | — | no | slashing | M2 |
| Archive | returns trolleys | Furnishing, light | — | B | no | gone | yes | — | M2 |
| Archive | paper bales | Furnishing, light | — | F | no | gone | yes | — | M2 |
| Archive | book cart | Furnishing, heavy | yes | F | no | debris | yes | — | M2 |
| Archive | lamp-sconce face | Hot face | — | never | — | — | no | fire | M2 |
| Archive | nib-rack face | Bladed face | — | never | — | — | no | piercing | M2 |
| Archive | spilled nibs | harm square | — | — | — | — | no | piercing | M2 |
| Carnival | crates | Furnishing, light | — | F, B | yes | gone | yes | — | M2 |
| Carnival | costume trunks | Furnishing, heavy | — | F | no | debris | yes | — | M2 |
| Carnival | beast crates | Furnishing, heavy, sturdy | — | B | no | debris | yes | — | M2 |
| Carnival | balancing ball | Furnishing, light | yes | B | no | gone | yes | — | M2 |
| Carnival | lamp-oil keg | Keg, heavy, sturdy | — | B (fire ignites) | yes | burst, gone | yes | fire when ignited | M2 |
| Carnival | braziers | Hot piece | — | never | — | — | yes | fire | M2 |
| Carnival | knife-wheel face | Bladed face | — | never | — | — | no | slashing | M2 |
| Carnival | broken bottles | harm square | — | — | — | — | no | slashing | M2 |
| Supper | serving trolley | Furnishing, light | yes | B | no | gone | yes | — | M2 |
| Supper | linen presses | Furnishing, heavy | — | F | no | debris | yes | — | M2 |
| Supper | plate-warmer braziers, boiling tureen | Hot piece | — | never | — | — | yes | fire | M2 |
| Supper | carving-station face | Bladed face | — | never | — | — | no | piercing | M2 |
| Supper | shattered crystal | harm square | — | — | — | — | no | slashing | M2 |
| Gut | tenant stools | Furnishing, light | — | B | no | gone | yes | — | M2 |
| Gut | lidded crocks | Furnishing, light | — | B | yes | gone | yes | — | M2 |
| Gut | rent chests | Furnishing, heavy | — | B | no | debris | yes | — | M2 |
| Gut | swollen sac | Keg, heavy, sturdy | — | B, F | yes | burst, gone | yes | acid | M2 |
| Gut | grinding-molar face | Bladed face | — | never | — | — | no | piercing | M2 |
| Gut | acid-scald fold | Hot face | — | never | — | — | no | acid | M2 |
| Gut | bone shards | harm square | — | — | — | — | no | piercing | M2 |
| Heart | toy blocks | Furnishing, light | — | B | no | gone | yes | — | M2 |
| Heart | toy chest | Furnishing, heavy | — | F, B | no | debris | yes | — | M2 |
| Heart | toy cart | Furnishing, light | yes | B | no | gone | yes | — | M2 |
| Heart | giant ball | Furnishing, heavy | yes | B | no | debris | yes | — | M2 |
| Heart | fireplace-grate face | Hot face | — | never | — | — | no | fire | M2 |
| Heart | pincushion face | Bladed face | — | never | — | — | no | piercing | M2 |
| Heart | thorn-rose vines | harm square | — | — | — | — | no | piercing | M2 |

Rough and Plank fills, M3 Kegs and tippable pieces are listed in 7.1 and
6.3.7. The rows are data for `PieceKind` (11.1); a skin's rules key is
its class, weight, sturdy, round, breakers, fragility, debris and
damage type.

---

## 8. Readability and art kit rules (change TR-20)

**(lead's call; stand as written)**

### 8.1 Rules for the kit

- **RC1+.** Every object on a walkable square is a rules piece. Dressing
  lives only on walls, backdrop, non-walkable squares or above head
  height. Exception: M3 Remnant volumes are the one floor-level particle
  allowed to imply a condition **(agreed, Q5)**.
- **RC2.** No piece uses a chair, tray, door, upright coffin lid,
  shield, tall staff, forward-pointing tube, orbiting element or unit
  silhouette. Chairs appear only tucked into tables; Low pieces that
  could read as shields come only as multi-square runs.
- **RC4+.** The saturated damage hue is allowed on contact hazards in
  every F2–5 theme. Fire hue stays limited to fire squares, Hot pieces
  and attacks (the RS 8.8 line extended).
- **RC5+.** One glyph set per piece class and square class, across
  themes, built from four composable marks **(lead's call, after the
  check:** the proposal's list tied the cover read to Low pieces and let
  walkable and non-walkable hazards share a spike**)**. Marks stack in
  the order base, motion, danger, cover, from the piece's base outward:

| Class | Base mark | Motion mark | Danger mark | Cover mark (M2 on) |
|---|---|---|---|---|
| Tall piece | "blocks sight" cap glyph on the **stub** state | — | — | at a peek only, on the hover shields (6.12) |
| Low piece | plain base | — | — | shield notch |
| Furnishing, light / heavy | scuffed base ring (thin / thick) | — | — | shield notch |
| Furnishing, round | scuffed base ring | arc mark | — | shield notch |
| Keg | scuffed base ring | — | raised solid hazard rim plus one abstract pulsing hazard pip on top (no literal fuse, so it fits casks and sacs); footprint ring (8.1 RC6+) | shield notch |
| Hazard piece | plain base | — | raised solid hazard rim plus damage-type icon | shield notch |
| Hazard face | its host | — | rim and type icon on the face, plus a floor decal on its one front square (below) | — |
| Harm square | floor fill | — | **inset dashed** rim plus a "walk-damage" footprint mark; the damage type shows through the RC4 hue and pattern | — |
| Rough | hatching | — | — | — |
| Plank | edge stripes | — | — | — |
| Debris | debris mesh with Rough hatching | — | — | — |
| Remnant (M3) | volume plus countdown number | — | its own hue and stipple, hazard-tier rim | — |

  The cover mark is one separate layer: off in M1 (CV1), on for every
  cover-giver from M2.

  **Extended by segment 4 (change HZ-29):** the hazards design's RC5+
  extension table adds rows in the same four marks for the Void (lip
  glyph and a new drop rim), Snag, Cord, vent, latent footprint, due
  square, Tide socket, Lurch lane, Handhold, Chainpost and staked pit,
  each in its own wave (hazards design 8).
- **Hazard faces read from the floor.** Every hazard face projects a
  floor decal onto its one front square: a half-rim along that square's
  edge facing the face, plus the type icon, drawn at hazard tier
  whatever the wall's cutaway state. The stub state of a faced wall or
  Tall piece keeps a coloured cap band. `TemplateReadability` checks
  faces at 4 rotations with the camera-side stub.
- **Kit silhouettes.** No Furnishing or Low piece in a theme shares a
  silhouette family with that theme's current or planned Kegs (7.1: the
  Carnival's balancing ball, the Supper's linen presses, the Heart's
  hatboxes). A Tall piece skinned like a hazard always carries that
  hazard (the Wheel of Knives always has its knife-wheel face, drawn as
  a blade rim on a post, never a flat round board that echoes the
  Knife-Thrower's target board, RC2).
  **Pending request (change TL-43; the RS owner and art rule; M2):**
  each theme's reserved trap-tell and device-body silhouette families
  join this rule; device bodies follow RC2's piece bans, and trap tells
  never idle and never reuse hazard prop families (traps and locks
  design 7, 8).
  **Extended by segment 7c (change LP-15; lead's call; M2):** loot
  containers, clasp hosts, the bait cache, Truants (M3) and the
  cross-theme variants (signature cache, Wager cages, Parting Fare host)
  join this rule: within a theme no family shares a silhouette with
  Furnishings, Low pieces or Kegs; container art is rank-blind
  (`ContainerArtBlind`); an inset container never shares a silhouette
  with the theme's Latch, Door or Hairline skins, and its wall-set mark
  is a solid bracket, never the harm square's inset dashed rim. V15
  `ContainerFamilyDistinct` checks it (loot sources design 4.3, 5.1
  CT1, CT2, CT15).
- **Liquid-holding pieces** are drawn dry or lidded, with no reflective
  fill, and their break leaves no liquid on the floor (RC1; the VFX
  residue rule, 8.2).
- **RC6+ order:** units > telegraph overlays (intents, push arrows,
  collision stars, Keg footprints while a preview or intent would burst
  them, phase-socket outlines) > hazards (condition squares, contact
  hazards, harm squares, Kegs and their idle footprints, Remnants) >
  cover shields > Furnishings and Low pieces > Tall pieces > floor
  (Rough, Plank) > walls.
- **Heights:** Tall pieces are at full wall height (FL 8.1 partitions;
  RC1) and fade or stub on the camera side. Low pieces and Furnishings
  ≤ 0.6 squares, in a stated low form (wreath trestles, foot stocks,
  ticket crates). V9 `TemplateReadability` checks that Tall and Low are
  distinguishable in stub states at 4 rotations, and checks each skin's
  kit height against its class.
- **Gates:** the greyscale, colour-blind (now including harm squares,
  Rough, Kegs, the VFX of 8.2 and, at M3, Miasma over harm squares and
  Rough) and 85% role-ID screenshot gates run with pieces in frame, and
  the role-ID gate includes "Keg or Furnishing". **Segment 7c adds
  (change LP-15; the RS owner applies; M2):** "container or Furnishing"
  and its other CT1 rows (loot sources design 5.1). A failing theme is
  held; the gate never changes a rule.

### 8.2 Art kit budget *(illustr.)*

Per theme, M2 (reconciled with the build table of 7.11):
- 2 Tall and 3 Low meshes (stub, full and cutaway states); further Tall
  and Low skins in 7.1 are dressing variants (retextures) of these;
- the Furnishings of 7.11: 1–3 light, 1–2 heavy and 0–2 round (the Wake
  and the Gut have none), each as intact plus break VFX (light) or
  intact plus a debris mesh with the Rough hatch (heavy);
- 1 Keg (Carnival, Gut);
- 1–3 contact hazards (floors 2–5), each with its floor decal;
- harm, Rough and Plank fills;
- the VFX set below.

**VFX (lead's call, after the check).** Every event finishes inside the
resolve beat *(illustr. ≤ 0.6 s)* and stays inside its previewed
squares. **Residue rule:** nothing remains on the floor after
resolution, no decal and no particle, except debris as Rough (RC1).

| Event | Spec |
|---|---|
| Collision | Collision star on the struck side |
| Contact | Contact flash in the hazard's type hue, on the unit |
| Shunt and Kick travel | A short tween along the previewed line; a scrape, never a physics roll |
| Light Smash, light break | The piece breaks apart and is gone, with no lingering chips |
| Heavy break | A swap to the debris mesh with the Rough hatch |
| Fire breaker on a fragile piece | A brief flare that ends with the piece gone; no embers left |
| Lamp-oil keg, unlit break | A splash on units only (Oiled), never a floor decal, so it never reads as oil squares |
| Lamp-oil keg, ignited | A fire burst inside the footprint, ending with the burst |
| Gut sac burst | An acid-and-gas puff that dissipates within the burst and never lingers (it must not read as a Remnant) |
| K2 burn-away | The web or bedsheet fades to plain floor with no embers |
| K3 Charged Water | Travels along the water surface between recipients, never between unit bodies (it must not read as an arc rider) |
| Phase socket | The authored change, after its one-round announcement outline |

That is about 12–14 rules pieces per theme on top of 41–50 templates. M3
adds a tippable Tall Furnishing, other Kegs and a Miasma volume on a
per-room particle budget.

**Godot:** Tall and Low pieces on GridMap or MultiMesh. Furnishings are
light Node3D scenes tweened from core events, ≤ 6 per room and ≤ 40 per
floor *(illustr.)*, with **no physics bodies**.

---

## 9. New mechanics considered

### 9.1 Table

| Mechanic | Source | Verdict | Wave |
|---|---|---|---|
| Directional one-tier cover, −2 to the roll | A, B, C, kits | **Adopted** (6.1) **(agreed, Q1)** | M2 |
| Cover as +2 AC | C, kits | Rejected: same hit chance, but changes shown AC and guards | — |
| Cover as disadvantage | new-mechanics | Simulator arm only (solo Arcanist +6 points of down rate); Q1 option B, rejected | — |
| Two tiers with a flank bonus | B | Rejected (dead tier; the Shade 9 rejection of flanking) | — |
| Take Cover / Hunker action | research | Rejected (Dodge exists; budget full) | — |
| Furnishings, Shunt (two modes), Smash | C, kits, fun critique | **Adopted** **(agreed, Q2)** | M2 |
| Kick (round pieces) | B, new-mechanics, fun critique | **Adopted**, brought forward **(agreed, Q2)** | M2 |
| Overturn, Haul | B, new-mechanics | Adopted | M3 |
| Shoot an object (ranged hit breaks or bursts) | new-mechanics | Adopted | M3 |
| Furnishing HP and AC | B | Rejected (bookkeeping) | — |
| Lift, carry, Hurl, stacking | B | Rejected (Hurl Later at most) | — |
| Kegs, sturdy in M2, no chains in M2 | kits, balance critique | **Adopted** **(agreed, Q2)** | M2 |
| Powder-keg blast push | B, C | Later catalogue | M3+ |
| Contact hazards with the obstacle-hazard save | A, B, C, kits | **Adopted**, changed (no Teetering; typed collision for verb-only pushes; area cap) **(agreed, Q3)** | M2 |
| Harm squares | A, new-mechanics | **Adopted** **(agreed, Q3)** | M2 |
| Briar with Bleeding | A, C, kits | M3, own question | M3 |
| Rough | all | **Adopted** | M2 |
| Plank narrow ground | all | **Adopted** | M2 |
| Glaze ice slide | C, new-mechanics, research | Future frost theme only | M3+ |
| Web and bedsheet burn-away | C, kits | **Adopted** **(agreed, Q4)** | M2 |
| Charged Water | B | **Adopted** **(agreed, Q4)** | M2 |
| Remnants (Miasma) | A, B, C, kits | **Adopted** **(agreed, Q5)** | M3 |
| Fog, Haze | B, C, kits | Deferred / Later | — |
| Drift, spread, Outpour, blood slicks | B, C, client | Rejected | — |
| `NoCheapKill` | B | **Adopted** | M2 |
| `hazardHitThisRound`, `hazardPushPerRound` | A, B | Simulator arms only | — |
| Environment kill credit `causedBy` | new-mechanics | **Adopted** | M2 |
| Morale trigger for hazard kills | new-mechanics | Rejected (duplicates T2/T3) | — |
| Phase sockets for boss phase (e) | A, new-mechanics | **Adopted** | M2 |
| One environment damage instance per action | balance critique | **Adopted** | M2 |
| Universal monster light Smash | balance critique | **Adopted** | M2 |
| Floor-1 teaching room, typed Low pieces | fun critique | **Adopted** | M2 |
| Contextual verb chip | fun critique | **Adopted** | M2 |
| Loot in crates | research | Rejected | — |

### 9.2 Adopted in full

The rules are in section 6. The ones not stated there:

- **`causedBy` (M2; TR-24).**
  - Every environment damage event (collision, contact, harm, Keg,
    ignition, Charged Water) records the unit whose action began the
    resolution.
  - "On a kill" features (Shade No Witnesses, Shade 8.1) fire for that
    unit only when the killing damage resolves inside that same action;
    never from ticks or later turns.
  - A monster-triggered Keg that kills a monster credits no party unit.
  - The kill log and the simulator's death-cause rows use it.
- **One environment damage instance per unit per action (M2; TR-13).**
  Replaces the drafts' scattered per-mechanic caps. Deterministic;
  previewed; the top-up rule, "action", voluntary movement and
  non-damage effects as in 6.4.
- **Universal monster light Smash (M2; TR-15).** Scored by path gain,
  one step.
- **Floor-1 typed Low pieces (M2; TR-29).** Fire-typed collision on a
  few Low pieces, not hazards.

---

## 10. Numbers (illustrative; toy, not simulator)

Scripts and outputs:
`/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/s3/`.

**Toy limits (told plainly):**
- party-of-4 fights are won 100% of the time;
- the AI is a simplified one-step scorer;
- the Shade has no Hide;
- melee monsters stop at the first adjacent hero.

So read deltas between arms, not absolute levels, and treat knock-in
rates as lower bounds.

| # | Finding | Fix adopted |
|---|---|---|
| TN1 | A's line-crossing test grants cover on 1–10% of ranged attacks (2–7% typical); the half-plane dot-product test gives 11–37% | Dot-product test (CV2), narrowed after the check to within 45 degrees of the line of fire; the re-run is owed and will read lower **(told plainly)** |
| TN2 | Disadvantage cover raises the floor-1 solo Arcanist's down rate from 7.2% to 13.4%; −2 and +2 AC are neutral (7.0–8.0%) | −2 default; disadvantage only an arm. **Owed:** the solo table re-run with the adopted arm (dot-product, −2, ignore clause), never run so far; gate `Floor1ClassSpread` |
| TN3 | Symmetric cover raises enemy marksman damage by 5–15% in open and mixed rooms | Told plainly; CV8 flank rule; no covered starts on floor 1 |
| TN4 | +2 AC (and equally −2) fails the armour 11 sustained row (25% < 35%); +2 AC stacked with gear fails the 10% row (6.25%) | Roll form with the ignore and suppress clauses; new positional row (TR-4) |
| TN5 | Every arm keeps band share within ±25% (1.00–1.12) and launcher ÷ greatsword ≥ 1.01; rounds +0.6% to +4.2% | `FightRoundsByTheme`; −2 is the cheapest arm |
| TN6 | Knock-into is safe (≤ 1.6% of hero downs even under stress; 0 with `NoCheapKill`) but too rare to feel: environment kills ≤ 0.9% of enemy kills at A's densities (target band 3–8%) | KP1 placement; hover alignment; Move Shunt and Kick; a success still collides. Damage not raised |
| TN7 | `collisionRate`: Ram at level 5+ in cluttered rooms reaches 450–496‰ against `HewCeiling`'s 400‰ | KP3 (≤ 30% cluttered; weighted ~390‰) as a rule; `HewCeiling` re-run counting contact; contact band = collision + 1 step (3/4/4), not 4/6/8 |
| TN8 | Model B fire spread burns a median of 6–12 rounds (max 17); 95–100% of rooms break its 4-round bound; its cloud text is wrong (5 cells, not 13) | No spread anywhere; Remnants only (pass every bound) |
| TN9 | Model C drift reaches 3 (5 with displacement); validator distance would need ≥ 6 | Drift rejected |
| TN10 | Keg chains: 4–23% chance with ≤ 2–3 per room; 0 with spacing ≥ 2 | Spacing ≥ 2 **and** sturdy Kegs in M2 **and** no burst triggers a burst (movable Kegs would undo spacing) |
| TN11 | Combo frequency under B/C would be routine (30–58% of fire casts touch a combo square) | Closed 5-row table; only K2 and K3 new in M2 |
| TN12 | Charged Water: mean 0.27 extra units, P(≥ 1) 23%, max 3; the cap of 6 never binds | Adopted (Q4), halved after weakness |
| TN13 | Clock: packages add +0.5 to +3.5 min per floor, on floors already 3–9 min over the gate. Model A ~+0.5 to +1.2; the adopted package is estimated at +0.8 to +1.6 | Combat-only verbs; hover-only or payoff chips; move shields only in the move tool; gates: net `FightRoundsByTheme` ≤ +1% rounds for the M2 package (Kegs and contact shorten fights, cover lengthens them) **and** `FloorMinutesDelta` ≤ +1.6 min per floor, the top of the told-plainly forecast, measured on the companions 7.4 time model with the verbs and reads as terms; a miss fires the risk-7 cut order **(told plainly; the minutes gate is a lead's call after the check)**. Segment 4's `HazardMinutesDelta` ≤ +0.3 min per floor sits **inside** this +1.6, and `FloorMinutesDelta` read with segment 4 on is the combined gate (change HZ-27, told plainly; hazards design 10) |
| TN14 | DEP-A1 (party-entry reading): Gut 0.38 and Heart 0.36 today; the Supper 0. With features counted, the Supper reaches 0.41–0.52 and a Heart with RT-14 refused 0.39–0.51 | TR-21 "interactive feature"; KP2 in every F2–5 fight layer at path distance 3–6 from party entry; the report uses that reading until the Arcanist owner rules which "start" is meant (11.3) |
| TN15 | 49 terrain condition sources checked: 0 new ids; 1 violation (B's overturned Supper brazier spilling a fire square) | Hot pieces never tip; no square creation by pieces |
| TN16 | A ranged scorer ended 0.4% of turns on harm squares | Destination value in the scorer (6.9) |
| TN17 | Flood fill costs ~0.15 ms; the Clearway refuses 6–11% of Shunts, disconnection ≤ 0.4% | Keep both; the width-2 fill is replaced after the check by the reachability and initial-state lengthening rules (6.3.6), at the same cost |

The toy did not model the Upheaval and Squall correction (6.5.2):
moving them from the collision band to the contact band raises their
contact damage by 1 per affected unit, inside the area cap of 2. The
`HewCeiling` and at-will rider re-runs (11.3) include them **(new lead's
call)**.

---

## 11. Data, validation and simulator

### 11.1 Records and configuration (JSON content; C# rules)

```
PieceKind      { id, class: tall|low|furnishing|keg|hazardPiece,
                 weight?: light|heavy, sturdy: bool, size: small|medium, heft,  // size and heft follow weight
                 round?, tippable?, breakers[], fragileToCollision: bool, debris: none|rough,
                 crossable[], givesCover, noise: {shunt, kick, smash},
                 contact?: {type, band: "contact", save: "hazardEdge"}, typedCollision?: type,   // F1 typed Low pieces
                 keg?: {radius:1, save:"reflexHalf", damage:{type, band:"keg"}, unlitEffect?, condition?},
                 // reflexHalf: the D-22 halving (death and healing design 20.3 step 2), the same rule as
                 // ability onSuccess "half"; Keg 4/6/8 -> 2/3/4 on a success (agreed 2026-11-28, roadmap D-22;
                 // M1-11 rulings 1.1, 1.2)
                 rulesKey,                                   // derived: matched by Socket.rulesKey (6.2.5)
                 glyph, kitHeight }
HazardFace     { hostSide: N|E|S|W, type, band: "contact", save: "hazardEdge" }
SquareClass   += Rough {moveCost:2}, Harm {onEnter:{band:"harm", type}, hazard:true, noTeetering:true},
                 Plank {check:{skill:"acrobatics", dc:"ladder"}}
ThemeRecord.terrainKit { tall[], low[], furnishings[], kegs[], hazardPieces[], hazardFaces[],
                 harmSkin, roughSkin, plankSkin, signatureLaw, usesFurnishingsKits[] }   // replaces RS 8.0 coverProps[]
                 // beside it, segment 4's hazardKit lists the theme's Tableaux (HZ-8; hazards design 6.2.2, 11.1)
TerrainLayer, Socket, PhaseSocket                      // 6.2.5
FurnishingState { furnishingId, square, state: intact|debris|gone, burstSpent }   // saved; terrainDigest
RemnantState (M3) { id, kind, sourceId, squares[], roundsLeft, createdSeq, causedBy }
Tuning: cover.effect = minus2|disadvantage|off; cover.penalty = 2;
        band.collision = [2,3,3] by the pusher's level band (Vanguard lead's call, illustr.);
        band.contact = [3,4,4], band.harm = [2,3,3] by the level band of the floor's lowest expected level;
        band.keg = [4,6,8] by floor; contactRate = 100‰ (beside collisionRate); shunt.moveCost = 2;
        heft = {light:8, heavy:12}; plank.dc = ladder; terrain.maxSockets = 16; caps.* ;
        arms.hazardPushPerRound = off   (all illustr.)
```

- **Band indexing (lead's call, after the check):** environment bands
  (contact, harm, Keg) are indexed by the floor, a build-time constant,
  so `ContactLethalityBound` and `BurstLethalityBound` are provable in
  V9; the collision band keeps the Vanguard design's indexing.

- `terrain.*` tuning and `cover.*` join the RS 11.1 configuration
  snapshot and are hashed into the reproducibility key (R-SEED-5;
  TR-17).
- Every push source declares `verbSourced: bool` in data, which picks
  the contact band or the typed collision band (6.5.2) **(new lead's
  call)**.
- **Segment 4 (change HZ-28):** a `hazards` child of the `terrain`
  stream draws after the 6.2.5 reservation step with fixed draw counts
  (at GS6, after **all** terrain; change SV-9; seeds and validation
  design 4.4);
  `hazardDigest` sits beside `terrainDigest`; V10 `RoomHazards` runs
  after V9 at build time; its reports, arms and save records are
  segment 4's (hazards design 11).

### 11.2 V9 `RoomTerrain` (change TR-18)

Build time; per template × orientation × layer × floor band × theme,
plus the floor-level capacity rows per Blueprint and rule choice.

**Phases and ids (change SV-20; M1 partial, M2; seeds and validation
design 5.1, 5.2):** the rows below are V9's `s` (build) part; its `g`
part is the GS5 lookup, a `floorRetry` check asserted at AP1 with
`proofRef` V9s. Checks are qualified `V9.CheckName`, so
`V9.TemplateReadability` and `V9.ContactLethalityBound` stay distinct
from V8's and V10's checks of the same name. A shared check has one
owner, and V9 calls it where noted.

| Check | Rule |
|---|---|
| `ClearwayConnected` | Joins every Portal, landing, start zone, `fleeExit`, mouth and anchor, at width ≥ 2 in fight rooms, all-optional-on |
| `ProtectedIntact` (proven) | No piece on `protectedSquares` or `protectedSightLines`. FL owns the check (structural part, `V8g` at AP1); V9s calls it as a proof (change SV-20) |
| `FreeSpaceAfterClutter` | ≥ 6 free squares per unit (FL 6.3) |
| `RoomMixAfterClutter` | Open room keeps a 5×5 core and a 3×6 lane |
| `ClutterBand` | Role band, both ends checked on all-on and all-off |
| `ReservationCapacity` (floor) | Every Blueprint and rule choice has enough fight slots and eligible layers for the reserved `teaching` or `signature`, `plank` and `cluttered` tags, and (M2) segment 4's `clock` and `void` tags on layers its eligibility rule allows, none of them cluttered (change HZ-2; cross-document comparison 2026-11-23, CC-91); Supper bay templates have ≥ 2 layers (6.2.5). KP3, KP8, KP9 and the Plank minimum follow by construction |
| `KitCoversRulesKeys` | Every theme kit offers ≥ 1 PieceKind for each socket `rulesKey` its layers use (6.2.5) |
| `CoverPresence`, `ArtilleryFlank`, `Floor1NoCoveredStarts` | CV8, with the halves, every start socket and the landing test as defined there; initial state only |
| `HideSpotBand` | KP4, by enumeration over the nook-touching optional sockets (6.2.5) |
| `EntryCaps` | RS 8.6, 8.8; KP5 |
| `KegSpacing` | KP6 |
| `HazardNearEnemy`, `DepA1Ring` | KP1, KP2: every F2–5 fight-room layer, on mandatory sockets |
| `ThemeSix`, `HomeTheme` | Keg and Remnant conditions in the 6; condition squares in their home theme and on matching FL region tags. The theme's 6 is owned by `V1.ThemeSix`, which V9 calls (change SV-20) |
| `ContactLethalityBound`, `BurstLethalityBound` | 6.5.4, 6.3.5 |
| `WayRound`, `ChokeLimit` | 6.2.3 |
| `ShortcutToCover` | KP7 |
| `SignatureInteraction` | KP9: every layer tagged `signature` holds its theme's signature interaction |
| `CutawayWithTall`, `TemplateReadability` | 4 rotations, stub states |
| `TippableExcluded` (M3) | `coverRoom` cover, `PartyEscape` and Hide band proven without tippable pieces |
| `PhaseSocketsBoth` | Arena sockets proven on and off |
| `AcrobaticsChoices` (floor) | ≥ 3 with Squeezes; every layer tagged `plank` holds a Plank |
| `TerrainDrawCount` | Fixed draws, reservation draws included (`hzTagMax` dummies from M1, SV-3); no layer above `terrain.maxSockets`; HE-7 re-derivation under the next `attempt/{a}` key with nothing carried over (SV-4). The V19g DrawLedger calls it at GS5's end; this row stays as its citation (change SV-20; seeds and validation design 5.2) |

The runtime is a lookup that cannot fail (FL-15); it is asserted at AP1
as a `floorRetry` check with `proofRef` V9s (change SV-9, SV-20; M1;
seeds and validation design 5.4). `FurnishingNeverSeals`
and Kick stops are **rules**, not assertions. V9 joins RS 11.2's
validator list beside V8.

### 11.3 Reports (M2)

| Report | Gate *(illustr.)* |
|---|---|
| `DamageShareByRangeBand` per grammar × cover arm (off / minus2 / disadvantage) | ±25%; launcher families ≥ 60%; damage-taken ≥ 50%; Precision re-run |
| `WorstCoveredHitChance` | Information, read against the armour 11 rows |
| Net `FightRoundsByTheme`; `FloorMinutesDelta`; `ExploreClicks`, `ExploreSeconds` | ≤ +1% rounds and ≤ +1.6 min per floor for the M2 package, both against the **baseline arm**: the same seeds and layers with `cover.effect` = off, Furnishings static, contact hazards and harm squares as plain obstacles and floor, and Kegs inert; reported as mean rounds per fight and floor-median minutes by theme, on minus off. A miss fires the risk-7 cut order; FL-16. With segment 4 on, `FloorMinutesDelta` is read as the combined terrain-plus-hazards gate, ≤ +1.6 min (change HZ-27; hazards design 10, 11.4) |
| `Floor1ClassSpread` with cover on | Classes 5.6 (10 points); the enemies 6.4 floor-1 guard rows (DH-26) |
| `CoverUseRate`, `HideSpotsPerRoom`, Shade Hide rate | Shade D4 |
| `WreckerCollisionRate`, `contactRate` by theme; `HewCeiling` with contact | Vanguard 10.1; the formula of 6.10 |
| `TeeteringUptimeOnBosses` | ≤ 25%: boss turn starts with Teetering ÷ all boss turn starts |
| `HazardPushDeaths`, `ContactHazardDeaths` | ≤ 10% of hero deaths. `HazardPushDeaths` has one definition, HZ's all-environment one; `ContactHazardDowns` is renamed `ContactHazardDeaths`, threshold meaning kept (change SV-24; M2; seeds and validation design 6.10) |
| `EnvKillShare` (alias `EnvironmentKillShare`; HZ owns) | 3–8% of enemy kills (a "felt" band, not a gate; FELT) (change SV-24; M2; seeds and validation design 6.3, 6.10) |
| `EnvDamageShareByClass` | Vanguards ≤ 50% (fun indicator) |
| `ShuntUseRate`, `FurnishingUseRate` (party, AI) | < 10% of fights is a fun failure even if safety gates pass |
| `BurstSelfDamageShare`, `BurstRound1Share` | ≤ 15%; ≤ 40% |
| `ArcanistComboShare`, DEP-A1 coverage | Arcanist cap; ≥ 40% on F2–5. DEP-A1 is read, until the Arcanist owner rules (TR-21), as an interactive feature at path distance 3–6 from any `partyEntrySquares` square, over non-boss fight rooms on floors 2–5; the report is re-run if the owner rules otherwise |
| `AcrobaticsChoicesPerFloor`, `PlankUseRate` | ≥ 3 |
| `PreviewExactness`; `PreviewHiddenDelta` | 100%, per save branch on the party-knowledge state; the hidden delta is information (6.12). The 100% is a zero-event gate with a structural partner, V18t `PreviewExactness` (previews and the core share one function), both rows BUILD (change SV-25; M2; seeds and validation design 6.6) |
| M3: `RemnantUptime`, `MiasmaIgnitionRate`, condition pressure per theme | Watch |

**Attribution for these reports (lead's call, after the check):** a kill
or a Down is environmental when the damage instance that crossed 0 HP is
environment damage (under the top-up rule, the instance that landed it),
and a hero death is attributed to the cause of its Down. *(Moved below
the table so that the table is one table, change SV-31.)*

### 11.4 Simulator arms

**(new lead's call:** the arms are gathered from 6.1, 6.5.4 and 6.10
into one list; the clutter and AI-aim arms are added**)**
**Qualified ids (change SV-22; M2; seeds and validation design 6.2):**
these arms join the qualified arm catalogue: the baseline is the named
toggle `ROOM.NoTerrain`; cover variants and clutter 0/30/50 are
`Toggles` values; the AI-aim arm is the `AIAim` axis (toy one-step,
lookahead), read as a band.
- **Cover:** `cover.effect` off / minus2 / disadvantage, per layout
  grammar, with the joint range-band arm owed by FL 11.4 arm 2 and
  weapons 21.3.
- **Push caps:** `hazardHitThisRound` and `hazardPushPerRound` on and
  off; off is the rule.
- **Clutter:** the cluttered share at 0%, 30% (the rule) and 50%, for
  `WreckerCollisionRate` and the Shade Hide rate.
- **Boss Teetering:** "bosses steady at turn start" on and off; off is
  the rule, on is the reserve lever (19.2).
- **AI aim:** the toy one-step scorer and a lookahead aimer, so knock-in
  rates are read as a band, not a point (risk 2).
- **Baseline (lead's call, after the check):** the no-terrain arm of
  11.3 (cover off, Furnishings static, contact hazards and harm squares
  as plain obstacles and floor, Kegs inert), against which the rounds
  and minutes gates are read.

---

## 12. Staging

**(lead's call; the Q1–Q5 content agreed)**

| Wave | Content |
|---|---|
| **M1** | Tall and Low pieces as blockers; the obstacle definition, corner rule and lines rule (TR-5); non-free pieces, the open room's 5×5 core and lane, Tall pieces at full wall height with the stub glyph (TR-19, TR-20's stub-glyph rule); the Clearway; the `terrain` stream with its reservation step (`hzTagMax` reserved as dummies, SV-3), layers in greybox, `terrainDigest`; V9 partial (`ClearwayConnected`, `ProtectedIntact`, `FreeSpaceAfterClutter`, `RoomMixAfterClutter`, `ClutterBand`, `ReservationCapacity` for the `cluttered` tag, `WayRound`, `CutawayWithTall`, `TerrainDrawCount`); the Kennel `coverRoom` from Tall trunks and root plates. **No cover rule, nothing movable, no hazards** |
| **M2** | Cover (**Q1**); Furnishings with Shunt, Kick, Smash (**Q2**); Kegs in the Carnival and Gut (**Q2**); contact hazards and harm squares on floors 2–5 (**Q3**); Rough; Plank; floor-1 teaching rooms and typed Low pieces; condition-square placement and entry caps; arena caps and phase sockets; the AI requests; previews, chips, charter extensions; full V9 and reports; K2 burn-away and K3 Charged Water (**Q4**) |
| **M3** | Remnants (**Q5**, after the M2 rider and layer reports, 6.6); Overturn; Haul; Shoot an object; Kegs in the Choir, Archive, Supper and Heart; depth-1 chains; Briar (own question); floor-1 Hot pieces only after floor-1 `HazardPushDeaths` and an RT-20 reopening (applied at Peril tier 2+ only, for the Gaol stove: HZ-25, hazards design 6.19) |
| **M3+ / M4** | Glaze for a frost theme; Fog if needed; powder-keg blast push |
| **Later** | Haze with NM-9; hide in cover; Pavise; carom and Over the Edge; lethal chasms; Hurl; multi-square Furnishings; ability-created surfaces (a separate client question) |

---

## 13. What segment 3 owns, and hooks handed to later segments (change TR-1)

**(lead's call; stands as written)**

| Owner | Owns |
|---|---|
| **Segment 3 (this)** | Piece and square classes and the obstacle definition; terrain layers, sockets, the Clearway, clutter bands, chokes, lanes, nooks; the cover rule; Furnishings, Kegs and their verbs; the knock-into **system** (contact hazards, harm squares: rules, bands, caps, in-room placement grammar); Rough and Plank; in-room placement of existing condition squares and the Choir and Carnival entry caps; the combo table; the Remnant system; arena terrain **rules and caps**; DEP-A1 presence; the `terrain` stream, V9 and the reports |
| **Segment 4 (hazards)** | Per-theme hazard catalogues and floor-wide density built from segment 3's classes, inside its caps; drop hazards, chasms, pits and what a fall does (Voids and the Snag, 6.5.7); gall (RT-13); Teetering counting (RT-14); theme-scale self-acting set pieces (vents that emit Remnants, flooding; Stirs, Cords and the Dream step's content); Hinge and Postern no-hazard squares. The hazard / trap line: a hazard is visible from reveal, acts by a rule and is never found or disarmed; "would detection change anything?" If yes, it is a segment-5 trap (change HZ-1; hazards design 6.1, 13) |
| **Segment 5 (traps, locks, checks)** | Traps (hidden, armed, sprung, one-shot), including the toppling shelf, icicles, chandelier and dumbwaiter; door verbs (FL-8); the DC ladder that Shunt, Smash, Plank and Overturn read; the out-of-combat Plank failure cost |

**Rule of thumb:**
- a static piece or an object anyone can use in a room: segment 3;
- something that acts on its own or sets floor-scale density: segment 4;
- something that is sprung: segment 5.

**Told plainly:** this contests the computed task's split. Approved FL
13 and RS 13 already give drops, gall and Teetering counting to segment 4.
Segment 3 takes the knock-into *system*, not drop hazards.

**Hooks handed off:**

| Segment | Receives from segment 3 | Owns |
|---|---|---|
| 4 Hazards | The piece and square classes, bands, caps, `ThemeSix`, the Remnant record and its environment-only source rule. **Taken up by the hazards design (segment 4, approved 2026-11-21; changes HZ-1 to HZ-30)** (cross-document comparison 2026-11-23, CC-93) | Catalogues and density inside the caps; drops; gall; Teetering counting; vents |
| 5 Traps, locks, checks | Plank and Shunt checks naming the ladder; Overturn strips that never include a Clearway square; **traps and pieces (lead's call, after the check):** a Furnishing may end on a trap's trigger square, never springs a trap and never hides a known trap's marker; trap damage is outside the one-instance cap (6.3.6, 6.4); segment 5 may narrow both. **Narrowed by segment 5 (change TL-14; M2):** pressure slabs (a piece ending on a plate or trapdoor springs it; one act springs at most one trap); the cap exclusion kept (TL-18) (traps and locks design 6.7, 6.10). **Taken up by the traps and locks design (segment 5, approved 2026-11-21; changes TL-n)** (cross-document comparison 2026-11-23, CC-93) | DCs; traps; door verbs; the out-of-combat Plank cost; how traps and pieces interact beyond these defaults |
| 6 Encounter placement | Start sockets with cover data (derived by segment 6 from FL `startZone` squares; change EP-5); `Floor1NoCoveredStarts`; KP5's ambush clause (defined, change EP-8); nooks as lurker sockets; the terrain-using enemy per theme (encounter placement design 6.6.1, 6.8.3). **Taken up by the encounter placement design (segment 6, approved 2026-11-22; changes EP-n)** (cross-document comparison 2026-11-23, CC-93) | Who starts where; ambush templates |
| 7 Loot | Furnishings never hold loot (reaffirmed, change LP-12; inset containers instead). **Pending request (change LP-11; segment 7c asks; M2):** set pieces an Alcove can take as its default occupant when a container would exceed the floor's container cap; if none are offered, the Alcove keeps a container and the cap is logged, not enforced (loot sources design 4.2, 14.2). **Taken up by the loot designs (segments 7a–7c, approved 2026-11-22; changes LE-n, IR-n, LP-n)** (cross-document comparison 2026-11-23, CC-93) | Containers and contents |
| 8 Events | Furnishing kinds by id. **Pending request (change EV-2; segment 8 asks; M2):** wall-mounted (RC1+) Alcove set pieces that can host the floor-1 Curiosity, first in the F1 host order; such a set piece takes the set-piece slot LP-11 prefers, never empties a leaf, never displaces a container 7c's cap needs and never sits on a walkable square; if none are offered, the F1 host falls to a free leaf side room, then none, logged; `F1CuriosityPresence` is reported to FL and segment 3 before F1 pool sizes freeze (events design 4.2, 14.2). **Taken up by the events design (segment 8, approved 2026-11-22; changes EV-n)** (cross-document comparison 2026-11-23, CC-93) | Events that name a Furnishing kind; Curiosity content and placement |
| 9 Boss rooms | 6.11's caps, phase sockets and `PhaseSocketsBoth`. **Taken up by the boss rooms design (segment 9, approved 2026-11-22; changes BR-n)** (cross-document comparison 2026-11-23, CC-93) | Interiors, counterplay identity, each boss's one authored terrain move (arena templates and Guises, change BR-1; boss rooms design 4.1, 5) |
| 10 Exploration and information | The previews and chips of 6.12; the "Needs a tall piece to hide" reason; glyphs. **Answered by the exploration and information design (segment 10, approved 2026-11-23; its 5, 9.2):** every 6.12 surface placed in its catalogue; `PreviewExactness` extended to the noise preview and Detour quote; the `CanSee` corner reading (XI-2; 6.2.1) | Overlays, chips and UI |
| 11–12 Seeds and validation | The `terrain` stream and its draw counts, `terrainDigest`, V9 enumeration, `PreviewExactness`. **Answered by the seeds and validation design (segments 11–12, approved 2026-11-23; its 4.3, 5.2, 11, SV-3):** `terrain` registered under `attempt/{a}` as a `leaf-formula` row with `hzTagMax` reserved (6.2.5); `terrainDigest` as a GS5 stage digest; V9 phased in the consolidated suite (11.2); `PreviewExactness` with its V18t partner (11.3). **Pending request (BR and ROOM):** the stamp before terrain and the arena-node dummies (6.2.5) | The stream tree (its 4.3), release seed set (its 4.11), retry logging (`GenerationRetried`, `StageRetried`, `GenerationFallback`, `GenerationDefect`; its 5.7) |

---

## 14. Binding requirements and how they are met

| Requirement | Source | How it is met |
|---|---|---|
| Cover decision in M2 with the range-band arm | weapons 9.5, 26; FL 13 | 6.1 (**agreed, Q1**); arms off / minus2 / disadvantage (11.4) |
| O2: interiors, terrain squares and density per theme and floor | RS 8.0, 13; FL 13 | Interiors and in-room placement: 6.2 and 7; floor-wide density moves to segment 4 (13, TR-1) |
| O3: `coverProps[]` per theme | RS 8.0, 8.3–8.11 | `terrainKit` (7.1, 7.11; TR-22) |
| O13: open-room Cleave and reach space; corridor and guard-post lines | weapons 26; FL 6.4 | The lane and the 5×5 core in every `RoomMix` open room (6.2.3; `RoomMixAfterClutter`); clear corridors; guard-post band (6.2.4) |
| O15: the Kennel solo-floor ranged risk | FL 7.3; FL 15 risk 11 | The per-grammar cover arm (11.4) read on the Kennel at floor-1 solo; `Floor1NoCoveredStarts` |
| No cover rule in M1 | weapons 2 | CV1 (TR-3 rewords 9.5 bullet 1) |
| One flat rules level; coplanar squares; no high ground | main 4.3, 6.8 **(decided)**; LG18 | No stand-on pieces; Planks coplanar; drops are flat voids (segment 4) |
| Symmetric line of sight | main 4.3 **(decided)** | Cover is roll maths; Furnishings never block sight in M2; M3 Overturn changes sight symmetrically |
| No corner cutting | the M0 skeleton plan's `CornerCut` (FL 3.1) | Every movement-blocking square blocks diagonals, including forced moves and a piece's own travel (TR-5) |
| No spreading, evolving or player-placed surfaces | main 2 **(decided)**; conditions 11.1, ability 8.4 **(agreed)** | Nothing timed in M2, except one gated signature Stir per F2–5 floor that changes no square (HZ-12; M2 (gated), else M3; 6.6); Remnants only from M3 (**agreed, Q5**; TR-27 flagged); Kegs leave nothing on the ground |
| Condition budget 32; theme 6 | conditions 16, 18.1 | 0 new ids; damage-only pieces; V9 `ThemeSix` |
| No single failed save lethal to the hero | conditions 10.4 | Contact ≤ 20% of hero HP; Keg ≤ 25%; failures on Planks stop, never fall |
| Lethal tags; ally confirmation; party floors | DH 17.6, 17.7, 10.4 | Extended (TR-23) |
| Never block protected squares or sight lines; runtime cannot fail | FL 6.1, 11.2; FL-15 | Clearway; V9; legality is a rule |
| Free space, `RoomMix`, LG20, `PartyEscape`, LG12, `NoSealedPocket`, `CutawayPockets` | FL 6.3–6.8 | V9 after clutter; Clearway ≥ 2 wide |
| 2–3 terrain layers per template | FL 8.2 | 6.2.5 |
| `coverRoom` cover | enemies 10.4; FL 6.7 | M1 Tall pieces; M2 cover |
| DEP-A1 ~40% | Arcanist 16, 19; RS 10.7 | TR-21; KP2; TN14 |
| ≥ 3 Acrobatics choices per floor | armour 7.3 | 2 Squeezes + ≥ 1 Plank per floor |
| Wrecker walls and edges; density report | Vanguard 8.3, 18 | Contact hazards (KP1); `WreckerCollisionRate`; `HewCeiling` re-run |
| Hazard edges and crossable hazards from floor 2 | trinkets 23 | Harm and condition squares from F2; F1 dry |
| Arena sight blockers and counterplay rules | enemies 17.5 | 6.11 |
| Lurker hide spots | enemies 25; RS 8.3, 8.7 | Nooks; Tall pieces; KP4 |
| Entry caps | RS 8.6, 8.8 | V9 `EntryCaps`; KP5 |
| Supper table layouts differ by bay | FL 7.8 | 2–3 table layers per bay |
| Hide needs a line-of-sight break | stat model 7.1 **(agreed)** | Unchanged; Tall pieces are Hide spots |
| Readability charter | RS 7.4 **(agreed)** | Extended (TR-20); gates run with pieces |
| Clock | FL-16; companions 7.4 **(agreed)** | Combat-only verbs; chips; the net rounds gate |
| Determinism; fixed draws | main 6.3 **(decided)**; R-SEED-3, 5 | `terrain` stream in the FL-2 pattern; integer rules |
| Exits never sealed | enemies 17.5 | `FurnishingNeverSeals`; Remnants clipped off flee paths |

---

## 15. Risks

| # | Risk | Signal | Response |
|---|---|---|---|
| 1 | Cover is a guess until the arm runs: −2 may be too weak in open Kennel glades and Carnival plazas, and cover can lengthen marksman fights | `DamageShareByRangeBand`; net `FightRoundsByTheme` | The cover arms; CV8 flank rule; the net rounds gate |
| 2 | Knock-into may still be unfelt; the toy AI under-aims, so humans will exceed its rates | `EnvKillShare` (alias `EnvironmentKillShare`, SV-24); `ShuntUseRate` | KP1; Shunt and Kick costs; collision on a success; the alignment highlight; the AI-aim arm |
| 3 | The Wrecker over-performs in dense rooms | `WreckerCollisionRate`; `HewCeiling` with contact; `TeeteringUptimeOnBosses` | KP3; contact = collision + 1 step; the "bosses steady" reserve lever |
| 4 | Arcanist push payoff: Gust now deals the typed collision band against contact hazards; Upheaval and Squall the contact band | The at-will rider bound; `ArcanistComboShare` | Counted in the rider bound; area cap of 2 |
| 5 | AI uses objects worse than players | `FurnishingUseRate` (AI) | Accepted as player power; universal light Smash; one terrain-using enemy per theme |
| 6 | Readability load: cover pips, rims, Keg rings, harm squares and stub glyphs stack up | Greyscale and role-ID gates | One glyph per class; RC6+ order; a failing theme is held |
| 7 | Clock: every package spends time on floors already over the gate | Net `FightRoundsByTheme`; `FloorMinutesDelta`; `ExploreSeconds` | Combat-only verbs; chips only on payoff. The cut order fires automatically, one step per failed run of either gate: Kick, Move Shunt, Rough density, harm squares. **Extended by segment 4 (change HZ-27):** then Moods, Dare rooms, second Stir kinds, Lurch and Prone Stirs, Tides, Void density 40% → 30%, and the signature Stir reverting to static density; segment 4's steps fire first when `HazardMinutesDelta` or the combined gate misses, these steps first when the terrain package misses with segment 4 off (hazards design 10) |
| 8 | RT-14 refusal costs the Heart's bedsheets and each full-6 theme's named fallback drop | The RT-14 ruling | Floor 5 keeps contact hazards, Furnishings, Planks and harm squares; harm squares no longer depend on it (6.5.5) |
| 9 | Approved-text footprint: several agreed texts and one decided text (M3) change | — | Each is flagged (17); the M2 package touches main 2 only by consolidating and clarifying (TR-8, TR-9) |
| 10 | Art volume: about 12–14 rules pieces per theme on top of 41–50 templates | M2 schedule review | The art team's stated appetite is the enabling assumption |
| 11 | Clutter against screen fit in the Archive, Supper and Heart | `TemplateReadability` at 4 rotations | Template rework; the clutter bands |
| 12 | Party-triggered Remnants read as player-made ground effects | M3 playtest notes; `RemnantUptime` | Environment-only sources; ≤ 1 per room; 2-round countdown; told plainly (Q5) |
| 13 | **All evidence is toy** | — | Every number is owed a simulator rerun before it settles |
| 14 | **Research partly from recall** (Lancer, Kill Team, Wildermyth, Pathfinder 2E details) | — | Marked *(recalled)*; none is load-bearing |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the cover penalty within the lever, bands,
  heft, Shunt movement cost, Kick distance, clutter bands, per-room
  caps, Plank DC and lengths, Remnant countdown and opacity, report
  thresholds other than the stated gates.
- The terrain layers and sockets themselves, within 6.2 and V9.
- Skins inside each theme kit, within the socket's rules key (a skin
  never changes a rule, 6.2.5) and the build table of 7.11.
- Godot scene structure, tweening and culling for pieces (8.2), with no
  physics bodies.
- Art, dressing and VFX inside the charter.
- Names, until the naming pass.
- **Not tuning:** one cover tier, directional, never from walls or
  units, never on Portal squares, ignored, suppressed and replaced as in
  CV4 **(agreed, Q1)**; walls never break; no carrying or throwing; Kegs
  sturdy with no chains in M2 **(agreed, Q2)**; always a save, flat
  previewed damage, no Teetering from damage-only hazards **(agreed,
  Q3)**; the closed combo table **(agreed, Q4)**; Remnants
  environment-only, fixed and non-spreading **(agreed, Q5)**; the
  Clearway ≥ 2 wide and `FurnishingNeverSeals` as rules; one environment
  damage instance per unit per action; `NoCheapKill`; no rules pieces
  from the display stream; build-time proof.

---

## 17. Changes to approved documents

Applied on 2026-11-21, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and rows owed by
other owners are listed again in 18 and recorded as such. Every row
names the exact section it touches. Rows that touch **decided** or
**agreed** text are flagged in the "Flag" column; **the client's
approval of 2026-11-21 covers them** (with Q1–Q5 where a row names one;
the answers of 2026-11-21 already agreed each of those rows in
substance). Told-plainly rows were told in 19.1 and 19.3 and confirmed
by the client on 2026-11-21. TR-13, TR-14, TR-15 and TR-21 are requests
to other owners, recorded as requests; they change nothing until their
owner rules. TR-13's one-instance cap and TR-15's `NoCheapKill` are
agreed in substance by Q3, so only their conditions and enemies wording
waits on those owners. Conditional rows are recorded now and apply only
when their condition is met: the Supper Poisoned rows that TR-14 ties
to the RT-14 ruling, including every M3 row that uses Poisoned in the
Supper (6.5.5). M3 items are recorded as M3
items and apply at M3: TR-27 (Remnants and the main 2 exception), TR-28,
and TR-6's Overturn and Haul.

**Narrowings recorded inside this design (no approved text changes):**
- Brink reads harm squares but not contact hazards (read literally);
- the floor-1 stove and candle rack are Low pieces, not hazards, so
  floor 1 stays dry (RT-20);
- the Kennel `coverRoom`'s M1 cover is line-of-sight breaks only;
- RC3 stands unchanged (RC5 gains RC5+, TR-20).

| # | Document / section (current status) | Change | Flag | Approved by |
|---|---|---|---|---|
| **TR-1** | FL 13 hand-off rows for segments 3 and 4; FL 17 row FL-22 (its RS 13 ownership amendment, whose pattern this row follows); RS 13 rows "Room shapes and terrain" and "Hazards" (**agreed** hook text) | Ownership line: segment 3 owns the room-terrain system and the knock-into system; segment 4 owns floor-scale density, drops, gall, Teetering counting and vents; segment 5 owns traps (13) | **Agreed** (RS hooks, filled) | Client, with this document |
| **TR-2** | Weapons 9.5 bullets 1–2 (M2 reading; lead's call); weapons 21.3 arms; enemies 17.5 arena bullet "pillars or cover" (**settled**; gains a rules meaning); armour 13 Pavise row (Later, designable) | Directional one-tier cover (the dot-product facing test within 45 degrees), −2 to the roll with the ignore (net disadvantage), suppress and replace (the larger of cover and the reaction bonus) clauses; weapons 7.5 gains step 2b and ability model 14.3 the matching step; no cover on Portal squares; walls, units and Grilles never give cover (6.1) | **Agreed** (Q1); the 45-degree bound, step 2b and the window rule told plainly after the check | **Client (agreed 2026-11-21, Q1)** |
| TR-3 | Weapons 9.5 bullet 1 ("Obstacles block line of sight only"; lead's call) | M1 wording: "Tall pieces block sight and movement; Low pieces block movement only; nothing gives cover; units never give cover" | — | Lead; weapons owner applies |
| **TR-4** | Armour 11 item 1 (**lead's call, confirmed to stand**); armour 8.1 rule 6 (charged exclusion) | Positional envelope row: "sustained maximum AC + cover leaves ≥ 25%"; the charged exclusion extends to cover (CV5) | **Confirmed to stand** (armour 11) | **Client (agreed 2026-11-21, Q1)**; armour owner applies |
| TR-5 | Conditions 10.1 bullet 2 ("It stops at walls, obstacles and units"); the M0 skeleton plan's `CornerCut` rule (walls only; cited in FL 3.1); ability 8.1 line rule; weapons 7.6 Skewer. Main 4.3 (**decided**) is used, not changed, for 8 directions and diagonal cost 1 | Obstacle defined; corner cutting blocked by every movement-blocking square, including forced moves and a piece's own travel; a Tall piece is a wall for lines, Skewer, reach and area origin; Low pieces never stop lines (6.2.1) | — | Lead; conditions, ability and weapons owners apply |
| TR-6 | Ability 7.2 universal actions table (new rows); stat model 7.1 Athletics row | Shunt (two modes), Kick, Smash (M2); Overturn, Haul (M3) (6.3) | — | **Client (agreed 2026-11-21, Q2)**; ability owner applies |
| **TR-7** | Vanguard 8.3 rejected list "breakable terrain (rejected: the fixed-terrain non-goal)"; Vanguard 9 table "Rejected" row "destructible terrain" (resting on **decided** main 2); RS 7.6 "breakable walls" (**agreed**; kept refused) | Breakable and movable Furnishings and Kegs; walls never break (6.7) | **Decided** (main 2 reading); **Agreed** (RS 7.6, kept); told plainly | **Client (agreed 2026-11-21, Q2)** |
| **TR-8** | Conditions 11.1 bullet 3 ("Squares never spread or change, with one exception (11.4)") (**agreed**); 11.4 "The single exception" (agreed that it exists); conditions 2 Non-goals bullet ("never spread or change, with the single exception of oil burning away") (**agreed**); stat model 2 Non-goals bullet ("except that fire burns an oil square to plain floor") (**agreed**); the Arcanist design "Never: ground zones (the one terrain change is the settled fire on oil)" | One consolidated list of sanctioned map changes, with waves: fire on oil (existing); Furnishing debris and removal (M2); boss phase sockets (M2); web and bedsheet burn-away (M2); Overturn debris and sight change (M3); Remnants (M3) | **Agreed** | **Client (agreed 2026-11-21, Q2, Q4, Q5)**; conditions and Arcanist owners apply |
| **TR-9** | Conditions 11.4 (sibling rule), 11.6; ability 8.4 (**agreed**; clarification); consumables 12 "never terrain" (clarification); enemies 6.4 guards; enemies 17.2 Volatile (precedent) | Kegs: a one-shot area, source `environment`, nothing on the ground; sturdy in M2; no burst triggers a burst in M2; counted in the guards every round (6.3.5) | **Agreed** (clarified, not changed) | **Client (agreed 2026-11-21, Q2)** |
| **TR-10** | Conditions 10.1 bullet 2 ("Stopping before a hazard is not stopping early and deals no damage"); 10.2; 11.3 hazard list; 11.5 M2 package (**agreed**); weapons 7.2 rule 4 (a second named exception class beside Battering Force); conditions 18.4 Teetering sources (unchanged; Brink read literally) | Obstacle-hazard branch: contact hazards are hazards; a failed save deals the contact band (a non-verb part, including Upheaval and Squall) or the typed collision band (verb-only) instead of collision; success is an ordinary obstacle stop; a successful harm-square save is an ordinary obstacle stop too (10.1's "deals no damage" no longer applies to harm squares), and a failed one lets the push continue; damage-only hazards never give Teetering; area cap of 2 saves per action by starting square; the save's difficulty (the pusher's ability difficulty, else 8 + Athletics), its name (a hazard-edge save) and its timing (weapons 7.5 step 10), offered for every 10.2 save; Stalwart order (6.5.2) | **Agreed** (M2 package extended) | **Client (agreed 2026-11-21, Q3)**; conditions and weapons owners apply |
| **TR-11** | Conditions 11.2 table and schema; **11.5 M2 set (agreed)**; RS 6.3 home-theme rule (lead's call); conditions 18.4 Prone crawl | Condition-free square classes (harm, Rough, Plank); harm squares join the M2 set; the home rule reads condition-free classes as theme-neutral; movement cost modifiers never stack above 2 | **Agreed** (conditions 11.5) | **Client (agreed 2026-11-21, Q3)** |
| TR-12 | Ability 7.2 (new row); stat model 7.1 Acrobatics row; armour 7.3 binding paragraph | Plank narrow ground (6.2.6) | — (a binding requirement met) | Lead; ability owner applies |
| TR-13 | Conditions 9.2, 9.3, 11.6 | Ruling requests: environment damage is rider-free; the damage-end row and setup payouts apply (payouts are not environment instances); one environment damage instance per unit per action by the top-up rule, with "action" as one use, voluntary harm entries, turn-end effects, Remnants and traps outside it, and non-damage effects as in 6.4; Charged Water's base before the origin's weakness, each recipient's weakness then halving (6.8) | — (request; the cap agreed in substance, Q3) | Conditions owner |
| TR-14 | Conditions 16; RS 8.0 fallbacks; RS 14 | RT-14 ruled "does not count" before M2 rosters freeze; harm squares and Supper Poisoned rows conditional on it | — (request) | Conditions owner, before M2 rosters |
| TR-15 | Enemies 5.5, 11.1, 11.3, 14.5, 17.3; conditions 17.3 | AI requests: `NoCheapKill` as the exact scorer term of 6.9 (including triggered environment damage); destination value; `usesFurnishings`; universal light Smash by path gain; the Holder includes contact hazards; Keg triggers count as attacks; tie-break; routers may cross non-lethal Remnants; one visible terrain-using enemy and one gas-aura monster per relevant theme (6.9, 6.6) | — (requests) | Enemies owner |
| TR-16 | Enemies 17.5 (**settled**) phase (e) "change arena terrain within the theme's 6"; RS 13 Boss rooms row (unchanged) | Arena caps; phase (e) narrowed to pre-placed phase sockets, existing kinds only, announced a round ahead and proven in both states; counterplay identity unchanged (6.11) | — (told plainly: narrows settled phase (e)) | Lead; segment 9 inherits |
| **TR-17** | FL 6.1 Streams and "After layout" step 1; FL 6.6; FL 8.2; FL 11.1 save; FL 11.2 `ProtectedIntact`; main 6.3 "Separate random streams" (**decided**, extended in the FL-2 pattern); RS 11.1 configuration snapshot; RS 6.4 R-SEED-5 | The `terrain` stream; layers; the Clearway (≥ 2 wide in fight rooms); `terrainDigest`; Furnishing save state; `terrain.*` and `cover.*` in the snapshot; static matrices stay wall- and Portal-only (6.2) | **Decided** (main 6.3; an addition consistent with its text) | Client, with this document |
| TR-18 | FL 11.2; RS 11.2 validator list | V9 `RoomTerrain` (11.2) | — | Lead |
| TR-19 | FL 6.3; FL 6.4 `RoomMix`; FL 8.1 | Pieces are non-free; the open room keeps a 5×5 core and a lane; Tall pieces at full wall height with a stub glyph (M1) | — | Lead |
| **TR-20** | RS 7.4 RC1, RC2, RC4, RC5, RC6, RC7 (**agreed**, RS Q3 charter); the RS 8.8 fire-hue line; RT-17 gates | Charter extensions RC1+ (including the M3 Remnant exception and the VFX residue rule), RC2 bans, RC4+ (with the Miasma hue at M3), RC5+ (the composable glyph table and hazard-face floor decals), RC6+ (Keg footprints at hazard tier unless about to burst), RC6's scale clause reading rules pieces as architecture, RC7 gains the fallen bell's struck-bronze gameplay cue; the kit silhouette and liquid rules; gates run with pieces in frame, with "Keg or Furnishing" in the role-ID gate (8.1, 8.2); the stub-glyph rule applies from M1 | **Agreed** (RS Q3 charter) | Client, with this document (Q5 for the Remnant exception) |
| TR-21 | Arcanist 16, 19 (DEP-A1); RS 10.7 (the floor-4 miss likely closed) | DEP-A1 counts interactive features (condition squares, contact hazards, harm squares, Kegs); the 3–6 ring rule; which "start" is meant | — (request; told plainly that E3 combos still need squares) | Arcanist owner |
| **TR-22** | RS 8.0 `coverProps[]`; RS 8.3, 8.4, 8.5, 8.8, 8.9 catalogue text (**agreed**, RS Q4), including RS 8.9's "Deliberately dry (no squares)" and RS 8.4's `signatureFeature` and `signatureFamily` ("cover gates"); RS 6.3's "the Gaol, Kennel and Supper are deliberately dry" (lead's call); RS 8.x terrain Law lines | `coverProps[]` → `terrainKit`; Gaol bars off (foot stocks, desks, benches); Wake coffins Low cover only; Supper tables split and braziers as Hot pieces; Carnival cages solid; "dry" narrowed to "no condition squares", so condition-free harm, Rough and Plank squares appear in the Gaol, Kennel and Supper; the Kennel's feature becomes "fallen logs (cover) and kennel gates (Portals)", family "cover logs"; one terrain Law line per theme (7) | **Agreed** (RS Q4 catalogue); told plainly | Client, with this document |
| TR-23 | DH 17.6, 17.7; ARC-25; ability 8.3 | Lethal tags, ally confirmations and previews extended to contact hazards, harm squares, Kegs, Kick lines and Overturn strips (6.12) | — | Lead; DH owner applies |
| TR-24 | Conditions 9.3 damage events; Shade 8.1 (classes owner check) | `causedBy` kill credit (9.2) | — | Lead; conditions and Shade owners apply |
| **TR-25** | Conditions 11.1, 11.2 binding bullet ("never the square"), 11.4 (**agreed**); conditions 2 and stat model 2 Non-goals bullets (**agreed**; listed in TR-8's consolidated exceptions) | Web and bedsheet burn-away, K2 (6.8) | **Agreed** | **Client (agreed 2026-11-21, Q4)** |
| **TR-26** | Conditions interaction table; 11.2 water row (**agreed**); conditions 18.6 Soaked arc (order with Charged Water); ability 8.2 friendly fire (read, not changed: Charged Water follows it); **stat model 2 Non-goals bullet "Spreading or evolving terrain surfaces, such as burning oil that spreads or conducting water. Held back until riders and layers are proven." (agreed)**; RS 8.6 Choir guards | Charged Water, K3 (6.8), in M2, before the stat-model gate, because it is instantaneous and makes no square | **Agreed** (Q4); told plainly | **Client (agreed 2026-11-21, Q4)** |
| **TR-27** | **Main 2 Non-goals bullet "Spreading, evolving or player-placed ground surfaces …" (decided)**; stat model 2 (agreed); ability model 2 (settled) and 8.4 (agreed); conditions 2, 11.1 (agreed), 11.4 "free of timers" (lead's call); conditions 5.4 (turn order of condition steps: the new round-end environment step); conditions 11.6 and 5.7 (a party-triggered Remnant counts as party-sourced for the per-fight cap, not hostile to every unit); RS 7.4 RC1 (agreed); RS 7.6 (agreed); Shade 9 table (smoke rejection untouched: no Haze) | M3 Remnants (Miasma), environment-only, fixed footprint, countdown, ignite once; a new round-end environment step. **Main 2 gains:** "Exception (M3): environment-made Remnant clouds with a fixed footprint and a short countdown, which never drift, spread or grow (room terrain design 6.6)" **(new lead's call:** the wording**)** | **Decided** (main 2); **Agreed** (stat model 2, ability 8.4, conditions 11.1, RC1, RS 7.6); told plainly | **Client (agreed 2026-11-21, Q5)**; applies at M3 |
| TR-28 | Ability 8.x targeting; conditions 18.7 (attacking ends Hidden); weapons 9.3 (unchanged: no improvised throw) | Shoot an object (6.3.7) | — | Lead; M3 |
| TR-29 | RS 8.2 and RT-20 reading; trinkets 23 | Floor-1 typed Low pieces, not hazards (6.5.6) | — (told plainly) | Lead; trinkets owner told |
| TR-30 | Vanguard 6.4 rule 8 `HewCeiling` (lead's call); Vanguard 10.1 pass table; the tuning file beside `collisionRate` | The contact term of 6.10: per-use damage counts `contactRate` and P(save); the per-unit clause reads the collision band only, the contact surplus bounded by `ContactLethalityBound` and the top-up cap; `contactRate` joins the tuning file | — (told plainly) | Lead; Vanguard owner applies |
| TR-31 | RS 12 M1 row ("No terrain", read to admit Old Ragged's root-columns; lead's call) | M1 terrain is Tall and Low pieces as blockers only, per 12 M1 (no cover rule, nothing movable, no hazards); Old Ragged's root-columns stay template walls, not terrain pieces (6.11). Added 2026-11-23 (cross-document comparison 2026-11-23, CC-86) | — (a reconciliation of 12 with RS 12) | Lead; RS owner applies |

Waves: TR-1, TR-3, TR-5, TR-17 (stream, reservation step, layers,
Clearway), TR-18 (partial), TR-19, TR-20's stub-glyph rule and TR-31 in M1;
TR-2, TR-4, TR-6 to TR-16, TR-17 (save state), TR-18 (full), TR-20 (the
rest) to TR-26, TR-29 and TR-30 in M2; TR-27 and TR-28 in M3, with
TR-6's Overturn and Haul.

---

## 18. Depends on systems not yet designed

The owed rows are the single list in 17; by owner:

- **The conditions owner:** TR-5 (10.1), TR-8 (11.1, 11.4), TR-9, TR-10
  (10.1, 10.2, 11.3), TR-11 (11.2, 11.5, 18.4), the TR-13 ruling
  requests, the TR-14 RT-14 ruling before M2 rosters freeze, TR-24
  (9.3), TR-25, TR-26 and, at M3, TR-27's round-end environment step
  (5.4) and the 11.6 attribution change.
- **The enemies owner:** the TR-15 requests, the enemies 6.4 guard rows
  of TR-9 and TR-26, TR-16's 17.5 reading.
- **The weapons and armour owners:** TR-2, TR-3 (weapons 9.5), TR-10's
  weapons 7.2 rule 4 exception class, TR-4 (armour 11, 8.1).
- **The ability and stat model owners:** TR-6 and TR-12 (ability 7.2,
  stat model 7.1 rows), TR-5's ability 8.1 line rule, TR-23 (ability
  8.3), TR-28 (M3 targeting).
- **The class owners:** TR-7 (Vanguard 8.3, 9 table), TR-30 (Vanguard
  6.4 rule 8 `HewCeiling`), TR-21 (Arcanist DEP-A1 and which "start"),
  TR-24 (Shade 8.1), TR-8's Arcanist sentence.
- **The DH owner:** TR-23 (17.6, 17.7).
- **The RS owner:** TR-1, TR-20, TR-22, TR-17's snapshot fields, TR-18's
  validator list, TR-31 (RS 12 M1; cross-document comparison 2026-11-23,
  CC-86).
- **The floor layout owner:** TR-17 (FL 6.1, 6.6, 8.2, 11.1, 11.2),
  TR-19.
- **Dungeon generation segments 4–12:** every hook in 13 (segments
  4–9 taken up by their approved designs, 13; segments
  11–12's answered by the seeds and validation design 11, with its
  pending stamp request in 6.2.5; cross-document comparison 2026-11-23,
  CC-93), including
  segment 4's catalogues, drops and vents, segment 5's DC ladder,
  segment 6's start sockets, and segment 9's arena interiors and
  authored terrain moves.
- **The simulator:** the arms of 11.4 and the reports of 11.3, including
  the owed floor-1 solo table re-run with the adopted cover arm.
- **The art team:** the kit and budget of 8.2, the glyphs of 8.1, stub
  states at 4 rotations.
- **Naming pass:** every placeholder name here.

---

## 19. Open items for the client

None. Approved by the client on 2026-11-21.

The client approved this document as a whole on 2026-11-21, including
every lead's call (the lead's calls of the answered proposal, the new
lead's calls in 19.1 and the lead's calls after the check in 19.3), the
told-plainly items (including "Told plainly after the check" in 19.3,
notably the 45-degree bound on the cover test) and the partial
rejections in 19.3, and its approval covers the flagged rows of 17. The
five questions were answered on 2026-11-21 **(agreed)**; the client also
said everything else in the proposal stands as written. The future items
in 19.2, which come back only if a report or a later design triggers
them, and the naming pass remain future items.

### 19.1 This document

**The five answers (agreed, 2026-11-21), each the recommended option:**

- **Q1, cover:** option A. One tier, directional by geometry (the
  dot-product facing test), −2 to ranged, thrown and spell attack rolls
  from 2 or more squares; ignored when the attack already has
  disadvantage, suppressed against charged AC, replaced (not added) by
  reaction AC; no cover on Portal squares; walls and units never give
  cover; melee, reach, saves and areas ignore it; symmetric; Hide still
  needs a broken line of sight; armour 11 item 1 gains the positional
  row; from M2, none in M1 (6.1; TR-2, TR-3, TR-4). Rejected: B,
  disadvantage; C, no cover.
- **Q2, movable objects:** option A. Furnishings (light and heavy per
  theme) with Shunt (movement or attack mode), Kick (round pieces, up to
  4 squares, previewed) and Smash (binary); Kegs in the Carnival
  (lamp-oil keg) and the Gut (swollen sac) in M2, sturdy, and no burst
  triggers a burst; the Clearway ≥ 2 wide and `FurnishingNeverSeals`;
  monsters Smash light pieces; reverses the Vanguard design's breakable
  and destructible terrain rejection for objects only; walls never
  break; carrying and throwing rejected (6.3, 6.7; TR-6 to TR-9).
  Rejected: B, static cover only.
- **Q3, knock-into hazards:** option A. Contact hazards (hazard pieces
  and hazard faces) and harm squares from floor 2 in M2, with the save
  rules (failure: typed contact damage instead of collision; success: an
  ordinary obstacle stop, no Teetering); always previewed; flat damage
  capped at 20% of the lowest legal hero's HP; one environment damage
  instance per unit per action; `NoCheapKill`; harm squares join the
  agreed M2 square set (conditions 11.5) (6.5; TR-10, TR-11). Rejected:
  B, harm squares in M3; C, condition squares only.
- **Q4, combos:** option A. Web and bedsheet burn-away (K2) and Conduct (renamed Charged Water, naming pass NP-19)
  through water (K3) in M2; a closed combo table of 5 rows (6.8; TR-25,
  TR-26). Rejected: B, burn-away only; C, neither.
- **Q5, lingering clouds:** option A. Environment-only Miasma Remnants
  in M3: a fixed footprint ≤ 9 squares, a 2-round countdown, never
  drifting, spreading or growing, ignitable once, ≤ 1 per room, only in
  themes whose 6 holds Poisoned, and only after the M2 rider and layer
  reports. This changes decided main 2 (flagged, TR-27), and a
  party-triggered cloud is player-made ground effect in substance (told
  plainly) (6.6). Rejected: B, never.

**Told plainly; confirmed by the client on 2026-11-21:**
- **the clock:** the adopted terrain package is estimated at +0.8 to
  +1.6 min per floor *(illustr.)*, on floors already over the 20-minute
  gate on the toy; it ships only under a net rounds gate (net
  `FightRoundsByTheme` ≤ +1% for the M2 package) and, after the check, a
  minutes gate at the top of that forecast, with a named cut order that
  fires automatically (risk 7);
- **symmetric cover helps enemy marksmen:** it can raise enemy
  marksman damage by 5–15%, because covered marksmen live longer;
  cover is not a party buff by default (CV7, TN3);
- **−2 to the roll and +2 AC give the same hit chance,** and both fail
  armour 11's sustained row as written; the positional row (TR-4) is
  what makes cover legal, not the form (CV5);
- **the Vanguard reversal:** "breakable terrain" and "destructible
  terrain" were rejected in the Vanguard design; they are reversed for
  objects only (6.7, TR-7);
- **main 2 changes in M3:** the decided non-goal gains an M3 Remnant
  exception, and a party-triggered cloud is player-made ground effect in
  substance (TR-27);
- **directional cover makes flanking matter** without a flanking rule,
  beside the Shade's rejection of flanking (CV6);
- **the Kennel `coverRoom`'s M1 "cover"** is Tall line-of-sight breaks
  only (CV1);
- **Gaol cover is desks, benches and foot stocks,** not cell bars;
  **Wake coffins are fixed Low cover,** not hiding places; **Supper
  tables split** into Tall and Low; **Supper braziers are Hot hazard
  pieces** that thaw Frozen units of either side (7.2, 7.4, 7.8; TR-22);
- **floor 1 still has no hazard edges** (trinkets owner): the stove and
  candle rack are typed Low pieces (6.5.6; TR-29);
- **the computed task's ownership split is contested:** drops, gall and
  Teetering counting stay segment 4's (13);
- **players will out-think monsters with Furnishings;** accepted as
  player power (6.9);
- **E3 Arcanist combos still need condition squares,** which the Supper
  never has (7.8; TR-21);
- **the toy's limits:** read deltas, not levels; knock-in rates are
  lower bounds (10).

**New lead's calls made while writing this document** (beyond the
answered proposal; confirmed by the client on 2026-11-21, with this
document):
1. **Upheaval and Squall are non-verb pushes** and take the contact
   band on a failed save, under the area cap of 2; the proposal had
   listed them as verb-only. The Arcanist design (8.2, 8.3) makes both
   non-verb, and conditions 10.1 names only Gust and Reel as
   verb-sourced spells (6.5.2, 6.10, TR-10). The `HewCeiling` and
   at-will rider re-runs include them (10).
2. **"Action" in the one-environment-damage-instance cap means one use**
   (ability model 9.3): each attack of Extra Attack, each cast,
   reaction, Shunt, Kick or Smash is its own (6.4).
3. **Ties in that cap** go to the first instance in resolution order,
   keeping its damage type (6.4). *Superseded after the check by the
   top-up rule (19.3), under which a later equal instance deals 0 and
   the type stays.*
4. **The cap limits damage only;** non-damage effects of every instance
   (Prone saves, Oiled, Poisoned) still resolve under their own rules
   and the per-fight condition cap (6.4).
5. **Kick end square:** the piece may cross Clearway squares but ends on
   the last legal square of its travel; a unit it hit still takes the
   collision; if no square is legal, Kick cannot be chosen (6.3.3).
6. **An Attack Shunt into a unit is legal** even when the unit's square
   would be an illegal end square, because the piece does not move
   (6.3.2).
7. **The Supper's serving trolley is light and round,** the theme's one
   light Furnishing (7.1, 8.2).
8. **The Remnant gate is read as named M2 reports passing** (the at-will
   rider bound and per-fight condition cap usage; V9 green,
   `PreviewExactness`, `HazardPushDeaths`, `BurstSelfDamageShare`, net
   `FightRoundsByTheme`), recorded in writing by the lead before M3
   Remnant work starts (6.6).
9. **The main 2 wording** for TR-27 (17).
10. **Numbers findings renamed TN1–TN17,** because N1–N14 are the
    Vanguard design's mechanics ids (front matter, 10).
11. **Every push source declares `verbSourced`** in data (11.1).
12. **The simulator arms gathered in 11.4,** with a clutter-share arm
    and an AI-aim arm added.
13. **Document shape:** the proposal's sections are renumbered to the
    floor layout template, adding Intent, Goals, Terms, Left to the
    builders and Depends on; V9 gains the `SignatureInteraction` check
    for KP9 (11.2).

### 19.2 Future items triggered by reports or later designs

These come back to the client only if a report or a later design
triggers them:
- Briar with Bleeding in the Kennel (M3; touches RS 6.3, 8.4, RT-20 and
  conditions 11.2), as its own question;
- floor-1 Hot hazard pieces (M3; reopens RT-20) if floor-1
  `HazardPushDeaths` passes (applied at Peril tier 2+ only, for the
  Gaol stove under the Treadwheel: HZ-25, hazards design 6.19);
- Glaze for a future frost theme;
- Haze with NM-9 Gloom;
- hide in cover;
- the "bosses steady at turn start" Teetering lever, if
  `TeeteringUptimeOnBosses` fails;
- Fog, if the Choir or Supper reports need it;
- the powder-keg blast push;
- Hurl;
- the RT-14 ruling refused (each full-6 theme's named fallback drop,
  the Heart's bedsheets and the Supper Poisoned rows go; harm squares
  stay; the client is told);
- a failed M2 rider or layer report holding M3 Remnants back (the client
  is told);
- `ShuntUseRate` or `FurnishingUseRate` below 10% of fights (a fun
  failure: the levers are Shunt cost, chip rules and KP1 density);
- the naming pass.

### 19.3 After the check (2026-11-21)

The builder, consistency, play and art check of this document raised 65
findings. Every one was fixed, merged into another fix, or (in part)
rejected with a reason; no client answer was overridden.

**Lead's calls made after the check** (marked **(lead's call, after the
check)** in the text; confirmed by the client on 2026-11-21, with this
document):
1. **Floor-level quotas are constructive:** KP1 and KP2 hold in every
   F2–5 fight layer on mandatory sockets; a reservation step in the
   `terrain` stream assigns `teaching` or `signature`, `plank` and
   `cluttered` slots, so KP3, KP8, KP9 and the Plank minimum hold on
   every seed (6.2.4, 6.2.5; V9 `ReservationCapacity`).
2. **Sockets carry a rules key,** and the skin draw only picks skins
   whose rules match; V9 `KitCoversRulesKeys` (6.2.5).
3. **The stream is specified in full:** candidate lists, the Supper bay
   exclusion, group draws, phase sockets, a global `maxSockets`, forced
   draws for withheld sockets (6.2.5).
4. **Cover faces within 45 degrees** of the line of fire, with a
   test-vector table (CV2).
5. **Cover sits at weapons 7.5 step 2b** (and the matching ability 14.3
   step); "already has disadvantage" means net; against a reaction the
   larger of cover and the reaction bonus applies, offers are evaluated
   with cover applied, and a reaction or window fill worth ≤ 2 is not
   offered against a Covered attack (CV4).
6. **CV8 is computable:** the halves by the entry-to-enemy bisector;
   every start socket treated as possibly a marksman; the landing test
   stated; initial-state guarantees only (CV8).
7. **The contact and harm save:** the pusher's ability difficulty, else
   8 + Athletics; it is a hazard-edge save for every rule keyed on that
   name; rolled at weapons 7.5 step 10, before step 11 (6.5.2).
8. **The one-instance cap is a top-up rule;** voluntary harm entries,
   turn-end effects, Remnants and traps are outside it; setup payouts
   are not instances (6.4).
9. **A successful harm-square save is an ordinary obstacle stop,** so a
   non-verb push still deals the collision band; a failed one lets the
   push continue under the cap; the area cap gives the save to the first
   2 units by starting square (6.5.2).
10. **`NoCheapKill` is an exact scorer term** (6.9; TR-15).
11. **`HewCeiling` gains an explicit contact term,** and its per-unit
    clause reads the collision band only (6.10; TR-30).
12. **Environment bands are indexed by the floor** (11.1).
13. **Verb resolution:** straight-line direction with no fallback; Move
    Shunt cannot be chosen into a unit; a light piece breaks on an
    Attack Shunt hit; the size rule on every piece hit; Attack Shunt,
    Kick and Smash end Hidden; piece travel obeys the corner rule;
    `fragileToCollision`; sturdy pieces Smashed at their heft (6.3.2–
    6.3.4).
14. **Kegs:** heavy and sturdy (heft 12); the unreachable third trigger
    dropped; burst line of effect from the Keg; the Smasher is outside
    its own burst (6.3.5).
15. **The combo order** inside one action, and Charged Water's recipients,
    base, weakness order, ally rule (ability 8.2) and order with the
    Soaked arc (6.8).
16. **`FurnishingNeverSeals`:** the vacuous width-2 fill is replaced by
    a reachability rule and a lengthening bound against the initial
    state; `protectedSightLines` added; trap squares allowed, never
    sprung (6.3.6).
17. **The `ThemeSix` withholding of harm squares under an RT-14 refusal
    is dropped** (6.5.5).
18. **Previews are functions of party knowledge,** with branches per
    save; `PreviewHiddenDelta`; cover shields specified (6.12).
19. **Report definitions:** the no-terrain baseline arm, kill and death
    attribution, Teetering uptime, the DEP-A1 reading, and a
    `FloorMinutesDelta` gate at +1.6 min per floor that fires the cut
    order (11.3, 11.4, risk 7).
20. **The Remnant countdown and stacking** are exact: round-end step
    position, creation round skipped, `sourceId`, the two `causedBy`
    meanings (6.6).
21. **Data fields** reconciled: Furnishings are their own class of Low
    height; `sturdy` is a flag; size, heft, noise, fragility; face sides;
    phase-socket states; layer tags and clutter role (6.2.5, 6.3.1,
    11.1).
22. **The teaching room teaches what the rules do:** an Attack Shunt or
    Kick of a light piece into a lone enemy (6.5.6, 7.2).
23. **Theme kits:** the Heart's harm skin is thorn-rose vines; the Wake
    and Gut have no round piece; no inert piece shares a Keg silhouette
    (balancing ball, linen presses, hatboxes); the Wheel of Knives
    always carries its face; low forms for trestles, stocks and ticket
    crates; liquid pieces dry or lidded; Archive and Heart role-prop
    rulings; rules pieces count as architecture for RC6's scale clause
    (7.1–7.10).
24. **The M2 build table** of 7.11, and the art budget reconciled to it
    (8.2).
25. **The readability kit:** the composable glyph table, a distinct
    walkable-hazard rim, hazard-face floor decals, Keg footprints at
    hazard tier unless about to burst, the Keg pip, the VFX spec and
    residue rule, the Miasma hue (8.1, 8.2, 6.6).
26. **Renames:** Shoot an object (was Strike an object), Bladed (was
    Spiked); the spiked cell door skin is removed.
27. **A segment-5 hook** for traps and pieces (13).

**Told plainly after the check** (none overrides a client answer);
confirmed by the client on 2026-11-21, with this document:
- **cover is narrower than the proposal's test:** the client chose the
  dot-product facing test; the lead keeps it and adds a 45-degree bound,
  because the bare test covered a target from about 270 degrees and made
  flanking useless. Covered shots will be rarer than TN1's 11–37%; the
  re-run is owed (CV2);
- **a +2 reaction does nothing against a Covered attack** and is never
  spent on one: "replaced, not added" made exact (CV4);
- **contact hazards are now in every F2–5 fight room** (KP1 at 100%, was
  ≥ 50%), so F2–3 rooms carry 1–2 instead of 0–2; DEP-A1 features are
  likewise in every F2–5 fight room (6.2.4);
- **`HewCeiling` failed as written:** a Ram into a contact hazard broke
  its per-unit clause at every level; the per-unit clause now reads the
  collision band only, a change to a Vanguard lead's call (TR-30);
- **Charged Water is the "conducting water" that stat model 2 held back**
  until riders and layers are proven; it arrives in M2, before that
  gate, because it is instantaneous and makes no square (TR-26);
- **"Deliberately dry" now means "no condition squares":** the Supper,
  Gaol and Kennel get condition-free harm, Rough or Plank squares, which
  changes agreed RS 8.9 and RS 6.3 text (TR-22);
- **the Kennel's signature feature** becomes "fallen logs (cover) and
  kennel gates (Portals)", family "cover logs" (RS 8.4; TR-22);
- **boss phase (e) is narrowed** to pre-placed phase sockets with
  existing kinds, announced a round ahead (TR-16);
- **a party-triggered Remnant counts as party-sourced** for the
  per-fight cap, a change to conditions 11.6 (TR-27);
- **the corner rule is the M0 plan's, not decided text,** so TR-5 is no
  longer flagged as touching decided main 4.3;
- **the clock now has a minutes gate** at +1.6 min per floor beside the
  rounds gate, and the cut order fires automatically (11.3, risk 7);
- **harm squares no longer depend on RT-14,** and the earlier
  withholding rule is dropped (6.5.5).

**Rejected in part, with reasons** (confirmed by the client on
2026-11-21, with this document):
- raising the contact band to 2× collision (4/6/6): it would fail
  `HewCeiling` further and reverse the TN7 and TB5 resolutions the
  client let stand; the harm-square stop fix and the "+1" preview
  headline answer the finding instead;
- making the Attack Shunt Prone a Shove contest: the client's Q2 answer
  names "a Prone save"; breaking the light piece on the hit ends the
  anvil instead;
- forbidding Furnishings on trap squares now: traps are segment 5's;
  the default is stated and segment 5 may narrow it.

---

## Appendix A: how the critique findings were resolved

### A.1 Fun critique

| Id | Finding | Resolution |
|---|---|---|
| TF1 | Shunt and Shove are almost never better than attacking | Move Shunt for light pieces costs 2 movement; Attack Shunt adds a Prone save on collision; Kick brought to M2 (6.3) |
| TF2 | A successful edge save makes a knock-into fizzle | Success is an ordinary obstacle stop, so non-verb pushes still collide, and after the check this holds for harm squares too; save odds and the failure's difference are the preview headline (6.5.2, 6.12) |
| TF3 | Floor 1 shows the solo player no terrain | Teaching room per floor-1 theme; typed Low pieces; Gaol noise identity (6.5.6, 7.2) |
| TF4 | Round-1 Keg alpha strike | Kegs ≥ 2 from enemy starts; no shooting Kegs in M2; Shoot an object (M3) ends Hidden; `BurstRound1Share` |
| TF5 | Cover makes Kiter fights slogs | CV8 artillery flank rule (role renamed Marksman, naming pass NP-19); move-hover shields kept while the move tool is open; Smash and (M3) Shoot an object break cover |
| TF6 | Lingering ask fully deferred | The M2 legal package bundled (6.6), including one gas-aura monster per Poisoned theme; Q4 bundles K2 and Conduct (renamed Charged Water, naming pass NP-19); M3 Remnants lead with ignition; Mist (renamed Fog, naming pass NP-19) dropped |
| TF7 | Themes feel samey | One signature interaction and terrain Law line per theme (7.1, KP9, TR-22) |
| TF8 | Enemies barely use terrain | One visible terrain-using enemy per F2–5 theme with telegraphed intent; bosses get an authored terrain move via segment 9 (6.9, 6.11) |
| TF9 | Hover-only verbs hurt discovery | Contextual chip when a verb pays off (6.12) |
| TF10 | Cover that hurts and cover that hides | Hazard pieces show the rim and the cover notch (the composable glyph table, 8.1); "Needs a tall piece to hide" reason (8.1, CV7) |
| TF11 | Wrecker dominance | KP3 cap; value spread to all classes (TF1, TF2); `EnvDamageShareByClass` |
| TF12 | Shortcuts never chosen | KP7: Planks and harm shortcuts end near enemy cover lines or flank squares |
| TF13 | Keep the fun-right rejections | Kept (6.6, 6.3.7) |

### A.2 Balance critique

| Id | Finding | Resolution |
|---|---|---|
| TB1 | Contact on verb pushes makes Gust a damaging at-will and Give Ground an area spike | Verb-only pushes deal the typed collision band; area forced movement deals contact to ≤ 2 units per action; Gust contact counted in the at-will rider bound (6.5.2, TR-10). Shockwave (renamed Upheaval, naming pass NP-19) and Squall corrected to non-verb (new lead's call 1) |
| TB2 | Brink on faces gives no-save boss disadvantage | Brink read literally (hazard squares only); contact hazards give no Teetering; `TeeteringUptimeOnBosses` gate with the "bosses steady" lever in reserve |
| TB3 | Movable Kegs break the spacing guarantee | Kegs sturdy in M2; no burst triggers a burst in M2 (6.3.5) |
| TB4 | A 1-wide Clearway plus Shunt builds funnels the AI cannot answer | Clearway ≥ 2 wide in fight rooms; `FurnishingNeverSeals` reachability and initial-state lengthening rules (the width-2 fill replaced after the check); universal monster light Smash (6.2.2, 6.3.6, 6.9) |
| TB5 | 4/6/8 contact blows through `HewCeiling` | Contact = collision + 1 step (3/4/4); `HewCeiling` counts contact; KP3 as a rule |
| TB6 | Environment damage stacks with no per-action cap | One environment damage instance per unit per action (6.4, TR-13) |
| TB7 | Enemy-triggered Kegs against a crowded party | Footprints always drawn; Keg triggers count as attacks in intent and focus spread; guards every round; `NoCheapKill` covers triggered environment damage |
| TB8 | RT-14 as a single point of failure | Damage-only hazards never give Teetering; harm squares need no ruling (the withholding dropped after the check); Supper Poisoned rows conditional (6.5.5) |
| TB9 | Cover against reaction AC timing undefined | Suppressed against charged AC; step 2b in the pipeline; the larger of cover and the reaction bonus, with offers evaluated with cover applied and the preview note (CV4) |
| TB10 | Symmetric cover buffs artillery (role renamed Marksman, naming pass NP-19), worst on floor 1 | No covered monster starts on floor 1; the solo table re-run with the adopted arm; `Floor1ClassSpread` |
| TB11 | Party-triggered Remnants as ground zones; clouds pin AI and routers | Clipped off Portals, the Clearway, flee exits and chokes; routers may cross non-lethal ones; party-sourced for caps; told plainly in Q5 |
| TB12 | Conduct (renamed Charged Water, naming pass NP-19) punishes the party in Choir water | Halved after weakness; once per unit per event; never from monster at-wills; counted in the guards |
| TB13 | `NoCheapKill` readable as immunity; per-round cap demoted on a weak toy | Phrased "does not prefer"; per-round caps kept as switchable simulator arms |
| TB14 | Shade parity | V9 Hide-spot band; Shade yardstick re-run; Overturn ordering stated |
| TB15 | Clock and companions | Verbs never offered on companion turns unless opened; net rounds gate ≤ +1% |
| TB16 | Determinism edge rules | Shoot an object rules (M3); `causedBy` for monster-triggered Kegs; fixed AI tie-break; HE-7 re-derivation; Stalwart order (6.3.7, 6.4, 9.2) |

### A.3 Consistency critique

| Id | Finding | Resolution |
|---|---|---|
| TC1 | Contact hazards are not "10.2 unchanged" | A new obstacle-hazard branch written against 10.1 bullet 2, 10.2, 11.3 and weapons 7.2 rule 4; 18.4 Brink read literally (TR-10) |
| TC2 | −2 against +2 AC, a false equivalence | Stated honestly (CV5); armour 11 row and armour 8.1 rule 6 flagged (TR-4); judges' notes corrected |
| TC3 | M1 Low pieces contradict weapons 9.5 bullet 1 | TR-3 rewords it for M1 |
| TC4 | RT-14 fallbacks not carried into kind lists | Supper Poisoned rows conditional; no floor-1 hurting pieces in the M3 plan before RT-20 and RT-14; RS 14 date (TR-14) |
| TC5 | Exceptions to "never change" multiplying | One consolidated TR-8 including the Arcanist sentence; debris marked a change; phase (e) via sockets |
| TC6 | The arena contract trespasses on segment 9 and RS 13 | Caps and kind rules only; counterplay identity stays RS 13 and segment 9; braziers never the counterplay (6.11) |
| TC7 | Tall pieces against FL 8.1 stubs and height | Full wall height; stub "blocks sight" glyph; V9 stub-state check (8.1) |
| TC8 | Corner cutting disagreement | Every movement-blocking square, including forced moves and piece travel; cited to the M0 `CornerCut` rule after the check (TR-5) |
| TC9 | "Shoot the keg" unplaced; targeting objects | M3 Shoot an object with a targeting row; Hidden ends; `NoCheapKill` extended; guards (TR-28) |
| TC10 | Lines, Skewer, reach undefined against pieces | Tall = wall; Low pieces never stop lines (TR-5) |
| TC11 | Rider-free ruling ambiguity; braziers thaw Frozen | Ruling text includes the damage-end row and payouts; told plainly (6.5.2, TR-13) |
| TC12 | Name collisions and inconsistent vocabulary | Fixture, Volatile, Roll, Hot Furnishing, "burst" as a class, Flanked dropped; Clearway and harm square unified (front matter) |
| TC13 | Wrong citations | Flanking rejection cited to the Shade 9 table; Volatile to enemies 17.2; the Vanguard 9 "Rejected" row with its main 2 justification (CV6, TR-7). While writing: Shockwave (renamed Upheaval, naming pass NP-19) and Squall cited to the Arcanist design 8.2 and 8.3 as non-verb (new lead's call 1) |
| TC14 | Remnants miss RC1 | RC1 in TR-27 and TR-20 |
| TC15 | Briar without flags | Its own future question with RS 6.3, 8.4, RT-20 and 11.2 listed (19.2) |
| TC16 | Rough stacking undefined | Costs never stack above 2 (6.2.1, TR-11) |
| TC17 | Faces against "adjacent to a hazard" rules | Adjacency = the single square in front; free-standing pieces preferred near enemy starts (6.5.1) |
| TC18 | The register needs one renumbering | One register TR-1 to TR-30 (TR-30 after the check); no draft ids used (17); numbers findings renamed TN (new lead's call 10) |
| TC19 | Unstated FL proof dependencies | Static matrices stay wall- and Portal-only; V9 `TippableExcluded` (6.2.1, 11.2) |

### A.4 Judges' grafts and numbers-pass fixes

Every judge graft is adopted (5.3) except where a critique overrode it:
- **"Shoot the keg" in M2** (judge 1) moves to M3 (TC9, TB3).
- **Mist (renamed Fog, naming pass NP-19) as a second Remnant** (judge 3) is deferred (TF6).
- **"Contact applies to verb pushes"** is kept but at the collision band
  (TB1).
- **Free-authored phase (e)** (judge 1) becomes phase sockets (TC5).

Numbers-pass fixes TN1–TN17 are adopted as listed in section 10.

## Appendix B: sources

Approved specs: all 19 under
`/home/user/DungeonTTRPG/docs/superpowers/specs/`. Checked directly
while writing this document:
- weapons 7.2 rule 4 (verb-sourced movement) and 9.5;
- conditions 10.1–10.4 and 11.1–11.6;
- the Arcanist design: Gust (verb-sourced for collisions), Upheaval and
  Squall (non-verb), "Never: ground zones";
- the Herald design: Give Ground (non-verb, collision bludgeoning);
- the Vanguard design 8.3 (Brink; the rejected list) and the 9 table
  "Rejected" row;
- the Shade 9 table (flanking, facing, smoke clouds);
- armour 11 item 1 and 8.1 rule 6;
- enemies 11.1 Exposure, 17.2 Volatile, 17.3 Holder, 17.5 arena bullets
  and phase (e);
- main 2 Non-goals; ability 8.4;
- RS 7.6, RS 8.0 `teeteringFallbackDrop` (Supper: Poisoned), RS 13 Boss
  rooms row (Supper: chandelier of icicles);
- FL 8.1 "tall piece", FL 13 hand-off rows.

Web checks (from the research briefs): bg3.wiki and Fextralife (Shove);
Game8 BG3; the Frosthaven rule-book mirror, rules summary and FAQ; Dized
Gloomhaven hazardous terrain; UFOpaedia and Fextralife (XCOM cover);
Ludo.guide XCOM 2; Game Developer (XCOM deep dive; Into the Breach UI);
GDC Vault and 80.lv (Into the Breach postmortem); Archives of Nethys
(PF2E forced movement); EN World (4E prone save); Lancer cover
summaries; a Steam discussion (BG3 AI shoving); DOS2 reviews (Senses.se,
TechRadar).

Toy simulations (scratch, not project files):
`/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/s3/`.
