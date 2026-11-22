# Items and Inventory: Design

Date: 2026-11-16
Status: Approved, 2026-11-16
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design) and
`2026-11-16-enemies-design.md` (enemies design)

Items marked **(agreed)** were decided by the client on 2026-11-16, either
directly or by choosing the lead's recommendation. Items marked **(lead's
call)** were filled in by the design lead while writing this document and
are confirmed when it is approved. Items marked **(settled)** restate a rule
from a parent document. Items marked **(change)** amend an approved document
and are listed, with an id, in section 23. Once approved, every item here is
binding on the builders.

This is the **generic layer**: the framework every item category plugs
into. Weapons, armour, consumables, scrolls and wands, trinkets, heirlooms
and curses each get their own later design (section 17). No item names or
lore are proposed here beyond illustrative examples.

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. They all live in one
tuning file, `inventory.json` (5.1), which is their only source; item
records hold only per-item facts (`value` inputs, `basePrice`, `size`).
The builders own the tuning numbers and the simulator calibrates them
(section 22), except where a value is marked as a client decision.

**Waves:** M1 is the vertical slice (one class, levels 1–3, one floor,
solo hero, basic loot). M2 is the full run with companions. M3 adds
profile, unlocks and boons. M4 adds breadth (species, multiclassing, more
content, balance passes). **(settled, main design 9)** Later is everything
after M3 that no milestone names, including M4. **(lead's call)**

## 1. Intent

Items are what the party finds, carries, spends and fights with. The
parents settle that capacity is counted in slots of base + Strength with no
weights, that loot is a fixed, curated per-floor budget with no junk, that
there is one loot summary per fight, and that potions, weapon switches and
out-of-class scrolls have fixed action costs. **(settled)** They leave open
where items live, who owns them, what coin is, how identification works,
what an item record looks like and how the categories plug in. This
document closes those questions.

The system should make every find a readable decision taken in a few
seconds, keep the pre-fight choice (what is on the belt) meaningful, give
Strength, Arcana and Leadership real item jobs, and never ask the player
to sort a bag. **(agreed)**

## 2. Goals, non-goals, anti-goals

### Goals

- **One integer per limit.** Capacity, size and stack counts are small
  integers the core computes and shows; the player never counts.
  **(agreed)**
- **The belt is the pre-fight choice.** What a unit can use in combat
  cheaply is what it put on its belt. **(agreed)**
- **Each settled sentence holds as written.** "Take (choose who)", "the
  thief's free slot", "slot cost" and the settled action costs keep their
  meaning. **(agreed, ownership option D)**
- **Items are build-shaping sidegrades.** Most of the power curve stays
  with levels and offers. **(lead's call, from enemies 15.9's "avoid flat
  +N")**
- **Knowledge has a stake.** Arcana decides when dormant power becomes
  usable; nothing is ever a fully unknown item. **(agreed)**
- **Deterministic.** Inventory operations never draw a random number; all
  loot, charges and stock are fixed at floor generation. **(settled, main
  design 6.3; enemies 15.3)**
- At most four things to read per unit in combat. **(settled, ability
  model 11.4)**

### Non-goals

- Weight, bulk, durability or wear. **(settled: weights, stat model 6;
  durability and wear, enemies 2, 15.9; change W-6)**
- A spatial inventory grid. **(agreed, 4.1)**
- A stash or any storage that outlives a camp visit. **(agreed)**
- A meta currency or any coin that survives the run. **(agreed)**
- Ammunition tracking. **(lead's call, recommended to the weapons
  design)**
- Tools as a category. Dungeon checks are party-best skills (stat model
  5.4), and tools would only add bookkeeping. **(lead's call)** There
  are no lockpicks either (traps and locks design 6.11, BQ6).
  **(change TL-26, kept)**

### Anti-goals

- **Loot clutter and mule shuffling.** Sorting junk, moving items between
  packs by hand, "who has the key". **(settled, enemies 2; agreed)**
- **Grind to win.** Coin, selling and trade must never create items or
  loot beyond the fixed, pre-rolled per-floor loot budget, which includes
  the camp channel (10.4) and, Later, the trader (10.9). **(settled, main
  design 2; enemies 2; budget wording lead's call)** The basic vendor's
  supply, tonics, Remedy and mundane bases are priced goods, not loot, and
  are held in check by price and caps (10.3, 10.5). The Ticket lock
  (coin) and the Fed sphincter (supply) buy access to a cache, never loot
  beyond the budget (traps and locks design 7.7, 7.9). **(change TL-32;
  M3)**
- **Bring-the-right-key play.** Carrying one of each resistance item and
  swapping per encounter. **(settled, stat model 2)**
- **Free refills.** Any route that restores a pool without the rest rules'
  costs, including by swapping items. **(settled, ability model 2)**
- **Bookkeeping.** Any count the core could track for the player.
  **(settled, ability model 2)**
- **Potion roulette.** Drinking unknown consumables to learn what they are,
  under permadeath. **(lead's call)**
- **Rules in the display layer.** Comparisons, fit lines, placement and
  prices are core queries. **(settled, main design 2)**
- **Protected content.** **(settled)**

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Stat model 6 | "Carrying capacity \| Item slots: base + Strength. No weights." | Capacity is counted in slots and scales with Strength |
| Enemies 2, 15.1, 15.9 | "No durability, wear or weights" | None of the three exists |
| Enemies 15.8 | "Strength threshold and slot cost"; "Take (choose who), Leave, Take all"; "the only place in the dungeon where inventory is sorted"; about 10 s median per summary | Each item has an integer slot cost. The "only place" sentence is clarified (change I-8). |
| Enemies 15.5 | Steal moves the item "to the thief's free slot, or to the leftovers marker if none is free" | Each unit owns its own items |
| Enemies 15.9 | "Mundane base items are not loot … every loot item has at least one property"; family caps per run; duplicate rerolls | Load-time validation (19.1) |
| Enemies 15.3 | Notables are a fixed per-floor budget per channel, pre-rolled on the loot stream; cost add = value × k | The items design supplies k (12.7) |
| Ability model 7.2 | Drink a potion: bonus action. Switch the wielded weapon: bonus action. Scroll or wand outside your class: action, trained Arcana, a failure uses it up. | Adopted; the switch swaps whole weapon sets (change I-5) |
| Ability model 2, 6.2 | No free refills; no bookkeeping; the pool rule (only `spent` is stored) | Spent counts live on the item instance |
| Ability model 15.3 | "The party carries at most 3 [supply]. How supply uses carrying slots is for the items design." | Answered: none (5.3) |
| Conditions 8, `tonic` row | Bonus action: one minor. Action: one major. "Thrown at an ally (action): minor only." | Adopted; giving a tonic keeps "minor only" |
| Conditions 14 | "At most 2 harmful lingering conditions per unit … Scarred is exempt"; Remedy ends one non-curse lingering condition | Shapes curses (section 13) and the trader's service (10.9) |
| Conditions 18.9, 18.10 | Coated: "its next 3 weapon hits carry the oil's rider". Pitted Armour: "the worn armour's value −1 per level". | Both stay conditions on the unit, never stored on the item |
| Conditions 18.11 | "Encumbered \| Moved to the items design" | Retired (change I-1) |
| Stat model 5.4 | "Presence is always the hero's. The hero is the leader." | Prices read the hero's Leadership only |
| Stat model 7.1, 7.2 | Arcana: "Identify magic items". "Where no cost fits, the check is a threshold with no roll." | Identification is a passive Arcana threshold (section 11) |
| Main design 4.1, 4.7, 9 | Camp: "full rest, a shop or reward"; one starting boon "such as an heirloom item"; "No permanent stat boosts"; M1 is solo, companions arrive in M2 | Scope of coin, heirlooms and waves |

### 3.2 Numeric conventions

**(settled, enemies 3.1)** Every formula uses `FloorDiv`; fractions are
compared by cross-multiplication; percentages are permille integers; rules
use integer arithmetic only. Prices are computed once per query and never
stored.

## 4. Framework decisions

Five framework decisions shape everything else. The client chose all five
on 2026-11-16. **(agreed)**

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Capacity | **Abstract slots per unit, no spatial grid, no weights.** Belt 2 (a feature or offer may raise it to a hard maximum of 3), trinkets 2, pack max(2, 3 + Strength) with item size 1 or 2, an active and a spare weapon set, armour (section 5). | Spatial grid (scored 25 against 44; about 2 s per summary for a handful of shape conflicts per run, see 4.1); pooled bag as primary (kept as the fallback for ownership, 6.4); no limit (Strength's agreed capacity job disappears); weight or bulk (settled out) |
| 2 | Ownership | **Option D: per-unit ownership with party-wide automatic placement and a suggested recipient** (section 6). The pooled bag is the named fallback. | A, one shared bag for everything (breaks "the thief's free slot" and "Take (choose who)"); B, per unit by hand (mule shuffling; four packs are a fake decision); C, pooled pack (the fallback: rewords 15.5 and 15.8, and Strength is felt weakly) |
| 3 | Coin and economy | **A run-scoped party purse, spent only at camp; a camp shop; Sell at one flat rate; Scrap leftovers at the stairs; no stash** (section 10). | No coin (breaks every settled "bought at camp" and cuts 15.7 salvage); coin without selling (Leave becomes pure regret) |
| 4 | Identification | **Always-known basics and obvious properties; consumables, scrolls and wands always identified; at most 1 dormant hidden property on Uncommon-or-better gear and trinkets; a passive Arcana threshold with no roll; camp identifies everything free; equipping reveals nothing; curses Later** (section 11). | Everything known (Arcana's agreed job becomes decoration); identify by use (potion roulette under permadeath); reveal on equip (free equip-scumming makes the threshold irrelevant) |
| 5 | Camp trade (client question 2) | **A basic vendor at every camp** (supply, potions in limited pre-rolled stock, tonics, Remedy, mundane bases). The main design's "a shop or reward" is a choice between a **notables shop** (1–2 notables *(illustr.)*) and a **reward card** (change I-13; the camp reward card is not the enemies 17.5 boss reward card, 10.4). | A shop at some camps only (on a shopless camp, coin, tonic resupply and every settled "bought at camp" break) |

The client also approved, as a **Later** option, the **wandering trader**
floor event (10.9). **(agreed)**

### 4.1 The grid verdict

**(agreed)** A spatial grid would not hurt the game; it would cost a lot
and add almost nothing. Grids pay off when inventory *is* the game (one
character's case as the core loop, or the grid as the build system). Here
loot is about one notable per fight with no junk, the party has four
units, builds come from levels, offers and smart loot, and the summary
target is about 10 seconds.

Three judges scored each approach independently (0–10, higher is better;
averages):

| Approach | Decisions per run | Low friction | Fit with approved docs | Four-unit UI | Build cost (high = cheap) | Fun | Total |
|---|---|---|---|---|---|---|---|
| **Abstract slots per unit, two sizes, belt (chosen)** | 7.0 | 6.7 | **9.0** | 7.0 | 7.3 | 6.7 | **43.7** |
| Pooled party bag, two sizes, belt | 6.3 | **8.0** | 6.7 | **8.0** | **8.0** | 6.3 | 43.3 |
| Spatial grid strip per unit | 6.0 | 4.0 | 4.0 | 3.3 | 2.3 | 5.7 | 25.3 |
| No limit | 2 | 10 | 2 | 9 | 10 | 3 | 36 (one judge) |
| Weight or bulk | 4 | 2 | 1 | 3 | 5 | 3 | 18 (one judge) |

Why the grid lost: about 2 s more per summary (15–25 s if players distrust
the auto-pack); drag, rotate and ghost-placement input that maps badly to a
controller; an authored shape per item; it stretches the agreed integer
wording ("slot cost", "the thief's free slot"); and it pushes against four
things to read per unit. What the grid's feel delivers is "a big thing
crowds out small things"; **one integer `size`, 1 or 2 (bulky), delivers
most of that feel with no geometry.**

The real carrying pressure comes from three per-category caps: the **belt**
(what is at hand in combat), **trinkets** (how much worn magic a unit can
have) and the **pack** (how much room Strength buys).

### 4.2 Kill switch

**(agreed)** Every capacity mechanism ships with a simulator metric that
proves it binds (19.3). If the pack never fills, tighten in this order and
re-measure after each step:

1. lower the pack base (`packBase`);
2. raise the share of bulky finds;
3. lower `stackMax`.

**Only then reconsider anything spatial.** Records keep a single integer
`size`, so a later shape prototype can set `size` equal to the shape's area
without migrating any data. **(agreed)**

## 5. Where items live

**(agreed for the places and counts; details lead's call)**

### 5.1 The tuning file

```json
{ "packBase": 3, "packMin": 2, "bulkySize": 2, "supplyCap": 3,
  "beltSlots": 2, "beltSlotsMax": 3, "trinketSlots": 2, "weaponSets": 2,
  "stackMax": { "potion": 3, "tonic": 3, "flask": 2, "coating": 2,
                "remedy": 1, "scroll": 1, "wand": 1, "talisman": 1 },
  "packInCombat": "action",
  "coinPerValue": 20, "minPrice": 1,
  "categoryPermille": { "weapon": 1000, "focus": 1000, "shield": 1000,
                        "armour": 1000, "trinket": 1200, "consumable": 600,
                        "scroll": 800, "wand": 1000, "talisman": 1500 },
  "leadershipDiscount": { "permillePerPoint": 30, "capPermille": 250 },
  "dormantAllowancePermille": 500,
  "sellPermille": 250, "scrapPermille": 100, "leftoversScope": "room",
  "roomGuardPermille": 250,
  "familyCapPerRun": { "weapon": 2, "armour": 2, "shield": 2, "focus": 2,
                       "trinket": 1, "scroll": 2, "wand": 2, "consumable": null,
                       "talisman": 1 },
  "rarity": { "common":   { "budget": 1, "props": [1, 1], "minFloor": 1 },
              "uncommon": { "budget": 2, "props": [1, 2], "minFloor": 1 },
              "rare":     { "budget": 3, "props": [2, 3], "minFloor": 3 },
              "veryRare": { "budget": 4, "props": [2, 3], "minFloor": 4, "perFloorMax": 1 } },
  "loot": { "rarityDeck": "…", "coinPool": "…", "salvagePermille": 120, "…": "…" },
  "vendor": { "potionStock": [2, 3], "bases": ["item.longsword", "…"] },
  "notablesShop": 2,
  "consumables": { "potionPermille": 1000, "…": "…" },
  "trader": { "floors": [2, 4], "presencePermille": 500, "stock": [2, 3],
              "containersFloor": 1, "carriedFloor": 1,
              "pricePermille": 1250, "barterPermille": 600, "tonicMax": 1,
              "servicePrice": { "identify": 20, "recharge": 40, "tend": 40 } },
  "kindToLocation": { "helm": "head", "hood": "head", "circlet": "head",
    "gloves": "hands", "gauntlets": "hands", "bracers": "hands",
    "boots": "feet", "cloak": "back", "amulet": "neck", "ring": "finger",
    "charm": "none", "capacity": "none" },
  "wornLocations": { "head": 1, "hands": 1, "feet": 1, "back": 1,
    "neck": 1, "finger": 2, "none": 2 },
  "trinketSubjects": ["Search", "…"],
  "trinketDomains": { "props.trinket.head": ["Search", "…"], "…": ["…"] },
  "sharedTrinketAllowlist": [], "copyRegistryAllowlist": ["…"],
  "trinketRarePerFloorMax": 1, "trinketButtonCap": 1,
  "trinketPolicyFilters": { "bigHitPermille": 200,
    "channelDefaults": ["…"], "channelMandatory": ["…"] },
  "ePlusGate": { "minSimRuns": 200, "minPlaytestRuns": 10,
    "freeSlotPickMinPermille": 500, "fullSlotPickMaxRatioPermille": 500,
    "nearTieCampMaxPermille": 250, "nearTieValueMargin": 1,
    "playtesterAsks": [3, 10], "vetoBindMaxPermille": 100 } }
```

`kindToLocation` and `wornLocations` are the armour design's location
rule (armour design 5.4); `ePlusGate` is its E+ gate (armour design 5.10),
whose numbers are the lead's binding numbers, **not tuning**: only the
lead changes them. **(change AR-31)**

A pre-approved lever may raise `trinketSlots` to 3 (armour design 5.10,
E+); it is off by default, and the count stays 2 until the lead switches
it on. **(change AR-47, conditional)**

The trinket keys are the trinkets design's (trinkets design 17.2, full
values there). `trinketRarePerFloorMax` and `trinketButtonCap` are client
decisions, **not tuning**: changing them needs client approval.
`trinketSubjects`, `trinketDomains`, `sharedTrinketAllowlist`,
`copyRegistryAllowlist` and the channel filter lists are a lead's-call
block that only the lead changes; `bigHitPermille` is tuning.
**(change TK-11)**

The `consumables` block is the consumables design's (consumables design
18.1, full keys there, including which are not tuning). The talisman
keys (`stackMax.talisman` 1, `categoryPermille.talisman` 1500,
`familyCapPerRun.talisman` 1; "1 per run" is a cap, not a promise,
consumables design 14.7) are settled with the talisman by the death and
healing design (death and healing design 15, 16). **(change CN-2; change
DH-33)** The block also holds the death and healing design's loot, kit
and camp keys as bare keys beside `offensiveSundryMin`:
`healingSundryMin`, `healingSundryMax`, `healingOffPathPermille`,
`supplyMainPathMin`, `heroKitPotions`, `recruitKitPotions` and
`campHeroPotionTopUp`; all are client numbers, **not tuning**, except
`healingOffPathPermille` (death and healing design 20.1). **(change
DH-33)** `rarity.*.props` ranges apply to gear;
a use-only record meets 19.1 rule 1 with its `use` (12.5). **(change
CN-6)**

The `loot` block is the loot economy design's (loot economy design 10.1,
full keys there, including which are not tuning): the rarity decks
`loot.rarityDeck` beside `rarity` (12.5), the coin pool and salvage share
(10.2) and the camp channel (10.4). **(change LE-2; M1 on floor 1, M2)** `sellPermille` is
250 *(illustr.)*; Sell 200‰ is the fallback if `SellShareOfCoin` passes
40% (10.6; loot economy design 6.4). `notablesShop` is 2, set inside the
agreed 1–2 (10.4). **(change LE-9; change LE-6; M2)**
The item rolls design adds its keys to the same block (item rolls design
9.1, full keys there): lanes, the weight split, usability, memory, Plain
Gifts, offers and the rest. `loot.usability.hardOn`,
`loot.memory.oncePerRunBy`, Scion-line-once at Common and Uncommon and
`loot.plainGifts` are client answers (agreed 2026-11-22, Q1–Q3), **not
tuning**. `loot.trinketScion` lets the trinket `familyCapPerRun` of 1
admit one Scion (12.2). **(change IR-5; change IR-6; change IR-9; M2)**

All values *(illustr.)*, except the `ePlusGate` block and the trinket and
death and healing keys named above as not tuning. `basePrice` (supply,
Remedy, mundane bases) and `size` stay on the records; `stackMax` is read from here by `kind`, and
gear is always 1. A save records the hash of `inventory.json` and resumes
only under the same file, so a tuning change (for example kill-switch
lever 3) applies to new runs and never reinterprets a saved `spent`.
**(lead's call)**

`packInCombat` takes `action | freeOncePerFight | closed`, so changing
how Retrieve works is a tuning change, not a redesign. **Its value
`action` is a client decision; any other value needs client approval.**
**(lead's call)** The `trader` block is Later (10.9).

### 5.2 Places per unit

| Place | Contents | Counts against the pack? | In combat |
|---|---|---|---|
| **Active weapon set** (main hand + off hand) | A weapon; weapon + shield; weapon + focus; light + light; a focus alone; rod + shield; rod + off-hand focus; or a two-hander, the staff or the two-handed instrument, stored at `main` with `off` derived as blocked. Slot rules: weapons design 11.1 **(change W-6)** | No | Attacks and granted abilities |
| **Spare weapon set** (1) | A second full loadout, shield included. This is what "a spare weapon" means (change I-7). It is **empty** only when both hands are empty. | No | The settled **bonus action** swaps the two whole sets, so it is always clear where the shield goes |
| **Armour** (1) | Body armour | No | Passive. Cannot be changed in combat. |
| **Trinkets** (2) | Worn items: rings, amulets, charms, cloaks, helms, gloves, boots; at most one per body location (rings two); never AC (armour design 5.4) **(change AR-30)** | No | Passive plus granted abilities. Cannot be changed in combat. |
| **Belt** (2; a feature or offer may add a 3rd; hard maximum 3) | `beltable` items only: consumables (not Remedy), scrolls, wands and talismans, one stack per slot **(change CN-1; change DH-33)** | No | **The only place consumables, scrolls, wands and talismans are used from in combat** |
| **Pack**: max(2, 3 + Strength) slots | Anything. Size-1 items cost 1 slot; bulky items cost 2. Bulky means two-handers, armour and shields, and only while in the pack. Occupancy is the sum of sizes; a pack index is only an ordering key. | It is the pack | Only through Retrieve (an action) |

Pack sizes *(illustr.)*: Strength −1 gives 2, 0 gives 3, +1 gives 4, +3
gives 6, and the +5 cap gives 8. A typical party of four (+3, 0, 0, −1) has
about 14 slots, so a recruit is also a gain in capacity.

### 5.3 Party counters (no slots)

**(lead's call)**

- **Coin:** the purse (10.1), an integer in party state.
- **Supply:** the settled cap of 3 (`supplyCap`), shown on the party bar.
  It uses **no slots**: putting it in slots too would cap it twice and add
  a "who carries the rations" step with no tactical content (change I-6).
  **Supply is an integer in party state and is never an item instance.**
  Found supply is banked up to the cap; any excess becomes the
  `supplyCount` of the fight's leftovers marker (section 9), which banks it
  up to the cap when reopened. The `supply` record exists only for its
  name, icon and `basePrice` and is never instantiated.
- **Keys and floor tokens:** one row, shown once. They **are** instances,
  at location `party(keys)`, floor-scoped and destroyed at the stairs.
  A floor key is carried by the floor's Keybearer and used automatically
  on walk-in (traps and locks design 6.11). **(change TL-23, kept)**
- **Wax Keys (M2):** a run-scoped party counter, `party.waxKeys`, cap 2,
  size 0. It is never an item instance and not `category: key`, so it
  survives the stairs. It opens one ordinary lock out of combat; its
  rules, channels and depth price are the traps and locks design's
  (6.11). **(change TL-24)**

Records for party counters have `size: 0` (12.3).

### 5.4 Capacity rules

**(lead's call)**

- **The cap is hard, and there is no Encumbered condition** (change I-1).
  Nothing can be added over capacity: Take becomes Swap, and a mid-fight
  pickup goes to `claimed` (7.1).
- **Capacity reads Strength excluding conditions.** Gear, level and offers
  count; a condition does not. Heightened (Strength) from a potion
  therefore cannot buy slots before looting that then vanish. (No approved
  condition lowers Strength; Sapped shifts damage one ladder step.)
- **Pack capacity** = max(`packMin`, `packBase` + Strength excluding
  conditions) + the sum of equipped capacity grants. Belt capacity =
  `beltSlots` + belt grants, never above `beltSlotsMax`. Trinket capacity =
  `trinketSlots` + trinket-slot grants.
- **Capacity falls only** when a Strength-raising item, offer, capacity
  trinket or belt or trinket-slot source is removed. **Nothing is ever
  ejected.** The pack, belt or trinket row shows hatched "over capacity"
  slots and cannot take anything until it is back under the cap; the items
  in it stay usable. This mirrors the pool rule that losing a source never
  pushes a pool negative (ability model 6.2).
- **Equip now or a camp equip that displaces a capacity trinket:** the new
  capacity is computed after the swap, then the displaced item is placed
  (6.2).
- Capacity, used slots and the over-capacity flag are derived and never
  stored. **(settled, stat model 6)**
- **Capacity trinkets** give at most a flat +1 or +2 pack slots and take a
  trinket slot. Infinite storage (the SRD bag of holding as written) is
  banned.

### 5.5 Combat HUD

**(lead's call)** In combat the HUD shows only the belt and a spare-set
icon per unit, to respect four things to read. Equipment, trinkets and the
pack appear only in the party view and the loot summary.

## 6. Ownership and placement

**(agreed: option D; rules lead's call)**

### 6.1 Per-unit ownership

- In the data, every physical item lives on exactly one unit, or in
  exactly one non-unit location (18.1).
- The player chooses a unit **only when that choice means something**:
  equipping an item, belting it, or putting it in a spare weapon set.

### 6.2 Party-wide placement

Anything going to a pack is placed by the core, in this order. Only a
unit whose free pack room is at least the item's `size` qualifies.

1. the chosen unit's pack;
2. otherwise the pack of a unit whose belt holds a matching item: the same
   `kind` for consumables, the same `category` for scrolls and wands (gear
   skips this step); the first match in unit order;
3. otherwise the unit with the most free room;
4. ties go to unit order.

**Moves fill stacks first** (lead's call): a stackable item first fills
matching stacks in the destination place, lowest slot index first (ties by
instanceId), then takes a free slot there. A remainder that fits nowhere
stays at its source; for a mid-fight pickup it becomes `claimed` (7.1).

A Swap is needed only when the **whole party** is out of room. The party
bar shows capacity as one line with the members' shares, for example
"Pack 11/14 (C1 6)", so Strength's job stays visible without four
counters.

### 6.3 Suggested recipient

The summary marks one member with ★: usable first, then the largest gain,
then free room, then unit order. **Gain** is an integer: tenths of expected
damage per turn against `floorTypicalAc` (the enemies 6.2 target-table AC
at the floor's standard room level, lead's call) for weapons; for armour
and shields, AC change × 10 + value difference − the stealth term (−10 if
the item would newly make the recipient the party's worst stealth
member); other items rank by value. Strained recipients rank after every
usable, unstrained one; for pieces, a member with a free slot and a free
matching location comes first (armour design 16.4) **(change AR-32)**. One click on
another portrait overrides it. Suggestion quality is a release
requirement (19.3: Take all agrees with the scorer's best placement at
least 90% of the time).

### 6.4 Fallback

If playtests still find "who" a chore, switch to the **pooled pack**
(capacity = the sum of each member's pack, with equipment and belts kept
per unit). Only the placement rule and two sentences of enemies 15.5 and
15.8 change. **(agreed as the named fallback)**

## 7. Action costs

**(settled where marked; the rest lead's call)**

### 7.1 Table

| Act | In combat | Out of combat |
|---|---|---|
| Use a belt item | Its declared cost. **(settled)** Potion: bonus action. Tonic on yourself: bonus action (minor) or action (major). Tonic thrown at an ally: action, range `throwAllyRange`, minor only. Scroll or wand in class (the reader's class is in `classTags`, any spell on its list): its ability's cost; no slot is spent; a scroll casts at its own tier and is never upcast. Outside your class: action + trained Arcana against 10 + tier; a failure uses it up. **(change CN-3)** | As any exploration action. Buffs obey the 10-turn cap. |
| **Administer** a belt potion or tonic to an adjacent ally (a trinket reach step may extend "adjacent" to 2 squares; trinkets design 6.1) **(change TK-28)** | The giver's **action**. The receiver spends nothing. Tonics are **minor only**, as when thrown. A potion does **not** lift Downed. Not on a Downed unit (a potion or healing scroll; a tonic stays legal, minor only) **(change CN-3)**. **Its purpose** is a receiver that cannot act or whose bonus action is committed; when the receiver could drink a handed-over dose itself, the button carries the hint "Hand over is cheaper". **(lead's call)** | As above |
| **Potion thrown at an ally** | The thrower's **action**, range 5 (`potionThrowRange`), line of sight, no roll, a willing ally only, never yourself; heals `thrownPotionPermille`. It never lifts Downed and is not used on a Downed unit (consumables design 11). **(change CN-4, agreed 2026-11-17, Q4; change CN-3)** | Not offered (Hand over is free) |
| Swap weapon sets | Bonus action **(settled)** | Free |
| Pick up or seize an item in or next to your square | **Free interaction (settled); it always succeeds.** A consumable goes to a free belt slot or a matching stack. A weapon goes to an empty spare set; wielding it is the set swap (bonus action, settled). Otherwise the item (or a stack's remainder, 6.2) becomes **claimed**: the unit holds it, it is unusable this fight, and it is resolved at the summary. Disarm seizure (enemies 15.5) therefore always works for a full unit. **If the party flees**, every claimed item goes to the flee pile in its room (`ItemLeft`); belt and spare-set pickups stay where they are. **(lead's call)** | n/a (out-of-combat loot comes through a summary) |
| **Hand over** to an adjacent ally (a trinket reach step may extend "adjacent" to 2 squares; trinkets design 6.1) **(change TK-28)** | The giver's free interaction. **Only from the belt or the weapon sets, never from the pack.** Never a talisman in combat **(change CN-3; change DH-34)**. One dose moves at a time. It lands on the receiver's belt or empty spare set, or is refused with a reason. | Free, at any distance, through the party view |
| **Take from a Downed adjacent ally** | The taker's free interaction. Belt or weapon-set items only; never a talisman in combat **(change CN-3; change DH-34)**. Lets the party recover the last heal. | Free |
| **Retrieve**: pack to belt or to an empty spare set | **Action** (`packInCombat: action`, a client decision). Were `freeOncePerFight` ever approved, the unit's fight state carries `retrieveFreeUsed`, cleared at combat start. Never a talisman in combat **(change CN-3; change DH-34)**. | Free |
| Belt to pack; reshuffling | Not allowed | Free |
| Equip or remove armour or trinkets | Not allowed (`NotInCombat`) | **Only at a short rest, at camp, or with "Equip now" on a newly taken item at its summary** **(agreed)**. "Newly taken" means the item's first entry into the party this run (8.3). |
| Drop | **No command** | No command. Items are shed by Leave or Swap at a summary, Sold at camp, or bartered (Later). |
| Party unit Disarmed (Later) | The weapon falls into its square. A bonus action re-wields it from there or swaps sets (settled). **At combat end it returns to its owner** (change I-4): to its original weapon-set slot if that slot is empty; otherwise placed through 6.2 with the owner as the chosen unit; otherwise it joins the summary as a card. A Downed owner still receives it. Only a weapon still at `ground(*)` at combat end returns. **(lead's call)** | n/a |

"Drink a potion" (ability model 7.2) means a potion on your own belt
(change I-5).

Opening a container out of combat costs the clicks of the loot sources
design's click table, and a quiet summary auto-closes (8.1; loot sources
design 4.6) **(change LP-8; M2)**. **Pending request (loot sources design
4.6, 11):** the items owner confirms that a Plain open costs an action in
combat; until it rules, container work in combat follows traps and locks
design 6.11 and 6.13 as written.

### 7.2 Why these rules

- **Retrieve has no bypass.** No in-combat command moves an item between a
  pack and any other place except Retrieve. This is a run-time invariant
  (19.2). It closes both bypass routes: dropping an item and picking it
  up, and handing a pack item to a neighbour's belt. That is also why there
  is no Drop command: dropping only ever loses value.
- **Armour and trinkets are rest-gated** **(agreed)**. Enemies are visible
  before engagement. If out-of-combat swaps were free, a unit could carry a
  fire-resist ring in its pack and swap it per encounter, which is exactly
  the stat model's "carrying one of each and swapping". Weapon sets and
  belts stay free to rearrange out of combat, because the settled set swap
  already prices weapon changes.
- **Moving items never costs ticks and never refills a pool.** Spent
  counts live on the instance, so handing over a half-spent wand changes
  nothing.

### 7.3 Greyed reasons

Every greyed button names one reason **(settled, ability model 15.4)**:
`NotInCombat`, `BeltFull`, `PackFull`, `NotBeltable`, `NotAdjacent`,
`NoFreeInteraction`, `StrengthThreshold`, `HardRequirement`, `Untrained`,
`OverCapacity`, `RestOnly`, `Clinging` (Later), `NoCoin`, `Unsellable`,
`SupplyFull`, `OutOfStock`, `CampChoiceMade`, `LocationOccupied`,
`ReactionCap`, `ButtonCap`, `DuplicateAnswer`, `AlreadyWarded`,
`AlreadyWardedThisFight`, `TrinketHelpedLimit` (trinkets design 15)
**(change TK-10)**, `FlaskThisTurn` (consumables design 18.4) **(change
CN-9)**, `Guarded` (Grab and attacks on a Dormant Windfall Truant, and
Take on its spilled coin, while its host group is unresolved; Steal is
the only verb; loot sources design 4.7.5, 4.7.8) **(change LP-3; M3)**,
and Later `ServiceUsed` and `NotBarterable`. `Untrained`
also covers armour and shield training (armour design 7.2, 16.3)
**(change AR-33)**. `DeadLine` is a warning verdict, never a suppression
(trinkets design 15) **(change TK-10)**.

## 8. The loot summary

**(settled: one per fight, enemies 15.8; layout lead's call)**

### 8.1 When it opens

- At combat end, after banked level-ups resolve **(settled)**.
- When a container opens.
- When a leftovers or flee-pile marker is reopened.

A Steal result does **not** open a summary: the item goes to the thief's
pack if it has room for the item's size, otherwise to the leftovers marker
in that room, exactly as enemies 15.5 says, and the party can reopen that
marker at once. **(settled; size reading lead's call)** A Steal on a
Dormant Windfall Truant takes its card only, by the same rule (18.1;
loot sources design 4.7.8) **(change LP-6; M3)**.

**Auto-close of quiet summaries (loot sources design 4.6; change LP-8;
M2).** Out of combat, a summary holding only coin and non-healing
sundries that all fit takes everything and closes itself after
`loot.summaryAutoCloseMs` *(illustr. 1.5 s)*, logging to the floor strip
(`SummaryAutoClosed{containerIds, coin, sundries}`). "All fit" uses
party-wide placement (6.2): each sundry goes to the suggested recipient
(6.3), else the next member with room; if any item fails to place, the
summary stays open. A summary with a notable, a healing sundry or any
choice stays open. The timer pauses while pointer or controller focus is
on the summary; a settings toggle (on by default) and the duration
setting are offered; each auto-closed summary's log line reopens a
reassign view for its sundries until the party leaves the room.

There is one screen and never a second UI. The camp shop and, Later, the
wandering trader reuse its card and comparison components. **Glean**
(optional; ships only if playtest shows players want it) opens every
visible Plain unlocked container in a room into **one merged summary**,
still this one screen, and it auto-closes under the same test (loot
sources design 4.6) **(change LP-9; M2)**.

### 8.2 Layout

```
┌ VICTORY · +140 XP (level scaling −10%) · +23 coin (salvage 8) · +1 supply ─────┐
│ Floor fact: "The crypt's guardians answer to a bell."                           │
├─────────────────────────────────────────────────────────────────────────────────┤
│ [1/1] ◆ UNCOMMON  EMBERCLEAVER of ? · family: longsword                         │
│   • Hits apply Burning            ? dormant · Arcana 13 vs 15                   │
├──────────────┬──────────────┬──────────────┬────────────────────────────────────┤
│ HERO ★       │ C1           │ C2           │ C3                                 │
│ +21 (tenths) │ +4           │ untrained    │ Strained: needs Str +1             │
│ fits: spare  │ fits: pack   │ fits: pack   │ fits: pack                         │
├──────────────┴──────────────┴──────────────┴────────────────────────────────────┤
│ [Take → Hero] [Equip now] [Leave]       Pack 11/14 (C1 6)    [Take all] [Done]  │
└─────────────────────────────────────────────────────────────────────────────────┘
```

- **Header:** the experience line with level scaling; the coin line, with
  found coin and 15.7 salvage shown separately; auto-banked counters ("+1
  supply", or "supply full: left"); any floor fact.
- **Cards (at most 3 new items per fight, settled):** name, family, rarity
  as frame shape plus a text label (never colour alone), known properties
  as one-line verbs, the dormant property as one hidden line carrying the
  Arcana readout ("? dormant · Arcana 13 vs 15"), which replaces the
  separate "?" pip, with the only other "?" in the name's suffix epithet
  ("Strider's Boots of ?") **(change IR-9; M2)**, an "Arcana identified"
  tag, and a cursed badge once a curse is known (Later). A reopened marker
  that holds more than 3 items pages in groups of 3 in instanceId order;
  Take all walks every page. **(lead's call)**
- **Comparison strip (core-computed):** one portrait per member with the
  gain (6.3), training and Strength fit, and a **fit line** (renamed
  from "fit verdict", so it is never confused with the item verdict;
  change IR-9): "fits:
  spare"; "fits: pack"; "fits if Longsword leaves" (the core suggests the
  lowest-value displacement: bulky first, then value, then instanceId);
  "Strained: needs Str +1" (the soft threshold, weapons design 10.1; the
  item can still be carried and wielded) **(change W-6)**. For armour,
  shields and worn items: "cannot wear: untrained"; "Strained: needs Str
  +1" or "+2" (a warning, never a refusal); "replaces X (feet)" (armour
  design 5.8, 16.3) **(change AR-33)**.
- **Merged card (item rolls design 6.1; change IR-9; M2):** a mark line of
  at most 2 marks (Answers here > Plays with > +1 line > Suits <name>);
  each member's footer reads, in this order, the `ItemVerdict` glyph and
  word in a footer badge (▲ upgrade, ⇄ another way, = redundant, ▽
  lesser, ✕ cannot use; item rolls design 4.14), the 6.3 gain, then the
  fit line ("▲ upgrade · +2 · fits: spare"). Where this card and the
  item rolls design differ, the item rolls design wins.
- **Suggested recipient (★):** 6.3.

### 8.3 Actions

| Action | What it does |
|---|---|
| **Take** | Places the item: equip if the matching slot is empty and the item is usable (armour and trinkets only on the item's first entry, as for Equip now); else the spare set (weapons); else the belt (merging into a stack first); else the pack, placed party-wide (6.2). When the party is full and the core's displacement (8.2) is a **pure-value swap** (a pack item that no member could use as an upgrade and whose value is below the new item's), Take reads **"Take (leaves *item*)"** and does it in one input. **(lead's call)** |
| **Equip now** | Shown when the item is an upgrade for the selected member **and the item is entering the party for the first time this run** (the instance's `everHeld` flag is unset). Otherwise greyed `RestOnly`, so an item left in leftovers and taken again cannot be worn outside a rest. The displaced item goes to the pack, or to leftovers if every pack is full (at camp, never to leftovers: 10.3). The one way to change armour or a trinket outside a rest. **(agreed; first-entry rule lead's call)** |
| **Swap** | Appears only when the party is full. Expands slots inline; one click picks the item to displace, which goes to leftovers **(settled)**. Only pack, belt or spare-set items can be displaced, never worn armour, trinkets or the active set. **(lead's call)** |
| **Leave** | Sends the item to leftovers. **Done** leaves every card not yet taken. |
| **Take all** | Applies each suggestion in instanceId order, including pure-value swaps, and **stops at the first item that would need a real swap** (a choice between items someone could use) |

### 8.4 Input and time

- **Keyboard and controller:** 1–3 select a card, arrow keys select a
  portrait, T takes, E equips, L leaves, A takes all, Enter is Done. No
  drag precision anywhere. (Remappable actions, settled main design 3.)
- **Time estimate** *(illustr.)*: about 35% of summaries have no item (2–3
  s); about 50% have one item that fits (4–9 s); about 10% need a swap
  (11–15 s); the rest have 2–3 items (15–25 s). **Median about 5–7 s**,
  under the settled 10 s target. The simulator re-runs this estimate under
  the sell-maximising policy (carry everything to camp), where late-floor
  packs are full; pure-value swaps cost one input there, so only real swaps
  count toward the swap share (19.3).

## 9. Leftovers, flee piles and the stairs

**(lead's call unless marked)**

- **A leftovers marker is fixed where it was created**: the summary
  location or the container. It never moves. New leftovers merge into an
  existing marker **only if it is in the same room** (`leftoversScope:
  room`); otherwise they start a new marker. Reopening a marker means
  walking back to it. This closes the "marker follows you" free stash.
  (change I-8) "Same room" means the same `roomId`: an Alcove shares its
  parent's, a Portal's squares belong to the lower-index adjacent room
  (off-path: the lower `branchIndex`), arena-adjacent doorway squares
  belong to the arena, and the floor-1 Bypass arm's squares to room
  Gb−1; leftovers read `effectiveIndex` (change FL-13; floor layout
  design 3.4, 11.1).
- **Flee pile (settled, enemies 15.6):** items on the ground when the party
  flees stay as a pile marker in that room. It never merges. Enemies never
  take it, but reaching it means going back past whatever is still there.
- **A leftovers marker may also hold supply** as an integer `supplyCount`
  (5.3). Reopening it banks the supply up to the cap.
- **At the stairs:** the stairs stand in the **Stairhead**, a small room
  behind the arena, reached through the Bossgate once `BossDefeated`
  opens it; floor 5 has none (the run ends) (change FL-6; floor layout
  design 6.11). **M3:** a Way Back Latch from the Stairhead, built off by
  default (floor layout design 6.9). A warning lists what is left
  behind. On descending, leftovers and flee piles are **scrapped** to
  coin at the scrap rate *(illustr. 10% of price)* (10.6). A marker's `supplyCount` and keys are
  simply lost. **An heirloom is lost, never scrapped.** **(agreed: Scrap)**
  Every other floor-scoped instance (in an unopened container, carried by
  a living monster, on the ground, or in trader stock) is destroyed
  (`ItemDestroyed{lost}`) with no coin. **(lead's call)**
- **Steal with a full thief:** the item goes to leftovers **(settled,
  enemies 15.5; 8.1)**.

## 10. Coin, camp and trade

**(agreed: framework decisions 3 and 5, and the wandering trader as
Later; details lead's call unless marked)**

### 10.1 The purse

- **One run-scoped party purse.** Coin is spent only at camp (and, Later,
  at a wandering trader, 10.9). It is lost at run end: there is no meta
  currency and no grind to win. **(agreed)**
- In M1 the coin line shows as a counter with nothing to spend (enemies
  15.8's M1 summary includes it).

### 10.2 Coin income

- **Found coin:** containers and caches. Each floor has a fixed coin pool
  per party, 35 / 50 / 70 / 90 / 0 *(illustr.)* at `companionCap` 3 and
  35 / 50 / 70 / 75 / 0 at cap 2, keyed by the build's `companionCap` and
  never by the actual party **(agreed 2026-11-22, Q4)**. Found coin is the
  pool less the salvage share, split into integer lumps by socket rank
  over containers and caches in the draw-free LootBind pass, with a
  main-path floor of 17 on floor 1 and lock lumps carved first (loot
  economy design 6.4). **(change LE-7; M1: the floor 1 pool and
  main-path floor; M2)** **M2:** on floors 2–4, 20% of
  found coin *(illustr.)* is carved onto one carrier, the **Heavy Purse**,
  dropped on resolve; a Steal takes one of {notable, purse, key} (loot
  economy design 8.3). **(change LE-16)**
- **Salvage (enemies 15.7):** a 12% share (`loot.salvagePermille` 120
  *(illustr.)*) of the fixed per-party coin pool, pre-assigned at
  generation in LootBind per fight, then per armed unit; each resolved
  unit pays its share, Cowed groups pay none, and killing more never pays
  more than the pool **(agreed 2026-11-22, Q3)**. The loot summary shows
  the fight's salvage total plus a line per non-zero share. The I-11
  ratio (10–15% of found coin; change I-11 fixes the base as found coin)
  holds by construction (loot economy design 6.4). **(change LE-8; M1:
  the floor 1 share; M2)**
- **Floor 5 pays before the arena (agreed 2026-11-22, Q1):** no camp
  follows it, so its coin pool and salvage are 0 and their lines are
  hidden; every coin fallback there becomes sundry doses. The floor 5
  boss share is the **Viaticum**, a choose-1-of-3 offer of Rare wands and
  tier-3 scrolls usable by the party, in a main-path container after the
  lair; the final signature drops as a **Memento** (12.5) (loot economy
  design 6.11). **(change LE-5; M2)**
- **Sell and Scrap** are a **separate stream**, reported separately.
  "Salvage" keeps its 15.7 meaning only; the item actions are called
  **Sell** and **Scrap**.
- Every change to the purse emits `CoinChanged{delta, source}` (18.3).

### 10.3 The basic vendor at every camp

**(agreed)** Every camp has a basic vendor selling:

- supply (within the settled cap of 3; greyed `SupplyFull` at the cap);
- healing potions, in **limited, pre-rolled stock** (`vendor.potionStock`;
  greyed `OutOfStock` when sold out);
- tonics and Remedy;
- mundane bases (`vendor.bases`), which come only from here and from kits
  **(settled, enemies 15.9)**;
- **(agreed 2026-11-21, Q3; M2)** 1 Wax Key, **only when the party has no
  Mechanics-trained member**: the stock reads the companions gap card's
  "Mechanics met" test on its camp-entry snapshot (CMP 7.3 G2) and draws
  nothing. Limited stock of 1 (`vendor.waxKeyStock`) at that camp's depth
  price; greyed `OutOfStock` when sold and `WaxKeysFull` at the cap of 2;
  a purchase raises `party.waxKeys` and creates no instance. Hiring a
  Mechanics companion at that Muster does not withdraw it (traps and
  locks design 6.11). **(change TL-25)**

**(lead's call)** Only the potions and the conditional Wax Key are limited
stock, as the client decided; tonics, Remedy and bases are not stock-limited, and their price is
the knob (10.5; conditions 22). Unlimited goods are created at purchase
(`ItemCreated`, no random draw). Vendor and shop wands and scrolls start
full (`spent = 0`), since camp refills wands anyway.

**Buying and room at camp (lead's call):** there is no floor, room or
stairs at camp, so **nothing at camp ever goes to leftovers**. A purchase
is placed through 6.2; Buy is greyed `PackFull` when no member has room
for it, and the player Sells first. Camp is a rest, so the party view
equips armour and trinkets freely, but an equip there that displaces an
item needs room for it (otherwise greyed `PackFull`).

**Leaving camp:** unbought stock (vendor potions, shop notables) is
destroyed (`ItemDestroyed{lost}`); an unbought vendor Wax Key lapses with
`vendor.waxKeyStock` (no instance, no event). **(change TL-25)**

### 10.4 Shop or reward

**(agreed; change I-13)** The main design's "a shop or reward" is a choice
at each camp between:

- a **notables shop**: 2 notables *(illustr.; set inside the agreed 1–2,
  change LE-6)* for coin; or
- a **reward card**.

**(lead's call)** The **camp reward card** is a separate screen from the
enemies 17.5 boss reward card, which is shown before camp and is
unchanged. What the camp reward card offers belongs to the camp and hub
design (section 24); the items design fixes only that any item it offers
comes from the floor's `camp` channel below and counts in the budget.

**Choice state (lead's call):** camp state `choice: unset | shop |
reward`, set by an explicit command and irreversible. Browsing the shop
does not commit; buying a notable needs `choice = shop`, and after the
reward is chosen the shop's notables are greyed `CampChoiceMade`. The basic
vendor is open either way.

**Stock is pre-rolled** in floor N's loot draw (enemies 10.5 step 3,
change I-10) as a **`camp` channel of the floor loot budget**: its count
(`notablesShop`) and items are drawn on the loot stream after the field
channels and wand charges, under the same family caps and owned-base
rerolls, and the vendor's limited potion stock is drawn right after it.
The camp channel is part of the fixed, pre-rolled budget, so the anti-goal
"never beyond the budget" holds (section 2). It is drawn whether or not the
shop is chosen. Stock is deterministic and cannot be save-scummed.
**(lead's call)**

**Camp channel (change LE-6; M2):** 2 entries at every camp 1–4, **graded for
floor N+1** (tier and `minFloor` read N+1; family caps and owned bases
read N), never Very rare; bought camp stock never counts toward floor
N+1's per-floor tier caps. A Wax Key drawn for TL's camp notables slot is
a third display entry outside the budget (loot economy design 6.3). **No
camp channel after floor 5 (agreed 2026-11-22, Q1; change LE-5).**

**Secret caches:** a cache behind a Hairline secret counts as a
container in the containers-and-secrets channel (enemies 15.3); secrets
are optional content only
(change FL-17; floor layout design 9.2).

**Family caps for stock (lead's call):** within floor N's own draw, every
drawn item (camp and trader stock included) counts for duplicate-base
rerolls and family caps, so one draw never repeats itself. For later
floors' draws, camp and trader stock counts toward family caps **only once
bought or bartered**, so an item the party could not afford never closes a
family for the rest of the run. Dreamed options (the unchosen options
of an offer set) never count (loot economy design 6.3, change LE-4; M2),
though they do write Scion-line memory, `oncePerRun` and gear floor keys
(item rolls design 4.7; change IR-6; agreed 2026-11-22, Q2).
Floor N+1 is generated when the party
leaves camp for it (it already reads owned bases, enemies 10.5 step 3),
after every purchase on floor N and at its camp is known, so this stays
deterministic; that generation and the camp-exit save are one atomic
write (loot economy design 6.10) **(change LE-14; M1)**. The run state keeps the per-family count.

### 10.5 Prices and pegs

**(lead's call unless marked)** All integer, in this order:

1. **Base price per dose** = `FloorDiv(assessedValue × coinPerValue ×
   categoryPermille, 1000)`, or `basePrice` for mundane and shop-only goods
   (supply, Remedy, bases). Gear is one dose.
2. **Assessed value:** the value of the known properties plus, per still
   dormant property, `FloorDiv(rarity budget × dormantAllowancePermille,
   1000)`. Once identified it is the true value. So a price, Sell value or
   barter credit never reveals how strong a hidden property is.
3. **Discount** `d` = clamp(`permillePerPoint` × the hero's Leadership
   modifier, 0, `capPermille`), where the Leadership modifier is Presence
   plus proficiency if trained in Leadership (doubled with mastery). It
   reads **the hero only** (stat model 5.4, 7.1), never a party maximum,
   and it is never a surcharge: a hero with Presence −1 and no training
   pays the base price.
4. **Camp price** = max(`minPrice`, `FloorDiv(base × (1000 − d), 1000)`).
5. **Trader price** (Later, 10.9) = max(camp price + 1,
   `FloorDiv(FloorDiv(base × pricePermille, 1000) × (1000 − d), 1000)`).
   The discount applies once, to the undiscounted base, and the trader is
   always strictly dearer than camp.
6. **Sell, Scrap and barter credit** = `FloorDiv(base × count × rate,
   1000)` per instance, with the rate `sellPermille`, `scrapPermille` or
   `barterPermille`; `count` is the doses left in a stack (1 for gear), and
   a wand's `spent` is ignored. A marker's Scrap is the sum of its
   instances' results.

- **Sell, Scrap and barter read the undiscounted base**, so Leadership
  never creates a buy-and-sell loop.
- **Pegs:**
  - healing per coin from potions is always **below** what a supply buys
    (one short rest's Recovery for the whole party), so coin cannot replace
    the agreed attrition limit. **P1:** `supply.basePrice × potionPermille ≤
    potionPrice × pegMarginPermille`, checked at party size 1 on camp
    prices after the Leadership discount and `minPrice` (consumables
    design 9.3) **(change CN-5)**;
  - **P2:** the Remedy price is above the supply price **(change CN-5)**;
  - **P4:** the basic vendor never sells a damage record **(change
    CN-5)**;
  - tonic and Remedy prices are the condition-economy knob (conditions 22:
    "tonic supply becomes the real balance knob").
- Every vendor- or camp-sellable record has a price above 0 (19.1).

### 10.6 Sell and Scrap

- **Sell** happens at camp only, at one flat rate *(illustr. 25% of
  price; Sell 200‰ as the fallback, loot economy design 6.4)*, with no
  haggling. **(agreed)** **(change LE-9; M2)** Heirlooms, keys and `unsellable`
  items sell for 0 and show `Unsellable`. A `kitBound` instance
  (`boundToUnitId` set, 18.1) gets the same `unsellable` treatment: it
  sells and scraps for 0 and shows "Unsellable: kit" (companions design
  7.6) **(change CMP-21)**. Camp identifies everything first
  (section 11), so Sell always reads the true value.
- **Scrap** applies only to leftovers and flee piles at the stairs
  *(illustr. 10%)* (section 9). **(agreed)**
- Both use the integer order in 10.5.
- Selling never produces an item, and loot is fixed per floor, so there is
  no grinding.

### 10.7 No stash

**(agreed)** A stash invites hoarding a resistance kit for the ice floor
(bring-the-right-key) and adds a second management screen. Revisit only if
playtests report painful forced Leaves; if one is ever added, it is about 4
slots, camp-only and run-scoped. **(lead's call for the revisit shape)**

### 10.8 Anti-hoarding (consumables)

- **Belt cap:** only the belt is cheap to use in combat. Out of combat the
  pack, not the belt, holds scrolls, so the belt does not bound them
  (consumables design 8). **(change CN-9)**
- **`stackMax` per kind.**
- **No stash.**
- **Limited potion stock per camp,** pre-rolled.
- **Potion healing per coin below supply.**
- **The companion scorer suggests "drink it"** when it values the potion.
- **Automatic triggers** for insurance (the talisman; consumables design
  14; death and healing design 15, 16) **(change CN-9; change DH-35)**.
- **A simulator report** of consumables unused at each camp, and of
  coin-funded against supply-funded HP (19.3).

Banded consumable strength is **not** counted as anti-hoarding: it makes
saving free, so it is neutral at best. **(lead's call)**

### 10.9 The wandering trader (Later)

**(agreed as a Later option; details lead's call unless marked)**

A rare floor event: a trader camped in a side room, offering a few items
and one service, for coin or barter.

**Placement (agreed):**

- **At most one per floor**, placed in an **off-path side room**.
- **Visible on the map once seen**, as a marker like a leftovers marker
  (`TraderDiscovered`).
- **Never interrupts exploration or combat.** Nothing opens on its own:
  the party steps adjacent and chooses Trade.
- **(lead's call)** Floors 2–4 only (`trader.floors`; never the
  solo-tuned floor 1 or the deep floor 5). Two draws on the **dungeon
  stream** at the floor plan (enemies 10.5 step 2), right after the slots
  are assigned and **always both, whatever their outcome**, so the stream
  position never depends on them: the presence roll (`presencePermille`,
  *illustr. 50%*) and the stock count (`stock`, *illustr. 2–3*). If
  presence succeeds but the plan has no eligible off-path side room, there
  is no trader; the draws are still consumed.
- **(lead's call)** The trader is a **non-combatant map object, not a
  unit.** It cannot be attacked, stolen from, cowed or intimidated; it
  never notices the party, makes no noise and never joins a fight; it is
  not in the encounter budget or the cluster budget. Trading is closed
  while combat is active anywhere on the floor (greyed: `NotInCombat`).
  Opening it and trading cost no ticks, as a loot summary does.

**Stock (agreed):**

- **2–3 items, pre-rolled from the floor's notable loot budget** on the
  loot stream. They are **part of the budget, never additional**, and are
  fixed at generation like every notable (deterministic).
- **Where the count comes from (lead's call).** After the loot step draws
  the per-channel counts (enemies 15.3 step 1), the trader count is
  carved out of the field channels, never the boss:
  - first from the **containers** share, keeping at least
    `containersFloor` *(illustr. 1)* container or secret notable;
  - any rest from the **enemy-carried** share, before carrier reservation
    (enemies 10.5 step 4), keeping at least `carriedFloor` *(illustr. 1)*;
  - traderCount = min(drawn stock, what both guards allow). **Below 2,
    no trader spawns** and the counts are left as they were.
  So exploration loot and carriers never vanish on a trader floor, and the
  boss is untouched.
- **Rules for the draws (lead's call).** Trader items obey smart loot,
  family caps and duplicate rerolls like any notable (family caps for
  later floors: 10.4). On the trader channel the reroll exclusions also
  exclude supply, healing potions and tonics. **A trader slot is never
  substituted** (enemies 15.3 step 2): if its table empties or its
  rerolls run out, the slot returns to the **containers** channel and is
  redrawn there after the trader draws, where the normal substitution
  applies, so the per-channel total still balances. A trader left with
  fewer items still appears, with what it has and its service.
- **Never sells supply or healing potions,** which protects attrition.
  Tonics only in very small amounts: **(lead's call)** at most 1 tonic
  dose (`tonicMax`), and it is **no draw**: the containers channel's last
  tonic instance (highest instanceId) moves to `trader(markerId)`. With
  none, the trader has no tonic. So the tonic comes from the floor's own
  allotment and is never added.
- **Draw order (lead's call)**, inside enemies 10.5 step 3: the per-channel
  counts; the trader carve (no draw); the items in channel order
  (containers, enemy-carried, boss, **trader**, then any returned trader
  slots on the containers channel); wand charges in item order (every
  wand, the trader's included); the camp channel and vendor potions
  (10.4); the trader's tonic move (no draw) and **service** draw. A floor
  without a trader consumes the loot stream exactly as before.

**Prices and payment:**

- **Prices above camp (agreed):** the trader price in 10.5 step 5, which
  is always strictly above the camp price. **(lead's call for the
  multiplier `pricePermille`, illustr. 1250, and the single discount)**
- **Coin (agreed).**
- **Barter (agreed; rate lead's call):** trade one carried item for one of
  the trader's. The offered item is credited at `barterPermille`
  *(illustr. 60%)* of its undiscounted base (10.5 step 6, using its
  assessed value if it is not identified); if the credit is below the
  trader's price, the party pays the difference in coin; a surplus is
  **lost** (no change is given). The rate sits between Sell (25%, change LE-9) and full
  price, so barter is the trader's better deal, but it never pays out coin.
- **Barter limits (lead's call):** one item for one item. The offered item
  must be in a pack, on a belt or in a spare weapon set; worn armour,
  trinkets and the active set are greyed `RestOnly`. Heirlooms, keys,
  party counters, `unsellable` items and Clinging items (Later) are greyed
  `NotBarterable`. A bartered item leaves the run (`ItemBartered`).
- **One command, all or nothing (lead's call):** check coin (`NoCoin`) →
  remove the offered item, if any (`ItemBartered`), freeing its slots →
  place the new item (a Swap if needed) → debit coin (`CoinChanged`).
- **Buys nothing outside barter** and **never stores the party's items**:
  it is not a stash and never a Sell outlet. **(agreed)**

**One service per trader (agreed):** chosen at generation on the loot
stream **(lead's call)**, paid in coin (`servicePrice`), usable **once per
trader** (then greyed `ServiceUsed`) **(lead's call)**:

| Service | Effect |
|---|---|
| Identify | Reveals every party-owned item at once, a curse included (Later), as camp would. **(lead's call: one item alone would be nearly worthless with camp one floor away)** |
| Recharge | Resets one wand's `spent` to 0, as camp would. A named exception to the `camp` refill tag, paid and once per trader (change I-16). |
| Tend | Ends one non-curse lingering condition on one unit, as Remedy does; never a curse **(lead's call: matches conditions 14's Remedy; named Tend so it is never confused with Medicine Treat, conditions 8)** |

**Taking an item (lead's call):** a bought or bartered item is placed by
Take's rules and party-wide placement (6.2), with two limits: **a trader
purchase never shows Equip now and never auto-equips armour or a
trinket** (they go to a pack), so the trader is never a way round the rest
gate; and if the party is full, Swap sends the displaced item to a
leftovers marker in the trader's room (section 9).

**At the stairs (lead's call):** the trader leaves with the floor. Unbought
stock is destroyed (`ItemDestroyed{lost}`), never scrapped, and never
carried to camp.

**Why it does not break the flow (lead's call):**

- it is off the main path and never pops up, so it costs no time unless
  the party chooses to go;
- a party that skips it forgoes 2–3 of the floor's notables, which it
  could only have bought; the guards above keep at least one container
  and one carried notable free on every trader floor, and the simulator
  checks that skipping is a real option (19.3);
- its stock is carved out of loot the floor already holds, so it never adds
  power or rewards detours with extra items;
- it sells nothing that substitutes for supply, so the attrition decision
  stays at camp;
- it is a short, single-screen visit with the summary's own components.

**Possible later flavour (not designed):** a Yielded enemy (enemies 14.6)
or a Cowed group's leader (14.8) as the trader. It would need its own
rules for the trader-as-unit case and is out of scope here.

**Enemies loot channels:** a `trader` channel in enemies 15.3, as a Later
**(change I-14)**.

## 11. Identification

**(agreed: framework decision 4; flow lead's call)** Ships in M2.

1. **Always known:** category, base, family and rarity; weapon die and
   armour category; every `obvious` property (each notable has at least
   one, so nothing is ever an "unknown sword"); anything seen in play,
   including a carrier using it (settled, enemies 15.4).
2. **Consumables, scrolls and wands are always identified.** **(agreed)**
3. **At most 1 hidden property per item,** on Uncommon-or-better gear and
   trinkets only. It is **dormant**: it does not function for the party
   until identified. Comparisons ignore it and show its hidden line with
   the Arcana readout (8.2; change IR-9); prices use
   the assessed value (10.5). **(agreed)** A monster carrier uses its item
   in full (enemies 15.4); dormancy is a party-side state. **(lead's
   call)** **Which property is dormant (lead's call):** an instance's
   dormant properties are exactly its non-`obvious` ones not yet in
   `known`. Fixed properties are always `obvious`, so the dormant
   property, if any, is the rolled one (at most 1 rolled line per item;
   item rolls design 4.6) **(change IR-4, approved 2026-11-22; M2)**.
   Validation keeps that count within `hiddenMax` (19.1 rule 14).
   **Plain Gifts (agreed 2026-11-22, Q3):** the Dream's Due, every offer
   option (the Wager and the Twin Coffer included), the fourth option and
   every Viaticum option are generated with `known` = every line, so a
   choose-one never hides a line; fit premiums, the arms floor and
   Wax-Key and stepped identities are field finds and keep Arcana's job
   (item rolls design 4.14) **(change IR-9; M2)**.
4. **At the summary, a threshold with no roll (settled, stat model 7.2):**
   the party's best full passive Arcana is compared with
   `identifyDifficulty`. The full passive is the maximum over members of
   10 + Intelligence + proficiency if trained in Arcana (doubled with
   mastery) + active effects, the same query form as passive perception
   (settled, stat model 6). If it meets the difficulty, everything is
   revealed, a curse included (Later).
5. **Otherwise the item is revealed** (Plain Gifts items are generated
   known, item 3; change IR-9):
   - automatically when the party's best passive Arcana rises (a level-up,
     a recruit or an effect), logged as "Arcana now reveals …". **(lead's
     call)** After any command or event that changes that maximum, the
     core re-checks every party-owned instance and every instance on an
     open card (summary, shop or trader), in instanceId order, emitting
     `ItemRevealed`;
   - **at camp**, where everything is identified free. **(agreed; a client
     decision, so any change to it needs client approval)**
   - Later, by a wandering trader's Identify service (10.9).

   `known` only grows: a reveal persists if passive Arcana later falls.
6. **Equipping reveals nothing**, so there is no identification by
   equip-scumming. **(agreed)** With curses (Later), Clinging taking hold
   reveals the curse.
7. **Later:** a "malign" hint if the threshold was missed by 5 or less and
   a curse is present; runes and shrines that identify items (optional
   content).

## 12. The generic item model

**(lead's call unless marked)**

### 12.1 Record versus instance

- **Record:** static JSON with a stable ID, validated at load (settled,
  main design 6.5). It holds everything that is the same for every copy.
- **Instance:** run state. It stores only what cannot be derived: the pool
  rule applied to items (settled, ability model 6.2). See 18.1.

### 12.2 Family record

```
family: { id: "family.longsword", category: "weapon", improvised: {...},
          defaultMonsterProfile: {...}, training: "martial", wave: "M1",
          tags: ["blade"], strengthClass: "none" }
```

- Weapon and focus families also hold `training` (`simple | martial |
  exotic`), `wave`, group tags and `strengthClass`, as in weapons design
  20.1. **(change W-6)**
- Family caps per run come from `familyCapPerRun` by the family's
  category *(illustr.)*: weapons and armour 2, trinkets 1, consumables
  unlimited. A cap of 2 lets a committed build find a higher-rarity
  upgrade of its weapon. How shop and trader stock count: 10.4.
  **Scions (change IR-5; M2):** a record may name `scionOf` (at most one
  child per record): same family and base, a superset of the parent's
  fixed lines (the parent's plus one, or a major replacing a minor of the
  same domain). **Trinket Scion exception (agreed family
  caps, approved 2026-11-22):** a trinket family's cap of 1 admits **one**
  Scion at a strictly higher tier, only while the party owns at most 1
  copy of the family; the lower copy then shows ▽ with a Sell hint (item
  rolls design 4.5). **Pending request:** the trinkets owner applies the
  same exception to trinkets design 13.8; until the trinkets owner
  applies it, and if refused, there are no trinket Scions and the
  trinket cap of 1 stands (item rolls design 14.2).
- Trinket families are finer than trinket kinds (`family.visored_helm`,
  never `family.helm`): at least 3 legal families per piece location, as
  a running total in each wave from M2. The Reliquary Shield counts under
  `shield` (armour design 5.4, 10.2) **(change AR-34)**.
- `improvised` lives on the family; validation requires it for every
  weapon family (settled, enemies 15.9).

### 12.3 Item record fields

| Field | Type | Meaning |
|---|---|---|
| `id`, `nameKey`, `flavourKey` | string | Stable ID and localisation keys |
| `family` | `family.*` | Cap key, duplicate rerolls, `improvised`, default monster profile |
| `base` | item ID or null | The mundane base it builds on. Null for mundanes and consumables. |
| `category` | enum | weapon, focus, shield, armour, trinket, consumable, scroll, wand, talisman (**change CN-1**; death and healing design 15, 16; **change DH-35**), key; `supply` is a counter record only, never instantiated (5.3); so is `waxKey` (M2), which is not `key` (5.3; traps and locks design 6.11) **(change TL-24)** |
| `kind` | enum (consumables) | potion, tonic, flask, coating, remedy |
| `tags[]` | strings | Read by effects and offers: `twoHanded`, `versatile`, `thrown`, `fire`, `bulky`… |
| `rarity` | enum | mundane, common, uncommon, rare, veryRare (12.5) |
| `channels[]` | enum | container, carried, boss, camp (the shop and the camp reward card, 10.4), `campReward` (the camp reward card only, never the notables shop) **(change CN-1)**, vendor, kit, boon; `trader` (Later, 10.9) |
| `minFloor`, `maxFloor?` | int | Depth gates |
| `size` | 0, 1 or `bulkySize` | Pack cost. 0 = party counter; `bulkySize` (2) = bulky. Stack size is not on the record: `stackMax` by `kind` in `inventory.json`, 1 for gear. |
| `equip.slot` | enum | mainHand, offHand, either, twoHand, armour, trinket, none. A focus's `hands` maps `off`, `main` and `two` to `off`, `main` and `twoHand` (bulky) (weapons design 11.1; change W-6). |
| `beltable` | bool | Only for consumable, scroll, wand and talisman; Remedy is not beltable **(change CN-1)**. A wand is used from the belt, never wielded. |
| `requirements` | object | `strength {min, mode}`, `training {group, mode}`; `hard` = cannot be equipped, `soft` = a named penalty set by the category design. Weapon and focus items write neither: training lives on the family, and Strength comes from the family's `strengthClass` through `weapons.json` (weapons design 10, 20.1; change W-6). |
| `classTags[]` | strings | Scrolls and wands: which classes use it "in class"; real class ids only **(change CN-1)** |
| `lootLane` | `lane.*` | Exactly one build lane from the closed `loot.lanes` (build lanes, not trainability; the UI says "Suits", never "lane"). Smart-loot weights, `onParty` and the usability floor (agreed 2026-11-22, Q1) read it through `LanesOf(member)` (item rolls design 4.2, 4.3) **(change IR-1; M1 data on floor 1 records, M2)** |
| `requires[]` | build tags | Smart-loot weighting (the offers vocabulary); secondary build tags beside `lootLane` (item rolls design 4.3, change IR-1) |
| `usableBy` | object | `party`, `monster`, `monsterTags[]` |
| `properties` | object | `fixed[]`, `rollable {table, count}`, `hiddenMax` (0 or 1; 0 below Uncommon). Every rollable table declares one `entryValue` that all its entries equal. |
| `grants` | object | `whileEquipped[]` (effects), `whileCarried[]` (rare: party-wide), `abilities[] {ability, cost, pool}` |
| `use` | object | `{ability, targets[] {mode: self \| adjacentAlly \| thrown, cost, tierLimit?}, difficulty: user \| fixed:N}`. One entry per way to use it, each with its own cost. Each entry also declares `side` (self \| ally \| enemy \| any) and `area` (unit \| burst:N), and optionally `range` and `potencyPermille` (`int` or a tuning-key reference); beneficial is derived from `side`. `use` gains `trigger` (talisman only; always automatic; death and healing design 15.2) **(change DH-35)**. `difficulty` is unchanged (consumables design 18.2). **(change CN-1)** |
| `pools[]` | object | `{id, max, refill: never \| rest \| camp}` |
| `charges` | `{min, max}` | Wands: the pre-rolled charge range |
| `identifyDifficulty` | int | Compared with the party's passive Arcana (section 11) |
| `curse?` | object (Later) | `{variant: clinging, cost: prop.*}` |
| `monsterProfile?` | object | `{effects, abilityShape: uses:N \| cooldown, useWhen, minFloor}`; overrides the family default. `useWhen` is a closed vocabulary (consumables design 15) **(change CN-1)** |
| `value` | int, checked at load | Σ fixed property values + rollable `count` × the table's `entryValue` + Σ values of the `use` ability and granted abilities, in offer-value units (12.6). Every roll gives the same value. A trinket button is a property whose effect is the grant; its value is counted once, inside that property (trinkets design 9.2) **(change TK-12)**. Each `ability.item.*` declares its offer value; the load check compares a scroll's value with its tier, a wand's with `wandValue(tier, pool max)`, and every other use-only record's with its rarity budget (consumables design 9.1) **(change CN-6)**. |
| `basePrice?` | int | Required for mundane and shop-only records (supply, Remedy) |
| `flags` | set | signature, heirloom, unsellable, noSteal, unique, srd (with `srdSource`). The `unsellable` treatment also applies to any instance with `boundToUnitId` set (18.1; companions design 7.6) **(change CMP-21)**. `oncePerRun`, always explicit: generated at most once per run in any form (dreamed options, the hidden fourth and the unused Wax-Key identity included), never lifted, required on every signature, Very rare record, heirloom template and `chase` record (the named chase Rares), and never drawn for the fourth option or a dual-identity cache; `chase` marks those named Rares. `unique` keeps its meaning (at most 1 owned) (item rolls design 4.7; agreed 2026-11-22, Q2) **(change IR-6; M2)**. |
| `display` | object | iconId, modelId, glint; `socket` (`head \| back \| none`, trinkets) (change AR-35). Never read by the rules. Theme presentation (Patina on models only, the theme stamp, provenance, a Reverie) comes from the found theme's `theme.loot` display record by display hash, with 0 rules bias (agreed 2026-11-22, Q4); icons stay global and untinted (item rolls design 5.1, 6.2) **(change IR-15; M2, M3)**. |
| `weapon{}`, `armour{}`… | sub-records | Owned by the category designs (section 17). `weapon{}` and `focus{}`: weapons design 20.1 (change W-6). `armour{armourCategory, verb}`, `shield{}` and `trinket{kind, wornOn}`: armour design 16.1; `armourValue` is derived and never stored (change AR-35). |

### 12.4 Properties and the buff test

A **property** is its own record (`prop.*`): an effect bundle from the
existing effects system (settled, main design 6.4).

```
prop: { id, value, bands: {b1, b2, b3}, obvious, test[], tags[] }
```

- **Strength scales by proficiency band** (b1 for levels 0–4, b2 for 5–8,
  b3 for 9–12; settled bands, ability model 9.3). A floor-1 find stays
  relevant on floor 4, and heirlooms scale without power creep from run
  count.
- **The buff test is a validation rule.** Every property declares at least
  one of: `economy` (it changes an action cost); `charge` (a pool use);
  `targeting` (shape or reach); `prevent` (it stops a loss); `rider`;
  `type` (a damage type or resistance); `cost` (a downside).
- **An always-on numeric modifier to attack, AC, saves or damage is
  rejected.** A number may appear only through a verb, for example "+3 AC
  against the first opportunity attack each fight". No item raises an
  ability score past +5 unless it is written as "up to the cap" (settled,
  stat model 5.5).
- At most 3 properties per item, each shown as one computed line; at
  most 1 of them rolled (item rolls design 4.6; change IR-4, read).

### 12.5 Rarity

Rarity uses SRD 5.1 tier names (CC-BY-4.0, attributed). **A tier is a
value budget, not a +N.** **(lead's call, from enemies 15.9's
recommendation)**

| Tier | Value budget | Properties | Depth | Notes |
|---|---|---|---|---|
| Mundane | 0 (priced by `basePrice`) | none | n/a | Shop and kit only (settled) |
| Common | 1 | 1 | floor 1+ | Most consumables |
| Uncommon | 2 | 1–2 | floor 1+ | The usual notable on floors 1–2 |
| Rare | 3 | 2–3 | floor 3+ | May carry a curse (Later) |
| Very rare | 4 | 2–3 | floor 4+ | At most 1 per floor |

Budgets, property counts and depths *(illustr.)* live in
`inventory.json` (`rarity`). Which tiers each floor deals is fixed by its
rarity deck, `loot.rarityDeck` beside `rarity` (base and alternate
variants of exactly equal value; `minFloor` kept as the depth gate; loot
economy design 6.3) **(change LE-2; M1 on floor 1, M2)**; a camp channel entry reads the
Depth column at floor N+1 (10.4, change LE-6). **A record's `value` equals its tier's budget
exactly** (curse cost excluded), so rarity always means the same amount of
power. **(lead's call)** **Later:** a cursed record keeps its deck tier
for value, price, Sell and Scrap while its benefit is authored to the next
tier's budget (13; loot economy design 8.5) **(change LE-22, Later)**.
The `props` ranges apply to gear; a use-only
record meets 19.1 rule 1 with its `use` (consumables design 9.1).
**(change CN-6)**

- **Signature items are independent of rarity.** A boss signature item uses
  the floor's top allowed tier plus one hand-authored property, so its
  value is that tier's budget plus the authored property's value. It is
  never cursed and carries **no cost add**, because bosses are validated by the
  boss guards (enemies 6.5 scope) (change I-9). "Legendary" is not used.
  "The floor's top allowed tier" is the highest tier in the floor's drawn
  rarity-deck variant, capped by the boss's authored forms (one form per
  top tier its floors can need), and **a Very rare signature consumes its
  floor's Very rare slot** (the deck's Very rare card takes the
  tier-stable path, logged). This replaces LE-3's pending exemption
  reading and its refusal fallback (item rolls design 4.13; loot economy
  design 6.3, 18.2) **(change IR-13, agreed 2026-11-22, Q5; M2)**.
  **Pending request:** the items owner confirms the slot rule; if
  refused, LE-3 stands as approved (signatures exempt from the Very rare
  `perFloorMax`; a floor whose field deck holds a Very rare draws its
  signature at Rare plus the authored property; item rolls design 14.2). A
  signature has at most 3 lines: at most 2 base lines plus one authored
  value-1 line echoing the boss's fight or kit (approved M1 records
  grandfathered). Flags `signature` and `oncePerRun`; never rolled, never
  `scionOf`, `identifyDifficulty` 0; cap-exempt when generated and taken,
  then counted toward its family cap; exempt from the run-wide owned-base
  reroll (item rolls design 4.7, 4.13). An unarmed boss **guards** its
  signature in a signature cache in its lair, and its plate reads
  **"Guards: <signature name>"**; in M1 Old Ragged guards Bellwarden's
  Mail **(change IR-13, agreed 2026-11-22, Q5; M1, M2)**. **Floor 5 (agreed 2026-11-22, Q1):** the
  final boss still carries and uses its signature (an unarmed final boss,
  none in M2, guards it in its signature cache during the fight instead;
  change IR-13), but on death it drops
  as a **Memento**: a run-record item card with full stats and a
  premise-neutral caption, 0 VP, shown on the victory screen and the run
  record only (loot economy design 6.11). **(change LE-5; M2)**
- **Quality**, used by duplicate rerolls ("equal or better quality",
  enemies 15.3), is (rarity tier, then value). A higher-tier item of an
  owned base is therefore a legal draw (change I-12).
- Items are build-shaping sidegrades steered by `requires`, which protects
  the "lucky loot must not break +5" principle.

### 12.6 Value

One `value` per record, in offer-value units (about one action's worth;
ability model 10.2):

- **gear:** the sum of its fixed property values, its rollable count times
  the table's `entryValue`, and its granted-ability values; a trinket
  button is a property whose effect is the grant, and its value is
  counted once, inside that property; capacity: +1 pack = value 1, +2 =
  value 2 (trinkets design 9.2, 14.1) **(change TK-12)**;
- **consumables, scrolls, wands and talismans:** the value of the `use`
  ability, from a use-value table owned by the consumables design
  (consumables design 9.1). Each `ability.item.*` declares its value; the
  load check compares a scroll's with its tier, a wand's with
  `wandValue(tier, pool max)`, every other use-only record's with its
  rarity budget; Remedy keeps its `basePrice` **(change CN-6)**.

The same number drives loot weighting, cost add, and (through the assessed
value of 10.5 while a property is dormant) shop price, Sell and Scrap
value and barter credit (Later). Items, offers and enemies share one
currency.

### 12.7 k: value to encounter cost

**k = 1 encounter point per value unit, provisional.** The simulator owns
the final number.

The enemies 6.5 validator passes a unit when `0.8 × price ≤ power ≤ 1.2 ×
price`. Because that is a band, both k = 1 and k = 2 pass most carriers
even when the item adds no threat, so k is chosen to hit the band
**centre**. All figures approximate:

- One encounter point adds `FloorDiv(1 × 1000, levelCost(0) = 10)` = 100
  permille to price.
- A value-1 property is about one action over a roughly 4-round fight,
  about +25% offence for a standard: threat about 1.25×, so by the square
  law **power about 1118**. k = 1 prices it at 1100 (about 2% from the
  centre); k = 2 at 1200, which would need threat about +44% (over-priced,
  and its room twice as thin).
- A value-2 item: threat about +50%, power about 1225, against a k = 1
  price of 1200.
- An elite (power about 2000): the same value-1 item is about +12.5%
  offence on a larger kit, power about 2120 against a k = 1 price of 2100.

Two guards back k = 1:

- validation rule 7 (19.1) keeps strong items from hiding in the band's
  headroom (k = 1 alone would let a value-2 item add up to about +107%
  threat);
- the simulator reports carrier power ÷ price and carrier-room difficulty
  with and without the item (19.3).

**Room guard** (change I-9): if the cost add is more than
`roomGuardPermille` *(illustr. 25%)* of the room's budget, the item goes
to a container in that room.

### 12.8 Stacks, charges and pools

- **A stack is one instance.** Its pool is `{max: stackMax, refill:
  never}` and stores `spent`; count = stackMax − spent. This is the settled
  pool rule, so no exception is needed.
- **Using** a dose increments `spent`. At count 0 the instance is
  destroyed (`ItemDestroyed{usedUp}`).
- **Merging** fills matching stacks in the destination place, lowest slot
  index first, ties by instanceId, up to `stackMax`; any remainder takes a
  free slot or stays at its source (6.2) (`ItemMerged`).
- **Hand over or Administer** moves one dose, splitting off a new instance
  (`ItemSplit`).
- Stacks merge only when the record, rolled properties, known state and
  `boundToUnitId` are identical, so two companions' kit potions stay two
  stacks (companions design 7.6) **(change CMP-21)**. Gear never stacks.
- **Wands:** charges are pre-rolled at generation (enemies 10.5 step 3),
  stored as `spent = max − rolled`, and refill at camp (settled `camp`
  tag; `ItemRecharged`). Vendor and shop wands start full (10.3). The one
  other refill is the trader's paid Recharge (Later, change I-16). On a
  carrier, `uses:N` equals the remaining charges, and the party
  gets the wand with whatever is left (settled, enemies 15.4).
- An item's `camp` spends count toward the one-camp-spend-per-turn rule
  (settled, ability model 6.4).
- Equipping, unequipping or handing over an item never touches `spent`.
- **Later (change W-6):** a weapon is destroyed when a `never` pool
  reaches 0 (Shatter, weapons design 18.3).

### 12.9 Requirements

- Strength and training gate **wearing, never carrying.** Each category
  design picks `hard` or `soft` per requirement.
- Proficiency is normally `soft`: an untrained unit loses proficiency, plus
  any penalty the category design sets.
- Comparisons show the fit as one icon and one reason.

### 12.10 JSON examples *(illustr.)*

```json
{ "id": "item.ember_longsword", "nameKey": "item.ember_longsword.name",
  "family": "family.longsword", "base": "item.longsword", "category": "weapon",
  "tags": ["versatile", "metal", "fire"], "rarity": "uncommon",
  "channels": ["container", "carried"], "minFloor": 1, "size": 1,
  "equip": { "slot": "either" }, "beltable": false,
  "requires": ["build.melee", "build.fire"],
  "usableBy": { "party": true, "monster": true, "monsterTags": ["armed"] },
  "properties": { "fixed": ["prop.ember_edge"],
                  "rollable": { "table": "props.weapon_minor", "count": 1 }, "hiddenMax": 1 },
  "identifyDifficulty": 13, "value": 2, "flags": [],
  "weapon": { "dice": ["1d8"], "versatileDice": ["1d10"], "damageType": "slashing",
              "altDamageType": null, "riderFlag": false,
              "hands": "versatile", "light": false, "finesse": false,
              "reach": 1, "minReach": 1, "range": null,
              "thrown": null, "loading": false, "pointBlank": false,
              "verb": "verb.parry" } }
```
```json
{ "id": "item.tonic_minor", "family": "family.tonic", "category": "consumable", "kind": "tonic",
  "rarity": "common", "channels": ["container", "carried", "vendor"], "size": 1,
  "equip": { "slot": "none" }, "beltable": true,
  "pools": [{ "id": "stack", "max": "stackMax", "refill": "never" }],
  "use": { "ability": "abl.tonic_cure", "difficulty": "user",
           "targets": [ { "mode": "self", "cost": "bonusAction", "tierLimit": "minor" },
                        { "mode": "self", "cost": "action", "tierLimit": "major" },
                        { "mode": "adjacentAlly", "cost": "action", "tierLimit": "minor" },
                        { "mode": "thrown", "cost": "action", "tierLimit": "minor" } ] },
  "identifyDifficulty": 0, "value": 1,
  "monsterProfile": { "effects": [], "abilityShape": "uses:1", "useWhen": "holdsMajorCondition", "minFloor": 1 } }
```
```json
{ "id": "prop.ember_edge", "value": 1, "obvious": true, "test": ["type", "rider"],
  "bands": { "b1": { "effect": "eff.ember_edge_1" }, "b2": { "effect": "eff.ember_edge_2" },
             "b3": { "effect": "eff.ember_edge_3" } } }
```

The ember longsword is an **Ember Edge** card (M2): Edge (fire) with the
element part rider-flagged (weapons design 18.2). Training (martial) and
the Strength threshold come from `family.longsword`, so the record writes
no `requirements` (weapons design 20.1). **(change W-6)**

`props.weapon_minor` declares `entryValue: 1`, so the ember longsword's
value is 1 + 1 × 1 = 2, the Uncommon budget; its one possible dormant
property is the rolled one.

The `srd` flag is set only on records whose name or mechanic comes from
SRD 5.1/5.2, and then `srdSource` must cite the entry. The ember longsword
is original, so it carries no `srd` flag.

## 13. Curses (Later)

**(agreed: curses Later; rules lead's call)**

- **A curse is always a cost attached to a real benefit.** The cost is a
  `whileEquipped` property (an Ill-Omened, Leaden or Ill-Starred effect,
  conditions 18.10) active while the item is worn. **It is not a lingering
  condition**, so the cap of 2 (conditions 14) cannot let it apply for
  free.
- **Clinging** is the only condition involved. It is applied **on equip,
  whatever the identification state**, and is **exempt from the cap of
  2**, as Scarred is (change I-2). It ends at camp, at a shrine, or with
  Arcana at a rune (failure: Weary 1, settled). Re-equipping applies it
  again.
- **Clinging blocks every move out of the slot:** unequip, the weapon-set
  swap, hand over, take-from-Downed, Equip-now displacement, Sell, barter
  and Disarmed (the weapon stays in hand). Each shows `Clinging`.
- **Placement:** Rare and above, never on floor 1, at most 1 per floor,
  never on consumables, signature items or heirlooms. **(change IR-20,
  Later; draws reserved from M2)** Curses are authored **cursed twins**
  (`cursedTwinOf`, same family and base), never rolled. The `curseGate`
  stream (2 draws per floor, always consumed from M2) decides the floor's
  curse and picks the card, so at most 1 per floor is structural.
  Eligible: Rare+ gear field cards from floor 2; also never on the
  Dream's Due, fit premiums, the arms floor, any offer option or the
  fourth, the Viaticum, camp entries or rings (item rolls design 4.15).
- `value` counts the benefit only. **A cursed carried item:** a monster
  carrier applies only the benefit, so its cost add uses the benefit-only
  value (the benefit's T+1 value; item rolls design 4.15, change IR-20). Hover shows "Carries: X" with **no curse mark**: finding it is the
  party's Arcana job.
- **Curses at a discount (change LE-22, Later):** a cursed card keeps
  its deck tier T for VP, price, Sell and Scrap; its benefit is authored
  to T+1's budget and the curse pays the difference. Never on a Dream's
  Due (loot economy design 8.5).
- Once known, a curse shows a cursed badge everywhere.

## 14. Heirlooms (M3)

**(lead's call)**

- An heirloom is a **profile-stored template, never a persisting
  instance.** The settled single starting boon (main design 4.7) creates a
  fresh instance each run.
- Fixed at Uncommon, always identified, banded by proficiency, and never
  grows with run count ("no permanent stat boosts", settled).
- Flags: `heirloom`, `unsellable`, `noSteal`. Never cursed. Outside the
  floor loot budget. Its value is for display only. It cannot be bartered.
- It **does not consume its family's cap**, but it **does block duplicate
  bases** (it counts as owned), and it counts as party-owned for the
  Dream's Due (loot economy design 6.12; change LE-19, read; M3). It sits
  outside loot pools and Scion-line memory, is `oncePerRun`, and counts
  as owned for item verdicts (item rolls design 4.15, read).
- Left in leftovers, it is lost at the stairs, never scrapped. The profile
  keeps the template.

## 15. Companions, recruits and run end

**(lead's call unless marked)**

- **A Downed companion keeps everything** (settled, main design 4.2:
  companions are downed, not killed). An adjacent ally may take its belt or
  weapon-set items (7.1). It stays Downed in the party with its gear until
  Medicine, a won fight, a completed short rest or camp (death and healing
  design 11.2). **(change DH-36, agreed 2026-11-17, Q2, Q4)**
- **Conditional:** if the death and healing design ever allows companion
  death (enemies 25), the dead unit's equipment, belt and pack join **that
  fight's summary** as cards outside the 3-item cap. Its capacity ends, and
  nothing is ejected from anyone else. The rule stays dormant: companions
  never die at launch (death and healing design 6.4, 11). **(change
  DH-36)**
- **A recruit** arrives with its kit record (mundane bases, full pools,
  settled ability model 11.2), and the party view opens. Every instance
  its kit issues is `kitBound` (`boundToUnitId` set to the recruit,
  18.1): it sells for 0 (10.6), anyone may equip it while its unit is in
  the party, and it is deleted from wherever it is in the party when its
  unit is dismissed or its hire undone (companions design 7.6) **(change
  CMP-21)**.
- **Run end:** every item, all coin and all supply are gone. Only heirloom
  *templates* persist (section 14).

## 16. Answers to enemies 15.9

| 15.9 item | Generic-layer answer | Category design that owns the detail |
|---|---|---|
| Weapon records, incl. multi-die (one-die conversion) | The weapon sub-record carries `dice[]` (a list, so "one die of the weapon's largest size", capped by the row die since enemies change W-5, is computable), `damageType`, `riderFlag` and tags | Weapons |
| Armour records in the table AC bands with monster Dexterity | The armour sub-record carries `armourCategory` and `verb`; value, Dexterity cap and Strength threshold are derived from `armour.json`, never written on the record (armour design 16.1; change AR-36), plus a content test against the enemies 6.2 bands | Armour and shields |
| Strength thresholds | Generic `requirements.strength {min, mode: hard\|soft}`, gating **wearing, never carrying** | Weapons; armour |
| `improvised` attack per weapon family | On the **family record**; required for every weapon family | Weapons |
| `monsterProfile`, `usableBy`, `family`, `requires`, `identifyDifficulty` | A **family default profile**, overridable per record; required exactly when `usableBy.monster`; applies benefits only | Each category |
| k | **k = 1, provisional** (12.7) | Generic; the simulator owns the number |
| Stacks of tonics and potions | One stack per belt or pack slot, per-kind `stackMax` *(illustr. 3)*; gear never stacks | Consumables |
| How supply uses slots | **None.** A party counter, settled cap 3 (5.3) | Generic (done) |
| Coin, salvage, prices, stash, selling | Coin: yes. 15.7 salvage sized against found coin (I-11). Prices from value and the hero's Leadership. Sell 25% (change LE-9; M2) and Scrap 10% *(illustr.)*. No stash. (Section 10) | Generic; consumables (pegs) |
| Rarity tiers ("avoid flat +N") | SRD tier names as value budgets, never a +N ladder (12.5) | Generic (done) |
| Identification flow | Section 11 | Generic (done) |
| Cursed carried items (Clinging) | **Yes,** a carrier may hold one, applying only the benefit; **unmarked** on hover (section 13) | Curses (Later) |
| Oils and Coated | Coated stays a **unit condition**; a `coating` consumable applies it to the wielder's current weapon | Consumables (Later part) |
| Disarmed in full | Party side: re-wield from the square or swap sets; returns to its owner at combat end (I-4; target slot in 7.1). Monster side: seizure through `claimed`. A Clinging weapon is never dropped. | Weapons (Later) |
| Pitted Armour | Stays a **unit condition** reading the worn armour. Swapping armour needs a rest and the condition follows the unit, so a swap cannot escape it. | Armour (Later) |
| Loot summary layout | Section 8 | Generic (done) |
| Theme materials for unarmed enemies | Deferred: a `channels: carried` record with `usableBy.monsterTags` | Later |
| Binding: mundane bases are not loot; ≥1 property; family caps and rerolls | Validation rules 1–3 and 11 (19.1). A consumable's `use` counts as its property. | Generic (done) |

## 17. Category roadmap

**(lead's call)** Each row is a later design that plugs into this layer.

| # | Category design | Wave | Must decide |
|---|---|---|---|
| 1 | **Weapons (with foci)** | M1 | **Designed:** `2026-11-17-weapons-design.md` (weapons design), approved 2026-11-17, which adopts the no-ammunition-tracking recommendation (weapons design 9.4). Disarmed and Coated stay Later. **(change W-15)** |
| 2 | **Armour and shields** | M1 | **Designed:** `2026-11-17-armour-design.md` (armour design), approved 2026-11-17, which takes the weapons design 17.9 inputs **(change W-13)**. Pitted Armour and the tower shield stay Later. **(change AR-37)** |
| 3 | **Starting kits** (small; may fold into 1 and 2) | M1 | `kit` records per class and per companion: weapon sets, armour, belt contents, starting coin. Answers ability model 20's "a minimal starting-equipment set for M1". Armour kits as in armour design 13.6 **(change AR-37)**. Every hero class kit holds 2 healing potions on the belt; every recruit kit 1; the hero's camp top-up of 1 (death and healing design 12.4, 13.2) **(change DH-37, agreed 2026-11-17, Q3)**. |
| 4 | **Consumables** (potions, tonics, flasks, coatings, Remedy) | M1 potions; M2 rest | **Designed:** `2026-11-17-consumables-design.md` (consumables design), approved 2026-11-17 **(change CN-8)**: effects, `stackMax`, the use-value table, prices and pegs against supply, thrown flasks, monster `useWhen`, banding. Potions can be thrown at a willing ally: action, range 5, `thrownPotionPermille`, never lifts Downed (consumables design 11) **(change CN-4)**. Coatings and Coated stay Later. |
| 5 | **Scrolls and wands** | M2 | **Designed:** in the consumables design (section 13), approved 2026-11-17 **(change CN-8)** |
| 6 | **Trinkets** | M2 | **Designed:** `2026-11-17-trinkets-design.md` (trinkets design), approved 2026-11-17. Piece tables by location, jewellery tables by kind (trinkets design 14.1), capacity table **(change TK-9)**, capacity trinkets (at most +2), trinket families and caps. From the armour design **(change AR-37)**: piece kinds; location domains (armour design 5.6); the strict no-AC rule; the one-table rule; the reaction cap; pieces at Uncommon at most; at least 3 legal families per piece location per wave; `requires` tags for location hunger (armour design 5.7); the piece power target (armour design 17.3) |
| 7 | **Heirlooms** | M3 | The template list, at Uncommon, banded |
| 8 | **Curses** | Later | The variants used as item costs, the Clinging flow, cursed carriers, the "malign" hint |
| 9 | **Wandering trader** (small) | Later | Service prices, presence chance, trader art and lines, within 10.9 |

`tool` is cut from the generic layer (section 2). Reinstate it only if a
category design shows a need. The traps and locks design keeps it cut:
no lockpicks; if the client ever reopens BQ6, the fallback is a size-0
party counter spent only on a Botch (traps and locks design 6.11, 19.2).
**(change TL-26, kept)**

## 18. Data and the core

**(lead's call)**

### 18.1 Instance

```
ItemInstance {
  instanceId         // run-state integer counter, incremented by every creation event; never an RNG draw
  recordId
  rolledProperties[] // fixed at floor generation (enemies 15.3 step 5); never rerolled
  spent { poolId: n }
  known [propId]     // revealed dormant properties; only grows
  everHeld           // set by the first ItemTaken this run; gates Equip now (8.3)
  curseKnown         // Later
  location           // exactly one of:
                     //   weaponSet(unit, active|spare, main|off) | armour(unit) | trinket(unit, i) |
                     //   belt(unit, i) | pack(unit, i) | party(keys) |
                     //   container(containerId) | carried(monsterId) | ground(square, fightId) |
                     //   claimed(unit, fightId) | summary(summaryId) | leftovers(markerId) |
                     //   pile(markerId) | shop(campId) | trader(markerId) |
                     //   windfall(unitId)  // on a Windfall Truant (change LP-2; M3)
  origin { channel, floor }
  substituted        // enemies 15.3 step 2
  boundToUnitId      // null unless issued by a recruit kit; kitBound is derived from it (companions design 7.6; change CMP-21)
}
```

`trader(markerId)` is Later. A two-hander sits at `main`, and `off` is
derived as blocked. A slot index `i` in the pack is an ordering key only:
occupancy is the sum of sizes. An instance that leaves the run (used up,
sold, scrapped, bartered, lost) is **deleted**, and its `ItemDestroyed`,
`ItemSold`, `ItemScrapped` or `ItemBartered` event is the record. Supply and
coin are integers in party state, never instances (5.3). **(lead's call)**
So is `party.waxKeys` (M2; 5.3) **(change TL-24)**. The vendor's
`vendor.waxKeyStock` (0 or 1) is set at camp entry from the gap card's
party snapshot and draws nothing (10.3; traps and locks design 11.1)
**(change TL-25)**. An instance at `container(containerId)` is never
destroyed by a lock or trap result, and a container's own additions
(`lockId?`, `trapId?`, `heldWaxKey`) are segment 7's (traps and locks
design 6.13, 11.1). **(change TL-27, kept)** `windfall(unitId)` is the
card on a Windfall Truant (`carried(monsterId)` is for monsters). **Loot
is never destroyed, with one exception:** a Windfall's card and coin
leave the floor when its shown count ends (`ItemDestroyed{gotAway}`);
kill method, damage, terrain, traps, locks and force still never destroy
loot (loot sources design 4.7.7) **(change LP-2; M3)**.

**Location transitions (lead's call).** Every legal move; 19.2 asserts that
no other occurs.

| From | To | Command or trigger | When | Event |
|---|---|---|---|---|
| generation | `container`, `carried`, `shop`, `trader`, `windfall` **(change LP-2; M3)** | floor loot draw | generation | `ItemCreated` |
| vendor (unlimited goods) | 6.2 placement | Buy | camp | `ItemCreated`, `ItemBought` |
| kit issue | 6.2 placement | on hire (companions design 7.6) **(change CMP-21)** | camp | `ItemCreated{kit}` |
| `carried` | `ground` | the monster dies or drops it (enemies 15.6) | combat | `ItemDropped` |
| `container`, `leftovers`, `pile`, `ground`, `claimed` | `summary` | summary opens (8.1) | out of combat | `ItemMoved` |
| `carried` | thief's `pack`, else `leftovers` | Steal (8.1) | out of combat | `ItemTaken` or `ItemLeft` |
| `windfall` | `ground` | nab (loot sources design 4.7.5) **(change LP-2; M3)** | combat | `ItemDropped` |
| `windfall` | thief's `pack`, else `leftovers` | Steal on a Dormant Truant, card only (8.1; loot sources design 4.7.8) **(change LP-2; change LP-6; M3)** | out of combat | `ItemTaken` or `ItemLeft` |
| `windfall` | deleted | the Truant gets away (`rathole`, `faded` or `partyFled`; loot sources design 4.7.7) **(change LP-2; M3)** | combat | `ItemDestroyed{gotAway}` |
| `summary` | equip, set, belt or pack (6.2) | Take, Take all, Equip now | out of combat | `ItemTaken`, `ItemEquipped` |
| `summary` | `leftovers` | Leave, Swap displacement, Done | out of combat | `ItemLeft` |
| pack, belt or spare set | `leftovers` | Swap or Equip-now displacement (dungeon only) | out of combat | `ItemLeft` |
| `ground` | `belt`, spare `weaponSet`, `claimed` | pickup or seizure (7.1) | combat | `ItemMoved`, `ItemClaimed` |
| `claimed` | `pile` | the party flees (7.1) | combat end | `ItemLeft` |
| `weaponSet` | `ground` | Disarmed (Later) | combat | `ItemDropped` |
| `ground` | owner (7.1 Disarmed row) | combat end | combat end | `ItemReturned` |
| `pack` | `belt`, empty spare set | Retrieve | combat (action) or out of combat | `ItemMoved` |
| `belt`, `weaponSet` | adjacent ally's `belt` or empty spare set | Hand over, take-from-Downed | combat (free interaction) | `ItemMoved` or `ItemSplit` |
| any party place | any party place | party view | out of combat (armour and trinkets only at a rest, camp or Equip now) | `ItemMoved`, `ItemEquipped` |
| `shop`, `trader` | 6.2 placement (armour and trinkets to a pack at a trader) | Buy, barter | camp; trader (Later) | `ItemBought`, `CoinChanged` |
| party place | deleted | use to 0, Sell, barter offer | any; camp; trader | `ItemDestroyed{usedUp}`, `ItemSold`, `ItemBartered` |
| party place (`boundToUnitId` set) | deleted | on dismissal or same-visit undo of its unit (companions design 7.6) **(change CMP-21)** | camp | `ItemDestroyed{dismissed}` |
| `leftovers`, `pile` | deleted | stairs | descent | `ItemScrapped` (heirloom: `ItemDestroyed{lost}`) |
| `container`, `carried`, `ground`, `trader`, `party(keys)`, `windfall` **(change LP-2; M3)** | deleted | stairs | descent | `ItemDestroyed{lost}` |
| `shop` | deleted | leaving camp | camp end | `ItemDestroyed{lost}` |

### 18.2 Derived, never stored

Count, assessed value, price, Sell and Scrap value, barter credit, gain,
comparisons, fit lines, the pure-value swap test, the suggested
recipient, slot occupancy, capacity and over-capacity. There is **no `tempMods` field**: Pitted Armour and Coated
are unit conditions.

### 18.3 Events

- New item events: `ItemCreated`, `ItemTaken`, `ItemMoved`,
  `ItemEquipped`, `ItemUsed`, `ItemSplit`, `ItemMerged`, `ItemClaimed`,
  `ItemRevealed`, `ItemLeft`, `ItemSold`, `ItemBought`, `ItemScrapped`,
  `ItemDestroyed{reason: usedUp | lost | dreamedAway | gotAway}` (`dreamedAway`: an
  unchosen option of an offer set dissolving when the choice resolves;
  loot economy design 6.3) **(change LE-4, M2; change LE-21, the Twin
  Coffer, M3)** (`gotAway`: a Windfall's card leaving with its Truant;
  not a dreamed option, never in Scion memory or "What the dream kept";
  loot sources design 4.7.7) **(change LP-2; M3)**, `ItemRecharged` (camp, and Later
  the trader), `ItemReturned` (Disarmed, Later); Later `ItemBartered`.
- Consumable events (consumables design 18.4): `ItemUsed` gains `mode:
  self | adjacentAlly | thrown | inClass | outOfClass`; new
  `ItemReadFailed{instance, unit}` and `ItemTriggered{instance, unit,
  trigger}` (the talisman; death and healing design 15.5)
  **(change CN-9; change DH-35)**.
- New trinket event: `TrinketPropertyFired{property, instanceId, unit,
  result}` (trinkets design 17.3) **(change TK-15)**.
- New state events **(lead's call)**: `CoinChanged{delta, source: found |
  salvage | sell | scrap | buy | barter | service}`, `SupplyChanged{delta,
  source}`, `MarkerCreated{kind: leftovers | pile | trader, room}`,
  `CampChoiceMade{choice: shop | reward}`; Later `TraderDiscovered` and
  `TraderServiceUsed{service, target}`.
- Each event carries the instanceIds, units and locations it changes, so a
  replay rebuilds the state exactly (19.2).
- Existing, unchanged: `ItemDropped` (enemy drops, and party Disarmed
  through the existing `disarmed` reason) and `LootSummary` (enemies 19.3).

### 18.4 Determinism

- Inventory operations never draw from any random stream.
- Placement order is fixed: **Take all** in instanceId order; the
  **suggestion** by usable, then gain, then free room, then unit order
  (6.3); **within a unit** the next ordering index; **stacks** fill the
  lowest slot index first, ties by instanceId (6.2).
- Loot, wand charges, camp stock and (Later) trader stock and service are
  fixed at floor generation on the loot stream, and trader presence and
  stock count on the dungeon stream (both always drawn, 10.9), so kill order, save and resume, and inventory choices can
  never change what drops or what is for sale.
- The loot plan is drawn on the keyed `lootPlan` child of the loot
  stream (enemies 10.5 step 3a); coin lumps, salvage shares and the Heavy
  Purse are bound in the draw-free LootBind pass; floor N+1's generation
  and the camp-exit save are one atomic write (10.4); a `LootLedger` per
  floor is saved for reports and V13 (loot economy design 6.10, 10.1).
  **(change LE-14; M1)**

## 19. Validation, testing and simulator

**(lead's call)**

### 19.1 Content validation

The build fails on any violation:

1. Every record in a loot channel (container, carried, boss, camp,
   trader) has count(fixed) + rollable `count` ≥ 1, or a `use`; a
   consumable's `use` counts as its property. Each rollable table it names
   is non-empty for every floor band in [`minFloor`, `maxFloor`]. A
   `mundane` record has only the vendor or kit channels.
2. Every rollable table declares one `entryValue`, and all its entries
   equal it. `value` = Σ fixed property values + rollable `count` ×
   `entryValue` + use and granted-ability values, and it **equals** the
   tier budget (curse cost excluded; a signature item adds its authored
   property, 12.5). A trinket button is a property whose effect is the
   grant; its value is counted once, inside that property **(change
   TK-12)**. Every vendor- or camp-sellable record has a price above
   0.
3. Every property passes the buff test. No bare always-on numeric
   modifiers; nothing raises an ability past +5.
4. Pool refills: consumables `never`, talismans `never` (a derived stack
   pool and no other pool) **(change CN-7; change DH-35)**, wands `camp` (the trader's Recharge
   is the one named exception, Later, change I-16), `rest` only with an
   authoring reason. Trinkets: `rest` and `fight` by default; `camp` only
   at Rare; never `never`. Trinket `fight` uses use the armour 16.3
   `fightUses` state (trinkets design 9.3) **(change TK-13)**.
5. `size` ∈ {0, 1, `bulkySize`}. Two-handers, armour and shields are
   bulky. Size 0 only for party-counter categories (`supply`, `key`,
   `waxKey` **(change TL-24)**).
   Records carry no `stackMax` (5.1).
6. `beltable` ⇒ category ∈ {consumable, scroll, wand, talisman} **(change
   CN-7)**. No record is both
   beltable and hand-equippable.
7. `monsterProfile` (own or family default) exists exactly when
   `usableBy.monster`. On the reference standard carrier for `minFloor`,
   **carrier power ÷ price lies within 90–110%** (the content test that
   backs k), with power computed by the enemies 6.5 integer formula. The
   reference carriers are test-only records in the items test content,
   `ref.carrier.standard.f{N}`, built from the enemies 6.2 standard row
   at floor N's standard room level. **(lead's call)**
8. Buffs on consumable, scroll, wand and talisman records last 10 turns
   or fewer; no concentration on non-spell consumables **(change CN-7)**.
   Floor-1 carried consumables are self-targeting (settled, enemies 15.3).
9. Curses only on Rare or higher, never on signature, heirloom,
   consumable, scroll, wand or talisman records **(change CN-7)**. Later:
   a cursed record is an authored twin (`cursedTwinOf`) of an uncursed
   record of the same family and base; never on rings (item rolls design
   4.15) **(change IR-20, Later)**.
10. Heirlooms: `unsellable`, `noSteal`, `identifyDifficulty` 0, uncursed.
11. Every weapon family has an `improvised` attack. Family caps resolve.
12. Records flagged `srd` cite `srdSource`. Infinite-storage records are
    rejected; capacity grants are at most +2.
13. Later: trader stock, checked after every reroll and the slot-return
    rule (10.9), never includes supply or healing potions, holds at most
    `tonicMax` tonic doses, leaves at least `containersFloor` container and
    `carriedFloor` enemy-carried notables, and its notables are counted in
    the floor loot budget's per-channel counts (enemies 20.1's count
    check).
14. `hiddenMax` is 0 below Uncommon, and count(non-`obvious` fixed
    properties) + (the rollable table contains a non-`obvious` entry ?
    rollable `count` : 0) ≤ `hiddenMax`. Every fixed property is
    `obvious`, so the rule holds per instance: the dormant property, if
    any, is the rolled one (item rolls design 4.6) **(change IR-4; M2)**.
15. Trinket domain map: each (effect kind, target scope, subject token)
    triple has one owning table, read from `trinketDomains` and the closed
    `trinketSubjects` vocabulary (5.1), token by token; shared triples
    only through `sharedTrinketAllowlist`.
16. Trinket denylist on `effectKinds[]`; every effect kind is an allowed
    kind (trinkets design 6.1, 6.2).
17. Trinket combat-first: the `obvious` property binds a combat hook;
    capacity exempt (also from armour 17.1 rule 14's zero-trigger-rate
    failure).
18. Class copy registry, bidirectional (trinkets design 11): a collision
    fails both records unless the pair is in `copyRegistryAllowlist`.
19. Trinket buttons: only on charms in M2, at most 1 per item; the button
    is a property whose effect is the grant, and its `button.abilityId`
    resolves to an ability-model record.
20. Trinket pools: no `never`; `camp` only on Rare; majors 1 / 1 / 1.
21. Rare trinkets: amulets only, `minFloor` 3+, never `signature`; pieces
    at most Uncommon. The per-floor count is a 19.2 invariant.
22. Trinket ally effects: `targets` is `self`, `adjacentAlly`, or
    `reach2Ally` only on Hand over and Administer reach steps; one ally
    per trigger.
23. One cost step and one reach step per action; one channel per use; a
    declared reach option is not a channel.
24. Trinket outputs: minor or token conditions only; never an AC-writing
    condition; zero added damage; every `channel` record carries
    `hostileOnly` and `riderWouldBeAttempted`.
25. Trinket carriers: `negates[]`, a role-answer pass, `carrier{}` limits,
    the enemies 9.1 reaction and bonus-action caps, Wardstone and
    Ward-Knot never with the Warded or Unbreakable affix, and the 6.4
    guards at generation (enemies 15.3 step 4).
26. Two classes: every trinket record declares `usefulFor[]` with at
    least 2 launch classes or `anyCompanion`.
27. Element families: at most 2 per wave per lane (finger, neck, back).
28. Rolled lines: a rollable trinket table never offers the item's own
    obvious line, a line sharing an `answers[]` key with it, a second
    channel on a channel ring, or a second button; an obvious line with
    no legal roll fails.

Rules 15–28 are summaries; the full text is trinkets design 18.1.
**(change TK-14)** Rule 28's limits apply to every category's rollable
tables, which are curated per family (3–5 eligible entries) and give at
most 1 rolled line per item; the item rolls design's V14 `ItemRolls`
checks also apply (item rolls design 4.6, 9.2; the trinkets, weapons
and armour owners confirm) **(change IR-4; M2)**.

Armour design 17.1 rules 10, 11, 13, 14 and 17 also apply (bulky armour
and shields and no AC on trinkets; one table, one property; name
collisions; hooks and dead properties; piece rules). **(change AR-38)**

Consumables design 19.1 rules 1–16 also apply (consumables, scrolls,
wands, coatings (Later), value and pegs, `useWhen` and tuning
references), its talisman rule included; so do the death and healing
design's 21.2 codes. **(change CN-7; change DH-35)**

### 19.2 Run-time invariants and tests

Invariants, asserted as property tests:

- exactly one location per instance;
- every location change matches a row of the transition table (18.1),
  the `windfall` rows included **(change LP-2; M3)**;
- 0 ≤ spent ≤ max;
- the belt holds only beltable items, and never more than `beltSlotsMax`;
- over capacity only ever arises from a capacity loss;
- no in-combat command moves an item to or from a pack except Retrieve;
- no armour or trinket move in combat, and none out of combat except at a
  short rest, at camp, or an item's first Take (Equip now, or Take's
  auto-equip, with `everHeld` unset);
- `known` only grows; supply stays within 0 and `supplyCap`;
- at most `trinketRarePerFloorMax` Rare trinkets per floor across all
  channels; once the floor's count is reached, Rare trinket draws are
  excluded from the weight table for the floor's remaining draws, in
  channel order containers, carried, camp, boss (asserted by the
  generator and a test) **(change TK-27)**; loot-plan identities, offer
  fallback trinkets included, count **first**, then that channel order
  (item rolls design 4.17) **(change IR-14; M2; pending request: the
  trinkets owner confirms the order for TK-27)**;
- nothing at camp goes to leftovers;
- no inventory command draws a random number (stream positions unchanged);
- coin never rises at a trader (Later).

Tests:

- placement order, pure-value swaps, and Take all stopping at the first
  real swap; the suggestion tie-breaks;
- stack merge and split, and a half-spent wand handed over keeps `spent`;
- capacity reads Strength without conditions; losing a capacity trinket
  ejects nothing;
- identification: threshold at equality reveals; a rise in passive Arcana
  reveals; camp reveals; equipping does not;
- leftovers merge only within a room; Scrap at the stairs, heirlooms lost;
- prices: Leadership reads the hero only and never surcharges; Sell reads
  the undiscounted base; the trader price is strictly above the camp price
  for every item (Later); an unidentified item's price does not depend on
  its dormant property;
- flee sends claimed items to the pile; a Disarmed weapon returns to the
  right slot (Later); a trader barter is all or nothing (Later);
- replay tests across summaries, camp purchases and sales, rebuilt from
  the 18.3 events; the same seed gives the same camp stock (and, Later,
  the same trader, whatever the presence outcome does to later dungeon
  draws).

### 19.3 Simulator and playtest reports

| Check | Target *(provisional)* |
|---|---|
| Share of floors on which the party fills before the stairs (sell-maximising policy: carry everything to camp) | Most floors from floor 2; never floor 1 |
| Summaries needing a real swap (pure-value swaps excluded, 8.3) | ≤ 15% |
| Size-binding rate (no-room events caused by size 2 rather than count) | Bulky stays only if > 10% |
| Take all agreeing with the scorer's best placement | ≥ 90% (a release requirement) |
| Retrieve actions per fight | Low but non-zero |
| Belt items used per fight | Rising toward elite and boss fights; zero means the belt is decoration |
| Strength's share of party capacity | Visibly above the base; the hauler is not a pure mule |
| Carrier power ÷ price (with the item) | 90–110% |
| Notables after floor 2 that someone would equip | ≥ 70% |
| Coin-funded HP against supply-funded HP per floor | Coin below supply |
| Consumables unused at each camp | Watch for hoarding |
| How often Arcana identification changes a choice | Reported (guards against decoration skills) |
| Leftovers value scrapped per floor | Watch for regret |
| Summary time and inputs | Median ≤ 10 s, ≤ 2 inputs |
| Later: trader visits, purchases and barters per floor; detour time | Visit rate reported, not targeted; a visit under 30 s |
| Later: free notables on a trader floor against a no-trader floor | Never fewer than 1 container and 1 carried notable; the gap is only the carved count |
| Later: wand charges used per floor, with and without a Recharge trader | Recharge adds at most one wand's charges per trader floor |

Trinket reports: see trinkets design 18.3 **(change TK-17)**.
Loot economy reports (`kPC`, `SellShareOfCoin`, `CampCoverage` and the
rest): see loot economy design 10.3; this table's reports are kept. On
floors 4–5 the "≥ 70%" notables row will likely fail; `LateFindRelevance`
≥ 50% is the working target there (loot economy design 6.5).
**(change LE-24; M2)**
Consumable, scroll, wand and talisman reports: see consumables design
19.3 **(change CN-9)**.

## 20. Staging

**(lead's call)**

- **M1** (one class, solo hero, one floor):
  - records, families, instances, events and validation;
  - weapon and armour notables with obvious properties only;
  - active and spare weapon sets; armour;
  - belt of 2 with potion stacks (bonus action);
  - pack max(2, 3 + Strength) with size 1 or 2; Retrieve as an action;
  - mid-fight pickup including `claimed`;
  - the basic summary (Take, Leave, Take all, **Swap** with the displaced
    item to leftovers, and **Equip now**) and fixed leftovers markers, so a
    full solo hero can always take a found upgrade;
  - rest-gated armour changes;
  - the starting kit; value and k = 1;
  - the coin line as a counter with nothing to spend.
  - **M1 measures the swap rate and forced Leaves for a solo hero.** M1 has
    no Sell, so its pack-fill data is not used for tuning: pack fill and
    the kill switch (4.2, 19.3) are tuned on M2 data with Sell enabled.
- **M2** (full run, companions):
  - party view, party-wide placement, suggested recipient;
  - comparison strip with fit lines; pure-value swaps;
  - hand over, Administer and take-from-Downed;
  - trinkets, including worn-piece kinds, and capacity trinkets
    **(change AR-39)**; trinkets settled in the trinkets design
    (`2026-11-17-trinkets-design.md`, 19) **(change TK-16)**;
  - tonics, flasks, scrolls and wands with camp refill; consumables staged
    as in the consumables design (`2026-11-17-consumables-design.md`,
    20), the Reprieve Phial at 334‰ as consumables design 14.10 and the
    death and healing design 22 stage it **(change CN-9; change DH-35)**;
  - rarity tiers and banded properties;
  - dormant properties and Arcana identification;
  - coin, the basic vendor, the notables shop or reward card, Sell;
  - stairs Scrap; rest-gated trinket changes;
  - simulator reports.
- **M3:** heirlooms as profile templates.
- **Later:** curses, Clinging and cursed carriers; Disarmed seizure;
  coatings and Coated; Pitted Armour; the Thief role (hook: it may take one
  belt item, never the pack, equipment or a `noSteal` item, holds it as its
  carried item and drops it under enemies 15.6); **the wandering trader**
  (10.9) with its `trader` channel (I-14); the yielded or cowed trader
  flavour; a stash only if playtests demand one.

## 21. Risks

- **Strength's capacity job may go dead** if packs rarely fill with
  curated loot and selling. Mitigation: the pack-full targets and the
  kill-switch order (4.2).
- **Party-wide placement depends on a good suggester.** A bad suggestion
  turns per-unit ownership into mule shuffling. Mitigation: the ≥ 90%
  agreement target and the pooled-pack fallback (6.4).
- **Retrieve as an action and rest-gated armour and trinkets may feel
  harsh** ("my potion is in my pack"). Mitigation: clear reason text and
  the belt as the pre-fight choice. Both are client decisions: if
  playtests confirm the harshness, the lead brings a proposal to the
  client (for example `packInCombat: freeOncePerFight`); builders do not
  change them as tuning.
- **Three lists per unit** (weapon sets, belt, pack) plus armour and
  trinkets is more structure than one bag. Mitigation: the combat HUD
  shows only the belt; the summary auto-places.
- **Dormant properties are a modest stake** until curses arrive. If Arcana
  rarely changes a choice, raise the rollable share of Rare and above
  records (a content lever: fixed properties are always obvious, so only
  rolled lines can be dormant; item rolls design 4.6, 15) **(change
  IR-4)**. Delaying the free camp identify would change a client
  decision, so it is only a proposal to bring to the client.
- **k = 1 rests on approximate action-worth maths.** Validation rule 7 and
  the carrier-room report must confirm it before content scales.
- **Scrap adds a little coin inflation.** Keep the scrap rate well below
  Sell.
- **Two sizes are a coarse volume signal.** Players who enjoy packing
  puzzles will not get one. The trade is deliberate.
- **The wandering trader (Later)** could become a must-visit detour or a
  coin sink that undercuts camp. Mitigation: stock carved from the
  existing budget with at least one container and one carried notable
  left free, no supply or healing, prices strictly above camp, and the
  visit and free-notable reports. If parties feel forced to detour, lower
  its presence chance or its stock.
- **The trader's Recharge** is a paid refill outside camp. Mitigation: once
  per trader, one wand, coin-priced, and the wand-charge report (19.3,
  change I-16).
- **Barter could become a laundering route** (turning unwanted loot into
  wanted loot above Sell value). Mitigation: one-for-one, no change given,
  and the barter rate is a tuning value.
- **Dependencies.** Category designs and dungeon generation are not yet
  designed (section 24). Numbers here are provisional until they are.
  Death and healing is settled in the death and healing design
  (`2026-11-17-death-healing-design.md`). **(change DH-35)**
- **Protected content.** SRD tier names and any SRD item are attributed
  (CC-BY-4.0). The bag of holding is cited as a banned shape, not used.
  Other games are mechanism references only.

## 22. Left to the builders

- All numbers in `inventory.json`: pack base and minimum, belt and trinket
  counts within the agreed shape, `stackMax` values, Sell, Scrap and barter
  rates, the notables-shop size, the Leadership discount within its shape,
  coin per value, category multipliers, the minimum price, the dormant
  allowance, the room guard, family caps, rarity budgets and depths, vendor
  stock, and the trader's floors, multiplier, presence chance, stock size,
  guards and service prices.
- **Not tuning:** `packInCombat: action` and the free camp identify are
  client decisions; changing them needs client approval.
- `identifyDifficulty` values per record.
- Party view, summary, shop and trader layouts within sections 5.5, 8 and
  10.
- Icons, frames and models within "never colour alone".
- The JSON schema, built by the core developers with the content designer.

## 23. Changes to approved documents

Applied on 2026-11-16, when this document was approved. Each change names
the exact section it touches. I-14 and I-16 are applied as text marked Later.

**Conditions design (2026-11-16):**

| # | Section | Change |
|---|---|---|
| I-1 | 18.11, row "Encumbered \| Moved to the items design" | Encumbered is **retired**. The hard slot cap replaces it: additions are blocked, nothing is ejected (items 5.4). |
| I-2 | 14, bullet "At most 2 harmful lingering conditions per unit"; 18.10, Cursed (curse) | Later. An item curse's cost is a `whileEquipped` item property, not a lingering condition. **Cursed (Clinging) from an item is exempt from the cap of 2**, as Scarred is (items 13). |
| I-3 | 8, `tonic` cure row | Add "Given to an adjacent ally (action): minor only", matching the thrown limit (items 7.1) |
| I-4 | 18.3, Disarmed (Later) | Party side: a party weapon still on the ground at combat end returns to its owner. A Clinging weapon is never dropped (Later). |

**Ability model (2026-11-16):**

| # | Section | Change |
|---|---|---|
| I-5 | 7.2, universal and skill actions table | New rows: Administer (action; tonic minor only; a potion does not lift Downed); Hand over (giver's free interaction, belt or weapon sets only); Take from a Downed ally (free interaction); Retrieve (action, `packInCombat` flag); pickup destinations including `claimed`; no Drop; no armour or trinket changes in combat. "Drink a potion" means a potion on your own belt. The row "Switch the wielded weapon \| Bonus action" becomes "Swap weapon sets (active and spare, shield included) \| Bonus action". (items 5.2, 7.1) |
| I-6 | 15.3, bullet "The party carries at most 3. How supply uses carrying slots is for the items design." | Answered: supply **uses no slots**; it is an integer party counter, never an item (items 5.3) |
| I-16 | 6.2, refill tag table, `camp` row ("Camp only"); 2, "Free refills" | **Later.** One named exception: a wandering trader's paid **Recharge** resets one wand's `camp` pool, once per trader. It is priced in coin, the client chose it, and the simulator reports wand charges per floor with and without it. Items validation rule 4 names the same exception. (items 10.9, 19.1, 19.3) |

**Stat model (2026-11-16):**

| # | Section | Change |
|---|---|---|
| I-7 | 6, Carrying capacity row; 10, "Items and inventory" bullet; 12, "base carrying slots" | Spare weapon = **one spare weapon set** (main + off hand, shield included). Pack = max(2, 3 + Strength) *(illustr. base 3, minimum 2)*, items of size 1 or 2, plus belt, trinkets, weapon sets and armour outside the pack. Capacity reads Strength excluding conditions. (items 5) |

**Enemies design (2026-11-16):**

| # | Section | Change |
|---|---|---|
| I-8 | 15.8, "Actions" and "Leftovers" bullets | "The only place in the dungeon where inventory is sorted" becomes **"the only place in the dungeon where found items are sorted into packs; mid-fight pickups go only to a free belt slot, an empty spare set or `claimed`, and purchases at camp or a trader use the same Take placement. Out of combat the party view rearranges weapon sets and belts at any time, and armour and trinkets at a short rest or camp."** "Take (choose who)" defaults to a core-suggested recipient, and pack placement is party-wide. "Leftovers become one map marker" becomes **one fixed marker per room**, scrapped at the stairs. (items 6, 8, 9) |
| I-9 | 15.3, steps 3–4 | A boss signature item carries no cost add (the boss guards validate). New fallback: a cost add above 25% *(illustr.)* of the room budget sends the item to a container in that room (items 12.7). |
| I-10 | 10.5, step 3; 15.3, step 1 (channel split); 20.1 (per-channel count check) | Append "then the next camp's `camp` channel (its count and notables) and the vendor's limited potions" after wand charges, still on the loot stream and under the same family caps and owned-base rerolls. The floor loot budget gains a **`camp` channel** (1–2 notables, *illustr.*) for the notables shop, drawn whether or not the shop is chosen; the count check includes it. Camp stock counts toward later floors' family caps only once bought (items 10.4). |
| I-11 | 15.7, bullet "Salvage is sized at about 10–15% of a floor's coin income" | "A floor's coin income" means found coin. Item Sell and Scrap income is reported separately (items 10.2). |
| I-12 | 15.3, step 2 | Clarification: "quality" = (rarity tier, then value) (items 12.5) |
| I-14 | 10.4, floor plan table; 10.5, step 2; 10.5, step 3 (draw order and the sentence "The only input from outside the loot stream is the party's `requires` tags and owned bases"); 15.3, steps 1–2; 20.1 (per-channel count check) | **Later.** A `trader` channel. 10.4: an optional off-path side room that can host the trader, floors 2–4. 10.5 step 2: two dungeon-stream draws, always made, for presence and stock count. 10.5 step 3: the trader count is carved first from the containers share, then the enemy-carried share, each keeping at least 1 notable *(illustr.)*, never the boss; below 2 there is no trader. Its notables are drawn after the boss channel, then any returned trader slots on the containers channel, then wand charges, the camp channel, the trader's tonic (moved from the containers channel, no draw) and its service. The "only input" sentence adds trader presence and count. 15.3 step 2: a trader slot is never substituted; it returns to the containers channel. Unbought trader stock counts toward later floors' family caps only once bought or bartered. The count check includes the trader channel. (items 10.4, 10.9) |

**Main design (2026-11-15):**

| # | Section | Change |
|---|---|---|
| I-13 | 4.1, bullet "Between floors the party reaches a camp: full rest, a shop or reward, and a chance to recruit." | **(agreed)** Every camp has a **basic vendor** (supply, potions in limited pre-rolled stock, tonics, Remedy, mundane bases). "A shop or reward" is a choice between a **notables shop** (1–2 notables) and a **camp reward card**, separate from the enemies 17.5 boss reward card (items 10.3, 10.4). |

**Dependency pointers (all four parents):**

| # | Section | Change |
|---|---|---|
| I-15 | Stat model 10, "Items and inventory" bullet; ability model 20, "Items and inventory" bullet; conditions 25, "Items and inventory" bullet; enemies 25, "Items and inventory" bullet and 15.9, "Left for the items design to decide" | The generic layer is no longer pending: point to this document. What remains open moves to the category designs (items 17): weapons, armour values and Strength thresholds, starting kits, tonics, Remedy and flasks, scrolls and wands, Coated, Disarmed, Clinging and Pitted Armour. |

None of these touches an agreed row: slots of base + Strength, no weights,
mundane bases are not loot, and family caps all stand.

## 24. Depends on systems not yet designed

- **Category designs** (section 17): every sub-record, property table,
  use-value table and price peg. The generic layer fixes only their shape.
  Weapons and foci are no longer pending: see the weapons design
  (`2026-11-17-weapons-design.md`). Disarmed and Coated stay Later;
  starting kits remain open in their category designs. **(change W-15)** Armour and shields are no
  longer pending: see the armour design (`2026-11-17-armour-design.md`),
  which also fixes the armour part of starting kits (13.6) and hands
  worn-piece kinds to the trinkets design. **(change AR-39)** Trinkets
  are no longer pending: see the trinkets design
  (`2026-11-17-trinkets-design.md`), which settles the trinket category,
  worn pieces included; what it still needs is in its section 23.
  **(change TK-16)** Consumables, scrolls and wands are no longer pending:
  see the consumables design (`2026-11-17-consumables-design.md`);
  coatings and Coated stay Later, and what it still needs is in its
  section 24. **(change CN-8)**
- **Death and healing:** no longer pending: see the death and healing
  design (`2026-11-17-death-healing-design.md`). Companions cannot die at
  launch, so 15's conditional rule stays dormant (death and healing design
  6.4, 11); no healing lifts Downed, as Administer assumes (death and
  healing design 11.5). **(change DH-35)**
- **Dungeon generation:**

| Requirement | Consumers | Wave |
|---|---|---|
| Containers and lair caches for the loot channels | Loot | M1 |
| Room identity for leftovers merging and flee piles | Leftovers | M1 |
| A stairs transition that runs the left-behind warning and Scrap: the Stairhead behind the arena, through a Bossgate that opens on `BossDefeated`; none on floor 5 (floor layout design 6.11, change FL-6) | Stairs | M1 |
| An off-path side room per floor that can host the trader, and its presence and stock-count draws at the floor plan | Wandering trader | Later |

- **Classes and offers:** see the classes design
  (`2026-11-17-classes-concepts-design.md`). The belt +1 is the Shade's
  *Deep Pockets* talent (hard maximum 3; classes design 8.4); capacity
  offers are unchanged; `classTags` for scrolls and wands resolve to the
  launch class ids (consumables design 13.1, change CL-12a); Arcana
  mastery's signature use (it could interact with identification) goes to
  the classes design's stage 2 (classes design 8.5, 14). **(change
  CL-13h)**
- **Companions:** companion kits; the companion scorer's "drink it"
  suggestion and its placement preferences.
- **Camp and hub screens:** the camp layout that hosts the vendor, the
  shop-or-reward choice, Sell and the party view; **what the camp reward
  card offers** (any item it offers comes from the `camp` channel, 10.4).
- **Dungeon content:** runes and shrines that identify items or end
  Clinging (Later).

## 25. Open items for the client

None. Approved by the client on 2026-11-16.
