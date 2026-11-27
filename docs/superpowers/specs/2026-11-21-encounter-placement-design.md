# Encounter Placement: Design (Dungeon Generation, Segment 6)

Date: 2026-11-21
Status: Approved, 2026-11-22
Names updated by the naming pass (naming pass design, approved 2026-11-24;
NP-35 with its 13.1 cites): Mousetrap → Baited Room; Sleeping Ward → the
Heap; Wake Order → Alarm Order; Wary → Watchful; "Caught facing the door"
→ "Facing the door"; "Distracted −2" → "Busy −2"; roles Marksman,
Hinderer and Swarmer (Minion stays the rank); Bodyguard → Challenger;
The Silk Archive → The Cobweb Archive (short form the Archive); roster
renames Pale Mare, Gall Slick, Shadow-Puppet, Weightlifter, Chief
Archivist, Still Footman; cites Exhorting, Found, Charged Water,
Receive, Bar the Way, the first-spring rule, Tracery Loft and
Fracturing. Internal ids, the `overwatch` socket class and history
(section 17 change rows, Appendix A) keep their wording; four section
17 rows carry "(renamed …, naming pass NP-35)".
Parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "C"),
`2026-11-16-enemies-design.md` (enemies design, "ENM", the key parent),
`2026-11-16-items-inventory-design.md` (items design, "ITEMS"),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design, "TK"),
`2026-11-17-consumables-design.md` (consumables design, "CN"),
`2026-11-17-death-healing-design.md` (death and healing design, "DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "CL"),
the stage-2 class designs `2026-11-18-vanguard-class-design.md` ("VG"),
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC")
and `2026-11-20-herald-class-design.md` ("HR"),
`2026-11-20-companions-design.md` (companions design, "CMP"),
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1),
`2026-11-21-floor-layout-design.md` ("FL", segment 2),
`2026-11-21-room-terrain-design.md` ("RT", segment 3),
`2026-11-21-hazards-design.md` ("HZ", segment 4) and
`2026-11-21-traps-locks-design.md` ("TL", segment 5, the most recent
template)

Items marked **(agreed)** are the client's answers of 2026-11-21 to the
four questions this design raised (Q1–Q4, sections 0.2 and 19.1), each
answered with the lead's recommendation (option A), or restate a client
answer recorded in an approved document. The client also said that
everything else in the lead's proposal stands as written: Model B as the
core with Model A as the M1 cut and only Model C's grafts (the Rumour
and the Prowl rejected); the pipeline (the `encounters` child stream,
after the `traps` pass and before `trapsResolve`); the budget and the
difficulty curve; party size (core and growth positions, the solo
rules); composition from legal ENM 17.4 templates; start states (guard
posts never start Watchful); socket classes; per-role placement; Billets and
the AI hand-off; the Keystone and breaks with the Stung rule; the
Clearway; the 17-formation catalogue (the catalogue holds 19 entries,
now 15 battle formations and 4 start shapes: told plainly after the check,
19.1); the Ritual (M3); Terrain
Pairings; activities; the ambush definitions, lurker geometry,
detection, the keyed placement-Stealth hash, the Waylay gates,
counter-ambush and the symmetric surprise table; patrols and the
Circuit; guard posts and lairs; the avoidable fight (Gb); joiners
(segment-6 levers only); the hand-offs to traps and locks; bosses and
arena hooks; class interplay; the floor-1 rules (EP-11, EP-13); the
clock; seeds; UI and previews, including the Encounter Read card and the
ENM 13.5 change; rest, backtracking and escalation; the full per-theme
catalogue for all themes; readability, art, animation and audio; the
adopted, deferred and rejected mechanics; the numbers and fixes (toy);
data, V12 `EncounterPlacement`, reports and arms; the staging; ownership
and hooks; the binding requirements; the EP-1 to EP-41 changes; the
told-plainly items; the risks; and the appendix resolutions and names.
The client approved this document as a whole on 2026-11-22.

