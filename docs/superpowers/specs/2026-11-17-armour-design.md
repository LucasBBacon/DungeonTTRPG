# Armour and Shields: Design

Date: 2026-11-17
Status: Approved, 2026-11-17
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design) and
`2026-11-17-weapons-design.md` (weapons design)

Items marked **(agreed)** were decided by the client on 2026-11-17 for
the three questions this design raised (equipment slots, armour values,
piercing), either directly or by choosing the lead's recommendation.
Items marked **(lead's call)** were filled in by the design lead and are
confirmed when this document is approved; the client confirmed on
2026-11-17 that the lead's calls in the proposal stand. Items marked
**(settled)** restate a rule from a parent document. Items marked
**(change)** amend an approved document and are listed, with an id, in
section 21. Once approved, every item here is binding on the builders.

This is the second **category design** on the items design's roadmap
(items design 17, row 2): body armour, shields, and the equipment-slot
model, including worn pieces (helms, gloves, boots, cloaks) as trinket
kinds. It takes the weapons design's shield hand-over (weapons 17.9) as
input. The trinkets design (items design 17, row 6) inherits the worn-piece
rules and location domains set here.

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. Tuning numbers live
in one file, `armour.json` (16.2). The builders own them and the simulator
calibrates them (section 20), except where a value is marked as a client
decision.

**SRD check (lead's call).** SRD 5.2 armour names and values, and the SRD
magic-item mechanisms adapted here (mithral, adamantine, arrow-catching,
the rust monster), are written from memory. **Every SRD 5.2 name, value and
magic-item mechanism must be checked against the SRD 5.2.1 document before
any armour, shield or property record is written**, and every record
flagged `srd` cites the entry it was checked against (section 15).

**Waves:** M1 is the vertical slice (one class, levels 1–3, one floor,
three enemy types, solo hero, basic loot). M2 is the full run with
companions. M3 adds profile, unlocks and boons. Later is everything after
M3 that no milestone names, including M4. **(settled, main design 9;
items design header)**

## 1. Intent

Armour decides how often a unit is hit, what it gives up for that, and
how it plays beyond its AC number. The parents settle the AC formula by
category, the shield on top, the heavy-armour Strength threshold and
stealth penalty, that armour never reduces damage, one armour place and
two trinket places per unit, the rest gate on changing them, and the
shield's bash as a weapon. **(settled)** They leave open the values, the
costs of each category, what makes one armour play differently from
another, how shields differ, how piercing and Pitted Armour act on
armour, how monsters wear it, and whether helms, gloves and boots get
their own slots. This document closes those questions.

The system should make every armour find worth reading, keep the party's
AC on the reference curve whatever drops, give each category a real trade,
and show worn pieces on the character without adding slots the loot
volume cannot fill. **(agreed)**

## 2. Goals, non-goals, anti-goals

### Goals

- **The category sets the numbers; the family sets the verb.** The
  category (light, medium, heavy) fixes the AC formula, Dexterity cap,
  Strength need, noise, speed and Acrobatics. Each family has exactly one
  passive **armour verb**, the weapons I1 pattern. **(lead's call)**
- **Any dropped armour is usable.** Values are banded by the wearer's
  proficiency band, so a floor-1 find is never obsolete, and piercing
  touches every armour, so no armour is a dead end against a weapon.
  **(agreed, Q2 and Q3)**
- **Each category pays a real cost** (6.2, section 7). **(lead's call)**
- **Worn pieces are real items with a body location** and show on the
  silhouette, inside the agreed two worn-item slots. **(agreed, Q1)**
- **One line per armour.** "Chain mail: heavy, Ringed." **(lead's call)**
- **Every verb is previewed** before the player commits. **(settled
  principle, ability model 8.3)**
- At most four things to read per unit in combat. **(settled, ability
  model 11.4)**

### Non-goals

- **Damage reduction** of any kind. "Armour is hit or miss." **(settled,
  stat model 6)**
- **Durability, wear and weights.** **(settled, enemies 2, 15.9; items
  design 2)** Pitted Armour (Later) is a condition on the unit, not wear.
- **Donning and doffing times.** The rest gate and Equip now already price
  armour changes. **(lead's call)**
- **A separate slot per body piece.** **(agreed, Q1)**
- **Set bonuses.** **(lead's call)**
- **Fidelity to the SRD 5.2 armour table.** We keep the categories and
  formula shape, adapted and attributed (section 15). **(settled, main
  design 2)**

### Anti-goals

- **An AC treadmill through loot or coin.** **(agreed, Q2)**
- **Flat +N armour.** A tier is a value budget, never +1 AC. **(settled,
  items design 12.4, 12.5)**
- **Dead +1s and a "missing helmet tax."** No AC from pieces. **(agreed,
  Q1)**
- **The plate-wearing caster.** Hard training with class ceilings (7.2).
  **(lead's call)**
- **Stacked defensive reactions and stacked disadvantage** (section 11).
  **(lead's call)**
- **Bookkeeping.** Any count the core could track. **(settled, ability
  model 2)**
- **Protected content.** **(settled)**

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Stat model 6, Armour class row (agreed) | Unarmoured 10 + Dexterity; light value + Dexterity; medium value + Dexterity up to +2; heavy flat, needs a Strength threshold; a shield adds on top | The AC query (6.1); values by band (6.2, AR-1) |
| Stat model 6, Passive stealth row (agreed) | "less a heavy armour penalty" | Clatter sizes it at −5 (7.3); AR-2 is a pointer only |
| Stat model 6, Speed row | Changed by armour, among others | Weighty and Strained (7.1, 7.3) |
| Stat model 6, rules | "Armour is hit or miss. It never reduces damage." | No damage reduction (4, decision 5) |
| Stat model 8.1 (agreed) | Piercing "ignores part of the target's armour" | P-mid (section 9) |
| Stat model 8.2 (lead's call) | Piercing's rider acts on the attack roll, larger against a weak target | Sized in 9.1 (AR-3) |
| Conditions 18.5, 18.9, 18.10 | Corroded (M2): AC −N, +1 against heavy; Fortified; Pitted Armour (Later): cap 2, `untilCamp`, Mechanics repair | Section 9; AR-11 to AR-15 |
| Conditions 18.7 | Hushed ignores the heavy-armour penalty | Clatter removal (7.3, AR-13) |
| Conditions 16 | At most 32 conditions in M1 and M2, budget full | Verbs use existing conditions; Strained is never a condition |
| Ability model 6.2 | `fight` pools for minor uses only | Every armour, shield and piece `fight` use is ruled minor (8.1 rule 5, AR-6) |
| Ability model 7.3 | Reactions resolve on automatic policies, or "ask me" | The reaction window algorithm (10.4) |
| Enemies 5.1, 5.2, 6.1, 6.2 | AC by the stat model formula; natural armour uses the medium formula; band ±1 after the role step; reference hero AC 15/16/17 | Monster loadouts (section 12) |
| Enemies 6.4 | Permadeath guards | Also checked at the lowest legal party AC (11, AR-20) |
| Enemies 15.1, 15.3 | About 6–8 notables per floor, camp 1–2, family caps, no junk | Slot verdict (5.1); loot shares (5.7) |
| Enemies 17.1, 17.2 | Role answers; soldier has Interpose or a Guard verb, never both; Plated "counts as heavy" | Role-answer rule (8.1), Plated (12.7) |
| Items design 4 decision 1, 5.1, 5.2 (agreed) | One armour place, two trinkets; both change only at a short rest, camp or Equip now on first entry | Slot model (section 5); AR-30 |
| Items design 5.2, 12.3 | Armour and shields are bulky in the pack | Validation rule 10 |
| Items design 12.2, 12.4–12.6 | Family caps (armour 2, shields 2, trinkets 1); buff test bans always-on +AC; rarity is a value budget; at most 3 properties | Property tables (section 14) |
| Items design 6.3 | Suggested recipient by gain | Armour gain with a tie-break (16.4, AR-32) |
| Weapons 10.1, 10.2 | Soft Strength thresholds with Strained; soft weapon training | Strength soft here; training hard (7.2) |
| Weapons 11.1, 11.2 | Shield in `off`; weapon + shield, rod + shield legal; a shield blocks versatile | Shields (10.1); buckler grip rule |
| Weapons 17.9 (W-13) | Shield bash 1d4 B, Push, riderFlag false, replaces one attack; Stagger only from Bashing Rim or a Technique; buckler no Parry, does not block versatile; tower shield Interpose, soft Strength +1; Reliquary Shield with `focus{}` | Section 10 |
| Weapons 18.1, 19 | Copy rule for properties; SRD check before data | 8.1, section 15 |
| Main design 2 | No D&D names; SRD content carries attribution | Section 15 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items design 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored.

### 3.3 Findings in the parent documents

Errata found while designing; each is fixed by a change in section 21.
**(lead's call)**

| # | Finding | Fix |
|---|---|---|
| F1 | The enemies 19.1 raider (a level 2 soldier with a spear and no shield) breaks the soldier kit rule ("reach weapon or shield + Shove") | Add `offHand: item.shield`; armour becomes gambeson (M2). With Dexterity in the secondary slot, AC 12 + 1 + 2 = 15 at L0–2 and 12 + 2 + 2 = 16 at L3–4, in band at every shift from −2 to +2 (AR-28). |
| F2 | Enemies 5.6's gear line reads "Hide: light"; the SRD has hide as medium | Hide is medium: the non-metal medium armour (AR-19) |
| F3 | Plated's "counts as heavy" could be read as switching the unit to the flat heavy formula, and its +2 AC has no place in the AC query | Category-reading rules only (Corroded, piercing, Clatter); the AC formula is unchanged; its +2 is the `traits` term of the AC query (6.1); its speed −1 is the heavy speed penalty, paid once (7.3, AR-27) |
| F4 | Enemies 7.3 says "AC moves by the table's AC change", which does not fit loadout AC computed from a formula | AC is recomputed from the loadout; a legal shift whose AC leaves the band fails validation (AR-22) |
| F5 | "A tower shield means soldier" names a Later item | "A tall shield means soldier"; M1 soldiers use a tall-shield skin of `family.shield` (AR-23) |
| F6 | Items 16 gives the armour sub-record a stored `value`, which collides with the offer-value `value` | `armourValue` is derived and never stored (AR-35, AR-36) |
| F7 | Under banding, items 6.3's armour gain is 0 between same-category armours | Tie-break (16.4, AR-32) |
| F8 | Reaction AC bonuses stack (an ally's Interpose plus your own Parry gives +4) | At most one per attack roll (10.4) |
| F9 | Ability increases come at levels 4 and 8 (stat model 5.1), not 5 and 9 | The reference check (6.3) uses 4 and 8 |
| F10 | "Dexterity-main lurkers are effectively unarmoured at levels 0–4" is wrong from level 1 | Light + Dexterity +2 = 14 is in the lurker band from level 1 |

## 4. Framework decisions

The client chose the first three on 2026-11-17. **(agreed)** The rest are
the lead's calls, which the client confirmed stand.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Equipment slots (Q1) | **(a) Model E:** one body-armour slot plus 2 worn-item (trinket) slots. Helms, gloves/gauntlets/bracers, boots and cloaks are worn-piece trinket kinds with `trinket{kind, wornOn}`; at most one per body location (rings two); never any AC; one fixed location verb each; Common or Uncommon only. **E+ (3 slots) pre-approved, off by default**, behind the lead's gate (5.10). Approves change AR-30. **(agreed)** | (b) E+ from the start: spends loot share before evidence says the slots are crowded. (c) P1, a real head slot: +2–3 notables per run and a reopened agreed row for one place. (d) P3, body + head + hands + feet: +4–5 notables per run, trinkets cut to 1. Full paper doll: worst friction, fit and loot health (5.2). Any model where pieces give AC: breaks the reference AC and every guard. |
| 2 | Armour values (Q2) | **(a) Banded by proficiency band:** light 12 + Dexterity; medium 13/14/15 + Dexterity (max +2); heavy 16/17/18, Dexterity ignored. Level-up line "AC +1: armour band". **(agreed)** | (b) The SRD ladder with depth gates (leather 11 … plate 18): AC becomes a coin and drop-luck treadmill, older finds go dead, monster loadouts must swap families by level. |
| 3 | Piercing (Q3) | **(a) P-mid:** −1 against any armour value (worn or natural), +1 more against `CountsAsHeavy`, +1 more against a piercing-weak target, ×2 with Puncture; shared reduction cap of 4 with Pitted Armour. Client rationale: it lets players work with any dropped armour. **(agreed)** | P-heavy-only: narrows agreed 8.1; piercing would do nothing against most monsters and contradict the soldier's answer "piercing or acid". P-flat (−2 against heavy only): heavy becomes worse than medium against archers. |
| 4 | Where armour identity lives | **I1: one passive verb per family**, used by any trained wearer, monsters included **(lead's call)** | I2, one bearing per category: no choice within a category. I3, numbers only: fails "not just an AC number". |
| 5 | Damage reduction | **None.** AC only. **(settled, stat model 6)** | Heavy DR 1: amends an agreed row, cuts 18–22% of floor-1 monster damage and nothing deep, breaks the 6.2 damage table and the guards. Physical resistance as a verb: far above a verb's size, eats the monster resistance budget. |
| 6 | Requirements | **Training hard, with class ceilings; Strength soft (Strained)** **(lead's call)** | Soft training: armour is rest-gated, so the emergency-pickup case does not apply, and it reopens the plate caster. Hard Strength: kept as the named fallback (7.1). |

Also confirmed to stand **(lead's call)**: heavy costs Strength +1/+2,
Stiff, Clatter −5 and Weighty speed −1; medium is Stiff; Turning is cut;
the buckler grip rule; the Reliquary Shield is notable-only; no set
bonuses; every bound in section 11, including the binding caster-defence
requirement on the class designs.

### 4.1 How other games handle armour and slots

Mechanism references only; no names or text are taken from any of them;
details as recalled. **(lead's call)**

| Game | What it does | We take | We avoid |
|---|---|---|---|
| **5E SRD 5.1 / 5.2** | Light, medium, heavy; medium caps Dexterity at +2; heavy has a Strength score need (speed −10 ft below it); some armour gives stealth disadvantage; shield +2; +1/+2/+3 magic armour; mithral and adamantine | The three categories and the formula (stat model 6); shield +2; the soft Strength penalty (speed); the mithral shape (Fitted) and the adamantine shape (Adamant), adapted | +N armour (the buff test bans it); donning and doffing times; a per-armour stealth table; a gold-bought AC ladder |
| **Pathfinder 2e** | Item bonus, Dexterity cap, check penalty, speed penalty; armour groups grant a specialization effect; potency runes | **One group-level effect per family** (our verb); a check penalty (Stiff); speed as heavy armour's cost | Potency runes (+N); resistance-style specialization (damage reduction) |
| **Battle Brothers** | Head and body slots; armour is a degrading point pool needing repair; heavy gear costs fatigue and initiative | Head and body as the readable silhouette; heavy gear costs tempo | Armour as HP or durability |
| **Darkest Dungeon** | Base armour rises with town upgrades; **2 trinket slots per hero**, each trinket a trade-off | Two worn-magic slots where every item is a choice; base protection rising with progression (banding) | Set bonuses; items useful to only one class |
| **DCSS** | Body armour, shield and several auxiliary slots (helmet, cloak, gloves, boots); auxiliaries are mostly +1 AC plus an ego; heavier armour hurts casting, evasion and stealth | Armour as a real trade against casting and stealth (hard training, Clatter); pieces as property carriers | A long list of tiny AC sources (the "dead +1" feel) |
| **Diablo, Path of Exile** | Full paper doll; implicit property plus rolled affixes; sets; very large drop volume | The base item's built-in implicit (our family verb) | Slot inflation that needs large loot volume; sets; flat +N |
| **Baldur's Gate 3** | 5E on a full doll (head, cloak, body, hands, feet, amulet, 2 rings); AC mostly from body and shield, pieces mostly carry effects | **AC from body and shield only; pieces carry effects** | Its loot volume and inventory management |
| **Divinity: Original Sin 2** | Armour pools absorb damage and block control until depleted | The lesson only: control became binary and fights a race to strip pools | Armour as a damage pool |
| **Pillars of Eternity** | Damage reduction; heavy armour slows action recovery | Heavy armour costs tempo (Weighty) | Damage reduction |

**Overall:** categories as the chassis, one verb per family, costs that
make each category a trade, and pieces as property carriers. Avoided: +N,
damage reduction, durability, sets, and slot inflation without the loot
volume to fill it.

## 5. Equipment slots

The client asked whether each piece of armour (helmet, chest, gauntlets,
boots) should be its own slot. The answer is model E. **(agreed, Q1)**

### 5.1 Why not one slot per piece

Each piece as its own slot is a good fantasy and the wrong rule here, for
reasons of arithmetic:

1. **Loot volume.** About 33–42 notables per run (6–8 per floor across
   4–5 floors, plus camp 1–2; roughly one per fight; settled, enemies
   15.1, 15.3). A unit has 5 notable-holding places today (2 weapon sets,
   armour, 2 trinkets), 20 for a party of four. Head, hands and feet add
   12 places (+60%); one real offer per new place needs about +12
   notables per run (+30%), breaking the "1 item or fewer per summary"
   and "about 10 seconds" targets. Without the extra loot, each new slot
   sees 0.3–0.5 offers per run and stays empty.
2. **AC.** Pieces adding AC (+3 for three pieces) take 15 points off every
   monster's hit chance and break the reference AC, the 6.2 table and the
   6.4 guards. Splitting the body value across pieces gives dead +1s and
   a missing-helmet tax. Pieces with no AC are trinkets with a body
   location, which is model E.
3. **Readability.** A full doll is 28 more spots to scan per rest for a
   party of four, against the items design's management goals and "four
   things to read".
4. **Art.** The camera is a fixed orthographic isometric angle (main
   design): gloves and boots are a few pixels, and modular skinned pieces
   multiply by families, species and body types, with clipping QA.
5. **Sets.** Under family caps, no stash and curated drops, a 3–4 piece
   set is completed in well under 10% of runs.

What we keep from the idea: helms, gloves, boots and cloaks exist as real
items, each tied to its body location and that location's design domain,
shown on the model or silhouette, one per location. What we drop: a slot
count per piece.

### 5.2 Verdict and judges' scores

Three independent judges scored three worked-out slot models, 0–10 per
criterion (higher is better; averages; totals out of 70):

| Model | Decisions per run | Customisation fun | Low friction | Readability | Fit with approved docs | Build and art cost (high = cheap) | Loot health | **Total** |
|---|---|---|---|---|---|---|---|---|
| **E: one body slot + 2 worn-item slots, pieces as trinket kinds (chosen)** | 6.7 | 6.3 | 9.0 | 9.0 | **10.0** | 9.0 | 9.0 | **59.0** |
| P3: body + head + hands + feet slots, trinkets cut to 1 | 7.0 | 7.0 | 6.3 | 7.0 | 5.0 | 6.3 | 6.0 | 44.7 |
| Full paper doll: body, head, hands, feet, back + 2 jewellery | 7.0 | **8.3** | 4.3 | 5.0 | 3.7 | 4.3 | 4.3 | 37.0 |

All three judges chose E. Their shared concern: E delivers the idea mostly
through flavour and display, and 2 slots shared by 7 kinds can feel
stingy. That is why E+ is pre-approved as a lever (5.10).

**Verdict: model E, with E+ pre-approved and off.** **(agreed, Q1)**

### 5.3 Places per unit

Approved places and counts are unchanged except for what the trinket row
holds (AR-30). **(agreed)**

| Place | Count | Holds | AC | Changes |
|---|---|---|---|---|
| Active weapon set | 1 | Weapon, weapon + shield, and so on (weapons 11.1) | Shield bonus, only while active | Bonus-action swap (settled) |
| Spare weapon set | 1 | As above | None while spare | As above |
| **Body armour** | 1 | One light, medium or heavy armour | **The only armour source of AC** | Short rest, camp, or Equip now on the item's first entry (settled) |
| **Worn items (trinkets)** | **2** (plus trinket-slot grants, as now) | Helm, gloves/gauntlets/bracers, boots, cloak, amulet, ring, charm, capacity trinket | **Never** | Same rest gate (settled) |
| Belt, pack | Unchanged | Unchanged | — | Unchanged |

### 5.4 Kinds and the location rule

**(agreed, Q1; details lead's call)**

- Each trinket record has `trinket{kind, wornOn}` (the client's record
  shape). `wornOn` is fixed by `kind`: `inventory.json` gains
  `kindToLocation`, the full kind enum with its location, and validation
  rule 17 fails any record whose `wornOn` differs from it, so the stored
  field can never disagree with the derived one. **(lead's call)**

  ```json
  "kindToLocation": { "helm": "head", "hood": "head", "circlet": "head",
    "gloves": "hands", "gauntlets": "hands", "bracers": "hands",
    "boots": "feet", "cloak": "back", "amulet": "neck", "ring": "finger",
    "charm": "none", "capacity": "none" },
  "wornLocations": { "head": 1, "hands": 1, "feet": 1, "back": 1,
    "neck": 1, "finger": 2, "none": 2 }
  ```

  The `none` count is *(illustr.)*.
- A unit wears **at most one item per location (two rings)**. **(lead's
  call)** An equip targets a trinket slot index. The check runs on the
  resulting set, with the item already in the targeted slot removed:
  `LocationOccupied` means the *other* slot holds an item of the same
  location beyond that location's count. So putting boots into the slot
  that holds the old boots is a replacement, and putting them into the
  other slot is refused.
- With 2 slots this mostly prevents two helms, so it is chiefly a
  readability rule; the `LocationOccupied` report (17.3) measures whether
  it ever binds.
- "Belt" is never a kind name, because it is a place.
- **Kind is not family.** Items 12.2 caps trinket families at 1 per run,
  so `family.helm` would mean one helm per run. Kinds are a field;
  families are finer (`family.visored_helm`). The trinkets design ships
  **at least 3 legal families per piece location** (head, hands, feet,
  back), counted as a running total of the families available in each
  wave from M2. **(lead's call)**

### 5.5 Piece rules

**(agreed, Q1; details lead's call)**

- Each piece carries **exactly one fixed location verb** from its
  location's table (shown as `obvious`).
- **Common or Uncommon only.** A Common piece has only that verb; an
  Uncommon piece adds one roll from the same location table. No Rare
  pieces: boss signatures stay on weapons and body armour.
- **No AC of any kind.** No trinket or piece property may declare any
  effect kind that reads or writes AC, charged or conditional included.
- Pieces have no training, Strength need, Clatter or Stiff.
- **Reaction cap:** at most one reaction-granting property per unit across
  its armour, shield and trinkets. An automatic trigger that fires off the
  wearer's turn to protect or affect another unit counts as
  reaction-granting for the cap, though it spends no reaction (trinkets
  design 7.1). **(lead's call)** The equip is legal;
  the cap is a suppression, never a refusal. `ActiveReactionProperty(unit)`
  picks the one that works by fixed slot order (body armour, then the
  shield in the active set, then trinket slot 1, then trinket slot 2); the
  others are shown greyed with the reason `ReactionCap`. The verdict says so
  before the equip ("Shelter would be greyed: one reaction property").
- **`ButtonCap` and `DuplicateAnswer`** suppressions use the same fixed
  slot order and pre-equip verdict as `ReactionCap`. `DuplicateAnswer`
  counts armour and shield prevent properties, each declaring `answers[]`
  (trinkets design 17.1); they win by slot order (trinkets design 7.1).
  **(lead's call)**
- **One-table rule:** each property lives in exactly one table across the
  weapon, armour, shield and trinket tables, and no always-on piece
  property copies an armour verb, weapon verb or another table's effect
  (so no "Sure-Footed boots", Quickdraw gloves or Far-flight gloves).
  Eligibility is not duplication: a property in one table may be eligible
  on another category's items (Quickdraw and the Push signatures on
  shields, 14.5).

### 5.6 Location domains

Handed to the trinkets design (items 17, row 6; AR-37). All examples are
M2 and *(illustr.)*. **(lead's call)**

| Location | Domain | Examples (one location verb each) |
|---|---|---|
| Head | Senses, mind, critical hits | **Visored Helm:** a critical hit against you attempts no rider; also, once per fight (`fight` minor), the first hit from an attacker you could not see that would attempt a rider attempts none (trinkets design 13.1). **Warden's Coif — Unshaken:** Frighten attempts against you have disadvantage, and you have advantage on saves against Frightened (moved here from armour). **Barred Helm:** advantage on saves against Dazed. |
| Hands | Grapple, handling, giving | **Grappler's Gauntlets:** escaping Grappled or Restrained costs a bonus action. **Deft Gloves:** Administer costs a bonus action instead of an action. |
| Feet | Movement, Prone, hazards | **Strider's Boots:** standing from Prone costs 1 square. **Ledgewalker's Boots:** advantage on hazard-edge and landing saves. **Runner's Boots:** once per fight (`fight` pool, minor; AR-6), Disengage as a bonus action. |
| Back | Elements, stealth | **Weathered Cloak:** advantage on saves against one element's rider: Burning or Chilled in M2, under the rider-ownership rule (trinkets design 5.7) (moved here from armour). **Grey Mantle:** your Hide checks against a unit that has not seen you this fight have advantage. |
| Neck | Life and the body | Trinkets design 5.5 |
| Finger | Your strike | Trinkets design 5.5 |
| None | Stored moments (charm); carrying (capacity) | Trinkets design 5.5 |

### 5.7 Loot

No budget change. About 36 notables per run *(illustr.)*. **(lead's
call)**

| Category | Per run | Offers per slot |
|---|---|---|
| Body armour notables | About 6, including a boss signature or a camp item | About 1.5 per body slot |
| Shield notables | About 2 | Offered only to shield users |
| Trinkets | About 9, of which about 4–5 are pieces | About 1.1 per trinket slot |

- An empty trinket slot means "not yet", never a penalty.
- **Location hunger in smart loot** is data, not a new draw rule
  **(lead's call)**: the party's build tags include `worn.open.<location>`
  for every piece location that some member leaves open, and each piece
  record carries `requires: [worn.open.<its location>]`. The existing
  smart-loot weighting toward `requires` tags (enemies 15.3 step 2, agreed
  and unchanged) then favours open locations, and the weight falls away
  once every member wears that location.
- **Suggested recipient:** a member with a free slot and a free matching
  location comes before value (16.4).

### 5.8 Summary card and display

**(lead's call)**

Summary card, one extra short line:

```
◇ COMMON  STRIDER'S BOOTS · boots (feet)
  • Strider: standing from Prone costs 1 square
```

Verdicts borrow typed-slot wording: "replaces Hob-nailed Boots (feet)"
(one candidate); "fits if Ember Ring leaves".

Display:

- Body-armour models are full suits with their own helmet-off head,
  gloves and boots.
- A helm is a rigid socket attachment with a `hidesHair` flag.
- A cloak hangs on a 2–3 bone chain, with no cloth simulation.
- Gloves and boots are a material or decal tint on the suit's own hand and
  foot region, plus a card icon.
- The party view shows a silhouette with worn items at their locations
  and two numbered sockets ("Worn items 1/2 · 2/2"). Empty locations are
  **never** drawn as holes: they show as the greyed suit part, or not at
  all.
- **M1 preview:** a display-only helm on the slice kit, and crested helms
  on enemy leaders (M2) for the silhouette rule. Never items, never rules.

### 5.9 Monsters and sets

- **Monsters have no trinket slots.** A carried piece notable uses the
  existing carried slot, benefit only, priced like any notable. **(lead's
  call)**
- **No set bonuses.** Tag-keyed text is allowed ("while in heavy
  armour…"). Soft theme-tag synergy, always charge-scaled and paying off
  at 2 items, is recorded as a Later option only. **(lead's call)**

### 5.10 E+: the pre-approved lever

**(agreed, Q1: pre-approved, off by default; the gate is the lead's call)**

- **What it is:** `trinketSlots` 2 → 3 in `inventory.json`. Nothing else
  changes: no AC, the location rule and piece rules stand.
- **Who switches it:** the lead only, never a builder as tuning (section
  20). The switch is reported to the client with the evidence.
- **Earliest:** once worn-piece kinds ship (M2) and the trinkets design
  has at least 3 legal families per piece location (5.4).
- **The gate (lead's call).** Its numbers are **binding lead's numbers,
  not builder tuning**: they live in an `ePlusGate` block of
  `inventory.json` that section 20 excludes from tuning, and only the lead
  changes them. The gate measures crowding, never full slots (full slots
  at the boss are the 17.3 target) and never weak items (a power gap goes
  to trinket tuning, not to E+). The lead switches E+ on when **any one**
  of these holds, measured over at least **200** simulated M2 runs with
  the current trinket roster, or over at least **10** playtest runs:
  1. **Displaced, not unwanted:** for pieces, or for jewellery (amulets,
     rings, charms) excluding Rare-tier records, the pick rate when the recipient has a free slot is
     at least **50%**, and the pick rate when both slots are full is less
     than **half** of that. A kind group picked rarely even into free
     slots fails the 17.3 power target instead and goes to trinket tuning.
  2. **Near-tie left behind:** at more than **25%** of camps, an offered
     trinket is left or sold while some member who could wear it has both
     slots full and its offer value is at least that member's weakest worn
     trinket's value less **1** (a near-tie or better that the player had
     to give up). Swapping out a trinket and re-equipping the displaced
     one at a later rest also counts as one such camp.
  3. **Players ask:** at least **3 of 10** playtesters, unprompted or in
     the exit survey, say they could not wear a helm, boots and a ring
     together and wanted to.

  ```json
  "ePlusGate": { "minSimRuns": 200, "minPlaytestRuns": 10,
    "freeSlotPickMinPermille": 500, "fullSlotPickMaxRatioPermille": 500,
    "nearTieCampMaxPermille": 250, "nearTieValueMargin": 1,
    "playtesterAsks": [3, 10], "vetoBindMaxPermille": 100,
    "landingEnvelopeMinPermille": 600, "bossHardLandMinPermille": 500 }
  ```
- **Safety veto:** E+ is not switched on if, with 3 slots, the enemies 6.4
  guards, the conditions 7.2 control guards or the AC envelope (11, item
  1) fail, or the condition landing envelope or its hero row (trinkets
  design 18.3) fails, or the `ReactionCap` and roll-mode cap (11, items 4
  and 7), `ButtonCap`, `DuplicateAnswer`, reaction-shaped triggers or the
  per-round and per-fight trinket limits bind on more than **10%** of
  fights, or the carrier 6.4 rerun fails.
- **Loot when on:** the trinket share rises by 1–2 notables per run,
  taken from weapons and consumables, inside the existing budget.
- **Approved changes when on:** AR-47 applies (section 21); the loot share
  above is this design's 5.7 table, not an enemies rule. Switching it off
  again before release is also the lead's call, on the reverse evidence.

## 6. Armour class

### 6.1 The AC query (derived, never stored)

**(lead's call)**

```
AC = base + dex + shield + traits + effects + reaction − reduction
base      10 unarmoured; worn armour's armourValue (category, band);
          natural armour = medium band + offset.
          Pitted Armour (Later) lowers the armour value by 1 or 2, never below 10.
dex       full (unarmoured, light) · up to +2 (medium; negative applies) ·
          none (heavy, either way)
shield    shield-kind bonus (active set only; 0 if the attack has Pry)
          + raise, where raise = 2 if Raise is active, the attack has no Pry
          and the effects term carries no Fortified bonus, else 0.
traits    rank, affix and trait AC (Plated +2). Not part of the armour value:
          Pitted and piercing never read it. Stacks with gear and conditions
          (gear and traits stack, conditions 6.7).
effects   conditions only, under the same-number rule (Corroded, Fortified).
          Never trinkets or properties.
reaction  at most ONE reaction AC bonus per attack roll (Parry, Interpose,
          Guarding), by the window algorithm (10.4)
reduction piercing on that attack roll (9.1), sharing the reduction cap of 4 with Pitted
```

- **Raise and Fortified (lead's call; 11, item 9):** both are +2 and never
  stack. The bonus applied is max(Fortified ? 2 : 0, (Raise and no Pry) ?
  2 : 0), so Fortified still applies against Pry and Raise adds nothing
  while Fortified is up.

- **Band** is read from the **wearer's** level (proficiency band: levels
  0–4, 5–8, 9–12), never from the item or the floor, so a hand-over moves
  the AC with the wearer and a find never ages. **(lead's call)**
- **Visible AC** is base + dex + shield + traits + effects, computed with
  Pry false, including an active Raise but no unspent reactions or
  charges. It is what the preview shows, what the AI's "lowest AC"
  targeting reads, and what validation uses. A buckler switched off by the
  two-hand grip (10.2) is out of visible AC until the flag clears.
- **`CountsAsHeavy(unit)`** is true for worn heavy armour, Plated, or the
  `armoured` trait. Corroded's +1, piercing's heavy part and Clatter read
  it.
- **Stiff** reads **worn** armour only, so natural-armour beasts are never
  Stiff. **Weighty** is `heavySpeedPenalty(unit)` = 1 if the unit wears
  heavy armour **or** has Plated, else 0; the `armoured` trait never costs
  speed (7.3).

### 6.2 Categories and values

**(agreed, Q2 for the values; costs lead's call)**

| Category | AC (b1 / b2 / b3: levels 0–4 / 5–8 / 9–12) | Dexterity | Strength | Costs | Identity |
|---|---|---|---|---|---|
| Unarmoured | 10 + Dexterity | Full | — | — | Class-feature hook ("while unarmoured"); immune to Pitted |
| **Light** | 12 + Dexterity | Full | — | None | The Dexterity-main armour; the only armour with no Acrobatics penalty |
| **Medium** | 13 / 14 / 15 + Dexterity (max +2) | Capped +2; negative applies | — | **Stiff** | For Dexterity +1 to +2; quiet |
| **Heavy** | 16 / 17 / 18 flat | Ignored either way (the Dexterity −1 armour) | Chain mail +1; splint and plate +2 (soft) | **Stiff**, **Clatter**, **Weighty** | Strength's defence job; reference +1 everywhere except against piercing |

These values are the client's decision; changing them needs client
approval. The SRD ladder is rejected (4, decision 2).

### 6.3 Reference check

Corrected for F9; monster hit chance against the enemies 6.2 table attack.

| Build | L1–3 | L4 | L5–7 | L8 | L9–10 |
|---|---|---|---|---|---|
| Reference hero (enemies 6.1) | 15 | 15 | 16 | 16 | 17 |
| Light, Dexterity +3 → +4 → +5 (both increases into Dexterity) | 15 | **16** | 16 | **17** | 17 |
| Medium, Dexterity +2 | 15 | 15 | 16 | 16 | 17 |
| Heavy | 16 | 16 | 17 | 17 | 18 |
| Heavy + shield | 18 (35% vs +4) | 18 | 19 (40–45%) | 19 | 20 (50% vs +9) |
| Worst charged stack: heavy + shield + Raise + one reaction | 22 (15%) | 22 | 23 | 23 | 24 (30%) |
| Unarmoured, Dexterity +1 | 11 (70% vs +4) | 11 | 11 | 11 | 11 (95% vs +9) |

- Light beats medium by +1 for a Dexterity-maxed build at levels 4 and 8,
  and ties elsewhere. It is the only quiet, unstiff armour.
- Medium is the armour for Dexterity +1 to +2.
- Heavy is +3 over medium for a Strength-main build with Dexterity 0, and
  pays in speed, noise and Acrobatics. Its Strength need (+1 or +2) almost
  never binds a Strength-main build, whose starting Strength is +2 or +3
  (stat model 5.1); it binds companions and off-builds (19).
- The unarmoured row is why the caster-defence requirement exists (11,
  item 2).

### 6.4 Showing banded AC

**(agreed: the level-up line; placement lead's call)**

- On level-up to 5 and 9, a unit whose worn armour's value changes shows
  the line **"AC +1: armour band"** in the level-up summary. Light and
  unarmoured wearers get no line (their AC grows with Dexterity at levels
  4 and 8 instead, which the ability-increase line already shows).
- The armour card shows the band ladder once: "Medium · 13 + Dex (max +2)
  · 14 at L5 · 15 at L9". **(lead's call)**
- A monster's gear line shows its current AC only. **(lead's call)** A
  monster armour verb that cannot fire (a minion's inert verb, or a verb
  whose trigger does not exist yet in the current wave, such as Ringed
  against an M1 party with no Bleed riders) is left off the gear line, so
  it never takes one of the four things to read; Lore shows it greyed.

## 7. Requirements and costs

### 7.1 Strength (soft)

**(lead's call)**

- `strengthClass` is `heavy` (+1: chain mail, tower shield) or `heavyPlus`
  (+2: splint, plate; SRD-shaped).
- **`IsStrained(unit)`** reads only the worn body armour and a shield in
  the **active** set, against Strength from base, species and ability
  increases (no conditions, no gear). A tower shield in the spare set or
  armour in the pack never strains. **(lead's call)**
- **Strained:** below the threshold, the unit takes speed −2 (on top of
  Weighty), the item's verb is off (tower shield: Interpose off), verb
  signatures are off; AC and other properties are kept.
- **One derived penalty per unit:** speed −2 however many items are
  Strained; one icon, reason `StrengthThreshold`, verdict "Strained: needs
  Str +1" (or +2); never a condition.
- **Fitted** removes the threshold.
- **Why soft:** it matches weapons 10.1 and keeps "slowed, never locked
  out". Named fallback: a hard threshold, if the simulator shows Strained
  wearers are common.
- **Monsters:** no issued loadout is Strained at any legal level shift
  (validation rule 19).

### 7.2 Training (hard)

**(lead's call)**

- **Four trainings, from class data:** light, medium, heavy, shields.
  **Data shape (lead's call):** a unit has an ordinal `armourTraining:
  none|light|medium|heavy`, where each step includes the ones below it,
  plus a separate boolean `shields`. `CanWear` compares the item's
  `armourCategory` against the ordinal, and shields against the boolean.
- **Untrained:** the item cannot be worn (greyed `Untrained`, "cannot
  wear: untrained"). Armour is rest-gated, so the weapons case for soft
  training (emergency pickups) does not apply. Hard training replaces
  5E's spell-failure rules.
- **Training ceiling.** Each class declares `armourTrainingCeiling` (none,
  light, medium or heavy) and `shieldTraining: allowed|never`. **(lead's
  call)**
  - The offer kind "armour training (next category)" raises the ordinal
    by one step, and is drawn only up to the ceiling. A separate offer
    kind gives "shield training", drawn only for a class whose
    `shieldTraining` is `allowed`.
  - Heavy training comes only from a class whose ceiling includes heavy,
    or from a capstone-tier feature.
  - This stops a caster climbing to plate + shield through offers.
- **Monsters** are trained in their loadouts. **(settled)**

### 7.3 Stealth, speed, Acrobatics

**(lead's call)**

- **Clatter** applies to `CountsAsHeavy` units: −5 to that member's
  passive stealth contribution (the party uses its worst member, so one
  heavy companion makes the party loud), and −5 to its own Stealth checks
  (Hide, Steal). It replaces "disadvantage on Stealth", which would be
  redundant with the existing disadvantage against an awake carrier.
  **Muffled** (a major property) and **Hushed** remove both parts.
  - The passive part sizes the stat model's agreed "heavy armour penalty"
    and changes no wording there (AR-2 is a pointer).
  - The check part is this design's rule **(lead's call)**: a gear
    modifier on the check total, of the same kind as any other gear
    modifier to a skill check (Grey Mantle's advantage on Hide). The Steal
    roll line of enemies 15.5 (agreed) is unchanged; the thief's Stealth
    simply carries the modifier.
- **Weighty:** `heavySpeedPenalty(unit)` (6.1): speed −1 for worn heavy
  armour or Plated, paid once, monsters included; the `armoured` trait
  costs no speed. **(lead's call)** A monster's authored `speed` is before
  penalties, and the derived speed (after Weighty and Strained) must stay
  within enemies 5.1's range of 3–8 (validation rule 4). So the pike
  soldier is authored at speed 6 and moves 5.
- **Stiff:** worn medium or heavy armour gives disadvantage on Acrobatics
  checks (slip past, escape a grapple by Acrobatics, narrow ground).
- **Binding requirement on dungeon generation and encounter content
  (lead's call),** the same way 11 item 2 binds the class designs: because
  Stiff is light armour's only lasting edge over medium (6.3), every floor
  from M2 offers at least **3** *(illustr.)* Acrobatics-gated choices
  (slip-past routes, narrow ground, grapplers that can be escaped by
  Acrobatics), with simulator evidence in the "Acrobatics checks per
  floor" report (17.3). **Named fallback** if that report misses its
  target after content tuning: Stiff also gives disadvantage on Reflex
  saves against hazards (hazard edges, traps and hazard squares,
  conditions 10.2–10.4). The armour values are not touched.
- **Don and doff:** none.

## 8. Armour verbs

### 8.1 Rules

**(lead's call)**

1. Verbs are **passive**, never reactions (the reaction economy stays with
   weapons, shields and charges), and never a save the attacker rolls.
2. A verb uses existing conditions of its own wave or earlier.
3. A verb is **off** when untrained, Strained or worn by a minion (inert,
   shown greyed in Lore and left off the gear line, 6.4).
4. Each verb has a mandatory `previewHint`.
5. **`fight` uses:** every per-fight use on armour, shields and pieces is
   a **minor use** under ability model 6.2 (AR-6): Ringed and Double Ring,
   Proof and Twice-Proof (each cancels one event and never adds damage,
   actions or healing), Shadowed, Warding Lining, Runner's Boots, and
   Forewarned's fight-start window. On monsters a `fight` use maps to
   `uses:1` (weapons 15.2).
6. **Roll-mode cap (AR-40; lead's call):** at most one gear-sourced effect
   that changes the roll mode of an attack against a unit applies to any
   one attack (Supple, Weave, Overlap, Forewarned, Hailguard), so stacked
   disadvantage cannot wipe out the party's advantage play (weapons 7.5
   nets one for one). At weapons 7.5 step 2:
   - the qualifying gear effects are collected. If more than one
     qualifies, the first by priority applies: unlimited passives
     (Supple, Forewarned), then once-per-round effects (Weave, Overlap),
     then charge effects (Hailguard). Ties break by gear slot order (body
     armour, shield, trinket 1, trinket 2). A suppressed effect is **not
     spent**;
   - **Overlap** qualifies only when the net mode without it is
     advantage (advantage sources outnumber disadvantage sources, condition
     sources included); it then removes every advantage source for that
     attack, so the roll is normal;
   - **charged exclusion (bound 11, item 1):** a gear roll-mode effect is
     suppressed, not spent, while the target's AC includes a charged bonus
     (Raise or Fortified), and no reaction AC bonus is offered in the step
     5 window against an attack rolled with a gear-sourced disadvantage.
     The preview names it ("Parry not offered: Supple already applies").
7. **Crit cap (lead's call):** at most 2 critical hits per unit per fight
   are downgraded by gear, counted in `critDowngradesThisFight` (16.3). At
   weapons 7.5 step 6, in this order:
   1. conditions (Fortified: "critical hits count as normal hits")
      downgrade first, spend nothing and do not count toward the cap;
   2. Proof, then Twice-Proof (free `fight` uses);
   3. Adamant charges, policy `always`; a charge is never spent once the
      cap is reached.

   A downgraded natural 20 still hits automatically; it is no longer a
   crit, so it has no crit rider and no crit doubling.
8. **Copy rule:** weapons 18.1's copy rule extends to `averb.*`.
9. **One-table rule** across all gear tables (5.5).
10. **Role-answer rule (validation):** nothing a monster type can wear or
    carry may negate an entry in its role's "Party's answer" column
    (enemies 17.1). Every armour verb, property and verb signature declares
    `negates[]` answer tags (for example, Sure-Footed and Deep Stance negate
    `shove` and `prone`, Unbowed negates `prone`, Stalwart and Immovable
    negate `shove`). The union of the issued verb with every property and
    signature the type may carry or roll must not meet its role's tags, and
    a carrier's notable rolls draw only from properties that pass. This is
    why soldiers never wear hide or splint, and brutes and skirmishers
    never wear leather.

### 8.2 The roster

Ten verbs across waves; canonical text in 13.1. M1: Supple, Sure-Footed,
Ringed. M2: Weave, Studded, Overlap, Shoulder, Stalwart, Proof. Later
(optional): Enclosed. Ringed is shared by chain shirt and chain mail on
purpose: the one allowlisted pair of validation rule 1. **(lead's call)**

## 9. Piercing, Corroded, Pitted Armour

### 9.1 Piercing (M2, with riders)

**(agreed, Q3)**

The reduction R applies to AC on that attack roll:

| Part | R |
|---|---|
| The target has any armour value (worn or natural) | 1 |
| The target `CountsAsHeavy` | +1 |
| The target is piercing-weak, whatever its armour | +1 |
| The attack has Puncture | ×2 |

- No flag and no save. **(settled, stat model 8.2)**
- **What makes an attack piercing (lead's call):** R applies when the
  attack's primary damage type (the weapon's or attack's dice type, after
  type-changing properties such as Spiked Boss) is piercing at declaration
  (weapons 7.5 step 1). An extra elemental die never makes or unmakes it.
  **Piercing-weak** means the target's effective ladder step for piercing
  is Weak at declaration, after resistance and weakness cancel (stat model
  8.3).
- **What counts as an armour value (lead's call):** worn body armour or
  `{natural: offset}`. A shield alone is not an armour value, and R never
  touches the shield part, so an unarmoured unit with a shield takes R
  only from the weak part.
- **Order (lead's call):** sum the parts, then double for Puncture, then
  apply the reduction cap against the Pitted reduction actually applied:
  `pittedApplied` = armourValueBefore − max(10, armourValueBefore −
  Pitted levels), or 0 with no armour value; effective R = clamp(R, 0,
  `armourReductionCap` − `pittedApplied`). A Pitted level that lowers
  nothing (the value is already 10) costs no piercing. R is subtracted
  from the AC on that roll only; visible AC is unchanged.
- **Preview (lead's call):** the attack preview shows the target's AC with
  the piercing part named, for example "AC 16 (−2 piercing)".
- Worked examples *(illustr.)*: medium 15 → 14; heavy 16 → 14 (equal to
  medium against piercing: the archer-floor matchup); heavy with
  Puncture 16 → 12; a piercing-weak plated brute with Puncture (1 + 1 + 1)
  × 2 = 6, capped at 4.
- **Validation:** a piercing monster's offence is computed against the
  reference hero's AC less R, the reference hero counting as medium
  armour (AR-21). The 6.4 guards run per attacker at the minimum over
  party members of (visible AC − that attacker's R), Puncture included
  where it has it (11, item 2).
- **In M1** riders do not exist yet (conditions 18.1): a piercing weakness
  is extra damage only (weak = damage + half, stat model 8.3), and no
  reduction applies. **(settled)**

### 9.2 Reduction cap

- Pitted plus piercing reduce AC by at most **4** (`armourReductionCap`).
  **(agreed, Q3)** The cap is part of the client's piercing decision and
  changes only with client approval.
- Corroded (a condition on the same-number AC slot) and Pry (which
  ignores the shield) are outside the cap.
- The simulator reports the combined swing (cap + Corroded + Pry). If it
  exceeds 25 points of hit chance in more than 5% of attacks against a
  target, the lead proposes cap 3 to the client with the swing report.
  **(lead's call for the trigger)**

### 9.3 Corroded

Unchanged. Its "+1 against heavy or armoured" reads `CountsAsHeavy`
(AR-12). **(settled; reading lead's call)**

### 9.4 Pitted Armour (Later)

**(lead's call; AR-11)**

- **Effect:** −1 per level, cap 2, on the worn or natural armour value
  only, never below 10. It never touches Dexterity, the shield or the
  unarmoured base.
- **Landing:** it does not land on a unit with no armour value; the rider
  then counts as not attempted.
- **Sources:** rust sources land only on `metal` armour (worn armour
  tagged `metal`, or natural armour whose `{natural: offset, metal: true}`
  says so), so leather, gambeson and hide are the rustproof sidegrades;
  acid ooze and Crushing land on any armour; **Blued** (Later) makes an
  armour immune.
- **Stacking:** with Corroded (a different layer), within the reduction
  cap.
- **Instance (lead's call):** Pitted is one instance with an intensity of
  1–2 (conditions 6.5 accumulating levels); a second landing raises the
  intensity.
- **Repair:** Mechanics at a short rest, `pittedRepairDifficulty` 13
  *(illustr.)*, any member, one attempt per instance per rest
  (`lastRepairRestId`). A successful repair removes the whole instance,
  both levels. **(lead's call)**
- **Lingering cap:** it counts toward the cap of 2 and follows the unit,
  so a swap cannot escape it.
- **Source placement:** floor 2 and deeper; visible from the type ("Pits
  armour"); counts toward the type's 2 conditions.

## 10. Shields

### 10.1 Sub-record and rules

**(settled in placement, weapons 11.1 and 17.9; details lead's call)**

```
shield { kind: shield|buckler|reliquary|tower,
         bash: {dice, damageType, riderFlag:false, verb} | null,
         grants: {reaction} | null, strengthClass, focus? }
```

- AC comes from `shieldAc` by kind. Training is "shields" (hard).
- A shield is bulky and never Disarmed. It is never Notched, except that
  a Reliquary Shield's focus verb can be (10.2, AR-15).
- It counts **only in the active set**, so swapping to a two-hander spare
  set gives up the shield bonus (a real defensive trade).
- **Hands:** a shield sits in `off`, so it cannot join a light + light
  pair; any one weapon + shield is legal (dagger + shield included); rod +
  shield is legal; a one-handed staff with any shield keeps its focus verb
  off.

### 10.2 Kinds

| Kind | Wave | AC | Identity and rules |
|---|---|---|---|
| **Shield** (round, heater and kite are skins) | **M1** | +2 | **Bash:** 1d4 B, Push, riderFlag false. Replaces one attack, like Shove; not an off-hand attack. Strength + proficiency to hit, Strength to damage, so it never qualifies for Exploit. The Push is verb-sourced (weapons 7.2 rule 4); Push signatures apply. Blocks the versatile die. |
| **Buckler** | M2 | +1 | No bash, no Parry. **Grip rule:** see below. It counts as holding the off hand for every other rule (staff focus off; no light + light). **For versatile wielders** (longsword, battleaxe, warhammer, war pick, spear, trident); with mace, flail, sabre or rapier the shield is better, and the card says so. |
| **Reliquary Shield** | M2 | +1 | **Notable only** (Uncommon and up; built on `item.shield`; never sold or in kits), so the mundane holy symbol is never dominated. `focus{tradition: divine, hands: off, focusVerb: fverb.spare}` plus the standard focus improvised bash, 1d4 B with no verb (weapons 21.1 rule 9); no shield bash. Rolls `props.shield_minor` only, never focus or `fverb` tables. Shield training gives the AC and the properties; the divine tradition gives Spare. This is the one exception to weapons 14's rule that an untrained focus's properties are inert (AR-44). Notched can suppress Spare, never the AC. Rod + Reliquary is legal (weapons 11.1). Counts under the `shield` family cap. |
| **Tower shield** | Later | +2 | Grants **Interpose**. Soft Strength +1 (Strained: speed −2, Interpose off). No bash. Falls back to +1 AC if it dominates the shield for Strength users. |

**Buckler grip rule (lead's call; AR-43, AR-48).** The grip is declared,
never read from the damage:

- Attack declaration (weapons 7.5 step 1) gains a grip input, `grip:
  oneHand|twoHand`. It is legal only for a versatile weapon in a set whose
  off hand is empty or holds a buckler. It defaults to `oneHand` when a
  buckler is held; the AI picks it through the scorer, the player in the
  attack preview ("two-hand grip: buckler off").
- Versatile reads: the attack uses `versatileDice` when the off hand is
  empty, or holds a buckler and the grip is `twoHand` (weapons 11.2, as
  amended).
- Declaring `twoHand` with a buckler sets the unit state
  `bucklerOffUntilOwnTurnStart` at declaration, **hit or miss**, on any
  attack, an opportunity attack or reaction attack on another unit's turn
  included. Until it clears at the start of the unit's own turn, the
  buckler gives no AC, and visible AC reads the flag.

### 10.3 Interpose (party version)

As enemies 17.1 defines it **(settled; party wording lead's call)**:

- A reaction with policy `ifItChangesOutcome`: when an adjacent ally is
  targeted by an attack roll, that ally gets +2 AC against it.
- It spends the unit's one reaction. Pry ignores it; Staggered switches it
  off.
- On a soldier, a tower shield's Interpose *is* the role's Interpose.

### 10.4 Reaction AC rule

**(lead's call; AR-8, AR-41)**

- At most **one** reaction AC bonus applies to any attack roll.
- **Candidate order:**
  1. the target's own pool-free reactions (Parry);
  2. allies' pool-free reactions (Interpose), in unit order;
  3. charge reactions: the target's own first (the Parrying property),
     then allies' (Guarding), in unit order.

  **Unit order** is the fixed unit order held in the state (stat model 6,
  initiative ties), the same order every other "unit order" rule uses.
- **Window algorithm.** For each candidate in that order:
  1. **skip** it if the attack ignores it (Pry ignores Interpose and
     Guarding; Pry Deep also ignores Parry), if its unit has no reaction
     left, or if the attack was rolled with a gear-sourced disadvantage
     (8.1 rule 6). A skipped reaction is not offered, not prompted and not
     spent;
  2. **evaluate its policy** (ability model 7.3): `always` fires;
     `ifItChangesOutcome` fires only if its bonus turns this hit into a
     miss; `whilePoolAtLeastHalf` fires while its pool is at least half;
     `never` does not fire; "ask me" prompts the player at this point,
     with the roll known;
  3. the **first that fires closes the window**. The rest are not taken
     and not spent.

  So a player who sets Parry to `always` accepts that a Parry that changes
  nothing still closes the window before an ally's Interpose; the preview
  says so.
- Graze's reaction test (weapons 7.6) already covers every AC increase in
  the window.

### 10.5 Monster bash

A monster's shield bash exists only if it is authored as one of its two
at-wills, and is then priced in the at-will band and by the forced-movement
scorer. The M1 soldier (warhammer + Shove) has none. **(lead's call;
AR-24)**

## 11. Bounds

All checked by validation (17.1). **(lead's call, confirmed to stand)**

1. **AC envelope gate** (levels 1–10, against the enemies 6.2 table attack
   at that level), stated as hit chance:
   - the maximum **sustained** party AC, with a normal roll mode, leaves
     at least a **35%** hit chance;
   - the maximum **charged** AC (+ Raise or Fortified, + one reaction),
     with a normal roll mode, leaves at least **15%**;
   - **(lead's call)** the maximum sustained AC **with the one allowed gear
     roll-mode effect** (disadvantage computed exactly) leaves at least
     **10%** *(illustr.)*. A gear roll-mode effect never combines with a
     charged bonus or a reaction AC bonus (8.1 rule 6, charged exclusion),
     so there is no fourth case.
   - Heavy + shield at level 1 is 18 against +4: 35% normal, 12.25% with
     disadvantage. The worst charged stack is 22: exactly 15%. All pass.
   - **Inputs (lead's call).** Maximum Dexterity by level comes from the
     stat model's best legal array plus both increases: +3 at levels 1–3,
     +4 at 4–7, +5 at 8–10 (stat model 5.1). The maxima include every
     class defence feature (an unarmoured defence or a ward) and every
     training reachable under each class's `armourTrainingCeiling` and
     `shieldTraining` (7.2), shield training included.
2. **Low end:**
   - the enemies 6.4 guards are evaluated at the reference hero **and** at
     the lowest legal party AC; failure fails the build. **Lowest legal AC
     (lead's call):** for each class and companion, the best sustained AC
     it can reach with its trainings and starting kit at the worst
     Dexterity of its suggested array; then the minimum over classes and
     companions. A member who chooses to take armour off is not the
     bound. From M2 each guard runs per attacker at the minimum over party
     members of (that visible AC − the attacker's piercing R), Puncture
     included where it has it (9.1);
   - **binding requirement on class designs:** every class with an armour
     ceiling below light ships a defensive feature (an unarmoured defence
     or a ward) that keeps its sustained AC no more than 3 below the
     reference at levels 1, 5 and 10, with simulator evidence, the same
     way weapons 6.4 binds precision features.
3. At most one reaction AC bonus per attack (10.4).
4. At most one gear roll-mode effect per attack (8.1 rule 6).
5. At most 2 gear crit downgrades per unit per fight (8.1 rule 7).
6. A reduction cap of 4 (9.2).
7. At most one reaction-granting property per unit across armour, shield
   and trinkets (5.5).
8. No trinket or piece AC, of any effect kind (5.5).
9. Raise and Fortified do not stack (the same-number rule extended to this
   gear charge; 6.1 gives the expression; AR-14).

## 12. Monster use

### 12.1 Natural armour

**(lead's call; AR-17)**

- The `armour` field takes `{natural: offset, metal?: bool}` (`metal`
  defaults to false; a trait such as a plated carapace sets it true, for
  the rust landing rule in 9.4). Its value is the medium band value + the
  authored offset (−3 to +2), so it shifts with the band.
- Medium Dexterity cap; counts as medium (except `CountsAsHeavy` for
  Plated and `armoured`); no verb.
- Armed types whose issue armour is display-only (furs, hides) may use it.

### 12.2 Level shift

AC is recomputed from the loadout. Validation fails any legal shift
outside the band; the author narrows `levelShift` or changes the loadout
(F4, AR-22).

- **Dexterity slot (lead's call).** Each type declares which spread slot
  its Dexterity occupies (`main`, `secondary` or `other` with an authored
  value), the data enemies 5.1 and 5.3 already need for the spread table.
  Validation rule 4 recomputes Dexterity from that slot at every shifted
  level; an `other` Dexterity stays as authored.
- A loadout never changes with level. A type that needs different armour
  at some level is a separately authored type, or narrows its
  `levelShift`.

### 12.3 Verified M1 loadouts

Band = table AC + role step ±1. M1 has no level shift (enemies 5.1), so
the legal levels below are the levels a type may be authored at; from M2
they are its native level plus `levelShift`. **(lead's call)**

| Unit | Loadout | Dexterity slot | AC by level | Verb |
|---|---|---|---|---|
| Claw brute | Natural, offset −2 | `other` 0 | 11 at L0–4 (band 9–11 / 10–12 / 11–13): in band | — |
| Greatclub brute (alt) | Natural (furs), offset −2 | `other` 0 | 11 at L0–4: in band | — (not leather: Supple would negate "opportunity-attack zones") |
| Warhammer-and-shield soldier | Chain shirt + shield | `other` +1 | 16 at L1–4 (bands 14–16 at L1–2, 15–17 at L3–4): in band; legal at L1–4 only (16 is above the L0 band 13–15) | Ringed (left off the M1 gear line: no Bleed riders yet, 6.4) |
| Recruit soldier (M2; a separate L0 type, not one of M1's three) | Leather + shield | `other` +1 | 15 at L0 (13–15): in band | Supple |
| Pike soldier (room level 2+) | Chain mail, Strength +2 (main) | — (heavy ignores it) | 16 at L1–4: in band; legal at L1–4 only; authored speed 6, derived 5 (Weighty) | Ringed (left off the M1 gear line) |
| Sling artillery | Unarmoured | `main` (+2 at L0–2, +3 at L3–4) | 12 at L0–2, 13 at L3–4 (bands 10–12, 11–13, 12–14): in band | — |
| Minions | Leather | `other` +1 | 13 at L0–2: in band | Supple, inert (left off the gear line) |

### 12.4 M2 patterns (checked)

- raider: gambeson + shield, Dexterity `secondary`: 15 at L0–2, 16 at
  L3–4, in band over the full shift [−2, +2] (F1);
- skirmishers: gambeson at every level, Dexterity `main` (14 / 15 / 16 /
  17);
- leaders: hide at L1–8;
- brutes: chain shirt from L4;
- soldiers: medium + shield to L8, heavy + shield from L7. **Piercing as
  the soldier's answer (lead's call):** medium + shield soldiers take only
  the R 1 every armoured unit takes, so at least half of the M2 soldier
  types on any floor whose roster includes a soldier are reach-weapon
  soldiers in heavy armour without a shield (the pike pattern, 16 at L1–4,
  `CountsAsHeavy`, R 2), and shield soldiers carry the acid (Corroded)
  answer. The enemies 17.1 answer "piercing or acid" is unchanged;
- artillery: unarmoured to about L6, light from L5 with Dexterity +3;
- lurkers: light from L1, Dexterity `main` (F10); never hide.

### 12.5 AI

**(lead's call; AR-25)**

- The scorer evaluates every candidate through the same resolve preview
  the player sees, with armour verbs applied, including whether Ringed and
  Proof are already spent. A monster does not Shove a Sure-Footed target
  or fish for crits on unspent Proof without pricing it.
- "Lowest AC" targeting (Kiter, Raider) reads **visible AC**.
- `verbDefencePermille` gives provisional prices; from M2 the simulator
  replaces them with measured trigger rates per floor theme.

### 12.6 Carriers

**(lead's call; AR-26)**

- A notable armour goes only to a carrier whose **worn** issue armour has
  the same `armourCategory`; natural armour never matches, even though it
  counts as medium. A notable shield goes only to a carrier with the same
  shield kind.
- Under banding the carrier's AC is unchanged (validation rule 6 checks
  that the carried notable's derived AC equals the issue loadout's at
  every legal shift); the verb and properties apply, benefit only, inside
  the threat band.
- Properties a carrier may roll pass the role-answer rule (8.1 rule 10).
- Reaction properties must fit the enemies 9.1 caps.
- This legality is enemies 15.3 step 4's "Legal means…" test (AR-26);
  the agreed 15.4 table is unchanged.

### 12.7 Plated

"Counts as heavy" applies to category-reading rules only (F3, AR-27).

## 13. Catalogue

How to read it: AC is b1 / b2 / b3. "Costs": St = Stiff, Cl = Clatter,
W = Weighty. Every verb is passive. "metal" means rust can pit it (Later).
Names marked SRD are subject to the SRD check (section 15). **(lead's
call)**

### 13.1 Body armour

| Family | Category | Wave | AC | Dexterity | Strength | Costs | Verb (canonical text) | Tags |
|---|---|---|---|---|---|---|---|---|
| Unarmoured (robes are a skin) | — | M1 | 10 + Dexterity | Full | — | — | None; class features hook here | — |
| **Leather** | L | **M1** | 12 + Dexterity | Full | — | — | **Supple:** opportunity attacks against you have disadvantage. | — |
| Gambeson | L | M2 | 12 + Dexterity | Full | — | — | **Weave:** if you moved at least 3 squares on your last own turn (the weapons 7.6 movement count), the first ranged weapon attack against you each round has disadvantage, unless you are Prone or Grappled. | — |
| Brigandine (studded leather skin) | L | M2 | 12 + Dexterity | Full | — | — | **Studded:** grapple attempts against you have disadvantage. | metal |
| **Hide** | M | **M1** | 13/14/15 + Dexterity | +2 | — | St | **Sure-Footed:** Shove attempts against you have disadvantage, and you have advantage on saves against falling Prone. | — |
| **Chain shirt** | M | **M1** | 13/14/15 + Dexterity | +2 | — | St | **Ringed:** the first time each fight that Bleeding would land on you, it does not. | metal |
| Scale | M | M2 | 13/14/15 + Dexterity | +2 | — | St | **Overlap:** once per round, the first attack against you with advantage is rolled normally. | metal |
| Breastplate | M | M2 | 13/14/15 + Dexterity | +2 | — | St | **Shoulder:** your Shove attempts have advantage. | metal |
| Half plate | M | Later (optional) | 13/14/15 + Dexterity | +2 | — | St | **Enclosed:** advantage on Reflex saves against area effects. | metal |
| **Chain mail** | H | **M1** | 16/17/18 | None | +1 | St, Cl, W | **Ringed** (shared on purpose) | metal |
| Splint (lamellar) | H | M2 | 16/17/18 | None | +2 | St, Cl, W | **Stalwart:** forced movement against you is 1 square shorter (after the 3-square sum), and you cannot be shifted. | metal |
| Plate | H | M2 | 16/17/18 | None | +2 | St, Cl, W | **Proof:** the first critical hit against you each fight is a normal hit instead. | metal |
| Natural (monsters only) | counts as M | M1 | Medium band + offset | +2 | — | — | None (traits carry the identity) | metal only with `metal: true` (12.1) |

### 13.2 Identity

Best at / worst at / monster use:

- **Leather.** Best: Dexterity skirmishers moving through zones;
  rustproof. Worst: standing fights; companions without ability
  increases. Monsters: minions (inert); never brutes or skirmishers
  (role-answer rule).
- **Gambeson.** Best: kiters crossing open ground on artillery floors;
  rustproof. Worst: holding a chokepoint; Prone and Grappled shut it off.
  Monsters: skirmishers and raiders.
- **Brigandine.** Best: mobile fighters against grabbers. Worst: rust
  floors, fights with no grappler. Monsters: duellists and leaders, never
  skirmishers (the Grapple answer).
- **Hide.** Best: frontliners against Shove, Topple and trips; the
  rustproof medium. Worst: Bleeding claws; Dexterity +3 or more.
  Monsters: leaders and controllers; never soldiers, brutes or lurkers.
- **Chain shirt.** Best: claw and blade floors. Worst: Prone-heavy enemies,
  rust. Monsters: soldiers (with a shield), brutes from L4. Teaches that a
  Bleed can fail, from M2.
- **Scale.** Best: the flanked frontliner. Worst: lone attackers, rust.
  Monsters: elite soldiers and leaders; forces the party to stack
  advantage.
- **Breastplate.** Best: active control with the shield bash and ledges
  (priced through its control value). Worst: pure defence. Monsters:
  brutes and soldiers on terrain floors.
- **Half plate.** Ships only if medium wearers lack an anti-area option;
  otherwise a breastplate skin.
- **Chain mail.** Best: the Strength tank (16, or 18 with a shield). Worst:
  stealth, piercing, rust, speed. Monsters: pike soldiers, heavy soldiers.
- **Splint.** Best: holding a chokepoint against Push, Hook and shift
  teams. Worst: stealth, Strength +1 builds. Monsters: leaders and bosses
  only (it would negate the soldier's "Shove it").
- **Plate.** Best: crit insurance under permadeath. Worst: the war pick
  with Puncture, Corroded, rust, Strength +1 builds. Adamant cannot roll
  on it. Monsters: elite knights, boss guards.

### 13.3 Shields

| Kind | Wave | AC | Strength | Bash | Identity | Best at / worst at | Monsters |
|---|---|---|---|---|---|---|---|
| **Shield** | **M1** | +2 | — | 1d4 B, Push | The pusher's wall | Best: the highest one-handed AC, doorway Push. Worst: damage (−35% against a two-hander, weapons 6.4), the versatile die, Pry. | M1 soldier (tall-shield skin), raider |
| Buckler | M2 | +1 (grip rule) | — | — | The duellist's hand-guard | Best: versatile wielders declaring the two-hand grip or +1 AC on each attack. Worst: control, maximum AC, non-versatile weapons. | Leader (longsword Parry + buckler) |
| Reliquary Shield | M2, notable only | +1 | — | — (1d4 B improvised focus bash) | The priest's shield and holy symbol (Spare) | Best: divine front-liners (mace + Reliquary). Worst: focus depth, control. | Cult priests; carried only by same-kind carriers |
| Tower shield | Later | +2 | +1 soft | — | The guardian's wall: Interpose | Best: guarding a caster in a corridor. Worst: alone, against Pry, Staggered, low Strength; no opportunity attack that round. | Soldier signature |

### 13.4 Worn pieces (trinket kinds; M2; never AC)

**(agreed, Q1)**

| Kind | `wornOn` | Domain | Visual |
|---|---|---|---|
| Helm, hood, circlet | head | Senses, mind, crit riders | Rigid socket |
| Gloves, gauntlets, bracers | hands | Grapple, handling, Administer | Tint and icon |
| Boots | feet | Movement, Prone, hazards | Tint and icon |
| Cloak | back | Elements, stealth | 2–3 bone chain |
| Amulet / ring / charm | neck / finger (2) / none | Neck: life and the body · Finger: your strike · None: stored moments, capacity | Icon |

Examples are in 5.6.

### 13.5 Merged and cut

| Cut or merged | Why, and where it went |
|---|---|
| Padded | SRD-dominated at 11; lives on as gambeson |
| Ring mail | Dominated |
| Studded leather as its own step | A brigandine skin |
| Half plate | Later and optional; otherwise a breastplate skin |
| Vestments (Composed) | Copied the staff's Steady Casting (weapons 18.1); its signature Serene was dead (Prone never ends Concentration); "Composed" is an enemies term. Robes are an unarmoured skin. |
| Turning (+2 AC reaction on armour) | A strictly better Parry; drove the 24-AC stack |
| Cushioned | Reduced damage, against agreed stat model 6 |
| Lined (armour), Steadfast (armour) | Moved to the back and head tables as Weathered and Unshaken |
| Windcloak | Copied Weave |
| Swapper's, Thrower's and Sure Grip gloves | Copied Quickdraw, Far-flight and Sure Grip |
| Punch Buckler | Widened the buckler's free value |
| Lookout Rim | No buff-test tag fits; cut against surprise |
| Spiked shield | The Spiked Boss property |
| Round, heater and kite shields | Skins |
| Pavise | Needs a cover rule |
| Parrying shield, main-gauche | Parry sources are capped |
| Lantern and hook shields | No supporting systems |

### 13.6 M1 selection and starting kits

M1 is one class, levels 1–3, one floor, three enemy types: the
warhammer-and-shield soldier, the sling artillery and the claw brute.

- **M1 families:** leather, hide, chain shirt, chain mail, shield,
  unarmoured, natural.
- **M1 verbs:** Supple, Sure-Footed, Ringed. Supple fires against soldier
  and minion zones; Sure-Footed against the brute's Shove; Ringed against
  the claw brute's Bleeding.
- **Monster-side teaching** of armour verbs starts in M2, once riders
  exist. Weapon verbs carry M1's monster teaching.
- **Starting kits *(illustr.)*:**
  - heavy-trained class: chain mail, with longsword + shield in one set
    and a two-hander or versatile weapon in the other (AC 18 or 16; this
    teaches the active-set rule);
  - medium-trained class: chain shirt or hide;
  - light-trained class: leather;
  - classes with no training: robes (unarmoured) plus their class defence
    feature.
- **Slice validation:** the M1 simulator's floor-1 friction targets and the
  6.4 guards run at the slice kit's actual AC and at its lowest legal AC.
- **Vendor:** sells every family of the current wave from camp 1 (banding
  removes depth gates).

## 14. Notable properties

### 14.1 Rules

**(lead's call)**

- They follow the buff test. Bands scale charges and charge dice only.
- At most 3 properties per item; value equals the tier budget.
- Verb signatures are off whenever the verb is off.
- No property gives always-on AC (validation rule 10).
- **Gear-prevention step (lead's call; AR-51).** Gear that stops a
  condition from landing (Ringed and Double Ring against Bleeding, Unbowed
  against Prone) acts at a new step of the conditions 9.2 pipeline,
  **between step 3 (contest) and step 4 (ward)**: after a failed contest or
  an automatic landing, before a Warded charge is spent. So neither the
  ward nor the gear use is spent when the save succeeds, and a ward is
  never spent on a condition the gear then stops. It emits
  `ConditionResisted{reason: ArmourVerb}` and `ArmourVerbFired`. Trinket
  properties also act at this step and emit `ConditionResisted{reason:
  TrinketProperty}` and `TrinketPropertyFired` (trinkets design 7.3). A charge
  prevention has a data policy, `always` by default, which the player may
  change to "ask me" like a reaction (ability model 7.3); `fight` uses
  always fire.

### 14.2 `props.armour_minor` (value 1)

| Property | Tags | Eligible | Effect | Wave |
|---|---|---|---|---|
| Fitted | prevent | heavy | No Strength threshold (mithral-shaped, adapted). For companions and off-builds; a Strength-main wearer gains nothing from it (19) | M1 |
| Forewarned | prevent (`fight` window, minor) | any | Until your first turn of each fight, attack rolls against you have disadvantage (roll-mode cap) | M1 |
| Unbowed | prevent, charge | not hide | 1 / 1 / 2 per rest, policy `always` (the player may set "ask me"): when Prone would land on you, it does not (the gear-prevention step, 14.1) | M1 |
| Shadowed | economy (`fight` pool, minor) | light | Once per fight, Hide as a bonus action | M1 |
| Adamant | prevent, charge | not plate | 1 / 1 / 2 per rest, policy `always`: a critical hit against you becomes a normal hit (adamantine-shaped; crit cap and order, 8.1 rule 7) | M1 |
| Warding Lining | prevent (`fight` pool, minor) | any | At combat start, gain Warded (1 charge) | M2 |
| Blued | prevent | metal | Pitted Armour cannot land on this armour | Later |

### 14.3 `props.armour_major` (value 2)

| Property | Tags | Effect | Wave |
|---|---|---|---|
| Muffled | prevent | Heavy only: no Clatter (passive or checks). Never on the same item as Fitted. | M1 |
| Resistant (element) | type | Fire, cold, lightning, acid, radiant or necrotic; never physical or psychic. On M1 notables only if the floor's theme deals that element. | M1 |
| Reprisal (element) | charge | 1 / 1 / 2 per rest, reaction when a melee attack hits you: the attacker takes 1d6 / 2d6 / 3d6 of the element (counts under the reaction cap) | M2 |
| Last Ward | charge (`camp`) | 1 per camp: a hit that would drop you to 0 HP misses instead; Rare and above | Later (depends on death and healing) |

**Last Ward (Later; gated on the death and healing design; change
CN-23):** owner and order with the talisman: death and healing design;
recommended narrowed (for example once per run) (consumables design
14.3, 14.9).

### 14.4 Armour verb signatures `props.averb.<verb>` (value 1)

| Verb | Signature |
|---|---|
| Supple | **Ghost Step:** Set attacks against you also have disadvantage |
| Weave | **Far Weave:** needs 2 squares |
| Studded | **Barbed Studs:** you also have advantage on checks to escape a grapple |
| Sure-Footed | **Deep Stance:** a Shove can never push you |
| Ringed | **Double Ring:** twice per fight (b3: three times) |
| Overlap | **Tight Overlap:** the first two such attacks each round |
| Shoulder | **Bull Rush:** once per turn, a Shove that pushes moves the target 2 squares (verb-sourced under weapons 7.2 rule 4 as amended by AR-49, so the verb-sourced once-per-round rule and the 3-square cap apply) |
| Stalwart | **Immovable:** 2 squares shorter (b3: verb-sourced movement cannot move you) |
| Proof | **Twice-Proof:** the first two critical hits each fight (inside the crit cap) |
| Enclosed | **Sealed:** Poisoned never lingers on you (blocks the lingering form; trinkets design F2) (Later) |

### 14.5 `props.shield_minor` (value 1)

| Property | Tags | Eligible | Effect | Wave |
|---|---|---|---|---|
| Spiked Boss | type | shield | The bash deals piercing (never with Quickdraw on one item; a set holding both suppresses Quickdraw, `QuickdrawTypeConflict`) | M1 |
| Raise | charge, prevent | any | 1 / 1 / 2 per rest, bonus action: +2 AC until the start of your next turn (`raiseActiveUntilOwnTurnStart`, 16.3; never a condition); part of the shield part (Pry ignores it); does not stack with Fortified (6.1) | M1 |
| Hailguard | charge | shield, tower | 1 / 1 / 2 per rest, automatic at weapons 7.5 step 2 (no reaction; policy `always` with the target filter `nonMinion`, which the player may change like a reaction policy, ability model 7.3): a ranged weapon attack roll against you whose net mode would not already be disadvantage has disadvantage. Inside the roll-mode cap, where it has the lowest priority and is not spent when suppressed (8.1 rule 6) | M1 |
| Guarding | charge | shield, buckler, reliquary | 1 / 1 / 2 per rest: Interpose (illegal on a tower shield or on a carrier that already has Interpose or a Guard verb) | M2 |
| Bashing Rim | rider | shield | The bash carries the Stagger rider (settled source) | M2 |
| Shelter | charge | shield, tower | 1 / 1 / 2 per rest, reaction: a ranged weapon attack against an adjacent ally targets you instead (arrow-catching-shaped, renamed). It resolves at weapons 7.5 step 1, after declaration and before step 2, so the new target's roll mode, AC and reaction window apply; it spends your reaction and counts under the reaction cap (5.5) | M2 |
| Bulwark Stance | tower signature | tower | If you did not move on your last own turn, Interpose may instead give an adjacent ally advantage on one Reflex save against an area | Later |

A shield's bash carries the verb Push, so a shield may also roll
`props.verb.push` (Heavy Push, Driving; weapons 18.4), and Quickdraw's
eligibility in `props.weapon_minor` (weapons 18.2) is widened to shields,
since it acts on the set (AR-50). They stay in their own tables (5.5).
**(lead's call)**

### 14.6 M1 notables

Rider-free; M1 conditions and families only. *(illustr.)*

| Notable | Base | Rarity | Properties |
|---|---|---|---|
| Duskleather Jerkin | leather | Common | Shadowed |
| Tumbler's Leathers | leather | Uncommon | Ghost Step, Forewarned |
| Boar-hide Coat | hide | Common | Deep Stance |
| Ironbark Hide | hide | Uncommon | Adamant, Forewarned |
| Ringwarden's Shirt | chain shirt | Uncommon | Double Ring, Unbowed |
| Kneeguard Mail | chain mail | Common | Unbowed |
| Hushmail Hauberk | chain mail | Uncommon | Muffled (major) |
| Gatewarden's Shield | shield | Common | Heavy Push |
| Pass-Keeper's Shield | shield | Common | Raise |
| Hailward Shield | shield | Uncommon | Hailguard, Spiked Boss |
| **Bellwarden's Mail** (floor-1 boss signature) | chain mail | Floor-1 top tier (budget 2) + one authored property | Adamant, Forewarned, + authored Unbowed |

**M2 examples:** Gale Gambeson (gambeson, Uncommon): Far Weave,
Forewarned. Knight's Plate (plate, Rare): Twice-Proof, Muffled. Pilgrim's
Reliquary (Reliquary Shield, Uncommon): Raise, Guarding.

## 15. Licensing

**(lead's call; mirrors weapons 19; AR-45)**

- **`srd` with `srdSource` (the SRD 5.2.1 armour table)** on the names
  leather, hide, chain shirt, scale, breastplate, half plate, chain mail,
  splint, plate and shield.
- **`adapted`, with the reason,** for: values banded by level; Strength 13
  and 15 mapped to +1 and +2, and soft; stealth disadvantage dropped for
  Clatter; padded, ring mail and studded leather cut or folded.
- **Magic-item mechanisms cite SRD 5.1/5.2:** mithral (Fitted), adamantine
  (Adamant), arrow-catching (renamed Shelter), the rust monster (as a
  Pitted source).
- **Original:** gambeson, brigandine, buckler, Reliquary Shield, tower
  shield and every armour verb. Ideas inspired by PF2e, DCSS, Lancer and
  others are labelled original and carry no names from those games.
- **Credits** keep the verbatim SRD 5.2 and SRD 5.1 attributions (main
  design 2) and confirm they cover the armour table and magic items.
- **Nothing is written to data before the SRD check** in the header.

## 16. Data and the core

**(lead's call)**

### 16.1 Data shape *(illustr.)*

- **Armour family:** `{category: armour, armourCategory:
  light|medium|heavy, training, wave, tags[metal], strengthClass:
  none|heavy|heavyPlus, verb, defaultMonsterProfile{effects:[averb.*],
  passive}}`.
- **Item sub-record:** `armour{armourCategory, verb}`, which must equal
  the family's. `armourValue`, the Dexterity mode, the threshold, Clatter,
  Weighty and Stiff are derived from `armour.json`; validation forbids
  writing them (F6).
- **Shield family:** `category: shield`, `training: shields`, `shield{}`
  as in 10.1.
- **Trinket sub-record:** `trinket{kind, wornOn}`, `wornOn` checked
  against `kindToLocation` (5.4), plus `display.socket: head|back|none`,
  which the rules never read.
- **Verb records `averb.*`:** `text, wave, oncePer (fight|round|turn|none),
  conditionsUsed[], effectKinds[], negates[], previewHint,
  isReaction:false, hook`.
- **Property records** (`props.armour_*`, `props.averb.*`,
  `props.shield_minor`, piece tables) also declare `negates[]` and `hook`
  (8.1 rule 10; validation rule 14).
- **Unit training:** `armourTraining: none|light|medium|heavy` and
  `shields: bool`; class data adds `armourTrainingCeiling` and
  `shieldTraining: allowed|never` (7.2).
- **Monster types:** `{natural: offset, metal?}` (12.1) and the Dexterity
  spread slot (12.2).

### 16.2 Tuning file

```json
{ "unarmouredBase": 10,
  "valueByCategory": { "light": [12,12,12], "medium": [13,14,15], "heavy": [16,17,18] },
  "dexMode": { "unarmoured": "full", "light": "full", "medium": "capped", "heavy": "none" },
  "mediumDexCap": 2,
  "strengthByClass": { "none": null, "heavy": 1, "heavyPlus": 2 },
  "heavySpeedPenalty": 1, "strainedSpeedPenalty": 2, "heavyStealthPenalty": 5,
  "stiffCategories": ["medium", "heavy"],
  "shieldAc": { "shield": 2, "buckler": 1, "reliquary": 1, "tower": 2 },
  "piercing": { "vsArmour": 1, "vsHeavyExtra": 1, "weakExtra": 1, "punctureMultiplier": 2 },
  "armourReductionCap": 4, "pittedCap": 2, "pittedRepairDifficulty": 13,
  "critDowngradeCapPerFight": 2, "reactionAcBonusesPerAttack": 1, "gearRollModeEffectsPerAttack": 1,
  "acEnvelope": { "sustainedMinHitPermille": 350, "chargedMinHitPermille": 150,
      "rollModeMinHitPermille": 100 },
  "sharedVerbAllowlist": [ ["armour.chain_shirt", "armour.chain_mail", "averb.ringed"] ],
  "validation": { "verbDefencePermille": { "averb.supple": 20, "averb.weave": 25, "averb.studded": 15,
      "averb.sure_footed": 15, "averb.ringed": 10, "averb.overlap": 30, "averb.stalwart": 10,
      "averb.proof": 25, "averb.enclosed": 20 }, "controlValue": { "averb.shoulder": "scorer" } } }
```

All values *(illustr.)*, except `valueByCategory`, `mediumDexCap`, the
`piercing` block and `armourReductionCap`, which are the client's
decisions (Q2, Q3) and need client approval to change. A save records the
file's hash, as `inventory.json` does. **(lead's call)**

### 16.3 State, queries and events

**(lead's call)**

- **No new item state.** Charges on items use the existing charge state.
- **Round (lead's call):** for every rule in this design, a round is the
  initiative round, which starts when the top of the initiative order
  acts; every round-scoped counter below resets then.
- **Unit state**, with its reset point:

| Field | Type | Resets | For |
|---|---|---|---|
| `squaresMovedLastOwnTurn` | integer | Snapshot at the end of the unit's own turn | Weave, Far Weave |
| `raiseActiveUntilOwnTurnStart` | bool | Start of the unit's own turn | Raise |
| `bucklerOffUntilOwnTurnStart` | bool | Start of the unit's own turn | Buckler grip rule (10.2) |
| `weaveSpent` | bool | Round start | Weave |
| `overlapCount` | integer 0–2 | Round start | Overlap, Tight Overlap |
| `bullRushSpent` | bool | Start of the unit's own turn | Bull Rush |
| `critDowngradesThisFight` | integer 0–2 | Fight start | Crit cap (8.1 rule 7) |
| `fightUses` | integer per property or verb (Ringed 1, Double Ring 2, or 3 at b3; Proof 1, Twice-Proof 2; Shadowed, Warding Lining 1; trinket `fight` properties Visor's unseen branch, Runner, Oilskin, Duskhem, Lifeline, Aid and Tally 1, keyed by (unit, property id); trinkets design 17.3) | Fight start | `fight` uses |
| `forewarnedOpen` | bool | Set at fight start, cleared at the unit's first own turn | Forewarned |
| `trinketPreventionReceivedThisRound` | bool | Round start | One received trinket prevention per round (trinkets design 7.3) |
| `trinketHelpedThisRound` | bool | Round start | One trinket Helped per round (trinkets design 7.1) |
| `trinketWardedThisFight` | bool | Fight start | One trinket Warded per fight (trinkets design 7.1) |

- Queries: `AC(unit, attack)`, `VisibleAC(unit)`, `PiercingReduction(attack,
  target)`, `CountsAsHeavy(unit)`, `heavySpeedPenalty(unit)`,
  `IsStrained(unit)`, `ActiveReactionProperty(unit)`, `CanWear(unit, item,
  slot)`. All derived, never stored.
- **`CanWear` priority:** it returns one verdict, checked in this order:
  `Untrained`, then `LocationOccupied`, then the items design's other
  reasons. `StrengthThreshold` is a warning verdict ("Strained: needs Str
  +1"), never a refusal. `ReactionCap` is not a `CanWear` reason; it greys
  the suppressed property after the equip (5.5).
- Events: `ArmourVerbFired{verb, unit, result}`;
  `ConditionResisted{reason: ArmourVerb}` (14.1);
  `TrinketPropertyFired{property, instanceId, unit, result}` (trinkets
  design 17.3).

### 16.4 Suggested recipient

(items 6.3; AR-32)

- Armour and shield gain = AC change × 10 + value difference, −10 if the
  item would newly make the recipient the party's worst stealth member.
- Strained recipients rank below every usable, unstrained recipient.
- For pieces, a free matching location comes first.

## 17. Validation, testing and simulator

### 17.1 Content validation

The build fails when **(lead's call)**:

1. An armour family other than unarmoured or natural has other than one
   verb, or two armour families share a verb, in any categories, except a
   pair in `sharedVerbAllowlist` in `armour.json` (only chain shirt and
   chain mail for Ringed).
2. A verb uses a condition from a later wave than its own.
3. A record writes an armour value, Dexterity cap, threshold, stealth,
   speed or Stiff.
4. An issue loadout's formula AC leaves its role band at any legal level
   shift, with Dexterity recomputed from the type's declared spread slot
   (12.2); a type lacks that slot; or its derived speed (authored speed
   after Weighty) leaves 3–8.
5. The union of a type's issued armour verb and every property and
   signature it may carry or roll has a `negates[]` tag that meets its
   role's "Party's answer" tags (8.1 rule 10).
6. A carried armour or shield notable sits on a carrier of a different
   worn `armourCategory` (natural armour never matches) or shield kind;
   its derived AC differs from the issue loadout's at any legal shift; or
   it breaks the enemies 9.1 caps.
7. A Reliquary Shield has a mundane, vendor or kit channel; rolls focus or
   `fverb` tables; or lacks a tradition or improvised bash.
8. A minion's armour verb is not inert.
9. A monster-type Pitted source is on floor 1, is not visible from its
   type, or is a rust source that lands on non-metal armour (natural
   armour reads `metal`, 12.1). Party sources (Crushing) are outside this
   rule.
10. Armour or shields are not bulky; any property gives always-on AC; or
    any trinket or piece property declares an AC effect kind.
11. One table, one property: a property id appears in two tables, or two
    records in any gear tables declare the same effect kinds and trigger
    (always-on copies; `charge` versions allowed).
12. An `averb.*` has no validation price.
13. A `prop.*` or `averb.*` display name matches a condition, affix,
    awareness state, behaviour profile, universal action, verb or
    property.
14. A property or signature does not bind a declared `hook` to an engine
    event or query, or has no property test showing an outcome change (a
    dead property). A simulator trigger rate of zero (17.3) is reported as
    a failure.
15. The AC envelope (11, item 1) fails at any level, or a 6.4 guard fails
    at the lowest legal party AC (11, item 2).
16. Class data: an armour training offer exceeds the class ceiling, or a
    shield training offer reaches a class whose `shieldTraining` is
    `never`.
17. Piece rules: not exactly one fixed location verb; `wornOn` differs
    from `kindToLocation` for its `kind`; fewer than 3 legal families
    available for any piece location (head, hands, feet, back) in any wave
    from M2, counted as a running total; a piece above Uncommon.
18. A record flagged `srd` lacks `srdSource`, or an adapted record lacks
    `adapted`.
19. An issued monster loadout is Strained at any legal level shift.

### 17.2 Tests

**(lead's call)**

- **AC query:** every category at every band, negative Dexterity in
  medium and heavy, shield in active versus spare set, the Raise and
  Fortified expression with and without Pry (Fortified still applies
  against Pry), Plated's `traits` term (never pitted or pierced), the
  buckler-off flag, visible AC against rolled AC.
- **Buckler grip:** `twoHand` declared on a miss still switches the
  buckler off; an opportunity attack on another unit's turn sets the flag;
  the flag clears at the unit's own turn start.
- **Piercing:** each part and the Puncture order; primary type after
  Spiked Boss; effective Weak after cancellation; shield-only targets;
  natural armour; the cap with 0, 1 and 2 Pitted levels, including a
  Pitted level that lowers nothing (value already 10); R never changes
  visible AC.
- **Reaction AC rule:** the window algorithm under each policy (`always`,
  `ifItChangesOutcome`, `whilePoolAtLeastHalf`, `never`, "ask me");
  skipped reactions (Pry against Interpose, Pry Deep against Parry, no
  reaction left) not offered; untaken reactions not spent; unit order;
  within step 3 the target's own charges before allies'; property tests
  that the preview and the resolve agree.
- **Roll-mode cap:** each priority tier against the others; slot-order
  ties; suppressed effects not spent (Weave, Overlap, Hailguard charges);
  Overlap only on net advantage; the charged exclusion (Raise or
  Fortified; no reaction AC bonus after gear disadvantage); Hailguard
  fixed at step 2.
- **Crit cap:** Fortified first and free; Proof before Adamant; no
  Adamant charge spent at the cap; the third gear downgrade refused; a
  downgraded natural 20 still hits with no rider or doubling.
- **Gear prevention:** Ringed and Unbowed after a failed save and before
  the ward; no use or ward spent on a successful save.
- **Requirements:** Strained derivation (active set only; no gear or
  condition Strength) and its one-penalty rule; `CanWear` priority
  (`Untrained`, then `LocationOccupied`); Strained as a warning; reaction
  cap suppression by slot order.
- **Location rule:** two rings legal, two helms refused, replacing into
  the occupied slot legal, `none` count.
- **Replay determinism:** a recorded fight with armour verbs and piercing
  replays bit-identically (main design core rule).

### 17.3 Simulator reports

| Report | Target *(provisional)* |
|---|---|
| AC by member against the reference, by level | Inside the envelope |
| 6.4 guards at the party's lowest AC and at the slice kit's AC | Pass (release gate) |
| Pick rate and death rate by armour category and shield kind | No monoculture: heavy + shield ≤ 50% of Strength-main members; buckler not the default for versatile users |
| Pick rate by heavy family | No heavy family above 50% of heavy wearers. If plate exceeds it, Proof's per-fight use is the first thing throttled (to a per-rest charge) |
| Verb trigger rate by family, with party and monster rows | No dead verbs (Studded and Enclosed are most at risk) |
| Families never worn after floor 2 | None |
| Party notice rate by armour mix | One heavy member visibly costs stealth |
| Strained frequency | Rare |
| Piercing hit gain by target category; combined reduction swing | Swing ≤ 25 points in ≥ 95% of attacks |
| How often the reaction rule and the roll-mode cap bind | Reported |
| Bash use rate | Low but non-zero |
| Trinket picks: pieces against jewellery, split by free slot and both slots full; `LocationOccupied` rate; near-tie trinkets left at camp; worn-item slots empty at the boss | Feeds the E+ gate (5.10); empty slots ≤ 25% at the boss |
| Piece power: a piece's gain against same-rarity jewellery | Within 1 value unit *(illustr.)*; a miss goes to trinket tuning, never to E+ |
| Piece property trigger rate | Fires or prevents at least once per 3 fights for its wearer, or the family is flagged for redesign. A family cut instead is replaced in the same location before the build ships, so validation rule 17 always holds |
| Acrobatics checks per floor | At least the 7.3 requirement (3 Acrobatics-gated choices per floor); a miss after content tuning triggers the named Stiff fallback (7.3) |
| Pitted and Corroded co-occurrence (Later) | Reported |
| The trinket rows of trinkets design 18.3, including the condition landing envelope and its hero row (release gate; in the E+ veto) | As trinkets design 18.3 |

## 18. Staging

**(lead's call)**

- **M1:** the AC query, bands, Strained, hard training with ceilings,
  Clatter, Weighty, Stiff, `CountsAsHeavy`; leather, hide, chain shirt,
  chain mail; the shield with its bash; Supple, Sure-Footed, Ringed; the
  M1 properties and notables (14.6); starting kits; natural armour
  offsets; loadout-band and role-answer validation; the AC envelope gate;
  the tall-shield skin; the display-only helm; the "AC +1: armour band"
  line (relevant from level 5, so first seen in M2).
- **Also M1 (lead's call),** because M1 already holds what they bound: the
  gear roll-mode cap with its priority and charged exclusion (8.1 rule 6,
  AR-40), since Forewarned and Hailguard both ship in M1; the
  one-reaction-AC-bonus window (10.4, AR-8, AR-41), since Parry, the
  Parrying property and the soldier's Interpose are M1; the crit cap and
  order (8.1 rule 7), for Adamant; the gear-prevention step (14.1,
  AR-51), for Ringed and Unbowed; and the unit state fields those rules
  use (16.3).
- **M2:** gambeson, brigandine, scale, breastplate, splint, plate; Weave,
  Studded, Overlap, Shoulder, Stalwart, Proof; buckler and its grip rule,
  Reliquary Shield; Guarding (party Interpose); piercing reduction and the
  cap; Corroded; the M2 properties; worn-piece trinket kinds,
  `kindToLocation` and `wornLocations`; the E+ gate reports.
- **Later:** half plate (Enclosed, optional), tower shield, Pitted Armour,
  Blued, Last Ward, theme-tag synergy.

## 19. Risks

- **Heavy armour may be under-picked** under its three real costs
  (Stiff, Clatter, Weighty). Its Strength need almost never binds a
  Strength-main wearer (starting Strength +2 or +3), so it is not counted
  as a cost. Mitigation: pick-rate reports. The named order to soften it:
  drop Weighty, then reduce Clatter to −3 *(illustr.)*. The client
  confirmed both costs, so the lead proposes each step to the client with
  the pick-rate report; neither is tuning.
- **One heavy family may dominate.** With the Strength need rarely
  binding, chain mail, splint and plate differ only by verb, and Proof is
  strong under permadeath. Mitigation: the 17.3 target (no heavy family
  above 50% of heavy wearers), with Proof's per-fight use the first thing
  throttled.
- **The 35% envelope is exactly met by heavy + shield at level 1.** Any
  later +AC source fails the gate by design. Elites (+1 attack) give some
  margin.
- **The caster AC floor depends on the class designs.** Until their
  defence features exist, unarmoured casters (AC 11) are the likeliest
  permadeath. The lowest-AC guard gate makes this a build failure, not a
  surprise.
- **Universal piercing** raises piercing monsters' hit chance against the
  party by about 5–10 points. It is priced in 6.5 (AR-21); re-check the
  6.2 hit curve in M2.
- **Learnability:** 9 armour verbs by M2 plus 15 weapon verbs.
  Mitigations: one line each, mandatory previews, only 3 in M1.
  Monster-side teaching of armour verbs starts only in M2.
- **Stiff makes Acrobatics a light-armour skill.** Light never beats
  medium on AC except at levels 4 and 8, so Stiff is light's lasting edge.
  The binding requirement on dungeon generation (7.3) and its named
  fallback guard this.
- **The buckler grip rule** is a declared choice per attack. If playtests
  find it fiddly, the fallback is buckler +1 against melee only.
- **Reaction ordering, the roll-mode cap and the crit order** are new core
  rules (engine scope), now in M1. Replay and preview property tests must
  cover them (17.2).
- **E may still feel stingy** (2 slots for 7 kinds) and needs about 25–30
  trinket families. Mitigations: the E+ gate, location-hunger smart loot,
  and the families requirement on the trinkets design.
- **Banded AC feels automatic.** Mitigation: the "AC +1: armour band"
  level-up line and the band ladder on the card (6.4). The client chose
  banding over loot-driven jumps (Q2).
- **Strained is harsh** (speed −3 in heavy armour). It is meant to stop
  wearing it, and the verdict says so before equipping.
- **SRD values, names and the mithral, adamantine and arrow-catching
  mechanisms are from memory.** Verify against SRD 5.2.1 before any `srd`
  record (header, section 15).

## 20. Left to the builders

- All numbers in `armour.json` and every *(illustr.)* value, except the
  client's decisions (16.2): band values, the medium Dexterity cap, the
  piercing sizes and the reduction cap of 4; and except the `ePlusGate`
  block (5.10), whose numbers are the lead's.
- `value` inputs, prices and `identifyDifficulty` per record, within the
  items design.
- Notable names beyond 14.6, skins and models; helm, cloak and tint art.
- Which trainings and ceilings each class declares, within 7.2 and the
  caster-defence requirement (11, item 2); starting kits beyond 13.6.
- Preview icons and wording within 8.1 rule 4.
- **Not tuning:** switching E+ on and the `ePlusGate` numbers (5.10, the
  lead's), switching to a hard Strength threshold (7.1, the lead's call),
  switching on the Stiff fallback (7.3, the lead's call), and any change
  to the band values, the piercing sizes or the reduction cap (client
  only; the 9.2 swing report lets the lead propose cap 3 to the client).

## 21. Changes to approved documents

Applied on 2026-11-17, when the client approved this document, except
AR-47. Each change names the exact section it touches. AR-1, AR-3 and
AR-30 carry the client's Q1–Q3 decisions; the rest are the lead's,
confirmed with this document. AR-47 (E+) is recorded as pre-approved by
the client and is applied only if the lead switches E+ on through the
5.10 gate; until then the approved documents keep 2 trinket slots.

**Stat model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| AR-1 | 6, Armour class row (agreed) | Wording only: "values by category and proficiency band (armour design 6.2); heavy ignores Dexterity, negative included; below the Strength threshold the wearer is Strained". The formula is unchanged. | **Client (agreed 2026-11-17, Q2)** |
| AR-2 | 6, Passive stealth row (agreed) | **Pointer only, no rule or wording change:** "(sized in armour design 7.3)". The Stealth-check part of Clatter is the armour design's own rule (7.3) and is not written into this row. | Lead |
| AR-3 | 8.2, piercing bullet (lead's call) | Sizing: 1 against any armour value, +1 against `CountsAsHeavy`, +1 against a weak target, ×2 with Puncture; shares a cap of 4 with Pitted (armour design 9.1). 8.1 (agreed) is unchanged and is met literally. | **Client (agreed 2026-11-17, Q3)** |
| AR-4 | 8.4, enemy traits | The `armoured` trait's profile makes `CountsAsHeavy` true (Corroded, piercing, Clatter); it never costs speed (armour design 7.3) | Lead |
| AR-5 | 10, "Items and inventory" bullet; 12, "All numbers" bullet | Armour values and Strength thresholds are no longer pending: point to this design | Lead |

**Ability model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| AR-6 | 6.2, `fight` row | "Minor uses" include every armour, shield and piece `fight` use: cancelling one event per fight (Ringed, Double Ring, Proof, Twice-Proof), Shadowed, Warding Lining, Runner's Boots and Forewarned's fight-start window (armour design 8.1 rule 5) | Lead |
| AR-7 | 7.2, actions table | New rows: shield bash (replaces one attack; Strength; 1d4 B Push); armour and shield training are hard; Strained speed −2; Stiff (Acrobatics disadvantage in medium and heavy armour) | Lead |
| AR-8 | 7.3, reaction policies | At most one reaction AC bonus per attack roll, resolved by the window algorithm in armour design 10.4 (skip ignored or unavailable reactions, evaluate each policy or "ask me" in candidate order, the first that fires closes the window) | Lead |
| AR-9 | 10.2, offer kinds | "Armour training (next category, up to the class's ceiling)"; "shield training" (only for a class whose `shieldTraining` is `allowed`; armour design 7.2) | Lead |
| AR-10 | 9.1, piercing bullet; 20 | Pointer to the sizing in AR-3; dependency pointer | Lead |

**Conditions design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| AR-11 | 18.10, Pitted Armour | The armour value only, never below 10; one instance with intensity 1–2; landing rule; rust only on metal (natural armour by its `metal` flag); Blued immunity; shares the reduction cap with piercing by the reduction it actually applies (armour design 9.1); repair details, a success removing the whole instance; floor 2+, visible, counts toward the type's 2 (armour design 9.4) | Lead |
| AR-12 | 18.5, Corroded | "+1 against heavy armour or armoured traits" reads `CountsAsHeavy`; outside the reduction cap | Lead |
| AR-13 | 18.7, Hushed | "Ignores the heavy armour stealth penalty, passive and checks (Clatter)" | Lead |
| AR-14 | 18.9, Fortified | Does not stack with the Raise shield property: the bonus is max(Fortified 2, Raise 2 unless Pry), so Fortified applies against Pry (armour design 6.1); its crit downgrade comes first and spends nothing (armour design 8.1 rule 7) | Lead |
| AR-15 | 18.10, Notched | A Reliquary Shield is a focus: Notched may land through it and suppresses Spare only, never the AC | Lead |
| AR-16 | 25 | Dependency pointer | Lead |
| AR-51 | 9.2, application pipeline | New gear-prevention step between step 3 (contest) and step 4 (ward): Ringed, Double Ring and Unbowed stop the condition after a failed contest or an automatic landing, before a ward is spent; emits `ConditionResisted{reason: ArmourVerb}` (armour design 14.1) | Lead |
| AR-52 | 10.1, "One move per hit" bullet | Pointer: armour verb signatures that move a target (Bull Rush) are verb-sourced (weapons 7.2 rule 4, AR-49) | Lead |

**Enemies design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| AR-17 | 5.1, `armour` row | Natural armour is `{natural: offset, metal?}`: medium band + offset; armed types may use it for display-only issue hides | Lead |
| AR-53 | 5.1, `abilities` and `speed` rows | `abilities`: the type declares Dexterity's spread slot (main, secondary or other), which armour validation reads at every shift (armour design 12.2). `speed`: authored before armour penalties; the derived speed stays within 3–8 (armour design 7.3) | Lead |
| AR-18 | 5.2, AC bullet | "Counts as medium … except that the armoured trait or Plated makes `CountsAsHeavy` true for Corroded, piercing and Clatter; the AC formula is unchanged." The band is checked at every legal shift. Issued armour verbs obey the role-answer rule. | Lead |
| AR-19 | 5.6, gear-line example | "Pike: piercing, reach 2, Set · Chain mail: heavy, Ringed" (fixes "Hide: light", F2) | Lead |
| AR-20 | 6.4, guards table and notes | Guards are also evaluated at the lowest legal party AC as armour design 11 item 2 defines it (build fails), and from M2 per attacker with its piercing R; new AC envelope rows in hit chance (sustained ≥ 35%, charged ≥ 15%, sustained with one gear roll-mode effect ≥ 10%, levels 1–10) | Lead |
| AR-21 | 6.5, Offence and Defence | Armour verbs price the defence term through `verbDefencePermille` (provisional), later measured per theme. Piercing offence is computed at the reference AC − R. Reaction non-stacking applies when pricing leader-and-soldier pairs. | Lead |
| AR-22 | 7.3, level shift bullet "AC moves by the table's AC change" | "AC is recomputed from the loadout; a shift whose AC leaves the band is illegal" (F4) | Lead |
| AR-23 | 8.1, silhouette rule | "A tall shield means soldier"; issue helms (crests on leaders) are display-only props (F5) | Lead |
| AR-24 | 9.1, after the caps table | A shield bash exists for a monster only if it is authored as one of its two at-wills | Lead |
| AR-25 | 11.1, scorer terms; 17.3, Kiter and Raider targeting | Candidates are scored through the resolve preview with armour verbs and spent state; "lowest AC" means visible AC | Lead |
| AR-26 | 15.3, step 4, "Legal means…" | For a notable armour or shield, legal also means: the carrier's worn issue armour has the same `armourCategory` (natural never matches) or the same shield kind; its derived AC is unchanged; its rolled properties pass the role-answer rule; reaction properties within the 9.1 caps (armour design 12.6). The agreed 15.4 table is unchanged. | Lead |
| AR-27 | 17.2, Plated | "Counts as heavy" for category-reading rules only; its +2 AC is the `traits` term of the AC query, never part of the armour value; its speed −1 is the heavy speed penalty, paid once (F3; armour design 6.1, 7.3) | Lead |
| AR-28 | 19.1, `monster.raider_spear` | `offHand: item.shield`, `armour: item.gambeson`; `levelShift` [−2, 2] is unchanged (in band throughout) (F1) | Lead |
| AR-29 | 20.1, 20.3, 25 | Validation rules 4, 5, 6, 8, 15 and 19 of armour design 17.1; armour simulator rows; dependency pointer | Lead |

**Items design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| **AR-30** | **5.2, places table, Trinkets row (agreed places and counts)** | **"Worn items: rings, amulets, charms, cloaks, helms, gloves, boots; at most one per body location (rings two); never AC." The count of 2 is unchanged.** | **Client (agreed 2026-11-17, Q1)** |
| AR-31 | 5.1, `inventory.json` | Add `kindToLocation` and `wornLocations` (armour design 5.4), and the `ePlusGate` block (armour design 5.10), whose numbers are the lead's and are not tuning | Lead |
| AR-32 | 6.3, suggested recipient | Armour and shield gain = AC change × 10 + value difference − the stealth term; Strained recipients rank after usable ones; for pieces, a free matching location first (F7) | Lead |
| AR-33 | 7.3, greyed reasons; 8.2, verdicts | Add `LocationOccupied` and `ReactionCap`; reuse `Untrained` for armour and shields; verdicts "cannot wear: untrained", "Strained: needs Str +1/+2" (a warning, never a refusal), "replaces X (feet)" | Lead |
| AR-34 | 12.2, family record | Trinket families are finer than kinds (at least 3 legal families per piece location, as a running total in each wave from M2); the Reliquary Shield counts under `shield` | Lead |
| AR-35 | 12.3, fields | `armour{armourCategory, verb}`, `shield{}`, `trinket{kind, wornOn}`; `armourValue` is derived and never stored; `display.socket` (F6) | Lead |
| AR-36 | 16, row "Armour records…" | Value, Dexterity cap and threshold are derived from `armour.json`, never written on the record (F6) | Lead |
| AR-37 | 17, rows 2, 3 and 6 | Row 2: designed, point to this document. Row 3: armour kits as in armour design 13.6. Row 6's brief adds piece kinds, location domains (armour design 5.6), the strict no-AC rule, the one-table rule, the reaction cap, pieces at Uncommon at most, at least 3 legal families per piece location per wave, `requires` tags for location hunger (armour design 5.7), and the piece power target (armour design 17.3). | Lead |
| AR-38 | 19.1 | Point to armour design 17.1 rules 10, 11, 13, 14, 17 | Lead |
| AR-39 | 20, M2; 24 | "Trinkets, including worn-piece kinds"; dependency pointer | Lead |
| AR-47 | 4, decision 1 ("trinkets 2"); 5.1, `trinketSlots`; 5.2, Trinkets row count | **Conditional:** applied only when the lead switches E+ on through the armour design 5.10 gate: trinkets 2 → 3, `trinketSlots: 3`; the switch and its evidence are reported to the client. The trinket loot share rises by 1–2 notables per run from weapons and consumables, inside the budget; that share is the armour design 5.7 table, so no enemies rule changes. | **Client (pre-approved 2026-11-17, Q1)**; the gate is the lead's |

**Weapons design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| AR-40 | 7.5, step 2 | At most one gear-sourced roll-mode effect on an attack against a unit, chosen by priority (unlimited passives, then once-per-round, then charges; ties by slot order); suppressed effects not spent; Overlap only on net advantage; gear roll-mode effects suppressed while the target has Raise or Fortified (armour design 8.1 rule 6). Hailguard is fixed here, never after the roll. | Lead |
| AR-41 | 7.5, step 5; 15.4, "No reaction priority rule" | One reaction AC bonus per attack, by the armour design 10.4 window algorithm (pool-free target, then pool-free allies in unit order, then charges with the target's own first; no reaction AC bonus after a gear-sourced disadvantage); 15.4 reads "No reaction priority rule except the AC-reaction order in 7.5 step 5" | Lead |
| AR-42 | 7.1, Graze text | "(Parry, Interpose or any reaction AC bonus; exact test in 7.6)" | Lead |
| AR-43 | 11.2, Versatile | "Uses `versatileDice` when the set's off hand is empty, or holds a buckler and the declared grip is `twoHand` (armour design 10.2). Any other shield, or a focus, blocks it." | Lead |
| AR-44 | 14, "Untrained in the tradition" bullet; 17.8, holy symbol notes; 17.9; 26 | 14: "except a Reliquary Shield, whose properties follow shield training (armour design 10.2)". The Reliquary Shield is notable-only; rod + Reliquary is legal; 17.9 and 26 point here | Lead |
| AR-48 | 7.5, step 1 | Declaration gains a `grip` input (`oneHand` or `twoHand`), legal only for a versatile weapon in a set whose off hand is empty or holds a buckler; default `oneHand` with a buckler; the AI picks it by the scorer. `twoHand` with a buckler sets `bucklerOffUntilOwnTurnStart` at declaration, hit or miss, reactions and opportunity attacks included (armour design 10.2) | Lead |
| AR-49 | 7.2, rule 4, verb-sourced list | Add armour verb signatures that move a target (Bull Rush, armour design 14.4) | Lead |
| AR-50 | 18.2, Quickdraw eligibility | Also eligible on shields (it acts on the set); a shield may also roll `props.verb.push` through its bash's Push (armour design 14.5) | Lead |

**Main design (2026-11-15):**

| # | Section | Change | Approved by |
|---|---|---|---|
| AR-45 | 2, Anti-goals, "Protected content" | The credits confirm the SRD 5.2/5.1 attributions cover the armour table and the adapted magic-item mechanisms; adapted armour records carry `adapted` (armour design 15) | Lead |
| AR-46 | 10 | Armour and shields are no longer pending | Lead |

AR-1, AR-3, AR-30 and AR-47 change an agreed row or count, and the
client approved each. AR-2 adds a pointer to an agreed row and changes no
rule or wording. No other change touches an agreed row: AR-26 amends
enemies 15.3 step 4 instead of the agreed 15.4 table; location hunger is
data on the agreed 15.3 step 2 weighting (5.7); Clatter's check part is a
gear modifier that leaves the agreed 15.5 Steal roll line as it is (7.3).
Stat model 6's AC formula and "Armour is hit or miss" and stat model 8.1
(agreed) are not changed.

## 22. Depends on systems not yet designed

- **Classes and offers:** each class's trainings,
  `armourTrainingCeiling` and `shieldTraining`; the binding caster-defence feature for every
  class with a ceiling below light, with simulator evidence (11, item 2);
  the "armour training" and "shield training" offer kinds; capstone heavy
  training.
- **Trinkets:** settled in `2026-11-17-trinkets-design.md` (approved
  2026-11-17; changes TK-1 to TK-8): the location tables and families (at
  least 3 legal families per piece location, as a running total each
  wave), within 5.4–5.6; neck, finger and none domains; capacity
  trinkets; the piece power target (17.3).
- **Starting kits:** armour per class and companion (13.6; items 17,
  row 3).
- **Companions:** armour kits; companions without ability increases and
  the light-armour case.
- **Death and healing:** the crit rule (Proof, Adamant and every guard
  assume SRD double dice); Last Ward (Later).
- **Dungeon generation and encounter content:** the **binding**
  Acrobatics requirement (at least 3 Acrobatics-gated choices per floor
  from M2, with simulator evidence; 7.3); stealth content for Clatter.
- **Enemy content:** rust, acid and ooze types as Pitted sources (Later);
  M2 grapplers for Studded; piercing-weak and armoured types.
- **Consumables:** none required; Hushed sources are the conditions
  design's.

## 23. Open items for the client

None. Approved by the client on 2026-11-17.

Future items that come back to the client only if the simulator or
pick-rate reports trigger them:

- **E+ switch (AR-47):** if the lead switches E+ on through the 5.10
  gate, the switch and its evidence are reported to the client.
- **Reduction cap 3:** if the 9.2 swing report exceeds its trigger, the
  lead proposes cap 3 to the client with the report.
- **Heavy armour costs:** if pick-rate reports show heavy armour
  under-picked, the lead proposes each softening step (drop Weighty, then
  reduce Clatter) to the client with the report (section 19).

## Appendix A: how the review findings were resolved

| Finding | Resolution |
|---|---|
| Light weakly dominated by medium | Corrected reference (light +1 at levels 4 and 8 for Dexterity-maxed builds); medium and heavy are Stiff; light is the only quiet, agile armour |
| Heavy + shield the default | Heavy pays Stiff, Clatter (−5, which replaces the redundant disadvantage) and Weighty (speed −1); its Strength need rarely binds a Strength-main wearer, so it is not counted as a cost (19); Muffled is a major property, never with Fitted; the heavy-family target (17.3) watches Proof |
| Vestments copied Steady Casting; Serene was dead | Cut; robes are an unarmoured skin; validation rule 14 (dead properties) |
| Turning was a better Parry | Cut; the worst stack is now 22 at level 1 |
| Duplicate "first ranged attack" and other duplicate effects | One-table rule; Windcloak cut; one Hailguard; Lined and Steadfast moved; Forewarned replaces Vigilant; the roll-mode cap |
| Reliquary dominance and focus-rule breaks | Notable only; standard focus improvised bash; rod + Reliquary legal; Notched and property rules stated |
| Buckler dominated, or a free +1 | Grip rule; "versatile wielders only" stated; Punch Buckler cut |
| Roster and name conflicts | One roster (13.1); renamed Supple, Ringed, Studded, Stalwart, Unshaken, Barred, Forewarned, Shelter, Double Ring, Deep Stance; validation rule 13 |
| E presented as no change | AR-30 went to the client (Q1, agreed); E+ pre-approved lever with a stated gate; `LocationOccupied` report |
| Inconsistent AC rules for pieces | Strict no-AC rule for any effect kind; Turn the Blow cut; reaction cap |
| Fight-dependent or unclear verbs | Studded staged with M2 grapplers; Enclosed Later and optional; Overlap text shortened; `squaresMovedLastOwnTurn` |
| Crit-negation stacking | Crit cap of 2 per fight; guards run with equipped parties |
| Issued verbs cancelled role answers | Role-answer validation; soldiers in chain shirt or chain mail, brutes natural or chain shirt, skirmishers in gambeson |
| M1 AC above the reference; cap test at the wrong level | AC envelope gate at every level; slice-kit and lowest-AC guard runs |
| Low-AC end unbounded | Guards at the lowest legal AC (build fails); binding caster-defence requirement |
| Penalty stacking | Reduction cap 4; swing report with a fallback to 3 |
| Piercing scope | P-mid: universal with a heavy bonus, so agreed 8.1 is met literally (Q3, agreed) |
| No AI integration | Resolve-preview scoring with spent state; visible-AC targeting; measured prices |
| Training offers reopened the plate caster | Class training ceilings |
| Stacked disadvantage | Gear roll-mode cap (AR-40) |
| Gain ignored category costs | Stealth term; Strained ordering (AR-32) |
| Proof and Ringed as `fight` uses | Ruled minor (AR-6); crit cap |
| Reaction rule conflicted with weapons 15.4 | AR-41, AR-42; pool-free reactions first |
| Shields catalogue errors (dagger + shield, warhammer) | Corrected in 10.1 and 10.2 |
| Cushioned reduced damage | Cut |
| `CountsAsHeavy` against enemies 5.2 | AR-18, AR-4 |
| Licensing | Section 15, AR-45 |
| Monster bash uncosted | Only if authored as an at-will (AR-24) |
| Hushed, Strained stacking, lurker line, Watchful, Quiver, piece waves | AR-13; one Strained penalty per unit; F10; Watchful dropped; Quiver dropped; all pieces M2 with display-only M1 helms |
| Parent errata F1–F10 | Section 3.3 and the changes it names |
| Check round: buckler grip read from damage | Declared grip at attack declaration, buckler-off flag hit or miss (10.2, AR-43, AR-48) |
| Check round: Hailguard could not resolve before the roll | Fixed at weapons 7.5 step 2, policy `always` with `nonMinion` (14.5, AR-40) |
| Check round: Plated +2 and Raise/Fortified had no defined place | `traits` term; max expression; Fortified applies against Pry (6.1) |
| Check round: reaction window, roll-mode cap and crit cap underdefined | Window algorithm and unit order (10.4); priority, no spend when suppressed, Overlap on net advantage, charged exclusion (8.1 rule 6); crit order (8.1 rule 7); all staged M1 (18) |
| Check round: Ringed and Unbowed had no pipeline step | Gear-prevention step between contest and ward (14.1, AR-51) |
| Check round: state, Pitted and validation inputs | State table and round definition (16.3); Pitted applied, one instance (9.1, 9.4); maximum Dexterity, lowest legal AC, hit-chance envelope with a roll-mode floor (11); Dexterity spread slot (12.2) |
| Check round: carriers and role answers | Worn same category only; derived AC equal; `negates[]` on properties and signatures (8.1 rule 10, 12.6) |
| Check round: agreed parent rows | AR-2 a pointer; AR-26 moved to enemies 15.3 step 4; location hunger as `requires` data; Clatter's check part a gear modifier; corrected closing sentence (21) |
| Check round: E+ gate measured full slots and weak pieces | Fixed lead's numbers in `ePlusGate`; free-slot against full-slot pick rates; near-tie left-behind; piece power target (5.10, 17.3) |
| Check round: heavy's Strength cost, light against medium, soldier piercing | Strength not counted as a cost; Fitted off M1 notables; heavy-family target; binding Acrobatics requirement with a Stiff fallback (7.3); heavy reach soldiers (12.4) |

## Appendix B: sources

- The seven parent documents, all in `docs/superpowers/specs/`.
- The lead's armour and shields proposal and review findings
  (2026-11-17), with the client's answers to Q1–Q3 and confirmation of the
  lead's calls.
- The monster loadout AC checks in 12.3 and 12.4 come from a scratch
  script that is not part of the project; the simulator replaces them
  from M1 (loadout-band validation) and M2 (patterns).
