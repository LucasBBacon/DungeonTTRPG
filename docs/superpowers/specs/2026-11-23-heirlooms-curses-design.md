# Heirlooms and Curses: Design (Segment 15)

Date: 2026-11-23
Status: Approved, 2026-11-23
Parents (immediate): `2026-11-23-starting-kits-design.md` (starting kits design, "SK", segment 14, approved 2026-11-23, **the most recent template**), `2026-11-23-camp-hub-design.md` (camp and hub design, "CH", segment 13), `2026-11-23-seeds-validation-design.md` ("SV", segments 11–12), `2026-11-23-exploration-information-design.md` ("XI", segment 10), the loot designs `2026-11-22-loot-economy-design.md` ("LE", 7a), `2026-11-22-item-rolls-design.md` ("IR", 7b) and `2026-11-22-loot-sources-design.md` ("LP", 7c), `2026-11-22-events-design.md` ("EV", segment 8), `2026-11-22-boss-rooms-design.md` ("BR", segment 9), and `2026-11-23-cross-document-comparison.md` (reviews, "CC").
Other parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` ("STAT"),
`2026-11-16-ability-resource-model-design.md` ("ABL"),
`2026-11-16-conditions-design.md` ("CON"),
`2026-11-16-enemies-design.md` ("ENM"),
`2026-11-16-items-inventory-design.md` ("ITEMS"),
`2026-11-17-weapons-design.md` ("W"),
`2026-11-17-armour-design.md` ("AR"),
`2026-11-17-trinkets-design.md` ("TK"),
`2026-11-17-consumables-design.md` ("CN"),
`2026-11-17-death-healing-design.md` ("DH"),
`2026-11-17-classes-concepts-design.md` ("CL"),
`2026-11-18-vanguard-class-design.md` ("VG"),
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC"),
`2026-11-20-herald-class-design.md` ("HR"),
`2026-11-20-companions-design.md` ("CMP"),
`2026-11-21-run-structure-themes-design.md` ("RS"; its change ids are RT-n), `2026-11-21-floor-layout-design.md` ("FL"), `2026-11-21-room-terrain-design.md` ("ROOM"), `2026-11-21-hazards-design.md` ("HZ"), `2026-11-21-traps-locks-design.md` ("TL") and `2026-11-21-encounter-placement-design.md` ("EP").
All 32 approved specifications and the cross-document comparison were read or grepped for: heirloom, curse, cursed, Clinging, Ill-Omened, Leaden, Ill-Starred, hex, bane, blight, boon, run modifier, meta-curse, Nightmare, difficulty, Depth gauge, shrine, desecrated, Curiosity price, item offered, Memento, Grave Goods, inherit, legacy, melancholia, madness, affliction, malady, Diseased, humour, Oath, Heirloom shelf, Recovery, pool and `kitDelta`. Every rule cites the sections it relies on. **This document edits no parent document** (13).

Items marked **(agreed)** are the client's answers of 2026-11-23 (final) to the six questions this design raised (Q1–Q6; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document; parent documents' questions are always named with their document ("SK Q1", "SV Q1", "EV Q4", "TK Q2") so that they never read as this document's Q1–Q6. The client also said that everything else in the lead's proposal stands as written: heirloom identity per ITEMS 14; the earning routes; Grip-marks as display-only history; the HL-3 F1 Due waiver for any heirloom slot; HL-4 (never a Curiosity price); the shelf; item curses and folk cost lines; Maladies as Cursed presets with their own slot and Cast-off deeds; Oaths; the Burdened twin and Redeemed template links; the full catalogue; art and audio; the new mechanics; the numbers; data and validator V22 `HeirloomsCurses`; staging (now with item curses and Maladies at M4); ownership and hooks; the binding requirements; HL-1 to HL-30; the risks; and the appendix resolutions and names. Names are placeholders pending the naming pass. (The design check renamed several placeholders, among them "Passings", now **Grip-marks**; 14.3 lists every rename. The client's own words above are quoted as given.)

**The lead's answers (0.1) and the recommended frame (3.3)** were accepted by the client by proceeding to the six questions. They are recorded here as the lead's answers; the client confirmed them on 2026-11-23, when it approved this document as a whole, **including every told-plainly item of 14.1**: the naming idea is narrowed to obsolete humoral and theological complaints; the rejected names and their reasons; "Geas" is rejected as an SRD name; "Black Bile" is rejected for its collision with the bile square; the reward is breadth, not power; inherited afflictions across runs, Weighted heirlooms with Entails, heirloom loss on death, a priced camp cure and boss-inflicted Maladies are all rejected.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-23, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal, mostly from the corpus check; they are listed in 14.1 and the client confirmed them all on 2026-11-23. Items marked **(lead's call, after the check)** were made in answer to the design check of this document (builder, consistency, fairness and narrative-art lenses); none overrides a client answer; they are listed in 14.3 with the "Told plainly after the check" items (T1–T14), and the client confirmed them all on 2026-11-23. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change HL-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-23 (with Q1–Q6 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge, a critic or a draft, or state an unwelcome forecast, and were told to the client in plain words (14.1; those found in the check, 14.3, T1–T14); the client confirmed them all on 2026-11-23. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q6 of this document (2026-11-23, final), or one recorded in an approved document (named with its document), restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal (the lead's answers of 0.1 and the frame of 3.3 included); confirmed by the client with this document on 2026-11-23 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-23 |
| **(lead's call, after the check)** | A lead's call made in answer to the design check; never overrides a client answer; listed in 14.3, with the "Told plainly after the check" items, and confirmed by the client on 2026-11-23 |
| **(change HL-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text are flagged ⚑ there; the client's approval of 2026-11-23 covers them |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a judge, a critic or a draft, or states an unwelcome forecast; told to the client in plain words (14.1; those found in the check, 14.3) and confirmed by the client on 2026-11-23 |
| **(request)** | Asks another owner to rule; changes nothing until that owner rules; the fallback is stated (14.2) |
| **(hook)** | Owned by another design; this design states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator or human sessions (8, 9). Never binding |

**The client's brief.** "Let's go for the heirlooms and curses. I haven't got much input for these; a consideration I have is to make the names of the curses a mix of old concepts of mind diseases, like melancholia, and fantasy curse names, but I'll leave this for your judgement. Do the usual research and considerations." Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; **combat is key**; contest the client or approved views where warranted.

**Change prefix.** This design uses **HL-n**. A case-insensitive grep for `HL-[0-9]` across `docs/` (the 32 approved documents and the reviews) finds **0 hits** (re-run while writing). The research drafts, the three models and the two components used colliding local HL numbers (HL-1 to HL-22 and HL-30 to HL-42); this document issues **one register, HL-1 to HL-31** (13; HL-31 was added after the design check), and no id has two meanings in it (A.3).

**Validator.** **V22 `HeirloomsCurses`** is free. V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV, V17 BR, V18 XI, V19 SV, V20 CH and V21 SK (`StartingKits`, SK-10). A grep for `\bV22` finds **0 hits** (re-run while writing). V22 joins the consolidated suite only through **HL-14** (RS 11.2 suite table; SV 5.2 becomes V1–V22), as V21 did through SK-10.

---

## 0. At a glance

- **An heirloom stays exactly what ITEMS 14 says** (settled): an authored, fixed-Uncommon profile template; a fresh instance each run; taken as the one starting boon; it never grows and its template is never lost. This design adds the missing half, **how heirlooms are earned**, which no approved document says; a display-only history called **Grip-marks**; and fixes for three seams.
- **Earning (change HL-1, HL-2, HL-31; agreed, Q2).** Every route is an achievement; none is a copied instance, a Memento or a death key.
  - R1, class milestones (one per class);
  - R2, a **Deed bequest** from any personal run, win or death: keyed to a carried item being credited with a boss's fall (a Core `BossFelled` fact: the weapon or focus that dealt the killing blow, or a shield or armour worn by a standing unit when the boss falls), tier-gated, chosen later at the shelf, never on the death path (lead's call, after the check);
  - R3, **Heart-won** templates: one per final boss, so three at M3;
  - Later: Oath wins (R4) and Redeemed templates (R5).
- **Curses come in three scopes, each with its own naming register** (agreed, Q1), so the name tells the player the scope (RC9):

| Scope | What it is | Register | Lasts | Umbrella word | Wave |
|---|---|---|---|---|---|
| **Malady** | a lingering state on one unit during a run | obsolete humoral and theological complaint | until camp, or until its Cast-off deed (it can reach a boss fight: told plainly, T8) | "Malady" | **M4** (agreed, Q5) |
| **Item curse** | the cost line on a cursed twin | folk curse | while worn; Clinging until camp | "Cursed" | **M4** (agreed, Q5) |
| **Oath** | an opt-in run modifier in the Depth gauge's reserved slot | oath, "Oath of the ..." | the whole run | "Oath" | Later (agreed, Q5, Q6) |

- **Maladies (M4; agreed, Q4, Q5):** named presets of the approved Cursed (curse) family: nine new variant ids that count once toward CON 16, so **0 new budget entries**; at most 1 per unit, in a slot of their own beside the cap of 2; sources are **telegraphed or priced** (Curiosity costs on F2–F4, paid by the hero by default; shrine bargains and opt-in desecrated shrines; elite abilities on F3–F4 whose intent is announced to everyone by the ENM 13.2 forecast pattern, a request); never from bosses, traps, hazards or random imposition, never on F1 or F5; every Malady prints a **Cast-off deed** that ends it in combat.
- **Item curses (M4; agreed, Q3, Q5):** the approved cursed twins unchanged, plus an honest class-wide chip, **"Not yet read: may Cling"** (presentation only; on every unread Rare+ field card whose family has an authored cursed twin, cursed or not), and seven folk cost lines.
- **Oaths (Later; agreed, Q6, a request with a fallback):** opt-in; they change the kind of play, never stats; content-only rewards. The drafts' word "Geasa" is rejected: "Geas" is an SRD spell name and RS 7.6 bans D&D-specific names (told plainly).
- **Links, all opt-in and power-neutral (Later):** the Burdened twin (an heirloom taken under the Oath of the Burdened Hand) and the Redeemed template. **Inherited afflictions across runs are rejected** (told plainly).
- **Rejected:** Weighted heirlooms with Entails and "Set Down"; a third stacking difficulty ladder; heirloom loss on death; a priced camp cure; boss-inflicted Maladies; the names "Black Bile", "Geas" and "Echo"; the thorned-ring and knot badges (told plainly).

### 0.1 The lead's answers in plain words (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

1. **The client's naming idea: adopted, scoped by register, and never mixed inside one scope** (agreed, Q1; told plainly where it narrows the brief).
   - **Mind-disease names go only on Maladies**, the run-scoped states the dream lays on a unit. They never go on items, heirlooms, enemies or Oaths.
     - An object is never named as a mental state.
     - Nothing inherited carries a mind word: "the family melancholia" reads as hereditary mental illness.
   - **Fantasy curse names go on item curse cost lines and on meta-curses.**
     - Item cost lines use the folk register. Ill-Omened, Leaden, Ill-Starred and Clinging are already approved (CON 18.10, ITEMS 13). New: Ill-Wished, Eyebitten, Short-Armed, Saltless, Black Lot, Graceless, Ill-Slept.
     - Meta-curses use the oath register: "Oath of the Crooked Road".
   - **"Mind diseases" is narrowed to obsolete humoral and theological complaints** (told plainly): the black and yellow humours, phlegm, the spleen, acedia, the glass conceit, tarantism. These are words the waking world has forgotten. A machine blocklist bans living diagnoses, slurs and gendered terms (5.3).
   - **Why the dream frame helps.** RS 7.5 says the dream "swallows what the waking world forgets", and retired medicine is exactly that. A Malady belongs to the dream's forgotten medicine, not to the hero's mind. Text always says the dream acts ("The dream lends you Saturn's weight"), never "you are depressed"; every Malady flavour line has the dream as its subject (V22 (e) `MaladyFlavourDreamActs`, lead's call, after the check).
   - **Effects follow the old metaphor, never a symptom:** heat, cold, damp, weight, glass, dance.
     - No Malady slows thought, saps motivation, refuses help, isolates, hallucinates, or deals psychic damage from a mind word.
     - No Malady lowers a pool's or HP's maximum: "less in the tank" is the textbook anergia of depression, so Leaden stays an item cost line only (V22 (d) `MaladyNeverLowersMax`; lead's call, after the check).
     - No symptom simulation (RC1 already bans fake tiles and lying UI).
     - No loss of agency (CON: agency conditions are enemy-only).
   - **Melancholia, the client's own example, stays, with conditions** (agreed, Q1): its payload is weight only: when forced movement moves you, you also fall Prone, once per round (5.2; the proposal's Leaden-on-a-`camp`-pool payload was replaced after the check, T5); it is the one agreed exception to NR1 and NR1b, tagged `nameException: Q1Melancholia` with `referentLiving: true` (5.3); "Saturn's weight" is flavour, never a subtitle-name; it needs a **mandatory tone sign-off**; its fallback name is **Atrabile**.
   - **"Black Bile" is rejected** because "bile" is an approved square kind (told plainly). No Malady display string uses the word "bile"; the humoral glosses live in the codex's historical notes only (lead's call, after the check). The agreed fallback Atrabile contains "bile" as a substring and keeps an explicit NR6 waiver (5.3; T7).
   - **Never comic, on any floor.** A Malady is harm, so RS 7.3 pillar 2 governs, even on the comic floors 1–2.
   - **Rejected names, with reasons** (told plainly):
     - Calenture: the leap into the sea is suicide imagery (RS 7.6).
     - Wanhope: despair.
     - Nostalgia and Mal du pays: they imply a waking home (RS 7.5).
     - Somnambulism, Hypochondria, Dementia: living diagnoses.
     - The Megrims and Quartan Ague: migraine and malaria are common living conditions.
     - Hysteria, the vapours, lunacy, mania, madness: gendered terms or slurs.
     - St Vitus' dance: a real saint.
     - Geas: an SRD 5.1 spell name (RS 7.6: no D&D-specific names).
   - **Safeguards** (agreed, Q1): a sensitivity read by a clinician or a person with lived experience "before Later or M4 content lock" (the client's words), read as due before **M4** content lock now that Maladies ship at M4 (F20), and held at M4 content-complete so a fallback can still be swapped in (lead's call, after the check); a content note in Settings, also shown once inline before the first Malady preview (lead's call, after the check); a display-only **Plain names** setting that shows each name's plain label on every surface (HL-27; the label is the rule kind plus a short rule tag, "Malady: Easy Opening", so two names never become one; lead's call, after the check).
2. **What an heirloom is:** unchanged (ITEMS 14). One authored Uncommon item, taken as the one starting boon, with a fresh instance each run. Identified, banded, never grows in power. Never cursed, except the opt-in Burdened twin (Later).
3. **How heirlooms are earned:** by doing things, never by dying or by copying loot (agreed, Q2):
   - meeting a class milestone;
   - having an item of a family credited with a boss's fall (the weapon or focus that felled it, or a shield or armour worn by a standing hero or companion when it fell), after which you may hang that family's authored heirloom on the shelf (you choose at your next shelf visit);
   - the first win over each final boss.
4. **How many:** 13 at M3 *(illustr.)*: 4 class milestones, 6 Deed families and 3 Heart-won. About 24 by M4. A competent player fills the M3 shelf in about 25–30 runs *(illustr.; 8.2)*. **The reward is breadth, not power** (told plainly).
5. **Where they live and how they enter a run:** on the Heirloom shelf (CH 4.8): 6 hooks in view, the rest wrapped beneath, never deleted. Placement follows SK-12. **Any heirloom slot now waives the F1 Due to 20 coin** (change HL-3), closing a seam all five drafts found.
6. **Can they be lost?** The template: never. The instance: only by leaving it at the stairs (settled). It is never a Curiosity price (change HL-4). **Heirloom loss on death is rejected** (told plainly).
7. **Do they grow?** In history only: a byname and up to three Deed marks (Grip-marks; the proposal's placeholder "Passings" was renamed after the check because it reads as "deaths", T12). Display-only and outside the seed key; never a count; never a hero's name.
8. **Curses on the hero during a run:** yes, as Maladies (M4): always telegraphed or priced, always ended by camp, at most one per unit, each with a combat deed that casts it off. **Boss-inflicted Maladies and a priced camp cure are rejected** (told plainly).
9. **Meta-curses:** yes, as Oaths in the one reserved slot the MAIN/RS owners hold (a request, agreed, Q6). Rewards are content only: an heirloom template, a frame sigil and a Book of Nights mark.
10. **Do heirlooms and curses link?** Only by choice: the Burdened twin and the Redeemed template. **Inherited afflictions: no** (told plainly). **Weighted heirlooms with Entails: no** (told plainly; 3.1).

### 0.2 The client's six decisions (agreed, 2026-11-23, final)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Naming | **A.** Three registers by scope: obsolete complaint names on Maladies only; folk words on item curse cost lines; "Oath of the ..." on meta-curses; naming rules NR1–NR10 and the blocklist; Melancholia kept with a weight-only rule, a mandatory tone sign-off and fallback name Atrabile; the display-only Plain names setting, content note and a sensitivity read before Later or M4 content lock (5.3; read as "before M4 content lock" once Q5 moved Maladies to M4, F20) | B: mind-disease names on all curses (the brief's literal mix). C: fantasy names only | HL-13 ⚑, HL-27, HL-30 ⚑ |
| Q2 | Heirloom earning | **A.** R1 class milestones; R2 Deed bequests from any personal run (win or death, keyed to the Deed, tier-gated, chosen later at the shelf, never on the death path); R3 Heart-won; R4 Oath wins and R5 Redeemed Later (4.1.2; "keyed to the Deed" is met through the Core `BossFelled` fact the Deed records, because the Deed itself is display only, T2) | B: milestones and Heart-won only | HL-1 ⚑, HL-2 ⚑, HL-6 ⚑, HL-7 ⚑, HL-31 ⚑ |
| Q3 | Hidden item curses | **A.** The class-wide "Not yet read: may Cling" chip, presentation only; the approved curse mechanics unchanged (4.2; the class is every card that can be cursed, T3) | B: full reveal before equipping. C: silent, as approved | HL-15 ⚑ |
| Q4 | Malady sources | **A.** Priced Curiosities and shrine bargains, plus intent-telegraphed elite abilities on F3–F4; never bosses, traps, hazards or random imposition; never on F1 or F5 (4.3.3; the intent is telegraphed by the ENM 13.2 forecast pattern, never a committed intent, T1) | B: choices only (no enemy sources) | HL-19 ⚑ (request) |
| Q5 | Waves | **A.** Item curses and Maladies move to **M4** (an agreed-Later item moved by the client); Oaths and the other opt-in pieces stay Later (10; rune cures stay Later with their hosts, T11) | B: keep curses Later | HL-29 ⚑ |
| Q6 | Meta-curses | **A.** Request the MAIN/RS owners of the CH-24 slot to fill it with Oaths (kind only, content rewards); a request with a fallback (4.5; on refusal the fallback reaches B's outcome, T14) | B: leave the slot unlit until the difficulty decision | HL-21 ⚑ (request) |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Naming: three registers by scope; NR1–NR10; blocklist; Plain names; content note; sensitivity read (0.1, 5.3) | M2 rules, M4 Malady content | lead's call; **agreed, Q1**; HL-13 ⚑, HL-27, HL-30 ⚑ |
| 2 | Heirloom identity: ITEMS 14 kept whole; the narrow-Uncommon authoring rule (4.1.1) | M3 | settled; HL-8 ⚑ |
| 3 | Earning: R1, R2 (tier-gated Deed bequest, keyed to the Core `BossFelled` credit), R3; R4, R5 Later (4.1.2) | M3 / Later | lead's call; **agreed, Q2**; HL-1 ⚑, HL-2 ⚑, HL-7 ⚑, HL-31 ⚑ |
| 4 | Bequest timing: at most one offer per run, written at WO6 by the non-Core profile-progress step, queued one per family, chosen (Hang it or Set aside) at the next shelf visit or in Begin again's boon picker, never on the death path (4.1.2) | M3 | HL-6 ⚑ (request to SV) |
| 5 | The F1 Due seam: any heirloom slot sets "arms satisfied" (4.1.4) | M3 | HL-3 ⚑ |
| 6 | History: Grip-marks, display only, bynames only (4.1.5) | M3 | HL-5 ⚑ |
| 7 | Loss: never the template; never a Curiosity price (4.1.6) | M3 | HL-4 ⚑ |
| 8 | Item curses: approved twins, the class-wide chip, folk cost lines (4.2) | **M4** | **agreed, Q3, Q5**; HL-15 ⚑, HL-16, HL-29 ⚑ |
| 9 | Maladies: Cursed presets, own slot, Cast-off deeds, telegraphed sources (4.3) | **M4** | **agreed, Q4, Q5**; HL-17 ⚑ to HL-20, HL-29 ⚑ |
| 10 | Oaths: one slot; kind, not stats; content rewards (4.5) | Later | **agreed, Q6**; HL-21 ⚑ (request), HL-22 |
| 11 | Links: Burdened twin, Redeemed; inherited afflictions rejected (4.4, 4.7) | Later | HL-23 ⚑, HL-24 |
| 12 | Waves: heirlooms M3; item curses and Maladies M4; Oaths and links Later (10) | M1–Later | **agreed, Q5**; HL-29 ⚑ |
| 13 | Corrections found in the corpus check while writing: heirloom line renames, the pool targets of Leaden Maladies (superseded after the check: no Malady uses Leaden), Saltless against DH's potion rule, the M4 condition budget reading, the shrine and malign-hint wave, Restless- and Conspicuous-dependent lines, the DH legacy hook's wave, Grip-marks bynames, name flags (2.3 F10–F21; 14.1) | M2–Later | new lead's call |
| 14 | Fixes after the design check: the `BossFelled` credit and the profile-progress step; bequest queue, order and decline; the run-end table; Grip-marks data; the F1 Due predicate and order; the chip's population; Malady state, cast-off actors, per-source ranges, the hero-pays rule, elite forecasts, replacement and targeting; no Leaden Maladies; payload, flavour and name corrections; glyphs, head-tell, audio, Plain-names labels, locale fields; report formulas and arms; register flags (14.3) | M1–Later | lead's call, after the check; HL-31 ⚑ |

### 0.4 Intent, goals and anti-goals (lead's call)

**Intent.** Heirlooms make the previous run matter without making the next one easier in numbers. Curses make the dream bite in fights, and every bite can be read before it lands.

**Goals.**
1. G1: every curse has a printed, chosen or telegraphed cost.
2. G2: an earning path the player can see and pursue.
3. G3: curses create combat decisions (Cast-off deeds; positioning against telegraphed elites).
4. G4: a naming system that teaches and never mocks.
5. G5: determinism unchanged. The template id and the run-modifier id go in `configHash`; history stays outside the key.

**Anti-goals.**
- Grind to win (MAIN 2); permanent stat boosts (MAIN 4.7).
- Hidden outcomes and lying UI (EV 4.4.8, RC1).
- A run-wide curse track (RS 9.1, rejected).
- Mercy through curses or rewards (EV Q4).
- Death counts or streak-keyed state (CH AN11).
- Agency loss on heroes (CON).
- Depicting real illness.

---

## 1. Survey

The research drafts surveyed these games and tables. Details the drafts marked "unsure" are not used as facts here; everything is from memory and general design knowledge (Appendix B).

| Source | What it does | Praise | Criticism | Lesson for us |
|---|---|---|---|---|
| Rogue Legacy 1/2 | heir traits named after real conditions (Dementia, OCD, Vertigo); permanent manor upgrades | each death gives a new character | ableism criticism; RL2 v1.0.2 renamed traits and removed Vertigo for nausea | no living diagnoses; no symptom simulation; no stat upgrades |
| Darkest Dungeon 1/2 | stress, afflictions, quirks, diseases ("Bad Humours"); Sanitarium cures; heirloom currency | the period register gives dignity | agency loss, cure grind, roster fatigue | keep the register; drop the persistence, agency loss and cure sinks |
| Slay the Spire | curse cards from priced events; boss relics with printed downsides; Ascension | a printed downside at the moment of choice | — | opt-in priced costs |
| Hades | Pact of Punishment (named opt-in conditions); Chaos gates (curse, then boon); keepsakes that rank up | legibility; one register | Tight Deadline tedium; bounty grind | name meta-curses in one register; content-only rewards |
| Binding of Isaac | imposed floor curses (Lost, Unknown, Blind) | the Labyrinth is welcomed | curses that deny information are resented | every imposed cost is visible and bounded (RC1) |
| Dead Cells | cursed chests lifted by kills; Boss Cells add new kinds of challenge | lifting a curse by play is loved | — | Cast-off deeds |
| Enter the Gungeon | a hidden run-wide Curse stat | risk and reward | cost hidden in a pause menu | RS 9.1's rejection of a curse track holds |
| NetHack, DCSS | cursed gear that welds to you; DCSS removed remove-curse scrolls (around 0.27) | knowledge play | gotcha welds, wiki dependence | identify must be cheap and costs readable |
| Risk of Rain 2 | Lunar items (printed downsides); Artifacts (rule modifiers) | Artifacts change kind, not stats | — | the model for Oaths |
| Inscryption | Death Cards carry a fallen run forward | the legacy feels real | it carries power forward | keep the memorial (Grip-marks), drop the power |
| Hollow Knight | Overcharm; fragile charms | a visible opt-in price | — | the Burdened twin |
| D&D 5E | cursed items hidden from Identify; the Bestow Curse menu; DMG madness tables | short single-rule costs | gotcha curses; madness tables criticised; Ravenloft (2021) offers Fear and Stress instead | single-rule costs; no random madness |
| Call of Cthulhu, Delta Green, Blades in the Dark | sanity and trauma | Delta Green and Blades move the cost into played, managed traits | sanity tables stigmatise | put the cost where the player can manage it |
| Weapons of Legacy, Wildemount Vestiges, Kingdom Death | items that grow with history | history is what players love | growth breaks balance | history, never numbers |
| Hellblade | clinical consultation and lived experience | respectful | — | a sensitivity read |

**Cross-cutting lessons.**
1. Printed opt-in costs are loved; imposed hidden costs are resented.
2. A curse cast off by a deed beats a cure by grind.
3. Modifiers that change the kind of play age better than stat stacks.
4. Real-condition names used as jokes draw lasting criticism; obsolete terms in a sincere register do not.
5. Legacy items carry emotion through memory, not power.

---

## 2. Audit

### 2.1 What the approved documents fix (settled unless a flagged HL changes it)

**MAIN 2 / 4.7 (decided):**
- no grind and no permanent stat boosts;
- one starting boon, "such as an heirloom item or a background perk"; boons do not stack;
- meta-curses become one reserved run-modifier slot on the Depth gauge, owned by the MAIN/RS difficulty decision (CH-24);
- a run modifier touches kits only through `kitDelta` (SK-15, request; V21 (m));
- Nightmare adds kind, not stats (RT-21); unlocks, the ladder and Nightmare leave loot unchanged (LE-19);
- no mercy (EV-10, EV Q4); an unlit easier lamp below tier 0 (CH 4.8).

**ITEMS 14 (lead's call, M3):** a profile template with a fresh instance each run; Uncommon, identified, banded, never grows; flags `heirloom`, `unsellable`, `noSteal`; **"Never cursed"**; outside the budget, display-only value; no barter; no family cap, but blocks duplicate bases; counts as owned for the Due and for verdicts; `oncePerRun`; lost at the stairs if left, the template kept; picked on the Heirloom shelf with its exact text shown; SK-12 placement and the SK-4 weapon-or-focus waiver already written in. ITEMS 15: only heirloom templates persist. ITEMS 19.1 rules 9–10 (curses never on heirlooms; heirlooms uncursed, `identifyDifficulty` 0).

**ITEMS 13, IR 4.15, LE 8.5 (curses, agreed Later until this document's Q5):**
- cursed twins (`cursedTwinOf`), benefit at T+1 on a tier-T card (LE-22, CC-43);
- the cost is a `whileEquipped` property (Ill-Omened, Leaden, Ill-Starred), never a lingering condition;
- Clinging applies on equip whatever the identification state, is exempt from the cap of 2, blocks every move out of the slot, and ends at camp, at a shrine or by Arcana at a rune (failure gives Weary 1); re-equipping applies it again;
- Rare+ gear field cards, F2+, at most 1 per floor via the 2-draw `curseGate` (consumed from M2; ignored while `loot.curse.permille` is null);
- barred from F1, the Due, fit premiums, the arms floor, offers and the fourth, the Viaticum, camp, signatures, heirlooms, consumables and rings;
- "hidden as approved; the malign hint is Later" (IR 4.15);
- cursed carriers apply the benefit only and show no curse mark; a cursed badge once the curse is known;
- shrine curse cures and curse bargains (EV-43, Later); no curse trunk (CH-24).

**ITEMS 11 (agreed, framework decision 4):** free camp identify (a client decision); equipping reveals nothing; Clinging taking hold reveals the curse; the malign hint is Later (item 7).

**CON:**
- 18.10: Cursed (curse), minor, Later. Variants: Ill-Omened (first d20 test each fight at disadvantage); Leaden (one named pool's maximum −1); Ill-Starred (type) (weak to one damage type); Clinging. Duration `untilCamp`; a shrine, or Arcana at a rune (failure Weary 1). Sources: cursed loot, desecrated shrines. An item curse's cost is a `whileEquipped` property, and item Clinging is cap-exempt.
- 18.10 also holds **Diseased (strain)** (Later): 3 stages, worsens at each short rest untreated, Treat cures it. Restless and Conspicuous are Later.
- 14: a cap of 2 harmful lingering conditions ("Further ones are blocked"), with Scarred and item Clinging exempt; Remedy ends only non-curse lingering conditions; camp clears everything; "drained, never helpless".
- 16: at most 32 distinct conditions **in M1 and M2 together**, family variants counted once; at most 6 harmful conditions per floor theme; at most 2 per enemy type. RT-15, RT-16 and EV-34 are pending rulings. 18.1: Later conditions sit outside the 32.
- 18.9: Warded never blocks lingering conditions. Agency conditions are enemy-only; random condition tables are a non-goal.

**ABL 6.2:** a pool is a limited count with one refill tag (`turn`, `fight`, `rest`, `camp`); every class has at least one `rest` pool and one `camp` pool. **STAT 6 and DH:** Recovery is hit points per short rest, a value, not a pool; a potion heals the receiver's Recovery (DH 142, CN 7.2).

**RS:** 7.3 pillar 2 (no humour on hits, downings or deaths) and pillar 4 (register by floor); 7.4 RC1 (no lying UI), RC5 (glyph registry), RC7 (one global condition skin, reserved cues), RC9 (names never lie); 7.5 the premise; **7.6 watch-list: no suicide imagery; no asylum or madness-as-monster themes; invented liturgies only; no D&D-specific names**; 9.1 rejects "a run-wide curse track"; 6.5 the final-boss pool is 2 in M2 (the Night Governess, the Rousing Engine) and 3 in M3 (adding the Lullaby, RT-18); 6.9 the Stratum Card; 11.2 the validator suite.

**LE:** 6.6 item 2 (the F1 hero-arms Due, the 20-coin waiver, "the Due never retargets"; SK-4 written in); 6.11 the Memento (never power); 6.12 harder is not richer; 8.2 Grave Goods (never a score); 6.3 deck shapes (F2 has no R card); LE-24 loot is never a lever for deaths.

**IR:** 4.14 `identifyDifficulty` and verdict glyphs; 6.1 bynames only through Deeds (an item that felled a boss), shown in the Whence pane, display only; 9 instance `deeds{}` display only; V14 (j) no rules code reads byname or Deeds; 8 rejects items that grow and memories that add lines. **FL 6.11:** `BossDefeated` is the canonical Core event that ends a floor (none on F5). **ITEMS 11 item 7 and ITEMS 24:** runes and shrines that identify items or end Clinging are Later dungeon content.

**SK:** SK-4 (a weapon or focus heirloom waives the Due to 20 coin; no retarget; pending IR, DH and LE confirmation); SK-12 placement; SK-7 variants as boons and their milestones; SK-5 given names (in the run's display sidecar) and the byname displacing them; SK-15 `kitDelta`; SK-16 shared-seed boons, fallback (a); 4.5.2 `noFallenHero`, "nothing ever returns as a boon or an item", the effective Oddment pool.

**CH:** 4.8 stations (the Heirloom shelf; the Depth gauge with tier, `nightmareTier`, the reserved easier lamp and the reserved run-modifier slot); 4.9 the launcher; 4.10 the death path, `DeathPathSeconds` and "Begin again"; 4.11 every unlock has a hub object (V20s `UnlockHasHubObject`); 4.12 reveals (none after a run-1 death; the shelf appears after the first unlock); AN1, AN10, AN11; CH Q2.

**EV:** 4.4.4 costs ("Item offered" never takes healing items, the Due, quest items or curses); 4.4.7 StateBlind; Q4 no mercy; 4.4.8 no cross-run power, no hidden outcomes; 4.6 never on F1; 4.9 Provisos; 4.5.10 the Unwoken Roll as the single fallen-hero device; EV-43 shrine curse cures and bargains (Later); `EventDeathShareDelta` ≤ +0.5 pp (gated: novice arm, F1 at party 1 and F2 at parties 1–2); a Grace ends at the arena seal (`ArenaEngaged`, BR-3).

**DH:** 18.2 calibration targets (F1 hero deaths under 5%; at least 50% of deaths on F3–5, half of those at elites and bosses; win rate 25–33%, the no-healer arm at 25% or more); 5.8 item 9 the legacy hook (a fallen hero's notable as a future heirloom), staged **Later** in DH's staging table; 4.1 permanent injuries rejected; 17.12 the neutral recap; 21.5 camp-1 margin 0.

**SV:** 4.6 the key `(runSeed, genVersion, manifestHash, configHash, unlockHash)`; `configHash` covers `difficulty.tier`, `boons[]` and `nightmareTier` (SV-12); profile memory sits outside the key; SV Q1 seeded runs advance nothing; WO6 writes `run.save` with `pendingProfile`, then the profile; 4.7 shared seeds; 5.2 the consolidated suite.

**ENM:** 2 lists "committed enemy intents and scripted wind-ups" as a non-goal (settled, ABL 5); 13 (agreed) splits the job: Lore tells what an enemy is (its abilities, once seen or revealed), Perception what it is about to do; 13.2 announces boss signature moves to everyone as live forecasts ("if its turn began now"), never commitments, with Gambits (BR-32) the one committed exception; 13.3 (agreed: no committed intents) reads intent against guile, else "Intent unreadable"; 9.2 a type's ability conditions count toward its 2 and the theme's 6; 15.5 enemies never pick up party items; 15.3 a cursed carrier's cost add uses T+1. **ABL:** readied actions are a launch non-goal. **CON 6.1:** one instance per condition per unit; **CON 19:** Chilled plus a `freezing` cold hit transforms into Frozen (a hard condition). **Camp order (CMP 7.11 = DH 13.1):** HP and pools are restored at step 3, lingering conditions end at step 4. **LE 6.15:** tier marks are 1–4 filled diamonds on container and socket previews. **EV 4.4.4 costs:** `payer: hero|anyMember|unit`; the hero pays by default, companions only on options that print "any member"; `PaymentFeasibility`. **SV 5.5:** a personal unravelled run is finalised at WO6 with its progress writes. **W 18.3:** no Shatter on heirlooms. **TK 8.2:** rings stay uncursable until the curse design asks (TK Q2); the Heartward Torc is the named Later curse host.

### 2.2 Every hook placed

| Hook (source) | Placed here |
|---|---|
| ITEMS 14 earning gap; ITEMS 17 row 7 template list | 4.1.2, 5.1 (HL-1, HL-2) |
| SK-4 seam for armour and shield heirlooms | 4.1.4 (HL-3) |
| SK-12 placement | 4.1.4 (read) |
| SK-15 `kitDelta` | 4.5 (read; Oaths declare it) |
| SK-16 shared seeds | 4.1.7 (read; HL-25 request) |
| SK 4.5.2 Oddment pool for heirlooms | 4.1.4 (read: already the default pool) |
| DH 5.8 item 9 legacy hook (Later) | 4.1.2 R2 (HL-7 ⚑, answered at M3) |
| CH-24 run-modifier slot | 4.5 (HL-21 request; agreed, Q6) |
| CH 4.11 hub objects; CH 4.12 reveals | 4.1.3, 4.8 (HL-9, HL-28) |
| IR 4.15 / IR-20 "curse designer" | 4.2 (HL-15, HL-16) |
| IR 6.1 Deeds and bynames | 4.1.5 (HL-5) |
| LE 8.5 / LE-22 | 4.2 (read; wave moved by HL-29) |
| CON 18.10 Cursed variants; desecrated shrines | 4.3 (HL-17 to HL-19) |
| CON 16 condition budget and theme six | 4.3.3, 10 (HL-19, HL-29 reading) |
| EV-43 shrine cures and bargains; EV 4.4.1 Strand "a curse ended" | 4.3.2, 4.3.3 (wave moved by HL-29) |
| ENM 9.2 condition counts; ENM 15.3 carriers; ENM 13.2 forecasts, extended to Malady abilities; ENM 2 and 13.3 (read: no committed intents) | 4.3.3 (HL-19 ⚑; T1) |
| FL 6.11 `BossDefeated`; IR 6.1 / CMP 8.7 Deeds | 4.1.2 R2 (HL-31 ⚑, the `BossFelled` credit) |
| TK 8.2 / TK Q2 rings; Heartward Torc | 4.6 (HL-26) |
| CL 9.1 Hexer / Baned | 11 (hook; no change) |
| HR 9.5 "Cures against boss afflictions" | 4.3.5 (no change; told plainly) |
| CMP 8.7 Deeds, Veteran | 4.1.5 (read) |
| RS 7.4 tone review; RC5 registry | 5.3, 6 |
| SV `curseGate` reservation | 4.2 (read) |

### 2.3 Findings (told plainly)

- **F1.** No approved document says how an heirloom is earned, so the shelf has nothing to fill it.
- **F2.** SK-4 waives the Due only for weapon and focus heirlooms. An armour or shield heirloom hero therefore also gets the U Due: two U pieces entering F2, the outcome SK-4 itself rejected. All five drafts found this independently.
- **F3.** DH 5.8's legacy hook as written (a fallen hero's notable becomes an heirloom) breaks three rules: the U cap (notables reach VR); MAIN 2 (players could farm the best roll into a permanent start); `noFallenHero`.
- **F4.** Hidden curses plus Clinging-on-equip is the NetHack/DCSS gotcha. The approved design softens it (free camp identify; Clinging ends at camp), but the card says nothing about the possibility.
- **F5.** Item-curse stakes are low: curses first appear on F3 in practice (F2's deck holds no R card); at most one per floor; Clinging ends at camp. This design accepts that and puts the curse layer's weight on Maladies.
- **F6.** "Cursed (curse)" as a hero state is a stub: no sources beyond shrines, no cures beyond camp, no names.
- **F7.** The run-modifier slot has no content, no reward rule and no conflict rule.
- **F8.** Several draft names collide: "Geas" (an SRD 5.1 spell; RS 7.6); "Black Bile" (the bile square); "Echo" (IR's Element Echo); a thorned-ring badge (the ENM affix Thorned); a knot badge (the charm kind word "knot").
- **F9.** The drafts said there are 2 final bosses. RS 6.5 says 3 at M3, so there are 3 Heart-won templates at M3.
- **F10 (new lead's call). Five heirloom line names collide with approved terms.** "Undertow" is an Arcanist spell (ARC; RS 7.x rename table keeps it); "Unbowed" is an approved armour property (AR, IR: Bellwarden's Mail); "Braced" is an EP pose word and sits beside the Vanguard's Later ability Brace; "Steady Sight" shares a head word with ARC's Steady Casting; "Called Across" reads as SK's given-name key "Called {givenName}". Fix: renamed to **Low Tide**, **Held Ground**, **Set Heels**, **Long Look** and **Carrying Voice** (all 0 hits); "Padding" (a pipeline term in IR and SV) becomes **Quilted** (5.1; A.2).
- **F11 (new lead's call). "Leaden on your Recovery pool" names no pool.** Recovery is a value (hit points per short rest; STAT 6, DH), not a pool, and approved Leaden acts on "one named pool's maximum". Every class has at least one `rest` and one `camp` pool (ABL 6.2). Fix: Acedia is Leaden on a `rest` pool; Melancholia ("the class pool" in the drafts) is Leaden on a `camp` pool; the pool is the unit's first of that tag in record order, named in the preview; a Malady's Leaden never takes a maximum below 1, so no verb is zeroed (4.3.1, 5.2; V22 (d)). **Superseded after the check (T5):** the check showed that Leaden on a Malady reads as the anergia of depression (NR4b), refills short at camp step 3 and is free on a spent or 1-use pool, so no Malady uses Leaden now; Melancholia and Acedia carry new payloads (5.2).
- **F12 (new lead's call). "Potions heal you 1 die less" misreads DH.** A potion heals the receiver's Recovery, not dice (DH; CN 7.2). Fix: Saltless reads "a potion heals you 2 less (minimum 1)" *(illustr.)* (5.2).
- **F13 (new lead's call). Q5 meets CON 16's budget.** CON 16 caps distinct conditions "in M1 and M2 together" at 32 (full at M2), and CON 18.1 puts Later conditions outside it. Read as written, an M4 Cursed family also sits outside the 32; it still passes CON 20.3's "expired with no effect" gate and the theme six. This is a reading the CON owner confirms (HL-29); fallback: item curses and Maladies stay Later (Q5's rejected option B, used only on refusal).
- **F14 (new lead's call). Q5 drags three Later pieces to M4 with it.** Maladies take shrine sources and cures, so EV-43's shrine curse cures and bargains and the desecrated-shrine Curiosity move to M4; the malign hint (ITEMS 11 item 7) already "ships with curses", so it moves to M4 too (HL-29).
- **F15 (new lead's call). Two folk lines need Later conditions.** Eyebitten uses Conspicuous and Ill-Slept uses Restless (CON 18.10, both Later). Q5 does not move those conditions, so these two lines stay Later; the other five folk lines ship at M4 (4.2, 5.2).
- **F16 (new lead's call). DH stages its legacy hook Later.** R2 answers it at M3, so HL-7 also re-stages DH's "a legacy hook" Later row (flagged).
- **F17 (new lead's call). The drafts' Grip-marks sample used a kit given name as a byname** ("Called Nine Rivets"). Given names live in the run's display sidecar and never leave the run (SK 4.5.1); Grip-marks carry **Deed bynames only** (4.1.5).
- **F18 (new lead's call). Prose and substring hits for new words.** "Oath" has 0 whole-word hits but sits inside Oathbroken (an RS enemy), Oathkeeper (a W protected name) and Oathbinder (an SK rejected example); "Bequest" has 0 hits but LP's Endless Wake prose says "bequests"; "Not yet read" has 1 EV prose hit; "First Cut" and "Answer" appear in prose; "Darned Hymnal" sat beside the Choir theme's hymnal crates and the banned Oddment texture "hymn sheets" (renamed Thumb-Worn Totem after the check, T12); "Kneel" is an EV option verb. All stand as placeholders and are flagged to the naming pass (A.2).
- **F19 (new lead's call). Maladies are not Diseased.** CON's Later Diseased (strain) has stages, worsens and is cured by Treat. A Malady is a Cursed preset: binary, never worsening, never ended by Treat. No Malady uses the Diseased family, and no Diseased strain takes a complaint name (NR5).
- **F20 (new lead's call). The sensitivity read moves with the wave.** Q1 placed it "before Later or M4 content lock"; with Q5 it is due before **M4** content lock.
- **F21 (new lead's call). The Burdened twin's cost line needs a defined pool.** NR5 already keeps Malady words off heirlooms; the cost line is drawn from the eight shippable folk cost lines (the three approved, Ill-Omened, Leaden and Ill-Starred, plus the five new M4 lines), never Clinging (a condition, not a line) and never Eyebitten or Ill-Slept until their conditions ship (4.7).

---

## 3. Models and judges

### 3.1 The models

| Model | Thesis | Kept | Rejected |
|---|---|---|---|
| **A, Opt-in Weight** | every curse is a weight chosen with its full text shown | Cursed-preset Maladies (0 new ids); Cast-off deeds; choice-source rules; Grip-marks; the HL-3 seam fix; Burdened via the modifier slot | the full curse reveal (Q3 option B, rejected); choice-only sources (too rare at 0.29 per run; Q4 option B, rejected); an F5 Curiosity source (impossible) |
| **B, The Bargain** | every heirloom carries a printed Entail, and an oath sets it down | the hereditary-stigma rule; the "Carried with its weight" mark; the folk vocabulary | Weighted heirlooms (a net-R start when the Entail is dodged); Set Down (harder is richer); a new condition id; Wanhope, Somnambulism, Nostalgia |
| **C, Afflictions of the Dreamer** | the curse lives on the dreamer | Maladies as the centre; telegraphed elite sources; companions can be afflicted; rank rings as display | a new `malady (strain)` id; boss sources; Provisos as sources; rank gating |
| **Curses component** | the 28-entry catalogue | the catalogue format; NR9; Graceless; the class-wide chip | Quartan Ague (malaria); the "Saturn's Lead" subtitle |
| **Heirlooms component** | catalogue and power budget | R1–R3; 6 hooks; the mismatch path; the samples; the lever order | "2 Heart-won" (it is 3 at M3) |

### 3.2 Judges' scores (fit / fairness / drama / cost / clarity, out of 10)

| Model | Judge 1 (veteran player) | Judge 2 (rules) | Judge 3 (tone) |
|---|---|---|---|
| A | 8 / 9 / 6 / 7 / 8 | 8 / 9 / 6 / 7 / 8 | 8 / 9 / 6 / 7 / 8 |
| B | 4 / 6 / 9 / 4 / 5 | 4 / 5 / 9 / 4 / 5 | 4 / 5 / 9 / 4 / 5 |
| C | 6 / 7 / 8 / 5 / 7 | 7 / 8 / 8 / 6 / 7 | 7 / 8 / 8 / 6 / 7 |

Judges 1 and 2 chose Model A as the frame. Judge 3 chose Model C's centre of gravity on Model A's chassis. All three rejected Model B's Weighted heirlooms and Set Down.

### 3.3 Model A's chassis with Model C's centre (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

- **Model A's opt-in-weight rules:** ITEMS 14 kept whole; Maladies as Cursed presets; every cost printed.
- **Model C's centre of gravity:** Maladies are where the curse layer bites, including telegraphed elite sources.
- The catalogues of the Heirlooms and Curses components.
- The corrections from three critic passes (Appendix A).

Model B contributes its hereditary-stigma rule and nothing structural (told plainly). **Weighted heirlooms with Entails are rejected** (told plainly): whenever the Entail can be dodged, they are a net-Rare start (8.1).

---

## 4. Rules

### 4.1 Heirlooms (M3)

#### 4.1.1 What an heirloom is (settled: ITEMS 14, IR 4.15, ITEMS 19.1 rule 10)

- An authored, fixed-Uncommon profile template, with a fresh instance each run.
- Flags `heirloom`, `unsellable`, `noSteal`, `oncePerRun`; `identifyDifficulty` 0.
- Banded b1/b2/b3; no rolled line; never grows.
- Display-only value; outside the budget; never bartered.
- Blocks duplicate bases; counts as owned for the Due and for verdicts.
- Lost at the stairs if left.
- Never Shattered (W 18.3) and never picked up by enemies (ENM 15.5).

**Narrow-Uncommon authoring rule (lead's call; change HL-8 ⚑).** Each template carries **one** authored banded line that is strong in a named situation and ordinary elsewhere, priced at the U budget. It is never a generalist best-in-slot.
- V22 (b) `HeirloomValueCap` **(lead's call, after the check:** the draft named no reference item**)**: the template's value equals the U budget; and for every heirloom template T and every authored or rollable U record R of the same family, IR-10's verdict of T against R is ⇄ or =, never ▲ (the SK `KitDueStrictUpgrade` pattern, with R in place of the dealt item).
- So a same-family U find on F2+ can still be a sidegrade (⇄).
- No template line repeats an approved verb or property of another record (V22 (b) `HeirloomNoVerbClone`; lead's call, after the check: the draft's Set Heels repeated the Splint's Stalwart).

**Eligible slot classes (M3):** SK-12's placeable classes: main hand (weapons, foci, two-handers), off hand (shields, off-hand foci) and armour. Trinket heirlooms wait for M4 (4.6).

#### 4.1.2 Earning (change HL-1 ⚑, HL-2 ⚑, HL-31 ⚑; agreed, Q2; MAIN 4.7 read: content unlocks, never power)

Every route is an achievement. None reads deaths, losses, streaks or counts (AN11, EV Q4, SK 4.4).

- **R1, class milestone** (one per class): "Fell the floor-3 boss as a <class>."
  - It is ordered **after** the SK-7 variant milestones ("Reach floor 3", "Fell the floor-2 boss") by difficulty only: it has no prerequisite, and one run often fires all three (change HL-10, a reading of SK 4.4) **(lead's call, after the check)**.
  - Hidden while the class is locked; "seeded runs do not count"; plain words; no percentages.
- **R2, Deed bequest** (answers DH 5.8 item 9; change HL-7 ⚑):
  - **The Core fact (change HL-31 ⚑, a request; lead's call, after the check).** Item Deeds and bynames are display only (IR 6.1, IR 9; V14 (j): no rules code reads them), so R2 is **never keyed to the display Deed**. At each boss kill, beside the canonical `BossDefeated` (FL 6.11), Core emits **`BossFelled{bossId, bossFloor, themeId, killingUnitId, credited[{instanceId, familyId, slotClass}]}`**. Credit, per slot class:
    - a weapon or focus: the one that dealt the killing blow (the IR 6.1 Deed);
    - a shield or armour: the one worn by **any party unit standing (not Downed) when the boss falls**, heroes and companions alike.
  - **Trigger.** In a personal run (the run-end table below), the run's `BossFelled` log holds a credited instance whose base family has an authored R2 template that is still locked, from a boss of that template's floor tier or deeper (change HL-2 ⚑, tier gating: 2 families need an F2+ boss, 2 an F3+ boss and 2 an F4+ boss *(illustr.)*).
  - **Effect.**
    - The family's **authored** U template unlocks when the player hangs it (below).
    - The credited instance is never copied: its rolls, tier and lines are discarded.
    - Kit pieces and companion kit pieces qualify. This pays off SK-5: the plain kit shield that stood at a boss's fall comes home as its family's heirloom, with its Deed byname, if it has one, as the first Grip-mark (4.1.5).
  - **Who computes it (lead's call, after the check).** A **non-Core profile-progress step** runs at WO6, after the run's last Core event. It reads the run's `BossFelled` log, applies the order below and writes the offer; the display layer fills `bynameKey` at that write from the instance's display Deed, if any. This step is the **only** reader of `BossFelled` for profile purposes and the only writer of bequest offers (V22 (h)). Core never reads offers, bynames or Grip-marks.
  - **Worn-piece bynames (a request, inside HL-31).** IR and CMP give a Deed byname only to an item that felled a boss. The IR, CMP and SK owners are asked whether a shield or armour credited by `BossFelled` also earns a Deed byname. Fallback: it earns none; the bequest and its Grip-mark carry the boss and theme marks only. R2 itself does not depend on the answer.
  - **Limits and order (lead's call, after the check).** WO6 writes **at most one** `bequestOffer` per run. When several credited instances qualify, the offer goes to: the deepest boss floor; then the earliest `BossFelled` in event order; then a weapon or focus before a worn piece; then instance creation order. At most one unlock per family per run; **R2 takes precedence over R5** for one family in one run.
  - **When it is chosen (change HL-6, request to SV):**
    - The offer rides `pendingProfile` as the kind `bequestOffer`, then joins the profile's **pending queue, one entry per family** (at most 6 at M3); a pending entry is never overwritten, and a second offer for a family already pending is dropped **(lead's call, after the check:** the draft's "persists until the next offer replaces it" let a chain of "Begin again" runs lose an earned unlock unseen**)**.
    - The player chooses **at the Heirloom shelf on the next hub visit**, using the hub's existing atomic write: **Hang it** (the template unlocks; it takes an empty hook if one exists, else the wrapped board; it never displaces a hung template) or **Set aside** (a recorded decline; the family can be offered again by a later run).
    - **"Begin again" (CH 4.10) skips the hub,** so its inline boon picker shows pending entries with the same two choices, and class pick shows one non-modal line, "An heirloom waits on the shelf." Nothing is lost to skipping.
    - On choosing, the template is re-checked as still locked (another route may have unlocked it); if not, the entry is dropped with no prompt.
    - **There is no Bequest card on the death path, and none on the win path.** Nothing is added to CH-14's `DeathPathSeconds`, and no reward appears in the moments after a permadeath (told plainly: it would read as consolation).
    - If the shelf is not yet revealed (CH 4.12: no reveals after a run-1 death), the queue waits, and the shelf's reveal carries it.
  - **No hero name and no death text** (SK `noFallenHero`; the Unwoken Roll stays EV's single fallen-hero device). Player-facing text never says "bequest" (NR2; lead's call, after the check): the bundle reads "Hang it?".
  - **Reading flagged to the SK owner:** SK 4.5.2's "nothing ever returns as a boon or an item" means "no instance returns". An authored template unlocking is content, which ITEMS 15 allows.
  - **Rejected:** the hook's literal form, where a fallen hero's notable becomes the template. That brings VR notables, rolled lines and farmable permanent starts (told plainly).
- **R3, Heart-won:** the first personal win over **each final boss** unlocks that boss's template.
  - An authored U piece of the boss signature's family, never the signature itself. LE 6.11's Memento and the U cap both hold.
  - **Three at M3** (the Night Governess, the Rousing Engine, the Lullaby). The Lullaby's can be earned only after that boss is unlocked (RT-18) and beaten. Told plainly: the drafts' "2" counted the M2 pool.
  - Its shelf bundle is tied with a thread matching that Memento's frame ribbon: display only; no figure hands it over (AN1, AN10, CH Q2).
  - The drafts' name "Echo" is rejected (IR's Element Echo).
- **R4, Oath win (Later):** the first personal win under each Oath unlocks that Oath's authored template.
- **R5, Redeemed (Later; change HL-24):** wearing a **known** cursed twin when a final boss falls, in a personal win, unlocks that family's **distinct** authored Redeemed template (`unlock.redeemedFrom`): an uncursed U item whose Whence reads "It clung once."
- **Never a source:** a copied instance, a Memento, Grave Goods, any death key, seeded runs, or the simulator.
- **What each run end writes (lead's call, after the check;** the draft listed exclusions only**):**

| Run end | R1, R3 | R2 offer | Grip-marks |
|---|---|---|---|
| Personal win | yes | yes | yes |
| Personal death | R1 yes (R3 needs a win) | yes | yes |
| Personal unravelled (a generation defect, finalised at WO6 with its progress writes, SV 5.5) | R1 for bosses already felled | yes, for `BossFelled` already logged | yes, for marks already earned |
| Abandoned | nothing (`AbandonWritesNoLore`) | nothing | nothing |
| Seeded, shared, replay, simulator | nothing (SV Q1, SV-13) | nothing | nothing |

  An unravelled run is not the player's fault, so it keeps what it earned (told plainly, T13).
- **One WO6, several unlocks (lead's call, after the check).** The profile-progress step writes, and the hub reveals, in this fixed order: SK-7 variant milestones; R1; the R2 offer; R3 (R4 and R5 Later, after R3).
- **Readable goals (change HL-9 ⚑):**
  - The shelf shows every locked template as a paper-cut outline on an empty hook shadow with its plain goal, worded to the credit rule: "Fell an F3+ boss with a crossbow in hand"; "Stand at an F3+ boss's fall with a shield on your arm"; "Stand at an F4+ boss's fall wearing a gambeson".
  - In a run, an eligible item's Whence shows "A boss's fall here would hang it on the shelf (F3+ boss)".
  - No percentages, no counts.

#### 4.1.3 The shelf (change HL-9 ⚑; CH 4.8, 5.2)

- Unlocks are never capped and never deleted.
  - **6 hooks** *(illustr.)* are in view; the player hangs any 6.
  - The rest lie wrapped on the lower board and can be swapped freely between runs.
  - This is a display state, not a stash (ITEMS 10.7 read). **The boon picker lists every unlocked template**, hung ones first; hooks never limit the choice, and hook arrangement stays outside `configHash` **(lead's call, after the check)**.
  - Rejected: a cap with "Return to the dream" deletion, a loss prompt with no gain in decisions.
- Kit-variant bundles, Old Friend and the Wayfarer's Pack keep their own row. The Lone Road toggle is unchanged.
- **Four states that never look alike (lead's call, after the check):** a hung template (on its hook); a stored template (a solid wrapped bundle with a byname tag, on the lower board); a locked template (a paper-cut outline on an empty hook shadow with its goal tag, never cloth); a pending offer (an open bundle reading "Hang it?" with the item's byname, or its family name when it has none). Kit variants keep their own tied row with the class glyph. A CH role-ID gate row covers all four in greyscale (HL-9).
- Each card shows: the exact line; the band; usable-class glyphs (the shelf comes before class pick, SK 4.4); "Replaces: <piece>" per class (SK-12); Grip-marks.
- **Mismatch path:** confirming creation with a class that cannot use the held heirloom reopens the boon picker inline (CH 4.10's "no longer valid" path). Nothing is ever issued unusable.
- The last-picked boon uses CH 4.10's existing "Begin again" carry; "new" flags on newly unlocked templates live in `uiMemory` (CH 4.12).

#### 4.1.4 Entering a run

- **Placement:** SK-12, unchanged (read).
- **The F1 Due (change HL-3 ⚑; SK 4.3, LE 6.6 item 2, the ITEMS 14 placement bullet, V13g (g)):**
  - **Any** heirloom (weapon, focus, shield or armour) sets F1 "arms satisfied". The Due takes the approved 20-coin waiver and never retargets.
  - **Order and predicate (lead's call, after the check;** the approved predicate is a state test on the hero's arms, which an armour or shield heirloom does not meet**):** WO1 kit issue → SK-12 placement → F1 generation's fixed deal (with SK-4's tie-break) → the hero-arms rule, with `armsSatisfied = heroArmsTier ≥ U OR the hero holds an instance flagged heirloom (any slot class, a two-hander and the Burdened twin included)`, read from the issued instances after placement, never from `boons[]` → the waiver (`DreamsDueWaived`; the 20 coin sits in the Due's container slot on F1, the approved waiver path, never as kit coin). V13g (g) gains one case per slot class and one for the Burdened twin.
  - An armour or shield heirloom hero plays F1 with the mundane kit weapon, U defence and 20 coin: parity by value, on a different axis.
  - Report `HeirloomSlotParity` (9.3).
  - **Fallback if refused:** no armour or shield templates at M3. Rows 8–10 of 5.1 are then replaced by three weapon or focus R2 families at the same tiers: a warhammer (F3+), a shortbow (F4+) and a rod (F4+) *(illustr.)*, keeping the 2/2/2 split and 13 templates; `HeirloomCoverage` and the 8.2 table are re-run **(lead's call, after the check)**.
  - SK-4's pending IR, DH and LE confirmations must close before heirlooms ship (SK 10).
  - **Read by the charm abilities design (2026-11-24; a reading, no change here):** the Bobbin's boon charm (`boonCharm`, never `heirloom`) is never arms, so it never sets `armsSatisfied` and never triggers this waiver; a V13g (g) case is requested from LE (CA-26; charm abilities design 4.11).
- **Oddments** (a reading, not a gap): SK 4.5.2's effective pool is the held boon's pool, then the default kit's. An heirloom has no `oddmentPoolId`, so it draws the default pool.
- **"Begin again"** carries the heirloom forward, re-validated (CH 4.10, read).

#### 4.1.5 History without power: Grip-marks (change HL-5 ⚑; IR 6.1, CMP 8.7, SV 4.6 profile memory)

- **Name (lead's call, after the check):** "Grip-marks" replaces the proposal's placeholder "Passings", which reads as "deaths" and shares a head word with the Herald's Passing Bell (T12).
- Record: `gripMarks[templateId] = {shownBynameKey?, earnedBynameKeys[≤8], deedMarks[≤3] (oldest dropped first), marks: set of markKind}`, kept in profile display memory, outside `manifestHash` and `configHash` **(lead's call, after the check:** the draft's single byname could not hold a choice, and its `marks[]` had no set rule**)**.
- **Write point (lead's call, after the check).** The profile-progress step at WO6 appends one `gripMark` entry per earned mark to `pendingProfile`: `{templateId, bynameKey?, bossId, themeId, markKind}`, for personal runs only (the run-end table, 4.1.2). Marks point at `{bossId, themeId, bossFloor}`, never at the run's files, which WO6 deletes.
- **Which template is credited.** A mark goes to the template whose heirloom instance was carried; or, for a `BossFelled` credit on an R2 family, to that family's R2 template, locked or not (a locked template shows its marks once unlocked). A family with both an R2 and a Heart-won template credits only the template actually carried, else the R2 one.
- A credited instance's **Deed byname** joins `earnedBynameKeys`. Whence reads, for example, "Gatesplitter. Felled the Jailer of Hours." The player picks which earned byname to show (checked against the V21 (g) collision pool).
- **Bynames only (new lead's call, F17):** a kit piece's given name ("Called Nine Rivets") lives in the run's display sidecar (SK 4.5.1) and never becomes a Grip-mark; a named piece that earns a Deed passes on its byname only.
- The newest three Deed marks show (boss and theme), the newest replacing the oldest. Marks, never counts (AN11).
- Display marks: "Carried to the night's end" (a personal win while carrying it; renamed after the check, because "Carried out of the Heart" is the Memento's own caption, LE Q1, CH 4.10, SK 4.5.2); "Carried with its weight" (a win while carrying its Burdened twin; Later).
- No hero names, no death text. Seeded runs write nothing. V22 (h): no Core reference.
- **Rejected for M3:** growth in kind (an alternate form after 3 Deeds), because it keys content to repeating one pick. Held for Later.
- Hub art: up to three notches on the grip wrap, and the byname on the bundle's tag.

#### 4.1.6 Loss (change HL-4 ⚑)

- The template is **never** lost (ITEMS 14, 15).
- The instance is lost only if left at the stairs (settled).
- New bar: an heirloom is **never a Curiosity "Item offered" price** (EV 4.4.4), alongside quest items and curses. An offer is a barter by another name.
- Grave Goods lists its fate in the neutral DH voice.
- **Rejected:** loss on death; "resting" a template; any streak-keyed state (AN11; DH 4.1) (told plainly).

#### 4.1.7 Seeds and the key

- The template id travels in `boons[]`, inside `configHash` (SV-12). Template content is in `manifestHash`.
- **Heirloom unlocks stay out of `unlockHash`** (change HL-11, an SV reading). They change no generation pool; including them would show two players with different shelves a spurious "will play differently" notice. Grip-marks and the hook arrangement are display.
- Shared and seeded runs use canonical grants, so no heirloom (SK-16 fallback (a)). The entry screen says so. Read by the charm abilities design (CA-20): the Bobbin charm boon is off on the same runs (charm abilities design 4.11).
- Request HL-25: Later share codes may carry one template id and one `runModifierId`, never Grip-marks.
- **Template supersession (HL-11):** a balance swap maps `supersededBy`, the player's unlock and Grip-marks migrate, and nothing is deleted; a rebalanced line keeps its id and bumps the content version; old replays play under their own `manifestHash`; the profile record carries `heirloomsSchemaVersion`.

### 4.2 Item curses (M4; agreed, Q3, Q5; change HL-29 ⚑)

**Kept whole (settled; only the wave moves):**
- cursed twins only, with the T+1 benefit on a tier-T card;
- `curseGate`, at most 1 per floor; Rare+ gear field cards on F2+ (F3+ in practice);
- every bar;
- Clinging on equip: exempt from the cap, blocks every move out of the slot, ends at camp, at a shrine or by Arcana at a rune;
- free camp identify; the badge once known; carriers apply the benefit only.

**Cure hosts at M4 (lead's call, after the check).** Rune hosts stay Later (ITEMS 11 item 7, ITEMS 24 "Dungeon content"; HL-29 does not move them), and EV-43's shrines move to M4. The approved Clinging rule is unchanged, but every player-facing cure line is **generated from the hosts present in the build**: at M4 it reads "camp or a shrine", and "Arcana at a rune" appears only once a rune host ships (V22 (g) `CureTextMatchesHosts`). No rune host is created by this design.

**Honest risk (change HL-15 ⚑; agreed, Q3; ITEMS 11 item 7 and IR 4.15, presentation only):**
- **Who shows the chip (lead's call, after the check:** the draft's "every unidentified Rare+ gear card on F2+" would have told the truth falsely on cards that can never be cursed**).** The chip is a pure function of public record and channel facts, and never reads whether the card is cursed. It shows on a card when **all** hold:
  - the card is a **field card** from a channel `curseGate` serves (so never an offer or the fourth, the Viaticum, a camp entry, a signature, a fit premium, the arms floor, a Due, a ring, a consumable or an heirloom);
  - the floor is F2+ and the card's tier is Rare or higher;
  - its record's family has an authored cursed twin (public data: some record carries `cursedTwinOf` for it);
  - the card is not yet `known` (the party's passive Arcana is below its `identifyDifficulty`, ITEMS 11);
  - `loot.curse.permille` is non-null.
  - It reads **"Not yet read: may Cling"** (a shape plus words). It states the rule and leaks nothing: V22t `UnreadChipClassWide` is an equivalence test that a cursed and an uncursed instance of one record, on one channel and floor, render the same chip.
  - "Unread" alone is rejected because EV retired it ("The Unread Page").
- The first equip of such a card shows **one** inline, non-modal line per card: "May Cling until camp. Ends at camp or a shrine." (the cure list generated as above). On F5 it reads instead: **"May Cling: no camp remains."** The once-per-profile onboarding line (4.8) is a separate key with its own text.
- The malign hint (threshold missed by 5 or less) ships with curses, at **M4**, as the Arcana reward (F14).
- **Lever (lead's call, after the check;** the draft's lever, "only while passive Arcana is below the threshold", was the chip's own rule**).** If `RareEquipRateDelta` shows the chip deters players from equipping Rares, the chip collapses to its shape alone, with the words on hover and in the inspect pane, once the player has equipped 10 such cards on the profile *(illustr.)*. It still never depends on whether a card is cursed.
- **Rejected:** Q3-B, a full reveal before equipping; Q3-C, silent as approved.

**Folk cost lines (change HL-16 ⚑; ITEMS 13, CON 18.10, LE-22):**
- `whileEquipped` properties, so they use no condition ids.
- Each passes CON 13's buff test and the preview.
- None takes a turn part, removes agency, or acts on squares, and **none applies a condition that is the input of a CON 19 transform into a hard condition** (V22 (g) `CostLineNoHardTransform`; lead's call, after the check: the draft's Ill-Wished applied Chilled, which a `freezing` cold hit turns into Frozen).
- Catalogue in 5.2. **Eyebitten and Ill-Slept stay Later**, shipping with Conspicuous and Restless; the other five ship at M4 (new lead's call, F15).

**Removal (read):** Clinging ends at camp. Keeping a known twin equipped after that is a known trade. Re-equipping applies Clinging again. EV-43's shrine cures and bargains read these rules and move to M4 with them (HL-29).

### 4.3 Maladies: run afflictions on a unit (M4; agreed, Q4, Q5)

#### 4.3.1 Form (change HL-17 ⚑; CON 6.1, 14, 16, 18.10)

- Maladies are named presets of the approved **Cursed (curse)** family, with `displayKind: malady`: **nine new variant ids in the Cursed family, counted once toward CON 16, so 0 new budget entries** **(lead's call, after the check:** the draft's "0 new condition ids" conflated ids with budget entries**)**.
  - Every Malady is its own preset id, even where its payload equals an approved variant (The Spleen's equals Ill-Omened's). So under CON 6.1 (one instance per condition per unit; agreed, read) a Malady never blocks or replaces a desecrated-shrine or item Cursed instance, and they coexist.
- `untilCamp`; binary; no stages; no worsening. Never the Diseased family (new lead's call, F19).
- **At most 1 Malady per unit, in its own slot, exempt from the cap of 2** (as Clinging and Scarred are).
  - Why (told plainly): if a Malady counted in the cap, a mild Malady bought from a Curiosity would *block* every later harmful condition ("Further ones are blocked", CON 14), letting players buy immunity; or an elite's Malady would block a condition the player would rather have had.
  - **A second Malady (lead's call, after the check).** A Curiosity or shrine Malady is payable only while the unit has none, so a player can never pay to swap a bad Malady for a mild one. **An elite's Malady replaces the unit's current one:** the new rule and Cast-off deed apply and deed progress resets (V22t `EliteMaladyReplaces`). Without this, a mild Curiosity Malady paid on the unit the Stratum Card says an elite will target would make that elite land "No effect: already afflicted", buying immunity through the slot itself (T9).
  - With the separate slot, the worst case is 2 lingering conditions + 1 Malady + Clinging, still "drained, never helpless".
  - **Fallback if CON refuses:** Maladies count in the cap, with the card line "This fills a lingering slot" and a `CapBlockedByMalady` report.
- Every payload is numbers, one disadvantage or one trigger. Never a turn part, never agency, never movement down to 0.
- **No Malady lowers a maximum** (a pool's or HP's; V22 (d) `MaladyNeverLowersMax`) **(lead's call, after the check;** supersedes F11's Leaden presets**)**. Three reasons: "less in the tank" is the anergia of depression (NR4b, 0.1); a Malady still active at camp step 3 (CMP 7.11 = DH 13.1) would leave the pool one short after camp, so its effect would cross camp against RS 9.1; and on a spent or 1-use pool it cost nothing. Item Leaden is unchanged (approved; `whileEquipped`).
- A Malady can land on **any party unit**, heroes and companions alike; who pays at a Curiosity follows EV's rule (4.3.3).
- **Warded never blocks a Malady**, because a Malady is lingering (CON 18.9 read). The plate and the preview say "Warded does not stop this."
- RS 9.1's rejection of a curse track stands: no Malady crosses a camp.
- **Runtime state (lead's call, after the check).** Each live Malady is a record in `run.save`'s dynamic state: `MaladyState{unitId, variantId, sourceRef, appliedSeq, deedProgress, fightsWonSinceOnset, endedBy?: camp|shrine|rune|castOff|replaced}`. Save and load keep it exactly (V22t, 9.2 (m)).

#### 4.3.2 Cures (read, plus change HL-18 ⚑)

- Camp (CON 14); a shrine (EV-43, M4); Arcana at a rune (failure gives Weary 1) once a rune host ships (Later; 4.2). Cure text is generated from the hosts in the build.
- **Cast off (HL-18):**
  - Each Malady prints one Core-observable combat deed that ends it at once. The chip shows the deed and its progress.
  - **The actor is the afflicted unit (lead's call, after the check).** The deed counts only when the afflicted unit performs it. Each deed names the Core events it reads (5.2; `castOff{coreEvents[], predicate, alt}` in data).
  - **`CastOffAlwaysReachable`:** every deed has the alternative "or fell an elite, or win 2 fights", which every class at b1 can reach in any state. **"Fell an elite"** means the afflicted unit lands the killing blow on an elite **other than the one that applied this Malady**. **"Win 2 fights"** counts fights won that **began after** the Malady landed. So killing the elite that caused it does not end it, and the 8.3 and 8.4 durations hold.
  - **Told plainly (T8): reachable is not "before the boss".** A Malady taken late on F3 or F4 may have no fights left in which to cast it off and so ride into the boss fight. That is part of its printed price. Two guards: no Malady source acts in a floor's boss antechamber zone (EP filter, `MaladyNotBeforeBoss`), and `MaladyAtBossShare` is reported per floor (9.3), with a lever in 14.2.
- **Remedy, Tend, Treat and Unweaving do not end a Malady** (curse family, read). Told plainly: the Cast-off deed is the lever for parties without a healer.
- EV 4.4.1's Strand "a curse ended" counts a cast-off.
- **Rejected:** a priced camp cure (told plainly). It would be a Sanitarium-style cure sink, and camp already clears Maladies for free.

#### 4.3.3 Sources (change HL-19 ⚑, a request to the EV, ENM, CON, RS and EP owners; agreed, Q4)

Every source is priced or telegraphed. None acts on F1 or F5. Each Malady record lists its sources with **their own floor ranges** (9.1; V22 (d)).

1. **Curiosity cost, F2–F4.** EV 4.4.4 gains a "Malady" cost row.
   - **Which Malady:** drawn StateBlind from `event/{floor}/{site}/malady`, among the presets whose Curiosity source covers that floor.
   - **Who pays (lead's call, after the check;** the draft's "names its target unit" chose no one**):** EV's HP rule. **The hero pays by default**; a companion may pay only on an option that prints "any member", which `curiosityValueTable` prices higher. On such an option the player picks the paying unit when choosing it, among units for which payment is feasible. The cost row carries `payer` (V22 (k) `CuriosityMaladyPayerLegal`; report `MaladyCompanionShare`).
   - **Payment feasibility:** the payer has no Malady and the preset is eligible for that unit. When no unit qualifies, the option is greyed with its reason, as every EV infeasible cost is; this is feasibility, not a leak (V16s `PaymentFeasibility` gains these rows; `CuriosityMaladyOfferLegal`).
   - The Malady is named, and its rule and deed are printed. Never on Leave or the free option. Never stacked with a Proviso on F2.
   - **Sincere on every floor (lead's call, after the check).** The cost row carries `registerLock: sincere`, which V16s `RegisterByFloor` respects; no comic-register string shares a card face with a Malady name, and the comic bus ducks on the card's Malady preview (V22 (g)).
   - **F2 Malady costs sit inside the gated `EventDeathShareDelta` ≤ +0.5 pp** (novice arm, F2 at parties 1–2). If that gate fails, the lever is to start Malady costs on F3.
2. **Shrines:** EV-43 bargains, plus desecrated shrines read as opt-in ("Kneel / Pass by", with the Malady printed). Both at M4 (HL-29). The payer rule is the Curiosity rule.
3. **Elite limited abilities, F3–F4** (Phrenitis on F4 only). Binding agency rules (V22 (d) `MaladyEliteAgency`):
   - **Form (lead's call, after the check).** A Malady is an optional limited-ability slot on an elite record, `maladyAbility?`, not its own enemy type, so the M4 wave switch and the per-floor filter turn the slot off without touching the type. ENM 9.2's 2 per type and the theme's 6 hold with the slot on and with it off.
   - **How the intent is telegraphed (lead's call, after the check; a request to ENM, flagged).** The approved corpus has no committed enemy intents: ENM 2 makes them a non-goal (settled, ABL 5) and ENM 13.3 says "agreed: no committed intents". So the Malady ability uses **ENM 13.2's announced-forecast pattern**, extended from boss signatures to `malady`-flagged elite abilities:
     - from the elite's first turn, its plate lists the ability with its name, shape, save DC and "Warded does not stop this", without waiting to be seen or revealed by Lore (an exception to ENM 13's job split, for this one ability);
     - whenever the elite would use it if its turn began now (the real AI, with 13.3's read simulation), a banner on the initiative bar shows the Malady and its target, **to everyone, whatever the elite's guile**;
     - it is a forecast, never a commitment: if the party changes the state, the banner updates; when the elite's turn comes, the AI decides again. ABL 5's wind-up deferral and ENM 2 stand.
     - This meets Q4's "intent-telegraphed" with the approved forecast mechanism, not a committed intent (told plainly, T1).
   - always a save, with its DC on the plate; the player dodges by moving out of range or line, or by changing what the forecast reads;
   - single target; never an on-hit passive; never an area effect;
   - **targeting (lead's call, after the check):** the scorer never selects a unit that already carries the same preset or for which the preset is ineligible; a unit with a different Malady may be selected, and a hit replaces it (4.3.1). V22t `EliteMaladyNeverTargetsSame`.
   - the danger preview shows it;
   - at most **one Malady-carrying elite type per floor** *(illustr.)*: the EP-owned filter, after placement and with no draw, keeps the `maladyAbility` slot on the first Malady-carrying type in frozen placement order and turns it off on every later one, and off on any elite placed in the boss antechamber zone (`MaladyNotBeforeBoss`);
   - the Stratum Card lists "Maladies abroad: <names>" at floor entry, after the filter (an RS 6.9 request);
   - it counts as the Cursed family toward the type's 2 and **the theme's 6**. A theme must declare Cursed in `harmfulConditions[]` before any of its elites or Curiosities can carry a Malady (V22 `MaladyInThemeSix`). RT-15, RT-16 and EV-34 are pending. Fallback: Curiosity and shrine sources only.
   - **Fallback if the ENM owner refuses the forecast extension:** existing tools only. The ability is listed once seen or revealed by Lore, with its DC; the threat-reach overlay's second tint shows it once seen (ENM 13.1); intent reads follow 13.3 (guile against passive Perception); the Stratum line stays (an RS 6.9 request). Q4's telegraph then rests on the Stratum line and those reads (told plainly, T1).
4. **Never:** bosses (camp follows the boss, and BR-34 keeps Renditions to changes of kind); traps (RT-16 pending); hazards; random imposition; Nightmare Provisos; F1; F5 (told plainly).

Why elites are a source, against Model A's choice-only rule (told plainly; agreed, Q4): choice-only sources give about 0.29 Maladies per run (8.3). Elite sources double that and put the decision into combat, where the player sees the forecast and moves. **Rejected:** Q4-B, choices only.

#### 4.3.4 F5 truthfulness

- No source acts on F5, and camp 4 clears everything, so no Malady reaches F5.
- **The Oath of the Borrowed Physic (Later) acts on F2–F4 only (lead's call, after the check):** the draft's F5 draw had an empty pool (no Malady has an F5 source) and was an F5 exception to the agreed Q4 answer. The draft's "ends at the arena seal" exception is withdrawn.
- V22t `UntilCampTextTruthfulF5` covers Cling lines on F5.

#### 4.3.5 What is not added

- No sanity, stress or madness meter (ENM non-goal). No random tables (CON 2).
- **HR 9.5's "Cures against boss afflictions" is not redefined** (told plainly). The drafts' HL-18 would have made that Herald line promise a cure for something bosses never inflict and Herald cannot end. The line keeps its approved meaning, and Herald text never uses "Malady".
- **Death recap (change HL-20, a request to DH):** the recap's killing chain annotates any roll that a Malady or a cost line changed (e.g. "Choler: advantage"); Grave Goods lists active Maladies and worn cursed items. Both in the neutral voice, from the event log (HZ-13 pattern). Without this, a permadeath partly caused by a curse would be unreadable.

### 4.4 Inherited afflictions: rejected (told plainly)

They are rejected because they are: cross-run penalty or power (EV 4.4.8); streak-keyed state (AN11); against `noFallenHero`; Darkest Dungeon-style quirk grind (MAIN 2); a breach of seed purity; against the spirit of RS 9.1. The only cross-run curse is the opt-in Oath.

### 4.5 Meta-curses: Oaths (Later; agreed, Q6; change HL-21 ⚑, a request to the MAIN/RS owners of the CH-24 slot)

- One opt-in **Oath** in the Depth gauge's reserved run-modifier slot (CH 4.8), chosen at the launcher (CH 4.9). Oaths unlock after the first win.
- **Kind, not stats** (RT-21 extended):
  - never ladder%, HP, damage, caps, pips or loot value (LE-19, LE 6.12);
  - never a duplicate of a Nightmare addition (`OathNotNightmare`);
  - stacks with Nightmare and the Perilous Door, with each pair reported;
  - **never eases a run**; the easier lamp stays separate (EV Q4).
- **F1 guard:** `F1DeathShareByOath` must not exceed the no-boon baseline (95% upper bound under 5%). The drafts' "inert on F1" rule is rejected, because 3 of the 8 catalogue entries fail it.
- An Oath that touches kits declares its `kitDelta` and the DH 21.5 and LE-24 gates it waives (SK-15, V21 (m)); it is reported outside Floor1ClassSpread.
- **Rank rings** on the slot lamp show the highest authored difficulty label (`OathRecord.rank`, a display label set by the writers, never a gate) of any Oath won under; display only **(lead's call, after the check)**. Oaths are **not gated** behind each other, and there is **no third stacking ladder** (told plainly: it would duplicate ladder% and Nightmare and fragment DH 18.2 calibration).
- **Rewards, content only:** an R4 heirloom template, a sigil on that Memento's frame (Framer), and a Book of Nights mark. Never coin, loot, XP or ladder progress. Seeded runs advance nothing.
- **Determinism (change HL-22):** `configHash` gains `runModifierId` (the drafts' `geasId`, renamed), reserved at M1 like `boons[]` and live Later; V19t `ConfigHashCoversRunConfig` covers it. Seeded draws come from `run/oath/{floor}` (DS7).
- **Launcher validation (lead's call, after the check).** On seeded and shared runs, Oaths are disabled until the SV owner rules on HL-25, because `runModifierId` enters `configHash` and the same code would otherwise play differently. Each `OathRecord` declares `requires[]` and `conflictsWith[]`: the Burdened Hand `requires: heirloomBoon` (so it greys out with Bare Board, Old Friend, the Wayfarer's Pack, a kit variant or no boon); the Single Door `conflictsWith` the Perilous Door. Under the Crooked Road both `curseGate` draws are consumed as normal (V16g `FixedDrawCount`, DS7) and only draw 1's result is overridden, and a forced twin carries its benefit at **card tier T, not T+1**, so a party that identifies before equipping cannot make the run richer (LE 6.12; `OathNeverEases` gated on a simulator win-rate check, the Oath held if it fails).
- Lone Road stays a separate shelf toggle. Told plainly: it is a de facto Oath, and the owners may fold it in later.
- **Fallback if the owners refuse or defer (agreed, Q6):** the slot stays unlit until the MAIN/RS difficulty decision; the catalogue (5.2) is held as ready content; R4 and the Burdened twin (which needs the Oath of the Burdened Hand) wait with it. **Rejected:** Q6-B, "leave the slot unlit until the difficulty decision" (the client's words). Told plainly (T14): on the owners' refusal this fallback reaches the same outcome as the rejected B; it differs only in that the request is made first.

### 4.6 Trinkets and charms (change HL-26)

- Rings stay uncursable. This design does **not** ask for Rare rings, so TK Q2 is not reopened.
- The Heartward Torc is the only trinket curse host. Its cursed twin is authored only if the TK owner agrees, and then ships with item curses at M4 (new lead's call, Q5); otherwise there is none.
- Charms are never cursed and never heirlooms (CH-24: charms get no hub surface). Read by the charm abilities design (CA-26, M3): its Bobbin boon is a fresh, uncursed Common charm each run, flagged `boonCharm`, never `heirloom`; its worn-slot row and `KitNoTrinket` waiver follow this section's pattern and are requested from SK, not from this design (charm abilities design 4.11).
- M4 trinket heirlooms are amulets only, placed in an empty worn slot with no displacement. They need an SK-12 worn-slot rule, a V21 `KitNoTrinket` waiver and a TK parity report.

### 4.7 Links (Later)

- **Burdened twin (change HL-23 ⚑):**
  - Each template may author one Burdened twin (`burdenedTwinOf`): the **identical** U benefit plus one folk cost line (one of the eight shippable lines, F21).
  - Reachable **only** by taking the Oath of the Burdened Hand, so it adds challenge, never power, and it is not a second boon.
  - **The cost line binds whenever the twin is worn. Clinging runs until camp 1 only** (approved Clinging, unchanged).
  - On F1 the cost line may not touch HP, healing or the first d20 test.
  - It counts for SK-4 and SK-12. Both texts show on the shelf.
  - Reread of "never cursed": "never a cursed record offered as a plain boon".
- **Redeemed (HL-24):** R5 in 4.1.2.
- **Oath wins feed the shelf (R4).** The Heirloom shelf and the Depth gauge sit side by side as the two pre-run choices.

### 4.8 Onboarding (change HL-28, a request to CH)

First-time inline lines appear once per profile, are recorded in `uiMemory`, and are never modal:
- the first equip of a not-yet-read R+ card: "Some Rare finds cling to you until camp. Read them first, or take the chance." (M4; a profile key distinct from the per-card equip line of 4.2, lead's call, after the check);
- the content note, inline and non-modal, before the first Malady preview of the profile, pointing to the Plain names setting (M4; lead's call, after the check);
- the first Malady chip (M4);
- the first cast-off (M4);
- the first pending offer at the shelf, or in Begin again's boon picker (M3);
- the first Oath lamp lit (Later).

They join CH 4.12's list and the SV 6.11 human-session plan.

---

## 5. Catalogue

All names are placeholders for the naming pass. Every number is *(illustr.)*. Each name was grep-checked across `docs/` (A.2).

### 5.1 Heirloom templates (M3: 13)

Each is U, banded b1/b2/b3, with one line and `identifyDifficulty` 0. The card footer reads "Heirloom · returns to the shelf when the night ends · lost for this night if left behind".

| # | Name | Base family | Route | Line (b1 / b2 / b3) |
|---|---|---|---|---|
| 1 | Tidewrack Spear | spear | R1 Vanguard | **Low Tide** *(renamed from Undertow, F10)*: +1d4 / 1d6 / 1d8 damage against a Teetering or Prone target |
| 2 | Quiet Knife | dagger | R1 Shade | First Cut: your first hit each fight on a foe that has not yet acted this fight deals +1d4 / 1d6 / 1d8 |
| 3 | Old Ash Staff | staff (W 17; two-handed as a focus) | R1 Arcanist (V21 (l) staff row) | Kindly Weight: once per fight, when you end an enemy's concentration, your next spell this fight costs 1 less from its class pool (ARC owner confirms) |
| 4 | Thumb-Worn Totem | Totem (the Herald's off-hand presence focus, HR 7.7) | R1 Herald | **Carrying Voice** *(renamed from Called Across, F10)*: your Rally reaches +2 / +2 / +3 squares (HR owner confirms) |
| 5 | Whetted Arming Sword | longsword | R2, F2+ boss | Answer: once per fight, after you are pushed, pulled or knocked Prone, your next weapon attack before the end of your next turn has advantage |
| 6 | Threshing Flail | flail | R2, F2+ boss | Winnow: when you drop a foe, push one other adjacent foe 1 / 1 / 2 squares |
| 7 | Long-Kept Crossbow | crossbow | R2, F3+ boss | **Long Look** *(renamed from Steady Sight, F10)*: no long-range disadvantage against a target that did not move on its last turn |
| 8 | Door-Plank Shield | shield | R2, F3+ boss | **Set Heels** *(renamed from Braced, F10; rule rewritten after the check, because "forced movement against you is shorter" repeated the Splint's approved Stalwart)*: while you have not moved this turn, your Shove pushes 1 / 1 / 2 squares farther |
| 9 | Patched Gambeson | gambeson (AR, light) | R2, F4+ boss | **Quilted** *(renamed from Padding, F10)*: the first critical hit on you each fight deals 1d6 / 2d6 / 3d6 less |
| 10 | Stair-Worn Byrnie | chain shirt (AR, medium) | R2, F4+ boss | **Held Ground** *(renamed from Unbowed, F10)*: advantage on saves against Frightened (renamed from "Stair-Worn Hauberk" after the check: a hauberk reads as chain mail, heavy, beside AR's Hushmail Hauberk; the naming pass checks "Stair-Worn" for a "Stairtop" pun) |
| 11 | Heart-won (the Night Governess) | the signature's family | R3 | one authored line that counters that boss's mechanic; never equal to the signature's lines (V22 (a)) |
| 12 | Heart-won (the Rousing Engine) | the signature's family | R3 | as 11; e.g. against a boss that pulls, "your first save against forced movement each fight has advantage" |
| 13 | Heart-won (the Lullaby) | the signature's family | R3, after RT-18 unlocks the boss | as 11 |

Each R2 template is keyed to one concrete base family (ITEMS 14 blocks duplicate bases) **(lead's call, after the check:** the draft's Base column named categories**)**. Goal texts follow the `BossFelled` credit rule (4.1.2): rows 5–7 "Fell an F<n>+ boss with a <family> in hand"; row 8 "Stand at an F3+ boss's fall with a shield on your arm"; rows 9–10 "Stand at an F4+ boss's fall wearing a <family>". If HL-3 is refused, rows 8–10 are replaced as 4.1.4 states.

**Coverage** (V22 `HeirloomCoverage`): at least 2 usable templates per class at M3, and each class's R1 template is usable by that class. M4 adds about 11 (including amulets). Later adds R4 (one per Oath) and R5 (one per cursed-twin family in an eligible slot class).

**Lever order.** Triggered if `HeirloomWinDelta` exceeds +3, or if `HeirloomPickShare` exceeds 2× fair share for 2 rounds of at least 30 offers:
1. narrow the line;
2. supersede the template (4.1.7);
3. last resort: remove the 20-coin waiver for that slot. That is an SK-4 change, so it goes back to SK-4's owners.

Never a loot lever (LE-24), and never a cross-run penalty.

### 5.2 Curse catalogue

Every row gives name, register, rule, source, telegraph, cure and wave, with flavour and a fairness note. Every record also carries a `plainNameKey` (the Plain names label, HL-27) and, per shipping locale, an optional `localeTermOverride` (5.3 NR10) **(lead's call, after the check)**. Cure text is generated from the hosts in the build (4.2): at M4, "camp or a shrine"; a rune once one ships.

**Item cost lines** (form: `whileEquipped` on a cursed twin; Clinging is the approved condition variant).

| # | Name | Register | Flavour (≤ 12 words) | Rule | Source | Telegraph | Cure | Wave | Fairness note |
|---|---|---|---|---|---|---|---|---|---|
| 1 | Clinging (approved) | folk | "It has decided it is yours." | no move out of the slot (ITEMS 13) | equipping any cursed twin | the "Not yet read: may Cling" chip; the first-equip line; reveal on take-hold | camp, a shrine (a rune once one ships) | M4 | cap-exempt; ends at camp |
| 2 | Ill-Omened (approved) | folk | "Every first step lands wrong." | first d20 test each fight at disadvantage | `curseGate` twin, Rare+ field card, F2+ (F3+ in practice) | chip, then the Cursed badge and the cost line | unequip once Clinging ends | M4 | — |
| 3 | Leaden (approved) | folk | "It weighs on what you carry." | one named pool's maximum −1 | as 2 | as 2 | as 2 | M4 | the pool is named in the line; item lines only, never a Malady (4.3.1) |
| 4 | Ill-Starred (type) (approved) | folk | "Born under the wrong sky." | weak to one named damage type | as 2 | as 2 | as 2 | M4 | the type is in the line |
| 5 | Ill-Wished | folk | "Someone wished you cold." | your speed is 1 lower in round 1 of each fight *(rewritten after the check: the draft's "start each fight Chilled" fed CON 19's Frozen transform)* | as 2 | as 2 | as 2 | M4 | applies no condition; shown in the move preview |
| 6 | Eyebitten *(renamed from Overlooked after the check: "overlooked" reads as "ignored", the opposite of its rule)* | folk | "Someone has looked at you too long." | Conspicuous is re-applied to you at each fight start | as 2 | as 2 | as 2 | **Later**, with Conspicuous (F15) | Conspicuous's approved interactions untouched (Soaked still ends it) |
| 7 | Short-Armed *(renamed from Widdershins after the check: the name promised reversal, the rule shortens a push)* | folk | "Every push you give falls short." | forced movement you cause moves 1 square fewer (minimum 1) | as 2 | as 2 | as 2 | M4 | shown in the move preview |
| 8 | Saltless (fallback Savourless) | folk | "Physic goes flat on your tongue." | a potion heals you 2 less, minimum 1 *(illustr.; rewritten, F12)* | as 2 | as 2 | as 2 | M4 | the heal preview shows it; close to the Supper's "Pass the Salt" |
| 9 | Black Lot | folk | "Your luck was drawn before you were." | your first critical hit each fight is a normal hit | as 2 | as 2 | as 2 | M4 | the roll log names it |
| 10 | Graceless | folk | "Kindness slides off it." | Graces and event boons do not land on you | as 2 | as 2 | as 2 | M4 | Curiosity previews mark "No effect: Graceless" on that unit |
| 11 | Ill-Slept | folk | "Your rest never quite takes." *(rewritten after the check: the draft's chest-pressure image is sleep paralysis)* | you are Restless at each short rest | as 2 | as 2 | as 2 | **Later**, with Restless (F15) | replaces "Hag-ridden" (gender edge; Hag's pot); its flavour is in the sensitivity read's scope |

**Maladies** (form: Cursed presets, each its own id, `untilCamp`, `displayKind: malady`; never on F1 or F5). Every Malady ends at camp or a shrine (a rune once one ships). Every Cast off is a deed **by the afflicted unit** and also reads "or fell an elite, or win 2 fights" (4.3.2). Sources carry their own floor ranges (9.1). Flavour lines always have the dream as subject (`MaladyFlavourDreamActs`); "bile" never appears in a display string, and the humoral glosses live in `historicalNoteKey` only **(all lead's call, after the check)**.

| # | Name | Register | Sources (floors) | Flavour (the dream acts) | Rule | Telegraph | Cast off (by the afflicted unit) | Wave and notes |
|---|---|---|---|---|---|---|---|---|
| 12 | Choler | complaint | Curiosity F2–3; elite F3 | "The dream heats your choler." | opportunity attacks against you have advantage | Curiosity printed cost; elite plate line, forecast banner and preview | land an opportunity attack | M4 |
| 13 | Pituita | complaint | Curiosity F2–3; shrine F2–3 | "The dream lends you its cold damp." | your initiative has disadvantage | printed cost; shrine card | hit a foe that has not yet acted this round *(rewritten after the check: readied actions are a launch non-goal, ABL)* | M4 |
| 14 | The Spleen | complaint | elite F3; shrine F2–3 | "The dream sours a humour under your ribs." | first d20 test each fight at disadvantage (Ill-Omened's payload under its own preset id) | plate line, forecast banner and preview; shrine card | Frighten or Shove a foe | M4 (kept pending tone review) |
| 15 | Surfeit *(renamed from Plethora after the check: a current clinical sign and everyday English)* | complaint | Curiosity F2–4; elite F3–4 | "The dream fills you over-full of blood." | weak to piercing | printed cost; plate line, forecast banner and preview | drop a foe with a piercing or slashing attack | M4; "the Surfeit" was a cause of death in the old Bills of Mortality, out of clinical use (NR1, cited note) |
| 16 | Melancholia | complaint | Curiosity F3–4; elite F4 | "The dream lends you Saturn's weight." | when forced movement moves you, you also fall Prone (at most once per round) *(rewritten after the check; was Leaden on a `camp` pool, T5)* | printed cost; plate line, forecast banner and preview | knock a foe Prone | M4; **mandatory tone sign-off; fallback Atrabile**; `nameException: Q1Melancholia`, `referentLiving: true` (5.3) |
| 17 | The Glass Conceit | complaint | Curiosity F3–4; elite F3–4 (predicate: the elite's kit deals bludgeoning) | "The dream glazes your bones." | weak to bludgeoning | printed cost; plate line, forecast banner and preview | end a fight having taken no bludgeoning damage | M4 |
| 18 | Tarantism | complaint | Curiosity F3–4; shrine F3–4 | "The dream plays a tune only you can hear." | your opportunity attacks have disadvantage *(rewritten after the check: the draft's stillness penalty mirrored a compulsion to move, NR4b, and stacked on Rooted or Restrained units)* | printed cost; shrine card | drop a foe on a turn in which you moved 3+ squares | M4; **second-look list**, with an NR3 note (named for Taranto; a living folk practice in the Salento); fallback Jactitation (NR1 check pending, A.2) |
| 19 | Acedia | complaint | Curiosity F3–4; shrine F3–4 | "The dream lays a dry heat on your limbs." | at the start of round 3 of each fight, if you are standing, you take 1d6 fire damage *(rewritten after the check; was Leaden on a `rest` pool, T5)* | printed cost; shrine card | hit with an attack in round 1 | M4; second-look list (real doctrine): humoral heat only, no scripture, no noon allusion; fallback Adustion |
| 20 | Phrenitis | complaint | elite F4 | "The dream sets a coal in your blood." | weak to fire | plate line, forecast banner and preview | end an enemy's concentration | M4; second-look list |
| — | Atrabile | complaint | spare | (historical note only: the black humour) | — | — | — | Melancholia's agreed fallback (Q1); an explicit NR6 waiver for its "bile" substring, because the client named it; no display string glosses it as bile (5.3; T7) |
| — | Adustion | complaint | spare | (historical note: a burning of the humours) | — | — | — | Acedia's fallback; replaces Torpor, a living clinical and physiological term (T6) |
| — | Jactitation | complaint | spare | (historical note: restless tossing in fever) | — | — | — | Tarantism's fallback; NR1 and NR1b checks pending (A.2) |

**Plain names labels** (`plainNameKey`; HL-27; each full label unique, `PlainNamesAllSurfaces`): Choler "Malady: Easy Opening"; Pituita "Malady: Slow Start"; The Spleen "Malady: First Stumble"; Surfeit "Malady: Weak to Piercing"; Melancholia "Malady: Falls When Moved"; The Glass Conceit "Malady: Weak to Bludgeoning"; Tarantism "Malady: Guard Pulled"; Acedia "Malady: Third-Round Heat"; Phrenitis "Malady: Weak to Fire". Cost lines: Clinging "Cursed: Clings"; Ill-Omened "Cursed: First Stumble"; Leaden "Cursed: One Use Fewer"; Ill-Starred "Cursed: Weak to <type>"; Ill-Wished "Cursed: Cold Start"; Eyebitten "Cursed: Easy to Spot"; Short-Armed "Cursed: Short Push"; Saltless "Cursed: Weak Potions"; Black Lot "Cursed: No First Crit"; Graceless "Cursed: No Graces"; Ill-Slept "Cursed: Restless Rest" *(all placeholders)*.

**Oaths** (opt-in at the Depth gauge; no cure within a run, by design; reward: an R4 template, a frame sigil and a Book mark).

| # | Name | Register | Oath line | Rule | Source | Telegraph | Cure | Wave | Notes |
|---|---|---|---|---|---|---|---|---|---|
| 21 | Oath of the Burdened Hand | oath | "Carry what was handed down, as it was handed down." | take your heirloom as its Burdened twin | the player, at the launcher | the run-start card, the Stratum Card corner, the Book of Nights | none in the run | Later | `requires: heirloomBoon`: greyed out with Bare Board, Old Friend, the Wayfarer's Pack, a kit variant or no boon |
| 22 | Oath of the Crooked Road | oath | "Take the gift that bites." | from F3, `curseGate` draw 1's result is forced true wherever an eligible twin exists; both draws are still consumed; a forced twin's benefit is at card tier T, not T+1 | as 21 | as 21 | none | Later | the chip still shows; `OathNeverEases` simulator gate (4.5) |
| 23 | Oath of the Shut Book *(renamed from Covered Hand after the check: no two Oaths share a head noun)* | oath | "Know nothing until the fire." | no field identification by passive Arcana; camp identifies free | as 21 | as 21 | none | Later | ITEMS 11's camp-identify client decision untouched; ITEMS 11 items 4–5 and STAT 7.2 field identification are changed for the run (HL-21 ⚑); replaces "Unread Seal" |
| 24 | Oath of the Short Fire | oath | "One rest a floor." | at most one short rest per floor | as 21 | as 21 | none | Later | removes an option, no multiplier; declares the waived DH 21.5 rows (HL-21 ⚑); replaces "Long Watch" (Stillwatch collision; a multiplier) |
| 25 | Oath of the Single Door | oath | "Walk the one way given." | Two Doors off (RS 6.11) | as 21 | as 21 | none | Later | `conflictsWith` Perilous Door; greyed out when Two Doors is off in the build |
| 26 | Oath of the Bare Board | oath | "Begin with nothing handed to you." | no starting boon | as 21 | as 21 | none | Later | equal to the no-boon arm by construction |
| 27 | Oath of the Borrowed Physic *(renamed from Lent Physic after the check: "Lent" is a real liturgical season, RS 7.6)* | oath | "Sleep where the dream sickens you." | on F2, F3 and F4, the hero begins the floor with one seeded Malady, drawn from `run/oath/{floor}` among the presets whose `oath` source covers that floor, and shown on the Stratum Card | as 21 | as 21 | the Malady's own cures | Later | hero only; never on F5 (4.3.4); replaces "Uneasy Bed" (collisions) |
| 28 | Oath of the Iron Sleep *(the "Oath of the ..." form restored after the check)* | oath | — | **held**: elites would gain an authored elite "kind addition" record | — | — | — | held | no such record exists ("Hard Rendition" has 0 hits; Renditions are boss-only, BR-34); an ENM request |

**Rejected with reasons:** A.2.

### 5.3 Naming rules and blocklist (change HL-13 ⚑, HL-30; agreed, Q1)

- **NR1:** a complaint name is an obsolete term (out of clinical use for about 100 years or more), tagged `obsoleteTerm`, with a cited note.
- **NR1b:** the term's referent is not a common living condition.
  - **The one agreed exception: Melancholia** (agreed, Q1). It is a living clinical specifier and its referent is common, so its record is tagged truthfully, `referentLiving: true`, with `nameException: Q1Melancholia`. V22 (e) accepts that tag only for that record and only together with `toneSignOff` and a passed sensitivity read; if either fails, Atrabile replaces it automatically. No other name can pass by this route **(lead's call, after the check)**.
- **NR2:** the blocklist: living diagnoses, slurs, gendered terms, asylum, bedlam, SRD spell names, **SRD and 5E class and subclass feature names (Sacred Oath names such as "Oath of Devotion" and "Oath of Vengeance" included)**, trinket kind words (HL-30), and, for player-facing heirloom strings, "passing", "bequest", "legacy" and "inherit" **(lead's call, after the check)**.
- **NR3:** no eponyms, real saints, peoples or genders, and no toponym that names a living people's practice without review (Tarantism is on the second-look list). Real doctrinal terms go to review. Heirloom template names follow the same gender rule and CH AN4's premise test: no waking-world lineage ("Grandsire's") **(lead's call, after the check)**.
- **NR4:** no suicide, self-harm or premise-leaking terms.
- **NR4b:** no payload reproduces a symptom of the living condition the word evokes, and no mind word deals psychic damage. **No Malady lowers a maximum** (`MaladyNeverLowersMax`), and no flavour depicts a living condition's signature image (the chest-pressure of sleep paralysis, the head-in-hand of grief) **(lead's call, after the check)**.
- **NR5:** register by scope, cross-checked both ways (whole word). No Malady word on any item, heirloom, enemy, Oath or Diseased strain; no oath word on an item or Malady; no folk cost word on a Malady. Every Oath reads "Oath of the <adj> <noun>" or "Oath of the <noun>", and no two Oaths share a head noun (V22 (e)) **(lead's call, after the check)**.
- **NR6:** the collision grep and the shared pool (V14 (f), V21 (g)), whole word and substring, **run on display strings as well as names** (so "bile" in a flavour line fails) and on fallbacks as well as live names. Explicit waivers are recorded with their reason: Atrabile's "bile" substring (the client's named fallback, Q1) **(lead's call, after the check)**.
- **NR7:** the rule line sits above the name on every surface.
- **NR8:** deeper is dreamier. Every Malady flavour line has the dream as its subject (`MaladyFlavourDreamActs`), and no flavour line carries a scriptural allusion (a forbidden-allusion list: "noonday", "demon" and others the tone review adds) **(lead's call, after the check)**.
- **NR9:** one name, one rule. No heirloom line clones an approved verb (`HeirloomNoVerbClone`).
- **NR10:** each name carries a translator note stating its register. A locale may substitute a period term of its own that passes the same rules. **Per-locale fields (lead's call, after the check):** `localeTermOverride{locale: {nameKey, historicalNoteKey, blocklistPassed, collisionGrepPassed, sensitivityReadBy}}`. Each shipping locale's terms pass a locale blocklist, a locale collision grep and a native-speaker sensitivity read before M4 content lock. Translator notes are pre-filled with known traps: Spanish "cólera" is also cholera, a living disease (Choler needs a substitute there); Melancholia, Spleen and similar words are everyday words in Romance and German locales; Italian "accidia" is the deadly sin. For the folk register, notes allow a native folk-belief term or a plain descriptive phrase that passes RC9.

**Safeguards:** the tone second-look list (Melancholia (mandatory sign-off), Acedia, Phrenitis, The Spleen, Tarantism); the sensitivity read "before Later or M4 content lock" (agreed, Q1), due before M4 content lock (F20) and **held at M4 content-complete**, so a fallback can be swapped in before lock (lead's call, after the check); its scope covers Malady names, Malady flavour, folk cost-line flavour and the codex plates; the content note, in Settings and once inline before the first Malady preview (4.8); Plain names with per-record labels (HL-27). **Rejected:** Q1-B, mind-disease names on all curses; Q1-C, fantasy names only.

### 5.4 Display surfaces, sample text

Every string is one whole-string key with named placeholders and translator notes.

- Shelf card: "Quiet Knife · Dagger · Uncommon. First Cut: your first hit each fight on a foe that has not yet acted deals +1d4. Replaces: dagger (A)."
- Locked outline: "Fell an F3+ boss with a crossbow in hand." / "Stand at an F4+ boss's fall wearing a gambeson."
- Pending offer bundle: "Hang it? Gatesplitter." (or the family name when there is no byname), with "Set aside".
- Begin again class pick: "An heirloom waits on the shelf."
- Whence with Grip-marks: "Gatesplitter. Felled the Jailer of Hours." then the Deed marks.
- Not-yet-read chip: "Not yet read: may Cling"; first equip (M4 hosts): "May Cling until camp. Ends at camp or a shrine."; on F5: "May Cling: no camp remains."
- Malady chip (Plain names off): "Choler. Opportunity attacks against you have advantage. Cast off: land an opportunity attack, or fell an elite, or win 2 fights." With Plain names on: "Malady: Easy Opening. Opportunity attacks against you have advantage. ..."
- Elite plate: "Inflicts a malady: Choler (save DC 13; out of reach 4). Warded does not stop this." Forecast banner: "Choler → <unit>". With Plain names on: "Inflicts a malady: Easy Opening".
- Stratum Card line: "Maladies abroad: Choler, The Spleen." With Plain names on: "Maladies abroad: Easy Opening, First Stumble."
- Curiosity cost row: "Pay: Choler on your hero until camp (rule, cast-off shown)." On an "any member" option: "Pay: Choler on any member until camp".
- Recap annotation: "Choler: advantage." Grave Goods: "Afflicted with Choler." / "Wearing a cursed Warhammer (Ill-Omened)."

---

## 6. Art and audio (lead's call)

**Art** (volumes *(illustr.)*):
- **Heirloom models:** about 13 at M3 and about 24 at M4: family re-trims with a **"handed-down" finish** (a worn-smooth grip and a sewn cloth tie, in a pattern distinct from the Memento ribbon and the kit bundle cloth), inside IR 6.2's trim mask and silhouette envelope. "Mended" is rejected (CH's mended blanket means Downed or Scarred).
- **Shelf:** 6 hooks; a lower board of solid wrapped bundles with byname tags (stored templates); paper-cut outlines on empty hook shadows for locked templates, never cloth; open bundles for pending offers; kit variants on their own tied row with the class glyph; Heart-won threads matching Memento ribbons; up to 3 grip notches per template (4.1.3) **(lead's call, after the check)**.
- **Glyphs**, registered in RC5, each a shape plus a word: **heirloom mark = a worn grip band**; **Cursed badge = a cross-barred ring** (placeholder; the thorned and knot rings are rejected); **Malady = a humour-wheel quarter** (placeholder; replaces the draft's "cracked circle", which shared the "cracked" grammar of DH's cracked-heart lethal tag); **not-yet-read chip = a folded page corner** (placeholder; replaces the draft's hollow diamond, which read as an empty LE 6.15 tier mark on the very cards that carry tier marks) **(lead's call, after the check)**. V22 (j) `GlyphsDistinctFromRegistry`: each new glyph passes the role-ID test against the **whole RC5 pattern registry** (LE 6.15 tier diamonds, CON 15 token clusters, BR's Rhythm marks and leash ring, EP's eye-tick ring, LP's floor ring, DH's cracked-heart tag) and against the other new glyphs, including together on a Burdened card and beside tier marks on one card, at greyscale and under the colour-blind filters.
- A tarnish finish on known cursed and Burdened items; never on unidentified items, so nothing leaks.
- **Clinging take-hold:** a brief cord-wrap on the hand, anchored to the UI and the model, never a floor particle (RC1); honours reduced motion; no flash.
- **Afflicted tell (lead's call, after the check):** a fine engraved double arc (two thin concentric lines) on the unit's base ring, in the global skin, never at the head; never hatching, which is Rough terrain's pattern (TL). The draft's "shadow halo" was the cartoon shorthand for depression and placed the Malady in the hero's head. It must pass the greyscale, colour-blind and role-ID gates against condition icons, the Scarred cue and the RC5 base marks.
- **Codex plates:** 9 Malady plates on an "Old Maladies" codex page, briefed as **emblems with no suffering figure**: Saturn's lead and sickle, the humour wheel, each humour's element and season, a glass vessel rather than a glass man. Dürer's Melencolia I is cited for engraving technique only. Sincere, never caricature; no asylum, physician or patient imagery; **no figure in a posture of grief, withdrawal or delusion, and no cloud, halo or shadow at a head** (the forbidden-imagery list) **(lead's call, after the check)**.
- **Oath art (Later):** 8 Oath sigils plus rank rings for the gauge lamp and the Memento frames.

**Audio** **(timbres reassigned after the check, lead's call:** the draft's cloth and leather cues sat inside CH AN9's hub-figure texture families and against each other**).** Every cue stays outside RC7's reserved list, the pending LP-7 string family, the HZ Stir sting and every AN9 hub-figure family (lead-leather creak, soft leather, placard-cloth flap, silk sleeve rustle, ribbon smoothing, bench creak); no bells, chimes or breath (AN9); all recorded in the RS timbre table, tagged for `ReservedCues` and run pairwise in the 85% blind-ID gate.
- Clinging take-hold: a damp clay press (M4).
- Malady onset: a soft, slow low reed swell, no strike and no chime, kept apart from hit foley and BR's Carillon thud (M4).
- Cast off: a dry ash crumble (M4).
- Heirloom pick and offer hung: one wood-on-wood hook settle, in two lengths, gated against the bench creak and the door (M3).
- Oath lamp lit: one low tone (Later).
- The comic bus ducks on any Malady event and on a card's Malady preview (RC8).

---

## 7. New mechanics (lead's call)

1. **The Deed bequest offer** (R2): the Core `BossFelled` credit, read at WO6 by the non-Core profile-progress step; one offer per run, queued one per family, chosen (Hang it or Set aside) at the shelf or in Begin again's picker.
2. **Tier-gated Deed families** (HL-2).
3. **Grip-marks:** display-only history, bynames only.
4. **Template supersession** (`supersededBy`).
5. **The Malady slot:** one per unit, beside the cap of 2; an elite's Malady replaces, a paid one needs an empty slot.
6. **Cast-off deeds** by the afflicted unit, with the always-reachable alternative.
7. **Forecast-telegraphed elite Malady abilities** (ENM 13.2's pattern extended), with the per-floor cap and the Stratum Card listing.
8. **The class-wide not-yet-read chip**, defined on public record and channel facts.
9. **Oaths**, with display-only rank rings.
10. **Burdened twins** and **Redeemed templates**.
11. **Plain names on every surface** (HL-27): every place a name is rendered has a per-record plain label, and the rule line always shows.
12. **No Malady lowers a maximum** (lead's call, after the check; supersedes the draft's Leaden presets, F11).

---

## 8. Numbers (toy, not the simulator; all *(illustr.)*; script kept in the design lead's scratch area, Appendix B)

Assumptions: P(reach F1–F5) = 1 / 0.95 / 0.85 / 0.65 / 0.45; win rate 0.28 for a competent player, 0.12 for a casual one; bosses felled per run ≈ 3.18.

### 8.1 Heirloom power against the other boons

- **Default hero:** mundane kit (0 VP) plus the U Due by mid-F1; coin 15; camp-1 margin 0.
- **Weapon heirloom hero:** U from the first fight, plus 20 coin. The gain is about 3–5 F1 fights at U instead of C, plus 20 coin (about 2/3 of a Remedy). The hero enters F2 with the same VP, and the edge decays at F3 (deck U U U R R R). Expected `HeirloomWinDelta` ≈ +1 to +3 points: inside the +3 cap.
- **Armour or shield heirloom without HL-3:** two U pieces, ≈ +4 to +6. Fails. With HL-3: parity by value; watch F1 deaths for Shade and Arcanist.
- **Model B's Weighted heirloom with a dodgeable Entail:** ≈ +3 to +6. Fails, which is why it is rejected.

| Boon | Gives | F1–F2 value | Decay |
|---|---|---|---|
| Heirloom | a U item + 20 coin | highest | overtaken by R finds from F3 |
| Old Friend | a named companion card at camp 1 | certainty | lasts the run |
| Wayfarer's Pack | +1 belt slot | one more potion | lasts the run |
| Kit variant | a 0-VP sidegrade + 1 skill | lowest by VP | the skill lasts the run |

Told plainly: the heirloom wins early and the other boons win late. That spread is variety; "strictly best" would not be, and the reports guard against it.

### 8.2 Earn rate (M3 roster of 13: 4 R1, 6 R2 tier-gated, 3 R3; the Lullaby's Heart-won only after its first win)

| Runs | Competent | Casual | Untiered R2 (competent) |
|---|---|---|---|
| 10 | 9.0 / 13 | 7.9 / 13 | 10.4 / 13 |
| 25 | 12.0 | 11.0 | 12.5 |
| 50 | 12.9 | 12.4 | 12.9 |

Median runs to a full M3 shelf (competent): about 30.

Told plainly:
- Tier gating slows the shelf only a little. The synthesis's estimate of 6–7 of 12 after 10 runs was optimistic.
- The M3 heirloom runway is about 25–30 runs (about 40–45 h).
- The answer is breadth arriving in later waves (M4 about +11; Later R4 + R5 about +8 to +12), not slower unlocks.
- `BequestOfferTakeRate` is tracked as a display-health measure.
- **After the check (told plainly, T4):** the toy assumed every R2 family earnable through a carried item; under the `BossFelled` credit rule (4.1.2) a shield or armour is credited whenever its wearer stands at a boss's fall, so rows 8–10 now unlock faster than the toy's weapon-like assumption, and the table is a lower bound for them. The builders re-run it with the credit rule before M3.

### 8.3 Curse frequency per run (from M4)

- **Cursed twins:** seen ≈ 0.68 per run (3 floors × 0.35 × reach); worn, at 40%, ≈ 0.27. Under the Crooked Road (Later), about 1.95 seen.
- **Curiosity Maladies:** offered ≈ 0.98 (F2–F4, with 40% of cards carrying the option); taken, at 30%, ≈ 0.29.
- **Elite Maladies landed:** ≈ 0.25 (F3–F4 only, at most one carrier type per floor, 50% fail the save).
- **Total:** ≈ 0.55 per run. Under the Borrowed Physic (Later, F2–F4 only), about 2.5.
- **After the check:** elite Maladies now replace a current Malady instead of bouncing off it, and a Curiosity Malady falls on the hero unless the option prints "any member"; both change the toy only slightly and are left to the simulator.

### 8.4 Death shift against DH 18.2

- A Malady lasts about half a floor (camp clears it; a cast-off rate of 30–60% shortens it). At +12% relative hazard while afflicted, that is about +0.6 death points per run, all on F2–F4. The F1 rate is unchanged and the death shape holds.
- A worn cursed twin is roughly neutral (±0.5).
- `MaladyAtBossShare` is not in the toy: a Malady taken late on F3 or F4 can reach the boss fight (4.3.2, T8).
- Oaths (Later): Borrowed Physic ≈ +3 to +5 (F2–F4); Short Fire ≈ +2 to +3; Single Door ≈ +1. All reported in their own arm.

### 8.5 Cure costs

- Camp clears free.
- A shrine costs a detour (about 1 tick); a rune (once one ships, Later) the same, with Weary 1 on a failed Arcana check.
- Remedy (30 coin) does not apply.

The real price of a Malady is carrying it for about half a floor, or meeting its deed.

### 8.6 Oath pacing

With 8 Oaths, a 0.20 win rate under an Oath, and an Oath taken 1 run in 3, clearing them all takes about 120 runs (about 180 h): a long-tail goal. Rank gating would roughly double it, which is why it is rejected.

### 8.7 Volume

| Wave | Writing | Art | Audio |
|---|---|---|---|
| M3 | ~13 Whence lines; ~60 bynames; 13 lines and goal texts (per credit rule); offer, Set aside, Begin again and Grave Goods lines | ~13 models; 13 bundles; paper-cut locked outlines; heirloom glyph; notches; shelf | 1 cue in two lengths |
| M4 | 9 Malady rule lines, 9 cast-off lines, 9 codex notes (≤ 60 words, cited); 20 plain-name labels; 9 cost lines (5 new); chip, equip and onboarding lines; the content note (Settings and inline); translator notes for every name, pre-filled with locale traps, and per-locale substitute terms with their own notes and checks; **elite Malady abilities: one name, plate line and forecast-banner string per carrier type** *(count from ENM)*; **EV-43 shrine and desecrated-shrine cards** *(count from EV)*; cursed-twin names across every Rare+ gear family; 3 fallback names (Atrabile, Adustion, Jactitation) with plates and notes held ready; ~11 more heirloom texts | ~11 more models; 3 glyphs; Clinging VFX; **a tarnish material pass on every Rare+ gear model**; the base tell; 9 codex emblem plates; **intent icon uses, a cast animation and VFX per carrier type**; **EV-43 shrine and desecrated-shrine props** | 3 cues |
| Later | 8 oath lines; 2 cost lines; R4 and R5 texts | 8 sigils; rank rings; a Burdened variant per template | 1 cue |

In all about 4–5k source words *(raised after the check from 3–4k)*, added to CH risk 13's translation budget. The sensitivity read is held at M4 content-complete, one step before lock, so a swap to a held fallback fits the schedule (lead's call, after the check).

---

## 9. Data and validator V22

### 9.1 Records (*illustr.*)

```
HeirloomTemplate { id, itemRecordId, family, slotClass, band{b1,b2,b3}, narrowLineTag,
  usableClasses[], unlock{ milestoneId | deedFamily+bossTierMin | heartWonBossId | oathId | redeemedFrom },
  hubObjectId, goalTextKey, supersededBy?, burdenedTwinOf? }
