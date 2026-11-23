# Enemies and Morale: Design

Date: 2026-11-16
Status: Approved, 2026-11-16
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model) and
`2026-11-16-conditions-design.md` (conditions design)

Items marked **(agreed)** were decided by the client on 2026-11-16, either
directly or by choosing the lead's recommendation. Items marked **(lead's
call)** were filled in by the design lead while writing this document and
are confirmed when it is approved. Items marked **(settled)** restate a rule
from a parent document. Items marked **(change)** amend an approved document
and are listed, with an id, in section 24. Once approved, every item here is
binding on the builders.

Numbers in this document are illustrative unless a rule says otherwise.
The builders own them and the simulator calibrates them (section 23). No
enemy names or lore are proposed here.

**Waves:** M1 is the vertical slice (one class, levels 1–3, one floor,
three enemy types, one boss, basic loot). M2 is the full run. Later is
everything after. **(settled, main design 9)**

## 1. Intent

Enemies are what the party spends its resources on, what its skills are
measured against, and what kills the hero. The parents settle the stat
model every unit shares, the one-step AI scorer, the condition gates and
the boss rules, and leave open how a monster is built, what it costs, how
it behaves before and after it notices the party, what it carries, how
groups break, and what a fight pays. **(settled)** This document closes
those questions.

The system should make every enemy readable at a glance, learnable across
runs, fair under permadeath, and cheap to author, while giving Stealth,
Perception, Intimidation, Lore and kill order real content. **(agreed)**

## 2. Goals, non-goals, anti-goals

### Goals

