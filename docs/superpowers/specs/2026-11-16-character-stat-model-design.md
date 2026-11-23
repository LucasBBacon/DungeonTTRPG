# Character Stat Model: Design

Date: 2026-11-16
Status: approved by client, 2026-11-16
Parent: `2026-11-15-dungeon-rpg-design.md` (the main design)

Items marked **(agreed)** were agreed with the client, either directly or by
approving the lead's proposal. Items marked **(lead's call)** were filled in
by the design lead while writing this document and have not been confirmed.
Approval of this document confirms them: once approved, every item here is
binding on the builders.

## 1. Intent

The stat model is the vocabulary every class, item, monster and condition is
written in. It covers four layers: abilities, derived numbers, skills, and
damage types with resistances.

The model should give a tabletop player a sheet they recognise, without
porting 5E. Names, values and mechanics may change wherever they do not fit
a dungeon game with no dialogue. **(agreed)**

The test for every stat: it is distinct from the others, it visibly drives
at least one system, and a player notices the difference between two
characters who differ in it. **(agreed)**

## 2. Goals, non-goals, anti-goals

### Goals

- Every ability owns one thing in a fight, one in defence and one in the
  dungeon. **(agreed)**
- A stat difference is felt three ways: a roll modifier, an always-visible
  number, and thresholds that need no roll. **(agreed)**
- No ability is compulsory for survival, and none is a safe dump for every
  build. **(agreed)**
- The same obstacle has several answers, each belonging to a different
  build. **(agreed)**
- A specialised build is slowed by a bad matchup, never locked out.
  **(agreed)**
- Heroes, companions and monsters all use the same model. **(agreed)**

### Non-goals

- Fidelity to 5E numbers or wording.
- Social skills and dialogue checks.
- Spreading or evolving terrain surfaces, such as burning oil that spreads.
  Held back until riders and layers are proven. **(agreed)** Conduct
  (lightning through connected water) is not held back: it arrives in M2,
  before this gate, because it is instantaneous and makes no square
  (room terrain design 6.8, K3). Fixed map squares that apply conditions
  are allowed; they never spread or change, except for the sanctioned map
  changes: fire burns an oil square to plain floor (conditions design 11);
  from M2, Furnishing debris and removal, boss phase sockets, and fire
  burning a web or bedsheet square to plain floor; from M3, Overturn
  debris and sight change, Remnants (room terrain design 6.3, 6.3.7,
  6.6–6.8, 6.11; conditions design 11), and pre-proven Stir states
  (hazards design 6.3). **(agreed)** (cross-document comparison
  2026-11-23, CC-16)
- **M3 exception:** environment-made Remnant clouds (Miasma only) with a
  fixed footprint and a short countdown, which never drift, spread or
  grow (room terrain design 6.6). "Until riders and layers are proven"
  is read as the M2 rider and layer reports passing, recorded by the
  lead in writing before Remnant work starts (room terrain design 6.6).
  **(agreed)** Applies at M3.
- **M3 exception (hazards design 6.3, 7; agreed 2026-11-21, its Q3):**
  pre-proven Stir states. At the round-end Dream step a Stir may change
  squares only between states proven in advance by the hazards
  validator (V10), using existing square kinds only, announced a round
  ahead and held while occupied where needed: Tides (held whole, so
  only their 2 proven states exist), Cord Flips, Turning Ring boards,
  environment-moved Furnishings (the Returns Trolley) and environment
  Overturn along a pre-proven strip. The flee and escape proofs hold in
  every state; nothing spreads, drifts or grows. **(agreed)** Applies at
  M3; the Inverted Cathedral's reversal round at M4 (the rules stay
  flat, only the push direction reverses).
- The ability and resource model (spell slots, action costs). Designed
  separately in the ability design
  (`2026-11-16-ability-resource-model-design.md`). (cross-document
  comparison 2026-11-23, CC-16)

### Anti-goals

- **Dead numbers.** No value on the sheet that does nothing, such as a 5E
  score whose only job is to produce a modifier.
- **Bring-the-right-key play.** Damage types must not reduce to carrying one
  of each and swapping.
- **Decoration skills.** A skill with no content on most floors has failed.
- **Runaway stacking.** A lucky loot run must not break the +5 cap.

## 3. Constraints

From the main design:

- Integer arithmetic only; all rounding is down.
- Deterministic: same seed and inputs, same result.
- Rules live in the core. Every exception is a modifier or a trigger in the
  effects system.
- Content only from the 5E SRD (5.1 or 5.2, both CC-BY-4.0) or original.
- A level-up takes well under a minute.

