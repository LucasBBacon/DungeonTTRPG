# Dungeon RPG: Design

Date: 2026-11-15
Status: awaiting client approval

Items marked **(decided)** were agreed with the client. Items marked
**(proposal)** are the lead's calls and have not been explicitly confirmed.

## 1. Intent

A turn-based RPG played in runs through a randomly generated dungeon. The
appeal is building a character with tabletop-style depth (race, class,
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
- Progression in the style of 5E: race and class at creation, class
  progression, multiclassing, a choice at almost every level. **(decided)**
- Dungeon has floors, bosses, and difficulty and rewards that scale with
  depth. **(decided)**
- The same seed and the same inputs always produce the same run.

### Non-goals

- Multiplayer.
- 3D presentation.
- Console, mobile or browser releases. Target is PC. **(decided)**
- Hex grids.
- Full fidelity to the 5E rules. We borrow its shape, not its text.
- A story campaign.
- Official mod support. Data-driven content leaves the door open.

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

## 3. Constraints

- Team: two developers, very experienced with Godot, strongest in C#.
  **(decided)**
- Engine: Godot 4 with C#. **(decided)**
- Platform: PC. **(decided)**
- Art budget is small. Presentation is 2D top-down. **(proposal)**

## 4. Game design

### 4.1 The run

- A run is 4–5 floors. Each floor takes roughly 15–20 minutes and ends in a
  boss. **(proposal)**
- The hero reaches about level 10 by the end of a run, so a level-up arrives
  about every 8–10 minutes. **(decided: cap of about 10; pacing is a
  proposal)**
- Between floors the party reaches a camp: full rest, a shop or reward, and
  a chance to recruit. **(proposal)**
- A run can be saved and resumed, but there is one save slot per run and it
  is deleted on death. **(proposal)**

### 4.2 Party

- The player builds one hero in full. **(decided)**
- Up to 2–3 companions are recruited during the run. Companions have a fixed
  class and lighter progression: they level automatically, with a choice
  only at a few key levels. **(decided: hero plus companions with lighter
  progression; the detail is a proposal)**
- The run ends when the hero dies. A companion reduced to zero hit points is
  downed, and gets back up on 1 hit point if the party wins the fight.
  **(proposal, expected to be tuned in playtests)**

### 4.3 Grid, exploration and combat

- Square grid. Movement in 8 directions. One diagonal rule everywhere: a
  diagonal step costs the same as an orthogonal step. **(decided: square,
  8-way, one fixed rule; the specific rule is a proposal)**
- Exploration and combat happen on the same map. Combat starts in place when
  enemies notice the party. There are no separate battle arenas.
  **(decided)**
- Out of combat, the player moves the hero and companions follow. In combat,
  all units act in initiative order. **(proposal)**
- Exploration is turn-stepped: the world advances one tick for each hero
  step or action. Effect durations and short rests are counted in ticks.
  **(proposal)**
- Enemies outside combat are asleep, stationary or patrolling, set per
  monster in data. **(proposal)**
- An enemy notices the party when it has line of sight and wins a
  perception check against the party's stealth. A side that is noticed
  without noticing back is surprised and loses its first turn. **(proposal)**
- Enemies within hearing range of a fight join it. Combat ends when no
  hostile unit is aware of the party, so fleeing by breaking line of sight
  is possible. **(proposal)**
- Reactions resolve automatically by default, following a per-reaction
  setting the player can change to "ask me". **(proposal)**
- A unit's turn has movement, one action, one bonus action, and one reaction
  per round. **(proposal)**
- Line of sight and fog of war use one symmetric visibility rule: if A can
  see B, B can see A. **(proposal)**
- Core resolution is a d20 roll plus modifiers against a target number, with
  advantage and disadvantage. **(proposal)**

### 4.4 Resting and attrition

- No full rests inside a floor. A full rest happens at the camp between
  floors.
- Short rests inside a floor are limited by a consumable supply.

Both are proposals. The purpose is to make resource use across a floor a
real decision, which 5E relies on a DM to enforce.

### 4.5 Progression

- At creation the player picks a race and a class. **(decided)**
- The first release of the full run has four classes. More arrive through
  unlocks. **(decided)**
- Every level offers a choice. Where the class table has nothing to offer,
  the game presents a pick-one-of-three offer drawn from a pool of feats,
  boons and upgrades suited to the build. **(decided)**
- Multiclassing is added only once the single-class paths are fun.
  **(decided)**

### 4.6 Dungeon, enemies and loot

- Each floor is generated from the run seed as rooms and corridors, with a
  guaranteed path from entrance to boss.
- Each floor has a theme that selects its tiles, enemies and hazards.
- Enemies are placed using a difficulty budget that grows with depth.
- Loot comes from tables weighted by depth and rarity.
- Bosses are hand-designed. Which boss appears on a floor is drawn from a
  pool.

All proposals.

### 4.7 Progress between runs

**(proposal: recommended to the client, not yet explicitly confirmed)**

- **Content unlocks lead.** Milestones add races, classes, subclasses,
  companions and loot to the pool. Examples: beat the floor 2 boss; win as a
  multiclass character.
- **One starting boon.** Before a run the player picks a single earned boon,
  such as an heirloom item or a background perk. Boons add variety and do
  not stack.
- **Difficulty ladder.** Each win unlocks an optional harder tier.
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
| `content/` | JSON data files | Classes, races, feats, items, monsters, tables |

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
  Opening a chest must not change the next attack roll.
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
| M0 Skeleton | Solution, automated tests, command and event loop, Godot draws a grid and moves a unit | Does the core and display split work? |
| M1 Vertical slice | One class, levels 1–3, one generated floor, three enemy types, one boss, basic loot, permadeath | Is the loop fun? Does the effects system hold? |
| M2 Full run | Four classes, companions, 4–5 floors, level 10, pick-one-of-three offers, save and resume | Does a 1–2 hour run hold together? |
| M3 Between runs | Profile, unlocks, starting boons, difficulty ladder | Do players come back? |
| M4 Breadth | Races, multiclassing, more content, balance passes with the simulator | Is every run different? |

M1 covers race selection only if it is trivial to include; otherwise races
arrive in M4.

## 10. Left to the builders

- Which four classes ship first, and their feature lists.
- All numbers: hit points, damage, experience curve, loot rates.
- The dungeon generation algorithm.
- The JSON schema details.
- The random number algorithm.
- UI layout and art style.
- The Godot version.

## 11. Open items for the client

- Confirm section 4.7, progress between runs.
- Confirm 2D top-down presentation.
- Confirm the companion and death rules in section 4.2.
- Who controls companions in combat: the player or AI.
- Which operating systems and input devices "PC" covers.
