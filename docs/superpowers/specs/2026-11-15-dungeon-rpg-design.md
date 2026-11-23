# Dungeon RPG: Design

Date: 2026-11-15
Status: approved by client, 2026-11-15

Items marked **(decided)** were agreed with the client, either directly or
by approving the lead's proposal. No (proposal) items remain since the
client's approval of 2026-11-15. (cross-document comparison 2026-11-23,
CC-1)

Markers added by later designs (cross-document comparison 2026-11-23,
CC-1):

- **(agreed YYYY-MM-DD, <design> Qn):** a client answer to question Qn
  of the named design, given on that date. Every agreed marker names its
  design ("hazards Q3"), so it never reads as a question of this
  document.
- **read**, **a reading**, **an addition** (after a change id): the
  change cites this passage and leaves its rule as written (read);
  interprets its existing wording without changing a rule (a reading);
  or adds a rule beside it without altering an existing one (an
  addition).
- **hook:** a need one design states and another owns; the asking
  design states only what it needs. **pending:** a request or open item
  awaiting the named owner's ruling or deliverable; it changes nothing
  until that owner rules.
- *(illustr.)*: an illustrative number, owned by the builders and
  calibrated by the simulator. Never binding.
- **M1–M4** name the milestones of section 9; **Later** means after M4,
  not scheduled.

## 1. Intent

A turn-based RPG played in runs through a randomly generated dungeon. The
appeal is building a character with tabletop-style depth (species, class,
multiclassing, a meaningful choice at almost every level) and seeing a
different build and a different dungeon every run.

The hard part of this game is rules complexity, not rendering. Class
features, items, conditions and enemy abilities will interact in ways nobody
plans for. The design is organised around keeping those rules cheap to test,
simulate and change.

## 2. Goals, non-goals, anti-goals

### Goals

- A complete run takes 1–2 hours. **(decided)**
- Permadeath: death ends the run. **(decided)**
- Some progress carries between runs. **(decided)**
- Every run plays differently: different dungeon, loot, enemies and build.
  **(decided)**
- Progression in the style of 5E: species and class at creation, class
  progression, multiclassing, a choice at almost every level. **(decided)**
- Dungeon has floors, bosses, and difficulty and rewards that scale with
  depth. **(decided)**
- The same seed and the same inputs always produce the same run.

### Non-goals

- Multiplayer.
- Console, mobile or browser releases. Target is PC. **(decided)**
- Hex grids.
- Full fidelity to the 5E rules. We borrow its shape, not its text.
- A story campaign.
- Official mod support. Data-driven content leaves the door open.
- Spreading, evolving or player-placed ground surfaces. Fixed condition
  squares placed by floor generation are allowed; see the conditions
  design, section 11. Exception (M3): environment-made Remnant clouds
  with a fixed footprint and a short countdown, which never drift,
  spread or grow (room terrain design 6.6; change TR-27, M3). **(agreed
  2026-11-21, room terrain Q5)** (cross-document comparison 2026-11-23,
  CC-1)
  Exception (M3; the reversal round M4): environment square changes
  only through pre-proven Stir states (Tides held whole in their two
  states, Cord Flips, Turning Ring boards, environment-moved
  Furnishings, environment Overturn), using existing square kinds,
  announced a round ahead, never spreading or growing (hazards design
  6.3, 7; change HZ-18, M3). **(agreed 2026-11-21, hazards Q3)**
  (cross-document comparison 2026-11-23, CC-1)

### Anti-goals

These would make the game a failure even if everything works.

- **Grind to win.** A player must not be able to substitute repeated runs
  for skill. This rules out permanent stat boosts.
- **Decision fatigue.** A level-up should take well under a minute. If the
  player manages four full character sheets, we have failed.
- **Rules in the display layer.** Any game rule implemented in a Godot
  script defeats the architecture.
- **Protected content.** No D&D name, and nothing outside the 5E SRD
  (CC-BY-4.0). Anything taken from the SRD carries the required attribution.
  The credits carry the verbatim SRD 5.2 (weapon masteries) and SRD 5.1
  (rarity names) CC-BY-4.0 attributions, and confirm they cover the armour
  table, the adapted magic-item mechanisms, the adapted trinket
  mechanisms and the adapted consumable, scroll and wand mechanisms;
  adapted SRD records, armour and trinkets included, carry an `adapted`
  flag (weapons design 19, armour design 15, trinkets design 16,
  consumables design 17).

## 3. Constraints

- Team: two developers, very experienced with Godot, strongest in C#.
  **(decided)**
- Engine: Godot 4 with C#. **(decided)**
- Platform: PC. **(decided)**
- Presentation is isometric 3D. 3D artists are available. **(decided)**
- During development the game uses a very simplified 3D version of that
  style: placeholder meshes and flat materials. **(decided)**
- Builds target Windows and Linux. **(decided)**
- Input is mouse and keyboard, routed through remappable actions so
  controller support can be added later. **(decided)**
  Exploration bindings, full rebinding and the accessibility package
  follow this (exploration and information design 4.10; change XI-20,
  read).

## 4. Game design

### 4.1 The run

- A run is 5 floors. Each floor takes roughly 15–20 minutes and ends in a
  boss. **(decided)** (run structure design 6.1; change RT-1)
- The hero reaches about level 10 by the end of a run, so a level-up arrives
  about every 8–10 minutes. **(decided)**