Profile heirlooms { heirloomsSchemaVersion, unlocked[], hooks[<=6], gripMarks{},
  pendingOffers[<=1 per R2 family] { templateId, bynameKey?, bossId, themeId, bossFloor, shownAtVisit? },
  declined[] }                                                          // uiMemory holds "new" flags; CH 4.10 holds the last boon
gripMarks[templateId] { shownBynameKey?, earnedBynameKeys[<=8], deedMarks[<=3] {bossId, themeId, bossFloor},
  marks: set<markKind> }                                                // display; bynames only
pendingProfile kind bequestOffer { templateId, bynameKey?, bossId, themeId, bossFloor }  // at most one per run, WO6
pendingProfile kind gripMark { templateId, bynameKey?, bossId, themeId, markKind }       // personal runs, WO6
Core event BossFelled { bossId, bossFloor, themeId, killingUnitId,
  credited[{instanceId, familyId, slotClass}] }                         // beside BossDefeated (HL-31)
CurseCostLine { id, property, value, nameKey, plainNameKey, register: folk, translatorNoteKey,
  localeTermOverride{}?, requiresCondition? }                           // Eyebitten, Ill-Slept: Later
CursedVariant { id, family: curse, displayKind: malady, payload,
  castOff{coreEvents[], predicate, actor: afflictedUnit, alt}, eligibility?,
  sources[{kind: curiosity|shrine|elite|oath, floorsMin, floorsMax, predicate?}],
  nameKey, plainNameKey, historicalNoteKey, translatorNoteKey, localeTermOverride{}?, fallbackNameKey?,
  tags[obsoleteTerm, referentLiving, toneReviewed, toneSignOff?, nameException?] }
