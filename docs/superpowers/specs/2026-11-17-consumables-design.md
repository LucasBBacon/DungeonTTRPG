# Consumables, Scrolls and Wands: Design

Date: 2026-11-17
Status: Approved, 2026-11-17
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design) and
`2026-11-17-trinkets-design.md` (trinkets design)

Items marked **(agreed)** were decided by the client on 2026-11-17 for the
four questions this design raised (scrolls, the save item, its cost, thrown
flasks), by choosing the lead's recommendation in each case. Under Q2 the
client approved the **principle** of the save item now; it ships with the
death and healing design, which sets its final numbers and owner order.
Items marked **(lead's call)** were filled in by the design lead; the
client said on 2026-11-17 that everything else in the lead's proposal
stands, and they were confirmed when the client approved this document on
2026-11-17. Items marked
**(settled)** restate a rule from a parent document; **(decided)** marks a
main-design client decision. Items marked **(change)** amend an approved
document and are listed, with an id, in section 23. Every item here is
binding on the builders.

**Two changes to agreed rows were not put to the client as separate
questions** (CN-11, ability model 6.4; CN-20, enemies 15.4). They were
written as the lead's recommendation and were **approved by the client on
2026-11-17** with this document (section 23, section 25.2).

This is the **category design** for rows 4 and 5 of the items design's
roadmap: row 4 (consumables: potions, tonics, flasks, coatings, Remedy) and
row 5 (scrolls and wands) of items design 17, which are its binding brief.
It also answers the client's three consumable ideas (section 5): scrolls
for members who cannot cast, an item that saves someone from going down,
and thrown beneficial flasks that hurt.

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. Tuning numbers live
in a `consumables` block of `inventory.json` (18.1). The builders own them
and the simulator calibrates them (section 22), except where a value is
marked as a client decision or a lead's number.

**SRD check (lead's call).** The SRD-shaped mechanisms adapted here (Spell
Scroll's class-list rule and "DC 10 + spell level" check, Potion of
Healing, alchemist's fire, oil, holy water, the spell shapes behind the
scroll and wand catalogue, wand charges) are written from memory. **Every
SRD-shaped mechanism must be checked against the SRD 5.2.1 document before
any record is written**, and every record flagged `srd` cites the entry it
was checked against (section 17).

**Waves:** M1 is the vertical slice, M2 the full run with companions, M3
profile and unlocks, Later everything after M3 that no milestone names.
**(settled, main design 9; items design header)** Items design 17 makes
consumables M1 for potions and M2 for the rest, and scrolls and wands M2.
**(settled)**

## 1. Intent

Consumables are what a unit spends once: the healing potion on its belt,
the tonic that ends a condition, the fire bomb a fighter throws, the
scroll a caster reads when its slots are gone, the wand that gives a few
casts per floor. The parents settle the belt, the stack sizes, the action
costs of drinking, giving and reading, the out-of-class Arcana rule, wand
charges with a camp refill, the tonic's shape, Remedy, the price formula
and the healing peg against supply. **(settled)** They leave open what each
kind is for, how strong it is, what it may never do, its value and price,
how it is found, how monsters use it, how flasks are thrown, what "in
class" means for a scroll, and whether potions can be thrown. This
document closes those questions and adds one new item, the Reprieve Phial.

The system should make consumables **tempo and answers, never a second
power curve**: each kind has one job the player can name, drinking in
ordinary fights is obviously right, coin never replaces rest healing, and
the rare consumable that saves a run fires itself so it cannot be hoarded.
**(lead's call)**

## 2. Goals, non-goals, anti-goals

### Goals

- **One job per kind.** Potion = self tempo healing; tonic = in-fight
  cure; Remedy = lingering cure out of combat; flask = thrown setup and
  small offence; coating = temporary weapon rider (Later); scroll = one
  cast of a class spell; wand = a small camp-refilled spell pool;
  talisman = an automatic one-use trigger. **(lead's call)**
- **Tempo over power.** Every value-1 consumable is about one action's
  worth (items 12.6). Strong save-the-run power exists only in an
  automatic, non-hoardable shape. **(lead's call)**
- **One healing record, one difficulty formula.** The healing potion
  reads the drinker's Recovery; every item effect uses the user's ability
  difficulty. No new number ladders. **(lead's call)**
- **Class identity kept.** In-class casters read scrolls slot-free with no
  check; out-of-class reading stays Arcana's job. **(agreed, Q1)**
- **A save is never silent.** The phial shows when it is armed, previews
  when it would fire, and announces when it does. **(lead's call)**
- At most four things to read per unit in combat; the combat HUD shows
  the belt. **(settled, ability model 11.4; items 5.5)**

### Non-goals

- **Tiered potion records** (minor, greater, superior) that fragment the
  2-slot belt and vendor stock. **(lead's call)**
- **Ground squares from flasks.** Flasks act on units only. **(settled,
  ability model 8.4; main design non-goals)**
- **A mid-fight revive item.** Getting a Downed companion up stays
  Medicine's trained-only job. **(settled, stat model 7.2; agreed, Q2)**
- **Learning spells from scrolls.** **(lead's call)**
- **Untrained scroll reading.** **(agreed, Q1)**

### Anti-goals

- **Nova turns:** a slot spell plus a scroll spell, or several bombs, in
  one turn. **(settled, ability model 2)**
- **Coin buys healing or lives:** potions replacing supply, or a purchasable
  extra life. **(settled, items 10.5 peg; lead's call for lives)**
- **Hoarding for the boss:** power consumables carried unused all floor.
  **(settled, items 10.8)**
- **Feel-bad friendly fire on a heal:** "my healing potion killed my
  hero". **(agreed, Q4)**
- **Free refills** of wands or scrolls. **(settled, ability model 2)**
- **Potion roulette.** **(settled, items 2; consumables are always
  identified, items 11)**
- **Protected content.** **(settled)**

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Main design 4.2 (decided) | "The run ends when the hero dies." Downed companions get up on 1 HP after a won fight; the first downing per floor gives Scarred | The hero has no downed state; a save item must act before 0 (section 14) |
| Items 4, 5.1, 5.2 (agreed) | Belt 2, a 3rd from a feature or offer, hard maximum 3; the only place consumables, scrolls and wands are used from in combat; `stackMax` per kind | Belt rules (section 8) |
| Items 7.1 | Potion: bonus action. Tonic: bonus action minor, action major, thrown at an ally (action) minor only. Scroll or wand in class: its ability's cost. Out of class: action + trained Arcana; a failure uses it up. Administer, Hand over, Take from Downed | Delivery (section 11); scrolls (section 13) |
| Items 10.3 (agreed) | The basic vendor: supply, healing potions in limited pre-rolled stock, tonics, Remedy, bases | Vendor list unchanged (section 10) |
| Items 10.5, 10.8 | Price formula; potion healing per coin below supply; tonic and Remedy prices as the condition knob; anti-hoarding list; banding not counted | Prices and pegs (section 9); section 8 |
| Items 11 (agreed) | Consumables, scrolls and wands are always identified | No dormant lines on consumables |
| Items 12.3, 12.5, 12.6, 12.8 | `use {ability, targets[], difficulty: user \| fixed:N}`; value = rarity budget; the use-value table is this design's; stacks are `never` pools; wand charges pre-rolled, `camp` refill | Data (section 18); value (9.1) |
| Items 17 rows 4, 5 | The brief: effects, `stackMax`, use-value table, prices and pegs, thrown flasks, "whether potions can be thrown (default no)", Coated (Later), `useWhen`, banding; `classTags[]`, `use.difficulty`, wand charges, the out-of-class Arcana rule | This document's scope |
| Ability model 6.2, 6.4 (agreed), 7.1 | Pool rule; one camp spend per unit per turn; "a pool spend is separate from the cost" | Slot-free in-class reads (13.2); the nova guard (13.5) |
| Ability model 7.2 | Use a scroll or wand outside your class: Arcana, trained only, action; a failure uses it up, no free retries | Out-of-class reads (13.2) |
| Ability model 8.2, 8.3, 8.4 (agreed), 9.5 | Friendly fire unless `selective`; the core preview; no ground zones; concentration check on any damage | Flasks (section 12); why glass damage fails (5.3) |
| Ability model 10.2 | Item-granted abilities do not count toward the hero's 8 limited abilities | Scrolls and wands do not crowd the sheet |
| Stat model 6 (agreed) | Ability difficulty = 8 + proficiency + the class's main ability; Recovery grows with level and Constitution; speed 6 | `user` difficulty (13.3); the healing record (7.2); the throw maths (5.3) |
| Stat model 7.2 | Trained-only uses include "using a scroll or wand outside your class" | Untrained readers have no scroll route (5.1) |
| Conditions 8, 13, 14 | Tonic cure tag; "consumables keep the 10-turn cap"; Remedy ends one non-curse lingering condition; Scarred exempt from the lingering cap | Tonic and Remedy (section 7) |
| Conditions 18.2, 18.9, 18.10 | Downed; Death-warded (Later, "the first drop to 0 hit points leaves it on 1", always `combatOnly`); Scarred (agreed; the hero is never Scarred) | The phial (section 14) |
| Enemies 6.4, 15.3, 15.4 (agreed) | Permadeath guards; sundry-like "1–2 supply and a few tonics"; carriers use what they carry | Monster use (section 15) |
| Weapons 9.1, 14 | Thrown range 5 for the dagger; wands are never wielded or foci | Flask range (12); wands (13.6) |
| Armour 14.3; trinkets 13.2, 13.5, 13.7, 23 (Death and healing bullet) | Last Ward (Later); Slinger's Bracers (Later); Lifeline Pendant; Scrollwright's Token and Last-Breath Charm (Later); death-ward owners left to death and healing | Inputs to sections 11, 13, 14 |
| Main design 2 | No D&D names; SRD content carries attribution | Section 17 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored.

### 3.3 Findings in the parent documents

Gaps found while designing; each is fixed by a change in section 23.
**(lead's call)**

| # | Finding | Fix |
|---|---|---|
| F1 | No document says that an in-class scroll read spends no slot, though it follows from items 7.1 and ability model 7.1 | Stated outright (CN-3, CN-12) |
| F2 | The tonic's "thrown at an ally" row has no range | `throwAllyRange` (CN-3, CN-17) |
| F3 | A scroll's pool is `never`, so ability model 6.4's camp-spend sentence does not cover scrolls; a caster could cast a slot spell and read a bonus-action scroll in one turn. (A wand charge is already covered: its pool is `camp`, items 12.8) | An explicit engine rule for scroll reads (13.5; CN-11, client-approved 2026-11-17) |
| F8 | The approved Administer row allows a potion on a Downed ally ("does not lift Downed"), and Hand over, Take from Downed and Retrieve allow any belt item in combat | Narrowed for potions, healing scrolls and the talisman (11, 14.5; CN-3, CN-12) |
| F9 | No approved document gives a Recovery formula (stat model 6 names the stat only) | An M1 placeholder and a dependency (7.2, 24) |
| F4 | Several condition sources name potions that this design cuts (Hasted, Fleet, Heightened, Unfettered, Invisible, Rallied) | Struck (CN-16) |
| F5 | Items 12.5 and 5.1 `rarity.props` ranges read as if a use-only record needed 2–3 properties at Rare | Ranges apply to gear only (CN-6) |
| F6 | Items 19.1 rules 8 and 9 key on `consumable`, not scroll or wand | Extended (CN-7) |
| F7 | Enemies 15.4's healing-potion row assumes a monster heal number that does not exist (monsters have no Recovery) | `monsterPotionPermille` (CN-20, client-approved 2026-11-17) |

## 4. Framework decisions

The client chose the first four on 2026-11-17. **(agreed)** The rest are
the lead's calls, which the client said stand.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Idea 1, scrolls (Q1) | **(a) Keep the Arcana requirement (option B):** in class = the reader's class is in `classTags`, any spell on the list, the spell's own cost, no slot spent, cast at the scroll's tier, never upcast; out of class = action + trained Arcana against 10 + tier, always rolled, a failure burns it. Untrained members have no scroll route; flasks are their one-use power. **(agreed)** | (b) D′, an untrained tier-1 flat roll: changes the trained-only list and thins Arcana's job where scrolls are commonest; **rejected by the client**. (c) A, anyone reads, no check: empties Arcana's agreed item job. C (plain scrolls) kept as a Later hook; D (untrained with disadvantage) a trap |
| 2 | Idea 2, save item (Q2) | **(a) The Reprieve Phial (option B):** talisman category, belt, holder only, automatic when the holder would drop to 0 HP (order Death-warded → talisman), leaves 334‰ of max HP, Rare, floor 3+, 1 per run, unique, never bought; **can save the hero**. Principle approved now; final numbers and owner order set by the death and healing design (15, 16). **(agreed; change DH-38)** | (b) No item, the extra life left to death and healing. Option A as stated (party-wide, fires on a downing): can never save the hero and is spent on the first harmless companion downing. Option C (smelling salts): undercuts Medicine and Lifeline |
| 3 | The phial's cost (Q3) | **(a) Scarred, the hero included, with `treatProof`:** Treat cannot end it; a Remedy or camp does; it does not set `downedThisFloor`. **(agreed)** | (b) Standard Scarred: one Treat roll at the next rest buys a life. (c) No Scarred: a free life |
| 4 | Idea 3, thrown flasks (Q4) | **(a) No glass damage;** potions throwable at a willing ally for the thrower's action, range 5, line of sight, no roll, at `thrownPotionPermille` 750 *(illustr.; knob 500–1000, merge rule at 1000)*; never lifts Downed; tonics unchanged (minor only), range `throwAllyRange`. **(agreed)** | (b) Keep potions unthrowable. (c) Glass damage as proposed: a tax that can down a 1-HP ally or end the run, breaks concentration and needs an AI friendly-fire term |
| 5 | Healing shape | **One Recovery-linked record** (`potionPermille` 1000) **(lead's call)** | Banded dice per drinker band (the named fallback, 7.2); tiered records (fragment the belt and stock) |
| 6 | Effect difficulty | **`user` for every item effect**, the existing schema default **(lead's call)** | New `tier` or `band` values (a schema change for no gain); fixed per-tier scroll DCs (a fighter's scroll would out-DC its own abilities) |
| 7 | Scroll nova guard | **A scroll read sets the camp-spend flag** **(lead's recommendation; CN-11 client-approved 2026-11-17, 25.2)**; a wand charge already does **(settled, items 12.8)** | Belt slots alone (four readers × 2–3 slots at the boss); every scroll costs an action (kills in-class bonus-action spells) |
| 8 | In class | **The reader's class is in `classTags`**; any spell on the list; no "has reached that tier" test **(lead's call)** | A tier test (misfires on companion kits; the floor cap already tracks tiers) |
| 9 | Loot classes | **Sundries** (Common consumables, outside the notable count, inside the fixed budget) and **notables** (scrolls, wands, Greater Fire Bomb, talisman) **(lead's call)** | Every consumable a notable (a bomb displaces a sword); consumables only by substitution (too scarce) |
| 10 | Fire bomb strength | **Fixed dice per record**; one-shots never banded **(lead's call)** | Banded 2d6/3d6/4d6 at value 1 (out-damages a tier-1 scroll at band 3) |
| 11 | Offensive flask nova | **At most one offensive flask per unit per turn** **(lead's call)** | Flasks as camp spends (blocks a fighter's Surge turn from ever throwing a bomb) |
| 12 | Remedy | **Out of combat only,** from the pack, not beltable **(lead's call)** | A belt action (a slot with no tactical content) |
| 13 | Talisman category | **A new category `talisman`**, like scroll and wand **(lead's call)** | A consumable `kind` (caps and permille are keyed by category: it would cost 36 coin and be uncapped) |
| 14 | Talisman channels | **Containers floor 3+, boss reward card, camp reward card (`campReward`); never bought; placed by one pre-roll per run (14.7)** **(lead's call, inside agreed Q2)** | The notables shop (coin buys a life); a weighted draw on every floor 3+ (near-certain every run) |
| 15 | Coatings | **Named rider only** (Later) **(lead's call)** | Element shift plus rider (duplicates Edge and Ember Ring; collides with TK-25); own-type rider flag (duplicates Honed) |
| 16 | Vendor | **The client's list unchanged** **(settled, items 10.3)** | Water and oil flasks on the vendor (a client-list change for little) |

### 4.1 How other games handle consumables, scrolls and extra lives

Mechanism references only; no names or text are taken from any of them.
Details are as recalled unless marked "checked". Anything SRD-shaped is
checked against SRD 5.2.1 before data (section 17). **(lead's call)**

| Game | What it does | We take | We avoid |
|---|---|---|---|
| **5E SRD 5.1 / 5.2** | Spell Scroll: the spell must be on your class list; a higher-level spell needs an ability check against DC 10 + spell level, and failure wastes it; fixed DC and attack by level. Potion of Healing tiers (2d4+2 up). Alchemist's fire, oil, acid, holy water. Wands with charges and a dawn recharge. Death Ward (drop to 1 instead of 0). 2024: drinking a healing potion is a bonus action | The class-list gate and the "10 + level" check shape; failure burns the scroll; bonus-action potions; charges plus a fixed refill | Tiered potion records that fragment stacks; "crumbles on a 1" wand noise; fixed scroll DCs that make a fighter's read stronger than its own abilities |
| **Pathfinder 2e** | Trick Magic Item (trained skill, item-level DC, crit-fail lockout); Healing Bomb (a thrown elixir that hits a willing target for no damage); hero points spent to stabilise | A trained-skill gate on off-class items; a beneficial throw that never hurts | Lockouts; meta currencies |
| **13th Age** | A potion spends one of your Recoveries | Healing that reads the drinker's own recovery number | — |
| **Cairn, Knave** | Anyone reads scrolls and spellbooks, paid in fatigue or slots | — | Every fighter as a part-time caster |
| **Baldur's Gate 3** (checked) | Any class reads any scroll, no check, at the reader's modifiers; drinking is a bonus action; throwing is an action that splash-heals; a direct hit damages the ally | Casters stay the better readers through their own numbers; drink = bonus action, throw = action | Ungated scrolls (they empty Arcana's job); splash healing (breaks the coin peg); friendly damage on a heal |
| **Divinity: Original Sin 2** | Anyone uses scrolls and grenades for action points | Grenades as the non-caster's area answer | Ungated scrolls |
| **Slay the Spire** | 3 potion slots (2 at high Ascension); an automatic extra life sits in a potion slot and heals about 30% on death | The slot cost of an automatic extra life; auto-firing, so it cannot be hoarded; a real chunk of HP, not 1 | — |
| **Hades** | A death-defiance charge (about half HP); extra charges bought with meta currency | Auto-firing; a visible pip | Meta-bought lives (the "grind to win" anti-goal) |
| **Risk of Rain 2** | A legendary item revives once, then becomes a husk | Rare, single, consumed visibly | — |
| **Battle Brothers** (checked) | A perk survives one fatal blow per battle on a few HP; struck-down brothers may survive with a permanent injury | The scar as the price of survival | A per-battle recharging life |
| **Wildermyth** (checked) | At 0 HP the player chooses death or a maim or scar | Survival leaves a mark the player sees | Permanent injuries (our Scarred lasts until camp) |
| **Darkest Dungeon** | 16 inventory slots for provisions; deathblow rolls on a death's-door state with a lingering debuff | Slot pressure as the hoarding cap | Hidden death rolls |
| **Shiren the Wanderer** | Revival herbs fire from the bag; enemies can steal them | — | Firing from the bag (here only the belt acts in combat) |
| **NetHack, DCSS, Brogue** | A life-saving amulet is destroyed on use; DCSS and Brogue keep extra lives almost absent; monsters use their carried wands and potions | Monsters spend what they carry (already settled) | — |
| **XCOM** | Grenades hit friends; medkits at range need a perk | Area offence hits allies; cheap ranged support is a perk step (our Slinger's Bracers) | — |

**Overall:** consumables that get used are **tempo** items (bonus-action
heals, cures) behind a hard slot cap; **power** consumables get hoarded for
the boss. Extra lives that work under permadeath are single, visible, rare,
automatic, cost a slot all run and leave a mark; recharging lives cheapen
death. Scroll gating splits tabletop (gated) from CRPGs (open); the gate
keeps class identity and a skill job, and a slot cap keeps the flexibility
small.

## 5. The client's three ideas

| Idea | Verdict | Decision |
|---|---|---|
| 1. Scrolls for members who cannot cast | **Adapt** | A caster out of slots reads any in-class scroll slot-free; a member trained in Arcana reads any scroll; an untrained member has no scroll route, and flasks are its one-use power **(agreed, Q1)** |
| 2. An item that saves a downed member | **Adapt** | Not a revive: the **Reprieve Phial** fires when its holder would drop to 0 HP, leaves it on 334‰ of maximum HP and Scars it; it can save the hero **(agreed, Q2, Q3)** (death and healing design 16) **(change DH-38)** |
| 3. Thrown beneficial flasks that hurt | **Adapt, decline the damage** | Potions become throwable at an ally for the thrower's action, range 5, at 75% strength; no glass damage **(agreed, Q4)** |

### 5.1 Idea 1: scrolls for members who cannot cast

**The client asked:** a one-use spell, each scroll a specific spell, usable
by a member that cannot cast it: a non-caster, or a caster out of spell
slots.

**What the approved rules already give:**

- **A caster out of slots can already read every in-class scroll.** Items
  7.1: "Scroll or wand in class: its ability's cost" **(settled)**. Ability
  model 7.1: "A pool spend is separate from the cost". A scroll's pool is
  its own `never` stack (ability model 6.1, 6.2; items 12.8). So an
  in-class read spends **no slot**. This design says it out loud (CN-3).
- **A non-caster can already read any scroll, if trained in Arcana:**
  action + trained Arcana; a failure uses it up; no free retries
  **(settled, ability model 7.2; items 7.1)**. It is one of the four
  trained-only uses (stat model 7.2).
- **What would be genuinely new:** an *untrained* non-caster reading a
  scroll.

**Options considered:**

| Option | What it does | Outcome |
|---|---|---|
| A. Anyone reads any scroll, no check | In class: the spell's cost. Out of class: an action, no check | Not chosen: changes the settled out-of-class rule and the trained-only list; empties Arcana's agreed item job (items 1); every fighter becomes a part-time caster; Scrollwright's Token loses its job |
| **B. Keep the gate; state the slot-free rule; add the missing limits** | Section 13 in full | **Chosen (agreed, Q1(a))** |
| C. B plus plain scrolls anyone reads (`classTags: ["any"]`) | A short list of Common utility scrolls with no damage, healing or major conditions | Not chosen: every candidate duplicated something else (Wardstone, the Outlining wand, Rally). **Kept as a Later hook** (25.3) |
| D′. B plus an untrained tier-1 flat roll | d20 + Intelligence, no proficiency, against 11 *(illustr.)*; a failure burns it | **Offered as Q1(b); rejected by the client, 2026-11-17** |
| D. Untrained with disadvantage | As D′ with disadvantage | Not offered: about 20% at tier 1 and it burns the scroll, a trap |

**Decision (agreed, Q1).** Keep the Arcana requirement:

- **In class:** the reader's class is in the record's `classTags`; any
  spell on the class list, known or not; the spell's own cost; **no slot
  spent**; cast at the scroll's tier; never upcast; no check.
- **Out of class:** action + trained Arcana against 10 + tier (11 to 14;
  a client number, not tuning), **always rolled**; a failure burns the
  scroll **(settled)**.
- **Untrained members have no scroll route.** Their one-use power is
  flasks (fire bombs, section 12), made real by at least 1 and at most 2
  offensive sundries per floor *(illustr.)* from floor 2 (section 10) and
  by the binding dependency that Arcana be reachable (DEP-1, 13.8).
  **Floor 1 has no offensive flask by design** (the Fire Bomb is floor 2+,
  and floor 1 is the solo floor), so the untrained hero's floor-1 gap is a
  stated choice, watched by a report (19.3). **(lead's call)**

Stated plainly: the client's caster case is delivered in full; the
non-caster case is delivered for **trained** readers; the untrained
fighter does not read scrolls.

**What it changes in agreed or settled rules:** wording only in items 7.1
and ability model 7.2 (CN-3, CN-12); one new sentence in ability model
6.4, an **agreed row**, which makes a scroll read count as a camp spend
(CN-11, **client-approved 2026-11-17**, 25.2; a wand charge already counts,
items 12.8). The trained-only list (stat model 7.2) is **unchanged**.

**Gained for the client:** slot-free in-class reads stated outright; any
spell on the class list, so a scroll gives a caster breadth; a visible
Arcana success chance; scrolls up to tier 4 on deep floors.

### 5.2 Idea 2: an item that saves a member who goes down

**The client asked:** not a party spell; if in inventory, automatically
consumed when a party member goes down in a fight; with more Scarred than
usual, no Scarred, or another balancing cost.

**What the approved rules fix:**

- **The hero has no downed state:** the run ends when the hero dies
  **(decided, main design 4.2; conditions 18.2)**. Enemies 25 asks the
  death and healing design to confirm this.
- **A companion downing is not lethal:** it gets up on 1 HP if the party
  wins **(decided)**. Its first downing per floor gives Scarred
  **(agreed)**: maximum HP −10% until camp, removed by Treat at a short
  rest or a Remedy; "the hero is never Scarred" (conditions 18.10).
- **Getting a downed companion up** is Medicine (trained, action); a potion
  does not lift Downed **(settled)**. Lifeline Pendant makes it a bonus
  action once per fight (trinkets 13.5).
- **Drop-prevention shapes that already exist, all Later:** Death-warded
  (conditions 18.9); armour Last Ward (1 per camp, a hit that would drop
  you to 0 misses; armour 14.3); Last-Breath Charm (1 per camp,
  Death-warded at fight start; trinkets 13.7). Trinkets 23 (Death and
  healing bullet) leaves owners to the death and healing design, which
  has since retired Last Ward and Last-Breath Charm (death and healing
  design 15.3).
- **The belt** is the only place consumables act in combat (items 5.2);
  `whileCarried` party-wide grants are banned for trinkets in M2.

**Options considered:**

| Option | What it does | Outcome |
|---|---|---|
| A. Anywhere in the inventory, party-wide, fires on a downing | When any member goes down, it is consumed and that member gets up | Not chosen: it **can never save the hero**, who does not go down; it is spent on the first, usually harmless, companion downing; firing from the pack breaks the belt-only rule; a mid-fight revive is Medicine's trained-only job; "more Scarred" cannot be expressed (Scarred is binary) |
| **B. A drop-save phial on the belt that protects its holder** | Section 14 | **Chosen (agreed, Q2(a))** |
| C. A companion-only reviver (smelling salts) | An action gets an adjacent Downed companion up without Medicine | Not chosen: nothing for the hero; undercuts Medicine and Lifeline; not automatic |
| D. No item | The extra life left to the death and healing design | Offered as Q2(b); **not chosen** |

**Decision (agreed, Q2, Q3).** The **Reprieve Phial** (section 14):

- talisman category; on the belt; protects its holder only;
- fires automatically when the holder would drop to 0 HP, after
  Death-warded;
- leaves the holder on 334‰ of its maximum HP (a client number, death
  and healing Q8; death and healing design 16) **(change DH-38)**;
- the holder gains Scarred, **the hero included**, with `treatProof`: Treat
  cannot end it; a Remedy or camp does; it does not set `downedThisFloor`
  **(agreed, Q3)**;
- Rare, floor 3+, 1 per run, unique, never bought;
- **the principle is approved now;** the death and healing design sets
  its final numbers and owner order (14.10; death and healing design 15,
  16).

**On "who carries it":** on a companion it only saves the Downed turns and
a Medicine action, at the cost of a Scar. Players will belt it on the hero
almost every time, and that is intended: it is **run insurance that costs
half the hero's belt**, not a carrier puzzle. **(lead's call)**

**What it changes:** conditions 18.10, 14 and 18.2 (the hero may gain
Scarred; the `treatProof` flag; `downedThisFloor` untouched) (CN-14);
conditions 5.4 step 4, 18.2 and 18.9 (where the save is checked and its
order) (CN-15); a new `talisman` category in items 12.3 and 5.1 (CN-1,
CN-2); a clarifying sentence in main design 4.2 with no change to the
decided text (CN-24).

**Gained for the client:** the hero-saving item, which the stated version
could never be.

### 5.3 Idea 3: thrown beneficial flasks that hurt

**The client asked:** throwing a beneficial flask from one member at
another deals a little damage (piercing, broken glass), but the contents
apply instantly, saving the receiver's action or bonus action.

**What the approved rules already give:**

- Tonic thrown at an ally: the thrower's action, minor only; **the receiver
  spends nothing** (items 7.1; conditions 8).
- Administer a belt potion or tonic to an adjacent ally: the giver's
  action; **the receiver spends nothing**; a potion does not lift Downed
  (items 7.1; ability model 7.2).
- Hand over: free; the receiver drinks with its own bonus action.
- Potions cannot be thrown, by default (items 17 row 4).
- Slinger's Bracers (trinkets 13.2, Later): once per fight, a flask or
  tonic throw costs a bonus action.

So "the receiver spends nothing" already exists. The genuinely new parts
were (i) potions at range and (ii) glass damage as the price.

**Why the glass damage fails** (tested against the approved rules):

| Test | Result |
|---|---|
| Decision or tax | Tax: it buys nothing the settled throw and Administer do not already give |
| Permadeath | If it resolves first, it downs a 1-HP companion, or takes a 1-HP hero to 0 and ends the run; a 1-HP ally is exactly who you throw at. A tonic adds no HP, so a thrown tonic always carries the risk |
| Concentration (ability model 9.5) | Any damage forces a check against at least 10; a +2 Constitution caster fails 35% (7 in 20). Supporting a caster breaks its spell |
| "Piercing" (ability model 9.1) | Piercing's rider is an armour reduction on the attack roll; a throw at a willing ally rolls nothing, so the label has no rule |
| AI and preview | A friendly-fire term in the suggester and a "may down" preview case |
| Bonus-action version | Duplicates Slinger's Bracers and copies a healer's bonus-action ranged heal |
| Fun | "My healing potion killed my hero" is a feel-bad, not a story |

**Options considered:**

| Option | What it does | Outcome |
|---|---|---|
| A. As stated | Glass damage (1d4 or 1–2 piercing), contents instant | Offered as Q4(c); not chosen: all the failures above |
| B. A "graze" that buys a bonus-action throw | Contents first, then 1 untyped damage that cannot go below 1 HP | Not offered: three carve-outs make it no longer damage; duplicates Slinger's Bracers |
| **C. Decline the damage; throwable potions with spill** | Section 11 | **Chosen (agreed, Q4(a))** |
| D. Keep potions unthrowable | No change | Offered as Q4(b); not chosen |

**Decision (agreed, Q4):**

- **No glass damage.** "The glass shatters and some spills" is text and
  animation only.
- **Potions** are throwable at a willing ally for the thrower's action,
  range 5, line of sight, no roll, at `thrownPotionPermille` 750
  *(illustr.; knob 500–1000)*; never lifts Downed.
- **Tonics** are unchanged (minor only), with the range `throwAllyRange`.

**Why 750 and not 500 (lead's call):** speed is 6 **(agreed, stat model
6)**, so almost any ally within 5 can be reached on foot and Administered
for the same action at full strength. The throw therefore wins only when
moving adjacent is blocked or costly: an opportunity attack, a hazard,
difficult ground, Restrained, speed 0, or wanting to keep position. At 500
it would be a dead option; at 750 the throw buys a saved move and no
opportunity attacks for a quarter of the heal. **The merge rule:** below 3%
of potion deliveries *(illustr.)* the builders raise the knob; at 1000
Administer becomes the throw's adjacent case and the two buttons merge
into one ("Give: adjacent or within 5"), so nothing is ever strictly
dominated.

**What it changes:** items 17 row 4 and items 7.1 (potion throw) (CN-4,
agreed); a range on the settled tonic throw (CN-3, CN-17); Slinger's
Bracers adds potions (CN-22).

**Gained for the client:** ranged potions, with the receiver still
spending nothing.

## 6. Kinds, jobs and allowed effects

### 6.1 Kinds and jobs

**(lead's call)**

| Category / kind | Job | `stackMax` | Wave |
|---|---|---|---|
| consumable / potion | Self tempo healing (bonus action) | 3 | M1 healing; Later sidegrades with their conditions |
| consumable / tonic | In-fight cure by tier (settled shape) | 3 | M2 |
| consumable / remedy | Lingering cure, out of combat | 1 | M2 |
| consumable / flask | Thrown setup and small offence | 2 | M2 (water, oil, fire bombs); Later others |
| consumable / coating | Temporary weapon rider through Coated | 2 | Later |
| scroll | One cast of a class spell | 1 | M2 |
| wand | A small camp-refilled spell pool | 1 | M2 |
| **talisman** (new category) | A belt item that fires itself on a trigger | 1 | M2, else Later (14.10) |

`stackMax` values are the settled tuning values **(settled, items 5.1)**,
with `talisman: 1` added (CN-2).

- Every value-1 consumable is about one action's worth (items 12.6).
- Only scrolls, wands, the Greater Fire Bomb and the talisman go above
  Common.
- Strong save-the-run power exists only in an automatic, non-hoardable
  shape (the talisman).
- Items do not count toward the hero's 8 limited abilities **(settled,
  ability model 10.2)**.

### 6.2 The consumable denylist

Many trinket denylist rows (trinkets 6.2) encode global anti-goals, not
trinket rules. This design reuses them as a closed `effectKinds[]`
denylist for every consumable, scroll, wand and talisman record, with the
relaxations of 6.3. **(lead's call)**

| Never | Bound it protects |
|---|---|
| AC of any form, and any output that writes AC (Corroded), except a class spell on a scroll or wand already inside the AC envelope | AC envelope (enemies 6.4) |
| Roll mode on attacks against the user; attack bonuses | Armour owns roll mode; the buff test |
| Added damage to another source's hit: the item's own change to that hit's dice, flat amount or ladder step. A condition the item applies keeps its own approved effects, which belong to the condition (Oiled and Soaked weakness are allowed; as trinkets 6.2) | Precision parity; coatings add zero damage |
| Ability modifiers | Buff test; gear Strength would move pack capacity |
| Max HP; temporary HP (Bolstered is Later) | Scarred is the client's cost |
| Speed, flat or conditional | Items are not a speed source **(agreed, stat model 6)** |
| Flat save bonuses; always-on resistance; condition-tag immunity | Buff test; the immunity budget |
| Granting or extending Steeled, Unyielding, composure; shortening hard conditions | Conditions 7.2 anti-lock guards |
| Extra actions, turns or attacks (Haste-shaped spells included) | Nova anti-goal |
| Class-pool max, refill, refill tag; Slot Recovery; Recovery; supply | Free-refill anti-goal |
| Training, mastery, spells known (no learning from scrolls) | Offers own access (ability model 10.2) |
| Recharge of wands or scrolls, except the trader's Recharge (Later, I-16) | Free refills |
| Retrieve or `packInCombat` changes; belt-slot, dose or stack grants | Client decision; belt maximum 3 |
| Coin, prices, loot draws; identification | Fixed budget; Arcana's job; camp identifies free |
| Passive party effects; `grants.whileCarried` | M2 narrowing (trinkets decision 7) |
| Crit downgrades | Armour's crit cap |
| Acting on a class ability or pool by id | Upgrades are offers |
| **Ground squares or terrain** of any kind | Ability model 8.4 **(agreed)**; conditions 11; the main-design non-goal on ground surfaces |
| **Area, splash or multi-target healing** | The coin peg |
| **Lifting Downed; reviving after death** | Owned by the death and healing design |
| **Damage parts on a beneficial delivery** | Q4 **(agreed)** |
| **Permanent effects; durations past camp** | No permanent boosts |
| Reaction-cost spells on scrolls and wands | An automatic policy would burn a one-shot silently |

### 6.3 Relaxations

Relaxed for consumables only, and only for the named categories.
**(lead's call; the talisman row agreed, Q2)**

| Relaxation | Who | Bound |
|---|---|---|
| Healing amounts | Potions; single-target healing spells on scrolls (never wands) | Peg P1 (9.3); never area |
| Cures | Tonic by tier; Remedy for one non-curse lingering condition (Scarred included) **(settled)** | `cureImmune` |
| Class spells | Scrolls and wands only **(settled, trinkets 6.2: "scrolls and wands own item spells")** | Floor cap, never-list (section 13) |
| Major and hard conditions | Only through a class spell on a scroll or wand, under every conditions 7.2 guard. Flasks: minor or setup conditions only in M2; one named major flask (the net) Later | Conditions 7.1 (hard control only from limited abilities) |
| Saving from a drop to 0 | The talisman only **(agreed, Q2)** | Section 14 |

### 6.4 Durations and the copy registry

- Buffs last 10 turns or fewer, with no concentration exemption
  **(settled, conditions 13: "consumables keep the 10-turn cap")**.
- Potions never need concentration; a scroll's concentration spell makes
  the reader concentrate, still capped at 10 turns.
- Non-scroll and non-wand records pass the class copy registry (trinkets
  11), so no flask copies a class feature. **(lead's call)**

## 7. Effect shapes and banding

### 7.1 The banding rule

**(lead's call)**

- **Persistent records scale by the user:** healing through Recovery;
  every item effect's difficulty through `user` (proficiency rises with
  level, settled); wand dart count by band.
- **One-shot damage records have fixed dice:** scrolls by tier, bombs by
  record. An old one-shot loses relative power, a gentle push to use it.
- Banding is not counted as anti-hoarding **(settled, items 10.8)**.

### 7.2 Healing potion (M1)

**(lead's call)**

- Heals `FloorDiv(receiver's Recovery × potionPermille, 1000)`, with
  `potionPermille` 1000 *(illustr.)*.
- Recovery here excludes the rest-only Medicine bonus (ability model
  15.2). Recovery is a settled stat ("hit points restored per short rest;
  grows with level and Constitution", stat model 6) that M1 needs for
  short rests anyway, so the record shape is buildable in M1. Recovery is
  7 at level 1 with Constitution +1, so the potion matches SRD 2d4+2.
- **Recovery = 4 + 2 × level + Constitution modifier, minimum 1**
  (`recovery.base` 4, `recovery.perLevel` 2; the lead's numbers, owned by
  the death and healing design 8). It replaces `recoveryPlaceholder`,
  which is removed. **(change DH-39)**
- **The heal effect** is one effect kind, `healRecovery {permille,
  monsterPermille}`, whose fields name tuning keys. `monsterPermille` is
  required whenever the record is `usableBy.monster` (validation rule 13).
- **Monsters have no Recovery:** a monster that drinks heals
  `FloorDiv(maxHP × monsterPotionPermille, 1000)`, `monsterPotionPermille`
  250 *(illustr.)*.
- **One formula, in order (new lead's call):**
  1. base = `FloorDiv(Recovery × potionPermille, 1000)` (a monster:
     `FloorDiv(maxHP × monsterPotionPermille, 1000)`);
  2. heal = `FloorDiv(base × potencyPermille, 1000)`, where
     `potencyPermille` is the target entry's (1000 when drunk or
     Administered, `thrownPotionPermille` when thrown);
  3. heal = max(1, heal);
  4. healing blocks then apply (Withered and the like) and may make it 0.
- **Fallback** if Recovery turns out too lumpy: banded dice 2d4+2 / 4d4+4 /
  6d4+6 for everyone; the record and its peg check keep the same shape.
  **Trigger (simulator criterion):** at equal level, the largest potion
  heal across the launch classes and companion kits is more than twice the
  smallest *(illustr.)*, or a level-1 potion heals under 4 or over 10
  *(illustr.)*.

### 7.3 Tonic (M2)

The settled single record: bonus action on yourself = minor; action =
major; thrown at an ally or Administered = minor only; never hard or
lingering **(settled)**. Thrown range `throwAllyRange` 5 *(illustr.)*
(CN-3). Value 1.

### 7.4 Remedy (M2)

- Ends one non-curse lingering condition, Scarred included **(settled,
  conditions 14)**, the phial's `treatProof` Scarred included **(agreed,
  Q3)**.
- Weary ends in full: Remedy removes the condition, not one level.
  **(lead's call)**
- `NotInCombat`; used from the pack or the party view at a short rest,
  camp or between fights; **not beltable**. A `basePrice` record.
  **(lead's call)**
- Its real niche is mid-floor: a Scar or a Weary before that floor's boss,
  when the Treat roll failed or nobody is trained in Medicine (camp clears
  lingering conditions anyway).

### 7.5 Coatings (Later, with Coated; W-11)

**(lead's call)**

- Applied as a bonus action from the belt to the wielder's active weapon
  (2024 basic-poison precedent); a free interaction with Quivered,
  **launcher coatings only** (weapons 18.2: Quivered is a launcher
  property).
- Gives Coated (`charges:3`, fallback `turns:3`; the fallback blocks
  pre-coating). Tagged `blade` (edge oils) or `launcher` (special arrows
  and bolts).
- **Named rider only:** the weapon's hits attempt one named condition
  rider (Venom → Poisoned; Serrating → Bleeding, never on a slashing
  weapon, which already has it). No element shift (Edge and Ember Ring own
  it) and no own-type rider flag (Honed owns it). Zero added damage.
- A charge is spent only on a hit that attempts the coating's rider,
  inside ability model 9.3 (one rider per target per use; an at-will's
  rider once per turn; the "already has it" gate). This changes the
  approved Coated text ("its next 3 weapon hits"), so it is part of
  CN-18. A new coating replaces the old.
- Never both beltable and wieldable **(settled, items rule 6)**.

## 8. Stacks, belt and anti-hoarding

- **The belt** (2 slots, a feature or offer may add a 3rd, hard maximum
  3) is the in-combat limit **(settled)**. Every scroll, wand and talisman
  takes a whole slot.
- **Honest note (lead's call):** the belt is **not** a hoarding cap for
  scrolls. Belts rearrange freely out of combat (items 7.2), and the pack
  holds any number of scrolls. A party can carry its tier-3 and tier-4
  scrolls in packs all floor and belt them before the boss. The real
  bounds are the camp-spend flag (one scroll read or wand charge per unit
  per turn, and never beside a slot spell or Surge, 13.5), family caps (2
  per spell per run), at most 1 Very rare per floor, and the boss report
  (19.3) with a pre-agreed lever (section 21).
- **Anti-hoarding toolkit** (banding not counted): belt cap and
  `stackMax`; no stash **(agreed)**; limited potion stock per camp
  **(agreed)**; bonus-action tempo, so drinking in ordinary fights is
  obviously right; the companion scorer suggests "drink it" (items 10.8);
  automatic triggers for insurance; fixed-dice one-shots; scrolls outside
  the companion scorer's boss reserve (13.7); monster carriers spend what
  they carry ("what is used is gone", settled); the unused-at-camp report.

## 9. Value, prices and pegs

### 9.1 Use-value table

Items 12.6 makes it this design's; value = rarity budget exactly (items
12.5). **(lead's call)**

| Value (rarity) | Records |
|---|---|
| 1 (Common) | Healing potion, tonic, water flask, oil flask, Fire Bomb, coating, tier-1 scroll |
| 2 (Uncommon) | Greater Fire Bomb (floor 4+), tier-2 scroll, Uncommon wand |
| 3 (Rare) | Tier-3 scroll, Rare wand, Reprieve Phial |
| 4 (Very rare) | Tier-4 scroll |
| `basePrice` | Remedy (and supply) |

`rarity.*.props` ranges apply to gear only; a use-only record meets rule 1
with its `use` (CN-6).

**Where the value comes from (new lead's call; CN-6):** items 12.3 sums
"the values of the `use` ability". Each `ability.item.*` record therefore
declares its offer value, and the load-time check compares the item's
summed value with:

- a scroll: its tier (derived from the ability, 18.2);
- a wand: `wandValue(tier, pools[camp].max)` (13.6), keyed on the pool's
  `max`, never on the `charges` range;
- every other use-only record (potion, tonic, flask, coating, talisman):
  its rarity budget (Common 1, Uncommon 2, Rare 3, Very rare 4; items
  12.5);
- Remedy: excepted (`basePrice`).

### 9.2 Prices

Items 10.5 formula; `coinPerValue` 20 *(illustr.)*. **(lead's call)**

| Category | Permille | Coin per value | Prices *(illustr.)* |
|---|---|---|---|
| consumable | 600 **(settled)** | 12 | Potion, tonic, water, oil, Fire Bomb 12; Greater Fire Bomb 24 |
| scroll | 800 **(settled)** | 16 | 16 / 32 / 48 / 64 by tier |
| wand | 1000 **(settled)** | 20 | Uncommon 40; Rare 60 |
| talisman | 1500 (new, CN-2) | 30 | Base 90; Sell at 25% = 22 (Sell 250‰, M2; loot economy design 6.4; change LE-9). Never bought **(agreed, Q2)**; the permille sets only Sell and budget accounting |
| `basePrice` | — | — | Supply 8, Remedy 30 *(illustr.)* |

**The Wax Key (M2; traps and locks design 6.11; change TL-24):** a
run-scoped party counter `waxKeys` (cap 2, size 0, no carrier), not a
consumable record, so it has no use-value row in 9.1. Priced by camp
depth 24 / 28 / 32 / 36 coin *(illustr.)*, after the Leadership discount;
checked against pegs K1 and K2 (9.3).

### 9.3 Pegs, as validation inequalities

**(lead's call; CN-5)**

- **P1, healing.** Potion healing per coin stays at least 20% dearer than
  supply's, checked at the worst case, party size 1 (floor 1 is solo, and
  some runs decline recruits): `supply.basePrice × potionPermille ≤
  potionPrice × pegMarginPermille`, `pegMarginPermille` 800 *(illustr.)*
  (8 × 1000 ≤ 12 × 800). It is checked on camp prices **after** the
  Leadership discount and `minPrice`, so rounding cannot invert it. Under
  the Recovery-linked shape it then holds at every party size, and
  supply's rest pools, Slot Recovery and Medicine bonus only widen the
  gap. Healing scrolls are checked on the same terms (their heal equals
  the potion's).
- **P2, Remedy.** Remedy price > supply price: a rest also grants a free
  Treat attempt at Scarred and Diseased for every unit.
- **P3, tonic.** The tonic price is the condition-economy knob
  **(settled, conditions 22)**, tracked against tonics used per fight.
- **P4, damage.** No damage record is ever sold by the basic vendor; coin
  cannot buy unlimited damage. Bombs are sundries; scrolls and wands come
  from loot and the camp channel.
- **P5, Leadership.** Sell reads the undiscounted base, so no buy-and-sell
  loop **(settled)**.
- **Wand value covers its charges:** the `wandValue` lookup (13.6).
- **Thrown potions** heal less than drinking (`thrownPotionPermille` ≤
  1000), so they never widen P1. **(lead's call)**
- **K1 and K2, the Wax Key (M2; traps and locks design 6.11; change
  TL-24).** Checked per camp on the depth price after the Leadership
  discount: K1, Wax Key price ≥ 2 × healing potion price; K2, Wax Key
  price ≤ the mean value of a locked cache on the next floor.
  **K2 defined; K2b added (M2; loot economy design 6.4 item 9; change
  LE-12):** floor N+1 does not exist when the key is priced, so "the mean
  value of a locked cache on the next floor" is a rule-derived expected
  Wax-Key value per floor, computed from data over TL 6.20's expected lock
  mix (a consequence cache counted at its Wax-Key identity). K2 compares
  the **undiscounted** price with that value; **K2b**, the price after
  the 25% Leadership discount ≥ 50% of that value (the key is never a
  must-buy). Both checked statically for camps 1–4 (camp 4 against
  F5).

## 10. Channels and per-floor budgets

**(lead's call unless marked)**

| Channel | What |
|---|---|
| Basic vendor **(agreed)** | Supply; healing potions in pre-rolled stock of 2–3 *(illustr.)*; tonics and Remedy, unlimited (price is the knob); 1 Wax Key only when the party has no Mechanics-trained member (M2; items 10.3; traps and locks design 6.11; change TL-25, **agreed, Q3 of 2026-11-21**); 1 Ear Trumpet (a run-scoped lane-gap party counter, cap 1, not a consumable record; 15 coin *(illustr.)*) only when no party unit (the hero included) is Perception-trained on the camp-entry snapshot, stable for the visit (the TL-25 test); its effect is suspended while any living party unit is Perception-trained and lasts through the next floor; stocked only if the M2 report `SoloListenClickShare` misses (M3, **conditional**; items 10.3; exploration and information design 4.5.2; change XI-26, **agreed, Q3 of 2026-11-23**) |
| Sundries (new loot class, CN-19) | Common consumables: healing potion, tonic, water, oil; Fire Bomb from floor 2. Drawn per floor **outside the notable count**, inside the fixed budget (items 2), `sundriesPerFloor` doses set per floor *(illustr. 3–5 on floors 1–2, 4–5 on floors 3–5)*. **At least 1 healing potion per floor** (`healingSundryMin` 1, drawn first; 2 on floors 4–5 only through the pre-authorised raise) and **at most 3** (`healingSundryMax`) **(client, Q3)**: the guaranteed potion goes in a main-path container in the first half of the main path, openable with no check, and is never substituted; random potions lean off-path (`healingOffPathPermille` 600) (death and healing design 12.4, 12.5) **(change DH-40)**. From floor 2, **at least 1 and at most 2 offensive flasks** per floor (`offensiveSundryMin`, `offensiveSundryMax`), the minimum drawn before the other sundries **(lead's call)**. Mostly containers, plus the settled single carried consumable per encounter |
| Notables | Scrolls, wands, the Greater Fire Bomb and the talisman count in the 6–8 notables per floor and the camp channel; their share steered by category weights by depth and the F5 tactical cap, ranges unchanged (M2; loot economy design 6.2; change LE-17) |
| Camp channel (notables shop, camp reward card) | Scrolls (never healing scrolls, P1), wands, the Greater Fire Bomb, all with channel `camp` (shop and reward card); the talisman **only** through `campReward` (the reward card, never the shop; CN-1) |
| Boss reward card | Scrolls, wands; the talisman from floor 3 (a boss-channel phial fills option 2; item rolls design 4.1; change IR-8). Floors 1–4 only: no final-boss card; on floor 5 the Viaticum (choose 1 of 3 Rare wands and tier-3 scrolls, usable options, never the phial) takes the boss share (M2; loot economy design 6.11; change LE-5, **agreed, Q1 of 2026-11-22**). **Offer slots (M2; item rolls design 4.8, 4.10; change IR-8, agreed, Q1 of 2026-11-22):** options Yours, Theirs, Wayward and the fourth; families distinct, effect tags distinct where legal, at most 1 damage option; a hero guarantee. **Usability fallback (M2; loot economy design 6.3; change LE-3; item rolls design 4.8; change IR-8, agreed, Q1 of 2026-11-22):** each option with no usable record is redrawn **on its own** as usable gear at the same tier (usability filter, ordinary weights, not fit-filtered), on `bossCardFallback` entry = option index, then the offer terminal ladder. **Martial parties (item rolls design 4.8; change IR-8):** "the boss gives spells" holds only for readers; for a solo Vanguard or a Herald without Arcana, boss cards and the Viaticum resolve mostly to gear. **Pending request (M3; item rolls design 4.8, 9.4; change IR-8; consumables owner):** a martial tactical boss-card pool (notable bombs and oils, arena tools; 2–3 *(illustr.)*), pulled forward from Later; until ruled, the pool is unchanged |
| Carried | Healing potion, tonic (any floor from M2; floor 1 self-targeting only, settled); flasks and scrolls floor 2+; wands floor 2+; never the talisman, Remedy or coatings |
| Substitution fallback | Supply or tonic **(settled, enemies 15.3)** |
| Trader (Later) | Never healing potions (settled); never the talisman |

- **M1:** no carried consumables; M1's carried split is at most one weapon
  or armour notable **(lead's call, enemies 15.3)**. Potions come from the
  kit and containers.
- **Family caps:** consumable `null` (settled); scroll 2 and wand 2, where
  one family = one spell **(settled values)**; talisman 1 (CN-2). Wand
  spells and scroll spells are disjoint, so no family duplicates another
  in play.
- **The phial is never a weighted draw.** It is placed by one pre-roll
  per run (14.7), which replaces one notable draw of the chosen floor and
  channel, so the fixed budget never has a hole. No slot is reserved on
  any floor. **(change DH-40)**
- **The Wax Key (M2; traps and locks design 6.11; change TL-24):** a
  run-scoped party counter, not a consumable record (9.2). Channels:
  sundries from floor 2 (at most 1 per floor, outside the
  `sundriesPerFloor` doses; a reading, loot economy design 6.2, change
  LE-17), one camp notables slot
  (one Wax Key at most), and the basic vendor's stock of 1 when the
  party has no Mechanics-trained member (TL-25). Out of combat only; at
  the cap of 2 a dropped key stays in its container.

## 11. Delivering potions and tonics

**(settled where marked; the throw agreed, Q4; the rest lead's call)**

| Method | Cost | Strength | Niche |
|---|---|---|---|
| Drink | Own bonus action **(settled)** | Full | The default |
| Hand over | Giver's free interaction, then the receiver's bonus action **(settled)** | Full | An adjacent ally who can act |
| Administer | Giver's action, adjacent **(settled)** | Full potion; tonic minor | An adjacent ally who cannot act or has committed its bonus action |
| Throw **(agreed, Q4)** | Giver's action, range 5, line of sight, no roll, no splash, willing ally only, never yourself | Potion `thrownPotionPermille` 750 (the 7.2 formula: at least 1, then healing blocks); tonic minor **(settled)** | The same receiver, when reaching it costs a move, an opportunity attack or a hazard |

- Neither throw nor Administer lifts Downed **(settled for Administer;
  agreed, Q4, for the throw)**; the preview says so.
- **Willing ally (new lead's call):** a unit on the thrower's side, read
  from the same side test as `selective` (ability model 8.2); a unit under
  a condition that makes it act for the enemy is not a legal target. No
  roll and no save either way.
- **Downed targets (new lead's call; a narrowing of the settled
  Administer row, CN-3, CN-12):** a Downed unit is not a legal target for
  a thrown or Administered potion or a healing scroll; the targeting
  cursor shows the reason (ability model 15.4: target-dependent reasons on
  the cursor), so a dose is never spent on a heal that cannot get the unit
  up. An Administered or thrown **tonic** stays legal on a Downed ally
  (minor only): its harmful conditions still matter when it gets up.
- **Combat only (new lead's call):** the throw is a combat target mode.
  Out of combat, Hand over is free at any distance through the party view
  (items 7.1), so the throw is not offered there.
- **Companion suggester:** proposes a throw only when the receiver cannot
  act or has committed its bonus action and the heal changes its drop odds
  this round; otherwise Hand over or Administer. No friendly-fire term.
- **Monsters:** potion use stays self-only in M2; a monster ally throw is
  a Later option, reported first.
- **Slinger's Bracers** (Later) stays the bonus-action step, once per
  fight, extended to potions (CN-22).
- **Later:** "a thrown beneficial dose ends Asleep" as a stated end
  trigger, shipped with Asleep: the one good side effect glass damage
  would have had, without the harm.
- **The knob (new lead's call):** `thrownPotionPermille` is tuning inside
  the client-approved range 500–1000. Moving it outside the range, or
  adding any damage to a beneficial throw, needs the client. Reaching 1000
  triggers the merge into one "Give" button (5.3), which is the lead's
  change to make, recorded in the reports.

## 12. Thrown flasks and terrain

**(lead's call)**

- **Cost and range:** the thrower's action; range `flaskRange` 5
  *(illustr.; the dagger's thrown range, weapons 9.1)*; line of sight;
  previewed by the core (ability model 8.3). Each target entry declares
  its `side` and `area` (18.2): Fire Bomb `side: any`, `area: burst:1`;
  oil `side: enemy`, `area: unit`; water one `enemy` entry and one `ally`
  entry, both `area: unit`.
- **No attack roll:** a save against the thrower's ability difficulty
  (`user`). Not a weapon attack, so weapons 9.2's adjacent-hostile
  disadvantage does not apply, as for spells. Any member throws with its
  own difficulty, which already rises with proficiency.
- **At most one offensive flask per unit per turn;** greyed
  `FlaskThisTurn` (CN-9). **Offensive is derived, never authored (new
  lead's call):** a flask is offensive if any of its harmful target
  entries (18.2) has a damage part or forced movement. So the Fire Bomb,
  Greater Fire Bomb and the Later Holy Water and Thunderstone are
  offensive; water and oil (setup flasks) are not. The unit's
  `offensiveFlaskUsed` is set at the cost-paid step (ability model 14.3
  step 3), resets with the unit's own turn budget (conditions 5.4
  turn-start step 1) and every tick out of combat, and binds monsters as
  well.
- **Areas:** single unit or burst r1; never `selective` (friendly fire,
  ability model 8.2).
- **Never terrain:** flasks act on units only. Existing terrain rules
  apply untouched: a fire area burns away oil squares (conditions 11.4).
  **(change TR-9, clarification; nothing changed)** A fire flask's area
  that includes a Keg's square bursts it once (M2: Carnival, Gut); the
  burst is the Keg's own one-shot area, source `environment`, and leaves
  nothing on the ground, so the flask still makes no terrain (room
  terrain design 6.3.5).
- **Conditions:** minor or setup only in M2; never hard. One named major
  flask (the net) is Later under the conditions 7.2 guards.
- **At an ally:** only water among flasks; automatic, no roll, no splash.
- **Trapwright (pending):** Toss, Dust and the ruling against Slinger's
  Bracers (SH-25) apply when the Trapwright ships (Shade design 8.2, 14).

**Water on a Burning ally (honest):** a thrown tonic already ends Burning
(a minor) for the same action and range, with no Soaked weakness. Water's
design job is **enemy setup** (Soaked, then cold or lightning; arcs on
lightning). On an ally it is a fallback that saves a tonic and also ends
Smouldering and overrides Corroded in one go, at the price of cold and
lightning weakness.

## 13. Scrolls and wands

**(agreed, Q1, for the in-class and out-of-class rules; the rest lead's
call unless marked)**

### 13.1 In class or out of class

An engine rule keyed on category `scroll` or `wand`, never per-record data.

- **In class:** the reader's class is in the record's `classTags[]`. For
  the hero, the hero class; for a companion, its class (ability model
  11.1: a companion has a class). **(agreed, Q1)**
- `classTags[]` = every launch class whose spell list holds the spell.
  Validation resolves them to real class ids: the launch ids `vanguard`,
  `shade`, `arcanist`, `herald` *(ids illustr.)*. The
  coverage-or-cut audit maps every M2 scroll and wand to the Arcanist or
  Herald list or moves it to Later (classes design 14). The tradition
  notes in section 16 are authoring hints, not data; no tradition tags in
  the schema; [primal] stays an authoring hint until the Rootwarden.
  **(change CL-12a)**
- In class covers **any spell on the class list, known or not** (ability
  model 6.5: about 7 known by level 10): a scroll is a spell you do not
  know, for one cast. **(agreed, Q1)**
- **No "has reached that tier" test:** the floor cap (13.4) tracks a full
  caster's tiers at floor entry, every launch caster is a full caster
  (ability model 6.5), and the test would misfire on companion kits. The
  simulator reports reads above the reader's class tier.
- **Out of class:** everyone else. Non-casters are always out of class.

### 13.2 Costs

| Read | Cost | Check | Failure |
|---|---|---|---|
| In class | The spell's own declared cost (action or bonus; never reaction) **(agreed, Q1)** | None | — |
| Out of class, trained in Arcana | Action, whatever the spell's cost **(settled)** | d20 + Arcana against `scrollArcana` = 10 + tier (11/12/13/14; a client number, not tuning) **(agreed, Q1)** | The scroll (or the wand charge) is used up; no free retry **(settled)** |
| Out of class, untrained | Greyed `Untrained` (ability model 15.4 pattern) **(agreed, Q1)** | — | — |

- **No slot is spent** on any read; a scroll casts at its own tier and is
  **never upcast** (CN-3). **(agreed, Q1)**
- **Odds** *(illustr.)*: a trained non-caster with Intelligence +0
  succeeds about 50–60% at each tier's entry floor; an Intelligence caster
  reading another class's scroll about 75–85%; with Arcana mastery about
  85%+. 10 + 2 × tier was rejected: the non-caster drops to about 30% at
  tier 4, so nobody would read a 64-coin scroll out of class.
- **Always a roll at every tier** **(agreed, Q1)**, so Scrollwright's Token
  (trinkets 13.7, Later) keeps its job. The preview shows the odds.
- Builder idea, not a rule: Arcana mastery's signature use could make
  out-of-class reads a threshold with no roll. If adopted, it comes back
  to the client, since Q1 chose "always rolled". **(new lead's call)**

### 13.3 Difficulty and attacks

- `use.difficulty: user`, the existing default (items 12.3). Ability
  difficulty is 8 + proficiency + the class's main ability **(agreed, stat
  model 6)**, one formula for every class, so a fighter's and a wizard's
  scroll hit equally hard at equal level, and the Arcana check is the only
  reader difference: a non-caster is not taxed twice.
- **Item spell attacks** use d20 + the user's main ability + proficiency
  for every reader, mirroring ability difficulty.
- Monster carriers use their own difficulty and attack.
- `fixed:N` stays legal in the schema for records that need it; no M2
  scroll, wand or flask uses it.

### 13.4 Power by floor and rarity

- `scrollFloorCap` by floor = [1, 2, 3, 4, 4] *(illustr.)*: a full
  caster's tiers at floor entry (ability model 10.1: tier 2 at level 3, 3
  at 5, 4 at 7). The array is read at index floor − 1 (floors count from
  1), and its length equals the floor count (validation rule 8).
- **Rarity = tier = value:** tier 1 Common (floor 1+); tier 2 Uncommon
  (floor 2+ by the cap); tier 3 Rare (floor 3+); tier 4 Very rare (floor
  4+, at most 1 per floor). This reuses items 12.5's budgets and depths
  exactly. `minFloor` = max(rarity depth, first floor whose cap allows the
  tier).

### 13.5 The nova guard

**(lead's recommendation; CN-11 changes an agreed row and was approved by
the client on 2026-11-17, 25.2)**

- **A scroll read** (in class or out of class, including a failed
  out-of-class attempt) **uses the unit's camp spend** for that turn.
- **A wand charge already does, whatever CN-11's fate:** a wand's pool is
  `camp`, and "an item's `camp` spends count toward the
  one-camp-spend-per-turn rule" **(settled, items 12.8; ability model
  6.4)**. Nothing in this section touches wands.
- **Where it runs (ability model 14.3):** at step 2 (validated), a scroll
  read is greyed with the ability model's existing camp-spend reason when
  the unit's flag is already set; at step 3 (cost paid), the read sets the
  flag, before any Arcana roll, so a failed read sets it too.
- **What it does:** the out-of-slots caster loses nothing; a caster cannot
  cast a slot spell and read a bonus-action scroll in one turn; and since
  Surge is itself a `camp` spend (ability model 6.6), a Surge turn cannot
  also read a scroll or use a wand charge, in either order.
- Ability model 6.4's agreed sentence is about `camp` pools, and a
  scroll's pool is `never`, so this is written as an explicit engine rule,
  not a pool rule, or builders would miss scrolls.
- **Build so either answer costs no rework (new lead's call):** the rule
  is one engine predicate, `scrollReadSetsCampSpend`, read from the
  `consumables` block (18.1) and **not tuning**. It governs scroll reads
  only. It is `true`: the client approved CN-11 on 2026-11-17. Had CN-11
  been declined it would be `false` (scroll reads neither check nor set
  the flag, wand charges still do, and the fallback bounds of 25.2 would
  apply); those fallback bounds are recorded only and are **not in
  force**.

### 13.6 Wands

- One spell, **tier 1–2 only**; a renewable tier 3–4 effect would be a
  slot battery. Never wielded, never a focus **(settled, weapons 14)**.
- `max` charges fixed on the record; starting charges pre-rolled
  `{min, max}` **(settled, items 12.8)**; `camp` refill **(settled)**;
  vendor and shop wands start full **(settled)**.
- **Value by lookup** (`wandValue`, keyed on tier and the `camp` pool's
  `max`, so every roll gives the same value): (tier 1, 2 charges) = 2,
  Uncommon, floor 2+; (tier 1, 3) = 3, Rare, floor 3+; (tier 2, 2) = 3,
  Rare, floor 3+. No Common wands. First floor 2, so two tier-1 charges
  never double a level-1 caster's slots. The pre-roll range obeys 1 ≤
  `charges.min` ≤ `charges.max` ≤ the pool's `max`.
- In class: the spell's cost per charge. Out of class: every charge needs
  its own action + Arcana (10 + tier); a failure spends the charge
  **(settled)**.
- A wand charge is a `camp` spend **(settled, items 12.8)**.
- The trader's Recharge stays the one named exception (Later, I-16).

### 13.7 Which spells, and edge cases

- **M2:** class-list spells of the four launch classes only. No
  scroll-only list (option C is a Later hook).
- **Never on a scroll or wand** (validation): cantrips; reaction-cost
  spells; extra actions, turns or attacks; slot, pool or Slot Recovery
  restore; knowledge or identification; capacity; revive, Death-warded or
  Downed-lifting spells; area or splash healing; any duration over 10
  turns (authors write a short variant); ground zones (already banned,
  ability model 8.4); teleport, until the ability model defines it
  (ability model 8, Targeting and areas, has no teleport rule yet).
  **Healing on wands** is never allowed (a camp-refilled heal is free
  healing).
- **Allowed:** concentration spells (the reader concentrates, ending any
  earlier concentration; d20 + Constitution works for anyone); major and
  hard conditions under every conditions 7.2 guard and the monster depth
  gates.
- **No learning** spells from scrolls. Later idea: a scroll adds its spell
  to the camp-swap pool.
- **Silenced** (Later) blocks reads. **Hand over** (free, belt) moves a
  scroll to the right reader. A healing scroll on a Downed companion is
  not a legal target (section 11).
- **Suggester and preview:** in-class reader first, then the
  Arcana-trained member with the best odds; never an untrained read; the
  preview shows in or out of class, the odds and the result; the area
  scorer's friendly-fire term applies to every item read, in or out of
  class.
- **No boss reserve for item spells (new lead's call):** the party
  scorer's limited cost holds a reserve back for the boss (ability model
  13). Item spells (`never` pools: scrolls) are excluded from that reserve
  and use a fixed low limited cost (`itemSpellThrift` 50 *(illustr.)*, a
  percentage of the best at-will score), so companions spend scrolls in
  ordinary fights. Reported (19.3).

### 13.8 Making the non-caster route real (binding dependency)

The agreed Q1 answer's non-caster case needs Arcana-trained non-casters.
Recorded as dependency **DEP-1**, binding on the classes and companion
designs:

- the hero can always gain Arcana through skill offers (settled sources:
  "offers and the starting boon add more", stat model 7.2);
- at least one recruitable non-caster companion per floor's recruit pool
  is trained in Arcana *(illustr.)*; read as met by a Muster board card (a
  non-caster) **or** any party member trained in Arcana, the hero
  included (the party can read any scroll) (companions design 7.3)
  **(change CMP-12)**;
- at least one launch non-caster class has Arcana on its skill list.

**Revisit trigger:** if non-caster out-of-class reads are under about 5%
of reads *(illustr.)*, the lead brings option C or the Arcana-mastery
threshold back to the client. D′ was rejected by the client and returns
only if the client asks for it. **(new lead's call)**

## 14. The Reprieve Phial (talisman)

**(agreed, Q2, Q3, for the principle and the cost; details lead's call
unless marked; final numbers and owner order set by the death and
healing design)**

Name checked against every approved document for clashes (none); the
protected-name check still runs before data.

### 14.1 Record

- Category `talisman` (CN-1); `beltable`; `stackMax` 1; `unique` (at most
  1 owned by the party); always identified (items 11).
- `use: {targets: [{mode: self, cost: none}], trigger: wouldDropToZero}`;
  `usableBy.monster: false`. A `trigger` use is always automatic; there is
  no policy field.
- Its stack pool is derived from the category (`stackMax.talisman` 1,
  refill `never`), as items 12.8 derives a consumable's from its `kind`;
  the record declares no `pools`.
- Rare, value 3, `minFloor` 3 **(agreed, Q2)**.

### 14.2 Trigger

- It fires when the holder's hit points would go from above 0 to 0, from
  any source: a hit, an area, a harmful tick, a hazard, a trap, friendly
  fire. It is checked **where damage lands**, so the zero-hit-point check
  (conditions 5.4 step 4) never sees 0.
- Only from the holder's own belt **(agreed, Q2)**. A phial in the pack
  does nothing.
- Never on a unit already Downed; it never lifts Downed.
- **In and out of combat** (the floors of death and healing 10.4 run
  first, so it never fires on floored damage). This is a recorded
  difference from Death-warded's `combatOnly`; the out-of-combat question
  is closed (death and healing design 16). **(change DH-41)**

### 14.3 Resolution order

**(agreed, Q2, for the order; CN-15; final, death and healing Q7)** It is
spent only if nothing free saved the unit:

1. a Death-warded token at the drop;
2. the phial.

The order is final: Death-warded → phial; Last Ward is retired (death and
healing design 15.2, 15.3). **(change DH-41)**

### 14.4 Where the check runs, and the effect

**Where the check runs (new lead's call; CN-15).** A damage event is as
conditions 9.3 defines it: one ability use against one target, one
turn-start tick batch, or one other separate source. Each Sparks dart,
each Scorching Rays hit and Frost Shard's burst are separate events.

1. **Total.** The event's total damage is computed after every per-part
   step of conditions 9.3 (ladder shifts, shells, temporary HP), summed
   over all its parts in data order.
2. **Save chain.** If the unit is not Downed and current HP minus the
   total is 0 or less, the chain runs in order, and the first link that
   fires ends it:
   - **Death-warded:** HP is left on 1;
   - **the phial:** steps 3 to 5.
3. **Scarred, synchronously.** The holder gains **Scarred** as an engine
   grant applied at once, bypassing the step 9 queue of conditions 9.2,
   so the new maximum exists before HP is set. It always lands, ignores
   `downedThisFloor`, does **not** set it, and stays exempt from the
   lingering cap. This Scarred instance carries the flag `treatProof`, so
   only a Remedy or camp ends it, never Treat **(agreed, Q3)**. If the
   holder is already Scarred, the existing instance gains the flag.
4. **HP** is set to `max(1, FloorDiv(new maximum HP × leaveHpPermille,
   1000))`, `leaveHpPermille` 334 (a client number, death and healing
   Q8; death and healing design 16) **(change DH-41)**, and the rest of
   the event's damage, every part of it, is discarded. A share of maximum
   HP does not depend on Recovery and has no monster or companion edge
   cases. It
   differs from Death-warded's "on 1" on purpose: under the 6.4 guards one
   enemy turn may deal 25% expected and 40% maximum of hero HP, and the
   save usually fires with more enemies to act, so 1 HP mostly delays
   death.
5. **The item is destroyed** (`ItemTriggered`, then
   `ItemDestroyed{usedUp}`).
6. **Then the event goes on:** the concentration check uses the
   **pre-save total** (ability model 9.5: half the damage taken); Asleep
   ends; on-damage triggers fire; riders apply, since the unit stands.
   Later hits of a multiattack are new events and resolve normally.

Replays rebuild all of this from the 18.4 events; tests cover a two-part
hit and a tick batch (19.2).

**Later downings (new lead's call):** Scarred is one binary instance. A
companion saved by the phial and later downed on the same floor is
already Scarred; its first-downing grant changes nothing and the
`treatProof` flag stays. `downedThisFloor` is set by that real downing as
usual.

### 14.5 Placement

**(lead's call, a balance fix)** The phial changes place **only out of
combat**. In combat it cannot be handed over, taken from a Downed ally or
Retrieved (greyed `NotInCombat`). Otherwise a free Hand over would pass
one phial to whoever is lowest each round. This narrows the approved Hand
over, Take from a Downed ally and Retrieve rows (CN-3, CN-12).

### 14.6 Why `treatProof` Scarred (Q3)

**(agreed, Q3)** Standard Scarred is removed by Treat at the next short
rest, so a party with Medicine would pay one Treat roll for an extra life.
`treatProof` Scarred costs about one floor of −10% maximum HP or a 30-coin
Remedy. It is an instance flag on Scarred, not a new condition
(conditions 18.1 holds M1 + M2 at exactly 32).

- **At a rest (new lead's call):** a `treatProof` instance is not offered
  for Treat on the rest screen and uses up no Treat attempt (conditions
  18.10: one per Scarred unit per short rest). A Remedy or camp ends the
  instance and its flag together.
- Rejected: "No Scarred" (a free life); standard Scarred (one Treat roll
  buys a life); max HP loss for the run (denylist; "Scarred is the
  client's cost").
- **The Scarred hero and the guards (new lead's call):** the enemies 6.4
  guards are not re-run against a Scarred hero, as they are not for a
  Scarred companion today. The "same-fight deaths after a save" report
  (19.3) watches the risk instead.

### 14.7 Channels and scarcity

**(agreed, Q2, for Rare, floor 3+, 1 per run, unique, never bought;
channels and placement lead's call; CN-1, CN-2, CN-27)**

- Containers (floor 3+), the boss reward card (floors 3–4: no final-boss
  card; M2; loot economy design 6.11; change LE-5, **agreed, Q1 of
  2026-11-22**), the camp reward card (channel `campReward`, CN-1).
- Never the basic vendor, the notables shop, the trader, a carrier, a
  starting boon or a meta reward. `familyCapPerRun` 1.
- **Placement: one pre-roll per run (new lead's call; CN-27).** "1 per
  run" is a cap, not a promise **(confirmed by the client, 2026-11-17)**.
  At run generation, on the `loot/run` key, a `vector` of length 3
  always drawn (index 0 offer, 1 floor, 2 channel; indices 1 and 2 are
  drawn even when the offer fails, so the count never depends on the
  outcome; a floor retry never moves it; the outcome is unchanged)
  **(change SV-5, lead's call; approved by the client, 2026-11-23; M1
  key, drawn with the phial, 14.10; seeds and validation design 4.3,
  11)**. **Pending request (segments 11–12, to this design's owner):**
  "phial always 3 draws" is not settled until this owner confirms it; if
  refused, `loot/run` becomes per-entry with 1 or 3 draws on its own
  key and nothing else moves (seeds and validation design 14.2):
  1. roll `talisman.offerChancePermille` *(illustr. 500)*; on a failure
     the run has no phial;
  2. otherwise roll its floor, uniform over `talisman.minFloor` to the
     last floor *(illustr.)*, and its channel by
     `talisman.channelWeights` *(illustr. container 2, boss 1,
     campReward 1)*, leaving out a channel the floor does not offer (the
     last floor has no camp after it and no boss reward card; LE-5);
  3. on that floor, one notable draw of that channel is replaced by the
     phial. For `campReward`, one of that floor's container draws moves to
     the camp reward card as the phial, so the notables shop's stock is
     untouched and the floor's count still balances. Which card the phial
     takes, and its Ledger A count, are set in loot economy design 6.13
     (a boss-channel phial fills the F3–F4 boss card's option 2; item
     rolls design 4.1);
     a camp's phial `minFloor` reads the generated floor N, not N+1 (a
     reading; loot economy design 6.3; change LE-6).
- **A declined offer is lost for the run:** a reward card is a choice, and
  so is choosing the notables shop over the reward card. A returning offer
  would make the phial near-certain. A phial in a container stays there
  until picked up, like any loot.
- `offerChancePermille` is the **first lever** when the talisman reports
  fire (19.3), before `leaveHpPermille`.
- Why floor 3+: late deaths cost the player the most time; floor-1
  fragility belongs to the death and healing design's level-1 HP rule.

### 14.8 Readability and AI

- An armed glow on the belt and a plate token ("Reprieve ready"); the
  damage preview shows "lethal → Reprieve fires" and the resulting HP.
- **The area preview flags in red "will fire Reprieve on X"** for the
  party's own areas, and the suggester never proposes such a throw.
- A distinct trigger animation, a log line, a first-time tooltip. A save
  is never silent.
- **AI:** the monster scorer reads the preview, so a hit on the holder
  loses its kill bonus. Because the holder is in practice the hero, the
  scorer gains a term that values "spends a Rare save" at about the kill
  bonus *(illustr.)*, extended to Death-warded (death and healing design
  16) **(change DH-41)**, so targeting does not shift away from the hero.
  Nothing reads "is the hero" (fairness invariant 1 holds). The report
  "share of monster attacks on the holder, with and without a phial"
  checks it.

### 14.9 Guards and the death-ward family

- **Guards:** the enemies 6.4 permadeath guards are computed **without**
  any party consumable or talisman; the phial only adds survival on top of
  guarded fights.
- **Death-ward family:** Recommendation superseded: Last-Breath Charm
  retired as recommended, and Last Ward also retired (death and healing
  design 15.3, Q7). The cap reads: at most one **recharging** drop
  prevention (today Death-warded) per unit per fight; **the phial is
  outside it** and always fires when armed (agreed Q2: it comes after the
  free sources in the order). Any cap that included the phial would change
  Q2 and go back to the client. **(change DH-41)**

### 14.10 Gating and staging

**(agreed, Q2: principle now, ships with death and healing)**

- The principle is approved with this document; the per-run pre-roll
  (14.7) places the phial, and the save-chain hook (14.4) exists in the
  damage pipeline.
- The death and healing design sets `leaveHpPermille` 334, the owner
  order Death-warded → phial, and that the phial fires from any source,
  in and out of combat (death and healing design 15, 16).
- Ships in M2 if the death and healing design is approved before M2
  content lock; otherwise Later, with Death-warded. Not in M1. **(change
  DH-41)**

## 15. Monster use

**(agreed rows of enemies 15.4 stand in substance; CN-20 refines their
wording and was approved by the client on 2026-11-17, 25.2; the rest
lead's call)**

| Item | Carrier use | `useWhen` |
|---|---|---|
| Healing potion | `uses:1`, bonus action, self, heals `monsterPotionPermille` of max HP (7.2) | `selfHpAtOrBelowPermille{500}` **(settled: at or below 50% HP)** |
| Tonic | `uses:1`, self, the settled self costs: drunk as an action it ends its worst condition | `selfHoldsTier{major}` **(settled: holds a major)** |
| Fire Bomb (floor 2+) | `uses:1` thrown area, opener-flagged | `minTargets{2}`, scored by the area scorer with its friendly-fire term |
| Oil flask (floor 2+) | `uses:1` thrown, single party unit | `groupHasDamageType{fire}` |
| Water flask (floor 2+) | `uses:1` thrown, single party unit (its `enemy` entry only; a monster ally throw is Later, 11) | `groupHasDamageTypeAny{cold, lightning}` |
| Wand | `uses:N` = remaining charges, with thrift **(settled)** | `always` |
| Scroll | `uses:1`, caster-tagged carriers only **(settled)** | `always` |

**`useWhen` is a closed vocabulary (new lead's call; 18.2):**

| Value | True when |
|---|---|
| `always` | The scorer decides alone ("Normal" in enemies 15.4) |
| `selfHpAtOrBelowPermille{n}` | hp × 1000 ≤ maxHp × n |
| `selfHoldsTier{tier}` | The unit holds a harmful condition of that tier the item can end (not `cureImmune`) |
| `minTargets{n}` | The area's best placement covers at least n enemies of the carrier |
| `groupHasDamageType{type}` | A unit of the carrier's encounter group, the carrier included, has an ability that deals that type |
| `groupHasDamageTypeAny{types}` | As above, for any of the types |

- `useWhen` only gates the use. **Targets** are chosen by the scorer, as
  for any ability (ability model 13); the AI never rolls dice.
- **The tonic's "worst condition"** is the highest tier it can end, then
  the lowest `seq` (conditions 5.4).
- **In class on a monster:** a monster carrier always reads as in class:
  the spell's declared cost, no Arcana check, never upcast. Rival-party
  members (enemies 18) have a class and use the party test (13.1) on their
  class id.
- **The camp-spend mark:** a carried wand's `uses:N` keeps the camp-spend
  mark, as rivals' converted `camp` pools do (enemies 18.1), so it obeys
  the one-camp-spend-per-turn rule. A carried scroll takes the mark while
  `scrollReadSetsCampSpend` is true.
- **Monster healing scrolls:** none. Healing scrolls are
  `usableBy.monster: false` (16.4).
- **Every carried item that deals damage** (bomb, damaging flask, scroll,
  wand) must pass the enemies 6.4 guards on the reference carrier at the
  carrier's earliest room level, or it is not carried there (19.1 rules 7,
  8 and 9). The Greater Fire Bomb is **never carried in M2**.
  **(new lead's call)**
- Difficulty: the monster's own ability difficulty (`user`); every monster
  has one.
- Never carried: Remedy, the talisman, coatings (until Later), Rime Lance
  in M2, the Greater Fire Bomb in M2.
- **Risk:** items 19.1 rule 7 (carrier power ÷ price within 90–110%) may
  not land for a `uses:1` item at k = 1. The simulator reports it; a
  sundry exemption is the lead's fallback.
- **Had CN-20 been declined (recorded only; CN-20 was approved on
  2026-11-17, so this does not apply):** oil and water would not be
  carried (the agreed row says "thrown area") and would stay party loot
  only. The potion and scroll rows would stay as written, with
  `monsterPotionPermille` still the engine's number for the potion, since
  monsters have no Recovery. The guard check on carried damaging items is
  this design's validation (19.1) and applies in either case.

## 16. Catalogue

One roster, one ruling per entry. Numbers *(illustr.)*. "User difficulty"
means the user's ability difficulty (13.3). `classTags` resolve to the
launch ids `vanguard`, `shade`, `arcanist`, `herald` *(ids illustr.)*;
the coverage-or-cut audit maps every M2 scroll and wand here to the
Arcanist or Herald list or moves it to Later (classes design 14). The
tradition in brackets is an authoring hint only; [primal] stays one until
the Rootwarden. **(lead's call; change CL-12a)**

### 16.1 Potions, tonic, Remedy

| Record | Rarity, value | Use | Effect | Channels | Wave |
|---|---|---|---|---|---|
| **Healing Potion** | C, 1; floor 1+ | Bonus action (own belt); action to Administer or throw | Heals the receiver's Recovery; thrown at 750; monsters heal 25% max HP; never lifts Downed | 12 coin; vendor stock 2–3; kit; containers; sundries; carriers from M2 | **M1** |
| **Healing potion (camp issue)** | Flag `unsellable`; stacks only with itself | As the Healing Potion | As the Healing Potion | Camp top-up only (death and healing design 13.2) **(change DH-40)** | M2 |
| **Tonic** | C, 1 | Bonus action minor / action major / thrown (range 5) or Administered minor | Settled shape; `cureImmune` respected; also ends Soaked and Oiled **(settled)** | 12 coin; vendor unlimited; sundries; substitution | M2 |
| **Remedy** | `basePrice` 30 | Out of combat, from the pack | Ends one non-curse lingering condition (Scarred, including `treatProof`; Weary in full) | Vendor only; never loot or carried | M2 |
| Antitoxin | C, 1 | Bonus action | Inured, up to 10 turns | Loot | Later (Inured) |
| Resistance Draught | C, 1 | Bonus action | Resisting (one named type, one held at a time), up to 10 turns | Loot | Later (Resisting) |
| Stoneskin Draught | C, 1 | Bonus action | Shielded, 1 charge | Loot | Later (Shielded) |

### 16.2 Flasks

| Record | Rarity, value | Effect | Best / worst | Wave |
|---|---|---|---|---|
| **Water Flask** | C, 1; floor 1+ | Single unit within 5. Enemy: Reflex save or Soaked. Willing ally: automatic (cancels Burning and Smouldering, overrides Corroded; leaves cold and lightning weakness). No square | Best: setting up a cold or lightning caster on a packed group. Worst: fire-themed enemies (it gives fire resistance) | M2 |
| **Oil Flask** | C, 1; floor 1+ | Single unit within 5: Reflex save or Oiled (the next fire hit takes +half and applies Burning one band higher). No square. **Oil the Works (M3; traps and locks design 6.11; change TL-26):** an oil flask can instead be spent on one rolled lock or trap attempt for advantage; a failed roll makes no noise | Best: a boss or brute before a fire spell or bomb the same round. Worst: parties with no fire source. Watched as a possible dead slot (19.3) | M2 (Oil the Works M3) |
| **Fire Bomb** | C, 1; floor 2+ | Square within 5: burst r1, not selective, fire 2d6, Reflex half; fire part rider-flagged (Burning); pays out Oiled; ends Soaked; burns oil squares (conditions 11.4) | Best: a non-caster's area answer to a clustered swarm. Worst: melee scrums with allies adjacent | M2 |
| **Greater Fire Bomb** | U, 2; floor 4+ | As Fire Bomb, fire 4d6 | Best: late packs. Worst: as above | M2 |
| Holy Water | C, 1; floor 2+ | Single unit: radiant 2d6, Reflex half; radiant-weak targets get the automatic rider (Blinded or Revealed) | Needs the named-major-flask ruling | Later |
| Sticky Flask | C, 1; floor 2+ | Single unit: Reflex save or Mired, 2 turns | Waits for Mired | Later |
| Weighted Net | C, 1; floor 3+ | Medium or smaller unit within 4: Reflex save or Restrained (major; breaker slashing; Escape; a tonic drunk as an action); the one named major flask, under conditions 7.2 | Best: stopping one melee threat for a round | Later |
| Thunderstone | C, 1; floor 2+ | Burst r1, not selective: Fortitude save or pushed 1 away from centre; no damage | Best: breaking a surround near a hazard | Later |

### 16.3 Coatings (Later, with Coated)

Each coating has a `blade` record (edge oil) and a `launcher` record
(arrows or bolts). C, value 1, floor 2+, `stackMax` 2, loot only.

| Record | Rider | Notes |
|---|---|---|
| Venom Oil / Venom Arrows | Poisoned (named rider) | Weapon keeps its damage type. Worst: undead, constructs |
| Serrating Oil / Serrated Arrows | Bleeding (named rider) | Never on slashing weapons (validation). Worst: bloodless enemies |

### 16.4 Scrolls (M2)

All: in class = the spell's cost, slot-free, sets the camp-spend flag
while `scrollReadSetsCampSpend` is true (13.5, CN-11 client-approved 2026-11-17); out of
class = action + trained Arcana against 10 + tier (11/12/13/14), about
50–60% for a trained non-caster with Intelligence +0; a failure burns
it. Prices 16 / 32 / 48 / 64. Channels: containers,
caster-tagged carriers (floor 2+), boss, camp channel (healing scrolls
excepted). Effects use user difficulty and the item spell attack.

| Scroll (shape) | Tier, rarity | Effect | Best / worst |
|---|---|---|---|
| **Fan of Cinders** (Burning Hands) [arcane] | 1, C | Cone L2: fire 3d6, Reflex half, rider-flagged (Burning); not selective; pays out Oiled | Best: a bunched melee pack, Oiled targets. Worst: allies in front |
| **Shockwave** (Thunderwave; our types have no thunder) [arcane, primal] | 1, C | Burst r1 on the reader, not selective: bludgeoning 2d8, Fortitude half; on a failed save pushed 2 (hazard edges and Teetering apply) | Best: a surrounded reader near pits. Worst: an adjacent ally (hit and pushed) |
| **Trembling Word** (Cause Fear) [arcane] | 1, C | One enemy within 6: Will save or Frightened of the reader (saveEnds Will, cap 3); a Demoralised enemy is Routed instead (T6) | Best: a group whose leader fell. Worst: fearless enemies, bosses |
| **Grasping Roots** (Entangle, on units only) [primal] | 1, C | Burst r1 within 8, not selective: Reflex save or Restrained (roots; breakers slashing, fire), sustained up to 3, concentration | Best: stopping a rush; setting up Reflex areas. Worst: ranged enemies; a reader already concentrating |
| **Jarring Whispers** (Dissonant Whispers) [arcane] | 1, C | One enemy within 8: psychic 3d6, Will half, rider-flagged (ends concentration or Unfocused) | Best: breaking an enemy caster's concentration |
| **Guiding Radiance** (Guiding Bolt) [divine] | 1, C; **Later** (Votary or Hexer; Herald design 8.8, HR-24) | Item spell attack, range 8: radiant 4d6, rider-flagged (Blinded, or Revealed on a hidden target); a miss uses it up | Best: undead; blinding an archer. Worst: out of class (two failure points) |
| **Withering Touch** (Inflict Wounds) [divine] | 1, C; **Later** (Votary or Hexer; Herald design 8.8, HR-24) | Melee item spell attack: necrotic 3d10, rider-flagged (Withered) | Best: a regenerating enemy or potion carrier. Worst: undead |
| **Frost Shard** (Ice Knife shape) [arcane, primal] | 1, C | Item spell attack, range 8: piercing 1d10; hit or miss, cold 2d6 burst r1 around the target, Reflex half, rider-flagged (Chilled), not selective | Best: a cluster of Soaked targets. Worst: a target in melee with allies |
| **Mending Word** (Healing Word) [divine] | 1, C | One ally within 6: heals its Recovery (the potion's number, 7.2); Withered blocks it; never lifts Downed; never on a Downed unit. In class a bonus action. `usableBy.monster: false` | Best: a healer out of slots. Loot only (never the camp channel, P1; never carried); counted in the delivery mix. Not the answer to idea 3 |
| **Scorching Rays** (Scorching Ray) [arcane] | 2, U | Three item spell attacks within 8: fire 2d6 each, rider-flagged; one Burning attempt per target per use; only the first hit pays out Oiled | Best: finishing spread-out wounded enemies |
| **Blinding Flash** (Blindness) [arcane, divine] | 2, U | One enemy within 8: Fortitude save or Blinded (saveEnds Fortitude, cap 2); no concentration | Best: shutting down an elite archer or caster |
| **Fireball** [arcane] | 3, R | Burst r2 within 12, not selective: fire 8d6, Reflex half, rider-flagged; pays out Oiled; burns oil squares | Best: packs at range. Worst: mixed melee |
| **Lightning Line** (Lightning Bolt) [arcane, primal] | 3, R | Line L10: lightning 8d6, Reflex half; Soaked hits arc (settled) | Best: a corridor queue; Soaked clusters |
| **Terror** (Fear) [arcane] | 3, R | Cone L3, **enemies only** for every reader (Herald design 8.8, HR-24): Will save or Frightened (saveEnds Will, cap 3), concentration; Demoralised → Routed | Best: collapsing a group's morale |
| **Slowing** (Slow) [arcane] | 3, R | Up to 3 enemies in a burst r2 within 12: Will save or Slowed (Chilled + Dazed, conditions "Slowed (spell)" row), concentration | Best: an elite trio or a boss's escort |
| **Unweaving** (Dispel Magic) [arcane, divine] | 3, R | One unit within 12. Enemy: ends spell-sourced beneficial conditions and its concentration. Ally: ends spell-sourced harmful conditions below hard. Never hard conditions, curses, lingering conditions, `oncePerLife` affixes or boss phase wards | Best: an enemy caster's buffs. Worst: fights with no spell effects |
| **Icefall** (Ice Storm) [arcane, primal] | 4, VR (1 per floor) | Burst r2 within 12, not selective: bludgeoning 2d8 and cold 4d6, Reflex half; cold rider-flagged (Chilled); bludgeoning shatters Frozen | Best: large late packs |
| **Blight** [primal] | 4, VR (1 per floor); **Later** (Rootwarden; Herald design 8.8, HR-24) | One creature within 8: necrotic 8d8, Fortitude half, rider-flagged (Withered); plants are weak | Best: bosses and regenerators. Worst: undead, constructs |

The names in brackets are the SRD shapes each entry adapts; they are
authoring references, never shown in game (section 17).

**`classTags` (the coverage-or-cut audit, closed; Arcanist design 8.10;
change ARC-24; Herald design 8.8; change HR-24):** `arcanist`: Fan of
Cinders, Shockwave, Grasping Roots, Frost Shard, Scorching Rays,
Fireball, Lightning Line, Slowing, Icefall, Blinding Flash (the Herald
declined); `arcanist` + `herald` (one shared record): Unweaving;
`herald`: Mending Word, Trembling Word, Jarring Whispers, Terror;
**Later**: Guiding Radiance, Withering Touch (Votary or Hexer), Blight
(the Rootwarden). The M2 tier-4 pool holds only Icefall; optional lever
(lead's call, not required): a `herald`-tagged Pall scroll at tier 4,
Very rare (Herald design 8.8). **Pending request (M3; traps and locks
design 9.2; change TL-35):** Unbolting [arcane, divine] needs a
`classTags` entry here; since the audit is closed, it waits
for the Arcanist (8.10) and Herald (8.8) owners' sign-off and changes
nothing until they rule.

**Later scrolls** (each waits for its condition or rule): Blessing (Bless,
Blessed; t1); Slumber (Sleep, Asleep; t1); Holding (Hold Person, Held;
t2); Hushing (Silence on a unit, Silenced; t2); Flicker Step (Misty Step;
t2) and Far Step (Dimension Door; t4), both waiting for a teleport rule in
ability model 8 (the traps and locks design's relocation traps and
Mirror Glyph wait for the same rule; traps and locks design 7; TL-41);
Elemental Endurance (Protection from Energy, Resisting;
t3); Banishing (Banishment, Banished; t4). Tier 2 is thin in M2 (two
scrolls plus the Rime Lance wand); Holding, Hushing and Flicker Step are
its reserves.

**Pending request (M3; traps and locks design 9.2; change TL-35;
consumables, Arcanist and Herald owners):** a tier-2 reserve,
**Unbolting** [arcane, divine]: action, range 6, opens one lock or Jammed
lock without its consequence, noise 10; never the Bossgate; off-class
readers use Arcana against 10 + tier as usual. With it, **Unweaving**
also turns a Runed lock Plain. Until the `classTags` entry (above)
is ruled, Unbolting is not in the roster and Unweaving is unchanged.

### 16.5 Wands (M2)

All: 2 charges max, starting charges pre-rolled {1, 2}, camp refill; each
charge is a camp spend (settled, items 12.8; 13.5); out of class every
charge is its own action + Arcana check; loot, carriers and the camp
channel; floor 2+.

| Wand (shape) | Value, rarity | Effect | Notes |
|---|---|---|---|
| **Wand of Sparks** (Magic Missile; our types have no force) [arcane] | (T1, 2) = 2, U, 40 coin | 3 / 4 / 5 lightning darts by user band, 1d4+1 each, split among visible enemies within 12; no attack roll, no save; not rider-flagged; each dart is its own damage event | Best: finishing minions; breaking concentration |
| **Wand of Outlining** (Faerie Fire) [arcane, divine, primal] | (T1, 2) = 2, U, 40 coin | Burst r1 within 12, not selective: Reflex save or Revealed, 2 turns | Best: ambush floors. Reveals hidden allies too |
| **Wand of Snaring** [primal] | (T1, 2) = 2, U, 40 coin | One enemy within 8: Reflex save or Restrained (cords; breakers slashing, fire), `turns:2`, no concentration | Never on floor-1 carriers |
| **Wand of Rime Lance** [arcane, primal] | (T2, 2) = 3, R, floor 3+, 60 coin | Item spell attack, range 10: cold 3d8, rider-flagged; `freezing` **only on a Soaked target** (Soaked + Chilled → Frozen, fallback Chilled; Steeled and resolve apply (ARC-24; Arcanist design 8.10)); otherwise Chilled | The only repeatable hard-capable item (the class Rime Lance also exists from Arcanist L3, Arcanist design 8.10; ARC-24): `usableBy.monster: false` in M2; freeze rate reported |

**`classTags` (Arcanist design 8.10; change ARC-24):** `arcanist`: Wand
of Sparks, Wand of Snaring, Wand of Rime Lance, Wand of Outlining (the
Herald declined; Herald design 8.8, HR-24).

### 16.6 Talisman

| Record | Rarity, value | Effect | Wave |
|---|---|---|---|
| **Reprieve Phial** | R, 3; floor 3+; 1 per run; unique **(agreed, Q2)** | Section 14 | M2, else Later (14.10) |

### 16.7 Cut or declined

| Entry | Reason |
|---|---|
| Haste, Fleet and Invisibility potions | Speed and extra-action anti-goals; Invisibility belongs on a class-spell scroll (CN-16 strikes the potion sources) |
| Heightened potion | An ability modifier (denylist) |
| Courage draught (Rallied) | A typed tonic that undercuts the tonic knob and copies Rally (CN-16) |
| Freedom Draught (Unfettered) | Unfettered blocks `restraint` conditions, Chilled and Mired: condition-tag immunity (denylist, 6.2); Unfettered stays a spell condition (CN-16) **(lead's call)** |
| Ward draught | Duplicates Wardstone (trinkets 13.7, M2) |
| Clarity draught | Duplicates the tonic plus roll mode |
| Regeneration potion | A pre-buffed heal that dodges the peg |
| Typed or strong tonics | Undercut the tonic knob (conditions 22) |
| Acid flask | Corroded writes AC (denylist) |
| Smoke Pellet (Veiled) | Roll mode on attacks against the user (denylist) |
| Frost bomb | A type-swap duplicate of the Fire Bomb |
| Flash flask, glowdust | Blinded is a major (the net is the one named major flask); glowdust copies the Outlining wand |
| Any terrain-making flask | Ability model 8.4 |
| Ember and Rime coatings | Element shift duplicates Edge and Ember Ring and collides with TK-25 |
| Shatter, Thunderwave-as-thunder, Magic Missile-as-force | No thunder or force type (stat model 8.1); Shatter duplicates a bomb |
| Haste, Expeditious Retreat | Extra actions |
| Revivify, Death Ward | Death and healing |
| Counterspell, Shield, Feather Fall, Hellish Rebuke | Reaction cost |
| Hunter's Mark, Hex, smites, Divine Favor | Added damage to another source's hit |
| Web, Grease, Fog Cloud, Spike Growth, Moonbeam, Spirit Guardians, walls, clouds, Flaming Sphere | Ground zones |
| Aid, Heroism | Max HP or temporary HP |
| Prayer of Healing, Mass Healing Word; any healing wand | Area healing; free healing |
| Lesser Restoration | Copies the tonic |
| Identify, Detect Magic | Knowledge; camp identifies free (kept; exploration and information design 4.5.3) |
| Mage Armour, Shield of Faith | AC on items |
| Summons | No summon system |
| Plain scrolls (`any`) | Later hook only (option C) |
| Smelling salts | Undercut Medicine and Lifeline (5.2, option C) |
| Glass damage on beneficial throws | Rejected by the client (Q4) |
| Untrained tier-1 scroll reads (D′) | Rejected by the client (Q1) |

### 16.8 M2 roster count

1 potion, 1 tonic, Remedy, 4 flasks, 15 scrolls (Herald design 8.8;
change HR-24), 4 wands; the phial gated.

**M3 (change XI-23):** +1 run-scoped party counter, the Lodestring
(Wax Key model; never a scroll, a deck card or a notable; exploration
and information design 4.6.2). The Ear Trumpet (M3, conditional; 10) is
a vendor counter outside this count.

## 17. Licensing

**(lead's call; mirrors trinkets 16)**

- Item names and texts are original. Scroll and wand names follow the
  ability's name, which the classes design owns; catalogue names here are
  placeholders.
- SRD-shaped mechanisms (Spell Scroll gating and check shape, Potion of
  Healing, alchemist's fire, oil, holy water, wand charges, the spell
  shapes in section 16) are adapted under CC-BY-4.0 and attributed in the
  credits **(settled, main design 2)**.
- Every record flagged `srd` cites `srdSource`, **checked against SRD
  5.2.1 before data is written**. The 5.2 Spell Scroll wording and the
  2024 bonus-action potion rule were not re-checked.
- Ideas from the games in 4.1 are labelled original and carry no names
  from them (no game's item or perk names).
- CN-24 adds consumables to the main design credits pointer.

## 18. Data and the core

**(lead's call)**

### 18.1 Tuning block

`inventory.json` gains a `consumables` block (the save hash covers
`inventory.json`, so a tuning change never reinterprets a save) (CN-2):

```json
{ "consumables": {
    "potionPermille": 1000, "monsterPotionPermille": 250,
    "thrownPotionPermille": 750, "thrownPotionPermilleRange": [500, 1000],
    "potionThrowRange": 5, "flaskRange": 5, "throwAllyRange": 5,
    "pegMarginPermille": 800,
    "scrollArcana": { "base": 10, "perTier": 1 },
    "scrollFloorCap": [1, 2, 3, 4, 4],
    "scrollReadSetsCampSpend": true,
    "itemSpellThrift": 50,
    "tier4ScrollsPerRunBeforeFinalBoss": 1,
    "wandValue": [ { "tier": 1, "max": 2, "value": 2 },
                   { "tier": 1, "max": 3, "value": 3 },
                   { "tier": 2, "max": 2, "value": 3 } ],
    "sundriesPerFloor": [[3, 5], [3, 5], [4, 5], [4, 5], [4, 5]],
    "offensiveSundryMin": 1, "offensiveSundryMax": 2,
    "healingSundryMin": [1, 1, 1, 1, 1], "healingSundryMax": 3,
    "healingOffPathPermille": 600, "supplyMainPathMin": 1,
    "heroKitPotions": 2, "recruitKitPotions": 1, "campHeroPotionTopUp": 1,
    "talisman": { "leaveHpPermille": 334, "minFloor": 3,
                  "offerChancePermille": 500,
                  "channelWeights": { "container": 2, "boss": 1,
                                      "campReward": 1 } } } }
```

- All values *(illustr.)*, except these, which are **not tuning (new
  lead's call)**:
  - **client numbers:** `scrollArcana` (10 + tier, Q1),
    `potionThrowRange` 5 and `thrownPotionPermilleRange` (Q4);
    `talisman.leaveHpPermille` 334 (death and healing Q8); `healingSundryMin`
    (floors 4–5 may rise to 2), `healingSundryMax`, `supplyMainPathMin`,
    `heroKitPotions`, `recruitKitPotions` and `campHeroPotionTopUp`
    (death and healing Q3; death and healing design 20.1) **(change
    DH-42)**;
  - **engine switches:** `scrollReadSetsCampSpend` (13.5, `true`; CN-11 client-approved
    2026-11-17);
    `tier4ScrollsPerRunBeforeFinalBoss` (the lead's pre-agreed lever,
    section 21: `null` = off, 1 when engaged; the loot generator reads it;
    **defaults to 1 while the item rolls usability floor ships and Rare
    gear is thin** (M2; item rolls design 4.7 item 9; change IR-16));
  - **owned elsewhere:** `talisman.leaveHpPermille`, whose value the
    death and healing design owns (16). `recoveryPlaceholder` is removed
    (7.2). **(change DH-42)**
- `scrollFloorCap`, `sundriesPerFloor` and `healingSundryMin` are read at
  index floor − 1; their length equals the floor count. **(change DH-42)**
- `throwAllyRange` (tonics) and `flaskRange` start equal to the potion's
  fixed range 5, so every belt throw has one range; a builder who moves
  them apart says why in the reports. **(new lead's call)**
- Plus `stackMax.talisman` 1, `categoryPermille.talisman` 1500 and
  `familyCapPerRun.talisman` 1.

### 18.2 Record changes

(CN-1)

- `category` gains `talisman`.
- `channels[]` gains `campReward`: offered on the camp reward card only,
  never in the notables shop. `camp` keeps its meaning (both). Only the
  talisman uses `campReward` in M2.
- `beltable` allows consumable, scroll, wand and talisman; Remedy is not
  beltable.
- `use.targets[]` entries gain:
  - `side`: `self | ally | enemy | any` (`any` is a square-targeted area,
    never `selective`);
  - `area`: `unit | burst:N`;
  - optional `range` and `potencyPermille`.
  **Beneficial is derived:** an entry is beneficial when `side` is `self`
  or `ally`, harmful otherwise. A beneficial entry never rolls (no attack,
  no save).
- `use` gains `trigger` (talisman only, closed list: M2 `wouldDropToZero`).
  A `trigger` use is always automatic; there is no policy field.
- `monsterProfile.useWhen` is a closed vocabulary with parameters
  (section 15): `always`, `selfHpAtOrBelowPermille{n}`,
  `selfHoldsTier{tier}`, `minTargets{n}`, `groupHasDamageType{type}`,
  `groupHasDamageTypeAny{types}`.
- The heal effect kind `healRecovery {permille, monsterPermille}` (7.2).
- **Tuning references:** the numeric fields `range`, `potencyPermille` and
  the heal's `permille` and `monsterPermille` are `int | tuningKeyRef`. A
  string names a key of the `consumables` block and is resolved at load;
  an unknown key fails the build, and validation runs on the resolved
  value.
- Each `ability.item.*` record declares its offer value (9.1).
- Stack pools are derived (items 12.8: by `kind`; a talisman by its
  category); records declare no stack pool and no `stackMax` (items 19.1
  rule 5). A wand declares its `camp` pool and `charges`.
- Scroll and wand tier is derived from the ability record, never stored.
- `use.difficulty` is unchanged.
- **Pending request (M2; item rolls design 4.10; change IR-23;
  consumables owner):** every scroll, wand and notable flask declares an
  `effectTag` from {damage, control, ward, mobility, utility}, used for
  offer distinctness; `use.damage` stays as an alias for loot economy
  design 6.11. Until ruled, no record declares it, and offer
  distinctness falls back to category and `use.damage` only (item rolls
  design 14.2).

### 18.3 Example records *(illustr.)*

```json
{ "id": "item.potion.healing", "category": "consumable", "kind": "potion",
  "rarity": "common", "beltable": true,
  "use": { "ability": "ability.item.heal_recovery", "difficulty": "user",
    "targets": [
      { "mode": "self", "side": "self", "area": "unit", "cost": "bonus" },
      { "mode": "adjacentAlly", "side": "ally", "area": "unit",
        "cost": "action" },
      { "mode": "thrown", "side": "ally", "area": "unit", "cost": "action",
        "range": "potionThrowRange",
        "potencyPermille": "thrownPotionPermille" } ] },
  "channels": ["kit", "container", "carried", "vendor"],
  "monsterProfile": { "abilityShape": "uses:1",
    "useWhen": { "selfHpAtOrBelowPermille": 500 } } }

{ "id": "ability.item.heal_recovery", "value": 1,
  "effects": [ { "kind": "healRecovery", "permille": "potionPermille",
                 "monsterPermille": "monsterPotionPermille" } ] }

{ "id": "item.flask.fire_bomb", "category": "consumable", "kind": "flask",
  "rarity": "common", "minFloor": 2, "beltable": true,
  "use": { "ability": "ability.item.fire_bomb", "difficulty": "user",
    "targets": [ { "mode": "thrown", "side": "any", "area": "burst:1",
                   "cost": "action", "range": "flaskRange" } ] },
  "channels": ["container", "carried"],
  "monsterProfile": { "abilityShape": "uses:1",
    "useWhen": { "minTargets": 2 } } }

{ "id": "item.talisman.reprieve", "category": "talisman", "rarity": "rare",
  "minFloor": 3, "beltable": true, "flags": ["unique"],
  "usableBy": { "party": true, "monster": false },
  "use": { "ability": "ability.item.reprieve", "trigger": "wouldDropToZero",
    "targets": [ { "mode": "self", "side": "self", "area": "unit",
                   "cost": "none" } ] },
  "channels": ["container", "boss", "campReward"] }
```

The potion and talisman records declare no `pools`: their stack pools are
derived (18.2).

### 18.4 State, events and greyed reasons

- **State:** a unit's turn budget gains `offensiveFlaskUsed` (bool; set
  at the cost-paid step; reset with the unit's own turn budget and every
  tick out of combat; monsters too) (12). The camp-spend flag (ability
  model 6.4) is set by every wand charge **(settled)**, and by every
  scroll read, failed reads included, while `scrollReadSetsCampSpend` is
  true (13.5). The `treatProof` flag lives on the Scarred instance (14.4).
- **Events:** `ItemUsed{instance, unit, target, mode: self | adjacentAlly
  | thrown | inClass | outOfClass}`; `ItemReadFailed{instance, unit}`;
  `ItemTriggered{instance, unit, trigger}`; plus the existing
  `ItemDestroyed{usedUp}` (CN-9).
- **Greyed reasons:** existing `Untrained`, `NotInCombat` and the ability
  model's camp-spend reason; new `FlaskThisTurn` (CN-9). Target-dependent
  reasons (a Downed target, out of range) are shown on the cursor
  (ability model 15.4).

## 19. Validation, testing and simulator

### 19.1 Content validation

**(lead's call)** New and extended rules; the build fails on any violation
(CN-7):

1. Every consumable (except Remedy), scroll, wand and talisman record is
   `beltable`; its stack pool is derived and takes `stackMax` from the
   tuning file (no record declares one).
2. Every effect kind passes the consumable denylist (6.2); non-scroll and
   non-wand records pass the class copy registry.
3. Every use target declares `mode`, `side`, `area` and `cost`. A
   beneficial entry (`side` self or ally) is never stronger than drinking
   (resolved `potencyPermille` ≤ 1000; tonic tier ≤ minor), carries **no
   damage part** and never rolls. A thrown beneficial entry has `side:
   ally` and `area: unit`, and exists only on potion, tonic and water
   records. A potion's thrown entry has range `potionThrowRange`.
4. Rule 8 extended: buffs last 10 turns or fewer on consumable, scroll,
   wand and talisman records; no concentration on non-spell consumables.
   Floor-1 carried items are self-targeting **(settled)**.
5. Rule 9 extended: no curses on consumable, scroll, wand or talisman
   records.
6. Rule 4 extended: a talisman's derived stack pool is `never`, and a
   talisman record declares no other pool.
7. Flasks: every entry's `area` is `unit` or `burst:1`; never `selective`;
   no terrain output; minor or setup conditions only in M2; fixed dice;
   `useWhen` exists exactly when `usableBy.monster`. A carried damaging
   flask passes the enemies 6.4 guards on the reference carrier at its
   `minFloor` (section 15).
8. Scrolls: rarity = tier; tier ≤ `scrollFloorCap[minFloor − 1]`, and
   the array's length equals the floor count; `classTags` non-empty and
   resolving to launch class ids (no `any`, no tradition tags in M2; the
   M2 mapping is Arcanist design 8.10, ARC-24, and Herald design 8.8,
   HR-24); the
   spell is not on the never-list (13.7); healing scrolls exclude the
   `camp` channel and are `usableBy.monster` false; monster-usable scrolls
   pass the 6.4 guards and items 19.1 rule 7. (CN-20 was approved on
   2026-11-17; this rule would apply without it too.)
9. Wands: tier ≤ 2; pool `camp`; 1 ≤ `charges.min` ≤ `charges.max` ≤ the
   pool's `max`; (tier, pool `max`) has a `wandValue` row; `minFloor` ≥ 2;
   no healing; a monster-usable damaging wand passes the 6.4 guards.
10. Talisman: `usableBy.monster` false; `unique`; channels ⊆ {container,
    boss, campReward}; `minFloor` ≥ `talisman.minFloor`; family cap 1;
    cost `none`; `trigger` from the closed list; target `side: self`.
11. Coatings (Later): zero added damage; tag `blade` or `launcher`; a named
    condition rider only; Serrating never on slashing weapons.
12. Value (9.1): every `ability.item.*` declares a value; a scroll's
    summed value equals its tier; a wand's equals `wandValue(tier, pool
    max)`; every other use-only record's equals its rarity budget; Remedy
    excepted. Pegs P1, P2 and P4 hold on camp prices after discount and
    `minPrice`.
13. Every `usableBy.monster` record with a `healRecovery` effect sets
    `monsterPermille`.
14. The enemies 6.4 guards are computed with no party consumables or
    talismans.
15. `thrownPotionPermille` lies within `thrownPotionPermilleRange`.
    **(new lead's call)**
16. `useWhen` is from the closed vocabulary (section 15); every
    `tuningKeyRef` resolves to a key of the `consumables` block.
    **(new lead's call)**
17. The death and healing codes on this block's data (death and healing
    design 21.2): `HealPerTargetAboveRecovery` (healing scrolls),
    `GuaranteedItemUnreachable`, `SundryOverfull` and
    `HealingFloorUnmet`. **(change DH-43)**

### 19.2 Run-time invariants and tests

**Run-time invariants (lead's call):** a beneficial throw never rolls,
never splashes and never deals damage; one unit never makes two camp
spends in one turn, a wand charge always counting as one, and a scroll
read counting as one while `scrollReadSetsCampSpend` is true; at most one
offensive flask per unit per turn; a scroll is never upcast; an untrained
out-of-class read is never attempted; a talisman fires at most once, only
from its holder's belt, never on a Downed unit, only after Death-warded
had its chance **(change DH-43)**, and never changes place in combat; the
hero's HP never shows 0 while a phial is armed on its belt.

**Tests (new lead's call):**

- the healing potion heals Recovery without the Medicine bonus; a monster
  heals `monsterPotionPermille` of max HP; a throw heals by the one 7.2
  formula, at least 1; **Withered on the receiver beats the minimum of 1**
  (the heal is 0);
- a Downed target is never offered for a potion, throw or healing scroll;
  a tonic on a Downed ally is offered (minor only); the throw is not
  offered out of combat;
- an in-class read spends no slot and never upcasts; an out-of-class read
  rolls at every tier and a failure spends the scroll or charge; an
  untrained read is greyed `Untrained`;
- the camp-spend flag: a slot spell, then a scroll read: blocked; Surge,
  then a scroll read: blocked; a scroll read, then Surge: blocked; a failed
  out-of-class read sets the flag; **with the switch off, a scroll is not
  blocked, and a wand charge still is**;
- `FlaskThisTurn` after one Fire Bomb; water and oil are not blocked; the
  flag resets at the unit's next turn start and binds monsters;
- the phial: the order Death-warded → phial **(change DH-43)**; HP set
  to the share of the new
  maximum; the concentration DC from the pre-save total; `treatProof`
  Scarred on the hero and on an already-Scarred unit; `downedThisFloor`
  untouched; no move in combat; a pack phial does nothing; the red area
  preview;
- **the rest screen:** a `treatProof` Scarred is not offered for Treat and
  uses no attempt; a standard Scarred on another unit at the same rest
  still gets its attempt; a Remedy ends the instance and the flag;
- the phial pre-roll: deterministic per seed; always three draws on
  `loot/run` (pending this owner's confirmation, 14.7), unmoved by a
  floor retry (V13t (m); change SV-5; seeds and validation design 4.3);
  a declined reward card loses it for the run;
- pegs P1, P2, P4 at every Leadership discount step and at `minPrice`;
- replay tests across throws, reads, failed reads and a phial firing,
  rebuilt from the 18.4 events, including **a two-part hit** (Icefall)
  and **a tick batch** that would each drop the holder to 0.

### 19.3 Simulator reports

**(lead's call)**

- Belt items used per fight, by kind and fight type (zero means the belt
  is decoration).
- Consumables unused at each camp (hoarding), and scrolls carried in packs
  at each boss.
- Coin-funded against supply-funded HP per party size (P1).
- Delivery mix: drink, Hand over, Administer, throw, healing scroll.
  Throws under 3% of potion deliveries → raise `thrownPotionPermille`;
  Administer share collapsing → lower it.
- Tonic and Remedy use against Treat.
- Scroll reads in class and out of class by reader class; Arcana success
  per tier (target 50–60% for trained non-casters, *illustr.*); non-caster
  share of reads (revisit trigger under 5%); reads above the reader's
  class tier; turns where the camp-spend flag blocked a read; companion
  scroll reads per non-boss fight (the boss-reserve exclusion, 13.7).
- Fights where an untrained unit had no offensive flask on its belt or in
  the party's packs, by floor (floor 1 expected, 5.1).
- **Scroll and bomb share of boss damage** (nova watch; alarm above 25%,
  *illustr.*).
- Wand charges per floor; Rime Lance freeze rate.
- Flask friendly-fire incidents; "oil thrown, then paid out within 2
  turns" rate.
- Monster carried-item use rate; kill-the-carrier-first rate; carrier
  power ÷ price for `uses:1` items.
- Talisman: runs offered a phial, and offers declined; fires per 100
  runs and by floor; runs saved per 100 (watch above about 15%: the
  first lever is `offerChancePermille`); same-fight deaths after a save
  (above about 50% → raise `leaveHpPermille`, which now goes to the
  client, death and healing design 16); hero share of fires; win
  rate with and without (target a lift of at most +10 points); potion
  doses not carried while it is belted; share of monster attacks on the
  holder.
- The death and healing reports on this design's items (death and healing
  design 21.5): guaranteed-potion and camp top-up counts, potions unused at
  camp, potions drunk per boss fight, Death-warded and phial fire rates.
  **(change DH-43)**
- The loot economy reports (M2; loot economy design 10.3; change LE-24):
  these reports are kept; `ViaticumBossShare` is read against the 25%
  scroll-and-bomb alarm above, and `BossCardUsabilityFallback` (by class)
  and `PotionsHeldAtDeath` are read beside them.

## 20. Staging

**(lead's call; the phial's gate agreed, Q2)**

- **M1:** the healing potion only (Recovery-linked): kit and containers;
  bonus-action self-drink from your own belt. No carried consumables, no
  vendor spending.
- **M2:** Administer, Hand over, take-from-Downed **(settled M2)**; the
  potion throw **(agreed, Q4)**; tonics (with their range); Remedy; water,
  oil, Fire Bomb, Greater Fire Bomb; sundries; scrolls tiers 1–4 and the
  four wands with camp refill; the scroll camp-spend switch (13.5);
  vendor and camp pricing; monster use from floor 2; validation; reports.
- **The Reprieve Phial:** M2 if the death and healing design is approved
  before M2 content lock, otherwise Later (14.10). **(change DH-44)**
- **Later:** coatings, special arrows and Coated; Quivered; Slinger's
  Bracers; Scrollwright's Token; sidegrade potions with their conditions;
  holy water, sticky flask, net, thunderstone; teleport scrolls (with a
  teleport rule); the Later scrolls of 16.4; plain scrolls (option C,
  only on the revisit trigger); monster ally throws; the Asleep wake
  trigger.

## 21. Risks

| Risk | Mitigation |
|---|---|
| **Scroll nova at the boss** (packs hold scrolls; four readers) | Camp-spend flag (13.5, CN-11, client-approved 2026-11-17); family caps; 1 Very rare per floor; boss-damage share alarm at 25%. **Pre-agreed lever:** at most 1 tier-4 scroll per run before the floor-5 boss (`tier4ScrollsPerRunBeforeFinalBoss` 1; engaged by default from M2, item rolls design 4.7 item 9, change IR-16) |
| **CN-11 declined at approval** (closed: the client approved CN-11 on 2026-11-17) | Not in force. Recorded only: `scrollReadSetsCampSpend: false` with the 25.2 fallback bounds, the pre-agreed lever set at once, the boss-damage alarm a release gate |
| **The non-caster scroll route is empty** | DEP-1 (13.8); at least 1 and at most 2 offensive sundries per floor from floor 2; the no-flask report; revisit trigger at 5% |
| **Thrown potion unused** | Starts at 750; the knob runs to 1000, where Administer merges into the throw |
| **The phial fires too often or too little** (crit rule and level-1 HP, death and healing design 7, 9) | Reports; `offerChancePermille` first, then `leaveHpPermille` (a client number, death and healing design 16) **(change DH-44)** |
| **Monsters retarget away from the hero** | The "spends a Rare save" scorer term; holder-attack report |
| **The phial burned by the party's own area** | Red area preview; the suggester never proposes it; deliberate (a death would end the run) |
| **A Scarred hero carries a weaker HP pool into the next fights** | Agreed price (Q3); Remedy at the vendor; same-fight-death report |
| **Coin replaces rest healing for a solo hero** | P1 with a 20% margin, after discount; limited potion stock |
| **Recovery numbers lumpy** | Banded-dice fallback with the same record and peg shape |
| **Oil flask is a dead slot** | Payout report; if low, party loot drops it and it stays monster-only |
| **Repeatable Frozen from Rime Lance** | Soaked-only freezing; no monster use in M2; freeze-rate report |
| **`uses:1` carriers fail items 19.1 rule 7** | Reported; sundry exemption as fallback |
| **Thin tier 2** | Named Later reserves; tier-2 offer report |
| **Bombs out-damage scrolls** | Fixed dice; Greater Fire Bomb at value 2; one offensive flask per turn |
| **A carried bomb breaks a permadeath guard** | Every carried damaging item passes the 6.4 guards (15; 19.1 rule 7); no carried Greater Fire Bomb in M2 |
| **SRD shapes from memory** | Check against SRD 5.2.1 before data |

## 22. Left to the builders

**(lead's call, new)**

- All numbers in the `consumables` block marked as tuning (18.1) and
  every *(illustr.)* value: heal permilles, `flaskRange` and
  `throwAllyRange`, `scrollFloorCap`, `wandValue`, sundry counts,
  `itemSpellThrift`, the talisman's `offerChancePermille` and
  `channelWeights`, prices and `basePrice`, report targets marked
  *(illustr.)*.
- Placeholder names, flavour text and the roster within section 16's
  counts, floors and rulings; `useWhen` details within section 15.
- Icons, the phial's armed glow, throw and trigger animations and card
  wording within 14.8.
- **Not tuning:** the Arcana gate, "always rolled" and `scrollArcana`
  10 + tier (Q1); the phial's rarity, floor, one per run (a cap), unique
  and never bought, and its holder-only belt trigger (Q2); `treatProof`
  (Q3); no damage on beneficial throws, `potionThrowRange` 5 and
  `thrownPotionPermilleRange` (Q4); `scrollReadSetsCampSpend` (`true`;
  CN-11 client-approved 2026-11-17); `tier4ScrollsPerRunBeforeFinalBoss` (the lead's lever);
  Recovery (the lead's numbers, death and healing design 8); the denylist
  and relaxations, one offensive flask per turn and the wand tier cap (the
  lead's); `leaveHpPermille` 334 (a client number, death and healing Q8)
  and the save order (death and healing design 15, 16). **(change
  DH-44)**

## 23. Changes to approved documents

Applied on 2026-11-17, when the client approved this document. The
changes that serve the Reprieve Phial (talisman) were applied as text
marked as waiting for the death and healing design (14.10), which sets the
phial's final numbers and owner order; those markers were removed when
that design was approved on 2026-11-17 (its changes DH-14 to DH-44). Each change names the exact
section it touches. Ids are provisional and match the proposal;
CN-10 and CN-13 were merged into other changes during review and are not
reused, and CN-25 (the D′ trained-only change) lapsed when the client
rejected D′ and is not reused either; CN-26 and CN-27 were added in the
final review. **Agreed or decided rows are flagged.** CN-11 and CN-20
were approved by the client on 2026-11-17.

**Narrowings recorded inside this design (no change id, no approved text
changes):** the consumable denylist (6.2); no healing on wands; wands tier
1–2 only; no ground squares from flasks; one offensive flask per unit per
turn; no damage parts on beneficial deliveries. Narrowings of approved
action rows (no potion or healing scroll on a Downed unit; the talisman
never moves in combat) change approved text and are in CN-3 and CN-12.

**Items design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CN-1 | 12.3, `category`, `channels[]`, `beltable`, `use`, `classTags[]`, `monsterProfile` rows; 5.2, Belt row **(agreed row)** | 12.3: `category` gains `talisman`. `channels[]` gains `campReward` ("the camp reward card only, never the notables shop"); `camp` keeps its meaning. `beltable`: "only for consumable, scroll, wand and talisman; Remedy is not beltable". `use.targets[]` entries gain `side` (self, ally, enemy, any), `area` (unit, burst:N) and optional `range` and `potencyPermille` (`int` or a tuning-key reference); beneficial is derived from `side`; `use` gains `trigger` (talisman only; always automatic). `monsterProfile.useWhen`: a closed vocabulary (consumables design 15). `classTags[]`: real class ids only. `use.difficulty` unchanged. 5.2 Belt row: contents "consumables (not Remedy), scrolls, wands and talismans"; "the only place consumables, scrolls, wands and talismans are used from in combat" | Lead; talisman part, including the 5.2 Belt row, **client (agreed 2026-11-17, Q2)** |
| CN-2 | 5.1, tuning file | `stackMax.talisman` 1; `categoryPermille.talisman` 1500; `familyCapPerRun.talisman` 1; the `consumables` block (consumables design 18.1) | Lead; talisman keys **client (agreed, Q2)** |
| CN-3 | 7.1, "Use a belt item" row **(settled row, clarified)**; Administer, Hand over, Take from a Downed ally and Retrieve rows **(settled rows, narrowed)** | "Use a belt item": "Scroll or wand in class (the reader's class is in `classTags`, any spell on its list): its ability's cost; no slot is spent; a scroll casts at its own tier and is never upcast. Outside your class: action + trained Arcana against 10 + tier; a failure uses it up. Tonic thrown at an ally: action, range `throwAllyRange`, minor only." The range fills a missing parameter. (The scroll camp-spend sentence is CN-11's alone.) Administer and potion throw: "not on a Downed unit (a potion or healing scroll; a tonic stays legal, minor only)". Hand over, Take from a Downed ally and Retrieve: "never a talisman in combat" | **Client (agreed, Q1)** for the scroll wording; Lead for the range and the two narrowings |
| CN-4 | 17, row 4; 7.1, new row "Potion thrown at an ally" | "Whether potions can be thrown (default no)" becomes "potions can be thrown at a willing ally: action, range 5, `thrownPotionPermille`, never lifts Downed (consumables design 11)" | **Client (agreed 2026-11-17, Q4)** |
| CN-5 | 10.5, Pegs | P1 as the margin inequality on camp prices after discount and `minPrice`; P2; P4. Supply pricing unchanged | Lead |
| CN-6 | 12.5 and 5.1 `rarity`; 12.3 `value` row; 12.6 | `props` ranges apply to gear; a use-only record meets rule 1 with its `use`. `value`: each `ability.item.*` declares its offer value; the load check compares a scroll's value with its tier, a wand's with `wandValue(tier, pool max)`, every other use-only record's with its rarity budget (consumables design 9.1) | Lead |
| CN-7 | 19.1, rules 4, 6, 8, 9; new rules | Rules 4, 6, 8, 9 extended to scroll, wand and talisman (rule 6: `beltable` ⇒ category ∈ {consumable, scroll, wand, talisman}); consumables design 19.1's rules added | Lead |
| CN-8 | 17, rows 4 and 5; 24 | "Designed: consumables design"; dependency pointer | Lead |
| CN-9 | 7.3; 18.3; 19.3; 20; 10.8 | Greyed `FlaskThisTurn`; events `ItemUsed{mode}`, `ItemReadFailed`, `ItemTriggered`; reports pointer to consumables design 19.3; staging pointer; 10.8 adds "automatic triggers" and notes that the pack, not the belt, holds out-of-combat scrolls | Lead |

**Ability model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CN-11 | 6.4, **agreed row**, new sentence after the lead's-call paragraph | "A scroll read (in or out of class, including a failed attempt) uses the unit's camp spend for that turn: it is greyed if the flag is set, and sets it when its cost is paid. (A wand charge is already a camp spend: its pool is `camp`.)" The agreed sentence is unchanged, but its scope widens beyond `camp` pools to scroll reads | **Client (approved 2026-11-17; lead's recommendation, 25.2)** |
| CN-12 | 7.2, scroll row; Treat row **(agreed, C4)**; Administer, Hand over, Take from a Downed ally and Retrieve rows (I-5); new rows | Scroll row: "against 10 + tier; in class, no slot is spent" (wording). Treat row: "Diseased and Scarred at a rest, except a `treatProof` Scarred". Administer: "not a potion or healing scroll on a Downed unit". Hand over, Take from a Downed ally, Retrieve: "never a talisman in combat". New rows: "Throw a belt flask: action, range 5, no attack roll; at most one offensive flask per turn"; "Throw a belt potion at an ally: action, range 5, line of sight, no roll, `thrownPotionPermille`; never lifts Downed; not on a Downed unit" | Lead; scroll wording **client (agreed, Q1)**; potion row **client (agreed, Q4)**; Treat row **client (agreed, Q3)** |

**Conditions design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CN-14 | 18.10 Scarred (Effect, Duration, Sources); 14, the "At most 2 harmful lingering conditions" bullet (lead's call) and the "Scarred (agreed)" bullet **(agreed)**; 8, `treat` row (C4); 18.2 Downed, Interactions | 18.10 Sources add "a talisman save (the holder, the hero included)"; "The hero is never Scarred: its downing ends the run" becomes "The hero is never Scarred by a downing (its downing ends the run); a talisman save may Scar it". 18.10 Duration: "Medicine Treat during a short rest removes it, except a `treatProof` instance (a talisman save), which is not offered for Treat and uses no attempt; a Remedy or camp ends it and its flag". 14, cap bullet: the exemption adds "or a talisman save". 14, Scarred bullet: "A companion that is downed and gets up, or any unit saved by a talisman (the hero included), gains Scarred … Medicine Treat during a short rest removes it, except a talisman's `treatProof` Scarred". 8, `treat` row: "Diseased and Scarred during a rest, except a `treatProof` Scarred". 18.2: a talisman save does not set `downedThisFloor` | **Client (agreed 2026-11-17, Q2, Q3)** |
| CN-15 | 5.4 step 4; 9.2 Entry points (engine grants row); 9.3 Damage events; 18.2 Sources; 18.9 Death-warded | The save chain runs on the event's summed total after 9.3's per-part steps, before HP is lowered: Last Ward (attack events; the hit becomes a miss) → Death-warded → talisman (consumables design 14.4). A talisman's Scarred is an engine grant applied at once, outside the step 9 queue. The concentration DC uses the pre-save total. Death-warded sources unchanged; the talisman differs from `combatOnly` (recorded, deferred to death and healing) | **Client (agreed, Q2)** for the order; Lead for the wording; final owner order death and healing |
| CN-16 | 18.4 Hasted, Fleet, Unfettered; 18.7 Invisible; 18.8 Rallied (**M2 row**); 18.9 Heightened; 18.7 Blinded and 18.4 Restrained sources | Strike "potions" from Hasted, Fleet and Heightened, the "potion `turns:3`" clauses from Hasted, Unfettered and Invisible (no potion grants them: the Freedom Draught is cut as condition-tag immunity, consumables design 16.7), and "courage potions" from Rallied. Blinded's "flash powder" and Restrained's "nets" read as monster and trap sources; the party net is a Later consumable | Lead |
| CN-17 | 8, `tonic` cure row | "Thrown at an ally (action, range `throwAllyRange`): minor only" | Lead |
| CN-18 | 18.9 Coated, Effect and Sources | Sources: "Coatings carry a named condition rider only; no element shift and no own-type rider flag (consumables design 7.5)". Effect: "its next 3 weapon hits carry the oil's rider" becomes "its next 3 weapon hits that attempt the coating's rider; a charge is spent only on such a hit (ability model 9.3 gates apply)". Weapons 5.5, which quotes the old Effect line, takes the same wording | Lead |

**Enemies design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CN-19 | 15.3 step 1 (the M-5 sentence) and Caps | "plus 1–2 supply and a few tonics" becomes "plus 1–2 supply and a few sundries (Common consumables, outside the notable count, inside the fixed budget; from floor 2 at least 1 offensive flask; consumables design 10)". Caps unchanged | Lead |
| CN-20 | 15.4, **agreed table** | Healing potion row: "heals `monsterPotionPermille` of max HP". "Bomb or flask" row: "thrown area **or a single party unit**; bombs opener-flagged; setup flasks by `useWhen`". Scroll row: "the carried spell passes the 6.4 guards on the reference carrier". "What the party gets" unchanged | **Client (approved 2026-11-17; lead's recommendation, 25.2)** |
| CN-27 | 15.3 step 2; 10.5 step 3 | "The Reprieve Phial is never a weighted draw. One pre-roll per run, on the loot stream, decides whether it appears, its floor and its channel; it replaces one notable draw of that floor and channel (a `campReward` phial takes a container draw of that floor). A declined offer is lost for the run (consumables design 14.7)" | Lead (inside agreed Q2) |
| CN-21 | 25, death and healing table | Adds the talisman needs (consumables design 24) | Lead |

**Trinkets and armour designs (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CN-22 | Trinkets 13.2 Slinger's Bracers (Later); 23, Consumables bullet and Death and healing bullet; 24.3 | Bracers: "a belt flask, tonic or potion"; 23 Consumables: the flask throw is designed; 23 Death and healing: the talisman added to the death-ward owner list; 24.3: Last-Breath Charm flagged for retirement (consumables design 14.9) | Lead (follows agreed Q4) |
| CN-23 | Armour 14.3, a note under the table on the Last Ward (Later) row | "Last Ward: owner and order with the talisman: death and healing design; recommended narrowed (for example once per run)" | Lead |

**Main design (2026-11-15):**

| # | Section | Change | Approved by |
|---|---|---|---|
| CN-24 | 10; 2, Anti-goals "Protected content" (credits pointer); 4.2 (**decided**), new sentence | Consumables, scrolls and wands are settled in this design; credits pointer for adapted consumable mechanisms. 4.2 gains: "Items that prevent a drop to 0 (consumables design 14) do not change this rule." No change to the decided text | Lead; the 4.2 sentence follows **agreed Q2** **(new lead's call: included, no longer optional)** |

**Dependency pointers across the parents:**

| # | Section | Change | Approved by |
|---|---|---|---|
| CN-26 | Stat model 10, "Items and inventory" bullet; ability model 20, "Items and inventory" bullet; conditions 25, "Items and inventory" bullet; enemies 25, "Items and inventory" bullet; weapons 26, "Consumables" bullet | "Consumables, scrolls and wands are settled in the consumables design (2026-11-17); coatings and Coated stay Later." (as W-15, AR-16 and TK-16 did for their areas) | Lead |

**Agreed and decided rows touched:** CN-1 changes the agreed items 5.2
Belt row under the client's Q2 answer; CN-3 clarifies a settled row and
narrows the settled Administer, Hand over, Take from a Downed ally and
Retrieve rows (lead's call); CN-12 does the same in the ability model
and changes the agreed Treat row (C4) under Q3; CN-14 changes the agreed
Scarred rows, the conditions 8 `treat` row and stat model 7.1's Medicine
row (below) under the client's Q2 and Q3 answers; CN-24 adds a sentence
beside the decided 4.2 text without changing it. **CN-11 (ability model
6.4) and CN-20 (enemies 15.4) change agreed rows and were not separately
asked: they were the lead's recommendation, approved by the client on
2026-11-17.**

**Stat model 7.1, Medicine row (agreed; under Q3, with CN-14):** "at a
short rest, Treat ends Diseased and Scarred (fixed difficulty, one attempt
per Scarred unit per rest)" gains "except a talisman's `treatProof`
Scarred, which is not offered and uses no attempt".

**Not changed:** the vendor list (items 10.3, agreed); `stackMax` values;
the trained-only list (stat model 7.2); `use.difficulty`; supply pricing;
family caps; belt counts; `packInCombat`; the 6.4 guards; main design
4.2's decided text.

## 24. Depends on systems not yet designed

- **Death and healing: answered** (death and healing design 25.2): the
  hero has no downed state; crits double the dice (S-3) and level-1 hero
  HP is 18 at the reference; companions cannot die (no would-die
  coverage); a Downed unit is inert; nothing but a win, Medicine, a
  completed short rest or camp lifts Downed; no massive-damage rule; the
  leave-HP is 334‰; the phial fires from any source; the order is
  Death-warded → phial, Last Ward and Last-Breath Charm retired; at most
  one recharging drop prevention per unit per fight, the phial outside it.
  **(change DH-44)**
- **Recovery formula: answered:** 4 + 2 × level + Constitution, minimum 1
  (death and healing design 8); `recoveryPlaceholder` removed (7.2, 18.1).
  **(change DH-44)**
- **Classes: answered** (classes design 11.G): the four launch classes
  are the Vanguard, Shade, Arcanist and Herald; their spell lists (the
  source of `classTags`) and the coverage-or-cut audit are its stage 2
  (CL-12a); DEP-1 (13.8) is met by the Shade; no launch class needs a
  teleport rule (classes design 11.G). **(change CL-12b)**
- **Companions: answered** (companions design 7.3): caster kits;
  Arcana-trained recruits (DEP-1), read as met by a non-caster board card
  or any party member trained in Arcana. **(change CMP-12)**
- **Ability model:** a teleport rule (Flicker Step, Far Step). No launch
  class needs a teleport rule (classes design 11.G); teleport scrolls stay
  Later. **(change CL-12b)**
- **Conditions content (Later):** Inured, Resisting, Shielded, Mired,
  Asleep, Held, Silenced, Blessed, Banished, Coated.
- **Trinkets (Later):** Slinger's Bracers and Scrollwright's Token numbers.
- **Multiclassing (Later):** in class for multiclass readers.
- **Lead's checks before data** (existing designs, not new systems): every
  SRD shape in section 16 against SRD 5.2.1; the protected-name check on
  every catalogue name.

## 25. Open items for the client

None. Approved by the client on 2026-11-17.

### 25.1 This document

Approved by the client on 2026-11-17, as a whole, including the lead's
calls. Q1–Q4 were answered on 2026-11-17 (the lead's recommendation in
each case). At approval the client also confirmed:

- **Q2's "1 per run" is a cap, not a guarantee** (confirmed by the
  client, 2026-11-17): the phial is offered in about half of runs
  (`offerChancePermille` *(illustr.)* 500, 14.7), as written.
- **CN-11 and CN-20 are approved** (25.2).

### 25.2 Two agreed-row wordings (approved 2026-11-17)

**(lead's recommendation; not separately asked; approved by the client on
2026-11-17)** The "if declined" notes below are recorded only and are
**not in force**:

- **CN-11, ability model 6.4 (agreed row):** a scroll read (in or out of
  class, including a failed attempt) uses the turn's camp spend. A wand
  charge already does under the settled rules (items 12.8), so CN-11 does
  not touch wands. **Why:** a scroll's pool is `never`, so without it a
  caster can cast a slot spell and read a bonus-action scroll in one turn,
  and four readers can empty their belts at the boss. **If declined:**
  `scrollReadSetsCampSpend` is `false`: scroll reads neither check nor set
  the flag, and wand charges still do. Scrolls then rely on belt slots,
  family caps, 1 Very rare per floor and the boss-damage alarm, and the
  pre-agreed lever (at most 1 tier-4 scroll per run before the floor-5
  boss; `tier4ScrollsPerRunBeforeFinalBoss` 1) applies from the start.
  **Approved 2026-11-17:** `scrollReadSetsCampSpend` defaults to `true`,
  so these fallback limits are not in force.
- **CN-20, enemies 15.4 (agreed table):** flasks may be single-target
  setup items, monster potions heal a share of maximum HP, carried scrolls
  pass the permadeath guards. **Why:** monsters have no Recovery, and the
  oil and water flasks are single-target. **If declined:** oil and water
  are party loot only; the potion and scroll rows stay as written, the
  engine still heals monsters by `monsterPotionPermille`, and the guard
  check on carried damaging items still applies, since it is this
  design's validation (section 15; 19.1). **Approved 2026-11-17:** the
  CN-20 wording applies; the "if declined" note is not in force.

### 25.3 Future items triggered by reports

Each comes back to the client only if a report or a later design triggers
it:

- **The non-caster scroll route,** if non-caster reads stay under about 5%
  *(illustr.)*: option C (plain scrolls) or an Arcana-mastery threshold
  (13.8).
- **Arcana mastery as a no-roll threshold** for out-of-class reads, if the
  classes design proposes it (it changes Q1's "always rolled").
- **`thrownPotionPermille` outside 500–1000,** or any change to the
  no-damage rule (Q4).
- **The phial's final numbers and the death-ward owners: answered** by
  the death and healing design (15, 16): `leaveHpPermille` 334;
  Last-Breath Charm and Last Ward retired. **(change DH-44)**
- **Monster ally throws** (11), if the delivery-mix report shows the party
  throw is healthy.

## Appendix A: how the review findings were resolved

**Identity critic**

| # | Finding | Resolution |
|---|---|---|
| ID-1 | The thrown potion at 50% is dominated by walking and Administering | Starts at 750 with the movement maths stated (5.3); knob to 1000, where Administer merges; one range, 5 |
| ID-2 | Water on a Burning ally is dominated by the thrown tonic | Water's job is enemy setup; ally use stated as fallback (12) |
| ID-3 | "Use bombs" leans on a resource made scarce | Offensive sundries at least 1 and at most 2 per floor from floor 2 (floor 1 none, by design); DEP-1 binding; no-flask report; revisit trigger. Scrollwright's Token stays Later, named as a revisit lever |
| ID-4 | Option D strawmanned | Replaced by D′ (untrained tier-1 flat roll), offered honestly in Q1; rejected by the client |
| ID-5 | Scroll numbers contradict across the material | One set: 10 + tier, `user`, no tier test; catalogue odds regenerated; talisman at 25% |
| ID-6 | The talisman's carrier choice is fake; AI shift | Framed as run insurance (5.2); scorer term; holder-attack report |
| ID-7 | Burned by own fire bomb; out-of-combat traps; four names | Red area preview, suggester ban; out-of-combat firing recorded and deferred; one name, Reprieve Phial |
| ID-8 | The belt is not a scroll hoarding cap | Said honestly (8); boss alarm; pre-agreed lever (21) |
| ID-9 | Mending Word reopens the delivery table | Not pitched as idea 3's answer; loot only (never the camp channel); counted in the delivery mix. Kept value 1: rarity = tier is a hard rule |
| ID-10 | Oil flask may be a dead slot | Payout report; fallback to monster-only |
| ID-11 | Verdict fairness and tone | Each idea lists what it gained; idea 1 says plainly the untrained fighter gets no route |
| ID-m1 | Rime Lance Soaked-only now | In the record (16.5) |
| ID-m2 | Friendly-fire term for item reads | Applies to every item read (13.7) |
| ID-m3 | Healing shape still open | Recovery-linked chosen for M1 (4, decision 5) |

**Balance critic**

| # | Finding | Resolution |
|---|---|---|
| BA-1 | No monster Recovery; M1 dependency | `monsterPotionPermille`; validation rule 13; carriers are M2; banded fallback |
| BA-2 | Numbers disagree | One answer per row: `user` everywhere; 10 + tier; no tier test; 25%; range 5 for all throws including tonics; talisman reward-only; `classTags` TBD with no tradition tags |
| BA-3 | Hand over passes the phial around | No change of place in combat (14.5) |
| BA-4 | Scarred costs the hero almost nothing | `treatProof` Scarred (agreed, Q3) |
| BA-5 | Coin buys a life | Notables shop removed; reward cards and containers only |
| BA-6 | Bombs bypass the nova guard; banded value 1 | One offensive flask per turn; fixed dice; Greater Fire Bomb at value 2 |
| BA-7 | P1 holds only barely for a solo hero | 20% margin after discount and `minPrice`; supply 8 *(illustr.)* |
| BA-8 | Camp-spend wording keyed on pools | Written as an explicit flag rule (13.5, CN-11) |
| BA-9 | Repeatable Frozen | Soaked-only; no monster use in M2; report |
| BA-10 | Smaller points | Out-of-combat firing recorded; Remedy from the pack; cut-list edits get CN-16; spill report; DEP-1 tracked |

**Consistency critic**

| # | Finding | Resolution |
|---|---|---|
| CO-1 | Eight contradictory numbers | Resolved in BA-2 and sections 8–14; Heightened cut |
| CO-2 | The talisman does not fit the schema | New category `talisman`; rules 4, 6, 8, 9 extended (CN-1, CN-2, CN-7) |
| CO-3 | The Scarred change touches more text | CN-14 cites 18.10, 14 (agreed), 18.2; `downedThisFloor` untouched; deep Scarred is an instance flag, not a condition |
| CO-4 | The nova guard changes an agreed row | CN-11 flagged; lead's recommendation, client-approved 2026-11-17 (25.2) |
| CO-5 | Change ids collide | One list (section 23) |
| CO-6 | Condition source lines contradict | CN-16 covers potions, flash powder, nets |
| CO-7 | Enemies 15.3 and 15.4 changes; M1 carriers | CN-19 cites M-5; CN-20 flagged and client-approved 2026-11-17 (25.2); M1 has no carried consumables |
| CO-8 | Coatings duplicate Ember Ring and break TK-25 | Named rider only; Quivered launcher only (CN-18) |
| CO-9 | Name collisions | Renamed: Fan of Cinders, Shockwave, Trembling Word, Flicker Step, Icefall, Elemental Endurance, Serrating Oil, Reprieve Phial; Ward Draught cut; protected-name check before data |
| CO-10 | Rules 8 and 9 key on `consumable` | Extended (CN-7) |
| CO-11 | Rare props counts on use-only records | CN-6 |
| CO-12 | Bomb banding breaks the banding rule | Fixed dice |
| CO-13 | Smoke Pellet breaks the denylist | Cut |
| CO-14 | No teleport rule | Teleport scrolls Later with a dependency |
| CO-15 | Tradition tags not in the schema | Class ids only; tags TBD |
| CO-16 | Talisman fires out of combat | Recorded difference; closed by the death and healing design 16 (any source; moot under its floors) **(change DH-44)** |
| CO-17 | Tonic range changes a settled row | CN-3, CN-17 |
| CO-18 | Remedy's "best use" wrong; Weary ruling | Mid-floor niche; Weary in full marked lead's call |
| CO-19 | Save order not tied to approved rules | Last Ward automatic-or-policy recorded as a dependency (24, CN-23) |
| Also found | Thunder and force are not damage types | Shockwave is bludgeoning; Sparks is lightning; Elemental Endurance lists fire, cold, lightning, acid |

**Final review (builder, consistency and play lenses; all lead's calls
inside the client's answers)**

| Area | Resolution |
|---|---|
| Camp spend | CN-11 and its switch (`scrollReadSetsCampSpend`) cover scroll reads only; wand charges are settled camp spends; checked at step 2, set at step 3; Surge blocks reads either way (13.5) |
| Phial damage order | The save chain on the event's summed total; synchronous Scarred; pre-save concentration DC; replay tests (14.4, CN-15) |
| Phial placement and rate | One pre-roll per run with `offerChancePermille`; declined offers lost; no reserved slots (14.7, CN-27); recharging-only drop-prevention cap (14.9) |
| Schema | `campReward` channel; target `side` and `area` with derived beneficial; closed `useWhen`; `healRecovery`; `int \| tuningKeyRef`; derived stack pools; declared item-ability values (9.1, 18.2, CN-1, CN-6) |
| Delivery maths | One heal formula, Withered after the minimum (7.2); Recovery placeholder and owner (7.2, 24) |
| Approved-row wording | Downed-target and talisman narrowings (CN-3, CN-12); `treatProof` across every Treat row (CN-14, stat model 7.1); Belt row (CN-1); Coated charge rule (CN-18); dependency pointers (CN-26) |
| Catalogue | Freedom Draught cut (condition-tag immunity); the denylist's added-damage row worded as trinkets 6.2; carried damaging items pass the 6.4 guards; no carried Greater Fire Bomb in M2 |
| Client numbers | `scrollArcana` 10 + tier and the potion throw range 5 are not tuning (18.1, 22) |

**Client decisions (2026-11-17)**

| # | Question | Answer | Where |
|---|---|---|---|
| Q1 | Scrolls for members who cannot cast | (a) Keep the Arcana requirement; D′ rejected | 5.1, 13 |
| Q2 | An item that saves someone from dropping | (a) The Reprieve Phial; principle now, gated on death and healing | 5.2, 14 |
| Q3 | The phial's cost | (a) Scarred, the hero included, `treatProof` | 14.4, 14.6 |
| Q4 | Thrown beneficial flasks | (a) No glass damage; throwable potions at 750 | 5.3, 11 |
| — | Everything else in the proposal | Stands as written; document approved as a whole, including the lead's calls | Throughout |
| — | Q2 "1 per run" | A cap, not a guarantee; `offerChancePermille` as written (about half of runs) | 14.7, 25.1 |
| — | CN-11, CN-20 (agreed-row wordings) | Not separately asked; approved at document approval | 25.2 |

## Appendix B: sources

- The nine parent documents, all in `docs/superpowers/specs/`.
- The lead's consumables, scrolls and wands proposal (2026-11-17),
  synthesised from research, a framework, a catalogue and three critics,
  with the client's answers to Q1–Q4 on 2026-11-17 and confirmation that
  the rest of the proposal stands.
- SRD Spell Scroll (several SRD mirrors); the 2024 bonus-action potion
  rule (community rules summaries; the wording covers Potion of Healing,
  other potions unclear). Both to be re-checked against SRD 5.2.1.
- Checked game references: Baldur's Gate 3 (potions and scrolls), Battle
  Brothers (the fatal-blow perk), Wildermyth (the 0-HP choice), Slay the
  Spire (potion data), Pathfinder 2e (Trick Magic Item, Healing Bomb).
- From memory, not re-verified: 5E potion tiers, death saves, Revivify,
  Death Ward, wand charges; PF2e hero points; 13th Age recoveries; Cairn
  and Knave; Divinity: Original Sin 2; Hades; Risk of Rain 2; Shiren;
  NetHack; DCSS; Brogue; Darkest Dungeon; XCOM. Mechanism references only.