- Between floors the party reaches a camp: full rest, a shop or reward, and
  a **Muster**: a recruit offer at every camp while the party has an open
  slot, a swap offer otherwise; hiring is optional. **(decided)**
  (companions design 7.1, 7.11; change CMP-1) Every camp also has a basic
  vendor (supply, potions in limited pre-rolled stock, tonics, Remedy,
  mundane bases; 1 Wax Key (M2) only when the party has no
  Mechanics-trained member, change TL-25; 1 Ear Trumpet (M3,
  conditional) only when no party unit is Perception-trained, change
  XI-26) (cross-document comparison 2026-11-23, CC-10). "A shop or
  reward" is a choice between a notables shop (1–2 notables) and a camp
  reward card, separate from the boss reward card
  (enemies design 17.5). **(decided)** (items design 10.3, 10.4) Each
  camp shows the next floor's theme; when route choice is on (run
  structure design 6.11), camps 1–2 offer a choice of two next floors
  (run structure design 6.8–6.11; change RT-10), and when the Last
  Hollow choice is on (run structure design 6.11), camp 4 offers a pick
  of the final boss (change RT-11). The camp is presented as Arrival,
  the Hollow and Travel on, with the order unchanged; the camp reward
  card, vendor potion stock keyed to the party and trimmed in Core,
  Quick Restock (`BuyBatch`) and Accept kit defaults are in camp and
  hub design 4.2 and 7 (changes CH-1, CH-15 to CH-18, M2).
- A run can be saved and resumed, but there is one save slot per run and it
  is deleted on death. **(decided)**

### 4.2 Party

- The player builds one hero in full. **(decided)**
- Up to 3 companions (a party of 4). One slot opens at each of camps 1–3;
  at most one hire per camp plus one carried hire; any offer may be
  declined; one dismissal or swap per camp, at camp only. If the M2
  floor-time gate fails after the levers, the cap is 2: no new slot opens
  at camp 3, and a party below the cap there still gets a hire board.
  **(agreed 2026-11-20, companions Q2)** (companions design 7.1,
  7.4–7.6; change CMP-2) Companions have a fixed class and lighter
  progression: they level
  automatically, with a choice only at a few key levels. **(decided)**
- The player controls every party member in combat. **(decided)**
- The run ends when the hero dies. A companion reduced to zero hit points is
  downed, and gets back up on 1 hit point if the party wins the fight.
  **(decided, expected to be tuned in playtests)** A fight ended by the
  morale rule counts as a win for getting up (enemies design 14.6).
  A companion that gets up
  from its first downing on a floor gains Scarred: maximum hit points −10%
  until camp, removed by Medicine Treat at a short rest. See the conditions
  design, section 14. Items that prevent a drop to 0 (consumables design
  14) do not change this rule. A downed companion also gets up on 1 hit
  point when the party completes a short rest, and at camp; Medicine gets
  the same companion up at most once per fight; companions are never
  killed (death and healing design 11).

### 4.3 Grid, exploration and combat

- Square grid. Movement in 8 directions. One diagonal rule everywhere: a
  diagonal step costs the same as an orthogonal step. **(decided)**
- Exploration and combat happen on the same map. Combat starts in place when
  enemies notice the party. There are no separate battle arenas.
  **(decided)** A party unit on a boss arena square counts as noticed
  by the boss's group, and Take Places is the placement step of that
  combat (boss rooms design 4.2.1, 4.2.2; changes BR-3, M1, and BR-4,
  M2, a reading).
- Out of combat, the player moves the hero and companions follow. In combat,
  all units act in initiative order. **(decided)**
- Exploration is turn-stepped: the world advances one tick for each hero
  step or action. Effect durations and short rests are counted in ticks.
  **(decided)** In combat, conditions on units count down on the wearer's
  turns; windows, auras and concentration effects count on the source's.
  Out of combat, one round is one tick. See the ability and resource
  model, section 9.6, and the conditions design, sections 5.1–5.2.
- Enemies outside combat are dormant, stationary or patrolling, set per
  group within a per-monster set in data: each monster type lists
  `allowedAwareness[]`, and the encounter template and the group's
  activity choose within it (encounter placement design 6.7; change
  EP-9, agreed encounter placement Q4, M2). **(decided)** "Dormant" is
  a behaviour, not the Asleep condition (conditions design 5.5).
- Each unaware enemy group makes one perception check per trigger, using
  its best member in line of sight, against the party's passive stealth,
  never an opposed roll; see the character stat model. Triggers: first
  line of sight, the party coming within 6 and then 3 squares, noise, and
  an attack. The party notices non-hiding enemies automatically on line of
  sight; hiding enemies roll Stealth against the party's passive
  perception (enemies design 12.2). A side that is noticed
  without noticing back is surprised and loses its first turn. **(decided)**
  Surprise is judged per side for the party and per group for monsters:
  the party is surprised only if no party unit has noticed any hostile
  unit taking part in that combat; each monster group is surprised if
  the party noticed it and it is unaware (encounter placement design
  6.8.2; change EP-2, agreed encounter placement Q1; recorded M1, in
  effect M2).
  Exception: the solo hero is never surprised in floor 1's first two
  main-path rooms (death and healing design 17.1). A note only, Later:
  if `SurpriseDeathShare` misses, a 2024-style surprise (disadvantage on
  initiative instead of a lost turn) goes to the client as a fallback
  (encounter placement design 9.1; EP-41 note, Later).
