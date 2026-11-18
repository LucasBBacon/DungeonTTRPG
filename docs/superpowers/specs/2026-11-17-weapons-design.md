# Weapons and Foci: Design

Date: 2026-11-17
Status: Approved, 2026-11-17
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design) and
`2026-11-16-items-inventory-design.md` (items design)

Items marked **(agreed)** were decided by the client, on 2026-11-17 for
the questions this document raised, either directly or by choosing the
lead's recommendation. Items marked **(lead's call)** were filled in by
the design lead while writing this document and are confirmed when it is
approved. Items marked **(settled)** restate a rule from a parent
document. Items marked **(change)** amend an approved document and are
listed, with an id, in section 25. Once approved, every item here is
binding on the builders.

This is the first **category design** on the items design's roadmap (items
design 17, row 1): weapons, with spellcasting foci. Armour and shields are
the next category design; this document hands it only what shields do as
weapons (17.9).

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. Tuning numbers live
in one file, `weapons.json` (20.2). The builders own them and the
simulator calibrates them (section 24), except where a value is marked as
a client decision.

**SRD check (lead's call).** SRD 5.2 weapon dice and weapon mastery wording
in this document are written from memory: the official SRD hosts could not
be reached while it was written. **Every SRD 5.2 die, name and mastery text
must be checked against the SRD 5.2.1 document before any weapon or verb
record is written**, and every record flagged `srd` cites the entry it was
checked against (section 19).

**Waves:** M1 is the vertical slice (one class, levels 1–3, one floor,
three enemy types, solo hero, basic loot). M2 is the full run with
companions. M3 adds profile, unlocks and boons. Later is everything after
M3 that no milestone names, including M4. **(settled, main design 9; items
design header)**

## 1. Intent

Weapons are what every unit fights with most turns, so they decide how
most turns feel. The parents settle the stat side (Strength adds to melee
damage, Dexterity adds to hit only), the damage types and their riders,
the weapon set and its bonus-action swap, the item record and its
properties, and the monster one-die rule. **(settled)** They leave open
what makes one weapon play differently from another, how families, groups
and training work, how reach, ranged, thrown and two weapons work, what
foci do, how monsters use weapons, and whether weapons wear out. This
document closes those questions.

The system should make picking up a different weapon change how a unit
plays at once, keep every weapon readable in one line, give Strength and
Dexterity builds comparable damage by different routes, and add no
bookkeeping. **(agreed)**

## 2. Goals, non-goals, anti-goals

### Goals

- **Two weapons differ by what they do, not only by their numbers.** Each
  family has a chassis (dice, type, hands, reach or range, Strength need)
  and exactly one *verb*: a short, automatic effect on its attacks.
  **(agreed, framework I1)**
- **The weapon owns its verb.** Any unit trained in the weapon uses the
  verb automatically, companions and monsters included. Classes own how
  often it fires and how it improves. **(agreed)**
- **Players learn a verb from enemies and later loot it.** Monsters use the
  same verb text as the party. **(agreed)**
- **Strength hits hard, Dexterity hits often, and both finish the fight in
  comparable time** (6.4). **(agreed, client question 3)**
- **One line per weapon.** "Pike: piercing, reach 2, Set." **(lead's
  call)**
- **Every verb is previewed** before the player commits (7.2 rule 7).
  **(settled principle, ability model 8.3)**
- At most four things to read per unit in combat. **(settled, ability
  model 11.4)**

### Non-goals

- **Durability, wear and weights.** The agreed non-goal stands unchanged;
  section 5 records why and what replaces it. **(agreed, client question
  1; settled, enemies 2, 15.9)**
- **Ammunition tracking.** **(lead's call, adopting items design 2 and
  17's recommendation)**
- **Weapon buttons** (one action button per weapon on its own pool).
  **(agreed, framework I1)**
- **Fidelity to the SRD 5.2 weapon table or mastery rules.** We borrow the
  idea of one short effect per weapon and six mastery effects, adapted and
  attributed (section 19). **(settled, main design 2)**
- **A cover rule in M1** (9.5). **(lead's call)**

### Anti-goals

- **Dead weapons.** A looted weapon that most of the party cannot use, or a
  family nobody equips after floor 2. **(lead's call)**
- **Bring-the-right-key play.** Carrying one weapon per enemy type and
  swapping. The bonus-action swap prices it. **(settled, stat model 2;
  ability model 7.2)**
- **Flat +N weapons.** A tier is a value budget, never +1 to hit or
  damage. **(settled, items design 12.4, 12.5)**
- **Save and condition noise.** A verb that rolls a save on every swing.
  **(settled, ability model 9.3)**
- **Bookkeeping.** Any count the core could track. **(settled, ability
  model 2)**
- **Protected content.** **(settled)**

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Stat model 5.2 | Strength: "Melee and thrown attacks and damage". Dexterity: "Attack rolls with ranged and light weapons". | Who rolls what (6.2). Both rows are reworded by W-1. |
| Stat model 5.3 | "Dexterity adds to hit but not to damage. Strength adds to both for melee and thrown weapons." | Dexterity-rolled attacks add no ability to damage except a negative Strength (6.2, W-1) |
| Stat model 6 | Attack bonus: ability + proficiency if trained in the weapon. Natural 20 always hits and crits. | Training is soft (10.2) |
| Stat model 8.1, 8.2 | Each damage type has a rider; a rider is attempted only when flagged or on a crit, and lands automatically on any hit of a type the target is weak to **(agreed)** | Mundane weapons carry no rider flag (12); this design does not limit weak-target riders (7.2 rule 9) |
| Ability model 6.6 | Martial Techniques: spent on a hit to add the weapon's rider, push 1 or add 1d6 | Class hooks into verbs (7.3) |
| Ability model 7.2 | Attack (Extra Attack at level 5), Shove, swap weapon sets (bonus action), opportunity attack (reaction) | Weapon attacks plug into these |
| Ability model 7.3 | Reactions resolve on automatic policies; reactions do not nest | Guard verbs declare one, with an optional target filter (7.1, W-4) |
| Ability model 9.3 | At most one rider per target per use; an at-will's rider at most once per turn per attacker; no rider on a target that already has the condition | Bounds on verbs (7.2) |
| Ability model 14.3 | `UseAbility` steps: roll, damage, concentration, rider, effects; reactions hook between the roll and the hit | Expanded for weapon attacks by the pipeline (7.5) |
| Conditions 6.3 | A re-application with an equal value and no later effective end has no effect | Notched cannot be re-applied while it lasts (5.6) |
| Conditions 10.1 | Forced movement: away or toward; stops at walls and units; collision damage; never provokes | Push, Hook and the new `shift` kind (W-8, 7.6) |
| Conditions 16 | At most 32 conditions in M1 and M2 | Verbs use existing conditions only (7.2 rule 6) |
| Conditions 18.3, 18.9, 18.10 | Disarmed (Later), Coated (Later), Pitted Armour (Later): conditions on the unit | Sections 5.5, 5.6, 16 |
| Enemies 5.4 | At most one die per at-will part; a monster rolls one die of the weapon's largest size | Revised by W-5 (15.1) |
| Enemies 5.3 | Dexterity-main monsters add Dexterity to hit, not damage; their flat damage is declared on the attack | Monster flat damage is unaffected by 6.2 (15.1) |
| Enemies 6.2, 6.4 | Target table and permadeath guards | Every monster weapon is checked against them (15) |
| Enemies 9.1, 9.2 | One reaction beyond the opportunity attack; at most 2 harmful conditions per type | Guard and condition verbs count (15.2) |
| Enemies 15.1, 15.9; items design 2, 3.1 | "No durability, wear or weights" | Stands (section 5) |
| Items design 5.2, 7.1 | Active and spare weapon set; swap is a bonus action; pickup to an empty spare set | Hands and grips (11) |
| Items design 12.2, 12.3, 12.9 | Family record with `improvised`; `weapon{}` sub-record owned by this design; requirements `hard` or `soft` | Data shape (20.1) |
| Items design 12.4–12.6 | Buff test; rarity is a value budget; at most 3 properties | Property tables (18) |
| Items design 17, row 1 | "No ammunition tracking; thrown weapons stay with the thrower; ammunition effects are properties on the weapon" | Adopted (9.4) |
| Main design 2 | No D&D names; SRD content carries attribution | Licensing (19) |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items design 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored.

## 4. Framework decisions

Six framework decisions shape everything else. The client chose all six on
2026-11-17. **(agreed)**

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Durability (Q1) | **(a) Decline.** The agreed non-goal "No durability, wear or weights" stands unchanged. Substitutes: charged weapon powers, consumable coatings, wear inflicted on enemies (Later), opt-in Shatter (Later) (section 5). | (b) amend the non-goal for a Keenness pool: two more pools per unit, duplicates the martial Technique, breaks the monster one-die rule and the floor-1 guards. Recorded as a declined backlog fallback only (5.7). |
| 2 | Notched (Q2) | **Yes, as a Later condition** on the unit, exactly as in 5.6 (change W-12) | No: loses the one honest answer to "should weapons wear down in combat" for the cost of one Later condition |
| 3 | Where weapon identity lives | **I1: the weapon owns its verb; any trained wielder uses it automatically; classes own frequency and upgrades** | I2, class-unlocked masteries (SRD 5.2 as written): looted weapons dead for most of the party, companion kits need picks, "mastery" already means skill mastery (stat model 7.2). I3, weapon buttons on pools: breaks "at most three named pools" and "four things to read". |
| 4 | Families, groups, training | **G1: about 30 SRD-sized families; simple, martial and exotic training; groups are tags only** | G2, about 12 broad families: a family cap of 2 starves builds and weapons in a group play alike. G3, training by group: more class data, more soft penalties to read. |
| 5 | Dual wielding | **D-b: the off-hand light attack costs the bonus action and adds no ability to damage unless negative; the dagger's Nick moves it into the Attack action.** D-c is the named fallback (11.3). | D-a, cut: loses a classic build and Dexterity's main damage route. D-c, a passive paired damage part: no decision, reads as a flat bonus. |
| 6 | Foci | **F3: a focus takes a hand, gives a focus verb to spells of its tradition, carries properties, and is never required for casting** | F1, casting requires a focus: Disarmed or an empty set locks a caster out ("slowed, never helpless"). F2, properties only: foci have no identity. |

And on Dexterity damage (client question 3) **(agreed)**:

- **Option (b).** The W-1 reading is approved: Dexterity-rolled attacks add
  no ability to damage (Strength only if negative, melee and thrown);
  `light` and `finesse` are defined; the stat model's Dexterity row reads
  "ranged, light and finesse weapons" (6.2).
- **Level-scaled Exploit** is approved, for every unit, monsters included,
  counted at expected value in monster validation (6.3, 15.1).
- **One precision feature per Dexterity-main class** is a binding
  requirement on the class designs (6.4).
- **Precision for launchers only** is pre-approved and **off by default**.
  The lead switches it on only through the gate in 6.5.
- Rejected: (a) without the Precision fallback, which would need another
  approval cycle on a gap the numbers already predict; (c) no
  Dexterity-keyed damage at all, which leaves Dexterity at 40–80% of the
  reference with weapons alone (6.4).

And on Aim (client question 4, raised by the play check; answered on
2026-11-17) **(agreed)**:

- **Option (a).** Aim is rewritten as "On your own turn, your first attack
  made before you move counts as having advantage for Exploit (it does not
  gain advantage)", and the keep-higher damage roll is dropped (7.1). It
  widens the trigger of the approved Exploit rule (6.3) to an Aim attack,
  so the longbow can trigger Exploit by itself (6.4).
- Rejected: (b) keep Aim as written, which left the longbow falling from
  52% to 38% of the reference with level, under the per-family floor, with
  only a chassis re-tune to fix it.

Everything else in the lead's proposal stands as written, and is recorded
below as **(lead's call)** unless a parent settles it. **(agreed)**

### 4.1 How other games handle weapons

Mechanism references only; no names or text are taken from any of them.
**(lead's call)**

| Game | What it does | We take | We avoid |
|---|---|---|---|
| **5E SRD 5.2** (weapon masteries) | Each weapon has one mastery: Cleave, Graze, Nick, Push, Sap, Slow, Topple, Vex. A class unlocks masteries in a few weapons. | **One short verb per weapon**, and six SRD names and effects (adapted, CC-BY-4.0, attributed) | Class-gated unlocks. Sap and Slow (7.2 rule 6). |
| **Brogue** | Weapon *shapes*: the spear hits the unit behind, the axe hits all adjacent, the rapier lunges | Geometry verbs that read on the board: Skewer, Cleave, Lunge | Its names and text. Its permanent acid corrosion of gear is one reason we decline durability. |
| **Battle Brothers** | Weapon types grant skill buttons; polearms strike from the second rank; spearwall, shieldwall, split shield; gear wears out and needs tools | Second-rank reach over allies, a spearwall-like reaction (Set), an anti-shield verb (Pry) | One button per weapon; tool and durability bookkeeping |
| **Into the Breach** | Push and pull are the main tactical verbs; every result is previewed | Positional verbs with a previewed landing square | Collision damage on every hit (7.2 rule 4) |
| **Baldur's Gate 3** | Weapon actions as buttons recharging on a short rest | A weapon carries a limited power: charge properties on notables (18) | Buttons by default (close to the player cooldowns the ability model rejected) |
| **Diablo, Path of Exile** | A base item with a built-in property, plus rolled affixes | **Verb signature tables**: properties that deepen the family's own verb (18.4) | Flat +N affix piles (the buff test bans them) |
| **Fire Emblem, Breath of the Wild, Divinity OS1 and OS2** | Weapon durability | Lessons only: durability caused hoarding and was softened or removed over these series (as recalled: Fire Emblem from Fates on; OS2 removed it) | Durability (section 5) |
| **Monster Hunter** | A sharpness gauge that wears within a hunt and is restored cheaply; nothing is lost | The best case for wear, weighed in 5.2 | A per-unit meter (5.3) |
| **XCOM** | A few readable numbers; overwatch | Overwatch as a launcher property (18.3); previews for everything | — |

## 5. Durability: verdict and substitutes

**(agreed: decline, client question 1, option a; Notched yes, client
question 2)**

### 5.1 What the approved documents already say

| Document and section | Exact text |
|---|---|
| Enemies 2, Non-goals | "Durability, wear, weight or junk loot. **(agreed)**" |
| Enemies 4, decision 1 (option C) | "…one loot summary per fight, no durability." |
| Enemies 15.1, rule 5 | "No durability, wear or broken drops." |
| Enemies 15.8, friction targets | "no durability or weights (the settled slots are the only constraint)" |
| Enemies 15.9, "Decided here, binding on the items design" | "No durability, wear or weights. **(agreed)**" |
| Items design 2, Non-goals | "Weight, bulk, durability or wear. (settled, stat model 6; enemies 2)" |
| Items design 3.1, constraints table | "No durability, wear or weights" → "None of the three exists" |

- The stat model covers only weights ("No weights", stat model 6), so items
  design 2's citation should credit durability to enemies 2 and 15.9 only.
  This is a citation fix, folded into change W-6. **(lead's call)**
- **An approved precedent for temporary wear exists: Pitted Armour**
  (conditions 18.10): minor, Later, cap 2, `untilCamp`, repaired by
  Mechanics during a rest, a condition on the unit, never item state
  ("There is no `tempMods` field", items design 18.2). Notched follows it.

### 5.2 The case for durability, weighed

- It makes the weapon a resource: every attack becomes push or save.
  Monster Hunter's sharpness is the well-loved example: it wears within a
  hunt, is restored cheaply, and nothing is lost.
- It gives the spare weapon set and Mechanics more to do.
- "Your weapon is being eaten" is classic dungeon flavour (the SRD rust
  monster, the black pudding).
- Fire Emblem: Three Houses kept broken weapons usable but weaker, and its
  per-attack "spend uses on a combat art?" choice was central to its
  combat.

The strongest version argued in review was **Keenness**: a pool of 3 per
weapon set, refilled at a rest; spending 1 adds a weapon die; at 0 the set
is Blunted (dice one size smaller); never breaks, costs no coin, never
stored on the item.

### 5.3 The case against, for this game

1. **Loot is scarce and hand-picked:** about 6–8 notables per floor
   *(illustr.)*, family caps of 2 per run, duplicate rerolls, no junk. A
   broken notable usually cannot be replaced; repair cheap enough to
   prevent that is a tax with no decision in it.
2. **Permadeath:** a martial unit makes about 40–55 attack rolls per floor
   *(illustr.)*, so 2–3 natural 1s. Break-on-a-1 destroys gear at random
   with no counterplay the preview can show, the heirloom included.
3. **Attrition is already handled** by HP, supply (cap 3), `rest` and
   `camp` pools, potion stock and the tuned target of reaching the boss
   with about half the camp power (ability model 2). A fourth clock would
   invalidate the simulator's attrition tuning.
4. **There is no inflation to sink.** Coin dies with the run, and repair
   costs would compete with supply against "coin cannot replace
   attrition" (items design 10.5).
5. **Readability:** four units × two weapon sets is 8 new meters, against
   four things to read and the 10-second loot summary.
6. **Symmetry and the core:** monster issue gear has no instance, so wear
   would be party-only or hidden per-monster state; leftovers, flee piles
   and Disarmed returns would need item state, which items design 18.2
   removed on purpose.
7. **It taxes the core verb.** Combat is the fun part; durability charges
   the player for attacking and rewards hoarding the best weapon.

**Keenness specifically** still costs too much: two more pools per unit; a
duplicate of the martial Technique ("spend a charge, add a die"); a free
martial resource for casters and companions; a monster Overstrike adds a
die to an at-will, breaking enemies 5.4 and the floor-1 guards (enemies
6.4); its whetstone would be a bought pool refill (the "free refills"
anti-goal); and it amends an agreed decision for a mechanic comparable
games dropped.

### 5.4 Verdict per variant

| Variant | Verdict |
|---|---|
| Full durability with breakage | **Rejected** |
| Repair costs (wear without breaking) | **Rejected:** a coin tax with no decision |
| Break on a natural 1 | **Rejected** |
| Notched or chipped stored on the item | **Rejected:** item state (items design 18.2) |
| Keenness pool | **Declined; backlog fallback only** (5.7) |
| **Notched as a temporary condition on the unit** | **Accepted, Later** (5.6) **(agreed)** |
| **Charged weapon powers** (`rest` or `camp` charges) | **Accepted** (5.5) |
| **Consumable coatings** (special arrows, edge oils) | **Accepted, Later, as consumables** (5.5) |
| **Wear the party inflicts on enemies** (Sundering, Crushing) | **Accepted, Later** (5.5) |
| **Opt-in Shatter** | **Accepted, Later** (5.5) |

### 5.5 The substitutes

What durability would have given, delivered without wear on the party's
gear. **(agreed)**

| Substitute | What it gives | Rule | Wave |
|---|---|---|---|
| **Charged weapon powers** | Use-or-save tension with no permanent loss | Notable properties with a `rest` or `camp` pool (buff-test tag `charge`), through `grants.abilities {ability, cost, pool}` (items design 12.3). Listed in 18.2–18.4. | M1 |
| **Consumable coatings** | Spending special ammunition and oils | Special arrows and bolts, and edge oils, are `coating` consumables (items design 5.1 `stackMax`) that apply Coated: "its next 3 weapon hits that attempt the coating's rider; a charge is spent only on such a hit (ability model 9.3 gates apply)" (conditions 18.9; consumables design 7.5) **(change CN-18)**. Tagged `launcher` or `blade` (W-11). Never both beltable and wieldable (items design 19.1 rule 6). | Later |
| **Wear on enemies** | "Break their gear" without item churn | **Sundering** (18.3): an armed monster uses its improvised attack for the fight. **Crushing** (18.3): Pitted Armour 1. | Later |
| **Opt-in Shatter** | A big, chosen sacrifice | **Shatter** (18.3): once, destroy this weapon as part of a hit, which becomes a critical and pushes 2. Rare and above only, never on heirlooms or signature items. The player chooses, so it is never a tax (W-6, items design 12.8). | Later |
| **Notched** | Grit from rust and acid monsters | 5.6 | Later |

### 5.6 Notched (Later)

**(agreed: exactly this rule; change W-12)**

- **Tier:** minor, binary, lingering, harmful.
- **Effect:** the weapons in the weapon set that was active when Notched
  landed roll their dice one size smaller (minimum d4) and lose their verb.
  On a focus it suppresses only the focus verb. Shields and natural weapons
  are unaffected.
- **The instance records the weapon set,** the way Teetering records its
  hazard. **(lead's call)** It records the instanceIds of the items in that
  set when it lands, not the set's slot, so rearranging the sets out of
  combat does not shed it. Handing a recorded weapon to an ally also
  escapes it; this is accepted as the same escape as the set swap.
- **Landing (lead's call):** Notched applies only if the active set holds
  at least one weapon or focus it would affect (not a shield, a natural
  weapon or a Sure Grip weapon). Otherwise it does not apply, takes no
  lingering slot, and the rider or effect counts as not attempted.
- **Re-application (settled, conditions 6.3):** a unit that already has
  Notched cannot gain it again: the equal value and the same `untilCamp`
  end mean "No effect: already Notched". The recorded instanceIds stay as
  they are, so a unit that swapped sets is not Notched again until the
  instance ends.
- **Duration:** `untilCamp`. A **Mechanics repair during a short rest**
  ends it: a fixed difficulty (`notchedRepairDifficulty`, 20.2), one
  attempt per instance per rest, **no free retry** (settled, stat model
  7.2). **(lead's call)** Any party member may attempt it, trained or not,
  as for Pitted Armour; the instance stores `lastRepairRestId` to enforce
  one attempt per rest.
- **Lingering cap:** counts toward the cap of 2 harmful lingering
  conditions (conditions 14).
- **Sources:** rust, acid and ooze-themed monsters only, **floor 2 and
  deeper**, and **visible from the monster type** (its gear line or type
  plate says "Notches weapons"). It counts toward that type's 2 conditions
  (enemies 9.2). **Party-facing only** in this wave: no party source
  Notches monsters, whose issue weapons have no sets. **(lead's call)**
- **Counterplay:** swapping to the spare set (a bonus action) escapes it.
  The **Sure Grip** property (18.2) makes a weapon immune.
- **Storage:** a condition on the unit; never item state. The agreed
  non-goal stands.
- **Budget:** a Later condition, outside the 32-condition M1 and M2 budget
  (conditions 16); conditions 18.1's Later count rises from 69 to 70
  (W-12).

### 5.7 Keenness: the declined backlog fallback

**(agreed)** Keenness (5.2) is recorded only as a declined fallback, in
case playtests show basic attacks feel flat. It is not designed further
here. Reviving it amends an agreed non-goal, so **only the client can
reopen it**; the builders and the lead may not ship it as tuning. **(lead's
call on who may reopen it)**

## 6. Attacks and damage

### 6.1 Layers of a weapon

**(lead's call; all data)**

1. **Chassis:** `dice[]` and versatile dice; damage type; hands
   (one-handed, versatile, two-handed) and the `light` and `finesse` flags;
   reach (with `minReach`) or range; `thrown`, `loading`, `pointBlank`;
   Strength class; training; size.
2. **Verb:** exactly one per family (section 7).
3. **Notable properties:** 1–3, under the buff test, from the band tables
   (section 18).

The damage type's rider (stat model 8.1) comes with the chassis, and fires
only on a critical hit, against a weak target, or when something flags it
(section 12). **(settled)**

### 6.2 Attack ability and damage

**(agreed: W-1, client question 3)**

| Weapon | To hit | Damage adds |
|---|---|---|
| Launchers (bows, crossbows, sling) | Dexterity | No ability **(settled)** |
| `light` and `finesse` melee; light thrown | The better of Strength and Dexterity (below) | **Strength-rolled:** Strength. **Dexterity-rolled:** no ability, except a negative Strength. |
| Everything else, melee or thrown | Strength | Strength, positive or negative |

- **`light`** means dual-wieldable (11.3) and Dexterity-eligible.
  **`finesse`** means Dexterity-eligible only (rapier, whip). **(agreed)**
- **Choice of ability (lead's call):** for a `light` or `finesse` attack
  the core picks the ability with the higher expected damage; ties go to
  Dexterity. There is no player toggle: the choice has no content the core
  cannot compute.
  - **Formula,** integers only (3.2): for each ability,
    `E = hitPermille × avgHitX2 + critPermille × critExtraX2`, where
    `avgHitX2` is twice the average damage of a hit (dice, ability term,
    Exploit if the attack qualifies for it (6.3), Precision if on) and
    `critExtraX2` twice the extra average a crit adds. Compared as
    integers; no division is needed.
  - **Knowledge:** a party unit uses the preview's knowledge state
    (ability model 8.3: unrevealed resistances hidden); a monster uses the
    true state.
  - **Once per attack:** computed at step 3 of the pipeline (7.5), after
    the roll mode is fixed, and
    recorded in the `AttackDeclared` event; resolution reuses it, so the
    preview and the resolution can never pick differently.
  - **Monsters** do not choose: each monster at-will declares its
    `ability` in data (enemies 5.2 already speaks of "the attack's
    ability"). A monster's flat damage comes from its row and is not
    touched by the no-ability rule above (enemies 5.3). `natural.*`
    templates roll Strength unless the template declares Dexterity.
- **Why Dexterity adds no Strength:** it stops a Dexterity +3 / Strength
  +2 build from taking both Dexterity accuracy and Strength damage, which
  review measured at 120–133% of the best Strength build. **(agreed)**
- Total damage of a hit is at least 1 unless immune. **(settled, stat
  model 8.3)**
- The stat model's Dexterity row becomes "Attack rolls with ranged, light
  and finesse weapons" (W-1). **(agreed)** The Strength row and the 5.3
  sentence on Strength are reworded to match the same approved reading
  (W-1). **(lead's call on the wording)**

### 6.3 Exploit

**(agreed: rule, level scaling and the Aim trigger; details lead's call)**

> **Exploit.** When an attack rolled with Dexterity hits with advantage,
> it deals extra weapon dice: 1 at levels 0–4, 2 at levels 5–8, 3 at
> levels 9–12. Once per turn per attacker.

- **Trigger: advantage OR an Aim attack (agreed, client question 4).** An
  attack qualifies if it has net advantage (below) **or** it is an Aim
  attack: the first attack of the wielder's own turn, made before the
  wielder moves (7.1, 7.6). An Aim attack counts as having advantage **for
  Exploit only**: its roll mode is unchanged, so it rolls one d20, and Vex
  and every other rule that reads the roll mode see its real net mode. An
  Aim attack with net disadvantage still qualifies; one with net advantage
  qualifies once, as any attack does.

- **Every unit**, monsters included, **counted at expected value** in a
  monster's validation, in the at-will band and in the permadeath guards
  alike (15.1). **(agreed)**
- **Bands (lead's call):** the proficiency bands; levels 0, 11 and 12 are
  monster-only.
- **Advantage is the net roll mode (lead's call):** outside Aim, Exploit
  needs net advantage after one advantage cancels one disadvantage
  (conditions design); an attack with both is a normal roll and does not
  qualify unless it is an Aim attack.
- **Which die (lead's call):** each Exploit die is the attack's actual
  damage die after every size change (versatile grip, Notched; 6.6), or a
  monster's one-die-rule die (15.1), dealing the weapon's damage type. It
  is a separate damage part of the same type, so a critical hit doubles it
  like any dice.
- **Which hit (lead's call):** the first qualifying hit in the turn, applied
  automatically; it cannot be held for a later hit. Off-hand attacks and
  reactions qualify. "Turn" means **the turn now being taken, by any
  unit** (20.3): an opportunity attack with advantage on another unit's
  turn uses that turn's allowance, as the camp-spend flag does (ability
  model 6.4).
- **Never on** improvised or unarmed attacks (13), and never from a
  Strength-rolled attack.
- **The source of advantage does not matter:** a Vex Helped, a Lunge, a
  Prone target, an ally's Help. If team simulator gates (21.3) show
  overshoot, the named fallback is "Exploit counts only advantage the
  attacker generated itself"; an Aim attack is self-generated and still
  qualifies under it. **(lead's call)**
- **Validation rates (lead's call):** a monster's expected Exploit per hit
  is `exploitAdvantageRatePermille` × the die average, from `weapons.json`
  (20.2): 250‰ by default, 1000‰ when the unit's own kit can give itself
  advantage (Lunge, Help, Charge, its own Topple or other Prone source) or
  carries Aim (a longbow).
  The tail is left to the round-1 0.5% guard (enemies 6.4), which
  simulates the real advantage sources.

### 6.4 Dexterity parity

**What weapons alone deliver.** Assumptions: main ability +3 / +4 / +5 at
levels 1 / 5 / 10; on-level AC (enemies 6.2); 65% hit chance; Extra
Attack at level 5; crits roll double dice; Dexterity builds have Strength
0. Expected damage per round *(illustr., Monte Carlo)*:

| Build (weapons only, no class features) | Level 1 | Level 5 | Level 10 |
|---|---|---|---|
| **Greatsword (Graze): the reference** | 7.9 (100%) | 17.3 (100%) | 19.2 (100%) |
| Greataxe | 82% | 83% | 81% |
| Longsword and shield | 65% | 66% | 67% |
| Shortsword (Vex) + dagger (Nick), Exploit 1/2/3 | 79% | 75% | 82% |
| Same, Exploit fixed at 1 die | 79% | 59% | 53% |
| Rapier (Lunge on half of turns) | 76% | 66% | 70% |
| Shortbow (Vex) | 53% | 64% | 72% |
| Longbow (Aim, agreed rewrite)* | ~74% | ~68% | ~76% |

\* The longbow row is the lead's estimate for the agreed Aim (7.1; client
question 4), assuming the first shot of each turn is planted *(illustr.)*;
it is not from the Monte Carlo run. Under the earlier keep-higher Aim the
run gave 52% / 42% / 38%.

- Weapons bring Dexterity melee to about 75–80% of the reference and
  launchers to about 50–75%. A fixed one-die Exploit collapses at level 5,
  which is why it scales.
- Launchers trade damage for range and safety. The longbow is the safest
  family. Aim lets its planted shot trigger Exploit by itself, so its
  share holds with level instead of falling **(agreed, client question
  4)**; it stays on the dead-weapon watch list and is held to the
  per-family floor below.

**Parity targets (lead's call, illustr.):** with class features included,
at levels 1, 5 and 10, against the greatsword reference built on the same
class's Strength-main spread:

| Build | Target |
|---|---|
| Best Dexterity melee build of each Dexterity-main class | ≥ 90% |
| Best launcher build of each Dexterity-main class | ≥ 75% |
| Every launcher family, trained wielder, its best class build (M2 parity set) | ≥ 60% |
| Any build, measured as VG-12: rest-pool damage floor-averaged at the worst legal build (every legal capacity step); camp-pool damage bounded by the nova bound and `CampOptionBound` (`actionsGranted` ≤ the base option's; was `SurgeOptionBound`) instead **(change VG-12; Vanguard design 10.1; renamed by change SH-5, Shade design 17)** | ≤ 110% |

**Binding requirement on the class designs (agreed):** every class whose
main ability is Dexterity has at least one **precision feature**: a class
feature that turns Dexterity's accuracy into damage, keyed to conditions
Dexterity builds create (advantage, a flank, a first strike, a marked
target), such as once-per-turn extra dice. Each such class design shows,
with simulator numbers, that its best Dexterity melee build meets the
target above from M2, and reports its best launcher build against the 75%
target, stating whether its precision feature applies to launcher attacks
and, if not, why. **(lead's call on the evidence asked for)** The
parity runs model reaction attacks, Opening's Answer trigger and both
contexts (solo floor 1, reference party); precision dice scale by class
level; the evidence is single-class, and multiclass Dexterity builds get a
separate M4 report against ≤ 110% (classes design 7.7, 8.4) **(change
CL-8)**.

### 6.5 Precision for launchers (pre-approved, off by default)

**(agreed: the rule and its default; the gate is the lead's call)**

> **Precision.** A hit with a launcher, rolled with Dexterity, deals +1 at
> levels 0–4, +2 at levels 5–8, +3 at levels 9–12, of the launcher's
> damage type.

- It ships **switched off**: `weapons.json` → `precisionLaunchers.enabled:
  false` (20.2).
- When on, it applies to **every unit**, monsters included, counted in the
  band and guards at its value on every hit (it is flat, so a crit does not
  double it). **(lead's call, from "resolved symmetrically", enemies 2)**
  A monster launcher's flat damage is set so that its total, Exploit and
  Precision included, stays in its row band (15.1): switching Precision on
  lowers the row flat, not the monster's total.
- **The gate (lead's call).** The lead switches Precision on if, and only
  if, all of the following hold:
  1. it is measured in the **M2 simulator**, after every Dexterity-main
     class's design is in data, precision features included;
  2. the measure is expected damage per round **at level 5**, single
     on-level standard target (enemies 6.2 row 5), over the fixed-seed
     parity scenario set (21.3), for each Dexterity-main class's best
     launcher build;
  3. **at least one Dexterity-main class's** figure is **below 75% of the
     greatsword reference** (6.4) at level 5;
  4. each class below that line has shown, under the binding requirement
     (6.4), whether its precision feature applies to launcher attacks, and
     why not if it does not (a class with no precision feature in data
     is the class design's defect, and the gate waits for it); and
  5. **safety veto:** launcher-main units do not take less than 50% of
     the damage melee-main units take per fight in the same parity set
     (21.3, "Damage taken by range band"). If they do, launchers are
     already paid in safety, and Precision stays off.
- **When switched on:** re-run the parity set at levels 1, 5 and 10. If
  any launcher build then exceeds 110% of the reference, the lead lowers
  the band values or switches it back off. Content validation and the
  enemies 6.4 guards must pass for every monster that carries a launcher
  with Precision on; the build does not ship until they do.
- **Limits:** the lead may switch it on or off and lower its values.
  Raising it above +1 / +2 / +3, or extending it beyond launchers, needs
  the client. The switch is logged in the tuning file's change history and
  reported to the client when made.

### 6.6 Damage dice

**(lead's call)** One rule for every way the dice of an attack change:

- **Size changes come first:** the versatile die when the off hand is
  empty (11.2), then Notched (one size smaller, minimum d4). The result is
  the attack's **actual damage die**. Thrown attacks use the family's
  one-handed `dice`, never the versatile die (9.3).
- **Exploit dice** are that actual die (6.3); a monster's are its
  one-die-rule die (15.1).
- **Crit doubling** doubles the weapon dice, the Exploit dice and a
  class's dice part placed before it (Opening's `damage.extraDice.opening`);
  flat terms (ability, Precision, row flat, Opening's rank-I
  `damage.flat.opening`) are not doubled. Opening's effectKinds are named
  apart from Precision (6.5) **(change SH-3; Shade design 6.4)**.
- **Executioner's Stroke** re-rolls the **weapon dice part only** (crit
  dice included; Exploit and Precision excluded) and keeps the higher
  total. Aim changes no dice: it only makes an attack qualify for Exploit
  (6.3), whose dice follow the bullets above. **(agreed, client question
  4, for Aim)**
- The total is at least 1 unless the target is immune (settled, stat model
  8.3).

## 7. Verbs

### 7.1 The 15 verbs

**(lead's call; framework I1 agreed)** These texts are canonical. Family
records reference a verb and cannot override its text.

| Kind | Verb | Source | Text | Wave |
|---|---|---|---|---|
| Position | **Push** | SRD 5.2, adapted | On a hit, you *may* push the target 1 square straight away. It cannot push a unit more than one size larger than you. | M1 |
| Position | **Hook** | original | On a hit, you *may* pull the target 1 square toward you. If it is already adjacent, you may instead **shift** it up to 2 squares around you, to another unoccupied square adjacent to you (7.6, W-8). Same size limit. | M2 |
| Position | **Topple** | SRD 5.2 | On a hit, the target makes a Fortitude save against your ability difficulty or falls Prone (tag `trip`). | M1 |
| Reach-out | **Cleave** | SRD 5.2, adapted | Once per turn, after a melee hit, make one attack roll against a different hostile adjacent to the first target and within your reach. Damage is the weapon's dice only (Strength only if negative). | M1 |
| Reach-out | **Skewer** | original | Once per turn, on a hit, the unit on the next square beyond the target along your line also takes the weapon's dice if the same roll meets its AC (7.6). Allies are never hit; the effect stops at them. | M1 |
| Tempo | **Nick** | SRD 5.2 | The off-hand attack made with this weapon is part of the Attack action instead of a bonus action, once per turn. It may be a throw. | M1 |
| Tempo | **Vex** | SRD 5.2, adapted | Once per turn, on a damaging hit made **without advantage** (net roll mode normal or disadvantage), gain Helped. It ends at the end of your next turn (W-7). | M2 |
| Tempo | **Lunge** | original | On your own turn: if you moved at least 2 squares this turn and are adjacent to a target you were not adjacent to at turn start, your first attack against it after that movement has advantage (7.6). | M1 |
| Tempo | **Flourish** | original | On your own turn, once per turn, after a melee hit, you may step 1 square without spending speed. The unit you hit makes no opportunity attack for that step; other units do. | M2 |
| Tempo | **Aim** **(agreed)** | original | On your own turn, your first attack made **before you move** counts as having advantage for Exploit (it does not gain advantage) (6.3, 7.6). | M1 |
| Guard | **Parry** | original | Reaction, policy `ifItChangesOutcome` with target filter `nonMinion`: +2 AC against one melee attack roll. | M1 |
| Guard | **Set** | original | Reaction: when a hostile enters your threatened zone from outside it by its own movement, make one attack with this weapon. Default policy `always` with target filter `nonMinion`. | M1 |
| Penetrate | **Graze** | SRD 5.2, adapted | Once per turn, when a Strength attack with this weapon misses, the target takes your Strength modifier (if positive) as the weapon's damage type. No rider, no verb. Never on a miss caused by a reaction (Parry, Interpose or any reaction AC bonus; exact test in 7.6) **(change AR-42)**. A natural 1 allows it. Minions take none. | M1 |
| Penetrate | **Pry** | original | This attack ignores the target's shield bonus and Interpose. | M1 |
| Penetrate | **Puncture** | original | This attack doubles piercing's armour reduction. | M2 (with riders) |

### 7.2 Bounds

**(lead's call; new engine rules, in the change list)**

1. **A verb is not a rider.** It has its own preview line and does not use
   the one-rider-per-target slot. One exception: **Topple shares the save
   slot with riders**; if a rider is attempted on that target in that use,
   Topple is not (W-4).
2. **Topple** fires at most **once per round per target**, and is never
   attempted against a Prone target (W-10).
3. **Condition verbs never fire on reactions:** not on opportunity attacks,
   not on Set attacks (W-10). **Self-movement and own-turn verbs never
   apply on reactions either:** Flourish, Driving, Lunge and Aim apply only
   on the wielder's own turn. Push and Hook may apply on reactions,
   subject to rule 4. Cleave, Skewer, Graze, Pry, Puncture and Exploit
   apply on reactions.
4. **Forced movement (lead's call; W-8).** All forced movement one hit
   causes, from every source (verbs, verb signatures, Techniques, charge
   properties), **sums to at most 3 squares and resolves once** under
   conditions 10.1, at step 10 of the pipeline (7.5). Opposite directions
   cancel square for square; a shift (7.6) and a push or pull on the same
   hit cannot combine, and the shift is dropped.
   - **The 3-square sum** counts squares **actually moved, across all
     moved units** (a Domino link at step 10b uses only the remainder).
     Parts are listed in data order, non-verb parts first, then verb
     parts; a verb part whose target was already verb-moved this round is
     dropped first and is not a stop; the total is cut to 3 by trimming
     from the last part, and a part **contributes** if it keeps at least
     1 square after the cut. Only contributing parts count for collision
     eligibility and the verb-moved-this-round mark **(change VG-8;
     Vanguard design 6.4 rule 6)**.
   - **Stopped early:** at least 1 square of a move's chosen distance was
     blocked by a wall, obstacle or unit, a 0-square blocked move
     included; an ignored verb part is not a stop, and stopping before a
     hazard is not stopping early **(change VG-8; Vanguard design 3.4)**.
   - **Verb-sourced** movement is: the verbs in 7.1, the verb signatures
     in 18.4 (Heavy Push, Long Hook, Crack, Keep Back, Binding Parry,
     Driving Graze), the shield bash's Push (17.9) and armour verb
     signatures that move a target (Bull Rush) **(change AR-49; armour
     design 14.4)**, and every **Empower** effect (the "empower the
     weapon's verb" Technique, 7.3) **(change VG-8; Vanguard design
     6.6)**. Everything else
     (a Technique push, Thunderous, Shatter) is **not verb-sourced**.
   - **Once per round** applies to verb-sourced movement only: a target
     already moved by a verb-sourced move this round ignores any further
     verb-sourced part; the per-target round state records only those.
   - **Collision damage** applies to the combined move only if a
     non-verb-sourced part contributed to it; a move made only of
     verb-sourced parts never deals it. The one exception is the
     Wrecker's Battering Force: once per round, a verb-only move stopped
     early deals collision damage as if a non-verb part had contributed,
     and its parts stay verb-sourced for every other rule **(change VG-8;
     Vanguard design 8.3)**.
5. **A verb is off** when the wielder is untrained (10.2) or Strained
   (10.1), and on improvised and unarmed attacks (13).
6. **Existing conditions only.** A verb applies only positional effects,
   attack-level modifiers or existing **minor or token** conditions, never
   a major or hard condition on a plain hit. M1 and M2 verbs use only M1 and M2
   conditions (the 32-condition budget is full, conditions 16, 18.1);
   Later verbs may use Later conditions. **Sap and Slow are dropped on
   design grounds:** Slow duplicates Chilled (cold's identity); Sap
   collides with the condition Sapped and the deliberately cut Off-Balance.
7. **Preview hint.** Every conditional verb declares a `previewHint`:
   Lunge, an icon on qualifying squares; Aim, a "planted" badge, with the
   Exploit dice in the damage preview, that clears when you move or make an
   attack; Skewer and Cleave, the second target highlighted; Set and
   reach, rings on the threat overlay; Push and Hook, the landing square;
   Parry, the attack previewed at AC + 2.
8. **Class abilities and spells.** A class ability that makes a weapon
   attack declares `weaponAttack: true` and inherits the active weapon's
   dice, type, reach, ability and verb. Spells never use weapon dice; they
   use the focus (section 14).
9. **The at-will rider bound** (ability model 9.3, "an at-will's rider is
   attempted at most once per turn per attacker") covers Cleave and Skewer
   second targets: a flagged or critical-hit rider attempted on either
   target spends it. It does **not** cover weak-target riders, which land
   on every damaging hit as stat model 8.2 agrees; repeated hits on one
   target are already limited by ability model 9.3's "not attempted on a
   target that already has that condition" (W-4).
10. **Once per turn** means once per turn of any unit (6.3, 20.3): a
    once-per-turn verb, Graze, Exploit and once-per-turn property may fire
    again on a reaction during another unit's turn, unless rule 3 keeps
    them to the wielder's own turn.

### 7.3 Class hooks

**(lead's call)** Classes own depth, never access:

- a Technique option, **"empower the weapon's verb"** (for example Push 2,
  Topple with disadvantage on the save), spent like any Technique (W-3);
- a new offer kind, **"verb training (family)"**: training, and so the
  verb, in one named family, including exotic ones (W-3). Exotic has no
  family before Later (the bolas), so in M1 and M2 the offer kind covers
  simple and martial families only, and builders do not gate on the exotic
  branch;
- subclasses that key off verb ids or group tags;
- **verb borrowing:** a subclass may apply the canonical verb of a
  trained, non-Strained weapon in the wielder's own spare set (11.1).
  Signatures, dice, type, reach and properties never travel **(change
  VG-19; Vanguard design 8.1, 9.1)**;
- the binding precision feature of each Dexterity-main class (6.4).

### 7.4 Names

**(lead's call)**

- "Brace" is already an Anchored source, so the pike verb is **Set**.
- The UI and data say "verb" (`verb.push`), never "mastery", which means
  skill mastery (stat model 7.2).
- `verb.cleave` is a different id from the illustrative ability "Cleave" in
  ability model 10.3, which needs no change.

### 7.5 The weapon attack pipeline

**(lead's call; binding)** Every weapon attack, a class ability with
`weaponAttack: true` included, expands step 5 of ability model 14.3 into
these steps, in this order:

1. **Declare** the attack and its mode: melee, reach, throw, launcher,
   off-hand, opportunity attack or Set. **Grip (change AR-48; armour
   design 10.2):** declaration gains a `grip` input (`oneHand` or
   `twoHand`), legal only for a versatile weapon in a set whose off hand
   is empty or holds a buckler; it defaults to `oneHand` with a buckler,
   and the AI picks it by the scorer. `twoHand` with a buckler sets
   `bucklerOffUntilOwnTurnStart` at declaration, hit or miss, reactions
   and opportunity attacks included.
2. **Fix the roll mode:** collect advantage and disadvantage from every
   source, net them (one cancels one), and consume Helped if it applies.
   Exploit (6.3) and Vex read this net mode; Aim (7.6) is recorded here
   as a separate flag that qualifies the attack for Exploit and does not
   change the mode. **Gear roll-mode cap (change AR-40; armour design 8.1
   rule 6):** at most one gear-sourced roll-mode effect applies to an
   attack against a unit, chosen by priority (unlimited passives, then
   once-per-round, then charges; ties by slot order); suppressed effects
   are not spent; Overlap qualifies only on net advantage; gear roll-mode
   effects are suppressed while the target has Raise or Fortified.
   Hailguard is fixed here, never after the roll.
3. **Choose the ability** (6.2) and record it, with the mode, in
   `AttackDeclared`.
4. **Roll** the attack.
5. **Reaction window:** the target side's reactions (Parry, Interpose)
   resolve here (ability model 14.3). **One reaction AC bonus per attack
   (change AR-41; armour design 10.4):** by the window algorithm, in the
   order step 0 window fills (reaction AC bonuses from camp-pool
   abilities, no reaction spent, ordered by source unit order then
   creation order; the first fill closes the window and counts as "the
   one reaction") **(change VG-4; Vanguard design 7.7)**, then a step-0b
   carried ward (Held Guard), then pool-free
   target, then pool-free allies in unit order, then
   charges with the target's own first (3a its Tricks-paid wards, such as
   Sworn Guard, then 3b its Parrying property) **(change SH-9; armour
   design 10.4)**; no reaction AC bonus is offered
   after a gear-sourced disadvantage.
6. **Hit or miss, and crit.** On a miss: Graze (7.6), then the target's
   `answeredBy[]` is written, then step 6a, then step 6b, then stop. A
   natural 20 hits and crits **(change SH-8; Shade design 8.3)**.
   - **6a. Verb on a miss** (Sure Form, Weaponmaster): on a qualifying
     miss, only the verb's own effect runs, at its normal step (forced
     movement at step 10, a Cleave follow-up at step 12, Lunge's carried
     advantage at step 13), with no damage step, no save slot and no
     Empower line. The miss is not a hit for any rule **(change VG-8;
     Vanguard design 8.1)**.
   - **6b. `missedBy` hook:** reactions keyed to the miss (Counterstroke)
     are offered here, with legality re-checked (reaction left, not
     Staggered, attacker in reach, HP above 0), so a Graze that drops or
     Staggers the unit removes the offer. `onParrySuccess` means a Parry,
     or a `countsAsParry` ward, was applied and the roll missed. With
     `sameReactionFollowUp`, the follow-up runs in the same reaction when
     the ward spent the reaction or carries one forward, and spends the
     reaction when the ward was a step-0 fill that spent none **(change
     SH-8; Shade design 8.3)**.
7. **On-hit spends:** Techniques and `onHit` charge properties
   (Executioner's Stroke, Thunderous, Quake, Crushing, Pinning, Shatter)
   are chosen now, before damage.
8. **Damage** (6.6): the weapon dice with Executioner's Stroke, then
   Exploit dice (from net advantage or Aim), then Opening's dice, then crit doubling, then the ability term, Precision
   and flat terms (Opening's rank-I flat +2 among them); minimum 1 **(change
   SH-3; Shade design 6.4)**. Then step 8a, then any concentration check, which uses the
   DH 20.3 step-3a value **(change SH-6; Shade design 7.4)**.
   - **8a. Duck marker:** when the attack hits, the damage event is only
     flagged as Duck-eligible and the attacker recorded; nothing is
     offered here (the offer and the halving are DH 20.3, steps 3 and 3a)
     **(change SH-6; Shade design 7.4)**.
9. **Rider attempt, or Topple** in the shared save slot (7.2 rule 1).
   A save that depends on the movement (Ram's collision Prone, Topple
   under Upend) **reserves** the slot here and is rolled at step 11, as
   Thunderous's Dazed save is; a reserved slot blocks riders and Topple
   on that target even when no save is rolled. Every Prone source whose
   record has `sharesToppleCount: true` shares Topple's once-per-round
   count (7.2 rule 2), and a Ram suppresses the weapon's Topple verb on
   its hit. Under Upend, Topple II's push is dropped **(change VG-8;
   Vanguard design 6.4 rule 6, 8.3)**.
10. **Forced movement:** every forced movement this hit causes is summed
    (7.2 rule 4, across all moved units) and resolved once.
    - **10b. Domino link** (Wrecker): when the combined move is stopped
      early by a hostile, that hostile is pushed 1 square the same way,
      inside the remainder of the 3-square sum; the link collides only
      through Battering Force, then Brink applies **(change VG-8;
      Vanguard design 8.3)**.
11. **Other on-hit effects** in data order (Thunderous's Dazed save after
    its push, the reserved saves of step 9, Quake, Pitted Armour,
    Rooted).
12. **Secondary targets:** Skewer first, then Cleave, with geometry read
    **after** step 10. A secondary attack re-enters the pipeline at step 2
    with its own restrictions (7.6); Skewer reuses the step 4 roll.
13. **Self-buffs:** Vex's Helped.
14. **Self-movement:** Flourish, then Driving.
15. **Events:** `VerbFired` and `ExploitApplied` (20.3).

So Topple lands before a push (Prone, then pushed; under Upend its save
follows the push, step 9), Skewer and Cleave read
the board after the push, and Vex's Helped is in place before the next
attack of the same Attack action, a Nick attack included.

### 7.6 Geometry and timing

**(lead's call)**

- **Skewer's line.** The line runs from the centre of the attacker's
  square through the centre of the target's square and on. The **next
  square** is the first square the extended line enters after leaving the
  target's square; where it leaves exactly through a corner, the diagonal
  square. This gives the obvious square for the 8 directions and one
  defined square for any ranged offset, such as (5, 2) or a knight's move.
  - An empty square, a wall, or a square the attacker cannot see
    (symmetric visibility) ends the effect; it never carries on.
  - An ally on that square stops it. **Long Skewer** b1 skips exactly one
    ally square and checks the square after it; b3 then checks one more
    square beyond the second unit, under the same rules.
  - The second unit is hit if the same roll's total meets its AC (Pry
    applies to that AC, and Puncture to its armour reduction). A natural
    20 hits it but is not a crit on it.
  - It takes the weapon dice only: no ability term, Exploit or Precision.
    It gets no reaction window and no verb. No flagged or crit rider is
    attempted on it (the at-will rider bound treats the rider as spent,
    7.2 rule 9); a weak-target rider still lands (stat model 8.2).
- **Cleave's second target** makes a new roll (pipeline step 2 on), with
  its own reaction window. It gets no Exploit, Precision, ability term
  (Strength only if negative) or verb; a weak-target rider lands; the
  at-will rider bound applies.
- **Shift (W-8).** A shift moves the target along the ring of squares
  adjacent to the source, one step at a time, each step to a ring square
  8-adjacent to the current one. Base Hook allows up to 2 steps; each
  signature or Technique that raises Hook's distance raises the shift
  equally; all of it counts toward the 3-square cap. It stops at units and
  walls, never passes through the source's square, and is blocked by
  Rooted, Restrained and Anchored. The end square is chosen by the player
  or the AI among the reachable ring squares, the AI using the fixed
  square order.
- **Movement facts** (20.3). "Moved" counts squares the unit entered by
  its own movement on its own turn, verb and property steps included
  (Flourish, Driving, Lunging). Forced movement, a shift, standing up from
  Prone, and steps taken on another unit's turn (Kiter's Step) are
  excluded.
  - **Aim** applies to the first attack the unit declares on its own
    turn, if that count is 0 at declaration (Steady Aim: at most 1). Any
    earlier attack that turn, hit or miss, a class ability with
    `weaponAttack: true` included, spends it; a miss gives no Exploit and
    passes nothing on. **(agreed rule, client question 4; timing lead's
    call)**
  - **Lunge** qualifies at declaration if the count is at least 2 (Quick
    Lunge: 1), the target is adjacent, the target was not adjacent at
    turn start, and no attack has been made against it since the
    movement that qualified (each target once per turn). A hand crossbow
    shot at that target earlier in the turn, before the movement, does not
    stop it.
- **Graze's reaction test.** Graze is denied if, and only if, the attack's
  total meets the target's AC as it was before every AC increase applied
  in the reaction window (so a Parry taken on an attack that would have
  missed anyway does not deny it). A natural 1 allows Graze.

## 8. Reach, threatened zone and opportunity attacks

**(lead's call)**

- **Reach** is 1 or 2, measured as Chebyshev distance (the larger grid
  offset, ability model 8.1). The maximum is 3, reached only through the
  Long Haft property on a weapon with `minReach` 2 (18.2).
- **Reach attacks pass over units:** they need line of sight but are not
  blocked by an ally or enemy in between, so a pike strikes over a
  shield-bearer.
- **`minReach` 2** (pike only): it cannot attack at distance 1 and uses its
  haft strike there (13). With Long Haft the pike attacks at distances 2
  and 3.
- **Threatened zone:** the squares at distance 1 up to the unit's largest
  reach that the unit can see (symmetric visibility) and could attack with
  some weapon in its active set, its improvised attack or its haft strike.
  A trigger fires only if a legal attack on the mover exists at that
  moment; otherwise the reaction is not taken and not spent.
  - An **opportunity attack** triggers when a hostile **leaves** the zone
    by its own movement.
  - **Set** triggers when a hostile **enters** the zone from outside it by
    its own movement.
  - So stepping away from next to a pike does not trigger Set; it provokes
    only when the hostile finally leaves the zone. Disengage prevents both.
  - Set and the opportunity attack share the round's one reaction.
  - Which weapon: the main-hand weapon (11.3) if it can reach the mover;
    otherwise the off-hand weapon, the haft strike or the improvised
    attack, in that order.
- **Movement and reactions:**
  - Movement is a sequence of single-square steps. A **leave** trigger
    resolves before the step that leaves, with the mover still on its last
    square inside the zone. An **enter** trigger (Set) resolves after the
    mover enters its first zone square, before its next step. Kiter's Step
    ("moves adjacent") resolves after the step that makes the mover
    adjacent.
  - If a reaction displaces the mover, or sets its speed to 0, its current
    **movement segment** ends where it stands. It keeps its unspent
    movement for a later segment this turn; its path is not re-planned
    automatically.
  - A **movement segment** is one contiguous run of steps; it ends when
    the unit takes any non-movement action, is displaced, or ends its turn.
    Overwatch's "ends its own move" means the end of a segment that is not
    cut short by displacement.
  - Self-movement verbs never apply on another unit's turn (7.2 rule 3);
    the only self-step on another unit's turn is the Kiter's Step reaction,
    which does not provoke. Reactions do not nest (ability model 7.3), so
    a reaction's push or step cannot itself trigger a reaction.
  - A class reaction may set the mover's remaining movement for this
    turn to 0; its speed is unchanged and it keeps its action and bonus
    action. Movement granted afterwards by Dash (whatever its action cost)
    or by Spurred is added as normal; forced movement is unaffected
    (classes design 8.3, Hold the Line) **(change CL-6)**.
  - **Slip past:** where a feature makes a slip-past provoke (the
    `slipPast` trigger), it resolves after a successful slip-past check
    (ability model 7.2), before the step that moves the hostile through
    or past the unit's square, and spends the reaction as an opportunity
    attack **(change VG-20; Vanguard design 7.4)**.
  - **Reaction attack on another's attack** (trigger kind
    `attackAgainstOther`): a reaction attack, **not** an opportunity
    attack, hooked after step 15 of the weapon pipeline (7.5), or after
    ability model 14.3 step 6 for each attack roll of a non-weapon
    attack. A legal attack on the attacker must exist at that moment;
    otherwise it is not taken and not spent **(change VG-20; Vanguard
    design 8.2)**.
- **Prone and reach:** a reach attack at distance 2 or more against a
  Prone target has disadvantage (conditions 18.4). Reach weapons therefore
  carry no Topple.

## 9. Ranged, thrown, loading, ammunition and cover

**(lead's call unless marked)**

### 9.1 Ranges

One range ring per weapon, in squares; no long-range band *(illustr.)*:

| Launchers | Range | Thrown | Range |
|---|---|---|---|
| Sling | 8 | Dagger, handaxe, spear, trident | 5 |
| Shortbow | 10 | Bolas | 6 |
| Light crossbow | 12 | Javelin | 8 |
| Longbow, heavy crossbow | 14 | | |
| Hand crossbow | 6 | | |

### 9.2 Shooting in melee

- Every ranged or thrown **weapon** attack made with a hostile adjacent has
  disadvantage. Exceptions: the hand crossbow's `pointBlank` tag and the
  Seeking property.
- A hostile counts if it is adjacent, not Incapacitated, and can see the
  attacker, as in 5E. A minion counts; a Downed, Asleep or Blinded hostile
  does not.
- Spell attacks are unaffected, so the Later condition Harried keeps its
  job.

### 9.3 Thrown and loading

- **Thrown** is an attack mode of a melee weapon. The weapon stays with the
  thrower: no pickup and no count. **(settled, items design 17)** Each
  Extra Attack may be a throw.
- The verb applies to throws, except that Cleave is melee only.
- There is no improvised throw of a non-thrown weapon.
- **On non-light weapons, thrown costs a die step:** a thrown-capable
  family's dice sit one step below its non-thrown peer in both grips. The
  spear and the trident (1d6 / v1d8) are one step below the longsword
  (1d8 / v1d10); the javelin (1d6) below the one-handed d8 families.
- **Thrown die:** a throw rolls the family's one-handed `dice`, never the
  versatile die: dagger d4, handaxe d6, spear d6, trident d6, javelin d6,
  bolas d4. It is derived, not stored (20.1).
- **Loading:** at most one attack with that weapon per action, bonus action
  or reaction.

### 9.4 Ammunition

None tracked. **(lead's call, adopting items design 2 and 17's
recommendation)** "Fire arrows" are an Edge property on the bow (18.2).
Special arrows and bolts are `coating` consumables tagged `launcher`
(Later, with Coated; W-11).

### 9.5 Cover

- No cover rule in M1. Obstacles block line of sight only, and units never
  give cover.
- Revisit in M2 with the dungeon generation design, using the simulator's
  party damage share by range band (21.3).
- A unit holding only a launcher threatens adjacent squares through its
  improvised stock or stave blow (13).

## 10. Strength thresholds and training

### 10.1 Strength thresholds (soft)

**(lead's call)**

- **`heavy` = Strength +1:** greatsword, greataxe, maul, glaive, halberd,
  pike, heavy crossbow. **The longbow needs Strength 0**, which gives
  Strength a ranged job.
- A family declares only its `strengthClass` (`heavy`, `longbow` or
  `none`); the threshold comes from `weapons.json` (20.2) and is never
  written on the family or the item.
- Below the threshold the weapon is **Strained**: disadvantage on its
  attacks, and its verb is off. It shows as one icon with the reason
  `StrengthThreshold` (items design 7.3).
- Strained is a **derived penalty, not a condition**, so it uses no
  condition budget and is never stored.
- The threshold reads **Strength excluding conditions**, so a potion
  cannot unlock a weapon mid-fight (as capacity does, items design 5.4).
- Soft beats hard because emergency pickups, hand-overs and Disarmed
  fallbacks keep working: "slowed, never locked out" (settled, stat
  model 2).
- Monsters are bound by thresholds at authoring (settled, enemies 15.2).

### 10.2 Training

**(agreed: G1; details lead's call)**

- Three training levels: **simple (S), martial (M), exotic (X)**. Classes
  grant simple and martial in data; exotic comes only from a class feature
  or a "verb training (family)" offer naming the family (7.3).
- Training is **soft** (items design 12.9): an untrained wielder gets no
  proficiency and **no verb**, and keeps the weapon's geometry (reach,
  range, thrown, hands).
- **Properties on an untrained or Strained weapon (lead's call):** its
  notable properties still work, except verb signature properties
  (18.4), which are off with the verb. A focus is the exception: untrained,
  its properties are inert (14).
- **Group tags** are `blade`, `axe`, `impact`, `flexible`, `polearm`,
  `spear`, `launcher` (subtags `bow`, `crossbow`), `thrown` and `focus`.
  They feed offer `requires`, subclass hooks and smart loot, never
  training. Chassis facts (`heavy`, `light`, `finesse`, reach) are fields,
  never group tags (20.1).

## 11. Hands, grips and dual wielding

### 11.1 Weapon set contents

**(lead's call; change W-6 to items design 5.2)** A weapon set has a main
hand (`main`) and an off hand (`off`), or one two-handed item in both:

| Slot | May hold |
|---|---|
| `main` | A weapon, a main-hand focus (rod), or nothing |
| `off` | A light weapon (only if `main` holds a light weapon), a shield, an off-hand focus (orb, tome, holy symbol, totem), or nothing |
| Both (`twoHand`) | A two-handed weapon, the staff, or a two-handed focus (instrument) |

So the legal sets include: a weapon; weapon + shield; weapon + focus;
**light + light** (new); a focus alone; **rod + shield**; rod + off-hand
focus; a two-hander; the staff; the instrument. The equip query validates
both slots and returns one reason code on failure. A focus's `equip.slot`
maps as `off` → `off`, `main` → `main`, `two` → `twoHand` (bulky).

### 11.2 Grips

- **Versatile:** uses `versatileDice` when the set's off hand is empty,
  or holds a buckler and the declared grip is `twoHand` (armour design
  10.2). Derived, never stored. Any other shield, or a focus, blocks it
  **(change AR-43)**.
- **Two-handed:** `equip.slot: twoHand`, off hand blocked, bulky (size 2)
  in the pack. **(settled, items design 5.2, 12.3)**
- **Guard verbs** (Parry) work from either hand.
- **Foci** take the off hand, the main hand (rod) or both hands (staff,
  instrument) (section 14).
- **The staff** may be held one-handed as a versatile weapon (main hand,
  with a shield or off-hand focus). Its focus verb and focus properties
  work **only while it is held two-handed** (off hand empty), and it then
  also rolls its versatile die.

### 11.3 Dual wielding

**(agreed: D-b)**

- **Hands are positions in the set (lead's call):** the light weapon in
  `main` is the main-hand weapon. Swapping the two weapons of a set is a
  free interaction out of combat; in combat it counts as the set swap
  (bonus action).
- Attacks of the Attack action, Extra Attack included, use the main-hand
  weapon.
- After at least one Attack-action attack this turn, the unit may make
  **one off-hand attack** with the off-hand weapon as its **bonus action**,
  once per turn. A class ability with `weaponAttack: true` and cost
  `action` does not count as the Attack action unless it says so; Cleave's
  follow-up and reaction attacks never do. Any thrown off-hand weapon may
  be thrown.
- The off-hand attack adds **no positive ability modifier** to damage. A
  negative Strength still applies to a melee or thrown off-hand attack
  (6.2); an off-hand launcher (the hand crossbow) adds nothing. It rolls
  with Strength or Dexterity by 6.2, carries the off-hand weapon's verb,
  and may qualify for Exploit (6.3).
- **Nick** (dagger) moves that attack into the Attack action, once per
  turn, freeing the bonus action for a potion, the set swap or a class
  feature. It may be a throw.
- **Reactions** use the main-hand weapon unless only the off-hand weapon
  can reach the target (8).
- The off-hand attack is a two-weapon attack, not a shield bash: a bash
  replaces one attack, like Shove (17.9).
- **Fallback D-c (agreed as the named fallback):** a passive "paired"
  damage part on the main-hand hit instead of the off-hand attack. **(lead's
  call)** The lead switches to it only if the M2 companion-turn playtest
  median exceeds 10 seconds and turns with an off-hand attack are
  measurably slower; the switch is reported to the client.

## 12. Damage types and riders

**(lead's call unless marked)**

- Each family has one physical type. `altDamageType` (chosen per attack)
  is allowed on **at most two families**, whether it comes from the family
  or a property.
- **`riderFlag` is false on every mundane base, shield bashes included.** A
  base weapon's riders come only from critical hits, weak targets
  **(settled, stat model 8.2)**, martial Techniques **(settled, ability
  model 6.6)**, properties (Honed, the Edge riders), Coated, and the
  monster affix Honed.
- The at-will bound covers flagged and crit riders on second targets;
  weak-target riders land on every damaging hit, as agreed (7.2 rule 9).
- Riders arrive in M2 **(settled, stat model 9)**; until then a weakness is
  extra damage only, and Puncture, Honed and the Edge riders wait for M2.

## 13. Improvised and unarmed attacks

**(lead's call; `improvised` required on every family, settled, items
design 12.2)**

- **Improvised:** **bludgeoning**, each die one size smaller (minimum d4),
  reach 1, Strength. No verb, rider, Exploit or properties.
  - Exceptions: launchers and foci, 1d4; the pike's **haft strike**, 1d6
    (the pike's normal answer to an adjacent foe). The greatsword's 2d4
    follows the rule.
  - Monsters: enemies 15.5's −30%.
- **Unarmed strike:** 1 + Strength bludgeoning, universal, no verb. It
  keeps "slowed, never helpless" when both sets are empty (W-2).

## 14. Foci

**(agreed: F3; details lead's call)**

- A focus takes a hand (17.8), gives a **focus verb** to spells of its
  tradition, and carries properties. **Casting never requires one.**
- Untrained in the tradition: no focus verb, and its properties are inert,
  except a Reliquary Shield, whose properties follow shield training
  (armour design 10.2) **(change AR-44)**.
- Every focus declares a **1d4 bludgeoning improvised bash**.
- Hands and legal sets: 11.1. The staff's focus side works only
  two-handed (11.2).
- Never called a "wand": wands are beltable consumables (items design
  12.3).
- **No +N spell-attack foci** (buff test).
- A Disarmed focus (Later) loses only its verb and properties; casting
  continues (16).
- The focus catalogue is in 17.8; focus property tables in 18.5.

## 15. Monster use

### 15.1 The revised one-die rule

**(lead's call; change W-5)**

- A monster rolls **one die: the smaller of the weapon's largest die and its
  row's die.** Row dice *(illustr.)*, from the enemies 6.2 example column:
  levels 0–1 d4, 2–3 d6, 4–6 d8, 7–9 d10, 10–12 d12.
- Flat damage comes from the row as now, and is **never below 0**.
- **Minions (W-17, lead's call)** roll that die **one size smaller,
  minimum d4**, with the flat set to keep their 50% band. At level 0, where
  a d4 with flat 0 is already above the band, the minion rolls d4 with
  flat 0 and passes the band; the 6.4 guards still apply.
- **Why:** under the unrevised rule plus "flat ≥ 0", the pike soldier
  (d10), the greatclub brute (d8) and d8 artillery are illegal on floor 1
  under the 6.2 band and the 6.4 crit guard. The revised rule keeps every
  family legal under the per-turn guards on every floor; for monsters the
  die is only variance. (The round guard for Set, below, still holds pike
  soldiers to room level 2.)
- **Exploit and Precision on monsters (lead's call):** they are core rules,
  not authored dice, so they sit outside 5.4's "at most one die per
  at-will part", as crit dice do. Each Exploit die is the monster's
  one-die-rule die. **Both count at expected value** (6.3, 6.5), in the
  at-will band and in the 6.4 guards, as the client decided. In the
  "with a crit" guard rows the Exploit expected value is doubled with the
  dice; Precision is flat and is not.
- **Band value (lead's call),** in integer permille: band EV = die
  average + flat + Σ (rate × verb EV per hit), with the rates in
  `weapons.json` (20.2):
  - Exploit: `exploitAdvantageRatePermille` (6.3);
  - Skewer and Cleave second targets: `secondTargetRatePermille` per
    verb, the second target taking the die average;
  - Graze: the monster's Strength modifier × the miss permille ÷ the hit
    permille, from the enemies 6.2 monster-versus-hero hit chance at the
    unit's level, so a miss's chip is priced per hit.
- **Guards count one hero (lead's call).** The per-unit 6.4 guards count
  only damage dealt to one hero. Skewer counts only with the hero as the
  primary target; Cleave's second target is a different unit and is
  excluded. Dice are at maximum, Exploit at expected value.
- **A round guard for Set (lead's call; W-5).** A Set reaction is damage
  a melee hero cannot avoid by approaching, short of Disengage, so it is
  checked with the unit's own turn: a non-boss unit's maximum damage to
  one hero across one round, its turn plus its Set attack, is at most 50%
  of the hero's HP with no crit, and at most 70% with one crit. The
  opportunity attack is not added: the hero chooses to leave, and the
  preview shows the provoke. The floor 1 summed round-1 guard and the
  0.5% round-1 simulation include Set and opportunity attacks under the
  AI's movement.
- *Worked check, pike soldier (Strength, soldier 90% damage):* a level 1
  pike soldier rolls d4+2: average 4.5 against a 4.05 target (band ceiling
  4.66); on its turn, maximum 6 (33% of 18 HP), with a crit 10 (56%): it
  passes the turn rows. The round guard: Set plus its turn is 12 with no
  crit (67% of 18 at room level 1, **fails**; 48% of 25 at room level 2,
  passes) and 16 with one crit (64% of 25, passes). A level 0 pike soldier
  (d4+1) fails room level 1 the same way (10 of 18, 56%). **So pike
  soldiers appear from room level 2.**
- *Worked check, launcher artillery (Dexterity, artillery 110% damage):*
  sling and light crossbow both roll d4 at levels 0–1. Exploit expected
  value per hit: 250‰ × 2.5 = 0.625.
  - Level 1 (target 4.95, band 4.20–5.69): flat 2 gives 2.5 + 2 + 0.625
    = 5.125, in band. Room level 1 (18 HP): no crit 4 + 2 + 0.625 = 6.625
    (37%, passes); with a crit 8 + 2 + 1.25 = 11.25 (63%, **fails**). Room
    level 2 (25 HP): 26% and 45%, passes. **A level 1 launcher artillery
    appears from room level 2.**
  - Level 0 (target 3.85, band 3.27–4.43): flat 1 gives 4.125, in band.
    Room level 1: no crit 5.625 (31%), with a crit 10.25 (57%): passes.
    So room level 1 fields level 0 artillery.
- *Worked check, dagger minion (minion 50% damage):* a level 1 minion's
  target is 2.25 (band 1.91–2.59). A Strength-rolled 1d4 (2.5) is in band;
  Dexterity-rolled, Exploit makes it 3.125 and fails. **Minion dagger
  at-wills are authored as Strength** (6.2: monsters declare the ability),
  and the band check catches any that are not. A level 0 minion's d4 alone
  (2.5) is above its 50% band (target 1.75, ceiling 2.01), and a level 2
  minion's d6 (3.5) above its own (ceiling 3.16); W-17 settles both: the
  level 2 minion rolls d4 (2.5, in band) and the level 0 minion d4 with
  flat 0 as the stated exception.

### 15.2 Identity, caps and counting

**(lead's call; change W-5)**

- **Monster weapon identity is verb + type + reach or range.** The gear
  line reads, for example, "Pike: piercing, reach 2, Set".
- **Condition verbs** (Topple) count toward the type's 2 conditions
  (enemies 9.2).
- **Guard verbs** (Parry, Set) count as the unit's one reaction beyond the
  opportunity attack (enemies 9.1).
- On minions, condition and guard verbs are **inert and shown greyed**.
- A soldier carries **Interpose or a Guard-verb weapon, never both**.
- **Graze on monsters** deals the monster's own Strength modifier.
- Graze, Skewer's second target, Cleave and Exploit count in the at-will
  band at expected value, at the rates in 15.1. The 6.4 checks include
  verb effects, including advantage handed to later units (Prone).
- **Carried properties (lead's call; enemies 15.4 says only that the
  at-will gains the property):** monsters have no pools (enemies 5.5), so
  a property's pool maps to uses for the monster's life: `rest` N and
  `camp` N become `uses:N`; `never` becomes `uses:1`; a `fight`-pool
  property (Opener, Quickdraw) becomes `uses:1`, which is one fight for a
  monster.

### 15.3 Role loadouts

| Role | M1 | M2 additions |
|---|---|---|
| Brute | The approved M1 brute is the **Bleeding claw + Shove** beast; an armed variant uses a greatclub (Topple, Prone counted once with Shove's) + Shove | Maul (Topple, counts as a condition); greataxe or greatsword |
| Soldier | **Warhammer and shield (Push) + Interpose + Shove.** A pike soldier (Set instead of Interpose; from room level 2, 15.1) is the optional second soldier. | Spear and shield (Skewer), once a party gives Skewer a second target; glaive or halberd elite (Bodyguard affix) |
| Artillery | **Sling (Push)** + dagger (level 0 from room level 1, level 1 from room level 2; 15.1) | Light crossbow (Skewer); shortbow. Longbow from floor 2, with the artillery band widened to 8–14 (W-5). |
| Minion | Dagger or spear, Strength-rolled (condition-free; their verbs rarely fire against a solo hero, which is accepted) | — |
| Skirmisher (M2) | — | Handaxe (thrown + Cleave), javelin (thrown + Pry), trident (thrown + Hook), sabre (Flourish) |
| Leader (M2) | — | Longsword (Parry) instead of other reactions; whip (Hook) for slaver types |

### 15.4 AI and scorer hooks

**(lead's call; changes W-4, W-5)**

- **Why these M1 defaults:** M1 is a solo hero, so Skewer and Cleave can
  never fire on the party. The default soldier and artillery carry verbs
  that fire against one target (Push), so monsters teach a verb before the
  player loots it (2, Goals). The M1 trigger-rate report checks it (21.3).
- Every verb has a **static scorer value** in HP equivalents: Helped = the
  expected gain on the next attack; Prone = its control value plus the
  advantage gain for adjacent allies.
- The cost of **entering a Set zone** applies only while the zone owner's
  reaction is unspent, and only for the first unit each round that would
  trigger the owner's Set under its current policy (by default the first
  non-minion). This prevents a stall exploit without letting a minion
  "spend" a zone it cannot trigger.
- **No reaction priority rule except the AC-reaction order in 7.5 step 5
  (change AR-41; armour design 10.4).** Set, opportunity attacks and Parry
  trigger at different moments and never compete at one decision point;
  each follows its own policy and filter (7.1, ability model 7.3).
- **Aim** is valued as the expected Exploit dice on the turn's first
  attack, and only when that attack is made from the current square before
  moving (Steady Aim: a destination at most 1 square away).
- **Lunge** is scored inside the square-plus-ability pairs (ability model
  13).

## 16. Disarmed (Later)

**(lead's call; change W-9; the settled party-side rule kept)**

- **Sources:** disarm Techniques, the Disarming property (whip, flail;
  already in conditions 18.3) and monster cooldown abilities (new). Never
  an at-will verb, because Disarmed is major.
- **Save:** Fortitude against the source's ability difficulty, with
  advantage when wielding two-handed.
- **Only the main hand drops.** Shields are never Disarmed. A Disarmed
  focus loses only its verb and properties; casting continues. Sure Grip
  and Clinging are immune.
- **Party side (settled, items design 7.1):** the weapon falls into its own
  square; a bonus action re-wields it from there or swaps sets; Distracted
  blocks recovery; at combat end it returns to its owner.
- **Monster side (settled, enemies 15.5 "Disarm seizure" and conditions
  18.3):** an issue weapon cannot be taken. A notable weapon may be seized
  with the free interaction. Meanwhile the owner uses its improvised
  at-will. **Changed (W-5):** a party unit standing on the square blocks
  recovery only once the owner has moved off it, so an owner still
  standing on its own weapon can pick it up.

## 17. Catalogue

**(lead's call)** How to read the tables:

- Monster dice follow the revised one-die rule (15.1).
- "Improvised" follows 13 unless stated.
- Training: S simple, M martial, X exotic. Types: B bludgeoning, P
  piercing, S slashing.
- Dice and names are SRD-shaped and must pass the SRD check in the header.

### 17.1 Blades (`blade`)

| Family | Wave | Tr. | Dice | Type | Hands and tags | Reach or range | Str | Verb |
|---|---|---|---|---|---|---|---|---|
| Dagger | **M1** | S | 1d4 | P | light, thrown | reach 1; thrown 5 | – | Nick |
| Shortsword | M2 | M | 1d6 | P | light | 1 | – | Vex |
| Sabre (covers scimitar, cutlass, falchion) | M2 | M | 1d6 | S | light | 1 | – | Flourish |
| Rapier | **M1** | M | 1d8 | P | one-handed, finesse | 1 | – | Lunge |
| Longsword (covers arming sword, bastard sword, katana) | **M1** | M | 1d8 / v1d10 | S | versatile | 1 | – | Parry |
| Greatsword (covers zweihander, claymore) | **M1** | M | 2d6 | S | two-handed, heavy | 1 | +1 | Graze |
| Sickle (covers khopesh, hookblade) | M2 | S | 1d4 | S | light | 1 | – | Hook |

- **Dagger.** Best at the off hand: Nick keeps the bonus action free, and
  any unit gets a throw at range 5. Worst at damage; never a main weapon.
  Monsters: minion weapon, artillery sidearm. Skins: throwing knives,
  darts, and a "throwing hammer" (Converted to bludgeoning) replacing the
  cut light hammer.
- **Shortsword.** Best at sustained Dexterity melee: a Vex hit gives
  advantage on the next attack, and advantage triggers Exploit. Shortsword
  plus dagger is the signature Dexterity build. Worst at groups, position
  and recovering from a miss. Monsters: M2 skirmisher; the scorer prices
  Helped.
- **Sabre.** Best at hit and slip: after a hit it steps out of a pin,
  round a corner, out of an ogre's reach, or opens a Skewer line for an
  ally; from M2 the light Dexterity weapon that delivers Bleed. Worst at
  holding a line. Monsters: M2 raider, scored as a post-hit retreat.
- **Rapier.** Best at diving onto artillery and casters: the largest
  Dexterity-eligible die, and the Lunge opener triggers Exploit; the
  duellist build with a shield. Worst when pinned, in corridors, and it
  cannot dual-wield. In M1 so the M1 simulator has a Dexterity melee
  build. Monsters: M2 duellist.
- **Longsword.** Best for the front liner who must survive: sword and
  shield gives the highest one-handed AC plus an active defence; two-handed
  it is the flexible damage pick. The baseline. Worst at tempo and
  position: Parry spends the reaction an opportunity attack would use, and
  does nothing against ranged attacks or spells. Monsters: M2 leader (d8
  with a shield, d10 two-handed, both row-capped).
- **Greatsword.** Best at reliable single-target Strength damage: 2d6 is
  narrow, and Graze chips on a miss, the steadiest kill clock against
  bosses and high-AC elites. Worst against groups and minions. No shield;
  Strength +1. Improvised 2d4 B. Monsters: brute.
- **Sickle.** Best at rearranging an adjacent enemy with Hook's shift: drag
  a blocker out of a doorway, swing a foe away from its healer, set up a
  flank, a Skewer line or a pike ring. Light, so it fits any off hand.
  Worst at damage and against Large or bigger. Monsters: M2 cultist
  minion, which teaches Hook.

### 17.2 Axes (`axe`): axes cleave

| Family | Wave | Tr. | Dice | Type | Hands and tags | Reach or range | Str | Verb |
|---|---|---|---|---|---|---|---|---|
| Handaxe | **M1** | S | 1d6 | S | light, thrown | 1; thrown 5 | – | Cleave (melee only) |
| Battleaxe | M2 | M | 1d8 / v1d10 | S | versatile | 1 | – | Cleave |
| Greataxe | **M1** | M | 1d12 | S | two-handed, heavy | 1 | +1 | Cleave |

- **Handaxe.** The light Dexterity or skirmisher answer to packs: throw on
  the approach, then cleave; the only slashing thrown weapon. In M1 so
  dual wielding has a main-hand partner for the dagger. Worst against lone
  targets and armour. Monsters: M2 skirmisher.
- **Battleaxe.** Cleave behind a shield. Loses raw two-handed damage to the
  greataxe. Monsters: M2 shield soldier variant.
- **Greataxe.** Best against packs and minion waves, and for big crits; the
  swingy group killer to the greatsword's reliable one. Worst at
  consistency and lone targets in the open. Monsters: brute.

### 17.3 Impact weapons (`impact`)

| Family | Wave | Tr. | Dice | Type | Hands and tags | Reach | Str | Verb |
|---|---|---|---|---|---|---|---|---|
| Mace | **M1** | S | 1d6 | B | one-handed | 1 | – | Topple |
| Greatclub | **M1** | S | 1d8 | B | two-handed (not heavy) | 1 | – | Topple |
| Warhammer | **M1** | M | 1d8 / v1d10 | B | versatile | 1 | – | Push |
| Maul | M2 | M | 2d6 | B | two-handed, heavy | 1 | +1 | Topple |
| War pick | M2 | M | 1d8 / v1d10 | P | versatile | 1 | – | Puncture |

- **Mace.** Topple with a shield or focus in the other hand, for anyone;
  the simple cleric or companion floors a target for the party. Worst at
  damage and against bosses (resolve); inert on minion carriers. The
  morningstar returns as a notable mace with Converted (piercing).
- **Greatclub.** The two-hander anyone can swing, with no threshold, that
  floors what it hits: Topple without the maul's Strength need. Worst at
  damage against the heavy weapons; gives up the shield. Monsters: the
  armed M1 brute variant.
- **Warhammer.** Shield-and-hammer control: clear a doorway, break a
  flank, and from M2 knock a foe toward a ledge; from M2 bludgeoning's
  Stagger rider makes hammers soldier-breakers. Worst at holding a target
  and raw damage.
- **Maul.** Sets up the party: a Prone target gives every adjacent ally
  advantage, and Exploit for Dexterity allies. Worst as support for reach
  and ranged allies (Prone gives them disadvantage), against bosses, and
  on save-heavy turns.
- **War pick.** Plate: against heavy-armoured soldiers, constructs and
  bosses it hits far more often. Worst against swarms and skeletons; does
  nothing special until piercing's reduction arrives in M2.

### 17.4 Flexible weapons (`flexible`)

| Family | Wave | Tr. | Dice | Type | Hands and tags | Reach or range | Str | Verb |
|---|---|---|---|---|---|---|---|---|
| Flail | **M1** | M | 1d8 | B | one-handed | 1 | – | Pry |
| Whip | M2 | M | 1d4 | S | one-handed, finesse | **2** (also adjacent) | – | Hook |
| Bolas | Later | X | 1d4 | B | light, thrown only | thrown 6 | – | Topple |

- **Flail.** Shield walls: it strips 2–4 AC from the soldier role every
  swing. In M1 as the answer to M1's shield soldiers with Interpose.
  Against unshielded foes, a plain d8.
- **Whip.** Board control from 2 squares while keeping a shield or focus;
  drags a caster or archer into your brute's reach. Worst at damage and
  against Large or Anchored targets.
- **Bolas.** Stops runners and grounds flyers (Prone ends Flying). Worst as
  support for allied archers, and against bosses and minions.

### 17.5 Polearms (`polearm`)

| Family | Wave | Tr. | Dice | Type | Hands and tags | Reach | Str | Verb |
|---|---|---|---|---|---|---|---|---|
| Pike | **M1** | M | 1d10 | P | two-handed, heavy, **minReach 2** | 2 only; haft strike 1d6 B when adjacent | +1 | Set |
| Glaive | M2 | M | 1d10 | S | two-handed, heavy | 2 (also adjacent) | +1 | Graze |
| Halberd | M2 | M | 1d10 | S | two-handed, heavy (tags `polearm`, `axe`) | 2 (also adjacent) | +1 | Cleave |

- **Pike.** Holds a line against chargers from the second rank: anyone who
  walks in takes a d10 before arriving. Worst against foes already
  adjacent, ranged attackers, and baiting (the first unit through spends
  its reaction). Monsters: optional M1 soldier from room level 2 (15.1),
  Set **instead of** Interpose.
- **Glaive.** Steady damage from the second rank against elites and
  bosses. Worst against minions and Prone targets at distance 2.
- **Halberd.** Groups seen from behind a front line, two rolls per turn
  into a cluster. Worst against lone targets.

### 17.6 Spears (`spear`; the pike also carries the tag)

| Family | Wave | Tr. | Dice | Type | Hands and tags | Reach or range | Str | Verb |
|---|---|---|---|---|---|---|---|---|
| Spear | **M1** | S | 1d6 / v1d8 | P | versatile, thrown | 1; thrown 5 | – | Skewer |
| Trident | M2 | M | 1d6 / v1d8 | P | versatile, thrown | 1; thrown 5 | – | Hook |
| Javelin | M2 | S | 1d6 | P | one-handed, thrown | 1; thrown 8 | – | Pry |

- **Spear.** Corridors and doorways, where one roll hits two foes; works
  with a shield; can be thrown. Worst in open rooms and against lone
  targets. Monsters: M1 minion, M2 soldier; monster spears teach "don't
  queue up in corridors" once a party gives them a second target.
- **Trident.** A thrown pull from 5 squares (a harpoon) for a Strength
  fighter: the spear's chassis with Hook. Worst at reach and sustained
  damage.
- **Javelin.** Breaks a shield wall from 8 squares while keeping your own
  shield. Worst against unshielded targets and for Dexterity builds.

### 17.7 Launchers (`launcher`, subtags `bow`, `crossbow`)

| Family | Wave | Tr. | Dice | Type | Hands and tags | Range | Str | Verb |
|---|---|---|---|---|---|---|---|---|
| Longbow | **M1** | M | 1d8 | P | two-handed | 14 | 0 | Aim |
| Light crossbow | **M1** | S | 1d8 | P | two-handed, loading | 12 | – | Skewer |
| Shortbow | M2 | S | 1d6 | P | two-handed | 10 | – | Vex |
| Sling | **M1** | S | 1d4 | B | one-handed: the only **non-light** launcher usable with a shield | 8 | – | Push |
| Hand crossbow | M2 | M | 1d6 | P | light, loading, **pointBlank** | 6 | – | Lunge |
| Heavy crossbow | M2 | M | 1d10 | P | two-handed, heavy, loading | 14 | +1 | Puncture |

No two launchers share a verb.

- **Longbow.** The planted sniper: shoot before you move, and the planted
  shot counts for Exploit (Aim), then reposition; holds a chokepoint from
  14 squares. Worst when engaged, for Strength −1 builds, and on turns it
  must move before it shoots. Monsters: from floor 2, artillery with
  range capped by the role band (W-5).
- **Light crossbow.** The sidearm for casters, companions and untrained
  units, with no Strength need; Skewer repays Loading in corridors. Worst
  for Extra Attack users and in open rooms. Monsters: M2 artillery.
- **Shortbow.** Kiting: moves and shoots every turn; the strongest
  sustained Dexterity launcher at levels 5–10. Worst against high AC and
  after a miss.
- **Sling.** The shield-bearer's ranged option and knockback at range; can
  free a grappled ally. Worst at damage. Monsters: M1 artillery, whose Push
  teaches the verb to a solo hero.
- **Hand crossbow.** Dive and shoot point-blank; the off-hand shot for a
  blade. Worst at range and with Extra Attack.
- **Heavy crossbow.** An anti-plate siege shot for units without Extra
  Attack, and reaction shots. Worst for low-Strength builds and against
  swarms.

### 17.8 Foci (`focus`; casting never requires one)

| Focus | Wave | Hands | Focus verb (buff-test tag) | Notes |
|---|---|---|---|---|
| **Staff** (one family: weapon + focus, any tradition) | M1 if the slice class casts, else M2 | two as a focus (focus verb and properties only two-handed, 11.2); versatile 1d6 / v1d8 B as a weapon, simple | **Steady Casting:** advantage on concentration checks (prevent) | Merges the quarterstaff. Its weapon verb is Parry. The family cap counts it under `focus`. |
| Orb (arcane) | M2 | off | **Reach Out:** cantrip range +4 (targeting) | The back-line artillery caster |
| Rod (arcane) | M2 | **main** | **Shape:** line and cone cantrips +2 length, inside the area cap (targeting) | Rod and shield |
| Tome (arcane) | M2 | off | **Annotate:** swap one known spell at a short rest instead of camp (economy) | The planning focus |
| Holy symbol (divine) | M2 | off | **Spare:** once per turn, one area spell leaves out one ally square (targeting) | A divine caster with a shield instead takes a **Reliquary Shield** (an armour-design shield with a `focus{}` sub-record), so no new placement rule is needed. The Reliquary Shield is notable-only; rod + Reliquary is legal (armour design 10.2; change AR-44) |
| Totem (presence) | M2 | off | **Loom:** Frighten and presence ranges +3 (targeting) | Breaks morale from range |
| Instrument (presence) | Later | two | **Carry:** Rally and presence buffs +3 range | Costs both hands for no weapon |

### 17.9 Shields as weapons (handed to the armour design)

**(lead's call; change W-13)**

- **Shield bash:** a `bash{}` attack mode, 1d4 B, Push, **riderFlag
  false**. It replaces one attack, like Shove, and is not a two-weapon
  off-hand attack.
- **Stagger from a bash** comes only from the Bashing Rim property or a
  Technique (W-9).
- **Buckler (recommended to the armour design):** does not block versatile
  dice, and grants **no** Parry, to limit Parry sources.
- **Tower shield (Later):** grants Interpose; soft Strength +1.
- **Designed in the armour design** (section 10; the Reliquary Shield in
  10.2) **(change AR-44)**.

### 17.10 Natural weapons (monster ability templates, never items)

| Template | Wave | Type | Reach | Effect |
|---|---|---|---|---|
| Claw | M1 | S | 1 | Bleeding on hit (the settled non-rider claw; counts toward the type's 2) |
| Bite | M1 | P | 1 | Plain bite; pack bite: Topple; grabbing bite: Grappled, cooldown only |
| Slam | M1 | B | 1 | Push |
| Tail or sting | M2 | P or B | 2 | Set (counts as its reaction); sting variant adds Poisoned |
| Tentacle or pseudopod | M2 | B or acid | 2 | Hook |
| Touch | M2 | necrotic or acid | 1 | Pry; the acid ooze is the Later Notched and Pitted Armour source |

Templates are `natural.*` ability records, used by unarmed types (enemies
15.2) (W-5).

### 17.11 Merged and cut

| Cut | Why, and where it went |
|---|---|
| Club | Dominated by the greatclub and warhammer; an off-hand push comes from Shove-through |
| Light hammer | Nick duplicated the dagger; it became a dagger skin with Converted (bludgeoning) |
| Main-gauche | The Parrying property covers it; too many Parry sources |
| Morningstar | Overlapped the mace and flail; a notable mace with Converted (piercing). The "Spiked" per-attack type choice was dropped. |
| Chain, war scythe, lance | Duplicate jobs; Lunging and Opener cover the charge fantasy |
| Cestus (Clinch verb) | Grappling belongs to the class designs; it would add a verb |
| Net (as a party weapon) | Restrained on a `fight` pool breaks ability model 6.2. A monster cooldown-3 ability (M2) and a Later consumable instead. |
| Darts, throwing knives | Dagger skins |
| Chakram, repeating crossbow, atlatl, warbow, harpoon, boomerang | The Ricochet, Repeater and Far-flight properties, or the longbow, the trident and the default no-pickup throw |
| Blowgun, staff sling | Deferred: they need Coated plus a stealth rule, or a cover rule |
| Scimitar, gladius, estoc, partisan, bo staff | Folded into the sabre, shortsword, war pick, spear and staff; the names live on as notables and skins |

## 18. Notable properties

**(lead's call)** Every property is a `prop.*` record under the buff test
(settled, items design 12.4).

### 18.1 Rules

- Bands b1 / b2 / b3 cover levels 0–4 / 5–8 / 9–12 (settled, items design
  12.4).
- Bands scale distances, charges and charge-power dice, **never a flat
  bonus to hit or damage**. Any number appears only through a verb.
- At most 3 properties per item, and `value` equals the rarity budget
  (settled, items design 12.5).
- Monster carriers get the benefit only (settled, enemies 15.4); pools map
  to uses as in 15.2 **(lead's call)**.
- **Always-on properties never copy another verb's effect,** focus verbs
  (`fverb.*`) included; `charge` properties are limited abilities and may.
  Each property declares its effect kinds from a closed list (20.1), which
  validation compares (21.1 rule 10).
- Quickdraw never sits on the same item as a `type` property (build time).
  A set that holds both is legal, but Quickdraw is suppressed while it
  does, with the reason code `QuickdrawTypeConflict` (runtime equip rule).

### 18.2 `props.weapon_minor` (value 1)

| Property | Tags | b1 / b2 / b3 | Eligible | Wave |
|---|---|---|---|---|
| Converted (physical type) | type | All weapon damage becomes the named type ("Skullsplitter": a bludgeoning longsword) | any | M1 |
| Edge (element) | type | The largest die deals fire, cold, lightning, acid, radiant or necrotic; on a launcher, the whole weapon die. Zero added damage. | any | M1 |
| Ember / Frost / Storm / Caustic / Dawn / Grave Edge | type, rider | Edge, with the element part rider-flagged (items design 12.10's Ember Longsword) | any | M2 |
| Honed | rider | The weapon part carries its type's rider flag | any | M2 |
| Long Haft | targeting | Reach +1: maximum 2, or 3 on a `minReach` 2 weapon (8) | reach families, spear | M1 |
| Far-flight | targeting | Range +4 / +6 / +8 (cap 20); thrown range +3 / +4 / +5 | launchers, thrown | M1 |
| Quickdraw | economy (`fight` pool, minor) | Swapping to this set is free once per fight | never with a `type` property; also shields, since it acts on the set (armour design 14.5; change AR-50) | M1 |
| Opener | charge (`fight` pool, minor) | Your first attack each fight has advantage | any | M1 |
| Parrying | prevent, charge | Parry reaction 1 / 2 / 3 per rest | non-Parry families | M1 |
| Lunging | charge, targeting | 1 / 2 / 2 per rest: move 2 squares as part of an attack; the target makes no opportunity attack | melee | M1 |
| Kiter's Step | prevent, charge | 1 / 1 / 2 per rest: reaction when a hostile moves adjacent; step 1 without provoking | launchers | M1 |
| Ricochet | charge, targeting | 1 / 1 / 2 per rest: after a hit, one roll against a different hostile within 2 squares of the target, dice only | launchers, thrown | M1 |
| Seeking | targeting | No disadvantage for an adjacent hostile | launchers, thrown | M2 |
| Repeater | economy | Ignores Loading once per turn | crossbows | M2 |
| Shove-through | economy | After a hit, Shove as a bonus action, once per turn | melee | M2 |
| Executioner's Stroke | charge | 1 / 1 / 2 per rest, `onHit`, target below half HP: roll the weapon dice twice | heavy blades, axes | M2 |
| Hungering | charge | 2 per rest: when you drop a unit, regain 2 / 3 / 4 HP (Withered blocks it) | any | M2 |
| Sure Grip | prevent | Cannot be Disarmed; immune to Notched | any | Later |
| Quivered | economy | Applying a coating is a free interaction | launchers | Later |
| Bloodletter | rider, cost | Slashing rider on every hit; you take 1 / 2 / 3 at turn start while your Bleed runs | slashing | Later |

A shield may also roll `props.verb.push` (18.4) through its bash's Push
(armour design 14.5) **(change AR-50)**.

### 18.3 `props.weapon_major` (value 2)

| Property | Tags | b1 / b2 / b3 | Eligible | Wave |
|---|---|---|---|---|
| Thunderous | charge | 1 / 1 / 2 per rest, `onHit`: push 2, then Fortitude save or Dazed | impact | M1 |
| Quake | charge | 1 / 1 / 2 per rest, `onHit`: hostiles adjacent to the target make a Fortitude save or fall Prone | mace, maul | M2 |
| Volley | charge, targeting | 1 per rest: one roll against each hostile in a radius-1 burst within range | launchers | M2 |
| Overwatch | charge | 1 / 1 / 2 per rest: reaction when a hostile ends its own move within half range; one attack | launchers | M2 |
| Pinning | charge | 1 per rest, `onHit`: Rooted for 1 turn; never against bosses | launchers | Later |
| Crushing | charge | 1 per rest, `onHit`: Pitted Armour 1 | impact | Later |
| Sundering | charge | 1 per rest, `onHit` against an armed monster: it uses its improvised attack for the fight | axes, impact | Later |
| Disarming | charge | 1 per rest, `onHit`: Disarm attempt (16) | whip, flail | Later |
| Shatter | cost, charge | Once (`never` pool): as part of a hit, destroy this weapon; the hit becomes a critical and pushes 2. Rare and above only; never on heirlooms or signature items. | any | Later |

### 18.4 Verb signature tables `props.verb.<verb>` (value 1)

These make individual weapons unique within a family. They are off
whenever the verb is off (10.2).

| Verb | Signatures |
|---|---|
| Push | **Heavy Push**: Push and Hook move 2 (b3: choose 1–3). **Driving**: after a push, step into the vacated square without provoking. |
| Hook | **Long Hook**: pull 2. **Crack** (whip): push instead of pull. |
| Topple | **Floorer**: also on a miss by 2 or less (bounds unchanged) |
| Cleave | **Wide Cleave**: the second target may be any hostile adjacent to you or to the first target within reach; b3 chains once after a kill. **Biting Cleave** (M2): the Cleave hit may carry the rider (same use). **Reaping** (charge 1 / 1 / 2 per rest): on a kill, one more attack against a different adjacent hostile. |
| Skewer | **Long Skewer**: b1 passes over one ally square; b3 also checks a third unit |
| Nick | **Paired Throw**: the Nick throw has no adjacent disadvantage. **Fan Throw** (charge 1 / 1 / 2 per rest): as the Attack action, throw at up to 3 hostiles, dice only. |
| Vex | **Shared Vex** (M2): give the Helped to an adjacent ally instead |
| Lunge | **Quick Lunge**: needs 1 square of movement. **Lunge Guard**: the Lunge target makes no opportunity attack against you this turn. |
| Flourish | **Long Flourish**: step 2; at b3 no one gets an opportunity attack |
| Aim | **Steady Aim**: Aim survives 1 square of movement (the first attack still counts for Exploit after moving 1 square) |
| Parry | **Deflect**: also against ranged weapon attacks. **Binding Parry**: a successful Parry pushes the attacker 1. **Riposte** (M2, charge 1 / 1 / 2 per rest): a successful Parry lets you attack the attacker in the same reaction; one reaction makes one attack, so where a Counterstroke is also offered the player picks one (the VG-18 echo ruling; change SH-8, Shade design 8.3). **Warding Staff** (staff): Parry may protect an adjacent ally. |
| Set | **Short Grip** (pike): may attack adjacent, with disadvantage. **Keep Back** (charge 2 per rest): a Set or opportunity-attack hit pushes 1. |
| Graze | **Driving Graze**: a Graze may also push 1 |
| Pry | **Pry Deep** (M2): also ignores Parry |
| Puncture | **Bore** (M2): hits count as bludgeoning for breaking layers, Brittle and Frozen |

### 18.5 Focus tables

**`props.focus_minor` (value 1):**

- Shaping: once per rest, +1 radius, inside the area cap.
- **Snap Casting:** 1 / 1 / 2 per rest, a tier-1 spell as a bonus action.
  (Not "Quickened", which conditions 18.11 lists as a Pathfinder name.)
- Attuned: one cantrip deals the named type.
- Conduit (M2): the cantrip part is rider-flagged.

**Focus verb signatures `props.fverb.<verb>` (value 1),** off whenever the
focus verb is off (untrained, Notched, Disarmed, or a staff held
one-handed):

- **Far Reach** (orb): Reach Out's range bonus rises by +2 / +3 / +4,
  total cap +8.
- **Shared Steadiness** (staff): Steady Casting also covers one adjacent
  concentrating ally.
- **Sparing** (holy symbol): Spare leaves out 1 / 1 / 2 more ally squares.

**`props.focus_major` (value 2):**

- Wellspring: 1 per camp, cast a known tier-1 spell without a slot (a camp
  spend, ability model 6.4).

### 18.6 M1 notables

Rider-free, M1 conditions only.

| Notable | Base | Rarity | Properties |
|---|---|---|---|
| Doorwarden's Pike | pike | Uncommon | Long Haft (reach 3), Quickdraw |
| Gatebreaker | warhammer | Common | Heavy Push |
| Wolfcleaver | greataxe | Uncommon | Wide Cleave, Opener |
| Vowkeeper | longsword | Uncommon | Binding Parry, Quickdraw |
| Thunderclap Greatclub | greatclub | Uncommon | Thunderous (major) |
| Hunter's Longbow | longbow | Common | Steady Aim |
| Corridor Warden | light crossbow | Uncommon | Long Skewer, Quickdraw |
| Skirmisher's Spear | spear | Common | Lunging |
| Duellist's Rapier | rapier | Common | Quick Lunge |
| Skullsplitter | longsword | Common | Converted (bludgeoning): an anti-skeleton sidegrade that costs the bonus-action swap |
| Bonebreaker Mace | mace | Common | Floorer |
| Wardwood Staff | staff | Common | Warding Staff |
| Magpie's Knife | dagger | Common | Far-flight (thrown 8) |
| **Headsman's Greataxe** (boss signature) | greataxe | Floor 1 top tier (budget 2) plus one authored property (items design 12.5) | Wide Cleave, Opener, plus authored Reaping |

"Vowkeeper" replaces "Oathkeeper", a protected name.

## 19. Licensing

**(lead's call; change W-14)**

- The credits ship the verbatim **SRD 5.2** CC-BY-4.0 attribution (weapon
  masteries) and the **SRD 5.1** one (rarity names, settled items design
  12.5).
- `srd` verbs (Push, Topple, Cleave, Nick, Vex, Graze) cite the SRD 5.2.1
  "Weapon Mastery Properties" entry in `srdSource` and carry an `adapted`
  flag where they differ: Push's size limit (the SRD says "Large or
  smaller"); Vex is not tied to a target and does not chain; Graze is
  Strength-only and once per turn.
- SRD weapon names and dice on family records carry the `srd` flag and cite
  the SRD 5.2.1 weapons table.
- **Nothing is written to data before the SRD check** in the header.
- Verbs inspired by other games are labelled "original" and carry no names
  or text from those games.

## 20. Data and the core

**(lead's call)**

### 20.1 Data shape *(illustr.)*

```json
"weapon": { "dice": ["2d6"], "versatileDice": null, "damageType": "slashing",
            "altDamageType": null, "riderFlag": false,
            "hands": "twoHanded", "light": false, "finesse": false,
            "strengthClass": "heavy", "reach": 1, "minReach": 1, "range": null,
            "thrown": null, "loading": false, "pointBlank": false,
            "verb": "verb.graze" }
"thrown" (when present): { "range": 5 }
"focus":  { "tradition": "arcane", "hands": "off", "focusVerb": "fverb.reach_out", "weapon": null }
```

- `hands` is `oneHanded`, `versatile` or `twoHanded`; `light` and
  `finesse` are separate flags. **The attack ability is derived, never
  stored:** a launcher (`range` set, tag `launcher`) rolls Dexterity; a
  `light` or `finesse` weapon the better of the two (6.2); everything else
  Strength.
- `thrown` holds only `range`; the thrown die is the one-handed `dice`
  (9.3). `minReach` is 2 for the pike, else 1. `strengthClass` is
  `heavy`, `longbow` or `none`, mapped to a threshold in `weapons.json`.
- The **family record** holds `improvised`, `defaultMonsterProfile
  {effects: [verb], atWill, normal, minFloor: 1}`, `training`
  (`simple | martial | exotic`), `wave`, group tags (top-level and subtags
  only) and `strengthClass`. Training and the Strength threshold are read
  from the family; a weapon or focus item record writes neither (W-6).
- **Verb records** (`verb.*`, `fverb.*`) hold `kind`, `text`, `wave`,
  `oncePer` (`turn | round | none`), `trigger`, `modes[]` (Cleave:
  `melee`), `ownTurnOnly`, `strengthOnly` (Graze), `verbSourcedMove`,
  `previewHint`, `conditionsUsed[]`, `effectKinds[]`, `isReaction`,
  `policy {kind, filter}` (reactions; `filter` is `none` or `nonMinion`),
  `hook` (the C# hook id, ability model 14.4), `srdSource?` and `adapted`.
- **Property records** declare `effectKinds[]` from the same closed list
  (for example `advantage`, `extraDice`, `forcedMove`, `selfMove`,
  `reachUp`, `rangeUp`, `acIgnore`, `reaction`, `typeChange`, `rider`).
- Monster at-wills declare `ability` (6.2).
- `natural.*` ability templates (17.10) are ability records, not items;
  they roll Strength unless they declare Dexterity.

### 20.2 Tuning file

`weapons.json` holds every tuning number of this design, as
`inventory.json` does for the items design:

```json
{ "exploitDiceByBand": [1, 2, 3],
  "precisionLaunchers": { "enabled": false, "byBand": [1, 2, 3] },
  "monsterRowDie": { "0": "d4", "2": "d6", "4": "d8", "7": "d10", "10": "d12" },
  "strengthByClass": { "heavy": 1, "longbow": 0, "none": null },
  "forcedMoveCapPerHit": 3, "reachMax": 3, "hookShiftSteps": 2,
  "altDamageTypeFamiliesMax": 2, "notchedRepairDifficulty": 13,
  "validation": { "exploitAdvantageRatePermille": 250,
                  "exploitAdvantageRateSelfPermille": 1000,
                  "secondTargetRatePermille": { "verb.skewer": 300, "verb.cleave": 400 } } }
```

All values *(illustr.)*. `forcedMoveCapPerHit` covers all forced movement
on one hit (7.2 rule 4). Graze's validation rate is not stored: it comes
from the enemies 6.2 hit chance (15.1). `precisionLaunchers.enabled`
changes only through the gate in 6.5. A save records the file's hash, as
for `inventory.json` (items design 5.1).

### 20.3 State, queries and events

- **Nothing new is stored on an item instance.** Strained, the attack
  ability, versatile dice and verb availability are queries. Notched and
  Disarmed are conditions on the unit.
- **Turn ids (lead's call).** The core keeps a global `turnId` that rises
  by one at the start of every unit's turn, a shared minion slot counting
  as one turn. **Once-per-turn uses** are stored as `lastUsedTurnId` per
  key and compared with the current `turnId`, so they reset at every
  unit's turn, as the camp-spend flag does (ability model 6.4, 14.2). The
  keys: Exploit, Graze, each once-per-turn verb in use (a dual-wielder's
  two verbs are separate keys), the off-hand attack, and each
  once-per-turn property in use (Shove-through, Repeater, Spare).
- **Own-turn movement state,** reset at the unit's own turn start:
  `squaresMovedOwnThisTurn` (int), `attacksDeclaredOwnThisTurn` (int),
  `adjacentAtTurnStart` (unitIds) and `lungeUsedTargets` (unitIds), for
  Aim, Steady Aim, Lunge and Quick Lunge (7.6).
- **Per-target round state** records Topple attempts and verb-sourced
  forced movement (7.2 rules 2 and 4), cleared at round start.
- **Events:** `AttackDeclared{source, target, mode, ability, weapon}`,
  `VerbFired{verb, source, targets, result}` and
  `ExploitApplied{source, dice, viaAim}` for the log, replays and the preview
  property test; Strained and Notched appear through the existing
  attack-resolution events.

## 21. Validation, testing and simulator

**(lead's call)**

### 21.1 Content validation

The build fails when:

1. a weapon family has other than one verb;
2. a verb references a condition of a later wave than the verb's `wave`,
   or a family's `wave` is earlier than its verb's;
3. reach exceeds 3; or `minReach` exceeds reach; or `minReach` is above 1
   without a haft strike or other improvised close attack; or reach 3 is
   reachable on a weapon whose `minReach` is 1;
4. a two-handed weapon is not bulky (settled, items design 19.1 rule 5);
5. a family with `range` lacks the `launcher` tag or the reverse, or any
   record stores an attack ability for a weapon (it is derived, 20.1);
6. a monster weapon has flat damage below 0, or a band EV outside its row
   band with verbs, Exploit and (if on) Precision included at the 15.1
   rates; or a non-boss unit fails the 6.4 guards or the round guard for
   Set (15.1) at its earliest legal room level;
7. a condition or guard verb on a minion is not inert, or is not counted
   under enemies 9.1 and 9.2;
8. a soft threshold does not name the Strained penalty, or a weapon family
   or item writes a Strength threshold or training instead of taking them
   from `strengthClass` and the family's `training`;
9. a focus lacks a tradition or an improvised bash;
10. a property is a bare number, or an always-on property declares an
    effect kind that a verb or focus verb on the same item also declares
    (the copy rule, 18.1); anything the kinds cannot show goes to the
    review checklist;
11. Quickdraw shares an **item** with a `type` property (sets are checked
    at equip time, 18.1);
12. more than two families have a per-attack type choice;
13. a family matches another's verb, hands and die while lacking a chassis
    feature the other has (strict dominance), **as seen by a unit trained
    in both**: training is not a distinguishing feature;
14. a **top-level** group tag (not a subtag) has fewer than 3 families
    across waves;
15. a weapon family lacks `improvised` (settled, items design 19.1 rule
    11);
16. a record flagged `srd` lacks `srdSource`, or an adapted verb lacks the
    `adapted` flag;
17. a Notched source is on floor 1 or not visible from its type (Later).

### 21.2 Tests

- Unit tests: the attack ability choice and its tie; Dexterity-rolled
  damage with negative Strength; Exploit by band, once per turn, on
  reactions; Precision off and on; every verb's text, once-per-turn and
  once-per-round bounds; Topple sharing the rider save slot; the threatened
  zone (leave, enter, Disengage, shared reaction); reach over units;
  `minReach` and the haft strike; shooting in melee and its exceptions; Loading;
  Strained reading Strength without conditions; versatile derived from the
  off hand; the off-hand attack and Nick; the revised one-die rule and
  Exploit on monsters; improvised and unarmed; Notched (Later) recording
  instanceIds, escaping by swap, not landing on an empty or shield-only
  set, and having no effect when re-applied.
- Further unit tests (lead's call): the pipeline order of 7.5 (Topple
  before the push; Skewer and Cleave geometry after it; Vex's Helped
  before a Nick attack; Graze before the stop); Skewer's next square for
  each of the 8 directions, each octant and each knight's-move offset,
  with empty, wall, unseen and ally squares; the shift path around the
  source; forced-movement summing, verb-sourced once per round and
  collision only from non-verb parts (Push with Thunderous); the damage
  dice rule (6.6: Executioner's Stroke, Exploit on Notched and versatile
  dice, crit doubling); Exploit and Vex on the net roll mode; Aim
  qualifying only the turn's first attack before movement for Exploit,
  with one d20 and the roll mode unchanged (Vex still fires on it), with
  net disadvantage, spent by an earlier miss, and with Steady Aim after 1
  square; minion dice one size smaller and the level 0 exception (W-17);
  Graze's reaction test and natural 1; the shooting-in-melee hostile test;
  the off-hand attack with negative Strength and as a hand crossbow shot;
  hands in a light + light set; staff focus only two-handed; turn ids
  across reactions and a shared minion slot; Lunge and Aim movement facts.
- Property test: the preview matches the resolution for every verb
  (settled, ability model 16), with the ability choice read from
  `AttackDeclared`.
- Replay tests across fights with forced movement, Set and Parry, and for
  each case of 8's movement and reactions: a Set hit that pushes the
  mover, an opportunity attack with Push or Hook, Keep Back, Kiter's Step
  and Overwatch on a split move.

### 21.3 Simulator reports

| Report | Target *(provisional)* | Wave |
|---|---|---|
| Expected damage per round by family | Within ±10% of its hand-class reference (one-handed, versatile, two-handed, light pair, launcher) | M1 (M1 families) |
| **Dexterity parity** at levels 1, 5 and 10 against the greatsword reference, class features included, fixed-seed scenario set | 6.4 targets; feeds the Precision gate (6.5) | M2 |
| Team gates (one Topple user + two Exploit users) | No build above 110% | M2 |
| Verb trigger rate per family, with **party and monster rows** | Reported; a verb that rarely fires is a dead verb. Each M1 monster type's weapon verb fires in at least some solo fights. | M1 |
| Saves per round | Within the ability model's watch | M2 |
| Rounds a melee unit ends unable to attack because of forced movement or Prone | Low; a lockout watch | M1 |
| Set-zone stalls (fights where no unit enters a zone for 3+ rounds) | Rare | M1 |
| Weapon swaps per fight | Low but non-zero (settled, ability model 16) | M1 |
| Damage share by range band (melee, reach 2–3, ranged) | Each band's share of party damage within ±25% of its share of party members; feeds the M2 cover decision (9.5) | M2 |
| **Damage taken by range band** (party damage taken per fight by the unit's main band, reach 3 units reported apart) | Launcher-main units take at least 50% of what melee-main units take; the safety veto on the Precision gate (6.5) | M2 |
| Launcher family floor (6.4) | Every launcher family ≥ 60% of the reference at levels 1, 5 and 10; the longbow measured with the agreed Aim (7.1), checking the estimate in 6.4 and the 110% cap | M2 |
| Families never equipped after floor 2 | None; the dead-weapon watch | M2 |
| Off-hand attack share of companion turn time | Feeds the D-c fallback (11.3) | M2 |
| Notched: instances per floor and repairs attempted, and how often a Notched unit's cap of 2 blocked another lingering condition | Reported (Later) | Later |

## 22. Staging

**(lead's call)**

- **M1** (one class, levels 1–3, one floor, three enemy types):
  - 15 families: dagger, rapier, longsword, greatsword, handaxe, greataxe,
    mace, greatclub, warhammer, flail, pike, spear, longbow, light
    crossbow, sling; plus the staff if the slice class casts.
  - 11 verbs: Push, Topple, Cleave, Skewer, Nick, Lunge, Aim, Parry, Set,
    Graze, Pry.
  - Rules: chassis and attack ability; the attack pipeline (7.5) and
    geometry (7.6); verbs and bounds; Strained; training; dual wielding
    (dagger and handaxe); the threatened zone and movement timing (8);
    ranged disadvantage in melee; thrown and Loading; improvised and
    unarmed; Exploit (1 die at levels 1–3), with Aim as a trigger; type
    properties; the revised one-die rule, minion dice (W-17) and the round
    guard for Set (15.1).
  - Monsters: claw brute (or the greatclub brute), warhammer-and-shield
    soldier (or the pike soldier from room level 2), sling artillery,
    Strength-rolled dagger or spear minions.
  - The M1 notables (18.6) and the M1 rows of 18.2–18.3.
  - The Dexterity parity gate applies **from M2**. If the slice class is
    Dexterity-main, the rapier is its M1 measuring build.
- **M2:** shortsword, sabre, sickle, battleaxe, maul, war pick, whip,
  glaive, halberd, trident, javelin, shortbow, hand crossbow, heavy
  crossbow; spear-and-shield soldiers and light crossbow artillery; the
  remaining foci; M2 natural weapons; Vex, Flourish, Hook (with `shift`)
  and Puncture; riders, Honed and the Edge riders; the
  Dexterity parity reports and the Precision gate (6.5).
- **Later:** bolas, instrument, Disarmed, Coated and coatings, Notched,
  Sundering, Crushing, Shatter, Sure Grip, Quivered, Bloodletter,
  Pinning, the tower shield.

## 23. Risks

- **Dexterity parity** depends on the class designs' precision features
  (6.4). Launchers are the weakest point. Mitigations: the binding
  requirement, the parity reports, the per-family launcher floor, the
  per-class Precision gate with its safety veto, and Aim counting for
  Exploit (agreed, client question 4), which keeps the longbow from
  falling with level. Its figures are an estimate (6.4) until the M2
  parity reports measure it.
- **The monster one-die change (W-5)** moves monster weapon identity onto
  verbs. Players may read a pike as hitting no harder than a dagger on
  floor 1. The gear line and verb previews carry the read.
- **Control stacking:** forced movement, Prone, Set rings and the reaction
  economy. Mitigations: once-per-round bounds, condition verbs off on
  reactions, the stall and lockout reports, the floor-1 soldier cap.
- **Overshoot from team advantage** (a Topple user plus Exploit users).
  Gated by team reports; the named fallback limits Exploit to
  self-generated advantage (6.3), which still includes Aim. A longbow
  user with Precision on and a class precision feature stacks three
  damage rules on its planted shot; the 110% cap (6.4, 6.5) bounds it.
- **Learnability:** 15 verbs in 5 kinds. Mitigations: one line each, the
  gear line, mandatory preview hints, and monsters teaching verbs first.
- **Ranged dominance with no cover** depends on dungeon generation;
  revisit in M2 (9.5).
- **Parry sources** are limited to the longsword, the staff and the
  Parrying property; watch for a flat defensive identity.
- **Engine scope:** the attack pipeline, the `shift` movement kind, the
  threatened-zone model with step-wise movement, and turn ids are new core
  code.
- **Notched as cap filler (Later).** Once a unit has swapped away from a
  Notched set, the instance costs little but still holds one of the two
  lingering slots, which can block a worse lingering condition. The rule
  is agreed as written; the Notched report (21.3) counts how often it
  blocks another condition, and the lead raises it with the client if it
  is played for.
- **Reach 3** (Long Haft on the pike) strikes from behind two ranks. It is
  limited to `minReach` 2 weapons, which keep their adjacent gap, and is
  reported apart in the range-band reports.
- **SRD text and dice are unverified.** Check them against the SRD 5.2.1
  document before writing records flagged `srd` (header, section 19).
- **No durability** may disappoint players who want gear grit. Mitigation:
  the substitutes in 5.5 and Notched; Keenness stays a client-only backlog
  fallback (5.7).

## 24. Left to the builders

- All numbers in `weapons.json` and every *(illustr.)* value: ranges, row
  dice, Exploit dice, property band values, Strength thresholds within the
  `heavy` shape.
- `identifyDifficulty`, `value` inputs and prices per weapon record, within
  the items design.
- Notable names beyond 18.6, skins and models.
- Which families each class and companion is trained in, and starting
  kits (items design 17, row 3).
- Preview icons and overlay art within 7.2 rule 7.
- **Not tuning:** switching Precision on (6.5, the lead's gate), switching
  to D-c (11.3, the lead's call on playtest evidence), and anything that
  touches durability (client only).

## 25. Changes to approved documents

Applied on 2026-11-17, when this document was approved. Each change names
the exact section it touches. W-1 and W-12 were approved by the client on
2026-11-17; the rest are the lead's, confirmed with this document.

**Stat model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| W-1 | 5.2, ownership table, Dexterity and Strength rows, "In a fight" column; 5.3, bullet "Dexterity adds to hit but not to damage" | Dexterity row: "Attack rolls with ranged, light and finesse weapons". Strength row: "Melee and thrown attacks and damage, except light and finesse attacks rolled with Dexterity; shoving and grappling". 5.3's second sentence becomes: "Strength adds to both for melee and thrown attacks rolled with Strength; a Dexterity-rolled attack adds only a negative Strength." 5.3 adds: `light` means dual-wieldable and Dexterity-eligible, `finesse` Dexterity-eligible only; Exploit (weapons 6.3) and the default-off launcher Precision (weapons 6.5) are the only **core** Dexterity-keyed damage rules; class precision features (weapons 6.4) are the class designs' own. | **Client (agreed 2026-11-17, Q3 option b);** the Strength-row and class-feature wording applies the same approved reading (lead) |

**Ability model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| W-2 | 7.2, universal and skill actions table | New rows: off-hand light attack (bonus action; Nick moves it into the Attack action); threatened zone (opportunity attack on leaving, Set on entering; Disengage stops both; movement resolves square by square, with the trigger timing of weapons 8); ranged and thrown **weapon** attacks with a hostile adjacent have disadvantage; Loading; thrown as an attack mode with no pickup; reach passes over units; unarmed strike (1 + Strength bludgeoning); improvised attack rule (weapons 8, 9, 11.3, 13) | Lead |
| W-3 | 6.6, Techniques bullet; 10.2, offer kinds list | Technique option "empower the weapon's verb"; new offer kind "verb training (family)". 10.3 is not changed: `verb.cleave` is distinct from its example ability. (weapons 7.3) | Lead |
| W-4 | 7.3, policies list; 9.3, first and second bullets; 13, "Open to more terms" bullet | A reaction policy may carry a target filter (`nonMinion`), set in data and changeable by the player like the policy. Topple shares the rider save slot. The at-will rider bound covers flagged and crit riders on Cleave and Skewer second targets (a stated reading; weak-target riders are untouched, stat model 8.2). For weapon attacks, 14.3 step 5 expands into weapons 7.5. New AI term: static verb scorer values. (weapons 7.1, 7.2, 7.5, 15.4) | Lead |

**Enemies design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| W-5 | 5.3, Dexterity-main bullet; 5.4, first and second bullets; 5.6, gear line row; 6.4, guards table and notes; 9.1, reaction column; 9.2, "Counts toward the type's 2"; 11.1, Opportunity cost row; 15.2, first bullet; 15.4, first table row; 15.5, Disarm seizure, first bullet; 17.1, Brute, Soldier and Artillery rows | 5.4 first bullet adds: "Crit dice, Exploit and Precision are core rules outside this limit (weapons 15.1)." Monster die = min(weapon's largest die, row die); flat ≥ 0. Each monster at-will declares its attack ability; flat damage is unaffected by weapons 6.2. Exploit and (if on) Precision count at expected value, at the `weapons.json` rates, in the band and the guards. New 6.4 row: a non-boss unit's maximum damage to one hero across one round, its turn plus its Set attack: ≤ 50% no crit, ≤ 70% with one crit; per-unit guards count one hero. The gear line shows the verb. Verb effects count in the band and guards; monster Graze uses the monster's Strength. Guard verbs are the reaction beyond the opportunity attack; condition verbs count toward the 2; both inert on minions. Set-zone entry cost only while the reaction is unspent, for the first unit each round that would trigger Set under its policy. Carried properties map pools to uses (weapons 15.2). Disarm seizure: a party unit on the square blocks recovery once the owner has moved off it. Unarmed types use `natural.*` templates (Strength unless declared). Brute: heavy **or two-handed** weapon. Soldier: Interpose **or** a Guard-verb weapon. Artillery range band 8–14 from floor 2. (weapons 6.2, 15, 16, 17.10) | Lead |
| W-17 | 5.4, second bullet ("A monster rolls one die of the weapon's largest size", as revised by W-5); 7.1, ranks table, Minion row, "Damage is 50% of the row" | A minion rolls the W-5 die **one size smaller, minimum d4**, and its flat (≥ 0) is set to keep its at-will in the 50% band (6.2, ±15%). Exception: at level 0, where a d4 with flat 0 (2.5) is above the band (ceiling 2.01), the minion rolls d4 with flat 0 and passes the band; the 6.4 guards still apply. This fixes the level 0 minion (d4 above its band) and the level 2 minion (d6 above its band). (weapons 15.1) | Lead |

**Items design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| W-6 | 5.2, places table, "Active weapon set" row; 8.2, fit verdict "cannot wear: needs Str +2" and the layout mock-up's C3 column; 12.2, family record; 12.3, `weapon{}` row, `requirements` row and `equip.slot` row; 12.8 (Later); 12.10, Ember Longsword example; 2, Non-goals, bullet "Weight, bulk, durability or wear" | The set follows weapons 11.1's slot rules: adds light + light, a focus alone, rod + shield, rod + off-hand focus, the staff and the two-handed instrument. The soft-threshold verdict and the mock-up read "Strained: needs Str +1". `weapon{}`, `focus{}` and family fields as in weapons 20.1. For weapons and foci, training lives on the family (`training: simple\|martial\|exotic`) and the item-level `requirements.training` is not used; the 12.10 example drops it. Strength requirements come from the family's `strengthClass` through `weapons.json`; weapon and focus items do not write them. A focus's `equip.slot` maps `off`, `main` and `two` to `off`, `main` and `twoHand` (bulky). 12.8 (Later): a weapon is destroyed when a `never` pool reaches 0 (Shatter). The Ember Longsword is re-expressed as an Ember Edge M2 card. The non-goal's citation credits durability to enemies 2 and 15.9 (stat model 6 covers only weights). | Lead |
| W-13 | 17, row 2 (armour and shields), "Must decide" | Add: the shield `bash{}` sub-record (1d4 B, Push, riderFlag false); the buckler grip recommendation; the Reliquary Shield with a `focus{}` sub-record; the tower shield (Later) (weapons 17.9). The armour design takes these as inputs. | Lead |

**Conditions design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| W-7 | 18.9, Helped, Sources and Duration | Add source **Vex verb**, with a per-source fallback `untilSourceTurnEnd`, the wearer as source (it ends at the end of its next turn) | Lead |
| W-8 | 10.1, Forced movement | New forced-movement kind **shift**: steps along the ring of squares adjacent to the source, each to an 8-adjacent ring square; never through the source; stops at units and walls; blocked by Rooted, Restrained and Anchored; never provokes; stops before hazards in M1 (weapons 7.6). All forced movement one hit causes sums to at most 3 squares and resolves once; collision damage applies only if a non-verb source contributed; verb-sourced movement moves a unit at most once per round (weapons 7.2 rule 4). | Lead |
| W-9 | 18.3, Disarmed (Later) and Staggered | Disarmed: add monster cooldown sources; Fortitude save; advantage when two-handed; main hand only; the focus rule; the monster-side blocking rule only after the owner moves. Staggered's "shield bashes" source means a bash with the Bashing Rim property or a Technique, not the base bash. | Lead |
| W-10 | 18.4, Prone, Sources | Topple's Prone is tagged `trip`; Topple at most once per round per target; condition verbs never fire on reactions | Lead |
| W-11 | 18.9, Coated, Sources | `coating` consumables tagged `launcher` (special arrows and bolts) or `blade` (edge oils) (consumables design) | Lead |
| W-12 | 18.10, new entry **Notched**; 18.1, waves table, Later count 69 → 70 | Notched (Later), exactly as weapons 5.6. Later conditions are outside the 32-condition budget (16). | **Client (agreed 2026-11-17, Q2);** the landing and repair details in 5.6 are the lead's |
| W-16 | 18.10, Pitted Armour, Sources | Add: the Crushing property (enemy-facing; reduces the monster's armour value, natural armour included; Later); the acid ooze touch (Later) (weapons 5.5, 17.10, 18.3) | Lead |

**Main design (2026-11-15):**

| # | Section | Change | Approved by |
|---|---|---|---|
| W-14 | 2, Anti-goals, "Protected content" bullet | The credits carry the verbatim SRD 5.2 (weapon masteries) and SRD 5.1 (rarity names) CC-BY-4.0 attributions; adapted SRD records carry an `adapted` flag (weapons 19) | Lead |

**Dependency pointers (all six parents):**

| # | Section | Change | Approved by |
|---|---|---|---|
| W-15 | Stat model 10, "Items and inventory" bullet; ability model 20, "Items and inventory" bullet; conditions 25, "Items and inventory" bullet; enemies 25, "Items and inventory" bullet; items design 17, row 1, and 24, "Category designs" bullet; main design 10 | Weapons and foci are no longer pending: point to this document. Disarmed and Coated stay Later; armour, starting kits, consumables (coatings), scrolls and wands remain open in their category designs. | Lead |

**No change** to "No durability, wear or weights" (enemies 2, 4, 15.1,
15.8, 15.9; items design 2, 3.1) beyond W-6's citation fix. **(agreed,
client question 1)**

None of these touches an agreed row except W-1 and W-12, which the client
approved. Stat model 8.2 (agreed) is not changed: weak-target riders are
left as they are (7.2 rule 9).

## 26. Depends on systems not yet designed

- **Classes and offers:** concepts settled in
  `2026-11-17-classes-concepts-design.md`; its stage 2 owes the binding
  precision feature of each Dexterity-main class with its simulator
  evidence, melee and launcher (6.4); each class's training lists; the
  Technique option and the "verb training (family)" offer (7.3);
  subclasses keyed to verbs or groups; disarm Techniques (Later). The M1
  slice class is the Vanguard, so no staff ships in M1 **(change CL-13d)**.
- **Armour and shields:** designed in
  `2026-11-17-armour-design.md`: the bash sub-record, buckler, Reliquary
  Shield and tower shield (17.9, W-13; armour design 10) and Pitted
  Armour (Crushing, Later; armour design 9.4) **(change AR-44)**.
- **Consumables:** consumables, scrolls and wands are settled in the
  consumables design (2026-11-17); coatings and Coated stay Later (W-11)
  **(change CN-26)**.
- **Starting kits:** weapon sets per class and companion (items design 17,
  row 3).
- **Companions:** kits drawn from these families; the D-c playtest.
- **Death and healing:** designed in `2026-11-17-death-healing-design.md`:
  the crit rule is settled (S-3, death and healing design 9). Every guard
  and parity figure here assumes SRD double dice for every unit (enemies
  25, S-3) **(change DH-48)**.
- **Dungeon generation:** a cover decision in M2 (9.5); room shapes that
  give corridors and open rooms (Skewer, Cleave, reach).
- **Enemy content:** the rust, acid and ooze types that Notch (Later), and
  monster cooldown disarms (Later).

## 27. Open items for the client

None. Approved by the client on 2026-11-17.

## Appendix A: how the review findings were resolved

**Identity**

| Finding | Resolution |
|---|---|
| Handaxe dominated the shortsword | The handaxe has Cleave; validation rule 13 bans strict dominance |
| Verb and family conflicts between drafts | One canonical catalogue (17); launcher verbs adopted; each family has one owning group |
| No Dexterity melee in M1 | Rapier in M1 |
| Hook dead on reach 1 | One Hook text plus the `shift` kind (W-8) |
| Push and Hook forced on the wielder | Both optional ("may") |
| Nick duplicated | Dagger only; light hammer cut |
| Too many Parry sources | Main-gauche cut; buckler loses Parry; Parrying only on non-Parry families |
| Quarterstaff and staff were two records | One `staff` family |
| No anti-armour or ranged control in M1 | Flail (Pry) in M1; sling in M1 |
| Verb creep | 15 verbs; Clinch, Ensnare and the lance cut |
| Weak or empty groups | Every group ≥ 3 families across waves; club cut |
| Properties eroding verbs | Copy rule (18.1); Second Edge cut |
| Vex chaining | Vex fires only on a hit without advantage |
| Trident dominated the other versatile weapons | The trident takes the spear's chassis, 1d6 / v1d8; thrown-capable families sit a die step below their peers (9.3) |
| Unreadable verb triggers | Mandatory `previewHint` |
| Shield bash rider | riderFlag false (W-9) |

**Balance**

| Finding | Resolution |
|---|---|
| Parity collapses at level 5 | Exploit scales with level; client question 3 answered |
| Hybrid Dexterity + Strength exploit | Dexterity attacks add no Strength (W-1) |
| Vex numbers wrong | Recomputed in 6.4 |
| Monster depth claims wrong | One-die rule revised (W-5) |
| Collision damage | None from verbs (W-8) |
| Movement lockout | Once-per-round bounds and a lockout report |
| Topple on reactions | Never on reactions |
| Pike Set fired on disengage | Threatened-zone rule (8) |
| AI could not value verbs | Static values and the Set-zone term (W-4, W-5); the reaction priority rule was later dropped for policies with filters |
| Graze too strong | Once per turn, not on reaction misses; "adds proficiency" cut |
| Exploit from team advantage | Team gates and the self-generated fallback |
| Wrong parity baseline | Greatsword is the reference |
| Edge on weak targets | Weak-target riders stay as stat model 8.2 agrees; repeats on one target are already stopped by ability model 9.3's "already has that condition" gate (7.2 rule 9) |

**Consistency**

| Finding | Resolution |
|---|---|
| Monster loadouts invalid under the guards | W-5 |
| Reaction caps broken | W-5 |
| Helped did not support Vex | W-7 |
| Condition budget misread | 7.2 rule 6 |
| Exploit asymmetric | Applies to every unit (6.3) |
| Agreed text changed without the client | W-1 and W-12 went to the client; ability model 10.3 untouched |
| Topple and rider double saves | Shared save slot |
| Disarmed recovery contradicted the settled rule | Settled rule kept (16) |
| Net on a `fight` pool | Net cut as a party weapon |
| Missing change entries | Section 25; Hedgewall cut |
| Spell disadvantage in melee | Weapon attacks only (9.2) |
| Spiked property and Quickdraw keys | Spiked dropped; co-occurrence ban |
| Licensing | Section 19 |
| Keenness conflicts | 5.3, 5.7 |
| Names and signature pricing | Vowkeeper, Snap Casting, Headsman's priced as a signature |
| Parity gate in M1 | Applies from M2 |

**Revision checks (builder, consistency and play; 2026-11-17)**

| Finding | Resolution |
|---|---|
| No attack-resolution order | The weapon attack pipeline (7.5) |
| Skewer undefined at range and in edge cases | Line, next square and second-target rules (7.6) |
| Reactions in mid-movement undefined | Step-wise movement, segments and triggers (8) |
| Monster Exploit at maximum broke the floor-1 roster; no validation rates | Expected value in band and guards, as the client decided; `weapons.json` rates; worked checks for artillery and minions (6.3, 15.1) |
| Set plus the unit's turn unchecked | Round guard for Set; pike soldiers from room level 2 (15.1, W-5) |
| Ability choice undefined | Integer formula, knowledge state, recorded once (6.2) |
| Turn scope of once-per-turn flags | Global turn ids; own-turn verbs listed (7.2 rules 3 and 10, 20.3) |
| Movement facts too thin | Squares moved, adjacency at turn start, Lunge targets (7.6, 20.3) |
| Dice rules overlapped | Damage dice (6.6) |
| Hands in a light pair, off-hand timing, negative ability | Hands as set positions (11.3) |
| Data shape gaps | `hands`, `light`, `finesse`, `minReach`, `strengthClass`, `thrown {range}`, richer verb records; attack ability derived (20.1) |
| Verb-sourced movement unclassified | One forced-movement rule for every source (7.2 rule 4, W-8) |
| Shift had no path | Ring movement (7.6, W-8) |
| Threatened zone ignored line of sight; reach 3 and `long` | Visible, attackable zone; `minReach`; reach 3 only on `minReach` 2 weapons (8, 18.2) |
| Set contents missed foci; staff grip | Slot rules (11.1, 11.2, W-6) |
| Notched edge cases | Landing, re-application (conditions 6.3), repair (5.6) |
| Unverifiable validation rules | Rules 2, 10, 11, 14 rewritten (21.1) |
| W-1 left the Strength row and forbade class precision features | W-1 extended and reworded |
| Focus properties copied focus verbs | Focus verb signatures (18.5) |
| Weak-target rider limit touched an agreed row | Dropped from 7.2 rule 9 |
| Training on items versus families | W-6 extended |
| Missing change rows | W-4 (7.3 filter), W-5 (5.4 bullets, 15.5, Brute row), W-6 (mock-up), W-16 (Pitted Armour) |
| Precision would break monster validation; gate used a median | Precision folded into monster flats; per-class gate; safety veto (6.5) |
| Longbow falls with level | Per-family launcher floor; Aim rewritten to count for Exploit, keep-higher roll dropped (client question 4, option a, agreed 2026-11-17; 6.3, 6.6, 7.1); Hunter's Longbow takes Steady Aim |
| Warhammer dominated the greatclub | Greatclub takes Topple; rule 13 ignores training |
| M1 monster verbs could not fire solo | Warhammer soldier and sling artillery by default (15.3); party and monster trigger rows |
| Launcher safety unmeasured | Damage-taken report and the gate's safety veto (21.3) |
| Dual wielding in M1 had one light family | Handaxe moved to M1 |
| Exotic training empty until Later | Stated; builders do not gate on it (7.3) |
| A level 0 minion's d4 exceeds its 50% band (enemies 5.4) | Minions roll one die size smaller, minimum d4, with a level 0 exception (W-17, 15.1) |

## Appendix B: sources

- The six parent documents, all in `docs/superpowers/specs/`.
- The lead's weapons proposal and review findings (2026-11-17), with the
  client's answers to questions 1–4.
- The parity figures in 6.4 come from a Monte Carlo script that is not part
  of the project, except the longbow row, which is the lead's estimate for
  the agreed Aim; the simulator replaces them from M2.
