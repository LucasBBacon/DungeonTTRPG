# Trinkets and Worn Pieces: Design

Date: 2026-11-17
Status: Approved, 2026-11-17
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design) and
`2026-11-17-armour-design.md` (armour design)

Items marked **(agreed)** were decided by the client on 2026-11-17 for
the four questions this design raised (domains, rarity, buttons, damage),
by choosing the lead's recommendation in each case. Under Q1 the client
also signed off change TK-1 (the armour 13.4 jewellery Domain cell) and
the rider-ownership rule (5.7, TK-2). Items marked **(lead's call)** were
filled in by the design lead; the client said on 2026-11-17 that
everything else in the lead's proposal stands, and they are confirmed
when this document is approved. Items marked **(settled)** restate a rule
from a parent document. Items marked **(change)** amend an approved
document and are listed, with an id, in section 22. Once approved, every
item here is binding on the builders.

This is the sixth **category design** on the items design's roadmap
(items design 17, row 6): rings, amulets, charms, capacity trinkets and
the worn pieces (helms, gloves, boots, cloaks) that the armour design made
trinket kinds. It takes the armour design's inherited brief (armour 5.4–
5.10, 13.4, 14.1, 16, 17, 21; AR-37) as input.

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. Tuning numbers live
in `trinkets.json` and the trinket keys of `inventory.json` (17.2). The
builders own them and the simulator calibrates them (section 21), except
where a value is marked as a client decision or a lead's number.