- Enemies within hearing range of a fight join it. Joiners move at their
  speed, are announced at least 1 round before they arrive (arrival is
  delayed if needed to keep the warning), and enter at the next round
  start. Joining groups are capped per fight: 1 on floors 1–2, 2 deeper;
  groups beyond the cap do not change state (encounter placement
  design 6.12). An Errant that ignores this joining rule is recorded
  only as a Later item for the client (encounter placement design 9.1;
  EP-38, Later). Combat ends when no hostile
  unit is aware of the party, so fleeing by breaking line of sight is
  possible. Combat also ends as a win when every hostile still in the
  fight is Routed (enemies design 12.4, 14.6). **(decided)** A Windfall
  Truant is never a hostile unit, so this rule holds as written (loot
  sources design 4.7.4; change LP-5, read, M3).
- Reactions resolve automatically by default, following a per-reaction
  setting the player can change to "ask me". **(decided)**
- Units can gain a jumping or flying state. These exist only to cross
  specific hazards and to help horizontal movement: a jumping or flying unit
  ignores hazards and obstacles tagged as crossable by that state. They do
  not affect line of sight, and there are no high-ground rules. All
  calculations stay on the flat grid. **(decided)** Void squares are
  flat hazards crossable by both states (hazards design 6.4; change
  HZ-9, M2 (gated), else M3).
- A unit's turn has movement, one action, one bonus action, and one reaction
  per round. **(decided)**
- Line of sight and fog of war use one symmetric visibility rule: if A can
  see B, B can see A. **(decided)** One core function, `CanSee`,
  implements it: symmetric shadowcasting between transparent squares, no
  range cap, units never block sight, and sight may glance past a single
  corner where movement may not. Downed party units contribute no
  knowledge; the geometry stays symmetric, so a monster can still see a
  Downed unit (exploration and information design 4.1.2; change XI-2, a
  reading, M1). The party's fog is `PartyKnowledge`, held in the core per
  floor: each square is Unknown, Chalked (architecture known, never yet
  seen), Seen (in line of sight of a living, non-Downed party unit now)
  or Remembered (seen before, not now), and square knowledge never goes
  back (exploration and information design 4.1.1; change XI-1, a
  reading, M1 states, M2 the rest). Telepathy, Clairvoyance, Arcane Eye
  and Spyholes stay out (exploration and information design 4.5.3;
  change XI-32, kept).
- Core resolution is a d20 roll plus modifiers against a target number, with
  advantage and disadvantage. **(decided)**

### 4.4 Resting and attrition

- No full rests inside a floor. A full rest happens at the camp between
  floors.
- Short rests inside a floor are limited by a consumable supply.

Both are decided. The purpose is to make resource use across a floor a
real decision, which 5E relies on a DM to enforce.

### 4.5 Progression

- At creation the player picks a species and a class. **(decided)**
- The first release of the full run has four classes. More arrive through
  unlocks. **(decided)**
- Every level offers a choice. Where the class table has nothing to offer,
  the game presents a pick-one-of-three offer drawn from a pool of feats,
  boons and upgrades suited to the build. **(decided)**
- Multiclassing is added only once the single-class paths are fun.
  **(decided)**

### 4.6 Dungeon, enemies and loot

- Each floor is generated from the run seed as rooms and corridors, with a
  guaranteed path from entrance to boss. Rooms are joined by Portals
  (doorways, doors, Arches, flaps and corridors of length 0 or more);
  open-plan themes are partitioned into ordinary rooms that read as one
  place on screen (floor layout design 6, 7; change FL-1).
- Each floor has a theme that selects its tiles, enemies and hazards.
  Hazards include fixed condition terrain (conditions design 11).
  Themes are drawn per floor from tiered pools (floor 1; floors 2–3
  band; floor 4), or picked from two drawn candidates when route choice
  is on (run structure design 6.11), with no repeat in a run; floor 5 is
  always the fixed final theme with a final-boss pool (run structure
  design 6.3–6.5; change RT-2).
- Enemies are placed using a difficulty budget that grows with depth:
  party weight × band% × depth% × ladder% (enemies design 10.1).
- Notable loot is a fixed per-floor budget, split by channel and pre-rolled
  on the loot stream. Enemies carry part of it and never add to it
  (enemies design 15.3). A Windfall Truant's card comes out of this
  budget, never on top of it (loot sources design 4.7.6; change LP-1,
  read, M3).
- Bosses are hand-designed. Which boss appears on a floor is drawn from a
  pool; the final boss may be picked at camp 4 when the Last Hollow
  choice is on (run structure design 6.11; change RT-11).

All decided.

### 4.7 Progress between runs

**(decided)**

- **Content unlocks lead.** Milestones add species, classes, subclasses,
  companions and loot to the pool. Examples: beat the floor 2 boss; win as a
  multiclass character. From M3, themes and final bosses also join the
  unlockable content, never power (run structure design 8.12; change
  RT-18, M3).
- **One starting boon.** Before a run the player picks a single earned boon,
  such as an heirloom item or a background perk. Boons add variety and do
  not stack. Starting kits get no hub peg: kit variants ship only as
  boon options; charms get no hub surface; meta-curses become one
  reserved run-modifier slot on the hub's Depth gauge, owned by the
  main design and run structure difficulty decision (camp and hub
  design 4.9, 2.2; change CH-24, read, M3/Later).
  Kit variants are boons with one bundled skill, 0 VP, under this
  rule as written (starting kits design 4.4; change SK-7, read,
  agreed 2026-11-23, starting kits Q1, M3); the CH-24 sentence above
  is read as written (starting kits design 13). Pending (owed by this
  document with run structure): a run modifier may change kit fields
  only through declared `kitDelta` entries in `configHash`, with a
  reserved V21 check (starting kits design 4.7, 9.2 (m), 14.2; change
  SK-15, request, Later).
