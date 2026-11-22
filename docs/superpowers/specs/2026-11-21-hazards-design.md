# Hazards: Design (Dungeon Generation, Segment 4)

Date: 2026-11-21
Status: Approved, 2026-11-21
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "C"),
`2026-11-16-enemies-design.md` (enemies design, "ENM"),
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
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1),
`2026-11-21-floor-layout-design.md` ("FL", segment 2) and
`2026-11-21-room-terrain-design.md` ("RT", segment 3, the most recent
template)

Items marked **(agreed)** were decided by the client on 2026-11-21 for
the four questions this design raised (Q1–Q4, sections 0, 6 and 19.1),
by choosing the lead's recommendation (option A) in each case, or
restate a client answer recorded in an approved document. The client
also said that everything else in the lead's proposal stands as written:
the method (Model A+: the Model A chassis with its grafts), the
taxonomy and the hazard/trap line, hazard points, room roles and quotas
(never scaled by party size), the placement rules, one clock per room,
the Dream step and the Stir charter, Warned or floored, the EnvRound
cap, Tableaux, the Void family and its ship gates, bile, deep water,
vents, the RT-14 recommendation with both guards, Moods, Dare rooms and
Plunge, the caps, the AI requests, the party levers and Cords, out of
combat, class interplay, companions and party size, the boss arena
hooks, previews and UI, the full per-theme catalogue across all ten
themes, readability, art, VFX and audio, the adopted, deferred and
rejected mechanics, the numbers and fixes, data, validators and the
simulator, the staging, the segment 4 / segment 5 line and the later
hooks, the binding requirements, the HZ-n changes, the told-plainly
items, the risks and the critique resolutions and names. The client
approved this document as a whole on 2026-11-21, and answered the one
item left for it, Q1's reach in M2, by accepting the lead's
recommendation (19.1).