## 4. Approaches considered

### A. The six abilities exactly as 5E has them

- Gains: most familiar; nothing to explain.
- Costs: Dexterity does too much, Intelligence does almost nothing outside
  casters, Charisma has no job without a story, and scores add a dead
  number.

### B. The six abilities with ownership redistributed (chosen)

- Gains: a recognisable sheet; class identity and multiclass requirements
  still work; SRD monsters keep their six abilities and need only
  rescaling; each ability gets a distinct job.
- Costs: several departures from 5E that experienced players must relearn.

### C. A custom set of about four stats

- Gains: cleanest on paper.
- Costs: loses the tabletop feel that is the appeal of the game, and every
  class and monster is invented from nothing.

### Decision

The client chose **B**. It keeps the familiar sense of a tabletop sheet while
fixing the abilities that have no job in this game.

## 5. Abilities

### 5.1 Values

- Six abilities: Strength, Dexterity, Constitution, Intelligence, Wisdom,
  Presence. Presence replaces Charisma. **(agreed)**
- An ability is a single modifier. There are no scores. **(agreed)**
- At creation the range is −1 to +3. The hard cap is +5 after all bonuses.
  **(agreed)**
- The starting spread is +3, +2, +1, +1, 0, −1. The class suggests where
  each value goes; the player may rearrange them. **(lead's call)**
- The −1 is kept on purpose: one visible weakness is part of a character.
  **(agreed)**
- The hero gains +1 to one ability at levels 4 and 8. **(agreed)**
- Species give traits, never ability points, so every species and class
  pairing is playable. "Species" replaces "race" throughout. **(agreed)**
- Bonuses to the same ability do not stack: only the highest applies.
  **(agreed)** The same holds for penalties: only the worst applies. A bonus
  and a penalty both apply. **(lead's call)**

### 5.2 Ownership

**(agreed)**

| Ability | In a fight | Defence | In the dungeon |
|---|---|---|---|
| Strength | Melee and thrown attacks and damage, except light and finesse attacks rolled with Dexterity; shoving and grappling (weapons design 6.2) | Fortitude; heavy armour requirement | Carrying capacity; forcing doors and obstacles |
| Dexterity | Attack rolls with ranged, light and finesse weapons (weapons design 6.2) | Armour class in light armour; Reflex | Stealth |
| Constitution | Holding concentration | Hit points; Fortitude | How much a short rest restores |
| Intelligence | Initiative; arcane casting | Reflex | Traps, locks, secrets, identifying loot |
| Wisdom | Divine casting; reading enemy intent (Perception against guile, enemies design 13.3) | Will | Perception; not being surprised |
| Presence | Its own casters; frightening and rallying | Will | Companions, shop prices, event outcomes |

Lore (Intelligence) tells what an enemy is; Perception (Wisdom) tells what
it is about to do.

### 5.3 Departures from 5E

All **(agreed)**.

- **Initiative uses Intelligence**, not Dexterity.
- **Dexterity adds to hit but not to damage.** Strength adds to both for
  melee and thrown attacks rolled with Strength; a Dexterity-rolled attack
  adds only a negative Strength. The strong character hits hard; the
  nimble one hits often. `light` means dual-wieldable and
  Dexterity-eligible, `finesse` Dexterity-eligible only. Exploit (weapons
  design 6.3) and the default-off launcher Precision (weapons design 6.5)
  are the only **core** Dexterity-keyed damage rules; class precision
  features (weapons design 6.4) are the class designs' own.
- **Three saves, not six**, each using the better of two abilities
  (section 6).
- **Constitution stays** despite being the ability nobody picks for fun,
  because it owns short-rest recovery. Class supplies most hit points over a
  run, and Constitution the smaller share; at level 1 the flat bonus is the
  larger share (death and healing design 7.2).

### 5.4 Party rule

**(agreed)**

- Dungeon checks use the best member of the party: traps, perception,
  forcing doors, and so on.
- Stealth uses the worst member.
- Presence is always the hero's. The hero is the leader.
- **Curiosity checks (events design 4.3.2; EV-5; M2; agreed
  2026-11-22):** dungeon skills use the best eligible member, companions
  included; party-movement Stealth ("slip past") uses the worst member;
  Leadership and Intimidation use the hero. A single-actor manipulation
  ("lift the key off its string") uses the acting unit's Stealth, a
  reading under the Steal precedent in 7.1 (pending confirmation by this
  document's owner).

Companions are therefore how a hero covers a weak ability, and a recruit can
carry a cost as well as a benefit.

### 5.5 Monsters, companions and multiclassing

**(lead's call)**

- Every unit has the six abilities and a level. Level sets proficiency for
  all units.
- Heroes and companions take hit points, save training, skills and main
  ability from their class.
- A monster has no class. Its data declares the same things directly: hit
  points, trained save, trained skills, main ability and traits.
- The +5 cap binds every unit, bosses included. Monsters may go below −1.
  SRD monsters with higher modifiers are rescaled when adapted, which keeps
  attack bonuses and armour class in one range for the whole game.
- When multiclassing arrives: save training comes from the first class
  taken, and each ability uses the main ability of the class that grants it.

### 5.6 Class coverage

Each ability should be the main ability of at least one class and a sensible
second ability for another. With four classes at launch, Presence is the one
most at risk: either one of the first four leads with it, or it stays a
secondary ability until more classes unlock. **(agreed as a target; the
choice of classes is left to the builders)**

## 6. Derived numbers

Formulas give the shape. Exact values are left to the builders.

| Number | Shape |
|---|---|
| Proficiency bonus | +2 at levels 0–4, +3 at 5–8, +4 at 9–12 (levels 0, 11 and 12 are monster-only) |
| Hit points | Flat adventurer bonus (11) + class amount per level (HP tier 6 / 7 / 8) + Constitution per level, at least 1 per level in total; never rolled; at least 1 per level; heroes and companions alike (death and healing design 7) |
| Armour class | Unarmoured: 10 + Dexterity. Light: armour value + Dexterity. Medium: armour value + Dexterity up to +2. Heavy: flat armour value; heavy ignores Dexterity, negative included; below the Strength threshold the wearer is Strained. Values by category and proficiency band (armour design 6.2). A shield adds on top. |
| Attack bonus | Ability + proficiency if trained in the weapon |
| Ability difficulty | 8 + proficiency + the class's main ability. One formula for spells and martial abilities. |
| Fortitude | Better of Strength and Constitution |
| Reflex | Better of Dexterity and Intelligence |
| Will | Better of Wisdom and Presence |
| Initiative | d20 + Intelligence, rolled once per fight |
| Speed | 6 squares. Changed only by species traits, monster role or traits, armour and conditions; affixes are traits (enemies design 8.3). |
| Passive perception | The best party member's full passive: 10 + Wisdom + proficiency if trained (double with mastery) + active effects (enemies design 12.2, 13.3); against a lurker, the highest over the living, non-Downed units with line of sight (EP-3, XI-2; cross-document comparison 2026-11-23, CC-18) |
| Passive stealth | The worst party member's full passive: the minimum, over members, of 10 + Dexterity + proficiency if trained in Stealth (double with mastery) + active effects, less Clatter (the heavy armour penalty, sized in armour design 7.3) (cross-document comparison 2026-11-23, CC-17) |
| Trap sense | The highest, over the living, non-Downed units in the party roster (companions included, whichever unit has line of sight), of the formulas that apply to each trap: 10 + Intelligence for anyone; 10 + Intelligence + proficiency for a Mechanics-trained unit (double with mastery); for traps tagged `rune`, also 10 + Intelligence + Arcana proficiency; for `poison` traps, also passive Medicine (10 + Wisdom + proficiency). Detection grades never drop. Perception finds lurkers, not traps. Trap Eye (SH-19) is re-scoped by the Shade owner (recommended: double proficiency on trap sense). M2 (traps and locks design 6.4; TL-6) |
| Guile (monsters) | 10 + Wisdom + proficiency + role adjustment, for every monster whether or not trained in Perception (enemies design 13.3) |
| Recovery | Hit points restored per short rest; grows with level and Constitution = `recovery.base` + `recovery.perLevel` × level + Constitution, minimum 1 (values in the death and healing design 8) |
| Carrying capacity | Pack slots: max(2, 3 + Strength) *(illustr. base 3, minimum 2)*, holding items of size 1 or 2. Belt, trinkets, weapon sets (active and one spare set, shield included) and armour sit outside the pack. Reads Strength excluding conditions. No weights. (items design 5) |

All rows **(agreed)**, with the unarmoured row a **(lead's call)**. The trap-sense
row was changed by TL-6 (traps and locks design 6.4), agreed by the client
on 2026-11-21.

Rules behind the table:

- **Nothing derived is stored.** Each number is computed on demand from base
  abilities plus active effects. **(agreed)**
- **One roll per contest.** Exactly one d20 is rolled, against a fixed
  number. There are no opposed rolls. **(agreed)** The three forms are
  **(lead's call)**:
  - *Attack:* the attacker rolls attack bonus against armour class.
  - *Save:* the defender rolls the save against the attacker's ability
    difficulty.
  - *Skill against a unit:* the user rolls the skill against 10 + the
    target's matching save, including its save training. Shove and grapple
    meet Fortitude; slipping past meets Reflex; frightening meets Will.
    Hiding and noticing meet passive perception or passive stealth.
- **Each class is trained in one save**, which adds proficiency. **(agreed)**
- **A passive value includes proficiency** when the member providing it is
  trained in the matching skill, and double proficiency with mastery.
  **(lead's call)** A party value taken from the best member is the
  maximum, over eligible members, of each member's full passive (10 +
  ability + proficiency or double proficiency + active effects). This also
  applies to party passive perception against lurkers (enemies design
  12.2, 13.3). A party value taken from the worst member (passive
  stealth) is likewise the minimum, over members, of each member's full
  passive (cross-document comparison 2026-11-23, CC-17).
- **Readings (TL-34; segment 8; M3; accepted by segment 8, events
  design 4.9, EV-30, 2026-11-22):** readings, one-off room
  checks, as no-roll thresholds on passives: Arcana reads runes and
  shrines, Lore reads inscriptions, Mechanics reads mechanisms, each
  yielding a floor fact. One Reading per room, the second F3 Q room's M3
  use; content is segment 8's (traps and locks design 6.12). From M2,
  Curiosity options use the same threshold principle, passive = 10 +
  modifier (events design 4.3.2; EV-5).
- **Speed is not tied to an ability.** **(agreed)**
- **Concentration is a Constitution check**, not a Fortitude save, so
  Strength cannot stand in for it. **(agreed)**
- **Armour is hit or miss.** It never reduces damage. **(agreed)**
- **Initiative ties** break by higher Intelligence, then by a fixed unit
  order held in the state. **(lead's call)**
- **Natural 20 and natural 1** apply to attack rolls only: a 20 always hits
  and is a critical hit, a 1 always misses. Saves and skill checks have no
  automatic results. **(lead's call)** A critical hit rolls all damage dice twice, for
  every unit (death and healing design 9).
- **Hit points gained per level are at least 1**, whatever the Constitution.
  **(lead's call)**

## 7. Skills

### 7.1 The list

**(agreed)**

| Skill | Ability | In the dungeon | In a fight |
|---|---|---|---|
| Athletics | Strength | Force doors, bars and chests (loud; noise per try, each failure lowers the difficulty and the third try always opens, M1; traps and locks design 6.11; TL-21); jump crossable hazards | Shove, grapple, resist being moved; M2: Shunt, Kick and Smash Furnishings (heavy pieces against heft; Attack Shunt's Prone save is against 8 + the Shunter's Athletics bonus); M3: Overturn a tippable Tall Furnishing (heft 15, *illustr.*) and Haul (room terrain design 6.3) |
| Acrobatics | Dexterity | Cross narrow or unstable ground: from M2, entering a Plank (narrow ground) in combat needs Acrobatics against the segment-5 ladder; a failure ends movement on the square before, with no retry that round, no fall and no damage; out of combat the leading unit checks once (room terrain design 6.2.6); pass a Squeeze: out of combat the leading unit makes one check for the party, and a failure still gets through, at a time and noise cost; in combat each unit checks as it enters; monsters never use one (floor layout design 9.2) | Slip past enemies, escape grapples, avoid attacks when disengaging |
| Stealth | Dexterity | Sneak past, set ambushes, steal from unaware enemies: only from a Dormant, Stationary or Patrolling carrier; adjacent; no other awake group member has line of sight; any party member's Stealth against the carrier's passive perception (−5 if Dormant, +5 if Keen), with disadvantage against an awake carrier; one try per carrier per floor; failure starts combat with nobody Surprised (enemies design 15.5); M3: a Dormant Windfall Truant can also be stolen from: adjacent; no awake un-Cowed hostile in its room has line of sight; Stealth against its fixed passive 15 (20, −5 Dormant; *illustr.*; a fixed value, never Keen); success takes its card only, its coin spilling as a guarded pile; the party has one Steal try per room, on a carrier in the host group or the Truant, not both (loot sources design 4.7.8; LP-6; agreed 2026-11-22, its Q1) | Hide to break line of sight and strike unseen. A class feature may let Hide succeed without breaking line of sight (classes design 8.4, Vanish); the Stealth roll and Hidden's end rules (conditions 18.7) stay |
| Mechanics | Intelligence | Find and disarm traps, pick locks (quiet; a Locked door is a Door with the padlock decal, on optional content only; floor layout design 9.2; one roll per lock per floor under the Margin rule, M1; traps and locks design 6.6, 6.11; TL-21), find secret doors: a member trained in Mechanics within 3 squares with line of sight to a Hairline secret's tell reveals the secret automatically, and Sound the wall opens a secret only at its tell (no roll; floor layout design 9.2); M3: the Lodestring never points at secret caches, so finding them stays this lane's (exploration and information design 4.6.1; XI-22; agreed 2026-11-23, its Q5; read, not changed) | Re-arm or trigger traps against enemies |
| Arcana | Intelligence | Identify magic items, read runes and shrines (also trap sense against `rune` traps; traps and locks design 6.4) | Use scrolls and wands outside your class; recognise a spell being cast |
| Lore | Intelligence | Know what a floor's theme is likely to hold: the roster's types and roles are revealed on entry (enemies design 10.6); M3: the next floor's roles are also shown at camp (run structure design 6.9; RT-10). It shows types only, never whether a boxed lurker is on the floor (traps and locks design 6.14; read, not changed) | Reveal an enemy's traits, resistances, weaknesses and abilities |
| Perception | Wisdom | Hear what is behind a door: Listen at a closed door (no roll) reports the groups in the room beyond at that moment, passively for a party with a member trained in Perception, who also hears their size band (floor layout design 9.2); spot hidden enemies. M3, conditional: the Ear Trumpet is a lane-gap item stocked only for a party with no Perception-trained unit, its effect suspended while the lane is present (exploration and information design 4.5.2; XI-26; agreed 2026-11-23, its Q3; this list kept, not changed) | Avoid surprise; find hidden attackers; read an enemy's intent when the party's best passive Perception is at least its guile (enemies design 13.3) |
| Medicine | Wisdom | Stretch short-rest supplies; cure poison and disease (passive Medicine also counts as trap sense against `poison` traps; traps and locks design 6.4); at a short rest, Treat ends Diseased and Scarred (fixed difficulty, one attempt per Scarred unit per rest), except a talisman's `treatProof` Scarred, which is not offered and uses no attempt (consumables design 14.6) | Get a downed companion back up, at most once per companion per fight (death and healing design 11.2); Treat (an action, adjacent): one roll against each instance's difficulty ends poison conditions, Bleeding, Withered, Hobbled, Condemned and Calcifying at stages 1–2 (conditions design 8) |
| Intimidation | Presence | Make weak groups back off without a fight; cow eligibility and the `broken` flag (enemies design 14.8) | Frighten enemies; break morale when a leader falls; Demand Surrender (enemies design 14.6) |
| Leadership | Presence | Better recruits (Recruiter's Eye, companions design 9.2), shop prices and event outcomes | Rally: steady an ally or give them a free step |

Leadership's "event outcomes" (and Presence's in 5.2) are one hero-only
threshold on one Curiosity option, with no dialogue tree and no roll, so
the social-skills non-goal in 2 stands (events design 4.3.2; EV-6; M2;
agreed 2026-11-22, a reading pending confirmation by this document's
owner).

Constitution has no skill; its three jobs are passive. Strength has one broad
skill.

### 7.2 Rules

- A skill check is d20 + ability + proficiency if trained, against a
  difficulty. **(agreed)**
- **Training does two things:** it adds proficiency, and it unlocks uses an
  untrained character cannot attempt at all. **(agreed)** The trained-only
  uses are: picking a lock, disarming or re-arming a trap, getting a downed
  companion up, and using a scroll or wand outside your class. **(lead's
  call)**
- **Mastery** is a second tier: double proficiency plus one signature use
  per skill. It is earned through pick-one-of-three offers. **(agreed)**
  Perception mastery's signature use is fixed as **Tactician**: readable
  enemies also show the ability, and reading extends to the next 3 enemies
  in initiative (enemies design 13.4). The other skills' signature uses
  stay the builders'. For Mechanics, Second Nature is proposed to the
  builders (traps and locks design 9.2; TL-36, M2).
- **Sources:** a class grants two or three skills from its own list. Offers
  and the starting boon add more. Companions come with fixed skills,
  plus one flex skill chosen on the join card from two options on its
  class list, fixed from then on (companions design 8.1). **(agreed)**
- **No free retries.** Exploration has no clock, so a failed check always
  costs something: noise, a sprung trap, a jammed lock. Where no cost fits,
  the check is a threshold with no roll. **(agreed)** For traps and locks
  this is met by the Margin rule, one roll per object per hand verb per
  floor, per-try force noise and Take time's ticks (traps and locks
  design 6.1, 6.6; TL-1, TL-4, TL-5; read, not changed). For Curiosities
  it is met by a printed price on every option but the free Leave, which
  closes the card without resolving it, and one attempt only (events
  design 4.3.1; EV-4; read, not changed). Exploration keeps no visible
  floor timer (exploration and information design 4.4.1, IN1; XI-14; read,
  not changed).
- **Lore never hides the basics.** Hit points and anything already seen in
  play are visible to everyone. Lore reveals the rest early. **(agreed)**

## 8. Damage types and resistances

### 8.1 Types

Ten types in three families. Each has a rider, a secondary effect that is
its identity. Names are placeholders; theming comes later. **(agreed)**

| Family | Type | Rider |
|---|---|---|
| Physical | Slashing | Bleed: damage over time |
| Physical | Piercing | Ignores part of the target's armour |
| Physical | Bludgeoning | Stagger: no reactions until the end of the target's next turn; shatters brittle or frozen targets |
| Elemental | Fire | Burning: damage over time that can spread to adjacent units |
| Elemental | Cold | Slows movement |
| Elemental | Lightning | Arcs to an adjacent unit. The arc deals the rider strength as lightning, one damage event per arced unit, no crit, no rider flag, never re-arcs; an at-will's Soaked arc spread happens at most once per turn across beams (Arcanist design 8.1; ARC-14, lead's call) |
| Elemental | Acid | Corrodes: lowers armour class and strips layers |
| Spirit | Psychic | Breaks concentration and applies Unfocused for 1 turn; attempted on a concentrating target or one not already Unfocused |
| Spirit | Radiant | Revealed on a hidden or invisible target; Blinded otherwise |
| Spirit | Necrotic | Blocks healing |

Poison is a condition, not a damage type. **(agreed)**

- **Open, pending ruling (RT-15; conditions owner):** whether radiant's
  rider counts 2 toward a theme's 6 harmful conditions, and whether
  Revealed is "harmful" at all. No M2 theme deals radiant; the ruling
  blocks only the M3 Mirror Court and Moth Wedding (run structure design
  8.0, 8.12).

### 8.2 When a rider applies

- Against a target **weak** to the type, any hit that deals damage of that
  type lands the rider automatically, with no save. This includes plain
  attacks that carry no rider of their own: a torch swung at a fire-weak
  enemy sets it burning. **(agreed)**
- Against any other target, a rider is only attempted when the ability or
  weapon says it carries one, or on a critical hit. The target then gets a
  save against the attacker's ability difficulty. Plain damage does not
  roll a rider every time, which would bury fights in saves and conditions.
  **(agreed that a save is needed; when it is attempted is the lead's
  call)**
- Piercing is the exception in form: its rider acts on the attack roll, so
  it is an always-on reduction to the target's armour value: 1 against any
  armour value, +1 against `CountsAsHeavy`, +1 against a weak target, ×2
  with Puncture; it shares a reduction cap of 4 with Pitted Armour (armour
  design 9.1). **(lead's call; sizing agreed 2026-11-17, armour design
  Q3)**
- Alembic exception, "your mix needs a spark" (Arcanist design 9.2;
  ALM-1): a condition instance with `origin: prime` and `source: you` is
  treated as absent by every damage part of your own at-wills (weapon
  attacks included), so it grants no automatic rider. A rider your own
  at-will would apply is not attempted on a target holding your inert
  reagent where a conditions 19 cancel, override or wash row would fire
  against it; the once-per-turn rider attempt is not spent. Your slot
  spells, Catalyse, Overchannel, allies and consumables read it normally.
  **(agreed 2026-11-20, Arcanist Q3; the rider sentence told plainly and
  confirmed by the client 2026-11-20)**

### 8.3 The ladder

**(agreed)**

| Step | Effect |
|---|---|
| Weak | Damage + half |
| Normal | Damage as rolled |
| Resistant | Half damage |
| Immune | No damage. Bosses only, plus the mindless exception below. |

- Two sources of the same resistance count once. **(agreed)**
- A resistance and a weakness to the same type cancel to normal. **(agreed)**
- Order of operations: flat changes first, then the ladder step, rounded
  down. **(agreed)**
- Damage that is not immune is at least 1. **(lead's call)**
- Mixed damage resolves each part separately: a flaming sword's slashing
  and fire portions each meet their own step. **(agreed)**
- The party follows the same rules. Armour and items grant resistances.
  **(agreed)**
- Your own inert prime (Alembic, 8.2) gives your own at-wills no weakness
  step and no resistance, and your at-will damage does not end it
  (Arcanist design 9.2; ALM-1). **(agreed 2026-11-20, Arcanist Q3)**
- Arcs and allies (Arcanist design 8.1; ARC-8): at-will arcs never reach
  allies, the Soaked "arcs to every adjacent unit" row included (ability
  8.2 wins); a selective cast leaves allies out of all its effects, arcs,
  forks and forced movement included, and selective may be taken on any
  slot cast whose arcs or forks reach an ally; you are never affected by
  your own casts' areas, arcs and forks. This narrows how the ladder reads
  for allies and changes no number. **(lead's call; told plainly and
  confirmed by the client 2026-11-20)**

### 8.4 Enemy traits

- Resistances and weaknesses come from **traits**, not per-monster lists. A
  trait such as "skeletal" or "armoured" always means the same profile, so
  the player learns a few rules once. **(agreed)** The `armoured` trait's
  profile makes `CountsAsHeavy` true (Corroded, piercing, Clatter); it
  never costs speed (armour design 7.3).
- Budget per enemy: at most two resistances, and at least one weakness
  alongside any resistance. **(agreed)** Ladder shifts that come from
  conditions (Soaked, Oiled, Exposed, Resisting, Shredded, Sapped,
  Shielded, Raging, Petrified, Ill-Starred and Frozen's shell) are not
  traits or layers and do not count toward the two (conditions design
  12.2).
- Condition-tag immunities: at most two per non-boss trait set, at most one
  of them a control tag; the theme rule below applies to them too
  (conditions design 12.2).
- No floor theme may have every enemy resist the same type. **(agreed)**
- A "mindless" trait grants immunity to psychic and to the `mind`
  condition tag. Psychic is the one damage-type immunity allowed outside
  bosses, and the theme rule above still applies to it. **(lead's call)**
  The floor-1 boss is never `mindless` and never psychic-immune
  (class-blind; a `mind` immunity alone is allowed), checked by
  `Floor1BossMindReachable` (enemies design 17.5, 20.1; Herald design
  7.16, HR-21). **(agreed 2026-11-20, Herald Q3)**
- Specialists can buy through: an offer such as "your fire ignores
  resistance" exists for committed builds. **(agreed)** "Ignores
  resistance" applies after the ladder and the cancel rule: a final
  Resistant reads Normal, never Weak; it covers trait and layer
  resistance (the layer stays unbroken); never condition-based shifts
  (Soaked's fire resistance, Frozen's shell, Resisting, Shielded); never
  immunity; `IgnoreResistanceSources ≤ 1` per hero (Arcanist design 9.4;
  EL-1, owed at M2). **(lead's call)** The Elementalist's Purity applies
  it to its heart family when that aspect ships (Arcanist design 9.4).

### 8.5 Layers

**(agreed)**

A layer is a resistance that holds until it is broken. A layer names the
types it resists and the types that break it. Ice armour melts to fire; a
stone shell cracks to bludgeoning; acid strips most layers.

- A layer breaks when a breaking type deals at least 1 damage to the unit.
  **(lead's call)**
- A broken layer stays broken for the rest of the fight. **(lead's call)**
- A layer's resistances count toward the enemy's budget of two. **(lead's
  call)** A ladder shift from a condition is not a layer and does not count
  (conditions design 12.2).

## 9. How this sits in the core

All **(lead's call)**, following the main design's architecture.

- Abilities, saves, skills and damage types are fixed lists in core code,
  because rules refer to them by name. Classes, species, traits, layers and
  items are data.
- A unit stores its six base abilities, level, class and trained skills.
  Everything in section 6 is a query that folds in modifiers from active
  effects. The non-stacking rule for abilities lives in that query.
- Other derived numbers stack normally unless a rule above says otherwise.
  Modifiers from conditions follow the same-number rule instead: only the
  worst penalty and the best bonus to each derived number apply, only the
  highest dice bonus to a roll, advantage and disadvantage as in 5E, and at
  most one speed halving, with speed 0 overriding everything (conditions
  design 6.7). Gear and traits stack as before.
- Damage travels as a list of typed parts. Each part passes through flat
  modifiers, then the target's ladder step for its type.
- A trait is a named bundle of effects. A layer is an effect with a break
  trigger.
- Content validation enforces the budget in 8.4 and the theme rule, and
  fails the build when broken.
- The display layer asks the core for every number it shows, including "what
  would +1 Strength change?" at level-up.

Suggested staging against the main milestones:

- **M1:** abilities, derived numbers, saves, skill checks for the skills the
  slice's content uses, the ladder, traits.
- **M2:** riders, layers, mastery, the remaining skills.

Until riders arrive in M2, a weakness in M1 is extra damage only. The damage
step should leave a clear place for the rider to be added.

## 10. Depends on systems not yet designed

Each item here is a commitment this model makes on another system's behalf.

- **Ability and resource model.** Settled in
  `2026-11-16-ability-resource-model-design.md`: skill action costs (its
  7.2), which abilities carry riders (its 9), and casting (its 6.5).
- **Death and healing.** Settled in `2026-11-17-death-healing-design.md`.
  Level 1 is fragile and death ends the run. Every party unit gets a flat
  hit point bonus (11) at every level (agreed 2026-11-17). A
  companion that gets up from its first downing on a floor gains Scarred
  (maximum hit points −10% until camp; Treat at a short rest removes it);
  settled in conditions design 18.10.
- **Noticing.** Settled in enemies design 12.2: each unaware group rolls
  once per trigger with its best member in line of sight. Triggers: first
  line of sight, 6- and 3-square bands, noise, attack. The party notices
  non-hiding enemies automatically on line of sight; hiding enemies roll
  Stealth against party passive perception.
- **Morale.** Intimidation needs enemies that can flee or back down.
  **(agreed)** Settled in enemies design 14.
- **Dungeon content.** Locks, traps, secrets, noise and non-combat
  encounters must appear on most floors, or the skills are decoration.
  Traps, locks, doors and containers are settled in
  `2026-11-21-traps-locks-design.md` (its 6, 17). Non-combat encounters
  are settled in `2026-11-22-events-design.md`: about one Curiosity per
  floor on F1–3 and Interludes on F2–5 (its 4.2, 4.13; M2).
- **Items and inventory.** Settled in
  `2026-11-16-items-inventory-design.md`: a spare weapon is one spare
  weapon set (main + off hand, shield included), and the pack holds
  max(2, 3 + Strength) *(illustr.)* slots of size-1 or size-2 items, with
  belt, trinkets, weapon sets and armour outside it (its 5). Weapons and
  foci are settled in `2026-11-17-weapons-design.md`; Disarmed and Coated
  stay Later. Armour and shields, including armour values and Strength
  thresholds, are settled in `2026-11-17-armour-design.md` (its 6.2, 7.1).
  Consumables, scrolls and wands are settled in the consumables design
  (2026-11-17); coatings and Coated stay Later. What remains open moves to
  the category designs (items design 17): starting kits and Clinging.
  Pitted Armour is in armour 9.4 and stays Later (AR-37; cross-document
  comparison 2026-11-23, CC-14).
- **Conditions.** Settled in `2026-11-16-conditions-design.md`: bleed,
  burning, slow, stagger, blind, frightened and the rest are defined there;
  this document only names them.

## 11. Testing

- Unit tests for each derived number, the non-stacking rule, the ladder and
  its order of operations, cancelling, mixed damage and layer breaking.
- Content validation for the trait budget and the theme rule.
- Simulator reports to watch, each tied to a known risk:
  - win rate by starting spread, to catch a compulsory or dump ability;
  - Intelligence, which now has initiative and three skills;
  - Constitution, in case it is still a tax;
  - single-type builds against each floor theme, to confirm they are slowed
    and not stopped.

## 12. Left to the builders

- All numbers: skill difficulties, rider strengths and
  durations, base carrying slots (pack base 3, minimum 2, *illustr.*; items
  design 5.1). Armour values and Strength thresholds are set in the armour
  design (6.2, 7.1). Class hit points are not builders' numbers: the
  flat 11 and the HP tiers 6 / 7 / 8 are the client's, and Recovery 4 / 2
  (`recovery.base`, `recovery.perLevel`) the lead's (death and healing
  design 7.1, 8) (cross-document comparison 2026-11-23, CC-15).
- Which save each rider calls for.
- The signature use for each skill's mastery, except Perception's
  (Tactician, section 7.2).
- The trait list and each trait's profile.
- How far an effect can push an ability below −1.
- The level-up and character sheet layout.

## 13. Changes this makes to the main design

Applied to `2026-11-15-dungeon-rpg-design.md` on 2026-11-16.

- "Race" becomes "species" throughout.
- Section 4.3: noticing uses one roll against a passive value, per
  section 6.
- Section 10: points here for everything this document settles.

## 14. Open items for the client

None at the time of writing.
