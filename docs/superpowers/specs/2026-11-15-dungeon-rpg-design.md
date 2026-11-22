# Dungeon RPG: Design

Date: 2026-11-15
Status: approved by client, 2026-11-15

Items marked **(decided)** were agreed with the client, either directly or
by approving the lead's proposal. Items marked **(decided)** are the lead's
calls and have not been confirmed.

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
  spread or grow (room terrain design 6.6; change TR-27, M3).
  Exception (M3; the reversal round M4): environment square changes
  only through pre-proven Stir states (Tides held whole in their two
  states, Cord Flips, Turning Ring boards, environment-moved
  Furnishings, environment Overturn), using existing square kinds,
  announced a round ahead, never spreading or growing (hazards design
  6.3, 7; change HZ-18, M3). **(agreed 2026-11-21, Q3)**

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
  mundane bases). "A shop or reward" is a choice between a notables shop (1–2
  notables) and a camp reward card, separate from the boss reward card
  (enemies design 17.5). **(decided)** (items design 10.3, 10.4) Each
  camp shows the next floor's theme; when route choice is on (run
  structure design 6.11), camps 1–2 offer a choice of two next floors
  (run structure design 6.8–6.11; change RT-10), and when the Last
  Hollow choice is on (run structure design 6.11), camp 4 offers a pick
  of the final boss (change RT-11).
- A run can be saved and resumed, but there is one save slot per run and it
  is deleted on death. **(decided)**

### 4.2 Party

- The player builds one hero in full. **(decided)**
- Up to 3 companions (a party of 4). One slot opens at each of camps 1–3;
  at most one hire per camp plus one carried hire; any offer may be
  declined; one dismissal or swap per camp, at camp only. If the M2
  floor-time gate fails after the levers, the cap is 2: no new slot opens
  at camp 3, and a party below the cap there still gets a hire board.
  **(agreed 2026-11-20, Q2)** (companions design 7.1, 7.4–7.6; change
  CMP-2) Companions have a fixed class and lighter progression: they level
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
  **(decided)**
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
  EP-9, agreed Q4, M2). **(decided)** "Dormant" is a behaviour, not the
  Asleep condition (conditions design 5.5).
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
  6.8.2; change EP-2, agreed Q1; recorded M1, in effect M2).
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
  fight is Routed (enemies design 12.4, 14.6). **(decided)**
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
  see B, B can see A. **(decided)**
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
  (enemies design 15.3).
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
  not stack.
- **Difficulty ladder.** Each win unlocks an optional harder tier. From
  M3, Nightmare tiers add kind, not stats, and the Perilous Door makes
  the ladder per-floor (run structure design 9.1; change RT-21, M3).
  Hazards escalate in kind under this rule (hazards design 6.19;
  change HZ-25, M3).
- **No permanent stat boosts.**
- **Hub.** A simple camp screen to begin with. A richer hub is out of scope
  until the run itself is proven.

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
computes rules.

### 6.3 Determinism

- The core uses its own seeded random number generator, not the platform
  one, so results do not change between runtime versions.
- Separate random streams for dungeon generation, combat rolls and loot.
  Opening a chest must not change the next attack roll. A `recruit` stream
  sits beside them, with per-camp, per-slot and gap substreams and
  `armReroll`, `eyeReroll`, `cosmetic` and reserved `pledge` (Later)
  children (companions design 7.2; change CMP-3). A `route` child of
  the dungeon stream is consumed only at run start and draws each
  floor's theme and boss; each floor generates from a dungeon substream
  keyed by floor index (run structure design 6.4; change RT-3). Each
  floor's dungeon substream has a `layout` child, drawn with fixed counts
  per Blueprint; the party-agnostic graph is drawn first and the party
  band picks only size classes; dressing draws from a display-side
  stream the core never reads (floor layout design 6.1; change FL-2).
  A `terrain` child of each floor's dungeon substream, drawn after
  layout with fixed counts, places room terrain; every piece on a
  walkable square comes from it, not from dressing (room terrain design
  6.2.5; change TR-17).
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
  TL-3, M1).
  An `encounters` child of each floor's dungeon substream, drawn after
  `traps` and before `trapsResolve` with fixed counts and dummy draws,
  places enemy groups, so a placement retry never moves layout,
  terrain, hazards or traps (encounter placement design 6.1; change
  EP-1, M1).
- Rules use integer arithmetic only.
- No reliance on unordered collection iteration.

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

- **Run save:** a snapshot of the full core state, written at safe points.
  The snapshot is the authority.
- **Command log:** kept alongside for replays, bug reports and tests. A log
  may stop replaying correctly after a content change, which is why it is
  not the save.
- **Profile save:** unlocks, boons and difficulty tier, stored separately
  from any run.
- Saves carry a version number.

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
- Each floor is assembled from 3D tile pieces chosen by the floor theme, so
  placeholder pieces can be swapped for final art without code changes.
  **(decided)**
- Mouse picking maps a screen position to a grid square in one place, used
  by all movement and targeting.

## 7. Testing

- **Unit tests** for every rule: attack resolution, conditions, each class
  feature.
- **Property tests** for generation: every floor is connected, the boss is
  reachable, nothing spawns inside a wall.
- **Replay tests:** recorded command logs must reproduce the same final
  state.
- **Balance simulation:** `Sim` plays many seeded fights with AI on both
  sides and reports win rates and fight lengths per build and floor.
- **Content validation** runs as a test, so a broken data file fails the
  build.
- All of the above run with `dotnet test`, with no engine needed.
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
| M3 Between runs | Profile, unlocks, starting boons, difficulty ladder; in-run content: Two Doors at camp 3, Omens, Seams, Nightmare tiers (run structure design 12; change RT-19, M3) | Do players come back? |
| M4 Breadth | Species, multiclassing, more content, balance passes with the simulator | Is every run different? |

M1 covers species selection only if it is trivial to include; otherwise
species arrive in M4.

## 10. Left to the builders

Abilities, derived numbers, skills, damage types and resistances are settled
in `2026-11-16-character-stat-model-design.md`. What that document leaves
open is listed in its own section 12.

Weapons and foci are settled in `2026-11-17-weapons-design.md`; they are no
longer pending. Disarmed and Coated stay Later; starting kits remain
open in their category designs (weapons design 25, 26).

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
`2026-11-17-classes-concepts-design.md`; the feature lists are its
stage 2 (classes design 14, 19; change CL-13a).

- The four launch classes' feature lists (classes design, stage 2).
- All numbers: hit points (shape settled there), damage, experience curve, loot rates.
- The dungeon generation algorithm.
- The JSON schema details.
- The random number algorithm.
- UI layout and art style.
- The Godot version.

## 11. Open items for the client

None.
