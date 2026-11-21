# Death and Healing: Design

Date: 2026-11-17
Status: Approved, 2026-11-17
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design) and
`2026-11-17-consumables-design.md` (consumables design)

Items marked **(agreed)** were decided by the client on 2026-11-17 for
the twelve questions this design raised (Q1–Q12, appendix A), by choosing
the lead's recommendation in each case; the client also said that
everything else in the lead's proposal stands. Items marked **(lead's
call)** were filled in by the design lead; they stand unless the client
objects, and are confirmed when this document is approved. Items marked
**(settled)** restate a rule from a parent document; **(decided)** marks a
main-design client decision. Items marked **(change DH-n)** amend an
approved document and are listed, with an id, in section 26. Once
approved, every item here is binding on the builders.

This is the design every approved document has been deferring to as "the
death and healing design". It settles hit points and the level-1 bonus,
Recovery, the crit rule, what happens at 0 HP for the hero and for
companions, the healing economy and camp, temporary HP, the owner and
order of the drop-prevention effects, the Reprieve Phial's final numbers,
and the telegraphs that make permadeath fair. It also answers the
client's question (section 5): "Since the game centres on a single run,
healing items should be limited, but guaranteed in my opinion, but what
do you think? What further considerations should we have?"

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. Tuning numbers live
in a new `death.json` and in the consumables block of `inventory.json`
(20.1). The builders own them and the simulator calibrates them (section
24), except where a value is marked as a client decision or a lead's
number.

**SRD check (lead's call).** The SRD-shaped mechanisms adapted here (the
double-dice critical hit, the 2d4+2 healing potion as a yardstick, Second
Wind, Death Ward, non-stacking temporary hit points) are written from
memory. **Every SRD-shaped mechanism must be checked against the SRD 5.2.1
document before any record or tuning value is written**, and every record
flagged `srd` cites the entry it was checked against. One open point needs
that check: whether the final 2024 rules apply crit dice to every attacker
(one 2022 playtest version limited them to player weapon and unarmed
attacks). The adapted rule here (every unit crits, section 9) stands
whatever the SRD says, but the record must cite what it was checked
against.

**Waves:** M1 is the vertical slice, M2 the full run with companions, M3
profile and unlocks, Later everything after M3 that no milestone names.
**(settled, main design 9)**

## 1. Intent

Death and healing decide whether a permadeath run feels fair. The parents
settle that the run ends when the hero dies, that companions are downed
and get up after a won fight, that the first downing on a floor Scars,
that camp is a full rest between floors, that short rests are bought with
supply, and that a potion heals the drinker's Recovery. **(settled)** They
leave open how many hit points anyone has, what Recovery is, how crits
work, what a Downed body is, how much healing a floor guarantees, and how
the death-ward effects line up. This document closes those questions.

The system should make **death possible only where the player could see it
coming**: hit points high enough that level 1 is not a coin flip, healing
that is limited but never absent, one readable death path for the hero,
and every protection placed **before** 0 HP rather than in a dying state
after it. **(lead's call)**

## 2. Goals, non-goals, anti-goals

### Goals

- **One death path.** Every death goes through a damage event and the save
  chain; nothing sets a party unit's HP to 0 directly. **(lead's call)**
- **Limited but guaranteed healing.** A seeded, announced minimum per
  floor, random healing above it, and a ceiling on top. **(agreed, Q3)**
