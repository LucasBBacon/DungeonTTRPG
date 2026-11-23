# Starting Kits: Design (Segment 14)

Date: 2026-11-23
Status: Approved, 2026-11-23
Parents (immediate): `2026-11-23-camp-hub-design.md` (camp and hub design, "CH", segment 13, approved 2026-11-23, **the most recent template**), `2026-11-23-seeds-validation-design.md` (seeds and validation design, "SV", segments 11–12), `2026-11-23-exploration-information-design.md` ("XI", segment 10), the loot designs `2026-11-22-loot-economy-design.md` ("LE", segment 7a), `2026-11-22-item-rolls-design.md` ("IR", segment 7b) and `2026-11-22-loot-sources-design.md` ("LP", segment 7c), `2026-11-22-events-design.md` ("EV", segment 8), `2026-11-22-boss-rooms-design.md` ("BR", segment 9), and `2026-11-23-cross-document-comparison.md` (reviews, "CC").
Other parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` ("CON"),
`2026-11-16-enemies-design.md` ("ENM"),
`2026-11-16-items-inventory-design.md` (items design, "ITEMS"),
`2026-11-17-weapons-design.md` ("W"),
`2026-11-17-armour-design.md` ("AR"),
`2026-11-17-trinkets-design.md` ("TK"),
`2026-11-17-consumables-design.md` ("CN"),
`2026-11-17-death-healing-design.md` ("DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "CL"),
the stage-2 class designs `2026-11-18-vanguard-class-design.md` ("VG"),
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC")
and `2026-11-20-herald-class-design.md` ("HR"),
`2026-11-20-companions-design.md` ("CMP"),
`2026-11-21-run-structure-themes-design.md` ("RS"; its change ids are RT-n), `2026-11-21-floor-layout-design.md` ("FL"), `2026-11-21-room-terrain-design.md` ("ROOM"), `2026-11-21-hazards-design.md` ("HZ"), `2026-11-21-traps-locks-design.md` ("TL") and `2026-11-21-encounter-placement-design.md` ("EP").
All 31 approved specifications and the cross-document comparison were read or grepped for: starting kit, kit, default kit, loadout, starting equipment or gear, starting purse or coin, boon, background, keepsake, memento, heirloom, curio, note, letter, lore, rename, engraving, Re-engraving, display name, item name, byname, Deed, Whence, Grave Goods, weightless, discard, junk, flavour item, F1 Due, the Dream's Due and spare weapon. Every kit item stat in this document was checked against W 17, AR 13 and the four class designs' kit sections (5). **This document edits no parent document** (13).

Items marked **(agreed)** are the client's answers of 2026-11-23 to the four questions this design raised (Q1–Q4; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document; parent documents' questions are always named with their document ("CH Q2", "SV Q1", "EV Q4") so that they never read as this document's Q1–Q4. The client also said that everything else in the lead's proposal stands as written: the six kits and their exact contents; the uniform budget; the Oddment rules (a display object with no slot, no price and no effect, Core-blind, outside the seed key, card text "Weighs nothing. Does nothing.", Leave it by the fire, no reward or punishment, profile memory of seen Oddments); kit lines; Deed bynames; companion kits unchanged in gear; the kit-variant catalogue; display surfaces; art and audio; the new mechanics; the numbers; data and validator V21 `StartingKits`; staging; ownership and hooks; the binding requirements; SK-1 to SK-17; the risks; and the appendix resolutions and names. Names are placeholders pending the naming pass.

**The lead's answers (0.1) and Model A with its grafts (3.2)** were accepted by the client by proceeding to the four questions. They are recorded here as the lead's answers; the client confirmed them on 2026-11-23, when it approved this document as a whole, **including every told-plainly item of 14.1**: kits are not made stronger; the client's sword example is contested and the name goes on the piece the F1 Due cannot replace; class-free background kits, Liveries and Haversacks are rejected; there is no real weightless inventory item and no letter or note; the same seed shows the same Oddment, but a text patch can change it; seeded runs never advance milestones; coin is exactly 15; the Herald's set B is 1 javelin.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-23, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-23. Items marked **(lead's call, after the check)** were made in answer to the design check of this document (builder, consistency, balance and narrative-art lenses); none overrides a client answer; they are listed in 14.3 with the "Told plainly after the check" items (T1–T12), and the client confirmed them all on 2026-11-23. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change SK-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-23 (with Q1–Q4 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge, a critic or a draft, or state an unwelcome forecast, and were told to the client in plain words (14.1; those found in the check, 14.3, T1–T12); the client confirmed them all on 2026-11-23. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q4 of this document (2026-11-23), or one recorded in an approved document (named with its document), restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal (the lead's answers of 0.1 and Model A with its grafts included); confirmed by the client with this document on 2026-11-23 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-23 |
| **(lead's call, after the check)** | A lead's call made in answer to the design check; never overrides a client answer; listed in 14.3, with the "Told plainly after the check" items, and confirmed by the client on 2026-11-23 |
| **(change SK-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text are flagged ⚑ there; the client's approval of 2026-11-23 covers them |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a judge, a critic or a draft, or states an unwelcome forecast; told to the client in plain words (14.1; those found in the check, 14.3) and confirmed by the client on 2026-11-23 |
| **(request)** | Asks another owner to rule; changes nothing until that owner rules; the fallback is stated (14.2) |
| **(hook)** | Owned by another design; this design states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator or by human sessions (8, 9). Never binding |

**The client's brief.** "Starting kits … should be enough to get the player started on a run, until they get enough resources to keep going themselves. They should also thematically match their source (if we're going for kits based on classes, otherwise they should thematically match the kit description)." The client also asked two questions: (1) should kits carry a note or hint to lore, perhaps a small item that weighs nothing and can be discarded? (2) could this be done by renaming some standard items in the kit, such as a sword renamed to something meaningful to the lore? Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; **combat is key**; contest the client or approved views where warranted.

**Change prefix.** This design uses **SK-n**. A case-insensitive grep for `\bSK-[0-9]` across `docs/` (the 31 approved documents and the reviews) finds **0 hits** (re-run while writing). The research drafts, the three models and the passes used colliding local SK numbers; this document issues **one register, SK-1 to SK-17** (13), and no id has two meanings (A.3).

**Validator.** **V21 `StartingKits`** is free. V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV, V17 BR, V18 XI, V19 SV and V20 CH (`Hollowfolk`, CH-25). A grep for `\bV21` finds **0 hits** (re-run while writing). V21 joins the consolidated suite only through **SK-10** (RS 11.2 suite table; SV 5.2 becomes V1–V21), as V20 did through CH-25.

---

## 0. At a glance

- **A kit is the class's starting gear, one per class creation pick.** There are six: Vanguard; Shade Blades and Shade Launcher; Arcanist; Herald Shield and Herald Totem. Contents are the approved class kits, with two corrections: SK-1 exact coin and SK-2 the Herald's illegal "2 javelins" (told plainly).
- **Kits do not get stronger** (told plainly). The approved package already carries a fresh solo hero through floor 1 to camp 1: mundane gear (0 VP), 2 belt potions, 1 guaranteed main-path potion, supply 2 plus 1 main-path supply, coin 15 plus the 17-coin floor (exactly the 32 coin camp 1 needs), and the F1 Dream's Due (an Uncommon weapon or focus for the hero). The kit is **meant to be outgrown on floor 1**, and the Due is that moment.
- **Every kit has the same budget:** VP 0, no trinket, an empty pack, 2 potions in one belt slot with **the second belt slot left empty on purpose**, coin exactly 15, supply 2 (a party counter).
- **Lore rides on three display-only layers, none in the rules or the seed key:**
  1. **One named piece per kit:** a *given name* on the piece the F1 Due cannot replace (the shield where the kit has one, else the armour), shown in the Whence pane **(agreed, Q2)**.
  2. **Kit lines:** one authored Whence line on every other kit item.
  3. **The Oddment:** one small lore object per hero per run. It takes no slot and does nothing, and its card says so **(agreed, Q3: M2, 6 per kit)**.

  The only *proper* name a kit item can have is earned: a kit item that fells a boss gets a **Deed byname** through the approved pipeline.
- **Kit variants (M3)** ship as **starting-boon options**, as CH-24 decides (MAIN 4.7), each a re-dressed mundane kit bundled with one skill training. A per-offer pick-rate report decides whether the client should revisit CH-24 **(agreed, Q1)**.
- **Hero kit gear stays sellable** at the Sell rate, the approved asymmetry with kitBound recruit gear; report `KitSoldAtCamp1` **(agreed, Q4)**.
- **Rejected as kit axes:** class-free background kits, Liveries (Model B), Haversacks (Model C), kit trinkets, extra potions, gold-instead kits, proper-name renames, a real weightless inventory item, a letter or note (told plainly).
- **Seams fixed independently of the four questions:** coin "about 15" becomes exactly 15 (the camp-1 margin is 0); the Herald's set B becomes 1 javelin; the missing ITEMS 18.1 hero kit-issue row; the heirloom-versus-F1-Due precedence; CH 4.9's "fixed per class" wording (MAIN 4.7 read only); creation warnings when a stat rearrangement Strains a kit item.

### 0.1 The lead's answers in plain words (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

1. **Are the kits enough to get a player started? Yes. They already are, and they should not be made stronger** (told plainly). The brief says "until they get enough resources to keep going themselves". In this game that moment arrives *inside floor 1*:
   - the F1 Dream's Due replaces the kit's main hand within about 15 to 25 minutes *(illustr.)*;
   - camp 1 then restocks potions and supply.

   Every way to make a kit "more" breaks an approved rule:
   - more potions: DH 5.8, "the answer is **not** more potions", and the potion counts are client numbers (DH 5.5; HE-1, DH-37);
   - a kit trinket: CMP 8.4 and MAIN 4.6 (loot outside the floor budget);
   - a non-mundane kit item: it breaks the F1 Due's strict-upgrade test (LE 6.6, V13g (g)) and LE 6.5's "F1 hero VP 0 (kit)";
   - more coin: it moves the LE camp-1 ledger;
   - a free flask or tonic: it is a dose outside the floor's sundry budget.

   This matches the best roguelikes. Slay the Spire's Strikes and Dead Cells' rusty sword are weak on purpose, and the identity lives in one piece. So the design effort goes into **identity, theme and lore, not amount**.

2. **Do kits match their source? Yes, by class.** Kits are per class and per creation pick (Shade Blades or Launcher, Herald Shield or Totem). Each has a silhouette, two physical damage types, a weapon verb per set, a named piece, kit lines and an Oddment pool written for that kit. Class-free "background" kits are **rejected as gear** (told plainly):
   - weapon training differs per class (W 10, AR), so a class-free kit is illegal for some class or shrinks to coin and potions;
   - it would be a second level-1 decision (CL 7.4; VG row 12; SH and ARC row 14);
   - it doubles the floor-1 balance matrix.

   MAIN 4.7's "background perk" survives as a boon (M3/M4) that carries its own Oddment pool. A background carries lore through what is in its pocket, not through gear.

3. **Client question 1: a note, or a small weightless item that hints at lore and can be thrown away? Yes, but as a display object called the Oddment (placeholder), not as an inventory item and not as a letter** (told plainly).
   - **Why not an item:**
     - The game has no weights. Capacity is abstract slots (ITEMS 4, agreed), so "weighs nothing" would mean a size-0 item, and size 0 is reserved for party counters that are never instances (ITEMS 5.3).
     - There is no Drop command (ITEMS 7.1–7.2).
     - An inert object among real gear gets read as a secret. Dark Souls' starting Pendant is the classic case, and it breaks RC1 ("no lying UI").
     - Once it is an item it can be sold, stolen, offered as an EV Curiosity price or scored by the Due, which makes it rules state. CH Q2 (agreed) forbids even "lucky tokens" and tiny effects.
   - **Why not a note or letter:** "Letter" and "Note" are taken (the CH Letter Rack, EV's Sealed Letter Strand, EV D1 "The Letter Addressed to You", the Ferryman's Note hook). A letter also implies someone writing to the hero, which commits a hero backstory that RS 7.5 and 19.2 leave open.
   - **What it is:** one Oddment per hero per run, such as "A porter's armband, mended with thread from another coat." It belonged to a resident of the dream with a trade, never to the hero.
     - It takes no slot, has no price and no effect. The rules core cannot see it, and it is outside the seed key.
     - Its card says plainly: **"Weighs nothing. Does nothing."**
     - It is shown on the creation card and in the hero's inspect header, and listed in Grave Goods.
     - At a camp Fire the player may **Leave it by the fire** (placeholder). It then sits as a fire prop for that visit, and the Book of Nights notes it.
     - Keeping it or leaving it is never rewarded or punished. Rewarding either teaches hoarding or a chore (CH Q2, AN11).
     - Profiles remember which Oddments they have seen, so new ones come first. The codex gains an Oddments page (M3).

4. **Client question 2: rename standard items in the kit, such as the sword? Yes, one piece per kit, as a *given name*. Not as a proper-name title, and not on the sword** (told plainly; contests the client's example).
   - **Not a proper-name title** ("Oathbinder"):
     - It breaks RC9 ("names may be whimsical but never lie"): a mundane item would read as a notable like Gatebreaker or Vowkeeper.
     - It spends IR 6.1's rule that bynames go only to items that earn a Deed, "so a byname means something".
     - It hides the family word that compare, verdicts and the Due announcement ("Warhammer → Uncommon Warhammer") depend on.
   - **What it is instead:** the card title stays the family word ("Shield"). The inspect card's Whence pane shows the given name as the item's one flavour line: *Called Nine Rivets*.
   - **Not on the sword:** the F1 Due exists to replace the kit's arms, usually within the first 15 to 25 minutes. A named sword would be the first thing the game takes away. So the name goes on the piece the F1 Due cannot target: **the shield where the kit has one, otherwise the armour**. The F1 Due is always a weapon or focus, dealt by slot, never by weapon set, so any weapon in the kit can meet a same-family Due; only shields and armour are out of its reach (LE 6.6) **(lead's call, after the check;** the draft's "set-B weapon or javelin" did not hold**)**.
   - **The sword still gets its lore beat, and it is earned in combat:** if the kit weapon fells a boss, it gets a **Deed byname** through the approved pipeline (IR 6.1, CMP 8.7). That is the only proper name a kit item can have. If the named piece earns a byname, the byname replaces the given name ("Once called Nine Rivets").
   - Every other kit item gets one **kit line**, for example "Rehafted more times than anyone admits."

5. **Both, layered: recommended and taken.** The named piece goes into every fight with you. The Oddment is the hint. The Deed byname is the reward. None of the three has a rules effect or touches the seed.

6. **Kit variants unlocked over runs: yes, at M3, as boon options** (CH-24 respected; agreed, Q1).
   - Each variant is a different mundane kit for one creation pick, plus one skill training. The skill training is MAIN 4.7's "background perk" in practice, and STAT 7.2 already lets the starting boon add skills.
   - I expect variants to lose often to an Uncommon heirloom, Old Friend or the Wayfarer's Pack (told plainly). A per-offer pick-rate report decides whether the client should revisit CH-24 (4.4).

### 0.2 The client's four decisions (agreed, 2026-11-23)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Where kit variants live | **A.** As starting-boon options, as CH-24 decides; each a re-dressed mundane kit bundled with one skill training; a per-offer pick-rate report; revisited only if a variant is taken at under half its fair share over 2 playtest rounds (4.4) | B: a hidden, defaulted "Kit" row on the class card that spends no boon (contests CH-24, CH 4.9, VG row 12, VG 7.10, SH 7.10). C: boons allowing at most 1 Common notable (lifts F1 entry VP above 0, LE 6.5) | SK-7 ⚑ (MAIN 4.7, read) |
| Q2 | Renamed items | **A.** One given name per kit, on the piece the F1 Due cannot replace, shown in the Whence pane with the family word kept as the card title; the kit weapon earns a proper name only through a Deed byname (4.5.1) | B: the given name on the sword (usually replaced within 20 minutes). C: a title rename with the family word beneath (Diablo style), bynames lifted to the name line (RC9, IR 6.1, locale cost) | SK-5 ⚑ (IR 6.1, read) |
| Q3 | When Oddments ship | **A.** M2, with 6 per default kit; the codex Oddments page in M3 (4.5.2, 10) | B: everything in M3, with the codex page | SK-6 ⚑ |
| Q4 | Hero kit gear sellable | **A.** Stays sellable at the Sell rate (the approved asymmetry with kitBound); report `KitSoldAtCamp1` (4.3) | B: 0 Sell through a `kitIssue` flag, like recruit gear but never deleted (held as a lever, 7) | — (no change; ITEMS 10.6 as approved) |

**Raised, not decided (future items, 14.2):** SK-16, how shared seeds treat variant boons (a request to the SV owner; recommended: variant boons are replaced by the default kit for the chosen creation pick; other boons follow SV's existing canonical-grant rule).

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Model A, class kits, with grafts from the lore layer, the catalogue and numbers passes, Model B's voice and Model C's empty belt slot (3.2) | M1–M3 | lead's call |
| 2 | What a kit is: `KitRecord`, issued at run start or on hire; default, variant and companion kinds (4.1) | M1 | lead's call; SK-3 |
| 3 | The uniform kit budget: VP 0, no trinket, empty pack, 2 potions in one belt slot, coin exactly 15, supply 2 (4.2) | M1 | lead's call; SK-1 ⚑, SK-3 |
| 4 | Kits and the economy: the F1 Due stays a strict upgrade; heirloom precedence; kit gear sellable; the price request (4.3) | M1–M3 | lead's call; **agreed, Q4**; SK-4 ⚑, SK-11 (request) |
| 5 | Kit variants as boon options with one skill; parity, risk band, roster, milestones, Heirloom-shelf bundle, pick rate, level-3 hint (4.4) | M3 | lead's call; **agreed, Q1**; SK-7 ⚑ |
| 6 | The named piece and its given name; kit lines; name rules (4.5.1) | M2 | lead's call; **agreed, Q2**; SK-5 ⚑ |
| 7 | The Oddment: record, issue, display key, memory, surfaces, Leave it by the fire, after the run, lore engine, codex, writing rules (4.5.2) | M2 / M3 | lead's call; **agreed, Q3**; SK-6 ⚑, SK-14 (request) |
| 8 | Creation, launcher and onboarding: kit strip, Oddment callout, creation warnings, Arcanist pack pressure (4.6) | M2 | lead's call; SK-9 ⚑ |
| 9 | Determinism: kit inside the key, display outside it; seeded runs read only; shared-seed request; run-modifier request (4.7) | M1–Later | lead's call; SK-6 ⚑, SK-15 and SK-16 (requests) |
| 10 | The catalogue: six default kits, eight companion named pieces, eight variants, display text (5) | M1–M3 | lead's call |
| 11 | Data and V21 `StartingKits`, reports, tooling (9) | M1–M3 | lead's call; SK-10 ⚑ |
| 12 | Corrections found while writing: the verb-per-set rule, the survivor-piece slot, the Arcanist variants' named piece, the variant skills, the Spearhand's losses, price-scale rows, name near-collisions (14.1) | M1–M3 | new lead's call |
| 13 | Fixes after the design check: named pieces on shields or armour only; kit issue order and placement; the display sidecar and named-piece log; one Oddment selection formula; memory write timing; the boon pick order; heirloom placement; `kit.acFloor`; Farcaster and Bookhand re-dressed; text, keys and checks (14.3) | M1–M3 | lead's call, after the check |

### 0.4 Intent, goals and anti-goals (new lead's call: added in the template's shape)

**Intent.** A kit is the hero's first ten minutes: enough to win the first fights, plain enough that the first Due feels like a gift, and particular enough that the player knows whose trade it came from. Its lore is carried in one named piece, a line on each item and one odd thing in a pocket, none of which can ever be mistaken for power.

**Goals.**
1. A fresh solo hero of any class and creation pick reaches camp 1 with the approved numbers: 2 + 1 potions, supply 2 + 1, at least 32 coin, the F1 Due (DH 21.5; LE 6.4, 6.6).
2. Every kit is mundane, legal, trained and un-Strained at the suggested array, with two physical damage types and a weapon verb in each set.
3. Every kit reads as its class at a glance: silhouette, verbs, named piece, kit lines, Oddment pool.
4. Every lore layer is provably inert: no Core read, no seed input, no price, no slot.
5. Kit variants widen choice without widening power, and the pick rate tells the client whether CH-24 should be revisited.
6. Every kit surface is reachable without a mouse and never relies on colour or quotes alone.

**Non-goals.** Class features, training and arrays (the class designs, STAT); weapon and armour stats (W, AR); potion and supply numbers (DH, ABL); prices (ITEMS, LE); companion abilities and kit upgrades (CMP); the codex host (segment 10); the premise (RS 19.2); the naming pass.

**Anti-goals.**
- A stronger kit, a kit trinket, extra potions, a gold-instead kit.
- A second level-1 decision or a class-free kit axis.
- A proper-name rename that outranks a notable or a byname.
- A lore object with a slot, a price, an effect or a hidden secret.
- Any reward, count or milestone for keeping or leaving an Oddment.
- Any text that writes the hero's past, a fallen hero or the Dreamer.

---

## 1. Survey

Research covered videogames, tabletop systems and lore-object practice. Lessons, with the game behind each (from memory and general design knowledge; Appendix B):

| Source | What it does | Lesson used |
|---|---|---|
| Slay the Spire | Weak fixed starter deck plus one signature relic per character; Neow's start bonus is a separate slot | Kits weak on purpose; identity in one piece; variety in its own slot |
| Dead Cells | Rusty sword plus a starter bow or shield; blueprints widen the pool | Unlocks widen the pool, not the power |
| Hades | Weapon Aspects and Keepsakes in separate slots; keepsakes have inscriptions *and* effects | Keep variants apart from boons where possible; take the inscription, never the bonus (CH Q2) |
| Enter the Gungeon | Weak named starter guns with flavour text | A named weak starter works if the name does not oversell |
| FTL | Ship layouts A/B/C unlocked by feats; criticised for obscure feats | Variants as sidegrades; plain milestones |
| Risk of Rain 2 | Loadout skills unlocked by challenges | Our variant model |
| Caves of Qud | Callings and castes set kits; a 2022 patch gave all a universal heal floor | A per-identity kit plus a universal healing floor (our DH package) |
| DCSS | Species × background sets the kit | Clear kit identity; also why a second kit axis is rejected |
| Brogue, Spelunky | Universal humble starts | Minimal kits make loot exciting |
| NetHack, Shadow of Mordor | Player naming; Nemesis-earned names | Earned names beat given ones; keep names display-only |
| Dark Souls (Pendant), Elden Ring (keepsakes) | A zero-effect starting gift became famous | Zero-effect objects are remembered; say plainly that they do nothing |
| Hollow Knight, Witcher 3 | Sellable lore junk becomes a sorting chore | Never give lore objects value |
| Fallout 3/4 | Notes held in a separate tab | Keep lore off the gear grid |
| Diablo 2 | Unique name with base type beneath | The rejected Q2 option C's form |
| D&D 5E backgrounds, trinket table | Sage's "letter from a dead colleague posing a question" | The flavour object is a question, not an answer |
| D&D 2024 | Package or gold instead | Gold-instead needs a pre-run shop; we have none (rejected) |
| Knave, Into the Odd, Mörk Borg, Electric Bastionland | Fast complete kits; one odd object carries tone | One strange object beats a page of lore |
| Dungeon World | One Signature Weapon, named and described | One named piece per character, not every item |
| Blades in the Dark | Load as a promise | Rejected: hidden inventory breaks RC1 |

**Cross-cutting lessons.**
1. Kits are weak on purpose, and identity lives in one piece.
2. Variants are sidegrades in their own slot.
3. Unlock goals must be plain.
4. Zero-effect lore objects are loved when the text is good and they ask nothing of the player.
5. Player-earned names beat authored proper names.
6. Lore objects must never share space or value with gear.

---

## 2. Audit

### 2.1 What the approved documents fix (settled unless a flagged SK changes it)

**Hero kits** (lead's calls in the class designs; numbers *(illustr.)* there except the 2 potions, which are settled by DH 12.4 and DH-37):

| Kit | Source | Armour | Set A | Set B | Belt | Coin |
|---|---|---|---|---|---|---|
| Vanguard | VG 7.10, VG row 12, VG-21 | chain mail (heavy, AC 16; Str +1; Ringed) | warhammer (1d8 B, v1d10, Push) + shield (+2; bash 1d4 B Push) → AC 18 | greataxe (1d12 S, two-handed, heavy, Cleave) → AC 16 | 2 potions | "about 15" |
| Shade Blades (default) | SH 7.10, SH row 14, SH 7.14 | leather (12 + Dex: AC 15 at Dex +3; Supple) | shortsword (1d6 P, light, Vex) + dagger (1d4 P, light, thrown 5, Nick) | sabre (1d6 S, light, Flourish) + dagger | 2 potions | "about 15" |
| Shade Launcher (creation pick) | SH 7.10 | leather | longbow (1d8 P, two-handed, range 14, Aim; Str 0) | sabre + dagger | 2 potions | "about 15" |
| Arcanist | ARC 7.11, ARC row 14 | leather (AC 14 at Dex +2) | staff held two-handed (1d6 / v1d8 B; Parry pool-free +2 against melee; Steady Casting) | dagger (thrown, piercing fallback) | 2 potions | "about 15" |
| Herald Shield (default) | HR 7.7, 7.11, HR row 10, HR-36 | chain shirt (medium, 13 + Dex; Ringed) | longsword (1d8 / v1d10 S, Parry) + shield → AC 16 at Dex +1 | "2 javelins" | 2 potions | "about 15" |
| Herald Totem (creation pick) | HR 7.7, 7.11 | chain shirt | longsword + Totem (Loom) → AC 14, with a creation warning | "2 javelins" | 2 potions | "about 15" |

- "No kit presets at creation": VG 7.10 and VG row 12; SH 7.10 "no other presets"; ARC row 14. A second level-1 pick breaks "one choice per level" (CL 7.4, ABL 10.3) (settled).
- VG 7.10: at level 3 the camp screen suggests a one-handed spare with a borrowable verb (sabre or sickle; M2) (settled).
- Kit is a class-record field, `chassisOnly` (CL 12.1; HR's dip guards). Kit records answered at ITEMS 17 row 3 (CC-46) (settled).
- Training: Herald `weaponTraining {simple: all, martial: hands == 1}` (CL, HR-9); every other class grants simple and martial (W 10.2) (settled).

**Companion kits (CMP 8.3, 8.4; M2):** Gatekeeper (chain mail, shield, warhammer), Brawler (chain mail, maul), Scout (leather, shortbow, dagger), Outrider (leather, short sword, darts as a dagger skin), Tinker (leather, dagger), Hedgemage (leather, staff), Drummer (hide (Sure-Footed), spear), Shieldbearer (chain shirt, shield, mace).
- Each kit has 1 potion. All kit instances are `kitBound`: they sell and scrap for 0 and are deleted on dismissal (CMP-21; ITEMS 10.6, 15, 18.1).
- No kit trinket. Kit upgrades come at levels 3, 6 and 9 (ABL 11.2, CMP-16). Accept kit defaults is CH-17.
- One silhouette and accent per kit; Deeds (CMP 8.7) (settled).

**Inventory (ITEMS):**
- No weights (ITEMS 4, agreed).
- Places: two weapon sets, 1 armour, 2 trinkets, a belt of 2 (hard max 3), and a pack of max(2, 3 + Str): Vanguard 6, Shade 3, Herald 3, Arcanist 2 (ITEMS 5.2).
- Size 0 means a party counter, never an instance (5.3); keys are the one exception.
- No Drop command (7.1). Gear never stacks. `stackMax` is 3 for potions and 2 for flasks.
- Mundane items are "shop and kit only" (12.5, settled); mundane bases come only from the vendor and kits (ENM 15.9).
- `channels[]` includes `kit` and `boon` (12.3). The `display` object is never read by rules (12.3).
- Only heirloom templates persist between runs (ITEMS 14, 15). There is no stash (10.7) (settled).

**Economy (LE):**
- Coin is never spendable on F1 (ITEMS 10.1, agreed).
- F1 main-path coin floor is 17, "so camp 1 holds ≥ 32 with the kit's 15" (LE 6.4 item 4; DH 21.5 report).
- F1 hero equipped VP is 0 (kit) (LE 6.5).
- **F1 hero-arms rule:** "kits are mundane, so the F1 Due is a U weapon or focus for the hero"; strict-upgrade test (IR-10); waiver 20 coin; "The Due never retargets" (LE 6.6, V13g (g); M1).
- Items sold or dropped at a camp count as still dealt for the Due's deficit and slot test (LE 6.6, pending the DH owner's confirmation).
- Sell is 250‰ (LE 6.4 item 6); LE 3.1 still prints 400‰ (stale); 200‰ is a live fallback (ITEMS 10.6).
- Camp-1 ledger: purse 15 + 35 + ~23 ≈ 73, restock 32, wish list ~122, coverage ~0.60 (LE 7).

**Healing and supply (DH, ABL):**
- HE-1 / DH-37: 2 potions per hero kit, 1 per recruit kit, camp top-up 1. These are client numbers.
- `Floor1BossSustain` reads "2 kit + 1 found".
- F1 hero deaths under 5% (DH 18.2, agreed).
- The run starts with 2 supply, a party counter (ABL 15.3).

**Boons and the hub:**
- MAIN 4.7 (decided): one starting boon, "such as an heirloom item or a background perk"; boons do not stack; no permanent stat boosts (MAIN 2, 4.7).
- CH-24 (in MAIN 4.7): "Starting kits get no hub peg: kit variants ship only as boon options."
- CH 4.9: "Starting kits are fixed per class … any alternative kit may only ship as a boon option."
- Heirlooms (ITEMS 14, M3): Uncommon, banded, `unsellable`, `noSteal`, `oncePerRun`, 0 VP for budget, counted as owned for the Due; picked at the hub's Heirloom shelf with exact text shown.
- STAT 7.2 (agreed): "Offers and the starting boon add more [skills]."

**Seeds (SV):**
- Hero creation inputs (class, species, kit) enter F1 `GenInputs` (GS0.1).
- `boons[]` sit in `configHash` (SV-12).
- Display fields are hashed in `displayHash`, outside the key.
- Shared and seeded runs use canonical unlocks and grants and write nothing to unlocks, codex or `loreMemory` (SV Q1, agreed; CH 4.16).

**Lore and tone:**
- Hero motives, the Dreamer and endings are open (RS 7.5, 19.2).
- Premise test (EV 4.11): "who made, wrote, wound or watched this?" must be a resident in plain sight.
- DreamerLine: no Dreamer handwriting or letter-writer. The Unwoken Roll is EV's single fallen-hero device.
- CH Q2 (agreed): no mechanical or informational effect.
- AN1: figures never give or take items. AN5: no "down" verbs in ambient lines. AN9: one texture family per figure. AN11: no counts.
- RC1: no lying UI. RC7: reserved cues. RC9: names never lie.
- The RS "Names avoided" table (RS 7.x, line 1558) bans "Bell" head words.

**Item naming (IR):**
- Names tell the roll: "of …" suffix epithets (4.6); Scion rank words ("Vowkeeper, Twice-Sworn").
- Whence pane: provenance, Reverie, byname, Deeds; one flavour line per item; collapsed; never on the summary (6.1).
- Bynames only for items that earn a Deed (6.1, lead's call).
- Icons global and untinted; Patina model-only (6.2).
- Name collisions checked (V14 (f)); display isolation (V14 (j)).
- Skins are an approved concept: darts as a dagger skin, shield skins, robes (W 17.1, AR 13).

### 2.2 Every hook placed

| Hook (source, wave) | Placed here |
|---|---|
| ABL 20, STAT 10 (I-15, CC-14): class kits need a minimal starting set for M1 | 5.1; M1 Vanguard (10); stale wording SK-17 |
| ITEMS 17 row 3: kit records | 9.1 `KitRecord` (SK-3) |
| ITEMS 18.1: kit issue on hire only | Hero and boon issue row added (SK-3) |
| ITEMS 20 M1: "the starting kit" | 10 |
| AR 13.6: slice kits by training band; validation at kit AC and lowest legal AC | V21 (e) `KitAcFloor` |
| W 25/26: weapon sets per class and companion | 5.1, 5.3 |
| DH HE-1, DH-37, DH 21.5 | 4.2; SK-1 |
| LE 3.1, 6.4 item 4, 6.5, 6.6 | 4.2, 4.3; SK-1, SK-4 |
| CL 5.6 Floor1ClassSpread (class × creation pick × L3 subclass) | 8, 9 (rows per variant) |
| CL 6.3 ForbiddenClassGrant | V21 (k) on variants and bundled skills |
| VG 7.10 level-3 spare suggestion | `KitRecord.level3SpareHintKey` (SK-7), 5.1 |
| HR 7.11/7.12 creation warning; HR-10b per-pick arrays | 4.6 creation warnings; SK-2 rerun |
| SH 7.14 Blades default and ladder preview | 4.6 |
| CMP 8.3/8.4/8.7/11.1 | 5.2; given names on companion kits (SK-5) |
| CMP 12 Old Friend issue | SK-3 issue row covers boon issue |
| MAIN 4.7 / CH-24 | 4.4 variants as boons (SK-7) |
| CH 4.9 wording | SK-8 |
| CH 4.11 milestones, `UnlockHasHubObject`, "New in the dream" | 4.4 (Heirloom-shelf bundle) |
| CH 4.12 onboarding | 4.6 (SK-9) |
| CH 4.15.1 LoreContext; 4.15.8 codex; 4.16 lore domain; 9.1 `loreMemory` | 4.5.2 Oddment (SK-6) |
| CH `AbandonWritesNoLore` | Extended to `oddmentsSeen` (SK-6) |
| DH 17.12 / LE 8.2 Grave Goods | 4.5, 5.4 (SK-6) |
| IR 6.1 Whence, bynames | 4.5.1 given names (SK-5) |
| IR 4.15 heirloom hooks; ITEMS 14 | SK-4, SK-12; taken up by the heirlooms and curses design (segment 15): SK-4 widened to every heirloom slot (HL-3), SK-12 read unchanged, amulet heirlooms a pending request (HL-26, M4) (4.1.4, 4.6 there) |
| SV GS0.1, SV-12, 4.7 | 4.7 (SK-16 request; heirlooms read fallback (a) and HL-25 is a pending request, heirlooms and curses design 4.1.7) |
| TK 24.2 charm boon type | Not a kit (no change); no "charm" fields on kits (`accentId`) |
| XI 4.10 `uiMemory` staged disclosure | 4.6 |
| ENM 18.1 rival party reuses class kits (Later) | Data kept reusable (9.1) |
| RS 19.2 hero motives | 4.5.2 writing rules |
| MAIN/RS run-modifier slot | SK-15 `kitDelta` reservation (request, Later); taken up by Oaths (HL-21, a pending request; Later; heirlooms and curses design 4.5) |

### 2.3 Findings (told plainly)

- **F1. Coin "about 15" is a defect, not wording.** 15 + 17 = 32 = 2 × 12 + 8, the camp-1 restock (2 potions and 1 supply). The margin is zero, so a 14-coin kit breaks DH 21.5. Fix: SK-1.
- **F2. The Herald's set B "2 javelins" is illegal.** HR lines 876 (7.2), 1011, 1012 (7.7), 1147 and 1148 (7.11) say "2 javelins". W 11.1 allows an off-hand light weapon only beside a light main hand, and the javelin is not light (W 17.6: one-handed, thrown); gear never stacks either (ITEMS 12.8). W 9.3 (settled, ITEMS 17) says a thrown weapon stays with the thrower, so 1 is enough. Fix: SK-2, with an HR 7.12 rerun.
- **F3. CH 4.9 says "fixed per class",** but the Shade and the Herald choose their kit as their creation pick. CH-24 and its sentence in MAIN 4.7 do not say it ("Starting kits get no hub peg: kit variants ship only as boon options") and need no change. Fix: SK-8, CH 4.9 wording only. The meaning of CH-24 is unchanged **(corrected after the check)**.
- **F4. ITEMS 18.1 has no row for the hero's kit issue at run start** (only recruit hire). Fix: SK-3.
- **F5. The F1 Due and an heirloom weapon conflict.** A U heirloom weapon (M3 boon) ties the mundane kit weapon at 0 VP in the fixed deal, with no tie-break stated. Then either no strict U upgrade exists on F1 (the deck has no R card), or the Due lands as a U upgrade over a mundane item the hero no longer uses. Fix: SK-4, widened to armour and shield heirlooms by HL-3 (heirlooms and curses design 4.1.4).
- **F6. Stat rearrangement can make a kit illegal at tick 0.** STAT 5.1 lets the player rearrange the suggested array. A Vanguard at Str −1 Strains the greataxe (heavy needs Str +1) and chain mail; a Shade below Str 0 Strains the longbow; low Dex drops leather AC below what the kit strip prints. Fix: SK-9 creation warnings and V21 checks over legal arrangements.
- **F7. Mundane bases have no basePrice in any approved document**, although ITEMS prices them "by basePrice" (10.5) and the vendor stocks them. This is a data gap. Fix: SK-11 (request to ITEMS and LE).
- **F8. "Kit" is overloaded.** CMP and CL 7.8 use "kit" for a companion's ability package (Gatekeeper kit, kit upgrades); CH-17 says "Accept kit defaults". A "Kit" chip on a hero's mundane sabre would also look like kitBound gear, which sells for 0 ("Unsellable: kit"). Fix: SK-13; the player-facing word goes to the naming pass. "Issued" has 26 hits and is rejected as the chip; the placeholder chip is "Starting gear" (0 hits).
- **F9. Stale text:** LE 3.1 Sell 400‰ against the approved 250‰; ABL 20 and STAT 10 still say kits "move to the category designs". Fix: SK-17.
- **F10 (new lead's call; corrected after the check). The proposal's "an on-hit verb in each set" fails approved kits.** The Arcanist's staff carries Parry (a guard verb) and the Launcher's longbow Aim (a planted-shot verb); neither is on-hit. VG 7.10's "on-hit, payable line in each set" is a Vanguard requirement, read by Practised Hand. The Arcanist's set B holds the dagger **alone**, where Nick does nothing: Nick only moves an off-hand attack into the Attack action (W 6, W 11.3). That set is the class's declared piercing fallback (ARC 7.11), not a verb set. Fix: V21 (d) checks a usable weapon or focus verb in each set **in the set's actual layout** (Nick counts only in an off hand beside a light main hand), on-hit only where a class feature reads it, and exempts a set its class design declares fallback-only, which `KitHasFallbackDamage` checks instead (4.2) **(lead's call, after the check;** the draft counted Nick as usable in that set**)**.
- **F11 (new lead's call; corrected after the check). The survivor-piece rule as drafted ("not the main hand, not a focus") does not hold.** The F1 hero-arms Due is dealt by slot (main hand, off hand), never by weapon set, and its card is the lowest U weapon or focus a tier above the dealt item, which the player equips wherever they like (LE 6.6, IR 4.9). So a U sabre, dagger, javelin or shortbow Due can replace a same-family kit weapon in set B. Writing first read the rule as "not the set-A main hand"; the check showed that fails for the Shade's sabre, the Arcanist's dagger, the Totem's javelin and the Throwhand's shortbow, and the Farcaster's and Bookhand's proposed named staff is a focus. Fix: the named piece is **the shield where the kit has one, otherwise the armour**, because the F1 Due is always a weapon or focus (4.5.1, 5.1, 5.3) **(lead's call, after the check)**.
- **F12 (new lead's call). Four variant skills collide with class creation defaults** (Maulhand Athletics with the Vanguard's Athletics; Throwhand "Stealth or Perception" with the Shade's defaults; Bookhand and Spearhand Medicine with the Arcanist's and Herald's). Fix: the skill rule and corrected illustrative skills (4.4, 5.3).

---

## 3. Models and judges

### 3.1 The models

- **Model A, class kits.** One kit per class creation pick. Variants are M3 boons that bundle one skill training. One skinned identity weapon and one Oddment per kit.
- **Model B, Liveries.** Class kits plus a class-independent "costume for the night" chosen at the launcher, outside the boon. It swaps at most one set-B item and brings its own Oddment pool.
- **Model C, Haversacks.** Class kits plus one chosen dose (Tonic, Water Flask or Oil Flask), carved from floor 1's own sundry draw, with lore by Haversack.

Three judges scored each model 1–10 on fit with approved rules, balance, theme, cost and clarity.

| Model | Fit | Balance | Theme | Cost | Clarity | Mean total /50 |
|---|---|---|---|---|---|---|
| A, class kits | 9, 9, 9 | 8, 8, 8 | 8, 7, 7 | 8, 7, 7 | 8, 8, 8 | **39.7** |
| B, Liveries | 5, 4, 4 | 7, 6, 7 | 8, 8, 8 | 5, 4, 4 | 5, 5, 6 | 28.7 |
| C, Haversacks | 5, 5, 5 | 7, 7, 6 | 6, 5, 6 | 6, 7, 6 | 6, 6, 7 | 30.0 |

**Judges' reasons, in short.**
- **A** is the only model that obeys CH-24 and MAIN 4.7 as written. It keeps every load-bearing number and adds no first-run decision. Its weaknesses were all fixable by grafts:
  - a descriptor skin on the main hand the Due removes;
  - Oddment pools of 3 that repeat by run 5;
  - "Set it down" anywhere outside combat;
  - bell names;
  - no dominance check.
- **B** has the best flavour idea ("tonight's costume", "what was left in its pocket"). But about half of all class × Livery pairs, and every Shade pair, come out "dress only". It contests decided text and grows the parity matrix to about 90 arms.
- **C** is honest balance (the carve is budget-neutral and seed-invariant). But only three legal doses exist, so most Haversacks are cosmetic behind a mechanical-looking choice. It also cuts found F1 sundries to 2–4 and contests CH-24.

### 3.2 Model A with grafts (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

**Model A as the frame**, with these grafts:

- **From the lore layer:** the given name (in the Whence pane, not the title) and kit lines.
- **From the catalogue and numbers passes:**
  - the survivor-piece rule;
  - the explicit kit budget;
  - `KitNoDominance` and the variant risk band;
  - the verified variant roster (with Duellist cut);
  - pools of 6;
  - tiered milestones;
  - the Arcanist pack-pressure UI fix;
  - companion trims.
- **From Model B:** the "what was left in its pocket" voice, the background-perk boon carrying an Oddment pool, and cloth-overlay art for variants.
- **From Model C:** the empty second belt slot as a teaching invitation. Tonic, water and oil are held only as a lever behind floor-1 death reports, never as a pre-run choice.

The critics changed seventeen points; Appendix A.1 lists every finding and its resolution. Liveries and Haversacks are rejected (told plainly); the names "Livery" and "Haversack" (0 hits) are retired with their models.

---

## 4. Rules

### 4.1 What a kit is (lead's call; change SK-3)

- A **kit** is a `KitRecord` (9.1) issued to its owner at run start (hero) or on hire or Old Friend issue (companion).
- **Kinds:**
  - **Default kit:** one per class creation pick. M1 ships the Vanguard; M2 ships all six.
  - **Kit variant:** M3, a boon option that replaces the default kit of one creation pick (4.4).
  - **Companion kit:** CMP 8.3, unchanged in gear.
- **Not kits:** heirlooms (boons, ITEMS 14), supply (a party counter), the Oddment (display, 4.5.2), background perks (boons).
- A kit never changes the hero's level-1 choice. Shade and Herald keep Blades/Launcher and Shield/Totem as their creation pick; Vanguard and Arcanist have no kit choice (settled).
- **Issue** (SK-3; **lead's call, after the check:** the draft's "placed by ITEMS 6.2" would have put the whole kit in the pack). At run start, inside WO1 and **before F1 generation**, so that the F1 party snapshot the Due and the LootPlan read already holds the kit (GS0.1):
  - each kit instance goes straight to its `KitRecord` slot: set A to `weaponSet(hero, active, main|off)`, set B to `weaponSet(hero, spare, main|off)`, the armour to `armour(hero)`, the potions to `belt(hero, 0)`. Events: `ItemCreated{kit}`, then `ItemEquipped`. ITEMS 6.2's pack placement applies only to a piece that SK-12 displaces (7 item 2);
  - **creation order**, which fixes `instanceId` (a run-state counter that also keys IR's `itemPart` display hash and 6.2's tie-breaks): set A main, set A off, set B main, set B off, armour, then the potions one by one into belt 0. The 2 potions are 2 instances merged into one stack. A piece SK-12 does not issue is never created and consumes no `instanceId`;
  - every kit instance has `origin = {channel: kit, floor: 0}` and `everHeld = true`. **Every instance issued from a `KitRecord`** (default, variant or Old Friend) carries `origin.channel = kit`, whatever brought the boon: a variant record's `channels: [boon]` describes the boon, never its instances;
  - hero kit coin is issued in the same step as `CoinChanged{delta: 15, source: kit}`, before F1 generation, so LE's `LootLedger` and the camp-1 "≥ 32" check count it. Companion kits carry no coin (4.2);
  - companion kits use the same row: on hire at camp, or at run start for Old Friend, with their `boundToUnitId` set (CMP-21);
  - V21t `KitIssueStable`: kit issue is byte-identical across reloads and replays.
- **A hero kit piece** is identified by one predicate, `origin.channel == kit && boundToUnitId == null`. The "Starting gear" chip (4.5.1), `KitSoldAtCamp1`, "leave the starting staff" (4.6) and every kit report use it; kitBound recruit pieces (`boundToUnitId` set) show CMP's label and never the chip (SK-13). Hero kit instances carry **no** `boundToUnitId`: they are ordinary party items, sellable at the Sell rate (settled by omission in ITEMS 18.1; stated here; agreed, Q4). The kit id, for reports, sits in the display sidecar (4.5.1), never on the Core instance **(lead's call, after the check)**.

### 4.2 The kit budget (lead's call; changes SK-1 ⚑, SK-3)

Every hero kit and every variant spends exactly this:

| Line | Value | Source |
|---|---|---|
| VP | 0: mundane bases only, no rolled lines | LE 6.5, 6.6 (settled) |
| Trinket | none | CMP 8.4 logic extended to heroes (SK-3) |
| Belt | 2 Healing Potions in **one** slot (stackMax 3); the second belt slot **empty on purpose** | DH-37 (client numbers) |
| Coin | **exactly 15** (`kit.coinStart`, hero kits only; issued as `CoinChanged{source: kit}`, 4.1) | SK-1 |
| Supply | 2, a party counter, not in the record; the kit strip's supply text is generated from the ABL constant, never written as a literal **(lead's call, after the check)** | ABL 15.3 (settled) |
| Pack | empty | lead's call |
| Gear rules | trained, never Strained at the suggested array; legal under W 11.1 (no stacked gear); ≥ 2 physical damage types across the sets; a usable weapon or focus verb in each set **in the set's actual layout** (Nick counts only in an off hand beside a light main hand), **on-hit where a class feature reads it** (the Vanguard's Practised Hand, VG 7.10) **(new lead's call, F10)**; a set its class design declares fallback-only (the Arcanist's set-B dagger, ARC 7.11) is exempt from the verb test and checked by the fallback rule instead **(lead's call, after the check)**; a fallback damage type | V21 (d) |
| AC | **`kit.acFloor`**, one number per class and creation pick, in data: the kit's highest sustained set AC at the suggested array minus 2 *(illustr.)*, never below the AR-20 lowest legal party AC (11). It drives the creation warnings (4.6) and variant parity (4.4). Over every **legal arrangement** (all permutations of that pick's suggested array, HR-10b; plus species modifiers once species ship, M4), sustained AC stays ≥ 11 **(lead's call, after the check:** the draft's "the class's AR-20 contribution" was not defined anywhere**)** | V21 (e) |

- **The empty belt slot** is the invitation for the first found sundry. It teaches the belt without a tutorial (lead's call).
- **Companion kits:** 1 kitBound potion, no coin, no trinket (settled).

### 4.3 Kits and the economy (lead's call; agreed, Q4; changes SK-1 ⚑, SK-4 ⚑, SK-11 request)

- **The F1 Due stays a U weapon or focus for the hero** and is a strict upgrade for every kit and variant, because every kit is mundane (V21 `KitDueStrictUpgrade`).
- **Heirloom precedence (SK-4).** Two parts:
  1. **Tie-break:** in the fixed deal, on equal VP, the higher tier is dealt, so a held heirloom is the slot's dealt item.
  2. **F1 hero-arms precedence:** a hero whose arms already hold U or better, **or who holds an heirloom in any slot (weapon, focus, shield or armour; M3; change HL-3, heirlooms and curses design 4.1.4)**, is treated as "arms satisfied". The F1 Due then takes the approved 20-coin waiver. Order: WO1 kit issue → SK-12 placement → the fixed deal with the tie-break → `armsSatisfied = heroArmsTier ≥ U OR the hero holds an instance flagged heirloom (any slot class, a two-hander and the Burdened twin included)`, read from the issued instances after placement, never from `boons[]` → the waiver (`DreamsDueWaived`; the 20 coin sits in the Due's container slot on F1, never as kit coin).

  Why the waiver and not a retarget to armour: a retarget would give the heirloom hero two U pieces entering F2. That makes the heirloom boon even more dominant and breaks LE 6.5's F1 intent. The waiver coin also adds margin to camp 1. "The Due never retargets" holds unchanged. This applies to every heirloom slot class: an armour or shield heirloom hero plays F1 with the mundane kit weapon, U defence and 20 coin (parity by value, on a different axis; report `HeirloomSlotParity`) **(change HL-3, M3; it replaces "only weapon and focus heirlooms")**. V13g (g) gains one named heirloom case per slot class and one for the Burdened twin (Later). Fallback if HL-3 is refused: no armour or shield heirloom templates at M3 (heirlooms and curses design 4.1.4).
- **Selling kit gear (agreed, Q4).** Hero kit gear stays sellable at the Sell rate, as approved (told plainly: this asymmetry with kitBound recruit gear is kept on purpose). Under the proposed price scale, a spare sells for 1 to 3 coin *(illustr.)*, which is noise. Report `KitSoldAtCamp1`, split into the named piece and the other pieces. The `kitIssue` 0-Sell flag is **held as a lever** (7), never shipped by default. Rejected: Q4-B, 0 Sell for hero kit gear.
  - **Sell, Swap/Leave and Curiosity rows (lead's call, after the check).** Every hero kit piece (4.1's predicate) shows the "Starting gear" chip on these rows, so a bought plain shield and the kit's named shield never look alike. When the named piece is selected there, one inline consequence line shows, non-modal and with no extra confirm: "Sold as plain gear. Its name goes with it." (one whole-string key per action: sold, left, offered). The given name itself never appears on these surfaces (4.5.1). No price change and no block: kit gear stays sellable at the Sell rate (Q4).
  - **Reading (new lead's call):** LE 6.6 counts items sold or dropped at this camp as still dealt (pending the DH owner), so selling a kit piece cannot steer the next Due. If the DH owner refuses that item, `KitSoldAtCamp1` is read together with `HeroDreamsDueShare` before the lever is considered.
- **Mundane prices (SK-11, request).** The proposed basePrice scale (8.4) also sets live vendor prices for mundane bases, so it goes to the ITEMS and LE owners with a camp-1 coverage rerun at both 250‰ and 200‰. **A missing basePrice on any kit family fails the build from M2**, because the vendor and Sell already need one. Until the owners rule, the data carries the 8.4 scale as placeholder data, labelled `placeholder`; V21 reads it from data and hard-codes nothing **(lead's call, after the check)**.
- **Coin on F1:** unspendable (ITEMS 10.1). A D&D-2024 "gold instead of a kit" is therefore rejected.
- **Loot is never a lever for F1 deaths** (LE-24, settled). The kit is not either, beyond the variant risk band (4.4).

### 4.4 Kit variants (M3; lead's call; agreed, Q1; change SK-7 ⚑)

- **Form.** A variant is a `KitVariantBoon`: a full mundane `KitRecord` that replaces the default kit of **one creation pick** (`replacesKit`), plus **one skill training**.
  - It sits in `boons[]` and `configHash` (SV-12), is `oncePerRun`, and is offered on the **Heirloom shelf** with its exact contents shown before the pick (the ITEMS 14 rule).
  - It applies only when its class and creation pick match. "Boons do not stack", so a variant never coexists with an heirloom.
  - **Pick order (lead's call, after the check).** The Heirloom shelf comes before class pick (CH 4.8), so the shelf shows every unlocked variant, each labelled with its class and the creation pick it replaces. Taking a variant pre-selects that class and creation pick at creation. If the player then changes the class or the creation pick, the strip shows one non-blocking line, for example "Your boon replaces Launcher gear. Choosing Blades returns the boon." (one whole-string key per variant). Confirming creation with a class or pick that no longer matches clears the variant and reopens the boon picker inline, on CH 4.10's "no longer valid" path. "Begin again" re-validates a carried variant the same way. V21t `VariantPickConsistent`: no run starts with a variant whose class and creation pick do not match.
- **Why a skill.** A 0-VP sidegrade alone loses to an Uncommon heirloom, Old Friend or the Wayfarer's Pack. The skill is MAIN 4.7's "background perk" in practice, and STAT 7.2 (agreed) lets the starting boon add skills.
  - The skill is never one the class already has. **(new lead's call, F12)** "Already has" means the class's recommended creation default skills (VG Athletics, Medicine; SH 7.14 Stealth, Medicine, Perception; ARC Medicine, Lore; HR Intimidation, Medicine). Each variant also names one **alternate** skill, outside those defaults; if the player's own picks already include the variant's skill, the boon grants the alternate. **(lead's call, after the check)** The skill is resolved at creation confirm, after the skill picks: the variant's skill if untrained, else the alternate, else a third skill listed in data (`skillFallback`, outside the defaults, under the same bounds). The shelf card shows the conditional form ("Perception (Lore if you already are)"); the creation card shows the resolved skill live.
  - It is never Leadership (price effects).
  - It is never Mechanics at M3, because Mechanics changes F1 Locked and trapped container access, which feeds the loot budget. Mechanics is held as a lever.
  - It is held to CL 6.3 ForbiddenClassGrant.
  - The bundled boon's value must not exceed a U heirloom's on F1–F2 (report).
- **Parity (V21 (k)).** Same budget as 4.2. AC at the suggested array never below the replaced pick's `kit.acFloor` (4.2). No theme or element answers (R-SEED-2).
  - **`KitNoDominance`:** on gear only, the variant loses on at least one axis against its default (AC, best die, reach or range, verbs, damage types, pool-free defence). It compares the **union of both sets**: which set comes first is never an axis, because swapping sets is free outside combat (ITEMS 7.1), so every fight can open in either set **(lead's call, after the check)**. This extends W validation rule 13 from families to kits.
- **Risk band (V21t `VariantRiskBand`).** Stated in death-rate terms **(lead's call, after the check)**: each variant row's F1 hero-death rate and solo F1-boss death rate sit inside its band and under DH 18.2's 5% *(band widths illustr.)*:
  - Vanguard variants (the F1-favoured class): [default, default + 3 points] (they may be deadlier, never safer);
  - Shade variants (the thinnest Floor1ClassSpread margin): [default − 3, default] (never deadlier);
  - Arcanist and Herald variants: [default − 3, default + 3].
  - Rows are reported outside the class mean, like the Herald kit × subclass rows.
- **Roster.** 8 at M3, 2 per class (5.3). The two Arcanist variants are gated on the W owner confirming a mundane orb and tome base; otherwise they move to M4.
- **Milestones (M3; CH 4.11):** plain, achievement-keyed, never deaths, losses or percentages (EV Q4, AN11), and tiered for pacing (8.6):
  - variant 1: "Reach floor 3 as a <class>";
  - variant 2: "Fell the floor-2 boss as a <class>";
  - M4 variant 3: "Fell the floor-4 boss as a <class>";
  - codex plates: "A win as a <class>".

  Milestones for a class the profile has not unlocked stay hidden. Every milestone card carries SV's "seeded runs do not count" line: **seeded runs never advance milestones** (told plainly; SV Q1).

  **Heirloom R1 (reading, change HL-10; M3).** The heirloom class milestone "Fell the floor-3 boss as a <class>" is ordered after these by difficulty only, with no prerequisite; one run may fire all three. One WO6 writes, and the hub reveals, in a fixed order: these variant milestones, then R1, the R2 offer, R3 (heirlooms and curses design 4.1.2).
- **Hub object (`UnlockHasHubObject`).** One bundle per class on the **Heirloom shelf**, where boons already live. It is tied in cloth and marked with the class glyph and a pattern, never colour alone.
  - There is **no coat tag on the Coat rack**: CH-24 says "starting kits get no hub peg" (told plainly; corrects Model A and the catalogue).
  - **(new lead's call)** CH 4.11's "kit tag in the Veteran book" stays the companion-kit unlock object; it is never used for a hero kit variant.
  - "New in the dream" names the variant.
- **Pick rate (report, per offer).** For each variant: the share of runs in which it was offered and taken, against a fair share of 1/N for the N options shown. An **offer** is a Heirloom-shelf view in which the variant was eligible (its class and the variant unlocked) **(lead's call, after the check)**.
  - **Trigger to revisit CH-24 (Q1's rejected option B comes back to the client only then):** taken at less than 0.5 × fair share over 2 playtest rounds with ≥ 30 offers each *(illustr.)*.
- **Level-3 hint.** `KitRecord.level3SpareHintKey` (display-only, per kit and variant) replaces VG 7.10's single suggestion. It never names a family already in the kit, so a Swordhand player is never told to find a sabre. **(lead's call, after the check)** The families it names are stored as structured ids, `level3SpareHintFamilies[]`, beside the key; the text is generated from them, and V21 (k) checks the ids, not the text.
- **Rejected:** Q1-B (a hidden defaulted Kit row on the class card); Q1-C (boons with one Common notable).

### 4.5 The lore layer (lead's call; agreed, Q2, Q3; changes SK-5 ⚑, SK-6 ⚑)

#### 4.5.1 The named piece and its given name (SK-5; agreed, Q2)

- **One named piece per kit** (hero and companion). The record stays `item.shield`, with the same stats, verbs, price, verdict and comparison.
- **Storage (lead's call, after the check).** The given name, kit line and accent are **not** fields on the Core `ItemInstance` (ITEMS 18.1 has none, and Core would serialise, copy and delete them). They live in a display-side sidecar in `run.save`'s display section: `displayOverlay[instanceId] = {kitId, givenNameKey?, kitLineKey?, accentId?, bynameKey?, onceCalledKey?}`.
  - It is seeded at WO1 from the `KitRecord` (and on hire or Old Friend issue for companions), after the Core issue events.
  - The display layer updates it only by reacting to Core events: `ItemMoved`, `ItemEquipped`, `ItemTaken` and `ItemReturned` keep the entry; `ItemSold`, `ItemDestroyed` (any reason) and `ItemLeft` followed by `ItemScrapped` or `ItemDestroyed{lost}` close it.
  - It is display-tagged runtime state, outside the key; it is **not** in `displayHash`, which hashes content (the keys it points to are content and are hashed there). Core assemblies cannot reference the sidecar type (V21 (h)).
- **Survivor-piece rule (lead's call, after the check).** The named piece is **the shield where the kit has one, otherwise the armour**. The F1 hero-arms Due is always a weapon or focus (LE 6.6), dealt by slot, never by weapon set, and the player equips it wherever they like, so any kit weapon can meet a same-family U Due; a shield or armour never can. V21 `KitNamedPieceNotF1Target` is computed over the F1 deck: for each class × creation pick (and variant), no card in the F1 hero-arms candidate set (U, usable, trained) has the named piece's family; by construction it holds for every shield and armour. Companion kits are exempt **(new lead's call:** companions never face the F1 Due; their named piece may be any piece, and an F2 or F4 Due that replaces it follows the event table below**)**.
  - Picks: Vanguard, the shield; Shade Blades and Shade Launcher, the leather (one shared name: the same coat, carried by the same trade); Arcanist, the leather; Herald Shield, the shield; Herald Totem, the chain shirt (5.1).
  - The draft's set-B picks (the Shade's sabre, the Arcanist's dagger, the Totem's javelin) are retired: a U sabre, dagger or javelin Due could replace each of them.
  - The report `NamedPieceHeldAtF3` reviews the picks after 2 playtest rounds; "held" means owned by the party at F3 entry, in any location. The armour is never an F1 target; on a shield kit it is the stated fallback.
- **Where it shows.** The card's name line stays the family word ("Shield") with a small "Starting gear" chip (placeholder, SK-13), so a kit item is never more prominent than a notable or a byname.
  - The given name is the item's **one flavour line in the Whence pane**: *Called Nine Rivets*. It shows on inspect, in Grave Goods and in the codex.
  - It never shows on the loot summary, compare and verdict rows, the carried-use floater, the HUD, the combat log, the vendor, the Swap/Leave and Curiosity pickers, or the Due announcement. Those use the family word; the Sell, Swap/Leave and Curiosity rows carry the chip and, for the named piece, the inline consequence line (4.3).
  - Accessible label, one whole-string key with a named placeholder and a translator note: "{family}, starting gear, called {givenName}". Quotes are never the only marker; where a locale uses quote marks, they are in the key, never added by code.
- **One rule: the name belongs to the instance.**

  | Event | Result |
  |---|---|
  | Equipped, swapped between sets, moved to a companion | Keeps the name |
  | Stolen or disarmed | Keeps the name; still named if recovered |
  | Sold | Sells as its base; the name is gone; the vendor never stocks named items |
  | Offered as a Curiosity price, Left or Swapped at a summary | Gone with the instance |
  | Replaced by a Due item | The Due item is a new instance; the name never transfers. Kept or sold by the player |
  | Upgraded, Re-engraved | Neither applies: mundane items have no rolled line; Re-engraving is held Later and never touches names |
  | Identified | Always known |
  | Earns a Deed (fells a boss) | The byname takes the Whence slot; the given name becomes "Once called Nine Rivets". IR 6.1's Deed-only rule holds |
  | Worn by a standing party unit when a boss falls (M3) | Credited by the Core `BossFelled` event for the heirloom R2 route, never by the display Deed; the plain kit shield or armour may come home as its family's authored heirloom; the instance is never copied (change HL-31, SK owner confirms; heirlooms and curses design 4.1.2). **Pending request (IR, CMP; SK confirms):** whether such a worn piece also earns a Deed byname; fallback: none, the bequest carries boss and theme marks only |
  | Death or win | Listed in Grave Goods from the named-piece log (below) |
  | Recruit kit piece dismissed | Deleted with the instance (kitBound, CMP-21) |

- **The given name never leaves the run (reading, recorded by the heirlooms and curses design 4.1.5, F17).** Grip-marks carry Deed bynames only; a named piece that earns a Deed passes on its byname, never its given name.
- **The named-piece log (lead's call, after the check).** ITEMS deletes an instance when it leaves the run, so the sidecar alone cannot tell Grave Goods what became of the name. `run.save.display.namedPieceLog[] = {instanceId, givenNameKey, fate: carried | sold | left | offered | lost | dismissed, floor}` gets one entry per named piece, written by the display layer when the instance's terminal Core event arrives (`ItemSold` → sold; `ItemLeft` then scrapped → left; `ItemDestroyed{offered}` → offered; `ItemDestroyed{lost}` → lost; `ItemDestroyed{dismissed}` → dismissed), and `carried` at death or win if no terminal event came. **"Replaced" means any fate other than carried**; a piece swapped into the pack and still owned is carried. Grave Goods reads this log.
- **Companion kit upgrades** are abilities and never swap the named piece (CMP owner confirms). Old Friend issues its kit with the name through the SK-3 issue row.
- **Kit lines.** Every other kit item gets one authored Whence line (`kitLineKey`). Kit items have no found theme, so the provenance line reads **"Carried from the start"** (placeholder) instead of "Found in <theme>, floor N", and there is no Reverie clash. **(lead's call, after the check:** the draft's "Issued at the Stairtop" asserted the premise that SK-14 only requests, and implied an issuer at the Stairtop that AN1 and CH Q1 rule out; the neutral line names no place and no act of issuing, whatever the RS owner rules on SK-14**)**. Kit lines and the provenance line pass the same word bans as Oddments and the per-word flags of names (V21 (g), (i)).
- **Name rules:**
  - no proper nouns, possessives, "of …", Scion rank words, theme names or "Bell*";
  - no idioms or puns that depend on the family word or English (critic: "Short Notice" on a shortsword dies in translation);
  - **(lead's call, after the check)** no word that names damage or a defect on a full-stat piece ("split", "bent", "broken", "cracked"), and no two given names sharing a head word (V21 (g) machine rows);
  - ≤ 18 characters *(illustr.)*;
  - collision-free by whole word and substring against record names, notables, epithets, Reveries, bynames, Patina captions, theme, Hollowfolk, Hollowfolk verbs ("Hear") and Proprietor names (V14 (f) pattern; V21 (g));
  - **(new lead's call)** each word of a name is also checked against property, verb, rank-word, party-counter and system-term names; a hit is not a build failure but is flagged to the naming pass (A.2 lists the current flags);
  - "no proper noun" is an authored tag (`properNoun: false`) that the tone review confirms; a linter cannot tell it from Title Case (V21 (g)).
- **Rejected:** Q2-B (the name on the sword); Q2-C (a title rename).

#### 4.5.2 The Oddment (SK-6; agreed, Q3)

- **Record.** `OddmentRecord` is display-domain data, never an ITEMS record. It has no price, size, slot, stack, unit, command or effect field. Core assemblies cannot reference the type (the `AmbientCoreBlind` / `CoreNoProfileRef` pattern).
  - **Records are never deleted (lead's call, after the check).** A patch may only mark one `retired`. A retired id still renders from a save, from Grave Goods and from the codex, and is never selected for a new run. Changing a pool's order is allowed and changes only new runs. V21t `OddmentRetiredLoads` loads a save that holds a retired id.
- **Pools (lead's call, after the check).** Each default kit has a pool of 6 (Q3). Each variant boon and each background-perk boon (M3) has its own pool of 3 (`oddmentPoolId` on its record). A hero draws from an **effective pool**: the held boon's pool, if any, followed by the default kit's pool for the creation pick, so a variant or background hero draws from 9 and the boon's own entries come first. An heirloom has no `oddmentPoolId`, so an heirloom hero draws the default kit's pool (reading; heirlooms and curses design 4.1.4).
- **Issue.** One per hero per run, by **one formula (lead's call, after the check;** the draft gave two derivations**)**:
  - **Order:** each part of the effective pool is permuted by H(`DisplayKey(run)` / `lore` / (campIndex 0, visitIndex 0, speaker "oddment", heroIndex, poolId)), the boon's part first, then the default kit's. **No `nightId`** enters it, because `nightId` is profile-scoped and two players of one code must see the same Oddment.
  - **Fresh seeded or shared runs** take the first entry of the order and write nothing. **Told plainly:** the same seed shows the same Oddment, but a text patch can change it (display content is outside the key).
  - **Personal runs** take the first entry not in the run-start `loreMemory` snapshot's `oddmentsSeen` (CH 4.16). When every entry has been seen, they take the least recently seen one: `oddmentsSeen` is kept in order of last sighting, and a sighting moves an id to the end.
  - **When:** the creation card shows the Oddment from run 2, before WO1, so the selection is computed from the run key the launcher has already drawn for this run (or the entered code), recomputed whenever the class, the creation pick or the boon changes, and frozen into `run.save`'s display section at WO1. A reload never changes it (`OddmentStableOnReload`).
  - **Replays:** read the Oddment from the replayed `run.save` display section and never recompute it (`ReplayOddmentMatchesSave`).
  - Selection never reads the forecast or the run's themes (R-SEED-2).
- **Display key (change SK-6 to CH-8 and SV 4.3).** The `lore` domain's camp root is camp-scoped, so the Oddment gets an explicit run-start row under it: `DisplayKey(run)` / `lore` / (campIndex 0, visitIndex 0, speaker "oddment", heroIndex, poolId), with no `nightId`. This is a flagged change, not a read.
- **Memory write (SK-6; lead's call, after the check:** the draft's "CH's write points: camp 1 reached, or run end" described writes CH and SV do not have**).** The mark is appended to `run.save`'s `pendingProfile` as a `loreSeen`-kind entry **on the first camp-1 entry (WO4)**, or at WO6 if the run ends before camp 1; **never at WO1**. It is flushed to `loreMemory.oddmentsSeen[]` only by SV's existing flushes (WO6, WO7 load, abandon), for personal runs only. So a start, quit, reload and abandon on F1 writes nothing, and abandoning cannot be used to roll through Oddments. `AbandonWritesNoLore` gains that case: "start, quit, reload, abandon on F1 → `oddmentsSeen` unchanged".
- **Where it shows (lead's call, after the check:** layouts and keys made localisable**):** the Oddment's name is a standalone label with no article ("Porter's armband"), shown in list layouts with a heading, never inserted into a sentence:
  - creation card: the heading "Also carried", the name below it (from run 2; 4.6);
  - **hero inspect header**: an Oddment line away from the belt, pack and equipment grids, non-draggable, with no hover target over slots, flat paper or string art and no item frame or glint. Its card shows the art, the caption, an alt-text string and "Weighs nothing. Does nothing.";
  - Grave Goods (DH 17.12, LE 8.2): the states below;
  - **never** on the HUD, the loot summary, compare, Sell, the Curiosity picker (it is not an item, so EV-9 needs no exclusion), the Due scorer or the Party Forecast.
- **Leave it by the fire** (placeholder; "Set it down" is rejected because AN5 bans "down"):
  - the **Leave** button appears only in the Oddment card opened from the camp **Fire station's rest anchor**; it is hidden everywhere else, never shown greyed. Keyboard and controller reachable;
  - one consequence line and confirm: "It stays by this fire. It does nothing either way." Undo works until the Fire station closes; after that, **leaving is final for the run**;
  - a UI-only display write: no command, no tick, no slot write (`LeaveWritesNoCommand`). The `left` state (with `leftAtCampSetId`) rides the next `run.save` rewrite, as CH's lore marks do (CH-9). **Told plainly:** SV has no write on quitting at camp, so a Leave followed by a quit before the next camp write is lost; on reload the Oddment is carried again and can be left again **(lead's call, after the check)**;
  - it shows as a small fire prop for that visit;
  - no reward, count, milestone or information either way (CH Q2, AN11).
- **States (lead's call, after the check):**

  | State | Inspect header | Grave Goods (death) | Win (M3) |
  |---|---|---|---|
  | Carried | The Oddment line and card | "Still carried when the dream closed:" heading, the name below | The Memento frame tag |
  | Left | A caption from the camp's own key (one whole-string key per camp set, `leftCaptionKey`, authored with that camp's own name or image, never an ordinal); no card action | "Left by a fire:" heading, the name below | No tag |

  The Book of Nights caption uses the same per-camp key. AN11's no-counts rule holds: no surface counts fires.
- **After the run.**
  - Death: the Grave Goods line for its state.
  - Win (M3): the Framer pins the **carried** Oddment's tag to the corner of the Memento frame, captioned "carried out of the Heart". This costs no figure line. It is **the first cut** if CH's `OneWinOwnerPerNight` budget refuses it.
  - Nothing ever returns as a boon or an item (ITEMS 15). Read as "no instance returns": an authored heirloom template unlocking is content, which ITEMS 15 allows (reading; heirlooms and curses design 4.1.2).
- **Lore engine (M3; CH owner).**
  - LoreContext (CH 4.15.1) gains display booleans `oddmentCarried` and `oddmentLeftThisNight`, plus a hidden `strataTag` key (never shown in a caption).
  - A Lingerer of the matching stratum may have one T2 line under AN3, AN5 and AN7 caps and `factOncePerRun`. Never on a death Night.
  - No figure ever asks for, takes or returns it (AN1). It never gates (AN2).
  - These lines are the second cut.
- **Codex (M3; agreed, Q3).** An "Oddments" page in the Library (CH 4.15.8 hosts; RS owns), filled on sight and write-only from the run's view. One illustrated plate per completed kit pool, behind "a win as <class>".
- **Writing rules.** They apply to every Oddment caption, name and codex paragraph, and **to every kit line and the provenance line** (lead's call, after the check).
  - **Author:** the previous carrier is a resident in plain sight with a trade (porter, footman, field-hand, dyer). Never a delver, adventurer, hero or the Dreamer. Never an echo of a past run's Grave Goods (`noFallenHero`; the Unwoken Roll stays EV's single fallen-hero device). Heirloom bequests and Grip-marks keep it: no hero name and no death text (reading; heirlooms and curses design 4.1.2, 4.1.5).
  - **Never** addresses "you" (no selfReference or AmbientSelfRefCap spend); never hero motives, names or family.
  - **Banned content:**
    - DreamerLine and AN5 words ("down", "went down", "back", "kept going", "wake");
    - RC7 reserved cues (bells, clocks, winding);
    - Hollowfolk signature props or voice textures (chalk and slates, pencils, placards, playing cards, library slips, guest lists, banner ribbons, hymn sheets);
    - **(lead's call, after the check)** named content from other designs: EV card titles and props, Strand names, RS faction motifs and role props, boss signatures, trinket and property names, trinket kind words (charm, knot, token), and level-up reward terms (Spell card, Offer, Talent);
    - theme names;
    - doors, routes and distances; numerals and number words in the `oddment.numberBlocklist` *(illustr.: 2, 3, 6, 12, 15, 17, 20, 32)*;
    - keys, maps, vials, coins, money, prices or receipts, or anything usable-looking;
    - the carrying kit's named piece, its families or their accessories;
    - implied heat, cold, light, sound or motion (RC4, RC9).
  - **Pool rules (lead's call, after the check):** no two items in one pool share an object noun; no Oddment repeats a frame or verb of a kit line in the same kit.
  - **Machine rows and authored tags.** Word lists, the blocklists, collisions, lengths, required fields and required author tags are machine rows of V21 (i) and fail the build. Implication bans cannot be linted, so each record carries authored tags the tone review confirms: `noUsableLook`, `noImpliedSense`, `countsReviewed`, `noKitEcho`.
  - **Shape:** a question, not an answer (the Sage's letter lesson). Caption ≤ 14 words; codex paragraph ≤ 60 words; dry register; CardFit at 150%.
  - **(new lead's call; updated after the check)** The tone review gives a named second look to the ribbon rosette (Herald Totem, next to "banner ribbons") and to every new sample of 5.1. **The check re-authored** the samples that broke these rules: the gatehouse rota and the scored strip (the Gaol's rota and tally texture), the tally of crows ("tally" is taken), the spell card (a level-up reward term), the blotting sheet and spectacle lens (RS's ink-and-spectacles motif), the list of toasts (the Supper's Toast), the usher's glove (the Receiving Line), the chair label (a place card), the cue card and blank speech (the Prompter's Cue), the cork (a bottle, next to "vials"), the rivet and the receipt (kit echoes and money), the filed thimble (the shortsword line's verb), the archery accessories, the string of knots (a trinket kind word), and the repeated glove, chair and "admits" items. Each pool stays at 6.
- **Premise row (SK-14, request to RS):** "Kits? The dream issues what its residents set aside." It is additive and commits nothing about the hero. The kit cards never depend on it: their provenance line is neutral (4.5.1).
- **Rejected:** Q3-B (everything in M3); a real weightless inventory item; a letter or note (told plainly).

### 4.6 Creation, launcher and onboarding (lead's call; change SK-9 ⚑)

- **Run 1 (CH 4.12: straight to creation).** The class card shows a simplified **kit strip**: the kit silhouette, a role word and AC, plus the hand-off line "Enough for the first stair. The dream owes you the rest." (placeholder; it ties the kit to the announced Due without naming the item, LE 6.15).
  - Shade and Herald show two strips with the default pre-selected (Blades, Shield). The Totem keeps its AC warning.
  - No variant row and no new decision.
  - Verbs, coin, supply, the given name and the Oddment line appear on inspect.
- **From run 2 (`uiMemory`).** The full strip shows: set icons with their verb text (never icon-only), armour and AC, "2 potions · 15 coin · supply 2" (the numbers generated from data and the ABL constant), and the "Also carried" heading with the Oddment's name below it.
- **The Oddment callout:** one-time and non-modal, the first time the hero inspect is opened at camp 1. Never at F1 entry, and never part of XI's vocabulary load.
- **Creation warnings (SK-9, extends the Totem precedent and ARC 7.14's warnings).** When the player rearranges the array so that a kit item is Strained or AC falls below the pick's `kit.acFloor` (4.2), the card says so in one line of shape plus text, never hue: "Greataxe: Strained at Str −1. Its verb is off."
  - Warnings never block (the ARC 7.14 rule). No auto-swap of gear (that would be a second kit axis).
  - Floor1ClassSpread adds a "worst legal rearrangement" arm, reported outside the mean.
- **M3:** variant boons appear on the Heirloom shelf with exact contents and skill (and its alternate). Taking one pre-selects its class and creation pick; a later change follows the pick-order rule of 4.4. "Begin again" carries the boon forward and re-validates it (CH 4.10).
- **Arcanist pack pressure (UI only).** When a Due U staff would fill the 2-slot pack, Swap/Leave offers "leave the starting staff" first (the staff found by 4.1's hero-kit predicate), with a one-time consequence line.
- **Playtest metric:** time on the creation screen on run 1.

### 4.7 Determinism (lead's call; changes SK-6 ⚑, SK-16 request)

- **Inside the key:** default kit and creation pick are F1 `GenInputs` (GS0.1, read). A variant travels in `boons[]` and `configHash` (SV-12, read).
- **Outside the key:** given names, kit lines, Oddments, kit strip text and art. All are display-tagged and outside `manifestHash`, so a text edit never moves a seed: their content (records and keys) is in `displayHash`, and the per-run sidecars sit in `run.save`'s display section (4.5.1, 9.1).
- **Seeded runs:** read `loreMemory` and write nothing; milestone progress is not advanced (SV Q1; told plainly).
- **Shared seeds and variant boons (SK-16, request to SV).** Undecided, told plainly. Options:
  - (a) on shared seeds, variant boons are replaced by the default kit for the chosen creation pick; other boons follow SV's existing canonical-grant rule (recommended);
  - (b) the share code carries `boons[]`, and the recipient is lent the variant.

  The player-facing result is stated on the seeded entry screen either way. Fallback until the SV owner rules: (a).
  - Heirlooms follow (a): shared and seeded runs use canonical grants, so no heirloom, and the entry screen says so. **Pending request (HL-25, SV owner rules; Later):** share codes may carry one heirloom template id and one `runModifierId`, never Grip-marks (heirlooms and curses design 4.1.7).
- **Run modifiers (SK-15, request to MAIN/RS; Later).** V21 invariants apply to the canonical kit. A future modifier may change kit fields only through declared `kitDelta` entries in `configHash`. It must declare the DH 21.5 and LE-24 gates it waives, and it is reported outside Floor1ClassSpread. A reserved V21 check fails the build if a modifier touches a kit without a declared delta.
  - **Oaths (Later; pending request HL-21 to the MAIN/RS owners of the CH-24 slot; agreed there, Q6):** an Oath that touches kits declares its `kitDelta` and the DH 21.5 and LE-24 gates it waives, and is reported outside Floor1ClassSpread; `runModifierId` enters `configHash` (HL-22). Fallback: the slot stays unlit until the MAIN/RS difficulty decision, and V21 (m) stays unused (heirlooms and curses design 4.5).

---

## 5. Catalogue

**(lead's call)** All names are placeholders for the naming pass. Each was grep-checked: 0 whole-word and 0 substring hits across `docs/` (A.2). Stats are the approved records, checked against W 17, AR 13.1 and the class kit sections (new lead's call: checked while writing; no discrepancy beyond F2).

### 5.1 Default hero kits

**(lead's call, after the check)** The gear of every kit is unchanged (the client confirmed the six kits). What changed: the named piece moved to the armour for the Shade, the Arcanist and the Herald Totem (4.5.1); the kit lines moved with it; and the Oddment samples and kit lines that broke 4.5.2's rules were re-authored in place (pools stay at 6). `acFloor` values are *(illustr.)*.

**Vanguard (M1)**
- Armour: chain mail (heavy, AC 16, Str +1, Ringed).
- Set A: warhammer (1d8 B, versatile 1d10 only with the off hand empty, Push) + **shield** (+2 AC; bash 1d4 B Push) → AC 18. Types B. On-hit: Push.
- Set B: greataxe (1d12 S, two-handed, heavy, Str +1, Cleave) → AC 16. Types S. On-hit: Cleave.
- Belt: 2 Healing Potions (one slot). Coin 15. Supply 2. Pack empty (6 slots). `acFloor` 16.
- **Named piece:** shield, called **Nine Rivets**.
- Kit lines:
  - warhammer: "The haft has been rewrapped over older wrapping."
  - greataxe: "Rehafted more times than anyone admits."
  - chain mail: "Scoured with sand until the rust gave up."
- Level-3 hint: sabre or sickle (M2), per VG 7.10.
- Oddment pool (6):
  1. "A stable-hand's hoof pick, the hook straightened."
  2. "A porter's armband, mended with thread from another coat."
  3. "A wool cap with the strap sewn on twice."
  4. "A cobbler's shoe-last, too narrow for any foot."
  5. "A sleeve garter, let out as far as it goes."
  6. "A cart-wheel spoke, planed flat along one face."

**Shade Blades (M2, default)**
- Armour: **leather** (12 + Dex: AC 15 at Dex +3, Supple).
- Set A: shortsword (1d6 P, light, Vex) + dagger (1d4 P, light, thrown 5, Nick).
- Set B: sabre (1d6 S, light, Flourish) + dagger (a second instance).
- Types P and S. Belt, coin and supply as above. Pack empty (3). `acFloor` 13.
- **Named piece:** leather, called **Grey Lacing**.
- Kit lines:
  - shortsword: "The guard filed short where it caught a sleeve."
  - dagger (A): "The grip wrapped by someone left-handed."
  - sabre: "Rewrapped at the hilt with a strip of blue cloth."
  - dagger (B): "Balanced with a twist of lead in the pommel."
- Level-3 hint: per kit (V21 (k): never a family already in the kit).
- Oddment pool (6):
  1. "A glove with each fingertip cut away, one at a time."
  2. "A tailor's pattern for a coat with one pocket too many."
  3. "A pocket square, still folded in its first creases."
  4. "A thimble, dented on the inside."
  5. "A sleeve button, the thread still tied to it."
  6. "A rag that smells faintly of oil and someone else's soap."

**Shade Launcher (M2, creation pick)**
- Armour: **leather**.
- Set A: longbow (1d8 P, two-handed, range 14, Aim; Str 0).
- Set B: sabre + dagger.
- Types P and S. Belt, coin and supply as above. Pack empty (3). `acFloor` 13.
- **Named piece:** leather, called **Grey Lacing** (shared with Blades: the same coat, carried by the same trade).
- Kit lines:
  - longbow: "Restrung so often the nocks are worn smooth."
  - sabre: as Shade Blades.
  - dagger: "Used mostly for cutting string."
- Oddment pool (6):
  1. "A grey feather labelled 'not this one' in a careful hand."
  2. "A field-hand's hat, the brim trimmed away at the front."
  3. "A scarecrow's button eye, sewn to nothing."
  4. "A strip of sacking stamped with half a farm mark."
  5. "A clay egg, painted speckled, from a nest box."
  6. "A milk-churn tag, the dairy's name scratched out."

**Arcanist (M2)**
- Armour: **leather** (12 + Dex: AC 14 at Dex +2, Supple).
- Set A: staff held two-handed (1d6 / v1d8 B; Parry, pool-free +2 against melee; Steady Casting; bulky).
- Set B: dagger (1d4 P, light, thrown 5, Nick; alone, so Nick does nothing here: the declared piercing fallback, ARC 7.11, exempt from the verb test, 4.2).
- Types B and P. Belt, coin and supply as above. Pack empty (2). `acFloor` 12.
- **Named piece:** leather, called **Felt Elbows**.
- Kit lines:
  - staff: "Worn pale where two hands have always held it."
  - dagger: "Its point ground back after it struck stone."
- Oddment pool (6):
  1. "A chipped mortar from a dyer's bench."
  2. "A ruler with the inches sanded off."
  3. "A herbalist's label, blank where the herb's name was."
  4. "A string with a bead pushed to one end."
  5. "A moth's wing, kept in a folded paper."
  6. "A pressed leaf between two squares of glass."

**Herald Shield (M2, default)**
- Armour: chain shirt (medium, 13 + Dex (max +2), Ringed).
- Set A: longsword (1d8 S, Parry; versatile 1d10 only with the off hand empty) + **shield** (+2; bash 1d4 B Push) → AC 16 at Dex +1.
- Set B: 1 javelin (1d6 P, one-handed, thrown 8, Pry) (SK-2).
- Types S and P. Belt, coin and supply as above. Pack empty (3). `acFloor` 14.
- **Named piece:** shield, called **Patched Twice**.
- Kit lines:
  - longsword: "Polished where a hand rests during long speeches."
  - javelin: "The shaft is newer than the head."
  - chain shirt: "Mended at the collar with an usher's thread."
- Oddment pool (6):
  1. "A footman's buckle, its prong missing."
  2. "A cracked wax seal pressed with a thumb, not a crest."
  3. "A sash pin bent straight, then bent again."
  4. "A paper lapel flower, one petal coloured in."
  5. "A collar stud, filed thin."
  6. "A boot-black tin, scraped to the bare metal."

**Herald Totem (M2, creation pick)**
- Armour: **chain shirt**.
- Set A: longsword + Totem (off-hand focus, Loom) → AC 14 at Dex +1, with the existing creation warning.
- Set B: 1 javelin (SK-2).
- Types S and P. Belt, coin and supply as above. Pack empty (3). `acFloor` 12.
- **Named piece:** chain shirt, called **Copper Links** (the Totem is a focus and the javelin a weapon; the F1 Due may replace either).
- Kit lines:
  - Totem: "Carved by someone who never finished the base."
  - longsword: as Herald Shield.
  - javelin: as Herald Shield.
- Oddment pool (6):
  1. "A wooden peg, worn smooth on one side."
  2. "A water glass, dry at the bottom."
  3. "A cushion tassel from a speaker's chair."
  4. "A spare shoelace, ironed flat."
  5. "A twist of paper holding loose tea leaves."
  6. "A ribbon rosette, its centre unpicked."

**(new lead's call)** The proposal's Herald kit line read "a usher's thread"; it is corrected to "an usher's thread" (grammar only).

### 5.2 Companion kits (M2; gear unchanged, CMP 8.3)

| Kit | Gear (settled) | Named piece (given name) | Trim note |
|---|---|---|---|
| Gatekeeper | chain mail, shield, warhammer | shield "Iron Rim" | Trim apart from the Vanguard |
| Brawler | chain mail, maul | maul "Birch Handle" | Apart from the Maulhand variant |
| Scout | leather, shortbow, dagger | shortbow "Waxed String" | |
| Outrider | leather, short sword, darts (dagger skin) | short sword "Worn Scabbard" | Apart from the Shade |
| Tinker | leather, dagger | dagger "Brass Pommel" | |
| Hedgemage | leather, staff | staff "Green Bark" | |
| Drummer | hide (Sure-Footed), spear | spear "Ochre Daub" | |
| Shieldbearer | chain shirt, shield, mace | shield "Boss Nail" | Apart from the Herald Shield |

- **(lead's call, after the check)** Three placeholder names were renamed: "Split Haft" and "Bent Tip" named damage on full-stat pieces (RC9), and "Painted Shaft" shared a head word with the retired "Straight Shaft". The quotes in this table are document notation only; the game shows the name with the "called" word (4.5.1).
- Companions get no Oddment until Later (AN13 holds companion voice).
- Their kit lines are 1 or 2 per kit.
- The named piece is display-only and kitBound with its instance (4.5.1, `CompanionNameOnIssuedPiece`).

### 5.3 Kit variants (M3; boon options; families from W 17)

| Class (replaces) | Variant | Armour | Set A | Set B | Gains | Loses | Skill / alternate *(illustr.)* | Named piece |
|---|---|---|---|---|---|---|---|---|
| Vanguard | **Swordhand** | chain mail | sabre (1d6 S, light, Flourish) + shield, AC 18 | warhammer (1d8 / v1d10 B, Push), AC 16 | Borrowable verb beside the shield (closes VG 7.10's Weaponmaster gap) | d12 and Cleave; main die d8 → d6 | Perception / Lore | shield |
| Vanguard | **Maulhand** | chain mail | maul (2d6 B, two-handed, heavy, Topple), AC 16 | handaxe (1d6 S, light, thrown 5, Cleave) + shield, AC 18 | Topple; a thrown option | d8 and Push on the shield set; versatile *(corrected after the check: it opens every fight at AC 18 if it likes, ITEMS 7.1)* | **Intimidation** / Lore *(new lead's call; was Athletics)* | shield |
| Shade (Launcher) | **Close Shot** | leather | hand crossbow (1d6 P, light, loading, pointBlank, range 6, Lunge) + dagger | sabre + dagger | Shooting from melee range | Range 14 and the d8 | Acrobatics / Arcana | **leather** *(after the check; was the sabre)* |
| Shade (Blades) | **Throwhand** | leather | handaxe (1d6 S, light, thrown 5, Cleave) + dagger (thrown 5) | shortbow (1d6 P, two-handed, range 10, Vex) | A ranged set inside a melee kit | Melee Vex (the shortsword); Flourish | **Athletics** / Acrobatics *(new lead's call; was "Stealth or Perception")* | **leather** *(after the check; was the shortbow)* |
| Arcanist (gated) | **Farcaster** | leather | dagger (P) + orb (Reach Out) | sling (1d4 B, one-handed, range 8, Push) *(after the check; was the staff)* | Artillery range; a ranged Push | Parry and Steady Casting (no staff); the staff's two-handed d8 | Perception / Arcana | **leather** *(new lead's call; was the staff, a focus)* |
| Arcanist (gated) | **Bookhand** | leather | dagger + tome (Annotate) | sling *(after the check; was the staff)* | Planning flexibility; a ranged Push | As Farcaster | **Arcana** / Perception *(new lead's call; was Medicine)* | **leather** *(new lead's call; was the staff)* |
| Herald (Shield) | **Macehand** | chain shirt | mace (1d6 B, one-handed, Topple) + shield, AC 16 | javelin (P, Pry) | Topple for escorts (HR 7.16) | Parry (the HR-36 trade, stated on the card); d8 → d6 | Perception / Lore | shield |
| Herald (Shield) | **Spearhand** | chain shirt | spear (1d6 P, thrown 5, Skewer) + shield, AC 16 | sling (1d4 B, one-handed, range 8, Push) | A ranged Push | Parry; d8 → d6; the javelin's Pry *(new lead's call; the proposal's "set B AC" is not a loss: both set Bs are AC 14)* | **Lore** / Perception *(new lead's call; was Medicine)* | shield |

- Every variant: 2 potions, coin 15, supply 2, VP 0, no trinket, an empty pack, its own kit lines and its own pool of **3 Oddments**, drawn ahead of the default kit's 6 (an effective pool of 9; 4.5.2), and a cloth overlay (scarf, sash, binding; never a role prop, RC2). Its named piece follows 4.5.1: the shield where it has one, else the leather or chain shirt.
- **(lead's call, after the check)** Farcaster and Bookhand were re-dressed. With the staff in set B they were the default Arcanist kit plus an orb or tome, a strict gear superset like the rejected "Bramble", because the opening set is never a cost (ITEMS 7.1 free swap; `KitNoDominance` compares the union). With a sling in set B they give up the staff, and with it Parry and Steady Casting, keep P and B damage, and gain a verb per set (Reach Out or Annotate; Push). `VariantRiskBand` judges the Arcanist's lower EHP; failing rows are cut. "In the opener" losses are struck from the table for the same reason, and Maulhand is re-checked against the Vanguard band (4.4).
- Every variant set is legal under W 11.1 (light + light; weapon + shield; weapon + off-hand focus; a two-hander; the staff; a one-handed weapon alone) and trained: Herald variants use simple weapons and martial one-handers only (HR-9).
- **Cut or rejected** (told plainly):
  - **Duellist** (rapier / sabre + sickle): held out. The draft's "−37% damage" figure was wrong (8.2). The real reason is that it has no thrown or ranged answer in either set, against the Shade's thin margin. The name "Duellist" also has 13 hits (A.2) and is retired with it.
  - Shade rapier + dagger: illegal (the rapier is not light).
  - Shade dagger + dagger / shortbow: one damage type.
  - Arcanist "Bramble" (staff / sickle + dagger): a strict superset of the default, so `KitNoDominance` rejects it.
  - Herald "Bell-ringer" and "Crier": renamed. "Bell" head words are banned by RS 7.x line 1558, and "Crier" has 85 hits.
  - Vanguard "Long Arm" (pike / flail + shield): held for M4.
- **Hand crossbow** loading and two-light legality: asked of the W owner (request, pending; W 11.1 and 11.3 already allow it, W 11.3 naming "an off-hand launcher (the hand crossbow)"); fallback in 14.2 **(corrected after the check)**.

### 5.4 Display surfaces, sample text

**(lead's call, after the check)** Every string below is one whole-string key; braces mark named placeholders with translator notes; nothing is assembled from fragments in code (8.8).

- Kit strip, run 2+: "Warhammer + shield · Push | Greataxe · Cleave — Chain mail AC 18 — 2 potions · 15 coin · supply 2", then the heading "Also carried" with "Porter's armband" below it.
- Inspect, Whence pane: "Carried from the start." then "Called Nine Rivets." (key "Called {givenName}.").
- After a Deed: "**Gatesplitter** (placeholder byname). Felled the Gaoler, floor 1." then "Once called Nine Rivets." (key "Once called {givenName}.").
- Oddment card: "A porter's armband, mended with thread from another coat." — "Weighs nothing. Does nothing."
- Leave confirm: "It stays by this fire. It does nothing either way." [Leave it] [Keep it]
- After a Leave: the camp's own `leftCaptionKey` caption in the inspect header and the Book of Nights.
- Grave Goods, carried: heading "Still carried when the dream closed:", then "Shield, called Nine Rivets" (key "{family}, called {givenName}") and "Porter's armband".
- Grave Goods, the named piece gone earlier: "Shield, called Nine Rivets: sold on floor 2" (one key per fate: sold, left, offered, lost).
- Grave Goods, Oddment left: heading "Left by a fire:", then "Porter's armband".
- Sell row, named piece: "[Starting gear] Shield · 2 coin", and on selection "Sold as plain gear. Its name goes with it."
- Heirloom shelf variant card: "Swordhand (Vanguard). Replaces your starting gear: sabre + shield / warhammer, chain mail. Trained in Perception (Lore if you already are). Loses: the greataxe."
- Creation, pick changed after taking a variant: "Your boon replaces Launcher gear. Choosing Blades returns the boon."

## 6. Art and audio (lead's call)

- **No new weapon silhouettes or inventory icons** (IR 6.2: silhouette = family; icons global and untinted).
- **Named-piece trims** go through the Patina trim-mask path: 5 hero (the two Shade kits share one named leather) and 8 companion at M2 (13), 8 at M3. Field `accentId`, not `charmId`: accents use cloth, wrap or binding, never a hanging token or pendant that reads as an equipped charm trinket (RC9).
- **Companion separation:** one trim per kit so the Vanguard reads apart from the Gatekeeper, the Herald Shield from the Shieldbearer, the Shade from the Outrider, and Maulhand from the Brawler (CMP 8.7).
- **Kit-strip dioramas** on the class card: 6 at M2, 8 at M3. This is a good outlet for the art team.
- **Oddments:** a flat paper-or-string icon outside the loot frame, no rarity glint, plus an illustrated inspect card: 36 icons and 36 cards at M2; 24 and 24 at M3, plus 3 and 3 per background-perk boon. Each ships an alt-text string.
- **Fire props:** 6 generic wrap families *(illustr.)* rather than one per Oddment: cloth bundle, paper twist, small hard object on cloth, coiled string, soft fabric fold, flat card under a stone. Every `OddmentRecord` maps to one through a required `firePropId`, checked by V21 (i), so a left glove never appears as an unrelated shape **(lead's call, after the check)**.
- **Codex plates:** one per completed kit pool: 14 at M3 (6 default and 8 variant pools).
- **Variant cloth overlays** on the hero model: 8 at M3.
- **Heirloom-shelf bundles:** one per class, with glyph and pattern.
- **Audio:** no new cues. The Oddment and Leave-it-by-the-fire actions use the generic soft UI sound. No bell, winding or clock sounds (RC7).
- **Later:** an Oddment carried into F5 appears as one Keepsake prop (RS 9.3); cost is one prop per Oddment.

---

## 7. New mechanics (lead's call)

1. **Hero kit issue** (SK-3): at run start, inside WO1 and before F1 generation, `ItemCreated{kit}` then `ItemEquipped` straight to the `KitRecord` slots, in the fixed creation order of 4.1; kit coin as `CoinChanged{source: kit}`. It also covers Old Friend's boon issue and variant boons.
2. **Heirloom placement** (SK-12; M3; **lead's call, after the check:** the draft's "the kit item it is a strict upgrade for, in the same set and slot" failed for other families and two-handers).
   - **Target:** the heirloom's slot class in set A: main hand, off hand or armour. A two-handed heirloom takes set A's main and off hand.
   - **Displaced pieces** go to the pack in issue order (4.1), each only if **at least 1 pack slot stays free afterwards**; otherwise that piece is not issued (never created, never owned, so nothing is lost). This keeps room for the F1 Due (4.6's Arcanist pack pressure).
   - **The given name moves only when the named piece is not issued:** it goes to the next issued piece in the kit's fixed order (`namedPiece.fallbackOrder`), and the receiving piece's kit line is suppressed, so it still carries one flavour line (IR 6.1). The order lists only shields and armour (4.5.1), so on a shield kit it ends at the armour; if no listed piece is issued (an armour heirloom on an armour-named kit), the kit carries no given name that run and the heirloom card says so. A named piece that is displaced but issued keeps its name in the pack.
   - The heirloom card shows "Replaces: <piece>" before the pick, and "(not carried: no room)" for a piece that will not be issued, for example "Replaces: staff (not carried: no room)" for an Arcanist.
   - V21 `HeirloomKitFits` checks the state **after** this rule has run, for every class × pick × heirloom template: the pack has at least 1 free slot, and every unissued piece is listed on the card. It holds by construction; the Arcanist staff and leather heirlooms (a 2-slot pack, bulky size-2 pieces, ITEMS 5.2) are named test rows.
   - Read unchanged by the heirlooms and curses design (4.1.4 there); M3 heirloom slot classes are exactly these placeable classes. **Pending request (HL-26, TK owner rules; M4):** amulet heirlooms, placed in an empty worn slot with no displacement, would need a worn-slot row here, a V21 `KitNoTrinket` waiver and a TK parity report (heirlooms and curses design 4.6).
3. **Kit variant boons** (SK-7) as in 4.4.
4. **Given names and kit lines** (SK-5) as in 4.5.1.
5. **The Oddment and Leave it by the fire** (SK-6) as in 4.5.2.
6. **Creation warnings** for Strain and AC (SK-9).
7. **Held levers, not shipped:**
   - `kitIssue` 0-Sell hero kit gear (only if `KitSoldAtCamp1` distorts camp-1 coverage; Q4 rejected it as a default);
   - a class-themed C non-healing dose in the empty belt slot, carved from F1 sundries (Model C's idea), only if F1 solo-death data asks for it;
   - Mechanics as a bundled variant skill.

---

## 8. Numbers (toy, not simulator; all *(illustr.)*)

Toy script kept in the design lead's scratch area (Appendix B). Assumptions: level 1, prof +2, primary +3, reference enemy AC 13 (65% hit, 5% crit), enemy attack +4.

### 8.1 Coin

- Kit 15 + F1 main-path floor 17 = **32** = camp-1 need (2 × 12 + 8). **Margin 0**, so SK-1 is mandatory.
- LE camp-1 ledger: purse ~73, restock 32, coverage ~0.60 (settled; unchanged).
- SK-4's waiver path adds 20 coin for heirloom heroes, any slot class (weapon, focus, shield or armour; HL-3, M3; heirlooms and curses design 4.1.4).

### 8.2 Damage per round (corrected per W 11.3: the off-hand attack adds no positive ability modifier; d6 + 3 ≈ 4.4, d8 + 3 ≈ 5.1, d10 + 3 ≈ 5.8)

| Kit / set | DPR |
|---|---|
| Vanguard warhammer + shield | 5.1 (~5.8 versatile without the shield) |
| Vanguard greataxe | 6.5 |
| Shade Blades shortsword + dagger (Nick) | **6.2** (4.4 + 1.75; the draft's 8.1 was wrong) |
| Shade longbow | 5.1 |
| Arcanist staff | 4.1 (the cantrip is the real damage, ~3.9) |
| Herald longsword | 5.1 with the shield (~5.8 two-handed) |
| Herald javelin | 4.4 |
| Swordhand sabre | 4.4 |
| Maulhand maul | 6.85 (its shield set is the handaxe's 4.4) |
| Close Shot hand crossbow + dagger | ~6.2 |
| Throwhand handaxe + dagger | ~6.2 |
| Duellist rapier (cut) | 5.1 (≈ −17% against Blades, not −37%) |
| Macehand mace | 4.4 |

**(corrected after the check)** The draft printed the d8 warhammer and longsword rows at the d6 figure. With the corrected rows, the Swordhand's and Macehand's "main die d8 → d6" loss reads as it should: 5.1 → 4.4, about −14%.

### 8.3 Effective HP (illustrative and understated)

- The toy gives every class the same 18 HP plus 2 potions × 7 (Recovery at level 1, Con +1). Class hit dice differ, so the spread is **understated**.
- Raw HP divided by enemy hit chance (32 effective HP; enemy +4): Vanguard AC 18 (35% hit) ≈ 91; Vanguard set B and Herald Shield, AC 16 (45%) ≈ 71; Shade, AC 15 (50%) ≈ 64; Arcanist and Herald Totem, AC 14 (55%) ≈ 58. Vanguard-to-Arcanist is about 1.6×, and more once hit dice differ **(corrected after the check:** the draft printed 111 and 87 and 1.9×, which the toy's own inputs do not give**)**.
- Losing a shield (+2 AC) raises incoming damage by ~29%; losing pool-free Parry by ~10–15%.
- The potion count is a client number, so **the kit is not the lever for this spread.**

### 8.4 Resale (SK-11 proposed scale; request)

| Kind | basePrice | Sells at 250‰ |
|---|---|---|
| Simple light or simple one-handed (dagger, handaxe, sickle, mace) | 6 | 1 |
| One-handed martial | 8 | 2 |
| Javelin, spear | 6 | 1 |
| Shield | 8 | 2 |
| Two-handed, staff or launcher (sling and hand crossbow included) | 12 | 3 |
| Off-hand focus | 10 | 2 |
| Leather / hide / chain shirt / chain mail | 8 / 12 / 16 / 24 | 2 / 3 / 4 / 6 |

**(new lead's call)** The proposal's scale had no row for the mace (simple, one-handed, not light) or for one-handed launchers; the mace joins the simple light row and the sling and hand crossbow join the launcher row, so every kit family in 5 has a price.

Whole-kit resale (FloorDiv per instance): Vanguard 13, Shade (Blades and Launcher) 8, Arcanist 6, Herald (Shield and Totem) 9. Variants: Swordhand 12, Maulhand 12, Close Shot 9, Throwhand 7, Farcaster 8, Bookhand 8, Macehand 8, Spearhand 10. A realistic spare sells for 1 to 3. `KitResaleBand`: within ±4 of the class default (all eight variants pass).

### 8.5 Kit lifetime

- Main hand: replaced by the F1 Due within floor 1 (~15–25 min).
- Shield and armour, the named pieces: never at the F1 Due. Armour until the F2 or F3 Due (~1.5–2.5 floors); shields often to F3. The Vanguard shield may go at the F2 or F4 Due (told plainly). A replaced piece is not deleted: the player may keep it or sell it, and Grave Goods records its fate from the named-piece log (4.5.1).

### 8.6 Unlock pacing

- With 2 milestones per class, 6 of 8 variants unlock by run 10 and all 8 by run 50.
- Tiering the M4 variant (F4 boss) and the codex plates (a win) stretches meaningful unlocks to about runs 60–120.

### 8.7 Oddment repetition

- A pool of 3, unseen-first, with 60% of runs on one class, repeats by about run 5.
- **6 per kit holds novelty to about run 10–12 per class** (the reason for Q3's 6). Repeats after that are acceptable; when every entry has been seen, the least recently seen comes back first (4.5.2).
- A variant or background-boon hero draws from an effective pool of 9 (the boon's 3, then the kit's 6).
- Report `OddmentRepeatRun`.

### 8.8 Volume

**(recounted after the check:** each Oddment's name, caption and alt text are counted as separate strings, and the M3 codex is counted in full**)**

| Wave | Writing | Art |
|---|---|---|
| M2 | 13 given names (5 hero, the Shade's two kits sharing one; 8 companion); ~30 kit lines; 36 Oddments × (name, caption, alt text) = 108 strings; ~25 UI strings, including the per-camp left captions and the Grave Goods fate keys. ≈ 1.7k words | 13 trims; 36 Oddment icons and 36 cards; 6 dioramas; 6 fire-prop wrap families |
| M3 | 8 variant names, 8 given names, kit lines; 24 variant Oddments × 3 strings; background-perk boon pools (3 each); ~60 codex paragraphs ≤ 60 words (one per M2 and M3 Oddment); ~10 Lingerer lines; milestone cards; pick-order lines. ≈ 4.5k words | 8 trims; 8 overlays; 24 Oddment icons and 24 cards (plus 3 and 3 per background boon); 8 dioramas; 14 codex plates; 4 shelf bundles |
| Per new unlock class | 1–2 kits, 2 variants, 6 + 6 Oddments with names, captions, alt texts and codex paragraphs, given names, lines, V21 rows. ≈ 1.1k words | ≈ 30 pieces (icons, cards, trims, dioramas, plates) |

All text goes through the RS tone review, the EV premise test and the localisation budget (CH risk 13): whole-string keys with named placeholders and translator notes, locale quote marks inside the keys, no sentence built from fragments, +35% length, CardFit at 150%.

---

## 9. Data and validator V21 (lead's call)

### 9.1 Records

```
KitRecord {
  id, ownerKind: class|companion, classId?, creationPickId?, companionKitId?,
  sets { A: {main, off?}, B: {main, off?, fallbackOnly?} }, armour, belt[],
  coinStart?                       // required and = 15 when ownerKind == class; absent for companions
  acFloor?                         // hero kits: highest set AC at the suggested array − 2 (illustr.), >= 11
  namedPiece { slotRef, givenNameKey, fallbackOrder[] }   // display; slotRef is the shield, else the armour; fallbackOrder lists shields and armour only
  kitLineKeys { slotRef -> key }                          // display
  oddmentPoolId?                                          // display; hero kits only
  level3SpareHintKey?, level3SpareHintFamilies[]          // display; structured family ids
  accentIds { slotRef -> accentId }                       // display (art)
  lootLanes[], chassisOnly: true
}
KitVariantBoon {
  id, boonType: kitVariant, replacesKit, kit: KitRecord, skillGrant, skillAlternate, skillFallback,
  oddmentPoolId,                                          // display; pool of 3
  unlock: milestoneId, hubObjectId, channels: [boon], oncePerRun: true
}
BackgroundPerkBoon + { oddmentPoolId }                    // M3; display; pool of 3; record owned by its boon design
OddmentRecord {                                           // display domain; no rules fields
  id, poolIds[], nameKey (standalone label, no article), captionKey (<= 14 words), altTextKey,
  codexKey (M3, <= 60 words), iconId, artId, firePropId (required),
  premiseAuthor: residentTrade, strataTag?,
  tags { properNoun: false, noUsableLook, noImpliedSense, countsReviewed, noKitEcho, ... },
  retired?                                                // never deleted
}
OddmentLeftCaption { campSetId -> leftCaptionKey }        // display; one whole-string key per camp set
oddment.numberBlocklist[]                                 // display data; V21 (i)
// Core ItemInstance is unchanged; instance origin = {channel: kit, floor: 0} for every kit piece (4.1)
run.save.display.displayOverlay[instanceId] { kitId, givenNameKey?, kitLineKey?, accentId?, bynameKey?, onceCalledKey? }
run.save.display.namedPieceLog[] { instanceId, givenNameKey, fate: carried|sold|left|offered|lost|dismissed, floor }
run.save.display.oddment { heroIndex, oddmentId, poolId, state: carried|left, leftAtCampSetId? }
loreMemory.oddmentsSeen[]                                 // profile; personal runs; ordered by last sighting;
                                                          // queued as a loreSeen entry at camp-1 entry (WO4) or WO6, flushed at WO6, WO7, abandon