- **Difficulty ladder.** Each win unlocks an optional harder tier. From
  M3, Nightmare tiers add kind, not stats, and the Perilous Door makes
  the ladder per-floor (run structure design 9.1; change RT-21, M3).
  The Perilous toggle sits on the camp door station, on Two Doors cards
  only (camp and hub design 7.6; change CH-20, read, M3); pending: the
  run structure owner's ruling on single-door cards and camp 4
  (fallback: not offered there).
  Hazards escalate in kind under this rule (hazards design 6.19;
  change HZ-25, M3).
  Bosses escalate in kind too: each boss authors a Nightmare
  Rendition, the Perilous Door uses it one tier up, and HP, damage,
  pips and thresholds never change (boss rooms design 4.14.2; change
  BR-34, M3).
  Unlocks, the ladder and Nightmare tiers leave the loot economy
  unchanged (loot economy design 6.12; change LE-19, M3).
  Events never ease a run for a losing streak: no mercy through events,
  confirmed; help for struggling players belongs in a visible difficulty
  setting, which is a future main design and run structure decision, not
  yet made (events design 4.4.7, 14.2; change EV-10, read, agreed
  2026-11-22, events Q4). Pending (owed by this document with run
  structure): until it is decided, the hub's Depth gauge keeps an unlit
  lamp reserved for it below tier 0, and no figure substitutes for it
  (camp and hub design 4.8, 14.2; M3).
- **No permanent stat boosts.** Curiosity gains are run-only, and from
  M3 Curiosity Sequels and Ripening enter the drawable set through the
  unlock snapshot, never as power (events design 4.4.1, 4.5.8, 4.5.9;
  changes EV-7, EV-27, EV-28, read; M3 / M3-late).
- **Hub.** A simple camp screen to begin with. A richer hub is out of scope
  until the run itself is proven. In M2 the hub is that simple screen:
  one static Stairtop backdrop with menu rows (Begin, Continue, Seeded
  night, Book of Nights, Profiles, Settings, the Hearsay log), holding
  one static figure, the Copyist, and the floor-k Lingerer vignette
  after a death on floor k (camp and hub design 4.8, 7.7; change CH-13,
  agreed 2026-11-23, camp and hub Q4, M2). From M3, after M2 sign-off,
  it becomes a menu-room that is never walkable, holding the hub
  stations and up to 3 settled Lingerers (camp and hub design 4.8, 5.2;
  change CH-6, a reading, agreed 2026-11-23, camp and hub Q5, M2/M3).
  Its fiction is the Stairtop, the one hollow that does not re-form
  (agreed 2026-11-23, camp and hub Q1). Hub figures give no mechanical
  or informational effect (camp and hub design 4.17; agreed 2026-11-23,
  camp and hub Q2).

## 5. Approaches considered

### A. Pure C# rules core, Godot as the display layer (chosen)

All rules live in a C# class library with no Godot references. Godot renders
state and sends input back as commands. Content lives in data files.

- Gains: tests run without the engine; seeded runs are reproducible; saves
  and replays fall out of the design; a command-line simulator can run
  thousands of fights for balance.
- Costs: a bridge between core events and Godot scenes; no inspector editing
  of content.

### B. Godot-native

Rules in nodes and scripts, content in custom Resources.

- Gains: fastest start, best editor tooling.
- Costs: rules are hard to test without the engine, determinism is harder to
  guarantee, and balance simulation needs headless Godot.

### C. Pure core, content authored as Godot Resources

As A, with content edited in the inspector and converted by an adapter.

- Gains: A's testability with comfortable editing.
- Costs: every content type is defined twice and must be kept in sync.

### Decision

The client chose **A**. The risk in this project is rules complexity, and A
is the only option that makes the rules cheap to test and simulate. The team
is strongest in plain C#. If hand-editing data files becomes painful, an
editor tool or C's adapter can be added later without touching the core.

## 6. Architecture

### 6.1 Projects

| Project | Kind | Purpose |
|---|---|---|
| `src/Core` | C# class library, no Godot references | All rules and state |
| `src/Sim` | C# console app | Runs headless fights and runs for balance |
| `tests/Core.Tests` | C# test project | Unit, property and replay tests |
| `game/` | Godot project | Rendering, input, audio, UI |
| `content/` | JSON data files | Classes, species, feats, items, monsters, tables |

`Core` targets the .NET version required by the Godot 4 release the team
picks. `game/` references `Core` as a project reference.

### 6.2 The core loop

1. The display layer, or an AI, submits a **command**: move, attack, cast,
   use item, end turn, choose level-up option.
2. The core validates the command against the current state.
3. The core applies it and returns a list of **events**: unit moved, attack
   rolled, damage dealt, condition applied, unit died, combat started.
4. The display layer plays those events as animation and sound. It never
   changes game state itself.

The core can also stop part-way through resolving a command and wait for a
**decision**: a level-up pick, a prompted reaction, a choice of target for a
triggered effect. While a decision is pending, the only valid command is an
answer to it. The pending decision is part of the state, so it is saved,
replayed and answered by AI like anything else. This is built in M0, because
it is expensive to add later.

The display layer may ask the core questions, such as "which squares can
this unit reach?" or "what are the odds of this attack?", but it never
computes rules. Fog, the map and every knowledge mark are drawn from core
queries over `PartyKnowledge` (exploration and information design 4.1.1,
4.11; change XI-1, read).

### 6.3 Determinism

