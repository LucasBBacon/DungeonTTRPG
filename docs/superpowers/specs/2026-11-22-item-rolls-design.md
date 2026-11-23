# Item Rolls and Loot Tables: Design (Dungeon Generation, Segment 7b)

Date: 2026-11-22
Status: Approved, 2026-11-22
Parent (immediate): `2026-11-22-loot-economy-design.md` (loot economy design, "LE", segment 7a, approved 2026-11-22, **the key parent and the template**).
Other parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "CON"),
`2026-11-16-enemies-design.md` (enemies design, "ENM"),
`2026-11-16-items-inventory-design.md` (items design, "ITEMS", **the item framework**),
`2026-11-17-weapons-design.md` (weapons design, "W"),
`2026-11-17-armour-design.md` (armour design, "AR"),
`2026-11-17-trinkets-design.md` (trinkets design, "TK"),
`2026-11-17-consumables-design.md` (consumables design, "CN"),
`2026-11-17-death-healing-design.md` (death and healing design, "DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "CL"),
the stage-2 class designs `2026-11-18-vanguard-class-design.md` ("VG"),
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC")
and `2026-11-20-herald-class-design.md` ("HR"),
`2026-11-20-companions-design.md` (companions design, "CMP"),
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change ids are RT-n),
`2026-11-21-floor-layout-design.md` ("FL", segment 2),
`2026-11-21-room-terrain-design.md` ("ROOM", segment 3; its change ids are TR-n),
`2026-11-21-hazards-design.md` ("HZ", segment 4),
`2026-11-21-traps-locks-design.md` ("TL", segment 5) and
`2026-11-21-encounter-placement-design.md` ("EP", segment 6).

Items marked **(agreed)** are the client's answers of 2026-11-22 to the five questions this design raised (Q1–Q5, sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document. The client also said that everything else in the lead's proposal stands as written: Model B corrected ("B-fixed") with its grafts; the pipeline; the weight split with static-set divisors; lanes; one rolled line with rolled-only dormancy and telling names; Scions; memory and relaxation; the Triad offers with the hero guarantee; the Due additions; the Wax-Key sibling; carried loot with `showsInUse`, the elite preference, KinSwap, the name banner and the roster flags; signatures with forms and second forms; determinism (`lineRoll`, `curseGate`, the fourth fallback entries); `theme.loot` records and the theme tables for every theme; the guards (IR-16); content requests and V14 warnings with playtest-blocking reports; V14 `ItemRolls`; the reports; the card budget; art and audio; the new mechanics adopted, held and rejected; the numbers and fixes (toy, with caveats); staging; ownership and hooks; the binding requirements; IR-1 to IR-23; the told-plainly items; the risks; and the appendix resolutions and names. The client approved this document as a whole on 2026-11-22.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-22, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-22. Items marked **(lead's call, after the check)** were made when the written document was checked; they are listed in 14.1, the client confirmed them all on 2026-11-22, and they never override a client answer. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change IR-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-22 (with Q1–Q5 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, a judge or a critic, or state an unwelcome forecast, and were told to the client in plain words (14.1, including those told after the check); the client confirmed them all on 2026-11-22. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q5 of this document (2026-11-22), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-22 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(lead's call, after the check)** | A judgement call made when the written document was checked (builder, consistency, play and art/UX lenses); never overrides a client answer; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(change IR-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, a judge or a critic, or states an unwelcome forecast; told to the client in plain words (14.1) and confirmed by the client on 2026-11-22 |
| **(hook)** | Owned by another segment or design; 7b states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (8, 9). Never binding |

**The client's brief.** Dungeon generation segment 7b: **item rolls and loot tables**. 7a (approved) owns the economy and curves, 7b turns a deck card into a specific item, 7c places sources. Shops stay hooks. 7b owns: the selection pipeline (category → family → base → rarity variant → rolled properties); theme loot tables and how much theme bias; quality and random properties; smart loot (strength, companions, hard filters against soft weights, the Dream's Due, never offering unusable items in offer sets and the Viaticum); duplicates, family caps and uniqueness; signature and boss items; dreamed options; Wax-Key and stepped identities; carried loot; identification and how loot is read; curse, heirloom and unlock hooks; determinism; data, validator and simulator; and the hooks to 7c and segment 10. The client had no specific considerations and asked for the usual research, making loot exciting where it is cheap. Standing guidance: not an exact 5E port; not limited by quantity; the art team is eager; combat is key; contest the client or approved views where warranted.

The client's five answers (Q1–Q5, **agreed**, 2026-11-22) are in 0.2: **nothing chosen or guaranteed is ever unusable, while field finds are steered softly (Q1); every gear find in a run is a new item, and once-per-run items are generated at most once (Q2); offers, the fourth option and the Dream's Due are shown identified (Q3); the theme reaches loot through presentation, hands and signatures, never the draw, with Element Echo held for an M3 decision (Q4); and an unarmed boss guards its signature in its lair (Q5).**

**Change prefix.** This design uses **IR-n** ("item rolls"). A case-insensitive whole-word grep for `\bIR-[0-9]+` across the 24 approved documents finds 0 hits. This document issues one register, **IR-1 to IR-23** (13); no id has two meanings. The research and model drafts used colliding local numbers (four IR-1…22 sets and a theme block IR-40…52); all are renumbered into this register (A.6).

**Validator.** **V14 `ItemRolls`** is free: V1–V7 RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE; a grep for `\bV1[4-9]\b` finds 0 hits.

