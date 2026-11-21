# Ability and Resource Model: Design

Date: 2026-11-16
Status: approved by client, 2026-11-16
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
- Spreading, evolving or player-placed ground surfaces, and persistent
  ground zones. Held back by the stat model. **(settled)** Fixed condition
  squares placed by floor generation are allowed (conditions design 11).
  **(agreed; change C11)**
- Counter-casting and readied actions at launch. **(lead's call)**
- Lair actions. **(lead's call)**

### Anti-goals

- **Nova turns.** A unit spending everything in one turn to trivialise a
  fight, the boss in particular.
- **Free refills.** Any route that restores resources without the costs the
  rest rules impose: level-ups, fleeing and re-engaging, swapping items.
  *Later:* one named exception, a wandering trader's paid Recharge of one
  wand's `camp` pool, once per trader (change I-16, items design 10.9).
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
- Levels 1–10 over 5 floors (run structure design 6.1); a level-up
  about every 8–10 minutes. **(settled)**

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
- A greyed-out button always shows the one reason it cannot be used (15.4).
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
| Toggle | Switched on or off once per turn, without using the turn's free interaction; stays on across turns and fights until switched off | A reckless stance |
| Sustained | Concentration, or its own stated end (short effects only, 9.6) | A restraining spell; a rage-style buff |
| Reaction | The round's reaction, plus any pool cost | Opportunity attack, a protective ward |

- Every unit has at least two at-wills, one of which deals damage. Content
  validation enforces this. **(lead's call)**
- Sources: the class table, with the subclass at level 3 **(lead's call,
  10.1)**; universal actions (7.2); items; pick-one-of-three offers; the
  companion's kit (11.1); the monster's stat block.
- Species give traits, never ability points. **(settled)** A trait is an
  effect bundle and may grant an ability like any other effect. **(lead's
  call)**

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
| `turn` | The unit's turn start | Once-per-turn passives; the reaction |
| `fight` | Combat start | Minor uses only, such as Rally once per ally. Every armour, shield and piece `fight` use is a minor use: cancelling one event per fight (Ringed, Double Ring, Proof, Twice-Proof), Shadowed, Warding Lining, Runner's Boots and Forewarned's fight-start window **(change AR-6, armour design 8.1 rule 5)**, and every trinket `fight` use (trinkets design 9.3) **(change TK-18)**. No major power is per-fight. |
| `rest` | A completed short rest, and camp | Martial charges, Second Wind, caster rest recovery |
| `camp` | Camp only. *Later:* one named exception, a wandering trader's paid **Recharge**, which resets one wand's `camp` pool, once per trader, priced in coin; the simulator reports wand charges per floor with and without it (change I-16, items design 10.9, 19.1, 19.3) | Spell slots, Surge, signature abilities, wand charges |
| `never` | Never | Consumables |

- There are no player cooldowns. Exploration ticks never refill a pool,
  except `turn` pools, which reset each tick out of combat (6.8).
- A pool's refill tag is read through a query that folds in effects, like
  its maximum, because an offer can change it (10.2).
- A level-up never refills anything. The spent count is unchanged, so only
  the increase in a maximum arrives ready to use.

### 6.3 Both clocks for every class

**(agreed)**

Every class has at least one `rest` pool and at least one `camp` pool.
Each class should draw roughly 40–60% of its limited power from camp pools
**(lead's call, checked by the simulator)**. A change to the supply rate
then moves every class, not half of them.

**(lead's call)**

- The share is measured in the offer value unit (10.2): each pool's uses
  per floor, at about two short rests per floor, times the value of what a
  use buys. The `restDefensiveTool` (6.6) counts at half value, and all
  three measures (full, half, excluded) are reported, combo flags such as
  Momentum included. The band holds at levels 5 and 10; a martial
  class's level-1 miss is accepted. **(agreed; change VG-13, Vanguard
  design 10.4)** The Arcanist's miss at levels 1–2 (26% and 35%) is
  accepted too **(agreed; change ARC-4, Arcanist design 11.8)**. The
  Herald's miss at levels 1–3 is accepted too, and its levels 3–4 are
  read on the class mean over both subclasses, subclass rows printed
  **(agreed; change HR-31, Herald design 11.10)**. Slot
  Recovery counts as `rest` power, in the camp share and in the VG-12
  parity reading alike, never one without the other **(change ARC-3,
  Arcanist design 7.4)**.
- The examples in 6.5 and 6.6 show which pools exist, not the split. As
  written the fighter is mostly rest-powered and the caster mostly
  camp-powered; builders tune the counts into the band. Casters will need
  `rest` features beyond Slot Recovery to get there.
- The rule and the band apply to class data. Companions must have at least
  one `rest` and one `camp` pool (11.1), but the band does not apply to
  them.

### 6.4 One camp spend per turn

**(agreed)**

A unit may spend from a `camp` pool at most once per turn. This caps the
nova turn without banning big turns.

**(lead's call)** The allowance is per unit, per turn of any unit: a unit may
make one camp spend during each unit's turn, whether on its own turn or as a
reaction on another's. Each unit's "camp spend used" flag clears at the start
of every unit's turn. Out of combat it clears every tick (6.8).

**(agreed; change CN-11, consumables design 13.5)** A scroll read (in or
out of class, including a failed attempt) uses the unit's camp spend for
that turn: it is greyed if the flag is set, and sets it when its cost is
paid. (A wand charge is already a camp spend: its pool is `camp`.)

A pool changed to `rest` by the timescale offer no longer counts as a camp
spend.

### 6.5 Spell slots

**(lead's call)**

- Four tiers. A full caster's slots by level (tier 1 / 2 / 3 / 4):

| Level | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|---|---|---|---|---|---|---|---|---|---|---|
| Slots | 2 | 3 | 3/1 | 3/2 | 3/2/1 | 3/2/2 | 3/2/2/1 | 3/3/2/1 | 3/3/2/2 | 3/3/3/2 |

  Values are illustrative; the shape is a new tier on each odd level from 3.
- Slots are `camp` pools, one pool per tier.
- **Slot Recovery:** when a short rest completes, a caster regains one spent
  slot of tier 2 or lower, the highest such tier spent. It is automatic, not
  a pool, and there is no choice to make. An offer may raise it to two
  slots, regained one at a time by the same rule. The Arcanist instead
  gains two slots per short rest as a class feature from class level 9
  (Slot Recovery 2), and that offer is excluded for it
  (`offerExcludes: slotRecovery+1`), as are `timescale:slots.*` and
  capacity and timescale on Overchannel **(change ARC-12, Arcanist
  design 7.4, 7.9)**. The Herald has no Slot Recovery 2 and the offer is
  excluded for it from the start (`offerExcludes: slotRecovery+1`)
  **(change HR-35, Herald design 7.5)**. The name keeps it apart
  from the stat model's Recovery, which is hit points.
- Spells are known, not prepared. A caster knows about seven spells by
  level 10 and may swap one at camp.
- A spell can be cast with a higher slot only if its data declares what
  that adds. The lowest slot that works is the default. A declared entry
  may add nothing: Mending Word's tier-2 entry adds no target, so a
  tier-2 slot is legal and casts the tier-1 effect; the cast's tier is
  the slot spent **(reading; change HR-6, Herald design 8.1)**. Every
  heal card carries `upcastCap: classSlotTable` now (content check
  `HealUpcastCapDeclared`); at M4 the runtime check
  `HealUpcastByClassSlots` caps a heal card's effective tier at the
  Herald's own class-level slot table **(change HR-34, Herald design
  5.3)**.
- No half casters or pact casters at launch. When multiclassing arrives,
  slots combine through a caster-level table, which is data.

### 6.6 Martial example

**(lead's call; illustrative, to show the shape for builders)**

A Strength fighter:

- **Second Wind:** bonus action, regain your Recovery (the Medicine bonus
  excluded). `rest` pool of 1, 2 from level 6. **(change DH-10, death and
  healing design 8, 12.1)**
- **Techniques:** `rest` pool of 2, 3 at level 5. One is spent
  after a hit is known and before damage is rolled, so it is never wasted on
  a miss. It makes that hit carry the weapon's rider, push the target 1
  square, add 1d4 damage (1d6 from level 5), or empower the weapon's verb
  (for example Push 2, Topple with disadvantage on the save). **(change
  W-3, weapons design 7.3; change VG-6, Vanguard design 7.8)**
- **Surge:** `camp` pool of 1, 2 at level 5. One extra action this turn.
  **(change VG-6)**

A martial class has at most three named pools, not counting its capstone
(6.7). **(lead's call)** Turn allowances and per-target round counts are
frequency limits, not named pools: they are outside this limit and the
cap of eight (10.2), count 0 to read, and stay outside the camp-share
ratio (6.3) but inside the 60% formula. A per-rest sub-limit on one
option of a named pool (the Hew-use cap) is not a named pool and is not
counted, like the `restDefensiveTool` sub-limit. **(change VG-10,
Vanguard design 3.4, 7.9)**

Full Recovery as a self-heal is for the fighter-type Second Wind only.
Every other class's `rest` defensive tool (at most half Recovery, or a
defensive or prevention effect) is a requirement on the classes design,
inside 6.3's band. **(change DH-10, death and healing design 12.1)**

### 6.7 Capstone

At level 9 each class gains its capstone: one signature ability with a
`camp` pool of 1, in place of a fifth spell tier. Level 10 is an ordinary
level choice from the class table or an offer. **(lead's call)**

### 6.8 Out of combat

**(lead's call)**

- Out of combat, any party member may act, one command at a time. Each
  command (a step by the hero, or an ability by anyone) advances the world
  one tick. **(settled: one tick per hero step or action)**
- Each tick, every unit's `turn` pools and turn budget reset, including the
  camp-spend flag. So a unit can use one action-cost ability per tick, and
  four units buffing takes four ticks.
- Pre-buffing before a fight is a legitimate tactic, bounded by the
  concentration rule in 9.6.

### 6.9 When a level-up happens

**(lead's call)**

Experience earned in a fight is banked. Level-ups, and companion choices at
their key levels, are resolved when combat ends, never during it. They are
presented together on one screen, hero first.

## 7. Action costs

### 7.1 Declared cost

Every ability declares exactly one cost **(lead's call)**:

| Cost | Meaning |
|---|---|
| `action` | The turn's action |
| `bonus` | The turn's bonus action |
| `reaction` | The round's reaction |
| `attack` | Replaces one attack of the Attack action |
| `move:N` | Spends N squares of movement; `move:0` happens during movement |
| `free` | The turn's one free interaction |
| `onHit` | Spent after a hit is known and before damage is rolled; no action |
| `onEvent:<id>` | Spent when the named event occurs (a Shove or grapple succeeds, a Graze fires, turn start), and never before it; no action **(change VG-7, Vanguard design 7.11, 8.2)** |
| `none` | A passive check or threshold; nothing is spent |

- A pool spend is separate from the cost.
- A pool spend may also be made on the unit's own turn with no action
  to mark a target (the Binder's Cinch (b), source-held `cinched[]`)
  **(change BN-4, Arcanist design 9.1)**.
- A feature or offer may change an ability's declared cost. The cost offer
  in 10.2 makes it a bonus action once per rest.

### 7.2 Universal and skill actions

**(lead's call unless marked)**

| Use | Cost |
|---|---|
| Attack (Extra Attack at level 5 for martial classes) | Action |
| Dash, Disengage, Dodge, Help | Action |
| Shove or grapple: Athletics against 10 + Fortitude **(settled)**; cannot target a unit more than one size larger than the user **(change A-10, enemies design 5.1)**. A successful Shove: push 1 or Prone **(change VG-9, Vanguard design 7.1)** | Replaces one attack |
| Slip past an enemy: Acrobatics against 10 + Reflex **(settled)** | Part of movement, once per turn |
| Escape a grapple | Action |
| Hide: Stealth | Action; a bonus action with a class feature; or part of an attack with a class feature (No Witnesses: on a kill, no bonus action, resolved after weapons design 7.5 step 15 so the killing attack does not end it; conditions design 18.7) **(change SH-12, Shade design 8.1)** |
| Search: Perception against the passive stealth of hidden units within line of sight. Monsters use it after losing the party; Keen gives advantage. **(change A-10, enemies design 11.1, 12.1)** | Action |
| Frighten: Intimidation against 10 + Will **(settled)**; range 6, line of sight **(change HR-14, Herald design 3.3)** | Action |
| Rally: Leadership | Bonus action, once per ally per fight |
| Study an enemy: Lore | Bonus action, once per enemy type per fight |
| Recognise a spell being cast: Arcana | `none`: a threshold with no roll |
| Get a downed companion up: Medicine, trained only **(settled)**; at most once per companion per fight (a Lifeline get-up counts) **(change DH-11, death and healing design 11.2)**. Reach the Fallen (Hearth L6 option): range 2, and the same action Treats that companion (only when taken as an action, never through the Lifeline bonus-action variant); 1 HP and Prone unchanged **(change HR-28, Herald design 9.2)** | Action |
| Treat: Medicine, one roll against each curable instance's recorded difficulty; ends poison conditions, Bleeding, Withered, Hobbled, Condemned and Calcifying at stages 1–2 (Diseased and Scarred at a rest, except a `treatProof` Scarred) **(agreed; change C4, conditions design 8; change CN-12, consumables design 14.6; change DH-11)** | Action, adjacent |
| Disarm or re-arm a trap: Mechanics, trained only **(settled)** | Action |
| Trigger a known trap against enemies: Mechanics. A known armed trap is a hazard for forced movement (conditions design 10, 11.3; the rules of 10.1 and 10.2 unchanged) **(change SH-15, Shade design 9.1)** | Action |
| Lure: choose a square you can see within 6; it emits noise radius 4 (enemies design 12.3). A group with no member in line of sight to you becomes Alerted toward it with no roll; a group that can see you makes its noticing roll. Once per group per floor (`luredThisFloor`). Only if the enemies owner accepts **(change SH-21, Shade design 9.1)** | Exploration action (1 tick) |
| Use a scroll or wand outside your class: Arcana, trained only, against 10 + tier; in class, no slot is spent **(settled; change CN-12, consumables design 13.2)** | Action. A failed check uses the scroll or charge up: no free retries **(settled)**. |
| Drink a potion (a potion on your own belt) **(change I-5, items design 7.1)** | Bonus action |
| Administer a belt potion or tonic to an adjacent ally: tonics minor only; a potion does not lift Downed; not a potion or healing scroll on a Downed unit (a trinket reach step may extend 'adjacent' to 2 squares; trinkets design 6.1) **(change I-5, items design 7.1; change TK-19; change CN-12, consumables design 11)** | Action (the giver's); the receiver spends nothing |
| Throw a belt potion at an ally: range 5, line of sight, no roll, `thrownPotionPermille`; never lifts Downed; not on a Downed unit **(agreed; change CN-12, consumables design 11)** | Action |
| Throw a belt flask: range 5, no attack roll; at most one offensive flask per turn **(change CN-12, consumables design 12)** | Action |
| Hand over to an adjacent ally, from the belt or the weapon sets only, never the pack; never a talisman in combat (a trinket reach step may extend 'adjacent' to 2 squares; trinkets design 6.1) **(change I-5, items design 7.1; change TK-19; change CN-12, consumables design 14; change DH-11)** | The giver's free interaction |
| Take from a Downed adjacent ally, belt or weapon-set items only; never a talisman in combat **(change I-5, items design 7.1; change CN-12, consumables design 14; change DH-11)** | Free interaction |
| Retrieve: pack to belt or to an empty spare set; never a talisman in combat **(change I-5, items design 7.1; change CN-12, consumables design 14; change DH-11)** | Action (`packInCombat` flag) |
| Pick up or seize an item in or next to your square: to a free belt slot or matching stack, an empty spare set, or else `claimed` (unusable this fight) **(change I-5, items design 7.1)** | Free interaction |
| Swap weapon sets (active and spare, shield included) **(change I-5, items design 5.2)** | Bonus action |
| Shield bash (the Shield kind, in the active set; bucklers, Reliquary and tower shields have none): 1d4 bludgeoning, Push; Strength + proficiency to hit, Strength to damage; not an off-hand attack **(change AR-7, armour design 10.2)** | Replaces one attack |
| Wear armour or a shield: armour and shield training are hard; untrained, it cannot be worn (`Untrained`) **(change AR-7, armour design 7.2)** | None; armour is never changed in combat |
| Strained: below a worn armour's or active shield's Strength threshold, speed −2, once per unit however many items are Strained **(change AR-7, armour design 7.1)** | None; a derived penalty, never a condition |
| Stiff: worn medium or heavy armour gives disadvantage on Acrobatics checks (slip past, escape a grapple by Acrobatics, narrow ground) **(change AR-7, armour design 7.3)** | None; a standing modifier |
| Opportunity attack | Reaction |
| Off-hand attack with a light weapon, after at least one Attack-action attack this turn; Nick moves it into the Attack action, once per turn **(change W-2, weapons design 11.3)** | Bonus action, once per turn |
| Threatened zone: an opportunity attack when a hostile leaves it by its own movement, Set when a hostile enters it from outside; they share the round's one reaction, and Disengage stops both. Movement resolves square by square, with the trigger timing of weapons design 8 **(change W-2)**. A slip-past trigger (`slipPast`) resolves after a successful slip-past check and before the step that moves the hostile through or past the square, as an opportunity attack. A separate trigger kind, `attackAgainstOther`, is a **reaction attack**, not an opportunity attack: it hooks after weapons design 7.5 step 15 for a weapon attack, or after 14.3 step 6 for each attack roll of a non-weapon attack, and its legality is re-checked then **(change VG-20, Vanguard design 7.4, 8.2)**. A third, `missedBy`, is also a reaction attack: it hooks after weapons design 7.5 step 6 / 6a, in the order the miss → Graze → `answeredBy[]` written → step 6a → the hook, where legality is re-checked. With `sameReactionFollowUp`, a follow-up on `onParrySuccess` (a Parry or a `countsAsParry` ward was applied and the roll missed) runs in the same reaction when the ward spent or carried the reaction, and spends the reaction after a step-0 fill. One reaction makes one attack: with the Riposte signature (weapons design 18.4) the player picks it or the follow-up **(change SH-8, Shade design 8.3)** | Reaction |
| Ranged or thrown **weapon** attack with a hostile adjacent: disadvantage (spell attacks unaffected) **(change W-2, weapons design 9.2)** | Replaces one attack |
| Loading weapon: at most one attack with it per action, bonus action or reaction **(change W-2, weapons design 9.3)** | Replaces one attack |
| Throw: an attack mode of a thrown-capable melee weapon; it stays with the thrower, no pickup **(change W-2, weapons design 9.3)** | Replaces one attack |
| Reach attack: passes over units, needs line of sight **(change W-2, weapons design 8)** | Replaces one attack |
| Unarmed strike: 1 + Strength bludgeoning, no verb **(change W-2, weapons design 13)** | Replaces one attack |
| Improvised attack: bludgeoning, each die one size smaller (minimum d4), reach 1, Strength; no verb, rider, Exploit or properties **(change W-2, weapons design 13)** | Replaces one attack |

There is no Drop command, and armour and trinkets cannot be changed in
combat. **(change I-5, items design 7.1)**

The Trapwright's trap rows (the hand-trigger clarified, Trigger Shot,
re-arm at 500‰, the hard part once per fight) and Wedge apply when the
Trapwright ships **(changes SH-15, SH-18; Shade design 14)**.

Switching weapons costs a bonus action so that a spare weapon is a fallback,
not a key carried for each enemy. **(lead's call, from the stat model's
anti-goal)**

A subclass may apply the canonical verb of a trained, non-Strained weapon
in the wielder's own spare set (weapons design 7.3). Signatures, dice,
damage type, reach and properties never travel. This is not a key per
enemy: only the verb travels, from the wielder's one spare set, and
each use costs a Technique. **(change VG-19, Vanguard design 8.1)**

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
- The player can change any reaction to "ask me". **(settled)** The player
  can also switch it to any of the automatic policies above. **(lead's
  call)**
- Policies are set per unit, per reaction, and last for the run. Their
  defaults come from data. **(lead's call)**
- A policy may carry a target filter (`nonMinion`), set in data and
  changeable by the player like the policy. **(change W-4, weapons design
  7.1)** A second filter, `notWhileSquaredUpElsewhere`: while the unit has
  an active Square Up, the reaction is not taken against a unit other
  than the Squared-Up one. **(change VG-11, Vanguard design 7.12)**
  Further filters **(change SH-7, Shade design 6.4, 7.12)**: `bigHit`
  (read on the death and healing design 20.3 step-3 total against
  current HP: the total is ≥ 250‰ of max HP, or it would drop the unit
  to 0 and the halved total would not); `noAllyHarm` (not taken when it
  would harm an ally); `ifOpeningUnavailable` (only if no known Opening
  trigger is live on a reachable target); and, on the Opening policy,
  `preferStealthTarget` (hold the allowance for a stealth-tier target
  this turn) and `holdCarryOnOverkill` (while a carry is live, no
  Opening on a minion or on a hit whose weapon damage already drops the
  target). `whilePoolAtLeastHalf`, for a pool with a strike counter,
  also needs a strike left (`remaining ≥ ceil(max / 2)` and
  `strike.remaining ≥ 1`).
  A further filter, `reserveForRestTool`: reserve 1 Weft while a Knit
  use is left; a pre-tick that protects an ally ignores it **(change
  ARC-6, Arcanist design 7.5, 7.14)**.
- A second policy class, **cast-option defaults** (`castDefaults`),
  sits beside reaction policies: visible defaults on the hero's own
  cast, shown ticked, which the player can untick; computed from static
  predicates only, never from scorer terms. They are not suggestions
  under 11.3 **(change ARC-6, Arcanist design 7.14)**. The Alembic's
  `castDefaults.prime` is one: no displaced rider, a payer actually
  available, own at-wills excluded, once per fight solo, a per-pair
  default reagent **(change ALM-8, Arcanist design 9.2)**.
- Monsters use their reactions' automatic policies; they have no "ask me".
  **(lead's call)**
- Reactions do not nest. A reaction, a boss action or an opportunity attack
  cannot be triggered while another reaction is resolving, and boss actions
  happen only between turns. So a pending decision is at most one level
  deep inside an ability's resolution. **(lead's call)**
- At most one reaction AC bonus applies to any attack roll, resolved by the
  window algorithm in armour design 10.4. Step 0: window fills (a reaction
  AC bonus from a camp-pool ability, such as Stand Fast, that spends no
  reaction), ordered by source unit order, then creation order; the first
  applies, counts as the one reaction AC bonus and closes the window.
  Fills inherit the other skip rules, but not "no reaction left". **(change
  VG-4, Vanguard design 7.7)** Then candidates in order, skipping any
  the attack ignores or whose unit has no reaction left; each candidate's
  policy (or "ask me") is evaluated in turn, and the first that fires
  closes the window. The rest are not taken and not spent. **(change AR-8)**
- At most one reaction save bonus applies to any save (the trigger kind
  `ownSave`, 14.3) **(change ARC-19, Arcanist design 7.5)**.

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
- No area covers more than 25 squares at levels 1–6, or more than 49 at
  levels 7–10 **(change ARC-9, Arcanist design 8.1)**. Content
  validation enforces this.

### 8.2 Friendly fire

Areas hit allies unless the ability is tagged `selective`. At-wills never
hit allies. **(lead's call)**

At-will arcs never reach allies either; this rule wins over the Soaked
arc. A `selective` cast leaves allies out of all its effects, arcs,
forks and forced movement included, and selective may be taken on any
slot cast whose arcs or forks reach an ally. A caster is never affected
by its own casts' areas, arcs and forks, except presence call or cure
areas whose record names the caster (Rouse, Hearthsong). **(change ARC-8,
Arcanist design 8.1, 8.2; change HR-13, Herald design 8.1)**

### 8.3 Preview

The core answers "what would this do?" for any ability and target: which
units are affected, the odds for each, expected damage, and any rider
chance. The display computes nothing. **(settled principle)**

**(lead's call)**

- The preview uses only what the party knows. A trait, resistance or
  weakness not yet revealed by Lore or seen in play is treated as unknown:
  damage is shown as normal and marked uncertain. Otherwise the preview
  would give away what Lore exists to reveal.
- Intent reading (enemies design 13.3) is the one place the display shows
  the monster AI's own output: gated by guile, computed live from the true
  state filtered by the awareness record, with no roll and no commitment.
  **(change A-8)**
- The Break-chain preview (on hover only) shows the loss cascade from
  party knowledge; one morale overlay family, at most one glint per
  unit, in the priority rout-ready, faltering, Frightened tether; the
  Spurred trail only on its holder's turn; the readability playtest gate
  counts glints per screen **(change HR-37, Herald design 7.14, 10.3)**.
- The preview leaves out reactions it cannot predict: an enemy reaction the
  party has not seen, and any "ask me" reaction.
- The property test in 16 compares preview and resolution with full
  knowledge and with reactions switched off.

### 8.4 No ground zones

**(agreed)**

Sustained areas are auras that move with a unit, or conditions placed on
units. Nothing is left on the ground. This stays clear of the terrain
non-goal. Fixed map terrain that applies conditions is part of the map, not
a ground zone (conditions design 11). **(change C11)**

## 9. Riders and damage

### 9.1 Damage parts

- Damage is a list of typed parts, each resolved separately. **(settled)**
- A part has a type, dice, a flat amount and a `rider` flag. The flag is the
  exact meaning of "carries a rider". **(lead's call)**
- A target weak to a part's type gets that type's rider automatically.
  **(settled)**
- A critical hit attempts a rider even when no part is flagged, subject to
  the bounds below. **(lead's call)**
- Piercing is the exception in form: its rider is an always-on reduction to
  the target's armour on the attack roll, larger against a weak target. It
  needs no flag, has no save, and does not count toward the bounds in 9.3.
  **(settled)** Sizing: 1 against any armour value, +1 against
  `CountsAsHeavy`, +1 against a weak target, ×2 with Puncture; shares a cap
  of 4 with Pitted (stat model 8.2, change AR-3; armour design 9.1).
  **(change AR-10)**

### 9.2 Rider saves

- A rider's save uses the save the builders assign to that rider type,
  against the attacker's ability difficulty. **(settled)**
- It is a separate roll from the ability's own save. Failing an ability's
  Reflex save against a fireball does not decide whether the target catches
  fire. **(lead's call)**

### 9.3 Bounds on save noise

**(lead's call)**

- At most one rider per target per ability use. It is chosen in this
  order: the first part, in data order, whose type the target is weak to
  (automatic, no save); else the first flagged part; else, on a critical
  hit, the first damaging part. The verb Topple shares this save slot: if
  a rider is attempted on that target in that use, Topple is not.
  **(change W-4, weapons design 7.2)**
- An at-will's rider is attempted at most once per turn per attacker. This
  covers flagged and critical-hit riders on Cleave and Skewer second
  targets; weak-target riders are untouched (stat model 8.2). **(change
  W-4, weapons design 7.2)**
- Damage dealt by a rider never triggers another rider.
- The lightning arc deals the rider strength band as lightning, one
  damage event per arced unit, with no crit, no rider flag and no
  re-arc. An at-will's Soaked arc spread happens at most once per turn
  across its beams. **(change ARC-14, Arcanist design 8.1)**
- Interplay's rider flag and Fused follow the one-rider order above; a
  Mote cash-in counts as the at-will's one rider attempt that turn
  **(change ARC-18, Arcanist design 10.3)**.
- Note: the Alembic's Prime is the hit's rider (gate and slot), offered
  once the hit is known, but outside the at-will once-per-turn bound
  **(change ALM-5, Arcanist design 9.2)**.
- A rider is not attempted on a target that already has that condition.
  Exceptions: the psychic rider is attempted on a concentrating target or
  one not already Unfocused, and an application with `worsen` bypasses this
  gate (conditions design 6.2, 6.4). **(change C8)**
- Rider strength is one table per damage type, indexed by proficiency band
  (levels 0–4, 5–8, 9–12). The strengths themselves are the builders'.
  **(settled that they are the builders'; bands change A-1, enemies design
  24)**

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
  **(lead's call)** All turn-start ticks together are one damage event
  (conditions design 6.8). **(change C10)**
- Proficiency is added only from a feature or an offer. **(lead's call)**
- A unit concentrates on one effect at a time. Starting another ends the
  first. **(lead's call)**
- Concentration ends when the unit is downed or incapacitated, when a
  psychic rider lands on it **(settled)**, when its duration ends, or when
  the player ends it as a free interaction. **(lead's call)**
- Handoffs: Afterhold (a trigger offer) turns the ended effect's
  sustained minor and fetter-type instances into `turns:1` when you end
  concentration voluntarily on your own turn by starting another; never
  Dazed or hard instances, and barred for the Binder **(change ARC-28,
  Arcanist design 10.3)**. The Binder's Belay turns the fetters it
  sustains into unlinked `turns:1` when concentration ends for any
  reason but its own `max` **(change BN-2, Arcanist design 9.1)**.

### 9.6 Durations

**(lead's call)**

- An effect lasts `rounds:N`, `turns:N`, `untilRest`, `untilCamp`, or
  until a stated trigger. A condition on a unit, sustained ones included,
  uses `turns:N` or another model of conditions design 5.1, never
  `rounds:N`. **(change C1)**
- In combat, a `turns:N` condition counts down on the wearer's turns, by
  the phase test of conditions design 5.2; windows
  (`untilSourceTurnStart`/`End`) run on the source's turns. A hard
  condition applied during the wearer's own turn ends at that turn's end,
  and Steeled follows. **(change C1)**
- `rounds:N` remains only for auras, concentration effects themselves and
  effects not on a unit. Such a round counts down at the end of the source
  unit's turn. If the source is dead, downed, gone or not in the initiative
  order, it counts down at the end of each round instead.
- Out of combat, one round, or one of the wearer's turns, is one tick, so a
  buff of 10 turns lasts 10 steps (conditions design 5.5). The main design counts durations in ticks; this fixes the ratio.
- An effect records the source's ability difficulty when it is applied, so
  repeat saves are unchanged if the source dies or loses a buff.
- A beneficial effect on an ally that lasts longer than 10 of the wearer's
  turns **(change C1)**, or until a rest or camp, must use concentration.
  Content validation enforces this. It is what bounds pre-buffing (6.8).
  Exempt: boons from events (shrines, floor and camp events) and the
  starting boon may last until rest or camp without concentration, one
  lingering boon per unit; self-only toggles are exempt from the 10-turn
  rule (conditions design 5.3, 13). **(change C5)**

## 10. Scaling and offers

### 10.1 Pacing

**(lead's call; levels per floor are illustrative)**

| Floor | Levels | Power beats |
|---|---|---|
| 1 | 1→3 | Tier 1 spells; tier 2 and subclass at level 3 |
| 2 | 3→5 | +1 ability at level 4 **(settled)**; Extra Attack, tier 3, stronger cantrips at level 5 |
| 3 | 5→7 | Tier 4 at level 7 |
| 4 | 7→9 | +1 ability at level 8 **(settled)**; capstone at level 9 (6.7), plus a pick-one-of-three offer **(agreed, change CL-4, classes design)** |
| 5 | 9→10 | Ordinary level choice: class table or offer |

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
  - verb training (family): training, and so the verb, in one named
    weapon family **(change W-3, weapons design 7.3)**;
  - armour training: the next category, up to the class's ceiling
    **(change AR-9, armour design 7.2)**;
  - shield training: only for a class whose `shieldTraining` is `allowed`
    **(change AR-9, armour design 7.2)**;
  - timescale: one `camp` pool becomes a `rest` pool, never the unit's
    last `camp` pool. At most one per run.
  - No capacity or timescale offer targets a turn allowance (`prime`
    included) **(change ARC-26, Arcanist design 7.8)**.
  - trigger: an ability gains one named extra trigger from its class's
    `triggerMenu`, never repeated; legal at any level (classes design 7.5)
    **(change CL-3)**, except an added Opening trigger record, which has
    `minClassLevel ≥ 5` (`TriggerFromFive`, with one stated exception)
    **(change SH-23, Shade design 6.6)**.
- Every offer declares `requires` tags (what build it suits) and a value in
  a single unit, about one action's worth of effect. The offer draw filters
  by both. **(lead's call)**
- An upgrade is an effect, filtered by ability ID or tag. Upgrades are never
  data patches. **(lead's call)**
- The hero has at most eight limited abilities from class and offers.
  Abilities granted by items (potions, scrolls, wands, trinkets) do not count;
  trinket buttons are capped at one per unit (trinkets design 9.2)
  **(change TK-20)**. Past that, offers only upgrade existing ones;
  trigger offers stay in the draw beside upgrades **(change CL-3)**.
  **(lead's call)**
  A limited ability is one named pool: every ability drawing on that pool
  is an option of it, and a spell tier's slot pool counts as one, with its
  known spells as options; an upcast is an option of the higher pool. An
  offer or talent that adds an ability to an existing pool adds an option,
  not a limited ability. A rest tool's per-rest sub-limit is not counted
  (classes design 3.4, 7.5, 8.2). **(change CL-2)** Nor is a per-rest
  sub-limit on one option of a named pool (the Hew-use cap); turn
  allowances and per-target round counts are frequency limits, not named
  pools, and count 0 here. **(change VG-10, Vanguard design 3.4, 7.9)**
- New spells are learnt through the same pick-one-of-three cards, drawn
  from the class's spell list. **(lead's call)**

### 10.3 Level-up cards

**(agreed)**

Each option shows what changes, computed by the core: "Cleave: 2 → 3 uses",
"Ember Burst: radius 1 → 2". The player never has to work it out.
Automatic gains at that level (a new slot tier, Extra Attack, proficiency)
are listed on the same screen above the choice. **(lead's call)**

## 11. Companions

### 11.1 Kit

**(lead's call)**

- A companion has a class for hit points, saves, skills and main ability
  **(settled, stat model 5.5)**. Its kit is companion data of its own, not
  the hero's class table.
- Two at-wills, up to three limited abilities, one reaction, and passives.
- At least one `rest` pool and one `camp` pool. The signature ability
  below is usually the camp pool.
- Limited abilities read as one line each: "2 per rest", "1 per camp".
- Exactly one signature `camp` ability per companion.
- The same pools, concentration and riders as the hero.

### 11.2 Progression

- Automatic, with a choice at a few key levels. **(settled)**
- The choice is one of two or three kit upgrades, at levels 3, 6 and 9.
  **(lead's call)**
- A recruit joins at camp with full pools. **(lead's call)**
- A companion's level always equals the hero's; a recruit joins at the
  hero's level with missed choices defaulted on the join card; one
  2-option 3/6/9 grammar (numeric steps only); Rethink: one choice
  re-picked per companion per camp, never a stance; no ability increases
  (fixed array; `companion.mainIncreases` off). **(change CMP-16,
  companions design 8.2)**

### 11.3 Suggested actions

**(agreed)**

- A setting turns on a suggested action for companions. It is off by
  default.
- With it on, the game highlights the action it would take for the active
  companion, and one key accepts it. The player can always do something
  else.
- A suggestion is one step: a destination square and one ability with its
  target, or either alone. Accepting it carries out that step. After it,
  the next step is suggested. **(lead's call)**
- Suggestions use only what the party knows, as the preview does (8.3).
  **(lead's call)**
- The release bar **(lead's call)**: on a fixed set of test scenarios the
  suggestion never hits allies for more than it gains, never spends a camp
  pool when an at-will scores at least as well, and never walks into an
  avoidable opportunity attack; in playtests, testers accept most
  suggestions.
- Nothing is ever taken without the player. The hero never gets
  suggestions.
- An optional accept-rest-of-turn key (player-pressed each turn; never
  the hero) is prototyped in M2 behind a playtest flag, off in every
  release build, and ships only if the client says yes after the M2
  prototype. **(agreed 2026-11-20 for the prototype; change CMP-19,
  companions design 8.6)**
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
  turn start. No recharge dice. The minimum cooldown is 2. **(change A-2,
  enemies design 9.3)**
- `readyAt: N` (default 1): first usable on the unit's Nth turn of the
  current fight, counted by the unit's `turnsTaken`, reset to 0 at each
  fight start. **(change A-2, enemies design 9.3)**
- Cooldowns reset to ready at every combat end; `uses` never reset.
  **(change A-2, enemies design 9.3)**
- Party-facing hard-control abilities have `readyAt ≥ 2` and `cooldown ≥ 3`
  and are never openers. **(change A-2, enemies design 9.3)**
- Lore reveals uses left and cooldowns ("ready in 2"). Anything already seen
  in play is visible to everyone. **(settled)**

### 12.2 Bosses

**(lead's call)**

- Boss fights are designed for 5–7 rounds, so a party's per-rest abilities
  run out and saved camp resources matter.
- Boss actions: a boss may act at the end of other units' turns, N times
  per round, where N = min(1 on floors 1–2 or 2 on floors 3–5, the number
  of party units). At most one at the end of any unit's turn, never after
  the boss's own. Never hard control. **(change A-3, enemies design 17.5)**
- Resolve: two or three visible pips per life. A pip is spent whenever a
  condition whose `controlRank` is at or above the boss's
  `resolveThreshold` (both static data) would land, by any route: a failed
  save, a successful skill check, or an automatic weak-target rider. It
  cancels only that condition. **(change C2, conditions design 12.3)**
- Pips spent, phases passed and uses belong to the boss's **life**, not the
  fight; cooldowns reset at combat end as for every monster. On a reset the
  boss returns Wary and leashed, with hit points raised to the top of its
  current phase band. **(change A-4, enemies design 17.5)**
- Unyielding: every boss has it. Hard conditions downgrade once the pips
  are gone, a composure window follows some conditions, and it resists
  lockouts (conditions design 12.3). **(change C2)**
- Phases triggered at hit point thresholds. A phase change ends all harmful
  conditions and grants Steeled, after the ability use that caused it
  (conditions design 9.3, 12.3). **(change C2)**
- Every boss has at least one way to deal with sustained effects, such as
  breaking concentration or shedding a condition.
- Boss actions are chosen by the same scorer as the boss's turns.
- Each boss flags 1–2 limited abilities as `signature`. They are listed
  from the boss's first turn and, whenever the boss would use one if its
  turn began now, announced to everyone with target or area. A forecast,
  not a wind-up: the deferral in 5 stands. **(change A-9, enemies design
  13.2)**
- Tuning target: a party with about half its camp resources wins with good
  play. **(agreed)**

## 13. AI

**(lead's call)**

One scoring formula, integers only, chances counted out of 20. These are
its core terms:

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
- **Open to more terms.** Builders add terms the core terms cannot express:
  healing and damage prevented, defence and buff value, ending
  concentration, and position (exposure to enemies, opportunity attacks
  taken, allies in reach). A term is data where possible, a C# hook
  otherwise. The monster AI adds the named data-weighted terms of enemies
  design 11.1: approach, exposure, opportunity cost, range, target
  preference, focus spread, Quarry, cohesion, body-block, area spread,
  hold and leash. **(change A-6)** The Loremaster's Unpick scorer
  term is added when the Loremaster ships (change LM-2, Arcanist design
  9.3). Every weapon verb adds a static
  scorer value in hit-point equivalents. **(change W-4, weapons design
  15.4)**
- **Units:** every term is in hit-point equivalents. A condition's control
  value is computed (conditions design 17.3); only its `denyPct` is set in
  the condition's data. **(change C14)**
- **Fight length** is estimated from the current state: the enemies' total
  hit points over the party's expected damage per round, and the reverse.
- **Search:** the scorer scores one step at a time, as pairs of a reachable
  square and an ability use from it (or either alone). It does not search
  whole-turn plans.
- **Knowledge:** the monster AI reads the true state, except that a party
  unit's position is known only through that monster's awareness record
  (enemies design 11.2). **(change A-5)** The party scorer, and
  so the suggestions and the simulator's party, uses only what the party
  knows (8.3).
- **Spending policies** are settings of the same scorer: spend freely is a
  pressure of 0, hold everything is a very high pressure, hold half keeps a
  boss reserve of half the camp pools. The scorer's own boss reserve is a
  further term, so the reports can tell it apart from the fixed policies.

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
  free interaction used, and the camp-spend flag, which clears at every
  unit's turn start (6.4).
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
   For weapon attacks this step expands into the weapon attack pipeline.
   **(change W-4, weapons design 7.5)** A hit by a non-weapon attack roll
   flags the damage event as Duck-eligible and records the attacker, as
   weapons design 7.5 step 8a does; nothing is offered here (the offer
   and halving are death and healing design 20.3, before and at step
   3a), and the concentration check uses the step-3a value **(change
   SH-6, Shade design 7.4)**.
6. Resolved.

Reactions hook in at named points, such as between an attack roll and the
hit being decided. The `missedBy` hook and its order at a miss are in
7.2's threatened-zone row **(change SH-8, Shade design 8.3)**. A
reaction trigger kind `ownSave` hooks when the unit rolls an initial save
against a hostile ability, with the roll known (not repeat saves,
hazard-edge saves or concentration checks) **(change ARC-19, Arcanist
design 7.5)**. Event kinds `harrowingWordFailedSave`, `ownActionResolved`
and `combatStart` are added for `onEvent` costs; `heraldFearLands` joins
when the Warcaller ships (M3) **(change HR-29, Herald design 14.1, 9.3)**.

### 14.4 Rules in the core

- Refills are triggers on turn start, combat start, rest completed and camp
  entered. A level-up needs no refill code.
- Several triggers on one event run in order of priority, then source unit
  order, then effect creation order.
- Every roll uses the combat stream. **(settled)**
- Effects that data cannot express, including reaction policies and AI
  terms, are C# hooks referenced from data by ID. **(settled)**

### 14.5 Content validation

The build fails when:

- a pool has other than one refill tag;
- a class lacks a `rest` pool or a `camp` pool;
- a unit has fewer than two at-wills, or none that deals damage;
- an area exceeds 25 squares at levels 1–6;
- a reaction has no automatic policy;
- a companion has other than one signature ability;
- a beneficial effect on an ally longer than 10 of the wearer's turns, or
  until rest or camp, lacks concentration, except event boons, the
  starting boon and self-only toggles (changes C1, C5);
- a class's limited abilities from its table and offers can exceed eight,
  counted as the distinct pool ids reachable from the class table plus the
  offers that create pools (10.2) **(change CL-2)**;
- an offer's `requires` tags do not resolve.

### 14.6 Staging

- **M1** (one class, levels 1–3): pools with `rest` and `camp` tags,
  at-wills, the slice class's slots or charges, short rests and supply,
  concentration if the class casts, bursts, cones and lines, monster uses
  and cooldowns, boss resolve, the monster AI. The M1 simulator plays the
  party with the same scorer at a fixed pressure, over single fights.
- **M2:** riders on damage parts, offers, companion kits, boss actions, the
  party scorer's pressure and reserve, suggestions, simulator spending
  policies, and whole-floor simulation. Out-of-combat choices in the
  simulator (when to rest, which fights to skip, offers, recruits, supply
  bought) are scripted policies, not the scorer. The recruit policy is
  `MusterBestFit`, written in full in companions design 11.3 (with
  `combatValuePermille`), with the simulator arms there; the DH 18.1,
  classes 6.6 and Herald design 5.4 readings pin to the reference
  schedule and this policy. **(change CMP-18)**

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
- Order: Downed companions get up on 1 hit point → Treat attempts →
  each unit regains its Recovery in hit points **(settled)** plus the
  Medicine bonus → `rest` pools reset → each caster's Slot Recovery (6.5).
  **(change DH-12, death and healing design 11.3)**
- The best trained Medicine member adds Wisdom + proficiency to every
  unit's Recovery. Medicine mastery's signature use is the builders' to
  choose. **(settled that it is the builders')**

### 15.3 Supply

**(lead's call; numbers illustrative)**

- The run starts with 2 supply. Each camp gives 1. Each floor holds 1–2
  more as loot, and more can be bought at camp.
- The party carries at most 3. Supply **uses no slots**: it is an integer
  party counter, never an item. **(change I-6, items design 5.3)**
- Target: about two short rests per floor of 4–6 fights plus the boss.
- `supplyMainPathMin` 1: at least one found supply cache is in a
  main-path container before the boss room, needing no optional fight and
  no check. **(client, agreed 2026-11-17, Q3; change DH-13, death and
  healing design 5.7, 12.4)**
- A rests-per-floor report gates on a median of at most 3 *(illustr.)*.
  Lever order if it fires: found supply 1 per floor; limited vendor supply
  stock (back to the client); supply cap 2. A higher supply price is not a
  lever. **(change DH-13, death and healing design 5.7)**

### 15.4 Unusable buttons

**(agreed)**

A button that cannot be used is greyed and names its one reason. The
reason is the validation code from 14.3. **(lead's call)**

- The action bar shows only reasons that do not depend on a target: no
  action left, no slot, camp spend already used this turn.
- Reasons that do depend on a target (out of range, no line of sight) are
  shown on the targeting cursor, not the bar.

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
  party can cast buffs just before a boss. Long buffs need concentration
  (9.6), and bosses carry a tool against sustained effects; watch it in
  playtests.
- **Radius 2 bursts** (25 squares) may be too strong, and square areas must
  read clearly in isometric 3D.
- **Rest interruptions** are luck. If they feel unfair, the patrol preview
  is the lever.
- **Protected content.** Every ability name and mechanic is checked against
  SRD 5.1 and 5.2 before it ships, with attribution.

## 18. Left to the builders

- All numbers: slot counts, pool sizes, dice, ranges, durations, supply
  rates and prices, thrift and pressure values, boss resolve and actions.
- The four launch classes' kits, within the rules above: at the classes
  design's stage 2 (`2026-11-17-classes-concepts-design.md`) **(change
  CL-13b)**.
- Each rider's save type and strength table.
- Spell and ability lists, names and descriptions.
- The expected-need estimate inside the party scorer.
- Hotbar, sheet, level-up and rest screen layout, including whether a rest
  plays out on the map.
- What counts as one "thing to read" in the playtest target (11.4):
  answered by the counting rule in classes design 7.6 **(change CL-13b)**.
- The JSON schema and effects vocabulary, built by the core developers with
  the content designer.

## 19. Changes this makes to the parent documents

Applied on 2026-11-16:

- Stat model section 10: the ability and resource model is no longer
  pending; point to this document for skill action costs, which abilities
  carry riders, and casting.
- Main design section 4.3: durations in combat count in rounds; out of
  combat one round is one tick (9.6 here).

## 20. Depends on systems not yet designed

- **Conditions:** no longer pending. The list, rules and control values
  are in `2026-11-16-conditions-design.md` (conditions design).
  **(change C13)**
- **Items and inventory:** the generic layer is no longer pending. It is
  in `2026-11-16-items-inventory-design.md` (items design). What remains
  open moves to the category designs (items design 17): starting kits
  (class kits need a minimal starting-equipment set for M1) and
  Clinging. **(change
  I-15)** Weapons and foci are no longer pending: they are in
  `2026-11-17-weapons-design.md` (weapons design). Disarmed and Coated stay
  Later. **(change W-15)** Armour and shields are no longer pending:
  armour values, Strength thresholds and Pitted Armour (Later) are in
  `2026-11-17-armour-design.md` (armour design). **(change AR-10)**
  Consumables, scrolls and wands are settled in the consumables design
  (`2026-11-17-consumables-design.md`, 2026-11-17); coatings and Coated
  stay Later. **(change CN-26)**
- **Enemies:** no longer pending. Monster cost is in enemies design 10.1,
  behaviour after losing sight in 12.1, morale in 14 and experience on
  resolution in 16 (`2026-11-16-enemies-design.md`). **(change A-7)**
- **Content volume:** counts of companions, themes, monsters and bosses for
  M2. The M1 slice class is the Vanguard (classes design) **(change
  CL-13b)**.

## 21. Open items for the client

None at the time of writing.
