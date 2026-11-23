# Classes, Stage 2: The Shade: Design

Date: 2026-11-18
Status: Approved, 2026-11-18
Parents: `2026-11-17-classes-concepts-design.md` (classes stage 1,
approved; the direct parent), `2026-11-18-vanguard-class-design.md`
(the approved stage-2 template; "Vanguard design" below), and through
classes stage 1
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

Items marked **(agreed)** were decided by the client on 2026-11-18 for
the three questions this design raised (Q1–Q3, section 4 and section
19), by choosing the lead's recommendation in each case. The client was
told plainly, before answering Q1, that the tiered band replaces the
stage-1 Opening band (which models at 121–174% of the greatsword
reference) and that, like the Vanguard, the Shade passes ≤ 110% only
under VG-12 plus the strike limit. The client was told plainly, before
answering Q3, that Sworn Guard changes the stage-1 Swordsworn card's
*mechanism*, not its intent. On Q2 the client added "keep it for
later": the client is comfortable with the fallback, and this document
records that **the unlock fallback is the expected default if trap
records are not ready** (14). The client also said that everything else
in the lead's proposal stands as written.

Items marked **(lead's call)** were filled in by the design lead; the
client confirmed them all on 2026-11-18, when it approved this document
as a whole, and they are binding. Items marked
**(settled)** restate a rule from an approved document; **(decided)**
marks a main-design client decision. Items marked **(change SH-n)**
amend an approved document and are listed, with an id and the exact
section they touch, in section 17. Every item here is binding on the
builders.

This revision applies the check round's findings (Appendix A, "Check
round"). Its new judgement calls are marked **(lead's call)**; none
changes a Q1–Q3 answer. The two moved pool steps, the level-4 strike
pip and SH-28 were told plainly to the client with the approval request,
and the client confirmed them with the document on 2026-11-18 (19.1).

This is **stage 2 of the classes design for the Shade**, the launch
Dexterity martial: its full table for levels 1–10, its signature verb
(Opening) and how it grows, its Tricks and Vanish pools, its core kit,
its three subclasses (Cutthroat, Trapwright, Swordsworn), the capstone,
the numbers, the staging and how every binding requirement on "the
classes design" is met for this class. The Shade is an **M2 class** and
has no M1 slice. Classes stage 1 section 14 lists what stage 2 owes;
section 12 below answers it for the Shade.

The client's brief: "I have no further considerations for the Shade, I
like your concepts; again do research on similar classes from other
games and let me know what you come up with." The client also invited
new mechanics where they are interesting, provided each comes with its
cost, the rules it touches, its bounds and a recommendation (section 9).

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. They are scratch
Monte Carlo models, not simulator evidence. The builders own them and
the simulator calibrates them (section 16), except where a value is
marked as a client decision, a settled value or a lead's number.

**Names are placeholders** pending the naming pass (a separate client
item). They are frozen for this document so that documents agree. Every
name new to this document was grep-checked as a whole word against the
13 approved documents and has no hits: Fade, Beset, Unready, Duck and
Catch Breath (stage 1 names kept or given), Strike limit, Sworn Guard,
Counterstroke, Open Guard, Held Guard, Lone Blade, Cross Steel, Turn
Aside, Blade Dance, Quiet Work, No Witnesses, Throat-Cut, Make an
Example, Ambuscade, Jury-Rig, Spring, Toss, Wedge, Killing Ground, Drag
Line, Trick Shot, Tripline, Trap Eye, Caught, Caught Out, Dust, Draw
Out, Clear Head, Tumble Clear, Spare Trick, Long Shadow, False Trail,
Lure, Shrugged Off, Wise to You.

Names avoided because they collide **(lead's call)**:

| Avoided name | Why |
|---|---|
| Slip | "slip past" (ability 7.2) |
| Riposte | The Riposte verb signature (weapons 18.4) |
| Exposed, Marked, Spotted | Conditions |
| Feint | Cut in stage 1 (a Steady-Aim and Luckstone copy) |
| Ambush, Bait, Tell | Enemies 17.4 |
| Hamstring | Hobbled sources |
| Engage | The Engaged awareness state (enemies 12.1); the draft name of Cross Steel |
| Lie Low | Trinkets 11 rule 3 |
| Snuff | Withdrawn with its effect (8.2) |
| Close In | A prose hit |
| Reset, Muffled, Jam | Prose and record hits |
| Carry (as a feature name) | The Instrument's focus verb (weapons 17.8). "The Vanish carry" in this document is prose for an entry in the `carries[]` state (13.1), never a feature name **(lead's call)** |
| Grit | Weapons prose ("grit from rust"); the Toss mode is named Dust (8.2) |

**SRD check (settled principle, classes stage 1; lead's call to repeat
it here).** The SRD-shaped mechanisms used here are a Sneak Attack-like
Opening, a Cunning Action-like Fade, an Uncanny Dodge-like Duck, an
Evasion-like Tumble Clear, Extra Attack, and the weapon verbs that adapt
SRD masteries. They were written from memory, and the research for this
document web-checked only parts of the 2024 Rogue (Cunning Strike,
Assassin, Thief), the Battle Master Riposte and the Swashbuckler, where
sources disagree on details. **Every SRD-shaped mechanism must be
checked against the SRD 5.2.1 document before any record is written**,
and every record flagged `srd` cites the entry it was checked against.
Class, subclass, option and feature names are original; no D&D-only
name or text is used.

**Waves:** M1 is the vertical slice (one class, levels 1–3, solo), M2
the full run with four classes and companions, M3 profile and unlocks,
M4 breadth (species, multiclassing), Later everything after that no
milestone names. **(settled, main design 9)**

## 1. Intent

Stage 1 fixed what the Shade *is*: a Dexterity martial whose verb is
Opening ("I set the fight up so my first blow lands huge, then I slip
away and set up the next"), with stealth and its own tempo as primary
leans, precision damage and dungeon skills as secondary, Tricks and
Vanish as its pools, and three subclasses named. **(settled)** It left
open how Opening's band grows, how the class grows from level 1 to
level 10, how many Tricks and Vanishes it gets and when, what each
subclass lets the player spec into, and what the capstone does in each
build.

The answer in one sentence: **the Shade grows sideways, not in dice.**
It keeps exactly three things to read at every level (at-wills, Tricks,
the Vanish row); Opening has two tiers (a big **stealth** die and a
small **open** amount) and **exactly two ranks**, at level 1 and level 5;
everything after that is a rank on a fixed row, a trigger, one
**signature Tricks option** per subclass that ranks up at 3, 5 and 7,
one **Vanish Art** per subclass from level 5, a level-6 feature that
**rewrites a verb**, and the capstone **From Nowhere**. How often the
Shade is Hidden, not how many dice it rolls, is the parity variable, so
one shared per-rest **strike limit** bounds it. **(lead's call; band
agreed, Q1)**

## 2. Goals, non-goals, anti-goals

### Goals

- **"My first blow lands huge" from turn 1** (the approved identity
  line): the stealth die is 1d6 at level 1 and 2d6 at level 5, and the
  ambush or Hidden blow is visibly the big one.
- **Research-led design** (the client's brief): section 4.1 surveys
  rogue and skirmisher designs and names what is taken and avoided.
- **Growth through what the Shade does with openings,** not through
  precision dice: trades, rackets, ripostes, re-stealth, traps.
- **Three things to read at every level** (settled, ability 11.4;
  classes 7.6), with room for one offer row.
- **A solo boss engine** that does not depend on stealth: Answer and
  Duck (settled, classes 8.4).
- **Three subclasses that are roles:** the quiet kill and the ambush
  (Cutthroat), the rigged room (Trapwright), the duellist who makes
  them miss and answers (Swordsworn).

### Non-goals

- **The other two launch classes' tables.** Their own stage-2
  documents.
- **Trap content and dungeon generation** (DEP-S1). This document states
  what the Trapwright needs from them, not how they are made.
- **Companion kits,** beyond what a Shade recruit's signature slice must
  not break.
- **The multiclass model** (M4); only the dip guards are honoured here.
- **Final names** (naming pass) and **final numbers** (simulator).

### Anti-goals

- **A steep precision curve.** The 5E curve works only because the 5E
  rogue has no Extra Attack; the Shade has Extra Attack, Nick and
  Exploit. No rank adds dice except the one allow-listed step at 5.
- **An unbounded re-stealth loop.** Every Hide the Shade buys in combat
  is pooled and strike-limited.
- **A fourth reading row:** no stored combo meter, no mark, no new pool.
- **Random or free ripostes:** every Counterstroke is a decision made
  with the roll known, pooled, on the one reaction.
- **Placed traps or ground zones** (ability 8.4, **agreed**).
- **A new condition:** the 32-condition budget stays untouched.
- **Automatic surprise of bosses or elites** (settled).

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Classes 8.4 | Chassis: Dexterity / Intelligence; Standard 7; Reflex; light ceiling, shield never; `weaponTraining {simple: all, martial: light \|\| finesse \|\| group:launcher}`; 3 of Stealth, Acrobatics, Mechanics, Arcana, Perception, Medicine; Opening; Tricks (Hide/Disengage/Dash, Duck, Catch Breath); Vanish; Cutthroat and Trapwright M2, Swordsworn unlock | 7.1 |
| Classes 6.4 | Leans: stealth, surprise and Steal P; own tempo P; single-target damage s; skills s; exploration s; mobility s; items (Trapwright lens); morale (Cutthroat lens); reactions (Swordsworn lens) | 7.1 |
| Classes 7.4 | The published ladder: talent at 2 and 7, subclass at 3, +1 ability at 4 and 8, offer at 5 and 9, subclass feature (changes a verb) at 6, capstone at 9, talent or offer at 10 | 5.5 |
| Classes 7.5, 7.6 | A named pool is one limited ability; the cap of eight; at most four things to read | 7.9, 10.6 |
| Classes 7.7 | Dip guards: `scalesBy: classLevel`, `minClassLevel`, `chassisOnly` | 12 |
| Classes 8.2 | One `restDefensiveTool` per class; 2 kit potions; Catch Breath half Recovery, 1 per rest, 2 from 6 | 7.3 |
| Ability 6.3, 6.4 | Rest and camp pools; 40–60% camp; one camp spend per turn | 7.5, 10.5 |
| Ability 6.6, 6.7 | The martial template; at most 3 named martial pools; capstone a camp pool of 1 at level 9 | 7.8 |
| Ability 7.2 | The class hook for a pooled bonus-action Hide | 7.4 |
| Ability 8.4 | No ground zones **(agreed)** | 8.2 |
| Weapons 6.4 | Any build ≤ 110% of the greatsword reference; a precision feature for Dexterity **(agreed)** | 6, 10.1 |
| Weapons 6.5 | The launcher Precision gate (pre-approved, off by default) | 10.2 |
| Weapons 7.1–7.5 | The canonical verbs; bounds; classes own depth, never access; the attack pipeline | 6.4, 7.3 |
| Conditions 7, 10, 11, 18.7 | Hard control only from limited abilities; forced movement and hazards; traps as sources; Hidden's end rules | 7.3, 8 |
| Enemies 11.1–11.3, 12.1–12.5 | The AI never reads class; the Hidden rule and awareness record; awareness states; noticing; silent kill; noise; joining; pursuit | 8.1, 9 |
| Enemies 14.8, 15.5 | `broken` and Cowed; the S-5 Steal row **(agreed)** | 7.6, 8.1 |
| Armour Q2 | Light armour is 12 + Dexterity with no band **(agreed)** | 5.5 |
| Trinkets 11 | The copy registry, bidirectional | 7.13 |
| Consumables 13.8 | DEP-1: a launch non-caster with Arcana; CN-11 scroll read uses the camp spend **(agreed)** | 7.1 |
| DH 8, 12.1, 12.4, 21.1 | Recovery; Medicine on solo floor 1; 2 kit potions; `Floor1BossSustain` | 7.10, 10.4 |
| Vanguard design 7.9, 10.1, 10.4 | VG-10 (turn allowances and one-option sub-limits count 0), VG-12 (the parity reading), VG-13 (the accepted level-1 martial camp miss) | 7.9, 10 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored. There is no d3 in the system (weapons 6.6's
d4 minimum stands), and none is used here.

### 3.3 Findings in the approved documents

Gaps and facts found while designing; each shaped a rule here or a
change in section 17. **(lead's call)** Ids D1–D14 are local to this
table.

| # | Finding | Where it is resolved |
|---|---|---|
| D1 | The stage-1 band (1d6, +1 die at 3, 5, 7, 9) models at 121–174% of the reference, because the Shade has Extra Attack, Nick and Exploit and 5E's rogue has none | 6.4; SH-1 |
| D2 | Stage-1 trigger 1 mixes "has not acted" (common; winning initiative) with "Surprised" (rare), so one tier cannot price both | 6.5; SH-2 |
| D3 | A trigger named "Opening" on the Opening verb reads badly | Renamed Beset (6.5; SH-2) |
| D4 | Hidden gives advantage, which fires Exploit and the stealth die; the Hide rate, not the dice, is the parity variable | 7.9 strike limit; SH-4 |
| D5 | VG-10 covers only a sub-limit on one option; a sub-limit shared by several options of one pool is not covered | SH-4 |
| D6 | Duck and Opening have no place in the attack pipeline or the damage steps | SH-3, SH-6 |
| D7 | On a miss, weapons 7.5 stops at step 6, so a reaction hooked at step 15 (Retort Strike) is never reached; a riposte needs a new hook | 8.3; SH-8 |
| D8 | The Shade has no Dexterity Parry: the longsword is untrained and the staff rolls Strength | 8.3 |
| D9 | No approved document designs traps; trap records, density and damage reading are all undesigned | 8.2; SH-15, SH-16 |
| D10 | Under the old pools the Shade fails `Floor1ClassSpread` in the escort split, under the correct comparator (the Vanguard mean across its picks): the old Tricks-2 package by a 19.3-point gap, and Tricks 3 alone (with the 1d3 band) by 12.6, against the 10-point limit (10.4) | 7.8; SH-24 |
| D11 | Only a leader's silent kill sets `broken` (enemies 12.2, 14.8), which makes the Cutthroat's stage-1 clause nearly empty | 8.1; SH-10 |
| D12 | A Tricks-free major condition from a turn allowance (an early Make an Example) is illegal under conditions 7.1 | 8.1 |
| D13 | Light armour has no armour band at 5 (armour Q2, **agreed**); the research streams' "armour band at 5" was an error, and the light-armour spread lever does not exist | 5.5, 10.4 |
| D14 | The Shade's level 1–4 camp share (32–37%) misses the band; VG-13 accepts only the level-1 martial miss | 10.5; SH-28 |

### 3.4 Terms

**(lead's call)** Used with these meanings everywhere below, beside the
stage-1 terms (classes 3.4) and the Vanguard design's terms (rank, Art,
turn allowance, window fill), which stand **(settled)**.

- **Tier:** each Opening trigger is `stealth` or `open` (data,
  `TriggerTierFixed`). A hit meeting any stealth trigger deals the
  stealth amount; otherwise the open amount.
- **Stealth die / open amount:** Opening's two values (6.4).
- **Signature Tricks option:** the one Tricks option a subclass adds (No
  Witnesses, Jury-Rig, Sworn Guard), ranked at 3, 5 and 7.
- **Art:** the one Vanish option a subclass adds from class level 5
  (Ambuscade, Tripline, Blade Dance), rank II at 9.
- **Strike option:** a `pool.tricks` option record tagged `strike`: one
  that makes you Hidden, makes a weapon attack, or sets a carry, except
  where a line states otherwise. The stated exceptions are listed in 7.3
  rule 3 (Jury-Rig is `device`; the No Witnesses II carry-only mode is
  untagged).
- **Strike limit:** the per-rest counter declared on `pool.tricks`'s
  record and spent by every record tagged `strike`, whether or not that
  record also spends a Trick (7.9; SH-4).
- **The Vanish carry:** an entry in the source-held `carries[]` state
  (7.5, 13.1); prose, not a feature name.
- **Turned blow:** a melee attack roll that misses after a Parry or a
  `countsAsParry` ward (a window candidate, a step-0 fill or a carried
  ward) was applied to it.
- **Glint:** the on-target display that a trigger is live (one shape per
  tier); Opening, Answer and Exploit are glints and count 0 to read.
- **Unaware (Cutthroat):** Dormant, Stationary or Patrolling (enemies
  12.1): the S-5 Steal set; never Alerted, Searching or Wary (the set is closed; enemies 15.5 S-5).

## 4. Framework decisions

The client chose Q1–Q3 on 2026-11-18, each time the lead's
recommendation. **(agreed)** The rest are the lead's calls, which the
client said stand.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Progression model | **Ranks**, with grafts from the Triggers and Setup models (6.1–6.3) **(lead's call)** | Triggers (its saturation claim fails: 113–114% at level 10). Setup, "Seam and Coup" (112–114% at levels 9–10 solo; replaces the approved Answer trigger) |
| 2 | Opening band (Q1) | **The tiered band:** stealth tier (Hidden, Surprised, the Vanish carry) 1d6 at levels 1–4, 2d6 at 5–10; open tier (Unready, Beset, Answer) +2 flat at 1–4, 1d6 at 5–10; no further dice steps (6.4; SH-1) **(agreed)** | The flat 1d3 at levels 1–4 (passes, but every trigger pays the same and the ambush stops feeling special). The stage-1 curve (121–174%) |
| 3 | Opening triggers (Q1) | **Trigger 1 splits:** Unready (open) and Surprised (stealth); **trigger 3 renamed Beset**; **the Vanish carry counts as Hidden for Opening** (6.5; SH-2) **(agreed)** | "Has not acted" in the stealth tier (stealth share about 440‰; fails). The carry as a "first strike" (contradicts the split) |
| 4 | Re-stealth bound | **One shared per-rest strike limit** on `strike`-tagged Tricks options: 1 at levels 1–3; at level 4, 1 plus 1 pip only the subclass signature strike may spend; 2 from level 5 *(illustr.)*; shown as pips on the Tricks row (7.9; SH-4) **(lead's call)** | No limit (111–118%). A separate Hide pool (a fourth row and a fourth limited ability). Unpooled Fade (5E shape; Hidden is worth more here) |
| 5 | Pools | **Tricks 2 / 3 / 4 at levels 1 / 3 / 8; Vanish 2 / 3 at levels 1 / 2; Catch Breath 1 / 2 from 6 (settled); From Nowhere camp 1 at 9** (7.8; SH-24) **(lead's call; the sizes as written, the Vanish and Tricks-4 steps moved one level earlier in the check round, 10.1, 10.4)** | The old package (Tricks 2, Vanish 2: fails `Floor1ClassSpread` by a 19.3-point gap escorted; Tricks 3 alone, by 12.6). "No pool step before 5" (a Vanguard-specific call, not a template rule) |
| 6 | Parity reading | **VG-12** as approved: rest-pool damage at the worst legal build ≤ 110%; camp-pool damage bounded by the nova bound and `CampOptionBound` (10.1) **(settled; the client was told plainly the Shade passes only under it)** | The all-in reading (camp damage included: 111–118%) |
| 7 | Trapwright staging (Q2) | **M2 only if trap records (DEP-S1) land in M2** with dungeon generation, with the content reading of trap damage (`SpringContentBound`); otherwise the Trapwright moves to the unlocks and the Swordsworn is promoted to M2. **The unlock fallback is the expected default** (the client: "keep it for later") (8.2; 14) **(agreed)** | The Trapwright in M2 unconditionally (no approved document designs traps). The strict parity reading (117–120% at any trap density) |
| 8 | Swordsworn mechanism (Q3) | **Sworn Guard:** a Tricks-paid +2 AC ward against one melee attack roll that counts as a Parry; Counterstroke after a turned blow from level 3 and (Open Guard) after any melee miss from level 5; every Counterstroke spends the strike limit (8.3) **(agreed)** | Strictly after a weapon Parry (fires on about 10% of melee rolls; the Shade has no Dexterity Parry). Borrowing a staff's Parry through VG-19 (a bulky staff in every kit; an unpaid +2 every round) |
| 9 | Throat-Cut | **Out of combat only,** silent-only, a 2× silent-kill threshold, `strike`-tagged (8.1) **(lead's call)** | The in-combat auto-crit on Surprised (115–121%; nova 1.9) |
| 10 | Capstone | **From Nowhere:** every Dexterity-rolled weapon hit this turn takes the stealth die (2d6), triggers waived; an automatic subclass rider (7.7) **(lead's call)** | `lingering` (a second Opening on another unit's turn; 1.86 with the Swordsworn). A free Jury-Rig rider (p95 about 90 against the bound of 74). Riders that only restate From Nowhere or only waive a Trick (the first Caught Out and Swordsworn texts; check round) |
| 11 | Tainted Blade | **Trades the stealth-tier dice only** for Poisoned; once per fight per elite or boss; Shrugged Off (7.6) **(lead's call)** | Trading any Opening (makes the verb a poison applicator; a boss lock) |
| 12 | Tripline | **Fires on use** against adjacent hostiles (8.2) **(lead's call)** | An aura that waits for enemies (a new aura trigger kind). Snuff (Blinded on several hostiles, a major condition; the flash flask was cut for the same reason) |
| 13 | Lone Blade | **A swap** for Beset's ally-hit clause, from level 5 (8.3) **(lead's call)** | An addition (+6 / +3 / +3 points). From level 3 (113% solo even as a swap) |
| 14 | Starting kit | **Blades** (shortsword + dagger / sabre + dagger) default; **Launcher** (longbow / sabre + dagger) as the creation pick; leather; 2 potions (7.10) **(lead's call)** | Further presets (a second level-1 pick) |
| 15 | Reading | **Three rows:** at-wills, Tricks, the Vanish row; Opening, Answer and Exploit are glints (7.1) **(lead's call)** | A trigger list shown as rules text. A separate strike-limit row |

### 4.1 How other games handle rogues and skirmishers

Mechanism references only. No names or text are taken. Web-checked in
the research streams: the 2024 Cunning Strike and the 2024 Assassin and
Thief; the PF2e rogue, Swashbuckler, Dueling Parry and Riposte, and
Snarecrafter; the Draw Steel Shadow; the 13th Age 2E Bravado change; the
Gloomhaven Scoundrel; XCOM 2 Reaper, Darkest Dungeon and the Frosthaven
Trapper (each in part); the Battle Master Riposte and Rakish Audacity;
Pillars of Eternity Riposte. Everything else is recall and flagged as
unsure. No Gloomhaven "Nightshroud" or "Saboteur" class was found; those
names are treated as misremembered. Every SRD-shaped mechanism is
checked against SRD 5.2.1 before any record is written **(settled,
classes stage 1)**.

| Game / feature | What it does | We take | We avoid |
|---|---|---|---|
| **5E Sneak Attack** | +1d6 per 2 levels, once per turn; **no Extra Attack** | Once per round; declinable; a separate part doubled on a crit | The curve: it overshoots here by 30–80 points |
| **5E Cunning Action** | Unlimited bonus-action Dash, Disengage, Hide | **Fade**, same shape | Unlimited use: Hidden is worth advantage + Exploit + the stealth die here, so Fade is pooled and strike-limited |
| **5E Uncanny Dodge / Evasion** | A reaction halves a hit; area save outcome | **Duck**; Tumble Clear (talent); Duck III | — |
| **2024 Cunning Strike** (checked) | Spend Sneak Attack dice on Poison, Trip, Withdraw | **Dice trades** (Tainted Blade): parity-neutral by construction | Withdraw's free movement (reads as a speed grant) |
| **2024 Assassin vs 2014** (checked) | 2014: auto-crit on Surprised; 2024: a small round-1 bonus | The Surprised die; Throat-Cut as an out-of-combat threshold only | In-combat auto-crits (a crit doubles weapon, Exploit and Opening dice: 115–121%) |
| **2024 Thief, Fast Hands** (checked) | Bonus-action item use | **Toss** (setup flasks only, pooled) | Bonus-action bombs (+7–8 parity points) |
| **PF2e rogue rackets** (checked) | A subclass changes *how* off-guard is created | Subclass Opening triggers (Caught, Lone Blade) and setter options | Narrow follow-up feats (the Twist the Knife "trap" critique) |
| **PF2e Swashbuckler / Dueling Riposte** (checked) | Panache; Finishers; riposte on a critical failure | The "turned blow" moment (Sworn Guard I) | A stored meter; a riposte-only extra reaction (a forbidden grant) |
| **5E Battle Master Riposte** (checked) | Pooled reaction attack on any melee miss | **Open Guard** (level 5) | An added superiority die |
| **5E Swashbuckler, Rakish Audacity** (checked) | Sneak Attack when alone with the foe | **Lone Blade** (as a swap, from level 5) | Stacking it on the ally trigger (+6 points) |
| **Pillars of Eternity Riposte** (checked) | Passive chance on a miss | — | Random, uncontrolled ripostes: every Counterstroke is a decision made with the roll known |
| **13th Age Momentum → 2E Bravado** (checked) | A state lost when hit was replaced | Answer is never lost by being hit; Duck sets it | Losing state when hit |
| **4e strikers** (recall) | Shallow rider growth over 30 levels | One dice step only | — |
| **Gloomhaven Scoundrel** (checked) | Bonus against flanked or isolated targets | The `aloneCounts` trigger (level 5+) | Ally-adjacency arithmetic |
| **Draw Steel Shadow** (checked) | Hide while observed; illusions | Vanish; **False Trail** | Teleport (a forbidden grant) |
| **XCOM 2 Reaper, Invisible Inc., Shadow Tactics** (Reaper checked; others recall) | Silent kills; concealment that escalates; noise lures | No Witnesses; Quiet Work; the strike limit; **Lure**; **Wise to You** | Per-action random detection (noise is deterministic here) |
| **Frosthaven Trapper; XCOM Claymore; Hitman accidents** (Trapper checked) | Move foes into traps; remote triggering; the level's own machinery | Spring, Drag Line, Killing Ground | Placing traps (ability 8.4 forbids ground zones) |
| **PF2e Snarecrafter** (checked) | Snares built in squares | That preparation pays | Every snare is a square effect, so it is rejected |
| **Darkest Dungeon, Battle Brothers** (DD checked) | Riposte as a standing status | — | Standing free-riposte states (Riposting is Later; `FlagNotCondition` rule 3) |
| **Blades in the Dark** (recall) | Flashbacks | — | Breaks determinism and "no free retries" |
| **Mark of the Ninja** (recall) | Visible noise rings | The hearing outline (enemies 12.4) and the Lure radius | — |

**Combined lesson.** Rogue designs that hold together do one of two
things: **(a)** precision is the class's only damage source, so its
curve may be steep (5E, 4e); or **(b)** precision stays shallow and the
class grows through what it does with openings: trades, rackets,
ripostes, mobility. This system is (b). Three further rules follow:
re-stealth loops need a hard limit; ripostes must be pooled, ride the
one reaction and fire on a miss; and trap classes need content density,
so the Trapwright is a dependency as well as a subclass.

## 5. The client's brief: the progression shape

### 5.1 The answer

**(lead's call)** The Shade grows by **depth on three fixed rows**
(at-wills, Tricks, the Vanish row), never by new rows. Opening and
Answer are glints and count 0. Each subclass is a role the player specs
into, and each gives **two further signature moves**, following the
Vanguard template: a **signature Tricks option** with ranks at levels 3,
5 and 7, and a **Vanish Art** at level 5 with rank II at level 9. The
level-6 feature rewrites a verb. The capstone, From Nowhere, sits on the
Vanish row and carries an automatic subclass rider.

`rankSchedule {core: [1, 5, 8], other: [1, 8], signature: [3, 5, 7],
art: [5, 9]}` **(lead's call)**: the Vanguard's schedule plus a core
rank III at level 8. The level-8 ranks add no damage; they fill a level
that otherwise gains only +1 Dexterity and sits in a parity trough
(10.1).

### 5.2 The two tiers

- **The stealth tier (the big die)** grows through Vanish, the strike
  limit's steps at 4 and 5, the Cutthroat, and the capstone. Each step
  widens *when* the Shade is Hidden or carries Hidden, never the band
  beyond 2d6; the Cutthroat's Surprised part (+1 / +1d4) is the one
  allow-listed extra part (`RankAddsNoDice`, 8.1).
- **The open tier (the small amount)** grows through added triggers
  (from level 5), the subclass rackets (Caught, Lone Blade) and Draw
  Out. Each step widens *which* targets count, never the amount beyond
  1d6.
- **Both tiers share one verb and one allowance:** Opening, once per
  round, the highest tier met **(settled shape, classes 8.4)**.

### 5.3 How signature moves progress deeper

| Level | Every Shade | Each subclass |
|---|---|---|
| 1 | Opening I (stealth 1d6 / open +2); Fade I, Duck I, Catch Breath; Vanish I | — |
| 2 | Vanish 3 | — |
| 3 | Tricks 3 | **Signature Tricks option, rank I** (No Witnesses, Jury-Rig, Sworn Guard) and the subclass clauses |
| 4 | — | A strike-limit pip only the signature strike may spend (No Witnesses, Counterstroke) |
| 5 | Extra Attack; **Opening II** (2d6 / 1d6); strike limit 2; Fade II, Duck II; added triggers legal | **Signature rank II**; **Vanish Art I**; the Swordsworn's Lone Blade |
| 6 | Catch Breath 2 (settled) | **Verb rewrite** (1 of 2) |
| 7 | Talent with a trigger option | **Signature rank III** |
| 8 | Fade III, Duck III, Vanish II; other known options rank II; Tricks 4 | — |
| 9 | **From Nowhere** with its automatic subclass rider | **Art rank II** |
| 10 | Talent or offer | — |

### 5.4 What each subclass lets the player spec into

| Subclass | Specs into | Signature Tricks option | Vanish Art | Level 6 (1 of 2) | From Nowhere rider |
|---|---|---|---|---|---|
| **Cutthroat** (M2) | The quiet kill between fights; the ambush opener; kill-then-fade; perfect stealth or terror | No Witnesses | Ambuscade | Throat-Cut / Make an Example | The first kill takes No Witnesses free |
| **Trapwright** (M2 if DEP-S1 lands in M2; otherwise an unlock, the expected default; resolved 2026-11-21: the fallback applies, the first unlock in M3, TL-37) | Known traps as weapons; setup-flask tempo; Caught; forced movement onto traps; doors | Jury-Rig (Spring, Re-arm, Toss and Dust; Wedge; Killing Ground) | Tripline | Drag Line / Trick Shot | Caught Out: a free Killing Ground, and one stealth-die hit on a Caught hostile next turn |
| **Swordsworn** (M2: promoted under the fallback, TL-37; cross-document comparison 2026-11-23, CC-73) | One-on-one melee; the reaction economy; Answer as the engine; finesse and light blades | Sworn Guard (with Counterstroke, Open Guard, Held Guard) | Blade Dance | Cross Steel / Turn Aside | One Counterstroke before your next turn costs no Trick and takes the stealth die |

### 5.5 Level table *(illustr.)*

**(lead's call)** Default array; Dexterity +3 → +4 (level 4) → +5
(level 8); Con +1; leather armour.

| Lvl | Floor | Automatic | Choice | HP / Rec / Catch Breath | AC | Prof / attack / difficulty | Tricks / strike limit / Vanish |
|---|---|---|---|---|---|---|---|
| 1 | 1 | Kit; at-wills; **Opening I** (stealth 1d6 / open +2); Fade I, Duck I, Catch Breath ×1; Vanish I | Creation pick: Blades or Launcher kit | 19 / 7 / 3 | 15 | +2 / +5 / 13 | 2 / 1 / 2 |
| 2 | 1 | **Vanish 3** | Talent (1 of 3) | 27 / 9 / 4 | 15 | +2 / +5 / 13 | 2 / 1 / 3 |
| 3 | 1→2 | **Tricks 3** | Subclass: signature I and clauses | 35 / 11 / 5 | 15 | +2 / +5 / 13 | 3 / 1 / 3 |
| 4 | 2 | Strike-limit signature pip | +1 Dex, alone **(settled)** | 43 / 13 / 6 | 16 | +2 / +6 / 14 | 3 / 1 (+1 signature) / 3 |
| 5 | 2→3 | Extra Attack; **Opening II** (2d6 / 1d6); strike limit 2; Fade II, Duck II; signature II; **Vanish Art I** | Offer (1 of 3) | 51 / 15 / 7 | 16 | +3 / +7 / 15 | 3 / 2 / 3 |
| 6 | 3 | Catch Breath ×2 **(settled)** | Subclass feature (1 of 2), changing a verb | 59 / 17 / 8 | 16 | +3 / +7 / 15 | 3 / 2 / 3 |
| 7 | 3→4 | Signature III | Talent (with a trigger option, **settled**) | 67 / 19 / 9 | 16 | +3 / +7 / 15 | 3 / 2 / 3 |
| 8 | 4 | **Fade III, Duck III, Vanish II; Tricks 4**; other known options rank II | +1 Dex | 75 / 21 / 10 | 17 | +3 / +8 / 16 | 4 / 2 / 3 |
| 9 | 4→5 | **From Nowhere** (camp 1) and its subclass rider; Art II | Offer (1 of 3) | 83 / 23 / 11 | 17 | +4 / +9 / 17 | 4 / 2 / 3 |
| 10 | 5 | — | Talent or offer | 91 / 25 / 12 | 17 | +4 / +9 / 17 | 4 (+1 Spare Trick) / 2 / 3 |

- HP = 11 + L × (7 + Con). Recovery = 4 + 2L + Con. Catch Breath heals
  FloorDiv(Recovery, 2).
- Light armour is 12 + Dexterity with no band (armour Q2, **agreed**),
  so AC rises only with Dexterity.
- **The early pool steps are deliberate (lead's call).** Unlike the
  Vanguard (favoured on floor 1, so no step before 5), the Shade needs
  Tricks 3 and Vanish 3 by level 3 to pass `Floor1ClassSpread` (10.4).
  Classes 15 anticipates that "stage 2 raises Tricks". Vanish 3 arrives
  at level 2, not 3 (check round): Vanish is camp damage, outside parity
  under VG-12, and the step is aimed at the level-2 floor-1 boss arrival
  (10.4), the worst moment in the class.
- **Level 4 stands alone (settled).** It still gives +1 to attack and
  AC; the ladder settles it as the only choice. The signature pip is an
  automatic step, not a choice **(lead's call)**: at levels 3–4 a
  subclass signature strike otherwise fires about once every 2–3 fights
  and competes with the only Fade-Hide (7.9).
- **Tricks 4 arrives at level 8, not 9 (lead's call, check round).**
  Level 8 otherwise gains only +1 Dexterity and defensive rank IIIs and
  reads at 89% typical; a fourth Trick adds Duck and Draw Out uses, not
  strikes, because the strike limit is unchanged. Level 9 keeps From
  Nowhere and Art II. Rerun owed (10.1).

**What the player feels at each step:** level 1, the big stealth glint;
level 2, a talent and a third Vanish; level 3, a third Trick and the
subclass; level 4, +1 to hit and AC and a second, signature-only strike
pip; level 5, the 2d6 stealth die, Extra Attack and the Art; level 6, a
verb rewrite; level 7, signature III; level 8, the rank IIIs and a fourth
Trick; level 9, "every strike is an opener".

## 6. Opening and the progression model

### 6.1 The three progression models

| Model | Core idea | Strength | Weakness |
|---|---|---|---|
| **Ranks** | Fixed rows gain ranks; one dice step at 5; subclasses add signature options and Arts | Pips on existing buttons, costing 0 to read; "ranks add no dice" answers parity; closest to the Vanguard template | Subtle growth; level 8 needs a beat |
| **Triggers** | Opening's trigger list grows through rackets and offers; the tier split and a saturation reading cap the value | Most "new way to open" moments | Saturation needs a stealth share ≤ 320‰; the derived share is about 440‰ (113–114% at level 10); picks past saturation add nothing in a party |
| **Setup / cash-in ("Seam and Coup")** | A held per-target state built by probes and misses, cashed by a Coup finisher; replaces Answer | The best felt decision | +1–3 points everywhere and still 112–114% at levels 9–10 solo; replaces the approved Answer trigger; its own designer leaned against it |

### 6.2 Verdict and judges' scores

Mean of three judges, 1–10; build cost is scored as "low cost", so
higher is cheaper. The total is the sum of the six means.

| Model | Depth and growth | Readability | Decision quality | Balance safety | Fit with docs | Build cost (low) | Total | First place |
|---|---|---|---|---|---|---|---|---|
| **Ranks** | 7.0 | **9.0** | 7.0 | **7.3** | **9.0** | **8.0** | **47.3** | **3 of 3** |
| Triggers | **7.7** | 6.0 | 6.0 | 5.0 | 7.0 | 7.0 | 38.7 | 0 of 3 (donor of guards) |
| Setup | **7.7** | 5.3 | **7.3** | 4.0 | 5.0 | 4.3 | 33.6 | 0 of 3 (rejected) |

**Verdict (lead's call): ranks**, the winner on every judge's card. It is
the closest fit to the Vanguard template (reading 3; 3 of 8 limited
abilities; `RankAddsNoDice` with one allow-listed step), and the only
model that passes every cell with every subclass once the band is fixed
(10.1).

### 6.3 The spine and its grafts

**(lead's call)**

- **The rank schedule:** 5.1.
- **From the Triggers model:**
  - trigger tier as data (`TriggerTierFixed`);
  - `TriggerFromFive` (added Opening triggers arrive at level 5);
  - the `preferStealthTarget` policy (7.12);
  - the offer scorer's "already live" display (7.11);
  - the Bad Footing, Blind Side and Old Wound triggers (6.5);
  - Draw Out (7.3; 9).
- **From the Setup model:**
  - the rename of trigger 3 to **Beset**;
  - the principle that declining dice buys an effect (Tainted Blade).
- **From the numbers check:**
  - the corrected stealth share (10.1);
  - the early pool steps (5.5, 7.8);
  - barring `launcherOpening`;
  - rejecting in-combat Throat-Cut.
- **Cut, with one-line reasons:** the Seam flag and Coup finishers
  (parity; replaces Answer); trigger saturation as the only bound (the
  share is too high); `lingering` (spends the nova margin).

### 6.4 Opening (signature verb; the weapons 6.4 precision feature, **agreed**)

> **Opening.** Once per round (a `turnAllowance`, classes 3.4), when
> your Dexterity-rolled weapon attack hits a target that meets a trigger
> you know, you may add Opening's damage: the **stealth** amount if any
> stealth-tier trigger is met, otherwise the **open** amount. Dice are
> of the weapon's damage type, a separate damage part, doubled on a
> critical hit; the open +2 at rank I is a flat term and is not doubled.

**Band (SH-1; agreed, Q1; values *illustr.*):**

| Rank | Stealth tier (Hidden, Surprised, the Vanish carry) | Open tier (Unready, Beset, Answer) |
|---|---|---|
| I, levels 1–4 | **1d6** | **+2** (flat; not doubled on a crit) |
| II, levels 5–10 | **2d6** | **1d6** |

- `scalesBy: classLevel`. The level-5 step is the one entry on
  `RankAddsNoDice`'s allow-list for the Shade (with the Cutthroat's
  Surprised part, 8.1). **No further dice steps** **(agreed)**.
- **Applicability (the 6.5 scope column is authoritative):**
  - each trigger record lists the attack scopes it carries (`melee`,
    `thrown`, `launcher`, `trapOrFlask`); an attack qualifies on a
    trigger only if the trigger's scope lists the attack's scope;
  - launchers qualify on Unready, Surprised, Hidden and the Vanish carry
    (the approved "first strike and Hidden", restated for the split),
    plus Caught (8.2), and on Answer against an attacker you Ducked
    under `duckFromRanged` (7.11);
  - thrown attacks qualify on every trigger whose scope lists `thrown`:
    Unready, Surprised, Hidden, the Vanish carry, Answer and Caught;
  - flasks and traps never qualify, except through Trick Shot (8.2,
    scope `trapOrFlask`);
  - granted attacks (`grantedBy`) never qualify **(settled)**.
- **Policy:** `applyOnFirstQualifyingHitOnOwnTurn` **(settled)**, with
  two player-set filters: `preferStealthTarget` (hold the allowance for a
  stealth-tier target this turn) and **`holdCarryOnOverkill`** (on by
  default; while a carry is live, do not apply Opening to a minion or to
  a hit whose weapon damage already drops the target, so the carry is
  not spent on an overkill) **(lead's call; SH-7)**. On reaction attacks
  the default is `never`; only Counterstroke defaults to `always`, and a
  reaction hit takes the open tier only unless a line says otherwise
  (7.5).
- **Exploit** is unchanged and independent (once per turn of any unit,
  granted attacks included). It may fire on the same hit **(settled,
  classes 8.4)**. Every parity figure includes both.
- **Pipeline placement (SH-3):** in weapons 7.5 step 8, Opening's dice
  are a separate part added after Exploit dice and before crit doubling,
  so 6.6's doubling covers them; the rank-I open +2 is a flat term added
  with the step-8 flat terms, after doubling, and is not doubled. Neither
  is the weapons 6.5 launcher term **Precision**, and the effectKinds
  are named apart (`damage.extraDice.opening`, `damage.flat.opening`).
- **Display:** one glint shape per tier. A big glint means the stealth
  die, a small glint the open amount. The tooltip names the trigger;
  triggers are never shown as rules text.
- **Validators:**
  - **`TurnAllowanceNoDamage`** allow-list += `damage.extraDice.opening`
    and `damage.flat.opening` (SH-3). The weapons Precision term is not
    on it.
  - **`OpeningCeiling`** (new, static, like `HewCeiling`), checked at
    levels 1, 4, 5 and 10. Every input is a named per-level table in the
    tuning file, so the check reads data only: `ref[L]`,
    `weaponsOnlyBest[L]`, `hiddenUplift[L]` (the advantage and Exploit
    value of the Hides the strike limit allows), `hitProbAtLeastOne[L]`
    (permille), `critFactor[L]` (permille, ≥ 1000), and the shares
    `stealthShare[L]` and `openShare[L]` (permille). With
    `stealthPart[L]` and `openPart[L]` the band's average values in
    tenths of a point (dice parts multiplied by `critFactor`, the flat
    part not), the integer test is:

    `FloorDiv((stealthShare × stealthPart + openShare × openPart) ×
    hitProbAtLeastOne, 1000000) ≤ FloorDiv(1100 × ref, 1000) −
    weaponsOnlyBest − hiddenUplift` (all in tenths of a point per round)

    - Default shares *(illustr.)*: stealth 300‰ and open 650‰ at levels
      1–4; at levels 5 and 10 the open share is 1000‰ − stealth (the
      saturation reading).
    - The measured worst-legal stealth shares are 177 / 143 / 243 /
      244‰ at levels 1 / 3 / 5 / 10 (scratch). The level-3 dip is not
      explained by the scratch model; the likely cause is the
      denominator (Tricks 3 buys more Duck-set Answers, so more open
      applications), which the rerun must confirm with every stealth
      source in 15 counted, subclass strikes included, and per subclass
      (13.3).
    - The tables are set once from the simulator and then frozen as
      data; the check guards later data edits and is not circular,
      because it never reads the simulator at build time. It does not
      prove parity on its own. **The observed stealth share is a release
      input (10.1).**
  - **`StrikeOptionCeiling`** (new, static), checked at levels 1, 5 and
    10 against two yardsticks, each an explicit expression over named
    tuning tables *(illustr.)*, compared on the **total** expected damage
    one use adds **(lead's call)**:
    - **Hide-shaped** strikes (Fade-Hide, No Witnesses, Cross Steel, any
      option that grants Hide or a carry): ≤ the **Fade-Hide yardstick**
      `FloorDiv(hideSuccess × (advantageUplift + exploitValue +
      stealthPart − openPart), 1000)`, with `hideSuccess` 700‰ (the
      worst-legal profile). Cross Steel passes because it has no
      advantage term.
    - **Attack-shaped** strikes (Counterstroke): ≤ one main-hand weapon
      attack with the open amount and no Exploit (`ignoresHelped`), at
      the reference target: the option never adds more than one attack.
      A rider that lifts one Counterstroke to the stealth die (the
      Swordsworn From Nowhere rider, 8.3) is camp-pool damage and is
      bounded by the nova bound instead.
    - Throat-Cut is out of combat and is bounded by the fights-avoided
      threshold (8.1), not by this check.

### 6.5 Catalogue: Opening triggers

Every trigger reads state and applies nothing. Ids *(illustr.)*. The
scope column is authoritative (6.4); `trapOrFlask` is read only through
Trick Shot (8.2).

| Id (name) | Tier | Level / source | Wave | Exact text | Scope |
|---|---|---|---|---|---|
| `unready` (**Unready**) | open | 1, base | M2 | "The target has not started a turn this fight." (`hasStartedTurnThisFight`, 13.1; an opportunity attack or other reaction is not a turn start) | melee, thrown, launcher, trapOrFlask |
| `surprised` | stealth | 1, base | M2 | "The target is Surprised." | melee, thrown, launcher, trapOrFlask |
| `hidden` | stealth | 1, base | M2 | "You are Hidden from the target." | melee, thrown, launcher, trapOrFlask |
| `vanishCarry` | stealth | 1, base | M2 | "You hold a live carry whose target filter the target meets; this own-turn weapon hit counts as Hidden for Opening" (consumption rules in 7.5) | melee, thrown, launcher |
| `beset` (**Beset**; stage-1 trigger 3, renamed) | open | 1, base | M2 | "Your attack is melee, and the target is Prone, Staggered or Grappled, or another unit's melee weapon hit landed on it since the end of your previous turn (`meleeWeaponHitBy[]`, 13.1), or it is drawn out by you this turn (Draw Out)." | melee |
| `answer` (**Answer**) | open | 1, base | M2 | "The target missed you with an attack roll, or you Ducked one of its hits, since the end of your previous turn." Held as `answeredBy[]` on the source; cleared at your turn end, at combat end and when you are Downed; never cleared by being hit | melee, thrown, trapOrFlask (launcher against a unit you Ducked, under `duckFromRanged`) |
| Cutthroat Surprised die | stealth (extra part) | 3, subclass | M2 | +1 at levels 3–4, +1d4 from 5, against a Surprised target (8.1) | as Opening |
| `caught` (**Caught**) | open | 3, Trapwright (the one `TriggerFromFive` exception) | with the Trapwright | 8.2 | melee, thrown, **launcher** |
| `loneBlade` (**Lone Blade**) | open | 5, Swordsworn; *replaces* Beset's ally-hit clause | with the Swordsworn | 8.3 | melee |
| `restrainedCounts` | open | 5+, `triggerMenu` | M2 | "Restrained targets count." | melee |
| `blindedCounts` | open | 5+ | M2 | "Blinded targets count." | melee |
| `teeteringCounts` | open | 5+ | M2 | "Teetering targets count." | melee |
| `poisonedCounts` | open | 5+ | M2 | "Poisoned targets count." | melee |
| `bleedingCounts` (**Old Wound**) | open | 5+ | M2 | "Bleeding targets count." | melee |
| `movedCounts` (**Bad Footing**) | open | 5+ | M2 | "A target with the verb-moved-this-round mark (weapons 7.2 rule 4) counts." | melee |
| `challengedCounts` (**Blind Side**) | open | 5+ | M2 | "A target Challenged by another unit counts." | melee |
| `aloneCounts` | open | 5+ | M2 | "A target with no other hostile within 2 squares of it counts." | melee |
| `dreadCounts` | open | 5+, `requires: subclass:cutthroat` | M2 | "A Frightened target counts." | melee |
| `launcherOpening` | — | — | — | **Barred** (114% raw at level 10) | — |
| `gapSight` (**Gap Sight**) | open | kit only: the Scout companion kit; never a hero trigger (CMP-33) | M2 | "Once per round, your launcher or thrown hit is against an Unready or Staggered target." (companions design 8.3) | thrown, launcher |
| `knifeWork` (**Knife Work**) | open | kit only: the Outrider companion kit's signature; never a hero trigger (CMP-33) | M2 | "Your action's two melee attacks against one target each count as against a Beset target, and Flank Sense's once-per-round limit is lifted for that action." (companions design 8.3) | melee |

The two kit-only rows (CMP-33 of the companions design) are not hero
triggers: they are never offered, never on `triggerMenu` and never
counted by `TriggerFromFive` or `OpeningTriggersAdded` (6.6); they were
checked against Lone Blade and Caught.

### 6.6 Trigger rules

**(lead's call)**

1. `trigger.tier` is data: `stealth` or `open`. Only `surprised`,
   `hidden`, `vanishCarry`, the Cutthroat Surprised die, From Nowhere and
   the two From Nowhere riders that name it (Caught Out, 8.2; the
   Swordsworn rider, 8.3) may deal the stealth die (`TriggerTierFixed`,
   an explicit allow-list). Each allow-listed rider is counted in the
   nova report and the stealth-share report (13.3).
2. Every added Opening trigger record has `minClassLevel ≥ 5`
   (`TriggerFromFive`). Caught is the one stated exception: at levels
   3–4 it pays only the open +2.
3. A hit that meets several triggers takes the highest tier. Triggers
   never add to each other.
4. `OpeningTriggersAdded ≤ 3`. This counts the subclass trigger, the
   level-7 talent and offers. Past saturation, extra picks are
   display-only value, so the generator stops drawing.

## 7. The core kit

### 7.1 Chassis

**(settled from classes 8.4; the last rows are lead's calls)**

| Field | Value |
|---|---|
| Main / suggested second | Dexterity / Intelligence (initiative feeds Unready; Mechanics) |
| HP tier, save | Standard 7; Reflex |
| Armour / shield | Light ceiling (12 + Dex); shield never |
| Weapons | `weaponTraining {simple: all, martial: light \|\| finesse \|\| group:launcher}`: dagger, sickle, handaxe, light crossbow, sling, staff and the other simple families; shortsword, sabre, rapier, whip, hand crossbow, shortbow, longbow, heavy crossbow. **No longsword**; the staff rolls Strength. So there is no Dexterity Parry (8.3) |
| Skills | 3 of Stealth, Acrobatics, Mechanics, Arcana, Perception, Medicine |
| Complexity | Medium |
| Things to read | **3 at every level:** at-wills; Tricks; the Vanish row (Arts, From Nowhere). Opening, Answer and Exploit are glints (0). Room for 1 offer row |
| Limited abilities | Tricks (rest), Vanish (camp), From Nowhere (camp 1) = **3 of 8**; named martial pools 2 of 3 plus the capstone |
| Not counted | The Catch Breath sub-limit (settled); the **strike limit** (SH-4); Opening's turn allowance; `answeredBy[]`, `carries[]`, `caughtBy[]` and the other 13.1 state (source-held) |
| `fallbackDamageTypes` | piercing (shortsword, dagger, rapier, bows); slashing (sabre, sickle, handaxe) |
| `level1MechanicIds` | opening, answerTrigger, tricks, fade, duck, catchBreathHalf, vanish, vanishCarry |
| `signatureVerb` | opening |
| `signatureEvents` | Opening dealt or traded; a Vanish or Art used; a Duck that set Answer; a subclass signature applied; a From Nowhere hit (SH-32) |
| `seedTarget` | `enemyNotYetActed`, for every Shade and every subclass (classes 12.1 has no subclass field). The Trapwright's trap on the path is a content check from floor 2 inside `TrapwrightHasMechanism` (8.2), not a `seedTarget` |
| `leans[]` | Stage 1 unchanged: stealth, surprise and Steal **P**; own tempo **P**; single-target damage **s**; skills **s**; exploration **s**; mobility **s**; items (Trapwright lens); morale (Cutthroat lens: Herald + Shade = 2 classes, legal); reactions (Swordsworn lens, M2 under the fallback, TL-37: Vanguard + Shade = 2, legal; cross-document comparison 2026-11-23, CC-73) |
| Tools, not leans (SH-27) | Duck; Tainted Blade's Poisoned; Make an Example's Frightened; Tripline's Prone; Drag Line's movement. The control row (Arcanist P, Herald s) and the damage-types-and-riders row are full |

**Pending request: Cased (exploration and information design 4.5.2;
XI-27; M3, conditional on the Trap Eye re-scope, TL-6).** The
exploration lead asks the Shade owner for a 0-click class rider: when
the Shade has had line of sight to every member the party has seen this
floor and that is not Hidden of a known unaware group for 2 consecutive
ticks *(illustr.; `cased.ticks`)* with no successful noticing roll by the
group, the group is Cased for the floor (the window restarts whenever a
new member is seen). Cased gives the one-lap patrol forecast horizon
(exploration and information design 4.8.1), a "Cased" label ("from what
you have seen"), and a noticing-odds band only if EP-41 is ruled and the
enemies 13 flag allows it. It counts only seen, non-Hidden members so it
never leaks a Hidden member. Not yet ruled by the Shade owner; if
refused, or TL-6 is unruled at M3, Cased is held to Later.

### 7.2 At-wills and the bonus action

**At-wills:**
1. A melee weapon attack with the active main hand and its verb.
   Off-hand attacks and Nick follow weapons 11.3 **(settled)**.
2. A thrown attack (dagger or handaxe, range 5) or a launcher attack.

**The bonus action is contested, and the card says so (lead's call).**
Fade, Catch Breath, Vanish, From Nowhere, Draw Out, drinking a potion,
the set swap and a non-Nick off-hand attack all compete for it. Dagger
Nick is the default off hand for this reason.

A scroll read (the DEP-1 Arcana reader) uses the camp spend, so it
competes with Vanish and From Nowhere (CN-11, **agreed**).

### 7.3 Tricks (`pool.tricks`, rest): "escape, soak or mend?"

**Pool *(illustr.; SH-24)*:** 2 at levels 1–2, 3 at levels 3–7, 4 at
levels 8–10. Spare Trick (level-10 talent) adds +1 that cannot pay a
`strike` option.

**Spare Trick legality (lead's call).** Tricks stay one integer count.
While the Spare Trick is unspent, `spareReserve` = 1, otherwise 0. A
`strike` option is legal only if `tricks.remaining − spareReserve ≥` its
Trick cost; a non-strike option spends the spare first (it sets
`spareReserve` to 0). Example: Tricks 5 (4 + spare), 4 spent on Ducks,
1 left: the first Duck spent the spare, so `spareReserve` is 0 and a
Fade-Hide is legal. With today's values the reserve never binds (at
most 2 strikes against four ordinary Tricks); the rule keeps the check
defined if the values change. Test in 12.1.

**Global Tricks rules (Vanguard 6.4 generalised; lead's call):**
1. **Cost:** 1 Trick per use unless a line says otherwise. Option records
   carry `costByTrigger[{trigger, tricks, strike}]` where the cost
   depends on the trigger (Counterstroke, 8.3), and `costWaivers[]`
   naming the source state that waives a cost (`bladeDance.active`, the
   From Nowhere rider windows, the Ambuscade and From Nowhere free No
   Witnesses). A waiver is not a refill (VG-11).
2. **One option per trigger.** The stated pairings, each inside one
   reaction, are Turn Aside (Duck with Counterstroke) and `breathOnDuck`
   (Duck with Catch Breath); they say so explicitly.
3. **The strike limit** (7.9): a record tagged `strike` also spends 1
   from it, whether or not it spends a Trick.
   - Tagged: Fade's Hide mode, Cross Steel (a Fade mode), No Witnesses
     (its Hide modes), Throat-Cut (a `pool.tricks` record with Trick cost
     0, 8.1) and Counterstroke.
   - **Stated exceptions, not tagged:** Jury-Rig (`device`, 8.2: Spring
     and Killing Ground deal trap damage, read as content) and the No
     Witnesses II carry-only mode (8.1).
   - **Not tagged** (no Hide, attack or carry): Duck, Catch Breath, Draw
     Out, Make an Example and Sworn Guard's ward.
   - **Signature pip (level 4 only, illustr.):** the second level-4 pip
     may be spent only by the subclass signature strike (No Witnesses'
     Hide modes, Counterstroke); Fade-Hide and Cross Steel spend only the
     shared pip.
4. **No Shadowed exception.** Shadowed is class-blind and works as
   written (armour 14.2). A Shadowed build is reported separately, as
   the trinkets +10% rule does.
5. Condition-applying options never fire on reaction attacks (weapons
   7.2 rule 3) **(settled)**.
6. **Rewrites are modes (lead's call).** A level-6 rewrite that adds a
   Tricks use joins the entry it rewrites as a mode: Cross Steel is a
   fourth Fade mode (Fade keeps Hide, Disengage and Dash); Make an
   Example is a mode of No Witnesses; Counterstroke, Open Guard and Turn
   Aside's follow-up are modes of the Sworn Guard and Duck entries they
   follow. Throat-Cut is not a picker entry (it fires from Opening's
   application).

**Catalogue: Tricks options**

| Option | Source | Wave | Cost | Tag | Rank I | II | III |
|---|---|---|---|---|---|---|---|
| **Fade** | core, 1 | M2 | bonus action, 1 Trick | Hide mode: `strike` | Hide, Disengage or Dash | L5: Hide has advantage on the Stealth roll against observers more than 3 squares away (two d20s are always drawn for a Fade II Hide; observers more than 3 squares away read the higher, the others the first drawn, so random-number use never depends on position); Disengage gives advantage on this turn's slip-past check | L8: one use may combine Disengage and Dash (never with Hide) |
| **Duck** | core, 1 | M2 | reaction, 1 Trick | — | Halve one hit's damage; sets Answer | L5: cancels that hit's rider or critical-rider attempt | L8: may instead halve area damage after a failed Reflex save (no Answer; not on damage Tumble Clear already halved) |
| **Catch Breath** | core, 1 | M2 | bonus action, 1 Trick | `restDefensiveTool`, `chassisOnly` | Heal FloorDiv(Recovery, 2); sub-limit 1 per rest, 2 from level 6 **(settled)** | — | — |
| **Draw Out** | L7 talent, or `precision` offer from L5 | M2 | bonus action, 1 Trick | not `strike` | Acrobatics against an adjacent hostile's guile; on a success it counts as Beset for your melee attacks this turn (`drawnOut`, 13.1); on a failure the Trick is spent | L8: reach +1 | — |
| No Witnesses | Cutthroat 3 | M2 | on a kill, 1 Trick | Hide modes `strike`; carry-only mode untagged | 8.1 | 8.1 | 8.1 |
| Jury-Rig | Trapwright 3 | with the Trapwright | bonus action, 1 Trick | `device` | 8.2 | 8.2 | 8.2 |
| Sworn Guard | Swordsworn 3 | with the Swordsworn | reaction, 1 Trick | ward untagged; Counterstroke mode `strike` | 8.3 | 8.3 | 8.3 |

`TricksPickerSize` ≤ 6 counts **entries, not modes** (rule 6): the
largest reachable set is Fade, Duck, Catch Breath, the signature, Draw
Out and one offer option.

### 7.4 Fade, Duck and Catch Breath

**Fade** (the stage-1 "bonus-action Hide, Disengage or Dash", named;
core):
- This is the ability 7.2 class hook, pre-cleared against Runner's Boots
  **(settled)**.
- Hide still needs a line-of-sight break and a Stealth roll against
  passive Perception.
- **Preview (lead's call):** the Hide mode shows its success chance
  against the best passive Perception among the observers before you
  commit, because the strike pip is spent on use, success or not.
  Spending the pip only on a success was weighed and is **held** for the
  rerun: the level 1–3 cells sit 0.1–2.5 points under the cap, and a miss
  there would reach the 10.7 lever that comes back to the client.
- Ranks in 7.3.

**Duck** (core):
- **Marker (weapons 7.5 step 8a, SH-6):** when a weapon attack roll hits
  you, step 8a only flags the damage event as Duck-eligible and records
  the attacker; nothing is offered there. Non-weapon attack rolls carry
  the same flag from ability 14.3's matching step.
- **Offer (DH 20.3, between step 3 and the new step 3a):** the reaction
  is offered with the event's step-3 total known, after per-part
  resistance, type ends and temporary HP. `bigHit` (7.12) reads that
  total against current HP. Duck is not offered when the step-3 total is
  0 (temporary HP absorbed it all).
- **Effect (DH 20.3 step 3a):** total = max(1, FloorDiv(total, 2)).
  Steps 4 onward (floors, the save chain, HP) read the halved total.
- **Concentration (lead's call):** uses the post-3a value. DH 20.3's
  "concentration uses step 3's value" and weapons 7.5 step 8's "then any
  concentration check" are amended to point to DH 20.3 step 3a (SH-6).
- **Reaction use:** Duck uses the one reaction, so it never follows a
  window candidate that spent the reaction on the same roll. A step-0
  fill (Blade Dance) or a carried ward (Held Guard) spends no reaction
  and does not block it: a hit that gets through either may be Ducked if
  you still have your reaction. The same-reaction pairings are Turn
  Aside and `breathOnDuck` (7.3 rule 2).
- **Answer:** it sets Answer against the attacker.
- **Registry:** it is not Dodging (a roll mode). Checked against
  Dodging, Adamant and Hungering.

**Catch Breath** (core, **settled**): heals half your Recovery;
`restDefensiveTool` and `chassisOnly`; excluded from every offer; Clear
Head upgrades it.

### 7.5 Vanish (`pool.vanish`, camp)

**Pool *(illustr.; SH-24)*:** 2 at level 1, **3 from level 2**. The
step is a spread and level-2 boss lever (10.4). Vanish is camp damage,
outside parity under VG-12, and the step also raises the camp share.

> **Vanish I.** Bonus action and the turn's camp spend. Hide without
> needing to break line of sight: you still roll Stealth against each
> enemy's passive Perception, and Hidden's end rules stand (conditions
> 18.7; CL-12c). **Whether or not the Hide succeeds**, gain a carry: your
> first Dexterity-rolled weapon hit on your own turn to which you apply
> Opening before the end of your next turn counts as **Hidden for
> Opening** (the stealth tier), against any target.

- This rewords the stage-1 text in two places (SH-2): "counts as a first
  strike" becomes "counts as Hidden for Opening" **(agreed, Q1)**; "first
  hit" becomes "first Dexterity-rolled weapon hit on your own turn to
  which you apply Opening" **(lead's call)**.
- **Carries (lead's call; state in 13.1).** Every carry setter (Vanish,
  the Arts, No Witnesses II, Cross Steel, Blade Dance II) writes one
  `carries[]` entry `{sourceOptionId, expiresAtOwnerTurnEndIndex,
  targetFilter (any | notAdjacentAtTurnStart:[ids]), usesLeft,
  coveredTargets[], surprisedDie, freeNoWitnesses}`.
  - A new carry from the **same source** refreshes that entry's expiry
    and never stacks; carries from different sources coexist, and a hit
    consumes at most one (the one expiring first, then the earlier
    written).
  - **Consumed** when Opening is applied, or traded (Tainted Blade), on
    an own-turn hit that meets `vanishCarry` and **no other stealth
    trigger**. A hit that is already Hidden or Surprised does not consume
    it; From Nowhere hits never consume it.
  - **Reaction hits never consume a carry and never take the stealth die
    from one** (a Counterstroke takes the open tier, unless a line says
    otherwise). Trick Shot never consumes one (no weapon hit).
  - **Out of combat:** a carry set while `combatActive` is false expires
    at the end of the owner's first turn after combat starts (second with
    Long Shadow), or at the next rest if no combat starts. The opening
    blow of a fight (DH 10.4) counts as an own-turn hit.
  - **Long Shadow** extends only Vanish and Art carries.
  - Owner Downed and combat end clear every carry.
- **II (L8):** Vanish also takes the Disengage action.
- **Other rules:** no maximised dice **(settled)**; greyed while you
  cannot take a bonus action; registry checked against Veiled, Shadowed
  and Duskhem.
- **Arts:** one per subclass, rank I at class level 5 and rank II at 9;
  an Art is an alternative Vanish option and never combines with base
  Vanish; bounded by **`CampOptionBound`** (SH-5; `SurgeOptionBound`
  renamed and generalised): expected damage ≤ 1200‰ of one Attack
  action against the reference encounter; `actionsGranted` ≤ the pool's
  base option (Surge 1, **Vanish 0**); areas ≤ 25 squares, selective
  only; no hard control; forced movement ≤ 3; M1/M2 conditions only.
  `CampOptionBound` is scoped to non-slot, non-slotless camp pools;
  slot pools are bounded by `SpellTierBound` and slotless casts by
  `SlotlessCastBound` instead (ARC-2; Arcanist design 8.1, 11.2).

**Catalogue: the Vanish row**

| Option | Level | Wave | Cost | Text (summary) |
|---|---|---|---|---|
| Vanish I | 1 | M2 | bonus action + the turn's camp spend | Hide without a line-of-sight break; the carry whether or not you succeed |
| Vanish II | 8 | M2 | same | Also takes the Disengage action |
| Ambuscade (Cutthroat Art) | 5 / II at 9 | M2 | same | 8.1 |
| Tripline (Trapwright Art) | 5 / II at 9 | with the Trapwright | same | 8.2 |
| Blade Dance (Swordsworn Art) | 5 / II at 9 | with the Swordsworn | same | 8.3 |
| From Nowhere | 9 | M2 | bonus action + camp spend; `pool.fromNowhere` camp 1 | Every hit this turn takes the stealth die (7.7) |

### 7.6 Talents

Menus of 3, each with an upgrade **(settled shape)**; texts are lead's
calls.

**Level 2.** Only Tainted Blade reshapes the signature verb; the other
two are honest utility, as the Vanguard's Grim Resolve was.

- **Tainted Blade** (upgrade, Opening; the first dice trade):
  - When you apply Opening's **stealth-tier dice** on your own turn, you
    may forgo them. The target then makes a Fortitude save against your
    ability difficulty or is Poisoned: `turns:2`, or `turns:1` against
    elites and bosses.
  - **Timing (lead's call):** the trade is an on-hit spend chosen at
    weapons 7.5 step 7, before damage; it reserves the step-9 shared
    rider slot on that target, which blocks the weapon verb's rider and
    Topple on it, and the save is rolled at step 9. It never fires on
    reactions.
  - On a From Nowhere turn it may trade at most once.
  - **Shrugged Off** (9): a target that saves is immune to your Tainted
    Blade for the fight.
  - Against elites and bosses it may land **once per fight per target**
    (`taintedLanded[]`, 13.1; a landing, distinct from Shrugged Off's
    save set).
  - Greyed against `poison`-immune targets.
  - The open amount can never be traded, so the verb stays Opening
    rather than a poison applicator.
  - **II (L8):** the save has disadvantage if the target is Surprised or
    you are Hidden from it.
- **Deep Pockets** (adds): belt +1, hard maximum 3 (items 24)
  **(settled text)**.
- **Light Fingers** (upgrade): advantage on Steal. One try per carrier
  per floor stays **(settled, S-5)**. Pinch is Later (9).

**Level 7** (a trigger option is required, **settled**):
- **One trigger** drawn from `triggerMenu`, excluding owned and barred
  triggers.
- **Draw Out** (adds a Tricks option; 9).
- **Clear Head** (upgrade, Catch Breath): it also ends one of Poisoned
  or Bleeding on you, and you stand from Prone at no movement cost.
  Frightened is not on its list, because its approved cures are rally,
  Clear-minded, `sourceGone` or an action tonic.

**Level 10** (when the talent source is picked):
- **Long Shadow** (upgrade, Vanish; listed first): your Vanish and Art
  carries last until the end of your second turn (7.5). Registry:
  Duskhem.
- **Spare Trick** (upgrade): Tricks +1, "a spare Duck or Catch Breath";
  it cannot pay a `strike` option.
- **Tumble Clear** (adds; the Evasion shape): against area damage that
  allows a Reflex save, a success takes none and a failure takes half.
  Registry: Enclosed.
- Sure Hands moves to the `delve` offer lane (7.11).

### 7.7 Capstone: From Nowhere

`pool.fromNowhere`, camp 1, `minClassLevel 9`; on the Vanish row.

> **From Nowhere.** Bonus action and the turn's camp spend (never on a
> Vanish or scroll turn). Until the end of this turn, **every**
> Dexterity-rolled weapon hit you make takes Opening at the **stealth
> die (2d6)**, triggers waived. Off-hand, Nick, thrown and launcher hits
> are included; reaction and granted attacks are excluded. Opening's
> allowance counts as spent for the rest of the round.

- **Acceptance criterion (classes 14):** it changes the primary lean
  itself, because every strike becomes an opener. It grants no refill,
  no second reaction and no second camp spend.
- **Interactions (lead's call):** on From Nowhere hits the Cutthroat's
  Surprised part applies against a Surprised target; Tainted Blade may
  trade at most once that turn; Trick Shot does not apply (it is not a
  weapon hit); no carry is consumed.
- **Nova *(scratch, L9, three attacks)*:** normal p95 37; capstone p95
  58 (ratio **1.57**); with a Vanish-success Hidden 1.62; with the
  Cutthroat Ambuscade carry ≤ 1.76. All pass the 2.0 bound. A rerun is
  owed with the interactions above and the revised Trapwright and
  Swordsworn riders (8.2, 8.3; 10.3).
- **`lingering` is removed** from the menu (decision 10).
- **Subclass riders** (automatic, L9): section 8.

### 7.8 Pools by level *(illustr.)*

| Pool | L1 | L2 | L3 | L4 | L5 | L6–7 | L8 | L9 | L10 |
|---|---|---|---|---|---|---|---|---|---|
| Tricks (rest) | 2 | 2 | 3 | 3 | 3 | 3 | 4 | 4 | 4 (+1 Spare Trick) |
| Strike limit (per rest, not counted) | 1 | 1 | 1 | 1 (+1 signature) | 2 | 2 | 2 | 2 | 2 |
| Catch Breath sub-limit (settled) | 1 | 1 | 1 | 1 | 1 | 2 | 2 | 2 | 2 |
| Vanish (camp) | 2 | 3 | 3 | 3 | 3 | 3 | 3 | 3 | 3 |
| From Nowhere (camp) | — | — | — | — | — | — | — | 1 | 1 |
| Opening (turn allowance) | 1d6 / +2 | same | same | same | 2d6 / 1d6 | same | same | same | same |

### 7.9 Turn allowances and sub-limits

**(lead's call; SH-4)**

- **Opening's allowance** is a `turnAllowance` (once per round), counted
  0 under VG-10 **(settled)**.
- **The strike limit** (`strikeLimit`, per rest, 1 at levels 1–3, 1 + 1
  signature pip at level 4, 2 from level 5 *(illustr.)*) is a per-rest
  counter declared on `pool.tricks`'s record and spent by every record
  tagged with the tag the pool declares (`strike`), **whether or not that
  record also spends a Trick** (Throat-Cut, a waived Counterstroke). It
  is a frequency limit and counts 0: "A per-rest counter on one named
  pool's record, spent by records tagged with its one declared tag,
  whether or not they spend from the pool, is a frequency limit and
  counts 0; it is not a named pool" (SH-4 extends VG-10 from one option
  to one tag). The signature pip is part of the same counter, restricted
  by a `spendableBy` list on the pip.
- **Validator `HideLimit`** (static, over records): every record that
  grants Hide, a carry or a weapon attack and is reachable from
  `pool.tricks` (or rides on a Tricks option, such as Throat-Cut) is
  tagged `strike`, or appears on a named waiver allow-list in the tuning
  file. The allow-list today: the No Witnesses II carry-only mode; the
  Ambuscade and Cutthroat From Nowhere free No Witnesses
  (`freeNoWitnesses`); the Trick waivers of Blade Dance and the
  Swordsworn From Nowhere rider (which waive the Trick, never the strike
  pip); and Caught Out's free Killing Ground (a `device` record). Every `costWaivers[]` entry (7.3 rule 1) must name an entry on
  this list. At runtime the counter enforces the limit; the static check
  is what fails the build.
- **Display:** pips on the Tricks row, e.g. "Tricks ●●● (1 may strike)";
  the signature pip is drawn in the subclass colour. Never shown as a
  second pool.
- **The Catch Breath sub-limit** stays as settled (classes 8.2).
- **Source-held state** (`answeredBy[]`, `carries[]`, `caughtBy[]` and
  the rest of 13.1) is state, not pools.

### 7.10 Starting kit *(illustr.; 2 potions settled)*

| Kit | Armour | Set A | Set B | Belt / coin |
|---|---|---|---|---|
| **Blades** (default) | leather (AC 15, Supple) | shortsword (Vex, P) + dagger (Nick, thrown, P) | sabre (Flourish, S) + dagger | 2 healing potions; about 15 coin |
| **Launcher** | leather | longbow (Aim, P; Str 0 meets it) | sabre + dagger | same |

- No other presets. No notable armour, so the kit raises no Shadowed
  question.
- **Array:** Dex +3, Int +2, Con +1, Wis +1, Str 0, Pre −1.
  **Strength is never negative:** it would cut every Dexterity-rolled
  light or finesse hit (weapons 6.2). The Cutthroat preview suggests
  Pre 0, Int +1.

### 7.11 Offers, lanes and `triggerMenu`

**`offerLanes`** *(illustr.)*:

| Lane | Weight | Carries |
|---|---|---|
| `shadow` | 1500‰ | Stealth and Acrobatics mastery; Fade, Vanish and From Nowhere trigger offers; Cutthroat Intimidation training and mastery (`requires: subclass:cutthroat`) |
| `precision` | 1300‰ | Opening and Duck triggers; Draw Out (from level 5); verb training in a light, finesse or launcher family; physical damage that ignores resistance |
| `delve` | 1200‰ | Mechanics, Arcana (DEP-1) and Perception mastery (Tactician reachable, never copied); Medicine; Sure Hands (out of combat, Mechanics and Acrobatics treat a d20 of 7 or less as 8; never Stealth, never in combat; also cuts Take time to 3 ticks: traps and locks design 6.6, TL-5) |

- **`offerLaneAdjust` ×1333‰:** Cutthroat on `shadow`, Trapwright on
  `delve`, Swordsworn on `precision`.
- **`offerExcludes`:** `capacity:tricks` (each Trick can become a strike
  or a Sworn Guard); `capacity:vanish`, `capacity:fromNowhere`,
  `timescale:vanish`, `timescale:fromNowhere`; `cost:*` (`cost:hide`
  would double-dip Fade); any capacity or cost change on Catch Breath or
  the strike limit; armour and shield training; `shape`; concentration.
- **The scorer** shows each trigger offer's measured "already live"
  rate.

**`triggerMenu`** (never repeated; ids *illustr.*):
- **Opening:** the added triggers in 6.5.
- **Duck:**
  - `duckFromRanged`: Answer set by a Duck adds the `launcher` scope to
    Answer against that attacker, so your launcher attacks qualify on it
    (still the open amount). A data change to the `answer` record's scope;
    it is in the registry and the parity check (`OpeningCeiling` open
    share);
  - `breathOnDuck`: Catch Breath in the same reaction, paying both
    Tricks and the sub-limit; registry: Stubborn Breath, Hungering.
- **Fade:** `fadeStands` (Fade's Disengage or Dash also stands you from
  Prone).
- **Vanish:**
  - `vanishBreakFree` (Vanish ends Grappled on you; M2 also Restrained);
  - **`falseTrail`** (9.1: for each group from whose every member with
    line of sight to you the Vanish made you Hidden, write
    `lastKnown(square)` in that group's shared record, the square
    unoccupied, non-hazard, seen by you and within 3 squares of you).
- **Subclass entries:** carry `requires: subclass:<id>` (section 8).
- **Removed or barred:** `lingering`, `launcherOpening`,
  `vanishDisengage` (now Vanish II).

### 7.12 Default reaction policies

| Reaction | Default |
|---|---|
| Opportunity attack | `always`, `nonMinion`; Opening not spent |
| Duck | **`bigHit`** (SH-7; one definition), read on the DH 20.3 step-3 total against current HP (7.4): the total is ≥ 250‰ of max HP, or it would drop you to 0 and the halved total would not |
| Opening on reaction attacks | `never` (Counterstroke `always`, open tier only, 7.5); Killing Ground springs under Trick Shot follow the same `never` unless set |
| Opening on own-turn hits | `applyOnFirstQualifyingHitOnOwnTurn` with `holdCarryOnOverkill` on (6.4) |
| `breathOnDuck` | `always` when HP after the Duck is ≤ half max |
| Sworn Guard | `ifItChangesOutcome`, `nonMinion` |
| Counterstroke / Open Guard | `always`, `nonMinion` / `whilePoolAtLeastHalf`, `nonMinion`, read for Open Guard as `tricks.remaining ≥ ceil(tricks.max / 2)` and `strike.remaining ≥ 1` |
| Killing Ground | `always`, `nonMinion`, **`noAllyHarm`** (SH-7) |
| Parry from a Parrying-property weapon | canonical `ifItChangesOutcome`, `nonMinion` |
| Draw Out | `ifOpeningUnavailable` (only if no known trigger is live on a reachable target) |

### 7.13 `effectKinds` for the registry

| Feature | effectKinds |
|---|---|
| Opening | `damage.extraDice.opening` (and the rank-I flat part `damage.flat.opening`); named apart from the weapons 6.5 launcher Precision term |
| Answer | `trigger.answer.set` |
| Fade I / II / III | `action.cost.bonus{hide, disengage, dash}`; `check.advantage{stealth>3, slipPast}`; `action.disengageAndDash.bonus` |
| Duck I / II / III | `damage.halve.reaction`, `answer.set`; `rider.cancel`; `damage.halve.save` |
| Catch Breath | `heal.recovery.half` |
| Draw Out | `opening.trigger.grant.beset` |
| Vanish I / II | `hide.noLosBreak`, `carry.set`; `action.disengage` |
| From Nowhere | `opening.unlimited.turn.stealthDie` |
| Tainted Blade | `opening.trade`, `condition.apply:poisoned` |
| Deep Pockets / Light Fingers | `belt.capacity+1` / `steal.advantage` |
| Clear Head | `condition.end{poisoned, bleeding}`, `stand.free` |
| Tumble Clear / Long Shadow / Spare Trick | `save.area.evasion` / `carry.extend` / `capacity.tricks.nonStrike` |

**Registry pass, named first:** Fade against Runner's Boots
(pre-cleared) and Shadowed; Duck against Dodging, Adamant and
Hungering; Vanish and Long Shadow against Veiled, Shadowed and Duskhem;
Draw Out against the cut Feint, Vex and Helped (it gives no roll mode);
`breathOnDuck` against Stubborn Breath; Tumble Clear against Enclosed.
Subclass rows are in section 8.

### 7.14 Recommended creation defaults and ladder preview

**Defaults (lead's call):** skills Stealth, Medicine (DH 8, "close to
compulsory" solo) and Perception (surprise and Tactician); kit Blades;
L2 Tainted Blade; L3 Cutthroat; L6 Make an Example; L7 Clear Head; L10
Long Shadow; offers the scorer's pick, with each trigger offer's
"already live" rate shown.

**Ladder preview lines (the turn-1 tutorial):**
- "Hide or Vanish, then hit: big die. Make them miss: small bonus."
- "Blades finish fights; the bow opens them."
- "Trapwright wants Mechanics; Arcana reads scrolls (and competes with
  Vanish for the camp spend)." Shown only while the Trapwright is
  offered in the current wave **(lead's call)**.
- "Swordsworn guards with a blade in hand: the bow must be stowed (set
  swap) to Sworn Guard." Shown only while the Swordsworn is offered in
  the current wave; Sworn Guard is greyed with that tooltip while no
  light or finesse `group:blade` weapon is held **(lead's call)**.

## 8. The three subclasses

**Shared shape (lead's call):** level 3, signature Tricks option I and
clauses; level 5, signature II and the Vanish Art I; level 6, a verb
rewrite (choose 1 of 2); level 7, signature III; level 9, the From
Nowhere rider and Art II. No subclass adds a pool or a row, so reading
stays 3. `SubclassCopiesBaseLean` passes for all three, because every
subclass mechanic id is new.

### 8.1 CUTTHROAT (M2): "no witnesses"

**Specs into:**
- the quiet kill between fights: silent kills and broken groups,
  cowable at 100% of budget with advantage, so fights are skipped;
- the ambush opener: the Surprised die and Ambuscade;
- the kill-then-fade loop: No Witnesses;
- one of two answers at level 6: perfect stealth (Throat-Cut) or terror
  (Make an Example).

**Does not spec into:** bosses (never Surprised, Wary, `fearless`);
`fearless` or mindless groups; worst-member party stealth; making
anything Surprised. **No Cutthroat feature applies Surprised**, so "no
automatic surprise of bosses or elites" holds by construction.

**Data:** `mechanicIds [silentKillBroken, surprisedOpeningDie,
noWitnesses, throatCut, makeAnExample, ambuscade]`.

**Level 3 clauses:**
- **Quiet Work** (SH-10):
  - A silent kill (enemies 12.2) of a **non-minion** member sets the
    group's `broken` flag. Today only the leader's silent kill does.
  - **Second route:** your second silent kill against the same group on
    this floor (minions count; `silentKillsBy[partyUnit]` on the group,
    13.1) also sets it. This ships **only together
    with a "body noticed" rule** in M2: a patrol that sees a corpse
    becomes Alerted to its square. That rule is owed by the enemies
    owner. Without it, the non-minion route alone ships.
  - **Layout (FL-9):** the floor offers exactly one route-avoidable
    fight, a guard post skipped by the Bypass; the lair and every other
    main-chain fight host are cut vertices, so `SkippablePool` holds at
    ≥ preBossPool − 100 with the patrol counted present (floor layout
    design 6.8). The second-kill route and the fights-avoided threshold
    below are read against that.
  - Never against a boss, a group with a living elite or boss, or a
    `fearless` group. **Evaluated after the kill resolves** (DH 20.3
    step 7): a silent kill of a group's only elite may set the flag if no
    elite or boss of the group is still alive (test in 12.1).
  - One cow attempt per group, on the hero's Intimidation.
- **Surprised die:**
  - Opening deals **+1 at levels 3–4 and +1d4 from level 5** against a
    Surprised target. This is a stealth-tier extra part, allow-listed in
    `RankAddsNoDice`. The d4 from level 5 is the pre-committed lever,
    applied. The +1 is a flat term and is not doubled on a crit; the d4
    is doubled.
  - **Unaware at declaration:** it also applies against a non-elite,
    non-boss unit that was **unaware** of you (3.4) when the attack was
    declared (SH-11). DH 10.4 **(agreed, Q6)** runs the combat-start step
    before the attack resolves, so the blow is in-combat damage and DH
    10.4's damage-floor reading is unchanged; only the target's
    awareness and `combatActive` are read earlier, at weapons 7.5 step 1.
    The branch matters for a silent-kill attempt (enemies 12.2): its
    target's group does not notice the attack, so the target is not
    Surprised.
  - A Tainted Blade trade forgoes this part too.

**Signature: No Witnesses** (`onEvent:kill`, own turn; 1 Trick + 1
strike limit; its Hide modes tagged `strike`; one picker entry whose
modes are Hide, the II carry-only mode and, if taken, Make an Example):
- **I (L3):** when your attack drops a hostile to 0 HP, you may Hide as
  part of that attack. No bonus action is used. The line-of-sight break
  and Stealth roll are normal. The Hide resolves after weapons 7.5 step
  15, so the killing attack does not end it (SH-12: ability 7.2's
  Hide-cost row and the conditions 18.7 end-rule ordering).
- **II (L5):** the line-of-sight break is waived against hostiles 4 or
  more squares away; you gain a carry (7.5) whether or not the Hide
  succeeds; if your strike limit is spent, you may instead pay 1 Trick
  alone to gain **only the carry**, with no Hide. That mode is a stated
  exception to the `strike` tag (7.3 rule 3; on `HideLimit`'s waiver
  list), is counted in the stealth share, and its parity is owed; the
  lever is to drop it.
- **III (L7):** the No Witnesses Stealth roll has advantage. The
  3-square-rule waiver from the drafts is **held** pending the Duskhem
  ruling (trinkets 11 rule 1); if the lead rules it distinct from
  Duskhem and Long Shadow, it replaces this text.
- **Environment kills (M2; TR-24, room terrain design 9.2):** a kill by
  environment damage (collision, contact, harm, Keg, ignition, Conduct)
  is credited by `causedBy` to the unit whose action began it. No
  Witnesses and Make an Example fire for that unit only when the killing
  damage resolves inside that same action; never from ticks or later
  turns. A monster-triggered Keg credits no party unit.

**Level 6 (choose 1):**
- **Throat-Cut** (rewrites Opening; out of combat only; spends the
  strike limit). Modelled as a `pool.tricks` option record with Trick
  cost 0, tag `strike` and trigger `onOpeningApplied{outOfCombat}`, so
  the strike limit covers it (7.3 rule 3, 7.9); it is not a picker
  entry. **Every reading is taken at declaration** (weapons 7.5 step 1,
  before DH 10.4's combat-start step; SH-11):
  - `combatActive` is false; the target is an unaware (3.4), non-elite,
    non-boss unit; you are Hidden from it, which out of combat means its
    group's awareness record for you is `unnoticed` or `lastKnown`
    (enemies 11.2); and the attack is **silent-kill eligible** (no other
    awake member of the group has line of sight to the target or to you,
    enemies 12.2).
  - **Threshold:** if Opening is applied and the hit's DH 20.3 step-3
    total is at least half the target's current HP (`2 × total ≥ HP`),
    DH 20.3 step 6 sets its HP to 0 instead of lowering it, and the kill
    is a silent kill.
  - Otherwise nothing changes: the hit deals normal damage and the
    strike limit is **not** spent. It never changes a fight that starts.
  - It is the 2× silent-kill threshold (classes 6.1).
  - The `strike` tag caps it at 1–2 per rest, closing the per-tick
    unlimited loop (ability 6.8).
- **Make an Example** (rewrites No Witnesses: a mode of its entry, 7.3
  rule 6; not `strike`): when your own-turn attack drops a non-minion
  hostile to 0, instead of No Witnesses you may pay 1 Trick. Choose one non-fearless, non-boss
  hostile within 6 squares that saw the victim. It makes a Will save or
  is **Frightened** of you (`turns:2`, `combatOnly`, `sourceTied`).
  - A unit that saves is immune to it for the fight. It never escalates
    to Panicked or Routed.
  - It is paid from a limited pool, so the major condition is legal
    under conditions 7.1 (SH-13). Recorded as a tool.

**Art: Ambuscade:**
- **I (L5):** as Vanish. Its carry entry has `surprisedDie: true` and
  `freeNoWitnesses: true`: the carried hit takes the Surprised die
  against a non-elite, non-boss target (the target is never Surprised),
  and if that hit kills, No Witnesses follows at no Trick and outside the
  strike limit (on `HideLimit`'s waiver list).
- **II (L9):** the carry entry has `usesLeft: 2`: it covers your first
  hit against each of up to two hostiles before the end of your next
  turn. `coveredTargets[]` binds to the first two distinct hostiles to
  which the carry is applied; a second hit on a bound hostile does not
  use it. Opening is still once per round, so the second use falls on
  your next turn.
- `CampOptionBound`: about +3 expected at level 5 (about 300‰), about
  400–550‰ at level 9. Pass.

**From Nowhere rider:** the first kill on your From Nowhere turn takes
No Witnesses free (no Trick, no strike limit; on `HideLimit`'s waiver
list).

**`triggerMenu`** (`requires: subclass:cutthroat`):
- `noWitnesses.onReaction`: after a kill by your opportunity attack;
  Hide only; the Trick and strike limit are still spent;
- `quietWork.readTheRoom`: after a silent kill, the group's remaining
  members and their awareness states are revealed for the floor;
  information only;
- `dreadCounts` (6.5).
- **Barred:** `surprisedDie.notActed`.

**Offers:** Intimidation training, then mastery, in the `shadow` lane.
This makes the cow payoff reachable on a Dexterity hero.

**effectKinds:** `group.flag.broken.silentKill`;
`opening.extraDie.surprised`; `hide.onKill`; `carry.set`;
`check.advantage:stealth.onKill`; `silentKill.threshold.double`;
`condition.apply:frightened.onKill`; `art.surprisedDie.carry`;
`hide.onKill.free`.

**Registry:** No Witnesses against the Duskhem Cloak and Long Shadow;
Make an Example against the Herald's Cadence Frighten, the Dirge's
`twoTargetFrighten` and boss roars; Ambuscade against Veiled and
Shadowed.

**Weaknesses (on the card):** a weak boss phase; fearless and mindless
themes; many eyes (no silent kills where group members watch each
other); it needs scouting play; cowing needs Presence and Intimidation;
No Witnesses competes with Fade-Hide for the same strike limit at level 3
(eased at level 4 by the signature pip, 7.9).

**Staging:** M2. The body-noticed rule (if not taken in M2) and Stake
Out are Later.

**Reports owed:** silent kills, broken groups and **fights avoided per
floor** against a baseline Shade. This is a lever with a threshold, not
just a report: Cutthroat ≤ baseline + 1 fight avoided per floor. If it is
exceeded, the first lever removes the second-kill route.

### 8.2 TRAPWRIGHT (M2 only if trap records land in M2; otherwise an unlock): "the room is rigged"

**Staging (agreed, Q2):** the Trapwright ships in M2 **only if** the
trap records (DEP-S1) land in M2 with dungeon generation, with the
content reading of trap damage (`SpringContentBound`). Otherwise it
moves to the unlocks and the Swordsworn is promoted to M2. **The unlock
fallback is the expected default** (the client: "keep it for later").
How the call is made is in 14. **Resolved (2026-11-21; TL-37):** the
fallback applies; the Swordsworn ships in M2 and the Trapwright is the
first Shade unlock in M3 (traps and locks design 0.1, 6.16).

**Pending request (hazards design 6.15, 18):** a Shade hazard hook owed
before Voids ship, and the Jury-Rig Stir re-time (C ± 1, never below 2;
M3, the Shade owner's call); recorded in 18 (cross-document comparison
2026-11-23, CC-7).

**Specs into:** known traps as weapons (remote springing, re-arming and
reaction timing); setup-flask tempo (oil before fire, water before cold
or lightning); Opening through **Caught** (launchers included); forced
movement onto traps (L6); doors (Wedge); Mechanics and Intelligence (the
`delve` lane).

**Never builds:** placing devices is ground-zone play (ability 8.4,
**agreed**) and the Sapper's lane (classes 9.1; SH-26).

**Hard dependency (SH-15, SH-16):**
- **Trap records:** `triggerSquares`, `mechanismSquare` (where Spring and
  the hand-trigger act), `area`, `effect`, `tier`, `detectDifficulty`,
  `disarmDifficulty`, `armed`, `knownToParty`, `loud`, `rearmable`,
  `hardPart` + `hardFallback`. (Shipped in M2 inside the trap record
  superset: traps and locks design 11.1; TL-10.)
- **`TrapwrightHasMechanism`** reads generated floors, which change by
  seed, so it is split in two **(lead's call)**:
  - **(a) a generator constraint, checked by a property test** over N
    fixed seeds per floor *(illustr. N = 50)*, which must hold on every
    seed: from floor 2, at least 2 main-path traps per floor, at least 1
    known-or-detectable trap on the main path before the first fight
    room (the old `knownTrapOnPath`), at least 1 trap in or within 6 of a
    fight room, and at least 2 closed unlocked doors *(illustr.)*. Boss
    rooms are not counted. The same test reports the **dead-room rate**
    (fight rooms with no trap within 6) per floor and fails above 500‰
    *(illustr.)* from floor 2;
  - **(b) the static `SpringContentBound`** below, over the generator's
    density parameters.
- **The content reading of VG-12** **(agreed, Q2)**: damage from traps
  and flasks is read as dungeon content, like flask damage, bounded by
  the static **`SpringContentBound`** over the generator's parameters
  (no seed is read): `maxTrapsPerFightRoom × 1500‰ × maxTrapAvg(floor) ≤
  50‰ × ref(levelForFloor(floor)) × roundsPerFloor`, FloorDiv
  throughout *(illustr.)*, with `levelForFloor`, `roundsPerFloor` and
  `ref` in the tuning file. It is pass or fail; the simulator separately
  reports springs per fight and the observed share in both directions
  (13.3). It is required: under the strict reading the subclass models
  at 117–120%.

**Data:** `mechanicIds [trapEye, caughtTrigger, juryRig, killingGround,
tripline, caughtOut, dust, dragLine | trickShot]`.

**Level 3 clauses:**
- **Trap Eye:**
  - (a) Gain Mechanics training, or another listed skill if you already
    have it.
  - (b) For party trap sense, your value is 10 + Intelligence +
    proficiency, used when it is higher than the best member's (stat
    model 6 trap-sense row; SH-19). Only party passive *stealth* is
    barred from leans; trap sense is not.
  - **Re-scope owed (TL-6; with the Trapwright, M3):** the changed stat
    model 6 trap-sense row already gives 10 + Intelligence + proficiency
    to every Mechanics-trained unit, so (b) adds nothing as written. The
    re-scope is the Shade owner's call, pending; the recommendation is
    double proficiency on trap sense (traps and locks design 6.4).
    The pending Cased rider request (7.1; exploration and information
    design 4.5.2, XI-27) waits on this ruling.
- **Caught** (Opening trigger, open tier; launchers and thrown attacks
  qualify; SH-17):
  - A hostile counts if, since the end of your previous turn, it took
    damage from or failed a save against a trap you sprang (by any
    route), a flask you threw, your Dust, or your Tripline.
  - Held as `caughtBy[]` on the source; cleared at your turn end, at
    combat end and when you are Downed. It never counts for the event
    that set it.
  - **It arrives at level 3 as the one stated exception to
    `TriggerFromFive`**: at levels 3–4 the open tier pays only +2.
    Modelled at 109.9 / 107.3% at level 3 (10.1). Lever: move it to
    level 5.

**Signature: Jury-Rig** (bonus action, 1 Trick; tag **`device`**: not
`strike`, because it adds no Hidden uplift and its damage is read as
content; one picker entry with modes):
- **I (L3):**
  - **Spring:** trigger a known armed trap whose trigger square or
    `mechanismSquare` you can see within 2 squares. Mechanics check
    against its `disarmDifficulty` (as the NM-1 hand-trigger). On a
    failure the Trick is spent, noise 4, and the trap **jams**: for this
    fight in combat, or until floor end out of combat (`jammedUntil`,
    13.1; no free retries). Universal Jams last to floor end; only this
    Jury-Rig jam keeps the per-fight scope (traps and locks design 6.6;
    TL-4). The trap resolves with its own data at
    source `environment`, you as causer. It is not selective, and the
    preview carries the lethal tag (DH 10.4).
  - **Re-arm:** an adjacent sprung or disarmed trap, trained Mechanics.
    No roll if you disarmed it yourself this floor (the trap's
    `disarmedBy`, 13.1). Once per trap per floor through Jury-Rig
    (`juryRigRearmedThisFloor`); the universal action route is also once
    per trap per floor (`rearmUsedThisFloor`), not while a group the trap
    has hit is Alerted or in combat, so a Trapwright re-arms one trap at
    most twice per floor, once by each route (traps and locks design 6.6;
    TL-15).
  - **Toss:** throw a belt **setup flask** (water or oil in M2) as the
    universal throw. Offensive flasks stay an action, one per turn.
  - **Dust (Toss mode; lead's call, check round):** with no flask, throw
    a handful of grit at a hostile within 5 that you can see: no item,
    no damage, no other effect; it makes a Reflex save against your
    ability difficulty or is **Caught** (8.2). It gives Caught and Toss a
    fuel floor on floors where water and oil are scarce (consumables 16:
    sundries only, never sold). It ships only with the Trapwright; the
    level-3 parity cell is rerun with it (Caught uptime), and the lever
    is to cut it.
- **II (L5):**
  - Spring range 6.
  - **Wedge:** an adjacent closed, unlocked door counts as locked for
    monster paths until it is forced, opened from your side, or the next
    camp. Forcing it is an action, Athletics against 10 + your Mechanics
    bonus, loud 6. One wedge at a time. Adopted **only if the enemies
    owner accepts the AI force-door term** (SH-18); otherwise the mode
    is cut (a lead's matter, not a client one).
- **III (L7): Killing Ground.** Spring may instead be your reaction,
  still 1 Trick, when a hostile enters a known armed trap's trigger
  square or one of its `area` squares within 6. It resolves before the
  mover enters its next square. On a trigger square it **replaces** the
  trap's native trigger (one resolution, never two). Reactions never
  apply hard control, so the trap's `hardFallback` applies. Policy
  (7.12): `always`, `nonMinion`, `noAllyHarm`.
- **Trap rules (SH-15):** a re-armed trap, by anyone, fires at 500‰
  damage and without its hard part; the multiplier never stacks (a trap
  re-armed twice still fires at 500‰); a trap's hard part fires at most
  once per fight. The 500‰ re-arm, the hand-trigger and Trigger Shot
  ship universally in M2 (traps and locks design 6.6, 6.7; TL-13,
  TL-15).
- **First lever:** if Jury-Rig use pushes the content bound or parity,
  apply a `springLimit` per-rest sub-limit on Spring (1, then 2; one
  option, so VG-10 covers it). The fallback is to tag Spring `strike`.

**Level 6 (choose 1; default by kit, lead's call):** the default is Drag
Line when an active set holds a Hook or Push weapon (sickle, whip; Push
verbs), and Trick Shot otherwise, because the Blades and Launcher kits
carry neither verb.
- **Drag Line** (rewrites Hook and Push): when your verb-sourced Hook or
  Push on a hit would move a hostile into a known armed trap's trigger
  square, its hazard-edge save (conditions 10.2) has **disadvantage**;
  on a failure it enters the square and the trap fires by its own
  trigger, with no check and no Trick. The 3-square sum and once-per-round
  verb movement are unchanged. The same disadvantage applies to any M2
  hazard a verb moves a hostile into, so the rewrite works on trap-light
  floors. **Auto-entry is Later**, beside the Vanguard's Over the Edge
  and pending the same hazard caps (Vanguard design 8.3, 14; B-S9).
  Registry: Brink.
- **Trick Shot** (rewrites Opening): when a trap you sprang or a flask
  you threw damages a hostile, you may add Opening's dice to that
  hostile's damage.
  - **Scope `trapOrFlask`:** it qualifies only on triggers whose scope
    lists it (Unready, Surprised, Hidden, Answer; 6.5); never Beset,
    Caught, the carry or any melee-only trigger. The tier is read before
    Hidden's end rules (conditions 18.7) run on the spring or throw.
  - **One target:** if several damaged hostiles qualify, the player
    picks one at preview; the default is the first in unit order.
  - The dice take the damage type of the trap or flask part they join,
    are halved with it on a successful save and are never doubled. It
    uses the once-per-round allowance.
  - Springs on another unit's turn (Killing Ground) follow the reaction
    default `never` unless the policy is set. It never applies to From
    Nowhere hits (7.7).

**Art: Tripline** (fires on use; decision 12):
- **I (L5):** as Vanish. Also, each hostile **adjacent to you when you
  use it** makes a Reflex save against your ability difficulty or falls
  **Prone** (`trip`, `sharesToppleCount`). Excluded: hostiles more than
  one size larger than you, and those already Prone. A unit that saves
  is immune for the fight. Selective. It sets up Beset, advantage and
  the carry on the next swing. It is an instantaneous effect, not an
  aura, so no new aura trigger kind is needed.
- **II (L9):** every hostile within 2 squares.
- `CampOptionBound`: damage 0; `actionsGranted` 0; at most 24 squares;
  Prone is an M1 minor. Pass.
- **Snuff is withdrawn** (decision 12). False Trail is a Shade-wide
  trigger option (7.11).

**From Nowhere rider: Caught Out** (rewritten in the check round; the
first text restated From Nowhere and did nothing). Until the start of
your next turn, your first Killing Ground costs no Trick (a cost waiver
on `HideLimit`'s list, not a refill). On your next turn, your first hit
on a hostile in your `caughtBy[]` takes Opening at the stealth die (2d6)
(`caughtOut {expiresAtOwnerTurnEndIndex}`, 13.1). That hit is on
`TriggerTierFixed`'s allow-list (6.6 rule 1) and is counted in the nova
and stealth-share reports; it is camp-pool damage (VG-12), at most one
stealth-die hit, so the next turn is no larger than a Vanish-carry turn.
The Killing Ground spring is trap damage under the content reading
(`SpringContentBound`). A nova rerun is owed (10.3). From Nowhere takes
the bonus action, so Jury-Rig's bonus-action modes are not usable on the
capstone turn; the rider uses the reaction instead.
**Rejected:** a free Jury-Rig that replaces an attack (p95 about 90
against the bound of 74).

**`triggerMenu`** (`requires: subclass:trapwright`):
- `spring.unaware`: Surprised or unacted hostiles save with disadvantage
  against your traps and flasks;
- `spring.quiet`: your springs make noise 6, not 10;
- `rearm.reach2`;
- `killingGround.allyMoved`;
- `caught.wedge`: a hostile that fails to force your wedged door is
  Caught.

**effectKinds:** `trap.spring.bonus`, `trap.rearm.bonus`,
`flask.throw.bonus{setupOnly}`, `door.wedge.bonus`;
`trap.spring.reaction`; `opening.trigger.caught`;
`skill.grant:mechanics`, `trapSense.proficiency`; `hide.noLosBreak`,
`carry.set`, `condition.apply:prone.onUse`;
`hazardEdge.disadvantage.verbMoved`;
`opening.deliver.trapOrFlask`; `flask.throw.bonus{dust}`;
`cost.waive.killingGround.once`, `opening.stealthDie.caughtNextTurn`.

**Registry:** Toss against Slinger's Bracers (VG-18 ruling; SH-25);
Dust against the cut flash flask (no Blinded; Caught only); Tripline
against Set, Bolas, Thunderstone and Earthshaker; Drag Line
against Brink; Killing Ground against Set.

**Weaknesses (on the card):** dependent on content, so rooms go dead on
trap-light floors (floor 1 gives Caught, Toss and Dust only); loud; friendly
fire; bosses (Unyielding; no guaranteed boss-room traps); two key
abilities; a contested bonus action; monsters avoid known traps (true
state, enemies 11.2; natives avoiding triggers and trusting areas, Once
Bitten and the mindless blunder are a pending request to the enemies
owner: traps and locks design 6.8, TL-16).

**Later:** Drag Line auto-entry (with Over the Edge, pending hazard
caps), chained springs, monster trap use, Holy Water and Sticky
flasks as Toss fuel.

### 8.3 SWORDSWORN (M2, promoted under the fallback, TL-37; cross-document comparison 2026-11-23, CC-73): "make them miss, then answer"

**Mechanism (agreed, Q3):** a Tricks-paid Sworn Guard ward that counts
as a Parry; Counterstroke after a turned blow from level 3 and (Open
Guard) after any melee miss from level 5; every Counterstroke spends the
strike limit. This changes the stage-1 card's mechanism ("a pooled
reaction strike after a Parry"), not its intent (SH-31).

**Specs into:** one-on-one melee (elites, bosses); the reaction economy;
Answer as the engine; finesse and light blades (rapier Lunge, sabre
Flourish, shortsword + dagger).

**Does not spec into:** stealth, groups, ranged threats, protecting
allies (the Bastion's lane) or goading (the Vanguard's Challenged).

**The Parry gap and its fix.** The Shade has no Dexterity Parry (D8).
Two routes were weighed:
- **Verb borrowing** (weapons 7.3 as changed by VG-19) lets a subclass
  apply the canonical verb of a trained, non-Strained weapon in the
  spare set; a staff in set B would give Parry. It is rejected as the
  primary route: it forces a bulky staff (2 pack size) into every
  duellist's kit; an unpaid borrowed Parry would be a pool-free +2 every
  round; and it reads badly for a rapier fantasy.
- **Sworn Guard** is chosen **(agreed, Q3)**: a **pooled class ward** on
  the approved "pool-backed reaction wards" lean (the Flare precedent).

**Hook (required; SH-8):**
- The `missedBy` trigger kind hooks after weapons 7.5 step 6 / 6a. On a
  miss the pipeline stops at step 6, so the Retort Strike hook at step
  15 is never reached (D7).
- **Order at a miss (lead's call):** step 6, the miss → Graze (7.6) →
  write `answeredBy[]` on the target → step 6a → the `missedBy` hook,
  where legality is re-checked (reaction left, not Staggered, attacker
  in reach, HP above 0, so a Graze that drops you or Staggers you
  removes the offer) → the attacker's pipeline stops.
- With `sameReactionFollowUp`, `onParrySuccess` is defined as "a Parry,
  or a `countsAsParry` ward, was applied and the roll missed". The
  follow-up runs **in the same reaction** when the ward spent your
  reaction (Sworn Guard, a Parrying-property Parry) or carries one
  forward (Held Guard's carried ward extends the Sworn Guard reaction),
  and **spends your reaction** when the ward was a step-0 fill that spent
  none (Blade Dance), so it is then legal only if you still have one.

**Data:** `mechanicIds [swornGuard, counterstroke, openGuard, loneBlade,
heldGuard, crossSteel, turnAside, bladeDance]`.

**Signature: Sworn Guard** (reaction, 1 Trick, with a light or finesse
`group:blade` weapon in either hand; greyed with the 7.14 tooltip while
none is held):
- **I (L3):**
  - **Guard:** when a hostile's melee attack roll targets you, with the
    roll known, +2 AC against that roll. It is a window candidate at the
    new armour 10.4 **step 3a** (the target's own class-pool-paid wards;
    the Arcanist's Flare joins it with `countsAsParry: false`, ARC-5,
    Arcanist design 7.5), ahead of **3b**, the target's own Parrying
    property, and then allies' charges (SH-9), with `countsAsParry: true`.
    Every window rule applies: one reaction AC bonus per roll
    **(settled)**. Pry Deep ignores it. It is unavailable while you are
    Staggered. A miss it causes sets Answer.
  - **Window order is intended (lead's call):** pool-free candidates
    come first, as armour 10.4 orders them, so an ally's Interpose that
    fires closes the window before Sworn Guard is offered. That miss is
    not a turned blow, and at levels 3–4 no Counterstroke follows; from
    level 5 Open Guard covers it ("misses you without a Parry"). The
    Shade saves the Trick.
  - **Counterstroke** (a mode of the Sworn Guard entry): when your Parry
    (Sworn Guard, or a Parrying-property Parry) is applied and the roll
    then misses, make **one main-hand melee weapon attack** against the
    attacker, under the follow-up rule above.
    - **Cost (`costByTrigger`, 7.3 rule 1):**

      | Trigger | Tricks | Strike limit |
      |---|---|---|
      | After Sworn Guard (the ward paid its Trick) | 0 more | 1 |
      | After a Parrying-property Parry | 1 | 1 |
      | Open Guard (II) | 1 | 1 |
      | After a blow turned by Blade Dance (Art) | 0 | 1 |
      | Turn Aside (level 6) | 1 (with Duck's) | 1 |
      | The From Nowhere rider window (once) | 0 | 1 |

    - **Opening** applies if this round's allowance is unspent (policy
      `always`; Answer is met, because `answeredBy[]` is written before
      the hook), at the **open tier only** (7.5), except under the From
      Nowhere rider.
    - **Bounds:** `ignoresHelped` (no off-turn Exploit theft); no
      condition verbs and no own-turn verbs; **no nesting** (never off
      an opportunity attack, a Set, a reaction attack, a boss action or
      a free attack); legality is re-checked at the hook; once per
      reaction.
    - **Riposte signature (weapons 18.4):** one reaction makes one
      attack; the player picks the Riposte or Counterstroke (the VG-18
      echo ruling).
  - **Rejected:** a turned-blow Counterstroke free of the strike limit.
    Sworn Guard is used with the roll known, so every Sworn Guard is a
    turned blow and the trigger is not self-limiting: 3 Tricks would
    mean 3 strikes per rest at level 3.
  - **Level 4 signature pip** (7.9): a second strike pip that only
    Counterstroke may spend.
- **II (L5): Open Guard** (a mode of the Sworn Guard entry).
  Counterstroke may also follow a melee attack roll by a hostile in your
  reach that **misses you without a Parry** (`onMeleeMissAgainstSelf`),
  under the same order at a miss. Cost: 1 Trick + 1 strike limit + your
  reaction. Policy: `whilePoolAtLeastHalf` (7.12). It fires on about
  45–55% of melee rolls, against about 10% for a turned blow, so the
  strike limit binds. It is pooled per use with no standing state, so it
  is not Riposting.
- **III (L7): Held Guard.** Sworn Guard's +2 also applies to that
  hostile's later melee rolls against you **during the current turn**,
  with no further reaction or Trick (`heldGuard {attackerId}`, cleared
  at the `turnEnd` of the unit whose turn it is). Armour 10.4 gains a
  **step-0b "carried ward" candidate** (SH-9): after the step-0 camp
  fills and before step 1; it spends nothing, is exempt from the "no
  reaction left" skip as step 0 is, inherits the Pry Deep, Staggered and
  gear-disadvantage skips, and counts as the one reaction AC bonus for
  the roll. A later miss is an `onParrySuccess` inside the carried
  Sworn Guard reaction; Counterstroke follows only if that reaction has
  not yet made one (once per reaction). **Design-time fallback:** if
  armour's owner does not accept the step-0b candidate, rank III is
  `counterstrokeStop` instead, and `counterstrokeStop` then leaves the
  `triggerMenu` **(lead's call)**.

**Level 5 clause: Lone Blade** (an Opening racket; open tier;
`TriggerFromFive`):
- For you, Beset's ally-hit clause is **replaced** by "the target is the
  **only hostile adjacent to you**". Prone, Staggered and Grappled still
  count.
- A swap, not an addition (decision 13).
- Tooltip: "Lone Blade: you open foes alone, not in a crowd."
- Lever if observed Lone Blade uptime exceeds ally-hit uptime by more
  than 50‰: "no other hostile within 2 squares".

**Level 6 (choose 1):**
- **Cross Steel** (rewrites Fade: a fourth Fade mode beside Hide,
  Disengage and Dash, 7.3 rule 6; bonus action, 1 Trick, `strike`):
  Disengage, and gain a carry limited to this turn, with `targetFilter
  notAdjacentAtTurnStart` (the `adjacentAtTurnStart[]` snapshot, 13.1):
  your first own-turn hit this turn on a hostile you were not adjacent to
  at turn start, to which you apply Opening, takes the stealth tier
  (2d6), and consumes it (7.5). No Stealth roll. Per use it is below a
  Fade-Hide (no advantage), so `StrikeOptionCeiling` passes.
- **Turn Aside** (rewrites Duck: a same-reaction pairing, 7.3 rule 2):
  when you Duck a melee hit from a hostile in your reach, you may also
  Counterstroke in the same reaction, paying both Tricks and 1 strike
  limit. Duck's halving resolves first (DH 20.3 step 3a); the
  Counterstroke resolves after the attacker's weapons 7.5 step 15, with
  legality re-checked there (reaction, Staggered, reach, HP). One
  reaction, no nesting.

**Art: Blade Dance:**
- **I (L5):** an option on the Vanish pool. No Hide. Until the start of
  your next turn, every melee attack roll against you takes +2 AC as a
  **step-0 window fill**: no reaction, no Trick. Pry ignores it; it is
  suppressed while Staggered. It counts as your Parry. A Counterstroke
  after a blow turned by Blade Dance costs no Trick (`bladeDance.active`
  on `HideLimit`'s waiver list); it spends the strike limit and your
  reaction (Q3). One reaction means one such Counterstroke per Blade
  Dance. Moving the II carry to rank I was weighed and not taken: the
  +2 on every melee roll until your next turn is the Art's payoff.
- **II (L9):** also a Vanish carry (without the Hide).
- **Envelope:** AC 17 + 2 = 19 against +9 is about 55% to hit; inside the
  charged row.
- `FlagNotCondition`: it waives a cost and never grants strikes over a
  duration.

**From Nowhere rider** (rewritten in the check round; the first text
saved at most one Trick): until the start of your next turn, your first
Counterstroke costs no Trick and takes Opening at the **stealth die
(2d6)** even though From Nowhere has spent this round's allowance. It
still spends the strike limit and your reaction (Q3). It is on
`TriggerTierFixed`'s allow-list (6.6 rule 1); its damage is camp-pool
damage under VG-12, bounded by the nova bound, and a nova rerun is owed
(10.3).

**`triggerMenu`** (`requires: subclass:swordsworn`):
`counterstrokeStop` (a hit sets the attacker's remaining movement to 0;
it leaves the menu if it becomes rank III under Held Guard's fallback);
`loneBladeMinions` (adjacent minions do not break Lone Blade; uptime
report); `bladeDanceTwoRounds` (envelope rerun).

**Rejected, with one-line reasons:** Press the Duel (the Vanguard's goad
lane; pulls bosses onto an HP-7, light-armour unit); Answering Lunge
(parity); self-steps after a Counterstroke (only Kiter's Step steps
off-turn); a Counterstroke push (Binding Parry echo); ranged guard
(Deflect); ally guard (the Bastion's lane); longsword training (access,
not depth); free-riposte states (Riposting is Later); riposte dice
(parity); the PF2e free-hand rule (it kills Nick); a riposte-only
reaction (a forbidden grant).

**effectKinds:** `reactionAc.self.melee{+2, countsAsParry}`;
`reactionAttack.onParrySuccess`, `reactionAttack.onMeleeMissAgainstSelf`;
`reactionAc.carry.sameAttackerTurn`;
`opening.trigger.replace{allyHit → onlyAdjacentHostile}`;
`action.disengage.bonus`, `carry.set.notAdjacentAtTurnStart`;
`reactionAttack.onDuck`; `reactionAc.windowFill.self.melee`,
`cost.waive.counterstroke.bladeDance`;
`cost.waive.counterstroke.untilTurnStart`,
`opening.stealthDie.counterstroke.once`.

**Registry:** Sworn Guard against Parry and the Parrying property;
Counterstroke against the Riposte signature and Riposting; Blade Dance
against Stand Fast and Step In II; Held Guard against Binding Parry.

**AI:** Counterstroke is priced in the enemies 11.1 opportunity-cost
term as known state, never by class (VG-16; SH-29). A deterrence report
is owed.

**Weaknesses (on the card):** groups; archers and casters; Pry Deep and
Staggered riders; a thin level 3 (1 strike per rest, shared with
Fade-Hide), eased at level 4 by the signature pip (7.9); standard HP in
light armour while standing in front; with a bow out, no Sworn Guard
until a set swap.

**Staging:** M2. It was M3, the first Shade unlock (stage-1 order),
promoted to M2 if the Trapwright fell back; the fallback applies
(2026-11-21; TL-37, 14), so the Swordsworn ships in M2. Its SH-8 engine
work lands in M2 (cross-document comparison 2026-11-23, CC-73).

## 9. New mechanics considered

The client invited new mechanics with their cost, the rules they touch,
their bounds and a recommendation. Cost: S small, M medium.
**(lead's call on every row; the band row agreed, Q1)**

| # | Mechanic | Cost | Touches | Bounds | Decision |
|---|---|---|---|---|---|
| N-1 | **Tiered Opening band** (stealth / open, from level 1) and the trigger-1 split | S | classes 8.4 | `OpeningCeiling`, `TriggerTierFixed` | **Adopted (agreed, Q1)** |
| N-2 | **Strike limit** (a shared tagged counter on the pool record) | S | classes 3.4, 7.5; Vanguard 7.9 | `HideLimit`, `StrikeOptionCeiling` | **Adopted** (SH-4) |
| N-3 | `OpeningCeiling`, `StrikeOptionCeiling`, `TriggerFromFive`, `TriggerTierFixed`, `CampOptionBound` | S | classes 12.2; VG-10/11/12 | static predicates | **Adopted** |
| NM-1 | **Trap rulings package:** known armed traps are hazards for forced movement (conditions 10, 11.3); a clarified hand-trigger row (action, adjacent, Mechanics against `disarmDifficulty`, failure noise 4); **Trigger Shot** (replaces an attack: thrown or launcher roll against AC 10 + 2 × tier at a trigger square; a hit springs it, a miss makes noise 4; no Opening or Exploit); re-arm at 500‰ with no hard part | M | conditions 10, 11.3, 11.6; ability 7.2; enemies 11.1; DH-18 unchanged | one spring per trap per round; the 3-square forced-movement sum | **Required with the Trapwright** in whatever wave it ships. **The hazard ruling ships in M2 regardless** (14); the hand-trigger (kept an action; handles and Pole it added), Trigger Shot (mechanisms included) and the 500‰ re-arm ship universally in M2 (traps and locks design 6.7; TL-13). "Monsters know their floor's traps" is already true (enemies 11.2, 11.3 invariant 7) and is not a change |
| NM-2 | **`missedBy` trigger kind** + `sameReactionFollowUp`; `onParrySuccess` defined | S–M | weapons 7.5 step 6/6a, 8, 18.4; ability 7.2, 7.3, 14.3; VG-18/20 | one reaction; no nesting; Staggered stops it | **Engine in M2; ships with the Swordsworn** |
| NM-3 | **False Trail** (a class writes `lastKnown`) | S | enemies 11.2 | only groups from whose every member with line of sight you became Hidden; Search, Revealed and the 3-square rule untouched; information, not a targeting override (12) | **Adopted in M2** as a Vanish `triggerMenu` entry |
| NM-4 | **Lure** (exploration: noise radius 4 at a seen square within 6; once per group per floor) | S | enemies 12.1–12.3, 15.5; ability 7.2 | exploration only; no Wary; `luredThisFloor` | **Adopted in M2, universal**, if the enemies owner accepts; report elite-room notables taken without a fight. Read by segment 8 (EV-14, read; events design 4.8, `2026-11-22-events-design.md`): Curiosity noise is never a Lure and never sets `luredThisFloor`; no Shade-lit Lure variant |
| NM-5 | **Draw Out** | S | classes 8.4; enemies 13.3 (guile, read only) | open tier; failure costs the Trick; bosses +2 guile | **Adopted in M2** (L7 talent; `precision` offer from L5) |
| NM-6 | Pinch (combat Steal of belt items) | M | enemies 15.5 (**agreed** S-5 row), items 7.1 | Hidden or Surprised or ally-Grappled; never bosses | **Later.** Low payoff for an agreed-row change; a future client item |
| NM-7 | **Shrugged Off** (per-source immunity to repeatable Poisoned after a save) | S | conditions 7.3, 18.5 | narrows only | **Adopted in M2** |
| NM-8 | Lie in Wait (reaction strike from Hidden) | S | weapons 8 (Overwatch timing); ability 2 non-goal framing | `strike` | **Later:** it slows play, the AI cannot price it, and it adds stealth share |
| NM-9 | Gloom squares (static concealment terrain) | S–M | conditions 11.1; generation | Hide in sight of observers more than 2 squares away | **Later**, paired with the Wise to You trial |
| NM-10 | **Wise to You** (−2 Stealth per re-Hide against the same group, maximum −4) | S | enemies 11.2 | narrows only; symmetric | **Ships in M2 with `wiseStep` 0** (the weapons 6.5 precedent); a lever |
| NM-11 | Vault (Acrobatics jumps) | S | stat 7.1 | tagged crossings only | **Later** ribbon |
| — | Seam/Coup combo flag | M | replaces Answer | — | **Rejected** (112–114% at L9–10; changes an approved trigger) |
| — | In-combat auto-crit on Surprised | — | — | — | **Rejected** (115–121%; nova 1.9) |
| — | Flanking, facing, vision cones, a light model, smoke clouds (rejection untouched by the M3 Remnants; Haze, smoke that blocks sight, is Later with NM-9: room terrain design 6.6, TR-27), placed snares, enemy swaps, Shade marks, poison stacking, flashbacks, Withdraw moves, a generic Ready | — | non-goals or forbidden grants | — | **Rejected** |

### 9.1 Adopted mechanics in full

**(lead's call; texts within these rules are the builders' until the
naming pass)**

- **Strike limit.** "A per-rest counter on one named pool's record,
  spent by records tagged with its one declared tag, whether or not they
  spend from the pool, is a frequency limit and counts 0; it is not a
  named pool." The tag is declared on the pool record. Validator:
  `HideLimit` (static: every record that grants Hide, a carry or a weapon
  attack from `pool.tricks` is tagged `strike` or on the named waiver
  list, 7.9).
- **Draw Out.** "Bonus action, 1 Trick. Choose a hostile within your
  melee reach. Roll Acrobatics against its guile (10 + Wisdom +
  proficiency + role adjustment; brutes, minions and mindless −2, bosses
  +2). On a success, until the end of your turn it counts as Beset for
  your melee attacks. On a failure the Trick is spent." It gives no roll
  mode, so it is not the cut Feint.
- **False Trail.** "When your Vanish leaves you Hidden from every member
  of a group that has line of sight to you, you may write
  `lastKnown(square)` into that group's shared awareness record for you,
  the square unoccupied, non-hazard, seen by you and within 3 squares of
  you." If any such member can still see you, that group's record is
  unchanged. Pursuit, Search and area aiming follow the false square.
  Monster areas follow invariant 7, so they never knowingly hit their
  own allies. A ghost marker shows on the party side.
  - **Pending request (exploration and information design 4.2, 5;
    XI-27):** the ghost marker uses a hollow double-footprint ring, never
    the Afterimage shape. Not yet ruled by the Shade owner and art; if
    refused, the Afterimage's base becomes a square paper tag with a
    punched hole instead.
  - **Not a targeting override (lead's call; classes 6.3):** False Trail
    writes information into the awareness record that the AI reads under
    enemies 11.2; the AI still scores freely on what it knows. It is an
    enumerated `ForbiddenClassGrant` exemption (`awareness.lastKnown.write`)
    and is recorded under SH-20 and section 12.
- **Lure.** "Exploration action (1 tick): choose a square you can see
  within 6; it emits noise radius 4 (enemies 12.3 rules). A group with no
  member in line of sight to you becomes Alerted toward it with no roll;
  a group that can see you makes its noticing roll. A group can be lured
  once per floor." Alerted never becomes Wary (enemies 12.1), so this is
  the approved "luring with noise" play given a button.
- **Shrugged Off.** "If a target succeeds on its save against a
  repeatable Poisoned source (an at-will, a turn allowance, a dice
  trade, or a `repeatable` bite), it is immune to that source unit's
  repeatable Poisoned for the fight." Held as a source-held set.
- **Wise to You.** `wise[partyUnit]` per group per fight rises by 1 each
  time that unit's Hidden against a group member ends, up to `wiseMax`
  2. Later Hide or Vanish rolls against that group take −`wiseStep` ×
  `wise`, applied per group to the one Stealth roll (each group
  compares the roll with its own modifier; no extra draw). It ships with
  `wiseStep` 0.
- **Trap rulings (NM-1)** and **`missedBy` (NM-2):** as in the table and
  8.2, 8.3.

## 10. Numbers

*(scratch; illustrative; not simulator evidence)*

**Model.** Scratch Monte Carlo scripts (the numbers check's `shade_lev`
model with the tier split, flat parts, the Vanish carry flag and a
configurable boss band). The weapons-only baseline reproduces weapons
6.4: 78 / 75 / 82%. **Profiles:** worst legal is Hide success 70%, a
Hide always feasible, ambush 35% solo / 20% party, and every leftover
Trick on Duck; typical and low use smaller inputs. **VG-12 reading:**
camp damage (Vanish, Arts, From Nowhere) is out of the parity number
and bounded by the nova bound and `CampOptionBound`. Model noise is
about ±0.5.

### 10.1 Parity (weapons 6.4, any build ≤ 110%)

Final band (stealth 1d6 / open +2; then 2d6 / 1d6), Tricks 2 / 3 / 4 at
1 / 3 / 9, strike limit 1 / 2, blades. Cells are % of the greatsword
reference, solo / party (one figure where they agree).

**Run before the check round.** Three revisions owe a rerun of named
cells and change no other: the level-4 signature pip (the Cutthroat and
Swordsworn L4 cells, 97–98 today, so there is room); Tricks 4 at level 8
(the L8 cells; L9–10 are unchanged, because Tricks were already 4
there); and Dust (the Trapwright L3–4 cells, through Caught uptime).
Vanish 3 at level 2 is camp damage and changes no parity cell (VG-12).

| L | Worst legal | Typical | Low |
|---|---|---|---|
| 1 | 107.5 | 103.3 | 99.3 |
| 2 | 107.1 | 103.8 | 99.2 |
| 3 | 108.4 / 107.0 | 104.3 / 103.0 | 100.3 / 98.9 |
| 4 | 97.0 / 95.3 | 93.0 / 91.1 | 89.8 / 87.8 |
| 5 | 103 / 103 | 99 / 99 | 94 / 94 |
| 6 | 102 / 103 | 98 / 99 | 94 / 95 |
| 7 | 102 / 103 | 98 / 99 | 94 / 94 |
| 8 | 93 / 93 | **89 / 89** | 85 / 85 |
| 9 | 109 / 108 | 103 / 105 | 99 / 101 |
| 10 | 109 / 109 | 103 / 104 | 99 / 100 |

**Subclasses, worst legal, levels 3–10:**

| Subclass | L3 | L4 | L5 | L6 | L7 | L8 | L9 | L10 |
|---|---|---|---|---|---|---|---|---|
| Cutthroat (Surprised die +1 at L3–4, +1d4 from L5) | 109.3 / 107.4 | 98.0 / 95.8 | 103.9 / 103.9 | ≈104 | ≈104 | ≈94 | 109.5 / 109.1 | 109.0 / 109.2 |
| Trapwright (content reading; Caught 25% from L3) | 109.9 / 107.3 | 97.1 / 95.3 | 104 / 104 | 103 / 103 | 103 / 104 | 94 / 93 | 109 / 109 | 109 / 109 |
| Swordsworn (Lone Blade swap from L5) | 109.1 / 103.0 | 97.4 / 91.2 | 105 / 101 | 104 / 101 | 104 / 101 | 94 / 91 | 110 / 107 | 110 / 107 |

**Comparisons (worst legal):**

| Band or variant | Result |
|---|---|
| The numbers check's 1d3 at L1–4 + lever A (rejected, Q1) | L1 105, L3 106 / 105, L10 109 |
| Open +1 at L1–4 | L1 100, L4 typical 87 (floor fail) |
| Open 1d4 at L3–4 | **114** at L3 (fail) |
| Uniform 1d6 / 1d4 at L1–4 | **112.5** at L1 (fail) |
| Stage-1 band (rejected, Q1) | 121–174 |
| 2d6 on every trigger | 117–122 |
| No strike limit | 111–118 |
| In-combat Throat-Cut | 115–121 |
| Trapwright, strict reading | 117–120 |
| Seam model | 112–114 at L9–10 |

**Verdict:**
- Every cell is ≤ 110% under VG-12 and the strike limit. The 90% floor
  (read at typical) holds except at level 8 (89%).
- The level-8 trough is structural: the reference gains Strength at 8,
  and Exploit steps at 9. The class-side step (Tricks 4 at level 8,
  5.5) is applied and its rerun owed; if level 8 still reads under 90%
  typical, the remainder goes to the weapons owner.
- The Cutthroat and Swordsworn sit within a point of the cap at levels
  9–10.
- **The client was told plainly** that, like the Vanguard, the Shade
  passes only under VG-12 plus the strike limit **(agreed, Q1)**. The
  all-in reading (camp damage included) was 111–118% and was not rerun.
- **The observed stealth share is the single binding number owed by
  the simulator.** Under the old trigger set (with "not acted" in the
  stealth tier) it was about 440‰. Under the split it measures 177 /
  143 / 243 / 244‰ worst-legal at levels 1 / 3 / 5 / 10. The level-3 dip
  is unexplained in the scratch model (6.4); the rerun counts every
  stealth source in 15, subclass strikes included, and reports level 3
  per subclass.

**Corrected claims from the drafts:** the core draft's worst-legal 105 /
103 / 109 was really about 111 / 107 / 112; its level-1 camp share of
42–44% is really about 33–37% (10.5).

### 10.2 Launchers (raw; subtract about 6 for the longbow)

| Kit | Typical L1 / 5 / 10 | Worst |
|---|---|---|
| Longbow | 93 / 89 / 97 (about 87 / 83 / 91 adjusted) | 95 / 92 / 101 |
| Shortbow | 63 / 76 / 83 | — |

- The best launcher is ≥ 75%, so the Shade **does not open the weapons
  6.5 Precision gate**.
- The sling (about 36–42%), the light crossbow at levels 5–10 (Loading
  against Extra Attack) and the heavy crossbow (Strained at Str 0) fail
  the 60% family floor for any class. They go to the weapons owner.

### 10.3 Nova bound

From Nowhere p95 ratio 1.57; with Hidden 1.62; with Ambuscade ≤ 1.76.
All under the 2.0 bound. `lingering` is removed. **Owed (check round):**
a rerun with the 7.7 interactions and the two rewritten riders (Caught
Out's stealth-die hit next turn; the Swordsworn's stealth-die
Counterstroke), each with and without Arts (13.3). Both riders add at
most one stealth-die hit, below the gap between 1.76 and 2.0 at the
scratch p95.

### 10.4 Sustain and spread

- **`Floor1BossSustain`** (DH 21.1 step 6; read at level 3, VG-24): Con
  +1: 24 + 33 + 5 = **62** against a need of 48, pass; Con −1: **51**
  against 48, pass.
- **`Floor1ClassSpread` proxy.** Level-3 solo floor-1 boss death, no
  escort / escort. The comparator is **the Vanguard's mean over its
  picks** (sword and board 3.1 / 12.4, greatsword 1.7 / 6.5, so a mean of
  2.4 / 9.45).

  | Shade package | Death % | Gap to the mean | Result |
  |---|---|---|---|
  | Old (Tricks 2, Vanish 1 left, 1d4) | 11.9 / 28.7 | 9.5 / 19.3 | fail |
  | Numbers recommendation (1d3, Tricks 3) | 6.8 / 22.1 | 4.4 / 12.6 | fail |
  | **Final** (Tricks 3 at L3 + Vanish 3, about 2 left at the boss) | **6.3 / 18.6** | 3.9 / **9.2** | pass, thin |
  | Final, Swordsworn | 5.0 / 16.2 | — | — |
  | Final, Cutthroat and Trapwright | close to the base | — | — |

- **The light-armour lever does not exist** (D13).
- **Remaining levers:** (1) Tainted Blade's Poisoned at the boss (not yet
  modelled; would help); (2) Duck policy tuning; (3) Vanguard-side data,
  only with the Vanguard owner (classes 15 allows it).
- **Level-2 boss arrivals:** 38.0 / 58.2 (with Vanish 2 at level 2)
  against the Vanguard's 13.5 / 30.9 and the reference hero's 51 / 71.
  The same with the 1d4 band (36.9 / 57.2), so the band choice did not
  cause it. It is not left as a report **(lead's call, check round)**:
  - **Vanish 3 moves to level 2** (5.5, 7.5): camp-pool, outside parity
    under VG-12, and it raises the level-2 camp share that SH-28 has to
    excuse. A rerun of the level-2 arrival cells is owed.
  - **A pre-committed request to the floor-pacing owner** (18; it reaches
    the client only if refused, 19.2):
    the floor-1 boss door needs level 3, or the XP that guarantees it.
    **Closed: answered by the Antechamber Catch-up** (run structure
    design 9.4; RT-8), which guarantees level 3 before the floor-1 boss
    (cross-document comparison 2026-11-23, CC-72).
  - The VG-24 reading still takes the boss check at level 3; the
    simulator owes the rate of level-2 boss arrivals, kept only as a
    check that the catch-up fires (CC-72).
- **The proxy is boss death only.** The gate is the full-floor clear
  rate, which the simulator owes.
- **Signature share** in solo boss rounds 2 and later: 54–67% (target
  about half). Opening fires in 55–66% of ordinary solo rounds 3 and
  later, and in 73–89% of boss rounds. The stage-1 risk is retired. The
  tier split changes the dice, not the triggers, so these rates stand.

### 10.5 Camp share

Offer units; Catch Breath at half value (the measure, VG-13).

| L | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|---|---|---|---|---|---|---|---|---|---|---|
| Half | 37% | 35% | 33% | 32% | 45% | 44% | 44% | 44% | 52% | 51% |
| Full / excluded | 32 / 42 | 30 / 42 | 29 / 38 | 28 / 38 | 41 / 49 | 41 / 49 | 40 / 49 | 40 / 50 | 45 / 62 | 44 / 62 |

- Levels 5–10 are in band.
- Levels 1–4 miss (32–37%). This needs the accepted martial level-1 miss
  (VG-13) extended to levels 1–4 (SH-28, told plainly to the client and
  confirmed by the client on 2026-11-18, 19.1). Vanish 3 at level 2 raises level 2 (levels 3–4 already had it);
  the figures above were run with it from level 3, and the level-2 cell
  is rerun.
- **60% rule:** the earlier figures (level 1 = 123‰ of level 10;
  increments at 3, 5 and 9 = 896‰ of growth) pass. A rerun in the offer
  value unit is owed.

### 10.6 Cap, reading and the 60% rule

3 of 8 limited abilities; 2 of 3 named martial pools, plus the capstone;
reading 3 at every level, with room for 1 offer row;
`TricksPickerSize` 6 of 6 (entries, not modes, 7.3 rule 6);
`OpeningTriggersAdded` ≤ 3.

### 10.7 Levers, in order, if the simulator misses

**(lead's call)**

| Problem | Levers, in order | Never |
|---|---|---|
| Over 110% at levels 1–4 | 1. Stealth die 1d4 at levels 1–4 (the fallback band). 2. Caught to level 5 (Trapwright); cut Dust. 3. The Surprised die to +1 at every level | A cut to Answer (the solo boss engine) |
| Headroom at levels 1–3 in the rerun | Spend the strike pip only on a successful Hide (7.4, held) | — |
| Level-2 boss arrivals stay high after Vanish 3 at 2 | The floor-pacing request (18), closed: answered by the Antechamber Catch-up (run structure design 9.4; RT-8); the arrival rate is kept only as a check that the catch-up fires (cross-document comparison 2026-11-23, CC-72) | A level-2 parity step |
| Over 110% at levels 5–10 | 1. Narrow Beset's ally-hit clause to flank-shaped (about −2). 2. Drop the No Witnesses II carry-only option. 3. Opening on main-hand or single-weapon hits only (about −3; costs boss survival) | A forbidden grant |
| Under 90% (typical) | Open die 1d8 at levels 5–10 | — |
| Cutthroat fights avoided > baseline + 1 per floor | Remove the second-kill route | — |
| Trapwright over the content bound | `springLimit` (1, then 2); then Spring tagged `strike` | — |
| Lone Blade uptime > ally-hit uptime + 50‰ | "No other hostile within 2 squares" | — |

The first lever for levels 1–4 changes an agreed value (the stealth die
at rank I), so it **comes back to the client** before it is applied
**(lead's call)**. The others are data inside the agreed shape.

## 11. No M1 slice; the M2 cut

**(lead's call)** The Shade is an M2 class. **M1 needs nothing** from
this document: no M1 rule is written for the Shade, and none of the
engine work below is on the M1 path.

**The M2 Shade, levels 1–10:**
- the whole core: chassis, Opening (tiers, triggers, glints), Fade,
  Duck, Catch Breath, Vanish, From Nowhere, talents, offers, lanes and
  `triggerMenu`;
- the strike limit, the validators (13.2) and the stealth-share report;
- Draw Out, False Trail, Shrugged Off, Lure (if the enemies owner accepts
  it), and Wise to You switched off;
- the NM-1 hazard ruling (known armed traps are hazards for forced
  movement), whatever the Trapwright's wave; on floor 1, pushes stop
  before trigger squares (traps and locks design 6.19; TL-19);
- the NM-2 engine (`missedBy`, `sameReactionFollowUp`);
- **two subclasses** (classes 13, M2 row): the Cutthroat, and
  **either** the Trapwright (if DEP-S1 lands in M2) **or** the Swordsworn
  (the expected default) (14). Resolved: the Swordsworn (TL-37).

**Conditions the Shade touches:** applies Poisoned (Tainted Blade),
Frightened (Make an Example), Prone (Tripline; Drag Line through
traps); ends Poisoned, Bleeding, Prone (Clear Head), Grappled and
Restrained (`vanishBreakFree`); reads Hidden, Surprised, Prone,
Staggered, Grappled, Restrained, Blinded, Teetering, Poisoned,
Bleeding, Challenged and Frightened (triggers). No new condition.

## 12. Binding requirements and how they are met

| Requirement (source) | How met | Status |
|---|---|---|
| Precision feature with parity (weapons 6.4 **agreed**; CL-8) | Opening, tiered band; launchers stated; Answer and reactions modelled; solo and party; ≤ 110% under VG-12 and the strike limit (10.1) | Met **(agreed, Q1)**; evidence owed: the stealth share |
| Weapons 6.5 Precision gate | Longbow ≥ 75% (10.2) | Not opened |
| Opening and Exploit may both fire (classes 8.4) | Unchanged; both included in every figure | Met |
| DEP-1 Arcana reader (consumables 13.8) | On the skill list; `delve` lane; camp-spend competition shown on the card | Met |
| Bonus-action Hide hook (ability 7.2) | Fade (pooled, strike-limited); Vanish | Met |
| Perception for Tactician | On the list and a default; mastery in `delve`; never copied | Met |
| Medicine on solo floor 1 (DH 8) | On the list and a default; `Floor1BossSustain` passes at both Con values | Met |
| One `restDefensiveTool`; 2 potions (classes 8.2; DH 12.4) | Catch Breath, half Recovery, 1 / 2 per rest, `chassisOnly`; both kits | Met |
| Rest and camp pools; 40–60% camp (ability 6.3) | Vanish 3 from L2; Arts; From Nowhere | Levels 5–10 in band; levels 1–4 miss, accepted under SH-28 (client told plainly; confirmed 2026-11-18, 19.1) |
| One camp spend per turn (ability 6.4) | Vanish, Arts, From Nowhere and scroll reads share it | Met |
| Lean, never require (classes 6) | Every Shade lean has a universal baseline (Hide, Steal, the trap hand-trigger, Lure); doors are not a recorded lean, so Wedge needs none | Met |
| Forbidden grants (classes 6.3) | No speed change (Dash is the universal action); no teleport (Vanish is a Hide); no second reaction; no refills (cost waivers are not refills, VG-11); one camp spend; no new condition. **False Trail** writes the awareness record (information the AI reads under enemies 11.2), not an AI targeting override: the AI still scores freely on what it knows; an enumerated `ForbiddenClassGrant` exemption (SH-20) **(lead's call)** | Met |
| Cap of eight, reading ≤ 4 (classes 7.5, 7.6) | 3 / 8; reading 3 | Met |
| Subclass at 3, verb change at 6, capstone at 9 (classes 7.4) | All three subclasses | Met |
| Capstone acceptance criterion (classes 14) | From Nowhere changes the primary lean; nova 1.57–1.76 | Met |
| Talent menus of 3 with an upgrade; a level-7 trigger option (classes 14) | 7.6 | Met |
| `Floor1ClassSpread` (classes 5.6) | Proxy passes thin at L3 under the correct comparator; L2 arrivals addressed by Vanish 3 at L2 and the floor-pacing request (10.4), now answered by the Antechamber Catch-up (run structure design 9.4; RT-8; cross-document comparison 2026-11-23, CC-72) | Met at L3 (proxy); simulator owed |
| Signature share in solo boss rounds (classes 12.3) | 54–67% | Met |
| Reliance reports (classes 6.6, 12.3, **agreed** Q3) | Stealth and own-tempo reliance, with and without the Shade (hero and recruit), the DEP-1 Arcana recruit-rule waiver printed (classes 7.8) (13.3) | Reports owed |
| Dip guards (classes 7.7) | `scalesBy: classLevel` on the band, the strike limit (the signature pip included) and the pool steps; Catch Breath `chassisOnly`; From Nowhere `minClassLevel 9` | Met; test in 12.1 |
| No automatic surprise of bosses or elites (settled) | No feature applies Surprised; unaware excludes Alerted, Searching and Wary; elites and bosses excluded from Throat-Cut and Ambuscade | Met |
| `LeanSpread` (classes 6.2) | Tools recorded (SH-27); morale and reactions rows at 2 classes | Met |
| Registry pass (classes 14) | Named rows in 7.13 and each subclass | Met; rulings owed (18) |
| `effectKinds`, subject and trigger on every feature (classes 14) | 7.13 and section 8 | Met |
| Ability 8.4 no ground zones (**agreed**) | The Trapwright operates the floor's own traps; Tripline fires on use | Met |

### 12.1 Test additions

**(lead's call)** Named tests the builders write with the first Shade
data, beside the stage-1 and Vanguard tests:

- **Dip test:** a Shade-1 dip gives strike limit 1, Opening I and Tricks
  without Catch Breath.
- **Tier precedence:** a hit meeting Answer and Hidden deals the stealth
  die once, never both.
- **Carry consumption:** a carry is consumed only by an own-turn hit to
  which Opening is applied or traded and that meets no other stealth
  trigger; a declined hit, a reaction hit (a Counterstroke included,
  which takes the open tier), a Trick Shot, a From Nowhere hit and a hit
  that is already Hidden or Surprised keep it; it expires at the end of
  the next turn (second with Long Shadow, Vanish and Art carries only);
  a second Vanish refreshes, never stacks; a carry set out of combat
  expires at the end of the first turn after combat starts, or at the
  next rest.
- **`holdCarryOnOverkill`:** with a carry live and the filter on, a hit
  on a minion or a hit whose weapon damage already drops the target does
  not take Opening and keeps the carry.
- **Ambuscade II:** the two covered targets bind to the first two
  distinct hostiles to which the carry is applied.
- **Spare Trick:** with the spare unspent, a strike option needs
  `remaining − 1 ≥ cost`; a non-strike option spends the spare first.
- **Signature pip:** at level 4 the second pip pays No Witnesses or
  Counterstroke and never Fade-Hide or Cross Steel.
- **Strike limit:** with the limit spent, Fade offers only Disengage and
  Dash; No Witnesses offers only the II carry-only mode; Counterstroke
  and Cross Steel are greyed; Throat-Cut does not fire; Duck, Catch
  Breath, Sworn Guard's ward and Jury-Rig stay live.
- **Duck placement:** Duck is offered on the DH 20.3 step-3 total, not
  when that total is 0; it halves to max(1, FloorDiv(total, 2)) at step
  3a; concentration reads the post-3a value.
- **Window order:** an ally's Interpose that fires closes the window
  before Sworn Guard (step 3a); Sworn Guard comes before the target's
  Parrying property (3b); Held Guard's carried ward (0b) spends nothing
  and counts as the one reaction AC bonus.
- **Order at a miss:** a Graze that drops or Staggers the Shade removes
  the Counterstroke offer; `answeredBy[]` is written before the hook.
- **Answer persistence:** being hit never clears `answeredBy[]`; the
  owner's turn end, combat end and the owner being Downed do (the same
  for `caughtBy[]`).
- **Counterstroke nesting:** no Counterstroke off an opportunity attack,
  a Set, a reaction attack, a boss action or a free attack; one per
  reaction; legality re-checked at the hook.
- **Unaware set:** the Cutthroat's unaware-at-declaration branch and
  Throat-Cut fire against Dormant, Stationary and Patrolling units only;
  never against Alerted, Searching or Wary, elites or bosses.
- **Throat-Cut containment:** a Throat-Cut hit that would not be a
  silent kill, or whose step-3 total is under half the target's current
  HP, deals normal damage and spends no strike pip; every reading is
  taken at declaration.
- **Quiet Work:** a silent kill of a group's only elite may set
  `broken`; a silent kill in a group with another living elite may not.
- **False Trail:** a group with one member that still sees you keeps
  its record unchanged.
- **Trap jams:** a failed Spring jams that trap for the fight (until
  floor end out of combat); a re-armed trap fires at 500‰ with no hard
  part, re-armed twice still 500‰; a hard part fires at most once per
  fight; Killing Ground on a trigger square resolves the trap once.

## 13. Data, validation and simulator

### 13.1 Record fields added

**(lead's call; illustrative shape)** Beside the classes 12.1 record and
the Vanguard design's 13.1 fields:

- on trigger records: `tier` (`stealth` | `open`), `minClassLevel`,
  `scope[]` (melee, thrown, launcher, trapOrFlask; authoritative, 6.4),
  `requires`, `barredFor`;
- on option records: `tags[]` gains `strike` and `device`;
  `costByTrigger[{trigger, tricks, strike}]` and `costWaivers[]` (7.3
  rule 1); `modeOf` (the entry a mode belongs to, 7.3 rule 6);
  `countsAsParry` on ward records;
- on pool records: `strikeCounter {tag, perRest[] by level,
  signaturePip {minClassLevel, maxClassLevel, spendableBy[]}}` (the strike
  limit declared on `pool.tricks`, 7.9);
- on trap records (DEP-S1): `mechanismSquare` beside the 8.2 fields;
- on the class: `rankSchedule` (5.1), `openingBand[{rank, minClassLevel,
  stealth, open}]`;
- on subclasses: `offerLaneAdjust`, `artId`, `fromNowhereRiderId`,
  `wave` and `fallbackWave` (14; SH-30);
- trigger kinds `missedBy`, `onMeleeMissAgainstSelf`, `onEvent:kill`,
  `onOpeningApplied{outOfCombat}`; the `sameReactionFollowUp` flag
  (SH-8);
- in the tuning file: the `OpeningCeiling` and `StrikeOptionCeiling`
  tables (`ref`, `weaponsOnlyBest`, `hiddenUplift`, `hitProbAtLeastOne`,
  `critFactor`, `stealthShare`, `openShare`, `hideSuccess`,
  `advantageUplift`, `exploitValue`), the `HideLimit` waiver list,
  `wiseStep` (0) and `wiseMax` (2), `bigHit` (250‰),
  `SpringContentBound` inputs (`maxTrapsPerFightRoom`, `maxTrapAvg`,
  `levelForFloor`, `roundsPerFloor`), and the `TrapwrightHasMechanism`
  seed count and thresholds.

**Unit state (lead's call), with each reset event:**

| Field | Holds | Reset or cleared |
|---|---|---|
| `strikeUsesThisRest` (with `signaturePipUsed`) | `strike` uses spent against the strike limit | Rest |
| `spareReserve` | 1 while the Spare Trick is unspent (7.3) | Rest; the first non-strike spend |
| `answeredBy[]` | Units that missed you with an attack roll or whose hit you Ducked (written before the `missedBy` hook) | The owner's `turnEnd`; combat end; owner Downed; never by being hit |
| `carries[]` | `{sourceOptionId, expiresAtOwnerTurnEndIndex, targetFilter, usesLeft, coveredTargets[], surprisedDie, freeNoWitnesses}` from Vanish, the Arts, No Witnesses II, Cross Steel, Blade Dance II (7.5) | Consumed as 7.5; the owner's `turnEnd` at that index (second with Long Shadow, Vanish and Art carries only); out of combat, the end of the first turn after combat start, or the next rest; owner Downed; combat end |
| `caughtBy[]` | Hostiles Caught by your springs, flasks, Dust or Tripline | The owner's `turnEnd`; combat end; owner Downed |
| `caughtOut {expiresAtOwnerTurnEndIndex}` | The Caught Out stealth-die hit, and its free Killing Ground until your next `turnStart` | Used; the index; combat end; owner Downed |
| `drawnOut {targetId}` | Draw Out's Beset target | The owner's `turnEnd` |
| `adjacentAtTurnStart[]` | Hostiles adjacent to you at your turn start (Cross Steel) | Rewritten at each owner `turnStart` |
| `shruggedOff[]` | Units immune to your repeatable Poisoned (Shrugged Off) | Combat end |
| `taintedLanded[]` | Elites and bosses your Tainted Blade has landed on this fight | Combat end |
| `savedAgainst[{optionId, unitId}]` | Fight immunities after a save (Make an Example, Tripline) | Combat end |
| `heldGuard {attackerId}` | Held Guard's carried ward | The `turnEnd` of the unit whose turn it is |
| `bladeDance {expiresAt}` | Blade Dance's fill and its Trick waiver | The owner's next `turnStart` |
| `fromNowhereCounter {expiresAt}` | The Swordsworn rider's one Counterstroke | Used; the owner's next `turnStart` |
| `wedgedDoorId` | The one wedged door | Forced; opened from your side; next camp; a new wedge |
| Per unit (all units): `hasStartedTurnThisFight`; `meleeWeaponHitBy[{unitId, atTurnIndex}]` | Unready; Beset's ally-hit clause (read by turn-index comparison against your previous `turnEnd`) | Combat end / entries older than your previous `turnEnd` |
| On the trap: `jammedUntil` (fight or floor), `rearmedBy`, `disarmedBy`, `juryRigRearmedThisFloor`, `hardPartUsedThisFight` | Jury-Rig state | Combat end (fight jams, hard part) / floor end |
| On the group: `wise[partyUnit]`, `luredThisFloor`, `silentKillsBy[partyUnit]` | Wise to You; Lure; Quiet Work's second route | Combat end / floor end / floor end |

The Tricks and Vanish uses spent are read from the pools' counts; the
strike limit needs its own counter because it is shared across records
(3.2 "nothing derived is stored" is kept: the counter is state, not a
derivation).

### 13.2 Content validation

The build fails when any of these fail (VG-11): `OpeningCeiling`,
`StrikeOptionCeiling`, `TriggerTierFixed`, `TriggerFromFive` (with
Caught as its one listed exception), `OpeningTriggersAdded`,
`HideLimit` (static, with its waiver list), `CampOptionBound`,
`SpringContentBound` (static, over the generator's parameters) and
`TrapwrightHasMechanism` (a property test over fixed seeds, part of the
build's test run) while the Trapwright ships, `TricksPickerSize`
(entries, not modes); the Vanguard checks as written (`RankAddsNoDice`
with the Shade allow-list, `RankKeepsBounds`, `TurnAllowanceNoDamage`
with its allow-list, `FlagNotCondition`); and the stage-1 checks
(`ForbiddenClassGrant`, `LeanSpread`, `SubclassCopiesBaseLean`,
`SubclassAddsPool`, `NameCollision`, the cap of eight, the copy
registry). **(change SV-20; M2)** These checks are registered as
`content.*` rows under CL 12.2 with the SH codes, and `NameCollision`
is run by `V7.NameRegistry`, which this document feeds (seeds and
validation design 5.2).

### 13.3 Simulator reports owed

- **the observed stealth share** at levels 1, 3, 5 and 10, worst legal
  and typical (the release input), with every stealth source in 15
  counted (the Caught Out and Swordsworn riders included) and level 3
  per subclass;
- **reliance reports** (classes 6.6, 12.3, **agreed** Q3) for stealth and
  own tempo, the Shade's primary leans: with and without the Shade, hero
  and recruit, printing the DEP-1 Arcana recruit-rule waiver (classes
  7.8);
- the capstone nova bound with Arts included and excluded, and with
  each subclass rider;
- offer-lane variety per subclass;
- parity at the worst legal build, solo and party, per subclass;
- camp share under three measures (10.5), and the 60% rule in the offer
  value unit;
- `Floor1ClassSpread` per pick and subclass; the full-floor clear rate;
  the rate of level-2 boss arrivals and the death rate there with Vanish
  3 at level 2;
- the subclass signature share at levels 3–4 for the Cutthroat and the
  Swordsworn (an M2 report), with the level-4 signature pip;
- Tainted Blade's Poisoned at the floor-1 boss (a spread lever);
- signature share and Opening fire rate in solo boss rounds;
- Cutthroat silent kills, broken groups and fights avoided per floor
  against a baseline Shade (a threshold, 8.1);
- Trapwright observed trap share in both directions against
  `SpringContentBound`; springs per fight; Dust use and Caught uptime;
  dead-room rate on trap-light floors (also gated in
  `TrapwrightHasMechanism`);
- Swordsworn Counterstroke rate by trigger; Lone Blade against ally-hit
  uptime; the AI deterrence report;
- Shadowed builds reported separately;
- Lure: elite-room notables taken without a fight;
- launcher family floors (to the weapons owner);
- **pending input request (encounter placement design 6.8):**
  `PartySurpriseShare` for the worst-legal Shade-in-party profile, fed to
  `stealthShare[L]` / `openShare[L]`; parity rerun if it exceeds the 10
  profile (18) (cross-document comparison 2026-11-23, CC-74).

**(change SV-22; M2)** The `Skill` arms `Novice`, `Basic` and `Expert`
are generalised from arcanist 5.4 to the Shade (request to the ARC
owner; fallback: the Shade owner writes a novice script by the M2
planning gate); the worst legal build above is the `WorstLegalShade`
arm (seeds and validation design 6.2).

## 14. Staging

**(lead's call; the Trapwright rule agreed, Q2)**

| Wave | Content |
|---|---|
| **M1** | Nothing. No M1 rule is needed for the Shade |
| **M2** | Section 11: the whole core; the strike limit and validators; Draw Out, False Trail, Shrugged Off, Lure (if accepted), Wise to You off; the NM-1 hazard ruling; the NM-2 engine; **the Cutthroat** (its second-kill route only with the body-noticed rule); **and the Swordsworn** (the fallback, applied 2026-11-21; TL-37). The trap records the Trapwright needs, the universal hand-trigger, Trigger Shot and 500‰ re-arm, and the `TrapwrightHasMechanism`, `SpringContentBound` and dead-room reports ship with traps (traps and locks design 0.1, 6.16). **Pending request (hazards design 6.15, 18; HZ-31):** the Shade hazard hook, owed before Voids ship (18) (cross-document comparison 2026-11-23, CC-7) |
| **M3** | The **Trapwright** as the **first Shade unlock** (TL-37), with the density reports as gates, per-group belief and witness-only secrecy for party-armed traps, and the Trap Eye re-scope; level-3 parity is rerun with the universal trap rows present before M3 (traps and locks design 6.16) |
| **Later** | Pinch, Lie in Wait, Gloom squares (with the Wise to You trial), Vault, Stake Out, the body-noticed rule if not taken in M2, Drag Line auto-entry (with Over the Edge), chained springs, monster trap use, Holy Water and Sticky flasks as Toss fuel, coatings |

**How the Trapwright call is made (lead's call):**
- The call is made **once, at the M2 planning gate**, when the dungeon
  generation design is approved or the M2 scope is fixed, whichever
  comes first.
- The Trapwright stays in M2 only if, at that gate, DEP-S1 (trap
  records and the density check) is in the M2 scope and NM-1 is
  accepted. Anything less applies the fallback **automatically**; it is
  not a new client question, because the client chose the rule and said
  "keep it for later".
- Under the fallback: the Swordsworn ships in M2 with the NM-2 engine
  and SH-8 / SH-9; the Trapwright becomes the first Shade unlock and
  ships in the first wave in which DEP-S1 and NM-1 land (M3 at the
  earliest; Later if they do not). Its rows SH-15 (beyond the hazard
  ruling), SH-16, SH-17, SH-18, SH-19, SH-25 and SH-26 are applied when
  it ships, not before, except that SH-15's hand-trigger, Trigger Shot
  and 500‰ re-arm rules ship universally in M2 with traps (traps and
  locks design 6.6, 6.7; TL-13, TL-15).
- The creation default at level 3 stays the Cutthroat in either case.
- The level-3 subclass screen shows only the subclasses of the current
  wave; the ladder preview's Trapwright line is hidden while it is not
  offered (7.14).
- **Resolved (2026-11-21; TL-37, agreed Q2 applied):** the traps and
  locks design (dungeon generation segment 5) does not put the SH-16
  density check in the M2 gate set, so the fallback applies: the
  Swordsworn ships in M2 and the Trapwright in M3. TP12's main-path
  minimums are an M2 gate for every party, not the SH-16 check. "The
  Trapwright places traps" is rejected (8.2; SH-26) (traps and locks
  design 0.1, 6.16).

## 15. Risks

| Risk | Mitigation, report and lever |
|---|---|
| The stealth share is the binding number, measured only in scratch | Release input; `OpeningCeiling` calibrated to it; every stealth source enumerated (Fade, Vanish, the Art carries, Shadowed, Duskhem, Long Shadow, the free No Witnesses, Cross Steel, the carry-only option, the Caught Out and Swordsworn riders, the level-4 signature pip; Gloom and Lie in Wait held back); Wise to You as a lever |
| Cutthroat, Swordsworn and the base sit within a point of the cap at L9–10 | Pre-committed levers (10.7); rerun owed |
| Floor-1 spread passes thin (9.2 against 10) and L2 arrivals fail | Vanish 3 at L2; the floor-pacing request (18), answered by the Antechamber Catch-up (run structure design 9.4; RT-8), with the level-2 arrival rate kept as a check that it fires (cross-document comparison 2026-11-23, CC-72); lever list (10.4); the full-floor simulator gate |
| The strike limit makes the early Shade feel less stealthy | Vanish 3 from L2; the big stealth die from L1; the carry sold as the loop (and kept from overkills by `holdCarryOnOverkill`); pips display; the Hide success chance shown before committing; spend-on-success held as a lever (10.7) |
| Bonus-action overload | Card warning; Nick default; tutorial preview line |
| The Trapwright depends on trap density (the traps and locks design ships the records in M2; density checks are M2 reports and M3 gates) (cross-document comparison 2026-11-23, CC-74) | The Q2 rule and its automatic fallback (14); Dust gives Caught and Toss a fuel floor where setup flasks are scarce; the dead-room rate is gated in `TrapwrightHasMechanism`; Drag Line's disadvantage works on any hazard |
| Under the fallback the Swordsworn ships in M2; at levels 3–4 one strike per rest made its signature fire about once every 2–3 fights | The level-4 signature pip (7.9), pre-committed and applied; Sworn Guard's ward and Answer carry level 3; signature share at L3–4 an M2 report for the Swordsworn and the Cutthroat; the next lever is the pip at level 3, only if the rerun keeps the 109.1 / 109.3 cells ≤ 110 **(lead's call)** |
| Out-of-combat floor clearing (Cutthroat) | Throat-Cut `strike`-tagged; body-noticed rule with the second-kill route; fights-avoided threshold lever |
| Poisoned lock on bosses | Stealth-dice-only trade; once per fight per elite or boss; Shrugged Off |
| The Swordsworn invites standard-HP melee; AI deterrence starves Counterstroke | Card warnings; deterrence report |
| Readability debt (tiers, triggers, carries, pips) | One glint per tier; tooltips name triggers; one carry glint whatever its source; `OpeningTriggersAdded` ≤ 3 |
| Duskhem echoes (No Witnesses III, Long Shadow) | The held text by default; registry ruling |
| The L8 trough and launcher family floors | Tricks 4 at L8 (5.5), rerun owed; what remains, and the launcher floors, to the weapons owner |
| Recall-based survey and SRD shapes | Check against SRD 5.2.1 before data |

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the band's values inside the agreed shape
  (two tiers, two ranks, no further steps), pool sizes inside the shape
  of 7.8, the strike limit's values (the signature pip included), the
  `OpeningCeiling` and `StrikeOptionCeiling` tables,
  `CampOptionBound`'s permille and reference encounter,
  `SpringContentBound` inputs, `TrapwrightHasMechanism` counts, lane
  weights and `offerLaneAdjust`, trigger ids, `bigHit`'s permille, the
  starting coin, the report thresholds.
- Level-up card, glint, pip, picker and tooltip layout.
- Wording of option, Art and trigger texts within the rules here, until
  the naming pass.
- **Not tuning:** the ranks spine and its schedule; three things to
  read; the tiered band's shape and the trigger tiers **(agreed, Q1)**;
  no dice step after level 5; Opening once per round, highest tier only;
  the strike limit as one shared tagged counter on `pool.tricks`;
  Tainted Blade trades stealth dice only; reaction hits never consume a
  carry; Throat-Cut out of combat only; From Nowhere's
  stealth die on every hit and its exclusions; the Trapwright staging
  rule and its fallback **(agreed, Q2)**; Sworn Guard as a Tricks-paid
  ward that counts as a Parry, every Counterstroke spending the strike
  limit **(agreed, Q3)**; no placed traps; no feature applies Surprised;
  one `restDefensiveTool` (settled).

## 17. Changes to approved documents

Applied on 2026-11-18, when the client approved this document, except
the Trapwright-only rows (SH-15 beyond its hazard ruling, SH-16 to
SH-19, SH-25 and SH-26), which are recorded now and apply only when the
Trapwright ships. The hazard ruling in SH-15 (known traps are hazards,
M2, regardless) applies now. Each change names the exact section it
touches. The client's Q1–Q3 answers approve the rows that name a
question; the rest were confirmed with the document on 2026-11-18.

**Narrowings recorded inside this design (no approved text changes;
recorded in classes 18 by SH-23's row):**
- Opening is never applied by default on reaction attacks (except
  Counterstroke, at the open tier); reaction hits never consume a carry;
- condition-applying Tricks options never fire on reaction attacks;
- Tainted Blade trades stealth-tier dice only;
- Throat-Cut out of combat and silent-only;
- Make an Example and Tripline immunity after a save;
- Jury-Rig re-arm once per trap per floor;
- Drag Line gives disadvantage on the hazard-edge save, with no
  auto-entry (conditions 10.1 and 10.2 unchanged);
- Lone Blade a swap, not an addition;
- False Trail only for a group from whose every watching member you are
  Hidden.

| # | Document / section (current status) | Change | Approved by |
|---|---|---|---|
| SH-1 | Classes 8.4, Signature verb bullet, "Once per round" sub-bullet: the band (*illustr.*; lead's call) | The tiered band (stealth 1d6 / 2d6; open +2 / 1d6; levels 1–4 / 5–10; no further steps) replaces "1d6, +1 die at 3/5/7/9" | **Client (agreed 2026-11-18, Q1)**; told plainly it replaces the stage-1 band |
| SH-2 | Classes 8.4, "Triggers" sub-bullets 1–4 and the Vanish bullet (lead's calls on the CL-8 basis) | Trigger 1 splits into Unready (open; "has not started a turn this fight") and Surprised (stealth); trigger 3 renamed Beset; tier as data; the Vanish carry counts as Hidden for Opening; consumption wording ("first Dexterity-rolled weapon hit on your own turn to which you apply Opening"); Answer narrowed to "attack roll" | **Client (agreed, Q1)** for the split, the rename and the carry; Lead for the wording |
| SH-3 | Vanguard 7.9 `TurnAllowanceNoDamage` (lead's call); weapons 6.6 and 7.5 step 8 (lead's call) | Allow-list `damage.extraDice.opening` and `damage.flat.opening` (named apart from the weapons 6.5 Precision term, which is not allow-listed); new `OpeningCeiling` and `StrikeOptionCeiling` with tuning tables; in step 8, Opening's dice go after Exploit dice and before crit doubling, and the flat +2 goes with the flat terms after doubling | Lead |
| SH-4 | Classes 3.4 (limited-ability bullet), 7.5 (counting); Vanguard 7.9 / VG-10 (lead's call) | "A per-rest counter on one named pool's record, spent by records tagged with its one declared tag, whether or not they spend from the pool, is a frequency limit and counts 0; it is not a named pool"; `strikeLimit` with the level-4 signature pip; the static `HideLimit` with its waiver list; `costByTrigger` and `costWaivers` (a waiver is not a refill) | Lead |
| SH-5 | Vanguard 7.3 `SurgeOptionBound`; weapons 6.4, parity targets, "Any build ≤ 110%" row (the VG-12 text); classes 6.4, single-target damage cap cell; classes 12.2, validator list (lead's calls) | Renamed and generalised to `CampOptionBound` (`actionsGranted` ≤ the base option's) in every approved document; scoped to non-slot, non-slotless camp pools beside `SpellTierBound` and `SlotlessCastBound` by ARC-2 (Arcanist design 11.2) | Lead |
| SH-6 | Weapons 7.5 (new step 8a, a marker only; step 8's "then any concentration check" points to DH 20.3); ability 14.3; DH 20.3 (the Duck offer between steps 3 and 3a; new step 3a; step 3's and step 8's "concentration uses step 3's value" read the step-3a value); conditions 9.3 and DH 15.2 (step 3a inserted; the pre-save total for concentration is the step-3a value; cross-document comparison 2026-11-23, CC-29) (lead's calls) | The Duck marker, offer and halving step (7.4) | Lead |
| SH-7 | Ability 7.3, policy filters (lead's call) | `bigHit` (on the DH 20.3 step-3 total), `noAllyHarm`, `preferStealthTarget`, `holdCarryOnOverkill`, `ifOpeningUnavailable`; `whilePoolAtLeastHalf` read for a pool with a strike counter | Lead |
| SH-8 | Weapons 7.5 step 6/6a, 8, 18.4; ability 7.2 threatened-zone row, 14.3 step 6; VG-18/20 (lead's calls) | `missedBy` and its order at a miss (Graze → `answeredBy` → 6a → the hook, re-checked); `sameReactionFollowUp` (same reaction after a reaction-spending or carried ward; spends the reaction after a fill); the `onParrySuccess` definition; the Riposte echo ruling | Lead; the Sworn Guard route **client (agreed, Q3)** |
| SH-9 | Armour 10.4, "Candidate order" and "Window algorithm" (lead's call) | Step 3 split: **3a** the target's own class-pool-paid wards (Sworn Guard, `countsAsParry: true`; Flare, `countsAsParry: false`; ARC-5, Arcanist design 7.5), **3b** the target's own Parrying property, then allies' charges; Blade Dance as a step-0 fill; a **step-0b** carried ward (Held Guard) that spends nothing, is exempt from the "no reaction left" skip, inherits the other skips and counts as the one reaction AC bonus | Lead; **client (agreed, Q3)** for the ward |
| SH-10 | Enemies 12.2, silent kill (lead's call, "the only exception"); 14.8 `broken` | Quiet Work sources; the body-noticed rule if the second-kill route ships | Lead |
| SH-11 | Enemies 12.1–12.2; main 4.3 (**decided**) combat start; DH 10.4 (**agreed**, Q6) | A clarification: Throat-Cut and the Cutthroat's unaware branch read the target's awareness, its group's record and `combatActive` at declaration (weapons 7.5 step 1), before DH 10.4's combat-start step; the blow stays in-combat damage, and DH 10.4's damage-floor reading is unchanged. The Cutthroat "unaware" set (Dormant / Stationary / Patrolling). **Main 4.3 and DH 10.4 are not changed.** If the lead's reading would change DH 10.4, this row needs the client | Lead (clarification) |
| SH-12 | Ability 7.2, Hide row; conditions 18.7 (lead's calls) | A Hide made as part of an attack (No Witnesses) and its end-rule ordering | Lead |
| SH-13 | Conditions, Frightened "Sources" line (lead's call) | Make an Example (Tricks-paid) | Lead |
| SH-14 | Conditions 7.3, 18.5 (lead's calls) | Shrugged Off | Lead |
| SH-15 | Conditions 10.3–10.4, 11.3, 11.6; ability 7.2 trap rows (lead's calls) | Known traps are hazards (M2, regardless); hand-trigger clarified; Trigger Shot; re-arm at 500‰ with no hard part, never stacking; hard part once per fight (with the Trapwright). The rules of conditions 10.1 and 10.2 are **not** changed (known traps simply become hazards they apply to): Drag Line only gives the 10.2 save disadvantage, and auto-entry is Later with the Vanguard's Over the Edge | Lead |
| SH-16 | Dungeon generation (undesigned) — **DEP-S1**; VG-12 (lead's call, client told) | Trap records (with `mechanismSquare`); `TrapwrightHasMechanism` as a generator constraint and seed property test (the trap-on-path guarantee and the dead-room rate included); the content reading with the static `SpringContentBound` over density parameters; `levelForFloor` | **Client (agreed, Q2)** for the content reading; Lead for the rest |
| SH-17 | Classes 8.4, "Launchers" sub-bullet (lead's call) | Launchers qualify on Unready, Surprised, Hidden and the carry, plus Caught | Lead |
| SH-18 | Ability 7.2; enemies 12.4 "Doors" paragraph and 11.1 scorer (lead's calls) | Wedge and the AI force-door term (or cut) | Lead |
| SH-19 | Stat model 6, trap-sense row (lead's call) | Trap Eye's proficiency lean | Lead |
| SH-20 | Enemies 11.2, awareness record (change A-5; lead's call); classes 6.3 forbidden grants and `ForbiddenClassGrant` (lead's call) | A class may write `lastKnown` into a group's shared record (False Trail), only when you are Hidden from every watching member; the reading that this is information, not an AI targeting override, as an enumerated `ForbiddenClassGrant` exemption | Lead |
| SH-21 | Enemies 12.3 noise sources; ability 7.2; enemies 14 group flags (lead's calls) | Lure; `luredThisFloor` | Lead (if the enemies owner accepts) |
| SH-22 | Enemies 11.2 (lead's call) | `wise[]`; tuning `wiseStep` 0, `wiseMax` 2 | Lead |
| SH-23 | Classes 7.5, the 'A new offer kind, "trigger" (CL-3)' bullet; ability 10.2, the offer-kinds list (the CL-3 text); classes 15, the plateau row; classes 3.3 PF3 (unless history is kept); classes 18 narrowings paragraph (lead's calls) | `TriggerFromFive` scoped to Opening-trigger records ("legal at any level" stays true for every other trigger offer); `TriggerTierFixed` with its allow-list; `OpeningTriggersAdded` ≤ 3; the narrowings above | Lead |
| SH-24 | Classes 8.4, Pools bullet (*illustr.*) | Tricks 2 / 3 / 4 at 1 / 3 / 8; Vanish 2 / 3 at 1 / 2 | Lead |
| SH-25 | Consumables 12, flasks (lead's call); registry | Toss (setup only), Dust (no item, Caught only) and the ruling against Slinger's Bracers | Lead |
| SH-26 | Classes 9.1, Sapper row (lead's call) | The Sapper never copies Jury-Rig or Tripline | Lead |
| SH-27 | Classes 6.2 `LeanSpread` (lead's call) | The Shade's tools recorded as tools | Lead |
| SH-28 | Classes 8.2, camp-pool chassis bullet (settled; VG-13); classes 11.B, camp-share row; Vanguard design 10.4 (the level-1 miss **agreed** there) | The accepted martial camp-share miss extended to levels 1–4 for the Shade (and to Arcanist levels 1–2 by ARC-4, Arcanist design 11.8). **This widens an agreed reading** | **Client, with this document's approval on 2026-11-18 (told plainly, 19.1)** |
| SH-29 | Enemies 11.1, opportunity-cost term (lead's call; VG-16) | Counterstroke priced as state | Lead |
| SH-30 | Classes 8.4, Subclasses bullets ("Cutthroat (M2)", "Trapwright (M2)", "Swordsworn (unlock)"); classes 13, M2 and M3 rows; classes 9.4; classes 7.3, the "Two per class at M2 ... aspects that change one rule" bullet (lead's call) | The Trapwright's wave becomes conditional (M2 only with DEP-S1; otherwise the first Shade unlock, the expected default); the Swordsworn is promoted to M2 under the fallback; the subclass `wave` / `fallbackWave` fields; a launch class's stage-1-named third subclass ships as a full subclass (as the Vanguard's three do), not as an aspect | **Client (agreed 2026-11-18, Q2)**; Lead for the 7.3 note |
| SH-31 | Classes 8.4, Subclasses bullets: the Cutthroat ("one more Opening die against Surprised targets"), Trapwright ("trigger or re-arm a known trap as a Tricks option") and Swordsworn ("a pooled reaction strike after a Parry") cards | Cutthroat: Quiet Work and the Surprised part (+1 / +1d4); Trapwright: Jury-Rig and Caught; **Swordsworn: Sworn Guard, a Tricks-paid ward that counts as a Parry, with Counterstroke** (the mechanism, not the intent) | **Client (agreed, Q3)** for the Swordsworn; Lead for the others |
| SH-32 | Classes 8.4, Pools bullet (the "bonus-action Hide, Disengage or Dash" sub-bullet), Growth bullets (Tainted Blade, From Nowhere) and "Signature events (12.1)" bullet | The Hide/Disengage/Dash ability named Fade, pooled and strike-limited; Tainted Blade trades stealth-tier dice only; From Nowhere at the stealth die; the signature events list of 7.1 | Lead |

**Not changed (checked):** ability 8.4 (**agreed**; no ground zones);
CN-11 (**agreed**); the S-5 Steal row (**agreed**; Pinch deferred);
"Opening and Exploit may both fire" (stage 1, CL-8); weapons 6.6's d4
minimum (no d3 is used); armour 14.2 Shadowed (the drafts' Shadowed
exception is dropped); armour Q2 (light armour has no band); main 4.3
(**decided**); DH 10.4 (**agreed**, Q6; SH-11 is a reading only);
conditions 10.1 and 10.2's rules (Drag Line has no auto-entry); the
32-condition budget; Catch Breath (settled).

**Check after applying (lead's call):** a grep for "one more die at 3,
5, 7 and 9" in classes 8.4 finds only history; a grep for "First
strike:" in classes 8.4 finds nothing and one for "Unready" finds it; a
grep for "counts as a first strike" in classes 8.4 finds nothing; a grep
for "qualify on first strike and Hidden only" in classes 8.4 finds
nothing; a grep for "Trapwright" in classes 8.4's Subclasses bullets
shows the conditional wave; a grep for "after a Parry" in classes 8.4 shows Sworn
Guard; a grep for "SurgeOptionBound" in **every approved document**
(the Vanguard design, weapons 6.4, classes 6.4 and 12.2) finds only
history beside `CampOptionBound`; a grep for "step 8a" in weapons 7.5
finds the Duck marker; a grep for "concentration uses step 3" in DH 20.3
finds step 3a; a grep for "missedBy" in weapons 7.5 finds the hook; a
grep for "3a" in armour 10.4 finds Sworn Guard; a grep for "legal at
any level" in classes 7.5, classes 15, classes 3.3 and ability 10.2
finds the `TriggerFromFive` scope note; a grep for
"damage.extraDice.precision" anywhere finds nothing.

## 18. Depends on systems not yet designed

- **Dungeon generation and trap content (DEP-S1):** trap records,
  density (`TrapwrightHasMechanism`), boss-room rules, closed doors;
  decides the Trapwright's wave (14). **Resolved with the fallback
  (TL-37):** the traps and locks design ships the records in M2, the
  density checks as M2 reports and M3 gates; the Trapwright is M3 (14).
  Owed by the Shade owner before M3: the level-3 parity rerun with the
  universal trap rows present, and the Trap Eye re-scope (TL-6; 8.2).
- **The exploration lead (pending requests, exploration and
  information design 4.5.2, XI-27):** the Cased rider (7.1; M3,
  conditional on TL-6) and the False Trail marker shape (9.1).
- **The enemies owner:** the body-noticed rule (the Cutthroat
  second-kill route); the AI force-door term (Wedge); Lure acceptance;
  Counterstroke deterrence pricing.
- **The armour owner:** the step-3a / 3b split and the step-0b carried
  ward (SH-9); without 0b, Held Guard falls back to `counterstrokeStop`.
- **The floor-pacing owner (pre-committed request, 10.4):** the floor-1
  boss door needs level 3, or the XP that guarantees it. **Closed:
  answered by the Antechamber Catch-up** (run structure design 9.4;
  RT-8); the level-2 arrival rate stays only as a check that the
  catch-up fires (cross-document comparison 2026-11-23, CC-72).
- **The hazards owner (pending request, hazards design 6.15, 18; HZ-31):**
  a Shade hazard hook owed before Voids ship (candidates: Operate a Cord
  without breaking Hidden; push-1 from Hidden at Void edges;
  VoidClassDeathGap ≤ BASE + 2). Until it is delivered, Voids do not
  ship. Also the Trapwright Jury-Rig Stir re-time (C ± 1, never below 2;
  M3, the Shade owner's call) (8.2). Not yet ruled by the Shade owner
  (cross-document comparison 2026-11-23, CC-7).
- **The encounter placement owner (pending input request, encounter
  placement design 6.8):** `PartySurpriseShare`, measured for the
  worst-legal Shade-in-party profile, is fed to this document's
  `stealthShare[L]` / `openShare[L]` tables (10); parity is rerun if it
  exceeds the 10 profile (cross-document comparison 2026-11-23, CC-74).
- **The other two stage-2 class documents:** `Floor1ClassSpread` as a
  real gate needs all four classes in data.
- **Companions:** a Shade recruit kit as a signature slice that does not
  copy the hero's Vanish Arts or From Nowhere. Answered by the
  companions design 8.3 and 11.3 (cross-document comparison 2026-11-23,
  CC-74).
- **The simulator:** the stealth share, parity per subclass, spread and
  full-floor clear, level-2 boss arrivals, fights avoided, the content
  bound. Pending (SV-22, M2): the generalised `Skill.*` arms, or the
  Shade owner's novice script by the M2 planning gate (13.3; seeds and
  validation design 6.2).
- **Naming pass:** every placeholder name here.
- **Rulings owed by the lead:** the Duskhem ruling (No Witnesses III),
  VG-18 (Toss and Dust against Slinger's Bracers; the Riposte echo).
  SH-11 is now written as a clarification (declaration-time reading);
  SH-28 was confirmed by the client with the document on 2026-11-18
  (19.1).
- **Lead's checks before data:** every SRD shape in this document
  (Opening against Sneak Attack, Fade against Cunning Action, Duck
  against Uncanny Dodge, Tumble Clear against Evasion, Extra Attack, the
  verbs' SRD sources) against SRD 5.2.1; `NameCollision` over the final
  names.

## 19. Open items for the client

None. Approved by the client on 2026-11-18. The future items in 19.2,
which come back only if a report or a later design triggers them, and
the naming pass remain future items.

### 19.1 This document

The client approved the document as a whole on 2026-11-18, including
every lead's call in it. The three questions it raised were answered on
2026-11-18 **(agreed)**, each with the recommended option:

- **Q1, Opening's band and triggers:** the tiered band (stealth tier:
  Hidden, Surprised, the Vanish carry = 1d6 at levels 1–4, 2d6 at 5–10;
  open tier: Unready, Beset, Answer = +2 flat at 1–4, 1d6 at 5–10; no
  further dice steps), with the trigger-1 split (Unready open, Surprised
  stealth), trigger 3 renamed Beset, and the Vanish carry counting as
  Hidden for Opening. The client was told plainly that this replaces the
  stage-1 band (121–174%) and that, like the Vanguard, the Shade passes
  ≤ 110% only under VG-12 plus the strike limit. The flat 1d3 and the
  stage-1 curve are rejected.
- **Q2, Trapwright staging:** in M2 only if trap records (DEP-S1) land in
  M2 with dungeon generation, with the content reading of trap damage
  (`SpringContentBound`); otherwise the Trapwright moves to the unlocks
  and the Swordsworn is promoted to M2. The client added "keep it for
  later": the unlock fallback is the expected default if trap records
  are not ready.
- **Q3, Swordsworn mechanism:** Sworn Guard, a Tricks-paid +2 AC ward
  against one melee attack roll that counts as a Parry; Counterstroke
  after a turned blow from level 3 and (Open Guard) after any melee miss
  from level 5; every Counterstroke spends the strike limit. The client
  was told this changes the stage-1 card's mechanism, not its intent.

**Told plainly to the client with the approval request, and confirmed by
the client with the document on 2026-11-18:**
- SH-28 extends the accepted level-1 martial camp-share miss to levels
  1–4 for the Shade (32–37% against the 40% floor).
- The check round moved two pool steps one level earlier, with the pool
  sizes as written (Tricks 2 / 3 / 4, Vanish 2 / 3): Vanish 3 to level 2
  (for the level-2 floor-1 boss arrival) and Tricks 4 to level 8 (for
  the level-8 trough); and it added a level-4 strike pip that only the
  subclass signature strike may spend. None changes a Q1–Q3 answer;
  every Counterstroke still spends the strike limit.

All three are confirmed by the client (2026-11-18).

### 19.2 Future items triggered by reports

These come back to the client only if a report or a later design
triggers them:

- the first level 1–4 parity lever (stealth die 1d4 at rank I), because
  it changes an agreed value (10.7);
- Pinch (an agreed-row change);
- Gloom squares together with a Wise to You trial as a softer bound;
- level-2 boss arrivals: closed, since the pre-committed floor-pacing
  request (18) is answered by the Antechamber Catch-up (run structure
  design 9.4; RT-8); the arrival rate stays only as a check that the
  catch-up fires (cross-document comparison 2026-11-23, CC-72);
- a `Floor1ClassSpread` miss whose levers run out;
- the naming pass.

## Appendix A: how the critique findings were resolved

**Judges and numbers check:**

| # | Finding | Resolution |
|---|---|---|
| J-1 | Ranks wins; core parity claims were optimistic | Ranks adopted; claims corrected (10.1) |
| J-2 | In-combat Throat-Cut 115–121% | Out of combat only, silent-only, `strike`-tagged |
| J-3 | Spring's flask mode tagged `strike` kills Spring at L3–4 | Jury-Rig tagged `device` |
| J-4 | Press the Duel pulls bosses onto HP 7 | Rejected |
| J-5 | Triggers saturation fails; `launcherOpening` 114% | Guards grafted; `launcherOpening` barred |
| J-6 | Seam fails at L9–10; replaces Answer | Rejected; the Beset rename kept |
| N-1 | The core band fails at L1–3 and L9–10 | Replaced by the tiered band; every cell ≤ 110 (10.1) |
| N-2 | Spread proxy fails | Tricks 3 at L3 + Vanish 3 (from L2 after the check round); thin pass under the correct comparator |
| N-3 | Lone Blade must be a swap, from L5 | Done |
| N-4 | Trapwright strict reading fails | Content reading required (Q2, agreed) |
| N-5 | L8 trough; launcher family floors | Weapons owner |

**Fun critic:**

| # | Finding | Resolution |
|---|---|---|
| F-1 | Flat 1d3 erases "my first blow lands huge" | Tiered band from L1 (agreed, Q1). +1 was tried and failed the L4 floor (87) and boss survival; +2 passes everywhere |
| F-2 | Lever A inverts the ambush; data contradiction | Trigger 1 split; the carry counts as Hidden; `TriggerTierFixed` consistent |
| F-3 | Re-stealth loop too rare | Vanish 3 (from L2 after the check round); stealth die 1d6 from L1; pips; the Wise to You trial as a future item |
| F-4 | Tainted Blade dominant, strips the verb | Trades stealth-tier dice only |
| F-5 | Subclasses thin at L3–4 | Caught at L3 (one exception); No Witnesses II carry-only option; the free turned-blow Counterstroke rejected (not self-limiting); the level-4 signature pip added in the check round (7.9), with the L3–4 signature share an M2 report (15) |
| F-6 | Tripline waits for enemies that never come | Fires on use against adjacent hostiles |
| F-7 | Dead levels; weak capstone; weak L10 talent | L4 keeps +1 Dex; L8 rank IIIs; From Nowhere 2d6 on every hit (1.57); Spare Trick renamed, Long Shadow listed first |
| F-8 | Readability debt | One glint per tier; tooltip triggers; `OpeningTriggersAdded` ≤ 3; tutorial preview |
| F-9 | Mechanics worth it | Lure, False Trail, Draw Out, Shrugged Off adopted; Pinch and Lie in Wait Later; Gloom with the Wise to You trial |
| F-10 | Lone Blade swap feels broken | Tooltip and L5 preview |

**Balance critic:**

| # | Finding | Resolution |
|---|---|---|
| B-1 | Final band never run with subclasses; validator defaults too low | Full rerun (10.1); `OpeningCeiling` calibrated to the measured shares, the simulator's share a release input; Cutthroat lever (+1d4) applied |
| B-2 | Wrong spread comparator | Vanguard mean used; Vanish 3 (from L2 after the check round); the armour-band lever found not to exist; residual levers listed |
| B-3 | Throat-Cut unbounded out of combat | `strike`-tagged; body-noticed rule with the second-kill route; fights-avoided threshold |
| B-4 | "Unaware" leaks to Wary and elites | Dormant / Stationary / Patrolling only; elites and bosses excluded; tests (12.1) |
| B-5 | `lingering` spends the nova margin | Removed |
| B-6 | Poison as defence | Once per fight per elite or boss; Shrugged Off; stealth dice only; Poisoned at the boss owed |
| B-7 | 90% floor and camp share thin at low levels | Open +2 keeps L4 typical at 91; L8 to the weapons owner; SH-28 |
| B-8 | Hide sources outside the limit | All enumerated (15); NM-8 and NM-9 held; Wise to You shipped off |
| B-9 | Trapwright untested with Tricks 3 | Rerun (109.9 at L3); `springLimit` first lever; Caught at L3 as the one exception at +2 |
| B-10 | Launchers, forbidden grants, cap, dips | Longbow ≥ 75%; none found; sub-limits under SH-4; dip test (12.1) |

**Consistency critic:**

| # | Finding | Resolution |
|---|---|---|
| C-1 | Hide limit not covered by VG-10 | SH-4; `strikeLimit`, shown as pips |
| C-2 | Lever A contradicts the Vanish text | `vanishCarry` its own stealth record; dependent texts re-worded |
| C-3 | Counterstroke hook unreachable on a miss | NM-2 a hard prerequisite (SH-8) |
| C-4 | Duck and Opening have no pipeline place | SH-3, SH-6 |
| C-5 | Make an Example: a major condition from a turn allowance | A Tricks-paid No Witnesses rewrite |
| C-6 | "Unaware" includes Wary; timing | The S-5 set; SH-11 ruling |
| C-7 | Parry analysis missed VG-19 borrowing | Evaluated; Sworn Guard chosen with reasons (agreed, Q3) |
| C-8 | The drafts' Shadowed exception should be dropped | Dropped; Shadowed builds reported separately |
| C-9 | No Witnesses change rows | SH-12; LoS normal at I, waived at ≥ 4 squares at II; III held text |
| C-10 | No d3 in the system | None used |
| C-11 | `TriggerFromFive` vs classes 15; Caught's level | Scoped to Opening records (SH-23); Caught the stated exception; Surprised part allow-listed |
| C-12 | Clear Head ends Frightened | Removed |
| C-13 | Tripline needs a new aura kind | Fires on use; no aura |
| C-14 | Names: Hamstring, Engage, Lie Low, two `bigHit` definitions | Not used / Cross Steel / dropped / one definition |
| C-15 | Silent rewording of Vanish and Answer | Recorded in SH-2 |
| C-16 | Pinch and Trap Eye touch agreed rows | Pinch Later; Trap Eye under SH-19 |
| C-17 | NM-1 ruling 3 duplicates existing rules | Not listed as a change |
| C-H | Change-list hygiene | SH-1 to SH-32 with sections and statuses; SH-30 to SH-32 added at write-up for the classes 8.4 and 13 texts the client's answers change |

**Check round (builder, consistency and play lenses; lead's calls):**

| Area | Finding | Resolution |
|---|---|---|
| Duck | Offered on a pre-resistance number; temp HP, minimum 1 and concentration undefined | Weapons step 8a a marker; offer on the DH 20.3 step-3 total, never on 0; 3a halves with minimum 1; concentration reads 3a (7.4; SH-6) |
| Carries | One flag could not hold the setters | `carries[]` entries with filters, uses and markers; refresh, consumption, out-of-combat expiry; reaction hits never consume; `holdCarryOnOverkill` (7.5; 13.1) |
| Capstone riders | Caught Out and the Swordsworn rider did nothing; Caught Out failed `TriggerTierFixed` | Both rewritten as one stealth-die hit each, allow-listed and in the nova and share reports (8.2, 8.3; 6.6) |
| Window order | Sworn Guard against Interpose and the Parrying property; no slot for Held Guard | Armour 10.4 steps 3a / 3b and 0b; Interpose-first intended (8.3; SH-9) |
| Strike limit | Throat-Cut and waived Counterstrokes outside a "sub-limit of pool options"; one cost per record | A counter on the pool record spent by tagged records; Throat-Cut a 0-Trick record; `costByTrigger`, `costWaivers`; static `HideLimit` with a waiver list (7.3, 7.9; SH-4) |
| Spare Trick | Which Trick is the spare | `spareReserve` rule and test (7.3) |
| Counterstroke | Sequencing at a miss; follow-up after a fill; Blade Dance cost | Order Graze → `answeredBy` → 6a → hook; same-reaction vs spends-reaction; cost table (8.3; SH-8) |
| Duck pairings | "Sworn Guard III" pointed at nothing | Pairings are Turn Aside and `breathOnDuck`; fills do not block Duck; Turn Aside resolves after step 15 (7.3, 7.4, 8.3) |
| State | Triggers read unrecorded state | Fields with reset events (13.1); Unready is "has not started a turn this fight" |
| Trick Shot | No scope, target, type or reaction rule | `trapOrFlask` scope, one picked target, the part's type, reaction default `never` (8.2) |
| Scopes | Thrown bullet against the catalogue; `duckFromRanged` empty | Scope column authoritative; `duckFromRanged` adds `launcher` to Answer against the Ducked unit (6.4, 7.11) |
| Throat-Cut, unaware branch | Timing unresolved; DH 10.4 already settles it | Declaration-time reading; DH 10.4 unchanged; SH-11 a clarification (8.1) |
| Validators | Not evaluable from data | Tuning tables and integer formulas; two yardsticks; static `HideLimit`; seed property test plus a static content bound (6.4, 7.9, 8.2) |
| Jury-Rig | Mechanism location, difficulty, jams, re-arm stacking, Killing Ground | `mechanismSquare`, `disarmDifficulty`, floor-end jams, no stacking, Killing Ground replaces the native trigger (8.2) |
| Held Guard, Open Guard | Reset event; optional III; `whilePoolAtLeastHalf` | Current turn's end; design-time fallback; policy defined (8.3, 7.12) |
| False Trail | Per-observer Hidden against a shared record; forbidden grants | Every watching member; one text; information, not an override (9.1; SH-20) |
| Picker size | Rewrites as entries | Rewrites are modes; the validator counts entries (7.3 rule 6) |
| Determinism and timing | Fade II rolls; Tainted Blade slot; From Nowhere combinations; Quiet Work; resets | Two d20s; step-7 spend reserving step 9; 7.7 interactions; evaluated after the kill; combat end and Downed resets |
| Names and change rows | `precision` collision; SH-5, SH-23, SH-28, SH-30 sections; Wedge baseline; `seedTarget`; D10 figures | Renamed `opening`; rows retargeted; Wedge dropped; `seedTarget` one value; 19.3 / 12.6 |
| Drag Line | Auto-entry the Vanguard deferred | Disadvantage on the 10.2 save; auto-entry Later (8.2; SH-15) |
| Reports | Reliance, nova with Arts, lane variety missing | Added (13.3; 12) |
| Play: early levels | Thin L3–4 signatures; L2 boss deaths; L8 trough | Level-4 signature pip; Vanish 3 at L2 and a floor-pacing request; Tricks 4 at L8 (5.5, 7.9, 10.4) |
| Play: Trapwright fuel | Setup flasks scarce | Dust; dead-room gate; the level-6 default by kit (8.2) |
| Play: Hide wasted on a failure | Strike spent on use | Success chance shown; spend-on-success held as a lever (7.4, 10.7) |
| Play: smaller items | Blade Dance "first"; 5.2 wording; Swordsworn preview; stealth share at L3 | Fixed (8.3, 5.2, 7.14); the L3 dip flagged and its rerun owed (6.4) |

## Appendix B: sources

Web-checked in the research streams (mechanism references only):
- [D&D 2024 Rogue (aidedd)](https://www.aidedd.org/en/rogue-2024/)
- [Dungeon Mister: Cunning Strike](https://dungeonmister.com/guides/classes-in-dungeons-dragons/rogues-cunning-strike-dnd-2024/)
- [D&D Fan: 2024 Assassin](https://dungeonsanddragonsfan.com/assassin-rogue-dnd-subclass)
- [Dungeon Mister: 2024 Thief](https://dungeonmister.com/guides/classes-in-dungeons-dragons/thief-dnd-2024-guide/)
- [Archives of Nethys: Rogue](https://2e.aonprd.com/classes.aspx?id=10)
- [Archives of Nethys: Twist the Knife](https://2e.aonprd.com/Feats.aspx?ID=570)
- [Archives of Nethys: Dueling Riposte](https://2e.aonprd.com/Feats.aspx?ID=367)
- [Archives of Nethys: Snarecrafter](https://2e.aonprd.com/Archetypes.aspx?ID=78)
- [Roll20: Dueling Parry](https://roll20.net/compendium/pf2/Dueling%20Parry)
- [Steel Compendium: Shadow](https://steelcompendium.io/compendium/main/Rules/Classes/Shadow/)
- [Pelgrane: 13A 2E changes](https://pelgranepress.com/files/13th_Age/13A_2e/13A_Changes_from_1e.pdf)
- [Gloomhaven Wiki: Scoundrel](https://gloomhaven.fandom.com/wiki/Scoundrel?oldid=5375)
- [XCOM wiki: Reapers](https://xcom.fandom.com/wiki/Reapers)
- [Darkest Dungeon wiki: Riposte](https://darkestdungeon.wiki.gg/wiki/Riposte)
- [Frosthaven Trapper guide](https://theboardgameschronicle.com/2024/07/21/strategies-frosthaven-trapper-strategy-guide/)
- [Arcane Eye: Swashbuckler](https://arcaneeye.com/class-guides/swashbuckler-5e-guide/)
- [bg3.wiki: Riposte](https://bg3.wiki/wiki/Riposte)
- [Pillars of Eternity II combat](https://pillarsofeternity.gamepedia.com/Pillars_of_Eternity_II:_Deadfire_combat)

Everything else is recall and is flagged in the table in 4.1. SRD 5.2.1
is the reference for every SRD-shaped check before data (18).
