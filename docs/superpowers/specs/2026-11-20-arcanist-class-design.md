# Classes, Stage 2: The Arcanist: Design

Date: 2026-11-20
Status: Approved, 2026-11-20
Names updated by the naming pass (naming pass design,
`2026-11-24-naming-pass-design.md`, approved 2026-11-24; NP rows
listed: NP-32, with its 13.1 cites of the agreed Q1, Q2 and Q5 renames,
and the register rows of naming pass section 5 whose sites name this
document). Internal ids, field names and change ids are unchanged.
Parents: `2026-11-17-classes-concepts-design.md` (classes stage 1,
approved; the direct parent), `2026-11-18-vanguard-class-design.md` and
`2026-11-18-shade-class-design.md` (the approved stage-2 templates;
"Vanguard design" and "Shade design" below), and through classes stage 1
`2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design),
`2026-11-17-consumables-design.md` (consumables design) and
`2026-11-17-death-healing-design.md` (death and healing design, "DH")

Items marked **(agreed)** were decided by the client on 2026-11-20 for
the three questions this design raised (Q1–Q3, sections 4 and 20), by
choosing the lead's recommendation in each case. The client was told
plainly, before answering Q1, that its prediction is right today (a
scripted novice clears solo floor 1 16–29 points behind the Vanguard in
the proxies *(illustr.)*); that the pre-ticked cast options and the
last-slot confirmation are defaults and confirmations of the player's
own action, not suggestions, under agreed ability 11.3; and how a
creation pick is defined for `Floor1ClassSpread` (pair × Mote key ×
level-3 subclass, ARC-15) together with `SetupAttribution` (ARC-16). The
client accepted all four with Q1. The client was told plainly, before
answering Q3, that the inert rule re-reads agreed stat model 8.2 and 8.3
for one subclass's own at-wills. The client also said that everything
else in the lead's proposal stands as written. With the approval request
the client was told plainly the two items in 20.1 (ARC-8, and the inert
rule's rider sentence extended to the conditions 19 cancel, override and
wash rows); the client confirmed both on 2026-11-20 when it approved
this document as a whole.

Items marked **(lead's call)** were filled in by the design lead; the
client confirmed them all on 2026-11-20, when it approved this document
as a whole, and they are binding. Items marked
**(settled)** restate a rule from an approved document; **(decided)**
marks a main-design client decision. Items marked **(change ARC-n)**,
and the subclass and aspect ids **BN-n**, **ALM-n**, **AS-1**, **LM-n**
and **EL-n**, amend an approved document and are listed, with the exact
section they touch, in section 18. Items marked **(told plainly)**
narrow or re-read agreed text and were told to the client in plain
words, before Q1 or Q3 or with the approval request (20.1); the client
confirmed them all on 2026-11-20. Every item here is binding on the
builders.

This is **stage 2 of the classes design for the Arcanist**, the launch
Intelligence full arcane caster: its full table for levels 1–10, its
signature verb (the element pair) and how it grows, its slot and Weft
pools, its core kit, its full spell list and `classTags` audit, its two
M2 subclasses (Binder, Alembic), sketches of its two unlock aspects
(Loremaster, Elementalist), the capstone, the numbers, the staging and
how every binding requirement on "the classes design" is met for this
class. The Arcanist is an **M2 class** and has no M1 slice. Classes
stage 1 section 14 lists what stage 2 owes; section 13 below answers it
for the Arcanist.

The client's brief: "Let's go for arcanist, intelligence based builds
are complex by nature, so we have to allow players to be able to get by
with some basic casting, but also allow for skill expression for players
who understand the mechanics more in depth, allowing them to carry out
interesting and creative uses of this classes kit. The skill floor of
this class will by its very nature likely be higher than the vanguard
and other simpler classes, but this is supplemented by the ability to
larger skill expression. What do you think? Feel free to contest against
this, and go ahead with the usual considerations and research." The
lead's answer, which the client accepted (Q1, **agreed**), is in section
5: **agree on complexity and ceiling; contest a higher survival floor.**

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. They come from
scratch Monte Carlo and arithmetic models, not simulator evidence; only
the gaps between proxy rows mean anything. The builders own them and the
simulator calibrates them (section 17), except where a value is marked
as a client decision, a settled value or a lead's number.

**Names are placeholders** pending the naming pass (a separate client
item). They are frozen for this document so that documents agree. Every
name new to this document was grep-checked as a whole word against the
14 approved documents and has no hits: Sculpt, Gust, Mote, Interplay,
Afterhold, Temper, Fray, Drench, Brand, Needles, Spatter, Detonation,
Streak, Undertow, Squall, Arrest, Skyfall, Gout, Pyre, Forked Bolt,
Dissolve, Deep Freeze, Cordon, Shackle, Coffle, Cinch, Fastness, Belay,
Slipped, Rebind, Reel, Catalyse, Prime, Splash, Tincture, Wide Mix,
Runoff, Reagent Surge, Dissect, Field Notes, Unpick, Weak Point,
Cross-Reference, Crux, Purity, Deepen, Unmake, Aftershock, Tempest,
Ruin, Flux, Scour, Etch, Opposed, Seal, Converge.
The list records the original check; the naming pass (approved
2026-11-24, NP-32) renamed Sculpt → Whittle (its Charge mode → Load),
Skyfall → Highfall, Gout → Deluge, Coffle → Lashed Pair, Dissect → Lay
Bare and Tempest → Weatherwork, and the text below uses the new names.

Names avoided because they collide **(lead's call)**:

| Avoided name | Why |
|---|---|
| Shape (the stage-1 Weft option) | The rod's focus verb **Shape** (weapons 17.8); forced rename to Sculpt (ARC-1) (renamed Whittle, naming pass NP-32) |
| Bend | Prose hits only, but "Sculpt" has none |
| Linger, Lingering | The conditions 5.1 duration model "Lingering"; three drafts used "Linger" for three things (now Afterhold) |
| Knot (any Binder name) | The trinket kind word (Ward-Knot, trinkets kind-word rule) |
| Hold, Held, Holdfast | A Later condition; Hold the Line (renamed Bar the Way, naming pass); a Vanguard unlock |
| Storm (as a label or template) | The Storm Ring |
| Gale | The Gale Gambeson |
| Volatile, Brittle | Existing hits |
| Detonate | Sits next to the spell template Detonation |
| Mantle, Spark | Withdrawn in stage 1 |
| Darts, Burst, Line, Beam | A dagger skin; ability 8.1 shape terms; the Mote's "two beams" |
| Exploit, Empower, Twin, Hasten | Weapons verb; weapon verb and Technique; Twin Verb; too close to Hasted |
| Attune, Scatter, Loom, Tether | The Attuned property; the Herald capstone; the Tipstaff's focus verb; a spell-list candidate |
| Overflow (the Elementalist's level-6 alternative) | Prose hits in DH; the draft's "name reserved" slot is filled by **Aftershock** (9.4) |

"Fan" shares a head word with the weapons property Fan Throw;
`NameCollision` gives a warning only, which is accepted.

**SRD check (settled principle, classes stage 1; lead's call to repeat
it here).** The SRD-shaped mechanisms used here are spell slots, an
Arcane Recovery-like Slot Renewal, Eldritch Blast-like at-will scaling,
Sculpt Spells- and Careful Spell-like selective areas, and spells shaped
on Burning Hands, Ice Knife, Chromatic Orb, Magic Missile, Thunderwave,
Entangle, Faerie Fire, Scorching Ray, Blindness, Hold Person, Gust of
Wind, Fireball, Lightning Bolt, Slow, Dispel Magic, Ice Storm, Cone of
Cold, Chain Lightning and Disintegrate. They were written from memory,
and the research for this document web-checked only parts. **Every
SRD-shaped mechanism must be checked against the SRD 5.2.1 document
before any record is written**, and every record flagged `srd` cites the
entry it was checked against. Ice Knife, Chromatic Orb and Chain
Lightning are the ones most likely to differ or be absent. Class,
subclass, option, feature and spell names are original; no D&D-only name
or text is used.

**Waves:** M1 is the vertical slice (one class, levels 1–3, solo), M2
the full run with four classes and companions, M3 profile and unlocks,
M4 breadth (species, multiclassing), Later everything after that no
milestone names. **(settled, main design 9)**

## 1. Intent

Stage 1 fixed what the Arcanist *is*: an Intelligence full arcane caster
and the Hinderer of the launch four, whose verb is the element pair
("I shape where damage goes and what it leaves behind"), with area
damage and control as primary leans, damage types and terrain
combinations as secondary, slots and Weft as its pools, Overchannel as
the illustrative capstone, and four subclasses named (Binder and Alembic
at M2, Loremaster and Elementalist as unlocks). **(settled)** It left
open how the class plays in plain hands and in expert hands, how big the
pools are, what the at-wills do in numbers, the full spell list, what
each subclass lets the player spec into, and how skill expression is
measured.

The answer in one sentence: **easy to cast, hard to master.** A plain
player gets by on a strong Mote bolt that works alone, two automatic
defensive Weft options, a few simple slot spells, a default build and
deterministic previews; an expert reaches higher through interactions
the approved documents already own (the conditions 19 table, fixed
terrain, forced movement, selective geometry, concentration swaps, slot
timing, boss pips), joined by one small pair passive, **Interplay**. The
survival floor equals the other launch classes'; the reading floor is
higher; the ceiling is the highest. **(agreed, Q1)**

## 2. Goals, non-goals, anti-goals

### Goals

- **"Get by with some basic casting"** (the client's brief): the Mote,
  `defaultRotation` (Knit at half HP among its steps), an automatic
  Flare, pre-ticked selective Whittle and a recommended starter build
  clear solo floor 1 close to the other classes.
- **"Interesting and creative uses of the kit"** (the brief): depth from
  systemic interactions anyone can see in the preview, never from
  authored per-spell combos.
- **Skill expression that is measured and bounded:** a required four-arm
  `SkillExpressionReport` (5.4), with every bound a static predicate on
  data, so no expert can break parity, nova or the guards.
- **Research-led design** (the brief): 4.1 surveys caster designs and
  names what is taken and avoided.
- **Three things to read at every level** (settled, ability 11.4;
  classes 7.6), with one spare row.
- **The element pair as a run-long identity**, both families in use.
- **Two M2 subclasses that are roles:** the reliable Hinderer (Binder)
  and the party chemist (Alembic).

### Non-goals

- **The Herald's table.** Its own stage-2 document; this document only
  routes the Herald-dependent `classTags` (8.10).
- **Terrain and dungeon generation.** This document requests a
  terrain-presence report (DEP-A1) and relies on no terrain for any
  card.
- **Companion kits**, beyond what an Arcanist recruit's signature slice
  must not break.
- **The multiclass model** (M4); only the dip guards are honoured here.
- **Final names** (naming pass) and **final numbers** (simulator).

### Anti-goals

- **A higher survival floor** (contested in the brief; **agreed**, Q1):
  a new player who picks the Arcanist blind must survive floor 1 about
  as often as with any other class.
- **A hero coach.** The hero never gets suggestions (ability 11.3,
  **agreed**); every default here is a static default or a confirmation
  of the player's own action.
- **A new pool, reading row or condition:** the condition budget of 32
  stays untouched; 6 of 8 limited abilities; 3 rows.
- **Daily preparation, rituals, a metamagic point pool, per-cast element
  choice, a third key, teleport, summons, ground zones, Mage Armour-like
  AC, temporary HP or a second heal.**
- **Chained control:** no foe is locked by stacked Arcanist control
  (Binder: Slipped; the per-fight lost-turn cap).
- **Hidden math:** every push, payout, cancel and arc is shown before it
  is committed (AM-1).

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Classes 8.5 | Chassis: Intelligence / Dexterity; Slight 6; Reflex; light ceiling **(agreed, stage 1 Q5)**, shield never; simple weapons, arcane foci and the staff never required; 2 of Arcana, Lore, Mechanics, Medicine, Perception; Intricate; the element pair fixed for the run; slots and Weft; Knit the `restDefensiveTool`; Binder and Alembic M2, Loremaster and Elementalist unlocks; Overchannel *(illustr.)* | 7.1 |
| Classes 3.4, 8.5 | Alembic priming **once per round**, through a Weft option | 9.2 |
| Classes 5.6 | `Floor1ClassSpread` ≤ 10 points (client number), every pick and subclass, read as an M2 release gate (confirmed by the client); about 60% of limited power after level 1; `SeedHasTarget`; the per-round boss breakdown | 5, 11, 14.2, 14.3 |
| Classes 6.3 | Forbidden grants | 13 |
| Classes 7.3, 7.4 | Subclasses and aspects; the published ladder, caster column: spell cards at 2, 5, 7 (and 10), subclass and its tier-2 spell at 3, +1 ability at 4 and 8, the level-6 verb change, offers at 9, capstone at 9 | 7.7 |
| Classes 7.5, 7.6, 7.7 | The cap of eight; at most four things to read; dip guards | 7.1, 11.11 |
| Classes 8.2 | One `restDefensiveTool`, half Recovery, 1 per rest, 2 from 6; 2 kit potions | 7.5 |
| Ability 6.3, 6.4 | Rest and camp pools, 40–60% camp; one camp spend per turn | 7.4, 11.8 |
| Ability 6.5 | Four slot tiers; the full-caster table; Slot Renewal; spells known, one camp swap; upcasts only where data declares them | 7.4 |
| Ability 6.7 | Capstone: a camp pool of 1 at level 9 | 7.9 |
| Ability 7.2, 7.3 | Spell attacks take no adjacent-hostile disadvantage; a policy on every reaction | 7.2, 7.14 |
| Ability 8.1, 8.2, 8.4 | Areas ≤ 25 squares at levels 1–6; friendly fire unless selective; at-wills never hit allies; no ground zones **(agreed)** | 7.5, 8.1 |
| Ability 9.3, 9.5 | Riders: one per target per use; concentration | 7.6, 8.1 |
| Ability 11.3 | Suggestions for companions only; the hero never gets suggestions **(agreed)** | 5.2, 7.14 |
| Stat 8.1–8.4 | Damage types and riders; when a rider applies **(agreed)**; the ladder **(agreed)**; enemy traits | 8.1, 9.2 |
| Conditions 7, 10, 11, 16, 18.6, 19 | Hard control only from limited abilities; forced movement and hazard edges; fixed terrain; the budget of 32; Soaked, Oiled, Frozen; the interaction table | 5.3, 8 |
| Enemies 6.2, 6.4, 14 | The 120% reference for single-use items; the generation guards; resolve pips and Unyielding | 8, 11 |
| Weapons 6.4, 17.8, 18.5 | Parity ≤ 110% of the greatsword reference (read under VG-12); focus verbs (the rod's Shape); Attuned | 11.1, 7.2 |
| Armour 10.4, 11 | The one-reaction-AC-bonus window; the caster-defence requirement (item 2) and envelope (item 1); AR-20 already at 11 | 7.12 |
| Trinkets 11; TK-25 | The copy registry; the Ember and Frost Rings | 7.15 |
| Consumables 13.1, 16, 19; CN-11 | Scrolls and wands in class "known or not"; `classTags`; a scroll read uses the turn's camp spend **(agreed)** | 7.3, 8.10 |
| DH 8, 12.4, 17.7, 18.2, 19.3, 21.1 | Medicine solo; 2 potions; the lethal friendly-fire confirmation; floor-1 deaths under about 5% per class; the reference policy; `Floor1BossSustain` | 5, 11.3 |
| Vanguard design 7.9, 10.1, 10.4 | VG-10 (turn allowances count 0), VG-12 (the parity reading), VG-13 (the accepted level-1 camp miss) | 7.8, 11 |
| Shade design 7.5, 10.5 | SH-5 (`CampOptionBound`), SH-7 (`bigHit`), SH-28 (camp miss to level 4) | 11 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored. There is no d3 in the system (weapons 6.6),
and none is used here.

### 3.3 Findings in the approved documents

Gaps and facts found while designing; each shaped a rule here or a
change in section 18. **(lead's call)** Ids F1–F14 are local to this
table.

| # | Finding | Where it is resolved |
|---|---|---|
| F1 | The stage-1 Weft option "Shape" collides with the rod's focus verb Shape (weapons 17.8) | Sculpt (ARC-1) (renamed Whittle, naming pass NP-32) |
| F2 | SH-5's generalised `CampOptionBound` catches every slot spell, and every area and hard-control spell fails it (Overchannel Detonation 3274‰ against 1200 *(illustr.)*) | `SpellTierBound`, `SlotlessCastBound`, `PayoffBound` (ARC-2) |
| F3 | The gate arm of `Floor1ClassSpread` (DH 19.3 plus the ability 13 scorer) is a competent player; it cannot see a novice | The novice arm of `SkillExpressionReport` (5.4; ARC-29) |
| F4 | The stage-1 kit (bolt 1d10 with no ability bonus, Weft 2) clears solo floor 1 27.7 points behind the Vanguard *(illustr.)* | Mote with Int to damage; Weft 3 (7.2, 7.5) |
| F5 | Fire + acid reaches 117–118% of parity at levels 9–10 when a camp-sourced Corroded is counted free for the Mote | `SetupAttribution` (ARC-16) |
| F6 | A self-primed cold Mote runs 112–115% at levels 3–4 | The inert rule (ALM-1, **agreed** Q3) |
| F7 | The lightning arc's damage, crit and rider behaviour is unspecified (stat 8.1) | ARC-14 |
| F8 | Ability 8.2 (at-wills never hit allies) and the Soaked arc to every adjacent unit disagree | ARC-8 |
| F9 | The area ceiling at levels 7–10 is unstated (ability 8.1) | 49 squares (ARC-9) |
| F10 | Restrained's `controlRank` is unset against Dazed | ARC-17 |
| F11 | Gust as a verb-sourced push would consume an ally's verb mark | `movedByYou` (ARC-7) |
| F12 | The rest-tool reserve cannot be built from `whilePoolAtLeastHalf`, a policy that cannot sit beside `ifItChangesOutcome` | `reserveForRestTool` (ARC-6) |
| F13 | Levels 1–2 camp share is 26% and 35%: rest-timed Slot Renewal dominates early | Accepted miss (ARC-4, **agreed** Q2) |
| F14 | AR-20 is already 11 (armour 11 item 2), the Shade's worst Dexterity; the Arcanist at Dex −1 also reads 11 | No new burden (7.12) |

### 3.4 Terms

**(lead's call)** Used with these meanings everywhere below, beside the
stage-1 terms (classes 3.4) and the Vanguard and Shade designs' terms
(rank, Art, turn allowance, glint), which stand **(settled)**.

- **Family:** one of fire, cold, lightning, acid. **Pair:** the two
  families chosen at creation. **Mote key:** the family of the pair the
  Mote deals. **Off-key family:** the other one.
- **Template [F]:** a damage spell authored once and generated as one
  **variant** per family (template × family). A spell card is a variant:
  its family is printed on the card and fixed from the draw on (7.3).
  **Family-free:** a spell with no family part (physical or no damage);
  always eligible, and never counts as either family.
- **Arms** (of `SkillExpressionReport`): novice, basic, reference,
  expert (5.4).
- **Setup step:** one condition or state on a target that a later hit
  reads as a payoff (Soaked, Oiled, Restrained for Reflex, Stunned for
  Reflex, Chilled for `freezing`).
- **Interplay mark:** the one source-held mark Interplay sets (10.3).
- **Binder control, fetter, stop:** 9.1. **Prime, inert:** 9.2.
- **Pre-tick:** a cast option shown ticked by default from static data;
  the player can untick it.

## 4. Framework decisions

The client chose Q1–Q3 on 2026-11-20, each time the lead's
recommendation. **(agreed)** The rest are the lead's calls, which the
client said stand and confirmed with the document on 2026-11-20.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Floor/ceiling principle (Q1) | **"Easy to cast, hard to master: equal survival floor, higher reading floor, highest ceiling."** The novice floor-1 row of `SkillExpressionReport` is a **required report** with an escalation threshold (more than 8 points behind basic after the defaults and previews → the lead applies the levers in order → if they run out, back to the client); no new gate (5; ARC-29) **(agreed)** | B: the novice row as a release gate in classes 5.6 (stricter; forces levers early; agreed gate text changed). C: accept a higher survival floor (needs `Floor1ClassSpread` widened and DH 18.2 relaxed) |
| 2 | Base model | **Simple core, deep options**, with grafts (6) **(lead's call)** | Element engine (a new mark, a new validator, new AI pricing; its floor-1 edge comes from a starter, not the engine). Spellcraft (7–8 options on one row by L7; Quickcast dominance) |
| 3 | Signature verb | **The element pair**, fixed for the run; default **cold + lightning ("Weatherwork")**, Mote key cold; the brief's "camp swap" read as the settled one-spell swap inside the pair (7.3) **(settled; defaults lead's call)** | Re-choosing the pair at camp (contradicts stage 1 8.5) |
| 4 | Pools | Slots per ability 6.5 **(settled)**; **Slot Renewal 2 at level 9**; **Weft 3 / 4 / 5 / 6 at levels 1 / 5 / 7 / 9** (7.4, 7.5; ARC-11, ARC-12) **(lead's call)** | Stage-1 Weft 2 / 3 / 4 (fails floor 1; 71% camp share at L10). Slot Renewal 2 at L6 (fails the 60% rule increments) |
| 5 | Camp share (Q2) | **Accept the level 1–2 miss** (26% and 35% against the 40% floor) as VG-13 and SH-28 did (ARC-4) **(agreed)** | Weft 2 at levels 1–2 (about 32% at L1; costs about 2 floor-1 clear points) |
| 6 | Alembic self-payout (Q3) | **"Your mix needs a spark":** a condition instance with `origin: prime` and `source: you` is treated as absent by every damage part of your own at-wills; your slot spells and allies read it normally (9.2; ALM-1) **(agreed; told plainly that it re-reads agreed stat 8.2 and 8.3 for one subclass's own at-wills)** | The reagent restriction (Prime may only apply a reagent your Mote key does not read): narrows the subclass's choices |
| 7 | Capstone | **Overchannel as approved text:** a known slot spell of tier ≤ 3 (not an at-will) without a slot, one free Whittle mode, still the turn's camp spend; plus Fused (7.9) **(settled; Fused lead's call)** | The computed brief's "tier 1 or 2" (narrower than approved text). Two free modes (nova) |
| 8 | Parity reading | **VG-12** with **`SetupAttribution`** (ARC-16) and Slot Renewal read as rest power in both measures (ARC-3) **(lead's call; told plainly, accepted with Q1)** | Capping at-will riders at band 1 (kept as a fallback lever only) |
| 9 | Slot-spell bounds | **`SpellTierBound`** (≤ 2000‰ single target, ≤ 1250‰ per target for areas), **`SlotlessCastBound`**, **`PayoffBound`** (ARC-2) **(lead's call)** | `CampOptionBound` read literally (every area and hard spell fails). 1200‰ for areas (the approved Detonation (fire) scroll fails) |
| 10 | Creation pick for the gate | **Pair × Mote key × level-3 subclass**, starters held at `pairDefaults`, every legal starter combination printed; `StarterDefaultsHaveAnswer` and the starter rule (7.7; ARC-15) **(lead's call; told plainly, accepted with Q1)** | Starters as picks (multiplies rows without a decision behind them) |
| 11 | Hero defaults | **Cast-option defaults** (`castDefaults`): visible, shown ticked, the player can untick them; static predicates; the last-slot confirmation; the pip glint (7.14; ARC-6, ARC-13) **(lead's call; told plainly as a reading of agreed 11.3, accepted with Q1)** | A hero coach (reverses agreed 11.3; not proposed) |
| 12 | Level-3 default | **Alembic** in `pairDefaults` until the per-subclass rows land (9.1); a creation default for players, which does not move the unweighted class-mean rows (5.4) **(lead's call)** | Binder as default now (novice 69–72% against 81–82% *(illustr.)*) |
| 13 | Reading | **Three rows:** at-wills; spellbook (all tiers + Overchannel); Weft. Glints count 0 **(lead's call)** | Knit on its own row (kept as the stage-1 lever, never with an offer row) |
| 14 | Starting kit | Leather, staff, dagger, 2 potions, exactly 15 coin (SK-1); no creation kit choice (7.11; starting kits design 5.1) **(lead's call)** | A kit pick beside the pair (a second level-1 decision) |

### 4.1 How other games handle casters

Mechanism references only. No names or text are taken. Web-checked in
part during research: the PF2e kineticist (forums and playtest
summaries), Gloomhaven element infusion and consumption, the 2024 wizard
(reveal summaries), Wildermyth's interfusion, the Slay the Spire
Defect's orbs and Focus, and DOS2 environmental combinations. The rest
is recall, flagged in Appendix B. Every SRD-shaped mechanism is checked
against SRD 5.2.1 before any record is written **(settled, classes stage
1)**.

| Game / feature | What it does | We take | We avoid |
|---|---|---|---|
| **5E wizard (SRD)** | Slots; prepared spells; Arcane Recovery on a short rest; cantrips scale; Sculpt Spells; Potent Cantrip | Slot Renewal **(settled)**; Mote scaling at 5; selective Whittle | Daily preparation (ability 2 non-goal); rituals |
| **5E warlock** | Few slots; an at-will blast with ability to damage and extra beams | **The Mote**: Int to damage, two beams at 5. The clearest "simple caster" | Pact slots refreshed per short rest (a second recovery) |
| **5E sorcerer** | A point pool; metamagic (careful, twinned) | Careful-like selective, paid from Weft | A metamagic point pool (a new pool and reading row) |
| **PF2e wizard** | Drain a bonded item (recast one spell per day); theses | Overchannel as a once-per-floor slotless recast | Thesis sprawl |
| **PF2e kineticist** | At-will impulses carry the class; optional taxed overflow; two-element composites | "The at-will carries; burst is optional"; two keys are enough | Gather/dismiss aura state |
| **4e wizard** | At-will / encounter / daily; the controller role; an orb that worsens saves | The controller role (our Hinderer); the orb idea → the Binder's Cinch | Per-encounter pools (ability 6.2: no major per-fight power) |
| **13th Age wizard** | An escalation die rewards late rounds | Nothing; noted for long boss fights | A round counter |
| **Shadow of the Demon Lord** | Traditions; castings by power | Nothing | Breadth reward (cap and reading) |
| **Lancer** | Heat and overcharge with escalating self-cost | The idea of a taxed spike | A new resource row |
| **Gloomhaven** | Shared, decaying elements infused and consumed by anyone | "A visible setup anyone can cash": our riders on units already are that | New board state |
| **Spirit Island** | Element thresholds unlock optional innate tiers | Optional bonuses that never gate the floor (Interplay) | Threshold counting |
| **Slay the Spire, Defect** | Orbs as a readable engine; Focus scaling; a starter deck that works | A strong default build; a readable engine | Hidden math |
| **Into the Breach** | Fully previewed pushes make positioning the skill | **The interaction preview (AM-1)**: every push, collision, hazard edge and ladder row shown | — |
| **DOS2, BG3, Magicka** | Surfaces and element combos; friendly fire; concentration | The wet-then-lightning feel, through Soaked on units; concentration as a decision | Ground surfaces and zones (ability 8.4); combo sprawl |
| **Wildermyth** | Interfusion: spells drawn from nearby objects | Terrain as fuel (fixed squares) | Authored per-object effects |

**Combined lesson.** Low-floor, high-ceiling casters share one shape: a
strong at-will that works alone, few simple limited spells, a default
build, deterministic previews, and depth from optional interactions
players can see. Nothing in the approved documents needs a new resource
to get there.

## 5. The client's brief: floor and ceiling

### 5.1 The question and the lead's answer

**The client's view.** Intelligence builds are complex; players must be
able to get by with basic casting; experts should find skill expression
and creative uses; the skill floor will likely be higher than the
Vanguard's, supplemented by a larger ceiling.

**The lead's answer (agreed, Q1):**
- **Agree:** "complex" (the creation label, printed "Intricate" since
  the naming pass, 5.9.9; more interactions, three
  rows that hold more options than the Vanguard's) and "higher ceiling"
  (the expert-over-basic floor-averaged damage gap grows from about 25%
  at level 1 to 34–49% at level 10 *(illustr.)*, 11.5).
- **Contest:** a higher **survival** floor. Four reasons:
  1. **The class is chosen blind and the first run is the harshest.** No
     test drive, solo floor 1, permadeath. A new player who dies in room
     3 learns "the mage is weak", not "I spent my slots too early". The
     1–2 hour run means mastery is learned across runs, so each run must
     be survivable the first time.
  2. **DH 18.2** wants floor-1 deaths under about 5% for every class
     (client number).
  3. **`Floor1ClassSpread`** (classes 5.6, 10 points, client number; an
     M2 release gate, confirmed by the client) averages each class over
     **every creation pick and subclass**. A class that only experts
     survive drags its mean down and fails.
  4. **The gate cannot see novices.** Classes 5.6 runs the reference
     policy; DH 19.3 defines that as rest, drink and container rules
     only, with combat from the ability 13 party scorer, a competent
     one-step scorer. So the gate measures a competent player.
- **The principle:** "**Easy to cast, hard to master: equal survival
  floor, higher reading floor, highest ceiling.**" A wider *full-run*
  expert-minus-novice gap than the Vanguard's is welcome. A wider
  *floor-1* gap is not.
- **Where the client is right (told plainly, accepted with Q1):** the
  floor is higher **today**. A scripted novice clears solo floor 1 16–29
  points behind the Vanguard in the proxies *(illustr.)*, and the gate
  arm cannot see it. The design closes that gap with automatic and
  display-only parts (5.2) and measures it with the novice row (5.4).

### 5.2 What basic play looks like

**Recommended starter build (creation defaults, classes 7.4) (lead's
call):**

| Choice | Default | Why |
|---|---|---|
| Array | Int +3, Dex +2, Con +1, Wis +1, Pre 0, Str −1 | Creation warns below Dex +2 (AC 13 or less; Dex −1 costs 19–23 clear points *(illustr.)*) and below Con +1 |
| Pair, key | cold + lightning ("Weatherwork"), Mote key cold | No self-cancel; Soaked makes both weak; Chilled slows melee; minors never spend boss pips |
| Starters | **Shard (lightning)** + **Upheaval** | One ranged group hit in the off-key family (sets an Interplay mark for the cold Mote), one "get off me". **In a party both can hit allies** (Shard's r1 burst lands on a foe in melee with an ally; Upheaval's r1 burst and push of 2 catch adjacent allies): the selective pre-tick protects them (7.14) and `defaultRotation` checks for allies (below) **(lead's call)**. Simulator A/B owed against Frost Shard and Fan (lightning) |
| Skills | Medicine + Lore | Medicine is close to compulsory solo (DH 8); Lore feeds the resistance preview |
| Kit | Leather, staff two-handed, dagger, 2 potions | Parry is pool-free; Steady Casting about 84% on DC-10 concentration checks *(illustr.)* |
| Level 3 | Alembic (9.1 says when this may change) | Minors always land on bosses |

**`defaultRotation`** (a static data record, read by the simulator's
novice and basic arms and printed as ladder-preview lines; never
highlighted live; the hero never auto-casts from it) **(lead's call)**.
Schema: an ordered list of rules `{when: predicateId[], do: actionId,
slotTiers: tier[], bonusAction?: actionId}`; the first rule whose `when`
all hold and whose action is legal is taken; the bonus-action rule is
checked before the action rules each turn and does not stop them
**(new lead's call)**:

| # | `when` | `do` | `slotTiers` |
|---|---|---|---|
| B | `hpAtMostHalf`, `knitAvailable` | `bonusAction: knit` | — |
| 1 | `enemiesInArea>=2`, `noAllyInAreaOrSelectiveAvailable` | `castBestArea` | 1 (2+ only when `isBossFight`) |
| 2 | `nonBossAdjacent` or `nonBossActsBeforeMyNextTurn`; `noAllyInAreaOrSelectiveAvailable` | `castBestControl`, else `castShockwave` | 1 (2+ only when `isBossFight`) |
| 3 | `isBossFight` | `castBestSingle` | any |
| 4 | — | `mote` (else `gust` when `nonBossAdjacent` and no ally in the line) | — |

- The predicate vocabulary is closed: `enemiesInArea>=2`,
  `noAllyInAreaOrSelectiveAvailable` (no ally would be damaged or moved
  by the cast, its arcs and forks included, or selective is affordable
  and makes it so), `nonBossAdjacent`, `nonBossActsBeforeMyNextTurn`, `hpAtMostHalf`,
  `knitAvailable`, `isBossFight`. Each reads static data and the board,
  never a scorer term.
- **"Best"** = the highest static `SpellTierBound` ‰ (`boundModel`, 11.2)
  among eligible known spells of the allowed tiers; ties by record id.
  "Control" = a spell whose record applies a major or hard condition.
- `slotTiers` is the slot hold: "2+ only when `isBossFight`" keeps tier-2
  and higher slots for the floor's boss. **Greedy** (the novice arm)
  means the arm ignores `slotTiers`.

**Ladder-preview lines:** "Mote every turn. Spend a spell when two or
more are close, or on the boss." "Upheaval when they reach you."
"Lightning spell, then cold bolt: the bolt's chill lands." "Allies in
your area? Whittle makes it miss them." "Your areas and pushes hurt
allies; your bolt never does." "Soaked foes are weak to cold and
lightning; fire dries them." "Bind the guard, not the boss." Plus the
pair's line (7.3).

**Why this clears floor 1:** the Mote alone (1d10 + Int, range 12, about
2–3 hits for a 12–18 HP standard); two tier-1 slots at the door, three
from level 2, refilled by Slot Renewal; Parry and Flare on automatic
policies and Knit at half HP; the tier-2 slot held for the boss from
level 3.
Basic-arm class mean 92.9% against the Vanguard's 98.3% *(illustr.)*, a
gap of 5.4.

**What the novice gets wrong, and the fixes** (none is a suggestion;
told plainly, accepted with Q1):
- **Greedy slots.** A **last-slot confirmation** on the DH 17.7 pattern,
  a confirmation of the player's own action (ARC-13). **Predicate (new
  lead's call):** it fires when all hold: (a) the cast, upcasts
  included, spends the last remaining slot of its pool's tier T, with T
  ≥ 3, or T = 2 and no other slot of tier 2 or higher remains; (b) no
  boss or elite is in the current fight; (c) the floor's boss is alive;
  (d) it has not fired yet this rest; (e) the player has not muted it
  for the run ("don't ask again"). The message is generated from data:
  "Last tier-{T} slot. {n} slot(s) of tier 2 or lower return on a short
  rest" for T = 2 (n = Slot Renewal's count at your level); "Last
  tier-{T} slot. It returns only at camp" for T ≥ 3. Conditions (b) to
  (e) keep it rare, so it is read rather than clicked through.
- **The pip trap.** `castDefaults` never pre-tick a major-control spell,
  Cinch or a control-purpose Prime on a unit with resolve pips left; the
  preview reads "strips a resolve pip only; Unyielding after"; a **pip
  counter glint** on the boss (0 to read). The Binder's card says "bind
  the guard, not the boss".
- **Dead picks.** `StarterDefaultsHaveAnswer` and the starter rule
  (7.7); Expert cards out of the level-2 draw; a "Recommended" badge
  from static data on every draw.

### 5.3 Where skill expression comes from

All systemic; nothing authored per spell.

| # | Pattern | Mechanism (existing rule unless marked) | Where |
|---|---|---|---|
| E1 | Soak, then strike | Drench, Undertow, Prime, water squares → Soaked: cold and lightning weak, riders automatic, lightning arcs to every adjacent unit | Floor 1+ |
| E2 | Freeze, then Fracturing | Chilled or Soaked + a limited `freezing` cold source → Frozen (hard; Steeled, resolve, Unyielding); bludgeoning shatters (+half, automatic Staggered) | L3+ (Rime Lance), L7 (Deep Freeze); a party combo |
| E3 | Oil, then burn | Oiled + fire → +half, Burning one band higher; every fire area burns oil squares to plain floor (settled terrain row) | Fire pairs |
| E4 | Push into terrain | Gust (1), Upheaval (2), Squall (3), Reel (2): water, fire, oil, web squares; hazard edges give Teetering | Wherever terrain exists (risk table, 16) |
| E5 | Restrain, then area | Restrained gives Reflex disadvantage; Stunned auto-fails Reflex | L2+ |
| E6 | Thread the party | Whittle selective, widen, Whittle II (both) | Floor 2+ |
| E7 | Deliberate friendly fire | **Temper** (new trigger): fire thaws a Frozen ally, cold puts out a Burning one, with no damage | Floor 2+ |
| E8 | Concentration management | One effect at a time; **Afterhold** (new trigger) bridges a swap; staff Steady Casting; **Fray** against enemy casters | L2+ |
| E9 | Slot timing | Slot Renewal returns the highest spent tier ≤ 2: spend the tier-2 slot just before a rest; Overchannel reuses the best tier-3 spell | L3+ |
| E10 | Pips economy | Minors never spend pips; majors on the escort; Cinch strips pips faster for the party | Every boss |
| E11 | Party setups | The conditions 17.3 `setup` term already prices Soaked and Oiled for payers | Floor 2+ (the Alembic's lens) |
| E12 | Interplay ordering | A mark from one family, cashed by the other family's hit before the end of your next turn (new, 10.3) | L1+ |

**The gain is realisation, not raw power.** Experts land more of the
*bounded* value: setup uptime, fewer wasted slots, less friendly fire,
more pips stripped. The automatic parts mean a basic player still gets
some of it by accident.

### 5.4 How skill expression is measured and bounded

**`SkillExpressionReport`** (ARC-29; classes 12.3; simulator; M2)
**(lead's call; the novice row's status agreed, Q1)**:

| Arm | Definition | Status |
|---|---|---|
| **Novice** | Scripted: `defaultRotation` with greedy slot spend (ignores `slotTiers`); Whittle only through pre-ticks; default reaction policies; aim seeded at random among the top 3 scored aims; no deliberate hazard pushes; the novice responses below | **Required report**; the floor-1 row has the escalation threshold below **(agreed, Q1)** |
| **Basic** | `defaultRotation` + `castDefaults`, slots held (`slotTiers`), the best scored aim, no lookahead | Required report |
| **Reference** | DH 19.3 + the ability 13 scorer: the arm `Floor1ClassSpread` gates **(settled)** | Gate (classes 5.6, unchanged) |
| **Expert** | The scorer + a 2-ply lookahead over the party's turn order (mark → cash; prime → payout; push → area; Restrain → area; concentration swap), `setup` term on | Simulator only; never an AI behaviour or a suggestion |

**What every arm enumerates (new lead's call):** the same picks as the
gate, pair × Mote key, crossed with the level-3 subclass (ARC-15), with
starters at `pairDefaults` and the pick seeded per run; each arm's
**class mean** is the unweighted mean over those picks. The level-3
default in `pairDefaults` (9.1) is therefore a creation default for
players and does not move any class-mean row; it changes which
per-subclass row a default-following player lands in.

**Pending request (seeds and validation design SV-22, 2026-11-23; M2;
the Arcanist owner rules, with the class owners):** the four arms are
generalised to every class as the simulator's `Skill` axis
(`Skill.Novice`, `Skill.Basic`, `Skill.Reference`, `Skill.Expert`; a
flagged extension of this section, lead's call, approved by the client
with that design); `Skill.Reference` stays DH 19.3 plus the ability 13
scorer **(settled)**, and the definitions above are the Arcanist's rows
of it. If refused, each class owner writes a novice script by the M2
planning gate (seeds and validation design 6.2, 11, 14.2). The novice
row's result class there is ESC, read at the M2 F1 balance freeze
(seeds and validation design 6.3, 7); its status here is unchanged.

**Novice responses (new lead's call; *illustr.* probabilities):** so that
the fixes applied before the escalation point are measured, the novice
arm confirms the last-slot confirmation with probability 50%; drops an
aim whose preview reads "strips a resolve pip only" with probability
50% (otherwise casts it); unticks no pre-tick; and never reads glints.
The basic arm always declines the last-slot confirmation (which never
fires in a boss or elite fight, 5.2) and never casts a "strips a pip
only" aim it has an alternative to.

A **modifiers-off** run is printed beside each arm, so each new
mechanic's share of the ceiling is visible **(lead's call)**. Defined as
`modifiersOff = {interplay.enabled: false; the arm's build never takes
the triggers moteFray, sculptTemper or afterhold}` **(new lead's
call)**.

**Targets** *(illustr.; lead's call)*:
- floor-1 clear: basic − novice ≤ 8 points (the escalation threshold,
  **agreed** Q1); expert − reference ≤ 5 points;
- floor-1 novice death share: **report only**, feeding the escalation
  above; the DH 18.2 test (about 5%) and the 20.2 return read the
  reference arm, as DH 19.3 does **(new lead's call)**;
- floors 2–5 clear and full-run win rate: expert − basic 8–20 points,
  and at least the Vanguard's gap + 5. Under 8: the depth is fake
  (lever: the element engine, 10.1). Over 20: the basic player is
  trapped; strengthen the defaults, never the expert;
- per-pair, per-subclass and per-card rows; a **dominant-combo flag**
  (expert win rate > 8 points above the class mean) and a **trap-card
  flag** (novice pick-to-clear delta below −5 points);
- diagnostics under every arm: setup payout rate (paid out against
  expired with no effect), friendly-fire share, Weft split (Whittle /
  Flare / Knit / subclass option), freeze rate, concentration drops by
  cause, pips stripped, turn starts denied, Interplay rider rate,
  off-pair scroll-read share, realised camp share.

**Escalation (agreed, Q1; procedure lead's call):**
1. **When it is read:** on the release seed set, at the first M2
   simulator run with AM-1, AM-2, the pip-aware `castDefaults`, the
   last-slot confirmation, the starter rule and
   `StarterDefaultsHaveAnswer` all in data. Earlier runs are diagnostics
   only **(new lead's call)**.
2. **What escalates:** the **class-mean** novice floor-1 row more than 8
   points behind the class-mean basic row, both means taken as above.
   Per-pair and per-subclass rows beyond 8 points are printed and
   answered with per-row data changes (a pair's starters in
   `pairDefaults`, card weights, `castDefaults` filters); they escalate
   only through the class mean **(new lead's call)**.
3. **The levers, in order** (11.8), one at a time, with a rerun after
   each; the lead stops at the first that meets the threshold **(new
   lead's call)**.
4. **If the levers run out:** back to the client. Lever 5, the
   floor-pacing request, is already in force (the Forecourt Top-up,
   run structure design 9.4, RT-8), so the levers count as run out after
   levers 1–4 (cross-document comparison 2026-11-23, CC-72).
5. **No new gate:** classes 5.6 is agreed text and stays as written.

**Bounded by construction:** `SpellTierBound`, `PayoffBound`,
`SlotlessCastBound` (all evaluated by the tuning file's `boundModel`,
14.1), the area caps, the anti-lock guards and every enemies 6.4 guard
are static predicates on data; parity (VG-12, with `SetupAttribution`
as its accounting rule, ARC-16) and the nova bound are measured at the
worst legal build. None reads a policy. An expert can only move
realised value toward the bound and can never pass a gate the reference
arm fails.

### 5.5 Level table *(illustr.; default array, both increases to Int)*

HP = 11 + L × (6 + Con); Recovery = 4 + 2L + Con; Knit heals
FloorDiv(Recovery, 2). AC is leather with Dex +2 (7.12). Spell attack =
proficiency + Int; spell difficulty = 8 + proficiency + Int.

| L | Floor | HP / Rec / Knit | AC | Prof / spell atk / DC | Slots t1/t2/t3/t4 | SR | Weft (Knit) | Mote | Known | Automatic | Choice |
|---|---|---|---|---|---|---|---|---|---|---|---|
| 1 | 1 | 18 / 7 / 3 | 14 | +2 / +5 / 13 | 2 | 1 | 3 (1) | 1d10+3 | 2 | Kit; Mote, Gust; Whittle I, Flare, Knit ×1; Interplay | **Pair + Mote key + 2 starter tier-1 spells** (the starter rule, 7.7) |
| 2 | 1 | 25 / 9 / 4 | 14 | +2 / +5 / 13 | 3 | 1 | 3 (1) | same | 3 | Slots 3 | **Spell card** (1 of 3; Expert cards excluded) |
| 3 | 1→2 | 32 / 11 / 5 | 14 | +2 / +5 / 13 | 3/1 | 1 | 3 (1) | same | 4 | Tier 2; subclass level-3 features; signature rank I | **Subclass**; its tier-2 spell is the level-3 known spell |
| 4 | 2 | 39 / 13 / 6 | 14 | +2 / +6 / 14 | 3/2 | 1 | 3 (1) | 1d10+4 | 4 | — | **+1 ability**, the only choice **(settled)** |
| 5 | 2→3 | 46 / 15 / 7 | 14 | +3 / +7 / 15 | 3/2/1 | 1 | 4 (1) | 2 × 1d10+4 | 5 | Tier 3; Mote two beams; Whittle II; signature rank II; subclass Art I | **Spell card** |
| 6 | 3 | 53 / 17 / 8 | 14 | +3 / +7 / 15 | 3/2/2 | 1 | 4 (2) | same | 5 | Knit ×2 **(settled)** | **Subclass feature** (1 of 2; changes a verb) |
| 7 | 3→4 | 60 / 19 / 9 | 14 | +3 / +7 / 15 | 3/2/2/1 | 1 | 5 (2) | same | 6 (5) | Tier 4; area ceiling 49 (ARC-9); signature rank III | **Spell card** (the draw holds a trigger option, settled) |
| 8 | 4 | 67 / 21 / 10 | 14 | +3 / +8 / 16 | 3/3/2/1 | 1 | 5 (2) | 2 × 1d10+5 | 6 (5) | **Flare II** | **+1 ability** |
| 9 | 4→5 | 74 / 23 / 11 | 14 | +4 / +9 / 17 | 3/3/2/2 | **2** | 6 (2) | same | 6 (5) | **Overchannel** and its subclass rider (Art II); **Slot Renewal 2** | **Offer** (1 of 3) |
| 10 | 5 | 81 / 25 / 12 | 14 | +4 / +9 / 17 | 3/3/3/2 | 2 | 6 (2) | same | 7 (6 or 5) | — | **Spell card** (the draw holds one tier-4 signature card of the pair while one is unknown, 7.7) **or** offer |

- "SR" is Slot Renewal's slots per short rest. "Known" counts spells
  known, the subclass spell included; the brackets show one less from
  level 7 if the level-7 trigger option is taken, and one less again at
  level 10 if the offer is taken.
- **Talents: none** (lead's call, as stage 1 7.4 draws the caster
  column). Reshaping comes from Weft ranks, the level-7 trigger option,
  offers and the subclass.

## 6. Floor/ceiling models

### 6.1 The three models

| Model | Core idea | Strength | Weakness |
|---|---|---|---|
| **Simple core, deep options (SC)** | Whittle (selective, widen, load), Flare, Knit, a strong Mote, defaults and previews; depth from the existing interaction table and terrain | Keeps every approved rule; adds nothing to count or read | Thin floor-1 depth by design; the fire + acid parity reading; the L10 camp share |
| **Element engine (EE)** | Residues per family (Burning, Chilled, Corroded, and a new source-held Ionised mark for lightning); **Conflux** (the other family's hit on a residue lands its rider free); **Detonate** (a slot cast consumes one residue for +half) | The richest ceiling | A new mark, a new validator (ComboBound), new AI pricing; party composition becomes a balance input; Detonate can consume the Chilled that keeps a melee foe off a basic player |
| **Spellcraft (SP)** | A metamagic menu on Weft: Sculpt, Lace (rider), Linger (extend), Fork (twin), Quickcast (bonus-action cast), arriving in waves | Familiar; many decisions | 7–8 options on one row by L7; Quickcast dominance; Fork near the lockout line; Linger lengthens Dazed |

A **new-mechanics package** (AM-1 to AM-12) was judged as a graft menu,
not a model (10).

### 6.2 Verdict and judges' scores

Mean of three judges, 1–10; build cost is scored as "low cost", so
higher is cheaper. The total is the sum of the six means.

| Model | Depth and growth | Readability | Decision quality | Balance safety | Fit with docs | Build cost (low) | Total | First place |
|---|---|---|---|---|---|---|---|---|
| **Simple core** | 6.7 | **9.0** | 7.3 | **8.0** | **9.0** | **8.7** | **48.7** | **3 of 3** |
| Element engine | **8.7** | 6.3 | **8.0** | 6.0 | 6.3 | 5.3 | 40.6 | 0 of 3 (held as a Later lever) |
| Spellcraft | 8.0 | 5.0 | 7.0 | 6.7 | 6.7 | 5.0 | 38.4 | 0 of 3 (held Later) |
| Mechanics package (one judge) | 8 | 8 | 8 | 8 | 8 | 6 | — | graft menu |

**Verdict (lead's call): simple core**, unanimous. It keeps every
approved rule (once-per-round priming, Overchannel ≤ tier 3, no hero
suggestions), adds nothing to count or read, and puts its depth into
interactions the documents already own. Its weaknesses (thin floor-1
depth, a dead Interplay wording, the fire + acid parity reading, the L10
camp share) are fixed by grafts.

**Why not the others.** The element engine's floor-1 edge (3.2 points
against 5.4) comes mostly from its Fan default starter; the engine
itself adds about +0.5 points on floor 1 *(illustr.)*. It also misses
the L10 camp share with either subclass. Spellcraft equals the simple
core on floor 1 and costs a crowded row.

### 6.3 The spine and its grafts

**(lead's call)**

| Graft | From | Where |
|---|---|---|
| Interaction preview and combo glint (AM-1) | Package | 10.2 |
| Play defaults: pre-ticked selective, `defaultRotation`, `pairDefaults`, `recommendedPickOrder`, creation warnings (AM-2) | Package | 7.14 |
| Pip-aware `castDefaults`; last-slot confirmation; pip glint | Critics, Binder | 5.2, 7.14 |
| Interplay in the "before the end of your next turn" wording, behind a lever | Package (AM-3) | 10.3 |
| `drawRequires: offFamily`; alternating `recommendedPickOrder`; off-key default starter | Element engine | 7.3, 7.7 |
| The fire + cold pair as "Opposed, for experts", with a safe key | Element engine | 7.3 |
| Fused (Overchannel counts as both families for Interplay) | Element engine | 7.9 |
| Global cast rules: one free Whittle mode per cast; ≤ 3 Weft per cast; refund on refusal | Spellcraft | 7.5 |
| Greyed tray toggles with a reason; live preview per toggle | Spellcraft | 7.5 |
| Novice and modifiers-off arms as required reports | Spellcraft, package | 5.4 |
| Temper, Fray, Afterhold as trigger offers; Insulate folded into Flare II | Package | 7.13, 10 |
| Slot Renewal 2 at L9; `PayoffBound` widened; `SetupAttribution` | Numbers check, critics | 7.4, 11 |

- **Held as Later levers:** the element engine (Conflux, Detonate,
  Ionised, ComboBound), used if the expert-minus-basic win-rate gap is
  under 8 points; Lace, Fork and Quickcast; AM-12 Pair Conflux if a pair
  is shown trapped.
- **Cut, with one-line reasons:** Linger (lengthens Dazed; name
  collides); a metamagic point pool (a new row); the simple core's
  "later in the same round" Interplay wording (never fires solo).

## 7. The core kit

### 7.1 Chassis

| Field | Value |
|---|---|
| Main / suggested second | Intelligence / Dexterity **(settled)** |
| `suggestedArray` | Int +3, Dex +2, Con +1, Wis +1, Pre 0, Str −1 **(lead's call)** |
| HP tier, save | Slight 6 **(settled)**; Reflex **(settled)**. HP = 11 + L × (6 + Con): 18 / 32 / 46 / 81 at L1 / 3 / 5 / 10, the reference row |
| Recovery | 4 + 2L + Con: 7 / 11 / 15 / 25; Knit heals half: 3 / 5 / 7 / 12 |
| Armour, shield | Light ceiling **(agreed, stage 1 Q5)**; shield never |
| `weaponTraining` | `{simple: all, martial: none}`; arcane foci (orb, rod, tome) and the staff, never required |
| Skills | 2 of Arcana, Lore, Mechanics, Medicine, Perception **(settled)**; default Medicine + Lore |
| Complexity | Intricate, shown at creation **(settled)** |
| Concentrator | 1 of 2 (Stillmind, Later) **(settled)** |
| Things to read | **3 at every level**: at-wills; spellbook (all tiers + Overchannel); Weft. Glints count 0. One spare row |
| Limited abilities | Tiers 1–4 (4) + Weft + Overchannel = **6 of 8** |
| Not counted | Knit sub-limit **(settled)**; Gust's and Reel's `movedByYou` round count; the at-will rider bound (engine); Whittle's one-per-cast rule; the Interplay mark; subclass turn allowances (VG-10) |
| `fallbackDamageTypes` | bludgeoning (staff, Upheaval), piercing (dagger, Shard's primary) |
| `level1MechanicIds` | `elementPair`, `mote`, `gust`, `weft`, `sculpt`, `flare`, `knitHalf`, `slotSpellbook`, `interplay` |
| `signatureVerb` | the slot pools + `elementPair` (dip guard 2) |
| `signatureEvents` | a damage part typed from the pair dealt by any ability; a Whittle used (ARC-1); an Interplay rider landed |
| `seedTarget` | `twoEnemiesWithinRadius1` **(settled)** |
| `leans[]` | Area damage **P**, control **P**, damage types and riders **s**, terrain combinations **s**; knowledge and initiative ribbons **(settled)** |
| Tools, not leans (SH-27 pattern) | Gust's push and Reel (the forced-movement row stays the Wrecker's lens); Flare (ward); Temper and Tincture (cleanse-like, no cleanse row) |
| Dip-guard fields | `chassisOnly` on Knit, armour, save, skill count, kit; `minClassLevel 9` on Overchannel and Slot Renewal 2; `scalesBy: classLevel` on Weft, Mote beams, Knit sub-limit |

- **The Interplay flag and the validators (new lead's call):** validators
  read data, never the tuning flag. With `interplay.enabled` off,
  `interplay` stays in `level1MechanicIds` as an inert mechanic, its
  `signatureEvents` entry is unreachable but allowed (the other two
  events keep the list live), the AM-1 line 8 and the Interplay ladder
  line are suppressed by the flag, and Fused (7.9) does nothing.

**Weaknesses (on the card) (settled):** low HP; friendly fire;
concentration under focus fire; single-target bosses (pips, Unyielding).
Added **(lead's call)**: flat AC 14 at the suggested array (light armour
does not band).

### 7.2 At-wills (one reading row)

**Mote** (the element bolt; one spell id, four family variants; the hero
uses `moteKey`) **(change ARC-10)**:
> **Action.** Ranged spell attack, range 12 (+4 with the orb's Reach
> Out). On a hit, **1d10 + Int** damage of your Mote key's family.
> **From class level 5, two beams**: two attack rolls, at the same
> target or different targets. A rider is attempted only against a
> target weak to the family (automatic) or on a critical hit; the
> at-will rider is attempted at most once per turn (ability 9.3). An
> at-will's Soaked arc spread happens at most once per turn across both
> beams (ARC-14). Never affects an ally, its arcs included (ARC-8).

- **Resolution order (new lead's call):** the player declares both
  beams' targets and their order (the scorer orders them for
  companions); the beams then resolve **fully in sequence**: beam 1
  attack → damage → rider attempt (weak-target automatic, crit, load or
  Interplay cash-in) → Interplay mark set → Prime (Alembic), then beam 2
  the same way. "First" in any once-per-turn rule (the at-will rider,
  the Soaked arc spread, Fray, Whittle load, the Prime pre-tick) means
  the first beam in that order that qualifies.
- **The once-per-turn rider bound counts every at-will rider attempt,**
  weak-target automatic ones included; the arc-spread sentence above
  follows from it and is kept as a reminder. A second beam on a weak
  target deals its weakness damage but attempts no rider.
- Spell attacks take no adjacent-hostile disadvantage (ability 7.2), so
  the Mote works in melee.
- Parity: 73 / 73 / 75 / 75 / 74 / 74% of the greatsword reference at L1
  / 3 / 5 / 7 / 9 / 10 *(illustr.)*.
- Lever if the floor-1 rows miss: 1d12 at L1–4 (about 79%; 11.8 lever
  3). Fallback if parity drifts: 2d10 + Int from L5 (about 60%).

**Gust** (utility) **(change ARC-10; ARC-7)**:
> **Action.** Line L4 from you. The first hostile creature in the line
> makes a Fortitude save or is pushed 1 square away from you. Allies in
> the line are ignored and never affected. No damage, no rider, never
> hard control.

- **Line shape** so the rod's focus verb (weapons 17.8: line and cone
  cantrips +2 length) has a target: L6 with a rod.
- **Counts as verb-sourced for collisions** (no collision damage, AR-52
  precedent), but uses the Arcanist's own source-held per-target round
  count `movedByYou` (shared with Reel) instead of the party's
  verb-moved mark, so it never blocks an ally's verb push that round.
  `FlagNotCondition` passes (no save, tier or cure; Shrugged Off
  precedent).
- M2 hazard edges apply: a Reflex save, then Teetering on a success
  (conditions 10.2).
- **Into a contact hazard (M2; TR-10, agreed Q3):** a failed save deals
  the typed collision band 2 / 3 / 3, never the contact band (Gust is
  purely verb-sourced); a success is an ordinary obstacle stop with no
  damage and no Teetering. It is environment damage, counted in the
  at-will rider bound (room terrain design 6.5.2; cross-document
  comparison 2026-11-23, CC-76).
- Uses: step a foe off you, then walk away under Supple; edge a foe into
  water, fire or oil; set up Teetering for an ally's push.

### 7.3 The signature verb: the element pair

1. **The pick.** At creation choose 2 of fire, cold, lightning, acid,
   plus the **Mote key**, one family of the pair. One creation choice.
   `elementPair[2]` and `moteKey` are **fixed for the run (settled,
   stage 1 8.5)**.
2. **Data.** Damage spells are authored as family templates and
   generated as variants (template × family). **The draw deals
   variants:** each card shows its family, and the family is fixed from
   the draw on; at most one variant per template appears in a draw
   (SP-4) **(new lead's call: one model, used in 7.7 and ARC-21)**.
   Only variants of the pair's two families are eligible. Family-free
   spells are always eligible.
3. **Camp swap.** One known spell may be swapped for another eligible
   spell at camp (the tome's Annotate allows it at a short rest)
   **(settled, ability 6.5)**. The subclass spell is never swapped. The
   pair and key never change. This is how the brief's "camp swap only"
   is read.
4. **Keys.** Physical secondary parts are allowed as fixed parts. No
   psychic, radiant or necrotic part on any Arcanist class record: the
   static content check `ArcanistDamageTypes` (every damage part in
   {fire, cold, lightning, acid, physical}), scoped to class records
   (ARC-27). The pair itself is known only at runtime, so the runtime
   invariant **`KnownVariantInPair`** holds instead: every known
   variant, and the family of Catalyse, Crux and Lay Bare, is in
   `elementPair`; asserted on creation, draw, swap and subclass gain,
   with a property test. The only exempt path is a gear shift (the Ember
   and Frost Rings, Attuned; item 6) **(new lead's call)**.
5. **Scrolls and wands** are in class when the spell is anywhere on the
   Arcanist list, off-pair variants included ("known or not",
   consumables 13.1). A Detonation (fire) scroll is in class for a Weatherwork
   Arcanist. This is the one sanctioned off-pair route: a one-shot, the
   turn's camp spend (CN-11), never a key switch. Stated on the scroll
   card; off-pair read share reported.
6. **Gear.** The Ember and Frost Rings (TK-25) and the focus property
   **Attuned** (weapons 18.5) may shift a Mote part: a bounded gear
   third key, covered by trinkets 11 and weapons 18.5. Registry note
   only.
7. **Interplay** (10.3) is the base passive that makes the two families
   matter together.

**Pair cards at creation** (one line each; the core computes the
cancels; the display shows them) **(lead's call)**:

| Pair | Label | Line | Default key |
|---|---|---|---|
| **cold + lightning (default)** | Weatherwork | "Soak, then strike: both weak on Soaked; no self-cancel" | cold |
| fire + acid | Ruin | "Burn, then strip armour; fire pays out Oiled" | fire |
| fire + lightning | Flux | "Oil for fire, Soak for lightning; Soaked resists fire, so the setups fight" | fire |
| acid + cold | Scour | "Corrode, then chill; acid ends Frozen; Soaked washes Corroded" | cold |
| acid + lightning | Etch | "Armour down, arcs; few natural combos" | lightning |
| fire + cold | Opposed | "Burning and Chilled cancel. For experts: fire thaws a Frozen ally, cold puts out a Burning one" | cold |

Each pair × key is its own `Floor1ClassSpread` row (ARC-15). The draw
rule `drawRequires: offFamily` (every spell-card draw holds at least one
card whose variant family is the off-key family; family-free cards do
not count) and an alternating `recommendedPickOrder` keep both families
in use (ARC-21). `recommendedPickOrder` is data: per level, an ordered
list of `{templateId, family: key | offKey | none}`, where template
variants alternate between `offKey` and `key` in pick order, starting
with `offKey` at level 1.

### 7.4 Slots and Slot Renewal

| L | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|---|---|---|---|---|---|---|---|---|---|---|
| t1/t2/t3/t4 | 2 | 3 | 3/1 | 3/2 | 3/2/1 | 3/2/2 | 3/2/2/1 | 3/3/2/1 | 3/3/2/2 | 3/3/3/2 |

- One `camp` pool per tier **(settled, ability 6.5)**. Each tier pool is
  one limited ability (CL-2).
- **Every slot cast is the turn's one camp spend (settled, ability 6.4;
  CN-11):** never with a scroll read, a wand charge, Overchannel or
  Wellspring. Stated on the card.
- **Upcasts** only where the spell's data declares them, as options of
  the higher pool, checked at the higher tier's arrival level (SP-2).
- **Slot Renewal (settled):** when a short rest completes, regain one
  spent slot of tier ≤ 2, the highest such tier spent. Automatic.
- **Slot Renewal 2 (change ARC-12; class feature, `minClassLevel 9`):**
  from class level 9, two slots per short rest, one at a time by the
  same rule. Ability 6.5's "an offer may raise it to two" is excluded
  for the Arcanist (`offerExcludes: slotRecovery+1`). Level 9, not 6: at
  L6 the 60%-rule increments sit at 499–506‰ *(illustr.)*.
- **Reading (change ARC-3; lead's call):** Slot Renewal counts as
  **rest** power, in the camp share **and** in VG-12 parity
  (floor-averaged), never one without the other.

### 7.5 Weft (`pool.weft`, rest): "whittle, guard or mend?"

**Size (change ARC-11):** **3 at L1–4, 4 at L5–6, 5 at L7–8, 6 at
L9–10** *(illustr.)*. This replaces stage 1's illustrative 2 / 3 / 4.

`rankSchedule {core: [1, 5], other: [1, 8], signature: [3, 5, 7]}` (the
Vanguard's schedule). Whittle II at 5 is the core rank II; Flare II at 8
is the other rank II.

**Global cast rules (lead's call):**
1. Each option costs 1 Weft unless stated.
2. **One Whittle mode per cast at rank I**; rank II allows two
   *different* modes, 1 Weft each. **Temper and selective are mutually
   exclusive** on one cast (Temper replaces selective, 10.3).
3. **At most one radius or length step per cast from any source**
   (`RadiusStepsPerCast ≤ 1`): Whittle widen, the Shaping property and a
   `shape` offer never stack. No area exceeds `AreaCapByLevel`.
4. **At most one free Whittle mode per cast** (from Overchannel); **it
   counts toward rule 2's mode limit**, so with Whittle II one more,
   different mode may be paid (two modes in all) **(new lead's call;
   matches 7.9 and 11.6)**.
5. **At most 3 Weft on one cast.** "Weft on one cast" is every Weft
   spend declared with or triggered by that cast: Whittle modes, Cinch
   (a), Prime riding it (Wide Mix, `primeOnSpellAttack`, `primeOnGust`)
   and Deepen **(new lead's call)**.
6. An option on a cast that is then refused (greyed, blocked) refunds.
7. Whittle shapes only your class casts (slot spells, Overchannel, the
   Mote for load), never scroll or wand reads unless you own
   `sculptOnScroll`.
8. The tray shows each eligible toggle with its cost, greys the rest
   with a reason ("r2 is at the cap", "already flagged", "strips a pip
   only"), and updates the core preview live. It never highlights a
   recommendation (ability 11.3).
9. **Rest-tool reserve:** automatic spends (the Flare and Flare II
   policies, pre-ticked Whittle, pre-ticked Prime) never take Weft below
   the reserve, **reserve = 1 Weft while at least one Knit use is left
   this rest, else 0** (filter `reserveForRestTool`, ARC-6) **(new lead's
   call: one Weft, not one per remaining use, so the larger L6 sub-limit
   does not starve the wards)**. Exception: a pre-ticked selective that
   keeps an ally from being damaged or moved ignores the reserve, since friendly fire costs more than Knit's 3–12 HP
   **(new lead's call)**. Manual spends ignore it.

| Option | Wave, level | Cost | Rules text |
|---|---|---|---|
| **Whittle** (rank I) | M2, 1 | 1 Weft, declared with the cast; no action | "Choose one mode. **Selective:** allies are left out of this cast, every effect included (damage, conditions, riders, arcs, forks, forced movement); it may be taken on an area cast, or on any slot cast whose preview shows an arc or fork reaching an ally **(new lead's call; ARC-8, told plainly and confirmed by the client on 2026-11-20)**. **Widen:** +1 radius on a burst or aura, or +1 length on a cone or line, never past the area cap for your level (at L1–6 only an r1 burst, a cone up to L3 or a line can widen). **Load:** this Mote cast's damage part is rider-flagged; the at-will rider is still attempted at most once per turn; greyed if the part is already flagged." |
| **Whittle II** | M2, 5 | 1 Weft per mode | "Choose two different Whittle modes on one cast." Adds no dice (`RankAddsNoDice`) |
| **Flare** (rank I) | M2, 1 | Reaction, 1 Weft | "When an attack roll is made against you, before its result applies, gain +2 AC against that attack." Joins the armour 10.4 window at **step 3a** as the target's own class-pool-paid ward (ARC-5; `countsAsParry: false`), before step 3b (the Parrying property). Staff Parry (step 1, pool-free) is evaluated first. One reaction AC bonus per attack. Skipped after a gear-sourced disadvantage (Supple) |
| **Flare II** | M2, 8 | Reaction, 1 Weft | "Flare may instead be taken when you roll an **initial save** against a hostile ability, with the roll known: +2 to that save." Same reaction. Not on repeat saves, hazard-edge saves or concentration checks. At most one reaction save bonus per save. Needs the reaction trigger kind `ownSave` (ARC-19). The package's Insulate folded into a rank, so there is one save ward |
| **Knit** | M2, 1 | Bonus action, 1 Weft + its sub-limit | "Heal yourself FloorDiv(Recovery, 2). Your Medicine bonus does not apply." `restDefensiveTool`, `chassisOnly`, excluded from every offer. Sub-limit 1 per rest, 2 from L6 **(settled)** |
| Subclass option | M2, 3 | Per subclass | Binder **Cinch**; Alembic **Prime** (9). An option of Weft, never a pool |

**Stage-1 lever kept (settled):** Knit moves to its own `rest` pool of 1
(7 of 8, reading 4). **It and an offer row are never used together** (5
rows would follow): if the lever is used, `offerExcludes` gains every
offer that adds a row (ARC-21) **(lead's call)**.

### 7.6 Concentration **(settled, ability 9.5; restated)**

- One effect at a time; starting another ends the first; releasing is a
  free interaction.
- The check: d20 + Con against max(10, half the damage), once per damage
  event; one hit's parts summed; all turn-start ticks together are one
  event. Con +1: about 60% at DC 10; staff Steady Casting about 84%
  *(illustr.)*.
- Ends when you are Downed, Incapacitated, Unfocused (psychic rider), or
  its duration ends. Flare and Knit never end it.
- **Only sustained control concentrates** on this list: Grasping Weeds,
  Dragfoot, Cordon (`ConcentrationOnlyControl`) **(lead's call)**. No
  damage spell concentrates.
- Proficiency on the check comes only from an offer (concentration
  training, `spellcraft` lane). No class feature at launch, which keeps
  "concentration under focus fire" a named weakness **(lead's call)**.
- **Afterhold** (a trigger offer, 10.3) is the only handoff in the base
  class; the Binder has Belay instead and is barred from Afterhold.
- The preview shows your concentration odds on every hit against you and
  "ends your Grasping Weeds" on any cast that would start another.

### 7.7 Creation picks and the published ladder (classes 7.4, caster column)

The per-level automatic grants and choices are in the level table (5.5)
**(lead's call)**.

- **Spells known:** 2 / 3 / 4 / 4 / 5 / 5 / 6 / 6 / 6 / 7, each one less
  from L7 if the L7 trigger option is taken, and one less at L10 if the
  offer is taken.
- **Draw (change ARC-21, classes 7.4 spell-card-draw bullet):** 3
  distinct cards by `drawWeight`, excluding known spells and tiers above
  your highest slot **(settled)**. Cards are variants of the pair's
  families or family-free spells (7.3). A template counts as known once
  any variant of it is known.
  - **Hard constraints (never relaxed):** distinct; not known; tier ≤
    your highest slot; at most one variant per template (SP-4).
  - **Soft constraints:** (s1) at least one card whose variant family is
    the off-key family (`drawRequires: offFamily`); (s2) at least one
    card carrying the static Recommended badge; (s3) no Expert card in
    the level-2 draw; (s4) at L10, one tier-4 family signature of the
    pair while one is unknown.
  - **Relaxation order (new lead's call):** the deterministic draw
    function first tries every soft constraint; when no draw satisfies
    them all, it drops s2, then s1, then s3, then s4, one at a time,
    until a draw exists. The draw records which constraint it relaxed.
  - **`DrawSatisfiable`** (build-time check; exhaustive over pair × key ×
    level × every reachable known set, with a property test as the
    fallback if the space grows): the hard constraints always leave ≥ 3
    cards (8.6), and the report lists every case in which a soft
    constraint was relaxed, and which.
  - Every tier-1 card eligible for the L2 draw has a damage part or a
    major condition (content check `L2DrawPlaysAlone`).
- Each card shows the core's computed "what changes", the
  pair-interaction line and, from static data, a Recommended or Expert
  badge.
- **Starter rule and its check (new lead's call):** at creation, the
  confirm button is disabled, with the reason shown ("pick a spell that
  deals damage"), while neither picked starter has a damage part; a
  no-damage start is a dead pick, so this is a creation rule, not a
  warning. The build-failing content check **`StarterDefaultsHaveAnswer`**
  holds every `pairDefaults` entry to the same rule. ARC-15's printed
  starter rows list only legal combinations.
- **Creation pick for `Floor1ClassSpread` (change ARC-15; told plainly,
  accepted with Q1):** for the Arcanist a creation pick is pair × Mote
  key (12 picks), crossed with the level-3 subclass as classes 5.6
  already does; starters held at `pairDefaults`; every legal starter
  combination is printed as its own row, outside the mean, with the
  Opposed rows printed first. The class mean is the **unweighted** mean
  over these picks, as classes 5.6 reads; pick popularity is never a
  weight **(new lead's call)**.

### 7.8 Turn allowances and frequency limits (count 0; VG-10, SH-4)

`movedByYou` (Gust, Reel; per target per round); the Interplay mark (one
held); subclass allowances `cinch`, `rebind`, `prime`, `runoff`;
per-target round counts (Cinch III); source-held sets `slippedFrom`,
`boundBy`, `cinched`, `primedBy` / `origin: prime`, `runoffThisRound`.
`TurnAllowanceNoDamage` holds: none carries a `damage.*` kind. **No
capacity or timescale offer ever targets a turn allowance (ARC-26, ALM-6
generalised).**

### 7.9 Capstone: Overchannel (`pool.overchannel`, camp 1, `minClassLevel 9`; on the spellbook row)

> **Overchannel.** Cast a known slot spell of tier 1–3 (not an
> at-will) without spending a slot. This uses the turn's camp spend, so
> it never shares a turn with a slot cast, a scroll read, a wand charge
> or Wellspring. It
> casts at the spell's minimum tier (no upcast) and its own action cost.
> It takes **one Whittle mode free** (no Weft), still inside the area
> cap; the free mode counts toward Whittle's mode limit, so a second,
> different mode may be paid under Whittle II. Your subclass's
> Overchannel rider applies. **Fused:** while Interplay is enabled, the
> cast's pair-family damage parts count as both pair families for
> Interplay: a Fused cast cashes a held mark of either family and never
> sets a mark. Family-free parts and family-free spells gain nothing.

- **Stage-1 text kept (settled, stage 1 8.5, R-19):** tier ≤ 3, one free
  Whittle. The computed brief's "tier 1 or 2" is narrower than the
  approved text and nothing needs it; drafts that gave two free modes
  are rejected (nova, 11.6).
- **Acceptance criterion (classes 14):** it changes how the primary
  leans work. From L9 a widened, selective tier-3 area (r3 is legal from
  L7 under ARC-9) is one free shaping decision instead of a
  friendly-fire trade; Fused makes the pair "one hand" for a cast.
- **Bounds:** `SlotlessCastBound` (every slot spell record of tier 1–3,
  evaluated by `boundModel` at L9 with the best free Whittle mode plus
  one paid mode under `AreaCapByLevel`, against the `SpellTierBound`
  lines; no upcast; the camp spend; exempt from `CampOptionBound`'s
  damage clause). Nova ≤ 2.0 × p95 non-capstone turn; the p95
  denominator is computed under the same setup reading as the numerator
  (11.6).
- **Registry:** against Wellspring (`focus_major`, slotless tier 1):
  same kind, never stack (one camp spend per turn), stated on both
  cards.

### 7.10 Pools by level *(illustr.)*

| Pool | L1 | L2 | L3 | L4 | L5 | L6 | L7 | L8 | L9 | L10 |
|---|---|---|---|---|---|---|---|---|---|---|
| Slots (camp) | 2 | 3 | 3/1 | 3/2 | 3/2/1 | 3/2/2 | 3/2/2/1 | 3/3/2/1 | 3/3/2/2 | 3/3/3/2 |
| Slot Renewal (per short rest) | 1 | 1 | 1 | 1 | 1 | 1 | 1 | 1 | **2** | 2 |
| Weft (rest) | 3 | 3 | 3 | 3 | 4 | 4 | 5 | 5 | 6 | 6 |
| Knit sub-limit **(settled)** | 1 | 1 | 1 | 1 | 1 | 2 | 2 | 2 | 2 | 2 |
| Overchannel (camp) | — | — | — | — | — | — | — | — | 1 | 1 |
| Mote | 1d10+Int | | | | 2 beams | | | | | |
| Whittle / Flare rank | I / I | | | | II / I | | | II / II | | |

### 7.11 Starting kit *(illustr.; 2 potions settled)*

- **Leather:** AC 12 + Dex; Supple (opportunity attacks against you have
  disadvantage).
- **Set A: staff**, held two-handed: Steady Casting (advantage on
  concentration checks), the Parry verb (pool-free +2 against melee,
  step 1), 1d6 / v1d8 bludgeoning.
- **Set B: dagger** (thrown, piercing fallback).
- **Belt:** 2 healing potions **(settled)**. **Exactly 15 coin**
  (`kit.coinStart`; starting kits design 4.2, SK-1, M1).
- No creation kit choice (the pair is the level-1 pick). Orb, rod and
  tome are loot or vendor items. The rod's verb now has a target (Gust).
- The kit's record and issue, its named piece (the leather), kit lines,
  Oddment pool, `kit.acFloor`, the set-B dagger as the declared
  fallback-only set, the pack-pressure option and the gated M3 variants
  are set by the starting kits design (4.1, 4.2, 4.6, 5.1, 5.3; approved
  2026-11-23).

### 7.12 Caster AC (armour 11 item 2 reading)

Bands as armour 6.3 (maximum Dexterity +3 at L1–3, +4 at L4–7, +5 at
L8–10, armour 11 item 1; the reference steps at L5 and L9):

| Build | L1–3 | L4 | L5–7 | L8 | L9–10 |
|---|---|---|---|---|---|
| Leather, Dex +2 (suggested array) | 14 | 14 | 14 | 14 | 14 |
| Reference (enemies 6.1) | 15 | 15 | 16 | 16 | 17 |
| Worst legal Dex (−1) | 11 | 11 | 11 | 11 | 11 |
| Maximum sustained (Dex +3 / +4 / +5) | 15 | 16 | 16 | 17 | 17 |
| Maximum charged (+ Flare or Parry, one only) | 17 | 18 | 18 | 19 | 19 |

- **Item 2 is not triggered** (it binds ceilings below light) **(settled
  reading, CL-13e)**.
- **AR-20 is already 11 (settled, armour 11 item 2; armour
  cross-reference table "11, AR-20"):** the bound reads the worst
  Dexterity of the suggested array. The Arcanist at Dex −1 equals the
  Shade's 11, so it **adds no new AR-20 burden and triggers no guard
  rerun**. AC 14 is the Arcanist's own kit AC, used only for sensitivity
  reports (11.7).
- **Envelope (armour 11 item 1):** hit chance against maximum sustained
  45–65% (row ≥ 35%), charged 35–55% (≥ 15%), with gear disadvantage
  20–42% (≥ 10%) *(illustr.; the L4 and L8 columns lower each band's
  floor by about 5 points; rerun owed at these bands)*. Pass.

### 7.13 Offers, lanes and `triggerMenu`

**`offerLanes`** *(illustr.)*:

| Lane | Weight | Carries |
|---|---|---|
| `sculpt` | 1500‰ | `shape` offers on a named spell (permanent +1 radius or `selective` tag; 7.5 rule 3 applies); `capacity:weft` (at most 1 per run); Whittle and Flare triggers |
| `spellcraft` | 1300‰ | Concentration training; `cost` (a tier-1 spell becomes a bonus action once per rest; still a camp spend); `rider` offers on a spell's damage part; spellbook and Overchannel triggers; ignore resistance in one pair family, once per run (EL-1) |
| `scholar` | 1200‰ | Arcana, Lore, Perception and Mechanics mastery (Tactician reachable as the Perception mastery, never copied); Medicine training |

- **`offerLaneAdjust` ×1333‰:** Binder on `spellcraft`; Alembic on
  `sculpt`; Loremaster on `scholar`; Elementalist on `spellcraft`.
- **`offerExcludes`:** capacity and cost on Knit and its sub-limit;
  `capacity:overchannel`, `timescale:overchannel`; `timescale:slots.*`
  (a rest-refilled tier would be a second Slot Renewal);
  `slotRecovery+1` (ARC-12); capacity and timescale on any turn
  allowance (ARC-26); the ignore-resistance offer while the Elementalist
  owns Purity; armour training past light; shield training; if the Knit
  lever is used, every offer that adds a row.

**`triggerMenu`** (never repeated; ids *illustr.*; subclass entries
carry `requires`):

| Ability | Trigger id | Text |
|---|---|---|
| mote | `moteFray` | **Fray.** When your Mote hits a concentrating hostile, that hit's concentration check is made with disadvantage. Once per turn |
| mote | `moteVsHeld` | Your Mote's damage part is rider-flagged against a Restrained or Prone target (the at-will rider bound applies) |
| gust | `gustPull` | Gust may pull 1 toward you instead of pushing |
| sculpt | `sculptOnScroll` | Whittle may shape your in-class scroll and wand reads |
| sculpt | `sculptTemper` | **Temper**, a Whittle mode (10.3) |
| spellbook | `afterhold` | **Afterhold** (10.3); `requires: not subclass:binder` |
| spellbook | `riderOnUpcast` | A spell cast above its minimum tier has its first damage event rider-flagged |
| Binder, Alembic | 9.1, 9.2 | Subclass entries |

Barred: `concentrationHandoff` (superseded by Afterhold and Belay), any
off-turn Weft spend, any trigger that lengthens a stop or hard instance.

### 7.14 Default reaction policies, cast-option defaults and creation defaults

**Default reaction policies (ability 7.3):**

| Reaction | Default |
|---|---|
| Staff Parry | `ifItChangesOutcome`, `nonMinion` (step 1; fires before Flare) |
| Flare | `ifItChangesOutcome`, filters `nonMinion`, `bigHit` (existing, SH-7) and `reserveForRestTool` (new, ARC-6) |
| Flare II (save form) | `ifItChangesOutcome`, filters `nonMinion` (the source) and `reserveForRestTool`; fires on `bigHit` (read on the expected failed-save damage) or a major or hard condition on a failure |
| Opportunity attack | `never` for the Arcanist hero (keeps the reaction for wards), changeable |

`reserveForRestTool` is the one new filter: `whilePoolAtLeastHalf` is a
policy and cannot sit beside `ifItChangesOutcome`, so it cannot do this
job. Its reserve is defined in 7.5 rule 9. On screen the policies print
their labels (naming pass 5.9.14: Always / Only if it matters / While
half full / Never / Ask me); the ids above never show.

**Knit has no automatic policy for the hero** (a bonus action on your
own turn is not a reaction, and an auto-cast would act for the player);
the hero casts it by hand. The simulator arms play it through
`defaultRotation`'s bonus-action rule (5.2) **(new lead's call)**.

**Cast-option defaults (`castDefaults`; change ARC-6, a new policy class
beside reaction policies; told plainly as a reading of agreed ability
11.3, accepted with Q1):** visible defaults on the hero's own cast,
shown ticked (the player can untick any of them), computed from
**static data only** (target rank, pips, an ally affected by the cast,
Weft above the reserve, party kit types and known spells), never from
scorer terms:
- Whittle selective is pre-ticked whenever the preview shows an ally
  affected by the cast: inside its area, reached by an arc or fork, or
  moved by it. It ignores the reserve in that case (7.5
  rule 9).
- No major-control spell, Cinch or control-purpose Prime is pre-ticked
  on a unit with resolve pips left; the preview says "strips a pip
  only".
- DH 17.7's lethal friendly-fire confirmation stays, its lethal test
  unchanged. Separately, a cast that would move an ally into a hazard
  square or over a hazard edge asks for confirmation whether or not it
  could kill (ARC-25; DH 10.4 is the related hazard section); the
  confirmation extends to contact hazards, harm squares and Keg
  footprints (room terrain design 6.12, TR-23). A
  non-lethal friendly-fire line ("hits Bram (ally) for about 9") is
  always in the preview (AM-1 line 1).
- The last-slot confirmation (5.2; ARC-13).
- The Alembic's `castDefaults.prime` (9.2; ALM-8).

**Creation defaults** (`pairDefaults`, `recommendedPickOrder`; static
data) **(lead's call)**:

| Choice | Default |
|---|---|
| Pair / key | Weatherwork / cold; other pairs per 7.3 |
| Starters | the pair's off-key Shard + Upheaval (Opposed: **Needles (fire)** + Upheaval, key cold: Needles sets the Interplay mark without leaving Burning, so the cold Mote's Chilled lands instead of cancelling) **(new lead's call)** |
| L2 card order | Snaring → Brand (key) → Needles (off-key) → Grasping Weeds → Fan (key); Opposed: Snaring → Brand (key) → Shard (off-key) → Grasping Weeds → Fan (key) |
| L3 | Alembic (9.1 says when this may change) |
| L5 | the pair's Detonation in the next family of the alternation (pairs with lightning: Streak (lightning)) |
| L6 | Binder Rebind; Alembic Wide Mix |
| L7 | Highfall or Dragfoot; Expert badge on the pair's signature |
| L9 | the offer draw (the scorer's offer value is already shown, settled) |
| L10 | the tier-4 signature card |

Creation warnings: below Dex +2 ("AC 13 or less: much more likely to die
on floor 1") and below Con +1 ("concentration and HP thin") **(lead's
call; the Dex wording new lead's call)**. Warnings never block; the
starter rule (7.7) is a rule, not a warning. **M2:** the kit warnings
extend these: a kit item Strained, or AC below the kit's `kit.acFloor`,
in one line, never blocking (starting kits design 4.6, SK-9).

### 7.15 `effectKinds` for the registry

| Feature | effectKinds | Subject / trigger |
|---|---|---|
| Mote | `damage.spell.family`, `rider.weakOrCrit`, `scales.beams` | target / spell attack |
| Gust | `forcedMovement.push{verbSourced, sourceCount}` | first hostile in line / Fortitude save |
| Interplay | `rider.flag.next` (source-held mark) | marked hostile / the other family's hit |
| Whittle | `area.selective`, `area.radius+1`, `rider.flag.cantrip` | own cast / declared with it |
| Temper | `area.allyEndsOnly` | allies in your area / cast |
| Flare / Flare II | `reactionAc.self+2`, `reactionSave.self+2` | self / attack roll (step 3a), own initial save |
| Knit | `heal.recovery.half` | self / bonus action |
| Slot Renewal 2 | `slotRecovery.count` | self / rest completed |
| Overchannel | `cast.slotless.camp`, `area.selective` / `area.radius+1` (one free) | self / camp spend |
| Fray | `check.disadvantage:concentration` | hit hostile / Mote hit |
| Afterhold | `condition.linger.onSwap` | own sustained instances / voluntary swap |

Binder and Alembic features, ranks and Arts have their own rows in 9.1
and 9.2.

**Registry pass, named first (classes 14 names the Alembic's priming
first):** **Prime against the Water and Oil Flasks** (lead's ruling):
Prime applies Soaked or Oiled (minor, `turns:3`) as the rider of your
own Mote hit (or, by 9.2's named routes, your own slot cast), for 1 Weft
and the round's `prime` allowance, inert to your own at-wills; the
flasks are thrown belt items, one offensive flask per turn, using a belt
slot and no class pool. Same effect kind, different trigger, cost and
subject; **passes**. Both may be used in one turn (a flask is not a
Weft spend); Prime's rider gate keeps a target from taking a reagent it
already holds. The dead-slot watch (the flasks' payout in Alembic
parties, 9.2 reports) stays. Then: Whittle widen against Shaping
(`focus_minor`; never stack, 7.5 rule 3) and the rod's Shape (no overlap
after the rename); Whittle load against Conduit (greyed when flagged);
Temper against the tonic and the Water Flask (different trigger and
subject); Flare against Parry, Interpose, Guarding, Sworn Guard, Raise,
Stand Fast (window order); Flare II against Enclosed and Tumble Clear;
Knit against Patch Up, Respite, Take Comfort, Hungering, Stubborn
Breath; Overchannel against Wellspring; Mote against the Ember and Frost
Rings and Attuned; Gust against Shove, Bowl Over and Blowback Jar
(Later); Fray against the psychic rider (stronger; the Herald lane keeps
it).

## 8. The full spell list

**(lead's call throughout, inside the settled rules named)**

### 8.1 The contract every spell obeys

- **Cost and pool:** an action unless stated; one slot of its tier; the
  turn's camp spend. Tradition **arcane** for every entry; the
  consumables 16 [primal] and [divine] notes are hints only (CL-12a).
- **Areas:** ≤ 25 squares at L1–6 **(settled, ability 8.1)**; ≤ 49 at
  L7–10 (ARC-9). Not selective unless Whittled. Chosen-target spells
  (Dragfoot, Cordon, Rays, Needles, forks) never choose allies; an arc
  from any slot spell's hit on a Soaked target still reaches adjacent
  allies unless the cast is selective (Whittle allows selective on such a
  cast, 7.5).
- **You and your own casts (new lead's call; part of ARC-8, told
  plainly and confirmed by the client on 2026-11-20, 20.1):** you are never affected
  by your own casts' areas, arcs and forks (damage, conditions, riders,
  forced movement), as Upheaval already states; Tincture is the one way
  to Soak yourself. The preview and the friendly-fire lines count you as
  unaffected.
- **Never:** ground zones (terrain changes only through the one
  consolidated list of sanctioned map changes, room terrain design TR-8:
  the settled fire on oil; Furnishing debris and removal, boss phase
  sockets, and web and bedsheet burn-away (M2); Overturn debris and
  sight change, Remnants, and pre-proven Stir states (M3; HZ-18;
  cross-document comparison 2026-11-23, CC-77)); teleport; summons; AC on anyone; temporary HP; healing (Knit
  stays the only heal); psychic, radiant or necrotic parts; fear or
  morale (the Herald's primary lean, `LeanSpread`); Shocked or any Later
  condition **(settled)**.
- **Hard control only from slots:** Arrest (Stunned); `freezing` on Rime
  Lance and Deep Freeze (Frozen). Steeled, the per-fight cap of 2 lost
  turn starts, composite lockout, resolve pips and Unyielding apply.
  **Depth gates do not apply to hero hard control** (they are
  monster-against-party, conditions 7.2); earlier drafts' claim is
  removed.
- **Riders:** at most one per target per use (ability 9.3 order).
  Rider-flagged parts attempt the family rider; weak targets take it
  automatically. The conditions 19 cancel, override and damage-end rows
  always apply first: a rider they suppress is not attempted (fire on
  Soaked attempts no Burning; Corroded on a Soaked target is washed
  off).
- **The ‰ samples** in 8.2–8.5 are `boundModel` values (11.2; tuning
  file, 14.1): each
  damage part judged against its own line (single target 2000‰, area
  1250‰ per target) at the tier's arrival level.
- **Lightning arc (change ARC-14, stat 8.1):** the arc deals the
  family's rider strength (band 2 / 4 / 6 *(illustr.)*) as lightning,
  one damage event per arced unit, no crit, no rider flag, never
  re-arcs. The caster picks the arced unit for an ordinary arc (the
  scorer for companions); on a Soaked target it reaches every adjacent
  unit. At-will arcs skip allies (ARC-8); a selective cast leaves allies
  out of its arcs and forks (SP-6) (ARC-8 told plainly and confirmed by
  the client on 2026-11-20, 20.1).
- **Upcasts (SP-2):** checked at the higher tier's arrival level.
  Because L1 and L3 share a reference (7.9 dpr) and L5 and L7 share one
  (17.3), area upcasts skip the flat tier. Validator
  **`AreaUpcastSkipsFlatTier`** **(new lead's call)**: for any record
  with an area damage part, `upcasts[]` holds no entry at tier 2 that
  adds area dice over the base, and no entry at tier 4 that adds area
  dice over the record's tier-3 entry (or over the base, for a tier-3
  record). Non-damage upcasts (Snaring's second target, Arrest's second
  target) and single-target parts are outside it. Scrolls, wands and
  Overchannel never upcast (CN-3).
- **Names (SP-3, ARC-20):** display name = template + family tag, e.g.
  "Fan (cold)". A variant that is an existing scroll or wand keeps that
  name (classes 10 exception, extended to wand spells). Variants of one
  template are one spell family in data, exempt from the head-word
  warning. `VariantIsScroll`: a scroll- or wand-named variant has the
  item's resolve data exactly in the compared fields: base-tier damage
  dice, shape and size, range, save, rider flag, conditions applied and
  the `freezing` gate. Not compared: `upcasts[]`, the attack and
  difficulty source (the caster's, not the item's), Whittle
  eligibility (SP-7) **(new lead's call)**, the `faltering` field and
  whether the caster may be a target (HR-25; Herald design 8.1, 8.8).
- **Durations** ≤ 10 turns. **Conditions used:** Burning, Smouldering,
  Chilled, Frozen, Soaked, Oiled (payout), Corroded, Restrained,
  Stunned, Dazed, Blinded, Revealed, Prone, Teetering, Staggered; engine
  states Concentrating and Steeled. **0 new; the budget of 32
  untouched.**
- **Wave:** every entry below is **M2**.

| Family | Rider | Ends | Ladder notes |
|---|---|---|---|
| Fire | Burning (spreads as Smouldering) | Chilled, Frozen, Soaked (that hit attempts no Burning), Oiled (pays out +half, Burning one band higher) | Soaked targets resist fire; any fire area or fire damage on an oil square turns it to plain floor (the settled terrain row, conditions 19), for every fire variant |
| Cold | Chilled | Burning, Smouldering | Soaked → weak, Chilled automatic; `freezing` → Frozen |
| Lightning | The arc (above) | — | Soaked → weak, arc to every adjacent unit |
| Acid | Corroded (AC −N; strips one layer) | Frozen | Soaked washes Corroded off |

### 8.2 Tier 1 (from L1; reference 7.9)

Legend: **[F]** family template; ‰ = `SpellTierBound` sample
*(illustr.)*; ★ Recommended; **Exp** Expert badge (low weight, never in
the L2 draw); W = `drawWeight` *(illustr.)*.

- **Fan [F]** ★ (Burning Hands shape). Cone L2 from you, 8 squares.
  Reflex: 3d6 family, half on success; rider-flagged. Upcast 5d6 at tier
  3 (no tier-4 step, `AreaUpcastSkipsFlatTier`). Fire = **Fan of
  Cinders** (pays out Oiled). 997‰. W 1000.
- **Shard [F]** ★ (Ice Knife shape; the default starter in the off-key
  family). Spell attack range 8: 1d10 piercing; hit or miss, burst r1 on
  the target (9 squares) 2d6 family, Reflex half, rider-flagged, not
  selective. Upcast tier 3: burst 4d6 *(illustr.)*, checked at L5 (the
  tier-2 burst step is removed by `AreaUpcastSkipsFlatTier`). Cold =
  **Frost Shard**. Primary 1152‰, burst 665‰. W 1200.
- **Brand [F]** ★ (Chromatic Orb shape, one type; the boss tool). Spell
  attack range 10: 3d8 family, rider-flagged. Upcast +1d8 per tier.
  1196‰. W 1200. Minors never spend pips.
- **Needles [F]** ★ (Magic Missile shape, no force). 3 / 4 / 5 needles
  by band, 1d4+1 family each, split among visible enemies within 12; no
  roll, no save, no rider flag; each its own damage event (breaks
  concentration); a weak target still takes one automatic rider. Upcast
  +1 needle per tier. Lightning = **Sparks** (the wand). 1329‰. W 1100.
- **Upheaval** ★ (Thunderwave shape; family-free; the scroll). Burst r1
  on you (you unaffected), not selective. Fortitude: 2d8 bludgeoning,
  half on success; on a failure pushed 2 away (non-verb: collisions,
  hazard edges, Teetering; each target its own hit for the 3-square sum,
  SP-5). Upcast 4d8 at tier 3. 854‰. W 1000.
- **Snaring** ★ (family-free; the wand's spell). One enemy within 8,
  Reflex or Restrained (cords; breakers slashing and fire) `turns:2`; no
  concentration. Upcast tier 2: one more enemy within 8. W 1000.
- **Grasping Weeds** (Entangle on units; family-free; the scroll;
  **concentration**). Burst r1 within 8, not selective. Reflex or
  Restrained (weeds; breakers slashing and fire; Escape at the recorded
  difficulty), sustained max 3. W 800.
- **Drench** **Exp** (family-free; new). Burst r1 within 8, not
  selective. Reflex: **2d4 bludgeoning**, half on success; on a failure
  Soaked `turns:3`. A willing ally may skip the save (Soaked, no
  damage), which puts out Burning and Smouldering. The bludgeoning part
  gives it standalone value. About 500‰ *(illustr.)*. W 600 (×2 for
  Weatherwork, ×0.5 with fire in the pair). Registry: against the Water
  Flask (a slot area, not a throw; passes).
- **Outlining** **Exp** (Faerie Fire shape; family-free; the wand's
  spell). Burst r1 within 12, not selective. Reflex or Revealed 2 turns
  (reveals hidden allies too). W 400.

### 8.3 Tier 2 (from L3; reference 7.9)

- **Spatter [F]** ★ (the ability 14.1 ember_burst shape). Burst r1
  within 8, 9 squares. Reflex: **3d6** family, half; rider-flagged.
  Widen to r2 legal from L3. Upcast 5d6 at tier 3 (no tier-4 step). 997‰
  (3d8 would be 1282‰, a fail). W 1200; ranked last for the Alembic
  (Catalyse supersedes it).
- **Rays [F]** (Scorching Ray shape). Three spell attacks within 8, 2d6
  family each, each its own damage event; one rider attempt per target
  per use; the first damage event on an Oiled target pays it out. Upcast
  +1 ray per tier. Fire = **Rays (fire)**: the template name, no override (naming pass N13). 1861‰. W 1000.
- **Lance [F]** (the Rime Lance wand shape). Spell attack range 10: 3d8
  family, rider-flagged. Cold = **Rime Lance**: `freezing` only on a
  Soaked target (Soaked + Chilled → Frozen, fallback Chilled; Steeled
  and resolve apply). Upcast tier 3 +1d8. 1196‰, measured as a
  single-target spell under the SP-1 reading (the hard clause covers
  unconditional hard control only). W 900.
- **Blinding Flash** (Blindness shape; family-free; the scroll). One
  enemy within 8, Fortitude or Blinded `saveEnds {Fortitude, cap 2}`; no
  concentration. W 800.
- **Arrest** (Hold shape on M2 conditions; family-free; new;
  unconditional hard). One enemy within 8, Will or Stunned `turns:1`
  (own-turn rule); Steeled follows; bosses spend a pip, then Unyielding
  downgrades to Dazed. 0‰. Upcast tier 4: a second enemy within 2 of the
  first, each its own save. W 900.
- **Squall** **Exp** (Gust of Wind on units; family-free; new). Line L6
  from you, not selective. Fortitude: 2d6 bludgeoning, half; on a
  failure pushed 3 away (non-verb; hazards; SP-5). Shatters Frozen.
  665‰. W 400.
- **Subclass tier-2 spells** (never drawn, never swapped): Binder
  **Shackle**, Alembic **Catalyse** (9).

### 8.4 Tier 3 (from L5; reference 17.3)

- **Detonation [F]** ★ (Fireball shape). Burst r2 within 12, 25 squares.
  Reflex: 8d6 family, half; rider-flagged. Fire = **Detonation (fire)**: the template name, no override (naming pass N13) (like
  every fire area, it clears oil squares, 8.1). No upcast; no widen
  before L7. 1214‰. W 1300.
- **Streak [F]** ★ (Lightning Bolt shape; a full template,
  `spellTemplate.families` all four, so every pair has it) **(new lead's
  call)**. Line L10, stops at the first wall. Reflex: 8d6 family, half.
  **Not rider-flagged** in any variant (as the scroll, SP-7); weak
  targets take their automatic rider. Lightning = **Lightning Line**
  (the `variantOverrides` name). 1214‰. W 1000.
- **Dragfoot** (Slow shape; family-free; the scroll; **concentration**).
  Up to 3 enemies you choose in a burst r2 within 12. Will or Slowed
  (Chilled + Dazed, each `[{sustained: {max 3}}, {saveEnds: {Will, cap
  3}}]`). W 1000.
- **Unweaving** (Dispel Magic shape; family-free; the scroll). As the
  scroll. W 500.
- **Undertow** **Exp** (family-free; new). Cone L3 from you, 15 squares.
  Fortitude: 5d6 bludgeoning, half; on a failure Prone (`shove`,
  `sharesToppleCount`; Anchored blocks) and Soaked. Not rider-flagged.
  Preview warns: Prone gives your own Motes disadvantage. 759‰. W 400.

### 8.5 Tier 4 (from L7; reference 17.3)

- **Highfall [F]** ★ (Ice Storm shape). Burst r2 within 12. Reflex: 2d8
  bludgeoning + 4d6 family, half; the family part rider-flagged; the
  bludgeoning shatters Frozen. Cold = **Icefall**. 997‰. W 1200.
- **Deluge [F]** (Cone of Cold shape). Cone L4 from you, 24 squares.
  Reflex: 6d8 family, half; rider-flagged. 1171‰. W 1000.
- **Family signatures** (one record per family; the draw offers the
  pair's two; the L10 draw guarantees one):
  - **Deep Freeze** (cold). One creature within 10. Fortitude: 5d6 cold,
    half; rider-flagged; on a failed save a Chilled or Soaked target
    becomes Frozen (fallback Chilled; Steeled, pips, Unyielding →
    Chilled). 759‰. W 900.
  - **Pyre** (fire). One creature within 10. Reflex: 8d6 fire, half;
    rider-flagged with `worsen` (Burning applied or one band higher, max
    3; never past 3 with the Oiled bump). 1214‰. W 900.
  - **Forked Bolt** (Chain Lightning, scaled). One creature within 10,
    Reflex 6d8, half; then up to 2 other enemies you choose within 3 of
    it, 3d8 each, Reflex half; forks never fork and never pick allies;
    only the first target rider-flagged. 1171‰ primary, 585‰ per fork. W
    900.
  - **Dissolve** (acid; Disintegrate, scaled, no dust). Spell attack
    range 10: 8d8 acid, rider-flagged with `worsen` (Corroded one band
    higher, max 3); strips up to 2 unbroken layers not acidproof. 1457‰.
    W 900.
- **Cordon** (family-free; new; **concentration**). Up to 3 enemies
  within 10 you choose; each Reflex or Restrained (arcane chains;
  breaker acid), sustained max 3. W 800.

### 8.6 Summary per pair

| Tier | Templates | Family-free | Cards per pair | Concentration | Hard |
|---|---|---|---|---|---|
| 1 | Fan, Shard, Brand, Needles | Upheaval, Snaring, Grasping Weeds, Drench, Outlining | 9 | Grasping Weeds | — |
| 2 | Spatter, Rays, Lance | Blinding Flash, Arrest, Squall (+ subclass spell) | 6 | — | Arrest; Rime Lance (conditional) |
| 3 | Detonation, Streak | Dragfoot, Unweaving, Undertow | 5 | Dragfoot | — |
| 4 | Highfall, Deluge, 2 pair signatures | Cordon | 5 | Cordon | Deep Freeze (conditional) |

Data: about 60 records generated at build time from templates
(`spellTemplate.families[]`, `variantOverrides` for scroll and wand
names and Rime Lance's `freezing`; oil-square clearing is the
family-level terrain row, not an override), plus the subclass spells.
"Cards per pair" counts templates once (one variant of a template is
known at most); the hard draw constraints always leave ≥ 3 eligible
cards, checked by `DrawSatisfiable` (7.7).

### 8.7 Where the creative uses come from

| Setup | Payoff | Notes |
|---|---|---|
| Drench, Undertow (Soaked) | Streak, Spatter, Needles, a Mote in cold or lightning | Weak: +half, automatic rider; lightning arcs to every adjacent unit (selective spares allies) |
| Soaked or Chilled | Rime Lance, Deep Freeze, then an ally's bludgeoning | Fracturing: +half, automatic Staggered. A party combo (one camp spend per turn) |
| Snaring, Grasping Weeds, Cordon | Any Reflex area | Fire frees weeds and cords; acid frees Cordon's chains (the preview says so) |
| Arrest (Stunned) | Detonation, Spatter | Stunned auto-fails Reflex |
| Upheaval, Squall, Gust next to fixed squares | Water, web, fire, oil | Hazard-edge save; Teetering → next save at disadvantage |
| Oil squares, flasks | Rays (first event), Pyre, Detonation (fire) | Every fire area clears oil squares (the settled terrain row) |
| Fire or cold on allies, through Temper | Thaw a Frozen ally; put out a Burning one | No damage, no payout (10.3) |
| Interplay | "Spell, then Mote" lands both families' riders | 10.3 |

### 8.8 Later reserves

Drowse (Asleep), Hushing (Silenced), Arrest's Held form (the Binder's natural
deepener; Holding merged into Arrest), Cast Out (Banished), Flicker Step and Far Step (a
teleport rule), Elemental Endurance (Resistant (type)), a brittle hex
(Exposed), a Shocked lightning spell (Shocked), a self aura (the ability
8.4 aura ruling and consumables 16.7 wording; AM-6), Quench (a
cleanse-lean ruling), readied casts (non-goal).

**Pending request (exploration and information design XI-27,
2026-11-23; M3; the Arcanist owner rules):** **Walls Have Ears**, a
batched Listen: out of combat, 1 click, 4 ticks *(illustr.)*, no slot
(owner's call); listens at every closed, unlistened door of the current
space at once, each counting as that door's once-per-floor Listen;
trained detail only if a Perception-trained member is in the party;
never through Hairlines or the Bossgate; never Hidden units; results as
Listen bubbles (exploration and information design 4.5.2, 4.8.1). The
request calls it a ritual; the owner rules it against the "rituals"
anti-goal (2). If refused, no batched Listen (exploration and
information design 14.2). Not decided here until the owner rules.

### 8.9 Concentration, control and lockout audit

- Concentration: Grasping Weeds, Dragfoot, Cordon. A three-way choice.
- No combination of Arcanist spells completes a composite lockout (Dazed
  is `oneOf`; Restrained and Blinded deny nothing).
- **Tests owed:** Dragfoot's Dazed + an ally's Later Distracted or
  Nauseated; Arrest + Dragfoot (hard exclusivity); Arrest + Cinch or Weak
  Point against the per-fight cap (1.75 expected lost turn starts
  against 2 *(illustr.)*); Rime Lance or Deep Freeze on a Steeled
  target; a Belayed Restrained + Dragfoot on the same target.
- Bosses: Restrained, Blinded, Dazed, Stunned, Frozen spend pips (ARC-17
  puts Restrained at or above Dazed's rank); Chilled, Burning, Corroded,
  Soaked, Prone, Revealed land normally. Intended play: minors and Brand
  on the boss, majors on the escort.

### 8.10 `classTags` audit (consumables 16.4, 16.5; CL-12a; change ARC-24)

| Item | Tier | classTags | Arcanist record or reason |
|---|---|---|---|
| Fan of Cinders | 1 | arcanist | Fan (fire) |
| Upheaval | 1 | arcanist | Upheaval |
| Grasping Weeds | 1 | arcanist | Grasping Weeds ([primal] is a hint) |
| Frost Shard | 1 | arcanist | Shard (cold) |
| Trembling Word | 1 | **herald** (claimed, HR-24) | Fear is the Herald's primary lean; the [arcane] hint moving to a presence class is legal (CL-12a) and stated |
| Grating Murmur | 1 | **herald** (claimed, HR-24) | Psychic would be a third key |
| Marking Light | 1 | **Later** (declined, HR-24; Votary or Hexer) | Radiant third key |
| Withering Touch | 1 | **Later** (declined, HR-24; Votary or Hexer) | Necrotic third key |
| Mending Word | 1 | herald | Stage 1 8.6 binds it |
| Rays (fire) | 2 | arcanist | Rays (fire) |
| Blinding Flash | 2 | arcanist (Herald declined, HR-24) | Blinding Flash |
| Detonation (fire) | 3 | arcanist | Detonation (fire) |
| Lightning Line | 3 | arcanist | Streak (lightning) |
| Terror | 3 | **herald** (claimed, HR-24; enemies only for every reader) | Fear |
| Dragfoot | 3 | arcanist | Dragfoot |
| Unweaving | 3 | **arcanist + herald** (one shared record, HR-24) | Unweaving |
| Icefall | 4 | arcanist | Highfall (cold) |
| Wilt | 4 | **Later** (Rootwarden; HR-24) | Necrotic third key |
| Wand of Sparks | 1 | arcanist | Needles (lightning) |
| Wand of Outlining | 1 | arcanist (Herald declined, HR-24) | Outlining |
| Wand of Fetters | 1 | arcanist | Snaring. Its "never on floor-1 carriers" restriction is read as a loot rule only; the consumables owner confirms |
| Wand of Rime Lance | 2 | arcanist | Lance (cold). Consumables 16.5 / 19's "only repeatable hard-capable item, floor 3+" note is updated: the class Rime Lance exists from L3. The wand's "Steeled, depth gates and resolve apply" is amended to "Steeled and resolve apply" (the conditions 7.2 depth gates are monster-against-party; 8.1), so the wand and the class variant agree under `VariantIsScroll` (ARC-24) **(new lead's call)** |

**The audit closes now:** every item is mapped, or defaults to Later
unless the Herald's stage 2 claims it. **Final values** from the Herald
design 8.8 (HR-24): its claims and declines are entered in the table
above (cross-document comparison 2026-11-23, CC-75). Off-pair reads are in class by
rule (8.1, 7.3). DEP-1 is untouched (the Shade carries it).

**Pending request (traps and locks design TL-35, 2026-11-21; M3; the
Arcanist owner signs off, with the consumables and Herald owners):** a
new tier-2 scroll, **Unbolting** (arcane and divine; action, range 6,
noise 10; opens one lock or Jammed lock without its consequence; never
the Bossgate), needs a CN 16.4 `classTags` entry although this audit is
closed; and the Unweaving scroll and the class Unweaving (8.4) turn a
Runed lock Plain (traps and locks design 9.2). Not decided here until
the owners rule.

## 9. The subclasses

Each subclass adds one Weft option (0 rows), a tier-2 spell on the
spellbook row, passives as glints, a level-6 choice of 2 verb changes,
and an Overchannel rider. No new pool: `SubclassAddsPool` passes; cap 6
of 8; reading 3. **(lead's call)**

### 9.1 BINDER (M2): "it stays where I put it"

**Identity.** Woven-force cords and sigil-shackles. The reliable
Hinderer: it keeps the *group* tied up, passing control from foe to
foe, and never locks one foe forever. Deepens the control primary lean
through `controlDuration` **(settled card: "longer and more reliable
control, never chained on one target")**.

**Specs into:** fetter duration; save reliability; concentration
resilience; control flow; stripping pips for the party. **Not into:**
damage; longer stops or hard control; boss lockdown; morale; forced
movement as a lean (Reel is a tool).

**Terms (lead's call):**
- **Binder control:** a major or hard instance your slot spell,
  Overchannel or Shackle applies to a hostile through its own
  `condition.apply` on a failed save (not riders; not scroll or wand
  reads unless `cinch.onScroll`). Instance fields `binderControl`,
  `sourceUnitId`.
- **Fetter:** Binder control whose condition denies no turn part and is
  not hard: Restrained and Blinded at M2 (Tangled and Grounded join when
  they ship). Tag `fetter`.
- **Stop:** Binder control that is a turn-part major (Dazed and Later
  ones) or hard. **Stops gain reliability only, never duration.**
- Party-facing instances are never Binder control.

**Level 3** (all at once; glints count 0):
- **Shackle** (tier 2, always known, family-free):
  > Action. One creature within 10. **Will** save. On a failure,
  > Restrained (binding `arcaneShackle`; breaker acid; escape difficulty
  > your spell difficulty), `turns:2`. No concentration, no damage, no
  > rider. **Upcast tier 3, Lashed Pair (L5):** two creatures within 10 of
  > you and within 4 of each other, each saving separately.
  - Will punishes brutes and makes Cinch useful (a Restrained target's
    Reflex is already at disadvantage). Acid pairs break their own
    Shackle: the preview says "will break Shackle". 0‰.
- **Cinch** (Weft option, signature, 1 Weft, own turn only, no action;
  turn allowance `cinch`, once per round):
  > Choose one. **(a) With a cast** that applies Binder control: one
  > target makes its initial save against it with disadvantage. **(b) On
  > a bound foe:** one hostile holding your Binder control; its next
  > repeat save against that instance before your next turn starts has
  > disadvantage (source-held mark `cinched[]`).
  - Greyed with a reason when the roll already has disadvantage, the
    target is Warded, Slipped from you or immune, or the cast is hard
    control on a Steeled target. The preview shows the odds swing
    ("fails 50% → 75%").
  - Never pre-ticked on a unit with pips left.
- **Fastness** (passive, `controlDuration`): when a fetter of yours is
  applied to a non-Unyielding hostile, its duration is overridden at
  application: `turns:N` → `saveEnds {save, cap min(N+1, 3), graceTurns:
  N}`; `saveEnds {cap c}` → cap min(c+1, 3); `sustained {max m}` → max
  min(m+1, 3). Shackle becomes 2 certain turns, then a save to keep a
  third. Needs `graceTurns` (BN-1).
- **Belay** (passive): when your concentration ends for any reason
  except its own `max` running out, each fetter it sustains on a
  non-Unyielding hostile becomes unlinked `turns:1`, marked `belayed`,
  never re-sustained or lengthened. Stops end normally (BN-2).
- **Slipped** (passive, "never chained"; source-held sets, not
  conditions; BN-3): (1) one Binder control instance per hostile at a
  time (`boundBy`); (2) when any instance of your Binder control ends on
  a living hostile, for any reason, it enters your `slippedFrom` set
  until the end of its next turn-end step; while Slipped from you, **no
  ability of yours** applies a major or hard condition to it (glint).
  Steeled, the per-fight cap, Unyielding, pips and composure apply on
  top. Allies are unaffected.
  - **A second instance (new lead's call):** a target in your `boundBy`
    or `slippedFrom` set is excluded from the cast's major and hard
    `condition.apply` parts **before saves are rolled**. It still takes
    the cast's damage and minor parts, and still saves if the spell has
    other effects on a failure. For a bundle (Dragfoot's Chilled + Dazed)
    only the major and hard components are dropped; the Chilled still
    applies on a failed save. The preview greys the dropped part with
    its reason ("already bound by you"); Cinch (a) and `castDefaults`
    skip that target.

**Ranks:** **Cinch II (L5):** (a) covers up to two targets of one cast;
(b) also covers Escape checks against your Restrained. **Cinch III
(L7):** the limit becomes a per-target round count; once per rest one
Cinch on a **fetter** costs no Weft (a cost waiver, SH-4; never on hard
control, classes 6.1 item 2). **Art I, Lashed Pair (L5)**; **Art II, the
Overchannel rider (L9):** an Overchannelled spell that applies Binder
control makes every target's initial save at disadvantage (no Weft,
outside the allowance, no damage).

**Level 6 (choose 1; each changes a verb):**
- **Rebind** (default): when a fetter of yours ends early on a hostile
  (not by your concentration ending, not at combat end), including at 0
  HP, move it to another hostile within 2 of the wearer (or its last
  square) you can see; that hostile saves fresh; on a failure it gains
  the fetter as `turns:k` (k = remaining wearer turns, minimum 1,
  maximum 2), unlinked, `rebound`, never lengthened. Turn allowance
  `rebind`, once per round. Never onto a Slipped, already bound, Downed
  or immune target, or an ally. Validator `RebindConserves`.
  - **Remaining turns by duration kind (new lead's call):** `turns:N` →
    the turns left; `saveEnds {cap}` (Fastness included) → cap minus the
    wearer turns elapsed; `sustained {max}` → max minus elapsed.
    **Ends early** = ends before that count reaches 0 for any reason
    other than your concentration ending or combat end (a successful
    save, a breaker, Escape, the wearer dropping to 0 HP).
  - **Target policy (automatic; "ask me" on your own turn only):**
    among eligible hostiles, units with no resolve pips left first; then
    by rank (elite, standard, minion); then nearest to the wearer; then
    unit order. Static data only, no scorer term **(new lead's call)**.
- **Reel** (expert): when a hostile fails its initial save against your
  Binder control, before the condition applies, push or pull it up to 2
  relative to you; verb-sourced for collisions; uses `movedByYou` (once
  per round per target); the 3-square sum per use; hazard-edge save and
  Teetering in M2; a failed contact-hazard save deals the typed collision
  band 2 / 3 / 3 as environment damage, counted in the at-will rider
  bound (TR-10, agreed Q3; 7.2; cross-document comparison 2026-11-23,
  CC-76). Default off (offered on the cast).

**`triggerMenu`** (`requires: subclass:binder`): `cinch.forAnAlly`
(Cinch on a hostile's save against an ally's major or hard condition; no
Binder clause, no Slipped); `cinch.onScroll` (your in-class control
scroll and wand reads count as Binder control); `mote.vsSlipped` (your
Mote's part is rider-flagged against a hostile Slipped from you; parity
row owed). Barred: Afterhold, any stop lengthening, off-turn Cinch,
Rebind on hard.

**Default level-3 pick:** not yet. The novice Binder clears solo floor 1
at 69–72% against the novice Alembic's 81–82% *(illustr.)*, because
Shackle on a pip boss only strips a pip. With pip-aware `castDefaults`,
the "bind the guard, not the boss" card line and the pip glint, its
per-subclass `Floor1ClassSpread` row is rerun. **The rule (new lead's
call):** on the rerun at the escalation point (5.4 item 1), the level-3
default becomes whichever subclass has the higher **novice** floor-1
clear rate in the mean over pair × key; a tie keeps the Alembic. The
choice is static data in `pairDefaults` and changes no rule. It steers
players who take the defaults; it does not move the class-mean rows,
which weigh both subclasses equally (5.4). A weak Binder novice row is
answered by its own lever (11.8 lever 1) and its per-row changes.

**Numbers *(illustr.)*:** Restrained uptime on one elite over 6 rounds
with 1 / 2 / 3 slots: base 17 / 34 / 50%, Binder 21 / 41 / 52%, Binder +
Cinch 34 / 64 / 71%; Slipped caps near 75%. Pips: 2 pips take 4.0
qualifying casts, 2.7 with Cinch. Camp share at L10 about 60% with Slot
Renewal 2. No Binder record carries `damage.*`.

**`effectKinds` (registry; classes 14) (new lead's call):**

| Feature | effectKinds | Subject / trigger |
|---|---|---|
| Shackle / Lashed Pair | `condition.apply:restrained{binderControl}` | one (Lashed Pair: two) hostile(s) / Will save failed |
| Cinch (a) / (b), II, III | `save.disadvantage:initial`, `save.disadvantage:repeat`, `check.disadvantage:escape` (II), `cost.waiver.perRest` (III) | target of your Binder control / declared with the cast; own turn on a bound foe |
| Fastness | `condition.duration.override{graceTurns}` | your fetter on a non-Unyielding hostile / on application |
| Belay | `condition.linger.onConcentrationEnd` | fetters your concentration sustains / concentration ends |
| Slipped | `condition.block.majorHard{sourceHeld}`, `condition.one.perSource` | hostile whose Binder control ended / instance ends |
| Rebind | `condition.transfer{turns≤2}` | another hostile within 2 of the wearer / fetter ends early |
| Reel | `forcedMovement.pushPull{verbSourced, sourceCount}` | hostile / failed initial save against your Binder control |
| Art II (Overchannel rider) | `save.disadvantage:initial` | every target / Overchannelled Binder control |
| `cinch.forAnAlly`, `cinch.onScroll`, `mote.vsSlipped` | `save.disadvantage:initial`; `binderControl.fromItem`; `rider.flag.cantrip` | hostile / an ally's major or hard condition; your control read; Mote hit |

**Data:** `mechanicIds [controlDuration, cinch, belay, slippedRespite,
shackle, rebind, reel]`. Records `spell.shackle` (upcast `coffle`),
`weft.cinch`, `binder.fastness`, `binder.belay`, `binder.slipped`,
`binder.rebind`, `binder.reel`, `overchannelRider.binder`. Validators
`FastnessBand`, `BinderFettersOnly`, `RespiteHolds`, `RebindConserves`.

**Reports:** control turns per slot against the base (target about 2×;
lever above 2.5×); turn starts denied per target (equals the base);
Slipped-blocked attempts; Belay turns by cause; Rebind transfers; pips
stripped and Shed against Binder control; Weft split; control reliance
(classes 6.1 item 5); per-subclass `Floor1ClassSpread` row.

**Levers if hot:** Belay on failed checks and psychic only; Cinch (a) on
stops only; `graceTurns` N → N−1; Rebind on 0 HP only. **If below the
base on bosses:** relax one-bind-per-foe to "one instance gets Fastness
or Belay"; then `noAllyFree` on Shackle. **Never:** extending stops or
hard; a free Cinch per fight; Binder clauses on Unyielding units.

**Weaknesses (card):** low damage; Unyielding switches off Fastness and
Belay; Weft-hungry; cannot stack its own control; adjacent allies,
Escape, breakers and Warded free fetters; minion packs; concentration
still breaks.

### 9.2 ALEMBIC (M2): "mix, then light"

**Identity.** The fight as a reaction vessel: douse, slick, then call
the spark, yours or the party's. Picks the Arcanist's secondary lean
(damage types and riders) through `priming` **(settled card)**. Tincture
is a tool, not a protection lean. No feature creates or changes a
square.

**Specs into:** party setups; payout timing; reagent choice; areas that
prime. **Not into:** at-will damage; new reagents; reagent squares; a
second prime per round.

**Level 3:**
- **Prime** (Weft option, signature I, 1 Weft; turn allowance `prime`,
  **once per round (settled, stage 1 3.4)**; no action):
  > When your Mote hits a hostile, after the hit's damage, the target
  > becomes Soaked or Oiled (your choice), minor, `turns:3`, no save.
  > Prime is that hit's rider. The instance records `origin: prime` and
  > `sourceUnitId: you` (ALM-4).
  - **Timing (new lead's call):** Prime is offered once the hit is known
    (after the attack roll and damage); a pre-tick (`castDefaults.prime`)
    auto-confirms it then. Weft and the `prime` allowance are spent only
    when it applies; a miss, a refusal or a greyed slot spends nothing
    (7.5 rule 6).
  - **Rider gate:** not offered for a reagent the target already holds
    (from any source).
  - **Rider slot (ALM-5):** Prime is that hit's rider on its target (a
    weak-target automatic rider greys it; on a crit, Prime **or** the
    crit rider; a Whittle load competes for the slot), but it does not
    count toward the at-will once-per-turn rider bound.
  - **One Alembic effect per hit:** at most one of Prime, Splash,
    Reagent Surge's free prime or a Runoff leap on a target per hit.
  - Cancels and overrides apply and are previewed: Soaked on a Burning
    target cancels both; Soaked washes off Corroded; Soaked + Oiled
    leaves fire normal with no automatic Burning. Never on a reaction
    attack, never on an ally. Bosses take it with no pip.
- **The inert rule, "your mix needs a spark" (change ALM-1; agreed, Q3;
  told plainly that it re-reads agreed stat 8.2 and 8.3 for one
  subclass's own at-wills):**
  > A condition instance with `origin: prime` and `source: you` is
  > treated as absent by every damage part of **your own at-wills**: no
  > weakness step, no resistance, no automatic rider, no Soaked arc
  > spread, no Oiled payout, and your at-will damage does not end it.
  > Your slot spells, Catalyse, Overchannel, every ally's hit and
  > consumables read it normally.
  - **Rider application (new lead's call, extending ALM-1 by one
    sentence; told plainly with the approval request and confirmed by
    the client on 2026-11-20, 20.1):** a rider your own at-will would apply is **not
    attempted** on a target holding your inert reagent when a conditions
    19 cancel, override or wash row would fire between that rider and
    the reagent (Burning or Smouldering against your Soaked; Corroded
    against your Soaked). The at-will's once-per-turn rider attempt is
    not spent, and an Interplay mark is not cashed (10.3). Riders with
    no such row land as on an unprimed target; a lightning-key crit arc
    on your own Soaked is an ordinary arc to one unit. So your bolt can
    neither set off nor strip your own mix.
  - **"At-wills" includes weapon attacks:** a staff or dagger with an
    Edge (element) property (weapons, M1), and any ring- or
    Attuned-shifted part, is an at-will (ability 5 table) and reads your
    prime as absent.
  - Soaked or Oiled from another source is not inert. If an ally's flask
    re-applies the condition, the source changes and it is no longer
    yours.
  - Card text up front: "Your at-wills (bolt and weapon attacks) can't
    set off or strip your own mix. Your spells and your allies can." On
    a primed target the glint reads the target's state only ("Soaked
    (yours): cold and lightning weak to spells and allies; fire
    resisted") and the preview greys the at-will's payoff line with that
    reason; neither names an ability of yours (ability 11.3) **(new
    lead's call)**.
  - Why it covers Soaked too: without it a self-primed cold Mote runs
    112–115% at L3–4 *(illustr.)*; with it 76–85%.
  - **Rejected (Q3):** the reagent restriction (Prime may apply only a
    reagent your Mote key does not read). It is **not** held as a lever:
    if the inert rule fails its tests or its parity rows, the question
    returns to the client **(new lead's call)**.
- **Catalyse** (tier 2, always known):
  > Action. Burst r1 within 8. 3d6 family (variant fixed when gained),
  > Reflex half, rider-flagged, not selective unless Whittled. Each
  > target holding Soaked or Oiled from any source saves with
  > **disadvantage** and takes this cast's family rider
  > **automatically** (one rider per target) **where the conditions 19
  > rows allow that rider**. The reagent is not consumed unless the
  > catalogue ends it.
  - **Precedence (new lead's call):** the catalogue's damage-end,
    cancel, override and wash rows apply first. Fire Catalyse on a
    Soaked target: fire resisted, Soaked ends, no Burning (the save is
    still rolled with disadvantage, since it is rolled before damage).
    Acid Catalyse on a Soaked target: Corroded is washed off at once.
    Cold and lightning on Soaked and fire on Oiled pay in full. The
    preview and pair card show it ("no Burning: Soaked"), and
    `castDefaults.prime` never chooses a reagent that would cancel your
    own Catalyse variant.
  - Unprimed it equals Spatter (997‰), so blind casting loses nothing.
    `PayoffBound`: primed 1744‰ (worst allowed case, cold or lightning
    on Soaked, the step's weakness included) against 1875‰; pass. Fire
    on Soaked and acid on Soaked now read lower; fire on Oiled is
    sampled too.
  - **Upcast tier 3 (Art I, L5):** burst r2 within 12, 6d6, same clause;
    910‰ / 1593‰.
  - The taught solo combo: Prime the boss (no pip), then Catalyse.
- **`castDefaults.prime`** (static predicate, ALM-8; rewritten, new
  lead's call): Prime is pre-ticked on a Mote hit when **all** hold:
  1. it is the first Prime opportunity this round and the target is a
     non-minion;
  2. the hit lands no rider of its own: no Interplay cash-in, Whittle
     load, crit rider or weak-target automatic rider (Prime would
     displace it);
  3. **a payer is available:** the reagent's payer count is at least 1,
     where payer count = your known slot spells whose rider the reagent
     pays (Soaked: cold and lightning; Oiled: fire; Catalyse counts for
     each reagent its variant's rider is allowed on, 8.1) for which you
     hold a slot of that tier or higher, plus companions' ready
     abilities and at-wills whose damage type it pays. **Your own
     at-wills never count** (they are inert to it);
  4. Weft is above the reserve (7.5 rule 9);
  5. solo (no companion in the fight): at most once per fight;
  6. the preview shows no cancel or wash of a condition a party unit
     applied (Soak on a target holding the party's Burning, Smouldering
     or Corroded).

  **Default reagent:** the one with the higher payer count; on a tie,
  the pair's entry in `pairDefaults`: Weatherwork Soak; Ruin Oil; Flux Oil
  with the fire key, Soak with the lightning key; Scour Soak; Etch Soak;
  Opposed Oil. A reagent with payer count 0 is never pre-ticked.

**Ranks:** **Prime II, Splash (L5):** on a hit, Prime may go to the
target or one adjacent hostile; on a miss, you may Prime one hostile
adjacent to the missed target. A Splash to an adjacent hostile, on a hit
or a miss, is an ability use, not a rider: it takes no rider slot and
the rider gate applies to that unit only **(new lead's call)**.
**Prime III, Tincture (L7):** a mode of Prime, bonus action, 1 Weft, the
round's allowance: one willing ally (or you) within
6 becomes Soaked; on a Burning or Smouldering ally both cancel (fire
out); washes off Corroded; otherwise fire-resistant and cold- and
lightning-weak `turns:3`. The preview shows both sides, including the
Frozen risk from a monster's `freezing` attack (depth gates and the
party-facing `turns:1` rule apply). Competes with Knit for the bonus
action. Registry ruling against the Water Flask (ALM-7); fallback
**Seal** (your Oiled cannot be shaken off).

**Level 6 (choose 1):**
- **Wide Mix** (default): Prime may ride one of your slot spells or
  Overchannel with an area instead of a Mote hit; after damage, every
  hostile in the area that failed its save (or was damaged, with no
  save) gains the chosen reagent, except where the spell's own rider
  took that target's slot, meaning **the cast's own rider landed on
  that target** **(new lead's call)**. Still 1 Weft and the round's
  allowance. Allies never.
- **Runoff** (expert): once per round (allowance `runoff`), when a prime
  of yours pays out (Oiled: a fire hit's payout; Soaked: the first
  weakness-step hit on it this round) by any unit's hit, one adjacent
  hostile gains the same reagent, no save, inert to your at-wills (the
  instance records `origin: prime`, `sourceUnitId: you`); never onto an
  ally, a unit already holding it, or one that received a leap this
  round (`runoffThisRound`). The target is chosen by an automatic
  policy: lowest unit order among eligible adjacent hostiles, with "ask
  me" only when the payout happens on your own turn **(new lead's
  call)**. Needs the payout-event hook; ships with Wide Mix alone if the
  hook slips.

**Art II, Reagent Surge (L9):** your Overchannelled spell primes, free,
every hostile it damaged that failed its save (or was hit), with one
reagent, except where the cast's own rider landed on that target; uses
the round's `prime` allowance; applied after damage (adds 0 to the
turn). On an Overchannel it **replaces** Wide Mix (no Weft is charged);
if the round's `prime` allowance is already spent it does nothing, and
the preview says so **(new lead's call)**.

**`triggerMenu`** (`requires: subclass:alembic`): `primeOnSpellAttack`
(Prime may ride a single-target slot spell attack's hit); `primeOnGust`
(Prime may ride a Gust whose target failed, after the push). The
draft's `tinctureOnBurning` is **deleted**: it was an off-turn Weft
spend and a Prime on a reaction, both barred (7.13 and below); Tincture
on your own turn already reaches an ally who caught fire since your last
turn **(new lead's call)**. Barred: a second Prime per round, Prime on
reactions, any off-turn Weft spend, self-payout, a second Runoff leap.

**Priming beyond a cantrip hit (change ALM-9):** classes 8.5 defines
priming as "a cantrip hit makes its target Soaked or Oiled". Here it may
also ride your slot spells and Overchannel (Wide Mix, Reagent Surge,
`primeOnSpellAttack`), a Gust (`primeOnGust`) and Splash's miss branch.
Still once per round, still a Weft option (Reagent Surge's free prime
uses the same allowance).

**`effectKinds` (registry; classes 14) (new lead's call):**

| Feature | effectKinds | Subject / trigger |
|---|---|---|
| Prime | `condition.apply:soakedOrOiled{origin: prime}`, `rider.slot` | hit hostile / own Mote hit, 1 Weft, `prime` allowance |
| Inert rule | `condition.inert.toOwnAtWills` | your primed instances / your at-will damage and riders |
| Catalyse | `damage.spell.family`, `save.disadvantage:primed`, `rider.auto:primed` | hostiles in r1 (upcast r2) / Reflex save |
| Splash (Prime II) | `condition.apply:soakedOrOiled` (ability use) | adjacent hostile / Mote hit or miss |
| Tincture (Prime III) | `condition.apply:soaked{ally}`, `condition.cancel:burning` | willing ally or you within 6 / bonus action |
| Wide Mix | `condition.apply:soakedOrOiled{area}` | hostiles failing the save / your area slot cast or Overchannel |
| Runoff | `condition.spread:prime` | one adjacent hostile / your prime's payout (any unit's hit) |
| Reagent Surge (Art II) | `condition.apply:soakedOrOiled{area, free}` | hostiles damaged / your Overchannel |
| `primeOnSpellAttack`, `primeOnGust` | `condition.apply:soakedOrOiled` | hit or failed target / own slot spell attack or Gust |

**Numbers *(illustr.)*:** party value of one Prime at L3: +2.2 / +4.3 /
+8.6 for a partial / full / two payers (Soaked); about +10 for one Oiled
fire payout. Nova: Reagent Surge 1.89, Restrained + Soaked group 1.95
(thin; rerun under 11.6). Camp share L10 about 59%. Expert-over-basic
damage gap 35–47%.

**Data:** `mechanicIds [priming, primeInert, catalyse, splash, tincture,
wideMix | runoff]` (Drench is a spell record, not a level-1 mechanic id;
both named in the `SubclassCopiesBaseLean` allow-list note). Validators:
`PayoffBound`, `PrimeSelfInert`.

**Reports:** payout rate (paid against expired with no effect) by pair
and party; party uplift on primed targets (lever: ≤ the Herald's
ally-tempo uplift + 10% *(illustr.)*); friendly-arc and friendly-cancel
shares; freeze rate; Weft split (is Knit starved?); Oil and Water Flask
payout in Alembic parties (dead-slot watch); Wide Mix / Runoff split;
per-subclass `Floor1ClassSpread` row.

**Levers:** if hot: Wide Mix primes at most 2; Runoff on Oiled only;
Catalyse's disadvantage only on your own primes; drop Splash's miss
branch. If it starves Knit: tighten the pre-tick; the Knit lever; +1
Weft from L3 for the Alembic. **Never:** a second Prime per round,
at-will self-payout, a third reagent, reagent squares.

**Weaknesses (card):** needs payers (solo, only its slots cash in; acid
pairs only Catalyse); Weft starvation; chemistry traps (Soaked resists
fire, puts out the party's Burning, washes Corroded; Soaked + Oiled;
Soaked arcs hit adjacent allies); Shed and phase changes wipe primes;
its own bolt never cashes.

### 9.3 LOREMASTER (unlock aspect, M3; sketch): "I know what you are, and so does everyone with me"

**Aspect shape (change AS-1, classes 7.3):** an aspect is a subclass
whose features hang off **one rule** but keep the full ladder shape (L3
tier-2 spell + the rule; a signature Weft option at 3 / 5 / 7; an L6
choice of 2; an L9 Overchannel rider).

- **The one rule:** your damage Studies the enemy. Picks the information
  ribbon as a lean; an information reliance report is owed.
- **L3:** **Lay Bare** (tier 2 [F]; spell attack range 10, 4d8,
  rider-flagged; on a hit the type is Studied with no roll; 1595‰).
  **Field Notes** (`studyRider`; turn allowance once per round): a
  damaging hit on an enemy of a type not yet Studied this fight makes a
  free Study (its own roll; a failure spends the type's once-per-fight
  Study). Grants Lore training, or another list skill if you have it.
  **Studied** is a per-fight state flag (not a condition).
- **Unpick** (Weft, signature; needs LM-1 `cooldown.delay` and the LM-2
  scorer term): 1 Weft, on damaging a unit of a Studied type, push one
  of its **revealed** cooldown or `readyAt` limited abilities back by 1;
  once per target per fight (II: per round, `delayCap 2` per ability per
  fight; III: once per rest one Unpick costs no Weft, a cost waiver, not
  per fight). Never a boss `signature` ability; other boss abilities at
  most +1 per fight. Denies no turn starts. Tactician is never copied
  (Unpick acts only on what Lore revealed).
- **L6 (choose 1):** **Weak Point** (single-target slot spells against a
  Studied type use its lowest revealed save for the initial save; repeat
  saves unchanged; never areas; lockout test with hard control owed) or
  **Cross-Reference** (Field Notes may fire twice per round, and Unpick
  may target a revealed ability of another unit of the same type within
  3).
- **L9 rider:** the spell Studies every type it damages, no roll, and
  one free Unpick on one target.
- **Owed first:** LM-0 (Study's difficulty and reveal persistence,
  enemies owner) at M2. Arcana mastery's no-roll threshold is **not**
  proposed (it would undercut DEP-1). Recommended Arcana mastery
  signature use, offered to the client later: identify magic items on
  pickup, no roll.

### 9.4 ELEMENTALIST (unlock aspect, M3; sketch): "my element is absolute"

- **The one rule:** one family of the pair (the **heart family**, not
  the Mote key: EL-2) ignores resistance on your class slot spells and
  Overchannel (**Purity**). Gear-shifted parts never gain it. Folds in
  Stormblood (settled, classes 9.2).
- **EL-1 (stat 8.4; owed at M2 whatever ships, because M2 offers already
  use the kind):** "ignores resistance" applies after the ladder and the
  cancel rule: a final Resistant reads Normal, never Weak; covers trait
  and layer resistance (the layer stays unbroken); never condition-based
  shifts (Soaked's fire resistance, Frozen's shell, Resistant (type),
  Shielded); never immunity; `IgnoreResistanceSources ≤ 1` per hero.
- **L3:** **Crux** (tier 2, heart family only; spell attack range 10;
  fire, lightning, acid 4d8 ≈ 1600‰; cold 3d6 + the freeze branch ≈
  930‰; its rider always Deepened). **Deepen** (Weft, signature; 1 Weft
  with a heart-family slot spell or Overchannel; on one target where
  that part's rider lands: fire Burning one band higher (max 3); cold
  Chilled, or Frozen through `freezing` if already Chilled or Soaked;
  lightning arcs to every adjacent unit; acid Corroded one band higher
  and one more layer stripped; II: two targets; III: once per rest
  free). Never on the Mote; never makes a rider land.
- **L6 (choose 1):** **Unmake** (heart-family parts count as a breaking
  type for layers resisting that family; broken for the fight, for
  everyone; bosses: ignored, not broken; Bore precedent, VG-18 ruling)
  or **Aftershock** (when a heart-family part drops a target to 0 HP,
  its rider lands on one adjacent hostile automatically, once per round;
  never an ally) **(new lead's call: the name fills the draft's reserved
  slot; "Overflow" has prose hits)**.
- **L9 rider:** a free Deepen at rank II.
- **Evidence owed:** resistant-target share by theme and floor;
  heart-family damage share (single-type drift); a parity row per family
  against a resistant reference; freeze rate; layer-break timing. Ships
  first at M3 if the resistance-share report is clean.

## 10. New mechanics considered

The client's brief asked for the usual considerations; each mechanic
comes with its cost, the rules it touches, its floor cost and a
recommendation **(lead's call)**.

### 10.1 Table

| # | Mechanic | Cost | Floor cost | Touches | Decision |
|---|---|---|---|---|---|
| AM-1 | Interaction preview + combo glint | S–M | none (teaches) | ability 8.3, 14.4, 16; conditions 15.4, 17.3, 19 | **Adopted, M2, required** (10.2) |
| AM-2 | Play defaults (pre-ticks, `defaultRotation`, `pairDefaults`, badges, warnings) | S | none | ability 7.3, 11.3 (reading); classes 7.4, 12.1; DH 17.7 | **Adopted, M2, required** (ARC-6, ARC-13; told plainly, accepted with Q1) |
| — | Pip-aware defaults, pip glint, last-slot confirmation | S | none | as above | **Adopted, M2** |
| AM-3 | **Interplay** (pair combo flag) | S | none (automatic) | classes 3.4, 7.6, 8.5; ability 9.3 | **Adopted, M2, behind `interplay.enabled`** (ARC-18) |
| AM-10 | **Temper** (friendly fire that only applies damage-end rows to allies) | S | opt-in | ability 8.2; conditions 18.6, 19 | **Adopted, M2 trigger** |
| AM-8 | **Fray** (Mote pressure on enemy concentration) | S | opt-in | ability 9.5, 13; enemies 17 | **Adopted, M2 trigger**; never in floor-1 monster kits as a mirror |
| AM-7 | **Afterhold** (concentration swap handoff; renamed from Linger) | S | opt-in | ability 9.5, 9.6; conditions 18.2 | **Adopted, M2 trigger**, fetters and minors only, barred for the Binder (ARC-28) |
| AM-9 | Insulate (+2 to an initial save) | S–M | opt-in | ability 7.3, 14.3; armour 10.4 analogue | **Adopted, folded into Flare II at L8** (ARC-19) |
| AM-11 | `SkillExpressionReport` (four arms) | M (sim) | — | classes 5.6, 12.3; DH 18.2, 19.3; ability 13 | **Adopted, M2, required report** (ARC-29; no gate, **agreed** Q1) |
| — | `SetupAttribution` (parity reading) | S | — | VG-12; weapons 6.4 | **Adopted** (ARC-16; told plainly, accepted with Q1) |
| — | The starter rule and `StarterDefaultsHaveAnswer`; creation pick definition | S | none | classes 5.6, 7.4 | **Adopted** (ARC-15; told plainly, accepted with Q1) |
| AM-4 | Consume (detonate a minor for a data bonus) | S | trap cards | conditions 6.8, 19 | **Later**; revisit on pick-to-clear rows |
| AM-5 | Pull toward a point (renamed **Converge**) | S–M | opt-in | conditions 10.1; a `clusterGain` AI term | **Later** |
| AM-6 | Anchored aura (the moving "wall") | S–M | opt-in | ability 8.4; consumables 16.7 wording | **Held** for the aura ruling |
| AM-12 | Pair Conflux rows | M | none | conditions 19 | **Later lever** if a pair is shown trapped |
| EE | Conflux, Detonate, Ionised, ComboBound | M | none / some traps | many | **Later lever** if the expert-basic win-rate gap is under 8 |
| SP | Lace, Fork, Quickcast, ModifierBound | S–M | opt-in, heavy reading | classes 8.5, 12.2 | **Later** |
| — | Hero Coach | S | none | reverses agreed ability 11.3 | **Not proposed**; raised only if the client asks |
| — | Counterspell, Shield-shaped spells, readied casts | — | — | ability 2 non-goal; consumables 16 cut; no nesting | **Rejected** |
| — | Delayed blast on a unit | — | — | matches Condemned (Later), fails `FlagNotCondition` | **Rejected** |
| — | Walls, clouds, webs, grease, surfaces | — | — | ability 8.4; conditions 11.1 | **Rejected** |
| — | Metamagic point pool; per-cast element choice; third key; teleport; summons; Mage Armour | — | — | cap, reading, anti-goals, classes 6.3 | **Rejected** |

### 10.2 Adopted in full: AM-1 interaction preview and combo glint

The core preview (ability 8.3; conditions 15.4) gains lines from a dry
run of the pipeline (fail chances, no rolls, party knowledge only;
unrevealed traits marked uncertain):
1. friendly fire on every ally the cast reaches, lethal or not ("hits
   Bram (ally) for about 9"), and damage-end and cancel rows on every
   affected unit, allies included ("ends Burning on Bram (ally)";
   "dries Soaked: no Burning");
2. payouts ("Oiled: +half, Burning band 2"; "Soaked: cold weak,
   automatic Chilled"; "Frozen: Fracturing +half, Staggered");
3. transforms with fallback and gates ("Will Freeze"; "Steeled: falls
   back to Chilled"; "Boss: spends a pip");
4. every unit an arc or fork reaches;
5. forced movement: destination, tie-break, collisions, hazard-edge
   odds, "Teetering on success", destination square conditions;
6. concentration: "ends your Grasping Weeds"; the target's break odds;
7. spellbook: "a short rest returns {n} slot(s) of tier 2 or lower" when
   Slot Renewal covers a spend (n from data);
8. Interplay: "Interplay: Chilled lands" on a marked target (suppressed
   while `interplay.enabled` is off);
9. the Alembic's inert rule: "your at-wills (bolt and weapon attacks)
   won't set off or strip your own mix" on a target holding your prime,
   and "your mix blocks this rider" where 9.2's rider sentence applies.

A **combo glint** (0 to read) marks a hostile whose current state makes
a payoff row live, and names **the state and the damage types it pays**
("Soaked: cold and lightning weak; fire resisted"; "Oiled: fire pays
out"; "Chilled: a `freezing` cold source freezes it"). It never names an
ability, the hero's or a companion's, so it describes the target and
does not point at an action (ability 11.3) **(new lead's call: narrowed
from the draft's "naming the ready ability")**. The display computes
nothing; the property test (ability 16) is extended: preview equals
resolution with full knowledge and reactions off. The glint never ranks
or highlights a choice.

### 10.3 Adopted in full: Interplay, Temper, Fray, Afterhold

**Interplay** (base passive, M2; `interplay.enabled`, on by default;
change ARC-18):
> When you deal damage of one family of your pair to a hostile on your
> turn, you mark it (you hold one mark; a new mark replaces the old).
> Your next hit on that target with a damage part of your **other**
> family, before the end of your next turn, has that part rider-flagged,
> and the mark ends. Own turn only, for setting and cashing. One rider
> per target per use (ability 9.3 order). A Mote cash-in counts as the
> at-will's one rider attempt that turn. The mark ends when you are
> Downed or Incapacitated, or at combat end.

**Data rules (new lead's call):**
1. **Only pair-family damage parts set or cash.** A physical part
   (Shard's piercing primary, Highfall's bludgeoning) and family-free
   spells never do; in a mixed spell the family part is the one that
   counts (Shard: the burst; Highfall: the family part).
2. **A target counts** when it takes more than 0 damage from such a
   part, by attack or by save (save-for-half damage counts).
3. **Within one cast, cashing resolves before setting.** A cast that
   cashed on any target sets no mark.
4. **A cash-in consumes the mark only if a rider attempt actually
   occurs** on that target. If the attempt is unavailable (the at-will's
   rider was already used this turn, the rider is suppressed by a
   conditions 19 row or by the inert rule), the mark stays.
5. **Multi-target casts** set the mark on the primary target (the
   attack or chosen target, or the unit on the area's point if it was
   damaged), else on the first damaged hostile in unit order; the player
   may instead declare one damaged hostile with the cast (the scorer
   for companions).
6. **Beams** resolve in the 7.2 order; each beam can cash or set.
7. **Fused** (7.9): only pair-family parts gain both families; a Fused
   cast cashes any held mark and never sets one.
8. With `interplay.enabled` off, nothing sets or cashes (7.1 note).

- Fires solo: L1 Shard (lightning) marks; next turn the cold Mote lands
  Chilled. (The simple core's "later in the same round" wording could
  never fire solo and is replaced.)
- Source-held mark: no save, tier or cure; glint; `FlagNotCondition`
  passes.
- Bound: riders only, no dice; under `SetupAttribution` a slot-set
  mark's extra Mote rider is charged to the slot spell's row in the
  parity accounting, not to the Mote (ARC-16; `SpellTierBound` is
  unchanged by it). Watch the Opposed pair, where it mostly fires
  cancels (the pair card says so; its default starters avoid it, 7.14).
- **The lever (lead's call):** `interplay.enabled` ships on. The lead
  turns it off if the modifiers-off rows of `SkillExpressionReport` show
  no ceiling gain, or if parity drifts; Fused then has no effect and the
  ladder line is dropped. It is not a client question.

**Temper** (Whittle mode via `sculptTemper`; 1 Weft; replaces selective
for that cast):
> Allies in the area take no damage and gain no condition, rider or
> payout from this cast, and are left out of its forced movement, arcs
> and forks (as selective, SP-6). Each damage-end row the cast's types
> would trigger still applies to them: fire ends Chilled, Frozen and
> Soaked, and ends Oiled with **no** payout; cold ends Burning and
> Smouldering.
> Fracturing never applies under Temper.

A tool, not a lean. No healing. Per-fight lost-turn cap and Steeled
unchanged.

**Fray** (`moteFray`): when your Mote hits a concentrating hostile, that
hit's concentration check has disadvantage; once per turn. No damage.
Weaker than the psychic rider by design (example: Con +2 at DC 10 holds
about 65%, about 42% with disadvantage *(illustr.)*).

**Afterhold** (`afterhold`; `requires: not subclass:binder`):
> When you end your concentration voluntarily, on your own turn, by
> starting another concentration effect, each `sustained` **minor or
> fetter-type major** instance (a major condition that denies no turn
> part and is not hard: Restrained and Blinded at M2) of the ended
> effect becomes `turns:1` (ends at the end of its wearer's next turn,
> by the conditions 5.2 test), never past its remaining `max`. Never applies to Dazed or other
> turn-part majors, or to hard instances; never when concentration ended
> by damage, Downed, Incapacitated or Unfocused.

The scorer's "replacing concentration" cost is reduced by the afterhold
value.

## 11. Numbers

**All illustrative; scratch models; simulator owed.**

### 11.1 Parity (weapons 6.4 ≤ 110%; VG-12; worst legal build: fire key, spare Weft on damage, `capacity:weft` at L9–10, Slot Renewal floor-averaged as rest power)

| Row | L1 | L3 | L5 | L7 | L9 | L10 |
|---|---|---|---|---|---|---|
| Greatsword reference (dpr) | 7.9 | 7.9 | 17.3 | 17.3 | 19.2 | 19.2 |
| Mote | 73 | 73 | 75 | 75 | 74 | 74 |
| Mote + crit riders | 75 | 75 | 77 | 77 | 78 | 78 |
| Core worst (load) | 84 | 90 | 85 | 85 | 102 | 102 |
| Core + Interplay (upper bound, marks counted free) | 87 | 95 | 93 | 95 | 102 | 102 |
| Fire + acid, free-setup reading | 85 | 93 | 92 | 94 | **117** | **118** |
| Fire + acid under `SetupAttribution` | 85 | 93 | ≤ 92 | ≤ 94 | ≈ 102 | ≈ 102 |
| Fallback: at-will riders at band 1 | 85 | 93 | 87 | 89 | 96 | 97 |
| Alembic without ALM-1 | 78 | **115** | 107 | 107 | 105 | 105 |
| Alembic with ALM-1 (**agreed**, Q3) | 79 | 85 | 78 | 78 | 79 | 79 |
| Binder | 78 | 81 | 70 | 68 | 58 | 53 |

**Reading:** `SetupAttribution` (ARC-16) is a **parity-accounting rule
of the simulator and the arithmetic model** **(new lead's call, placing
the accepted reading)**: the value a camp-sourced condition (Corroded,
an Interplay mark) adds to your own at-wills is charged, in the parity
report, to the source spell's row (read as camp power) and not to the
at-will's. It adds nothing to the static `SpellTierBound` evaluation,
which stays a per-record predicate. The band-1 rider is a fallback lever
only. ALM-1 is required and agreed. A failed contact-hazard save from
Gust or Reel deals the typed collision band 2 / 3 / 3 (TR-10, agreed
Q3); it is environment damage, counted in the at-will rider bound, so it
adds no row here (7.2; cross-document comparison 2026-11-23, CC-76).

### 11.2 Slot spells against the bounds (at arrival level)

All pass `SpellTierBound` except three drafts, now fixed: Spatter at 3d8
(1282‰ → 3d6, 997‰); Rime Lance under a literal hard clause (1196‰ → the
SP-1 reading); a tier-4 Detonation (fire) upcast (1366‰ → no flat-tier area
upcasts, now the validator `AreaUpcastSkipsFlatTier`, which also moved
Shard's burst upcast to tier 3 and removed Fan's and Spatter's tier-4
rows). Under `CampOptionBound` read literally, every area and hard
spell fails (Overchannel Detonation 3274‰ against 1200; r3 at 49 squares
against 25), which is why ARC-2 is required.

**How a record becomes ‰ (`boundModel`, tuning file, 14.1; new lead's
call):**
- Each damage part is judged against its own line: single-target parts
  against 2000‰, area parts against 1250‰ per target (Shard's primary
  and burst separately; Forked Bolt's primary as single, each fork as
  area-like at 1250‰). Needles count with every needle on one target.
- Expected value at the reference target of enemies 6.1 at the tier's
  arrival level (and each upcast entry's arrival level): the attack hit
  and save-fail rates per level from that reference, a 5% crit rate,
  the rider value in ‰ per family and band, the arc value in ‰; the
  reference target has **no** trait weakness or resistance.
- Hard control is priced by the hard clause (SP-1), not by ‰; a spell
  with no damage part (Arrest, Shackle) reads 0‰.
- **`PayoffBound`** iterates the setup-step list of 3.4 (Soaked, Oiled,
  Restrained → Reflex disadvantage, Stunned → Reflex auto-fail, Chilled
  → `freezing`), one step at a time per record, the step's own effect
  included (Soaked's weakness, Oiled's payout), the conditions 19
  precedence applied (8.1).
- **`SlotlessCastBound`** evaluates every tier-1–3 slot spell record at
  L9 with the best free Whittle mode plus one paid mode, under
  `AreaCapByLevel`.

`PayoffBound` (≤ 1.5 × the `SpellTierBound` line, **one setup step per
target, save penalties included**): Catalyse primed 1744 (cold or
lightning on Soaked), upcast 1593, Detonation on Soaked 1821; pass. Stacked steps (Restrained + Soaked
2124; Stunned + Soaked 2428) count as two steps and are therefore
outside a single cast's evaluation: they cost a prior camp spend each
turn and are read as multi-turn combos, bounded by the nova row.

**The 1250‰ area line** is a deliberate departure from enemies 6.2's
120% **(lead's call)**: at 1200‰ the class variant of the approved
Detonation (fire) scroll (8d6, 1214‰) and Streak fail.

### 11.3 Caster AC and sustain

AC per 7.12. `Floor1BossSustain` (DH 21.1 step 6 reads static keys; no
change needed):

| Reading | Need | Con +1 (60) | Con −1 (49) |
|---|---|---|---|
| Static guard keys (the rule) | 48 | pass | pass by 1 |
| Report: at the Arcanist's AC 14 | 52.4 | pass | fail |
| Report: at AC 11 | 65.5 | fail | fail |
| Report: AC 14 with one reaction ward per boss round | 44.0 | pass | pass |

The Arcanist at Con −1 is the designed binding case. **Fix (lead's
call):** report Arcanist door-HP and potion medians separately, so a key
recalibration shows its Arcanist impact first (16).

### 11.4 Floor-1 spread (solo, clear %, N = 12,000; Vanguard 98.3, Shade 88.7)

| Package | Novice | Basic (`defaultRotation`) | Expert | Basic gap to the Vanguard |
|---|---|---|---|---|
| Core, Binder | 69.4 | 93.8 | 95.6 | 4.5 |
| Core, Alembic | 81.0 | 92.0 | 96.1 | 6.3 |
| **Core, class mean** | **75.2** | **92.9** | **95.8** | **5.4** |
| Element engine, class mean (reference) | 76.9 | 95.1 | 96.1 | 3.2 |
| Stage-1 kit (bolt 1d10, Weft 2), with Parry | — | 70.8 | — | 27.7 (fail) |
| Core, Con −1 | — | 91.3 | — | 7.0 |
| Core, Dex −1 (AC 11) | — | 76.6 | 79.7 | 21.7 (warning) |

- **The gate arm (reference: the scorer) was not run in the proxies.**
  The basic arm is a scripted lower bound on it; the reference arm is
  expected at or above it. Simulator owed.
- The Shade's 9.6 gap sets the spread; the Arcanist's class mean must
  stay at or above about 88.3 across **all** picks (ARC-15).
- Floor-1 skill gaps: expert − basic 0–4 (meets ≤ 5); **basic − novice
  11–24 (misses ≤ 8; worst for the Binder)**. The floor-1 risk is slot
  discipline and the pip trap, not combo knowledge. Weft 4 changes
  nothing (Weft is not binding on floor 1).
- **Fixes, in data before the escalation point (5.4):** the last-slot
  confirmation; pip-aware defaults; `StarterDefaultsHaveAnswer` and the
  starter rule; the Opposed starters (7.14). The novice responses (5.4)
  are what let these move the novice row. The level-3 default is **not**
  among them: the class means weigh both subclasses equally, so it
  cannot move the escalating number (5.4) **(new lead's call)**. Then,
  if the row still misses, the levers in 11.8, the first of which
  targets the Binder's pip trap.
- The Dex −1 rows (basic and novice) are printed beside the class mean
  as the creation warning's evidence (7.14).

### 11.5 Skill expression: floor-averaged damage, expert ÷ basic − 1

| Model | L1 | L3 | L5 | L7 | L9 | L10 |
|---|---|---|---|---|---|---|
| Core | 26% | 28% | 24% | 24% | 30% | 34% |
| Core, Alembic | 26% | 35% | 32% | 35% | 42% | 47% |
| Core, Loremaster / Elementalist | 26% | 28–29% | 26% | 27% | 33–34% | 37–38% |

The Binder's gap is control (11.10), not damage. Slot timing is real
skill: holding tier 2 makes Slot Renewal return a tier-1 slot (3.7
against 8.9 per recovery at L3). The gap grows with level, matching
"depth on floors 2–5".

### 11.6 Nova (Overchannel ≤ 2 × the p95 non-capstone turn; L9)

| Case | Mean ÷ p95 | p95 ÷ p95 |
|---|---|---|
| Base (one free mode + a paid mode) | 0.91 | 1.47 |
| Binder rider | 1.06 | 1.68 |
| Alembic, primed group | 1.16 | 1.89 |
| Restrained + Soaked group | 1.36 | 1.95 (thin) |

**Fix:** rerun with the denominator computed under the same setup
reading as the numerator, and with `PayoffBound` widened; one free
Whittle (not two). `SlotlessCastBound` passes.

### 11.7 Guard sensitivity (reports only; AR-20 stays 11)

Content tuned at a 6.4 row's edge reads higher at the Arcanist's own AC
14 (a 25% floor-1 boss reads 27.3%); round-1 death chance for a level-3
lowest-HP hero rises from 4.5% at AC 15 to 5.7% at AC 14. These are
reports; the binding guards already run at AC 11.

### 11.8 Camp share, the 60% rule and the lever order

| L | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|---|---|---|---|---|---|---|---|---|---|---|
| Core + Slot Renewal 2 at L9 (half-Knit measure) | 26 | 35 | 40 | 47 | 46 | 55 | 52 | 55 | ≈ 55 | ≈ 57 |

- L3–10 in band (with subclasses: Binder about 60%, Alembic about 59% at
  L10). **L1–2 miss low, accepted (ARC-4; agreed, Q2)**, because
  rest-timed Slot Renewal dominates early. Stage 1's Weft 2 / 3 / 4
  gives 71% at L10.
- 60% rule: L1 ≈ 197‰ of L10 (≤ 400); increments at 3 + 5 + 9 ≈ 591‰ (≥
  500). Pass. Rerun in the offer value unit owed.
- Novice-realised share (all Weft to Flare): 25 / 49 / 68%. Reported.

**Levers if the floor-1 rows miss, in order (lead's call; lever 1 and
its place first are new lead's calls; the escalation in 5.4 applies
them):**
1. **A default target filter** for major-control casts (Shackle,
   Arrest, Snaring, Blinding Flash): the cast's pre-selected target is
   a unit with no resolve pips left, when one is in range. A cast-option
   default of the ARC-6 class (static data; the player re-targets
   freely). Moves the novice arm only: the novice arm takes a
   pre-selected target when one is offered, and the basic arm already
   aims by score.
2. The Snaring-shaped starter as the default second card. Moves both
   arms, the novice more (a tier-1 Restrain with no concentration does
   not depend on holding tier-2 slots).
3. Mote 1d12 at L1–4 (about 79% parity). Moves both arms; closes the gap
   only through the basic arm's ceiling near 93–98%.
4. Flare policy `always` while solo. Moves both arms, as lever 3.
5. The shared floor-pacing request (boss door at level 3). Moves both
   arms, as lever 3. **In force:** answered by the Forecourt Top-up
   (run structure design 9.4, RT-8), so it is already in every run and
   the levers are run out after 1–4 (cross-document comparison
   2026-11-23, CC-72).

One lever at a time, each rerun over its blast radius;
`Floor1ClassSpread` is re-read after every lever (seeds and validation
design 6.9, SV-26, read; this order kept).

**Never:** the HP tier, the armour ceiling, item AC, temporary HP, a
second reaction, a forbidden grant, Weft 2 at levels 1–2 (rejected, Q2),
or the reagent restriction (rejected, Q3).

### 11.9 Slots per floor

Casts per floor (camp fill + 2 Slot Renewals + Overchannel): 4 / 6 / 8
/ 10 / 15 / 16 *(illustr.)* at L1 / 3 / 5 / 7 / 9 / 10 (L9 and L10
corrected, cross-document comparison 2026-11-23, CC-77), about 19% of rounds at L1
rising to 67% at L10. The Mote carries floor 1; late on, composition
skill takes over from slot timing.

### 11.10 Control

Hard control (Arrest, 3 casts): plain 45% land, 1.26 expected lost turn
starts; with Cinch 70%, 1.75; with Weak Point 65%, 1.68 (never
combined). All under the per-fight cap of 2. After pips, Dazed uptime ≤
about 50%, never a lockout.

### 11.11 Cap and reading

6 of 8; reading 3. With the Knit lever 7 of 8 and reading 4, with no
offer row allowed.

## 12. No M1 slice; the M2 cut

**(lead's call)** The Arcanist is an M2 class. **M1 needs nothing** from
this document: no M1 rule is written for the Arcanist, and none of the
engine work below is on the M1 path.

**The M2 Arcanist, levels 1–10:**
- the whole core: chassis, Mote, Gust, the element pair and pair cards,
  slots and Slot Renewal (Slot Renewal 2 at 9), Weft (Whittle, Whittle
  II, Flare, Flare II, Knit), concentration as settled, Overchannel with
  Fused, offers, lanes and `triggerMenu`;
- the full spell list (8.2–8.5) and the `classTags` mapping (8.10);
- Interplay (behind its lever); Temper, Fray and Afterhold as triggers;
- AM-1 and AM-2, the pip-aware `castDefaults`, the last-slot
  confirmation and the pip glint;
- `SkillExpressionReport` with the novice row and its escalation;
- every validator in 14.2;
- **two subclasses** (classes 13, M2 row): the Binder and the Alembic.

**Conditions the Arcanist touches:** applies Burning, Chilled, Frozen
(transform), Soaked, Oiled (Alembic), Corroded, Restrained, Stunned,
Dazed, Blinded, Revealed, Prone, Teetering (hazard edge); ends Burning,
Smouldering, Chilled, Frozen, Soaked, Oiled (fire payout or Temper),
Corroded (Soaked wash), Restrained (breakers); reads all of these and
Staggered, Steeled and Concentrating. No new condition.

## 13. Binding requirements and how they are met

| Requirement (source) | How met | Status |
|---|---|---|
| A rest and a camp pool; 40–60% from camp (ability 6.3; classes 8.2) | Weft, Slot Renewal (rest); slots, Overchannel (camp); 11.8 | L3–10 in band; L1–2 miss accepted (ARC-4, **agreed** Q2) |
| Exactly one `restDefensiveTool`, ≤ half Recovery, sub-limit 1 / 2 (classes 8.2; DH 21.2) | Knit | Met |
| ≥ 2 at-wills, one damaging; at-wills never hit allies (ability 6.1, 8.2) | Mote, Gust; ARC-8 for arcs | Met |
| Slots and Slot Renewal as settled; one camp swap (ability 6.5) | 7.4, 7.3; Slot Renewal 2 (ARC-12) | Met (change) |
| One camp spend per turn, scrolls included (ability 6.4; CN-11 **agreed**) | Slot casts and Overchannel | Met |
| Cap of eight; reading ≤ 4 (classes 7.5, 7.6) | 6 / 8; reading 3 | Met |
| Published ladder (classes 7.4) | 5.5, 7.7; 9 | Met |
| About 60% of limited power after L1 (classes 5.6) | 197‰ / 591‰ | Met (rerun in offer units owed) |
| Areas ≤ 25 squares at L1–6; friendly fire unless selective; no ground zones (ability 8.1, 8.2, 8.4 **agreed**) | `AreaCapByLevel`, `SculptCap`, Whittle selective | Met (+ ARC-9) |
| A policy on every reaction; one reaction AC bonus per attack (ability 7.3; armour 10.4, 11) | 7.14; step 3a (ARC-5) | Met |
| Hard control only from limited abilities; M1/M2 conditions only; no Shocked; budget 32 (conditions 7.1, 16, 18.5) | 8.1 | Met |
| No teleport, summons, item AC, refills outside rests, second camp spend, healing beyond the rest tool (classes 6.3) | `ForbiddenClassGrant` passes; cost waivers are per rest (SH-4) | Met |
| The hero never gets suggestions (ability 11.3 **agreed**) | `castDefaults` static, shown ticked, the player can untick them; confirmations of own actions; glints describe a target's state and name no ability, never rank (10.2) | Met (ARC-6, ARC-13; told plainly, accepted with Q1) |
| Lean, never require; subclass leans (classes 6.1, 6.2, 7.3) | 7.1, 9 | Met |
| Capstone camp 1 at L9; changes the primary lean; nova ≤ 2.0 (ability 6.7; classes 14) | Overchannel 7.9 | Met (rerun owed) |
| Parity ≤ 110% (weapons 6.4; VG-12) | 11.1 with ARC-16 and ALM-1 | Met (rerun owed) |
| Caster defence (armour 11) | Not triggered (light); AR-20 stays 11 | Met |
| `Floor1BossSustain` at both Con (DH 21.1) | 60 / 49 against 48 | Met (thin at Con −1) |
| `Floor1ClassSpread` ≤ 10 points, every pick (classes 5.6, **agreed** Q2 of stage 1) | ARC-15; 11.4 | Stage 2 (simulator; per-pair, per-starter, per-subclass rows) |
| Floor-1 deaths under about 5% per class (DH 18.2) | The reference arm's death share is the one read against DH 18.2 (and 20.2); the novice death share is report-only and feeds the 5.4 escalation | Report owed |
| `SeedHasTarget` (classes 5.6) | `seedTarget` `twoEnemiesWithinRadius1` (7.1); the check in 14.2 | Met (content check) |
| Per-round breakdown of the solo boss (classes 5.6) | Signature share in rounds 2+ of the floor-1 boss fight, by level 1–3 and subclass (14.3) | Report owed |
| Dip guards (classes 7.7) | 7.1 | Met (M4 report) |
| Creation defaults and ladder preview (classes 7.4) | 7.14, 5.2 | Met |
| `classTags` audit (classes 14; consumables 16) | 8.10 | Met (closed with Later defaults) |
| Registry pass; `effectKinds`, subject and trigger on every feature (classes 14) | 7.15 (core) and the `effectKinds` tables in 9.1 (Binder) and 9.2 (Alembic); Prime against the Water and Oil Flasks ruled first (7.15) | Met; remaining rulings owed (19) |
| Concentrator count (Stillmind's two) | 1 of 2 | Met |
| DEP-1 (consumables 13.8) | Untouched (the Shade carries it) | Met |

### 13.1 Test additions

- The composite-lockout set (8.9).
- Interplay: solo cash-in; the Opposed pair; Fused on Overchannel; mark
  ends on Downed.
- Temper on a Frozen and a Burning ally; Temper never shatters.
- Selective covering forced movement, arcs and forks.
- The Alembic inert-rule set (ALM-1): own fire Mote on own Oiled; own
  cold Mote on own Soaked; an ally's cash-in; source change on flask
  re-application; cancel on a Burning target; rider slot; once per round
  across all uses; Wide Mix; Runoff; Catalyse; boss and phase change;
  Tincture.
- The Binder set: Cinch greyed on a Restrained target's Reflex; Slipped
  blocking your own crit Staggered; Fastness off on Unyielding; acid
  breaking Shackle; Belay when Downed; Reel through web and into water;
  0-HP Rebind.
- Gust's `movedByYou` not blocking an ally's verb push.
- The arc's damage event and concentration break; the at-will Soaked arc
  spread once per turn across beams.
- The preview property test with the AM-1 lines (preview equals
  resolution with full knowledge and reactions off).
- `castDefaults` read static data only (a property test: no scorer term
  changes a pre-tick).
- The last-slot confirmation fires exactly under 5.2's predicate (a) to
  (e), upcasts included, with the per-tier message from data (Slot
  Renewal 1 and 2).
- Mote beams: the 7.2 resolution order; a weak-target automatic rider
  uses the once-per-turn bound; Prime offered only after a hit.
- Interplay data rules (10.3): a physical part never sets or cashes; a
  cash-in with no rider attempt keeps the mark; cash before set in one
  cast; the multi-target default mark; the flag off makes the 7.1
  mechanic inert.
- Selective on a single-target lightning slot spell: Brand (lightning)
  on a Soaked foe adjacent to an ally, with and without Whittle.
- You are never affected by your own burst (Shard, Catalyse, Grasping
  Weeds cast on an adjacent foe).
- Temper leaves allies out of forced movement, arcs and forks.
- The Alembic additions: an own at-will rider against own inert Soaked
  (Burning cancel, Corroded wash: not attempted, attempt not spent); a
  lightning-key crit arc on own Soaked; an Edge (fire) weapon attack on
  own Oiled; fire and acid Catalyse on Soaked; the default Weatherwork
  Alembic with an Interplay mark and the Prime pre-tick (no pre-tick);
  no Alembic trigger spends Weft off-turn; Reagent Surge with the
  allowance spent; Splash to an adjacent hostile.
- The Binder additions: Dragfoot over your Shackled target (Dazed
  dropped before the save, Chilled kept); Rebind from a Fastness
  `saveEnds` instance and from a `sustained` one.
- `DrawSatisfiable` and `AreaUpcastSkipsFlatTier` over the generated
  records; `KnownVariantInPair` as a property test.
- Dip guards: Overchannel and Slot Renewal 2 absent below class level
  9; Weft, Mote beams and the Knit sub-limit scale by class level.

## 14. Data, validation and simulator

### 14.1 Record fields added

**(lead's call; illustrative shape)** Beside the classes 12.1 record and
the Vanguard and Shade designs' 13.1 fields:

- on the class: `elementPair[2]`, `moteKey`, `pairDefaults` (with the
  per-pair default reagent, 9.2), `recommendedPickOrder` (per level,
  `{templateId, family: key | offKey | none}[]`, 7.3),
  `defaultRotation` (rules `{when, do, slotTiers, bonusAction?}` over the
  closed predicate vocabulary, 5.2), `castDefaults`,
  `creationWarnings[]`, `rankSchedule`;
- on spell records: `spellTemplate {families[]}`, `variantOverrides`,
  `drawWeight`, `badge` (`recommended` | `expert`), `upcasts[{tier,
  adds}]`, `concentration`, `freezing`, `riderFlag`, `worsen`, `srd`
  (the SRD 5.2.1 entry cited);
- on option records: `sculptModes[]`, `countsAsParry`,
  `reserveForRestTool` on policies;
- on condition instances: `origin` (`prime`), `sourceUnitId`,
  `binderControl`, `belayed`, `rebound`; on `saveEnds`, `graceTurns`
  (BN-1);
- source-held state: the Interplay mark, `movedByYou`, `slippedFrom`,
  `boundBy`, `cinched`, `runoffThisRound`;
- trigger kinds `ownSave` (ARC-19) and the payout-event hook (Runoff);
- on subclasses: `offerLaneAdjust`, `artId`, `overchannelRiderId`;
- in the tuning file: the `SpellTierBound`, `SlotlessCastBound` and
  `PayoffBound` lines; **`boundModel`** (hit and save-fail rates per
  level against the enemies 6.1 reference, crit rate, rider ‰ per family
  and band, arc ‰, the per-part line rule; 11.2); `AreaCapByLevel` (25
  / 49); the arc band; `interplay.enabled`; the `SkillExpressionReport`
  thresholds and the novice-response probabilities (5.4).

### 14.2 Content validation

The build fails when any of these fail (VG-11): `SpellTierBound`,
`SlotlessCastBound`, `PayoffBound`, `CampOptionBound` (scoped by ARC-2),
`AreaCapByLevel`, `AreaUpcastSkipsFlatTier`, `SculptCap`,
`RadiusStepsPerCast`, `AtWillAllySafe`, `ArcanistDamageTypes`,
`VariantIsScroll`, `OneTemplatePerDraw`, `DrawSatisfiable`,
`ConcentrationOnlyControl`, `L2DrawPlaysAlone`,
`StarterDefaultsHaveAnswer`, `SeedHasTarget` (with `seedTarget`
`twoEnemiesWithinRadius1`), `PrimeSelfInert`, `FastnessBand`,
`BinderFettersOnly`, `RespiteHolds`, `RebindConserves`; the Vanguard checks as written
(`TurnAllowanceNoDamage`, `FlagNotCondition`, `RankAddsNoDice`,
`RankKeepsBounds`); and the stage-1 checks (`ForbiddenClassGrant`,
`LeanSpread`, `SubclassCopiesBaseLean`, `SubclassAddsPool`,
`NameCollision`, the cap of eight, the copy registry). The runtime
invariant `KnownVariantInPair` (7.3) is asserted in play and property
tested, not a content check.

These run as `content.*` rows under classes 12.2 (pre-generation, no V
id); `NameCollision` runs as `V7.NameRegistry`, which this document's
names feed; `SeedHasTarget` (classes owner) is executed in V12g
(seeds and validation design 5.2, SV-20; M1 / M2).

### 14.3 Simulator reports owed

- **`SkillExpressionReport`** (5.4): four arms, the modifiers-off runs,
  per-pair, per-subclass and per-card rows, the dominant-combo and
  trap-card flags, the diagnostics; the novice floor-1 row read against
  its escalation threshold;
- `Floor1ClassSpread` per pair × key × subclass, every legal starter
  combination printed (Opposed first); the Dex −1 basic and novice rows;
  per-class floor-1 death share against DH 18.2 (reference arm);
- the per-round breakdown of the solo boss (classes 5.6): signature
  share in rounds 2+ of the floor-1 boss fight, by level 1–3 and
  subclass;
- the draw's relaxation log (`DrawSatisfiable`, 7.7);
- parity at the worst legal build, solo and party, per subclass and per
  pair, under `SetupAttribution`;
- the capstone nova bound with each subclass rider, under the
  same-reading denominator;
- camp share under the three measures, and the 60% rule in the offer
  value unit;
- `Floor1BossSustain` sensitivity: door-HP and potion medians at Con −1,
  reported separately;
- Weft split and friendly-fire share in the reference party (the stage-1
  Weft report);
- control reliance (classes 6.1 item 5) for the Binder; party uplift on
  primed targets for the Alembic;
- the per-theme terrain-presence report (DEP-A1);
- off-pair scroll-read share.

## 15. Staging

**(lead's call)**

| Wave | Content |
|---|---|
| **M1** | Nothing. No M1 rule is needed for the Arcanist |
| **M2** | Section 12: the whole core kit; the full spell list; the Binder and the Alembic; Interplay (behind its lever); Temper, Fray, Afterhold, Flare II; AM-1, AM-2; `SkillExpressionReport`; all validators; the `classTags` mapping |
| **M2 prerequisites owed by other owners** | EL-1 (the meaning of "ignores resistance"); LM-0 (Study persistence); the arc ruling (ARC-14, written: stat 8.1); ARC-17 (Restrained's rank, written: conditions 12.3); the M2 hazard-edge rules (Reel, Gust, Upheaval; written: conditions 10.1–10.3) (cross-document comparison 2026-11-23, CC-77); the per-theme terrain-presence report (DEP-A1) |
| **M2 cut fallbacks** | Runoff waits on the payout-event hook (Wide Mix alone ships); Interplay can be cut by its lever; if `graceTurns` slips, Fastness lengthens `saveEnds` caps only |
| **M3** | The Loremaster and Elementalist unlock aspects (the Elementalist first if the resistance-share report is clean) |
| **M4** | The dip report for Weft 3 at class level 1 (multiclass envelope) |
| **Later** | The 8.8 reserves; AM-4, AM-5, AM-6, AM-12; the element engine; Lace, Fork, Quickcast; Seal (the Tincture fallback) |

## 16. Risks

| Risk | Mitigation, report and lever |
|---|---|
| **Novice floor 1.** The real gap is slot discipline and the pip trap; the defaults may not close it | The novice row with its escalation threshold (5.4, **agreed** Q1); the lever order (11.8); back to the client if the levers run out |
| **Pick-mean drag.** Six pairs, two keys, two subclasses: a trapped pair (Opposed, Etch) or subclass can pull the class mean below the Shade's line | Per-row reports (Opposed rows first); `StarterDefaultsHaveAnswer`; the Opposed starters chosen not to cancel (7.14); honest pair cards; AM-12 lever |
| **Terrain dependency.** E3–E4 need water, oil, fire, web and hazard squares near fights; the room terrain design's KP2 commits the generator (cross-document comparison 2026-11-23, CC-77) | DEP-A1 with a requested target *(illustr.)* of at least one interactive square within 6 of the start in about 40% of non-boss rooms on floors 2–5; until then no card relies on terrain (Squall is Expert for this reason). **Pending request (room terrain design TR-21, 2026-11-21; the Arcanist owner rules):** DEP-A1 counts interactive features (condition squares, contact hazards, harm squares, Kegs); the 3–6 ring rule (KP2: every F2–5 fight-room layer has a mandatory interactive feature at path distance 3–6 from some `partyEntrySquares` square); which "start" is meant (the report reads party entry until ruled; room terrain design 6.2.4, 11.3, TN14). Told plainly: E3 combos still need condition squares, which the Supper never has. **Pending request (hazards design 6.15, 18, O13; HZ-31; M2; the Arcanist owner rules; cross-document comparison 2026-11-23, CC-7):** Stirs, Cords, Voids and vents count as interactive features for DEP-A1 and KP2 (the room terrain owner defines the term); and a second Gust charge in Void rooms (`ArcanistComboShare`); neither in force until ruled (19) |
| **Sustain recalibration.** The Arcanist at Con −1 passes `Floor1BossSustain` by 1; an upward key recalibration fails it first | Door-HP medians reported separately (11.3) |
| **Ceiling invisible on run 1.** Floor 1 is mostly Mote turns | The ladder preview and Interplay sell the later depth |
| **Interplay unproven.** It may mostly fire cancels in the Opposed pair, or drift parity | Modifiers-off rows; the lever (10.3) |
| **Nova thin cases** (1.89, 1.95) | Rerun under the same-reading denominator (11.6) |
| **Reading load.** The Weft row holds Whittle (3 modes), Flare, Knit and a subclass option | Tray greying and waves; the "Intricate" label's price |
| **The inert rule feels like a bug** | Card text up front; greyed payoff line with its reason; Catalyse as the taught solo combo; ALM-1 tests (13.1) |
| **Off-pair scrolls** dilute the pair identity | Bounded as consumables; reported |
| **Dependencies on other owners:** EL-1, LM-0, the arc ruling and ARC-17 (both written; cross-document comparison 2026-11-23, CC-77), the Herald's stage 2 for `classTags` (answered by the Herald design 8.8, HR-24; cross-document comparison 2026-11-23, CC-75), the payout-event hook for Runoff, the ability 8.4 aura ruling | 19; M2 cut fallbacks (15) |
| **Dip value** of Weft 3 at class level 1 | M4 report |
| **All evidence is proxy** | Every number is owed a simulator rerun before it settles |
| **Recall-based survey and SRD shapes** | Check against SRD 5.2.1 before data |

## 17. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: pool sizes inside the shape of 7.10, Mote
  dice inside the parity rows, spell dice and areas inside
  `SpellTierBound`, `drawWeight`, the arc band, lane weights and
  `offerLaneAdjust`, trigger ids, the
  `SkillExpressionReport` targets other than the agreed 8-point novice
  threshold, report thresholds and lever sizes. The starting coin is no
  longer tuning: exactly 15 (starting kits design 4.2; change SK-1).
- Level-up card, glint, pip, tray, preview-line and tooltip layout.
- Wording of option, spell and trigger texts within the rules here,
  until the naming pass.
- **Not tuning:** the simple-core spine; three things to read; the
  element pair fixed for the run and the Mote key; one camp spend per
  turn; slot tiers as settled; Slot Renewal 2 as a class feature at 9;
  Overchannel's approved text (tier ≤ 3, one free Whittle); priming once
  per round; the inert rule **(agreed, Q3)**; the novice row as a
  required report with its 8-point escalation and no new gate **(agreed,
  Q1)**; the accepted level 1–2 camp miss **(agreed, Q2)**;
  `castDefaults` from static data only; no hero suggestion; Binder stops
  gain reliability only; Slipped; no new condition; one
  `restDefensiveTool` (settled).

## 18. Changes to approved documents

Applied on 2026-11-20, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and the rows owed by
other owners (19; LM-0 and EL-1 are owed at M2 whatever ships, 15) are
recorded as such. The unlock-only rows (LM-1 and LM-2 for the
Loremaster, EL-2 for the Elementalist) are recorded now and apply only
when that aspect ships. Each change names the exact section it touches.
The client's Q1–Q3 answers (2026-11-20) approve the rows that name a
question; rows marked "told plainly" were accepted with Q1 or told with
the approval request (20.1) and confirmed by the client on 2026-11-20;
the rest were confirmed with the document on 2026-11-20.

**Narrowings recorded inside this design (no approved text changes;
recorded in classes 18):**
- priming stays once per round; Overchannel stays tier ≤ 3 with one free
  Whittle;
- the pair is never re-chosen; the camp swap is one spell inside the
  pair;
- Shocked stays Later; the arc is the lightning rider;
- Binder stops gain reliability only, never duration;
- Afterhold never applies to Dazed, other turn-part majors or hard
  instances;
- the ignore-resistance offer is excluded while the Elementalist owns
  Purity.

| # | Document / section (current status) | Change | Approved by |
|---|---|---|---|
| ARC-1 | Classes 8.2 rest-tool table ("Shape, guard or mend?"), 8.5 Arcanist card (Weft bullet, Overchannel example, signature event "a Shape used", and the "Weft report and lever" bullet: "Shape, Flare, Knit"; "leaving Weft for Shape and Flare"), 10 rename table ("Shaping → Weft: Shape") (lead's calls) | Weft "Shape" → **Sculpt**, forced by `NameCollision` with weapons 17.8's rod verb; passed to the naming pass (renamed **Whittle**, naming pass NP-32) | Lead |
| ARC-2 | Classes 6.4, single-target damage cap cell and area row; classes 12.2, validator list; weapons 6.4, parity targets, "Any build ≤ 110%" row (the VG-12 text); Shade design 7.5, Arts bullet (SH-5); Vanguard design 7.3 (lead's calls) | Add **`SpellTierBound`** (slot pools: ≤ 2000‰ single target, ≤ 1250‰ per target for areas, a stated departure from enemies 6.2's 120% for the Detonation (fire) scroll; hard clause on unconditional hard control only, SP-1), **`SlotlessCastBound`** (Overchannel) and **`PayoffBound`** (one setup step per target, save penalties included; ALM-3); scope `CampOptionBound` to non-slot, non-slotless camp pools; the parity text reads "the nova bound and `CampOptionBound`, or for slot pools `SpellTierBound` and for slotless casts `SlotlessCastBound`" | Lead (required) |
| ARC-3 | Ability 6.3 (reading); weapons 6.4 parity row; Vanguard design 10.1 (the VG-12 reading); classes 6.4 single-target cap cell | Slot Renewal counts as rest power in the camp share and in VG-12 parity alike (read beside ARC-16) | Lead |
| ARC-4 | Classes 8.2, camp-pool chassis bullet; classes 11.B, camp-share row; ability 6.3; Vanguard design 10.4 (VG-13); Shade design SH-28 | The accepted camp-share miss extended to Arcanist levels 1–2 (26% and 35%). **Widens an agreed reading** | **Client (agreed 2026-11-20, Q2)** |
| ARC-5 | Armour 10.4, "Candidate order" step 3a; Shade design 8.3 (the SH-9 definition) | Step 3a becomes "the target's own class-pool-paid wards" (Sworn Guard, `countsAsParry: true`; Flare, `countsAsParry: false`), both before step 3b | Lead |
| ARC-6 | Ability 7.3, policy filters and policy classes; ability 11.3 (**agreed**, reading only) | New filter `reserveForRestTool` (reserve 1 Weft while a Knit use is left; a pre-tick that protects an ally ignores it); a new policy class **cast-option defaults** (visible, shown ticked, the player can untick them; static predicates only); the reading that these are not suggestions under 11.3 | Lead; **told plainly, accepted with Q1 (2026-11-20)** |
| ARC-7 | Conditions 10.1 (verb-sourced collisions) | Gust and Reel count as verb-sourced for collisions, with the source-held `movedByYou` round count instead of the shared verb mark | Lead |
| ARC-8 | Ability 8.2 (at-wills never hit allies); stat 8.3 ladder (**agreed**); conditions 18.6 Soaked "Effect" line and 19 "ladder" Soaked row | At-will arcs never reach allies (8.2 wins); a selective cast leaves allies out of all its effects, arcs, forks and forced movement included (SP-6), and selective may be taken on any slot cast whose arcs or forks reach an ally; you are never affected by your own casts' areas, arcs and forks | Lead; **told plainly with the approval request (20.1); confirmed by the client 2026-11-20** |
| ARC-9 | Ability 8.1, area cap | Area ceiling 49 squares at L7–10 | Lead |
| ARC-10 | Classes 8.5, "At-wills" bullet | Mote = 1d10 + Int, two beams from L5; Gust a line L4 push 1 | Lead |
| ARC-11 | Classes 8.5, Pools bullet, Weft sub-bullet (*illustr.*) | Weft 3 / 4 / 5 / 6 at L1 / 5 / 7 / 9; Whittle II at 5; Flare II at 8 | Lead |
| ARC-12 | Ability 6.5, Slot Renewal bullet; the Arcanist `offerExcludes` | Slot Renewal 2 as an Arcanist class feature at L9; the offer excluded; `timescale:slots.*`, capacity and timescale on Overchannel excluded | Lead |
| ARC-13 | Classes 7.4 (creation defaults, badges); classes 12.1 (record fields); DH 17.7 (confirmation pattern) | `castDefaults`, `defaultRotation` (schema, 5.2), `pairDefaults`, `recommendedPickOrder`, creation warnings, the starter rule, the last-slot confirmation (predicate (a)–(e) and per-tier message, 5.2), the pip glint, the default target filter if lever 1 is used | Lead; **told plainly, accepted with Q1 (2026-11-20)** |
| ARC-14 | Stat 8.1, lightning rider; ability 9.3; trinkets (Storm Ring arc ruling) | Arc damage = the rider strength band, one damage event per arced unit, no crit, flag or re-arc; an at-will's Soaked arc spread at most once per turn across beams | Lead |
| ARC-15 | Classes 5.6, `Floor1ClassSpread` "Input" sub-bullet; classes 7.4, row 1 | For the Arcanist, a creation pick is pair × Mote key (12 picks), crossed with the level-3 subclass as classes 5.6 already does; starters held at `pairDefaults`, every legal starter combination printed; the class mean unweighted; content check `StarterDefaultsHaveAnswer` and the starter rule | Lead; **told plainly, accepted with Q1 (2026-11-20)** |
| ARC-16 | Weapons 6.4, parity row; Vanguard design 10.1 (VG-12 reading); read beside ARC-3 | `SetupAttribution`: the value a camp-sourced condition adds to your own at-wills is charged to the source spell's row in the parity accounting (simulator and arithmetic model), not to the at-will's; the static `SpellTierBound` is unchanged by it (11.1) | Lead; **told plainly, accepted with Q1 (2026-11-20)** |
| ARC-17 | Conditions 12.3 data | Restrained's `controlRank` at or above Dazed's | Lead |
| ARC-18 | Classes 3.4 (terms), 7.6 (reading), 8.5 (Arcanist card); ability 9.3 | Interplay (base passive, `interplay.enabled`, on by default) and Fused | Lead |
| ARC-19 | Ability 14.3, trigger kinds; ability 7.3; armour 10.4 (analogue) | Reaction trigger kind `ownSave`; at most one reaction save bonus per save | Lead |
| ARC-20 | Classes 10, NameCollision exception | Exception extended to wand spells; template + family-tag display names; variants exempt from the head-word warning (SP-3) | Lead |
| ARC-21 | Classes 7.4, spell-card-draw bullet; classes 7.5 (offer rows) | The draw deals variants (template × family, family fixed on the card), at most one variant per template (SP-4); `drawRequires: offFamily`; a Recommended card; Expert cards out of the L2 draw; a tier-4 signature at L10; the relaxation order and `DrawSatisfiable` (7.7); `L2DrawPlaysAlone`; the Knit lever excludes row-adding offers; the spells-known table | Lead |
| ARC-22 | Classes 12.2; Vanguard design 6.4, rule 6 | SP-1 (hard clause on unconditional hard only), SP-2 (upcasts at arrival level; `AreaUpcastSkipsFlatTier`), SP-5 (each target of a save area is its own hit for the 3-square sum), SP-7 `VariantIsScroll` over the compared field set (8.1) | Lead |
| ARC-23 | Conditions 18 "Sources" lines: Soaked (Drench, Undertow, Prime, Tincture), Oiled (Prime, Wide Mix), Restrained (Snaring, Shackle, Cordon), Frozen (Rime Lance, Deep Freeze) | Source rows | Lead |
| ARC-24 | Consumables 16.4, 16.5 (`classTags`; the Rime Lance note and the Wand of Rime Lance's "depth gates" words); consumables 19 | The 8.10 mapping; Later defaults for Herald-dependent items; the wand reads "Steeled and resolve apply" | Lead |
| ARC-25 | DH 17.7 (DH 10.4 related) | Forced movement of an ally into a hazard square or over a hazard edge asks for confirmation, lethal or not; DH 17.7's lethal test is unchanged | Lead |
| ARC-26 | Ability 10.2, offer kinds; Vanguard design 7.9 (VG-10) | No capacity or timescale offer targets a turn allowance (ALM-6 generalised) | Lead |
| ARC-27 | Classes 12.2; the registry note | The content check `ArcanistDamageTypes` (parts in fire, cold, lightning, acid, physical) scoped to class records; the runtime invariant `KnownVariantInPair`; gear shifts (the Ember and Frost Rings, Attuned) the only exempt path, in the registry note | Lead |
| ARC-28 | Ability 9.5; conditions 18.2 (Concentrating) | Afterhold (fetters and minors; never Dazed or hard; barred for the Binder) | Lead |
| ARC-29 | Classes 12.3, simulator reports | `SkillExpressionReport` (four arms, modifiers-off runs, targets, flags); the novice floor-1 row required, with the 8-point escalation; **no new gate** (classes 5.6 unchanged) | **Client (agreed 2026-11-20, Q1)** for the row's status; lead for the report |
| BN-1 | Conditions 5.1 (`saveEnds`), 17.3 (AI duration term) | `graceTurns` on `saveEnds` | Lead |
| BN-2 | Conditions 18.2; ability 9.5 | Belay | Lead |
| BN-3 | Vanguard design VG-11, `FlagNotCondition` list | `slippedFrom`, `boundBy`, `cinched` | Lead |
| BN-4 | Ability 14, cost timings | An own-turn, no-action Weft spend that marks a target | Lead |
| ALM-1 | Classes 8.5, Alembic bullet ("An Oiled-plus-fire chain needs a slot"); **stat 8.2 and 8.3 (agreed)**; conditions 18.6 Soaked and Oiled rows and 19 ladder, cancel and override rows (readings) | The inert rule widened from Oiled-plus-fire to every prime and every at-will reading (9.2), with the rider sentence: your at-will's rider is not attempted where a cancel, override or wash row would fire against your inert reagent (lead's call, inside the agreed rule) | **Client (agreed 2026-11-20, Q3; told plainly)**; the rider sentence told plainly with the approval request (20.1) and confirmed by the client 2026-11-20 |
| ALM-3 | (folded into ARC-2) | `PayoffBound` | Lead |
| ALM-4 | Conditions 17.2, instance fields | Instance field `origin: prime` | Lead |
| ALM-5 | Ability 9.3, note | Prime is the hit's rider (gate and slot), offered once the hit is known, outside the at-will once-per-turn bound | Lead |
| ALM-6 | (generalised as ARC-26) | No capacity or timescale offer on `prime` | Lead |
| ALM-7 | Registry (trinkets 11) | Tincture against the Water Flask's ally use; fallback Seal | Lead / registry |
| ALM-8 | Ability 7.3 (with ARC-6) | `castDefaults.prime` as a static predicate (9.2: no displaced rider, a payer actually available, own at-wills excluded, once per fight solo, per-pair default reagent) | Lead |
| ALM-9 | Classes 8.5, Alembic bullet ("a cantrip hit makes its target Soaked or Oiled") | Priming may also ride the Alembic's slot spells, Overchannel and named triggers (9.2); still once per round, still a Weft option | Lead |
| AS-1 | Classes 7.3, the "aspects that change one rule" bullet | The aspect shape: one rule, the full ladder | Lead |
| LM-0, LM-1, LM-2 | Enemies (Study persistence and difficulty); enemies 9.3 (`cooldown.delay`); ability 11, 13 (scorer term) | Study persistence; `cooldown.delay`; the scorer term (LM-0 owed at M2; LM-1 and LM-2 unlock-only, applied when the Loremaster ships) | Enemies owner / lead |
| EL-1, EL-2 | Stat 8.4; the aspect | The meaning of "ignores resistance"; the heart family is not the Mote key (EL-1 owed at M2 whatever ships; EL-2 unlock-only, applied when the Elementalist ships) | Lead |

**Not changed (checked):** DH 21.1 step 6 (it already reads static keys;
variants are reports); armour 11 item 2 and AR-20 (11 stands); the HP
tier; the armour ceiling (**agreed**, stage 1 Q5); classes 5.6
(**agreed**; no new gate, Q1); DH 18.2; ability 11.3 (**agreed**; read,
not changed); ability 8.4 (**agreed**; no ground zones); CN-11
(**agreed**); priming once per round; Overchannel's stage-1 text; DEP-1;
the 32-condition budget.

**Check after applying (lead's call):** a grep for "a Shape used" in
classes 8.5 finds only history; a grep for "Shape, guard or mend" in
classes 8.2 finds "Whittle" (Sculpt until the naming pass, NP-32); a grep for `CampOptionBound` in weapons 6.4,
classes 6.4 and 12.2, the Vanguard design and the Shade design finds the
scope note beside `SpellTierBound`; a grep for "Oiled-plus-fire chain
needs a slot" in classes 8.5 points to the inert rule; a grep for "Weft
(`rest` 2 / 3 / 4" in classes 8.5 finds only history; a grep for "offer
may raise it to two" in ability 6.5 finds the Arcanist exclusion.

## 19. Depends on systems not yet designed

- **Dungeon generation and terrain (DEP-A1):** a per-theme
  terrain-presence report and the requested target (16); no card relies
  on terrain until then. **Pending request from segment 3 (room terrain
  design TR-21):** rule whether DEP-A1 counts interactive features, the
  3–6 ring rule, and which "start" is meant (16); the floor-4 miss is
  likely closed by the Supper's pieces (room terrain design 7.8).
- **Hazards (segment 4): pending request (hazards design 6.15, 18, O13;
  HZ-31; M2; the Arcanist owner rules; cross-document comparison
  2026-11-23, CC-7):** a second Gust charge in Void rooms
  (`ArcanistComboShare` reported); and the DEP-A1/KP2 extension, under
  which Stirs, Cords, Voids and vents count as interactive features (the
  room terrain owner defines the term, 16). Neither is in force until
  ruled; Gust stays as 7.2.
- **The enemies owner:** LM-0 (Study persistence), LM-1
  (`cooldown.delay`); Fray's mirror never in floor-1 monster kits; the
  AI pricing of Afterhold and Belay.
- **The conditions owner:** the arc ruling (ARC-14); Restrained's rank
  (ARC-17); `graceTurns` (BN-1); the payout-event hook (Runoff);
  the M2 hazard-edge rules. ARC-14 (stat 8.1), ARC-17 (conditions
  12.3), BN-1 (conditions 5.1, 17.3) and the hazard-edge rules
  (conditions 10.1–10.3) are written; the payout-event hook stays owed
  (cross-document comparison 2026-11-23, CC-77).
- **The stat-model owner:** EL-1, owed at M2 whatever ships.
- **The armour owner:** the step-3a wording (ARC-5) with the Shade's
  SH-9.
- **The consumables owner:** the Wand of Fetters loot reading; the Rime
  Lance note and the wand's "depth gates" words (ARC-24).
- **The registry owner (trinkets 11):** the remaining rulings named in
  7.15 and Tincture against the Water Flask (ALM-7); Prime against the
  Water and Oil Flasks is ruled here (7.15).
- **The Herald's stage 2:** claims or declines the Herald-dependent
  `classTags` (8.10); unclaimed items default to Later. Answered by the
  Herald design 8.8 (HR-24); the 8.10 values are final (cross-document
  comparison 2026-11-23, CC-75).
- **The floor-pacing owner:** lever 5 (boss door at level 3), shared
  with the Shade's request. Answered by the Forecourt Top-up (run
  structure design 9.4, RT-8): lever 5 is in force, so the levers are
  run out after 1–4 (11.8; cross-document comparison 2026-11-23, CC-72).
- **Exploration and information (segment 10):** **pending request
  XI-27** (M3): Walls Have Ears, a batched Listen (8.8; exploration and
  information design 4.5.2); the Arcanist owner rules.
- **Companions:** an Arcanist recruit kit as a signature slice that does
  not copy Overchannel or the subclass Arts. Answered by the companions
  design (`2026-11-20-companions-design.md`, approved 2026-11-20): the
  Tinker and Hedgemage kits carry no Overchannel or Arts (8.3);
  scorer-chosen beam order and Prime's solo clause read as in its 8.6.
- **Starting kits:** answered by the starting kits design
  (`2026-11-23-starting-kits-design.md`, segment 14, approved
  2026-11-23); the kit hooks (7.11, 7.14, row 14) are taken up: SK-1
  coin exactly 15 (M1); SK-9 creation warnings (M2); the Arcanist
  variants (Farcaster, Bookhand) are M3 boon options, gated on the
  weapons owner's mundane orb and tome bases, else M4.
- **Heirlooms and curses (segment 15): pending request (heirlooms and
  curses design 5.1, 11, 17; approved 2026-11-23; M3; the Arcanist owner
  confirms):** the Arcanist's R1 heirloom, the Old Ash Staff (staff,
  two-handed as a focus), carries **Kindly Weight**: once per fight, when
  you end an enemy's concentration, your next spell this fight costs 1
  less from its class pool. Not in force here until confirmed; no row of
  that design's 13 changes this document. Its F10 renames keep this
  document's names: Undertow stays the cone (the heirloom line is Low
  Tide) and Steady Casting stays the staff's (the line is Long Look).
- **The simulator:** the four arms, parity, nova, spread, camp share,
  sustain sensitivity. The arms become the `Skill` axis of the seeds
  and validation design (6.2; **pending request SV-22**, 5.4).
- **The ability 8.4 aura ruling:** AM-6 and the self-aura reserve.
- **Naming pass:** every placeholder name here. Done: the naming pass
  design (approved 2026-11-24; NP-32) renamed this document's names.
- **Lead's checks before data:** every SRD shape in this document
  against SRD 5.2.1 (Ice Knife, Chromatic Orb and Chain Lightning
  first); `NameCollision` over the final names.

## 20. Open items for the client

None. Approved by the client on 2026-11-20. The future items in 20.2,
which come back only if a report or a later design triggers them, and
the naming pass remain future items.

### 20.1 This document

The three questions this design raised were answered on 2026-11-20
**(agreed)**, each with the recommended option:

- **Q1, the floor/ceiling principle:** option A, "Easy to cast, hard to
  master: equal survival floor, higher reading floor, highest ceiling."
  The novice floor-1 row of `SkillExpressionReport` is a required report
  with an escalation threshold (more than 8 points behind basic after
  the defaults and previews → the lead applies the levers in order → if
  they run out, back to the client); no new gate. The client was told
  plainly, and accepted with it: that its prediction is right today
  (novices 16–29 points behind on floor 1 in the proxies); that the
  pre-ticks and the last-slot confirmation are defaults and
  confirmations of the player's own action, not suggestions, under
  agreed ability 11.3; and the creation-pick definition for
  `Floor1ClassSpread` (ARC-15) and `SetupAttribution` (ARC-16).
  Rejected: B (the novice row as a gate) and C (accept a higher survival
  floor).
- **Q2, the level 1–2 camp share:** accept the miss (26% and 35%) as
  VG-13 and SH-28 did (ARC-4). Rejected: Weft 2 at levels 1–2.
- **Q3, the Alembic's inert rule:** option A, "your mix needs a spark"
  (9.2; ALM-1). The client was told plainly that this re-reads agreed
  stat 8.2 and 8.3 for one subclass's own at-wills. Rejected: the
  reagent restriction.

**Told plainly with the approval request (new lead's call); confirmed
by the client on 2026-11-20:**
ARC-8 reconciles agreed texts: ability 8.2's "at-wills never hit allies"
wins over the Soaked "arcs to every adjacent unit" row for at-wills, and
a selective cast leaves allies out of every effect, arcs and forced
movement included, and selective may be paid on a single-target slot
cast whose arc on a Soaked target would reach an ally (unselected, that
arc still reaches allies as the row says). It narrows how the agreed
stat 8.3 ladder row reads for allies; it changes no number. With it,
you are never affected by your own casts' areas, arcs and forks (8.1).

Also told plainly and confirmed by the client on 2026-11-20: the inert
rule's rider sentence (9.2; ALM-1) applies
the agreed Q3 rule to condition-to-condition rows as well. A rider your
own at-will would apply is not attempted where a conditions 19 cancel,
override or wash row would fire against your inert reagent, so the bolt
can neither set off nor strip your own mix. It reads the settled
conditions 19 rows for one subclass's own at-wills, as Q3 already does
for the damage rows, and changes no number.

**New lead's calls in this revision** (beyond the proposal the client
said stands; listed so the approval covers them; confirmed by the
client with the document on 2026-11-20):
- the escalation's procedure (5.4): when the novice row is read, that
  the class mean escalates and per-row misses get per-row changes, and
  one lever at a time with a rerun after each;
- the level-3 default rule (9.1): whichever subclass has the higher
  novice floor-1 rate at the escalation point; a tie keeps the Alembic;
- the class mean for `Floor1ClassSpread` is unweighted over pair × key ×
  subclass (7.7);
- the rejected Q2 and Q3 options are not held as levers (11.8, 9.2);
- `interplay.enabled` ships on, and turning it off is the lead's lever
  (10.3);
- the Elementalist's level-6 alternative is named **Aftershock** (9.4);
- the modifiers-off run beside each arm (5.4); the AM-1 inert-rule
  preview line (10.2).

**Further new lead's calls from the check round** (each makes agreed or
standing text buildable; none changes a client decision; confirmed by
the client with the document on 2026-11-20):
- one spell-card model, the draw dealing variants, with a relaxation
  order and `DrawSatisfiable` (7.3, 7.7); Streak a full template (8.4);
- `boundModel`, `AreaUpcastSkipsFlatTier` and the `VariantIsScroll`
  field set (8.1, 11.2), with Shard's, Fan's and Spatter's upcasts
  brought into line and the Wand of Rime Lance's "depth gates" words
  amended (ARC-24);
- `SetupAttribution` placed as a parity-accounting rule, with
  `SpellTierBound` unchanged by it (11.1; ARC-16);
- every arm enumerates pair × key × subclass, so the level-3 default
  moves no class-mean row; the novice responses; the new lever 1 (a
  default target filter for major control) ahead of the others (5.4,
  11.8);
- the last-slot confirmation's predicate, narrowed so it is rare (5.2);
- the combo glint narrowed to the target's state, naming no ability
  (10.2), so that it stays inside the agreed ability 11.3 reading the
  client accepted;
- Interplay's data rules and the Mote's beam order (10.3, 7.2);
- the Alembic: Prime timing, the inert rule's rider sentence inside the
  agreed rule, Catalyse precedence, the rewritten `castDefaults.prime`,
  Splash, Wide Mix, Runoff and Reagent Surge details, `tinctureOnBurning`
  deleted, ALM-9 (9.2);
- the Binder: second-instance exclusion and Rebind's durations and
  policy (9.1);
- you are never affected by your own areas; selective may be taken on a
  slot cast whose arcs or forks reach an ally; the party friendly-fire
  defaults (8.1, 7.5, 5.2, 7.14);
- the starter rule disables a no-damage start; the Opposed starters
  (7.7, 7.14); Knit has no hero auto-policy (7.14).

The client approved the document as a whole on 2026-11-20, including
every lead's call and both told-plainly items above.

### 20.2 Future items triggered by reports

These come back to the client only if a report or a later design
triggers them:

- the novice floor-1 row still more than 8 points behind basic after the
  levers in 11.8 run out (Q1);
- the inert rule failing its tests or parity rows (Q3; the rejected
  fallback is not applied without the client);
- a `Floor1ClassSpread` miss whose levers run out (classes 5.6);
- a floor-1 death share above about 5% for the Arcanist (DH 18.2),
  read on the reference arm as DH 19.3 does; the novice death share is
  report-only and feeds the 5.4 escalation;
- a hero coach, only if the client asks (ability 11.3);
- the Arcana mastery signature use (identify magic items on pickup) when
  the Loremaster ships;
- the naming pass.

## Appendix A: how the critique findings were resolved

*Names in this appendix are kept as written at approval; the naming
pass (approved 2026-11-24, NP-32) has since renamed Sculpt (Whittle),
Tempest (Weatherwork) and Slot Recovery (Slot Renewal), among others
(front matter).*

**Research conflicts (computed brief against approved text):**

| Finding | Resolution |
|---|---|
| Priming "once per turn" | Once per round (stage 1 3.4, 8.5) |
| Overchannel "tier 1 or 2" | Tier ≤ 3, one free Sculpt (stage 1 8.5, R-19) |
| "Camp swap" of the pair | The settled one-spell swap inside the pair; pair fixed |
| Shocked listed | Later; the arc is the lightning rider |
| "Shape" name | Sculpt (ARC-1) |
| `CampOptionBound` catches slots | ARC-2 |
| Hero Coach | Not proposed (ability 11.3) |
| Area ceiling at L7–10 unstated | ARC-9 |

**Judges' grafts:** all adopted as listed in 6.3, except: the element
engine and spellcraft modifiers held Later; AM-4, AM-5, AM-6, AM-12
Later or held; AM-9 folded into Flare II.

**Numbers check (F1–F13 of the numbers check):** F1 → ARC-16 with band-1
fallback; F2 → ALM-1 (**agreed**, Q3); F3 → 3d6 Spatter, SP-1, SP-2; F4
→ ARC-2; F5 → `PayoffBound` one step per target; F6 → ComboBound not
adopted ("first damage event" kept in `riderOnUpcast` and Rays wording);
F7 → Slot Recovery 2 at L9; F8 → ARC-4 (**agreed**, Q2); F9 → reports
beside the static rule; F10 → reports (AR-20 stays 11); F11 → novice row
(**agreed**, Q1), defaults, levers; F12 → Knit lever excludes row-adding
offers; F13 → Interplay AM-3 wording, arc ruling ARC-14, Afterhold never
on Dazed, pip-aware defaults.

**Critic: fun**

| # | Finding | Resolution |
|---|---|---|
| 1 | Slot discipline and pip trap | Last-slot confirmation; pip-aware defaults and glint; novice report required with escalation (**agreed**, Q1); Alembic default (a creation default; it does not move the class mean, 5.4); the default target filter as lever 1, Snaring as lever 2 |
| 2 | "Bolt simulator, one element" | Off-key default starter; `drawRequires: offFamily`; Interplay; the ladder line |
| 3 | Terrain may not exist | DEP-A1 report and target request; no card relies on terrain |
| 4 | Bloat and trap cards | Expert badges, weights ≤ 600, out of the L2 draw; `L2DrawPlaysAlone`; Drench gains 2d4; AM-4 Later |
| 5 | Binder moment weak | Odds-swing preview; glints; "bind the guard"; Rebind default. The optional pip buff is held as a lever |
| 6 | Alembic inert rule feels like a bug | Card text and greyed payoff line with reason; Catalyse as the taught solo combo; Prime II renamed Splash |
| 7 | Naming chaos | Name table (front matter); Linger → Afterhold; Storm label → Tempest; Undertow kept for the cone, AM-5 → Converge; Detonate not adopted |
| 8 | Weft small | Flare filter `bigHit`; Knit lever exclusivity |
| 9 | Dead levels | Flare II at L8; Slot Recovery 2 at L9; tier-4 signature guaranteed at L10 |
| 10 | Capstone reads as a free slot | Fused under Interplay |
| 11 | Pair balance | Per-pair rows; honest cards; AM-12 lever |
| 12 | Off-pair scrolls | Kept, stated, reported |

**Critic: balance**

| # | Finding | Resolution |
|---|---|---|
| C1 | Gate averages every pick | ARC-15; `StarterDefaultsHaveAnswer`; per-row reports |
| C2 | Parity text routes slots to `CampOptionBound` | ARC-2 amends all three texts |
| C3 | Restrained's rank | ARC-17: at or above Dazed |
| H1 | Setup leak | ARC-16; ARC-3 consistency in both measures |
| H2 | Pip trap; Binder default | Pip-aware defaults; Alembic default with the 9.1 rule |
| H3 | Overchannel widened | One free Sculpt; same-reading denominator; `PayoffBound` widened |
| H4 | Arc unspecified | ARC-14 |
| M1 | AR-20 already 11 | Accepted; AC 14 rows are reports; door-HP medians reported |
| M2 | Gust consumes allies' verb mark | `movedByYou` (ARC-7) |
| M3 | Pushes move allies into hazards | Selective removes forced movement; pre-tick on hazard; ARC-25 |
| M4 | Camp share rulings | ARC-3, ARC-4 (**agreed**, Q2); Slot Recovery 2 at L9; no Weft 7 |
| M5 | Control rules | Afterhold limits; tests listed in 8.9; Tincture warning |
| M6 | Reading and cap | Knit lever exclusion; ARC-26 |
| L1 | Dip | M4 report |
| L2 | Forbidden grants | Clean; waivers per rest |
| L3 | Names | Resolved (fun 7) |

**Critic: consistency**

| # | Finding | Resolution |
|---|---|---|
| 1 | Basic arm is not the gate arm | Arms relabelled (5.4); no new gate (**agreed**, Q1) |
| 2 | Pick enumeration | ARC-15 |
| 3 | ALM-1 touches agreed text | Named sections; **agreed**, Q3, told plainly |
| 4 | Per-fight free uses; Cinch III on hard | Unpick III per rest; Prime III is Tincture; Cinch III fetters only |
| 5 | L6 must be 1 of 2 | All four subclasses offer two |
| 6 | 1250 is not a mirror | Stated as a departure; all citing sections named in ARC-2 |
| 7 | AR-20 settled at 11 | Dual-reading work deleted |
| 8 | Defaults versus 11.3 | ARC-6 policy class; static predicates only; Alembic pre-tick rewritten; 7.4 named; told plainly, accepted with Q1 |
| 9 | Slot Recovery 2 inconsistent | L9, offer excluded, ARC-12 |
| 10 | NameCollision gaps | ARC-20 wand exception; ARC-1 places listed; internal collisions renamed |
| 11 | Rod verb dead | Gust as a line L4 |
| 12 | Source lists | ARC-23 |
| 13 | Arcs and allies | ARC-8 (told plainly, 20.1; confirmed by the client 2026-11-20), ARC-14 |
| 14 | Handoffs | Belay (Binder) and Afterhold (base), ARC-28, BN-2 |
| 15 | Armour 10.4 step 3a wording | ARC-5 with Shade 8.3 |
| 16 | Depth gates misread | Claim removed; Rime Lance note updated (ARC-24) |
| 17 | classTags open | Closed with Later defaults; arcane-hint note; Snaring loot reading |
| 18 | Camp-share requests disagree | One item, levels 1–2 (**agreed**, Q2) |
| 19 | Draw-rule section; spells known | ARC-21; table updated |
| 20 | Attuned | ARC-27 |
| 21 | Wording | Interplay wording; Afterhold never on Dazed; `bigHit` reused, one new filter justified |

**Client round (2026-11-20):** Q1 A, Q2 A, Q3 A, each the
recommendation; everything else stands. This revision converts the
options to decided rules, records the rejected alternatives in one line
each (section 4, 20.1), and adds the new lead's calls listed in 20.1.

**Check round (builder, consistency and play lenses, 2026-11-20):** every
finding was fixed or merged; none needed the client. Merged pairs: the
template family and the draw (7.3, 7.7); the flat-tier upcasts (8.1);
`tinctureOnBurning` (deleted); the novice class mean and the level-3
default (5.4); the inert rule's condition rows (9.2); Catalyse's
precedence (9.2); Overchannel's mode count (7.5 rule 4); Streak (8.4);
the last-slot confirmation's predicate and frequency (5.2);
`castDefaults.prime` (9.2). The resulting new lead's calls are listed in
20.1.

## Appendix B: sources

Web-checked in part during research: PF2e kineticist (forum and playtest
summaries); Gloomhaven element infusion and consumption; 2024 wizard
reveal summaries (Memorize Spell, Scholar, Sculpt Spells moved to 6,
Potent Cantrip; sources conflict on Arcane Recovery in SRD 5.2);
Wildermyth wiki (Interfusion slot, Natural Ally); Slay the Spire Defect
orbs and Focus; DOS2 environmental combinations (gamepressure,
Fextralife); skill floor and ceiling definitions (gamedesignskills; Game
Developer "skill ceiling" and "easy to learn, hard to master"). From
memory and to be checked before citation: the 5E warlock and sorcerer,
PF2e wizard, 4e wizard implements, 13th Age, Shadow of the Demon Lord,
Lancer, Spirit Island, Into the Breach, BG3, Magicka. Every SRD shape is
checked against SRD 5.2.1 before records are written.