LoreContext + { oddmentCarried, oddmentLeftThisNight, strataTag }   // M3
```

Every display field is tagged `display` and sits outside `manifestHash`. Content fields (keys, records, pools) are in `displayHash`; the per-run sidecars (`displayOverlay`, `namedPieceLog`, `oddment`) are display-tagged runtime state in `run.save`'s display section, never content and never Core **(lead's call, after the check:** the draft wrote them as a field on the Core instance and called them content**)**. **(new lead's call)** `namedPiece.fallbackOrder[]` (SK-12's "fixed per-kit order", made a field) and `skillAlternate` (4.4) were added so that every rule is computable; **(after the check)** so were `acFloor`, `skillFallback`, `level3SpareHintFamilies[]`, `fallbackOnly`, the boons' `oddmentPoolId`, `leftCaptionKey`, `numberBlocklist`, the authored tags and the sidecars.

### 9.2 V21 `StartingKits` (registered in the consolidated suite by SK-10 ⚑)

**Static checks (s):**
- (a) `KitMundane`: VP 0, mundane, no rolled line (M1).
- (b) `KitHealing`: hero 2, recruit 1, one belt slot (M1).
- (c) `KitNoTrinket`, `KitPackEmpty` (M1). `KitCoin`: `coinStart` is required and equals 15 when `ownerKind == class`, and is absent for companions (M1, data assert only; first consumer at M2).
- (d) `KitSetLegal` (W 11.1, no stacked gear), `KitTrained` (no Strain at the suggested array), `KitTwoTypes`, `KitVerbPerSet` (a usable weapon or focus verb in each set **in its actual layout**, Nick counting only in an off hand beside a light main hand; on-hit where a class feature reads it; a `fallbackOnly` set exempt; applied to every variant set too), `KitHasFallbackDamage` (M1; would have caught F2).
- (e) `KitAcFloor`: `acFloor` ≥ 11 (AR-20); the suggested array reaches `acFloor`; over every legal arrangement (4.2), sustained AC ≥ 11 (M1).
- (f) `KitDueStrictUpgrade`: the F1 deck holds a legal U arms card for every kit (M1 if the slice includes the Due; else M2).
- (g) Names, given names, kit lines and the provenance line. **Machine rows** (fail the build): the word lists (possessive, "of", Scion rank words, theme, Bell*, idiom tag, damage and defect words on full-stat pieces); no two given names sharing a head word; ≤ 18 chars; collisions by whole word and substring (the V14 (f) pool plus Hollowfolk verbs), run on display strings and fallbacks as well as names, with the pool shared with the heirloom and curse names and the Grip-mark byname choice (M2; HL-13, NR6, heirlooms and curses design 5.3); required fields. **Flags** to the naming pass: the per-word list (property, verb, rank-word, party-counter and system-term names). **Authored tags** the tone review confirms: `properNoun: false` (M2).
- (h) `KitDisplayCoreBlind`: Core assemblies cannot reference the `displayOverlay`, `namedPieceLog` or `OddmentRecord` types. `KitDisplayInert` test: strip or randomise the display section and the content keys, and the core state hash is unchanged (M2).
- (i) `OddmentInert` (no rules fields) and text checks over Oddments, kit lines and the provenance line. **Machine rows:** premise author is a resident trade; required tags `noDreamer`, `noHeroMotive`, `noSecondPerson`, `noFallenHero`; DreamerLine and AN5 words; RC7 cues; Hollowfolk props and textures; EV card titles and props, Strand names, RS faction motifs and role props, boss signatures, trinket and property names, trinket kind words, level-up reward terms; theme names; door, route and distance words; `numberBlocklist`; money words; the kit's own families and named piece; effect verbs; no shared object noun inside a pool; the "Does nothing" line present; `firePropId` present; `OddmentPoolMin` 6 per default kit, 3 per variant or background boon. **Authored tags** the tone review confirms: `noUsableLook`, `noImpliedSense`, `countsReviewed`, `noKitEcho` (M2).
- (j) `KitNamedPieceNotF1Target` (hero kits and variants, computed over the F1 deck: for each class × creation pick × variant, no card in the F1 hero-arms candidate set (U, usable, trained) has the named piece's family; holds for every shield and armour), `KitResaleBand` (reads prices from data; a missing basePrice on any kit family fails the build), `KitLegalOverArrangements` (a warning exists for every Strain case and every AC below `acFloor`) (M2).
- (k) Variants: `KitNoDominance` (gear only, over the union of both sets), `KitVariantParity` (AC at the suggested array ≥ the replaced pick's `acFloor`), CL 6.3 ForbiddenClassGrant on the skill, skill, alternate and fallback not in the class's creation defaults and not Leadership or Mechanics, `level3SpareHintFamilies` disjoint from the kit's families and the hint text generated from them, `UnlockHasHubObject` on the Heirloom shelf (M3).
- (l) `HeirloomKitFits` (after SK-12's placement: ≥ 1 free pack slot, every unissued piece listed on the card; Arcanist staff and leather heirlooms as named rows); SK-4 heirloom cases in V13g (g), one per slot class (HL-3) (M3). **Pending request (HL-26, TK owner rules; M4):** amulet heirlooms would need an SK-12 worn-slot row and a `KitNoTrinket` waiver for the heirloom piece only (heirlooms and curses design 4.6).
- (m) Reserved: a modifier touching a kit without a declared `kitDelta` fails the build (Later).

**Tests (t):**
- `KitIssueStable` (kit issue byte-identical across reloads and replays; creation order of 4.1);
- `SeededRunOddmentReadOnly`, `ReplayOddmentMatchesSave`, `OddmentStableOnReload`, `OddmentRetiredLoads`, `LeaveWritesNoCommand` (with the carried and left fixtures of 4.5.2's state table);
- `AbandonWritesNoLore` extended to `oddmentsSeen`, including "start, quit, reload, abandon on F1 → `oddmentsSeen` unchanged";
- `VariantPickConsistent`;
- `CompanionNameOnIssuedPiece`;
- `Floor1ClassSpread`, `Floor1BossSustain` and DH 18.2 F1 deaths per class × pick × variant × worst-legal-arrangement, with the 4.4 risk band (`VariantRiskBand`);
- the novice `SkillExpressionReport` row for Arcanist variants (ARC-29).

**Reports (r; analytics, outside the gates):** `KitSoldAtCamp1` (named piece and other pieces split), `VariantPickPerOffer`, `NamedPieceHeldAtF3` (owned by the party at F3 entry, any location), `OddmentRepeatRun`, `OddmentLeftRate`, `OddmentReadRate`, `OddmentMisuseRate` (drag or use attempts; target < 5% after the callout), F1 hero deaths by kit, creation time on run 1, and the playtest question "did the Oddment do anything?". Every kit report uses 4.1's hero-kit predicate, with the kit id read from the display sidecar.

### 9.3 Tooling

- A kit sheet generates `KitRecord`s and the kit-strip text.
- A name linter runs the machine rows of (g) and (i) in the writers' tool before review; the authored tags go to the tone review.
- A matrix runner dedupes simulator arms by rules hash.

---

## 10. Staging by wave

| Wave | Content |
|---|---|
| **M1** | Vanguard default kit (approved contents); coin exactly 15 (SK-1); hero kit-issue row with the creation order and `KitIssueStable` (SK-3); V21 (a)–(e), plus (f) if the Due is in the slice, registered by SK-10; display fields reserved and unused; kit strip for the one class if creation exists in the slice |
| **M2** | All six default kits; SK-2 javelin fix and HR 7.12 rerun; given names and kit lines on hero and companion kits, with the display sidecar and the named-piece log; trims; Oddments (36, 6 per kit; agreed, Q3) with `oddmentsSeen` and the display tuple; Leave it by the fire; Grave Goods line; kit strip, run-1 form and callout; creation warnings; Arcanist pack-pressure UI; V21 (g)–(j); `KitSoldAtCamp1` (agreed, Q4); SK-11 decision by LE |
| **M3** | 8 variant boons (Arcanist pair gated) with skills and alternates (agreed, Q1), milestones and Heirloom-shelf bundles; per-offer pick-rate report; SK-4 (with HL-3's any-slot waiver) and SK-12 before heirlooms ship; the `BossFelled` credit for kit pieces (HL-31); Deed bynames on kit items; codex Oddments page and plates (agreed, Q3); LoreContext facts and Lingerer lines; Memento frame tag; background-perk boons with Oddment pools (3 each); V21 (k)–(l) |
| **M4** | Third variant per class (F4-boss milestone); Arcanist variants if delayed; Vanguard "Long Arm"; species-flavoured Oddments; unlock-class kit packages; an SK-12 worn-slot row for amulet heirlooms if the TK owner agrees (HL-26, pending) |
| **Later** | Companion Oddments; F5 Keepsake prop; player-chosen labels (UI only); daily seeds that fix the kit; `kitDelta` modifiers (Oaths, HL-21, pending) |

---

## 11. Ownership and hooks

| Owner | What this design needs from them (generated from 13) |
|---|---|
| VG, SH, ARC, HR owners | SK-1 coin; SK-2 javelin (HR); `level3SpareHint` per kit (SK-7); SK-9 creation warnings |
| CMP | Named piece per kit; upgrades never swap it (SK-5); SK-3 Old Friend issue (CMP 12) |
| LE (DH confirms) | SK-1 (LE 3.1); SK-4 precedence and tie-break; SK-11 price ledger rerun; SK-17 stale Sell rate; kit coin as `CoinChanged{source: kit}` in `LootLedger` (SK-3) |
| ITEMS | SK-3 issue row, creation order and `KitRecord`; SK-5 (the Core instance is unchanged; the display sidecar lives in SV's `run.save` display section); SK-11 basePrice scale; SK-12 heirloom placement |
| IR | SK-5 given-name line in Whence; byname displaces it; V14 (f) pool shared |
| CH (codex: RS) | SK-6 tuple, write timing, LoreContext facts, frame tag, per-camp left captions; SK-8 (CH 4.9 wording); SK-9 onboarding |
| SV | SK-5 and SK-6 display sidecars in `run.save`; SK-6 display row and the creation-screen run key; SK-10 suite; SK-16 shared-seed boons |
| RS | SK-10 suite row; SK-14 premise row; tone review; codex hosting of the Oddments page |
| MAIN/RS | SK-15 `kitDelta` (MAIN 4.7 is read only, by SK-7; no wording change) |
| ABL, STAT | SK-17 stale wording; STAT signs off bundled skills (SK-7) |
| XI / segment 10 | SK-9 kit strip, callout, `uiMemory`, accessibility |
| W | Hand crossbow two-light; mundane orb and tome bases (requests) |
| Naming pass | SK-13 "kit", "Oddment", "Leave it by the fire" and every placeholder; the A.2 flags |

---

## 12. Binding requirements and how they are met

| # | Requirement | Met by |
|---|---|---|
| 1 | Enough to start a run, until the player can keep going (brief) | Approved package unchanged; exact coin (SK-1); empty belt slot; the Due as the hand-off (4.2, 4.3) |
| 2 | Thematic match to source (brief) | Per-class kits, named pieces, kit lines and Oddment pools written per kit (5) |
| 3 | Client question 1, lore object | The Oddment (4.5.2; agreed, Q3) |
| 4 | Client question 2, renamed items | Given names on the piece the F1 Due cannot replace (the shield, else the armour), plus earned Deed bynames (4.5.1; agreed, Q2) |
| 5 | MAIN 4.7 one boon, no stacking, no permanent boosts (decided) | Variants are boons; 0 VP; skills bounded (4.4; agreed, Q1) |
| 6 | CH-24 kit variants only as boons; no hub peg | 4.4; Heirloom shelf, no coat tag |
| 7 | CH Q2 no mechanical or informational effect (agreed) | Oddment, names and lines are display-only, with text bans (V21 (h)–(i)) |
| 8 | DH client potion numbers | 2 / 1 unchanged (V21 (b)) |
| 9 | F1 Due strict upgrade; LE 6.5 VP 0 | Mundane kits (V21 (a), (f)); SK-4 |
| 10 | Floor1ClassSpread ≤ 10 (client); F1 deaths < 5% (agreed) | Per-row reruns; variant risk band; worst-arrangement arm |
| 11 | One choice per level (CL 7.4) | No new level-1 pick (4.1) |
| 12 | RC1, RC9, IR 6.1 | Family word leads; bynames stay Deed-only |
| 13 | RS 7.5 open motives; EV premise test | Resident-trade authors; `noHeroMotive`, `noFallenHero` |
| 14 | SV key and SV Q1 | Display outside the key; seeded runs write nothing and advance no milestone; replays read the save |
| 15 | CH `AbandonWritesNoLore`, AN1, AN5, AN9, AN11 | 4.5.2 write timing (queued at camp-1 entry or WO6, flushed only by SV's flushes) and text bans |
| 16 | Accessibility and localisation (CH 4.13, XI, CH risk 13) | Text chips and labels, alt text, glyph and pattern, controller parity; whole-string keys with placeholders; never quotes alone |
| 17 | Combat is key | Kit verbs per set; the Due upgrade moment; names earned by felling bosses |
| 18 | Hero kit gear sellable (agreed, Q4) | ITEMS 10.6 unchanged; `KitSoldAtCamp1`; `kitIssue` held as a lever only |
| 19 | Every kit legal, trained and verb-bearing in each set (new lead's call) | V21 (d), (j), (k) with `KitVerbPerSet` |

---

## 13. Changes to approved documents

Applied on 2026-11-23, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; as in the LE, IR, EV, BR, XI, SV and CH precedents, the Flag column also marks rows that only *read* such text without changing it, so that every touch of it is visible **(new lead's call:** read flags added to SK-2, SK-3, SK-5, SK-7, SK-9, SK-10, SK-12, SK-16 and SK-17, which the proposal's register did not flag**)**. **The client's approval of 2026-11-23 covers the flagged rows** (with Q1–Q4 where a row names one). Told-plainly rows were told in 14.1 and, for those found in the check, 14.3, and confirmed by the client on 2026-11-23. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; recorded as readings, approved by the client with this document on 2026-11-23; each owner confirms its own reading):**
- Hero kit instances carry no `boundToUnitId` and sell at the Sell rate (ITEMS 10.6, 18.1; agreed, Q4).
- LE 6.6's "sold or dropped at this camp count as still dealt" covers sold kit pieces (LE and DH owners; pending DH as LE states).
- VG 7.10's "on-hit, payable line in each set" is a Vanguard requirement, not a rule for every kit (VG owner; F10).
- CH 4.11's "kit tag in the Veteran book" is the companion-kit unlock object (CH owner).
- MAIN 4.7's CH-24 sentence ("Starting kits get no hub peg: kit variants ship only as boon options") is read as written; only CH 4.9 says "fixed per class" (SK-8; corrected after the check).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| SK-1 | VG 7.10, SH 7.10, ARC 7.11, HR 7.11 (lead's calls, settled); ITEMS 17 row 3 (settled); LE 3.1 (agreed parent summary) | Coin "about 15" → **exactly 15** (`kit.coinStart`); DH 21.5 and LE 6.4 item 4 depend on it (2.3 F1; 4.2) | ⚑ Settled (lead's calls with load-bearing arithmetic); ⚑ Agreed (LE 3.1, read) | Lead's call; told plainly; class owners, ITEMS and LE owners apply | M1 |
| SK-2 | HR 7.2 (line 876), HR 7.7 kit-pick table (lines 1011, 1012), HR 7.11 kit table (lines 1147, 1148) (lead's calls, settled); HR 7.12 | Set B "2 javelins" → **1 javelin** (W 11.1, ITEMS 12.8, W 9.3 (settled, ITEMS 17)); rerun `Floor1BossSustain` and HR 7.12 (2.3 F2) | ⚑ Settled (read flag added, new lead's call) | Lead's call; told plainly; HR owner applies | M2 |
| SK-3 | ITEMS 17 row 3, ITEMS 18.1 location-transition table (settled); CMP 12 Old Friend | `KitRecord` shape; the 18.1 row rewritten as: "kit issue → the `KitRecord.slotRef` locations (weaponSet active/spare main/off, armour, belt 0) \| run start, WO1, before F1 generation; on hire; Old Friend at run start \| `ItemCreated{kit}`, `ItemEquipped`"; creation order set A main, set A off, set B main, set B off, armour, potions one by one into belt 0 (2 instances, one stack); `origin = {kit, floor 0}`, `everHeld = true` for every instance issued from a `KitRecord`; a piece SK-12 does not issue consumes no `instanceId`; kit coin as `CoinChanged{source: kit}`; no `boundToUnitId` on hero pieces; hero kits explicitly have no trinket (4.1, 4.2, 7) (rewritten after the check) | ⚑ Settled (read flag added, new lead's call) | Lead's call; ITEMS and CMP owners apply | M1 |
| SK-4 | **LE 6.6 item 2 (fixed deal; F1 hero-arms rule; waiver) (lead's call, agreed register)**; V13g (g) | Tie-break on equal VP takes the higher tier; heirloom arms (U or better) count as satisfied and the F1 Due waives to 20 coin; no retarget; named heirloom case in V13g (g) (4.3) | ⚑ Settled; "The Due never retargets" kept | Lead's call; LE owner applies, DH and IR owners confirm | M3 |
| SK-5 | ITEMS 12.3 `display` (settled); **IR 6.1 Whence (lead's call, read and extended)**; V14 (f) | Given name as the named piece's Whence line (the shield, else the armour); kit lines and the neutral "Carried from the start" provenance; a byname displaces a given name ("Once called"); name pool extended; companion kit named pieces; the per-instance overlay and the named-piece log as display sidecars in `run.save`'s display section (SV 4.8.3 `run.save` row, read), with no field added to the Core `ItemInstance` (4.5.1) (after the check) | ⚑ Settled (IR 6.1 Deed-only rule kept, read flag added, new lead's call) | **Client (Q2, with this document)**; ITEMS, IR and CMP owners apply | M2 |
| SK-6 | CH 4.15.1, 4.15.8, 4.16, 9.1 `loreMemory`, `AbandonWritesNoLore`, Memento wall (CH 4.10); **CH-8 and SV 4.3 display table (agreed)**; DH 17.12, LE 8.2 Grave Goods | The Oddment: record, pools, the run-start display tuple `DisplayKey(run)` / `lore` / (campIndex 0, visitIndex 0, "oddment", heroIndex, poolId) with no `nightId`, selection from the creation-screen run key frozen at WO1, the mark queued as a `loreSeen` entry at camp-1 entry or WO6 and flushed at WO6, WO7, abandon, Leave it by the fire and its states, Grave Goods lines, per-camp left captions, LoreContext facts, codex page, frame tag (4.5.2) (tuple and write timing restated after the check) | ⚑ **Agreed** (CH-8 determinism, extended); ⚑ Settled (DH 17.12, LE 8.2) | **Client (Q3, with this document)**; CH, SV, DH, LE owners apply | M2 / M3 |
| SK-7 | **MAIN 4.7 (decided, read)**; **STAT 7.2 (agreed, read)**; ITEMS 14 (parallel); SV-12 (read); CL 5.6 rows; CH 4.11; VG 7.10 level-3 suggestion | Variant boons with a bundled skill, alternate and `skillFallback`, resolved at creation confirm; the pick-order rule; Heirloom-shelf hub object; per-offer pick-rate trigger; `level3SpareHint` per kit with structured families (4.4) | ⚑ **Decided** (read); ⚑ **Agreed** (read) — read flags added (new lead's call) | **Client (Q1, with this document)**; STAT owner signs off skills; CH, CL, VG owners apply | M3 |
| SK-8 | **CH 4.9 (settled; told plainly)** | "Fixed per class" → "one default kit per class creation pick (Shade Blades or Launcher, Herald Shield or Totem); no other presets; variants ship only as boon options". Meaning unchanged. MAIN 4.7 and CH-24 already read correctly and are not touched (2.3 F3; corrected after the check) | ⚑ **Settled** (CH 4.9; told plainly) | Lead's call; CH owner applies | M2 |
| SK-9 | CH 4.12 (settled); XI 4.10 `uiMemory` (settled); STAT 5.1 (agreed, read) | Kit strip (run-1 and full forms), Oddment callout, creation warnings for Strain and AC below `kit.acFloor` (never blocking), worst-arrangement arm, the variant pick-order line (4.4, 4.6) | ⚑ Settled; ⚑ Agreed (STAT 5.1, read) — read flags added (new lead's call) | Lead's call; CH, XI owners apply; class owners inform | M2 |
| SK-10 | **RS 11.2 validator suite table (settled)**; **SV 5.2 consolidated suite and build dependency graph (settled)** | RS 11.2 gains a V21 `StartingKits` row (s/t/r split; M1 (a)–(f), M2 (g)–(j), M3 (k)–(l)); SV 5.2's consolidated suite becomes V1–V21; static and display-only parts as V20 | ⚑ Settled (read flag added, new lead's call) | Lead's call; RS and SV owners apply | M1 |
| SK-11 | ITEMS 10.5 (settled); LE 7 ledger (request) | Mundane basePrice scale (8.4), with a coverage rerun at 250‰ and 200‰; adoption by owners | — | Request; ITEMS and LE owners rule | M2 |
| SK-12 | ITEMS 14, 18.1 (settled) | Heirloom placement at issue: the heirloom's slot class in set A (a two-hander takes main and off); displaced pieces to the pack in issue order only while ≥ 1 slot stays free; the given name moves by `fallbackOrder` only when the named piece is not issued, suppressing the receiver's kit line; `HeirloomKitFits` after placement (7) (rewritten after the check) | ⚑ Settled (read flag added, new lead's call) | Lead's call; ITEMS owner applies | M3 |
| SK-13 | Naming pass (request) | Player-facing term for hero starting gear distinct from the companion "kit"; chip never matches kitBound's "Unsellable: kit" label (2.3 F8) | — | Request; naming pass | M2 |
| SK-14 | RS 7.5 premise table (request) | Additive row: "Kits? The dream issues what its residents set aside." | ⚑ Agreed (RS Q3, read) | Request; RS owner rules | M2 |
| SK-15 | MAIN 4.7 run-modifier slot (request) | `kitDelta` declaration rule; reserved V21 (m) (4.7) | ⚑ Decided (read) | Request; main design and RS owners rule | Later |
| SK-16 | SV 4.7 (request); **SV Q1 (agreed, read)**; LE 6.12 | Shared seeds and variant boons; recommended: variant boons are replaced by the default kit for the chosen creation pick; other boons follow SV's existing canonical-grant rule (4.7) | ⚑ Agreed (read flag added, new lead's call) | Request; SV owner rules | M3 |
| SK-17 | LE 3.1 (Sell 400‰; **agreed flat rate kept**); ABL 20 (line 1371); STAT 10 (line 570) | Stale text: Sell 400‰ → 250‰; "starting kits … move to the category designs" → "starting kits are set in the four class designs, the companions design and the starting kits design" (2.3 F9) | ⚑ Agreed (the flat rate, read) — read flag added (new lead's call) | Lead's call; LE, ABL, STAT owners apply | M2 |

The options the client rejected (Q1-B and C, Q2-B and C, Q3-B, Q4-B) are not register rows.

---

## 14. Open items for the client

None. Approved by the client on 2026-11-23.

The client approved this document as a whole on 2026-11-23, including the lead's answers of 0.1 and Model A with its grafts (which the client had accepted by proceeding); every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the 26 lead's calls after the check in 14.3); the told-plainly items (those in 14.1 and "Told plainly after the check" T1–T12 in 14.3, all as written); and the readings recorded in 13; and its approval covers the flagged rows of 13. The four questions raised before writing were answered on 2026-11-23 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. Writing raised **no new client question**: every new lead's call in 14.1 sits inside the four answers, corrects the proposal's own text against an approved rule, or is flagged against the text it touches in 13. The design check raised none either (14.3). The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them (including the owner-refusal fallbacks and the report-triggered items), and the naming pass remain future items.

### 14.1 This document

**The four answers (agreed, 2026-11-23), each the recommended option:**
- **Q1, kit variants (SK-7):** option A, boon options as CH-24 decides, each a re-dressed mundane kit bundled with one skill training; a per-offer pick-rate report, revisited only if a variant is taken at under half its fair share over 2 playtest rounds. Rejected: B, a hidden defaulted Kit row on the class card; C, boons with one Common notable.
- **Q2, renamed items (SK-5):** option A, one given name per kit on the piece the F1 Due cannot replace, shown in the Whence pane with the family word kept as the card title; the kit weapon earns a proper name only through a Deed byname. Rejected: B, the name on the sword; C, a title rename.
- **Q3, Oddments (SK-6):** option A, ship in M2 with 6 per kit; the codex Oddments page in M3. Rejected: B, everything in M3.
- **Q4, hero kit gear (4.3):** option A, stays sellable (the approved asymmetry); report `KitSoldAtCamp1`. Rejected: B, 0 Sell through `kitIssue` (held as a lever).

**The lead's answers (0.1) and Model A with its grafts (3.2)** were accepted by the client by proceeding and confirmed by the client on 2026-11-23, with this document, including every told-plainly item below.

**Told plainly (confirmed by the client on 2026-11-23, with this document):**
1. Kits are not made stronger: no extra potions, no kit trinket, no non-mundane item, no extra coin, no free dose (0.1 item 1).
2. The client's sword example is contested: the given name goes on the piece the F1 Due cannot replace, because the sword is usually replaced within 15 to 25 minutes (0.1 item 4; agreed, Q2). After the check, that piece is the shield where the kit has one, otherwise the armour (14.3, T1).
3. Class-free background kits, Liveries (Model B) and Haversacks (Model C) are rejected; the background perk survives as a boon with its own Oddment pool (0.1 item 2, 3).
4. There is no real weightless inventory item and no letter or note; the Oddment is a display object (0.1 item 3).
5. The same seed shows the same Oddment, but a text patch can change it (4.5.2).
6. Seeded runs never advance milestones (4.4, 4.7; SV Q1).
7. Coin is exactly 15, not "about 15": the camp-1 margin is 0 (2.3 F1; SK-1).
8. The Herald's set B is 1 javelin; "2 javelins" is illegal under W 11.1 and the no-stack rule (2.3 F2; SK-2).
9. Hero kit gear stays sellable, an asymmetry with kitBound recruit gear kept on purpose (agreed, Q4).
10. Variants are expected to lose often to an Uncommon heirloom, Old Friend or the Wayfarer's Pack (0.1 item 6).
11. Starting kits get no hub peg and no coat tag; the variant's hub object is the Heirloom-shelf bundle (4.4).
12. "Set it down" is rejected (AN5); the action is Leave it by the fire (4.5.2).
13. The Vanguard's named shield may go at the F2 or F4 Due; Grave Goods records what became of it, from the named-piece log (8.5, 4.5.1).
14. SK-16 (shared seeds and variant boons) is undecided and goes to the SV owner (4.7).
15. The research is partly from recall and must be checked before any claim is quoted (1; Appendix B).

**New lead's calls (made while writing; confirmed by the client on 2026-11-23, with this document):**
1. **The verb-per-set rule (F10).** V21 (d) checks a usable weapon or focus verb in each set, and an on-hit verb only where a class feature reads it (the Vanguard's Practised Hand). The proposal's "an on-hit verb in each set" would fail the approved Arcanist (Parry) and Launcher (Aim) kits (4.2, 9.2). *Corrected after the check:* Nick does nothing on the Arcanist's lone set-B dagger; that set is exempt as a declared fallback (14.3 item 7).
2. **The survivor-piece slot (F11).** *Superseded after the check (14.3 item 1):* the writing-time reading "not the set-A main hand" did not hold; the named piece is the shield, else the armour. Kept from writing: the check applies to hero kits and variants; companion kits are exempt (4.5.1).
3. **The Arcanist variants name their leather (F11).** Farcaster's and Bookhand's proposed named staff is a focus, which the survivor rule forbids; they have no shield, so the leather is named. *After the check* this is simply the general rule (14.3 item 1), and their set B is now a sling (14.3 item 10) (5.3).
4. **The variant skill rule and four corrected skills (F12).** "Already has" means the class's creation default skills; each variant names an alternate, granted if the player already trained the skill. Illustrative skills corrected: Maulhand Athletics → Intimidation; Throwhand "Stealth or Perception" → Athletics; Bookhand Medicine → Arcana; Spearhand Medicine → Lore. Alternates are listed in 5.3 (4.4, 5.3, 9.1 `skillAlternate`).
5. **Spearhand's losses corrected.** Both its set B (sling) and the default's (javelin) are AC 14, so "set B AC" is not a loss; it loses Parry, d8 → d6 and the javelin's Pry (5.3).
6. **Price-scale rows completed.** The mace joins the simple row and one-handed launchers join the launcher row in SK-11's proposed scale; whole-kit and variant resale computed; all variants within `KitResaleBand` (8.4).
7. **Name near-collisions flagged to the naming pass,** with a per-word check added to V21 (g) as a flag, not a failure (4.5.1; A.2). The names stand as placeholders.
8. **Four Oddment samples got a named tone-review look** (the cork, the list of toasts, the cue card and blank speech, the ribbon rosette). *After the check,* three of them were re-authored with the other failing samples; the ribbon rosette keeps its second look (14.3 item 19).
9. **The usher's thread** kit line corrected to "an usher's thread" (grammar only) (5.1).
10. **Selling kit gear cannot steer the Due:** LE 6.6's "sold at this camp counts as still dealt" covers kit pieces (a reading, 4.3, 13).
11. **The Veteran book's kit tag** stays the companion-kit unlock object; hero variants use the Heirloom-shelf bundle only (4.4).
12. **Fields made explicit:** `namedPiece.fallbackOrder[]` (SK-12's fixed order) and `KitVariantBoon.skillAlternate` (9.1).
13. **0.4 Intent, goals and anti-goals** added in the template's shape; **12** numbered and extended (items 18 and 19); **13** given the Flag and Approval columns in the CH shape, with read flags added to SK-2, SK-3, SK-5, SK-7, SK-9, SK-10, SK-12, SK-16 and SK-17.
14. **Stats checked against the corpus** (5): every kit item matches W 17, AR 13.1 and the class kit sections; the only discrepancy is F2. Small precisions added from those sources: the dagger's thrown 5 and Nick on the Arcanist's dagger, the hand crossbow's range 6, the versatile die's empty-off-hand condition, the chain shirt's Dex cap.

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a lead's call (confirmed by the client on 2026-11-23, with this document) unless marked.

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| Variants rarely picked | `VariantPickPerOffer` under 0.5 × fair share over 2 playtest rounds with ≥ 30 offers each *(illustr.)* | Q1's rejected option B (a defaulted Kit row on the class card) comes back to the client as a CH-24 revisit; nothing changes without the client | Client |
| Kit sales distort camp 1 | `KitSoldAtCamp1` shows coverage moved beyond LE 6.4's band | The held `kitIssue` 0-Sell lever goes to the client (Q4-B); never applied without the client | Client; LE owner |
| Named piece rarely held | `NamedPieceHeldAtF3` low after 2 playtest rounds | On a shield kit, the given name moves to the kit's armour (the fallback); on an armour kit the pick stands and the report is read with the F2–F3 Due data | Lead |
| F1 solo deaths high for a kit | DH 18.2 rows by kit above 5% | The held class-themed C dose in the empty belt slot, carved from F1 sundries, goes to the DH and LE owners | Lead; DH, LE owners |
| Variant outside the risk band | `VariantRiskBand` fails | The variant row is cut or re-dressed; the band never widens | Lead |
| Oddment read as a buff | `OddmentMisuseRate` ≥ 5% after the callout, or the playtest question | The card text and placement are revised; no effect is ever added | Lead |
| Price scale | ITEMS and LE owners rule on SK-11 | Until adopted, the 8.4 scale stays in data labelled `placeholder` (a missing basePrice fails the build from M2); fallback: owners' own prices, `KitResaleBand` re-run | ITEMS, LE owners |
| Heirloom precedence refused | LE or DH owner refuses SK-4 | The tie-break gap remains; heirlooms ship only with a named V13g (g) case agreed by LE | LE owner (DH, IR confirm) |
| Any-slot heirloom waiver refused | LE owner refuses HL-3, or DH, IR or SK confirmations stay open at M3 | No armour or shield heirloom templates at M3 (three weapon or focus families replace them); heirlooms do not ship until the confirmations close (heirlooms and curses design 4.1.4, 14.2) | LE owner (DH, IR, SK confirm) |
| Worn-piece bynames | IR or CMP owners refuse a Deed byname for a shield or armour credited by `BossFelled` (HL-31; SK confirms) | The bequest and its Grip-mark carry boss and theme marks only; the given name is unaffected | IR, CMP owners; SK confirms |
| Shared seeds and variant boons | SV owner rules on SK-16 | Fallback: (a), variant boons replaced by the default kit for the chosen creation pick, other boons by SV's canonical-grant rule, stated on the seeded entry screen | SV owner |
| Premise row | RS owner rules on SK-14 | Fallback: no premise row; Oddment writing and the neutral provenance line unchanged | RS owner |
| `kitDelta` modifiers | MAIN/RS run-modifier decision (SK-15) | Reserved V21 (m) stays unused until then | Main design, RS owners |
| Arcanist variants | W owner confirms mundane orb and tome bases | Fallback: Farcaster and Bookhand move to M4 | W owner |
| Hand crossbow | W owner confirms loading and two-light legality | Fallback: Close Shot's set A becomes hand crossbow alone; re-run `KitNoDominance` | W owner |
| Display sidecars, creation-screen run key | SV owner declines the `run.save` display sidecars or a run key before WO1 (SK-5, SK-6) | The overlay and log ride CH-9's `loreRunState` record instead; the creation card omits the Oddment line and the Oddment is selected at WO1 | Lead; SV owner |
| Bundled skills | STAT owner signs off (SK-7) | Fallback: variants ship without a skill only if the client agrees (they would likely fail the pick-rate trigger) | STAT owner; client |
| Memento frame tag, Lingerer lines | CH's `OneWinOwnerPerNight` or AN7 budgets refuse them | First cut: the frame tag; second: the Lingerer lines | Lead; CH owner |
| Naming pass | Scheduled | Every placeholder, incl. the A.2 flags and SK-13 | Naming pass |

### 14.3 After the design check

The design check (builder, consistency, balance and narrative-art lenses) found that the named piece for half the kits could be replaced by the F1 Due, that kit issue would have filled the pack, that the display data sat on the Core instance, and a set of gaps in which this design broke its own rules. Every fix below sits inside the four agreed answers and the proposal the client confirmed; **none overrides a client decision**: kits are not made stronger, the given name goes on the piece the F1 Due cannot replace (Q2), Oddments ship in M2 with 6 per kit (Q3), and hero kit gear stays sellable at the Sell rate (Q4). The check raised **no client question**.

**Lead's calls after the check (confirmed by the client on 2026-11-23, with this document):**
1. **The named piece is the shield where the kit has one, otherwise the armour.** The F1 Due is dealt by slot, never by weapon set, and is always a weapon or focus, so only shields and armour are out of its reach (LE 6.6). The Shade (both picks), the Arcanist, the Herald Totem, Close Shot and Throwhand now name their armour: placeholders Grey Lacing (the Shade's shared leather), Felt Elbows, Copper Links. Thin Edge, String Cutter and Straight Shaft are retired and their pieces get kit lines. `KitNamedPieceNotF1Target` is computed over the F1 deck (4.5.1, 5.1, 5.3; V21 (j)).
2. **Kit issue defined:** straight to the `KitRecord` slots at WO1, before F1 generation; a fixed creation order; 2 potion instances in one stack; `origin = {kit, floor 0}`, `everHeld = true`; kit coin as `CoinChanged{source: kit}`; `KitIssueStable` (4.1, 7; SK-3).
3. **One hero-kit predicate,** `origin.channel == kit && boundToUnitId == null`, for every instance issued from a `KitRecord` (variants and Old Friend included); the chip, the reports and the Arcanist pack-pressure option use it (4.1).
4. **The display sidecar:** the given name, kit line, accent and byname keys live in `run.save.display.displayOverlay[instanceId]`, updated only by reacting to Core events; the Core `ItemInstance` is unchanged; V21 (h) restated (4.5.1, 9.1; SK-5).
5. **The named-piece log** records each named piece's fate (carried, sold, left, offered, lost, dismissed); "replaced" means any fate other than carried; Grave Goods reads it (4.5.1).
6. **Sell, Swap/Leave and Curiosity rows** show the "Starting gear" chip, and for the named piece one inline line, "Sold as plain gear. Its name goes with it."; no price change, no block; `KitSoldAtCamp1` split (4.3).
7. **The verb-per-set rule reads each set's actual layout;** a fallback-only set (the Arcanist's lone dagger) is exempt and checked by `KitHasFallbackDamage` (2.3 F10, 4.2; V21 (d)).
8. **`kit.acFloor`**, one number per class and creation pick, drives the creation warnings, variant parity and V21 (e); legal arrangements are the permutations of the pick's suggested array, plus species later (4.2, 4.4, 4.6).
9. **Variant pick order:** the shelf pre-selects class and creation pick; a later change shows one line and, on confirm, clears the variant and reopens the boon picker; the skill is resolved at creation confirm with `skillFallback`; an offer is an eligible shelf view; `VariantPickConsistent` (4.4).
10. **`KitNoDominance` compares the union of both sets.** Farcaster and Bookhand are re-dressed with a sling in set B (no staff); Maulhand's losses corrected; "in the opener" struck (4.4, 5.3).
11. **The risk band in death-rate terms:** Vanguard [default, +3], Shade [−3, default], Arcanist and Herald ±3, all under 5% (4.4).
12. **Heirloom placement:** the heirloom's slot class in set A (a two-hander takes both hands); displaced pieces to the pack only while 1 slot stays free; the name moves only when the named piece is not issued, to a shield or armour only, suppressing the receiver's kit line; `HeirloomKitFits` after placement, with Arcanist rows (7; SK-12; V21 (l)).
13. **One Oddment selection formula:** `DisplayKey(run)` / `lore` / (campIndex 0, visitIndex 0, "oddment", heroIndex, poolId), no `nightId`; effective pools (the boon's 3, then the kit's 6); least recently seen after all are seen; computed on the creation screen and frozen at WO1; retired records never deleted (4.5.2; SK-6).
14. **Memory write timing:** the mark is queued at camp-1 entry (WO4) or WO6, never at WO1, and flushed only by SV's existing flushes; `AbandonWritesNoLore` gains the quit-reload-abandon case (4.5.2).
15. **Leave it by the fire:** only from the Fire station's Oddment card; undo until the station closes; final for the run; rides the next `run.save` rewrite; the carried and left states defined on every surface; per-camp captions, never ordinals (4.5.2).
16. **Localisable strings:** Oddment names are standalone labels without articles, shown in list layouts; every surface is one whole-string key with named placeholders; quote marks live in the keys; the given name is shown with the "called" word (4.5.1, 4.5.2, 5.4, 8.8).
17. **The provenance line** is the neutral "Carried from the start", replacing "Issued at the Stairtop" (4.5.1).
18. **Writing rules widened** to kit lines and the provenance line, and to named content from other designs (EV cards and props, Strands, RS motifs and role props, boss signatures, trinket and property names, trinket kind words, level-up reward terms); a number blocklist; money, receipts and kit echoes banned; pool rules; machine rows split from authored tags (4.5.2; V21 (g), (i)).
19. **Samples re-authored in place,** pools kept at 6: Vanguard 1, 3, 4, 5, 6 (the tin tag, the scored strip, the receipt, the rivet, the gatehouse rota); Shade Blades 2, 3, 4, 5; Shade Launcher 2–6; Arcanist 1, 3, 4, 5; Herald Shield 1, 4, 5, 6; Herald Totem 1, 2, 4, 5. Kit lines: the Shade shortsword ("filed short"), the Vanguard chain mail, the Launcher longbow (no "knot"). Companion names: Split Haft → Birch Handle, Bent Tip → Brass Pommel, Painted Shaft → Ochre Daub. The running example is now the porter's armband (5.1, 5.2).
20. **Name rules:** no damage or defect words on full-stat pieces; no two given names sharing a head word; "no proper noun" is an authored tag (4.5.1).
21. **Structured level-3 hint families** beside the key (4.4, 9.1).
22. **`coinStart` is hero-only;** the strip's supply text comes from the ABL constant (4.2, 9.1; V21 (c)).
23. **A missing basePrice on a kit family fails the build from M2;** the 8.4 scale stands as labelled placeholder data until SK-11 is ruled (4.3, 8.4).
24. **SK-16's fallback** reads "variant boons are replaced by the default kit for the chosen creation pick; other boons follow SV's existing canonical-grant rule" (0.2, 4.7).
25. **Corrections:** F3 and SK-8 limited to CH 4.9 (MAIN 4.7 read only); W 9.4 → W 9.3; the d8 DPR rows; the EHP figures; the hand crossbow "to be confirmed"; Thin Edge's W 18.2 "Edge" flag noted on retirement; the volume, trim, icon, card and plate counts; the six fire-prop wrap families with a required `firePropId` (2.3, 5.3, 6, 8.2, 8.3, 8.8, 13, A.2).
26. **Register and bookkeeping:** SK-2, SK-3, SK-5, SK-6, SK-7, SK-8, SK-9, SK-12 and SK-16 restated; 11 and 17 regenerated from 13; a 14.2 row for the SV sidecars; new names checked (A.2).

**Told plainly after the check** (none overrides a client answer; confirmed by the client on 2026-11-23, all as written):
- **T1.** **Half the draft's named pieces did not meet Q2.** The F1 Due could replace the Shade's sabre, the Arcanist's dagger and the Herald Totem's javelin. Those kits now name their armour, so Q2's "the piece the F1 Due cannot replace" is true by rule. Armour usually lasts to the F2 or F3 Due, so those names may leave sooner than a shield's.
- **T2.** **Farcaster and Bookhand were stronger than the default Arcanist kit,** because the set a hero opens with is never a cost (sets swap free outside combat). They now give up the staff, and with it Parry and Steady Casting, for a sling. They may fail the risk band and be cut.
- **T3.** **The approved Arcanist's set-B dagger has no working verb:** Nick needs an off hand. The kit is unchanged; that set is exempt as the class's declared fallback.
- **T4.** **Quitting at camp right after Leave it by the fire can undo the Leave,** because SV has no write on quitting. On reload the Oddment is carried again. Once written, leaving is final for the run.
- **T5.** **About a third of the confirmed Oddment samples, three kit lines and three companion names broke this design's own rules** and were rewritten. The running example "a rivet wrapped in cloth" is now "a porter's armband". Pools stay at 6.
- **T6.** **"Issued at the Stairtop" is replaced by "Carried from the start".** The old line asserted a premise the RS owner has not ruled on.
- **T7.** **Several draft numbers were wrong:** the d8 DPR rows (4.4 → 5.1), the Vanguard EHP (111 and 87 → 91 and 71; 1.9× → 1.6×) and the writing volume (M3 ≈ 3.5k → ≈ 4.5k words; about 6k in all).
- **T8.** **Some checks the draft promised cannot be automated** ("no proper noun", "counts that match game numbers", "usable-looking", implied senses). They are now authored tags confirmed by the tone review, with explicit blocklists for what a machine can check.
- **T9.** **Hero kit gear stays sellable (Q4).** The Sell row now shows the "Starting gear" chip and, for the named piece, a line saying its name goes with it.
- **T10.** **A variant or background-boon hero draws its Oddment from 9** (the boon's 3 first, then the kit's 6), not from 3.
- **T11.** **The F3 finding misquoted CH-24.** Only CH 4.9 says "fixed per class"; MAIN 4.7 needs no change.
- **T12.** **Two requests are still pending:** the W owner's confirmation of the hand crossbow and of mundane orb and tome bases. The draft called the first confirmed; it is not.

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | Variant boons rarely picked against heirlooms, Old Friend and the Wayfarer's Pack, even with a skill | Per-offer reporting and the Q1 trigger |
| 2 | Bundled skills are power-adjacent | Mechanics and Leadership excluded; class defaults excluded with an alternate; CL 6.3 applies; boon value compared with a heirloom's; STAT owner signs off |
| 3 | Variants that drop a shield or Parry may fail F1 gates (Maulhand, Farcaster and Bookhand, which give up the staff after the check, Macehand, Spearhand) | The risk band; failing rows are cut |
| 4 | The named piece may not survive play (the Vanguard shield at the F2/F4 Due; named armour at the F2 or F3 Due) | `NamedPieceHeldAtF3`, the armour fallback on shield kits, and the named-piece log in Grave Goods |
| 5 | Players read the Oddment as a hidden buff (the Pendant effect) | "Does nothing" line, placement off the grids, `OddmentMisuseRate`, the playtest question |
| 6 | Writing drift into hero biography, fallen heroes, the Dreamer, hints or Hollowfolk props | V21 text bans and tone review; bans cannot catch implication |
| 7 | Name collisions with the pending naming pass | V21 (g) by whole word and substring, plus the per-word flags (A.2) |
| 8 | Localisation load: about 6k words across M2 and M3 (8.8), against CH risk 13 | Text budgets; whole-string keys with named placeholders; list layouts for Oddment names; CardFit at 150% |
| 9 | SK-4 crosses owners (LE, DH, IR) | If refused, the tie-break gap remains and heirloom heroes may get a hollow upgrade (14.2) |
| 10 | The SK-11 price scale moves vendor prices and the zero camp-1 margin | Owner rerun at 250‰ and 200‰ before adoption |
| 11 | Arcanist variants depend on mundane focus bases that no record confirms yet | Gated; fallback M4 |
| 12 | The Memento frame tag and Lingerer lines compete for CH's tight budgets | They are the first and second cuts |
| 13 | The SK-16 decision may disappoint players who expect to share a variant run | Stated on the seeded entry screen |
| 14 | Hero kit gear sold for small coin teaches "sell the spare" as a camp-1 chore (Q4) | Resale is noise (1–3 coin); `KitSoldAtCamp1`; LE 6.6's still-dealt rule; the held lever |
| 15 | The named piece is sold, left or offered without the player knowing it carries a name (Q4) | The "Starting gear" chip and the inline "Its name goes with it" line on Sell, Swap/Leave and Curiosity rows (4.3) |
| 16 | The display sidecars drift from Core state | Updated only by reacting to Core events; `KitDisplayInert`; `KitIssueStable`; the 14.2 fallback onto `loreRunState` |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the risk band width, pick-rate thresholds and offer minimums, the 18-character name cap, the fire-prop count, the price scale until SK-11 is ruled, the DPR and EHP toys, the unlock pacing, art and writing counts, the Due timing estimate.
- Godot scene structure for the kit strip, the hero inspect header's Oddment line and the fire prop, with no rules state.
- The Oddment selection hash details inside the `lore` domain and the stated tuple (4.5.2).
- Line text beyond the samples, inside the writing rules and tone review.
- Names, until the naming pass.
- **Not tuning:** the kit budget (VP 0, no trinket, empty pack, 2 potions in one belt slot, coin exactly 15, supply 2); mundane-only kits; no second level-1 decision; variants only as boons **(agreed, Q1)**; the given name in Whence on the piece the F1 Due cannot replace (the shield, else the armour), the family word as the title, bynames Deed-only **(agreed, Q2)**; Oddments at M2 with 6 per default kit **(agreed, Q3)**; hero kit gear sellable **(agreed, Q4)**; the Oddment has no slot, price, effect, command or Core read and sits outside the key; no reward, count or milestone for keeping or leaving it; seeded runs write nothing and advance no milestone; the F1 Due never retargets.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **The class owners (VG, SH, ARC, HR):** SK-1, SK-2 (HR), SK-7 (VG 7.10 level-3 hint), SK-9 (inform).
- **The ITEMS owner:** SK-1 (17 row 3), SK-3 (18.1 row and creation order), SK-5 (12.3; no Core instance field), SK-11 (request), SK-12.
- **The LE owner:** SK-1 (LE 3.1), SK-3 (kit coin in `LootLedger`), SK-4, SK-6 (LE 8.2), SK-11 (request), SK-17; the camp-1 coverage rerun.
- **The DH owner:** SK-4 (confirm), SK-6 (DH 17.12); LE 6.6's still-dealt item.
- **The IR owner:** SK-4 (confirm), SK-5.
- **The CMP owner:** SK-3 (CMP 12), SK-5 (companion named pieces; upgrades never swap them).
- **The CH owner:** SK-6, SK-7 (CH 4.11), SK-8 (CH 4.9), SK-9; the Memento frame tag and Lingerer lines (M3).
- **The SV owner:** SK-5 and SK-6 (the `run.save` display sidecars; SV 4.3; the creation-screen run key), SK-10 (SV 5.2), SK-16 (request).
- **The RS owner:** SK-10 (RS 11.2), SK-14 (request), SK-15 (request, with MAIN); codex hosting of the Oddments page; the tone review.
- **The main design owner:** SK-7 (MAIN 4.7, read only), SK-15 (request).
- **The ABL and STAT owners:** SK-17; STAT signs off bundled skills (SK-7).
- **The XI owner (segment 10):** SK-9; codex page hosting with CH 4.15.8.
- **The W owner:** hand crossbow confirmation; mundane orb and tome bases.
- **The simulator:** the V21 (t) rows per class × pick × variant × worst-legal-arrangement; `VariantRiskBand`; the matrix runner.
- **Human sessions (SV 6.11) and, later, telemetry:** the V21 (r) reports, creation time on run 1, the playtest question.
- **Art, audio and writing:** 5, 6, 8.8.
- **Naming pass:** SK-13 and every placeholder here, with the A.2 flags.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Critique resolutions

| Finding (critic) | Resolution |
|---|---|
| DPR toy gave the off hand +3 (consistency, high) | Recomputed: Blades 6.2; Duellist ≈ −17%. Duellist held out for its missing ranged answer, not the false −37% (8.2, 5.3). EHP labelled illustrative and understated |
| "Bell-ringer" and "Bellrope" collide with RS 1558 and the bell rope (consistency, high) | Renamed; "Bell*" and "bell rope" added to the V21 blocklist (5.3, V21 (g)) |
| "Set it down", "set down" and "Carried down from the Stairtop" break AN5 (consistency, high) | "Leave it by the fire"; "Left at the second fire"; "Issued at the Stairtop"; V21 word list (4.5). *After the check:* per-camp captions replace the ordinal, and "Carried from the start" replaces the Stairtop line (14.3) |
| `oddmentsSeen` written at run start breaks `AbandonWritesNoLore`; no run-start tuple (consistency, high) | Never at issue; explicit tuple; flagged change SK-6. *After the check:* queued at camp-1 entry or WO6, flushed only by SV's flushes, tuple without `nightId` (4.5.2) |
| Coat tag breaks CH-24's "no hub peg" (consistency, medium) | Heirloom-shelf bundle only (4.4) |
| Survivor piece not guaranteed (consistency, balance) | Static check plus report; armour fallback. *After the check:* the shield where the kit has one, else the armour; the writing-time "not the set-A main hand" reading is retired (4.5.1, 14.3) |
| SK-4 imprecise; retargeting stacks two U (consistency, balance; high) | Tie-break plus waiver precedence, no retarget (4.3) |
| SK-11 sets live vendor prices; 200‰ fallback (consistency, medium) | Request with a rerun at both rates; V21 reads data (4.3) |
| Premise row implies fallen heroes (consistency, medium) | Resident-trade authors; `noFallenHero`; row reworded (4.5.2, SK-14) |
| Captions read as hints (doors, distances, theme tags) (consistency, low) | Bans added; door-list and bark samples replaced; `strataTag` hidden |
| "Kit" overloaded; chip looks like kitBound (consistency, coverage) | SK-13 naming pass; placeholder chip "Starting gear"; "Issued" rejected as the chip (26 hits) |
| Shared seeds and boons undecided (consistency, low) | SK-16 request, options stated |
| Given name on the name line outranks bynames (balance, high) | Given name lives in Whence; byname displaces it there; no IR 6.1 placement change (4.5.1; agreed, Q2) |
| Direction rule contradicts the roster (balance, high) | Replaced by a ±3-point band with per-class sides and the 5% cap (4.4) |
| Pick-rate trigger has no baseline (balance, medium) | Per-offer rate against 1/N, with a minimum of 30 offers (4.4) |
| First-run strip overload (balance, medium) | Run-1 form: silhouette, role, AC and hand-off only (4.6) |
| Oddment row beside the empty belt slot (balance, medium) | Moved to the inspect header, non-draggable; misuse metric (4.5.2) |
| `KitNoDominance` hollow with a skill; Mechanics on F1 (balance, low) | Gear-only check; skill bounds; Mechanics excluded at M3 (4.4) |
| Stat rearrangement makes kits illegal (coverage, high) | Creation warnings, checks over arrangements, worst-arrangement arm (SK-9) |
| Heirloom placement undefined (coverage, high) | SK-12; `fallbackOrder` field (new lead's call) |
| Idiom and pun names (coverage, medium) | Plain descriptive samples; idiom ban; "Hear" stem in the collision list |
| Oddments reuse Hollowfolk props (coverage, medium) | Samples re-authored; V21 ban (5.1); four samples given a named tone-review look (new lead's call) |
| Run-modifier hook missing (coverage, medium) | SK-15 |
| Level-3 hint duplicates variant gear (coverage, medium) | `level3SpareHint` per kit (4.4) |
| "Charm" field collides with the trinket kind (coverage, medium) | `accentId`; cloth and binding only (6) |
| Seeded and replay coverage gaps (coverage, medium) | Replays read the save; milestone cards carry the SV line (4.4, 4.5.2) |
| Unlock classes lack a kit plan (coverage, medium) | Per-class package row (8.8, 10) |
| Accessibility thin (coverage, low) | Labels, alt text, glyph and pattern, controller parity (4.5, 4.6, 6) |
| Companion name lifecycle (coverage, low) | 4.5.1; `CompanionNameOnIssuedPiece` |
| Reports miss the lore bets (coverage, low) | `NamedPieceHeldAtF3`, `OddmentRepeatRun`, playtest question (9.2) |
| M1 scope inconsistent (coverage, low) | M1 ships the Vanguard kit and structural checks; `KitCoin` a data assert; names at M2 (10) |

### A.2 Names checked (case-insensitive, whole word and substring, the 31 approved documents and the reviews; re-run while writing)

- **0 hits, used:** the SK-n prefix, V21, Oddment(s), Leave it by the fire, given name, kit line, kit strip, `accentId`, Starting gear, Also carried, Weighs nothing, Once called, Swordhand, Maulhand, Close Shot, Throwhand, Farcaster, Bookhand, Macehand, Spearhand, Nine Rivets, Patched Twice, Iron Rim, Waxed String, Worn Scabbard, Green Bark, Boss Nail, Gatesplitter, `KitRecord`, `KitVariantBoon`, `OddmentRecord`, `StartingKits`, `kitIssue`, `KitSoldAtCamp1`, `level3SpareHint`, `kitDelta`, `oddmentsSeen`, `givenNameKey`, `kitLineKey`, `coinStart`, and every V21 row and report name in 9.2. **Added while writing (new lead's call), 0 hits:** `KitVerbPerSet`, `skillAlternate`, `fallbackOrder`. **Added after the check, 0 hits:** Grey Lacing, Felt Elbows, Copper Links, Birch Handle, Brass Pommel, Ochre Daub, Carried from the start, `displayOverlay`, `namedPieceLog`, `skillFallback`, `fallbackOnly`, `level3SpareHintFamilies`, `leftCaptionKey`, `OddmentLeftCaption`, `numberBlocklist`, `BackgroundPerkBoon` (this design's label for the M3 background-perk boon record), `KitIssueStable`, `VariantPickConsistent`, `OddmentRetiredLoads`. `kit.acFloor` is a `KitRecord` field, distinct from CMP's check `CompanionAcFloor` (CMP 8.1). Every Oddment object noun in 5.1 was grepped; the draft-stage candidates lectern, napkin, apron, plumb line, sealing wax, marble, clasp, ferrule, badge, straw, rope and seed were dropped for hits on theme props, boss attacks, trinkets, room rulings or system terms.
- **Retired after the check:** Issued at the Stairtop (premise), Thin Edge (also W 18.2's "Edge (element)" property), String Cutter, Straight Shaft (innuendo; shared head word), Split Haft and Bent Tip (damage words), Painted Shaft (shared head word); each had 0 hits and is no longer used.
- **Existing terms used as defined:** Heirloom shelf (CH), Stairtop (CH), Book of Nights (CH), Old Friend and Wayfarer's Pack (CMP), `kitBound` (CMP-21), "Does nothing" (17 hits, plain prose, used as card text), Dream's Due (LE), Whence, byname, Deed (IR, CMP).
- **Taken (rejected):**
  - Keepsake (RS 9.3), Letter, Note, Token, Memento, Tally, Curios, Relic, Bundle, Pocket (54);
  - Issued (26, as a chip), Breaker (34), Margin (67), Warden (27), Crier (85), Knifework (SH Knife Work);
  - Bell-ringer (RS 1558), Lamplighter (RS enemy), Plain Speech (HR), Errata, Open Floor;
  - Set it down (AN5 "down");
  - **Duellist** (13 hits, incl. CL, W, IR "Duellist's Rapier"): retired with the cut variant (new lead's call).
- **Retired with their models:** Haversack and Livery (0 hits each).
- **Near-collisions kept, flagged to the naming pass (new lead's call; per-word check):**
  - **Iron Rim** (Gatekeeper shield): "Rim" is in the shield property **Bashing Rim** (AR, W 17.9), a real RC9 risk on a shield;
  - **Patched Twice** (Herald Shield shield): "Twice" is in the Scion rank word "Twice-Sworn" (IR 4.6), the armour property Twice-Proof and EV's Twice-Told card;
  - **Waxed String** (Scout shortbow): "string" is a substring of the party counter **Lodestring** (XI);
  - **Copper Links** (Herald Totem chain shirt): "links" is used for XI's Plunge links (a UI word, not a record name);
  - **Green Bark** (Hedgemage staff): "bark" is the RC8 system term (CH flagged Barker for the same reason);
  - **Boss Nail** (Shieldbearer shield): "Boss" is the game's boss term (a shield boss is meant);
  - **usher** in a Herald kit line: next to EV's resident the **Night Usher** (CH bars it as an ambient figure); an usher's trade is allowed by the premise test, so it stays a flag;
  - **Gatesplitter** (sample byname): next to the notable Gatebreaker (0.1 item 4), deliberately as a byname sample only.
- **Prefixes:** `\bSK-[0-9]` 0 hits; `\bV21` 0 hits (specs and reviews).

### A.3 SK cross-reference

| Id | Sections here |
|---|---|
| SK-1 | 0, 2.3 F1, 4.2, 4.3, 8.1, 10 |
| SK-2 | 0, 2.3 F2, 5.1, 10 |
| SK-3 | 2.2, 2.3 F4, 4.1, 4.2, 7, 9.1, 11 |
| SK-4 | 2.3 F5, 4.3, 8.1, 10 |
| SK-5 | 0.2, 4.5.1, 5.2, 7, 9.1, 11 |
| SK-6 | 0.2, 4.5.2, 4.7, 9.1, 11 |
| SK-7 | 0.2, 4.4, 5.3, 9.1 |
| SK-8 | 0, 2.2, 2.3 F3, 11, 13 |
| SK-9 | 2.3 F6, 4.6 |
| SK-10 | header (Validator), 9.2 |
| SK-11 | 2.3 F7, 4.3, 8.4 |
| SK-12 | 7, 9.1, 9.2 (l) |
| SK-13 | 2.3 F8, 4.5.1 |
| SK-14 | 4.5.2 |
| SK-15 | 4.7, 9.2 (m) |
| SK-16 | 0.2, 4.7 |
| SK-17 | 2.3 F9 |

SK-1 to SK-17 each have one meaning. The drafts' local SK numbers (class-kits SK-1..10, background-kits SK-1..8, pack-kits SK-1..11, lore-layer SK-1..8, catalogue SK-1..9, numbers SK-1..11) are superseded by this register.

## Appendix B: sources

- Game details in section 1 are from memory and general design knowledge; none is cited as a rule, and each must be checked before it is quoted to anyone outside the team.
- The segment 14 proposal (the synthesis), its three models (A, B, C), the lore-layer, catalogue and numbers passes, the judges' and critics' reports, and the toy script, illustrative only, kept in the design lead's scratch area, not in the repository.
- The 31 approved documents listed in the header and the cross-document comparison of 2026-11-23.