**SRD check (lead's call).** The SRD-shaped mechanisms adapted here
(rarity tier names, Warded as a ward, the element channel shape, rest-
charged item powers, the capped capacity item) and the SRD 5.2 class and
feat shapes the copy registry protects against (Steady Aim, Patient
Defense, Cunning Action, halfling Luck, Heroic Inspiration) are written
from memory. **Every SRD-shaped mechanism must be checked against the SRD
5.2.1 document before any trinket or property record is written**, and
every record flagged `srd` cites the entry it was checked against
(section 16).

**Waves:** M1 is the vertical slice, M2 the full run with companions, M3
profile and unlocks, Later everything after M3 that no milestone names.
**(settled, main design 9; items design header)** Trinkets are an M2
category; M1 has only the display-only helm (armour 5.8).

## 1. Intent

Trinkets are the worn magic a unit chooses at a rest: two slots, shared by
helms, gloves, boots, cloaks, amulets, rings, charms and capacity items.
The parents settle the slot count, the location rule, the piece domains,
the ban on AC, the rest gate, the buff test and value budgets, the
reaction cap and the gear-prevention step. **(settled)** They leave open
what jewellery does, whether a kind promises specific effects, what no
trinket may ever do, how rare a trinket can be, whether trinkets grant
buttons, how trinkets touch damage, how their effects stack, and the
roster. This document closes those questions.

The system should make every trinket a small rule the player feels within
a fight or two, let the kind on the card predict what the item does, keep
every number the encounter maths rests on away from trinkets, and fill
two slots with real choices from about 9 finds a run. **(agreed, Q1)**

## 2. Goals, non-goals, anti-goals

### Goals

- **The location promises the subject; the shape tells you when.** Each
  body location owns a domain and a signature shape, and each jewellery
  kind owns a lane. **(agreed, Q1)**
- **Bend one rule, never add a number.** Every trinket is a verb, a cost
  step, a prevention or a channel. **(lead's call)**
- **One line per trinket.** "Ember Ring: your strike, 1/rest, one part
  turns fire." **(lead's call)**
- **Class-neutral.** Every trinket is useful to at least 2 of the 4 launch
  classes or to any companion. **(lead's call)**
- **Every trinket fires** at least once per 3 fights for its wearer.
  **(settled for pieces, armour 17.3; extended here)** Capacity trinkets
  are exempt: they have no combat line, and their hook is the capacity
  query (8.3, 18.1). **(lead's call)**
- At most four things to read per unit in combat; at most one trinket
  button on the action bar. **(settled, ability model 11.4; agreed, Q3)**

### Non-goals

- **Fundamental numbers on trinkets:** AC, attack, saves, damage, ability
  scores, speed, hit points. **(settled for AC, armour 5.5; the rest
  lead's call)**
- **Trade-off lines** (+X / −Y) in M2. Drawbacks arrive with curses.
  **(lead's call)**
- **Auras and passive party effects** in M2. **(lead's call)**
- **Set bonuses.** **(settled, armour 5.9)**
- **Strict per-kind domains** (helm, hood and circlet each their own job).
  **(agreed, Q1)**

### Anti-goals

- **The best-in-slot hunt:** one trinket every build must wear.
  **(lead's call)**
- **The "cloak of protection" must-have:** a flat defence every unit
  wants. **(lead's call)**
- **Bring-the-right-key play:** swapping resistance trinkets per floor or
  per room. **(settled, stat model 2; items design 2)**
- **Action-bar bloat** from item buttons. **(agreed, Q3)**
- **Free refills** and class-pool shortcuts. **(settled, ability model 2)**
- **Stun-proof parties:** stacked preventions that delete the monster
  condition design. **(lead's call)**
- **Protected content.** **(settled)**

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Items design 4 decision 1, 5.2 (agreed) | Two trinket slots; changed only at a short rest, camp or Equip now | The slot count; the rest gate stops per-room swaps |
| Armour 5.4 (agreed Q1) | `trinket{kind, wornOn}`; `kindToLocation`; one item per location, two rings | Kinds and locations are fixed; this design adds no kind enum |
| Armour 5.5 (agreed Q1) | One fixed location verb per piece; pieces Common or Uncommon; no AC of any kind; reaction cap; one-table rule | Piece rules (5.5, 7, 13) |
| Armour 5.6, 13.4 (agreed Q1 for 13.4) | Head = senses, mind, crit riders; hands = grapple, handling, Administer; feet = movement, Prone, hazards; back = elements, stealth; jewellery "Trinkets design" | Piece domains stand; the jewellery cell is filled (TK-1) |
| Armour 5.7, 5.10, 17.3 | About 9 trinkets per run, 4–5 of them pieces; location hunger as `requires`; E+ gate; piece against jewellery within 1 value unit | Roster size (13.8); power checks (8.3); E+ reports (TK-4) |
| Armour 14.1 (AR-51) | Gear-prevention step between conditions pipeline steps 3 and 4 | Trinket preventions act there (TK-5, TK-21) |
| Armour 16.3 | `fightUses` state; round = initiative round | Trinket `fight` uses (TK-7) |
| Armour 17.1 rules 10, 11, 13, 14, 17 (AR-38) | No AC on trinkets; one table, one property; name collisions; hooks and dead properties; piece rules | Still apply (18.1) |
| Items design 5.4 | Capacity trinkets at most +2 pack slots; no infinite storage | Capacity kind (13.7) |
| Items design 11 | At most 1 dormant property on Uncommon or better; equipping reveals nothing; camp identifies free | Dormant lines (7.2, 15) |
| Items design 12.2 | Trinket family cap 1 per run | Families finer than kinds (13) |
| Items design 12.4, 12.5 | Buff test; a tier is a value budget; value equals the budget | Power budget (8) |
| Items design 12.8, 19.1 rule 4 | Pools store `spent`; `rest` only with an authoring reason | Pool rules (9.3, TK-13) |
| Ability model 6.2 (AR-6) | `fight` pools for minor uses only | Every trinket `fight` use is minor (TK-18) |
| Ability model 7.2, 7.3 | Universal action costs; reaction policies with target filters | Cost and reach steps (6.1); charge policies (9.1) |
| Ability model 8.2, 8.3, 14.3 | Areas hit allies unless `selective`; the preview uses only what the party knows; `UseAbility` steps (targets locked in unit order at step 4, per-target resolution at step 5) | Ring target and knowledge rules (9.4) |
| Ability model 9.1–9.3 | Damage parts with a rider flag; weak target gets the rider automatically; rider saves against the attacker's difficulty; one rider per target per use; no rider on a target that already has that condition | Rings (9.4, 9.5) |
| Ability model 10.2 | Eight limited abilities; item-granted abilities do not count | The loophole `ButtonCap` closes (9.2, TK-20) |
| Conditions 7.1, 7.2, 12.2 | Hard control only from limited abilities; anti-lock guards; immunity budget | Denylist rows (6.2) |
| Conditions 18.9 | Warded blocks the next harmful condition at step 4 (not lingering conditions, Downed or Surprised; at most once per ability use per target); Helped: "its next attack roll or skill check has advantage" | Charm outputs (13.7, TK-22) |
| Conditions 9.2, 18.6 | Pipeline steps and entry points; `damageType` ends (fire ends Chilled, Frozen, Soaked, Oiled; cold ends Burning, Smouldering); Oiled's fire payout | Ring channel filters and the Oiled ruling (9.4, TK-25); gear-prevention order (7.3, TK-21) |
| Enemies 6.4, 9.1, 9.2, 15.2, 15.3, 15.4, 17.1, 17.2 | Guards; rank caps (≤1 reaction beyond the opportunity attack, ≤1 bonus-action ability, one carried limited ability beyond the cap); 2 harmful conditions per type; only `armed` types carry; carrier legality at 15.3 step 4; carried pools map to `uses`; role answers; banned affix pairs | Monster use (section 12) |
| Weapons 6.4, 18.1 | Precision parity; charged echoes of weapon properties are legal, always-on copies are not | Class-copy rules (11); ring echoes (13.6) |
| Stat model 6, Speed row (agreed) | Speed changed only by species, role, traits, armour and conditions | No trinket speed (6.2) |
| Main design 2 | No D&D names; SRD content carries attribution | Section 16 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items design 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored.

### 3.3 Findings in the parent documents

Errata found while designing; each is fixed by a change in section 22.
**(lead's call)**

| # | Finding | Fix |
|---|---|---|
| F1 | Armour 5.6's Ledgewalker's Boots ("advantage on saves against Teetering") can never fire: Teetering is the result of a successful hazard-edge save (conditions 10.2, 18.4) | "Advantage on hazard-edge and landing saves" (TK-2) |
| F2 | Armour 14.4's Later signature Sealed (save advantage against Poisoned) would copy Antidote Locket under armour 17.1 rule 11 | Sealed reworded to "Poisoned never lingers on you" (TK-6) |
| F3 | Armour 13.4's jewellery Domain cell is a pointer ("Trinkets design") and 5.6's last row lists placeholder subjects | Filled with the four lanes (TK-1) |
| F4 | Ability model 10.2 exempts item abilities from the eight-limited cap, so two trinket buttons per unit would be free bar space | `ButtonCap` (9.2, TK-20) |
| F5 | Items 12.3 `pools[]` refill has no `fight` value, and AR-6 covers only armour, shield and piece `fight` uses | Trinket `fight` uses live in `fightUses` (TK-7, TK-13, TK-18) |
| F6 | Armour 5.5 bans trinket AC, but an output condition that writes AC (Corroded) reads two ways | No trinket output writes AC; Corroded outside every trinket list in M2 (6.2, rule 24) |
| F7 | Items 12.6 and 19.1 rule 2 do not say how a granted button is valued | A button is a property whose effect is the grant, valued once (TK-12) |
| F8 | Items 17 row 6 lists property tables by kind ("rings, amulets, charms, cloaks, helms, gloves, boots"), which contradicts the location tables | Piece tables by location, jewellery tables by kind (TK-9) |

## 4. Framework decisions

The client chose the first four on 2026-11-17. **(agreed)** The rest are
the lead's calls, which the client said stand.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Domains (Q1) | **(a) Themed (B′):** pieces keep the approved location domains strictly; jewellery lanes ring = your strike, amulet (neck) = life and the body, charm (none) = stored moments, capacity = carrying; the kind inside a location is flavour with a lint-only authoring lean; shared jewellery pool deferred to Later. Approves TK-1 and the rider-ownership rule (TK-2). **(agreed)** | (b) Strict per kind: reopens agreed 13.4, needs about 35–42 families and new art, answer kinds often fail to drop. (c) Open: the kind predicts nothing in a new player's first runs and reverses agreed-Q1 wording. |
| 2 | Rarity (Q2) | **(a) Rare only on amulets,** floor 3+, at most 1 Rare trinket per floor, never a boss signature; rings, charms and pieces Common or Uncommon; no Very rare. **(agreed)** | (b) Common and Uncommon only: floors 3–5 offer no trinket that matches Rare weapons and armour, and curses have no trinket host Later. Rare on all jewellery: two rings or charms stack two Rares. |
| 3 | Buttons (Q3) | **(a) At most one trinket-granted button per unit (`ButtonCap`), on charms only in M2;** rings channel automatically. **(agreed)** Charm abilities get a future design pass (24.2). | (b) Rings hold the button lane and charms go automatic. No buttons at all: loses the stored-moment charm. One button per ring: the Pillars / WotR bar bloat. |
| 4 | Damage (Q4) | **(a) Zero added damage, literally:** a ring-shifted part never gains the weakness bonus, but still lands its rider automatically on a weak target. **(agreed)** | (b) Edge-style full ladder: one ring gives every class a weakness key on every spell and needs a party damage ceiling. |
| 5 | Stacking | **One merged cap, `DuplicateAnswer`,** counting armour and shield, plus runtime limits (section 7) **(lead's call)** | Three separate caps (`DuplicateProperty`, `DuplicateAnswer`, `TagStack`): three overlapping greyed reasons |
| 6 | Trade-offs | **None in M2;** drawbacks arrive as curses (Later, Rare+) **(lead's call)** | `cost` lines of negative value break "value equals budget"; numeric +X / −Y fails the buff test |
| 7 | Party effects | **No passive party effects or auras in M2;** ally effects are wearer-triggered, one ally per trigger **(lead's call)** | One Rare amulet aura per party: a Later option for the client (24.3). Passive buffs on 4 units × 2 slots: fails ability model 9.6 |
| 8 | Class features | **Class copy registry at any charge level,** bidirectional; trinkets never act on class abilities or pools **(lead's call)** | The weapons copy rule alone (charged echoes legal): lets a rest charge copy a class feature |

### 4.1 How other games handle trinkets and slot domains

Mechanism references only; no names or text are taken from any of them;
details as recalled. Anything SRD-shaped is checked against SRD 5.2.1
before data (section 16). **(lead's call)**

| Game | What it does | We take | We avoid |
|---|---|---|---|
| **5E SRD 5.1 / 5.2** | Loose kind patterns (boots move, cloaks hide, amulets keep you alive, rings do anything); attunement cap of 3; ring and cloak of protection (+1 AC and saves); score-setting gauntlets, amulet and headband; rest-charged item powers; bag of holding | Kind as a loose promise; a hard count limit (our 2 slots); rest-charged powers | AC on rings and cloaks; score-setting items; free slot refills; infinite storage; "ring of anything" |
| **Pathfinder 1e** | Full slot list with slot affinity; the "big six" required +N items ate the wealth | Slot affinity as readability | Any trinket holding a number the maths expects |
| **Pathfinder 2e** | Invested-item limit; fundamental bonuses moved into runes, then automatic bonus progression | A count limit plus per-item usage text | Fundamental numbers on items |
| **13th Age** | Chakra slots; each true item has a quirk that takes over when you wear too many | A small cost line gives an item character (our Later curses) | Kind-typed default bonuses |
| **Shadow of the Demon Lord** | Cheap, narrow enchanted objects, often uses per rest | The register for Common trinkets | — |
| **Mausritter, Cairn** | Spell items with usage dots; recharge needs a condition | Charged items are the most legible | — |
| **Baldur's Gate 3** | AC on body and shield; pieces carry effects; rings and amulets grant once-per-rest spells | AC from body and shield only; one granted button at most | Stat-setting items, displacement cloaks, stacked roll-mode effects |
| **Diablo 4** | Aspects by category and slot; the amulet is a wildcard slot | Slot categories read well | A wildcard slot becomes the best-in-slot hunt |
| **Path of Exile** | Slot-locked affix pools (movement speed only on boots) | Location-locked tables | A slot that alone owns a needed number becomes a tax |
| **Darkest Dungeon 1 and 2** | 2 trinket slots; +X / −Y trade-offs; class-locked and set trinkets | Two slots where every item is a choice | Class locks (dead loot under a family cap of 1); numeric trade-offs; sets |
| **Slay the Spire** | Relics bend one rule; boss relics trade power for a drawback | "Bend one rule" beats "+N"; the Rare feel | Counters the unit plate cannot show |
| **Hades** | Strict god domains, each a status verb | A strict domain reads well when it is a verb or condition theme | — |
| **DCSS** | Single-property rings; amulets rarer and more singular; swap-per-encounter resistance rings | The amulet as the signature jewel; one line per item | Swap-per-encounter play (our rest gate stops it) |
| **Gloomhaven** | Items spent and recovered on rest, or consumed | `rest` charges on trinkets; `never` pools on consumables only | — |
| **Pillars of Eternity, Pathfinder: WotR** | Full paper dolls, many per-rest item spells | — | Action-bar bloat (our `ButtonCap`) |
| **Into the Breach** | Pilot skills: one positional rule bend each | The model for feet and back verbs | — |

**Overall:** games that type effects strictly by kind break their own
pattern whenever a needed number has nowhere else to live; games that
make kind cosmetic rely on large content and a count limit. The stable
middle is **themed locations with hard global bans on fundamental
numbers**. Best-in-slot in 5E, BG3 and Pathfinder came from flat numbers
and stat-setting items, not from slot themes; the buff test and the
denylist (6.2) remove the cause.

## 5. Domains

### 5.1 The client's question

> "Should specific trinket and worn piece categories only give specific
> gameplay changes? I.e. rings only being magical effects, gauntlets
> resistances, etc. What should we limit these items to affecting in
> general?"

**Answer (agreed, Q1):** yes, as a theme with a hard edge, owned by the
**body location**, not by the kind. Each location owns a domain and a
signature shape (5.5); what no trinket may ever do is one closed denylist
(6.2).

### 5.2 What was already fixed

**(settled)** Each piece has exactly one fixed location verb from its
location's table (armour 5.5), and the four piece domains are agreed
(armour 13.4). Boots, cloaks and the three jewellery kinds already map
one-to-one to a location, so "strict per kind" differs from "per
location" only on head (helm, hood, circlet) and hands (gloves,
gauntlets, bracers), plus how narrow each jewellery lane is.

### 5.3 The three models

| Model | Description | Strongest case | Costs |
|---|---|---|---|
| A. Strict per kind | Each of the 12 kinds owns exclusive subjects | Best card prediction; `LocationOccupied` becomes a real choice | 12 promises to learn; about 35–42 families; new art; each piece kind appears in about 45% of runs against about 73% per location; reopens agreed 13.4 |
| **B′. Themed (chosen)** | Pieces keep the location tables; each jewellery kind gets a lane and a closed table; inside a location the kind is flavour with a lint-only lean; shared jewellery pool Later | Readability bought by the location rule, plus jewellery identity, at about the framework's option-B cost | Kind inside head and hands predicts only loosely |
| C. Open | One shared trinket table; the denylist is the only limit | A curated roster learnt by name; about 5× more Uncommon pairs | The kind predicts nothing; the stacking limit must be rebuilt as a runtime cap; fiction strains; reverses agreed-Q1 wording |

What makes kinds feel different in play is **shape** (when and how an item
acts), not only topic, so each location also has a signature shape.

### 5.4 Verdict and judges' scores

Three independent judges scored the three models, 0–10 per criterion
(higher is better; averages; totals out of 70):

| Model | Learnability | Variety and surprise | Decision quality | Low best-in-slot risk | Fit with docs | Low content cost | Loot health | **Total** |
|---|---|---|---|---|---|---|---|---|
| A. Strict | 9.0 | 4.0 | 6.3 | 6.0 | 3.0 | 3.0 | 4.0 | 35.3 |
| **B′. Themed (chosen)** | 7.3 | 7.0 | **8.0** | **7.0** | **9.0** | **6.3** | **8.0** | **52.7** |
| C. Open | 4.0 | **9.0** | 6.7 | 5.0 | 4.0 | 6.0 | 7.0 | 41.7 |

All three judges chose themed. Each also recommended deferring the shared
pool, merging the stacking caps, and grafting the strict model's per-kind
lean (as lint only) and card glyphs. All four grafts are adopted.

**Verdict: themed (B′).** **(agreed, Q1)**

| Model | Agreed rows it would touch |
|---|---|
| A. Strict | Armour 13.4 Domain column (split head and hands; back loses elements); possibly enemies 15.3 |
| **B′. Themed** | **Armour 13.4, the jewellery row's Domain cell only** (a pointer filled in, not a reversal; TK-1) |
| C. Open | Armour 5.5 bullets 1–2 |

### 5.5 Domains, lanes and shapes

**(agreed, Q1 for the domains and lanes; shapes lead's call)**

| Location (count) | Kinds | Domain: owns these subjects | Signature shape | Never here |
|---|---|---|---|---|
| **Head** (1) | helm, hood, circlet | **(settled, armour 13.4)** Senses, mind, crit riders. Read here **(lead's call)** as: senses (Search; Blinded and Revealed against you); mind (save advantage against one named mind or fear condition); crits and unseen hits against you attempt no rider; the psychic rider (Unfocused) | Passive: reacts to what is done to your mind and senses | Crit downgrades (armour's Proof and Adamant); "cannot be Surprised" (Perception's job, the lurker answer) |
| **Hands** (1) | gloves, gauntlets, bracers | **(settled, armour 13.4)** Grapple, handling, Administer. Read here **(lead's call)** as: grapple and escape; handling (Hand over, pickup); giving (Administer and other ally-targeted item use) | Passive cost or reach step: changes what your hands do for the party | Weapon swaps (Quickdraw), Retrieve, added cures, escape-check advantage (Barbed Studs) |
| **Feet** (1) | boots | **(settled, armour 13.4)** Movement, Prone, hazards. Read here **(lead's call)** as: movement verbs and the movement cost of standing, Prone, Teetering and hazard edges, Disengage economy; Jumping over crossable hazards (Later) | Passive or `fight` minor use: reacts to ground and footing | Speed of any kind; Shove-proofing (Sure-Footed, Deep Stance) |
| **Back** (1) | cloak | **(settled, armour 13.4)** Elements, stealth. Read here as: the element riders back owns under 5.7 **(agreed, Q1)** (Burning, Smouldering, Chilled); stealth (Hide, staying Hidden) and shake-off economy **(lead's call)** | Passive: reacts to weather and being seen | Always-on resistance (Resistant); Muffled and Shadowed copies; roll mode against attacks; Corroded |
| **Neck** (1) | amulet (torc, locket, pendant) | **Life and the body:** the `poison`, `bleed`, `disease` tags; Withered; getting a Downed companion up; one element damage event; prevention for one adjacent ally against a non-control minor, limited **(lead's call)** to the neck's own subjects (Poisoned, Bleeding, Withered; Diseased when it ships) | Automatic prevention or blunting: keeps you going | Healing amounts; Scarred; concentration (Later candidate); mind and fear (head's in M2); any control tag as a whole; another location's riders, for yourself or an ally |
| **Finger** (2) | ring (band) | **Your strike:** one damage part of your own damaging attack or ability (type shift with rider flag; rider flag on its own type); a declared +1 reach option; the cost of Help, the action that sets up an ally's strike (Sigil) | Automatic channel charge on your own use, or an option you declare with it: changes the strike you are making | Added damage; class pools, slots or spells; movement or teleport; defence of any kind; buttons in M2; Study (head, Later) |
| **None** (2) | charm (talisman, knot, token) | **Stored moments:** Warded and Helped from the charm's own pool or trigger | Button or once-per-fight trigger: a moment you choose when to spend | Fight-start automatic Warded (Warding Lining); save rerolls (Inspired, Later); coin |
| **None** | capacity | **Carrying:** +1 or +2 pack slots **(settled, items design 5.4)**; one capacity trinket counts per unit, so trinket capacity is at most +2 per unit (7.1) **(lead's call)** | No combat line | Belt or trinket slots |

### 5.6 Authoring lean and display kinds

**(lead's call)**

- **Authoring lean (graft from strict; a lint warning only, never a
  validation failure):** helm → crit riders; hood → senses; circlet →
  mind; gauntlets → grapple; gloves → handling; bracers → giving and
  reach. The agreed 13.4 row and the approved example names stay
  unchanged, so Warden's Coif (a hood) keeps a mind verb.
- **Talismans, knots, lockets, tokens, torcs, pendants and bands** are
  display names and silhouettes under the existing `charm`, `amulet` and
  `ring` kinds. **No new kind enums**; `kindToLocation` is unchanged.

### 5.7 Rider ownership

**(agreed, Q1: signed off by the client; TK-2)**

Each element rider's condition has exactly one owning location:

| Rider condition (element) | Owner |
|---|---|
| Burning and Smouldering (fire), Chilled (cold) | Back |
| Blinded and Revealed (radiant), Unfocused (psychic) | Head |
| Withered (necrotic) | Neck |
| Corroded (acid) | None in M2: it writes AC (6.2) |

Agreed 13.4 gives back "Elements"; this rule reads it as "the riders of
the elements back owns", which is why it went to the client. Smouldering
and Revealed complete the table under the same rule (each follows its
element's owner) **(lead's call, a reading of the agreed rule)**.
Ownership binds every answer to a rider condition, for yourself or for an
ally (Kinward, 13.5): a property in another location's table that names a
rider condition it does not own fails rule 15. Rings are not answers:
they apply riders, so Ember and Frost (finger) are not owner breaches.
**(lead's call)**

### 5.8 The client's two examples

**"Rings only magical effects."** Yes, made class-neutral. A ring changes
the attack or ability you are making: one damage part's type and rider
(Ember, Frost), its rider flag (Whetstone), its reach when you declare it
(Longreach), or makes Help, which sets up an ally's strike, cheaper
(Sigil). It works on weapon attacks, cantrips and
spells alike, and never grants spells, slots or class pool uses. A fighter
wants rings as much as a caster.

**"Gauntlets give resistances."** Three readings were weighed:

| Reading | Meaning | Verdict |
|---|---|---|
| R-a: always-on element resistance on gauntlets | Moves or copies Resistant (`props.armour_major`, armour 14.3); a value-2 line on a piece | **Rejected:** breaks the one-table rule and the piece rule (armour 5.5), and makes resistance a hands tax |
| R-b: blunt one element damage event per rest | A value-2 major, so jewellery only | **On the amulet** (Emberward Amulet, 13.5) |
| R-c: the element's rider does not stick | Save advantage against Burning or Chilled | **On the cloak** (Weathered Cloak, approved). The card says "riders", never "resistance" |

Gauntlets stay grapple (Grappler's Gauntlets, approved). **(agreed, Q1)**

## 6. Allowed and forbidden effects

### 6.1 Allowed effect kinds

Each only inside its owning table (5.5). **(lead's call)**

1. **Save advantage against one named condition, or one non-control tag**
   (`poison`, `bleed`, `disease`). Never a control tag as a whole (`mind`,
   `fear`, `restraint`, `prone`, `sight`), never "all Will saves".
   Precedent: Unshaken, Barred, Weathered.
2. **A prevention charge at the gear-prevention step** (between conditions
   pipeline steps 3 and 4; AR-51; order in 7.3): one named minor or major
   condition, or one non-control tag, for yourself; or, neck major only,
   for one adjacent ally against the neck's own minor subjects (5.5, 5.7).
   Never a hard condition. Precedent: Unbowed, Ringed.
3. **One cost step on a universal action** (action → bonus action, or bonus
   action → free interaction), or **one movement-cost step** (half your
   speed → 1 square) on standing from Prone or shaking off, as a passive
   verb, a `fight` minor use or a `rest` charge.
   - Eligible: Administer, escape (Grappled, Restrained), standing from
     Prone, Disengage, Help, Study (Later), getting a Downed companion up,
     shake-off.
   - Not eligible: Attack, Dash, Hide (Shadowed is armour), Dodge (held:
     echoes the SRD monk's Patient Defense), Shove or grapple, Frighten,
     Rally, Retrieve (`packInCombat` is a client decision), Treat (cure
     pricing).
4. **One reach step on Hand over or Administer** (adjacent → within 2
   squares in line of sight). A reach step and a cost step from different
   items may apply to the same action.
5. **A charged type or rider shift on your own damaging use** (a
   channel): one part becomes a named element and is rider-flagged, or
   carries its own type's rider flag. Zero added damage (9.4). Acts on
   "any damaging attack or ability you make", never on a class ability by
   id or tag.
6. **Granting Warded or Helped** from the trinket's own pool or trigger,
   to you or one adjacent ally.
7. **Movement verbs** (feet only): costs to stand, hazard-edge and landing
   saves, Disengage economy; Jumping over crossable hazards Later. Never
   squares of speed.
8. **Advantage on one in-combat skill use inside the domain:** Search
   (head), Hide (back).
9. **Tag-keyed text** ("while concentrating", "while wielding a shield")
   only if at least 2 launch classes can meet it.
10. **Capacity:** +1 or +2 pack slots, capacity kind only. **(settled)**
11. **A declared reach option** (finger only): one melee weapon attack or
    touch-range damaging ability you make gains +1 reach, chosen when the
    use is declared (Longreach, 9.4). Not a channel.
12. **A rider stop on hits against you** (head only): a hit against you
    attempts no rider in one named case (Visor: a critical hit; once per
    fight, an unseen attacker). Never a crit downgrade.
13. **Element damage-event blunting** (neck major only): the fire, cold or
    other named element parts of one damage event against you move one
    ladder step toward resistant, within the Emberward bounds (13.5).
14. **Stealth persistence** (back only): you stay Hidden after one named
    event (Duskhem), subject to the lead's check against Hidden's end
    rules and Shadowed (23).

Items 11–14 were used by M2 lines without being listed; they are added
here so that rules 15 and 16 can check them. **(lead's call)** Outputs
are minor or token conditions only, never an AC-writing condition
(rule 24).

### 6.2 The trinket denylist

A closed list on `effectKinds[]` (rule 16). **(lead's call unless
marked)**

| Never | Bound it protects |
|---|---|
| AC of any form, including reaction and conditional **(settled, armour 5.5)** | The AC envelope; the 6.4 guards at the lowest legal AC |
| Any condition that writes AC as an output (Corroded); Corroded is also left out of trinket preventions and cost steps in M2 | Removes the two-way reading of armour 5.5 without changing it (F6) |
| Roll mode on attacks against the wearer | Armour and shields own it; roll-mode cap; E+ can never add a source |
| Attack bonus; always-on advantage on your attacks | Buff test (items 12.4); the 65% hit assumption |
| **Added damage of any kind:** flat, dice, weakness bonus on shifted parts, retaliation, arc damage **(agreed, Q4)**. "Added damage" means any change to the damage of the hit itself: dice, flat amount or ladder step. Damage dealt later by a rider condition's own ticks (Burning, Bleeding) is the condition's, not the trinket's, and is counted in the precision ceiling report (11, item 6) **(lead's call)** | Precision parity (weapons 6.4); Techniques' "+1d6 on hit"; Reprisal is armour |
| Ability modifiers, including "set to" and "up to the cap" | Narrows items 12.4; gear Strength would move pack capacity (items 5.4) |
| Max HP; temporary HP (Bolstered is Later) | Dead number; Scarred is the client's cost of a downing |
| Speed, flat or conditional; Dash cost steps **(settled, stat model 6 Speed row, agreed)** | Items are not a listed speed source |
| Flat save bonus; advantage on a whole defence | Buff test; the "cloak of protection" must-have |
| Always-on damage resistance | One-table rule; Resistant is `props.armour_major` |
| Condition-tag immunity | Monster immunity budget (conditions 12.2); the enemies 17.1 Party's answer |
| Granting or extending Steeled, Unyielding, composure or Rallied; shortening hard conditions | Conditions 7.2 anti-lock guards |
| Applying hard or major conditions; offensive morale (Frighten economy, Demoralise, Rout) | Hard control only from limited abilities (conditions 7.1); the morale engine |
| Extra actions, turns or attacks; a second reaction; two cost steps on one action | Nova anti-goal; Surge and Extra Attack are class powers |
| Class pools: +max, refill, refill-tag change, "not spent on a miss" | Free-refill anti-goal; capacity and timescale offers |
| Training, mastery, spells known, concentration proficiency | Offers and classes own access |
| Class-list or scroll spells; slot-free or bonus-action casting; wand or scroll recharge | Scrolls and wands own item spells; focus tables own casting shape |
| Healing amounts, regeneration, heal on hit or kill, extra cures, Recovery, supply, Slot Recovery | Coin peg (items 10.5); Second Wind; Hungering is a weapon minor; tonic pricing |
| Scarred | Client decision |
| Retrieve or any pack-in-combat change | `packInCombat` is a client decision (items 5.1) |
| Coin, prices, Sell, loot draws, rerolls, smart-loot tags, family caps | Fixed loot budget; grind anti-goal; Leadership pricing |
| Identification, Arcana thresholds | Arcana's agreed job; camp identifies free (client decision) |
| Trinket-slot or belt-slot grants; doses; stack sizes | Belt maximum 3 through features or offers only; E+ is the lead's lever |
| Passive party effects, auras, `grants.whileCarried` | Narrowing for M2 (section 10) |
| Crit downgrades | Armour's crit cap |
| Acting on a class ability or pool by id or tag | Upgrades are offers (ability model 10.2) |

## 7. Caps, suppressions and runtime limits

### 7.1 The caps

**(lead's call unless marked)**

| Cap | Rule | Status |
|---|---|---|
| **Reaction cap** | One reaction-granting property per unit across armour, shield and trinkets; slot-order suppression `ReactionCap` | **(settled, armour 5.5)**, extended: an automatic trigger that fires off the wearer's turn to protect or affect **another unit** (Kinward, Heartward) counts as reaction-granting for the cap, though it spends no reaction. Self-only automatic preventions stay exempt (TK-3) |
| Roll-mode and crit caps | Untouched: trinkets have no roll-mode effect against the wearer and no crit downgrade | **(settled)** |
| **`ButtonCap`** | At most 1 trinket-granted button per unit; extras greyed by slot order, with a pre-equip verdict ("Ward-Knot would be greyed: one trinket button"). E+ never adds a button | **(agreed, Q3)**; TK-3, TK-10 |
| **`DuplicateAnswer`** | Among a unit's worn trinket properties **and its armour and shield properties**, at most one property answers each key. Keys are read from each property's `answers[]` field (17.1), never inferred. **Prevent keys** are pairs (scope, subject), scope `self` or `ally`, subject a condition id, a non-control tag, a control tag or an element; only the effect kinds `preventCondition` and `saveAdvantage` declare them. A tag key collides with every condition id that carries that tag (`self:poison` collides with `self:Poisoned`); `self:Withered` and `ally:Withered` do not collide. **Identity keys:** every property also answers its own property id, so a second copy of the same id (two Aid lines, two Ember rings) is greyed and its pool is never spent. **Capacity** is one key, so only one capacity trinket counts per unit. Armour and shield win (they come first in slot order); extra trinket lines are greyed `DuplicateAnswer`, never refused. Cost steps and channels have no prevent keys; they are bounded by the identity key and the per-use rules below **(lead's call)** | TK-3, TK-10 |
| **One channel per use** | At most one trinket channel per **use**, from all worn trinkets. A use is one attack roll or one ability resolution: each attack of an Attack action (Extra Attack, a Nick or off-hand attack) and each opportunity attack is its own use. Only the wearer's `UseAbility` resolutions and weapon attack pipelines are eligible; never damage-over-time ticks, rider damage, `onApply` effects or gear retaliation. Candidate rings are evaluated in trinket slot order; the first whose policy, filters and part rule all pass fires, and later rings are not evaluated or spent **(lead's call)** | Rule 23 |
| **Trinket wards never stack** | A trinket-granted Warded has no effect on a unit already Warded (no refresh, no intensity). The button is greyed `AlreadyWarded` on that target | TK-10, TK-22 |
| **One trinket Warded per fight** | A unit receives at most one trinket-granted Warded per fight, from any source (its own Wardstone, an ally's Ward-Knot). A second is greyed `AlreadyWardedThisFight` on that target **(lead's call, new)** | TK-10, TK-22 |
| **One received trinket prevention per round** | At most one trinket property, from any wearer, stops a condition on a given unit per round; others are not spent. The counter and its order are defined in 7.3 | TK-21 |
| **Ally preventions yield to wards** | An ally prevention (Kinward, Heartward) yields only when step 4 would actually spend a Warded charge on this application: the target holds Warded, the condition is ward-eligible (not lingering, Downed or Surprised), and no ward has been spent on this target in this use. The test is the same function step 4 uses. Otherwise the ally prevention may fire **(lead's call)** | TK-21 |
| **One trinket Helped per round** | A unit gains at most one Helped per round from a trinket source (Trophy Tally; a Sigil-cheapened Help counts). A Sigil variant whose Helped would be blocked is hidden and its reason shown as `TrinketHelpedLimit` | TK-10, TK-21, TK-22 |
| **One cost step, one reach step** | Never two cost steps on one action from any mix of sources (Deft Gloves plus a rolled Administer step); one reach step and one cost step may combine | Rule 23 |
| **Combat-first** | Every trinket's `obvious` property binds a combat hook; capacity is exempt | Rule 17 |

Slot order for every suppression is fixed: body armour, the shield in the
active set, trinket slot 1, trinket slot 2 (the `ReactionCap` machinery,
armour 5.5). Suppression is never a refusal: the equip is legal and the
suppressed line is greyed with its reason. **(settled pattern, armour
5.5)**

### 7.2 Dormant lines

**(lead's call)** Verdicts and suppressions ignore dormant properties
(they do not function, items 11), so a verdict never leaks a hidden line.

**When suppression is recomputed (lead's call, new):** by strict slot
order (7.1), on these events only: equip or unequip, `ItemRevealed`, and a
weapon-set swap (it changes "the shield in the active set"). A reveal
during combat (items 11 step 5: passive Arcana rising through an effect or
a recruit) shows the line at once, but its suppression recompute is
deferred to fight end, so a line that was working never greys mid-fight.
Counters (`fightUses`, `spent`, the per-round and per-fight flags) belong
to the property, not to its active status, and persist across a
recompute. Any newly greyed line is announced in the log and on the card.

### 7.3 Gear-prevention order

**(lead's call, new; TK-21)** Inside the gear-prevention step (between
conditions pipeline steps 3 and 4), candidates are tried in this order;
the first that fires stops the condition and the rest are not evaluated
or spent:

1. Armour and shield verbs on the target, in slot order (Ringed, Unbowed).
2. The target's own trinket preventions, in slot order (Graveward).
3. Ally preventions (Kinward, Heartward), in ascending unit order of the
   wearers, and only if the yield-to-ward test (7.1) is false.

- **The per-round counter** ("one received trinket prevention") covers
  only steps 2 and 3. It is checked before any policy, filter or "ask me"
  evaluation, and is set only by a successful stop. A Warded charge spent
  at step 4 and Emberward (a damage step, not a condition stop) never
  touch it.
- **Triggers:** every gear prevention, armour and trinket alike, acts on
  "a condition would land": after a failed contest or an automatic
  landing (a weak-target rider, a failed skill-check contest). Kinward's
  trigger is `allyConditionWouldLand`, the same semantics.
- **Entry points:** the gear step runs wherever the ward runs and skips
  wherever the ward skips (conditions 9.2 entry-point table: `onApply` and
  `onEnd` relations, a Staged last stage and engine grants skip it). After
  a transform or downgrade, the step 7 re-check reruns the gear step with
  steps 2, 4 and 5, so a transform result such as Withered meets
  Graveward and Kinward. Terrain with no contest enters at step 2 and
  meets the gear step as an automatic landing.
- **Outside combat** there is no initiative round: each condition
  application event is its own round, and the per-round counters reset
  before each. The per-fight limits do not apply outside combat, where
  trinket buttons cannot be used (9.2).

## 8. Rarity and power budget

**(settled: value equals the tier budget exactly, items 12.5; bands scale
charges only)**

### 8.1 Value units

About one action's worth each. **(lead's call)**

| Shape | Value |
|---|---|
| Passive verb with a named trigger, or a `fight` minor use (AR-6) | 1. Exception: Visor (a passive crit branch plus a `fight` unseen branch) is one value-1 property (lead's valuation, 13.1) |
| `rest` charge of a value-1 effect, **1 / 1 / 2** by band *(illustr.)* | 1 |
| `rest` charge of a major effect (element blunting, ally prevention, ally Warded), **1 / 1 / 1** at every band | 2 (jewellery only) |
| `camp` 1 charge | 2; Rare only; a camp spend (ability model 6.4); none in the M2 roster |
| Granted button | Counted once, inside the property whose effect is the grant (TK-12) |
| Capacity | +1 pack = 1; +2 pack = 2 (TK-12) |

### 8.2 Rarity and feel

**(agreed, Q2 for the tiers; feel lead's call)**

| Rarity | Composition | Feel | Examples |
|---|---|---|---|
| **Common (1)** | One obvious line. A piece's Common is its location verb | **"A habit":** one small rule you feel every fight or two. No hidden line | Strider's Boots, Antidote Locket, Ember Ring, Wardstone |
| **Uncommon (2)** | Obvious line + one roll from the same table, **or** one jewellery major alone. May hold the one reaction-shaped trigger. The rolled line may be the one dormant property; the obvious line never is | **"A plan":** two lines, or one planned moment you build a turn around | Kinward Torc; an Ember Ring rolled with Aid |
| **Rare (3): amulets only, floor 3+, at most 1 Rare trinket per floor, never a boss signature** | A major + one rolled minor; the minor may be dormant | **"A signature jewel":** the late find that matches Rare weapons and armour. Never fundamental maths | Heartward Torc |
| Very rare | None | — | — |

- Neck count is 1, so a unit holds at most one Rare trinket by
  construction. **(agreed, Q2)**
- Rings cannot be cursed until the curse design (Later) asks for Rare
  rings, which would reopen Q2 with the client. **(agreed, Q2)**
- `trinketRarePerFloorMax` (17.2) is the client's decision and is not
  tuning. **(lead's call)**
- **In M2 the limit is effectively one per run.** Heartward Torc is the
  only Rare family and the trinket family cap is 1 per run (items 12.2),
  so a run holds at most one Rare trinket. `trinketRarePerFloorMax`
  binds only once a second Rare family exists; the generator check stays
  (19.2 invariant, TK-27), and the simulator reports the Rare-found rate
  per run and the Rare offer and pick rate per floor (18.3). A second
  Rare neck family is a Later option (24.3). **(lead's call)**

### 8.3 Power checks

- Piece against same-rarity jewellery: within 1 value unit **(settled,
  armour 17.3)**. Rare amulets are reported separately (TK-4).
- Every trinket fires or prevents at least once per 3 fights for its
  wearer **(settled for pieces; extended to all trinkets)**. Capacity
  trinkets are exempt (2, Goals); the trigger rate is also reported per
  short-rest interval, because short rests are limited by supply (main
  design 4.4) and a `rest` charge is felt per interval, not per fight.
- Carried-eligible trinkets: carrier power ÷ price 90–110% **(settled,
  items 19.1 rule 7)**.

## 9. Passive, charged and granted

### 9.1 Shapes

**(lead's call)**

| Shape | Where | Rules |
|---|---|---|
| **Passive** | Mostly pieces | Event-shaped, with a named trigger and a mandatory `previewHint`. Never a number. Never an always-on copy of another table (one-table rule) |
| **Charged, automatic** | Neck, finger, some pieces | Prevention or channel charges with a data policy. Defaults: condition preventions `always` with target filter `nonMinion`; element blunting `always` with `nonMinion` and `bigHit` (13.5); channels `always` with the default filters `nonMinion`, `notResistant` (never shift onto a target known to resist or be immune to the new type) and `noCancelOwnSide` (9.4). Channels also carry the mandatory `hostileOnly` filter and the built-in precondition `riderWouldBeAttempted` (9.4), which the player cannot change. The player may change any default to "ask me" (ability model 7.3). Automatic `fight` uses of preventions and channels always fire |
| **Cost-step, reach-step and declared uses** | Pieces, rings, Lifeline | A `fight` or `rest` use that changes an action's cost or reach (Runner, Oilskin, Lifeline, Aid, Longreach) is offered as a separate action variant beside the normal one ("Shake off (Oilskin, 1 square)") and spent only when the player or AI picks that variant. Never automatic. A passive verb with no pool (Strider, Deft, Catcher) simply applies **(lead's call)** |
| **Granted (button)** | Charms only in M2 | At most 1 per unit (`ButtonCap`). Original trinket-list abilities only. Usable only in combat, after initiative is rolled. They need no difficulty of their own: trinket outputs carry no save-or-suffer control |

### 9.2 Granted buttons

**(agreed, Q3)**

- At most **one** trinket-granted button per unit, from any mix of worn
  trinkets, whatever the slot count (E+ included).
- In M2 only charms carry buttons. Rings, amulets and pieces never do.
- The button is a property whose effect is the grant; validation rule 19
  checks it and its value is counted once (TK-12).
- Ability model 10.2's eight-limited cap still exempts item abilities;
  `ButtonCap` is what bounds them (TK-20).
- In combat only the one trinket button and its charge pips appear on the
  action bar.
- **Combat only (lead's call, new):** a trinket button can be used only
  after initiative is rolled, so a Warded cannot be stored before opening
  a known boss or elite door (that would recreate the cut fight-start
  Warded charm, 13.9).
- Charm abilities are a planned area for growth: a future design pass on
  "stored moments" is an open item (24.2).

### 9.3 Pools

**(lead's call; TK-13)**

- Trinkets use `rest` and `fight` by default. `camp` only on Rare.
- **`never` pools are banned** on trinkets: single use belongs to
  consumables.
- Trinket `fight` uses are tracked in the armour 16.3 `fightUses` state,
  not in `pools[]`, whose refill enum has no `fight` (TK-7, TK-18).
- Every trinket `fight` use is a minor use under ability model 6.2 (TK-18).

### 9.4 Rings: channels and zero added damage

**(agreed, Q4 for zero added damage; the rest lead's call)**

- **Zero added damage, literally.** A ring-shifted part never gains the
  weakness bonus: against a target weak to the new type it gains no
  weakness bonus (it resolves as below). The weakness still triggers that type's rider
  automatically (ability model 9.1). A trinket never adds dice,
  retaliation or arc damage. The weapon Edge keeps its weakness bonus
  (weapons 18.2), so rings are a rider and control tool, not a second
  Edge. **(agreed, Q4)**
- **How a shifted part resolves (lead's call, new).** The shifted part's
  damage is resolved exactly as the unshifted part's would be: at the
  target's true ladder step against the part's **original** type, layers
  and shells included, and it never counts as a layer's breaker type
  (stat model 8.5). The new type is used only for the rider (the 9.3
  choice, the automatic weak-target landing) and for `damageType` ends
  (conditions 18.6), subject to `noCancelOwnSide`. So a part the target
  resists stays resisted (no buy-through: the cut Shifting Band stays
  cut, 13.9), a part the target is weak to keeps only its original
  weakness, and resistance or immunity to the new type never deletes
  damage. Damage after the shift always equals damage before it. "Weak"
  here covers trait weakness and condition ladder shifts (Oiled, Soaked)
  alike.
- **Oiled and Soaked (lead's call, new; TK-25).** A fire-shifted part
  never pays out Oiled: no +half, no band-higher Burning, Oiled is not
  consumed and no oil square burns away. Because Oiled makes the target
  weak to fire, Burning lands as an ordinary automatic rider at its
  normal band, and a later true fire hit still pays Oiled out. A
  fire-shifted part on a Soaked target ends Soaked and attempts no Burning
  rider (conditions 9.3 order exception), so `riderWouldBeAttempted`
  fails and the ring does not fire. A cold-shifted part on a Soaked target
  is weak-typed: Chilled lands automatically, never Frozen (a ring is not
  a `freezing` source).
- **Which part (lead's call, new):** a channel acts on the first damaging
  part, in data order, that it would change: for a type shift, a part
  whose type is not already the ring's element and to which the target is
  not, as far as the party knows, weak; for Whetstone, an eligible part
  not already rider-flagged. If no part qualifies, the channel does not
  fire and no charge is spent.
- **Which target (lead's call, new):** a channel shifts the part for **one
  target per use**. Targets are taken in the order ability model 14.3
  step 4 locks them ("targets locked, in unit order"). The channel is
  decided inside the step 5 per-target loop, after that target's attack
  roll or save and before its damage: it goes to the first target that
  takes damage from this use and passes every filter, the precondition
  and the part rule. Once it fires, later targets in the loop are not
  reconsidered; other targets of the same use resolve unchanged. This
  keeps rings from rider-flagging whole areas.
- **Filters and precondition (lead's call, new):**
  - `hostileOnly` (mandatory, not player-editable): the target is hostile
    to the wearer. Areas hit allies unless `selective` (ability model
    8.2); a ring never shifts a part against an ally caught in one.
  - `riderWouldBeAttempted` (built-in precondition): evaluated as if the
    ability model 9.3 selection ran with the shifted part, it requires
    that the ring's part would be the chosen rider for that target (no
    weak-typed part and no earlier flagged part, an Edge or Honed part,
    takes the slot first), that the target lacks that rider's condition,
    and, for an at-will, that the once-per-turn rider slot is free. If it
    fails, the channel does not fire and no charge is spent. Whetstone on
    a part whose type the target is already weak to fails it (the rider
    would land anyway).
  - `noCancelOwnSide` (default, "ask me" may override): the channel does
    not fire if the new type would end, cancel or transform a harmful
    condition on the target whose source is on the wearer's side (fire
    ends Chilled, Frozen and Soaked; cold ends Burning and Smouldering;
    Burning and Chilled cancel). A shifted part never ends Oiled (above),
    so a party-Oiled target is fair game. So an Ember Ring never thaws
    the party's Frozen enemy, and a Frost Ring never puts out the party's
    Burning.
  - `nonMinion` and `notResistant` (defaults, as 9.1).
- **Knowledge (lead's call, new):** for a party wearer, the filters, the
  precondition and the part choice read party knowledge, the same record
  the preview uses (ability model 8.3). A ring's fire-or-skip decision
  therefore never differs between the preview and the resolution, and a
  skip never reveals a hidden trait; a non-fire emits no event that names
  one. The core then resolves damage and riders on the truth. A
  resistance, immunity or weakness that the shifted part meets and the
  party did not know is revealed under the settled "seen in play" rule,
  exactly as any hit would reveal it. Monster carriers read the true state
  (the AI reads truth except positions, enemies 11.2).
- **Preview:** the preview shows the shift with what the party knows
  (ability model 8.3).
- **One channel per use** from all worn trinkets, by slot order (7.1).
- **Longreach is not a channel (lead's call, new).** Reach decides whether
  a target is legal (ability model 14.3 step 2), long before damage. A
  Longreach use is a declared option: the attack is offered with +1 reach
  only when the target is beyond the wearer's normal reach, the player or
  AI chooses it at declaration, and the charge is paid at step 3 (cost
  paid), whether the attack hits or misses. It does not take the
  one-channel slot, so Ember may still fire on the same attack.

### 9.5 Rider saves

**(settled, ability model 9.2, 9.3)**

- A ring's rider uses the wearer's difficulty for that use, as Edge riders
  do.
- At most one rider per target per use; an at-will's rider at most once
  per turn. The ring's flagged part takes its place in the 9.3 order, and
  the ring fires only when that place would win (`riderWouldBeAttempted`,
  9.4), so a charge is never spent on a rider that cannot be attempted.

## 10. Trade-offs, party and ally effects

**(lead's call)**

- **No trade-offs in M2.** A verb may be situational ("while Hidden"); that
  is a trigger, not a cost. Curses (Later, Rare+, items 13) bring
  drawbacks: only the Rare amulet (and Later charms, if Q2 is reopened)
  can host one.
- **No passive party effects, auras or `whileCarried` in M2.**
- **Ally effects are wearer-triggered:** they spend the wearer's action,
  bonus action or charge, and reach one ally per trigger (adjacent; within
  2 squares only for Hand over and Administer reach) (rule 22).
- **Off-turn ally triggers count under the reaction cap** (7.1).

## 11. Trinkets and class features

**(lead's call; binding on the class designs)**

1. **Class copy registry.** Class, subclass and companion features declare
   `effectKinds[]`, subject and trigger. A trinket property with the same
   effect kinds, subject and trigger as a class feature fails the build
   **at any charge level**. This is stricter than the weapons copy rule on
   purpose. It is **bidirectional**: a collision fails both records,
   whichever was authored first, and the lead resolves it by changing one
   or by an entry in the lead-owned `copyRegistryAllowlist` (17.2) that
   records the accepted pair and the reason (rule 18). The registry is
   empty until the class records land (23). It also covers species traits
   and armour and shield signature properties (for example Bulwark
   Stance) in every direction **(change CL-11; classes design 3.3)**,
   and weapon verb signatures **(change VG-18; Vanguard design 7.13)**.
   Class features never match armour or shield signatures. Payable
   lines prefer original effects. Echoes of verb signatures (for
   example Warding Staff) need a lead's ruling.
2. **Weapon, armour, shield and focus properties keep weapons 18.1:**
   charged echoes are legal (Ember Ring echoes Ember Edge; Longreach
   echoes Long Haft); always-on copies are not.
3. **Pre-cleared registry entries:** Runner's Boots (bonus-action
   Disengage once per fight) against any Cunning-Action-shaped feature is
   accepted: it is the only M2 entry in `copyRegistryAllowlist`. Dodge as
   a bonus action is held until the registry exists. Luckstone (a self Helped button) is cut as a copy of the SRD 5.2
   rogue's Steady Aim.
4. **Trinkets act only on** universal actions, conditions, item use,
   movement, their own pools and "any damaging use you make". Never on a
   class ability by id or tag, never on a class pool.
5. **No access:** no training, spells, slots, pool capacity or
   proficiency.
6. **Binding class requirements are proven with empty trinket slots:** the
   Dexterity precision parity (weapons 6.4) and the caster-defence AC
   requirement (armour 11, item 2). Plus a **ceiling report**: a precision
   class's damage per round with its best legal trinkets, rider-condition
   ticks included (6.2), is at most +10% *(illustr.)* over trinket-free at
   the 6.4 reference levels.
7. **No class locks.** Every trinket is useful to at least 2 of the 4
   launch classes or to any companion (rule 26). Each record declares
   `usefulFor[]`, which the lead signs off. `requires` tags steer, never
   lock.
8. **Classes may build on trinket outputs** (Warded, Helped, element
   riders), never on trinket kinds or ids.
9. **Companions** have fixed kits with choices at 3, 6 and 9, so trinkets
   are their main build lever. At most one Medicine- or Lore-dependent
   family per wave, steered by `requires`, so companions are not handed
   dead loot. Companion slot fill at the boss is reported.

## 12. Monster use

**(settled where marked; the rest lead's call)**

- **(settled, armour 5.9; enemies 15.2)** No trinket slots. A trinket uses
  the carried slot, benefit only. Only `armed` types have a carried slot.
- **(settled, enemies 15.4; weapons 15.2)** Pools map to uses: `rest` N and
  `camp` N → `uses:N`; `fight` → `uses:1`.
- **Conditions count:** a carried trinket's output conditions count toward
  the type's 2 harmful conditions (enemies 9.1, 9.2), so a type already at
  2 cannot carry an element ring (rule 25). Carriers read the true state
  for every filter (9.4).
- **Role answers:** every carried-eligible property declares `negates[]`,
  checked per carrier type by rule 25 (armour 8.1 rule 10 pattern). The
  only M2 carrier-type exclusion is Strider's Boots, never on brutes
  ("Prone it"). Container-only families need no exclusion: they never
  reach a monster, and a party-side negation of a monster's answer is not
  what armour 8.1 rule 10 protects. **(lead's call)**
- **Carrier legality (TK-23, at enemies 15.3 step 4):** a notable trinket
  is legal on a carrier only if, with the instance's actual rolled
  properties in place, the composed unit passes the enemies 6.4 guards
  (the round-1 kill guard included); its output conditions fit the type's
  2 harmful conditions (9.1, 9.2); a reaction-shaped property (Kinward,
  Heartward) is the unit's one reaction beyond the opportunity attack, so
  a carrier that already has one (a Guard verb, an armour reaction
  property) cannot take it; a carried button (Wardstone, Ward-Knot) is a
  bonus-action ability under the ≤1 cap and is the item's one limited
  ability beyond the limited cap; its role answers pass; and its
  `carrier{}` limits (17.1) hold. Any `ReactionCap`, `ButtonCap` or
  `DuplicateAnswer` conflict with the carrier's issue gear makes it
  illegal: monsters never hold greyed lines. If no carrier is legal, the
  item goes to a container in the same room (15.3 step 4, unchanged).
  **(lead's call)**
- **Banned pairs:** Wardstone and Ward-Knot are never on a carrier with
  the Warded or Unbreakable affix (enemies 17.2 bans Warded with
  Unbreakable; a carried ward would recreate the pair or add a second
  ward once the affix ward is spent), and a carried Ward-Knot's `useWhen`
  never targets such a unit. **(lead's call)**
- **Party-side opener rule:** no trinket property may negate a role's
  signature opener outright. Visored Helm's unseen branch is a
  once-per-fight rider stop; the opener's damage stands.
- **Every M2 combat family classified (lead's call):**

| Location | Carried-eligible (limits) | Container-only |
|---|---|---|
| Head | Visored Helm; Barred Helm | Warden's Coif; Watcher's Hood |
| Hands | — | Grappler's Gauntlets; Deft Gloves; Catcher's Bracers |
| Feet | Strider's Boots (never on brutes) | Ledgewalker's Boots; Runner's Boots |
| Back | Weathered Cloak | Grey Mantle; Oilskin Cloak; Duskhem Cloak |
| Neck | Emberward Amulet; Kinward Torc (elites; never in a boss encounter); Heartward Torc (elites, floor 3+; never in a boss encounter; an instance whose rolled line is Lifeline is container-only) | Antidote Locket; Graveward Amulet; Lifeline Pendant |
| Finger | Ember Ring; Frost Ring | Whetstone Ring; Sigil of Aid; Longreach Band |
| Charm | Wardstone; Ward-Knot (elites; never in a boss encounter) | Trophy Tally |
| Capacity | — | Porter's Satchel; Mule Strap |

  Carried-eligible: 11 of 28 combat families (39%, about a third). A
  carried ward or ally prevention counts toward the encounter's
  control-defence budget alongside boss resolve.
- **What the party gets:** each trinket record declares its
  `monsterProfile` (enemies 15.4, agreed: "each item declares a
  `monsterProfile`"; the 15.4 table itself is unchanged). For trinkets:
  passive and automatic lines apply inside the threat band; buttons are
  used through `useWhen`; the carrier's `uses` are its own count and never
  write the instance's `spent`, so the party receives the trinket as
  generated (`spent` 0), as for a property weapon. **(lead's call)**
- **(settled)** Carried trinkets count toward the per-encounter carried cap
  (1 on floors 1–2). No monster-only trinkets in M2.
- **Tell (graft from strict):** hover reads "Carries: amulet (life)".
  Using it reveals the item ("seen in play", settled).
- **AI:** monster carriers use the existing resolve-preview scoring; a
  carried button (Wardstone, Ward-Knot) needs a `useWhen`.

## 13. Catalogue

One roster. Every M2 entry uses only M1 or M2 conditions. All names
original and *(illustr.)*. "C" = Common, "U" = Uncommon, "R" = Rare.
Uncommon pieces add one roll from their location table. **(lead's call)**

### 13.1 Head (4 M2)

| Family | Kind | Line | Wave | Notes |
|---|---|---|---|---|
| **Visored Helm** | helm | **Visor:** a critical hit against you attempts no rider; also, once per fight (`fight` minor), the first hit from an attacker you could not see that would attempt a rider attempts none | M2 | **(change TK-2)** Unseen branch added as a `fight` minor so it meets the trigger rate without erasing the lurker opener's damage. "Could not see" means: at the attack roll the attacker is Hidden from you or you are Blinded. The `fight` use is spent only when a rider would otherwise be attempted and the passive crit branch did not already apply. Valued as one value-1 property (the lead's valuation; the exception to 8.1 noted there). Crits still double; never a downgrade |
| **Warden's Coif** | hood | **Unshaken** (approved): Frighten attempts against you have disadvantage; advantage on saves against Frightened | M2 | Container-only |
| **Barred Helm** | helm | **Barred** (approved): advantage on saves against Dazed | M2 | |
| **Watcher's Hood** | hood | **Vigil:** your Search has advantage | M2 | The party's lurker answer; trigger-rate watch; container-only |
| Shaded Circlet | circlet | Advantage on saves against Blinded | Later | First replacement if a head family is cut |
| Scholar's Circlet | circlet | Once per fight, Study is a free interaction | Later | Moved from finger (Study is senses, not your strike); depends on what Study reveals |

### 13.2 Hands (3 M2)

| Family | Kind | Line | Wave | Notes |
|---|---|---|---|---|
| **Grappler's Gauntlets** | gauntlets | **Grappler:** escaping Grappled or Restrained costs a bonus action | M2 | **(change TK-2)** Restrained added (webs, nets) for the trigger rate; matches the conditions `escape` cure. Container-only |
| **Deft Gloves** | gloves | **Deft** (approved): Administer costs a bonus action | M2 | Container-only |
| **Catcher's Bracers** | bracers | **Catcher:** Hand over and Administer may target an ally within 2 squares in line of sight | M2 | Reach step (6.1 item 4); container-only |
| Clenching Gauntlets | gauntlets | **Clench:** once per fight, a unit you grapple has disadvantage on its first escape check | Later | Renamed from "Holder" (behaviour-profile clash); `fight` minor, for the conditions 7.2 anti-lock spirit; container-only |
| Slinger's Bracers | bracers | Once per fight, throwing a belt flask, tonic or potion costs a bonus action | Later | **(change CN-22)** The flask throw and the potion throw at an ally are designed (consumables design 11) |

### 13.3 Feet (3 M2)

| Family | Line | Wave | Notes |
|---|---|---|---|
| **Strider's Boots** | **Strider** (approved): standing from Prone costs 1 square | M2 | Movement-cost step (6.1 item 3). Carried: never on brutes |
| **Ledgewalker's Boots** | **Ledgewalker:** your hazard-edge and landing saves have advantage | M2 | **(change TK-2)** F1; container-only |
| **Runner's Boots** | **Runner** (approved): once per fight, Disengage as a bonus action | M2 | Offered as an action variant (9.1). Pre-cleared on the class copy registry against Cunning-Action shapes (`copyRegistryAllowlist`); container-only |
| Wader's Boots | 1 / 1 / 2 per rest: entering a water or oil square applies nothing to you this turn | Later (feet reserve) | Recast as a charge (an always-on full prevention is outside 6.1) |
| Leaper's Boots | 1 / 1 / 2 per rest: Jumping over one crossable hazard as part of movement | Later (feet reserve) | Depends on main design 4.3 Jumping sources and on Athletics's agreed jump use staying useful |

### 13.4 Back (4 M2)

| Family | Line | Wave | Notes |
|---|---|---|---|
| **Weathered Cloak** | **Weathered** (approved): advantage on saves against one element's rider: Burning or Chilled in M2 | M2 | **(change TK-2)** Rider ownership (5.7); Corroded excluded. The element is fixed by the record, not the instance: two property records, `prop.trinket.weathered_burning` and `prop.trinket.weathered_chilled`, in `props.trinket.back` under one family, as armour Resistant (element) works. No new instance state **(lead's call)** |
| **Grey Mantle** | **Grey Mantle** (approved): Hide has advantage against a unit that has not seen you this fight | M2 | "Has not seen you this fight" reads the awareness record's per-fight `everNoticed[partyUnit]` flag (TK-26). Hide makes one check against several observer groups: roll two d20s once from the combat stream; the higher die is used against groups with `everNoticed` false, the first die against the rest. Trigger-rate watch; container-only |
| **Oilskin Cloak** | **Oilskin:** once per fight, shaking off costs 1 square instead of half your speed; a shake-off paid this way does not end Corroded | M2 | `fight` minor; a movement-cost step offered as a variant ("Shake off (Oilskin, 1 square)") beside the normal shake-off (9.1), so a player who needs Corroded ended picks the normal one |
| **Duskhem Cloak** | **Duskhem:** once per fight, when your attack made while Hidden drops its target to 0 HP, you stay Hidden from every hostile group whose `everNoticed` flag for you is false | M2 | Stealth persistence (6.1 item 14); checked against Hidden's end rules and Shadowed before data (23); container-only |
| Groundsilk Cloak | 1 / 1 / 2 per rest: one lightning arc or Burning spread skips you | Later (back reserve) | Charged; no redirect |
| Smoke Cloak | When you Hide, gain Veiled until your next turn ends (1 / 1 / 2 per rest) | Later | Needs Veiled; automatic, because pieces never carry buttons |

### 13.5 Neck (6 M2)

| Family | Rarity | Line | Wave | Notes |
|---|---|---|---|---|
| **Antidote Locket** | C | **Antidote:** advantage on saves against `poison`-tag conditions | M2 | Sealed reworded (TK-6, F2). Overlaps Inured (Later) as Weathered overlaps Resisting (approved precedent). Container-only; trigger rate reported per floor theme |
| **Graveward Amulet** | C | **Graveward:** 1 / 1 / 2 per rest, Withered does not land on you | M2 | Gear-prevention step (7.3). Container-only; trigger rate reported per floor theme |
| **Lifeline Pendant** | C | **Lifeline:** once per fight, getting a Downed adjacent companion up (Medicine, trained) costs a bonus action | M2 | `fight` minor (counts toward Medicine's once per companion per fight; death and healing design 11.2), offered as an action variant (9.1). `requires: skill.medicine.trained`; a warning verdict when the wearer is not Medicine-trained (15); companions only (a hero downing ends the run); container-only |
| **Emberward Amulet** | U (major) | **Emberward:** 1 / 1 / 1 per rest, the fire parts of one damage event against you resolve one ladder step toward resistant; never past resistant, never to immune, and never combined with another step source on the same event (the strongest applies) | M2 | The client's "resistance", reading R-b. Bounds **(lead's call, new)**: acts on all fire parts of the event together; `bigHit` is true when the sum of the fire parts, after crit doubling and before any ladder step, is at least `FloorDiv(maxHP × bigHitPermille, 1000)`; the charge is spent only if the final ladder step of some fire part changes (as Shielded's "used only if it changes something"). A fire Resistant source in an earlier slot (Resistant armour) draws the warning verdict `DeadLine`, which is not a suppression |
| **Kinward Torc** | U (major) | **Kinward:** 1 / 1 / 1 per rest, automatic: when Poisoned, Bleeding or Withered would land on an adjacent ally (a failed save or an automatic landing), it does not land | M2 | Narrowed to the neck's own subjects (5.5, 5.7): Burning and Chilled are back's **(lead's call)**. Trigger `allyConditionWouldLand` (7.3). Reaction-shaped (counts under `ReactionCap`); yields to a ward step 4 would spend (7.1); one received prevention per round |
| **Heartward Torc** | **R** (floor 3+) | Fixed Kinward + one rolled neck minor (Antidote, Graveward or Lifeline), which may be dormant | M2 | The only M2 Rare family. Kinward answers `ally:` keys and the rolled minors `self:` keys, so all three rolls stay legal under rule 28. Elites only as carriers (12; a Lifeline roll is container-only); Later curse host |
| Frostward, Stormward, Causticward, Dawnward, Duskward Amulets | U (major) | As Emberward for cold, lightning, acid, radiant, necrotic | Later | At most 2 element families per wave per lane; Causticward blunts damage only |
| Staunch Torc | C | Advantage on saves against `bleed` | Later | Dead under Ringed armour |
| Torc of Seasons | R | Element chosen at each rest | Later | Needs a new instance field (items 18.1); pushes bring-the-key scouting |
| Stillmind Pendant | C | Once per fight, your first concentration check has advantage | Later | Only if at least 2 launch classes concentrate; never with Steady Casting |
| Bulwark Locket | U | Rest charge granting Shielded before a damage event | Later | Needs Shielded; class copy check against defensive Techniques |

### 13.6 Finger (5 M2)

| Family | Rarity | Line | Wave | Notes |
|---|---|---|---|---|
| **Ember Ring** | C | **Ember:** 1 / 1 / 2 per rest, policy `always` (`hostileOnly`, `nonMinion`, `notResistant`, `noCancelOwnSide`; fires only if its rider would be attempted): when an attack or ability you make deals damage, one part deals fire instead and is rider-flagged (Burning). Its damage is unchanged (9.4). Against a fire-weak target Burning lands automatically | M2 | Charged echo of Ember Edge (weapons 18.1). Part, target, filters and the Oiled ruling in 9.4. Carried: Burning counts toward the type's 2. A verdict warns when the active weapon already flags a part first in 9.3 order (an Ember Edge or Honed weapon): the ring then fires only on other uses |
| **Frost Ring** | C | **Frost:** as Ember, cold, Chilled | M2 | Kiting and back-line protection; never puts out the party's Burning (`noCancelOwnSide`) |
| **Whetstone Ring** | C | **Whetstone:** 1 / 1 / 2 per rest: one part of your damaging use carries its own type's rider flag, only for slashing (Bleeding), fire (Burning), cold (Chilled), necrotic (Withered) and psychic (Unfocused) | M2 | Charged echo of Honed; a channel under 9.4 (same filters and precondition). Excludes bludgeoning and radiant (major riders), acid (AC write), lightning (arc damage), piercing (always-on already). A verdict warns on a Honed single-part weapon, where it never fires; container-only |
| **Sigil of Aid** | C | **Aid:** once per fight, Help costs a bonus action | M2 | Cost step, not a button, offered as a variant (9.1); hidden with reason `TrinketHelpedLimit` when its Helped would be blocked. Help sets up an ally's strike, which keeps it in the finger lane **(lead's call)**; container-only |
| **Longreach Band** | C | **Longreach:** 1 / 1 / 2 per rest, when you declare a melee weapon attack or touch-range damaging ability against a target beyond your normal reach, you may give it +1 reach (maximum 2, or 3 on a `minReach` 2 weapon); the charge is paid on declaration, hit or miss | M2 | A declared option, not a channel (6.1 item 11, 9.4). Charged echo of Long Haft; melee and touch casters; container-only |
| Storm, Grave, Dawn Rings | C | As Ember for other elements | Later | 2 element rings per wave. Storm needs a ruling (arc damage breaks zero added damage); Dawn restricted to its Revealed branch (Hidden's targeting rule) |
| Seeker's Band | C | **Sightline:** once per rest, one ranged weapon attack ignores the adjacent-hostile disadvantage | Later | Charged echo of Seeking; dead on a Seeking weapon and for casters |
| Caustic Ring | C | Acid shift | **Blocked** | Outputs Corroded, which writes AC (rule 24) |

### 13.7 None: charms and capacity (3 + 2 M2)

| Family | Rarity | Line | Wave | Notes |
|---|---|---|---|---|
| **Wardstone** | C | **Wardstone** (button): 1 / 1 / 2 per rest, bonus action, combat only: you gain Warded (1). Greyed `AlreadyWarded` while you hold Warded, and `AlreadyWardedThisFight` once you have had a trinket Warded this fight | M2 | Self only. Warded blocks hard conditions (approved Warded text); the landing envelope gate checks bosses still land control |
| **Ward-Knot** | U (major) | **Ward-Knot** (button): 1 / 1 / 1 per rest, bonus action, combat only: one adjacent ally (not you) gains Warded (1). Greyed `AlreadyWarded` on a Warded ally and `AlreadyWardedThisFight` on an ally that has had a trinket Warded this fight | M2 | Distinct from Wardstone by target. The card says "stops the ally's next harmful condition, hard control included" |
| **Trophy Tally** | C | **Tally:** once per fight, automatic: the first time any damage event reduces a hostile to 0 HP while it is adjacent to you at the moment of the drop, you gain Helped (fallback `untilSourceTurnEnd`, you as source, as the Vex verb, W-7) | M2 | Renamed from "Hunter's Tally" (Hunter is an elite affix, enemies 17.2). Not spent if you already hold Helped or have had a trinket Helped this round. Container-only (no beast carriers) |
| **Porter's Satchel** | C | +1 pack slot | M2 | Capacity, value 1; no combat line, no dormant property. Renamed from "Porter's Charm" so no kind word ("charm", "knot") sits on the capacity lane |
| **Mule Strap** | U | +2 pack slots (the settled maximum) | M2 | Capacity, value 2. Renamed from "Mule Knot". Only one capacity trinket counts per unit (7.1). A fallback if it is never picked is held until the pick report triggers it; its table is chosen then |
| Bloodknot | C | Once per fight: first drop to half HP or below, gain Warded | Later | Held so M2 has at most two ward charms |
| Kinstone | C | Move a Warded charge between you and an adjacent ally | Later | Needs a `sourceTied` ruling; only if the ward non-stacking rule holds |
| Scrollwright's Token | C | Advantage on the next off-class scroll Arcana or trap Mechanics check | Later | Trigger-rate risk |
| Last-Breath Charm | R | 1 per camp: at fight start gain Death-warded | Later, retired | Retired (Later; nothing shipped changes): it duplicates Death-warded on a timer and the Reprieve Phial owns the extra-life fantasy (death and healing design 15.3, Q7) |
| Muse's Token | R | 1 per camp: gain Inspired | Later | Only with the Leadership-mastery owner's agreement; reopens Q2 |

All Later charm entries go through the charm-abilities pass (24.2) before
they are approved. **(lead's call)**

### 13.8 M2 roster count

| Location | M2 families | Binding floor |
|---|---|---|
| Head | 4 | ≥3 **(settled)** |
| Hands | 3 | ≥3 **(settled)**; reserves Clenching, Slinger's |
| Feet | 3 | ≥3 **(settled)**; reserves Wader's, Leaper's |
| Back | 4 | ≥3 **(settled)** |
| Neck | 6 (1 Rare) | ≥3 target, non-binding |
| Finger | 5 | ≥3 target |
| None: charm | 3 | ≥3 target |
| None: capacity | 2 | — |
| **Total** | **30** *(illustr.)* | Inside armour 19's "about 25–30"; a run (about 9 trinkets) sees about 30% |

**Offence** (changes the outcome of your own attacks or turns: channels,
reach, Helped, Hidden kills, the free Disengage): Ember, Frost,
Whetstone, Longreach, Sigil, Trophy Tally, Duskhem, Runner's: 8 of 30
(27%). **Recovery and tempo** (getting up, getting free, giving,
reviving): Strider's, Grappler's, Deft, Catcher's, Lifeline: 5 of 30
(17%). The earlier "13 of 30 (43%)" counted both as offence; the report
now keeps them apart (18.3). If offence picks fall below target, the
first Later reserves to promote are Seeker's Band, the Storm Ring once
ruled and the Smoke Cloak once Veiled exists. **(lead's call)**

### 13.9 Cut

| Cut | Why |
|---|---|
| Shifting Band (resisted part becomes an unresisted element) | Copies the agreed stat model 8.4 buy-through offer; deletes resistance as a monster tool |
| Luckstone (self Helped button) | Copies the SRD 5.2 rogue's Steady Aim |
| Doubled Knot (wards become 2) | Ward-stacking lock |
| Physician's Locket (Treat as bonus action) | Undercuts cure pricing |
| Guardring (rider saves missed by 2 or less fail) | A back-door +2 to difficulty |
| Steady Hands (Administered potion also ends Staggered) | An added cure |
| Ring of Passage (blink) | Movement belongs to feet |
| Ring of the Second Word (no slot spent on a total miss) | Free class-pool refill |
| Second Chance Token; Lucky Bone as a save reroll | Copy Inspired; replaced by Muse's Token, Later |
| Spurred boots | Reads as speed; copies Rally's token |
| Dodge as a bonus action | Echoes Patient Defense; held for the registry |
| Quickdraw, Retrieve and escape-advantage gloves; Slipping Bracers | Copy weapon or armour properties, or route around Retrieve |
| Tinder Ring; any damage or no-save ring button | Rings carry no buttons in M2 (Q3); no-save application at range |
| Fight-start automatic Warded charm | Copies Warding Lining |
| Knucklebone, Turn of Luck, Kindred Glint, Ward Echo, Watchful Clasp | Shared-pool entries; the pool is Later |
| Help-whistle charm | Duplicates Sigil of Aid |
| Steadfast-named items, Clearmind Circlet | Name clashes (enemies affix; Clear-minded condition) |
| Tag-wide "mind" or "fear" protections; a mind amulet in M2 | Control tags as a whole; amulet overload |

## 14. Property tables and example notables

### 14.1 Tables

All minors `entryValue` 1; majors 2 and jewellery only. **(lead's call)**

| Table | Entries (M2) | Rolled by |
|---|---|---|
| `props.trinket.head` | Visor, Unshaken, Barred, Vigil | Uncommon head pieces |
| `props.trinket.hands` | Grappler, Deft, Catcher | Uncommon hands pieces |
| `props.trinket.feet` | Strider, Ledgewalker, Runner | Uncommon boots |
| `props.trinket.back` | Weathered (Burning), Weathered (Chilled), Grey Mantle, Oilskin, Duskhem | Uncommon cloaks |
| `props.trinket.ring` | Ember, Frost, Whetstone, Aid, Longreach | Uncommon rings (rule 28: never a second channel line on a channel ring; Ember, Frost and Whetstone are channels, Aid and Longreach are not) |
| `props.trinket.neck` | Antidote, Graveward, Lifeline | Uncommon and Rare amulets |
| `props.trinket.neck_major` | Emberward, Kinward | Fixed only |
| `props.trinket.charm` | Wardstone, Tally | Uncommon charms (rule 28: never a second button) |
| `props.trinket.charm_major` | Ward-Knot | Fixed only |
| `props.trinket.capacity` | Pack +1 (value 1), Pack +2 (value 2) | Fixed only; no rolls in M2 |

No Rare-only tables in M2 (Heartward uses the neck tables).

### 14.2 Example notables *(illustr.)*

| Notable | Rarity | Lines | Value |
|---|---|---|---|
| Strider's Boots | C | Strider | 1 |
| Ashen Strider's Boots | U | Strider + rolled Runner | 2 |
| Watcher's Hood of the Bar | U | Vigil + rolled Barred (dormant until identified) | 2 |
| Ember Ring | C | Ember | 1 |
| Kindler's Ember Ring | U | Ember + rolled Aid | 2 |
| Graveward Amulet | C | Graveward | 1 |
| Kinward Torc | U | Kinward (major) | 2 |
| Heartward Torc | R, floor 3+ | Kinward + rolled Antidote (may be dormant) | 3 |
| Wardstone | C | Wardstone (button) | 1 |
| Tallied Wardstone | U | Wardstone + rolled Tally | 2 |
| Mule Strap | U | Pack +2 | 2 |

## 15. Display

**(lead's call)**

Card, extending armour 5.8, with a **lane word** in the header, a **shape
glyph** on each line (⛨ prevent, ⟲ cost, reach or declared step, ✦
channel, ◈ token, ▸ button), and a "?" for a dormant line with its glyph hidden:

```
◇ UNCOMMON  KINWARD TORC · amulet (neck) — life
  ⛨ Kinward: 1/rest, poison, bleeding or withering doesn't land on an adjacent ally
  ⚠ would be greyed on Bram: ReactionCap (Shelter)
```

```
◇ COMMON  EMBER RING · ring (finger) — your strike
  ✦ Ember: 1/rest, one hit's part turns fire, rider attempted
  fits finger 1/2
```

```
◆ RARE  HEARTWARD TORC · amulet (neck) — life
  ⛨ Kinward: 1/rest, poison, bleeding or withering doesn't land on an adjacent ally
  ? (unidentified)
```

- **Names carry the verb:** each family name contains its property
  keyword. Display names never clash with conditions, affixes, behaviour
  profiles, universal actions, verbs or properties (armour 17.1 rule 13).
  Hence "Holder" → "Clench"; "Steadfast" not used; "Seeker" kept off M2;
  the Ember Ring line is "Ember", never "Channel" (loot `channels[]`);
  Watcher's Hood's property is "Vigil"; "Hunter's Tally" → "Trophy Tally"
  (Hunter is an elite affix). The card writes "charm (none)", never
  "Charm" (the Charmed condition is Later). Kind display words (5.6) are
  never used in capacity family names ("Porter's Satchel", "Mule Strap";
  never "belt", armour 5.4).
- **Party view:** silhouette with location pips; jewellery is icon-only;
  helm socket and cloak chain as approved (armour 5.8); charge pips on the
  card; in combat only the one trinket button and its pips appear on the
  action bar.
- **Fires:** a floater with the trinket name and a log line. A prevention
  emits `ConditionResisted{reason: TrinketProperty}`, a new additive
  reason (TK-5, TK-21). `ArmourVerb` is not renamed.
- **Greyed reasons:** `LocationOccupied`, `ReactionCap` (settled); new
  `ButtonCap`, `DuplicateAnswer`, `AlreadyWarded`,
  `AlreadyWardedThisFight` and `TrinketHelpedLimit`, the first two with a
  one-line pre-equip verdict (TK-10). **Warning verdicts** (never a
  suppression): `DeadLine` (a line another worn source already covers,
  Emberward behind a fire Resistant source), a ring whose rider slot the
  active weapon already takes (13.6), and Lifeline on a wearer who is not
  Medicine-trained.
- **Art:** icons kit-bashed from kind silhouette × material × element tint
  × lane glyph.

## 16. Licensing

**(lead's call; mirrors armour 15)**

- All trinket names and texts are original.
- SRD-shaped mechanisms (rarity tier names; Warded as a ward; the element
  channel shape; rest-charged item powers; the capped capacity item) are
  adapted under CC-BY-4.0 and attributed in the credits **(settled, main
  design 2)**.
- Every record flagged `srd` cites `srdSource`, **checked against SRD
  5.2.1 before data is written** (header). Any token or reroll text is
  checked against SRD 5.2 species and feat text (the halfling Luck and
  Heroic Inspiration shapes), and the copy registry's pre-cleared and cut
  entries (Steady Aim, Patient Defense, Cunning Action) against the SRD
  5.2.1 class text.
- Ideas inspired by the games in 4.1 are labelled original and carry no
  names from those games.
- TK-24 adds trinkets to the main design credits pointer.
- **Nothing is written to data before the SRD check.**

## 17. Data and the core

**(lead's call)**

### 17.1 Data shape *(illustr.)*

- **Trinket item record:** the items 12.3 fields, with `category:
  trinket` and `trinket{kind, wornOn}` (armour 16.1). No new kind enums.
- **Property records** (`prop.trinket.*`) declare `value`, `table`,
  `effectKinds[]`, `subject` (one or more tokens from the closed subject
  vocabulary, 17.2), `trigger`, `pool`, `policy`, `reactionShaped`,
  `test[]` and `tags[]` (items 12.4), `negates[]`, `hook`, `previewHint`,
  `obvious`, and **(lead's call, new)**:
  - `answers: [{scope, key}]`: the `DuplicateAnswer` keys (7.1), on every
    prevent property, armour and shield records included; `AnswerOwner`
    and rule 28 read this field, never `subject`;
  - `channel: {kind: typeShift|riderFlag, element?, riderTypes?,
    filters[]}` (9.4);
  - `costStep: {action, to}` and `reachStep: {actions[], squares}` (6.1
    items 3, 4) and `declaredReach: {squares, max}` (item 11);
  - `button: {abilityId}`, pointing to an ability-model record that holds
    cost, target, pool, output and `useWhen` (replaces `grantsButton`);
  - `targets: self|adjacentAlly|reach2Ally` (rule 22);
  - `element?` (rule 27); `usefulFor[]` (rule 26);
  - `srd` and `srdSource` (16);
  - `carrier: {ranks[], excludeBoss, minFloor}` for carried-eligible
    properties (12).
- Every trinket item record declares its `monsterProfile` (enemies 15.4).

```json
{ "id": "item.trinket.kinward_torc", "family": "family.kinward_torc",
  "category": "trinket", "rarity": "uncommon", "minFloor": 1,
  "trinket": { "kind": "amulet", "wornOn": "neck" },
  "properties": { "fixed": ["prop.trinket.kinward"], "hiddenMax": 0 },
  "channels": ["container", "carried"],
  "usableBy": { "party": true, "monster": true, "monsterTags": ["armed"] },
  "monsterProfile": { "mode": "passive" },
  "requires": ["formation.adjacent"], "value": 2 }
```

```
prop.trinket.kinward: { value: 2, table: "props.trinket.neck_major",
  effectKinds: ["preventCondition"], subject: ["Poisoned", "Bleeding", "Withered"],
  answers: [{scope: "ally", key: "Poisoned"}, {scope: "ally", key: "Bleeding"},
            {scope: "ally", key: "Withered"}, {scope: "self", key: "prop.trinket.kinward"}],
  trigger: "allyConditionWouldLand", targets: "adjacentAlly",
  pool: { refill: "rest", bands: [1,1,1] },
  policy: { mode: "always", filters: ["nonMinion"] }, reactionShaped: true,
  carrier: { ranks: ["elite"], excludeBoss: true, minFloor: 1 },
  usefulFor: ["anyCompanion"], test: ["prevent", "charge"], tags: ["prevent"],
  negates: [], obvious: true }
```

### 17.2 Tuning files

- **`trinkets.json`** holds the property tables (14.1), per-property pool
  bands and the trinket tuning numbers. A save records its hash, as
  `inventory.json` does. **(lead's call, new: the domain map lives in
  `inventory.json` with the other cross-category keys, so it has one
  source)**
- **`inventory.json`** gains (TK-11):

```json
"trinketSubjects": ["Search", "Blinded", "Revealed", "Unfocused", "Frightened",
  "Dazed", "critRider", "unseenRider", "Grappled", "Restrained", "HandOver",
  "Administer", "Prone", "standCost", "hazardEdge", "Disengage", "Burning",
  "Smouldering", "Chilled", "Hide", "stayHidden", "shakeOff", "poison",
  "bleed", "disease", "Poisoned", "Bleeding", "Withered", "Downed",
  "fireEvent", "Warded", "Helped", "Help", "partType", "partRider",
  "declaredReach", "pack"],
"trinketDomains": { "props.trinket.head": ["Search", "Blinded", "Revealed",
                       "Unfocused", "Frightened", "Dazed", "critRider", "unseenRider"],
  "props.trinket.neck": ["poison", "bleed", "disease", "Poisoned", "Bleeding",
                         "Withered", "Downed"],
  "props.trinket.neck_major": ["fireEvent", "Poisoned", "Bleeding", "Withered"] },
"trinketRarePerFloorMax": 1,
"trinketButtonCap": 1,
"sharedTrinketAllowlist": [],
"copyRegistryAllowlist": [ { "trinket": "prop.trinket.runner",
  "shape": "cunningActionDisengage", "reason": "trinkets design 11, item 3" } ],
"trinketPolicyFilters": { "bigHitPermille": 200,
  "channelDefaults": ["nonMinion", "notResistant", "noCancelOwnSide"],
  "channelMandatory": ["hostileOnly", "riderWouldBeAttempted"] }
```

  `trinketDomains` lists, for every table (the other tables follow the
  same pattern), the subject tokens it owns; every token comes from the
  closed `trinketSubjects` vocabulary, so rule 15 checks exact
  membership, and a property with several subject tokens is checked
  token by token.
- **Not tuning (lead's call, new):** `trinketRarePerFloorMax` (Q2) and
  `trinketButtonCap` (Q3) are the client's decisions and need client
  approval to change; `trinketSubjects`, `trinketDomains`,
  `sharedTrinketAllowlist`, `copyRegistryAllowlist` and the channel
  filter lists are a lead's-call block that only the lead changes.
  `bigHitPermille` and the pool bands are tuning *(illustr.)*.

### 17.3 State, queries and events

- **No new item state.** Charges use the existing `spent` state (items
  12.8); Weathered's element is per record (13.4).
- `fightUses` (armour 16.3) lists trinket `fight` properties: Visor's
  unseen branch, Runner, Oilskin, Duskhem, Lifeline, Aid and Tally at 1
  (TK-7). Entries are keyed by (unit, property id); a duplicate id is
  greyed (7.1) and never spent.
- **New unit-state rows (TK-7)**, in the armour 16.3 table:
  `trinketPreventionReceivedThisRound` (bool, resets at round start; 7.3),
  `trinketHelpedThisRound` (bool, resets at round start) and
  `trinketWardedThisFight` (bool, resets at fight start). Outside combat
  the round flags reset before each condition application event (7.3).
- **New awareness flag (TK-26):** `everNoticed[partyUnit]` on each group's
  awareness record (enemies 11.2), set on the first `noticed` and reset at
  fight start (Grey Mantle, Duskhem). Nothing derived is stored
  **(settled, stat model 6)**.
- **Queries:** `ActiveReactionProperty(unit)` (settled) reads the new
  `reactionShaped` flag; new `ActiveTrinketButton(unit)` and
  `AnswerOwner(unit, scope, key)`, by the fixed slot order of 7.1 and
  reading `answers[]`; new `ChannelPart(use, target)` for 9.4, which
  reads party knowledge for party wearers. All derived, never stored.
- **Events:** `TrinketPropertyFired{property, instanceId, unit, result}`,
  with `result ∈ {prevented, shifted, flagged, costStepped, reachStepped,
  granted, blunted, notSpent}`; `ConditionResisted{reason:
  TrinketProperty}` (TK-5, TK-15). A channel that does not fire emits
  nothing that names a hidden trait (9.4).
- **AI:** monster carriers use the resolve-preview scoring; buttons need
  `useWhen` (12).
- **Determinism:** every trinket fire replays bit-identically (main design
  core rule); integer arithmetic and permille only **(settled)**.

## 18. Validation, testing and simulator

### 18.1 Content validation

New rules in items 19.1, numbered 15 onward because items 19.1 runs to
rule 14 (TK-14). In this document a bare "rule N" always means items 19.1
rule N; armour rules are always cited as "armour 17.1 rule N". The build
fails when **(lead's call)**:

| Rule | Content |
|---|---|
| 15 | **Domain map:** each (effect kind, target scope, subject token) triple has one owning table, read from `trinketDomains` and the closed `trinketSubjects` vocabulary (17.2); a property with several subject tokens is checked token by token; a property outside its table fails; shared triples only through `sharedTrinketAllowlist` (empty in M2) |
| 16 | **Denylist** (6.2) on `effectKinds[]`, and every effect kind is one of 6.1's |
| 17 | **Combat-first:** the `obvious` property binds a combat hook; capacity exempt (its hook is the capacity query; also exempt from armour 17.1 rule 14's zero-trigger-rate failure) |
| 18 | **Class copy registry** at any charge level, bidirectional (11): a collision fails both records unless the pair is in `copyRegistryAllowlist`. The registry is empty until class records land (23) |
| 19 | **Buttons:** only on charms in M2, at most 1 per item; the button is a property whose effect is the grant, and its `button.abilityId` resolves to an ability-model record |
| 20 | **Pools:** no `never`; `camp` only on Rare; majors 1 / 1 / 1 |
| 21 | **Rare:** amulets only, `minFloor` 3+, never `signature`; pieces at most Uncommon (settled). The per-floor count `trinketRarePerFloorMax` is a generation invariant, not content validation (items 19.2, TK-27) |
| 22 | **Ally effects:** `targets` is `self`, `adjacentAlly`, or `reach2Ally` only on Hand over and Administer reach steps; one ally per trigger |
| 23 | **One cost step and one reach step per action; one channel per use** (7.1); a declared reach option is not a channel |
| 24 | **Outputs:** minor or token conditions only; never an AC-writing condition; zero added damage as 6.2 defines it; every `channel` record carries `hostileOnly` and `riderWouldBeAttempted` |
| 25 | **Carriers:** `negates[]` and a role-answer pass per carrier type; `carrier{}` limits present on every carried-eligible property; output conditions within the type's 2; a reaction-shaped property counts as the type's one reaction beyond the opportunity attack and a button under the bonus-action cap (enemies 9.1); Wardstone and Ward-Knot never with the Warded or Unbreakable affix; the 6.4 guards pass with the carrier (checked at generation, enemies 15.3 step 4) |
| 26 | **Two classes:** every record declares `usefulFor[]` with at least 2 launch classes or `anyCompanion`, signed off by the lead; tag-keyed text met by at least 2 |
| 27 | **Element families:** at most 2 element families per wave per lane (finger, neck, back), counted from `element` |
| 28 | **Rolled lines:** a rollable table never offers the item's own obvious line, and never a line sharing an `answers[]` key with it, a second channel on a channel ring, or a second button. An Uncommon or Rare obvious line with no legal roll in its table fails; Heartward keeps all 3 (Antidote, Graveward, Lifeline) |

Armour 17.1 rules 10, 11, 13, 14 and 17 still apply **(settled, AR-38)**.
The authoring lean (5.6) is a lint warning, never a failure.

### 18.2 Tests

**(lead's call, new)**

- **Domain map and denylist:** a property outside its table, a shared
  pair outside the allowlist and each denylist row fail the build.
- **Suppression:** `ButtonCap`, `DuplicateAnswer` and the reaction-shaped
  extension of `ReactionCap` by slot order; armour and shield win
  `DuplicateAnswer`; two lines with the same property id (two Aid lines)
  grey the second and never spend its pool; `self:` and `ally:` keys do
  not collide; a tag key collides with its condition ids; one capacity
  trinket counts; pre-equip verdicts match the post-equip state;
  suppressed lines are never spent.
- **Recompute:** suppression is recomputed on equip or unequip,
  `ItemRevealed` (deferred to fight end in combat) and a weapon-set swap,
  each tested; counters survive a recompute.
- **Dormant lines:** invisible to verdicts and suppression; suppression
  recomputed and announced on identify.
- **Channels:** damage after a shift always equals damage before it,
  including a resisted, immune or weak original part; a fire-weak target
  takes normal damage and Burning automatically; a shifted part never
  breaks a layer; `notResistant`, `nonMinion` and `hostileOnly` skip
  without spending, and an ally in a friendly-fire area is never shifted;
  `riderWouldBeAttempted` fails without spending on a target already
  Burning, behind an earlier flagged or weak-typed part, and on an
  at-will's second rider in a turn; `noCancelOwnSide` stops Ember on the
  party's Frozen and Chilled targets and Frost on the party's Burning
  target; Oiled is not paid out by a shifted part and Burning lands at
  normal band; Soaked stops Ember; the part and target choice of 9.4,
  including no qualifying part; each attack of an Attack action is its own
  use; two rings fire by slot order, one per use; ticks and retaliation
  never trigger a channel; a channel's fire-or-skip decision is the same
  in the party-knowledge preview and the resolution, and a skip emits no
  event naming a hidden trait.
- **Longreach:** offered only beyond normal reach; paid on a miss; Ember
  still fires on the same attack.
- **Gear prevention:** the 7.3 order (armour, own trinkets, allies by unit
  order); trinket preventions after a failed contest or an automatic
  landing and before the ward; nothing spent on a successful save; one
  received trinket prevention per unit per round, checked before "ask
  me" and set only on a stop; Kinward yields only when step 4 would spend
  a ward, so it fires on a Warded ally against a lingering Poisoned and on
  the second rider of one use; the step 7 re-check reruns the gear step
  (a transform to Withered meets Graveward); `onApply` relations skip it;
  outside combat each application is its own round.
- **Emberward:** `bigHit` on the summed fire parts after crit; no charge
  spent when the step changes nothing; `DeadLine` warning behind fire
  Resistant armour.
- **Charms:** `AlreadyWarded` and `AlreadyWardedThisFight` grey the
  button; a trinket ward on a Warded unit has no effect; buttons are
  unusable before initiative; one trinket Helped per round, Sigil-cheapened
  Help included (`TrinketHelpedLimit`); Tally's Helped survives to the end
  of the wearer's next turn and Tally is not spent while the wearer holds
  Helped.
- **Costs and reach:** one cost step and one reach step combine; two cost
  steps on one action are refused; Oilskin, Runner, Lifeline and Aid are
  spent only when their variant is chosen.
- **Carriers:** a carrier with a Guard verb cannot take Kinward; a carrier
  with a bonus-action ability cannot take Wardstone; Wardstone is never on
  an Unbreakable or Warded-affix elite; a Heartward with a Lifeline roll
  goes to a container; the party receives a used carried trinket with
  `spent` 0.
- **Stealth:** `everNoticed` set on first `noticed` and reset at fight
  start; Grey Mantle's two-die Hide replays bit-identically.
- **Pools:** `fightUses` reset at fight start; band 1 / 1 / 2 and majors
  1 / 1 / 1; carriers map `rest` N to `uses:N` and `fight` to `uses:1`.
- **Replay determinism:** a recorded fight with every M2 trinket replays
  bit-identically.

### 18.3 Simulator reports

Added to armour 17.3 and items 19.3 (TK-8, TK-17). Targets *(illustr.)*,
except the landing envelope, whose numbers are the lead's (TK-4):

| Report | Target |
|---|---|
| **Condition landing envelope (release gate; also in the E+ safety veto)** | With the maximum legal trinket and armour prevention loadout, each monster type's signature condition lands at least 60% as often as trinket-free, and a boss's hard control lands at least once per boss fight in at least 50% of fights. A second row measures the boss's hard control **on the hero** with the maximum legal companion-support loadout (Ward-Knot, Kinward, Heartward on companions). These numbers are binding lead's numbers in `ePlusGate` (`landingEnvelopeMinPermille: 600`, `bossHardLandMinPermille: 500`; TK-4), not *(illustr.)* |
| Trigger rate per property per kind | At least once per 3 fights for its wearer, also reported per short-rest interval and, for neck lines (Antidote, Graveward, Emberward, Lifeline), per floor theme; capacity exempt |
| Pick rate by location and family | No family above 50% of eligible wearers within any class |
| Roll concentration | Measured per table against each obvious line's legal roll set: no property above max(40%, 1.5 ÷ the number of legal entries) of kept Uncommon rolls; `props.trinket.charm` is report-only |
| **Offence** share of M2 families and picks (channels, reach, Helped, Hidden kills, free Disengage) | At least 25% *(illustr.)* (now 27%, 13.8) |
| Recovery and tempo share (getting up, getting free, giving, reviving) | Reported |
| Rare-found rate per run; Rare offer and pick rate per floor | Reported (8.2) |
| Button press rate per fight | Watch for unused-button bloat; feeds the charm pass (24.2) |
| `ButtonCap`, `DuplicateAnswer`, `ReactionCap` (reaction-shaped) and per-round limit bind rates | Reported; in the E+ veto at 10% |
| Party trinket preventions per fight | Ceiling *(illustr. 3)* |
| Precision class damage with best trinkets | At most +10% over trinket-free |
| Rare amulet, reported separately; effect on boss win rate | Reported |
| Antidote, Graveward and Lifeline against tonic, Remedy and Treat use; companion downings with and without Lifeline | Reported |
| Element share of finger and neck picks; second same-element offer in a run | De-weighted by smart loot |
| Companion slot fill at the boss | ≤ 25% empty **(settled)** |
| Piece against jewellery; the E+ rows | **(settled, armour 17.3)** |

## 19. Staging

**(lead's call)**

- **M1:** the display-only helm and crested enemy helms only **(settled,
  armour 5.8)**.
- **M2 (the whole category):** the property tables of 14.1; the 30
  families of section 13; Rare amulets from floor 3; capacity +1 and +2;
  `ButtonCap`, `DuplicateAnswer`, `AlreadyWarded`, the reaction-shaped
  extension, the per-round and per-fight limits and the channel filters;
  dormant properties; carriers; rules 15–28; the simulator reports; the E+ gate reports.
- **Later:** everything marked Later in section 13; the shared jewellery
  pool and "C-split" (only the Uncommon rolled line drawn from a wider
  pool), only if the lane-prediction playtest holds at 80% *(illustr.)*;
  kind-flexible variants, only if `LocationOccupied` binds in the armour
  17.3 report; Rare charms (reopening Q2); one-aura-per-party Rare amulet
  (needs the client); curses; theme-tag synergy (armour 5.9); trader-sold
  trinkets; heirloom templates (M3); exploration-first trinkets; E+ by the
  lead's gate.

## 20. Risks

| Risk | Mitigation |
|---|---|
| **Prevention density** collapses monster condition design | Trinket wards never stack; one trinket Warded per unit per fight; one received trinket prevention per round; ally preventions yield to a ward that would be spent and count under the reaction cap; `DuplicateAnswer` counts armour; the landing envelope is a release gate and in the E+ veto |
| **Warded stops hard control** through a rest-charged button | Approved Warded behaviour; at most two ward charms in M2; buttons combat-only; one trinket Warded per unit per fight, so three companions cannot ward the hero again and again in the boss fight; Ward-Knot carried only by elites outside boss encounters; the envelope's boss row and its hero row |
| **Defensive skew** makes trinkets read as boring insurance | Offence share reported apart from recovery and tempo (27% offence, 13.8), target ≥25% *(illustr.)*; rings own the strike lane; named Later reserves to promote; pick reports |
| **Rings waste charges** under `always` | `riderWouldBeAttempted`, `noCancelOwnSide`, `hostileOnly`; filters on party knowledge; the weapon-flag verdict (13.6) |
| **Amulet must-have** (one neck, life lane) | No healing amounts; Kinward narrowed to the neck's own subjects and capped; Lifeline a `fight` minor; mind kept on head in M2; no family above 50% within any class; the element major moves off neck if neck pick share is too high |
| **Two rings as the best kind** | One channel per use; `DuplicateAnswer`; at most 2 element rings per wave; zero added damage |
| **Rings feel weak** without the weakness bonus (Q4) | Rider control as their job, automatic on weak targets; trigger-rate and pick reports; Q4(b) stays the recorded alternative |
| **Dead slots** (trigger rate below 1 in 3) | Watch Visored, Watcher's, Grey Mantle, Duskhem, Barred, Weathered, and the theme-dependent neck lines Antidote, Graveward, Emberward and Lifeline, by floor theme; the Lifeline training verdict; same-location reserves in section 13 |
| **Thin feet and hands at exactly 3** | Reserves ship in the same build if one is cut (armour 17.3 pattern) |
| **Loot dilution** (30 families, about 9 finds) | A run sees about 30%; `requires` tags steer; second same-element offer de-weighted |
| **Rare amulet trips the E+ "displaced" gate** | Reported separately (TK-4); tune Rare frequency, never E+ |
| **Thin Rare tier** (one Rare family, at most one per run) | Rare offer and pick reports per floor; a second Rare neck family is a Later option (24.3) |
| **Medicine-dependent entries become dead loot for companions** | At most one Medicine or Lore family per wave; `requires` steering |
| **New greyed reasons to teach** | Pre-equip verdicts; one line each |
| **Charms feel thin** with two ward buttons and Tally in M2 | The charm-abilities pass (24.2), with the button press-rate report as its input |
| **Bring-the-key** (element amulets and cloaks by floor theme) | Rest gate; charged, single-event effects; Torc of Seasons Later |
| **Unruled dependencies** | Duskhem against Hidden's end rules; Leaper's against Jumping; Inspired owner; Storm and Dawn rings; Kinstone `sourceTied` |
| **Class copies discovered late** | The bidirectional registry makes the second-authored record change; Runner's pre-cleared |
| **SRD shapes from memory** | Check against SRD 5.2.1 before any record (header, section 16) |

## 21. Left to the builders

**(lead's call, new)**

- All numbers in `trinkets.json` and every *(illustr.)* value: pool bands,
  `bigHitPermille`, family names, notable names, the roster within section
  13's counts and floors, report targets marked *(illustr.)*.
- `value` inputs, prices and `identifyDifficulty` per record, within the
  items design.
- Icons, glyph art, tints and card wording within section 15.
- `useWhen` for carried buttons; `requires` tags per record within the
  existing vocabulary.
- **Not tuning:** `trinketRarePerFloorMax` and `trinketButtonCap` (client
  decisions, Q2 and Q3); zero added damage (Q4); the domains and lanes
  (Q1); `trinketSubjects`, `trinketDomains`, `sharedTrinketAllowlist`,
  `copyRegistryAllowlist` and the channel filter lists (the lead's); the
  landing envelope numbers in `ePlusGate` (the lead's, TK-4); adding a
  kind enum; the E+ switch (armour 5.10, the lead's).

## 22. Changes to approved documents

Applied on 2026-11-17, when the client approved this document. Each change names
the exact section it touches. TK-1 carries the client's Q1 decision for
the armour 13.4 cell, and the rider-ownership rule in TK-2 was signed off
under Q1; the rest are the lead's, confirmed with this document.

**Narrowings recorded inside this design (no change id, no approved text
changes):** no ability modifiers on trinkets (narrows items 12.4); no
`whileCarried` in M2; no added damage; Rare only on amulets; no boss
signature trinkets; no `never` pools; no trinket output that writes AC,
and no Corroded in trinket lists in M2.

**Armour design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| TK-1 | 5.6, last row; **13.4, Amulet / ring / charm row, Domain cell (agreed, Q1)** | 5.6's last row becomes three location rows: neck = life and the body; finger = your strike; none = stored moments (charm) and carrying (capacity) (trinkets design 5.5). The 13.4 Domain cell, now "Trinkets design", reads "Neck: life and the body · Finger: your strike · None: stored moments, capacity". No other 13.4 cell changes | **Client (agreed 2026-11-17, Q1)** for 13.4; Lead for 5.6 |
| TK-2 | 5.6, examples column | Ledgewalker's Boots: "advantage on hazard-edge and landing saves"; Visored Helm gains the once-per-fight unseen-attacker branch; Grappler's Gauntlets covers Restrained; Weathered Cloak covers Burning or Chilled in M2, under the rider-ownership rule (trinkets design 5.7) | Lead; **rider-ownership rule signed off by the client (agreed 2026-11-17, Q1)** |
| TK-3 | 5.5, reaction cap bullet; new bullet | Off-turn automatic triggers that protect or affect another unit count as reaction-granting for the cap. New bullet: `ButtonCap` and `DuplicateAnswer` suppressions use the same fixed slot order and pre-equip verdict as `ReactionCap`; `DuplicateAnswer` counts armour and shield prevent properties (each declaring `answers[]`, trinkets design 17.1), which win by slot order | Lead |
| TK-4 | 5.10, criterion 1; safety veto bullet; `ePlusGate` | Criterion 1: "jewellery (amulets, rings, charms) excludes Rare-tier records". Veto adds: the condition landing envelope and its hero row (trinkets design 18.3); the bind rates of `ButtonCap`, `DuplicateAnswer`, reaction-shaped triggers and the per-round and per-fight limits at 10%; the carrier 6.4 rerun. `ePlusGate` gains `landingEnvelopeMinPermille: 600` and `bossHardLandMinPermille: 500`, binding lead's numbers | Lead |
| TK-5 | 14.1, gear-prevention step | Trinket properties also act at this step and emit `ConditionResisted{reason: TrinketProperty}` and `TrinketPropertyFired` | Lead |
| TK-6 | 14.4, Enclosed signature Sealed (Later) | "Sealed: Poisoned never lingers on you" (blocks the lingering form) instead of save advantage, so it no longer copies Antidote Locket under rule 11 | Lead |
| TK-7 | 16.3, state table; events | `fightUses` lists Visor's unseen branch, Runner, Oilskin, Duskhem, Lifeline, Aid and Tally at 1, keyed by (unit, property id). New rows: `trinketPreventionReceivedThisRound` (bool, round start), `trinketHelpedThisRound` (bool, round start), `trinketWardedThisFight` (bool, fight start). New event `TrinketPropertyFired{property, instanceId, unit, result}` | Lead |
| TK-8 | 17.3, simulator reports | Adds the rows in trinkets design 18.3 | Lead |

**Items design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| TK-9 | 17, row 6 | "Property tables (rings, amulets, charms, cloaks, helms, gloves, boots)" becomes "piece tables by location, jewellery tables by kind (trinkets design 14.1), capacity table"; adds a pointer to the trinkets design | Lead |
| TK-10 | 7.3, greyed reasons | Add `ButtonCap`, `DuplicateAnswer`, `AlreadyWarded`, `AlreadyWardedThisFight`, `TrinketHelpedLimit`; add the warning verdict `DeadLine` (never a suppression) | Lead |
| TK-11 | 5.1, `inventory.json` | Add `trinketSubjects`, `trinketDomains`, `sharedTrinketAllowlist` and `copyRegistryAllowlist` (a lead's-call block), `trinketRarePerFloorMax` and `trinketButtonCap` (client decisions, not tuning), `trinketPolicyFilters` | Lead |
| TK-12 | 12.3, `value` row; 12.6; 19.1 rule 2 | Clarification: "a trinket button is a property whose effect is the grant; its value is counted once, inside that property". Capacity: +1 pack = value 1, +2 = value 2 | Lead |
| TK-13 | 19.1, rule 4 | "Trinkets: `rest` and `fight` by default; `camp` only at Rare; never `never`. Trinket `fight` uses use the armour 16.3 `fightUses` state" | Lead |
| TK-14 | 19.1, new rules 15–28 | Trinkets design 18.1 | Lead |
| TK-15 | 18.3, events | Add `TrinketPropertyFired` | Lead |
| TK-16 | 20, M2 staging; 24 | "Trinkets settled in the trinkets design"; dependency pointer | Lead |
| TK-17 | 19.3, simulator reports | Pointer to trinkets design 18.3 | Lead |
| TK-27 | 19.2, run-time invariants | "At most `trinketRarePerFloorMax` Rare trinkets per floor across all channels; once the floor's count is reached, Rare trinket draws are excluded from the weight table for the floor's remaining draws, in channel order containers, carried, camp, boss" (asserted by the generator and a test) | Lead |
| TK-28 | 7.1, Administer and Hand over rows | Add "(a trinket reach step may extend 'adjacent' to 2 squares; trinkets design 6.1)", matching TK-19 | Lead |

**Ability model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| TK-18 | 6.2, `fight` row (as amended by AR-6) | "…and every trinket `fight` use (trinkets design 9.3)" | Lead |
| TK-19 | 7.2, Hand over and Administer rows | Add "(a trinket reach step may extend 'adjacent' to 2 squares; trinkets design 6.1)"; the items 7.1 source rows change with it (TK-28) | Lead |
| TK-20 | 10.2, limited-ability bullet | "Abilities granted by items (potions, scrolls, wands, trinkets) do not count; trinket buttons are capped at one per unit (trinkets design 9.2)" | Lead |

**Conditions design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| TK-21 | 9.2, gear-prevention paragraph (AR-51); entry-point table; step 7 | Names trinket properties; adds `reason: TrinketProperty`; adds the gear-prevention order (trinkets design 7.3: armour and shield verbs, then the target's own trinkets, then ally preventions by unit order); adds the per-round limits (one received trinket prevention per unit per round, counted only on a trinket stop; ally preventions yield only when step 4 would spend a ward; one trinket Helped per unit per round); outside combat each application is its own round. Entry-point table gains a column "Gear step": skipped wherever the ward is skipped. Step 7 reads "steps 2, the gear step, 4 and 5 run once more" | Lead |
| TK-22 | 18.9, Warded and Helped | Warded: "a trinket-granted Warded has no effect on a unit already Warded; a unit receives at most one trinket-granted Warded per fight". Helped: sources add "trinkets (Trophy Tally, fallback `untilSourceTurnEnd` with the wearer as source; Help made cheaper by Sigil of Aid counts under the trinket limit)" | Lead |
| TK-25 | 18.6, Oiled and Soaked interactions; 19, interaction table damage-end row | Oiled: "a ring-shifted fire part (trinkets design 9.4) does not pay out Oiled: no +half, no band-higher Burning, Oiled stays and no oil square burns; Burning lands as an ordinary automatic rider at its normal band". Soaked: "a ring-shifted fire part ends Soaked and attempts no Burning rider, as any fire hit" | Lead |

**Enemies design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| TK-23 | 15.3, step 4 ("Legal means…", as amended by AR-26) | Adds: "For a notable trinket, legal also means: with its rolled properties in place it passes the 6.4 guards; its output conditions fit 9.2; a reaction-shaped line is the unit's one reaction beyond the opportunity attack and a button is its bonus-action ability (9.1); its role answers pass; no suppression conflict with the issue gear; Wardstone and Ward-Knot never with the Warded or Unbreakable affix; its `carrier` limits hold (trinkets design 12)". Carried wards and ally preventions count toward the encounter's control defence. The agreed 15.4 table is unchanged | Lead |
| TK-26 | 11.2, awareness record | Adds a per-fight flag `everNoticed[partyUnit]` to each group's record: set on the first `noticed`, reset at fight start (trinkets design 13.4) | Lead |

**Main design (2026-11-15):**

| # | Section | Change | Approved by |
|---|---|---|---|
| TK-24 | 10; 2, Anti-goals "Protected content" | "Trinkets are settled in the trinkets design; they are no longer pending"; credits pointer for adapted trinket mechanisms | Lead |

TK-1 is the only change that touches an agreed row, and only by filling
the pointer cell the client approved (Q1). TK-23 amends enemies 15.3 step
4, a lead's sentence (AR-26), and leaves the agreed 15.4 table unchanged.
**Not changed:** stat model 6 (Speed, AC); the armour 5.4 location rule
and the 5.5 piece rules other than TK-3's reaction-cap extension and
suppression bullet; armour 14.3 Resistant; `packInCombat`; belt and trinket-slot counts; every other
agreed row. **Would need the client, and not recommended:** trinket speed;
always-on resistance on trinkets; a Retrieve change; belt or trinket-slot
grants.

## 23. Depends on systems not yet designed

- **Classes and offers:** every class, subclass and companion feature
  declares `effectKinds[]`, subject and trigger for the copy registry
  (11); the ceiling report's reference builds. Answered by the classes
  design (`2026-11-17-classes-concepts-design.md`) **(change CL-13f)**:
  the four launch classes for the two-class rule (rule 26) are the
  Vanguard, Shade, Arcanist and Herald; at least 2 launch classes
  concentrate (Arcanist and Herald), so Stillmind's condition is met
  (Later) (classes design 8.7, 11.G).
- **Companions:** kits and choices at 3, 6 and 9; Medicine and Lore
  training spread (Lifeline steering).
- **Death and healing:** answered by the death and healing design
  (settled): nothing lifts Downed beyond Medicine (Lifeline counts toward
  its once per companion per fight), a won fight, a completed short rest
  or camp; the drop-prevention chain is Death-warded → Reprieve Phial
  (talisman), owned there; Last Ward and Last-Breath Charm retired (death
  and healing design 11, 15; consumables design 14.9, 14.10).
- **Consumables:** the flask throw is designed (consumables design 11);
  Slinger's Bracers numbers stay Later **(change CN-22)**.
- **Conditions content (Later):** Veiled (Smoke Cloak), Shielded (Bulwark
  Locket), Inspired and its Leadership-mastery owner (Muse's Token),
  Bolstered.
- **Dungeon generation:** hazard edges and crossable hazards per floor
  (Ledgewalker, Leaper's); water and oil squares (Wader's); Jumping
  sources (main design 4.3).
- **Enemy content:** the Support role (Graveward exclusions, Later);
  grapplers and web or net users (Grappler's Gauntlets); lurkers (Watcher's
  Hood, Visored Helm).
- **Curses:** whether Rare rings or charms are needed, which reopens Q2.
- **Lead's checks before data** (existing designs, not new systems):
  Duskhem against Hidden's end rules and Shadowed; Storm Ring against arc
  damage; Dawn Ring against Hidden's targeting rule; Kinstone `sourceTied`.

## 24. Open items for the client

### 24.1 This document

Approved by the client on 2026-11-17.

### 24.2 Future design pass: charm abilities ("stored moments")

**(agreed, Q3: the client wants charm abilities discussed further as a fun
mechanic; scope and timing lead's call, new)**

A dedicated pass on expanding what charms can store and when the player
spends them, brought to the client as its own proposal.

- **Fixed frame:** `ButtonCap` stays 1 per unit; the denylist (6.2), the
  caps of section 7, zero added damage and value equals budget all hold.
  Anything that would change one of these goes to the client as a
  question in that pass.
- **What it may widen:** the charm lane's subjects and shapes (for example
  new stored moments beyond Warded and Helped, Later charm entries in
  13.7, a once-per-fight trigger family), new Later conditions as outputs
  where the conditions budget allows, and whether Rare charms reopen Q2.
- **Inputs:** M2 playtest and simulator data on the button press rate,
  Wardstone and Ward-Knot pick and trigger rates, the landing envelope,
  and the `ButtonCap` bind rate.
- **When:** after the first M2 playtest data on those reports, and before
  any Later charm entry is approved. Until then the M2 charm roster in
  13.7 stands.

### 24.3 Future items triggered by reports

Each comes back to the client only if a report or a later design triggers
it:

- **Shared jewellery pool or C-split:** only if the lane-prediction
  playtest holds at 80% *(illustr.)* (19).
- **Rare charms or rings:** reopen Q2 (curses, Muse's Token). Last-Breath
  Charm is retired: the Reprieve Phial owns the party's extra-life fantasy
  (death and healing design 15.3; consumables design 14.9).
- **One aura per party on a Rare amulet:** a Later option, not
  recommended for M2 (section 4, decision 7).
- **The element major moving off neck,** if the neck pick share is too
  high (20).
- **A second Rare neck family** (for example Emberward plus a rolled neck
  minor), if the Rare offer and pick reports show the Rare tier too thin
  on floors 4–5 (8.2, 20). It adds a family to the roster, so it comes
  back with the report.
- **Q4(b),** the weakness bonus on ring-shifted parts, only if rings fail
  their pick and trigger targets after tuning.

## Appendix A: how the review findings were resolved

**Identity critic**

| # | Finding | Resolution |
|---|---|---|
| I-1 | Two catalogues contradict; no single roster | One roster (section 13), one ruling per entry; 30 families inside the armour 19 envelope |
| I-1a | Luckstone kept vs cut | Cut (Steady Aim copy) |
| I-1b | Stillwater auto-success vs Stillmind advantage | Stillmind advantage, Later; auto-success cut |
| I-1c | Element amulet pair | Emberward in M2 (matches Ember Ring); Frostward Later |
| I-1d | Graveward scope | "Withered (the necrotic rider)": one text |
| I-2 | Shifting Band copies the stat model 8.4 buy-through | Cut |
| I-3 | AC leaks through Corroded | No trinket outputs Corroded; Corroded outside trinket preventions and cost steps in M2; Caustic Ring blocked |
| I-4 | Charm lane is four Wardeds; Kinward dominates Ward-Knot; two charm buttons collide | Two ward charms only; Bloodknot, Kinstone Later; Doubled Knot cut; Kinward under the reaction cap, narrowed, yields to a ward that would be spent; `ButtonCap` |
| I-5 | Defensive skew; missing stealth and caster roles | Duskhem added; rings work on spells; offence share target; Medicine and Lore entries steered |
| I-6 | Element rings: "zero added damage" a fig leaf; `always` wastes charges; two rings double-shift | Zero added damage made literal (Q4, agreed); `notResistant`; one channel per use; part and target choice (9.4) |
| I-7 | Stacking caps unresolved | One `DuplicateAnswer`, counting armour verbs |
| I-8 | Class and verb copies | Runner's pre-cleared; registry strictness for class features, weapons 18.1 for gear; Helped limit and precision ceiling; Holder → Clenching; Sealed reworded (TK-6) |
| I-9 | Dead or fragile entries; approved-example fixes | TK-2; trigger-rate watch list; Leaper's and Scholar's Later |
| I-10 | Client answer needs one sentence and a menu | 5.1; the three readings (5.8); Q3 offered the ring-button alternative; lint-only lean |
| I-11 | Carrier share; Kinward elites; Rare neck count; Torc of Seasons; Heartward strength | Carried share about a third; Kinward out of boss encounters; Torc of Seasons Later; Heartward = Kinward + a minor, reported separately |

**Balance critic**

| # | Finding | Resolution |
|---|---|---|
| B-1 | Prevention layers stack uncapped | Ward non-stacking; one received trinket prevention per round; ally preventions yield to wards; armour counted; landing envelope gate (18.3, TK-4) |
| B-2 | Automatic ally protection bypasses the reaction cap | Reaction-shaped triggers count under `ReactionCap` (TK-3) |
| B-3 | Type shift adds damage | Weakness bonus removed from shifted parts (Q4); Shifting Band cut |
| B-4 | Advantage economy feeds precision and crits | Luckstone cut; shared pool empty in M2; one trinket Helped per round; precision ceiling report |
| B-5 | Carriers outside the 6.4 guards | Guards rerun with carriers (TK-23); Helped trinkets container-only; carried wards count toward control defence |
| B-6 | Visored unseen branch erases the lurker opener | `fight` minor; party-side opener rule |
| B-7 | Element halving bounds; Torc of Seasons | Bound text in Emberward; Torc of Seasons Later |
| B-8 | Lifeline revive nova | `fight` minor; companion-death report |
| B-9 | Rest-charge volume | Majors 1 / 1 / 1; `nonMinion` and `bigHit`; preventions-per-fight ceiling |
| B-10 | Oilskin and Groundsilk act as tag immunity | Oilskin a `fight` minor; Groundsilk Later, charged, no redirect |
| B-11 | Catalogue disagreements; Corroded | Narrower text adopted; Corroded ruling (I-3) |
| B-12 | E+ veto misses new caps | TK-4 |
| B-13 | Dormant lines leak through verdicts | Verdicts ignore dormant lines; recompute on identify (7.2) |
| B-14 | Kinstone multiplies wards | Later, only if the non-stacking rule holds |

**Consistency critic**

| # | Finding | Resolution |
|---|---|---|
| H1 | "No changes" was false | Section 22: TK-1 to TK-28, narrowings listed separately |
| H2 | Rule 11 breaks: Antidote vs Sealed; escape-advantage allowlist | Sealed reworded (TK-6); escape-check advantage off the hands allowlist |
| H3 | Buttons cannot be valued | Button-as-property (TK-12) |
| H4 | Kinward and Heartward exceed the allowlist | Narrowed Kinward; Heartward = Kinward + a minor, no camp charge |
| H5 | 5.6 examples rewritten without an id; Weathered narrows agreed 13.4 | TK-2; rider ownership signed off by the client (Q1) |
| H6 | Jewellery `fight` uses not covered by AR-6 | TK-18; `fightUses` (TK-7, TK-13) |
| H7 | Items 19.1 rule 4 needs an authoring reason for `rest` | TK-13 |
| M1 | Display-name collisions | Steadfast, Clearmind not used; Holder → Clench; Seeker → Sightline; "Ember", not "Channel"; "Vigil"; "charm (none)" |
| M2 | Wader's and Groundsilk always-on full preventions | Both Later, recast as charges; Groundsilk redirect dropped |
| M3 | Catcher's Bracers changes a universal-action row | Reach step in the allowlist (6.1 item 4); TK-19, TK-28; reach and cost steps may combine |
| M4 | Carried conditions count to the type budget; beast carriers illegal | Rule 25; Trophy Tally (then Hunter's Tally) container-only |
| M5 | Corroded and the AC ban | Rule 24; Corroded out of M2 trinket lists |
| M6 | Catalogues disagree; roster size | One roster of 30; Kinstone Later; Physician's Locket cut; Seeker's Band Later |
| M7 | Rider difficulty contradicts the framework | Wearer's difficulty (ability model 9.2; 9.5) |
| M8 | E+ criterion 1 is a binding lead's number | TK-4 |
| M9 | Shifting Band vs the buy-through offer | Cut |
| M10 | Torc of Seasons needs instance state | Later; if revived, an items 18.1 change |
| L1 | 5.6 and 13.4 pointer cells; an unnecessary 5.7 change | TK-1 completes both; no 5.7 change (5.7 binds `worn.open.*` only on piece records) |
| L2 | Rare charms contradict the rarity rule | Recorded as a Later reopening of Q2 (24.3); Last Ward ownership to death and healing, which retired Last Ward and Last-Breath Charm (death and healing design 15.3) |
| L3 | Leaper's vs Athletics jump use | Later, dependent on main design 4.3 and an Athletics check |
| L4 | Capacity value undefined | +1 = 1, +2 = 2 (TK-12) |
| L5 | `charm` kind near the Charmed condition | "charm (none)" on the card |
| L6 | Change ids collide with rule numbers | Items 19.1 rules 15–28 (TK-14) |

**Second review (builder, consistency and play lenses, 2026-11-17)**
**(lead's call)**

| # | Finding | Resolution |
|---|---|---|
| R-1 | A ring could shift a resisted or immune part to normal (added damage; Shifting Band back) | A shifted part resolves exactly as the unshifted part (original type's true step, layers included, never a breaker); damage after equals damage before (9.4) |
| R-2 | Channel target order, allies in areas | `hostileOnly`; ability model 14.3 step 4 order; decided in the step 5 loop (9.4) |
| R-3 | Charges spent on riders that cannot be attempted | `riderWouldBeAttempted` precondition; weapon-flag verdicts (9.4, 13.6) |
| R-4 | Rings undo the party's Frozen, Chilled, Burning | `noCancelOwnSide` default filter (9.4) |
| R-5 | Oiled and Soaked undefined | "Weak" covers condition shifts; shifted fire never pays out Oiled, Burning lands at normal band (9.4, TK-25) |
| R-6 | "Use", ring order, eligible sources | One attack roll or ability resolution; slot order; `UseAbility` and weapon pipelines only (7.1) |
| R-7 | Longreach as a channel | A declared option, paid at step 3, not a channel (6.1 item 11, 9.4) |
| R-8 | Filters on truth leak hidden traits | Party knowledge for party wearers, truth for carriers; revealed as seen in play (9.4) |
| R-9 | Gear-prevention order, counter semantics, entry points, step 7, Kinward trigger, out-of-combat rounds | 7.3; TK-21 |
| R-10 | Yield-to-ward predicate wrong | Yields only when step 4 would spend a ward (7.1). The proposal to drop the yield rule and narrow trinket Warded to major or hard conditions is not adopted: both are confirmed (the runtime limits; the approved Warded text) |
| R-11 | `DuplicateAnswer` keys, scope, tags, duplicates, field | (scope, key) pairs in `answers[]`; tag keys; identity keys; capacity key (7.1, 17.1) |
| R-12 | Tally, Sigil, Oilskin, Emberward, Visor, Weathered details | 13.1, 13.4, 13.5, 13.6, 13.7; cost-step variants (9.1) |
| R-13 | Monster carriers: caps, classification, exclusions, banned pairs, party receipt, TK-23 target | Section 12 table; carrier legality at 15.3 step 4 (TK-23); 15.4 unchanged |
| R-14 | Suppression recompute events | 7.2 |
| R-15 | Rules not checkable; schema gaps; rule numbering | Subject vocabulary, `copyRegistryAllowlist`, `usefulFor[]`, `targets`, generation invariant (TK-27); schema (17.1); numbering convention (18.1) |
| R-16 | Kinward breaches rider ownership | Narrowed to Poisoned, Bleeding, Withered; ownership binds ally answers; Smouldering and Revealed added to 5.7 |
| R-17 | Effect kinds missing from 6.1 | 6.1 items 3 (movement cost), 11–14 |
| R-18 | Stun-proof hero in the boss fight | One trinket Warded per unit per fight; combat-only buttons; envelope hero row |
| R-19 | Offence share overstated | Honest split, 27% offence (13.8, 18.3) |
| R-20 | Sigil of Aid is not "your strike" | Not moved: the client approved the M2 catalogue with Sigil on the finger, and Help is the action that sets up an ally's strike; Study (Scholar's) moved to head, Later |
| R-21 | Thin Rare tier | Effectively one per run noted; reports; second Rare family a Later option (24.3); catalogue unchanged |
| R-22 | Names and labels | Trophy Tally, Porter's Satchel, Mule Strap; (settled) readings relabelled; TK-1 three rows; Helped quoted; `test[]`, `monsterTags` |
| R-23 | Smaller gaps | Capacity exempt from trigger-rate rules; one capacity trinket counts; zero added damage defined; roll concentration per legal set; envelope keys in `ePlusGate`; round-state rows (TK-7); TK-28 for items 7.1; Heartward Lifeline container-only; neck lines on the watch list. Smart-loot de-weighting by floor theme is not adopted (it would change agreed enemies 15.3) |

**Judges' grafts adopted:** lane word and shape glyphs on cards, hidden on
dormant lines (15); per-kind lean as lint only (5.6); the three readings
of resistance (5.8); kind-level carrier tells (12); exclusive subjects
with shared shapes as the validation framing (rule 15); one merged
stacking cap (7.1); roll-concentration and per-property trigger reports
(18.3); capacity +1 + minor as a fallback (now held until the pick report triggers it, 13.7); shared pool, C-split and
kind-flexible variants recorded as Later options with stated triggers
(19). **Not adopted:** floor-theme loot weighting (would change agreed
enemies 15.3); `need.<domain>` tags beyond the existing `requires`
vocabulary.

**Validation framing (graft from strict):** every property declares
`effectKinds[]`, a `subject` (tokens from the closed `trinketSubjects`
vocabulary: condition id, tag, element, universal action or event) and a
`trigger`; each (effect kind, target scope, subject token) triple has
exactly one owning table (rule 15). Subjects are exclusive; shapes may
repeat across tables.

## Appendix B: sources

- The eight parent documents, all in `docs/superpowers/specs/`.
- The lead's trinkets proposal (2026-11-17), synthesised from research,
  a framework, three domain models, two catalogues, three judges and
  three critics, with the client's answers to Q1–Q4 on 2026-11-17 and
  confirmation that the rest of the proposal stands.
- Game references in 4.1 are from memory and are mechanism references
  only.