- The core uses its own seeded random number generator, not the platform
  one, so results do not change between runtime versions. It is a
  SplitMix64 key algebra: every child key is derived from its parent's
  key, label and indices, never from another stream's counter, and every
  bounded integer uses multiply-shift `Bound` (seeds and validation
  design 4.1; change SV-1, M1).
- Separate random streams for dungeon generation, combat rolls and loot.
  Opening a chest must not change the next attack roll. A `recruit` stream
  sits beside them, with per-camp, per-slot and gap substreams and
  `armReroll`, `eyeReroll`, `cosmetic` and reserved `pledge` (Later)
  children (companions design 7.2; change CMP-3). A `route` child of
  the dungeon stream is consumed only at run start and draws each
  floor's theme and boss; each floor generates from a dungeon substream
  keyed by floor index (run structure design 6.4; change RT-3).
  `dungeon`, `loot` and each floor substream are never drawn; they only
  derive children. Every stream is a row of the stream registry (path,
  count class, count, stage, retry scope, owner, wave), and core code
  opens a stream only through its registry handle (seeds and validation
  design 4.2, 4.3; changes SV-2, SV-31, M1). Each floor substream's
  generation children (`layout`, `terrain` and its `hazards`, `traps`,
  `encounters`, `trapsResolve`, and dressing) sit under an
  `attempt/{a}` child: a = 0 normally, and a floor retry takes the next
  attempt (a keyed child, never `seed + 1`); a single-stage retry uses
  that stage's keyed `retry1` child. `curiosities`, `checks`, every
  loot and recruit key and (Later) `trader` are attempt-invariant, so a
  retry never moves them (seeds and validation design 4.3, 5.5;
  changes SV-4, SV-31, M1; `curiosities` M2). Each floor's dungeon
  substream has a `layout` child, drawn with fixed counts
  per Blueprint; the party-agnostic graph is drawn first and the party
  band picks only size classes; dressing draws from a display-side
  stream the core never reads (floor layout design 6.1; change FL-2).
  A `terrain` child of each floor's dungeon substream, drawn after
  layout with fixed counts, places room terrain; every piece on a
  walkable square comes from it, not from dressing (room terrain design
  6.2.5; change TR-17).
  A `hazards` grandchild of `terrain` (`terrain/hazards`), drawn after
  all terrain with fixed counts, places hazards (hazards design 11; change
  HZ-28; seeds and validation design 4.3; change SV-31, M2).
  A `traps` child of each floor's dungeon substream, drawn after
  hazards and before encounter placement with fixed counts, draws trap
  and lock geometry and kinds; a `trapsResolve` child with fixed draws
  runs after encounters are placed and binds traps and locks to them.
  Object checks (pick, disarm, force and the like) draw from a keyed
  `checks` stream with no unit in the key and an `attemptIndex` per
  check kind; out-of-combat saves forced by traps or consequence locks
  (keyed by `springIndex`) and out-of-combat attacks on objects also
  draw from `checks`, so a trap or lock handled between fights never
  moves the next fight's rolls (traps and locks design 6.22; change
  TL-3, M1). The same keyed-check spec, with one registered domain per
  roll kind, also covers out-of-combat noticing (`h:notice`, M1) and
  out-of-combat Steal (`h:steal`, reserved M1, live M2), lurker Stealth
  (`h:lurkStealth`, M2), out-of-combat hazard saves (`h:envSave`: falls
  M2, Plunge and Dare rooms M3) and Curiosity checks (`h:curiosity`, M2)
  (cross-document comparison 2026-11-23, CC-13, CC-97); in-combat
  noticing, Steal and hazard saves stay on the combat stream. So no
  roll made outside a fight, including the noticing roll that starts
  one, ever moves the next fight's rolls (seeds and validation design
  4.3; change SV-6, M1 / M2 / M3).
  An `encounters` child of each floor's dungeon substream, drawn after
  `traps` and before `trapsResolve` with fixed counts and dummy draws,
  places enemy groups, so a placement retry never moves layout,
  terrain, hazards or traps (encounter placement design 6.1; change
  EP-1, M1). It is a node with a `plan` sub-key (slot plan, step 2a)
  and a `place` sub-key (placement), and its retry is
  `encounters/retry1` (seeds and validation design 4.2, 4.3; change
  SV-31, M1).
  A `lootPlan` child of the loot stream, keyed by floor index, is
  drawn in a new LootPlan step before the approved loot draws; each
  consumer draws from its own keyed grandchild with fixed counts, so
  adding a consumer never shifts another's draws. The approved loot
  draws (step 3) are keyed per floor (`loot/floor/{N}`) and per entry
  block (`loot/floor/{N}/{block}/{entry}`); their approved order is kept
  as the order in which blocks are evaluated, and the count dummies and
  ghost draws that held positions on one sequential stream are retired.
  The Reprieve Phial pre-roll (`loot/run`) is always 3 draws, pending the
  consumables owner's confirmation; fallback in seeds and validation
  design 14.2 (seeds and validation design 4.3; change SV-5, M1)
  (cross-document comparison 2026-11-23, CC-12). A draw-free LootBind
  step runs after `trapsResolve`, and floor N+1's generation and the
  camp-exit save are one write (loot economy design 6.10; change
  LE-14, M1).
  A `curiosities` child of each floor's dungeon substream, a fixed 12
  draws per floor *(illustr.)* in a fixed draw map, is drawn by
  EventDraw after layout and before hazards; the draw-free EventPlace
  runs after LootBind, and loot never reads Curiosities (events design
  4.10; change EV-21, M2).
  A per-record `boss9` keyed child of the route seed, 4 fixed draws,
  never consumed on the route stream, draws each boss's Rendition and
  mirror bit; arena stamping makes no draws (boss rooms design 4.10.1,
  4.15; change BR-2, M2).
