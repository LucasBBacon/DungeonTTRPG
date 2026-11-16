# Ability and Resource Model: Design

Date: 2026-11-16
Status: awaiting client approval
Parents: `2026-11-15-dungeon-rpg-design.md` (main design) and
`2026-11-16-character-stat-model-design.md` (stat model)

Items marked **(agreed)** were agreed with the client, either directly or by
approving the lead's proposal. Items marked **(lead's call)** were filled in
by the design lead and are confirmed when this document is approved. Items
marked **(settled)** restate a rule from a parent document. Once approved,
every item here is binding on the builders.

## 1. Intent

This model defines what characters can do beyond moving and attacking, what
limits it, and when those limits reset. It is the layer that turns the main
design's rest rules into decisions: no full rest inside a floor, and short
rests limited by a consumable supply. The purpose of both, in the main
design's words, is to make resource use across a floor a real decision.
**(settled)**

The model should feel like a tabletop sheet (spell slots, per-rest features)
without porting 5E, and stay fast to read when one player runs up to four
units in initiative order. **(agreed)**

## 2. Goals, non-goals, anti-goals

### Goals

- Attrition costs the party but rarely decides the run. A careful party
  reaches the boss with about half its camp resources. Bosses are tuned so
  that half is enough with good play, and a full party has an easier time.
  A party that wasted its resources arrives in real danger but has ways out:
  falling back to break line of sight, a supply bought at camp, fights
  skipped with Stealth or Intimidation. **(agreed)**
- Every class feels attrition, not only casters. **(agreed)**
- A drained unit is slowed, never helpless. **(agreed)**
- A companion turn takes under 10 seconds. **(agreed as a playtest target)**
- One resource mechanism in the core, so that a switch to point pools later
  would not need a rebuild. **(agreed)**

### Non-goals

- 5E's spell list, spell levels 1–9, rituals, daily preparation.
- Terrain surfaces and persistent ground zones. Held back by the stat model.
  **(settled)**