- **Built asymmetrically, resolved symmetrically.** Monsters declare their
  numbers inside a validated table and resolve with exactly the party's
  rules. **(agreed)** Attack, save, damage and condition resolution are the
  party's rules. Monster-only data (rank templates, morale states, guile,
  boss actions, flee policies) is declared and validated, never a hidden
  modifier. **(lead's call)**
- **One number drives difficulty:** enemy level relative to the room,
  times rank. **(agreed)**
- **You can read a threat by sight.** Role, rank, gear and affixes show on
  the model and the plate before any Lore. **(lead's call)**
- **The threat and the reward are the same object.** The enemy that glints
  carries the item and uses it against you. **(agreed)**
- **Morale is deterministic and visible.** The break line is on the plate
  before the fight turns. **(agreed)**
- **Each knowledge skill answers a different question.** Lore tells what an
  enemy is; Perception tells what it is about to do; enemies keep some
  ability to surprise mid-combat. **(agreed)**
- **Permadeath guards are computed, not hoped for.** Every unit is checked
  against the reference hero at the earliest room it can appear in.
  **(lead's call)**
- At most four things to read per unit. **(settled, ability model 11.4)**

### Non-goals

- Monsters built with player-character rules (classes, rest and camp pools,
  level-ups). The one exception is the rare rival party, a Later stretch
  goal (section 18). **(agreed)**
- A party morale meter, stress or panic. **(lead's call)**
- Durability, wear, weight or junk loot. **(agreed)**
- Committed enemy intents and scripted wind-ups. **(settled, ability model
  5; agreed)**
- Multi-square units. Every unit occupies one square in M1 and M2.
  **(lead's call)**
- 5E's challenge-rating maths, encounter multiplier or optional morale
  rule (DMG material, not SRD). **(lead's call)**

### Anti-goals

- **Loot clutter.** Walking from body to body, sorting junk, duplicate
  swords. **(agreed)**
- **Grind to win.** Main design 2 rules out substituting repeated runs for
  skill **(settled)**. This document extends it: kill count must never raise
  loot or experience beyond a fixed per-floor amount. **(agreed, framework
  decision 1)**
- **Unfair deaths.** A full-HP hero dying in one round to an ordinary fight,
  or to an unannounced reinforcement. **(lead's call)**
- **Free-stacking affixes** that produce unreadable units. **(lead's call)**
- **Decoration skills.** Intimidation, Stealth or Perception with no content
  on most floors. **(settled, stat model 2)**
- **Rules in the display layer.** Intent, previews and break lines are core
  queries. **(settled, main design 2)**
- **Protected content.** **(settled)**

## 3. Constraints

From the parents, all **(settled)**:

- Every unit has the six abilities and a level; level sets proficiency; the
  +5 cap binds every unit (stat model 5.5).
- Nothing derived is stored; one d20 per contest, no opposed rolls (stat
  model 6).
- Every unit has at least two at-wills, one damaging; monster limits are
  `uses:N` for life or `cooldown:N`; no recharge dice (ability model 6.1,
  12.1).
- One integer scorer, chances out of 20, thrift for monsters; it never
  rolls dice (ability model 13).
- Depth gates for hard control; Steeled; the per-fight cap; boss resolve,
  Unyielding and phases (conditions design 7.2, 12.3).
- 32 conditions in M1 and M2; 6 harmful conditions per theme; 2 per enemy
  type (conditions design 16).
- Separate random streams for generation, combat and loot (main design 6.3).
- Integer arithmetic, rounding down (made exact in 3.1); deterministic;
  rules in the pure C# core; content in JSON.

How this design uses the settled rules:

| Settled rule | How the enemies design uses it |
|---|---|
| Stat model 5.5: same model for every unit; +5 cap | Monsters declare stats inside a validated table. Elite bonuses are effects, not ability changes. |
| Stat model 8.4: traits, resistance budget, mindless | Checked on the composed unit, affixes and items included |
| Ability model 6.1 and 12.1: at least 2 at-wills; `uses` and `cooldown`; no recharge dice | Kit caps, plus `readyAt` and the cooldown minimum (9) |
| Ability model 13: scorer, thrift, open terms | Profiles are weights on the open terms; the Hidden rule narrows "reads the true state" (11) |
| Conditions 7.2: depth gates; Steeled; per-fight cap | `rank: elite` is read at application; hard control is limited and timed (9) |
| Conditions 12.3: resolve, Unyielding, phases | Bosses, with life-long state (17.5) |
| Conditions 16: 32-condition budget; 6 per theme; 2 per type | One counting rule (9.2); a separate budget of 4 morale states (14.9) |
| Conditions 18.8: reserved morale states | Fully specified in section 14 |
| Main design 4.3: dormant, stationary or patrolling; hearing; LOS escape | Awareness states, noise, joining, pursuit (12) |
| Main design 4.6: budget grows with depth; loot tables; boss pools | Depth %, the per-floor loot budget, boss pools (10, 15, 17.5) |

### 3.1 Numeric conventions

**(lead's call)** Every formula in this document follows these rules.

- **One division helper.** Every division uses `FloorDiv`, which rounds
  toward negative infinity. C# `/` truncates toward zero and differs on
  negative operands (experience scaling, negative modifiers, level-shift
  ratios), so it is never used directly in rules.
- **Fractions are compared by cross-multiplication**, never by dividing
  first. "At or above ½ startCost" means `2 × lossCost ≥ startCost`; "at or
  below ½ HP" means `2 × hp ≤ maxHp`; "at or below ¼ HP" means
  `4 × hp ≤ maxHp`; the potion's "at or below 50% HP" is the ½ line.
- **Percentages and multipliers are permille integers.** Rank multipliers:
  standard 1000, elite 2000; a second affix multiplies by 1200. Minions use
  their own formula (7.1).
- **Unit cost** = `FloorDiv(levelCost × rankPermille × affixPermille,
  1,000,000) + costAdd`, where `affixPermille` is 1000 with zero or one
  affix and 1200 with two. One truncation, at the end.
- **Unit counts are kept in halves:** a minion is 1, any other unit 2, and
  a cap of "4 units" is 8 halves.
- **Fill minimum** = `budget − FloorDiv(budget × 15, 100)`, which is
  ⌈85% × budget⌉. The fill maximum is the budget.
- **Room levels are held in tenths** (`expectedTenths`, 6.3); room level
  = `FloorDiv(expectedTenths, 10)`.
- **Experience percent** = clamp(100 − `FloorDiv(25 × (10 × heroLevel −
  expectedTenths), 10)`, 50, 150), applied to each member share, then
  floored (16).

## 4. Framework decisions

Four framework decisions shape everything else. The client chose all four
on 2026-11-16. **(agreed)**

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Enemy gear and loot | **C: visible loadout, curated drops.** Gear is visible and drives the enemy's attacks. Issue gear never drops (at most salvage coin). Notable items come from a fixed per-floor loot budget pre-rolled on the loot stream, carried mostly by elites, leaders and bosses. No junk, duplicate rerolls, family caps, one loot summary per fight, no durability. Carriers use their consumables; Steal from unaware carriers; drop on death, rout or yield. | A, no enemy gear (threats unreadable by sight; Stealth's steal use has no content; elites become pure risk); B, full drops with filters (clutter guaranteed; loot rises with kill count, breaking "grind to win") |
| 2 | How close an enemy is to a player character | **Hybrid:** built asymmetrically, resolved symmetrically. Stats are declared within a validated level × role × rank table; the same resolution rules apply. | PC-like enemies with classes and pools (breaks "four things to read", expensive content, see 18.2); bare stat blocks (breaks stat model 5.5: Shove, Frighten and noticing have nothing to roll against) |
| 3 | Power scale and difficulty budget | **Relative level (PF2e shape):** cost = f(enemy level − room level) × rank, with the computed-threat validator (6.5) failing any composed unit more than about ±20% from its price | Absolute challenge rating (DMG maths, poor party-size scaling); computed threat as the price itself (opaque; drifts with every formula change) |
| 4 | Morale model | **Deterministic group thresholds:** the leader falls, or half the group's cost is lost. Intimidation is the only die. | A numeric meter (hidden bookkeeping, hard to preview); morale rolls (dice the player cannot influence, under permadeath) |

The client also answered three questions **(agreed)**:

1. **Morale ships in M2,** with a separate, hard-capped budget of **4
   enemy-only morale states**: Emboldened, Demoralised, Routed, Cowed
   (14.9).
2. **Party weight grows sublinearly:** hero 15 + 8 per companion, using the
   actual party, fixed at floor generation, less the shortfall term on
   floors 2–5 (10.1; companions design, Q1) **(change CMP-5)**.
3. **Intent display:** a baseline for everyone in M1 (threat-reach overlay,
   turn log, announced boss signature moves, reinforcement warnings), plus
   **intent reading** in M2: when the party's best passive Perception is at
   least an enemy's guile, hovering it shows whom it would target "if its
   turn began now". A Perception mastery adds the ability and extends
   reading from the next 1 enemy to the next 3 (section 13).

Mechanism references from other games, all **(lead's call)**:

- Into the Breach and DCSS show that deterministic, role-consistent enemies
  are learnable, not dumb.
- Brogue's labels show how to display awareness.
- Diablo III's free-stacking affixes produced unreadable deaths; ours are
  bounded and validated. Diablo III's Loot 2.0 (fewer, better drops) is the
  model for curated loot.
- Battle Brothers' rout chases are tedious; our routers drop their loot and
  leave instead. Its named champions are a model for carriers.
- XCOM 2's panic and Darkest Dungeon's stress are why the party has no
  morale.
- Slay the Spire's visible optional elite is the model for the optional
  elite room.

## 5. Anatomy of an enemy

**(agreed for the hybrid; details lead's call)**

### 5.1 Authored fields

Authored on the base type. The composed unit exists only at runtime.

| Field | Rule | Wave |
|---|---|---|
| `nativeLevel` 0–12 | Plus a legal `levelShift` range of −2 to +2 | M1 (shift M2) |
| `role` | Brute, soldier, skirmisher, artillery, controller, leader, lurker or minion (17.1) | M1: brute, soldier, artillery, minion |
| `rank` | Minion, standard, elite or boss (7) | M1: minion, standard, boss |
| `abilities` | Main and secondary follow the spread table (5.3). Intelligence (initiative) and Wisdom (perception and guile) are authored; the others default to 0. Range −3 to +5. The type declares Dexterity's spread slot (`main`, `secondary` or `other`), which armour validation reads at every shift (armour design 12.2). **(change AR-53)** | M1 |
| `hp` | Within the band of the table row | M1 |
| `trainedSave` | Exactly one | M1 |
| `skills` | 0–2 from Perception, Stealth, Athletics, Acrobatics and Intimidation, and only skills the rules or the AI use | M1 |
| `armour` | Natural armour `{natural: offset, metal?}`: the medium band value + offset (armour design 12.1), or from loadout. Armed types may use natural armour for display-only issue hides (furs). **(change AR-17)** | M1 |
| `speed` | Default 6, authored before armour penalties; the derived speed stays within 3–8 (armour design 7.3); 0 only if `immobile` **(change AR-53)** | M1 |
| `size` | Field only. A unit cannot shove or grapple a target more than one size larger; this binds the party too **(change A-10)**. Multi-square bosses are Later. Every M1–M2 boss is ≤ Large, and each floor-1 theme with two bosses has a Medium one (boss rooms design 4.7.6; read). | M1 |
| `senses` | 0–1 sense trait: **Keen** (advantage on the group's noticing roll and on Search; +5 to its passive perception) or **Sightless N** (treats Hidden units within N squares as noticed; immune to `sight`, which uses its 1 control-tag immunity). No darkvision, because there is no light model. | M2 |
| `traits` | Within the settled budgets | M1 |
| `atWills`, `limited`, `reaction`, `bonus` | Ability ids, within the kit caps (9.1) | M1 |
| `signature` | Boss only: which limited abilities are announced signature moves (13.2) | M1 |
| `concentrates` | Only controllers, caster leaders and bosses | M1 |
| `behaviour`, `awareness`, `morale` | Profiles (11, 12, 14) | M1 (morale field only in M1) |
| `mindless` | Implies `fearless` and the Automaton profile default | M1 |
| `armed`, `loadout`, `improvised`, `notableSlot`, `promotable` | Gear (15) and elite promotion (7.2) | M1 (promotion M2) |
| `earliestRoomLevel` | **Computed by guard validation, not authored.** The generator never places the unit in an earlier room. | M1 |
| `themes` | Which floor themes may field the type | M1 |

### 5.2 Derived and never stored

**(settled)** These use the same formulas as the party:

- Proficiency: +2 at levels 0–4, +3 at 5–8, +4 at 9–12. **(change S-1)**
- Attack = the attack's ability + proficiency + effects. There is no free
  attack field.
- Ability difficulty = 8 + proficiency + main ability + effects.
- Saves use the better of the two abilities, plus proficiency if trained.
- Initiative = d20 + Intelligence.
- Passive perception = 10 + Wisdom (+ proficiency if trained, double with
  mastery: change S-12), plus active effects.
- **Guile** = 10 + Wisdom + proficiency + role adjustment (13.3), for
  every enemy, whether or not it is trained in Perception. **(agreed;
  change S-11)**
- **AC uses the stat model's armour formula.** Natural armour uses the
  medium formula and counts as medium for every rule that reads the armour
  category, except that the armoured trait or Plated makes `CountsAsHeavy`
  true for Corroded, piercing and Clatter; the AC formula is unchanged
  (armour design 6.1). A shield adds on top. The table AC (6.2) is a **validation band
  of ±1**, not a top-up target. The loadout picks an armour record and a
  Dexterity that land in the band, using lighter armour when a shield is
  carried. The band is checked on the base type at every legal level
  shift (armour design 12.2); an affix's AC change is governed by the
  threat band (6.5) instead. Issued armour verbs obey the role-answer rule
  (armour design 8.1 rule 10). **(lead's call; change AR-18)**

### 5.3 Ability spread by level

**(lead's call)** Untrained saves only grow with depth if the abilities do.

| Level | Main | Secondary (±1 allowed) | Others |
|---|---|---|---|
| 0–2 | +2 | +1 | 0 (−1 allowed for flavour) |
| 3–5 | +3 | +2 | 0 |
| 6–8 | +4 | +3 | 0 or +1 |
| 9–12 | +5 | +4 | 0 or +1 |

- **Save lever:** every non-boss has at least one save in the low or mid
  band, so save-based builds always have a target. Lore reveals it ("Weak:
  Will"). A boss has no save below mid, because resolve covers that gap.
- **Dexterity-main monsters** add Dexterity to hit, not to damage
  **(settled)**. Their flat damage is declared on the attack, so they are
  not underpowered. Each monster at-will declares its attack ability in
  data; its flat damage is not touched by the weapons design's no-ability
  rule for Dexterity-rolled attacks (weapons 6.2). **(change W-5)**

### 5.4 Damage shape

**(lead's call)**

- Each at-will damage part has **at most one die**. The rest of its average
  is flat. Limited abilities may roll more dice. This keeps the one crit
  rule working and cuts spikes under permadeath. Crit dice, Exploit and
  Precision are core rules outside this limit (weapons 15.1). **(change
  W-5)**
- Issue gear sets the damage **type, die, properties (reach, thrown), rider
  flag and armour category**. A monster rolls **one die: the smaller of
  the weapon's largest die and its row's die** (row dice *(illustr.)*, from
  the 6.2 example column: levels 0–1 d4, 2–3 d6, 4–6 d8, 7–9 d10, 10–12
  d12; a greatsword brute rolls at most 1d6). The flat amount comes from
  its table row and is **never below 0**. **(change W-5; weapons 15.1)**
- **Minions** roll that die **one size smaller, minimum d4**, with the flat
  (≥ 0) set to keep the at-will in the minion's 50% band (6.2, ±15%).
  Exception: at level 0, where a d4 with flat 0 (2.5) is already above the
  band (ceiling 2.01), the minion rolls d4 with flat 0 and passes the band;
  the 6.4 guards still apply. **(change W-17; weapons 15.1)**
- So a dropped weapon has the same type, properties and rider it had when
  it hit you. Its numbers depend on who wields it.

### 5.5 Universal actions and what an enemy lacks

**(lead's call)**

- Every monster may use Dash, Disengage, Dodge and Search.
- Shove, Grapple, Hide, Help and Frighten are used only when the kit
  declares them. They then count toward the caps in 9.1.
- An enemy has no rest or camp pools, level-ups, offers, "ask me"
  reactions, mastery, or Lore, Arcana or Medicine uses.

**Pending request (TR-15, M2; room terrain design 6.9):** a
`usesFurnishings` kit tag (Strongman, Fire-Eater, Glutton, a Gaol warden
brute, Gut Ooze *(illustr.)*) for Attack Shunt, Kick and Smash of heavy
pieces, never Move-Shunt to barricade; and a universal light Smash: every
monster with a melee attack may Smash an adjacent light Furnishing; a
request to this owner, not yet a rule.

**Cords (change HZ-15; hazards design 6.3.4; M2, with the Stirs):** the
tag is extended by `usesCords`: one monster type per F2–5 theme may
Operate a Cord, with telegraphed intent, valued through the preview,
under `NoCheapKill`. An act set by an Operated Cord credits the operator
for experience and morale; on-kill hooks never fire from a Dream-step
act. **Pending request (HZ-15; ability model 7.2):** a unit that starts
its turn adjacent to a Cord may Operate it as its bonus action; the
ability model owner rules, not yet a rule.

### 5.6 Plate contract

**(lead's call)** This applies the settled "four things to read".

| Shown | Item |
|---|---|
| Always | (1) Name, with the **rank frame** and **role glyph**. The morale state **tints the frame strip** rather than adding an icon; the morale profile is shown on hover, not on the plate. Awareness is a **small corner glyph**. A notable item adds **one glint icon**. **The loot mark is one badge (change LE-16; loot economy design 6.15, 8.3; M2):** the notable's star, a coin disc for the Heavy Purse, or both merged into one "loot" badge with a count; with the key pip, a plate carries at most 2 marks; purse badges count in the glints-per-screen gate (14.10). |
| Always | (2) HP bar, with boss phase marks and resolve pips |
| Always | (3) The condition row, under the settled icon rules |
| Always | (4) Affix names, one word each |
| Once seen | One "ready" marker for a seen hard-control or area ability that is ready next turn |
| On hover or inspect | Gear line, with the weapon's verb ("Pike: piercing, reach 2, Set · Chain mail: heavy, Ringed"; changes W-5, AR-19), "Carries: …" (a purse reads, for example, "Carries: 12 coin"; change LE-16), cooldowns and uses, the morale profile and break line, traits once revealed by Lore, the intent line (13.3) |

A UI test renders the worst legal composed elite on each floor at minimum
zoom. From M2 it runs per theme, beside the readability charter's gates
(a greyscale value test per theme, a silhouette lineup of all factions ×
roles at minimum zoom, colour-blind simulation of the damage types and
square kinds) **(change RT-17; run structure design 7.4)**.

## 6. Power scale and guards

**(agreed for relative level; tables lead's call; all numbers
illustrative)**

### 6.1 Reference hero

Used by every guard and by validation. Reference hero = class HP tier 6,
Con +1, under 11 + level × max(1, tier + Con) (death and healing design
7); the lowest legal hero is tier 6, Con −1. **(change DH-25)**

| Level | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|---|---|---|---|---|---|---|---|---|---|---|
| HP | 18 | 25 | 32 | 39 | 46 | 53 | 60 | 67 | 74 | 81 |
| AC | 15 | 15 | 15 | 15 | 16 | 16 | 16 | 16 | 17 | 17 |
| Attack | +5 | +5 | +5 | +6 | +7 | +7 | +7 | +8 | +9 | +9 |
| Ability difficulty | 13 | 13 | 13 | 14 | 15 | 15 | 15 | 16 | 17 | 17 |
| Typical damage per round | 7 | 7 | 7.5 | 9 | 17 | 17 | 18 | 21 | 24 | 25 |

### 6.2 Target table

Standard rank, soldier baseline. This is the only target table.

| Level | Prof. | Main | AC | Attack | Ability difficulty | HP | Damage per hit (example at-will) | High save |
|---|---|---|---|---|---|---|---|---|
| 0 | +2 | +2 | 12 | +4 | 12 | 9 | 3.5 (1d4+1) | +4 |
| 1 | +2 | +2 | 13 | +4 | 12 | 12 | 4.5 (1d4+2) | +4 |
| 2 | +2 | +2 | 13 | +4 | 12 | 15 | 5.5 (1d6+2) | +4 |
| 3 | +2 | +3 | 14 | +5 | 13 | 18 | 7.5 (1d6+4) | +5 |
| 4 | +2 | +3 | 14 | +5 | 13 | 22 | 8.5 (1d8+4) | +5 |
| 5 | +3 | +3 | 15 | +6 | 14 | 32 | 10.5 (1d8+6) | +6 |
| 6 | +3 | +4 | 15 | +7 | 15 | 36 | 12.5 (1d8+8) | +7 |
| 7 | +3 | +4 | 16 | +7 | 15 | 40 | 13.5 (1d10+8) | +7 |
| 8 | +3 | +4 | 16 | +7 | 15 | 46 | 15.5 (1d10+10) | +7 |
| 9 | +4 | +5 | 17 | +9 | 17 | 50 | 17.5 (1d10+12) | +9 |
| 10 | +4 | +5 | 17 | +9 | 17 | 54 | 18.5 (1d12+12) | +9 |
| 11 | +4 | +5 | 17 | +9 | 17 | 58 | 20.5 (1d12+14) | +9 |
| 12 | +4 | +5 | 17 | +9 | 17 | 62 | 22.5 (1d12+16) | +9 |

What the table is built to do:

- The hero hits an on-level standard about 65% of the time at every level.
- Monsters hit the reference hero 50% of the time at level 1, rising to 65%
  at level 10. This rising hit rate is part of how difficulty grows with
  depth.
- A standard's HP is about 2.2–2.5 rounds of one member's typical damage.
  Levels 0–2 sit lower, for the solo hero.
- Damage per hit is about 22–25% of the hero's HP.

Validation bands:

- HP: ±15%.
- At-will average: ±15%.
- AC: ±1 after the role step (base type and level shift; affixes are
  governed by the threat band, 5.2, 6.5).
- Limited abilities: up to 200% single-target, or 120% per target for
  areas.

**Role shapes** are applied on top of the row: percentages for HP and
damage, flat steps for AC and attack (17.1).

### 6.3 Where levels sit

- Rooms carry an *expected party level* that rises along the critical
  path, for example 1.0 at the floor 1 entrance and 2.7 at its boss. It is
  held in tenths as `expectedTenths`. **Room level =
  `FloorDiv(expectedTenths, 10)`.**
- **Main-path rooms** (index i of n on the floor's main path, entry 0, boss
  n − 1), **(lead's call)**:
  - **M1:** linear, `expectedTenths = start + FloorDiv((end − start) × i,
    n − 1)`, with start and end from the table below (10 and 27 on floor
    1).
  - **M2:** `start + FloorDiv(10 × X, 270)`, where X is the nominal
    experience (16) of every band placed strictly earlier on this floor's
    main path, capped at `end`. On floor 5 the divisor is 540 (16).
  - **Counting (change FL-4; floor layout design 3.4, 6.3):** n counts
    the entry (0) through the arena (n − 1), the antechamber (n − 2)
    included; the floor-1 antechamber is an extra room (beyond run
    structure design 6.6's 7, so floor 1's n is 8), and the floors
    2–5 antechamber counts as an empty or special room. The elite room
    and Circuit side rooms count as side rooms; the Bypass arm, Alcoves,
    corridors and the Stairhead are not counted rooms. "Room k" means
    `mainPathIndex` k throughout this document, except 10.2, whose
    "Room N" stays a room level as defined there.
- **Off-path rooms** (the optional elite room, side rooms, cache rooms)
  inherit the value of the main-path room they branch from. **(lead's
  call)** Every square carries an `effectiveIndex` (its `mainPathIndex`
  where it has one, otherwise the attachment index of its room or
  branch; Alcoves take their parent's), and `expectedTenths` reads it;
  the Surprise rule (11.3 invariant 6, 12.2) reads `mainPathIndex`
  **(change FL-13; floor layout design 3.4)**.
- **Every group has a `homeRoom`**, fixed at generation: its placement room,
  or, for a patrol, the route room with the highest `expectedTenths`.
  **Cost, `earliestRoomLevel` checks, experience scaling and cow
  eligibility always read the group's homeRoom**, never the room where a
  fight happens, so a patrol or joiner fighting elsewhere keeps its price.
  **(lead's call)**

| Floor | Party levels | Expected size | Room expected level | Standard levels | Elite levels | Minion levels | Boss level | Depth % |
|---|---|---|---|---|---|---|---|---|
| 1 | 1→3 | 1 (solo) | 1.0 → 2.7 | room −2 to +1 | room −1 only (guard-limited) | room −3 to 0 | 3–4 | 80 |
| 2 | 3→5 | 2 | 3.0 → 4.7 | room −2 to +1 | room −1 to +2 | room −3 to 0 | 6 | 90 |
| 3 | 5→7 | 3 | 5.0 → 6.7 | room −2 to +1 | room −1 to +2 | room −3 to 0 | 8–9 | 100 |
| 4 | 7→9 | 4 | 7.0 → 8.7 | room −2 to +1 | room −1 to +2 | room −3 to 0 | 10–11 | 105 |
| 5 | 9→10 | 4 | 9.0 → 9.8 | room −2 to +1 | room −1 to +2 | room −3 to 0 | 12 | 110 |

The floor 1 boss stays at proficiency +2, so it hits at +4 or +5, not +6.
"Expected size" is a calibration assumption only; budgets use the actual
party (10.1). The shortfall term in 10.1 reads `expectedCompanions(floor)`,
computed from the slot schedule and the cap, not this column **(change
CMP-6; companions design 7.7)**.

### 6.4 Permadeath guards

**(lead's call)** Validation computes these against the reference hero at
the unit's **earliest legal room level**. The simulator computes the
round-1 row.

| Guard | Limit |
|---|---|
| A non-boss unit's expected damage on its turn | ≤ 25% of the hero's HP |
| A non-boss unit's maximum damage on its turn, no crit | ≤ 40% |
| A non-boss unit's maximum damage on its turn, with a crit | ≤ 60% |
| A non-boss unit's maximum damage on its turn plus its own death effect (Volatile), no crit | ≤ 60% |
| A non-boss unit's maximum damage to one hero across one round, its turn plus its Set attack, no crit / with one crit **(change W-5)** | ≤ 50% / ≤ 70% |
| Chance that a full-HP hero is reduced to 0 in round 1, for **every** template × band × composition the generator can emit on that floor, with surprise wherever the template's awareness allows and every focus term on (Quarry, kill bonus, setup), death effects included | < 0.5% each |
| Floor 1, solo: an encounter's summed round-1 expected damage to the hero (surprise included where allowed) | ≤ 50% of the hero's HP |
| Boss: expected damage to one unit per round (its turn plus boss actions) | ≤ 35% |
| Boss: worst spike (dice at maximum, a crit, one boss action) | ≤ 70% |
| Boss: a full-HP hero cannot be reduced to 0 in one round | Unless at least one crit lands |
| Lowest legal hero (lowest HP, tier 6 Con −1, at the AR-20 lowest legal AC): a non-boss unit's maximum damage on its turn, with a crit **(change DH-26)** | ≤ 75% |
| Lowest legal hero: chance that a full-HP hero is reduced to 0 in round 1 (same definition as the reference row), per encounter, floors 2–5 **(change DH-26)** | < 2% *(illustr.)* |
| Floor 1, solo boss: expected damage to the hero per round (its turn plus boss actions); the 35% boss row stays for every other boss **(change DH-26)** | ≤ 25% *(illustr.)* |
| Floor 1, solo boss at the lowest legal hero: worst spike (dice at maximum, a crit, one boss action) **(change DH-26)** | ≤ 75% |
| Floor 1, solo boss at the lowest legal hero: a full-HP hero cannot be reduced to 0 in one round **(change DH-26)** | Even with one crit *(illustr.)* |
| Boss: `NoUntelegraphedSpike`: any boss or boss-action effect whose maximum without a crit exceeds 30% of the floor's reference hero *(illustr.)* is a bannered signature on its Downbeat, a Gambit (M3) or an announced arena act **(change BR-8; boss rooms design 4.3.3; M2)** | Else rejected |
| Boss: a forecast Downbeat signature's maximum without a crit, at the lowest legal hero **(change BR-8; boss rooms design 4.3.3; M2)** | ≤ 45% *(illustr.)* |
| Floor 1 cumulative round-1 budget, reference and lowest legal hero: the sum over floor 1's encounter slots of the highest round-1 death chance among the templates the generator can emit there, plus the floor-1 boss's one-round death chance (`guard.floor1CumulativeRoundOnePermille`) **(change DH-26)** | ≤ 2.5% *(illustr.)* |
| AC envelope, levels 1–10: the 6.2 table attack's hit chance against the maximum sustained party AC, normal roll mode **(change AR-20)** | ≥ 35% |
| AC envelope: against the maximum charged AC (Raise or Fortified, plus one reaction), normal roll mode **(change AR-20)** | ≥ 15% |
| AC envelope: against the maximum sustained AC with the one allowed gear roll-mode effect **(change AR-20)** | ≥ 10% |

- **Party sizes (change CMP-8; companions design 7.9):** every guard is
  evaluated at every reachable party size and weight, down to the
  shortfall minimum (10.1); the floor-1 solo round-1 row also applies to
  solo parties on floors 2–5. **(told plainly)**
- **Pending request (EP-30, M1 static parts, M2 the rest; encounter
  placement design 6.2, 9.2, 10):** the rows are enumerated over placed
  geometry (formations, start squares and Billets on), beside segment
  6's own `LandingShooters` (1/2/2/2/2 on the entry lane) and its F4–5
  fill cap (a +2-level unit on a back or overwatch position fills at ≤
  925‰); a pre-existing gap is flagged: the toy's F4–5 control fights at
  maximum fills with +2 units showed a 1.8–3.4% round-1 death chance
  against the < 0.5% row; this owner rules the gap; not yet ruled.
  **(told plainly)**
- **Armour (change AR-20; armour design 11, items 1 and 2):** the guards
  are also evaluated at the lowest legal party AC as armour design 11
  item 2 defines it, and a failure fails the build. From M2 each guard
  runs per attacker at the minimum over party members of (visible AC −
  that attacker's piercing R), Puncture included (armour design 9.1). The
  AC envelope rows are read at levels 1–10 against the maximum party AC,
  not at the reference hero.
- **Boss spike rows (change BR-8; boss rooms design 4.3.3; M2):** the
  worst-spike rows read the boss's worst own-turn effect with a crit plus
  the worst boss action that can hit the same unit that round (on floor 1,
  whose menu has no Strike, the boss's own turn). On floors 3–5, focus
  spread (11.1) is read to forbid two boss actions on the same unit in one
  round, so the rows enumerate one boss action per target (a reading sent
  to this owner for confirmation). The round in which a phase entry occurs
  is enumerated with the new kit.
- The guards assume the **SRD double-dice crit** for every unit
  **(settled; change S-3, death and healing design 9)**.
- **(change DH-26)** All rows are computed without party consumables or
  the talisman. "Before the hero's first turn" replaces "round 1" for
  ambush-eligible templates only after the surprise model is reconciled
  and the 10.2 worked examples pass (death and healing design 21.1). The
  floor-1 boss sustain check (`Floor1BossSustain`) reads consumables, so
  it is a content-validation code, not a 6.4 guard (death and healing
  design 21.1 step 6).
- **Terrain (change TR-9, TR-26; room terrain design 6.3.5, 6.8):**
  every round in which a monster-triggerable Keg is in range of
  party-reachable squares counts in the guards, not round 1 only (a Keg
  burst is a one-shot area, source `environment`, sturdy in M2, and no
  burst triggers a burst in M2); Conduct (K3) counts in the guards too.
  **(M2)**
- **Hazards (change HZ-7; hazards design 6.11):** Stir acts count in the
  guards in the rounds they are due (the Keg and Conduct precedent;
  **M2 (gated), else M3**); round 1 never has an act (`firstAct` ≥ 2),
  so the round-1 rows are unchanged. Deep falls count in the Choir
  lightning spike row and environment Chilled in the Supper freeze-spike
  row. Beside the guards, the **EnvRound cap** is a resolution rule:
  environment damage to one party unit within one round is capped at
  `CapRound` by a round-scope top-up (room terrain design 6.4's
  mechanic); monsters are uncapped. **(M2)**
- **"On its turn" means the whole kit**, limited abilities included,
  each checked at its best case; the per-unit rows are checked for every
  ability, not only the at-will. **(lead's call)**
- **Weapon verbs and core damage rules (change W-5; weapons 15.1, 15.2):**
  verb effects count in the at-will band and in these guards, including
  advantage handed to later units (Prone); a monster's Graze deals its own
  Strength modifier. Exploit and, if on, Precision count at expected value,
  at the `weapons.json` rates, in the band and the guards; in the "with a
  crit" rows the Exploit value is doubled with the dice, Precision is not. The per-unit guards count
  only damage dealt to one hero (Skewer only with the hero as the primary
  target; Cleave's second target excluded). The opportunity attack is not
  added to the Set round row; the floor 1 summed round-1 guard and the
  round-1 simulation include Set and opportunity attacks under the AI's
  movement.
- The encounter rows were tightened from a median encounter because
  permadeath deaths come from the tail (lairs, elite rooms, ambushes,
  maximum fills), and three units each at 25% can sum past a solo hero's
  HP. **(lead's call)**
- *At-will example:* a level 1 brute (1d4+3: maximum 7, with a crit 11,
  which is 61% of 18 HP) fails at room level 1 and passes from room level
  2 (11 is 44% of 25). So brutes never appear in the earliest floor 1
  rooms. No special rule is needed.
- *Limited-ability example:* a level 2 brute's Charge (17.1) is its at-will
  with advantage and no extra die, so its maximum stays 1d6+3 = 9 (36% of
  25), 15 with a crit (60%), and its expected damage rises by about half,
  to about 18% of 25, still under 25%. A "+1 die" Charge (2d6+3: 15, or 27
  with a crit) would fail every floor 1 room, which is why the die is
  added only from floor 2. **(lead's call)**
- Floor 1 elites can only be level 1 (room −1). A level 2 elite's two hits
  can reach 56% of the hero's HP.

### 6.5 Computed threat

**(agreed: the validator and its ±20%; formula lead's call)** The single
definition of the validator that framework decision 3, the affix rules
(8.3), item assignment (15.3) and the content tests (20.1) call. All
integers, per 3.1.

- **Reference.** Every composed unit is evaluated against the reference
  hero (6.1) at the unit's **own level** (clamped to 1–10), and its price
  is evaluated at **level difference 0**. So the result does not depend on
  the room it is placed in.
- **Offence** (tenths of HP per round): the unit's expected damage to the
  reference hero over a fixed 4-round window, divided by 4. Each round it
  uses its best available option: limited abilities as early as `readyAt`,
  cooldown and uses allow, its at-wills otherwise. Hit chances are out of
  20 against the hero's AC, crits included; saves use the hero's best
  save. Each harmful condition it applies adds its scorer control value
  (conditions 17.3) × the chance it lands. A piercing attacker's hit
  chances are computed at the reference AC − its R (armour design 9.1).
  **(change AR-21)**
- **Defence** (tenths of HP): `FloorDiv(HP × 40, h + f)`, where h is the
  reference hero's hit chance out of 20 against the unit's AC, and f is the
  chance out of 20 that the unit fails its middle save against the hero's
  ability difficulty. Attacks and saves are weighted equally. Armour
  verbs price the defence term through `verbDefencePermille`
  (provisional), later measured per floor theme (armour design 12.5).
  Reaction non-stacking (at most one reaction AC bonus per attack, armour
  design 10.4) applies when pricing leader-and-soldier pairs. **(change
  AR-21)**
- **Threat** (permille) = `FloorDiv(offence × defence × 1000, offenceRef ×
  defenceRef)`, where the reference values are those of the on-level
  standard soldier row (6.2) at the same level. That row's threat is 1000.
- **Power** (permille) = `isqrt(threat × 1000)`, the square-law
  conversion (7.2).
- **Price** (permille) = `FloorDiv(rankPermille × affixPermille, 1000) +
  FloorDiv(costAdd × 1000, levelCost(0))`.
- **Pass** when `FloorDiv(price × 80, 100) ≤ power ≤ FloorDiv(price × 120,
  100)`.
- **Scope.** Standards, elites and carriers are validated this way.
  **Minions** are not: HP 1 makes the defence term meaningless, so they are
  validated by the rank rule alone (7.1: HP 1, 50% damage ±15%, the kit
  limits), and the cost-residual report calibrates their price (22).
  **Bosses** are validated by the boss guards (6.4) and their authored
  budget, not by this band.

## 7. Ranks and the elite template

**(lead's call)**

### 7.1 Ranks

`rank` is a field on the **composed** unit. The conditions design's depth
gates read the **source unit's rank at the moment a condition is applied**.
**(change C-1)**

| Rank | Definition | Cost multiplier | Wave |
|---|---|---|---|
| **Minion** | HP 1. Any damage kills it, including ticks. It takes no damage from a miss, and none on a successful save against "half on success" effects. This is a rank rule, not a resistance. Damage is 50% of the row; it rolls the 5.4 die one size smaller, minimum d4, with the flat set to keep that band (level 0: d4, flat 0, the stated exception) **(change W-17)**. Two at-wills; no limited abilities, no harmful conditions, no reaction beyond the opportunity attack, no bonus-action ability. **Same-type minions in one group share one initiative slot** and act in unit order. Never an elite, an item carrier, affixed or a concentrator. Morale profile `normal` by default. **At most 2 minions in a solo encounter.** | max(2, `FloorDiv(levelCost, 3)`) + costAdd (always 0) | M1 |
| **Standard** | The table row, shaped by its role | ×1 | M1 |
| **Elite** | The template in 7.2 | ×2, including the first affix; a second affix ×1.2 | M2 |
| **Boss** | Hand-designed, from a pool per floor **(settled)** (17.5) | Authored budget | M1 |
| **Windfall** (non-combatant) | The Truant: a **non-combatant unit**, not a roster type, minion, monster or group member; faction "dream". One square; units may move through it but not end a move there; no cover; never provokes or makes opportunity attacks. 0 XP, 0 salvage; never a purse or key carrier; no morale (never Routed, Cowed or Yielded); **never counts as a hostile** (14.6). It is **nabbed**, not killed: once awake, any action that targets it (an attack, an action-cost spell or ability, a thrown item, or Grab from an adjacent square), any area or effect including its square, any damage or any forced movement nabs it; bonus actions and reactions cannot target it; while Dormant with its host group unresolved only Steal touches it (15.5), and Grab and attacks are greyed "guarded". A nab is not an attack roll, a hit, damage dealt or a kill: no on-hit, on-damage, on-kill or morale trigger, no XP or salvage. **(change LP-3; loot sources design 4.7.1, 4.7.5; agreed, Q1, Q2; M3)** | None: outside the encounter budget, the ambush cap and the joiner caps | M3 |

The "weak" rank is cut. Padding uses a −1 or −2 level shift with the
"ragged" tint.

### 7.2 The elite template

The single definition of "elite":

- **Two routes to the rank:** an authored elite type, or a standard type
  marked `promotable: true`, which the generator promotes. Minion and boss
  types are never promoted. **Pending request (change IR-12; item rolls
  design 4.12; M2):** the RS owner and segment 9 flag the Supper's
  Butler-at-Arms `promotable` and the Silk's Spine-Binder `armed`, so
  those floors' elite notable is carried instead of falling to the lair
  cache.
- **HP ×2**, from the role-adjusted row.
- **+1 to attack, ability difficulty and all saves.** This comes through an
  effect bundle `rank.elite`, the same path items use. It is not an ability
  change, so the +5 cap is untouched, and it is not a monster-only rule.
- **AC unchanged.**
- **Damage per turn is 150% of a standard's**, through one of two options
  chosen in data:
  - (a) *Multiattack:* the Attack action makes two at-will attacks at 75%
    of the per-hit target each. The die is kept and the flat amount is
    cut.
  - (b) One extra limited ability beyond the standard kit cap, with its
    at-will at 100%.
- **Affixes:** floor 1: 0, or 1 from the gentle set (Warded, Plated,
  Swift); floors 2–3: 1; floors 4–5: up to 2.
- **Morale:** `steady` by default. A type may lower it to `normal`, never
  to `skittish`.
- **No resolve pips.** Unyielding only through authored data or the
  Unbreakable affix **(settled: "elites by choice")**.
- **Death-warded** only through the Relentless affix (Later). Never on
  bosses **(settled)**.
- **Placement:**
  - only in rooms whose room level is at least 2;
  - on floor 1, only in the optional elite room in the floor's second half
    (never in the floor 1 boss escort);
  - floor 2: the only non-boss rank allowed party-facing hard control
    **(settled gate)**;
  - floor 3 and deeper: the gates allow hard control on standards too, but
    it stays on controllers and elites.
- **Presentation:** a distinct frame, a crown on the model, and the affix
  names on the plate.

**Why ×2.** Threat = offence × defence (6.5). The template gives 1.5 × 1.09
(accuracy) × 2 × 1.05 (saves) ≈ 3.4. One affix adds about 10–20%, so threat
is about 3.8–4.1. Power is the square root of the threat ratio (Lanchester's
square law), so about 2.0, which matches ×2 under the ±20% validator. ×3 and
×2.5 both fail that check. The simulator owns the final figure.

### 7.3 Level shift

M2. A deterministic transform by k rows, −2 to +2:

- proficiency comes from the new level;
- the main and secondary abilities follow the spread table;
- HP and flat damage scale by the ratio of the new row to the native row,
  as `FloorDiv(value × newRow, nativeRow)` (3.1), and the die is
  unchanged;
- AC is recomputed from the loadout; a shift whose AC leaves the band is
  illegal (armour design 12.2) **(change AR-22)**;
- at −2 the type also drops its second limited ability, which is the
  difference the player notices.

Shifts use a "ragged" tint downward and a "veteran" tint upward. A shift
beyond ±2 needs a newly authored variant.

## 8. Roles and composition

**(lead's call)**

### 8.1 Roles

A role record sets three things: the stat shape, a default behaviour
profile, and kit and composition rules. The cards are in 17.1. A type may
override profile weights within ±50% of the role default; validation checks
the composed unit.

**Silhouette rule:** a unit's role must be readable at isometric zoom from
its model and issue gear, before any Lore. A bow means artillery, a tall
shield means soldier, a banner means leader. Issue helms (crests on
leaders) are display-only props. **(change AR-23)** The readability
charter's **role prop grammar** extends it: leaders carry a vertical
emblem; soldiers a tall flat object (shield, door, tray, coffin lid,
chair); artillery points something forward; brutes are oversized mass
inside one square; skirmishers lean low; controllers have an orbiting
element; lurkers show a tell at rest; minions are small and share one
palette **(change RT-17; run structure design 7.4 RC2)**.

### 8.2 Composition order

Fixed, and run on the dungeon random stream:

1. base type;
2. level shift;
3. rank template (minion or elite);
4. affixes (elites only);
5. theme overlay (M2: swaps one damage part's type and adds the matching
   trait profile; drawn only from the theme's `legalOverlays[]`, each of
   which keeps the theme's 6) **(change RT-6; run structure design 8.0)**;
6. loadout, including the notable item pre-assigned by the loot plan (15.3).

### 8.3 Affix rules

The catalogue is in 17.2.

- Always visible by name and one line of text, never behind Lore.
- At most one affix per exclusion group. At most one affix that applies a
  harmful condition.
- No affix grants a damage immunity, hard control, a recharge die, or
  anything the scorer cannot value.
- Every token or start-of-fight grant (Warded, Death-warded) is
  `oncePerLife`. It is granted at the unit's first combat start only, so
  re-engaging never refreshes it. **(change C-14)**
- **The fully composed unit must pass every budget:**
  - 2 harmful conditions (9.2);
  - the theme's 6 harmful conditions;
  - at most 2 resistances, with a weakness alongside any resistance;
  - at most 2 tag immunities, at most 1 of them control;
  - the kit caps;
  - the depth gates;
  - the ±20% threat band against its price (6.5).

  An affix that pushes a base outside the threat band is illegal on that
  base.

  **Pending request (HZ-24, M3; hazards design 9.2):** `floorborn`, an
  immunity to the theme's Stir acts, counts as one tag immunity, with no
  horizon-2 read; not yet ruled.
- **Commanding** is never applied to a member of a group that already has
  a leader, so a group has at most one (14.4). Validation checks it.
  **(lead's call)**
- **Per encounter:** affixed elites at most 1 on floors 1–3, 2 on floors
  4–5; each affix at most once per encounter and at most once per floor;
  at most one aura affix per encounter.
- **Content tests** enumerate every legal base × level shift × elite × affix
  (and affix pair) × theme overlay (every entry of the theme's
  `legalOverlays[]`; **change RT-6**). A broken combination fails the
  build, not a run.

## 9. Kit-building rules

**(lead's call unless marked)**

Build order:

1. role, then the table row;
2. at-will 1 (damaging, usually the loadout weapon);
3. at-will 2 (the role's verb: 17.1);
4. 0–2 limited abilities (an opener and/or a cooldown threat);
5. at most 1 reaction, with its automatic policy;
6. traits and layers;
7. count harmful conditions;
8. morale, behaviour and awareness profiles;
9. rank, affixes, theme overlay, loadout;
10. full validation.

### 9.1 Caps by rank

| | At-wills | Limited (`uses` / `cooldown`) | Reaction beyond the opportunity attack | Bonus-action ability | Harmful conditions | Concentration |
|---|---|---|---|---|---|---|
| Minion | 2 | 0 | 0 | 0 | 0 | No |
| Standard | 2 | 0–2 | ≤1 | ≤1 | ≤2 | Controllers and caster leaders |
| Elite | 2 (+ multiattack) | 0–2 (+1 with option b) | ≤1 | ≤1 | ≤2 | Controllers and caster leaders |
| Boss | 2 | 2–4 | ≤1 | ≤1 | ≤2 per phase kit, ≤3 across phases **(change C-2)** | Yes |

A shield bash exists for a monster only if it is authored as one of its
two at-wills (armour design 10.5). **(change AR-24)**

A carried notable item may add one limited ability beyond the cap. Its
conditions still count.

A **Guard verb** on a weapon (Parry, Set) is the unit's one reaction beyond
the opportunity attack. On minions, guard and condition verbs are inert and
shown greyed. **(change W-5; weapons 15.2)**

### 9.2 Counting conditions

The single counting rule for "2 conditions per enemy type". **(change C-2)**

- **Counts toward the type's 2:** harmful conditions the unit's own data
  can apply. That means its ability effects, its declared universal verbs
  (Shove gives Prone, Grapple gives Grappled, Frighten gives Frightened),
  flagged weapon riders (issue gear or carried), affixes and carried items.
  Condition verbs on a weapon (Topple) count here too; they are inert on
  minions. **(change W-5; weapons 15.2)**
- **Counts toward the theme's 6, but not the type's 2:** the *implicit*
  riders of every damage type the theme's monsters deal, which fire on a
  crit or against a weak target. The fire rider brings Burning and
  Smouldering, which count as 2 toward the theme's 6.
  Terrain conditions also count toward the 6 **(settled)**. **(change C-4)**
  In practice this pushes each theme to 2–3 damage types, which also helps
  readability.
  **Pending ruling (RT-15, conditions owner):** whether the radiant
  rider counts 2 toward the theme's 6, and whether Revealed is harmful
  at all. Not decided here; no M2 theme deals radiant, so it blocks only
  M3 themes that do (run structure design 8.0).
- **Counts toward neither:** own-side buffs (Rallied, Spurred, Helped,
  Warded on itself). They do count toward the wave's condition budget and
  the plate.

### 9.3 Limits and timing

- `uses:N` lasts the monster's whole life; `cooldown:N` counts at its turn
  start. **(settled)**
- **Cooldown minimum 2.** A cooldown of 1 is a disguised at-will.
  **(change A-2)**
- **New field `readyAt`** (default 1). It counts the **unit's own turns in
  the current fight**: an ability with `readyAt` N is first usable on the
  unit's Nth turn of that fight. Each unit keeps a `turnsTaken` counter,
  reset to 0 at every fight start, so a joiner entering in round 3 or a
  boss re-engaged after a reset also waits. **(lead's call)** Every
  party-facing **hard-control** ability has `readyAt ≥ 2` and `cooldown ≥
  3`, and is never opener-flagged. Nobody loses the hero's first turn to a
  stun. **(change A-2)**
- **At every combat end, each cooldown resets to ready.** Monsters have no
  turns out of combat, and `readyAt` already guards the next fight's
  opening. `uses` never reset. **(lead's call; change A-2, A-4)**
- `uses:1` signature moves are flagged as openers. On floor 1, no limited
  ability fires in a surprise round, and a floor-1 lurker never uses
  Ambush Strike in any round (its opener is its at-will at 150%, 17.1)
  **(change EP-11; encounter placement design 6.17; M2)**.
- **Pending request (BR-22, M2; boss rooms design 4.9):** every floor-1
  boss limited ability has `readyAt` ≥ 2, so with floor-1 Rhythm loops of
  ≥ 3 beats the first signature lands on boss turn ≥ 3; not yet ruled.
- **Pending request (BR-33, M3; boss rooms design 4.14.1):** a new
  readiness source: where a hazards design 6.17 Stir ships in an arena,
  one non-hard-control boss signature may be Keynote-bound, ready only in
  the round after each strike; a boss on its own due squares is Overtaken
  and its bound signature misses its next window, once per phase, no pip
  loss; not yet ruled (the HZ owner rules the Stir side).
- **Later (change LM-1; Arcanist design 9.3):** a `cooldown.delay` hook
  (the Loremaster's Unpick pushes a revealed `cooldown` or `readyAt` back),
  applied when the Loremaster ships.

### 9.4 Hard control

- Only on limited abilities; behind the depth gates (none on floor 1;
  elites and bosses on floor 2); checked at runtime. **(settled)**
- At most 1 hard-control source per encounter, 2 in a floors 4–5 boss
  encounter.
- **Majors on at-wills:** at most one per encounter, and none on floors
  1–2. The settled universal skill contests (Grapple, Frighten) are
  excepted.
- `worsen` appears only on limited abilities. Every condition in a kit must
  be one the scorer can value. **(settled)**

### 9.5 The M1 kit space

Floor 1 forbids monster hard control, bosses included. M1 monsters
therefore use only Prone (Shove), Grappled, Bleeding (the settled non-rider
claw), Frightened (a boss roar on cooldown, or Frighten) and Dazed
(limited).

## 10. Encounter budget and composition

**(agreed for party weight; the rest lead's call)**

### 10.1 Costs and budgets

**Unit cost** = `levelCost(unit level − room level of its homeRoom)` × rank
multiplier, plus the item cost add (15.3), computed exactly as in 3.1. It
is computed once, at generation, and **frozen** on the composed unit as
`cost` (19.1). Group costs, experience shares and cow eligibility read the
frozen value; stealing or using the item does not change it. **(lead's
call)**

| Level difference | −3 | −2 | −1 | 0 | +1 | +2 | +3 |
|---|---|---|---|---|---|---|---|
| levelCost | 4 | 5 | 7 | 10 | 14 | 20 | 28 |

**Party weight** = 15 for the hero + 8 per companion: solo 15, duo 23, trio
31, four 39. **(agreed)** It uses the **actual** party, fixed when the floor
is generated, so recruiting a fourth party member is a net gain **(change
CMP-4)**. On floors 2–5, less `partyWeight.shortfall[expected − actual]`
(config per cap, *illustr.* [0, 2, 3, 4] at cap 3), where expected is
`expectedCompanions(floor)`, computed from the companions design's slot
schedule for the floor being generated; shipped with the deterministic
solo composition table and `SoloBudgetMonotone` **(change CMP-5;
companions design 7.7; client agreed 2026-11-20, Q1)**. Solo
composition rule: a solo party on floors 2–5 meets at most 1 standard
unit plus the minion cap, the remainder spent by a deterministic table
(no RNG) owned here, mapping (floor, band, weight) to one composition;
`SoloBudgetMonotone` checks its fill and expected round damage are
non-decreasing in weight (companions design 7.7). A companion
lost mid-floor does not lower the floor's budgets. **(lead's call)**
The solo table gains a formation column (Picket Line, Crescent or Door
Line with a front standard; a solo shooter is a minion-cost unit), and
`SoloShapeMonotone` extends `SoloBudgetMonotone`: as weight rises the
standard's level never falls, threat lanes at fight start never
decrease, and front positions ≥ back positions. Formations declare core
and growth positions, filled core first. A solo hero on floors 2–5
meets no ambush form (no Cuckoo, Mousetrap or Waylay) and no Kill Box,
Crossfire, Pincer or Bait and Nook; `billetScale` is 0 for a solo hero
on floors 2–5 and 500‰ on floor 1. Every placement gate on party size
reads `partyCountAtGeneration` **(change EP-19; encounter placement
design 6.3; M1 static parts, M2 the rest)**.

**Encounter budget** = weight × band% × depth% × ladder% ÷ 1,000,000,
with `FloorDiv` (3.1). **(change M-4)**

- band: patrol 70, guard post 100, lair 140, optional elite room 160, boss
  200 (authored, including escort and reserves);
- depth%: 80 / 90 / 100 / 105 / 110 for floors 1–5. This meets main design
  4.6's "grows with depth" and cools the solo floor 1;
- ladder%: 100 at the base tier, +10 per difficulty tier. The ladder never
  raises caps or raw stats. **M3:** Nightmare tiers add kind, not stats,
  and the Perilous Door (+1 ladder on one floor) makes the ladder
  per-floor **(change RT-21, client, M3; run structure design 9.1)**.

The generator fills each encounter to **85–100%** of its budget, never
over: from the fill minimum of 3.1 (⌈85%⌉) up to the budget. Inside that
window the fill target rises along the main path: `fillTarget‰ = 850 +
FloorDiv(150 × mainPathIndex, max(1, n − 1))`, the minimum accepted
cost, with the lair at ≥ 925‰ and the elite room at 950–1000‰ *(illustr.)*
**(change EP-20; encounter placement design 6.2; M2)**. A **Mousetrap**
Ambush uses 85% of its band (`FloorDiv(guard × 85, 100)`); the Cuckoo and
the Waylay fill at their full band **(change EP-10; encounter placement
design 6.8.1; M2)**.

### 10.2 Worked examples

The arithmetic is checked; a content test asserts each one, including its
template, the 3-type cap and the role rule (10.3). "Room N" is the room
level of the group's homeRoom.

| Case | Budget (fill range) | Template: composition | Cost |
|---|---|---|---|
| Floor 1, solo, patrol, room 1 | 8 (7–8) | Picket: one level 0 soldier (−1: 7) | 7 |
| Floor 1, solo, guard post, room 1 | 12 (11–12) | Picket: level 1 soldier (10) + 1 level 0 minion (2) | 12 |
| Floor 1, solo, lair, room 2 | 16 (14–16) | Pack: level 2 brute (10) + 2 level 1 minions (2 + 2) | 14 |
| Floor 1, solo, elite room, room 2 | 19 (17–19) | Champion: level 1 elite soldier with one gentle affix (7 × 2 = 14) + 2 level 1 minions (2 + 2) | 18 |
| Floor 3, trio, guard post, room 5 | 31 (27–31) | Warband: level 5 leader (10) + level 5 soldier (10) + the same soldier type shifted to level 4 (7) + 2 level 3 minions (2 + 2) | 31; 3 types; 4 units (minions count ½) |
| Floor 3, trio, lair, room 5 | 43 (37–43) | Honour Guard: level 5 elite soldier with Bodyguard (20) + level 6 artillery (14) + 2 level 4 minions (2 + 2) | 38 |
| Floor 5, four, lair, room 9 | 60 (51–60) | Gauntlet: level 9 elite controller with two affixes (`FloorDiv(10 × 2000 × 1200, 1,000,000)` = 24) + two level 10 brutes of one type (14 + 14) | 52 |

Solo check for the floor 1 guard post: the hero takes about 8 damage, about
45% of 18 HP, falling to about 35% with limited abilities. Simulator target:
a guard post costs 25–35% of the party's HP-equivalents.

### 10.3 Caps by depth

A minion counts as ½ a unit.

| Floor | Units | Elites per encounter | Hard-control sources | Controllers | Ranged share | Ambushes per floor | Joining groups per fight |
|---|---|---|---|---|---|---|---|
| 1 | 4; ≤2 minions in a solo encounter; ≤2 brutes | Elite room only | 0 **(settled)** | ≤1, not in rooms 1–2 (`mainPathIndex` 1–2; change FL-4) | ≤ ½ | ≤1, not in rooms 1–2 (`mainPathIndex` 1–2; change FL-4) | ≤1 |
| 2 | 5 | 1 | ≤1, elites and bosses only **(settled)** | ≤1 | ≤ ½ | ≤1 | ≤1 |
| 3 | 6 | 1 (2 in the elite room) | ≤1 | ≤2 | — | ≤2 | ≤2 |
| 4–5 | 6 | 2 | ≤1 (≤2 in the boss encounter) | ≤2 | — | ≤2 | ≤2 |

On every floor:

- at most 3 distinct types per encounter (a level-shifted copy of a type
  is the same type);
- every encounter of 3 or more units (counted whole) has at least 2
  distinct roles, except Pack, Skirmish Screen and Scavengers; Picket and
  Sleeper are exempt **(lead's call)**;
- on floors 1–3, an encounter with a hard-control source or an ambush
  (the Waylay included) has no Quarry declarer (11.4) **(change EP-12;
  encounter placement design 6.8.1; M2)**;
- an **ambush** (the Ambushes per floor column) is one of four forms: the
  **Cuckoo** (a standard bait + 1–2 lurkers; full guard band; floor 1
  solo, or floors 2–5 at party ≥ 2), the **Mousetrap** (a bait cache + 1
  lurker, 2 from floor 3 at party ≥ 3; 85% band; floors 2–5, party ≥ 2,
  ≤ 1 per floor and ≤ 2 per run), the **Waylay** (an ordinary Stationary
  group in the Kill Box; full band; floors 2–5, party ≥ 2) and the
  **boxed lurker** (M3), which counts first. Only the Mousetrap can
  Surprise the party. The cap is a maximum, never a quota **(change
  EP-10; encounter placement design 6.8.1; M2)**;
- the **fairness bundle** holds for every placed encounter: `FirstSight`,
  `LandingShooters` (1/2/2/2/2 on the entry lane), `StartArc`,
  `BacklineReach`, `LeaderReach` and `AuraCover`, `CounterPresent`,
  `CounterCost`, the Kiter doorway lane, the LOS breaker, `LurkBlind`,
  `AmbushStandoff`, `TellSight` and KP5 (encounter placement design 9.2,
  6.8.3) **(change EP-8; M1 static parts, M2 the rest)**.

### 10.4 Floor plan and guarantees

A floor holds 4–6 fights plus the boss **(settled)**. **M3:** floors 2–5
may also hold at most one **Annex** group, outside the slots: Dormant
(Sleeping Ward; a boxed lurker keeps its traps and locks 6.14 state),
one extra encounter budget at the patrol band at its
host room's level, deducted from no slot, inside the cluster budget, the
joiner cap and the 6.4 rows, paying 0 experience (16), never a lurker
(except as the boxed lurker), elite or leader on floor 2; it serves
exactly one of the boxed lurker, the Wager guard, the guarded Dare
garrison or the Tattle beyond-room, in that priority **(change EP-26;
client agreed 2026-11-21, Q2; encounter placement design 6.13)**:

| Slot | Count | Placement |
|---|---|---|
| Patrol | 1 | Loops through 2–4 rooms, never the entry or boss room, and stays clear of main-path rooms 1 and 2 for the first 20 ticks **(change DH-28)**. Realised as the **Circuit**: 2–4 Room nodes, never rooms 0–2, the entry, the arena or the floor-1 antechamber; always the floors 2–5 antechamber, where it lives in the post-lair tail; at least one cover room; on floor 1 it hosts the CL-10 group; every Circuit square at effective path > 8 from each fight host under `hearingState(k)`, with the sight clause and the near-pass rule (floor layout design 6.7) **(change FL-10)** |
| Guard post | 3 (2 on a trimmed floor, read from `pacing.trimmedFloors[]`, when lever 3 applies in the lever order of companions 7.4; **change RT-7**; run structure design 6.6) | Main path, at chokepoints and doors. Exactly one is route-avoidable (the Bypass target, Gb); every other main-chain fight host is a cut vertex (floor layout design 6.8) **(change FL-9)** |
| Lair | 1 | Main path, past the midpoint; a cut vertex between the entry and the antechamber (floor layout design 6.2 LG6) **(change FL-9)** |
| Optional elite room | 1 (M2) | **Off the main path and visible before commitment** (seen through a Grille vantage, which passes sight and noticing only; floor layout design 6.5, 9.2; **change FL-7**). Its carrier is Stationary and awake, never Dormant. |
| Boss | 1 | An authored arena (17.5) |
| Trader room (Later) | 0–1, floors 2–4 | An optional off-path side room that can host the wandering trader, a non-combatant map object outside the encounter and cluster budgets **(change I-14; items design 10.9)** |

The placements are unchanged; layout realises them as slot hosts and
`equivalentHosts` (floor layout design 6.5) **(change FL-3)**.

Floor guarantees (the generator content test asserts each one):

- exactly one route-avoidable fight, a guard post: the Bypass (a quiet
  closed-Door corridor on floor 1; a Squeeze or a quiet two-door
  corridor on floors 2–5), with Last Turning drawn over the Blueprint's
  `bypassTargets[]` (floor layout design 6.8) **(change FL-9)**;
- at least one patrol routed through a room with cover, so the party can
  set an ambush (the Circuit's `coverRoom`; floor layout design 6.7;
  **change FL-10**);
- an awareness mix: at least one patrolling, one stationary and one dormant
  group;
- **M2:** at least one cowable group, and at least one leader group from
  floor 2 (morale ships in M2, so this is unconditional); floor 1's
  guaranteed cowable group lies on the main path before the boss room
  (classes design 8.6) **(change CL-10)**, on main-path Q room 3, which
  floor 1's Circuit includes (floor layout design 6.5, 6.7; **change
  FL-10**);
- **M2:** at least one steal target.

**Cluster budget** (M1): the groups within hearing of any room sum to at
most 1.5 × that room's budget (`2 × sum ≤ 3 × budget`). For a patrol, use
the worst point on its route.

Layout proves it statically **(change FL-11; floor layout design
6.6)**: under `hearingState(k)` (main-path Doors with index < k open;
in proofs every Door that is not `forcedClosed` counts open), with
Circuit-internal Doors open, plus generated-state and all-doors-open
reports; `HearingGap` > 8 effective path between every pair of slot
homes, repaired draw-free in LS12 over its repairable set; near pairs
only at effective path 9–10, capped per floor, with `nearPairMinIndex`.
At runtime noise and pathing read the live graphs. `ChainPullRate`
target 0.3–0.6 joins per floor *(illustr.)*.
**Pending (FL-11, the enemies owner's call):** the Gaol `pairDoor`
reading (a guard post and a neighbour at open path 6–8 through one
propped-open `pairDoor`, counted closed by the static check; a loosening,
told plainly) applies only if this owner accepts it; otherwise the Gaol
uses its one near pair.

**Variety:** no template twice in a row on the main path; each template at
most 2 times per floor (Picket is exempt on floor 1, where the solo budgets
allow little else; **lead's call**); each type in at most 3 encounters per
floor (the theme's minion type is exempt); the theme's signature type
appears at least twice. Also no (template, battle formation) pair twice
in a row on the main path; each battle formation at most 2 times per
floor; at least 2 activity kinds per floor when 2 or more non-patrol
awake groups exist; all are hard assertions **(change EP-20; encounter
placement design 6.2; M2)**.

**Later (EP-38, client only; encounter placement design 9.1):** the
Errant, an out-of-depth elite walking a side loop, telegraphed from
floor entry; its "deaf to the din" would change main design 4.3's
joining, so it comes back only as a client decision; not adopted.

### 10.5 Generator procedure

Deterministic:

1. **Inputs:** rooms, main path, `expectedTenths` per room (6.3), party
   weight, ladder tier, theme, and the floor's boss. Theme and boss come
   from the route pre-roll on the `route` child of the dungeon stream,
   consumed only at run start; each floor generates from a dungeon child
   stream keyed by floor index **(change RT-3; run structure design
   6.4)**.
1a. **Layout** (the `layout` child of the floor's dungeon substream):
   the floor layout design 6.1 runs here and outputs the rooms, main
   path, slot hosts and `equivalentHosts` **(change FL-3)**.
2. **Floor plan** (dungeon stream): assign slots to layout's slot hosts;
   fix each slot's homeRoom **(change FL-3)**. **Later (change I-14):** right after the slots are assigned,
   two dungeon-stream draws for the trader, always both, whatever their
   outcome: presence and stock count (items design 10.9).
3a. **LootPlan** (the floor's keyed `lootPlan` child of the loot stream,
   before step 3): over the provisional socket set (layout's containers
   and caches, the `traps` pass's lock kinds, step 2's slot plan and the
   carrier sockets predicted from step 4's fixed order), pick the
   floor's rarity-deck variant, deal the cards onto sockets by tier and
   socket rank, reserve the Dream's Due, and pre-draw dual identities
   and offer options. Fixed draw counts on keyed grandchildren; step 4
   takes no draw, so the prediction is exact. **(change LE-14; loot
   economy design 6.9 item 2, 6.10; M1)**
3. **Loot draw** (loot stream): the notable count per channel, then the
   items in channel order (containers, enemy-carried, boss), then wand
   charges in item order (15.3), then the next camp's `camp` channel (its
   count and notables) and the vendor's limited potions, still under the
   same family caps and owned-base rerolls **(change I-10; items design
   10.4)**. The only input from outside the loot stream is the party's
   `requires` tags and owned bases, read with the party snapshot's lanes
   and usability (`LanesOf`, `UsableBy`; change IR-2; item rolls design
   4.3) (and, Later, trader presence and
   count). **The loot stream is consumed only in steps 3a and 3.**
   **`lineRoll` and `curseGate` (change IR-14; item rolls design 4.1,
   4.17; M1 reserved, M2):** every identity, dreamed options and unused
   fallbacks included, has its own `lineRoll` entry (4 draws, always
   consumed: line, alternate line, element, LootPlan wand charges);
   `curseGate` takes 2 draws per floor (reserved M2, used Later); the
   boss-card and Viaticum fallback kinds grow from 3 to 5 entries. Parent
   identities keep their positions, and each identity is filtered before
   its draw (filter-first, 15.3 step 2).
   **(change LE-14)** The approved draw order above is unchanged: the
   count draws are consumed as dummy draws and their results ignored,
   since the deck of step 3a fixes the counts; channel slot *i* draws its
   ordinary identity on the *i*-th socket of that channel in allocation
   order, filtered to that socket's tier; the boss channel draws only the
   signature (the reward card's options are drawn in step 3a). The Due's
   slot is a ghost draw (15.3 step 2). The party snapshot at generation
   (owned gear, levels, training, living members) is also read, for the
   Due, fit premiums and usability fallbacks. **(change LE-2, LE-14; loot
   economy design 6.3, 6.10 items 2, 3, 7; M1 for F1, M2)**
   **Later (change I-14):** on a trader floor, the trader count is carved
   first from the containers share, then the enemy-carried share, each
   keeping at least 1 notable *(illustr.)*, never the boss; below 2 there
   is no trader. The trader's notables are drawn after the boss channel,
   then any returned trader slots on the containers channel, then wand
   charges, the camp channel, the trader's tonic (moved from the
   containers channel, no draw) and its service (items design 10.9).
   **The Reprieve Phial is never a weighted draw.** One pre-roll per run,
   on the loot stream, decides whether it appears, its floor and its
   channel; it replaces one notable draw of that floor and channel (a
   `campReward` phial takes a container draw of that floor). A declined
   offer is lost for the run (consumables design 14.7). **(change CN-27)**
   After the notable items and before the wand charges, the sundry and
   found-supply block runs in the order of the death and healing design
   12.5 (guaranteed potions and supply first among the sundries, fixed
   draw counts). **(change DH-27)**
4. **Carrier reservation** (no random draw): each enemy-carried item takes
   the first free carrier room in a fixed order: the optional elite room,
   then the lair, then guard posts by main-path index, then the patrol.
   The boss item goes to the boss room.
5. **Template choice** (dungeon stream): pick per slot, weighted,
   respecting the no-repeat rules. A carrier room's template must include
   a slot whose role and `usableBy` fit its item. The weights read the
   formation tier and the slot's position (the first main-path fight
   FT1; the lair its highest legal tier; from floor 2 the last guard
   post before the Forecourt FT2 or FT3); one draw picks the first of a
   ranked list **(change EP-20; encounter placement design 6.1, 6.2;
   M2)**.
6. **Fill** each slot against *budget − the item's cost add*, from the
   fill minimum to the budget (3.1), honouring `earliestRoomLevel` and the
   caps. Candidates are tried in a fixed order. The boss is not drawn
   here: it is the step-1 input, the first drawable entry of the route
   pre-roll's boss order for the (theme, floor) cell, over records whose
   `themes[]` and `eligibleFloors` match the cell; on floor 1 only bosses
   passing `Floor1BossMindReachable` (17.5) are placed **(change HR-21;
   Herald design 7.16; change RT-3; run structure design 6.4 R-SEED-3)**.
7. **Compose:** affixes, overlay, loadout; freeze each unit's `cost`.
8. **Validate.** On a failure, try the next candidate, up to K tries, then
   fall back to a Picket of the theme's cheapest legal type. Validation
   includes V12 `EncounterPlacement`, and the fallback chain is: the
   next-ranked formation for the same template, then the next template
   candidate, then the theme's cheapest legal Picket in Picket Line, then
   that Picket placed role-blind under the hard constraints only; every
   retry is logged **(change EP-32; encounter placement design 6.1, 11.2;
   M1 static parts, M2 the rest)**.
9. **Cluster pass:** downgrade or move the cheaper neighbour when the
   cluster budget is exceeded. A move stays within the group's slot host
   or goes to a host layout lists as equivalent for that role
   (`equivalentHosts`), never off a proven host; the pass is the
   backstop to layout's `HearingGap` **(change FL-3, FL-11; floor layout
   design 6.1, 6.6)**.
10. **Awareness:** set states, patrol routes, lurker placement rolls and
    ambush tells. Lurker placement Stealth takes no stream draw: it is a
    keyed hash (12.2) **(change EP-3; encounter placement design 6.8.5;
    M2)**.
11. **Experience:** fix each group's experience shares (16).

Every step except 3a and 3 draws only from the dungeon stream (the floor's
substream keyed by floor index; **change RT-3**), in the order listed.
**(lead's call)** Step 1a draws only from that substream's `layout`
child (floor layout design 6.1) **(change FL-3)**. Steps 2 and 4–11
(dungeon generation segment 6) draw only from that substream's
`encounters` child, after the `traps` pass and before `trapsResolve`,
with fixed draw counts and dummy draws **(change EP-1; encounter
placement design 6.1; M1)**. After `trapsResolve`, the draw-free
**LootBind** pass reads the final socket set: it applies the named
remaps (a carrier that falls back to a container in step 8, or whose
group moves in step 9, passes its card to a same-room container at its
tier), and computes salvage shares, the Heavy Purse and coin lumps
(15.7) **(change LE-14; loot economy design 6.9 item 7, 6.10 item 4;
M1)**.

Content test: for every theme × floor in its range × party size 1–4 (at
every reachable party weight, down to the shortfall minimum, 10.1;
**change CMP-8**) × band × ladder tier (and, since the boss is an input,
every boss record of that (theme, floor) cell; **change RT-3**), every
allowed template fills
within tolerance, and every
slot of the floor plan has at least one allowed template. The test runs on
the M1 roster and templates separately, so the M1 floor is proven fillable
on its own. The fallback should therefore never fire in practice. Every
theme that can appear on floor 1 has at least one compliant floor-1 boss
(17.5), since there is no fallback for bosses **(change HR-21; Herald
design 7.16)**.

### 10.6 Floor themes

M2.

- **Roster:** 5–7 types covering at least 4 roles, including at least one
  type each that can lead, be a minion, be promoted to elite, and is
  `armed`.
- **Budgets:**
  - the settled 6 harmful conditions, counting rider and terrain
    conditions;
  - no type resisted by every enemy, and no tag every enemy is immune to
    **(settled)**;
  - the water rule **(settled)**;
  - weaknesses across the roster cover at least 2 damage families;
  - at-wills use 2–3 damage types;
  - **at least half the roster is non-fearless.**
- **Readability:** one visual faction per theme; one signature mechanic
  expressed through existing rules (for example, webs and Restrained); on
  floor entry, a party member trained in Lore sees the roster's types and
  roles. **Pending request (EP-28, M2; encounter placement design
  6.20):** the preview also lists the theme's formations and activities
  by name; not yet ruled.
- **Next-floor line at camp:** every party sees the next floor's theme and
  its non-fearless band ("Next floor: theme X, about Y% non-fearless"),
  class-blind and information only **(change HR-23; Herald design 5.7)**.
  When route choice is on (`route.twoDoorsEnabled`, run structure design
  6.11), a door camp shows one such line per door **(change RT-10)**.
- **Theme record:** the record of run structure design 8.0, including
  `tier` (a pool and presentation tag), `eligibleFloors` (at most 2
  adjacent), `pairKey` (`primaryDamageType`, `squareKind`), `final`,
  `shipStatus`, `counterType`, `themeDamage`, `hardenedLayer`, `frost`,
  `legalOverlays[]`, `placementRules[]` and the Keepsake fields
  (`keepsakeProps`, `keepsakeArenaDressing`) **(change RT-2, RT-4)**.
- **Pools:** themes are drawn per floor from tiered pools (floor 1;
  floors 2–3 band; floor 4), or picked from two drawn candidates when
  route choice is on, with no repeat in a run; floor 5 is always the
  fixed final theme with a final-boss pool. Pool rules RP1–RP6 (no
  repeat, ContrastAdjacent, morale floor, RouteSufficiency, weakness
  coverage, damage spread) bind every theme **(change RT-2; run structure
  design 6.3–6.5)**.

## 11. Enemy AI

**(lead's call unless marked)**

### 11.1 The scorer and its terms

The settled one-step integer scorer, in hit-point equivalents, with chances
out of 20. It never rolls dice and uses thrift (default 150). **(settled)**
The added terms are data-weighted percentages. **(change A-6)**

| Term | Shape | Why | Wave |
|---|---|---|---|
| Approach | The best expected score reachable *next* turn from the destination, half-discounted | A one-step scorer otherwise stands still when nothing is in reach | M1 |
| Exposure | − the party's expected damage against this square next round × `caution%` | Artillery stays back | M1 |
| Opportunity cost | − the opportunity attacks the path provokes × `oaAversion%` (higher means it avoids them). Entering a Set zone costs the same way, but only while the zone owner's reaction is unspent, and only for the first unit each round that would trigger the owner's Set under its policy (by default the first non-minion) **(change W-5; weapons 15.4)**. Other *known* reaction triggers (the Hold the Line stop, Retort Strike, the Swordsworn's Counterstroke) are priced the same way, as state, as Interpose is previewed; the term never reads class (Vanguard design 7.4; Shade design 8.3) **(change VG-16; change SH-29)** | Brutes plough through; skirmishers Disengage or slip past; no Set-zone stall | M1 |
| Range | − \|distance to the nearest noticed target − `preferredRange`\| × step | Kiting | M1 |
| Hazard path | Settled 17.3 term; a cost of at least 100% for any hazard that could kill the mover | Never suicidal | M1 |
| Target preference | + bonus% × expected damage when the target matches the preference, capped at 25% of its best at-will | Personality without exclusivity | M1 (nearest); M2 (others) |
| Setup | Settled 17.3 term × `setup%` | Pack play | M2 |
| Focus spread | Beyond `freeFocus` (2) attacks on one unit in a round, later attacks lose 30% of their expected damage. A killing attack is exempt. The counter is kept per target, reset at each round start, and counts every enemy attack roll or forced save against it, boss actions and opportunity attacks included. | Limits dogpiling | M2 |
| Quarry | +40% to the group's expected damage against the Quarry. The declarer's own attacks are exempt from focus spread. | Honest, telegraphed focus | M2 |
| Cohesion, body-block, area spread, hold | Stay inside the leader's aura; guard a ward; spread out against the party's *known* areas; hold chokepoints | Formation | M2 |
| Leash | Candidate squares beyond `leash` of the post are dropped. **Sentinel profile and bosses only.** | Guards and arenas | M2 (bosses M1) |

Candidates are scored through the resolve preview, with armour verbs
and their spent state applied (Ringed, Proof); "lowest AC" means visible
AC (armour design 12.5). **(change AR-25)**

The AI force-door term (Wedge, change SH-18) is added only when the
Trapwright ships (Shade design 14).

**Pending request (FL-21 (c), M2; floor layout design 15):** a
doorway-hold AI term, against parties holding a chokepoint doorway; a
request to this owner, not yet a rule. The Billet term below (EP-6) is
offered as its answer.

**Pending request (EP-6, M2; encounter placement design 6.6.3):** the
**Billet term** specifies the "hold" and "cohesion" half of the M2
"Cohesion, body-block, area spread, hold" row: while the group's
formation holds, each candidate end square inside the unit's Billet hold
squares gains, once, `billet%` × `billetBase` × `billetScale`
(`billetBase` frozen per unit at composition, `billetScale` frozen per
group; weights *(illustr.)* Holder 40, Commander 30, Kiter 25, Raider 15
until its first attack, Charger 10, all others 0); the **invariant-7
guard** (when any candidate this turn attacks or deals damage,
candidates without an action lose the bonus); the **Stung rule** (a
unit's Billet weight is 0 for the rest of the fight once it takes damage
from a source it cannot answer from its hold squares; melee positions
also when no ally can damage that source); a **fall-back square**
destination bonus for Kiters and Commanders on a break, at ≤ 50% HP
(Kiter) or when adjacent to a noticed party unit; weights 0 after a
formation break (6.6.4 of that design). Invariants 1, 4, 7 and 10 (11.3)
are kept; not a leash. Not yet ruled. If refused, formations ship as
static starts with no AI term (encounter placement design 19.2).

**Pending request (TR-15, M2; room terrain design 6.9):** terrain terms,
not yet ruled: `NoCheapKill` with the exact text of room terrain design
6.9 (for each hero a candidate would damage, if the hero's current HP
minus the action's non-environment expected damage is ≤ the candidate's
maximum environment damage to that hero, that environment damage scores
0 and the candidate gets no kill or down bonus for that hero; non-boss
monsters, every trigger they cause: push, Shunt, Kick, Keg and Conduct;
agreed in substance, Q3); a destination value (the destination square's
`onEnter` and `onTurnEnd` value); Furnishing verbs valued by the existing
forced-movement, collision and area terms, light Smash by the reduction
in next-turn path length to the preferred target; a Keg trigger valued as
an area under the friendly-fire floor and `NoCheapKill`, and counted as
an attack in the intent read and focus spread; Shunt and Kick ties by the
fixed square order (ability 8.1). Cover adds no term: Exposure reads the
resolve preview.

**Pending request (hazards design 6.12, extends TR-15; M2 (gated), else
M3):** the destination value adds the value of an act due at this
round's end on that square (one step, no horizon-2 read); `NoCheapKill`
extends to due footprints, monster Cord use and vent mouths; the fall
value toward a given hero is scored once per fight (a board-history
term, so invariant 1 holds); not yet ruled.

**Pending request (BR-6, BR-14, M2; boss rooms design 4.3.1, 4.6):**
bosses only: the **Rhythm**, a per-band loop of Off, Lead and Down beats
read as a per-turn preference order over the scorer (never a wind-up,
no new search depth; it reads only the boss's own turn count and move
legality), with a Rhythm strip on the plate and initiative entry; and
per-boss data weights (Rhythm obedience, Device-face avoidance, lane
preference, escort Command toward screening the Device), `deviceBait`
and the doorway band; not yet ruled. If the Rhythm is refused, bosses
keep cooldowns only, with ready-state pips (13.2).

**Kill bonus** = min(`killBonus%` × the target's T, 1.5 × the attacker's
best at-will score). T is the target's best expected one-step score
(conditions 17.3). So a dangerous unit is worth more, and **nothing reads
"is the hero".**

Universal actions are valued: Dash and Disengage by the position terms;
Dodge by reduced exposure, when cornered; Search only when a party unit is
`lastKnown` within 6 squares and nothing is attackable; Help as setup;
Shove and Grapple with the forced-movement term. After each step the unit
rescores with what is left of its turn, so it can retreat after striking.

### 11.2 The Hidden rule

**(change A-5)**

- The AI reads the true state, **except that a party unit's position is
  known only through that monster's awareness record**: `unnoticed`,
  `noticed(square)` or `lastKnown(square)`. Each group's record also
  holds a per-fight flag `everNoticed[partyUnit]`: set on the first
  `noticed`, reset at fight start (trinkets design 13.4). **(change
  TK-26)**
- The record is shared within the group.
- **False Trail** **(change SH-20; Shade design 9.1)**: a class may
  write `lastKnown(square)` into a group's shared record for itself only
  when it is Hidden from every member of that group with line of sight
  to it; otherwise the record is unchanged. This is information the AI
  reads under this rule, not a targeting override: the AI still scores
  freely on what it knows (an enumerated `ForbiddenClassGrant`
  exemption, classes design 6.3).
- **Wise to You** **(change SH-22; Shade design 9.1)**: each group also
  holds `wise[partyUnit]` per fight, rising by 1 each time that unit's
  Hidden against a group member ends, up to `wiseMax`; that unit's later
  Hide or Vanish rolls against the group take −`wiseStep` × `wise`.
  Tuning: `wiseStep` 0 (ships off; a lever), `wiseMax` 2.
- A Hidden hero stays at its last known square for that observer. An area
  aimed there hits whatever is really there.
- Without this rule, Hide and Stealth do nothing against the AI.
- **Pending request (TL-16, M2; traps and locks design 6.8):** natives
  (every generated group and its joiners) read the floor's trap states
  as true state: trigger squares of armed traps are hazards (lethal ones
  barred by invariant 7, non-lethal ones costed, conditions design
  17.3), they step over known wires, and they trust device areas
  (entering one springs nothing); mindless types treat non-lethal traps
  as floor and spring them; **Once Bitten**: a group with any member
  inside the area of a device spring that resolves treats every known
  device area on the floor as a hazard for the rest of the floor (one
  bit, `bitten`, inherited by joiners); the opener rule (a spring that
  affects an unaware group starts combat as the springing unit's
  surprise action); monster pushes and Shunts score only traps the party
  has Revealed; not yet ruled. If refused: rigged-fight density drops
  until rigged share × party-hit ≤ 0.03 (traps and locks design 19.2).
- **Pending request (TL-17, M3; traps and locks design 6.8):** per-group
  trap belief (`armed`/`spent`) and witness-only secrecy: a trap the
  party re-arms out of a group's sight is unknown to that group, an A-5
  narrowing like this rule's; not yet ruled.

### 11.3 Fairness invariants

These hold on every difficulty tier and are asserted by property tests:

1. No term reads hero status, class or level.
2. **Monsters never target downed units and never aim areas at them**.
   Also enforced by the core: Downed units are inert (death and healing
   design 11.4). **(change DH-28)**
3. Focus spread applies as above.
4. No knowledge of hidden positions.
5. Reinforcements are announced at least 1 round before they arrive.
   Asserted at runtime as `ArrivalThroughPortal`: every non-Hidden unit
   entering a room containing a party unit enters through a Portal after
   its footsteps; no generated rest, camp or backtrack threats and no
   off-map reinforcements **(change EP-16; encounter placement design
   6.12, 6.21; M1 static parts, M2 the rest)**.
6. No limited abilities in floor 1 surprise rounds, no hidden lurkers in
   floor 1's main-path rooms 0–2, and the solo hero is never Surprised on
   floor-1 squares with main-path index 0 or 1 (death and healing design
   17.1). Main-path rooms are indexed from 0, the entry. **(change
   DH-28)** A floor-1 lurker never uses Ambush Strike, surprise round
   or not, and the floor-1 ambush is a Cuckoo only, only in the role
   order E G Gb Q L G F B **(change EP-11; encounter placement design
   6.17; M2)**. Floor-1 Landmark ticks are not hazards and leave it unchanged
   (hazards design 6.1, 7.2–7.4; change HZ-19; M3). Floor-1 traps keep
   it: no trap in main-path rooms 0–1 and no damaging trap in rooms 0–2
   (traps and locks design 6.19; change TL-19).
7. Stupidity floors: never enter a hazard that could kill it; no
   net-negative friendly fire; no control on a target that zeroes it; never
   idle with a reachable target. **Pending request (TR-15, M3; room
   terrain design 6.6, 6.9):** routed units never enter hazards, except
   non-lethal Remnants; not yet ruled. Never enter a Void **(change
   HZ-10; hazards design 6.4; M2 (gated), else M3)**. **Pending request
   (HZ-13 and hazards design 6.12; M2 (gated), else M3):** due squares
   are hazards for this invariant (hazards design 6.3.1); never end a
   turn on a due square whose act could kill this unit; routed units
   never path adjacent to a Void and treat due squares as hazards; not
   yet ruled. **Pending request (TL-16, M2; traps and locks design
   6.8):** armed trap trigger squares are hazards under this invariant
   (lethal ones barred, read on true state); `NoCheapKill` (agreed, room
   terrain design) extends to trap springs a monster causes; a monster
   with no path while under attack breaks line of sight and reroutes, or
   returns home, rather than standing still; not yet ruled.
8. No healing or refill of uses out of sight. The one bounded exception is
   the boss reset rule in 17.5.
9. Affixes are always visible.
10. The AI never reads whether its intent is readable. The read pass
    (13.3) consumes no random number and changes no state, except the
    explicit `AbilityRevealed` marks of a Tactician read (13.4), which it
    makes after every resolved command whether or not anyone hovers.
    **Hovering changes nothing.** **(lead's call)**

### 11.4 Quarry

**(change C-15)** Handed over by conditions 18.11.

- **Declared by** a Commander, a Stalker or the Hunter affix, at the start
  of its turn, before scoring, at no action cost. It has no mechanical
  effect, so it is not a condition.
- **Candidates:** non-downed party units recorded as noticed in the
  group's awareness record (the Hidden rule). **(lead's call)**
- **Target:** the candidate with the lowest (current HP − the group's
  expected damage against it this round). The group's expected damage is
  the sum, over members able to reach that unit this turn (movement plus
  reach or range), of each member's best at-will expected damage. Ties go
  to higher T, then unit order. It never reads identity. **(lead's call)**
- **Lasts** until the target is downed or out of every member's sight, or
  until the declarer's turn start after 3 of its own turns.
- **Shown** as a line from the declarer and an icon over the target, to
  everyone, whatever the guile (13.3).
- **Counterplay:** break line of sight, kill or Challenge the declarer, or
  bodyguard the target.

### 11.5 Difficulty knobs and performance

Knobs are data only, never scorer code, and never the invariants:

- thrift: 200 / 150 / 110;
- `freeFocus` / spread penalty: 1 / 50%, 2 / 30%, 3 / 20%;
- kill bonus cap: 1.0×, 1.5× or 2.0× the best at-will;
- `setup%`: 50 / 100 / 130;
- pursuit rounds: −1 or +1;
- noticing: −2 or +2;
- Quarry frequency.

The joiner warning never drops below 1 round. Guile is not a difficulty
knob. **(lead's call)**

**Performance:** p95 under 50 ms per enemy decision on the minimum PC. The
party threat map is computed once per enemy turn and also serves the
player's reach overlay. Intent reads (13.3) reuse the same decision code
and count against the same budget.

## 12. Awareness, noticing, hearing and pursuit

**(lead's call unless marked)**

### 12.1 Awareness states

Behaviour states, not conditions, so they sit outside the condition budget.
Each shows as a corner glyph.

| State | Behaviour | Counts as aware? |
|---|---|---|
| Dormant | Does not move; −5 to noticing; re-rolls only on noise it hears, on adjacency or on damage | No |
| Stationary | Holds its post | No |
| Patrolling | A fixed loop, 1 square per tick, pausing 2 ticks at waypoints. Once seen, the route is drawn and feeds the rest preview **(settled)**, drawn over known squares only (Seen, Remembered, Chalked), with an open "continues" end where it leaves known space, so it never leaks unexplored Circuit rooms **(change XI-6; exploration and information design 4.8.1; agreed, Q2; M2)**. | No |
| Alerted | Walks to a noise, looks for 3 ticks, then returns. **It does not become Wary**, so luring with noise stays a legitimate Stealth play. | No |
| Engaged | In combat, following its profile | Yes |
| Pursuing | Engaged but has noticed no one; moves to the last known square and Searches | Yes |
| Searching | After its profile's pursuit rounds, checks the last known square and up to 3 nearby squares in fixed order | **No**, so combat can end |
| Wary | Back at its post for the rest of the floor: cannot be Surprised, +3 to noticing. **Only groups that were Engaged and lost the party become Wary.** | No |

This answers ability model 20's "behaviour after losing sight": Pursuing,
then Searching, then Wary, with no healing and no refill.

The Cutthroat's "unaware" set is Dormant, Stationary or Patrolling, never
Alerted, Searching or Wary (12.2; Shade design 3.4) **(change SH-11)**.

**Start states and activities** **(change EP-9; client agreed
2026-11-21, Q4; encounter placement design 6.4, 6.7; M2)**: no new state.
Each type carries `allowedAwareness[]` (19.1); a group's start state lies
in its template's Awareness (17.4) ∩ every member type's
`allowedAwareness[]`, so the same type can be on guard in one room and
asleep in another. Each group's activity maps 1:1 onto an existing state
through a posture (Posted and Busy are Stationary, Dormant is Dormant,
Afoot is Patrolling) and changes no noticing value.

**Patrol forecast** **(change XI-6; exploration and information design
4.8.1, 4.8.2; agreed, Q2, Q4; M2)**: a drawn route feeds one core query,
`PatrolForecast(groupId, square, predicate)`, over **known** patrols
only (seen, or heard through Listen "passing" or F-LISTEN). It is
earned by continuously watching the group move 4 squares *(illustr.)*
of its route, or across one observed turn or Portal crossing, or by
Cased; a single glimpse gives only the route pip, and a heard-only
patrol earns only the heard Portal's window. It is shown as a window
("from +4 to +8 ticks, if undisturbed") over route squares seen
traversed with timing; horizon 20 ticks, or one lap (cap 60 ticks
*(illustr.)*) with a Perception-trained watcher by sight or Cased; it
is withdrawn by events, not age (noise that could reach the route, an
alert, a contradicting sighting, or the group last seen Alerted,
Searching or Pursuing). The rest preview, the Easy Plain auto-pick gate
and auto-Probe suppression read it, never true state. **Pacing gate
(agreed, Q2):** `PatrolFightRate` stays at or above 60% *(illustr.)* of
the approved-surfaces baseline (owned by EP); on a miss, in order, the
horizon stays 20 ticks for everyone, the forecast becomes hover-only,
then route and memory only. Patrol behaviour is unchanged.

**Pending request (EP-15, M2; encounter placement design 6.21):** the
Wary row's "back at its post" reads "its Billet hold squares" (the
Hedgehog: the group re-forms in its battle formation's defensive
variant, facing the Portal where the party was last seen; a group that
lost ⅓ of its members or its Keystone re-forms without Billets and
watches every Portal); not yet ruled. If refused, Wary groups return to
their start squares.

**Deferred (M3; encounter placement design 9.1):** the Messenger (EP-35:
a runner Rousing a target group to Stationary, a new Dormant→Stationary
route here) and the Wake Order (EP-37: per-member Dormant waking); not
adopted.

### 12.2 Noticing

**(change M-2, S-6)**

- Each unaware group makes **one roll per trigger**, using its best member
  with line of sight: d20 + Wisdom (+ proficiency if trained in
  Perception) against the party's passive stealth, the worst member
  **(settled)**.
- **Triggers:** first line of sight; the nearest party unit entering 6
  squares, then 3; any noise the group hears; an attack on it or its group,
  which is noticed automatically. It never rolls per tick.
- **Without line of sight** **(lead's call)**: a noise the group hears with
  no member in line of sight sets it Alerted toward the noise, with no
  roll. A band trigger with no member in line of sight is ignored.
- **Silent kill** **(lead's call)**: a kill by one attack, when no other
  awake member of the group has line of sight to the target or the
  attacker, is not noticed automatically. It is the only exception to the
  automatic notice, and the only way to set the `broken` flag's stealth
  sources (14.8): a leader's silent kill and the Cutthroat's Quiet Work
  **(change SH-10; Shade design 8.1)**.
- **Declaration-time reading** **(change SH-11; Shade design 8.1)**:
  Throat-Cut and the Cutthroat's unaware branch read the target's
  awareness, its group's record and `combatActive` at declaration
  (weapons 7.5 step 1), before the combat-start step of death and
  healing design 10.4. The blow stays in-combat damage. A clarification
  only: main 4.3 and death and healing 10.4 are unchanged.
- **Body noticed** (only if Quiet Work's second route ships in M2;
  otherwise Later) **(change SH-10; Shade design 8.1)**: a patrol that
  sees a corpse becomes Alerted to its square.
- The 6- and 3-square bands use the same grid distance as ranges.
  Noticing and Steal rolls use the **combat stream**, in the order the
  triggers occur. **(lead's call)** Lurker Stealth uses a keyed hash per
  roll, `H(floorSeed, unitId, "lurkStealth", band)` with band ∈
  {placement, 6, 3}, d20 = 1 + (hash mod 20), replay-stable and
  independent of draw order **(change EP-3; encounter placement design
  6.8.5; M2)**.
- **The party notices non-hiding monsters automatically on line of sight.**
  Only hiding monsters (lurkers) roll Stealth against the party's passive
  perception. They roll once at placement, and again only at the 6- and
  3-square bands. At a band the roll is compared against the highest
  passive Perception among living, non-Downed party units with line of
  sight to the lurker; with none in line of sight, that band is spent
  **(change EP-3; encounter placement design 6.8.5)**. Downed units
  contribute no knowledge, as in Quarry (11.4); line of sight stays
  symmetric, so a monster can still see a Downed unit **(change XI-2;
  exploration and information design 4.1.2; M1)**.
- **Surprise** **(settled)**: a monster group noticed without noticing
  back is Surprised. The party is read per side: it is Surprised only if
  no party unit has noticed any hostile unit taking part in that combat;
  a marked tell does not count as noticing **(change EP-2; client agreed
  2026-11-21, Q1; encounter placement design 6.8.2; recorded in M1, in
  effect in M2)**. Joining groups are never Surprised and never Surprise.
  Exception: the solo hero is never surprised in floor 1's first two
  main-path rooms (death and healing design 17.1). **(change DH-29)**
- **Pending request (BR-3, BR-4; boss rooms design 4.2.1, 4.2.2; M1 /
  M2):** a party unit entering an arena square (doorway squares included)
  counts as noticed by the boss's group, so the boss fight starts by
  noticing (`ArenaEngaged`, once per floor); from M2, when combat is not
  already active, **Take Places** (the player places each party unit
  out of combat in the Forecourt or on an arena square on either Arch's
  doorway band or placement squares, within its Speed) is the placement
  step of that combat start, before initiative; not yet ruled.

### 12.3 Noise

Measured as path distance, never through walls:

| Source | Radius |
|---|---|
| Walking; quiet checks | 0 |
| A failed quiet check | 4 |
| Loud checks (force a door, Athletics); each force try on a lock **(change TL-21; traps and locks design 6.11, 6.18)** | 6 |
| Each combat round | 8 |
| A loud trap or a shattered door (a loud spring, an alarm effect, a Crier or a Splinter; traps and locks design 6.18) | 10 |
| Lure: an exploration action at a seen square within 6; a group can be lured once per floor (`luredThisFloor`, 14.2) **(change SH-21; Shade design 9.1; if the enemies owner accepts)** | 4 |
| A Curiosity's priced noise: generic noise at the card's square only, resolved by these hearing rules with the joining caps (12.4); never a Lure and never sets `luredThisFloor`; it moves units only through the hearing rules (the EP 13 reading) **(change EV-14; events design 4.4.4, 4.8; M2)** | As printed (≤ 4 on floor 1, never reaching a Circuit square: `F1NoiseClearOfCircuit`) |

Modifiers: each closed door on the path −3; Conspicuous +3 **(settled)**;
Hushed silent **(settled)**; Dormant groups hear at half radius.

**Pending request (TL-30, M2; traps and locks design 6.18, 6.11):**
alarm traps are one-shot, or re-arm at most once per floor; groups that
arrive because of an alarm or a Crier arrive Alerted and cannot be
Surprised that fight; not yet ruled. **Pending request (TL-41, M3; traps
and locks design 6.3, 7):** a scent modifier: the `scent` trigger's
noise (the Scent Bladder's 8) is heard by hound families only; not yet
ruled.

Portal kinds **(change FL-7; floor layout design 9.2)**: a closed Door,
Locked Portal or latched Latch is a closed door (−3); Arches, drawn
Drapes and Squeezes add 0; corridors add their length; Grilles, the
Bossgate and unfound secrets are not hearing paths. At runtime the
radius reads the live hearing graph, each Portal in its current state
**(change FL-11; floor layout design 6.6)**.

### 12.4 Joining a fight

**(change M-3)**

1. At the start of each combat round, groups inside the fight's noise
   radius that are not Cowed or already Engaged become Alerted toward the
   fight and move at full speed. The radius is measured as path distance
   (12.3) from the nearest square occupied by any combatant. **(lead's
   call)**
2. **Selection and cap:** 1 joining group per fight on floors 1–2, 2
   deeper. When more groups qualify than the cap allows, the shortest path
   wins, then the lower `groupId`. Groups beyond the cap **do not change
   state** (they stay Stationary, Patrolling or Dormant) and are never made
   Wary. **(lead's call)**
3. **A directional "footsteps" marker appears at least 1 round before
   arrival.** The core knows the path length, so the timing is exact. With
   a party member trained in Perception, the warning comes 2 rounds ahead
   and shows the group's size. This is the M1 reinforcement warning (13.1).
   The warning is guaranteed by delaying the group, never by skipping it:
   **arrival round = max(path-based round, warning round + 1)**, or
   warning round + 2 with a trained member. **(lead's call)**
4. Once a group gains line of sight, it **enters at the start of the next
   round**, keeping its own morale group. Each joining unit rolls
   initiative on the combat stream at the start of its entry round, after
   the round-start effects and in unit order; a shared minion slot rolls
   once, with its lowest-id member's Intelligence. **(lead's call)**
   **Pending request (BR-3, M2; boss rooms design 4.2.1):** if combat is
   already active when `ArenaEngaged` fires (a floors 2–5 Forecourt patrol
   fight), the boss group joins it as joiners under this item and Take
   Places is skipped; not yet ruled.
5. **Other groups during a fight:** while combat is active, every group not
   in it (patrols, Alerted and Searching groups) advances one tick per
   round, at round start, after the joiner checks. **(lead's call)**
6. **Hearing outline** (M2): out of combat, the map shows which known
   groups a fight *here* would draw in. A chain-pull is then a decision,
   not an ambush. Listen at a closed door feeds it (floor layout design
   9.2) **(change FL-18)**. The layer is labelled "known groups only"
   (reading, XI-13; exploration and information design 4.8.4).

Placement keeps this model unchanged and adds only its own levers and
assertions (Dormant near-pair partners, `JoinedRangedShare`,
`JoinedUnitsCap`, at most one placed call source per encounter,
arrival-Portal arrows; encounter placement design 6.12; EP-22, a
reading; M2). **Deferred (M3; EP-36; encounter placement design 9.1):** the
Call Post, a bell rope or gong sounding noise 10 within these joiner
rules; not adopted. The Errant (EP-38) is a Later client item (10.4).

**Doors:** monsters open unlocked doors (1 square of movement) and never
close or lock them (read by the fog's Remembered table, XI-5;
exploration and information design 4.2). Locked doors stop monster paths, which is a Mechanics
payoff. (Wedge and the force-door term, change SH-18, apply when the
Trapwright ships; Shade design 14.) Monsters move on the monster
movement graph: they pass Arches, corridors and Drapes (leaving Drapes
open) and never use a Squeeze, a Grille, the Bossgate, an unfound secret
or an unlifted Latch; a Grille passes sight and noticing only **(change
FL-7; floor layout design 6.6, 9.2)**. Monsters never lift a Latch; its
`barSideRoomId` is the deeper end, by the return-Portal principle
**(change FL-19; floor layout design 9.2)**. The party opens unlocked
doors by moving into them (M1) and may close them (M2) under the party
door verbs; monster door rules are unchanged **(change FL-8; floor
layout design 9.2)**.

### 12.5 Pursuit

Pursuit rounds by profile: Charger, Stalker and Ambusher 3; Holder, Mob,
Automaton and Raider 2; Kiter, Disruptor, Commander, Protector and
Opportunist 1; Sentinel 0 (2 once Provoked, 17.3); Boss 0, leashed to its
room. **(lead's call for the added profiles)** A hero who breaks line of
sight and keeps moving is safe within 1–3 rounds, which is the settled
escape valve.

## 13. Telegraphs and intent reading

**(agreed; details lead's call where marked)**

The job split **(agreed)**: Intelligence and Lore tell the party **what an
enemy is** (traits, resistances, abilities, cooldowns). Wisdom and
Perception tell it **what an enemy is about to do**. Enemies keep some
ability to surprise mid-combat: a guileful enemy's intent stays unreadable
to a party that has not invested in Perception.

**Owed at M2 (change LM-0; Arcanist design 9.3):** the enemies owner sets
Study's difficulty (Lore, ability model 7.2) and how long a Study's
reveal persists; the Loremaster's free Study rider reads both.

### 13.1 Baseline for everyone

M1, whatever the party's skills. **(agreed)**

- **Threat-reach overlay:** on a toggle or hover, the squares each visible
  enemy can attack next turn. Ready limited abilities that have been seen
  show in a second tint.
- **Turn log:** every enemy action, target and result, in order.
- **Announced boss signature moves** (13.2).
- **Reinforcement warnings:** the joiner "footsteps" marker at least 1
  round ahead (12.4).

Further telegraphs, all **(lead's call)**:

- Cooldown "ready in N" and uses left, once seen or revealed by Lore
  **(settled)**; awareness glyphs (M1).
- Challenged and Quarry lines, leader aura rings and the hearing outline
  (M2).
- Boss phase marks, resolve pips and the boss-action count on the
  initiative bar (M1 marks and pips; M2 boss actions).
- The formation chip and the Keystone chip on the plate (never on the
  model), and the public break triggers: Keystone lost, leader falls,
  line crossed, Demoralised, a third of the group's members down
  (counted in noticed members, never HP), the end of its third contact
  round; on a break the chip reads "Line broken"; a chip names a
  formation only once every member of its encounter has been noticed
  (`ChipNoLeak`) **(change EP-7; encounter placement design 6.6.4, 6.20;
  M2)**.

### 13.2 Announced boss signature moves

**(agreed that they are always announced; mechanism lead's call)**

- Each boss flags 1–2 of its limited abilities as `signature` in data. The
  M1 boss flags its roar and its limited Dazed, which enters its kit at
  phase 2 (17.5).
- **From the boss's first turn,** the signature moves in its current kit
  are listed on its plate and initiative entry with name, shape and ready
  state, without waiting to be seen or revealed by Lore. A phase that
  swaps one in lists it on entry. From M2 the boss's Playbill also lists
  them, with their shapes, before the fight, from the Stratum Card
  **(change BR-5; boss rooms design 4.12; M2)**.
- **Whenever the boss would use a signature move if its turn began now**
  (computed live by the real AI, with exactly the read simulation of
  13.3), a banner on the initiative bar shows the move and its target or
  area, to everyone, whatever the boss's guile and whether or not the
  party could otherwise read it.
- It is a forecast, not a commitment. If the party changes the state, the
  banner updates. The settled deferral of scripted wind-ups (ability model
  5) is unchanged; committed signature moves stay a Later option (21),
  except **Gambits**: one `gambit`-flagged boss signature per boss, never
  on floor 1, committed at the end of the boss's own turn on squares the
  existing area scorer picks one-step, resolved at its next turn start,
  with an escape guarantee and no digit on the map **(change BR-32; boss
  rooms design 4.14.3; client agreed 2026-11-22, Q3; M3 gated, else
  Later)**.
  A Stir's announced act (the Dream step) is an environment act, not a
  wind-up, so the deferral stands **(change HZ-12; hazards design 6.3;
  M2 (gated), else M3)**. **Pending request (HZ-24, M3; hazards design
  9.2):** Stoke, a monster kit action that moves a Stir's countdown 1
  toward acting (C becomes max(2, C − 1); never makes an act due in a
  round where it was not due at round start), with telegraphed intent,
  one Stoker type per theme; not yet ruled.
- Boss actions (M2) that use a Signature are announced the same way before
  the turn they would follow.

### 13.3 Intent reading

M2. **(agreed)**

**Guile.** Every enemy has a derived guile **(agreed)**:

guile = 10 + Wisdom modifier + proficiency bonus + role adjustment

Proficiency is added for **every** enemy, not only types trained in
Perception, so guile grows with level. **(agreed)**

| Unit | Adjustment |
|---|---|
| Brute, minion, mindless type (Automaton profile) | −2 |
| Soldier, artillery, skirmisher | 0 |
| Leader, controller, lurker | +2 |
| Boss | +2 (signature moves always announced, 13.2) |

- **Precedence (lead's call):** boss, then mindless, then role. Exactly one
  adjustment applies: a mindless controller is −2; a boss is +2 whatever its
  role. Elite rank, affixes and level shifts add nothing beyond the Wisdom
  and proficiency they change. A minion uses −2 whatever its role.
- Guile is a derived number, never stored (change S-11). In M2 no condition
  or item changes it. **(lead's call)**
- Illustrative: a level 1 brute with Wisdom 0 has guile 10 (10 + 0 + 2
  − 2); a level 5 soldier with Wisdom 0 has 13; a level 5 leader with
  Wisdom +2 has 17; a level 9 lurker with Wisdom +1 has 17; a level 9
  controller with Wisdom as its main ability (+5, 5.3) has 21. A trained
  level 1 hero with Wisdom +1 reads 13; a trained level 10 hero with
  Wisdom +3 reads 17, or 21 with mastery.
- **Calibration:** because guile and a trained party's reading score both
  grow with proficiency, Perception investment keeps its value with
  depth: an untrained party reads mainly low-guile roles, a trained one
  most enemies, and mastery nearly all. The role adjustments and monster
  Wisdom are the levers; the readable-share gate in 20.3 (about 40%
  untrained, 75% trained, 95% with mastery) checks the result in the
  simulator.

**The reading rule.**

- The party's reading score is its **best passive Perception**: the
  maximum, over the party members in the fight who are not downed, of each
  member's full passive perception: 10 + Wisdom, + proficiency if trained
  (double with mastery), plus active effects. **(settled best-member rule,
  stat model 5.4; change S-12)** Conditions and effects that change a
  passive value apply, so readability can change mid-fight; the score is
  recomputed with the reads after each command.
- If reading score ≥ an enemy's guile, hovering that enemy shows **whom it
  would target "if its turn began now"**: the target unit, or "no attack"
  when the decision is a move, Dash, Dodge or a buff. Otherwise the hover
  says **"Intent unreadable"**.
- There is **no roll**: it is a threshold, deterministic, and repeatable
  without cost. **(agreed)**
- **Scope:** reading applies to the **next 1 enemy** in initiative order:
  the next enemy-side initiative slot due to act after the current turn.
  **(agreed)** Details **(lead's call)**:
  - the next N enemy-side slots after the current turn are counted,
    wrapping into the next round; dead and removed units are skipped;
    a hidden unit's slot is counted but returns Hidden;
  - a shared minion slot counts as one slot and its read shows each
    member's target;
  - a joining group counts once it has rolled initiative;
  - boss actions are not slots: they are covered only by the signature
    forecast (13.2).
- **The enemy must be visible to the party:** at least one non-downed
  party member has line of sight to it. A hidden lurker shows no intent.
  **(lead's call)**
- **Computed live by the real AI.** The read runs the monster scorer with
  the same inputs as a real turn: the Hidden rule (11.2), its awareness
  record, cooldowns and uses. It commits nothing: when the enemy's turn
  actually comes, the AI decides again from the state then. **(agreed: no
  committed intents)**
- **What "if its turn began now" simulates** **(lead's call)**. The read
  clones the state and, on the clone only:
  1. applies that unit's deterministic turn-start steps: cooldown
     countdowns, duration decrements, and ongoing damage at its average,
     never below 1 HP;
  2. **skips every rolled turn-start step** (saves to end conditions,
     aura saves such as Chilling); the conditions stay as they are;
  3. evaluates `readyAt` against `turnsTaken + 1` (9.3), and cooldowns
     against the round in which that slot will act;
  4. adds a Quarry declaration if the unit is a declarer that would make
     one (11.4);
  5. runs the scorer step by step, applying its moves without resolving
     any opportunity attacks, until its first ability use. That use's
     target (and, with Tactician, its ability) is the read. If that use
     targets no party unit (a buff, Dodge), or the turn ends with no
     ability use, the read is "no attack".
- **Read results:** `NotInScope`, `Hidden`, `Fleeing` (a Routed unit,
  14.5), `Unreadable` (with the guile once Lore has revealed the type),
  or `Target` (a unit or none, plus the ability with Tactician). A
  cornered router (14.5) returns `Target`. **(lead's call)**
- **The read pass (lead's call):** the core recomputes every read after
  each resolved command, never per frame, and caches them until the state
  changes. It consumes no random number. Its only state change is the
  Tactician reveal of 13.4, made whether or not anyone hovers (invariant
  10, 11.3).
- **Guile display (lead's call):** an unreadable hover shows the guile
  number only once Lore has revealed that type; otherwise it says "beyond
  your Perception". A readable hover always shows the target line.
- **Quarry and signature announcements** are shown to everyone, whatever
  the guile: they are honest, telegraphed focus (11.4, 13.2).

### 13.4 Tactician

**(agreed for the effect; placement lead's call)**

- **Tactician** (name illustrative) is the **signature use of Perception
  mastery** (stat model 7.2), so it is earned through a pick-one-of-three
  offer like any mastery. **(lead's call; change S-10)**
- If any party member in the fight who is not downed has it, readable
  enemies also show **which ability** they would use, and reading extends
  from the next 1 enemy to the **next 3 enemies** in initiative. The guile
  test still uses the party's best passive Perception. **(agreed; party-wide
  application lead's call)**
- **Reveal (lead's call):** an ability named by a Tactician read counts as
  seen in play. The read pass marks it seen at the end of the command that
  produced the read, as an explicit state change raising
  `AbilityRevealed`, whether or not anyone hovers. So the state never
  depends on the UI, and replays stay deterministic.

### 13.5 What reading does not do

**(lead's call)**

- The party scorer, and so companion suggestions and the simulator's party,
  does not read intents in M2. Feeding reads into suggestions is a Later
  option.
- Reading never reveals traits, resistances, HP thresholds or unseen
  abilities except the one ability Tactician names, which then counts as
  seen in play **(settled "seen in play" rule)** through the reveal of
  13.4.
- The preview (ability model 8.3) keeps using only what the party knows. An
  intent read is the one place the display shows the monster AI's own
  output. **(change A-8)** **Pending request (EP-28, M2; encounter
  placement design 6.20):** a second place: the Encounter Read card shows
  a group's Billet hold squares and fall-back square to parties with a
  Perception-trained member (display only; the AI never reads whether it
  is shown, invariant 10); not yet ruled. If refused, no hold overlay.
- **Open item (XI-12, XI-27; M3, conditional; exploration and
  information design 4.8.3, 4.5.2):** Lore does not reveal a group's
  Perception bonus today. The noticing-roll tag on the move preview
  shows odds only where Lore has revealed it, and Cased's odds band
  (Shade request) waits on the same flag; both are gated on EP-41's
  ruling (pending client); not yet ruled by the enemies owner. If
  refused, the tag and Cased show no odds.

### 13.6 Rejected alternatives

- **Target-intent hover for everyone** (the lead's earlier proposal):
  Perception would have had no fight job, and enemies no way to surprise.
- **Committed intents** (Into the Breach): every enemy would need a
  wind-up the one-step scorer cannot reason about **(settled deferral)**.
  Boss Gambits (13.2) are the one exception: their squares are picked
  one-step on present occupants, so this objection does not apply
  **(change BR-32; boss rooms design 4.14.3; agreed, Q3; M3 gated, else
  Later)**.
- **A rolled Insight check:** a die the player cannot plan around, and a
  free retry each hover.
- **Reach overlay only:** kept as the baseline, but alone it gives
  Perception nothing to do in a fight.

## 14. Morale

**(agreed for the model and for M2; details lead's call unless marked)**

### 14.1 Principles

- **Deterministic, visible, group-based.** There is no meter and no morale
  dice. Intimidation is the only roll. **(agreed)**
- The breaking point is shown on the plate and in the preview ("Kill: group
  breaks; 2 members flee").
- **Party morale: none.** Agency conditions are enemy-only because the
  player controls every unit **(settled)**. The hero's life already *is*
  the party's morale.
  - The party meets only fear effects: Frightened, and Shaken (Later).
  - Leadership's Rally is its cure **(settled)**.
  - Panicked, Routed, Demoralised, Cowed and Yielded never apply to party
    units, and validation enforces this.

### 14.2 Groups and profiles

**The group** is the morale unit. Record: `{groupId, homeRoom, members,
leader?, startCost, lossCost, flags}` (19.1). Flags include
`luredThisFloor` (Lure, 12.3; reset at floor end) **(change SH-21; Shade
design 13.1)**.

- `startCost` is the sum of the members' frozen `cost` (10.1) when the
  group **enters** the fight; members already resolved earlier on the
  floor are excluded.
- `lossCost` is the summed frozen cost of members who are dead, Routed,
  Yielded or Fled. **A member counts as lost the moment it is Routed.** A
  Rally that ends Routed takes its cost back out.
- **Membership is fixed at generation** and joining never merges groups.
  Groups that join by hearing keep their own id and leader. A boss's
  authored reserve groups (17.5) are created with the boss's `groupId`, so
  they are its escort; an ordinary joiner at a boss fight keeps its own
  group. **(lead's call)**

**Morale profiles** are a monster-data field read by the morale engine.
They are not a tag immunity, because the `morale` tag allows none.
**(change C-13)** **Each member checks group events against its own
profile.** There is no "group profile".

| Profile | Demoralised when its leader falls (T1) | Demoralised by losses (T2) | Demoralised by the first loss (T3) | Demoralised as last survivor (T4) | Routs while Demoralised at | Default for |
|---|---|---|---|---|---|---|
| `fearless` | No | No | No | No | Never; also never Cowed or Yielded | Mindless types, constructs, most undead, bosses |
| `steady` | Yes | Only with no living leader | No | Yes | ≤ ¼ HP **(change C-7)** | Elites, fanatics, bodyguards |
| `normal` | Yes | Yes | No | Yes | ≤ ½ HP **(settled)** | Most types, **minions included** |
| `skittish` | Yes | Yes | Yes | Yes | ≤ ½ HP | Standard-rank scavengers and animals |

- `mindless` implies `fearless`. Bosses are always `fearless`.
- **Fear immunity and fearlessness are separate axes.** A `fear`-immune
  `normal` type ignores Frighten and cowing but still breaks when its
  leader dies. A `fearless` type can be Frightened but never Routed.

### 14.3 Triggers

The engine runs a **morale checkpoint** after each top-level resolution
unit: a whole ability use (all its targets, damage and deaths), one square
of movement including any opportunity attacks it provoked, one turn-start
or turn-end phase, or one boss action. **(lead's call)**

- Loss events (a death, a removal, a Rout, a Yield, a Flight) are appended
  to a **FIFO queue** in the order they happen.
- The checkpoint pops events in order. For each, it evaluates T1–T4 for
  the affected group's members in unit order and applies the resulting
  states. Any Routed loss this produces is appended to the queue.
- HP-line Rout checks for Demoralised units run in the same pass.
- It stops when the queue is empty, so cascades resolve deterministically
  and a unit never routs partway through an area's per-target resolution.
- A Void fall (`voidFall`) is a removal loss event, and the Dream step's
  sub-step 2b is a checkpoint **(change HZ-10; hazards design 6.3, 6.4;
  M2 (gated), else M3)**. A trapdoor's runtime Void (F2+, V10-proven
  Void rooms only) is an ordinary Void, and its `voidFall` reads as
  written (traps and locks design 6.3; change TL-31; M2).

**Every trigger is edge-triggered**: it fires on the event, never merely
because a count sits at a threshold.

| # | Trigger | Effect | Wave |
|---|---|---|---|
| T1 | **The leader falls** (dies, or is Routed, Yielded or Fled) | The Emboldened aura ends (14.4). Every other non-fearless member of the leader's group is Demoralised. This replaces the settled Emboldened `onEnd` relation **(change C-18)**. | M2 |
| T2 | **A loss event leaves `2 × lossCost ≥ startCost`** | Demoralises each `normal` or `skittish` member that is **not Emboldened**, and `steady` members only if no leader lives. It fires again on each further loss at or past the line, so a rallied group re-breaks on its next loss. | M2 |
| T3 | **The group's first non-minion loss** | Demoralises its `skittish` members that are not Emboldened | M2 |
| T4 | **Last survivor:** a loss leaves one non-lost member, in a group that had ≥2 members at fight start | That member is Demoralised if it is non-fearless. A lone unit is never Demoralised at fight start. | M2 |
| T5 | **A Frighten would land on a Demoralised enemy** | **Routed** instead of Frightened, by morale-engine substitution (below). Demoralised is not consumed. The roll already has advantage **(settled)**. | M2 |
| T6 | **A limited fear effect would land on a Demoralised enemy** | Routed, by the same substitution | M2 (scrolls and class fear spells) **(change CL-1; classes design)** |
| T7 | **The boss falls** | Every non-fearless escort member (the boss's group, its reserve groups included; not ordinary joiners) is Routed. Mindless servants go **inert** and are removed as resolved. | M2 |
| T8 | An ally within 3 squares is downed or killed | Shaken **(settled)**. **Minion deaths do not trigger it.** | Later |
| T9 | A party Frighten misses by 4 or less | Shaken **(settled)**. On a leader, Shaken blocks Rally. | Later |

**Pending request (HZ-24, M3; hazards design 9.2):** a new
edge-triggered event, **Startle**, fired by a Stir act or a Cord: the
named groups' skittish, non-Emboldened members become Demoralised, at
the same FIFO checkpoint; fearless and mindless units are unaffected;
not yet ruled.

**Pending request (TL-30, M2 or M3 as the owner rules; traps and locks
design 7):** a new morale trigger, the Rattle Wire's "rudely woken"
rider; not yet ruled. If refused, the Rattle Wire is noise 10 only
(traps and locks design 19.2).

**The T5/T6 substitution** **(lead's call; change C-12)**. These are not
condition transforms:

- The Frighten (or limited fear effect) runs the full conditions pipeline
  as Frightened (or as its own condition), with every block, ward and
  resolve step.
- If it reaches step 9 (it would be applied) and the target is Demoralised
  and not Emboldened, the condition is discarded with no `onApply`, any
  step 6 transform it took part in is not applied (its source is not
  consumed), and the morale engine applies **Routed** directly with
  trigger T5 or T6, before the checkpoint. Otherwise it lands as normal.
- **Precedence:** when Routed and the settled Frightened + limited fear →
  Panicked transform are both possible, **Routed wins**: a fleeing unit
  gains nothing from Panicked.

**Routing rules:**

- A Demoralised unit at or below its profile's HP line becomes **Routed**:
  `2 × hp ≤ maxHp` for `normal` and `skittish`, `4 × hp ≤ maxHp` for
  `steady` (3.1). This is checked whenever its HP changes and when
  Demoralised is applied.
- **A minion has no HP line.** It is Routed at once when Demoralised **by
  T1 or T2**. A minion Demoralised by T3 or T4 stays Demoralised, and
  Frighten (T5) still routs it.
- Pack templates therefore do not collapse on the first kill. Minions cost
  about ⅓ of a standard, so killing two of them rarely crosses half losses.
  Killing the pack's alpha or leader does break them, which is the readable
  lever. Every leader template also holds at least one non-minion besides
  its leader (17.4), so one kill never ends it.
- Simulator target: under 10% of Pack fights, and under 10% of fights
  against any leader template (Warband, Den, and any template given a
  Commanding elite), end through morale before round 2.

**The readable Intimidation play** is T1 → T5 → T2:

1. Kill the leader. The preview says "Kill: group Demoralised".
2. Frighten the strongest survivor, with advantage, to Rout it.
3. Its loss pushes the rest past half, and the fight collapses.

### 14.4 Leaders, Emboldened and Command

- **Leader is a role, not a rank.** A leader can be a standard, an elite
  with the Commanding affix, or the boss for its escort. There is at most 1
  per group, and a leader's group needs at least 2 other members.
- **Emboldened** is an aura on members of the leader's own group within 2
  squares of the living leader, not on the leader itself. Units of other
  groups standing in it are unaffected **(lead's call)**. It gives:
  - advantage on saves against fear;
  - Frighten against it has disadvantage;
  - blocks Routed and Panicked **(settled)**;
  - **shields members from T2 and T3** **(change C-9)**;
  - offers no shield against T1, because the aura ends when the leader
    falls. It ends with no `onEnd` relation; T1 does the Demoralising,
    so fearless members are never Demoralised by it **(change C-18)**;
  - **Waver:** while the leader's Frightened penalties are active (its
    source in line of sight), its aura is **suppressed**: members gain no
    Emboldened, Frighten has no aura disadvantage, and Routed, T2 and T3
    are no longer blocked. Suppression is not an end: no `onEnd` and no
    T1 (C-18 unchanged); members Demoralised during the gap stay so until
    a Rally. The aura returns when the leader stops being Frightened or
    loses sight of its source. Lever: suppression only from round 2.
    **(change HR-20, HX-2; Herald design 10.4; recommended to the enemies
    owner; touches the settled "blocks Routed")**

  The positional play is to pull or shove members out of the banner's
  reach before breaking them.
- **Command** is the leader's single bonus-action ability on one shared
  **cooldown of 3**. It mirrors the party's Leadership Rally, which is one
  ability with two options:
  - **Rally:** Rallied on members of its own group in the aura,
    **including the leader itself**. It ends Demoralised and Routed
    **(settled)**, removes rallied routers' cost from losses, and blocks
    `fear` for its duration. That gives the leader an honest window.
    **The settled once-per-ally-per-fight limit binds** (ability model
    7.2; the "Rally used" state, conditions 17.2): a member it has already
    rallied this fight is not rallied again, so a re-broken group stays
    broken. **(settled; lead's call to apply it to monsters)**
  - **Order:** one ally gains Spurred (a free 2-square move).
- **Counters to Rally:** kill the leader; break the aura; **a Frightened
  leader cannot Rally** **(change C-9)**; a Shaken leader cannot Rally
  (Later; settled); Deafened members cannot be Rallied (Later; settled);
  wait out the visible cooldown.
- **The leader's own morale:** T1 does not apply to it. A Demoralised
  leader may still Rally, and that ends its own Demoralised.
- **AI value of Rally** = the restored units' expected damage per round ×
  the expected rounds left (at most 3), in HP-equivalents. Thrift applies.

### 14.5 Routed in practice

- **A fixed flee policy, not the scorer:** it Dashes toward a **retreat
  anchor** taken from generation's ranking for its fight host and the
  party's entry Portal (rooms reachable without crossing that Portal,
  through non-door Portals first; homeRooms of un-engaged groups
  excluded unless nothing else qualifies): the first anchor the party
  has not explored, else the farthest, else it is Cornered. Every non-elite
  fight host has a non-door flee exit (`fleeExit`) **(change FL-12; floor
  layout design 6.8)**. "Explored" here means entered; Chalked or
  Traced rooms never count (reading, XI-1; exploration and information
  design 4.1.1). It never enters a hazard (**pending request
  (TR-15, M3; room terrain design 6.6):** except a non-lethal Remnant,
  which it treats as passable; not yet ruled; **pending request (hazards
  design 6.12, M2 (gated), else M3):** it never paths adjacent to a Void
  and treats due squares as hazards; not yet ruled); it
  does **not** Disengage, so leaving the party's reach provokes an
  opportunity attack: a payoff without a chase.
- **Routed units make no opportunity attacks.** The party may move through
  a rout freely.
- **Cornered** means no reachable square increases its distance from the
  nearest visible party unit. Only then does it attack, using the scorer
  restricted to adjacent targets.
- **It throws down its notable item the moment it is Routed.** Breaking
  morale never costs loot. **(change C-8)**
- **If it ends a turn unseen, it leaves the fight and the floor**
  **(settled + change C-8)**. It never warns other groups, never joins a
  later fight and never returns healed. A visibly flagged "runner" role is
  a Later option; **a non-hostile runner, the Windfall Truant (7.1), is
  pulled forward to M3**, and the hostile Thief stays Later (17.1)
  **(change LP-3; loot sources design 4.7; agreed, Q1, Q2; M3)**.
- A Routed unit's intent read (13.3) returns `Fleeing`. **(lead's call)**

### 14.6 Surrender, Yield and the end of the fight

**Demand Surrender** (a new Intimidation use) **(change S-4)**:

- **Cost:** an action. The Dirge Herald's Parley makes it a bonus action
  for 1 Cadence, once per round (Herald design 9.1) **(change HR-19)**.
- **Roll:** Intimidation against 10 + Will.
- **Range:** within 2 squares; 4 for a Dirge Herald with Terms of
  Surrender and the Totem in its active set (Loom does not stack)
  **(change HR-19; Herald design 9.1)**.
- **Targets:** a Demoralised or Routed unit. Never a boss. An elite only
  when Routed.
- **Advantage** comes from Demoralised **(settled)**.
- **Success:** the target **Yields**.
- **Failure:** nothing changes, and a second attempt against that unit has
  disadvantage. The Frighten repeat rule does not apply.
- Restricting the targets keeps it from becoming a save-or-die check
  against healthy enemies.

| | Frighten → Routed | Demand Surrender → Yielded |
|---|---|---|
| Range | Frighten's range (6, line of sight) **(change HR-14; Herald design 18)** | 2 squares (4 with Terms of Surrender; HR-19) |
| Can be undone | A Rally undoes it | Permanent |
| Payoff | Opportunity attacks as it flees | Information (below) |

**Yielded** is a *disposition*, like dead, not a condition. There is one
disposition, reached two ways: Demand Surrender, or the end-of-fight rule.
A third route is either the Herald's Lay Down Arms or the universal Offer
Terms (HX-5, the enemies owner's decision; if adopted, Lay Down Arms is
cut), never both **(change HR-19; Herald design 8.3, 10.1)**.

- A yielded unit drops its notable item and salvage at once; a
  Keybearer also hands over the floor key **(change TL-23; traps and
  locks design 6.11; M2)**.
- It is removed as resolved, with its full experience share.
- **Interrogation is automatic: one floor fact per fight**, however many
  enemies yield. In priority order: the nearest undiscovered trap on the
  path to the stairs (the Stairhead; on floor 5, the main path to the
  arena; floor layout design 6.11; **change FL-6**), an undiscovered
  secret (a Hairline secret; floor layout design 9.2; **change FL-17**),
  the boss's Lore entry, one unexplored room's layout (an unmapped
  room: not entered and interior not Chalked; drawn as interior Chalked;
  readings, XI-1, XI-4; exploration and information design 4.1.1,
  4.3.6), the floor key:
  its carrier and its lock **(change TL-23; traps and locks design
  6.11; M2)**. **The player picks the category** from this
  list; the priority order is the default selection (Pick the Question,
  HX-6; Herald design 10.5) **(change HR-19)**. It is shown in the loot
  summary.
  The five categories are the shared fact family (F-TRAP, F-SECRET,
  F-BOSS, F-ROOM, F-KEY); Curiosities also use F-LISTEN and F-CACHE,
  which interrogation does not (events design 4.4.1) **(change EV-7;
  M2)**. Each fact is recorded as an instance (`{category, targetId}`)
  in the floor's **FactLedger**, one per-floor, in-run ledger shared
  with the elite intel line (15.8), Curiosities, Fragments, Parting
  Words and the Tithe Bowl; a category whose default target is already
  known falls through to the next unknown instance, and dedup never
  reads profile memory (the codex is write-only), so a seed gives the
  same facts to every player (events design 4.4.2) **(change EV-8;
  M2)**.
  **Pending request (XI-25, M3; exploration and information design
  4.6.4):** F-TREASURE joins these categories after F-KEY (default
  priority unchanged), choosable through Pick the Question, at most once
  per floor through Yield, inside the shared cap of 2; kill switch: if
  `YieldCategoryMix` shows F-TREASURE above 50% *(illustr.)* of picks,
  it is restricted to the optional elite; not yet ruled.
- There is no spare, rob or execute menu. A choice with no mechanical
  difference is noise, and a reward for executing would make mercy a trap.
  Curiosity options follow the same rule: no two share an effect set
  (`MechanicalDifference`; events design 4.3.1) **(change EV-4)**.
- Captives as next-camp Pledges, never mid-floor joins (Later): Meet at
  camp, or Send home for a Parting Word from the categories above
  **(change CMP-10; companions design 7.12)**.

**End of fight** **(change M-3)**: combat also ends, **as a win**, when
every hostile unit still in the fight is Routed.

- Routers still in sight Yield. Unseen ones are removed as Fled.
- Downed companions get up, as on any win. **(change M-1)**
- If the hero reaches 0 HP in the event that ends the fight, the run
  ends (death and healing design 10.3). **(change DH-30)**
- Simulator target: zero party movement after the last non-routed enemy
  falls.
- **A Windfall (7.1) never counts as a hostile**, so it never keeps
  combat alive. The combat-end check runs **after each Dream sub-step**,
  not only between Dream steps: combat ends the moment the last hostile
  resolves (a down, Rout or Yield included); if it ends before the
  Windfall hop sub-step (3b), 3b and 4 are skipped and a live Windfall in
  the fight's room is nabbed (15.8) **(change LP-5; loot sources design
  4.7.4; M3)**.

### 14.7 What the party gains from morale

- Full experience on resolution.
- The thrown-down loot.
- Fewer rounds, so fewer resources spent.
- One floor fact.
- Opportunity attacks on fleeing units.

Experience does not depend on how a group is resolved, so morale and
Intimidation never cost levels.

### 14.8 Out of combat: Cowed

**Who and how:**

- The **hero** rolls, because Presence is always the hero's **(settled)**.
- The roll is Intimidation against 10 + the group's best Will, as an
  exploration action, against a group the party can see.
- The attempt makes the group aware.
- **One attempt per group** **(settled: no free retries)**.
- The Herald's **Show of Force** (exploration, a tier-1 slot) gives
  advantage on one cow attempt against one eligible group this floor;
  never a retry, caps unchanged **(change HR-22; Herald design 8.2)**.
  Events grant no Cow-advantage Grace and never offer cow menus
  (events design 4.8). **Pending request (EV-13, M3 if ruled; events
  design 4.8):** a Herald-lit Curiosity option, "your Show of Force
  costs no slot this floor"; not yet ruled (with the Herald owner).

**Eligible groups:**

- every member is non-fearless and not `fear`-immune;
- no living elite or boss;
- **remaining cost** (the summed frozen `cost` of unresolved members) **at
  most 75% of the party's guard-post budget** at the group's homeRoom
  (`4 × remaining ≤ 3 × budget`), so every fresh patrol-band group
  qualifies (patrols fill to 60–70%); **or** the group carries the
  **`broken` flag**, which also gives advantage, and its remaining cost is
  at most 100% of that budget. **(lead's call for the 100% cap)**

**The `broken` flag** is a group flag, not a condition. It is set when the
group's leader died, or its losses crossed ½, in a fight the party then
broke off; or its leader was removed by a silent kill (12.2); or, by the
Cutthroat's Quiet Work, a non-minion member was removed by a silent kill,
or (only with the body-noticed rule, 12.2) the same party unit's second
silent kill against the group this floor (`silentKillsBy[partyUnit]`)
landed; never against a boss, a group with a living elite or boss
(evaluated after the kill), or a `fearless` group **(change SH-10; Shade
design 8.1)**. It persists for the floor. It is how "Demoralised", which
is `combatOnly`, carries into
exploration.

**Success:**

- the group steps aside, never joins fights by hearing, does not block
  short rests and leaves the cluster budget;
- it pays its unpaid experience shares at once;
- it **keeps its notable item**, so safety and loot are a real trade;
  a Keybearer's group hands over the floor key **(change TL-23; traps
  and locks design 6.11; M2)**. If the enemies owner refuses this
  outcome, Cow does not yield the key (traps and locks design 19.2).

**Failure:** combat starts, nobody is Surprised, and the group can never be
cowed again.

**Breaking Cowed** later **(settled: any harmful act breaks it)**:

- the attacked members become Shaken (Later; in M2 they start with no
  state);
- the fight pays no further experience, because the shares were already
  paid, but the notable item is now winnable.

So "cow now, come back for the sword" costs a full fight and is not an
exploit.

**Display:** once seen, the group banner shows a "cowable" glyph and the
target number. The preview gives the chance.

**Guarantee:** at least one cowable group per floor in M2. The Scavengers
template is built for it. Floor 1's guaranteed cowable group lies on the
main path before the boss room (classes design 8.6) **(change CL-10)**.

### 14.9 How morale states sit in the existing rules

**(agreed for the budget; change C-3, C-6)**

- **A separate morale-state budget**, presented honestly as a budget raise
  rather than relabelled as "engine states": M2 = 32 conditions + **4
  morale states** (Emboldened, Demoralised, Routed, Cowed), with a **hard
  cap of 4** on the class. They are enemy-only and shown as a frame tint or
  banner, not in the icon row.
- **Applied by the morale engine at the checkpoint, not by pipeline steps
  2–7.** Wards, resolve pips, Unyielding, Steeled, hard exclusivity and
  composure never see them.
  - The one exception is the Frighten itself in T5 and T6, which passes
    through the pipeline as normal, as Frightened; only at step 9 does the
    morale engine substitute Routed (14.3). A Warded target spends its
    ward on that Frighten and nothing lands. A `fear`-immune or Rallied
    target blocks it at step 2. Routed itself never enters steps 2–7.
- **Not in the hard tier.** They never count as a lost turn start. A
  Stunned unit that becomes Routed simply cannot move this turn.
- **Exempt from the in-fight-end rule** (conditions 8), extended from
  "Demoralised, Routed" to all four. **(change C-16)**
- Yielded and Fled are dispositions and the `broken` flag is a group flag;
  none of them counts toward any budget.

### 14.10 Display and AI

- Each member's frame strip is tinted by its morale state (5.6); its
  profile is shown on hover and inspect, not on the plate.
- Hovering the group shows a cost-weighted pip bar with its break line
  ("Breaks: leader falls, or 2 more pips") and the aura ring.
- **Party scorer** (suggestions and the simulator) gains a **morale term**.
  A unit an action would Rout or make Yield, cascades included, scores as
  if killed × 90% (tunable). It reads only what the party knows, and
  profiles and break lines are always visible.
- **Break-chain preview** (universal, hover only): any preview whose
  resolution causes loss events shows the FIFO cascade (14.3) from party
  knowledge only, with "aura drops" (Waver), "Fearless: Frightens only"
  and "Immune (known)"; unrevealed immunities show as "uncertain"
  **(change HR-37, HX-7; Herald design 10.3)**.
- **Morale overlay:** one family of enemy markers, at most one glint per
  unit, in priority rout-ready, faltering, the Frightened tether to its
  source; the Spurred trail shows only on its holder's turn; glints name
  no ability. The readability playtest gate counts glints per screen with
  four units **(change HR-37, CM-4; Herald design 7.14)**.

### 14.11 Rejected: morale in Later

Recorded for reference. Keeping morale in Later would have cut the morale
states, T1–T7, Demand Surrender, the end-of-fight rule, drop-on-rout,
Scavengers and the cowing guarantee, leaving Intimidation only Frighten for
the whole of M1 and M2: the stat model's "decoration skill" anti-goal.
The client chose M2. **(agreed)**

## 15. Enemy gear and loot

**(agreed: option C; details lead's call unless marked)**

### 15.1 The policy

| Gear | Becomes an item? | What the player sees |
|---|---|---|
| **Issue gear** (the weapon, armour and shield an enemy fights with) | **Never** | The model, plus the gear line on hover. It drives damage type, die, properties and armour category. On resolution, armed units pay their pre-assigned share of the floor's salvage coin (15.7; change LE-8). |
| **Notable items** (pre-placed from the floor budget) | **Yes** | A glint icon, "Carries: …" on hover. The enemy uses it. It drops on death, rout or yield, or can be stolen. |

Five rules prevent clutter **(agreed)**:

1. **Issue gear never drops.** It is part of the stat block and the model.
   At most it becomes one line of coin, paid from the floor's fixed
   salvage share (15.7; change LE-8).
2. **Notable items come from a fixed per-floor budget**, pre-rolled when
   the floor is generated. Enemies carry part of that budget, so killing
   more never gives more items.
3. **No junk by construction.** Every loot item has at least one property.
   Duplicates of gear the party already owns, or of a base already drawn
   for this floor, are rerolled, and each item family is capped per run
   (15.3 step 2).
4. **One loot summary per fight.** Everything gathers automatically, with
   comparisons against each party member's gear.
5. **No durability, wear or broken drops.**

Target: **1 item or fewer per loot summary on average.** Hard rule **(lead's
call)**: the loot plan assigns at most **3 notable items to any one
encounter** (carried, room containers and caches together). The boss's
reward card is a separate screen and does not count. It stays outside
the floor's notable count (Ledger A), but its taken option counts in the
floor's value (Ledger B) and toward family caps **(change LE-3; loot
economy design 6.1 item 2, 6.3; M2)**.

### 15.2 Who carries

- **Only types flagged `armed` have gear slots:** main hand, off hand or
  shield, armour, and one carried slot. Humanoids and armed constructs are
  armed. Beasts, oozes and natural-weapon users are not. Unarmed types
  attack with `natural.*` ability templates (weapons 17.10), rolled with
  Strength unless the template declares Dexterity. **(change W-5)**
- Monsters are **trained in their loadout weapons and armour**, and
  **Strength thresholds bind them as they bind the party** (checked at
  authoring).

| Rank | Issue gear | Notable item |
|---|---|---|
| Minion | Yes, if armed | Never |
| Standard | Yes | Only from the enemy-carried quota, and only when no elite or leader in the encounter can take it |
| Leader | Yes, with a banner, horn or staff prop | Second priority. The banner may *be* the item; a horn never is, because horns are reserved for joiners (the Kennel Houndmaster's horn included) **(change LP-21; loot sources design 5.3; M2)**. |
| Elite | Yes | First priority for the quota. **Guaranteed only in the optional elite room**, met by allocation: the elite-room carrier takes the floor's highest carrier-legal **gear** card (weapon or focus, armour or shield, trinket), never a consumable; a consumable top card goes to the elite room's lair cache **(change LE-10, LE-18; loot economy design 6.9 items 2, 6; M2)**. The elite's gear card is filtered to records whose lines show in use (`showsInUse`) when that set is non-empty, so the prize is presented in use **(change IR-12; item rolls design 4.12; M2)**. An unarmed elite there guards a **lair cache** instead ("Guards: cache"). Other elites without a quota item show nothing. Resolving the elite (killed, Yielded, Routed or Cowed) or stealing its notable, whichever comes first, gives the **elite intel line** (15.8) **(change LE-18; M2)**. |
| Boss | Yes, if armed | **Guaranteed** signature item, visibly carried by an armed boss, plus the boss reward card (17.5). An **unarmed** boss **guards** its signature in a **signature cache** in its lair (the "Guards: cache" precedent of the elite row; not a ranked socket, not the elite lair cache), and its plate reads **"Guards: <signature name>"**; M1: Old Ragged guards Bellwarden's Mail in its den **(agreed, Q5; change IR-13; item rolls design 4.13; M1)**. The signature is the boss's authored form for the top tier of the floor's drawn deck variant (one form per top tier its floors can need), with at most 2 base lines plus one authored value-1 line echoing the boss's fight or kit (≤ 3 lines; M1 records grandfathered); a boss that carries one form guards any second form, and "Carries: …" and "Guards: …" always name the chosen form; a draw-free second form in another lane is required for specialist chassis in M2 and every boss in M3; a Very rare signature consumes the floor's VR slot, pending the ITEMS owner **(change LE-3; loot economy design 6.3; change IR-13; item rolls design 4.13; M1 / M2 / M3)**. **Floor 5:** no reward card; the final boss still carries and uses its signature, which drops as a **Memento** (a run-record item card, 0 value, shown on the victory screen and run record only) **(agreed, Q1; change LE-5; loot economy design 6.11; M2)**; an unarmed final boss (none in M2) guards it in its cache and it drops as the Memento at victory **(change IR-13; item rolls design 4.13)**. |

**Heavy Purse (change LE-16; loot economy design 8.3; M2):** on floors
2–4, about 20% of the floor's found coin *(illustr.)* is carried as coin
only by one unit, bound in LootBind (10.5): the Keybearer, else the lair
leader, else the highest-level non-elite unit in the lair, else the
highest-level non-elite non-minion unit on the floor (ties by unitId);
**never the elite**; if no unit qualifies, its coin returns to the
found-coin lumps. It drops on resolve (killed, Yielded, Routed); a
Cowed carrier keeps it. Never on floors 1 or 5.

**Pending request (agreed, Q6; loot economy design 6.9 item 8):** if the
M2 simulator's `EliteNetWinDelta` misses −5 to +2, the elite room's
death cost (budget, watcher rules) and experience (16) are tuned here,
not loot; open until the M2 report.

**Pending request (LP-27, M2 if agreed; loot sources design 7):** the
Uncorked glint state on carriers of consumable notables (15.4); not yet
ruled.

### 15.3 Notable items

**Generation** (loot random stream **(settled)**; deterministic):

1. **The floor loot budget is a count of notables per channel.** Each
   entry's tier is fixed by the floor's spoils list; identity and
   properties come from the record. **(change LE-1; loot economy design
   6.1; M1)** The split is about 40% containers
   and secrets (secret caches count as containers; floor layout design
   9.2; **change FL-17**), 40% enemy-carried, 20% boss. Illustratively, 6–8 notables
   per floor, of which 2–3 are enemy-carried, plus 1–2 supply and a few
   sundries (Common consumables, outside the notable count, inside the
   fixed budget; from floor 2 at least 1 offensive flask; consumables
   design 10). Enemies carry *part of* this budget and never add to it.
   **(change M-5; change CN-19)** The tiers come from a fixed **rarity
   deck** per floor: a binding base variant and, where the depth gates
   allow, an alternate variant of exactly equal value, picked by one
   fixed draw; carried cards are 2 / 2 / 3 / 3 / 3 per floor
   *(illustr.)* within the per-encounter caps below **(change LE-2; loot
   economy design 6.3, 6.9 item 2; M1 for F1, M2)**. **M1's split** is its own: containers, at most 1
   enemy-carried weapon or armour notable, and the boss's signature item.
   **(lead's call)** **Floor 5:** the boss share is the **Viaticum**, a
   choose-1-of-3 offer at Rare (Rare wands and tier-3 scrolls, every
   option usable by the party snapshot), in a main-path Plain container
   after the lair, outside the Antechamber, Circuit rooms and fight
   hosts, else in the lair room; it counts 1 notable. The room is pending
   the FL and enemies owners' confirmation. **(agreed, Q1; change LE-5;
   loot economy design 6.11; M2)** *Reading recorded for this owner's
   confirmation (loot sources design 4.10, 13; LP-14; LE 18.2):* nothing
   lies between the lair and the Forecourt on floor 5, so the fallback is
   the real rule: the Viaticum is the lair room's card-bearing container,
   hosted on the Bedside Stand **(agreed, Q5; M2)**. The budget also holds a **`camp`
   channel** (**2** notables at every camp 1–4, inside the approved
   1–2; graded for floor N+1, never Very rare; *illustr.*; **change
   LE-6; loot economy design 6.3; M2**) for the next camp's notables shop, drawn whether
   or not the shop is chosen. No camp channel follows floor 5 **(change
   LE-5)**. Camp stock counts toward later floors'
   family caps only once bought. **(change I-10; items design 10.4)**
   **Later (change I-14):** a `trader` channel, carved from the
   containers and enemy-carried shares (10.5 step 3). Unbought trader
   stock counts toward later floors' family caps only once bought or
   bartered (items design 10.9).
   **Windfall (change LP-1; loot sources design 4.7.6; agreed, Q1;
   M3):** a card moved to a Windfall (7.1) stays tallied in its origin
   channel, tagged `onWindfall`; the split above is unchanged.
2. **Draws** (10.5 step 3): weighted toward the party's `requires` tags at
   generation ("smart loot"); a base the party already holds at equal or
   better quality (quality = rarity tier, then value; **change I-12**;
   items design 12.5), or a base already drawn for this floor, is rerolled;
   each item family is capped per run. **(agreed)** "Base" reads as
   **base ?? family** (trinkets, scrolls, wands and notable flasks dedupe
   by family) (item rolls design 4.7 item 2).
   **Memory (change IR-6; item rolls design 4.7; agreed, Q2; M2, Rare
   staged):** a gear or trinket record generated by any memory-writing
   event (real or dreamed, offer options and both dual identities
   included; ghosts and signatures excepted) excludes itself and its
   Scion ancestors for the rest of the run (Scion-line-once; read from
   `LootLedger.generated[]`, never from player actions; C and U from M2,
   Rare from the `loot.memory.scionLineOnce.R` switch). A record flagged
   `oncePerRun` (explicit in data) is generated at most once per run in
   any form, and never as the fourth option or either identity of a
   dual-identity cache. A boss signature writes no floor key, no memory
   and no `generated[]` row (`bossContent[]`), is exempt from the
   run-wide owned-base reroll, and is family-cap-exempt when taken (then
   counts toward the caps). Consumables, scrolls and wands are outside
   Scion-line memory (family caps govern).
   **Termination (lead's
   call):** at most 8 rerolls per notable, each excluding capped families,
   owned bases and this floor's bases from the weight table (met by
   filtering before the identity draw, with padding: the same distribution
   at a fixed count; item rolls design 4.1). If the table is empty, the
   socket first relaxes its own target preferences in their listed order,
   then, draw-free, logged and only as its socket kind allows, the rungs:
   lift Scion-line memory (never `oncePerRun`); category re-weight at the
   tier; the tier-stable path (Very rare → Rare + fit premium, Very rare
   cards only); and last a supply or tonic draw, substituted
   and flagged `substituted`, so the per-channel count still balances.
   This floor's bases are never lifted, and the tier never changes
   silently **(change IR-7; item rolls design 4.7 item 8; M2)**. A
   guaranteed draw is never substituted (death and healing design 12.5).
   **(change DH-27)** Offer options use their own terminal ladder (item
   rolls design 4.8).
   **Later (change I-14):** a trader slot is never substituted; it returns
   to the containers channel (items design 10.9).
   **Weights (change LE-17; loot economy design 6.2; M2):** a legal
   record's weight is its category multiplier for the floor
   (`loot.categoryWeight[f]`) × the `requires` bias (3; 4 for the boss
   card's options) × the companion lever (1 by default), normalised per
   lane, then per family, then per record, each divisor counted on the
   static legal set at the tier, factor by factor in a fixed order with a
   FloorDiv after each **(change IR-3; item rolls design 4.2; M2)**; the
   lane is the record's `lootLane` (item rolls design 4.3). Filters are not weights: the Dream's Due, a fit
   premium and the per-run arms floor restrict the candidates first.
   **Usability (change IR-2; item rolls design 4.3; agreed, Q1; M2):**
   one `UsableBy` query, read per socket kind (`inClass` or `arcanaRead`
   for scrolls); a **hard** floor (no record no living member can use) on
   chosen and guaranteed draws (the Due, fit premiums, the arms floor,
   every offer option and the fourth, the Viaticum, stepped and Wax-Key
   identities, camp entries) and on Very rare cards; a **soft** ×100‰
   *(illustr.)* on Common, Uncommon and Rare field cards (containers and
   carried). Targeted sockets (the Due, fit premiums, an offer's Yours and
   Theirs slots) apply the `requires` bias against their target alone.
   **Element Echo (change IR-19; item rolls design 4.4; agreed, Q4: held
   for an M3 client decision):** default off (`loot.elementEcho =
   false`); identities stay theme-blind.
   **The Dream's Due (change LE-13; loot economy design 6.6, 6.10 item
   3; M1 for the F1 hero arms, M2):** the floor's first container-channel
   slot is constrained to one announced upgrade for a fixed target (F1,
   F3, F5 the hero; F2, F4 the member with the largest gear deficit). Its
   approved parent draw is still made as a **ghost draw** at the Due's
   tier with the ordinary weight table, then discarded and never counted
   toward caps or same-floor bases; the Due's identity is drawn on
   `lootPlan`, and later items treat it as this floor's base. The F2
   arms-floor card is drawn the same way **(change LE-17)**.
   **Offer sets (change LE-4; loot economy design 6.3; M2):** a
   choose-1-of-N offer (the boss reward card, the Viaticum and others)
   counts once; its unchosen options are dreamed options, pre-rolled,
   outside value and family caps, and destroyed when the choice resolves
   (`ItemDestroyed{dreamedAway}`).
   **The Reprieve Phial is never a weighted draw.** One pre-roll per run,
   on the loot stream, decides whether it appears, its floor and its
   channel; it replaces one notable draw of that floor and channel (a
   `campReward` phial takes a container draw of that floor). A declined
   offer is lost for the run (consumables design 14.7). **(change CN-27;
   DH-27)**
3. **Carrier rooms are reserved before composition** (10.5 step 4). That
   room is filled against its budget minus the item's cost add, so the
   item makes its carrier more dangerous without breaking the budget (10.5
   step 6). **Cost add = item value × k encounter points per offer-value
   unit.** The items design supplies k. A boss signature item carries no
   cost add; the boss guards validate it. **(change I-9)**
   **Later (change IR-20; item rolls design 4.15):** a cursed carried
   item's cost add uses its benefit's T+1 value.
   **Pending (item rolls design 4.12, 14.2; M2):** if `EliteKpcDelta`
   exceeds about 10% *(illustr.)*, a per-line monster cost-add multiplier
   for reaction lines engages (lead and enemies owner); open until the M2
   report.
4. **Assignment:** the first legal carrier in the order boss, elite,
   leader, standard. Legal means `usableBy` matches and the fully composed
   unit, item included, passes every budget, the threat band (6.5)
   included. For a notable armour or shield, legal also means: the
   carrier's worn issue armour has the same `armourCategory` (natural
   armour never matches) or the same shield kind; its derived AC is
   unchanged; its rolled properties pass the role-answer rule; reaction
   properties fit the 9.1 caps (armour design 12.6). **(change AR-26)**
   For a notable trinket, legal also means: with its rolled properties in
   place it passes the 6.4 guards; its output conditions fit 9.2; a
   reaction-shaped line is the unit's one reaction beyond the opportunity
   attack and a button is its bonus-action ability (9.1); its role answers
   pass; no suppression conflict with the issue gear; Wardstone and
   Ward-Knot never with the Warded or Unbreakable affix; its `carrier`
   limits hold (trinkets design 12). Carried wards and ally preventions
   count toward the encounter's control defence. **(change TK-23)**
   Otherwise the next candidate is tried (10.5 step 8); if none
   is legal, the item goes to a container in the same room. If the cost
   add is above 25% *(illustr.)* of the room's budget, the item also goes
   to a container in that room. **(change I-9; items design 12.7)**
   **KinSwap (change IR-12; item rolls design 4.12; M2):** after
   identities exist, the draw-free LootBind pass may swap the carriers of
   two carried cards of one encounter that share rank and tier (one pass,
   pairs by socketId), only when the swap strictly raises their summed
   kin score (issue family 3, shield kind 2, armour category 1) and both
   stay carrier-legal; logged `KinSwap`, identities unchanged. A rolled
   line the final carrier rejects is replaced, draw-free, by the
   identity's pre-picked alternate line, else the item goes to a
   same-room container.
5. **Locked in.** Item, properties and charges are fixed now (the rolled
   line, its element and LootPlan wand charges come from the identity's
   `lineRoll` entry; change IR-14; item rolls design 4.17; M1 reserved,
   M2). Kill order,
   save and resume, and the method of winning can never change what drops.

**Caps:**

- at most 1 enemy-carried notable per encounter on floors 1–2, 2 deeper;
- at most 1 carried consumable the enemy can use per encounter;
- **floor 1 consumables are self-targeting only:** a healing potion or a
  tonic, never a bomb or a party-targeting wand.

### 15.4 Carriers use what they carry

**(agreed)** Each item declares a `monsterProfile`. **(change CN-20,
client-approved 2026-11-17; consumables design 15)**

| Item kind | On the carrier | AI use | What the party gets |
|---|---|---|---|
| Rider or property weapon | Its weapon at-will gains the property or rider flag. A property's pool maps to uses for the monster's life: `rest` N and `camp` N become `uses:N`; `never` and `fight` become `uses:1` **(change W-5; weapons 15.2)** | Normal | The weapon |
| Notable armour or shield | Its effect applies, inside the threat band | Passive | The armour |
| Healing potion | `uses:1` bonus-action self-heal; heals `monsterPotionPermille` of max HP | Only at or below 50% HP | Nothing if drunk |
| Tonic | `uses:1` cure of its own worst condition | When it holds a major condition | The tonic if unused |
| Bomb or flask (floor 2+) | `uses:1` thrown area **or a single party unit**; bombs opener-flagged; setup flasks by `useWhen` | Normal | The bomb if unused |
| Wand | `uses:N`, with charges pre-rolled | With thrift | The wand with its remaining charges, refilled at camp **(settled `camp` tag)**; *Later:* a wandering trader's paid Recharge also resets one wand's `camp` pool, once per trader (change I-16; items design 10.9) |
| Scroll | `uses:1`, caster-tagged carriers only; the carried spell passes the 6.4 guards on the reference carrier | Normal | The scroll if unused |

- **What is used is gone.** That creates a kill-the-carrier-first choice,
  the DCSS lesson.
- **Using an item reveals it**, under the settled "seen in play" rule.
  Arcana identifies the rest after the fight.
- The AI never values an item as loot. It drinks the potion because the
  scorer says so, never to deny it to the party.

**Pending request (LP-27, M2 if agreed; loot sources design 7):**
**Uncorked.** A carried consumable notable gets a data use threshold
(such as the HP fraction at which a self-heal fires); while the carrier
is at or below it, its existing glint shows an "uncorked" shape
sub-state (the glint glyph with an open stopper notch, never a hue
change). Hover: "Uncorked: may use its <tier word> <noun>". A state
read, not a new mark and not an intent read; it counts in the
glints-per-screen gate (14.10). Not yet ruled.

### 15.5 Getting items from living enemies

**Steal** (Stealth's settled use, defined here; M2) **(agreed; change
S-5)**:

- **When:** the carrier is Dormant, Stationary or Patrolling (never
  Alerted, Engaged, Pursuing, Searching or Wary), the thief is adjacent,
  and **no other awake member of the carrier's group has line of sight to
  the thief.** Any party member may attempt it.
- **Cost:** one exploration action.
- **Roll:** the thief's Stealth (d20 + Dexterity + proficiency if trained)
  against the carrier's passive perception (10 + Wisdom + proficiency if
  trained, **−5 if Dormant, +5 if Keen**). **(lead's call for Keen)**
- **An awake carrier** (Stationary or Patrolling) imposes disadvantage on
  the roll. **(lead's call)**
- **Noticing:** moving adjacent uses the normal band triggers (12.2); the
  Steal itself causes no extra noticing roll.
- **What is taken (change LE-16; loot economy design 8.3, 6.15; M2):** a
  successful Steal takes **one** of {notable, purse, key}, the thief's
  choice; the action's hover previews the carrier's stealable items as
  rows (notable, purse with its amount, key). A stolen purse forfeits
  that unit's salvage share (15.7).
- **Success:** the carrier's awareness is unchanged. The item moves to the
  thief's free slot, or to the leftovers marker if none is free; the
  carrier's `linkedAbilityId` is removed from its kit; its frozen `cost`
  and the group's experience are unchanged.
- **Failure:** combat starts, nobody is Surprised, and the noise counts as
  fight noise.
- **One try per carrier per floor**, recorded as a `stealAttempted` flag on
  the carrier.
- **The floor key (change TL-23; M2; traps and locks design 6.11):** a
  Keybearer's floor key can be taken by Steal under these rules, with
  the same one try per carrier (one of the three choices above; change
  LE-16).
- **The elite room:** the carrier is never Dormant, so Steal has
  disadvantage, and the Champion's watcher (17.4) sees every square
  adjacent to it. Stealing there takes a Stealth play first, such as
  luring the watcher away with noise. Target: under 15% of elite-room
  notables are taken without a fight.
- **Steal on a Dormant Truant (change LP-6; loot sources design 4.7.8;
  agreed, Q1; M3):** the Windfall (7.1) can be stolen from while Dormant.
  Adjacent, and no awake un-Cowed hostile in its room has line of sight
  to the thief. Stealth against its fixed passive **20, −5 Dormant = 15**
  *(illustr.)*, a fixed row, not Keen; Light Fingers gives advantage.
  The roll is the keyed hash `H(floorSeed, windfallId, "windfallSteal",
  0, die)` on `checks`, never the combat stream. **The room's one Steal
  try:** the party steals either a carrier in the host group or the
  Truant, not both (`stealTryUsed` on the host group, set and checked by
  both paths). Success takes **the card only**; its coin spills as a
  dropped pile, "guarded" while the host group is unresolved, then a
  1-tick Take or auto-gathered at fight end; the Truant leaves.
  Failure starts the fight as any failed Steal (nobody Surprised) and the
  Truant wakes at its starting count after the live-party-size check.
  The elite-room metric above excludes it.

**Disarm seizure** (Later, with Disarmed) **(change C-17)**:

- A dropped *issue* weapon stays issue gear, as settled. A party unit
  standing on its square blocks recovery once the owner has moved off it,
  so an owner still on its own weapon can pick it up. **(change W-5;
  weapons 16)**
- A dropped *notable* weapon may be seized with the free interaction and
  becomes party loot for good. The owner falls back to its `improvised`
  at-will (same family, −30% damage, no rider).

**Enemies never pick up party items or any dropped item**, except their own
Disarmed weapon. No loot ping-pong, and an heirloom can never walk off the
floor.

### 15.6 Drops

**(agreed: drop on death, rout or yield)**

- **Killed:** drops in its square, or on the nearest safe square if that
  square is a hazard or impassable. Safe means passable and not a hazard
  square. Nearest is by grid distance, then a fixed neighbour order (N,
  NE, E, SE, S, SW, W, NW), then square index. **(lead's call)** **Loot
  is never destroyed** by terrain, damage type or kill method, nor
  container contents by traps, locks or force (traps and locks design
  6.13; TL-27). **Sole exception (change LP-2; loot sources design
  4.7.7; agreed, Q1; M3):** a Windfall's card and coin leave the floor
  when its shown count ends (`ItemDestroyed{gotAway}`); kill method,
  damage, terrain, traps, locks and force still never destroy loot. A monster
  removed by a Void fall drops its carried loot on the last safe square;
  summons and Figments drop none **(change HZ-10; hazards design 6.4; M2
  (gated), else M3)**.
- **Routed:** thrown down the moment it routs. Unused uses are lost from
  the carrier's kit.
- **Yielded:** drops at once.
- **Cowed:** kept (the floor key excepted, 14.8; change TL-23).
- **Party flees:** the carrier keeps any unused item. Items already on the
  ground stay as a pile marker. A live Windfall (7.1) gets away at once
  with its bundle (`WindfallGotAway{cause: partyFled}`) **(change LP-5;
  loot sources design 4.7.4, 4.7.7; M3)**.

### 15.7 Salvage coin

- Salvage is a **12% (120‰) share of the floor's fixed coin pool**,
  pre-assigned at generation (LootBind, 10.5) to the floor's armed units
  that are not summoned or joining later: each encounter group with an
  armed unit gets 1 coin, in main-path order then by groupId, while the
  share lasts; the rest is split over those groups by Σ armed level, then
  inside a group over its armed units by level (largest remainder, ties
  by id). Each resolved **armed** unit (killed, Yielded or Routed) pays
  its share on the fight's coin line. Cowed groups pay none, and their
  share is lost. Killing more never pays more than the pool. **(agreed,
  Q3; change LE-8; loot economy design 6.4 item 2; M1 for the F1 share,
  M2)**
- The coin pool is per party *(illustr.)*: **35 / 50 / 70 / 90 / 0** per
  floor at `companionCap` 3 and 35 / 50 / 70 / 75 / 0 at cap 2, keyed by
  the cap, never by the actual party. Salvage = `FloorDiv(pool × 120,
  1000)` (4 / 6 / 8 / 10 / 0); the rest is found coin in containers and
  caches. Salvage is therefore about 10–15% of a floor's coin income by
  construction: flavour and a
  small "you won" signal. A floor's coin income means found coin; item
  Sell and Scrap income is reported separately. **(change I-11; items
  design 10.2; agreed, Q4; change LE-7; loot economy design 6.4 items 1,
  3; M1 for the F1 pool, M2)**
- Coin exists (items design 10.1); salvage is its pre-assigned share of
  the pool. The loot summary's coin line shows the fight's salvage total
  with one per-unit line for each unit whose share is non-zero; a fight
  whose total is 0 shows no salvage line. **(change LE-8)**
- **Floor 5:** pool and salvage 0; the coin and salvage lines are hidden
  rather than shown at 0. **(agreed, Q1; change LE-5; loot economy design
  6.4 item 5, 6.11)**

### 15.8 The loot summary: one per fight

**(agreed: one per fight)**

- **When:** at combat end, after banked level-ups resolve **(settled)**, so
  comparisons use the new levels.
- **Gathering:** every item dropped in the fight is gathered automatically,
  wherever it fell, with the coin line, the experience line with its level
  scaling (16) and any floor fact. The floor-fact slot also carries the
  **elite intel line** when it fires (15.2): one non-power fact about that
  floor's boss (its signature item's card), kept afterwards under the
  boss name on the pause screen's floor-info panel; the line takes the
  next unknown instance from the floor's boss-fact queue (`bossQueue`)
  in the shared FactLedger (14.6), so it never repeats a fact a
  Curiosity already gave; F-BOSS is run-specific where segment 9
  exposes seeded boss variables. **(change LE-18; loot economy design
  6.9 item 6, 6.15; M2) (change EV-7, EV-8; events design 4.4.1,
  4.4.2; M2)**
  A live Windfall (7.1) in the fight's room at combat end is nabbed, and
  its card and coin are gathered with the rest **(change LP-5; loot
  sources design 4.7.4, 4.7.5; M3)**.
- **Each entry shows:** name and family; known properties, with an
  "unidentified" mark if any remain; **core-computed comparisons against
  every party member** (AC change, expected damage change, proficiency
  fit, Strength threshold and slot cost).
- **Actions:** Take (choose who), Leave, Take all. Take defaults to a
  core-suggested recipient, and pack placement is party-wide. When slots
  are full, a **swap** sends the displaced item to the leftovers. **This
  is the only place in the dungeon where found items are sorted into
  packs; mid-fight pickups go only to a free belt slot, an empty spare set
  or `claimed`, and purchases at camp or a trader use the same Take
  placement. Out of combat the party view rearranges weapon sets and belts
  at any time, and armour and trinkets at a short rest or camp.**
  **(change I-8; items design 6, 8)**
- **Leftovers** become one fixed map marker per room, scrapped at the
  stairs. Clicking it reopens the same screen. **(change I-8; items
  design 9)** Leftovers and flee piles are map auto-marks, and the
  floor-info panel is the Floor-info panel (exploration and information
  design 4.3.3, 4.9.9; XI-7).
- **Mid-fight pickups:** a unit in or adjacent to a dropped item's square
  may pick it up with the free interaction. Wielding a picked-up weapon
  costs the settled bonus action.
- **Waves:** a basic summary (coin line and items, take or leave) in M1;
  comparisons and swap in M2.
- **Friction targets** (simulator and playtest): mean summary size of 1
  item or fewer, never more than 3; no junk by construction; no durability
  or weights (the settled slots are the only constraint); about 10 seconds
  median per summary.

### 15.9 The items-design interface

**The items design must provide:**

- Weapon records, including multi-die records (for the one-die
  conversion), with properties and rider flags.
- Armour records whose category values work with monster Dexterity and land
  in the table bands.
- Strength thresholds.
- An `improvised` attack per weapon family.
- For every family a monster can wield: `monsterProfile` (effects when
  monster-wielded, ability shape, `useWhen`, `minFloor`), `usableBy`,
  `family`, `requires` and `identifyDifficulty`.
- The **value-to-encounter-cost conversion k**.

**Decided here, binding on the items design:**

- **Mundane base items are not loot.** They come only from shops and
  starting kits; every loot item has at least one property. **(lead's
  call, from option C's "no junk")**
- No durability, wear or weights. **(agreed)**
- Family caps per run and duplicate rerolls. **(agreed)**

**Left for the items design to decide:**

**Answered (change I-15):** the generic layer is no longer pending; see
the items and inventory design (items design 16). What remains open moves
to the category designs (items design 17): weapons, armour values and
Strength thresholds, starting kits, tonics, Remedy and flasks, scrolls and
wands, Coated, Disarmed, Clinging and Pitted Armour. Weapons and foci are
since settled by the weapons and foci design (2026-11-17); Disarmed and
Coated stay Later **(change W-15)**. Armour values, Strength thresholds
and Pitted Armour (Later) are since settled by the armour and shields
design (2026-11-17) **(change AR-29)**. Consumables, scrolls and wands
are since settled by the consumables design (2026-11-17); coatings and
Coated stay Later **(change CN-26)**. The original list:

- Slots: stacks of tonics and potions, and how supply uses slots
  **(settled as open)**.
- Coin: whether it exists, salvage values, prices (Leadership), a camp
  stash or selling.
- Rarity tiers. Recommendation: avoid flat +N items, which are dead numbers
  under the +5 cap.
- The identification flow.
- Cursed carried items (Clinging): can a carrier hold one, and is it
  marked?
- Oils and Coated; Disarmed in full; Pitted Armour.
- The loot summary layout.
- Theme materials for unarmed enemies (Later, optional).

## 16. Experience

**(lead's call)** The parents leave the source open.

- **Experience on resolution:** a member pays out when it is killed,
  Routed and fled, Yielded or Cowed. In M1, only kills exist. A member
  removed by a Void fall pays out as killed (the `voidFall` trigger;
  **change HZ-10; hazards design 6.4; M2 (gated), else M3**).
- Sneaking past pays nothing, but the group's experience stays available.
  Monsters never respawn, so experience is finite and cannot be ground.

**Group experience** is fixed at generation as the band's nominal value:

- patrol 70, guard post 100, lair 140, elite room 160, boss 200 (escort and
  reserves included);
- a **Mousetrap** Ambush pays its band's value even though it is filled
  at 85% **(change EP-10; encounter placement design 6.8.1; M2)**;
- joiners keep their own value;
- **M3:** the Annex group (10.4) pays 0 experience; its reward is the
  Wager prize, the Dare cache or the box contents, so the floor pool and
  the 20.1 check are unchanged **(change EP-26; client agreed
  2026-11-21, Q2; encounter placement design 6.13)**;
- Curiosities and Interludes pay 0 experience, on the Annex precedent,
  so the floor pool and the 20.1 check are unchanged (events design
  4.4.8) **(change EV-7; M2)**;
- **on a trimmed floor** (`pacing.trimmedFloors[]`, lever 3, applied only
  in the lever order of companions 7.4): a trimmed floor 4 pays boss 300,
  a trimmed floor 5 pays lair 240 (boss 200); a trimmed floor 3 (when
  the list is [3, 4, 5]) is paid as floor 4. The 10.1 budget bands (boss
  200, lair 140) are unchanged **(change RT-7; run structure design 6.6,
  10.2)**.

It is split into **per-member shares** by frozen `cost` (10.1): integer
arithmetic, with the remainder going to the highest-cost member, then unit
order.

**Each share is paid once**, at the combat end or cow that resolves that
member. A fight the party breaks off pays for the members already resolved.
A later fight or cow pays only the unpaid shares.

**Level scaling** (the PF2e shape, self-correcting):

- each share × (100 − 25 × (hero level − the expected level of the
  group's homeRoom)) %, computed in tenths and clamped to 50–150%, exactly
  as in 3.1;
- a party ahead of the curve earns less, and one behind earns more;
- the loot summary shows the adjustment.

**Curve:** levels 2–9 cost 270 each; level 10 costs 540, because floor 5
covers one level.

| Floor pool check | Experience | Share of the 540 a floor needs |
|---|---|---|
| Baseline: patrol + 3 guard posts + lair + boss | 710 | 131% |
| One guard post bypassed | 610 | 113% |
| Plus the optional elite room | 870 | 161%, which level scaling damps |
| Trimmed floor 4: 2 guard posts, boss 300 **(change RT-7)** | 710; 610 after one bypass | 131%; 113% |
| Trimmed floor 5: 2 guard posts, lair 240 **(change RT-7)** | 510 before the final boss; 410 after one bypass | Only experience earned before the final boss counts |

The 110–135% validation check (20.1) runs on the pool without the optional
elite room, which is extra by design. **(lead's call)**

**Pending request (FL-21 (d), M2; floor layout design 6.8):** a party
that takes the Bypass **and** times the patrol reaches 540 counting the
boss (100%), below the 20.1 band, and Den and Sleeper lairs can be
sneaked past in-room. Options for this owner: drop "Stealth-bypass
candidate" from the lair templates, re-read 20.1 as "one guard post or
one Dormant lair", or rely on the camp catch-up, measured by
`MultiBypassRate`. Not yet ruled.
**Pending request (EP-23, M2; encounter placement design 6.10):** the
**Sleeping Ward sneak line** answers it: Den, Sleeper and Dormant Pack,
Gauntlet and Warband lairs seat sleepers beside the lair's Clearway, so
every route through passes within 3 of at least one sleeper (two band
rolls at Dormant −5); `LairSneakRate` is reported, and above 25%
*(illustr.)* this owner re-reads 20.1 as "one guard post or one Dormant
lair" skippable; not yet ruled.

- **Camp catch-up:** at camp the hero is raised to at least the next
  floor's entry level, and companions with it, before the camp restore
  and the Muster (companions design 7.11) **(change CMP-17)**. Target: this fires in under 20% of runs at camps 2–4
  **(change RT-8)**; the floor-1 Antechamber Catch-up raises the hero to level 3 before the floor-1 boss
  **(change RT-8; run structure design 9.4)**.
- **Banking:** experience is banked and resolved at combat end
  **(settled)**.

## 17. Catalogue

**(lead's call; all numbers illustrative)**

### 17.1 Roles

| Role | HP / damage / AC / attack | At-will 1 + at-will 2 (verb) | Signature | Party's answer | Profile | Guile adj. | Wave |
|---|---|---|---|---|---|---|---|
| **Brute** | 130% / 120% / −2 / −1 | Heavy or two-handed weapon + Shove **(change W-5)** | **Charge** (cooldown 3: a straight-line move, then its at-will with advantage; from floor 2 a type may instead add +1 die, guards permitting; 6.4). Conditions under 9.2, at most 2, Shove's Prone included: the M1 brute is Bleeding claw + Shove (Prone); an M2 brute keeps Shove plus one of Grapple (Grappled) or Staggered on its limited ability, never both, and takes Bleeding only if it drops Shove. Earliest room level by guard. At most 2 per floor 1 encounter. | Kite it, use opportunity-attack zones, Prone it, punish its low AC and Will | Charger | −2 | M1 |
| **Soldier** | 100% / 90% / +2 / 0 | Reach weapon or shield + Shove | **Interpose** reaction (`ifItChangesOutcome`: +2 AC for an adjacent ally against one attack), **or** a Guard-verb weapon (Parry, Set) instead, never both **(change W-5)**. Challenge only as an elite (Bodyguard) **(settled source)**. At most 1 per floor 1 encounter. | Go around it (slip past), Shove it out of the doorway, piercing or acid, kill what it guards | Holder | 0 | M1 |
| **Artillery** | 70% / 110% / −1 / 0 | Ranged (8–12; 8–14 from floor 2, **change W-5**) + weak melee | M2: a limited area on cooldown 3 (burst 1 or line 4), not selective; elemental riders. Never hard control. | Close distance, break line of sight, spread out, dive past the soldier | Kiter | 0 | M1 |
| **Minion** | Rank rule | Attack + a second attack or Help | Packs share one initiative slot; at most 4 per pack (2 in a solo encounter) | Areas, cleaves, chokepoints | Mob | −2 | M1 (template) |
| **Skirmisher** | 80% / 100% / +1 / 0; speed 8 | Melee + thrown | Slips past; Disengage as its bonus action; M2 Help sets up allies (Helped) | Reach, opportunity-attack zones, Grapple, Chilled, close formation | Raider | 0 | M2 |
| **Controller** | 90% / 70% / 0 / 0; concentrates | Damage + a minor (Chilled, Poisoned or Corroded) | One limited control ability. Hard control only where the gates allow. Carries the type's 2 conditions. At most 1 per encounter on floors 1–2. | Focus it, psychic rider, Treat, Rally | Disruptor | +2 | M2 |
| **Leader** | 110% / 80% / 0 / 0; trained Will and Intimidation | Weapon + Frighten | **Command** (Rally or Order, cooldown 3); Emboldened aura; Quarry | Kill it first; Frighten the Demoralised; Frighten the leader to stop its Rally | Commander | +2 | M2 |
| **Lurker** | 80% / 100% (150% on its opener) / 0 / 0 | Melee + Hide | Starts Hidden; **Ambush Strike** (`uses:1`, +2 dice); Poisoned, Bleeding or Blinded. Never in floor 1's main-path rooms 0–2 **(change DH-28)**. On floor 1 it never uses Ambush Strike: it opens with its at-will at 150%, stated plainly on its card **(change EP-11; encounter placement design 6.17; M2)**. **M3:** a theme's lurker may be placed as a **boxed lurker** (`containerForm`, F2–5, at most 1 per floor): no placement Stealth roll; marked when party passive Perception ≥ 10 + its proficiency, checked with trap detection **(change TL-28; traps and locks design 6.14)**. | Perception, the radiant rider (Revealed), staying grouped, reading the tell | Ambusher | +2 | M2 |
| Support | — | — | One `uses:1` heal or buff; at most 1 per encounter | Withered, focus | — | — | Later |
| Summoner | — | — | Calls a pre-paid reserved pack, with a visible countdown | Kill it before the countdown ends | — | — | Later |
| Thief | — | — | Steals and flees; must be visibly flagged and drop its haul when Routed. Stays Later; the non-hostile Windfall (7.1, M3) is not a Thief (loot sources design 4.7.1; LP-3, read) | — | — | — | Later |

**Swarm units are cut.** They would need a new resolution rule. A minion
pack sharing one initiative slot gives the same feel.

### 17.2 Elite affixes

Affixes are always visible. The first affix is priced into ×2; a second
affix costs ×1.2. **Gentle** means legal on floor 1.

| Group | Affix | Effect | Floor | Roles | Wave |
|---|---|---|---|---|---|
| Defence | **Warded** (gentle) | Warded with 1 charge (2 on floors 4–5), `oncePerLife` | 1+ | Any | M2 |
| Defence | **Plated** (gentle) | Armour counts as **heavy** for category-reading rules only (so Corroded's +1, the piercing rider and the stealth penalty apply), +2 AC as the `traits` term of the AC query, never part of the armour value (outside the ±1 base band, governed by the threat band: 5.2, 6.5), speed −1 as the heavy speed penalty, paid once (armour design 6.1, 7.3) **(change AR-27)** | 1+ | Brute, soldier, leader | M2 |
| Defence | **Hardened** | One theme layer (the theme's `hardenedLayer`, whose shown breaker is its `counterType`; **change RT-4**) with its breaker shown. Inside the 2-resistance budget, with a weakness alongside. | 2+ | Any | M2 |
| Defence | Blurred | 3 charges, `oncePerLife` | 3+ | Skirmisher, lurker, controller | Later |
| Mobility | **Swift** (gentle) | Speed +2 | 1+ | Brute, skirmisher, lurker | M2 |
| Mobility | **Slippery** | Immune to `restraint` (its 1 control immunity) | 2+ | Skirmisher, lurker | M2 |
| Mobility | **Pouncer** | Bonus action, cooldown 3: Jumps up to its speed and must end adjacent to an enemy. On a skirmisher it replaces the bonus-action Disengage. | 2+ | Brute, skirmisher | M2 |
| Sustain | **Vampiric** | On a melee at-will hit, regains half the damage. Withered counters it. | 2+ | Brute, skirmisher | M2 |
| Sustain | Relentless | Death-warded, `oncePerLife` | 2+ | Not minions | Later |
| Sustain | Regenerating | Regains 10% of max HP at turn start; Withered suppresses it | 3+ | Brute, soldier | Later |
| Punish | **Volatile** | Death burst, radius 1, theme damage (the theme's `themeDamage` = its `primaryDamageType`; **change RT-4**), Reflex for half. The kill preview shows it. Counted in the guards with its turn maximum (6.4). | 2+ | Brute, leader | M2 |
| Punish | Thorned | Retaliating | 3+ | Soldier, brute | Later |
| Punish | Riposting | Riposting while its reaction is unused | 3+ | Soldier, skirmisher | Later |
| Command | **Commanding** | Becomes its group's leader: Command (cooldown 3) and the Emboldened aura. Never in a group that already has a leader (8.3). | 2+ | Soldier, controller | M2 |
| Command | **Hunter** | Declares Quarry | 2+ | Skirmisher, artillery, leader | M2 |
| Command | **Horn-bearer** | `uses:1` at or below half HP: the nearest unaware group in hearing becomes Alerted and joins, announced a round early. Only where the cluster budget and the joining cap allow. At most one placed call source per encounter **(change EP-22; encounter placement design 6.12; M2)**. | 3+ | Leader, artillery | M2 |
| Goad | **Bodyguard** | Challenge: bonus action, cooldown 3, applies Challenged to an adjacent enemy | 2+ | Soldier | M2 |
| Goad | Protector | Guarded on an adjacent ally | 3+ | Soldier | Later |
| Rider | **Honed** | Its weapon at-will carries its damage type's rider | 2+ | Armed types | M2 |
| Rider | **Venomous** | Its damaging at-will applies Poisoned on a failed Fortitude save | 2+ | Skirmisher, lurker | M2 |
| Aura | **Chilling** | Turn-start Fortitude save within 1 square or Chilled; a success gives immunity for the fight **(settled aura rule)** | 3+, frost themes (themes with `frost`; **change RT-4**) | Brute, soldier | M2 |
| Aura | Dread | The same shape, Will save or Shaken | 3+ | Leader, controller | Later |
| Control defence | **Unbreakable** | Unyielding, permanent | 2+ | Any | M2 |
| Control defence | **Steadfast** | Advantage on saves against `fear` and `goad`; morale profile becomes `fearless`. Not legal if it would break the theme's non-fearless half. | 2+ | Leader, soldier | M2 |
| Setup | Spotter | Spotted on hit | 3+ | Skirmisher | Later |

**Banned pairs** (beyond one affix per group): Bodyguard with Thorned,
Riposting or any aura; Slippery with Swift; Warded with Unbreakable;
Vampiric with Plated on floors 1–3. Bodyguard, Commanding and Pouncer each
use the bonus-action slot, so the kit cap excludes any two of them
together.

**Cut:** flat "+1" affixes (dead numbers); elemental-damage affixes (the
theme overlay does this); teleporting; summoning; "cannot be Surprised" (it
would delete the Stealth guarantee). No affix changes guile. **(lead's
call)**

### 17.3 Behaviour profiles

The schema is canonical. The weights are approach / caution / oaAversion,
in percent, all illustrative.

| Profile | Default for | Approach / caution / oaAversion | Range | Targeting | What the player notices | Wave |
|---|---|---|---|---|---|---|
| **Charger** | Brute | 150 / 30 / 30 | 1 | Nearest, last attacker; kill bonus 130% | Walks through opportunity attacks to reach you; never retreats | M1 |
| **Holder** | Soldier | 100 / 80 / 100 | 1 | Nearest to its ward | Holds the doorway; shoves toward hazards (M2). **Pending request (TR-15, M2; room terrain design 6.9):** "hazards" includes contact hazards; not yet ruled. **Pending request (hazards design 6.12; M2 (gated), else M3):** also toward due footprints and Voids, the intent reading "pushes toward the edge"; not yet ruled. **Pending request (EP-6, M2; encounter placement design 6.6.3):** the hold is the Billet term (11.1) on the choke square, weight 40; not yet ruled | M1 (hold term M2) |
| **Kiter** | Artillery | 60 / 150 / 200 | 5 | Lowest visible AC in range; concentrating units **(change AR-25)** | Keeps 4–6 squares away; steps back when engaged; falls back at 50% HP. **Pending request (EP-6, M2; encounter placement design 6.6.3):** "falls back" gains a destination, its fall-back square (also on a formation break or when adjacent to a noticed party unit), and a Billet weight 25; not yet ruled | M1 |
| **Mob** | Minion | 150 / 0 / 0 | 1 | Nearest; spreads to free adjacent squares | Surrounds you; ignores danger | M1 |
| **Automaton** | Mindless | 120 / 0 / 0 | 1 | Nearest, then unit order | No exposure or cohesion terms; walks through minor hazards (the lethal floor still holds). **Pending request (hazards design 6.12; M2 (gated), else M3):** ignores non-lethal due squares, keeping the lethal floor (shown on its card); segment 4 does not raise the mindless share and asks this owner; not yet ruled. **Pending request (TL-16, M2; traps and locks design 6.8):** mindless types treat non-lethal traps as floor and spring them (the mindless blunder), lethal ones still barred; not yet ruled | M1 |
| **Boss** | Bosses | Authored; `leash: room` | — | Authored | A phase may swap the profile. **Pending request (FL-21 (a), M1; floor layout design 6.11):** a Provoked analogue that extends the leash to the Sanctum when the boss is damaged from outside its reach; not yet ruled; segment 9 asks for the narrow form, pursuit to the Arch squares, never into the Forecourt (boss rooms design 4.6). **Pending request (BR-6, BR-14, M2; boss rooms design 4.3.1, 4.6):** the Rhythm and per-boss data weights (11.1), and a narrower leash value `chain(n)` that limits the unit's own movement only (forced movement unaffected; a unit pushed beyond it returns toward its anchor), replacing a `fixedInPlace` exception; not yet ruled | M1 (M2 adds boss actions) |
| **Raider** | Skirmisher | 120 / 120 / 200 | 1 | Lowest visible AC; concentrating units **(change AR-25)** | Hits, then uses leftover movement to leave reach; slips past | M2 |
| **Protector** | Bodyguard elites | 80 / 60 / 120 | 1 | Whoever threatens its ward | Body-blocks; Challenges the unit nearest the leader | M2 |
| **Disruptor** | Controller | 80 / 120 / 150 | 3 | Highest T | Spends control on the most dangerous unit; setup at 150% | M2 |
| **Commander** | Leader | 70 / 120 / 150 | 2 | Quarry, else nearest | Stays behind its line; uses Command; declares Quarry. **Pending request (EP-6, M2; encounter placement design 6.6.3):** a Billet weight 30 and a fall-back square on a formation break or when adjacent to a noticed party unit; not yet ruled | M2 |
| **Stalker** | Hunter affix, pack beasts | 130 / 80 / 80 | 1 | Its own Quarry | Picks one unit and keeps after it | M2 |
| **Ambusher** | Lurker | 100 / 150 / 200 | 1 | Lowest HP fraction in reach | Waits Hidden, opens, retreats and Hides again. **Pending request (encounter placement design 6.8.3, 6.15):** "waits Hidden" made precise as "until a party unit is within 3 squares and in its line of sight, or its group or the bait is attacked or notices"; not yet ruled | M2 |
| **Opportunist** | Skittish scavengers | 100 / 150 / 150 | 1 | Lowest HP fraction above 0 | Picks off the wounded and breaks early | M2 |
| **Sentinel** | Door, treasure and shrine guards | 80 / 100 / 100 | By kit | Nearest inside its leash | Never leaves its 6-square leash while it can answer. **Provoked (lead's call):** when damaged by a unit it cannot reach from inside its leash, it drops the leash for that fight and switches to Holder with pursuit 2, shown on the plate ("Provoked: leaves post"). Plinking from outside the leash is therefore never free. **Pending request (FL-21 (b), M2; floor layout design 6.5):** the Champion's elite uses a Sentinel-style hold, so noticing through a Grille leaves it Alerted but holding; not yet ruled. The elite's Billet is offered as that hold if EP-6 is accepted (11.1; encounter placement design 6.10) | M2 |

**No per-instance randomness.** Two units of the same type behave the
same, which is what makes them learnable, and what makes intent reading
(13.3) worth learning.

**Pending request (TR-15, M2; room terrain design 6.9, 6.6):** one
visible terrain-using enemy per F2–5 theme, with a telegraphed intent (a
Strongman Shunts a crate at the backline; a Glutton Kicks a trolley; a
Fire-Eater breathes on a Keg; a Gut Ooze shoves toward molars); and one
gas-aura monster each in the Gut, Silk and Heart (a Gut spore-bloat, a
Silk mildew moth, a Heart sleeping-draught nurse; unit auras are legal
under ability 8.4); a request to this owner, not yet a rule.

### 17.4 Encounter templates

| Template | Slots | Band | Floors | Awareness | Notes | Formations **(change EP-4)** | Wave |
|---|---|---|---|---|---|---|---|
| **Picket** | 1–2 standards + 0–2 minions (≤2 when solo) | Patrol or guard post | 1+ | Patrol or stationary | The universal fallback. On floor 1 solo, budgets make a patrol one standard: the floor's Stealth and ambush teaching target. | Picket Line; Door Line (front + artillery standard); Kill Box (Waylay, F2+). Patrol File; Revel, Bout | M1 |
| **Pack** | 1 brute (M2: or skirmisher) + 2–4 minions of one family (2 when solo) | Patrol, guard post or lair | 1+ | Patrol or dormant | Minions share one initiative slot | Crescent; Pincer (F3+). Patrol File; Sleeping Ward; gorged Revel | M1 |
| **Line and Bow** | 1–2 soldiers or brutes + 1 artillery | Guard post or lair | 1+ | Stationary at a chokepoint | Teaches "go around" | Door Line; Phalanx (2 soldiers, F2+); Kill Box (Waylay, F2+). Revel, Bout | M1 |
| **Boss Court** | Boss + escort (0–2 standards or a minion pack; no elite on floor 1) | Boss | All | Wary from the start | Boss alone in M1. Escort budget: the shortfall-adjusted party weight × 200% × depth% × ladder%; boss HP ignores the shortfall (17.5) **(change CMP-7)**. Floors 2–5: ≥ 1 non-fearless group (17.5) **(change BR-19; agreed, Q4; M2)**. **Pending request (BR-12, M2; boss rooms design 4.5.3):** AddCap, a per-arena headcount trimming reserves (17.5) | Retinue for the escort (segment 9 owns the arena set) | M1 / M2 |
| **Champion** | 1 elite + 1–2 minions or standards, at least one of them an awake **watcher** placed with line of sight to every square adjacent to the elite | Elite room | 1+ (second half of floor 1) | Stationary, awake, visible from the approach (through the Grille vantage behind an opt-in vantage Door; floor layout design 6.5; **change FL-7**) | Guaranteed notable item or cache; Steal needs the watcher drawn off first (15.5). **Pending request (FL-21 (b)):** a Sentinel-style hold for the elite (17.3) | Champion Watch | M2 |
| **Warband** | Leader + 1–2 front + 0–4 minions | Guard post or lair | 2+ | Stationary or dormant | Morale showcase; the usual carrier | Retinue; Phalanx (2 front); Pincer (F3+); Kill Box (Waylay); **M3:** Ritual (the Choir only, F3; change EP-27). Revel, Bout; Sleeping Ward | M2 |
| **Den** | Leader-role "mother" (a beast silhouette is allowed) + 1 non-minion standard + 2–4 minions | Lair | 2+ | Dormant | Stealth-bypass candidate. The extra standard keeps one kill from ending the lair (14.3). **Pending request (FL-21 (d)):** in-room sneaks against 20.1 (16); the Sleeping Ward sneak line (EP-23, pending; 16) | Crescent (the mother as alpha). Sleeping Ward | M2 |
| **Skirmish Screen** | 2–3 skirmishers | Patrol or guard post | 2+ | Patrol | Tests formation | Skirmish Spread (patrol only; never a Waylay). Patrol File | M2 |
| **Ambush** | Bait (1 standard or a container) + 1–2 lurkers as a separate group | Guard post; × 0.85 for a Mousetrap **(change EP-10)** | 1+ (after room 2) | The lurker group Stationary and Hidden at start; the bait group Stationary **(change EP-10)** | A visible **tell** within 6 squares; trained Perception marks it (the tell) on the map (encounter placement design 6.8.4). A container bait (the Mousetrap's reserved bait cache) only on floors 2–5 at party ≥ 2, at most 1 per floor and 2 per run *(illustr.)* **(change EP-10; encounter placement design 6.8.1)**. **M3:** the bait container may be the lurker itself, a boxed lurker (17.1) **(change TL-28; traps and locks design 6.14)** | Bait and Nook (Cuckoo or Mousetrap) | M2 |
| **Battery** | 2 artillery + 1 soldier (optionally an elite bodyguard) | Guard post | 3+ | Stationary | Banned on floors 1–2 by the ranged cap | Crossfire (F3+, party ≥ 3); Kill Box (Waylay) | M2 |
| **Honour Guard** | Bodyguard elite + artillery or controller + 0–2 minions | Lair | 2+ | Stationary | Challenged showcase | Honour Ring | M2 |
| **Gauntlet** | Controller + 2 brutes | Lair | 3+ | Dormant or stationary | Hard control on the controller only | Grindstone. Sleeping Ward | M2 |
| **Scavengers** | 2–3 skittish standards or minions; no elite or leader | Patrol | 1+ | Patrol | Always cowable (content test); fills the cowing guarantee | Scramble. Patrol File | M2 |
| **Sleeper** | 1 dormant elite at room level +0 to +2 (its legal elite range, 6.3) + 0–2 dormant standards or minions to reach the fill | Lair | 2+ | Dormant | Stealth bypass or steal target; never cowable (it holds an elite). **Pending request (FL-21 (d)):** in-room sneaks against 20.1 (16); the Sleeping Ward sneak line (EP-23, pending; 16) | Picket Line (the elite as the front Keystone). Sleeping Ward; gorged Revel | M2 |
| Summoner Circle; Mender's Band; Feud | — | — | 3+ | — | Feud needs AI for monsters hostile to each other. **Later (EP-40; encounter placement design 9.1):** Standoff / Riot, Gaol Jailers against Inmates, a three-sided fight, waits on Feud | — | Later |
| Rival Party | See section 18 | Boss or elite room | 3+ | Wary | At most one per run | — | Later (stretch) |

- **Promotion in templates (lead's call):** a standard slot may hold a
  promoted elite wherever the floor's elite cap (10.3) allows, unless the
  template names the slot's rank.
- **Formations (change EP-4; encounter placement design 6.4, 6.6.6,
  11.1; M1 static parts, M2 the rest):** the column lists each
  template's battle formations, then (after the full stop) its start
  shapes; the `FormationDef` table of the encounter placement design
  owns their positions, Keystones, breaks and counters. A Den fights as
  a Crescent and a Sleeper as a Picket Line once Engaged.
- **Waylay (change EP-12; encounter placement design 6.8.6; M2):** an
  ordinary Stationary Line and Bow, Picket, Warband or Battery (minded
  members, at least one ranged or skirmisher member, no elite) in the
  **Kill Box** at a guard post on floors 2–5 at party ≥ 2, watching a
  Portal whose approach side is a Door that starts closed; never
  Surprises the party; its tell is the **Spoor mark** on the watched
  Portal's frame, present if and only if a Waylay lies beyond; it counts
  as an ambush (10.3).
- **Class answers (change EP-29; encounter placement design 6.16; M2):**
  each formation record carries `answers{shade, vanguard, arcanist,
  herald}`; a formation leaves at most one class without a lever, and
  each class has a showcase on at least one main-path fight on floors
  1–3.
- **Per-theme eligibility (change EP-31; encounter placement design 6.4;
  M2):** a per-theme template eligibility table is a content-test input
  (for example, no Ambush in the Choir, Carnival, Supper or Heart);
  `TemplateLegal`, `AcidSourcesAfterOverlay`, one shover type per
  encounter in Void and clock rooms, and Busy barred for an all-mindless
  group.
- Content test: each template's own encounter, at every party size and
  floor in its range (and every reachable party weight, down to the
  shortfall minimum, 10.1; **change CMP-8**), fills within tolerance and keeps the 3-type cap and
  the role rule (10.3).

### 17.5 Boss framework

**Anatomy** (settled items marked):

- hand-designed, from a pool per floor **(settled)**; boss pool records
  carry `eligibleFloors` **(change HR-21; Herald design 7.16)**, and
  `themes[]`, `final` and `shipStatus` **(change RT-4; run structure
  design 6.4, 8.0)**;
- **the final-boss pool:** floor 5's boss comes from the records marked
  `final` (2 in M2), drawn by the route pre-roll; when the Last Hollow
  choice is on (`route.lastHollowChoiceEnabled`, run structure design
  6.11), the final boss is instead picked at camp 4 from the first two
  drawable entries of that order **(change RT-11; run structure design
  6.5)**;
- the same stat model as every unit **(settled)**;
- **Unyielding** permanently **(settled)**;
- **resolve pips:** 2 on floor 1, 2–3 on floors 2–3, 3 on floors 4–5.
  `resolveThreshold` defaults to Dazed's `controlRank` **(settled
  mechanism)**. A fear effect that would land on a boss with pips spends a
  pip instead (Frightened's `controlRank` equals Dazed's), and the fear
  repeat set persists on the boss's life state beside its pips (the
  conditions owner's ruling, HR-16a/d; HX-1, HX-1b; Herald design 10.2);
- **phases** **(settled)**. Phase changes end harmful conditions and grant
  Steeled;
- at least one tool against sustained effects **(settled)**;
- tuned so that a 5–7 round fight is won by a party with about half its
  camp resources **(settled)**. **Pending request (FL-21 (e), M2; floor
  layout design 6.8):** tune it including the floors 2–5 patrol arriving
  as an ordinary joiner, measured by `BossFightJoinRate`; not yet ruled.
  **Pending request (BR-13, M2; boss rooms design 4.5.4):** segment 9's
  answer: the patrol joins through an Arch after footsteps under the
  1/1/2/2/2 caps, counts in AddCap, is enumerated in the 35% row at its
  worst arrival round, slips a (d) reserve due the same round by one
  round, and never counts as the floors 2–5 non-fearless group; not yet
  ruled;
- `fearless`; guile 10 + Wisdom + proficiency + 2, with announced
  signature moves (13.2, 13.3);
- **a floor-1 boss** (`eligibleFloors` includes floor 1) is never
  `mindless` and never psychic-immune; class-blind, and a `mind`
  immunity alone passes (`Floor1BossMindReachable`, 20.1). Escort and
  reserve groups are not constrained **(change HR-21; Herald design 7.16;
  client agreed 2026-11-20, Q3)**, except that a theme below 75%
  non-fearless joins the floor-1 pool only with `F1EscortBreakable`
  (every escort and reserve group of its floor-1 bosses drawn from
  non-fearless types) and a passing reference-arm `Floor1ClassSpread` in
  its split **(change RT-9; run structure design 8.2)**. **Floors 2–5
  narrow this:** every Boss Court, in every formation at every party size
  after AddCap, holds at least one **non-fearless group** (an escort group
  or the (d) reserve group all of whose members are non-fearless types;
  `mindless` types never count, nor does the patrol joiner), placed in the
  escort at fight start wherever the escort budget allows **(change BR-19;
  boss rooms design 4.7.4; client agreed 2026-11-22, Q4; M2)**;
- never Death-warded **(settled)**; never affixed;
- **HP** ≈ 6 × the party's typical damage per round × 0.85 (the rest of
  the budget is the escort), of the **actual** party at floor generation,
  stored per boss as `hpByPartySize[1..4]` (authored to this formula,
  simulator-calibrated) and read by the actual size; the escort budget
  uses the shortfall-adjusted weight × 200% × depth% × ladder%; HP ignores
  the shortfall. Content test: a row per boss per party size **(change
  CMP-7; companions design 7.9)**. Illustratively about 40 on floor 1 (solo,
  level 3), 265 on floor 3 (trio, level 7), 430 on floor 5 (four, level
  10).

**Immunities:** at most 1 damage-type immunity, never to the theme's
counter type (its `counterType`; **change RT-4**; run structure design
8.0); at least one weakness; at most 2 resistances; at most 2 tag
immunities, never `goad` (Challenged is the tank's settled tool).
**Pending request (BR-20, M2; boss rooms design 4.7.5):** `final` records
and floor-1 bosses take no damage-type immunity; no boss resists its
theme's `counterType`; no boss is immune or resistant to its own arena
clock's damage type; not yet ruled.

**Kit:**

- 2 at-wills, 2–4 limited abilities (1–2 of them `signature`), at most 1
  reaction beyond the opportunity attack, at most 1 bonus-action ability;
- 1–3 boss-action options;
- at most 2 harmful conditions per phase kit and 3 across phases;
- **at most one party-facing hard-control ability**, with cooldown ≥ 3 and
  `readyAt` 2, lasting 1 turn, behind the depth gates (none on floor 1).
  Gambits (13.2) are never hard control, except the Head of the Table's
  Frozen Toast, whose Frozen lands only on a unit still standing on its
  one committed square; it remains the boss's one hard-control ability
  **(change BR-32; boss rooms design 4.14.3; client agreed 2026-11-22, Q3;
  M3 gated, else Later)**.

**Boss actions** (M2) **(change A-3)**:

- **Count per round:** min(1 on floors 1–2 or 2 on floors 3–5, the number
  of party units).
- **Timing:** at most one at the end of any one unit's turn, never after
  the boss's own turn.
- **Menu:** **Strike** (one at-will); **Reposition** (half speed; it
  provokes); **Shed** (end one harmful condition) **(settled)**;
  **Command** (one escort member moves); **Signature** (a flagged limited
  ability, announced per 13.2).
- **Never hard control.** Hampered never blocks boss actions
  **(settled)**.
- **Pending request (BR-7, BR-17, M2; boss rooms design 4.8.1, 4.7.2):**
  Signature is never a boss action (every boss authors its 1–3 options
  from Strike, Reposition, Shed and Command; floor 1: Reposition, Shed,
  Command); Shed ends only a condition on the boss at the start of the
  round; no two boss actions on the same unit in one round; none on a
  unit in a live Gambit footprint (M3); a forced spend before the boss's
  turn; and **Upended**, a boss state from a Device's Expose payoff (no
  boss actions until the end of the boss's next turn, once per phase,
  applied whatever its save or a Prone immunity), which with the denial
  budget are the only boss-action denial sources; not yet ruled.
- **Pending request (BR-21, M2; boss rooms design 4.8.2):** "party
  units" in the count is read as non-Downed party units at round start;
  not yet ruled.
- **Pending request (BR-41, M3 option; boss rooms design 7):** each
  boss-action option carries one Prompt on the unit whose turn just
  ended, gated on `ThreatMix`; not yet ruled.

**Phases:**

- Thresholds are on the HP bar from the start: 50% on floor 1; 66% and 33%
  on floors 2–5.
- **Damage carries through thresholds.** If one event crosses two, both
  entries resolve in order.
- **On entry**, besides the settled effects, exactly one authored change:
  (a) swap one ability; (b) swap the behaviour profile; (c) gain Warded 1
  **(settled source)**; (d) call an **authored reserve group**, paid from
  the boss budget, excluded from the cluster and joining caps, announced a
  round early, and created in the boss's group as escort (14.2); (e)
  change arena terrain within the theme's 6, never on a party unit's
  square, only through pre-placed phase sockets: existing kinds only,
  announced one round ahead, never on the Clearway, and proven in both
  states by V9 (room terrain design 6.11) **(change TR-16)**; at M3 it may
  also create a Remnant (room terrain design 6.6, 6.11) **(M3; agreed, Q5)**.
- **Timing of (d) and (e) (change BR-10; boss rooms design 4.3.4; M2):**
  an entry in round R is announced at once and resolves at step 1 of
  round R+1's arena round-end step (hazards design 6.3's round-end Dream
  step). At most one headline (a due (d) arrival, a due (e) change, a due
  Stir act or a live Gambit) is announced and unresolved at a time; the
  newer one slips, and a headline takes at most 2 rounds of delay in
  total.
- **Pending request (BR-9, BR-11, BR-12, M2; boss rooms design 4.4,
  4.5.2, 4.5.3):** triggers stay HP-only; the last threshold (50% on
  floor 1, 33% on floors 2–5) uses only (a), (d) or (e), (b) and (c) only
  at 66%; phase marks show their change type from turn 1 (13.1); a (d)
  reserve waits in a visible, inert reserve socket in the arena, never
  acts in its arrival round and rolls initiative as a joiner (12.4 item
  4) in round R+2; reserves count in **AddCap** (non-boss hostile units in
  the arena ≤ arena party size + 1 *(illustr.)*), trimmed with the cost
  buying Few-and-Strong, never trimming the last non-fearless group; not
  yet ruled.
- **Never on entry:** regain HP or pips, or refresh uses.

**Boss state belongs to its life, not the fight** **(change A-4)**:

- Pips spent, phases passed and uses persist across combat ends. Cooldowns
  reset to ready at combat end and `turnsTaken` resets at the next fight
  start, as for every monster (9.3), so `readyAt` still guards a
  re-engagement's opening turn.
- A boss that loses the party returns to its post as **Wary**, leashed to
  its room, and **regains HP up to the top of its current phase band**.
- Retreat-rest-return therefore costs the party supply and keeps the phase
  progress it earned, but it cannot whittle a boss down through repeated
  pokes.
- **Pending request (BR-6, BR-14, M2; boss rooms design 4.3.1, 4.6):**
  the life state also holds the Rhythm pointer and any pending signature,
  Device charges, reserve state and the Rendition; not yet ruled. Beside
  this rule, segment 9 bans short rests in the Sanctum while the boss
  lives and holds party per-fight resources across Sanctum combat ends
  (the ability model and death and healing owners apply it; boss rooms
  design 4.2.5); this rule is read, unchanged.
- The exits are never sealed, so fleeing by breaking line of sight still
  works **(settled)**. "Exits" means the arena entrances; the Bossgate to
  the Stairhead behind the arena is not an entrance and opens on
  `BossDefeated` (none on floor 5) (floor layout design 6.11) **(change
  FL-6; a clarification)**.

**The arena** is an authored room template stamped into generation; there
are no separate arenas **(settled)**; its template grammar and Guises are
boss rooms design 4.1 (read):

- at least 2 entrances (kept; realised as two dog-leg Arches from the
  Forecourt antechamber, ≥ 5 apart, never Doors, with the symmetric
  `AntechamberScreened`; `AntechamberDominatesArena` on every floor;
  floor layout design 6.11) **(change FL-5)**;
- pillars or cover that block line of sight (at least 2 Tall pieces or
  clusters; cover has the rules meaning of room terrain design 6.1:
  directional one-tier cover, M2) **(change TR-2)**;
- at least one feature tied to the boss's counterplay (a brazier against an
  ice layer, a chasm edge for shoves; the chasm edge is met by the
  arena Void line from M2 (gated), not counted as a contact hazard, and
  by a contact hazard otherwise: change HZ-11; hazards design 6.4);
- optional set-piece hazards within the theme's 6;
- terrain caps and kind rules: room terrain design 6.11 (contact
  hazards, Furnishings, Kegs, cover-giving squares, clutter); the
  counterplay identity is unchanged **(change TR-16)**;
- the boss and escort start **Wary**, so the boss room never surprises the
  party and is never Surprised.

**Boss death** Routs the non-fearless escort, and mindless servants go
inert (T7).

**Signature item:** guaranteed, visibly carried by an armed boss or
guarded in its lair's signature cache by an unarmed one ("Guards:
<signature name>", 15.2; agreed, Q5; change IR-13; item rolls design
4.13), plus a pick-one-of-three
reward card that reuses the offer UI. **(change LE-3; loot economy design
6.3; M2)** The signature is the boss's authored form for the top tier of
the floor's drawn deck variant, plus its authored echo line (15.2;
change IR-13). The card is one offer set at
C / U / R / R on floors 1–4 *(illustr.)*, **never Very rare**; it is
outside the floor's notable count and the 3-per-encounter cap, and its
taken option counts in the floor's value and toward family caps. Its
options fill three slots in order (Yours, Theirs, Wayward), each usable
by a member in the party snapshot at generation (hard floor), with
distinct families, distinct fine categories or effect tags, and at most
1 damage option; an option with no usable record is drawn **on its own**
as usable gear at the same tier, not fit-filtered (`bossCardFallback`,
entry = option index; replacing the all-three fit-premium redraw), then
the offer terminal ladder; if no option is usable by the hero, Theirs is
redrawn as hero-usable gear (entry 4); a boss-channel phial (consumables
design 14.7) fills option 2 **(agreed, Q1; change IR-8; item rolls
design 4.1, 4.8, 4.10; M2)**. Unchosen options are dreamed away (15.3
step 2). **Floor 5:** no reward card; the signature drops as the
Memento (15.2), and the Viaticum takes the boss share (15.3 step 1)
**(agreed, Q1; change LE-5; loot economy design 6.11)**. **Perilous floor
(M3):** on a floor entered through the Perilous Door, this card shows its
pre-drawn fourth option and becomes choose 1 of 4 (a reading accepted by
the client on 2026-11-22; agreed, Q5; change LE-19; loot economy design
6.12).

**M1 boss:** resolve, Unyielding and one phase at 50%; no boss actions;
**no hard control**, which the floor 1 gate forbids. Its kit keeps the
condition caps (2 per phase kit, 3 across phases). **(lead's call)**

- **Phase 1:** at-wills: the Bleeding claw and a plain bite; limited: a
  cooldown roar (Frightened, `signature`) and a damage-only slam on
  cooldown. Conditions: Bleeding, Frightened.
- **Phase 2 (at 50%),** option (a): the roar is swapped for a limited
  Dazed (`signature`). Conditions: Bleeding, Dazed.
- Across phases: Bleeding, Frightened, Dazed = 3. Prone is left to the
  soldier's Shove.
- Both signature moves are flagged and announced (13.2): the roar from the
  first turn, the Dazed from phase 2's entry.
- **Pending request (BR-22, M2; boss rooms design 4.9):** M1 unchanged; in
  M2 the roar takes `readyAt` 2 (9.3), with the M1 values re-run; if
  refused, this boss is exempt and its Rhythm opens on the roar; not yet
  ruled.

## 18. Later stretch goal: the rival adventuring party

**(agreed as a Later stretch goal; details lead's call)**

### 18.1 What it is

A rare set-piece encounter: a rival party of adventurers built with
near-full player-character rules (class kits and levels from the class
data), hand-tuned, met at most once per run, either as an optional
encounter or as a floor's boss.

- **Placement:** floor 3 or deeper. Either an entry in a floor's boss pool,
  or an optional encounter in the elite-room slot (off the main path,
  visible before commitment). **At most one per run.** Its template is
  Rival Party (17.4).
- **Members:** 2–3 rivals (illustrative), each a class from the class data
  at the room level or one below, with hand-picked kit choices, the
  party's derived numbers, and gear chosen by the author.
- **Pools become limited uses for that one fight.** Every `rest` and `camp`
  pool converts to `uses:N` for the unit's life, with N its maximum. Rests,
  Slot Recovery and refills never apply. A use converted from a `camp`
  pool keeps a **camp-spend** mark, so the settled one-camp-spend-per-turn
  rule (ability model 6.4) still binds each rival and caps nova turns.
  **(lead's call)**
- **Hard control** in a rival kit obeys 9.3 like any monster's: `readyAt ≥
  2`, `cooldown ≥ 3` (a converted `uses:N` hard-control ability gains that
  cooldown), never opener-flagged, and behind the depth gates.
- **AI:** the party scorer (ability model 13), which already reasons about
  pools for the simulator and suggestions, at a fixed pressure set in the
  rival's data, plus a monster behaviour profile for movement. The Hidden
  rule, every fairness invariant and the p95 budget apply.
- **Validation:** the whole rival party is validated against the **boss
  guards** (6.4): expected damage to one unit per round ≤ 35% of the hero's
  HP, worst spike ≤ 70%, no one-round kill of a full-HP hero without a
  crit. The depth gates and hard-control caps apply as for bosses.
- **Budget, experience and loot:** the boss band (200) for budget and
  experience, whichever slot it takes. Its gear is issue gear, except at
  most one notable item per rival (at most 3 in all), drawn from the
  **enemy-carried channel** of the floor's loot budget, so the floor total
  and the boss's own signature item are unchanged. **(lead's call)**
- **Rank:** `rank: elite` is a **flag only**, for the depth gates, Demand
  Surrender and the absence of resolve. The elite template (HP ×2, the
  `rank.elite` bundle, multiattack) is **not** applied: rivals use
  player-character numbers plus the boss guards. **(lead's call)**
- **Caps:** a rival is exempt from the monster kit caps (9.1) and the
  2-per-type condition count (9.2), which a class kit cannot meet. The
  boss guards, the depth gates, the hard-control caps (9.4), the theme's
  6 harmful conditions and the 3-items-per-encounter cap (15.1) still
  bind. **(lead's call)**
- **In a boss slot** the rival party is treated as a boss encounter for
  budget, experience, the reward card, the arena and the reset rule
  (17.5: HP back to full on a reset, spent uses stay spent), with one
  announced `signature` per rival party (13.2). The boss channel's
  signature item is carried by the captain and counts toward the rival
  party's 3. The validation rules that a boss is fearless and has 1–2
  signature abilities apply to the party as a whole, not to its members.
  **(lead's call)**
- **Morale:** rivals are `steady`, with a captain as leader. They can be
  Routed and can Yield; a yielding rival gives the floor fact. No resolve
  pips, Unyielding only by authored data, and Demand Surrender works on a
  rival once Routed. T7 does not apply: a rival party has no boss.
- **Readability:** a class glyph replaces the role glyph; the plate
  contract holds; the kit is listed on inspect. Guile is 10 + Wisdom +
  proficiency + 2, as for leaders (13.3).
- **Content:** a handful of authored rival parties in total.
- **Prerequisites:** the M2 party scorer, at least four classes, and
  companion kits proven in play.

### 18.2 Why full PC rules are wrong for ordinary enemies

- **The kit is never seen.** An ordinary enemy lives 3–5 rounds. A
  player-character kit (several limited abilities, subclass features,
  passives) would mostly go unused, so content cost buys nothing the player
  notices.
- **Attrition pools mean nothing for a one-fight unit.** Rest and camp
  pools exist to make spending across a floor a decision. A unit that lives
  one fight either **novas** (spends everything at once, breaking the
  guards and the nova-turn anti-goal) or **hoards** (thrift keeps it from
  ever spending), and both are wrong.
- **Readability.** The four-things-to-read plate limit cannot hold a
  player-character sheet, and fights against several of them would bury
  the player.
- **Pricing is unreliable.** 3.5e's class-level NPCs were famously hard to
  price: their power depended on build and gear, and challenge ratings
  missed in both directions. 4e, 5e and PF2e each moved to monster-building
  rules with target tables, which is the shape of our hybrid (decision 2).
- **AI cost.** The scorer would have to weigh pools, pressure and spell
  choice for every enemy, against a 50 ms budget.
- **Content volume.** Every enemy would need a class, a subclass, level
  choices and gear, multiplying the M2 content cost (about 22–27 types).

### 18.3 Why a few rare, hand-tuned ones work

- **It is a boss-length fight,** so the kit is seen: 5–7 rounds, with the
  plate read slowly and the inspect panel used.
- **Limited uses solve attrition** for one fight, and the one-camp-spend
  rule, kept through the camp-spend mark, bounds the nova.
- **The AI already exists:** the party scorer is built for the simulator
  and suggestions.
- **Hand-tuning plus the boss guards** replaces unreliable pricing with a
  validated, authored budget.
- **Rarity keeps content cost to a handful** and keeps the mirror match a
  surprise: a set piece, not a system.

## 19. Data and the core

**(lead's call)**

### 19.1 Monster data

Illustrative:

```json
{ "id": "monster.raider_spear", "nativeLevel": 2, "levelShift": [-2, 2],
  "role": "soldier", "rank": "standard", "promotable": true,
  "abilities": { "str": 2, "dex": 1, "con": 0, "int": 0, "wis": 0, "pre": -1 },
  "hp": 15, "trainedSave": "fortitude", "skills": ["perception"],
  "speed": 6, "traits": [], "atWills": ["atk.spear", "act.shove"],
  "limited": [], "reaction": "react.interpose",
  "behaviour": "holder", "allowedAwareness": ["stationary"], "morale": "normal",
  "armed": true,
  "loadout": { "mainHand": "item.spear", "offHand": "item.shield", "armour": "item.gambeson", "carried": null },
  "improvised": "atk.improvised_pierce", "notableSlot": true, "salvageCoin": "byLevel",
  "themes": ["theme.raiders"] }
```

- **Composed unit (generation output):** base id, level, rank, affixes,
  overlay, `carriedItem { itemId, rolledProperties, chargesOrUses,
  linkedAbilityId }`, `costAdd`, **`cost`** (frozen unit cost at its
  homeRoom, costAdd included; 10.1), `groupId`, `earliestRoomLevel`.
- **Type awareness:** `allowedAwareness[]` replaces a single
  `awareness`; the group's start state is chosen within it (12.1)
  **(change EP-9; client agreed 2026-11-21, Q4; encounter placement
  design 6.7; M2)**.
- **Group record:** `{groupId, homeRoom, members, leader?, startCost,
  lossCost, flags, awareness, experienceShares, paidShares}`. It gains
  the placement fields `encounterId`, `linkedGroupIds[]`, `formationId`,
  `startShapeId`, `activityId`, `posture`, `watchedPortal?`,
  `openZone[]?`, `ambushForm?`, `tellSquare?`, `spoorPortalId?`,
  `pairings[]`, `defensiveVariantId`, `billetScale`,
  `nearPairPartnerGroupId?`, `callSourceUnitId?`, `annex?`, per unit
  `billet`, `keystone`, `facing` (presentation only), `leaderId?` and
  `wardOf?`, and a reserved `captiveSocket?` (Later) **(change EP-4;
  encounter placement design 6.13, 11.1; M1 static parts, M2 the
  rest)**.
- **Unit state additions:** awareness record per party unit, morale state,
  `oncePerLife` grants used, `turnsTaken` (9.3), `stealAttempted` (15.5),
  "Rally used" (14.4), boss life state (pips, phase, uses).
- **Loot plan record:** per-channel counts, items with their `substituted`
  flags (15.3).

### 19.2 Queries

The display asks; it never computes:

- `ThreatReach(unit)` for the overlay (shares the party threat map).
- `ReadIntent(enemy)` → `NotInScope` | `Hidden` | `Fleeing` |
  `Unreadable(guileIfLoreKnown)` | `Target(unit | none, ability?)`, the
  ability only with Tactician; cached per state (13.3). Guile is derived
  on demand from Wisdom, level (proficiency) and role, never stored.
- `ThreatPrice(unit)` → power and price in permille, pass or fail (6.5),
  for validation and the content tools.
- `SignatureForecast(boss)` → the move and its target or area, if the boss
  would use one now (13.2).
- `BreakLine(group)` and the morale preview for any action.
- `CowChance(group)`, `StealChance(carrier)`.
- `HearingOutline(square)` (M2).

### 19.3 Events

- `NoticeRolled`, `AwarenessChanged`, `JoinerIncoming {group, rounds}`,
  `JoinerEntered`.
- `QuarryDeclared`, `QuarryEnded`.
- `MoraleChanged {unit, state, trigger}`, `GroupBroken`, `UnitFled`,
  `UnitYielded`, `GroupCowed`, `FloorFactRevealed`.
- `ItemCarriedRevealed`, `ItemUsedByMonster`,
  `ItemDropped {unit, item, square, reason: death | routed | yield | disarmed}`,
  `ItemSeized`, `ItemStolen` / `StealFailed`.
- `LootSummary {fightId, coin, experience, items[], comparisons[], floorFact?}`.
- `BossPhaseEntered`, `BossReset`.
- `AbilityRevealed {unit, ability, reason: used | lore | tactician}`.

Intent reads are queries. The read pass raises only `AbilityRevealed` for
a Tactician read (13.4).

## 20. Validation, testing and simulator

**(lead's call)**

### 20.1 Content validation

The build fails when:

- a derived number is stored, or attack does not come from `attackAbility`;
- the spread table, the HP, damage or AC bands, or the one-die rule are
  broken;
- a guard fails (6.4);
- `earliestRoomLevel` is not respected in a generator test;
- the save lever rule is broken;
- a kit cap or the 9.2 counting rule is broken;
- hard control appears outside limited abilities, the depth gates, or the
  `readyAt` and `cooldown` minimums;
- a trait, immunity or theme budget is broken (including the non-fearless
  half and rider counting);
- the threat band (6.5) is broken on any enumerated combination;
- a group has more than one leader, or Commanding sits in a group that
  has a leader;
- a banned affix pair appears;
- an affix grant is not `oncePerLife`;
- minion rules or solo minion caps are broken;
- a mindless type is not fearless, or a boss is not fearless;
- a boss whose `eligibleFloors` includes floor 1 is `mindless` or
  psychic-immune (`Floor1BossMindReachable`; a `mind` immunity alone
  passes), or a theme that can appear on floor 1 has no compliant
  floor-1 boss **(change HR-21; Herald design 7.16)**, or a theme below
  75% non-fearless is in the floor-1 pool without `F1EscortBreakable` and
  a passing reference-arm `Floor1ClassSpread` in its split **(change
  RT-9; run structure design 8.2)**;
- a boss has no `signature` ability, or more than 2;
- a leader type lacks Command;
- an armed type lacks an `improvised` fallback;
- a carried item breaks the composed budgets;
- notable items across all channels (the `camp` channel included, change
  I-10; and, Later, the `trader` channel, change I-14), substitutions
  included, do not exactly equal the floor loot budget's per-channel
  counts, two notables on a floor share a base (deck notables only, a boss
  signature exempt; base read as base ?? family; item rolls design 4.7
  items 2 and 7), or an encounter is
  assigned more than 3;
- a morale state is applicable to a party unit, or the morale class holds
  more than 4 states;
- any guarantee or the cluster budget fails per seed;
- floor experience, without the optional elite room, falls outside
  110–135% of need after one bypass (seeded generator test; 16); on
  floor 5 only experience earned before the final boss counts **(change
  RT-7; run structure design 10.2)**; the bypass case is `SkippablePool`
  in its structural form (every main-chain fight host but one guard post
  a cut vertex), read as every entry→antechamber path passing ≥
  `preBossPool` − 100, with the patrol counted present (floor layout
  design 6.8) **(change FL-9)**. **Pending request (FL-21 (d)):** the
  540 multi-bypass case and Den and Sleeper in-room sneaks (16), with
  the "one guard post or one Dormant lair" re-reading if `LairSneakRate`
  exceeds 25% (EP-23, pending; 16). **M3 if HZ-23 (change EV-36;
  events design 4.4.1, 9.2):** a Curiosity bypass route only skins an
  FL-generated Chute (`GraphUnchanged`), and the band is re-checked
  with segment 8's passes on (`XpBandAfterEvents`);
- a Portal breaks `PortalKindsLegal` (the return-Portal principle; a
  Latch's `barSideRoomId` is its deeper end) (floor layout design 9.2,
  11.2) **(change FL-19)**;
- a template's encounter breaks the 3-type cap or the role rule (10.3);
- an armour rule fails: armour design 17.1 rules 4 (AC band and derived
  speed at every legal shift), 5 (role-answer rule), 6 (notable armour
  and shield carriers), 8 (minion armour verbs inert), 15 (AC envelope;
  6.4 guards at the lowest legal party AC) and 19 (an issued loadout
  Strained at any legal shift). **(change AR-29)**
- a theme validator fails: V1 ThemeBudget, V2 BossCoverage, V3
  PoolRules, V4 ThemeFloorFill, V5 RouteEnumeration, V6
  RouteDeterminism, V7 CharterChecks **(change RT-17; run structure
  design 11.2)**;
- V12 `EncounterPlacement` fails: its build-time geometry checks, its
  generation-time re-checks and floor assertions (the 10.3 caps with the
  ambush definition, the 10.4 guarantees and variety, state in the
  group's legal state set, `TemplateLegal`, the floor-1 package) or its
  runtime assertions, after its fallback chain (10.5 step 8) **(change
  EP-32; encounter placement design 11.2; M1 static parts, M2 the
  rest)**.

### 20.2 Tests

- **Morale:** queue determinism (the checkpoint after a whole area use,
  never mid-area); edge-triggering after a Rally; a member is never
  rallied twice in one fight; the minion T1/T2 rule; a lone standard is
  not Demoralised at fight start; a fearless last survivor is not
  Demoralised; the end-of-fight rule; the T5 substitution at step 9 and
  its precedence over Panicked; a Warded target spends its ward on the
  Frighten; no double experience after breaking Cowed.
- **AI:** golden scenarios per profile, including "plink a Sentinel from
  outside its leash"; every fairness invariant as a property test; the
  Hidden rule (an area aimed at a last known square); `readyAt` counted
  in the unit's own turns for a joiner and a re-engaged boss.
- **Intent reading:** for units with no rolled turn-start step and no
  joiner entry or boss action before their turn, a read equals the
  decision the AI makes when that enemy's turn begins with no intervening
  command (the rest are reported under read accuracy, 20.3); hovering
  changes no state, and the read pass draws no random number (stream
  position unchanged; state hash unchanged except for Tactician
  reveals); the same command sequence with and without hovers gives the
  same state; guile = 10 + Wisdom + proficiency + role adjustment for
  every enemy, Perception-trained or not, matching the examples of 13.3
  (a level 1 brute 10, a level 5 soldier 13, a level 5 leader with Wisdom
  +2 17); guile rises when a level shift raises proficiency; the guile
  threshold at equality reads; Tactician extends
  scope to 3 slots and shows the ability; a hidden enemy returns Hidden; a
  signature forecast shows whatever the guile.
- **Steal:** the Dormant and Keen modifiers; disadvantage on an awake
  carrier; one try per carrier; a stolen item's cost stays frozen.
- **Loot:** kill order, save and resume and the method of winning never
  change what drops; replay tests across steal, rout and yield.
- **Generation:** the worked examples in 10.2; every guarantee per seed;
  the M1 floor fills from M1 templates alone; the same seed consumes the
  dungeon and loot streams identically across implementations (10.5).

### 20.3 Simulator reports

| Area | Report |
|---|---|
| Combat | Hit rates both ways by floor; rounds per fight by band (patrol about 2, guard post about 3, lair about 4, boss 5–7); HP-equivalents spent per band at party sizes 1–4, read against an expected curve; hero death per fight by party size **(change CMP-9; companions design 7.9)**; round-1 downs |
| Deaths | Hero deaths by cause: surprise, joiners, chain-pull, affix, focus, hazard push, brute pair |
| Templates and units | Rounds to resolution per template, with the share under 2 (and the share of Pack and leader-template fights ended by morale before round 2, under 10%); elite-room notables stolen without a fight (under 15%); cost residual per type (recalibrates the cost table and the minion and elite multipliers); damage share by role |
| Affixes | Win rate by affix and by affix pair. Outliers block release. |
| AI | Stall detector; decision time; joiner warning lead time (must be ≥1) |
| Intent | Share of enemies readable by floor and party Perception investment, with guile = 10 + Wisdom + proficiency + role adjustment (13.3), gated: targets about 40% for an untrained party, 75% trained, 95% with mastery (**lead's call**; outliers block release); read accuracy (a read matching the action taken, with and without intervening party actions, and with rolled turn-start steps as an expected mismatch); hero deaths with and without reading |
| Morale | Share of fights ended by morale, and rounds saved; Rally uses, and the share undone by counters; Frighten, Demand Surrender and cow attempts and success by floor; **an Intimidation-focused hero against a baseline**; the share of enemies with no morale content, per theme; the early-end target (14.3) with every adopted morale mechanic on (Antiphon, Mourners, Scatter, Waver, Offer Terms if adopted), with and without a Dirge **(change HR-32; Herald design 11.9)** |
| Bosses | Boss kills involving re-engagement, and HP lost per reset |
| Experience | Pool against need, with and without bypass and cow; catch-up frequency |
| Loot | Notables placed against the floor budget; loot summary size |
| Plate | Maximum plate elements on the worst composed unit per floor |
| Themes | ThemeParity, ClassThemeSpread, FloorTimeByTheme and LevelAtDoorByFloor, read from fixed arrival states at both caps; the charter's readability gates; the other reports of run structure design 11.4 **(change RT-17; run structure design 7.4, 11.4)** |
| Armour | The armour design's 17.3 reports, monster rows included: 6.4 guards at the party's lowest AC; verb trigger rate with monster rows; piercing hit gain by target category and the combined reduction swing; how often the reaction rule and the roll-mode cap bind **(change AR-29)** |

## 21. Staging

**(agreed for morale in M2 and the intent waves; the rest lead's call)**

- **M1** (the slice):
  - **Types:** 3 (brute with the settled Bleeding claw, soldier,
    artillery), the minion template and one boss.
  - **AI:** profiles Charger, Holder, Kiter, Mob, Automaton and Boss; the
    M1 scorer terms; the awareness record and Hidden rule; awareness states
    Dormant to Wary; noticing triggers; noise and joining with warnings,
    the joining cap (1 per fight on floors 1–2) and the cluster budget,
    which are the M1 guards against chain-pulls.
  - **Telegraphs (the baseline):** threat-reach overlay, turn log,
    announced boss signature moves, reinforcement warnings.
  - **Budget:** ranks minion, standard and boss; target table, guards,
    `earliestRoomLevel`, cost, budget and depth %.
  - **Generation:** templates Picket, Pack, Line and Bow (Pack and Line
    and Bow fill the lair), Boss Court (boss alone); the generator and its
    fallback; room levels and homeRooms (6.3); the computed-threat
    validator (6.5).
  - **Rewards and rules:** experience on kill with shares and level
    scaling; issue gear, shared records, the one-die rule; the boss's
    signature item and at most 1 carried weapon or armour notable on the
    floor (M1's own channel split, 15.3); basic loot summary; the floor
    loot budget; `readyAt`, `turnsTaken` and kit caps; the morale **data
    fields only**; guile derived (unused until M2).
- **M2:**
  - **Types and ranks:** roles skirmisher, controller, leader and lurker;
    elite rank and promotion; the M2 affixes; level shifts and tints;
    themes (8 themes of 5–7 types, 56 types, and 15 bosses; the floor-5
    deep overlay retires and returns as Nightmare tier 3 in M3)
    **(change RT-5; run structure design 8.1, 12)**.
  - **AI:** the remaining profiles; Quarry, focus spread, setup, hold and
    leash; the hearing outline.
  - **Intent reading:** guile, the reading rule, Tactician as Perception
    mastery's signature use.
  - **Bosses:** boss actions, escorts, phase options and life-long state.
  - **Generation:** the remaining templates; the optional elite room;
    ambush tells; the deeper joining cap (2 per fight).
  - **Morale:** the 4 morale states, T1–T7, Command's Rally against
    morale, Demand Surrender and Yielded, the end-of-fight rule, drop on
    rout, the floor fact, the `broken` flag, cowing, the cowing guarantee
    and the morale scorer term.
  - **Loot:** carried consumables, stealing, smart loot, duplicate
    rerolls, family caps, salvage, comparisons and swap.
- **Later:** Shaken with T8 and T9; support, summoner and
  thief roles; Later affixes; the Feud template; Disarm seizure; oils;
  cursed carried items; multi-square bosses; committed boss signature moves
  (a scripted area marked at the end of the boss's turn, resolving at its
  next turn start, at most one per boss), pulled to M3 as boss Gambits if
  their gates pass, else kept here (13.2) **(change BR-32; boss rooms
  design 4.14.3; agreed, Q3)**; the hostile runner role (the
  non-hostile Windfall runner is pulled to M3: 7.1, 14.5; **change LP-3;
  loot sources design 4.7**); captives as
  next-camp Pledges, never mid-floor joins **(change CMP-10)**; intent reads feeding companion suggestions; **the rival
  adventuring party (stretch goal, section 18)**.

Content volume: M1 = 3 types and 1 boss. M2 = 8 themes of 5–7 types (56
types) and 15 bosses, with the pre-authorised Cut 1 and Cut 2 as the
lever; the floor-5 deep overlay retires (Nightmare tier 3, M3). **(change
RT-5; client agreed 2026-11-21, Q4; run structure design 8.1, 12)**

## 22. Risks

- **Cost calibration.** Minions against a solo hero are worth more than ⅓
  of a standard, because the solo hero has no areas; the solo cap of 2 is
  the guard. The single-target threat validator cannot price minions (HP
  1), so minions are validated by their rank rule instead (6.5). The elite
  ×2 rests on the square-law estimate. Mitigation: cost-residual and per-size
  HP-equivalent reports before content scales.
- **Morale is too strong or too weak.** Cascades could trivialise fights,
  or break lines could never be reached. Mitigation: the Pack and
  leader-template resolution report (under 10% before round 2), the Intimidation-hero report, and
  edge-triggering.
- **Intimidation becomes the best way to skip fights.** Every fresh patrol
  is cowable at 75% of the guard-post line. Mitigation: one attempt per
  group, failure starts a fight, and the item is forfeited. Watch cow
  success per floor, and tighten to 60% if needed.
- **Intent reading is too strong or too weak.** Guile and the party's
  reading score both grow with proficiency (13.3), so the readable share
  depends on monster Wisdom, the role adjustments and how much the party
  invests in Perception. Mitigation: the readable-share gate (20.3; about
  40% untrained, 75% trained, 95% with mastery); guile adjustments and
  monster Wisdom are the levers within the agreed formula. A read that often differs from
  the action taken (because party actions intervene) may feel like a lie;
  the hover's "if its turn began now" wording and the turn log are the
  guard.
- **Live reads cost time.** Reads run the real AI. Mitigation: cache per
  state, recompute only after a command, and count against the p95 budget.
- **Chain-pulls under permadeath.** Mitigation: the cluster budget,
  joining caps, the warning at least a round ahead, and the hearing
  outline. Watch deaths by joiners and chain-pulls.
- **The loot may feel stingy** to players who expect to strip every body.
  A deliberate trade against clutter. Mitigation: item quality (no junk,
  smart loot), the glint and "Carries" telegraph, and playtest sentiment.
- **Plate overload at isometric zoom.** Mitigation: the plate contract, and
  a worst-case UI test per floor.
- **AI quality and cost.** A one-step scorer with many terms can stall or
  behave oddly. Mitigation: golden tests per profile, a stall detector,
  read accuracy, and p95 under 50 ms.
- **Boss reset rule.** Regaining HP to the phase top may feel cheap, or
  still be exploitable. Mitigation: the re-engagement report. Both
  alternatives (sealing the arena, no reset) are known and worse.
- **Experience level scaling** may feel opaque. Mitigation: show the
  adjustment in the loot summary. Catch-up should fire in under 20% of
  runs.
- **Dependencies.** Items (k, records, coin) and generation (anchors,
  routes, tells) are not yet designed. **(change DH-31)**
  The guards and loot numbers are provisional until they are.
- **Precedent.** The morale-state budget raise could invite more
  relabelling. Mitigation: a hard cap of 4 on the morale class, recorded in
  conditions 16.
- **M2 content volume.** 56 types and 15 bosses, plus affixes, elites
  and eight themes, is the largest M2 content cost. Cut 1 and Cut 2 are
  the lever **(change RT-5; run structure design 12)**.
- **The rival party** could pull scope forward. It stays Later and behind
  its prerequisites (18.1).
- **Protected content.** All content is original or SRD 5.1/5.2-shaped
  (CC-BY-4.0, attributed), for example the SRD Pack Tactics trait and
  SRD-shaped minion ideas. The DMG's challenge-rating maths, encounter
  multiplier and optional morale rule are not used. Other games are cited
  as mechanism references only. Names that match other games (Tactician,
  affix names) are checked before shipping.

## 23. Left to the builders

- All numbers: the target table, guard thresholds within their shape, cost
  table, band and depth percentages, profile weights, noise radii, pursuit
  rounds, guile adjustments within their sign, salvage, experience curve,
  loot counts.
- The trait list, affix names and texts, and each theme's roster within
  the budgets.
- Enemy names, models, lore and the visual faction per theme.
- Plate, hover, loot summary and intent-line layout within 5.6 and 13.
- Which abilities each boss flags as `signature`, within 1–2.
- The JSON schema, built by the core developers with the content designer.
- The final name of Tactician.

## 24. Changes to approved documents

Applied on 2026-11-16, when this document was approved. Each change names
the exact section it touches.

**Main design (2026-11-15):**

| # | Section | Change |
|---|---|---|
| M-1 | 4.2, bullet "The run ends when the hero dies. A companion reduced to zero hit points is downed…" | A fight ended by the morale rule (enemies 14.6) counts as a win for getting up |
| M-2 | 4.3, bullet "An enemy notices the party when it has line of sight and wins a perception check…" | Each unaware group rolls once per trigger with its best member in line of sight. Triggers: first line of sight, 6- and 3-square bands, noise, attack. The party notices non-hiding enemies automatically on line of sight; hiding enemies roll Stealth against party passive perception (enemies 12.2). |
| M-3 | 4.3, bullet "Enemies within hearing range of a fight join it. Combat ends when no hostile unit is aware…" | Joiners move at their speed, are announced at least 1 round before arrival (arrival is delayed if needed to keep the warning), enter at the next round start, and are capped per fight (1 on floors 1–2, 2 deeper); groups beyond the cap do not change state. Combat also ends as a win when every hostile still in the fight is Routed (enemies 12.4, 14.6). |
| M-4 | 4.6, bullet "Enemies are placed using a difficulty budget that grows with depth" | Budget = party weight × band% × depth% × ladder% (enemies 10.1) |
| M-5 | 4.6, bullet "Loot comes from tables weighted by depth and rarity" | Notable loot is a fixed per-floor budget, split by channel and pre-rolled on the loot stream. Enemies carry part of it and never add to it (enemies 15.3). |

**Stat model (2026-11-16):**

| # | Section | Change |
|---|---|---|
| S-1 | 6, Proficiency bonus row | +2 at levels 0–4, +3 at 5–8, +4 at 9–12 (levels 0, 11 and 12 are monster-only) |
| S-2 | 6, Speed row | "Changed only by species traits, monster role or traits, armour and conditions"; affixes are traits |
| S-3 | 6, bullet "Natural 20 and natural 1" | *If adopted by death and healing (section 25):* a critical hit rolls all damage dice twice, for every unit |
| S-4 | 7.1, Intimidation row | In a fight: add Demand Surrender (enemies 14.6). In the dungeon: cow eligibility and the `broken` flag (enemies 14.8). |
| S-5 | 7.1, Stealth row, "steal from unaware enemies" | Define it: only from a Dormant, Stationary or Patrolling carrier; adjacent; no other awake group member has line of sight; any party member's Stealth against the carrier's passive perception (−5 if Dormant, +5 if Keen), with disadvantage against an awake carrier; one try per carrier per floor; failure starts combat with nobody Surprised (enemies 15.5) |
| S-6 | 10, "Noticing" bullet | The proposal is settled with the triggers in M-2 (enemies 12.2) |
| S-7 | 10, "Morale" bullet | Points to enemies section 14 |
| S-8 | 7.1, Perception row, "In a fight" column | Add: read an enemy's intent when the party's best passive Perception is at least its guile (enemies 13.3) |
| S-9 | 5.2, Wisdom row, "In a fight" column | "Divine casting; reading enemy intent (Perception against guile)". Lore (Intelligence) tells what an enemy is; Perception (Wisdom) tells what it is about to do. |
| S-10 | 7.2, "Mastery" bullet; 12, bullet "The signature use for each skill's mastery" | Perception mastery's signature use is fixed as Tactician: readable enemies also show the ability, and reading extends to the next 3 enemies in initiative (enemies 13.4). The other skills' signature uses stay the builders'. |
| S-11 | 6, derived numbers table (new row) | Guile (monsters): 10 + Wisdom + proficiency + role adjustment, for every monster whether or not trained in Perception (enemies 13.3) |
| S-12 | 6, Passive perception row and the bullet "A passive value includes proficiency…" | A passive value adds double proficiency with mastery. The party value is the maximum, over eligible members, of each member's full passive (10 + ability + proficiency or double proficiency + active effects). This also applies to party passive perception against lurkers (enemies 12.2, 13.3). |

**Ability model (2026-11-16):**

| # | Section | Change |
|---|---|---|
| A-1 | 9.3, last bullet (rider strength bands) | Bands are levels 0–4, 5–8 and 9–12 |
| A-2 | 12.1 | New `readyAt` field (default 1), counted in the unit's own turns in the current fight (`turnsTaken`, reset at each fight start). Cooldown minimum 2. Cooldowns reset to ready at every combat end. Party-facing hard-control abilities have `readyAt ≥ 2` and `cooldown ≥ 3` and are never openers (enemies 9.3). |
| A-3 | 12.2, "Boss actions" bullet | N per round = min(1 on floors 1–2 or 2 on floors 3–5, party units). At most one at the end of any unit's turn, never after the boss's own. Never hard control (enemies 17.5). |
| A-4 | 12.2, "Resolve… two or three visible pips per fight" | Pips, phases passed and uses belong to the boss's **life**, not the fight; cooldowns reset at combat end as for every monster. On a reset the boss returns Wary and leashed, with HP raised to the top of its current phase band (enemies 17.5). |
| A-5 | 13, "Knowledge" bullet | "The monster AI reads the true state, **except that a party unit's position is known only through that monster's awareness record**" (enemies 11.2) |
| A-6 | 13, "Open to more terms" bullet | Adds the named data-weighted terms of enemies 11.1 (approach, exposure, opportunity cost, range, target preference, focus spread, Quarry, cohesion, body-block, area spread, hold, leash) |
| A-7 | 20, "Enemies" bullet | Answered: monster cost (enemies 10.1), behaviour after losing sight (12.1), morale (14), experience on resolution (16) |
| A-8 | 8.3, new bullet after "The preview uses only what the party knows" | Intent reading (enemies 13.3) is the one place the display shows the monster AI's own output: gated by guile, computed live from the true state filtered by the awareness record, with no roll and no commitment |
| A-9 | 12.2, new bullet | Each boss flags 1–2 limited abilities as `signature`. They are listed from the boss's first turn and, whenever the boss would use one if its turn began now, announced to everyone with target or area. A forecast, not a wind-up: the deferral in 5 stands (enemies 13.2). |
| A-10 | 7.2, Shove or grapple row; new Search row | Shove and grapple cannot target a unit more than one size larger than the user. New row: **Search**, an action: Perception against the passive stealth of hidden units within line of sight (monsters use it after losing the party; Keen gives advantage) (enemies 5.1, 11.1, 12.1). |

**Conditions design (2026-11-16):**

| # | Section | Change |
|---|---|---|
| C-1 | 7.2, "Depth gates" bullet | "Elite" means `rank: elite` on the composed source unit, read at application |
| C-2 | 16, bullet "At most 2 conditions per enemy type" | The counting rule of enemies 9.2. Bosses: at most 2 per phase kit, 3 across phases. |
| C-3 | 16, first bullet (32 conditions) | Add a separate budget of **4 morale states** (Emboldened, Demoralised, Routed, Cowed), hard-capped, enemy-only, shown as a frame tint or banner |
| C-4 | 16, theme bullet | Implicit rider conditions of every damage type the theme's monsters deal count toward the 6; the fire rider's Burning and Smouldering count as 2 |
| C-5 | 18.1, waves table and its Later count; 21, M2 bullet; 25, Enemies bullet | The four morale states move from Later to M2, in the new morale budget (C-3): a new morale row in 18.1 and the Later count from 73 to 69; the 21 M2 bullet gains the morale states; the 25 "Enemies: morale" dependency is resolved by enemies 14 |
| C-6 | 5.7, 6.6, 7.1 Hard row, 9.2 | Morale states are not in the hard tier, never count as a lost turn start, are not blocked by Steeled or hard exclusivity, and are applied by the morale engine outside pipeline steps 2–7. The Frighten of T5 and T6 still passes through the pipeline as Frightened; Routed is substituted only at step 9 (C-12). |
| C-7 | 18.8, Demoralised | Sources become T1–T4, cost-weighted. Loss triggers skip Emboldened members; fearless members are never Demoralised. Edge-triggered. The rout line is set by the member's morale profile: ½ HP for `normal` and `skittish`, ¼ for `steady`, immediate for minions Demoralised by T1 or T2. Wave M2. |
| C-8 | 18.8, Routed | It throws down its notable item, makes no opportunity attacks, and leaves the floor without warning anyone or returning. A Rally removes its cost from losses. Wave M2. |
| C-9 | 18.8, Rallied and Emboldened | An enemy leader's Rally includes itself and keeps the settled once-per-ally-per-fight limit. A Frightened leader cannot Rally. Emboldened covers only the leader's own group; it also shields members from the loss triggers T2 and T3; its source is the leader's aura (Command). Wave M2. |
| C-10 | 18.8, Shaken, first source | Minion deaths do not trigger it |
| C-11 | 18.8, Cowed | Eligibility and the `broken` flag (enemies 14.8). Experience is paid once per member. Wave M2. |
| C-12 | 9.2, step 9; 18.8, Routed | **Not a transform row.** A morale-engine substitution: when a Frighten or a limited fear effect would be applied at step 9 to a Demoralised enemy that is not Emboldened, the condition is discarded (no `onApply`; a step 6 transform it took part in is not applied and its source is not consumed) and the morale engine applies Routed. Demoralised is not consumed. Routed wins over the Frightened + limited fear → Panicked transform (enemies 14.3). |
| C-13 | 12.1, `morale` row | `fearless` is a morale profile read by the engine, not a tag immunity. `mindless` implies `fearless`. |
| C-14 | 18.9, Warded and Death-warded | Monster affix grants are `oncePerLife` |
| C-15 | 18.11, Quarry row | Defined in enemies 11.4 as a no-effect AI focus telegraph |
| C-16 | 8, exemption sentence ("morale states (Demoralised, Routed)") | Covers all four morale states |
| C-17 | 18.3, Disarmed (Later) | Issue weapons cannot be taken. A dropped notable weapon may be seized with the free interaction, becomes party loot and does not return at combat end. The owner uses its `improvised` attack. |
| C-18 | 18.8, Emboldened interactions; 19, the `onEnd` row "Emboldened (`sourceGone`, enemy groups) → Demoralised" | Remove the relation. Emboldened ends with no `onEnd`; the morale engine's T1 Demoralises the non-fearless members when the leader falls, whether it dies or is Routed, Yielded or Fled (enemies 14.3). |

No change is needed to the ability model's rejection of telegraphed
wind-ups (its section 5): announced signature moves are live forecasts, not
committed wind-ups (13.2).

## 25. Depends on systems not yet designed

- **Items and inventory:** the generic layer is no longer pending: see the
  items and inventory design (2026-11-16; its 16 answers 15.9). What
  remains open moves to its category designs (items design 17): starting
  kits, Coated, Disarmed and Clinging.
  **(change I-15)** Weapons and foci are no longer pending: see the weapons
  and foci design (2026-11-17). Disarmed and Coated stay Later; starting
  kits remain open in their category designs. **(change W-15)** Armour and shields are no
  longer pending: see the armour and shields design (2026-11-17), which
  sets armour values, Strength thresholds, natural armour and monster
  loadouts (its 12) and Pitted Armour (Later, its 9.4). **(change AR-29)**
  Consumables, scrolls and wands are settled in the consumables design
  (2026-11-17); coatings and Coated stay Later. **(change CN-26)**
- **Death and healing** (the guards and AI depend on these): settled in
  the death and healing design (2026-11-17); every row below is answered
  there (its 25.1). **(change DH-31)**

| Need | Why | Answer (death and healing design) |
|---|---|---|
| Hero HP at level 1 of about 18 (the proposed flat bonus) | Or the floor 1 table rows must shrink | 18 for the reference hero (tier 6, Con +1): 11 + 7; the bonus persists at every level, so the 6.1 table matches; lowest legal 16, covered by new 6.4 rows (7, 21.1) |
| **A crit damage rule.** Recommended: SRD double dice, applied to everyone (S-3). | Every guard is computed with it | Adopted: double dice for every unit, attack rolls only (9) |
| Confirmation that the hero has no downed state | Boss and encounter guards read "reduced to 0" | Confirmed: 0 HP ends the run (10) |
| Whether damage on a downed companion kills it | The AI invariant "never target or aim areas at downed units" is required either way | No: a Downed unit is inert; invariant 2 is now also a core rule (11.4) |
| A morale win counts as a win | For getting up (M-1) | Yes (11.2) |
| **The Reprieve Phial (talisman) needs** (consumables design 24): its final leave-HP; whether it fires out of combat; the owner order of Death-warded, Last Ward, Last-Breath Charm and the phial; whether companions can die (then the phial also covers "would die") **(change CN-21)** | The save runs in the damage pipeline before HP is lowered; the 6.4 guards are computed without it (consumables design 14.4, 14.9) | Leave-HP 334‰; fires from any source, but the floors come first; owner the death and healing design, order Death-warded → phial, Last Ward and Last-Breath Charm retired; companions never die, no would-die coverage (15, 16, 11.6) |

- **Dungeon generation** (the generator content test asserts each):

| Requirement | Consumers | Wave |
|---|---|---|
| Expected party level per room along the critical path, in tenths | Budget, room level, experience scaling | M1 |
| Floor plan slots, with the elite room off the path and visible before commitment | Encounters, loot | M1 / M2 |
| Path-distance hearing through doors (−3 per closed door); locked doors block monster paths | Joining, cluster budget | M1 |
| Patrol loops that avoid the entry room and stay clear of main-path rooms 1 and 2 for the first 20 ticks **(change DH-28)**; answered by the Circuit and Circuit isolation (floor layout design 6.7) **(change FL-10)** | Awareness | M1 |
| Retreat anchors, ranked per (fight host, party-entry Portal), non-door paths first, with the runtime unexplored filter and fallbacks; a non-door `fleeExit` per fight host, never repaired into a Door; `PartyEscape` and the LG20 entry landing (floor layout design 6.2, 6.8) **(change FL-12)** | Routed | M2 (the flee exit M1) |
| Exactly one route-avoidable fight, a guard post (the Bypass; floor layout design 6.8) **(change FL-9)** | Stealth guarantee | M1 |
| A cover room on at least one patrol route; answered by the Circuit's `coverRoom` (floor layout design 6.7) **(change FL-10)** | Party ambush | M1 |
| Ambush tells within 6 squares; lurker placement; answered by lurker geometry and `TellSight` (encounter placement design 6.8.3) | Ambush | M2 |
| Containers and lair caches for the loot channels | Loot | M1 |
| Boss arena templates: at least 2 entrances, line-of-sight blockers, a counterplay feature | Bosses | M1 |
| Hazard placement within theme budgets | Conditions 11 | M2 |
| When laying out the main path, reserve at least 2 containers openable with no check in main-path rooms before the boss room, one in the first half; a run-time assertion and one retry with the dungeon stream at seed + 1 (death and healing design 12.4 HE-7) **(change DH-32)** | Healing guarantee | M1 |
| A `mainPathIndex` on main-chain rooms, their Alcoves and the Portals joining consecutive main-path rooms (the lower index; arena-adjacent squares take the arena's), null elsewhere, and an `effectiveIndex` on every square (death and healing design 17.1) **(change DH-32; change FL-13; floor layout design 3.4)** | Floor-1 surprise rule (11.3 invariant 6, 12.2) | M1 |
| Trap ids and a detection snapshot when a trap fires (death and healing design 10.4) **(change DH-32)** | Party trap floor | M1 |

- **Companions:** captives as next-camp Pledges, never mid-floor joins
  (Later, optional; Meet at camp and Send home / Parting Word, companions
  design 7.12) **(change CMP-10)**; the rival party's dependence on
  companion kits (18.1).
- **Classes and offers:** Perception mastery offered through the
  pick-one-of-three pool, so Tactician is reachable in a run (13.4).
  Settled in the classes design (2026-11-17): Perception mastery sits in
  the Shade and Herald offer lanes and the universal draw. **(change CL-13c)**

## 26. Open items for the client

None. Approved by the client on 2026-11-16.