- Each floor is generated in one canonical order (GS0–GS17) and
  checked at two assertion points: AP1 after traps, before loot (HE-7's
  point), and AP2 after LootBind and EventPlace. A failed check climbs
  one retry ladder (LR0–LR5) in every build, at most 3 attempts; in a
  release build a floor that still cannot be built ends the run cleanly
  (agreed 2026-11-23, seeds Q2) (seeds and validation design 4.4, 5.4, 5.5;
  changes SV-9, SV-10, M1 / M2).
- Rules use integer arithmetic only.
- No reliance on unordered collection iteration. Both are enforced by
  the Core analyzer (seeds and validation design 4.5, DS11; change
  SV-28, M1).

### 6.4 Effects system

Every rule exception is expressed the same way, whether it comes from a
class feature, a feat, an item, a condition or a monster ability:

- **Modifiers** change a number or a flag: +2 to armour class, advantage on
  a save, resistance to fire.
- **Triggers** react to a rule event: on hit, on taking damage, at turn
  start, when an ally is attacked.

The rules engine raises events at fixed points (attack roll, damage, saving
throw, movement, turn start and end) and asks all active effects to modify
or respond. New content should need new data, and only rarely new code.
This system is the main technical risk, and the vertical slice exists
largely to prove it.

### 6.5 Content

- Content is JSON, loaded and validated at startup. A bad reference or a
  missing field fails loudly with the file and field named.
- Every content item has a stable string ID.
- Effects that cannot be expressed in data are implemented in C# and
  referenced from data by ID.

### 6.6 Saving

- **Run save:** a snapshot of the full core state, written at safe
  points; while a run is on a floor, the snapshot is the one suspend slot
  below. The snapshot is the authority. Resolving a Curiosity is a safe
  point, with a fixed write order, so a reload cannot undo a cost or
  re-roll (events design 4.10; change EV-21, a reading, M2). The run
  snapshot includes `PartyKnowledge` (exploration and information design
  4.1.1; change XI-1, read). Every generated floor is stored with the
  run as an immutable static floor file (`FloorStatic`, named by its
  content hash) with the `GenInputs` it was generated from, so a load
  never regenerates a floor and a patch never changes one already
  built. Every save file carries the run's `runId` and a monotone
  `seq`, and saves follow fixed write orders (WO1–WO8: run start, on a
  floor, Curiosity resolve, floor exit, Travel on, death, victory or
  unravel, load, camp command); floor N+1 is generated at Travel on and
  committed with the camp-exit save (seeds and validation design 4.8;
  change SV-14, M1 run save and log, M2 the rest).
- **Suspend slot (permadeath):** while a run is on a floor, one suspend
  slot holds the full core state: a snapshot at floor entry
  (`suspend.base`) plus a state-delta journal (`suspend.journal`) with
  one record appended and flushed after every committed command (every
  step, act, combat action, death, Down and paid act), compacted into a
  fresh base often; quitting writes it too. The base is written to a
  temporary file, flushed, then atomically replaced over the slot. No
  frame whose content depends on a draw, a reveal, a Down, a death, a
  spent counter or a paid act is shown until that command's write has
  landed; the same rule covers camp commands that draw, spend or reveal
  (the outcome rule). Loading reads and validates the slot, then
  replaces it atomically with the re-validated state, then accepts
  input; this meets "deleted on load" (seeds and validation design
  4.8.3, 4.8.4; change SV-14, agreed 2026-11-23, seeds Q4, M2). The
  slot always wins over the safe-point snapshot, and `PartyKnowledge` is
  forward-only, so a resume never undoes a death, a spent counter or a
  look. Camp and other safe points are unchanged, and no suspend slot
  exists outside a floor (exploration and information design 4.12;
  change XI-28, agreed 2026-11-23, exploration Q1, M2; the file
  mechanics are seeds and validation design 4.8) (cross-document
  comparison 2026-11-23, CC-1).
- **UI annex of the run save:** display state only (Waymarks and
  dismissed loose ends), outside the reproducibility key and never read
  by rules, the simulator or validators (exploration and information
  design 4.3.4, 4.7; change XI-7, an addition, M2).
- **Command log:** kept alongside for replays, bug reports and tests. A log
  may stop replaying correctly after a content change, which is why it is
  not the save. A replay is valid only when its reproducibility key and
  `rulesVersion` match the build (seeds and validation design 4.7;
  change SV-17, read, M1).
  Open item (pending, owed by this document): the game's telemetry
  consent setting, under which a generation defect's local bug-report
  bundle is sent (seeds and validation design 5.5; hook, agreed
  2026-11-23, seeds Q2). Until it exists, bug reports stay local and the
  player can attach them by hand (seeds and validation design 14.2).
  The same setting is a Settings row in the hub, and the camp and hub
  reports `CampSeconds`, `CampClicks`, `RunWallClock`, `HubSeconds` and
  `DeathPathSeconds` come from human sessions (seeds and validation
  design 6.11) until it exists (camp and hub design 4.5, 4.13, 9.2;
  hook, pending; M2).