- Counter-casting and readied actions at launch. **(lead's call)**
- Lair actions. **(lead's call)**

### Anti-goals

- **Nova turns.** A unit spending everything in one turn to trivialise a
  fight, the boss in particular.
- **Free refills.** Any route that restores resources without the costs the
  rest rules impose: level-ups, fleeing and re-engaging, swapping items.
- **Bookkeeping.** A number the player must track that the game could track
  for them.
- **A guaranteed safe rest.** It would remove the decision this model exists
  for. **(agreed)**
- **Protected content.** Ability names and mechanics come from the SRD 5.1
  or 5.2 with attribution, or are original. **(settled)**

## 3. Constraints

From the parents:

- The turn is movement, one action, one bonus action, one reaction.
  **(settled)**
- Reactions resolve automatically by default; the player can set any
  reaction to "ask me". **(settled)**
- Concentration is a Constitution check. **(settled)**
- Riders are only attempted when an ability or weapon carries one, or on a
  critical hit; a weak target gets the rider automatically. **(settled)**
- Integer arithmetic, rounding down; deterministic; nothing derived is
  stored; rules in the pure C# core; content in data. **(settled)**
- 8-direction square grid; a diagonal step costs 1; symmetric visibility.
  **(settled)**
- Levels 1–10 over 4–5 floors; a level-up about every 8–10 minutes.
  **(settled)**

## 4. Approaches considered

Four independent drafts were scored by three reviewers (game design,
engineering, player experience). They reduced to three approaches.

### A. Spell slots and named charges (chosen)

Casters spend tiered spell slots that refill at camp. Martial classes spend
small named pools that refill on a short rest or at camp. Everyone has
at-will attacks or cantrips.

- Gains: most recognisable to tabletop players; slots combine cleanly for
  multiclassing; its pool mechanism is general enough to express B and C.
- Costs: the heaviest hero sheet; inherits 5E's imbalance between classes
  that refill on rests and classes that refill at camp.

### B. Two pools per unit, one per rest and one per floor

Every unit has a small pool refilled on a short rest and a larger pool
refilled only at camp, plus one power per fight.

- Gains: the boss is the payoff of the floor by construction.
- Costs: five layers of resource to learn, learnt by losing a boss under
  permadeath; class identity reduced to pool sizes.

### C. One point pool per unit

One bar per unit; abilities cost points by tier; a short rest restores half.

- Gains: lightest to read; smallest first milestone.
- Costs: reads as a mana bar; class identity shrinks to a few numbers;
  martial classes barely feel attrition.

### Decision

The client chose **A**, with ideas grafted from B and C (section 5). It keeps
the tabletop sheet the stat model chose its abilities to protect. A's
weaknesses are fixed by the grafts; C's are built into it. Because the pool
mechanism is general, a later switch to points would not need a new core.

## 5. Ideas grafted from B and C

All **(agreed)**. Each is specified in the section named.

From B:

- Every class has at least one rest pool and one camp pool (6.3).
- At most one camp-pool spend per unit per turn (6.4).

From C:

- Monster abilities recharge on fixed countdowns, never on dice (12.1).
- A greyed-out button always shows the one reason it cannot be used (15.3).
- Level-up cards show exactly what changes (10.3).
- Auras and fight-length effects attach to units, never to patches of
  ground (8.4).

The lead also takes these smaller ideas from the drafts. All **(lead's
call)**:

- From B: boss design rules (12.2); simulator spending policies (16); each
  companion has exactly one signature camp ability (11.1).
- From C: one AI scoring formula in integers (13); every unit has at least
  two at-wills (6.1); offers priced in one value unit (10.2); rider strength
  in one table per type (9.3); the "while the pool is at least half full"
  reaction policy (7.3); lines in the 8 directions only (8.1).

Rejected from the drafts **(lead's call)**: a guaranteed safe room before the
boss; companion turns played automatically; in-fight resource income;
player cooldowns; telegraphed monster wind-ups (deferred: the AI scorer
cannot reason about them yet); upgrades written as data patches instead of
effects.

## 6. Abilities and resources

### 6.1 Kinds of ability

**(lead's call)**

| Kind | Limited by | Examples |
|---|---|---|
| At-will | Action cost only | Weapon attack, cantrip, Shove, Hide |
| Limited | One pool: a slot tier, a named charge pool or an item stack | A tier 2 spell, Surge, a potion |
| Passive | Nothing: a modifier or trigger in the effects system | Extra Attack; once-per-turn bonus damage |
| Toggle | Switched on or off once per turn, as a free interaction; stays on across turns and fights until switched off | A reckless stance |
| Sustained | Concentration, or its own stated end | A restraining spell; a rage-style buff |
| Reaction | The round's reaction, plus any pool cost | Opportunity attack, a protective ward |

- Every unit has at least two at-wills, one of which deals damage. Content
  validation enforces this. **(lead's call)**
- Sources: the class table (subclass at level 3), universal actions (7.2),
  items, pick-one-of-three offers, the companion's fixed kit, the monster's
  stat block. Species give traits only. **(settled)**

### 6.2 The pool rule

**(lead's call)**

A **pool** is any limited count: a slot tier, a named charge pool, a stack
of consumables.

- Its maximum is derived on demand from level, class, items and effects.
- The state stores only how much of it has been spent.
- Current = maximum − spent, never below zero. Losing an item that raised a
  maximum cannot push a pool negative.
- Every pool has exactly one refill tag:

| Tag | Spent count reset to zero at | Used for |
|---|---|---|
| `turn` | The unit's turn start | Once-per-turn passives |
| `round` | The unit's turn start | The reaction |
| `fight` | Combat start | Minor uses only, such as Rally once per ally. No major power is per-fight. |
| `rest` | A completed short rest, and camp | Martial charges, Second Wind, caster rest recovery |
| `camp` | Camp only | Spell slots, Surge, signature abilities, wand charges |
| `never` | Never | Consumables |

- There are no player cooldowns. Exploration ticks never refill a pool.
- A level-up never refills anything. The spent count is unchanged, so only
  the increase in a maximum arrives ready to use.

### 6.3 Both clocks for every class

**(agreed)**

Every class has at least one `rest` pool and at least one `camp` pool.
Each class should draw roughly 40–60% of its limited power from camp pools
**(lead's call, checked by the simulator)**. A change to the supply rate
then moves every class, not half of them.

### 6.4 One camp spend per turn

**(agreed)**

A unit may spend from a `camp` pool at most once per turn. A reaction taken
on another unit's turn counts against that other turn's allowance, not the
reacting unit's own. **(lead's call)** This caps the nova turn without
banning big turns.

### 6.5 Spell slots

**(lead's call)**

- Four tiers. A full caster's slots by level (tier 1 / 2 / 3 / 4):

| Level | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|---|---|---|---|---|---|---|---|---|---|---|
| Slots | 2 | 3 | 3/1 | 3/2 | 3/2/1 | 3/2/2 | 3/2/2/1 | 3/3/2/1 | 3/3/2/2 | 3/3/3/2 |

  Values are illustrative; the shape is a new tier on each odd level from 3.
- Slots are `camp` pools, one pool per tier.
- Every caster also has a `rest` pool, **Recovery**, of 1. On a completed
  short rest it regains one spent slot of tier 2 or lower, the highest such
  tier spent. There is no choice to make.
- Spells are known, not prepared. A caster knows about seven spells by
  level 10 and may swap one at camp.
- A spell can be cast with a higher slot only if its data declares what
  that adds. The lowest slot that works is the default.
- No half casters or pact casters at launch. When multiclassing arrives,
  slots combine through a caster-level table, which is data.

### 6.6 Martial example

**(lead's call; illustrative, to show the shape for builders)**

A Strength fighter:

- **Second Wind:** bonus action, heal 1d10 + level. `rest` pool of 1, 2 from
  level 6.
- **Techniques:** `rest` pool of 2, 3 at level 5, 4 at level 9. One is spent
  after a hit is known and before damage is rolled, so it is never wasted on
  a miss. It makes that hit carry the weapon's rider, push the target 1
  square, or add 1d6 damage.
- **Surge:** `camp` pool of 1, 2 at level 9. One extra action this turn.

A martial class has at most two named pools besides universal ones.
**(lead's call)**

### 6.7 Capstone

At level 9 or 10 each class gains one signature ability with a `camp` pool
of 1, in place of a fifth spell tier. **(lead's call)**

## 7. Action costs

### 7.1 Declared cost

Every ability declares exactly one cost: `action`, `bonus`, `reaction`,
`move:N` (spends N squares of movement), or `free`. A unit gets one free
interaction per turn. **(lead's call)**

### 7.2 Universal and skill actions

**(lead's call unless marked)**

| Use | Cost |
|---|---|
| Attack (Extra Attack at level 5 for martial classes) | Action |
| Dash, Disengage, Dodge, Help | Action |
| Shove or grapple: Athletics against 10 + Fortitude **(settled)** | Replaces one attack |
| Slip past an enemy: Acrobatics against 10 + Reflex **(settled)** | Part of movement, once per turn |
| Escape a grapple | Action |
| Hide: Stealth | Action; a bonus action with a class feature |
| Frighten: Intimidation against 10 + Will **(settled)** | Action |
| Rally: Leadership | Bonus action, once per ally per fight |
| Study an enemy: Lore | Bonus action, once per enemy type per fight |
| Recognise a spell being cast: Arcana | No action; a threshold with no roll |
| Get a downed companion up: Medicine, trained only **(settled)** | Action |
| Use a trap, or a scroll or wand outside your class, trained only **(settled)** | Action. A failed check uses the scroll or charge up: no free retries **(settled)**. |
| Drink a potion | Bonus action |
| Switch the wielded weapon | Bonus action |
| Opportunity attack | Reaction |

Switching weapons costs a bonus action so that a spare weapon is a fallback,
not a key carried for each enemy. **(lead's call, from the stat model's
anti-goal)**

### 7.3 Reaction policies

- Every reaction resolves automatically by default; the player can set any
  reaction to "ask me". **(settled)**
- Each reaction's data names its automatic policy **(lead's call)**:
  - `always`;
  - `ifItChangesOutcome`, such as a ward that turns this hit into a miss
    (the core knows the roll);
  - `whilePoolAtLeastHalf`, so that automatic reactions cannot quietly
    drain a pool;
  - `never`, for reactions only worth taking by choice.
- The player can change any reaction's policy, including to "ask me".
  **(settled)**

## 8. Targeting and areas

### 8.1 Shapes

**(lead's call)**

Distance is the larger of the two grid offsets, which follows from a
diagonal step costing 1.

| Shape | Covers | Sizes |
|---|---|---|
| Single or several targets | Units within range R with line of sight | Cantrips about 12; spells 6–12 |
| Burst, radius r | Every square within distance r of a point | r1 = 3×3, r2 = 5×5 |
| Aura, radius r | A burst centred on a unit, moving with it | |
| Cone, length L | Measured from the user in one of the 8 directions. Orthogonal: squares 1..L forward, no further sideways than forward. Diagonal: both offsets 0..L, not the user's own square. | (L+1)² − 1 squares either way: L2 = 8, L3 = 15 |
| Line, length L | 1 square wide in one of the 8 directions, stopping at the first wall | L squares |

- Orthogonal and diagonal cones of the same length cover the same number of
  squares, so aiming diagonally is never a trick.
- An area includes a square only if the area's origin can see it, using the
  symmetric visibility rule. **(settled rule, applied here)**
- Squares are enumerated in a fixed order (row by row), for determinism.
- No area covers more than 25 squares at levels 1–6. Content validation
  enforces this.

### 8.2 Friendly fire

Areas hit allies unless the ability is tagged `selective`. At-wills never
hit allies. **(lead's call)**

### 8.3 Preview

The core answers "what would this do?" for any ability and target: which
units are affected, the odds for each, expected damage, and any rider
chance. The display computes nothing. **(settled principle)**

### 8.4 No ground zones

**(agreed)**

Sustained areas are auras that move with a unit, or conditions placed on
units. Nothing is left on the ground. This stays clear of the terrain
non-goal.

## 9. Riders and damage

### 9.1 Damage parts

- Damage is a list of typed parts, each resolved separately. **(settled)**
- A part has a type, dice, a flat amount and a `rider` flag. The flag is the
  exact meaning of "carries a rider". **(lead's call)**
- A target weak to a part's type gets that type's rider automatically.
  **(settled)**
- A critical hit attempts the rider of each damaging part that does not
  already carry one, subject to the bounds below. **(lead's call)**

### 9.2 Rider saves

- A rider's save uses the save the builders assign to that rider type,
  against the attacker's ability difficulty. **(settled)**
- It is a separate roll from the ability's own save. Failing an ability's
  Reflex save against a fireball does not decide whether the target catches
  fire. **(lead's call)**

### 9.3 Bounds on save noise

**(lead's call)**

- At most one rider attempt per target per ability use: the first flagged
  part in data order.
- An at-will's rider is attempted at most once per turn per attacker.
- Damage dealt by a rider never triggers another rider.
- A rider is not attempted on a target that already has that condition.
- Rider strength is one table per damage type, indexed by proficiency band
  (levels 1–4, 5–8, 9–10). The strengths themselves are the builders'.
  **(settled that they are the builders')**

### 9.4 Where riders come from

**(lead's call)**

- At-wills rarely carry a rider.
- Limited abilities usually do.
- Martial classes attach riders on demand, for example by spending a
  Technique.
- Weapons found as loot can carry riders as a property.

### 9.5 Concentration

- The check is d20 + Constitution **(settled)**, against the higher of 10
  and half the damage taken **(lead's call)**.
- One check per damage event. The parts of one hit are summed first.
  **(lead's call)**
- Proficiency is added only from a feature or an offer. **(lead's call)**
- A unit concentrates on one effect at a time. Starting another ends the
  first. **(lead's call)**
- Concentration ends when the unit is downed or incapacitated, when a
  psychic rider lands on it **(settled)**, when its duration ends, or when
  the player ends it as a free interaction. **(lead's call)**

### 9.6 Durations

**(lead's call)**

- An effect lasts `rounds:N`, `untilRest`, `untilCamp`, or until a stated
  trigger.
- In combat, a round counts down at the end of the source unit's turn.
- Out of combat, one round is one tick, so a buff of 10 rounds lasts 10
  steps. The main design counts durations in ticks; this fixes the ratio.
- An effect records the source's ability difficulty when it is applied, so
  repeat saves are unchanged if the source dies or loses a buff.

## 10. Scaling and offers

### 10.1 Pacing

**(lead's call; levels per floor are illustrative)**

| Floor | Levels | Power beats |
|---|---|---|
| 1 | 1→3 | Tier 1 spells; tier 2 and subclass at level 3 |
| 2 | 3→5 | +1 ability at level 4 **(settled)**; Extra Attack, tier 3, stronger cantrips at level 5 |
| 3 | 5→7 | Tier 4 at level 7 |
| 4 | 7→9 | +1 ability at level 8 **(settled)**; signature camp ability at level 9 |
| 5 | 9→10 | Capstone pick |

Proficiency raises attack and ability difficulty for everything at once.
**(settled)**

Level-ups mid-floor add only each pool's increase (6.2). The slot table is
budgeted for about two fresh uses per floor from levelling; builders count
that when setting numbers.

### 10.2 Offers

- Where the class table has no choice, a pick-one-of-three offer fills the
  level. **(settled)**
- Offer kinds **(lead's call unless marked)**:
  - capacity: +1 to a pool;
  - cost: an action ability becomes a bonus action, once per rest;
  - rider: an ability's damage part gains the rider flag;
  - shape: +1 radius, or the `selective` tag;
  - concentration training;
  - a damage type that ignores resistance **(settled, stat model 8.4)**;
  - skill mastery **(settled)**;
  - timescale: one `camp` pool becomes a `rest` pool. At most one per run.
- Every offer declares `requires` tags (what build it suits) and a value in
  a single unit, about one action's worth of effect. The offer draw filters
  by both. **(agreed: one value unit)**
- An upgrade is an effect, filtered by ability ID or tag. Upgrades are never
  data patches. **(lead's call)**
- The hero has at most eight limited abilities. Past that, offers only
  upgrade existing ones. **(lead's call)**

### 10.3 Level-up cards

**(agreed)**

Each option shows what changes, computed by the core: "Cleave: 2 → 3 uses",
"Ember Burst: radius 1 → 2". The player never has to work it out.

## 11. Companions

### 11.1 Kit

**(lead's call)**

- Two at-wills, up to three limited abilities, one reaction, and passives.
- Limited abilities read as one line each: "2 per rest", "1 per camp".
- Exactly one signature `camp` ability per companion.
- The same pools, concentration and riders as the hero.

### 11.2 Progression

- Automatic, with a choice at a few key levels. **(settled)**
- The choice is one of two or three kit upgrades, at levels 3, 6 and 9.
  **(lead's call)**
- A recruit joins at camp with full pools. **(lead's call)**

### 11.3 Suggested actions

**(agreed)**

- A setting turns on a suggested action for companions. It is off by
  default.
- With it on, the game highlights the action it would take for the active
  companion, and one key accepts it. The player can always do something
  else.
- Nothing is ever taken without the player. The hero never gets
  suggestions.
- The suggestion comes from the party scorer (section 13), the same one the
  simulator uses. A poor suggestion is visible to players, so its quality is
  a release requirement.

### 11.4 Other measures against fatigue

**(lead's call)**

- Toggles stay on until switched off.
- Reactions resolve on their automatic policies.
- A short rest is one screen with one confirmation.
- Each pool is shown as pips with an icon for its refill tag.
- A party bar shows the supply count.
- Playtest target: under 10 seconds per companion turn, and at most four
  things to read per unit.

## 12. Monster abilities

### 12.1 Limits

**(agreed: fixed countdowns; rest lead's call)**

- At will.
- `uses: N`: a count for the monster's whole life, never refilled, not even
  when a new fight starts. Fleeing and coming back gains the party nothing
  and costs the monster nothing.
- `cooldown: N`: usable again N rounds after use, counted at the monster's
  turn start. No recharge dice.
- Lore reveals uses left and cooldowns ("ready in 2"). Anything already seen
  in play is visible to everyone. **(settled)**

### 12.2 Bosses

**(lead's call)**

- Boss fights are designed for 5–7 rounds, so a party's per-rest abilities
  run out and saved camp resources matter.
- Boss actions: a boss may act at the end of other units' turns, N times
  per round.
- Resolve: two or three visible pips per fight, each turning a failed save
  into a success.
- Phases triggered at hit point thresholds.
- Every boss has at least one way to deal with sustained effects, such as
  breaking concentration or shedding a condition.
- Tuning target: a party with about half its camp resources wins with good
  play. **(agreed)**

## 13. AI

**(lead's call)**

One scoring formula, integers only, chances counted out of 20:

```
score = expected damage (capped at the target's hit points)
      + control value × fail chance / 20
      + kill bonus
      − friendly fire
      − limited cost
```

- **Monsters:** limited cost = best at-will score × thrift / 100. Thrift is
  an integer percentage in the monster's data, default 150. The cost drops
  to zero on a turn the data marks as an opener, at or below half hit
  points, or when the uses left are at least the turns the fight is
  expected to last. An ability off cooldown costs nothing to use.
- **Party** (the simulator, and suggestions): limited cost = best at-will
  score × pressure / 100, where pressure compares what the rest of the floor
  is expected to need with what the party has left, including rests its
  supply allows. It is clamped to 0–400. A reserve is held back for the
  boss.
- Ties break by data order, then by unit order.
- The AI never rolls dice. Odds come from the same queries as the preview.

## 14. Data and the core

**(lead's call)**

### 14.1 Data shape

Illustrative:

```json
{ "id": "pool.surge", "max": { "byLevel": [1,1,1,1,1,1,1,1,2,2] }, "refill": "camp" }
```

```json
{ "id": "ability.ember_burst", "cost": "action",
  "resource": { "slot": { "minTier": 2, "higherTier": { "addDice": "1d6" } } },
  "target": { "shape": "burst", "radius": 1, "range": 8, "selective": false },
  "resolve": { "save": "reflex", "onSuccess": "half" },
  "damage": [ { "type": "fire", "dice": "3d6", "rider": true } ],
  "concentration": false, "tags": ["arcane", "fire", "area"],
  "ai": { "minTargets": 2 } }
```

### 14.2 Unit state

- Spent counts in a list sorted by pool ID.
- The turn budget: movement left, action used, bonus used, reaction used,
  camp spend used, free interaction used.
- A concentration link to the effect it sustains.
- Active toggles.
- Monster cooldowns and uses, keyed by ability ID.

### 14.3 Using an ability

`UseAbility` passes through fixed steps, each raising an event in the
effects system:

1. Declared.
2. Validated: cost, pools, the camp-spend rule, range, line of sight. The
   first failure returns one reason code, which the display shows.
3. Cost paid.
4. Targets locked, in unit order.
5. For each target: attack roll or save, then damage (then any
   concentration check), then the rider attempt, then effects applied.
6. Resolved.

Reactions hook in at named points, such as between an attack roll and the
hit being decided.

### 14.4 Rules in the core

- Refills are triggers on turn start, combat start, rest completed and camp
  entered. A level-up needs no refill code.
- Several triggers on one event run in order of priority, then source unit
  order, then effect creation order.
- Every roll uses the combat stream. **(settled)**
- C# hooks referenced by ID are used only for reaction policies and AI
  terms that data cannot express. **(settled mechanism)**

### 14.5 Content validation

The build fails when:

- a pool has other than one refill tag;
- a class lacks a `rest` pool or a `camp` pool;
- a unit has fewer than two at-wills, or none that deals damage;
- an area exceeds 25 squares at levels 1–6;
- a reaction has no automatic policy;
- a companion has other than one signature ability;
- the hero's limited abilities can exceed eight;
- an offer's `requires` tags do not resolve.

### 14.6 Staging

- **M1** (one class, levels 1–3): pools with `rest` and `camp` tags,
  at-wills, the slice class's slots or charges, short rests and supply,
  concentration if the class casts, bursts, cones and lines, monster uses
  and cooldowns, boss resolve, the monster AI.
- **M2:** riders on damage parts, offers, companion kits, boss actions, the
  party scorer, suggestions, simulator spending policies.

## 15. Short rests and supply

### 15.1 Resting

- A short rest can start only out of combat with no hostile unit aware of
  the party. **(settled)**
- It takes 20 ticks, during which the world advances. If a patrol notices
  the party, combat starts, no supply is spent and nothing is gained.
  **(lead's call)**
- There is no guaranteed safe spot. **(agreed)** Before the player confirms,
  the core reports any patrol route already seen that passes within hearing
  range. **(lead's call)**

### 15.2 On completion

**(lead's call unless marked)**

- The party spends 1 supply.
- Each unit regains its Recovery in hit points **(settled)** and its `rest`
  pools reset.
- The best trained Medicine member adds Wisdom + proficiency to every
  unit's Recovery. Medicine mastery's signature use is the builders' to
  choose. **(settled that it is the builders')**

### 15.3 Supply

**(lead's call; numbers illustrative)**

- The run starts with 2 supply. Each camp gives 1. Each floor holds 1–2
  more as loot, and more can be bought at camp.
- The party carries at most 3. How supply uses carrying slots is for the
  items design.
- Target: about two short rests per floor of 4–6 fights plus the boss.

### 15.4 Unusable buttons

**(agreed)**

A button that cannot be used is greyed and names its one reason: no slot,
camp spend used this turn, out of range, no line of sight, and so on. The
reason is the validation code from 14.3.

## 16. Testing

- Unit tests: each refill tag; a level-up leaves spent counts alone; the
  camp-spend rule including reactions; cone and burst squares in all 8
  directions with equal cone sizes; the concentration target and the
  psychic break; every rider bound.
- Property test: the preview always matches the resolution.
- Replay tests across short rests and camp.
- Content validation as in 14.5.
- Simulator reports, each tied to a risk:
  - per-class results at 1, 2 and 3 rests per floor (a large swing means a
    class's split between rest and camp is lopsided);
  - camp resources left at the boss door, and boss win rate against them,
    under three fixed spending policies: spend freely, hold half, hold
    everything (target: about half, and half is enough);
  - resources unspent when reaching camp (high means too generous);
  - each unit's share of damage in ordinary fights (catches casters
    hoarding for the boss);
  - fresh uses gained per floor from levelling;
  - concentration held, by Constitution;
  - saves rolled per round once riders arrive;
  - weapon switches per fight.
- Playtests: level-up time, hero turn time, companion turn time, whether a
  newcomer can read a caster's sheet, how often suggestions are accepted.

## 17. Risks

- **The AI decides the balance.** Simulator results for camp-heavy classes
  are only as good as the party scorer's spending choices. Compare it
  against the fixed spending policies.
- **Pre-buffing at the boss door.** With one round per tick out of combat, a
  party can cast buffs just before a boss. Long buffs need concentration,
  and bosses carry a tool against sustained effects; watch it in playtests.
- **Radius 2 bursts** (25 squares) may be too strong, and square areas must
  read clearly in isometric 3D.
- **Rest interruptions** are luck. If they feel unfair, the patrol preview
  is the lever.
- **Protected content.** Every ability name and mechanic is checked against
  SRD 5.1 and 5.2 before it ships, with attribution.

## 18. Left to the builders

- All numbers: slot counts, pool sizes, dice, ranges, durations, supply
  rates and prices, thrift and pressure values, boss resolve and actions.
- The four launch classes' kits, within the rules above.
- Each rider's save type and strength table.
- Spell and ability lists, names and descriptions.
- The expected-need estimate inside the party scorer.
- Hotbar and sheet layout.

## 19. Changes this makes to the parent documents

To be applied once this document is approved:

- Stat model section 10: the ability and resource model is no longer
  pending; point to this document for skill action costs, which abilities
  carry riders, and casting.
- Main design section 4.3: durations in combat count in rounds; out of
  combat one round is one tick (9.6 here).

## 20. Open items for the client

None at the time of writing.