**Local id schemes** (each whole-word grep-checked, 0 hits): tensions **IX1–IX24**; Appendix A findings **IF** (fun), **IB** (balance) and **IK** (consistency); toy failures **IT1–IT15** (8; the drafts numbered them F1–F15, which read as floors, so they are renamed **(new lead's call)**); this document's client questions **Q1–Q5** (local, as in every parent).

**Names are placeholders** until the client naming pass; every new name was grep-checked case-insensitively as a whole word across the 24 approved documents (A.5). The check forced these: "Hallmark" (three meanings across the drafts) → **Patina** for the theme trim, and maker houses dropped; "Stray" (EP K4 *The Strays*) → **Wayward**; "Kin/Akin" (beside Kinward and Kindred Glint) → **Plays with** and `carriedFamilies`; "Lineage" (the registers' term of art "agreed lineage") → **Scion**; "unique" (already defined by CN 14.1) → new flag **`oncePerRun`**; `useTag` (CN's `use` namespace) → **`effectTag`**; "Strongman's Barbell" (Strongman is a Carnival roster type) → **Ever-Lifted Barbell**; "tally" stamp (Tally reserved, LE A.5) → **cell number**, and after the check → **striped shirt** (stamps are pictograms, 6.2). While writing, two more **(new lead's call)**: the draft mechanic name "Two Ways" (an ordinary phrase with 6 hits in 6 documents) is dropped, the mechanic is called the **Wax-Key sibling**; and the comparison query is named **`ItemVerdict`** (0 hits), because "verdict" is an ordinary word in 19 approved documents.

**Numbers in this document are illustrative** unless a rule says otherwise, and are marked *(illustr.)* where they first appear. Every rate and share comes from the 7b toy (8, Appendix B); the M2 simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile, unlocks, ladder), **M4** breadth, **Later** **(settled, main design 9)**.

Abbreviations: F1–F5 = floors 1–5; C / U / R / VR = Common, Uncommon, Rare, Very rare; **VP** = value points (ITEMS 12.6); "the Due" = the Dream's Due (LE 6.6); **S0** = the static legal set of a card (4.1); ‰ = permille; VG, SH, AR, HR in the number tables = solo Vanguard, Shade, Arcanist, Herald; P4 = a party of 4.

---

## 0. At a glance

### 0.1 The lead's answers to the brief

**The design in one line:** every notable is an **authored record**, chosen by **one flat, lane-split weighted draw over usable records**, carrying **at most one rolled verb line that its name reads aloud**; the theme lives in the hands that carry loot, the bosses' signatures and the item's look, **never in the draw**.

- **Authored against random.** Authored first, with a small, readable random part. An item is an authored record (family, base, fixed lines); a U or R gear record may carry **one** rolled line from its family's approved equal-value table (ITEMS 12.3, 12.10; TK 8.2; AR 5.5). **No numeric ranges, no greater affixes, no affix piles, no attunement** (told plainly: the brief's "random affixes with ranges" breaks value = budget, the buff test and band scaling; variety is in *kind*, never size). Fixed lines are always obvious, so the only line that can ever be dormant is the rolled one, and the name always tells it ("Vowkeeper of the Whetstone", "Strider's Boots of ?"; every rollable epithet is a suffix, lead's call, after the check).
- **Smart loot strength.** Keep the approved soft bias, **requiresBias 3 (4 on boss-card options)** (settled). Add a closed **build-lane** vocabulary (one lane per record) and one shared **`UsableBy`** query. The usability floor is **hard** on everything chosen or guaranteed (the Due, fit premiums, the arms floor, every offer option and the fourth, the Viaticum, both Wax-Key identities, VR cards) and **soft at 100‰** *(illustr.)* on C/U/R field cards **(agreed, Q1)**. Expect a solo hero to be on-build about **65–76%** of the time, with **24–36% usable "wayward" finds** (toy, illustr.), not the 83–100% the drafts claimed (told plainly). That is healthy: wayward finds are where surprise and build drift come from. An adaptive bias is held as a null lever.
- **Theme bias.** **0‰ on identities, category weights, lane weights and rolled lines** **(agreed, Q4)**. This is settled five times (RS 6.4 LootDoorInvariant, RS 9.1 "theme-keyed loot" rejected, ENM 15.3, TK R-23, LE 6.10 item 7), and the toy shows a lane-preserving ×1.5 tilt adds **+0.02 kin items per floor**: imperceptible (told plainly: the brief's theme loot tables are delivered as presentation, hands and signatures). The theme reaches loot through: theme rosters visibly carrying notables; KinSwap; per-boss signatures; an **"Answers here"** mark; **Patina** trims; a theme stamp; a provenance line and one **Reverie** line. **Element Echo** is held for an M3 client decision, default off (IR-19, agreed, Q4). The draft's theme weight tilt is **rejected**, not held.
- **Duplicates and uniques.** Every gear and trinket find in a run is a new name: a record already **generated** (any gear or trinket identity, dreamed offer options and both dual identities included; ghosts and signatures excepted, 4.7) never returns at its tier or below (Scion line, IR-6, **agreed, Q2**). A new flag **`oncePerRun`** means "generated at most once per run, dreamed options included"; CN's `unique` keeps its approved meaning. Memory counts **generation**, never what was shown or what the player did, so LootDoorInvariant holds. `oncePerRun` records are never placed where they might never be presented (the hidden fourth option, dual-identity caches).
- **Offer sets.** Every width-3 offer is built in slots: **Yours** (the focus member's lanes), **Theirs** (another member, or for a solo hero another category or effect), **Wayward** (usable, off every lane, preferring a line the party lacks), plus the pre-drawn **fourth**. Families are always distinct, categories or effect tags distinct where legal, at most 1 damage option. **Every option is usable**, with a per-option fallback (replacing the approved all-or-nothing boss-card rule) and a hero guarantee. Offers, the fourth and the Due are **shown identified** (Plain Gifts, **agreed, Q3**).
- **The Due and the Viaticum.** The Due keeps LE 6.6 exactly (target, slot, lowest card, strict upgrade, F4–5 sidegrade, waiver); 7b adds the two-hander and trinket-pair test, counts items sold at camp as still dealt, and holds a lever to start the sidegrade on F2 if the corrected simulator still shows high waivers. The Viaticum keeps its approved pool, gains slots and effect-tag distinctness, and its fallback becomes "usable gear at ordinary weights".
- **Carried loot.** Identities stay theme-blind and come from the carrier-legal subset. Gear whose lines are visible in use (`showsInUse`) weighs ×2 on carried cards, and the elite's gear card prefers it, so the party watches the prize fight them. A carrier's first use of the item in a fight shows the item's name in a floater (never the boss banner). A draw-free **KinSwap** remap lets same-rank carriers trade notables so the Turnkey swings the mace you will pick up.
- **Signatures.** One authored form per top tier a boss can need, a value-1 line that echoes the boss's fight or kit, a universal-chassis signature in every theme, draw-free second forms where usability is low. **An unarmed boss guards its signature in its lair; its kit is unchanged** (M1: Old Ragged guards Bellwarden's Mail) **(agreed, Q5)**; worn or lodged signatures are an M3 option.
- **Identification and comparison.** ITEMS 11 is kept for field finds (passive Arcana, free camp identify, equipping reveals nothing). Per member, the core shows one verdict: **▲ upgrade, ⇄ another way, = redundant, ▽ lesser, ✕ cannot use**, never by hue alone, plus at most two marks. "=" is reserved for genuinely redundant items, so a distinct sidegrade never reads as trash.
- **Curse and unlock hooks.** Curses (Later) are authored **cursed twins**, gated by a reserved **2-draw per-floor** `curseGate` consumed from M2, so switching curses on changes no seed; barred from offers, the Viaticum, camp, the Due, fit premiums, signatures, heirlooms, rings and F1. Heirlooms (M3) sit outside pools. Unlocks (M3) declare an existing lane or bring ≥ 3 records per tier their new lane enters.

### 0.2 The client's five decisions (agreed, 2026-11-22)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Should loot ever hand you something nobody in the party can use? | **A.** A **hard** usability floor on everything chosen or guaranteed (the Due, fit premiums, the arms floor, every offer option and the fourth, the Viaticum, both Wax-Key identities, VR cards); a **soft** 100‰ *(illustr.)* on C/U/R field cards (4.3) | B: a hard floor on every draw (fewer surprises and distinct builds; martial Rare cards fall to tonics on the M2 roster); C: as approved, soft weights only, boss cards may show unusable options | IR-2 ⚑, IR-8 ⚑ |
| Q2 | Should every gear find in a run be a new item? | **A.** A record already generated never returns at its tier or below (Scion-line memory); `oncePerRun` records are generated at most once, dreamed options included; memory counts generation (4.7) | B: `oncePerRun` items only (signatures, VR, named Rares); C: as approved (owned-base and same-floor rerolls only) | IR-6 ⚑ |
| Q3 | Should offers and the Dream's Due be shown fully identified? | **A.** Offers, the fourth option, the Viaticum and the Due are generated identified (Plain Gifts); field finds keep ITEMS 11 (4.14) | B: show only the dormant line's kind ("? a back line"); C: as approved (a choose-one may hide a line) | IR-9 ⚑ |
| Q4 | How should themes show in loot? | **A.** 0 rules bias; presentation, hands and signatures (Patina, stamp, provenance, Reverie, "Answers here", carried loot, KinSwap); Element Echo held for an M3 decision, default off (4.4, 5) | B: adopt Element Echo in M3 now; C: reopen theme-keyed weights | IR-15, IR-19 ⚑ |
| Q5 | What does an unarmed boss drop? | **A.** It guards its signature in its lair; its kit is unchanged (M1: Old Ragged guards Bellwarden's Mail); worn or lodged signatures are an M3 option (4.13) | B: worn or lodged signatures in M2, with every affected boss re-validated | IR-13 ⚑ |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | **Pipeline:** card → static legal set → run-state exclusions → usability → target filter → weights → one identity draw (9 reserved, filter-first with padding) → a 4-draw `lineRoll` block → draw-free finishing. Category, lane, family and record are factors of **one** draw; an empty set relaxes the socket's own target first, then the IR-7 rungs (4.1, 4.2, 4.7 item 8) | M1 (filter-first), M2 | lead's call; IR-2 ⚑, IR-3, IR-6 ⚑, IR-7, IR-14 ⚑ |
| 2 | **Weights:** `categoryWeight × 1/|lanes| × 1/|families in lane| × 1/|records in family| × bias × …`, divisors computed on the **static** legal set. Fixes the approved normaliser's arms over-share (49–61% against about 32%) (4.2) | M2 | lead's call; IR-3 |
| 3 | **Lanes:** one `lootLane` per record, namespaced `lane.<group>.<x>`; trinkets follow TK's agreed jewellery lanes (4.3) | M1 data, M2 | lead's call; IR-1 ⚑ |
| 4 | **Usability:** hard on chosen, guaranteed and VR draws; soft 100‰ on C/U/R field cards (4.3) | M2 | **agreed, Q1**; IR-2 ⚑ |
| 5 | **Item shape:** ≤ 1 rolled line; fixed lines obvious; rolled-only dormancy (4.6) | M2 | lead's call; IR-4 ⚑ |
| 6 | **Scions:** a higher-tier sibling that contains its parent's lines plus one ("+1 line over your Vowkeeper"); the trinket Scion exception to family cap 1 (4.5) | M2 | lead's call; IR-5 ⚑ |
| 7 | **Memory:** Scion-line-once at tier ≤ T from generation outputs; `oncePerRun`; dedupe key base ?? family (4.7) | M2 (Rare staged) | **agreed, Q2**; IR-6 ⚑ |
| 8 | **Relaxation:** the socket's own target relaxations, then memory → category re-weight → tier-stable (logged `SubstitutedVP`) → supply or tonic, per a socket × rung matrix; same-floor keys are never lifted; the tier never moves silently (4.7) | M2 | lead's call; IR-7 |
| 9 | **Offers:** Yours / Theirs / Wayward + fourth; per-option usable fallback; hero guarantee (4.8) | M2 (Wager, Twin M3) | lead's call; **agreed, Q1** (usability); IR-8 ⚑ |
| 10 | **Plain Gifts:** the Due, every offer option, the fourth and the Viaticum shown identified (4.14) | M2 | **agreed, Q3**; IR-9 ⚑ |
| 11 | **The Due:** LE 6.6 kept; two-hander and pair test; camp sales count as dealt; F2 sidegrade held as a lever (4.9) | M1 (F1), M2 | lead's call; IR-10 |
| 12 | **Wax-Key sibling:** same category, different family, preferring a different lane; the lock card names the category (4.11) | M2 | lead's call; IR-11 ⚑ |
| 13 | **Carried:** `showsInUse` ×2, elite preference, KinSwap, the carried-use name floater, the Silk and Supper elite roster fixes (4.12) | M2 | lead's call; IR-12 ⚑ |
| 14 | **Signatures:** one form per top tier needed; ≤ 3 lines with a value-1 echo of the boss's fight or kit; ≥ 1 universal-chassis signature per theme (M2+); a draw-free second form wherever usable rate < 70%, required up front for specialist chassis; **unarmed bosses guard their signature in the lair** (M1 Old Ragged guards Bellwarden's Mail) (4.13) | M1 / M2 / M3 | lead's call; **agreed, Q5**; IR-13 ⚑ |
| 15 | **Determinism:** `lineRoll` (line, alternate, element, charges), `curseGate` (2 per floor), fourth and hero-guarantee fallback entries; no approved parent position moves (4.17) | M1 reserve, M2 | lead's call; IR-14 ⚑ |
| 16 | **Theme:** 0 rules bias; `theme.loot` record for display and validation; Patina, stamp, provenance, Reverie, "Answers here" (4.4, 5) | M2 / M3 | **agreed, Q4**; IR-15 ⚑ |
| 17 | **Element Echo** held for an M3 client decision, default off (4.4) | M3 option | **agreed, Q4**; IR-19 ⚑ |
| 18 | **Guards:** a Rare-tactical per-floor cap on F3–F4 and the tier-4 scroll lever defaulted to 1 while Rare gear is thin (4.7 item 9) | M2 | lead's call; IR-16 |
| 19 | **Content:** Rare gear (≥ 3 per lane per tier), ≥ 2 VR per lane group, value-2 majors for blades, axes, polearms and spears, 2–3 Rare neck families, Herald tier-2 and tier-3 scrolls. V14 **warns** and names each gap; it errors only on cards with no legal path (told plainly: M2 stays thin until content lands; LE 18 accepted this) (9.4) | M2 | lead's call |
| 20 | **V14 `ItemRolls`** and 7b reports (9.2, 9.3) | M1 static subset, M2 | lead's call; IR-21, IR-22 |
| 21 | `effectTag` on scrolls, wands and notable flasks (4.10) | M2 | lead's call; IR-23 |
| 22 | Curse, heirloom and unlock hooks (4.15, 4.16) | M3 / Later | lead's call; IR-20 |

### 0.4 Intent, goals and anti-goals

The approved documents fixed *how much* loot a floor holds and *at what tier* (LE), *what* an item is (ITEMS 12), *what* lines each category may carry (W, AR, TK, CN) and *that* loot never reads the theme or the door (RS 6.4). None of them said which record a card becomes, how a rolled line is chosen, how an offer avoids three of the same thing, how a find stays new, or how a theme shows in loot without becoming loot shopping.

The answer in one sentence: **one deterministic pipeline turns every deck card into an authored, usable-where-it-matters record with at most one rolled line, and the theme dresses the result and the hands that carry it, never the draw.**

- **Goals:** every chosen or guaranteed item usable; every gear find new within a run; offers that are three different decisions; a roll the player can read from the name; a theme the player can see on every floor; seeds that never move when content, doors or options change; a validator that names every content gap.
- **Non-goals:** the economy, tiers and counts (7a); sources and placement (7c); shop presentation (camp and hub); the screens themselves (segment 10).
- **Anti-goals:** numeric ranges and affix piles; theme-keyed weights or any door-dependent identity; hidden lines inside a choose-one; pity counters; coin or VP created outside the deck; build quotas by class (LE 6.8).

---

## 1. How games generate items

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Diablo 2 | Item level, nested treasure classes; magic 1+1 affixes, rares up to 6; authored uniques, sets, runewords | Authored chase items make the stories | Affix piles that are about 99% vendor sorting |
| Diablo 3 (Loot 2.0, 2014) | Smart loot, about 85% rolled for your class (wiki/forum figure); fewer, better drops; build-defining legendaries | A strong, stable on-build share; excitement in an authored verb, not stats | Multiplier power creep; set-dictated builds |
| Diablo 4 (S4 "Loot Reborn", 2024) | Rares 2 affixes, legendaries 3; greater affixes visibly marked; conditional stats consolidated | Fewer lines; the special roll visible on the card | Numeric "greater" rolls (break value = budget) |
| Path of Exile | ilvl-tiered affixes, up to 3+3; base- and slot-locked pools; deep crafting | Pools locked by family keep rolls legal | Loot filters as a requirement; crafting depth |
| Grim Dawn | Monster infrequents: named items tied to named monsters | Tie an item to a monster that visibly uses it | Volume, clutter |
| Borderlands | Guns from parts; maker predicts behaviour from the silhouette | A glanceable line glyph and a name that predicts behaviour | Loot piñata, gear checks |
| Hades | Fixed pools per god; choose 1 of 3; duo boons with prerequisites | Coherent, distinct triples; one option pushes elsewhere | — |
| Slay the Spire | Class pools; 3-card rewards; relics leave the pool once obtained; boss relic trade-offs; rare pity offset (−5%, +1% per common, StS wiki) | Class pools; seen-removes for named items; boss triples | Pity counters (DH 5.4 rejected; the 7a deck already fixes value variance) |
| Dead Cells | Random affixes plus quality; cursed chests; blueprints unlock into the pool | Unlocks join the pool | Minor affixes as noise |
| Binding of Isaac | Pools per room type; seen items never repeat; empty pool → filler | Source-keyed pools (7c); never-twice; a defined filler | — |
| Enter the Gungeon | Quality D–S by chest; a synergy icon when a pair is held | A data-driven "Plays with" mark | — |
| Risk of Rain 2 | No survivor bias, stacking items, about 30+ per run | — | No bias works only with stacking and volume; we have slots and about 34 notables |
| Brogue | Vaults: choose 1, the rest lock; runics hidden until learned | The vault is our offer set | Hidden lines inside a choose-one |
| DCSS | Unrandarts (authored, beloved); randarts; identification steadily cut (from memory, unsure of current rules) | Authored named items; less identification busywork | Random randart soup |
| NetHack | BUC curses; price and altar identification | Curse knowledge as a skill job (our Arcana) | Unknowns that kill outright |
| Darkest Dungeon | Class-restricted trinkets with trade-offs; ancestral pairs | 2-piece pairs only (Later) | Class locks with thin parties; large sets |
| Baldur's Gate 3 | All items authored and placed; synergy through shared conditions | Authored records plus a shared condition vocabulary | Static, route-dependent builds |
| Divinity: OS2 | Level-scaled random gear | — | Everything obsolete each level |
| Pillars of Eternity II | Soulbound items unlock lines through use | Deeds as display (CMP 8.7 extended) | Items that grow (breaks value = budget) |
| Monster Train, Wildfrost, Inscryption | Clan/tribe pools; one-icon modular sigils | Lanes as drafting identities; the rolled line as one sigil | — |
| D&D 5E (2014 DMG) | Tables A–I by rarity; special features (creator, history, minor property, quirk); sentient items; curses hidden from Identify; attunement 3 | Display-only provenance and quirks | Control loss; curses as gotchas; attunement (slots and family caps already do its job) |
| D&D 2024 DMG (moderate confidence) | Rarity then theme (Arcana, Armaments, Implements, Relics) | Theme changes kind, not power — our category multipliers already do this | Theme keyed to the dungeon door |
| Pathfinder 2E | Fundamental runes (curve) vs property runes (variety) | Our split: tiers and bands are the curve, lines are variety | — |
| 13th Age, Shadowdark, Mörk Borg, Dungeon World | Quirks; benefit + cost + personality; terse grim d-tables; bespoke items with drawbacks | Grim Surreal Reveries; the cursed-twin template | Rules-bearing quirks |
| Torchlight | Enchanting at vendors | — | Random upgrade services; coin as a reroll sink |

Sources: Diablo Wiki (Smart Loot, Loot 2.0); Gamespot and Wowhead (D4 Season 4); Maxroll; Slay the Spire wikis; Crawl forum and DCSS changelog; D&D Beyond and EN World (2024 DMG); Archives of Nethys. The rest is from memory; doubtful points are marked.

---

## 2. The current-systems audit

### 2.1 What the approved documents fix for 7b (settled unless changed by a flagged IR)

- **7b never chooses a tier.** The deck, the step rule and camp grading fix it (LE-1; ENM 15.3 step 1). An illegal (tier, category) pair re-weights the category, never the tier (LE 6.2).
- **Value = tier budget exactly:** C1 / U2 / R3 (F3+) / VR4 (F4+, perFloorMax 1) (ITEMS 12.5). Every rollable table has one `entryValue` (19.1 rule 2). ≤ 3 properties, one line each; the buff test; bands b1/b2/b3; no numeric ranges (ITEMS 12.4; W 18.1). The only pre-rolled number is wand charges (CN 13.6).
- **Shapes already approved:** ITEMS 12.10 Ember Longsword (fixed + 1 rolled minor); TK 8.2 (C obvious; U obvious + 1 roll or one major; R amulet major + rolled minor; rule 28); AR 5.5 (pieces C/U only; U adds 1 location roll). M1 notables are fully authored (W 18.6; AR 14.6).
- **Weights (LE 6.2, LE-17):** `categoryWeight[f][cat] × (onParty ? requiresBias 3, 4 on boss-card options : 1) × anyCompanion × 1/|lane|`. Filters (Due, fit, arms floor) come before weights.
- **Rerolls (ENM 15.3 step 2, agreed):** an owned base at ≥ quality and a base already drawn this floor are rerolled; family caps per run (weapon, armour, shield, focus, scroll, wand 2; trinket, talisman 1); ≤ 8 rerolls, then a `substituted` supply or tonic; a guaranteed draw is never substituted (DH-27). ENM 20.1 fails a floor where two notables share a base.
- **Theme-blind loot (settled):** RS 6.4, RS 9.1, ENM 10.5 step 3, ENM 15.3, TK R-23, LE 6.10 item 7, the bring-the-key anti-goal. The one theme read is AR 14.3's Resistant (element) on authored notables, through one always-consumed draw r ∈ [0,720).
- **Identification (ITEMS 11, agreed):** ≤ 1 dormant line, U+ gear and trinkets; passive Arcana threshold; free camp identify (client decision); equipping reveals nothing; consumables always identified.
- **Offer sets (LE-3, LE-4, LE-19):** count once; dreamed options outside VP and caps; the fourth pre-drawn from M2; the boss-card fallback fires only when **no** option is usable; the Viaticum redraws each unusable option.
- **Signatures (I-9, LE-3, ENM 15.2):** top legal tier + one authored property, no cost add, never cursed; F5 Memento at 0 VP; boss carries "if armed".
- **Determinism (LE 6.10):** keyed `lootPlan` grandchildren; 9 draws per identity; parent order unchanged; ghost draws for the Due and arms floor (retired by change SV-5, M1: parent step 3 is keyed per entry block, so no position is held; seeds and validation design 4.3); LootBind draw-free.
- **Curses (Later), heirlooms (M3), unlocks (M3)** as in ITEMS 13, 14 and LE 6.12.
- **`unique`** already means "at most 1 owned by the party" for the Reprieve Phial (CN 14.1, record rule 10).
- **Unarmed elites** guard a lair cache instead of carrying ("Guards: cache", ENM 15.2 Elite row); 7b extends this precedent to unarmed bosses, in a separate, unranked signature cache (4.13, agreed Q5).

### 2.2 What the approved documents owe 7b

LE 6.13 items 1–12 (all met; 12 shows how): a record per (tier, category) card with re-weighting; the Due filter; the fit filter; distinct triples, the fourth, the Twin pair and usability fallbacks; Wax-Key identities; camp graded N+1, never VR; the weight composition; ≥ 3 per lane per tier; a tier-stable substitution path; the phial swap; carrier-legal and elite gear-only subsets; content requests. Also owed: LE 3.2's container contents and reports (ITEMS 19.3, CN 19.3, TK 18.3); ITEMS 16's theme materials for unarmed enemies (Later hook); TK 18.3's element de-weighting.

### 2.3 Tensions and resolutions

| Id | Tension | Resolution |
|---|---|---|
| IX1 | Brief asks for theme loot tables; theme-keyed loot is settled out | 0 rules bias **(agreed, Q4)**; theme in hands, signatures and presentation (4.4, 5); Element Echo held (IR-19). Told plainly |
| IX2 | "Random affixes with ranges" vs value = budget, entryValue, bands, buff test; "affix" is ENM's elite term | ≤ 1 rolled **line**, no ranges (4.6). Told plainly |
| IX3 | Roster wall: about 30 Rare draws per run vs 0 Rare weapons, shields or foci in M2 | Content requests (9.4); tier never moves silently (IR-7); tactical guard (IR-16); `LateFloorGearShare` |
| IX4 | Boss card may show 1–2 unusable options | Per-option usability (IR-8, **agreed, Q1**) |
| IX5 | `requires` is a soft weight (ITEMS, TK rule 7), a hard filter for offers (CL 7.5), and an equip gate in CMP 8.4 | Lanes and `requires` are weights; usability is a separate predicate; CMP 8.4's Presence tags fail `UsableBy` for companions (4.3) |
| IX6 | "Requires lane" undefined | `lootLane` (IR-1) |
| IX7 | TK 18.3 "second same-element offer de-weighted" needs offer history, not an allowed input | Owned-item proxy in the smart roll (4.6) |
| IX8 | Trinket cap 1 blocks C → U of one family | Trinket Scion exception (IR-5) |
| IX9 | "Base" undefined for trinkets, scrolls, wands | Dedupe key base ?? family (IR-6) |
| IX10 | Signature ambiguities: circular top tier, eligibleFloors spanning tiers, 3-line cap, categories, caps, drawn vs fixed, unarmed bosses | IR-13 (**agreed, Q5** for unarmed bosses) |
| IX11 | No draw counts for rolled lines, lootPlan wand charges, Resistant on lootPlan identities; cap-accounting order | IR-14; frozen generation order (4.17) |
| IX12 | Carrier legality is final only after composition | Pre-filter at draw; LootBind remap; `CarrierRemapRate` (4.12) |
| IX13 | Two substitution paths (ENM supply/tonic vs LE tier-stable) | Tier-stable always first (IR-7) |
| IX14 | Name collisions (affix, variant, quality, signature, lane, stray, hallmark, unique, lineage, kin…) | A.5 |
| IX15 | "Usable" ambiguous: soft-untrained weapons; out-of-class scroll reads are a rolled Arcana check (CN Q1) | `UsableBy` with `inClass` / `arcanaRead` split (4.3) |
| IX16 | ITEMS 19.3 wants ≥ 70% late finds equipped; LE accepts ≥ 50% | Read both with `ShrugShare`; the Scion pull lever (IR-18). Told plainly: 70% is unlikely on M2 |
| IX17 | "Rarity variant" has no approved meaning | Scions: sibling records under I-12 (IR-5) |
| IX18 | Wax-Key identity's category unconstrained; K2 prices by category | Same category, different family (IR-11) |
| IX19 | Identification stakes already low (free camp identify) | Keep Arcana's job on field finds; Plain Gifts removes it only from choices (**agreed, Q3**). Told plainly |
| IX20 | `unique` already defined by CN 14.1; drafts claimed it had no meaning | New flag `oncePerRun`; `unique` untouched (IR-6). Told plainly: the research was wrong |
| IX21 | LootDoorInvariant's "same identities" vs signatures that follow the door's boss | Signatures are boss content, not draws, and never feed a deck exclusion (no floor key, no Scion-line memory, no `generated[]` row; 4.7 item 7, lead's call, after the check); RS owner confirms (hook) |
| IX22 | A fixed bias gives different on-build rates by party size | `RequiresHitRateBySize`; `requiresTargetPermille` lever (null) |
| IX23 | The Due's strict-upgrade test ignores dormant lines | Plain Gifts makes it exact (IR-9, **agreed, Q3**) |
| IX24 | Theme skins could imply an element (RC4) | Patina bans damage hues; element VFX only from properties (6) |

---

## 3. Roll models

### 3.1 The models

- **Model A, authored only.** Every legal (base, rolled entry) pair is unfolded at load into a named variant record; nothing rolls at runtime. 94 bases become 202 identities.
- **Model B, authored plus one rolled line.** Authored records; a U/R record may carry one line from its family's approved table; the name reads the roll; Scions for upgrades; display-only Patina/Reverie theme layer.
- **Model C, parts and lines.** Moulds (one name across tiers) with 0–2 rolled slots, maker houses that tilt lines, a lane > family > record weight split.

### 3.2 Judges' scores (mean of three judges, 1–10)

| Model | Excitement | Readability | Usefulness | Build diversity | Theme | Determinism / testability | Fit with docs | Low build cost |
|---|---|---|---|---|---|---|---|---|
| A authored only | 6.0 | 8.0 | 7.0 | 4.7 | 6.0 | 8.7 | 7.0 | 5.0 |
| **B authored + 1 line** | **7.0** | **8.0** | **8.0** | 6.0 | **7.3** | 8.0 | **8.0** | **7.7** |
| C parts and lines | 8.0 | 5.3 | 7.0 | 8.0 | 6.7 | 6.7 | 6.0 | 5.7 |

All three judges chose **B**, amended.

### 3.3 Verdict and corrections (lead's call)

**Model B, corrected ("B-fixed"),** stands as written (the client, 2026-11-22):
- B's item shape and identification rules (closest to ITEMS 12.10, TK 8.2, AR 5.5 as written; 0% dead gear, 0% record repeats, 100% usable offers, about 3.2 reads per card on the target roster, lowest content and V14 cost).
- **C's weight split** replaces B's formula, which was wrong as written (dividing by laneCount × familyCount gave no lane share; arms ran 48–60% against a 30% target).
- **A's lexicon naming**, with the "?" blank for a dormant roll.
- **C's mould ladder as the authoring shape for Scions** (same model and name, +1 line per tier): the cheapest route over the roster wall.
- **C's tier ≤ T memory**, so higher Scions of an owned family stay open.
- **`oncePerRun` counted by generation** (A's "once shown" breaks LootDoorInvariant: whether the fourth option is shown depends on the door).
- **VR depth ≥ 2 per lane group**: with 1 per lane group, the same VR appears in 52–62% of runs and the Shade and Arcanist end with 6 distinct builds in 300 runs; with 24 VR records it recovers to 35–41.
- **Rejected:** Model A, because it doubles identities for no play gain the toy could measure; Model C as the runtime model, because it adds about one read per card (3.5–4.8), about 2,300 line combinations to validate, and its 250‰ soft floor leaked 22–27% dead gear (it claimed 4–9%) (told plainly).

---

## 4. Rules in full

### 4.1 The pipeline: one card → one item (lead's call; changes IR-2, IR-3, IR-6, IR-7, IR-14)

Input: a card {floor f, tier T, channel, socketId, rank, flags: due | fit | armsFloor | elite | carried | offer(set, slot) | stepped | waxKey | campN+1 | phial}, its source kind and entry index. Each flag is a **socket kind**; 4.3 gives every socket kind's usability floor and scroll rule, and 4.7 item 8 its relaxation order.

| Step | What happens | Draws |
|---|---|---|
| 1. Static legal set **S0** | Rarity = T; `channels[]` fits; minFloor ≤ f ≤ maxFloor (camp entries read f = N+1 for tier, minFloor, maxFloor, `categoryWeight` and `scrollFloorCap`, lead's call, after the check); wave and unlock snapshot; tier gates (pieces ≤ U; Rare trinkets amulets F3+; scrollFloorCap; wands F2+; no Common wands); the phial is never a weighted draw (it is placed by CN 14.7's pre-roll, below); no healing in offers or camp scrolls. **Carried:** `usableBy.monster`, channel `carried`, no container-only family or line, F1 consumables self-targeting. **Elite:** gear only. The lane, family and record divisors of 4.2 are computed **here** | 0 |
| 2. Run-state exclusions | Capped families; owned dedupe keys at ≥ quality (I-12); this floor's keys (4.7); Scion-line memory and `oncePerRun` memory; per-floor counters (VR perFloorMax, trinketRarePerFloorMax in the 4.17 order, the F5 tactical cap, the IR-16 guards, the tier-4 lever). Camp entries use floor N's keys and sit outside floor N's and N+1's field per-floor counters | 0 |
| 3. Usability | Hard filter on chosen, guaranteed and VR cards; soft factor on C/U/R field cards (4.3; **agreed, Q1**) | 0 |
| 4. Target filter | One per socket kind: the Due (4.9); fit premium; arms floor; offer slot (4.8); Wax-Key sibling (4.11); elite `showsInUse` preference (4.12) | 0 |
| 5. Empty? | Draw-free, **in this order** (lead's call, after the check): first the socket's own target relaxations in their listed order; only when those are exhausted, the IR-7 rungs that apply to that socket kind (4.7 item 8). Steps 1–4 are set intersections, so this order decides only what is relaxed | 0 |
| 6. Weights | 4.2 over the survivors | 0 |
| 7. Identity | Draw 1 of the 9 reserved picks one record by multiply-shift over records sorted by id; draws 2–9 are consumed as padding | 9 (approved) |
| 8. `lineRoll` | Line, alternate line, element, charges (4.6, 4.17) | 4 |
| 9. Draw-free finishing | Dormant state; name; `known` flag (IR-9); Patina, stamp, Reverie by display hash; LootLedger row; floor keys and counters updated | 0 |

- **Filter-first with padding (lead's call; told plainly).** Filtering before draw 1 gives the same distribution as ENM 15.3 step 2's "reroll, each excluding…" conditioned on success, at a fixed count, and never substitutes merely because 9 rerolls ran out. It does **not** give the same identity per seed as a literal reroll loop. It is adopted now and must be locked **before** M1 golden seeds and V13 (m) fixtures are recorded (hook to segment 11; taken up: filter-first is one of the locks and golden preconditions before the M1 golden seeds, seeds and validation design 4.5, 4.11; change SV-18).
- **Signatures** skip steps 1–8: they are boss content (4.13), and they feed no exclusion of any deck draw (4.7 item 7).
- **The phial swap (LE 6.13 item 10).** The swapped card's identity is still drawn at its fixed count and discarded as a ghost; it never enters memory, floor keys or `oncePerRun`.
- **The boss-channel phial (CN 14.7, agreed CN Q2; CN 10 row, settled; lead's call, after the check).** CN 14.7's pre-roll can place the phial in the boss channel (F3–F4 boss card; never F5, LE-5). It then takes the boss card's **Wayward slot (option index 2)**: that slot's identity draw and its `lineRoll` entry are consumed as ghosts; the phial is outside Scion-line memory and `oncePerRun` (CN's `unique` and its family cap 1 govern it), counts as usable by every member (talisman: always, 4.3), is identified (consumables always are), and the other slots treat it as the `talisman` fine category and the `utility` effect tag for distinctness. It is never in the Viaticum, the Wager, the Twin Coffer or camp stock (settled). LE 6.13 defines the swap only for container cards; this rule fills the boss-channel case without changing CN 14.7's channels.

### 4.2 Category, lane and family (lead's call; change IR-3)

**Categories at two levels (lead's call, after the check).** A record has a **fine category** (`loot.categories`: weapon, focus, armour, shield, piece, jewel, scroll, wand, flask, talisman) and a **category group** (`loot.categoryGroup`: the four LE 6.2 groups). Groups: **arms** = weapon, focus; **guard** = armour, shield; **trinket** = piece, jewel (pieces sit with trinkets, as TK and the 5.3 tables already list them); **tactical** = scroll, wand, flask (notable flasks and bombs), talisman. `categoryWeight` and the lane divisor use the **group**; offer and camp-pair distinctness, the Wax-Key sibling's "same category", the Due's slot and the UI's category word use the **fine category**.

For each surviving record r (lane L, family F, group g, tier T), records sorted by id, the weight is an Int64 computed **factor by factor in this fixed order, with a FloorDiv after each factor** (lead's call, after the check; the earlier "one FloorDiv at the end" was undefined):

```
w := categoryWeight[f][g] × SCALE                 // permille; SCALE = 2^20
w := FloorDiv(w, |lanes of g in S0 at T|)
w := FloorDiv(w, |families of L in S0 at T|)
w := FloorDiv(w, |records of F in L in S0 at T|)
w := w × bias(r)                                   // requiresBias: 3; 4 on boss-card options (settled); 1 off-party
w := FloorDiv(w × anyCompanionPermille(r), 1000)   // 1000 unless the CMP-30 lever fires (IR-17)
w := FloorDiv(w × usabilityPermille(r), 1000)      // 100 on soft sockets if no member can use r (IR-2, agreed Q1)
for each socket term t in the order below:
w := FloorDiv(w × t(r), 1000)                      // 1000 when the term does not apply
```

| # | Socket term (all *(illustr.)*) | Applies when | Key |
|---|---|---|---|
| 1 | `showsInUse` 2000 | Carried cards, records with a `showsInUse` line (IR-12) | `loot.carried.showsInUsePermille` |
| 2 | Wax-Key other lane 2000 | The Wax-Key identity, records in a lane other than the stepped identity's (4.11) | `loot.waxKey.otherLanePermille` |
| 3 | Spell-family repeat 500 | Scrolls and wands whose spell family already has a row in `LootLedger.generated[]` this run, of any kind except ghost (4.7 item 4) | `loot.memory.spellFamilyRepeatPermille` |
| 4 | Named VR first 2000 | VR cards, records flagged `chase` (IT2) | `loot.vr.namedFirstPermille` |
| 5 | Scion pull (null = 1000) | F3+ field and camp, the next Scion of an owned family (IR-18) | `loot.levers.scionPullPermille` |

`bias(r)` is evaluated against the socket's **target** where the socket has one (4.3). Overflow bound: every intermediate stays below 2^62 (1000 × 2^20 × 6, then at most five terms of ≤ 6000‰ each, stays below 2^60); V14 (d) checks the largest factor in data against it. Weight 0 arises only from a 0 factor; a positive survivor whose weight floors to 0 is a V14 (d) error.

- **Divisors on the static set S0, not the survivors** (resolves IB6). Run-state exclusions then remove mass; renormalisation over all survivors spreads it across every category, instead of funnelling a whole lane's share into the last record standing (the mechanism behind the toy's VR collapse). V14 tests that `LootMixByCategory` stays within ±5 points across run histories.
- **Effects:** `categoryWeight` becomes a true share before bias (it restores the LE 6.2 30/20/20/30 mix in the toy); a family with 4 records and a family with 1 compete equally (report `FamilyShareByLane`); an unlocked record dilutes only its own family.
- **Category and family are not separate draws.** P(record) = P(category) · P(lane | category) · P(family | lane) · P(record | family): the brief's cascade exists exactly, in one draw. An illegal (T, c) pair has no lanes in S0, so its weight is 0 and the rest renormalise (LE 6.2, unchanged).

### 4.3 Smart loot (lead's call; agreed, Q1; changes IR-1, IR-2, IR-17)

**Lanes (IR-1; M1 data on F1 records, M2 full).** Every loot record carries exactly one `lootLane` from the closed `loot.lanes`. Lanes are **build lanes, not trainability**: martial training alone does not put every weapon on-lane. The UI never shows the word "lane" (TK 5.5 and CL 7.5 use it); it says "Suits" (one word for the field mark and the targeted offer slot, 6.1). Lane ids avoid `shadow`, `precision`, `delve`, `worn.open.<loc>` and the "arms floor" reading. *(illustr.; class, kit and category owners finalise)*

| Group | Lanes |
|---|---|
| Strike (weapons) | `lane.strike.heavy` (two-handed martial melee), `lane.strike.blade` (one-handed or versatile martial melee), `lane.strike.finesse` (light, finesse), `lane.strike.reach` (pike, spear, glaive), `lane.strike.ranged` (launchers and dedicated thrown) |
| Cast (foci) | `lane.cast.arcane` (orb, rod, tome, staff), `lane.cast.sacred` (holy symbol, totem) |
| Guard | `lane.guard.heavy`, `lane.guard.medium`, `lane.guard.light`, `lane.guard.shield` |
| Pieces and jewellery (universal) | `lane.piece.head/hands/feet/back`; `lane.jewel.ring/amulet/charm/capacity` (TK's agreed jewellery lanes, TK 5.5 Q1, mapped 1:1) |
| Tactical | `lane.read.arcanist`, `lane.read.herald`, `lane.read.shared` (Unweaving), `lane.wand`, `lane.tool` (notable flasks and bombs, talisman; universal) |

`requires[]` stays as secondary build tags (`build.*`, `worn.open.<loc>`, `skill.medicine.trained`, `formation.adjacent`, weapon group tags).

**`LanesOf(member)`** = class lanes ∪ subclass lanes ∪ kit lanes ∪ universal lanes, from data (`loot.classLanes`, `subclassLanes`, kit rows). *(illustr.)* Vanguard: strike.heavy, strike.blade, strike.reach, guard.heavy, guard.shield. Shade: strike.finesse, strike.ranged, guard.light. Arcanist: cast.arcane, guard.light, read.arcanist, wand. Herald: strike.blade, cast.sacred, guard.medium, guard.shield, read.herald. Trained Arcana adds **use** of off-class scrolls and wands, never **lanes** for them.

**`onParty(r)`** = some living member m of the generation snapshot has r's lane in `LanesOf(m)`, `UsableBy(m, r)`, and meets every snapshot-evaluable `requires[]` tag (`worn.open.<loc>` open; TK `usefulFor[]` contains m's class, or `anyCompanion` and m is a companion). It reads only the party snapshot, so LootDoorInvariant holds. **On a targeted socket** (the Due, a fit premium, the Yours slot and the Theirs slot), `onParty` is evaluated against the socket's **target member alone** (×3, or 4 on the boss card, only if r is in `LanesOf(target)` and meets the target's `requires`), matching the smart line roll, which already reads the target only (lead's call, after the check; no draw count changes).

**`UsableBy(m, r)` (IR-2): one query every filter shares**

| Category | Usable when |
|---|---|
| Weapon | Trained in the family's `trainingGroup`. Soft-untrained counts as **unusable** for loot filters (it may still be equipped; the verdict shows it as untrained, never ✕, 4.14). A Strength shortfall is usable, **except on the Due and fit-premium filters**, which read LE 6.6's fixed-deal test "(trained, Strength)" so the targeted item is one the next deal will give the target (lead's call, after the check) |
| Focus | Tradition matches |
| Body armour | Category ≤ m's ceiling (hard) |
| Shield | m's class or kit allows shields |
| Trinket, piece | No hard requirement fails; Presence-keyed lines fail for companions (CMP 8.4) |
| Scroll | `inClass`: `classTags` ∋ m's class. `arcanaRead`: m is trained in Arcana; the read is a rolled check (CN Q1, 10 + tier), so it is usable with a shown difficulty |
| Wand | In class, or trained Arcana (CN rule); Heralds without Arcana cannot (HR declined wands) |
| Flask, talisman | Always |

- **Usability by socket kind (resolves IK7; lead's call, after the check: one normative table).** "Hard" excludes records no living member can use; "soft" weighs them ×100‰ *(illustr.)*. `inClass` and `arcanaRead` say which scroll reads count as usable (told plainly: a usable option that is a DC-13 gamble is not "for you", so every socket that names a member requires `inClass`).

| Socket kind (4.1 flag) | Floor | Scroll use |
|---|---|---|
| Field container card (C/U/R), carried card | soft | `arcanaRead` |
| Field VR card | hard | `arcanaRead` |
| Elite gear card | soft (gear only) | — |
| The Due | hard (target) | `inClass` |
| Fit premium | hard (target) | `inClass` |
| Arms floor | hard (hero) | — (gear only) |
| Stepped identity, Wax-Key identity (used and unused) | hard | `arcanaRead` |
| Camp entry (bought: chosen, Q1) | hard | `arcanaRead` |
| Offer: Yours, Theirs | hard (focus or target member) | `inClass` |
| Offer: Wayward, the fourth | hard (some member) | `arcanaRead` |
| Offer per-option fallback, hero-guarantee redraw | hard (gear; the hero for the redraw) | — (gear only) |
| Viaticum option | hard | as its slot: Yours and Theirs `inClass`; Wayward `arcanaRead` (approved readers, difficulty shown) |
| Wager option (M3), Twin Coffer option (M3) | hard (chosen: Q1) | as the slot they fill (Yours, Theirs, Wayward) |

- **Usability floor (IR-2, ⚑ agreed ENM 15.3 step 2; agreed, Q1).**
  - **Hard** on everything chosen or guaranteed, per the table: the Due, fit premiums, the arms floor, every offer option and the fourth (Wager and Twin Coffer included), the Viaticum, both Wax-Key and stepped identities, camp entries (bought, so chosen; lead's call, after the check, reading Q1's "chosen"), and VR cards (consistent with the approved M2 VR rule).
  - **Soft** on C, U **and R** field cards (containers and carried): ×100‰ *(illustr.)* for records no member can use. This keeps Sell value and later-recruit value, keeps build drift possible, and stops martial Rare field cards falling to tonics on the M2 roster (resolves IB1 and IF7).
  - Ghost draws stay unfiltered (approved). Sundries are untouched.
  - **Rejected (Q1):** B, a hard floor on every draw (fewer surprises and distinct builds; martial Rare field cards fall to tonics on the M2 roster); C, the approved soft weights only, with boss cards that may show unusable options.
- **Not tuning (new lead's call):** which sockets are hard (`loot.usability.hardOn`) is the client's answer and changes only with the client; the 100‰ soft factor is tuning.
- **Strength (toy, illustr.).** Solo on-build about 65–76% with hard floor + bias 3, 24–36% wayward; dead gear 0% with the hard floor, single digits expected with 100‰ (to verify in the simulator). A full party is about 99% on-party; the hero's share stays the Due's job (LE 6.6).
- **Levers, null by default.** `loot.requiresTargetPermille`: the per-draw bias becomes max(3, the bias reaching the target share), capped at 6 *(illustr.)*; fired only if `RequiresHitRateBySize` shows a gap > 10 points or solo on-build < 65% (⚑ LE 6.2 if fired). `loot.heroLean` stays rejected as a default (LE 6.8: no class quotas).
- **Companions** are members whose kits declare lanes. **`anyCompanion` (IR-17):** stays a lever (LE 6.8, CMP-30), but gains a party-size key so it can fire at party ≥ 3 only *(illustr. 1500‰)*. The toy says CMP-30 fails at party 3–4 in every model (40–75% of companion trinket slots empty), so the M2 calibration is expected to fire it (told plainly). 2–3 Rare neck families are part of the content request (9.4).
- **7a side effect (hook):** dead Sell gear mostly disappears; 7a re-runs SellShareOfCoin, K1/K2, CampCoverage and LootMixByCategory in M2.

### 4.4 Theme bias (lead's call; agreed, Q4; told plainly; changes IR-15, IR-19)

- **0‰ on identities, category weights, lane weights and rolled lines** **(agreed, Q4)**. Door choice must never become loot shopping. The theme draft's lane-preserving tilt (×1.5 on the union of both door themes) adds +0.02 kin items per floor in the toy against its claimed +0.3, and it would reopen RS 9.1. **Rejected.**
- **The approved Resistant read stays on authored records only** (AR 14.3, RS 6.4). A Resistant line **rolled** from a table picks from its legal elements theme-blind, with the owned-repeat damping (resolves IB8: on the five M2 themes with n = 0 the fallback element would make rolled Resistant nearly worthless; and widening the theme read to every rolled line would widen RE15 unflagged). Camp stock is generated with floor N and never reads floor N+1's theme.
- **How the theme reaches loot** (5): theme rosters carry notables (roster shape alone gives 22–36% carried-family share on M2); KinSwap; per-boss signatures with epitaphs; the "Answers here" mark; Patina; theme stamp; provenance; Reverie.
- **Element Echo (IR-19, ⚑ settled RS 6.4, RS 9.1, ENM 15.3, TK R-23, LE 6.10 item 7; agreed, Q4).** Offensive element-choice lines read the found floor's elemental `counterType` through the existing element draw; fixed fallback when none. Identities and draw counts unchanged; about 1–2 lines per floor. **Held for an M3 client decision, default off** (`loot.elementEcho = false`). Its code path may ship in M2 behind the switch so playtest builds can compare (told plainly: a playtest build with it on runs outside the settled rule, as an experiment only). **(New lead's call:)** the switch is a client key, not tuning; a release build ships it `false` until the client adopts Echo, and V14 (j) fails a release configuration with it on.
- **Rejected (Q4):** B, adopting Element Echo in M3 now (before any playtest has compared it); C, reopening theme-keyed weights (the toy shows +0.02 items per floor, and it breaks the door guarantee).

### 4.5 Rarity variants and VP: Scions (lead's call; change IR-5)

- Each record has one rarity (I-12). A **Scion** (`scionOf: <recordId>`) shares its parent's family and base; its fixed lines are a **superset** of the parent's (the parent's lines plus one, or a major replacing a minor of the same domain). The same model is reused (C's mould shape), with a stronger Patina finish.
- The comparison reads "+1 line over your Vowkeeper" (▲); the owned-base reroll allows it because its quality is higher; the weapon and armour family cap of 2 allows one rung.
- **Trinket Scion exception (IR-5, ⚑ ITEMS 12.2 family caps, TK 13.8; TK owner).** A trinket family's cap of 1 admits **one** Scion at a strictly higher tier, only while the party owns ≤ 1 copy of the family; the lower copy then shows ▽ with a Sell hint (resolves IB14). Report `TrinketScionRate`; revert if the share of trinket families a run sees (about 30%) falls by > 5 points.
- **VP is exact:** value = Σ fixed + rolled `entryValue` = budget(T). Patina, stamp, Reverie and provenance add 0. V14 checks every record and every Scion superset.
- Content target: about 1 Scion line (C–R) per gear lane in M2; VR records are Scion tops where a parent exists *(illustr.)*.

### 4.6 Rolled lines: the only random property (lead's call; change IR-4)

| Tier | Legal shapes | Rolled |
|---|---|---|
| C | 1 fixed obvious line | 0 |
| U | 1 fixed + 1 rolled minor; or fully authored (2 minors, or 1 major) | ≤ 1 |
| R | 2 fixed + 1 rolled minor; or fixed major + 1 rolled minor; or fully authored | ≤ 1 |
| VR, signatures, heirlooms, M1 notables | Fully authored | 0 |
| Pieces, trinkets | AR 5.5 and TK 8.2 as written | ≤ 1 |
| Scrolls, wands, flasks, talisman | No lines; wands roll charges only | — |

- **Fixed lines are always `obvious`** (⚑ narrows ITEMS 11 item 3's "non-obvious fixed or rolled" and makes 19.1 rule 14 an instance guarantee). The dormant line, if any, is the rolled one.
- **Target share:** about 40–50% of U/R gear records rollable *(illustr.)*.
- **Curated per-family tables (resolves IF9).** Each family declares 3–5 eligible entries from its category table, all sensible on that family, never the whole table. Eligibility also applies WEAPONS 18.1 (Quickdraw never with `type`; Shatter R+ only), Adamant never on plate, TK rule 28 **extended to every category** (no repeat of the obvious line, no shared `answers[]`, no second channel or button), wave gates, and on carried cards `carrierSafe` lines only.
- **Smart roll:** integer weight per entry 2 × (matches a member's `requires` or `synergy[]` ? 2 : 1) ÷ (already on a party-owned instance ? 2 : 1), i.e. 1, 2 or 4. For the Due, fit premiums and the Yours and Theirs slots, "member" is the target only. **Draws (lead's call, after the check):** d0 picks the entry by multiply-shift over the cumulative integer weights of eligible entries sorted by id; d1 pre-picks an **alternate** entry from the eligible set minus d0's pick, with the same weights, stored on the identity bundle; LootBind uses the alternate, draw-free, if the final carrier rejects d0's line (4.12). An element-choice line's element is picked by d2 as a multiply-shift of r over the cumulative integer weights of its legal elements sorted by id, each weight 2 ÷ (that element already on a party-owned instance of the line ? 2 : 1). The owned-repeat factor is the allowed-input reading of TK 18.3's element de-weighting (IX7).
- **Power bound (resolves IB7).** Equal `entryValue` is not equal combat value. The M2 simulator reports `LinePowerDelta` per entry at b1/b2/b3, by line kind: offence lines by expected-damage delta, defence lines by kPC delta; utility lines with no metric are exempt and listed (lead's call, after the check: a simulator gate, not a static V14 check). An entry outside ±25% *(illustr.)* of its table median moves table (minor ↔ major) or narrows eligibility. The concentration ceiling (no entry above max(40%, 1.5 ÷ legal entries), TK 18.3) is checked **after** smart weighting.
- **The name tells the roll (lead's call).** Every rollable property declares one fixed epithet in `loot.lineLexicon`, always a **suffix** ("… of the Bar", "… of the Whetstone"; the TK 15 precedent). **Suffix only (lead's call, after the check):** a prefix blank would leak which entries are possible and reads badly ("? Vowkeeper"), so every dormant name has the one form "<Name> of ?" ("Strider's Boots of ?"); V14 (f) checks it. Identification fills the blank with the **reveal chime** (global UI family, 6.3). The lexicon passes AR 17.1 rule 13 and TK 15. **Scion names:** a Scion's record name adds a fixed rank word ("Vowkeeper, Twice-Sworn"), so record display names are unique (V14 (f)).
- **Report `RollFitRate`** (the rolled line matches some member's requires or synergy), target ≥ 60% *(illustr.)*.
- **Re-engraving** (cycle a rolled line to the next entry at camp, value-neutral) is passed to the camp owner as an M3 hook with no price; adopting it would change ITEMS 18.1 and ENM 15.3 step 5 ("never rerolled"), the camp owner's change.

### 4.7 Duplicates, caps and memory (lead's call; agreed, Q2; changes IR-6, IR-7)

1. **Family caps (settled), unchanged.** They count taken or owned items; bought camp stock counts once bought. A signature is cap-exempt when generated and taken (like its owned-base reroll exemption, item 7): it may take a family one over its cap, and it counts toward the cap for every later draw (lead's call, after the check).
2. **Dedupe key = base ?? family** (resolves IX9): trinkets, scrolls, wands and notable flasks dedupe by family, for rerolls and for ENM 20.1.
3. **This floor's keys** = every identity generated earlier in the frozen generation order (4.17) whose event writes floor keys (table below): the Due, arms floor, fit premiums, stepped and Wax-Key identities, offer options and the fourth, fallbacks and the hero-guarantee redraw used, and parent identities in channel order. **Signatures write no floor key** (item 7). **Consumable offer options enter floor keys only for intra-set distinctness**, never for field draws (resolves IB4: an F2 boss card would otherwise eat 4 of the 5 U tactical records). Floor keys are never lifted (ENM 20.1, item 8).
4. **Scion-line-once (⚑ IR-6, agreed ENM 15.3 step 2; agreed, Q2).** A gear or trinket record **generated** by an event that writes memory (table below: every gear and trinket identity, real or dreamed, offer options and both dual identities included; ghosts and signatures excepted) excludes itself and its **Scion ancestors** for the rest of the run. **Scion lines are true lines (lead's call, after the check):** V14 (e) allows at most one Scion child per record, so "its line at tier ≤ T" is the record plus every ancestor reached through `scionOf`, and higher Scions stay legal. Memory reads **generation outputs only** (`LootLedger.generated[]`), never player actions (resolves IB16): a take-then-sell loop cannot lock or unlock records; player choices reach generation only through the approved owned-items snapshot. Generation, not choice, is the agreed key (Q2), so an offer item the player passed up never returns at its tier or below. Consumables, scrolls and wands are exempt (family caps govern), with the spell-family repeat term (×500‰ on a spell family already in `generated[]`, any kind but ghost; 4.2 term 3). **Staged:** C/U from M2; Rare once V14 (c) passes at Rare for every solo class. **(New lead's call:)** the Rare switch (`loot.memory.scionLineOnce.R`) is flipped in data the first build in which V14 (c) passes at Rare for every solo class, and no later than the end of M3; until then Rare repeats are possible and are reported in `RecordRepeatRate` (told plainly: the agreed "never returns" holds for C and U from M2 and for Rare from that switch, and a logged relaxation rung 1 can still lift it, item 8). **Dead Rare repeats (lead's call, after the check):** while the Rare switch is off, Scion-line-once still applies at Rare to records **no member of the generation snapshot can use**, which costs nothing (they are dead for the party anyway) and stops the same unusable Rare dropping on F3, F4 and F5; report `DeadRareRepeatRate`.
5. **`oncePerRun` (⚑ IR-6, ITEMS 12.3 flags; narrows LE-4 for memory only; agreed, Q2).** A flag, always explicit in data: generated at most once per run in any form, dreamed options, the hidden fourth and the unused Wax-Key identity included. V14 requires the flag on every signature, VR record, heirloom template and record flagged `chase` (the "named chase Rares"), and lists any record of those classes without it. Never lifted. CN's `unique` keeps its meaning ("at most 1 owned"); the phial keeps CN 14.1 (resolves IK3).
6. **`oncePerRun` records are ineligible for draws that might never be presented** (resolves IB3): the fourth option, and both identities of a dual-identity cache. Eligibility is fixed at generation, so it is door-invariant. Uniques stay legal in field, carried, Due, fit and offer slots Yours/Theirs/Wayward.
7. **Signatures never feed exclusions (⚑ IR-6, ENM 20.1, RS 6.4; lead's call, after the check; resolves the door-dependence the check found).** A signature depends on the door (its theme's boss), so anything it excluded would make deck identities depend on the door, against RS 6.4's "same identities" (settled). Therefore a signature: writes **no floor key** and is not pre-registered; writes **no Scion-line memory** and never carries `scionOf`; is recorded in `LootLedger.bossContent[]`, **not** `generated[]`; and reaches later generation only through the approved owned-items snapshot once taken (family caps count it; it is exempt from the run-wide owned-base reroll, so taking it does not foreclose every chain-mail notable for the run). **ENM 20.1's shared-base check covers deck notables only** (a reading; signatures exempt). Told plainly: on F1 Kennel Wood a field Kneeguard Mail and the guarded Bellwarden's Mail can both be chain mail on the same floor. Report `SignatureBaseShadow` (floors where a deck notable shares the signature's base).
8. **Relaxation (IR-7; lead's call, after the check: one normative order per socket kind).** When steps 1–4 leave nothing (4.1 step 5), the socket first relaxes **its own target preferences, in their listed order** (column 2 below); only when those are exhausted do the IR-7 rungs run, draw-free and in order, and only the rungs the matrix allows. Each step is logged in `RelaxationStepRate` (and `OfferSlotRelaxed`, `WaxKeySiblingRelaxed` where named):
   1. lift Scion-line memory (never `oncePerRun`);
   2. category re-weight at T (approved);
   3. tier-stable path: VR → R + fit premium, logged in `SubstitutedVP` (approved M2 VR rule; also used by IR-13's VR signature rule); defined for VR cards only;
   4. the approved `substituted` supply or tonic.

   The earlier draft's "lift the same-floor key" rung is **withdrawn**: ENM 20.1 floor keys are never lifted, and V14 (b) and (c) make the ladder reach a legal record instead.

| Socket kind | Own target relaxations, in order | Rung 1 | Rung 2 | Rung 3 | Rung 4 | Terminal |
|---|---|---|---|---|---|---|
| Field C/U/R (container, carried) | — | yes | yes | — | yes | supply or tonic |
| Field VR | — | yes | yes | yes | yes | supply or tonic |
| Elite gear card | drop the `showsInUse` filter | yes | yes (within gear) | yes if VR | yes | supply or tonic |
| Camp entry | the pair's category difference | yes | yes | — (never VR) | yes | supply or tonic |
| The Due | — (slot and target fixed) | yes | — | — | — | waiver as approved (DreamsDueWaived) |
| Arms floor | — | yes | — | — | — | an ordinary identity of that card, hard-usable by some member, logged `ArmsFloorUnmet` (V14 (b) names the gap) |
| Fit premium | — | yes | — | — | — | LE 6.9 item 3 as approved: an ordinary hard-usable identity at T **plus 20 fallback coin** (F5: sundries), logged |
| Stepped identity | — | yes | yes | yes if VR | yes | supply or tonic (unreachable when V14 (b) passes) |
| Wax-Key identity | same category → any category (`WaxKeySiblingRelaxed`); the other-lane term needs no relaxation | yes | yes | — | yes | supply or tonic |
| Offer slot (boss card, Viaticum, Wager, Twin Coffer) | 4.8's slot list, then the per-option fallback | in the fallback | — | — | **never** | 4.8's offer terminal ladder |

   **The tier never moves silently:** any tier change is logged (resolves the toy's 3–12 silent step-downs per run, IT1). Guaranteed draws (the Due, the arms floor, fit premiums, offer options) never reach rung 4. **Precedence:** tier-stable before supply/tonic (resolves IX13).
9. **Run caps kept:** VR perFloorMax 1, trinketRarePerFloorMax 1 (client), phial 1 per run, Heartward 1 per run, scrollFloorCap, F5 tactical cap 400‰, plus the **IR-16 guards**: `rareTacticalFieldMaxPerFloor` 2 on F3 and F4 *(illustr.)* (Rare scrolls, wands and notable flasks in field draws; **new lead's call:** once it is reached, Rare tactical records take weight 0 in field draws for the rest of the floor, enforced in channel order as the F5 cap is), and the tier-4 scroll lever (`tier4ScrollsPerRunBeforeFinalBoss`, CN 18.1) defaulted to **1** while the usability floor ships and Rare gear is thin (resolves IB5; `ScrollBossShareByClass` reads it). Told plainly (after the check): once the lever is spent (for example Icefall taken on F4, LE 6.3), LE 6.11 item 5 drops every tier-3+ scroll from the Viaticum, so it draws from Rare wands only and may push the F5 boss share to gear fallbacks; V14 (g) checks that the Viaticum is still satisfiable after the lever is spent, and `ViaticumAfterLeverFallbackRate` reports it.

**What each generation event writes (normative; lead's call, after the check).** `generated[].kind` records the event.

| Event (`kind`) | Scion-line memory (gear, trinkets) | `oncePerRun` | Floor keys: gear | Floor keys: consumables |
|---|---|---|---|---|
| Field container or carried card (`field`, `carried`) | yes | yes | yes | yes |
| Camp entry (`camp`) | yes | yes | floor N's keys | floor N's keys |
| The Due, arms floor, fit premium (`due`, `armsFloor`, `fit`) | yes | yes | yes | yes |
| Stepped identity; Wax-Key identity, used or unused (`stepped`, `waxKey`) | yes (both) | ineligible (item 6) | yes (both) | yes (both) |
| Offer options 0–2 (`offer`) | yes, taken or not | yes | yes | intra-set only |
| The fourth (`fourth`) | yes | ineligible (item 6) | yes | intra-set only |
| Per-option fallback used; hero-guarantee redraw (`fallback`, `heroRedraw`) | yes | yes | yes | — (gear only) |
| Unused fallback entry (draws consumed, no identity) | no row | no | no | no |
| Ghost draws (the Due's and arms floor's parent draws, retired by change SV-5 and no longer drawn; the phial's swapped card, the boss-channel phial's slot) | no row | no | no | no |
| Signature (`LootLedger.bossContent[]`, not `generated[]`) | no | its own flag only | no | no |

- **Rejected (Q2):** B, memory for `oncePerRun` items only (signatures, VR, named Rares), so ordinary records could repeat; C, the approved owned-base and same-floor rerolls only.
- **Not tuning (new lead's call):** `loot.memory.oncePerRunBy = "generated"` and Scion-line-once at C and U are the client's answer.

### 4.8 Offer sets and dreamed options (lead's call; agreed, Q1; change IR-8)

| Option index | Slot | Filter (in relaxation order) | Bias |
|---|---|---|---|
| 0 | **Yours** | Usable (`inClass` for scrolls) by the **focus member**, in its lanes. Focus member = the LE 6.6 Due target rule for that floor | 4 on the boss card (settled); 3 elsewhere; evaluated against the focus member (4.3) |
| 1 | **Theirs** | The **target member**: among other living members, the largest deficit under the Due scorer (LE 6.6 item 2), then the lowest memberId (lead's call, after the check). Filter rungs: (a) in the target's lanes **and ▲ for the target**; (b) in the target's lanes. Solo: a different fine category from Yours, or a different `effectTag` in consumable-only sets | as Yours, against the target |
| 2 | **Wayward** | Usable by someone, in no member's lanes. Filter rungs: (a) a property id no party-owned item has ("new line"); (b) any such record. If none, any usable record of a third fine category or effect tag | 1 (off every lane, so `onParty` is false) |
| 3 | **Fourth** (pre-drawn, LE 6.12) | The Wayward rule, excluding the families of the other three; never `oncePerRun` | as Wayward |

- **Construction order (lead's call, after the check).** Slots are resolved **one at a time, in option-index order 0, 1, 2, 3**; each slot is resolved fully (its option draw, its slot relaxation, and if needed its per-option fallback and the offer terminal ladder) before the next slot starts, so every later slot is filtered against final earlier options. The hero guarantee runs last (below). If CN 14.7's pre-roll put the phial on this boss card, it fills option 2 and that slot draws only ghosts (4.1).
- **Within a set:** families distinct (always); fine categories distinct where legal, else `effectTag`s distinct; at most 1 damage option on the boss card and the Wager (extends the approved Viaticum rule); at most 1 VR (Wager, approved).
- **Slot relaxation** (logged `OfferSlotRelaxed`): the slot's filter rungs in the table's order → the lane rule → fine-category or effect distinctness → the per-option fallback. Family distinctness is never relaxed.
- **Per-option fallback (⚑ LE 6.3 LE-3, ENM 17.5, CN 10 boss-card row; agreed, Q1).** An option with no usable record is drawn **on its own** as **usable gear at the same tier, with the usability filter and ordinary weights** (requiresBias 4 on the boss card for every slot, as LE settles for boss-card options, and 3 on the Viaticum; `onParty` over the whole party), on `bossCardFallback` / `viaticumFallback` **entry = option index**. It is **not** fit-filtered (resolves IB2: fit premiums stay a Risk Ledger instrument, LE 6.9, and martial parties do not get 4–5 lane-targeted picks per run that casters never get). The Viaticum's approved "fit-premium gear" fallback wording changes the same way (⚑ LE 6.11 item 5). A **fourth entry** (the fourth option) and a **fifth entry** (the hero guarantee) are added to both fallback kinds (IR-14).
- **Offer terminal ladder (lead's call, after the check).** If the fallback set is empty too, in order: (1) lift Scion-line memory for that option (logged); (2) a usable notable tool (flask or bomb, always usable) at the same tier, fine-category distinctness waived; (3) usable gear or a tool one tier lower (never below C), logged in `SubstitutedVP` (Ledger B counts the lower VP). Offer options are guaranteed draws and **never** reach rung 4 (supply or tonic); healing never enters an offer (V14 (l)). V14 (g) makes step (3) unreachable on the shipping roster or names the gap.
- **Trinkets in the fallback pool** (a reading: LE 6.9 item 2c defines gear to include trinkets). A fallback trinket is a LootPlan identity, so it **counts first** against `trinketRarePerFloorMax`, like every LootPlan identity (4.17); once the cap is reached, Rare trinkets are excluded from the floor's later parent draws (lead's call, after the check, replacing "checked last", which could not be computed before the parent draws). Gear-only sets then reach about 2.8 distinct categories instead of 1.6–1.7.
- **Hero guarantee (resolves IF6, IT7; boss card and Viaticum).** If after options 0–3 are final no option 0–2 is usable by the hero, the Theirs option is redrawn as **hero-usable gear** on the fallback kind's **entry 4** (its own key, never the entry Theirs may already have used), filtered against the families and fine categories of options 0, 2 and 3 (fine-category distinctness may relax; family distinctness never does). Its `lineRoll` uses option index 8 (4.17).
- **Martial boss cards (told plainly).** For a solo Vanguard, or a Herald without Arcana, boss cards and the Viaticum resolve mostly to gear. CN 10 gets a note: "the boss gives spells" holds only for readers. A martial tactical pool (notable bombs and oils, arena tools) is a CN content request for M3, pulled forward from Later (⚑ CN 10 pool addition, filed with IR-8).
- **Display order:** Yours, Theirs, Wayward left to right; the fourth on the right with its approved Perilous badge. **The slot label sits in the offer frame header, never in the item's mark line** (lead's call, after the check): Yours and Theirs read "Suits <name>" (honest: the slot targeted that member); Wayward reads "Wayward". An option filled by a per-option fallback, the terminal ladder or the hero-guarantee redraw is not lane-targeted, so its header reads "Wayward" (or none), never "Suits <name>". If `OfferPickBySlot` shows Yours > 60%, the lever drops the Yours header label and the per-member glyphs alone show who gains (resolves IF11).
- **Dreamed options** are fully assembled (lines rolled), shown identified (IR-9, **agreed, Q3**), outside VP and family caps (LE-4 kept), inside Scion-line memory, `oncePerRun` and gear floor keys (4.7 table). They dissolve with `ItemDestroyed{dreamedAway}`. Grave Goods gains a display-only line, **"What the dream kept"**, naming them, kept apart from 7c's "Got away" line (loot sources design 7; change LP-29; M3).

### 4.9 The Dream's Due (lead's call within LE 6.6; change IR-10)

- **Kept exactly (settled, LE 6.6, LE-13):** target (F1, F3, F5 the hero; F2, F4 the largest-deficit member); slot by deficit; **the lowest deck card** above the dealt item; strict upgrade; the F4–F5 sidegrade ("a `requires` lane or property line no dealt item in that slot has" — the property-id test is already approved, told plainly); next slot; waiver with DreamsDueWaived + 20 coin; F1 U weapon or focus; never carried, substituted or cursed.
- **7b's candidate test:** usable by the target (`inClass` for scrolls; a Strength shortfall is unusable here, LE 6.6's fixed-deal "(trained, Strength)", 4.3); fits slot S; tier > dealt S, or S empty. **Two-handers** must exceed both dealt main hand and off hand, or find them empty. **Trinkets** must exceed the lower dealt trinket or fill an empty trinket slot. The test reads **fixed lines only**, before any line draw.
- **Items sold or dropped at this camp count as still dealt** for the deficit and the slot test (⚑ LE 6.6; resolves IB11: no emptying a slot at camp to steer the Due).
- **Weights** inside the filter use bias 3 against the **target alone** (×3 only if the record is in `LanesOf(target)` and meets the target's `requires`, else ×1; lead's call, after the check, so a party Due is not lane-blind for its target); the smart roll targets the member too. Fit premiums weigh the same way. Shown identified (IR-9, **agreed, Q3**).
- **Held lever `loot.dueSidegradeFromFloor` = 4** (the approved behaviour). Told plainly: the toy's F2 and F5 waiver figures (27–45%, 57–96%) came from an off-spec model ("highest non-VR card"); they must be re-run with LE 6.6's lowest-card rule before anything changes. If waivers stay high, firing the lever to 2 is a change to LE 6.6 for the client and DH owner. Retargeting the F5 Due away from the hero is **rejected**: the hero floors are the reason there is no hero quota (resolves IK5).

### 4.10 Boss card and Viaticum (lead's call; change IR-8)

| Offer | Pool | 7b rules |
|---|---|---|
| Boss card F1–F4 | CN 10 row (settled): F1 tier-1 scrolls (C); F2 tier-2 scrolls and U wands; F3–F4 tier-3 scrolls, Rime Lance; the talisman (the phial) from F3 only when CN 14.7's pre-roll places it in the boss channel, never as a weighted draw | Slots on effect tags; ≤ 1 damage; per-option fallback; hero guarantee; never VR; none on F5; a boss-channel phial takes option 2 (4.1) |
| Viaticum F5 | Rare wands and tier-3 scrolls; ≤ 1 `use.damage`; the tier-4 lever; never the phial (settled) | Slots; effect-tag distinctness; `arcanaRead` allowed with difficulty shown on Wayward (Yours and Theirs need `inClass`, 4.3); fallback as usable gear; **hero guarantee** (Theirs redrawn as hero-usable gear on `viaticumFallback` entry 4; under permadeath it is the hero's last choice before the arena; lead's call, after the check). **Host: the Bedside Stand** in the F5 lair room, the second named exception to 4.18; its silhouette hosts nothing else (loot sources design 4.10; change LP-14; **agreed, loot sources Q5**; M2) |
| Wager (M3) | Stepped tier, ≤ 1 VR, "the others fit premiums" (TL 6.15) | Yours = the free stepped option; the other two are **Theirs slots**: fit premiums for the two members with the largest deficits (solo: two other fine categories). The Wager has no Wayward slot (lead's call, after the check: a fit premium is drawn from a member's lane, so it cannot be Wayward); hard floor; identified |
| Twin Coffer (M3) | Same tier, different categories (TL 6.13) | Yours + Wayward, else Theirs; prefer the most different lane; a choose-one, so an **offer**: hard floor and generated identified under Plain Gifts (lead's call, after the check: 0.4's anti-goal, no hidden lines inside a choose-one) |
| Camp pair | 2 entries graded N+1, never VR (settled) | Entry 2 differs in fine category from entry 1 where legal; hard floor (bought, so chosen; 4.3) |

`effectTag` (IR-23): every scroll, wand and notable flask declares one of {damage, control, ward, mobility, utility}; `use.damage` stays as an alias for LE 6.11 (CN owner confirms).

### 4.11 Wax-Key and stepped identities: the Wax-Key sibling (lead's call; change IR-11)

- The stepped identity is drawn first; the Wax-Key identity second, at the approved tier (one lower, never below C; the same tier without the fit filter after a fit-premium step).
- The Wax-Key identity is filtered to **the same fine category (4.2) and a different family**, preferring a **different lane** (×2000‰ *(illustr.)* on other lanes, 4.2 socket term 2; resolves IF13). If the same-category set is empty: any category, logged `WaxKeySiblingRelaxed` (exists for 97–100% of stepped identities in the toy); then 4.7 item 8's rungs.
- **Lock-card wording (⚑ LE 6.9 item 4 and LE 6.15, agreed LE Q2 wording; TL 6.11 consequence-lock contents-hint bullet; TL owner):** the approved "notable" becomes each identity's **fine-category display noun** (weapon, armour, shield, focus, trinket, scroll, wand, tool), the one constraint the sibling rule actually holds, with the rarity word shown in its frame-shape glyph (ITEMS 8.2). Tier step: "holds a Rare weapon; by key, an Uncommon weapon". Fit step: "holds an Uncommon weapon, chosen for <name>; by key, an ordinary Uncommon weapon". After a relaxed sibling (any category) each identity shows its own noun ("by key, an Uncommon shield") (lead's call, after the check: a lane-like word such as "blade" would misstate a sibling in another lane).
- Both identities are fully pre-rolled, lines included; neither may be `oncePerRun`; both count as floor keys; both are hard-floor sockets (**agreed, Q1**). The opening history selects; nothing draws at open (settled). K2 pricing is unchanged.

### 4.12 Carried loot (lead's call; change IR-12)

- **Identity:** theme-blind, from the carrier-legal subset (4.1 step 1). Carried cards already restrict rolled lines to `carrierSafe` entries; a line that the **final** carrier still rejects at LootBind (legality that depends on the carrier, known only after composition) is replaced, draw-free, by the alternate entry d1 pre-picked at roll time (4.6); if the alternate is rejected too → the approved same-room container fallback at LootBind. Report `CarrierRemapRate`. A pre-filter reading the drawn theme's roster is **rejected** (door-dependent).
- **`showsInUse`** is derived, not authored: a line tagged charge, rider, reaction or onHit with its own VFX, or a non-null `monsterProfile.abilityShape`. **×2** on carried cards (⚑ LE 6.2). The **elite's** gear card is filtered to `showsInUse` records when that set is non-empty (⚑ LE 6.9 item 2c, ENM 15.2 elite row): "presented in use" becomes true. Report `EliteShowsInUseRate`, target ≥ 70%, and **`EliteKpcDelta`** with the filter on and off; above about 10% *(illustr.)*, a per-line monster cost-add multiplier for reaction lines engages (resolves IB12).
- **KinSwap (⚑ LE 6.9 items 2 and 7; ENM 15.3 step 4 carrier assignment; resolves IK4).** Allocation already orders sockets by (rank desc, socketId asc) and deals tiers, not identities, so no tie-break is possible there. Instead, a named, draw-free **LootBind remap** (lead's call, after the check: score and order made exact). After identities exist, score(card, carrier) = 3 if the notable's family is in the carrier's issue families, else 2 if its shield kind matches the carrier's shield, else 1 if its armour category matches the carrier's armour, else 0. Over the carried cards of one encounter, take each pair (i, j) with i < j by socketId, **in one pass**, where both cards share rank and tier; swap their carriers only if score(i→carrier j) + score(j→carrier i) is **strictly greater** than the current sum **and** both cards stay carrier-legal after the swap; otherwise skip the pair. Three or more same-rank carriers are handled by the same single pass; it always terminates. Each swap is logged `KinSwap`; the swap never changes identities, so LootDoorInvariant holds.
- **Roster requests (⚑ RS 8.7, 8.9 rosters; ENM 7.2; RS owner, segment 9):** flag the Silk's **Spine-Binder** `armed` (it already wields an awl-pike) and the Supper's **Butler-at-Arms** `promotable`. Without this, those floors' elite notable always falls to the lair cache.
- **Visible use (segment 10; lead's call, after the check):** the **carried-use floater** names the item ("Wolfcleaver: Wide Cleave") in the unit-floater layer with the ENM glint glyph, **never** in the RC7 banner slot, which stays reserved for boss signatures. It fires on a carrier's **first** use of the item in a fight and on every charge or limited use; repeat at-will riders go to the combat log only (ABL 11.4 fatigue limits; RC7: batching never hides a cue, the log keeps every use). Using reveals the item (ENM 15.4); hover reads "Carries: …". **The ENM glint icon is the only rules-bearing marker of a carried notable** (one glyph per rule; colour-blind safe); the draft's shared Patina "notable accent" is cut.
- **Unchanged (settled):** 1 carried notable per encounter on F1–2, 2 deeper; ≤ 1 carried consumable; scrolls only to caster-tagged carriers F2+; never the talisman; a cursed carried item is unmarked (Later).

### 4.13 Signatures and Mementos (lead's call; agreed, Q5; change IR-13; segment 9 owns the records; taken up by the boss rooms design 4.11.1, change BR-27)

- **Forms per tier:** each boss authors one form per top tier its `eligibleFloors` can need (U on F1–F2; R on F3–F4; VR only once VR gear exists). A boss spanning F2–F3 authors both. **Top tier (lead's call, after the check):** `topTier(floor)` = the highest tier in the floor's drawn deck variant (read after the LE `variant` draw), capped by the boss's authored forms; no other input. If the variant holds a VR card and the boss has a VR form, the VR form is chosen and the deck's VR card takes the tier-stable path (4.7 item 8 rung 3, logged); if the variant holds no VR card, the R form is chosen. V14 (h) enumerates (boss, floor, variant) to check a form exists for every reachable top tier.
- **Data (lead's call, after the check):** the **boss record is authoritative**: `signatureForms: {U: [formId…], R: [formId…], VR: [formId…]}` in authored order, first form first, second forms after it. `theme.loot.signatures[]` is derived from the boss records at load and V14 (h)/(k) check that they match. Form selection is draw-free and happens **before** the LootPlan consumers of 4.17; it registers nothing in floor keys (4.7 item 7).
- **Shape:** the boss's visible issue family; ≤ 2 base lines + **one authored value-1 line that echoes the boss's fight or kit** (its announced signature move, ENM 13.2, a condition its kit inflicts, or its family's verb); ≤ 3 lines; value = the top tier's budget plus the authored line (ITEMS 12.5, LE 6.3: U form 3 VP, R form 4 VP). **Echo check (lead's call, after the check):** V14 (h) **warns** when the authored line's verb tag matches neither `boss.signatureAbilityTag` nor a condition tag in the boss's kit; it is a warning for forms authored from M2 on, and approved M1 records are **grandfathered**: Bellwarden's Mail (AR 14.6, authored Unbowed) is the named exception. R and VR forms need a value-2 major on the base family; until weapons R-1 lands, R/VR signatures use impact, launcher, focus or armour bases (every theme-draft R signature already does).
- **Categories:** weapons and body armour (AR 5.5), plus **foci and shields** (⚑ AR 5.5, agreed armour Q1: "boss signatures stay on weapons and body armour"). Never trinkets (TK rule 21).
- **Universal chassis:** each theme has ≥ 1 signature on a simple weapon, the staff or light body armour (all four classes can use them). The clause applies from **M2** to complete theme catalogues; the M1 Kennel Wood subset (Old Ragged only) is exempt (told plainly: the M1 signature is specialist and fits the M1 Vanguard; lead's call, after the check). **Pending reading (boss rooms design 4.11.1; change BR-27; IR owner; M2):** under the RS 12 graded Cut 1 step 1c, a **required** second form on a universal chassis (Old Ragged's) counts as the Kennel's universal-chassis signature once the Frame-Master's Spear is cut; if this owner refuses, Old Ragged's second form is authored on a universal chassis under 1c (boss rooms design 14.2).
- **Second form:** any boss whose `SignatureUsableRate` < 70% gets a second form in another lane in M2 (all bosses in M3). **Every specialist-chassis signature is listed now as "second form required in M2"** (5.2, 9.4), and V14 (h) warns on a specialist signature with no second form, rather than waiting for the M2 report (lead's call, after the check). Chosen **draw-free**: the first form on a living member's lane, else the first usable, in authored order (toy: usable rises to 100%). The approved boss-channel parent draw is consumed as a dummy (⚑ LE 6.10 item 2 read: the boss channel's parent position is unchanged). **No second form on F5 (boss rooms design 4.11.1; change BR-27; ⚑ the 5.2 Wind-Down Hammer row; told plainly; M2):** the final boss always carries its first form, because the Memento is 0 VP and usability is moot; **a cut boss's second form is not owed** (RS 12 graded Cut 1).
- **Carriage per form (lead's call, after the check).** Carriage is derived per form at load: a form is `carried` only if the boss is `armed` and the form's family is in the boss's issue gear (or it is a visible second piece the boss model shows: body armour, a shield or a focus); otherwise it is `guarded`. A boss that carries its first form therefore **guards** a second form in another family. **The chosen form is known at generation**, so the boss's "Carries: …" hover, its carried-use floater and the plate's "Guards: …" line always name the **chosen** form; a carried boss never visibly swings one form while another drops (RC1, no lying UI). V14 (h) checks that every `carried` form's family is in the boss's issue gear. **On the boss (boss rooms design 4.11.1; change BR-27; told plainly; M2):** segment 9 authors each boss's `armed` flag and issue gear so that a carried form is the piece the boss visibly uses (an `armed` boss has an at-will in its first form's family); a carried item supplies **chassis only** (damage die and reach; its lines are display only, never enlarging the boss's kit); where the chassis differs, the boss's **damage type follows its theme's set** (the Warming Pan's *There, There* deals psychic), and the carried-use floater still names the item.
- **Unarmed bosses (⚑ ENM 15.2 boss row "visibly carried", LE-3; ⚑ LE 6.11 item 4 and ITEMS 12.5's F5 sentence, agreed LE Q1, "carries and uses its signature"; agreed, Q5; resolves IK1).** ENM 15.2 gives gear slots only to `armed` types. An unarmed boss **guards** its signature in a **signature cache** in its lair, by the approved unarmed-elite precedent ("Guards: cache", ENM 15.2 Elite row); its kit is untouched. **M1:** Old Ragged (a hound, RS 8.4; settled kit) guards **Bellwarden's Mail** in its den.
  - **Placement (new lead's call; name changed after the check):** the guarded signature sits in one **signature cache** (`guardedBy: "signatureCache"`; not the SR4 elite "lair cache", which is a ranked deck socket) inside the boss's lair (read by 7c as the boss's arena; loot sources design 4.4), placed by 7c (hook); it is **not a ranked socket**, is never locked or trapped, holds nothing else, is not a deck card and counts in no Ledger A socket; it opens once the boss is resolved. The boss plate reads **"Guards: <signature name>"** (the ENM 15.2 "Guards: cache" form with the item named, as a carried signature shows "Carries: …").
  - **Presentation (lead's call, after the check; a named exception to 4.18's "container art never implies contents").** One cross-theme **signature cache** variant: its lid is barred until the boss is resolved, then open; the item's **global icon** is shown on a plate decal on the cache body, never as a free-standing model, mannequin, armour stand or weapon rack on a walkable square (RC1+, RC2). Hover reads the item card, identified (`identifyDifficulty` 0). It is one RC5 pattern-registry mark (base: cache; a signature sub-mark shared with the plate's "Guards:" line). M1 ships it: Old Ragged's den uses this variant, and the M1 boss plate carries the "Guards: Bellwarden's Mail" line.
  - **Old Ragged's coat (lead's call, after the check).** RS 8.4's "huge hound in the dead lord's coat" (settled) wears a torn cloth **surcoat** that is never loot (segment 9 and art); the mail is the separate signature in the den's signature cache, so the player never reads the coat and the mail as one item. Added to the RS owner's IR-13 confirmation.
  - **F5 (new lead's call):** an unarmed final boss (none in M2) keeps the Memento rule unchanged: its guarded signature drops as the 0-VP Memento at victory, its cache shown in the lair during the fight. Told plainly: this departs from LE 6.11 item 4 and ITEMS 12.5's F5 sentence ("carries and uses its signature"), which assumed an armed final boss; flagged in IR-13.
  - **Rejected (Q5):** B, worn or lodged signatures in M2, with every affected boss re-validated. Worn or lodged signatures on unarmed bosses remain an **M3 option** needing a further change to ENM 15.2 and AR 12.6 and segment-9 re-validation.
- **Headsman's Greataxe** (W 18.6) has no M2 boss that wields a greataxe; it stays a validated record with no channel, offered to segment 9 for an M3 boss (the Ministry of Small Mercies, F1–2). Told plainly: the draft's "Wolfcleaver vs Headsman's" example of the base clash never arises in M1; the real clash is chain mail (4.7 item 7).
- **Flags and accounting:** `signature`, `oncePerRun`; never rolled or cursed; never `scionOf`; `identifyDifficulty` 0 (seen in play); no cost add; cap-exempt when taken, then counts toward family caps (4.7 item 1); exempt from the run-wide owned-base reroll; recorded in `LootLedger.bossContent[]` (4.7 item 7). **A VR signature consumes its floor's VR slot** (the deck's VR card takes the tier-stable path, logged) unless the ITEMS owner rules otherwise (resolves IB13). ⚑ This **withdraws LE-3's pending exemption reading** (LE 6.3; LE 10.2 V13s (c), "VR ≤ 1 per floor (signatures exempt, pending ITEMS)") and replaces LE 18.2's ITEMS-refusal fallback; if the ITEMS owner refuses the slot rule, LE-3 stands as approved (signatures exempt, with LE 18.2's fallback: a floor whose deck holds a VR draws its signature at R + authored) (14.2).
- **Memento (agreed, LE Q1):** the F5 signature drops at 0 VP with the approved caption "carried out of the Heart"; a second, premise-neutral epitaph line joins the RS 7.4 tone review.
- **RS owner confirms** that signatures are boss content outside LootDoorInvariant's "same identities" (IX21); with 4.7 item 7, no deck identity depends on the door.

### 4.14 Identification and comparison (lead's call; agreed, Q3; change IR-9)

- **Kept (ITEMS 11, agreed):** ≤ 1 dormant line (now always the rolled one); passive Arcana threshold, no roll; free camp identify; equipping reveals nothing; carriers reveal by use. `identifyDifficulty` by tier *(illustr.)*: U 12, R 14, VR 16; 0 for fully authored records, signatures and heirlooms.
- **Plain Gifts (⚑ IR-9, ITEMS 11 items 3 and 5 (agreed, client decision 4), TK 15; agreed, Q3).** The Due, every offer option, the fourth and every Viaticum option are generated with `known` = every line ("the dream shows its gifts plainly"); "offer" covers the Wager and the Twin Coffer (M3), which are choose-ones (lead's call, after the check). The roll distribution is unchanged (the `known` form, not "obvious-only entries"; resolves IK8). Fit premiums, the arms floor and Wax-Key and stepped identities are field cards and keep Arcana's job (new lead's call: the arms floor and stepped identities named). `known` is set draw-free in step 9, so no seed moves. **Not tuning (new lead's call):** `loot.plainGifts = true` is the client's answer.
- **Rejected (Q3):** B, showing only the dormant line's table domain ("? a back line") and its line glyph; C, as approved (a choose-one may hide a line).
- **Verdict per member** (core query **`ItemVerdict(member, instance)`** (new lead's call: name), reusing ITEMS 6.3 gain; shape plus word, never hue):

| Glyph | Meaning |
|---|---|
| ▲ upgrade | Usable; a tier above that slot's dealt item, or an empty slot (the Due's test), **and** ITEMS 6.3 gain ≥ 0 for that member; a higher tier with a negative gain shows ⇄ (lead's call, after the check: the glyph never contradicts the gain beside it) |
| ⇄ another way | Same tier (or higher with negative gain); brings a lane or obvious property id the member's gear lacks |
| = redundant | Same lines as an owned item, or a lower rung of an owned Scion line |
| ▽ lesser | Lower tier, nothing new |
| ✕ cannot use | Equipping or using is actually refused for that member. Soft-untrained weapons are **not** ✕ (ITEMS 8.2 and W 10.1 let them be wielded): they show ⇄ or ▽ with ITEMS' "untrained" fit line. `UsableBy` stays the loot filter predicate; ✕ is the display predicate (lead's call, after the check) |
| Consumables | Scrolls, wands and flasks have no slot tier: they show only ✕ (cannot read or use) or no glyph, plus the `arcanaRead` difficulty where it applies; they are excluded from `ShrugShare` |

"=" is reserved for genuine redundancy (resolves IF3): a distinct same-tier item is always ⇄. Report `ShrugShare` (notables = or ▽ for every member), target ≤ 25% per floor and ≤ 35% on F4–F5 *(illustr.)*; a miss fires the Scion pull lever (IR-18).

### 4.15 Curse and heirloom hooks

- **Curses (Later; change IR-20).** Authored **cursed twins** (`cursedTwinOf`, same family and base), never rolled curses. Benefit authored at T+1, the card keeps tier T (LE-22); cost a `whileEquipped` line plus Clinging (ITEMS 13). Gate: `curseGate`, **2 draws per floor**, always consumed from M2, ignored while `loot.curse.permille` is null; draw 1 decides whether the floor gets a curse, draw 2 picks among eligible cards whose record has a twin, listed in frozen generation order (4.17) then entryIndex, by multiply-shift (lead's call, after the check), so "≤ 1 per floor" is structural. Eligible: Rare+ gear field cards, F2+. Barred: F1; the Due; fit premiums; the arms floor; every offer option and the fourth; the Viaticum; camp entries; signatures; heirlooms; consumables; rings (TK Q2) (the offer, Viaticum, camp, fit and arms-floor bars are new). A cursed carrier's cost add uses the benefit's T+1 value (resolves IB15). Hidden as approved; the malign hint is Later. **Shrine curse cures and curse bargains (Later; change EV-43; lead's call; conditions and items owners):** segment 8 hosts shrine Curiosities that cure or bargain with these curses (events design 13, 10); they read the cursed-twin rules here and change none of them. A Curiosity's "item offered" price never takes a curse (events design 4.4.4).
- **Heirlooms (M3; reading of ITEMS 14).** A boon-channel template at U, no roll, always identified, `oncePerRun`; outside pools and Scion-line memory; blocks its dedupe key as owned; no family cap; counts as owned for the Due and for verdicts.

### 4.16 Unlock hooks (M3; reading of LE 6.12)

Unlock records carry `lootLane`, tier, `unlock` and wave, and join their lane at the existing budget; the 4.2 split means a new record dilutes only its own family. A **new lane** opens only with ≥ 3 records at every tier it enters (V14). V14 runs per unlock set (none, all, each single); `RequiresHitRate` may drop ≤ 5 points; `UnlockValueNeutral` holds because value is exact.

### 4.17 Determinism (lead's call; change IR-14)

| Kind (new or changed) | Draws per entry | Entries | Wave |
|---|---|---|---|
| `lineRoll:<source>` | 4: d0 line, d1 alternate line (pre-picked for a carrier rejection at LootBind, 4.6), d2 element, d3 wand charges | One per identity of every source, dreamed options and unused fallbacks included | Reserved M1 (inert), used M2 |
| `curseGate` | 2 | 1 per floor | Reserved M2, used Later |
| `bossCardFallback`, `viaticumFallback` | 9 | **3 → 5** entries: entry = option index 0–3 (the fourth option's slot is entry 3), plus **entry 4, the hero-guarantee redraw** (lead's call, after the check) | M2 |

- **Keying:** `Derive(lootPlan, "lineRoll:" + source, entryIndex × 16 + optionIndex)` in the one key algebra (seeds and validation design 4.1, 4.2; change SV-1; lead's call, approved by the client 2026-11-23; M1): `lineRoll:{source}` is a registry-enumerated composed label, one registered label per source kind, entering `Derive` through `LabelId` (FNV-1a only as an input to `Mix`) and never built from data at run time; the index is unsigned and every bounded pick uses `Bound`. The rows of the table above are stream-registry rows, `loot/lootPlan/{N}/lineRoll:{source}/{entry·16+option}` and `loot/lootPlan/{N}/curseGate/0`, attempt-invariant (seeds and validation design 4.3; change SV-2, classification only, every count unchanged). `source` is the consumer kind (`dreamsDue`, `bossCard`, …) or `parent.container`, `parent.carried`, `parent.camp` for parent identities. optionIndex 0–3 are the options, 4–7 their per-option fallbacks, 8 the hero-guarantee redraw; ×16 leaves 9–15 spare (lead's call, after the check: the earlier ×8 had no index for the redraw). No key collides, and the hero redraw never reuses the draws of a fallback Theirs already used.
- **Parent stream unchanged.** Parent identities keep their positions; **the approved AR 14.3 Resistant draw for parent-drawn authored notables keeps its approved position** and serves only authored Resistant. A **rolled** element-choice line on any identity, parent or lootPlan, takes its element from d2 of that identity's `lineRoll` entry (`lineRoll:parent.*` for parent identities), theme-blind (lead's call, after the check). Parent wand charges keep their step-3 slot; d3 serves lootPlan wands only. M1 notables have no rolls, so **no M1 seed changes** beyond the filter-first adoption (4.1). **Positions read as block addresses (change SV-5; lead's call, approved by the client 2026-11-23; M1):** parent step 3 is keyed per floor and per entry block, `loot/floor/{N}/{block}/{entry}` (9 draws per identity entry), with the approved order kept as the order the blocks are evaluated; the AR 14.3 Resistant draw is the `resistant` block (r = `Bound(u, 720)`, then `r mod n`, K-rule 4) and parent wand charges the `wandCharges` block (seeds and validation design 4.3). **Pending request (to this owner, SV-5):** name the Resistant draw's owning entry; until it rules, the authored Resistant notable's own identity entry (seeds and validation design 14.2).
- **Always consumed.** The toy shows rolling only when needed varies 3–14 draws per run with the party, which breaks R-SEED-3.
- **Frozen generation order** (a segment-11 contract; taken up as pipeline stage GS10, with parent identities in GS11's keyed blocks, seeds and validation design 4.4): signature form selection (draw-free, 4.13); then LootPlan consumers in LE 6.10 order (the Due; arms floor; fit premiums; each cache's stepped then Wax-Key identity; boss-card options in option-index order 0–3, each slot resolved fully with its fallback before the next, then the hero-guarantee redraw (4.8); Viaticum options likewise; Wager; Twin Coffer); then parent identities in channel order (containers, carried, boss signature dummy, camp). Floor keys and caps accumulate in this order; reordering changes identities (told plainly). **Rare-trinket cap order (⚑ TK-27, ITEMS 19.2 "containers, carried, camp, boss"; lead's call, after the check):** LootPlan identities, fallback trinkets included, count **first** against `trinketRarePerFloorMax`; TK-27's order then holds among parent channels (TK owner confirms; filed under IR-14, fallback in 14.2).
- **Display parts** (Patina, stamp, Reverie, byname) use the `display` key's `itemPart` domain over (floor, instanceId) on the display side (`DisplayKey`, which Core assemblies cannot reference, DS12; seeds and validation design 4.3; change SV-2); the rules core never reads them. Told plainly: the same seed can *look* different across doors; identities never differ.
- **Client switches add no draws:** Plain Gifts (`known`), the usability split and Element Echo change no draw count or position (V14 draw-count test, 9.2).
- Reserved draws per run rise from about 846 (computed by 7b from the LE 6.10 table, illustr.; LE states no total) to about 1,300 *(illustr.; segment 11 computes)*, constant across seeds, doors, waves, parties and curses on or off. Segment 11's reading (seeds and validation design 8.1, SN1): about 1,300 is the **spent** count; the loot **address space** alone is about 4,500; `DrawLedgerPerRun` publishes both, and under DS7 neither is a stability requirement.

### 4.18 Hooks to 7c (sources)

Taken up by the loot sources design (`2026-11-22-loot-sources-design.md`, segment 7c, approved 2026-11-22; changes LP-13, LP-14), which states how each hook below is met (its 4.4, 4.10, 11); its pending readings stay with this owner and segment 9.

Each socket receives an **identity bundle** {recordId, rolledLineId, alternateLineId?, elementId?, charges?, dormant?, `known`, `oncePerRun` hit, `showsInUse`, Patina id, Reverie key, `foundTheme`, slot}. Carried sockets also receive remap eligibility; 7c supplies LootPlan with the provisional armed roster per carried socket. Bait caches and the boxed lurker take ordinary identities; the Twin Coffer takes Yours + Wayward; container art never implies contents (two named exceptions: the signature cache, 4.13, and the **Bedside Stand**, the F5 Viaticum host, 4.10; loot sources design 4.10; change LP-14; **agreed, loot sources Q5**; M2; 7c checks the rule as CT2 `ContainerArtBlind`, loot sources design 5.1); no source reads the theme for identity; 7c never re-deals a dreamed `oncePerRun` record. **(New lead's call, with Q5; renamed after the check:)** 7c places one **signature cache** in the lair of every boss whose **chosen** signature form is `guarded` (4.13; form selection precedes placement; "lair" read by 7c as the boss's **arena**, not FL 6.5's L room, pending the IR owner's and segment 9's confirmation, loot sources design 4.4, 13), outside the socket count, the socket ranks (it is not the SR4 elite lair cache) and the locked-or-trapped quota.

### 4.19 UI hooks (segment 10)

The card budget (6.1), merged with ITEMS 8.2 and TK 15; verdict glyphs in their footer badge; the mark table (6.1: Answers here, Plays with, +1 line, Suits); "Plays with: <feature>" (a record's `synergy[]` tag matches a member's class feature id); "+1 line over your <item>"; the suffix epithet with "?" and the reveal chime; the line-tag → shape table (6.1); the offer frame header labels ("Suits <name>", "Wayward"), offer order and the fourth's position; the dissolve and "What the dream kept"; the carried-use floater; the "Guards: <signature name>" plate line and the signature cache's plate decal; the Whence pane (provenance, Reverie, byname, Deeds); the theme stamp; the M3 "new to your codex" badge. ENM 15.8 limits hold: glyphs replace text, never add rows.

---

## 5. Theme loot tables

### 5.1 What a theme table is (lead's call; agreed, Q4; change IR-15)

A theme table is a **`theme.loot`** record on the RS 8.0 theme (additive, display and validator only; RS owner):

```
theme.loot {
  carriedFamilies[] : family ids     // derived from the RS 8.x kit columns of armed roster types, leaders, bosses (V14 (k) checks)
  answers[]         : family | prop | answers[] keys   // answer the theme's 6, counterType or Law verbs
  patina            : { materialSet, trimPalette, charm? }   // 6.2: material on the trim mask, at most one charm; hue reviewed at the RS 7.4 art gates
  stamp             : glyphId        // a pictogram (6.2 bans); card corner inside the cross-theme "found" frame, never on the icon body
  reveries[]        : { lineKey, part }   // about 12 per theme (illustr.); part = haft | grip | binding | blade | worn | any; RC1/RC9-vetted
  provenance        : captionKey     // "Found in <theme>, floor N"
  signatures[]      : derived at load from boss.signatureForms (4.13); V14 checks equality
  memento?          : { bossId, epitaphKey }   // F5
  proprietors[]     : ProprietorRecord        // 7c Proprietor tags, display only; a required `generic` record (loot sources design 4.5, 9.1; change LP-13; M2)
}
```

The rules core never reads it, except V14 and the Element Echo switch (IR-19). **"Answers here"** (display): `AnswersHere(item, floor)` matches an item's `answers[]` key, type or verb against the floor's 6, `counterType` and `damageTypes`. It reads the floor where the item is **presented during play** (field, carried, the Due, the Viaticum on F5); never on boss cards, camp stock or anything generated for floor N+1 (bring-the-key) (resolves IK14; told plainly: the Due is a floor-N container find, so it may carry the mark). Report `AnswerHereShare` per theme, target ≥ 30% *(illustr.)*, simulated in M2 and reported at playtest beside `LateFloorGearShare`. **On a miss (lead's call, after the check):** the named content request for that theme is answer records in common lanes at C and U (9.4), and the theme with the lowest share is the first candidate for M3 trims pulled forward.

Shared gates on every table: F1 C/U only; R F3+; VR F4+ ≤ 1 per floor; scrollFloorCap [1,2,3,4,4]; wands F2+; pieces C/U; Rare trinkets amulets F3+. Cells marked **[req]** are empty in approved content and feed 9.4.

### 5.2 Summary

| Theme | Floors, wave | Law | counterType | Resistant draw (approved, authored only) | Carried families (look; illustr., derived from RS 8.x kits, V14 (k)) | Key answers | Signatures (univ. = universal / spec. = specialist chassis; carriage M2) | Patina | Stamp |
|---|---|---|---|---|---|---|---|---|---|
| Gaol of Small Hours | F1, M2 | Lockdown | psychic | fallback | club (key-club), halberd, mace (key-mace), shield | Muffled, Shadowed, Ringed, Grappler, Jarring Whispers | Stopclock Mace (univ.); Cell Zero Flail (spec.) | iron, whitewash, striped cloth | striped shirt |
| Kennel Wood | F1, M1–M2 | Pack and horn | fire | fallback | spear (boar spear), crossbow, dagger (hunting knife) | Wolfcleaver, Ringed, Deep Stance, Ember | Bellwarden's Mail (spec., **guarded**); Frame-Master's Spear (univ.) | moss wood, hunting green, antler | collar buckle |
| Endless Wake | F1, M2 cond. | Sleeping house | radiant | fallback | mace, greatclub, staff, rapier, dagger | Vigil, Visor, Forewarned, Barred, Dawn Edge | Lying-in-State Leathers (univ., **guarded**); Executor's Letter-Knife (univ.) | wax ivory, crepe, jet | lily |
| Drowned Choir | F2–3, M2 | Drowning line | lightning | {cold, lightning} | mace, Reliquary shield, staff, warhammer | Storm/Frost Edge, Lightning Line, Sparks, Rime Lance | Sunken Carillon Hammer (spec.); Precentor's Baton (univ.) | silver, verdigris, storm-glass grey | pipe stop |
| Silk Archive | F2–3, M2 | Webs and silence | fire | fallback | rapier, glaive, pike | slashing families, Ember, Fireball, Grappler, Antidote | Vellum Gambeson (univ.); Index Crossbow (univ.) | vellum, ink lacquer, gold leaf | inkpot |
| Carnival of the Last Evening | F2–3, M2 | Oil and fire both ways | cold | {fire} | dagger, rapier, trident, greatclub | Emberward, Weathered (Burning), Oilskin, Frost, Ledgewalker | Ever-Lifted Barbell (univ.); Curtain-Fall Rod (spec.) | gilt, greasepaint, ticket card | ticket punch |
| Long Supper | F4 (F3 M3), M2 | Freeze and shatter | fire | {cold} | rapier, buckler, mace | impact, Skullsplitter, Bore, Ember, Wardstone, Icefall | Oxblood Brigandine (univ.) | old silver, oxblood, linen | assay mark |
| Gut of the World | F4, M2 | Attrition | cold | {acid, necrotic} | spear, shield, handaxe, sabre | light armour, Graveward, Antidote, Kinward, Heartward | Swallowed Splint (spec., **guarded**); Appetite's Pestle (univ.) | bone, sinew, apron cloth | ribcage |
| Dreamer's Heart | F5, M2 | Waking | radiant | fallback | shield, spear | staff, Barred, Grappler, Antidote, wards, Terror | Warming Pan (univ.); Wind-Down Hammer (spec.); Mementos | nursery rose, yarn, felt | rocking horse |
| Inverted Cathedral | F3–4, M4 | Edges and light | TBD | TBD | TBD | Ledgewalker, edge answers | TBD (segment 9) | lead came, censer brass | rose tracery |

Findings: only 4 of 9 M2 themes get a real element from the approved draw; every M2 theme meets the universal-chassis rule (the M1 Kennel subset is exempt, 4.13); Silk and Supper need IR-12's roster flags; Rare and VR gear rows are empty in 8 themes. "Carriage" is "carried" where the boss visibly wields it and segment 9 confirms `armed`; "guarded" where the boss is unarmed (4.13); carriage is derived per form, so a second form outside the boss's issue gear is always guarded. The "Carried families" column is illustrative: Kennel Wood and the Gaol are read from the RS 8.4 and 8.3 kit columns; the rest are derived the same way by V14 (k) when segment 9 confirms the kits (lead's call, after the check). **Segment 9's armed flags (boss rooms design 4.11.1; change BR-27; M2):** carried: the Jailer, the Thing in Cell Zero, the Master in the Frame, the Executor, the Drowned Carillon, the Precentor, the Weaver, the Strongest Man, Madame Ashes, the Head of the Table, the Appetite and both final bosses; guarded: Old Ragged, the Guest of Honour, the Mouthless Librarian and the Pylorus; so each "if armed" cell below reads as settled by this list. Stamps were renamed after the check to pictograms with no numerals, letters, keys or buttons (6.2).

**Second forms required in M2 (lead's call, after the check).** Each specialist-chassis signature is well under the 70% `SignatureUsableRate` gate for solo classes (Bellwarden's Mail about 25%), so its second form is requested now (9.4), not after the M2 report; the F5 Wind-Down Hammer takes none, and a cut boss's second form is not owed (boss rooms design 4.11.1; change BR-27). Lanes are proposals for segment 9; each second form re-trims an existing family model (6.2).

| Signature (first form) | Second form lane (proposal) | Carriage of the second form |
|---|---|---|
| Bellwarden's Mail (chain mail, heavy) | `lane.guard.light` (light body armour) | guarded |
| Cell Zero Flail (martial) | `lane.strike.finesse` or a simple weapon | guarded |
| Sunken Carillon Hammer (martial heavy) | the staff (`lane.cast.arcane` / simple) | guarded |
| Curtain-Fall Rod (arcane focus) | a simple or martial weapon (`lane.strike.blade`) | guarded |
| Swallowed Splint (heavy armour) | `lane.guard.light` or `lane.guard.medium` | guarded |
| Wind-Down Hammer (warhammer) | **none: no second form on F5** (boss rooms design 4.11.1; change BR-27; M2) | — |

### 5.3 The tables

**Reading the boss tables (lead's call, after the check).** Every signature "Lines" column is an **illustrative placeholder for segment 9**, not budget-complete: a U form must total 3 VP and an R form 4 VP (ITEMS 12.5, LE 6.3), and several rows are one value-1 base line short (Cell Zero Flail; the R forms Precentor's Baton, Index Crossbow, Oxblood Brigandine, Swallowed Splint, Appetite's Pestle, Wind-Down Hammer). Many authored lines also do not yet echo the boss's fight or kit. V14 (d) (value = budget) and V14 (h) (the echo warning) are the gate when segment 9 authors the records. Patina lines name materials first; each theme's single geometric **charm** is fixed in 6.2, and any other geometric detail named in a Patina line (pommel blocks, inlays, tags, dials) is Reverie text or a material read, never a mesh.

**Containers lines (7c).** Each theme's "Containers" line below is a summary; the container families, skins, Proprietors (`theme.loot.proprietors[]`, 5.1: the noun is always the skin's, never a category, tier, threat or safety) and signature loot moments are 7c's per-theme catalogue (loot sources design 5.2, 5.3; change LP-13; M2).

**T1. The Gaol of Small Hours (F1; M2).** Law: lockdown (doors, noise, keys); rewards stealth, shut doors, stolen keys, cowing. Damage slashing, bludgeoning, piercing; counter psychic; the 6: Bleeding, Staggered, Grappled, Dazed (boss), Prone, Frightened.

| Category | C | U |
|---|---|---|
| Weapons, foci | Gatebreaker (warhammer), Bonebreaker Mace; Corridor Warden (light crossbow) | Thunderclap Greatclub; Doorwarden's Pike |
| Armour, shields | Duskleather Jerkin (noise); Kneeguard Mail; Gatewarden's Shield | **Hushmail Hauberk** (Muffled: *the* Gaol find); Ringwarden's Shirt; Hailward Shield |
| Trinkets | Warden's Coif, Barred Helm, Grappler's Gauntlets, Strider's Boots, Grey Mantle | U pieces; Kinward Torc |
| Consumables | Jarring Whispers, Trembling Word (herald); Healing Potion, tonic | — |

Patina: flat iron, flaking whitewash, striped grip wraps, padlock-brass rivets, mismatched clock dials on pommels (the one wrong detail); no keyhole (RC3). Reveries: "Its little dial shows a different hour than yours." / "Marks cut in the grip: someone's sentence, unfinished." / "A cell number painted over twice." Carried: the Turnkey (armed, promotable) swings a key-club behind a cell-door shield; the Jailer-Sergeant carries a halberd; the Keybearer's keyring is a key, not loot (TL-23).

| Boss | Signature | Chassis | Lines (illustr.) | Carriage |
|---|---|---|---|---|
| Jailer of Hours | Stopclock Mace (U + authored) | universal | Thunderous (major) + authored Opener ("the Countdown") | carried |
| Thing in Cell Zero | Cell Zero Flail (U + authored) | specialist | Pry Deep + authored Shove-through | carried if armed, else guarded |

Containers (7c): cell caches behind Plain padlocks; one Crier lock after room 2.

**T2. The Kennel Wood (F1; M1 subset, M2).** Law: the pack and the horn; rewards walls, chokepoints, leader kills, area attacks. Damage slashing, piercing; counter fire; the 5: Bleeding, Grappled, Dazed (boss), Prone, Frightened.

| Category | C | U |
|---|---|---|
| Weapons, foci | Skirmisher's Spear, Hunter's Longbow, Magpie's Knife | **Wolfcleaver**, Thunderclap Greatclub, Doorwarden's Pike |
| Armour, shields | Boar-hide Coat (Deep Stance), Kneeguard Mail | Ironbark Hide, Ringwarden's Shirt, Tumbler's Leathers |
| Trinkets | Grappler's Gauntlets, Strider's Boots, Warden's Coif, Ember Ring, Trophy Tally | U pieces; Kinward Torc |
| Consumables | Fan of Cinders (arcanist); Healing Potion | — |

Patina: moss-stained wood, hunting-green leather, collar brass, antler grips, painted-moon silver; never lantern-amber flame (RC4). Reveries: "A collar tag, the name scratched out." / "Bristles caught in the binding." / "Its shadow leans toward a moon you cannot see." (a still model detail, never animated). Carried: the Huntsman (boar spear), the Poacher (crossbow), the Houndmaster (knife, horn emblem); hounds never carry; M1 keeps ≤ 1 carried weapon or armour notable.

| Boss | Signature | Chassis | Lines | Carriage |
|---|---|---|---|---|
| Old Ragged (M1) | **Bellwarden's Mail** (approved, AR 14.6) | specialist (heavy; fits the M1 Vanguard; second form required in M2, 5.2) | Adamant + Forewarned + authored Unbowed (approved; grandfathered from the echo check, 4.13) | **guarded in the den's signature cache** (IR-13; agreed, Q5); kit unchanged; the hound's coat is a torn surcoat, never loot (4.13) |
| Master in the Frame | Frame-Master's Spear (U + authored) | universal | Long Haft + Lunging + authored Long Skewer | carried |

Containers: the pen padlock (Plain, M1); the Huntsman's chest (Keyed).

**T3. The Endless Wake (F1; M2 conditional, else M3).** Law: the sleeping house (tells, Perception). Damage bludgeoning, piercing, psychic; counter radiant; the 6: Staggered, Unfocused, Bleeding, Dazed, Prone, Frightened.

| Category | C | U |
|---|---|---|
| Weapons, foci | Bonebreaker Mace, Wardwood Staff, Duellist's Rapier; Dawn Edge rolls | Vowkeeper |
| Armour, shields | Duskleather Jerkin | Tumbler's Leathers, Ironbark Hide (Forewarned) |
| Trinkets | **Watcher's Hood** (Vigil), **Visored Helm**, Barred Helm, Warden's Coif | Watcher's Hood of the Bar |
| Consumables | Jarring Whispers; Healing Potion | — |

Patina: candle-wax ivory modelled solid, mourning crepe, jet beads, coffin-wood grips, lily engraving. Reveries: "A black ribbon that will not come untied." / "Lily pressed flat in the binding." Carried: the Pallbearer (coffin pole, lid shield), the Undertaker (black cane-sword).

| Boss | Signature | Chassis | Lines | Carriage |
|---|---|---|---|---|
| Guest of Honour | Lying-in-State Leathers (U + authored) | universal (light) | Ghost Step + Forewarned + authored Shadowed | guarded (unarmed); worn form M3 option |
| Executor of the Will | Executor's Letter-Knife (U + authored) | universal | Fan Throw + Opener + authored Paired Throw | carried if armed |

Containers: the Will-Box (Keyed); the Viewing Door (Tattle).

**T4. The Drowned Choir (F2–3; M2).** Law: the drowning line (Soaked, cold, lightning); punishes fire reliance and clumping in water. Damage bludgeoning, cold, lightning; counter lightning; the 6: Soaked, Chilled, Staggered, Grappled, Prone, Frightened.

| Category | C | U | R (F3) |
|---|---|---|---|
| Weapons, foci | Bonebreaker Mace, Gatebreaker; Storm/Frost Edge rolls | Thunderclap Greatclub; staff | **[req]** Rare impact weapon |
| Armour, shields | Boar-hide Coat | Gale Gambeson, Resistant (cold, lightning) records, **Pilgrim's Reliquary** | Knight's Plate (example) |
| Trinkets | Weathered Cloak, Grappler's Gauntlets, Strider's Boots, Frost Ring | U pieces | Heartward Torc |
| Consumables | Frost Shard, Water Flask | Wand of Sparks | Lightning Line, Wand of Rime Lance, Slowing |

Patina: tarnished silver, desaturated verdigris, storm-glass grey, organ-pipe tin, hymnal vellum; no dripping water (RC1). Reveries: "A hymn number stamped on the haft." / "Its tin rings a quarter-tone flat when tapped." (card text only, no audio, RC7) / "Water-lines on the binding, though it is dry." Carried: the Drowned Verger (mace, pew-back shield: Choir floors are where Pilgrim's Reliquary is seen in use); the Deacon (crozier).

| Boss | Signature | Chassis | Lines | Carriage |
|---|---|---|---|---|
| Drowned Carillon (F2) | Sunken Carillon Hammer (U + authored) | specialist | Thunderous + authored Driving | carried if an armed construct, else guarded |
| Precentor (F3) | Precentor's Baton (staff, R + authored) | universal | Wellspring (major) + authored Shared Steadiness | carried |

Containers: Barb locks; the Vestry door (Keyed); the Wager room "the Loft of Responses" (M3).

**T5. The Silk Archive (F2–3; M2).** Law: webs and silence, fire is the key. Damage piercing, psychic; counter fire; the 6: Restrained, Poisoned, Unfocused, Grappled, Prone, Frightened.

| Category | C | U | R (F3) |
|---|---|---|---|
| Weapons, foci | Duellist's Rapier; slashing families; Ember Edge | glaive, Doorwarden's Pike; Wolfcleaver, Corridor Warden | **[req]** Rare launcher |
| Armour, shields | Duskleather Jerkin | Hushmail Hauberk, Gale Gambeson | **[req]** Rare light armour |
| Trinkets | Grappler's Gauntlets, Antidote Locket, Watcher's Hood, Grey Mantle, Ember Ring | U pieces; Kinward Torc | Heartward Torc |
| Consumables | Fan of Cinders, Fire Bomb, Oil Flask | Scorching Rays | Fireball |

Patina: vellum wraps, ink-black lacquer, lamp-gold leaf, thread bindings (never web texture, RC1), spectacle-glass inlays. Reveries: "A marginal note in a hand not yours." / "A shelf-mark, overdue." Carried: the Shusher (glaive, lectern board); the Head Archivist (quill-sword); the elite Spine-Binder needs the IR-12 `armed` flag.

| Boss | Signature | Chassis | Lines | Carriage |
|---|---|---|---|---|
| Mouthless Librarian (F2) | Vellum Gambeson (U + authored) | universal (light) | Far Weave + Warding Lining + authored Shadowed | guarded (unarmed; confirmed by segment 9, boss rooms design 4.11.1, BR-27) |
| Weaver of the Index (F3) | Index Crossbow (R + authored) | universal | Overwatch (major) + authored Long Skewer | guarded unless armed; lodged form M3 option |

Containers: the web seal (fire opens it); paper traps; the Closed-Stack cage (Keyed); the Margin-Crawler drawer (M3).

**T6. The Carnival of the Last Evening (F2–3; M2).** Law: oil and fire both ways; rewards cold, spacing, burning oil away. Damage fire, piercing, bludgeoning; counter cold; the 6: Burning, Smouldering, Oiled, Staggered, Prone, Frightened. Keeps Emberward live (RS 8.8).

| Category | C | U | R (F3) |
|---|---|---|---|
| Weapons, foci | Magpie's Knife, Duellist's Rapier, trident; Frost Edge | push weapons (Gatebreaker lane) | **[req]** Rare cold-answer weapon |
| Armour, shields | Boar-hide Coat | Resistant (fire) records; Ironbark Hide | — |
| Trinkets | Weathered Cloak (Burning), Oilskin Cloak, Ledgewalker's Boots, Frost Ring | **Emberward Amulet** | Heartward Torc |
| Consumables | Frost Shard, Water Flask, tonic | — | Wand of Rime Lance |

Patina: gilt and greasepaint stripes, bruise-purple tassels, ticket-stub tags, carousel-pole brass; no ember glow on non-fire items. Reveries: "A ticket stub tied on: ADMIT ONE." / "Greasepaint in the grain that will not wash out." Carried: the Lion-Tamer (prod-fork); the Ringmaster (cane-sword).

| Boss | Signature | Chassis | Lines | Carriage |
|---|---|---|---|---|
| Strongest Man Who Ever Lived (F2) | Ever-Lifted Barbell (greatclub, U + authored) | universal | Shove-through + Opener + authored Floorer | carried |
| Madame Ashes (F3) | Curtain-Fall Rod (R + authored) | specialist (arcane focus; ⚑ AR 5.5 focus) | Wellspring + Attuned (fire) + authored Conduit | carried |

Containers: the High-Striker prize cage; the Ring key (Keyed); the Ticket lock (M3; coin buys access, never loot).

**T7. The Long Supper (F4 in M2; F3–4 from M3).** Law: freeze and shatter both ways. Damage cold, bludgeoning, piercing; counter fire; the 6: Chilled, Frozen, Staggered, Poisoned, Prone, Frightened.

| Category | C | U | R / VR (F4) |
|---|---|---|---|
| Weapons, foci | Bonebreaker Mace, Skullsplitter, Gatebreaker, Duellist's Rapier | Vowkeeper, Thunderclap Greatclub, war pick (Bore) | **[req]** Rare impact and Ember weapons; **[req]** VR gear |
| Armour, shields | buckler | Resistant (cold) records | Knight's Plate (example) |
| Trinkets | Weathered Cloak, Antidote Locket, Wardstone, Ember Ring | Ward-Knot | Heartward Torc |
| Consumables | Fan of Cinders, Fire Bomb, Oil Flask | Greater Fire Bomb (F4), Scorching Rays | Fireball; VR Icefall |

Patina: old silver, oxblood lacquer, napkin linen, place-card tags, clear crystal without frost or blue. Reveries: "A place card bearing a name you almost know." / "Engraved: *not before the Head is seated*." Carried: the Butler-at-Arms (rapier, salver shield; needs `promotable`, IR-12); the Seneschal (ladle).

| Boss | Signature | Chassis | Lines | Carriage |
|---|---|---|---|---|
| Head of the Table (F4) | Oxblood Brigandine (R + authored) | universal (light) | Reprisal (cold, major) + authored Forewarned | carried (worn, with a carving-knife at-will; confirmed by segment 9, boss rooms design 4.11.1, BR-27) |

Containers: the Iced padlock (fire thaws, or a bludgeoning Smash); the cellar door (Keyed).

**T8. The Gut of the World (F4; M2).** Law: attrition (Corroded, Withered); punishes heavy armour and heal reliance. Damage acid, piercing, necrotic; counter cold; the 6: Corroded, Withered, Poisoned, Grappled, Prone, Frightened. The one theme whose answers are mostly defensive.

| Category | C | U | R / VR (F4) |
|---|---|---|---|
| Weapons, foci | Skirmisher's Spear; Frost Edge | handaxe, sabre; Grave Edge | **[req]** Rare cold weapon |
| Armour, shields | Duskleather Jerkin, leather, gambeson, brigandine; shield | Tumbler's Leathers, Gale Gambeson, Resistant (acid, necrotic) | **[req]** Rare light armour |
| Trinkets | Graveward Amulet, Antidote Locket, Grappler's Gauntlets, Frost Ring, Lifeline Pendant | Kinward Torc | Heartward Torc |
| Consumables | Frost Shard, tonic | — | Wand of Rime Lance; VR Icefall |

Patina: desaturated bone and sinew, flesh-pink leather, apron cloth, brass rent-tokens (no keys or keyholes, RC3, TL-23); bile green never used. Reveries: "A rent receipt, paid in teeth." / "A tenant's name on a brass plate; no door you know." Carried: the Antibody Knight (spear, rib-plate shield); the Landlord (cleaver).

| Boss | Signature | Chassis | Lines | Carriage |
|---|---|---|---|---|
| Pylorus | Swallowed Splint (R + authored) | specialist (heavy) | Resistant (acid, major) + authored Unbowed | guarded (unarmed); lodged form M3 option |
| Appetite | Appetite's Pestle (greatclub, R + authored) | universal | Thunderous (major) + authored Hungering (Withered blocks it in its own Gut: "the Law both ways", stated on the card) | carried if armed |

Containers: sphincter caches; the Tenant's strongbox (Keyed); the Gutworm strongbox (M3).

**T9. The Dreamer's Heart (F5; M2).** Law: waking; a capped war on concentration. Damage psychic, piercing; counter radiant; the 6: Unfocused, Poisoned, Dazed, Restrained, Prone, Frightened.

| Category | C (spiky deck only) | U | R / VR |
|---|---|---|---|
| Weapons, foci | Wardwood Staff; Dawn Edge | staff (Shared Steadiness), totem | **[req]** Rare staff; F5 VR (spiky) **[req]** |
| Armour, shields | shield | Pilgrim's Reliquary | Knight's Plate (example) |
| Trinkets | Barred Helm, Grappler's Gauntlets, Antidote Locket, Warden's Coif, Wardstone | Ward-Knot, Kinward Torc | Heartward Torc |
| Consumables | Trembling Word | — | Terror, Unweaving, Slowing; **the Viaticum** (approved) |

Patina: nursery rose, sheet off-white, desaturated moonlight grey, yarn, felt, toy-block pommels, sewn name-tapes; toys played sinister. Provenance shows the floor where an item was *found*; nothing implies who the Dreamer is (RS 7.5). Reveries: "A name-tape sewn in, the name unpicked." / "It is the size you remember it." Carried: the Sleepwalker (fire-poker, headboard shield); the Mother-Shape (knitting-spear).

| Boss | Signature | Chassis | Lines | Memento epitaph (tone review) |
|---|---|---|---|---|
| Night Governess | Warming Pan (mace, R + authored) | universal | Quake (major) + Opener + authored Floorer | "Warming Pan, carried out of the Heart. Still warm." |
| Rousing Engine | Wind-Down Hammer (warhammer, R + authored) | specialist | Thunderous (major) + authored Driving | "Wind-Down Hammer, carried out of the Heart. It has stopped." |

Both Heart signatures are carried (boss rooms design 4.11.1; change BR-27; M2): chassis only on the boss, damage psychic or piercing by the Heart's set (the Warming Pan's at-will *There, There* deals psychic); **no second form on F5**, so the Wind-Down Hammer always drops as its first form.

**T10. The Inverted Cathedral (M4; F3–4; stub).** No roster, 6, bosses or counterType yet (NM-9, RT-15 pending; never in a pool set with the Choir). Provisional answers: Ledgewalker's Boots, Strider's Boots, push weapons, Deep Stance, Stalwart. Patina: lead came, censer brass, inverted gilt; radiant hue reserved. **V14 blocks shipping until the record is complete.**

---

## 6. Readability, art and audio

### 6.1 The card budget (lead's call; segment-10 contract; resolves IF4)

**Merged with the approved layouts (lead's call, after the check; ⚑ ITEMS 8.2 and TK 15, filed under IR-9).** ITEMS 8.2 and TK 15 already fix a card; 7b's budget is written onto them, and where they differ this spec wins:

1. **Name line:** the record name with its suffix epithet (the roll). While dormant, "?" appears **only in the epithet slot** ("Strider's Boots of ?"). No separate "rolled" pip. Rarity keeps ITEMS 8.2's frame shape plus label.
2. **Mark line:** at most **2 marks**, by priority: Answers here > Plays with > +1 line > Suits <name>. The offer slot label is **not** a mark: it sits in the offer frame header (4.8), so it never competes with item marks.
3. **Body:** the property lines with their line glyphs. A dormant line stays as TK 15's hidden-glyph line and carries the Arcana readout ("? dormant · Arcana 13 vs 15"), which **replaces** ITEMS 8.2's separate "?" pip, so a dormant item shows one "?" in the name and one hidden line, nothing else. ITEMS 8.2's "Arcana identified" tag is kept.
4. **Footer, per member, in this order:** [the `ItemVerdict` glyph and word, inside a fixed filled rounded-square **footer badge**] [the ITEMS 6.3 gain] [ITEMS 8.2's fit wording, renamed the **fit line** in the segment-10 hook so it is never confused with the verdict] (for example "▲ upgrade · +2 · fits: spare", "⇄ another way · −1 · untrained").

**Marks (one glyph and one word each; placeholders for segment 10; registered in the RC5 pattern registry; shape-distinct from the verdict glyphs and the line glyphs):**

| Mark | Glyph (placeholder) | Word | Rule |
|---|---|---|---|
| Answers here | ✦ four-point star | "Answers here" | 5.1 |
| Plays with | ⧉ linked squares | "Plays with: <feature>" | 4.19 |
| +1 line | ⊕ circled plus | "+1 line over your <item>" | 4.5 |
| Suits | ◉ ringed dot | "Suits <name>" (field mark: the record is on that member's lane; the same word heads a Yours or Theirs offer frame) | 4.3, 4.8 |

**Line tags → line glyph shapes (lead's call, after the check; owners: segment 10 with the W, AR and TK owners).** TK 15's five shapes are kept for trinkets and extended by line tag, reusing the tags that derive `showsInUse`: passive defence and resist → TK's *prevent* shape; cost, reach or step → TK's *cost* shape; channel → *channel*; token → *token*; button and charge → *button*; rider and onHit → a new *edge* shape; reaction → a new *answer* shape; major lines take their tag's shape with a double outline. Verdict glyphs never share a container with line glyphs (the footer badge), so ▲ ▽ never sit beside TK's ▸ at the same size.

All flavour (provenance, the Reverie, a byname, Deeds, Patina caption) sits in a collapsed **Whence** pane: opened on inspect, in Grave Goods, on the Memento and in the codex; never on the post-fight summary. **One flavour line per item:** the Reverie, chosen by display hash **only among the theme's Reveries whose `part` tag fits the item's family** (haft, grip, binding, blade, worn, any), so a ring is never said to have a haft (lead's call, after the check; V14 (f) checks ≥ 3 compatible Reveries per theme per category). No Reverie or epitaph may imply heat, cold, light, sound or motion as an effect (RC4, RC9); the Warming Pan epitaph's "Still warm" and the Kennel Reverie "Its shadow leans toward a moon" go to the RS 7.4 tone review on that test. **Bynames** are given only to items that earn a Deed (felled a boss), so a byname means something. Item **Deeds** reuse CMP 8.7's Deeds pipeline (display only, never power); if CMP cuts its Deeds to M3, item Deeds follow.

### 6.2 Art (lead's call; agreed, Q4; change IR-15)

- **Silhouette = family.** A Gaol mace and a Heart mace share an outline. The theme changes only material on a **trim mask** (grip, wrap, rivets, pommel, finish).
- **Patina = materials plus at most one charm (lead's call, after the check).** (a) A **material set** on the trim mask: a shader job, M2-cheap. (b) At most **one optional charm** per theme on a fixed socket, inside the family's silhouette envelope (≤ 10% of bounding length, no dangling elements): Gaol, padlock-brass rivets; Kennel, an antler grip; Wake, jet beads; Choir, an organ-pipe tin ferrule; Silk, thread bindings; Carnival, carousel-pole brass; Supper, an old-silver collar; Gut, brass rent-tokens; Heart, a sewn name-tape. Every other geometric detail in a 5.3 Patina line is Reverie text or a material read. *(illustr.; art finalises)*
- **Where Patina shows (lead's call, after the check).** Patina is **model-only**: unit models at tactical zoom and the inspect render. Icons stay global and untinted, so the card's theme read in M2 is the stamp and the provenance line, not Patina. Themes without trims in M2 show the **base finish**, with the stamp and provenance still shown.
- **Patina bans:** flame, frost, acid or storm effects; the keyhole motif and keys (RC3; TL-23); square-kind textures (water, web, oil, bile, bedsheet) (RC1); roster-prop silhouettes on items; clock faces on lock-like objects. Element VFX come only from properties. **Hue** is checked at the RS 7.4 art gates (greyscale and colour-blind simulation against the RC4 damage-hue key), not by V14, because the key does not exist yet; each theme's Tone Sheet lists "review against the key" items: oxblood (fire or bleed red), bruise-purple, flesh-pink (Gut necrotic), verdigris (acid and the reserved bile green), and gilt or lamp-gold leaf (conditional on the radiant hue's final value) (lead's call, after the check).
- **Stamps:** pictograms only, shown in the card corner inside one cross-theme "found" frame so the stamp reads as provenance, not maker; no numerals, letters, keys, keyholes, faces, clock faces or button shapes (V14 (f), (k)) (lead's call, after the check).
- **No Patina notable accent (cut after the check):** the ENM glint icon is the one marker of a carried notable (4.12).
- **Icons:** one per family or notable, global; the theme stamp sits in the card corner.
- **Scions** reuse the parent model with a stronger finish.
- **Signatures keep their family's silhouette envelope** (length class, head-mass position, one- or two-handed grip); bespoke detail lives inside it. Where the name's object cannot read as its family (the Ever-Lifted Barbell), the model is a greatclub dressed as a barbell and the hover verb line leads with the family word. An art-gate check (RS 7.4 greyscale and role-ID gates) confirms each signature reads as its family at minimum zoom (lead's call, after the check).
- **Signatures:** first forms are bespoke models; **tier forms reuse one model with a finish step**; second forms re-trim an existing family model, never a bespoke model; guarded signatures show their global icon on the signature cache's plate decal (4.13); the F5 Memento card renders the same model on the victory screen.
- **Art volume (illustr.; lead's call, after the check):**

| Item | M2 | M3 |
|---|---|---|
| Trim masks (once per family model) | about 80 | — |
| Material sets | 2 (Kennel, Gaol) | 8 more |
| Charms | about 2 | about 8 more (about 10 small meshes) |
| Stamps | 10 | — |
| Signature first forms (bespoke) | 15 | + 2 (Wake) |
| Signature tier forms | finish step, no model | — |
| Signature second forms (re-trim) | about 6 (5.2) | about 11 more |
| VR records (≥ 2 per lane group) and non-Scion Rare records (9.4) | Scion re-finishes where a parent exists; new models counted per request | — |

  Order: Kennel and Gaol at M2 (M1 art carries over), the rest at M3. Risk 9 holds: signature authoring roughly doubles per boss.

### 6.3 Audio

No new cue sounds (RC7 reserves them; "sting" is RC7's word for the boss banner and is not used for any 7b event). Every 7b presentation event reuses the global UI sound family, never a combat cue (lead's call, after the check):
- the identification **reveal chime** (UI family);
- the dreamed-option dissolve (UI family; no new cue) and "What the dream kept" (silent);
- the signature cache opening (the standard container-open UI sound, not a reserved door or trap cue; 7c adds only an optional quiet theme foley tail, loot sources design 5.1 CT12; change LP-23; M2);
- the carried-use floater (one quiet UI-family tick on the first floater per carrier per fight, mutable in options; never the banner sting).

Reveries that mention sound ("rings a quarter-tone flat") are card text only.

---

## 7. New mechanics considered

| Mechanic | Source | Verdict | Where |
|---|---|---|---|
| Lanes and the Suits mark | NM, all drafts | **Adopt** M1 data / M2 | 4.3 |
| Usability floor ("No Dead Finds") | NM, A, B | **Adopt**, split hard/soft (agreed, Q1) | 4.3 |
| The Triad (Yours / Theirs / Wayward + fourth) | NM, all | **Adopt** M2 | 4.8 |
| Never twice (memory + `oncePerRun`) | NM, A, B, C | **Adopt**, staged, generation-counted (agreed, Q2) | 4.7 |
| Telling names (epithets, "?" blank) | NM, A, B | **Adopt** M2 | 4.6 |
| Line-kind hallmark decals | NM, C | **Narrowed:** the TK line glyph extended; no maker houses, no decals | 6.1 |
| Scions (rarity steps) and the trinket exception | NM, B, C | **Adopt** M2 | 4.5 |
| Plain Gifts | NM, B, C | **Adopt** (agreed, Q3) | 4.14 |
| Seen in Hand (showsInUse, elite, the carried-use name floater) | NM, B, C | **Adopt** M2 (the name display moved from a banner to a floater after the check) | 4.12 |
| Signature second forms | NM, A, C | **Adopt** M2 where usable < 70%, else M3 | 4.13 |
| Guarded signatures (unarmed bosses) | consistency critique IK1 | **Adopt** M1 (agreed, Q5); worn or lodged forms an M3 option | 4.13 |
| Wax-Key sibling (draft name "Two Ways", dropped for collision) | NM, B, C | **Adopt** M2 | 4.11 |
| Whence (provenance, bynames, Deeds) | NM, A, B | **Adopt**, inside the card budget | 6.1 |
| Theme Patina | NM, B, theme draft | **Adopt** M2 text, Kennel and Gaol trims; M3 rest | 6.2 |
| Theme Echo / Element Echo | all | **Hold** for an M3 client decision, default off (agreed, Q4) | 4.4 |
| Lane-preserving theme tilt | theme draft Model B | **Reject** (+0.02 items per floor; reopens RS 9.1; agreed, Q4) | 4.4 |
| Verdict glyphs | NM, all | **Adopt**, "=" narrowed | 4.14 |
| Re-engraving | NM | **Hook** to the camp owner, M3 | 4.6 |
| Roll block and curse reservation | NM, A, B, C | **Adopt**, 4 + per-floor 2 | 4.17 |
| KinSwap | theme draft | **Adopt** as a LootBind remap | 4.12 |
| "Answers here" | theme draft | **Adopt** M2 | 5.1 |
| "What the dream kept" | A | **Adopt** M2 | 4.8 |
| Maker houses with line tilt | C | **Reject** (a theme-lore stamp on every floor reads as theme loot that is not; an unfelt tuning layer) | — |
| Tactical dream quirks; item memories that add lines | brief, NM | **Reject** (value = budget, RC1); kept as Reveries and Deeds | 6.1 |
| Duplicate → coin; camp trade-up for a higher tier | brief, NM | **Reject** (creates coin or VP outside the deck) | — |
| Numeric ranges, greater affixes, attunement, rare pity | brief, research | **Reject** | 4.6 |
| 2-piece Pairs | A, research | **Park** for Later (breaks value = budget; sets beyond 2 unreachable at about 34 notables) | — |

Adopted mechanics are specified in full in section 4 at the references given. "NM" is the new-mechanics brief; A, B and C are the model drafts; the drafts' own local ids appear only in the cross-reference A.6.

---

## 8. Numbers with fixes (toy, not simulator; all illustr.)

Source: the 7b toy (`sim7b.py` and companions, 300 runs per party per configuration). **APPROVED** = LE 6.2 as written; A, B, C = the three models; **M2** roster = approved notables; **TGT** roster = content requests filled. Columns: VG, SH, AR, HR = solo Vanguard, Shade, Arcanist, Herald; P4 = party of 4 with a Shade hero.

**Caveats (told plainly; resolves IK18).** The toy (1) used "the Due takes the highest non-VR card" instead of LE 6.6's lowest card; (2) gave trinkets random `usefulFor` instead of TK rule 26's ≥ 2 classes or `anyCompanion`; (3) treated out-of-class scroll reads as free; (4) tested the sidegrade by lane only; (5) did not model the Risk Ledger, Wax-Key draws in runs, coin or the F5 tactical cap. Every target below is re-run in the M2 simulator before it becomes a value in data.

| Measure | APPROVED | B-fixed (expected) | Fix in this design |
|---|---|---|---|
| Unusable field gear, solo (TGT) | 13 / 38 / 49 / 20% | 0% hard; low single digits at 100‰ (to verify) | 4.3 split floor (C's 250‰ leaked 22–27%) |
| Solo on-party / wayward (TGT) | 51–60% / 12–22% | 65–76% / 24–36% | Healthy; lever held (4.3) |
| Offers all usable (VG+HRc) | 40% | 100% | 4.8 |
| Offers with ≥ 1 hero-usable option (VG+HRc, TGT) | 21% | 42–48% → ~100% | Hero guarantee (IT7) |
| Mean distinct categories, martial sets | 1.7 | 1.6–1.7 → about 2.8 | Trinkets in the fallback pool (IT8) |
| Sets with > 1 damage option (casters) | 27–43% | 0% | ≤ 1 damage |
| Gear record dealt twice | 5–18% | 0–1% | 4.7 |
| Arms share of field VP (TGT, target about 32%) | 49–61% | about 30% | 4.2 split (B as written: 50–60%; bug E1) |
| Trinket share (target about 19%) | 5–8% | 6–18% | Rare neck families; trinket Scions; anyCompanion |
| Same F4 VR identity per run | — | 52–62% (1 per lane group) | ≥ 2 VR per lane group → end builds 35–41 (Shade, Arcanist) |
| Companion trinket slots empty, P4 | 75% | about 40% | `anyCompanion` lever (IR-17) |
| Signatures usable, solo SH / AR | 65% / 71–73% | 100% with second forms | 4.13 |
| Kin share from a ×1.5 tilt | — | +0.02 items per floor | Tilt rejected |
| Reads per card | 2.9–3.2 | about 3.2 (C: 3.5–4.8) | Card budget (6.1) |
| Reserved draws per run | about 846 (7b's count from the LE 6.10 table) | about 1,300 | Constant by construction |

**Toy failures and fixes (IT1–IT15; the drafts' F1–F15):**
- **IT1 roster wall.** On M2, F4–F5 Due waived 78–100%, F3 13–56%; tactical items 50–75% of field VP; 3–12 silent fit-premium step-downs per run. → The tier never moves silently (IR-7); tactical guard (IR-16); content requests (9.4); V14 warnings and `StepDownPerRun`, `LateFloorGearShare` (target ≥ 50% on F4–F5) **block M2 playtest sign-off**, not the build (resolves IF1 with IK6).
- **IT2 VR collapse.** → ≥ 2 VR per lane group as a content request sized by `ChaseVarietyByClass` (≤ 35%); "named VR first" only as a ×2 weight (4.2 socket term 4, records flagged `chase`); until content lands, the F4 VR card takes the approved tier-stable path rather than dealing the same item every run.
- **IT3/IT4 weights.** → 4.2.
- **IT5 over-claimed on-build.** → Expectations corrected; lever held.
- **IT6 diversity under the hard floor.** → Soft floor on C/U/R field cards; `BuildDiversity` ≥ 15 distinct end builds per class per 100 seeds *(illustr.)* as a blocking report.
- **IT7, IT8 offers.** → 4.8.
- **IT9 Due saturation.** → Re-run with the correct card rule; held lever (4.9).
- **IT10 CMP-30.** → IR-17 plus Rare neck families.
- **IT11 theme tilt.** → Rejected.
- **IT12 signatures.** → 4.13.
- **IT13 pipeline order.** The IR-7 rungs run only after the socket's own target relaxations are exhausted (4.1 step 5, 4.7 item 8). → V14 test with a fixture where both paths are possible.
- **IT14 unique by shown.** → `oncePerRun` by generation.
- **IT15 readability.** → Card budget.

Added after the balance critique: **`ScrollBossShareByClass`** (CN 19.3 alarm at 25%, now read for every class, not just the Viaticum); the IR-16 guard addresses the expected spike for solo Arcanists on the M2 roster.

---

## 9. Data, validator V14 and simulator

### 9.1 Records and keys (`inventory.json` `loot.*`; all illustr.)

```
loot.categories       [weapon, focus, armour, shield, piece, jewel, scroll, wand, flask, talisman]   // fine categories (4.2)
loot.categoryGroup    { weapon:"arms", focus:"arms", armour:"guard", shield:"guard", piece:"trinket",
                        jewel:"trinket", scroll:"tactical", wand:"tactical", flask:"tactical", talisman:"tactical" }  // LE 6.2 groups
loot.lanes            { "lane.strike.blade": {category:"weapon", universal:false}, ... }   // fine category
loot.classLanes       { vanguard:[...], shade:[...], arcanist:[...], herald:[...] }
loot.subclassLanes    {...}        // kit lanes live on companion kit rows
loot.effectTags       [damage, control, ward, mobility, utility]   // alias use.damage
loot.weightSplit      "lane>family>record", divisorsOn: "staticLegalSet", scale: 1048576   // 4.2 factor order
loot.requiresBias     { default: 3, bossCard: 4 }                  // settled
loot.usability        { hardOn: [due, fit, armsFloor, offer, fourth, viaticum, stepped, waxKey, camp, wager, twinCoffer, vr],   // agreed Q1, not tuning
                        arcanaReadOn: [field, carried, vr, stepped, waxKey, camp, wayward, fourth],   // Viaticum, Wager, Twin Coffer: per slot
                        fieldUnusablePermille: 100 }
loot.memory           { scionLineOnce: {C:true, U:true, R:false}, deadRareOnce: true, oncePerRunBy: "generated",
                        spellFamilyRepeatPermille: 500 }  // agreed Q2; R flips per 4.7 item 4
loot.plainGifts       true                                          // agreed Q3, not tuning (new lead's call)
loot.trinketScion     true                                          // TK owner
loot.lineRoll         { draws: 4, smartMatch: 2, ownedDivisor: 2 }
loot.offer            { slots: ["yours","theirs","wayward"], fourth: "wayward",
                        maxDamage: 1, heroGuarantee: true, fallback: "usableGear", keyingStride: 16 }
loot.carried          { showsInUsePermille: 2000, eliteShowsInUse: true, kinSwap: true }
loot.waxKey           { otherLanePermille: 2000 }
loot.vr               { namedFirstPermille: 2000 }
loot.signatureCarriage { guardedBy: "signatureCache", plateLine: "Guards: <name>" }   // agreed Q5 (new lead's call: keys; renamed after the check)
loot.tacticalGuard    { rareTacticalFieldMaxPerFloor: {3:2, 4:2}, tier4ScrollsDefault: 1 }
loot.levers           { requiresTargetPermille: null, anyCompanion: {minParty:3, permille:null},
                        scionPullPermille: null, dueSidegradeFromFloor: 4, yoursLabelDrop: null }
loot.curse            { permille: null, gateDrawsPerFloor: 2 }
loot.elementEcho      false                                          // IR-19, agreed Q4: an M3 client decision; false in release builds
loot.identifyDifficultyByTier { U: 12, R: 14, VR: 16 }
loot.lineLexicon      { "prop.honed": {suffix:"of the Whetstone"}, "prop.trinket.barred": {suffix:"of the Bar"}, ... }   // suffix only
record (adds)  lootLane, category (fine), effectTag?, scionOf? (≤ 1 child per record), rollable.eligible[] (3–5 per family), synergy[],
               flags: oncePerRun (explicit), chase?, cursedTwinOf? (Later)
weapon (adds)  trainingGroup
boss (adds)    signatureForms {U:[formId…], R:[formId…], VR:[formId…]} (authored order; carriage derived per form), signatureAbilityTag
prop (adds)    carrierSafe, showsInUse (derived), lexicon, verbTag
reverie        { lineKey, part }
instance (adds) rolledLineId, alternateLineId?, elementId?, known, patinaId, reverieKey, foundFloor, foundTheme, deeds{}
               // patinaId, reverieKey, foundTheme and deeds are display only (V14 (j))
snapshot (adds) soldOrDroppedThisCamp[]     // the Due's "still dealt" rule (4.9)
LootLedger (adds) generated[] {recordId, kind, floorIndex, tier, socketKind, slot?, entryIndex, usableBy[], onParty,
                               oncePerRun, relaxationStep}     // kind per the 4.7 table
                  bossContent[] {bossId, formId, carriage, floorIndex}   // signatures; never read by exclusions
theme.loot     (5.1; display and validator only)
```

Pools are **derived at load** from records; there are no hand tables to drift.

### 9.2 V14 `ItemRolls` (change IR-21; beside V13 in RS 11.2 and LE 10.2)

**Phase model (seeds and validation design 5.1, 5.2; change SV-20; lead's call; M1 model, M2 complete).** Checks carry qualified ids `V14.<Check>`; the static rows are phase `s`, the generation-time asserts phase `g` at GS10/GS11, the tests phase `t`. Waves as in its 5.2: M1 (a), (b), (d), (h), (j) and the draw-count test; M2; Later (m). The simulator gates below are not V14 parts: they move to the report registry and keep their ids as aliases.

**Static (M1 subset marked).**
- (a) **M1** Every loot record has exactly one `lootLane` and one fine category; every class, subclass and kit declares `LanesOf`.
- (b) **M1** Every (floor, tier, category) a deck variant can request has a legal record or a re-weight path, for every solo-class snapshot. **Error** only when a reachable card has no path at all (no record, no re-weight, no `SubstitutedVP` path).
- (c) Lane coverage, per (floor, tier, lane, solo class whose `LanesOf` contains the lane): ≥ 3 legal records from ≥ 2 families, and records ≥ 1.5 × the most cards of that tier the floor can deal. **Warning** with a named content request below that. The Rare memory switch reads (c) at Rare (4.7 item 4).
- (d) **M1** Value = budget per record; ≤ 3 lines; ≤ 1 rolled line; fixed lines obvious; per-family rolled tables legal (rule 28 for all categories, W 18.1, Adamant); 4.2 weights: no positive survivor floors to 0, every intermediate below 2^62.
- (e) Scion supersets; at most one Scion child per record (true lines); trinket Scion rules.
- (f) Lexicon, Reverie, byname and Patina names: no collisions (AR 17.1 rule 13; TK 15); every rollable epithet a suffix; record display names unique (Scion rank words); ≥ 3 part-compatible Reveries per theme per category; stamps are pictograms (6.2); RC1/RC9 vetting list (including "no Reverie implies heat, cold, light, sound or motion as an effect").
- (g) Offer shapes satisfiable or with a fallback for every party lane set and solo class, including martial parties without Arcana and Heralds without wands; hero guarantee satisfiable; the offer terminal ladder's tier step unreachable or its gap named (worst cases: solo Shade, solo Vanguard, Herald without Arcana on F3, F4 and F5 on the M2 roster); the Viaticum satisfiable after the tier-4 lever is spent.
- (h) **M1** Signatures: a form per boss for every reachable (floor, variant) top tier; ≤ 3 lines; R/VR forms on a family with a value-2 major; carriage derived per form (`carried` only on an `armed` boss and a form in its issue gear, else `guarded`; agreed Q5, check a new lead's call); `theme.loot.signatures[]` equals the boss records. **Warnings, M2+:** the authored line's verb tag matches neither `boss.signatureAbilityTag` nor a kit condition tag (approved M1 records grandfathered); a specialist-chassis signature has no second form (F5 signatures exempt, boss rooms design 4.11.1). **M2+ on complete theme catalogues:** ≥ 1 universal-chassis signature per theme (the M1 Kennel subset exempt; the Kennel under Cut 1c pending the BR-27 reading, 4.13).
- (i) No `oncePerRun` record eligible for the fourth or a dual-identity cache; every record of an implicit `oncePerRun` class carries the explicit flag.
- (j) **M1** Display isolation: no rules code reads `display`, `theme.loot`, Patina, Reverie, byname, Deeds or provenance (except V14 and the Echo switch); no exclusion reads `bossContent[]`; a release configuration with `loot.elementEcho` on fails (new lead's call, agreed Q4).
- (k) Every drawable theme has a complete `theme.loot`; carried families are issue families of armed types in the RS 8.x kits; Patina records are complete and their ids exist (hue is an RS 7.4 art gate, 6.2); stamps obey 6.2's bans; the Cathedral is blocked until complete.
- (l) Camp entries and the phial swap are never VR; offers never hold healing; the phial is never a weighted draw and reaches an offer only as a boss-channel pre-roll placement in option 2 (4.1); unlock lanes have ≥ 3 records per tier entered.
- (m) `cursedTwinOf` links (Later).

**Simulator gates (M2; fixed seed set of 1,000 seeds; lead's call, after the check; report-registry rows, the named gate set `IR-gate-1k`, class SHIP/REL, seeds and validation design 4.11, 6.12).** `oncePerRun` and Scion-line depth: no (tier, lane) empties before F5 without a logged relaxation, read on the worst seed (minimum remaining depth); `LinePowerDelta` per entry within band (4.6).

**Generation-time (each with an action; phase `g`; failure class `repair`, the relaxation rungs and fallbacks as LR1, then `defect`, LR5; seeds and validation design 5.2, 5.5).** Every offer option usable (fallback, terminal ladder, then assert); families distinct (assert); `oncePerRun` never generated twice (assert); ENM 20.1 floor keys unique among deck notables (assert; signatures exempt, 4.7 item 7); hard-floor sockets never hold an unusable record (assert); every tier change logged in `SubstitutedVP` (assert); relaxation steps logged.

**Tests.** **M1** Constant draw counts across seeds, doors, waves, unlock snapshots, opening histories, curses on/off, Plain Gifts on/off and Echo on/off; LootDoorInvariant (identical `generated[]` across a door swap; signatures live in `bossContent[]`, so the comparison needs no exemption); pipeline order (the IR-7 rungs run only after the socket's target relaxations are exhausted, tested on a fixture where both paths are possible); Scion-line fixture (a parent with one child; generating the child excludes the parent, generating the parent leaves the child legal); exclusion-history stability of `LootMixByCategory` (±5 points).

### 9.3 Simulator reports (change IR-22; M2; targets illustr.)

Every report reads `LootLedger` rows at the **generation snapshot** unless it says otherwise (lead's call, after the check: numerators, denominators and snapshot times made explicit).

**Result classes and reading rules (seeds and validation design 6.3–6.6, 6.12; change SV-25, with the SV-23 classes; lead's call; M2).** Each row declares one result class and is read by that class's rule (6.4) at the minimum N of 6.5: the playtest-blocking rows (`DeadRareRepeatRate`, `StepDownPerRun`, `LateFloorGearShare`, `BuildDiversity`) are class PB, read one-sided at z = 1.96; `RelaxationStepRate` is a BUDGET row at its approved rate (5.6). Zero gates take a structural partner (6.6): `DeadRareRepeatRate` = 0 and `OncePerRunGeneratedTwice` = 0 are partnered by V14's generation asserts on the ledger; `DormantAtChoice` = 0 by a V14 static check. **Pending request (to this owner, SV-25):** the `DormantAtChoice` proof; if refused, the row is read at 30,000 floors (≤ 0.01%) with no structural partner, which bounds a rate, not impossibility (seeds and validation design 14.2).

| Report | Definition (numerator ÷ denominator; snapshot) | Target |
|---|---|---|
| `RequiresHitRateBySize`, `OnBuildShare` | notables with `onParty` ÷ notables, field cards, by party size | solo on-build 65–80%; gap ≤ 10 points (LE) |
| `WaywardShare` | field notables usable by some member but on no member's lane ÷ field notables (field-find meaning only; the offer slot is `OfferPickBySlot`) | reported (24–36% expected) |
| `UsableDropShare` | notables usable by some member at generation ÷ notables, split hard / soft sockets | 100% hard; soft reported |
| `OfferUsableRate`, `OfferDistinctness`, `OfferHeroUsableRate` | per set; boss card and Viaticum reported separately | 100% / ≥ 2.8 categories / 100% |
| `OfferPickBySlot` | picks by slot ÷ offers taken, from a declared bot policy (highest `ItemVerdict` gain, ties by option index) and from playtest telemetry | no slot > 60%, Wayward ≥ 10% |
| `BossCardGearFallbackRate` by class | fallback options ÷ options | read beside RequiresHitRate; gap ≤ 10 points |
| `RecordRepeatRate`, `OncePerRunGeneratedTwice` | records generated twice at tier ≤ the earlier one ÷ runs, split by tier (R exempt until its switch) / count | 0 at C and U (R reported until the switch) / 0 |
| `DeadRareRepeatRate` by solo class | unusable Rare records generated twice ÷ runs | 0 (playtest-blocking) |
| `RelaxationStepRate`, `SubstitutedVPByClass` | steps by rung and socket kind ÷ draws | rung 1 ≤ 2%; rung 4 ≈ 0 for gear |
| `StepDownPerRun` | rung-3 and rung-4 events, offer terminal steps (2)–(3) and fit-premium coin fallbacks, per run | ≤ 1 (playtest-blocking) |
| `LootMixByCategory`, `FamilyShareByLane` | field VP by group ÷ field VP | ±5 points of LE 6.2 |
| `RollConcentration`, `RollFitRate`, `LinePowerDelta` | — | TK ceilings / ≥ 60% / within band |
| `ShrugShare`, `UpgradeShare`, `SidegradeShare` | gear notables = or ▽ for every member ÷ gear notables, at presentation | ≤ 25% (≤ 35% F4–5) / F1 ≥ 60% / — |
| `LateFloorGearShare` | gear notable VP ÷ field notable VP on F4–F5 | ≥ 50% (playtest-blocking) |
| `ChaseFoundRate`, `ChaseVarietyByClass` | runs generating ≥ 1 chase record ÷ runs / the maximum, over VR and `chase`-flagged Rare records, of the share of runs that generate that record | ≥ 50% / ≤ 35% |
| `BuildDiversity` | distinct sorted sets of equipped familyIds at the final boss, per class per 100 seeds | ≥ 15 (playtest-blocking) |
| `ScionUpgradeRate`, `TrinketScionRate` | — | ≥ 1 per run in ≥ 40% of runs / revert test |
| `SignatureUsableRate`, `SignatureBaseShadow` | — | ≥ 70% per boss (M3 ≥ 90%) / reported |
| `EliteShowsInUseRate`, `EliteKpcDelta`, `CarrierRemapRate`, `KinSwapRate` | — | ≥ 70% / ≤ 10% / ≤ 10% / — |
| `DormantAtChoice`, `DormantShareAtSummary`, `ArcanaChangedChoice` | — | 0 (IR-9, agreed Q3) / reported (ITEMS 21) |
| `AnswerHereShare` | items with the mark at presentation ÷ items presented, per theme | ≥ 30% per theme; a miss files 5.1's content request |
| `ScrollBossShareByClass` | — | below the CN 19.3 25% alarm |
| `ViaticumAfterLeverFallbackRate` | Viaticum options falling to gear after the tier-4 lever is spent ÷ Viaticum options | reported |
| `CompanionTrinketEmpty` | — | ≤ 25% (CMP-30) |
| Re-read in 7a | SellShareOfCoin, K1/K2, CampCoverage, LateFindRelevance, HeroUpgradeDrought | — |

### 9.4 Content requests (to category owners; sized by V14 and the reports)

| Request | Count *(illustr.)* | Owner | Wave |
|---|---|---|---|
| Rare weapon and focus records (as Scion tops) | ≥ 3 per lane × 7 lanes (minimum 2 per lane) | W, ITEMS | M2 priority |
| Rare body armour and shield records | ≥ 3 per lane × 4 lanes | AR | M2 priority |
| Value-2 majors for blades, axes, polearms, spears (theme draft R-1) | about 4–6 | W | M2 |
| VR gear, build-around verbs | ≥ 2 per lane group (heavy, fine/ranged, arcane, sacred) | category owners | M2 (LE asked 2–3) |
| Rare neck families | 2–3 | TK | M2 |
| Herald tier-2 and tier-3 scrolls; more tier-2 scrolls | +2, +1 | CN | M2 |
| Signature second forms for specialist chassis (5.2) | 5 (none on F5; a cut boss's not owed; boss rooms design 4.11.1, change BR-27) | segment 9 | M2 priority |
| Answer records in common lanes at C and U, per theme whose `AnswerHereShare` misses | as the report names | category owners | M2 (on a miss) |
| Lexicon epithets (suffixes) for every rollable entry; `carrierSafe`, `synergy[]`, `verbTag` tags | about 40 | owners | M2 |
| Scion lines | about 1 per gear lane | owners | M2 |
| Martial tactical boss-card items | 2–3 | CN | M3 (from Later) |
| Element amulets, Wader's Boots, Stillmind Pendant, Holy Water, leader-emblem charms | as TK and CN Later lists | TK, CN | M3–Later |
| Rare bomb shapes, arena-keyed Viaticum tools | — | CN | Later (LE 6.13) |
| Reveries (about 12 per theme, part-tagged) and provenance captions | about 110 | writing | M2 |

---

## 10. Staging by wave

| Wave | 7b content |
|---|---|
| **M1** (F1, Vanguard slice) | `lootLane` on F1 records; `UsableBy` and `LanesOf`; filter-first with padding, locked before golden seeds; the F1 Due (approved); signatures kept out of floor keys and memory (`bossContent[]`); Old Ragged guards Bellwarden's Mail in its den's signature cache, with the "Guards: Bellwarden's Mail" plate line (agreed, Q5); `lineRoll` reserved and inert; V14 (a), (b), (d), (h), (j) and the draw-count test |
| **M2** | Weight split; usability floor split; rolled lines, lexicon, smart roll, `LinePowerDelta`; Scions and trinket Scions; Scion-line memory for C/U and `oncePerRun`; relaxation and tier-stable precedence; offer slots, per-option fallback, hero guarantee; Plain Gifts (agreed, Q3); Due additions; Wax-Key sibling; `showsInUse`, elite preference, KinSwap, the carried-use floater, roster flags; signatures per tier, second forms for specialist chassis and wherever usable < 70%; `theme.loot`, "Answers here", Patina text and Kennel and Gaol trims, stamps, provenance, Reveries; `curseGate` reserved; IR-16 guards; full V14 and reports; content requests as priority; 7a re-runs |
| **M3** | Wager and Twin Coffer slots; second signature forms for every boss; worn/lodged signature option; full Patina art; heirloom and unlock hooks; Scion-line memory at Rare (flipped the first build V14 (c) passes at Rare for every solo class, possibly M2, no later than the end of M3; 4.7 item 4); item Deeds if CMP's ship; codex badge; Re-engraving hook; martial tactical boss-card items; Element Echo **only** if the client adopts it at its M3 decision (agreed, Q4) |
| **M4** | Tuning only: `requiresTargetPermille`, `anyCompanion`, Scion pull, label drop; the Cathedral table once its theme exists |
| **Later** | Cursed twins through the reserved gate; trader channel (same pipeline); 2-piece Pairs stay parked |

---

## 11. What 7b owns, and hooks handed on

| Owner | What |
|---|---|
| **7b** | The pipeline; lanes and usability; weights within LE 6.2's frame; rolled lines and lexicon; Scions; memory and `oncePerRun`; relaxation; offer slots; Due item choice; Wax-Key sibling; carried pre-filter, `showsInUse`, KinSwap; signature rules; identification numbers and verdicts; `theme.loot`; the `lineRoll` and `curseGate` blocks; V14; 7b reports; content requests |
| **7a (approved)** | Budgets, decks, tiers, ledgers, coin, the Risk Ledger, the Due's target and card, category weights. Hook: re-run coin pegs and category mix after IR-2 and IR-3 |
| **7c** (taken up by the loot sources design, approved 2026-11-22) | Sources and sockets; container kinds and art; the provisional armed roster per carried socket; bait caches, boxed lurkers, Twin Coffer placement; the signature cache (4.18); Law-flavoured containers; Proprietor tags from `theme.loot.proprietors[]` and the Bedside Stand (loot sources design 4.5, 4.10, 11; changes LP-13, LP-14) |
| **Segment 9 (bosses)** | Signature records and forms (budget-complete lines, the echo, second forms), `signatureAbilityTag`, kits, armed status (carriage derives from them), Old Ragged's surcoat, Memento epitaphs, the Headsman's Greataxe home (taken up by the boss rooms design 4.11.1; change BR-27) |
| **Segment 10 (UI)** (answered by the exploration and information design, approved 2026-11-23: every hook placed in its UI surface catalogue, its 5) | Card budget merged with ITEMS 8.2 and TK 15, glyphs and the footer badge, marks, the line-tag shapes, Whence pane, reveal chime, offer layout and frame headers, the carried-use floater, the "Guards:" plate line and the signature cache decal, stamps |
| **Segment 11 (seeds)** (taken up by the seeds and validation design, `2026-11-23-seeds-validation-design.md`, segments 11–12, approved 2026-11-23: its 4.3 registry rows, 4.4 GS10, 4.11 and 8.1; changes SV-1, SV-2, SV-5, SV-18; its two requests to this owner pending, 4.17, 9.3) | The 4.17 draw table, keying and frozen generation order; the filter-first adoption before golden seeds |
| **Camp and hub owner** | Re-engraving (hook), shop comparisons reuse verdicts |
| **Later systems** | Curses (the gate and twins), trader, Pairs, the malign hint |

---

## 12. Binding requirements and how they are met

| Requirement (brief and LE 6.13) | How it is met |
|---|---|
| Pipeline category → family → base → variant → properties | One draw factorised by 4.2; Scions as variants; ≤ 1 rolled line (4.1, 4.5, 4.6) |
| Theme loot tables, how much theme bias | `theme.loot` per theme (5); 0 rules bias; Echo held (4.4; agreed, Q4) |
| Quality and random properties | Tier = quality; ≤ 1 rolled verb line; no ranges (4.6) |
| Smart loot: strength, companions, hard vs soft | 4.3 |
| Never unusable in offers and the Viaticum | Hard floor, per-option fallback, hero guarantee (4.8; agreed, Q1) |
| Duplicates, caps, uniqueness | 4.7 (agreed, Q2) |
| Signatures and bosses | 4.13 (agreed, Q5 for unarmed bosses) |
| Dreamed options distinct and meaningful | Slots, distinct families, new-line Wayward, identified (4.8, 4.14) |
| Wax-Key and stepped identities | 4.11 |
| Carried loot; elite gear-only | 4.12 |
| Identification and reading | 4.14 (agreed, Q3), 6.1 |
| Curse, heirloom, unlock hooks | 4.15, 4.16 |
| Determinism with fixed counts | 4.17 |
| Data, V14, reports | 9 |
| LE 6.13 (1) record per card, re-weight | 4.1, 4.2 |
| (2) Due filter | 4.9 |
| (3) fit filter | 4.1 step 4, 4.3 |
| (4) triples, fourth, Twin, fallbacks | 4.8, 4.10 |
| (5) Wax-Key | 4.11 |
| (6) camp N+1, never VR | 4.1, 4.10 |
| (7) weight composition | 4.2 |
| (8) ≥ 3 per lane per tier | V14 (c) warning + requests |
| (9) tier-stable substitution | 4.7 item 8 |
| (10) phial swap | 4.1 (ghost, outside memory; a boss-channel phial takes the boss card's option 2, CN 14.7) |
| (11) carrier-legal and elite subsets | 4.12 |
| (12) content requests | 9.4 |
| Readability charter (RC1, RC3, RC4, RC7, RC9; ITEMS 8.2; ABL 11.4; ENM 15.8) | 6 |
| LootDoorInvariant | Theme never read by rules; memory by generation; signatures feed no exclusion; display hash only (4.4, 4.7, 4.17) |

---

## 13. Changes to approved documents

Applied on 2026-11-22, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; **the client's approval of 2026-11-22 covers them** (with Q1–Q5 where a row names one). **(New lead's call:)** as in the LE, TL and EP precedents, the Flag column also marks rows that only *read* agreed, decided or settled text without changing it, so that every touch of such text is visible; this adds flags to IR-1, IR-14 and IR-15 and names LE Q1 on IR-8 (the Viaticum fallback wording in LE 6.11 item 5 sits inside LE's agreed Q1). **After the check (lead's call)** the Flag and section columns were completed on IR-3, IR-4, IR-6, IR-9, IR-10, IR-11, IR-12, IR-13, IR-14 and IR-15. Told-plainly rows were told in 14.1 and confirmed by the client on 2026-11-22. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes):**
- LE 6.9 item 2c's "gear" includes trinkets, so a per-option fallback may hold a trinket; it counts first against `trinketRarePerFloorMax`, as a LootPlan identity (4.8; the order change is in IR-14);
- LE 6.9 item 3's "only if no fit lane exists" reads as "no usable record in the target's lanes at T"; the socket then keeps an ordinary hard-usable identity and adds the approved 20 fallback coin (F5: sundries) (4.7 item 8);
- ENM 20.1's shared-base check covers deck notables only; a signature is exempt (4.7 item 7, filed under IR-6 with ⚑ RS 6.4);
- CN 14.7's boss channel for the phial (agreed, CN Q2) is kept: a boss-channel phial fills the F3–F4 boss card's option 2 (4.1), the case LE 6.13 left open;
- ENM 15.3 step 2's "base" reads as **base ?? family** for trinkets, scrolls, wands and notable flasks, for rerolls and for ENM 20.1 (4.7 item 2);
- ENM 15.3 step 2's "reroll, each excluding…" is met by filter-first with padding, the same distribution at a fixed count (4.1);
- AR 14.3's and RS 6.4's theme read for Resistant stays on authored records; a rolled Resistant line is theme-blind (4.4);
- RS 6.4's LootDoorInvariant "same identities" covers deck draws; signatures are boss content and feed no exclusion of any deck draw (IX21, 4.7 item 7; the RS owner confirms);
- ENM 15.2's unarmed-elite "Guards: cache" precedent extends to unarmed bosses' signatures (4.13; agreed, Q5);
- CN Q1's out-of-class scroll read is usable with a shown difficulty (`arcanaRead`) on field cards, the Wayward slot and the Viaticum, never on the Yours slot, the Due or fit premiums (4.3);
- ITEMS 14's heirloom sits outside pools and Scion-line memory and counts as owned for the Due and verdicts (4.15);
- LE 6.12's unlock normalisation is met by the 4.2 split (4.16).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| **IR-1** | ITEMS 12.3 (field `lootLane` added); LE 6.2, 6.6, 6.9, 6.12 and V13s (n) (made concrete); **ENM 15.3 step 2 (agreed; reading)** | `lootLane` field, closed `loot.lanes`, `LanesOf`, `onParty` (4.3) | ⚑ **Agreed** (ENM 15.3 step 2, read) | Lead's call; ITEMS owner applies; class, kit and CMP owners finalise lanes | M1 data / M2 |
| **IR-2** | **ENM 15.3 step 2 (agreed)**; ENM 10.5 step 3 ("only input" sentence); LE 6.2; **LE 6.10 item 7 (settled)** | `UsableBy` (`inClass` / `arcanaRead`, one socket-kind table; a Strength shortfall unusable on the Due and fit filters as LE 6.6's deal reads it); hard floor on chosen (camp entries included), guaranteed and VR draws; soft 100‰ on C/U/R field cards; targeted sockets bias against the target (4.3) | ⚑ **Agreed** (ENM 15.3 step 2); **Q1** | **Client (Q1, agreed 2026-11-22)**, with this document; enemies owner applies | M2 |
| IR-3 | LE 6.2 formula (LE-17); ENM 15.3 step 2 (weights paragraph, the per-lane normaliser restated inside the agreed step) | Lane > family > record split, divisors on the static legal set; fine categories and LE groups; fixed factor order with a FloorDiv per factor (4.2) | ⚑ **Agreed** (ENM 15.3 step 2, weights paragraph) | Lead's call | M2 |
| **IR-4** | **ITEMS 11 item 3 (agreed)**; ITEMS 19.1 rule 14; ITEMS 12.4, 12.5 (reading); ITEMS 21 (the hidden-share lever becomes a content lever: raise the rollable share of R+ records); TK rule 28; W 18; AR 14 | ≤ 1 rolled line; fixed lines obvious; rolled-only dormancy; curated family tables; rule 28 for all categories (4.6) | ⚑ **Agreed** (ITEMS 11) | Lead's call; TK, W, AR owners confirm | M2 |
| **IR-5** | ITEMS 5.1, **12.2 `familyCapPerRun` (agreed family caps)**; TK 13.8 | Scions; trinket Scion exception to family cap 1 (≤ 1 owned) (4.5) | ⚑ **Agreed** (family caps) | Lead's call; TK owner applies (request) | M2 |
| **IR-6** | **ENM 15.3 step 2 (agreed)**; ENM 20.1 (reading: signatures exempt from the shared-base check); ITEMS 12.3 flags; LE-4 (6.3, narrowed for memory); LE 6.12; **LE 6.10 item 7 (settled; widened input: run-wide `LootLedger.generated[]`)**; **RS 6.4 (settled; read)** | Scion-line-once from generation outputs, every gear identity real or dreamed (the 4.7 event table); true Scion lines; dead-Rare memory before the Rare switch; `oncePerRun` flag, explicit; dedupe key base ?? family; gear-only dreamed floor keys; `oncePerRun` barred from the fourth and dual-identity caches; signatures write no floor key, no memory and no `generated[]` row (`bossContent[]`), and are exempt from the run-wide owned-base reroll and cap-exempt when taken (4.7) | ⚑ **Agreed** (ENM 15.3 step 2); ⚑ **Settled** (LE 6.10 item 7, RS 6.4); **Q2** | **Client (Q2, agreed 2026-11-22)**, with this document; enemies and items owners apply | M2 (Rare staged, 4.7 item 4) |
| IR-7 | ENM 15.3 step 2 (termination); LE 6.3 M2 VR rule; LE 6.13 item 9; LE 6.9 item 3 (read, kept: the fit premium's 20 fallback coin) | Relaxation: the socket's target relaxations first, then four rungs per a socket × rung matrix (the same-floor-key rung withdrawn); tier-stable before supply/tonic; no silent tier change (4.7 item 8) | — | Lead's call; enemies owner applies | M2 |
| **IR-8** | LE 6.3 (LE-3, LE-4); **LE 6.11 item 5 (fallback wording; agreed, LE Q1)**; ENM 17.5; CN 10 boss-card row; **CN 14.7 (agreed, CN Q2; read: the boss channel kept)**; LE 6.13 item 10 (the boss-channel phial case filled) | Offer slots with option indices and a construction order; the Theirs target rule; distinct families and effect tags; ≤ 1 damage on the boss card and the Wager; per-option usable-gear fallback and the offer terminal ladder; hero guarantee (boss card and Viaticum); trinkets in the fallback; the boss-channel phial in option 2 (CN 14.7 kept); CN 10 martial note and M3 pool addition (4.1, 4.8, 4.10) | ⚑ **Agreed** (LE Q1, wording only; LE-3 client-approved; CN 14.7, read); **Q1** | **Client (Q1, agreed 2026-11-22)**, with this document; CN owner applies (pool addition a request) | M2 (Wager, Twin M3) |
| **IR-9** | **ITEMS 11 items 3 and 5 (agreed, client decision 4)**; TK 15; ITEMS 8.2 card layout (the "?" pip replaced by the hidden-line Arcana readout; "fit verdict" renamed the fit line; the footer order) | Plain Gifts: the Due, offer options (Wager and Twin Coffer included), the fourth and the Viaticum generated `known` (4.14); the card budget merged with ITEMS 8.2 and TK 15 (6.1) | ⚑ **Agreed**; **Q3** | **Client (Q3, agreed 2026-11-22)**, with this document; items owner applies | M2 |
| IR-10 | **LE 6.6 (LE-13; settled)** | Due: two-hander and pair test; camp sales count as dealt; bias against the target alone; held lever `dueSidegradeFromFloor` (default 4) (4.9) | ⚑ **Settled** (LE 6.6); firing the lever needs the client and DH owner | Lead's call; DH owner confirms | M1 (F1) / M2 |
| **IR-11** | **LE 6.9 item 4 (LE-11, agreed LE Q2 wording)**; LE 6.15; TL 6.11 (consequence-lock contents-hint bullet) | Wax-Key sibling (same fine category, different family, prefer a different lane); the lock card names each identity's fine-category noun, tier step and fit step (4.11) | ⚑ **Agreed** (LE Q2) | Lead's call; TL owner applies; the client sees the wording with this document | M2 |
| **IR-12** | LE 6.2; LE 6.9 items 2 and 7 (LE-10, LE-18); ENM 15.2 elite row; ENM 15.3 step 4 (carrier assignment, overridden by KinSwap); ENM 7.2; **RS 8.7, 8.9 rosters (agreed RS Q4 catalogue)** | `showsInUse` ×2 on carried; elite preference; `EliteKpcDelta` lever; KinSwap LootBind remap (scored, one pass); the carried-use floater; Spine-Binder `armed`, Butler-at-Arms `promotable` (4.12) | ⚑ **Agreed** (RS Q4 catalogue) | Lead's call; RS owner and segment 9 apply the roster flags (request) | M2 |
| **IR-13** | **ENM 15.2 boss row (agreed, LE Q1 for F5)**; ENM 17.5; LE 6.3 (LE-3; the pending VR-cap exemption reading withdrawn); **LE 10.2 V13s (c)** ("signatures exempt, pending ITEMS"); **LE 18.2** (the ITEMS-refusal fallback replaced); LE 6.10 item 2 (read: the boss-channel dummy draw); **LE 6.11 item 4 and ITEMS 12.5's F5 sentence (agreed, LE Q1: "carries and uses its signature")**; ITEMS 12.5 (I-9); **AR 5.5 (agreed, armour Q1: signatures on weapons and body armour)** | Signature forms per tier (top tier from the drawn variant); boss record authoritative, carriage derived per form; ≤ 3 lines with a value-1 echo of the boss's fight or kit (M1 grandfathered); universal chassis (M2+); draw-free second forms, required for specialist chassis; foci and shields; unarmed bosses guard in a signature cache (Old Ragged guards Bellwarden's Mail; its coat a surcoat); the "Guards: <signature name>" plate line; VR signature consumes the VR slot (4.13) | ⚑ **Agreed** (ENM 15.2; AR 5.5, armour Q1; LE 6.11 item 4 and ITEMS 12.5 F5, LE Q1); **Q5** | **Client (Q5, agreed 2026-11-22)**, with this document; segment 9; ITEMS owner (VR slot); AR owner (foci and shields); RS owner (IX21 reading; Old Ragged's surcoat) | M1 / M2 / M3 |
| **IR-14** | LE 6.10 item 1 table (LE-14); ENM 10.5 step 3, 15.3 step 5 ("properties fixed at step 3"); **AR 14.3, RS 6.4 (settled; reading)**; TK-27 and ITEMS 19.2 (Rare-trinket channel order: LootPlan identities count first) | `lineRoll` (4 draws: line, alternate, element, charges), `curseGate` (2 per floor), fallback entries 3 → 5 (the fourth and the hero guarantee), keying ×16; d2 hosts rolled element lines on every identity, AR 14.3's parent draw only authored Resistant; filter-first adopted; LootPlan identities first in the Rare-trinket order (4.1, 4.17) | ⚑ **Settled** (RS 6.4, read) | Lead's call; armour and RS owners confirm; TK owner confirms the order (request); segment 11 applies | M1 reserve / M2 |
| **IR-15** | RS 8.0 theme record (additive); reads **RS 6.4 (settled)**, **RS 7.4 (agreed, RS Q3)**, 9.3, RC1, RC3, RC4, RC5, RC7, RC9; ITEMS 12.3 `display`; AR 14.3 (scope kept to authored) | `theme.loot` display record; Patina (materials and one charm, model-only); pictogram stamps; provenance; part-tagged Reveries; "Answers here"; the hue check moved to the RS 7.4 art gates; rolled Resistant theme-blind (4.4, 5, 6) | ⚑ **Settled** (RS 6.4, read); ⚑ **Agreed** (RS 7.4, read); **Q4** | **Client (Q4, agreed 2026-11-22)**, with this document; RS owner applies; art; writing | M2 / M3 |
| IR-16 | LE 6.2 (beside the F5 cap); CN 18.1 (lever default) | `rareTacticalFieldMaxPerFloor` on F3–F4; tier-4 scroll lever defaults to 1 while the usability floor ships (4.7 item 9) | — | Lead's call; CN owner applies | M2 |
| IR-17 | LE 6.8; CMP-30 | `anyCompanion` lever gains a party-size key (≥ 3) (4.3) | — | Lead's call; CMP owner confirms | M2 lever |
| IR-18 | LE 6.2 composition; LE 6.3 camp grading | Scion pull lever (×2 next Scion of an owned family on F3+ field and camp), null (4.14) | — | Lead's call | M2 lever |
| **IR-19** | **RS 6.4; RS 9.1; ENM 15.3; TK R-23; LE 6.10 item 7 (settled)** | Element Echo option, default off, false in release builds until the client adopts it (4.4) | ⚑ **Settled**; **Q4** | **Client (Q4, agreed 2026-11-22: held for an M3 decision)**; RS owner | M3 option |
| IR-20 | ITEMS 13; LE 8.5 (LE-22); ITEMS 19.1 rule 9; ENM 15.3 cost add | Cursed twins; per-floor gate; widened bars; T+1 carried cost add (4.15) | — | Lead's call; curse designer | Later (draws M2) |
| IR-21 | RS 11.2 validator table; LE 10.2 | V14 `ItemRolls` (9.2) | — | Lead's call; RS owner applies | M1 / M2 |
| IR-22 | LE 10.3 (additions) | 7b reports (9.3) | — | Lead's call | M2 |
| IR-23 | CN record fields; LE 6.11 item 5 (alias) | `effectTag` on scrolls, wands and notable flasks (`use.damage` alias) (4.10) | — | Lead's call; CN owner applies (request) | M2 |

Waves: IR-1 (F1 data), IR-10 (the F1 Due, as approved), IR-13 (Old Ragged guards Bellwarden's Mail; V14 (h)), IR-14 (`lineRoll` reserved, filter-first) and IR-21 (V14 (a), (b), (d), (h), (j) and the draw-count test) in M1; IR-1 to IR-18 and IR-21 to IR-23 in M2; IR-13 second forms for every boss and the worn or lodged option, and the IR-19 decision, in M3; IR-20 Later (its draws reserved from M2).

---

## 14. Open items for the client

None. Approved by the client on 2026-11-22.

The client approved this document as a whole on 2026-11-22, including every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the lead's calls after the check in 14.1), the told-plainly items (including "Told plainly after the check" in 14.1: among them the VR-signature slot reversal of LE-3's pending reading, passed-up offer options writing Scion-line memory, and the carried-use name floater) and the readings recorded in 13, and its approval covers the flagged rows of 13. The five questions it raised were answered on 2026-11-22 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. The check (builder, consistency, play and art/UX lenses) raised nothing that needed a new client question: every fix sits inside the five answers, keeps an approved rule, or is flagged against the text it changes. The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them, and the naming pass remain future items.

### 14.1 This document

**The five answers (agreed, 2026-11-22), each the recommended option:**

- **Q1, usability (IR-2, IR-8):** option A. A hard usability floor on everything chosen or guaranteed (the Due, fit premiums, the arms floor, every offer option and the fourth, the Viaticum, both Wax-Key identities, VR cards); a soft 100‰ on C/U/R field cards (4.3, 4.8). Rejected: B, a hard floor everywhere; C, as approved, soft weights only.
- **Q2, new items (IR-6):** option A. A record already generated never returns at its tier or below (Scion-line memory); `oncePerRun` records are generated at most once, dreamed options included; memory counts generation (4.7). Rejected: B, `oncePerRun` items only; C, as approved.
- **Q3, Plain Gifts (IR-9):** option A. Offers, the fourth option, the Viaticum and the Dream's Due are shown identified; field finds keep ITEMS 11 (4.14). Rejected: B, the dormant line's kind only; C, as approved.
- **Q4, theme in loot (IR-15, IR-19):** option A. 0 rules bias; presentation, hands and signatures (Patina, stamp, provenance, Reverie, "Answers here", carried loot, KinSwap); Element Echo held for an M3 decision, default off (4.4, 5). Rejected: B, Echo in M3 now; C, theme-keyed weights.
- **Q5, unarmed bosses (IR-13):** option A. An unarmed boss guards its signature in its lair; its kit is unchanged (M1: Old Ragged guards Bellwarden's Mail); worn or lodged signatures are an M3 option (4.13). Rejected: B, worn or lodged signatures in M2.

**New lead's calls (made while writing; confirmed by the client on 2026-11-22, with this document):**
1. **Document shape:** the proposal's section numbers 0–12 are kept so every cross-reference holds; intent, goals and anti-goals join 0 as 0.4; the closing parts follow as 13 (changes), 14 (open items), 15 (risks), 16 (left to the builders) and 17 (depends on).
2. **Toy failure ids renamed IT1–IT15** (the drafts' F1–F15 read as floors; `IT[0-9]+` has 0 hits).
3. **"Two Ways" dropped** as a mechanic name (an ordinary phrase with 6 hits); the mechanic is the **Wax-Key sibling** (4.11, 7).
4. **`ItemVerdict(member, instance)`** names the comparison query ("verdict" is an ordinary word in 19 approved documents; `ItemVerdict` has 0 hits) (4.14).
5. **Client keys marked not tuning** (9.1): `loot.usability.hardOn` (Q1); `loot.memory.oncePerRunBy` and Scion-line-once at C and U (Q2); a new key **`loot.plainGifts = true`** (Q3); `loot.elementEcho` (Q4); a new key **`loot.signatureCarriage`** (beside LE's `loot.signatureTierRule`) for the guarded carriage and plate line (Q5). The soft 100‰ and every other number stay tuning.
6. **Element Echo in release builds:** the switch ships `false` in every release build until the client adopts Echo; V14 (j) fails a release configuration with it on; playtest builds may turn it on as an experiment (4.4).
7. **When Rare Scion-line memory starts:** `loot.memory.scionLineOnce.R` flips the first build in which V14 (c) passes at Rare for every solo class, no later than the end of M3; until then Rare repeats are reported in `RecordRepeatRate` (4.7 item 4, 10).
8. **Where a guarded signature sits:** one signature cache (renamed from "lair cache" after the check) in the boss's lair, placed by 7c, never locked or trapped, holding nothing else, outside the deck, Ledger A, the socket ranks and the locked-or-trapped quota; it opens once the boss is resolved; the boss plate reads "Guards: <signature name>"; the item is shown identified (4.13, 4.18, 4.19).
9. **An unarmed final boss** (none in M2) keeps the Memento rule: its guarded signature drops as the 0-VP Memento at victory (4.13).
10. **The IR-16 guard's mechanism:** once `rareTacticalFieldMaxPerFloor` is reached on F3 or F4, Rare tactical records take weight 0 in field draws for the rest of that floor, enforced in channel order as the F5 cap is (4.7 item 9).
11. **Plain Gifts scope made explicit:** the arms floor and stepped identities are field cards and keep ITEMS 11, like fit premiums and Wax-Key identities; `known` is set draw-free in step 9 (4.14).
12. **V14 (h) checks carriage:** `carried` only on an `armed` boss and a form in its issue gear, else `guarded` (9.2; derived per form after the check).
13. **Flags in 13** also mark reads of agreed, decided or settled text (LE, TL and EP precedent): IR-1, IR-14 and IR-15 flagged; IR-8 names LE Q1 (the Viaticum fallback wording).
14. **Owner-refusal fallbacks** in 14.2, each stated so a refusal never blocks M2.

**Lead's calls after the check (made when the written document was checked; none overrides a client answer; confirmed by the client on 2026-11-22, with this document):**
1. **Relaxation order:** a socket first relaxes its own target preferences in their listed order, then the IR-7 rungs, per a socket × rung matrix; the "lift the same-floor key" rung is withdrawn (ENM 20.1 keys are never lifted), so the ladder has four rungs; offer options use their own terminal ladder (memory → a usable tool at T → one tier lower, logged) and never reach supply or tonic (4.1, 4.7 item 8, 4.8).
2. **Signatures feed no exclusion:** no pre-registered floor key, no Scion-line memory, no `scionOf`, no `generated[]` row (`bossContent[]` instead); ENM 20.1's shared-base check covers deck notables only; signatures are cap-exempt when taken (4.7 items 1 and 7).
3. **Weights in fixed point:** a fixed factor order with a FloorDiv after each factor, SCALE 2^20, every socket term named with its key (4.2).
4. **Two category levels:** fine categories for distinctness, the Wax-Key sibling, the Due slot and the camp pair; the four LE 6.2 groups for `categoryWeight` and the lane divisor; pieces sit in the trinket group (4.2).
5. **Offer construction:** option indices 0–3, slots resolved one at a time, the Theirs target (largest deficit, then lowest memberId), "prefer ▲" as a filter rung, fallback bias 4 on the boss card and 3 on the Viaticum, the hero guarantee on its own fallback entry 4 with keying ×16, the hero guarantee on the Viaticum too (4.8, 4.10, 4.17).
6. **Fallback trinkets count first** against `trinketRarePerFloorMax`, like every LootPlan identity; the TK-27 order change is filed under IR-14 (4.8, 4.17).
7. **Memory event table:** every gear and trinket identity, real or dreamed, writes Scion-line memory (offer options taken or not, the fourth, both dual identities, fallbacks, the hero redraw), as Q2's "memory counts generation" reads; ghosts and signatures write nothing (4.7).
8. **True Scion lines:** at most one Scion child per record (4.7 item 4, V14 (e)).
9. **Dead Rare repeats:** before the Rare switch, Scion-line-once still applies at Rare to records no member can use; `DeadRareRepeatRate` blocks playtest sign-off (4.7 item 4).
10. **One socket-kind usability table,** with camp entries, the Wager and the Twin Coffer hard (chosen, Q1) and the Twin Coffer and Wager generated identified as offers (Q3) (4.3, 4.10, 4.14).
11. **Targeted bias:** the Due, fit premiums and the Yours and Theirs slots weigh `onParty` against their target alone (4.3, 4.9).
12. **A Strength shortfall is unusable on the Due and fit filters,** matching LE 6.6's fixed deal (4.3).
13. **The fit premium keeps LE 6.9 item 3:** no fit record means an ordinary identity plus the approved 20 fallback coin (4.7 item 8).
14. **KinSwap made exact:** a score, one pass over pairs by socketId, strict improvement, legality kept (4.12).
15. **`lineRoll` d1 is an alternate line** pre-picked at roll time and used draw-free at LootBind; d2 picks rolled element lines on every identity, parent or lootPlan; element picks use damped integer weights (4.6, 4.17).
16. **Signature data and form choice:** the boss record is authoritative; carriage is derived per form; `topTier` is the drawn variant's highest tier capped by authored forms; the chosen form is the one every hover and plate names (4.13).
17. **The echo rule loosened to "the boss's fight or kit",** checked as an M2+ warning; Bellwarden's Mail grandfathered; the universal-chassis clause applies from M2 (the M1 Kennel subset exempt) (4.13, V14 (h)).
18. **Specialist signatures get second forms requested now** (5.2 table, 9.4) (4.13).
19. **The boss-channel phial** (CN 14.7, kept) fills the F3–F4 boss card's option 2, with a ghost identity (4.1).
20. **The VR-slot rule stays as the client approved it,** now flagged as withdrawing LE-3's pending exemption (LE 6.3, V13s (c), LE 18.2); the refusal fallback is LE-3 as approved (4.13, 14.2).
21. **Wager without a Wayward slot:** Yours plus two Theirs fit-premium slots (4.10).
22. **Lock-card wording** uses each identity's fine-category noun, with a fit-step form (4.11).
23. **V14 split** into static checks and M2 simulator gates; (c) restated per solo class; reports given definitions (9.2, 9.3).
24. **Card budget merged with ITEMS 8.2 and TK 15,** filed under IR-9; verdict glyphs in a footer badge; ▲ needs a non-negative gain; ✕ only where equipping is refused; consumables show only ✕ or nothing; a mark table and a line-tag shape table (6.1, 4.14).
25. **Presentation:** the carried-use name **floater** replaces the "name banner" (first use per carrier per fight; never the RC7 banner); the Patina notable accent is cut (the ENM glint is the one marker); the signature cache shows the item's icon on a plate decal; Old Ragged wears a torn surcoat that is never loot; suffix-only epithets and Scion rank words; part-tagged Reveries; pictogram stamps (striped shirt, inkpot, ribcage, rocking horse replace cell number, shelf-mark, house-key bow, button); Patina is materials plus one charm, model-only; signatures keep their family silhouette; the hue check moves to the RS 7.4 art gates; the "reveal chime" replaces "sting" (4.6, 4.12, 4.13, 5, 6).
26. **`AnswerHereShare` misses file a content request** for that theme (5.1, 9.4).
27. **Camp entries** use f = N+1 for tier, floors, `categoryWeight` and `scrollFloorCap`, floor N's keys, and sit outside both floors' field counters (4.1).
28. **`curseGate` draw 2** orders candidates by frozen generation order, then entryIndex (4.15).

**Told plainly (gathered from where they stand; confirmed by the client on 2026-11-22):**
- The brief's "random affixes with ranges" is not delivered: ≤ 1 rolled verb line, no ranges, no greater affixes, no attunement (0.1, 4.6).
- The brief's theme loot tables are delivered as presentation, hands and signatures; the theme has no rules weight, and a ×1.5 tilt would add only +0.02 kin items per floor (0.1, 4.4).
- Solo on-build is about 65–76%, not the 83–100% the drafts claimed (0.1, 4.3).
- Filter-first gives the same distribution as the approved reroll loop, not the same identity per seed; it must be locked before M1 golden seeds (4.1).
- An `arcanaRead` scroll is usable but not "for you" (4.3).
- The CMP-30 `anyCompanion` lever is expected to fire at party 3–4 (4.3).
- A playtest build with Element Echo on runs outside the settled rule, as an experiment only (4.4).
- The agreed "never returns" holds for C and U from M2 and for Rare from its switch; a logged relaxation rung can still lift Scion-line memory, never `oncePerRun` (4.7).
- The F4–F5 sidegrade's property-id test is already approved; the toy's F2 and F5 waiver figures came from an off-spec model and must be re-run (4.9).
- Martial boss cards and the Viaticum resolve mostly to gear for a solo Vanguard or a Herald without Arcana; "the boss gives spells" holds only for readers (4.8).
- The draft's "Wolfcleaver vs Headsman's" base clash never arises in M1; the real clash is chain mail (4.13).
- The Due is a floor-N container find, so it may carry "Answers here" (5.1).
- Reordering the frozen generation order changes identities (4.17).
- The same seed can *look* different across doors; identities never differ (4.17).
- M2 stays thin until Rare and VR gear content lands; V14 warns, it does not author (0.3, 9.2).
- The research was wrong that `unique` had no meaning; CN 14.1 defines it (IX20).
- ITEMS 19.3's 70% late-finds-equipped target is unlikely on M2 (IX16).
- Identification stakes are already low under free camp identify; Plain Gifts removes Arcana's job only from choices (IX19).
- Model C's soft floor leaked 22–27% dead gear against its claimed 4–9% (3.3).
- Every toy number carries the five caveats of 8.
- A critic's claim that "Wind-Up" has 0 hits is wrong (hits in 5 documents); the name Wind-Down Hammer stands (A.4 IK13).

**Told plainly after the check (confirmed by the client on 2026-11-22):**
- The written draft let a signature (which follows the door's boss) block its base for the floor's deck draws, so a door swap could change deck identities against RS 6.4. Signatures now feed no exclusion; the cost is that a field chain-mail notable and Bellwarden's Mail can share a floor on F1 (4.7 item 7).
- The draft claimed "the phial never sits in an offer"; CN 14.7 (agreed) puts it on the F3–F4 boss card. The approved channel is kept, and a boss-channel phial takes the Wayward option (4.1).
- "A VR signature consumes its floor's VR slot", which the client accepted with the proposal, reverses LE-3's pending reading that signatures are exempt from the VR cap; it is now flagged as that reversal, and an ITEMS refusal returns to LE-3 as approved (4.13).
- Every gear offer option writes Scion-line memory, taken or not, because Q2 keys memory on generation: an item you passed up on a boss card will not drop later at its tier or below (4.7).
- Most signature line columns in 5.3 are one base line short of their budget, and most authored lines do not yet echo their boss; they are placeholders for segment 9, gated by V14 (4.13, 5.3).
- The M1 slice has no universal-chassis signature: Bellwarden's Mail is heavy armour for the M1 Vanguard (4.13).
- Six specialist signatures would leave solo Shades, Arcanists and Heralds a dead boss drop in M2 without second forms; they are requested now (5.2, 9.4).
- The carried-use name display is a floater on the first use per fight, not a banner on every swing (4.12).
- Patina is visible only on models, never on icons or cards; in M2 seven themes show their theme in loot through the stamp, provenance and Reverie only (6.2).
- Once the tier-4 scroll lever is spent, the Viaticum draws from Rare wands only and may fall to gear (4.7 item 9).
- Before the Rare memory switch, a dead Rare will not repeat, but a usable Rare can (4.7 item 4).
- The IR-16 guard can still leave a solo Arcanist or Shade a soft-weighted unusable Rare on F3–F4 when no usable Rare gear exists; the guard is kept (the scroll spike is the worse failure), and `DeadRareRepeatRate` and `LateFloorGearShare` watch it (4.7).

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a new lead's call or a lead's call after the check (confirmed by the client on 2026-11-22, with this document):

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| `requiresTargetPermille` | `RequiresHitRateBySize` gap > 10 points or solo on-build < 65% | Bias becomes max(3, the bias reaching the target), capped at 6 | Client (a change to LE 6.2) |
| `anyCompanion` at party ≥ 3 | `CompanionTrinketEmpty` > 25% (expected) | The lever fires at 1500‰ *(illustr.)* | Lead; CMP owner |
| `dueSidegradeFromFloor` 4 → 2 | `DreamsDueWaivedRate` still high after the lowest-card re-run | The sidegrade starts on F2 | Client and DH owner (a change to LE 6.6) |
| Scion pull | `ShrugShare` misses its target | ×2 on the next Scion of an owned family on F3+ field and camp (IR-18) | Lead |
| Yours label drop | `OfferPickBySlot` Yours > 60% | The Yours frame header label ("Suits <name>") is dropped; glyphs alone show gain | Lead; segment 10 |
| Reaction cost add | `EliteKpcDelta` > 10% | A per-line monster cost-add multiplier for reaction lines | Lead; enemies owner |
| Trinket Scion revert | Share of trinket families seen falls > 5 points | The trinket Scion exception is withdrawn | Lead; TK owner |
| Element Echo | M3, after playtest comparison | Adopted (IR-19 applied) or kept off | **Client** |
| Worn or lodged signatures | M3 | ENM 15.2, AR 12.6 changes and segment-9 re-validation | Client; enemies, armour owners; segment 9 |
| Re-engraving | Camp owner takes the hook | A value-neutral camp service; changes ITEMS 18.1 and ENM 15.3 step 5 | Camp and hub owner; client |
| **TK owner refuses** the trinket Scion exception | — | No trinket Scions; trinket family cap 1 stands; `TrinketScionRate` is not reported (new lead's call) | TK owner |
| **RS owner or segment 9 refuses** the roster flags | — | The Silk's and the Supper's elite notables fall to the lair cache as approved; `EliteShowsInUseRate` is reported per theme (new lead's call) | RS owner |
| **RS owner refuses** the IX21 reading | — | No deck identity depends on the door either way (signatures feed no exclusion, 4.7 item 7); the remaining question, whether the boss's own signature may follow the door, returns to the client (new lead's call; updated after the check) | RS owner; client |
| **ITEMS owner refuses** "a VR signature consumes the VR slot" | — | LE-3 as approved: signatures exempt from the VR cap (LE V13s (c)), with LE 18.2's fallback (a floor whose deck holds a VR draws its signature at R + authored) (lead's call, after the check) | ITEMS owner |
| **AR owner refuses** foci and shields as signatures | — | Segment 9 re-authors those forms (the Curtain-Fall Rod and any shield form) on weapons or body armour (new lead's call) | AR owner; segment 9 |
| **CN owner refuses** `effectTag` | — | Distinctness falls back to category and `use.damage` only; `OfferDistinctness` reported (new lead's call) | CN owner |
| **TL owner refuses** the lock-card category word | — | The approved LE Q2 wording stays; the sibling rule still applies (new lead's call) | TL owner |
| **TK owner refuses** LootPlan identities counting first in the Rare-trinket order | — | Fallback trinkets may be Rare only if no Rare trinket is reserved on the floor's deck (decidable from the dealt tiers and categories before the LootPlan consumers); TK-27's order stands (lead's call, after the check) | TK owner |
| **ITEMS owner refuses** the merged card (6.1) | — | ITEMS 8.2's pip and fit verdict stay; 7b's epithet "?" and verdict badge are added beside them, and segment 10 re-measures reads per card (8) (lead's call, after the check) | ITEMS owner; segment 10 |
| **RS owner refuses** Old Ragged's surcoat | — | The den's signature cache stays; the plate line and the cache decal carry the read alone (lead's call, after the check) | RS owner; segment 9 |
| **Segment 11 refuses** filter-first | — | A literal 9-draw reroll loop with the same filters, falling to the relaxation ladder instead of `substituted`; locked before golden seeds either way (new lead's call). Not triggered: segments 11–12 adopted filter-first as a lock before the M1 goldens (seeds and validation design 4.5, 4.11) | Segment 11 |
| **DH owner refuses** "camp sales count as dealt" | — | LE 6.6 as approved; the steering route is reported in `DreamsDueWaivedRate` by class (new lead's call) | DH owner |
| Weapons R-1 value-2 majors not landed | M2 content | R/VR signatures stay on impact, launcher, focus or armour bases (4.13) | W owner |
| CMP cuts its Deeds to M3 | CMP staging | Item Deeds follow (6.1) | CMP owner |
| The Inverted Cathedral | M4 theme design | Its `theme.loot` completed; V14 (k) blocks shipping until then | RS owner; segment 9 |
| Naming pass | — | Every placeholder name here | Client |

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | **The roster wall decides M2.** Without Rare gear and VR depth, F3–F5 resolve to scrolls, fallbacks and relaxations; V14 can warn and name gaps but cannot author content | Content requests (9.4) as M2 priority; the tier never moves silently (IR-7); IR-16 guards; `LateFloorGearShare` and `StepDownPerRun` block playtest sign-off |
| 2 | **The usability split** may still feel scripted for solo heroes or leak dead gear | `WaywardShare`, `UsableDropShare`, `BuildDiversity` (blocking) |
| 3 | **Equal value is not equal power** | `LinePowerDelta` (new; needs good kPC models); table moves or narrowed eligibility |
| 4 | **Memory thins deep pools,** and an unseen dreamed option can spend a Scion line or a gear key (by design: Q2 counts generation, 4.7 table); uniques are protected from the fourth and caches, others are not | Consumable offer options outside field floor keys; `RelaxationStepRate`; the M2 simulator's worst-seed depth gate (9.2) |
| 5 | **Order dependence:** any refactor of the generation order changes identities | The frozen order as a segment-11 contract; the draw-count tests |
| 6 | **Martial parties' boss cards are mostly gear** until the M3 tactical pool lands | Trinkets in the fallback; the CN content request (M3) |
| 7 | **Theme is cosmetic in rules;** the client may want more than presentation | Echo is the only invariant-safe rules path, held for M3 (agreed, Q4); `AnswerHereShare` |
| 8 | **Plain Gifts and free camp identify** leave Arcana a thin identification role | ITEMS 21's hidden-share lever becomes a content lever under IR-4: raise the rollable share of R+ records; `ArcanaChangedChoice` |
| 9 | **Signature authoring roughly doubles** per boss (tier forms, second forms) | Tier forms reuse one model; second forms re-trim existing family models; in M2 only for specialist chassis and where `SignatureUsableRate` < 70% (6.2) |
| 10 | **Draw load:** about 1,300 reserved draws per run and four new kinds; implementers must follow the static-set normalisation exactly | The draw-count tests; the `LootMixByCategory` history test |
| 11 | **Toy evidence is approximate** (five caveats in 8) | Every target re-run in the M2 simulator before it becomes data |
| 12 | **7a pegs move:** Sell share, category mix and coin coverage | 7a re-runs SellShareOfCoin, K1/K2, CampCoverage, LootMixByCategory in M2 |
| 13 | **Owner dependencies:** TK (trinket Scions, the Rare-trinket order), RS (roster flags, IX21, Old Ragged's surcoat), ITEMS (VR slot, the merged card), AR (foci and shields), CN (`effectTag`), TL (wording), segment 11 (filter-first), DH (camp sales) | Each has a fallback in 14.2 |
| 14 | **Guarded signatures** are less visible than carried ones (the party never sees them in use) | The plate line names the item; the signature cache shows its icon on a plate decal; worn or lodged forms remain an M3 option |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: lane lists per class, subclass and kit (within 4.3); the soft usability factor; the `identifyDifficulty` by tier; the rollable share; the smart-roll weights; the `LinePowerDelta` band; the Wax-Key lane preference, the spell-family repeat and named-VR-first terms; the `showsInUse` factor; the placeholder mark and line glyphs (6.1); the charms; the IR-16 caps; the `anyCompanion` permille; lever caps; every report target other than the stated gates; reserved draw totals (segment 11 computes).
- Record contents: which families are rollable, their 3–5 eligible entries, Scion lines, lexicon epithets, `synergy[]` and `carrierSafe` tags, within 4.5–4.6 and the content requests (9.4).
- Theme presentation: Patina material sets, trim palettes, stamps and Reverie text within 5 and 6, through the RC vetting list.
- Godot scene structure for the card, the Whence pane, the verdict glyphs, the carried-use floater and the signature cache decal, with no physics.
- Names, until the naming pass.
- **Not tuning:** one draw per identity factored by 4.2 with divisors on S0, in its fixed factor order; the relaxation order (target relaxations, then rungs, 4.7 item 8); signatures feed no exclusion; ≤ 1 rolled line and fixed lines obvious; value = budget exactly; the hard usability sockets **(agreed, Q1)**; Scion-line-once by generation and `oncePerRun` **(agreed, Q2)**; Plain Gifts **(agreed, Q3)**; 0 rules bias from the theme and Echo off until the client decides **(agreed, Q4)**; unarmed bosses guard their signatures **(agreed, Q5)**; the tier never moves silently; no `oncePerRun` record in the fourth or a dual-identity cache; the `lineRoll` and `curseGate` blocks always consumed; the frozen generation order; display never read by rules.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **The enemies owner:** IR-2 (15.3 step 2, 10.5 step 3), IR-3 (15.3 step 2 weights paragraph), IR-6 (15.3 step 2, 20.1), IR-7, IR-8 (17.5), IR-12 (15.2 elite row, 15.3 step 4, 7.2), IR-13 (15.2 boss row, 17.5), IR-14 (10.5 step 3, 15.3 step 5), IR-20 (cost add); the reaction cost-add lever (14.2).
- **The items owner:** IR-1 (12.3), IR-4 (11, 19.1 rule 14, 21), IR-5 (5.1, 12.2), IR-6 (12.3 flags), IR-9 (11; 8.2, the merged card), IR-13 (12.5; the VR slot ruling; the F5 sentence), IR-14 (19.2 order), IR-15 (12.3 `display`), IR-20 (13, 19.1 rule 9).
- **The weapons, armour and trinkets owners:** IR-4 (W 18, AR 14, TK rule 28); IR-5 (TK 13.8); IR-13 (AR 5.5 foci and shields); IR-14 (TK-27 order with LootPlan identities first, 4.17); IR-9 (TK 15, the merged card); the line-tag shape table (6.1); the content requests (9.4).
- **The consumables owner:** IR-8 (CN 10 note and the M3 martial pool; the boss-channel phial in option 2, CN 14.7 read), IR-16 (CN 18.1 default), IR-23 (`effectTag`); Herald scrolls (9.4).
- **The DH owner:** IR-10 (the Due additions; the lever).
- **The companions owner:** IR-17 (the party-size key); Deeds (6.1); kit lanes (4.3).
- **The class owners:** `loot.classLanes` and `subclassLanes` (4.3).
- **The traps owner:** IR-11 (the lock-card wording, TL 6.11).
- **The RS owner:** IR-6 (RS 6.4 read), IR-12 (roster flags), IR-13 (IX21 reading; Old Ragged's surcoat), IR-14 (reading), IR-15 (`theme.loot` on RS 8.0; the RS 7.4 art gates take the Patina hue check and the signature silhouette check), IR-19 (Echo), IR-21 (V14 in RS 11.2); the Memento epitaph line and the Reveries flagged in 6.1 ("Still warm") on the RS 7.4 tone review.
- **Dungeon generation 7a:** the re-runs of SellShareOfCoin, K1/K2, CampCoverage and LootMixByCategory (4.3); IR-3, IR-7 (LE 6.9 item 3 kept), IR-13 (LE 6.3, V13s (c), 18.2), IR-18.
- **Dungeon generation 7c:** the identity bundle, the provisional armed roster, the signature cache (4.18).
- **Segment 9 (bosses):** signature records, forms, second forms, carriage, epitaphs, the Headsman's Greataxe home, the Cathedral bosses (boss rooms design 4.11.1, change BR-27; the Kennel universal-chassis reading pending with this owner).
- **Segment 10 (UI) and audio:** 4.19, 6.
- **Segment 11 (seeds):** IR-14, the frozen order, filter-first before golden seeds (taken up by the seeds and validation design, approved 2026-11-23: 4.3, 4.4 GS10, 4.11, 8.1; changes SV-1, SV-2, SV-5, SV-18). Pending requests from it to this owner: the Resistant draw's owning entry (SV-5, 4.17) and the `DormantAtChoice` proof (9.3); fallbacks in its 14.2.
- **Segment 12 (validation):** V14 and the 7b reports (taken up by the seeds and validation design, approved 2026-11-23: V14 in its 5.1–5.2 phase model, the reports in its 6.3–6.6 and 6.12; changes SV-20, SV-25, with the SV-23 classes).
- **The camp and hub design:** Re-engraving (hook); shop comparisons reuse `ItemVerdict`.
- **The simulator:** every report of 9.3; the toy re-runs listed in 8.
- **Art and writing:** Patina, stamps, signature models, Reveries, provenance captions (6, 9.4).
- **Naming pass:** every placeholder name here.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Judges' grafts taken

All three judges' winner (B-fixed) is adopted. Grafts: C's weight split (4.2) with static-set divisors; the content gate as warnings plus playtest-blocking reports (resolving judge and critic differences); ≥ 2 VR per lane group; Scion-line memory at tier ≤ T; `oncePerRun` by generation; usability split; the Triad with hero guarantee and trinket fallback; Due additions (with the F2 and F5 changes held, see IK5); theme-tables signature rules and `theme.loot`; KinSwap (reshaped, IK4); presentation stack inside a card budget; `showsInUse`; Wax-Key wording; Plain Gifts; a trimmed 4-draw block plus A's per-floor gate; trinket Scions. Not taken: retargeting the F5 Due; a hard floor on Rare field cards; worn signatures on M1 Old Ragged.

### A.2 Fun critique (IF)

| Id | Finding | Resolution |
|---|---|---|
| IF1 | F4–F5 run dry on M2 | Tier never moves silently; IR-16; `LateFloorGearShare` blocks playtest sign-off; requests (8, 9.4) |
| IF2 | Same VR every run | ≥ 2 VR per lane group; named-first only ×2; `ChaseVarietyByClass`; tier-stable interim (8) |
| IF3 | Late finds read as trash | "=" only for redundancy; ⇄ for distinct; Scion pull lever; `ShrugShare` (4.14) |
| IF4 | Card overload | Card budget; one Reverie; bynames only with Deeds; no rolled pip (6.1) |
| IF5 | Dead signatures for casters | Second forms in M2 for specialist chassis and where < 70%; universal chassis; echo warned by V14 (4.13) |
| IF6 | Martial boss cards of three weapons | Trinkets in fallback; hero guarantee; M3 martial tactical pool; CN note (4.8) |
| IF7 | Hard floor scripted; diversity collapse | Soft on C/U/R field; Wayward new-line preference; `BuildDiversity` blocking (4.3) |
| IF8 | Themes feel absent | `AnswerHereShare` simulated; Echo switch buildable for playtest; KinSwap, elite preference, roster flags; tilt rejected (4.4, 4.12) |
| IF9 | Roll invisible within a run | Curated family tables; smart roll; `RollFitRate`; reveal chime; dormancy kept on field finds (4.6) |
| IF10 | Companions naked at 3–4 | `anyCompanion` party-size lever expected to fire; Rare neck families; trinket Scions (4.3) |
| IF11 | Yours slot trivialises the choice | `OfferPickBySlot` blocking; label-drop lever; Theirs prefers ▲ (4.8). Bias 4 on the boss card is settled and kept |
| IF12 | Name collisions | Wayward, Patina, Plays with, Scion (A.5) |
| IF13 | Wax-Key dull | Prefer a different lane within the category (4.11) |

### A.3 Balance critique (IB)

| Id | Finding | Resolution |
|---|---|---|
| IB1 | Hard floor on Rare + M2 roster → tonics for martial | Soft floor on R field cards; hard only on chosen, guaranteed and VR (4.3) |
| IB2 | Per-option fallback hands out fit premiums | Fallback is usable gear at ordinary weights, not fit-filtered (4.8) |
| IB3 | Generated uniques burnt unseen | `oncePerRun` barred from the fourth and dual-identity caches (4.7) |
| IB4 | Dreamed consumables starve field pools | Consumable offer options enter floor keys only for set distinctness (4.7) |
| IB5 | Scroll boss-damage spike | IR-16 guard; tier-4 default 1; `ScrollBossShareByClass` |
| IB6 | Post-filter normalisation funnels mass | Divisors on the static legal set (4.2) |
| IB7 | Equal value hides power creep | `LinePowerDelta`; concentration checked after smart weighting; Re-engraving out of M2 (4.6) |
| IB8 | Rolled Resistant on n = 0 themes; unflagged widening | Theme read only on authored records; rolled Resistant theme-blind; camp never reads N+1 (4.4) |
| IB9 | Filter-first changes per-seed identities | Adopted and locked before M1 golden seeds; told to segment 11 (4.1) |
| IB10 | CMP-30 fails, nothing wired | Party-size lever expected to fire; Rare neck families in requests (4.3). Kept a lever per IK10 |
| IB11 | Due saturation, manipulation, two-handers | Corrected re-run; camp sales count as dealt; two-hander and pair test (4.9) |
| IB12 | Elite showsInUse skews threat | `EliteKpcDelta` and cost-add lever (4.12) |
| IB13 | Two VR on one floor | VR signature consumes the VR slot (4.13), flagged after the check as withdrawing LE-3's pending exemption (IR-13) |
| IB14 | Trinket ladder waste | Scion only while ≤ 1 owned; lower copy ▽ with Sell hint (4.5) |
| IB15 | Cursed carriers underpriced | Carried cost add at T+1 (4.15) |
| IB16 | Memory reads player choices | Memory reads generation outputs only (4.7) |

### A.4 Consistency critique (IK)

| Id | Finding | Resolution |
|---|---|---|
| IK1 | Old Ragged wearing the mail breaks ENM 15.2, AR 12.6, a settled kit | Old Ragged guards it in the den's signature cache and wears a torn surcoat that is never loot; worn/lodged is an M3 option; flagged (IR-13; agreed, Q5) |
| IK2 | Base clash misdiagnosed; pre-registration forecloses chain mail | Correct example (chain mail); signatures exempt from run-wide owned-base reroll; after the check, no pre-registration at all (signatures feed no exclusion; ENM 20.1 covers deck notables only); `SignatureBaseShadow` (4.7) |
| IK3 | `unique` already defined by CN | New flag `oncePerRun`; `unique` untouched (4.7) |
| IK4 | Kin tie-break not computable at allocation | Flagged KinSwap LootBind remap (4.12) |
| IK5 | Due changes unflagged; toy off-spec | Property-id test is approved (told plainly); F2 sidegrade held as a lever; F5 retarget rejected; re-run required (4.9) |
| IK6 | Content gate contradicts LE 18 | V14 errors only on no-path; warnings plus playtest-blocking reports (9.2) |
| IK7 | Scroll usability ignores CN Q1 | `inClass` / `arcanaRead` split (4.3) |
| IK8 | Plain Gifts two ways | `known` form; scope Due, offers, fourth, Viaticum (4.14) |
| IK9 | Bias 4 extended unflagged | Kept at 4 on the boss card only (settled); 3 elsewhere (4.8) |
| IK10 | `anyCompanion` as a standing rule | Stays a lever, party-size key flagged (IR-17) |
| IK11 | Several unflagged changes | Each has an IR row (13): ENM 10.5 step 3 in IR-2; ENM 15.2 elite in IR-12; Scion pull IR-18; trinket fallback reading with cap order (4.8); fallback entries IR-14 |
| IK12 | Core choices inconsistent across judges | Memory: Scion-line-once at tier ≤ T, Rare staged; curse gate per floor; block of 4; keying ×8, widened to ×16 after the check for the hero-guarantee entry (4.7, 4.17) |
| IK13 | Name collisions; lane prefixes; `use.*` | Patina, stamp, Scion, Wayward, Plays with, `carriedFamilies`, `lane.*` namespace, TK jewellery lanes, `effectTag` (A.5). Told plainly: the critic's "Wind-Up has 0 hits" is wrong (case-insensitive whole-word grep: hits in 5 documents); the name Wind-Down Hammer stands |
| IK14 | "Answers here" exclusions | Reads the floor where presented during play; never boss cards, camp stock or N+1 (5.1) |
| IK15 | Signature categories and majors | Foci and shields flagged (AR 5.5); R/VR forms only on families with a value-2 major (4.13) |
| IK16 | "Once shown" breaks LootDoorInvariant | Generation-counted; V14 test (4.7, 9.2) |
| IK17 | Re-engraving contradicts ITEMS 18.1 and LE 8.1 | Hook only, no price (4.6) |
| IK18 | Toy assumptions bias verdicts | Caveats and re-run list (8) |
| IK19 | Hosting Resistant moves an M1 draw | AR 14.3's parent Resistant draw stays put for authored Resistant; d2 of each identity's own `lineRoll` entry hosts rolled element lines, parent or lootPlan, so no approved position moves (4.17) |
| IK20 | IR numbering collides across drafts | One register (13; A.6) |
| — | Phial swap not mentioned | Ghost, outside memory; after the check, a boss-channel phial (CN 14.7) takes the boss card's option 2 (4.1) |

### A.5 Names checked (case-insensitive whole-word grep across the 24 approved documents)

Re-run while writing this document (2026-11-22): every name below still has the stated hit count; the additions made while writing are marked.

- **0 hits, used:** IR-n, V14, IX/IF/IB/IK/IQ ids, Patina, Scion, Wayward, Whence, Reverie, byname, `lootLane`, `LanesOf`, `lineRoll`, `curseGate`, `oncePerRun`, `effectTag`, KinSwap, `carriedFamilies`, `showsInUse`, `lineLexicon`, "Plain Gifts", "Plays with", "Answers here", "What the dream kept", theme stamp, assay, Stopclock, Pestle, Curtain-Fall, Frame-Master, Warming Pan, Lying-in-State, Letter-Knife, Index Crossbow, Wind-Down, Ever-Lifted, Cell Zero Flail, Carillon Hammer, Precentor's Baton, Vellum Gambeson, Oxblood Brigandine, Swallowed Splint, Triad, B-fixed, `theme.loot`, `scionOf`, `cursedTwinOf`, `SignatureBaseShadow`, `rareTacticalFieldMaxPerFloor`, `heroGuarantee`, "Seen in Hand", and every new report name in 9.3. **Added while writing (new lead's call):** `ItemVerdict`, `loot.plainGifts`, `elementEcho`, the toy-failure ids IT1–IT15, and the plate line "Guards: <signature name>" (an extension of ENM 15.2's "Guards: cache", 1 hit, used deliberately). **Added after the check (0 hits each):** signature cache (replacing "lair cache", which names the SR4 elite socket), reveal chime (replacing "sting", RC7's boss term), carried-use floater (replacing "name banner"), fit line, footer badge, offer terminal ladder, `bossContent`, `alternateLineId`, `keyingStride`, `signatureAbilityTag`, `verbTag`, `trainingGroup`, `soldOrDroppedThisCamp`, `ArmsFloorUnmet`, `DeadRareRepeatRate`, `ViaticumAfterLeverFallbackRate`; stamps striped shirt, inkpot, ribcage and rocking horse (replacing cell number, shelf-mark, house-key bow and button; "hourglass" was rejected: it is the TL Hour lock glyph).
- **Existing meaning, used deliberately:** Deeds (CMP 8.7, extended to items); "Suits" (ordinary English in ABL; UI word only); `usableBy` (the approved record field; the query is `UsableBy`); `onParty` (LE 6.2, 1 hit; the same predicate, made concrete); `bossCardFallback`, `viaticumFallback` (LE 6.10 kinds, gaining a fourth entry); "Guards:" (ENM 15.2).
- **Rejected for collision:** Hallmark (three draft meanings), Stray (EP K4 *The Strays*), Kin/Akin (Kinward, Kindred Glint), Lineage (registers' "agreed lineage"), `unique` as generation memory (CN 14.1), `useTag` (CN `use`), `worn.*`/`arms.*`/`kit.*` lane prefixes (AR 5.7 tags, the arms floor, companion kits), Strongman (Carnival roster type) → Ever-Lifted Barbell, tally (LE A.5) → cell number → striped shirt (after the check: a numeral stamp breaks the pictogram rule), Mourner (Wake roster and Herald feature; A's example "Mourner's Rapier" dropped), "Two Ways" (ordinary phrase, 6 hits; dropped while writing) → Wax-Key sibling, "Verdict" as a code name (71 hits as a word) → `ItemVerdict`, F1–F15 as toy-failure ids (read as floors) → IT1–IT15, affix (ENM), variant, quality, shadow, delve, keepsake, Threshold, Due, Vault, Marked. Placeholders pending the naming pass: Oxblood (an RS palette word) and Precentor, Carillon, Vellum, Swallowed (theme-roster words) are kept as boss-tied placeholders.

### A.6 IR cross-reference (source drafts → this register)

| This register | Research / models / NM / theme draft ids (draft-local; never read as this register) |
|---|---|
| IR-1 lanes | research IR-1, A IR-1, B IR-1, C IR-1, NM IR-1 |
| IR-2 usability | smart IR-1, A IR-3, B IR-2, C IR-7, NM IR-2 |
| IR-3 weights | A IR-2, B IR-3, C IR-8, tabletop IR-3 |
| IR-4 shape | tabletop IR-5, A 5.1, B IR-5, C IR-2/3, NM IR-5 |
| IR-5 Scions | A IR-13, B IR-5/9, C IR-11, NM IR-6 |
| IR-6 memory | research IR-5, tabletop IR-6/7, A IR-6, B IR-7/8, C IR-10, NM IR-4 |
| IR-7 relaxation | tabletop IR-22, A IR-14, B IR-18, C IR-18 |
| IR-8 offers | research IR-3, tabletop IR-8/9, A IR-4/8, B IR-10, C IR-9, NM IR-3 |
| IR-9 Plain Gifts | research IR-8, A IR-9, B IR-11, C IR-12, NM IR-7 |
| IR-10 Due | B 6, C 11, numbers IT9 |
| IR-11 Wax-Key | tabletop IR-10, A IR-10, B IR-12, C IR-13, NM IR-9 |
| IR-12 carried | research IR-7, smart IR-9, A IR-11, B IR-13, C IR-14, NM IR-8, theme IR-43/44 |
| IR-13 signatures | tabletop IR-13, A IR-12, B IR-14, C IR-15, NM IR-8, theme IR-42/49 |
| IR-14 determinism | research IR-4, tabletop IR-4, smart IR-6, A IR-7, B IR-4, C IR-6, NM IR-14 |
| IR-15 theme presentation | tabletop IR-11, smart IR-7, B IR-15, C IR-5, NM IR-10/11, theme IR-40/41/47/48/52 |
| IR-16 tactical guard | balance critique |
| IR-17 anyCompanion | numbers IT10, balance critique |
| IR-18 Scion pull | NM IR-6 (camp pull), fun critique |
| IR-19 Element Echo | research IR-6, tabletop IR-12, A IR-15, B IR-16, C IR-19, theme IR-46 |
| IR-20 curses | tabletop IR-14, A IR-17, B IR-17, C IR-17, NM IR-14 |
| IR-21 V14 | tabletop IR-18, smart IR-16, A IR-16, B IR-19, C IR-20, theme IR-51 |
| IR-22 reports | tabletop IR-19, B IR-20 |
| IR-23 effectTag | B 3.1 `useTag`, consistency critique |
| Rejected | theme IR-45 (tilt), C hallmark houses, research/A IR-18 Pairs (parked), NM IR-13 (hook only) |

## Appendix B: sources

Diablo Wiki (Smart Loot; Loot 2.0); Blizzard forums (D3 drop tables); Gamespot and Wowhead (Diablo 4 Season 4); Maxroll season guide; Slay the Spire wikis (card rewards); Crawl forum and DCSS 0.30 changelog; D&D Beyond (DMG 2024); EN World (2024 random item tables; items by level); RPGBot; Archives of Nethys (PF2E runes); Paizo forums. Other game details are from memory, with doubtful points marked (Hades god cap, Gungeon synergy weighting, D4 details, DCSS current rules). Toy scripts (not project files): `/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/ir7b/sim7b.py`, `/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/ir7b/report_out2.md`, `/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/ir7b/extras_out.md`. No project files were modified.