- **Profile save:** unlocks, boons and difficulty tier, stored separately
  from any run. The grants that change rules (difficulty tier, boons,
  Nightmare tier) enter the run's `configHash` (seeds and validation
  design 4.6; change SV-12, read, field M1, live M3). From M2 it also
  holds `curiosityMemory` (version, checksum, `shownCount`, `fullShown`,
  `recentRuns`, `last`, `pendingClosure` and the rest of events design
  9.1): display and launcher memory of the Curiosities seen, outside the
  reproducibility key; a file failing its version or checksum reads as
  empty. Personal
  runs write every field; seeded runs (shared, daily and typed codes)
  write `shownCount` and `fullShown` only, plus their marked
  `runHistory[]` entry below; the simulator writes no player profile
  (launcher-dependent arms write sandbox profiles in its launcher
  emulator only) (events design 4.5.1, 4.10, 9.1; change EV-19, agreed
  2026-11-22, events Q1, M2; seeds and validation design 4.6, 4.7; changes
  SV-13, SV-16, M2).
  From M2 it also holds `runHistory[]`: one record per finished run,
  personal or seeded, with `seedMode`, the result, the floor reached and
  the code. A seeded run appears there marked seeded and advances no
  unlocks, records, codex entries or launcher memory; `recentRuns` is
  never written for a seeded run (seeds and validation design 4.7, 4.9;
  change SV-16, agreed 2026-11-23, seeds Q1, M2) (cross-document
  comparison 2026-11-23, CC-1).
  Each `recentRuns` entry also holds `f1BossId`, written at run start
  for every launched personal run and read by the launcher's F1-boss
  streak rule (memory 2); outside the reproducibility key (boss rooms
  design 4.10.3; change BR-25, agreed 2026-11-22, boss rooms Q1, M2).
  Beside it, each entry holds `f1ThemeId`, written at run start for
  every launched personal run and read by the floor-1 theme redraw
  (run structure design 6.4, R-SEED-6: each of the last three personal
  runs); outside the reproducibility key (M2) (cross-document comparison
  2026-11-23, CC-6). From M3 it
  also holds `bossMemory {bossId: lastOutcome, renditionsSeen[]}`,
  display only for the Boss Ledger and Grudge, read as a snapshot at
  run start and never by draws, stats or AI (boss rooms design 7;
  change BR-35, M3).
  From M2 it also holds `uiMemory`, display only and outside the
  reproducibility key: one-line first-sight callouts per mark family for
  staged first-run disclosure; the same seed gives identical knowledge
  and previews on every profile (exploration and information design
  4.10; change XI-21, an addition, M2).
  From M2 it also holds `loreMemory` (version, checksum, `nightIndex`,
  `loreShownCount`, `loreLastShownNight`, `lastFiredNights`,
  `lingererLastOnBalcony`, `deferredT0[]`, the bounded `hearsay[]` and
  the rest of camp and hub design 9.1): display memory of the ambient
  lines heard at camp and hub, outside the reproducibility key and
  never referenced by Core; a file failing its version or checksum
  reads as empty. Lore selection uses the display domain `lore`,
  outside the reproducibility key. Personal runs flush their lore marks
  through `pendingProfile`, and leaving the hub rewrites the profile
  with the hub's lore marks (personal Nights only); seeded runs,
  replays and the simulator write no lore (camp and hub design 4.16,
  9.1; changes CH-7, M2; CH-9, read).
- Saves carry a version number. A run's reproducibility key is
  (`runSeed`, `genVersion`, `manifestHash`, `configHash`, `unlockHash`);
  saves also carry `rulesVersion` and `saveVersion`, and each floor's
  generation identity is the key plus that floor's `GenInputs` digest
  (seeds and validation design 4.6; change SV-12, M1 fields / M2 / M3).
  Every `saveVersion` bump ships a migration. A run in progress when a
  patch lands continues: floors already generated are kept exactly,
  later floors use the new version, the configuration, unlock and
  `drawableSet` snapshots and route state never change, and a patch
  that can change the run marks it not comparable for records and
  shared codes (seeds and validation design 4.10, 4.10.1; change SV-15,
  agreed 2026-11-23, seeds Q3, M2).

### 6.7 Enemy AI

Lives in the core and submits the same commands a player does. Each enemy
scores its available actions and picks the best, with behaviour tuned per
monster in data.

### 6.8 Display layer

- Holds a view of the core state and updates it from events.
- Queues events so animations play in order while the core has already
  moved on.
- Owns input, camera, UI, audio and visual effects only.
- Renders in 3D with an orthographic camera at a fixed isometric angle.
  **(decided)**
- The rules grid stays flat: one level per floor, no height in the rules.
  Jumping and flying are movement states on that flat grid (section 4.3);
  the display layer draws them as raised or arcing units. **(decided)**
- The camera rotates in 90 degree steps, and walls between the camera and
  the party are cut away or faded so units are never hidden. **(decided)**
  A rule mark on a cut-away or faded wall projects a front-square
  half-rim decal, readable at all 4 rotations (exploration and
  information design 4.9.7; change XI-18, M2).
- Each floor is assembled from 3D tile pieces chosen by the floor theme, so
  placeholder pieces can be swapped for final art without code changes.
  **(decided)**
- Mouse picking maps a screen position to a grid square in one place, used
  by all movement and targeting.

## 7. Testing

- **Unit tests** for every rule: attack resolution, conditions, each class
  feature.
- **Property tests** for generation: every floor is connected, the boss is
  reachable, nothing spawns inside a wall. Validator V19 `Conformance`
  checks the seed machinery itself (every stream registered, every
  stream draws exactly its declared count, no clock, profile or OS
  read), alongside golden seeds and the release seed set (seeds and
  validation design 4.11, 5.2; changes SV-18, SV-19, M1 / M2).