**The two brief questions** (CQ1–CQ2 here, to keep them apart from this
document's client questions Q1–Q4) were answered by the lead in the
proposal (0.1). The client accepted those answers by proceeding to
Q1–Q4; they are recorded as the **lead's answers**, and the client
confirmed them all on 2026-11-22, when it approved this document as a
whole. **Two parts of them were contested by the lead:** "no hidden
whole groups" (CQ1) and "no glued archers" (CQ2), each against a reading
the brief invited; the client confirmed both with this document on
2026-11-22 (19.1).

Items marked **(lead's call)** were filled in by the design lead inside
approved rules and were in the proposal the client answered; the client
confirmed them all on 2026-11-22, when it approved this document as a
whole, and they are binding. Items marked **new lead's call** were made
while writing this document, beyond that proposal; they are listed in
19.1 and the client confirmed them all on 2026-11-22. Items marked
**(lead's call, after the check)** were made when revising this
document after the design check that followed the client's Q1–Q4
answers; they are listed in 19.1, the client confirmed them all on
2026-11-22, and they never override a client answer. Items
marked **(settled)** restate a rule from an approved document;
**(decided)** marks a main-design client decision. Items marked
**(change EP-n)** amend an approved document and are listed, with the
exact section they touch, in section 17; rows that touch **decided** or
**agreed** text are flagged there, and the client's approval of this
document on 2026-11-22 (with Q1–Q4 where a row names one) covers them.
Items marked **(told plainly)** narrow or re-read agreed or settled
text, or state an unwelcome forecast, and were told to the client in
plain words (19.1, including those told after the check); the client
confirmed them all on 2026-11-22. Every item here is binding on the
builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q4 of this document (2026-11-21), or one recorded in an approved document, restated |
| **lead's answer (CQn)** | The lead's answer to brief question n, accepted by the client by proceeding; confirmed by the client with this document on 2026-11-22. Its two contested parts were confirmed on 2026-11-22 |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-22 |
| **new lead's call** | A lead's call made while writing this document, beyond the answered proposal; listed in 19.1 and confirmed by the client on 2026-11-22 |
| **(lead's call, after the check)** | A lead's call made when revising this document after the design check (builder, consistency, play and art lenses); listed in 19.1 and confirmed by the client on 2026-11-22; never overrides a client answer |
| **(change EP-n)** | Amends an approved document; listed with its exact section in 17 |
| **(told plainly)** | Narrows or re-reads agreed or settled text, or states an unwelcome forecast; told to the client in plain words (19.1) and confirmed by the client on 2026-11-22 |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (10, 11). Never binding |

**The client's brief:** dungeon generation segment 6 of 12, encounter
placement: which monster groups go in which rooms; composition by role,
rank, template, leader and elite; start states and their spread; start
positions and formations; guard posts, lairs, patrols, the avoidable
fight, joiners; the hand-off from segment 5; the difficulty curve;
party-size scaling; floor 1; hooks to segments 7–12. Two questions:
**CQ1, should hidden enemy groups try to ambush the players?** and
**CQ2, how tactical should placements be (archers in the backline,
compositions)?** Standing guidance: not an exact 5E port; not limited by
quantity; the art team is eager; combat is key; contest the client or
approved views where warranted. The approved documents owe this segment
ENM 10.5 steps 2 and 4–11, the solo composition table, the ambush and
lurker-placement rows of ENM 25, the doorway-hold AI request (FL-21 (c))
and the segment-6 hooks of RS, RT, HZ and TL (3.2).

The client's four answers (Q1–Q4, **agreed**, 2026-11-21) are in 0.2:
**the party's surprise is judged per side (Q1); one zero-XP Annex group
per floor on F2–5 (Q2); a seen lurker tell counts in the danger band
(Q3); and awareness is chosen per group inside a per-type set (Q4).**

**Change prefix.** This design uses **EP-n** ("encounter placement"). A
grep for `EP-[0-9]` as a whole id across the 22 approved documents finds
0 hits (the only near match, `DEP-1`, is a different prefix used in CL,
CN, SH, ARC, CMP and RS). This document issues one register, **EP-1 to
EP-41** (17); no id has two meanings. The research drafts' overlapping
local numbers (EP-1..18, EP-1..28, EP-30..33 and others) are all
renumbered into it; the cross-reference is in A.6. Local id schemes,
each grep-checked as a whole word with 0 hits:
- brief questions **CQ1–CQ2**;
- tensions **ET1–ET20**;
- Appendix A findings **XF** (fun), **XB** (balance) and **XK**
  (consistency);
- formation tiers **FT1–FT3** **(new lead's call:** the proposal said
  "Tier 1–3", which collides with the ability model's spell tiers, "tier
  1" to "tier 4", in ABL, C and the class designs**)**;
- this document's client questions **Q1–Q4** (local, as in every parent).

The validator id **V12** is free (V1–V7 RS, V8 FL, V9 ROOM, V10 HZ, V11
TL); a grep for `V1[2-9]` finds 0 hits. ("ROOM" is the room terrain
design's tag for its validator owner, so as not to collide with RS's
change ids RT-n; seeds and validation design 13, change SV-31.)

**Names are placeholders** until the client naming pass. Each was
grep-checked case-insensitively as a whole word across the 22 approved
documents; the list and the renames forced by collisions are in A.5. The
case-insensitive pass caught "set piece" (a TL term), "Feast" (Supper
backdrop text), "Huddle" and "Absorbed" (plain words in HR, DH and SH),
all of which the drafts had used. **New lead's calls:** the proposal's
"disposition" (Posted, Busy, Dormant, Walking) collides with ENM 14.6,
where Yielded and Fled are "dispositions"; it is renamed **posture**
(0 hits), and the posture "Walking" (a plain word, 13 hits) is renamed
**Afoot** (0 hits). The art "stance grammar" of the proposal is renamed
the **pose grammar** (8), since "stance" is a Vanguard rules word.

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every
rate and share comes from crude toy models (10, Appendix B); the M2
simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile,
unlocks, ladder), **M4** breadth, **Later** **(settled, main design
9)**.

Abbreviations: RC = the readability charter RC1–RC9 (RS 7.4); RE = RS's
tension ids; LG = FL's graph invariants; KP and CV = RT's placement and
cover rules; VP = HZ's placement rules; TP = TL's placement rules; Gb =
the route-avoidable guard post (FL 6.8); F1–F5 = floors 1–5; "party 2"
= a party of two units.

---

## 0. At a glance

### 0.1 The lead's answers to the brief

**The design in one line.** *Model B's named formations and activities
on Model A's deterministic pipeline, with soft Billets, public breaks,
validator-proven counters, three ambush forms under one cap, and no
hidden groups, alarms or spawns.*

#### CQ1. Should hidden enemy groups try to ambush the players?

**Lead's answer: yes to ambushes; no to hidden whole groups. Most
ambushes hurt through position, not through a lost turn; the party
ambushes far more often than it is ambushed.**

1. **Three ambush forms, one approved cap** (change EP-10). The ENM 10.3
   column "Ambushes per floor" (F1 ≤ 1 and not in rooms 1–2; F2 ≤ 1;
   F3–5 ≤ 2, **settled**) has never said what counts. This design
   defines it:
   - **Cuckoo:** the approved ENM 17.4 Ambush with a *standard* as bait,
     plus 1–2 Hidden lurkers as a separate group. The party sees the
     bait, so under the per-side reading (EP-2, Q1, **agreed**) **the
     party keeps its turn**. The lurker's edge is its Hidden opener
     (150%) and, off floor 1, its Ambush Strike (`uses:1`, +2 dice).
   - **Baited Room:** the same template with a *container* as bait.
     **The only placement that can take the party's first turn.**
     Floors 2–5, party of 2 or more, at most 1 per floor and 2 per run
     *(illustr.)*. Never on floor 1, never against a solo hero.
   - **Waylay:** an ordinary, minded, Stationary group in a **Kill Box**
     out of sight of the landing, F2–5, party of 2 or more. It never
     hides, so under the rule that the party notices non-hiding
     monsters automatically on line of sight (MAIN 4.3, **decided**;
     ENM 12.2, **settled**) nobody is Surprised. The ambush is the
     geometry: a crossfire already set when you step through. It is
     announced by a **Spoor mark** on the door frame and by Listen, it
     guarantees a second way in **that costs something**, and the party
     can Surprise *it*.
   - The M3 boxed lurker (TL 6.14, **agreed**) also counts toward the
     cap; per its approved touch table it **never Surprises the party**
     (told plainly; the drafts had this wrong). It stands on its TL 6.14
     container socket, not on a guard-post host.
2. **Hidden whole groups are rejected (contested by the lead).**
   Starting archers, soldiers or skirmishers Hidden would delete the
   decided auto-notice of non-hiders, which is the one reason surprise
   deaths are bounded at all under permadeath. It would make Perception
   a must-take, break DH 10.4's "hidden information never kills", put
   deaths in the "Safe" danger band against DH 21.5's "deaths from Safe
   ≈ 0", need non-lurkers to roll Stealth against ENM 12.2, and
   multiply the ENM 6.4 surprise rows by group size. The pattern is the
   most-criticised one in the genre (Pathfinder: Kingmaker camp
   ambushes, Fire Emblem ambush spawns, Darkest Dungeon night ambushes).
   In the toy, hidden groups cost about 4 points of win rate and added
   only hidden-information deaths.
3. **Who ambushes.** Lurker forms only in the five themes with a lurker
   type (Gaol Silent Penitent, Kennel Ghillie, Wake Late Uncle, Archive
   Margin-Crawler, Gut Gutworm); the Choir, Carnival, Supper and Heart
   get Waylays only. No roster exceptions (RE9; TL 6.14, **agreed**);
   the themes differ in kind (told plainly: F5 has Waylays and no lurker
   ambush).
4. **How often** *(illustr.)*: about 0.3 / 0.4 / 0.5 / 0.6 / 0.6
   generated ambush placements per floor (F1→F5), about 2–3 per run out
   of about 30 fights, plus the M3 boxed lurker. Never at the avoidable
   fight (Gb), the Forecourt, the elite room, a Q room, the Sanctum, or
   a floor's first fight host. Those host rules leave **one** legal
   guard-post host on most floors and **none** on trimmed F3 and F4, so
   the generated forms reach the ENM 10.3 cap of 2 only on untrimmed F5
   with the Bypass round the first guard post; elsewhere the second
   place under the cap is the boxed lurker's (told plainly, after the
   check; 6.8.1).
5. **Floor 1.** Never in rooms 0–2 (DH 17.1, **agreed**; invariant 6,
   **settled**). At most one Cuckoo, only in the role order where the
   last guard post comes *after* the lair, and **a floor-1 lurker never
   uses Ambush Strike** (change EP-11, closing a loophole the per-side
   reading opens).
6. **No rest, camp or backtrack ambushes are generated.** Monsters never
   respawn (ENM 16, **settled**). Only units already on the map and
   already forecast can interrupt a rest. The backtrack threat is the
   **Hedgehog**: a group that lost the party waits at its own Billets,
   re-formed, on the Watchful state it earned.
7. **The party is the better ambusher.** Hold the Hide on the patrol;
   Busy and Dormant groups caught out of shape; the trap first-spring rule
   (TL-16, pending); marked lurkers struck first; Lure (SH-21, pending).
   The binding bound is **`PartySurpriseShare`** fed into the Shade's
   `stealthShare[L]` table, not a ratio: the toy shows party-to-monster
   surprise at 12:1 or more, so a "3:1" target would never bind.

#### CQ2. How tactical should placements be?

**Lead's answer: very tactical at the opening, readable on first sight,
built to break, and never scripted.** Archers start at the back and
*prefer* to stay there while the line holds; they are not glued there.

1. **Placement is where multi-step intent lives.** The scorer is
   one-step (ENM 11.1, **settled**; ABL 5 "no committed intents",
   **settled**). It cannot plan "hold the choke while the bows shoot";
   the generator, which sees the whole room, can. Each group is a
   **named formation**; each unit gets a **Billet** (hold squares plus
   one fall-back square), read by a small data-weighted scorer term that
   never beats invariant 7 (EP-6).
2. **Roles stand where their RC2 silhouette says:** soldiers and brutes
   at the choke facing the entry, marksmen at the back (on Low-cover
   overwatch from F2; uncovered on F1, `Floor1NoCoveredStarts`),
   skirmishers on the flanks, hinderers behind a soldier, the leader
   one rank behind inside its aura, lurkers in nooks.
3. **Glued archers are bad design, not hard design (contested by the
   lead).** In the toy a rigid leashed backline *halved* fight cost
   (−47% HP-equivalents), raised win rate to 47.5% and moved 82% of
   deaths to bosses: leashed units never engage and get picked apart.
   Soft Billets add about +14% per fight with no clock cost, and
   scouting wins it back.
4. **Every formation carries a guaranteed weakness** that V12 proves
   exists in the room (EP-8): a lane round the line to every back unit,
   a reachable leader, a second door for every Kill Box, a Shove-out
   square for every Phalanx, a line-of-sight breaker near the landing
   wherever shooters overwatch it.
5. **Every formation breaks on public triggers** (EP-7): its Keystone
   falls or is shoved off its hold squares, its leader falls, a
   *noticed* party unit reaches its backline, it is Demoralised, a third
   of its members are down, or its third round in sight of the party
   ends. A "Line broken" chip shows; the plain profiles take over.
6. **Variety without per-instance randomness:** template × formation ×
   activity × theme style × room. Same formation, same behaviour (ENM
   17.3, **settled**), so formations are learnable puzzles; the room
   changes the answer.

### 0.2 The client's four decisions (agreed, 2026-11-21)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Party surprise judged per side (MAIN 4.3, decided; ENM 12.2) | **A.** The party is Surprised only if no party unit noticed any hostile in the fight. Visible bait never costs the party its turn; only the capped Baited Room can. Monsters keep the per-group rule (6.8.2) | B: the per-group reading (every Cuckoo can take the party's turn, so Cuckoos return to 85% fill) | EP-2 ⚑ |
| Q2 | The Annex group (ENM 10.4, 16; TL 6.15) | **A.** At most 1 per floor on F2–5, Dormant, patrol band, 0 XP, inside every budget and guard, serving the Tattle beyond-room, the Wager guard, the Dare garrison or the boxed lurker (6.13) | B: no Annex (every Tattle a Crier; Wager and Dare rooms unguarded; the boxed lurker needs its own owner ruling) | EP-26 ⚑ |
| Q3 | Seen lurker tells in the danger band (DH 17.3) | **A.** A seen tell counts as a known threat (the lurker type's card); an unseen one adds nothing (6.20) | B: the band ignores hidden lurkers, and ambush deaths may land in "Safe" | EP-14 ⚑ |
| Q4 | Per-group awareness within a per-type set (MAIN 4.3 "set per monster in data", decided) | **A.** Each type lists `allowedAwareness[]`; the template and the group's activity choose within it (6.7) | B: one state per type (activities shrink to one per type) | EP-9 ⚑ |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Model B (named formations and activities) as the core; Model A's zone solver as the M1 cut; Model C's grafts only, its Rumour and Prowl rejected (FL 9.1 rejected a floor-wide alarm) | M1/M2 | lead's call |
| 2 | Segment 6 runs ENM 10.5 steps 2 and 4–11 on an `encounters` child stream, after the `traps` pass and before `trapsResolve` | M1 | lead's call; EP-1 ⚑ (MAIN 6.3, decided, extended) |
| 3 | Budgets as approved; a fill curve inside 85–100%; formation tiers FT1–FT3 by floor; hard-feature spacing; enforced no-repeat | M2 | lead's call; EP-20 |
| 4 | Party size by core and growth positions; solo on F2–5: front-standard formations only, Billet weight 0, no ambush form (Cuckoo, Baited Room, Waylay), Kill Box, Crossfire, Pincer or Bait and Nook; on F1 Billets at half weight | M1/M2 | lead's call; the Cuckoo bar and the F1 half weight are lead's calls after the check; EP-19 |
| 5 | Start states only Dormant, Stationary or Patrolling; **guard posts never start Watchful** (contested against Model A's brief); state inside the group's legal state set (6.4) | M1 | lead's call; EP-9 |
| 6 | Activities map 1:1 onto the approved states and four postures; they change the start shape and presentation, never noticing; awareness per group inside a per-type set | M2 | lead's call; **agreed, Q4**; EP-9 ⚑ |
| 7 | A 19-entry catalogue: 15 battle formations in three tiers and 4 start shapes (the proposal counted 17; told plainly after the check); Billets; Keystone; public breaks; the Stung rule against plinking | M1 static; M2 full | lead's call; EP-4, EP-6, EP-7 |
| 8 | Ambushes: Cuckoo, Baited Room, Waylay and the boxed lurker on the one ENM 10.3 cap; per-side party surprise; floor-1 lurkers never use Ambush Strike | M2 | lead's answer, CQ1; **agreed, Q1**; EP-2 ⚑, EP-10 to EP-12 |
| 9 | Fairness bundle proven in V12: `FirstSight`, `LandingShooters` 1/2/2/2/2 on the entry lane, `StartArc`, `BacklineReach`, `LeaderReach`, `CounterPresent`, `CounterCost`, `LurkBlind`, `TellSight`; the ENM 6.4 rows on placed geometry | M1/M2 | lead's call; EP-8, EP-30 |
| 10 | Joiners: segment-6 levers only; one placed call source per encounter; no "join chance" | M2 | lead's call, told plainly; EP-22 |
| 11 | The Annex group for off-slot needs | M3 | lead's call; **agreed, Q2**; EP-26 ⚑ |
| 12 | No generated rest or backtrack ambushes; the Hedgehog re-form; the rest preview adds Alerted, Searching and Pursuing groups | M2 | lead's call; EP-15, EP-16 |
| 13 | Readability: formation and Keystone chips on the board; the Encounter Read card; Perception sees hold squares; Lore sees the counter; a seen tell counts in the danger band | M2 | lead's call; **agreed, Q3**; EP-14 ⚑, EP-28 |
| 14 | Clock: ≤ +0.2 rounds per fight and no net minutes at party 4 on F4–5 *(illustr.)*, with a cut order applied before release if it misses | M2 | lead's call, told plainly; EP-33 |
| 15 | V12 `EncounterPlacement`, the fallback chain, reports and simulator arms | M1 skeleton, M2 | lead's call; EP-32 |

---

## 1. Intent

The enemies design fixed what monsters are, what they cost, how they
notice and how they think one step at a time **(settled, ENM 10–17)**.
The floor layout design built the hosts (chokepoint guard posts, the
cut-vertex lair, the Grille elite room, the Circuit and the Bypass); the
room terrain design authored start sockets with cover data; the hazards
design kept footprints off starts; the traps design placed its devices
first and waits, in `trapsResolve`, to bind keys and Tattles to the
groups this segment places. None of them said where each monster
stands, what it is doing when the party arrives, which way it faces,
what it does when the line holds, or what an ambush is.

The answer in one sentence: **every group is a named formation with a
readable job, standing where its role says, doing something a scout can
read, holding its ground only while it can trade, and breaking on public
triggers; ambushes are positional far more often than they take a turn,
and the party is the better ambusher.**

## 2. Goals, non-goals, anti-goals

### Goals

- Openings that are tactical, readable at isometric zoom and different
  from room to room.
- Placement that carries the intent the one-step scorer cannot plan.
- Ambushes the party can read, answer and turn round, under the one
  approved cap, with party turn loss rare and bounded.
- Every formation with a counter that physically exists, proven at build
  time.
- A shape that holds at every party size, from a solo hero to four.
- Deterministic placement: same seed, same inputs, same room.
- Room for the art team: postures, activities, Spoor marks, pose
  grammar.

### Non-goals

- Budgets, bands, caps and XP (the enemies design owns them).
- Scorer weights (the enemies owner rules the Billet term, EP-6).
- Layout geometry, sockets, hazards and traps (segments 2–5).
- The display (segment 10), seeds (segment 11) and validation tooling
  (segment 12), beyond the hooks in 13.
- Boss arenas (segment 9).

### Anti-goals

- Hidden whole groups, ambush spawns, off-map waves, wandering monsters
  and respawns.
- A floor-wide alarm meter (FL 9.1 rejected it).
- Rest, camp or backtrack ambushes.
- Leashed backlines and scripted behaviour.
- A "join chance" or any random joining.
- Turn loss from information the party never had a chance to read.

---

## 3. Constraints

### 3.1 What the parents already fix

- **States** (MAIN 4.3, **decided**; ENM 12.1, **settled**): ordinary
  groups start Dormant, Stationary or Patrolling, "set per monster in
  data". Watchful is reached only by Engaged groups that lost the party
  (the boss and its escort start Watchful). Alerted is a transient walk to a
  noise. Surprised is a condition (C 5.5), not an awareness state.
- **Noticing** (ENM 12.2, **settled**): one roll per trigger by the best
  member with line of sight against the party's passive stealth (worst
  member); the party notices non-hiders automatically on line of sight;
  only lurkers roll Stealth ("once at placement", again at the 6- and
  3-square bands); a band trigger with no member in line of sight is
  ignored; joiners never Surprise and are never Surprised. Party passive
  Perception is the maximum (S-12).
- **Budgets** (ENM 10.1, **settled**): weight 15 + 8 per companion
  (actual party, minus the CMP-5 shortfall on F2–5); bands patrol 70,
  guard 100, lair 140, elite 160, boss 200; depth 80/90/100/105/110;
  ladder; fill 85–100%; "an ambush uses 85% of its band". XP fixed per
  band (ENM 16); monsters never respawn.
- **Caps** (ENM 10.3, **settled**): units 4/5/6/6/6; elites;
  hard control (F1 0); hinderers; ranged share ≤ ½ on F1–2; ambushes
  per floor; joiners 1/1/2/2/2; ≤ 3 types; the role rule; no Quarry
  declarer with hard control or an ambush on F1–3.
- **Floor plan** (ENM 10.4, **settled**): 4–6 fights plus the boss (1
  patrol, 3 guard posts or 2 trimmed, 1 lair, 1 optional elite room,
  the boss); exactly one route-avoidable guard post (Gb); a patrol
  through a cover room; awareness mix ≥ 1 patrolling, 1 stationary, 1
  dormant; the M2 cowable, leader-from-F2 and steal-target guarantees;
  the cluster budget; the variety rules.
- **Generator** (ENM 10.5, **settled**, as amended by FL-3, RT-3, TR-17,
  TL-3): steps 2–11; step 9 moves only within a host or to
  `equivalentHosts`; "every step except 3 draws only from the dungeon
  stream … in the order listed" (lead's call).
- **Templates** (ENM 17.4, **settled**): slots and Awareness per
  template, read in full in 6.4.
- **AI** (ENM 11.1, 11.3, 17.3, **settled**): one-step scorer; the M2 row
  "Cohesion, body-block, area spread, hold" exists without a spec; Leash
  for the Sentinel and bosses only; invariants 1–10; the Sentinel's
  "Provoked" (lead's call).
- **Guards** (ENM 6.4, **settled**): round-1 death < 0.5% "with surprise
  wherever the template's awareness allows"; lowest legal hero < 2% on
  F2–5; F1 solo summed round-1 ≤ 50% HP; F1 cumulative ≤ 2.5%; every
  reachable weight (CMP-8).
- **Layout** (FL, approved): slot hosts; chokepoint guard posts; the
  cut-vertex lair; the Grille elite room; the Circuit and `coverRoom`;
  the Bypass and `SkippablePool`; `HearingGap` > 8; near pairs at 9–10;
  LG12 `fleeExit`; LG13 flank pairs; LG20 start zones ≥ 7 from the entry
  mouth; Listen (FL 9.2) at a closed door; `nearStart`, `skittishShape`,
  `ledShape`; `ProtectedIntact` (FL 11.2); the F1 role orders "E G Gb Q
  L G F B" and "E G Gb Q G L F B"; `F1LessonLadder` (FL 7.2); the
  floor-wide alarm **rejected** (FL 9.1).
- **Terrain** (RT): start zones and cover data (RT names "start
  sockets" in CV8 but has no start-socket record; segment 6 derives them,
  6.6.1); CV8's flank square
  and `Floor1NoCoveredStarts`; KP4 hide spots and nooks; KP5 Kegs; the
  KP8 teaching piece; units never occupy pieces; the Clearway never
  blocked.
- **Hazards** (HZ): VP3, VP6; the shover and flyer caps (6.11, 6.12);
  Stir footprints; Tide states; `singer` (the Hymn: WetEdge sockets rise
  one round after the last singer falls); the Stoker; candle racks; Dare
  garrisons.
- **Traps** (TL): `traps` before segment 6 and `trapsResolve` after
  (6.22); TP2; ≤ ½ a group in device areas (6.9); trap damage charged to
  the budget; the Keybearer; the Tattle; the Wager guard "from the
  floor's own segment-6 budget … it does not add one" (6.15); the boxed
  lurker's touch table (6.14); the first-spring rule (TL-16, pending); alarm
  arrivals Alerted (TL-30, pending).
- **Readability** (RS 7.4, **agreed**): RC1–RC9, including RC1 "no
  hidden rules", material only on its own squares, idle motion on
  containers and Furnishings reserved for lurker tells; RC1+ walkable
  objects are rules pieces; RC2 role grammar; RC3 faces mean minds,
  animations duplicate states; RC7 reserved cues; RC8 barks only at idle
  and listed moments; RC9 names never lie.
- **DH:** 17.1 the solo hero never Surprised at `mainPathIndex` 0–1
  (**agreed**); 17.3 the band only in combat, hidden lurkers add
  nothing; 18.2 targets (**agreed**); 21.1 step 1 surprise
  reconciliation **pending**; 21.5 "deaths from Safe ≈ 0".
- **Classes:** `SeedHasTarget` on F1; CL-10; the Shade 10 worst-legal
  profile (ambush 35% solo / 20% party) feeding `stealthShare[L]`.
- **CMP:** CMP-8; the solo composition table owed; the floor-time gate
  (median ≤ 20 min at party 4 on F4–5, **agreed**).

### 3.2 What the approved documents owe this design

- ENM 10.5 steps 2 and 4–11 (FL 13), `placementRules[]` application,
  near-pair partners, the Rival Party fit (FL 13).
- ENM 25 rows: "ambush tells within 6 squares; lurker placement"; "a
  cover room on at least one patrol route".
- The M2 scorer row "Cohesion, body-block, area spread, hold" (via a
  request to the enemies owner); FL-21 (c), the doorway hold.
- The solo composition table (ENM 10.1, CMP 7.7).
- RS 13: theme placement rules, the Wake `nearStart` bias, Heart shapes,
  signature ≥ 2, Pack one family.
- RT 13: who starts where; ambush templates; KP5's "party start squares
  in ambush templates".
- HZ 13: start sockets outside footprints; shover and flyer caps; tags;
  candle racks; Dare garrisons.
- TL 13: the hand-off to `trapsResolve`; the Keybearer; Tattle Dormant
  groups; Wager guards; the boxed-lurker budget.
- CL `SeedHasTarget` on F1; the KP8 lone enemy at the teaching piece.

### 3.3 Tensions found and their resolutions

| Id | Tension | Resolution |
|---|---|---|
| ET1 | MAIN 4.3 says "a **side** noticed without noticing back"; ENM 12.2 says "a **group**" | Per side for the party, per group for monsters (EP-2; **agreed, Q1**) |
| ET2 | ENM 10.3 caps "ambushes" but never defines one | Defined (EP-10): Cuckoo, Baited Room, boxed lurker, Waylay |
| ET3 | Invariant 6 bars limited abilities only in F1 *surprise rounds*; under the per-side reading an F1 Cuckoo has no surprise round, so Ambush Strike would become legal | "On floor 1 a lurker never uses Ambush Strike" (change EP-11) |
| ET4 | ENM 10.5 step 10 puts lurker placement rolls on the dungeon stream; ENM 12.2 puts lurker Stealth on the combat stream | A keyed per-unit hash (change EP-3; both rows named), in the domain `h:lurkStealth` on the floor's attempt-invariant `checks` stream, not on `encounters` (seeds and validation design 4.3; changes SV-6, SV-31; M2) |
| ET5 | Only 5 of 9 themes have a lurker; F5 has none | Waylay for the other four; no roster swap (told plainly) |
| ET6 | F1's only legal ambush host is the guard post adjacent to the lair, whose Gaol lesson is the shut-door pair | Gaol "shut-door pair or hooded cot" (change EP-13 to FL 7.2); the F1 ambush only in the L-G role order (EP-11) |
| ET7 | `Floor1NoCoveredStarts` vs "archers behind cover" | The F1 archer starts uncovered and may *fall back* to cover (a reading for the RT owner, EP-6) |
| ET8 | Off-slot groups (Tattle beyond, Wager guard, Dare garrison, boxed lurker) have no budget source, and TL 6.15 says the Wager guard "does not add one" | The Annex group (EP-26, **agreed, Q2**) |
| ET9 | Tattle Locked Portals sit only on optional content (LG14), whose only slot host is the elite room (never Dormant) | No slot group ever stands beyond a Tattle; without the Annex every Tattle resolves to a Crier, so every Tattle is a Crier until the Annex ships in M3 (told plainly; Q2; after the check) |
| ET10 | The danger band ignores hidden lurkers, against "deaths from Safe ≈ 0" | A seen tell counts as a known threat (change EP-14, **agreed, Q3**) |
| ET11 | Activities need per-group state, while MAIN 4.3 says awareness is "set per monster in data" | A type-level `allowedAwareness[]` read by the template and the group (EP-9, **agreed, Q4**) |
| ET12 | Watchful "back at its post" has no defined post | Its Billet hold squares (EP-15, a reading) |
| ET13 | Formation holds vs invariant 7 (never idle with a reachable target) | The Billet bonus is lost whenever any candidate attacks (EP-6) |
| ET14 | The Clearway "never blocked" (FL 11.2) vs units posted on chokes and sleepers straddling it | Holders start on Clearway-adjacent sockets, sleepers beside it (EP-24; no change) |
| ET15 | Several drafted compositions fit no ENM 17.4 template (Line and Bow has no minions) | Recast to legal templates (7); V12 asserts template legality (EP-31) |
| ET16 | Template Awareness (Pack patrol or dormant; Skirmish Screen patrol; Den dormant) vs "feeding Pack", "Den awake", "Stationary Screen" | The activity's state must lie in the template's Awareness set (EP-9); Waylay only from Stationary templates (EP-12) |
| ET17 | Joining is deterministic hearing (MAIN 4.3), yet the toy's `ChainPullRate` is over target | Segment-6 levers only; no "join chance" or `JoinBudget` without the client (EP-22) |
| ET18 | The Hymn's singers *hold back* the Tide; drafts said "killing singers stops the Stir" | The Ritual re-written (EP-27) |
| ET19 | A Perception overlay of hold squares is a second place the display shows AI inputs (ENM 13.5) | Flagged change EP-28 |
| ET20 | The F1 awareness mix needs a Dormant group | A Dormant Pack: the F1 lair by default, and a Pack guard post is also legal (ENM 17.4 lets a Pack stand at a guard post, Dormant). The proposal's "no F1 guard template can be Dormant" was wrong (lead's call, after the check; 6.5) |

### 3.4 Terms

| Term | Meaning |
|---|---|
| **Formation** | A **battle formation**: a named shape for a template, with core and growth positions, Billets, a Keystone, break triggers and a written counter; every group has one from the moment it is Engaged (6.6) |
| **Start shape** | A named start arrangement used by a Busy, Dormant or Afoot group before it is Engaged (Revel, Bout, the Heap, Patrol File); a Posted group's start shape is its battle formation (6.6.6, 6.7) (lead's call, after the check) |
| **Start socket** | A derived square a unit may start on: a walkable `startZone` square off the Clearway and every socket piece (6.6.1) (lead's call, after the check) |
| **Watched lane** | The entry lane of the group's `watchedPortal` (6.6.1) |
| **Floor allocation** | The draw-free step (9b) that meets every floor-level quota before the per-slot state and activity draws (6.1) (lead's call, after the check) |
| **Formation tier (FT1–FT3)** | The opening, combined-arms and centrepiece tiers of 6.2 |
| **Billet** | A unit's hold squares (0–3) and optional fall-back square, read by the Billet term while its formation holds (6.6.3) |
| **Keystone** | The unit whose loss breaks its formation (6.6.4) |
| **Break** | The moment a formation stops holding; "Line broken" (6.6.4) |
| **Stung rule** | A unit's Billet weight falls to 0 once it is hurt by a source it cannot answer from its hold squares (6.6.3) |
| **Activity** | What a group is doing at placement (dicing, sleeping, rounds); maps 1:1 onto a state and a posture (6.7) |
| **Posture** | Posted, Busy, Dormant or Afoot: the start shape family an activity uses (6.7) |
| **Socket class** | front, ward, back, overwatch, flank, nook, watch, table, blind: derived classes over the start sockets (6.6.1) |
| **Entry lane** | The 3×3 landing plus every square within 3 path of it (6.6.1) |
| **Open zone** | The squares within 6 of a lurker that it can see; the lurker springs only inside 3 of it (6.8.3) |
| **Cuckoo, Baited Room, Waylay** | The three generated ambush forms (6.8.1) |
| **Spoor mark** | A Waylay's tell on the watched Portal's frame or wall (6.8.6) |
| **Bait cache** | A reserved container prop family used only as Baited Room bait (6.8.1) |
| **Kill Box** | The Waylay formation (6.6.6, 6.8.6) |
| **Hedgehog** | A Watchful group re-formed at its Billets in its defensive variant (6.21) |
| **Hold the Hide** | An exploration verb: wait Hidden in the cover-room window for the patrol (6.9) |
| **Terrain Pairing** | A deterministic seating that pairs a unit with a terrain or hazard feature (6.6.8) |
| **Annex** | The one optional, zero-XP, off-slot group per floor on F2–5 (6.13) |
| **Encounter Read card** | The inspect card listing a group's formation, breaks, Keystone and counter (6.20) |
| **`encounters`** | The segment-6 node under the floor's attempt node, `dungeon/floor/{N}/attempt/{a}/encounters` (6.1; SV-1, SV-31; cross-document comparison 2026-11-23, CC-101) |
| **`GroupPlacement`** | The record handed to `trapsResolve` (6.13) |
| **V12 `EncounterPlacement`** | This segment's validator (11.2) |

---

## 4. How other games handle encounter placement, formations and ambushes

| Game | What it does | We take | We avoid |
|---|---|---|---|
| XCOM: EU / XCOM 2 | Pods of 1–3 with a leader; pods "scamper" to cover on activation; XCOM 2 concealment lets the player ambush a pod; reinforcements flared a turn ahead | The player as ambusher (Hold the Hide); telegraphed arrivals (invariant 5) | Scamper: no free reposition on notice; pod chaining (`HearingGap`, the hearing outline) |
| Darkest Dungeon 1/2 | Ranked formations, brawlers front and casters back; rank-gated skills; surprise both ways; camp night ambushes | Role and rank grammar readable at a glance (RC2); "go after the backline" tools | Camp ambushes; random party shuffles |
| Slay the Spire | An easy pool for the first fights of an act, then a hard pool; no repeats within a window; elites marked on the map | A simple first fight per floor; no-repeat windows; visible optional elites | — |
| Hades | Spawn waves telegraphed by spawn markers; armoured elites by depth | "Show where and when before it arrives" | Off-map waves (we have none) |
| Invisible Inc | Readable guard routes, peeking, a global alarm rising each turn | Drawn patrol routes; Listen as peeking | A floor alarm meter (FL 9.1 rejected it; the toy Rumour overshot) |
| Shadow Tactics / Desperados III | Hand-placed mutually-watching guards; placement as a readable puzzle | Mutual watch (the Champion watcher); placement as puzzle | Hand authorship (ours must be template-driven and proven) |
| Fire Emblem | Lances in front of archers; bosses on thrones; reinforcements from forts | Archers behind armour as a timeless read | "Ambush spawns" acting on arrival |
| Advance Wars | Fog: a hidden unit stops your move ("Trap!") rather than taking your turn | **The Waylay principle: the geometry punishes, not the turn order** | — |
| FFT / Tactics Ogre / Triangle Strategy | Ranged units on height | Low cover substitutes for height on our flat floor (MAIN 4.3, **decided**) | — |
| Divinity OS2 / BG3 | Height and environmental combos; unaware units are Surprised; stealth openers | The party's stealth opener; environment pairings within caps | Failed passive checks that start an ambush; opener-stacking exploits |
| Pathfinder: Kingmaker / WotR | Tabletop surprise rounds; camp ambushes | — | Rest ambushes, the most-cited frustration |
| Pillars of Eternity | Scouting reveals hidden creatures; chain pulls | Visible hearing joins | Chain pulls; "spawned from nowhere" |
| Gloomhaven / Frosthaven | Per-player-count spawn keys (absent / normal / elite); documented deterministic monster focus | Core and growth positions by party size; public, learnable AI | — |
| Brogue / DCSS | Leader-and-follower hordes; sleeping and wandering states; vault guardians; out-of-depth monsters | Packs with leaders; sleeping lairs; deterministic guardians | Wandering spawns; out-of-depth deaths without tells (the Errant is Later and telegraphed) |
| Cogmind | Alert rises with player hostility, transparently | Escalation caused by the player and visible, **per group** | A floor-wide meter |
| Battle Brothers | Strong per-faction formations (noble shield walls with polearms; goblin ambushers; undead walls with necromancers behind) | **Per-theme formation styles** | Ambush deployments that start the party in a losing shape |
| Mordheim | Visible reaction stances (Ambush, Overwatch) | Readable hold zones (the Perception overlay) | — |
| Unicorn Overlord | Enemy squads inspectable before engagement, front and back rows | The Encounter Read card; formations read before commitment | — |
| D&D 5E 2014 vs 2024 | 2014: a surprised side loses its first turn; 2024: disadvantage on initiative, because a fight could end before one side acted | Our decided rule is 2014's; we keep party surprise rare | Party surprise as a common event (the 2024 fallback stays a Later lever, EP-41 note) |
| 4E DMG / PF2 / OSR / Lazy DM | Role vocabularies and templates (Double Line, Wolf Pack); "what are they doing"; activity tables | Activities as start states and tells | 4E's "positioning left to the GM": we author it |

**Cross-game lessons.** Players accept danger they could have read and
resent turn loss from information they never had. The best-loved ambush
is the player's own. Faction formations are the cheapest identity.
Archers-behind-armour is universal, but every good game supplies a way
round it. Per-player-count placement keeps a fight's shape. Generated
placement needs templates and proofs where hand-made games have
authorship.

---

## 5. Placement models

### 5.1 The models

- **Model A, Budget and Zones.** Role zones derived from geometry
  (front, back, rear-centre, flank, nook, watch); one zone recipe per
  template; an anchor and a single post term; ambushes only as the
  lurker template. Cheapest and most rule-faithful.
- **Model B, Named Formations and Activities.** Template × formation ×
  activity; Billets; public breaks; a tier ladder; per-theme styles; the
  lurker and posted ambush forms; core and growth positions.
- **Model C, The Living Floor.** Model B plus a floor Rumour meter
  (Quiet, Uneasy, Astir, Roused) and the Prowl (the patrol hunts toward
  Rumour squares and lies in wait), spoor trails, runtime Waylays.

### 5.2 Judges' scores (mean of three judges, 1–10)

| Criterion | A | B | C |
|---|---|---|---|
| Interest and variety | 4.7 | 8.3 | 8.3 |
| Tactical depth | 5.7 | 8.0 | 7.7 |
| Fairness | 8.0 | 8.3 | 6.3 |
| Readability | 7.3 | 8.7 | 6.3 |
| Theme expression | 4.0 | 8.7 | 8.0 |
| Determinism and AI | 9.0 | 8.0 | 6.0 |
| Pacing | 7.7 | 7.3 | 5.3 |
| Fit with the docs | 9.0 | 8.0 | 3.7 |
| Build cost (low is high) | 9.0 | 5.0 | 2.7 |
| **Total (of 90)** | **64.3** | **70.3** | **54.3** |

All three judges chose **B as the core, A as its M1 cut, C as a source
of grafts only.**

### 5.3 Verdict and corrections (lead's call)

- **B is the core.** In the toy it had the lowest F1 death (2.2%), the
  best elite-and-boss share of F3–5 deaths (61%), the most variety, and
  no clock cost.
- **A is the M1 cut:** static start squares, front and back classes
  only, no AI term. A is *not* the end state: it missed the "≥ 50% of
  F3–5 deaths at elites and bosses" reading (49.4%) because zone posts at
  guard posts moved deaths there, and its one-recipe-per-template variety
  is thin.
- **C's ecology is rejected.** The Rumour reopens FL 9.1's rejected
  alarm; with routs as a source it reached Astir on 68–77% of floors
  (target 30–50%) and punished the Herald's core loop; tuned into band,
  the Prowl joined 0.001 fights per floor because the cut-vertex lair
  confines it to the post-lair tail. Its grafts are taken: arrival-Portal
  arrows, Gb never an ambush, posture not state, the Lore preview listing
  formations.
- **Corrections to B before it ships** (from the toy and the critiques):
  breaks fired at a median of round 1 (tightened, EP-7); posted holds
  invite door-camping and plinking (the Stung rule and the doorway lane,
  EP-6, EP-8); the Waylay geometry did not raise damage (gated, EP-12);
  its "first-entry" lurker roll is replaced by a keyed hash (EP-3); its
  0-XP satellites become the Annex (EP-26); its names are unified (A.5).

---
## 6. Rules in full

### 6.1 Pipeline (lead's call; change EP-1)

**Order** (MAIN 6.3, TL 6.22, **settled**; unchanged): `layout` →
`terrain` → `hazards` → `traps` (segment 5, pass 1) → **segment 6
`encounters`** → `trapsResolve` (segment 5, pass 2) → the shared floor
assertion point (V8–V12). The loot draw (ENM 10.5 step 3) keeps the loot
stream. **Read (change LE-14; M1 at WO1, its WO5 camp-exit commit M2; agreed 2026-11-24, roadmap D-08):** loot is now steps 3a (LootPlan, on
the keyed `lootPlan` child stream) and 3, and the draw-free LootBind
pass runs after `trapsResolve`, before the V13g checks at the shared
assertion point; this order is unchanged (loot economy design 6.10).
**The two assertion points (change SV-9; M1; order kept):** the one
floor assertion point becomes **AP1**, the structural point after the
`traps` pass (FL's structural asserts, V9g, V10g, V11g1; HE-7's point),
and **AP2**, the shared point after LootBind and EventPlace, where V12's
floor assertions run. In the canonical stage table, steps 2 and 2a are
GS8, steps 4–11 GS12 (after LootPlan GS10 and loot GS11), `trapsResolve`
GS13 (seeds and validation design 4.4, 5.4).

**Stream (change EP-1).** Segment 6 draws only from `encounters =
Derive(attemptKey, "encounters")` (was `Derive(floorDungeonSubstream,
FNV-1a("encounters"))`: `Derive` applies the FNV-1a `LabelId` itself,
and the parent is the floor's attempt node; changes SV-1, SV-4),
written as RT and TL write their streams (lead's call, after the check;
"sibling" dropped, SV-31), with fixed draw counts and dummy draws
(R-SEED-3). `floorSeed` in the keyed hashes below is the floor key
`dungeon/floor/{N}` (keyed form below; SV-1; cross-document comparison
2026-11-23, CC-101). This
amends ENM 10.5's last paragraph ("every step except 3 draws only from
the dungeon stream", lead's call), adds a child to MAIN 6.3's stream
list (**decided**, extended as FL-2, TR-17 and TL-3 extended it; **new
lead's call:** flagged in 17 on that precedent) and adds a node to the
RS seed tree (segment 11 owns the tree). Purpose: a segment-6 retry
never perturbs segments 3–5.

**Keyed form (changes SV-1, SV-2, SV-31; M1; draw counts
unchanged):** `Derive` and `H` are the one key algebra of the seeds and
validation design (4.1; a child derives from its parent's key, and every
bounded draw uses `Bound`, never `mod`). `encounters` is the never-drawn
node `dungeon/floor/{N}/attempt/{a}/encounters`, a child of the floor's
attempt node beside `traps` and `trapsResolve` (`hazards` is a
grandchild, under `terrain`). One stage draws each stream, so it has two
sub-keys: **`encounters/plan`** (step 2a, GS8; 1 + 2 × floor cap + 1)
and **`encounters/place`** (steps 5, 5b, 10a, 10b, GS12; 4 per slot ×
maxSlots, the boss slot's draws spent as dummies). `floorSeed` is the
floor key `dungeon/floor/{N}`; the lurker hashes of 6.8.5 sit on its
attempt-invariant `checks` child (seeds and validation design 4.2, 4.3).

| Step | Work | Draws (fixed) |
|---|---|---|
| 2 | Slots to hosts; homeRooms frozen (FL-3) | 0 |
| 2a | **EncounterPlan** (below): ambush count, form and host per ambush, the Annex purpose; the Tattle-bias set read only | 1 + 2 × floor cap + 1 per floor, dummies when unused |
| 3 | Loot (loot stream; unchanged; step 3a, LootPlan, before it on `lootPlan`: loot economy design 6.10, LE-14) | — |
| 4 | Carrier reservation (elite room, lair, guard posts by index, patrol; unchanged) | 0 |
| 5 | Template choice, weighted by tier and position (6.2) under the variety rules, the Keybearer filter (6.13) and the 2a form as **hard filters**; yields a ranked list (below) | 1 per slot |
| 5b | **Formation choice** from the legal list filtered by tier, floor, `partyCountAtGeneration`, host kind, theme, socket-class coverage and the 2a ambush form; yields a ranked list (lead's call, after the check) | 1 per slot |
| 6 | Fill that formation: core positions, then growth (6.3), against `budget − itemCostAdd − trapChargeCost`, from the fill target up (6.2); a type is admitted only if the group's legal state set stays non-empty (6.4) | 0 (fixed order) |
| 7 | Compose; freeze cost at the homeRoom | 0 |
| 8 | Validate; K = 4 candidates *(illustr.)* walk the ranked lists; the fallback chain (below) | 0 |
| 9 | Cluster pass (within host or `equivalentHosts`); any change re-runs 5b–8 for that slot (below) | 0 |
| 9b | **Floor allocation** (below): forced states, Busy caps, joiner levers | 0 |
| 10a | Start state within the group's legal state set, for slots the allocation left open (6.5) | 1 per slot |
| 10b | Activity and its start shape (6.7), filtered to keep every floor maximum | 1 per slot |
| 10c | Terrain Pairings (6.6.8) and the defensive variant | 0 |
| 10d | Start-square solver and Billets: deterministic, draw-free; fixed role priority; ties by fixed square order (ABL 8.1) | 0 |
| 10e | Patrol File order and Circuit phase (the `layout` pass's phase); the tell square (the first legal socket by id that satisfies `TellSight`) | 0 |
| 10f | Lurker Stealth: keyed hashes per band (6.8.5; change EP-3), domain `h:lurkStealth` on the floor's `checks` stream (SV-6; M2) | 0 on the stream |
| 10g | Floor repair pass (below) | 0 |
| 11 | XP shares (unchanged) | 0 |
| out | `GroupPlacement` record to `trapsResolve` (6.13) | — |

**Ranked lists (lead's call, after the check).** Steps 5 and 5b each
spend one weighted draw, which picks the first candidate. The rest
follow in descending weight, ties by id. The K candidates of step 8 and
the fallback chain walk these lists with no new draw. **Composition
guarantees** (ENM 10.4: the cowable group, a leader from F2, the steal
target) are step-5 hard filters on the last slot, in the fixed slot
order of 9b, that can still meet each unmet one, so no draw is spent on
them; the variety rules, the ≤ 2-per-floor battle formation count and
the no-repeat rule filter steps 5 and 5b in slot order.

**Step 2a, the EncounterPlan (lead's call, after the check).** Inputs:
`mousetrapsThisRun` (carried in the run state, as TL carries its run
counts), the boxed-lurker sockets the `traps` pass drew, the floor's
role order and `partyCountAtGeneration`. Draws in this order:
1. **ambush count**, 0 to the floor cap (6.8.1), 1 draw. **The boxed
   lurker counts first against the cap**; the count drawn is the number
   of generated forms in the remaining places, clipped to the legal hosts;
2. **form and host per ambush**, 2 draws per place up to the floor cap,
   dummies when unused. A host takes at most one form (6.8.1);
3. **the Annex purpose**, 1 draw among the needs present, used only to
   break the priority tie of 6.13 between needs of equal rank in
   different rooms (a dummy otherwise);
4. the Tattle-bias set: read from the `traps` candidates, 0 draws.

2a's choices are final for segment 6; `trapsResolve` only confirms or
drops a binding (6.13). **An allocated form whose fill fails** at steps
6–8 becomes a non-ambush guard post that keeps its step-5 draw (the
template list re-filtered without the form), and the form is logged as
not drawn (the ENM 10.3 cap is a maximum, never a quota; 6.8.1).

**Step 9, the cluster pass (lead's call, after the check).** ENM step 9
may downgrade a composition or move a group to an equivalent host. Any
such change re-runs steps 5b–8 for that slot, draw-free, from its ranked
lists. A move is legal only if the slot's ambush form, formation
coverage and host rules still hold at the new host; otherwise the pass
downgrades instead.

**Step 9b, the floor allocation (lead's call, after the check).**
Draw-free. Slot order is fixed: main-path slots by `mainPathIndex`, then
the elite room, then the patrol. In that order:
1. each unmet **floor minimum** is forced on the first slot whose legal
   state set (6.4) admits it: the awareness mix (≥ 1 Patrolling, ≥ 1
      Stationary, ≥ 1 Dormant, ENM 10.4), the 6.5 spread and the F1
   Dormant Pack (6.17);
2. the joiner levers of 6.12 are applied to each near-pair partner;
3. every **floor maximum** becomes a filter on the 10a and 10b draws of
   the open slots: the 6.5 Dormant and Stationary counts; the Busy cap
   (6.5); the `StealthOpenable` ceiling. (Each battle formation ≤ 2
   per floor and no (template, battle formation) repeat are already hard
   filters at 5b.)

**Step 10g, the floor repair pass (lead's call, after the check).**
Draw-free, after 10f. In order: (1) ≥ 2 activity kinds, by switching
the last eligible Busy group's activity to its next-ranked legal one;
(2) `StealthOpenable`: a shortfall switches the cheapest eligible Posted
group to Busy (or Dormant where its legal state set allows); an excess
switches the last Busy group to Posted; (3) the showcases, by switching
a group to its next-ranked legal formation that serves the missing
class. Each switch re-runs 10c–10f for that group only.

**Pending request (HL-19; M4; agreed, Q4 of the heirlooms and curses
design; the EP owner rules):** an EP-owned **Malady filter**, draw-free,
after placement: at most one Malady-carrying elite type per floor
*(illustr.)*; it keeps the optional `maladyAbility` slot on the first
Malady-carrying type in frozen placement order, turns it off on every
later one, and turns it off on any elite placed in the boss antechamber
zone (`MaladyNotBeforeBoss`). It touches no type, budget or draw; the
Stratum Card's "Maladies abroad" line reads its result (an RS 6.9
request). Nothing changes until the EP owner rules (heirlooms and
curses design 4.3.3, 9.1, 11).

**Start sockets and constructive exclusions before 10d** (lead's call;
EP-25; the socket definition is a lead's call after the check): the
solver works only over **start sockets** (6.6.1), never over every
square. Removed from them before 10d: the `traps` pass's trigger
squares; squares that would put more than ½ a group in device areas;
Stir footprints. CV8's guarantees and `Floor1NoCoveredStarts` are
re-proven over this set in V12 (11.2).

**`trapChargeCost` (lead's call, after the check; owned with TL).** TL
6.9 charges expected party-side trap damage in HP-equivalents; budgets
are in levelCost units. The conversion is `trapChargeCost =
FloorDiv(expectedTrapHpEq × budget, fightHpEqTarget)`, where
`fightHpEqTarget` is the expected HP-eq per fight for that band and
floor (ENM 10.2, set by the simulator) *(illustr.)*.

**Fallback chain, per slot** (change EP-32 to ENM 10.5 step 8): (1) the
next-ranked formation on the step-5b list for the same template,
re-entering at 5b; (2) the next step-5 candidate; (3) the theme's
cheapest legal Picket in **Picket Line**; (4) **terminal:** that Picket
placed role-blind under the hard constraints only (TP2, VP3, CV8, LG20,
KP5, `Floor1NoCoveredStarts`), with no formation clauses, so every
*slot* ends legal. Every retry is logged for segment 12 (the chain is an
LR1 in-stage repair, counted by `FormationFallbackRate`; the floor
retry below is logged `StageRetried`; seeds and validation design 5.5;
change SV-10). Report
`FormationFallbackRate` (< 10% overall, < 5% on F1, *illustr.*).

**Floor-level failure policy (lead's call, after the check; mirrors TL
6.22).** Floor assertions (the awareness mix, the 6.5 spread, the Busy
caps, variety, `StealthOpenable`, showcases, `SeedHasTarget`, the Annex,
Tattle and Wager bindings) run at the shared assertion point after
`trapsResolve` (**AP2**, after LootBind and EventPlace, as V12g; the
Tattle and Wager bindings are TL's actions at GS13 and V12g only
asserts them; seeds and validation design 5.4; change SV-9). The slot
chain cannot repair them, so:
1. the repair pass 10g runs inside segment 6 before the hand-off;
2. if a floor assertion still fails, segment 6 is retried **once** on
   the keyed child `…/attempt/{a}/encounters/retry1` (its `plan` and
   `place` sub-keys; changes SV-2, SV-4) with the same fixed draw
   counts, and `trapsResolve` re-runs on the new placement (at its own
   unchanged key; the rerun span is GS8–GS15; seeds and validation
   design 4.3, 5.4);
3. **terminal:** the floor ships with the failing assertion logged in a
   `V12FloorFallback` report. Only these may be waived: the class
   showcases, ≥ 2 activity kinds, the `StealthOpenable` ceiling and the
   Annex binding (its need falls to its approved failure action, 6.13).
   **Never waived:** the awareness mix, the F1 package (6.17) with
   `SeedHasTarget`, the ENM 10.3 caps, the Baited Room limits and every
   hard constraint of the terminal Picket. If one of these still fails,
   the slot the allocation forced takes a terminal group in the forced
   state: the terminal Picket for Patrolling or Stationary, and the
   theme's cheapest legal Pack, role-blind in the Heap, for Dormant
   (every theme roster has a Pack fill, a content test), which by
   construction meets them.

**On the one ladder (seeds and validation design 5.5; change SV-10;
M1; this policy kept):** the slot chain, the cluster re-run 5b–8, 9b
and 10g are **LR1** in-stage repairs; the retry above is **LR2**
(`stageRetry(encounters)`, at most once per attempt, logged
`StageRetried{floor, attempt, stage, check}`); the terminal floor
fallback is **LR4** (`waivable(V12FloorFallback)`, for the waivable set
only, logged `GenerationFallback`). The never-waived list has the class
`neverWaived`: should one still fail after the forced-state terminal
group, that is **LR5** `GenerationDefect`, which takes the next floor
attempt (`dungeon/floor/{N}/attempt/{a+1}`, at most 3 attempts in
every build). Dev and CI builds then fail hard once the floor is
recorded (never silent); a release build whose last attempt fails ends
the run cleanly, "the dream unravels", in place of DH 12.4's crash
(agreed, Q2 of the seeds and validation design). Every build runs the
same ladder.

### 6.2 Budget and difficulty curve (lead's call; change EP-20)

**Budgets are the approved formula** (ENM 10.1, **settled**). Segment 6
adds no slot, budget or XP, **except the Annex** (EP-26, **agreed, Q2**):
one extra encounter budget at the patrol band, paying 0 XP, inside the
cluster, joiner and 6.4 bounds (6.13). A **FloorEnvelope** (the sum of slot
budgets) is a *reporting* figure only, never used to move budget between
slots (lead's call): the 6.4 guards and the per-band XP are proven per
template × band.

Illustrative budgets at the expected party (weights 15/23/31/39/39,
ladder 100):

| Floor (party) | Patrol | Guard | Lair | Elite room | Boss escort | Baited Room (`FloorDiv(guard × 85, 100)`) |
|---|---|---|---|---|---|---|
| F1 (solo) | 8 | 12 | 16 | 19 | 24 | — |
| F2 (duo) | 14 | 20 | 28 | 33 | 41 | 17 |
| F3 (trio) | 21 | 31 | 43 | 49 | 62 | 26 |
| F4 (four) | 28 | 40 | 57 | 65 | 81 | 34 |
| F5 (four) | 30 | 42 | 60 | 68 | 85 | 35 |

**Fill curve inside the approved 85–100%** *(illustr.)*: `fillTarget‰ =
850 + FloorDiv(150 × mainPathIndex, max(1, n − 1))`, where n and
`mainPathIndex` are FL's (main-path rooms and the host's index); the
patrol uses its homeRoom's index (FL: n − 2 on F2–5) and the elite room
its parent room's index (lead's call, after the check). **The fill
target is the minimum accepted cost:** step 6 takes the first legal fill
in fixed order whose cost lies from `ceil(fillTarget‰ × budget ÷ 1000)`
up to the budget (lead's call, after the check). Exceptions:
- **Gb stays on the curve** (Model A's 1000‰ rejected: a
  stiffest-on-the-floor Gb raised skips, and ENM 20.1's band is measured
  without the elite room) (lead's call, after XB-L3).
- Lair ≥ 925‰; elite room 950–1000‰; **Cuckoo and Waylay at full band**
  (6.8). **The Baited Room budget** is `FloorDiv(guard × 85, 100)`, and its
  window is 850–1000‰ of that budget (lead's call, after the check).
- **F4–5: any composition with a +2-level unit on a back or overwatch
  position fills at ≤ 925‰** (lead's call, after XB-H1).
- F1: the 2.5% cumulative round-1 budget (ENM 6.4, **settled**) is a hard
  ceiling; on a miss the highest-index guard post steps down by 50‰ until
  it passes, **never below 850‰**; if it still misses, that slot takes
  its fallback chain (lead's call, after the check). When the F1 Cuckoo
  is drawn, the lair fills at the low end of the window (850‰), which
  **overrides the lair's 925‰ floor** on that seed (XB-M5).

**Formation tiers and the run curve.** Tiers rate the 15 battle
formations; the 4 start shapes carry no tier (lead's call, after the check;
6.6.6). A Heap or gorged Revel holding an elite counts as a
hard feature through "an elite present" below.
- **FT1 (opening):** Picket Line, Door Line, Crescent, Scramble.
- **FT2 (combined arms):** Phalanx, Skirmish Spread, Retinue, Honour
  Ring, Champion Watch, Bait and Nook, Kill Box (Waylay).
- **FT3 (centrepiece):** Crossfire, Pincer, Grindstone, Ritual (M3).

| Floor | Legal tiers | Notes |
|---|---|---|
| F1 | FT1, plus Champion Watch (elite room) and Bait and Nook (Cuckoo, 6.17) | No Waylay, Phalanx, Crossfire, Pincer |
| F2 | + FT2 | Kill Box at party ≥ 2 |
| F3 | + FT3 (Crossfire and Pincer at party ≥ 3); Ritual (M3, the Choir only) | |
| F4–5 | All except the Ritual | **New lead's call:** the proposal listed "Ritual (M3)" on F4–5, but the Ritual is Choir-only (6.6.7) and the Choir is an F2–3 theme, so it is legal only on F3 |

**Within a floor:** the first main-path fight draws from FT1
(`FirstFightSimple`, 100%); the lair takes **the highest tier legal for
its template and composition on that floor** (a Den lair's Crescent is
FT1, an Honour Guard's Honour Ring FT2; lead's call, after the check);
**from F2**, the last guard post before the Forecourt is FT2 or FT3 (F1
is exempt: its only FT2 guard formation is the Cuckoo's Bait and Nook;
lead's call, after the check);
**hard-feature spacing:** hard features are an ambush form, an elite
present, a leader template, a rigged host, a near pair, an FT3
formation; the first fight host carries none; no host carries more than
2; two consecutive main-path fights carry features only from F3. On F1
the role order forces the ambush host next to the lair, so F1 adjacency
is reported (`F1FeatureAdjacency`), not banned.

**Variety (hard V12 assertions; change EP-20 to ENM 10.4 Variety):** the
approved rules, plus no (template, battle formation) pair twice in a row
on the main path; each battle formation ≤ 2 per floor; ≥ 2 activity
kinds per floor (when ≥ 2 non-patrol awake groups exist). These are met
by the floor allocation (9b) and the repair pass (10g), not hoped for
from per-slot draws (6.1). The toy saw 11–39% repeats when this was
only reported (XB-L4).

### 6.3 Party size (lead's call; change EP-19)

- **Budgets scale by weight** (approved). **Shape scales by positions:**
  each formation declares **core positions** (its minimum shape) and
  **growth positions** in fill order, each within the template's slot
  ranges (V12; XK4). Step 6 fills core first, then growth (Gloomhaven's
  per-player-count key: more bodies in a fixed shape, not more stats).
- **`FormationSurvivesFill`:** a formation is drawable at a weight only
  if its core fits the fill at that weight, down to the shortfall
  minimum (CMP-8).
- **What the gates read (lead's call, after the check):** every
  placement gate on party size (the Baited Room, Kill Box, Crossfire,
  Pincer, `StartArc`, the solo rules) reads **`partyCountAtGeneration`**,
  the actual units at floor generation; growth positions read the
  weight. Each group freezes **`billetScale`** at generation, so the AI
  reads group data, never live party state (invariant 1).
- **Solo on F2–5** (ENM 10.1: ≤ 1 standard + the minion cap,
  **settled**): only Picket Line, Crescent and Door Line **with a front
  standard**; the shooter, if any, is a minion-cost unit, so a ranged
  standard is never the solo standard (lead's call, after XB-H4).
  **`billetScale` 0 for a solo hero on F2–5** (a lone holder only adds
  rounds and gives a ranged hero free shots). `StartArc` 120°. **No
  ambush form** (no Cuckoo, Baited Room or Waylay: a Cuckoo's bait plus
  lurker standards breaks the ENM 10.1 solo rule), and no Kill Box,
  Crossfire, Pincer or Bait and Nook (the Cuckoo bar: lead's call, after
  the check).
- **Floor 1 (always solo; lead's call, after the check):** ENM 10.1's
  solo rule covers F2–5 only, so the F1 Cuckoo stands (6.17).
  **`billetScale` 500‰ on F1**, with the Stung rule, so the F1 lessons
  work (the Turnkey holds the cell door; the Huntsman holds the trail
  mouth, so "go around" means something). FL-21 (c)'s doorway hold is
  thus taught where formations are first met. A toy and simulator
  re-run is owed (10).
- **The solo composition table** (owed by ENM 10.1 and CMP 7.7) gains a
  formation column. **Re-tuned (cross-document comparison 2026-11-23,
  CC-104):** levels are relative to the homeRoom's room level, minion
  levels are stated, and each cell is the highest-cost fill at or below
  B, so every cell lies inside the ENM 10.1 window (85–100%; lowest 852‰,
  at B 54). A cell is taken only if its cost is also at or above
  `ceil(fillTarget‰ × B ÷ 1000)` (6.2); where it is not (for example B 8
  at a fillTarget above 875‰), the slot takes its fallback chain (6.1),
  so no placed cell lies below fillTarget. Costs are ENM 10.1's
  `levelCost` (a minion `max(2, FloorDiv(levelCost, 3))`):

| Solo budget B *(illustr.)* | Composition (cost) | Formation |
|---|---|---|
| 7–8 | 1 standard at −1 (7) | Picket Line (Patrol File as the start shape when patrolling) |
| 9 | 1 standard at −1 + 1 minion at −2 (9) | Picket Line |
| 10–11 | 1 standard at 0 (10) | Picket Line |
| 12 | 1 standard at 0 + 1 minion at −1 (12) | Picket Line (a Pack needs 2 minions, so no Crescent; lead's call, after the check) |
| 13 | 1 standard at 0 + 1 minion at 0 (13) | as above |
| 14 | 1 standard at 0 + 2 minions at −1 (14) | Crescent or Door Line (minion shooter) |
| 15 | 1 standard at 0 + minions at 0 and −1 (15) | as above |
| 16–17 | 1 standard at 0 + 2 minions at 0 (16) | as above |
| 18 | 1 standard at +1 + 2 minions at −1 (18) | as above |
| 19 | 1 standard at +1 + minions at 0 and −1 (19) | as above |
| 20 | 1 standard at +1 + 2 minions at 0 (20) | as above |
| 21 | 1 standard at +1 + minions at +1 and 0 (21) | as above |
| 22–23 | 1 standard at +1 + 2 minions at +1 (22) | as above |
| 24 | 1 standard at +2 + 2 minions at −1 (24) | as above |
| 25 | 1 standard at +2 + minions at 0 and −1 (25) | as above |
| 26 | 1 standard at +2 + 2 minions at 0 (26) | as above |
| 27 | 1 standard at +2 + minions at +1 and 0 (27) | as above |
| 28–29 | 1 standard at +2 + 2 minions at +1 (28) | as above |
| 30–31 | 1 standard at +2 + minions at +2 and +1 (30) | as above |
| 32–37 | 1 standard at +2 + 2 minions at +2 (32) | as above |
| 38–54 | 1 standard at +3 + 2 minions, the highest-cost pair at or below B (32–46) | as above |

- **`SoloShapeMonotone`** extends `SoloBudgetMonotone`: as weight rises,
  the standard's level never falls, threat lanes at fight start never
  decrease, and front positions ≥ back positions.
- **Parties of 3–4:** the 6-unit cap binds; growth goes to flank minions
  and a second front; Crossfire and Pincer (F3+) and a 180° arc unlock.
- **Never scale hazards, traps, terrain or formation geometry by party
  size.** Sockets, Billets and break triggers are identical at every
  size, so every guard is a function of the composition and the room.
- The ENM 6.4 rows and the F1 solo summed-round-1 row run on placed
  geometry at every party size (EP-30); the F1 row also applies to solo
  F2–5 as approved.

### 6.4 Composition (settled templates; lead's call on use)

The ENM 17.4 templates are used as written (**settled**). The ones that
bind placement most:

| Template | Slots (settled) | Awareness (settled) → states | Battle formations here | Start shapes here |
|---|---|---|---|---|
| Picket | 1–2 standards + 0–2 minions | Patrol or stationary → Patrolling, Stationary | Picket Line, Door Line (a front standard + a marksman standard); Kill Box (Waylay) | Patrol File; Revel, Bout (Busy) |
| Pack | 1 brute (M2: or skirmisher) + 2–4 minions of one family | Patrol or dormant → Patrolling, Dormant | Crescent; Pincer (F3+) | Patrol File; the Heap; gorged Revel (Dormant) |
| Line and Bow | 1–2 soldiers or brutes + 1 marksman | Stationary at a chokepoint → Stationary | Door Line, Phalanx (2 soldiers); Kill Box (Waylay) | Revel, Bout (Busy) |
| Warband | Leader + 1–2 front + 0–4 minions | Stationary or dormant → Stationary, Dormant | Retinue, Phalanx (2 front), Pincer (F3+), Ritual (M3); Kill Box (Waylay) | Revel, Bout (Busy); the Heap |
| Den | Mother + 1 standard + 2–4 minions | Dormant → Dormant | Crescent (the mother as alpha) | the Heap |
| Skirmish Screen | 2–3 skirmishers | Patrol → Patrolling | Skirmish Spread (never Waylay; never at a guard post, 6.10) | Patrol File |
| Ambush | Bait (standard or container) + 1–2 lurkers as a separate group | Hidden → the bait group Stationary; the lurker group Stationary with the Hidden condition at start (EP-10) | Bait and Nook (Cuckoo / Baited Room) | — |
| Battery | 2 marksmen + 1 soldier (± Challenger elite) | Stationary → Stationary | Crossfire; Kill Box (Waylay) | — |
| Honour Guard | Challenger elite + marksman or hinderer + 0–2 minions | Stationary → Stationary | Honour Ring | — |
| Gauntlet | Hinderer + 2 brutes | Dormant or stationary → Dormant, Stationary | Grindstone | the Heap |
| Scavengers | 2–3 skittish standards or minions | Patrol → Patrolling | Scramble (its contact shape) | Patrol File |
| Sleeper | Dormant elite + 0–2 | Dormant → Dormant | Picket Line (the elite as the front Keystone) | the Heap; gorged Revel |
| Champion | Elite + 1–2 with a watcher | Stationary, awake → Stationary | Champion Watch | — |

**The Awareness column as states (lead's call, after the check).**
"Patrol" reads Patrolling; "stationary" and "Stationary, awake" read
Stationary; "dormant" reads Dormant. The Ambush row's "Hidden" is a
condition, not a 12.1 state: the bait group is Stationary (a container
bait has no state) and the lurker group is Stationary with Hidden at
start (EP-10 names the row). **A group's legal state set** = the
template's states ∩ every member type's `allowedAwareness[]` (Q4,
**agreed**). Step 6 admits a type only if that set stays non-empty and
still holds any state the floor allocation forced on the slot (6.1).
**New battle-formation homes (lead's call, after the check):** a Den
fights as a Crescent and a Sleeper as a Picket Line once Engaged, since
every group needs a battle formation (6.6.6).

**Rules** (approved, restated as fill constraints): ≤ 3 types; ≥ 2
roles at 3+ whole units (Pack, Skirmish Screen, Scavengers excepted;
Picket and Sleeper exempt); leaders ≤ 1 per group with ≥ 2 others
including a non-minion; Exhorting never in a leader group; elites only
where ENM 7.2 allows; theme caps (Gut ≤ 2 acid sources, Supper ≤ 1
Frozen source, Heart ≤ 2 psychic types and ≤ 1 Shadow-Puppet pack); F1 ≥ 750‰
non-fearless, 0 mindless, 0 hard control.

**New composition rules** (lead's call; EP-31):
- **Composition grammar:** from F2, a formation with a back position
  fills ≥ 1 front position at every party size; a minion is never the
  only body between the landing and a marksman.
- **Per-theme template eligibility table** (a content-test input; 7.1):
  the Supper has no Honour Guard or Battery Challenger (no promotable
  soldier); the Gut has no Gauntlet (acid cap) and no Skirmish Screen;
  the Archive has no Skirmish Screen; the Carnival and Heart have no
  Gauntlet; the Choir, Carnival, Supper and Heart have no Ambush.
- **`AcidSourcesAfterOverlay`:** the Gut acid cap counts the Tenants'
  acid overlay.
- **One shover type per encounter in Void and clock rooms** (HZ 6.11,
  applied to every brute and soldier, since every such at-will carries
  Shove).
- **Archive cowable groups hold no Inkling** (mindless; ENM 14.8).
- **Mixed-group rooms:** only the Ambush (bait group + lurker group), the
  Champion (one group with its watcher) and runtime joiners. Each counts
  as one encounter for budget, units cap, 3-type cap, XP and variety,
  with morale per group (two-group "watched sleep" rooms: M3).
- **Mindless members of a Busy group** **(new lead's call:** the
  proposal both barred Busy for mindless groups and placed Frozen
  Footmen in the Supper's Busy Revel, 7.8**):** Busy is barred for a
  group whose members are all mindless; in a mixed Busy group, mindless
  members take no activity and no Billet and stand on front sockets in
  their plain idle (RC3: no tactical stance, no activity loop).
- **Roster placeability** (told plainly; an assertion): every approved
  roster type must be placeable in some reachable (theme, floor, wave)
  cell. Without EP-11 and EP-13 the Silent Penitent and the Ghillie
  would never appear in M2.

### 6.5 Start states and their spread (lead's call)

- **Only Dormant, Stationary or Patrolling** (MAIN 4.3, **decided**).
  The Boss Court starts Watchful (**settled**). **No ordinary group starts
  Watchful or Alerted** (contested by the lead: the brief's "Dormant, Wary,
  patrolling, Alerted" list and Model A's "guard posts Wary" both fail
  ENM 12.1 and ENM 17.2's cut of "cannot be Surprised", which would
  delete the Stealth guarantee).
- **The state must lie in the group's legal state set** (the template's
  Awareness read as states, ∩ its types' `allowedAwareness[]`; 6.4; V12;
  XK3). **The F1 lair is a Dormant Pack** (lead's call, kept: it is the
  sleeping-lair lesson and meets the awareness mix). A Pack guard post may
  also be Dormant on any floor (ENM 17.4), and from F2 so may a Warband
  (a Gauntlet is a lair-only template, ENM 17.4, so it never holds a
  Dormant guard post; cross-document comparison 2026-11-23, CC-100); the
  first fight host never is (it is Posted or Busy, 6.17). **Told
  plainly, after the check:** the proposal's "no F1 guard template can be
  Dormant" was wrong; ET20 is corrected.

| Floor | Patrolling | Stationary | Dormant | Ambush forms *(illustr.)* | Busy share of Stationary *(illustr.)* |
|---|---|---|---|---|---|
| F1 | 1 (Circuit) | 2–3 | 1 (the lair Pack) | Cuckoo ≤ 1 (6.17) | 600‰ |
| F2 | 1 | 2–3 | 1–2 | ≤ 1 | 500‰ |
| F3 | 1 | 2–3 | 1–2 | ≤ 1 generated (+ boxed lurker, M3) | 300‰ (cap from F3, XF13) |
| F4–5 | 1 | 3 | 1 | ≤ 1 generated, 2 only on untrimmed F5 (+ boxed lurker, M3) | 300‰ |

The Busy cap is `FloorDiv(busyShare‰ × stationaryCount + 500, 1000)`,
rounding half up (so 300‰ of 3 Stationary groups is 1, and 600‰ of 3 is
2), which keeps one Busy group on F4–5; the floor allocation
(9b) forces the minima and filters the maxima of this table (lead's
call, after the check).

- **The dream wakes as you descend:** deeper floors are more Posted,
  less Busy.
- **Theme biases:** the Wake's Dormant bias outside `nearStart` (RS
  8.5); Heart `skittishShape` rooms take skittish leaderless groups,
  `ledShape` rooms take a Retinue or a fearless Pale Mare anchor (RS
  8.11). (The proposal's "Tattle-eligible beyond-rooms ×3 Dormant
  weight" is removed: no slot host lies beyond a Tattle, ET9; lead's
  call, after the check.)
- **`StealthOpenable`** (lead's call, after XF12 and XB-M1): each floor
  holds **at least 2 and, at party ≥ 2, at most 3** non-patrol groups the
  party can Surprise by a path out of their line of sight; at least 1
  before the lair on F2–5, counted only if a party-median sneak chance is
  ≥ 50% *(illustr.)* (Dormant, or Busy with backs turned). Counted after
  10f and repaired by the 10g pass (6.1).
- **The awareness mix** (ENM 10.4) is asserted after step 10, never
  assumed (a Stationary Honour Guard lair forces a Dormant guard post: a
  Pack, or a Warband from F2).

### 6.6 Formations and positions

#### 6.6.1 Socket classes (lead's call; change EP-5)

**Start sockets (lead's call, after the check).** RT has no start-socket
record (RT 6.2.5 `Socket.class` lists piece classes only, and RT 13 hands
"segment 6's start sockets" to this design). They are derived: **the
start sockets of a room are its FL `startZone` squares that are
walkable, not on the Clearway, and not occupied by any socket piece in
any on/off state, sorted by (y, x) per ABL 8.1.** CV8 and
`Floor1NoCoveredStarts` range over this set (a reading for the RT owner,
EP-5). No new authored field; an optional `roleHint` on the RT 6.2.5
Socket (an M3 request) only breaks ties.

Socket classes are derived deterministically from FL `partyEntrySquares`
and the landing, the Clearway, CV2 cover, KP4 nooks and the room's
Portals, **over the start sockets only**. **Definitions (lead's call,
after the check):** the **landing** is the watched Portal's LG20 landing
(the main-path entry for main-path hosts, the Door for the elite spur);
the **entry axis** runs from the landing centroid to the forward
Portal's landing, or, with no forward Portal (a dead end such as the
elite spur), to the centroid of the farthest third of start sockets;
**thirds** are `FloorDiv` over the start sockets sorted by path from the
landing (ties by square order). A socket may meet several classes and
keeps all of them for eligibility; for solver priority it takes one, by
**precedence nook > watch > overwatch > back > ward > flank > front >
table**; blind applies only in a Waylay host.

| Class | Definition *(illustr. thresholds)* |
|---|---|
| **front** | Nearest third by path to the landing, or adjacent to the choke / forward Portal (start sockets are never on the Clearway, 6.6.5) |
| **ward** | Behind ≥ 1 front socket on the entry axis, within 2 of it |
| **back** | Farthest third with line of sight to the entry lane |
| **overwatch** | A back socket Covered against the entry lane; F2–5 only as a start |
| **flank** | > 45° off the landing-to-forward-Portal axis, or nearest a flank-pair Portal (LG13) |
| **nook** | A KP4 nook or Drape square; never adjacent to a Void, Stir footprint or Tide socket (VP6) |
| **watch** | Line of sight to every square adjacent to the elite socket |
| **table** | A start socket adjacent to an **existing RT Low piece or Furnishing of the theme** named by the activity (a desk, table, hay manger, pew-row edge, catafalque, cantor's desk); units never occupy pieces. Segment 6 adds no piece: V12 `TableSocketPieceExists`; a new activity piece is a request to the RT owner (lead's call, after the check) |
| **blind** | Out of line of sight of the landing and `partyEntrySquares`, with line of sight to the watched lane (Waylay) |

**Entry lane** (lead's call): the 3×3 landing plus every square within 3
path of it. It is the party's first two steps, and KP5's undefined
"party start squares in ambush templates" is defined as the entry lane
plus, for lurkers, the open zone (EP-8). The **watched lane** is the
entry lane of the group's `watchedPortal` (lead's call, after the
check).

#### 6.6.2 Per-role placement

| Role (ENM 17.1) | Socket priority | Facing | Billet | Never |
|---|---|---|---|---|
| Soldier (Holder) | front at the choke | The watched Portal | Hold 1: the choke square | nook, blind |
| Brute (Charger) | front, then flank | Entry | Hold 2 (loose); a ≥ 3-square charge lane to the entry lane *(illustr.)* | cover |
| Marksman (Kiter) | overwatch (F2+), then back | Entry lane | Hold 1; a fall-back square (6.6.3) | adjacent to a front unit on F1 (so it can be reached); blind in solo fights |
| Skirmisher (Raider) | flank | 45° to the axis | Until its first attack | a Pincer against party ≤ 2 |
| Hinderer (Disruptor) | ward | Entry | Hold 1 | nearest unit to the landing |
| Leader (Commander) | ward, one rank behind the line | Entry | Hold 1, within 2 of ≥ 2 own members (`AuraCover`) | nook, blind, nearest unit; Kennel leaders stay visible from the entry mouth at ≥ 7 (FL 7.3, **settled**) |
| Lurker (Ambusher) | nook only | Its open zone | Ambusher profile unchanged | a Waylay; a near pair; leader |
| Swarmer (Mob) | flank, then front, then table | Entry | None | the only body between the landing and a marksman (F2+) |
| Mindless (Automaton) | front by unit order | Entry | **None** (RC3: no tactical stance) | Waylay, Busy (6.4) |
| Elite | front or ward by role; a Sleeper on the deepest socket | — | By role | nook, blind, Waylay |
| Challenger (Protector) | adjacent to its ward, between it and the landing | Entry | `ward` (approved) | — |

#### 6.6.3 Billets and the AI hand-off (lead's call; change EP-6; a request to the enemies owner)

**Facing is presentation only** (`units[].facing`, for the pose grammar
and the art): no rule reads it (lead's call, after the check).

Each placed non-patrol, non-mindless, non-lurker unit gets `billet = {
holdSquares[0–3], fallBackSquare? }`, seated at its battle-formation
square (for a Busy or Dormant group, the square the formation would give
it, not its start shape square; 6.7). **Billet** is used instead of "post"
(every document uses "guard post") and "anchor" (FL-12 retreat anchors).

**Hold squares (lead's call, after the check; draw-free).** A position's
`hold` count N comes from its `FormationDef` entry. A Holder's set is the
choke square. Any other position's set is its battle square plus up to
N − 1 squares within 1 of it, taken in fixed square order, the choke
square first where adjacent, skipping trigger squares, Void, squares
within 2 of a Void edge or lethal hazard, and squares whose occupation
would block the Clearway. A unit with N = 0 has none; its distance tests
use its battle square. **The fall-back square** is chosen by the
position's `fallBack` predicate, an enum: `coverBehind` (the nearest
square ≥ 2 farther from the landing that is Covered against the landing),
`losBreak` (the nearest such square out of line of sight of the
landing), `towardLeader` (the nearest such square within 2 of the
leader), each then filtered by the rules below and by CV8.

- **Billet term** (specifies the "hold" and "cohesion" half of ENM
  11.1's M2 row; answers FL-21 (c), the doorway hold): while the group's
  formation holds, each candidate end square inside the hold squares
  gains, once, `billet%` × `billetBase` × `billetScale`. **`billetBase`**
  is a constant per unit, frozen at composition: its best at-will
  expected damage against a reference AC for its floor *(illustr.)*, so
  the term is live before any party unit is noticed or reachable (lead's
  call, after the check). `billetScale` is frozen per group (6.3: 0 for a
  solo hero on F2–5, 500‰ on F1, else 1). Weights by profile
  *(illustr.)*: Holder 40, Commander 30, Kiter 25, Raider 15 (until its
  first attack, then 0), Charger 10; Mob, Automaton, Ambusher,
  Opportunist, Stalker 0; Protector 0 (uses `ward`); Sentinel 0 (keeps
  its Leash).
- **Invariant-7 guard:** if any candidate this turn attacks or deals
  damage, candidates without an action lose the Billet bonus. A unit
  with a reachable target never idles on its Billet; it attacks from it
  when it can.
- **Stung rule** (lead's call, after XF1 and XB-H5; definitions are
  lead's calls after the check): a unit's Billet weight is 0 for the
  rest of the fight once it has taken damage since its last turn from a
  source it cannot answer from its hold squares. **Melee positions**
  (Holder, Charger, Raider) are also Stung, permanently, when at the
  start of their turn no ally in the group can damage that source. A
  formation holds only while it can trade. This mirrors the approved
  Sentinel "Provoked" rule without reusing its name. Definitions:
  - **source** = the acting unit (an attacker, or a user of forced
    movement). Environment damage (traps, Kegs, hazards, Stir acts),
    condition ticks and friendly damage never sting;
  - **cannot answer** = no at-will of the unit has the source in range
    and line of sight from any of its hold squares (from its battle
    square when it has none). A source the group cannot see (Hidden or
    unnoticed) counts as "cannot answer" **without reading its square**
    (invariant 4);
  - evaluated **at the start of the unit's turn**, from board and group
    state only; the flag never clears.
- **Fall-back square:** a destination bonus (+60% of `billetBase`
  *(illustr.)*), applied on every turn until the unit reaches the square
  or the fight ends and never re-granted, when the formation breaks
  (Kiter, Commander),
  when a Kiter drops to ≤ 50% HP (ENM 17.3's "falls back at 50%" gains a
  destination), or when a Kiter or Commander starts its turn adjacent to
  a noticed party unit. Rules: ≥ 2 squares farther from the landing; off
  hazards and triggers; Covered where possible; it **prefers squares
  that lose line of sight to the landing** (a retreating shooter visibly
  opens a window, XF5); it has a CV8 flank square within 6 (CV8 extended
  to fall-back squares). **On F1 a fall-back square may be Covered** (a
  reading: `Floor1NoCoveredStarts` governs start sockets only; the RT
  owner to confirm). The F1 archer starts in the open and "retreats to
  the crates when you close in".
- **No Billet within 2 of a Void edge or lethal hazard** (lead's call,
  after XB-M3): shovers start near the push lane but never camp it.
- Invariants: reads only board and formation state (1); no hidden
  positions (4); never outranks the stupidity floors (7); the AI never
  reads whether the overlay is shown (10). Not a leash; the Leash stays
  with the Sentinel and bosses.
- **Rejected:** an "anti-trickle" wait rule for Mob and Charger units
  (XF1c). It would idle a unit with a reachable target against invariant
  7. Door-camping is answered instead by the Stung rule, the Kiter
  doorway lane (EP-8), the contact-round count of `maxRounds` (6.6.4)
  and the `DoorCampShare` gate (11.3).

**What placement tells the AI:** role and profile (from the type),
Billet, fall-back square, watched Portal, open zone (lurkers), break
state, and the group's leader and ward links. Nothing else; no scripts.

#### 6.6.4 Keystone and breaks (lead's call; change EP-7)

Each formation names a **Keystone** (the unit whose loss breaks it: the
choke soldier, the Pack alpha, the leader, the Kill Box's melee unit
nearest the watched lane). `keystoneRule` is `any` (the one Keystone) or
`last` (several Keystones, such as the Ritual's singers: it breaks when
the last falls).
It is shown as a chip on its plate, never on the model, so RC2
silhouettes stay clean.

A formation breaks at the first of these (timing and operands: lead's
calls, after the check):
1. **Keystone lost:** dead, Routed, Yielded, Cowed, or pushed off its
   hold squares by any forced movement (a Shove's push 1 is enough;
   before it has reached them, or when it has none, pushed off its
   battle square). Attack Shunt does not count: it never moves the unit
   (ABL).
2. **Leader falls** (ENM 14.3 T1).
3. **Line crossed:** a *noticed* party unit ends its turn adjacent to a
   back or overwatch unit (not ward), **from round 2**, while a front
   unit still stands. A Hidden Shade reaching the backline does not
   break it (invariant 4; a stealth reward).
4. **Demoralised** (ENM 14).
5. **A third of the group's members are down** (`ceil(members ÷ 3)`;
   down = dead, Routed, Cowed or Yielded; counted in members, not
   HP: an HP threshold on a public chip would reveal what ENM 13.5
   forbids, XK-H5).
6. **The end of its third contact round** (`formation.maxRounds`,
   *illustr.*; mandatory for the clock). A **contact round** is one of
   this group's rounds since it became Engaged in which at least one
   party unit was in line of sight of a member. When no party unit is
   visible for a whole round, there is no break: the approved
   lost-the-party route runs (Searching, then Watchful, then the Hedgehog,
   6.21), so stepping back out of sight does not walk the line into a
   prepared choke (lead's call, after the check).

**Timing and operands (lead's call, after the check):** breaks are
checked after every resolved action and before the next scoring;
"back", "overwatch" and "front" mean the placement position roles, not
the current squares; trigger 3 is checked at the end of each party
unit's turn from round 2, so a party unit already adjacent at the end of
round 1 and still there at the end of its next turn counts.

On a break: Billet weights 0 for the group for the rest of the fight;
each Kiter and Commander takes its one fall-back; the chip turns to
**"Line broken"** and the turn log records why: a UI and formation beat
only (the chip's frame splits and the formation outline flashes and
dissolves, one VFX; no unit plays a fear motion, RC3; RC8 lists no bark
for it; lead's call, after the check). Joiners never carry Billets.
**Start shapes** (Revel, Bout, the Heap, Patrol File) are start
arrangements, not holds: once the group is Engaged it takes its battle
formation (6.6.6, 6.7), whose Billets and breaks apply from then.

**Gates** *(illustr.)*: median `FormationBreakRound` 1.8–2.8, with
fights that end unbroken counted at their last round and the censored
share reported; median rounds the party spends engaging the line ≥ 1 (a
formation nobody engages is not a puzzle either).

#### 6.6.5 The Clearway (lead's call; EP-24; no rule change)

FL 11.2 (**settled**) puts nothing that blocks movement on the Clearway.
So **Holders start on Clearway-adjacent sockets with the choke inside
their hold squares**; the Billet pulls them in once the fight starts.
Sleepers lie **beside** the Clearway so every route passes within 3 of
one. No unit starts on a Clearway square.

#### 6.6.6 The formation catalogue

**Battle formations and start shapes (lead's call, after the check).** The
catalogue holds **19 entries**: **15 battle formations** and **4
start shapes**. The proposal (and the client's "stands as written" list)
called it "the 17-formation catalogue"; the count was wrong, the
entries are unchanged (told plainly, 19.1). A battle formation gives
Billets, the Keystone and breaks from the moment its group is Engaged;
every group draws one at step 5b. A start shape gives only the start squares
of a Busy, Dormant or Afoot group; a Posted group's start shape is its battle
formation. `GroupPlacement` carries both (`startShapeId`, `formationId`;
6.13). Tiers rate battle formations only (6.2).

**Battle formations:**

| Formation | Templates; hosts | Tier, floors, wave | Core → growth | Keystone | Written counter (V12 proves the enum part) |
|---|---|---|---|---|---|
| **Picket Line** | Picket (patrol, guard post), Sleeper (lair) | FT1; all; **M1** | 1 front at the choke → 2nd front, flank minions | Front standard (the elite for a Sleeper) | Go round; Shove the holder; sneak |
| **Door Line** (the brief's "archers behind") | Line and Bow; Picket (front + marksman standard); guard post, lair | FT1; all; **M1** | Soldier/brute front + marksman back (overwatch F2+) → 2nd front | Front unit | `BacklineReach` lane; Shove the holder out |
| **Phalanx** (the brief's "shield wall") | Line and Bow (2 soldiers), Warband (2 front); guard post, lair | FT2; F2+; M2 | 2 soldiers on adjacent front sockets beside the choke (Interpose pair) → marksman or leader behind | Either soldier | A Shove-out square beside each; areas over the line; the flank lane |
| **Crescent** (the brief's "pack") | Pack, Den (mother as alpha); patrol, guard post, lair | FT1; all; **M1** | Alpha + 2 minions arcing toward the entry → 2 more minions | Alpha | Back to a wall or choke; kill the alpha (T3); areas |
| **Skirmish Spread** | Skirmish Screen; patrol only | FT2; F2+; M2 | 2 skirmishers on flanks ≥ 2 apart → 3rd | — (breaks on first contact) | Reach and opportunity zones; close up |
| **Retinue** | Warband; guard post, lair, boss escort | FT2; F2+ (F1 boss escort only); M2 | Leader on ward + 1 front → 2nd front, minions inside the aura | Leader | `LeaderReach`; Frighten or kill the leader |
| **Honour Ring** | Honour Guard; lair | FT2; F2+; M2 | Challenger adjacent to its ward → minions | Ward | Draw the Challenger; kill the ward; Challenge games |
| **Champion Watch** | Champion; elite room only | FT2; F1 second half+; M2 | Elite + watcher on a watch socket | Watcher | Draw the watcher off (Lure), then Steal |
| **Crossfire** | Battery; guard post | FT3; F3+, party ≥ 3; M2 | 2 marksmen on separate overwatch sockets crossing the room's middle at 5–8 + soldier on the lane | Soldier | CV8 flank squares; a Tall piece within 3 of the landing that blocks at least one line; areas ignore cover |
| **Kill Box** (the brief's "kill box") | Line and Bow, Picket, Warband, Battery; Waylay guard post only (6.8) | FT2; F2+, party ≥ 2; M2 | Shooter(s) on blind/overwatch facing the watched Portal + the melee unit nearest the watched lane on a blind flank → more melee on blind flanks | The melee unit nearest the watched lane | Listen; the other way in (with its cost); Lure; Surprise it |
| **Pincer** | Warband, Pack; guard post, lair | FT3; F3+, party ≥ 3; M2 | Brute front + 2 on opposite flanks | Brute | One wing at a time (text only); choke back at the door |
| **Grindstone** | Gauntlet; lair only | FT3; F3+; M2 | Hinderer on ward + 2 brutes either side of its sightline | Hinderer | Focus the hinderer (`BacklineReach`) |
| **Scramble** | Scavengers; patrol (its contact shape) | FT1; all; M2 | Loose cluster, a non-door `fleeExit` within 6 | — | Cow it (Herald `cowableGroup`); Frighten |
| **Bait and Nook** | Ambush; Cuckoo or Baited Room guard post only | FT2; F1 (Cuckoo, 6.17)+; M2 | Bait in plain sight + lurkers on nooks | — | Mark the tell; shoot the bait; strike the marked lurker first |
| **Ritual** (the brief's "ritual") | Warband; the Choir only, a Tide WetEdge host | FT3; F3; **M3** | Leader (the Deacon) + 1 front + 2–3 `singer` minions round the WetEdge host | The singers (`keystoneRule: last`) | 6.6.7 |

**Start shapes:**

| Start shape | Groups | Wave | Start arrangement | Then |
|---|---|---|---|---|
| **Patrol File** | Any Afoot (patrolling) group | **M1** | Front, leader, marksmen, skirmishers, swarmers last (6.9) | Its battle formation on contact |
| **Revel** (the brief's "feast") | Any Busy group whose template is Stationary-capable; "gorged Revel" for a Dormant Pack or Sleeper | M2 | Members on table sockets round a Low piece or Furnishing; marksmen *not* on cover | Surprise it; areas on the clump |
| **Bout** | Busy Picket, Line and Bow or Warband | M2 | 2 members shadow-sparring mid-room (same type, or a solo loop facing each other with no contact; 8), the rest watching | Surprise; split the pair from the watchers |
| **The Heap** (the brief's "sleeping ward") | Any Dormant group; the Annex | M2 | Sleepers beside the Clearway; mother or elite deepest | Sneak line (two band rolls at Dormant −5); Steal; silent kill; areas on the cluster |

**Counters V12 can prove (lead's call, after the check).** Each battle
formation's `counter.guarantees[]` is drawn from a closed predicate
enum: `BacklineReach`, `LeaderReach`, `ShoveOutSquare` (a square from
which a push 1 moves the Keystone outside its hold squares),
`SecondPortal`, `FlankPair`, `TallWithin3OfLanding`, `CV8FlankSquare`.
`CounterPresent` checks only the enum; text-only counters ("one wing at
a time", "areas") are marked unproven in the record.

**Defensive variant** (every battle formation; used only by the
Hedgehog, 6.21).

**A Waylay always uses the Kill Box** **(new lead's call:** the
proposal's Archive and Supper catalogue entries named a Battery Waylay
"Crossfire"; Crossfire is the non-Waylay FT3 formation**)**. A Battery
Kill Box seats its two marksmen on separate blind or overwatch sockets
whose lines cross the watched lane, so it reads as a crossfire while
keeping every Kill Box rule (6.8.6).

#### 6.6.7 Ritual: the singers hold back the Tide (lead's call, after XK1; change EP-27; M3)

The HZ rule (**settled**): The Hymn Must Never Stop's WetEdge sockets
**rise one round after the last `singer` falls**, framed as a reward
option for lightning parties. So the Ritual is a *choice*, not an
"interrupt": singers keep the room dry; silencing them floods it.
Lightning parties want the flood (Charged Water); others leave one singer
standing and fight round it. It is a **Choir-only** formation on a Tide
WetEdge host. Any other theme needs a new HZ singer entry (a request to
the HZ owner, Later). The Ritual is a Warband (the Deacon leads; the
singers are Chorister minions), so it needs no new ENM 17.4 template row
(lead's call, after the check). **Its sound is a rule signal (M3; lead's
call, after the check):** the singers' chant uses a reserved timbre
outside the Choir bed (a single sustained drone voice), the bed's choral
layer ducks in a Ritual room, and the "last singer falls" sting joins
the RC7 reserved cues, so the party can hear whether the ritual still
runs.

#### 6.6.8 Terrain Pairings (lead's call; EP-21)

(Renamed from the drafts' "Set Pieces": "set piece" is a TL term.) A
deterministic pass after seating, within approved caps, **at most 2
pairings per encounter and at most 1 that aims environment damage at the
party** *(illustr.)*:
- marksmen on Low-cover overwatch from F2 (CV8 flank square
  guaranteed);
- Holders on Clearway-adjacent choke sockets;
- shovers on front sockets with a VP4 push lane, within VP3 and the
  shover caps, never with a Billet within 2 of the edge;
- the Fire-Eater within 4 of a slick, Kegs ≥ 2 from starts **and** from
  the entry lane (KP5);
- Choir marksmen on a dry island; the Gall Slick on the gall edge;
- **M3 / pending:** `usesCords` types by their Cord; Stoker types by
  their vent (HZ-24); the TR-15 terrain-user by its Furnishing; natives
  in known device areas after TL-16 (≤ ½ a group, never on triggers,
  never on F1, and never adjacent to a shove lane into a *Found*
  device area, since Found-only scoring would make that a legal
  repeatable combo, XB-M3).

The formation name carries the pairing in the Read card ("Door Line at
the Ledge"). Geometry modifiers drawn from the host are the main defence
against solved formations (XF11).

### 6.7 Activities (lead's call; change EP-9; agreed, Q4)

Each non-boss, non-joiner, non-lurker group draws one **activity** from
its theme's table, filtered by its legal state set (6.4; V12 asserts it),
room pieces and animation readiness (below). Each maps 1:1 onto an
approved state and a **posture**, and names a **start shape**. **One rule
(lead's call, after the check):** the activity sets only the start
shape and the presentation; the group's **battle formation** (step 5b)
sets the Billets, the Keystone, the breaks and the board chip, and is
what the no-repeat and ≤ 2-per-floor counts read. A Busy Retinue is a
Retinue standing in a Revel start shape.

| Posture | State | Start shape | Examples |
|---|---|---|---|
| **Posted** | Stationary | = the battle formation | on guard, roll call, waiting (Waylay) |
| **Busy** | Stationary | Revel or Bout: clumped, facing the piece; Billets at battle squares | dicing, feasting, sparring, praying, cataloguing, rehearsal |
| **Dormant** | Dormant | the Heap / gorged Revel | sleeping, digesting, vespers, vigil |
| **Afoot** | Patrolling | Patrol File (Scavengers take Scramble on contact) | rounds, the hunt, sleepwalking |

- **Busy groups form up.** On notice they move to their Billets by
  normal movement over 1–2 turns (no free reposition; no "scamper").
  Catching a group Busy is the scouting reward: a bad shape, not a lost
  turn. The toy shows Busy start shapes give back the formation premium
  (0.157 vs naive 0.161 HP-eq).
- **No noticing modifier in M2** (contested by the lead). A "Busy
  −2" option would change ENM 12.2, blur the Dormant −5 / Watchful +3
  contrast, and press on the Shade's stealth share; it is a Later lever,
  only with `PartySurpriseShare` headroom.
- **Not legal:** Busy for the Champion, Waylay, Honour Ring, Boss Court,
  all-mindless groups (6.4) and Watchful groups.
- **F2+ clump cap:** at most 2 Busy members inside any radius-1 area (the
  Revel-plus-Surprise-plus-area opener is the largest single burst in
  the design; XB-M1, XF13). F1 keeps clusters for the Arcanist
  `twoEnemiesWithinRadius1` seed target.
- **Presentation:** an idle animation that duplicates the state glyph
  (RC3); an idle bark (RC8 allows idle); **units animate, props do not**
  (RC1 reserves idle motion on containers and Furnishings for lurker
  tells; dice lie still on the Low desk); trained Listen adds an
  activity word ("dice", "snoring", "chanting") beside the size band (a
  request to FL 9.2 / ABL 7.2 and segment 10).
- **Animation readiness (lead's call, after the check):** each theme
  activity carries `animReady`; V12 `ActivityHasAnimation` makes an
  activity drawable only if its loop exists for every type its group
  can contain, else the group is Posted. Each F2–5 theme ships 1–2
  bespoke M2 activities (at least one), so the Listen word never names
  a loop the screen does not play (8).
- **Activity and tell audio (lead's call, after the check):** positional,
  and played only while the source is in a party unit's line of sight
  (Dormant breathing included). Through a door only the Listen result
  speaks, so sound never gives away what trained Listen buys. An audible
  lurker tell (the Late Uncle's cane) joins the RC7 reserved-cue list in
  a timbre family no music bed uses, or the tell stays silent.
- **Per-group awareness** (change EP-9; **agreed, Q4**): MAIN 4.3
  (**decided**) says awareness is "set per monster in data". This
  design reads it as a type-level `allowedAwareness[]` that the
  template's Awareness column and then the group's activity choose
  within. The same Huntsman can be on guard in one room and asleep in
  another. Rejected: B, one state per type.
- **F1 themes get ≥ 3 activities each in M2** (not M3), because F1
  repeats in every run (XF10).

### 6.8 Ambushes (lead's answer, CQ1; changes EP-2, EP-3, EP-10 to EP-14)

#### 6.8.1 Definition and forms (change EP-10 to ENM 10.3, 10.1, 16, 17.4)

| Form | Composition | Party Surprised? | Band | Floors, party | Cap |
|---|---|---|---|---|---|
| **Cuckoo** | Standard bait + 1–2 lurkers (ENM 17.4) | **No** (per-side reading) | **Full guard band** (the 85% priced a lost turn that no longer happens; at 85% it was an XP bargain, toy 0.166 vs control 0.179, XB-H3) | F1 solo (6.17 only); F2–5, party ≥ 2 (lead's call, after the check) | Counts |
| **Baited Room** | **Bait cache** (a reserved container prop family, distinct from ordinary loot containers) + 1 lurker; 2 lurkers from F3 at party ≥ 3 (lead's call, after the check) | **Yes** (the only placement that can) | `FloorDiv(guard × 85, 100)` (approved 85%) | F2–5, party ≥ 2; never F1, never solo | ≤ 1 per floor, ≤ 2 per run *(illustr.)*; counts |
| **Boxed lurker** (TL 6.14, **agreed**, M3) | The container is the lurker | **Never** (no row of its touch table Surprises the party; a marked lurker attacked is itself Surprised) | The Annex (6.13) | F2–5 | ≤ 1 per floor (approved); counts first |
| **Waylay** | An ordinary Stationary group in the Kill Box | Never (mutual notice) | Full band | F2–5, party ≥ 2 | Counts |

The ENM 10.1 sentence "an ambush uses 85% of its band" and the ENM 16
sentence "ambushes pay their band's value even though filled at 85%" are
re-worded to "a **Baited Room** Ambush", and the ENM 17.4 Ambush row's
Band "Guard post × 0.85" reads "guard post; × 0.85 for a Baited Room" and
its Awareness "Hidden" reads "the lurker group Stationary and Hidden at
start; the bait group Stationary" (change EP-10; the 17.4 cells: lead's
call, after the check). Container bait is
already in the approved M2 Ambush row; restricting it to party ≥ 2 and
capping it per run is a tightening of an M2 row, flagged against ENM
17.4 (XK-M8). The reserved **bait cache** prop keeps ordinary chests
honest, so looting does not become paranoid (XF9). **Bait-cache rules
(lead's call, after the check):** a bait cache never moves at idle; it
never uses a silhouette family used by that theme's boxed-lurker hosts
or loot containers; it carries one cross-theme bait mark registered in
RC5+; all three are V7 `CharterChecks`. **Generalised (change LP-15;
M2):** the no-shared-silhouette rule now covers every container family
under CT1 `ContainerFamilyDistinct` (with V7), with the role-ID gate row
"container or bait" (loot sources design 5.1). **Bait-cache contents (change
LE-20; M2):** a real ordinary entry at SR2 ("the cheese is real"), so a
Baited Room lure is worth taking; bait value is party-size dependent and
reported (loot economy design 6.9 item 6). **New lead's call:**
an ambush form with no legal fill in its window at the floor's weight is
simply not drawn on that floor (the ENM 10.3 cap is a maximum, never a
quota). **New lead's call:**
the Baited Room ships in M2 with the Cuckoo (both after DH 21.1 step 1,
12); the proposal's Archive entry called its bait-cache reading tray an
"M3 variant", which is corrected to M2 (7.6; the loot sources design
reads the bait cache and its placement as M2, 13). **Pending request
(LP-16; M2):** the loot sources design asks the EP owner to re-skin two
bait caches, the Archive one as an ink-lacquered document case (not a
reading tray: RC2 tray ban, a second Archive tray) and the Gut one as a
tenant's sewing basket (not a third strongbox); 7.6 and 7.9 stand until
the EP owner rules (loot sources design 5.3).

**Host rules** (the Cuckoo, Baited Room and Waylay): a guard-post host;
never Gb (the Bypass stays a stealth choice, not a trap), the Forecourt,
the elite room, a Q room, the Sanctum, or the floor's first fight host;
never in a Void or clock room (an out-of-sight start plus a push lane is
an untelegraphed kill, XB-M3); one ambush form per room; never shared
with a rigged device, a Tattle or Crier target, or a near pair; no
Quarry declarer in any ambush encounter on F1–3 (approved; extended to
the Waylay). **The boxed lurker is exempt:** it stands on its TL 6.14
container socket under TL's placement rules, as the Annex (6.13).

**What the hosts allow (told plainly, after the check).** With 3 guard
posts (2 trimmed), one the first fight host and one Gb, most floors have
**one** legal ambush host, and trimmed F3 and F4 (E G Gb Q L Q F B, E G
Gb Q L F B) have **none**. Only a floor whose Gb is not its second guard
post can have two; in the canonical orders that is untrimmed F5 with the
Bypass round the first G. So the generated forms reach the ENM 10.3 cap
of 2 only there; on F3–5 the second place under the cap is the M3 boxed
lurker's. The host rules stay: they are what keeps ambushes off the
first fight, the Bypass and the lair chain.

**Frequency** *(illustr.; generated forms; the boxed lurker adds up to
1 from F2 in M3)*:

| Floor | Cap (settled) | Cuckoo | Baited Room | Waylay | Mean |
|---|---|---|---|---|---|
| F1 | ≤ 1, index ≥ 3 | Wake 500‰, Kennel 300‰, Gaol 250‰ (L-G order only) | 0 | 0 | 0.3 |
| F2 | ≤ 1 | Archive only (where a fill exists) | ≤ 200‰ where a fill exists (rarely, 7.6) | ≤ 400‰ (the F2 Kill Box forms of 7.5–7.7) | 0.4 |
| F3 | ≤ 2 (≤ 1 generated; none trimmed) | Archive | ≤ 1 (Archive; 2 lurkers at party ≥ 3) | ≤ 1 | 0.5 |
| F4–5 | ≤ 2 (≤ 1 generated, 2 on untrimmed F5; none on trimmed F4) | the Gut only, on F4; none on F5 | ≤ 1 (the Gut, F4) | ≤ 1 (≤ 2 on untrimmed F5) | 0.6 |

#### 6.8.2 Party surprise is read per side (change EP-2; agreed, Q1)

At combat start **the party is Surprised only if no party unit has
noticed any hostile unit taking part in that combat.** Monster groups
keep the per-group rule (ENM 12.2). This clarifies decided MAIN 4.3
(which already says "side"), settled ENM 12.2 (which says "group") and
agreed DH 17.1 (which already uses side language: "the party side is
treated as having noticed").

**How it is computed (lead's call, after the check):**
- the party keeps **`partyNoticed`**, a set of unit ids, per floor:
  a unit joins it when a party unit has line of sight to it while it is
  not Hidden, or on a successful band check against it; it never leaves
  within the floor. (ENM keeps awareness records only for monster
  groups; this is the party-side record.)
- **the combat at start** = every group with a member that attacks, is
  attacked or notices in the triggering action, plus every group sharing
  an `encounterId` with one of them (so a Cuckoo's bait group takes part
  when its lurker strikes). Groups merely in sight, and party members in
  another room, do not change it;
- **order:** (1) build that membership; (2) the party is Surprised iff
  `partyNoticed` holds no member and the DH 17.1 exemption does not
  apply; (3) each monster group is Surprised iff the party noticed it
  and it is unaware (ENM 12.2); (4) at most one surprise opener per side
  is applied, by trigger order (6.8.7).
- **fixed at declaration:** membership and Surprised are settled when
  the triggering action is declared. Its result never re-picks either;
  a kill only removes the killed unit. The full combat-start order is
  M1-10 rulings section 5. **(agreed 2026-11-27, roadmap D-19; M1-10 rulings 4.1)**

Consequences, told plainly:
- With a standard bait in sight, a Cuckoo never costs the party its
  turn; the lurker keeps its Hidden opener.
- Only Baited Rooms take turns. A **marked tell does not count as
  "noticed a hostile"**: only a successful band roll or seeing a unit
  does.
- The DH 21.1 step-1 reconciliation re-runs the 10.2 examples on this
  reading; the "before the hero's first turn" rows then bind only fights
  containing Hidden units. **No lurker ambush ships before DH 21.1 step
  1 passes.**
- In the toy the per-side reading removed about a third of the ambush
  death excess (4.6% vs 5.8% per ambush fight; control 2.4%). The toy's
  solo F2–5 arm placed Cuckoos, which this design now bars (6.3); solo
  ambush exposure is F1 only (corrected after the check).
- Rejected (Q1, B): the per-group reading, under which every Cuckoo can
  take the party's turn, Cuckoos return to 85% fill and F2 Cuckoos drop
  to about 70% of the guard band *(illustr.)* to pass the 6.4 round-1
  row.

#### 6.8.3 Lurker geometry (lead's call; EP-8)

- **`LurkBlind`:** a lurk socket has no line of sight to the landing or
  `partyEntrySquares`. With symmetric line of sight, the lurker's first
  noticing trigger is therefore the 6-square or 3-square band, never
  "first sight from the door".
- **Open zone:** the squares within 6 of the lurker that it can see. The
  party gets passive Perception against the lurker's Stealth at the 6
  band and again, with a fresh roll, at the 3 band (6.8.5): **two
  chances before the strike**. **The lurker springs only when a party
  unit is within 3 squares and in its line of sight**, or when its group
  or the bait is attacked or notices; the 6-square band is a warning
  check only (lead's call, after the check). E3 and `AmbushFiredRate`
  are re-run on this reading.
- **`AmbushStandoff`:** the open zone contains no `partyEntrySquares`
  square; lurk sockets at path ≥ 3 (Cuckoo) or ≥ 4 (Baited Room) from them
  *(illustr.)*.
- **`TellSight`:** the tell is within 6 of the lurker (approved) and in
  line of sight of the landing; on F1, visible from the doorway at ≥ 7.
- **KP5 defined:** no Keg, contact hazard or trigger adjacent to the
  entry lane or the open zone.

#### 6.8.4 Detection and marking (lead's call, after XK-H3)

**Trained Perception marks the tell's square on the map** (ENM 17.4,
**settled**: "trained Perception marks *it*", and "it" is the tell).
Noticing the lurker itself stays the 12.2 passive check at the bands.
The toy had modelled the mark as an automatic spot and reported "lurker
ambushes void against trained parties"; that was a misread of the
approved text, so the drafts' open client question is withdrawn (told
plainly). The E3 arm is re-run on the correct reading (11.3). Also
report `AmbushFiredRate` (lurker openers that actually landed, trained
vs untrained): < 10% trained means invisible content; > 50% untrained
means an unreadable tell (XF4).

**The spring as a beat** (an art ask): a reveal animation, a camera
nudge and a log line ("The Penitent rises from the cot"), for both the
lurker's strike and the party's counter-strike.

#### 6.8.5 Lurker placement Stealth (change EP-3)

ENM 12.2 (**settled**) rolls lurker Stealth "once at placement, and
again only at the 6- and 3-square bands": three rolls. Each is a keyed
hash, **one per roll** (lead's call, after the check, correcting the
proposal's single hash, which made the 3-band check repeat the 6-band
result): `H(dungeon/floor/{N}/checks, "h:lurkStealth", groupId,
positionIndex, band, die)` with band ∈ {placement, 6, 3} (the keyed
form of the seeds and validation design 4.1, 4.3; changes SV-1, SV-6:
the floor key's attempt-invariant `checks` child, the `h:` domain
prefix and the `die` index added; M2, registered as a reserved, inert
domain in M1). The d20 is `1 + Bound(d, 20)` (multiply-shift, not
`mod 20`); advantage uses die 0 and 1 as in TL 6.22. The placement
value is the standing Stealth until
the first band. **At a band:** the trigger is the nearest party unit
entering it; the value is compared against the highest passive
Perception among **non-Downed** party units with line of sight to the
lurker (change XI-2 to ENM 12.2's lurker band, approved 2026-11-23; M1;
Downed units contribute no knowledge; exploration and information design
4.1.2); with none in line of sight, that band is spent (the mirror of ENM 12.2's ignored
band trigger). `unitId` = (groupId, position index), so it is stable
under fallback re-placement. The hashes are replay-stable and
independent of draw order. They amend ENM 12.2's "lurker Stealth … use
the combat stream" (lead's call) and ENM 10.5 step 10; both rows are
named in 17 (XK-H6), and the three rolls are kept. Model B's
"first-entry roll on the combat stream" was rejected because it changes
"roll once at placement" as well.

#### 6.8.6 Waylay (change EP-12)

- **Templates:** Stationary templates only (Line and Bow, Picket,
  Warband, Battery); minded members only (RC3); ≥ 1 ranged or skirmisher
  member; no elite; a leader may anchor it. Never Skirmish Screen (its
  Awareness is Patrol, XK3).
- **Host:** a guard-post host on F2–5 with ≥ 2 Tall pieces, Drapes or
  nooks on the enemy half; **the watched Portal's approach side is a Door
  that starts closed** (Listen works only at a closed door, FL 9.2;
  XK-M4).
- **Geometry gate** (from the toy, which found Waylays *weaker* than
  control fights, 0.150–0.162 vs 0.179 HP-eq): 1–2 units with an attack
  reaching the watched lane (the entry lane of `watchedPortal`, 6.6.1)
  at 4–8; melee within 3 of the lane; all units out of sight of
  `partyEntrySquares`, ≥ 7 from the mouth (LG20), ≥ 3 path from the
  landing. **HP-eq must sit between control +5% and control +15%** at
  every party size *(illustr.)*, a simulator gate per template ×
  formation × floor band (11.2). A Waylay easier than a guard post is
  worse than none (XF3, XB-H3).
- **`CounterPresent` and `CounterCost`** (lead's call, after XF3): the
  unwatched way in must exist (a second Portal or a flank pair) **and**
  cost at least one named thing: a path ≥ 6 squares longer, a Squeeze, a
  known trap, a hazard edge, or passing the sightline of another group's
  start zone (outside `HearingGap`, so no chain pull). "A known trap"
  means a trap at Hunch or Found grade at generation; `CounterCost`
  is checked at generation time (11.2). The Waylay becomes "pay the toll
  or take the crossfire".
- **Tells:** the **Spoor mark** on the approach side of the watched
  Portal. **One cross-theme base mark** (a three-stroke tally,
  registered in the RC5+ pattern registry apart from the trap-tell,
  lock and Hunch families; lead's call, after the check), skinned per
  theme in material only: barnacle scrape on the Choir door frame (not
  water), ink smudges on the Archive jamb, sawdust in the Carnival lintel,
  a wine stain on the Supper door, a flesh-tone bruise on the Gut
  sphincter rim (not bile green), a crayon line on the Heart door. It
  is drawn on **both jambs and the lintel, at or above head height**,
  and must read at all 4 camera rotations (V7 check). **Present if and
  only if a Waylay lies beyond**, so never a decoy (RC9). It is never on
  floor squares (RC1 places a material only on its own squares; RC1+
  keeps dressing off walkable squares; XK6). **Pending (XI-18; M2):**
  when its wall is cut away or faded, the Spoor mark projects a
  front-square half-rim decal, readable at all 4 rotations, as a
  projection of a wall mark, not a floor mark (LP Rathole precedent);
  the EP owner confirms (exploration and information design 4.9.7).
  Listen reports "still";
  **trained Listen adds which Portal is watched** (not just "waiting"),
  so Perception training buys a better read, not the same one (XF3d).
- **The right read pays visibly (lead's call, after the check):** when
  the first party unit the group notices stands closer by path to a
  non-watched Portal than to `watchedPortal`, the Kill Box **starts
  broken** (6.6.4: Billet weights 0, the Kiters' fall-backs granted) and
  the chip reads **"Facing the door"**. Facing itself is
  presentation only (6.6.3).
- **`FirstSight` exemption**; `LandingShooters` measured on the entry
  lane (≤ 1 on F2, ≤ 2 on F3–5); `StartArc` 120° at party 2.
- **It can be Surprised:** it is Stationary, so in the Cutthroat's
  unaware set.

#### 6.8.7 Counter-ambush (the party's answers)

1. Mark the tell; strike the marked lurker while its group is unaware
   (it is Surprised, core rule).
2. Shoot the bait from outside the open zone.
3. Lead with the best Perception member (party maximum), remembering
   that stealth is the worst member (CMP 7.8).
4. Listen at Waylay doors; read the Spoor mark; take the costed way
   round.
5. Lure (SH-21, pending; once per group per floor) a holder off its
   Billet.
6. Hide in: Waylay groups and Cuckoo bait are Stationary.
7. Hold the Hide on the patrol (6.9).
8. The trap opener (TL-16, pending). "Never stacks with Ambush" is read
   as **at most one surprise opener per side per combat** (a reading for
   the TL owner).

#### 6.8.8 The symmetric surprise table (for DH 21.1 and TL-16)

| Opener | Who can be Surprised | Notes |
|---|---|---|
| Party Hide, sneak, Shade, Lure on a Dormant, Stationary or Patrolling group | That group | Includes Waylays and Cuckoo bait |
| Party trap spring on an unaware group (TL-16) | That group | The springer's surprise action; one opener per side |
| Party attacks a marked, unaware lurker | The lurker's group | Core rule; the boxed lurker per its table |
| Baited Room: the party noticed no hostile | **The party** | F2–5, party ≥ 2, capped |
| Cuckoo: the party saw the bait | Nobody on the party side | The lurker's Hidden opener only |
| Waylay; boxed lurker unmarked | Nobody (party side) | Positional / per TL 6.14 |
| Joiners; Crier and alarm arrivals | Never | ENM 12.2; TL-30 pending |
| Boss Court; Watchful groups | Cannot be Surprised | Approved |

**Binding metric:** `PartySurpriseShare` (party-initiated surprise
fights over all fights), measured for the worst-legal Shade-in-party
profile, fed to the Shade's `stealthShare[L]` / `openShare[L]` tables;
parity is re-run if it exceeds the Shade 10 profile (35% solo, 20%
party). It is not itself a placement cap (XK-H7 corrected the drafts'
"OpeningCeiling" name). `SurpriseRatio` is a health report only. Add
`AmbushTurnLossByPartySize`: no party size carries more than 50% of all
party-Surprise rounds *(illustr.)*.

### 6.9 Patrols and the Circuit (lead's call; EP-17, EP-18)

- **Patrol File** order (draw-free): soldier or brute, leader,
  marksmen, skirmishers, swarmers last. The route is drawn once seen and
  feeds the rest preview (**settled**). **Display (change XI-6; agreed,
  Q2; M2):** the route is drawn on known squares only, with "continues"
  ends, and every consumer reads `PatrolForecast` over known patrols
  (exploration and information design 4.8.1).
- **Cover-room window** (EP-17): the Circuit `coverRoom` holds ≥ 2 KP4
  Hide spots with line of sight to a lane square within 4, out of the
  lane's sight from the previous waypoint. On F2–5 the window is in a
  Circuit side room, never the Forecourt (its approach stays calm). V12
  checks it; segment 6 cannot construct it, so it is a **request to FL
  6.7 and RT KP4**, with a failure action: the next Circuit side room; if
  none qualifies, the floor is logged and the patrol template is a
  Picket (XK-M5).
- **Hold the Hide** (a new exploration verb; a new ABL 7.2 row and an FL
  6.10 auto-walk entry): legal when every party unit is Hidden or out of
  sight of the drawn route, inside a room the route enters. Ticks pass
  with no clicks until a patrol unit enters a party unit's line of sight,
  any noticing roll, any group turns Alerted, or 20 ticks *(illustr.)*.
  Noticing is normal; if the patrol fails, the party's first attack
  Surprises it. **Once per floor, in the cover-room window only**
  (XB-M1). Ticks count toward the clock, not as a rest.
- **F1:** Scavengers in Patrol File (Scramble on contact) on the
  Circuit through Q room 3 (CL-10, **settled**); clear of rooms 0–2 for
  20 ticks (**settled**).
- **F2–5:** the Forecourt patrol (homed and paid at n − 2, **settled**)
  is never Kill Box, Crossfire or an elite group, because it may join the
  boss fight (FL-21 (e), pending).

### 6.10 Guard posts and lairs (lead's call)

- **Guard posts:** chokepoint hosts (FL 6.5). Battle formations by tier:
  Picket Line, Door Line, Crescent (a Pack), Phalanx, Retinue, Pincer,
  Crossfire, Bait and Nook, Kill Box. The watched Portal is the
  main-path entry. **Stationary**, or **Dormant** as a Pack (any floor)
  or a Warband (from F2). **Lead's calls, after the check:** Skirmish
  Spread and Grindstone are not guard-post formations (the Skirmish
  Screen is Patrolling-only, so segment 6 never seats it at a guard
  post, a narrowing of use, not of ENM 17.4; the Gauntlet's band is the
  lair); a Pack guard post may be Dormant, as ENM 17.4 allows.
- **Lair:** the cut vertex past the midpoint; the highest tier legal for
  its template and composition (6.2). Den, Sleeper and Dormant Packs,
  Gauntlets and Warbands use the **Heap sneak line** (EP-23;
  answers FL-21 (d)): sleepers
  beside the lair's Clearway so every route through passes within 3 of
  ≥ 1 sleeper (two band rolls at Dormant −5). Report `LairSneakRate`; if
  > 25% *(illustr.)*, the enemies owner re-reads ENM 20.1 as "one guard
  post or one Dormant lair" skippable.
- **Elite room:** Champion Watch, Stationary and awake, ≥ ½ of the start
  zone visible through the Grille at ≥ 7 (**settled**). The watcher's
  hold squares include the elite's adjacent squares, so after an Alerted
  return it re-takes its watch next turn; watch the < 15% "stolen
  without a fight" target (XB-L1). FL-21 (b), the Champion hold: the
  elite's Billet is offered as the hold if the enemies owner accepts
  EP-6.

### 6.11 The avoidable fight (Gb) (lead's call)

Stationary or Dormant; FT1–FT2; **never an ambush form** (C's graft);
its formation faces the main-path entry, so the Bypass corridor really
is the stealth route (FL 6.8). Fill on the normal curve (6.2). Always
counted in `StealthOpenable`. Its XP stays in the pool (`SkippablePool`,
**settled**). Never Scavengers (XK-Low).

### 6.12 Joiners and reinforcements (lead's call, told plainly; EP-22)

- **The approved model is unchanged:** joining by hearing only (MAIN
  4.3, **decided**); caps 1/1/2/2/2; footsteps ≥ 1 round ahead (2 with
  trained Perception); "groups beyond the cap do not change state".
- **No "join chance" and no `JoinBudget`** (told plainly, XK5): joining
  is deterministic hearing, so the toy's 0.45 join chance was a
  modelling stand-in, and its over-target `ChainPullRate` (0.65–1.38 on
  F2–5, target 0.3–0.6) must be fixed with levers segment 6 owns (FL 13:
  "the reinforcement and Dormant partners for near pairs"):
  1. **Dormant partners** (hear at half radius: combat noise 8 becomes
     4; each closed door −3), applied in the floor allocation (9b) only
     where the partner's legal state set allows Dormant; otherwise the
     partner stays Posted and carries no call source;
  2. partners at the far end (10) of the 9–10 band, behind a closed door
     where FL allows: a **request to FL** (LS12), not a segment-6 action
     (segment 6 moves groups only within step 9);
  3. **no near pair on Waylay, Ambush or FT3 hosts**, so a showpiece
     stays one puzzle (XF7);
  4. only if that misses: a **request to the FL owner** to drop the
     Archive/Carnival +1 on F2–3 (their floors; cross-document comparison
     2026-11-23, CC-89) or the F3–5 near-pair cap (FL 6.6).
  `JoinBudget` remains available only as a client change to MAIN 4.3
  (not proposed).
- **Partner rules (the definition is a lead's call, after the check):**
  the **partner** of an FL near pair is its higher-`mainPathIndex` host
  (lever 3 keeps near pairs off ambush and FT3 hosts). Never a
  lurker; never an elite on F1–2; preferably a Pack (it can be Dormant).
  Scavengers are preferred only for a patrol partner (their only state is
  Patrolling).
- **Assertions:** `JoinedRangedShare` (after a join the F1–2 ranged ½
  cap still holds); `JoinedUnitsCap` (host + joiner ≤ the 10.3 unit cap +
  3, the FL free-space assumption); **at most one placed call source per
  encounter** (a Horn-bearer, a Call Post, a Messenger target, a TP8
  noise trap in hearing).
- **`ArrivalThroughPortal`** (EP-16): every non-Hidden unit entering a
  room containing a party unit enters through a Portal after its
  footsteps (strengthens invariant 5).
- **Arrival-Portal arrows** on the hearing outline (segment 10): the
  party sees where a joiner would come in, and Receive and Bar the Way has
  a door to hold. The outline is labelled "known groups only" (XI-13, a
  wording reading; M2; exploration and information design 4.8.4).
- **No off-map reinforcements on any floor.** Boss reserves stay with
  segment 9 (ENM 17.5). **Reading (BR-11; M2; pending the EP owner's
  confirmation, with the enemies owner):** segment 9's reserve sockets
  keep this rule and `ArrivalThroughPortal`, because the (d) reserve
  units wait inert inside the arena from reveal and are already in the
  room; a socket is not a Portal (boss rooms design 4.5.2).

### 6.13 Hand-offs to traps and locks (lead's call; EP-25, EP-26)

**`GroupPlacement` record** to `trapsResolve` (fields added after the
check are lead's calls, after the check):
```
{ groupId, encounterId, linkedGroupIds[],   // the Ambush bait and lurker groups, the Champion
  slot, homeRoom, hostRoom, templateId,
  formationId,                              // the battle formation: Billets, Keystone, breaks
  startShapeId,                             // Revel, Bout, the Heap, Patrol File, or = formationId
  activityId, posture, state, watchedPortal?, openZone[]?,
  ambushForm?: cuckoo|mousetrap|waylay|boxed, tellSquare?, spoorPortalId?,
  pairings[], defensiveVariantId, billetScale,
  nearPairPartnerGroupId?, callSourceUnitId?,
  units[{ unitId,                           // (groupId, position index)
          typeId, rank, role, pos, socketId, socketClass, startSquare, facing,
          billet{holdSquares[], fallBackSquare?}, keystone: bool,
          leaderId?, wardOf?, inDeviceArea: bool, hidden: bool, lurkStealthKey? }],
  keybearerCandidates[]  // leader > soldier > marksman > other non-minion, by path to the lock;
                         // never a ward behind two fronts on F1–2, never a nook
  tattleBound?, annex?, rigged?, trapChargeCost, captiveSocket? (reserved) }
```

- **TP2 constructive:** trigger squares are removed before 10d, so
  `trapsResolve`'s re-place request becomes an assertion that should
  never fire; the approved fallback stays as a backstop (TL 6.20, read).
  **If it fires (lead's call, after the check):** segment 6 answers
  draw-free by re-running 10d for that group with the trigger squares
  excluded, then re-running that group's V12 checks; if that fails, TL
  drops the trap.
- **Keybearer (one owner: TL; lead's call, after the check):** segment 6
  writes only the ordered `keybearerCandidates[]`, and `trapsResolve`
  binds, as TL 6.22 says. Segment 6 guarantees a candidate: for each
  lock, the last slot within 2 rooms of it (in the fixed slot order of
  9b) admits at step 5 only templates with a Keybearer-capable role,
  unless an earlier slot within 2 rooms already holds one, and the Gaol
  Turnkey stands within 1 room of its lock and of the keyring cache (FL 7.2). `KeyReachable`'s fallback
  stays.
- **Tattle:** binds to the floor's **Annex** (F2–5). TL's "a Dormant
  group beyond" stays as written, but no slot host ever lies beyond a
  Tattle (ET9). Otherwise it resolves to a Crier (approved). TP10 and the
  Wake `nearStart` bind. **Told plainly:** every Tattle on F1 is a Crier
  (no Annex on F1), including the Wake's Viewing Door; and **every
  Tattle on every floor is a Crier until the Annex ships in M3**,
  including the Heart's Nursery door (after the check).
- **The Annex group** (change EP-26; **agreed, Q2**): at most **1 per
  floor**, F2–5 only; Dormant; the Heap start shape; funded at the
  **patrol band** at the host room's level (a boxed lurker: one lurker at
  room level); **0 XP** (its reward is the Wager prize, the Dare cache or
  the box contents; **read (LE-10; M3):** the Annex has no prize socket
  of its own: its prize is the prize of what it serves, the Wager or Dare
  socket with that socket's one step, never a second, the boxed lurker's
  +0 entry, or nothing extra for the Tattle form; loot economy design 6.9
  item 1), so `WagerXpDelta` = 0 and the 710 pool and ENM 20.1
  hold; never a lurker (except as the boxed lurker), elite or leader on
  F2; it serves exactly one of: the Tattle beyond-room, the Wager guard,
  the guarded Dare garrison, the boxed lurker. Report `AnnexEngageRate`.
  **Rejected alternatives:** mapping the Wager guard onto a guard-post
  slot moves main-path XP into optional content (a Gb skip plus a Wager
  skip drops the pool to 510) and breaks ENM 10.4's "main path, at
  chokepoints" (XB-M4, XK-H2); and Q2's option B, no Annex.
- **Annex funding, choice and binding (lead's calls, after the check;
  inside Q2):**
  - **Funding:** the Annex is **one extra encounter budget** at the patrol
    band at its host room's level, gated by the cluster budget, the
    joiner cap, `ChainPullRate` and the 6.4 rows. It is deducted from no
    slot. EP-26 names this against TL 6.15's "it does not add one" and
    re-reads TL 6.15's "full-strength group" as "a patrol-band group".
  - **Priority** when several needs exist: **boxed lurker > Wager guard >
    Dare garrison > Tattle beyond**, then the lowest roomId (2a's Annex
    draw breaks only exact ties, 6.1). **Told plainly:** the uses
    exclude each other; the needs not served fall to their approved
    failure actions (the box becomes a real container, the Wager Site
    reverts to an ordinary side room, the Dare room is unguarded, the
    Tattle is a Crier).
  - **Binding:** 2a's choice is final for segment 6; `trapsResolve` only
    confirms or drops it.
  - **Position:** V12 requires `AnnexHearingGap` > 8 from every slot
    home (the Wager's ≥ 9 already holds from TL), since the Annex is not
    an FL slot home and LS12's proof does not cover it.
  - **The boxed-lurker Annex** keeps its TL 6.14 state (Hidden in its
    container, no placement roll) and takes no start shape: a reading of Q2's
    "Dormant", told plainly.
  - **Guarded forms:** the guarded Wager (TL 6.15) and the guarded Dare
    garrison (HZ 6.10 (b)) are served by the Annex **Dormant among the
    devices** (the party may wake it, sneak, or turn the room on it), a
    reading of "stands", told plainly.
- **Wager guard:** the Annex, Dormant in the Alcove, ≥ 9 path from other
  Dormant homes, taking the room's joiner slot (TL 6.15).
- **Boxed lurker:** the Annex; counts first toward the ENM 10.3 ambush
  cap (6.1); if the Annex or the cap is spent, the socket becomes a real
  container (the approved failure action).
- **Rigged hosts:** `trapChargeCost` (expected party-side trap damage,
  converted to levelCost units, 6.1) subtracted before fill (TL 6.9).

### 6.14 Bosses and arena hooks (segment 9)

Boss Court escorts use **Retinue** on arena start zones ≥ 9 from both
entrances (FL 6.11, **settled**), Watchful from the start (**settled**);
segment 9 owns interior tagging and the arena formation set. The F2–5
Forecourt patrol joins as an ordinary joiner (FL-21 (e), pending). The
F1 escort is non-fearless and breakable (RT-9 `F1EscortBreakable`), with
no elite. Souvenir slot dressing (RS 9.3) is never a table socket.
**Rival Party fit** in elite templates (FL 13): a Later stub (EP-34), so
the hand-off is not lost.

**Pending request (BR-19; M2):** the boss rooms design asks the EP owner
to change "Boss Court escorts use Retinue" to "Boss Court escorts use
segment 9's `ArenaFormation`s; EP's Retinue is one allowed shape on
F2–5" (2–3 authored formations per boss, each pinned by a Rendition).
This rule stands until the EP owner rules; if refused, arena formations
are authored as Retinue variants inside this shape on F2–5 (boss rooms
design 4.5.1, 14.2). **Agreed (Q4 of the boss rooms design,
2026-11-22; BR-19, narrowing HR-21; M2):** on F2–5 every Boss Court
formation, at every party size after AddCap, holds ≥ 1 non-fearless
escort or reserve group, in the escort at fight start wherever the
escort budget allows (boss rooms design 4.5.1, 4.7.4).

### 6.15 AI hand-off (summary)

| Datum | Read by | Rule |
|---|---|---|
| Billet hold squares, fall-back square | Billet term (EP-6) | Data-weighted, one-step; zeroed on a break or by Stung |
| `formationId`, break state, Keystone | Billet term; display | Deterministic breaks (6.6.4) |
| `ward` | Holder and Protector terms (approved) | Assigned by the solver |
| `watchedPortal` | Billet orientation; Hedgehog | — |
| Leash | Sentinel and bosses (approved) | Billets are not leashes |
| Open zone | Ambusher profile ("waits Hidden" read as "until a party unit is within 3 squares and in its line of sight, or its group or the bait is attacked or notices"; after the check) | A request to make ENM 17.3's wait precise |
| `billetScale`, `billetBase` | Billet term | Frozen at generation and composition (6.3, 6.6.3) |
| `units[].facing` | Display only | No rule reads it (6.6.3) |
| Retreat anchors | Morale flight (FL-12) | Unchanged |

### 6.16 Class interplay (lead's call; EP-29)

Each formation record carries an `answers{shade, vanguard, arcanist,
herald}` field naming each class's lever. **A formation may leave at
most one class without a lever**, and a per-floor V12 assertion requires
each class to have a **showcase** (a formation whose written counter is
its lever) on ≥ 1 main-path fight on F1–3 (XF8). **Extended to bosses
(BR-19; agreed, Q4 of the boss rooms design; M2):** every boss authors
one answer window per launch class in segment 9's `BossAnswerMatrix`,
which V17 checks; the Herald row on F2–5 is the non-fearless Boss Court
group of 6.14 (boss rooms design 4.7.4).

| Class | Levers placement guarantees | Watch |
|---|---|---|
| Shade | `StealthOpenable`; Busy and Dormant start shapes; `BacklineReach` lanes; a Hidden backline reach does not break the line; Waylays and Cuckoo bait are Surprisable; the seed target `enemyNotYetActed` | `PartySurpriseShare` against its `stealthShare` table |
| Vanguard | Keystones on chokes; a Shove (push 1) that ends the Keystone outside its hold squares breaks the formation (6.6.4); Phalanx Shove-out squares (`ShoveOutSquare`); arrival-Portal arrows for Receive and Bar the Way | Pointless in Skirmish Spread: give it the Phalanx showcase |
| Arcanist | Areas ignore cover (CV7); clusters on F1 (seed target); **a line-of-sight breaker within 3 of the landing in every overwatch or Crossfire host**; fall-back squares that open windows | Formations raised its HP-eq loss most (+20%, toy; the Kiter targets "lowest visible AC in range"): the `ClassHpEqDelta` gate, an absolute spread (11.3); levers: the Crossfire gate, then `LandingShooters`, never the Kiter profile |
| Herald | `LeaderReach`, `AuraCover`, Retinue; morale breaks formations; cowable guarantees; **fearless-anchored skins** (Gut Knights, Wake Pallbearers, Choir Vergers, Archive Misprints, the Pale Mare) are tagged "kill the anchor, then the rest Cow" (RE9 guarantees ≥ 714‰ non-fearless) | No Rumour source (rejected), so routs cost nothing extra |

### 6.17 Floor 1 (lead's call; EP-11, EP-13)

- **Mandatory (settled / agreed):** no solo Surprise at `mainPathIndex`
  0–1 (DH 17.1); no lurker or ambush in rooms 0–2; 0 hard control; ≤ 1
  hinderer, not in rooms 1–2; 0 mindless; ≥ 750‰ non-fearless; units 4
  (≤ 2 minions solo, ≤ 2 brutes, ≤ 1 soldier per encounter); elites only
  in the second-half elite room at level 1; `Floor1NoCoveredStarts`;
  ranged ≤ ½; leaders only as a Picket's leader-role standard, a boss
  escort or a Champion elite; the 2.5% cumulative budget.
- **This design:**
  - the first fight host: Picket Line or Door Line, Posted or Busy, FT1,
    at the 850‰ fill minimum (11 of 12 at the expected solo weight; G1
    and W1 are recast to 11, 7; on the Kennel the first G is the
    Huntsman +0 with one minion, 12, because K1 moved to room level 2,
    7.3; cross-document comparison 2026-11-23, CC-2); the KP8 teaching room hosts a lone
    front enemy in line with the light piece;
  - **the lair is a Dormant Pack** (6.5); a Pack guard post may also be
    Dormant;
  - **`billetScale` 500‰** (F1 is always solo; 6.3; lead's call, after
    the check), so the Holder lessons work;
  - **no "last guard post is FT2 or FT3" rule on F1** (6.2; lead's call,
    after the check);
  - **the F1 ambush is a Cuckoo only, only in the role order "E G Gb Q L
    G F B"** (the last G after the lair, so the ambush never stacks into
    the lair-Forecourt-boss chain, XF10), weights Wake 500‰, Kennel 300‰
    (not on the same seed as the horn near pair), Gaol 250‰ via EP-13;
    never a Baited Room (F1 is always solo);
  - **a floor-1 lurker never uses Ambush Strike** (change EP-11 to ENM
    17.1, 9.3 and invariant 6: needed because, under the per-side
    reading, a Cuckoo has no surprise round, so invariant 6 alone would
    no longer bar it). It opens with its at-will at 150%, stated plainly
    on its card;
  - **feasibility:** a standard-bait Cuckoo fits the F1 solo guard budget
    (for example bait −1 (7) + lurker −3 (4) = 11 of 12) if native levels
    allow; settled by the CMP-8 content test, not asserted (XB-M5,
    XK-M8). Where native levels do not allow a legal fill in the window
    on a seed, the F1 Cuckoo is not drawn on that seed (6.8.1, new
    lead's call);
  - **the Gaol (change EP-13 to FL 7.2 `F1LessonLadder`):** the guard
    post adjacent to the lair teaches "the shut-door pair **or** the
    hooded cot" (one per seed), so the Silent Penitent has a legal home.
    The bait is the Turnkey (a standard), not a padlocked cache, which
    would repeat room 3's lesson;
  - no Waylay, Phalanx, Crossfire, Pincer, Annex or Stand-To-like
    posture; `StartArc` 120°; `LandingShooters` 1;
  - `SeedHasTarget`: ≥ 2 main-path encounters satisfy each class's CL
    `seedTarget` predicate, verbatim (corrected after the check): the
    Shade `enemyNotYetActed` (an unaware group); the Vanguard
    `anyEnemyInMelee` (any group with a melee unit the hero can close
    on); the Arcanist `twoEnemiesWithinRadius1` (a Revel start shape or a
    Crescent at start); the Herald `cowableGroup` (the Scavengers patrol
    through Q room 3 and at least one more: a leaderless Picket or the
    lair Pack);
  - V12: ≥ 3 legal (template, formation, activity) triples per F1 theme;
    `FormationFallbackRate` < 5% on F1.

### 6.18 The clock (lead's call, told plainly; EP-33)

- Segment 6 had no approved allowance. Line: **≤ +0.2 rounds per fight
  on average** and **no net minutes at party 4 on F4–5** *(illustr.)*.
  The toy measured −0.04 to −0.16 rounds per fight under B, but its
  breaks fired at round 1; tightening them (6.6.4) will cost rounds, so
  the toy's "clock-neutral" result is **invalid until re-measured**
  (XB-H5).
- Activities, chips and the Read card add 0 clicks; Listen costs are
  unchanged; Hold the Hide is 1 click.
- **Cut order, applied before release if the line misses** (the
  companions 7.4 levers stay untouched): (1) no Crossfire or Kill Box on
  F4–5 guard posts at party 4 (corrected after the check: the proposal's
  first lever cut Kill Boxes from lairs, where none can stand); (2)
  `maxRounds` 3 → 2; (3) halve the Waylay share; (4) Revel and Bout start
  in Posted formation (display only).

### 6.19 Seeds (lead's call; change EP-1, EP-3)

- The `encounters` child stream and its draw table (6.1); segment 11
  owns the tree. It is the node `dungeon/floor/{N}/attempt/{a}/encounters`
  with the sub-keys `plan` (step 2a, GS8) and `place` (steps 5, 5b, 10a,
  10b, GS12), counts unchanged, each a registry row (seeds and
  validation design 4.2, 4.3; changes SV-1, SV-2; M1).
- Lurker Stealth by keyed hashes, one per roll (6.8.5), in the domain
  `h:lurkStealth` on the floor's `checks` stream (SV-6; M2).
- One segment-6 retry per floor attempt on the keyed child
  `…/attempt/{a}/encounters/retry1` (6.1), the LR2 rung of the one
  ladder (seeds and validation design 5.5; SV-10).
- The solver, Billets, Patrol File, Terrain Pairings and the fallback
  chain are draw-free.
- Same seed and same inputs give the same placement; retries never
  perturb segments 3–5.

### 6.20 UI and previews (lead's call; EP-14, EP-28; segment 10 owns the display)

- **On the board, only:** the formation chip (its battle formation's
  icon, with its name on hover; RC9 names never lie) and the Keystone
  chip. Everything else lives in the **Encounter Read card** on inspect
  (XF14).
- **`ChipNoLeak` (lead's call, after the check):** a formation chip names
  a formation only once **every member of every group in its encounter
  has been noticed**. Until then a group with Hidden or unnoticed members
  shows no formation chip (a Cuckoo's bait shows only its own plate); the
  Bait and Nook chip appears once a lurker is noticed or marked. No chip,
  Read card field or break-trigger count (the "a third of members" line
  counts noticed members only) refers to an unnoticed unit. A V12 / V7
  assertion.
- **Board read surfaces (lead's call, after the check; registered in the
  RC5+ pattern registry so no two differ by hue alone, and added to the
  colour-blind and greyscale gates; the registry entry is a request to
  the RS owner under EP-12, pending; cross-document comparison
  2026-11-23, CC-103):**

| Surface | Shape and pattern | Anchor |
|---|---|---|
| Formation chip | The battle formation's icon; the name on hover | The Keystone's plate; with no Keystone, the front-most unit's |
| "Line broken" | The same chip with a split frame | As above |
| Keystone | A notched-crown pip on the plate | The Keystone's plate |
| Hold squares (Perception) | Corner brackets on each square | The squares |
| Fall-back (Perception) | A chevron arrow joined to the unit by a dashed tether | The fall-back square |
| Marked tell | A ring with an eye tick | The tell square |
| Seen-tell band marker | The marked-tell ring, hollow, on the danger band's threat list | The tell square |

- **Read card:** seen types and roles; the activity word; the formation
  name and break triggers (all parties); **hold squares and fall-back**
  for parties with a Perception-trained member (change EP-28 to ENM
  13.5, which calls an intent read "the one place" the display shows AI
  output); **the counter line**, free the *first* time a formation is met
  in a run, then with Lore for that family (XF11); the Keystone; any call
  source; the hearing-outline draw list.
- **Lore roster preview** (ENM 10.6) lists the theme's formations and
  activities by name on floor entry.
- **Listen** (FL 9.2): the activity word (trained); "still"; the watched
  Portal for a Waylay (trained); never Hidden lurkers.
- **Danger band** (change EP-14; **agreed, Q3**): a tell that has been in
  a party unit's line of sight counts as a known threat using the lurker
  type's card; a marked lurker counts fully. A tell nobody saw adds
  nothing. **Where it stands (lead's call, after the check):** DH 17.3
  computes reach from the enemy's square, which is hidden, so a seen tell
  treats the lurker as anywhere within 6 of the tell square: a hero is
  in its reach whenever within 6 + the lurker type's move + its reach of
  the tell square. No hidden position is read (DH 17.2; invariant 4).
  This answers "deaths from Safe ≈ 0" without leaking anything the art
  had not already shown. Rejected: B, the band keeps ignoring
  hidden lurkers.
- **Rest preview** (EP-16): adds Alerted, Searching and Pursuing groups
  to the seen patrol routes; a group last seen Alerted, Searching or
  Pursuing shows "last seen <state> at tick N; route unknown" and no
  forecast (exploration and information design 4.8.1; cross-document
  comparison 2026-11-23, CC-105). Its movers read `PatrolForecast` over known
  patrols (change XI-6; agreed, exploration and information Q2; cross-document comparison 2026-11-23, CC-106; M2; exploration and information
  design 4.8.1).
- **Hearing outline:** arrival-Portal arrows; Hedgehog groups marked.
- **Approach band out of combat** (a DH 17.3 change): M3, a later client
  decision (EP-41). **Also waiting on EP-41 (XI-12, XI-27; M3):** the
  noticing step tag's odds on the move preview and Cased's noticing-odds
  band, each also on the ENM 13 flag; if EP-41 is refused, neither ships
  with odds (exploration and information design 4.8.3, 4.5.2, 14.2).
- Display only: no draw, no state change; the AI never reads it
  (invariant 10).

### 6.21 Rest, backtracking and escalation (lead's call; EP-15, EP-16)

- **No generated rest, camp or backtrack ambushes**, no wandering rolls,
  no relocation (ENM 16; DH 10.4). Only units already on the map and
  forecast can interrupt a rest.
- **Hedgehog** (a reading of ENM 12.1's "back at its post"): a group
  that turns Watchful by the approved route returns to its **Billet hold
  squares** in its battle formation's defensive variant: Keystone toward
  the Portal where the party was last seen, activity Posted; Watchful +3
  noticing and un-Surprisable as approved; no healing (invariant 8), no
  new members. **Details (lead's calls, after the check):**
  - a group **without Billets** (a patrol, or any group in a start shape that
    never formed up) re-forms at its battle formation's squares in its
    homeRoom (for a patrol, the Circuit room where it lost the party);
  - survivors take positions in core-then-growth order by the 10d role
    priority; a marksman goes on overwatch only where its Billet already
    is overwatch;
  - `defensiveVariant` = `{facePortal: lost|all, billets: keep|none}`:
    `lost` and `keep` by default.
  - **A group that lost ⅓ of its members or its Keystone re-forms
    without Billets** (`billets: none`, facing only), and **watches every
    Portal** (`facePortal: all`) of a
    multi-Portal host, so the disengage-rest-return loop cannot re-arm a
    fresh formation start (XF6, XB-L5). Report `ReEngageRate`. The real
    lever stays the DH rests gate.
- **Escalation stays per group:** Alerted on noise; Watchful after losing
  the party; Once Bitten (TL); Crier and alarm arrivals Alerted (TL-30);
  joiners. **Rejected:** the Rumour meter, the Prowl, Stand To and
  Uneasy (FL 9.1 rejected a floor-wide alarm, and changing a capped-out
  group's posture on hearing would change decided MAIN 4.3's "groups
  beyond the cap do not change state"). Told plainly: a floor can feel
  quieter once the patrol dies (FL 19.2 (c) stays open).

---
## 7. Per-theme encounter catalogue

All compositions are *illustr.* "type ±diff (cost)" at the expected
party and must pass CMP-8 and the 6.4 rows on placed geometry. Unit
costs by level difference (**settled**): 4/5/7/10/14/20/28; minion
max(2, levelCost ÷ 3); elite ×2. Fill window 85–100% of the slot budget
(the illustrative budgets of 6.2). Entries the drafts had built on
illegal templates are recast here (XK4; A.3). **New lead's call:** four
entries whose arithmetic missed the fill window are corrected (K5, S3 at
F3, GU4) or completed (GU6), and the Choir patrol (C2) is restated
cleanly; each is marked where it stands. **After the check (lead's
calls):** entries that missed the fill floors are recast (G1, K1, W1 to
the 850‰ minimum of 11; K6 and W5 to 19 of 19; C3, S4 and GU6 to the
lair's 925‰); each group names its battle formation and, where Busy,
Dormant or Afoot, its start shape; seated poses on pews become floor-socket
poses; the Baited Room takes 2 lurkers from F3 at party ≥ 3; F2 Kill Box
forms are added for the Choir, Archive and Carnival; and the Ritual gains
its leader.

### 7.1 Summary

| Theme (floors) | Formation style | Missing roles → templates that cannot fill | Ambush forms | Showcase |
|---|---|---|---|---|
| Gaol (F1) | The cell-door line: the Turnkey holds the door, Lamplighters along the far wall | hinderer → — | Cuckoo (the hooded cot, EP-13) | Doors, noise, keys |
| Kennel (F1) | A Crescent round logs; the Houndmaster behind but visible | hinderer → — | Cuckoo (Ghillie) | Morale and positioning |
| Wake (F1) | Mourners circling the coffin; Pallbearers at Drapes | hinderer → — | Cuckoo (Late Uncle), the F1 signature | Reading tells, the sleeping house |
| Choir (F2–3) | Choir rows (a Phalanx skin); the Organist on the dry island | lurker → Ambush | Waylay | The drowning line |
| Archive (F2–3) | Aisles: reach at aisle mouths, Misprints down the lane | skirmisher → Skirmish Screen | Cuckoo, Baited Room, Waylay | Webs, silence, fire |
| Carnival (F2–3) | The ring: the Lion-Tamer on the choke, the audience on the flanks | hinderer, lurker → Gauntlet, Ambush | Waylay | Oil and fire both ways |
| Supper (F4) | The table: servers between door and table | lurker; no promotable soldier → Ambush, Honour Guard, Battery Challenger | Waylay | Freeze and fracture |
| Gut (F4) | The membrane: Antibody Knights across tube chokes | skirmisher; acid cap → Skirmish Screen, Gauntlet | Cuckoo, Baited Room | Attrition |
| Heart (F5) | The Nursery: loose and skittish early, led late | brute, lurker → Gauntlet, Ambush | Waylay | Waking |
| Cathedral (M4) | No roster | all | — | — |

### 7.2 The Gaol of Small Hours (F1; M2 core)

Roster: Inmate (swarmer), Escapee (skirmisher), Turnkey (soldier,
promotable, the Keybearer), Lamplighter (marksman), the Lifer (brute),
Silent Penitent (lurker), Jailer-Sergeant (leader). Every formation
faces one door; the counter is always "a second door, or a door you
close".

- **G1 Roll Call** (the first G, index 1): Picket, Turnkey −1 (7) + 2
  Inmates −1 (4) = 11 (the 850‰ minimum; after the check); **Picket
  Line**, the Turnkey on the
  Clearway-adjacent socket at the cell door, in line with the laundry
  cart (KP8); roll call (Posted). Lessons: doors isolate; Kick or Shunt
  the cart; the party may Surprise them (the hero is never Surprised at
  index 0–1). *Touches ENM 10.3; DH 17.1; RT KP8.*
- **G2 The Junction Desk** (Gb): Picket, the Lifer +0 (10) + Inmate −1
  (2) = 12; Picket Line facing the main-path door; dicing (Busy, a Revel
  start shape round the desk; the dice lie still on the Low desk). The quiet
  bypass. *Touches FL 6.8.*
- **G3 The Shuffle** (the patrol through Q room 3): Scavengers, Escapee
  −3 (4) + 2 Inmates −1 (4) = 8; Patrol File, then Scramble when met;
  always cowable. The cover-room window among the desk lines. *Touches
  CL-10; FL 6.7.*
- **G4 Lights-Out** (the lair, the Yard): Pack (the Escapee leads
  Inmates), Escapee +0 (10) + 2 Inmates +0 (6) = 16; a **Heap**
  start shape (a Crescent once awake) beside the Clearway (on floor squares by the cots; the cots stay
  Penitent sockets). Silent kill on the Escapee, then Cow; the
  Arcanist's radius-1 target. *Touches FL-21 (d); `SeedHasTarget`.*
- **G5 Visiting Hour** (the last G, L-G order only, 250‰; EP-13):
  Cuckoo, Turnkey −1 bait (7) + Silent Penitent −3 (4) = 11 of 12; Bait
  and Nook; the hood that turns to follow is the tell, visible from the
  doorway at ≥ 7. F1: at-will opener. *Touches ENM 17.4; FL 7.2.*
- **G6 The Keyring Office** (the elite room): Champion, elite Turnkey −1
  with Plated (14) + Lamplighter −2 (5) = 19; Champion Watch; the
  Lamplighter watches from beside the warden's stove. Draw it off with a
  bell-wire lure, then Steal the keyring. *Touches ENM 7.2, 15.5.*

### 7.3 The Kennel Wood (F1; M1 subset)

Roster: Whelp (swarmer), Gaunt Hound (skirmisher), Kennel Mastiff
(brute), Huntsman (soldier), Poacher (marksman), Houndmaster (leader),
Ghillie (lurker). M1: Mastiff, Huntsman, Poacher, Old Ragged.

- **K1 The Huntsman's Stand** (guard post; **M1**): Line and Bow,
  Huntsman −1 (7) + Poacher −2 (5) = 12 of 12, **at a guard post of room
  level 2 or more** (on F1 the last G, `mainPathIndex` 5; from M2 it
  shares that host's draw with K3 and K5). **Re-checked (change RT-22;
  cross-document comparison 2026-11-23, CC-2):** the Huntsman is now the
  M1 warhammer-and-shield soldier, legal only at L1–4 (armour 12.3), so
  its −1 shift needs room level 2 (L1; the Poacher at −2 is the L0 sling
  marksman). At room level 1 (the first G) the −1 Huntsman would be L0
  and the old Poacher −3 below level 0, and dropping the shift (Huntsman
  +0 (10) + Poacher −1 (7) = 17) overruns the budget of 12, so the K1
  room is raised (lead's call): the first G takes the 6.3 solo table's
  B 12 cell instead, the Huntsman +0 (10) + one minion at −1 (2) = 12 on
  Picket Line (the ENM 21 minion template in M1; a Whelp from M2);
  **Door Line**: the Huntsman at
  the 1-wide trail mouth, the Poacher on the log line 6–8 back,
  uncovered; the glade's second mouth is the `BacklineReach` lane. "Go
  around"; ≤ 1 marksman per F1 solo encounter (O15).
- **K2 Kennel Run** (the lair; M1 with the ENM 21 minion template, M2
  with Whelps): Pack, Mastiff +0 (10) + 2 Whelps +0 (6) = 16; **a gorged
  Revel start shape at a hay manger (Dormant)** (the proposal's carcass is not
  an RT piece; after the check), a Crescent round logs when awake. Back
  to a log; kill the Mastiff (T3 Demoralises the Whelps;
  `PackMoraleEarly` must hold).
- **K3 Horn and Hounds** (the last G, the horn near pair at index ≥ 4):
  Picket with the leader-role standard, Houndmaster −2 (5) + Gaunt Hound
  −3 (4) + Whelp −1 (2) = 11; Picket Line with the Houndmaster behind but
  visible at ≥ 7. Kill the leader; beat the footsteps. *Touches FL 6.6;
  RS 8.2 (g).*
- **K4 The Strays** (the patrol): Scavengers, Gaunt Hound −3 (4) + 2
  Whelps −1 (4) = 8; Patrol File on the ring trail; the fallen-log
  window.
- **K5 Bracken Blind** (the last G, L-G order, 300‰, never on K3's
  seed): Cuckoo, Poacher −1 bait (7) + Ghillie −3 (4) = 11 of 12
  **(new lead's call:** the proposal's Poacher −2 (5) + Ghillie −2 (5) =
  10 is below the 11–12 window; recast to the Gaol pattern, if native
  levels allow, else not drawn on that seed, 6.17**)**; the twitching
  leaf cape within 6, readable from 7.
- **K6 The Trophy Room** (the elite room): Champion, elite Huntsman −1
  with Warded (14) + Whelp 0 (3) + Whelp −1 (2) = 19 (the elite room's
  950‰; after the check); Champion Watch among the
  mounted antlers (Tall); both Whelps watch.

### 7.4 The Endless Wake (F1; M2 conditional, else the first M3 unlock)

Roster: Black-Crepe Guest (swarmer), Black-Glove Cousin (skirmisher),
Pallbearer (soldier, fearless), Gravedigger (brute), Hired Wailer
(marksman), the Late Uncle (lurker, fearless), Undertaker (leader).
Hiding comes from canopies and Drapes, never coffins (RT 7.4). Dormant
bias only outside `nearStart`.

- **W1 Pallbearers' Line** (the first G): Line and Bow, Pallbearer −1
  (7) + Hired Wailer −3 (4) = 11 (the 850‰ minimum, if native levels
  allow; after the check); Door Line; the Pallbearer beside a
  Drape Portal, the Wailer behind a pew row, uncovered. **Draw the
  curtain:** the Drape blocks the Wailer's line of sight (FL 9.2).
- **W2 The Undertaker's Count** (Gb): Picket with the leader-role
  standard, Undertaker −2 (5) + Cousin −3 (4) + Guest −1 (2) = 11; Picket
  Line; the Will-Box key at the hip. The servants' passage; Steal on the
  way past.
- **W3 The Uncle's Chair** (the last G, L-G order, 500‰; the Wake's
  signature): Cuckoo, a kneeling standard bait at the coffin (Cousin −3,
  4; units never sit on pieces, after the check) + Late Uncle
  −1 (7) = 11; the cane tapping on the coffin tell socket, visible at
  ≥ 7. The floor's tell lesson; an at-will cane, never the razor's Ambush
  Strike.
- **W4 The Last Round** (the lair): Pack (the Cousin leads Guests),
  Cousin +0 (10) + 2 Guests +0 (6) = 16; a Heap start shape, slumped
  against the pew backs on the floor sockets beside the rows, round the
  catafalque (a Crescent once awake). The creaking board (noise 6; Dormant hear at half
  radius); the M3 candle rack (snuffing all candles wakes it Watchful,
  announced).
- **W5 Undertaker's Parlour** (the elite room): Champion, elite
  Pallbearer −1 with Plated (14) + Guest 0 (3) + Guest −1 (2) = 19 (after
  the check); Champion Watch;
  a fearless elite cannot be Cowed; draw the Guests off with the
  Gramophone (noise 6).
- **The Viewing Door Tattle** (TL 7.4): no Annex on F1, and no slot group
  ever stands beyond a Tattle (ET9), so it resolves to a Crier (told
  plainly).
- **Patrol:** Scavengers on the gallery ring (Cousin + 2 Guests), Patrol
  File.

### 7.5 The Drowned Choir (F2–3)

Roster: Chorister (swarmer), Eel-Acolyte (skirmisher, skittish), Drowned
Verger (soldier, fearless), Clapper-Monk (brute), Storm Organist
(marksman), Tidecantor (hinderer), Deacon (leader). RS 8.6 caps: no
water under or adjacent to party entry; a dry island within 2 of each
entrance; ≤ ½ of starts in water in every Tide state.

- **C1 Evensong** (guard post): Line and Bow. F2 (20): 2 Vergers −1/−2
  (7 + 5) + Organist −2 (5) = 17. F3 (31): Verger 0 (10) + Verger −1 (7)
  + Organist 0 (10) = 27. **Phalanx** (choir rows) across the nave
  choke, the Organist on the island. Shove a Verger into water; Charged Water;
  the side-aisle flank. Solo F2: Verger 0 alone, Picket Line. **Kill Box
  form** (F2–3 Waylay, party ≥ 2; after the check): the same Line and
  Bow on a Waylay host, the Organist on a blind socket, the Vergers on
  the blind flanks; the barnacle-scrape Spoor mark.
- **C1b Evensong Rows** (guard post, F3, Warband): Deacon −1 (7) +
  Verger 0 (10) + Verger −1 (7) + 3 Choristers −1 (6) = 30; **Retinue**
  with the Choristers in water as Charged Water bait for the party (both sides'
  lightning matters).
- **C2 The Procession** (the patrol, Picket): F2 (14): Verger 0 (10) +
  Chorister 0 (3) = 13; F3 (21): Verger 0 (10) + Verger −1 (7) + 2
  Choristers −1 (4) = 21 **(new lead's call:** restated; the proposal's
  draft carried a struck-through leader version, and the Picket's
  leader-role version is dropped so the Deacon stays a Warband and Ritual
  showpiece; wording corrected after the check**)**. Patrol File; the chapel window. May join the
  boss (FL-21 (e)).
- **C3 Quire Watch** (the lair, Honour Guard): F2 (28): elite Verger −1
  Challenger (14) + Organist 0 (10) + 2 Choristers −1 (4) = 28 (the lair's
  925‰; after the check); Honour
  Ring on the causeway before the dais island. The Organ Swells (M2,
  gated).
- **C4 Tidewatch** (Waylay, F3 only, ≤ 1): Picket, Tidecantor +1 (14) +
  Eel-Acolyte 0 (10) + Chorister 0 (3) = 27; Kill Box facing the nave
  door (closed); the Eel in water behind organ-pipe clusters, the
  Tidecantor on a dry flank; **a barnacle-scrape tally on the nave door
  frame** (not water, after the check);
  the side-aisle way round costs a Squeeze. Ships only after the Waylay
  gate.
- **C5 Vespers** (the lair, Den, F3): Deacon +1 (14) + Clapper-Monk +1
  (14) + 4 Choristers +0 (12) = 40; a Heap start shape beside the
  pew rows (a Crescent, the Deacon as mother, once awake); the
  Belfry Wire as a trap opener (TL-16, pending).
- **C6 The Hymn** (Ritual, F3, **M3**): a Warband, the Deacon leading a
  Verger and 2–3 `singer` Choristers that ring the WetEdge host (the
  leader added after the check); the water rises one round after the last singer falls.
  Lightning parties want the flood; others leave one singer.

### 7.6 The Cobweb Archive (F2–3)

Roster: Inkling (swarmer, mindless), Scribe-Spider (hinderer),
Margin-Crawler (lurker, skittish), Spine-Binder (brute, promotable),
Shusher (soldier), Misprint (marksman, fearless), Chief Archivist
(leader). Groups with an Inkling are never cowable.

- **S1 Aisle Watch** (guard post, F2): **Picket** (recast from Line and
  Bow), Shusher 0 (10) + Misprint −1 (7) + Inkling −1 (2) = 19; Door Line
  down an aisle ≥ 6. Burn the webs; the dogleg round the stack.
  **Kill Box form** (F2–3 Waylay, party ≥ 2; after the check): Shusher 0
  (10) + Misprint 0 (10) = 20 with no Inkling (a Waylay is minded only);
  ink smudges on the jamb.
- **S2 Cataloguing** (guard post, F3, Warband): Chief Archivist 0 (10) +
  Shusher 0 (10) + Shusher −1 (7) + 2 Inklings −1 (4) = 31; Retinue round
  a reading desk; cataloguing (Busy, a Revel start shape; the Inklings stand
  Posted, 6.4).
  Steal the Closed-Stack key.
- **S3 Margin Notes** (Cuckoo, F2–3): F2 at full band (20): bait Shusher
  0 (10) + Margin-Crawler 0 (10) = 20; F3 (31): bait Shusher −1 (7) + 2
  Margin-Crawlers 0 (20) = 27 **(new lead's call:** the proposal's "bait
  at −2" gives 25, below the 27–31 window**)**. A page that turns by
  itself within 6. **Baited Room form (M2):** a bait-cache reading tray
  (pending request LP-16, M2: an ink-lacquered document case; loot
  sources design 5.3) +
  1 Margin-Crawler, F2–3 at party ≥ 2, or 2 Margin-Crawlers from F3 at
  party ≥ 3 (for example F3 party 3, budget 26, window 23–26: Crawler 0
  (10) + Crawler +1 (14) = 24; after the check), fill per CMP-8 **(new lead's
  call:** the proposal called it an M3 variant, against its own M2
  staging, 6.8.1; where no legal fill exists at the weight, as at F2
  party 2, whose 15–17 window falls between lurker costs 14 and 20, it
  is not drawn, 6.8.1**)**. **Told plainly (after the check):** at the
  expected party the one-lurker form rarely fits, so the Archive Baited Room
  is mostly an F3 party-3+ event and the F2 Baited Room is rare.
- **S4 Silent Reading** (the lair, Sleeper, F3): elite Spine-Binder +1
  Hardened (28) + Scribe-Spider +1 (14) = 42 (the lair's 925‰; after the
  check); a Heap start shape between carrels (a Picket Line, the
  elite in front, once awake). The Shade's showcase.
- **S5 Web Weavers** (the lair, Gauntlet, F3): elite Scribe-Spider 0 (20)
  + 2 Spine-Binders 0 (20) = 40; Grindstone in a web field; Web Shot is
  the one hard-control source.
- **S6 Aisle Kill Box** (Battery, F3, Waylay): 2 Misprints 0 (20) +
  Shusher 0 (10) = 30; **Kill Box** with the two Misprints at two stack
  ends crossing the watched lane (6.6.6; **new lead's call:** renamed
  from the proposal's "Aisle Crossfire"); ink smudges on the jamb; never
  solo.
- **Patrol:** Picket, Shusher 0 (10) + Scribe-Spider −3 (4) = 14;
  cowable (no Inkling).

### 7.7 The Carnival of the Last Evening (F2–3)

Roster: Roustabout (swarmer, skittish), Knife-Thrower (skirmisher),
Lion-Tamer (soldier, promotable), Weightlifter (brute), Carousel Horse
(brute, mindless), Fire-Eater (marksman), Ringmaster (leader).

- **CA1 The Show** (guard post, Warband): F2: Ringmaster −1 (7) +
  Lion-Tamer −1 (7) + 2 Roustabouts −1 (4) = 18; Retinue as the ring, in
  a Revel start shape while Busy (the audience); it breaks at its first
  loss.
- **CA2 The Fire-Eater's Pit** (guard post, F2): **Picket** (recast),
  Weightlifter 0 (10) + Fire-Eater −1 (7) + Roustabout −1 (2) = 19 (also its
  **Kill Box form**, F2–3 Waylay at party ≥ 2, sawdust in the lintel;
  after the check); Door
  Line with a shove lane to the pit (one shover type if Pit Breathes is
  live; no Billet within 2 of the pit edge). A Keg ≥ 2 from starts and
  the entry lane as party bait.
- **CA3 The Knife Act** (the patrol, Skirmish Screen): 2 Knife-Throwers
  −1 (14); Skirmish Spread on the backstage ring.
- **CA4 The Booth Gallery** (Waylay, F3): **Picket** (recast),
  Knife-Thrower 0 (10) + Lion-Tamer +1 (14) + 2 Roustabouts 0 (6) = 30;
  Kill Box behind booth rows; sawdust in the lintel; the back-lot way
  round past a hazard edge.
- **CA5 Carousel** (the lair, Line and Bow, F2): Carousel Horse 0 (10) +
  Weightlifter 0 (10) + Fire-Eater −1 (7) = 27; Door Line; the Horse is
  loose front mass with no Billet.
- **CA6 The Tamer's Ring** (the lair, Honour Guard, F3): elite Lion-Tamer
  0 Challenger (20) + Fire-Eater +1 (14) + 2 Roustabouts 0 (6) = 40;
  Honour Ring round the slick.
- **Cowable:** Scavengers of 3 Roustabouts.

### 7.8 The Long Supper (F4; F3 from M3)

Roster: Still Footman (swarmer, mindless), Glutton (brute, promotable),
Butler-at-Arms (soldier), Sommelier (hinderer; ≤ 1 Frozen source),
Ice Sculptor (marksman), Carver (skirmisher, skittish), the Seneschal
(leader).

- **SU1 Second Course** (guard post, Line and Bow; recast without
  minions): Butler +1 (14) + Butler 0 (10) + Ice Sculptor +1 (14) = 38;
  Door Line, servers between door and table, the Sculptor behind the Low
  table (overwatch). Plate-warmer braziers end Chilled and Frozen both
  ways.
- **SU2 The Seating Plan** (guard post, Warband): Seneschal +1 (14) + 2
  Butlers 0 (20) + 2 Footmen 0 (6) = 40; a **Retinue in a Revel start shape**
  at the table head
  (Busy; the Footmen stand Posted, frozen mid-bow, 6.4). Shove the
  Seneschal onto the known Cold Seat once TL-16 lands.
- **SU3 The Cellar Pour** (the lair, Gauntlet): Sommelier +2 (20) +
  Glutton +2 (20) + Glutton +1 (14) = 54; Grindstone beside the warmers.
- **SU4 Full Belly** (the lair, Sleeper): elite Glutton +2 (40) + Carver
  +1 (14) = 54; a gorged Revel start shape (Dormant; legal for a Sleeper after
  the check), a Picket Line once awake.
- **SU5 The Carvers** (the patrol, Scavengers): 2 Carvers +1 (28);
  Patrol File; the cowable group.
- **SU6 The Service Hatch** (Battery, Waylay): 2 Ice Sculptors +1 (28) +
  Butler 0 (10) = 38; **Kill Box** with the Sculptors' lines crossing the
  table lanes behind high tables (6.6.6; **new lead's call:** the
  proposal said "Crossfire"); a wine stain on the door.

### 7.9 The Gut of the World (F4)

Roster: Tenant (swarmer, skittish), Gutworm (lurker), Antibody Knight
(soldier, fearless), Gall Slick (brute, mindless), Acid-Spitter
(marksman), Spore-Priest (hinderer), the Landlord (leader). Every
composition is checked with `AcidSourcesAfterOverlay`.

- **GU1 Immune Sweep** (the patrol, Picket): 2 Antibody Knights 0 (20) +
  2 Tenants 0 (6) = 26; Patrol File (fearless Knights: not the cowable
  group).
- **GU2 Membrane** (guard post, Line and Bow): 2 Knights +1 (28) +
  Acid-Spitter 0 (10) = 38; Phalanx across the tube choke; acid sources
  1; the braid loops are flank pairs.
- **GU3 Rent Day** (guard post, Warband; arithmetic corrected): Landlord
  +1 (14) + Knight +1 (14) + 3 Tenants 0 (9) = 37; Retinue; rent
  collection (Busy, a Revel start shape). Kill the Landlord; the skittish Tenants break.
- **GU4 Burrow** (Cuckoo): bait Acid-Spitter +1 (14) + 2 Gutworms 0 (20)
  = 34 of 40 **(new lead's call:** the proposal's Spitter 0 bait gave 30,
  below the 34–40 full-band window; the +1 shooter keeps the F4–5 ≤ 925‰
  rule moot**)**; a pulsing bulge within 6; off Stir footprints.
  **Baited Room form (F4; 2 lurkers only at party ≥ 3):** a bait-cache
  strongbox (pending request LP-16, M2: a tenant's sewing basket; loot
  sources design 5.3) + Gutworm +2 (20) + Gutworm +1 (14) = 34, the full
  Baited Room budget at party 4 (85% of 40; window 29–34).
- **GU5 Sated Den** (the lair, Den): Landlord +2 (20) + Gall Slick +2
  (20) + 4 Tenants +1 (16) = 56; a Heap start shape, digesting (a
  Crescent once awake); acid sources
  1.
- **GU6 Antibody Escort** (the lair, Honour Guard): elite Antibody
  Knight +1 Challenger (28) + Spore-Priest +2 (20) + 2 Tenants +1 (8) = 56
  of 57 **(new lead's call:** the proposal left the fill to CMP-8; raised
  from 50 to the lair's 925‰ after the check**)**;
  Honour Ring.
- **Cowable:** a Tenant Scavengers group.

### 7.10 The Dreamer's Heart (F5)

Roster: Shadow-Puppet (swarmer, mindless), Sleepwalker (soldier, promotable,
skittish), Storybook Knight (soldier, skittish), Pale Mare (skirmisher,
fearless), Insomniac (marksman), Lullabist (hinderer), Mother-Shape
(leader). `skittishShape` rooms 1–2; `ledShape` the elite room and the
highest-index guard post.

- **H1 Sleepwalkers** (the patrol, Picket): 2 Sleepwalkers +1 (28);
  Patrol File, sleepwalking.
- **H2 Story Time** (`skittishShape` G; recast as Picket): Storybook
  Knight +1 (14) + Sleepwalker +1 (14) + 2 Shadow-Puppets +1 (8) = 36; a Bout start shape, the two shadow-sparring with no contact (8), re-enacting
  the tale (the Shadow-Puppets stand Posted, 6.4); a Picket Line once awake;
  breaks early.
- **H3 Knitting Circle** (`ledShape` G, Warband): Mother-Shape +1 (14) +
  Sleepwalker +1 (14) + Sleepwalker 0 (10) + Shadow-Puppet +1 (4) = 42;
  Retinue, knitting (Busy, a Revel start shape). Herald Waver; reach the leader.
- **H4 Night Stable** (the elite room, Champion): elite Sleepwalker +2
  (40) + Pale Mare +1 (14) + Insomniac +1 (14) = 68; Champion Watch; the
  fearless Pale Mare watches.
- **H5 Nursery Den** (the lair, Den): Mother-Shape +2 (20) + Lullabist
  +2 (20) + 4 Shadow-Puppets +1 (16) = 56; a Heap start shape (a Crescent
  once awake); the Lullaby Glyph.
- **H6 Bedpost Watch** (Battery, Waylay): 2 Insomniacs +1 (28) +
  Sleepwalker +1 (14) = 42; Kill Box behind toy-fort walls; a crayon line
  on the door.

### 7.11 The Inverted Cathedral (M4)

No roster exists. A flagged request to the RS and enemies owners: 7
types; ≥ half non-fearless; ≤ 1 mindless; **one shover type only** (up
to 50% Void rooms); marksman, hinderer and skirmisher heavy; ≤ 1 flyer
per Void room; a lurker optional (NM-9). Shapes to build later: Rood
Line, Censer Procession (avoiding Void-adjacent squares), Tracery Loft
Waylay across a Void (only if the Void rule of 6.8.1 is relaxed for that
theme, a later decision).

---

## 8. Readability, art, animation and audio

**Pose grammar** (extends RC2; a request to the RS owner):

| Position | Posted idle | Facing |
|---|---|---|
| front | Braced, weight forward, weapon or shield across the body; each shield stays inside its own square, edge-aligned with a Phalanx neighbour's, the Interpose link shown by a thin shared outline VFX (after the check) | The watched Portal |
| back / overwatch | Weapon aimed down the lane; a crouch behind cover reads "covered" | The entry lane |
| flank | Low lean, turned 45° | The party's flank |
| ward / leader | Upright, emblem high, a gesture to the line | The line |
| watch | A whole-body pivot between elite and door, the role prop sweeping (a lantern arc); no head-only tracking (after the check) | Split |
| lurk | Hidden: the tell only (RC1 reserves the idle motion) | — |
| Busy | Activity loop; the role prop stays on the model in its RC2 orientation (a shield slung upright on the back, a bow across the lap still pointing forward); nothing is set on the floor (after the check) | Inward |
| Dormant | Lying, or slumped against an adjacent Low-piece edge, a breath loop; the role prop held in its RC2 orientation (a leader asleep hugging its emblem upright); the Dormant glyph stays (RC3) | — |
| "To Billet" | Ready-weapon-and-run, one per rig family | — |
| Broken | No unit motion (a break is not a morale state, RC3): the chip's frame splits and the formation outline flashes and dissolves, one VFX; the log "Line broken" (after the check) | — |
| Hedgehog | The Posted pose turned to the lost Portal | Portal |

**Rules:** banners are carried by leaders only (a planted standard would
be a rules piece, RC1+); no bells or horns in idles (RC7); props stay
still (RC1); Cowed and Routed animations never pass through doors (RC3);
the formation chip set is RC7 global skin; Spoor marks live on frames
and walls only. **After the check (lead's calls):** in every posture the
role prop stays attached to the model, inside its square, in its RC2
orientation, and nothing is placed on a walkable square (RC1+); the RS
role-ID and greyscale gates also run on the Busy and Dormant poses; **a
head or hood that turns to follow the party is reserved for lurker tells
in every theme**, beside RC1's idle-motion reservation; idle loops carry
no weapon trails or hit flashes, and a Bout is a same-type pair or a
solo shadow-spar loop played facing each other with no contact; units
never sit on pieces: seated poses are floor-socket poses against an
adjacent Low-piece edge; activity and tell audio follows 6.7.

**Per theme** (the art brief): the Gaol's ledger idles, dice still on
the desk, the Penitent's hood swivel as the only tracking idle; the
Kennel's gnawing and panting inside the crescent, the Ghillie's cape
twitch as the only foliage motion near walkable squares; the Wake's
bowed heads, lids horizontal, veils lifted on non-fearless types; the
Choir's water pouring from mouths in time, pew-back shields edge to
edge; the Archive's page orbit on the Scribe-Spider, the page-turn reserved
for the Crawler; the Carnival's juggling (units only), applause that
drops out on a downing; the Supper's pouring and carving, Footmen frozen
mid-bow, chairs tucked in; the Gut's membrane pulsing in step, no spores
on non-hinderers; the Heart's toys played sinister, never cute; a
knitting idle; cower and flee, never a pratfall.

**Production** *(illustr.; lead's call, after the check)*. **Rig
families:** biped, quadruped (hounds, the Carousel Horse), multi-leg
(spiders), amorphous (oozes, the Gutworm), object-mount (bait caches,
boxed-lurker hosts).
- **M2:** per rig family, **per-position Posted idles** (front, back or
  overwatch, flank, ward or leader, watch), a Dormant pose, a "lean on
  an adjacent Low piece" pose and "to Billet"; per type, its RC2 prop
  orientation in each; the F1 activities (≥ 3 per F1 theme) and 1–2
  bespoke activities per F2–5 theme (`animReady`, 6.7); generic loops
  only for the remaining F2–5 Busy activities, which stay undrawable
  until ready; one break VFX; one chip icon per battle formation (**17
  icons:** the 15 battle formations plus "Line broken" and "Facing
  the door"; the proposal's "19" read as "17 formations plus 2"
  is corrected after the check); per theme a Spoor-mark skin of the base
  mark and a bait-cache prop. Activity props are existing RT pieces
  (6.6.1), so segment 6 adds none.
- **M3:** the remaining activity loops; per-type variants of the
  position idles; the Ritual audio.
- **The `FormationRead` gate runs only after the per-position idles
  land.**

| Animation kind *(illustr.)* | Count basis | M2 | M3 |
|---|---|---|---|
| Per-position Posted idles | 5 positions × 5 rig families | 25 | per-type variants, about 60 |
| Dormant, lean-on-piece, to Billet | 3 × 5 rig families | 15 | — |
| Activity loops | F1: 3 themes × ≥ 3; F2–5: 6 themes × 1–2 | about 15–21 | about 30 more |
| Lurker reveal and counter-strike beats | 5 lurker types × 2 | 10 | — |
| Total | | **about 65–71** | **about 90 more** |

The proposal's "about 290 animations" came from the toy, which is not an
art count; this table, built from the rosters of 7, replaces it, and art
production owns it.

**Gates:** the `FormationRead` playtest at isometric zoom with the Read
card closed: players name **and use** the counter ≥ 70% *(illustr.)*,
run with the RS role-ID gate.

---

## 9. New mechanics considered

### 9.1 Table

| Id | Mechanic | Wave | Verdict |
|---|---|---|---|
| EP-4/6/7 | Formations, Billets, Keystone, breaks | M1 static; M2 full | **Adopted** (6.6) |
| EP-8 | Fairness bundle | M1/M2 | **Adopted** (9.2) |
| EP-9 | Activities and postures | M2 (art M3) | **Adopted** (6.7; **agreed, Q4**) |
| EP-10–12 | Cuckoo, Baited Room, Waylay with Spoor marks | M2 (the Baited Room tightened) | **Adopted** (6.8) |
| EP-15 | Hedgehog re-form | M2 | **Adopted** (6.21) |
| EP-17 | Hold the Hide and the cover-room window | M2 | **Adopted** (6.9) |
| EP-19 | Core and growth positions; the solo column | M2 | **Adopted** (6.3) |
| EP-21 | Terrain Pairings | M2 partial; M3 rest | **Adopted** (6.6.8) |
| EP-23 | the Heap sneak line | M2 | **Adopted** (6.10) |
| EP-26 | Annex groups | M3 (Wager, Dare, boxed, Tattle) | **Adopted** (6.13; **agreed, Q2**) |
| EP-27 | Ritual (the Choir Tide) | M3 | **Adopted** (6.6.7) |
| EP-28 | Encounter Read card, overlays | M2 | **Adopted** (6.20) |
| EP-29 | Class-answer matrix | M2 | **Adopted** (6.16) |
| EP-35 | Messenger: a skittish runner with a satchel runs for help, Rousing a target group to Posted (no join, no Watchful) | M3 | Deferred; needs an FL-12 retreat-anchor exception and a new Dormant→Stationary route in ENM 12.1 (flagged) |
| EP-36 | Call Post: a bell rope or gong that sounds noise 10 within joiner rules; Muffle out of combat | M3 | Deferred until TR-15 Furnishings; one call source per encounter |
| EP-37 | Alarm Order: sleepers wake one by one by distance, damage or adjacency | M3 | Deferred; per-member awareness is a core change to ENM 12 |
| EP-38 | Errant: an out-of-depth elite walking a side loop, telegraphed from floor entry | Later | Its "deaf to the din" changes decided MAIN 4.3 joining; client only |
| EP-39 | Captive: a chained non-combatant behind a formation (Parting Word, Pledge) | Later (reserve `captiveSocket` now) | With CMP 7.12 Pledges; **new lead's call:** it uses CMP's own "captive" (the same concept), not a new name. Segment 8 hosts the Pledge wanderer and shrine sources and a freed captive's Testimony (EV-41, a request to the companions owner; Later; events design 4.8); the events design declines Model C's M3 Parting-Word Captive, so Pledges stay one Later package |
| EP-40 | Standoff / Riot: Gaol Jailers vs Inmates, a three-sided fight | Later | ENM 17.4 Feud is Later; a three-sided 6.4 enumeration |
| EP-41 | Approach band out of combat | M3 | A DH 17.3 change; a later client decision. Also records the 2024-style surprise as a Later fallback, only if `SurpriseDeathShare` misses |
| — | Rumour floor meter; the Prowl hunter | — | **Rejected** (FL 9.1; toy: overshoot, then inert) |
| — | Stand To / Uneasy (posture on hearing) | — | **Rejected** (MAIN 4.3 "beyond the cap do not change state") |
| — | Hidden whole groups (Tier C) | — | **Rejected** (0.1; contested by the lead) |
| — | Rigid backline leash | — | **Rejected** (toy: −47% fight cost; contested by the lead) |
| — | Watchful-start guard posts (Model A's alternative) | — | **Rejected** (ENM 12.1, 17.2) |
| — | "Busy −2" for Busy groups | Later lever | Only with `PartySurpriseShare` headroom |
| — | Anti-trickle wait rule | — | **Rejected** (invariant 7) |
| — | `JoinBudget` / join chance | — | Not proposed (MAIN 4.3); client only |

### 9.2 The fairness bundle in full (EP-8; proven in V12 at build time on the worst case and re-checked at generation time, 11.2)

| Rule | Statement *(thresholds illustr.)* | Wave |
|---|---|---|
| `FirstSight` | ≥ ⅔ of non-lurker units in line of sight of at least one landing square on entry (after the check); the Waylay exempt (announced by Spoor and Listen) | M1 |
| `LandingShooters` | Ranged attackers with line of sight to the **entry lane** at fight start: 1/2/2/2/2 (F1–F5); 3 only at party 4 outside the lair and the last G; solo −1 (min 1); Waylay on F2: 1 (6.8.6; cross-document comparison 2026-11-23, CC-102) | M1 |
| `StartArc` | Every start (lurkers included) inside 120° from the landing centroid, bisected by the line from the landing centroid to the start-zone centroid (after the check), on F1–2 or at party ≤ 2; 180° otherwise | M2 |
| `BacklineReach` | For every back, overwatch, ward or leader unit, a path from the landing avoiding squares adjacent to front starts, ≤ shortest + 4 | M2 |
| `LeaderReach` / `AuraCover` | The leader reachable from any CV8 flank square (after the check) in ≤ 6 without entering more than one front unit's reach; starts within 2 of ≥ 2 own members; never nearest to the landing | M2 |
| `CounterPresent` | Every predicate in the formation's `counter.guarantees[]` holds in the room (the closed enum of 6.6.6: `BacklineReach`, `LeaderReach`, `ShoveOutSquare`, `SecondPortal`, `FlankPair`, `TallWithin3OfLanding`, `CV8FlankSquare`); text-only counters are unproven | M2 |
| `CounterCost` | For the Waylay: the unwatched way in costs ≥ 1 named thing (6.8.6); checked at generation time | M2 |
| Kiter doorway lane | Every back or overwatch Billet includes ≥ 1 square, ≤ 1 step from the start, with line of sight to the landing, so a shooter can answer a door-camper | M2 |
| LOS breaker | Every overwatch or Crossfire host has ≥ 1 Tall piece or Drape within 3 of the landing, off the Clearway | M2 |
| `LurkBlind`, `AmbushStandoff`, `TellSight`, KP5 | 6.8.3 | M2 |
| Approved, made constructive | `Floor1NoCoveredStarts`, CV8 (extended to fall-back squares), LG20, VP3, VP6, the shover and flyer caps, Stir footprints, Tide states, Kegs (KP5), TP2, ≤ ½ in device areas, the Choir water half, the Carnival's no oil near entry, the Kennel leader's visibility | M1/M2 |

---

## 10. Numbers and fixes (illustrative; toy, not simulator)

The toy (scripts in the lead's scratch area; not project files) used
FL-sized rooms, a one-step AI with the Billet term, morale, the four
classes, approved budgets and caps, and 800 runs per arm on the expected
party path. **Enemy damage was scaled by K = 0.14–0.59 to hit the DH
18.2 targets** (the toy hero has no limited abilities or consumables),
so **read deltas, not absolutes**; the win-rate CI is about ±3.3 points.

| Arm | Win | F1 death | F5 death | Deaths on F3–5 | Elite+boss share of F3–5 deaths |
|---|---|---|---|---|---|
| Base (approved, role-blind) | 31.8% | 2.9% | 38.2% | 79.5% | 54.6% |
| A zones | 28.2% | 3.1% | 44.7% | 80.5% | **49.4%** (misses ≥ 50%) |
| **B formations** | **34.0%** | **2.2%** | 36.7% | 80.5% | **61.2%** |
| C living floor | 30.8% | 3.5% | 39.1% | 76.4% | 59.1% |
| B rigid backline | 47.5% | 2.9% | 26.9% | 72.9% | 75.2% |
| Tier C hidden groups | 29.9% | 2.2% | 42.1% | 81.1% | 61.1% |

| Finding | Fix |
|---|---|
| Formations +14–16% HP-eq per fight; Busy start shapes give it back | Adopted as intended |
| A rigid backline halves fight cost | Rejected; soft Billets |
| Breaks at median round 1 (front lost, "flanked" almost at once in 10–12-wide rooms) | The tightened line-crossed trigger (6.6.4) **plus** the Stung rule and the doorway lane, so later breaks do not open a plinking window; re-measure, gate 1.8–2.8 |
| Clock: B −0.04 to −0.16 rounds per fight | Invalid after the break fix; re-measure before M2 sign-off (6.18) |
| Waylay weaker than control (0.150–0.162 vs 0.179) | The geometry gate, band control +5% to +15% (6.8.6) |
| Lurker ambushes void against trained parties | A misread of "marks it"; re-run E3 with the tell marked and noticing per 12.2 (6.8.4) |
| The per-side reading removes ~⅓ of the ambush death excess | Adopted (Q1) |
| The Cuckoo at 85% cheaper than control | The Cuckoo at full band (EP-10) |
| F4–5 round-1 death 1.8–3.4% at K = 1 in **control** fights (max fills at +2) | The 6.4 rows on placed geometry; `LandingShooters` 1/2/2/2/2; +2 shooters ≤ 925‰; flagged to the enemies owner as a pre-existing 6.4 gap (EP-30) |
| `LandingShooters` 1/2/2/3/3 never bound | Tightened and measured on the entry lane |
| Rumour Astir on 68–77% of floors; Prowl 0.001 joins when tuned | Rejected |
| `ChainPullRate` 0.65–1.38 on F2–5 | Segment-6 join levers (6.12) |
| Template repeats 18–39% unenforced | A V12 hard assertion |
| Solo 2.5× harsher; formations +11% solo | The solo column, `billetScale` 0 for a solo hero on F2–5 (500‰ on F1, after the check), front-standard only, no solo ambush form |
| Arcanist HP-eq +20% under formations | The LOS breaker, the fall-back window, the `ClassHpEqDelta` gate |
| Party-initiated surprise 3–15% per fight; monster-initiated ~0% | `PartySurpriseShare` binds (the Shade table); the ratio a report only |
| Healing (out of scope, for the DH owner) | Approved rests alone needed K ≪ 1; the ENM 20.3 per-fight costs imply ~0.8–1.1 HP-eq of extra healing per floor to reach "60–80% HP at the boss door" (told plainly) |

**Re-runs owed before release** (XB-L2): every toy delta at K = 1 with
real kits; E3 with the correct marking reading, the per-band lurker
rolls and the spring at 3 (6.8.3, 6.8.5); the clock after the break fix
and the contact-round `maxRounds`; formations × party size on the 6.4
rows; F1 at `billetScale` 500‰. **The catalogue corrections of 7 (K5,
S3 at F3, GU4, GU6, and those made after the check) are not in the toy**
(told plainly).

---
## 11. Data, validators and simulator (change EP-4, EP-32)

### 11.1 Records (C# rules core, JSON content)

```jsonc
// FormationDef (new table; ENM 17.4 gains a formations column; fields
// marked * were added after the check, lead's calls)
{ "id": "formation.door_line", "displayName": "Door Line",
  "kind": "battle",                                   // * battle | startShape
  "templates": ["template.line_and_bow", "template.picket"],
  "hostKinds": ["guardPost", "lair"],                 // * guardPost|lair|patrol|eliteRoom|bossEscort
  "themes": null,                                     // * e.g. ["theme.choir"] for the Ritual
  "ambushForms": null,                                // * e.g. ["waylay"] for the Kill Box
  "tierRules": [{"tier": 1}],                         // * [{tier, ifElite?}]; start shapes have none
  "minFloor": 1, "maxFloor": 5, "minPartySize": 1,    // * maxFloor; party gates read partyCountAtGeneration
  "states": ["Stationary"],
  "core":   [{"pos":"front1","roles":["soldier","brute"],"socket":["front"],"hold":1,"keystone":true},
             {"pos":"back1","roles":["artillery"],"socket":["overwatch","back"],"hold":1,"fallBack":"coverBehind"}],
  "growth": [{"pos":"front2","roles":["soldier","brute"],"socket":["front"],"hold":1,"minWeight":23}],
  "keystoneRule": "any",                              // * any | last (the Ritual's singers)
  "breaks": ["keystoneLost","leaderFalls","lineCrossed","demoralised","thirdDown","maxRounds:3"],
  "counter": {"text":"Go around: the side lane reaches the bow.",
              "guarantees":["BacklineReach","ShoveOutSquare"],   // * closed enum (6.6.6)
              "unproven":[]},
  "answers": {"shade":"backlineLane","vanguard":"shoveKeystone","arcanist":"areaOverLine","herald":null},
  "defensiveVariant": {"facePortal": "lost", "billets": "keep"},   // * (6.21)
  "wave": "M1" }

// A start shape (* after the check): start squares only
{ "id": "shape.revel", "displayName": "Revel", "kind": "startShape",
  "postures": ["Busy", "Dormant"], "gorgedFor": ["template.pack", "template.sleeper"],
  "seat": {"socket":["table"], "requiresPiece":"theme"}, "wave": "M2" }

// ActivityDef (theme tables)
{ "id": "activity.gaol.dicing", "state": "Stationary", "posture": "Busy",
  "start shape": "shape.revel", "requiresPiece": "Low:desk", "listenWord": "dice",
  "idleAnim": "gaol_dice", "animReady": true, "bark": "idle",
  "forbidOn": ["champion","waylay","bossCourt"] }

// ENM 19.1 group record gains (6.13): encounterId, linkedGroupIds[], formationId,
//   startShapeId, activityId, posture, watchedPortal?, openZone[]?, ambushForm?,
//   tellSquare?, spoorPortalId?, pairings[], defensiveVariantId, billetScale,
//   nearPairPartnerGroupId?, callSourceUnitId?, units[].billet, units[].keystone,
//   units[].facing, units[].leaderId?, units[].wardOf?, annex?, captiveSocket? (reserved)
// Type data gains allowedAwareness[] (Q4)
// placementRules[] (RS 8.0) gains: formationWeights{}, activities[], spoorSkin,
//   baitCacheProp, ambushWeightsF1
// EncounterPlan (per floor): ambushCount, ambushAlloc[{form, hostSlot}], annex?{purpose, room},
//   tattleBias[], fillTargets[], drawLog; run state carries mousetrapsThisRun
```

`tier` in `tierRules` holds 1–3 for FT1–FT3 (a data field, not the
spell tiers); `ifElite` may raise it for a rule that needs it.

### 11.2 V12 `EncounterPlacement` (change EP-32)

V12 runs in three parts (lead's call, after the check: room geometry
also depends on per-seed inputs, the optional RT sockets, the `traps`
pass's triggers and device areas, Stir footprints, Tide states, door
start states and the actual composition, so a build-time result cannot
simply be looked up at runtime).

**Phases and classes (seeds and validation design 5.1, 5.2; change
SV-20; M1 static + chain, M2):** the build-time part is **V12s**; the
geometry re-check is **V12g** at GS12; the floor assertions are
**V12g** at AP2; the runtime assertions are **V12r**; tests are
**V12t**. `s` and `t` parts fail the build; `g` parts act by their
class: `stageRetry(encounters)`, then `waivable(V12FloorFallback)` for
the waivable set and `neverWaived` for the never-waived list (6.1). The
simulator gate below is not a validator part: it moves to the report
registry as SHIP or REL rows owned by EP, keeping its id as an alias
(seeds and validation design 6.3, 6.10).

**Build time** (enumerated per layer × orientation × template ×
formation × variant × party size 1–4 × reachable weight × floor band ×
theme, pruned by floor gates; **on the worst case**: every optional
socket off and every device-area square excluded, or per on/off mask
where the worst case is too strict):
- socket-class coverage for core positions; `FormationSurvivesFill`;
  growth positions inside template slot ranges;
- `FirstSight`, `LandingShooters`, `StartArc`, `BacklineReach`,
  `LeaderReach`, `AuraCover`, `CounterPresent`, the Kiter doorway lane,
  the LOS breaker, `LurkBlind`, `AmbushStandoff`, `TellSight`, KP5, the
  watch socket;
- fall-back squares and their CV8 flank squares; CV8 and
  `Floor1NoCoveredStarts` over the derived start sockets (6.6.1); no
  Billet within 2 of a Void edge or lethal hazard; no start on the
  Clearway;
- VP3, VP6, the shover and flyer caps, `Floor1NoCoveredStarts`, Stir
  footprints, Tide states, the Choir water, the Carnival oil.

**Generation time, geometry re-check** (on the actual seed, cheap path
and line-of-sight checks): the build-time geometry predicates for each
placed group, `CounterCost` (6.8.6), `TableSocketPieceExists`,
`AnnexHearingGap`, `ChipNoLeak`'s inputs and `ActivityHasAnimation`.

**Simulator gate** (a report-registry row, not a V12 part; SV-20, SV-24;
11.3, 11.4; per template × formation × floor band
on sampled layers, not per case): **the ENM 6.4 rows over placed
geometry with Billets on** (EP-30), including Keg, Charged Water and Stir acts
and shove-success × fall value for shover positions within 4 of a Void
edge; the Waylay HP-eq band.

**Generation time, floor assertions** (after `trapsResolve`, at the
shared assertion point, **AP2**, in its code-fixed order after V11g2;
seeds and validation design 5.4; change SV-9):
- the ENM 10.3 caps with the ambush definition; Baited Room ≤ 1 per floor,
  ≤ 2 per run, party ≥ 2, never F1;
- the ENM 10.4 guarantees, the awareness mix after step 10, and variety,
  plus no (template, battle formation) repeat, each battle formation ≤ 2
  per floor, ≥ 2 activities, the Busy caps;
- state ∈ the group's legal state set (6.4); activity legal for the
  template; start shape and battle formation legal for the template
  and host kind; every catalogue composition a legal template fill
  (`TemplateLegal`); no solo ambush form on F2–5;
- `FirstFightSimple`; hard-feature spacing on the first fight host; the
  tier ladder by floor;
- the `StealthOpenable` floor and ceiling; class showcases per floor
  (F1–3);
- the floor-1 package (6.17) and `SeedHasTarget`; per-theme eligibility;
  `AcidSourcesAfterOverlay`; one shover type in Void and clock rooms;
  signature ≥ 2; Pack one family; Heart shapes; the Wake `nearStart`;
- trigger-free starts; ≤ ½ in device areas; the Keybearer, Tattle, Annex,
  Wager and boxed-lurker bindings; `JoinedRangedShare`; `JoinedUnitsCap`;
  one call source per encounter; `ProtectedIntact` (its group part;
  the structural part is FL's V8g at AP1; pending request below); the
  cover-room
  window; `AnnexHearingGap` > 8; roster placeability (a content test).

**Runtime assertions:** `ArrivalThroughPortal`; a Billet never applied
after a break; no Billet bonus on a non-acting candidate when an attack
is available.

**Failure policy** (6.1): per slot, the fallback chain, ending in a
role-blind Picket that keeps every hard constraint; per floor, the 10g
repair pass, one retry of segment 6 on the keyed child
`…/attempt/{a}/encounters/retry1` with `trapsResolve` re-run, then the
terminal floor fallback with its waivable and never-waived assertions
and the `V12FloorFallback` report (lead's call, after the check). Every
retry logged (`StageRetried`, `GenerationFallback`; the ladder LR1, LR2,
LR4 and LR5 of 6.1; seeds and validation design 5.5; SV-10).

**Pending requests from the seeds and validation design (segments
11–12, approved 2026-11-23; change SV-20; M1/M2; the EP owner rules):**
- RS V7's signature-type assertion moves to **V12g** (signature ≥ 2,
  above), and its "fixed seed sample per (theme, floor, party size)"
  becomes a **V12t** regression row, `SignatureTypeTwice`, over the
  release seed set. Fallback if refused: V19t runs the sample row.
- `ProtectedIntact` is split: the structural part is FL's (V8g at
  AP1, `floorRetry`); the **group part** (step-9 group moves) is V12g at
  AP2, `stageRetry(encounters)` then `neverWaived`. Fallback if refused:
  FL owns both parts, the group part run at AP2 under V8g with the same
  class.
- With the companions owner, confirm `content.CMP8.Formations` and
  `content.CMP8.SoloColumn` (the CMP-8 tests extended to formations and
  the solo column, 13), owned by CMP over EP's data, run with CMP 11.2
  at tier 1. Fallback: the rows run as 13 words them, under CMP 11.2's
  suite.
(Seeds and validation design 5.2, 5.4, 5.7, 11, 14.2.)

### 11.3 Reports and gates *(illustr.)*

| Report | Target |
|---|---|
| `PartySurpriseShare` (worst-legal Shade-in-party profile) | Within the Shade `stealthShare` assumptions (35% solo / 20% party); **binding** |
| `SurpriseDeathShare` | < 5% of hero deaths; 0 on F1 |
| `AmbushDeathShare` | ≤ 15% |
| `AmbushFiredRate` (trained / untrained) | ≥ 10% / ≤ 50% |
| `AmbushTurnLossByPartySize` | No size > 50% of party-Surprise rounds |
| `XpPerRisk` by template and form | No form > 10% cheaper than a control guard post |
| `WaylayListenRate`, `KillBoxCounterUse`, `WaylayRound1Damage` | Reported; counter use ≥ 40% |
| `BacklineReachRate` | 40–70% |
| `FormationBreakRound` | Median 1.8–2.8, unbroken fights counted at their last round, the censored share reported; the party engages the line ≥ 1 round |
| `RoundsPerFightDelta{segment}` (the segment-6 line beside TL's; the keyed family, seeds and validation design 6.10, SV-24) | ≤ +0.2; no net minutes at party 4 on F4–5 |
| `DoorCampShare`: the share of posted fights in which every party unit ends ≥ 2 consecutive turns on landing or entry-lane squares while no party unit is adjacent to an enemy | ≤ 15%, with an HP-eq delta ≤ the Busy discount (a gate, after the check) |
| `ClassHpEqDelta`: per-hero HP-eq lost, B arm − Base arm, by class | An absolute spread: max − median ≤ 0.03 HP-eq per fight *(illustr.)* (after the check; a ratio breaks when the median delta is ≤ 0) |
| `StealthOpenableShare`, `RevelOpenerDamage`, `LairSneakRate` | ≥ 2 and ≤ 3 per floor; Revel fights median ≥ 2 rounds; ≤ 25% |
| `ChainPullRate`, `JoinerWarningLead` | 0.3–0.6; ≥ 1 always |
| `FormationEntropy`, `TemplateEntropy`, triples per floor | ≥ 1.5 bits; ≥ 4 |
| `FormationFallbackRate` | < 10%; < 5% on F1 |
| `V12FloorFallback` (floors shipped with a waived assertion) | < 1%, never a never-waived one; **read pooled** per (floor, cap), never per cell (BUDGET; seeds and validation design 5.6; SV-11) |
| `ReEngageRate`, `AnnexEngageRate`, `ContainersOpened` | Reported (containers −10% against baseline = the paranoia alarm) |
| `F1FeatureAdjacency` | Reported |
| `FormationRead` (playtest) | Name and use the counter ≥ 70% |
| `PatrolFightRate` (change XI-6; agreed, exploration and information Q2; cross-document comparison 2026-11-23, CC-106; M2): combats per floor, F1–5, whose first noticing roll was made by a Patrolling group, over the same count in the BASE arm (`XI.BASE`; seeds and validation design 6.2; SV-22) | ≥ 60% *(illustr.)*; **a gate**, owned by EP, read at M2 sign-off and every balance pass; on a miss, segment 10's cut order (exploration and information design 4.8.1, 9.3) |

**New lead's call:** `RoundsPerFightDelta` is TL's report name (TL 6.9,
≤ 0.1 for traps); segment 6 reports its own line under the same name
with a `segment` key, so the two allowances stay separate and additive.

**Lever order on a miss:** the Waylay share → the Baited Room weight → the
Crossfire / Kill Box floor gate → `billet%` and `maxRounds` → only then
the approved DH 18.2 levers (the F1 ambush and lair budgets). Read
(SV-26; M2): one lever at a time with its blast radius rerun; the F1
ambush and lair budgets, shared with DH 18.2, are applied once, by the
owner higher on the precedence ladder PR0–PR5 (seeds and validation
design 6.9).

**Result classes and reading (seeds and validation design 6.3, 6.4,
6.6, 6.12; changes SV-23, SV-24, SV-25; M2):** every row above carries
one class. `PartySurpriseShare` and the segment-6 clock line
(`RoundsPerFightDelta{segment}`, EP-33) are **BIND**;
`PatrolFightRate` is **REL** (PR1); `FormationFallbackRate` and
`V12FloorFallback` are **BUDGET** (with LR2 `encounters` ≤ 3% of floors
at ρ = 0.3, 5.6); `FormationRead` is a human-session row (H; the
playtest calendar of 6.11, SV-27); F1 `SurpriseDeathShare` = 0 is a
zero gate, two **BUILD** rows: the structural V12 F1 package (EP-2) and
the simulator smoke. The other rows keep their approved meaning and
take their class in the report registry (EP owns the thresholds). Each
row declares its sidedness and z by its class (6.4); `ClassHpEqDelta`
is read de-noised; the `PatrolFightRate` ratio uses a bootstrap
interval.

### 11.4 Simulator arms

Base (approved role-blind; the qualified toggle `EP.Base`); A (the M1
static zones); B (full); B-rigid;
B with per-group surprise; B + Baited Room; the Waylay geometry arm; the
solo arm (F2–5); the F1 `billetScale` arm (0 vs 500‰); the door-camp
arm (`DoorCampShare`); the Shade-in-party worst-legal arm
(`Skill.WorstLegalShade`); K = 1 with real kits for every arm.
Qualified names (seeds and validation design 6.2; change SV-22; M2):
every arm is a vector over the shared catalogue's axes, so "Base"
never collides with another owner's baseline; `PatrolFightRate` reads
`XI.BASE`, not `EP.Base` (11.3).

---

## 12. Staging

| Wave | Contents |
|---|---|
| **M1** | EP-1 pipeline and stream; EP-4 static formations (Picket Line, Door Line and Crescent, and the Patrol File start shape) with front and back sockets, no AI term; the derived start sockets; `FirstSight`, `LandingShooters`; constructive TP2 and the Keybearer candidate guarantee; EP-2 recorded (M1 has no lurkers, so no party Surprise); the solo table with its formation column; the V12 core with the fallback chain (V12s static and the chain; the `encounters/plan` and `/place` keys, the LR2 `retry1` child and `StageRetried` logging; `h:lurkStealth` registered as a reserved, inert domain, used from M2; seeds and validation design 4.3, 5.2, 5.5; SV-2, SV-6, SV-10, SV-20) |
| **M2** | Billets, Stung, fall-back, Keystone, breaks; the fairness bundle; activities (F1 themes ≥ 3; 1–2 bespoke per F2–5 theme, `animReady`); per-position Posted idles per rig family (8); the remaining formations and start shapes except the Ritual; the Cuckoo and the Baited Room (after DH 21.1 step 1); the Waylay with Spoor marks (after its gate); Hold the Hide and the window; the Hedgehog; the rest preview; arrival arrows; Terrain Pairings on approved rules; the Heap sneak line; the tier ladder and variety assertions; the Read card and overlays; the class matrix; join levers; per-theme eligibility; V12 full; reports |
| **M3** | The Annex (Wager guard, Dare garrison, boxed lurker, Tattle beyond; until then every Tattle is a Crier, told plainly); the Ritual; Messenger, Call Post, Alarm Order (if per-member state is cheap); natives in device areas (after TL-16); TR-15 pairings; `roleHint`; the full activity art; two-group "watched sleep" rooms on F4–5; the approach band (client) |
| **M4** | The Cathedral roster and shapes |
| **Later** | Errant; Captive with Pledges; Standoff/Riot; Rival Party fit; the "Busy −2" lever; the 2024-style surprise fallback; any Rumour-like escalation only by explicit client override of FL 9.1 |

---

## 13. What segment 6 owns, and hooks handed to later segments

**Owns** (FL 13): ENM 10.5 steps 2 and 4–11; `placementRules[]`
application; the formation, start shape and activity tables; the start-socket
derivation; start squares, Billets, Keystones; near-pair partners; ambush allocation; the Annex; the
`GroupPlacement` hand-off; V12; the Rival Party fit stub.

**Does not own:** budgets, bands, caps and XP (ENM); scorer weights (the
enemies owner rules EP-6); layout geometry, the Circuit and Listen (FL);
sockets and pieces (RT); hazards (HZ); traps, locks and `trapsResolve`
(TL); the display (segment 10).

| Segment | Hook |
|---|---|
| **7 Loot** | Carriers on sockets the party can reach for Steal (never a nook; never a ward behind two fronts on F1–2); the Champion notable; the bait-cache prop family (CT1, LP-15; re-skins LP-16 pending; loot sources design 5.1, 5.3); activity containers (a dice pot, a feast plate); Annex prizes (none of its own: loot economy design 6.9 item 1); boxed-lurker contents (loot economy design 6.9, LE-20). **Taken up by the loot sources design (segment 7c, approved 2026-11-22; its 4.4, 4.8, 5)** |
| **8 Events** | Activity vignettes in Q rooms ("remains of a revel"); no event spawns or moves units; Cowed kneeling never crosses a door. **Taken up by the events design (segment 8, approved 2026-11-22; its 4.13):** vignettes are Glimpse dressing beside the Q room's Curiosity, under RC1+, never a second decision (M2); "no event spawns or moves units" is held: Curiosity noise is generic noise that moves units only through the hearing rules, never a Lure and never sets `luredThisFloor` (EV-14, a reading pending the EP owner's confirmation, with the enemies owner; M2); figures are framed non-unit map objects, never on a walkable square (events design 4.11; EV-45; M2 implied figures, M3 frame); M3 Overtures may apply Soaked, Oiled or Revealed to a tied group at its fight's first round, never on the elite, the Annex, the boss or a lurker host (for Revealed) (EV-32, a reading of this row pending the EP owner, enemies owner request; M3; events design 4.9) |
| **9 Boss arenas** | Retinue escorts, arena start zones ≥ 9, the patrol joiner, Souvenir dressing. **Taken up by the boss rooms design (segment 9, approved 2026-11-22; its 4.5, 4.7.4):** arena escorts use segment 9's `ArenaFormation`s, Retinue one allowed shape on F2–5 (BR-19, a request pending the EP owner, 6.14; M2); every F2–5 Boss Court holds ≥ 1 non-fearless group (agreed, boss rooms Q4; cross-document comparison 2026-11-23, CC-106; M2); `answers{}` extended to bosses as `BossAnswerMatrix` (BR-19, 6.16; M2); reserve sockets read as keeping "no off-map reinforcements" and `ArrivalThroughPortal` (BR-11, a reading pending the EP owner's confirmation, 6.12; M2); the patrol joiner joins under the 1/1/2/2/2 caps and counts in AddCap (BR-13, an FL-21(e) proposal the enemies owner rules) |
| **10 Information** | Formation and Keystone chips (`ChipNoLeak`; the read-surface table of 6.20); the Read card; the Perception hold overlay; the Lore counter line; the Listen activity word and watched Portal; Spoor marks; arrival-Portal arrows; the seen-tell band marker (6.20); rest-preview movers; the Lore roster preview of formations; "Line broken" and "Facing the door". **Taken up by the exploration and information design (segment 10, approved 2026-11-23; its 4.3.5, 4.8, 4.9.7, 5):** every surface placed in its catalogue (5), EP 13 at 5 surfaces; the Listen bubble carries the activity word and watched Portal if EP-9 and EP-12 are ruled (FL-18 requests; 4.3.5); hold squares and fall-back stay the EP-28 tier, if ruled; arrival-Portal arrows on a hearing outline labelled "known groups only" (XI-13, a reading; M2; 6.12); rest-preview movers read `PatrolForecast` over known patrols (XI-6, agreed, exploration and information Q2; M2; 6.20); the `PatrolFightRate` gate, owned by EP (XI-6, agreed, exploration and information Q2; M2; 11.3; cross-document comparison 2026-11-23, CC-106); Spoor marks projected under cutaway (XI-18, pending the EP owner's confirmation; M2; 6.8.6); the noticing step tag's odds and Cased's odds band wait on EP-41 (XI-12, XI-27; M3; 6.20); Downed units excluded from the lurker band (XI-2; M1; 6.8.5) |
| **11 Seeds** | The `encounters` node, its draw table, the `retry1` child, the per-band `lurkStealth` keys. **Taken up by the seeds and validation design (segments 11–12, approved 2026-11-23; its 4.3, 5.5):** `…/attempt/{a}/encounters` a node with the sub-keys `plan` (GS8) and `place` (GS12), counts unchanged (SV-2; M1); `encounters/retry1` the LR2 child (SV-10; M1); `h:lurkStealth` (groupId, positionIndex, band, die) on the floor's `checks` stream (SV-6; M2, reserved in M1); the key algebra (SV-1); 6.1, 6.8.5, 6.19 |
| **12 Validation** | V12, the reports, the CMP-8 tests extended to formations and the solo column. **Taken up by the seeds and validation design (segments 11–12, approved 2026-11-23; its 5.1, 5.2, 5.4–5.7, 6.3, 6.10):** V12 split into V12s, V12g (GS12 geometry; AP2 floor assertions) and V12r, its simulator gate moved to the report registry (SV-20); every retry logged as `StageRetried` (5.7); `V12FloorFallback` < 1% read pooled (SV-11); the CMP-8 rows as `content.CMP8.Formations` and `content.CMP8.SoloColumn` (a request to the EP and companions owners, pending; 11.2); signature ≥ 2 and `ProtectedIntact`'s group part in V12g (requests pending; 11.2) |

---

## 14. Binding requirements and how they are met

| Requirement | Source | Met by |
|---|---|---|
| Steps 2, 4–11; step 9 within host or equivalent | FL 13; ENM 10.5 | 6.1 |
| Ambush tells within 6; lurker placement | ENM 25 | 6.8.3 |
| A cover room on a patrol route, usable for a party ambush | ENM 10.4, 25 | 6.9 (the window, Hold the Hide) |
| Doorway-hold AI | FL-21 (c) | The Billet term (EP-6) |
| In-room sneaks past Den and Sleeper | FL-21 (d) | The Heap sneak line; `LairSneakRate` |
| Champion hold | FL-21 (b) | The elite's Billet offered |
| The patrol as a boss joiner | FL-21 (e) | The patrol never Kill Box, Crossfire or elite |
| The solo composition table, `SoloBudgetMonotone` | ENM 10.1; CMP 7.7 | 6.3 |
| Every guard at every weight | CMP-8 | V12 build-time 6.4 on placed geometry |
| Placement rules, `nearStart`, Heart shapes, signature ≥ 2, Pack one family | RS 13 | 6.4, 6.5, V12 |
| Who starts where; KP5's ambush clause; nooks; the terrain user | RT 13 | 6.6, 6.8.3, 6.6.8 |
| Footprints, Tide states, shover and flyer caps, tags, candle racks, Dare garrisons | HZ 13 | 6.1 exclusions, 6.6.8, the Annex |
| TP2, device areas, the Keybearer, Tattle, Wager, boxed lurker, trap charge | TL 13; TL 6.22 | 6.13 |
| No solo surprise at index 0–1; no lurkers in rooms 0–2 | DH 17.1; invariant 6 | 6.17 |
| Surprise reconciliation before the "first turn" rows | DH 21.1 step 1 | A gate on every lurker ambush |
| F1 `SeedHasTarget`; CL-10 | CL 5.6 | 6.17 |
| Shade parity inputs | SH 10 | `PartySurpriseShare` fed to `stealthShare` |
| Floor-time gate | CMP 7.4 (**agreed**) | 6.18 |
| RC1–RC9 | RS 7.4 (**agreed**) | 6.7, 6.20, 8 |
| No floor-wide alarm | FL 9.1 | The Rumour and the Prowl rejected |
| Monsters never respawn; XP finite | ENM 16 | 6.21; the Annex at 0 XP |
| Rival Party fit | FL 13 | A Later stub (EP-34) |
| Determinism; fixed draws | MAIN 6.3 (**decided**); R-SEED-3 | The `encounters` stream; the keyed hash (6.19); registry rows under the one key algebra (seeds and validation design 4.1–4.3) |

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | Formations read as UI only (breaks too early) | Tightened breaks with Stung; the `FormationBreakRound` 1.8–2.8 and "used the counter" gates |
| 2 | Door-camping and plinking posted holds | The Stung rule; the Kiter doorway lane; `maxRounds` counted in contact rounds; the `DoorCampShare` gate |
| 3 | Clock overrun once breaks move later | The line ≤ +0.2 rounds / 0 net minutes; the cut order applied before release |
| 4 | The Waylay a solved toll gate or a dud | `CounterCost`; the HP-eq band; "Facing the door" |
| 5 | The Arcanist as the formations' victim | The LOS breaker; fall-back windows; `ClassHpEqDelta` |
| 6 | Solo F2–5 too harsh | The solo column; `billetScale` 0; front-standard only; no ambush form solo on F2–5 (the Cuckoo included, after the check); a V12 assertion |
| 7 | The F4–5 round-1 row already broken at max fills | EP-30; the enemies owner rules the pre-existing gap |
| 8 | Stealth supply breaches Shade parity | The `StealthOpenable` ceiling; Hold the Hide once per floor; the Busy clump cap; `PartySurpriseShare` fed to the Shade table |
| 9 | Container paranoia | The bait-cache prop family; the `ContainersOpened` report |
| 10 | Generated sockets make ugly formations in odd rooms | V12 proves legality only; art and QA review per layer; `roleHint` in M3; `FormationRead` |
| 11 | V12 build-time cost (~1.1M cases), and per-seed inputs that a cached result cannot see | Prune by floor gates and template eligibility; cache per layer on the worst case; re-check geometry per seed; the 6.4 rows and the Waylay band as a simulator gate, not per case (11.2) |
| 12 | Art volume | M2 per-position idles per rig family, not per type; F1 activities first; activities drawable only when `animReady`; the M3 full pass (8) |
| 13 | Formations solved after a few runs | Terrain Pairings vary the counter by room; the counter line free only on first meeting; no-repeat |
| 14 | Many pending dependencies (DH 21.1, TL-16, TL-30, TR-15, FL-21, SH-21, the solo table) | Each gated feature names its gate; nothing ships on an unruled rule (19.2 names each fallback) |
| 15 | Lurker supply uneven; F5 has no lurker ambush; one legal ambush host on most floors, none on trimmed F3–4 | Told plainly; the Waylay carries F5; the boxed lurker fills the second place in M3 |
| 16 | Re-engage farming | The Hedgehog without Billets after losses; all Portals watched; the DH rests gate |
| 17 | **All evidence is toy** | Every number is owed a simulator re-run before it settles |
| 18 | The floor-level quotas fail on a seed | The floor allocation (9b), the repair pass (10g), one retry, the terminal floor fallback with `V12FloorFallback` (6.1); row order fixed (cross-document comparison 2026-11-23, CC-106) |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: fill-curve constants, socket-class
  thresholds, Billet weights by profile, the fall-back bonus,
  `maxRounds`, ambush weights and frequencies, the Baited Room per-run cap,
  the Waylay HP-eq band, `StealthOpenable` and its sneak threshold,
  `LandingShooters` exceptions, the Hold the Hide tick limit, K
  candidates, report thresholds other than the stated gates.
- Activity lists per theme within 6.7 (≥ 3 per F1 theme in M2).
- The solver's fixed role priority and square order, within 6.6 and
  ABL 8.1.
- Godot scene structure for chips, the Read card, the hold overlay,
  Spoor marks and posture idles, with no physics.
- Art, animation and audio inside the charter (8).
- Names, until the naming pass.
- **Not tuning:** only Dormant, Stationary or Patrolling starts, and no
  Watchful guard posts; no hidden whole groups; no generated rest or
  backtrack ambushes; per-side party surprise **(agreed, Q1)**; only the
  Baited Room takes the party's turn, never on F1 or against a solo hero;
  a floor-1 lurker never uses Ambush Strike; no solo ambush form on F2–5;
three lurker Stealth rolls; Billets lose to invariant 7
  and to the Stung rule; public breaks; every formation's counter proven
  to exist; no unit on the Clearway; the Annex at 0 XP, ≤ 1 per floor,
  F2–5 **(agreed, Q2)**; a seen tell in the danger band **(agreed,
  Q3)**; awareness inside `allowedAwareness[]` **(agreed, Q4)**; no join
  chance; never scaled by party size beyond positions; build-time proof.

---

## 17. Changes to approved documents

Applied on 2026-11-22, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the owners of
the sections they touch **(lead's call)**, and rows owed by other owners
are listed again in 18. Every row names the exact section it touches.
Rows that touch **decided** or **agreed** text are flagged in the "Flag"
column; **the client's approval of 2026-11-22 covers them** (with
Q1–Q4 where a row names one; the answers of 2026-11-21 already agreed
EP-2, EP-9, EP-14 and EP-26 in substance). **New lead's call:** as in
the TL precedent, the Flag column also marks rows that only *read*
agreed or decided text without changing it, so that every touch of
agreed text is visible; and EP-1 is flagged for extending MAIN 6.3
(decided), which the proposal had not flagged. Told-plainly rows were
told in 19.1 and confirmed by the client on 2026-11-22. Rows marked
"request" are requests to other owners, recorded as pending requests;
they change nothing until their owner rules, and 19.2 names each
fallback. M3, M4 and Later rows are recorded now as applied text marked
with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes):**
- ENM 12.1's "back at its post" reads "its Billet hold squares" (EP-15);
- `Floor1NoCoveredStarts` (RT) governs start sockets, not fall-back
  squares (EP-6; the RT owner confirms);
- KP5's "party start squares in ambush templates" reads "the entry lane
  plus the open zone" (EP-8);
- ENM 17.4's "trained Perception marks it" reads "marks the tell"
  (6.8.4);
- TL-16's "never stacks with Ambush" reads "at most one surprise opener
  per side per combat" (a reading for the TL owner, 6.8.7);
- ENM 17.3's Ambusher "waits Hidden" reads "until a party unit is within
  3 squares and in its line of sight, or its group or the bait is
  attacked or notices" (6.8.3, 6.15; a request to make it precise;
  narrowed after the check);
- RT CV8's "every enemy start socket" reads over the derived start
  sockets of 6.6.1 (a reading for the RT owner; after the check);
- Q2's "Dormant" Annex reads: the boxed-lurker Annex keeps its TL 6.14
  state; the guarded Wager and Dare forms are served by the Annex
  Dormant among the devices; TL 6.15's "full-strength group" reads "a
  patrol-band group" (6.13; after the check; told plainly);
- DH 17.3's reach for a seen tell is taken from the tell square, never
  the lurker's (6.20; after the check).

| # | Document / section (current status) | Change | Flag | Approval |
|---|---|---|---|---|
| **EP-1** | ENM 10.5 last paragraph (lead's call); **MAIN 6.3 streams (decided; extended like FL-2, TR-17, TL-3)**; RS 6.4 R-SEED-3 seed tree | Segment 6 draws from an `encounters` child of the floor's dungeon substream, after `traps` and before `trapsResolve`, with fixed draw counts (6.1) | **Decided** (MAIN 6.3, extended; new lead's call to flag) | Client, with this document; RS and segment-11 owners apply |
| **EP-2** | **MAIN 4.3 surprise (decided)**; ENM 12.2 Surprise bullet (settled); **DH 17.1 (agreed; read)**; DH 21.1 step 1 | Party surprise judged per side; monsters per group (6.8.2) | **Decided** (MAIN 4.3, clarified); **Q1** | **Client (agreed 2026-11-21, Q1)**, with this document; enemies and DH owners apply |
| EP-3 | ENM 12.2 "lurker Stealth … combat stream" (lead's call); ENM 10.5 step 10 | Keyed `lurkStealth` hashes, one per roll (placement, the 6 band, the 3 band), so ENM 12.2's three rolls are kept and only their stream changes (6.8.5; per-band keys after the check) | — (both lead's-call rows named) | Lead's call; enemies owner applies |
| EP-4 | ENM 17.4 (a formations column); ENM 19.1 group record | Template × formation × activity; record fields; `captiveSocket` reserved (6.6, 11.1) | — | Lead's call; enemies owner applies |
| EP-5 | RT 6.2.5 Socket (optional `roleHint`; request); RT CV8 and `Floor1NoCoveredStarts` (read: over the derived start sockets) | Derived start sockets and socket classes (6.6.1; the start-socket derivation after the check) | — | Lead's call; RT owner (hint, M3) |
| EP-6 | ENM 11.1 M2 row "Cohesion, body-block, area spread, hold"; 11.3 invariants 1, 4, 7, 10 (kept); 17.3 Holder, Kiter, Commander lines; RT CV8 (extended to fall-back squares); RT `Floor1NoCoveredStarts` (a reading: starts only) | The Billet term, the invariant-7 guard, the Stung rule, the fall-back square (6.6.3) | — | **Request; the enemies owner rules**; RT owner confirms the reading |
| EP-7 | ENM 13.1 (chips); 14.3 (read) | Keystone; public breaks (⅓ members) (6.6.4) | — | Lead's call; enemies owner applies |
| EP-8 | ENM 10.3 (added rules); RT KP5 (definition); FL LG13, LG20 (read) | The fairness bundle (9.2, 6.8.3) | — | Lead's call; RT owner applies KP5 |
| **EP-9** | **MAIN 4.3 "set per monster in data" (decided)**; ENM 12.1 (no new state); **RS RC1, RC3, RC8 (agreed; read)**; FL 9.2 and ABL 7.2 Listen rows (request: the activity word) | Activities and postures; `allowedAwareness[]`; state ∈ template Awareness (6.7) | **Decided** (MAIN 4.3, read per group); **Agreed** (RC, read); **Q4** | **Client (agreed 2026-11-21, Q4)**, with this document; enemies owner applies; FL and ABL owners (the Listen word, request) |
| EP-10 | ENM 10.3 ambush column; ENM 10.1 "an ambush uses 85%" → "a Mousetrap Ambush" (renamed Baited Room, naming pass NP-35); ENM 16 "ambushes pay … at 85%" → the same; ENM 17.4 Ambush row (container bait tightened: party ≥ 2, F2–5, ≤ 1 per floor, ≤ 2 per run; Band "Guard post × 0.85" → "guard post; × 0.85 for a Mousetrap"; Awareness "Hidden" read as "the lurker group Stationary and Hidden at start; the bait group Stationary"; the last two after the check) | The ambush defined; the Cuckoo at full band; the bait-cache prop (6.8.1) | — | Lead's call; enemies owner applies |
| EP-11 | ENM 17.1 lurker row; ENM 9.3 F1 bullet; ENM 11.3 invariant 6 (DH-28 lineage); **DH 17.1 (agreed; read, not changed)** | A floor-1 lurker never uses Ambush Strike; the F1 ambush only in the L-G role order (6.17) | **Agreed** (DH 17.1, read; tightened only) | Lead's call; enemies owner applies |
| EP-12 | ENM 10.3 (the Quarry rule extended to the Waylay); ENM 17.4 (the Kill Box as a formation); FL 9.2 (trained Listen adds the watched Portal; request); **RS RC1 (agreed; read: frame and wall marks)**; RS RC5+ pattern registry (the Spoor base mark and bait mark; after the check; extended to the 6.20 board read surfaces as a request to the RS owner, pending; cross-document comparison 2026-11-23, CC-103) | The Waylay posture, gates and Spoor marks (6.8.6) | **Agreed** (RC1, read) | Lead's call; enemies owner applies; FL owner (request) |
| EP-13 | FL 7.2 `F1LessonLadder` (the Gaol's guard post adjacent to the lair); FL 7.2 "Law in layout" bullet and the FL theme table's Gaol row ("1 shut-door pair"); RS 8.3 Law, Lockdown ("one shut-door pair") (the last three named after the check) | "The shut-door pair or the hooded cot" (6.17); on hooded-cot seeds the floor has no shut-door pair, and the Gaol's Law shows through its door network and bell tripwires (told plainly) | — | Lead's call; FL owner applies |
| **EP-14** | **DH 17.3 danger band (agreed lineage)**; DH 21.5 | A seen tell counts as a known threat, its reach taken from the tell square (6.20; the square after the check) | **Agreed** (DH 17.3); **Q3** | **Client (agreed 2026-11-21, Q3)**, with this document; DH owner applies |
| EP-15 | ENM 12.1 Wary row ("its post", a reading; Wary renamed Watchful, naming pass NP-35) | The Hedgehog re-form; no Billets after ⅓ down or the Keystone lost; watches all Portals (6.21) | — | Request; enemies owner rules the reading |
| EP-16 | ENM 11.3 invariant 5 (strengthened as an assertion); ABL 15.1 and the DH rest preview (request) | No generated rest threats; `ArrivalThroughPortal`; rest-preview movers (6.12, 6.20, 6.21) | — | Lead's call; ABL and DH owners (request) |
| EP-17 | ABL 7.2 (a new exploration row); FL 6.10 (an auto-walk entry); FL 6.7 `coverRoom` and RT KP4 (requests) | Hold the Hide; the cover-room window (6.9) | — | Request; ABL, FL and RT owners |
| EP-18 | FL 6.7 (read) | Patrol File order (6.9) | — | Lead's call |
| EP-19 | ENM 10.1 solo table and `SoloBudgetMonotone`; **CMP 7.7 (agreed, CMP Q1; read)**, 7.9, CMP-8 | Core and growth positions; the solo column; `SoloShapeMonotone`; `billetScale` 0 for a solo hero on F2–5 and 500‰ on F1; no solo ambush form on F2–5; gates read `partyCountAtGeneration` (6.3; the last three after the check) | **Agreed** (CMP 7.7, filled as owed) | Lead's call; enemies and companions owners apply |
| EP-20 | ENM 10.4 Variety; ENM 10.5 step 5 (weights); ENM 10.1 fill (inside the window) | Tiers, spacing, no-repeat, the fill curve (6.2) | — | Lead's call; enemies owner applies |
| EP-21 | HZ VP3, VP4, VP6, 6.11, 6.12; RT KP5, TR-15; TL 6.9 (read) | Terrain Pairings; no Billet near Void edges (6.6.8) | — | Lead's call; M3 parts on their owners' rulings |
| EP-22 | FL 6.6 (a request on caps only if needed); ENM 12.4 (read); **MAIN 4.3 joining (decided; read, not changed)**; ENM 17.2 Horn-bearer; TL TP8 | Join levers; one call source; `JoinedRangedShare`, `JoinedUnitsCap`; arrival arrows (6.12) | **Decided** (MAIN 4.3, read); told plainly | Lead's call; FL owner (request, only if needed) |
| EP-23 | ENM 17.4 Den and Sleeper rows; ENM 20.1, 16; FL-21 (d) | The Sleeping Ward sneak line (renamed the Heap, naming pass NP-35) (6.10) | — | Request; enemies owner rules FL-21 (d) |
| EP-24 | FL 11.2 `ProtectedIntact` (read, no change) | No unit starts on the Clearway (6.6.5) | — | Lead's call |
| EP-25 | TL 6.20 TP2 failure action (a backstop); TL 6.22; TL 6.11; `KeyReachable`; TP10 | Constructive TP2 and its draw-free re-place; the hand-off record; the Keybearer candidate guarantee, TL binding (6.13; after the check) | — | Lead's call; TL owner told |
| **EP-26** | ENM 10.4 "4–6 fights" (settled); ENM 16 (a 0-XP group); ENM 20.1; TL 6.11 Tattle (an Annex may be the group beyond); TL 6.14 Placement and budget; **TL 6.15 "it does not add one" and "a full-strength group" (agreed lineage; the latter re-read as "a patrol-band group" after the check)**; HZ 6.10 Dare garrison | The Annex group: one extra patrol-band budget, its priority, its binding and its readings (6.13) | **Agreed** (TL 6.15 lineage); **Q2** | **Client (agreed 2026-11-21, Q2)**, with this document; enemies, TL and HZ owners apply; M3 |
| EP-27 | ENM 17.4 formations column (the Ritual as a Warband formation, no new template row; corrected after the check); HZ 7.5 (read); RS RC7 (the "last singer falls" sting as a reserved cue; request) | The Choir Ritual; singers hold back the Tide (6.6.7) | — | Lead's call; M3 |
| EP-28 | ENM 13.5 "the one place" (lead's call, A-8); ENM 10.6 Lore preview; **RS RC7, RC9 (agreed; read)** | The Read card; the Perception hold overlay; the counter line free on first meeting (6.20) | **Agreed** (RC, read) | Request; enemies owner rules 13.5; segment 10 |
| EP-29 | ENM 17.4 formation column (answers); RS `placementRules` (fearless-anchor tags) | The class-answer matrix and showcases (6.16) | — | Lead's call; RS owner applies the tags |
| EP-30 | ENM 6.4 (rows enumerated over placed geometry); ENM 20.1 | 6.4 on placed geometry; `LandingShooters`; the F4–5 +2 shooter fill cap; the pre-existing gap flagged (6.2, 9.2, 10) | — (told plainly) | Request; enemies owner rules the gap |
| EP-31 | ENM 17.4 (the eligibility table as a content-test input); RS 8.10 Gut cap; HZ 6.11 | Per-theme eligibility; `AcidSourcesAfterOverlay`; one shover type; `TemplateLegal`; the Busy rule for mindless members (6.4; the last a new lead's call) | — | Lead's call; RS owner applies the Gut reading |
| EP-32 | ENM 10.5 step 8; ENM 20.1; RS 11.2 validator list | V12 and the fallback chain (6.1, 11.2) | — | Lead's call |
| EP-33 | FL 6.10 (a new segment-6 clock line beside TL-38's); **CMP 7.4 (agreed; levers untouched)** | The clock line and cut order (6.18) | **Agreed** (CMP 7.4, read); told plainly | Lead's call; FL and CMP owners |
| EP-34 | FL 13 | The Rival Party fit stub (6.14) | — | Lead's call; Later |
| EP-35 | FL-12 retreat anchors; ENM 12.1 | Messenger (flagged exceptions) (9.1) | — | Deferred; M3 |
| EP-36 | ENM 12.3, 12.4, 17.2; RT TR-15 | Call Post (9.1) | — | Deferred; M3 |
| EP-37 | ENM 12.1, 12.2 (per-member Dormant) | Wake Order (renamed Alarm Order, naming pass NP-35) (9.1) | — | Deferred; M3 |
| **EP-38** | **MAIN 4.3 joining (decided)**; ENM 12.4; ENM 10.4 elite slot | Errant (client only) (9.1) | **Decided** | Client only; Later |
| EP-39 | CMP 7.12 | Captive (9.1) | — | Later |
| EP-40 | ENM 17.4 Feud | Standoff / Riot (9.1) | — | Later |
| **EP-41** | **DH 17.3 ("only while `combatActive`")**; **MAIN 4.3 (the 2024 fallback note)** | The approach band; the surprise fallback lever recorded (6.20, 9.1) | **Agreed** (DH 17.3 lineage); **Decided** (MAIN 4.3, a note only) | Client, at M3 / Later |

Waves: EP-1, EP-18, EP-24 in M1; EP-2 recorded in M1, in effect in M2;
EP-4, EP-8, EP-16, EP-19, EP-25, EP-30 and EP-32 in M1 for their static
parts and M2 for the rest; EP-3, EP-5 (classes), EP-6, EP-7, EP-9 to
EP-15, EP-17, EP-20 to EP-23, EP-28, EP-29, EP-31 and EP-33 in M2; EP-5
(`roleHint`), EP-21 (pending parts), EP-26, EP-27 and EP-35 to EP-37 in
M3; EP-41 in M3 / Later; EP-34 and EP-38 to EP-40 Later.

---

## 18. Depends on systems not yet designed

The owed rows are the single list in 17; by owner:

- **The enemies owner:** EP-2 (12.2), EP-3 (12.2, 10.5), EP-4 (17.4,
  19.1), EP-6 (the Billet term, Stung and the fall-back square in 11.1
  and 17.3), EP-7, EP-9 (`allowedAwareness[]`), EP-10 (10.1, 10.3, 16,
  17.4), EP-11 (17.1, 9.3, invariant 6), EP-12, EP-15 (the "post"
  reading), EP-19 (10.1), EP-20, EP-23 (FL-21 (d)), EP-26 (10.4, 16,
  20.1), EP-27, EP-28 (13.5, 10.6), EP-29, EP-30 (the pre-existing 6.4
  gap), EP-31, EP-32; the Ambusher wait reading (6.15); FL-21 (b), (c),
  (e) as answered here.
- **The DH owner:** EP-2 (21.1 step 1 re-run on the per-side reading),
  EP-14 (17.3), EP-16 (the rest preview), EP-41 (M3); the healing
  finding of 10.
- **The ability model owner:** EP-9 (the Listen activity word, 7.2),
  EP-16 (15.1), EP-17 (Hold the Hide, 7.2).
- **The companions owner:** EP-19 (7.7 solo column), EP-33 (7.4,
  untouched levers).
- **The Shade owner:** `PartySurpriseShare` into `stealthShare[L]` and
  `openShare[L]` (6.8.8); SH-21 Lure (pending) as a counter.
- **The RS owner:** EP-1 (the seed tree), EP-9 (RC readings), EP-12
  (Spoor marks on frames and walls; the 6.20 board read surfaces in the
  RC5+ registry, a pending request; cross-document comparison
  2026-11-23, CC-103), EP-29 (fearless-anchor tags), EP-31
  (Gut cap), the pose grammar (8), the Cathedral roster request (7.11),
  roster placeability.
- **The floor layout owner:** EP-9 and EP-12 (Listen rows, FL 9.2),
  EP-13 (`F1LessonLadder`), EP-17 (FL 6.7, 6.10), EP-22 (FL 6.6, only if
  needed), EP-33 (FL 6.10).
- **The room terrain owner:** EP-5 (`roleHint`, M3; the start-socket
  reading of CV8), EP-6
  (`Floor1NoCoveredStarts` reading, CV8 extension), EP-8 (KP5), EP-17
  (KP4), EP-21 (TR-15).
- **The hazards owner:** EP-21 (VP rules, 6.11, 6.12), EP-26 (Dare
  garrisons), EP-27 (read; other singer themes Later).
- **The traps owner:** EP-25, EP-26 (TL 6.11, 6.14, 6.15), the
  opener-per-side reading (6.8.7); TL-16 and TL-30 (pending) as
  dependencies.
- **Dungeon generation segments 7–12:** every hook in 13.
- **The simulator:** the arms of 11.4 and the reports of 11.3.
- **The art team:** the pose grammar, the per-theme art brief and the
  production list of 8.
- **Naming pass:** every placeholder name here.

---

## 19. Open items for the client

None. Approved by the client on 2026-11-22.

The client approved this document as a whole on 2026-11-22, including
the lead's answers to the two brief questions CQ1–CQ2 (including their
two contested parts, "no hidden whole groups" and "no glued archers"),
every lead's call (the lead's calls of the answered proposal, the other
contested lead's calls, the new lead's calls in 19.1 and the lead's
calls after the check in 19.1), the told-plainly items (including "Told
plainly after the check" in 19.1) and the readings recorded in 17, and
its approval covers the flagged rows of 17. The four questions it raised
were answered on 2026-11-21 **(agreed)**; the client also said
everything else in the proposal stands as written. The future items in
19.2, which come back only if a report, an owner's refusal, a wave or a
later design triggers them, and the naming pass remain future items.

### 19.1 This document

**The four answers (agreed, 2026-11-21), each the recommended option:**

- **Q1, party surprise judged per side:** option A. The party is
  Surprised only if no party unit noticed any hostile in the fight;
  visible bait never costs the party its turn; only the capped Baited Room
  can. Per group for monsters (6.8.2; EP-2, against MAIN 4.3, decided,
  and ENM 12.2). Rejected: B, the per-group reading (Cuckoos at 85%
  fill).
- **Q2, the Annex group:** option A. At most 1 per floor on F2–5,
  Dormant, patrol band, 0 XP, inside every budget and guard, serving the
  Tattle beyond-room, the Wager guard, the Dare garrison or the boxed
  lurker (6.13; EP-26, against ENM 10.4, 16 and TL 6.15). Rejected: B,
  no Annex.
- **Q3, seen lurker tells in the danger band:** option A. A seen tell
  counts as a known threat (the lurker type's card); unseen adds nothing
  (6.20; EP-14, against DH 17.3). Rejected: B, the band ignores hidden
  lurkers.
- **Q4, per-group awareness within a per-type set:** option A. Each type
  lists `allowedAwareness[]`; the template and the activity choose
  within it (6.7; EP-9, against MAIN 4.3 "set per monster in data",
  decided). Rejected: B, one state per type.

**The lead's answers to the brief (accepted by the client by proceeding;
confirmed by the client on 2026-11-22, with this document):**
- **CQ1:** yes to ambushes in three forms, the Cuckoo, the Baited Room and
  the Waylay (with the M3 boxed lurker), under the one ENM 10.3 cap; **no
  hidden whole groups (contested by the lead)**; no generated rest or
  backtrack ambushes; the Hedgehog; the party as the better ambusher,
  bounded by `PartySurpriseShare` (0.1, 6.8, 6.21). **The client
  confirmed the contested part on 2026-11-22, with this document**; if
  the client ever wants hidden groups after all, 19.2 names the cost.
- **CQ2:** very tactical at the opening: named formations with soft
  Billets, Keystones, public breaks and validator-proven counters;
  archers start at the back and prefer to stay there; **no glued archers
  (contested by the lead)** (0.1, 6.6). **The client confirmed the
  contested part on 2026-11-22, with this document.**

**Other contested lead's calls** (in the proposal; confirmed by the
client on 2026-11-22, with this document): guard posts never start Watchful and no ordinary group starts
Watchful or Alerted (6.5); no noticing modifier for Busy groups in M2 (6.7).

**Told plainly; confirmed by the client on 2026-11-22:**
- **The boxed lurker never Surprises the party** under its approved
  touch table; the drafts listed it as a party-Surprise source (6.8.1).
- **The themes differ in kind:** the Choir, Carnival, Supper and Heart
  have Waylays only; F5 has no lurker ambush (0.1, 7.1).
- **Party surprise becomes rare by design:** with a standard bait in
  sight a Cuckoo never costs the party its turn; only the Baited Room does;
  the "before the hero's first turn" rows bind only fights with Hidden
  units, and **no lurker ambush ships before DH 21.1 step 1 passes**
  (6.8.2).
- **The drafts' "lurker ambushes void against trained parties" was a
  misread** of "trained Perception marks it"; that open question is
  withdrawn and the E3 arm is re-run (6.8.4).
- **The F1 lair is a Dormant Pack** (6.5); the proposal's reason, "no F1
  guard template can be Dormant", was wrong and is corrected after the
  check (below).
- **Without EP-11 and EP-13, the Silent Penitent and the Ghillie would
  never appear in M2;** roster placeability is now asserted (6.4).
- **Every Tattle on F1 resolves to a Crier** (no Annex on F1), including
  the Wake's Viewing Door (6.13, 7.4); see also the M2 item below.
- **There is no "join chance";** the toy's over-target `ChainPullRate` is
  fixed with segment-6 levers only, and `JoinBudget` would be a client
  change to MAIN 4.3 (6.12).
- **A floor can feel quieter once the patrol dies;** FL 19.2 (c) stays
  open (6.21).
- **The clock:** segment 6 had no approved allowance, and the toy's
  "clock-neutral" result is invalid until re-measured after the break
  fix; the cut order applies before release (6.18).
- **The F4–5 round-1 guard already fails in control fights at maximum
  fills** in the toy; this is a pre-existing 6.4 gap flagged to the
  enemies owner (EP-30, 10).
- **F1 feature adjacency is reported, not banned,** because the F1 role
  order forces the ambush host next to the lair (6.2).
- **Healing (for the DH owner):** approved rests alone needed K ≪ 1 in
  the toy; about 0.8–1.1 HP-eq of extra healing per floor would reach
  "60–80% HP at the boss door" (10).
- **The catalogue corrections are not in the toy** (7, 10).

**Told plainly after the check** (the design check that followed the
client's Q1–Q4 answers; confirmed by the client on 2026-11-22, with this
document; none changes a client answer):
- **The catalogue holds 19 entries, not 17.** The client's "stands as
  written" list says "the 17-formation catalogue"; the entries are as
  written, the count was wrong. They are now read as **15 battle
  formations and 4 start shapes** (Patrol File, Revel, Bout, the Heap),
  so a Busy, Dormant or Afoot group carries both a start shape (cross-document comparison 2026-11-23, CC-106) and a
  battle formation (6.6.6, 6.7). The chip set is 17 icons: the 15 battle
  formations plus "Line broken" and "Facing the door" (8).
- **Ambushes are rarer than the proposal's figures.** The approved host
  rules leave one legal guard-post host on most floors and none on
  trimmed F3 and F4, so generated ambushes run about 0.3 / 0.4 / 0.5 /
  0.6 / 0.6 per floor (about 2–3 per run, not 3–4), and the ENM 10.3 cap
  of 2 is reached by generated forms only on untrimmed F5; from M3 the
  boxed lurker fills the second place (0.1, 6.8.1). The host rules are
  kept on purpose.
- **The Archive Baited Room is mostly an F3 event** (one lurker rarely fits
  the window at the expected party); the Baited Room now allows 2 lurkers
  from F3 at party ≥ 3, not only on F4–5 (6.8.1, 7.6).
- **No solo Cuckoo on F2–5.** A Cuckoo's bait and lurker standards break
  the settled ENM 10.1 solo rule; the proposal's list missed it. Solo
  ambush exposure is F1 only (6.3, 6.8.2).
- **Every Tattle is a Crier until the Annex ships in M3**, on every
  floor, including the Heart's Nursery door (6.13).
- **The Annex's four uses exclude each other** on a floor; priority
  boxed lurker > Wager guard > Dare garrison > Tattle, and the needs not
  served take their approved failure actions (6.13).
- **Readings of the agreed Q2 text:** the boxed-lurker Annex is Hidden
  in its box, not Dormant; the guarded Wager and Dare forms are served
  by the Annex Dormant among the devices; TL 6.15's "full-strength" guard
  is read as a patrol-band group (6.13, 17).
- **The proposal's single lurker-Stealth hash gave one chance, not
  two:** each of ENM 12.2's three rolls now has its own key, and the
  lurker springs only within 3 squares, so the party really gets two
  checks before a Baited Room takes its turn (6.8.3, 6.8.5).
- **A Pack guard post may be Dormant** (ENM 17.4 allows it); the
  proposal's "no F1 guard template can be Dormant" (ET20) was wrong.
- **On hooded-cot Gaol seeds the floor has no shut-door pair** (EP-13
  now names FL 7.2's Law-in-layout bullet, the FL theme table and RS 8.3
  Lockdown).
- **F1 Billets work at half weight**, so F1's "go around" lessons mean
  something (6.3, 6.17).
- **Art scope rises in M2:** per-position Posted idles per rig family
  replace "one generic animation per posture", because the
  `FormationRead` gate cannot pass on one idle; the proposal's "about 290
  animations" was a toy figure and is replaced by a count table (8).
- **Several catalogue entries missed the document's own fill and tier
  rules** and are recast (G1, K1, W1, K6, W5, C3, S4, GU6), and the
  lair's tier rule is read per template (6.2, 7).

**New lead's calls made while writing this document** (beyond the
answered proposal; confirmed by the client on 2026-11-22, with this
document):
1. **Formation tiers are labelled FT1–FT3**, since "Tier 1–3" collides
   with the ability model's spell tiers (front matter, 6.2).
2. **"Disposition" is renamed "posture"** (ENM 14.6 uses "disposition"
   for Yielded and Fled), and **the posture "Walking" is renamed
   "Afoot"** (a plain word, 13 hits) (front matter, 6.7, A.5).
3. **The art "stance grammar" is renamed the pose grammar** ("stance" is
   a Vanguard rules word) (8, A.5).
4. **EP-1 is flagged against MAIN 6.3 (decided)**, on the FL-2, TR-17
   and TL-3 precedent of flagging stream additions (6.1, 17).
5. **The Ritual is legal only on F3** (Choir-only, and the Choir is an
   F2–3 theme), correcting the proposal's F4–5 listing (6.2).
6. **A Waylay always uses the Kill Box;** the Archive and Supper Battery
   Waylays are renamed Kill Boxes whose two marksman lines cross the
   watched lane (S6 "Aisle Kill Box", SU6) (6.6.6, 7.6, 7.8).
7. **Mindless members of a Busy group** take no activity and no Billet
   and stand Posted on front sockets; Busy is barred only for all-mindless
   groups (6.4; applies to S2, SU2, H2).
8. **An ambush form with no legal fill at the floor's weight is not
   drawn** (the ENM 10.3 cap is a maximum, never a quota), including the
   F1 Cuckoo where native levels do not allow it and the Archive Baited Room
   at F2 party 2 (6.8.1, 6.17, 7.6).
9. **The Baited Room ships in M2** (after DH 21.1 step 1), so the Archive's
   bait-cache reading tray is its M2 Baited Room form, not an M3 variant
   (6.8.1, 7.6).
10. **Catalogue arithmetic:** K5 recast to Poacher −1 (7) + Ghillie −3
    (4) = 11; S3 at F3 recast to bait −1 (7) + 2 Crawlers 0 (20) = 27;
    GU4 recast to bait +1 (14) + 2 Gutworms 0 (20) = 34, with a Baited Room
    form of 34; GU6 completed at 50 of 57; C2 restated without the
    leader draft (7).
11. **The Flag column marks readings of agreed or decided text** (EP-9,
    EP-11, EP-12, EP-19, EP-22, EP-28, EP-33), beyond the proposal's ⚑
    set, on the TL precedent (17).
12. **The chip icons** were read as "the 17 formation chips plus 'Line
    broken' and 'Facing the door'" (19); after the check they are
    the 15 battle-formation chips plus those two, 17 in all (8).
13. **`RoundsPerFightDelta` keeps TL's report name** with a `segment`
    key, so the TL and segment-6 allowances stay separate and additive
    (11.3).
14. **EP-39 uses CMP's own "captive"** (the same concept), not a new
    name (9.1, A.5).
15. **The flat-floor citation** in 4 is MAIN 4.3 (no high-ground rules,
    decided), not "main 6.8" as the proposal had it (4).
16. **The fallbacks of 19.2** for each request to another owner.

**Lead's calls after the check** (each marked "(lead's call, after the
check)" where it stands; confirmed by the client on 2026-11-22, with
this document; none overrides a client answer):
1. **Pipeline:** formation choice moved to a new step 5b, before fill;
   ranked candidate lists from the single weighted draw; step 2a's exact
   schedule (the boxed lurker first on the cap; a failed form becomes a
   plain guard post); step 9 changes re-run 5b–8; the draw-free floor
   allocation (9b) and repair pass (10g); the floor-level failure policy
   with one retry on `retry1` and `V12FloorFallback`; the stream written
   `Derive(…, FNV-1a("encounters"))`; `trapChargeCost` (6.1).
2. **Start sockets derived** from FL `startZone` squares; socket-class
   precedence, landing, axis and thirds defined; the watched lane (6.6.1).
3. **Fill semantics:** the fill target is a minimum; n and the index for
   the patrol and the elite room; the Baited Room budget and window; the
   F1 step-down floor of 850‰; the F1-Cuckoo lair override (6.2).
4. **The lair's tier is the highest legal for its template; F1 is
   exempt from "the last guard post is FT2 or FT3"** (6.2).
5. **Battle formations and start shapes split**, with `startShapeId` and
   `formationId`; Scramble the Scavengers' contact shape; Crescent for
   Dens, Picket Line for Sleepers; gorged Revel for Sleepers; the Ritual
   a Warband with the Deacon (6.4, 6.6.6, 6.7).
6. **`FormationDef` gains** `kind`, `hostKinds[]`, `themes[]`,
   `ambushForms[]`, `tierRules`, `maxFloor`, `keystoneRule` and the closed
   `counter.guarantees[]` enum; `GroupPlacement` gains the fields of
   6.13 (11.1).
7. **Legal state set** = template states ∩ members' `allowedAwareness[]`,
   enforced in fill; the Awareness column mapped to states (6.4).
8. **Billets:** hold-square and fall-back algorithms; `billetBase`
   constant per unit; `billetScale` frozen per group (0 solo on F2–5,
   500‰ on F1); facing presentation only (6.3, 6.6.3).
9. **The Stung rule's definitions** (source, cannot answer, unseen
   sources, timing; the melee clause permanent) (6.6.3).
10. **Breaks:** any forced move off the hold squares breaks a Keystone
    (Attack Shunt excluded); timing and operands; `maxRounds` counts
    contact rounds and an out-of-sight party leads to the lost-the-party
    route; the Kill Box Keystone named; the break shown as a UI beat
    (6.6.4).
11. **Per-side surprise computed** with `partyNoticed`, combat membership
    and a fixed order (6.8.2).
12. **Lurker Stealth keyed per band**, and the spring at 3 squares
    (6.8.3, 6.8.5).
13. **The Waylay:** the watched lane; "Facing the door" starts the
    Kill Box broken; the Spoor base mark on both jambs and the lintel,
    with Choir and Gut re-skins (6.8.6).
14. **Ambushes:** no solo Cuckoo on F2–5; 2 Baited Room lurkers from F3 at
    party ≥ 3; the boxed lurker exempt from the guard-post host rule; the
    restated frequencies; the 17.4 Ambush cells in EP-10; bait-cache
    rules (6.3, 6.8.1).
15. **Guard posts:** no Skirmish Spread or Grindstone; a Pack guard post
    may be Dormant (6.10).
16. **Joiners:** the partner defined; levers applied in 9b; lever 2 a
    request to FL (6.12).
17. **Hand-offs:** TL binds the Keybearer from segment 6's candidates;
    TP2's draw-free re-place; the Annex funding, priority, binding,
    `AnnexHearingGap` and its two readings (6.13).
18. **The Hedgehog** for groups without Billets, and the extended
    defensive variant (6.21).
19. **UI:** `ChipNoLeak`; the read-surface table; the danger band's reach
    from the tell square (6.20).
20. **V12 in three parts** (worst-case build time, generation-time
    re-check, simulator gate); report definitions for `DoorCampShare`
    (now gated), `ClassHpEqDelta` (absolute spread),
    `FormationBreakRound` (censored), `CounterCost` (generation time),
    `LeaderReach`, `StartArc` and `FirstSight` (9.2, 11.2, 11.3).
21. **F1:** first fights at the 850‰ minimum of 11; the CL `seedTarget`
    predicates verbatim (6.17).
22. **Art and audio:** per-position M2 idles per rig family; `animReady`
    and `ActivityHasAnimation`; role props kept on the model; the watch
    pose a whole-body pivot and head tracking reserved for lurker tells;
    no sitting on pieces; table sockets on existing RT pieces
    (`TableSocketPieceExists`); same-type or shadow-spar Bouts; edge-aligned
    shields; positional activity audio; the Ritual's reserved chant; the
    animation count table (6.7, 8).
23. **The clock's first cut lever** is "no Crossfire or Kill Box on F4–5
    guard posts at party 4" (6.18).
24. **Catalogue recasts** (G1, K1, W1, K2, K6, W3, W4, W5, C1, C2 note,
    C3, C4, C5, C6, S1, S3, S4, CA2, SU4, GU6, H2) (7).
25. **Formation sketches** for the M1 set and three M2 showcases
    (Appendix C).

### 19.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a
wave or a later design triggers them; each fallback is a new lead's call
(confirmed by the client on 2026-11-22, with this document):
- **If the RT owner refuses the start-socket reading (EP-5):** RT adds a
  `StartSocket {socketId, square, coverFrom[], nook}` record to the
  TerrainLayer, and 6.6.1 reads it instead; nothing else changes.
- **If the enemies owner refuses EP-6 (the Billet term):** formations
  ship as the M1 cut (static starts on front and back sockets, no AI
  term); the Keystone and breaks still show; the Read card drops hold
  squares.
- **If the enemies owner refuses EP-15 (the "post" reading):** Watchful
  groups return to their start squares, facing the lost Portal; the
  ⅓-down rule still re-forms them without Billets.
- **If the enemies owner refuses EP-28 (ENM 13.5):** no hold overlay; the
  Read card shows the formation name, breaks, Keystone and counter line
  only.
- **If the enemies owner refuses EP-11:** no floor-1 Cuckoo (the F1
  ambush cap stays unused), and the Gaol and Kennel lurkers become an
  RS-owner roster item (roster placeability fails for them).
- **If the FL owner refuses EP-13:** no Gaol F1 Cuckoo; the Silent
  Penitent goes to the RS owner as a roster-placeability failure.
- **If DH 21.1 step 1 fails on the per-side reading:** no lurker ambush
  ships (Waylay only), and the DH levers (F1 ambush and lair budgets)
  apply, never a looser threshold.
- **If TL-16, TL-30 or SH-21 are refused:** the trap opener, Crier
  arrivals and Lure drop out of the counter lists (6.8.7, 6.8.8); nothing
  else changes.
- **If the ABL owner refuses Hold the Hide (EP-17):** the party times the
  patrol with ordinary waits; the cover-room window is still built.
- **If the FL owner cannot supply the cover-room window:** the next
  Circuit side room; if none, the floor is logged and the patrol template
  is a Picket (6.9).
- **If the FL owner refuses the Listen additions (EP-9, EP-12):** the
  activity shows only on sight, and Listen reports "still" for a Waylay;
  the Spoor mark carries the read.
- **If `ChainPullRate` stays over 0.6 after the segment-6 levers:** the
  request to the FL owner (6.12, item 4); `JoinBudget` only as a client
  change to MAIN 4.3.
- **If `PartySurpriseShare` exceeds the Shade profile:** the Shade parity
  re-run; levers: the `StealthOpenable` ceiling, the Busy share, Hold the
  Hide.
- **If `SurpriseDeathShare` misses:** the 2024-style surprise fallback
  (EP-41 note) goes to the client.
- **If the clock line misses:** the cut order of 6.18, before release.
- **If `LairSneakRate` exceeds 25%:** the enemies owner re-reads ENM 20.1
  (6.10).
- **If the M2 simulator still shows the Annex as a large net loss
  (`RiskSocketNetWinDelta` below −5; pending request from the loot
  economy owner, agreed there as Q6):** the fix comes from encounter and
  experience tuning (the Annex's funding and band, 6.13; ENM 15.2, 16;
  DH 18.2), not loot (loot economy design 6.9 item 8).
- **If the client wants hidden whole groups after all (CQ1):** they need
  non-lurker Stealth (an ENM 12.2 change), a new DH 10.4 reading and the
  6.4 surprise rows per group member; the toy cost about 4 points of win
  rate.
- **Pending from the events design (segment 8, approved 2026-11-22):**
  the EP owner confirms two readings of the segment-8 hook (13): EV-14,
  Curiosity noise as generic noise under "no event spawns or moves
  units" (M2); and EV-32, M3 Overtures on tied groups. If the EP or
  enemies owner refuses EV-14, Curiosity noise prices become bound tick
  costs and the events design re-prices (events design 14.2); if EV-32
  is refused, no Overtures ship.
- **Pending from the boss rooms design (segment 9, approved
  2026-11-22):** the EP owner rules BR-19's 6.14 request (segment 9's
  `ArenaFormation`s, Retinue one allowed shape on F2–5) and confirms
  BR-11's reading of 6.12 and EP-16 (reserve sockets are already in the
  room) (M2). If the 6.14 request is refused, arena formations are
  authored as Retinue variants inside 6.14's shape on F2–5 (boss rooms
  design 14.2).
- **Pending from the exploration and information design (segment 10,
  approved 2026-11-23):** the EP owner confirms the Spoor projection
  under cutaway (XI-18, 6.8.6; M2), rules EP-28 (hold squares and
  fall-back), and owns `PatrolFightRate` (XI-6, 11.3; M2). If the EP
  owner declines `PatrolFightRate`, segment 10 owns the report and the
  gate is unchanged; if the client refuses EP-41, watch lines ship
  without the noticing step tag's odds and Cased ships without its odds
  band (exploration and information design 14.2).
- **Pending from the seeds and validation design (segments 11–12,
  approved 2026-11-23):** the EP owner accepts the signature-twice
  authority in V12g and the V12t sample row (fallback: V19t runs it);
  accepts `ProtectedIntact`'s group part as V12g at AP2 (fallback: FL
  owns both parts, under V8g); and, with the companions owner, confirms
  the `content.CMP8.*` rows (fallback: they run as 13 words them, under
  CMP 11.2) (11.2; SV-20; seeds and validation design 11, 14.2).
- **At M3:** the approach band out of combat (EP-41); two-group "watched
  sleep" rooms.
- **Later:** the Errant (a MAIN 4.3 change, client only); a Rumour-like
  escalation only by explicit client override of FL 9.1.
- **The Cathedral roster** (M4; 7.11).
- **The naming pass.**

---

## Appendix A: how the critique findings were resolved

This appendix is history and keeps the names as written; the naming
pass (NP-35, 2026-11-24) renamed several of them (front matter).

### A.1 Fun critique (XF)

| Id | Finding | Resolution |
|---|---|---|
| XF1 | Billets invite door-camping; `LandingShooters` makes the doorway safest | The Stung rule (EP-6); the Kiter doorway lane (EP-8); the anti-trickle wait **rejected** (invariant 7); `DoorCampShare` |
| XF2 | Breaks at median round 1 hollow out formations | The tightened line-crossed trigger, shipped *with* Stung and ⅓-members; two gates (6.6.4) |
| XF3 | The Waylay a solved chore | `CounterCost`; the HP-eq band +5% to +15%; "Caught facing the door"; trained Listen names the watched Portal |
| XF4 | Lurker ambushes bimodal | The corrected marking reading (the tell only, noticing per 12.2; no advantage added); the reveal beat; `AmbushFiredRate` |
| XF5 | Kiter + overwatch + Crossfire focus the Arcanist | The LOS breaker; fall-back squares that lose sight of the landing; the `ClassHpEqDelta` gate |
| XF6 | Disengage-rest-reset loop | The Hedgehog without Billets after ⅓ down or the Keystone lost; watches all Portals; `ReEngageRate` |
| XF7 | Chain pulls make showpieces chaotic | No near pair on Waylay, Ambush or FT3 hosts; arrival arrows; join levers (`JoinBudget` not adopted, XK5) |
| XF8 | Some classes have no answer | The `answers{}` matrix; ≤ 1 class without a lever; per-floor showcases; fearless-anchor tags |
| XF9 | Container bait poisons looting | The bait-cache prop family; `ContainersOpened` |
| XF10 | The F1 ambush stacks with the lair; F1 repeats most | The F1 ambush only in the L-G order; ≥ 3 triples per F1 theme; F1 activities in M2 |
| XF11 | Formations solved within a few runs | Terrain Pairings; the counter line free on first meeting only; no-repeat enforced |
| XF12 | Party openings cluster late | A pre-lair openable group at party-median sneak ≥ 50% |
| XF13 | Revel + Surprise + area is a one-turn win | The radius-1 clump cap from F2; Busy ≤ 30% from F3; Revel median ≥ 2 rounds |
| XF14 | UI pile-up | The board shows the chip and Keystone only; the Read card on inspect |
| XF15 | Spoor as a floor decal | Moved to frames and walls (also XK6) |

### A.2 Balance critique (XB)

| Id | Finding | Resolution |
|---|---|---|
| XB-H1 | The F4–5 round-1 guard breaks on placed geometry | EP-30; `LandingShooters` 1/2/2/2/2 on the entry lane; +2 shooter fill ≤ 925‰; flagged to the enemies owner |
| XB-H2 | Turn-loss risk lands on solo and duo | The Mousetrap at party ≥ 2, never solo or F1; 2 lurkers only F4–5 at party ≥ 3; `AmbushTurnLossByPartySize` |
| XB-H3 | The ambush an XP bargain under per-side | The Cuckoo at full band; the Mousetrap at 85%; the Waylay band's lower bound; `XpPerRisk` |
| XB-H4 | Solo leaks multi-threat starts | Front-standard-only solo formations; `billetScale` 0 for a solo hero on F2–5; no solo ambush form; front ≥ back in `SoloShapeMonotone` |
| XB-H5 | The plinking exploit; the break fix invalidates "no clock cost" | The Stung rule; re-measure; the clock line and cut order before release |
| XB-M1 | Stealth supply can breach the Shade bound | The `StealthOpenable` ceiling 3; Hold the Hide once per floor, window only; the clump cap; the worst-legal Shade profile |
| XB-M2 | Joiner fixes change MAIN 4.3 | Segment-6 levers first; `JoinBudget` only as a client change (not proposed); one call source; `JoinedUnitsCap` |
| XB-M3 | Posts camp hazard edges | No Billet within 2 of a Void or lethal hazard; no ambush forms in Void or clock rooms; shove × fall in 6.4; the Revealed-area native rule; Kegs away from the entry lane |
| XB-M4 | Off-slot XP and budget holes | The Annex (not slot mapping); no Annex on F1; the Annex inside the cumulative, cluster and chain-pull counts |
| XB-M5 | F1 ambush feasibility misstated; an adjacency spike | The standard-bait Cuckoo shown feasible (CMP-8 to confirm); never a Mousetrap on F1; the lair fill lowered; the L-G order only |
| XB-M6 | `LandingShooters` empty for the Waylay | Measured on the entry lane: ≤ 1 F2, ≤ 2 F3–5 |
| XB-M7 | The class spread widens against the Arcanist | `ClassHpEqDelta`; the Crossfire needs a Tall piece blocking a line |
| XB-L1 | The elite Steal too cheap | The watcher's hold squares include the elite's adjacent squares |
| XB-L2 | Toy calibration hides absolute risk | Re-runs at K = 1 owed (10) |
| XB-L3 | Gb at 1000‰ raises skips | Gb stays on the curve |
| XB-L4 | No-repeat violations | A V12 hard assertion |
| XB-L5 | The Hedgehog's other-Portal answer | Watches all Portals; `ReEngageRate`; no healing |

### A.3 Consistency critique (XK)

| Id | Finding | Resolution |
|---|---|---|
| XK1 | The Ritual singer rule inverted | Re-written: singers hold back the Tide; Choir only; a Tide WetEdge host (EP-27) |
| XK2 | Boxed lurkers listed as party-Surprise sources | Removed; per TL 6.14 the party is never Surprised by one |
| XK3 | Activities break template Awareness | State ∈ the legal state set asserted; feeding Packs are Dormant; no Den awake; the Waylay from Stationary templates; a Dormant guard post as a Pack or (from F2) a Warband (corrected after the check); the F1 lair a Dormant Pack |
| XK4 | Compositions fit no template | Recast: S1, CA2, CA4, C4, H2 as Picket; SU1 and C1 without minions; GU3 arithmetic corrected; `TemplateLegal` in V12; growth inside slot ranges (and the fill-window corrections of 7, new lead's call) |
| XK5 | Join chance / `JoinBudget` not allowed | Segment-6 levers only; the Errant marked as a MAIN 4.3 change |
| XK6 | Floor Spoor breaks RC1 / RC1+ | Spoor marks on frames and walls |
| XK7 | The per-side reading reopens Ambush Strike on F1 | EP-11: a floor-1 lurker never uses Ambush Strike |
| XK8 | Clearway starts; derived classes beyond sockets | Holders on Clearway-adjacent sockets, sleepers beside it (EP-24); classes over the derived start sockets only (defined after the check, 6.6.1) |
| XK-H1 | The Tattle dead without extra groups | Told plainly; the Annex (Q2) |
| XK-H2 | Off-slot options all change approved rows | Rows listed in EP-26; client Q2 |
| XK-H3 | "Marks it" misread as an auto-spot | The tell marked; noticing per 12.2; the question withdrawn; E3 re-run |
| XK-H4 | Gaol and Kennel lurkers never spawn | Kennel 300‰; the Gaol hooded cot via EP-13; the roster-placeability assertion |
| XK-H5 | The overlay and an HP threshold leak 13.5 data | EP-28 flagged; breaks count members |
| XK-H6 | Decided rows labelled readings | The stream (EP-1, now flagged against MAIN 6.3), the keyed hash (EP-3), per-side (EP-2, Q1), Stand To (rejected), per-group awareness (EP-9, Q4) all named |
| XK-H7 | The Shade metric misnamed | `PartySurpriseShare` fed to `stealthShare[L]` |
| XK-M1 | "Set piece" collides | Terrain Pairings; tiers numbered FT1–FT3 |
| XK-M2 | Post, springZone, Column, Rite, Roused, Watch | Billet, open zone, Patrol File, Ritual; Roused and bare "Watch" dropped |
| XK-M3 | EP numbering incoherent | One register EP-1 to EP-41; the A.6 cross-reference |
| XK-M4 | Waylay Listen needs a closed Door | The watched Portal's approach is a Door that starts closed |
| XK-M5 | New obligations on other segments | The ABL 7.2 row, the FL 6.10 entry, the FL 6.7 / RT KP4 requests with a failure action |
| XK-M6 | The terminal fallback might fail | A role-blind Picket under hard constraints only |
| XK-M7 | The tabletop draft's Waylay state and ×0.85 | Not imported; the ENM 10.1 and 16 sentences re-worded |
| XK-M8 | The Mousetrap tightening and F1 feasibility | Flagged against ENM 17.4; feasibility by CMP-8 |
| XK-Low | Static props; Rival Party; Scavengers on Gb; Kill Box cut wording; awareness mix after step 10 | Units animate only; the EP-34 stub; Gb never Scavengers; the cut order's first lever is now "no Crossfire or Kill Box on F4–5 guard posts at party 4" (after the check: no Kill Box stands in a lair); V12 asserts the mix after step 10 |

### A.4 Judges' grafts taken

From A: the M1 cut, the envelope as reporting, the solo table, the
contests (no Wary guards; XP is a pool). From A and C: the keyed lurker
hash. From the formations brief: the role table, per-profile weights,
the invariant-7 guard, the F1 covered fall-back reading, the Hidden-Shade
break exemption, the Cuckoo/Mousetrap/Waylay taxonomy,
`LurkBlind`/`TellSight`, the pose grammar. From the mechanics brief: the
Keystone, Hold the Hide, the Read card, `captiveSocket`, the Shade-bound
metric. From C: arrival arrows, Gb never an ambush, the Lore preview of
formations. From the catalogue: the eligibility table, the acid and
shover assertions, the Gaol lurker home, theme skins and art notes. From
the toy: the Waylay gate, 6.4 on placed geometry, enforced no-repeat.
**Not taken:** C's slot mapping for the Wager guard (XB-M4); `JoinBudget`
(XK5); Model A's Gb at 1000‰ (XB-L3).

### A.5 Names checked

Grep-checked case-insensitively, whole word, across all 22 approved
documents, **0 hits**: Billet, Keystone, Stung, Hedgehog, Cuckoo,
Mousetrap, Waylay, Spoor (mark), Bait cache, Annex, Picket Line, Door
Line, Phalanx, Crescent, Patrol File, Skirmish Spread, Retinue, Honour
Ring, Champion Watch, Crossfire, Kill Box, Pincer, Grindstone, Sleeping
Ward, Revel, Bout, Scramble, Ritual, Bait and Nook, Busy, Posted,
posture, Afoot, open zone, entry lane, Hold the Hide, Encounter Read
(card), Terrain Pairing, Caught facing (the door), Line broken, Wake
Order, Messenger, Call Post, Errant, Standoff, Riot, Prowl, Rumour;
`EncounterPlacement`, `EncounterPlan`, `GroupPlacement`, `FormationDef`,
`ActivityDef`, `allowedAwareness`, `lurkStealth`, `watchedPortal`,
`holdSquares`, `fallBackSquare`, `captiveSocket`, `keybearerCandidates`,
`ambushForm`, `formationId`, `activityId`, `StartArc`,
`BacklineReach`, `LeaderReach`, `AuraCover`, `LandingShooters`,
`FirstSight`, `FirstFightSimple`, `LurkBlind`, `TellSight`,
`AmbushStandoff`, `CounterPresent`, `CounterCost`, `StealthOpenable`,
`SoloShapeMonotone`, `FormationSurvivesFill`, `TemplateLegal`,
`AcidSourcesAfterOverlay`, `FloorEnvelope`, `PartySurpriseShare`,
`SurpriseRatio`, `SurpriseDeathShare`, `AmbushDeathShare`,
`AmbushTurnLossByPartySize`, `AmbushFiredRate`, `XpPerRisk`,
`WaylayListenRate`, `KillBoxCounterUse`, `WaylayRound1Damage`,
`BacklineReachRate`, `FormationBreakRound`, `DoorCampShare`,
`ClassHpEqDelta`, `StealthOpenableShare`, `RevelOpenerDamage`,
`LairSneakRate`, `JoinerWarningLead`, `FormationEntropy`,
`TemplateEntropy`, `FormationFallbackRate`, `ReEngageRate`,
`AnnexEngageRate`, `ContainersOpened`, `F1FeatureAdjacency`,
`FormationRead`, `JoinedRangedShare`, `JoinedUnitsCap`,
`ArrivalThroughPortal`; ids `EP-[0-9]` (whole id), `V1[2-9]`, ET1–ET20,
CQ1–CQ2, FT1–FT3, XF1, XB-H1, XK1, XK-H1. **Added after the check, also
0 hits:** start shape, battle formation (as a term), contact round,
floor allocation, repair pass, watched lane, rig family, `startShapeId`,
`billetScale`, `billetBase`, `partyNoticed`, `partyCountAtGeneration`,
`keystoneRule`, `tierRules`, `hostKinds`, `animReady`, `encounterId`,
`linkedGroupIds`, `tellSquare`, `spoorPortalId`, `mousetrapsThisRun`,
`trapChargeCost`, `retry1`, `ChipNoLeak`, `V12FloorFallback`,
`AnnexHearingGap`, `TableSocketPieceExists`, `ActivityHasAnimation`,
`ShoveOutSquare`, `SecondPortal`, `FlankPair`, `TallWithin3OfLanding`,
`CV8FlankSquare`. **"Layout" was not used as the term**: FL's `layout`
stream and segment name it, so the Busy, Dormant and Afoot arrangements
are **start shapes**. "Start socket" is RT's own words (CV8), reused in
the same sense and now defined (6.6.1).

**Reused on purpose (existing names, same meaning):** `ChainPullRate`
(FL, ENM), `RoundsPerFightDelta` (TL; segment-keyed, 11.3),
`WagerXpDelta` (TL), `SeedHasTarget` (CL), `Floor1NoCoveredStarts` (RT),
`partyEntrySquares` and `equivalentHosts` (FL), "captive" (CMP; EP-39),
`encounters` (a plain word in several documents; as a stream name it is
new, 6.1).

**Renames forced by collisions:** "Feast" → **Revel** (Supper backdrop
text "the same feast again"); "Absorbed" → **Busy** (a plain word in DH
and SH); "Huddle" (an HR plain word) and "Rummage" → **Scramble**;
"Shield Wall" → **Phalanx** (the Vanguard's Shieldwall, 53 hits); "post"
/ "anchor" → **Billet** (guard post; FL-12 anchors); "Fallback" (315
generic hits) → **fall-back square**; "Set Piece(s)" → **Terrain
Pairing** (a TL term); tier names "Showpiece" (HZ), "Plain" (a TL lock
kind), "Combined", "Simple" → numbered tiers **FT1–FT3** (new lead's
call: "Tier n" collides with spell tiers); "Rite" (an HR column) →
**Ritual**; "Column" (55 hits) → **Patrol File**; "Screen" (76) →
**Skirmish Spread**; "Court" (19) and "Banner Ring" (a duplicate) →
**Retinue**; "Bottleneck" and "Bow Line" (duplicates) → **Picket Line**
/ **Door Line**; "springZone" (the TL verb "spring") → **open zone**;
"Provoked" (the Sentinel, approved) not reused → **Stung**;
"Roused"/"Rouse" and bare "Watch" dropped; "Pastime" → **activity**;
"Waiting Room" dropped in favour of the Cuckoo/Mousetrap/Waylay names;
"Seats" avoided ("Cold Seat") → **core and growth positions**;
"Offering" (1 hit) → **bait cache**; **new lead's calls:**
"disposition" (ENM 14.6: Yielded and Fled are dispositions) →
**posture**; "Walking" (13 hits) → **Afoot**; "stance grammar" (the
Vanguard's stances) → **pose grammar**. Avoided as already taken:
Muster, Tableau, Stir, Station, Vigil, Hunt, Shieldwall, Killing Ground,
Lie in Wait, Stake Out, Runner, Warned, Groggy, Signal, Lull, Lookout.

### A.6 EP cross-reference (source drafts → this register)

| Source | Local id | Here |
|---|---|---|
| Videogame research | EP-1 stream; EP-2 model; EP-4 Post/Fallback; EP-5 fairness; EP-6 Waylay; EP-7 F1 lurker; EP-8 activities; EP-9 Wary re-form; EP-10 lair sneak; EP-11 variety; EP-12 Ritual; EP-13 near pairs; EP-14 V12; EP-15 TP2; EP-16 party size; EP-17 reports | EP-1; EP-4; EP-6; EP-8; EP-12; EP-11; EP-9; EP-15; EP-23; EP-20; EP-27; EP-22; EP-32; EP-25; EP-19; EP-33 |
| Tabletop research | EP-1 sockets; EP-2 formations; EP-3 activities; EP-4 Waylay; EP-5 Stand To; EP-6 traps; EP-7 Circuit; EP-8 leaders; EP-9 curve; EP-10 Hunt; EP-11 V12 | EP-5; EP-4; EP-9; EP-12; rejected; EP-25; EP-17/18; EP-8; EP-20; rejected; EP-32 |
| AI/fairness research | EP-2 per-side; EP-3 Waylay rooms; EP-4 no phantom flanks; EP-5/6 Billets and breaks; EP-12 Hue and Cry; EP-18 2024 fallback | EP-2; EP-10; EP-16; EP-6/7; rejected; EP-41 note |
| Model A | EP-2 fill curve; EP-3 off-slot; EP-7-alt Wary guards; EP-9 per-side; EP-11 keyed roll; EP-13 solo; EP-17 rest | EP-20; EP-26; rejected; EP-2; EP-3; EP-19; EP-16 |
| Model B | EP-3 first-entry roll; EP-10 Waiting Room; EP-18 satellites; EP-25 Stand To; EP-28 2024 surprise | rejected for EP-3; EP-10/12; EP-26; rejected; EP-41 note |
| Model C | EP-18 Rumour; EP-19 Prowl; EP-20 JoinBudget; EP-23 off-slot mapping | rejected; rejected; not proposed; not taken |
| Formations brief | EP-1–EP-16 | EP-4, 5, 6, 7, 8, 9, 10, 12, 11, 15, 16, 19, 20, 27, 32, 28 |
| Catalogue | EP-30 Gaol; EP-31 Annex; EP-32 eligibility; EP-33 acid | EP-13; EP-26; EP-31; EP-31 |
| Mechanics brief | EP-1 Pastimes; EP-2 Keystone; EP-3 door open; EP-4 Waylay; EP-5 lurk rooms; EP-6 Hold the Hide; EP-7 Wake Order; EP-8 Messenger; EP-9 Call Post; EP-10 Seats; EP-11 Set Pieces; EP-12 Read; EP-13 Riot; EP-14 Errant; EP-15 Captive; EP-16 Re-form | EP-9; EP-7; EP-8; EP-12; EP-10/2; EP-17; EP-37; EP-35; EP-36; EP-19; EP-21; EP-28/41; EP-40; EP-38; EP-39; EP-15 |

---

### A.7 The design check (after the check)

The revision answered a four-lens check (builder, consistency, play and
art; 79 findings). Every finding was fixed, merged into another fix, or
rejected with a reason; none overrode a client answer. The resulting
lead's calls and told-plainly items are listed in 19.1 under "after the
check", and each is marked where it stands.

## Appendix B: sources

- The 22 approved parent documents listed at the head of this document.
- The lead's encounter-placement synthesis (2026-11-21): three model
  drafts (A, B, C), three judges' scores, the formations, mechanics,
  catalogue, videogame, tabletop and AI/fairness research briefs, and the
  fun, balance and consistency critiques (Appendix A).
- The design check of 2026-11-22 (builder, consistency, play and art
  lenses; A.7).
- The toy simulator runs of 10 (800 runs per arm; enemy damage scaled by
  K = 0.14–0.59; scripts in the lead's scratch area, not project files).
- Games and tabletop sources as cited in 4 (from the research briefs;
  play knowledge and published rules, not re-verified here).

---

## Appendix C: formation sketches (lead's call, after the check)

Top-down reference grids for blockout and the read tests, *(illustr.)*:
real rooms come from RT layers and the solver (6.6), so these show the
intent, not a template. Legend: `P` the watched Portal (the party
enters here), `L` landing squares, `=` Clearway, `C` the choke square,
`*` the Keystone, `F` front, `B` back, `O` overwatch (Covered), `#`
cover (Low), `T` Tall piece, `f` flank, `w` ward or leader, `m` minion,
`a` alpha, `z` sleeper, `t` table piece, `s` shooter, `k` melee, `x` the
proven counter lane, `Q` a second Portal, `.` floor.

**Picket Line** (M1): the Holder on the choke; the counter goes round.
```
 . . . . . . . . . .
 . f . . . = . . . .
 . . . . F*C . . . .
 x x x x . = . . m .
 x . . . . = . . . .
 x . . . L L L . . .
 . . . . L L L . . .
 . . . . . P . . . .
```

**Door Line** (M1): front on the choke, the bow back (on overwatch from
F2; uncovered on F1); `BacklineReach` and the Shove-out square.
```
 . . . . # O . . . .
 . . . . . . . . . .
 x x x . . = . . . .
 x . . . F*C . . . .
 x . . . . = . . . .
 x . . . L L L . . .
 . . . . L L L . . .
 . . . . . P . . . .
```

**Crescent** (M1): the alpha leads an arc of minions toward the entry;
back to a wall or the choke, kill the alpha.
```
 . . . . . . . . . .
 . . . m . . . m . .
 . . m . . = . . m .
 . . . . a*= . . . .
 . . . . . = . . . .
 . . . . L L L . . .
 . . . . L L L . . .
 . . . . . P . . . .
```

**Patrol File** (M1 start shape): the file on its route, front first,
swarmers last; the cover-room window (`h` Hide spots) beside the lane.
```
 . . h . . . . . . .
 . . . . . . . . . .
 - - F w s f m - - >    (the drawn route)
 . . . . . . . . . .
 . . h . . . . . . .
```

**Kill Box** (M2 showcase; Waylay): shooters on blind sockets out of
sight of the landing, the melee Keystone nearest the watched lane, the
second way in (`Q`) with its cost (`~` a Squeeze); the Spoor mark on the
frame of `P`.
```
 . s T . . . . T s .
 . . . . . . . . . .
 . k*. . = . . . k .
 . . . . = . . . . .
 T . . L L L . . . T
 . . . L L L . . . .
 ~ Q . . . P . . . .
```

**The Heap** (M2 showcase start shape): sleepers beside the
Clearway so every route passes within 3 of one; the mother or elite
deepest; the sneak line along `=`.
```
 . . . . z . . . . .
 . . . z . = z . . .
 . . . . . = . . . .
 . . z . . = . z . .
 . . . . . = . . . .
 . . . . L L L . . .
 . . . . . P . . . .
```

**Revel** (M2 showcase start shape): a Busy group clumped round an
existing RT piece (`t`), backs turned; at most 2 members inside any
radius 1 from F2; marksmen not on cover. On notice it walks to its
battle formation's Billets over 1–2 turns.
```
 . . . . . . . . . .
 . . m t t s . . . .
 . . . t t m . . . .
 . . . F . = . . . .
 . . . . . = . . . .
 . . . . L L L . . .
 . . . . . P . . . .
```