MaladyState { unitId, variantId, sourceRef, appliedSeq, deedProgress, fightsWonSinceOnset,
  endedBy?: camp|shrine|rune|castOff|replaced }                         // run.save dynamic state
EliteRecord + { maladyAbility? { variantId, saveDc, range, flags[malady] } }
CuriosityCost + { kind: malady, payer: hero|anyMember, registerLock: sincere }
OathRecord { id, register: oath, kindAdditions[], kitDelta[], waivedGates[], requires[], conflictsWith[],
  rewardTemplateId, sigilId, rank }                                     // rank: an authored display label
RunSnapshot + { runModifierId? }                                        // reserved M1; in configHash
```

Keyed consumers (DS7):
- `event/{floor}/{site}/malady` picks which Malady a Curiosity offers, StateBlind, among presets whose Curiosity source covers the floor. It never draws from the `curiosities` stream, so V16g FixedDrawCount holds.
- `run/oath/{floor}` serves the Borrowed Physic (Later), F2–F4 only.
- The per-floor elite cap and the boss-antechamber rule are an EP-owned filter applied after placement, with no new draw (a request): it keeps the `maladyAbility` slot on the first Malady-carrying type in frozen placement order and turns it off on every later one and on any elite in the boss antechamber zone.
- `BossFelled` is a Core fact; the profile-progress step that reads it at WO6 is non-Core and draws nothing.

### 9.2 V22 `HeirloomsCurses` (registered by HL-14, from M1)

| Row | Phase | Check | Wave |
|---|---|---|---|
| (a) | s | templates are U, authored, flags set, `identifyDifficulty` 0, uncursed except a Burdened twin with an identical benefit; a Heart-won never equals its signature; each R2 template names one concrete base family | M3 |
| (b) | s | `HeirloomValueCap` (4.1.1: value = U budget; never ▲ against any same-family U record); `HeirloomNoVerbClone` | M3 |
| (c) | s | unlock source, hub object, goal text worded to the credit rule (`HeirloomGoalReadable`), `HeirloomKitFits` rows, at least 2 per class (`HeirloomCoverage`), `HeirloomUnlockLegal` (no death, loss or streak keys) | M3 |
| (d) | s | Malady whitelist; `untilCamp`; a Core-observable deed by the afflicted unit, its Core events named, with its alternative (`CastOffAlwaysReachable`); every source range (Curiosity, shrine, elite, Oath) inside F2–F4, each checked on its own; `MaladyEliteAgency`; `MaladyInThemeSix` (with the ability on and off); `MaladyNetHarder`; `MaladyNeverLowersMax`; no payload stacks with an heirloom line so as to zero an approved verb | M4 |
| (e) | s | NameRegister NR1–NR10: blocklist (SRD spell names, SRD and 5E class and subclass feature names, trinket kind words, the player-facing heirloom word list); collision pool on names and display strings, fallbacks included, with recorded waivers; register by scope (whole word); the "Oath of the ..." form and unique Oath head nouns; `nameException` only on the Q1 Melancholia record, with `toneSignOff`; `MaladyFlavourDreamActs`; the forbidden-allusion list; `MaladyNeverOnHeirloom`; `NameKeyHasTranslatorNote`; `PlainNameKeyUnique`; locale overrides carry their checks | M2 (rules), M3 and M4 (rows) |
| (f) | s | `OathKindOnly`, `OathNotNightmare`, `OathNeverEases`; `kitDelta` and waived gates declared; `requires` and `conflictsWith` set | Later |
| (g) | s | cost-line buff test; `CostLineNoHardTransform`; preview coverage, including Graceless; a line with `requiresCondition` never ships before that condition; `CureTextMatchesHosts`; no comic-register string on a card face with a Malady name | M4 |
| (h) | s | no Core reference to `BossFelled` consumers, Grip-marks, hooks or pending offers; the profile-progress step is the only reader of `BossFelled` for profile purposes | M3 |
| (i) | s | each cursed-twin family in an eligible slot class maps to a Redeemed template; active when R5 ships | Later |
| (j) | s | `GlyphsDistinctFromRegistry` (every new glyph against the whole RC5 pattern registry and the others, greyscale and colour-blind filters) | M3 (heirloom glyph), M4 (the other three) |
| (k) | g | `EliteMaladyCapPerFloor`, `MaladyNotBeforeBoss`, `CuriosityMaladyOfferLegal`, `CuriosityMaladyPayerLegal`; `CrookedRoadGateLegal` (Later) | M4; Later |
| (l) | t | `BequestRidesWO6` (including a crash between writes), `BequestPersonalOnly`, `BequestCreditKeyed` (weapon and focus by killing blow; shield and armour by a standing wearer), `BequestOneOfferPerRun`, `BequestOrderStable`, `BequestNeverLostToSkip`, `BequestRecheckLocked`, `BequestCompanionItem`, `RunEndWritesTable`, `SeededWritesNothing`, `HeirloomUnlockNotInUnlockHash`, `HeirloomUnlockSurvivesSwap`, `HeirloomDueWaiverAllSlots` (each slot class and the Burdened twin), `HeirloomNeverOfferPrice`, `HeirloomIdInConfigHash`, `GripMarksBynameOnly` | M3 |
| (m) | t | `RunModifierIdInConfigHash` (field M1); `OneMaladyPerUnit`, `EliteMaladyReplaces`, `EliteMaladyNeverTargetsSame`, `MaladyNeverBlocksCondition`, `WardedNeverBlocksMalady`, `MaladyNeverF1`, `UnreadChipClassWide` (a cursed and an uncursed instance render the same chip), `UntilCampTextTruthfulF5`, `RecapNamesCurseContribution`, `PlainNamesAllSurfaces` (distinct labels on every surface, the codex included), `MaladySurfacesNotHoverOnly`, and save/load stability for Clinging and `MaladyState` (deed progress and fights won included) (M4) | M1 (field), M4 |

The Curiosity Malady cost row must also enter V16s `PaletteWhitelist`, `PaymentFeasibility` (the payer and eligibility rows), `StateBlind` and `RegisterByFloor` (`registerLock`) (a request to EV, M4).

### 9.3 Reports

All reports are calibration class, beside DH 18.2: they inform the client and never fail a build. Each is split by healer and no-healer arm and by party size. Each names its source: **sim** (the simulator), **human** (SV 6.11 sessions) or **telemetry** (profile data; the simulator writes no profile) **(lead's call, after the check:** the draft named reports with no formula, unit or arm**)**. Rates are in percentage points (pp) unless stated.

**Simulator arms (lead's call, after the check).** Beside HL-12's calibration arms, the simulator runs two **control arms** that are never calibration targets: **curse-off** (`loot.curse.permille` null and Maladies off) and **chip-off** (the curse layer on, the chip hidden). They exist only to give the curse reports a baseline.

- **Heirlooms (M3):**
  - `HeirloomWinDelta` (sim) = win rate with heirloom template t − win rate of the no-boon tier-0 arm, per template, in pp; ≤ +3.
  - `HeirloomSlotParity` (sim) = F1 hero death share with an armour or shield heirloom − the same with a weapon or focus heirloom, per class, in pp; within ±3.
  - `F1DeathShareByBoon` (sim) < 5% per boon.
  - `HeirloomPickShare` (telemetry) = picks of template t ÷ shelf views in which t was unlocked and usable, against a fair share of 1/N.
  - `HeirloomVsVariantShare` (telemetry; the SK Q1 / CH-24 trigger); `HeirloomDeathShape` (sim).
  - `BequestOfferTakeRate` (telemetry) = Hang it ÷ (Hang it + Set aside), per family.
- **Item curses (M4):**
  - `CursedTakeRate` (sim) = equips of a **known** cursed twin ÷ known cursed twins seen; 20–60%.
  - `ClingRegret` (sim, human) = Clinging take-holds the player would have refused (sim: an equip later replaced within the same floor; human: a session question) ÷ all take-holds; ≤ 40%.
  - `RareEquipRateDelta` (sim) = Rare+ field-card equip rate, curse layer on − the chip-off arm, in pp.
  - `CostLineBiteRate` (sim) = fights in which a worn cost line changed a roll or value ÷ fights with one worn.
  - `CursedDeathShare` (sim) = deaths with a cursed twin worn, curse layer on − the curse-off arm, in pp.
  - `F5ClingShare` (sim).
- **Maladies (M4):**
  - `MaladyDeathShare` (sim) = F3–4 death share, curse layer on − the curse-off arm, in pp; ≤ +2 (F2 is inside the gated `EventDeathShareDelta`).
  - `MaladyAtBossShare` (sim) = boss fights entered with a living afflicted unit ÷ boss fights, per floor.
  - `MaladyCastOffShare` (sim) = Maladies ended by cast-off ÷ Maladies landed; 30–60%, per class.
  - `MaladyCuriosityTakeRate` (sim, telemetry); `MaladyCompanionShare` (sim, telemetry) = Curiosity Maladies paid by a companion ÷ all paid; `MaladyNetHarder` (sim, dynamic: payment state of each payer); `CapBlockedByMalady` (fallback only).
- **Oaths (Later):** `OathTakeRate` (telemetry), `OathWinRate` per Oath and per Nightmare pair (sim), `F1DeathShareByOath` (sim).

**Calibration arms (change HL-12 ⚑, a DH 18.2 reading):** the base arm is no boon at tier 0; a second arm uses the modal boon mix; from M4 the curse layer is on in both arms; Oath runs form their own arm. The control arms above sit outside DH 18.2.

---

## 10. Staging by wave (agreed, Q5)

| Wave | Content |
|---|---|
| **M1** | Reserve `runModifierId` in the snapshot schema (HL-22); register V22 in the RS 11.2 suite with only the (m) `RunModifierIdInConfigHash` field row live (HL-14) |
| **M2** | Naming rules NR1–NR10 and the blocklist adopted for the naming pass (HL-13, HL-30), with V22 (e)'s rule rows live; string keys reserved; the CON condition-budget reading (HL-29) and the theme-six rulings (RT-15, RT-16, EV-34) requested |
| **M3** | 13 heirloom templates; R1–R3 (HL-1, HL-2); the `BossFelled` Core fact (HL-31); the Bequest via WO6 (HL-6); the HL-3 seam fix (after SK-4 closes); HL-4; Grip-marks (HL-5); HL-7 to HL-12; V22 (a)–(c), the (e) rows, (h), the (j) heirloom glyph and (l), via HL-14 |
| **M4** | **Item curses** (agreed, Q5; HL-29): cursed twins through `curseGate` with `loot.curse.permille` set, Clinging, the chip and equip lines (HL-15), the malign hint, five folk lines (HL-16), the Torc twin if TK agrees (HL-26). **Maladies** (agreed, Q5): HL-17 to HL-20; Curiosity, shrine, desecrated-shrine and elite sources; EV-43 shrine cures and bargains; the Stratum Card line; Plain names and the content note (HL-27); the sensitivity read and tone sign-offs before content lock. Onboarding lines (HL-28). V22 (d), (g), (j), (k), (m). Trinket heirlooms (HL-26); about 11 more templates |
| **Later** | Oaths, after the MAIN/RS difficulty decision (HL-21; agreed, Q6); Burdened (HL-23); Redeemed (HL-24); R4; Eyebitten and Ill-Slept with their conditions (F15); share codes (HL-25); V22 (f), (i); growth in kind (held) |

**Fallback (CON refuses the HL-29 budget reading, or the M4 sensitivity read fails Melancholia and its fallback):** the affected rows move to Later unchanged (Q5-B, used only on refusal) or ship under the fallback name; nothing else moves.

---

## 11. Ownership and hooks

| Owner | What this design needs from them (generated from 13) |
|---|---|
| ITEMS | HL-1, HL-8, HL-15, HL-16, HL-23, HL-24 applied; HL-29 (ITEMS 11, 13, 20 waves) |
| SK | HL-3 (SK-4); HL-10 reading; the `noFallenHero` reading; Grip-marks bynames only (F17); HL-31 (SK-5 named-piece Deeds, confirm) |
| CH | HL-6 (Begin again picker), HL-9, HL-27, HL-28; hub objects; the shelf role-ID gate row |
| SV | HL-6 (the profile-progress step, `bequestOffer`, `gripMark`, the run-end table), HL-11, HL-14, HL-22, HL-25 |
| CON | HL-16 (CON 19 read), HL-17 (CON 6.1 coexistence), HL-18; the Warded reading; HL-29 budget reading (CON 16, 18.1, 21) |
| EV | HL-4, HL-19; HL-29 (EV-43 to M4) |
| ENM / EP | HL-19 (ENM: the 13.2 forecast extension and the `maladyAbility` slot; EP: the per-floor and antechamber filter); the Iron Sleep record (held) |
| FL (Core events), IR, CMP | HL-31: the `BossFelled` event and its worn-piece credit; the worn-piece byname request (IR, CMP; SK confirms) |
| DH | HL-7 (staging row), HL-12, HL-20 |
| RS | HL-13, HL-14, HL-21 (RS 6.11, Later), HL-30; the Stratum line; the "Old Maladies" codex page; tone review; the timbre table rows |
| MAIN/RS difficulty decision | HL-21 (request; agreed, Q6) |
| LE, IR | HL-29 (LE 8.5, LE 11; IR 4.15, IR 10 waves); HL-21 (IR 4.15, LE 8.5 under the Crooked Road, Later) |
| TK | HL-26 |
| HR | the Herald boss-room line: no change (told plainly) |
| CL | hook only: an enemy-facing "curse" button stays CL's and is never named with a Malady term |
| ARC, HR | confirm Kindly Weight and Carrying Voice (5.1) |
| Writing, naming pass | NR1–NR10; the sensitivity read; the A.2 flags |

---

## 12. Binding requirements and how they are met

| # | Requirement | Met by |
|---|---|---|
| 1 | Brief: heirlooms and curses; the naming consideration | 4.1, 4.2–4.5; three registers (agreed, Q1) |
| 2 | MAIN 2 / 4.7: no permanent stat boosts; one boon (decided) | ITEMS 14 kept; content unlocks; Grip-marks display only; Weighted heirlooms rejected |
| 3 | LE 6.12: harder is not richer | Oath rewards are content only; Set Down rejected |
| 4 | RS 9.1: no run-wide curse track | Maladies are `untilCamp` |
| 5 | RS 7.6 watch-list | NR1–NR10; Calenture, Wanhope and "Geas" rejected; no madness-as-monster |
| 6 | RC1: no lying UI | the chip; printed costs; truthful F5 text; recap annotation |
| 7 | RC7 / RC9 | global skin; register by scope |
| 8 | CON: "drained, never helpless"; agency enemy-only | the payload whitelist; no Malady lowers a maximum (`MaladyNeverLowersMax`); no cost line feeds a hard-condition transform (`CostLineNoHardTransform`) |
| 9 | CON 16 budget | Cursed presets: nine new variant ids counted once (0 new budget entries); theme six declared; the M4 budget reading (HL-29) |
| 10 | EV Q4 no mercy; EV 4.4.8 | StateBlind; no death keys; no cross-run power |
| 11 | CH AN11 | marks, never counts; the Bequest off the death path |
| 12 | SV Q1; SV-12 | seeded runs write nothing; `runModifierId` in the key |
| 13 | DH 18.2 | no F1 or F5 sources; gated F2 delta; reports per arm |
| 14 | ITEMS 11 free camp identify (agreed) | untouched; the chip is presentation only (agreed, Q3) |
| 15 | Combat is key | Cast-off deeds by the afflicted unit; forecast-telegraphed elite Malady abilities (ENM 13.2's pattern); heirloom lines that read in fights |
| 16 | Q5 wave move (agreed) | every item-curse and Malady row at M4 (10; HL-29) |

---

## 13. Changes to approved documents

Applied on 2026-11-23, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch (lead's call), and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; as in the SK and CH precedents, the Flag column also marks rows that only *read* such text without changing it. **The client's approval of 2026-11-23 covers the flagged rows** (with Q1–Q6 where a row names one). Told-plainly rows were told in 14.1 and, for those found in the check, 14.3, and confirmed by the client on 2026-11-23. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; recorded as readings, approved by the client with this document on 2026-11-23; each owner confirms its own reading):**
- SK 4.5.2's "nothing ever returns as a boon or an item" means no instance returns.
- Heirloom Oddments use the default pool (SK 4.5.2, read).
- Warded never blocks a Malady (CON 18.9).
- HR 9.5's "Cures against boss afflictions" keeps its approved meaning.
- ITEMS 14's "never cursed" means never a cursed record offered as a plain boon (for HL-23 only).
- CON 16's "in M1 and M2 together" leaves an M4 Cursed family outside the 32, like Later conditions (CON 18.1) (new lead's call; part of HL-29).
- Grip-marks carry Deed bynames only; SK 4.5.1's given names never leave the run (new lead's call).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| HL-1 | ITEMS 14 (new "Earning" bullet), ITEMS 17 row 7, CH 4.11; **MAIN 4.7 (decided, read)** | Earning routes R1–R5 (4.1.2) | ⚑ Decided (read); ⚑ Settled (ITEMS 14) | **Client (Q2, with this document)**; ITEMS, CH owners apply | M3 (R4, R5 Later) |
| HL-2 | ITEMS 14 earning bullet | R2 tier gating; one unlock per family per run; R2 before R5 for one family; at most one offer per run, in the 4.1.2 order | ⚑ Settled (ITEMS 14) | Client (Q2); ITEMS owner applies | M3 |
| HL-3 | **SK 4.3 (SK-4)**, LE 6.6 item 2, ITEMS 14 placement bullet, V13g (g) (settled) | Any heirloom slot (weapon, focus, shield, armour) sets "arms satisfied"; the 20-coin waiver; no retarget; `HeirloomSlotParity` (4.1.4) | ⚑ Settled ("the Due never retargets" kept) | Lead's call; LE owner applies, DH, IR, SK owners confirm | M3 |
| HL-4 | **EV 4.4.4 "Item offered" (agreed register)** | Heirlooms are never a price (4.1.6) | ⚑ Settled | Lead's call; EV owner applies | M3 |
| HL-5 | IR 6.1 Whence, CMP 8.7, SV 4.6 profile memory, V14 (j) | Grip-marks, bynames only (4.1.5) | ⚑ Settled (IR 6.1 Deed-only rule, read) | Lead's call; IR, CMP, SV owners apply | M3 |
| HL-6 | SV write order WO6 and `pendingProfile`; SV 5.5 (unravelled runs); CH 4.10 ("Begin again" boon picker), 4.12 | The non-Core profile-progress step at WO6; `pendingProfile` kinds `bequestOffer` (at most one per run) and `gripMark`; the pending queue, one per family, never overwritten; Hang it or Set aside at the shelf or in Begin again's picker; the run-end table; never on the death path (4.1.2, 4.1.5) | ⚑ Settled (read); ⚑ Agreed (CH 4.10, read) | Request; SV owner rules; CH applies | M3 |
| HL-7 | **DH 5.8 item 9; DH staging "Later … a legacy hook" row (settled)** | The legacy hook answered as a Deed bequest at M3, with no power transfer; the staging row re-staged (4.1.2; F16) | ⚑ Settled | Client (Q2); DH owner applies | M3 |
| HL-8 | ITEMS 14 authoring | The narrow-U rule; `HeirloomValueCap` against every same-family U record; `HeirloomNoVerbClone` (4.1.1) | ⚑ Settled (ITEMS 14) | Lead's call; ITEMS owner applies | M3 |
| HL-9 | **CH 4.8 (hub form, agreed: CH Q1, Q4, Q5)**, 5.2, 4.10 | 6 hooks, the wrapped board, the boon picker listing every unlocked template, the four shelf states (paper-cut locked outlines), goals worded to the credit rule, class chips, the mismatch path, the pending bundle; a CH role-ID gate row (4.1.2, 4.1.3) | ⚑ Agreed (CH 4.8, CH Q1/Q4/Q5, read) | Lead's call; CH owner applies | M3 |
| HL-10 | **SK 4.4 (agreed, SK Q1, read)**, CH 4.11, CH 4.12 | R1 ordered after the variant milestones by difficulty only (no prerequisite); the fixed WO6 write and reveal order | ⚑ Agreed (read) | Lead's call; SK, CH owners confirm | M3 |
| HL-11 | SV 4.6 (reading) | Heirloom unlocks outside `unlockHash`; supersession; `heirloomsSchemaVersion` (4.1.7) | ⚑ Settled (read) | Request; SV owner rules | M3 |
| HL-12 | **DH 18.2 (agreed targets, read)** | Calibration arms; the curse layer in both arms from M4 (9.3) | ⚑ Agreed (read) | Lead's call; DH owner applies | M3 / M4 |
| HL-13 | RS 7.6 (additive), RS 7.4, the naming pass, V14 (f), V21 (g) | NR1–NR10, the blocklist, the second-look list, the Melancholia `nameException`, the collision grep on display strings and fallbacks, `localeTermOverride`; the sensitivity read "before Later or M4 content lock" (agreed, Q1), read as before M4 content lock (F20) and held at M4 content-complete (5.3) | ⚑ Settled (read) | **Client (Q1, with this document)**; RS owner applies | M2 |
| HL-14 | **RS 11.2 validator suite table (settled)**; **SV 5.2 (settled)** | RS 11.2 gains a V22 `HeirloomsCurses` row (waves as 9.2); SV 5.2 becomes V1–V22; registered at M1 with only the (m) `RunModifierIdInConfigHash` field row live, the (e) rule rows live from M2, the rest at their waves | ⚑ Settled (read) | Lead's call; RS, SV owners apply | **M1** (registration) |
| HL-15 | **ITEMS 11 item 7 (agreed, framework decision 4)**, IR 4.15 (presentation) | The class-wide chip on its public-fact population; the per-card first-equip line and the F5 line; cure text generated from the hosts in the build; the chip lever; the malign hint ships with curses at M4 (4.2) | ⚑ Agreed (presentation only; mechanics unchanged) | **Client (Q3, with this document)**; ITEMS, IR owners apply | M4 |
| HL-16 | ITEMS 13, CON 18.10, CON 19 (transforms, read), LE 8.5 | Folk cost lines; none feeds a hard-condition transform (`CostLineNoHardTransform`); Eyebitten and Ill-Slept gated on their Later conditions (4.2, 5.2) | ⚑ Settled (read) | Lead's call; ITEMS, CON owners apply | M4 (two lines Later) |
| HL-17 | **CON 14 (cap bullet), 16, 18.10 (settled)**; **CON 6.1 (agreed, read)** | Maladies as Cursed presets with `displayKind: malady`: nine new variant ids counted once toward CON 16, each its own id so it coexists with another Cursed instance under CON 6.1; their own slot; 1 per unit; an elite's Malady replaces, a paid one needs an empty slot; no Malady lowers a maximum; `MaladyState` in run.save (4.3.1) | ⚑ Settled; ⚑ Agreed (CON 6.1, read) | Lead's call; CON owner applies (fallback 14.2) | M4 |
| HL-18 | CON 18.10 duration and cures | Cast-off deeds by the afflicted unit, with the alternative ("fell an elite" other than the one that applied it; fights that began after onset); cure text from the hosts in the build (4.3.2) | ⚑ Settled (read) | Lead's call; CON owner applies | M4 |
| HL-19 | EV 4.4.4 (`payer`), EV-43, V16s (`PaymentFeasibility`, `StateBlind`, `RegisterByFloor`); **ENM 2 non-goals (settled, ABL 5)**; **ENM 13 job split, 13.1, 13.2, 13.3 (agreed: no committed intents)**; ENM 9.2; CON 16 theme six; RS 6.9 Stratum Card; the EP filter (per-floor cap, boss antechamber) | Malady sources and agency rules: the hero pays by default; the elite ability as an optional `maladyAbility` slot, telegraphed by ENM 13.2's forecast pattern extended to it (listed on the plate from the first turn; never a committed intent; ABL 5's deferral and ENM 2 stand), with the existing-tools fallback (4.3.3, T1) | ⚑ Agreed (EV Q4, read); ⚑ Settled (ENM 2, ABL 5, read); ⚑ Agreed (ENM 13, 13.3: an exception for one ability kind) | **Client (Q4, with this document)**; request: EV, ENM, CON, RS, EP owners rule | M4 |
| HL-20 | DH 17.12, Grave Goods (DH, LE 8.2) | Recap annotation; Grave Goods lines (4.3.5) | ⚑ Settled (read) | Request; DH owner rules | M4 |
| HL-21 | **MAIN 4.7 slot (decided)**, CH-24, CH 4.8, 4.9, RS 9.1, SK-15; for the run under an Oath only: **ITEMS 11 items 4–5 (agreed, framework decision 4)** and **STAT 7.2 (settled)** (the Shut Book), **IR 4.15 and LE 8.5** (`curseGate` under the Crooked Road), **RS 6.11** (Two Doors, the Single Door), **DH 21.5** (waived rows, the Short Fire) | The Oath catalogue, rules, launcher validation (`requires`, `conflictsWith`; off on seeded runs until HL-25) and rewards; fallback: slot unlit until the difficulty decision (4.5) | ⚑ Decided (read); ⚑ Agreed (ITEMS 11); ⚑ Settled (STAT 7.2, IR 4.15, LE 8.5, RS 6.11, DH 21.5); all Later | **Client (Q6, with this document)**; request: MAIN and RS owners rule | Later |
| HL-22 | SV 4.6 (SV-12), V19t | `runModifierId` in `configHash` | ⚑ Settled (read) | Lead's call; SV owner applies | field M1 |
| HL-23 | **ITEMS 14 "Never cursed", ITEMS 19.1 rules 9–10**, IR 4.15, LE 8.5, ITEMS 13 | The Burdened twin, via the Oath only (4.7) | ⚑ Settled | Lead's call; ITEMS, IR, LE owners apply | Later |
| HL-24 | ITEMS 13, LE 8.5, CH 4.11 | Redeemed templates (R5) | — | Client (Q2); ITEMS, CH owners apply | Later |
| HL-25 | SV 4.7, SK-16 | Share codes carry the template id and `runModifierId`, never Grip-marks | ⚑ Agreed (SV Q1, read) | Request; SV owner rules | Later |
| HL-26 | TK 8.2, TK 19, SK-12, V21 | Trinkets and charms; the Torc twin at M4 only if TK agrees (4.6) | ⚑ Agreed (TK Q2 read, not reopened) | Request; TK owner rules | M4 |
| HL-27 | CH settings | Plain names on every surface, with a unique per-record `plainNameKey` label; the content note in Settings and once inline (4.8) | — | **Client (Q1, with this document)**; CH owner applies | M4 |
| HL-28 | CH 4.12, SV 6.11 | First-time lines, including the inline content note and the pending offer in Begin again (4.8) | ⚑ Settled (read) | Request; CH owner rules | M3 / M4 / Later |
| HL-29 | **ITEMS 13 heading "(agreed: curses Later)"; ITEMS 11 items 6 and 7; ITEMS 20 "Later: curses, Clinging and cursed carriers"; CON 18.10 Cursed "minor · Later", CON 18.1 waves table, CON 21 staging (CON 16 reading); IR 4.15 "Curses (Later)", IR 10 Later row; LE 8.5 heading "(Later)", LE 11 Later row; EV-43 shrine curse cures and bargains (Later), EV 10 staging row ("Later: ... curse cures and bargains"), EV 13's EV-43 wave cell; TK 8.2 (Torc, conditional); ITEMS 24 "Dungeon content" (read: runes stay Later)** | Item curses (cursed twins, Clinging, cursed carriers, the malign hint, five folk lines) and Maladies move from Later to **M4**, with EV-43's shrine cures and bargains and the desecrated-shrine source; CON confirms that the M4 Cursed family sits outside CON 16's M1–M2 32 and passes CON 20.3's gate; the curse trunk (CH-24), rune hosts (ITEMS 11 item 7's runes, ITEMS 24), Oaths, the Burdened twin, Redeemed, Eyebitten and Ill-Slept stay Later, so at M4 cures are camp, a shrine and Cast-off (F13, F14, F15, T11) | ⚑ **Agreed** (curses Later); ⚑ Settled (CON, IR, LE, EV, ITEMS 24 waves) | **Client (Q5, with this document)**; ITEMS, CON, IR, LE, EV owners apply; CON confirms the reading | M4 |
| HL-30 | RS 7.6 | Add to the blocklist: SRD spell names; SRD and 5E class and subclass feature names (Sacred Oath names included); trinket kind words; for player-facing heirloom strings, "passing", "bequest", "legacy" and "inherit"; the Malady forbidden-allusion list (5.3) | ⚑ Settled (read) | Lead's call; RS owner applies | M2 |
| HL-31 | **FL 6.11 (`BossDefeated`, settled, read)**; **IR 6.1, IR 9 instance `deeds{}`, V14 (j) (settled: display only, read)**; CMP 8.7 Deeds; SK-5 (named-piece Deeds) | A new Core event `BossFelled` beside `BossDefeated`, crediting the killing-blow weapon or focus and every shield or armour worn by a standing party unit; R2 keyed to it, never to the display Deed, so V14 (j) holds; the request on worn-piece bynames (4.1.2) | ⚑ Settled (FL 6.11, IR 6.1, IR 9, V14 (j), read) | Lead's call, after the check; request: FL (Core events), IR, CMP owners rule; SK owner confirms | M3 |

The options the client rejected (Q1-B and C, Q2-B, Q3-B and C, Q4-B, Q5-B, Q6-B) are not register rows. HL-31 was added after the design check; no id has two meanings (A.3).

---

## 14. Open items for the client

None. Approved by the client on 2026-11-23.

The client approved this document as a whole on 2026-11-23, including the lead's answers of 0.1 and the frame of 3.3 (which the client had accepted by proceeding); every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the 22 lead's calls after the check in 14.3); the told-plainly items (those in 14.1 and "Told plainly after the check" T1–T14 in 14.3, all as written); and the readings recorded in 13; and its approval covers the flagged rows of 13. The six questions raised before writing were answered on 2026-11-23 **(agreed)**, each with the recommended option (A). Writing raised **no new client question**: every new lead's call sits inside the six answers, corrects the proposal's own text against an approved rule, or is flagged against the text it touches in 13. **The design check raised none either**: every lead's call after the check (14.3) implements an answer the client gave, closes a gap the answer left, or corrects a draft rule against an approved one; those that change what the client may have pictured are told plainly (T1–T14). The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them (including the owner-refusal fallbacks and the report-triggered items), and the naming pass remain future items.

### 14.1 This document

**The six answers (agreed, 2026-11-23, final), each the recommended option:**
- **Q1, naming (HL-13, HL-27, HL-30):** option A, three registers by scope: obsolete complaint names on Maladies only, folk words on item curse cost lines, "Oath of the ..." on meta-curses; naming rules NR1–NR10 and the blocklist; Melancholia kept with a weight-only rule, a mandatory tone sign-off and fallback name Atrabile; the display-only Plain names setting, content note and a sensitivity read "before Later or M4 content lock" (the client's words; read as due before M4 content lock once Q5 moved Maladies to M4, F20). Rejected: B, mind-disease names on all curses; C, fantasy names only.
- **Q2, heirloom earning (HL-1, HL-2, HL-6, HL-7, HL-24):** option A, R1 class milestones, R2 Deed bequests from any personal run (win or death, keyed to the Deed, tier-gated, chosen later at the shelf, never on the death path), R3 Heart-won; R4 Oath wins and R5 Redeemed Later. Rejected: B, milestones and Heart-won only.
- **Q3, hidden item curses (HL-15):** option A, the class-wide "Not yet read: may Cling" chip; presentation only; the approved curse mechanics unchanged. Rejected: B, full reveal before equipping; C, silent.
- **Q4, Malady sources (HL-19):** option A, priced Curiosities and shrine bargains plus intent-telegraphed elite abilities on F3–F4; never bosses, traps, hazards or random imposition; never on F1 or F5. Rejected: B, choices only.
- **Q5, waves (HL-29):** option A, item curses and Maladies move to M4 (an agreed-Later item moved by the client); Oaths and other opt-in pieces stay Later. Rejected: B, keep Later.
- **Q6, meta-curses (HL-21):** option A, request the MAIN/RS owners of the CH-24 slot to fill it with Oaths (kind only, content rewards), with a fallback. Rejected: B, leave the slot unlit until the difficulty decision.

**The lead's answers (0.1) and the frame of 3.3** were accepted by the client by proceeding and confirmed by the client on 2026-11-23, with this document, including every told-plainly item below.

**Told plainly (confirmed by the client on 2026-11-23, with this document):**
1. The naming idea is adopted but narrowed: mind-disease names to obsolete humoral and theological complaints, and only on Maladies; never on items, heirlooms, enemies or Oaths (0.1 item 1).
2. Rejected names, with reasons: Calenture (suicide imagery), Wanhope (despair), Nostalgia and Mal du pays (a waking home), Somnambulism, Hypochondria and Dementia (living diagnoses), the Megrims and Quartan Ague (common living conditions), Hysteria, the vapours, lunacy, mania and madness (gendered terms or slurs), St Vitus' dance (a real saint) (0.1, A.2).
3. "Geas" is rejected as an SRD spell name (RS 7.6); the meta-curse word is "Oath".
4. "Black Bile" is rejected for its collision with the approved bile square; Melancholia's fallback is Atrabile.
5. The reward is breadth, not power; the M3 shelf fills in about 25–30 runs (0.1 item 4, 8.2).
6. Inherited afflictions across runs are rejected (4.4).
7. Weighted heirlooms with Entails, and Set Down, are rejected (3.3, 8.1).
8. Heirloom loss on death is rejected (4.1.6).
9. A priced camp cure is rejected (4.3.2).
10. Boss-inflicted Maladies are rejected (4.3.3).
11. DH 5.8's legacy hook is not taken literally: no fallen hero's notable becomes a template (4.1.2).
12. No Bequest card appears on the death or win path (4.1.2).
13. There are 3 Heart-won templates at M3, not 2 (2.3 F9).
14. With the separate Malady slot, the worst case is 2 lingering conditions + 1 Malady + Clinging (4.3.1).
15. Remedy, Tend, Treat and Unweaving do not end a Malady; the Cast-off deed is the no-healer lever (4.3.2).
16. Elite sources double Malady frequency against choice-only sources (4.3.3, 8.3).
17. HR 9.5's line keeps its meaning; Herald text never uses "Malady" (4.3.5).
18. No third stacking ladder; Oaths are not rank-gated (4.5).
19. Lone Road is a de facto Oath; the owners may fold it in later (4.5).
20. Item curses stay low-stakes (F3+, at most 1 per floor, camp clears them); the curse layer's weight sits on Maladies (2.3 F5).
21. The research is from recall and must be checked before any claim is quoted (1; Appendix B).

**New lead's calls (made while writing; confirmed by the client on 2026-11-23, with this document):**
1. **Five heirloom line renames and one more (F10):** Undertow → Low Tide (an ARC spell); Unbowed → Held Ground (an approved armour property); Braced → Set Heels (EP pose word; VG's Later Brace); Steady Sight → Long Look (ARC's Steady Casting); Called Across → Carrying Voice (SK's "Called {givenName}" key); Padding → Quilted (a pipeline term). All 0 hits (5.1, A.2).
2. **Leaden Maladies by pool tag (F11):** Acedia was Leaden on a `rest` pool (Recovery is a value, not a pool) and Melancholia on a `camp` pool, floored at 1. **Superseded after the check** (14.3 item 12, T5): no Malady lowers a maximum (`MaladyNeverLowersMax`).
3. **Saltless rewritten against DH (F12):** "a potion heals you 2 less (minimum 1)" *(illustr.)*, because a potion heals Recovery, not dice (5.2).
4. **The M4 condition-budget reading (F13):** an M4 Cursed family sits outside CON 16's M1–M2 32, like Later conditions, and passes CON 20.3's gate; CON confirms; fallback: the rows go Later (HL-29, 10).
5. **Q5 carries EV-43, the desecrated shrine and the malign hint to M4 (F14)** (4.2, 4.3.3, HL-29).
6. **Eyebitten and Ill-Slept stay Later (F15)** with Conspicuous and Restless; five folk lines ship at M4; `requiresCondition` in data and V22 (g) (4.2, 5.2, 9.1).
7. **The DH legacy hook's Later staging row is re-staged by HL-7 (F16)** (13).
8. **Grip-marks carry Deed bynames only (F17):** the Whence sample is "Gatesplitter. Felled the Jailer of Hours."; `GripMarksBynameOnly` (4.1.5, 9.2 (l)).
9. **Prose and substring hits flagged, not renamed (F18):** Oath (Oathbroken, Oathkeeper, Oathbinder), Bequest (LP prose; now internal only), Not yet read (EV prose), First Cut, Answer, Kneel (an EV option verb) (A.2). The draft's Darned Hymnal (Choir hymnal crates; the hymn-sheet texture ban) was renamed Thumb-Worn Totem after the check (T12).
10. **Maladies never use the Diseased family (F19);** NR5 extended to Diseased strains (4.3.1, 5.3).
11. **The sensitivity read is due before M4 content lock (F20)** (0.1, 5.3, 10).
12. **The Burdened twin's cost line comes from the eight shippable folk lines, never Clinging, Eyebitten or Ill-Slept (F21)** (4.7).
13. **The Heartward Torc's twin follows the item-curse wave (M4) if TK agrees** (4.6, HL-26).
14. **Waves made consistent under Q5:** every item-curse and Malady row reads M4 in 0, 4, 5.2, 6, 8.7, 9.2, 10 and 13; V22 (j) split M3 (heirloom glyph) / M4 (the other three); (k) split M4 / Later (Crooked Road).
15. **The curse catalogue gives every row its register, rule, source, telegraph, cure and wave in full** (5.2), and the naming rules move from the appendix to 5.3; display samples added (5.4).
16. **The 13 register has Flag and Approval columns in the SK shape**, with read flags on HL-5, HL-6, HL-10 to HL-14, HL-16, HL-18 to HL-22, HL-25, HL-26, HL-28 and HL-30, which the proposal's register did not flag. **After the check**, HL-2, HL-8 (Settled, ITEMS 14) and HL-9 (Agreed, CH 4.8) are flagged too, HL-17, HL-19, HL-21 and HL-29 name every section they touch, and HL-31 is added (14.3 item 20).

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a lead's call (confirmed by the client on 2026-11-23, with this document) unless marked.

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| HL-3 refused | LE, DH or IR refuses the any-slot waiver | No armour or shield templates at M3; rows 8–10 of 5.1 become a warhammer (F3+), a shortbow (F4+) and a rod (F4+) R2 family *(illustr.)*; `HeirloomCoverage` and 8.2 re-run (4.1.4) | Lead; LE owner |
| `BossFelled` refused | FL, IR or CMP owners refuse HL-31's Core fact or its worn-piece credit | Weapon and focus R2 families keyed to the killing blow only; no armour or shield R2 templates at M3 (the same replacement families as HL-3's fallback) | FL, IR, CMP owners; lead |
| Worn-piece bynames | IR, CMP or SK owners refuse a byname for a credited shield or armour (HL-31) | The bequest and its Grip-mark carry the boss and theme marks only; R2 itself unchanged | IR, CMP, SK owners |
| Elite forecast | ENM owner refuses the 13.2 forecast extension (HL-19) | Existing tools only: the ability shown once seen or revealed by Lore; the threat-reach tint; 13.3 reads; the Stratum line (4.3.3, T1) | ENM owner; lead |
| Maladies at the boss | `MaladyAtBossShare` high, or `MaladyDeathShare` above baseline +2 pp at boss fights | Every Malady also ends at the arena seal (`ArenaEngaged`, the 4.3.4 Grace precedent), and the chip reads "until camp or the arena seal" | Lead; EV, BR owners |
| SK-4 confirmations | IR, DH, LE confirmations still open at M3 | Heirlooms do not ship until they close | LE, DH, IR owners |
| Bequest write | SV refuses `bequestOffer` at WO6 (HL-6) | R2 ships without the offer: the unlock applies directly at WO6, shown at the next shelf visit, still never on the death path | SV owner; lead |
| Unlocks and `unlockHash` | SV refuses HL-11 | Unlocks enter `unlockHash`; the "will play differently" notice states "different shelves" plainly | SV owner |
| Malady slot | CON refuses HL-17 | Maladies count in the cap; "This fills a lingering slot"; `CapBlockedByMalady` | CON owner; lead |
| Theme six | RT-15, RT-16, EV-34 rule against | Curiosity and shrine sources only | CON, RS, EV owners |
| M4 budget reading | CON refuses HL-29's reading | Item curses and Maladies stay Later unchanged | CON owner; client informed |
| Tone | the sensitivity read or tone review fails a name | The held fallback name (Atrabile for Melancholia, Adustion for Acedia, Jactitation for Tarantism, Savourless for Saltless) or a new name under NR1–NR10 | Writing; lead |
| F2 Curiosity Maladies | `EventDeathShareDelta` gate fails | Malady costs start on F3 | Lead; EV owner |
| Elite sources feel imposed | `MaladyDeathShare` above baseline +2 pp, or sessions | Elite sources cut back to F4, then off (Q4-B) only with the client | Client |
| Chip deters Rares | `RareEquipRateDelta` (against the chip-off arm) | The chip collapses to its shape alone, with the words on hover and in the inspect pane, after 10 such equips on the profile *(illustr.)*; never dependent on whether a card is cursed (4.2) | Lead |
| Item curses dead | `CursedTakeRate` below 20% | Benefit budget reviewed with LE; never a loot lever | Lead; LE owner |
| Heirloom dominance | `HeirloomWinDelta` > +3 or `HeirloomPickShare` > 2× fair share | The 5.1 lever order; the last step returns to SK-4's owners | Lead; SK-4 owners |
| Oaths | MAIN/RS owners refuse or defer HL-21 | Slot unlit; catalogue held; R4, Burdened and the Borrowed Physic wait (on refusal, the same outcome as the rejected Q6-B, T14) | MAIN, RS owners |
| Crooked Road eases | the `OathNeverEases` simulator win-rate check fails | The Oath is held | Lead |
| Iron Sleep | ENM rules on an elite kind-addition record | Oath 28 joins or stays held | ENM owner |
| Torc twin | TK owner rules (HL-26) | No trinket curse host if refused | TK owner |
| Recap | DH refuses HL-20 | Grave Goods lists curses only; the recap risk stays in 15 | DH owner |
| Share codes | SV rules on HL-25 | Fallback: share codes carry neither | SV owner |
| Naming pass | scheduled | Every placeholder, with the A.2 flags | Naming pass |

### 14.3 After the design check

The design check (builder, consistency, fairness and narrative-art lenses) was answered inside the client's six answers. No client answer is overridden; where a fix narrows or re-reads an answer's wording, it is told plainly below.

**Lead's calls after the check (confirmed by the client on 2026-11-23, with this document):**
1. **The `BossFelled` Core fact and the profile-progress step (HL-31 ⚑, request):** R2 keys to a Core event at each boss kill, never to the display Deed (IR 6.1, IR 9, V14 (j)); weapon or focus by the killing blow, shield or armour worn by a standing party unit; a non-Core step at WO6 is the only reader and fills `bynameKey` from the display layer (4.1.2, 9.1, V22 (h)).
2. **Bequest cardinality, order and life cycle:** at most one offer per run, by deepest boss floor, then earliest event, then weapon before worn piece, then creation order; R2 before R5; a pending queue, one per family, never overwritten; Hang it or Set aside; a re-check that the template is still locked; the pending offer shown in Begin again's picker and as a class-pick line (4.1.2, 9.1).
3. **The run-end table and the WO6 order:** personal unravelled runs keep R1, R2 and Grip-marks already earned; abandoned and seeded runs write nothing; one fixed write and reveal order (4.1.2; HL-6, HL-10).
4. **Goal texts worded to the credit rule**, locked templates as paper-cut outlines, and the four shelf states kept apart in greyscale (4.1.2, 4.1.3, 6; HL-9).
5. **The boon picker lists every unlocked template**; hooks are display; Hang it never displaces a hung template; `uiMemory` "new" flags and CH 4.10's last-boon carry (4.1.3).
6. **`HeirloomValueCap` defined against every same-family U record** (never ▲), and **`HeirloomNoVerbClone`** (Set Heels rewritten off the Splint's Stalwart) (4.1.1, 5.1).
7. **The F1 Due order and predicate** (WO1 → SK-12 → fixed deal → `armsSatisfied` read from issued instances, any slot class and the Burdened twin → waiver in the Due's container slot), and **named replacement families** if HL-3 is refused (4.1.4, 14.2).
8. **Grip-marks data and write point:** the `gripMark` pendingProfile kind; credited template rules; a list of earned bynames; a three-entry FIFO of Deed marks; a set of marks; references to boss and theme, never to deleted run files (4.1.5, 9.1).
9. **A concrete base family on every template** (ITEMS 14 blocks duplicate bases): the Herald's Totem for row 4; gambeson and chain shirt for R2 rows 9–10, the latter now the Stair-Worn Byrnie (5.1).
10. **The chip's population on public facts** (a `curseGate` field channel, F2+, Rare+, a family with an authored twin, not yet known, `permille` non-null), an equivalence test, one first-equip line per card, a separate onboarding key, and a lever that differs from the rule (4.2; T3).
11. **Cure text generated from the hosts in the build:** at M4, camp or a shrine; a rune only once a rune host ships (4.2, 4.3.2; HL-29; T11).
12. **No Malady lowers a maximum** (`MaladyNeverLowersMax`): Melancholia and Acedia take new payloads (5.2; T5).
13. **The Malady slot:** an elite's Malady replaces the current one and resets deed progress; a paid Malady needs an empty slot; each preset its own id, coexisting with other Cursed instances under CON 6.1; `MaladyState` in run.save (4.3.1; HL-17; T9).
14. **Cast-off actors and windows:** the afflicted unit acts; "fell an elite" excludes the one that applied it; "win 2 fights" counts fights that began after onset; each deed names its Core events (4.3.2, 5.2).
15. **Curiosity Maladies:** drawn StateBlind among presets whose Curiosity source covers the floor; the hero pays by default, a companion only on an "any member" option priced higher; feasibility rows in V16s; `registerLock: sincere` (4.3.3; T10).
16. **Elite Malady abilities:** an optional `maladyAbility` slot; ENM 13.2's forecast pattern extended to it, with the existing-tools fallback; targeting never picks a unit that already carries the same preset or is ineligible; the EP filter keeps the first carrier in frozen placement order and none in the boss antechamber zone (4.3.3; HL-19; T1, T8).
17. **Per-source floor ranges** in the Malady record, with predicates (e.g. bludgeoning kits); the Borrowed Physic limited to F2–F4 (4.3.4, 5.2, 9.1).
18. **Oath launcher validation:** off on seeded and shared runs until HL-25; `requires` and `conflictsWith`; the Crooked Road consumes both draws and grants its forced twin at card tier T; rank as an authored display label (4.5, 5.2).
19. **Naming corrections** (5.2, 5.3, A.2; T6, T7, T12): flavour lines with the dream as subject and no scriptural allusion; "bile" kept out of display strings; Surfeit for Plethora; Adustion for Torpor; Jactitation held for Tarantism, whose rule and flavour are rewritten; Ill-Wished applies no condition; Pituita's deed avoids readied actions; Eyebitten, Short-Armed, Grip-marks, Thumb-Worn Totem, Long-Kept Crossbow, Shut Book, Borrowed Physic, the Iron Sleep form; the Melancholia `nameException` tagged truthfully; NR2, NR3, NR5, NR6, NR8 and NR10 extended (with `localeTermOverride`).
20. **Register hygiene:** flags on HL-2, HL-8, HL-9; full section lists on HL-17, HL-19, HL-21, HL-29, HL-30; HL-14 registered at M1; HL-31 added (13).
21. **Art and audio:** glyphs off the diamond and ring families with `GlyphsDistinctFromRegistry`; the afflicted tell on the base, never at the head; emblem-only codex plates and a forbidden-imagery list; cues moved outside CH AN9's families; Plain names labels per record; the content note shown inline once (5.2, 6, 4.8; HL-27, HL-28).
22. **Reports:** each with a formula, unit, source (sim, human, telemetry) and comparison arm; curse-off and chip-off control arms; `MaladyAtBossShare`, `MaladyCompanionShare` (9.3); the M4 volume and the sensitivity-read timing (8.7).

**Told plainly after the check** (none overrides a client answer; confirmed by the client on 2026-11-23, all as written):
- **T1. Q4's "intent-telegraphed" uses the approved forecast, not a committed intent.** The corpus has no committed enemy intents (ENM 2, settled with ABL 5; ENM 13.3, agreed). The elite Malady ability is announced to everyone as a live forecast, ENM 13.2's boss-signature pattern extended to it (a request to ENM). If ENM refuses, the telegraph rests on the Stratum line and the existing reads.
- **T2. Q2's "keyed to the Deed" is keyed to the Core fact the Deed records.** Deeds are display only and no rules code may read them (V14 (j)), so R2 reads `BossFelled` instead. For weapons and foci this is the same event as the Deed; for shields and armour, which never land a killing blow, the credit is "worn by a standing unit when the boss falls", a new rule requested from the FL, IR and CMP owners (HL-31). Whether such a piece also gets a byname is a separate request; R2 does not depend on it.
- **T3. Q3's chip is class-wide over cards that can be cursed,** not "every unidentified Rare+ card": a card from a channel `curseGate` never serves, a ring, or a family with no authored twin never shows it, because "may Cling" would be false there (RC1). Whether a given card is cursed is still never read.
- **T4. The earn-rate toy (8.2) is a lower bound** for the shield and armour families under the credit rule; the builders re-run it.
- **T5. Melancholia and Acedia changed payloads.** The proposal's Leaden on a pool read as the anergia of depression, left a pool one short after camp, and cost nothing on a spent pool. Melancholia's rule is still weight only, as Q1 agreed: when forced movement moves you, you also fall Prone (once per round). Acedia now takes a round-3 heat. No Malady lowers a maximum.
- **T6. Plethora and Torpor fail NR1** (a current clinical sign; a current physiological term). Plethora becomes Surfeit; Acedia's fallback becomes Adustion.
- **T7. Two naming exceptions are recorded, not hidden.** Melancholia is the one agreed exception to NR1 and NR1b and is tagged truthfully (`referentLiving: true`); Atrabile, the client's named fallback, keeps "bile" as a substring under an explicit NR6 waiver, while no display string uses "bile".
- **T8. A Malady can reach a boss fight.** One taken late on F3 or F4 may have no fight left to cast it off. No source acts in the boss antechamber zone, `MaladyAtBossShare` is reported, and a lever (end at the arena seal) is held in 14.2.
- **T9. An elite's Malady replaces the unit's current one.** Otherwise a mild paid Malady would buy immunity from the elite the Stratum Card names.
- **T10. The hero pays a Curiosity Malady by default** (EV's HP rule); a companion pays only where the option prints "any member", priced higher. Otherwise every paid Malady would land on a companion, who is never killed.
- **T11. At M4 a Malady or Clinging ends at camp, at a shrine or by its Cast-off deed; there is no rune cure** until a rune host ships (ITEMS 24, Later). The cure text says only what the build holds.
- **T12. Several placeholders were renamed** for tone, truth or collision: Passings → Grip-marks (it read as "deaths"; the Passing Bell); "bequest" is internal only ("Hang it?"); Overlooked → Eyebitten; Widdershins → Short-Armed; Darned Hymnal → Thumb-Worn Totem; Grandsire's Crossbow → Long-Kept Crossbow; Stair-Worn Hauberk → Stair-Worn Byrnie; Plethora → Surfeit; Oath of the Lent Physic → Borrowed Physic; Covered Hand → Shut Book; Oath of Iron Sleep → Oath of the Iron Sleep; the Memento's caption is no longer reused. All are still placeholders for the naming pass.
- **T13. A personal unravelled run keeps what it earned** (R1, R2 and Grip-marks for bosses already felled): a generation defect is not the player's fault (SV 5.5).
- **T14. Q6's fallback reaches the rejected B's outcome on refusal.** If the MAIN/RS owners refuse the Oaths, the slot stays unlit until the difficulty decision, as B would have left it; A differs only in that the request is made first.

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | Tone: Malady names may still offend (Melancholia is a living clinical specifier, tagged `referentLiving: true` as the one agreed exception; Acedia is real doctrine; Tarantism names a place and a living folk practice) | Second-look list, sensitivity read held at M4 content-complete, fallbacks held ready (Atrabile, Adustion, Jactitation), flavour with the dream as subject, emblem-only plates, the content note shown inline, Plain names with per-record labels |
| 2 | CON may refuse the Malady slot, the theme-six counting or the M4 budget reading | Stated fallbacks (14.2) |
| 3 | Elite sources may feel imposed despite telegraphs | `MaladyDeathShare`, player sessions; Q4-B only with the client |
| 4 | HL-3 may raise F1 deaths for armour or shield heirloom heroes (Shade, Arcanist) | `HeirloomSlotParity`; fallback: no armour or shield templates at M3 |
| 5 | Pending dependencies (SK-4 confirmations, SK-16) gate the M3 ship | Listed in 14.2 and 17 |
| 6 | Short runway: the M3 shelf fills in 25–30 runs | Breadth at M4 and Later |
| 7 | Heirlooms may dominate variants anyway | The lever order, ending at an SK-4 change |
| 8 | The Oath slot depends on the undecided MAIN/RS difficulty decision | Request with a fallback (agreed, Q6) |
| 9 | The Burdened twin needs five "never cursed" texts reread | HL-23 lists them |
| 10 | Item curses stay low-stakes; a take rate below 20% means dead content | `CursedTakeRate`; the weight sits on Maladies |
| 11 | Cast-off deeds may skew by class | The always-reachable alternative; per-class reports |
| 12 | The Bequest via WO6 needs an addition to SV's write order | HL-6 request, fallback |
| 13 | Localisation of the archaic registers (Spanish "cólera" is cholera; Melancholia and Spleen are everyday words in several locales; the folk register has no direct equivalent) | Translator notes pre-filled with known traps; NR10 period substitutes with `localeTermOverride` and per-locale blocklist, collision grep and native-speaker read; whole-string keys; CardFit at 150% |
| 14 | Placeholder glyph shapes unchecked against RC5 until the registry pass | `GlyphsDistinctFromRegistry` against the whole RC5 registry (the draft's hollow diamond and cracked circle already replaced) |
| 15 | Moving curses to M4 (Q5) loads M4 with a condition family, nine codex plates, new elite abilities and a sensitivity read | The rows can fall back to Later unchanged (10); the read is scheduled before content lock |
| 16 | Name flags (F18) may force renames late | Flagged now to the naming pass (A.2) |
| 17 | R2 needs a new Core fact (`BossFelled`, HL-31) and a worn-piece credit rule no approved text defines | A request with a fallback (14.2): without it, no armour or shield R2 templates at M3 (the 4.1.4 replacement families) |
| 18 | A Malady taken late on F3 or F4 can ride into the boss fight (T8) | No source in the boss antechamber zone (`MaladyNotBeforeBoss`); `MaladyAtBossShare`; the 14.2 lever |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: hooks in view (6), tier-gating split, `HeirloomSlotParity` ±3, the band values, elite cap per floor, save DCs, Saltless's 2, the frequency and death-shift toys, the earn-rate toy, report thresholds, art and writing counts.
- Godot scene structure for the shelf, the gauge lamp, the chip, the head-tell and the Clinging VFX, with no rules state.
- Line text beyond the samples, inside NR1–NR10 and the tone review.
- Names, until the naming pass.
- **Not tuning:** ITEMS 14 identity; earning only by achievement, never deaths, losses, streaks or counts **(agreed, Q2)**; the Bequest never on the death path; three registers by scope **(agreed, Q1)**; the chip class-wide and presentation-only **(agreed, Q3)**; Malady sources only priced or telegraphed, never bosses, traps, hazards, random imposition, F1 or F5 **(agreed, Q4)**; item curses and Maladies at M4 **(agreed, Q5)**; Oaths kind-only with content rewards **(agreed, Q6)**; 1 Malady per unit; `untilCamp`; every Malady has a Cast-off deed with the alternative; no inherited afflictions; no heirloom loss on death; no priced camp cure; seeded runs write nothing.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **ITEMS owner:** HL-1, HL-2, HL-8, HL-15, HL-16, HL-21 (ITEMS 11 items 4–5, Later), HL-23, HL-24, HL-29 (with ITEMS 24 read).
- **SK owner:** HL-3 (confirm), HL-10 (reading), HL-31 (confirm), the `noFallenHero` and Grip-marks readings.
- **LE owner:** HL-3, HL-23, HL-29 (LE 8.5, LE 11).
- **IR owner:** HL-3 (confirm), HL-5, HL-15, HL-21 (Later), HL-29 (IR 4.15, IR 10), HL-31 (V14 (j) reading; worn-piece bynames).
- **DH owner:** HL-3 (confirm), HL-7, HL-12, HL-20, HL-21 (DH 21.5, Later).
- **CON owner:** HL-16, HL-17, HL-18, HL-19 (theme six), HL-29 (budget reading); RT-15, RT-16, EV-34.
- **EV owner:** HL-4, HL-19 (`payer`, feasibility, `registerLock`), HL-29 (EV-43, EV 10, EV 13).
- **ENM and EP owners:** HL-19 (the forecast extension, the `maladyAbility` slot, the per-floor and antechamber filter); the Iron Sleep record.
- **FL owner (Core events) and CMP owner:** HL-31 (`BossFelled`; worn-piece credit and bynames).
- **CH owner:** HL-1 (CH 4.11), HL-6, HL-9, HL-27, HL-28.
- **SV owner:** HL-5, HL-6, HL-11, HL-14, HL-22, HL-25, HL-28 (SV 6.11).
- **RS owner:** HL-13, HL-14, HL-19 (Stratum Card), HL-21 (RS 6.11, Later), HL-30; the "Old Maladies" codex page; the tone review.
- **Main design and RS owners:** HL-21.
- **TK owner:** HL-26.
- **ARC and HR owners:** confirm Kindly Weight and Carrying Voice.
- **The simulator:** V22 (t) rows, the 9.3 reports, the calibration arms.
- **Human sessions (SV 6.11):** onboarding lines, the chip, Malady readability, `ClingRegret`.
- **STAT owner:** HL-21 (STAT 7.2 under the Shut Book, Later).
- **Art, audio and writing:** 5, 6, 8.7; the sensitivity read.
- **Naming pass:** every placeholder, with the A.2 flags.

---

## Appendix A: critique resolutions and names

### A.1 Critique resolutions

| Finding | Resolution |
|---|---|
| Heart-won count (RS 6.5: 3 at M3) | 3 at M3; the Lullaby's after its unlock; toy re-run (8.2) |
| The Bequest has no write point | `bequestOffer` at WO6, chosen at the shelf (HL-6) |
| "Geas" is an SRD spell (RS 7.6) | renamed Oath; `runModifierId`; SRD names blocklisted (HL-30) |
| Theme condition budget | `MaladyInThemeSix`; the theme declares Cursed |
| F2 Curiosity Maladies vs the gated delta | inside `EventDeathShareDelta`; lever: start on F3 |
| "Inert on F1" contradicts the catalogue | replaced by `F1DeathShareByOath`; F1 limits on the Burdened twin |
| Iron Sleep cites no field | held; ENM request |
| Long Watch is a multiplier | replaced by the Oath of the Short Fire (a removed option) |
| Single Door interactions | `conflictsWith` Perilous; greyed out when Two Doors is off |
| HL-18, Herald's boss afflictions | dropped; told plainly |
| Quartan Ague and the Megrims have living referents | retired (NR1b, NR4b) |
| Acedia is real doctrine | second-look list; humoral gloss; fallback Adustion |
| Eyebitten and Leaden-Recovery rewrite approved rules | Eyebitten re-applies Conspicuous; Acedia uses approved Leaden on a `rest` pool (corrected while writing: Recovery is not a pool, F11); **superseded after the check:** no Malady uses Leaden (T5) |
| Badge and heirloom glyphs share a family; knot collides with charms | worn grip band; cross-barred ring; no knots on glyphs; after the check, `GlyphsDistinctFromRegistry` against the whole RC5 registry, the Malady glyph a humour-wheel quarter and the chip a folded page corner |
| R5 overlaps R2 | a distinct template; one unlock per family per run; V22 (i) scoped |
| Melancholia's payload stacks with Braced | Leaden (`camp` pool) only, floored at 1; a stacking row in V22 (d); **superseded after the check:** a weight payload (Prone when forced movement moves you, once per round), and Set Heels no longer touches forced movement against you (T5) |
| Uneasy Bed / Cold Pillow collisions | Oath of the Borrowed Physic |
| Unlocks in `unlockHash` | kept out (HL-11) |
| HL-9 Oddment pool is not a gap | downgraded to a reading |
| Burdened Clinging duration | the cost binds while worn; Clinging until camp 1 |
| The cap of 2 lets Maladies block conditions | their own slot (HL-17); fallback stated |
| Elite agency underspecified | `MaladyEliteAgency` |
| F5 breaks "until camp" | no F5 sources; truthful F5 text; after the check, the Borrowed Physic acts on F2–F4 only (its F5 pool was empty) |
| The recap does not name curses | HL-20 |
| Register numbering mixed | one register, HL-1 to HL-30 (HL-31 added after the check) |
| Hidden unlock goals | locked templates shown as paper-cut outlines with goals worded to the credit rule; a Whence hint |
| The chip as noise | `RareEquipRateDelta`, with a lever |
| Phlegm and Spleen read as comic | Pituita; Spleen kept pending review; NR4b; Tarantism and Acedia revised |
| Graceless makes some gains duds | a preview mark |
| Plain names only partial | all surfaces (HL-27); after the check, a unique per-record label so two names never collapse into one |
| Bequest on the death screen | moved off the death path entirely |
| Warded vs Malady | never blocks; the preview says so |
| Trinket and charm hooks | HL-26 |
| Companions | any party unit; companion kit items qualify for the Bequest |
| Seeds: the Malady pick stream, the elite cap, share codes | keyed consumers; the EP filter; HL-25 |
| Onboarding | HL-28 |
| Localisation | NR10; translator notes; whole-string keys; the chip splits at 150% |
| Audio collisions | thud, cloth tear, cloth fold; **superseded after the check:** clay press, reed swell, ash crumble and a wood hook settle, outside CH AN9's families |
| Mended, Unread, Seal, Long Watch | handed-down finish; "Not yet read"; Shut Book; Short Fire |
| V22 format | s, g and t phases with waves (9.2) |
| The lever "swap the template" | supersession (HL-11) |
| Reports miss the arms | split by healer arm and party size; `CostLineBiteRate`; `MaladyNetHarder` |
| Accessibility | `MaladySurfacesNotHoverOnly`; reduced motion; no flash |
| Corpus check while writing (this document) | F10–F21 (2.3); new lead's calls 1–16 (14.1) |
| The design check of this document (builder, consistency, fairness, narrative-art) | lead's calls after the check 1–22 and told-plainly items T1–T14 (14.3); HL-31 (13) |

### A.2 Names checked (case-insensitive, whole word and substring, the 32 approved documents and the reviews; re-run while writing)

- **Prefixes:** `HL-[0-9]` 0 hits; `\bV22` 0 hits.
- **0 hits, used:** Malady, Maladies, Grip-marks, Heart-won, Melancholia, Acedia, Atrabile, Adustion, Choler, Pituita, Spleen, Surfeit, Glass Conceit, Tarantism, Phrenitis, Burdened, Redeemed, Ill-Wished, Ill-Slept, Eyebitten, Short-Armed, Saltless, Savourless, Black Lot, Graceless, Crooked Road, Shut Book, Short Fire, Single Door, Bare Board, Borrowed Physic, Iron Sleep, Handed-down, Old Maladies, Cast off / Cast-off, Plain names, Saturn, Tidewrack Spear, Quiet Knife, Old Ash Staff, Thumb-Worn Totem (substring flag below), Whetted Arming Sword, Threshing Flail, Long-Kept Crossbow, Door-Plank Shield, Patched Gambeson, Stair-Worn Byrnie, Kindly Weight, Winnow, cross-barred, humour-wheel quarter, folded corner, grip band, Sullens, Frets, `HeirloomsCurses`, `runModifierId`, `bequestOffer`, `supersededBy`, `burdenedTwinOf`, `redeemedFrom`, `heirloomsSchemaVersion`, `displayKind`, `HeirloomWinDelta`, `HeirloomSlotParity`, `CuriosityMaladyOfferLegal`, `SeededWritesNothing`, `run/oath`. **Added while writing (new lead's call), 0 hits:** Low Tide, Held Ground, Set Heels, Long Look, Carrying Voice, Quilted, `GripMarksBynameOnly`, Hang it (`MaladyLeadenFloor` retired after the check). **Added after the check (lead's call, after the check), 0 hits (re-run):** Grip-mark, Eyebitten, Short-Armed, Surfeit, Adustion, Jactitation, Borrowed Physic, Shut Book, Iron Sleep, Byrnie, Long-Kept, Thumb-Worn, `BossFelled`, Easy Opening (and the other plain labels), humour-wheel, "Carried to the night's end", ash crumble, hook settle; "Oath of" and "Sacred Oath" (0 hits in the corpus).
- **Hits, handled:**
  - bile (153): an approved square kind, so "Black Bile" is rejected.
  - Echo (129, incl. IR's Element Echo): "Heart-won" instead.
  - Thorned (2): an ENM affix, so that badge is rejected.
  - knot: a charm kind word, so no knot glyphs.
  - Hag (2): the Hag's pot, so Ill-Slept instead of Hag-ridden.
  - mended: CH's blanket, so "handed-down".
  - Unread: retired by EV, so "Not yet read" (1 EV prose hit, "a Retelling not yet read in full"; flagged).
  - Stillwatch (20): Long Watch dropped. pillow: the Heart's dust pillow, so Cold Pillow dropped. Uneasy: a rejected EP model's word, dropped.
  - Complaint: the codex title is "Old Maladies" instead.
  - Restless (2) and Conspicuous (7): approved Later conditions, so Eyebitten and Ill-Slept wait for them (F15).
  - ferrule: an IR trim detail, so the notches go on the grip. Hooked and Nail: Boss Nail is a kit name, so neither is used for the badge.
  - vow: an RS Obsession kind, so "Oath". Sworn (54): avoided as a display word.
  - **Undertow (12):** an Arcanist spell → Low Tide (F10).
  - **Unbowed (17):** an approved armour property (AR; IR Bellwarden's Mail) → Held Ground (F10).
  - **Braced (1) and Brace (47):** an EP pose word; VG's Later ability Brace → Set Heels (F10).
  - **Steady (78):** ARC's Steady Casting → Long Look (F10).
  - **Padding (10):** IR and SV pipeline prose → Quilted (F10).
  - "Called" + given name: SK's "Called {givenName}" key → Carrying Voice (F10).
- **Kept, flagged to the naming pass (new lead's call; F18):**
  - **Oath:** 0 whole-word hits; substring of **Oathbroken** (an RS enemy), **Oathkeeper** (a W protected name) and **Oathbinder** (an SK rejected example). The register check is whole word, so Oathbroken passes NR5, but the naming pass looks at it.
  - **Bequest:** 0 hits; LP's Endless Wake prose reads "grave goods and bequests". Internal only after the check (NR2); the player sees "Hang it?".
  - **First Cut (5):** plain prose ("the first cut" of a cut order); a line name only here.
  - **Answer (82):** plain prose; a line name only here.
  - **Thumb-Worn Totem:** 0 hits for the name; "Totem" (79) is the Herald's approved off-hand focus kind (HR 7.7), used as defined. It replaces Darned Hymnal, whose "hymnal" sat beside the Choir theme's hymnal crates (ROOM) and the banned Oddment texture "hymn sheets" (SK 4.5.2), and whose "darned" read as a mild oath.
  - **Set aside (3):** RS prose only ("its residents set aside"); the shelf button label is flagged.
  - **Oaths against SRD and 5E:** the eight placeholder Oaths were checked against the SRD 5.1 and 5E Sacred Oath names (Devotion, the Ancients, Vengeance, Conquest, Redemption, Glory, the Watchers, the Crown) and the Oathbreaker: no match (NR2, HL-30).
  - **Kneel:** an EV option verb (K9, "Kneel by the coat"); used here as the desecrated-shrine option label.
  - **Gatesplitter (3):** SK's sample byname, reused as a sample.
  - **Stair-Worn Byrnie:** checked for a "Stairtop" pun.
- **Existing terms used as defined:** Heirloom shelf, Depth gauge, Book of Nights, Framer, Memento, Stairtop (CH); Old Friend, Wayfarer's Pack (CMP); Lone Road (32); `curseGate` (22); Clinging (30); Ill-Omened, Leaden, Ill-Starred (CON); Grace, Strand, Proviso, StateBlind, `curiosityValueTable`, `EventDeathShareDelta`, the Unwoken Roll (EV); Stratum Card (RS); the Night Governess, the Rousing Engine, the Lullaby, the Jailer of Hours (RS, BR); Whence, byname, Deed (IR, CMP); `pendingProfile`, WO6, `configHash`, `unlockHash`, DS7 (SV); `kitDelta` (SK).
- **Hits found by the design check, handled (lead's call, after the check):**
  - **Passing Bell (12):** a Herald spell head word, and "passing" reads as "deaths" → Grip-marks.
  - **diamond (9):** LE 6.15 tier marks, CON 15 token clusters, BR's Lead diamond → the chip is a folded page corner.
  - **cracked:** DH's cracked-heart lethal tag → the Malady glyph is a humour-wheel quarter.
  - **Hauberk (3):** AR/IR's Hushmail Hauberk is chain mail (heavy) → Stair-Worn Byrnie.
  - **hatching:** Rough terrain's pattern (TL) → the afflicted tell is an engraved double arc.
  - **"Carried out of the Heart":** the Memento's own caption (LE Q1, CH 4.10, SK 4.5.2) → "Carried to the night's end".
  - **Stalwart:** the Splint's approved verb, repeated by the draft's Set Heels → Set Heels rewritten.
- **Rejected on tone or rules:** Calenture, Wanhope, Nostalgia, Mal du pays, Somnambulism, Hysteria, the Vapours, Green-sickness, Lunacy, Moonstruck, Mania, Madness, Dementia, Hypochondria, Catalepsy, Monomania, Lypemania, Lycanthropy, Choreomania, St Vitus' Dance, Distemper, Lethargus, Mare-ridden, the Megrims, Quartan Ague, Crow-Marked (Marked has 484 hits), Iron-Debt, Hex, Blight, Bane, Withering, Thrall, Humours, Entail as an heirloom cost, Geas (an SRD spell), Black Bile, Echo. **After the check:** Passings (reads as "deaths"), Plethora (a current clinical sign; everyday English), Torpor (current physiology), Overlooked (reads as "ignored"), Widdershins (promised a reversal its rule lacked), Darned Hymnal, Grandsire's Crossbow (a waking-world lineage; gendered), Stair-Worn Hauberk, Oath of the Lent Physic (a liturgical season), Oath of the Covered Hand (a shared head noun), Oath of Iron Sleep (the agreed form), "noonday" in Acedia's flavour (the scriptural noonday demon), the hollow diamond, the cracked circle and the shadow halo.
- **Spares for the naming pass:** the Sullens and the Frets; both risk NR1b as mood words, so they need a second look.

### A.3 HL cross-reference

| Id | Sections here |
|---|---|
| HL-1 | 0.2, 0.3, 4.1.2, 13 |
| HL-2 | 4.1.2, 5.1 |
| HL-3 | 0.1, 4.1.4, 8.1 |
| HL-4 | 0.1, 4.1.6 |
| HL-5 | 4.1.5, 9.1 |
| HL-6 | 4.1.2, 14.2 |
| HL-7 | 2.3 F16, 4.1.2 |
| HL-8 | 4.1.1 |
| HL-9 | 4.1.2, 4.1.3 |
| HL-10 | 4.1.2 |
| HL-11 | 4.1.7 |
| HL-12 | 9.3 |
| HL-13 | 0.2, 5.3 |
| HL-14 | header (Validator), 9.2, 10 |
| HL-15 | 0.2, 4.2 |
| HL-16 | 4.2, 5.2 |
| HL-17 | 4.3.1 |
| HL-18 | 4.3.2 |
| HL-19 | 0.2, 4.3.3, 14.2 |
| HL-20 | 4.3.5 |
| HL-21 | 0.2, 4.5 |
| HL-22 | 4.5, 9.1, 10 |
| HL-23 | 4.7 |
| HL-24 | 4.1.2 R5, 4.7 |
| HL-25 | 4.1.7 |
| HL-26 | 4.6 |
| HL-27 | 0.1, 7, 10 |
| HL-28 | 4.8 |
| HL-29 | 0.2, 2.3 F13–F15, 4.2, 4.3, 10, 14.3 |
| HL-30 | 5.3, 13 |
| HL-31 | 0.3, 2.2, 4.1.2, 9.1, 13, 14.2, 14.3 |

The drafts' local numbers map to this register as follows: Model A's HL-4 (full reveal) → Q3 option B (rejected); the synthesis's HL-4 (Grip-marks) → HL-5, its HL-4b (chip) → HL-15; the Curses component's HL-30 → HL-13, HL-31 → HL-15, HL-32 → HL-16, HL-33 → HL-17, HL-34 → HL-18, HL-35 and HL-36 → HL-19, HL-37 → HL-21, HL-38 → HL-22, HL-39 → HL-23, HL-40 → HL-24, HL-41 → HL-27, HL-42 → HL-14; the Heirlooms component's HL-9 (Oddment pool) → a reading; the drafts' HL-18 (Herald) → dropped. Those draft-local numbers are retired and never cited in this document's text; HL-1 to HL-31 each have one meaning. HL-31 (the `BossFelled` Core fact) was added after the design check and maps to no draft number.

## Appendix B: sources

- Game details in section 1 are from memory and general design knowledge; none is cited as a rule, and each must be checked before it is quoted to anyone outside the team.
- The segment 15 proposal (the synthesis), its three models (A, B, C), the Heirlooms and Curses components, the judges' and critics' reports, and the earn-rate toy script, illustrative only, kept in the design lead's scratch area, not in the repository.
- The 32 approved documents listed in the header and the cross-document comparison of 2026-11-23.