- **One healing currency.** Recovery scales potions, rests, Second Wind,
  scrolls and tier-1 heal spells alike. **(lead's call)**
- **Hidden information never kills.** Out-of-combat damage and undetected
  traps cannot take a party unit below 1 HP. **(agreed, Q6)**
- **Threat display, not hidden rolls.** A danger band, lethal tags, an
  end-turn warning and a death recap. **(lead's call)**
- **No required healer.** Healing is spread thin across classes, kits and
  items. **(agreed, Q10)**

### Non-goals

- **A downed or dying state for the hero.** **(decided, main design 4.2;
  agreed, Q1)**
- **Companion permadeath at launch.** **(agreed, Q2)**
- **Death saves, massive damage, instant death against the party.**
  **(lead's call)**
- **Passive regeneration** in or out of combat. **(lead's call)**
- **Resurrection.** Revivify and its kin stay cut (consumables 16.7).
  **(settled)**

### Anti-goals

- **Yo-yo healing:** any heal lifting a Downed unit, so a body bounces up
  and down every round. **(settled, conditions 18.2; lead's call to keep)**
- **Pity timers:** hidden mercy keyed to low HP or dry spells.
  **(lead's call)**
- **Free between-fight healing** that moves all stakes into each fight.
  **(settled, main design 4.4)**
- **A free life per floor** from a recharging drop prevention. **(agreed,
  Q7)**
- **Death by information the player never had.** **(agreed, Q6, Q9)**
- **Protected content.** **(settled)**

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Main design 4.1, 4.2 (decided) | The run ends when the hero dies; one save slot per run, deleted on death. A companion at 0 is downed and gets up on 1 HP if the party wins; a morale win counts; the first downing per floor Scars. Items that prevent a drop to 0 do not change this rule | The hero has no downed state (section 10); companions are Downed (11) |
| Main design 4.4 (decided) | No full rest inside a floor; camp is the full rest; short rests are limited by supply | HP wears down per floor, not per run (5.2) |
| Stat model 6 (agreed) | HP: fixed class amount per level + Con per level, never rolled, at least 1 per level. Recovery: HP per short rest, grows with level and Con. Natural 20 and 1 on attack rolls only | The HP formula (7), Recovery (8), crits (9) |
| Stat model 7.2 | Getting a downed companion up is trained-only Medicine | Medicine get-up (11.2) |
| Ability model 6.3 (agreed), 6.6, 6.9 | Every class has `rest` and `camp` pools, 40–60% camp; Second Wind example; level-ups at combat end | Class healing (12), level-up HP (7.4) |
| Ability model 11.3 (agreed) | Suggestions are off by default; the hero never gets suggestions | The end-turn warning names no action (17.4) |
| Ability model 15 | Short rest: 20 ticks, no aware hostile, no guaranteed safe spot (agreed); Recovery + the best trained Medicine member's Wis + proficiency; supply start 2, +1 per camp, 1–2 found, cap 3 | The rest order (11.3), the supply guarantee (5) |
| Conditions 5.4, 5.5, 9.3 | Zero-HP check; out-of-combat ticks cannot take a unit below 1; damage events and the save chain | The damage function (20.3) |
| Conditions 18.2, 18.9, 18.10 | Downed; Death-warded (Later, `combatOnly`); Bolstered (Later, no stacking); Scarred (agreed) | Sections 11, 14, 15 |
| Enemies 6.1, 6.4 | Reference hero 18 HP at level 1, +7 per level; the permadeath guards, computed with the SRD double-dice crit | The HP formula reproduces 6.1 exactly (7.1); guards (21.1) |
| Enemies 11.3 | No term reads hero status; never target or aim areas at downed units; no limited abilities in floor-1 surprise rounds, no lurkers in floor 1's first two rooms | Downed is inert (11.4); floor-1 surprise (17.1) |
| Items 5.3, 9, 15 | Supply overflow to leftovers; a Downed companion keeps its gear; the conditional companion-death rule | No new overflow rule (5.6); 15 stays dormant |
| Consumables 7.2, 10, 14 (agreed Q2, Q3) | A potion heals the receiver's Recovery; sundries; the Reprieve Phial: holder-only belt trigger, Rare, floor 3+, 1 per run, `treatProof` Scarred, final numbers owed here | Sections 12, 16 |
| Armour 14.3; trinkets 13.5, 13.7 | Last Ward (Later); Lifeline Pendant; Last-Breath Charm (Later) | Section 15 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored.

### 3.3 Findings in the parent documents

Gaps found while designing; each is fixed by a change in section 26.
**(lead's call)**

Ids PF1–PF9 (parent findings) are local to this table.

| # | Finding | Fix |
|---|---|---|
| PF1 | The reference hero (18 + 7 per level) is exactly 11 + 7L, but stat model 10 proposed the bonus "for the hero at level 1" only | The flat bonus at every level, for every party unit (7.2; DH-4, DH-9) |
| PF2 | A party that flees, or ends a fight without winning, with no trained Medicine member, leaves a companion Downed until camp | Rest and camp get-up (11.2; DH-1, DH-15, DH-21) |
| PF3 | Sundries have an offensive minimum but no healing minimum; at plain weights about 27% of floors 2–5 hold no found potion | `healingSundryMin` (5.5; DH-40) |
| PF4 | The settled substitution fallback ("supply or tonic", enemies 15.3) could replace a guaranteed draw | A guaranteed draw is never substituted (DH-27) |
| PF5 | Nothing said what a Downed body is to damage, areas or forced movement; the AI invariant had no core rule behind it | Downed is inert (11.4; DH-21, DH-28) |
| PF6 | The reference hero is not the frailest legal hero | Lowest-legal-hero guard rows (21.1; DH-26) |
| PF7 | Last Ward on a camp recharge is a free life per floor; narrowed to "above half HP" it almost never fires | Retired (15.3; DH-38, DH-45) |
| PF8 | The phial at 25% leaves a saved hero dead to the next one or two attacks most of the time | 334‰ (16; DH-38, DH-41) |
| PF9 | The review's surprise model gives the reference hero 0.87% in the floor-1 lair (main-path room 2), above the approved < 0.5% row (the lair is read at enemies 10.2 room level 2, not `mainPathIndex` 2; floor layout design 3.4, **change FL-4**) | Reconcile with the core's real surprise rule before any guard row is written (21.1); then floor-1 ambush and lair budgets and the floor-1 cumulative budget (21.1). The Q9 rule (17.1) covers rooms 0 and 1 only and does not reach this lair |

## 4. Framework decisions

The client chose Q1–Q12 on 2026-11-17. **(agreed)** The rest are the
lead's calls, which the client said stand.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Death model (Q1) | **Instant:** 0 HP ends the run; no downed or dying state for the hero; all protection comes before 0 (6) **(agreed)** | Last Stand (reverses recorded readings, a free life per floor, dead time; parked as a Later assist rung). Mortal companions (a second permadeath, cascades; parked as a Later Hard rung) |
| 2 | Companion death (Q2) | **None at launch;** Downed is the fail state, and a Downed unit is inert **(agreed)** | Third-downing loss; the Mortal rung at launch |
| 3 | Healing guarantee (Q3) | **The package G1–G6:** camp full heal; one supply cache on the main path; kits 2 / 1; 1 found potion on every floor, main path, first half, no check, never on a carrier, at most 3; the camp top-up; counts announced; random top-up leaning off-path; a pre-authorised raise to 2 on floors 4–5 (5.5) **(agreed)** | Fixed counts (removes loot texture, teaches "spend exactly N"); per-fight healing (rests become pointless); pity timers (hidden, exploitable); fully random (deaths come from the tail); rests and classes only (the hero has nothing to drink) |
| 4 | Getting up (Q4) | **A win, Medicine (once per companion per fight), a completed short rest, camp;** all on 1 HP except camp (full) **(agreed)** | Any heal lifts Downed (yo-yo); Medicine only (soft lock after a flee) |
| 5 | HP shape (Q5) | **11 + level × max(1, class HP tier + Con)** at every level for every party unit; tiers slight 6 / standard 7 / sturdy 8 (the client's "bands"; renamed here only to avoid a clash with the proficiency and encounter bands, lead's call); `hp.companionBonus` a separate key **(agreed)** | Bands 5/6/7 with a middle reference (a Con −1 light hero at 15 HP; a soldier crit is 67% of it). 5E max die at level 1 + 7 (a d6 Con −1 caster at 12 HP). Level-1-only bonus (the 6.1 table would not match). Hero-only bonus (companions become downing magnets) |
| 6 | Out-of-combat damage (Q6) | **Never below 1 HP for a party unit;** undetected traps never below 1, even in combat; spotted traps stay lethal in a fight, with a warning **(agreed)** | Traps may kill with a cap and a warning (hidden information kills); detected traps floored in combat (traps become a safe tool) |
| 7 | Death-ward owners (Q7) | **Retire Last Ward and Last-Breath Charm.** Order: Downed discard → temporary HP (per part) → floors → Death-warded → Reprieve Phial; at most one recharging prevention per unit per fight **(agreed)**. Q7 listed the steps before the chain as a group; their order among themselves is fixed by the damage function (20.3) **(lead's call; reading confirmed by the client on 2026-11-17)** | Last Ward armed above half HP (almost never fires); Last Ward per camp (a free life per floor); Last Ward once per run (kept only as the recorded fallback, now moot) |
| 8 | Phial leave-HP (Q8) | **334‰** of maximum HP after Scarred, minimum 1 **(agreed)** | 250 (a saved hero dropped by the next 1–2 attacks 36–75% of the time); 1 HP plus Dodging (only delays death) |
| 9 | Floor-1 surprise (Q9) | **The solo hero is never Surprised in floor 1's first two main-path rooms** (17.1), read as rooms 0 (the entry) and 1 (reading confirmed by the client on 2026-11-17) **(agreed)** | Guards alone (the 6.4 rows bound round-1 death but cannot make it zero for a level-1 hero in the rooms every run passes through first). The rule does not reach the floor-1 lair in room 2; that failure is closed by the surprise reconciliation and the floor-1 budgets (21.1) |
| 10 | Healer class (Q10) | **No required healer;** heals = one Recovery per target, higher tiers add targets; win-rate gap with and without a healer at most 10 points (a client number) **(agreed)** | A dedicated healer class (compulsory pick); heals sized by caster ability (breaks "scroll = potion") |
| 11 | Target death rate (Q11) | **Calibration targets:** floor-1 hero deaths under about 5% of runs; most deaths on floors 3–5 at elites and bosses; a competent player wins roughly a quarter to a third of runs (18.2) **(agreed)** | — |
| 12 | Movement undo (Q12) | **Later, in combat only,** for a move that revealed nothing and rolled nothing (17.13) **(agreed)** | At launch; out of combat (every command advances a tick and can roll noticing) |
| 13 | Recovery | **4 + 2 × level + Con, minimum 1** (8) **(lead's call)** | A share of max HP (spread about 2.3× at level 10; fires the potion fallback) |
| 14 | Crits | **S-3: double dice, every unit, attack rolls only** (9) **(lead's call)** | Maximised dice (spikier); double total (lethal at level 1); no crits against the party (two rule sets); confirmation rolls (noise) |
| 15 | Instant death | **No massive damage; no instant-death, save-or-die or "drop to 0" effect against a party unit** (10.2) **(lead's call)** | Massive damage (only the hero can die, so overkill does not matter) |
| 16 | Damage on Downed | **Inert: no damage or harmful effect from any source; only an ally's grapple-drag moves it** (11.4) **(lead's call)** | Damage on Downed kills (the only remaining sources are misclicks and hazards); never moved (contradicts approved Grappling) |
| 17 | Repeat downings | **Scarred only; the Weary lever written and off** (11.6) **(lead's call)** | Escalation on by default (untested) |
| 18 | Class healing | **Tier-1 heal = target's Recovery; higher tiers add targets, range or riders** (12.2) **(lead's call; requirement on classes)** | Recovery + caster ability (healer gap) |
| 19 | Self-heals | **Full Recovery only for a fighter-type Second Wind;** others a `rest` defensive tool at most half Recovery (12.2) **(lead's call)** | Every class a full-Recovery self-heal (doubles hero sustain) |
| 20 | Temporary HP | **Class and spell only; no stacking; a grant at most the target's Recovery** (14) **(lead's call)** | Item temporary HP (consumables denylist); stacking |
| 21 | Danger display | **Probabilistic bands by exact convolution (a normative algorithm, 17.3); known threats only; assumes every threat picks the hero; computed before any drop prevention** (17.3) **(lead's call)** | "Worst case can kill" (fires at full HP in ordinary floor-1 fights); exact % as the main display (kept in the tooltip) |
| 22 | End-turn warning | **On by default, opt-out; fires only when Deadly and some candidate from a bounded, roll-free set (17.4) would lower the band; names no action** (17.4) **(lead's call)** | Fires whenever Deadly (click-through noise); a "drink it" prompt (contradicts ability 11.3) |

### 4.1 How other games handle death and healing

Mechanism references only; no names or text are taken from any of them.
Details are as recalled unless marked "checked". Anything SRD-shaped is
checked against SRD 5.2.1 before data. **(lead's call)**

| Game | What it does | We take | We avoid |
|---|---|---|---|
| **5E SRD 5.1** | Level-1 HP = max hit die + Con, then average or roll. Hit dice spent on short rests. Death saves (3 successes or failures). Massive damage. Medicine DC 10 stabilises. Crit = damage dice twice. Potion 2d4+2. Temporary HP does not stack | Double-dice crits; the 2d4+2 potion as a yardstick; non-stacking temporary HP | Rolled HP; death saves; any heal lifting a downed creature (yo-yo) |
| **5E 2024 / SRD 5.2** | Drinking a potion is a bonus action. Second Wind 1d10 + level with uses that grow. Crit dice for every attacker (to verify) | Bonus-action potions; Second Wind's shape | — |
| **Pathfinder 2e** | Dying 1–4, raised by Wounded on every recovery. Treat Wounds (skill-gated, then immune for an hour). Hero points. Crit = beat the DC by 10 | **Wounded** as the model for escalation on repeat downings (our Weary lever) | Dying tracks on the main unit; meta currencies |
| **13th Age** | About 8 recoveries a day; one healing currency for rests, potions and rally. Death saves 16+ | **One receiver-scaled healing currency** (our Recovery) | Death saves |
| **4e** | Healing surge = 1/4 of max HP. Bloodied at half. Second Wind once per encounter. Crits deal maximum damage | Receiver-scaled healing; a visible "bloodied" mark | Max-damage crits; 1/4-of-max healing (fails our potion fallback triggers) |
| **Dungeon World** | Last Breath: a 2d6 roll and a bargain with death | — | A roll deciding death |
| **Torchbearer, OSR** | Attrition through light, food and checks; slow natural healing; legible danger; retreat is real | **Legibility matters more than harshness**; supply as the attrition clock | Very slow healing; hidden death tables |
| **Shadow of the Demon Lord** | Healing rate = 1/4 Health. At 0, fate rolls each round; damage while down kills | — | Fate rolls; damage on a downed body killing it |
| **Cairn, Mausritter** | A fast-refilling HP layer over a slow, dangerous layer | Most fights cost the cheap layer (rests) | — |
| **Lancer** | A per-mission repair budget spent on rests | Healing as a counted per-floor budget (supply) | — |
| **Slay the Spire** (potion pity checked) | Rest sites heal 30% *or* upgrade; a guaranteed rest before each boss; potion drop chance moves ±10% after each miss or drop | **Guarantee the placement of healing**, with a cost on it | Pity timers (hidden and exploitable) |
| **Hades** (checked) | Death Defiance pips; healing fountains mostly unlocked between runs and random | A visible, automatic extra life | Citing it as a guaranteed-healing precedent |
| **Darkest Dungeon** | Death's Door with a hidden deathblow roll; provisions bought ahead | Provisions as pressure | **Hidden death rolls** |
| **XCOM** | Bleed-out timer; wounds cost infirmary days; hit-chance fudging | A rescue window as a tactical problem (parked in the Mortal rung) | Hidden fudging |
| **Battle Brothers, Wildermyth** (checked) | Surviving leaves an injury or scar; in Wildermyth the player may choose it | Survival leaves a mark (our Scarred) | Permanent injuries |
| **Fire Emblem** | The main unit's death = game over; Danger Zone shows all enemy reach; rewinds in later games | **When the main unit's death ends the game, the answer is threat display** | Rewinds as the fix |
| **Baldur's Gate 3** | Any heal lifts Downed; hitting a downed unit counts as a failed save | — | Yo-yo healing; finishing off bodies |
| **Divinity: Original Sin 2** | Free full heals between fights; cheap resurrection | — | Free between-fight healing |
| **Pathfinder: WotR** | Camping supplies; knocked-out companions recover after combat on easy settings | Rest supply as the clock | — |
| **Into the Breach** | Perfect telegraphs; grid power as a shared life | **Death possible only where the player could see it coming** | — |
| **Monster Train** | One life (the pyre) plus disposable units | The hero/companion split | — |
| **DCSS, Brogue** | Brogue's generator keeps key items from being starved | **A deterministic generator floor** instead of adaptive mercy | Free regeneration between fights |

**Overall (lead's call):**

- Guarantee the **placement** of healing (camp, the floor minimum), not
  its delivery into the player's hand.
- Put a cost on guaranteed healing (supply, a rest's risk); keep random
  healing on top of a baseline.
- Extra lives that work under permadeath are visible, automatic, rare and
  leave a mark.
- When the main unit's death ends the game, the answer is **strong threat
  display**, not a hidden roll.
- Adaptive pity healing is rare and exploitable; a seeded generator
  minimum is the accepted form.

## 5. The client's question: "limited, but guaranteed"

### 5.1 The answer

**(agreed, Q3)** **Yes, with one precise meaning: guarantee availability
per floor, never delivery per fight.** Under one-life permadeath, deaths
come from the bad tail (enemies 6.4 makes the same point about
encounters). Fully random healing puts the life-saving resource in that
tail. The version that keeps the game tense is a **seeded minimum per
floor, announced, with random healing above it and a ceiling on top**.

One honest point, told to the client with the answer: guaranteed potions
are not what prevents the deaths that feel unfair. Those come from damage
spikes and surprise, and are handled by the guard rows (21.1), the
telegraphs (17), the floor-1 surprise rule (17.1) and a phial that
actually saves (16).

### 5.2 Why "per floor" is the right unit

Camp is a full rest after every floor: it restores all HP and all pools,
clears every lingering condition (Scarred included) and gets Downed
companions up. **(decided, main 4.4; agreed, Q4)** So HP is **not** a
resource that lasts the whole run; it wears down over one floor (about
15–20 minutes, 4–6 fights and a boss). What carries across the run is
coin, belt and pack items, and the hero's life. A run-wide guarantee would
add almost nothing; a per-floor guarantee is the one that matters.

### 5.3 What approved rules already guaranteed

| Guarantee | Source | Status |
|---|---|---|
| Full heal between floors | Camp (main 4.4) | Decided |
| About 2 short rests per floor | Supply: start 2, +1 per camp, 1–2 found per floor, buyable, cap 3 (ability 15.3) | Lead's call |
| Healing that refills at every rest | Every class has a `rest` pool (ability 6.3) | Agreed |
| Potions on sale | Limited pre-rolled vendor stock at every camp (agreed), illustratively 2–3 (consumables 10) | Agreed (count illustr.) |

The two rests alone give each member about **0.62–0.67× of max HP per
floor** whatever the seed: roughly 80% of the guarantee. **The gap was
healing during a fight:** a rest cannot happen in combat, Medicine cannot
help the hero, and a seed could produce a floor with no potion at all.

### 5.4 What "guaranteed" could have meant

| Option | Verdict | Why |
|---|---|---|
| (a) An exact potion count per floor | Rejected | Fair, but removes loot texture and teaches "spend exactly N" |
| **(b) A seeded minimum per floor, random above it, with a ceiling** | **Chosen (agreed, Q3)** | Mirrors the approved `offensiveSundryMin`; drawn first on the loot stream, so deterministic per seed; inside the fixed loot budget; removes the bad tail and keeps variance above it |
| (c) More guaranteed supply | A **placement** rule instead | Extra supply overflows the cap of 3; what helps is one cache always reachable on the main path |
| (d) A full heal at camp | Already decided | The strongest guarantee in the game |
| (e) A healing source in every fight | Rejected | Healing becomes a drip, rests become pointless, and the floor-long decision of main 4.4 disappears |
| (f) Pity timers | Rejected | Hidden state; exploitable (skip pickups, bleed on purpose); rewards playing at low HP; the same seed would give different loot |
| (g) A generator floor in Recovery units | A **build check** only (21.3) | Proves the guarantee at every floor and party size; too opaque for players |
| (h) Fully random healing | Rejected | Under permadeath, deaths come from the tail |
| (i) Healing only from rests and classes | Rejected | The hero has nothing to drink; a healer becomes compulsory |
| **(j) The camp top-up** | **Chosen, as part of (b) (agreed, Q3)** | The most literal and felt form of "guaranteed"; rewards drinking over hoarding, because it only issues a potion to a party that used its potions (13.2) |

### 5.5 The package

**(agreed, Q3)**

| Layer | Rule |
|---|---|
| **G1 Camp** | Full rest after every floor **(decided, unchanged)** |
| **G2 Supply** | Counts and cap unchanged. `supplyMainPathMin` 1: at least one found supply cache is in a main-path container before the boss room, needs no optional fight and no check |
| **G3 Starting kits** | **2 healing potions in every hero class kit** (floor 1 is solo with no vendor before it); **1 in every recruit kit** |
| **G4 Found potions** | `healingSundryMin` **1 on every floor**, drawn first (loot procedure, 12.5). Placed in a **main-path container in the first half of the main path, openable with no check, never on a carrier** (carriers drink what they carry). `healingSundryMax` 3 caps all potion sundries on a floor |
| **G5 Camp top-up** | The hero leaves camp with at least 1 healing potion; a dose already held by a companion is moved to the hero before a free one is issued (13.2) |
| **G6 Announcement** | The floor-entry card says "This floor holds at least 1 healing potion and 1 supply on the main path." Counts only, never locations |
| Random top-up | Further potion sundries, random containers leaning **off-path** (`healingOffPathPermille` 600 *(illustr.)*), so extra healing is earned; potions taken from carriers; limited vendor stock (coin-gated); healing scrolls (loot only); class healing; the phial |

**Pre-authorised raise (agreed, Q3):** if the report "floors where the
guaranteed potion was the only one found" fires on floors 4–5 *(illustr.:
above 30% of those floors)*, `healingSundryMin` rises to 2 there, and the
floor-entry card says 2. Room is already made for it (5.6). No further
raise without the client.

**The package's counts are client numbers (new lead's call).** The kit
counts (2 / 1), `healingSundryMin` 1 (2 on floors 4–5 only through the
raise), `healingSundryMax` 3, `supplyMainPathMin` 1 and the top-up of 1
are not tuning. `healingOffPathPermille` and `sundriesPerFloor` are.

### 5.6 How the guarantee fits approved rules

- **Supply cap 3.** The guarantee adds no supply count. Overflow already
  has a rule: excess found supply goes to the fight's leftovers marker and
  banks up to the cap when reopened; markers are scrapped at the stairs
  (items 5.3, 9). **No new rule.**
- **Sundry room.** From floor 2, 1 offensive plus 1 healing minimum use 2
  of the 3-dose low end. `sundriesPerFloor` becomes per floor, **[3–5,
  3–5, 4–5, 4–5, 4–5]** *(illustr.)*, so a raise to 2 healing on floors
  4–5 still leaves tonics, water and oil a draw. Validation
  `SundryOverfull` guards it (21.2).
- **Vendor and P1.** Quantity changes, price does not, so P1 holds at
  every Leadership discount step (worst step: potion 9, supply 6, 6000 ≤
  7200).
- **Healer classes.** The guarantee is blind to party composition. If a
  healer makes potions redundant, lower random potion weight for everyone;
  never scale the guarantee by class (18.1).
- **Companions.** A potion heals the receiver's Recovery, so it is equally
  good on anyone. Companions add HP demand after wins (they get up on 1
  HP); rests and camp pay for that, not the in-fight guarantee.
- **Hoarding.** Guaranteed potions raise the unused-at-camp count. The
  guards are the camp top-up (which rewards drinking), the unused-at-camp
  and "died with a potion on the belt" reports, and the end-turn warning
  (17.4). The suggester is **not** a hoarding guard for the hero: the hero
  never gets suggestions **(settled, ability 11.3)**.
- **Seed reshuffle.** Drawing the minimum first changes every seed's loot
  once. Acceptable before M1 content lock; logged.
- **Substitution.** A guaranteed draw is never substituted away: a narrow
  exception to the settled fallback (enemies 15.3; DH-27).
- **Stream order.** The loot stream never alters the floor plan (enemies
  10.5). Main-path containers with no check are a **constructive
  dungeon-generation requirement**: the dungeon stream reserves them when
  it lays out the main path, a run-time assertion checks them, and a
  failed attempt is regenerated by one fixed retry policy (12.4 HE-7;
  DH-32). There is no loot-time insertion.

### 5.7 What "limited" means here

**(lead's call)** **Supply, not potions, sets the healing volume.**

- At party size 3–4 a potion is about 8–10% of the party's HP pool, while
  a rest restores about a third of **every** member.
- Supply sells without limit at 8 coin, and the cap limits holding, not
  spending. A party can leave camp with 3, spend them, and find 1–2 more:
  **4–5 rests per floor** against a target of about 2. Only patrol risk
  during the 20-tick rest holds that back.
- So "limited" is enforced on supply. A rests-per-floor report has a gate
  of median ≤ 3 *(illustr.)*. Lever order if it fires:
  1. found supply 1 per floor instead of 1–2 (a lead's call row);
  2. limited vendor supply stock of 2–3, like potions (changes the agreed
     vendor row, so it goes back to the client);
  3. supply cap 2.
- A higher supply price is **not** a lever: the P1 peg (potion healing per
  coin at least 20% dearer than supply) would fail.
- **What potions are for:** in-fight tempo for the hero. One bonus-action
  drink undoes about 1.3–1.5 guarded enemy turns.
- **What else stays limited, unchanged:** limited pre-rolled vendor
  potion stock (agreed), illustratively 2–3; the P1 peg; belt slots and
  `stackMax`; `healingSundryMax`; no healing scrolls through the camp
  channel; no passive regeneration.

### 5.8 Further considerations

The client asked what else to consider. **(lead's call)**

1. **The guarantee does not prevent the unfair deaths.** Spikes and
   surprise do. The levers are the lowest-legal-hero guard rows (21.1), the
   danger band (17.3), the floor-1 surprise rule (17.1) and the phial at
   334‰ (16).
2. **The solo floor-1 boss is the choke point.** Floor-wide totals look
   generous (about 2.2× max HP on floor 1), but rests cannot help inside a
   boss fight. Reference level-3 hero: 32 HP, Recovery 11; a boss at the
   approved 35%-per-round row for 5–7 rounds deals 56–78. The hero arrives
   at about 70% (22 HP) holding 2 kit potions, 1 found potion and possibly
   a self-heal: 55–66 HP of effective life. Over 6 rounds that needs 67,
   so the approved 35% row fails **even with** a full-Recovery self-heal.
   Hence the floor-1 solo boss gets its own, lower expected-damage row of
   ≤ 25% per round (21.1, DH-26), settled now rather than left as a lever,
   and a sustain check (`Floor1BossSustain`, 21.1 step 6) whose inputs are
   calibrated by the simulator's boss-door reports **(lead's call)**. The
   answer is **not** more potions and **not** a limit on drinking.
3. **Potion-chugging at later bosses.** A potion (31–33% of hero HP)
   matches the boss guard's 35% per round, and the belt holds up to 6
   doses. A report watches potions drunk per boss fight; a prepared lever
   exists (12.3) but is off, and never applies to the floor-1 solo boss.
4. **Class sustain.** If every class had a full-Recovery self-heal, the
   hero's floor healing would roughly double (about 3.5× max HP at floor 3
   against a need of 1.0–1.5×). Hence only the fighter-type Second Wind
   heals a full Recovery (12.2).
5. **Mid-fight Medicine as a free decoy.** Hence once per companion per
   fight **(agreed, Q4)** (11.2).
6. **Heal spells as the biggest source.** Slot Recovery returns a slot per
   rest. Hence heals sized at Recovery and scaled by targets (12.2).
7. **Level-ups heal.** About +14 HP per member per floor arrives through
   level-ups at combat end (ability 6.9). Reported, never counted in the
   guarantee.
8. **Protecting the hero.** The AI never reads "is the hero" (enemies
   invariant 1). Companion kits need protector tools (guard, intercept,
   body-block) so the player can spend actions keeping the hero alive: a
   requirement on the companions and classes designs (27).
9. **Late deaths cost the most playtime.** A floor-5 death throws away up
   to 2 hours. A legacy hook (a fallen hero's notable as a future heirloom
   candidate, items 14) is noted as a Later idea.
10. **Difficulty ladder.** Harder tiers keep the same healing guarantee;
    the ladder raises budgets only.

## 6. The death model

### 6.1 The three models

| Model | In one line |
|---|---|
| **Instant** | The hero dies at 0 and the run ends. Companions are Downed, never killed. All protection comes **before** 0 |
| **Last Stand** | At 0 the hero is Downed with a visible 2-turn countdown once per floor (if an ally stands). Any ally may Rescue; the hero gets up on its Recovery with Scarred. A second drop, an expired countdown or no ally standing kills |
| **Mortal companions** | The hero dies at 0. A companion at 0 starts a visible Dying clock (3 of its turns; 1 on a repeat downing; dies at once on a third downing in a fight). Universal Stabilise stops the clock. A dead companion is gone; the next camp guarantees a recruit |

### 6.2 Verdict and judges' scores

Three independent judges scored the three models, 0–10 per criterion
(higher is better), shown as judge 1 / 2 / 3; totals are the mean of the
three judges' sums, out of 70:

| Criterion | Instant | Last Stand | Mortal |
|---|---|---|---|
| Tension and stakes | 8 / 8 / 8 | 5 / 5 / 5 | 9 / 9 / 9 |
| Fairness | 9 / 8 / 8 | 8 / 7 / 7 | 7 / 7 / 7 |
| Low frustration | 8 / 7 / 8 | 7 / 8 / 7 | 5 / 5 / 5 |
| Decision quality | 8 / 8 / 8 | 7 / 7 / 7 | 9 / 8 / 8 |
| Fit with approved docs | 10 / 9 / 9 | 3 / 3 / 3 | 5 / 5 / 4 |
| Low build cost | 8 / 8 / 8 | 5 / 5 / 5 | 4 / 5 / 3 |
| Fit with 1–2 hour runs | 9 / 8 / 9 | 7 / 6 / 6 | 6 / 6 / 5 |
| **Total (mean)** | **58** | **41** | **43.7** |

All three judges picked Instant.

**Verdict: Instant.** **(decided, main 4.2; agreed, Q1)**

- It matches the decided rule and **every recorded reading of it**:
  consumables 5.2 ("the hero has no downed state"), conditions 18.2 ("for
  the hero, the run ends"), conditions 18.10, the trinkets Lifeline note,
  and the 6.4 guards' "reduced to 0".
- One death path and one readable fail state; no dead time with the main
  character inert; no hidden roll.
- Fairness moves to **before** 0: a higher HP floor, guards checked at the
  lowest legal hero, probabilistic telegraphs, lethal previews and the
  phial.

**Its weaknesses, with their answers:** companions can be played as
ablative units (Medicine once per fight, a ready Weary lever, downing
reports); non-lethal out-of-combat damage removes some tension (accepted:
hidden information is highest there).

### 6.3 What each model would have changed

| Row | Instant (chosen) | Last Stand | Mortal |
|---|---|---|---|
| Main 4.2 (decided) | Wording kept; adds the rest and camp get-up and "never killed" (Q2, Q4) | Changes the recorded reading "no downed hero" | Adds the Dying clock, Stabilise, death, a guaranteed recruit |
| Consumables 5.2, 14 (agreed Q2) | Order kept with Last Ward removed (Q7); leave-HP 334 (Q8) | The order changes; trigger becomes would-die | Phial gains a would-die hook |
| Conditions 18.2 Downed (settled) | Routes added (rest, camp); Medicine once per fight; inert | The Downed row changes for the hero | Rewritten (clock, Stable, death) |
| Items 15 (settled + conditional) | Conditional rule stays dormant | — | Conditional rule activates |
| Ability 7.2 | Medicine row narrowed | New Rescue action | New Stabilise action |
| Enemies 10.1 (budgets) | Unchanged | Unchanged | Cascade risk |

### 6.4 Parked as Later rungs

**(agreed, Q1, Q2; details lead's call)** Neither is designed further
here.

- **Last Stand → a Later assist or "Story" rung.** Once per run
  (`lastStand.scope = run`), a deterministic countdown with no death
  saves, free before the phial. It does nothing on solo floor 1, so it is
  not a launch safety net.
- **Mortal companions → a Later "Hard" rung.** A visible clock, universal
  Stabilise, a repeat downing shortening the clock, a new AI invariant (no
  scoring term reads Dying, Stable, downings or distance to a body), the
  Fallen list and the guaranteed recruit. Choosing it turns on items 15's
  conditional loot rule and the phial's would-die coverage.

## 7. Hit points

### 7.1 Formula

**(agreed, Q5; change DH-4)**

- **Max HP = `hp.adventurerBonus` 11 + level × max(1, classHp + Con).**
- `classHp` comes from the class's **HP tier**: slight 6, standard 7,
  sturdy 8 (`hp.classTiers`). The classes design assigns each class a
  tier. The client's Q5 answer calls these "bands"; this document says
  "HP tier" so they are not confused with the proficiency bands (b1–b3),
  the encounter bands (enemies 10.1), the armour categories or the danger
  band (17.3) **(lead's call, a name only)**.
- Never rolled; at least 1 per level **(settled, stat model 6)**.
- **Companions** use the same formula with `hp.companionBonus` 11, a
  separate tuning key with the same default. It is the lever if companions
  run too durable for their party weight (8 against the hero's 15).
- **Monsters** keep their table HP (enemies 6.2); no bonus.

**The reference hero is tier 6 with Con +1:** 18, 25, 32 … 81, the
enemies 6.1 row exactly, so no approved table moves (DH-25).

| Level | Reference (6, +1) | Lowest legal (6, −1) | Standard (7, +2) | Sturdy, max Con (8, +3 → +5) |
|---|---|---|---|---|
| 1 | 18 | 16 | 20 | 22 |
| 2 | 25 | 21 | 29 | 33 |
| 3 | 32 | 26 | 38 | 44 |
| 5 | 46 | 36 | 56 | 71 |
| 7 | 60 | 46 | 74 | 95 |
| 10 | 81 | 61 | 101 | 141 |

**The reference is not the frailest legal hero.** The lowest legal hero
is tier 6 with Con −1 **and** the lowest legal party AC under AR-20,
11–25% lighter than the reference. It gets its own guard rows (21.1).

**Client numbers (new lead's call):** `hp.adventurerBonus` 11 and the
tiers 6 / 7 / 8 are not tuning; moving them goes back to the client with
the deaths-by-HP-tier report. `hp.companionBonus` is tuning, starts at 11 and
may only be lowered.

### 7.2 The level-1 bonus

- Stat model 10 recorded the lead's proposal as "a flat hit point bonus
  for the hero at level 1", and enemies 25 asked for "hero HP at level 1
  of about 18".
- **The bonus applies at every level and to every party unit (agreed,
  Q5).** The reference row (18 + 7 per level) is exactly 11 + 7L; a
  level-1-only bonus would not reproduce it at later levels, and a
  hero-only bonus makes level-1 to level-3 companions downing magnets.
- **Effect on stat model 5.3** ("class supplies most hit points"): at
  level 1 the flat 11 exceeds the class's 6–8. From level 2 on, class + Con
  supply most HP; at level 10 class supplies 60 of the reference's 81. The
  5.3 sentence gains "over a run" (DH-7).
- **Honest cost:** the flat bonus compresses class identity. A sturdy class
  has about 25% more HP than a light one at level 10 with equal Con (5E is
  closer to 2×). Con +5 sturdy builds reach 141 against the reference 81,
  so reference-tuned content may feel soft for tanks. Report: deaths by
  HP tier (21.5).

### 7.3 State

**(lead's call)**

- Each unit stores current HP; the maximum is derived on demand
  **(settled, nothing derived is stored)**.
- A level-up (at combat end, ability 6.9) adds the increase in maximum,
  Δ = derived maximum after − derived maximum before, both computed with
  the unit's current conditions (Scarred included), to current HP, so only
  the increase arrives. **A Downed unit gains no HP from a level-up:** its
  HP stays 0 and it later gets up on 1 (combat-end order, 20.5)
  **(lead's call)**. Invariant: a Downed unit always has current HP 0.
- Every other change to the maximum only clamps current HP. Scarred
  clamps it (conditions 18.10); ending Scarred restores the maximum and
  heals nothing.

### 7.4 Level-up HP

The increase arrives as above. It is not a healing source: it is reported
(`levelUpHp`) and never counted in the guarantee. **(lead's call)**

## 8. Recovery

**(lead's call; change DH-5)** Needed before M1 potion data. The shape is
fixed; 4 and 2 are **lead's numbers**: not tuning for the builders, moved
only by the lead on report evidence and always re-checked against the
consumables 7.2 fallback triggers **(lead's call)**.

**Recovery = `recovery.base` 4 + `recovery.perLevel` 2 × level + Con,
minimum 1.** It replaces `recoveryPlaceholder` (consumables 7.2), which
had the same shape and came out 1 lower; the placeholder is replaced, not
tuned (DH-39).

| Level | Reference (Con +1) | % of reference HP | Con −1 | Con max | Max/min | Rest with Medicine (Wis +2, trained) |
|---|---|---|---|---|---|---|
| 1 | 7 | 39% | 5 | 9 | 1.80 | 11 (61%) |
| 3 | 11 | 34% | 9 | 13 | 1.44 | 15 (47%) |
| 5 | 15 | 33% | 13 | 18 | 1.38 | 20 (43%) |
| 7 | 19 | 32% | 17 | 22 | 1.29 | 24 (40%) |
| 10 | 25 | 31% | 23 | 29 | 1.26 | 31 (38%) |

**The fallback triggers of consumables 7.2 pass:** a level-1 potion heals
5–9, inside 4–10; the largest heal over the smallest at equal level is at
most 1.8, under 2. `potionPermille` stays 1000. Recovery reads neither
the class HP tier nor Scarred.

**Uses of Recovery:**

- a short rest heals Recovery plus the best trained Medicine member's Wis +
  proficiency **(settled)**;
- a potion heals Recovery, excluding the Medicine bonus **(settled)**;
- a healing scroll heals the same as a potion (P1);
- a fighter-type Second Wind heals Recovery, excluding the Medicine bonus;
- a tier-1 heal spell heals the target's Recovery (12.2).

**Medicine rest bonus.** At level 1 it is about +4, 57% of Recovery,
which makes Medicine training close to compulsory for the solo floor-1
hero. Measures: the bonus stays out of the guaranteed healing floor; a
report compares floor-1 hero deaths Medicine-trained against untrained;
reserve lever (a lead's call row): bonus = FloorDiv(Wis + prof, 2) + 1.
A further reserve if the Con-is-a-tax report fires: `+ Con × (prof − 1)`.

## 9. Critical hits

**(lead's call; S-3 adopted; change DH-6)**

- A natural 20 on an **attack roll** always hits and crits; a natural 1
  always misses. Saves and skill checks have no automatic results
  **(settled, stat model 6)**.
- **A crit rolls all damage dice twice, for every unit, party and monsters
  alike.** Exploit dice double with the weapon dice; flat terms and
  Precision do not (weapons 15.1). A flagged rider is attempted on a crit
  (stat model 8.2, unchanged).
- **Why it is safe at low HP:** monster at-wills roll at most one die
  (enemies 5.4), so a crit adds at most one die; double-dice crits shave
  only 3–7% off expected turns-to-0, so the effect sits in the tail, where
  the 6.4 guards already account for it; every guard, armour Proof and
  Adamant, and weapon parity already assume this rule.
- **Lead's check before data:** the SRD 5.2.1 wording (header note).

## 10. The hero at 0 HP

### 10.1 The run ends

**(decided, main 4.2; agreed, Q1)** The hero has no Downed or dying state.
At 0 HP the run ends: the core finishes the killing damage event's
bookkeeping, emits `RunEnded{cause: heroDeath, killingEventId}` as its last
event and rejects every later command (20.3 step 7). The **host**, not the
core, deletes the save slot when it receives that event **(decided, main
4.1; mechanics lead's call)**.

### 10.2 One death path

**(lead's call)**

- Nothing sets a party unit's HP to 0 or kills it directly. Every death
  goes through a damage event and the save chain (15).
- **Content denylist:** no instant-death, save-or-die or "drop to 0"
  effect may target a party unit. Validation `PartyInstantDeath`.
- **No massive-damage rule.** Only the hero can die, so overkill does not
  matter.

### 10.3 Simultaneous death

**(lead's call; change DH-15, DH-30)** If the hero reaches 0 in the same
event that kills the last enemy (a Volatile burst, for example), the
hero's death wins and the run ends. The burst is in-combat damage,
because `combatActive` stays true until the combat-end procedure has run
(20.5), so the out-of-combat floor never applies to it. The lethal tag on
the hero's **own** attack reads "killing this triggers a burst that is
lethal to you" (Volatile kill preview, enemies 14).

### 10.4 Out-of-combat and trap floors

**(agreed, Q6; change DH-15, DH-18)**

- Damage to a **party unit** that resolves while **`combatActive` is
  false** (20.5) cannot take it below 1 HP. This covers traps, hazards,
  falls, the party's own areas and ticks.
- **"Out of combat" is judged when the damage event resolves, by reading
  `combatActive` (lead's call).** An ability use whose targets or area
  include a hostile unit, or that is noticed, runs the combat-start step
  before it pays its cost and resolves, so an ambush's opening strike is
  in-combat damage, covered by the 6.4 round-1 rows. A party area used out
  of combat that includes no hostile unit and is not noticed (one opening
  a door, say) starts nothing: its damage to a party unit is floored. This
  closes the free-damage loophole.
- **Undetected traps:** damage from a trap the party had not detected when
  it fired cannot take a party unit below 1 HP, **in or out of combat**. A
  trap that starts combat counts here.
- **Trap provenance (lead's call).** When a trap fires, every damage event
  it causes carries `source {kind: trap, trapId, detectedAtFire}`, a
  snapshot. The undetected-trap floor covers every event carrying a
  `trapId` with `detectedAtFire` false: the trap's direct damage,
  collision damage from its push (to the pushed unit only), and the tick
  parts of condition instances it applied, which store
  `sourceTrapUndetected` for their lifetime. Detecting the trap later does
  not change a snapshot. Terrain or hazard squares the trap creates are
  ordinary hazards once visible, and a monster pushed into a trap gets no
  floor (the floor is party-only).
- **Mixed events (lead's call):** in an event or tick batch that holds both
  undetected-trap parts and other parts, the floor protects against the
  trap parts only. If the other parts alone would take the unit to 0, the
  event is not clamped; otherwise it is clamped so HP stays ≥ 1.
- **Detected traps in combat** are ordinary damage: lethal, and shown with
  a lethal tag on the move preview (17.6). Hidden information never kills,
  and a known trap is never a safe tool.
- **Falls: the FallFloor (agreed, Q6 extended; hazards Q2; change HZ-10;
  M2 (gated), else M3).** Fall damage (a Dreamfall's fall band, and the
  fall band of a later Void failure that leaves the unit on the edge)
  can never take a party unit below 1 HP, **in or out of combat**
  (hazards design 6.4, `2026-11-21-hazards-design.md`).
- **Stir acts: Warned or floored (agreed, Q6 extended; hazards Q1;
  change HZ-14; M2 (gated), else M3).** A Stir act cannot take a party
  unit (hero or companion) below 1 HP unless the unit is **Warned** for
  that act: it took an unlost turn after the announcement, not under
  hard control, ended it on a due square of that Stir having entered by
  its own movement or with a legal move out of the footprint available,
  and has not been moved since. Every other case is **floored**. The
  flag is computed at the unit's turn end and shown (17.5); monsters get
  no floor (hazards design 6.3.2).
- **Monsters keep the existing tick rule** (harmful ticks out of combat
  cannot take any unit below 1, conditions 5.5). The new floor is
  party-only.
- Conditions 10.4 ("no single failed save or entry is lethal to the hero")
  stays as a hazard-design guideline under the new floor. Trap content is
  owned by dungeon generation (conditions 11.6). Moving an ally into a
  hazard square or over a hazard edge asks for confirmation (17.7;
  Arcanist design 7.14, ARC-25).
- **Dare rooms (M3; change HZ-21).** An unguarded Dare room's Stir runs
  per out-of-combat tick, so its damage to the dare-taker is out of
  combat and this floor applies; a taker reduced to 1 HP by the room is
  returned to the threshold and the dare resets (hazards design 6.10).
- The floors apply after temporary HP and **before** the save chain
  (20.3), so the phial is never spent on floored damage.

### 10.5 Protection comes before 0

The HP formula and the lowest-legal guard rows (7, 21.1); the 6.4 guards;
the floor-1 surprise rule (17.1); the telegraphs (17); the phial (16).

## 11. Downed companions

### 11.1 The state

**(decided)** At 0 HP a companion is Downed. Downed implies Incapacitated,
gives Prone on apply, suppresses harmful turn-counted conditions, ends
concentration and keeps its initiative slot **(settled, conditions
18.2)**. A companion is never killed at launch **(agreed, Q2)**.

### 11.2 Getting up

**(agreed, Q4; change DH-1, DH-8, DH-11, DH-15, DH-21, DH-36)**

| Route | Result |
|---|---|
| A won fight, morale win included **(decided, main 4.2; M-1)** | Up on 1 HP |
| Trained Medicine, an action (Lifeline Pendant: a bonus action, once per fight) **(settled route)** | Up on 1 HP, Prone. **At most once per companion per fight** (Lifeline counts); a second attempt is greyed `AlreadyGotUpThisFight`. **Reach the Fallen** (Hearth L6 option): the get-up reaches 2 squares, and the same action also Treats that companion (only when taken as an action, never through the Lifeline bonus-action variant); still once per fight, up on 1 HP, Prone unchanged (Herald design 9.2) **(touches agreed Q4; change HR-28)** |
| **A completed short rest** (out of combat) | Up on 1 HP, then the normal rest order (11.3) |
| **Camp** | Up, full heal (13.1) |

- **Why the rest route.** Without it, a party that flees, or ends a fight
  without winning, and has no trained Medicine member leaves the companion
  Downed until camp: a soft lock for the rest of the floor.
- **Why once per fight.** A Medicine get-up leaves the companion on 1 HP
  and Prone. The kill bonus (enemies 11.1) reads threat, not HP, so a
  well-kitted 1-HP companion becomes a top target, and after the first
  Scarred a re-downing costs nothing. Unlimited get-ups would make one
  Medicine action soak about 0.6–0.8 of a potion's worth of enemy damage,
  free: the cheapest healing in the game.
- **Medicine out of combat (lead's call).** A Medicine get-up out of
  combat (after a flee, say) is allowed: an action, on 1 HP, setting
  Scarred on the first downing of the floor. It does not increment
  `medicineGetUpsThisFight`, which is read only while `combatActive` is
  true and resets at combat start.

### 11.3 Order at rest completion

**(lead's call; change DH-12)** Get up → Treat attempts → Recovery +
Medicine bonus → `rest` pools reset → Slot Recovery. A fresh Scarred from
this get-up can be Treated at the same rest.

### 11.4 A Downed unit is inert

**(lead's call; change DH-21, DH-28)**

- It takes no damage: damage events against it are discarded and logged
  `DamageDiscarded{downedInert}`.
- It gains no new harmful condition, and never gains temporary HP.
- **Forced movement:** only an ally's grapple-drag moves it **(settled,
  conditions 18.2: "Willing or downed allies are grabbed with no check")**;
  a drag is forced movement, so the party can move a body in combat. Every
  other forced movement, hostile, environmental or allied, skips it.
  Areas from either side skip it. Hazards, terrain and ticks do nothing to
  it.
- **Party targeting:** a Downed ally is an illegal target for any party
  ability or item use that deals damage, forces movement (other than the
  grapple-drag) or applies a harmful condition: it is greyed on the cursor
  (`TargetDownedInert`), not resolved and discarded. Get-up actions,
  Treat, Take from a Downed ally and a tonic Administer (minor only,
  items 7.1) stay legal.
- **Square occupancy:** a Downed unit does not block movement. Any unit
  may move through its square but may not end its move there. This avoids
  an invulnerable wall at a chokepoint and a body blocking the party's own
  corridor.
- **A body is not a unit for forced-movement stopping (lead's call).** A
  push or pull passes over its square with no collision; if the move would
  end on that square, the mover stops on the last legal square before it,
  again with no collision. A landing (conditions 10.3) or an Engulfed
  expulsion uses the nearest free square, skipping Downed squares, in the
  fixed square order (ability 8.1).
- This makes enemies invariant 2 ("never target or aim areas at downed
  units") a **core rule** as well as an AI rule, and covers friendly fire.
- **After a win** the companion gets up where it lies (11.2). **After any
  other combat end** (a flee by breaking line of sight, say) the body
  stays on its square **(lead's call)**. A party unit that ends a move
  adjacent to it out of combat picks it up as a carried follower (one
  free interaction). A carried body moves with the party out of combat.
  Items 15 already keeps it in the party with its gear.
- **A body left behind (lead's call):** the rest route still applies to
  it (Q4 is unchanged: a completed short rest gets up every Downed
  companion, on 1 HP, where it lies), but the settled rest condition "no
  aware hostile" reads every party unit, the body included. So a party
  cannot rest while aware enemies stand over a body it ran from. At the
  stairs every Downed companion goes with the party wherever its body lies,
  so a companion is never lost **(agreed, Q2)**. The party bar shows where
  each body lies.
- **At combat start,** a carried body is placed Prone on the free square
  nearest its carrier, in the fixed square order (ability 8.1)
  **(lead's call)**.

### 11.5 What never lifts Downed

**(settled, kept)** Potions (drunk, thrown or Administered), healing
scrolls, tonics, heal spells and the phial. `liftsDowned` is a reserved
data flag: no record may carry it (validation `LiftsDownedUnapproved`)
until the classes design proposes one and the client approves it.

### 11.6 Scarred, repeat downings and the party

**(lead's call unless marked; change DH-23)**

- **Scarred by route:** every get-up (Medicine, Lifeline, a win, a rest,
  camp) sets Scarred if this was the first downing on the floor; the same
  rest's Treat may then remove it, and at camp, step 3 (13.1) clears it at
  once. So a camp get-up leaves no Scarred, and the parent texts that
  grant Scarred on a get-up from a first downing (main 4.2, conditions 14
  and 18.2) stay true as written **(lead's call)**.
  Conditions 18.10's "by either route (Medicine or a won fight)" becomes
  "by any get-up route" (DH-23).
- **Repeat downings:** Scarred on the first downing per floor stays the
  only cost. **The Weary lever is written and off**
  (`downed.repeatDowningWeary` false): when on, a companion that gets up
  from its second or later downing on the same floor gains Weary 1, exempt
  from the lingering cap of 2 as Scarred is (conditions 14). Switch it on
  if the downings-per-fight report shows companions used as ablative
  tanks.
- **The costs of a downing:** lost turns, a Medicine action, Scarred, a
  weaker boss fight. Items 15's conditional loot rule stays dormant; the
  phial needs no would-die coverage **(agreed, Q2)**.
- **Party wipe:** only the hero's death ends the run. With every companion
  Downed, the hero fights on or flees with them.

## 12. Healing sources and the economy

### 12.1 Sources

**(lead's call unless marked)**

| Source | Job | Rule |
|---|---|---|
| **Camp** | Reset between floors | Section 13 **(decided)** |
| **Short rest** (1 supply, 20 ticks, no aware hostile, no guaranteed safe spot) | The floor's main healing | The order of 11.3 **(settled shape)** |
| **Healing potion** | In-fight tempo; the hero's lifeline | Recovery, bonus action; Hand over, Administer, or throw at 750‰ **(agreed, consumables Q4)**; never on a Downed unit |
| **Fighter-type Second Wind** | Self-sufficiency | **Regain your Recovery**, `rest` pool (DH-10). The 1d10 + level example drifts from 36% of HP to 19% |
| **Other classes' rest tool** | Self-sufficiency, solo floor 1 | A `rest`-pool defensive tool tagged `restDefensiveTool: true` (exactly one per launch class): a self-heal of at most FloorDiv(Recovery, 2), or an effect kind on the approved defensive list (21.2), inside the 40–60% band of ability 6.3. **Requirement on the classes design.** Binds class records, not companion kits (companions design 8.1; change CMP-24) |
| **Class heal spells** | Optional healer power | **Tier 1: the target's Recovery, exactly.** Higher tiers add targets, range or riders, never more HP per target; a declared upcast entry may add nothing (Mending Word targets 1 / 1 / 2 / 3, its tier-2 entry adds no target; the cast's tier is the slot spent; Herald design 8.1, a reading; change HR-6). `rest` or `camp` pools only; never at-will; never on wands |
| **Healing scroll** | A potion bought with a camp spend | One cast of the tier-1 heal = a potion's heal (P1 holds); loot only, never the camp channel |
| **Medicine** | Getting up, Treat, the rest bonus | Trained only for getting up **(settled)**; once per companion per fight **(agreed, Q4)**; "stretch supplies" stays the builders' choice and must never mean free rests |
| **Hungering, Regenerating** | Small extras | As designed; Withered blocks both |
| **Level-ups** | Not a healing source | 7.4 |

- **No passive regeneration,** in or out of combat. Exploration has no
  clock, so any per-tick regeneration would be infinite healing.
- Using limited heals out of combat is allowed, because it spends pools.
- **Monsters:** no healing out of sight (enemies invariant 8). A monster's
  potion heals 25% of its max HP **(settled, consumables 7.2)**.
- **Pending request (change HZ-30; M3; the DH owner rules; not a source
  until ruled):** the Forbidden Course, a party-only environment heal in
  the Long Supper (an Operate on a covered plate, ≤ 2 per fight, half a
  potion's heal plus Poisoned) (hazards design 7.8).

### 12.2 Why heal spells scale by targets

**(lead's call; agreed in principle, Q10)**

- A level-5 full caster with Slot Recovery casts about 8–9 slot spells
  per floor. At "Recovery + ability" (19 at level 5), half of those spent
  on tier-1 heals is about 76 HP: 1.65× a 46-HP member's maximum, and
  2.5–5× the floor's guaranteed potion healing.
- "Each tier adds half Recovery" would make a tier-3 heal 57% of max HP.
- Sizing at exactly Recovery keeps "a scroll equals a potion" true and the
  healer gap modest. One launch class may lean on healing; none is
  required **(agreed, Q10)**. Validation `HealPerTargetAboveRecovery`.

### 12.3 Boss potion lever (prepared, off)

**(lead's call)** `potion.laterInFightPermille` 1000. If the boss-potion
report fires, set it to 500: the second and later potions **the same
unit** drinks in one fight heal FloorDiv(Recovery, 2), shown on the heal
preview. Never applies to the floor-1 solo boss. No hidden diminishing
returns. Not on at launch: the floor-1 boss needs 3–5 heals (5.8).

### 12.4 The guarantee's rules

**(agreed, Q3, for the package; keys and placement detail lead's call)**

- **HE-1 Kits.** `heroKitPotions` 2 on every hero class kit;
  `recruitKitPotions` 1. Kit potions sit on the belt.
- **HE-2 Floor minimum.** `healingSundryMin` [1, 1, 1, 1, 1] by
  floor, drawn **first** among the sundries (before `offensiveSundryMin`;
  the exact draw order is 12.5), inside `sundriesPerFloor` and the fixed
  budget. `healingSundryMax` 3 counts every healing potion sundry of the
  floor, in containers and carried.
- **HE-3 Placement.** The guaranteed potion goes in a main-path container
  in the first half of the main path (12.5), openable with no check, never
  on a carrier, and is never substituted. Random potion containers lean
  off-path (`healingOffPathPermille` 600).
- **HE-4 Supply placement.** `supplyMainPathMin` 1, a main-path
  container before the boss room, reachable with no optional fight and no
  check. Counts, cap and "no guaranteed safe spot" **(agreed)** are
  unchanged. Overflow follows items 5.3 and 9.
- **HE-5 Camp top-up.** `campHeroPotionTopUp` 1 (13.2).
- **HE-6 Healing floor.** A build check (21.3).
  *Pending request (change HZ-30; M3; the DH owner rules):* how the
  Forbidden Course's environment heal is counted in the floor's healing
  supply (`HealSupplyPerFloor`, as the hazards design names it; 7.8).
- **HE-7 Generation requirement (constructive).** When the dungeon
  stream lays out the main path (enemies 10.5 step 1a; floor layout
  design 6.1, LS11) **(change FL-3)**, it **reserves** at
  least 2 containers openable with no check in main-path rooms before the
  boss room, at least one with an index in the first half (12.5). A
  run-time assertion checks the plan before the loot step; if it fails,
  the attempt is discarded and the floor is regenerated once with the
  dungeon stream reseeded at seed + 1, logged `GenerationRetried`; a
  second failure is a crash bug, never a silent fallback **(lead's
  call)**. The floor layout runtime assertions run with this check at
  one floor assertion point before the loot step and share its single
  retry, which re-derives every child stream of the dungeon substream;
  only assertions proven structurally at build time may trigger it, and
  there is no other retry policy (floor layout design 6.1, 11.2)
  **(change FL-15)**. Build-time checks cover every room template and the release
  seed set (21.2); they cannot prove every seed, which is why the
  requirement is constructive (DH-32).
- **M1 slice:** hero kit 2, plus 1 guaranteed main-path container potion
  and `supplyMainPathMin` 1 on floor 1, so the floor-1 HE-6 cell and the
  floor-1 boss check rest on what M1 ships. Sundries proper, the offensive
  minimum, recruits and the camp top-up arrive in M2.

### 12.5 The loot-stream procedure for the guarantee

**(lead's call; change DH-27)** Within enemies 10.5 step 3, the sundry and
found-supply block runs **after** the per-channel notable items
(containers, enemy-carried, boss) and **before** the wand charges. Every
draw count below is fixed per item, whatever the outcome, so seeds stay
stable.

Definitions: the main path's rooms have indices 0 … n − 1, with 0 the
entry and n − 1 the boss room. n counts `mainPathIndex` 0 (the entry)
to n − 1 (the arena), the floor-1 antechamber (RS 6.6's extra room)
included; the Bypass arm, Alcoves, corridors and the Stairhead are not
counted rooms, and "room k" means `mainPathIndex` k (floor layout design
3.4, 6.3) **(change FL-4)**. A room is in the **first half** when its
index < FloorDiv(n − 1, 2). An **eligible guaranteed container** is a
container in a main-path room, openable with no check, never a carrier.
A container holds any number of items.

1. **Phial pre-roll** (run level, unchanged, consumables 14.7): once per
   run, before floor 1's draws.
2. **Counts:** one draw for the found supply count, 1–2 (ability 15.3),
   then one draw for the floor's sundry count within its
   `sundriesPerFloor` range.
3. **Guaranteed potions:** for each of the floor's `healingSundryMin`
   potions, one draw chooses uniformly among eligible guaranteed
   containers in the first half. A second potion (floors 4–5 after the
   raise) may share the first's container.
4. **Guaranteed supply:** one draw chooses uniformly among eligible
   guaranteed containers with index < n − 1 that are HE-4-eligible:
   never in the bypassed guard post's (Gb) room or a Circuit room, the
   floors 2–5 Forecourt included, so G2's "no optional fight" holds on
   every seed (floor layout design 6.5) **(change FL-3)**; excluding a container that
   already holds a guaranteed item when another exists.
5. **Second found supply:** one draw, always; when step 2 gave 2, it
   chooses uniformly among all containers of the floor (when step 2 gave
   1, the draw is consumed and ignored).
6. **Offensive minimum:** `offensiveSundryMin`, as approved (consumables
   10).
7. **Remaining sundries,** the sundry count less the doses of steps 3 and
   6, three draws each, always: (a) the record, weighted; once the floor's
   healing potion sundries (containers plus carried potions already drawn
   this floor) reach `healingSundryMax`, the potion weight is 0; (b) the
   side, one draw against `healingOffPathPermille`: off-path or main path,
   used only when (a) chose a healing potion, falling back to the main
   path when the floor has no off-path container; (c) the container,
   uniform among the chosen side's containers for a potion, among all
   containers otherwise.

A guaranteed draw is never substituted (DH-27). The validation code
`GuaranteedItemUnreachable` is a run-time assertion on the result of
steps 3 and 4, and a build-time check over the release seed set (21.2).

## 13. Camp

### 13.1 The full rest

**(decided; order lead's call)** Camp is the one full rest and the run's
strongest guarantee. In order:

1. Downed companions get up **(agreed, Q4)**, wherever their bodies lay
   (11.4); a first downing sets Scarred, which step 3 clears (11.6).
   - Then camp catch-up for the hero and companions, with their choices
     (enemies 16; companions design 7.11) **(change CMP-31)**.
2. All HP and every pool are restored.
3. Every lingering condition ends, Scarred and `treatProof` Scarred
   included.
4. +1 supply.
5. The hero's potion top-up (13.2).
   - Then the Muster (companions design 7.11), revisitable until leaving
     camp **(change CMP-31)**.
6. The vendor opens.

Recruits arrive with full HP and pools and a kit holding 1 potion.

### 13.2 The hero's potion top-up

**(agreed, Q3, G5; mechanics new lead's call)**

- **Checked on arrival at camp, over the whole party.** A "healing potion"
  here is the ordinary healing potion or the camp-issue record below, on
  any belt or in any pack; a healing scroll or the phial does not count.
  A kit-issued ordinary healing potion (`boundToUnitId` set) counts as a
  healing potion (companions design 7.6; change CMP-21). If the hero
  holds one, nothing happens. Run again on leaving camp only if a
  dismissal there deleted a healing potion the hero held (a kit-bound
  dose); it then moves a companion's dose first and issues only if no
  unit holds one. Selling, handing over or drinking potions at camp never
  triggers it (companions design 7.6) **(change CMP-22)**.
- **If the hero holds none but a companion does,** camp moves one dose to
  the hero, with no new potion created: from the companion holding the
  most doses, ties by party order, belt before pack (`PotionMovedAtCamp`).
- **Only if no party unit holds a healing potion** does the hero gain 1
  free potion (`PotionToppedUp`), before the vendor opens. So handing
  every potion to a companion before the stairs gains nothing, and the
  free potion goes only to a party that really drank its potions.
- **The free potion is a separate record,** "healing potion (camp
  issue)": the same effect, size and use rules as the healing potion, flag
  `unsellable` (items 12.3), so it stacks only with itself (items 12.8)
  and never turns into coin **(lead's call; change DH-40)**. It goes to a
  belt stack of the same record or an empty belt slot, else the pack. If
  neither has room, the camp screen holds it and the leave-camp
  confirmation names it; leaving without making room forfeits it (an
  explicit choice).
- So the hero leaves camp with at least 1 healing potion unless the
  player gives it away there. Checking on arrival, not on leaving, means
  handing potions around at camp never buys a second free one.

## 14. Temporary HP (Bolstered, Later)

**(lead's call; change DH-22)**

- Sources: class features and spells only, never items (consumables
  denylist).
- A higher value replaces a lower one; it never stacks **(settled)**.
- **A single grant is at most the target's Recovery** (validation
  `BolsteredAboveCap`). It lasts for the fight only, or 10 turns
  **(settled)**.
- It is absorbed in the per-part damage steps (conditions 9.3), so the
  floors and the save chain see the total after absorption (20.3).
- It is not healing: it does not end Bleeding and does not count toward
  max HP or Recovery.
- Withered blocks gaining it **(settled)**. A Downed unit never gains it.

## 15. Drop prevention: owner and order

### 15.1 Owner

**This design owns the order**, closing conditions 9.3's "Final owner
order: death and healing design". **(lead's call)**

### 15.2 The chain

**(agreed, Q7; change DH-17)**

1. **Before the chain** (not links), in this order, which is the
   damage function's (20.3) and is normative:
   1. a Downed target discards the event (11.4);
   2. the per-part steps, temporary HP included (14);
   3. the event's total is summed;
   4. out-of-combat and undetected-trap damage clamps at 1 HP, on the
      post-absorption total (10.4).

   Q7 listed these steps as a group ("floors and Downed discard →
   temporary HP"); it did not order them among themselves. Clamping
   before absorption is not possible, because the floors test the summed
   total after resistances **(lead's call; reading confirmed by the
   client on 2026-11-17)**. Example: HP 5, temporary HP 3, an undetected
   trap deals 10: temporary HP absorbs 3, the total of 7 clamps to 4, and
   the unit ends on HP 1, temporary HP 0.
2. **Death-warded:** left on 1 HP; `combatOnly`, `charges:1`,
   `recharging: true`. Sources: the tier-3 Death-Ward-shaped class spell
   (Later) and the elite affix. The "rare charm" source is removed with
   Last-Breath Charm. **When it fires,** HP is set to 1 and the rest of the
   event's damage is discarded; the event then continues as in
   consumables 14.4 step 6 (riders, concentration, logging)
   **(lead's call)**.
3. **Reprieve Phial** (16), `recharging: false`.

The concentration difficulty uses the pre-save total **(settled)**: the
summed total after the per-part steps and **before** the floor clamp and
the chain (step 1.3's value) **(lead's call)**. With Last Ward retired, no
link acts at the attack step: every link reads the event's summed total
**(new lead's call)**.

### 15.3 Retired effects

**(agreed, Q7)**

- **Last Ward** (armour 14.3, Later). The "armed above half HP" narrowing
  almost never fires (under the guards, no non-boss on-level at-will, a
  crit included, can take the reference hero from above half to 0 from
  level 3 up); per camp it would be a free life per floor, much stronger
  than the phial (consumables 14.9). Adamant is already armour's anti-spike
  tool, and the phial owns the extra-life fantasy. The row is removed
  (DH-45); the agreed Q2 text that names it as link 1 is edited (DH-17,
  DH-38).
- **Last-Breath Charm** (trinkets 13.7, Later). It duplicates Death-warded
  on a timer and was already flagged (trinkets 24.3). Nothing shipped
  changes (DH-47).
- **Validation (new lead's call):** a property or trinket record that
  grants a drop prevention outside this chain fails
  `RetiredDropPrevention`; the names Last Ward and Last-Breath Charm are
  not reused for a drop-prevention effect.

### 15.4 The cap

**(agreed, Q7)**

- At most one **recharging** prevention fires per unit per fight
  (`dropPrevention.rechargingPerFight` 1; state
  `dropPreventionFiredThisFight`). Today this means a re-cast Death Ward
  cannot save the same unit twice in one fight; it caps any future
  recharging prevention.
- **Each chain link declares `recharging` in data** (Death-warded true,
  the phial false). A recharging link whose unit has
  `dropPreventionFiredThisFight` set is **skipped without spending its
  charge**, and the chain moves on to the next link. The phial never reads
  or sets the flag. Lethal-tag previews (17.5) apply the same test, so
  they never show "Death-warded fires" when the cap would skip it
  **(lead's call)**.
- **The single-use phial is outside the cap and always fires when armed**
  **(agreed, consumables Q2)**.

### 15.5 Events

**(lead's call)** Death-warded emits `DropPrevented{link: deathWarded}`
(the enum stays open for future links; `lastWard` is not added). The
phial keeps its existing `ItemTriggered` and `ItemDestroyed{usedUp}`
(consumables 14.4, 18.4), so there are no double events.

## 16. The Reprieve Phial's final numbers

**(agreed, consumables Q2, Q3; leave-HP agreed, Q8; rest lead's call)**
Everything not listed here is unchanged from consumables 14: belt only,
holder only, Rare, floor 3+, about 50% of runs, 1 per run (a cap), never
bought, never moves in combat, `treatProof` Scarred.

- **`talisman.leaveHpPermille` 334** of the maximum after Scarred, minimum
  1: about 30% of the unscarred maximum, 9 HP at level 3, 22 at level 9.
  The key stays where consumables 18.1 places it; its value is owned here.

  Chance the saved hero is dropped by the next 1 / 2 / 3 on-level attacks:

  | leaveHpPermille | 1 attack | 2 attacks | 3 attacks |
  |---|---|---|---|
  | 250 | 36–47% | 63–75% | 79–89% |
  | **334** | **9–21%** | **39–49%** | **63–75%** |
  | 500 | 0–1% | 19–32% | 42–61% |

  After a save, the kill bonus refocuses on the holder. At 250 a save
  followed by death a moment later would be the commonest outcome, the
  most bitter in the game. 334 gives the hero one turn to act or drink.
  The phial's scarcity is unchanged.
- **334 is a client number (new lead's call).** The reports and levers of
  consumables 19.3 and 14.7 are unchanged: "same-fight deaths after a
  save" above about 50% still points at `leaveHpPermille`, fire and
  saved-run rates point at `offerChancePermille` first, and the two levers
  stay separate. But moving `leaveHpPermille` now goes back to the client;
  `offerChancePermille` stays tuning.
- **Trigger: any source, in and out of combat (unchanged).** The floors of
  10.4 run first, so in practice it never fires out of combat or on an
  undetected trap. Consumables 14.2's parenthetical "(a trap that kills
  the hero ends the run too)" is amended (DH-41). **CO-16 closes.**
- **"1 HP plus Dodging": rejected.** A 1-HP unit dies to any hit, area or
  save; Dodging only delays the next attack and adds a condition inside
  the chain.
- **No would-die coverage** while companions cannot die **(agreed, Q2)**.
- **AI:** the scorer values spending an armed save at about the kill bonus
  (consumables 14.8), extended to Death-warded. Armed units are neither
  avoided nor hunted, and targeting reads no identity.
- **Staging:** M2 if this design is approved before M2 content lock
  (consumables 14.10); otherwise Later.

## 17. Fairness: surprise, telegraphs and lethal previews

### 17.1 No surprise on floor 1's first two main-path rooms

**(agreed, Q9; mechanics new lead's call; change DH-2, DH-28, DH-29)**

- **Indexing (stated once, used everywhere in this document):** the main
  path's rooms are numbered from 0, the entry. "Floor 1's first two
  main-path rooms" are **main-path rooms 0 and 1** **(lead's call, the
  reading of Q9; confirmed by the client on 2026-11-17)**.
- **Rule:** while the hero is the only party unit, the party is never
  Surprised when combat starts with the hero standing on a floor-1 square
  whose `mainPathIndex` is 0 or 1. When the rule applies, the party side is
  treated as having noticed (no Surprised); the monsters' surprise is
  computed normally, so they may still be Surprised by the hero.
- **`mainPathIndex` per square,** filled in by dungeon generation: a
  room's squares, and the squares of a main-path room's Alcoves, take the
  room's index; a Portal (corridor or doorway) takes an index only if it
  joins consecutive main-path rooms, and then the lower one, except that
  arena-adjacent squares take the arena's index; every other square has
  none, so the floor-1 Bypass arm has none. Every square also carries an
  `effectiveIndex`, which `expectedTenths`, Keepsakes and leftovers read;
  this rule reads `mainPathIndex` only (none = not 0–1) (floor layout
  design 3.4, 6.1 LS10) **(change FL-13)**.
- **Judged by the hero's square** at the moment the noticing that starts
  combat resolves, not by the group's `homeRoom`; a patrol or joiner that
  reaches those squares is covered too.
- **Scope:** the rule does **not** reach room 2. The floor-1 lair of the
  enemies 10.2 worked example sits in room 2, read as an example at
  enemies 10.2's room level 2, not `mainPathIndex` 2 (floor layout
  design 3.4) **(change FL-4)**; that failure is closed by
  the surprise reconciliation and the floor-1 budgets (21.1), not by this
  rule. Widening the rule would change Q9 and goes back to the client.
- It extends enemies invariant 6 (no limited abilities in floor-1 surprise
  rounds, no hidden lurkers in floor 1's first two rooms) and the settled
  surprise rule of main 4.3 and enemies 12.2, which gain the exception.
  Invariant 6's existing lurker clause and the patrol row (enemies 10.4)
  are read with their stricter scope, the entry plus the next two
  main-path rooms (indices 0–2), and DH-28 writes that reading down; no
  approved ban is narrowed.
- **Guards:** the 6.4 round-1 rows treat these two rooms as no-surprise
  rooms. The surprise reconciliation (21.1 step 1) still runs for every
  other room.
- The existing generation requirement (patrol loops avoid the entry room
  and the first two main-path rooms for the first 20 ticks) is unchanged in
  effect; DH-28 restates it with indices (rooms 0, 1 and 2).
- **Why:** a level-1 hero dying before its first turn is the death most
  likely to stop new players coming back.
- **Hazards (told plainly; change HZ-19, M3):** this rule is unchanged.
  Landmark ticks are a non-hazard class (no damage, condition or unit
  movement) legal on floor 1; at Nightmare tier 2+ floor-1 hazard
  packages place no Void, Stir or Lurch in main-path rooms 0–1 (hazards
  design 6.1, 6.19, 7.2–7.4).

### 17.2 What the telegraphs compute

**(lead's call)** Everything in 17.3–17.12 is computed by the core from
**party knowledge only**: exactly the ability model 8.3 knowledge set,
plus enemies 13.5 reading. It uses integer maths, consumes no random
number, changes no state, and is never stored or replayed. Where a report
or the death recap needs a past value (the band at the hero's last turn
end), it replays the event log to that point and recomputes it
**(lead's call)**.

### 17.3 Hero danger band

Recomputed after every command, **only while `combatActive` is true**
(20.5); out of combat no band is shown.

- **Inputs:** each visible enemy whose threat reach covers the hero before
  the hero's next turn, using **known** kits only (seen at-wills, seen
  limited abilities that are ready); announced joiners and boss actions;
  known ticks and hazards.
- **Assumes every known threat picks the hero,** so it never leaks AI
  intent (enemies 13.6).
- **Bands,** from exact convolution of the integer dice, crits included:
  - **Safe:** chance of death < 1% (`danger.riskyPermille` 10);
  - **Risky:** 1–20%;
  - **Deadly:** ≥ 20% (`danger.deadlyPermille` 200), or expected damage
    reaches current HP + temporary HP.
- **Unknown kits:** a visible enemy with an unseen kit adds nothing to the
  number and sets an "unknown threats" marker on the band. It is never
  estimated.
- Shown on the portrait and the initiative bar; the tooltip gives the
  exact % and "known threats only". Companions show "can be Downed" on the
  same basis.

**The algorithm (normative; lead's call):**

1. **Reach:** an enemy threatens the hero if a path from its square to a
   square from which a known ability reaches the hero costs at most its
   speed (no Dash), over current occupancy and known terrain. Opportunity
   attacks on its way are ignored.
2. **Threat list:** every reaching enemy's best-damage known ability
   (one use per enemy turn, its whole turn as enemies 6.4 counts it);
   announced joiners and boss actions due before the hero's next turn;
   and the **hero's own next turn-start tick batch**, with the batch cap,
   since it lands before the hero can act.
   *Pending request (change HZ-13; the DH owner rules):* the threat list
   gains environment acts due before the hero's next turn (hazards
   design 6.3.1, 6.11).
3. **Each threat is evaluated independently** against the hero's current
   state: no cross-threat conditions (one threat's Prone does not grant
   the next advantage). Attack: hit chance from d20 + bonus against the
   hero's current AC, with advantage or disadvantage from the current
   state, natural 20 a crit (double dice, 9), natural 1 a miss. Area or
   save: the save chance from the hero's current save bonus, with half or
   none per record. Per-part steps (resistances, shells) apply as the
   damage function would apply them now.
4. **Convolution:** the per-threat damage distributions are convolved
   into one. **Death chance** = the probability that the summed damage is
   ≥ current HP + temporary HP.
5. **Before prevention:** the band ignores Death-warded and the phial. When
   an armed save would cover the result, the band carries a "Reprieve
   armed" or "Death-warded" marker and the tooltip says "before saves"
   (17.5's tags read the same way).
6. **Numbers:** probabilities are fixed-point integers at scale 10⁹,
   rounded down after every multiplication in the convolution, so every
   intermediate fits in 64 bits; a band test compares
   `probability × 1000` against `permille × 10⁹`.

### 17.4 End-turn warning

- On by default, opt-out.
- Fires only when the hero would end its turn Deadly **and** at least one
  **candidate** would lower its band.
- **Candidates (normative; lead's call),** each taken alone, never
  combined:
  - each end square of the reachable-square set already computed for the
    move preview (moving changes who reaches the hero);
  - each available action or bonus action whose effect on the hero's state
    involves no roll: drinking a potion (+Recovery), Dodge, Disengage, the
    class's defensive tools, Second Wind.
  Each candidate is scored by recomputing the band (17.3) on the resulting
  state. Actions whose band change depends on a roll (attacks, enemy
  saves) are not candidates. The search consumes no random number and
  changes no state.
- Text: "You can be killed this round (Deadly, 27%)." It **names no
  action**, so it is a warning, not a suggestion **(settled, ability 11.3:
  the hero never gets suggestions)**.
- Report: "warning overridden, then died that round".
- **Party-fight lever (prepared, off; lead's call):**
  `danger.partyWarningRule` `always` (launch) or `noCompanionInReach`: in a
  party fight, the warning (never the band) fires on Deadly only when no
  companion is in reach of the same threats. It reads party knowledge only
  and leaks no AI intent. Switch it on if the danger calibration report
  (21.5) shows Deadly on more than its target share of party-fight turn
  ends.
- *Pending request (change HZ-13; the DH owner rules):* due squares
  count as hazards for the move candidates, and the warning reads
  environment acts through 17.3's band (hazards design 6.3.1, 6.18).

### 17.5 Lethal tags

On every damage preview, outgoing and incoming: "lethal"; "lethal →
Death-warded / Reprieve fires"; "will Down X"; on the hero's own attack,
"killing this triggers a burst that is lethal to you".

**Warned or floored marks (change HZ-14; M2 (gated), else M3):** a
floored party unit on a due square carries a bracketed "1" pip under
its HP bar and "floored this time" (never a shield); a Warned unit
carries the lethal tag (10.4; hazards design 6.3.2, 6.18).

*Pending request (change HZ-13; the DH owner rules):* lethal tags
extend to Stir acts due on due squares and to fall branches (hazards
design 6.3.1, 6.18).

### 17.6 Move preview

Flags opportunity attacks, Set triggers and **known** hazards and traps
that could be lethal. Known contact hazards and harm squares count as
hazards here: the move preview sums harm-square damage per square
entered, with a lethal tag, and lethal tags (17.5) extend to contact
hazards, harm squares, Kegs, Kick lines and Overturn strips (M3) (room
terrain design 6.12, `2026-11-21-room-terrain-design.md`) **(change
TR-23)**. *Pending request (change HZ-13; the DH owner rules):* due
squares count as hazards here, and the preview shows the act's result
("Lands on due square: …") (hazards design 6.3.1, 6.18).

### 17.7 Friendly fire

A party action whose maximum damage could kill the hero needs an explicit
confirmation. The suggester never proposes one (extends consumables 14.8
and ability 8.2).

Separately, a party action that would move an ally into a hazard square
or over a hazard edge asks for confirmation, lethal or not (10.4 is the
related hazard section); the lethal test above is unchanged (Arcanist
design 7.14, ARC-25). The same confirmation covers contact hazards, harm
squares, Keg footprints, Kick lines and Overturn strips (M3) (room
terrain design 6.5.4, 6.12) **(change TR-23)**. This confirmation pattern is also the model for
the Arcanist's last-slot confirmation, a confirmation of the player's own
action (Arcanist design 5.2, ARC-13).

**Rescue push (agreed, hazards Q2; change HZ-10; M2 (gated), else
M3):** pushing an ally into a Void is legal behind this confirmation.
The Dreamfall it causes lands on the secondary Snag, and the faller
also loses its next action (Groggy as normal); the FallFloor (10.4)
still holds (hazards design 6.4). *Pending request (change HZ-13; the
DH owner rules):* due squares count as hazards for this confirmation
(hazards design 6.3.1).

### 17.8 Plates

**(change DH-20)** A "bloodied" mark at 50% and a "low" mark at 25% on
party plates (not "critical", which reads as a crit); the hero's portrait
pulses while Deadly; the Downed marker reads "gets up if you win, or at a
rest"; armed Death-warded and Reprieve are shown.

### 17.9 Get-up preview

For Medicine and Lifeline: the visible enemies that can reach the
companion before its next turn; that companion's band after getting up on
1 HP; "already got up this fight" when greyed.

### 17.10 Get-up overlay

Selecting a Downed companion shows which allies can reach an adjacent
square and get it up this round, with the opportunity attacks on each
path.

### 17.11 Heal preview and rest screen

The heal preview shows overheal, Withered and other blocks, and "cannot
lift Downed". The rest screen shows, for each member, HP after get-up,
Treat and Recovery, plus the supply left and the patrol warning
**(settled)**.

### 17.12 Death recap and floor-entry card

- **Death recap** from the event log: the last two rounds; the killing
  chain with every roll and its odds; the band at the hero's last turn
  end (recomputed by replay, 17.2); what was left unspent, worded
  neutrally ("2 potions on the belt"), never as blame.
  *Pending request (change HZ-13; the DH owner rules):* the recap names
  the Stir when an environment act is in the killing chain (hazards
  design 6.18).
- **Floor-entry card** headed by the **Stratum Card** (theme name, act,
  signature line, damage types, non-fearless band, boss name; run
  structure design 6.9) above the guaranteed counts (G6) **(change
  RT-12)**, then the floor count line "Main path: n rooms; side rooms: k;
  secrets: s" (s from floor 2; the floor-1 card omits it), with its
  party-bar tally, counts only, never locations (floor layout design 9.2)
  **(change FL-20, M2)**, and a party-bar tally "found 1 of at least 1": the healing
  potions picked up this floor from containers and carriers, whatever their source. The `guaranteed`
  flag on `LootTaken` stays for reports only.

### 17.13 Movement undo (Later)

**(agreed, Q12; scope new lead's call)**

- In combat only. A unit may undo movement made **in its current turn**,
  back to the turn's start or to the last point that closed the window.
- **The window closes** on any action, bonus action, reaction, free
  interaction or ability use, and on any move that revealed a square or a
  unit, triggered a reaction, a Set or any other trigger, entered a
  hazard, or consumed a random number (a noticing roll included).
- Undo is a core command, validated and logged like any other, so replays
  reproduce it. It never reaches into another unit's turn.

## 18. Calibration targets

### 18.1 Healer gap

**(agreed, Q10; definition and levers new lead's call)**

- **Gate:** the simulator's win rate for parties with a healer and parties
  without one differs by **at most 10 points**. 10 is a client number,
  not tuning.
- **A party with a healer** is one where at least one member's kit holds
  an ability with a `healRecovery` effect whose target entries include an
  ally other than self; potions, scrolls and self-only heals such as
  Second Wind do not count. Both arms use the same seeds and the reference
  policy (19.3).
- **Levers if it fails,** in order: heal-spell targets or pool sizes in
  class and companion data (for the Herald, heal-specific sub-limits in
  Herald data; never the Herald's slot or Cadence pools; Herald design
  5.4, change HR-38); then random potion weight for everyone. Never
  the guarantee, and never by class (5.6).
- **Reports beside the gate (classes design 12.3; Herald design 5.4):**
  the Herald arms, policy forced per subclass with the Herald design 5.4
  construction (no Herald / Herald without heal cards, with a "Hearth,
  no card" row / Herald with heal cards / Herald, free draws / Herald,
  card added), healer classification columns ("ever held an ally heal"
  per run and per fight), a healer-count breakdown (0 / 1 / 2+, the 2+
  row read first, with escalation) and a heal-source breakdown (Herald
  hero card, Hearth widening, Herald recruit, heal-kit recruit, scroll
  reads, get-ups by route), with `MoraleValueReport`,
  `AllyTempoValueReport`, heal telemetry and the trap-room report. They
  are reports only: the gate stays exactly as defined above, its kit
  snapshot read per fight (Herald design 5.4, a reading), and 18.2's "at
  least a quarter" reads the gate's no-healer arm. The Herald's
  heal-isolation gate (classes design 12.3; Herald design 5.4) is a
  separate gate and does not change this one. **(change CL-9, HR-26,
  HR-38)**
- **While no heal kit ships (companions design 5.4):** "heal-kit recruit"
  prints 0 and "healer count 2+" prints N/A; the gate text is unchanged,
  and at M2 its healer arm is exactly a Herald hero holding a heal card
  **(agreed, companions Q3; change CMP-23)**.
- **M2 reading (companions design 11.3, 11.4):** the gate's pass or fail
  verdict reads the M2 simulator only, never a toy, pinned to the
  reference schedule (party 1 / 2 / 3 / 4 / 4) and the written recruit
  policy `MusterBestFit` (the `Reference` arm); the companions design's
  11.4 reports (healer counts by party size, small parties reported
  separately) sit beside it as reports **(change CMP-18, CMP-29)**.

### 18.2 Target death rate

**(agreed, Q11: client calibration targets; reading new lead's call)**

| Target | Value |
|---|---|
| Hero deaths on floor 1 | Under about 5% of runs |
| Where deaths fall | Most on floors 3–5, at elites and bosses |
| Win rate, competent player | Roughly a quarter to a third of runs |

- **"Competent player"** is the simulator's reference party policy (19.3)
  over the release seed set, checked against playtests. When route choice
  is on (`route.twoDoorsEnabled`; run structure design 6.11), it picks
  doors with the Reference chooser inside that policy, and the
  random-draw baseline is stated beside it **(change RT-10)**.
- **Reading (lead's call):**
  - floor 1 passes when the upper bound of a 95% interval on the floor-1
    hero-death share, over the release seed set, is under 5%;
  - "most deaths on floors 3–5 at elites and bosses" passes when at least
    50% of hero deaths fall on floors 3–5 **and** at least 50% of those
    fall in elite or boss fights;
  - the win-rate range is read **separately** for the healer and no-healer
    arms (18.1), and the no-healer arm must reach at least a quarter, so
    the 10-point gap can never hide a healer becoming close to required.
- **Floor 1 is met by construction plus simulation:** the floor-1
  cumulative budget (21.1 step 5) keeps the summed guarded round-1 risk
  well under 5%, and the simulator checks the rest.
- These are **calibration targets, not validation gates**: a miss is
  reported to the client with the report that shows it, not a build
  failure.
- **M2 reading (companions design 11.4):** the pass or fail verdicts read
  the M2 simulator only, never a toy; "checked against playtests" stands
  as a check beside the verdict, and a disagreement goes to the client
  **(change CMP-29)**.
- **Lever order when a target is missed:**
  - floor-1 deaths at 5% or more: floor-1 ambush and lair budgets, the
    floor-1 cumulative budget, the floor-1 solo boss rows (21.1), then the
    Medicine reserve lever (8); never the HP formula or the package
    without the client;
  - deaths concentrated in ordinary fights rather than elites and bosses:
    the encounter tail (template caps, joiner caps) before boss rows;
  - win rate above a third: enemy budgets (enemies 10.1) and the rests
    gate (5.7); win rate below a quarter: guard rows and boss rows first;
    then, **only if its own 5.5 report trigger has fired**, the
    pre-authorised potion raise; otherwise the miss goes to the client
    (28.2).

## 19. AI

### 19.1 Monsters

**(lead's call)**

- Invariant 2 is unchanged and now also enforced by the core (Downed is
  inert). Invariant 1 holds: no term reads hero status.
- Kill bonus and Quarry are unchanged. Armed saves are valued as in 16.
- Carriers drink only their own potion. Monsters never loot containers, so
  they cannot take guaranteed loot.

### 19.2 Party scorer and companion suggester

- The party scorer (simulator, and the companion suggester) may weight the
  hero's 0 as run-ending, because invariant 1 binds monsters only. Its
  healing term is the change in drop chance × the cost of a drop.
- **The companion suggester (off by default, ability 11.3)** proposes
  Administer or throw to the hero when the heal moves the hero's band;
  never proposes a mid-fight get-up that would leave the companion Deadly;
  never proposes a party area that could kill the hero; never proposes
  drinking to overheal by more than about 50%.
- The hero gets no suggestions **(agreed, ability 11.3)**.

### 19.3 Simulator policy

The reference policy rests when HP ≤ Recovery + one enemy round, or before
the boss; drinks at Risky; opens every main-path container. **One enemy
round** = FloorDiv(max HP × `sim.restRoundPermille`, 1000), with
`sim.restRoundPermille` 250 *(illustr.; the 6.4 per-turn expected guard)*
**(lead's call)**.

**When route choice is on (`route.twoDoorsEnabled`; run structure design
6.11):** the reference policy picks the next floor at each door camp with
the **Reference chooser**: the door whose theme has the higher floor-clear
rate in the ThemeParity table for (hero class, party size, floor), read at
that floor's fixed arrival state; ties go to Door A. Random and Oracle
choosers are reported beside it. With the switch off, every floor takes
Door A (the random draw) **(change RT-10)**.

## 20. Data and the core

### 20.1 Tuning

*(all illustr. except the client numbers of 5.5, 7.1, 16 and 18 and the lead's numbers and lists marked below)*

A new `death.json` holds the `hp.*`, `recovery.*`, `crit.*`, `damage.*`,
`downed.*`, `dropPrevention.*`, `danger.*`, `potion.*`, `guard.*`,
`sim.*`, `validation.*` and `calibration.*` keys. The loot, kit and camp
keys go in the consumables block of `inventory.json` (consumables 18.1)
as bare keys beside `offensiveSundryMin`, in that block's existing style;
`sundriesPerFloor` keeps its name and is reshaped per floor (DH-42).
`talisman.leaveHpPermille` stays where consumables 18.1 places it. The
save hash covers both files.

| Key | Value | Status |
|---|---|---|
| `hp.adventurerBonus` | 11 | Client number (Q5) |
| `hp.companionBonus` | 11 | Tuning, may only go down |
| `hp.classTiers` | slight 6, standard 7, sturdy 8 | Client number (Q5) |
| `recovery.base` / `recovery.perLevel` | 4 / 2 | Lead's numbers (not tuning, 8) |
| `crit.mode` | `doubleDice` | Lead's call (engine switch) |
| `damage.outOfCombatFloor` | 1 | Client rule (Q6) |
| `damage.undetectedTrapFloor` | 1 | Client rule (Q6) |
| `downed.inert` | true | Lead's call (engine switch) |
| `downed.getUpAtRest` | true | Client rule (Q4) |
| `downed.medicineGetUpsPerFight` | 1 | Client rule (Q4) |
| `downed.repeatDowningWeary` | false | Lever |
| `dropPrevention.rechargingPerFight` | 1 | Client rule (Q7) |
| `talisman.leaveHpPermille` | 334 | Client number (Q8) |
| `danger.riskyPermille` / `danger.deadlyPermille` | 10 / 200 | Lead's numbers |
| `danger.partyWarningRule` | `always` (lever: `noCompanionInReach`) | Lever (17.4) |
| `restDefensiveKinds` | the approved non-heal effect kinds for a `rest` defensive tool | Lead's list (21.2) |
| `healingSundryMin` | [1, 1, 1, 1, 1] | Client number (Q3); floors 4–5 may rise to 2 |
| `healingSundryMax` | 3 | Client number (Q3) |
| `sundriesPerFloor` | [3–5, 3–5, 4–5, 4–5, 4–5] | Tuning; replaces the single `[3, 5]` range |
| `healingOffPathPermille` | 600 | Tuning |
| `supplyMainPathMin` | 1 | Client number (Q3) |
| `heroKitPotions` / `recruitKitPotions` | 2 / 1 | Client numbers (Q3) |
| `campHeroPotionTopUp` | 1 | Client number (Q3) |
| `potion.laterInFightPermille` | 1000 (lever: 500) | Lever |
| `guard.floor1BossRounds` | 6 | Tuning; calibrated by the simulator, read by the check (21.1) |
| `guard.floor1BossDoorHpPermille` | 700 | Tuning; calibrated from "HP at the boss door" |
| `guard.floor1BossPotionsHeld` | 3 | Tuning; calibrated from "potions held at the boss door", never above `heroKitPotions` + `healingSundryMin` |
| `guard.floor1CumulativeRoundOnePermille` | 25 | Tuning (2.5%), down only (21.1) |
| `sim.restRoundPermille` | 250 | Tuning (19.3) |
| `validation.healingFloorPermille` | 650 | Lead's number |
| `calibration.healerGapPoints` | 10 | Client number (Q10) |

### 20.2 State

- `combatActive` on the run state (20.5);
- `currentHp` per unit;
- `downedThisFloor` (existing);
- `downingCountThisFloor` (for the Weary lever);
- `medicineGetUpsThisFight` per unit (reset at combat start);
- `dropPreventionFiredThisFight` per unit (reset at combat start);
- `heroPotionTopUpPending` on the camp state (13.2);
- a Downed companion's body: its square, or `carriedBy` (11.4);
- `sourceTrapUndetected` on condition instances a trap applied (10.4);
- Bolstered as a condition instance holding its value.

### 20.3 One damage function

**(lead's call)** In order:

1. If the target is Downed: discard, `DamageDiscarded{downedInert}`, stop.
2. Per-part steps: ladder shifts and shells, type ends and layer breaks,
   transforms, temporary HP **(settled steps)**.
3. Sum the event's total. Duck (a reaction) is offered here, with this
   total known, if the event is Duck-eligible and the total is above 0
   (Shade design 7.4, SH-6).
   3a. If Ducked: total = max(1, FloorDiv(total, 2)). This step-3a value
   is the **pre-save total** that concentration uses; steps 4 onward read
   it.
4. Floors, on the post-absorption total of step 3a: for a party unit, if
   `combatActive` is false (20.5) or the event carries undetected-trap
   parts (10.4, with the mixed-event rule), clamp so HP stays ≥ 1,
   `DamageClamped{outOfCombat | undetectedTrap}`.
5. If the total still reaches 0: the save chain (15.2), with the cap
   (15.4). A link that fires ends the chain and fixes HP as 15.2 says.
6. Lower HP.
7. At 0: a companion → Downed (engine grant); a monster → dead or a
   disposition. **The hero → the core finishes this event's bookkeeping
   (concentration, logging), emits `RunEnded{heroDeath, killingEventId}`
   as its final event, and rejects every later command.** No later
   target of an area, rider, phase change (conditions 9.3), XP or
   combat-end step is processed. `killingEventId` is the damage event's
   sequence id. The host deletes the save slot on that event.
8. Otherwise the event continues (riders, on-damage triggers);
   concentration uses step 3a's value.

The turn-start zero-HP check (conditions 5.4 step 4) stays as the backstop
for tick batches, whose chain already ran where the damage landed.

### 20.4 Events and greyed reasons

- `UnitDowned`; `UnitGotUp{reason: win | medicine | rest | camp}`;
  `DropPrevented{link}`; `DamageDiscarded{downedInert}`;
  `DamageClamped{reason}`; `HealApplied{source, amount, overheal,
  blocked}`; `RunEnded`; `FloorEntered{…, guaranteedPotions,
  guaranteedSupply}`; `PotionToppedUp`; `PotionMovedAtCamp`;
  `CombatStarted`, `CombatEnded{reason}`; `GenerationRetried`;
  `MoveUndone` (Later).
- `LootTaken` carries a `guaranteed` flag for reports only.
- Greyed reasons: `AlreadyGotUpThisFight`, `TargetDownedInert`, plus the
  existing reasons.

### 20.5 Combat state and the combat-end order

**(lead's call)**

- **`combatActive`** is a run-state flag. It becomes true at the
  **combat-start step**, which runs before the cost-paid and resolution
  steps of any ability use whose targets or area include a hostile unit,
  or that is noticed, and when noticing starts combat (main 4.3). It
  becomes false only after the whole combat-end procedure below has run
  (conditions 5.5). Every floor test (20.3 step 4), the band (17.3) and
  `medicineGetUpsThisFight` read this flag.
- **At combat start:** `medicineGetUpsThisFight` and
  `dropPreventionFiredThisFight` reset; carried bodies are placed (11.4).
- **Combat end, in order:**
  1. the current unit's turn-end step (conditions 5.5);
  2. `combatOnly` instances end;
  3. on a win, Downed companions get up on 1 HP where they lie, setting
     Scarred and `downedThisFloor` if this is the floor's first downing;
  4. XP and level-ups (ability 6.9): each levelled unit gains Δ (7.3),
     except a Downed unit, whose HP stays 0;
  5. `combatActive` becomes false (`CombatEnded{reason}`).
- If the hero reached 0, none of this runs (20.3 step 7).

## 21. Validation, testing and simulator

### 21.1 Guard rows

**(lead's call; change DH-26)**

1. **First, reconcile the surprise model.** The numbers review's model
   (each enemy acting again at 50% before the hero) gives the **reference**
   hero 0.87% in the floor-1 lair, which fails the approved row (< 0.5%,
   surprise included). Either the review's model differs from the core's
   real surprise rule (enemies 12.2 and the turn order), or approved
   content fails a guard it claims to pass. Re-run the enemies 10.2 worked
   examples on the core's actual rule, with 17.1 applied, before any row is
   written.
2. **The reference rows stay verbatim,** including the round-1 row at
   < 0.5%. No row is loosened.
3. **New lowest-legal-hero rows,** at lowest HP (tier 6, Con −1)
   **combined with** the existing AR-20 lowest legal AC:
   - a non-boss unit's maximum turn damage with a crit ≤ **75%** of HP
     (2/3 fails the approved L2 brute in the 10.2 lair, at 15 of 21);
   - round-1 death chance (same definition as the reference row) < **2%**
     *(illustr.)* per encounter on floors 2–5; on floor 1 the
     per-encounter value is bounded by the cumulative budget of step 5
     instead, which is the stricter test.
4. **"Before the hero's first turn"** replaces "round 1" for
   ambush-eligible templates **only after** step 1 shows the 10.2 worked
   examples pass. If they fail, the levers are floor-1 ambush and lair
   budgets, never a looser threshold. The 17.1 rule is not a lever for the
   room-2 lair: it covers main-path rooms 0 and 1 only.
5. **Floor-1 rows (new; lead's call):**
   - **Floor-1 solo boss, expected damage to the hero per round** (its
     turn plus boss actions) ≤ **25%** of the hero's HP *(illustr.)*,
     beside the approved 35% boss row, which stays for every other boss.
     5.8 item 2 shows 35% cannot pass step 6 for any class.
   - **Floor-1 solo boss at the lowest legal hero:** worst spike (dice at
     maximum, a crit, one boss action) ≤ **75%** of HP; a full-HP lowest
     legal hero cannot be reduced to 0 in one round **even with one crit**
     *(illustr.)*.
   - **Floor-1 cumulative budget,** for the reference and the lowest legal
     hero: the sum, over floor 1's encounter slots, of the highest round-1
     death chance among the templates the generator can emit in that slot,
     plus the floor-1 boss's one-round death chance, ≤
     `guard.floor1CumulativeRoundOnePermille` 25 (2.5%) *(illustr.)*, half
     of the client's Q11 floor-1 target. Per-encounter rows cannot bound a
     share of runs; this row can. The simulator computes it, as it already
     computes the round-1 row.

   These rows go into 6.4 (DH-26).
6. **Floor-1 boss sustain check (`Floor1BossSustain`; new lead's call).**
   It reads party consumables, so it is **not** a 6.4 guard: it is a
   content-validation code (21.2) and leaves consumables 14.9 untouched.
   For every launch class, at hero level 3, the floor-1 boss-door level
   (lead's call; Vanguard design 10.3, VG-24), at the reference and the
   lowest legal Con:

   `bossExpectedPerRound × guard.floor1BossRounds ≤
   FloorDiv(maxHp × guard.floor1BossDoorHpPermille, 1000) +
   guard.floor1BossPotionsHeld × Recovery + restToolHeal`

   `restToolHeal` is the class's `restDefensiveTool` heal amount, or 0 for
   a tool that does not heal. The keys default to 6 rounds, 700‰ and 3
   potions *(illustr.)*; the simulator calibrates them from the "HP at the
   boss door" and "potions held at the boss door" medians (21.5), and the
   check only reads them, so no build gate runs the simulator. Reference
   check at level 3, 6 rounds: at 35% per round, need 67 against 55–66,
   which fails **even with** a full-Recovery self-heal; at 25%, need 48,
   which passes for every class at the reference (22 + 33 = 55 before any
   rest tool). At the lowest legal Con (26 HP, Recovery 9) the budget is
   18 + 27 = 45 before the rest tool: a half-Recovery self-heal (4) passes,
   but a class whose rest tool does not heal needs the boss nearer 23%
   *(illustr.)*. The check is meant to bind there; that is the floor-1
   boss's real ceiling.

The 6.4 guards are still computed **without** any party consumable or
talisman (consumables 14.9) **(settled)**; only step 6 reads them, and it
is not a guard.

**Party sizes (companions design 7.7, 7.9; change CMP-8):** these rows
are evaluated at every reachable party size and weight, down to the
shortfall minimum, and the floor-1 solo round-1 row also applies to solo
parties on floors 2–5.

### 21.2 Content validation

**(lead's call; the data each code reads is named)**

- `PartyInstantDeath` (10.2);
- `HealPerTargetAboveRecovery` (12.2): every target entry of a
  `healRecovery` effect has permille ≤ 1000, at every upcast level;
- `BolsteredAboveCap` (14);
- `MissingRestDefensiveTool` (12.1): each launch class has exactly one
  `rest`-pool ability tagged `restDefensiveTool: true`, and it is either a
  self heal with `healRecovery` permille ≤ 500 (≤ 1000 for the one
  fighter-type Second Wind) or an effect kind on the approved defensive
  list (`restDefensiveKinds` in `death.json`, maintained by the lead);
- `LiftsDownedUnapproved` (11.5);
- `RetiredDropPrevention` (15.3): drop prevention has an enumerated
  vocabulary, the `wouldDropToZero` trigger and the Death-warded grant;
  any property or trinket record using either fails;
- `Floor1BossSustain` (21.1 step 6);
- `GuaranteedItemUnreachable` (off the main path, on a carrier, behind a
  check, or outside the first half for the potion): a run-time assertion
  on every generated floor (12.5) and a build-time check over every room
  template and the release seed set; HE-7's reservation is checked the
  same way (12.4);
- `SundryOverfull` (minimums exceed the floor's low sundry count);
- `HealingFloorUnmet{floor, partySize}` (21.3);
- a grep check that no "gated on the death and healing design" marker
  remains in the specs folder (section 26).

### 21.3 HE-6 healing floor

**(lead's call; a build check, not a player rule)**

- For each floor and each legal party size n, at the floor's midpoint
  reference level, the guaranteed healing per member must reach
  `validation.healingFloorPermille` 650 of max HP.
- Guaranteed healing = guaranteed rests (3 on floor 1, else 2) × n ×
  Recovery + guaranteed potions (`healingSundryMin`, plus 2 kit potions on
  floor 1) × Recovery.
- It recomputes automatically when Recovery or HP change.

| Floor (level) | n = 1 | n = 2 | n = 3 | n = 4 |
|---|---|---|---|---|
| 1 (L2, solo) | 2.16× | — | — | — |
| 2 (L4) | 1.00× | 0.83× | 0.78× | 0.75× |
| 3 (L6) | 0.96× | 0.80× | 0.75× | 0.72× |
| 4 (L8) | 0.94× | 0.78× | 0.73× | 0.71× |
| 5 (L9) | 0.93× | 0.78× | 0.73× | 0.70× |

- Every cell passes 0.65. Rests supply 0.62–0.67× of it: **supply is the
  real guarantee**.
- The Medicine bonus, random top-up, vendor, the camp top-up and level-ups
  add roughly 0.3–0.7× more and are not counted.
- The late-floor guarantee at party size 4 is thin (0.70×) by design.
  Watch "guarantee was the only potion" (the pre-authorised raise, 5.5).

### 21.4 Tests

- Property tests: Downed inertness (allied drag allowed, occupancy), floors
  before the chain, chain order, the recharging cap, the rest get-up
  order, Medicine once per fight, the level-up HP delta, the Scarred clamp,
  the top-up checked on arrival over the whole party ("potions handed to
  companions before the stairs never yield a free potion"), "a Downed unit
  always has current HP 0", the order example of 15.2 (HP 5, temporary HP
  3, an undetected trap for 10 ends on HP 1, temporary HP 0), a skipped
  recharging link keeps its charge.
- Scenario tests: "hero dies alongside the last enemy" (a Volatile burst
  killing the last enemy is in-combat damage); "a party area out of combat
  that hits no hostile is floored"; "an opening ambush strike is
  in-combat damage"; "an undetected trap that starts combat is floored";
  "a trap's Bleeding keeps its floor after the trap is detected"; "no
  party surprise on floor-1 squares with `mainPathIndex` 0–1, corridors
  included"; "a push stops before a body, with no collision"; "after a
  flee the body stays, and the party cannot rest beside aware enemies";
  "the core stops at `RunEnded`"; "a levelled Downed companion stays on 0
  after a flee"; replays of a tick-batch death and a multi-part lethal
  hit.
- The danger band, the end-turn warning and every preview consume no
  random number and change no state.

### 21.5 Simulator reports

- Hero deaths by floor, cause (crit, focus, own area, detected trap,
  surprise) and band at the previous turn end, each split by whether a
  save (Death-warded or the phial) was armed. **Deaths from Safe should
  be about 0.**
- **Danger calibration:** among hero turn ends shown Deadly in party
  fights, the share that ended in hero death that round; and the share of
  party-fight hero turn ends shown Deadly at all (target ≤ 25%
  *(illustr.)*). Above target, the 17.4 party-fight lever.
- The Q11 targets (18.2): floor-1 death share, deaths by floor and by
  encounter kind (ordinary, elite, boss), win rate.
- Deaths with a potion on the belt; deaths with a rest available.
- "Warning overridden, then died that round".
- HP at the boss door (target 60–80% *(illustr.)*) and healing potions
  held at the boss door, each with the floor-1 solo boss separately; their
  medians calibrate the `guard.floor1Boss*` keys (21.1 step 6).
- **Rests per floor (gate: median ≤ 3)**; rests interrupted while the hero
  is under 50%.
- Potions unused at camp; camp top-ups granted.
- Floors where the guaranteed potion was the only one; guaranteed items
  left unpicked.
- Healing available per floor ÷ party max HP, with a `levelUpHp` column
  (non-guaranteed).
- **Boss fights:** potions drunk per fight; fights that needed more than 4
  heals; the floor-1 solo boss separately.
- Companion downings per fight and per floor; Medicine get-ups per fight;
  the share of enemy turns spent on units at 1–3 HP.
- Death-warded and phial fire rates; same-fight deaths after a save; runs
  saved per 100.
- **Win rate with and without a healer** (gate 10 points, 18.1) and with
  and without a Medicine member; floor-1 hero deaths Medicine-trained
  against untrained. Beside the gate, as reports: the three Herald arms,
  the healer count (0 / 1 / 2+) and the heal-source breakdown (18.1;
  classes design 12.3) **(change CL-9)**.
- Coin at camp 1 against 2 potions + 1 supply (≥ 32 coin).
- Hero level minus room level at fight start (under-levelled stealth
  heroes void the guards).
- Deaths by class HP tier (flat-bonus compression).
- Run time lost to deaths.

## 22. Staging

**(lead's call)**

| Wave | Content |
|---|---|
| **M1** | HP formula and bonus; Recovery; the S-3 crit rule; the hero death path and `RunEnded`; the out-of-combat and undetected-trap floors; potions at Recovery; hero kit 2 potions; rests and supply; 1 guaranteed floor-1 main-path potion, `supplyMainPathMin` 1 on floor 1 and HE-7; `combatActive` and the combat-end order; the floor-1 surprise rule; the guard rows, the floor-1 rows and `Floor1BossSustain` (after the surprise reconciliation); lethal tags; the danger band and end-turn warning; plates; a basic death recap |
| **M2** | Downed rules (inert, allied drag, occupancy, rest and camp get-up, once-per-fight Medicine); companions' HP; recruit kits; sundries with `healingSundryMin` (12.5) and `supplyMainPathMin` on floors 2–5; the camp top-up and its record; the floor-entry card; heal spells at Recovery; the rest-tool requirement; the Reprieve Phial at 334 **if this design is approved before M2 content lock** (consumables 14.10) |
| **Later** | Death-warded (with the tier-3 spell); Bolstered; the Weary lever (a switch, not a build); movement undo; the Last Stand and Mortal rungs; a legacy hook |

## 23. Risks

| Risk | Mitigation, report and lever |
|---|---|
| **Floor-1 surprise exceeds the guards** (the reference lair in room 2, an example read at enemies 10.2 room level 2 (**change FL-4**), at 0.87% under the review's model, the lowest legal hero 7–9%) | Reconcile the surprise model first; floor-1 ambush and lair budgets; the floor-1 cumulative budget (21.1 step 5); never loosen the row. The 17.1 rule covers rooms 0–1 only and does not reach the lair |
| **The solo floor-1 boss needs 3–5 heals** | The floor-1 solo boss row of ≤ 25% per round, the lowest-legal boss rows and `Floor1BossSustain` (21.1); boss-door reports calibrate its inputs; no drink limit there |
| **Supply gives 4–5 rests per floor,** so "limited" leaks | Rests gate median ≤ 3; lever order (5.7) |
| **Boss potion-chugging** on floors 2–5 | Boss potion report; `potion.laterInFightPermille` lever (off) |
| **The healer gap** (Slot Recovery heals) | Heals at Recovery per target; the 10-point gate (18.1) |
| **Ablative companions** | Medicine once per fight; downing reports; the Weary lever |
| **Danger band noise,** worst in party fights where every threat is assumed to pick the hero | Probabilistic bands; the warning fires only when a bounded candidate helps (17.4); the danger calibration report and its target; the party-fight warning lever (off); override report |
| **Hoarding** | Camp top-up; died-holding report; neutral recap |
| **Flat bonus compresses classes;** tanks feel soft | Deaths-by-HP-tier report; the tiers and bonus go back to the client (7.1) |
| **Companion HP equal to the hero's** against party weight 8 | The approved flat-HP-spent report must pass; lever `hp.companionBonus`, never the weights |
| **Late-floor guarantee is thin** (0.70× at party size 4) | Pre-authorised raise to 2 on floors 4–5 |
| **Availability is not delivery** (a low-HP party may not reach the potion or rest safely) | First-half placement; announced counts; interrupted-rest report; the safe-spot rule reopens only if that report fires |
| **Calibration targets missed** (Q11) | Reports (21.5); the lever order of 18.2; the client is told |
| **Seed reshuffle** | Once, before M1 content lock; logged |
| **Inert bodies look odd** (an explosion skips a body) | Accepted for fairness; tooltip |
| **Late deaths cost 1–2 hours** | Telegraphs, the phial; a Later legacy hook |
| **Under-levelled stealth heroes** void the guards | Level-gap report; the XP design keeps it at 0 or above |
| **SRD shapes from memory** (crit wording above all) | Check against SRD 5.2.1 before data |

## 24. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: `hp.companionBonus` (down only),
  `sundriesPerFloor`, `healingOffPathPermille`, report targets, the
  lowest-legal round-1 threshold (floors 2–5), the floor-1 rows' values
  and the `guard.*` keys (21.1), `sim.restRoundPermille`, the reserve
  Medicine lever.
- Placeholder wording of the floor-entry card, the warning, plates and
  recap within section 17; icons and the pulse.
- How the rest screen and the death recap lay out.
- **Not tuning:** the HP shape, `hp.adventurerBonus` and the HP tiers (Q5);
  the package's counts (Q3); the floors (Q6); the Medicine get-up limit
  and the rest and camp get-up (Q4); the retirements and the cap (Q7);
  `leaveHpPermille` 334 (Q8); the floor-1 surprise rule (Q9); the
  10-point healer gap (Q10); the Q11 targets; the crit rule, inert Downed,
  the instant-death denylist, Recovery's shape, the heal-per-target rule
  and the temporary-HP cap (the lead's); Recovery's 4 and 2 and the danger
  thresholds (the lead's numbers); the normative algorithms of 12.5, 17.3,
  17.4, 20.3 and 20.5.

## 25. Answers to the needs lists

### 25.1 Enemies 25, "Death and healing"

| Need | Answer | Section |
|---|---|---|
| Hero HP at level 1 of about 18 (the proposed flat bonus) | **18** for the reference hero (tier 6, Con +1): 11 + 7. The bonus persists at every level, so the 6.1 table matches exactly. Lowest legal is 16, covered by new guard rows **(agreed, Q5)** | 7, 21.1 |
| A crit damage rule (S-3) | **Adopted:** double dice for every unit, attack rolls only | 9 |
| Confirmation that the hero has no downed state | **Confirmed (agreed, Q1):** 0 HP ends the run; the guards' "reduced to 0" stands | 10 |
| Whether damage on a downed companion kills it | **No.** A Downed unit is inert: no damage and no harmful condition from any source; the allied drag stays legal. Invariant 2 is now also a core rule | 11.4 |
| A morale win counts as a win | **Yes** (M-1), for getting up | 11.2 |
| The Reprieve Phial: final leave-HP | **334‰ (agreed, Q8)** | 16 |
| The Reprieve Phial: whether it fires out of combat | Yes, any source; the floors come first, so it is never reached out of combat or on an undetected trap | 16 |
| The Reprieve Phial: owner order of Death-warded, Last Ward, Last-Breath Charm and the phial | Owner: this design. Death-warded → phial; Last Ward and Last-Breath Charm retired **(agreed, Q7)** | 15 |
| The Reprieve Phial: whether companions can die | **No (agreed, Q2);** no would-die coverage | 11.6, 16 |
| Dungeon generation (new requirements this design adds) | HE-7: at least 2 main-path containers openable with no check before the boss room, one in the first half, reserved constructively with one retry policy (M1); `mainPathIndex` per square for the surprise rule (M1); trap ids and detection snapshots (M1) | 12.4, 12.5, 17.1, 10.4 |

### 25.2 Consumables 24

| Need | Answer | Section |
|---|---|---|
| Confirmation that the hero has no downed state | Confirmed **(agreed, Q1)** | 10 |
| The crit rule (S-3) and level-1 hero HP (about 18) | S-3; 18 at the reference | 7, 9 |
| Whether companions can die (if yes, the phial also covers "would die") | No at launch **(agreed, Q2)**; no would-die coverage | 11.6 |
| Whether damage on a Downed unit kills it | No: inert | 11.4 |
| Whether anything lifts Downed | A win, Medicine (once per companion per fight), a completed short rest, camp **(agreed, Q4)**. Never items or heal spells; `liftsDowned` reserved | 11.2, 11.5 |
| Massive-damage or instant-death exceptions | No massive-damage rule; instant-death and save-or-die effects against party units are denylisted | 10.2 |
| The final leave-HP (25% against 1 HP plus Dodging) | 334‰ **(agreed, Q8)**; 1 HP plus Dodging rejected | 16 |
| Whether the phial fires out of combat | Yes, any source; moot under the floors. CO-16 closes | 16 |
| The owner split and order of Death-warded, Last Ward, Last-Breath Charm and the phial | Owner: this design. Death-warded → phial; Last-Breath and Last Ward retired **(agreed, Q7)** | 15 |
| Whether Last Ward is automatic | Moot: retired | 15.3 |
| At most one recharging drop prevention per unit per fight (the phial outside that cap) | Yes **(agreed, Q7)**; the phial outside the cap | 15.4 |
| **Recovery formula, required before M1 potion data** | 4 + 2L + Con, minimum 1; replaces `recoveryPlaceholder` | 8 |

### 25.3 Other deferred items

| Item | Answer |
|---|---|
| Main design 4.2, "gated on the death and healing design" | Ungated; rest and camp get-up and "never killed" added (DH-1) |
| Stat model 6, Recovery "grows with level and Con" | Formula set (8) |
| Stat model 10, the hero level-1 bonus | Flat 11 at every level, for every party unit (7.2) |
| Stat model 7.1, Medicine `treatProof` (gated) | Ungated; get-up once per companion per fight (DH-8) |
| Ability 6.6, Second Wind example | Fighter-type: regain Recovery; other classes a half-Recovery or other `rest` tool (12.1) |
| Ability 7.2, Medicine get-up and the talisman rows | Once per companion per fight; talisman rows ungated (DH-11) |
| Conditions 5.4, 9.2, 9.3, 14, 18.2, 18.9, 18.10 gated text | Ungated; chain final (15); Bolstered and Death-warded (14, 15) |
| Conditions 25, "the rest of death and healing" | Settled here (DH-24) |
| Armour 14.3 Last Ward | Retired (15.3) |
| Trinkets 13.5 Lifeline ("no revive nova") | Unchanged; counts toward Medicine's once per fight (DH-46) |
| Trinkets 13.7 Last-Breath Charm; 23; 24.3 | Retired (DH-47) |
| Items 15, companion death | Stays dormant (DH-36) |
| Items 17 row 3, starting kits (potions) | Hero 2, recruit 1 (DH-37) |
| Items 24, whether healing lifts Downed | No (11.5) |
| Stat model, weapons 26, armour 22 crit dependencies | S-3 settled (DH-6, DH-45, DH-48) |

## 26. Changes to approved documents

Applied on 2026-11-17, when the client approved this document. Each
change names the exact section it touches; ids are provisional. **Agreed, decided and
settled rows are flagged**; the client's Q1–Q12 answers approve the ones
that name a question, and the rest are confirmed with the document. Every
change removes the "gated on the death and healing design" marker in its
row.

**Check after applying:** a grep for "gated on the death and healing
design" over the specs folder finds nothing; a grep for "death and
healing design" finds only this document and dependency sections that
point to it as settled; a grep for "Last Ward" finds only retirement
notes, history (appendices, change logs of other designs) and this
document; and a grep for "25%" finds no remaining phial leave-HP.

**Narrowings recorded inside this design (no change id, no approved text
changes):** the party instant-death denylist; no healing per tick; heal
spells sized at Recovery per target; temporary HP capped at Recovery;
`liftsDowned` reserved; Second Wind's full Recovery only for the
fighter-type tool; `RetiredDropPrevention`.

**Main design (2026-11-15):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-1 | 4.2, the "run ends when the hero dies" bullet **(decided)** | Remove "(gated on the death and healing design)". Add: "A downed companion also gets up on 1 hit point when the party completes a short rest, and at camp; Medicine gets the same companion up at most once per fight; companions are never killed (death and healing design 11)." The decided text is otherwise unchanged | **Client (agreed 2026-11-17, Q1, Q2, Q4)** |
| DH-2 | 4.3, the noticing bullet, sentence "A side that is noticed without noticing back is surprised and loses its first turn" **(decided)** | Add: "Exception: the solo hero is never surprised in floor 1's first two main-path rooms (death and healing design 17.1)." | **Client (agreed 2026-11-17, Q9)** |
| DH-3 | 10, pointer paragraphs | "Hit points, Recovery, crits, the hero's death, Downed companions, the healing guarantee and drop prevention are settled in `2026-11-17-death-healing-design.md`; they are no longer pending (death and healing design 26, 27)." The "All numbers: hit points" bullet gains "(shape settled there)" | Lead |

**Stat model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-4 | 6, Hit points row **(agreed)** | "Flat adventurer bonus (11) + class amount per level (HP tier 6 / 7 / 8) + Constitution per level, at least 1 per level in total; never rolled; at least 1 per level; heroes and companions alike (death and healing design 7)" | **Client (agreed 2026-11-17, Q5)** |
| DH-5 | 6, Recovery row **(agreed)** | Adds "= `recovery.base` + `recovery.perLevel` × level + Constitution, minimum 1 (values in the death and healing design 8)". Writes the shape only; meaning unchanged | **Agreed row filled: Lead; confirmed with the document** |
| DH-6 | 6, "Natural 20 and natural 1" bullet | "If adopted by death and healing (enemies design 25):" removed; "a critical hit rolls all damage dice twice, for every unit" is the rule | Lead |
| DH-7 | 5.3, "Class supplies most hit points" **(agreed)** | Becomes "Class supplies most hit points over a run, and Constitution the smaller share; at level 1 the flat bonus is the larger share" | **Client (agreed 2026-11-17, Q5)** |
| DH-8 | 7.1, Medicine row **(agreed)** | `treatProof` sentence ungated. "Get a downed companion back up" gains "at most once per companion per fight" | **Client (agreed 2026-11-17, Q4)** |
| DH-9 | 10, Death and healing bullet | Points to the death and healing design; "a flat hit point bonus for the hero at level 1" becomes "a flat hit point bonus (11) for every party unit at every level" | **Client (agreed 2026-11-17, Q5)** |

**Ability model (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-10 | 6.6, Second Wind (illustrative) | "Heal 1d10 + level" becomes "regain your Recovery (the Medicine bonus excluded)". Adds: other classes' `rest` defensive tool (at most half Recovery, or a defensive or prevention effect) is a requirement on the classes design, inside 6.3's band | Lead |
| DH-11 | 7.2 rows: "Get a downed companion up: Medicine, trained only" **(settled)**; Treat (`treatProof`); Hand over, Take from a Downed ally, Retrieve | Medicine get-up: "at most once per companion per fight (a Lifeline get-up counts)". Treat, Hand over, Take and Retrieve rows ungated | **Settled row narrowed: client (agreed 2026-11-17, Q4)** |
| DH-12 | 15.2 On completion | Order: get up (Downed companions, on 1 HP) → Treat → Recovery + Medicine bonus → `rest` pools → Slot Recovery | Lead's call row |
| DH-13 | 15.3 Supply | Counts and cap unchanged. Adds `supplyMainPathMin` 1, the rests-per-floor gate and the lever order (death and healing design 5.7) | **Client (agreed 2026-11-17, Q3)** for the placement; Lead for the gate |

**Conditions design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-14 | 5.4, turn-start step 4 | Ungated (CN-15): the chain runs where damage lands; at 0 the hero ends the run, a companion is Downed | Lead |
| DH-15 | 5.5, harmful-ticks bullet and combat-end bullet **(the combat-end bullet cites a settled rule)** | Harmful ticks: adds "All damage to a party unit that resolves while no combat is active, and damage from an undetected trap in or out of combat, cannot take it below 1 hit point (death and healing design 10.4); monsters keep this tick rule". Combat end: "after any other end it stays Downed until Medicine, a completed short rest or camp"; adds "if the hero reaches 0 in the event that ends the fight, the run ends" | **Client (agreed 2026-11-17, Q4, Q6)** |
| DH-16 | 8, `treat` table row; 9.2 entry-points row "A talisman's Scarred" | Ungated | Lead |
| DH-17 | 9.3, save chain | Final: Downed discard → per-part steps, temporary HP included → sum → floors on the post-absorption total → the chain (death and healing design 20.3); Death-warded → talisman, each link declaring `recharging`; a capped recharging link is skipped without spending its charge; Last Ward removed; at most one recharging prevention per unit per fight, the talisman outside it; owner: the death and healing design | **Agreed Q2 order text edited: client (agreed 2026-11-17, Q7)** |
| DH-18 | 10.4 Hazard lethality; 11.6, Traps bullet | 10.4 becomes a hazard-design guideline under the party floor. 11.6 adds: "Damage from a trap the party had not detected cannot take a party unit below 1 hit point, in or out of combat; detected traps are ordinary damage" | **Client (agreed 2026-11-17, Q6)** |
| DH-19 | 14, lingering cap bullet and Scarred bullet | Ungated; the Weary lever's exemption recorded (off) | Lead |
| DH-20 | 15.1 Plate | "Bloodied" at 50%, "low" at 25% on party plates; Downed marker text; armed saves shown | Lead |
| DH-21 | 18.2 Downed **(settled entry)**: Effect, Duration, Sources, Interactions | Effect: inert (no damage, no harmful condition, no temporary HP; every forced movement except an ally's grapple-drag and every area skip it; not a unit for forced-movement stopping; greyed as a target of harmful party effects); does not block movement but no unit ends a move on it; after a non-win end the body stays until a party unit picks it up; current HP stays 0 while Downed (death and healing design 11.4, 20.5). Duration: "Ends on Medicine (trained, action, once per companion per fight), a won fight or a completed short rest, getting up on 1 hit point, or at camp" (5.5). "For the hero, the run ends" kept. Interactions ungated | **Settled routes kept, routes added: client (agreed 2026-11-17, Q2, Q4)** |
| DH-22 | 18.9 Bolstered, Death-warded (Later) | Bolstered: a grant at most the target's Recovery; not healing; never on Downed; class and spell sources only. Death-warded: the "rare charm" source removed; interactions ungated ("first link of the save chain, before the talisman; at most one recharging prevention per unit per fight") | Lead (Later entries; the charm removal follows Q7) |
| DH-23 | 18.10 Scarred, Sources **(agreed entry)** | "By either route (Medicine or a won fight)" becomes "by any get-up route (Medicine, a won fight, a completed short rest, camp, where camp clears it at once)"; talisman parts ungated | **Agreed entry edited: Lead's call row; rest route client (agreed 2026-11-17, Q4)** |
| DH-24 | 22, "Rules owned by designs that do not exist yet" bullet; 25, Death and healing bullet | "the rest of death and healing" removed from 22; 25 points to the death and healing design as settled | Lead |

**Enemies design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-25 | 6.1, the intro's second sentence ("The level 1 figure assumes the stat model's proposed level 1 HP bonus (dependency, section 25)") | Replaced by "Reference hero = class HP tier 6, Con +1, under 11 + level × max(1, tier + Con) (death and healing design 7); the lowest legal hero is tier 6, Con −1". First sentence and table unchanged | Lead |
| DH-26 | 6.4 Permadeath guards | The crit bullet becomes settled (S-3). Reference rows unchanged. New rows, all computed without party consumables: lowest legal hero (lowest HP + AR-20 lowest AC): crit ≤ 75%, round-1 < 2% *(illustr.)* on floors 2–5; floor-1 solo boss expected damage per round ≤ 25% *(illustr.)*; floor-1 solo boss at the lowest legal hero: worst spike ≤ 75%, no one-round kill even with one crit; the floor-1 cumulative round-1 budget ≤ 2.5% *(illustr.)*. "Before first turn" only after the surprise reconciliation (death and healing design 21.1). The sustain check `Floor1BossSustain` is **not** added to 6.4 (it reads consumables; death and healing design 21.1 step 6) | Lead's call rows |
| DH-27 | 10.5 step 3; 15.3 step 2 (CN-27 gated notes) | Ungated: the phial pre-roll now places the phial. Step 3 gains: "after the notable items and before the wand charges, the sundry and found-supply block runs in the order of the death and healing design 12.5" (guaranteed potions and supply first among the sundries, fixed draw counts). "A guaranteed draw is never substituted" added to the substitution sentence | **15.3's settled substitution sentence narrowed: client confirms with the document** (follows Q3) |
| DH-28 | 11.3, invariants 2 and 6; 10.4 Patrol row; 17.1 Lurker row; 25, the patrol-loop row | Main-path rooms are indexed from 0, the entry. 2: "Also enforced by the core: Downed units are inert (death and healing design 11.4)". 6: "no hidden lurkers in floor 1's main-path rooms 0–2"; adds "and the solo hero is never Surprised on floor-1 squares with main-path index 0 or 1 (death and healing design 17.1)". Patrol row and 25 row: "stays clear of main-path rooms 1 and 2 for the first 20 ticks". Lurker row: "never in floor 1's main-path rooms 0–2". Each keeps the stricter reading; no ban is narrowed | Lead; the invariant 6 exception **client (agreed 2026-11-17, Q9)** |
| DH-29 | 12.2, Surprise bullet **(settled)** | Adds the floor-1 exception of DH-2 | **Client (agreed 2026-11-17, Q9)** |
| DH-30 | 14.6, the end of the fight | Note: the hero dying in the event that ends the fight ends the run | Lead |
| DH-31 | 22, "Dependencies" risk bullet; 25, Death and healing table | "death and healing (crit rule, hero HP)" removed from 22; every 25 row answered (death and healing design 25.1) | Lead |
| DH-32 | 25, Dungeon generation table | New M1 row: "When laying out the main path, reserve at least 2 containers openable with no check in main-path rooms before the boss room, one in the first half; a run-time assertion and one retry with the dungeon stream at seed + 1 (death and healing design 12.4 HE-7)". New M1 row: "A `mainPathIndex` on every square, corridors taking the lower index of the rooms they join (death and healing design 17.1)". New M1 row: "Trap ids and a detection snapshot when a trap fires (death and healing design 10.4)" | Lead |
| — | 4 / 10.1 floor budgets | **Unchanged** | — |

**Items design (2026-11-16):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-33 | 5.1 tuning; 5.2 Belt row **(agreed row, CN-1)** | Talisman wording ungated; the new loot, kit and camp keys (death and healing design 20.1) | Lead (agreed wording kept) |
| DH-34 | 7.1, Hand over, Take from a Downed ally, Retrieve (CN-3) | Ungated | Lead |
| DH-35 | 12.3 `category` and `use.trigger`; 18.3 events; 19.1 validation; 20 staging; 21 risks; 24 dependencies | Ungated or pointed to the death and healing design; 24's "whether any healing lifts Downed" answered (no) | Lead |
| DH-36 | 15, the Downed-companion bullet **(settled)**; the conditional companion-death rule | "Stays Downed in the party with its gear until Medicine, a won fight, a completed short rest or camp". The conditional rule stays dormant | **Client (agreed 2026-11-17, Q2, Q4)** |
| DH-37 | 17, row 3 Starting kits | "Every hero class kit holds 2 healing potions on the belt; every recruit kit 1; the hero's camp top-up of 1 (death and healing design 12.4, 13.2)" | **Client (agreed 2026-11-17, Q3)** |
| — | 5.3 supply overflow; 9 leftovers; 10.3 vendor **(agreed)** | **Unchanged** | — |

**Consumables design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-38 | 4, decisions row 2; 5, the intro table's idea 2 row; 5.2 decision text **(agreed Q2)** | "Order Last Ward → Death-warded → talisman" becomes "order Death-warded → talisman"; "leaves 25% of max HP" and "leaves it on 25% of maximum HP" become "leaves 334‰ of max HP"; 5.2's "after Last Ward" removed; "gated on the death and healing design" removed | **Client (agreed 2026-11-17, Q7, Q8)** |
| DH-39 | 7.2 | `recoveryPlaceholder` replaced by Recovery = 4 + 2 × level + Con, minimum 1 (death and healing design 8); "has no approved formula yet" removed | Lead |
| DH-40 | 10, Sundries row and phial note; 16.1 catalogue | `healingSundryMin` 1 (drawn first; 2 on floors 4–5 only through the pre-authorised raise), `healingSundryMax` 3, `sundriesPerFloor` per floor, main-path first-half placement, the off-path lean for random potions, never substituted. "While the phial is gated" note removed. 12 gains the record "healing potion (camp issue)": the healing potion's effect and use, flag `unsellable`, channel camp top-up only (death and healing design 13.2) | **Client (agreed 2026-11-17, Q3)** for the counts; Lead for the rest |
| DH-41 | 14.1–14.4, 14.7–14.10 | Ungated. 14.2: the trap parenthetical becomes "(the floors of death and healing 10.4 run first, so it never fires on floored damage)"; the out-of-combat question closed. 14.3: order final, Death-warded → phial. 14.4: `leaveHpPermille` 334, Last Ward link removed. 14.8: the save-value term extends to Death-warded. 14.9: "Recommendation superseded: Last-Breath Charm retired as recommended, and Last Ward also retired (death and healing design 15.3, Q7). The cap reads: at most one recharging drop prevention (today Death-warded) per unit per fight; the phial is outside it." 14.10: ships in M2 if approved before content lock | **Agreed Q2 rows: client (agreed 2026-11-17, Q7, Q8)** |
| DH-42 | 18.1 tuning; 18.4 events | `talisman.leaveHpPermille` 334, a client number; `recoveryPlaceholder` removed; `sundriesPerFloor` reshaped per floor; new bare keys `healingSundryMin`, `healingSundryMax`, `healingOffPathPermille`, `supplyMainPathMin`, `heroKitPotions`, `recruitKitPotions`, `campHeroPotionTopUp` beside `offensiveSundryMin`; the phial's events unchanged | Lead; 334 **client (Q8)** |
| DH-43 | 19.1 validation; 19.2 run-time invariants and tests; 19.3 reports | New codes and reports (death and healing design 21.2, 21.5). 19.2: the invariant and the phial test read "Death-warded → phial"; the Last Ward test is removed. The 50% threshold and separate levers **unchanged**; moving `leaveHpPermille` goes to the client | Lead (19.2 follows Q7) |
| DH-44 | 20 staging; 21 risk row "The phial fires too often or too little"; 22 Not tuning; 24 Death and healing and Recovery bullets; 25.3 phial bullet; appendix CO-16 row | Answered; CO-16 closed | Lead |

**Armour design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-45 | 14.3, Last Ward row and its note (CN-23); 18 Staging, Later list; 22, Death and healing bullet | Last Ward removed from the Later table and listed as retired (death and healing design 15.3); 18: Last Ward removed from the Later list; 22: the crit rule settled (S-3) and the "Last Ward (Later)" clause removed (retired) | **Client (agreed 2026-11-17, Q7)** (a Later item; edits agreed Q2 text) |

**Trinkets design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-46 | 13.5, Lifeline Pendant row | "(no revive nova before the death and healing design)" becomes "counts toward Medicine's once per companion per fight" | **Client (agreed 2026-11-17, Q4)** |
| DH-47 | 13.7, Last-Breath Charm row; 18.3 reports; 20 Risks, "Unruled dependencies" row; 23, Death and healing bullet; 24.3, Rare charms bullet; appendix L2 row | Retired (Later; nothing shipped changes); 18.3: "companion deaths with and without Lifeline" becomes "companion downings with and without Lifeline"; 20: "Last Ward against Death-warded" removed; 23 answered | **Client (agreed 2026-11-17, Q7)** |

**Weapons design (2026-11-17):**

| # | Section | Change | Approved by |
|---|---|---|---|
| DH-48 | 26, Death and healing bullet | The crit rule is settled (S-3, death and healing design 9) | Lead |

**Agreed, decided and settled rows touched:** DH-1 and DH-2 add to decided
main-design bullets; DH-4, DH-7 and DH-8 change agreed stat-model rows
(Q5, Q4); DH-5 fills the agreed stat-model Recovery row's shape (Lead,
confirmed with this document); DH-23 edits the agreed conditions 18.10
Scarred entry (Lead's call, the rest route Q4); DH-11, DH-15, DH-21 and
DH-36 narrow or add to settled rows; DH-29 adds to the settled enemies
12.2 Surprise bullet (Q9); DH-33 touches the agreed items 5.2 Belt row
(Lead; wording kept, only the gated marker removed); DH-17, DH-38, DH-41,
DH-43 and DH-45 edit agreed Q2 text (Q7, Q8); DH-27 narrows a settled
sentence. Each names the client answer that approves it, except DH-5,
DH-27 and DH-33, which the client confirms with this document.

**Not changed:** the 6.4 reference rows; floor budgets (enemies 10.1); the
vendor list (items 10.3); supply counts, cap and price; `stackMax`
values; the phial's rarity, floor, channels, `offerChancePermille` and
`treatProof`; the Scarred size; the trained-only list (stat model 7.2).

## 27. Depends on systems not yet designed

- **Classes:** an HP tier (6 / 7 / 8) per launch class; each class's
  `rest` defensive tool, tagged `restDefensiveTool`
  (`MissingRestDefensiveTool`); heal spells at Recovery per target; the
  tier-3 Death-Ward-shaped spell (Later); which class may lean on healing;
  any `liftsDowned` proposal (client approval). Answered by the classes
  design (`2026-11-17-classes-concepts-design.md`, 8, 11.E): tiers
  8 / 7 / 6 / 7 (Vanguard / Shade / Arcanist / Herald); four half- or
  full-Recovery heal tools, one per class; the Herald the one healing
  lean, on its heal cards only; no `liftsDowned` proposal. **(change
  CL-13g)**
- **Companions:** kits with protector tools (guard, intercept,
  body-block); Medicine training spread; recruit kits with 1 potion.
- **Dungeon generation:** HE-7's constructive container reservation and
  retry policy (12.4); `mainPathIndex` per square for 17.1; trap ids and
  the detection snapshot for 10.4; containers known before the loot step
  (12.5). HE-7's reservation, retry sharing and `mainPathIndex` are
  answered by the floor layout design
  (`2026-11-21-floor-layout-design.md`, 6.1, 6.5, 11.2) **(change FL-3,
  FL-13, FL-15)**.
- **Camp and hub screens:** the top-up step and the floor-entry card.
- **Experience:** keep hero level minus room level at 0 or above
  (under-levelled stealth heroes void the guards).
- **Lead's checks before data:** every SRD shape in this document against
  SRD 5.2.1, the crit wording above all.

## 28. Open items for the client

### 28.1 This document

None. Approved by the client on 2026-11-17.

The approval covers the document as a whole: the new lead's calls, DH-27
(the narrowing of enemies 15.3's settled substitution sentence) and both
readings put to the client, which the client confirmed:

- **Q7 order:** the floors run **after** temporary HP (15.2, 20.3),
  because the floors test the summed total after resistances (HP 5,
  temporary HP 3, an undetected trap for 10 leaves HP 1);
- **Q9 rooms:** "floor 1's first two main-path rooms" are rooms 0 (the
  entry) and 1, so the rule does not reach the floor-1 lair in room 2
  (17.1).

### 28.2 Future items triggered by reports

Each comes back to the client only if a report or a later design
triggers it:

- `healingSundryMin` above 2, or above 1 outside floors 4–5, or the
  floors 4–5 raise on any trigger other than its own 5.5 report;
- the lever order of 5.7 reaching its second step (limited vendor supply
  stock, an agreed row);
- `leaveHpPermille` other than 334;
- `hp.adventurerBonus` or the class HP tiers;
- a missed Q11 target or a failed 10-point healer gate;
- a `liftsDowned` effect;
- the Later rungs (Last Stand, Mortal companions) and movement undo when
  their wave comes.

## Appendix A: how the review findings were resolved

**Numbers review** (ids NR-n; the last two rows cite the review's own sections)

| # | Finding | Resolution |
|---|---|---|
| NR-1 | Lowest-legal crit row at 2/3 breaks the approved L2 brute | ≤ 75% (21.1, DH-26) |
| NR-2 | Lowest legal hero ignores AC; surprise is the hole | Lowest HP combined with AR-20 lowest AC; surprise reconciliation first; Q9 (17.1, 21.1) |
| NR-3 | "Worst case can kill" fires at full HP | Probabilistic bands, < 1% / 1–20% / ≥ 20% (17.3) |
| NR-4 | Last Ward above half HP almost never fires | Retired (Q7) |
| NR-5 | Phial 40% and chained levers contradict consumables 19.3 and 14.7 | 50% and separate levers kept; leave-HP 334 (Q8) |
| NR-6 | "Never moved" contradicts approved Grappling | Allied drag legal (11.4) |
| NR-7 | A full-Recovery self-heal for every class doubles sustain | Fighter-type only; others ≤ half or another tool (12.1) |
| NR-8 | Floors 3+ run out of sundry room | Flat minimum 1; `sundriesPerFloor` [4–5] from floor 3 |
| NR-S1 | d6 Con −1 caster is not one-shot | Corrected in decision 5 |
| NR-S2 | Level-10 HP-share spread is 2.3×, not 2.15 | Corrected in decision 13 |
| Review §6.2 | Option (j) camp top-up | Adopted (Q3) |
| Review §7 | Under-levelled hero; rest order; out-of-combat timing; staging; difficulty ladder | Report; order stated (11.3); judged at resolution (10.4); 22; same guarantee on every tier |

**Judges (grafts)**

| Graft | Resolution |
|---|---|
| Lowest HP + AC rows, 75%, "before first turn" | Adopted, with the surprise reconciliation gate |
| Probabilistic band | Adopted; % in the tooltip |
| Phial 334, 50% threshold, separate levers | Adopted (Q8) |
| Downed inert with allied drag | Adopted |
| Retire Last Ward and Last-Breath; one recharging cap | Adopted (Q7) |
| Second Wind for the fighter only | Adopted |
| "Out of combat" at resolution | Adopted |
| Rest get-up | Adopted (Q4) |
| Camp top-up (j) | Adopted (Q3) |
| Main-path placement; overflow; HE-6 | Adopted; overflow uses items 5.3 |
| Mortal and Last Stand as Later rungs | Parked (6.4) |
| Boss chugging report | Adopted; lever off |
| Under-levelled report; M1 staging | Adopted |
| Get-up overlay (the graft's "rescue overlay"), get-up warning, recap with unspent resources | Adopted (17) |

**Fairness critic**

| # | Finding | Resolution |
|---|---|---|
| 1 | Floor-1 surprise contradicts the approved < 0.5% row | Reconcile first; row unchanged; Q9 (17.1) |
| 2 | Downed occupancy undefined | Does not block; cannot end a move there |
| 3 | Out-of-combat loophole; trap ambiguity | Judged at resolution; undetected traps floored, detected traps lethal in combat |
| 4 | Mid-fight Medicine is a trap | Once per fight (Q4); get-up preview with threats and band; suggester never proposes into Deadly. Lifeline without Prone kept as a lever only |
| 5 | Potion-chugging at bosses | Report; prepared visible lever (off); not on the floor-1 boss |
| 6 | Full-Recovery self-heal for all | NR-7 resolution |
| 7 | Danger band noise | Probabilistic; warning only when an action helps; override report; one threshold (200‰) |
| 8 | Thin companion stakes | Weary rule written; reports; Mortal rung; Q2 |
| 9 | Availability is not delivery | First-half placement; interrupted-rest report |
| 10 | Phial at 250 feels fake | 334 (Q8) |
| 11 | Last Ward dead | Retired (Q7) |
| 12 | The guarantee must be felt | Floor-entry card, counts only |
| 13 | Hoarding | Died-holding report; neutral recap; (j) |
| 14 | `healingSundryMin` schedules disagree | Flat 1 |
| 15 | Simultaneous death | Recorded (10.3); own-attack lethal tag |
| 16 | Late deaths | Legacy hook noted as Later |
| 17 | Under-levelled hero | Report |

**Balance critic**

| # | Finding | Resolution |
|---|---|---|
| 1 | The solo floor-1 boss is the choke point | New validation row; lever a lower boss row; kit stays at 2; no drink limit |
| 2 | Medicine get-up as an unlimited decoy | Once per companion per fight (Q4); reports |
| 3 | Heal spells vs "scroll = potion"; healer volume | Tier 1 = Recovery; tiers add targets; validation; the 10-point gate (Q10) |
| 4 | Supply, not potions, sets "limited" | Gate and lever order (5.7); told to the client |
| 5 | Level-up HP uncounted | `levelUpHp` report column, non-guaranteed |
| 6 | Medicine rest bonus near-compulsory on floor 1 | Report; reserve lever |
| 7 | A potion is worth less with depth | Flat 1 plus [4–5] room; pre-authorised raise on floors 4–5 |
| 8 | Companion HP against party weight 8 | The existing report must pass; lever `hp.companionBonus` |

**Consistency critic** (ids CC-Hn high, CC-Mn medium; not the milestones M1, M2)

| # | Finding | Resolution |
|---|---|---|
| CC-H1 | Rest and camp get-up touch settled rows; Scarred trigger undefined | Flagged (DH-15, DH-21, DH-36; Q4); Scarred by route defined (DH-23) |
| CC-H2 | Thresholds loosen an approved row; "before first turn" may break 10.2 | Reference row verbatim; separate lowest-legal rows; reconcile before rewording |
| CC-H3 | Phial levers; 334 vs agreed "25%"; trap text in 14.2; Dodging alternative | 50% and separate levers kept; 334 put to the client (Q8, DH-38); 14.2 amended (DH-41); CO-16 closed; Dodging rejected with reason |
| CC-H4 | Drag contradicts Grappling | Fixed: allied drag legal; nothing in approved text said drag was Later, so nothing is deleted |
| CC-H5 | Change list incomplete | Section 26 by exact section, plus the grep check |
| CC-H6 | "Drink it" for the hero contradicts ability 11.3 | No hero suggestions; the warning names no action; hoarding guard is reports and (j) |
| CC-M1 | HE-4 re-invents supply overflow | Cites items 5.3 and 9; no new rule |
| CC-M2 | Generator fallback breaks stream order and substitution | A constructive generation requirement with a run-time assertion and one retry (12.4 HE-7, DH-32); substitution exception recorded (DH-27, DH-40) |
| CC-M3 | Four schedules and key names | One key each: `healingSundryMin` (flat 1), `supplyMainPathMin` |
| CC-M4 | Mandatory self-heal pressures 6.3 | Requirement counts inside the 6.3 band |
| CC-M5 | Cleanup knock-ons | Cap meaning stated; agreed Q2 rows named; charm source removed; events defined |
| CC-M6 | Bonus scope departs from stat model 10 and 5.3 | DH-7 and DH-9 with Q5 |
| CC-M7 | Danger thresholds and knowledge set | 200‰; ability 8.3 set; unknown kits flagged, not estimated |
| CC-M8 | Floor scope | Party-only; monsters keep the tick rule; judged at resolution; 10.4 a guideline |
| Low | Naming | `downed.getUpAtRest`; "low" mark; `hp.adventurerBonus`; `talisman.leaveHpPermille` kept. Undo in combat only. Weary exempt from the lingering cap. Simultaneous death recorded in conditions 5.5. Second Wind excludes the Medicine bonus |

**Research gaps found and closed**

| Gap | Resolution |
|---|---|
| A Downed companion after a flee with no Medicine member is stuck | Rest get-up (Q4) |
| Sundries have no healing minimum | `healingSundryMin` (Q3) |
| Carriers are not reliable party healing | Guarantees count containers only |
| The reference hero is exactly 11 + 7L | HP formula (Q5) |

**Client decisions (2026-11-17)**

| # | Question | Answer | Where |
|---|---|---|---|
| Q1 | The hero at 0 HP | Yes: no downed or dying state; 0 HP ends the run (Instant). Last Stand parked as a Later assist rung | 6, 10 |
| Q2 | Companion death | No at launch; Downed is their fail state and Downed units are inert. Mortal companions parked as a Later Hard rung | 6.4, 11 |
| Q3 | "Limited but guaranteed" | The package G1–G6, random top-up leaning off-path, the pre-authorised raise to 2 on floors 4–5 | 5, 12.4, 13.2 |
| Q4 | Getting up | A Downed companion also gets up at a completed short rest or at camp; Medicine once per companion per fight | 11.2 |
| Q5 | HP | 11 + level × max(1, class band + Con) at every level for every party unit; `hp.companionBonus` separate; bands 6 / 7 / 8 (called HP tiers in this document, a name only) | 7 |
| Q6 | Out-of-combat and trap damage | Never below 1 HP for a party unit; undetected traps likewise; spotted traps lethal in a fight with a warning | 10.4 |
| Q7 | Last Ward and Last-Breath Charm | Both retired; order floors and Downed discard → temporary HP → Death-warded → phial; one recharging prevention per unit per fight. The steps before the chain run Downed discard → temporary HP (per part) → floors (20.3; lead's call, 15.2; reading confirmed by the client on 2026-11-17) | 15 |
| Q8 | Phial leave-HP | 334‰, replacing 25% | 16 |
| Q9 | Floor-1 surprise | No surprise of the solo hero in floor 1's first two main-path rooms, read as rooms 0 (the entry) and 1 (reading confirmed by the client on 2026-11-17) | 17.1 |
| Q10 | Healer class | Spread thin; one Recovery per target, higher tiers add targets; gap at most 10 points (client number) | 12.2, 18.1 |
| Q11 | Target death rate | Floor-1 hero deaths under about 5% of runs; most deaths on floors 3–5 at elites and bosses; a competent player wins about a quarter to a third | 18.2 |
| Q12 | Movement undo | Yes, Later, in combat only | 17.13 |
| — | Everything else in the proposal | Stands as written | Throughout |

## Appendix B: sources

- The ten parent documents, all in `docs/superpowers/specs/`: main 4.1,
  4.2, 4.3, 4.4, 10; stat model 5.3, 6, 7, 10; ability model 6.3–6.9,
  7.2, 11.3, 15, 20; conditions 5.4, 5.5, 9.2, 9.3, 10.4, 11.6, 14, 15.1,
  18.2, 18.9, 18.10, 22, 25; enemies 5.4, 6, 10.5, 11.3, 12.2, 14.6,
  15.3, 22, 25; items 5.3, 9, 15, 17, 24; consumables 4, 5.2, 7.2, 10, 14,
  18.1, 19–25; armour 14.3, 22; trinkets 13.5, 13.7, 23, 24.3; weapons
  15.1, 26.
- The lead's death and healing proposal (2026-11-17), synthesised from
  research, a framework, three death models, three judges, an economy
  variant, a numbers review and four critics, with the client's answers to
  Q1–Q12 on 2026-11-17 and confirmation that the rest of the proposal
  stands.
- Checked game references: Slay the Spire (potion drop rule), Hades
  (healing fountains), Battle Brothers and Wildermyth (surviving with a
  mark).
- From memory, not re-verified: 5E SRD 5.1 and 2024 rules (crits, death
  saves, Second Wind, Death Ward, potions); Pathfinder 2e; 13th Age; 4e;
  Dungeon World; Torchbearer and OSR games; Shadow of the Demon Lord;
  Cairn; Mausritter; Lancer; Darkest Dungeon; XCOM; Fire Emblem; Baldur's
  Gate 3; Divinity: Original Sin 2; Pathfinder: WotR; Into the Breach;
  Monster Train; DCSS; Brogue. Mechanism references only. SRD shapes wait
  on the SRD 5.2.1 check.