Items marked **(lead's call)** were filled in by the design lead inside
approved rules and were in the proposal the client answered; the client
confirmed them all on 2026-11-21, when it approved this document as a
whole, and they are binding. Items marked **new lead's call** were made
while writing this document, beyond that proposal; they are listed in
19.1 and the client confirmed them all on 2026-11-21. Items marked
**(lead's call, after the check)** were made while revising this
document after the design check that followed the client's Q1–Q4
answers; they are listed in 19.1 and the client confirmed them all on
2026-11-21. Items marked **(settled)** restate a rule from an approved
document; **(decided)** marks a main-design client decision. Items
marked **(change HZ-n)** amend an approved document and are listed,
with the exact section they touch, in section 17; rows that touch
**decided** or **agreed** text are flagged there, and the client's
approval of this document on 2026-11-21 (with Q1–Q4 where a row names
one) covers them. Items marked **(told plainly)** narrow or re-read
agreed or settled text, or state an unwelcome forecast, and were told to
the client in plain words (19.1, including those told after the check);
the client confirmed them all on 2026-11-21. Every item here is binding
on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q4 of this document (2026-11-21), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-21 |
| **new lead's call** | A lead's call made while writing this document, beyond the answered proposal; listed in 19.1 and confirmed by the client on 2026-11-21 |
| **(lead's call, after the check)** | A lead's call made in response to the design check that followed the client's answers; listed in 19.1 and confirmed by the client on 2026-11-21; never overrides a client answer |
| **(change HZ-n)** | Amends an approved document; listed with its exact section in 17 |
| **(told plainly)** | Narrows or re-reads agreed or settled text, or states an unwelcome forecast; told to the client in plain words (19.1) and confirmed by the client on 2026-11-21 |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (10, 11). Never binding |

**The client's brief:** dungeon generation segment 4 of 12, hazards.
"Nothing is barred hazard-wise, simply consider if it'll be considered
fair and interesting against the mechanics and game loop, and consider
it against our themes. You can get as creative as you'd like." The
approved documents owe this segment per-theme hazard catalogues and
floor-scale density, drops and what a fall does, bile (RT-13),
Teetering counting (RT-14), vents, theme-scale set pieces, the Hinge
and Postern no-hazard radius, the Sanctum bridge rule, the Inverted
Cathedral's drop and the Kennel's staked pits (3.2).

The lead's answers, which the client accepted (Q1–Q4, **agreed**), are
in section 0: **a hazard is visible from reveal, acts by a rule, and is
never found by a check or disarmed; the static Model A density chassis
carries M2, with one telegraphed signature Stir per floor-2–5 floor
behind a gate (Q1; at most one per floor, and in M2 only on the Choir
and Carnival floors, told plainly after the check); a fall never kills a party unit, which Dreamfalls to
the room's Snag, while non-elite monsters that fall are defeated, elites
Clutch once and bosses never fall (Q2); the moving dream (Tides, Cord
Flips, the Turning Ring, the Returns Trolley, the Cathedral's reversal
round) arrives at M3 under pre-proven states (Q3); and floor 1 stays
hazard-free in the base game, with one damaging hazard package per
floor-1 theme from Nightmare tier 2 (Q4).**

**Change prefix.** This design uses **HZ-n** ("hazards"). A grep for
`HZ-` across the 20 approved documents finds 0 hits. This document
issues one register, HZ-1 to HZ-30 (17). The check that followed the
client's answers added no HZ row; its changes sit inside the existing
rows and are listed in 19.1. Local id schemes, each
grep-checked as a whole word with 0 hits: tensions **HT1–HT19**,
placement rules **VP1–VP10**, Stir charter rules **SC1–SC14**, toy
fixes **F-a to F-k**, and the Appendix A findings **HF** (fun), **HB**
(balance) and **HC** (consistency) **(new lead's call:** the proposal's
tension ids E1–E19 collide with the Arcanist's E3 combo ids, and its
critique numbers were bare**)**.

**Names are placeholders** until the client naming pass. Each was
grep-checked case-insensitively as a whole word across the 20 approved
documents. These have 0 hits: Stir, Stirring, Snag, Clutch, Dreamfall,
Tide, Mood, Lurch, Snatch, Plunge, Lull, Chainpost, Tableau, Tableaux,
"Dare room", Operate, Startle, Handhold, Lamplighting, Brim,
Peristalsis, Fitful, "Dream step", "Dream card", FallFloor, EnvRound,
EnvDC, "due square", "Landmark tick", Groggy, Gullet, `voidFall`,
`hazardDigest`, `RoomHazards`, Stoke, `floorborn`, "Mute round", Taint,
"Turning Ring", "Returns Trolley", Treadwheel, Stampede, "Open Grave",
"Painted Hunt", "Organ Swells", "Pit Breathes", "Drowned Crypt",
"Drowned Nave", "Carrel Rows", "Fire-Eater's Pit", "Laid Table", "Bile
Low", "Thorn Garden", "Forbidden Course", Courses, "Horn Post",
"Curfew Rope", "Lockdown Lever", "Small Hour Strikes", "Guttering
Candles", "Mourning Cord", "Leaning Stacks", "High Wire", "Chained
Bear", "Alarm Clock", "Falling Toys", "Counting Sheep", "Toss and
Turn", "Dark Under the Bed", "Dreamer Turns Over", "Fall Upward",
"Chimes Ring Upward", "Open Window", "Carving Hound", "Service Lift",
Nightlight, "Long Night", "hymn bellows", "clapper chain", "Sluice
Wheel", "Acid Tide", Digestion, "High Water", "Silence Kept", "Last
Orders", "Nearer Waking", Encore, "Second Sitting", Fasting, "Spring
Tide", "Hymn Must Never Stop". Added after the check, with the same
0-hit grep: "latent footprint", "standalone Cord", "Dream tick", "Wind
Back", `HazardMinutesDelta`, `StirMinutesDelta`, `StirKindRecord`,
`StirInstance`, `LurchForm`, `holdIfOccupied`, `RoomVoidSet`,
`MenuIndex`, `CapRound`, `RescuePushRate`, `DownedAfterDreamfall`.
- **Hits read in context and kept:** "Cord" (4 hits, all the
  lower-case "cords" of the Snaring and Grasping Roots binding; a Cord
  here is a capitalised rules piece, and the naming pass is asked to
  check it); "Void" (5 hits: the generic verb "void the guards", FL's
  backdrop "void beyond the room edge" and RT 6.5.7's "void squares",
  which this document fills); "Overdue" (1 hit: RS 8.7's Silk Obsession
  "overdue borrowers", which the Silk Omen deliberately echoes); "Wheel
  of Knives" (RT's Carnival piece; this document's Stir runs on that same
  piece); "Emit", "Warned" and "Overturn" (ordinary verbs, or RT's M3
  Overturn, which the Overturn part rides).
- **New lead's call:** six proposal names collide and are replaced
  (A.5): the Stir part "Topple" (the weapon verb) becomes the
  **Overturn** part; the "waking" fall state (the Heart's Law word) is
  **Groggy**; "the Throat" (the Shade's Throat-Cut) is **the Gullet**;
  "the Draught" (the consumables' draughts) is **the Open Window**; "the
  Night Bell" and "Bells Ring Upward" (RS 8.0 avoids "Bell" head words
  beside the Herald's Passing Bell) are **the Curfew Rope** and **the
  Chimes Ring Upward**; the "Reading Room" and "Long Table" tableaux
  (FL's generic reading rooms and the Supper's Long Table Blueprint) are
  **the Carrel Rows** and **the Laid Table**. The proposal's "Dream
  Rules codex" is not a new system: it is a set of pages in RS's M3
  codex (RS 12).

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every
rate and share comes from crude toy models (10, Appendix B); the M2
simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile,
unlocks, ladder), **M4** breadth, **Later** **(settled, main design
9)**. **M2 (gated)** means the item ships in M2 only if its named gates
pass; otherwise it falls back to M3 **(lead's call)**.

Abbreviations: KP = RT's placement rules KP1–KP9; RC = the readability
charter RC1–RC9 (RS 7.4); K1–K5 = RT 6.8's closed combo table; TN =
RT's numbers findings; LG = FL's graph invariants.

---

## 0. At a glance

### 0.1 The answers to the brief

**1. What a hazard is (lead's call; change HZ-1).** A hazard is visible
from the moment its room is revealed, acts by a rule, and is never found
by a check or disarmed. The party can avoid it, time it, plug it,
Operate it or turn it on its enemies. A trap is hidden, or is one armed
device that is sprung, with detect and disarm checks. The one-line
test: **would detection change anything?** If yes, it is a segment-5
trap.

**2. Static against dynamic: Model A+ (lead's call; Q1 agreed).**
- **Model A (static density) is the chassis.** In M2 almost every
  hazard is a static square, piece or region placed by a budget, and it
  acts only on entry, at turn end, or on the forced-movement edge save.
- **Dynamic hazards are Stirs:** telegraphed room routines that act in
  the round-end environment step conditions 5.4 already reserves (the
  **Dream step**). Each Stir is announced on exact squares one full
  round ahead, hits both sides, and never acts in round 1.
- **M2 (gated) gets one signature Stir room per F2–5 floor (agreed,
  Q1):** the Choir's **Organ Swells** first, the Carnival's **Pit
  Breathes** as the alternative. It ships only if the reading-time
  playtest, the TR-27 written report pass, and the clock and
  fight-length gates pass; otherwise it falls back to M3. The broad
  Stir catalogue is M3.
- **Told plainly:** the toy shows Stirs barely move kills (+0.03 to
  +0.06 points) or fight length. What they buy is a best square that
  changes during the fight, and they cost reading time. In M2 the
  brief's dynamic vision is at most one breathing room per floor.
- **Told plainly after the check:** both Q1 candidates belong to F2–3
  themes, so in M2 a Stir can only appear on a Choir or Carnival
  floor. If both pass their gates (6.3), every run has one or two Stir
  floors (two in about a third of runs, when the Silk is not drawn); the
  Silk floor and F4–5 have none. Q1's "one per F2–5 floor" is therefore
  a cap in M2, not one on every floor. **The client accepted this on
  2026-11-21 (19.1):** Q1 stays a cap in M2, both pilots may ship on
  their own gates (one or two Stir floors per run), and F4–5 Stirs
  arrive at M3. On toy numbers the Stir
  is also likely to miss the clock gate unless its measured reading
  cost comes in well under the toy's (10), in which case Q1's own
  fallback sends it to M3.
- *Rejected:* B, nothing timed in M2.

**3. Drops and chasms (agreed, Q2; change HZ-9, HZ-10, HZ-11).** A
lethal-safe drop under one flat rules level is a **Void** square: flat,
non-walkable, crossable only by Jumping and Flying, with line of sight
passing over it.
- A party unit that falls **Dreamfalls**: it takes the fall band (the
  contact band), which can never take it below 1 HP (**FallFloor**),
  and is placed Prone on the room's **Snag**, a proven landing on the
  party side. It is **Groggy** until the end of its first turn that
  starts after the placement. A Void failure after a Dreamfall already
  taken in the same fight leaves it on the edge.
- A non-elite monster that falls is removed and defeated (`voidFall`
  experience, paid as a kill).
- Elites and group leaders **Clutch** the edge once per fight, and
  cannot fall until they have taken a turn after the Clutch.
- Bosses never fall.
- **Teetering's disadvantage does not apply to the Void-fall save.**
- **Pilots, M2 (gated):** Silk "The Stacks Have No Bottom" (at most 2
  Void rooms per F2–3 Silk floor) and Choir "The Drowned Crypt" (F3, at
  most 1 room), under the Void ship gates (6.4).
- Voids, not Stirs, are the measured lever on TN6's environment-kill
  shortfall. In-combat drops into another room are rejected (the toy
  doubled hero deaths); their fiction survives as the out-of-combat
  **Plunge** (M3). They unblock the Inverted Cathedral (RS 8.12).
- *Rejected:* B, lethal fall damage; C, Voids wait for M3.

**4. Bile (RT-13; change HZ-4, HZ-5).** Gut only. 6–12% of a Gut fight
room's walkable squares *(illustr.)*, in pools on FL's bile region tags.
Corroded on entry; a Fortitude save negates it. There is always a dry
route at most 2 squares longer.

**5. Teetering (RT-14; change HZ-6).** The lead recommends "does not
count", with two guards: no monster, boss, trap, Stir, Cord or square
record may apply Teetering directly (party abilities such as Brink
excepted), and bosses never gain Teetering from a Void-edge success or from
Clutch. The conditions owner rules.

**6. Vents (M3; change HZ-16).** A vent emits its Miasma Remnant **when
struck** by default. A vent that breathes on a rhythm is the room's one
Stir. Silk, Gut and Heart only (the Supper only if RT-14 is ruled "does
not count").

**7. The moving dream (agreed, Q3; change HZ-18).** At M3: Tides,
Cord Flips, the Turning Ring, the Returns Trolley and the Cathedral's
reversal round (M4 with its theme), under these rules: every state is
pre-proven, existing square kinds only, announced a round ahead, held
while occupied where needed (a Tide is held whole, so only its two
proven states exist; 6.3), and the flee and escape proofs hold in
every state. This reopens decided main 2's non-goal, agreed conditions
11.1, agreed RT 6.3 and RS 7.6's "Later"; the flagged rows of 17 carry
them. *Rejected:* B, Tides and Flips only; C, Later.

**8. Floor 1 (agreed, Q4; change HZ-19, HZ-25).** The base game keeps
floor 1 hazard-free (RT-20 kept literally). Floor 1 gets **Landmark
ticks** from M3: a non-hazard class of harmless rhythm (doors that
swing shut, a mute round, candle countdowns, a horn that startles
beasts) that teaches the Stir interface before anything can hurt. From
**Nightmare tier 2**, each floor-1 theme gains one damaging hazard
package (the Gaol Treadwheel; the Kennel's staked pits with the
Painted Hunt at tier 2, the Stampede replacing the Painted Hunt at tier
3; the Wake's Open Grave), with DH 17.1 and the
floor-1 guard rows still holding. *Rejected:* B, floor 1 hazard-free
at every tier.

**9. How creative, and how fair.** As creative as the catalogue in
section 7: an organ that discharges into its own flood; a fire-eater
pit that breathes; a menu card that shows which course will hurt you in
three rounds; a choir whose silence drowns the nave; a bed you must hold
on to when the sleeper turns; a cathedral you fall *up* out of. Fair by
construction:
- every environment act is announced a full round ahead on exact
  squares;
- no environment act can kill a hero who could not have stepped away
  (**Warned or floored**);
- environment damage to one party unit is capped per round (the
  **EnvRound cap**);
- previews are exact.

**10. The lead's five limits on "nothing is barred" (lead's call; the
client accepted them with Q1–Q4):**
1. **No hero dies to a fall.** "I was shoved off by the AI" is the most
   resented permadeath death there is.
2. **Nothing acts on a square it did not announce, and nothing draws a
   random number at act time** (saves excepted). Read as "hazards make
   no act-time draws; traps' object checks use `checks`" (traps and
   locks design 6.22; change TL-3).
3. **No spreading surfaces.**
4. **Floor 1 stays dry for the solo hero in the base game.**
5. **Escalation is in kind, not damage:** nothing deals more than the
   approved bands.

Everything else in the brief is in this document, in some wave.

### 0.2 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Hazard / trap / terrain line; "would detection change anything?" | M2 | lead's call (a reading); HZ-1 |
| 2 | Hazard points per room, room roles, quotas; never scaled by party size | M2 | lead's call; HZ-2 |
| 3 | Placement rules VP1–VP10 (entry, Clearway, push lanes, Hide spots, Hinge and Postern) | M2 | lead's call; HZ-3 |
| 4 | Bile density and placement; Fortitude save; RS 8.10 "acid sources" | M2 | lead's call; HZ-4, HZ-5 (agreed text, flagged) |
| 5 | RT-14 "does not count", plus the kit guard and the boss guard | M2, before rosters freeze | request; HZ-6 (conditions owner rules) |
| 6 | EnvRound cap: per-round, per-party-unit environment damage cap | M2 | lead's call; HZ-7 |
| 7 | Tableaux: named static compositions in the KP9 signature slot | M2 | lead's call; HZ-8 |
| 8 | Void, Dreamfall, Snag, Clutch, FallFloor; Silk and Choir pilots | M2 (gated) | **agreed, Q2**; HZ-9 to HZ-11 |
| 9 | Dream step, one signature Stir per F2–5 floor (at most; Choir and Carnival floors in M2), Warned or floored, Cords that re-time | M2 (gated) | **agreed, Q1**; HZ-12 to HZ-15 |
| 10 | Struck vents; breathing vents as a room's Stir | M3 | lead's call; HZ-16 |
| 11 | Broad Stir catalogue; Lurch (soft environment forced movement) | M3 | lead's call; HZ-17 |
| 12 | Tides, Cord Flips, the Turning Ring, the Returns Trolley, the reversal round | M3 (Cathedral M4) | **agreed, Q3**; HZ-18 |
| 13 | Landmark ticks on floor 1 | M3 | lead's call, told plainly; HZ-19 |
| 14 | Moods only as Omens or Nightmare content | M3 | lead's call; HZ-20 |
| 15 | Dare rooms on the existing out-of-combat tick | M3 | lead's call; HZ-21 |
| 16 | Snatch, Plunge, Startle (morale event), Chainposts | M3 | lead's call; HZ-22 to HZ-24 |
| 17 | Escalation through Omens and Nightmare tiers (kind, not stats); floor-1 hazards at tier 2+ | M3 | **agreed, Q4**; HZ-25 |
| 18 | Inverted Cathedral package | M4 | lead's call; HZ-26 |
| 19 | Segment-4 clock allowance (+0.3 min per floor) inside the terrain tolerance, with a combined gate; risk-7 cut order extended | M2 | lead's call, told plainly; HZ-27 |
| 20 | V10 `RoomHazards`, reports, the `hazards` stream, `hazardDigest` | M2 | lead's call; HZ-28 |
| 21 | Readability and the reserved RC7 cues | M2 / M3 | lead's call; HZ-29 |
| 22 | The Forbidden Course (party-only environment heal) | M3 | lead's call, DH owner rules; HZ-30 |

---

## 1. Intent

The main design fixed one flat square grid, symmetric line of sight and
one flat rules level per floor **(decided, main 4.3, 6.8)**, let
Jumping and Flying cross only hazards tagged crossable, and refused
spreading, evolving or player-placed ground surfaces while allowing
fixed condition squares placed by floor generation **(decided, main
2)**. The conditions design built those squares and the hazard-edge,
landing and lethality rules (10, 11); the room terrain design built the
room's classes (contact hazards, harm squares, Kegs, Planks, the
Clearway), their bands and caps, and an M3 round-end environment step
for Remnants (RT 6, TR-27). None said how much of it a floor holds, what
a fall does, whether anything may act on its own, or how a theme's
hazards differ from its neighbour's.

The answer in one sentence: **hazards are visible, rule-bound room
content placed by a per-room budget and proven at build time; the few
that act on their own are telegraphed a full round ahead on exact
squares, hit both sides, and can never kill a hero who could not step
away; and a fall moves a party unit, never kills it.**

## 2. Goals, non-goals, anti-goals

### Goals

- **Every F2–5 floor has hazards that change where units stand,** in
  readable amounts set by a budget, never by party size (6.2).
- **Themes play differently:** one Law, one catalogue and one signature
  hazard per theme (7).
- **The room is a weapon for both sides:** every hazard can be turned on
  monsters, and the best plays are pushes, holds and timing (6.13).
- **Falls exist and matter** without a hero ever dying to one (6.4).
- **Every act is foreseen:** due squares, countdowns and exact previews
  (6.3, 6.18).
- **Every seed works:** every state is proven at build time by V10, and
  the runtime cannot fail (11).
- **The clock is respected:** segment 4 spends at most +0.3 min per
  floor (10, HZ-27).

### Non-goals

- **Traps, locks, checks and the DC ladder.** Segment 5 (13).
- **Encounter fill, loot, events and boss interiors.** Segments 6–9;
  this document hands them hooks (13).
- **Height, stand-on pieces, multi-level rules geometry.** Main 6.8
  **(decided)**. Depth is backdrop only.
- **Light and darkness hazards.** RS 7.6 and enemies 5.1; Later with
  NM-9.
- **New condition ids.** Every hazard reuses its theme's 6 or the
  universal tax (C 16).
- **Final names, numbers and art:** the naming pass, the simulator and
  the art team.

### Anti-goals

- **Murder by environment:** a hero killed by something it could not
  see coming or step away from.
- **Surface soup:** spreading, drifting or combining ground (the
  Divinity: Original Sin 2 and Noita failure).
- **A tax on one side:** a hazard that only ever hurts the party.
- **"Wait it out" hazards:** rhythms that reward stalling (SC6).
- **Instant death of any kind,** for heroes and companions.
- **Item loss:** nothing falls out of the world.
- **Readability overload:** more than one clock in a room.
- **Shove-trivialised elites and bosses:** one push removing a room's
  centrepiece.

## 3. Constraints

### 3.1 What the parents already fix

The 20 approved documents were grepped for hazard, Void, chasm, drop,
fall, Teetering, bile, vent, Remnant, timed, environment, lair, Brink
and Snag, and each hit was read in context.

- **Flat level (decided):** main 4.3 and 6.8. Jumping and Flying only
  cross tagged hazards. Walkable squares are coplanar (FL LG18). Drops
  are "flat voids" (RT 6.5.7, RT 14).
- **Surfaces (decided and agreed):** main 2's non-goal (no spreading,
  evolving or player-placed surfaces), amended only by TR-27 for M3
  Remnants. C 11.1's closed list of sanctioned map changes (TR-8). C
  11.4: squares hold no timers except the M3 Remnant countdown.
- **"Nothing timed in M2" was agreed, not a lead's call:** RT 0.1 item
  5, RT 0.2 row 13 and RT 6.6 (agreed, RT Q5; TR-27). The C 5.4 round-end environment step exists
  from M3, behind a written pass on the M2 rider and layer reports (RT
  6.6). Q1 of this document reopens it for exactly one signature Stir
  per F2–5 floor **(agreed, Q1)**.
- **RS 7.6 (agreed):** shifting rooms, gravity or height, and darkness
  or light mechanics are refused for M2, and may appear Later only as
  flagged changes. Q3 brings the shifting-room family to M3 under
  pre-proven states **(agreed, Q3)**.
- **Hazard edge and lethality (settled):**
  - C 10.2 hazard-edge save: Teetering on success for condition squares;
    damage-only hazards never give it; area cap of 2 saves per action;
  - C 10.3 landing has no failure branch;
  - C 10.4: no single failed save, and no entry without a save, may kill
    the hero;
  - DH 10.4: out-of-combat damage ("traps, hazards, falls, the party's
    own areas and ticks") and undetected-trap damage are floored at 1
    HP for party units;
  - DH 11.4: Downed units are inert.
- **Segment 3 caps (approved):**
  - one environment damage instance per unit per action, with the top-up
    rule (RT 6.4);
  - bands: contact 3/4/4, harm 2/3/3, collision 2/3/3, Keg 4/6/8
    *(illustr.)*;
  - `ContactLethalityBound` ≤ 20% of the lowest legal hero's maximum HP;
  - `NoCheapKill`;
  - K1–K5 closed (a new row needs a client question);
  - KP1–KP9 and the zero-hazard roles;
  - the Clearway and `FurnishingNeverSeals`;
  - Remnants: Miasma only, ≤ 1 per room, `roundsLeft` 2.
- **Floor 1:** RT-20 / TR-29 (no hazards of any kind); DH 17.1 (no
  surprise in main-path rooms 0–1 for the solo hero); ENM invariant 6
  (rooms 0–2).
- **Condition budget (C 16):** full at 32. At most 6 harmful conditions
  per theme; 8 of 9 themes are full, the Kennel at 5. Squares apply only
  minor or major conditions.
- **AI:** a one-step integer scorer (ENM 11.1); stupidity floor 7 (never
  enter a hazard that could kill it); scripted wind-ups are deferred
  (ABL 5, ENM 13.2); ABL 2 lists "Lair actions" as a non-goal (lead's
  call).
- **Experience (ENM 16):** a member pays out when killed, Routed and
  fled, Yielded or Cowed.
- **Clock:** floors run 15–20 min. Companions 7.4 (party 4, F4–5, ≤ 20
  min) is the first M2 gate. The terrain package already takes up to
  +1.6 min per floor (RT TN13, 11.3).
- **Theme caps:** RS 8.6 (Choir entry squares and dry islands), RS 8.8
  (Carnival entry; carousel horses never move), RS 8.9 (Supper dry, no
  ice), RS 8.10 (Gut: at most 2 acid-dealing units per encounter,
  besides bile squares).

### 3.2 What the approved documents owe this design

Each is met in section 14.

| # | Owed | Source |
|---|---|---|
| O1 | Per-theme hazard catalogues and floor-scale density, inside segment 3's caps | RT 13 (TR-1), FL 13, RS 13 |
| O2 | Drops, chasms and pits, and what a fall does | RT 6.5.7, FL 13, RS 13 |
| O3 | Bile (RT-13) | RS 8.10, C 11.2 |
| O4 | Teetering counting (RT-14) | C 16, RS 8.0, RT 6.5.5 |
| O5 | Vents (M3 Remnant sources) and `causedBy` for self-acting sources | RT 6.6 |
| O6 | Theme-scale self-acting set pieces ("flooding") | RT 13 |
| O7 | The Hinge and Postern no-hazard radius | FL 13 |
| O8 | No crossable bridge into the Sanctum (LG3) | FL 13 |
| O9 | The Inverted Cathedral's lethal-safe drop | RS 8.12 |
| O10 | Kennel staked pits | RS 8.4 |
| O11 | Wrecker value by hazard density | Vanguard 8.3, 18 |
| O12 | Hazard-edge coverage from floor 2 (Ledgewalker) | trinkets 23 |
| O13 | DEP-A1 interactive features | Arcanist 16, 19; TR-21 |
| O14 | ENM 17.5's "a chasm edge for shoves" | ENM 17.5; RT 6.5.7 |

### 3.3 Tensions found and their resolutions

| Id | Tension | Resolution |
|---|---|---|
| HT1 | Dynamic hazards against "squares never change" (main 2, C 11.1) | M2 Stirs change no square. Square-changing hazards (Tides, Flips, the Turning Ring, rails) are M3 under Q3, generalising the approved boss phase sockets (RT 6.11) **(agreed, Q3)** |
| HT2 | Telegraphed rhythms against the one-step AI | Acts resolve at round end, before any mover's next turn: a one-step quantity, like `onTurnEnd`. Due squares are hazards for pathing |
| HT3 | Room rhythms against the ABL 2 "Lair actions" non-goal | A Stir has no creature, no choice and no initiative slot: it is a clock. Flagged to the ABL owner as a reading |
| HT4 | Moving architecture against static proofs (FL 9.1, RS 7.6, FL-15) | Every state is pre-proven by V10. No door ever locks or seals; the `fleeExit` is never touched. Moving dream content is M3 at the earliest **(agreed, Q3)** |
| HT5 | Light hazards against "no light model" (RS 7.6, ENM 5.1) | Later with NM-9. Candles are a dial, never light |
| HT6 | Drops against the flat level, lethality and the economy | Void + Dreamfall + Snag + FallFloor; an ENM 16 experience trigger; loot on the edge (ENM 15.6) **(agreed, Q2)** |
| HT7 | Floor 1 | RT-20 kept. Landmark ticks are a non-hazard class (told plainly). Damaging content only at Nightmare tier 2+ **(agreed, Q4)** |
| HT8 | The condition budget | No new condition ids. Everything reuses the theme's 6 or the universal tax |
| HT9 | The clock | At most one signature Stir per floor in M2 (toy: +0.26 min per Stir floor, read as on top of static density's +0.23; 10, told plainly). A segment-4 allowance of +0.3 min inside the terrain tolerance, split into a static and a Stir share, with a combined terrain-plus-hazards gate. The cut order is extended (HZ-27) |
| HT10 | Caps undefined for self-acting hazards | Each Dream-step act is one "use". The EnvRound cap bounds stacks. `NoCheapKill` is extended. On-kill hooks never fire from an act |
| HT11 | Entry caps under changing terrain | V10 proves every state |
| HT12 | Segment-5 ownership collisions | The HZ-1 line. Visible rhythmic twins only; the sprung devices stay traps |
| HT13 | "Acid units" against "acid sources" | The RS 8.10 change row (HZ-5) |
| HT14 | The Dreamer's identity and reserved audio | Nothing shows the Dreamer (RS 7.5). New RC7 cues are flagged (HZ-29) |
| HT15 | Save state | `hazardDigest`; new save fields for Stir phases, Tide states, Cord uses and Mood stage |
| HT16 | Out of combat | The DH 10.4 floor holds. Stirs are dormant out of combat, except in Dare rooms and Landmark ticks |
| HT17 | Teetering counting under Voids | HZ-6 (RT-14 "does not count", kit and boss guards) |
| HT18 | Tides against RS 8.6's entry caps and dry islands | Every Tide state is proven against RS 8.6 (HZ-18) |
| HT19 | Ownership wording between segments 3, 4 and 5 | HZ-1's rows, applied by each owner |

### 3.4 Terms

| Term | Meaning |
|---|---|
| **Hazard** | Visible room content that acts by a rule (entry, turn end, edge save, Dream step, or an Operated Cord) and is never found by a check or disarmed (6.1) |
| **Static hazard** | A segment-3 class (contact piece or face, harm patch, Keg, condition square including bile) placed at floor-scale density |
| **Void** | A non-walkable, condition-free drop square, in regions of 2–10 squares; a hazard (6.4) |
| **Dreamfall** | What happens to a party unit that fails a Void save: the fall band (floored at 1 HP), then Prone on the Snag, then Groggy (6.4) |
| **Snag** | A proven landing socket on the party side of a Void room, primary and secondary, one pair per room (6.4) |
| **Clutch** | An elite's or group leader's once-per-fight save from falling (6.4) |
| **FallFloor** | Fall damage never takes a party unit below 1 HP, in or out of combat (6.4) |
| **Groggy** | A placement state after a Dreamfall: no reactions, no Portal exit, until the end of the unit's first turn that starts after the placement. Shown as a pip; not a condition (6.4) |
| **Fall band** | The contact band (the harm band where 6.11 item 5 requires it), typed by the Void's skin (6.4) |
| **Dream step** | The C 5.4 round-end environment step, used by Stirs (6.3) |
| **Stir** | A pre-placed footprint with a rhythm and one closed effect list, acting in the Dream step (6.3) |
| **Footprint** | A Stir's fixed set of squares, chosen at generation |
| **Latent footprint** | A footprint while it is not due: drawn as a thin outline at hazard tier from reveal (6.18) |
| **Countdown (C)** | A Stir instance's integer count of round ends until its next act (6.3) |
| **Due square** | A footprint square whose act resolves at the end of the current round (C = 1 at round start) (6.3.1) |
| **Act** | One Stir resolution at the Dream step |
| **Tide** | A Stir that toggles pre-proven square sockets between states (M3, Q3) |
| **Cord** | A Low piece face or wall socket with one verb: a **bound Cord** re-times or turns one Stir; a **standalone Cord** has its own effect (6.3.4) |
| **Clock** | Any record that shows a countdown digit or meter on the map; at most one per room (6.2.2) |
| **Operate** | The object action that uses a Cord (6.3.4) |
| **Lurch** | Environment forced movement used only by Stirs (M3, 6.3.3) |
| **Handhold** | A piece whose adjacent squares hold fast against Lurch and the Heart quake |
| **Warned or floored** | A Stir act cannot drop a party unit below 1 HP unless the unit is Warned: in its last turn it could have stepped away, or walked in of its own will (6.3.2) |
| **EnvRound cap** | The per-round cap on environment damage to one party unit (6.11) |
| **EnvDC** | The environment's save difficulty by floor (6.3.5) |
| **Landmark tick** | A harmless rhythm or dial; **not a hazard** (6.1) |
| **Mood** | A floor-wide integer stage 0–2, Omen or Nightmare only (6.9) |
| **Dare room** | An optional, signposted showcase side room (6.10) |
| **Plunge** | An out-of-combat one-way void well to a deeper main-path room (6.10) |
| **Tableau** | A named static hazard composition in a theme's signature slot (6.2) |
| **Hazard points** | A room's hazard budget unit (6.2) |
| **Clock room** | A room holding a clock (6.2.2) |
| **Void room** | A room holding a Void region |

---

## 4. How other games handle environmental hazards

Confidence: high for Into the Breach, Brogue chasms, Frosthaven and
Pathfinder 2E complex hazards (web-confirmed); otherwise from knowledge,
marked *(recalled)* where it is load-bearing.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Into the Breach | Environment events (tides, lightning, conveyors, chasms) marked a turn ahead on exact tiles; fixed resolution order; hits both sides | Announce one full round ahead on exact squares; resolve at one fixed step; symmetry is the fun; pushes into kill tiles | Effects that are a flat tax on one side |
| Hoplite | Lava kills anything; deterministic pushes | One removal tile plus pushes is enough depth | — |
| Enter the Gungeon / Hollow Knight | Pits: the player returns to the edge, damaged; enemies are removed | Asymmetric fall: the party relocates, monsters are removed (Dreamfall) | Returning to the same edge (no tempo cost) |
| Brogue | Chasms drop you a level; machine rooms; vents on a rhythm; deep water loses items | Falls as tense relocation; Dare rooms; vents | Item loss; cross-level falls in combat |
| DCSS | Shafts relocate; warnings on risky moves | Prompts and lethal tags | — |
| Baldur's Gate 3 | Shove off ledges, barrels | Falls are the most satisfying verb | Shove-trivialised elites (hence Clutch); unannounced enemy shoves |
| Hades | Traps hurt enemies; trap-damage boons | Class ownership of the environment | — |
| Gloomhaven / Frosthaven | Flat hazardous-terrain damage; Rising Brine water moves each round | Moving-water fronts are fun; per-scenario special rules become one rule per theme | Upkeep and forgotten rules (the engine does it) |
| Pathfinder 2E complex hazards | Routines in initiative; partial disable | The Stir period and effect list; Cords as partial disable | Hidden complex hazards (segment 5 or Later) |
| D&D 5E lair actions | Initiative-20 slot | A fixed slot | Mid-round slots, unfair by initiative; ours resolve at round end |
| D&D 4E terrain powers *(recalled)* | "Pull the chandelier rope" | Cords; the bloodied threshold idea (rejected for Clutch after the toy) | — |
| XCOM 2 / Divinity: Original Sin 2 / Noita | Random spread, surface soup | — | Any random or organic spread |
| Invisible Inc / Spelunky ghost / Dredge | Escalation meter with known thresholds | Moods with visible thresholds | Wall-clock pressure (fights auto-walk and the floor clock) |
| Darkest Dungeon | Curios with known odds; torch meter | Optional risk/reward rooms; a light meter Later | — |
| The Binding of Isaac curse rooms | The hazard is the entry price | Dare rooms | — |
| Blasphemous / Fear & Hunger | Instant-death pits and coin-flip deaths | Dread through rhythm | Instant death of any kind |
| Inscryption / Mario + Rabbids | Boss-board rules; a neutral chomp that bites whoever is near | One rhythm per boss (segment 9); Chainposts | Third-faction AI |
| Triangle Strategy / Tactics Ogre | Readable elemental terrain per map | Theme Law as terrain identity | — |
| OSR practice (information, choice, impact) | "Telegraph, then punish" | The fairness test for every entry | Random lethality |

**Lessons that bind the design:**
1. One fixed, displayed resolution step, with no draws except saves.
2. Announce at least one full round ahead.
3. Both ways, or cut it.
4. Never instant death; relocation beats death.
5. Authored states only.
6. Few, loud, themed hazards beat many quiet ones.
7. Floor-wide pressure only as a visible meter.
8. Bound removal on elites and bosses.
9. No item loss.
10. One rule per theme.

---

## 5. Hazard models

### 5.1 The models

- **Model A, static density.** Tableaux, Void/Snag/Clutch, Contraptions,
  struck vents; nothing acts on its own.
- **Model B, environment turn.** The Dream step, Stirs, Fair Warning
  (adopted here as **Warned or floored**), Tides.
- **Model C, floor states.** An "Unrest" state bound at reveal that
  changes room layers.
- **The lead catalogue.** A Stir/Tide/Cord/Mood kit per theme.
- **N1–N16, new mechanics.** Sixteen low-cost mechanics (9.1).

### 5.2 Verdict and judges' scores

Three judges (player and dynamics; rules and AI; art and production)
scored every proposal. Scores are the mean of the three, rounded to one
decimal (10 = best; "low build cost" is scored high when the cost is
low).

| Model | Interest | Fairness | Readability | Theme | Determinism and AI | Pacing | Fit with docs | Low build cost | Sum |
|---|---|---|---|---|---|---|---|---|---|
| **A: static density** | 5.7 | **8.7** | **9.0** | 5.7 | **9.3** | **8.0** | **8.7** | **8.0** | **63.1** |
| B: environment turn | 8.3 | 7.3 | 6.0 | 8.3 | 7.3 | 5.0 | 5.0 | 3.7 | 50.9 |
| C: floor states | 6.7 | 7.3 | 6.3 | 7.3 | 8.3 | 6.0 | 5.7 | 4.0 | 51.6 |
| Lead catalogue | **9.0** | 7.0 | 6.0 | **9.7** | 7.3 | 5.0 | 5.0 | 3.0 | 52.0 |
| N1–N16 | 8.7 | 8.3 | 6.7 | 8.0 | 7.7 | 5.7 | 6.0 | 4.3 | 55.4 |

**Adopted: Model A+ (lead's call; stands as written).**
- **Chassis: Model A.** Budgets, placement, Void, Snag and struck vents.
- **Grafted on top:**
  - the Dream step from B and N1, limited to **one signature Stir room
    per F2–5 floor** in M2 (gated) **(agreed, Q1)**;
  - N1's **Warned or floored** rule, with the definition tightened by
    the balance critique;
  - the lead catalogue as the content pool, released wave by wave;
  - the toy's fixes F-a to F-k;
  - the critics' corrections (Appendix A).

All three judges converge on this blend, and the toy calls it the best
measured one:
- its chassis is the cheapest model on the clock (static density alone
  measured +0.23 min per floor) and fully inside approved documents for
  M2;
- Voids are the only measured lever on TN6;
- one Stir per floor buys most of B's theme expression for +0.26 min
  per Stir floor in the toy (read here as on top of static density).

**Told plainly:** in M2 the brief's dynamic vision shows up mostly as
at most one breathing room per floor, on the Choir and Carnival floors.
The rich dynamic catalogue arrives at M3. **Told plainly after the
check:** read as additive, the toy's static and Stir costs come to
about +0.49 min on a Stir floor, above the +0.3 allowance, so the Stir ships
in M2 only if the reading-time playtest measures it well under the toy
(10).

### 5.3 Grafts

**(lead's call; stand as written)**

| Graft | From | Why |
|---|---|---|
| Dream step at the C 5.4 round-end step | B, N1 | One fixed, displayed slot; no initiative unfairness |
| One signature Stir per F2–5 floor in M2 | toy, judges | Theme expression at +0.26 min per Stir floor (toy) |
| Warned or floored, with "had a legal move" | N1, balance critique | Tail insurance: 41–50% of hero Stir hits were unwarned |
| `firstAct` ≥ 2 | consistency critique | Round 1 is never touched; ENM 6.4 round-1 rows unchanged |
| Void with Dreamfall to a Snag, not the nearest safe square | A, toy | A fall costs about 1.6 rounds; the nearest safe square costs 0 |
| Clutch once, with the "after its turn" hold; half-HP Clutch rejected | balance critique, toy | No round-1 elite removal; half-HP halved removals for no safety |
| Void-fall save ignores Teetering's disadvantage | fun critique | Bounds the Wrecker's Brink loop without a Vanguard row |
| Perimeter and flank Voids (F-b) | toy | Mid-room Voids lengthened fights |
| EnvRound cap as a rule | balance critique | The toy's 52% one-round stack |
| SC13 footprint value and SC14 payoff verb | fun critique | Stirs otherwise ignored |
| Struck vents by default | A | Event-driven; no clock |
| Moods Omen and Nightmare only | toy | Per-room ramps raised F2 deaths 4.7% → 7.1% |
| Landmark ticks as a non-hazard class on floor 1 | consistency critique | Teaches the interface without reopening RT-20 |

---

## 6. Hazard rules in full

### 6.1 Taxonomy and the line with segment 5 (lead's call; change HZ-1; a reading of RT 13 / TR-1, FL 13, RS 13)

| Family | What | Acts when | Owner | Wave |
|---|---|---|---|---|
| **Static hazard** | Segment-3 classes (contact pieces and faces, harm patches, Kegs, condition squares including bile) placed at floor-scale density | Entry, turn end, edge save | Classes are segment 3's; density is segment 4's | M2 |
| **Void** | Non-walkable drop region | Edge save | 4 | M2 (gated, Q2) |
| **Stir** | Pre-placed footprint, a rhythm, one closed effect list | Dream step | 4 | M2 (gated, Q1): one per F2–5 floor; M3 broad |
| ↳ **Tide** | A Stir that toggles pre-proven square sockets | Dream step | 4 | M3 (Q3) |
| ↳ breathing vent | A Stir whose effect emits a Remnant | Dream step | 4 | M3 |
| **Struck vent** | A face that emits a Remnant when struck | Inside an action | 4 | M3 |
| **Cord** | A piece with one verb, used with Operate: bound to one Stir, or standalone with its own effect (6.3.4) | Inside an action; any damage or unit movement at a later Dream step | 4 (piece class from 3) | With Stirs |
| **Landmark tick** | **Not a hazard.** A harmless rhythm or dial: doors, a mute round, countdowns, a startle. No damage, no condition, no unit movement | Dream step or out-of-combat tick | 4 (hooks to 5, 6, 8) | M3 |
| **Mood** | A floor-wide integer stage | Named events between fights | 4 | M3, Omen and Nightmare only |
| **Dare room** | An optional showcase side room | — | 4 (reward 7, events 8) | M3 |

**The line with segment 5 (lead's call):**

| Test | Terrain (3) | Hazard (4) | Trap (5) |
|---|---|---|---|
| Visible | Yes | Always, from reveal; rule card on hover | Hidden until detected, or a known armed device |
| Acts | Never on its own | Entry, turn end, edge save, Dream step, or an Operated Cord | Sprung by a trigger square or tripwire |
| Checks | None | None to find. Never disarmed: avoided, timed, plugged, Operated | Detect, disarm, re-arm |
| Lethality | Bands, the instance cap | C 10.4, the Stir charter, the EnvRound cap, Warned or floored | DH 10.4 undetected floor; detected traps tagged |

**Grey cases:**
- An **open** trapdoor or shaft is a Void (a hazard). A **closed**
  trapdoor that springs is a trap. No unit falls at the spring; from F2,
  in a V10-proven Void room (`SnagProven`) of a theme with a Void skin,
  its squares then open as an ordinary Void (6.4); on F1 and elsewhere
  it recloses (traps and locks design 6.3; change TL-31; M2).
- A visible device with a countdown, meter or rhythm is a Stir. Its
  hidden or sprung twin stays segment 5's: the toppling-shelf tripwire
  (the Leaning Stacks are its Stir twin), the dumbwaiter drop (the
  Courses), the sluice one-shot Soak (the Sluice Wheel Cord), the icicle
  chandelier, the acid geyser, trapdoor stages, the bell tripwire, the
  portcullis, the Appeals trapdoor and the Kennel horn tripwires. These
  sprung twins are segment 5's traps; the Stir and Cord twins named here
  are unchanged (traps and locks design 6.3, 7; change TL-10).
- A Cord that arms or disarms a trap is segment 5's. **Cords never arm
  traps.**
- A horn post anyone may blow is a Cord (or a Landmark tick on floor
  1). A horn tripwire is a trap.
- Known armed traps remain hazards for C 10.1–10.4 **(settled, C 11.3,
  SH-15)**. That is not a transfer of ownership.
- **Boss counterplay features stay segment 9's** (RS 13): the
  Carillon's sluice and bell rope, the Kennel gate, the Jailer's
  pendulum. Segment 4 supplies only the Stir and Cord machinery.
  Non-arena look-alikes get distinct skins and names (the Choir's
  **hymn bellows** and **clapper chain**, never "the bell rope").
  Arena trap devices are segment-9 instances in segment 5's TrapRecord
  format (traps and locks design 7.8; change TL-40; with segment 9).
- The FL owner is told that a sluice *wheel* is a piece, not the FL 7.5
  sluice-gate Door skin.

### 6.2 Density and placement (change HZ-2, HZ-3)

#### 6.2.1 Hazard points

**Hazard points per fight room** *(illustr.)*. A layer on top of RT
6.2.4; KP1–KP9 and segment 3's class caps still bind.

| Feature | Points |
|---|---|
| Contact piece or face beyond KP1's mandatory one | 1 |
| Harm patch (2–8 squares) | 1 |
| Condition patch beyond the home-theme base share | 1 |
| Keg | 1 |
| Struck vent | 1 |
| Void region (2–10 squares) | 2 |
| Stir (with its Cords) or breathing vent | 2 |
| Chainpost | 2 |
| Tide | 3 |
| Landmark tick | 0 |

| Floor | Room budget | Notes |
|---|---|---|
| F1 | 0 | Landmark ticks only (M3), ≤ 1 per room, main-path index ≥ 3. Nightmare tier 2+: 6.19 |
| F2 | 3 | |
| F3 | 4 | |
| F4 | 5 | |
| F5 | 5 | |
| Elite room | +1 | |
| Dare room (M3) | ×2 | |
| Arena | segment 9, inside RT 6.11 | + at most 1 Stir and 1 Void region (HZ-11) |

#### 6.2.2 Room roles

| Role | Rule |
|---|---|
| Entry, Stairhead, corridors, Bypass arm, Alcoves, non-combat rooms, the F2–5 Forecourt, the Circuit `coverRoom` | 0 **(settled, RT 6.2.4)** |
| Circuit rooms (patrol fight location varies) | Static only, ≤ 3 points; no Void, no Stir |
| The floor's route-avoidable guard post | −1 point; no Void, no Stir (keeps the stealth bypass clean) |
| Main-path rooms 0–1 of any floor; the floor's first fight room | No Void, no Stir |
| Other guard posts, lairs | Stirs and Voids allowed from F2 (M3: Lurch Stirs and Tides from F3). The floor-1 Nightmare packages of 6.19 are the only exception (lead's call, after the check) |
| Elite room | Preferred host of the floor's signature Stir (the KP9 signature slot) |
| Hinge and Postern squares | No hazard on the square or its 8 neighbours; no Void within 2 (fills FL 13's undefined radius) |
| Antechamber–Sanctum interface | No Void or crossable hazard region between them (LG3) |

**One clock per room:** at most one clock per room in M2 and M3, boss
arenas included. It is never waived, not even in Dare rooms. M4 may
test two at a Nightmare tier. **A clock is any record that shows a
countdown digit or a meter on the map (lead's call, after the
check):** a Stir (Tides, breathing vents, the Leaning Stacks and the
Alarm Clock included), a Chainpost, a standalone Cord that resolves as
Now (6.3.4), and the Landmark ticks with a
rhythm or dial (the Small Hour Strikes, Guttering Candles). Remnant
countdowns, Cord recharges and uses, and vent trigger counts are drawn
as **pips, never digits**, so a digit on the map always means "the
room's clock". A Wager room's Disturbance track is that room's one
clock (traps and locks design 6.15; change TL-29; M3). A Windfall's
countdown digit is its room's one clock: a Windfall host holds no other
clock, and a clock Landmark is never placed in a `windfallHost` room
(loot sources design 4.7.2, 4.7.4; change LP-4; M3). **Voids are
static, not clocks**, but a Void never shares a room with a Lurch Stir,
at any tier. In M2 a room holds at most one
Void region (lead's call, after the check).

**Tableaux (lead's call; HZ-8).** A Tableau is a named, static
composition of segment-3 classes that fills a theme's KP9 signature
slot in M2: the Drowned Nave, the Carrel Rows, the Fire-Eater's Pit, the
Laid Table, the Bile Low and the Thorn Garden (7). A Tableau is authored
as layer sockets with a rules key (RT 6.2.5), spends the room's hazard
points like any other content, and is the art team's per-theme
set-dressing brief for M2. The RS 8.0 theme record gains a
`hazardKit` field listing its Tableaux.

#### 6.2.3 Floor quotas *(illustr.)*

- **M2 (gated, Q1):** at most one signature Stir room per F2–5 floor.
- **M3:** clock rooms ≤ 40% of a floor's fight rooms: F2 1, F3 1–2, F4
  2, F5 2 plus the arena.
- **Voids:** only in drop themes. M3 target ≤ 40% of non-patrol fight
  rooms in a drop theme (the Inverted Cathedral ≤ 50%); the M2 pilot as
  in 6.4.
- The first clock room and the first Void room of a run each show a
  one-time `lessonTag` card.
- **Reservation:** new tags `clock` and `void` after `cluttered` in the
  RT 6.2.5 reservation order, so every quota is constructive on every
  seed **(change HZ-2)**.

**Party size:** hazards never scale with party size: not count,
footprint, band or period. Footprints scale with room size only, and FL
already ties room size to the party band. The ENM 6.4 guards run with
hazards present at every party size, solo on F2–5 included (CMP-8)
**(settled)**.

#### 6.2.4 Placement rules VP1–VP10 (V10; change HZ-3)

- **VP1. Entry:** KP5 extended. No condition patch or bile within 2 of
  `partyEntrySquares`. No Void, vent mouth, footprint or Cord within 3.
  "Within n" in VP1–VP10 always means path distance ≤ n from every
  `partyEntrySquares` square (or from the named squares).
- **VP2. Clearway:** never Void, bile or fire. A Clearway square borders
  a Void on at most 3 squares (auto-walk never edge-hugs). 2 wide in
  every state.
- **VP3. Enemy starts:**
  - ≤ half on or adjacent to a condition patch;
  - ≤ 2 start squares within 2 of a Void edge;
  - every start zone has a hazard-free path to the party side ≤ the
    shortest path + 2;
  - no Billet hold square within 2 of a Void edge or lethal hazard
    (encounter placement design 6.6.3, 6.6.8; change EP-21; M2).
- **VP4. Push lanes:** every Void region and contact piece has a push
  lane (`PushLaneExists`): a pair of adjacent walkable, non-hazard
  squares, pusher square P and target square T, such that a push from P
  through T under C 10.1's direction rule (ties broken in fixed square
  order), with no other unit on the path, reaches the hazard within 3
  steps, in every state. Segment 6 seats shovers on front sockets with a
  push lane, within VP3 and the shover caps (a Terrain Pairing;
  encounter placement design 6.6.8; change EP-21; M2).
- **VP5. Void geometry:** the room's perimeter third, or 1-wide flank
  strips beside the Clearway, never mid-room (F-b). Runs ≤ 2 wide
  wherever a Jump crossing is offered.
- **VP6. Hide spots:** never adjacent to a Void, never in a Stir
  footprint, never a Tide socket (Shade parity). Segment 6's nook
  sockets obey the same rule (encounter placement design 6.6.1; change
  EP-21; M2).
- **VP7. Boss lanes:** no Void within 3 of a boss start zone or boss
  lane (F-c).
- **VP8. Flee and escape:** `fleeExit`, `PartyEscape` and
  `NoSealedPocket` hold in every state.
- **VP9. Themes:** home square kinds only (RS 8.x). Bile only in the
  Gut. Voids only in drop themes (6.4's list; the Wake only at
  Nightmare tier 2+). The Supper has no condition squares.
- **VP10. Planks** may cross a Void. A Plank failure stops before it,
  with no fall **(settled, RT 6.2.6)**. Forced movement onto a Plank
  over a Void treats the Plank as floor.

Traps add their own rules TP1–TP12 (V11) on top of these: no trigger
square on a Snag, a Stir footprint, a Plank or a Hinge or Postern
radius, or within 1 of a Void edge (2 on F2–3) (traps and locks design
6.20; change TL-20; M2).

### 6.3 The Dream step, Stirs and telegraphs (agreed, Q1; change HZ-12 to HZ-15; M2 gated, else M3)

**The slot.** This is the C 5.4 round-end environment step (TR-27),
brought from M3 to M2 for Stirs only **(agreed, Q1; change HZ-12;
reopens agreed RT 0.1 item 5 / RT 6.6 and TR-27's wave)**. It runs
after the last turn-end step and any boss actions that follow it, and
before the next round. It shows on the initiative bar as a neutral
**Dream card** carrying the room's countdown. The Dream step that ends
round R **belongs to round R** (for the EnvRound cap, the guard rows and
reports). Conditions it applies count as applied outside the unit's own
turn (C 5.2). Order inside the step **(sub-steps: lead's call, after
the check)**:
1. **Acts.** Each Stir with C = 1 acts, in `stirId` order (at most one
   per room):
   - **1a.** Per unit on the footprint, in fixed square order: the save,
     then damage through the pipeline of 6.11 item 2, then the
     non-damage parts (Taint, Startle).
   - **1b.** Forced movement (Lurch) and Dreamfall placements, in the
     same order (6.4).
   - **1c.** Damage events and downs land as the damage lands (C 9.3);
     a zero-HP sweep follows 1b.
   - Then each Stir that acted raises `actsDone` and sets C to its
     period (or is spent at `maxActs`); every other live Stir's C goes
     down by 1 (Timing, below).
2. **Combos** they trigger, in RT 6.8 order (K1, K2, K4, K5; **never
   K3**).
   - **2b.** The morale checkpoint (ENM 14.3's FIFO: removal events,
     Startle) and experience credit (`voidFall`, kills).
3. **Remnant countdowns** (M3), skipping Remnants created this round.
   - **3b. Windfall hops** (M3; loot sources design 4.7.4; change
     LP-4). Each awake Windfall Truant hops up to `hop` squares along
     its route and its C falls by 1 (the round-1 step only stands it
     up). The first blocked hop holds C once (the Turn slip precedent,
     below); on any later blocked hop C falls anyway. At C = 0 it gets
     away.
4. **New announcements:** every footprint whose C is now 1 becomes
   **due squares** for the coming round, with its announcement sting
   (8).

No draws at act time except saves, which use the combat stream.

**The M2 pilot (agreed, Q1).** The Choir's Organ Swells is the first
choice; the Carnival's Pit Breathes is the alternative. They are the
only two M2 Stirs, each in its theme's signature room, at most one per
floor. **New lead's call (revised after the check):** both are built
to the playtest (the art budget's 2 Stirs, 8); the Organ is measured
first. **Each kind ships if its own theme gates pass** (`FootprintValue`,
its spike row — the Choir lightning spike row or the RT 7.7
flask-then-breath guard — and `StirBothWaysShare` on its theme) while
the shared gates pass, so an M2 run has one Stir floor or two, and
never more than one new rhythm per floor. If a shared gate fails, both
fall back to M3. (This document's draft shipped one kind only; that
left about a third of runs with no Stir at all, against Q1's "one per
floor".)

**The ship gates for the M2 Stir:**
- **the client's gates (agreed, Q1):**
  - the reading-time playtest (6.18's glance test and the measured
    seconds per due round, replacing the toy's 4-second assumption);
  - the TR-27 written report pass (RT 6.6), brought forward for the
    Dream step alone;
  - the fight-length gate: `StirRoundsDelta` ≤ 0;
  - the clock gate: `HazardMinutesDelta` ≤ +0.3 min on Stir floors,
    with `StirMinutesDelta` (the Stir's increment over static-on) ≤
    +0.1 min *(illustr.)*, and the combined terrain-plus-hazards gate
    of 10 (the split and the combined gate: lead's call, after the
    check);
- **added by the lead (lead's call; stands as written):**
  `StirBothWaysShare` ≥ 30% (definitions in 11.4).

**The record.** Content and generation-time instance data are split
**(lead's call, after the check; 11.1)**:
```
StirKindRecord { id, theme, hostKind, period (2–4; Lurch ≥ 3), firstAct (≥ 2), maxActs (4 illustr.),
  parts[] (closed list), bandClass {Harm, Contact}, isSignature, save {type, EnvDC by floor band},
  lurchForm? {Benign, Hazardous}, payoff (SC14), holdSlip (1; the Alarm Clock 3),
  tell {glyph, latent outline, announcement sting, act cue, idle loop, act anim ≤ 1.0 s},
  roles[], floors[], minTier?, ruleCard }
StirInstance { id, kind, roomId, hostSocket, footprint (SquareRef[], fixed order), cordIds[], phaseOffset }
```

**Timing (the countdown; lead's call, after the check).** Each Stir
instance holds an integer **C**, the number of round ends until its
next act:
- C is set to `firstAct` at the start of round 1 (plus 1 if the party
  is Surprised), so round 1 never has an act and the ENM 6.4 round-1
  rows are unchanged by construction (`firstAct` ≥ 2 everywhere; Model
  B's phase 1 is rejected).
- The footprint is **due** during a round if and only if C = 1 at that
  round's start. Step 4 of the Dream step announces it at the end of
  round R−1; step 1 resolves it at the end of round R. Every unit gets
  exactly one turn between warning and act, whatever its initiative.
- After step 1, a Stir that acted sets C to its period (or is spent
  once `actsDone` = `maxActs`); every other live Stir's C goes down
  by 1. Acts therefore fall at the end of rounds `firstAct`,
  `firstAct + period`, and so on.
- `maxActs` counts resolved acts only. A held act is not an act.
- The countdown digit shows C. It sits on the hazard face's floor decal
  on the host's front square (RT 8.1, "hazard faces read from the
  floor") and on the Dream card, never only on a wall or Tall host
  (8). Out of combat it reads "every 3".
- The phase is fixed and shown on reveal ("Swells at the end of rounds
  2, 5, 8"), and redrawn whenever a Cord or Stoke shifts it.
- Runtime state per instance: `{C, actsDone, holdUsed,
  nowUsedThisRound, warned flags}` (11.1).

**Closed part list:**

| Part | Rule | Wave |
|---|---|---|
| **Strike** | Typed damage on the footprint. Area footprints use the harm band; a line or face strike of ≤ 3 squares, and the floor's signature Stir, may use the contact band, always under the EnvRound cap and only if it passes `ContactLethalityBound` after weakness (6.11 item 5). Save for half. Rider-free, never arcs. The damage-end row and setup payouts apply | M2 (gated) |
| **Taint** | One minor or major condition from the theme's 6. Save negates. Source `environment`; counts in the per-fight cap (C 5.7, 11.6) | M3 |
| **Lurch** | Environment forced movement (6.3.3) | M3 |
| **Portal** | Unoccupied, unlocked doors or drapes in the room close or open. Never locks. Never the `fleeExit`. Held while occupied | M3 |
| **Mute round** | Party-*verb* noise (Smash, Shunt, Kick, loud checks) in the room is not propagated until the end of the next round. Combat-round noise is unaffected, so ENM 12.3 and the FL hearing proofs stand | M3 |
| **Startle** | The new ENM 14.3 morale event (9.2) for named groups in the room | M3 |
| **Emit** | One Miasma Remnant (breathing vent) | M3 |
| **Turn** | Toggles Tide sockets between the Tide's 2 whole states (Q3). **Held whole (lead's call, after the check):** if any socket that would change to a non-walkable kind, or to a kind the record flags `holdIfOccupied`, is occupied at Dream step 1, the whole Turn slips one round and is re-announced (a Windfall's
blocked hop extends this precedent: it slips once, then C falls; loot
sources design 4.7.4; change LP-4; M3). Sockets changing between walkable kinds (dry to water) change under units, and that is not an entry (HZ-18). So only the 2 proven states ever exist | M3 |
| **Overturn** | A tippable Tall Furnishing falls along a pre-proven strip (rides RT's M3 Overturn) **(new lead's call:** renamed from the proposal's "Topple", the weapon verb**)** | M3 |

**Never:**
- hard control;
- a square change outside Turn, Overturn and the sanctioned K1/K2
  burn-aways;
- an effect on a Downed unit;
- starting K3 Conduct;
- a choice at act time;
- footprint squares within 3 of party entry, on Portals, the
  `fleeExit`, Hinge or Postern squares, Snags or Hide spots;
- a footprint covering a full cross-section of the Clearway, or the
  whole room (every footprint leaves walkable non-footprint squares
  that its units can reach: there is no room-wide Stir);
- acting out of combat (except Dare rooms and Landmark ticks).

**The Stir charter SC1–SC14 (lead's call; extends RC1–RC9).** Every
Stir must pass all fourteen:
- **SC1.** Visible from reveal, with a one-line rule card.
- **SC2.** Announced one full round ahead.
- **SC3.** Damage ≤ the contact band, with a save, inside the
  one-instance cap and the EnvRound cap.
- **SC4.** Both ways. No immunity without a visible tag.
- **SC5.** No hard control. Conditions from the theme's 6 only.
- **SC6.** Breaks stalemates or amplifies. Never a "wait it out"
  hazard.
- **SC7.** Changes the best square.
- **SC8.** One line of fiction tied to the theme's Law or Obsession.
- **SC9.** Dormant out of combat.
- **SC10.** ≤ 1.0 s of animation per step, batched, skippable.
- **SC11.** None damaging on floor 1 in the base game (6.19 for
  Nightmare tier 2+).
- **SC12.** One reserved RC7 cue pair (an announcement sting and an act
  cue), in a timbre family no music bed uses (8).
- **SC13. Footprint value.** The footprint covers at least one of the
  room's valuable squares: a cover square, a choke square (a cut vertex
  of the room's Clearway graph), a Cord's adjacent square, or a
  Conduct-bait water patch. V10 checks it as `FootprintValue`.
- **SC14. A payoff verb.** Standing in, or putting a monster in, the
  footprint sometimes helps someone: defuse or charge, burn-away freeing
  the Restrained, thawing the Frozen. Strike-only Stirs with no payoff
  are cut.

#### 6.3.1 Due squares (change HZ-13)

Due squares are hazards for:
- C 11.3 pathing, Commanded and Confused movement, and follower and
  companion pathing;
- ENM invariant 7;
- DH 17.5–17.7.

They are **not** hazards for C 10.1's stop-before-hazard rule, the C
10.2 edge save or the C 10.3 landing save (lead's call, after the
check). Pushing or dragging a unit onto a due square, or a Jump ending
on one, needs no save, because the act's own save handles the harm:
one fate, one save. Push, Ram, Give Ground, Gust, Jostle, Heave and
Barge gain a target: "hold him there until the organ swells".

#### 6.3.2 Warned or floored (agreed, Q1; change HZ-14, a new DH 10.4 row)

A Stir act cannot take a party unit (hero or companion) below 1 HP
unless the unit is **Warned** for that act. **Definition (lead's call,
after the check; it makes the agreed rule computable and matches its
intent):** a unit is Warned for a Stir's act at the end of round R if
all of these hold:
- it took a turn in round R (after the announcement), and that turn
  was not lost (C 5.7) and it was not under hard control during it;
- at the end of that turn it stood on a due square of that Stir;
- either it entered the footprint by its own movement during that turn
  (a free choice with the overlay showing), **or** at some point in
  that turn a path existed within its movement budget (C 11.3 pathing;
  provoking allowed; no Jump) to a walkable, unoccupied square outside
  the footprint that is not a hazard;
- it has not been moved by any other unit, Lurch or Dreamfall since
  the end of that turn.

Every other case is **floored**: units that were Restrained, Grappled,
Frozen or at speed 0 all turn; units that lost their turn or had none
since the announcement (joiners, Surprised units); units placed on the
square after their turn ended, by forced movement, Lurch or Dreamfall,
even if they were Warned before. The flag is computed once at the
unit's turn end, stored per unit keyed by `(stirInstanceId, round)`,
and cleared to floored if the unit is moved after its turn. The
preview reads the flag (during the turn it shows the projected value).
The toy found 41–50% of hero Stir hits were of the "unwarned" kind, so
the tail is real.

The rule is **shown**:
- a floored unit on a due square carries a bracketed "1" pip under its
  HP bar and "floored this time" (never a shield, which is the cover
  vocabulary; 8);
- a Warned unit carries the lethal tag.

Monsters get no floor; that is the party's exploit.

Segment 5 follows this rule as the precedent for a shown Hunch zone: a
warning, floored only out of combat, by forced movement or in its first
round (traps and locks design 6.5; change TL-8).

#### 6.3.3 Lurch (change HZ-17; M3; a C 10.1 change)

Lurch is environment forced movement used only by Stirs: the third
named exception class beside Battering Force and verb pushes.
- 1 square (at most 2, taken as single steps) along painted arrows.
  Non-verb. Never provokes. Never passes a Portal.
- **Soft stop:** no collision damage at walls, obstacles, units or
  Downed bodies.
- **Benign form:** also stops before hazards.
- **Hazardous form** (F3+, or on floor 1 at Nightmare tier 2+ for the
  6.19 packages that name it): makes the full hazard-edge save at
  EnvDC. The area cap of 2 counts the first 2 units, in downstream-first
  resolution order, whose step reaches a hazard; units beyond the cap
  stop before the hazard as an ordinary non-early stop (no collision, no
  Teetering). One counter per act (6.4, 6.11).
- There is no collision form: every Lurch is a soft stop (the Stampede
  included; 6.19). The record carries `lurchForm` {Benign, Hazardous}.
- Resolves from the downstream end first.
- **Holds fast:** Rooted and Restrained units **(settled)**, Anchored
  units once Anchored ships (a Later condition, C 18), plus Hidden units
  and units adjacent to a **Handhold** piece ("backs to logs" as a
  universal rule). **Lead's call, after the check:** the proposal also
  listed Guarded, which is a Later bodyguard condition with no meaning
  of stillness, so it is dropped.
- Separation beyond 1 ends a grapple **(settled, C 18.4)**.
- Period ≥ 3. The band or ring covers ≤ 25% of walkable squares.
- **Escalation never takes a period below its kind's floor** (Lurch 3,
  every other Stir 2), whatever a Mood, Omen or tier does (6.9, 6.19).

#### 6.3.4 Cords and Operate (change HZ-15)

A Cord is a Low piece face or wall socket. **Operate** is a new ABL 7.2
object action: one action, from an adjacent square, no check, noise 4
*(illustr.)*. Every Cord has exactly one verb.

**Bound Cords** act on one Stir instance (C as defined in 6.3, Timing;
the verb rules are a lead's call after the check):
- **Now:** if C > 2, C becomes 2: the footprint is announced at this
  round's Dream step and acts at the end of the next round. If C ≤ 2
  it has no effect, and Operate is refused with a preview line ("the
  organ is already swelling"). Never a same-round act. At most once per
  Stir per round.
- **Hold:** C rises by `holdSlip` (1; the Alarm Clock's Wind Back is
  Hold with a slip of 3), and every later act shifts with it. (The
  Windfall's one blocked-hop slip extends this precedent: loot sources
  design 4.7.4; change LP-4; M3.) If the
  Stir is due, its due marks are withdrawn at once and Warned or
  floored is not evaluated that round. Once per Stir per fight.
- **Reverse:** a Lurch direction reverses (M3).
- **Flip (M3, Q3):** a Tide's next Turn is set as Now (C = 2 if
  greater): announced at this round's end, resolved at the end of the
  next round, under the Tide's hold rule (6.3, Turn part).
- **Kick (M3, Q3):** the environment moves a Furnishing along a
  pre-proven rail as a Stir act, announced the same way.

M2 Cords may only re-time (Now, Hold). Flip (a square change) and Kick
(the environment moves a Furnishing) are M3 under Q3 **(agreed, Q3)**.

**Standalone Cords (lead's call, after the check):** some catalogue
Cords have their own effect and no Stir (the Lockdown Lever, the
Curfew Rope and its cleat, the Mourning Cord, the Horn Post, the
clapper chain, the Wheel of Knives, the censer rope). Each has its own
record (verb, effect, timing, recharge, use scope, noise) and one verb.
- **Any standalone Cord that deals damage, applies a condition on
  squares or moves a unit resolves as Now:** its squares are announced
  as due at this round's Dream step and it acts at the end of the next
  round, under every Stir rule (Warned or floored, the EnvRound cap,
  one use per act). It counts as a clock, so it never shares a room
  with a Stir, Chainpost or Landmark tick (6.2.2).
- A standalone Cord that only opens or shuts unoccupied Portals, or
  fires a morale event (Startle), may resolve at once inside the
  action; it touches no unit's square.
- Recharges and uses show as pips, never digits (6.2.2).

- **Use scope:** each Cord record states `perFight` or `perFloor`
  uses (Hold once per fight; the Curfew Rope once per floor).
- **Placement:** on the party's approach ring, at path distance 4–6
  from every `partyEntrySquares` square (KP2's ring, read with VP1's
  "within 3" = "≤ 3").
- **Bonus-action Operate (lead's call; the ABL owner is asked):** a unit
  that starts its turn adjacent to a Cord may Operate it as its bonus
  action.
- **Credit:** an act set by an Operated Cord credits the operator for
  experience and morale. **On-kill hooks never fire from a Dream-step
  act** (RT 6.4: on-kill fires only inside an action).
- **Monsters:** one type per F2–5 theme with the `usesCords` tag (the RT
  6.9 terrain-using enemy). Telegraphed intent, valued through the
  preview, under `NoCheapKill`.
- **Out of combat,** a Cord sets the room's starting phase. It is loud,
  so it can draw joiners.
- **Gate:** `CordUseRate` ≥ 20% of Cord fights, else that theme's Cord
  is cut (no inert dressing).

#### 6.3.5 EnvDC

| Floor | EnvDC *(illustr.)* |
|---|---|
| F2–3 | 13 |
| F4 | 14 |
| F5 | 15 |

Indexed one above RT 6.2.6's Plank difficulty of 12. Segment 5's ladder
cites this: its Standard DC is EnvDC on F2–5 (traps and locks design
6.2; change TL-2). **New lead's call:** floor-1 Nightmare
content (6.19) uses EnvDC 13. The landing save over a Void (6.4) uses
the floor's EnvDC (lead's call, after the check; C 10.3 sets none).

### 6.4 Drops: Void, Dreamfall, Snag, Clutch (agreed, Q2; change HZ-9 to HZ-11; M2 gated, else M3; Cathedral M4)

**Void square** (a new C 11.3 hazard class):
- non-walkable, condition-free, in regions of 2–10 squares;
- crossable by Jumping and Flying **(settled, main 4.3)**; line of sight
  passes over it;
- one cross-theme lip glyph (RC5); depth is backdrop only (LG18),
  rendered as a sealed shaft below the floor plane (8);
- no unit, Furnishing, Keg or Downed body ever stands on it.
  Furnishings and Kegs pushed in are removed silently ("the dream
  swallows it"; Furnishing removal is already sanctioned).
- **Drop themes:** the Silk, Choir, Gut, Carnival, Heart and Cathedral,
  and the Wake at Nightmare tier 2+ only (the Open Grave, 6.19;
  `HazardKitRecord.VoidMinTier`) **(lead's call, after the check)**.

Brink reads Voids, because they are squares **(settled; Vanguard
unchanged)**.

**Forced entry** (push, pull, drag, Lurch) uses the C 10.2 edge save:
Reflex at the pusher's difficulty (EnvDC for a Lurch), square by
square, Stalwart first. The area cap of 2 is **one shared counter per
use** across contact hazards, harm squares and Voids (HZ-9 amends C
10.2's area-cap sentence to "a contact hazard, harm square or Void");
units beyond the cap stop before the Void as a non-early stop (no
collision, no Teetering). The last safe square is the last walkable
square on the path before its first Void square, a Plank over the Void
included (VP10) **(lead's call, after the check)**.
**Teetering's disadvantage does not apply to the Void-fall save
(agreed, Q2; change HZ-9 to C 18.4 Teetering and the C 19 combo
row).** It still applies at contact and harm edges. A Void
removal is always a fair roll, which bounds the Wrecker's Brink loop
without changing Brink.
- **Success:** stop on the last safe square, plus Teetering (if RT-14
  is ruled "does not count"; never on a boss).
- **Failure:**

| Who | Result |
|---|---|
| **Boss** | `noDrop` **(settled)**: takes the contact band, Prone; no Teetering from this; never removed |
| **Elite or group leader** | **Clutch**, once per fight: the contact band, Prone, Teetering, a "holding on" pip. **It cannot fall until it has taken a turn after the Clutch**: a further failure before then deals the contact band again and leaves it on the edge. After its turn, a failure makes it fall. **"Taken a turn" (lead's call, after the check):** `HoldingOn` clears at the end of the elite's first turn-end step that begins after the Clutch, whether or not its act phase was lost (Stunned, Surprised and lost turns still clear it once its initiative slot has passed); a Clutch during its own turn clears at the end of its following turn. A Routed or Yielded elite keeps the hold until then. The pip shows until it clears. ("Champion" is an encounter template, not a rank, so its elite is covered as an elite) |
| **Other monster** | **Falls:** removed and defeated. A new ENM 16 experience trigger `voidFall`, paid as a kill. A morale "removal" event (ENM 14.3). `causedBy` is the pusher, so on-kill hooks fire inside the action. Carried loot lands on the last safe square **(settled, ENM 15.6)**. Summons and Figments: removed, no loot |
| **Party unit (hero or companion)** | **Dreamfalls** (below) |
| Flying | Crosses; immune unless its Flying ends over the Void |
| Downed | Inert; never moved **(settled, DH 11.4)** |

Half-HP Clutch is **rejected**. In the toy it halved removals (in-room
environment kills 4.2–4.7% against 7.3–7.7%) and bought no measured
safety, because falls never downed heroes.

**Dreamfall (party; agreed, Q2):**
1. Takes the **fall band**: the contact band (or the harm band where
   6.11 item 5 requires it), typed by the Void's skin (psychic in the
   Heart, cold in the Drowned Crypt, acid in the Gut's Gullet, piercing
   in the Silk Stacks). **New lead's call (revised after the check):** a
   skin with no named type deals its theme's `primaryDamageType` (RS
   8.0 `pairKey`), and a skin may only use a damage type already in its
   theme's `damageTypes[]` (never radiant before RT-15 is ruled);
   V1/V10 check every `FallType`. **FallFloor:** fall damage can never
   take a party unit below 1 HP, in or out of combat **(change HZ-10, a
   new DH 10.4 row)**.
2. On the failed save the unit **leaves the grid at once**: it is
   untargetable and occupies nothing for the rest of the use. At the
   end of the current use (or at Dream sub-step 1b for a Lurch) it is
   placed **Prone** on the room's Snag (below). Several fallers are
   placed in the order their saves failed. The placement is an entry (C
   11.2), but Snags are never hazards. Grapples end. Remaining movement
   is lost. Action and bonus action are kept, except after a rescue
   push (Deliberate entry, below). Concentration is checked as normal
   damage. Nothing is lost.
3. **Groggy** until the end of its first turn that starts after the
   placement: no reactions, and it cannot leave the room through a
   Portal. A placement state shown with a pip, not a condition, so no
   theme's 6 changes **(new lead's call:** renamed from the proposal's
   "waking", the Heart's Law word**)**.
4. **Once a party unit has taken a Dreamfall in a fight,** a later Void
   failure does not drop it again: it stays on the edge (its last safe
   square), Prone and Teetering, with the fall band floored at 1 HP.
   The rule keys on Dreamfalls actually taken (`FellThisFight` is set
   only by a placement), so a failure caught by Snatch does not count.
5. It is never removed from the board beyond the use, so there is no
   Banished analogue and no lost turn start. A solo hero is always on
   the board.

**Snag** (V10 `SnagProven`):
- two sockets per Void **room** (a `RoomVoidSet`, not per region),
  primary and secondary, each 2–4 squares;
- on the party half of the room (RT's bisector rule), all squares inside
  the room, adjacent to the 3×3 interior of the Portal mouth that faces
  the LG20 landing but never overlapping it;
- ≥ 4 path squares from every enemy start zone; ≥ 3 from any `fleeExit`
  Portal;
- no hazard within 2; not a Hide spot; outside every footprint and Tide
  state;
- never water-connected to an Organ footprint; reachable by Clearway
  pathing from `partyEntrySquares`.
- **The Snag squares carry only the Snag glyph.** Its fiction prop (the
  dry font, the returns desk, the coffin, the wardrobe, the net, the
  chandelier) is wall-mounted or stands on adjacent non-walkable
  squares, never on a Snag square (8).

**Snag choice at placement (lead's call, after the check):**
1. the primary, if no hostile is within 1 of any of its squares and one
   of its squares is free;
2. else the secondary, under the same test;
3. else the **overflow square**: among free, walkable, non-hazard
   squares on the party half that are ≥ 4 path squares from every enemy
   start zone and have no hostile within 2, the one with the least path
   distance to the primary Snag, ties in fixed square order;
4. if no square passes step 3, the free Snag square (primary first)
   with the fewest hostiles within 1.

Within a socket, the first free square in fixed order is used. It is
never "the nearest free square anywhere". The fall preview's sub-line
names hostiles adjacent to the landing (6.18).

**Why the Snag** (it replaces RT 6.5.7's "nearest safe square"): the
toy measured about 1.6 rounds lost per Snag fall against 0 for the
nearest safe square. The Snag is what makes a fall matter.

**Landing failure (change HZ-10, a C 10.3 failure branch):** a Flying,
Jumping or Levitating unit whose state ends over a Void saves at the
floor's EnvDC; a failure is treated as a failed edge save, and its
"edge" (for the second-fall rule) is the nearest walkable non-hazard
square, ties in fixed square order. **No voluntary Jump, Flight or
Levitation may end its movement over a Void square** (lead's call,
after the check), so this branch comes only from forced moves and
states that expire.

**Deliberate entry:**
- Monsters never step in (stupidity floor).
- The party's **rescue push** of an ally is legal behind the DH 17.7
  confirmation. **Lead's call, after the check:** a Dreamfall caused by
  an ally's forced movement lands on the secondary Snag (steps 2–4 of
  the Snag choice), and the faller also loses its next action (Groggy
  as normal); the FallFloor still holds (Q2). It stays a rescue, not a
  free extraction. `RescuePushRate` is reported.
- **Let Go** (a voluntary fall) is **Later**: with the FallFloor it is a
  free emergency teleport.
- **In-combat Swallow** into another room is **rejected**: in the toy
  it cost 3.5–3.9 rounds per fall and doubled hero deaths. Its fiction
  becomes Plunge (6.10).

**Snatch (M3; change HZ-22):** a reaction on the failed-save event, by
a party unit or a monster leader adjacent to the faller's last safe
square at that moment, and not Prone, Restrained, Grappled or Groggy.
The faller stops on that square, Prone and Teetering, with **no
damage**, and the rest of the forced move ends. A Snatched failure is
not a Dreamfall (step 4). For a monster leader, Snatching an elite
whose hold has expired counts as that elite's Clutch for the fight
(timing and reactors: lead's call, after the check). The toy caught
25% of party falls.

**Density and pilots:**
- **M2 (gated, agreed Q2):**
  - Silk "The Stacks Have No Bottom", ≤ 2 Void rooms per F2–3 Silk
    floor;
  - Choir "The Drowned Crypt", F3 only, ≤ 1 room.
- **The Heart is not the pilot:** a Wrecker should not solve floor 5
  with one verb. Heart "The Dark Under the Bed" is M3, ≤ 1 Void room on
  F5 until `FallsByWrecker` and `ClassThemeSpread` pass.
- **M3:** the Gut's Gullet, the Carnival's High Wire, the Heart; up to
  40% of non-patrol fight rooms in drop themes.
- **M4:** the Inverted Cathedral, up to 50%.
- **Trapdoor Voids (change TL-31; M2; touches the agreed Q2 gates,
  approved by the client 2026-11-21):** a segment-5 trapdoor that
  springs from F2 inside a V10-proven Void room (`SnagProven`) of a
  theme with a Void skin opens its squares as a runtime Void. It sits inside that proven room, counts toward these
  Void-room quotas, and from then on is an ordinary Void (edge save,
  Dreamfall to the Snag, `voidFall`). No unit falls at the spring; on
  F1 and elsewhere the trapdoor recloses (traps and locks design 6.3).

**Ship gates for Voids in any theme (agreed, Q2):**
- `ClassThemeSpread` with Voids on: Vanguard–Shade gap in run death
  rate ≤ BASE + 2 percentage points;
- `FightRoundsDelta` ≤ 0 after perimeter placement;
- `DropRemovalShare` ≤ 15%;
- `TeeteringUptimeOnBosses` ≤ 25%;
- `PartyFallsPerFight` ≤ 0.25 *(illustr.)*;
- `PreviewExactness` 100%, act and fall branches included;
- the Shade hazard hook delivered by the Shade owner (6.15).
- **Added by the lead after the check:** `DownedAfterDreamfall` (a
  hero Downed within one round of its Dreamfall) = 0 on the solo row.

Every report is defined in 11.4.

**Told plainly:** the M2 pilot alone will not reach TN6's 3% (the
projection is in 10).

**Arena (change HZ-11 to RT 6.11):** at most 1 Void region, ≥ 3 from
the Arches, entrances, start zones and boss lanes. Its own line, not
counted as a contact hazard. ENM 17.5's "a chasm edge for shoves" is met
by a Void from M2 (gated), and by a contact hazard otherwise
**(settled)**.

### 6.5 Bile (change HZ-4, HZ-5; M2; RT-13 applied)

- **Rule (agreed, RT-13):** Corroded on entry, no `onTurnEnd`. Gut only.
- **Save (lead's call inside C 11.2's "a save type and difficulty, or
  automatic"):** Fortitude negates, once per entry. On forced entry it
  comes after a failed edge save. The Gullet's Dreamfall Corroded uses
  the same save.
- **Density** *(illustr.)*: 6–12% of a Gut fight room's walkable
  squares, in pools of 3–8 on FL 7.9 bile region tags. ≤ 2 pools per
  room. Fight-legal tubes ≤ 5%, never across the only route. Corridors
  and the Forecourt 0 (the zero-hazard roles, RT 6.2.4).
- **Points:** bile up to 6% of a room's walkable squares is the Gut's
  home base share (0 points); each pool beyond it is 1 point (6.2.1)
  **(lead's call, after the check)**.
- **Placement:**
  - none within 2 of party entry; the Clearway core dry;
  - ≤ half of enemy starts on or adjacent to bile;
  - **the dry-route check:** from every `partyEntrySquares` square to
    every enemy start zone and every exit Portal, the shortest
    bile-free path is ≤ the shortest path + 2, in every Tide state
    ("Corroded is a choice, not a toll");
  - ≥ 1 Plank flush across bile per Gut floor (KP7);
  - ≥ 1 molar face or acid-scald fold within 3 of a bile edge in the
    signature room.
- **Both ways:** pushing monsters into bile lowers their AC.
- **RS 8.10 (change HZ-5; agreed text, flagged):** "at most 2
  acid-dealing **units** per encounter, besides bile squares" becomes
  "at most 2 acid **sources** per encounter". A source is an
  acid-dealing unit, a swollen sac, Brim or the Gullet. Bile squares
  are never counted, so the Acid Tide (whose states are bile sockets,
  7.9) is not a source. RT 6.3.5 and 7.9 already say
  "sources"; the RS owner reconciles the two texts.
- **Watch:** `BileCorrodedShare` by class; the Vanguard's
  `verbDefencePermille` on the Gut first; the Graveward trigger rate.

### 6.6 Deep water

- Drowning is rejected **(settled, C 10.4, RT 7.5)**.
- Deep water exists as the **Drowned Crypt** Void: a Dreamfall returns
  the unit Soaked at the dry-font Snag.
- **Wading** (M3, optional): water squares tagged `deep` that apply
  Soaked as water and cost 2 to enter (the movement-cost cap of 2
  holds).
- Later: an `amphibious` Eel-Acolyte may dive and surface, shown by a
  submerged marker.

### 6.7 Vents (change HZ-16; M3, after the TR-27 written gate)

- A vent is a hazard face (the contact band if a unit is pushed into
  it) with a mouth square.
- **Struck vent (the default, 1 point):**
  - emits one Miasma Remnant (radius ≤ 1, clipped per RT 6.6,
    `roundsLeft` 2) when the face is attacked or Smashed, a unit is
    driven into it, or a Keg bursts within 2;
  - at most 3 triggers per fight, shown as pips; **at most one trigger
    per use**, so a Keg burst that also drives a unit into the face is
    one trigger; a trigger while the vent's Remnant still exists
    refreshes its `roundsLeft` (a same-`sourceId` refresh) and spends a
    pip (lead's call, after the check);
  - `causedBy` is the triggering unit; a party trigger is party-sourced
    for the per-fight cap (fills RT 6.6's gap);
  - when a monster's push triggers it, the Remnant's first application
    (its `onEnter` application at creation, to units inside the
    footprint, inside the triggering use) counts inside that push's use
    for the EnvRound cap. `NoCheapKill` scores vent mouths like contact
    hazards.
- **Breathing vent (2 points):** a Stir with the Emit part (period 3,
  first act at the end of round 2). It is the room's one clock.
  `causedBy` is `environment`, or the operator if its Cord was
  Operated.
- **Counterplay:**
  - **plug:** a Furnishing on the mouth square suppresses emission while
    it stays;
  - **ignite:** K5, both ways.
- **Themes:** only where Poisoned is in the 6: the Silk (ink press), the
  Gut (spore pore), the Heart (dust-pillow seam); the Supper only if
  RT-14 is ruled "does not count".
- ≤ 1 per room, ≤ 2 per floor. Out of combat they cannot be triggered.

### 6.8 Teetering counting (change HZ-6; the conditions owner rules; before M2 rosters freeze)

**Ruling text offered for C 16:**
> "Conditions granted only by the universal positional rules (the C
> 10.2 hazard-edge success, C 10.3 landing, the Void branch, Clutch and
> Brink) form an uncounted universal-positional class beside the engine
> states. No monster, boss, trap, Stir, Cord or square record may apply
> Teetering directly."

Brink is a party ability, so it stays legal.

**Boss guard:**
- bosses gain no Teetering from a Void-edge success or from Clutch;
- no Void lies within 3 of boss starts or lanes (VP7);
- Brink on bosses is unchanged.

If uptime still exceeds 25%, use RT's approved reserve lever, "bosses
steady at turn start" **(settled, RT 19.2)**. Any change to Brink
itself would need a Vanguard row, and none is proposed.

**Reasons:**
- Teetering is identical in every theme, has one glyph, ends itself,
  carries no rider and is learned once. The 6 exists to bound each
  theme's learning load.
- Counting it strips identity no theme authored: Grappled from the
  Gaol, Choir, Silk and Gut; Staggered from the Carnival; Poisoned from
  the Supper (and with it Supper vents); the Heart's bedsheets.
- Voids put Teetering on every drop edge.
- A third universal tax would shrink the free 4 to 3.
- The toy shows the ruling costs nothing in removals (+0.1–0.2 points)
  but adds 6–8 points of boss uptime, hence the boss guard.

**If refused:**
- a Void-edge success becomes a plain stop;
- the RS 8.0 fallback drops apply, and every drop theme names its
  fallback in V1 `ThemeBudget` before it ships;
- the Supper vent and the Heart's Toss and Turn (a bedsheet Tide, 7.10)
  are cut;
- the client is told through a 19.2 row.

### 6.9 Floor-state hazards: Moods (change HZ-20; M3, Omen and Nightmare only)

- At most one Mood per theme. An integer stage 0–2.
- Advanced only by a named, visible event, **capped at +1 per two
  rooms**: the stage never exceeds floor(fight rooms cleared on this
  floor ÷ 2). Event counts are kept in a `MoodCounter` beside
  `MoodStage` (lead's call, after the check). Never by wall-clock time,
  exploration ticks or rests.
- A Mood never takes a period below its kind's floor (6.3.3).
- Shown on the HUD and the Stratum Card with every threshold visible
  from floor entry.
- Changes hazard states only (periods, starting states), never
  awareness, joiners, hearing, Portals or monster stats, so it is not
  FL 9.1's rejected floor-wide alarm.
- Saved in `hazardDigest`.
- **Gate:** no Mood may raise the F2 death share beyond noise (DH 18.2).
- **Curiosity costs (events design 4.9; change EV-31; M3):** on an Omen
  floor a Curiosity option's printed cost may be the named, visible
  event that advances the theme's Mood, inside the cap above; an Omen
  floor may also swap a card for its Omen Retelling (kind, not stats).

**Delivery:** the base game has none. Each Mood is an Omen (one per
theme, announced at floor entry) or Nightmare content.

**Rejected:**
- Model C's stage-bound room layers (triple V9/V10 enumeration, a
  stretch of main 2, invisible in play);
- the per-room-cleared ramp (toy: +1.7 min per floor; F2 death 4.7% →
  7.1%);
- rest-driven Digestion (it attacks DH 5's guaranteed rest supply and
  kicks a losing party);
- the Choir Hymn as a floor meter (it rewards stalling).

| Theme | Mood (Omen) | Event | Effect |
|---|---|---|---|
| Choir | High Water ("Spring Tide") | every two fights won | later rooms' Tide starts one band higher (max +1) |
| Silk | Silence Kept ("Overdue") | every three out-of-combat loud events | Leaning Stacks start one notch lower |
| Carnival | The Last Evening ("Encore") | every two fights won | Pit period 3 → 2; Act III sets the arena (segment 9) |
| Gut | Digestion ("Fasting") | each fight that reaches round 6, capped +1 per two rooms | stage 1: Brim period −1 (Peristalsis is exempt: its Lurch floor is 3); stage 2: Pylorus bile phase at 75% (segment 9) |
| Heart | Nearer Waking ("Fitful Sleep") | `mainPathIndex` past the elite room | Dreamer period 3 → 2 |

**New lead's call:** the Carnival Mood keeps its name "The Last
Evening" only as the Mood's display line under the Omen "Encore"; the
theme's own name (the Carnival of the Last Evening) wins any naming
clash.

**Lead's call, after the check:** the Supper's "Last Orders" ("Second
Sitting") is not a Mood. It advances a menu index per fight won rather
than a 0–2 escalation stage, so it moves to the Courses as a menu rule
(7.8; `MenuIndex` in the floor state, 11.1). It stays the Supper's Omen
("Second Sitting"), which switches the menu rule on; the Supper has no
Mood.

### 6.10 Hazard rooms: Dare rooms and Plunge (change HZ-21, HZ-23; M3)

**Dare room:**
- A side-room Site on F2–5, ≤ 1 Dare-or-Wager per floor (a Wager room
  shares the slot; traps and locks design 6.15; change TL-29; M3),
  never critical, ≤ 9×9.
- **Signposted** before entry: a hazard glyph on its Portal, Listen
  hears the Stir's tell, and a line on the Stratum Card.
- One showcase Stir (one clock, never two), double points, and a
  reserved reward container (segment 7, +1 rarity step *(illustr.)*).
  **The step (loot economy design 6.9; change LE-10; M3):** the Dare
  cache is an SR5 step socket under the one step rule that also reads
  traps and locks design 6.11 and 6.15: its card is at least one tier
  above the floor's reference tier, by allocation first, else a raise
  from the step allowance (≤ 2 per floor), else a fit premium at a gate.
  It sits behind a hazard, not a lock or trap, so it is outside the
  locked-or-trapped quota.
- **Two forms:**
  - **(a) Unguarded, "the rhythm is the lock".** The Stir runs per
    out-of-combat tick (C 5.5: one round is one tick; read by the M4
    Ebb, loot sources design 4.7.11, change LP-34), so there is no
    new round mode and no contest of main 4.3. Auto-walk is off inside.
    One chosen dare-taker; the others wait at the threshold. DH 10.4's
    1-HP floor applies.
    - **Anti-brute-force:** a taker reduced to 1 HP by the room is
      returned to the threshold and the dare resets. No short rest
      inside the room. Two attempts per floor; after the second failure
      the container seals for the floor.
    - **Open item (loot economy design 6.9 item 6, 8.1; M3):** reward
      width by first attempt is left to this document's owner; not
      adopted, not yet ruled.
  - **(b) Guarded:** the floor's **Annex** group, Dormant among the
    devices (the party may wake it, sneak, or turn the room on it),
    funded as one extra encounter budget at the patrol band at the host
    room's level, 0 XP (its reward is the Dare cache), in ordinary
    combat once woken. If the Annex serves a higher-priority need, the
    Dare room is unguarded (encounter placement design 6.13; change
    EP-26; **agreed, Q2**; M3). The Annex adds no prize of its own: the
    Dare cache keeps its one step, never a second, and a Dare whose
    garrison is dropped keeps its card (loot economy design 6.9 items 1,
    7; change LE-10; M3).
- Measured on the "taken" arm of `FloorTimeByTheme`, target ≤ 60 s per
  dare. `DareBruteForceRate` reported.

**Plunge:**
- An out-of-combat, one-way, signposted void well in a side room or
  Dare room. **It is a skin of FL's M3 Chute (directed edge, FL 9.1)**,
  with an explicit exception to LG14's return-Portal principle (its far
  end lies deeper on the main path), requested of the FL owner (HZ-23;
  lead's call, after the check).
- The party steps in as one, takes the harm band (floored at 1 HP), and
  arrives at a linked room deeper on the main path.
- The target must be cleared or non-combat, and ≥ `HearingGap` from any
  un-engaged group. Never the Sanctum, never past the antechamber.
- ≤ 1 per floor. `NoSealedPocket` and `PartyEscape` hold.
- It is the only hazard that saves clock. In combat it is a plain Void.
- **Curiosity bypass routes (events design 4.4.1; change EV-36; M3 if
  HZ-23):** segment 8 adds no link of its own; a Curiosity route may
  only skin an FL-generated Chute, under `GraphUnchanged`, and waits on
  HZ-23 like Plunge.
- **Pending request (TL-41, M3; traps and locks design 6.3, 7):**
  segment-5 relocation traps (the Dumbwaiter Drop to its linked room out
  of combat; in combat within the same room only, so the in-combat
  Swallow rejection of 6.4 holds) beside Plunge and FL's Chute; they
  wait for the ability model's teleport rule (ABL 8), else they stay
  Prone in place; not yet ruled.

### 6.11 Caps and fairness

1. **One use per act:** each Dream-step act, and every combo it
   triggers, is one use for the RT 6.4 one-instance cap and for the
   area cap of 2 saves.
2. **The EnvRound cap (change HZ-7; a resolution rule, not a report):**
   - environment damage to one party unit within one round is capped at
     `CapRound`, 30% *(illustr.)* of the lowest legal hero's maximum HP
     for the floor's level band, using the RT 6.4 top-up mechanic at
     round scope;
   - **included:** collision, contact, harm, Keg, Stir acts and falls,
     the first Remnant application of a monster-push struck vent, and
     involuntary trap damage to a party unit (traps and locks design
     6.10; change TL-18; M2);
   - **outside:** `onTurnEnd` ticks (Burning) and other Remnant
     applications; self-caused trap damage (Bear it, a party-caused
     spring on oneself, a Botch the unit itself rolled, a chosen
     in-combat entry into a shown Hunch zone) is counted in full and
     does not fill the cap (change TL-18);
   - monsters are uncapped (Clutch damage is therefore never capped).
   - **The pipeline per instance (lead's call, after the check):** the
     final amount after save, weakness and resistance; then the
     per-use top-up (RT 6.4); then the round-scope top-up, which deals
     max(0, min(amount, `CapRound` − `envTakenThisRound`)), where
     `envTakenThisRound` counts damage actually dealt; then the 1 HP
     floors last (Warned or floored, the FallFloor, DH 10.4).
   - `CapRound` is stored per floor in the floor hazard state. Floor-1
     Nightmare content uses floor 1's own level band (6.19); an arena
     uses its floor's value. Out of combat, one tick is one round for
     the cap. The Dream step that ends round R charges round R (6.3).
   - This closes the toy's worst case (52% of the lowest legal F2
     hero's HP in one round from five separate uses).
3. **Placement rules beside it:**
   - no Keg inside a Stir footprint on F2–3 (the Carnival Pit has no
     Keg in its ring; a Keg at ring distance 2 is party bait);
   - at most 1 shover type per encounter in clock rooms and in Void
     rooms, and **at most 2 units with Shove-type kits starting within
     4 of a Void edge** (lead's call, after the check; VP3 already caps
     starts within 2 at 2). `PartyFallsPerFight` is reported by shover
     count. Segment 6 counts every brute and soldier as a shover type,
     since every such at-will carries Shove (encounter placement design
     6.4, 6.6.8; changes EP-21, EP-31; M2).
4. **Guards:** Stir acts are counted in the ENM 6.4 guard rows in the
   rounds they are due (the Keg and Conduct precedent). Deep falls count
   in the Choir lightning spike row. Environment Chilled counts in the
   Supper freeze-spike row.
5. **Lethality:**
   - `ContactLethalityBound` covers falls and Stir acts, and for
     environment acts and fall bands it is computed **after weakness**
     (lead's call, after the check): V10 proves it against a target
     weak to the type through its own theme's square conditions
     (Soaked to cold and lightning, Oiled to fire). An act or fall band
     that fails drops to the harm band (the Organ, 7.5; the Drowned
     Crypt, 7.5);
   - C 10.4 holds;
   - Warned or floored and the FallFloor give `ForecastLethalSurprise` =
     0 by construction;
   - the danger band includes environment acts due before the hero's
     next turn (change HZ-13, DH 17.3).
6. **K3:** environment acts never start Conduct. The closed K-table is
   unchanged; Stirs are new sources for K1, K2, K4 and K5 only **(told
   plainly)**.

### 6.12 AI (requests to the enemies owner; extends TR-15)

1. **Destination value** adds the value of an act due at this round's
   end on that square: a one-step quantity, with no horizon-2 read. The
   toy showed 94% of monsters could step off with one-step reasoning;
   `floorborn`'s horizon-2 read is rejected.
2. **Stupidity floor:**
   - never end a turn on a due square whose act could kill this unit;
   - never enter a Void;
   - routed units never path adjacent to a Void and treat due squares as
     hazards.
3. **`NoCheapKill`** extends to due footprints, monster Cord use and
   vent mouths. Falls need no cover, because they cannot down heroes.
4. **Fall farming:** the fall value toward a given hero is scored once
   per fight (a board-history term, not hero status, so invariant 1
   holds).
5. **The Holder** shoves toward due footprints and Voids. The intent
   reads "pushes toward the edge".
6. **Mindless types** ignore non-lethal due squares but keep the lethal
   floor (readable stupidity on their card). The mindless share is
   **not** raised by segment 4; the enemies owner is asked.
7. **Flying:** ≤ 1 flyer per Void-room encounter (a segment-6 hook).
8. **Terrain Pairings (encounter placement design 6.6.8; change EP-21; M2):**
   segment 6 seats shovers by a VP4 push lane, never with a Billet
   within 2 of the edge. **M3 / pending, on their owners' rulings:**
   `usesCords` types by their Cord; Stoker types by their vent (HZ-24).

### 6.13 Party exploitation and levers

**Intended plays:**
- push or drag non-elites into Voids;
- soften an elite's Clutch, then finish it after its turn;
- push monsters into bile for AC;
- hold monsters on due squares after their turn, since they get no
  Warned floor;
- Operate Now so the act lands after your push round;
- Hold a quake to protect a fragile party;
- strike a vent at a monster's feet, then ignite it;
- push Furnishings into Voids to open lanes;
- rescue-push an ally to the (secondary) Snag, at the cost of its next
  action (6.4);
- set a room's starting phase out of combat (at a noise risk);
- kill the Choristers first to flood the nave for lightning, or last to
  keep it dry.

**Levers if reports fail:** Void density (40% → 30%), Snag distance (4 →
6), the shover cap, the Clutch "after its turn" hold, Cord placement,
period (3 → 4), footprint size.

### 6.14 Out of combat, stealth, hearing and patrols

- Damaging Stirs are dormant out of combat and play an idle tell. Their
  phase is shown.
- Landmark ticks tick out of combat.
- Squares apply at ticks under DH 10.4. Auto-walk and followers path
  around hazards and Voids.
- Cords can be Operated (noise 4). Struck vents cannot be triggered.
- A fall makes no game noise ("the dream swallows it"; not propagated);
  its RC7 cue still plays (8). The push that caused it makes noise.
- A Stir act adds no noise beyond the combat round.
- Circuit rooms hold no Voids or Stirs. Patrol routes avoid Void-adjacent
  squares. Joiner landings are never Void-adjacent.

### 6.15 Class interplay

| Class | Gains | Watch and gate |
|---|---|---|
| Vanguard (Wrecker) | Void removals; Brink on Void squares; due-square pushes | Toy: about 55–60% of environment kills. `FallsByWrecker` against `HewCeiling` (removal valued as remaining HP, capped by Clutch; the TR-30 owner). The Void-save disadvantage is removed (6.4). The spread gate |
| Shade | Mute rounds (M3); Hide never in footprints or next to Voids; the quake's "still" rule favours stillness | Toy: 0% of environment kills. **Ship gate:** a Shade hazard hook owed by the Shade owner before Voids ship. Candidates: Operate a Cord without breaking Hidden; a push-1 from Hidden at Void edges. Vanguard–Shade gap ≤ BASE + 2 |
| Arcanist | Gust, Squall and Shockwave into Voids and footprints; fire into vents; Stirs, Cords and Voids count for DEP-A1 (TR-21) | The owner's call: a second Gust charge in Void rooms. `ArcanistComboShare` |
| Herald | Give Ground into footprints and Voids; Startle plus Frighten = Rout (M3) | Give Ground stays "a tool, not a lean" |
| Trapwright (DEP-S1) | Hook: Jury-Rig may re-time a Stir (C ± 1, never below 2, so SC2 holds) | The owner's call |
| Heavy armour | Bile and Corroded | `verbDefencePermille` on the Gut |
| Trinkets | Ledgewalker fires on Void edges; Emberward on fire Stirs; Antidote on vents | Trigger-rate reports per theme |

### 6.16 Companions and party size

- Companions use every rule a hero does (Dreamfall, FallFloor, Groggy,
  Snatch, Warned or floored).
- Follower pathing and the suggested action read due squares and Voids;
  companions never idle in a footprint (a bug gate).
- Prop verbs push into footprints only when the player opens the target
  (RT 6.10).
- Companions Operate Cords only on command.
- Barks (RC8): a companion calls a Stir's first due round once per
  fight. Never on telegraphs.
- Every report has solo, duo and four rows.
- Party-of-4 risks and their fixes: Snag crowding (two sockets, the
  overflow rule), quake anchors (the "still" rule), Snatch (no damage).

### 6.17 Boss arena hooks (segment 9 owns them)

Each arena may have ≤ 1 clock (one Stir) and ≤ 1 Void region inside
RT 6.11's caps and phase sockets, under every room rule (one clock per
room; no Void with a Lurch Stir). Bosses are `noDrop`, are moved by
Lurch like anyone, and never gain Teetering from Voids or Clutch. Each
row is one clock **(lead's call, after the check: the proposal's
paired rows broke the one-clock and Void rules)**.

| Boss | Arena rhythm option |
|---|---|
| The Jailer of Hours (Gaol, F1) | The pendulum stays party-only counterplay at every tier, not a hazard (the Gaol's one tier-2+ package is the Treadwheel, 6.19) |
| The Drowned Carillon | Its phase (e) flood becomes Tide High; the arena's own sluice and bell rope stay segment 9's identity |
| The Precentor | The Organ |
| The Mouthless Librarian | A leaning index (the Overturn part) |
| Madame Ashes | Pit Breathes; or, in an F3 arena only, the Turning Ring (6.2.2), with the pit as static fire on the hub |
| The Head of the Table | Courses; the final course |
| The Pylorus | Brim, with the Gullet in phase (e) (no Lurch Stir in a Void arena, so Peristalsis is not used here) |
| The Night Governess | The Dreamer Turns Over (with Handholds) |
| The Rousing Engine | The Alarm Clock made boss |
| The Master in the Frame (Kennel, F1) | The Kennel gate (segment 9) |

### 6.18 Previews and UI (change HZ-29)

- **Latent footprint (lead's call, after the check; the RT Keg
  precedent):** from reveal, and out of combat, every footprint is drawn
  as a thin outline in its host's rim style at hazard tier, so the
  valuable squares SC13 asks for can be planned around between acts.
- **Due overlay:** at announcement the outline is promoted to the RC6
  telegraph tier, above hazards. Pattern plus ticking fill plus a
  countdown digit, never colour alone.
- **The Dream card** on the initiative bar, carrying the countdown and
  a universal Dream tick (8).
- **Rule card** on hover: one line of rule, one of fiction. Only the
  next state is drawn at telegraph tier; the full cycle goes on the
  card.
- **The countdown digit** sits on the host's floor decal and the Dream
  card, legible at all 4 camera rotations (6.3, 8).
- **Previews show one headline plus one sub-line; branches go on
  hover.** Examples:
  - "Edge: 45% falls (removed)";
  - "Edge: Clutches, −4, Prone";
  - "Falls → Snag, Prone, −4 (cannot down) · lands 7 from the
    Arcanist" (adding "beside the Mastiff" when a hostile is adjacent
    to the landing);
  - "Lands on due square: Organ swells, −3, Reflex half".
- Warned and floored marks (6.3.2). The Snag link glyph and fall arrow.
  "Holding on", "Groggy" and "floored this time" pips in the unit-tier
  pip list.
- A Chainpost's due ring label updates live as units move: it always
  names the unit that would be bitten now (9.2).
- `PreviewExactness` 100%, including act and fall branches. The death
  recap names the Stir (DH 17.12).
- **Glance test** in playtest: can a player name what happens at round
  end within 3 s?

### 6.19 Escalation and floor 1 at Nightmare tier 2+ (agreed, Q4; change HZ-25)

**Escalation is in kind, not damage (lead's call).** Omens (RS 9.1) and
Nightmare tiers (RS 3.4, 9.1; RT-21: "add kind, not stats") escalate
hazards by adding a kind, shortening a period by at most 1 (never
below its kind's floor, 6.3.3), or starting a Mood stage higher; never
by raising a band, an EnvDC or the EnvRound cap. V10 enumerates Mood
stages and a tier class (11.3).

**Floor 1 at Nightmare tier 2+ (agreed, Q4).** The base game keeps
floor 1 hazard-free at every tier below 2 (RT-20 kept literally; the
Landmark ticks of M3 are non-hazards). From tier 2 each floor-1 theme
gains **one damaging hazard package**:

| Theme | Package (tier 2+) | Form |
|---|---|---|
| Gaol | **The Treadwheel** | A hazardous Lurch Stir lane (period 3, 1 square per act) toward the warden's stove, which is promoted to a Hot piece at this tier, typed bludgeoning ("scalding iron"; the Hot class already carries non-fire types, RT 6.5.1, and the Gaol has no fire). This is RT 7.2's anticipated M3 stove promotion, under its `HazardPushDeaths` gate |
| Kennel | **Staked pits**, with **the Painted Hunt**; **the Stampede** replaces the Painted Hunt at tier 3 | Staked pit: a harm square (piercing; the staked-pit skin), damage only, with the C 10.2 edge save and no Teetering (lead's call, after the check: no hybrid class, so no Bleeding, no Prone and no leave cost). The Painted Hunt: a benign Lurch Stir, 1 square along a lane, period 3; fallen logs hold fast (Handholds). The Stampede: a hazardous Lurch Stir, 2 single soft-stop steps along the same lane, period 3, with edge saves only toward the staked pits on its lane; no collision; the herd is act-only VFX, never units (8) |
| Wake (conditional theme) | **The Open Grave** | A Void (the Wake is a drop theme at tier 2+ only, 6.4), its fall band bludgeoning; the Snag is the floor squares beside the empty, lidless coffin, proven by `SnagProven` |

**Lurch on floor 1 (lead's call, after the check):** these packages
are the only Lurch Stirs below F3 (6.2.2) and the only floor-1 users
of the hazardous form (6.3.3): the Treadwheel at tier 2+, the Stampede
at tier 3. Both are soft stops; no Lurch anywhere deals collision.

**New lead's call (the Kennel reading):** Q4 names the Kennel's
Stampede, staked pits and the Painted Hunt. They are read as one
package that escalates in kind: staked pits plus the non-damaging
Painted Hunt at tier 2; the Stampede replaces the Painted Hunt at tier
3, still inside the client's "tier 2+" (**told plainly**). So each
floor-1 theme has one damaging package at any tier.

**Rules for floor-1 Nightmare hazards (new lead's call, inside Q4):**
- DH 17.1 holds: no Void, Stir or Lurch in main-path rooms 0–1; the
  floor-1 room-0–2 rows of ENM invariant 6 hold; the floor's first
  fight room holds none (6.2.2).
- At most 2 hazard rooms per floor-1 floor, at main-path index ≥ 3,
  with the F2 room budget (3 points).
- **Floor 1's own bands (revised after the check):** the bands and
  `CapRound` are indexed by floor 1's lowest expected level band, not
  F2's; EnvDC 13. V9 `ContactLethalityBound` and V10 `EnvRoundBound`
  are run at floor-1 levels for each tier package (the bound proven,
  the stack reported), and a package that fails drops one band step.
  Warned or floored, the FallFloor and the EnvRound cap hold.
- The floor-1 guard rows (ENM 6.4, DH-26) and `Floor1ClassSpread` are
  re-run per tier with the package on; a failing package moves to the
  next tier up.
- The solo hero is not exempt at tier 2+: those players have already
  won a run **(told plainly)**.
- **Staked pits and the Kennel's dryness:** the pit is a harm square,
  condition-free, so the Kennel stays "deliberately dry" (RS 6.3, read
  as "no condition squares") and adds nothing to its 5. RS 8.4's Hooks
  bullet lists staked pits as "Later (drop hazard)" and RS 13's Hazards
  row calls them a drop hazard; this design re-reads them as a walkable
  harm square at M3, Nightmare tier 2+ only **(told plainly)**. RT
  19.2's Briar (Bleeding vines in the Kennel) stays its own future
  question.

---

## 7. Per-theme hazard catalogue

**(lead's call; stands as written; the Q1–Q4 content agreed)**

Points as in 6.2.1. **Pilot** marks the M2 (gated) entries.

### 7.1 Summary

| Theme (floor) | Law | M2 | M2 (gated) | M3 headline | Later, M4 or Nightmare |
|---|---|---|---|---|---|
| Gaol (F1) | Lockdown: doors, noise, keys | dry (RT-20) | — | Landmark ticks: the Small Hour Strikes; standalone Cords: the Lockdown Lever, the Curfew Rope | The Treadwheel (Nightmare tier 2+, Q4) |
| Kennel (F1) | Pack and horn; backs to logs | dry | — | The Horn Post (Landmark Cord) | Staked pits and the Painted Hunt (tier 2+), the Stampede (tier 3) (Q4) |
| Wake (F1, conditional) | The sleeping house; tells | dry | — | Guttering Candles, the Mourning Cord | The Open Grave (tier 2+, Q4); the Long Night (Later, NM-9) |
| Choir (F2–3) | The drowning line | The Drowned Nave tableau | **Organ Swells** (pilot, Q1); **the Drowned Crypt** Void (F3, Q2) | The Hymn Must Never Stop (Tide, Q3); the clapper chain | — |
| Silk (F2–3) | Webs and silence; fire is the key | The Carrel Rows tableau | **The Stacks Have No Bottom** Void (Q2) | Lamplighting; the Leaning Stacks; the ink press vent | The Returns Trolley (M3, Q3); the Lamp's eye (Later) |
| Carnival (F2–3) | Oil and fire both ways | The Fire-Eater's Pit tableau | **Pit Breathes** (alternative pilot, Q1) | The Turning Ring (Q3; F3 only); the Wheel of Knives; the High Wire Void; the Chained Bear | — |
| Supper (F4; F3 from M3) | Freeze and shatter both ways; dry | The Laid Table tableau | — | The Courses (the first M3 Stir); the Open Window; the Forbidden Course | The Service Lift (Later) |
| Gut (F4) | Attrition | Bile; the Bile Low tableau | — | Brim; Peristalsis; the Gullet Void; the spore pore; the Acid Tide (Q3) | — |
| Heart (F5) | Waking | The Thorn Garden tableau; bedsheets (RT-14) | — | The Dark Under the Bed Void; the Dreamer Turns Over; the Alarm Clock; Falling Toys; Counting Sheep; Toss and Turn (a bedsheet Tide, Q3); the dust-pillow seam vent | The Nightlight (Later) |
| Inverted Cathedral (F3–4) | Edges and light | — | — | — | M4: Fall Upward; the Chimes Ring Upward (Q3); the censer rope |

**The signature Stir pilot (agreed, Q1):** the Choir's Organ Swells is
the first choice and the Carnival's Pit Breathes the alternative (6.3).

### 7.2 The Gaol of Small Hours (F1)

**M2:** nothing; RT-20 holds.

**M3, Landmark ticks (non-hazard; told plainly):**
- **The Small Hour Strikes.** Host: the Landmark clock-case, in a fight
  room at main-path index ≥ 3. Every 4 rounds, from the end of round 3:
  - **Portal:** every open, unoccupied, unlocked cell door in the room
    swings shut. Never locked; never the `fleeExit`; held while
    occupied.
  - **Mute round:** party-verb noise is not propagated until the end of
    the next round. Combat noise is unaffected.
  - Hinge pips on doors due to shut are mandatory. A one-time
    `lessonTag`: "Strike: next round your tools are silent."
  - The clock hands reach toward the hour. One reserved low chime,
    distinct from alarm-bell timbre (the Gaol bed has no bells; this is
    a cue).
  - It is the room's clock (6.2.2).
  - An FL-8 Portal-source flag. `Floor1ClassSpread` and the DH 18.2
    floor-1 rows are re-run with it on.
  - Segment 5's Hour lock (Gaol, Wake) also opens itself on the floor's
    Landmark tick; its glyph is an hourglass, never a clock face
    (traps and locks design 6.11; change TL-22; M3).
- **The Lockdown Lever.** A standalone Cord: one Operate shuts or opens
  every unoccupied, unlocked cell door in the room, at once (6.3.4).
  Noise 4. Party-only.
- **The Curfew Rope** (two standalone Cords; lead's call, after the
  check):
  - **the rope:** Operate: a peal of noise 10 at the end of the next
    round, shown on the Dream card. Once per floor, within joiner caps;
    it can pull a group to a prepared room. The Jailer-Sergeant rings it
    with an announced joiner.
  - **the cleat:** Operate cuts the rope, so its peal Cord is spent for
    the floor. Once per floor.

**Nightmare tier 2+ (Q4):** the Treadwheel, a hazardous Lurch lane
toward the stove, which is promoted to a Hot piece typed bludgeoning
(6.19).

### 7.3 The Kennel Wood (F1)

**M3, Landmark Cord:**
- **The Horn Post.** A standalone Cord, **once per floor** (lead's
  call, after the check: once per fight made it a near-automatic Rout
  opener in every Kennel fight): a cracked, wrong note. **Startle:** the
  skittish, non-Emboldened members of **one group**, the one named on
  the post's preview (the group nearest the post, ties in fixed order),
  are Demoralised. Noise 6 (the joiner cap of 1 holds).
- The floor-1 morale lesson in one object: kill the Houndmaster, blow
  the horn, Frighten the Mastiff. A reserved cue of the rattle family,
  never a horn timbre (RS 8.4 reserves horns for joiners; 8). The
  floor-1 rows (DH 18.2, `Floor1ClassSpread`) are re-run with the Horn
  Post on, with a Rout-rate row per class.

**Nightmare tier 2+ (Q4; 6.19):**
- **The Painted Hunt** (a benign Lurch Stir: 1 square along a lane,
  period 3; logs hold fast). It moves units, so it is not a Landmark
  tick. The toy measured it harmless (downs 0.32% against 0.32%).
- **Staked pits** (a harm square: the harm band, piercing, damage only;
  the C 10.2 edge save with no Teetering).
- **The Stampede** (tier 3, replacing the Painted Hunt): a hazardous
  Lurch Stir, 2 single soft-stop steps along the lane, period 3, edge
  saves only toward the staked pits; no collision; an act-only figment
  herd, never units (8).

### 7.4 The Endless Wake (F1, conditional)

**M3 with segment 6:**
- **Guttering Candles.** A Dormant group in a room at main-path index ≥
  3 sits by a candle rack (a typed Low piece) with 3 candles; at most
  one rack per room (6.2.1's floor-1 rule; lead's call, after the
  check). A near-miss noise snuffs one. At 0 the group wakes Wary,
  announced. A dial, never light (RS 7.6 kept), and the room's clock. A
  soft exhale cue.
- **The Mourning Cord.** A standalone Cord: Operate draws or opens
  every unoccupied drape in the room, at once. Party-only (monsters
  changing Portals is an FL-8 change).

**Nightmare tier 2+ (Q4):** the Open Grave Void (bludgeoning), with the
floor squares beside the empty, lidless coffin as the Snag (6.19).

**Later:** the Long Night (light, NM-9). **Rejected:** the Eulogy Pause
(a party-only tax).

### 7.5 The Drowned Choir (F2–3)

**M2:** the **Drowned Nave** tableau: a flooded aisle band, pew islands,
a dry causeway Plank, storm-glass faces as Conduct bait, RS 8.6 islands.

**M2 (gated, Q1): the Organ Swells.**
- Host: a storm-glass pipe face whose front square touches water.
- Footprint: a **fixed generation-time socket set**, the water
  connected within path 3 of the front square, ≤ 12 squares (stored in
  the record, so it is not a K3 clone).
- Period 3; acts at the end of rounds 2, 5 and 8.
- Strike: lightning, **the harm band** (lead's call, after the check:
  every unit it hits stands in water and is Soaked, so the Soaked
  weakness lifts the harm band to about the contact band, and the
  contact band would break `ContactLethalityBound` after weakness, 6.11
  item 5), Reflex for half. **Never starts Conduct.**
- **Payoff (SC14):** with no unit on the footprint it discharges
  harmlessly. Emptying it defuses it; holding an enemy there charges
  it.
- Cord: the **hymn bellows** (Hold).
- Counted in the Choir lightning spike row. The pipes fill in 3 visible
  notches. **Cues (8):** announcement, an air-rush intake through the
  pipes (non-pitched); act, an electrical crack. No organ tone, which
  is the Choir bed's timbre.

**M2 (gated, Q2): the Drowned Crypt (F3).**
- A well-shaft Void with the universal lip and the depth backdrop;
  black water shows only far below in the backdrop, never at floor
  level (it must never share a material with walkable water squares).
  The "rail" is a run of Low pieces under the shield-notch rules, or
  wall-side dressing off walkable squares. The dry font (wall-side) is
  its Snag.
- Dreamfall: the cold fall band at the harm band (6.11 item 5: Choir
  heroes are often Soaked) plus Soaked.
- Monsters are pulled under.
- Never water-connected to an Organ footprint; no Stir in a Crypt room.

**M3:**
- **The Hymn Must Never Stop.** An in-room trigger Tide (Q3). WetEdge
  sockets rise one round after the last `singer` falls. Routed or
  Yielded singers count as stopped. A backstop at the end of round 4.
  Segment 6 seats the singers as the Choir Ritual, a Warband formation
  on a Tide WetEdge host (encounter placement design 6.6.7; change
  EP-27; M3). **Pending request (EP-27, Later):** a singer Tide in any
  other theme needs a new HZ singer entry; not yet ruled.
  - Framed as a reward option for lightning parties, never as a floor
    meter.
  - The **Sluice Wheel** Cord (Flip, Q3: the Tide turns at the end of
    the next round, announced at this one, under the Tide's hold rule;
    one verb; lead's call, after the check). It is the catalogue's
    first Cord Flip.
  - `HymnRoomRoundsDelta` ≤ 0.
- **The clapper chain.** A standalone Cord on the fallen-bell face,
  resolving as Now (6.3.4): the squares within path 2 are announced at
  this round's end, and at the end of the next round units on them make
  Fortitude or are Staggered. Noise 6; recharge 2 (pips). It is the
  room's clock, so never in a Hymn room.

### 7.6 The Silk Archive (F2–3)

**M2:** the **Carrel Rows** tableau (web fields around carrels, a
lamp-sconce face for K2).

**M2 (gated, Q2): the Stacks Have No Bottom.**
- A perimeter Void aisle crossed by a flush Plank ruler.
- Web lines one edge, so a push offers a choice: Restrained or a fall.
- The Snag is the floor squares beside the returns desk (the desk on
  adjacent non-walkable squares).
- Fall band piercing (spilled nibs and rulers; the Silk's
  `primaryDamageType`, 6.4; revised after the check: the Silk has no
  bludgeoning).

**M3:**
- **Lamplighting.** Period 3. A lamp-sconce flares 3 squares forward:
  fire at the harm band, Reflex for half; K2 burns the webs and frees
  the Restrained. "Fire is the key" becomes timing. Cord: the lamp
  chain (Now).
- **The Leaning Stacks.** A tippable Tall Furnishing with a lean meter
  of 3; loud events in the room lower it. At 0 the strip becomes due,
  and the stack overturns at that round's end: the contact band, Reflex
  for half, Prone on a failure. It leaves Rough debris and opens the
  sight line (rides RT's M3 Overturn).
  - At least half the strip lies within 3 of enemy starts.
  - The hidden shelf tripwire stays a trap.
- **The ink press** struck vent (designed beside a lamp sconce).
- **The Returns Trolley** (Q3): a Furnishing the environment moves
  along a pre-proven rail, held while occupied.

**Later:** the Reading Lamp's eye (light, NM-9).

### 7.7 The Carnival of the Last Evening (F2–3)

**M2:** the **Fire-Eater's Pit** tableau (a fire core, an oil ring at
distance 2, a Keg at ≥ 3 from entry and ≥ 2 from enemy starts,
Strongman shove lanes).

**M2 (gated, Q1, the alternative): Pit Breathes.**
- Period 3. Fire on the ring of 1 around the pit squares at the harm
  band, Reflex for half, the Oiled payout. K1 burns ring oil.
- **No Keg in the ring on F2–3.** Kegs at distance 2 are party bait.
- Counted in the RT 7.7 flask-then-breath spike guard.
- Cord: the Ringmaster's damper (Hold). **Cues (8):** announcement, a
  bellows inhale; act, a whoomph.

**M3:**
- **The Turning Ring** (Q3; F3 only, since Lurch Stirs start at F3,
  6.2.2). Ring boards around a non-walkable hub where the painted
  horses stay on their poles. Every 3 rounds, units on the ring Lurch 2
  clockwise in single soft-stop steps.
  - A brake Cord (Hold) and a reverse Cord (Reverse): two bound Cords,
    one verb each.
  - Cues: a mechanical ratchet per step, never a calliope note (the
    Carnival bed is a detuned calliope; 8).
  - FL 7.7's letter holds ("carousel horses always on poles"); its
    spirit is contested **(told plainly)**. Tested at 4 camera
    rotations.
- **The Wheel of Knives.** RT's existing Carnival piece gains a
  standalone Cord whose line strike resolves as Now (6.3.4): the line
  is announced at this round's end and struck at the end of the next,
  at the harm band, Reflex for half; recharge 3 (pips). The
  Knife-Thrower Operates it, as a telegraphed intent. It is the room's
  clock, so never in a Turning Ring or Pit room.
- **The High Wire.** A 2-wide Void under a Plank wire, fall band
  bludgeoning; the safety net hangs on the wall beside the landing and
  the Snag is the floor squares under it.
- **The Chained Bear.** A Chainpost (9.2).

### 7.8 The Long Supper (F4; F3 from M3)

**M2:** the **Laid Table** tableau: a table-spine sight wall,
plate-warmers (Hot) every 3 squares, the carving-station face. Dry.

**M3 (the first broad Stir):**
- **The Courses.** Every 3 rounds the serving hatch (not the dumbwaiter)
  serves. A **printed menu card** shows the next 3 courses (a
  permutation drawn at generation). Under the Omen "Second Sitting"
  ("Last Orders"), the menu advances one course per fight won, and the
  next Courses room opens on that course (`MenuIndex`, 6.9):
  - **Soup:** fire to units adjacent to Hot pieces. Ends Chilled and
    Frozen, both ways.
  - **Sorbet:** Chilled down a table lane (Fortitude negates).
  - **Roast:** carving, piercing, the contact band.
  - The bellpull Cord (Now; the Glutton uses it). A course bell cue. The
    best fix for the Supper's DEP-A1 miss.
- **The Open Window** (renamed from "the Draught", 0.1 names). For bays
  with no hatch: Chilled, then Lurch 1 toward a warmer line, whose
  contact fire ends the Chilled.
- **The Forbidden Course** (lead's call; the DH owner rules; HZ-30). A
  party-only Operate on a covered plate served with Soup or Roast, ≤ 2
  per fight. Heals half a potion (DH 12.1) and gives Poisoned
  automatically (Chilled if RT-14 removes Poisoned). The Glutton's
  reach for it is an intent. **Pointer (events design 4.9; change
  EV-33, a request to the DH and conditions owners; M3):** segment 8's
  Easing (priced Weary or rest-tick relief, never HP) follows this
  HZ-30 path and is counted in `HealSupplyPerFloor`; no Curiosity
  option heals (events design 5.3, Su4).
- **The Carving Hound.** A Chainpost under the table.

**Later:** the Service Lift (cross-room).

### 7.9 The Gut of the World (F4)

**M2:** bile (6.5) and the **Bile Low** tableau (pools at tube lows, a
Plank flush, molar faces at bends, a swollen sac).

**M3:**
- **Brim.** Period 3. Units adjacent to bile make Fortitude or gain
  Corroded. One acid source.
- **Peristalsis.** A 2-row band along the tube (a static motion mark at
  idle; the band steps only during the act, 8). Every 3 rounds, units
  in it Lurch 1 down-tube with edge saves into molars or bile (the
  hazardous form, F4). Hidden units and units adjacent to a Handhold
  hold fast (Anchored units too, once Anchored ships). The sphincter
  valve Cord (Reverse). It breaks doorway turtles. Never in a Void room.
- **The Gullet** (renamed from "the Throat", 0.1 names). A sphincter
  Void; the acid fall band, Fortitude or Corroded; the Snag is a lower
  fold.
- **The spore pore** struck vent.
- **The Acid Tide** (Q3). A route-closer, not a damage source: its two
  states are bile sockets (so it is not an RS 8.10 source, 6.5), with
  bile ≤ 12% and the dry-route check holding in both. Its valve is a
  Flip Cord (Q3).
- **Digestion** as the Omen "Fasting" only.

### 7.10 The Dreamer's Heart (F5)

**M2:** the **Thorn Garden** tableau (thorn-rose harm patches,
pincushion faces); bedsheet squares if RT-14 keeps them.

**M3:**
- **The Dark Under the Bed.** A perimeter Void along the bed rim, the
  psychic fall band, the wardrobe as the Snag's prop ("you tumble out of
  the wardrobe": a Tall piece with its doors flush or absent, beside
  the Snag squares). ≤ 1 room per F5 until the class and Wrecker gates
  pass.
- **The Dreamer Turns Over.** Period 3. Its footprint is the
  **mattress**: a fixed set of the room's squares that excludes every
  square within 3 of entry, Snags, Hide spots, one Clearway lane and
  every Handhold-adjacent square (no room-wide Stir, 6.3; lead's call,
  after the check). Units on it make Reflex or fall Prone **unless**
  Flying, Rooted (or Anchored, once it ships), **or they did not move
  on their last turn** ("lie still and it passes"). Startle on skittish
  dreamers.
  - The music box Cord (Hold), worked by the Lullabist.
  - V10 requires ≥ 2 × the maximum party size Handhold-adjacent squares
    within 3 of entry.
  - **Gate:** a hero-to-monster Prone ratio ≤ 1.2; otherwise it moves to
    Nightmare tiers.
  - The breath cue; the mattress heaves only during the act (8); never
    a body (RS 7.5).
- **The Alarm Clock.** A Stir with a 6-round countdown (`firstAct` 6,
  `maxActs` 1). Its footprint is the ring of 3 around the clock (under
  the 6.3 exclusions): units on it make Will or are Dazed. The same act
  fires Startle (a morale event, no squares) for every group in the
  room and a peal of 10 within joiner caps. Cords: **Wind Back** (Hold
  with a slip of 3, once) and **Ring Now** (Now). A Herald party may
  ring it deliberately.
- **Falling Toys.** Period 2. Shadow sockets, the contact band, Reflex
  negates. No residue.
- **Counting Sheep.** The surreal Lurch showpiece and the first M3
  Lurch: figment sheep cross a lane every 3 rounds. Between acts the
  sheep sit in a pen (a non-walkable fold) at the lane's end, which
  carries the host glyph and the countdown; during the act they are
  flat, translucent figments (8). A benign soft stop at every tier
  (lead's call, after the check: the proposal's "hazardous toward a
  Void at Nightmare tiers" broke the Void and Lurch rule, 6.2.2).
- **Toss and Turn** (Q3; exists only if RT-14 keeps the bedsheets). A
  Tide whose two pre-proven states swap the room's bedsheet-web sockets
  between two layouts (Restrained on entry, the home square), period 3,
  first Turn at the end of round 3, held whole while occupied (6.3,
  Turn part). The Q3 Tide family; it is the room's clock.
- **The dust-pillow seam.** The Heart's vent (6.7).

**Later:** the Nightlight (light). **Rejected:** "the floor is lava"
(RC6, tone).

### 7.11 The Inverted Cathedral (M4)

- Rose-window Voids look down into sky. You **fall upward into light**
  and land at the chandelier Snag (a fallen fixture on non-walkable
  squares beside the landing). The sky is desaturated and clamped below
  the RC6 Void backdrop ceiling; the "light" is carried by the lip rim
  and the fall VFX (8).
- Up to 50% of rooms; Plank catwalks.
- The censer-rope Cord: a standalone Cord whose contact sweep across a
  Void resolves as Now (6.3.4).
- **The Chimes Ring Upward:** a Stir that declares a reversal round
  (pushes resolve as pulls), announced on the Dream card (Q3, RS 7.6's
  "shifting rooms" and "gravity or height" clauses, HZ-18). The rules
  stay flat: only the push direction reverses.
- Its "light" Law waits for NM-9.
- Church cap (RS 7.6) holds: never in a pool set with the Choir.

---

## 8. Readability, art, VFX and audio (change HZ-29)

**(lead's call; stands as written, with the check's additions marked)**

- **RC1:** every Void is a Void; no fake holes. Backdrop depth never
  reads as walkable. A fall leaves no residue. Glyphs only where rules
  apply. A set piece never moves dressing on walkable squares.
  **Floor-level motion (lead's call, after the check, replacing the
  proposal's "the Turning Ring boards are the one floor animation"):**
  motion on walkable squares happens only during an act animation (≤
  1.0 s, SC10), never at idle, and only for the records on this list:
  the Turning Ring's rotation (Q3), Peristalsis's band step, a Tide's
  fill change, and the Dreamer's heave. At idle a travelling band is a
  static motion mark. None may read as a square kind.
- **Figments (lead's call, after the check):** the Stampede herd and
  Counting Sheep exist only during their act, as flat, translucent,
  non-unit figments (no base ring, no shadow, below unit value) that
  fade by the act's end (RC2). "Herd or sheep figment against monster"
  joins the role-ID gate.
- **RC4:** Voids carry the Void rim (below) but no damage-hue fill.
  Bile green stays reserved; the Gullet backdrop uses flesh tones, never
  bile green. **Due overlays and latent footprints always use the
  telegraph-tier pattern and colour, with the damage-type icon. The
  fire hue appears only in act VFX and on actual fire squares or Hot
  pieces** (reworded after the check).
- **RC5:** one Void lip glyph across themes. One Snag glyph. One Cord
  glyph with used and unused pips. One vent glyph with trigger pips.
  Stirs use their host's glyph plus a countdown numeral on the host's
  floor decal (6.3). The marks are placed in RT's composable table
  below.
- **RC6:** units > telegraphs (due overlays, push and fall previews) >
  hazards (latent footprints included) > floor > walls > **Void
  backdrop** (new bottom tier, after the check). The backdrop's value
  ceiling sits below the floor's darkest walkable tone.
- **The Void backdrop (lead's call, after the check):** each Void region
  renders a sealed shaft mesh below the floor plane, generated from the
  region outline and fading to theme fog, so no level geometry, skybox
  or neighbouring room shows through at any of the 4 camera rotations
  (VP5 puts Voids on the perimeter, where walls are cut away).
- **RC7 (flagged; agreed charter text):** reserved cues, each in a
  timbre family no music bed uses (table below):
  - universal: the party fall (a held breath, then a gasp at the Snag);
    a distinct monster-removal cue; Cord Operated; the **Dream tick**, a
    theme-neutral tick on the Dream card each Dream step;
  - **each Stir has two layers (lead's call, after the check):** an
    announcement sting at Dream step 4 and an act cue at step 1;
  - per shipped Stir: the Organ (an air-rush intake, then an electrical
    crack), the Pit (a bellows inhale, then a whoomph); later the lamp
    roar, timber crack, ring ratchet, drumroll, course bell, gut
    gurgle-pop, the breath, the alarm ring;
  - floor 1: the small-hour chime, the Horn Post rattle, the candle
    exhale;
  - the struck bronze already exists (TR-20).
  - **Skipping or batching cuts animation, never cues.** Cues within one
    Dream step play serialised in step order, at least 150 ms apart
    *(illustr.)*.
- **Colour-blind and greyscale:** the four composable marks (base,
  motion, danger, cover), the pattern registry below, a digit plus a
  clock-pip for countdowns. Deuteranopia, protanopia, tritanopia and
  greyscale passes with every state in frame. "Water square against
  Void" joins the greyscale gate.
- **Tone:** comic, then dry, then absent by floor (RS 7.3 pillar 4).
  Never humour on telegraphs or deaths. Sinister, never cute (Counting
  Sheep's placid wrong faces). Nothing shows the Dreamer (RS 7.5).

**RC5+ extension table (lead's call, after the check; extends RT's
four-mark table, RT 8.1):**

| Record | Base mark | Motion mark | Danger mark | Cover mark |
|---|---|---|---|---|
| Void | Lip glyph on the walkable-side edge of each Void square; no fill | — | A new **drop rim**, distinct from the dashed harm rim and the solid piece rim | — |
| Snag | Snag glyph plus link glyph to its Void | — | — | — |
| Cord | Cord glyph with used / unused pips | — | — | — |
| Vent | Face: vent glyph with trigger pips; mouth: base mark | — | Hazard-face rim (RT) | — |
| Latent footprint | Thin outline in the host's rim style | Lurch arrows, if any | Hazard tier | — |
| Due square | — | Lurch arrows, if any | Telegraph pattern, ticking fill, countdown digit | — |
| Tide socket | Next-state mark | — | Telegraph tier when due | — |
| Lurch lane | — | Painted arrows | — | — |
| Handhold | Handhold base mark on the piece | — | — | Its cover mark, if a cover piece |
| Chainpost | Piece rim | — | Reach-ring outline; telegraph tier when due | — |
| Staked pit | Harm-square row (RT): inset dashed rim, piercing icon | — | Harm tier | — |

**Pattern registry (lead's call, after the check):** every hatch,
stipple and fill in use is listed once: Rough hatching, water ripple,
web lines, bile stipple, harm-square dashed inset, Remnant haze, due
ticking fill, Tide next-state mark, latent outline. No two patterns may
be told apart only by hue.

**RC7 timbre table (lead's call, after the check; checked against the
RS 8.x Sound lines):**

| Cue | Timbre family | Kept out of |
|---|---|---|
| Organ (Choir) | Non-pitched air and electrical crack | The Choir bed's organ and choir |
| Pit (Carnival) | Bellows air and low burst | — |
| Turning Ring | Mechanical ratchet | The Carnival bed's calliope |
| Gut Stirs | Wet gurgle-pop, short | The Gut bed's wet drones (no sustained tones) |
| Heart Stirs, the breath | Dry breath, no pitch | The Heart bed's lullaby and music box |
| Horn Post (Kennel) | Wooden rattle | Horns (RS 8.4 reserves them for joiners) |
| Small Hour Strikes | Low chime | Alarm-bell timbre |
| Dream tick, the fall, Cord Operated | Theme-neutral foley | Every bed |

The Heart bed's quotations of earlier floors' scores are stripped of
every reserved timbre (a request to the RS and audio owners, HZ-29).
**Gate:** an audio blind-ID test beside the 85% role-ID gate:
playtesters name each cue from the bed mix at ≥ 85%.

**Art budget** *(illustr.)*: about 5 assets per dynamic record (idle
loop, latent outline, due tell, act animation, the two-layer cue). M2
(gated) needs 2 Stirs (both built to the playtest; 6.3), 2 Void skins
with their backdrop shafts, 1 lip glyph and 2 Snag props (the dry font
and the returns desk), plus shared items: one fall VFX (a dissolve into
depth, no per-rig animation), one Snag-arrival VFX, a generic Clutch
edge VFX with the "holding on" pip (the Prone pose reused, never
per-rig), and the monster-removal cue. M3 adds Snatch and Plunge VFX
and the other Snag props (coffin, wardrobe, net, fold, chandelier) as
separate items. **Tableaux** are the art team's per-theme set-dressing
brief for M2. Every countdown must be legible at 4 rotations with the
camera-side stub (`TemplateReadability`), before the M2 Organ
playtest; Void rooms at 4 rotations join the same check.

---

## 9. New mechanics considered

### 9.1 Table

| Mechanic | Verdict | Wave |
|---|---|---|
| N1 Dream step and Stirs | Adopted, scoped to one signature Stir per F2–5 floor in M2 **(agreed, Q1)** | M2 (gated) / M3 |
| N1 Warned or floored | Adopted, with "had a legal move" added, and voluntary entry counted as Warned (6.3.2) | With Stirs |
| N2 Contraptions | Merged into Cords; Flip and Kick are M3 (Q3) | M2 re-time only |
| N3 Dreamfall, Snag, Clutch | Adopted **(agreed, Q2)**; half-HP Clutch rejected; Let Go Later | M2 (gated) / M3 |
| N4 Plunge | Adopted as an out-of-combat shortcut; in-combat Swallow rejected | M3 |
| N5 Lull | Optional; never bound in the toy. Held as a trinket hook | Later |
| N6 Tides | Adopted under phase-socket rules **(agreed, Q3)** | M3 |
| N7 Floor meters (Moods) | Omen and Nightmare only, capped triggers | M3 |
| N8 Dare rooms | Adopted on the existing out-of-combat tick, with anti-brute-force | M3 |
| N9 `floorborn` and Stoke | Stoke adopted (never makes an act due in a round it was not due at round start); `floorborn` as one tag immunity, no horizon-2 read | M3 |
| N10 Snatch | Adopted, no damage | M3 |
| N11 Chainposts and turning faces | Adopted | M3 |
| N12 Vents | Struck by default; breathing as a Stir | M3 |
| N13 Dream Rules pages | Adopted inside RS's M3 codex: records, lore and replays only; never gates rule text **(new lead's call:** not a separate codex**)** | M3 / Later (replays) |
| N14 Ferry and Shift | Merged into Lurch (benign and hazardous forms); "Shift" is W-8's forced-movement kind | M3 |
| N15 Environment sources for K1–K5 | Adopted, **except K3** (never started by the environment) | With Stirs |
| N16 Escalation | Adopted (Omens, Nightmare tiers, kind not stats) **(agreed, Q4** for floor 1**)** | M3 |

### 9.2 Adopted in full (those not already in section 6)

- **Chainpost (M3, 2 points).** A non-walkable hazard piece with a reach
  ring of 1. Every 2 rounds it bites the one unit in its ring that is
  first in fixed square order **at act time**: the harm band, Reflex for
  half. The due ring is announced like any footprint, and its label
  updates live as units move, always naming the unit that would be
  bitten now ("bites: the Strongman"; `PreviewExactness` covers it;
  lead's call, after the check). It counts as the room's clock. Never
  within 3 of entry. No AI, no faction.
- **Turning face (M3).** A contact face whose live side alternates
  between its two front squares at each act. An orientation state, no
  square change. The cheapest rhythm piece.
- **Stoke (M3; the enemies owner; HZ-24).** A monster kit action that
  moves a Stir's countdown 1 toward acting: C becomes max(2, C − 1)
  (6.3, Timing). It never makes an act due in a round where it was not
  due at round start, so SC2 holds; at C ≤ 2 it has no effect and the
  AI does not score it. Telegraphed intent. One Stoker type per theme.
- **Startle (M3; HZ-24; the enemies owner).** A new edge-triggered ENM
  14.3 morale event, fired by an act or a Cord. Named groups'
  skittish, non-Emboldened members become Demoralised. The same FIFO
  checkpoint. Fearless and mindless units are unaffected. Named Startle
  because "Rouse" is a Herald ability.
- **Dream Rules pages (M3).** Surviving a hazard unlocks a lore line, a
  record (times survived, monsters it claimed) and, Later, a
  deterministic replay of the best environment kill, as pages of RS's
  M3 codex. Never power, never rule text (RC1, main 4.7).

---

## 10. Numbers and fixes (illustrative; toy, not simulator)

All numbers are toy output *(illustr.)*, not the M2 simulator. The toy
reproduces TN6's base environment kill share (0.73% against "≤ 0.9%").

**Run level (600 runs each):**

| Metric | BASE | A | B | C | Lead catalogue | N |
|---|---|---|---|---|---|---|
| EnvKillShare | 0.73% | 2.15% | 1.65% | 1.45% | 2.13% | 2.09% |
| — of which falls (points) | 0 | 1.44 | 0.83 | 0.71 | 1.44 | 1.36 |
| — of which Stir acts (points) | 0 | 0 | 0.04 | 0.03 | 0.06 | 0.06 |
| Hero deaths from environment | 0.1% | 0 | 0.2 | 0.5 | 0 | 0 |
| Fight minutes per floor | 20.39 | +0.23 | +0.44 | +0.29 | +0.44 | +0.51 |
| Boss Teetering uptime | 13.1% | 13.7 | 13.9 | 15.0 | 14.6 | 14.2 |
| StirBothWaysShare | — | — | 16.7% | 23.6 | 16.5 | 15.4 |

**Matched rooms:**
- Stir fights cost +7.5 s (solo F2), +13.6 s (duo), +20–23 s (party
  3–4). Rounds change by −0.07 to +0.11.
- Void rooms reach 6.4–7.7% environment kills in-room, but add 0.10–0.18
  rounds.
- Snag: 1.6 rounds lost per fall. Removed until round end: 1.9. Swallow
  into another room: 3.5–3.9, with hero deaths doubled. The nearest safe
  square: 0.
- No Clutch: 8.5–9.1% in-room. Half-HP Clutch: 4.2–4.7%.
- Void arenas: boss Teetering 26–33%.

**Other measurements:**
- **Void density (run level):** 30% of rooms F3+ gives 2.1%; 50% F3+
  gives 3.0%; 50% F2+ gives 3.5%; one signature Stir room per floor adds
  +0.26 min per Stir floor. The toy reported it apart from Model A's
  +0.23 and did not say whether it includes it; this document reads the
  two as additive, the conservative reading (told plainly, below).
- **Stir kinds:** the Prone quake reaches 42–46% both-ways, but heroes
  go Prone 1.5–1.7× as often as monsters. Lurch: 17–20%.
- **Floor 1:** a benign Lurch is harmless.
- **Class spread:** Vanguard death 0.200 → 0.154–0.164; the Shade takes
  0% of environment kills.
- **Floor states:** per-room-cleared +1.7 min and F2 death 4.7% → 7.1%;
  per-rest invisible at toy rest rates; per long fight +0.31 min.
- **Worst one-round stack:** 52% (F2) of the lowest legal hero's HP, 46%
  (F3), 40% (F4), 35% (F5).

| Failure | Fix adopted |
|---|---|
| Stirs move neither kills nor rounds; reading costs +0.44–0.51 min | One signature Stir per floor in M2 (+0.26); SC13 footprint value; SC14 payoff; the contact band allowed for the signature Stir; the reading-time playtest before the pilot commits |
| StirBothWaysShare 8–20% | Gate redefined as "acts that changed a monster's move or hit it"; both-ways comes from held-in-place monsters and post-turn pushes, not a higher mindless share |
| The quake taxes the party | Handholds plus the "did not move" rule; ratio gate ≤ 1.2; M3 only |
| Voids lengthen fights | Perimeter and flank placement (F-b); re-measure `FightRoundsDelta` ≤ 0 |
| TN6 still missed | Voids are the lever: up to 40% of drop-theme rooms at M3; Clutch for elites once, not half HP |
| Boss Teetering > 25% | Narrowed F-c: no Teetering on bosses from a Void-edge success or Clutch; no Void within 3 of boss lanes; the reserve lever |
| Swallow doubles deaths | Rejected; Plunge out of combat |
| Class spread widens | Ship gate (gap ≤ BASE + 2); the Shade hook owed before Voids; the Void save ignores Teetering's disadvantage |
| Per-room meters too steep | Moods Omen-only, capped +1 per two rooms |
| 52% one-round stack | The EnvRound cap as a rule; no Keg in F2–3 footprints; ≤ 1 shover type (and ≤ 2 shover units near Void edges) in clock and Void rooms |
| Warned or floored and FallFloor unmeasurable on average | Kept as tail insurance; they make `ForecastLethalSurprise` = 0 by construction |

**Told plainly: TN6 projected by actual availability.** The toy's
3.0–3.5% assumed Voids in every theme from F2. Real availability:
- M2 (gated): Silk F2–3 and the Choir Crypt F3 only, about 1.5–2% run
  level;
- M3: the Gut F4, the Heart F5 and the Carnival High Wire added, about
  2.5–3%;
- M4: the Cathedral, about 3–3.5%.

The 3–8% band is a felt band, not a gate **(settled, RT 11.3)**.
Segment 4 reaches its floor only at M3 to M4.

**The clock (lead's call, told plainly; change HZ-27).** Segment 4 is
allowed ≤ +0.3 min per floor (`HazardMinutesDelta`, net; renamed after
the check so it never shares RT's `FloorMinutesDelta` id), *inside* the
TN13 terrain package's +1.6 min tolerance, not on top of it. No approved
budget existed. **After the check (lead's calls):**
- **The combined gate:** terrain plus hazards, `FloorMinutesDelta` (RT,
  read with segment 4 on) ≤ +1.6 min. This is what makes "inside"
  enforceable: terrain at +1.5 and hazards at +0.3 fails it.
- **Segment 5's line is separate:** traps and locks are a new budget
  line outside this envelope (on top of FL-16's commitment), reported
  apart with their own cut order (traps and locks design 6.21; change
  TL-38; M2).
- **The split** *(illustr.)*: static density (Voids, Tableaux, bile,
  vents) ≤ +0.2; the Stir's increment over static-on
  (`StirMinutesDelta`) ≤ +0.1, on Stir floors.
- **Told plainly:** the toy put static density at +0.23 and a Stir
  floor at +0.26; read as additive, about +0.49 together. On those numbers the M2
  Stir misses its clock gate unless the reading-time playtest measures
  it well under the toy's 4-second assumption, or static density comes
  in under its share; if it misses, Q1's own fallback sends it to M3.
- **The toy's absolute minutes are not comparable with the gates:** its
  BASE of 20.39 fight minutes per floor is already above companions
  7.4's 20 min and RS's 15–20 min floors, so only its deltas are used.

The RT risk-7 cut order is extended, after its own steps (Kick, Move
Shunt, Rough density, harm squares), by:
1. Moods;
2. Dare rooms;
3. second Stir kinds;
4. Lurch and Prone Stirs;
5. Tides;
6. Void density from 40% to 30%;
7. the signature Stir reverts to static density.

No cut touches a client number. **New lead's call (revised after the
check):** segment 4's cuts fire first when `HazardMinutesDelta` misses
its +0.3 **or when the combined gate misses**; RT's own steps fire
first when the terrain package misses its +1.6 with segment 4 off.

---

## 11. Data, validators and simulator (change HZ-28)

### 11.1 Records (C# rules core, JSON content)

```csharp
record HazardKitRecord(ThemeId Theme, SquareShareBand[] HomeShare, bool VoidTheme, int VoidMinTier /*0; the Wake 2*/,
  VoidSkin? Void, StirKindId[] Stirs, CordId[] Cords, VentSkin? Vent, TableauId[] Tableaux, MoodId? Mood,
  OmenId? Omen, int MinTier /*0; floor-1 Nightmare packages 2 or 3*/); // RS 8.0 `hazardKit`
record HazardPointTable(int[] RoomBudgetByFloor, RoleModifier[] Roles, Quota ClockRooms, Quota VoidRooms);
// Content (6.3)
record StirKindRecord(StirKindId Id, ThemeId Theme, HostKind Host, int Period /*2–4; Lurch ≥ 3*/,
  int FirstAct /*≥ 2*/, int MaxActs, StirPart[] Parts, BandClass Band /*Harm|Contact*/, bool IsSignature,
  SaveSpec Save, LurchForm? Lurch /*Benign|Hazardous*/, LurchArrow[] Arrows, PayoffSpec Payoff /*SC14*/,
  int HoldSlip /*1; the Alarm Clock 3*/, TellSpec Tell, RoleId[] Roles, int[] Floors, int MinTier, RuleCard Card);
record TideKindRecord(TideId Id, SocketKindState[] StateA, SocketKindState[] StateB, SquareKind[] HoldIfOccupied);
// Generation-time instance (6.3)
record StirInstance(StirInstanceId Id, StirKindId Kind, RoomId Room, SocketRef HostSocket,
  SquareRef[] Footprint /*sorted, fixed order*/, CordId[] Cords, int PhaseOffset);
record CordRecord(CordId Id, StirInstanceId? Stir /*null = standalone*/, CordVerb Verb /*Now|Hold|Reverse|Flip|Kick*/,
  SocketRef Socket, int Noise, UseScope Scope /*PerFight|PerFloor*/, int Uses, int Recharge,
  StandaloneEffect? Effect /*standalone only: effect, timing Now|AtOnce*/);
record RoomVoidSet(RoomId Room, VoidRegion[] Regions /*M2: ≤ 1*/, SnagId Primary, SnagId Secondary);
record VoidRegion(RegionId Id, SquareRef[] Squares /*2–10*/, DamageType FallType, BandRef FallBand /*contact, or harm*/,
  ConditionId? FallCondition, bool JumpCrossing);
record SnagSocket(SnagId Id, SquareRef[] Squares /*2–4, fixed order*/, PropRef Prop /*off the Snag squares*/);
record VentRecord(VentId Id, PieceRef Face, SquareRef Mouth, VentMode Mode /*Struck|Breathing*/, int MaxTriggers /*3*/);
// Runtime (saved)
record StirPhase(StirInstanceId Stir, int C, int ActsDone, bool HoldUsed, bool NowUsedThisRound,
  WarnedFlag[] Warned /*(unitId, round)*/);
record TideState(TideId Tide, int StateIndex, bool PendingHold);
record CordUse(CordId Cord, int UsesThisFight, int UsesThisFloor, int RechargeLeft);
record VentTriggers(VentId Vent, int Used);
record UnitFallState(bool ClutchUsed, bool HoldingOn, int ClutchRound, int HoldingOnClearsAfterTurnSeq,
  bool Groggy, bool FellThisFight, bool LosesNextAction);
record FloorHazardState(int MoodStage, int MoodCounter, int MenuIndex, int CapRound, StirPhase[] Phases,
  TideState[] Tides, CordUse[] Cords, VentTriggers[] Vents);
```

- `VoidRegion.FallType` must be in the theme's `damageTypes[]`; the
  default is the theme's `primaryDamageType` (6.4). V1 and V10 check
  it.
- Floor-1 Nightmare packages are `HazardKitRecord` entries tagged with a
  minimum tier (6.19) **(new lead's call)**; the Wake's Void is gated by
  `VoidMinTier` 2.
- The record split, the runtime records and the field names are a
  lead's call after the check; the draft's single `StirRecord` mixed
  content with generation-time data.

### 11.2 Stream and save

- A `hazards` child of the `terrain` stream, after the RT 6.2.5
  reservation step, with fixed draw counts (R-SEED-3). It draws the
  Stir choice, sockets, Snags and Menu permutations.
- No draws at act time except saves, on the combat stream.
- `hazardDigest` sits beside `terrainDigest`. FL 11.1's save fields gain
  `FloorHazardState` and `UnitFallState`.

### 11.3 V10 `RoomHazards`

Build time, after V9; HE-7 single retry at seed + 1. Per template ×
orientation × layer × state × floor band × theme × Mood stage × tier
class {< 2, ≥ 2} (the last two added after the check, so periods
changed by Moods and the floor-1 packages are proven).

| Check | Rule |
|---|---|
| `HazardPointsWithinBudget` | By floor and role; zero-hazard roles hold 0 |
| `OneClockPerRoom` | Never waived; a clock is any record with a countdown digit or meter (6.2.2), arenas included |
| `PeriodFloor` | No Mood stage, Omen or tier takes a period below its kind's floor (6.3.3) |
| `F1NoHazards` | Tier class < 2: no hazard of any family; Landmark ticks only, ≤ 1 per room, at main-path index ≥ 3 (Guttering Candles included). Tier class ≥ 2: the 6.19 package only, at its tier |
| `EntryCapsAllStates` | VP1, KP5 extended; RS 8.6 and 8.8 in every state |
| `ClearwayIntact` | Never Void, bile or fire; 2 wide in every state (VP2) |
| `FootprintExclusions`, `FootprintValue` | 6.3 "Never" list (no room-wide footprint); SC13, with a choke square defined as a cut vertex of the room's Clearway graph |
| `StirRound1Silent` | `firstAct` ≥ 2 |
| `VoidQuotasAndGeometry` | Drop themes (the Wake only in tier class ≥ 2); perimeter or flank; ≤ 2 starts within 2 (VP3, VP5); M2 ≤ 1 region per room; no Lurch Stir in the room; every `FallType` in `damageTypes[]` |
| `SnagProven` | Two sockets per room; on the party half (RT's bisector rule), all squares in the room, adjacent to the 3×3 interior of the landing-facing Portal mouth, never overlapping it; the 6.4 distances; reachable by Clearway pathing from `partyEntrySquares`; never water-connected to the Organ; no prop on a Snag square |
| `PushLaneExists` | VP4's (P, T) pair definition, in every state |
| `HingePosternClear` | The square and its 8 neighbours; no Void within 2 |
| `NoSanctumBridge` | LG3 |
| `HideNotOnEdge` | VP6 |
| `BossLaneClear` | VP7 |
| `HandholdDensity` | Heart quake rooms (7.10) |
| `TideStatesProven` | Both whole states × layer (M3); no mixed state exists (Turn part) |
| `BileRules` | 6.5, the dry-route check in every Tide state |
| `VentOnlyInPoisonThemes` | 6.7 |
| `ContactLethalityBound` (V9's, extended here) | Environment acts and fall bands after weakness (6.11 item 5); floor-1 packages at floor-1 levels (6.19) |
| `EnvRoundBound` | **A report, not pass/fail:** for one unit, the room's maximal per-round static stack (collision + contact + Keg + Stir act) is summed and flagged if it exceeds 2 × `CapRound` |
| `PlungeLinks` | 6.10 |

**New lead's call:** the proposal's typo `BoissLaneClear` is written
`BossLaneClear` everywhere. V10 joins RS 11.2's validator list beside V8
and V9. The runtime is a lookup that cannot fail (FL-15).

### 11.4 Reports and gates *(illustr.)*

Every row has solo, duo and four rows.

| Report | Target |
|---|---|
| `EnvKillShare` | 3–8% felt band (not a gate) |
| `EnvHeroDownShare` | ≤ 10% of hero downs |
| `StirHeroDownShare` | ≤ 8% |
| `HazardPushDeaths` (all environment) | ≤ 10% of hero deaths **(settled)** |
| Floor-1 environment deaths | 0 in the base game |
| `ForecastLethalSurprise` | 0 (any non-zero is a bug) |
| `PreviewExactness` (act and fall branches) | 100% |
| `StirRoundsDelta`, `FightRoundsDelta` (Void rooms) | ≤ 0 |
| `HazardMinutesDelta` | ≤ +0.3 min |
| `StirMinutesDelta` (Stir floors) | ≤ +0.1 min *(illustr.)* |
| `FloorMinutesDelta` (RT, terrain plus hazards) | ≤ +1.6 min (the combined gate, 10) |
| `EnvStepSeconds` | ≤ 1.0 |
| `StirBothWaysShare` (changed a monster's move or hit it) | ≥ 30% |
| `StirIgnoredRate` | ≤ 50% |
| `CordUseRate` | ≥ 20% |
| `DropRemovalShare` | ≤ 15% |
| `EliteRemovedBeforeFirstTurn` | 0 |
| `PartyFallsPerFight` | ≤ 0.25, also by shover count |
| `DownedAfterDreamfall` | 0 on the solo row; watch on duo and four |
| `RescuePushRate` | Watch |
| `FleeAfterFallRate` | Watch |
| `SnagPunishRate` | 10–30% (below 10%, falls are toothless: move the Snag further) |
| `FallsByWrecker` | Against `HewCeiling` |
| `ClassThemeSpread` with Voids | Vanguard–Shade gap ≤ BASE + 2 |
| `TeeteringUptimeOnBosses` | ≤ 25% |
| `QuakeProneRatio` | ≤ 1.2 |
| `HymnRoomRoundsDelta` | ≤ 0 |
| `EnvRoundCapBinds` | Watch |
| `DareBruteForceRate` | Watch |
| `BileCorrodedShare` by class | Watch |
| Ledgewalker trigger rate on Void floors | Watch |
| Floor-1 guard rows and `Floor1ClassSpread` per Nightmare tier (6.19) | As DH-26 and classes 5.6 |
| Floor-1 Rout rate per class with the Horn Post on (7.3) | As `Floor1ClassSpread` |
| Audio blind-ID (8) | ≥ 85% |

**Definitions (lead's call, after the check):**

| Report | Definition |
|---|---|
| `ClassThemeSpread` | The absolute gap between Vanguard and Shade run death rate, in percentage points, on drop-theme floors; BASE is the same metric with Voids off |
| `DropRemovalShare` | `voidFall` defeats ÷ all monster defeats, in Void-room fights |
| `TeeteringUptimeOnBosses` | Boss turn starts with Teetering ÷ boss turn starts, over all boss fights |
| `PartyFallsPerFight` | Party Dreamfalls ÷ Void-room fights, per party; solo, duo and four rows |
| `DownedAfterDreamfall` | Heroes Downed within one round of their Dreamfall placement ÷ Dreamfalls |
| `StirBothWaysShare` | Acts that damaged a monster, or where a monster's chosen destination differs when the scorer is rerun with the due-square term zeroed, ÷ acts |
| `EnvKillShare` | A kill counts as environment when the killing damage or removal came from an environment instance or `voidFall`, whatever `causedBy` names |
| `HazardMinutesDelta` | Segment 4 on minus off with segment 3 on in both arms (11.5); "off" = the hazard-point layer at 0, no Void, Stir or vent, RT 6.2.4's bases kept; rounds become minutes with the playtest-measured seconds per round plus per due round |
| `StirMinutesDelta` | The same, Stir on minus Stir off, static segment-4 content on in both arms |
| `RescuePushRate` | Dreamfalls caused by an ally ÷ party Dreamfalls |

### 11.5 Simulator arms

- Stirs off / on; period 2 / 3 / 4;
- Clutch none / elites-once / elites-once with the hold;
- FallFloor off / on; the Warned rule off / on;
- Void density 30 / 40 / 50%, perimeter or mid-room;
- the Void-save disadvantage on / off;
- the EnvRound cap off / on;
- the quake "still" rule against Handholds only;
- **the baseline arm (new lead's call, made consistent after the
  check):** segment 3 on and segment 4 off (as defined for
  `HazardMinutesDelta` in 11.4); `HazardMinutesDelta` is on minus this
  arm. RT 11.3's no-terrain baseline stays RT's own arm, used for the
  combined gate.

**The first measurement before any pilot commits:** reading time per due
round in playtest, which replaces the toy's 4-second assumption.

---

## 12. Staging

**(lead's call; the Q1–Q4 content agreed)**

| Wave | Contents |
|---|---|
| **M1** | Nothing. Pushes stop before hazards (C 10.1) **(settled)** |
| **M2** | The HZ-1 line; hazard points, roles and quotas; VP1–VP10; bile and the RS 8.10 reading; the RT-14 request with both guards; the EnvRound cap; Tableaux; the `hazardKit` field; V10, reports and the `hazards` stream; the clock allowance and cut order |
| **M2 (gated, Q2)** | Void, Dreamfall, Snag, FallFloor, Clutch with the hold, the landing-failure branch, the `voidFall` trigger, the arena Void line; pilots in the Silk (the Stacks Have No Bottom) and the Choir (the Drowned Crypt, F3); the Void backdrop shaft, fall and Snag VFX. Gates: the Shade hook delivered, spread ≤ BASE + 2, `FightRoundsDelta` ≤ 0, `DropRemovalShare` ≤ 15%, `TeeteringUptimeOnBosses` ≤ 25%, `PartyFallsPerFight` ≤ 0.25, `PreviewExactness` 100%; `DownedAfterDreamfall` 0 solo (added after the check) |
| **M2 (gated, Q1)** | The Dream step for at most one signature Stir per F2–5 floor (the Organ Swells and Pit Breathes, each shipping on its own theme gates, so only Choir and Carnival floors); the countdown; latent footprints and due squares; Warned or floored; bound Cords Now and Hold; the charter; two RC7 cue pairs and the Dream tick. Gates: the client's (the reading-time playtest, the TR-27 written pass, `StirRoundsDelta` ≤ 0, `HazardMinutesDelta` ≤ +0.3 with `StirMinutesDelta` ≤ +0.1 and the combined gate) and the lead's `StirBothWaysShare` ≥ 30% |
| **M3** | The broad Stir catalogue; Lurch; struck and breathing vents; Tides, Cord Flips (the Sluice Wheel, the Acid Tide valve), the Turning Ring (F3), the Returns Trolley, rails (Q3); standalone Cords; Landmark ticks; Moods as Omens; Dare rooms; Plunge; Snatch; Startle; Chainposts; turning faces; Stoke; the Heart and Gut Voids and the High Wire; the Dream Rules pages; Omens and Nightmare tiers 1–3, with the floor-1 packages from tier 2 (Q4); the Forbidden Course (the DH owner) |
| **M4** | The Inverted Cathedral, with the Chimes Ring Upward (Q3); two clocks per room tested at a Nightmare tier |
| **Later** | Let Go; cross-room drops (the Service Lift, deep Dreamfall); Lull; light hazards with NM-9 (the Long Night, the Lamp's eye, the Nightlight); wall-clock floor states only as an opt-in tier 5; replays |

**Fallback (lead's call):** each M2 (gated) row that misses a gate, or
whose owner rows (18) have not landed when M2 content locks, ships at
M3 instead; the client is told (19.2).

---

## 13. What segment 4 owns, and hooks handed to later segments

**(lead's call; stands as written)**

| Owner | Owns |
|---|---|
| **Segment 3 (room terrain)** | The piece and square classes, bands, caps, the Clearway, layers, the knock-into system, the combo table, the Remnant system, arena terrain rules and caps (RT 13, TR-1) **(settled)** |
| **Segment 4 (this)** | Floor-scale density (hazard points, roles, quotas); placement rules VP1–VP10; Voids and falls; bile density; Teetering counting (as a request); vents; Stirs, Cords, Tides, Lurch and the Dream step's content; Landmark ticks; Moods; Dare rooms and Plunge; per-theme catalogues and Tableaux; the `hazards` stream, V10 and the reports |
| **Segment 5 (traps, locks, checks)** | Traps (hidden, armed, sprung, one-shot), including the sprung twins listed in 6.1; door verbs (FL-8); the DC ladder (traps and locks design 6.2, 6.3, 13; change TL-10) |

**Rule of thumb:**
- a static piece or an object anyone can use in a room: segment 3;
- something that acts on its own, falls away, or sets floor-scale
  density: segment 4;
- something hidden or sprung: segment 5.

**Hooks handed off:**

| Segment | Receives from segment 4 | Owns |
|---|---|---|
| **5 Traps, locks, checks** | The HZ-1 line. Traps kept: the dumbwaiter drop, the toppling-shelf tripwire, the icicle chandelier, the sluice Soak, the acid geyser, trapdoor stages, the bell tripwire, the portcullis, the Appeals trapdoor, horn tripwires. EnvDC as a reference for the DC ladder. Cords never arm traps and need no check | DCs; traps; door verbs; how traps and hazards share a room beyond these defaults |
| 6 Encounter placement | Start sockets outside footprints and Tide states at fight start; ≤ 2 starts within 2 of a Void edge; ≤ 1 flyer, ≤ 1 shover type and ≤ 2 shover units within 4 of a Void edge in Void rooms; the `usesCords`, `singer` and Stoker tags; Wake candle racks on Dormant groups (index ≥ 3, ≤ 1 per room); Dare garrisons; the mindless share (the enemies owner). **Filled by the encounter placement design (6.1 exclusions, 6.6.8 Terrain Pairings, 6.13):** the guarded Dare garrison is the floor's Annex (change EP-26; agreed, Q2; M3) | Who starts where |
| 7 Loot | Fallen loot on the edge; Dare containers (+1 rarity step); no item loss. **Filled by the loot economy design (6.9, 6.14; change LE-10; M3):** the Dare cache is an SR5 step socket under the one step rule; fallen loot on the edge, no item loss (7c). **Changed by the loot sources design (4.7.7; change LP-2; agreed, Q1; M3):** the sole exception is that a Windfall's card and coin leave the floor when its shown count ends (`ItemDestroyed{gotAway}`); kill method, damage, terrain, traps, locks and force still never destroy loot | Containers and contents |
| 8 Events | Unguarded Dare forms; Moods on the Stratum Card; empty Tide naves as non-combat puzzles; the Gaol's small hours. **Filled by the events design (4.2, 4.9, 4.13; changes EV-31, EV-35):** no Curiosity is placed in a Dare or Wager room, which stays segment 4's (M2); segment 8 places no Landmark and no clock Landmark: FL owns the sockets, segment 4 the ticks (EV-35; M2); no Mood on the Stratum Card in M2, handed to segment 10 and segment 4, and in M3 an Omen card's cost may be the named, visible event that advances a Mood within 6.9's cap (EV-31; M3); empty Tide naves are not M2 Curiosities, and an empty nave may be an M3 Curiosity host only if segment 4 offers it (**open item**, segment 4's to offer; M3 option); the Gaol's small hours are a Glimpse and Retelling motif only, no rule (events design 5.3; M2) | Event content |
| 9 Boss arenas | 6.17: one clock and one Void line per arena inside RT 6.11, under the room rules; `noDrop`; the boss Teetering guard; counterplay identities kept | Interiors, counterplay, phases |
| 10 Exploration and UI | Stir rule cards on reveal; due overlays; countdown glyphs; the menu card; Snag glyphs; Portal hazard and Dare signposts; Listen hears tells; Plunge links | Overlays and UI |
| 11–12 Seeds and validation | The `hazards` stream; V10; `hazardDigest`; save fields; reports and arms | The stream tree, release seed set, retry logging |
| 2 Floor layout | Plunge as a Chute skin with an LG14 exception; the Hinge and Postern radius; the sluice-wheel piece reading | Graph invariants |

---

## 14. Binding requirements and how they are met

| Requirement | Source | How it is met |
|---|---|---|
| O1: per-theme catalogues and floor density, inside segment 3's caps | RT 13, FL 13, RS 13 | 6.2, 7 |
| O2: what a fall does | RT 6.5.7 | 6.4 **(agreed, Q2)** |
| O3: bile | RT-13 | 6.5 |
| O4: Teetering counting | RT-14 | 6.8 (the conditions owner rules) |
| O5: vents and `causedBy` | RT 6.6 | 6.7 |
| O6: flooding set pieces | RT 13 | The Hymn Tide (Q3); Tide High in the Carillon arena |
| O7: the Hinge and Postern radius | FL 13 | 6.2.2; `HingePosternClear` |
| O8: no crossable bridge into the Sanctum | FL 13, LG3 | `NoSanctumBridge` |
| O9: the Inverted Cathedral drop | RS 8.12 | 6.4, 7.11 |
| O10: Kennel staked pits | RS 8.4 | A walkable harm square at Nightmare tier 2+ **(agreed, Q4)**; re-read from "Later (drop hazard)", told plainly; 6.19 |
| O11: Wrecker value by density | Vanguard 8.3, 18 | `FallsByWrecker`; the spread gate |
| O12: hazard edges from floor 2 | trinkets 23, RT-20 | F1 dry in the base game; Voids on Silk and Choir floors; the owner is told |
| O13: DEP-A1 features | TR-21 | Stirs, Cords, Voids and vents count for KP2 (request) |
| O14: a chasm edge for shoves | ENM 17.5 | The arena Void line (HZ-11), else a contact hazard |
| Theme record fields | RS 8.0 | `hazardKit`; `placementRules`; `teeteringFallbackDrop` |
| One flat rules level; coplanar squares | main 4.3, 6.8 **(decided)**; LG18 | Voids are flat; depth is backdrop |
| No spreading surfaces | main 2 **(decided)** | Nothing spreads; square changes only through pre-proven states (Q3, flagged) |
| Guards every round | ENM 6.4 | 6.11 |
| No single failed save lethal to the hero | C 10.4 | FallFloor; Warned or floored; bands unchanged |
| Party floors | DH 10.4 | Extended (HZ-10, HZ-14) |
| Loot never destroyed (sole exception: a Windfall that gets away, M3; loot sources design 4.7.7; change LP-2) | ENM 15.6 | Loot on the edge |
| Experience sources | ENM 16 | The `voidFall` trigger |
| Clock gate | companions 7.4, RT TN13 | +0.3 allowance with its static and Stir shares; the combined terrain-plus-hazards gate; the cut order |
| Readability | RC1–RC9 | Section 8 |
| No light model | RS 7.6, ENM 5.1 | Light Later |
| Floor-1 safety | RT-20, RS 8.2, DH 17.1, ENM invariant 6 | 7.2–7.4; Landmark ticks only in the base game; 6.19 at tier 2+ |
| Determinism; fixed draws | main 6.3 **(decided)**; R-SEED-3 | The `hazards` stream; no act-time draws |
| Condition budget | C 16 | No new ids |

---

## 15. Risks

| # | Risk | Signal | Response |
|---|---|---|---|
| 1 | Clock: Stirs cost reading time and never shorten fights; Voids lengthen them until placed on the perimeter. On toy numbers the M2 Stir misses its clock share (told plainly, 10) | `HazardMinutesDelta`; `StirMinutesDelta`; the combined `FloorMinutesDelta`; `StirRoundsDelta`; `FightRoundsDelta` | At most one Stir per floor; the reading-time playtest first; perimeter Voids; the +0.3 allowance and its split; the extended cut order; Q1's fallback to M3 |
| 2 | The TN6 band is reached only at M3–M4 (told plainly) | `EnvKillShare` | Void density is the lever, not damage |
| 3 | Wrecker dominance; the Shade gains nothing | `FallsByWrecker`; `ClassThemeSpread` | The Void save ignores Teetering's disadvantage; the Clutch hold; the spread gate; the Shade hook as a ship gate |
| 4 | Falls feel toothless | `SnagPunishRate` | Snag distance, the Groggy state, the `SnagPunishRate` floor |
| 5 | Edges become a party safety net | `FleeAfterFallRate`; `RescuePushRate` | Let Go Later; the Snag ≥ 3 from the `fleeExit`; Groggy blocks Portal exits; a rescue push lands on the secondary Snag and costs the next action; no voluntary movement ends over a Void |
| 6 | The AI shovers farm party falls, or land a solo hero beside a hostile | `PartyFallsPerFight` by shover count; `DownedAfterDreamfall` | Once-per-hero fall value; the shover type and unit caps; the Snag choice checks hostiles at both sockets, with an overflow square |
| 7 | One-round stacks | `EnvRoundCapBinds` | The EnvRound cap as a rule |
| 8 | Players ignore Stirs and Cords | `StirIgnoredRate`; `CordUseRate` | Footprint value, the payoff rule, Cord placement and bonus-action Operate, cut-if-unused |
| 9 | Readability overload, visual and audio | The glance test; greyscale gates; audio blind-ID | One clock per room (any digit or meter), never waived; pips for everything else; one headline plus one sub-line; the RC7 timbre table |
| 10 | Owner load (conditions, DH, enemies, ABL, Vanguard, Shade, FL, RS, art) | Owner rows in 18 | M2 (gated) rows slip to M3 by default if owner rows have not landed |
| 11 | RT-14 refused | The ruling | The fallback drops named; the Supper vent and Toss and Turn cut |
| 12 | Validation growth (Tide states × layers) | HE-7 retry rate | One Tide per room, ≤ 2 states |
| 13 | Floor 1 shows little of its themes' environment in the base game (told plainly) | Floor-1 playtest notes | Landmark ticks at M3; Nightmare content (Q4) |
| 14 | The "not a lair action" reading may be disputed | The ABL owner | Falls back to "a set-piece hazard within the theme's 6" (ENM 17.5) |
| 15 | The Turning Ring reads as the carousel moving, against FL 7.7's spirit (told plainly) | Rotation playtest | Horses on poles; boards only; 4 camera rotations; cut to Later if it misreads |
| 16 | Floor-1 Nightmare hazards meet the solo hero | Per-tier floor-1 guard rows | 6.19 rules; floor 1's own bands; a failing package drops a band step, then moves up a tier |
| 17 | **All evidence is toy** | — | Every number is owed a simulator rerun before it settles |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: hazard points and budgets, quotas, bile
  density, EnvDC, the EnvRound cap's 30%, periods, `maxActs`, footprint
  sizes, Snag distances, noise values, Dare budgets and reward steps
  (the step itself: loot economy design 6.9),
  report thresholds other than the stated gates.
- The layer sockets, footprints and Snags themselves, within 6.2 and
  V10.
- Skins inside each theme's catalogue, within the rules key.
- Godot scene structure for the Dream card, due overlays and fall
  animation, with no physics.
- Art, VFX and audio inside the charter (8).
- Names, until the naming pass.
- **Not tuning:** visible from reveal, never found or disarmed; no
  random draw at act time; announced a full round ahead on exact
  squares; `firstAct` ≥ 2; Warned or floored **(agreed, Q1)**; one clock
  per room; the FallFloor, Dreamfall to the Snag, Clutch once with the
  hold, bosses never fall, and no Teetering disadvantage on the Void save
  **(agreed, Q2)**; pre-proven states only for the moving dream
  **(agreed, Q3)**; floor 1 hazard-free in the base game **(agreed,
  Q4)**; no spreading surfaces; escalation in kind, not damage; never
  scaled by party size; the EnvRound cap as a rule; build-time proof.

---

## 17. Changes to approved documents

Applied on 2026-11-21, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and rows owed by
other owners are listed again in 18. Every row names the exact section
it touches. Rows that touch **decided** or **agreed** text are flagged
in the "Flag" column; **the client's approval of 2026-11-21 covers
them** (with Q1–Q4 where a row names one; the answers of 2026-11-21
already agreed each of those rows in substance). Told-plainly rows were
told in 19.1 and confirmed by the client on 2026-11-21. HZ-6, HZ-13,
HZ-15, HZ-17, HZ-24 and HZ-30 are requests to other owners, recorded as
pending requests; they change nothing until their owner rules. M2
(gated), M3 and M4 rows are recorded now as applied text marked with
their wave: an M2 (gated) row takes effect in M2 only if its gates pass,
else at M3 (19.2), and M3 and M4 rows take effect at their wave.

**Narrowings recorded inside this design (no approved text changes):**
- Brink reads Voids, because they are squares (Vanguard unchanged);
- the floor-1 Landmark ticks are not hazards, so floor 1 stays dry in
  the base game (RT-20, RS 8.2 read as written);
- a due square is not a hazard for C 10.1's stop rule, the C 10.2 edge
  save or the C 10.3 landing save;
- the K-table stays closed: Stirs feed K1, K2, K4 and K5 only.

| # | Document / section (current status) | Change | Flag | Approved by |
|---|---|---|---|---|
| HZ-1 | RT 13 (TR-1 ownership rows); FL 13 hand-off row "4 Hazards"; RS 13 "Hazards" and "Traps and locks" rows (**agreed** hook text, filled); C 11.3 (known armed traps, unchanged); FL 7.5 (told: the sluice wheel is a piece) | The hazard / trap / terrain line; open against closed trapdoor; horn post against tripwire; the sprung twins stay segment 5's; boss counterplay stays segment 9's (6.1) | **Agreed** (RS hooks, filled as a reading) | Lead's call; owners apply |
| HZ-2 | RT 6.2.4 (a layer on its density table); RT 6.2.5 reservation order; FL 6.3 | Hazard points, the role table, quotas, the party-size rule, the `clock` and `void` reservation tags (6.2) | — | Lead's call |
| HZ-3 | RT 6.2.4 KP1–KP9 (KP5 extended); RS 8.6 and 8.8 (applied per state); FL 13 (Hinge and Postern radius); FL LG3 | VP1–VP10 (6.2.4) | — | Lead's call |
| HZ-4 | C 11.2 bile row and contest field (RT-13); C 11.5; RT 7.9 | Bile density and the Fortitude save (6.5) | — | Lead's call; conditions owner applies |
| **HZ-5** | RS 8.10 "Balance caps" bullet ("at most 2 acid-dealing units per encounter, besides bile squares"; **agreed**, RS Q4); RT 6.3.5, RT 7.9 | "Acid units" becomes "acid sources", bile exempt (6.5) | **Agreed** (RS Q4 catalogue); told plainly | Client, with this document; RS owner reconciles |
| HZ-6 | C 16 (RT-14 pending); RS 8.0 counting bullet and `teeteringFallbackDrop`; RT 6.5.5 | RT-14 "does not count", the kit guard, the boss guard (6.8) | — (request) | Conditions owner, before M2 rosters freeze |
| HZ-7 | RT 6.4 (the one-instance cap, TR-13 wording); C 10.4; ENM 6.4 | The EnvRound cap: a round-scope top-up for party units (6.11) | — | Lead's call; conditions and enemies owners apply |
| HZ-8 | RS 8.0 theme record (lead's call field names); TR-22 `terrainKit`; RS 7.5 (checked: nothing shows the Dreamer) | Tableaux; the `hazardKit` field (6.2.2, 11.1) | — | Lead's call; RS owner applies |
| **HZ-9** | C 10.2 (a Void branch; the area-cap sentence becomes "a contact hazard, harm square or Void", one counter per use); C 11.3 (Void squares are hazards); C 18.4 Teetering ("its next save against being forced into that hazard has disadvantage"); C 19 interaction table, combo row "Teetering + forced movement" (the Void save excepted); RT 6.5.1 drop row; RT 6.5.7 (replaced); main 4.3 (**decided**; used, unchanged) | The Void square class; the edge-save Void branch; the Void-fall save ignores Teetering's disadvantage; Clutch with the hold; `noDrop` without Teetering (6.4) | **Agreed (Q2)** | **Client (agreed 2026-11-21, Q2)**; conditions owner applies |
| **HZ-10** | DH 10.4 (**agreed**, DH Q6: a new in-combat fall row); C 10.3 (a failure branch); C 10.4; DH 17.7 (rescue push); ENM 16 (the `voidFall` payout); ENM 14.3 (removal as a loss event); ENM 15.6; ENM invariant 7; trinkets TK-2 (Ledgewalker reads Void saves) | Dreamfall, the FallFloor, Groggy, the second-fall rule, the Snag and its choice order, the landing-failure branch (EnvDC; no voluntary movement ends over a Void), the rescue push (secondary Snag, the next action lost), `voidFall` paid as a kill (6.4) | **Agreed** (DH Q6 extended); **Q2** | **Client (agreed 2026-11-21, Q2)**; DH, conditions and enemies owners apply |
| HZ-11 | RT 6.11 (TR-16 arena caps); ENM 17.5 ("a chasm edge for shoves") | The arena Void line, not counted as a contact hazard (6.4) | — | Lead's call; segment 9 inherits |
| **HZ-12** | C 5.4 round-end environment step (TR-27: wave M3 → M2 for Stirs only; its sub-steps, 6.3); **RT 0.1 item 5, RT 0.2 row 13 and RT 6.6 "M2: nothing timed and nothing spreads" (agreed, RT Q5)**; ABL 2 "Lair actions" (lead's call; a flagged reading); ABL 5 / ENM 13.2 (not a wind-up) | The Dream step in M2 for one signature Stir per F2–5 floor, gated (6.3) | **Agreed** (RT Q5 reopened) | **Client (agreed 2026-11-21, Q1)**; ABL owner told |
| HZ-13 | C 11.3; ENM invariant 7; DH 17.3 (the threat list gains "environment acts due before the hero's next turn"); DH 17.4, 17.5, 17.6, 17.12; RT 6.12 | Due squares; previews; the danger band includes environment acts (6.3.1, 6.18) | — (request) | DH and enemies owners |
| **HZ-14** | DH 10.4 (**agreed**, DH Q6: a new row); DH 17.5 (marks) | Warned or floored (with a legal move), shown (6.3.2) | **Agreed** (DH Q6 extended) | **Client (agreed 2026-11-21, Q1)**; DH owner applies |
| HZ-15 | ABL 7.2 (a new object action, Operate, and the bonus-action rule); RT 6.3 grammar; RT 6.4 (on-kill never from acts); RT 6.9; ENM `usesFurnishings` (extended by `usesCords`) | Cords and Operate: bound Cords (Now, Hold; Reverse, Flip and Kick at M3) on the countdown C, and standalone Cords; the credit rule (6.3.4) | — (request for the bonus-action rule) | Lead's call; ABL owner rules on the bonus action |
| HZ-16 | RT 6.6 sources row; C 11.1 (Remnants already sanctioned); RT 6.8 K5 | Struck and breathing vents; `causedBy` (6.7) | — | Lead's call; applies at M3 after the TR-27 gate |
| HZ-17 | C 10.1 (a third named exception class); weapons 7.2 rule 4 | Lurch (soft stops only; benign and hazardous forms; the downstream-first area-cap order; the floor-1 tier-2+ exception), holds fast for Rooted, Restrained, Hidden and Handhold-adjacent units, and Anchored once it ships (6.3.3) | — (request) | Conditions owner; M3 |
| **HZ-18** | **Main 2 Non-goals bullet "Spreading, evolving or player-placed ground surfaces …" (decided)**; **C 11.1 sanctioned list (agreed; a new row "pre-proven Stir states")**; C 11.2 (a state change under a unit is not an entry); C 11.4; **RS 7.6 "shifting rooms" and "gravity or height (main 4.3, 6.8)" (agreed; Later → M3, the reversal round M4; the rules stay flat, only the push direction reverses)**; RS 8.8, FL 7.7, RT 7.7 (the carousel's spirit; told plainly); **RT 6.3 "Furnishings move only by verbs" (agreed, RT Q2)**; RT 6.3.7; RT 6.11 (precedent); stat model 2 Non-goals (agreed) | Tides (held whole, 2 states only), Cord Flips, Turning Ring boards, environment-moved Furnishings (the Returns Trolley), environment Overturn, the reversal round (6.3, 7) | **Decided** (main 2); **Agreed** (C 11.1, RS 7.6, RT 6.3, stat model 2); told plainly | **Client (agreed 2026-11-21, Q3)**; applies at M3 (the reversal round at M4) |
| HZ-19 | RS 8.2 floor-1 terrain bullet and RT-20 (read as written; told plainly); TR-29; DH 17.1; ENM invariant 6; FL-8 (a Portal source); FL 6.6 (the mute applies to party verbs only) | Landmark ticks: a non-hazard class legal on floor 1 (no damage, condition or unit movement) (6.1, 7.2–7.4) | — (told plainly) | Lead's call; trinkets and FL owners told; M3 |
| HZ-20 | RS 6.9 (the Stratum Card line); RS 9.1 Omens row; FL 9.1 (not a floor-wide alarm); FL 11.1 (save) | Moods as Omens and Nightmare only, capped triggers (6.9) | — | Lead's call; with the client's Omens package (RT-19); M3 |
| HZ-21 | FL 6.3, 6.8; C 5.5; DH 10.4 | Dare rooms on the out-of-combat tick; anti-brute-force (6.10) | — | Lead's call; M3 |
| HZ-22 | ABL 7.2 / 7.3 (a reaction row) | Snatch, with no damage (6.4) | — | Lead's call; ABL owner applies; M3 |
| HZ-23 | FL 6.1 / 6.2 graph invariants; FL LG14 (an explicit Plunge exception to the return-Portal principle); FL 9.1 Chute row (Plunge as a Chute skin); FL 6.9 | Plunge, a one-way out-of-combat link (6.10) | — (request) | FL owner; M3 |
| HZ-24 | ENM 14.3 triggers table (a new edge-triggered event); ENM 8.3; ENM 13.2 | Startle; Stoke; `floorborn` as one tag immunity (9.2) | — (request) | Enemies owner; M3 |
| **HZ-25** | RS 9.1 (Omens, Nightmare tiers); **RT-21 / main 4.7 "Difficulty ladder" (decided: "add kind, not stats", read, not changed)**; RS 8.2 floor-1 terrain bullet and **RT-20 (Nightmare tier 2+ only)**; RT 6.5.6 ("no hazards of any kind" on floor 1, tier 2+ only); RT 7.2, RT 12 and RT 19.2's floor-1 Hot-piece row (the Gaol stove promoted at tier 2+, under its floor-1 `HazardPushDeaths` gate); **RS 8.4 Hooks bullet "staked pits are Later (drop hazard)" and RS 13 Hazards row (agreed, RS Q4)** (re-read as a walkable harm square at tier 2+; told plainly); RS 6.3 "deliberately dry" (kept: the pit is condition-free); trinkets 23 ("floor 1 is dry by design", the RT-20 row) and RS 14 "Hazard edges per floor" (floor 1 gains hazard edges at tier 2+); C 10.1 and 6.3.3 (Lurch on floor 1 at tier 2+) | Escalation in kind; floor-1 hazard packages at Nightmare tier 2+ (6.19) | **Decided** (main 4.7, read); **Agreed** (RS catalogue text); told plainly | **Client (agreed 2026-11-21, Q4)**; M3 |
| HZ-26 | RS 8.12 (the Inverted Cathedral row); RS 12 | The Inverted Cathedral package (7.11) | — | Lead's call; M4 |
| HZ-27 | RT TN13; RT 15 risk 7 (the cut order); RT 11.3 `FloorMinutesDelta` (read as the combined gate); companions 7.4 (no new lever) | The segment-4 clock allowance (`HazardMinutesDelta` ≤ +0.3 inside the +1.6 tolerance, split into static and Stir shares); the combined gate; the cut order extended (10) | — (told plainly) | Lead's call |
| HZ-28 | RT 11; FL 11, FL 11.1 (save fields); RS 11.2 validator list; R-SEED-3 | V10, reports and their definitions, arms, the `hazards` stream, `hazardDigest`, the split records and save fields (11) | — | Lead's call |
| **HZ-29** | RS 7.4 RC1, RC4, RC5, RC6, RC7 (**agreed**, RS Q3 charter); RC8 (barks); RT 8.1 four-mark table (extended); RS 8.4, 8.6, 8.8, 8.10, 8.11 Sound lines (read; the Heart bed's quotations stripped of reserved timbres, a request) | Readability rules; the floor-motion whitelist and figments; the RC5+ table and pattern registry; the Void backdrop tier; the reserved RC7 cues, two layers per Stir, the Dream tick and the timbre table (8, 6.18) | **Agreed** (RS Q3 charter, extended) | Client, with this document; RC and art owners apply |
| HZ-30 | DH 12.1 sources; DH 12.4 `HealSupplyPerFloor` | The Forbidden Course (a party-only environment heal, ≤ 2 per fight) (7.8) | — (request) | DH owner; M3 |

Waves: HZ-1 to HZ-8, HZ-13, HZ-15 (re-time verbs), HZ-27, HZ-28 and
HZ-29 (M2 parts) in M2; HZ-9 to HZ-12 and HZ-14 in M2 (gated), else M3;
HZ-16 to HZ-25 and HZ-30 in M3; HZ-26 in M4.

---

## 18. Depends on systems not yet designed

The owed rows are the single list in 17; by owner:

- **The conditions owner:** HZ-4 (11.2), HZ-6 (the RT-14 ruling with
  both guards, before M2 rosters freeze), HZ-7 (10.4 wording), HZ-9
  (10.2 and its area-cap sentence, 11.3, 18.4, the C 19 combo row),
  HZ-10 (10.3, 10.4), HZ-17 (10.1, M3), HZ-18 (11.1, 11.2, 11.4, M3),
  HZ-25 (10.1 floor-1 Lurch at tier 2+, M3).
- **The DH owner:** HZ-10 (10.4 fall row, 17.7), HZ-13 (17.3–17.6,
  17.12), HZ-14 (10.4, 17.5), HZ-30 (12.1, 12.4).
- **The enemies owner:** HZ-7 (6.4 guard rows), HZ-10 (14.3, 15.6, 16,
  invariant 7), HZ-11 (17.5), HZ-13, HZ-15 (`usesCords`), HZ-24
  (Startle, Stoke, `floorborn`), the 6.12 AI requests, the mindless
  share.
- **The ability model owner:** HZ-12's "not a lair action" reading,
  HZ-15 (Operate and the bonus-action rule), HZ-22 (Snatch).
- **The class owners:** the Shade hazard hook (a Void ship gate, 6.15);
  the Arcanist's second Gust charge in Void rooms and TR-21's DEP-A1
  reading; the Vanguard `HewCeiling` removal term (TR-30's owner); the
  Trapwright's Jury-Rig re-time hook.
- **The trinkets owner:** HZ-10's Ledgewalker reading; told that floor 1
  stays without hazard edges in the base game (HZ-19); HZ-25 (trinkets
  23: floor 1 gains hazard edges at Nightmare tier 2+).
- **The RS owner:** HZ-5, HZ-8, HZ-20, HZ-25 (RS 8.4, 13, 14), HZ-26,
  HZ-28's validator list, HZ-29 (with the audio owner: the Heart bed's
  quotations).
- **The floor layout owner:** HZ-1 (FL 7.5 sluice wheel), HZ-3 (FL 13),
  HZ-19 (FL-8, FL 6.6), HZ-20 (FL 9.1, 11.1), HZ-21, HZ-23 (LG14
  exception, the Chute row), HZ-28 (FL 11, 11.1).
- **The room terrain owner:** HZ-2, HZ-3, HZ-7, HZ-11, HZ-15, HZ-16,
  HZ-18, HZ-25 (RT 6.5.6, the stove row), HZ-27, HZ-29 (RT 8.1's
  table).
- **Dungeon generation segments 5–12:** every hook in 13.
- **The simulator:** the arms of 11.5 and the reports of 11.4.
- **The art team:** the art budget of 8, Tableaux, the glyphs and cues.
- **Naming pass:** every placeholder name here.

---

## 19. Open items for the client

None. Approved by the client on 2026-11-21.

The client approved this document as a whole on 2026-11-21, including
every lead's call (the lead's calls of the answered proposal, the new
lead's calls in 19.1 and the lead's calls after the check in 19.1), the
told-plainly items (including "Told plainly after the check" in 19.1,
notably that Q1 is a cap in M2) and the partial narrowings recorded in
17, and its approval covers the flagged rows of 17. The four questions
were answered on 2026-11-21 **(agreed)**; the client also said
everything else in the proposal stands as written. The one item that
needed the client, Q1's reach in M2, was answered on 2026-11-21 with the
lead's recommendation (19.1). The future items in 19.2, which come back
only if a report or a later design triggers them, and the naming pass
remain future items.

### 19.1 This document

**The four answers (agreed, 2026-11-21), each the recommended option:**

- **Q1, one breathing room per floor in M2:** option A. Reopens RT
  decision 5 (the client's segment-3 Q5 answer, "nothing timed in M2";
  in section terms RT 0.1 item 5, RT 0.2 row 13 and RT 6.6)
  for exactly one signature Stir per F2–5 floor (the Choir's Organ
  Swells first choice, the Carnival's Pit Breathes the alternative),
  announced a full round ahead on exact squares, never in round 1,
  Warned or floored, shipping only if the reading-time playtest, the
  TR-27 written report pass, and the clock and fight-length gates pass;
  otherwise it falls back to M3 (6.3; HZ-12 to HZ-15). Rejected: B,
  nothing timed in M2.
- **Q2, the drop rule:** option A. Void squares; the party Dreamfalls
  (the fall band floored at 1 HP, Prone on the room's Snag, a Groggy
  state, a second failure stays on the edge); non-elite monsters are
  removed and defeated (`voidFall` experience); elites and leaders
  Clutch once and cannot fall until they have taken a turn; bosses
  never fall; Teetering's disadvantage does not apply to the Void save;
  pilots in the Silk Stacks (≤ 2 Void rooms per F2–3 Silk floor) and
  the Choir's Drowned Crypt (F3, ≤ 1), M2 gated with the ship gates
  (6.4; HZ-9 to HZ-11). Rejected: B, lethal fall damage; C, Voids wait
  for M3.
- **Q3, the moving dream at M3:** option A. Tides, Cord Flips, the
  Turning Ring, the Returns Trolley and the Cathedral's reversal round
  are all allowed at M3 under the rules (pre-proven states, existing
  kinds only, announced a round ahead, held while occupied where needed,
  the flee and escape proofs holding). This reopens decided main 2's
  non-goal, agreed conditions 11.1, agreed RT 6.3 and RS 7.6's "Later"
  (HZ-18, flagged). Rejected: B, Tides and Flips only; C, Later.
- **Q4, floor 1 at Nightmare tier 2+:** option A. One damaging hazard
  per floor-1 theme at tier 2+ (the Gaol Treadwheel, the Kennel
  Stampede and staked pits, the Wake's Open Grave, the Painted Hunt),
  with DH 17.1 and the floor-1 guard rows still holding; the base game's
  floor 1 stays hazard-free (RT-20 kept literally; Landmark ticks at M3
  are non-hazards) (6.19; HZ-25). Rejected: B, floor 1 hazard-free at
  every tier.

**The lead's five limits on "nothing is barred"** (0.1 item 10), which
the client accepted: no hero dies to a fall; nothing acts on unannounced
squares and nothing draws at act time; no spreading surfaces; floor 1
dry for the solo hero in the base game; escalation in kind, not damage.

**Told plainly; confirmed by the client on 2026-11-21:**
- **M2's dynamic vision is at most one breathing room per floor.** Stirs
  barely move kills or fight length; they buy a changing best square at
  a reading cost (0.1, 5.2).
- **The TN6 band (3–8% environment kills) is reached only at M3–M4:**
  about 1.5–2% at M2, 2.5–3% at M3, 3–3.5% at M4 (10).
- **The clock:** segment 4 takes up to +0.3 min per floor, *inside* the
  terrain package's +1.6 tolerance (TN13), not on top of it; the cut
  order is extended (10, HZ-27). After the check this is enforced by a
  combined gate.
- **The Turning Ring** keeps FL 7.7's letter (horses on their poles)
  but contests its spirit: the ring boards move units (7.7, risk 15).
- **Landmark ticks** are a non-hazard class legal on floor 1; they move
  doors and mute noise, which changes floor-1 play, so the floor-1 rows
  are re-run with them on (6.1, 7.2–7.4; HZ-19).
- **Floor 1's base content is thin:** in the base game the Gaol, Kennel
  and Wake show little of their environment until M3's ticks (risk 13).
- **The K-table stays closed:** environment acts never start Conduct
  (6.11).
- **The M2 Void pilot alone will not reach TN6's 3%** (6.4, 10).
- **Counting Sheep's surprise is declined for M2:** it would add a
  forced-movement exception to M2 (A.1 HF13).
- **Floor-1 Nightmare content meets the solo hero:** tier 2+ players
  have won a run, so no solo exemption applies there (6.19).
- **Staked pits** are re-read from RS 8.4's "Later (drop hazard)" as a
  walkable harm square at tier 2+ only; after the check they are
  condition-free, so the Kennel stays "deliberately dry"; the Kennel's
  Briar stays its own future question (6.19).
- **RS 8.10's acid cap is re-read** from units to sources (HZ-5).

**New lead's calls made while writing this document** (beyond the
answered proposal; confirmed by the client on 2026-11-21, with this
document):
1. **Both pilot Stirs are built to the playtest, and each ships on its
   own theme gates** (`FootprintValue`, its spike row,
   `StirBothWaysShare` on its theme) while the shared gates pass (6.3,
   8). Revised after the check: the draft shipped one kind only, which
   left a third of runs with no Stir.
2. **Renames for collisions** (0.1, A.5): the Stir part Topple →
   **Overturn**; the "waking" fall state → **Groggy**; the Throat →
   **the Gullet**; the Draught → **the Open Window**; the Night Bell →
   **the Curfew Rope**; Bells Ring Upward → **the Chimes Ring Upward**;
   the Reading Room tableau → **the Carrel Rows**; the Long Table
   tableau → **the Laid Table**.
3. **The Dream Rules codex is pages of RS's M3 codex,** not a new system
   (9).
4. **Local id schemes** HT1–HT19 (tensions, was E1–E19), VP1–VP10
   (placement rules), HF/HB/HC (critique findings) (front matter).
5. **Fall types:** a Void skin with no named type deals its theme's
   `primaryDamageType` (revised after the check from bludgeoning, which
   four themes lack), and a skin uses only a damage type already in its
   theme's `damageTypes[]`, never radiant before RT-15 is ruled (6.4,
   11.1).
6. **The Kennel's Nightmare package escalates in kind:** staked pits
   plus the non-damaging Painted Hunt at tier 2; the Stampede replaces
   the Painted Hunt at tier 3 (6.19, 7.3).
7. **Floor-1 Nightmare rules:** no Void, Stir or Lurch in main-path rooms
   0–1 or the first fight room; ≤ 2 hazard rooms per floor at
   main-path index ≥ 3 with the F2 budget (3 points); floor 1's own
   bands (revised after the check from the F2 bands) and EnvDC 13; the
   floor-1 guard rows and `Floor1ClassSpread` re-run per tier, a
   failing package moving up a tier (6.19, 6.3.5).
8. **The Carnival Mood's display line** "The Last Evening" yields to the
   theme's name in any naming clash; the Omen is "Encore" (6.9).
9. **Cut-order precedence:** segment 4's cuts fire first when its own
   +0.3 misses or the combined gate misses; RT's steps fire first when
   the terrain package misses with segment 4 off (10; revised after the
   check).
10. **The segment-4 baseline arm:** segment 3 on, segment 4 off;
    `HazardMinutesDelta` (renamed from `FloorMinutesDelta` (segment 4))
    is on minus this arm (11.5).
11. **`BossLaneClear`** replaces the proposal's typo (11.3).
12. **Chainposts cost 2 hazard points** and count as the room's clock in
    the points table and the one-clock rule (6.2.1, 6.2.2; the proposal
    stated both only in its mechanics section).
13. **The M2 (gated) fallback** also fires when a gated row's owner
    rows have not landed by M2 content lock; the client is told (12,
    19.2).
14. **Document shape:** the proposal's sections are renumbered to the
    RT template, adding Intent, Goals, Terms, Left to the builders and
    Depends on.

**Told plainly after the check** (the design check that followed the
client's answers; confirmed by the client on 2026-11-21, with this
document):
- **Q1 in M2 is a cap, not one Stir on every floor.** Both named
  candidates are F2–3 themes, so a Stir can appear only on a Choir or
  Carnival floor. With both allowed to ship on their own gates, every M2
  run has one or two Stir floors; the Silk floor and F4–5 have none
  (0.1, 6.3).
- **The M2 Stir will probably miss the clock gate on toy numbers:**
  static density (+0.23) plus a Stir floor (+0.26), read as additive,
  is about +0.49 against the +0.3 allowance. It ships only if the
  reading-time playtest measures it well under the toy; otherwise Q1's
  fallback sends it to M3 (10). The toy's absolute fight minutes (20.39
  per floor) are above companions 7.4's 20, so only its deltas are used.
- **The Organ strikes at the harm band, not the contact band:** every
  unit it hits is Soaked, and the weakness would break
  `ContactLethalityBound` (7.5, 6.11). The Drowned Crypt's cold fall
  band is lowered the same way.
- **The Stampede sits at tier 3,** inside the client's "tier 2+", with
  staked pits and the Painted Hunt at tier 2 (6.19).
- **The Gaol stove is promoted to a Hot piece typed bludgeoning
  ("scalding iron")**, because the Gaol has no fire type; it is RT's
  own anticipated M3 promotion, under its `HazardPushDeaths` gate
  (6.19).
- **Floor-1 Nightmare content uses floor 1's own damage bands,** not
  F2's (6.19).
- **The rescue push costs more:** the ally lands on the secondary Snag
  and loses its next action, and no voluntary Jump or Flight may end
  over a Void. The FallFloor still holds (Q2) (6.4).
- **The Horn Post is once per floor and Startles one group,** not every
  beast in the room every fight (7.3).
- **Four catalogue pairings are cut to keep one clock per room and no
  Void beside a Lurch Stir:** Madame Ashes takes the Pit or (F3 only)
  the Turning Ring; the Night Governess drops Toss and Turn; the
  Pylorus takes Brim with the Gullet instead of Peristalsis; Counting
  Sheep is never hazardous toward a Void (6.17, 7.10). The Turning Ring
  is F3 only (7.7).
- **The Supper's Last Orders is not a Mood** but a Courses menu rule
  under the same Omen (6.9, 7.8).

**Lead's calls made after the check** (each is marked "lead's call,
after the check" where it sits; confirmed by the client on 2026-11-21,
with this document):
1. The countdown C and the exact Now, Hold, Stoke, Flip and Kick rules
   (6.3, 6.3.4, 9.2).
2. The computable Warned definition, with voluntary entry counted as
   Warned and the floored mark as a bracketed "1" pip (6.3.2).
3. The Dream step's sub-steps, its round ownership, and conditions
   applied in it counting as outside the unit's turn (6.3).
4. The damage pipeline and `CapRound` (floor 1 by its own band; out of
   combat one tick is one round; Clutch outside the cap) (6.11).
5. One shared area-cap counter across contact, harm and Void, and the
   Lurch cap order (6.4, 6.3.3; HZ-9).
6. Dreamfall placement order, the Snag choice with hostile checks at
   both sockets and an overflow square, Snags per room, ≤ 1 Void region
   per room in M2, and `DownedAfterDreamfall` = 0 solo (6.4).
7. The Clutch hold's clearing rule (6.4).
8. Snatch timing and reactors; the second-fall rule keyed on Dreamfalls
   taken (6.4).
9. The rescue-push cost and no voluntary movement ending over a Void
   (6.4).
10. Fall types default to the theme's `primaryDamageType`; the Silk
    Stacks fall band is piercing (6.4, 7.6; revises new lead's call 5).
11. The period floor under escalation; Peristalsis exempt from the Gut
    Mood; V10's Mood-stage and tier-class dimensions (6.3.3, 6.9, 11.3).
12. Lurch on floor 1 at tier 2+ only for the 6.19 packages; no
    collision Lurch; Guarded dropped from the hold-fast list (6.3.3,
    6.19).
13. Tides held whole (6.3, Turn part).
14. The split record and the runtime records (11.1).
15. Report definitions, the `HazardMinutesDelta` rename, the static and
    Stir clock shares and the combined gate (10, 11.4, 11.5).
16. The V10 check definitions (`PushLaneExists`, `FootprintValue`'s
    choke square, `EnvRoundBound` as a report, `SnagProven`,
    `PeriodFloor`) (11.3).
17. Due squares outside C 10.1, 10.2 and 10.3; the landing save at
    EnvDC (6.3.1, 6.3.5).
18. Bile in tubes only, the dry-route check's origin and Tide states,
    and bile's point cost (6.5).
19. One struck-vent trigger per use and the refresh rule (6.7).
20. Cords at path distance 4–6 from entry; bound and standalone Cords;
    damaging standalone Cords resolve as Now; the Curfew Rope as two
    Cords; the Sluice Wheel and Acid Tide valve as Flip Cords (6.3.4,
    7).
21. Mood cap as floor(fight rooms cleared ÷ 2) and `MoodCounter`; Last
    Orders moved to the Courses (6.9).
22. The staked pit as a plain harm square (6.19, 7.3).
23. Groggy's clearing rule (6.4).
24. The Wake as a drop theme at tier 2+ only, and its Snag wording (6.4,
    6.19).
25. Plunge as a Chute skin with an LG14 exception (6.10, HZ-23).
26. Toss and Turn defined as a bedsheet Tide; Lockdown Hour removed;
    one vent name (7.10, 13).
27. Guttering Candles only at index ≥ 3, one rack per room (7.4).
28. `StirBothWaysShare` marked as the lead's addition to the Q1 gates
    (6.3).
29. One clock per arena row (6.17).
30. The Organ and Crypt bands lowered; `ContactLethalityBound` after
    weakness (6.11, 7.5).
31. The Dreamer's mattress footprint and the Alarm Clock's ring
    footprint; Wind Back as Hold with a slip of 3 (7.10).
32. The shover unit cap (6.11).
33. The Horn Post once per floor, one group (7.3).
34. The Chainpost target at act time with a live label (9.2).
35. Section 8's additions: latent footprints, the floor-motion
    whitelist and figments, the RC5+ table, the pattern registry, the
    Void backdrop tier and shaft, two-layer cues, the Dream tick, the
    timbre table and the audio blind-ID gate, countdowns on floor
    decals, Snag props off the Snag squares, the floored pip, the RC4
    rewording and the budget lines (8, 6.18).
36. New lead's call 1 revised: both pilot Stirs may ship, each on its own
    theme gates (6.3).

**Q1's reach in M2 (answered by the client, 2026-11-21):** the one item
that needed the client.
- **The question.** As written, Q1 gives at most one Stir floor per
  F2–5 floor and in practice one or two per run (Choir and Carnival
  floors only). A Stir on every F2–5 floor in M2 would need new M2
  (gated) Stirs for the Silk, Supper, Gut and Heart, which adds build,
  art and clock cost and runs into SC14 (Strike-only Stirs with no
  payoff are cut). The lead recommended keeping Q1 as a cap in M2 and
  filling F4–5 at M3.
- **The answer (2026-11-21):** the client accepted the recommendation.
  Q1 stays a cap in M2: Stirs appear only on Choir and Carnival floors,
  both pilots may ship on their own gates, so an M2 run has one or two
  Stir floors; F4–5 Stirs arrive at M3.

### 19.2 Future items triggered by reports or later designs

These come back to the client only if a report or a later design
triggers them:
- an M2 (gated) row (the Stir pilot, the Void pilots) that misses a gate
  and ships at M3 (the client is told);
- the RT-14 ruling refused (Void-edge successes become plain stops; the
  RS 8.0 fallback drops apply; the Supper vent and Toss and Turn are
  cut);
- the "bosses steady at turn start" lever, if
  `TeeteringUptimeOnBosses` fails;
- the Heart's Void rooms beyond 1 per F5, once `FallsByWrecker` and
  `ClassThemeSpread` pass;
- a theme's Cord cut under `CordUseRate` < 20%;
- the Dreamer Turns Over moved to Nightmare tiers if `QuakeProneRatio`
  fails;
- two clocks per room at a Nightmare tier (M4);
- Let Go; cross-room drops; Lull; light hazards with NM-9;
  wall-clock floor states as an opt-in tier 5 (Later);
- Briar with Bleeding in the Kennel's base game (RT 19.2);
- the naming pass.

---

## Appendix A: how the critique findings were resolved

### A.1 Fun critique

| # | Finding | Resolution |
|---|---|---|
| HF1 | Void + Brink + Wrecker loop, and the Heart as pilot | The Void-fall save ignores Teetering's disadvantage (Brink itself unchanged, avoiding a Vanguard row); ≤ 2 enemy starts within 2 of a Void; the pilot moved to the Silk and the Choir, Heart Voids M3 with ≤ 1 room until gates pass; the Shade hook and spread as a ship gate |
| HF2 | Stirs are a reading chore | SC13 footprint value, SC14 payoff verb; the signature Stir may use the contact band; one Stir per floor in M2 |
| HF3 | The Heart quake taxes the party | Handholds plus the "did not move" rule; ratio gate ≤ 1.2; M3 only |
| HF4 | Let Go as a free teleport | Later |
| HF5 | Edges have no drama | The Groggy state (no reactions, no Portal exit); the fall preview names separation; the `SnagPunishRate` floor of 10% |
| HF6 | Repeated falls and Snag camping | A second fall stays on the edge, floored; hostile checks at both Snag sockets, then an overflow square (6.4) |
| HF7 | The Hymn rewards stalling | In-room trigger Tide only; framed as a lightning reward; the round-4 backstop |
| HF8 | Rest Digestion kicks a losing party | Rejected; the long-fight trigger as an Omen only, capped |
| HF9 | Lurch undoes planning | Period ≥ 3; Hidden and Handhold-adjacent units hold fast (Guarded dropped after the check); ≤ 25% footprint |
| HF10 | Cords ignored | Approach-ring placement; bonus-action Operate when starting adjacent; `usesCords` monsters; cut if under 20% |
| HF11 | Readability overload | One clock per room, never waived; Dare rooms one showpiece; headline plus sub-line; the glance test |
| HF12 | The Warned rule is invisible | The floored pip and the lethal tag (the shield mark replaced after the check) |
| HF13 | Too little dream surprise | Surprise at reveal and floor entry: uncanny fiction on the pilot Stirs, Omens; Counting Sheep as the first M3 Lurch. **Declined** for M2: it would add a forced-movement exception to M2 (told plainly) |
| HF14 | Floor-1 rhythm unnoticed | Mandatory hinge pips and `lessonTag`; the Painted Hunt at Nightmare tier 2+ (Q4) |
| HF15 | Boss Teetering in Void arenas | Narrowed F-c adopted |

### A.2 Balance critique

| # | Finding | Resolution |
|---|---|---|
| HB1 | The stack bounded only by a report | The EnvRound cap as a resolution rule (HZ-7) |
| HB2 | Pit Breathes Kegs contradict F-g | No Keg in the ring on F2–3; a Keg at distance 2 as bait |
| HB3 | Clutch-once fails in round 1 | Cannot fall until it has taken a turn after the Clutch; `EliteRemovedBeforeFirstTurn` = 0 |
| HB4 | "Warned" too narrow | Requires a legal move off the footprint during the last turn, or voluntary entry (6.3.2, defined after the check) |
| HB5 | Snatch can down the saved unit | Snatch deals no damage |
| HB6 | The Snag as a free flee route | The Snag ≥ 3 from the `fleeExit`; Groggy blocks Portal exits; Let Go Later; `FleeAfterFallRate` |
| HB7 | The AI farms party falls | Once-per-hero scoring; ≤ 1 shover type and ≤ 2 shover units near Void edges; `PartyFallsPerFight` ≤ 0.25; the Snag overflow rule |
| HB8 | The quake worsens with party size | The "did not move" rule; the Handhold density check; M3 only |
| HB9 | Class spread, no gate | Gate ≤ BASE + 2; the Shade hook with the pilot; the Arcanist Gust request; density before Clutch as the lever |
| HB10 | F-c overreaches on Brink | Narrowed: only Void-edge success and Clutch; Brink unchanged; the reserve lever |
| HB11 | The mindless share is not a hazard lever | Not raised by segment 4; the enemies owner asked; both-ways from held monsters |
| HB12 | Hymn stalling | Backstop round 4; Routed counts as stopped; `HymnRoomRoundsDelta` |
| HB13 | Meters break DH calibration | Omen and Nightmare only; +1 per two rooms; the F2 death gate |
| HB14 | Gut corrosion stack | One Fortitude rule for bile and the Gullet; the sources reading |
| HB15 | Drowned Crypt lightning spike | The Snag never water-connected to an Organ; no Stir in a Crypt room; counted in the spike row |
| HB16 | K3 inconsistency | Written once: environment acts never start K3 |
| HB17 | Dare brute force | Return to the threshold at 1 HP; no rest inside; two attempts then seal |
| HB18 | Plunge as a free opener | Target cleared or non-combat, ≥ `HearingGap` |
| HB19 | Struck vent stacking | The first Remnant application inside the push's use for the EnvRound cap; `NoCheapKill` treats vent mouths as contact |
| HB20 | The Gaol mute lowers floor-1 difficulty | Floor-1 rows re-run; the mute limited to party verbs; the Curfew Rope once per floor |
| HB21 | A benign Lurch on floor 1 | At Nightmare tier 2+ only (Q4) |
| HB22 | The Forbidden Course | A party-only interact, M3, the DH owner |
| HB23 | The Open Window → Frozen pipeline | Environment Chilled counted in the freeze-spike row |
| HB24 | Ledgewalker weaker | A trigger-rate report on Void floors |
| HB25 | Credit mismatch | Experience and morale to the operator; on-kill never from acts |
| HB26 | Clock | The pilot not committed until the reading-time and F-b measurements; the cut order extended |

### A.3 Consistency critique

| # | Finding | Resolution |
|---|---|---|
| HC1 | "Nothing timed" is agreed | Q1 cited RT 0.1 item 5, RT 0.2 row 13 and RT 6.6 (agreed, RT Q5) and TR-27; the written-pass gate **(agreed, Q1)** |
| HC2 | Flip bundled under timing | M2 Cords re-time only; Flip, Kick and the trolley under Q3 |
| HC3 | Benign Stirs contradict the taxonomy | Landmark ticks, a non-hazard class without unit movement; unit-moving floor-1 content needed Q4 **(agreed)** |
| HC4 | RS 7.6 missing | Cited in HZ-18 and Q3 ("Later → M3") |
| HC5 | Name collisions | A.5 |
| HC6 | F-c edits Brink | Narrowed; RT's reserve lever; the guard wording permits Brink |
| HC7 | DH 17.3 not assumed | HZ-13 adds the row |
| HC8 | Model B's phase 1 | `firstAct` ≥ 2; `StirRound1Silent` |
| HC9 | Acid cap and save inconsistency | The HZ-5 change row; one Fortitude rule |
| HC10 | Boss counterplay and segment-5 collisions | Arena items stay segment 9's; the hymn bellows and clapper chain renamed; horn post against tripwire; FL told about the sluice wheel |
| HC11 | Arena caps | HZ-11: the Void line separate from contact hazards |
| HC12 | No clock allowance exists | A lead's call inside the tolerance (told plainly); the risk-7 row |
| HC13 | A hush mutes combat noise | The Mute round mutes party-verb noise only |
| HC14 | Void quota against the clock cap; the Heart floor; the TN6 projection | Voids are static, not clocks; the Heart is F5 and M3; TN6 re-projected by availability (told plainly) |
| HC15 | "M2-late" undefined | "M2 (gated: falls back to M3)" throughout |
| HC16 | The Hinge radius given three ways | The square and its 8 neighbours; no Void within 2 |
| HC17 | The Warned rule must be visible | Marks in the DH 17.5 row |
| HC18 | EnvDC cites a missing ladder | Cites RT 6.2.6; a hook to segment 5 |
| HC19 | The Organ reproduces Conduct | Footprint fixed at generation; the spike row; never starts K3 |

### A.4 Judges' grafts and toy fixes

All judges' grafts are adopted as written above, except:
- the `floorborn` horizon-2 read (rejected);
- half-HP Clutch (rejected);
- the Heart as the M2 pilot (moved to M3);
- Let Go at M3 (moved to Later).

The toy fixes F-a to F-k are adopted, with F-c narrowed (only Void-edge
success and Clutch) and F-d's mindless-share half referred to the
enemies owner. Named in the text: F-b (perimeter and flank Voids), F-c
(the boss Teetering guard and VP7), F-g (no Keg in an F2–3 footprint).

### A.5 Names

| Rejected name | Collides with | Used instead |
|---|---|---|
| Rouse | Herald ability | **Startle** |
| Primed | Arcanist vocabulary | **due squares** |
| Shift, Sway | W-8's forced-movement kind (Shift) | **Lurch** |
| Hush | Hushed (condition); RS 8.0's avoided Hush | **Mute round** |
| the Deep | RS act or band "Deep" | **the Drowned Crypt** |
| Fallen | DH "Reach the Fallen"; Herald | the experience trigger `voidFall` |
| Work | Shade Quiet Work | **Operate** |
| Restless | Later condition | **Fitful Sleep** |
| Champion (as a rank) | ENM encounter template | "elites and group leaders" |
| anchor | ENM anchor ranking | **Handhold** |
| landing (for the Snag) | LG20 landing, C 10.3 | "beside the LG20 landing" |
| Topple (Stir part) | Weapon verb Topple (ABL, C 18.4) | **Overturn** part (new lead's call) |
| waking (fall state) | The Heart's Law "waking"; RS 8.11 | **Groggy** (new lead's call) |
| the Throat | Shade Throat-Cut | **the Gullet** (new lead's call) |
| the Draught | Consumables draughts (Resistance Draught and others) | **the Open Window** (new lead's call) |
| the Night Bell | RS 8.0's avoided "Bell" head words (Herald Passing Bell) | **the Curfew Rope** (new lead's call) |
| Bells Ring Upward | The same | **the Chimes Ring Upward** (new lead's call) |
| the Reading Room (tableau) | FL 7.6's reading rooms; classes 7.6 | **the Carrel Rows** (new lead's call) |
| the Long Table (tableau) | FL 7.8's Supper Blueprint "The Long Table" | **the Laid Table** (new lead's call) |
| Dream Rules codex | RS 12's M3 codex | Pages of that codex (new lead's call) |
| Perilous, Gauntlet, Lever, Pulse, Quake, Toll, Knell, Lair, Lucid, Swallow, Trial, Ordeal room | Various (Perilous Door, Herald Toll and Knell, ABL "Lair actions", the Lucid Key) | Not used as names ("Ordeal" replaced by **Dare room**; "Swallow" kept only as a rejected mechanic's label) |

### A.6 The design check

A design check (builder, consistency, play and art lenses) followed the
client's Q1–Q4 answers. Its findings were applied in place and marked
"after the check"; none overrides a client answer. The resulting
told-plainly items, the lead's calls and the one item that needed the
client (Q1's reach in M2, answered on 2026-11-21) are listed in 19.1;
the client confirmed them all when it approved this document on
2026-11-21.

---

## Appendix B: sources

- **Toy models** (crude, not the simulator): the scratch scripts of the
  hazards research pass, 600 runs per arm; they reproduce TN6's base
  environment kill share (0.73% against "≤ 0.9%"). Every number from
  them is *(illustr.)*.
- **Games:** the section 4 table; Into the Breach, Brogue chasms,
  Frosthaven and Pathfinder 2E complex hazards were web-confirmed; the
  rest are from knowledge, with D&D 4E terrain powers *(recalled)*.
- **Approved documents:** the 20 parents listed above, grepped for
  hazard, Void, chasm, drop, fall, Teetering, bile, vent, Remnant,
  timed, environment, lair, Brink and Snag, each hit read in context.
