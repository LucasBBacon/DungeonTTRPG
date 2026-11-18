# Classes, Stage 2: The Vanguard: Design

Date: 2026-11-18
Status: Approved, 2026-11-18
Parents: `2026-11-17-classes-concepts-design.md` (classes stage 1,
approved; the direct parent), and through it
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
`2026-11-17-death-healing-design.md` (death and healing design)

Items marked **(agreed)** were decided by the client on 2026-11-18 for
the three questions this design raised (Q1–Q3, section 4 and section
19), by choosing the lead's recommendation in each case; the client also
said that everything else in the lead's proposal stands, and approved
proceeding with change VG-12 after being told plainly what it changes
(10.1). Items marked **(lead's call)** were filled in by the design lead;
they were confirmed when the client approved this document on
2026-11-18. Items marked **(settled)** restate a rule from an
approved document; **(decided)** marks a main-design client decision.
Items marked **(change VG-n)** amend an approved document and are listed,
with an id and the exact section they touch, in section 17. Once
approved, every item here is binding on the builders.

**Revision of 2026-11-18 (lead's call).** A check pass after the
client's answers revised a number of lead's calls; appendix A (K-n)
lists them. No client decision changed. The largest revisions are: the
collision band (2 / 3 / 3, so that `HewCeiling` passes), the camp-share
figures with Momentum counted and the lever applied (41% and 40%), the
Momentum clauses, Ram standing in for Press, and the exclusion of
Technique capacity offers.

This is **stage 2 of the classes design for the Vanguard**, the M1 slice
class: its full table for levels 1–10, its Techniques and how they grow,
its core kit, its three subclasses (Weaponmaster, Bastion, Wrecker), the
capstone, the numbers, the M1 cut and how every binding requirement on
"the classes design" is met for this class. The other three launch
classes get their own stage-2 documents. Classes stage 1 section 14 lists
what stage 2 owes; section 12 below answers it for the Vanguard.

The client's brief: "Since this is leaning towards protection and weapon
craft, how should this progression go? I also really like the signature
Techniques, maybe those can be progressed going further into the class
too. Weaponmaster, Bastion and Wrecker sound like great subclass
specifications for roles: consider what they would each allow the
character to spec into, and if they give further signature moves."

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. They are scratch
models, not simulator evidence. The builders own them and the simulator
calibrates them (section 16), except where a value is marked as a client
decision, a settled value or a lead's number.

**Names are placeholders** pending the naming pass (a separate client
item). They are frozen for this document so that documents agree. Every
name new to this document was grep-checked against the approved
documents as a whole word and has no hits; the recheck in the revision
caught one draft name, Carry, which is the Instrument's focus verb
(weapons 17.8). Several earlier names fail `NameCollision` and are
renamed here **(lead's call)**. Only Challenge is a stage-1 name; the
others come from this design's drafts:

| Earlier name (stage 1 or this design's drafts) | Renamed | Why |
|---|---|---|
| Challenge (Bastion option; **the one stage-1 name**) | **Square Up** | Collides with the Bodyguard affix's ability (enemies 17.2) |
| Carry (the rider option, draft) | **Bite Deep** | Collides with the Instrument's focus verb Carry (weapons 17.8) |
| Second Form | **Borrowed Form** | Collision |
| Second Stance | **Stance Drill** | Collision |
| Late Breath | **Stubborn Breath** | Collision |
| Drive (the push option) | **Press** | Collision |
| Weapon Forms | **Practised Hand** | "Mastery"-style names are barred for verb features (weapons 7.4) |

Hold the Line nearly matches a non-SRD Cavalier feature and goes to the
naming pass unchanged.

**SRD check (settled principle, classes stage 1; lead's call to repeat
it here).** The SRD-shaped mechanisms used here (Second Wind, Action
Surge as Surge, Extra Attack, Shove, the weapon verbs that adapt SRD
masteries) were written from memory, and the research for this document
web-checked only parts of the Battle Master and the 2024 Fighter, where
sources disagree on details. **Every SRD-shaped mechanism must be
checked against the SRD 5.2.1 document before any record is written**,
and every record flagged `srd` cites the entry it was checked against.
Class, subclass, option and feature names are original; no D&D-only name
or text is used.

**Waves:** M1 is the vertical slice (one class, levels 1–3, solo), M2
the full run with four classes and companions, M3 profile and unlocks,
M4 breadth (species, multiclassing), Later everything after that no
milestone names. **(settled, main design 9)**

## 1. Intent

Stage 1 fixed what the Vanguard *is*: a Strength martial whose verb is
"I hit, then decide what the hit does", protection first, weapon craft
second, its own tempo through Surge, with three subclasses named.
**(settled)** It left open how the class grows from level 1 to level 10,
how many Techniques and Surges it gets and when, what each subclass lets
the player spec into, and what the capstone does in each build.

The answer in one sentence: **the Vanguard keeps exactly four things to
read at every level (at-wills, Second Wind, Techniques, Surge), and all
growth is depth on those four rows**: Technique **ranks**, one
**signature Technique** per subclass that ranks up at levels 3, 5 and 7,
one **signature Surge move (an "Art")** per subclass from level 5, and a
level-6 feature that **rewrites a verb**. Protection grows through the
stance and the capstone; weapon craft grows through the verbs.
**(lead's call)**

## 2. Goals, non-goals, anti-goals

### Goals

- **Signature Techniques that progress further into the class** (the
  client's brief): every subclass owns one signature option that gains
  ranks at 3, 5 and 7, and one Surge Art at 5 that gains a rank at 9.
- **Protection that grows in *whom* and *how*, not in size:** one
  reaction AC bonus per attack always (settled, armour 10.4).
- **Weapon craft that grows in what a hit does, not how much it deals.**
- **Four things to read at every level** (settled, ability 11.4;
  classes 7.6).
- **A felt decision on floor 1,** not a Hew-every-hit default.
- **Three subclasses that are roles:** a verb toolkit (Weaponmaster), a
  mark-and-hold defender (Bastion) and a board-control bruiser (Wrecker).

### Non-goals

- **The other three classes' tables.** Their own stage-2 documents.
- **Companion kits** (the companions design), beyond what a Vanguard
  recruit's signature slice must not break.
- **The multiclass model** (M4); only the dip guards are honoured here.
- **Final names** (naming pass) and **final numbers** (simulator).

### Anti-goals

- **Damage-adding options** that break weapons 6.4 parity. No rank and
  no Empower line adds dice or flat damage; Hew's die step is the one
  named exception.
- **A fifth reading row:** no in-fight stance toggles, no stored combo
  meter, no new pool.
- **Long reactive chains** that make the Bastion play itself on enemy
  turns.
- **A new condition:** the 32-condition budget stays untouched.
- **More floor-1 power** for the class that is already favoured there
  (10.3).

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Classes 8.2, 8.3 | Chassis: Strength / Constitution; Sturdy 8; Fortitude; heavy ceiling, shield, simple + martial; 2 of Athletics, Intimidation, Medicine, Perception, Lore; Second Wind (full Recovery, rest 1, 2 from 6), Techniques, Surge; a stance creation pick; Weaponmaster M1, Bastion and Wrecker M2 | 7.1 |
| Classes 6.4 | The launch leans: protection P; single-target damage, damage types and riders, reach and zones, reactions s; own tempo (Surge); forced movement as the Wrecker's lens | 7.1 |
| Classes 7.4 | The published ladder: talent at 2 and 7, subclass at 3, +1 ability at 4 and 8, offer at 5 and 9, subclass feature (changes a verb) at 6, capstone at 9, talent or offer at 10 | 5.4 |
| Classes 7.5, 7.6 | A named pool is one limited ability; the cap of eight; four things to read, the Vanguard at four from level 1, so it takes only options, upgrades and triggers | 7.9, 10.5 |
| Classes 7.7 | Dip guards: `scalesBy: classLevel`, `minClassLevel`, `chassisOnly` | 6.4 rule 9 |
| Ability 6.3, 6.4 | Rest and camp pools; 40–60% camp (lead's call there); one camp spend per turn | 7.3, 10.4 |
| Ability 6.6, 6.7 | The martial template; at most 3 named martial pools; capstone a camp pool of 1 at level 9 | 7.8 |
| Ability 7.3; armour 10.3, 10.4, 11 | Policy on every reaction; Interpose +2; one reaction AC bonus per attack; the AC envelope | 7.4–7.7 |
| Weapons 6.4 | Any build ≤ 110% of the greatsword reference | 10.1; VG-12 |
| Weapons 7.1, 7.2, 7.3, 7.5 | The 15 canonical verbs; bounds (forced movement ≤ 3 summed, verb-sourced once per round, collisions only with a non-verb part); classes own depth, never access; the attack pipeline, Techniques chosen at step 7 | 6.4–6.6 |
| Conditions 7, 10.1, 10.2, 16, 18 | Hard control only from limited abilities; forced movement and collisions; edges; the budget of 32, full; Challenged, Teetering, Staggered | 6, 8 |
| Enemies 11.1, 11.3 | The AI never reads class; opportunity-cost term | 7.4; VG-16 |
| Trinkets 11 | The copy registry, bidirectional | 7.13 |
| DH 8, 12.1, 12.4, 21.1 | Recovery; the one full-Recovery rest tool; 2 kit potions; `Floor1BossSustain` | 7.2, 7.10, 10.3 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored.

### 3.3 Findings in the approved documents

Gaps and facts found while designing; each shaped a rule here or a
change in section 17. **(lead's call)** Ids D1–D12 are local to this
table.

| # | Finding | Where it is resolved |
|---|---|---|
| D1 | Both stage-1 Wrecker clauses are base rules already. A Technique push is not verb-sourced, so it collides (weapons 7.2 rule 4; conditions 10.1). Teetering on a successful edge save is the M2 rule for everyone (conditions 10.2) | 8.3 |
| D2 | Shieldwall does nothing on solo floor 1: it needs an adjacent ally, and the pick is made with no test drive (main 4.5) | 7.5 |
| D3 | Weapons 6.4 "any build ≤ 110%" binds the class's own pools, and no measure for pools is defined. A greatsword Vanguard that Hews every Technique and spends every Surge on Attack sits at 114–121% | 10.1 |
| D4 | Under the ability 6.6 template, the camp share cannot reach 40–60% at level 1, because the full-Recovery Second Wind dominates rest value | 10.4 |
| D5 | A Technique is chosen at pipeline step 7, after the roll. No option may change its own attack roll; every line acts at step 7 or later, or on a later attack | 6.4 |
| D6 | The stage-1 suggested kit (longsword Parry, greatsword Graze) has no on-hit verb to empower, which makes the M1 Weaponmaster nearly empty | 7.10 |
| D7 | The Bastion's Hold the Line clause (stop on a Set hit) works only with a pike | 8.2 |
| D8 | The Challenged-Disengage stop is already a base rule: leaving the challenger's reach provokes even after Disengage (conditions 18.8), and Hold the Line stops on that leave-triggered opportunity-attack hit | 7.4, 8.2 |
| D9 | Drive Through's geometry fails: after a push of 1 the target is out of Shove reach | 7.6 |
| D10 | Grim Resolve is nearly dead in M1: only the M1 boss applies Frightened (its roar) and Dazed (limited, from phase 2) (enemies 9.5, 17.5), so it has no target in any other fight | 7.6 |
| D11 | Once-per-round `turn` pools silently break "at most three named martial pools" | 7.9; VG-10 |
| D12 | M1 has no offers (ability 14.6). The M1 ladder at levels 1–3 is the kit, the level-2 talent and the subclass | 11 |

### 3.4 Terms

**(lead's call)** Used with these meanings everywhere below, beside the
stage-1 terms (classes 3.4: once per round, once per turn, granted
attack, limited ability, a signature fires), which stand **(settled)**.

- **Rank:** a level of an option (I, II, III). An option's current rank
  is the highest rank whose `minClassLevel` is at most your class level,
  so higher ranks arrive automatically, and an option learned after its
  rank-II level arrives at rank II (6.3; 13.1). A rank never adds dice,
  except Hew's named step.
- **Signature Technique:** the one Technique option a subclass adds
  (Borrowed Form, Square Up, Ram).
- **Art:** the one Surge option a subclass adds from class level 5
  (Arsenal, Step In, Earthshaker).
- **Free effect:** an option or Empower line applied without spending a
  Technique (6.4 rule 3).
- **Payable:** an Empower line a free allowance may pay for (6.6).
- **Turn allowance:** a `turn` pool of 1 on its owner that only limits an
  option, passive or reaction to once per round; not a named pool (7.9;
  VG-10). A per-target count (Topple's) is a separate record kind, the
  **per-target round count** (7.9).
- **Stopped early:** at least 1 square of a move's chosen distance was
  blocked by a wall, obstacle or unit, a 0-square blocked move included.
  A verb part ignored because its target was already verb-moved this
  round is not a stop (6.4 rule 6) **(lead's call)**. Stopping before a
  hazard is not stopping early **(settled, conditions 10.1)**.
- **Window fill:** a reaction AC bonus that applies to an attack roll
  from a camp-pool ability without a reaction being spent, at the new
  step 0 of the armour 10.4 window (7.7; VG-4).
- **Momentum:** the Vanguard's one combo flag (6.7).

## 4. Framework decisions

The client chose Q1–Q3 on 2026-11-18, each time the lead's
recommendation. **(agreed)** The rest are the lead's calls, which the
client said stand.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Technique progression model | **Ranks**, with grafts from the other two models (6.1–6.3) **(lead's call)** | Catalogue (picker load, Hew still dominant, heaviest QA, carve-outs). Combo (a new subsystem, AI lookahead, picker overflow); only its Momentum flag is kept |
| 2 | Camp share at level 1 (Q1) | **Accept the level-1 miss.** Measure with the `restDefensiveTool` (Second Wind) at half value and report all three measures; hold the 40–60% band at levels 5 and 10 (about 45% and 40%) (10.4; VG-13) **(agreed)**. With Momentum counted and the lever applied: 41% and 40% | Surge 2 from level 1 (adds floor-1 power to the class already safest there and widens the floor-1 spread) |
| 3 | Momentum in M1 (Q2) | **Momentum ships in the M1 slice Vanguard.** A paid Press or Empower sets it; the next hit on that target before the end of your next turn may take Press or Empower free; it never chains and never pays for Hew; from level 3 in M2 it may take the subclass signature instead (6.7) **(agreed)** | Momentum at M2 only (a thinner floor 1: the signature fires in about 15% of rounds instead of about 37%) |
| 4 | Stance re-pick (Q3) | **The stance may be re-picked once per camp**, rest-gated, never in combat; Stance Drill at level 7 still makes both stances live (7.5; VG-17) **(agreed)** | A permanent pick with Stance Drill as the only way to the second stance (a solo Hold the Line Vanguard could not switch to Shieldwall when companions join) |
| 5 | Parity reading for class pools | **VG-12:** rest-pool damage measured floor-averaged at the worst legal build against weapons 6.4's ≤ 110%; camp-pool damage bounded by the nova bound and `SurgeOptionBound` (10.1) **(lead's call; the client was told plainly and approved proceeding)** | The row as written (every plan fails it, this one included). A per-fight measure (126–164%; meaningless for rest pools) |
| 6 | Pools | **Techniques 2 / 3 from level 5 / 3 at level 9; Surge 1 / 2 from level 5; Second Wind 1 / 2 from level 6; Stand Fast camp 1 at level 9** (7.8; VG-6) **(lead's call)** | Stage-1 Techniques 4 at level 9 (parity). Surge 2 at level 9 (camp share at level 5 out of band) |
| 7 | Hew | **1d4 at levels 1–4, 1d6 from level 5** (its rank II); Hews per rest capped at the class-table pool; Deep Reserves' use deals no damage; Technique capacity offers excluded (6.4 rule 7; 7.11; VG-6) **(lead's call; the last two from the revision)** | 1d6 from level 1 (Hew dominates every hit; parity) |
| 8 | Empower | **One canonical table; no line adds dice or flat damage; every line tagged with a `payable` flag** (6.6) **(lead's call)** | Damage lines (beat Hew 1d4 and fail `HewCeiling`) |
| 9 | Free effects | **At most one free effect per hit, and it is that hit's Technique;** the spare-verb fire (Twin Verb, Arsenal II, the Weaponmaster's Stand Fast rider) is the one exception, control verbs only (6.4 rule 3) **(lead's call)** | Per-feature slot rules (unreadable; stacked free effects) |
| 10 | Surge Arts | **One Art per subclass from class level 5, rank II at 9, under `SurgeOptionBound`** (7.3; 8) **(lead's call)** | Arts from level 3 (floor-1 power). Two-attack Arsenal (2000‰). Blade Storm (fails at level 3) |
| 11 | Stances | **Learned, never toggled** (7.4, 7.5) **(lead's call; re-pick agreed, Q3)** | In-fight toggles (a fifth reading row) |
| 12 | Starting kit | **Chain mail; warhammer + shield active; greataxe spare; 2 potions**; for the M2 Weaponmaster, a recommended one-handed spare with a borrowable verb (7.10; VG-21) **(lead's call)** | Longsword + shield / greatsword (no on-hit verb to empower, D6). Kit presets at creation (a second level-1 pick) |
| 13 | Capstone | **Stand Fast as a window fill over every attack roll against adjacent allies for one round, shield required, with a solo clause and an automatic subclass rider** (7.7; VG-4, VG-15) **(lead's call)** | The stage-1 "first attack roll" form (invisible; dead in two of three builds) |
| 14 | Wrecker | **Re-specced:** Battering Force, Brink, Ram, then Domino or Upend; collision band flat 2 / 3 / 3 *(illustr.)* (8.3; VG-2) **(lead's call; lowered from 2 / 4 / 6 in the revision, because 4 and 6 fail `HewCeiling` against Hew 1d6 once both collided units are counted, 10.1)** | The stage-1 card (both clauses are base rules already, D1). The dice band (Ram out-damages Hew at level 10). The 2 / 4 / 6 flat band (fails `HewCeiling` at levels 5 and 10) |
| 15 | Bastion redirect | **Take the Blow moves to Later** with Guarded or Holdfast (8.2) **(lead's call)** | Take the Blow at M2 (reproduces Guarded, fails `FlagNotCondition`; its ranged trigger copies Shelter) |

### 4.1 How other games handle martial and defender progression

Mechanism references only. No names or text are taken. Recall-based
unless marked "checked". The Battle Master and the 2024 Fighter were
web-checked in part, and the sources disagree on details; every
SRD-shaped mechanism is checked against SRD 5.2.1 before any record is
written **(settled, classes stage 1)**.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| **5E Battle Master** (checked in part) | Small superiority pool, many maneuvers, more known at 3/7/10/15, short-rest refresh | One pool, many options; small pool steps | Damage-adding maneuvers (parity); non-SRD maneuver text |
| **5E Fighter 2024 / Weapon Mastery** (checked in part) | Extra uses for Second Wind; new spends on existing pools; each weapon has one mastery | New spends on existing rows; verbs per weapon (already our verbs) | Class-gated mastery unlocks |
| **5E Barbarian 2024, Brutal Strike** | Trade a resource for an effect at the hit | "Hit, then decide" (Techniques) | Extra damage dice on the trade |
| **5E Cavalier** (non-SRD) | Mark, ally reaction AC, a movement stop on an opportunity-attack hit | The defender shape | Its names (Hold the Line goes to the naming pass) |
| **PF2e Fighter / Guardian** (checked in part) | Reactive Strike at 1; Taunt; Intercept; stances entered as actions | One taunt, one intercept, one reaction, deepened by later choices | Toggle stances (they cost a reading row) |
| **D&D 4e Fighter** | Mark everything you hit; strike when a marked foe attacks others | Square Up + Retort Strike ("ignore me and pay") | Uniform power structures |
| **13th Age** | Effects chosen after seeing the natural die | `naturalHighFree` as a trigger only | A base mechanic on natural rolls |
| **Lancer** | Talent ranks I/II/III; Brace | Ranks as the growth axis | Brace (it reproduces Anchored, which is Later) |
| **Gloomhaven** | Push, retaliate, shield cards; once-per-scenario loss cards | Arts as camp-gated big moves | Retaliate damage that costs nothing |
| **Into the Breach** | Push 1, bump damage, exact previews | Collisions, Ram, Domino, exact landing previews | Long chains |
| **Battle Brothers** | Shieldwall, Spearwall, Indomitable, Rotation | The trigger shapes | Flat aura stacking (dead numbers, the AC envelope) |
| **Darkest Dungeon** | Guard redirect, riposte as status | Protection as a choice of whom and when | Redirect as a standing state (Guarded is Later) |
| **XCOM 2** | Bladestorm zones; Templar momentum | Telegraphed reaction zones; momentum that lasts a turn | Hoarded meters (turtling) |
| **Hades** | Aspects rewrite a weapon's attack | "The level-6 feature changes a verb" | — |
| **Fire Emblem combat arts** | Utility moves (shove, swap) beside damage moves | Positioning on the same menu as damage | The weapon triangle (a key per enemy) |
| **Slay the Spire Ironclad** | One scaling axis touches every card | The axis is the option repertoire, not damage | Strength stacking |

**Combined lesson.** Martial depth comes from options and timing, not
numbers. A defender feels active when it chooses whom to protect and
punishes foes who ignore it. Forced movement needs exact previews and
terrain payoffs. With little reading space, a stance works as a lasting
identity, not a per-turn toggle. Momentum should last a turn, not be a
hoarded meter.

## 5. The client's brief: the progression shape

### 5.1 The answer

**(lead's call)** The Vanguard grows by **depth on four fixed rows**,
never by new rows. Each subclass is a role the player specs into, and
each gives **two further signature moves**: a signature Technique that
ranks up at 3, 5 and 7, and a Surge Art at 5 that ranks up at 9. The
level-6 feature rewrites a verb, and the capstone extends the stance's
protection to the line, with a rider in the subclass's own voice.

### 5.2 The two hands

- **The shield hand (protection)** grows through the stance (Hold the
  Line or Shieldwall), the Bastion, and the capstone Stand Fast. Each
  step widens *whom* the Vanguard protects and *how*, never the size of
  the bonus. Protection stays at one reaction AC bonus per attack
  **(settled, armour 10.4)**.
- **The sword hand (weapon craft)** grows through Empower, Practised
  Hand, Borrowed Form and the level-6 verb rewrites. Each step changes
  *what a hit does*, never how much it deals.
- **Both hands share one verb:** Techniques, "I hit, then decide"
  **(settled, classes 8.3)**.

### 5.3 How signature Techniques progress deeper

| Level | Every Vanguard | Each subclass |
|---|---|---|
| 1 | Press, Hew, Empower (rank I); Momentum | — |
| 3 | — | **Signature Technique, rank I** (Borrowed Form, Square Up, Ram) and the subclass clauses |
| 5 | Press II, Hew II (1d6), Empower II; Extra Attack (two decisions per turn) | **Signature rank II**; **Surge Art** arrives |
| 6 | — | **Verb rewrite** (1 of 2) |
| 7 | Talent with a trigger option | **Signature rank III** |
| 8 | Other known options rank II | — |
| 9 | **Stand Fast**, with its automatic subclass rider | **Surge Art rank II** |
| 10 | Talent or offer | — |

### 5.4 What each subclass lets the player spec into

| Subclass | Specs into | Signature Technique | Surge Art | Level 6 (1 of 2) | Stand Fast rider |
|---|---|---|---|---|---|
| **Weaponmaster** (M1) | Verb depth; a two-set toolkit; breadth or reliability | Borrowed Form | Arsenal | Twin Verb / Sure Form | Free Empower, plus the spare set's verb, on a hit against an attacker that missed a covered ally |
| **Bastion** (M2) | Mark, hold, shield, punish | Square Up | Step In | Retort Strike / Shield Craft | Covers allies within 2 squares |
| **Wrecker** (M2) | Collisions, hazard edges, crowd breaking, Prone set-ups, the Shove as a weapon | Ram (replaces Press) | Earthshaker | Domino / Upend | Pushes a melee attacker whose attack misses a covered ally |

### 5.5 Level table *(illustr.)*

**(lead's call)** Default array, Con +2. Str goes +3 → +4 (level 4) →
+5 (level 8). AC is shown as shield set / two-hander set.

| Lvl | Floor | Automatic | Choice | HP / Rec | AC | Prof / attack / difficulty |
|---|---|---|---|---|---|---|
| 1 | 1 | Kit; at-wills; Second Wind 1; Techniques 2 (Press I, Hew I 1d4, Empower I); Surge 1; Momentum | Stance: M1 Hold the Line; M2 Hold the Line or Shieldwall | 21 / 8 | 18 / 16 | +2 / +5 / 13 |
| 2 | 1 | — | Talent (7.6) | 31 / 10 | 18 / 16 | +2 / +5 / 13 |
| 3 | 1→2 | — | Subclass (M1: Weaponmaster; M2: 1 of 3): signature rank I and subclass clauses | 41 / 12 | 18 / 16 | +2 / +5 / 13 |
| 4 | 2 | — | +1 ability, alone (settled) | 51 / 14 | 18 / 16 | +2 / +6 / 14 |
| 5 | 2→3 | Extra Attack; Techniques 3; **Surge 2**; rank II for Press, Hew (1d6), Empower and the signature; **Surge Art**; armour band +1 | Offer (1 of 3) | 61 / 16 | 19 / 17 | +3 / +7 / 15 |
| 6 | 3 | Second Wind 2 (settled) | Subclass feature (1 of 2), changing a verb | 71 / 18 | 19 / 17 | +3 / +7 / 15 |
| 7 | 3→4 | Signature rank III | Talent (with a trigger option) | 81 / 20 | 19 / 17 | +3 / +7 / 15 |
| 8 | 4 | Other known options rank II | +1 ability | 91 / 22 | 19 / 17 | +3 / +8 / 16 |
| 9 | 4→5 | **Stand Fast** with its subclass rider; Art rank II; armour band +1 | Offer (1 of 3) (agreed, CL-4) | 101 / 24 | 20 / 18 | +4 / +9 / 17 |
| 10 | 5 | — | Talent or offer | 111 / 26 | 20 / 18 | +4 / +9 / 17 |

- **No pool steps up before level 5 (lead's call).** Level 3 often lands
  at the floor-1 boss, and a level-up delivers its gain ready to use
  (ability 6.2). The class is already favoured on floor 1 (10.3), so this
  is deliberate.
- **Level 4 stands alone (settled, lead's call there).** Level 8 gains an
  automatic beat, which the ladder allows: its automatic column was "—",
  not a rule.
- **The stance re-pick** (Q3, 7.5) is not a level beat; it happens at
  camp.

## 6. Techniques

### 6.1 The three progression models

| Model | Core idea | Strength | Weakness |
|---|---|---|---|
| **Catalogue** | A growing repertoire: tiers I–III, a cap of 8 known, camp swaps, Learn talents | Most "new button" moments; costs 0 toward the cap (CL-2) | Picker load (8 options, every hit); Hew still dominant; the heaviest QA; Masterstroke, Combination and Pin Down need carve-outs or invite lockouts |
| **Ranks** | A fixed set of at most 6 options, each with ranks I/II/III | Pips on existing buttons, costing 0 to read; "ranks add no dice" answers parity | Subtle growth; a heavy Empower table; automatic rank-ups; level 9 crowded (as drafted) |
| **Combo** | Leads set Momentum; the next hit cashes in a free option or a subclass Finisher | Best felt decision; lowers Hew use; lifts the level 1–2 signature share | A new subsystem; AI lookahead; picker overflow; pulls against Hold the Line |

### 6.2 Verdict and judges' scores

Mean of three judges, 1–10; build cost is scored as "low cost", so
higher is cheaper.

| Model | Depth and growth | Readability | Decision quality | Balance safety | Fit with docs | Build cost (low) | Total | First place |
|---|---|---|---|---|---|---|---|---|
| Catalogue | 8.3 | 5.3 | 6.7 | 5.7 | 6.0 | 4.0 | 36.0 | 0 of 3 |
| **Ranks** | 7.0 | **8.0** | 6.0 | **8.0** | **7.7** | **5.7** | **42.4** | **3 of 3** |
| Combo | 7.7 | 6.3 | **8.3** | 6.7 | 6.0 | 5.3 | 40.3 | 0 of 3 |

**Verdict (lead's call): ranks**, the winner on every judge's card. The
combo model's best idea, the felt decision, is taken as one flag
(Momentum, 6.7); the catalogue model's guards and table are taken
without its repertoire.

### 6.3 The spine and its grafts

**(lead's call)**

- **The rank schedule (from the systems draft):** rank II at level 5,
  signature rank III at level 7, and no core rank III. That leaves level
  9 to the capstone and the Art rank II.
- **From the fun critique:** other known options gain rank II at level
  8. This splits the level-5 reading wall and fills the level-8 plateau.
- **From the catalogue model:**
  - the `HewCeiling` check, extended to rank lines and free effects;
  - the canonical Empower table with tags and the payable flag (6.6);
  - the legal-only picker, in a fixed order, with the scorer's
    suggestion pre-highlighted, at most 6 shown
    (`TechniquePickerSize`);
  - Low Cut as learnable content.

  No tiers, no repertoire cap and no camp swaps.
- **From the combo model: Momentum only**, as one flag (6.7). Leads,
  Finishers, a cash-in band and lookahead are not taken.
- **Cut, with one-line reasons:**
  - Hew "late call" (pipeline step 8b) and Hew "Spill": policy and the
    preview already cover overkill;
  - Set III (halved movement): a new rule shape;
  - Masterstroke, Combination, Reave, Pin Down, Ring the Bell and Read
    the Guard: carve-outs, flat damage, a lockout ladder, and overlap
    with the roll-mode cap.

### 6.4 Global Technique rules

1. **Cost.** 1 Technique from the `rest` pool, spent at pipeline step 7
   after the hit and the crit are known **(settled, classes 8.3)**.
2. **One Technique per hit (lead's call, classes 8.3, "Timing").** There
   is one carve-out: the Square Up II pairing (8.2), where Square Up and
   one other option apply to the same hit. Each is paid for, or the
   second is the hit's one free effect (a Momentum cash-in). The
   carve-out is recorded in VG-2.
3. **One free effect per hit, and it is that hit's Technique (lead's
   call).** Free effects are:
   - Practised Hand (Borrowed Form III, where Practised Hand pays for
     Borrowed Form, is Practised Hand's free effect);
   - a Momentum cash-in;
   - `naturalHighFree`;
   - `surge.ramFree`;
   - Arsenal's per-hit payable line;
   - the Weaponmaster's Stand Fast rider.

   **The spare-verb fire is the one exception:** Twin Verb, Arsenal II's
   second verb and the Weaponmaster Stand Fast rider's spare verb fire
   the spare set's verb under Twin Verb's list and bounds (8.1). They do
   not occupy the slot and carry control verbs only. **When more than one
   free effect is available on a hit, the player (or the scorer) picks
   one; only the one taken is spent, and the others stay available under
   their own limits (lead's call).** So from level 5 (two attacks a
   turn), a Weaponmaster holding Momentum who takes the cash-in on one
   hit keeps Practised Hand for the other. At levels 3–4 (one attack a
   turn) the two compete for the same hit (6.7).
4. **Timing.** Every effect acts at step 7 or later, or on a later
   attack. No option changes its own roll (D5).
5. **Reactions.** On another unit's turn, an option follows the per-unit
   policy {never, ifKills, always, ask}, default `never` **(settled)**.
   **Condition-applying options never fire on reaction attacks**,
   mirroring weapons 7.2 rule 3. This narrows classes 8.3's "usable on
   reaction attacks" (recorded in classes 18, VG-1) **(lead's call)**.
6. **Bounds no rank may break (lead's call; the first two restate
   weapons 7.2 rule 4 as read here):**
   - all forced movement from one hit sums to ≤ 3 squares **actually
     moved, across all moved units**, and resolves once (step 10; the
     Domino link at step 10b uses only the remainder; VG-8);
   - verb-sourced movement moves a unit at most once per round;
   - one shared rider/Topple save slot per target per hit. A slot
     reserved for a deferred save (Ram, Upend) blocks riders and Topple
     on that target even when no save is rolled;
   - Topple, and every Prone source whose record has `sharesToppleCount:
     true`, at most once per round per target (a **per-target round
     count**, 7.9). The values are listed below;
   - one rider per target;
   - M1/M2 conditions only;
   - no new condition (the budget of 32 untouched).

   **The 3-square sum (lead's call).** It decides which parts of a move
   count:
   - **Order.** A hit's forced-movement parts are listed in data order:
     non-verb parts first (Press or Ram, Drive Through's +1 and other
     class and talent parts), then verb parts (the weapon's verb and its
     Empower line). A verb part whose target was already verb-moved this
     round is dropped first, and is not a stop.
   - **Distance.** Each part takes its maximum, or a distance the player
     or scorer chooses where the part says "chosen".
   - **Truncation.** The total is cut to 3 by trimming from the last
     part. A part **contributes** if it keeps at least 1 square after
     the cut. Only contributing parts count for collision eligibility,
     Battering Force's "only verb-sourced parts" and the
     verb-moved-this-round mark.
   - **Brink (a)** (Wrecker) then sets the combined distance anywhere
     from 1 to the cut total, trimming from the last part again.
   - **Collisions.** A move collides if any non-verb part contributes;
     Battering Force (8.3) is the one verb-only exception.

   **`sharesToppleCount` values (lead's call):** Topple (and Upend), Ram,
   Skittles, Drive Through's Prone, Earthshaker's Prone and Shield
   Craft's Topple are all `true`. A source that would knock Prone a unit
   whose count is spent this round is greyed in the preview. **A paid or
   free Ram suppresses the weapon's Topple verb on that hit** (the
   preview says so), so the two never contest the slot.
7. **Hew cap (lead's call).** Hews per rest are at most the class-table
   pool (2, or 3 from level 5). Deep Reserves adds one use. That use is
   the one spent once the class-table pool's worth has been spent this
   rest, read from the pool's count (nothing derived is stored, 3.2; no
   Hew counter is needed, 13.1). It cannot pay for Hew, and a move it
   pays for deals no collision damage (it still sets Momentum when it
   pays for Press or Empower). Capacity
   offers on Techniques are excluded (`offerExcludes`, 7.11; 10.1). The
   cap is a **per-rest sub-limit on one option of a named pool**. Like
   the `restDefensiveTool` sub-limit, it is not a named pool and is not
   counted (VG-10).
8. **Validators** (classes 12.2; VG-11), each a data predicate:
   - `RankAddsNoDice`: Hew's die step is allow-listed;
   - `RankKeepsBounds`;
   - **`HewCeiling` (a static formula, lead's call).** Each rank line,
     free effect, collision payoff and Art per-use line has a
     **per-use damage**: the damage it adds to each hostile, summed over
     every hostile one use can damage, times its trigger rate. A
     collision counts both the moved unit and the unit it strikes when
     both are hostile. The trigger rate is `collisionRate` (400‰
     *(illustr.)*) for collision payoffs and 1000‰ otherwise, both held
     in the tuning file. A line passes if, at class levels 1, 4, 5 and
     10:
     - its per-use damage ≤ Hew's expected damage (die average × (1000
       + crit permille ÷ hit permille) ÷ 1000);
     - no single unit takes more from one use than Hew's expected
       damage.

     An Art is checked per target line: the damage one target's line
     adds, with its collision. The Art as a whole falls under
     `SurgeOptionBound` (7.3).

     The pass table is in 10.1;
   - `TechniquePickerSize`: every reachable known-option set, built from
     the option sources and `offerExcludes`, has at most 6 options, and
     the picker shows legal options only (6.8).
   - `TurnAllowanceNoDamage` (7.9), `SurgeOptionBound` (7.3) and
     `FlagNotCondition` (9.2) are stated as predicates where they are
     defined.
9. **Rank and dip guards.** Every rank, Hew's die step and every
   subclass feature read **class level** (`scalesBy: classLevel`,
   `minClassLevel`) **(settled shape, classes 7.7)**.

### 6.5 Base options

**(lead's call)**

| Option | Wave | Rank I (when learned) | Rank II | Tags | effectKinds |
|---|---|---|---|---|---|
| **Press** | M1 | Push the target 1 square. Not verb-sourced, so the combined move can collide (weapons 7.2 rule 4). Normal size limit | (L5) Push up to 2, distance chosen | position | `forcedMovement.push`, `collision.enable` |
| **Hew** | M1 | +1d4 *(illustr.)*: a separate part of the weapon's type, doubled on a crit, no rider | (L5) +1d6 (the settled stage-1 size) | damage | `damage.extraDice` |
| **Empower** | M1 (M1 lines) / M2 | Apply this weapon's Empower line (6.6). Greyed when no line fires | (L5) The line's II text | per line | `verb.empower` |
| **Bite Deep** | M2 | The weapon damage part gains the rider flag (ability 9.3 order; one rider per target) | (L8) The rider's save has disadvantage | control | `rider.flag`, `save.disadvantage` |
| **Low Cut** | M2 (L2 talent) | The target makes a Fortitude save or is Staggered (`turns:1`). Save slot; own turn only. On a bash, the W-9 Technique Stagger source (settled) | (L8) Disadvantage if the target is Prone or Squared Up by you | control | `condition.apply:staggered` |

**Press is replaced by Ram for the Wrecker** (8.3). The picker never
grows from the subclass pick. **For a Wrecker, Ram stands in for Press
in every rule that names Press (lead's call):**
- Momentum: a paid Ram sets it, and a cash-in may take Ram (6.7);
- Pivot: Ram may pull or shift instead (7.6);
- Drive Through's trigger (7.6);
- `onGraze` (7.11);
- the Square Up II pairing list (8.2). This only matters for a companion
  kit or a later multiclass, because a Wrecker is never a Bastion.

A free Ram passes `HewCeiling`: it adds the same collision as Press, and
its Prone deals no damage (10.1). The level-3 card for a Wrecker that
owns Pivot says "Pivot now shapes Ram".

**Low Cut and Cover Step are mutually exclusive (lead's call):** the
`technique` and `guard` lanes never offer Cover Step to a Vanguard who
knows Low Cut, and never offer Low Cut to one who knows Cover Step
(`offerExcludes`, 7.11). So no build knows more than 6 options (6.8).

### 6.6 The canonical Empower table

**(lead's call)**

- **Every Empower effect is verb-sourced** (a clarification to weapons
  7.2 rule 4, VG-8). The collision rule is the one in 6.4 rule 6: any
  move in which a non-verb part contributes collides, and Battering Force
  is the one verb-only exception.
- Each line is its own record keyed by verb id, `{verbId, wave, ranks[],
  tag, payable}` (13.1), because `payable` and the tag vary per line.
- **No line adds dice or flat damage.** That keeps the table under
  `HewCeiling` at Hew 1d4.
- `payable` means a free allowance may pay for the line.

| Verb | Wave | Rank I | Rank II (L5) | Tag | Payable |
|---|---|---|---|---|---|
| Push (and the bash's Push) | M1 | Push up to 2 (chosen) | Push up to 3 (chosen) | position | yes |
| Topple | M1 | The save has disadvantage (settled example, ability 6.6) | On a failed save it is also pushed 1 (Prone at step 9; the push joins the step-10 combined move as a verb part). Dropped when Upend is used, since Upend moves Topple's save after the movement (8.3) | control | yes |
| Cleave | M1 | The follow-up may target any hostile adjacent to **you** | The follow-up ignores the target's shield bonus | geometry / accuracy | yes |
| Skewer | M1 | The line passes over one ally square | — (capped at I: the second unit gets no verb, weapons 7.6, and Skewer's geometry is read after step 10) | geometry | yes |
| Nick | M1 | The Nick throw takes no adjacent disadvantage | — (capped at I) | geometry | yes |
| Lunge | M1 | The target makes no opportunity attack against you this turn | — (capped at I: roll-mode overlap) | guard | yes |
| Pry | M1 | Your other attacks on this target this turn (the bash, an Arsenal swap) also have Pry. Narrowed in the revision so that it lives inside one turn's pipeline (`FlagNotCondition`, 9.2) | — | accuracy | yes |
| Parry (on any hit with the Parry weapon) | M1 | Until your next turn starts, your Parry may protect an adjacent ally against a melee attack roll, inside that roll's one window | — | guard | yes, pending the registry-scope ruling (VG-18) |
| Set (on the Set hit, under the reaction policy) | M1 | Push the mover 1 out of your zone | Push up to 2 | position | yes |
| Graze | M1 | **No line.** Graze carries "no rider, no verb" (weapons 7.1); the `onGraze` trigger covers it | — | — | — |
| Aim | M1 | **No line** (Exploit reads Dexterity) | — | — | — |
| Hook | M2 | Pull up to 2, or shift up to 3 | The target makes no opportunity attack against you until the end of your turn | position / guard | yes |
| Vex | M2 | Fires even when the attack had advantage | The Helped may go to an ally adjacent to the target | tempo | yes |
| Flourish | M2 | The step provokes from no unit | — (capped at I: avoids the speed-change ruling) | guard | yes |
| Puncture | M2 | The doubled reduction also applies to your next attack on this target this turn | — | damage | **no** |

- A verb whose II cell is "—" keeps its I text at rank II. The level-up
  card says so.
- **Cut:** a ranged Parry line. It stacked with the Shieldwall solo
  clause on floor 1 and copied Deflect.

### 6.7 Momentum

**(agreed, Q2: ships in M1; text lead's call)**

> **Momentum.** When you spend a Technique on **Press or Empower** and
> the hit resolves, you gain Momentum against that target. You hold at
> most one; a new one replaces the old.
>
> **Cash in:** your next hit on that target, before the end of your
> next turn, may take Press or Empower at your rank at no cost. This is
> that hit's free effect. From level 3 in M2, it may instead take your
> subclass signature option.
>
> **Limits:**
> - A cash-in never grants Momentum and is never Hew.
> - Own turn only, for both setting it and cashing it in.
> - Momentum ends when you cash it in, when it expires, when you are
>   Downed or Incapacitated, or when combat ends.

**Clauses (lead's call; each gets a unit test):**
- **Set.** Only a *paid* Press or Empower on **your own turn** sets
  Momentum, never a free effect. A paid Press or Empower on a reaction
  attack sets nothing. For a Wrecker, Ram stands in for Press (6.5).
- **Cash-in window.** "Your next hit" means any of your own-turn hits on
  that target until Momentum is cashed in or expires. A hit that does
  not cash it in leaves it in place: one that takes Practised Hand, pays
  for Hew or declines. Hits on other units' turns (an opportunity attack,
  a Retort Strike) neither spend it nor end it.
- **Paying again.** A paid Press or Empower on the Momentum target,
  instead of a cash-in, replaces it with a fresh Momentum and a fresh
  expiry.
- **What a cash-in takes:** payable lines only (Puncture is not
  payable). Hew is never taken. The subclass signature (from level 3 in
  M2) follows its own bounds and once-per-round limits.
- **Which hits count.** A Cleave follow-up that hits the Momentum target
  is a hit on that target and may cash it in. A Skewer second unit is
  not "hit" for this rule (it reuses the step-4 roll and takes no verb).
  Sure Form's miss is not a hit (8.1).
- **Expiry.** Momentum is cleared at the `turnEnd` event of your next
  turn (`expiresAtOwnerTurnEndIndex`, 13.1). Because it is set only on
  your own turn, "before the end of your next turn" always means the
  end of your following turn.
- **The target leaves (lead's call).** Momentum also ends when its
  target dies, is Downed or leaves the fight, so the glint never points
  at nothing. "Leaves the fight" means it is removed from the combat
  roster (fled or despawned).

- **Loop guards.** Cash-ins per rest are at most the Techniques you
  spend, so the pool bounds the combo.
- **M1 scope (lead's call).** In M1 the cash-in menu is Press and the
  M1 Empower lines only; the signature alternative arrives with the M2
  signatures. A Practised Hand Empower is free, so it never sets
  Momentum.
- **Practised Hand at levels 3–4 (lead's call).** With one attack a
  turn, a Weaponmaster's Momentum cash-in and Practised Hand compete for
  the same hit, because a hit takes one free effect (6.4 rule 3). Taking
  one forfeits the other for that round. So Momentum adds little to the
  M1 Weaponmaster before Extra Attack, and the reports say so (10.3;
  13.3). The agreed rule is not changed.
- **State.** It sits on the Vanguard as `momentum {targetId,
  expiresAtOwnerTurnEndIndex}`. It changes nothing about the target. It
  passes `FlagNotCondition` (9.2) and shows as a glint plus a row badge,
  counting 0 to read.
- **AI.** The party scorer prices a paid Press or Empower with a fixed
  Momentum term *(illustr.)*: the value of one free option times the
  conversion rate (about 55% at levels 1–4, 85–95% from level 5). The
  rate is about 0 when another free effect already covers your next hit
  on that target, as with Practised Hand at levels 3–4. It needs no
  lookahead search. Enemies see the glint; nothing reads class.
- **Merged.** Follow-Through, the momentum waiver from the systems
  draft, is merged into Momentum, so the class has one combo flag.

### 6.8 Full Technique catalogue by level and wave

| Option | Source | Earliest | Wave | Ranks (levels) |
|---|---|---|---|---|
| Press | base | 1 | M1 | I 1, II 5 |
| Hew | base | 1 | M1 | I 1 (1d4), II 5 (1d6) |
| Empower | base | 1 | M1 lines M1; M2 lines M2 | I 1, II 5 |
| Momentum cash-in | base (flag) | 1 | M1 (agreed, Q2) | — |
| Bite Deep | base | 1 | M2 | I 1, II 8 |
| Low Cut | L2 talent (M2 menu); offer (not with Cover Step) | 2 | M2 | I, II 8 |
| Borrowed Form | Weaponmaster | 3 | M2 | I 3, II 5, III 7 |
| Square Up | Bastion | 3 | M2 | I 3, II 5, III 7 |
| Ram (replaces Press) | Wrecker | 3 | M2 | I 3, II 5, III 7 |
| Cover Step | offer (guard lane, Bastion-weighted; not with Low Cut) | 5 | M2 | I only |

**Cover Step:** an ally adjacent to you or to the target shifts 1 square
around you. It provokes from no unit, never enters a hazard, and is
forced movement on the ally, not a created action.

At most 6 options can be known by any build: 3 base, Bite Deep, Low Cut
**or** Cover Step (never both, by the exclusion in 6.5), and the
signature. A Wrecker knows at most 5, because Ram replaces Press. The
`TechniquePickerSize` validator checks every reachable option set, so
the claim is checked, not assumed (6.4 rule 8).

## 7. The core kit

### 7.1 Chassis

**(settled, classes 8.3, unchanged; the last rows are lead's calls)**

| Field | Value |
|---|---|
| Main / suggested second | Strength / Constitution |
| HP tier, save | Sturdy 8; Fortitude |
| Armour / shield / weapons | heavy ceiling; shield allowed; simple + martial |
| Skills | 2 of Athletics, Intimidation, Medicine, Perception, Lore |
| Complexity | Simple |
| Things to read | **4 at every level:** at-wills; Second Wind; Techniques; Surge (Stand Fast is shown on the Surge row) |
| Named pools | 3 martial + capstone = **4 limited abilities by level 10** (cap room 4). Turn allowances do not count (7.9) |
| `fallbackDamageTypes` | bludgeoning (warhammer, bash); slashing (greataxe) |
| `level1MechanicIds` | techniques, holdTheLine, shieldwall, secondWindFull, surge, momentum |
| `signatureVerb` | techniques |
| `signatureEvents` | a Technique option applied (paid or free); a Momentum cash-in; a Hold the Line stop; an enemy rerouting around your zone (defined in 7.4); a Shieldwall Interpose; a Stand Fast block |
| `seedTarget` | anyEnemyInMelee |
| `leans[]` | **The stage-1 launch leans, unchanged (settled, classes 6.4):** protection P; single-target damage s; damage types and riders s; reach, zones and opportunity attacks s; reactions s; own tempo (Surge); forced movement and terrain as the Wrecker's subclass lens. The subclasses deepen leans the class already holds: the Weaponmaster damage types and riders, the Bastion protection and reach and zones, the Wrecker forced movement. `SubclassCopiesBaseLean` compares mechanic ids, not utility rows (classes 7.3), so this passes. `LeanSpread` is counted on these rows. Card wording: "protection first; damage through position" |

**At-wills:**
1. A weapon attack with the active main hand and its verb.
2. With a shield active, the shield bash (1d4 B, Push, verb-sourced,
   replaces one attack). With the two-hander, Shove (universal; push 1
   or Prone, VG-9).

### 7.2 Second Wind (`pool.secondWind`, rest)

**(settled, classes 8.2, 8.3; DH 8, 12.1)**

- **Cost and effect:** bonus action, own turn; regain your Recovery
  (Medicine bonus excluded).
- **Pool:** 1, or 2 from level 6.
- **Tags:** `restDefensiveTool`, `chassisOnly`. It is **the one
  full-Recovery rest tool**.
- **Guards (lead's call):** no offer may raise its capacity or change
  its cost (`offerExcludes`). Grim Resolve and Stubborn Breath modify it.

### 7.3 Surge (`pool.surge`, camp)

**(lead's call; cost settled, ability 6.4)**

- **Cost:** `none`, own turn, when no ability is resolving. It uses the
  turn's camp spend.
- **Pool:** 1, or **2 from level 5** (VG-6).
- **Base option:** one extra action of any kind.
- **Options from class level 5 (M2):** the subclass Art (8.1–8.3). An
  Art never combines with the extra action.
- **Denial:** greyed while any condition denies your action (Stunned,
  Incapacitated, a Dazed that took your action).
- **`SurgeOptionBound`** (new; VG-11), a data predicate over the Art
  record:
  - **expected damage ≤ 1200‰** *(illustr.)* of one Attack action at
    that level. It is computed by a fixed formula against the
    **reference encounter** in the tuning file: the weapons 6.4 hit and
    crit rates, 3 adjacent hostiles, and `collisionRate` (10.1). It is
    not a simulator result. The simulator reports the observed value
    beside it;
  - **tempo clause:** the record's `actionsGranted` is at most 1; option
    effects ride on that action;
  - areas ≤ 25 squares, `selective` only;
  - no hard-control `condition.apply` kind;
  - forced movement ≤ 3 per unit;
  - M1/M2 conditions only.
- **Nova bound:** reported with Arts included and without them.

### 7.4 Hold the Line (stance, M1)

**(settled shape, classes 8.3, CL-6; clarifications lead's call)**

- **Text.** When a reaction attack keyed to one of these triggers hits
  (an opportunity attack on `leaveZone` or `slipPast`, or your Set on
  `setHit`), that hostile's remaining movement this turn becomes 0:
  `{leaveZone, slipPast, setHit}`, where `slipPast` needs the Bastion
  clause or `slipPastProvokes`, and `setHit` needs the Bastion clause or
  `onSetHit`. The keying is a clarification **(lead's call)**:
  - **`leaveZone`:** a hostile leaving your threatened zone by its own
    movement (weapons 8).
  - **`slipPast`** (the Bastion clause, 8.2, and the `slipPastProvokes`
    trigger): after a successful slip-past check (ability 7.2), the
    trigger resolves before the step that moves the hostile through or
    past your square, and it spends your reaction as an opportunity
    attack. The hostile is still on its last square before that step.
  - **`setHit`:** your Set hit (8.2; `onSetHit`).
- **What it does not change:** speed; it is not a condition; action and
  bonus action stay; later Dash or Spurred movement adds as normal;
  forced movement is unaffected.
- **Disengage** prevents the opportunity attack as normal (weapons 8),
  except where Challenged-type goads say leaving provokes anyway
  (conditions 18.8). An earlier draft line, "Disengage stops it", is
  **not adopted**, because it contradicts 18.8.
- **Policy:** rides the opportunity attack, `always`, `nonMinion`
  (`notWhileSquaredUpElsewhere` for a Bastion with an active Square Up,
  7.12). Pool-free; counts 0.
- **AI precondition (VG-16).** The enemies 11.1 opportunity-cost term
  prices *known* reaction triggers as state, the stop included, as
  Interpose is previewed. This ships in M1, so monsters neither walk
  into the stop blind nor ignore it.
- **Visibility:**
  - threatened squares carry a Hold the Line marker in the movement
    preview and the enemy-intent display;
  - an enemy rerouting around your zone is a signature event. **A
    reroute (lead's call)** is a move in which the planner's cheapest
    path, with the Hold the Line term priced at 0, would have crossed one
    of your triggers above, and the chosen path does not;
  - `Floor1ClassSpread` prints the Hold the Line fire rate and reroute
    rate per fight.
- **Pushing a foe out of your zone (lead's call).** A pushed unit never
  provokes, so a push can release a foe toward the back line. The attack
  preview and the party scorer mark "releases from your zone" as a cost
  when a companion is within the pushed foe's reach or speed after the
  push.

### 7.5 Shieldwall (stance, M2) and the stance re-pick

**Shieldwall (VG-3; lead's call):**

- **Requires** a shield in the active set.
- **Ally clause (settled):** Interpose +2 for an adjacent ally against
  one attack roll.
  - It spends your reaction.
  - Pry ignores it; Staggered switches it off.
  - Policy `ifItChangesOutcome`, `nonMinion`.
  - It is learned, so Guarding is illegal for you (CL-7).
- **Solo clause:** while no ally is within 2 squares of you, it may
  instead cover **you** against a **ranged weapon** attack roll.
  - It is the target's own pool-free candidate (armour 10.4).
  - Envelope at level 1: 18 + 2 = 20 against +4, a 25% hit chance; with
    Raise, 22, exactly 15%. Pass.
  - **Registry (VG-18):** checked against Deflect, Hailguard and Shelter
    under trinkets 11 rule 1. If refused, the pick is labelled "for
    parties" and Hold the Line stays the default.
- **Card text:** "covers you against ranged attacks while alone".

**Stances are learned, never toggled (lead's call).** A stance has no
on/off state and is never in `activeToggles` **(settled, classes 8.3)**.

**The camp re-pick (agreed, Q3; VG-17).** Once per camp, the Vanguard
may replace its stance with the other one.

- **Where and when (lead's call):** on the camp screen only, as part of
  the full rest, once per camp visit. Never in combat and never at a
  short rest. It costs nothing and is undone only by the next camp.
- **Waves:** M1 has one stance, so the re-pick is not shown; it arrives
  with Shieldwall in M2.
- **Guarding (lead's call on the armour row, VG-17):** a Guarding shield
  carried by a Vanguard who re-picks Shieldwall becomes runtime-inert,
  with a warning on the camp screen before the re-pick is confirmed; it
  wakes again if the stance returns to Hold the Line.
- **Stance-bound triggers (lead's call):** a trigger whose record has
  `requires: stance:<id>` (`onSetHit`, `slipPastProvokes`,
  `pushOnTurnedMiss`) goes dormant while that stance is not live, and
  wakes when it returns. The camp-screen warning lists every trigger
  that the re-pick would put to sleep. The level-7 draw offers a
  stance-bound trigger only for a stance that is live, and with Stance
  Drill both are.
- **With Stance Drill (lead's call):** both stances are live, so the
  re-pick is hidden.
- **Companions:** a Vanguard companion kit's stance is fixed by its kit
  (the companions design); the re-pick is the hero's.

### 7.6 Talents

Menus of 3; each menu has an upgrade **(settled shape, classes 7.4,
14)**. Texts are lead's calls.

**Level 2, M1 (M1 conditions only):**
- **Drive Through** (adds; D9 fix). Once per round, when the hit's
  combined move includes a Press, Ram or Empower push part, you may also
  Shove the target as part of that hit, at step 10, **before** the
  movement.
  - The Shove rolls Athletics against 10 + its Fortitude; size limits
    apply.
  - On a success, choose Prone (tag `shove`; `sharesToppleCount: true`)
    or +1 square to the hit's push. The extra square is a non-verb part
    in the 3-square sum (6.4 rule 6), so the move can collide.
  - **Own turn only for the Prone choice** (6.4 rule 5); on a reaction
    attack only the +1 square may be chosen.
  - It costs no Technique and replaces no attack. It is **not** a Shove
    for `onShoveSuccess` (7.11), because the hit has already taken its
    one Technique (6.4 rule 2) **(lead's call)**.
- **Pivot** (upgrade). Your Press (for a Wrecker, your Ram) may instead
  pull up to its distance, or shift the target 1 square around you (2
  from rank II). A shift and a push cannot combine (weapons 7.2 rule 4).
  Shifts never collide, so a shifted Ram makes no Prone save; a pulled
  Ram keeps its save if the pull is stopped early.
- **Grim Resolve** (upgrade; D10 fix). Second Wind also ends one of
  Frightened, Dazed or **Bleeding** on you, and you stand from Prone at
  no movement cost. Bleeding comes from the M1 claw brute. The stand
  folds in the core's `standOnBreath` trigger.

**Level 2, M2:** Drive Through, Pivot, **Low Cut** (adds). Grim Resolve
moves to level 7.

**Level 7, M2 (a trigger option is required, settled):**
- **Grim Resolve II** (upgrade): as level 2, and it may end Staggered
  instead.
- **Stance Drill** (adds): learn the other stance. Both are live and
  share one reaction. The card warns that Shieldwall makes Guarding
  illegal.
- **One trigger option** from `triggerMenu` (7.11). The draw excludes
  owned triggers, triggers barred for your subclass, and triggers for
  stances you lack.

**Level 10 (if the talent source is picked):**
- **Deep Reserves** (upgrade): Techniques +1. That use cannot pay for
  Hew, and a move it pays for deals no collision damage (6.4 rule 7).
- **Stubborn Breath** (upgrade; a cost change ability 7.1 allows).
  Second Wind may instead be your reaction when damage leaves you at or
  below half your maximum HP and above 0. Policy `always`. It spends the
  round's reaction. Registry: Reprieve Phial, Hungering.
- **Sound the Advance** (adds; M2): when you Surge, one ally you can see
  within 6 squares gains Spurred. A class source is allowed (CL-5). It
  is a token, never a created action.

### 7.7 Capstone: Stand Fast

`pool.standFast`, camp 1, `minClassLevel` 9 **(settled shape, ability
6.7; text lead's call; VG-4, VG-15)**.

- **Cost:** bonus action plus the turn's camp spend, so never on a Surge
  turn. Requires a shield in the active set, **checked at use and again
  at each roll**: after a swap to the two-hander set the fill does not
  apply until the shield set is active again **(lead's call)**.
- **Effect:** until the start of your next turn, **every** attack roll
  against an ally adjacent to you takes Interpose's +2 as a **window
  fill**, with no reaction spent. Adjacency is read at each roll.
- **Window rules:**
  - armour 10.4 gains **step 0** (window fills, ahead of the target's
    own candidates);
  - a fill closes that roll's window;
  - **inherited skip rules, stated in full (lead's call):** Pry ignores
    it; it is not offered after a gear-sourced disadvantage; it is
    suppressed while you (the source) are Staggered or Incapacitated.
    The 10.4 skip "its unit has no reaction left" **does not apply**,
    because a fill spends no reaction;
  - **several step-0 fills on one roll** (Stand Fast, Step In II, or two
    units' fills) are ordered by source unit order, then by creation
    order. The first applies and closes the window, and it gets the
    "blocked" event. An ally under any fill counts as **covered** for
    the subclass riders below;
  - never covers you (except under the solo clause below).
- **Display.** The fill is shown in the reaction term of the resolve
  preview ("AC +2, Stand Fast"), like Parry's `previewHint`. It is
  **not** in visible AC (armour 6.1 unchanged). The AI reads it through
  the resolve preview. Every turned miss shows "Stand Fast: blocked" and
  is a signature event.
- **Solo clause.** If no ally is within your Stand Fast range when you
  use it (adjacent, or within 2 squares for a Bastion), it covers **you**
  against ranged weapon attack rolls this round, still one window. So a
  Bastion with an ally 2 squares away covers the ally and is not solo.
  Registry against Raise and Deflect; if refused, the clause is cut.
- **Automatic subclass rider at level 9:**
  - **Bastion:** covers allies within 2 squares instead of adjacent.
  - **Wrecker:** an attacker adjacent to you whose melee attack misses a
    covered ally is pushed 1 square, not verb-sourced, once per attacker
    per Stand Fast (the use's `riderPushed[]` set, 13.1). Registry:
    Binding Parry; barred together with `pushOnTurnedMiss`.
  - **Weaponmaster:** your first hit, before the end of your next turn,
    on an attacker that missed a covered ally takes its payable Empower
    line free (that hit's free effect), **and also fires your spare
    set's verb under Twin Verb's list and bounds** when that verb is on
    the list, outside the slot (6.4 rule 3) **(lead's call, so that the
    rider does what Practised Hand cannot)**. With the default kit's
    greataxe spare it fires nothing extra; 7.10 gives the recommended
    spare. Eligible attackers are held in the use's
    `riderEligible[]` (13.1). The double fire is logged as a signature
    event.
- **Envelope at level 9:** 20 + Raise 2 + fill 2 = 24 against +9, a 30%
  hit chance, at least the required 15%. Pass, including the Bastion's
  reach 2 (same bonus, more allies). The reliance report is rerun.
- **Nova:** no damage. Pass.

### 7.8 Pools by level *(illustr.)*

| Pool (refill) | L1 | L2 | L3 | L4 | L5 | L6 | L7 | L8 | L9 | L10 |
|---|---|---|---|---|---|---|---|---|---|---|
| Second Wind (`rest`) | 1 | 1 | 1 | 1 | 1 | **2** | 2 | 2 | 2 | 2 |
| Techniques (`rest`) | 2 | 2 | 2 | 2 | **3** | 3 | 3 | 3 | 3 | 3 (+1 Deep Reserves: non-Hew, no collision damage) |
| Hew-use cap per rest | 2 | 2 | 2 | 2 | 3 | 3 | 3 | 3 | 3 | 3 |
| Surge (`camp`) | 1 | 1 | 1 | 1 | **2** | 2 | 2 | 2 | 2 | 2 |
| Stand Fast (`camp`) | – | – | – | – | – | – | – | – | **1** | 1 |

Second Wind's steps are settled; the rest are lead's calls (VG-6).

### 7.9 Turn allowances

**(lead's call; VG-10)** Two record kinds keep per-round limits out of
the pool counts.

**`turnAllowance`** (owner-scoped, refill `turn`: a `turn` pool of 1 on
its owner, refilled at the owner's turn start, classes 3.4):
- Practised Hand (Sure Form and Borrowed Form III spend it);
- Twin Verb;
- Battering Force;
- the once-per-round Square Up (Called Out and Step In spend it);
- Drive Through;
- `naturalHighFree`;
- `surge.ramFree` (refilled by a Surge, spent by the first Ram that
  turn);
- `retortStrike.wardensReach`.

**`perTargetRoundCount`** (keyed by target, reset at the start of the
round):
- Topple's count, shared by every source with `sharesToppleCount: true`
  (6.4 rule 6);
- the verb-moved-this-round mark (weapons 7.2 rule 4).

**Per-use sets** sit on the use that owns them, not on either kind: the
Stand Fast riders' `riderEligible[]` and `riderPushed[]` (13.1).

A turn allowance or per-target count:
- is not a named pool and is outside the cap of eight and the
  three-pool limit;
- counts 0 to read when shown as a glint;
- stays outside the camp-share ratio, and inside the 60% formula as a
  level-3 or level-6 increment.

**Priority (lead's call):** turn-start refills run at a higher priority
than `onEvent:turnStart` spends (ability 14.4), so Called Out spends the
fresh Square Up allowance.

**`TurnAllowanceNoDamage`** (new validator, a data predicate): the
effects a turn allowance grants carry no `damage.*` effectKind, except
allow-listed ids. The allow-list holds one id, Battering Force's
`collision.damage`, which `HewCeiling` bounds (10.1). Verbs, Shoves and
conditions that an allowance fires (Twin Verb, Drive Through, Square Up)
are allowed, because they carry no `damage.*` kind.

### 7.10 Starting kit

*(illustr.; lead's call; VG-21 against armour 13.6's illustration)*

- **Armour:** chain mail (AC 16; Str +1 met; Ringed answers the claw
  brute's Bleeding).
- **Set A (active):** warhammer (1d8 B, versatile, Push) + shield (+2;
  bash 1d4 B Push). AC 18.
- **Set B:** greataxe (1d12 S, two-handed, heavy, Cleave). AC 16.
- **Belt:** 2 healing potions **(settled, DH 12.4)**. **Coin:** about 15.
- **Why:**
  - two physical types;
  - an on-hit, payable line in each set (Push, Cleave geometry), so
    Practised Hand fires on hits in either set (42–53% signature share
    at level 3, M1);
  - it narrows the floor-1 spread: warhammer + shield is the kit that
    dies most, 15.1% at level 2 (10.3).
- **What the kit does not do for the M2 Weaponmaster (stated plainly,
  lead's call).** With the shield set active, the spare verb is the
  greataxe's Cleave, which Borrowed Form and Twin Verb cannot take. They
  work only with the greataxe active (borrowing the warhammer's Push),
  which costs AC 2 and the shield, so Stand Fast and its rider are off in
  that set. No M1 slashing weapon has a borrowable verb.
  - **The fix is a recommendation, not a kit change:** at level 3 the
    camp screen suggests a one-handed spare main hand with a borrowable
    verb, carried with the shield: a sabre (Flourish, slashing) or a
    sickle (Hook, slashing), both M2. A mace (Topple, bludgeoning) works
    for Borrowed Form only, because Twin Verb excludes Topple.
  - With such a spare as set B (replacing the greataxe set, and keeping
    a slashing type), both sets keep AC 18 and the shield, so the
    Weaponmaster's verb craft and its protection live in the same set.
  - The M2 signature-share figure for Borrowed Form is owed for the set
    the player fights in (shield set with a borrowable spare, and
    greataxe active), not assumed from the M1 figure (13.3).
- **No kit presets at creation.** A second level-1 pick would break "one
  choice per level" (classes 7.4; ability 10.3).

### 7.11 Offers, lanes and `triggerMenu`

**`offerLanes` *(illustr.)*:**

| Lane | Weight | Carries |
|---|---|---|
| `technique` | 1500‰ | Low Cut / Cover Step option offers (mutually exclusive, 6.5); trigger offers |
| `guard` | 1500‰ | trigger offers on Hold the Line, Shieldwall, Stand Fast; Cover Step |
| `might` | 1200‰ | Athletics or Intimidation mastery; a physical type that ignores resistance |

- **New subclass field `offerLaneAdjust`** (VG-14): ×1333‰ on the
  Weaponmaster's `technique`, the Bastion's `guard` and the Wrecker's
  `might`.
- **`offerExcludes`:**

  | Excluded | Why |
  |---|---|
  | `capacity:secondWind` | Breaks "once per rest, twice from level 6" (settled, classes 8.2) |
  | `cost:*` | No action-cost limited ability to cheapen |
  | `timescale:surge`, `timescale:standFast` | Would gut the camp share and the nova bound; ability 10.2 bars only the *last* camp pool, and Stand Fast exists from level 9 |
  | `capacity:surge`, `capacity:standFast` | Camp damage sits outside the parity row (VG-12), so it cannot also be sold as capacity; ability 6.7 fixes the capstone at 1 |
  | `capacity:techniques` | **(lead's call, revision)** Each extra use adds about 1.2–2.5 parity points at level 10 through collisions and Momentum, so capacity offers would push the worst legal build past 110% (10.1). Deep Reserves is the one capacity step, and its use deals no damage |
  | `option:coverStep` when Low Cut is known; `option:lowCut` when Cover Step is known | `TechniquePickerSize` (6.5, 6.8) |
  | `shape`, concentration training | Dead for the Vanguard |
  | `verbTraining`, armour and shield training | Already trained or at the ceiling |

**`triggerMenu`** (ids *illustr.*; never repeated; legal at any level):

| Ability | Trigger | Text | Notes |
|---|---|---|---|
| techniques | `onShoveSuccess` | Spend a non-damage option when your Shove succeeds; the pushes sum to ≤ 3 | M2; needs `onEvent` timing (VG-7) |
| techniques | `onGrappleSuccess` | Spend Square Up or Low Cut when your grapple succeeds | M2; part-pays the weapons 17.11 grapple debt |
| techniques | `onGraze` | Spend Press (Ram for a Wrecker) when your Graze fires | M2 |
| techniques | `naturalHighFree` | A natural 19–20 hit's non-damage option costs nothing; once per round | M2; the one free-cost trigger |
| holdTheLine | `onSetHit` | The stop also applies when your Set hits | `barredFor: bastion`; `requires: stance:holdTheLine` |
| holdTheLine | `slipPastProvokes` | A hostile that slips past you still provokes you (the `slipPast` trigger, 7.4) | `barredFor: bastion` (built in); `requires: stance:holdTheLine` |
| shieldwall | `pushOnTurnedMiss` | When your Interpose turns a hit into a miss, push the attacker 1, not verb-sourced | Registry: Binding Parry; barred with the Wrecker's rider; `requires: stance:shieldwall` |
| surge | `breakFree` | When you Surge, end Grappled on you (M2: also Restrained) | — |
| standFast | `twoRounds` | Lasts until the end of your next turn | Envelope rerun |
| Subclass triggers | — | See 8.1–8.3 | `requires: subclass:<id>` |

### 7.12 Default reaction policies

Each is changeable to any policy or "ask me" **(settled principle,
ability 7.3; defaults lead's call)**.

| Reaction | Default |
|---|---|
| Opportunity attack (carries Hold the Line) | `always`, `nonMinion`. For a Bastion with an active Square Up: `notWhileSquaredUpElsewhere`, `nonMinion` (8.2) |
| Shieldwall (ally and solo) | `ifItChangesOutcome`, `nonMinion` |
| Parry (if wielded) | `ifItChangesOutcome`, `nonMinion` (canonical) |
| Techniques on reaction attacks | `never` |
| Practised Hand on a Set hit | `always`, `nonMinion` |
| Practised Hand on an opportunity-attack Push | `never` (it would undo the Hold the Line stop) |
| Retort Strike | `always`, `nonMinion` |
| Stubborn Breath | `always` (the threshold is in the trigger) |
| Stand Fast | not a reaction (automatic fill) |

- **New policy filter `notWhileSquaredUpElsewhere`** (VG-11; ability
  7.3) **(lead's call)**. While you have an active Square Up, the
  opportunity attack is not taken against a unit other than the
  Squared-Up one. Taking it would end your own Square Up, because
  Challenged ends when its challenger targets another unit (conditions
  18.8). The policy may be changed to `always` or "ask me".
- **Own-turn automatic spends** sit outside the reaction policies,
  because they are not reactions. **Called Out** (8.2) is one: the hero
  is prompted at turn start. It has no companion default, since
  companion kits carry no subclass (classes 7.8).
- The draft filter `allyVisibleAcBelowMine` is cut: no reaction used
  it.

### 7.13 `effectKinds` for the registry

| Feature | effectKinds | Subject | Trigger |
|---|---|---|---|
| Second Wind / Stubborn Breath | `heal.recovery` | self | bonus action / reaction at ≤ half HP |
| Grim Resolve | `condition.end{frightened,dazed,bleeding,staggered}`, `stand.free` | self | on Second Wind |
| Press | `forcedMovement.push`, `collision.enable` | hit target | onHit |
| Hew | `damage.extraDice` | hit target | onHit |
| Empower | `verb.empower` (per-line sub-kinds) | per line | onHit / Set hit |
| Bite Deep | `rider.flag`, `save.disadvantage` | hit target | onHit |
| Low Cut | `condition.apply:staggered` | hit target | onHit, own turn |
| Momentum | `momentum.load`, `cost.free.momentum` | self → target | paid Press, Ram or Empower on your turn / a later own-turn hit on that target |
| Drive Through | `shove.free` | pushed target | a combined move with a Press, Ram or Empower push part |
| Pivot | `forcedMovement.pull`, `forcedMovement.shift` | hit target | Press (Ram for a Wrecker) |
| Surge | `action.extra` | self | own turn |
| Hold the Line | `movement.remainingZero` | mover | a keyed hit: `leaveZone` or `slipPast` opportunity attack, `setHit` Set |
| Shieldwall | `reactionAc.ally`; `reactionAc.self.ranged` | ally / self | attack roll |
| Stand Fast | `reactionAc.windowFill.noReaction` | adjacent allies | each attack roll |
| Sound the Advance | `condition.apply:spurred` | ally within 6 | on Surge |
| Stance Drill | `stance.learn` | self | level-up |
| Stance re-pick | `stance.swap` | self | camp |
| Low Cut II | `save.disadvantage` | hit target | onHit, own turn, target Prone or Squared Up by you |
| Cover Step | `forcedMovement.shift.ally` | ally adjacent to you or the target | onHit |
| Deep Reserves | `capacity.techniques.nonDamage` | self | level-up |
| **Weaponmaster** | | | |
| Practised Hand | `cost.free.empower` | hit target | onHit / Set hit, once per round |
| Borrowed Form I–III | `verb.borrow`; II adds `verb.empower`; III adds `cost.free.empower` | hit target | onHit (Technique) |
| Twin Verb | `verb.borrow.extra` (control verbs) | hit target | onHit, once per round |
| Sure Form | `verb.onMiss` | missed target | qualifying miss, once per round |
| Arsenal I–II | `action.attack`, `weapon.swap.free`, `cost.free.empower`; II adds `verb.borrow.extra` | self → each target | Surge option |
| `practisedHand.payRider` | `cost.free.rider` | hit target | onHit |
| `twinVerb.offHand` | `verb.borrow.offHand` | hit target | onHit |
| `borrowedForm.onReaction` | `verb.borrow` (Push, Hook) | hit target | reaction-attack hit |
| Stand Fast rider (Weaponmaster) | `cost.free.empower`, `verb.borrow.extra` | attacker that missed a covered ally | your next hit on it |
| **Bastion** | | | |
| Square Up I | `condition.apply:challenged` | hit target | onHit, own turn |
| Square Up (no hit, Called Out and Step In only) | `condition.apply:challenged` | hostile in reach / adjacent | turn start / end of the Art's Dash |
| Square Up II (pairing) | `technique.pair` | hit target | onHit |
| Called Out (III) | `condition.apply:challenged`, `cost.timing.turnStart` | hostile in reach that attacked an ally | `onEvent:turnStart` |
| Shieldwall reach 2 / slip-past clause | `reactionAc.ally.reach2`; `opportunityAttack.trigger.slipPast`, `movement.remainingZero` | ally / mover | attack roll / slip past |
| Retort Strike | `reactionAttack.attackAgainstOther` | Squared-Up attacker | its attack roll against another unit |
| `retortStrike.wardensReach` | `reactionAttack.attackAgainstOther` | hostile in reach attacking an adjacent ally | once per round |
| Shield Craft | `verb.bash.choose{push,hook,topple}` | bash target | bash hit |
| Step In I–II | `action.dash`, `condition.apply:challenged`, `forcedMovement.swap.ally` (pending VG-22); II adds `reactionAc.windowFill.noReaction` | self / adjacent hostile / ally | Surge option |
| Stand Fast rider (Bastion) | `reactionAc.windowFill.reach2` | allies within 2 | each attack roll |
| **Wrecker** | | | |
| Battering Force | `collision.damage` | moved unit and the unit it strikes | verb-only move stopped early, once per round |
| Brink | `forcedMovement.distance.choose`; `condition.apply:teetering` | hostile moved by you | forced movement you cause |
| Ram I–III | `forcedMovement.push`, `collision.enable`, `condition.apply:prone` (deferred save); III (Skittles) adds `condition.apply:prone` on the struck hostile | hit target / struck hostile | onHit (Technique) |
| Domino | `forcedMovement.push.link` | blocking hostile | step 10b |
| Upend | `forcedMovement.push`, `condition.apply:prone` (no save if stopped early) | Topple target | Topple fires |
| Earthshaker I–II | `shove.multi`, `forcedMovement.push`, `condition.apply:prone` | up to 3 adjacent hostiles | Surge option |
| `surge.ramFree` | `cost.free.ram` | hit target | first Ram on a Surge turn |
| Stand Fast rider (Wrecker) | `forcedMovement.push` | adjacent melee attacker that missed a covered ally | turned miss, once per attacker per use |

**Registry pass, named first** (all under the scope ruling VG-18):
- Stand Fast against Raise, Guarding, Bulwark Stance and Warding Lining;
- Shieldwall solo against Deflect, Hailguard and Shelter;
- the Parry line against Warding Staff;
- Drive Through against Shove-through;
- `pushOnTurnedMiss` against Binding Parry;
- Stubborn Breath against Reprieve Phial and Hungering;
- Sound the Advance against Rally's Spurred.

### 7.14 Recommended creation defaults and ladder preview

**(lead's call; settled requirement, classes 7.4)**

| Choice | Default | Why or note |
|---|---|---|
| Array | Str +3, Con +2, Wis +1, Pre +1, Int 0, Dex −1 | Heavy armour ignores Dex; Wis feeds Medicine and Will |
| Skills | Athletics, Medicine | DH 8 calls Medicine "close to compulsory" on solo floor 1 |
| Stance | Hold the Line | Works solo; keeps Guarding legal |
| Level 2 | Drive Through (M1; M2 solo or with one companion). **Pivot in M2 when two or more companions are recruited at the level-up** | Pivot keeps foes inside your zone instead of pushing them toward the back line (7.4) **(lead's call)** |
| Level 3 | Weaponmaster | Fewest decisions; M1 |
| Level 6 | Twin Verb / Retort Strike / Domino with a shield active, otherwise Upend | — |
| Level 7 | Grim Resolve II | Stance Drill flagged as "the defender pick" |
| Level 10 | Deep Reserves | — |
| Offers | scorer suggestion | — |
| Camp re-pick | Hold the Line while solo; the camp screen suggests Shieldwall once two or more companions are recruited and a shield is in the active set | Lead's call |

**Ladder preview lines:**
- "Bastion shines with companions; alone, it catches runners."
- "Wrecker needs walls and hazards."
- "Weaponmaster: control, not damage; best with two different verbs."

## 8. The three subclasses

**(lead's call throughout; VG-2 replaces the stage-1 cards)** Shape
shared by all three:
- level 3: a signature Technique (I) and clauses;
- level 5: signature rank II and the Surge Art;
- level 6: a verb rewrite (1 of 2);
- level 7: signature rank III;
- level 9: the Stand Fast rider and Art rank II.

No subclass adds a pool or a row. `SubclassCopiesBaseLean` and
`SubclassAddsPool` pass for each.

### 8.1 WEAPONMASTER (M1): "the right tool, then the next one"

**Specs into:**
- verb depth (a free Empower every round);
- a two-set toolkit (borrow the spare set's verb);
- at level 6, breadth (two verbs on one hit) or reliability (verbs on a
  miss).

**Does not spec into:** damage numbers, damage-type swapping, or a
chosen weapon family (Chosen Arms is Later).

**Data:**
- Lean: deepens the class's damage-types-and-riders lean (7.1). Not
  control, or `LeanSpread` fails.
- `mechanicIds [freeEmpower, verbBorrow]`; M1: `[freeEmpower]`.
- Card amended to "keyed to weapon verb ids; group tags as `requires`
  filters" (VG-2).

**Level 3:**
- **Practised Hand** (M1):
  - Once per round (a turn allowance), on a hit whose verb (or bash
    Push) can fire, apply its **payable** Empower line without spending
    a Technique.
  - It is that hit's one free effect (6.4 rule 3).
  - Movement stays verb-sourced and never collides.
  - It inherits every verb bound and never fires on granted attacks.
  - Shown by a "Form ready" glint (counts 0).
  - In M1, on-hit and Set-hit lines only. The Parry-success and Graze
    timings are not needed with the default kit.
- **Borrowed Form** (signature, M2). Spend a Technique after a hit: the
  hit applies your **spare set's main-hand verb** instead of the active
  verb. It replaces only the active verb's effects from step 7 on;
  pre-roll effects already applied (Lunge's advantage, Aim's Exploit
  flag) stand **(lead's call)**.
  - **Borrowable:** Push, Hook, Topple, Vex, Flourish.
  - **Never borrowable:**
    - Cleave and Skewer (extra dice fail `HewCeiling`);
    - Nick, Lunge, Aim, Pry and Puncture (structure or pre-roll);
    - Parry, Set and Graze.
  - **Only the canonical verb travels.** Signatures, dice, type, reach
    and properties never do.
  - The verb is off if the spare weapon is untrained or would Strain
    you.
  - Needs VG-19 (weapons 7.3, verb access).
  - **II (L5):** the borrowed verb also takes its payable Empower line,
    for one spend.
  - **III (L7):** Practised Hand may pay for Borrowed Form.
- **Example with the default kit:** greataxe active, warhammer spare. A
  greataxe hit pushes 2. With the shield set active the default spare
  verb is Cleave, which never travels; 7.10 gives the recommended M2
  spare.

**Level 6 (choose 1; each changes a verb):**
- **Twin Verb:** once per round (turn allowance), a hit also fires your
  spare set's verb, at no cost and without occupying the slot.
  - **Push, Hook, Vex or Flourish only.** No Topple (a free Prone each
    round boosts the greatsword build), and no Cleave or Skewer.
  - All movement bounds apply; the damage type never changes.
  - "Both verbs" lives **only** here.
- **Sure Form:** once per round, when your melee attack misses and the
  miss was not caused by a reaction (Graze's test, weapons 7.6),
  Practised Hand may fire that weapon's verb as though it hit, dealing
  no damage.
  - **Pipeline (VG-8, new step 6a; lead's call):** on a qualifying miss,
    Graze resolves as normal, then only the verb's own effect runs, at
    its normal step: forced movement at step 10, a Cleave follow-up at
    step 12, Lunge's carried advantage at step 13. There is no damage
    step, no save slot (step 9) and no Empower line.
  - **The miss is not a hit for any rule:** not for Momentum, Hold the
    Line, Square Up or signature events other than "Sure Form fired".
  - Push and Hook move the target as written.
  - Cleave makes its follow-up against a hostile adjacent to the missed
    target, inside your reach.
  - Lunge carries its advantage to your next attack on that target this
    turn.
  - Topple is excluded (it copies Floorer).
  - It spends Practised Hand for the round. Parity: about +0.6 DPR with
    the greataxe *(illustr.)*.

**Surge Art: Arsenal** (M2, class level 5):
- Take the Attack action. Before each attack, choose which set's main
  weapon makes it; the swaps are part of the Art.
- Each hit fires its verb with its payable line free (that hit's free
  effect, 6.4 rule 3).
- **After the Art (lead's call):** the set used for the last attack stays
  active, with its AC and threatened zone. The preview shows the AC that
  results.
- The Art shows as one named sequence ("Form: Push, then Cleave").
- About 1000‰ of an Attack action *(illustr.)*. Pass.
- Registry: a camp-charged echo of Quickdraw's swap; no stacking with
  Quickdraw.
- **II (L9):** one hit in the sequence may also fire the other set's
  verb, under Twin Verb's list and bounds.

**Triggers (`requires: subclass:weaponmaster`):**
- `practisedHand.payRider` (M2 riders): Practised Hand may pay for
  Bite Deep.
- `twinVerb.offHand`: Twin Verb may borrow the off-hand weapon's verb.
- `borrowedForm.onReaction`: Push and Hook only.
- **Cut in the revision:** `practisedHand.changeOfHands`. A second
  Practised Hand in one round breaks the approved once per round (classes
  3.4, 8.3).

**Weaknesses:**
- the lowest ceiling with a greatsword (+0%);
- verb-dependent, so weak against bosses more than one size larger,
  Rooted or Anchored targets, and Aim;
- kit- and loot-dependent;
- leaving the shield set costs AC 2;
- one more decision per hit.

**Bound:** no Weaponmaster build may exceed the class's best
non-Weaponmaster build.

**Staging:**
- M1: Practised Hand on M1 on-hit lines.
- M2: Borrowed Form I–III, Arsenal, Twin Verb or Sure Form, M2 lines,
  triggers.
- Later: Chosen Arms.

### 8.2 BASTION (M2): "look at me"

**Specs into:**
- **mark:** Square Up I, II, III;
- **hold:** stance clauses;
- **shield:** reach 2, Step In, the Stand Fast reach rider;
- **punish:** Retort Strike.

It deepens leans the class already holds (7.1): protection (primary)
and reach, zones and opportunity attacks (secondary). `mechanicIds
[squareUp, interposeReach2, slipPastStop, stepIn, squareUpPair,
calledOut]`.

**Level 3:**
- **Square Up** (stage 1's Challenge, renamed).
  - A Technique option, own turn only, applies Challenged (M2 minor,
    `goad`) to the hit target. No save.
  - **Once per round across every source** (a turn allowance).
  - Challenged keeps its own end rules **(settled)**, read as approved:
    it ends when the challenger "**targets** another unit with a harmful
    effect" (conditions 18.8). So Cleave and Skewer secondary targets
    end it, and a third unit struck by a collision does not.
  - The classes 8.3 paraphrase "harms" is corrected to "targets" (VG-2).
  - **Step (lead's call):** Square Up has no save, so it applies at
    pipeline step 11 with the other on-hit effects (Low Cut and Bite
    Deep use the step-9 save slot). So a step-12 Cleave or Skewer secondary
    on another unit ends the Challenged that the same hit just applied,
    and the preview says so.
  - **The preview warns on every attack that would end it:** "ends your
    Square Up on X" is shown on a Cleave or Skewer secondary, on an
    attack on a second target (an Extra Attack, a Shield Craft bash), and
    on an opportunity attack against another hostile. **The Bastion's
    opportunity-attack policy defaults to `notWhileSquaredUpElsewhere`
    while a Square Up is active (7.12; lead's call)**, so Hold the Line
    does not end the mark by default.
- **Stance clause, either stance:**
  - **Shieldwall:** the ally clause reaches allies within 2 squares (the
    settled stage-1 clause).
  - **Hold the Line:** the stop also applies when your Set hits (the
    settled stage-1 clause), **and** a hostile that slips past you
    provokes you. This is the built-in `slipPastProvokes`, barred as a
    trigger.
  - The draft's Challenged-Disengage clause is **dropped**: it is
    already a base rule (D8).
  - Solo value: a Squared-Up kiter that Disengages is stopped by the
    base rules, and one that slips past is stopped by the clause.
- **Square Up with no hit (VG-2):** only **Called Out** and **Step In**
  may apply Square Up without a hit, listed by source id; no other
  Technique or Art may. It applies on your own turn to the hostile those
  texts name. If the round's Square Up allowance is already spent, Step
  In stays legal and its mark is skipped, with a preview warning; Called
  Out is greyed **(lead's call)**.

**Ranks:**
- **II (L5), pairing:** when you spend Square Up on a hit, the same hit
  may also take one other option (Press, Hew within its cap, Empower,
  Bite Deep), paid with a second Technique **or, on your Momentum
  target, as a Momentum cash-in** (the hit's one free effect; lead's
  call). So the rank adds supply as well as a way to spend. Each option
  keeps its bounds. This is the single carve-out to one-per-hit (6.4
  rule 2).
- **III (L7), Called Out:** at the **start of your turn**, you may spend
  a Technique to Square Up one hostile in your reach that made an attack
  roll against an ally of yours since your last turn, with no hit. It
  counts as the round's Square Up. The hostiles that qualify are read
  from `attackedAllyLog` (13.1), cleared at your turn end.
  - A free Called Out was considered and **rejected (lead's call)**: a
    Square Up every round at no cost breaks the settled "Challenge is
    pooled, never applied at will on every hit" (classes 8.3, Cap
    respected).
  - It is an own-turn spend, so it is no second reaction.
  - It needs the `onEvent:turnStart` cost timing (VG-7).
  - The "Renew" refresh fallback is dropped.

**Level 6 (choose 1; each changes a verb):**
- **Retort Strike** (Square Up's punishment gains a strike).
  - **Trigger:** a unit you Squared Up, within your reach, makes an
    attack roll against a unit other than you.
  - **Effect:** after that attack resolves, make one **reaction
    attack** with your reaction. It is a new trigger kind,
    `attackAgainstOther`, and is **not** called an opportunity attack
    (VG-20).
  - **Hook point (lead's call):** after weapons 7.5 step 15 for a weapon
    attack, or after ability 14.3 step 6 for each attack roll of a
    non-weapon attack. It fires after **each** attack roll, not after the
    whole action, and the reaction limits it to one per round.
  - **Legality is re-checked at that moment:** a legal attack on the unit
    must exist now (weapons 8). An attacker that moved out of reach (by
    Flourish at step 14) or died is not struck, and the reaction is not
    spent.
  - **Bounds:**
    - no Techniques and no condition-applying verbs. Position verbs
      (Push, Hook) may apply under weapons 7.2 rule 3;
    - no Hold the Line stop (not a keyed trigger, 7.4);
    - no nesting off reactions or boss actions;
    - Staggered switches it off.
  - **Deterrence logging:** "X chose another target: Squared Up by you"
    is a signature event.
- **Shield Craft** (the bash's verb). Each shield bash chooses Push, Hook
  (M2) or, on your own turn, Topple (once per round per target,
  `sharesToppleCount: true`; the save slot). It is verb-sourced.
- **Rejected for level 6: Take the Blow** (a step-1 melee retarget),
  moved to **Later** with Guarded or the Holdfast unlock. It reproduces
  Guarded, whose listed source is "Protect Technique" (conditions 18.9),
  so it fails `FlagNotCondition`; its ranged trigger copied Shelter,
  which fails at any charge level (trinkets 11 rule 1).

**Surge Art: Step In** (M2, class level 5):
- Take the Dash action. At its end, Square Up one hostile adjacent to you
  with no hit (the round's Square Up).
- **Swap add-on, pending ruling VG-22:** once during the Dash, while
  adjacent to a willing, non-Downed ally, you may swap places with it.
  The swap costs 1 square of your movement and is 1 square of forced
  movement for the ally: no provoke, never a collision, never into or
  adjacent to a hazard. It is illegal while either unit is Grappled,
  Grappling, Restrained, Rooted or Anchored, or if the size does not
  fit.
- The Art works without the swap if the ruling is refused.
- About 0‰ of damage, and one action of tempo. Pass.
- **II (L9):** until your next turn starts, attack rolls by the
  Squared-Up unit against an ally adjacent to you take Interpose's +2 as
  a window fill. Smaller than Stand Fast: one attacker against the whole
  line.

**Triggers (`requires: subclass:bastion`):** `retortStrike.wardensReach`.
Retort Strike also triggers once per round (a turn allowance, 7.9) when
any hostile in your reach attacks an ally adjacent to you. The
composite-lockout test applies (12.1).

**Report owed:** the share of rounds with a Square Up active, beside the
Retort trigger and deterrence rates (13.3).

**Rejected for the Bastion:**
- Call Them Out and Taunting Roar (a multi-target Challenge cancels
  itself; stage 1 withdrew it);
- Hold Fast (a refresh has no semantics in conditions);
- `retortBeforeTheBlow` (pre-roll prevention);
- `takeTheBlowRanged`;
- Rebuke (a cost-free strike that stacks, and a withdrawn name).

**Weaknesses:**
- lower damage;
- one foe at a time, with Cleave and Skewer ending its own Square Up;
- one reaction covers everything;
- Pry and Staggered;
- ranged foes more than 6 squares away shed it;
- bosses resist it (Unyielding, the shed action);
- thin solo value from level 6;
- Technique-starved until offers.

**Staging:** M2 (Challenged is M2). Later: Take the Blow, a
Guarded-based guard.

### 8.3 WRECKER (M2): "the room is my weapon"

The stage-1 card is **replaced** (D1; VG-2). Its two clauses are already
baseline behaviour for every unit.

**Specs into:**
- collisions;
- hazard edges;
- crowd breaking;
- Prone set-ups that pay the party and the Shade's Opening;
- the Shove as a weapon.

**Data:**
- Lean: forced movement and terrain, secondary. **Prone from Wrecker
  features is a tool of that lean, not a control lean** (a ruling,
  VG-2), or `LeanSpread` fails.
- `mechanicIds [batteringForce, brink, ram, earthshaker]` plus `domino`
  or `upend`.

**Level 3:**
- **Battering Force:** once per round (turn allowance), when a hit's
  forced movement made only of verb-sourced parts is stopped early, it
  deals collision damage as if a non-verb part had contributed. The
  parts stay verb-sourced for every other rule (VG-8). Its
  `collision.damage` is the one allow-listed damage kind under
  `TurnAllowanceNoDamage` (7.9), bounded by `HewCeiling` (10.1).
- **Brink:**
  - (a) You choose the distance of forced movement you cause, from 1 to
    its maximum. On a hit this is the **combined** distance, from 1 to
    the cut total of the 3-square sum (6.4 rule 6). **Wrecker only.**
    Monsters do not gain precise edge placement.
  - (b) When forced movement you cause ends with a **hostile** adjacent
    to a hazard square it did not enter, it becomes **Teetering** with no
    save. This is a new Teetering source (conditions 18.4, VG-5); the
    budget is unchanged.
  - **Ordering:** a later push's edge save is made at disadvantage
    before that movement ends Teetering.
  - M2 hazards only. Allies are never made Teetering.
- **Ram** (signature; replaces Press for the Wrecker, so the picker does
  not grow).
  - **I:** push 1, not verb-sourced. If that move is stopped early, the
    moved unit makes a Fortitude save or falls Prone (tag `collision`).
  - **Ram's save:** shares the save slot, **reserved at step 9 and
    rolled at step 11** (VG-8). A reserved slot blocks riders and Topple
    on that target even when no save is rolled. A paid or free Ram
    suppresses the weapon's Topple verb on that hit (6.4 rule 6).
    `sharesToppleCount: true`; never against a Prone target; never on
    reactions.
  - **Stand-ins:** for a Wrecker, Ram stands in for Press in Momentum,
    Pivot, Drive Through and `onGraze` (6.5).
  - **II (L5):** push up to 2.
  - **III (L7), Skittles:** a **hostile** the moved unit strikes, no
    more than one size larger than you, also makes the save or falls
    Prone (its own save slot on that hostile, rolled at step 11; no
    movement). It never targets a unit already Prone, and it counts
    toward that hostile's Topple count (`sharesToppleCount: true`).

**Level 6 (choose 1; each changes a verb):**
- **Domino** (changes Push). When a combined move that **includes any
  verb-sourced part** (so a Ram plus the weapon's Push qualifies) is
  stopped early by a **hostile** no more than one size larger than you,
  that hostile is pushed 1 square the same way **(lead's call)**.
  - **Direction:** the vector of the moved unit's last attempted step,
    with ties broken as in conditions 10.1.
  - One link only.
  - The link is verb-sourced (once per round for that hostile).
  - It is inside the hit's **3-square sum across all moved units**, and
    uses only the squares the primary move left (6.4 rule 6). With no
    remainder there is no link; Brink (a) lets the Wrecker shorten the
    primary move to leave one, and the preview shows the trade.
  - It never moves allies.
  - It resolves at a new step 10b, after step 10's move and its
    collisions. The link collides only through Battering Force, if that
    allowance is unspent this round. Then Brink applies to the linked
    hostile.
- **Upend** (changes Topple). When Topple fires on your hit, you may
  push the target 1 square (verb-sourced, inside the sum).
  - If the push is stopped early (3.4: a 0-square blocked push counts;
    an ignored verb part does not), the target falls Prone (tag `trip`)
    with no save.
  - If it is not stopped, Topple's save is made after the movement, at
    step 11.
  - Topple II's push on a failed save is dropped when Upend is used.
  - All of Topple's bounds hold, and the save slot is used even when no
    save is rolled.
  - Step reorder (VG-8). Registry: Floorer, Thunderous.

**Surge Art: Earthshaker** (M2, class level 5):
- One Athletics roll against 10 + Fortitude of **up to 3** hostiles
  adjacent to you, each no more than one size larger. Each one you beat
  is pushed 1 square away or knocked Prone (tag `shove`), your choice
  per target.
- Pushes are not verb-sourced, so they collide, and Brink applies.
- **Order (lead's call):** targets resolve in an input order chosen by
  the player or scorer (default: unit order). Each push resolves fully
  (move, collisions, Brink) before the next.
- Each unit is moved once. A unit already damaged by a collision in
  this Art takes no further collision damage from it.
- The Athletics roll is **not a Shove**: it fires no `onShoveSuccess` or
  Drive Through. Its Prone choice has `sharesToppleCount: true`.
- Hostiles only.
- At most 267–361‰ *(illustr.;* computed under the dice band, lower
  under the flat band*)*. Pass. The 3-target cap answers the area-lean
  overlap. `drawIn` is rejected.
- **II (L9):** each push may be up to 2 (chosen).
- Registry: Quake, Shockwave.

**Triggers (`requires: subclass:wrecker`):** `surge.ramFree`. On a Surge
turn, your first Ram costs nothing (a free effect, not a refill).

**Rejected or Later for the Wrecker:**
- `domino.secondLink` (rejected);
- Avalanche (fails `SurgeOptionBound` under the dice band; redundant);
- collision + Strength (parity);
- ignoring a size step (breaks the settled A-10);
- Throw (Later: needs a free-hand grapple rule);
- Over the Edge auto-entry (Later: needs hazard caps);
- lethal chasms (Later: dungeon generation);
- breakable terrain (rejected: the fixed-terrain non-goal);
- carom pushes (Later).

**Collision band (lead's call):** flat 2 / 3 / 3 at levels 1–4 / 5–8 /
9–10 *(illustr.)*. The shape (flat, not dice) is the lead's; the values
are the builders', inside `HewCeiling`. Under a dice band, Ram's
collision value out-damages Hew at level 10. The draft band 2 / 4 / 6
failed `HewCeiling` at levels 5 and 10 once both collided units were
counted (10.1), so it was lowered in the revision.

**Weaknesses:**
- open, hazard-light rooms;
- Large or bigger bosses;
- Rooted, Restrained, Anchored and Stalwart targets;
- artillery and fliers;
- its own follow-up (a push moves the target off your next attack);
- friendly-fire geometry;
- slower previews.

**Reports owed:** value by theme hazard density; friendly-collision
damage share; minion clear rate against the Arcanist (pass/fail under
`LeanSpread`).

**Staging:**
- M1 ships the groundwork:
  - collision at most once per unit per hit;
  - shifts never collide;
  - collision damage is not a hit;
  - the collision causer on events;
  - the `techniquePush` tag.
- M2: the whole subclass.

## 9. New mechanics considered

None adds a condition: the budget of 32 is untouched. None grants
anything on the forbidden list (classes 6.3).

| # | Mechanic | What it adds | Cost | Touches | Decision |
|---|---|---|---|---|---|
| N1 | Technique ranks | Visible growth, 0 reading | S: data field, upgrade effects, 2 validators | ability 6.6, 10.2–10.3; classes 7.4 | **Adopted:** rank I in M1, II/III in M2 |
| N2 | Surge Arts + `SurgeOptionBound` | Signature moves; camp-share lever | S–M | ability 6.4; classes 8.3, 12.2–12.3 | **Adopted M2**, `minClassLevel 5` |
| N3 | Off-timing spends (`onShoveSuccess`, `onGrappleSuccess`, `onGraze`, `turnStart`) | "Decide after the result"; grapple debt | M | ability 7.1; weapons 7.5, 17.11; classes 8.3 | **Adopted M2** as triggers and Called Out. `onParrySuccess` (riposte) reserved for the Swordsworn |
| N4 | Verb borrowing | Two-set toolkit | M | weapons 7.1, 7.3, 11; ability 7.2 | **Adopted M2** (VG-19) |
| N5 | Intercept / Take the Blow | Bodyguard redirect | M | classes 6.3; armour 14.5; conditions 18.9 | **Later** with Guarded or Holdfast |
| N6 | Window fill (armour 10.4 step 0) | Capstone that protects the line | S–M | armour 10.4, 11; ability 7.3 | **Adopted M2** |
| N7 | `attackAgainstOther` reaction attack | Mark punishment | S | weapons 8; ability 7.2 | **Adopted M2** (Retort Strike) |
| N8 | Collision Prone, one-link chain | Into the Breach board play | M | weapons 7.2, 7.5; conditions 10.1 | **Adopted M2** |
| N9 | Edge pressure (Brink) | Hazard payoff | S | conditions 10.2, 18.4 | **Adopted M2**; Over the Edge Later |
| N10 | Stances as a record kind; camp re-pick; Stance Drill | Solo-to-party switch | S | classes 8.3, 18; armour 14.5 | **Adopted:** record kind in M1; re-pick (agreed, Q3) and Stance Drill in M2 |
| N11 | Momentum flag | Combo feel | S | classes 3.4, 6.3, 7.6 | **Adopted in M1** (agreed, Q2) |
| N12 | Shieldwall solo cover; Shield Craft | Floor-1 viability; shield as a verb weapon | S | armour 10.3–10.4; classes 8.3 | **Adopted M2** |
| N13 | Ally swap primitive | Rescue | S | conditions 10.1; classes 6.3 | **M2 add-on, pending ruling VG-22** |
| N14 | Turn allowance and per-target round count record kinds | Keeps the cap and pool counts honest | S | classes 3.4, 7.5; ability 6.6, 10.2 | **Adopted now (M1)** |
| — | In-fight stance toggles | Swap mid-fight | 5th reading row | ability 11.4 | **Rejected** (Later for an Adept or Berserker) |
| — | Stored combo meter | Build and spend | New pool or row | ability 6.6 | **Rejected** |
| — | Brace / Anchored self-brace | Planted defender | Reproduces Anchored | conditions 16 | **Later** (Holdfast) |
| — | Flat adjacency or formation auras | Formation play | Dead numbers; envelope | conditions 13; armour 10.4 | **Rejected** |
| — | Zone-entry stop | Pillars-style engagement | Overlaps Set, Hold the Line | weapons 8 | **Later** (Holdfast) |
| — | Universal sweep / Blade Storm | Crowd answer | Area lean; 2241‰ at level 3 | classes 6.4 | **Rejected** |
| — | Masterstroke, Combination | Crit and combo spends | Carve-outs | classes 8.3 | **Rejected** |
| — | Damage reduction | Tankiness | Stat model 6 bars DR; Shielded Later | — | **Rejected** |
| — | Kill refunds; free swaps outside a camp spend; destructible terrain | — | Forbidden refill; anti-goal; non-goal | — | **Rejected** |

### 9.1 Adopted mechanics in full

**Ranks (N1).**
- Option records carry `rankCategory` (`core`, `other`, `signature` or
  `art`) and `ranks[{rank, minClassLevel, text, effectKinds}]`.
  **`ranks[].minClassLevel` is the single source of rank levels (lead's
  call).**
- Class field: `rankSchedule {core: [1, 5], other: [1, 8], signature:
  [3, 5, 7], art: [5, 9]}`. It is not read at runtime. A validator
  checks each record's `minClassLevel` values against
  `rankSchedule[record.rankCategory]`, and allows a `rankOverride` on
  Hew so that the fallback lever (Hew 1d4 to level 6, 10.1) needs no
  schedule change.
- Current rank: the highest rank whose `minClassLevel` is at most the
  class level (3.4).
- Empower lines are records of their own, keyed by verb id: `{verbId,
  wave, ranks[], tag, payable}` (6.6).
- Validators: `RankAddsNoDice` (Hew allow-listed) and `RankKeepsBounds`.

**Surge Arts (N2).**
- An Art is an option on the Surge pool, own turn, using the camp spend.
- It grants only what it states and is greyed under action denial.
- One per subclass from class level 5; rank II at level 9.
- `SurgeOptionBound` as in 7.3.

**Window fill (N6).**
- Armour 10.4 gains step 0: fills from camp-pool abilities, which close
  the window. Several fills on one roll are ordered by source unit
  order, then creation order (7.7).
- Shown in the reaction term of the preview.
- Inherits the skip rules listed in 7.7. The "no reaction left" skip
  does not apply.
- Counts as "the one reaction" in armour 11 item 1's charged case.

**Verb borrowing (N4).**
- An "effective verb set" query on the attack.
- The borrowed verb is the spare weapon's canonical text, read with the
  spare weapon's training and Strain.
- Signatures never travel.
- Implemented as an effect keyed to a verb id, never as a family-record
  override.

**Momentum (N11):** see 6.7.

**Stances (N10).** A `stance` record kind: learned, never toggled; a
`stance.swap` at camp (7.5); `stance.learn` from Stance Drill.

**Turn allowances (N14):** see 7.9.

### 9.2 `FlagNotCondition` (governance; VG-11)

**(lead's call)** A class state flag sits outside the 32 only if all of
these hold:
1. it is held by its source unit, or it is a fact about one turn's
   movement or pipeline;
2. it has no save, no tier and no cure;
3. its `effectKinds` match no entry of the **condition equivalence
   table**, in any wave. The conditions design carries no effectKinds,
   so VG-11 adds that table as content data, `conditions.equiv.json`:
   one row per catalogued condition, giving the effectKinds it is
   equivalent to (for example *(illustr.)* Guarded: `attack.redirect`;
   Challenged: `goad.disadvantageVsOthers`; and so on for the 32). The
   builders write it from the conditions catalogue before data, and the
   registry pass checks it;
4. it is shown as a glint or on a row.

Each criterion is a data predicate over the flag's record: `heldBy`
(source or `turnPipeline`), the absence of `save`, `tier` and `cure`
fields, its `effectKinds`, and a `display` of `glint` or `row`.

Momentum, Hold the Line's stop, the Stand Fast state and the source-held
`empowerMarks` (13.1) pass. Take the Blow and Brace fail.

## 10. Numbers

*All illustrative: scratch models, not simulator evidence.* Base case:
R = 21 rounds per floor, 3 rest fills and 1 camp fill per floor. Results
swing about ±7 points with R.

### 10.1 Parity (weapons 6.4, any build ≤ 110%)

Greatsword Vanguard, every Hew-legal Technique spent on Hew, every Surge
spent on Attack:

| Reading | L1 | L5 | L10 |
|---|---|---|---|
| This plan, rest pools only (the VG-12 reading) | 109.7% | 109.3% | 108.4% |
| **Worst legal build, rest pools only (the VG-12 measure)**: the plan above plus every legal capacity step (Deep Reserves at level 10; capacity offers are excluded), its use spent on the best non-Hew option (a Press that sets Momentum, cashed in as a colliding Press) | **109.7%** | **109.3%** | **109.6%** (110.0% if every collision strikes a second hostile) |
| This plan, Surge counted (the row's current text) | 114.5% | 118.9% | 117.9% |
| Stage-1 concept numbers, Surge counted | 118.4% | 114.1% | 120.7% |
| Per-fight measure (fresh pools each fight) | 126–164% | — | — |

- **Stated plainly:** under weapons 6.4 as written, every plan fails,
  this one included. The pass rests on change VG-12 **(lead's call; the
  client was told plainly on 2026-11-18 and approved proceeding)**:
  - rest-pool damage is counted floor-averaged;
  - camp-pool damage is bounded by the nova bound and
    `SurgeOptionBound`;
  - parity is measured at the **worst legal build**: every legal
    capacity step (Deep Reserves; Technique capacity offers are excluded
    in the revision), and R at the simulator's observed lower bound.
- **Why the worst legal build still holds (revised):**
  - the all-Hew plan is the worst pool plan. A paid Press with its
    Momentum cash-in is worth less than a Hew: 1.2 + 0.7 against 2.7 at
    levels 1–4, and 1.8 + 1.6 against 3.8 at level 10 *(illustr.;*
    collision rate 400‰, half of collisions striking a second
    hostile*)*;
  - the one legal capacity step is Deep Reserves. Its use cannot pay for
    Hew and deals no collision damage itself (6.4 rule 7), so it adds
    only the Momentum cash-in it can set: about +1.2 points at level 10;
  - capacity offers on Techniques are excluded (`capacity:techniques`,
    7.11). Each would add about 1.2–2.5 points at level 10, and three
    such steps would pass 110%;
  - `capacity:surge` is excluded.
- **At R = 15** the rest-only figure is about 116%. **If simulated R is
  that low, the fallback lever is Hew 1d4 to level 6** *(lead's call,
  illustr.)*.
- **Builds** (weapons-only → with pools):
  - longsword + shield 65 → 81%;
  - maul 87–92 → 105–110%;
  - Wrecker hammer + shield with Battering Force 81 / 87 / 86% (flat
    band 2 / 3 / 3; 81 / 88 / 90% under the draft 2 / 4 / 6).
- **Subclass deltas:**
  - Weaponmaster greatsword +0%;
  - Weaponmaster maul +2.6 points;
  - greataxe Sure Form +1.5 points;
  - Twin Verb has no Topple, which keeps the greatsword build flat.
- **`HewCeiling` pass table** (the static formula of 6.4 rule 8;
  `collisionRate` 400‰ *(illustr.)*; both collided units counted when
  hostile). Hew's expected damage is 2.7 (1d4) at levels 1–4 and 3.8
  (1d6) at levels 5 and 10, with crits. Each cell gives per-use damage /
  the most one unit takes:

  | Line | L1–4 | L5 | L10 | Result |
  |---|---|---|---|---|
  | Every Empower line | 0 / 0 | 0 / 0 | 0 / 0 | Pass |
  | Press, Ram, a Momentum cash-in, `surge.ramFree` (moved unit + struck hostile) | 1.6 / 2 | 2.4 / 3 | 2.4 / 3 | Pass |
  | Battering Force (the same two units) | 1.6 / 2 | 2.4 / 3 | 2.4 / 3 | Pass |
  | Domino link with Battering Force (up to 3 hostiles: the moved unit, the linked hostile, the unit the link strikes) | — | — | 3.6 / 3 | Pass, with little room |
  | Skittles, Upend, Brink | 0 / 0 (Prone and Teetering deal no damage) | 0 / 0 | 0 / 0 | Pass |
  | Wrecker Stand Fast rider, `pushOnTurnedMiss` (1 square, non-verb) | 1.6 / 2 | 2.4 / 3 | 2.4 / 3 | Pass |
  | Earthshaker, per target line (pushed unit + struck unit) | — | 2.4 / 3 | 2.4 / 3 | Pass; the whole Art falls under `SurgeOptionBound` |
  | The draft band 2 / 4 / 6, Press | 1.6 / 2 | 3.2 / **4** | **4.8** / **6** | **Fail** at levels 5 and 10; hence 2 / 3 / 3 |

  - The Domino row sets the ceiling on the band: a level-9–10 band of 4
    gives 4.8, which fails.
  - Owed in the simulator: the observed collision rate per subclass and
    theme. If it exceeds `collisionRate`, the tuning value is raised and
    the table rerun.

### 10.2 Nova bound

Stand Fast deals no damage: the p95 turn ratio is 0.72 against the bound
of 2.0. Pass.

| Art | L5 | L10 | Result |
|---|---|---|---|
| Arsenal (rewrite) | 1000‰ | 1000‰ | Pass |
| Step In | about 0‰ | about 0‰ | Pass |
| Earthshaker (3 targets) | ≤ 267‰ | ≤ 361‰ | Pass |
| Blade Storm | — | — | Fails at level 3; rejected |
| Two-attack Arsenal | — | — | 2000‰; rejected |
| Avalanche, dice band | — | — | 1346‰; rejected |

### 10.3 Sustain and spread

| Check | Result |
|---|---|
| `Floor1BossSustain` (DH 21.1), level 3, Con +2 | 28 + 36 + 12 = **76** against a need of 48. Pass |
| Same, level 3, Con −1 | 58. Pass |
| Same, level 2, Con −1 | 45 against 48: **fail**. Not Vanguard-specific. **The check reads level 3, the boss-door level (lead's call; VG-24)** |
| Solo floor-1 boss, P(death) | Vanguard 1–15% against a featureless AC 15 hero at 21–51%. The Vanguard is structurally favoured, and its own levers move the result about 2–4 points |

The design does not widen the spread:
- no pool rises before level 5;
- Arts arrive at class level 5;
- Hew is 1d4 at levels 1–4;
- the default kit is the riskiest (warhammer + shield, 15.1% at level
  2);
- the ranged Parry line is cut, and the Shieldwall solo clause is the
  only ranged cover;
- Momentum adds 3–5 points of boss death at level 2 (players Hew less),
  which narrows the spread.

`Floor1ClassSpread` prints every creation pick and subclass, **diffed
against stage 1 per added feature**. The real gate needs all four
classes in data.

**Signature share in rounds 2 and later** (target about half; a report,
not a gate):

| Case | Share |
|---|---|
| Level 1–2, Hew policy | 10–20% |
| Level 1–2, with Momentum (the M1 slice, agreed Q2) | 36–38% |
| Level 3, Weaponmaster with the default kit | 42–53% |
| Level 3, Weaponmaster with Momentum | about 43–56% *(revised)*: at one attack a turn Momentum and Practised Hand compete for the same hit (6.7), so Momentum adds little before Extra Attack. The draft's 44–77% assumed the two never overlap |
| Level 3, Bastion or Wrecker with Momentum (M2) | up to the draft's 77% *(illustr.)*, where no other free effect competes; to be rerun (13.3) |

The scorer pre-highlights Press or Empower when the preview shows a
payoff (a collision, Prone, a peel, an edge), and labels Hew "+2.5 avg".

### 10.4 Camp share

Target 40–60% (lead's call, ability 6.3; restated as a settled chassis
rule in classes 8.2).

**The headline figures include Momentum, which ships (agreed, Q2), and
the named lever, applied now (lead's call).** Momentum raises rest
value. Its cash-ins are free options worth about a Technique each, at
the conversion rates of 6.7, on about half of the Techniques. Without
the lever it would take levels 5 and 10 below the band (39% and 37%).

| Measure | L1 | L5 | L10 |
|---|---|---|---|
| Full Second Wind, with Momentum and the lever | 16% | 35% | 30% |
| **Half Second Wind (the measure, agreed Q1), with Momentum and the lever** | **20%** | **41%** | **40%** |
| Second Wind excluded, with Momentum and the lever | 28% | 50% | 61% |
| *For reference:* half Second Wind, with Momentum, without the lever | 20% | 39% | 37% |
| *For reference:* half Second Wind, without Momentum (the draft figures) | 23% | 45% | 40% |

- **The lever, applied (lead's call):** the Arts and Stand Fast riders
  are valued and sized at their utility weight. An Art is worth 400‰ of
  an Attack action beyond the action it grants (300‰ in the draft), and
  800‰ at rank II (600‰). Stand Fast with its rider is worth about 8.4
  HP-equivalents per use (5.6). The texts in 8.1–8.3 and 7.7 are the
  ones this weight is measured on. The Weaponmaster rider's spare verb
  (7.7) is part of that weight. The weight is utility, not damage: no
  Art gains dice, and `SurgeOptionBound` is unchanged. The simulator
  checks it (13.3).
- **Level 5 sits near the band's lower edge (41%), and level 10 at it
  (40%),** rather than at the draft's 45% and 40%; the agreed decision (hold the band at levels 5 and 10) is
  met. The about-45% and about-40% figures the client saw were the
  draft's, without Momentum.

- **(agreed, Q1)** The camp share is measured with the
  `restDefensiveTool` (Second Wind) at half value, and all three
  measures are reported. **The level-1 miss is accepted**: it is
  structural, because no camp pool can rise before level 5 without
  adding floor-1 power. The band holds at levels 5 and 10 (about 45% and
  40% in the draft; 41% and 40% with Momentum counted and the lever
  applied, as tabled above). Raising Surge to 2 from level 1 is rejected.
- Arts at class level 5 do not affect level 1.
- The camp-share pass does not rely on camp damage being exempt from
  parity: Arts are valued at their utility, not as damage.
- **If level 5 or 10 falls out of band in the simulator (lead's
  call):** raise the same weight further, never a camp pool before
  level 5 and never `capacity:surge`.

### 10.5 Cap, reading and the 60% rule

| Check | Result |
|---|---|
| Limited abilities by level 10 | 4 of 8. Pass, given VG-10 |
| Named martial pools | 3 of 3. Pass |
| Reading count | 4 at every level. Pass. Ranks are pips; the Arts and Stand Fast sit on the Surge row; Momentum, Practised Hand and Twin Verb are glints (counting 0, per VG-11's row-modifier rule) |
| 60% rule, level-1 value | 178‰ (HP-equivalents) against the 400‰ bound. Pass |
| 60% rule, increments at 3/5/9 | 522‰ against 500‰. Pass (Arts at 5, Art II at 9) |

**Owed:** a rerun in the offer value unit (classes 5.6) before levels 3,
5 and 9 are fixed, including the level-8 rank move and the revised Art
and Stand Fast weight (10.4); an earlier draft measured 409‰ in another
unit.

### 10.6 Technique economy and Stand Fast

- Pool 2 covers about 44% of hits at levels 1–4. Pool 3 covers about 33%
  from level 5.
- A pool step spent on Hew would add about +2.8 parity points at level
  10. The one legal step, Deep Reserves, adds about +1.2, because its
  use deals no damage itself (10.1).
- Stand Fast is worth about 5.6 HP-equivalents per use, and about 8.4
  with its subclass rider (10.4). It is a protection capstone, not a
  power spike, which is intended.

## 11. M1 cut

Levels 1–3; no riders, no offers, M1 conditions only. **(lead's call;
Momentum agreed, Q2)**

**In:**
- Second Wind 1; Techniques 2 (Press I, Hew I 1d4, Empower I on M1
  lines); Surge 1 (extra action only).
- **Momentum** (agreed, Q2), with Press and M1 Empower lines as its
  cash-ins, and its clauses (6.7).
- Hold the Line only (so no stance re-pick is shown).
- The M1 level-2 menu: Drive Through, Pivot, Grim Resolve (with Bleeding
  and the stand).
- Weaponmaster level 3: Practised Hand on on-hit and Set-hit payable
  lines.
- Kit: chain mail; warhammer + shield / greataxe; 2 potions.
- Engine and data, with no new rules text beyond the above:
  - option records with `ranks[]` (rank I);
  - payable tags;
  - the stance record kind;
  - the turn-allowance and per-target round count kinds,
    `sharesToppleCount`, and the 3-square sum rule (6.4 rule 6; 7.9);
  - the Momentum flag and its scorer term;
  - the glints;
  - collision at most once per unit per hit; shifts never collide;
    collision damage is not a hit;
  - the collision causer on events;
  - the `techniquePush` tag;
  - the Hold the Line scorer pricing (VG-16).

**Conditions the Vanguard touches:**
- applies: Prone (Topple, Drive Through);
- ends: Frightened, Dazed, Bleeding (Grim Resolve);
- Steeled; engine states.

Collisions with walls and units apply. Pushes stop before hazards
**(settled, conditions 10.1)**.

**Out:**
- Bite Deep, Low Cut, Shieldwall, the stance re-pick;
- Bastion, Wrecker, Arts;
- ranks II/III, triggers, offers;
- Borrowed Form, Stand Fast.

**Simulator (M1):**
- `Floor1BossSustain` at level 3;
- the per-round boss breakdown;
- signature share per kit, with Momentum;
- Hew's pick share at levels 1–4, and for the Weaponmaster at levels 3–4
  (where Momentum and Practised Hand compete for one hit, 6.7);
- the Hold the Line fire and reroute rate.

## 12. Binding requirements and how they are met

| Requirement (source) | How met | Status |
|---|---|---|
| Rest and camp pools; 40–60% camp (ability 6.3; classes 8.2, 8.3 "Camp share") | Surge 2 at level 5; Arts; Stand Fast, weighted toward camp (10.4) | Levels 5 and 10 in band under the half measure, Momentum included (41% and 40%); the level-1 miss accepted **(agreed, Q1; VG-13 changes the settled chassis row)** |
| One camp spend per turn (ability 6.4) | Surge, Arts, Stand Fast and scroll reads share it | Met |
| At most 3 named martial pools; cap of 8; reading 4 (ability 6.6; classes 7.5–7.6) | 3 + capstone; turn allowances, per-target counts and the Hew-use sub-limit are not pools (VG-10) | Met, given VG-10 |
| One full-Recovery `restDefensiveTool`, once per rest, twice from level 6 (classes 8.2; DH 12) | Second Wind; `capacity:secondWind` excluded | Met |
| At least 2 at-wills, one damaging (ability 6.1) | Weapon attack; bash or Shove | Met |
| Extra Attack at level 5 (classes 7.4) | Level 5 | Met |
| Policy on every reaction; one reaction AC bonus per attack (ability 7.3; armour 10.4) | 7.12; the fill closes the window | Met; VG-4 |
| Forbidden grants (classes 6.3) | No second reaction (Called Out on own turn); no refill (Momentum and free effects are cost flags; VG-11 confirms a waiver is not a refill); no speed change (no self-steps; the swap pending VG-22); no targeting override (Take the Blow is Later); no created ally action; no second camp spend | Met |
| Hard control only from limited abilities; M1/M2 conditions (conditions 7, 16) | Prone, Staggered and Challenged from pools; Teetering a source only | Met |
| Talent menus of 3 with an upgrade; level-7 trigger option; M1 menu with M1 conditions (classes 7.4, 14) | 7.6 | Met |
| Level-6 feature changes a verb (classes 7.4) | Twin Verb, Sure Form, Retort Strike, Shield Craft, Domino, Upend | Met |
| Capstone: camp 1 at level 9, changes the primary lean, within the nova bound and the forbidden grants (ability 6.7; classes 14) | 7.7 | Met |
| AC envelope (armour 11) | 18 sustained; charged maximum 22 at level 1, 24 at level 9 | Met; evidence owed |
| Parity ≤ 110% (weapons 6.4) | Only under VG-12 | **Change requested (lead's call; client told)** |
| Dip guards (classes 7.7) | Class-level reads on ranks, Hew, subclass and capstone. **M4 proposal:** the creation stance is granted only when the Vanguard is the first class | Met; M4 item |
| 2 kit potions (DH 12.4) | 7.10 | Met |
| Grapple talents owed (weapons 17.11) | `onShoveSuccess`, `onGrappleSuccess` | Partly met; Throw Later |
| Composite-lockout test (classes 14) | 12.1 | Owed |
| `NameCollision` (classes 10) | Renames in the header | Met; naming pass |
| `effectKinds`, subject and trigger on every feature (classes 14; trinkets 11) | 7.13, one row per core, talent and subclass feature, rank and Art | Met; registry pass owed; the condition equivalence table (9.2) owed |
| Offer lanes, `triggerMenu`, default policies, creation defaults, ladder preview (classes 14) | 7.11, 7.12, 7.14 | Met |
| SRD check (classes stage 1) | Header; 18 | Owed before data |

### 12.1 Composite-lockout test additions

**Invariant (revised):** each case must leave the mover a legal way to
act or escape this turn (its action or its bonus action), unless it
spent them itself, and must lock it beyond the current turn in no case.
Each case states its expected outcome; the test confirms it.

| Case | Expected outcome |
|---|---|
| Hold the Line keyed to its triggers (`leaveZone`, `slipPast`, `setHit`), against Retort Strike and `wardensReach` | The stop applies only to keyed reaction attacks; Retort Strike never stops; the mover keeps its action |
| A Squared-Up unit that Disengages | It provokes and is stopped, and its action is spent (by its own Disengage). Its bonus action remains, and it is not locked beyond this turn. The scorer rates Dash-first above Disengage |
| A slip past a Bastion | It provokes before the step past, and a hit stops it on its last square; action and bonus action remain |
| Topple II push + Hold the Line | Prone, then pushed (no provoke); action remains, so it can stand |
| **Single Bastion:** Shield Craft Topple Prone + Square Up (Challenged) + Hold the Line + Retort Strike (+ Low Cut Staggered) | Every piece applies at most once per round per its limit; the unit can stand and act; Staggered ends at its own turn; no lockout |
| **Wrecker + Bastion companion:** Ram, Upend and Skittles Prone + Hold the Line + Square Up | Prone and the stop apply at most once per round (`sharesToppleCount`); the unit keeps its action to stand or Dash |
| Square Up + a Hold the Line opportunity attack on a second hostile | Under the default `notWhileSquaredUpElsewhere`, no attack and the mark stays; under `always`, the attack ends the Square Up (preview warned) |
| Drive Through Prone + Hold the Line | As Topple II |
| Brink Teetering + Hold the Line | The edge save is made at disadvantage on a later push; the unit can still act |
| Grappled + Prone | It can still attempt an escape with its action |
| Earthshaker Prone + Hold the Line | As Topple II, once per target |
| Domino link | One link, inside the 3-square sum; no second link |
| Stance Drill (both stances live) | One reaction covers both; no double stop |
| Every movement source after a stop: Dash, bonus-action Dash, Surge Dash, Spurred, slip past (now provoking a Bastion), forced movement by allies | Each adds movement as normal; only the current segment was stopped |

## 13. Data, validation and simulator

### 13.1 Record fields added

**(lead's call; illustrative shape)** Beside the classes 12.1 record:

- on option records: `rankCategory`, `ranks[{rank, minClassLevel, text,
  effectKinds}]` (the single source of rank levels, 9.1), `tags[]`
  (position, control, guard, geometry, accuracy, tempo, damage);
  `rankOverride` on Hew only;
- **Empower line records**, keyed by verb id: `{verbId, wave, ranks[],
  tag, payable}` (6.6);
- on Prone sources: `sharesToppleCount: bool` (6.4 rule 6);
- on triggers: `requires` (`subclass:<id>`, `stance:<id>`) and
  `barredFor`;
- on Art records: `actionsGranted` (7.3);
- on the class: `rankSchedule` (a validator input, 9.1), `hewUseCap`
  (per rest, by level), `stances[]` with the `stance` record kind;
- on subclasses: `offerLaneAdjust`, `artId`, `standFastRiderId`;
- `turnAllowance` and `perTargetRoundCount` records (7.9);
- in the tuning file: `collisionRate` and the `SurgeOptionBound`
  reference encounter (6.4 rule 8; 7.3);
- the condition equivalence table, `conditions.equiv.json` (9.2);
- cost timing `onEvent:<id>` (VG-7) and trigger kind
  `attackAgainstOther` (VG-20).

**Unit state (lead's call), with each reset event:**

| Field | Holds | Reset or cleared |
|---|---|---|
| `momentum {targetId, expiresAtOwnerTurnEndIndex}` | The one Momentum flag (6.7) | Cash-in; the owner's `turnEnd` at that index; target dead, Downed or off the roster; owner Downed or Incapacitated; combat end |
| `stanceIds[]` | The learned stances: one, or both with Stance Drill (7.5) | Camp re-pick; Stance Drill |
| `stanceRepickUsedCampId` | The camp visit whose re-pick was used | The next camp visit |
| `attackedAllyLog[{attackerId}]` | Hostiles that made an attack roll against your ally since your last turn (Called Out) | The owner's `turnEnd` |
| `standFast {expiresAt, riderEligible[], riderPushed[]}` | The active window and the rider sets (7.7) | The owner's next `turnStart` (`twoRounds`: next `turnEnd`) |
| `empowerMarks[{kind, targetId, expires}]` | Source-held target-facing Empower effects: Pry's other attacks this turn, Lunge's and Hook II's "no opportunity attack against you", Puncture's next attack, the Parry line | Each line's own expiry (this turn, or your next turn start) |

The Technique uses spent this rest are read from the pool's count, not
stored (3.2). The Hew-use cap needs no counter: Hew is barred only on
the Deep Reserves use, which is the use spent once the class-table
pool's worth is gone, so Hews per rest can never pass the class-table
pool (6.4 rule 7).

### 13.2 Content validation

The build fails when any of these fail (VG-11): `RankAddsNoDice`,
`RankKeepsBounds`, `HewCeiling`, `TechniquePickerSize`,
`SurgeOptionBound`, `TurnAllowanceNoDamage`, `FlagNotCondition`; and the
stage-1 checks as written (`ForbiddenClassGrant`, `LeanSpread`,
`SubclassCopiesBaseLean`, `SubclassAddsPool`, `NameCollision`, the cap
of eight, the copy registry).

### 13.3 Simulator reports owed

- parity at the worst legal build, with real R;
- the observed collision rate per subclass and theme, against
  `collisionRate` (10.1);
- camp share under three measures, with Momentum counted and the
  revised Art and Stand Fast weight (10.4);
- the 60% rule in the offer value unit;
- `Floor1ClassSpread` per pick and subclass, diffed per feature;
- signature share at levels 1–3 under Momentum, kit and scorer variants;
- the M2 Weaponmaster's signature share in the set it fights in (the
  shield set with a borrowable spare, and the greataxe active; 7.10);
- Hew's pick share at levels 1–4 in M1, and for the Weaponmaster at
  levels 3–4 (6.7); target below 50% in M2;
- Hold the Line fire and reroute rates;
- Stand Fast blocked misses per use;
- the protection reliance report with and without the Bastion;
- Retort trigger and deterrence rates, and the share of rounds with a
  Square Up active (8.2);
- the AI Disengage behaviour against a Bastion;
- Earthshaker minion clear rate against the Arcanist;
- friendly-collision share;
- Wrecker value by theme;
- stance re-pick rate by party size (a report) **(lead's call)**;
- the composite-lockout cases in 12.1.

## 14. Staging

**(lead's call)**

| Wave | Content |
|---|---|
| **M1** | Section 11: levels 1–3; Second Wind, Techniques (Press, Hew 1d4, M1 Empower lines), Surge, Momentum, Hold the Line, the M1 level-2 menu, the Weaponmaster's Practised Hand; the record kinds (ranks, stance, turn allowance, per-target round count); the collision groundwork; VG-16 scorer pricing; the M1 kit |
| **M2** | Levels 4–10; ranks II and III; Bite Deep, Low Cut, Cover Step; Shieldwall and the camp re-pick; the M2 level-2, level-7 and level-10 menus; offers, lanes and `triggerMenu`; Borrowed Form, Twin Verb, Sure Form and Arsenal; the Bastion; the Wrecker; Stand Fast with its riders; the swap add-on if VG-22 is ruled in; every report in 13.3 |
| **M4** | The creation-stance dip proposal (12) with the multiclass model |
| **Later** | Take the Blow and Guarded-based guards; Brace; zone-entry stop; Chosen Arms; Throw; Over the Edge; lethal chasms; carom pushes; in-fight stance toggles (for another class) |

## 15. Risks

| Risk | Mitigation, report and lever |
|---|---|
| Parity passes only under VG-12, and only inside model noise (±7 points with R); the worst legal build sits at 109.6–110.0% at level 10 | Worst-legal-build measure; the Hew cap; Deep Reserves deals no damage; `capacity:techniques` excluded; fallback Hew 1d4 to level 6; the simulator sets R |
| `HewCeiling` passes on an assumed `collisionRate` (400‰); the Domino row has little room | The observed rate per subclass and theme is reported; the band (2 / 3 / 3) is the lever, never a dice band |
| The camp share sits near the band's lower edge once Momentum is counted (41% at level 5, 40% at level 10) | The Art and Stand Fast weight is already applied; the simulator checks it; the same weight is the further lever (10.4) |
| Momentum adds little for the M1 Weaponmaster at levels 3–4 (it competes with Practised Hand for one hit) | Stated (6.7); the scorer term set to about 0 in that case; Hew's pick share at levels 3–4 reported in M1 |
| The Weaponmaster's signature lives in a different set from its protection with the default kit | The M2 spare recommendation at level 3 (7.10); the signature share in the set it fights in is reported |
| Hold the Line and Square Up undercut each other (an opportunity attack ends the mark) | `notWhileSquaredUpElsewhere` default; the preview warning; the mark-up share report (8.2) |
| The floor-1 spread stays wide (the Vanguard is favoured 5–20×) | No floor-1 power added beyond stage 1 except the Shieldwall solo clause; the per-feature diff report; the lever is the other classes' data |
| Hew remains the dominant default | Momentum (in M1); the scorer highlight; Hew 1d4 early; Hew's pick share report (target below 50% in M2) |
| Picker and decision load | The legal-only picker (≤ 6); one free-effect rule; one "Ready" glint; playtest of companion turns under 10 s |
| The Bastion plays itself on enemy turns | Square Up, pairing and Step In decided on its own turn; Called Out prompted for the hero (7.12); deterrence logged |
| Wrecker value depends on terrain | Walls set the floor of its value; reports by theme; no dependence on lethal hazards |
| A Momentum glint reads like a mark (budget optics) | `FlagNotCondition` ruling; it shows as the owner's badge first |
| The camp re-pick becomes a chore or a trap (Guarding goes inert) | Once per camp, camp screen only; the warning before confirming; the scorer suggestion (7.14); the re-pick rate report |
| Registry refusals (Shieldwall solo, Parry line, Stand Fast solo) | Each has a named fallback ("for parties"; no line; clause cut) |
| The Empower table authoring load (about 25 lines) | Players read only their weapon's line; tags make QA mechanical |
| Level-up wall at level 5 | Rank II for other options moved to level 8 |
| Recall-based survey and SRD shapes | Check against SRD 5.2.1 before data |

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: Hew's die (inside "1d4 then 1d6" unless the
  fallback lever fires), pool sizes inside the shape of 7.8, the
  collision band's values inside `HewCeiling`, `collisionRate`, the Art
  and Stand Fast utility weight (10.4), `SurgeOptionBound`'s permille
  and reference encounter, the Momentum scorer term and conversion
  rates, lane weights and `offerLaneAdjust`, trigger ids, the starting
  coin, the report thresholds.
- Level-up card, picker, glint, badge and camp-screen layout.
- Wording of option and Art texts within the rules here, until the
  naming pass.
- **Not tuning:** the ranks spine and its schedule; four things to read;
  no rank or Empower line adds dice or flat damage; one free effect per
  hit; the Hew cap and the non-damage Deep Reserves use; the 3-square
  sum rule; Ram standing in for Press; Momentum's rules and its M1 wave
  (Q2); the camp-share
  measure and the accepted level-1 miss (Q1); the camp re-pick (Q3);
  Shieldwall's shield requirement and solo clause; Stand Fast as a
  window fill; Take the Blow at Later; Interpose's +2 (settled); one
  full-Recovery Second Wind (settled).

## 17. Changes to approved documents

Applied on 2026-11-18, when the client approved this document. Each
change names the exact section it touches. **Agreed and settled rows are flagged**; the
client's Q1–Q3 answers approve the rows that name a question, the client
approved proceeding with VG-12 after being told what it changes, and the
rest are confirmed with the document.

**Narrowings recorded inside this design (no approved text changes;
recorded in classes 18 by VG-1):**
- condition options never fire on reaction attacks;
- Square Up own turn only;
- one free effect per hit;
- the Hew-use cap;
- Twin Verb's verb list;
- Brink's chosen distance is Wrecker-only;
- Earthshaker 3 targets;
- Square Up without a hit only from Called Out and Step In.

| # | Document / section | Change | Approved by |
|---|---|---|---|
| VG-1 | Classes 18, "Narrowings recorded inside this design" paragraph | Add the narrowings above | Lead |
| VG-2 | Classes 8.3: Techniques bullet and its "Timing" sub-bullet, Growth bullets (level 2 talents), Creation pick bullet (the Hold the Line sub-bullet), Subclasses bullets, Cap respected bullets; classes 11.C, the "Classes own depth, never access" row | Technique text (Press; Hew 1d4 / 1d6; options; one free effect); **the Square Up II pairing carve-out to one Technique per hit** (6.4 rule 2); Ram stands in for Press for the Wrecker (6.5); Drive Through rewrite; M2 level-2 menu; Grim Resolve text; Weaponmaster card ("keyed to verb ids; group tags as `requires` filters"), and the 11.C row's "Weaponmaster keyed to groups" to match; Bastion card (Square Up, the no-hit application by Called Out and Step In only, the slip-past clause, "harms" corrected to "targets"); Wrecker card replaced (8.3 here); Prone from Wrecker effects a forced-movement tool (leans ruling); Hold the Line keyed to `leaveZone`, `slipPast` and `setHit` (7.4), "Disengage stops it" not added | Lead (the 8.3 items are lead's calls) |
| VG-3 | Classes 8.3, Creation pick bullet, Shieldwall sub-bullet | Requires a shield; the solo ranged clause | Lead |
| VG-4 | Armour 10.4, "Candidate order" and "Window algorithm"; ability 7.3, the window paragraph (AR-8); armour 11 item 1; weapons 7.5 step 5 (the restated window order) | Step 0 window fills, ordered by source unit order then creation order; a fill counts as "the one reaction"; the skip rules inherited except "no reaction left" (7.7) | Lead |
| VG-5 | Conditions 18.4, Teetering, "Sources" line | Add Brink; the edge save made before the movement ends Teetering | Lead |
| VG-6 | Classes 8.3, Techniques, Surge and "M1 content" bullets ("push, +1d6 and empower-verb Techniques"); ability 6.6, Techniques and Surge bullets (illustrative, the W-3 example); classes 13, M1 row ("+1d6") | Techniques 2 / 3 / 3; Surge 2 at level 5; Hew 1d4 at levels 1–4, 1d6 from level 5. **Classes 13's M1 row and 8.3's M1 content bullet gain Momentum** | Lead; **Momentum in M1: client (agreed 2026-11-18, Q2)** |
| VG-7 | Ability 7.1, declared costs; classes 8.3, Technique timing | `onEvent:<id>` cost timing (Shove, grapple, Graze, turn start) | Lead |
| VG-8 | Weapons 7.2 rule 4; weapons 7.5 steps 6 and 9–11, new steps 6a and 10b; conditions 10.1, the "One move per hit" bullet | Empowered movement stays verb-sourced; the Battering Force exception; the 3-square sum across all moved units, counted in squares actually moved, with the part order, truncation and "contributes" rule of 6.4 rule 6; "stopped early" defined (3.4); the save slot reserved at step 9 and rolled at step 11 for collision-conditional effects (Thunderous precedent), a reserved slot blocking riders and Topple; `sharesToppleCount`; a Ram suppresses the Topple verb on its hit; **step 6a**, Sure Form's verb on a miss (8.1); the Upend order, with Topple II's push dropped under Upend; **step 10b**, the Domino link (inside the remainder, colliding only through Battering Force, then Brink) | Lead |
| VG-9 | Ability 7.2, Shove row | Write the push as "push 1 or Prone" | Lead. **The Shove contest is settled; only the push wording changes** |
| VG-10 | Classes 3.4 (terms), 7.5 (counting); ability 6.6 (the three-pool sentence), 10.2 (the CL-2 text) | Turn allowances and per-target round counts are frequency limits, not named pools; outside the cap and the three-pool limit; count 0; outside the camp-share ratio, inside the 60% formula. **A per-rest sub-limit on one option of a named pool (the Hew-use cap) is not a named pool and is not counted**, as the `restDefensiveTool` sub-limit is not | Lead |
| VG-11 | Classes 12.1, 12.2, 12.3, 7.6; ability 7.3 (policy filters) | `rankSchedule` (a validator input) and `rankCategory`; `offerLaneAdjust`; `triggerMenu` ids; validators as data predicates (`RankAddsNoDice`, `RankKeepsBounds`, `HewCeiling` as the static formula, `TechniquePickerSize` over reachable option sets, `SurgeOptionBound` with `actionsGranted` and the reference encounter, `TurnAllowanceNoDamage` with its allow-list, `FlagNotCondition` with the condition equivalence table); `collisionRate` in the tuning file; the nova bound with Arts; the policy filter `notWhileSquaredUpElsewhere`; a row-modifier passive or glint counts 0; a cost waiver is not a refill (the 6.3 reading) | Lead |
| VG-12 | Weapons 6.4, parity targets table, "Any build ≤ 110%" row; classes 6.4, the single-target damage cap cell ("Every build ≤ 110% of the greatsword reference", read "as measured by weapons 6.4, VG-12") | Measured floor-averaged at the worst legal build for rest pools; camp-pool damage bounded by the nova bound and `SurgeOptionBound`. This **changes the row's meaning** | Lead (the row is the lead's call); **the client was told plainly on 2026-11-18 and approved proceeding** |
| VG-13 | Ability 6.3, the measure bullet ("The share is measured in the offer value unit"); **classes 8.2, the camp-pool chassis bullet (settled)**; **classes 8.3, the "Camp share" bullet (settled)**; classes 11.B, the camp-share row (a note: "level-1 martial miss accepted") | The camp share counts the `restDefensiveTool` at half value; all three measures reported, Momentum included; the band holds at levels 5 and 10; the level-1 martial miss accepted. **Both classes rows are settled rows changed by the client** | **Client (agreed 2026-11-18, Q1): the measure and the level-1 miss** |
| VG-14 | Classes 12.1, subclass fields | Subclass field `offerLaneAdjust` (one name) | Lead |
| VG-15 | Classes 8.3, Growth bullet, level 9 (Stand Fast); classes 14, registry list | Stand Fast "every roll", shield required, solo clause, subclass riders | Lead |
| VG-16 | Enemies 11.1, the opportunity-cost term | Prices known reaction triggers (the Hold the Line stop, Retort Strike) as state; never class | Lead |
| VG-17 | Classes 8.3, Creation pick bullet ("a **permanent** creation pick"); classes 18, narrowings paragraph ("the stance a permanent creation pick"); armour 14.5, Guarding row | The stance may be re-picked once per camp, never in combat; Guarding gains a runtime-inert state with a warning | **Client (agreed 2026-11-18, Q3)**, since the permanent pick was confirmed with stage 1; armour row: Lead |
| VG-18 | Classes 14, "Registry pass" bullet; trinkets 11 rule 1 (the registry's scope, as amended by CL-11: "It also covers ... weapon verb signatures") | Weapon verb signatures join the registry list. Class features never match armour or shield signatures (trinkets 11 rule 1). Payable lines prefer original effects. Echoes of verb signatures (Warding Staff) need a ruling | Lead |
| VG-19 | Weapons 7.3, class hooks; ability 7.2, a note | A subclass may apply the canonical verb of a trained, non-Strained weapon in the wielder's own spare set. Signatures, dice, type, reach and properties never travel. A note says why this is not a key per enemy | Lead (7.3 is a lead's call) |
| VG-20 | Weapons 8, "Movement and reactions" bullets; ability 7.2, threatened-zone row | New trigger kind `attackAgainstOther` (a reaction attack, not an opportunity attack), hooked after weapons 7.5 step 15, or after ability 14.3 step 6 per attack roll, with legality re-checked then; the `slipPast` opportunity-attack trigger point (before the step past, after a successful check; 7.4) | Lead |
| VG-21 | Armour 13.6, "Starting kits" bullet (illustrative) | Vanguard kit: chain mail, warhammer + shield / greataxe | Lead |
| VG-22 | Conditions 10.1; classes 6.3 (reading) | Ally swap primitive (1 square of forced movement on the ally; not a teleport or a speed change) | Lead (ruling pending; Step In works without it) |
| VG-23 | Conditions 18.3, Staggered, "Sources" line | Clarify that collision damage is not a hit, so the "automatic on bludgeoning-weak" source does not fire from it. **Stat model 8.2 (agreed) is unchanged**: it reads "any hit". If the lead reads 8.2 otherwise, this becomes a client item | Lead (clarification only) |
| VG-24 | DH 21.1, `Floor1BossSustain` row | State that the check reads hero level 3, the boss-door level | Lead |

**Not changed:** stat model 8.2 (agreed); Interpose's +2 (settled,
armour 10.3; enemies 17.1); the Shove contest (ability 7.2); ability 6.7
(the capstone is a camp pool of 1); the Challenged end rules
(conditions 18.8); armour 6.1 (visible AC); "a choice every level" (main
4.5); one full-Recovery Second Wind (DH 12.1); the 32-condition budget.

**Check after applying:** a grep for "2 / 3 / 4" in classes 8.3 finds
nothing; a grep for "4 at level 9" in ability 6.6 finds only history; a
grep for "permanent" in classes 8.3 and 18 shows the camp re-pick; a
grep for "Challenge" in classes 8.3 finds only "Challenged" and history;
a grep for "harms another unit" in classes 8.3 finds nothing; a grep for
"Any build" in weapons 6.4 shows the VG-12 measure; a grep for "step 0"
in armour 10.4 shows the window fill; a grep for "Teetering" sources in
conditions 18.4 shows Brink; a grep for "+1d6" in classes 13's M1 row
finds nothing; a grep for "+1d6" in classes 8.3's M1 content bullet
finds nothing; a grep for "Momentum" in classes 13's M1 row finds it; a
grep for "step 0" in weapons 7.5 step 5 finds the window fill; a grep
for "keyed to groups" in classes 11.C finds nothing; a grep for "40–60%"
in classes 8.2 and 8.3 shows the level-1 note; a grep for "≤ 110%" in
classes 6.4 shows the VG-12 reference; a grep for "weapon verb
signatures" in trinkets 11 rule 1 finds it; a grep for "One move per
hit" in conditions 10.1 shows the VG-8 sum.

## 18. Depends on systems not yet designed

- **The other three stage-2 class documents:** `Floor1ClassSpread` as a
  real gate needs all four classes in data; the `LeanSpread` minion
  clear check reads the Arcanist.
- **Companions:** a Vanguard recruit kit as a signature slice that does
  not copy the hero's Momentum or Arts; protector kits on at least two
  classes (classes 7.8).
- **Dungeon generation:** hazard density by theme (Wrecker value);
  walls and edges near main-path fights.
- **Camp and hub screens:** the stance re-pick step and its Guarding
  and stance-bound trigger warning; the level-3 spare-weapon suggestion
  for the Weaponmaster (7.10).
- **Naming pass:** every placeholder name here, Hold the Line included.
- **Rulings owed by the lead:** VG-18 (registry scope, the Parry line),
  VG-22 (the ally swap).
- **Lead's checks before data:** every SRD shape in this document
  (Second Wind, Surge as Action Surge, Extra Attack, Shove, the verbs'
  SRD sources) against SRD 5.2.1; `NameCollision` over the final names.

## 19. Open items for the client

None. Approved by the client on 2026-11-18.

### 19.1 This document

The client approved the document as a whole on 2026-11-18, including
every lead's call in it. The three questions it raised were answered on
2026-11-18 **(agreed)**, each with the recommended option:

- **Q1, camp share at level 1:** accept the level-1 miss; measure with
  the `restDefensiveTool` (Second Wind) at half value and report all
  three measures; hold the 40–60% band at levels 5 and 10 (about 45%
  and 40%). Raising Surge to 2 from level 1 is rejected. With Momentum
  counted, as it ships, and the named lever applied, the figures are 41%
  and 40% (10.4): the band holds, as agreed. The client confirmed these
  figures on 2026-11-18.
- **Q2, Momentum in M1:** Momentum ships in the M1 slice Vanguard.
- **Q3, the stance re-pick:** once per camp, rest-gated, never in combat;
  Stance Drill at level 7 still makes both stances live.

The client was also told plainly about **VG-12** (weapons 6.4 parity
measured floor-averaged at the worst legal build for rest pools;
camp-pool damage bounded by the nova bound and `SurgeOptionBound`) and
approved proceeding with it as the lead's call. Under it, the worst
legal build now sits at 109.6–110.0% at level 10, because the revision
excludes Technique capacity offers (10.1). The approval of 2026-11-18
confirmed every lead's call in it.

**The revision of 2026-11-18 needed no new client decision.** It changed
lead's calls only (appendix A, K-n). Two of them were worth noting
because the client saw the earlier figures: the collision band is now
2 / 3 / 3 (was 2 / 4 / 6), and the camp share, with Momentum counted,
is 41% and 40% at levels 5 and 10 (was about 45% and 40%). The client
confirmed both revised figures with the approval on 2026-11-18.

### 19.2 Future items triggered by reports

These come back to the client only if a report or a later design
triggers them:

- parity at the worst legal build above 110% after the Hew-to-level-6
  lever;
- the camp share at level 5 or 10 out of band after the Art-weight
  lever;
- a `Floor1ClassSpread` miss whose levers run out;
- VG-23, if the lead reads stat model 8.2 as covering collision damage;
- the naming pass;
- the creation-stance dip proposal at M4.

## Appendix A: how the critique findings were resolved

**Fun and identity critique (F-n)**

| # | Finding | Resolution |
|---|---|---|
| F-1 | Signature rarely fires on floor 1; Hew 1d4 makes it smaller | Scorer highlight and "+2.5 avg" label (10.3); Momentum in M1 (6.7, agreed Q2); kit with on-hit verbs (7.10) |
| F-2 | Stand Fast invisible; dead in two of three builds | Automatic subclass riders, solo clause, "blocked" events (7.7) |
| F-3 | Late plateau; level-5 wall | Rank II for other options moved to level 8; Art II at level 9 (5.5); 60% rule rerun owed |
| F-4 | Weaponmaster's signature least visible; "both verbs" duplicated | Arsenal as a named verb sequence; Borrowed Form II = empowered borrow; both verbs only in Twin Verb; default kit; card wording (8.1) |
| F-5 | Free allowances with different slot rules | One free-effect rule (6.4); one free-cost trigger (`naturalHighFree`); Follow-Through merged into Momentum; one "Ready" glint |
| F-6 | Bastion plays itself | Own-turn decisions at each rank; deterrence events; Called Out `ask` for the hero (8.2) |
| F-7 | Step In depends on a Later primitive | Fallback built in; the swap is an add-on (8.2; VG-22) |
| F-8 | Wrecker chain granted twice | Skittles (Prone, no movement) at rank III; Domino at level 6; `secondLink` cut (8.3) |
| F-9 | Grim Resolve nearly dead in M1 (only the M1 boss's roar and its limited Dazed) | Ends Bleeding and stands you (7.6) |
| F-10 | Hold the Line unseen solo | Zone markers, reroute events, fire-rate report (7.4) |
| F-11 | Striker half stops growing | Card wording "protection first; damage through position"; no rules change |
| F-12 | Not worth the cost: full combo, pairing, step 8b, Set III; re-pick wanted | Combo reduced to Momentum; pairing the one carve-out; step 8b and Set III cut; re-pick agreed (Q3) |

**Balance under permadeath critique (B-n)**

| # | Finding | Resolution |
|---|---|---|
| B-S1 | Parity passes only by redefinition; offers ignored | Stated plainly (10.1); VG-12 as a change, told to the client; worst-legal-build measure with its own row; `capacity:techniques` and `capacity:surge` excluded; Deep Reserves cannot pay for Hew and deals no collision damage |
| B-S2 | Hew 1d4 makes damage Empower lines beat Hew | No Empower line adds damage (6.6); `HewCeiling` at levels 1–4 |
| B-S3 | Floor-1 power grows | Arts at class level 5; ranged Parry line cut; per-feature spread diff; Momentum noted as spread-narrowing; riskiest kit by default |
| B-S4 | Redirect breaks forbidden grants; Shelter precedent weak | Take the Blow moved to Later (8.2) |
| B-S5 | Called Out is a second reaction | Moved to the start of your own turn (8.2) |
| B-S6 | Disengage trap the AI cannot see | Clause dropped (already a base rule); scorer term VG-16 as an M1 precondition; lockout case added (12.1) |
| B-S7 | Twin Verb Topple boosts the greatsword | Topple removed from Twin Verb (8.1) |
| B-S8 | Earthshaker is an area lean | Cap of 3; `drawIn` rejected; minion clear rate pass/fail (8.3) |
| B-S9 | Forced-movement cap read two ways; chosen distance helps monsters; Brink check | Sum across all units; `secondLink` cut; chosen distance Wrecker-only; Over the Edge Later pending hazard caps |
| B-S10 | Turn allowances may break the pool limits | VG-10 record kind; `TurnAllowanceNoDamage` |
| B-S11 | Dip guards on ranks | Class-level reads (6.4 rule 9); M4 stance proposal (12) |
| B-S12 | Camp share propped by the same exemption | All three measures reported; Arts valued at utility (10.4) |
| B-S13 | Smaller items: Stand Fast shield and envelope; `Floor1BossSustain` level; Follow-Through on Hew kills; swap into hazard edges; L1–2 share | Shield required, envelope rerun; VG-24 (level 3); Follow-Through merged into Momentum, which only Press or Empower sets; the swap never enters a square adjacent to a hazard; Momentum in M1 (Q2) |

**Consistency critique (C-n)**

| # | Finding | Resolution |
|---|---|---|
| C-1 | The camp band is a settled chassis rule | Measure change to the lead (ability 6.3); level-1 miss agreed by the client (Q1; VG-13) |
| C-2 | `turn` pools break the pool counts | VG-10 |
| C-3 | Called Out is a disguised reaction | Own-turn spend at turn start (VG-7) |
| C-4 | Take the Blow reproduces Guarded; ranged trigger copies Shelter | Take the Blow Later; `takeTheBlowRanged` deleted; rule-1 rechecks listed (VG-18) |
| C-5 | Verb borrowing has no change row | VG-19 |
| C-6 | Stand Fast visible-AC contradiction; skip rules | Reaction term in the preview; every skip rule inherited; all rows named (VG-4, VG-15) |
| C-7 | The Challenged-Disengage clause is a base rule; "Disengage stops it" contradicts 18.8 | Clause dropped; the line not adopted; slip-past clause added instead (8.2, 7.4) |
| C-8 | New reactions called "opportunity attacks" | Reaction attack of trigger kind `attackAgainstOther` (VG-20) |
| C-9 | "Harms" changes the Challenged text | Approved "targets" kept; paraphrase corrected; preview shows the end (8.2) |
| C-10 | Name collisions | Square Up, Press, Borrowed Form, Stance Drill, Stubborn Breath, Bite Deep (for Carry); Hold Fast, Press On, Answer Steel and Hamstring not used; Guarding Blow and Warden's Strike not used; Hold the Line to the naming pass |
| C-11 | The kit breaks the subclass or the types | Warhammer + shield / greataxe; no presets (7.10, VG-21) |
| C-12 | Second Form II overlaps level 6 | Borrowed Form II = empowered borrow (8.1) |
| C-13 | Collision Prone at the wrong save step | Slot reserved at step 9, rolled at step 11 (VG-8) |
| C-14 | N3 overrides stat model 8.2 | Clarification only: a collision is not a hit, so agreed 8.2 is untouched (VG-23) |
| C-15 | Graze lines contradict "no verb" | No Graze line; `onGraze` trigger with timing row VG-7 |
| C-16 | Echo basis misattributed | Registry-scope ruling (VG-18); originals preferred |
| C-17 | Hew 1d4 needs rows; Deep Reserves a hidden sub-pool | VG-6; allow-list in `RankAddsNoDice`; the Hew-use cap (6.4 rule 7), a sub-limit exempt under VG-10 |
| C-18 | Condition-on-reaction narrowings | Recorded in classes 18 (VG-1) |
| C-19 | Camp re-pick amends a recorded narrowing; Guarding inert state | Agreed by the client (Q3); VG-17 |
| C-20 | Swap conflict (Later or M2); tempo clause | Add-on pending VG-22; tempo clause in `SurgeOptionBound`; Step In buys one Dash |
| C-21 | `retortBeforeTheBlow` reintroduces prevention | Rejected |
| C-22 | Brink ordering; 18.4 source | VG-5 |
| C-23 | Chosen distance needs rows | Wrecker-only, so it lives in the subclass text; Push's canonical text untouched |
| C-24 | 60% evidence in two units | Rerun in the offer value unit owed (10.5) |
| C-25 | Lane field name | `offerLaneAdjust` (VG-14) |
| C-26 | Surge 2 at level 5 rows; timescale excludes | VG-6; `offerExcludes` (7.11) |

**Check pass of 2026-11-18 (K-n), after the client's answers** **(lead's
calls unless marked)**

| # | Finding | Resolution |
|---|---|---|
| K-1 | `HewCeiling` failed on its own numbers and counted one unit | Static formula with `collisionRate`, both units counted; band 2 / 3 / 3; pass table (6.4 rule 8; 10.1) |
| K-2 | Momentum's set, cash-in, refresh and expiry underspecified | Clauses with unit tests (6.7); the agreed rule unchanged |
| K-3 | Ram replaced Press but rules naming Press were not remapped | Ram stands in for Press everywhere (6.5) |
| K-4 | The 3-square sum had no order, truncation or "contributes" rule | 6.4 rule 6; "stopped early" defined (3.4) |
| K-5 | Moves after step 10 (Skewer II, Topple II, Domino, Upend) | Skewer II cut; Topple II dropped under Upend; step 10b defined (VG-8) |
| K-6 | Save-slot priority; which Prone sources share Topple's count | `sharesToppleCount`; Ram suppresses Topple; reserved slots block (6.4 rule 6) |
| K-7 | Domino trigger and direction; Earthshaker order | Any verb-sourced part; last-step vector; Earthshaker input order (8.3) |
| K-8 | Hold the Line did not key slip-pasts | `{leaveZone, slipPast, setHit}`; slip-past trigger point (7.4; VG-20) |
| K-9 | Stand Fast's inherited skips; fill order; shield check | Skips listed in full; fill order; shield checked at each roll (7.7) |
| K-10 | Retort Strike's hook, re-check and bounds | Hook points; legality re-check; bounds reworded (8.2) |
| K-11 | Turn allowances could not model per-target counts or riders | `perTargetRoundCount`; per-use sets; refill priority (7.9) |
| K-12 | Validators not evaluable from data; Battering Force broke `TurnAllowanceNoDamage` | Predicates; allow-list; condition equivalence table; `actionsGranted`; reference encounter (7.3, 7.9, 9.2) |
| K-13 | 7 options were reachable | Low Cut and Cover Step mutually exclusive; validator over reachable sets (6.5, 6.8) |
| K-14 | Rank data had two sources; Empower `payable` per line | `minClassLevel` the single source; `rankCategory`; line records (9.1) |
| K-15 | Missing unit state and effectKinds | 13.1 state table; 7.13 rows for every feature |
| K-16 | Sure Form had no pipeline step; Square Up's no-hit form too wide | Step 6a; no-hit form by source id; Square Up at step 11 (8.1, 8.2) |
| K-17 | Camp share without Momentum | Recomputed with Momentum; lever applied; 41% / 40% (10.4) |
| K-18 | Worst legal build unshown; capacity offers could pass 110% | Row added; `capacity:techniques` excluded; Deep Reserves non-damage (10.1) |
| K-19 | Leans moved without a change row | Stage-1 leans restored (7.1) |
| K-20 | Pry line was a hidden debuff; `changeOfHands` broke once per round; Carry collided | Pry narrowed; trigger cut; renamed Bite Deep |
| K-21 | Weaponmaster's signature and protection in different sets; Momentum versus Practised Hand at levels 3–4 | M2 spare recommendation; scorer term; reports (7.10; 6.7) |
| K-22 | Hold the Line opportunity attacks ended Square Up; the Bastion had no supply step | `notWhileSquaredUpElsewhere`; the pairing may take a Momentum cash-in; a free Called Out rejected (8.2) |
| K-23 | Change rows missed sections (VG-2, 4, 6, 8, 10, 11, 12, 13, 18) | Rows extended; VG-13 retargeted to classes 8.2, 8.3 and 11.B, credited to the client (Q1) |

**Research and draft findings carried into this design:** D1–D12 (3.3),
and from the drafts: the Hold the Line name collision (naming pass); the
Surge denial rule (7.3); the Stand Fast weakness that motivated the
window fill (7.7); the timescale offer exclusion (7.11); the name
"mastery" banned for verb features, hence Practised Hand (weapons 7.4).

## Appendix B: sources

- Approved specs in `docs/superpowers/specs/` (the 12 parents listed in
  the header). Facts were re-checked for this document in:
  - classes stage 1: 3.1, 3.4, 4, 7.4–7.7, 8.2, 8.3, 12, 13, 14, 15,
    18, 20;
  - ability model 6.2–6.7, 7.1–7.3, 10.2, 14.6;
  - weapons 6.4, 7.1–7.5, 8, 17.11;
  - armour 10.3, 10.4, 11, 13.6, 14.5;
  - conditions 10.1, 10.2, 18.3, 18.4, 18.8, 18.9;
  - enemies 11.1, 17.2;
  - trinkets 11;
  - death and healing 8, 12.1, 12.4, 21.1;
  - name searches across all twelve documents.
- Web checks (research briefs): aidedd.org (Fighter 2024);
  dungeonmister.com and dungeonsanddragonsfan.com (Battle Master 2024);
  2e.aonprd.com and rpgbot.net (PF2e Guardian). Everything else in 4.1
  is recalled and unverified.
- Scratch models (illustrative, not simulator evidence), kept outside
  the repository in the session scratchpad: `numbers/vg_numbers.py`,
  `numbers/final.py`, `core/variants.py`, `wm/parity.py`,
  `wrecker/parity.py`.
