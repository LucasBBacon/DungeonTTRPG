# Classes, Stage 1: General Class Concepts: Design

Date: 2026-11-17
Status: Approved, 2026-11-18 (drafted 2026-11-17; revised 2026-11-18, appendix A, R-n)
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design),
`2026-11-17-consumables-design.md` (consumables design) and
`2026-11-17-death-healing-design.md` (death and healing design)

Items marked **(agreed)** were decided by the client on 2026-11-17 for
the five questions this design raised (Q1–Q5, section 4 and section 20),
by choosing the lead's recommendation in each case; the client also said
that everything else in the lead's proposal stands. Items marked
**(lead's call)** were filled in by the design lead; the client
confirmed all of them when approving this document on 2026-11-18.
Items marked **(settled)** restate a rule from a parent document;
**(decided)** marks a main-design client decision. Items marked
**(change CL-n)** amend an approved document and are listed, with an id
and the exact section they touch, in section 18. Once approved, every
item here is binding on the builders.

This is **stage 1** of the classes design: the general class concepts.
It settles the "lean, never require" rule and the utility map, the
answer to the client's identity-timing question (section 5), the class
structure (launch four, subclasses, the published level ladder, offers,
multiclass hooks, companions, species), one concept card per launch
class, and how every binding requirement the approved documents place on
"the classes design" is met. **Stage 2**, the full class tables, comes
later (section 14): dice bands, pool sizes, AC numbers, spell lists,
talent menus and capstone texts belong there. Where this document gives
one, it only shows the shape.

The client's question (section 5): "Should the player feel the
difference between classes from the first level, or should we allow
these differences to show and develop as the player puts more levels
into a class?"

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. The stage-2 class
tables own them and the simulator calibrates them (section 16), except
where a value is marked as a client decision or a lead's number.

**Names are placeholders** pending a naming pass (a separate client
item, section 14). They are frozen for stage 1 so that documents agree,
and they already pass the `NameCollision` check (section 10).

**SRD check (lead's call).** Several mechanisms here are adapted from SRD
shapes: Second Wind, Action Surge, Extra Attack, Sneak Attack, Cunning
Action, Shield, Healing Word, Cause Fear, Hunter's Mark, Rage, Unarmored
Defense and Mage Armor. They were written from memory. **Every
SRD-shaped mechanism must be checked against the SRD 5.2.1 document (or
5.1) before any record is written**, and every record flagged `srd`
cites the entry it was checked against. Class, subclass and ability names
are original; no D&D-only name or text is used. **(settled principle,
main design 2; ability model 17)**

**Waves:** M1 is the vertical slice (one class, levels 1–3, solo), M2
the full run with four classes and companions, M3 profile and unlocks,
M4 breadth (species, multiclassing), Later everything after that no
milestone names. **(settled, main design 9)**

## 1. Intent

Classes are where the player's build lives. The parents already fix most
of a class's frame: one main ability, one trained save, two or three
skills, an HP tier, a `rest` and a `camp` pool, at least two at-wills,
one `rest` defensive tool, armour and weapon trainings, a subclass at
level 3, a capstone at level 9 and a cap of eight limited abilities.
**(settled)** They leave open which four classes ship, what each one
*is*, when the player feels it, and how classes lean on shared utilities
without any of them becoming compulsory. This document closes those
questions at the level of concepts.

The system should make **every class distinct in kind from turn 1, close
in power at every level, and never required**: a party of any four
classes, or a solo hero of any class, can win; a class makes some things
better, never possible only with it. **(lead's call)**

## 2. Goals, non-goals, anti-goals

### Goals

- **Felt on turn 1.** Each class plays differently in its first fight,
  in *how* the player acts, not in bigger numbers. **(agreed, Q2)**
- **Growth in degree and direction.** Levels add uses, dice and riders
  (degree) and the subclass at level 3 picks the second lean (direction).
  **(agreed, Q2)**
- **Lean, never require.** No utility, healing included, needs a
  particular class. **(agreed, Q3)**
- **A choice every level, under a minute.** One published ladder, one
  choice per level. **(settled, main design 2, 4.5; lead's call on the
  ladder)**
- **Few classes, wide branching.** Four classes at M2; variety from class
  × subclass × species × offers × gear. **(lead's call)**
- **Multiclass-safe tables.** Built now so either multiclass model is safe
  when it is decided at M4. **(lead's call)**

### Non-goals

- **Full class tables.** Stage 2.
- **The multiclass model itself.** Decided at M4 (7.7). **(settled, main
  design 4.5)**
- **Half casters, pact casters** at launch. **(settled, ability model
  6.5)**
- **Summons and pets.** There is no summon system (consumables 16).
  **(settled)**
- **A class that is a role-lock**, such as a pure healer or a pure tank.
  **(lead's call)**

### Anti-goals

- **A required class.** A party that lacks a class must lose only
  efficiency. **(agreed, Q3)**
- **Generic floor 1.** A class that only becomes itself by level 3 plays
  generic for the most-replayed floor of a permadeath game. **(agreed,
  Q2)**
- **One-level dips** that copy a front-loaded chassis. **(lead's call)**
- **Uniform resource shapes** that erase identity (the 4e lesson).
  **(lead's call)**
- **Bring-the-right-key play** through per-enemy damage-type switching.
  **(settled, stat model 2)**
- **Decision fatigue:** more than four things to read per unit.
  **(settled, ability model 11.4)**
- **Protected content.** **(settled)**

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Main design 4.1, 4.5, 4.7, 9 (decided) | 5 floors **(change RT-1; run structure design 6.1)**, about level 10, a level-up every 8–10 minutes; species and class at creation; four classes in the first full run, more through unlocks; a choice every level; multiclassing only once single-class paths are fun | Launch four (7.1); the ladder (7.4); unlocks (9); multiclass guards (7.7) |
| Main design 4.2 | The player builds one hero in full; companions have a fixed class and lighter progression | Companion kits (7.8) |
| Stat model 5.1, 5.4–5.6 | Starting spread, the class suggests where values go; species give no points; Presence is always the hero's; each ability a main or a sensible second (target) | Suggested arrays (8); Herald (8.6); species (7.9) |
| Stat model 6, 7.2 | One trained save per class; ability difficulty from the class's main ability; 2–3 skills from the class list; mastery only through offers | Save principle (8.2); skill lists (8) |
| Stat model 8.1 | Ten damage types; force is not one | Fallback types (6.5) |
| Ability model 6.1–6.7 | At least 2 at-wills, one damaging; `rest` and `camp` pools, 40–60% camp; one camp spend per turn; four slot tiers; at most 3 named martial pools; capstone a `camp` pool of 1 at level 9 | Chassis rules (8.2) |
| Ability model 7.2, 7.3 | Hide as a bonus action with a class feature (Shadowed armour also gives one per fight, armour 14.2); a policy on every reaction; no nesting; one reaction AC bonus per attack | Shade Tricks; reaction policies |
| Ability model 10.1–10.3, 11 | Subclass at 3; +1 ability at 4 and 8; offers fill gaps; cap of 8 limited abilities; level-up cards; companion kits | Ladder (7.4); cap counting (7.5); companions (7.8) |
| Conditions 7, 16, 18.1 | Hard control only from limited abilities; agency conditions enemy-only; a 32-condition M1/M2 budget, full | Only M1/M2 conditions are used (11.B) |
| Enemies 11.3, 14 | The AI never reads class; morale states, triggers T1–T9, cowing | Protection via Challenged; the Herald (8.6) |
| Weapons 6.3, 6.4, 7.3 | Exploit is core; every Dexterity-main class has a precision feature with evidence (agreed); classes own depth, never access | The Shade's Opening (8.4) |
| Armour 7.2, 10.4, 11 | Training ceilings; one reaction AC bonus per attack; a ceiling below light needs a defensive feature with evidence | Ceilings (8); Arcanist light (agreed, Q5) |
| Trinkets 11 | Copy registry, bidirectional; classes build on trinket outputs, never ids; requirements proven with empty trinket slots | `effectKinds` on every feature (12) |
| Consumables 13.1, 13.8 | `classTags` from class spell lists; DEP-1, a launch non-caster with Arcana | The Shade has Arcana; the `classTags` audit (14) |
| Death and healing 7.1, 12.1, 12.2, 18.1, 21.1 | HP tier per class; exactly one `restDefensiveTool`, at most half Recovery unless fighter-type; heals exactly the target's Recovery, higher tiers add targets; at most one healing-lean class, gap ≤ 10 points (agreed); `Floor1BossSustain` | The four rest tools (8); the Herald's heal cards (8.6) |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored.

### 3.3 Findings in the parent documents

Gaps found while designing; each is fixed by a change in section 18.
**(lead's call)** Ids PF1–PF11 are local to this table.

| # | Finding | Fix |
|---|---|---|
| PF1 | Enemies 14.3 makes T6 (a limited fear effect on a Demoralised enemy) "Later (spells)", so a Presence caster's fear spells cannot rout at M2 | T6 for class fear spells moves to M2 (CL-1) |
| PF2 | Ability 10.2's cap of eight counts every limited ability but names no unit; a caster's slot tiers plus spells known would pass it by level 7, and abilities sharing one pool are counted in different ways | One unit: a named pool, with every ability drawing on it as an option, counts as one limited ability; a slot tier is such a pool (CL-2) |
| PF3 | Offers can only raise numbers or add abilities; levels 6–8 have no offer, and a unit at its reading limit (7.6) can take no new ability, so the middle levels flatten | A "trigger" offer kind, legal at any level and below the cap, with a trigger option on every level-7 talent menu and spell-card draw (CL-3; 7.5). Added Opening triggers arrive at level 5 (`TriggerFromFive`; change SH-23, Shade design 6.6) |
| PF4 | Ability 6.7 makes level 9 an automatic capstone with no choice, against "a choice every level" | Level 9 adds a pick-one-of-three offer; 6.7 unchanged (CL-4, agreed Q4) |
| PF5 | Spurred has one source (Rally); a class cannot grant it | A class and presence-spell source (CL-5) |
| PF6 | A move-ending opportunity attack has no rule; speed may be changed only by species, armour and conditions | A class reaction may set the mover's remaining movement for the turn to 0 without changing speed (CL-6) |
| PF7 | Armour's Guarding row is illegal on a carrier with Interpose, but says nothing on a class Interpose that is learned and not active | Legality reads "learned" (CL-7) |
| PF8 | Weapons 6.4's parity evidence is silent on reaction attacks, on solo against party contexts and on multiclass builds | Narrowed evidence rules (CL-8) |
| PF9 | DH 18.1's healer gate cannot tell a Herald's healing from the Herald itself | Herald arms and a healer count as reports beside the unchanged gate (CL-9) |
| PF10 | The cowable-group guarantee (enemies 10.4, 14.8) does not say where the floor-1 group sits; every boss is `fearless` (enemies 14.2), so a Herald can reach the solo boss with nothing to break | Floor 1's guaranteed cowable group lies on the main path before the boss room (CL-10) |
| PF11 | The copy registry covers classes, trinkets and consumables (consumables 6.4), not species traits or armour and shield signatures | The registry covers them in every direction (CL-11) |

### 3.4 Terms

**(lead's call)** Used with these meanings everywhere below.

- **Once per round:** a pool of 1 with refill `turn` (ability 6.2): it
  refills at the start of its owner's turn, so it covers the owner's turn
  and every other unit's turn up to the owner's next. Reaction uses spend
  it too.
- **Once per turn:** a per-unit flag cleared at the start of every unit's
  turn, the meaning of weapons 6.3 and ability 6.4 ("the turn now being
  taken, by any unit").
- Every class rider is tagged with one of the two. Opening, the
  Weaponmaster's free empower, the Alembic's priming and Drive Through's
  Shove are **once per round**; Exploit stays **once per turn** as weapons
  6.3 states.
- **Granted attack:** an attack resolved because of another unit's
  ability (for example the Warcaller's grant) carries `grantedBy:
  unitId`. Class riders (Opening, Techniques and every other class rider)
  never apply to it. Exploit reads nothing from the flag and applies as
  weapons 6.3 states.
- **Limited ability (the unit of the cap of eight, CL-2):** one named
  pool. Every ability record that draws on the pool is an option of it,
  and a slot tier's pool counts as one. An offer or talent that adds an
  ability to an existing pool adds an option, not a limited ability. The
  per-rest sub-limit of a `restDefensiveTool` (8.2) is not a named pool
  and is not counted. **(change VG-10)** Nor is a per-rest sub-limit on
  one option of a named pool (the Vanguard's Hew-use cap). **(change
  SH-4)** A per-rest counter on one named pool's record, spent by records
  tagged with its one declared tag, whether or not they spend from the
  pool, is a frequency limit and counts 0; it is not a named pool (the
  Shade's strike limit, Shade design 7.9).
- **Turn allowance and per-target round count (change VG-10):** a turn
  allowance (a `turn` pool of 1 on its owner that only limits an option,
  passive or reaction to once per round) and a per-target round count
  (Topple's) are frequency limits, not named pools. They are outside the
  cap of eight and the three-named-pool limit, count 0 to read when
  shown as a glint, stay outside the camp-share ratio and sit inside the
  60% formula (Vanguard design 3.4, 7.9).
- **A signature fires** in a round when any event in the class's
  `signatureEvents[]` (12.1) occurs in that round.
- **Interplay (change ARC-18):** the Arcanist's base passive
  (`interplay.enabled`, on by default). Damage of one pair family to a
  hostile on your turn sets your one source-held mark on it; your next
  hit on it with a part of the other family, before the end of your next
  turn, is rider-flagged. **Fused:** an Overchannel cast's pair-family
  parts count as both families for Interplay (Arcanist design 7.9,
  10.3).

## 4. Framework decisions

The client chose Q1–Q5 on 2026-11-17, each time the lead's
recommendation. **(agreed)** The rest are the lead's calls, which the
client said stand.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Launch four (Q1) | **Vanguard (Strength; the M1 slice class), Shade (Dexterity), Arcanist (Intelligence), Herald (Presence)**, with the single light healing lean on the Herald's spell cards and its Hearth subclass (7.1, 8) **(agreed)** | B, Votary for the Herald (Presence secondary until unlocks; healing on a Wisdom divine class). C, a Herald with no heal cards (the 10-point healer gap passes without being tested). D, Sapper for the Shade (no Dexterity main: fails weapons 6.4) |
| 2 | Identity timing (Q2) | **The hybrid, "verb at 1, lens at 3, mastery at 9":** distinct in kind on turn 1 and close in power; subclass at 3; capstone at 9; with the guards `Floor1ClassSpread` (10 points), about 60% of limited-power value after level 1, `SeedHasTarget` and the per-round solo boss breakdown (5) **(agreed)** | Front-loaded (subclass moved to level 2; doubled floor-1 balancing; highest dip risk; flat middle levels). Growth (cannot be built purely under the approved documents; martials generic on floor 1) |
| 3 | "Lean, never require" (Q3) | **The rule (6.1) and the forbidden grants (6.3); reliance reports for protection, control, stealth, morale and tempo at 10 points as reports, not gates; healing keeps its approved gate (DH 18.1)** **(agreed)** | Every reliance report a gate (over-constrains the four classes before any data exists). No written rule (leans drift into requirements one feature at a time) |
| 4 | Level 9 (Q4) | **Option B: one automatic capstone per class plus a pick-one-of-three offer;** ability 6.7 unchanged (CL-4) **(agreed)** | Option A, a choice of two capstones per class (amends 6.7; doubles capstone authoring for content few runs see) |
| 5 | Arcanist armour (Q5) | **Light ceiling at launch** (8.5) **(agreed)** | The Spellguard "glass mage": ceiling none, AC = 13 + Dexterity, +1 at 5 and 9 *(illustr.)*. It needs evidence at Dexterity −1 and at maximum, and tightens the enemies 6.4 generation guards for every party (armour 11 item 2) |
| 6 | Subclass level | **Level 3,** as approved (7.3) **(settled, ability 10.1)** | Level 2 (the front-loaded model) |
| 7 | One rest pool that asks a question | **Every class has one `rest` pool whose abilities compete** (Techniques, Tricks, Weft, Cadence); every `restDefensiveTool` heals (8) **(lead's call)** | Non-healing rest tools (they retune the shared floor-1 boss for everyone) |
| 8 | The ladder | **One published ladder for martials and casters** (7.4) **(lead's call)** | Per-class ladders (the lanes disagreed; harder to read and to balance) |
| 9 | The cap of eight | **A named pool counts as one limited ability, with every ability drawing on it as an option; a slot tier's pool is such a pool** (3.4, 7.5; CL-2) **(lead's call)** | Each spell known counts (casters hit the cap by level 7); each ability on a shared pool counts (both casters reach 8 by level 9 and lose their offers) |
| 10 | Multiclassing | **Built for now, decided at M4,** with dip guards (7.7) **(lead's call)** | Choosing the model now (no single-class evidence yet) |
| 11 | Companions | **A kit carries a simplified slice of its class's signature verb, no subclass field;** recruit-pool rules per floor (7.8) **(lead's call)** | Companions as class plus subclass (too much to read) |
| 12 | Species | **Baseline axes only;** in the copy registry (7.9; CL-11) **(lead's call)** | Species acting on class pools or granting signature verbs |
| 13 | Save training | **The save the main ability feeds** (8.2) **(lead's call)** | Per-class reasons (inconsistent) |
| 14 | The M1 slice class | **The Vanguard** (7.2) **(lead's call)** | A caster (pulls the staff and Concentrating into M1) |

### 4.1 How other games handle classes

Mechanism references only; no names or text are taken from any of them.
Details are as recalled unless marked "checked". Four points were
checked by web search: SRD 5.2 puts every subclass at level 3; the 5.2
fighter gets Weapon Mastery and Second Wind at level 1; Pathfinder 2e
dedication feats come from level 2 and block a second dedication;
Baldur's Gate 3 builds are shaped by one-level
dips (community sources). Anything SRD-shaped is checked against SRD
5.2.1 before data. **(lead's call)**

| Game | What it does | We take | We avoid |
|---|---|---|---|
| **D&D 5E / SRD 5.2** (checked in part) | Signature features at level 1 (Second Wind, Weapon Mastery, Sneak Attack, Rage, spellcasting); every subclass at 3; multiclassing splits levels and combines slots by caster level | Verb at 1, lens at 3; slots combined by a caster-level table (ability 6.5) | One-level dips into front-loaded chassis |
| **Pathfinder 2e** (checked in part) | Class feats at 1 and every even level, plus separate skill, general and ancestry tracks; dedication feats as multiclassing | A small choice every level; the dedication offer as an M4 candidate | Parallel feat tracks (choice overload) |
| **D&D 4e** | Roles × power sources; one uniform at-will / encounter / daily structure; defender marks; heals keyed to the receiver's surge | Role frames; a mark as a condition (Challenged); heal = the target's Recovery (DH 12.2) | Uniform resource shapes that erase identity |
| **13th Age** | Talents chosen at level 1; classes deliberately vary in complexity | The creation pick; a simple / medium / complex spread | — |
| **Lancer** | Frames with license ranks; role tags | Ranks as class-level scaling | — |
| **Shadow of the Demon Lord** | Novice path at 1, expert at 3, master at 7 | Big combinations at fixed beats | — |
| **Dungeon World** | Playbooks; starting moves; one advanced move per level | Short sheets, one change per level | — |
| **Gloomhaven** | Unique decks from turn 1; hand size as attrition; locked classes | Distinct from turn 1; classes as unlocks | — |
| **ICON** (unverified) | Jobs each with a full tactical kit; support never required | Every class can win fights | — |
| **Baldur's Gate 3** (checked: community sources) | Some subclasses at 1–2; one-level dips drive builds | Dip guards built in now | A portable, front-loaded chassis |
| **Darkest Dungeon** | Heroes distinct from the start; healing spread across several heroes | Healing spread thin: valued, not required | Rank locks |
| **Slay the Spire** | A starter mechanic and relic; a class-filtered card pool plus colourless cards | Class-weighted offers plus universal ones | In-fight energy income (rejected, ability 5) |
| **Monster Train** | Primary clan plus allied clan | Variety from a combination: class × subclass × species | — |
| **Into the Breach** | Every squad solves the same problem with different verbs | Identity through verbs, not stats | — |
| **XCOM 2** | Rookies promote; a two-column pick per rank; medkits for all, one class heals better | Fast binary picks; a healer valued, not required | Level-1 sameness (it works only for disposable units) |
| **Wildermyth** | Three classes, deep branching | Few classes, wide branching | Cross-cutting layers that favour one class |
| **Battle Brothers** | No classes; role from background, perks and gear | Gear already carries role, so classes must not copy gear identity | — |
| **FFT / Tactics Ogre** | Jobs with secondary skill sets | An M4 study candidate | High UI cost at launch |
| **Divinity: Original Sin 2** | Classless, with presets | Presets as recommended defaults | Roles collapsing without classes |
| **Pathfinder: WotR** | About 25 classes, archetypes, prestige classes | — | Option overload |
| **Hades** | Weapon "aspects" that change one rule | Unlockable subclasses as aspects | — |
| **Griftlands** | A gimmick resource per character | A signature from turn 1 | — |
| **DCSS** | Species plus background as a starting kit | The kit as an identity carrier | — |
| **Fire Emblem** | Promotion at mid levels; weapon access by class | The level-3 subclass beat | Hard access locks (classes own depth, not access) |

**Overall (lead's call):**

- Short-run roguelikes front-load **identity**, because a run that ends
  early must still have played the character.
- Tabletop games that front-load **power** (5E 2014, BG3) pay for it in
  dips; front-load identity, not power.
- Games whose classes share one resource structure (4e) lose identity;
  give each class its own question.
- Few classes with wide branching are enough when offers and gear are
  rich.

## 5. The client's question: identity timing

> "Should the player feel the difference between classes from the first
> level, or should we allow these differences to show and develop as the
> player puts more levels into a class?"

### 5.1 The answer

**Both, in different senses. (agreed, Q2)**

> You feel the class on turn 1, in *how you play*: the Vanguard decides
> what each hit buys, the Shade sets up one huge strike and slips away to
> set up the next, the Arcanist shapes where damage lands, the Herald
> breaks enemies before killing them. As you put levels into the class, you choose *how far* it
> leans (more uses, bigger dice, riders) and *which way* (the subclass at
> level 3 picks its second lean; level 9 makes the lean spectacular once
> per floor).

In one line: **verb at 1, lens at 3, mastery at 9.** Different in kind
at level 1 and close in power; growth in degree and in direction.

### 5.2 What the approved documents already force

All **(settled)**:

- **Floor 1 is solo.** It covers levels 1→3, the hero has no Downed
  state, and it ends in a solo boss (main 4.3; DH 4, 12.4, 21.1). Every
  class must answer floor 1 alone, as itself.
- **A level-up comes every 8–10 minutes** (main 4.1). A class that
  "grows in" by level 3 plays generic for a whole floor, and floor 1 is
  the most-replayed content in a permadeath run.
- **Every class already has a full kit at level 1:** 2 at-wills (ability
  6.1), a `rest` pool and a `camp` pool (6.3), exactly one rest defensive
  tool (DH 12.1), tier-1 spells for casters (10.1), precision parity at
  level 1 for Dexterity classes (weapons 6.4), and caster AC at level 1
  (armour 11).
- **The flat 11 HP outweighs the class tier at level 1** (DH 7.2), so a
  level-1 difference cannot come from numbers. It has to come from verbs.
- **The class is chosen at creation with no test drive** (main 4.5).

### 5.3 The three models

- **Front-loaded.** Each class unmistakable from level 1; the subclass
  moves to level 2. The strongest early feel. It needs a change to the
  level-3 subclass, doubles floor-1 balancing (eight class and subclass
  pairs live through the boss), carries the highest dip risk, and its
  middle levels feel flat.
- **Growth.** A plain but complete level 1, divergence from level 2,
  bloom at 3. The most dip-resistant, with the gentlest learning curve.
  Under the approved documents it **cannot be built purely**: the
  level-1 gates force the Dexterity class and the casters to be distinct
  anyway. Martials feel alike on floor 1, and many runs never reach the
  class's designed shape.
- **Hybrid.** "Verb at 1, lens at 3, mastery at 9." Different in kind at
  level 1, close in power; grows in degree (more uses, dice, riders) and
  in direction (the subclass picks the second lean).

### 5.4 Verdict and judges' scores

Three independent judges scored each model from 1 to 10 (higher is
better in every column). The table shows the average of the three. All
three judges chose the hybrid; the client agreed. **(agreed, Q2)**

| Model | Early feel | Growth payoff | Run-length fit | Readability | Fit with docs | Low build cost | Total |
|---|---|---|---|---|---|---|---|
| Front-loaded | 9.0 | 5.0 | 8.0 | 6.7 | 5.0 | 4.0 | 37.7 |
| Growth | 4.0 | 8.0 | 5.0 | 8.0 | 6.0 | 7.0 | 38.0 |
| **Hybrid** | 8.0 | 8.0 | 8.7 | 7.0 | 9.0 | 7.0 | **47.7** |

### 5.5 What the hybrid keeps from the other two

**(lead's call)**

- **From the front-loaded model:** "identity is front-loaded, power is
  not". Class identity is complete at level 1; power stays inside the
  parity bands. Also kept: its dip guards (7.7), and its rule that
  subclass lenses add triggers to existing buttons rather than new
  buttons (7.3).
- **From the growth model:** a preview of the class ladder at creation;
  a content check that every class's level-1 hook has a target on floor
  1 (`SeedHasTarget`); and an even level-1 sheet.

### 5.6 Guards that keep "close in power" honest

**(agreed, Q2: the four guards and the 10 points; definitions, the
tolerance and the miss rules lead's call)**

- **`Floor1ClassSpread`** (simulator gate): the solo floor-1 clear rates
  of the best and worst launch class differ by at most **10 points**
  (client number).
  - **Clear** = the hero reaches floor 2 alive.
  - **Input:** every launch class × creation pick × level-3 subclass,
    with each class's `suggestedArray`, under the reference policy (DH
    19.3) on the release seed set. A class's rate is the mean over its
    picks and subclasses; each pick and subclass is also printed.
    **(change ARC-15)** For the Arcanist a creation pick is pair × Mote
    key (12 picks), crossed with the level-3 subclass as above; starters
    are held at `pairDefaults`, every legal starter combination is
    printed as its own row (outside the mean), and the class mean is
    unweighted. Content check `StarterDefaultsHaveAnswer` and the
    starter rule (Arcanist design 7.7). **(change HR-10; told plainly,
    accepted with Herald Q1)** For the Herald a creation pick is kit ×
    level-3 subclass (4 rows); starters are held at the **no-heal
    defaults**, every legal starter combination is printed as its own
    row outside the mean, heal-card rows first, and the class mean is
    unweighted (Herald design 7.7). Lever only, if used (HR-10b, told
    plainly then): a creation pick may carry its own `suggestedArray`
    (Totem Dex +2) (Herald design 7.16).
  - **Splits:** every boss is `fearless` (enemies 14.2), so the report
    splits by what does vary: the floor-1 theme's non-fearless share of
    its roster (enemies 10.4 allows 50–100%; bands *(illustr.)* 50–74%
    and 75–100%) and whether the boss's escort can be cowed or routed.
    **(change RT-9)** The floor-1 pool defaults to 750‰ or more
    non-fearless (inside the 50–100% bands), except that a theme below
    75% non-fearless joins the floor-1 pool only with `F1EscortBreakable`
    (every escort and reserve group of its floor-1 bosses drawn from
    non-fearless types) and a passing reference-arm `Floor1ClassSpread`
    in its split (run structure design 8.2).
    **The 10-point limit applies inside every split,** not only to the
    pooled number.
  - **Per-class floor-1 death share** (report): each class's floor-1
    hero-death share with a 95% interval, read against DH 18.2's "under
    about 5%" per class.
  - **Headroom measures** (report, since clear rates sit near the top):
    HP share at the boss-room door and rounds to clear the boss, per
    class and split, so a drift shows before the clear rates move.
- **About 60%** of a class's limited-power value arrives after level 1
  (agreed target; tolerance 55–65%, which only the lead may move on report
  evidence). As a formula, per class in the offer value unit (ability
  10.2), summed over its limited abilities and their options: the value at
  level 1 is at most 400‰ of the value at level 10 *(illustr. bound)*, and
  the increments at levels 3, 5 and 9 together are at least 500‰ of
  (value at level 10 − value at level 1) *(illustr.)*.
- **`SeedHasTarget`** (content check): every class's level-1 signature has
  a target on floor 1. Each class names a `seedTarget` predicate (12.1);
  the check passes when, on every floor-1 seed of the validation corpus,
  at least N *(illustr. 2)* main-path encounters satisfy it.
  **(change SV-20; M2)** Owned here, it is a generation (`g`) check
  executed in V12g at the second floor assertion point (AP2); a miss
  acts by V12g's failure class, `stageRetry(encounters)`, then V12's
  waivable or never-waived end (seeds and validation design 5.1, 5.2,
  5.4).
- **Per-round breakdown of the solo boss** (simulator report): in each
  class's floor-1 boss fight, the share of rounds 2 and later in which
  the signature fires (3.4), by level (1–3) and subclass. Rounds after the
  escort is down (boss alone) are reported separately. A share under
  about half *(illustr.; tuning)* is too thin.
- **What a miss means:** `Floor1ClassSpread` blocks the M2 release until
  the lead applies the lever order in 15; if the levers run out, it goes
  to the client. The 60% share and the per-round breakdown are reports to
  the lead, who answers each miss with a stage-2 data change. The
  reliance reports stay reports (Q3). **(lead's call; reading
  `Floor1ClassSpread` as an M2 release gate confirmed by the client,
  2026-11-18)** **(agreed 2026-11-20, Herald Q1; change HR-38)** The
  **heal-isolation gate** (per M2 Herald subclass, "Herald with heal
  cards" − "Herald without heal cards" ≤ 10 points win rate, on the
  reference arm) also blocks the M2 release; the Herald reliance and
  desirability reports stay reports, with escalation thresholds (Herald
  design 5.4). **(change SV-23; M2)** Result classes (seeds and
  validation design 6.3): `Floor1ClassSpread` and the heal-isolation
  gate are **REL** (they block wave sign-off until the levers pass or
  the client re-rules, as above); the CL reliance reports escalate as
  **ESC**. After the M2 cap decision they bind at the shipped cap
  (reading of CMP 7.4). `Floor1ClassSpread` is read on the observed
  spread inside every split, and a marginal split fails (seeds and
  validation design 6.4).
- **(change CMP-29)** The companions reports are added (companions design
  11.4), and **`FloorTimeByPartySize`** is the first M2 gate (agreed
  2026-11-20, companions Q2; companions design 7.4). Pass or fail
  verdicts read the M2 simulator only, never a toy. **(change SV-21,
  kept)** The M1 simulator skeleton prints reports only (seeds and
  validation design 6.1).
- **(change BR-40; M2)** The boss-room reports and arms join these
  (boss rooms design 9.3, 9.4): `ClassBossContribution` (clear rate of
  the fixed arrival state per class, spread ≤ 10 points across classes,
  beside `ClassThemeSpread`; the Herald row adds fear-target
  availability on F2–5) and the boss arm per class × size × floor ×
  Rendition at both Con values. `BossDeathShare` F1 ≤ 2.5 per 100
  arrivals *(illustr.)* is a simulated gate per class at the reference
  and lowest legal Con, beside `Floor1ClassSpread` (boss rooms design
  4.9 item 8); the signature-share row (≥ 40% *(illustr.)* of boss-side
  damage in rounds 2+, "Device used once" arm) reads beside the
  per-round breakdown above (boss rooms design 4.9 item 7). The guards
  above are unchanged.

## 6. Lean, never require

### 6.1 The rule

**(agreed, Q3)** This extends the approved rule that classes "own depth,
never access" (weapons 7.3; trinkets 11 rule 5) to every utility. One
statement in five lines:

1. **How a class leans.** It may make a utility **better** (more effect
   or targets per use), **cheaper** (a bonus action instead of an
   action), **more often** (a pool where the baseline has one use),
   **more reliable** (advantage, or a threshold in place of a roll) or
   **wider** (range or reach).
2. **How far.** About **twice** the baseline's rate or cost
   *(illustr.)*. Never free, and never unlimited for a hard effect.
   Always inside the cap the approved documents set for that utility. A
   lean that needs a cap raised is a change request, not a class feature.
3. **What it may never do.** Give exclusive access, or rewrite a shared
   formula. The shared formulas are: worst-member Stealth, potion values,
   the healing guarantee, Rally once per ally per fight, one reaction per
   round, one camp spend per turn, and speed. The full list of forbidden
   grants is in 6.3.
4. **A route without the class.** Every leaned utility keeps a route any
   party can use: a universal action, a skill reachable through offers, a
   weapon verb, an item or a companion. No dungeon content needs a class
   to be passed. Content checks `LeanWithoutBaseline` and
   `ClassGatedContent` (12.2).
5. **Reliance report.** For every utility a launch class or subclass
   leans on, the simulator compares win rates with and without that lean
   (6.6). Target: at most **10 points**, the client's healer number
   reused. **These are reports, not gates (agreed, Q3).** Healing keeps
   its approved gate (DH 18.1, agreed) and gains the heal-isolation gate
   (Herald design 5.4). **(agreed 2026-11-20, Herald Q1; change HR-38)**

### 6.2 The spread rule

**(lead's call)**

- **A lean** is a record {`utilityId`, `weight`: primary, secondary or
  ribbon, `source`: class or subclass id} (12.1). A tool that a class
  merely has, such as a pool-backed reaction ward, is not a lean unless it
  is recorded as one; 6.4 shows such tools as "tool", not as leans. The
  Shade's Duck, Tainted Blade's Poisoned, Make an Example's Frightened,
  Tripline's Prone and Drag Line's movement are recorded as tools
  (change SH-27; Shade design 7.1).
- **For the spread rule, a class and its subclasses count as one class.**
- No utility is the *primary* lean of more than one launch class.
- No utility is leaned on by more than two classes (each with its
  subclasses).
- **Scope:** the rule binds the classes unlocked by default (the launch
  four). It is re-run for each unlock tier against every class available
  in that tier; a row past two there is flagged to the lead, who either
  lowers the unlock's weight on that row or keeps it with a reliance
  report on that utility.
- Two utilities are required somewhere: protector tools (on a class or
  subclass and on several companion kits; DH 27) and Arcana on a
  non-caster (DEP-1, consumables 13.8).
- A utility marked "ribbon" in 6.4 may never be a primary lean.

### 6.3 Forbidden grants

**(agreed, Q3)** No class, subclass, companion kit or species trait may
grant any of the following:

- a second reaction per round;
- teleport; and, for a class, subclass or companion kit, +speed or a
  change to speed (stat model 6; a reaction that sets a mover's remaining
  movement to 0 is allowed: CL-6). Species traits stay one of stat model
  6's approved speed sources and are bound by that row as written.
  **Reading (change VG-22; ruling pending):** an ally swap is 1 square
  of forced movement on the ally, not a teleport or a speed change
  (Vanguard design 8.2, Step In);
- free or guaranteed-safe rests, supply generation, or `rest` refills
  outside rests;
- more loot or gold;
  **Reading (change XI-14; M2):** an information source (a treasure
  pointer such as the Lodestring or F-TREASURE) never changes loot value,
  n, identities or placement (IP10), so it is not "more loot"
  (exploration and information design 4.5.1, 4.6.5);
- changed potion or tonic values;
- party passive stealth above the worst member's;
- AI targeting overrides other than conditions (Challenged).
  **Reading (change SH-20):** writing `lastKnown` into a group's shared
  awareness record (False Trail, only when you are Hidden from every
  watching member) is information, not an AI targeting override; it is
  an enumerated `ForbiddenClassGrant` exemption (Shade design 7.11, 9.1);
- extra units or companions;
- a freshly created action for an ally;
- `liftsDowned` (client approval required, DH 11.5);
- Rally more than once per ally per fight;
- a condition outside the 32 M1/M2 conditions (a Later condition needs a
  budget ruling);
- a second camp spend in a turn.

Content validation `ForbiddenClassGrant` (12.2) fails the build on any
record that grants one of these, through the enumerated checks in 12.2.
**(lead's call)**

### 6.4 The utility map

Launch lean column: **V** Vanguard, **S** Shade, **A** Arcanist, **H**
Herald. **P** primary lean, **s** secondary lean, **sub** a subclass
lens, **C** companion kits. Caps are **(settled)** unless marked.

| Utility | Universal baseline (every party) | How a class leans | The cap (no class goes past it) | Launch lean |
|---|---|---|---|---|
| Single-target damage | 2 at-wills, one damaging (ability 6.1); weapon verbs; crits; Exploit on Dexterity-rolled hits with advantage or Aim; Extra Attack at 5 for martials | Precision dice; Techniques +1d6; Surge; ignore-resistance offers | Every build ≤ 110% of the greatsword reference (as measured by weapons 6.4, VG-12: rest-pool damage floor-averaged at the worst legal build; camp-pool damage bounded by the nova bound and `CampOptionBound` (formerly `SurgeOptionBound`, renamed and generalised by SH-5, Vanguard design 10.1, Shade design 7.5), or for slot pools `SpellTierBound` and for slotless casts `SlotlessCastBound` (change ARC-2)); Dexterity-main melee ≥ 90%; launchers reported against 75%; all with empty trinket slots; every launcher family ≥ 60% (M2 parity set); trinkets +10% at most (weapons 6.4, trinkets 11 rule 6); one camp spend per turn; capstone turn damage ≤ 2× the 95th-percentile non-capstone turn of the same unit (12.3) *(illustr.; lead's call)*. **(change ARC-2)** `SpellTierBound` holds slot pools to ≤ 2000‰ single target, `SlotlessCastBound` holds Overchannel, `PayoffBound` allows one setup step per target, and `CampOptionBound` is scoped to non-slot, non-slotless camp pools (12.2; Arcanist design 11.2). **(change ARC-3)** Slot Recovery counts as rest power here and in the camp share alike (Arcanist design 7.4) | S s, V s |
| Area damage | Flasks (one offensive per turn; none on floor 1), scrolls, sweeping verbs | Slot areas; shape (+1 radius, `selective`) | ≤ 25 squares at levels 1–6; allies hit unless `selective`; at-wills never hit allies; no ground zones (ability 8); slot areas ≤ 1250‰ per target under `SpellTierBound` (change ARC-2; Arcanist design 11.2). **(change HR-13)** `PresenceAreaSide`: a presence area with no damage part affects one side by data (fear and push areas enemies only; call and cure areas allies only), so no Herald area needs friendly-fire shaping (Herald design 8.1) | A P |
| Damage types and riders | Weapon types; weak targets take riders; crits attempt riders; terrain combos | Riders on demand (Techniques); an element pair; ignore-resistance offers | One rider per target per use; at-will riders rare and once per turn (ability 9.3–9.4); **no single-type kit, no switching types per enemy** (lead's call; stat model 2). **Reading (change HR-3; told plainly):** the Herald's Plain Speech (Harrowing Word deals bludgeoning against a psychic-immune target) is an automatic fallback on immunity, never a player switch (Herald design 7.2) | A s, V s |
| Conditions and control | Shove, grapple, Frighten, Topple and Push verbs, scrolls, wands | Limited major or hard control; repeat-save pressure; priming | Hard control only from limited abilities; agency conditions enemy-only (cond 7.1, 7.3); Steeled, Unyielding, boss pips and the shed action; the 32-condition budget full (cond 16); a controller ranks up through duration and reliability, never chained lockouts (lead's call) | A P, H s |
| Forced movement and terrain | Shove, Push, Hook, Topple, shield bash, fixed hazards | Technique pushes (not verb-sourced, so collisions apply); hazard edges | ≤ 3 squares per hit; a verb moves a target once per round; fixed terrain only; M1 pushes stop before hazards (weapons 7.2) | V sub (Wrecker) |
| Reach, zones, opportunity attacks | Everyone threatens; Set; reach weapons; Disengage | A movement-ending opportunity attack; Challenged | One reaction per round; Disengage stops both triggers | V s |
| Protection (guard, intercept, body-block) | Positioning; Dodge; Guarding shields; Parry and Set | A pool-free class Interpose; Square Up (Vanguard design 8.2); heavy armour and shield | One reaction AC bonus per attack (armour 10.4); Pry; Staggered; Challenged's own end rules; never "untargetable"; the AI never reads class (enemies 11.3) | V P, C (required, DH 27) |
| Reactions | Opportunity attack, Parry, Set | Pool-backed reaction wards or strikes | No second reaction; no nesting; a policy on every reaction (ability 7.3) | V s; tools, not leans: S (Duck), A (Flare) |
| Own tempo | Move, action, bonus action; the cost offer | Bonus-action Hide, Disengage, Dash (pooled); Surge | One camp spend per turn; Runner's Boots pre-cleared; bonus-action Dodge held (trinkets 11 rule 3) | S P, V (Surge) |
| Ally tempo | Help; Rally (Rallied or Spurred, never both) | Spurred grants; Helped at range only if the registry clears it | Ally effects transfer an action or spend the ally's own reaction; never create an action; never fire the ally's limited ability (lead's call) | H s |
| Healing and Recovery | The class-blind guarantee: 2 kit potions, the floor potion, camp top-up, rests, the Medicine bonus, every class's rest tool (DH 12) | Recovery-sized heals with better tempo: bonus action, range, more targets per tier | Exactly the target's Recovery; `rest` or `camp` pools only; never at will or on wands; one class at most; healer gap ≤ 10 points (DH 12.2, 18.1, agreed Q10); no-healer arm at least a quarter of runs (DH 18.2, agreed Q11); no class changes potion values | H (spell cards; Hearth sub) |
| Medicine, Treat, getting up | Anyone trained; once per companion per fight; the cost offer can make Treat a bonus action | Treat at range 2; an extra cure | Once per companion per fight stays; no free rests; `liftsDowned` reserved | H sub (Hearth) |
| Drop prevention | Reprieve Phial | None at launch (the Death-Ward-shaped spell is Later) | One recharging prevention per unit per fight (DH 15.4) | — |
| Wards and Helped (tokens) | Help, Vex, trinket wards | Grant tokens; build on token outputs | Tokens are charges; one trinket Warded per fight; classes build on outputs, never on trinket ids (trinkets 11 rule 8) | — at launch (Votary unlock). **(change HR-33)** Any Herald Helped would sit under ally tempo; none ships at M2 (CO-B3 stands; Herald design 8.3) |
| Stealth, surprise, Steal | Worst-member passive stealth; Hide (action); silent kills; a second route per floor | A pooled bonus-action Hide (the class hook of ability 7.2; Shadowed armour gives it once per fight, armour 14.2); opener damage | Worst member stays; Clatter stays; no automatic surprise of bosses or elites (lead's call) | S P |
| Morale | The hero's Intimidation; Frighten; Demand Surrender (Demoralised or Routed only); cowing; Rally | Cheaper, ranged or two-target Frighten; routs through T5 and T6 (limited fear effects) | No meter; Demoralised only from T1–T4 (enemies 14.3); `fearless` units exempt; never bosses; elites only when Routed; one cow attempt per group; the 75% / 100% cow cap; equal experience (enemies 14) | H P |
| Information | Best passive Perception against guile; Study (Lore); Arcana recognises spells | A free Study rider; revealing resistances. **Pending requests (change XI-27; M3; owners' rulings pending):** the Arcanist ritual Walls Have Ears (a batched Listen at the current space's closed doors) and the Shade rider Cased (watching a known unaware group earns a one-lap patrol forecast), conditional on the Trap Eye re-scope ruling (TL-6), its odds band also on EP-41 and the ENM 13 flag; both stay inside IP1–IP10 (exploration and information design 4.5.1, 4.5.2) | Tactician stays the Perception mastery offer and is never copied (stat model 7.2, enemies 25); Lore never hides basics | A sub (Loremaster, unlock) |
| Initiative | d20 + Intelligence | A bonus | Surprise rules; floor-1 fairness | ribbon (A via Int) |
| Skills | 2–3 class skills; offers; best-member checks; trained-only uses | The skill on the list plus a class rider on its use | No free retries; mastery only through offers; no class-exclusive skill use | S s |
| Items | Belt 2 (max 3), Administer, throw, one offensive flask per turn, a scroll read as a camp spend | Belt +1; flask riders; in-class scroll lists | Belt hard maximum 3; potion values fixed; the registry covers item-tempo trinkets | S sub (Trapwright) |
| Loot and identification | Passive Arcana threshold; camp identifies everything | Earlier reveal | Fixed loot budget; never more loot | ribbon |
| Exploration | Best member; Search; forcing doors; Acrobatics routes | Softer failure costs; more secrets | No per-tick benefit (exploration has no clock); no free retries | S s |
| Rests and supply | About 2 rests per floor; supply cap 3; Slot Recovery | Rest-quality ribbons | No free or safe rests; the timescale offer at most once per run | ribbon |
| Mobility | Speed 6, Dash, Disengage, slip past | Spurred on self or allies; cheaper Disengage | No +speed; no teleport (stat model 6; ability 8) | S s |
| Concentration | Casters; the Con check; staff Steady Casting | Concentration training (feature or offer) | One at a time; ally buffs over 10 turns need it (ability 9.6) | structural (A, H) |
| Companions and leadership | Recruits at camp; Presence and Leadership are the hero's | Better recruits and prices (a Herald hero) | No extra units; party weight prices companion power (enemies 10.1) | H s (hero only) |
| Summons | None (consumables 16) | — | Needs a new system: Later | — |

### 6.5 Fallback damage types

**(lead's call)** Every class names its "slowed, never locked out"
fallback from the ten damage types of stat model 8.1, in the field
`fallbackDamageTypes[]` (12.1). **Force is not a
damage type;** no class ability deals it. The Arcanist's fallback is
physical, through its simple weapon or staff. A psychic fallback is a
control rider (Unfocused), not a neutral one, so it never serves as a
class's only fallback. **Reading (change HR-3; told plainly):** the
Herald's fallback is its weapon's physical type; its Plain Speech makes
Harrowing Word bludgeoning against a psychic-immune target, an
automatic fallback on immunity, not a switch (Herald design 7.1, 7.2).

### 6.6 Reliance reports

**(agreed, Q3; definition lead's call)**

- **Utilities reported:** protection, control, stealth, morale and own
  and ally tempo; plus any utility a stage-2 class or subclass marks as a
  lean.
- **The "without" arm** means **no unit of that class or subclass, hero
  or recruit**, which matches how the healer gap already counts companion
  heals (DH 18.1). Its recruit pools are drawn with that class's kits
  removed. The recruit-pool rules (7.8) are written so that other classes'
  kits still meet them; a rule that only the removed class could meet is
  waived in that arm alone, and the waiver is printed with the report.
- **Composition sampler:** party arms use the scripted recruit policy
  (ability 14.6) and the reference party policy (DH 19.3) over the
  release seed set. On solo floor 1, "without" a class means the mean of
  the other three launch classes. **(change CMP-18)** The reading pins to
  the reference slot schedule and `MusterBestFit` (companions design
  11.3).
- **Target 10 points,** as a **report**. A miss goes to the lead, whose
  lever order is: the class's lean size (pool, range, targets), then the
  universal baseline route's content density, never a forbidden grant.
  A report becomes a gate only if the client asks.
- **Healing** keeps its approved gate exactly as DH 18.1 defines it
  (parties with no member holding an ally `healRecovery` ability against
  parties with at least one, recruit heal kits included). The Herald arms
  and the healer count of CL-9 are reports beside it, broken down by heal
  source (Herald hero card, Herald recruit, heal-kit recruit).
  **(change HR-26; told plainly, accepted with Herald Q1)** The reports
  beside it gain the healer classification columns (report only), the
  policy-forced Herald arms per subclass (Herald design 5.4's
  construction), the "Hearth, no card" row, the 2+ row with its
  escalation, source rows (Hearth, scroll, get-ups by route),
  `MoraleValueReport`, `AllyTempoValueReport`, heal telemetry and the
  trap-room report. **(agreed 2026-11-20, Herald Q1; change HR-38)**
  Healing also gains the **heal-isolation gate** (per M2 Herald
  subclass, with heal cards − without ≤ 10 points; an M2 release gate,
  5.6); the Herald reliance and desirability reports are required
  reports with escalation thresholds. The DH 18.1 gate itself is
  unchanged (Herald design 5.4). **(change CMP-23; agreed 2026-11-20,
  companions Q3)** While no heal kit ships, the "heal-kit recruit" row
  prints 0 and the "healer count 2+" row prints N/A; the gate text is
  unchanged, and at M2 its healer arm is exactly a Herald hero holding a
  heal card (companions design 5.4).
- **(change CMP-29)** The companions reports join these (companions
  design 11.4), with `FloorTimeByPartySize` as the first M2 gate (agreed
  2026-11-20, companions Q2). The DH 18.1 and 18.2 pass or fail verdicts
  read the M2 simulator only, never a toy; DH 18.2's "checked against
  playtests" stands as a check beside the verdict, and a disagreement
  goes to the client.

## 7. Structure

### 7.1 Choosing the launch four

**(agreed, Q1; method lead's call)** Every candidate slate was scored
against the binding checklist (section 11). The survivors were ranked on
a distinct level-1 play pattern, coverage of the utility map with no
repeated primary lean, and fit with the M1 slice.

| Check | **A: Vanguard, Shade, Arcanist, Herald** (chosen) | B: Herald → Votary | C: Herald without heal cards | D: Shade → Sapper |
|---|---|---|---|---|
| Main abilities | Str, Dex, Int, **Pre** | Str, Dex, Int, Wis | Str, Dex, Int, Pre | Str, Int, Int, Pre |
| Dexterity-main class with precision (weapons 6.4) | Shade | Shade | Shade | **none: fails** |
| DEP-1, a non-caster with Arcana (consumables 13.8) | Shade | Shade | Shade | Sapper |
| Healing lean (at most one) | Herald, light, cards only | Votary | none (gap passes untested) | Herald |
| Two concentrators (Stillmind, Later) | A, H | A, Votary | A, H | A, H |
| Caster traditions | arcane + presence | arcane + divine | arcane + presence | arcane + presence |
| Distinct primary leans | protection / stealth / area and control / morale | protection / stealth / area / prevention | as A | items overlap the Shade's role |
| Presence target (stat 5.6) | met | secondary only | met | met |

Why A:

- It gives Presence, the ability most at risk, a main-ability home.
- It shows the morale system on the hero, which is where Presence pays
  (stat model 5.4).
- It answers the client's healer picture literally: healing is spread
  thin across potions, every class's rest tool, and one support class
  whose tempo heal competes with its fear spells for slots. M2 can then
  *test* the 10-point gap instead of passing it trivially.
- Wisdom and Constitution have no main ability at launch. Stat 5.6 is
  agreed **as a target**, so this is allowed; the first unlocks fill both
  (9.4).

### 7.2 The M1 slice class

**The Vanguard. (lead's call)**

- It has no slots and no concentration, so the staff (weapons 26) and
  Concentrating (conditions 18.1) both stay out of M1.
- It uses M1 conditions only.
- It follows the ability 6.6 template exactly.
- Its M1 subclass is the Weaponmaster.

### 7.3 Subclasses

All **(lead's call)** except the level, which is **(settled, ability
10.1)**.

- **At level 3,** as approved. The subclass lands at the floor-1
  antechamber, before the boss **(change RT-8; Antechamber Catch-up, run
  structure design 9.4)**: the player has played the verb alone and now
  picks the lens for the party game.
- **Two per class at M2.** The Vanguard has a third, because the
  Weaponmaster ships in M1. Further subclasses come through unlocks (main
  4.7), as "aspects" that change one rule. **(change AS-1)** An aspect
  hangs its features off one rule but keeps the full ladder shape (for
  the Arcanist: a level-3 tier-2 spell and the rule, a signature Weft
  option at 3 / 5 / 7, a level-6 choice of 2, a level-9 Overchannel
  rider; Arcanist design 9.3). **(change SH-30)** A launch
  class's stage-1-named third subclass (the Shade's Trapwright or
  Swordsworn, whichever does not ship in M2) ships as a full subclass, as
  the Vanguard's three do, not as an aspect (Shade design 14). With the
  DEP-S1 fallback applied, that is the Trapwright, in M3 (change TL-37;
  traps and locks design 6.16).
- **A subclass re-points the lean.** It either deepens the primary lean
  through a mechanic the class does not have at level 1, or picks the
  class's second utility; it never adds a role. It adds **triggers and
  options to existing buttons, never new buttons.** It never adds a
  fourth named martial pool (ability 6.6) and never pushes past the cap
  of eight.
- **The level-6 subclass feature changes a verb,** not a number. This is
  the fix for a plateau at levels 6–8.
- Subclasses key off weapon verb ids or group tags (weapons 7.3) and off
  trinket *outputs* (Warded, Helped, element riders), never trinket ids
  (trinkets 11 rule 8).
- **No subclass copies its own base class's level-1 mechanic.** Each
  class lists `level1MechanicIds` (for example `techniques`,
  `holdTheLine`, `opening`, `elementPair`, `cadenceFrighten`) and each
  subclass lists the `mechanicIds` it introduces; `SubclassCopiesBaseLean`
  (12.2) compares the two lists, not utility rows. Intended results:
  Bastion introduces `squareUp` (passes, deepening protection;
  cross-document comparison 2026-11-23, CC-61); Binder
  introduces `controlDuration` (passes); Dirge introduces
  `twoTargetFrighten` (passes, deepening morale); Cutthroat introduces
  `silentKillBroken` (passes; its Surprised clause is a trigger on
  Opening, allowed). "Elementalist" (ignore resistance in one family)
  stays an unlock aspect, not an M2 subclass: it narrows the pair toward a
  single-type kit, against the 6.4 cap, and needs its own evidence.
- **Spread result at launch:** counted per 6.2, no utility row in 6.4 has
  more than two classes, and no primary lean repeats.

### 7.4 The published ladder

**(lead's call)** One ladder for every class. Automatic gains are listed
above the level's single choice (ability 10.3).

| Lvl | Floor | Automatic beat | The choice: martial | The choice: caster |
|---|---|---|---|---|
| 1 | 1 | Full class kit (8) | Creation pick, 1 of 2–3 from M2 (stance, kit style); at M1 the Vanguard's stance pick has one option | Creation picks: Arcanist, the element pair and its Mote key (one choice, pair × key; ARC-15, Arcanist design 7.3); Herald, the Totem-or-shield kit (no tradition lean); each plus 2 starter spells |
| 2 | 1 | Pool steps per table | **Class talent**, 1 of 3, reshaping the signature verb | **Spell card**, 1 of 3, from the class list |
| 3 | 1→2 | Tier 2 (casters) | **Subclass** (1 of 2) | **Subclass** (1 of 2); it grants its own tier-2 spell, which is the level-3 known spell |
| 4 | 2 | — | **+1 ability**, the only choice **(settled, stat 5.1; lead's call that it stands alone)** | same |
| 5 | 2→3 | Extra Attack (martial); tier 3 and stronger cantrips (caster); signature scaling step | **Offer**, 1 of 3, class-weighted | **Spell card** |
| 6 | 3 | Second Wind 2 (Vanguard, ability 6.6, lead's call); the other rest tools' per-rest sub-limit 2 (8.2); pool steps | **Subclass feature** (1 of 2), changing a verb | same |
| 7 | 3→4 | Tier 4 (casters); signature step | **Class talent** | **Spell card** |
| 8 | 4 | — | **+1 ability**, the only choice | same |
| 9 | 4→5 | **Capstone** (a `camp` pool of 1, ability 6.7) | **Offer**, 1 of 3 **(agreed, Q4; CL-4)** | same |
| 10 | 5 | — | Class talent **or** offer: the player picks the source (6.7) | Spell card or offer |

- **Spells known:** 2 at level 1, then +1 at levels 2, 3, 5, 7 and, if
  the player takes the spell card there, 10: 7 by level 10, or 6 if the
  level-10 pick is an offer. Both match "about seven" (ability 6.5). One
  swap at camp stays **(settled)**. The level-3 spell is the subclass's
  own: always known, never swapped **(lead's call)**. **(change ARC-21)**
  The Arcanist's spells known by level are 2 / 3 / 4 / 4 / 5 / 5 / 6 /
  6 / 6 / 7, one less from level 7 if the level-7 trigger option is
  taken, and one less at level 10 if the offer is taken (Arcanist design
  7.7).
- **The spell-card draw (lead's call):** 3 distinct cards from the class
  list, a weighted sample without replacement by each spell's
  `drawWeight`, excluding known spells and spells whose minimum tier is
  above the caster's highest slot tier. **(change ARC-21)** For the
  Arcanist the draw deals variants (template × family, the family fixed
  on the card), at most one variant per template (SP-4); soft
  constraints `drawRequires: offFamily`, a Recommended card, no Expert
  card in the level-2 draw and a tier-4 signature at level 10, relaxed
  in a fixed order and checked by `DrawSatisfiable`; every tier-1 card in
  the level-2 draw plays alone (`L2DrawPlaysAlone`) (Arcanist design
  7.7).
- **Trigger options at level 7 (lead's call):** every level-7 talent menu
  and every level-7 spell-card draw includes at least one trigger option
  (7.5) from the class's `triggerMenu`, in place of one of the three.
- **A preview of the ladder** is shown at creation for every class, with
  recommended defaults for every creation choice. **(lead's call)**
  **(change ARC-13)** The Arcanist's defaults are data: `castDefaults`
  (visible pre-ticks), `defaultRotation`, `pairDefaults` and
  `recommendedPickOrder`, with Recommended and Expert card badges,
  creation warnings that never block, the starter rule, the last-slot
  confirmation, the pip glint and, if its lever 1 is used, a default
  target filter (Arcanist design 5.2, 7.7, 7.14, 11.8).

### 7.5 The cap of eight and offers

**(lead's call)**

- **Counting (CL-2; 3.4):** a limited ability is **one named pool**.
  Every ability record drawing on that pool is an option of it, and a
  spell tier's slot pool counts as one, with its known spells as its
  options; an upcast is an option of the higher pool. An offer or talent
  that adds an ability to an existing pool adds an option, not a limited
  ability. A `restDefensiveTool`'s per-rest sub-limit (8.2) is not
  counted. **(change VG-10)** Nor are a per-rest sub-limit on one option
  of a named pool (the Hew-use cap), turn allowances or per-target round
  counts: they are frequency limits, outside the cap and the
  three-named-pool limit, count 0, stay outside the camp-share ratio and
  sit inside the 60% formula (3.4; Vanguard design 7.9). **(change
  SH-4)** Nor is a per-rest counter on one named pool's record, spent by
  records tagged with its one declared tag, whether or not they spend
  from the pool: a frequency limit that counts 0, not a named pool (the
  Shade's strike limit; 3.4; Shade design 7.9).

  | Class | Limited abilities by level 10 | Cap room | Reading room (7.6) |
  |---|---|---|---|
  | Arcanist | 4 tiers + Weft + capstone = 6 | 2 | 1 new row |
  | Herald | 4 tiers + Cadence + capstone = 6 | 2 | 1 new row |
  | Vanguard | Second Wind + Techniques + Surge + capstone = 4 | 4 | none: at 4 from level 1 |
  | Shade | Tricks + Vanish + capstone = 3 | 5 | 1 new row |

  So an offer that creates a new pool fits the cap for every class, but
  the reading limit is the real bound: the Vanguard takes only options,
  upgrades and triggers, and the others take at most one new pool
  **(lead's call)**. **(change ARC-21)** If the Arcanist's Knit lever
  (8.5) is used, its `offerExcludes` gains every offer that adds a row,
  since the lever spends the spare row (Arcanist design 7.5). The validator (ability 14.5) counts the distinct pool
  ids reachable from the class table plus the offers that create pools.
- **Offers are class-weighted.** Each class names 2–3 **offer lanes**,
  `offerLanes: [{tag, weightPermille}]` (12.1): a weight multiplier on
  offers carrying that tag, applied after the `requires` filter (which
  stays a hard eligibility filter). `offerExcludes: [offerId or tag]`
  removes offers outright. **The draw:** 3 distinct offers, a weighted
  sample without replacement over the eligible offers, lane weights
  applied; universal offers stay in the draw at weight 1000‰. The draw
  also drops any offer that would give the unit a fifth thing to read
  (7.6), so an upgrade, option or trigger offer takes its place. Each
  class needs at least 2 viable lanes; variety is measured in 12.3.
- **A new offer kind, "trigger" (CL-3):** an ability gains one named extra
  trigger from its class's `triggerMenu` (12.1), for example "Opening also
  counts Restrained targets". A trigger offer picks one trigger the unit
  does not own and never repeats it. It is legal at any level, below the
  cap as well as past it, so offers reshape verbs rather than only raising
  numbers. **(change SH-23)** `TriggerFromFive` scopes this for
  Opening-trigger records only: an added Opening trigger has
  `minClassLevel` ≥ 5 (Caught the one listed exception), its tier is
  fixed by `TriggerTierFixed` with its allow-list, and
  `OpeningTriggersAdded` ≤ 3; "legal at any level" stays true for every
  other trigger offer (Shade design 6.6).
- **The Herald is excluded from the `cost` offer on Frighten**
  (`offerExcludes`), because Cadence already makes it a bonus action and
  the two would double-dip. **(change HR-11)** The Dirge's Refrain
  (`onEvent`, sharing the `frighten` allowance) and Parley (Demand
  Surrender as a bonus action) are subclass verb changes, not cost
  offers; the exclusion stands (Herald design 9.1).

### 7.6 What counts as a "thing to read"

**(lead's call; answers ability model 18, "what counts as one thing to
read")** The target is at most four per unit (ability 11.4, settled).

- A row of at-wills counts as 1.
- Each pool row, with every ability that draws on it, counts as 1.
- All the slot tiers together count as 1 (a "spellbook" row).
- A toggle counts as 1 only if it is meant to switch more than once per
  fight.
- A passive counts as 0 if it marks the battlefield (a glint, a ring);
  otherwise 1. Opening and Exploit mark every qualifying target with a
  glint (shown on the target and in the attack preview), so both count 0.
  **(change VG-11)** A row-modifier passive (one that only changes how an
  existing row's options resolve, such as Momentum, Practised Hand or
  Twin Verb) or a glint counts 0 (Vanguard design 10.5). **(change
  ARC-18)** Interplay (3.4) marks its target with a glint and so counts
  0; Fused sits on the spellbook row with Overchannel (Arcanist design
  7.1).
- A pool-free reaction with an automatic policy (Hold the Line,
  Shieldwall) counts 0.
- A capstone is shown on the row of the pool it extends, and adds no row:
  Stand Fast on the Surge row, Overchannel and Scatter on the spellbook
  row, From Nowhere on the Vanish row.

At level 1: Vanguard 4, Shade 3, Arcanist 3, Herald 3; the same at level
10 before offers. Party-level readability is an M2 playtest gate: median companion-turn time under 10
seconds (settled target).

### 7.7 Multiclassing later (M4)

Multiclassing is an M4 item (main 9), "only once single-class paths are
fun" (main 4.5). The client's phrase "as the player puts more levels into
a class" fits both candidate models, so the single-class tables are
built now to keep either one safe.

**Dip guards, recorded as constraints on the class tables (lead's
call):**

1. **Chassis from the first class only:** save (stat model 5.5, lead's
   call); armour ceiling and shield training; HP tier at level 1; skill
   count; starting kit and potions; and **the `restDefensiveTool`**, so a
   character never holds two and a Vanguard dip never gives a second
   full-Recovery Second Wind. Each of these carries `chassisOnly: true`
   (12.1). A second class whose rest pool also holds its rest tool
   (Tricks, Weft, Cadence) grants the pool and its other options, never
   the tool.
2. A second class grants its **signature verb at class-level-1
   strength**, plus its weapon training. Never heavy armour, never a
   second save. The verb is named by `signatureVerb: [abilityId or
   poolId]` (12.1): Vanguard `techniques`; Shade `opening`; Arcanist the
   slot pools and `elementPair`; Herald `cadence` (without its rest tool).
3. **Signature verbs, pool sizes and precision dice scale by levels in
   that class** (`scalesBy: classLevel`, 12.1).
4. **A heal spell card from a class other than the first class needs class
   level 3 in that class** (`minClassLevel`, 12.1). A single-class Herald
   may take Mending Word from level 1. **(change HR-34)** Data now: every
   heal card carries `upcastCap: classSlotTable` (`HealUpcastCapDeclared`);
   runtime at M4: `HealUpcastByClassSlots` caps a heal card's effective
   tier at the Herald's own class-level slot table (Herald design 5.3,
   7.1).
5. **The capstone needs class level 9** (`minClassLevel`). In a 10-level
   run a multiclass character gives it up; that is the intended, visible
   cost.
6. **Shared limits span the character:** the cap of eight and one camp
   spend per turn count across both classes; the limit of 3 named martial
   pools applies **per class** (so up to 6 per character), with the
   reading limit (7.6) and the cap of eight as the character-wide bounds.
7. **Precision parity.** Weapons 6.4's evidence covers single-class
   builds. Multiclass Dexterity builds get a separate M4 report against
   the ≤ 110% cap only (CL-8). Precision dice scaled by character level
   are therefore not needed and are rejected.
8. Slots combine through the caster-level table (settled, ability 6.5).

**The model itself is decided at M4:** (a) a 5E-style level split, which
fits the main design's wording and the slot table; or (b) a
dedication-style "cross-training" offer card that grants another class's
signature verb. Guards 1–5 make either one safe. The milestone "win as a
multiclass character" (main 4.7) works with both.

### 7.8 Companions and hero classes

- **Approved shape (settled, ability 11.1–11.2):** a companion has a
  class for HP, save, skills, main ability and in-class scrolls. Its kit
  is its own data: 2 at-wills, up to 3 limited abilities, 1 reaction, one
  signature `camp` ability, choices at levels 3, 6 and 9. **Companion
  class ids are the launch class ids,** so `classTags` work.
- **Authoring guidance (lead's call):**
  - A kit carries a **simplified slice of its class's signature verb.**
    There is no subclass field.
  - The player learns four class shapes once, and a recruit reads in
    under 10 seconds.
  - Caster companions have a **single combined `camp` slot pool**, which
    counts as one of the "up to three limited abilities".
- **Keeping the hero distinct (lead's call):** a recruit of the hero's own
  class carries a *different* slice from the hero's core verb, and
  same-class recruits are weighted down in the recruit draw.
- **Herald recruits** cannot use Presence (always the hero's, stat 5.4)
  and never have a heal as their signature. **(lead's call)** The Herald
  class record carries `companionMainAbility: Wisdom` (its suggested
  second); a Herald recruit's main ability, difficulty (stat model 6) and
  kit attacks key off it. Herald companions have no presence-tradition
  spells at M2 and no Frighten feature (no Cadence): the morale verbs that
  read Presence (Intimidation, Leadership, Rally, cowing) stay the hero's
  (stat 5.4), and the universal Frighten action is unchanged. Their signature is tempo: a "Drummer"
  kit that grants Spurred (CL-5 names companion kits of the Herald class
  as a source). **(change HR-12)** Herald recruits are non-casters at M2;
  no Herald recruit kit heals (`NoHealOnHeraldKit`), so CL-9's "Herald
  recruit" heal-source row is a permanent 0; their "different slice" is
  no Frighten or Rekindle, with Spurred shared (CL-5) (Herald design
  7.17).
- **Recruit-pool rules per floor** (requirements on the companions
  design, lead's call):
  - at least one kit with protector tools (DH 27), and protector kits
    exist on **at least two classes** (the Vanguard and a Herald shield
    kit that Interposes), so the protection reliance report's "without"
    arm (6.6) still meets the rule;
  - an Arcana-trained non-caster (DEP-1). At launch only the Shade's list
    holds Arcana among non-casters, so the stealth report's "without" arm
    waives this rule (6.6);
  - at least one Medicine-trained recruit and one Mechanics-trained
    recruit;
  - **at most one kit with an ally heal** per board (zero at M2), and
    that heal is never the kit's signature.
  - **(change CMP-11)** "Each floor's pool" is read as each Muster board
    together with the current party: a gap card fills the first unmet
    need; needs already met are waived and printed; the Arcana need's
    gap card is a non-caster (companions design 7.3).
- **Optional (lead's call):** recruits of classes not yet unlocked for
  the hero, as previews; only non-casters until their spell lists exist,
  because of `classTags`.

### 7.9 Species

- Species give traits only, never ability points, and every pairing is
  playable. **(agreed, stat 5.1)** Species ship in M4 unless trivial in
  M1. **(settled, main 9)**
- **(lead's call)** Species traits lean on *baseline* axes only: senses,
  hazard crossing, a resistance, one skill. They never act on class pools
  and never grant another class's signature verb. They obey the forbidden
  grants (6.3); speed stays governed by stat model 6, where species traits
  are an approved source.
- **The copy registry extends to species traits (CL-11).** Species,
  classes, trinkets and consumables are checked against each other in
  every direction.

## 8. The launch four

### 8.1 How the rosters scored

Three rosters were drafted: classic archetypes adapted, mechanic-first
concepts, and party-role / utility-first concepts. Three judges scored
them (1–10, higher is better); the averages:

| Roster | Distinctness | Utility coverage | Lean, not require | Fantasy appeal | Fit with docs | Low build cost | Total |
|---|---|---|---|---|---|---|---|
| Classic | 7.0 | 7.0 | 7.3 | 8.7 | 8.0 | 7.0 | 45.0 |
| Mechanic | 9.0 | 7.7 | 7.3 | 7.0 | 7.3 | 6.7 | 45.0 |
| **Role** | 8.0 | 9.0 | 9.0 | 8.0 | 9.0 | 7.0 | **50.0** |

All three judges chose the role roster and the same launch four. The
cards below merge the best concepts from all three rosters and apply
every critique finding (appendix A).

### 8.2 Shared chassis rules

Every class meets the fixed chassis rules **(settled)**:

- HP tier 6 / 7 / 8; one trained save; 2–3 skills; one main ability;
- at least 2 at-wills, one of them damaging;
- a `rest` pool and a `camp` pool, with 40–60% of limited power from
  camp. **(agreed 2026-11-18, Vanguard Q1; change VG-13)** The share
  counts the `restDefensiveTool` at half value; all three measures (full,
  half, excluded) are reported, Momentum included; the band holds at
  levels 5 and 10; the level-1 martial miss is accepted (Vanguard design
  10.4), and for the Shade it is extended to levels 1–4 **(client, with
  the Shade design's approval on 2026-11-18; change SH-28: this widens
  an agreed reading; Shade design 10.5)**, and for the Arcanist it is
  extended to levels 1–2 (26% and 35%) **(agreed 2026-11-20, Arcanist
  Q2; change ARC-4: this widens an agreed reading; Arcanist design
  11.8)**, and for the Herald it is extended to levels 1–3, with levels
  3–4 read on the class mean over both subclasses, subclass rows printed
  **(agreed 2026-11-20, Herald Q2; change HR-31: this widens an agreed
  reading; Herald design 11.10)**;
- exactly one `restDefensiveTool` **(change CMP-24)** (binds class
  records, not companion kits);
- subclass at level 3 and capstone at level 9;
- the cap of eight limited abilities;
- `effectKinds` declared for the copy registry;
- a starting kit with 2 potions.

**One rest pool that asks a question (lead's call).**

| Class | Rest pool | The question | Its `restDefensiveTool` |
|---|---|---|---|
| Vanguard | Techniques | What does this hit buy? | Second Wind: full Recovery (the one fighter-type tool) |
| Shade | Tricks | Escape, soak or mend? | Catch Breath: half Recovery |
| Arcanist | Weft | Sculpt, guard or mend? (ARC-1) | Knit: half Recovery |
| Herald | Cadence | Frighten, move or mend? | Rekindle: half Recovery |

Every rest defensive tool heals, so no class pulls the shared floor-1
boss down for everyone, and no `restDefensiveKinds` entry is needed at
launch.

**One use per rest (lead's call).** A `restDefensiveTool` may be used at
most once per rest, twice from level 6 (matching Second Wind), whatever
pool it draws on. Catch Breath, Knit and Rekindle each carry their own
`rest` sub-limit pool of 1 (2 from level 6), which the ability spends
together with its shared pool. The sub-limit is not a named pool: it is
exempt from the 3-named-pool limit and from the cap of eight (3.4). So a
Shade, Arcanist or Herald heals at most half Recovery once per rest (twice
from level 6), and Second Wind stays the one full-Recovery rest tool.
`Floor1BossSustain`'s `restToolHeal` (DH 21.1 step 6) is the heal times
the sub-limit's uses at the boss-room level.

**Save training follows one principle (lead's call):** a class trains the
save its **main ability feeds** (stat model 6: Fortitude = Strength or
Constitution, Reflex = Dexterity or Intelligence, Will = Wisdom or
Presence). It is legible, stays stable when the player rearranges the
array, and leaves each class's weak save as part of its named weakness.
Reflex appears twice, which is unavoidable with four classes and three
saves.

### 8.3 VANGUARD: defender and striker (Strength martial; the M1 slice class)

| Chassis | |
|---|---|
| Main / suggested second | Strength / Constitution |
| HP tier, save | Sturdy 8; Fortitude |
| Armour, shield, weapons | Heavy ceiling; shield allowed; simple + martial |
| Skills | 2 of Athletics, Intimidation, Medicine, Perception, Lore |
| Complexity | Simple |

- **Leaned utility:** protection, primary. Weapon craft and riders on
  demand, secondary. Its own tempo through Surge.
- **Signature verb: Techniques, "decide after the hit".** The ability 6.6
  template exactly (ability 6.6, lead's call there; numbers illustrative):
  - **Second Wind:** `rest` 1, 2 from level 6. A bonus action that
    regains your Recovery. The tagged `restDefensiveTool` and **the one
    full-Recovery rest tool** (DH 12.1).
  - **Techniques:** `rest` 2 / 3 / 3 *(illustr.)* (3 from level 5;
    VG-6): **Press** (push 1), **Hew** (+1d4 at levels 1–4, +1d6 from
    level 5; Hews per rest at most the class-table pool), **Empower**
    (apply the weapon's canonical Empower line), or (from M2) **Bite
    Deep** (the weapon's rider). For a Wrecker, Ram stands in for Press.
    At most one free effect per hit, and it is that hit's Technique
    (VG-2; Vanguard design 6.4–6.6).
    **Timing (lead's call):** at most one Technique per hit, with one
    carve-out, the Bastion's Square Up II pairing (VG-2; Vanguard design
    6.4 rule 2, 8.2); spent after
    the hit and the crit are known, before damage is rolled (ability 6.6);
    Hew is a separate damage part, doubled on a crit. A Technique spent
    on an event other than a hit (a Shove, a grapple, a Graze, turn
    start) uses the `onEvent:<id>` cost timing (VG-7). Usable on reaction
    attacks. On another unit's turn a Technique follows a per-unit policy
    {never, ifKills, always, ask}, default `never`; companions and the
    automatic flows use the same policy.
  - **Surge:** `camp` 1, 2 from level 5 (VG-6). One extra action, using
    the turn's camp spend.
  - These are 3 named pools, the martial maximum.
- **At-wills:** a weapon attack with its verb, and a shield bash or a
  second weapon mode.
- **Creation pick, a stance (lead's call):** a creation pick, not a
  toggle: it has no on/off state and is never in `activeToggles`. **It
  is no longer permanent: the stance may be re-picked once per camp**, on
  the camp screen, never in combat (agreed 2026-11-18, Vanguard Q3;
  VG-17; Vanguard design 7.5). Both stances are pool-free reactions with automatic
  policies, so they count 0 toward reading (7.6). At M1 the pick has one
  option, Hold the Line; the ladder's "2–3" applies from M2.
  - **Hold the Line** (M1): an opportunity attack that hits **sets the
    mover's remaining movement for this turn to 0**. Its speed is
    unchanged and it is not a condition (CL-6). Movement granted
    afterwards by Dash (whatever its action cost) or by Spurred is added
    as normal; forced movement is unaffected. So escaping costs the mover
    an action or a granted move, and the composite goad-plus-move-stop
    lock stays out of reach. **Keyed to `{leaveZone, slipPast, setHit}`**
    (VG-2): an opportunity attack on a hostile leaving your zone or
    slipping past you (`slipPast` needs the Bastion clause or
    `slipPastProvokes`), or your Set hit (`setHit` needs the Bastion
    clause or `onSetHit`) (Vanguard design 7.4).
  - **Shieldwall** (M2): a pool-free Interpose reaction for an adjacent
    ally, **+2 AC (the settled Interpose value, armour 10.3; enemies
    17.1)** against one attack roll, policy `ifItChangesOutcome`. It is
    **learned** exactly when picked; for Guarding shields, legality reads
    "learned" (CL-7), so a Shieldwall Vanguard can never carry Guarding.
    A Vanguard who wants Guarding gear picks Hold the Line.
    **Requires a shield** in the active set. **Solo clause:** while no
    ally is within 2 squares of you, it may instead cover you against a
    ranged weapon attack roll (VG-3; registry pending VG-18; Vanguard
    design 7.5).
- **Level-1 identity:** "I hit, *then* decide what the hit does." Four
  things to read.
- **Growth:**
  - level 2: a talent. **The M1 menu uses M1 conditions only** *(names
    and texts illustr.)*: *Drive Through* (once per round, when the hit's
    combined move includes a Press, Ram or Empower push part, Shove the
    target as part of that hit at step 10, before the movement; on a
    success choose Prone, own turn only, or +1 square to the push, a
    non-verb part; it costs no Technique and replaces no attack), *Pivot*
    (the push may instead pull 1 or shift the
    target 1 square sideways) and *Grim Resolve* (Second Wind also ends
    one of Frightened, Dazed or Bleeding on you, and you stand from Prone
    at no movement cost). **The M2 menu** is Drive Through, Pivot and
    *Low Cut* (a Staggered option on a Technique); Grim Resolve moves to
    level 7 (VG-2; Vanguard design 7.6);
  - level 3: subclass;
  - level 5: Extra Attack, Techniques 3, Surge 2 (VG-6);
  - level 6: subclass feature;
  - level 7: talent;
  - level 9: capstone, for example *Stand Fast* *(illustr.)*: a bonus
    action and the turn's camp spend, **shield required**; until the
    start of your next turn, **every** attack roll against an ally
    adjacent to you takes Interpose's +2 (settled) as a window fill
    inside that attack's one reaction-AC-bonus window, with no reaction
    spent. **Solo clause:** with no ally in range, it covers you against
    ranged weapon attack rolls. An automatic **subclass rider** at level
    9 (Bastion: allies within 2 squares; Wrecker: push a missing melee
    attacker 1; Weaponmaster: a free payable Empower line on that
    attacker) (VG-15; Vanguard design 7.7). **(lead's
    call: it is a capstone effect, not a reaction, so it is no second
    reaction; the stage-2 registry and envelope checks confirm it.)**
- **Subclasses:**
  - **Weaponmaster** (M1): keyed to weapon verb ids, with group tags as
    `requires` filters (VG-2); once per round (Practised Hand), apply the
    hit's payable Empower line without spending a Technique, as that
    hit's free effect. M2 signature: Borrowed Form (Vanguard design 8.1).
  - **Bastion** (M2): **Square Up** (stage 1's Challenge, renamed) as a
    Technique option, own turn only, applying Challenged on a hit (M2;
    source "tank challenges", settled); at most one Square Up per round
    across every source. Only Called Out and Step In may apply it without
    a hit. A feature for each stance: with Shieldwall,
    its reach grows to allies within 2 squares; with Hold the Line, the
    move-stop also applies when your Set hits, and a hostile that slips
    past you provokes you (VG-2; Vanguard design 8.2).
  - **Wrecker** (M2): **card replaced (VG-2; Vanguard design 8.3).**
    Battering Force (a verb-only move stopped early deals collision
    damage, once per round), Brink (choose the forced-movement distance;
    a hostile left beside a hazard edge becomes Teetering), Ram (replaces
    Press: push 1; Prone on a failed Fortitude save if stopped early),
    then Domino or Upend at level 6. Prone from Wrecker features is a
    tool of the forced-movement lean, not a control lean (lead's ruling).
    The stage-1 clauses (Technique pushes collide; Teetering on hazard
    edges) are base rules for every unit.
- **Weaknesses:** ranged enemies and fliers; slow to reach artillery;
  loud (Clatter); few skills.
- **Cap respected:**
  - Square Up is pooled, never applied at will on every hit.
  - Challenged keeps its own end rules: 2 turns; it ends at more than 6
    squares or when the challenger **targets** another unit with a
    harmful effect (conditions 18.8; the paraphrase corrected, VG-2); the
    boss shed action removes it.
  - One reaction AC bonus per attack.
  - Surge competes with scroll reads for the camp spend.
- **Camp share (agreed 2026-11-18, Vanguard Q1; change VG-13):** the
  6.6 template is rest-heavy (ability 6.3 flags this). Measured with
  Second Wind at half value (all three measures reported, Momentum
  included), the 40–60% band holds at levels 5 and 10 (41% and 40%
  *(illustr.)*) through Surge 2 from level 5, the Arts and Stand Fast;
  the level-1 martial miss is accepted. Simulator evidence at levels 1,
  5 and 10 (Vanguard design 10.4).
- **M1 content:** M1 conditions only; Hold the Line; Press, Hew (1d4)
  and Empower (M1 lines) Techniques only; Momentum (agreed 2026-11-18,
  Vanguard Q2; VG-6). M1 has no riders (conditions 18.1;
  ability 14.6), so the rider option and rider talents such as Low Cut
  arrive at M2. No staff, no Concentrating.
- **Fallback type:** its weapon's physical type, with a second physical
  type from the spare set.
- **Signature events (12.1):** a Technique option applied (spent, or
  granted free by the Weaponmaster); a Hold the Line stop; a Shieldwall
  Interpose. `seedTarget`: `anyEnemyInMelee`.

### 8.4 SHADE: skirmisher and specialist (Dexterity martial)

| Chassis | |
|---|---|
| Main / suggested second | Dexterity / Intelligence |
| HP tier, save | Standard 7; Reflex |
| Armour, shield, weapons | Light ceiling; shield never; `weaponTraining: {simple: all, martial: light \|\| finesse \|\| group:launcher}`, a filter resolved to a family id list at build time (12.1) |
| Skills | 3 of Stealth, Acrobatics, Mechanics, **Arcana**, Perception, **Medicine** |
| Complexity | Medium |

- **Leaned utility:** stealth and openers, and its own tempo, both
  primary. Precision damage and dungeon skills, secondary.
- **Signature verb: Opening,** the precision feature weapons 6.4 requires
  **(agreed requirement)**:
  - **Once per round** (3.4): a pool of 1 with refill `turn`, from the
    start of your turn to the start of your next. That is stricter than
    once per turn, so reaction attacks cannot fire it a second time.
    On a Dexterity-rolled weapon hit it adds the **tiered band**, by
    **Shade level** **(agreed 2026-11-18, Shade Q1; change SH-1)**: the
    **stealth** amount (1d6 at levels 1–4, 2d6 at 5–10) if any
    stealth-tier trigger is met, otherwise the **open** amount (+2 flat
    at 1–4, 1d6 at 5–10) *(illustr.)*; no further steps. It replaces the
    stage-1 band (1d6 at 1, one more die at 3, 5, 7 and 9) (Shade design
    6.4). The dice are a separate damage part, doubled on a crit; the
    flat +2 is not doubled.
  - **Applying it (lead's call):** the player may decline Opening on a
    qualifying hit; the automatic policy is
    `applyOnFirstQualifyingHitOnOwnTurn`, so an opportunity attack does
    not spend it unless the policy is changed.
  - **Triggers** (each trigger's tier, `stealth` or `open`, is data;
    change SH-2; Shade design 6.5, 6.6):
    1. **Unready** (open): the target has not started a turn this fight.
       **Surprised** (stealth): the target is Surprised. (Stage-1
       trigger 1, split; **agreed 2026-11-18, Shade Q1; SH-2**.)
    2. **Hidden** (stealth): you are Hidden from the target.
    3. **Beset** (open; stage-1 trigger 3, renamed, **agreed, Shade Q1;
       SH-2**): the target is Prone, Staggered or Grappled; or another
       unit's melee weapon hit landed on it since the end of your previous
       turn, and your attack is melee.
    4. **Answer (lead's call)** (open): the target missed you with an
       attack roll, or you Ducked one of its hits, since the end of your
       previous turn (SH-2). This is the trigger that works solo and in
       melee against a boss.
  - **Launchers** qualify on Unready, Surprised, Hidden and the Vanish
    carry (the stage-1 rule restated for the split; SH-2; Shade design
    6.4). Caught is added when the Trapwright ships (SH-17; Shade design
    14).
  - **Opening and Exploit may both fire.** Parity is proven with both,
    including the worst case of Hidden plus Vanish and the Answer trigger,
    solo and in the reference party, with reaction attacks modelled
    (CL-8).
  - **Granted attacks** (`grantedBy`, 3.4) never trigger Opening,
    Techniques or any other class rider. Exploit applies to them as
    weapons 6.3 states.
- **Pools:**
  - **Tricks** (`rest` 2 / 3 / 4 at levels 1 / 3 / 8 *(illustr.; change
    SH-24)*). Three separate abilities draw on it:
    - **Fade** (change SH-32; Shade design 7.4): a bonus-action Hide,
      Disengage or Dash, pooled, its Hide mode strike-limited (tagged
      `strike`, Shade design 7.9): the Cunning-Action shape,
      pre-cleared against Runner's Boots; a pooled bonus-action Hide is
      the class hook of ability 7.2 (Shadowed armour gives one per fight,
      armour 14.2: a charged echo checked in the registry pass);
    - **Duck:** a reaction that halves one hit's damage. It is not
      Dodging; registry check pending;
    - **Catch Breath:** a bonus action that heals half Recovery; the
      tagged `restDefensiveTool`, once per rest (twice from level 6; 8.2).
  - **Vanish** (`camp` 2, 3 from level 2 *(illustr.; change SH-24)*, so
    the camp share reaches the band from level 5, SH-28): a bonus
    action. You Hide **without needing to break line of sight**, still
    rolling Stealth against passive perception (CL-12c);
    Hidden's own end rules (conditions 18.7) still apply, so in melee
    reach it usually ends at the end of your turn. **Whether or not the
    Hide succeeds,** your first Dexterity-rolled weapon hit on your own
    turn to which you apply Opening before the end of your next turn,
    against any target, counts as **Hidden for Opening** (the stealth
    tier) **(agreed 2026-11-18, Shade Q1; change SH-2)**; that hit
    consumes it (Shade design 7.5).
    So "strike, then Vanish" sets up next round's Opening even in melee.
    No "maximised dice" option.
- **At-wills:** a melee weapon attack and a thrown or launcher attack.
- **Creation pick:** blades or launcher kit. Skills have recommended
  defaults.
- **Level-1 identity:** "I set the fight up so my first blow lands huge,
  then I slip away and set up the next." Three things to read.
- **Growth:**
  - level 2: a talent, for example *Deep Pockets* (belt +1, hard maximum
    3; items 24), *Light Fingers* (advantage on Steal) or *Tainted Blade*
    (Opening may apply Poisoned instead of its stealth-tier dice only;
    change SH-32; Shade design 7.6);
  - level 5: Extra Attack and an Opening step;
  - level 9: capstone, for example *From Nowhere*: this turn, every
    Dexterity-rolled weapon hit takes Opening at the stealth die,
    triggers waived, inside the capstone nova bound (SH-32; Shade design
    7.7).
- **Subclasses:**
  - **Cutthroat** (M2): **card revised (change SH-31; Shade design
    8.1):** **Quiet Work**, a silent kill of a non-minion member marks
    its group `broken`; and the **Surprised part** on Opening against
    Surprised targets, +1 at levels 3–4 and +1d4 from 5 (a stealth-tier
    extra part on Opening).
  - **Trapwright** (M2 only if trap records, DEP-S1, land in M2;
    otherwise the first Shade unlock, the expected default) **(agreed
    2026-11-18, Shade Q2; change SH-30; Shade design 8.2, 14)**; **the
    fallback is applied: the first Shade unlock, M3** (2026-11-21;
    change TL-37; traps and locks design 6.16): **card revised
    (SH-31):** **Jury-Rig**, a Tricks option that springs or
    re-arms a known trap (stat model 7.1), and the **Caught** trigger on
    Opening; a Mechanics-mastery lane.
  - **Swordsworn** (the first unlock; promoted to M2 under the
    fallback, applied 2026-11-21, change TL-37) **(agreed 2026-11-18,
    Shade Q2; change SH-30)**: **Sworn
    Guard**, a Tricks-paid ward that counts as a Parry, with
    **Counterstroke**, a pooled reaction strike after a turned blow
    **(agreed, Shade Q3; change SH-31: the mechanism of the stage-1 "a
    pooled reaction strike after a Parry" changes, not its intent;
    Shade design 8.3)** (Riposting is Later).
  - Each subclass record carries `wave` and `fallbackWave` (SH-30;
    Shade design 13.1).
- **Cut:** "Quiet Company" (it bends worst-member Stealth) and "Feint" (a
  Steady-Aim and Luckstone copy).
- **Weaknesses:** fragile once found; solo boss rounds 3 and later lean
  on the Answer trigger and Vanish, and every Tricks use spent on a Hide
  is a heal or a Duck not taken; enemies with many eyes; a loud party. It
  cannot carry heavy-armour allies past the worst-member rule.
- **Requirements carried:** precision with launcher applicability stated
  (weapons 6.4, 6.5); DEP-1 (consumables 13.8); the bonus-action Hide
  hook; Perception (so Tactician is reachable); Medicine (DH 8, "close to
  compulsory" on solo floor 1).
- **Fallback type:** piercing or slashing by weapon set.
- **Signature events (12.1):** Opening dealt or traded; a Vanish or Art
  used; a Duck that set Answer; a subclass signature applied; a From
  Nowhere hit (change SH-32; Shade design 7.1).
  `seedTarget`: `enemyNotYetActed` (an unaware first target).

### 8.5 ARCANIST: controller (Intelligence full arcane caster)

| Chassis | |
|---|---|
| Main / suggested second | Intelligence / Dexterity |
| HP tier, save | Slight 6; Reflex |
| Armour, shield | **Light ceiling (agreed, Q5);** shield never |
| Weapons, foci | Simple; arcane foci (orb, rod, tome) and the staff, never required |
| Skills | 2 of Arcana, Lore, Mechanics, Medicine, Perception |
| Complexity | Complex |

- **Leaned utility:** area damage and control, primary. Damage types and
  terrain combinations, secondary. Knowledge and initiative are ribbons.
- **Signature verb: the element pair.**
  - At creation, choose two families from fire, cold, lightning and acid.
    The pair is **fixed for the run** (hero state `elementPair[2]`) and is
    never re-chosen, at camp or anywhere else.
  - **Data model (lead's call):** spells carry a fixed family. Arcanist
    damage spells are authored in family variants, and the spell cards
    offered are filtered to the pair; the element bolt takes one family of
    the pair, chosen at creation. The camp spell swap swaps one known
    spell for another inside the pair, never the pair. So the Arcanist
    holds two keys through the spells it knows, never ten, and never picks
    a family per cast (the "bring-the-right-key" anti-goal).
  - Areas are shaped by Weft. The physical fallback is the staff or a
    simple weapon (6.5).
- **Pools:**
  - **Slots:** a `camp` pool per tier, with automatic Slot Recovery
    (settled, ability 6.5).
  - **Weft** (`rest` 3 / 4 / 5 / 6 at levels 1 / 5 / 7 / 9 *(illustr.;
    change ARC-11, replacing the earlier 2 / 3 / 4; Arcanist design
    7.5)*; Sculpt II at 5, Flare II at 8): the rest feature beyond Slot
    Recovery that ability 6.3 asks for. Three abilities draw on it:
    - **Sculpt** (renamed from "Shape", which collides with the rod's
      focus verb, weapons 17.8; change ARC-1), with modes: make an area
      `selective`, +1 radius inside the cap, or give a cantrip its rider
      flag for one cast;
    - **Flare:** a reaction for at most +2 AC against one attack, policy
      `ifItChangesOutcome`, inside the one-reaction-AC-bonus window and
      the armour 11 item 1 envelope;
    - **Knit:** a bonus action that heals half Recovery; the tagged
      `restDefensiveTool`, once per rest (twice from level 6; 8.2).
  - **Weft report and lever (lead's call):** the simulator reports the
    Arcanist's friendly-fire damage share and its Weft spend split (Sculpt,
    Flare, Knit) in the reference party. The named lever: Knit moves to
    its own `rest` pool of 1, leaving Weft for Sculpt and Flare (the
    Arcanist then counts 7 toward the cap of eight and has 4 things to
    read, using its one spare row).
- **At-wills:** an element bolt (a rider only on weak targets and crits),
  and a utility cantrip (a minor effect such as push 1, never hard
  control). **(change ARC-10)** The bolt is **Mote**, 1d10 + Int of the
  Mote key's family, two beams from level 5; the cantrip is **Gust**, a
  line L4 that pushes the first hostile 1 square on a failed Fortitude
  save (Arcanist design 7.2).
- **Interplay** (base passive, `interplay.enabled`, on by default;
  change ARC-18): damage of one pair family marks a hostile, and your
  next hit on it with the other family is rider-flagged (3.4; Arcanist
  design 10.3).
- **Concentrator** 1 of 2 (Stillmind, Later).
- **Level-1 identity:** "I shape where damage goes and what it leaves
  behind." Three things to read.
- **Growth:** spell cards at levels 2, 5, 7 (and 10); the subclass at 3
  grants its tier-2 spell (the level-3 known spell, 7.4); tier 3 at 5;
  tier 4 at 7; capstone at 9, for example *Overchannel* *(illustr.)*:
  cast a known spell of tier 3 or lower without a slot, with one free
  Sculpt mode (ARC-1), still using the turn's camp spend; **Fused**: its
  pair-family parts count as both families for Interplay (ARC-18;
  Arcanist design 7.9).
- **Subclasses:**
  - **Binder** (M2): longer and more reliable control, never chained on
    one target.
  - **Alembic** (M2): **priming.** Once per round, through a Weft option
    (an option of the Weft pool, not a new limited ability), a cantrip hit
    makes its target Soaked or Oiled (minor), so the party's typed hits
    land riders. **(change ALM-9)** Priming may also ride the Alembic's
    slot spells, Overchannel and named triggers; still once per round,
    still a Weft option (Arcanist design 9.2). An Oiled-plus-fire chain
    needs a slot: **(agreed 2026-11-20, Arcanist Q3; change ALM-1)** the
    inert rule widens this to every prime and every at-will reading: your
    own primes are treated as absent by your own at-wills, and your
    at-will's rider is not attempted where a cancel, override or wash row
    would fire against your inert reagent (Arcanist design 9.2). The
    Water and Oil Flasks are checked against it first in the registry
    pass (14).
  - **Loremaster** (unlock): Study as a free rider on the first hit.
  - **Elementalist** (unlock aspect): ignore resistance in one family.
- **Arcana mastery (new lead's call):** the Loremaster's Study rider is
  its own subclass feature, **not** Arcana mastery's signature use. This
  design does not propose the "Arcana mastery as a no-roll threshold for
  out-of-class reads" option (consumables 25); Arcana mastery's signature
  use stays with stage 2, and goes back to the client only if stage 2
  proposes the no-roll threshold.
- **Armour (agreed, Q5):** a **light ceiling** at launch. The ceiling-none
  glass mage fails armour 11 item 2 at the worst Dexterity of its array
  under every drafted formula, and it would tighten the enemies 6.4
  generation guards for every party. The Spellguard alternative is
  recorded as rejected (section 4, row 5).
- **Weaknesses:** low HP; friendly fire; concentration under focus fire;
  single-target bosses (resolve pips, Unyielding).
- **Signature events (12.1):** a damage part typed from the pair dealt by
  any ability; a Sculpt used (ARC-1); an Interplay rider landed (ARC-18;
  Arcanist design 7.1). `seedTarget`: `twoEnemiesWithinRadius1`.

### 8.6 HERALD: morale and support (Presence full caster; the single light healing lean)

| Chassis | |
|---|---|
| Main / suggested second | Presence / Wisdom |
| HP tier, save | Standard 7; Will |
| Armour, shield, weapons | Medium ceiling; shield allowed (a Totem-or-shield kit choice); `weaponTraining: {simple: all, martial: hands == 1}`, read as one-handed and versatile (`hands ∈ {oneHanded, versatile}`; change HR-9; Herald design 7.1), resolved to a family id list at build time (12.1) |
| Foci | Presence tradition: Totem (Loom, +3 range on Frighten and on hostile-facing presence effects only; change HR-15; Herald design 7.1); never required |
| Skills | 2 of Leadership, Intimidation, Medicine, Lore, Perception |
| Complexity | Medium |

- **Leaned utility:** morale, primary; it pays most as the hero (stat
  model 5.4). Ally tempo, secondary. Healing only through **spell cards**
  and the **Hearth** subclass. **(agreed, Q1)**
- **Signature verb: Cadence, "break them before you kill them".**
  - **Cadence** (`rest` 4 / 4 / 5 / 7 / 8 at levels 1–4 / 5–6 / 7–8 / 9
    / 10 *(illustr.; levels 1–4 agreed 2026-11-20, Herald Q2; change
    HR-1, replacing the earlier 2 / 3 / 4; Herald design 7.3)*). Three
    abilities draw on it:
    - **Frighten as a bonus action** at range 6: the T5 route, already M2
      **(settled)**;
    - **you or one ally gains Spurred:** a new class source (CL-5);
      Spurred costs no speed, so it is not +speed, and on solo floor 1 the
      Herald grants it to itself. **(change HR-2; told plainly)** This is
      **Spur**: no action, cost `onEvent: ownActionResolved`, once per
      round; after your action resolves, you or one ally within 6 gains
      Spurred. Spur on a Rallied ally is legal (Herald design 7.3, 7.4);
    - **Rekindle:** a bonus action that heals half Recovery; the tagged
      `restDefensiveTool`, once per rest (twice from level 6; 8.2); it
      feeds `Floor1BossSustain`.
  - **Rally is unchanged:** once per ally per fight. Cadence makes the
    Herald's turns richer, never Rally more frequent.
- **At-wills:** **Harrowing Word** (psychic, Will save; **(change HR-3;
  told plainly)** 1d10 + Pre on a failure, Pre on a success (Undertone;
  minions none); a **Faltering** target, one that is Frightened or that
  spent a resolve pip since your last turn, makes the save with
  disadvantage, replacing the earlier "+1 die" (fallback +1d4), so fear
  also pays as damage, bosses included; **Plain Speech:** bludgeoning
  against a psychic-immune target, failure and Undertone damage alike; it
  never applies Frightened itself; Herald design 7.2) and a weapon
  attack.
- **Slots and spells:** `camp` slots.
  - The fear spells need **T6 moved to M2 (CL-1).** CL-1 is applied
    (2026-11-18), so T6 for class fear spells is at M2 (cross-document
    comparison 2026-11-23, CC-61).
  - Marching calls grant Spurred to several allies.
  - **Heal cards:** *Mending Word*, tier 1, a bonus action at range 6,
    healing exactly the target's Recovery; higher tiers add targets, never
    HP per target: **(change HR-6; told plainly)** targets 1 / 1 / 2 / 3
    at tiers 1–4; its tier-2 upcast entry is declared and adds no target,
    so a tier-2 slot is legal, and the cast's tier is the slot spent
    (Herald design 5.3, 8.1). A single-class Herald may take it from level 1 (7.7
    guard 4 binds only a second class). **It may target the Herald
    itself (lead's call; confirmed by the client, 2026-11-18)**: it is a
    `camp` slot spell under DH 12.2, not a rest tool, so Second Wind
    stays the one full-Recovery *rest* tool.
    `Floor1BossSustain` and `Floor1ClassSpread` are run for the Herald
    with and without a heal card. A Herald who never takes a heal card
    counts as a party without a healer under DH 18.1; one who does counts
    as a healer, because the card's targets include an ally.
  - The card shares its name with the consumables 16 scroll on purpose:
    scroll names follow class ability names (consumables 17), so the
    scroll's `classTags` include the Herald.
- **Concentrator** 2 of 2.
- **Level-1 identity:** "I break them before I kill them." Floor 1 always
  has a cowable group on the main path before the boss room (enemies
  10.4, 14.8; CL-10). Three things to read.
- **Growth:** spell cards at 2, 5, 7 and 10; capstone *Scatter* at 9: a
  limited fear effect through the pipeline. **(change HR-5; told
  plainly)** Each Demoralised enemy in sight that is neither a boss nor
  fearless makes a Will save with disadvantage and on a failure routs by
  T6 substitution; every other hostile in sight, **bosses and fearless
  units included**, makes a Will save or is Frightened `turns:1`, and is
  never routed (Herald design 7.9). Wards, fear and mind immunity,
  Rallied and Emboldened block or resist it as usual, and "Routed wins"
  over Panicked. Gated on CL-1, which is applied (T6 at M2;
  cross-document comparison 2026-11-23, CC-61).
- **Subclasses:**
  - **Dirge** (M2): **Antiphon** (not "Two Voices"; change HR-8):
    Cadence's Frighten may target two enemies within 2 squares of each
    other, one roll per target (T5 for each); the tier-2 spell **Toll**
    with **Mourners**; **Knell**; Demand Surrender at range 4 with the
    Totem; **Refrain** or **Parley** at level 6; **Last Toll** at 9
    (Herald design 9.1). It works only through the existing triggers T1–T6; it adds
    **no new Demoralise trigger** (enemies 14.3 stays fixed), and a class
    Demoralise trigger would need its own change row.
  - **Hearth** (M2): **(change HR-7; told plainly; `HearthNoHeal`
    accepted with Herald Q1)** **Hearthkeeper:** +1 side-by-side target
    on class heal-card casts at tier 2+ in combat, and on Soothe; the
    heal card also ends one poison condition, and on a Withered target
    ends Withered instead of healing. No Hearth record heals
    (`HearthNoHeal`). Cures with no HP may ride Spur; heals may not.
    Treat at range 2 (Herald design 9.2). **The healer-gap lever.**
  - **Warcaller** (unlock): an ally attacks with its *own reaction*, once
    per ally per round; the attack carries `grantedBy` (3.4), so no class
    riders apply.
  - **Pennant** (unlock): a concentration aura of tempo.
- **Bonus-action conflict (change HR-2; told plainly):** Mending Word,
  Cadence's Frighten and Rekindle, and Rally share the bonus action, so
  only one of them fits a turn; Spur rides the action. The level-up card
  and the preview say so (Herald design 7.3).
- **Weaknesses:** bosses (always `fearless`, enemies 14.2) and
  fearless-heavy rosters (undead, constructs) mute its morale half;
  modest damage; as a companion it loses Presence.
- **Floor-1 guarantee:** floor 1's guaranteed cowable group lies on the
  main path before the boss room (CL-10). Layout hosts it on the floor-1
  Circuit, which always includes main-path Q room 3, and the floor's one
  route-avoidable fight is a guard post (changes FL-9, FL-10; floor
  layout design 6.7, 6.8). Curiosity noise is generic noise at its own
  square, never a Lure, and its radius may not reach a Circuit square
  (`F1NoiseClearOfCircuit`; change EV-14, events design 4.4.4).
- **Fallback type:** its weapon's physical type; Harrowing Word's psychic
  is a control rider, not its fallback (6.5). **(change HR-3)** Plain
  Speech makes Harrowing Word bludgeoning against a psychic-immune
  target (Herald design 7.1).
- **Signature events (12.1):** Frightened or Routed applied by the
  Herald, or a Demoralise its loss event triggered; a Faltering save
  rolled with disadvantage (change HR-4, replacing "Harrowing Word's
  bonus die dealt"); a resolve pip spent to the Herald's fear or Dazed
  effect (its own row; HR-4; Herald design 7.1). `seedTarget`:
  `cowableGroup`.

### 8.7 The four at a glance

| | Vanguard | Shade | Arcanist | Herald |
|---|---|---|---|---|
| Main | Str | Dex | Int | Pre |
| HP tier, save | 8, Fortitude | 7, Reflex | 6, Reflex | 7, Will |
| Armour / shield | Heavy / yes | Light / no | Light / no | Medium / yes |
| Primary lean | Protection | Stealth, own tempo | Area, control | Morale |
| Signature verb | Techniques | Opening | Element pair | Cadence |
| `rest` pool | Techniques (+ Second Wind) | Tricks | Weft | Cadence |
| `camp` pool | Surge | Vanish | Slots | Slots |
| M2 subclasses | Weaponmaster (M1), Bastion, Wrecker | Cutthroat, Swordsworn (the DEP-S1 fallback, applied; the Trapwright is M3; change TL-37) | Binder, Alembic | Dirge, Hearth |
| Things to read at level 1 | 4 | 3 | 3 | 3 |
| Complexity | Simple | Medium | Complex | Medium |

## 9. Unlocks and rejected concepts

### 9.1 Unlock candidates (main 4.7 milestones)

**(lead's call)**

| Concept | Main / HP / save / ceiling | Leaned utility and core mechanic | Why unlock, not launch |
|---|---|---|---|
| **Votary** (cleric-type, prevention first) | Wis / 7 / Will / medium + shield | Warded charges, Revealed, Treat at range 2; Recovery heals; a self-Warded rest tool needs a `restDefensiveKinds` entry, or else a half-Recovery heal | The first unlock; a Wisdom main; the Slate B healing home |
| **Tracker** (ranger-type, martial) | Wis (Dex second) / 7 / Reflex / medium | **Prey:** a bonus-action mark that is a buff on the hunter (conditions 18.11 moved Hunted here), outside the condition budget; extra dice on the marked target, launchers included | A Wisdom main; most likely to lift launcher builds; overlaps the Shade |
| **Rootwarden** (druid-type) | Wis / 7 / Will / medium + shield | A primal list on the divine tradition; Restrained and terrain control on existing squares; no shapeshifting or summons | Gives the [primal] scroll hints a home |
| **Holdfast** (pure defender) | Con / 8 / Fortitude / heavy + shield | Interpose, Challenge and a hold stance | A Constitution main; its natural states (Guarded, Fortified) are Later |
| **Berserker** | Str or Con / 8 / Fortitude / medium | *Fury,* a self-only toggle with a cost | Raging and Reckless are Later; needs a ruling on self-only toggles as modifiers outside the 32 |
| **Adept** (monk-type) | Dex (Wis second) / 7 / Reflex / none + unarmoured defence | Stances that rewrite its at-wills; precision keyed to Staggered or Prone | Needs caster-style AC evidence (armour 11 item 2) and unarmed upgrades |
| **Sapper** (alchemist or engineer) | Int non-caster / 7 / Reflex / light | Belt +1; class bombs (a class pool, never an item); trap rigging | Overlaps the Trapwright; depends on trap density. SH-26 applies here when the Trapwright ships (Shade design 14) |
| **Marshal** (commander) | Int or Pre / 7 / Will / medium + shield | Orders that move the giver's action to an ally, with a self mode | Weak on solo floor 1; lengthens companion turns |
| **Hexer** (setup curse) | Pre / 6 / Will / light | A Baned-based curse (Hexed was folded into Baned, conditions 18.11; Baned is Later, so it needs the budget ruling, 9.3), or a change to conditions 18.11 when the Hexer is designed | Folded into Herald Dirge for now; a later Presence alternative |
| **Beastbound** | — | A bonded companion in one of the 3 slots | Floor 1 is solo; needs proven companion kits |

### 9.2 Folded

Stormblood becomes an Arcanist aspect; Breaker becomes the Vanguard's
Wrecker; Duellist becomes the Shade's Swordsworn; Seer becomes the
Loremaster or the Votary; Captain becomes a Herald aspect. **(lead's
call)**

### 9.3 Rejected

**(lead's call)**

- **Summoner or pets:** there is no summon system (consumables 16).
- **HP as a resource:** it fights permadeath and the healing guarantee.
- **A key-swapping elementalist:** the "bring-the-right-key" anti-goal.
- **A pure healer:** it breaks the 10-point gap or is dead weight.
- **A drop-prevention class:** one recharging prevention per unit per
  fight (DH 15.4).
- **Any class whose identity is a Later condition,** until the condition
  budget is reopened.
- **The Spellguard glass mage** at launch (agreed, Q5).

### 9.4 Unlock order

**(lead's call)** Votary → Tracker → Rootwarden → Adept → Hexer,
Holdfast and Berserker (after the condition-budget ruling) → Sapper →
Marshal. How many ship at M3 is a content-volume decision for the M3
plan. **(change SH-30)** The first Shade unlock subclass is whichever of
the Trapwright and the Swordsworn did not ship in M2: the Trapwright,
the expected default, once DEP-S1 lands (Shade design 14). Resolved
2026-11-21: the fallback applies and the Trapwright is the first Shade
unlock, in M3 (change TL-37; traps and locks design 6.16).

## 10. Names

**(lead's call)** New content check **`NameCollision`:** no class,
subclass, ability or spell name may equal any condition, verb, property,
affix, AI profile, enemy ability, morale event or display term, trinket
or item id or name in the approved documents, or a name used in a
condition's source list. The one exception is a class spell whose own
scroll carries its name (consumables 17), **extended to a class spell
whose own wand carries its name (change ARC-20)**. It also **warns** on
a near collision: two names in the class layer that share their head
word (the rest tools are named apart for this reason). **(change
ARC-20)** An Arcanist spell variant's display name is its template name
plus a family tag ("Fan (cold)"); the variants of one template are one
spell family in data, exempt from the head-word warning (SP-3;
Arcanist design 8.1). All names are placeholders
pending the naming pass, which gives the four rest tools distinct first
words.

| Drafted name | Collides with | New name |
|---|---|---|
| Quarry (mark) / Stalker (class) / Hunter | The enemy focus mechanic; the Stalker AI profile; the Hunter affix (enemies 11.4) | Prey / Tracker |
| Weave | Gambeson armour property | Spellguard (rejected alternative) / Weft (pool) |
| Shaping | Weapon property ("Shaping: +1 radius") | Weft: Sculpt (was "Shape", which collides with the rod's focus verb Shape, weapons 17.8; ARC-1) |
| Shoulder | Breastplate property; armour verb Bull Rush | Drive Through (talent) |
| Bulwark | The tower shield's Bulwark Stance; Bulwark Locket | Bastion (subclass) / Holdfast (unlock) |
| Breaker | The conditions' "breaker types" | Wrecker |
| Volley | Weapon property | — (Tracker camp ability named at stage 2) |
| Deflect | Parry upgrade | Flare |
| Last Stand | DH's rejected downed model | Stand Fast |
| Dread | Enemy aura | Dirge |
| Slip | The universal "slip past" | Duck |
| Guard (stance) | The Guard verb read by Guarding legality | Hold the Line / Shieldwall |
| Duellist / Fencer | Monster role; "fencer stances" condition source | Swordsworn |
| Rebuke / Cutting Word | Hellish Rebuke (an SRD spell name); Cutting Words | Harrowing Word |
| Envenom | The Envenomed condition | Tainted Blade |
| Spark / Arcane Mantle / Force Dart | Wand of Sparks; Grey Mantle; force is not a type | Withdrawn |
| Last Wall / Last Line | A self-cancelling multi-target Challenge | Withdrawn |
| Hamstring | Hobbled's source "hamstring Techniques" (conditions 18.4) | Low Cut |
| Command spells | The enemy leader ability and affix Command; the Commanded condition (enemies 14.4, 17.2) | Marching calls |
| Rout (capstone) | The morale loss event "a Rout" and the Routed state (enemies 14.3) | Scatter |
| Banner (subclass) | The morale-state display banner and the group banner (enemies 14.8, 14.9) | Pennant (restated for the brief's "Banner"; HR-30; Herald design 9.4) |
| Second Breath | Near collision with Second Wind (shared head word) | Rekindle |
| Dread (Herald keyword) | Enemy aura; banned head word (above) | Faltering (HR-30) |
| Two Voices | Head word shared with Harrowing Word's "two words" | Antiphon (HR-30) |
| Graze (success damage) | The greatsword's weapon verb | Undertone (HR-30) |
| Steadying Spur / Steady Measure | Rally's Steady option and a morale profile | Soothe (HR-30) |

**(change HR-30)** The Herald design freezes its new names (Faltering,
Undertone, Plain Speech, Spur, Antiphon, Toll, Soothe and the rest) and
records its avoided names (Grim Refrain, Tend, Order, Falter, Kindle,
Field Dressing, Halting Word and others) in its front matter (Herald
design, front matter).

## 11. Binding requirements and how they are met

Status: **Met** = met by the concept; **Met (CL-n)** = met with a listed
change; **Stage 2** = the shape is fixed, the evidence is owed with the
tables; **Companions** = a requirement passed to the companions design.

### A. Structure and progression

| Requirement | Source | How met | Status |
|---|---|---|---|
| Species and class at creation; four classes at M2; more through unlocks | Main 4.5, 4.7, 9 | Slate A; unlock list (9) | Met (agreed, Q1) |
| M1 = one class, levels 1–3 | Main 9; weapons 26; cond 18.1 | Vanguard: no staff, no Concentrating, M1 conditions, Weaponmaster | Met |
| A choice every level; offers fill gaps; level-up well under a minute | Main 2, 4.5; ability 10.2–10.3 | Published ladder (7.4); one choice per level; +1 ability alone at 4 and 8; level 9 offer | Met (CL-4, agreed Q4) |
| Multiclassing only once single-class paths are fun | Main 4.5 | Dip guards (7.7); model decided at M4 | Met |
| Subclass at 3; pacing beats; capstone a `camp` pool of 1 | Ability 6.1, 6.7, 10.1 | Kept as approved | Met |
| Class suggests the spread; species give no points | Stat 5.1 | Suggested main and second per class; species on baseline axes | Met |
| One main ability; ability difficulty formula | Stat 5.5, 6 | Str / Dex / Int / Pre | Met |
| Every ability a main or a sensible second; Presence at risk | Stat 5.6 (target) | Presence main; Wis and Con seconds; unlocks fill the mains | Met as a target |
| One trained save | Stat 6 | One principle: the save the main ability feeds (8.2) | Met |
| 2–3 skills from the class list | Stat 7.2 | Lists in 8.3–8.6 | Met |
| Casting traditions: arcane = Int, divine = Wis, presence its own | Stat 5.2; weapons 17.8 | Arcanist arcane; Herald presence; [primal] waits for the Rootwarden | Met (CL-12) |

### B. Resources and abilities

| Requirement | Source | How met | Status |
|---|---|---|---|
| A `rest` and a `camp` pool; 40–60% camp; caster rest features beyond Slot Recovery | Ability 6.3 (agreed) | Every class has both; Weft and Cadence; Shade Vanish at camp 2, 3 from level 2 (SH-24); Vanguard camp-weighted features. Measured with the `restDefensiveTool` at half value; level-1 martial miss accepted (8.2; VG-13, agreed Vanguard Q1), extended to levels 1–4 for the Shade (8.2; SH-28, client with the Shade design's approval; Shade design 10.5), to levels 1–2 for the Arcanist (8.2; ARC-4, agreed Arcanist Q2; Arcanist design 11.8) and to levels 1–3 for the Herald, levels 3–4 read on the class mean over both subclasses (8.2; HR-31, agreed Herald Q2; Herald design 11.10) | Stage 2 (simulator at levels 1, 5, 10) |
| One camp spend per turn, scroll reads included | Ability 6.4; consumables 13.5 | No feature grants a second; capstones use the turn's spend | Met |
| At least 2 at-wills, one damaging | Ability 6.1 | Listed per class | Met |
| No cooldowns; no major power per fight | Ability 6.2 | `rest` and `camp` only | Met |
| At most 3 named martial pools | Ability 6.6 | Vanguard 3; Shade 2 | Met |
| At most 8 limited abilities | Ability 10.2 | One named pool = one limited ability, its abilities its options; a slot tier is such a pool; totals 3–6 by level 10 (7.5) | Met (CL-2) |
| Full casters only; spells known; about 7; one swap at camp | Ability 6.5 | The ladder gives 7, or 6 if the level-10 pick is an offer (7.4) | Met |
| Concentration only from a feature or offer; ally buffs over 10 turns need it | Ability 9.5–9.6 | Spells and auras; training through talents and offers | Met |
| Areas ≤ 25 squares at levels 1–6; friendly fire; at-wills never hit allies | Ability 8 | Weft respects the cap | Met |
| A policy on every reaction; no nesting | Ability 7.3 | Flare, Duck, Hold the Line and Shieldwall have default policies; Opening and Techniques on reaction attacks follow stated policies (8.3, 8.4) | Stage 2 |
| Riders: rare on at-wills, usual on limited abilities, on demand for martials | Ability 9.4; cond 18.1 (M1 has no riders) | Priming and the Shove talent on pools; Techniques' rider option from M2 | Met |
| Hard control only from limited abilities; agency enemy-only; 32-condition budget | Cond 7.1, 7.3, 16 | Only M1/M2 conditions used at launch; the Tracker's Prey is a caster-side state, as Hunted was (conditions 18.11); the Hexer waits on Baned (9.1) | Met |
| Buff rule: no flat +1s | Cond 13 | Tokens, action changes, targeting | Met |
| Hide as a bonus action only from a class feature | Ability 7.2 | Shade Tricks (pooled; Shadowed armour also gives one per fight, a charged echo for the registry pass); Vanish (CL-12c) | Met |
| Extra Attack at 5 for martials | Ability 7.2, 10.1 | Vanguard, Shade | Met |

### C. Weapons, Dexterity and foci

| Requirement | Source | How met | Status |
|---|---|---|---|
| A precision feature for every Dexterity-main class; evidence: melee ≥ 90%, launchers reported against 75%, every launcher family ≥ 60% (M2 parity set), ≤ 110%, empty trinket slots, trinkets ≤ +10% | Weapons 6.4 (agreed); trinkets 11 rule 6 | Opening (four triggers, once per round); launcher applicability stated; solo and party contexts; reactions modelled | Stage 2 (CL-8) |
| The launcher Precision gate waits on these features | Weapons 6.5 | The Shade's launcher statement; Tracker later | Stage 2 |
| Exploit is core; do not duplicate it | Weapons 6.3 | Opening's triggers differ; parity proven with both; Exploit unchanged, granted attacks included | Met |
| Classes own depth, never access | Weapons 3, 7.3 | Techniques empower verbs; Weaponmaster keyed to weapon verb ids, group tags as `requires` filters (VG-2) | Met |
| Training lists; exotic only from a feature or offer; `weaponAttack: true` | Weapons 10.2; 7.2 rule 8 | Chassis; training filters resolved to family id lists at build time (12.1); stage 2 data | Stage 2 |
| Grappling belongs to the class designs | Weapons 17.11 | Opening counts Grappled; grapple talents at stage 2 | Stage 2 |
| The M1 slice class decides whether the staff ships in M1 | Weapons 26 | Vanguard: no staff in M1 | Met |
| Foci never required | Weapons 14 | Stated for both casters | Met |

### D. Armour and AC

| Requirement | Source | How met | Status |
|---|---|---|---|
| `armourTrainingCeiling` and `shieldTraining` per class; heavy only from a heavy class or a capstone-tier feature | Armour 7.2, 22 | Heavy / light / light / medium; shields V and H | Met |
| A ceiling below light ships a defence within 3 of the reference at levels 1, 5 and 10 | Armour 11 item 2 | Not triggered at launch (Arcanist light, agreed Q5) | Met |
| One reaction AC bonus per attack | Armour 10.4 | Flare (≤ +2) and Shieldwall (+2, settled Interpose) join the window; Stand Fast fills it, never adds to it | Met |
| AC envelope (35% / 15% / 10%) | Armour 11 item 1 | Heavy plus shield sits at the line; no class AC above armour | Stage 2 |

### E. HP, healing and defence

| Requirement | Source | How met | Status |
|---|---|---|---|
| HP tier per class | DH 7.1 | 8 / 7 / 6 / 7 | Met |
| Only one fighter-type full-Recovery Second Wind | DH 12.1; ability 6.6 | Vanguard only; dips cannot copy it (7.7) | Met |
| Exactly one `restDefensiveTool`, a heal of ≤ half Recovery or an approved kind | DH 12.1, 21.2 | Four separate tagged abilities, all heals, each once per rest (twice from level 6; 8.2); no `restDefensiveKinds` additions at launch | Met |
| `Floor1BossSustain` per class at the lowest legal Con | DH 21.1 step 6 | Every tool heals; `restToolHeal` = heal × the per-rest sub-limit (8.2); Medicine on four lists; the Herald run with and without a heal card | Stage 2 |
| Heals: exactly the target's Recovery; tiers add targets; `rest` / `camp` only | DH 12.2 | Mending Word card | Met |
| At most one healing-lean class; healer gap ≤ 10 points; no-healer arm at least a quarter of runs | DH 18.1 (agreed Q10); DH 18.2 (agreed Q11) | Herald, cards only; the DH 18.1 gate as defined; Herald arms and healer count as reports beside it | Met (CL-9) |
| `liftsDowned` reserved | DH 11.5 | None proposed | Met |
| Temporary HP and Death-warded are Later | DH 14, 15 | None at launch | Met |
| Two potions in every hero kit | DH 12.4 | Starting kits at stage 2 | Stage 2 |

### F. Companions and protector tools

| Requirement | Source | How met | Status |
|---|---|---|---|
| Fixed class; kit as its own data | Main 4.2; ability 11.1 | Class ids = launch ids; kit = signature slice | Met |
| Protector tools on classes and companion kits; the AI never reads class | DH 5.8 item 8, 27; enemies 11.3 | Vanguard (Shieldwall, Bastion Square Up); a protector kit in every recruit pool, on at least two classes; taunt only via Challenged | Met |
| DEP-1 recruit with Arcana | Consumables 13.8 | Recruit-pool rule (7.8) | Companions |
| Medicine spread across recruits | DH 27 | Recruit-pool rule, plus Mechanics | Companions |
| Under 10 seconds per companion turn; at most 4 things to read | Ability 11.4 | Counting rule (7.6); M2 playtest gate | Stage 2 / playtest |
| The rival party (Later) reuses class kits | Enemies 18.1 | Class data kept reusable | Met |

### G. Items, registry and scrolls

| Requirement | Source | How met | Status |
|---|---|---|---|
| `classTags` = every launch class whose list holds the spell; every M2 scroll and wand resolves | Consumables 13.1, 19.1 | Coverage-or-cut audit tied to Slate A | Met (CL-12); audit at stage 2 |
| DEP-1: a launch non-caster with Arcana | Consumables 13.8 | Shade | Met |
| Belt +1 feature or offer (max 3) | Items 24 | Shade talent Deep Pockets (CL-13h) | Met |
| Copy registry: `effectKinds`, subject, trigger; collisions fail both | Trinkets 11 rules 1–3 | Help-at-range mode dropped (Sigil of Aid); Feint cut (Luckstone); Duck is not Dodging (pending check); Tricks pre-cleared | Met; checks at stage 2 |
| Every trinket useful to ≥ 2 launch classes; tag text needs ≥ 2 classes | Trinkets 11 rule 7, rule 26 | Shields V + H; concentration A + H; reactions all four; full audit at stage 2 | Stage 2 |
| Stillmind needs ≥ 2 concentrators | Trinkets 23 (Later) | A and H; satisfied, not a selection driver | Met |
| A teleport rule? | Consumables 24, 25 | No launch class needs one | Met (CL-12) |
| Arcana mastery's signature use | Items 24; consumables 25 | Stays with stage 2; the no-roll threshold not proposed (8.5) | Stage 2 |

### H. Skills and morale hooks

| Requirement | Source | How met | Status |
|---|---|---|---|
| Mastery only through offers; Tactician reachable | Stat 7.2; enemies 25 | Perception mastery in the Shade and Herald offer lanes; nobody copies Tactician | Met |
| Presence is always the hero's | Stat 5.4 | Herald recruits use `companionMainAbility` (Wisdom), have no presence spells and no Frighten feature, and have a tempo signature (7.8) | Met |
| Demand Surrender only on Demoralised or Routed; never bosses | Enemies 14.6 | Changed for the Dirge: range 4 with the Totem (Terms of Surrender; Loom does not stack) and Parley's bonus-action cost; targets and failure rule unchanged (Herald design 9.1) | Met (HR-19) |
| T6 for fear spells | Enemies 14.3 | Needed for the Herald | Met (CL-1) |
| A cowable group per floor | Enemies 10.4, 14.8 | Unchanged; floor 1's group lies on the main path before the boss room (bosses are always fearless) | Met (CL-10) |

## 12. Data and the core

### 12.1 Class record

**(lead's call; illustrative shape for stage 2)** A class is data, like
everything else (main 6.5). Its record names, at least:

- `id`, `mainAbility`, `suggestedArray`, `hpTier`, `trainedSave`;
  `companionMainAbility` where the main ability is Presence (7.8);
- `armourTrainingCeiling`, `shieldTraining`;
  `weaponTraining: {simple, martial}`, each `all`, `none` or a filter
  expression over chassis fields and group tags (for example
  `light || finesse || group:launcher`, `hands == 1`), resolved to a
  family id list at build time and listed in the stage-2 data (weapons
  10.2 keys training by family);
- `skillList`, `skillCount`;
- pools with refill tags, abilities with costs, at-wills; each limited
  ability names its pool, and a `restDefensiveTool` also names its
  per-rest sub-limit pool (8.2);
- exactly one ability tagged `restDefensiveTool`;
- creation picks with a recommended default each;
- the ladder (talent menus, spell list with `drawWeight` per spell,
  subclass list with level 3, capstone);
- `offerLanes: [{tag, weightPermille}]` (2–3 lanes) and `offerExcludes:
  [offerId or tag]` (7.5);
- `triggerMenu: {abilityId: [triggerId…]}`, the named extra triggers a
  trigger offer or option may grant (7.5); trigger ids are listed per
  class in stage 2 (VG-11; Vanguard design 7.11);
- `rankSchedule` on the class (a validator input, not read at runtime)
  and `rankCategory` on option records (VG-11; Vanguard design 9.1);
- `effectKinds[]`, subject and trigger on every feature (trinkets 11
  rule 1);
- `leans[]`: records {`utilityId`, `weight` (primary, secondary or
  ribbon), `source` (class or subclass id)}, read by the reliance reports
  and the spread rule (6.2);
- `level1MechanicIds[]` on the class and `mechanicIds[]` on each
  subclass (7.3); `offerLaneAdjust` on each subclass, a weight
  multiplier on one of the class's `offerLanes` (VG-14; Vanguard design
  7.11);
- `fallbackDamageTypes[]` (6.5);
- `signatureEvents[]`: the events that count as the signature firing
  (3.4; 5.6; the Herald's, with "a resolve pip spent to the Herald's
  fear or Dazed effect" as its own row: change HR-4, Herald design 7.1),
  and `seedTarget`, a predicate id for `SeedHasTarget` (for
  example `cowableGroup`, `enemyNotYetActed`, `twoEnemiesWithinRadius1`,
  `anyEnemyInMelee`);
- dip-guard fields (7.7): `signatureVerb: [abilityId or poolId]`;
  `chassisOnly: true` on the `restDefensiveTool`, armour, save, skill
  count and kit; `minClassLevel` on heal cards and the capstone;
  `scalesBy: classLevel` on pools and dice;
- `complexity` (simple / medium / complex), shown at creation;
- **(change ARC-13)** creation and cast defaults as data, for the
  Arcanist: `castDefaults`, `defaultRotation` (rules over a closed
  predicate vocabulary), `pairDefaults`, `recommendedPickOrder`,
  `creationWarnings[]`, and `badge` on spell records (Arcanist design
  5.2, 14.1).

Also new: `utilities.json`, one record per 6.4 row, {`id`,
`baselineRoutes[]`}, each route an action, offer skill, verb, item or
companion-kit id, read by `LeanWithoutBaseline`.

### 12.2 Content validation

**(change SV-20)** This suite is registered as `content.*` rows with the
VG, SH, ARC and HR codes (seeds and validation design 5.2); its build
(`s`) checks fail the build, and `SeedHasTarget` acts by its failure
class (5.6).

**(lead's call)** The build fails when:

- `ForbiddenClassGrant`: a class, subclass, companion kit or species
  record grants anything on the list in 6.3. Enumerated checks: the
  `effectKinds` for a second reaction, teleport, speed change (not for
  species), rest or supply generation, loot or gold, potion or tonic
  values, party stealth, AI targeting (with the enumerated False Trail
  `lastKnown` exemption, SH-20, 6.3), extra units, a created ally
  action and `liftsDowned`; any condition id outside the M1/M2 budget;
  any pool or ability that raises Rally above once per ally per fight;
  any refill-tag override (a `rest` refill outside a rest; a cost
  waiver, such as a free effect, is not a refill, VG-11); and any
  camp-spend override;
- `LeanWithoutBaseline`: a utility in a class's `leans[]` has an empty
  `baselineRoutes[]` in `utilities.json` (12.1);
- `ClassGatedContent`: a dungeon, encounter or reward record names a
  class id or class-only ability as a requirement;
- `NameCollision` (10); run by `V7.NameRegistry`, which this document
  and the class docs feed (change SV-20; seeds and validation design
  5.2);
- `LeanSpread`: among the classes unlocked by default, a utility is the
  primary lean of two classes, or is leaned on by more than two classes,
  each counted with its subclasses (6.2); re-run per unlock tier as a
  flag to the lead;
- `SubclassCopiesBaseLean`: a subclass's `mechanicIds` share an id with
  its class's `level1MechanicIds` (7.3);
- `SubclassAddsPool`: a subclass adds a fourth named martial pool or a new
  button where a trigger would do (7.3);
- `SeedHasTarget` (content check on the floor-1 generator): on some
  floor-1 seed of the validation corpus, fewer than N *(illustr. 2)*
  main-path encounters satisfy a class's `seedTarget` (5.6); a
  generation check executed in V12g at AP2, not a build check (change
  SV-20; seeds and validation design 5.2, 5.4);
- the existing rules apply as written: `MissingRestDefensiveTool`,
  `HealPerTargetAboveRecovery` (DH 21.2), the cap of eight counted per
  CL-2 as distinct pool ids reachable from the class table plus the
  offers that create pools (ability 14.5), the copy registry (trinkets
  11).
- **(change VG-11)** Stage-2 validators are data predicates, added per
  class: for the Vanguard `RankAddsNoDice`, `RankKeepsBounds`,
  `HewCeiling` (a static formula), `TechniquePickerSize` (over every
  reachable option set), `CampOptionBound` (`SurgeOptionBound` renamed
  and generalised by SH-5: `actionsGranted` ≤ the base option's, with
  the reference encounter; Shade design 7.5; scoped by ARC-2, below), `TurnAllowanceNoDamage` (with its
  allow-list) and `FlagNotCondition` (with the condition equivalence
  table) (Vanguard design 6.4 rule 8, 7.3, 7.9, 9.2).
- **(change ARC-2)** For slot pools, **`SpellTierBound`** (≤ 2000‰
  single target, ≤ 1250‰ per target for areas, a stated departure from
  enemies 6.2's 120%; its hard clause covers unconditional hard control
  only, SP-1); for slotless casts (Overchannel), **`SlotlessCastBound`**;
  and **`PayoffBound`** (one setup step per target, save penalties
  included; ALM-3). `CampOptionBound` is scoped to non-slot, non-slotless
  camp pools (Arcanist design 11.2, 14.2).
- **(change ARC-22)** SP-1 as above; SP-2, upcasts checked at the
  higher tier's arrival level, with `AreaUpcastSkipsFlatTier`; SP-5,
  each target of a save area is its own hit for the 3-square sum; SP-7,
  `VariantIsScroll` over the compared field set (Arcanist design 8.1).
- **(change ARC-27)** `ArcanistDamageTypes`: every damage part on an
  Arcanist class record is fire, cold, lightning, acid or physical. The
  runtime invariant `KnownVariantInPair` (asserted in play and property
  tested, not a content check) keeps every known variant in the pair;
  gear shifts are the only exempt path (Arcanist design 7.3).
- **(change HR-27)** For the Herald: `HeraldDamageTypes`,
  `RoutOnlyBySubstitution`, `NoClassDemoraliseTrigger`, `ScatterTargets`,
  `NoHeraldRallied`, `SpurredSources`, `AllyUpliftBound`,
  `PresenceAreaSide`, `LoomHostileOnly`, `OneCadenceFrightenPerTurn`,
  `NoHealInDefaults`, `NoHealTrigger`, `HealGrowsOnlyByTierOrHearth`,
  `NoHealOnHeraldKit`, `HeraldHealSources`, `HearthNoHeal`,
  `HearthCureList`, `SideBySideTarget`, `HearthkeeperClassCastOnly`,
  `HealUpcastCapDeclared`, `HeraldStarterHasAnswer`,
  `HeraldL2PlaysAlone`, `DrawSatisfiable` (s1–s4), `MournersWithinUse`,
  `DirgeNoDemoralise`, `SurrenderTargetsLegal`, `YieldTargetsOnlyBroken`
  and `Floor1BossMindReachable` (HR-21), plus the Arcanist and Vanguard
  checks that apply. `HealUpcastByClassSlots` is a runtime rule at M4,
  not a content check (Herald design 14.2).

### 12.3 Simulator reports

**(agreed, Q2 and Q3 for the first two bullets; definitions and the rest
lead's call)**

- `Floor1ClassSpread` (≤ 10 points, client number; a gate on the M2
  release, 5.6, confirmed by the client 2026-11-18; result class REL,
  change SV-23) inside every split,
  with the per-class death share and the headroom measures; the
  per-round signature firing in each class's floor-1 boss fight,
  boss-only rounds separate (a report).
- Reliance reports per leaned utility (6.6; ≤ 10 points, reports).
- Healing: the DH 18.1 gate as defined there; beside it, as reports, the
  Herald arms (no Herald / Herald without heal cards / Herald with heal
  cards), the healer count (0 / 1 / 2+) and the heal-source breakdown
  (CL-9). **(change HR-26; told plainly, accepted with Herald Q1)** The
  reports gain the healer classification columns, the policy-forced
  arms per subclass, the "Hearth, no card" row, the 2+ row with its
  escalation, source rows (Hearth, scroll, get-ups by route),
  `MoraleValueReport`, `AllyTempoValueReport`, heal telemetry and the
  trap-room report (Herald design 5.4, 14.3).
- **(agreed 2026-11-20, Herald Q1; change HR-38)** **The heal-isolation
  gate:** per M2 Herald subclass, win rate with heal cards − without ≤
  10 points (lead's target 5), an M2 release gate on the reference arm
  (5.6); Warcaller and Pennant rows join when they ship (M3). **The
  Herald reliance and desirability reports** are required reports with
  escalation thresholds. The DH 18.1 gate is unchanged (Herald design
  5.4).
- Camp share per class at levels 1, 5 and 10 (ability 6.3).
- Precision parity, solo and in the reference party, with reactions and
  Opening's Answer trigger modelled (CL-8); Exploit uptime by party
  composition.
- The capstone nova bound: capstone turn damage ≤ 2 × the 95th-percentile
  non-capstone turn damage of the same unit, over the same seed set, at
  the same level, with one camp spend *(illustr.)*; reported with Surge
  Arts included and without them (VG-11). Tuning inputs such as
  `collisionRate` sit in the tuning file (VG-11; Vanguard design 6.4
  rule 8).
- Power after level 1 (about 60%; the formula in 5.6).
- Offer-lane variety: over 100 simulated runs of each class × subclass,
  the median pairwise Jaccard distance of offer and talent picks is ≥ 0.5
  *(illustr.)*.
- The Arcanist's friendly-fire share and Weft spend split (8.5).
- **(change ARC-29)** `SkillExpressionReport` for the Arcanist: four
  arms (novice, basic, reference, expert), each with a modifiers-off
  run, its targets and its dominant-combo and trap-card flags. The
  novice floor-1 row is required, and a class-mean novice row more than
  8 points behind the basic row escalates through the lever order
  **(agreed 2026-11-20, Arcanist Q1, for the row's status)**. **No new
  gate:** 5.6 is unchanged, and the reference arm stays the one
  `Floor1ClassSpread` gates (Arcanist design 5.4).
- Creation pick rate per class (playtest and telemetry, 15).
- `Floor1BossSustain` per class at the reference and the lowest legal Con
  (DH 21.1 step 6), the Herald with and without a heal card. It is an
  arithmetic BUILD content check, not a simulator row (change SV-23;
  seeds and validation design 5.2, 6.3).

## 13. Staging

**(lead's call)**

**(change SV-29, read; M1–M3)** The wave sign-off checklists (seeds and
validation design 7) read this table unchanged: M1 asks for the CL M1
checks and `Floor1BossSustain` (Vanguard, BUILD); M2 for
`Floor1ClassSpread` ≤ 10 in every split, `HealerGap` and
`HealIsolation` as REL rows; M3 for `Floor1ClassSpread` per Nightmare
tier and heal isolation for Warcaller and Pennant.

| Wave | Content |
|---|---|
| **M1** | The Vanguard, levels 1–3: Second Wind, Techniques (Press, Hew 1d4, Empower with the M1 lines; no riders in M1), Surge, Momentum (agreed 2026-11-18, Vanguard Q2; VG-6), Hold the Line, the Weaponmaster; the M1 level-2 talent menu (8.3); M1 conditions only; the class record shape; `restDefensiveTool`; `ForbiddenClassGrant`, `NameCollision`, `ClassGatedContent`; `Floor1BossSustain` for the Vanguard |
| **M2** | All four classes, levels 1–10, two subclasses each (the Vanguard three; the Shade's Cutthroat and the Swordsworn: the DEP-S1 fallback is applied (agreed 2026-11-18, Shade Q2; SH-30; Shade design 14; applied 2026-11-21, change TL-37; traps and locks design 6.16)); the trap record fields the Trapwright needs ship now, with `TrapwrightHasMechanism` and `SpringContentBound` as M2 reports, not gates (change TL-37); spell lists and the `classTags` audit; Shieldwall; offers with lanes and the trigger kind (CL-3); companion kits as signature slices and the recruit-pool rules; T6 for fear spells (CL-1); the remaining checks and every report in 12.3 |
| **M3** | The unlock mechanism with the first candidates in the order of 9.4 (Votary first); the first Shade unlock subclass, a full subclass: the Trapwright (the DEP-S1 fallback applied; SH-30; change TL-37; traps and locks design 6.16), with `TrapwrightHasMechanism` and `SpringContentBound` now gates; unlock aspects (Elementalist, Loremaster, Warcaller, Pennant) as content allows |
| **M4** | Multiclassing, model chosen then (7.7), with the separate multiclass precision report (CL-8); species on baseline axes (7.9; CL-11); more unlocks |
| **Later** | Classes whose identity is a Later condition (Holdfast, Berserker, Hexer) after a budget ruling; Riposting; the Death-Ward-shaped spell; summons |

## 14. Stage 2: the full class tables

Builders and the lead produce, per launch class:

- the table for levels 1–10 on the published ladder;
- pool sizes, Opening dice bands, signature steps;
- talent menus (3 options each, at least one that upgrades rather than
  adds; every level-7 menu with a trigger option; the M1 menu with M1
  conditions only);
- the `triggerMenu` per ability;
- subclass features (the level-6 one changes a verb);
- the capstone. **Acceptance criterion (lead's call):** each capstone
  changes how the class's primary lean works, inside the nova bound and
  the forbidden grants (no refills, no second reaction, no second camp
  spend);
- the spell list per caster;
- starting kits (weapons, armour, belt, coin, 2 potions); **(change
  HR-36; told plainly)** the Herald's kits carry a longsword (Parry), not
  the draft's mace, so no Topple; on solo floor 1 its escort answer is
  Shove or the shield bash's Push at Str 0 (Herald design 7.11, 7.16);
- `effectKinds`, subject and trigger on every feature;
- offer lanes with `requires` tags;
- default reaction policies;
- recommended defaults for every creation choice and the ladder preview.

Also owed at stage 2:

- **The `classTags` audit:** map every M2 scroll and wand spell in
  consumables 16 to the Arcanist or Herald list, or move it to Later
  (CL-12a).
- **Registry pass:** against all trinkets, armour and shield signatures,
  weapon verb signatures (VG-18), species (CL-11) and consumables
  (consumables 6.4). Class features never match armour or shield
  signatures (trinkets 11 rule 1); payable lines prefer original
  effects; echoes of verb signatures (Warding Staff) need a ruling
  (VG-18). Named first: Vanish
  against Veiled (Smoke Cloak), Shadowed and Duskhem; Tricks' bonus-action
  Hide against Shadowed (a charged echo, weapons 18.1, trinkets 11 rule
  2); the Alembic's priming against the Water and Oil Flasks; Stand Fast
  against the reaction-AC window and the armour 11 item 1 envelope, and
  against Raise, Guarding, Bulwark Stance and Warding Lining, with its
  "every roll" fill, shield requirement, solo clause and subclass riders
  (VG-15; Vanguard design 7.7, 7.13).
- **Composite-lockout test:** Hold the Line (CL-6) with Challenged,
  covering every movement source after the stop: a Dash for an action, a
  bonus-action Dash (Tricks, monster kits, Surge-granted), the Spurred
  free move, slip past, and forced movement by allies. Each must behave
  as 8.3 states.
- **Naming pass:** a separate client item; the names here are frozen
  placeholders.
- **SRD check:** every SRD-shaped mechanism against SRD 5.2.1 before any
  record is written.

**Simulator evidence owed:** every report in 12.3.

**Playtests:** companion-turn time; level-up time; and the claim "I can
tell the classes apart in the first fight".

## 15. Risks

| Risk | Mitigation, report and lever |
|---|---|
| ~~The T6 change is refused~~ (closed: CL-1 approved and applied 2026-11-18) | Recorded only. Had it been refused, the Herald's routs would run on T5 only; fallback Cadence Frighten as the whole rout engine |
| **Floor-1 spread:** the Vanguard (full Second Wind, sturdy 8, AC 18) is structurally favoured; the Herald depends on the roster's non-fearless share and the escort | `Floor1ClassSpread` inside every split, with the headroom measures; CL-10; a creation pick-rate measure per class (playtest). Levers are any class's data, the Vanguard's own included (Techniques size, Surge timing, shield AC through starting kits); only the existence of the one full-Recovery Second Wind is protected. Precedence (change SV-26, read; M2): a PR1 gate; spread first, DH F1 levers only if the spread still passes; re-read after every ARC lever; one lever at a time, its blast radius rerun (seeds and validation design 6.9) |
| **The Shade's solo rounds 3 and later** | The Answer trigger (8.4) and the Vanish carry (Hidden for Opening; SH-2); the per-round breakdown, boss-only rounds separate, run before stage 2 sets the Tricks size; then stage 2 raises Tricks |
| **Camp share for the martials** | Both may miss 40–60% after the fixes. The pool cap stops new pools, so the lever is weighting capstone and subclass value toward camp |
| **The healer gap** | The DH 18.1 gate, with the Herald arms and healer count beside it (CL-9), and the heal-isolation gate (HR-38; 12.3). Lever order (DH 18.1): heal-card targets (Hearth) and heal-specific sub-limits in Herald and companion data, never the Herald's slot or Cadence pools (change HR-38; Herald design 5.4); then random potion weight for everyone. Never the guarantee |
| **Caster sheets** are the busiest | The counting rule and the spellbook row hold them at 3; spell cards add the most options per level |
| **The level 6–8 plateau** | The level-6 feature changes a verb; a trigger option on every level-7 talent menu and spell-card draw (7.4); trigger offers legal at any level (added Opening triggers from level 5, `TriggerFromFive`; SH-23, Shade design 6.6). Still the weakest-felt band |
| **Dip pressure at M4** | The guards contain it but do not remove it; front-loaded identity makes every signature verb attractive alone |
| **The registry** | Duck, Vanish, Tricks' Hide, priming, Stand Fast, Spurred grants and the Totem-or-shield kits need clean results; late collisions force renames or reshapes after stage-2 data exists |
| **Condition budget** | No launch class uses a Later condition; unlocks with a "natural" kit (Berserker, Holdfast, the Hexer's Baned) wait for a ruling |
| **Hold the Line too weak or too strong** (new meaning, 8.3) | Reported through the protection reliance report and the composite-lockout test (stage 2); the lever is the stance's trigger (hit only), never a speed change |
| **Placeholder names** colour how the concepts feel | The naming pass |
| **Recall-based survey and SRD shapes** | Check before quoting and before records (4.1; SRD check) |

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: pool sizes, dice bands, Flare's AC (at most
  +2), the Harrowing Word die, Vanish's size, the report thresholds of
  12.3 marked *(illustr.)* (the per-round "about half", the nova bound,
  the variety distance, the split bands, `SeedHasTarget`'s N, the 60%
  formula's permille bounds). Shieldwall's +2 is the settled Interpose
  value, not tuning.
- Talent menus, spell lists, subclass features and capstone texts, inside
  the cards and rules here (stage 2).
- Starting kits within DH 12.4 (2 potions) and the trainings here.
- Offer lane tags and their weights.
- Level-up card, creation screen and ladder preview layout.
- **Not tuning:** the launch four and the Herald's healing lean (Q1); the
  hybrid and its four guards (Q2); the 60% target itself (only the lead
  moves it inside 55–65%, on report evidence, 5.6); the rule, the
  forbidden grants and reports not gates (Q3); level 9 option B (Q4); the
  Arcanist's light ceiling (Q5); the 10-point numbers (client numbers);
  the subclass at level 3; one full-Recovery Second Wind; heals at
  Recovery per target; the dip guards; the save principle; the spread
  rule.

## 17. Answers to the needs lists

| Need | Source | Answer | Section |
|---|---|---|---|
| Which four classes ship first | Main 10 | Vanguard, Shade, Arcanist, Herald **(agreed, Q1)**; feature lists at stage 2 | 7.1, 8 |
| Which class is the M1 slice class | Ability 20 | The Vanguard | 7.2 |
| The four launch classes' kits, within the rules | Ability 18 | Concept cards here; tables at stage 2 | 8, 14 |
| What counts as one "thing to read" | Ability 18 | The counting rule | 7.6 |
| Whether the staff ships in M1 | Weapons 26 | No: the slice class has no slots | 7.2 |
| Precision feature with evidence; training lists; Technique option; subclasses keyed to verbs or groups | Weapons 26 | Opening; chassis tables; Techniques; Weaponmaster | 8.3, 8.4 |
| Trainings, ceilings, caster-defence feature; capstone heavy training | Armour 22 | Heavy / light / light / medium; no ceiling below light; heavy only from the Vanguard or a capstone-tier feature | 8, 11.D |
| `effectKinds` for the registry; the four classes for the two-class rule; two concentrators | Trinkets 23 | Declared on every feature; Slate A; Arcanist and Herald | 11.G, 12.1 |
| The four classes and spell lists; DEP-1; a teleport rule | Consumables 24 | Slate A, lists at stage 2 with the `classTags` audit; Shade with Arcana; no teleport rule needed | 8, 11.G |
| Belt +1; capacity offers; `classTags`; Arcana mastery's signature use | Items 24 | Deep Pockets; capacity offers unchanged; the audit; Arcana mastery at stage 2 | 8.4, 8.5 |
| Perception mastery offered so Tactician is reachable | Enemies 25 | In the Shade and Herald offer lanes, and universal | 11.H |
| HP tier; `restDefensiveTool`; heals at Recovery; which class may lean on healing; any `liftsDowned` | DH 27 | 8 / 7 / 6 / 7; four heals; Mending Word; the Herald, cards only; none | 8, 11.E |

## 18. Changes to approved documents

Applied on 2026-11-18, when the client approved this document. Each
change names the exact section it touches. **Agreed, decided and settled
rows are flagged**; the client's Q1–Q5 answers approved the ones that
name a question, and the rest were confirmed with the document.

**Narrowings recorded inside this design (no change id, no approved text
changes):** the "lean, never require" rule; the forbidden grants;
`ForbiddenClassGrant`, `LeanWithoutBaseline`, `ClassGatedContent`,
`NameCollision`, `LeanSpread`, `SeedHasTarget`; Opening once per round;
granted attacks (`grantedBy`) fire no class riders, while Exploit stays
as weapons 6.3 states; heals only on cards; each rest tool once per rest
(twice from level 6); the stance a creation pick, re-pickable once per
camp and never in combat (no longer permanent: agreed 2026-11-18,
Vanguard Q3; VG-17); the element
pair fixed for the run; the save principle; the dip guards; the
recruit-pool rules and `companionMainAbility` (requirements on the
companions design). Species speed is untouched: stat model 6's Speed row
stands as written.

**Narrowings recorded inside the Vanguard design (VG-1; no approved
text changes):** condition options never fire on reaction attacks;
Square Up own turn only; one free effect per hit; the Hew-use cap; Twin
Verb's verb list; Brink's chosen distance is Wrecker-only; Earthshaker 3
targets; Square Up without a hit only from Called Out and Step In
(Vanguard design 17).

**Narrowings recorded inside the Shade design (SH-23; no approved text
changes):** Opening is never applied by default on reaction attacks
(except Counterstroke, at the open tier), and reaction hits never consume
a carry; condition-applying Tricks options never fire on reaction
attacks; Tainted Blade trades stealth-tier dice only; Throat-Cut out of
combat and silent-only; Make an Example and Tripline immunity after a
save; Jury-Rig re-arm once per trap per floor; Drag Line gives
disadvantage on the hazard-edge save, with no auto-entry (conditions
10.1 and 10.2 unchanged); Lone Blade a swap, not an addition; False Trail
only for a group from whose every watching member you are Hidden (Shade
design 17).

**Narrowings recorded inside the Arcanist design (no approved text
changes; Arcanist design 18):** priming stays once per round;
Overchannel stays tier ≤ 3 with one free Sculpt; the pair is never
re-chosen, and the camp swap is one spell inside the pair; Shocked stays
Later, and the arc is the lightning rider; Binder stops gain reliability
only, never duration; Afterhold never applies to Dazed, other turn-part
majors or hard instances; the ignore-resistance offer is excluded while
the Elementalist owns Purity.

**Narrowings recorded inside the Herald design (no approved text
changes; Herald design 18):** Rally stays once per ally per fight, and
no Herald record grants Rallied; Rekindle stays self-only, and Mending
Word never targets Downed; healing grows only by a heal card's tier or
the Hearth's one widening; the Dirge adds no Demoralise trigger (T1–T7
fixed); Scatter never routs a boss or a fearless unit; "Banner" stays
rejected (Pennant).

**Main design (2026-11-15):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-13a | 10, bullet "Which four classes ship first, and their feature lists" | Add a pointer paragraph: "The four launch classes (Vanguard, Shade, Arcanist, Herald), class structure and concepts are settled in `2026-11-17-classes-concepts-design.md`; the feature lists are its stage 2." The bullet becomes "The four launch classes' feature lists (classes design, stage 2)" | **Client (agreed 2026-11-17, Q1)** for the four; pointer: Lead |

**Ability model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-2 | 10.2, the cap-of-eight bullet; 14.5, the row "a class's limited abilities … can exceed eight" | Add: "A limited ability is one named pool: every ability drawing on that pool is an option of it, and a spell tier's slot pool counts as one, with its known spells as options; an upcast is an option of the higher pool. An offer or talent that adds an ability to an existing pool adds an option, not a limited ability. A rest tool's per-rest sub-limit is not counted (classes design 3.4, 7.5, 8.2)." The 14.5 row counts the distinct pool ids reachable from the class table plus the offers that create pools | Lead |
| CL-3 | 10.2, offer kinds list; the "Past that, offers only upgrade existing ones" sentence | Add: "trigger: an ability gains one named extra trigger from its class's `triggerMenu`, never repeated; legal at any level (classes design 7.5)". Past the cap, trigger offers stay in the draw beside upgrades | Lead |
| CL-4 | 10.1, Floor 4 row ("capstone at level 9 (6.7)") | Becomes "capstone at level 9 (6.7), plus a pick-one-of-three offer". 6.7 unchanged | **Client (agreed 2026-11-17, Q4)** |
| CL-13b | 18, "The four launch classes' kits" and "What counts as one thing to read"; 20, "Content volume" bullet ("which class is the M1 slice class") | Point to the classes design: kits at its stage 2; the counting rule in its 7.6; the M1 slice class is the Vanguard | Lead |

**Stat model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-12c | 7.1, Stealth row ("Hide to break line of sight") | Add: "A class feature may let Hide succeed without breaking line of sight (classes design 8.4, Vanish); the Stealth roll and Hidden's end rules (conditions 18.7) stay." Hidden's sources (conditions 18.7) are unchanged: Vanish is a Hide | Lead |

**Conditions design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-5 | 18.4, Spurred, "Sources" line | Add "class features, companion kits of the Herald class and presence spells (classes design 7.8, 8.6)". Rally's option and "Rallied or Spurred, never both" unchanged | Lead; the Rally source stays **(settled)** |

**Enemies design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-1 | 14.3, T6 row, "M2 (scrolls), Later (spells)"; 21, Later bullet "fear spells (T6)" | T6 becomes "M2 (scrolls and class fear spells)"; remove "fear spells (T6)" from the Later bullet | Lead (C-12 lineage) |
| CL-10 | 14.8, "Guarantee" paragraph; 10.4, floor guarantees, the M2 cowable bullet | Add: "Floor 1's guaranteed cowable group lies on the main path before the boss room (classes design 8.6)." (Bosses are always `fearless`, 14.2, so no condition is needed.) | Lead |
| CL-13c | 25, "Classes and offers" bullet | Point to the classes design: Perception mastery sits in the Shade and Herald offer lanes and the universal draw | Lead |

**Weapons design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-6 | 8, "Movement and reactions" bullets | Add: "A class reaction may set the mover's remaining movement for this turn to 0; its speed is unchanged and it keeps its action and bonus action. Movement granted afterwards by Dash (whatever its action cost) or by Spurred is added as normal; forced movement is unaffected (classes design 8.3, Hold the Line)." | Lead |
| CL-8 | 6.4, the evidence paragraph after the targets table **(agreed requirement)** | Parity runs model reaction attacks, Opening's Answer trigger and both contexts (solo floor 1, reference party); precision dice scale by class level; the evidence is single-class; multiclass Dexterity builds get a separate M4 report against ≤ 110% | The **agreed** requirement is only narrowed: Lead; confirmed with the document |
| CL-13d | 26, "Classes and offers" bullet | Point to the classes design; "the M1 slice class, which decides whether the staff ships in M1" answered: the Vanguard, no staff in M1 | Lead |

**Armour design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-7 | 14.5, Guarding row ("illegal … on a carrier that already has Interpose or a Guard verb") | "has Interpose" reads a **learned** class Interpose (Shieldwall, learned exactly when it is the Vanguard's stance pick), whether or not it is used | Lead |
| CL-13e | 22, "Classes and offers" bullet | Point to the classes design: ceilings heavy / light / light / medium; no class below light at launch, so armour 11 item 2 is not triggered | **Client (agreed 2026-11-17, Q5)** |

**Trinkets design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-11 | 11, rule 1 (class copy registry) | The registry covers species traits and armour and shield signature properties (for example Bulwark Stance) in every direction | Lead |
| CL-13f | 23, "Classes and offers" bullet | Point to the classes design: the four launch classes; Arcanist and Herald concentrate | Lead |

**Consumables design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-12a | 13.1, `classTags` bullet ("Until the classes design lands, catalogue tags are TBD"); 16, header paragraph | `classTags` resolve to the launch ids `vanguard`, `shade`, `arcanist`, `herald` *(ids illustr.)*; the coverage-or-cut audit maps every M2 scroll and wand to the Arcanist or Herald list or moves it to Later (classes design 14); [primal] stays an authoring hint until the Rootwarden | Lead |
| CL-12b | 24, "Classes" bullet; 24, "Ability model: a teleport rule" bullet | "No launch class needs a teleport rule (classes design 11.G)"; teleport scrolls stay Later | Lead |

**Items design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-13h | 24, "Classes and offers" bullet | Point to the classes design: belt +1 is the Shade's Deep Pockets talent; capacity offers are unchanged; `classTags` resolve through CL-12a; Arcana mastery's signature use goes to its stage 2 | Lead |

**Death and healing design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CL-9 | 18.1, after "Levers if it fails"; 21.5, the "Win rate with and without a healer" bullet | Add, as reports beside the gate: three Herald arms (no Herald / Herald without heal cards / Herald with heal cards), a healer-count breakdown (0 / 1 / 2+) and a heal-source breakdown (Herald hero card, Herald recruit, heal-kit recruit). The gate itself stays exactly as 18.1 defines it, and 18.2's "at least a quarter" reads its no-healer arm | The **agreed** gate (Q10) is unchanged; reports only: Lead |
| CL-13g | 27, "Classes" bullet | Point to the classes design: tiers 8 / 7 / 6 / 7; four half- or full-Recovery heal tools; the Herald the one healing lean; no `liftsDowned` proposal | **Client (agreed 2026-11-17, Q1)** |

**Not changed:** `restDefensiveKinds` (all four tools heal); the subclass
level (ability 10.1); ability 6.7 (Q4, option B); DH 21.1 step 6; main
4.5 ("a choice every level" holds); stat 5.6 (met as a target); every
cap in 6.4's cap column.

**Check after applying:** a grep for "Later (spells)" in the enemies
design finds nothing; a grep for "at most eight limited" shows the
named-pool clause; a grep for "Spurred" in the conditions design shows the
class source; a grep for "teleport" in consumables 24 shows the answer;
a grep for "TBD" in consumables 13.1 and 16 finds only the audit's Later
moves; a grep for "which class is the M1 slice class" finds only
history; a grep for "belt +1" in items 24 shows the Deep Pockets
pointer; a grep for "Hide to break line of sight" in stat model 7.1
shows the Vanish clause.

## 19. Depends on systems not yet designed

- **Classes, stage 2:** the full tables and all evidence (14).
- **Companions:** kits as signature slices; one combined slot pool for
  caster kits; the recruit-pool rules (7.8): a protector kit (protector
  kits on at least two classes), an Arcana-trained non-caster, Medicine
  and Mechanics recruits, at most one ally-heal kit per floor pool;
  Herald recruits on `companionMainAbility` with a tempo signature and no
  presence spells; same-class weighting. Designed in the companions
  design (approved 2026-11-20): pool rules read per Muster board plus
  party (CMP-11; companions design 7.3).
- **Species (M4):** traits on baseline axes, in the copy registry.
- **Multiclassing (M4):** the model, inside the dip guards (7.7).
- **Dungeon generation:** `SeedHasTarget` and its `seedTarget`
  predicates; the floor-1 cowable group before the boss room (CL-10).
  CL-10 is hosted by the floor-1 Circuit (floor layout design 6.7,
  change FL-10) and change EV-14; `seedTarget` is realised by encounter
  placement design 6.17 (cross-document comparison 2026-11-23, CC-61).
  Taken up for `SeedHasTarget` by segments 11–12: executed in V12g at
  AP2, and `NameCollision` run by `V7.NameRegistry` (change SV-20;
  seeds and validation design 5.2, 5.4).
- **Profile and unlocks (M3):** the unlock order (9.4).
- **Naming pass:** every placeholder name here.
- **Lead's checks before data:** every SRD shape in this document against
  SRD 5.2.1; `NameCollision` over the final names.

## 20. Open items for the client

None. Approved by the client on 2026-11-18.

### 20.1 What the approval covered

The client approved the document as a whole on 2026-11-18. The five
questions were answered **(agreed, 2026-11-17)**:

- **Q1, launch four:** Vanguard, Shade, Arcanist, Herald, with the single
  light healing lean on the Herald's spell cards and its Hearth subclass.
- **Q2, identity timing:** the hybrid, "verb at 1, lens at 3, mastery at
  9", with its guards.
- **Q3, lean, never require:** the rule and the forbidden grants;
  reliance reports as reports at 10 points; healing keeps its gate.
- **Q4, level 9:** option B, a capstone plus a pick-one-of-three offer.
- **Q5, Arcanist armour:** light ceiling; the Spellguard glass mage
  rejected.

The approval also confirmed every lead's call in this document, the most
visible being Hold the Line's meaning (8.3; CL-6), the Arcana-mastery
reading (8.5), the corrected section targets of CL-6, CL-10, CL-12c and
CL-13, and the revision of 2026-11-18 (appendix A, R-n): the pool as the
unit of the cap of eight, one rest-tool use per rest, Opening's fourth
trigger, Vanish's carried first strike, the Stand Fast, Overchannel and
Scatter examples, and the placeholder renames. The two readings flagged
for the client's review were confirmed by the client:

- **`Floor1ClassSpread` as an M2 release gate** inside every split (5.6,
  12.3);
- **Mending Word may target the Herald itself** (8.6).

### 20.2 Future items

These are not open items for this document. Stage 2 (section 14: the
full class tables and all evidence) is the next stage of the classes
design and is taken up by the four class designs (Vanguard, Shade,
Arcanist and Herald designs; cross-document comparison 2026-11-23,
CC-9). The rest come back to the client only if
a report or a later design triggers them:

- a reliance report the lead proposes to turn into a gate;
- a failed 10-point healer gate whose levers run out;
- a missed `Floor1ClassSpread` whose levers run out (5.6);
- the naming pass;
- a `liftsDowned` effect or a `restDefensiveKinds` entry (the Votary);
- Arcana mastery as a no-roll threshold, if stage 2 proposes it
  (consumables 25);
- the multiclass model at M4;
- a glass-mage class (the Adept or a Spellguard aspect) needing armour 11
  item 2 evidence.

## Appendix A: how the critique findings were resolved

**Identity and fun critique** (ids I-n)

| # | Finding | Resolution |
|---|---|---|
| I-1 | The Herald has the weakest level-1 hook; T6 for spells is Later; four leans stacked | T6 moved (CL-1); Harrowing Word pays more against Frightened targets; heals only on cards, in Hearth; morale primary, tempo secondary (8.6) |
| I-2 | Cadence "Rally an already-Rallied ally" and the "ignore the limit" capstone | Removed; Rally unchanged; a forbidden grant (6.3) |
| I-3 | Read counts over 4; no definition of a "thing" | Counting rule (7.6); stance as a permanent creation pick, a pool-free reaction counting 0; Flare folded into Weft |
| I-4 | Ally-keyed hooks dead on solo floor 1 | Opening's self triggers; `SeedHasTarget`; per-round solo boss breakdown |
| I-5 | Companion kits copying the hero's signature | Different slices for the hero's own class; same-class recruits weighted down; Herald recruit with a tempo signature (7.8) |
| I-6 | Dead middle and late levels | Level-6 feature changes a verb; trigger offer kind (CL-3); slot tier = one ability (CL-2); cheap capstones |
| I-7 | The lanes' ladders disagree | One published ladder (7.4) and a one-sentence answer (5.1) |
| I-8 | The rule in four forms; reliance "without" arm undefined; Quiet Company | One five-line rule (6.1); "without" = no unit of that class or subclass; Quiet Company cut |
| I-9 | Subclasses copying the base lean | Elementalist an unlock aspect; Alembic primes targets, not terrain; hazards stay with the Wrecker |
| I-10 | Shoulder and Primer at will | Shove-after-push a talent (no Technique cost, once per round); priming once per round through Weft |
| I-11 | Creation load; name drift | Recommended defaults and a ladder preview; one frozen name per concept; naming pass |

**Balance critique** (ids BA-n, MR-n)

| # | Finding | Resolution |
|---|---|---|
| BA-1 | Non-healing rest tools retune floor 1 for all; Shade lacks Medicine | All tagged tools heal; Duck and Flare ordinary abilities; Medicine on the Shade's list |
| BA-2 | The healer gate cannot isolate healing; counts not seen | Heals only on cards; Herald arms and healer count as reports beside the DH 18.1 gate (CL-9); at most one heal-kit recruit per floor pool |
| BA-3 | T6 | CL-1; T5 fallback (15) |
| BA-4 | Casters break the cap of eight | CL-2; totals in 7.5 |
| BA-5 | Arcanist unarmoured AC fails armour 11 | Light ceiling (agreed, Q5) |
| BA-6 | Opening and Exploit double-dipping on reactions | Opening once per round; granted attacks fire no class riders, Exploit unchanged; reactions modelled (CL-8) |
| BA-7 | Bimodal flank tuning; nova stacking | Flank trigger replaced by "an ally hit it since your last turn"; two-context evidence; no maximised dice; capstone nova bound ≤ 2× |
| BA-8 | Vanguard composite lock; speed 0 from a stance | Challenge pooled; Hold the Line sets the mover's remaining movement for the turn to 0 without changing speed (CL-6; new meaning 8.3); composite goad-plus-move-stop test at stage 2 |
| BA-9 | At-will condition engines | Moved to pools or once per turn; Exploit uptime report |
| BA-10 | Re-Rally | As I-2 |
| BA-11 | Quiet Company | Cut |
| BA-12 | Camp share, Shade and Vanguard | Vanish camp 2; camp-weighted capstone and subclass features; evidence at levels 1, 5, 10 |
| BA-13 | Floor-1 spread; fearless boss | `Floor1ClassSpread` inside every split (bosses are always fearless, so the splits are roster share and escort cowability); CL-10 |
| BA-14 | Utilities at risk of becoming required (Mechanics, Arcana, Medicine, protection, Presence) | Mechanics on 2 lists, Medicine on 4, Arcana on 2 plus DEP-1; recruit-pool rules; protection reliance report; Herald hero and recruit reported separately |
| BA-15 | Mending Word and Cadence share the bonus action | Stated on the card and in the preview |
| BA-16 | "Perfect Ward" outside the window | Withdrawn; the Arcanist capstone is Overchannel |
| BA-17 | Dip grants (a second Second Wind, healer kit, Hide) | The rest tool and kit go with the chassis; a second class's heal cards need class level 3 in it; signature at class-level-1 strength (7.7) |
| BA-18 | Precision scaling conflict | Class level; single-class evidence; a separate M4 report (CL-8) |
| MR-1–7 | Missing requirements | `restDefensiveKinds` not needed; reactions in parity (CL-8); Herald-arm healer reports (CL-9); composite lockout test (stage 2); spread by split (5.6); recruit-pool rules (7.8); change requests logged (18) |

**Consistency critique** (ids CO-n)

| # | Finding | Resolution |
|---|---|---|
| CO-B1 | Force is not a damage type | Withdrawn; fallback types from stat 8.1 (6.5) |
| CO-B2 | Name collisions | Rename table and `NameCollision` (10) |
| CO-B3 | Registry collisions (Help at range vs Sigil of Aid; Feint vs Luckstone; Dodging) | Help mode dropped for Spurred; Feint cut; Duck halves damage, check pending |
| CO-B4 | Rally rule broken | As I-2; Rally was already a bonus action |
| CO-H1 | T6 | CL-1 |
| CO-H2 | Cap of eight and no slot for spell cards | CL-2; the caster column of the ladder |
| CO-H3 | Multi-mode tools fail the validator | Each tool a separate tagged ability on a shared pool, counted as options of that one pool (CL-2) |
| CO-H4 | Arcanist armour floor | Light ceiling (agreed, Q5) |
| CO-H5 | Level 9 vs 6.7 | Option B (agreed, Q4); CL-4 leaves 6.7 untouched |
| CO-H6 | Two choices at levels 4 and 8 | The +1 ability is the only choice there |
| CO-H7 | Opening and Exploit pay on the same trigger | "Both may fire; parity proven with both"; worst case named |
| CO-H8 | Shade camp share | As BA-12 |
| CO-M1 | Shoulder duplicates armour content | Talent Drive Through, following a Technique push |
| CO-M2 | Element switching | Pair fixed for the run; spells carry fixed families inside the pair; the camp swap stays inside the pair (8.5) |
| CO-M3 | Primer at will | Once per round through a Weft option (Alembic) |
| CO-M4 | Multi-target Challenge capstones cancel themselves | Withdrawn; the Vanguard capstone is Stand Fast |
| CO-M5 | Mending Word and the `classTags` audit | The same spell by design (consumables 17); coverage-or-cut audit (CL-12a) |
| CO-M6 | Source lists changed (Spurred, Vanish, Frighten cost offer) | CL-5; CL-12c; Herald excluded from the cost offer on Frighten |
| CO-M7 | Shieldwall vs Guarding; tower Bulwark Stance | CL-7 "learned"; the registry covers armour signatures (CL-11) |
| CO-M8 | Save reasons inconsistent | One principle (8.2) |
| CO-M9 | Shield two-class rule; Herald Totem | Herald shield allowed with a Totem-or-shield kit; V + H meet the rule; full audit at stage 2 |
| CO-M10 | Companion kit as class plus subclass | Guidance only; no subclass field; caster companion budget noted |
| CO-L1 | Scope creep | Numbers marked illustrative; unlocks kept short; tables deferred (14) |
| CO-L2 | Subclass at level 2 | Rejected (4, row 6) |
| CO-L3 | Creation pick load | One small pick with defaults; counting rule |
| CO-L4 | Main-ability coverage | Stated as a target; Votary, Tracker and Holdfast named |
| CO-L5 | Stillmind is Later | Satisfied, not a driver |

**Judges' grafts**

| Graft | Resolution |
|---|---|
| Hybrid timing with front-loaded parity and the growth lane's preview and seed check | Adopted (5.5) |
| Role roster as the base; the mechanic roster's Wrecker, priming and T6 flag; the classic roster's unlock depth, Rootwarden and Hunted-style states | Adopted (8, 9) |
| The front-loaded lane's dip guards | Adopted (7.7) |
| The growth lane's character-level precision dice | Rejected for class-level dice plus a separate M4 report (BA-18) |

**Corrections made while writing this document (new lead's calls)**

| # | Point | Resolution |
|---|---|---|
| W-1 | The proposal cited enemies 14.9 for the cowable guarantee | The guarantee is in enemies 14.8 ("Guarantee") and 10.4 (floor guarantees); CL-10 targets both |
| W-2 | The proposal cited conditions 11.2 for move-ending reactions; 11.2 covers terrain squares | Opportunity-attack timing and movement segments live in weapons 8; CL-6 targets it |
| W-3 | "Ends the mover's current move" was ambiguous: under weapons 8 a cut segment keeps its unspent movement, so the stance would do nothing | The mover's remaining movement for the turn is set to 0, speed unchanged, action kept; later Dash and Spurred movement are added as normal (8.3; CL-6) |
| W-4 | Spurred's entry is in conditions 18.4 | CL-5 targets it |
| W-5 | Guarding is a shield property in armour 14.5 | CL-7 targets it |
| W-6 | "Loremaster … a proposed signature use for Arcana mastery" mixed a subclass feature with a mastery use | Separated (8.5) |
| W-7 | The parents' pointer and dependency sections still list the classes as pending | Pointer changes CL-13a–h |
| W-8 | No content check enforced the forbidden grants or the spread rule | `ForbiddenClassGrant`, `LeanSpread`, `SubclassCopiesBaseLean`, `SubclassAddsPool` (12.2) |

**Revision of 2026-11-18** (ids R-n; a builder, a consistency and a play
check, after the client's Q1–Q5 answers; all new lead's calls)

| # | Finding | Resolution |
|---|---|---|
| R-1 | The cap-of-eight unit was inconsistent (three findings) | One named pool = one limited ability (3.4, 7.5; CL-2); totals 6 / 6 / 4 / 3; the reading limit is the real bound |
| R-2 | Rest tools on shared pools heal 2–4 times per rest | One use per rest, twice from level 6, as an uncounted sub-limit; `restToolHeal` defined (8.2) |
| R-3 | `LeanSpread` and `SubclassCopiesBaseLean` failed the launch content | Lean records, class plus subclasses as one, launch-four scope; Duck and Flare tools, not leans; mechanic-id comparison (6.2, 7.3) |
| R-4 | The per-round guard could not pass; "fires" undefined | `signatureEvents[]`; boss-only rounds separate; Opening's Answer trigger; Cadence Spurred on self; Harrowing Word on resolve pips (3.4, 5.6, 8.4, 8.6) |
| R-5 | `Floor1ClassSpread` inputs undefined; split by a fearless boss that always exists | Clear, input and splits defined; 10 points inside every split; death share and headroom reports; an M2 release gate; CL-10 unconditional (5.6) |
| R-6 | 60%, nova bound and variety not checkable | Formulas (5.6, 12.3) |
| R-7 | "Once per turn" and "once per round" mixed; Opening's timing | Terms (3.4); Opening declinable, crit-doubled, ally clause reworded (8.4) |
| R-8 | Technique timing; Drive Through and Challenge costs | Timing and policy stated; costs and per-round limits stated (8.3) |
| R-9 | Stance both pick and toggle; Bastion dead for Hold the Line | A permanent pick; a Bastion feature per stance; M1 pick of one (8.3) |
| R-10 | Hold the Line wording | Remaining movement set to 0; later Dash and Spurred added; test rows (8.3, 14; CL-6) |
| R-11 | Element pair both switchable and fixed | Fixed families, cards filtered to the pair, pair fixed for the run (8.5) |
| R-12 | Spells known, the draw and guard 4 | Subclass spell fixed; 6 or 7 known; the draw defined; guard 4 for a second class only; Mending Word on self allowed (7.4, 7.7, 8.6) |
| R-13 | Offer lanes, exclusions and trigger offers had no data | `offerLanes`, `offerExcludes`, `triggerMenu`; the draw; level-7 trigger options; a real example (7.5) |
| R-14 | Exploit removed for granted attacks without a change | Exploit unchanged; `grantedBy` read by class riders only (3.4) |
| R-15 | CL-9 redefined the agreed healer gate | The DH 18.1 gate as defined; Herald arms and counts as reports (6.6; CL-9) |
| R-16 | Herald recruits' main ability undefined | `companionMainAbility: Wisdom`; no presence spells or Frighten; CL-5 widened (7.8) |
| R-17 | Dip guards and validators had no data | Dip-guard fields; `fallbackDamageTypes`, `seedTarget`, `utilities.json`; enumerated `ForbiddenClassGrant` (12) |
| R-18 | Species speed ban amended stat model 6 | Species keep stat model 6's speed route (6.3, 7.9) |
| R-19 | Stand Fast refilled a `rest` pool; capstones off-lean | Stand Fast and Overchannel rewritten; a capstone acceptance criterion (8.3, 8.5, 14) |
| R-20 | Dirge and Rout added morale triggers | Dirge through T1–T6 only; Scatter routs by T6 substitution (8.6) |
| R-21 | M1 riders; the M1 talent menu | No riders in M1; an M1 menu of three; Low Cut at M2 (8.3, 13) |
| R-22 | Reading counts at level 9 and with offers | Pool-free reactions and glinted passives count 0; capstones on their pool's row; the offer draw drops fifth rows (7.5, 7.6) |
| R-23 | "Without" arms break the recruit-pool rules | Protector kits on two classes; waivers printed (6.6, 7.8) |
| R-24 | Smaller fixes | Shieldwall +2 settled; Flare ≤ +2; launcher family ≥ 60%; CL-12c retargeted; Shadowed and flasks in the registry pass; CL-13h; Hexer on Baned; names renamed; citations and labels corrected; the Vanguard's own levers; the Arcanist's Weft report; caster companions' one pool; weapon-training filters |

## Appendix B: sources

- Approved specs in `docs/superpowers/specs/` (the 11 parents listed in
  the header). Facts were re-checked for this document in:
  - main design 4, 9, 10;
  - stat model 5, 6 (Speed row), 7 (7.1 Stealth row), 8.1;
  - ability model 6.1–6.7 (6.2 `turn` refill), 7.2, 7.3, 10.1–10.3, 11,
    14.5, 14.6, 18, 20;
  - conditions 11.2, 18.1, 18.4 (Spurred, Hobbled), 18.7 (Hidden), 18.11;
  - enemies 10.4, 14.2, 14.3 (T6), 14.8, 14.9, 21, 25;
  - weapons 6.3, 6.4, 6.5, 7.3, 8, 26;
  - armour 7.3, 11, 14.2 (Shadowed), 14.5, 22;
  - trinkets 11, 23;
  - consumables 6.4, 13.1, 13.8, 16, 17, 24, 25;
  - items 24;
  - death and healing 4, 5.8, 12.1, 12.2, 18.1, 18.2, 21.5, 22–27;
  - name searches across all eleven documents.
- [D&D Beyond: Character Classes (2024 Basic Rules)](https://www.dndbeyond.com/sources/dnd/br-2024/character-classes)
- [SRD 5.2 Fighter (mdbook mirror)](https://mailbun.codeberg.page/dnd-srd5.2-mdbook/Book/03_Classes/05_Fighter.html)
- [Archives of Nethys: PF2e archetypes](https://2e.aonprd.com/Archetypes.aspx)
- [Paizo forum: multiclass dedications](https://paizo.com/threads/rzs6r3rg)
- [bg3.wiki: Class](https://bg3.wiki/wiki/Class); [PC Gamer on BG3 one-level dips](https://www.pcgamer.com/games/baldurs-gate/baldurs-gate-3-just-got-the-hexblade-one-of-d-and-ds-most-op-multiclass-options-and-its-still-the-best-1-level-dip-you-could-possibly-take)
- [Darkest Dungeon II heroes (wiki)](https://darkestdungeon.wiki.gg/wiki/Heroes_(Darkest_Dungeon_II)); [Wildermyth classes](https://playerassist.com/all-classes-in-wildermyth/)
- Everything else in 4.1 is recalled and unverified.