- **Replay tests:** recorded command logs must reproduce the same final
  state.
- **Balance simulation:** `Sim` plays many seeded fights with AI on both
  sides and reports win rates and fight lengths per build and floor. It
  is one simulator with six instruments, and every report row declares
  its result class, which says in advance what a miss does (seeds and
  validation design 6.1, 6.3; changes SV-21, SV-23, read, M1 skeleton,
  M2).
- **Content validation** runs as a test, so a broken data file fails the
  build.
- All of the above run with `dotnet test`, with no engine needed. CI
  tiers (pull request, nightly, weekly, sign-off), their time budgets,
  per-OS jobs and the minimum-spec runner are in seeds and validation
  design 5.8 (change SV-28, read, extended, M1).
- The Godot layer is checked by manual playtest at each milestone.

## 8. Ways of working

- Work in vertical slices: each slice is playable end to end.
- One main branch, short-lived feature branches, each change reviewed by the
  other developer.
- Automated build and tests on every push.
- A playtest at the end of every milestone, with the question "is this fun?"
  answered before more content is added.
- Significant decisions are recorded in `docs/`.

## 9. Milestones

| Milestone | Contents | Question it answers |
|---|---|---|
| M0 Skeleton | Solution, automated tests, command and event loop, Godot draws a placeholder 3D isometric grid and moves a unit | Does the core and display split work? |
| M1 Vertical slice | One class, levels 1–3, one generated floor, three enemy types, one boss, basic loot, permadeath | Is the loop fun? Does the effects system hold? |
| M2 Full run | Four classes, companions, 5 floors (change RT-1), level 10, pick-one-of-three offers, save and resume | Does a 1–2 hour run hold together? |
| M3 Between runs | Profile (the profile file and its `curiosityMemory`, `recentRuns[].f1BossId`, `runHistory[]`, `uiMemory` and `loreMemory` fields arrive in M2: section 6.6; events design 9.1; change EV-19, agreed events Q1; cross-document comparison 2026-11-23, CC-11; camp and hub design 9.1, change CH-7), the menu-room Stairtop hub (the M2 hub is the simple screen of section 4.7; camp and hub design 4.8, 10; change CH-6), unlocks, starting boons, difficulty ladder; in-run content: Two Doors at camp 3, Omens, Seams, Nightmare tiers (run structure design 12; change RT-19, M3) | Do players come back? |
| M4 Breadth | Species, multiclassing, more content, balance passes with the simulator | Is every run different? |

M1 covers species selection only if it is trivial to include; otherwise
species arrive in M4.

The sign-off checklists for M1, M2 and M3 are in the seeds and
validation design 7 (change SV-29, read, M1–M3).

## 10. Left to the builders

Abilities, derived numbers, skills, damage types and resistances are settled
in `2026-11-16-character-stat-model-design.md`. What that document leaves
open is listed in its own section 12.

Weapons and foci are settled in `2026-11-17-weapons-design.md`; they are no
longer pending. Disarmed and Coated stay Later; starting kits are set
in the four class designs (weapons design 25, 26) (cross-document
comparison 2026-11-23, CC-9).

Armour and shields are settled in `2026-11-17-armour-design.md`; they are
no longer pending (armour design 21, 22).

Trinkets are settled in `2026-11-17-trinkets-design.md`; they are no
longer pending (trinkets design 22).

Consumables, scrolls and wands are settled in
`2026-11-17-consumables-design.md`; they are no longer pending. Coatings
and Coated stay Later (consumables design 23, 24).

Hit points, Recovery, crits, the hero's death, Downed companions, the
healing guarantee and drop prevention are settled in
`2026-11-17-death-healing-design.md`; they are no longer pending (death
and healing design 26, 27).

The four launch classes (Vanguard, Shade, Arcanist, Herald), class
structure and concepts are settled in
`2026-11-17-classes-concepts-design.md` (classes design 14, 19; change
CL-13a). Feature lists, subclasses, capstones and kits are settled in
the four approved class designs: `2026-11-18-vanguard-class-design.md`,
`2026-11-18-shade-class-design.md`, `2026-11-20-arcanist-class-design.md`
and `2026-11-20-herald-class-design.md` (cross-document comparison
2026-11-23, CC-9).

The random number algorithm, the stream registry, the save file
mechanics and validation are settled in
`2026-11-23-seeds-validation-design.md`; they are no longer pending
(seeds and validation design 4.1, 4.8, 16; change SV-1).

The camp and the hub (camp form, stations, the ambient figures and their
lore, hub form by wave) are settled in
`2026-11-23-camp-hub-design.md`; they are no longer pending, and its
hooks on this document are taken up in sections 4.1, 4.7, 6.6 and 9
(camp and hub design 11, 13; changes CH-6, CH-7, CH-9, CH-13, CH-20,
CH-24). Telemetry consent and the visible easier setting stay pending
here (sections 6.6, 4.7).

Starting kits (default hero kits, companion kits, kit variants, given
names and Oddments) are settled in `2026-11-23-starting-kits-design.md`;
they are no longer pending, and its hooks on this document are taken
up in section 4.7 (starting kits design 11, 13; changes SK-7, read;
SK-15, request, pending).

- All numbers: hit points (shape settled there), damage, experience curve, loot rates.
- The dungeon generation algorithm.
- The JSON schema details.
- UI layout and art style.
- The Godot version.

## 11. Open items for the client

None.
