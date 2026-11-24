# Camp and Hub: Design (Segment 13)

Date: 2026-11-23
Status: Approved, 2026-11-23
Names updated by the naming pass (naming pass design `2026-11-24-naming-pass-design.md`, approved 2026-11-24; NP rows listed): NP-26 (the Hollowfolk → the Fireside, with the V20 validator id `Hollowfolk` and every code identifier unchanged; Kindler → Log-Carrier; Chalkwright → Slate-Sketcher; Lingerer → Straggler; Indexer → Shelver; Barker → Showcaller; Sitter → Roll-Reader; Framer → Gilder; Keeper's Cart → the Pedlar's Cart; the Stair (hub station) → the Banister; Coat rack → the Newel; trophy pole → ribbon post; Quiet camp (setting) → No idle chatter; "Plain text for speakers" → "Legible captions"; Seeded night → "Play a run code"; "Same as last night" → "Same as last run"; the "Residents of the Fireside" page; the seeded and version-crossed marks → "run code" and "older build"; Nightmare tiers → Peril tiers; Middle Dream → Midwater; Lucid Key → the Clear Key; The Silk Archive → The Cobweb Archive, short form the Archive; Bedside Stand → the Washstand; "the Hollow ahead" → "the next camp"; Signature item → "the boss's prize"), with the register's cites from other rows: Wish → The Long Shot (NP-22), "Hang it?" → "Shelve it?" and Set aside → Not this time (NP-28), Wayfarer's Pack → Wayfarer's Belt (NP-30), kit strip → gear strip and the Shade and Herald kit picks Twin Blades, Bow and Blade, Shield hand and Focus hand (NP-27). Kept: Stairtop, the Hollow, Last Hollow, Book of Nights, the quiet camp (camp 2), the Lullaby boss and Lullaby Glyph (W-DREAM-1), The Waking City (held, W-DREAM-HOLD-1), and "controller" (the input device, not the enemy role). Historical records (14.3 item 40 and T5, Appendix A) keep the names as written, with notes.
Parents (immediate): `2026-11-23-exploration-information-design.md` (exploration and information design, "XI", segment 10, approved 2026-11-23, **the most recent template**), `2026-11-23-seeds-validation-design.md` (seeds and validation design, "SV", segments 11–12, approved 2026-11-23), `2026-11-22-boss-rooms-design.md` ("BR", segment 9), `2026-11-22-events-design.md` ("EV", segment 8), the loot designs `2026-11-22-loot-economy-design.md` ("LE", segment 7a), `2026-11-22-item-rolls-design.md` ("IR", segment 7b) and `2026-11-22-loot-sources-design.md` ("LP", segment 7c), `2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change ids are RT-n), and `2026-11-23-cross-document-comparison.md` (reviews; its Q-1 amends ITEMS 10.1).
Other parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "C" in section numbers, "CON" in prose),
`2026-11-16-enemies-design.md` (enemies design, "ENM"),
`2026-11-16-items-inventory-design.md` (items design, "ITEMS"),
`2026-11-17-weapons-design.md` ("W"),
`2026-11-17-armour-design.md` ("AR"),
`2026-11-17-trinkets-design.md` ("TK"),
`2026-11-17-consumables-design.md` ("CN"),
`2026-11-17-death-healing-design.md` (death and healing design, "DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "CL"),
the stage-2 class designs `2026-11-18-vanguard-class-design.md` ("VG"),
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC")
and `2026-11-20-herald-class-design.md` ("HR"),
`2026-11-20-companions-design.md` (companions design, "CMP"),
`2026-11-21-floor-layout-design.md` ("FL", segment 2), `2026-11-21-room-terrain-design.md` ("ROOM", segment 3), `2026-11-21-hazards-design.md` ("HZ", segment 4), `2026-11-21-traps-locks-design.md` ("TL", segment 5) and `2026-11-21-encounter-placement-design.md` ("EP", segment 6).
All 30 approved specifications and the cross-document comparison were read for every camp, hub, rest, vendor, keeper, Muster, Notables, recruit, training, reroll, codex, profile, unlock, launcher, Afterword, Unwoken Roll, run history, run record, Stratum Card, next-floor card and Travel-on passage. **This document edits no parent document** (13).

Items marked **(agreed)** are the client's answers of 2026-11-23 to the five questions this design raised (Q1–Q5; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document; parent documents' questions are always named with their document ("EV Q4", "SV Q1", "XI Q1") so that they never read as this document's Q1–Q5. The client also said that everything else in the lead's proposal stands as written: the camp order unchanged and presented as Arrival, the Hollow and Travel on; the stations; saves at camp; the time and click budget and its reports; camp variation and seam camps; the run told without words; the hub form and surfaces; the launcher; after the run and the death-path budget; unlocks and milestones; onboarding; controller and accessibility; the ambient charter AN1–AN13; the lore engine; determinism (lore outside the reproducibility key, the display domain `lore`, the profile field `loreMemory`, seeded runs writing no lore); the full Fireside catalogue with its sample lines; the creative moments adopted, held and rejected; art and audio; the new mechanics (the camp reward card, vendor potion stock, `BuyBatch` Quick Restock, Accept kit defaults, the speculation restart, the Perilous Door at the door station, the floor-k Straggler in the hub); the numbers; data and validator V20 `Hollowfolk`; staging; ownership and hooks; the binding requirements; CH-1 to CH-24; the risks; and the appendix resolutions and names. Names are placeholders pending the naming pass. (The register has since grown to CH-26 after the design check: CH-25 registers V20 in the consolidated suite and CH-26 narrows CMP's Travel-on confirmation; both flagged, 13.)

**The lead's answers (0.1) and Composite "H" (3.2)** were accepted by the client by proceeding to the five questions. They are recorded here as the lead's answers; the client confirmed them on 2026-11-23, when it approved this document as a whole, **including every told-plainly item of 14.1**: no empty bedrolls for the dead inside a run; no tiny flavour effects; camp time budgeted but unmeasured; the streak guarantee to about Night 25 in M3; the Afterword stays on the run-start card; the `ink` chain reward dropped and Ask topics rejected; Listen renamed Hear out; Go down again renamed Begin again; no hub peg for starting kits, no hub surface for charms, no trunk for curses; seeded runs repeat ambient lines; the walkable camp and hub rejected; the name collisions fixed.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-23, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-23. Items marked **(lead's call, after the check)** were made in answer to the design check of this document (builder, consistency, player and art-narrative lenses); none overrides a client answer; they are listed in 14.3 with the "Told plainly after the check" items (T1–T13), and the client confirmed them all on 2026-11-23. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change CH-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-23 (with Q1–Q5 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge, a critic or a draft, or state an unwelcome forecast, and were told to the client in plain words (14.1; those found in the check, 14.3, T1–T13); the client confirmed them all on 2026-11-23. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q5 of this document (2026-11-23), or one recorded in an approved document (named with its document), restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal (the lead's answers of 0.1 and Composite H included); confirmed by the client with this document on 2026-11-23 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-23 |
| **(lead's call, after the check)** | A lead's call made in answer to the design check; never overrides a client answer; listed in 14.3, with the "Told plainly after the check" items, and confirmed by the client on 2026-11-23 |
| **(change CH-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text are flagged ⚑ there; the client's approval of 2026-11-23 covers them |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a judge, a critic or a draft, or states an unwelcome forecast; told to the client in plain words (14.1; those found in the check, 14.3) and confirmed by the client on 2026-11-23 |
| **(request)** | Asks another owner to rule; changes nothing until that owner rules; the fallback is stated (14.2) |
| **(hook)** | Owned by another design; this design states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator or by human sessions (8, 9). Never binding |

**The client's brief.** Segment 13: **the camp** (between floors) and **the hub** (between runs). Specifically: "we should have NPCs there that can't be recruited as companions or purchase items from; these serve as set dressing and to give bits of lore about the world." Everything else is left to the lead. Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; **combat is key** (camp must not bloat the run); contest the client or approved views where warranted.

**Change prefix.** This design uses **CH-n**. A case-insensitive grep for `\bCH-[0-9]` across the 30 approved documents and the reviews finds **0 hits** (re-run while writing). The research drafts, the three models and the passes used colliding local CH numbers; this document issues **one register, CH-1 to CH-26** (13; CH-25 and CH-26 added after the check), and no id has two meanings (A.3).

**Validator.** **V20 `Hollowfolk`** is free. V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV, V17 BR, V18 XI and V19 SV. A grep for `\bV20` finds **0 hits** (re-run while writing). V20 joins the consolidated suite only through **CH-25** (RS 11.2 suite table; SV 5.2 becomes V1–V20), as every earlier validator did (lead's call, after the check). The drafts' names `Hollows` and `Hearth` are not used (`Hearth` is a Herald subclass).

---

## 0. At a glance

- **Camp** is a fixed isometric **diorama** around one fire: no walking, no grid, no ticks. Stations are hotspots mirrored in a **Camp List** for keyboard and controller. A 0.4 s camera dolly moves to the focused station *(illustr.)* **(agreed, Q3)**.
- **The camp order is kept exactly** (CMP 7.11 = DH 13.1 = RS 6.8). It is *presented* in three phases: **Arrival** (get-up, the catch-up picks, then one rest summary), **the Hollow** (Muster, vendor, Notables, party view, in free order) and **Travel on** (one click on the lit door, whose caption always carries the Party Forecast; a confirm only when something the party owns or is owed would be lost) (CH-1, CH-2; readings; CH-26 ⚑ after the check).
- **The hub** is the decided "simple camp screen" in M2 (MAIN 4.7) and a richer **menu-room**, never walkable, in M3. Its fiction is **the Stairtop**, "the one hollow that does not re-form": premise-neutral **(agreed, Q1)**.
- **The ambient cast, the Fireside**, answers the client's brief: 15 named figures plus "settled" Stragglers. They **structurally cannot** sell, hire, give, gate or affect rules. Their only verb is **Hear out**. They sit at the Edge of the Light or beside a record-only hub object, never at a service station. They react to how the last run ended and to what this run has done, never count deaths, and fall silent rather than repeat (lead's call; CH-7 to CH-14).
- **No mechanical or informational effect at all** from any ambient figure; cosmetics and restatements of public rules already on screen only **(agreed, Q2)**.
- **Lore sits outside the reproducibility key.** A profile field `loreMemory` and a display domain `lore`. Core cannot reference either. Selection reads a stored run-start snapshot plus a run-scoped lore state, so nothing repeats inside a run and a reload shows the same lines; seeded runs read lore memory and write nothing to it (CH-7, CH-8, CH-9).
- **Owed content filled:** the camp reward card (take 1 of the camp channel's 2 entries free, or 1 of 3 on a Reprieve Phial camp; or buy either or both; a notables-slot Wax Key is shop-only); vendor potion stock keyed to the party at floor generation; `BuyBatch` Quick Restock; Accept kit defaults; the speculation restart; the Perilous Door at the door station; the M3 Tithe Bowl placed (CH-15 to CH-20).
- **Clock:** camp time is budgeted by RS 0.1 but measured by nothing. New reports `CampSeconds`, `CampClicks`, `RunWallClock`, `HubSeconds`, `DeathPathSeconds`, with a cut order (CH-5).
- **Told plainly:** the client's "empty bedrolls for the dead" cannot happen inside a run, because companions are never killed (DH 6) and the hero's death ends the run. Camp shows only true signals; mourning lives in the hub.

### 0.1 The lead's answers in plain words (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

1. **Can the camp and hub have NPCs who can't be recruited or sell, and only give set dressing and lore? Yes, and it is guaranteed in data, not by convention.** The ambient figures use their own record type with no price, stock, item, kit, unit, Muster, reward, command or effect field; the validator rejects any record that adds one, and the rules core cannot even see the type. Service and lore never share a body: the keeper sells, the Muster board hires, and every Fireside figure sits with empty hands and its work set down. Their only action is "Hear out". There are three layers:
   - **Camp regulars** who belong to the hollows: the **Log-Carrier** (keeps the fire, every camp), the **Slate-Sketcher** (chalks pictures of places on a slate at the edge of the light, camps 1–3) and the **Stillwatch** (faces the Heart's door, camp 4 only).
   - **Stragglers**: at each camp, one minded resident of the floor you just cleared sits at the edge of the light, out of uniform, its Obsession slipping "where the dream thins" (RS 7.5). The route varies, so the cast varies for free.
   - **The Stairtop cast** in the hub: the **Copyist** (writes each night into the Book of Nights), the **Roll-Reader** (by the Unwoken Roll, sincere, after deaths only), the **Understudy** (rehearses scenes from every stratum, does boss impressions) and the **Gilder** (frames Mementos, paints a door for each unlocked theme). Stragglers you have heard often "settle" in the hub.
2. **Can they give tiny flavour-only effects? No, not even one coin or one hint** (told plainly; agreed, Q2). Any value turns listening into a chore at every camp, breaks the display/rules split that keeps seeds honest, and runs into "no permanent stat boosts" (MAIN 4.7) and "no mercy" (EV Q4). They may give cosmetics (codex pages, hub props) and may restate a public rule already on screen.
3. **Will a losing streak make them repetitive? It is the design's first concern.** A hero who dies on floor 1 never sees a camp, so the hub carries the streak. Lines advance one beat per night *whether you win or lose*; at most one figure reacts to a death (none in M2, the Roll-Reader in M3); no figure counts deaths or pities you; and a figure with nothing new to say stays silent. The floor-1 Straggler you died among sits in the hub that night, with a line that is never comic (CH-13). The toy shows at least one unseen hub line every night through about Night 25 in M3; after that the long tail depends on later content, and we say so (told plainly).
4. **Walkable camp?** No (agreed, Q3). Walking adds little forced time, but walkable bodies invite a "visit everyone" chore four times a run for hundreds of runs. The diorama keeps the breath and the art showcase.
5. **Waking world or dream for the hub?** The Stairtop, which commits no premise item (agreed, Q1). A waking-world house would commit hero motives and an ending (a win shown as waking).
6. **Camp before the boss** (camp 4, the Last Hollow) is tension by subtraction: a low fire, the Log-Carrier silent, almost no lines, one plain spend-down line, the Stillwatch watching the far door.
7. **Permadeath in the hub** is carried by one quiet object (the hero's empty coat peg, M3) and the approved Unwoken Roll, with a strict budget on how many death beats a streaking player sits through.

### 0.2 The client's five decisions (agreed, 2026-11-23)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Hub fiction | **A.** The **Stairtop**, a dream-side hollow that does not re-form; commits no premise item (4.8) | B: a waking-world house where sleepers lie down (commits hero motives and shows an ending when a win reads as waking; RS 19.2, RS 7.5 "few wake"). C: menu only | CH-6 ⚑ (MAIN 4.7; RS 7.5 flagged after the check) |
| Q2 | Ambient effects | **A.** No mechanical or informational effect at all; cosmetics (codex pages, hub props) and restatements of public rules already on screen only (4.17; schema-enforced by AN1–AN3 and V20) | B: cosmetics plus one tiny effect (chores, determinism, MAIN 4.7, EV Q4) | — (enforced through CH-7, CH-21) |
| Q3 | Camp form | **A.** The diorama with a 0.4 s station dolly *(illustr.)*, no walking, the Camp List for keyboard and controller (4.1) | B: a walkable camp (~50 s a camp of voluntary chore; 1.5–2× art). C: a plain menu | CH-22 ⚑ |
| Q4 | Hub figures in M2 | **A.** Yes, minimal: the static Copyist at the Book of Nights and the floor-k Straggler vignette on the M2 simple camp screen (a reading of decided MAIN 4.7) (4.8, 7.7) | B: no hub figure until M3 | CH-13 ⚑ |
| Q5 | Settled Stragglers | **A.** Yes, at most 3 visible in the hub, rotating (5.1) | B: keep camp and hub casts separate | CH-6 ⚑ (M3 hub contents) |

**Raised, not decided (future items, 14.2):** the visible easier setting EV Q4 points to (a lamp is reserved on the Depth gauge; no figure substitutes for it); voice acting for the Log-Carrier (Later).

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Composite "H": C's diorama frame, A's Arrival card, Camp List, Travel-on confirm and M2 hub screen, A's non-walkable M3 menu-room, B's Stairtop fiction, the cast pass whole, the creative pass's visual storytelling, the numbers fixes (3.2) | M2/M3 | lead's call |
| 2 | Camp form: diorama, stations, dolly, Camp List, Hear-out sub-list (4.1) | M2 | **agreed, Q3**; CH-22 ⚑ |
| 3 | Camp order kept; presented as Arrival (get-up, catch-up, rest summary), the Hollow, Travel on (4.2) | M2 | lead's call; CH-1 ⚑, CH-2 ⚑, CH-26 ⚑ (after the check) |
| 4 | Stations: Fire, Bedrolls, Muster Post, Pedlar's Cart (with Notables), Door(s), Edge of the Light (4.3) | M2/M3 | lead's call |
| 5 | Saves at camp: WO4 carries get-up and offers; WO8 for draw/spend/reveal commands only; talk is UI-only; `loreSeen` queue; speculation restart (4.4) | M2 | lead's call; CH-2 ⚑, CH-3 ⚑, CH-9 ⚑, CH-19 ⚑ |
| 6 | Time and click budget, five new reports, the cut order, Quick camp (4.5) | M2 | lead's call; told plainly; CH-5 ⚑ |
| 7 | Camp variation: act-keyed seam camps, the quiet camp, the Last Hollow; register rule (4.6) | M2 | lead's call; CH-4 ⚑, CH-11 ⚑ |
| 8 | The run told without words: ribbon post, ribbon and chalk, truthful bedrolls, residue, Strand cords (4.7) | M2/M3 | lead's call; told plainly; CH-23 ⚑ |
| 9 | Hub form: M1 screens; M2 simple Stairtop screen with the Copyist and floor-k Straggler vignette; M3 menu-room (4.8) | M1–M3 | **agreed, Q1, Q4**; CH-6 ⚑, CH-13 ⚑ |
| 10 | Launcher silent and outside the seed; no kit peg (4.9) | M2/M3 | settled; told plainly; CH-24 ⚑ |
| 11 | After the run: death, win, unravel, abandon, seeded; death-path budget; "Begin again" fast path (4.10) | M2/M3 | lead's call; CH-14 ⚑ |
| 12 | Unlocks and milestones as hub objects; "New in the dream" (4.11) | M3 | lead's call |
| 13 | Onboarding, controller and accessibility (4.12, 4.13) | M2/M3 | lead's call; CH-22 ⚑ |
| 14 | The ambient charter AN1–AN13 (4.14) | M2 | lead's call; **agreed, Q2** (AN1–AN3); CH-10 ⚑, CH-12 ⚑, CH-21 ⚑ |
| 15 | The lore engine: `LoreContext`, triggers, delivery, tiers, pacing, death ownership, streak guarantee, codex (4.15) | M2 engine, M3 chains | lead's call; told plainly; CH-10 ⚑, CH-14 ⚑ |
| 16 | Determinism: outside `manifestHash`, in `displayHash`; Core blind; seeded runs write no lore (4.16) | M2 | lead's call; CH-7 ⚑, CH-8 ⚑, CH-9 ⚑ |
| 17 | No mechanical effect (4.17) | — | **agreed, Q2** |
| 18 | The Fireside catalogue, 15 figures, settled Stragglers, floor-k Straggler in the hub (5.1) | M2/M3 | lead's call; **agreed, Q4, Q5** |
| 19 | New mechanics: camp reward card, potion stock, `BuyBatch`, Accept kit defaults, speculation restart, Perilous toggle (7) | M2/M3 | lead's call; CH-15 ⚑ to CH-20 |
| 20 | Data, V20 `Hollowfolk`, reports, chain-graph tooling (9) | M2 | lead's call; CH-25 ⚑ (after the check) |
| 21 | The design check's fixes (14.3) | M2/M3 | lead's call, after the check |

### 0.4 Intent, goals and anti-goals (new lead's call: added in the template's shape)

**Intent.** The camp is the run's one breath between fights, and the hub is where the dream remembers you. Both should be short, legible and felt, and should carry the world's lore in bodies and objects that can never be mistaken for, or become, a service.

**Goals.**
1. A returning player clears a camp in about 90 s without a hire *(illustr.)*; nothing at camp is required beyond the catch-up picks and one click to Travel on.
2. Every ambient figure is provably inert: no field, command or rules read connects it to the core.
3. A player on a losing streak, who never sees a camp, still meets the cast and hears something new each Night through about Night 25 in M3.
4. The camp and hub show only true signals about the run.
5. Every station, figure and line is reachable without a mouse and without timed text.
6. The same seed produces the same run for every profile, whatever its lore memory.

**Non-goals.** The camp order and camp rules themselves (CMP, DH, RS); vendor prices and stock rules (ITEMS, LE); the Muster's offers (CMP); the Afterword and Unwoken Roll limits (EV); the save file format (SV); codex hosting (segment 10); the visible easier setting (MAIN/RS); the premise (RS 19.2).

**Anti-goals.**
- A walkable camp or hub; a "visit everyone" chore.
- Any reward, hint, discount or effect for listening.
- A new-dialogue marker, an unread counter or a completion percentage for lore.
- Death counts, streak counts, pity or "try something easier".
- Humour near downings, deaths, memorials or unravels.
- A figure who sells, hires, gives or gates.
- Any figure implying the Dreamer, waking as a promise, or hero motives.

---

## 1. Survey

Mechanism references only; no names or text taken. Hades figures confirmed by web search in the research pass (about 21–22k lines; Hades II about 30k voice lines and 400k+ words); Valve's dynamic dialogue confirmed from Ruskin's GDC 2012 slides. Everything else is from recall and must be checked before it is quoted to anyone (told plainly).

| Source | What works | What fails | Taken |
|---|---|---|---|
| Hades, Hades II | Reactive hub lines keyed to how you died (Hypnos); priority tiers; play-once lines | Gift/nectar chores; long hub walks late; Mirror grind | Death-key reactivity; tiers; one death owner |
| Valve/Ruskin rule databases | Most specific matching rule wins; lines write facts back | Needs tooling | Specificity inside tiers |
| Darkest Dungeon 1–2 | Short narrator lines; DD2's short inn stop | Narrator repetition audible; Hamlet/Altar grind | Text, no VO; short menu-led stops |
| Dead Cells | Fast restart | — | 2-click fast path |
| Enter the Gungeon, Loop Hero, Wildfrost | Residents arrive as the hub grows | They become shops; unlock pacing gates | Growth by arrival, never commerce |
| Slay the Spire | One-screen campfire decision | — | One-screen vendor and rest summary |
| Outer Wilds, Spelunky 2 camp | A fire, a small cast, lore reacting to discoveries, zero mechanics | — | The tone target |
| Dragon Age, BG3, Three Houses, Pillars | Banter | "Check every NPC" chore; power behind walking; rest bonus optimisation | No effect, no walking, no new-dialogue markers |
| Dark Souls, Elden Ring, Hollow Knight | Lore in objects; NPCs react to progress | Missable quests (FOMO) | Object captions; never missable |
| Into the Breach | Remarks on lost timelines | — | Last-run reactivity |
| Children of Morta | Scenes advanced by runs | Unskippable early | Always skippable |
| Pentiment, Kentucky Route Zero | Unvoiced, spare, sincere text | Typography hurts legibility | Text, with a plain-font setting |
| Cult of the Lamb | — | Base upkeep chores | No upkeep |
| Inscryption | Diegetic objects | Authored story vs open premise | Objects only |
| Tabletop (5E rests, Torchbearer, Blades downtime, Ironsworn, Dungeon World, Sly Flourish "secrets and clues") | Camp as a fixed pacing beat; level-up at camp; separate the clue from the mouth | Rumour tables lie | Secrets pool deliverable by any figure; unreliable about history, never rules |

**Combined lessons.** (1) Reactivity to the last run is the anti-streak engine. (2) Every line plays once if possible, with a filler tier on cooldown and silence after it. (3) Lore must never carry power or required information. (4) The hub grows by residents and objects arriving, never by shops. (5) The camp is a short readable breath, not a walk. (6) Nothing is missable under permadeath. (7) Objects carry deep lore cheaply. (8) A streaking player must be able to skip the hub.

---

## 2. Audit

### 2.1 What the approved documents fix (settled unless a flagged CH changes it)

- **(decided)** MAIN 4.1: between floors, a camp with a full rest, a shop or reward, and a Muster; every camp has a basic vendor; each camp shows the next floor's theme. No camp in M1 (MAIN 9) and none after F5 (ITEMS 10.2, LE 6.11). One save slot per run, deleted on death.
- **(decided)** MAIN 4.7: unlocks are content, never power; one starting boon; a difficulty ladder; Peril tiers add kind, not stats; no permanent stat boosts; "**Hub.** A simple camp screen to begin with. A richer hub is out of scope until the run itself is proven." MAIN 2: anti-goals grind and decision fatigue; non-goal a story campaign.
- **(settled; lead's call in CMP)** Camp order, CMP 7.11 = DH 13.1 = RS 6.8: 1 Downed get up; 2 catch-up with level and skill choices, hero first; 3 restore HP and every pool to the post-catch-up maxima; 4 lingering conditions end (Scarred included); 5 +1 supply; 6 hero potion top-up (DH 13.2); 6a next-floor card, door cards, or final-boss card / Last Hollow choice; 7 snapshot, card A, the Eye's card; 8 Muster (revisitable until Travel on); 9 vendor; 10 shop or reward; 11 Travel on (top-up re-check CMP-22; door committed; F(N+1) generated).
- **(agreed)** ITEMS 10.1 as amended by Q-1 (cross-document comparison): one run-scoped purse, spent at camp, a Ticket lock (M3) and a Later trader. ITEMS 10.3: limited stock only for potions, the Wax Key and the Ear Trumpet; unbought stock destroyed on leaving; "no floor, room or stairs at camp". ITEMS 10.4: `choice: unset | shop | reward`, explicit and irreversible; camp channel 2 entries pre-rolled in floor N's loot draw. ITEMS 10.6: Sell at camp only, 250‰; Scrap at the stairs only. ITEMS 11 item 5 (client decision): camp identifies everything free.
- **(agreed)** DH: companions are never killed (DH 6; DH Q1/Q2); G1 camp full rest; G5 the hero's top-up (client numbers). ABL 15.1/15.3: short rests on floors only, supply start 2, +1 per camp, cap 3.
- **(settled)** Camp re-picks: Vanguard stance once per camp (VG-17); one spell swap for full casters (ABL 6.5, ARC 7.3, HR 7.5); Rethink once per companion per camp (CMP 8.2); Recruiter's Eye (CMP 9.2); one dismissal or swap per camp (CMP 7.6); same-visit hire undo; `KitChoicesDefaulted` (CMP 8.2).
- **(agreed)** SV 4.8.3: WO4 floor exit to camp; WO5 Travel on (F(N+1)'s first safe point); WO7 is **Load** (read, validate, flush `pendingProfile`; at camp, rewrite `run.save` by atomic replace and write no slot); SV defines no write on quitting; WO8 every camp command that draws, spends or reveals rewrites `run.save` before its outcome frame. SV 4.4 speculative generation. SV 5.5 unravel at Travel on, credited as a death at camp N. SV 4.9 personal-run codes shown only after the run ends. SV Q1: seeded runs write only `shownCount` and `fullShown` (EV's `curiosityMemory` seen counts) and a marked `runHistory[]` entry (V19t `SeededRunWritesSeenOnly`). SV 4.6: display content outside the key; display interactions UI-layer only; V19s `CoreNoProfileRef`, DS12.
- **(agreed)** RS 7.3 pillars (no humour on downings, deaths, the recap; register by floor: comic F1–2, dry F3–4, absent F5); RC1–RC9; RC7 reserved cues; RC8 a closed bark-trigger list (V7). RS 7.5: "Camps? Lucid hollows where the dream thins. Unlocks? The dream remembers you. Death: you did not wake." "Some go down; few wake." Open (RS 19.2): the Dreamer's identity, whether it can wake, hero motives, endings.
- **(agreed)** EV 4.5.10: the Afterword (M2 lean: one run-start line plus one Strand closure line; never silenced by exhaustion; `AfterwordExhaustion`); M3 keeper lines naming Strands; the Unwoken Roll, "the single fallen-hero device": once per run, after the first death, last 3 names "and others", marks never counts, hideable. EV 0.1 item 7 / EV Q4: nothing reads streaks to change draws; display lines may react to how the last run ended. DV2 (the `selfReference` cap) is **inside the seed** (EV 4.5 table).
- **(settled)** XI 4.6.3: Tithe Bowl (M3), one purchase per camp; at the fourth camp only the Sketch is offered. XI 4.10: codex never a HUD input; `NoHoverOnly`; staged disclosure via `uiMemory`; text scale 150%.
- **(agreed)** R-SEED-2/R-SEED-6: theme-blind hero creation; the launcher's forecast and redraws stay outside the seed and invisible.

### 2.2 Every hook placed

| # | Hook (source, wave) | Placed in this design |
|---|---|---|
| 1 | Camp layout; camp reward card content (ITEMS 10.4/24, M2) | 4.1–4.3; CH-15 |
| 2 | Reprieve Phial via `campReward` (CN 14.7, M2) | Reward card pick 1 of 3; never in the shop (CH-15) |
| 3 | Top-up step; Stratum Card (DH 27, M2) | Top-up: the rest summary (4.2, Arrival A.3). Stratum Card: the floor-entry card, shown on entering F(N+1) after Travel on (6, Transitions), unchanged and owned by DH and RS |
| 4 | Muster screen in the order (CMP 0/17/18, M2) | Muster Post (4.3) |
| 5 | Next-floor, door, final-boss, Last Hollow cards (RS 2/18) | Door station (4.3) |
| 6 | Stance re-pick and warnings (VG 15, M2) | Party view at the Bedrolls |
| 7 | Re-engraving (IR 4.6, M3 hook) | Held Later (5.4) |
| 8 | Vendor potion scaling (LE 6.8, DH HE-6, M2) | CH-16 |
| 9 | The Long Shot, Ferryman's Note, Blood Price (LE 8.1, M3) | Held; promotion order stated (5.4) |
| 10 | Coin ledger, "the next camp sells rare", unused tags (LE 6.15/6.7, M2) | Pedlar's Cart and party view |
| 11 | Camp-4 spend-down line and soft confirm (LE 6.15, M2) | The ledger line as LE's UI text; the Travel-on confirm when the purse is at least the cheapest vendor item; the keeper's spoken line restates the ledger line (`publicRuleRef`) |
| 12 | Tithe Bowl (XI-24, M3) | Pedlar's Cart, camps 1–4, Sketch only at camp 4 |
| 13 | Ear Trumpet row (XI-26, M3 conditional) | Vendor panel |
| 14 | Lore roster preview; Atlas page on dream track (XI 4.13, M3) | Door card detail tier; ribbon above door |
| 15 | Wax Key row and coverage-strip line (TL-25, M2) | Vendor panel |
| 16 | Afterword; keeper Strand lines; Strand display (EV 4.5.10/4.5.7) | Afterword unchanged on run-start card; Strands as cords over the fire (M3) |
| 17 | Unwoken Roll (EV 4.5.10, M3) | Hub object with the Roll-Reader (CH-14) |
| 18 | Pledges, Testimony (CMP 7.12, EV-41, Later) | Muster cards only; never ambient |
| 19 | Hawker's Hour, Lost Property (EV-42, LP, Later) | Not camp (Hawker is a floor event); Lost Property held |
| 20 | Coin owed (EV) | Stays off |
| 21 | Lone Road (CMP 7.5, M3) | Heirloom shelf toggle; camp shows no open-slot bedrolls |
| 22 | Old Friend (CMP 7.2), Wayfarer's Belt (CMP 12, M3 row) boons; Veteran record (CMP 8.7), M3 | Heirloom shelf; Veteran book in the Library |
| 23 | Milestones → unlocks; strata; Peril 1–3; Perilous Door (RS 18/12, M3) | Unlocks and milestones (4.11); Depth gauge; door station (7.6) |
| 24 | Codex: Curiosity, Dream Rules, TL pages, Boss Ledger, Whence (RS 12, M3) | Library station; Residents pages (4.15.8) |
| 25 | Grave Goods, Memento, memento wall (LE 6.11/8.2) | Book of Nights; Memento wall (M3) |
| 26 | Floor Atlas, fatal-room map (XI 7, M3) | Book of Nights; recap |
| 27 | Seeded entry, marks, codes, unravel card (SV 4.9/17, M2) | Hub rows (4.8); the Letter Rack (M3) |
| 28 | Launcher order, LauncherEmu (R-SEED-6, M2) | Launcher (4.9), silent |
| 29 | Sequel beat (EV 4.5.8, M3) | Run-start card, unchanged |
| 30 | Death recap (DH 17.12) | Death path (4.10) |
| 31 | Heirloom templates (ITEMS 14, M3) | Heirloom shelf, exact text shown before pick. **Taken up by the heirlooms and curses design (segment 15; approved 2026-11-23):** earning R1–R3 and the shelf (4.8, 4.11; HL-1, HL-9, HL-10, M3; R4, R5 Later, HL-24); pending offers in Begin again's picker (4.10; HL-6, a request to SV); Grip-marks display only (4.11; HL-5); first-time lines (4.12; HL-28, a request) |
| 32 | Charm abilities (TK 24.2) | No hub surface; may add a boon type later (told plainly). **Taken up by the charm abilities design (segment 16; approved 2026-11-24):** the Bobbin boon type at M3 in the launcher's and Begin again's boon picker, with no hub surface or shelf object (CH-24 stands) (4.9, 4.10; change CA-20, a request to the MAIN/CH owners, pending; agreed, charm abilities Q4); its earn milestone and charm-family unlocks on the Library's loot-unlock object (4.11; change CA-21, a reading, CH confirms) |
| 33 | Curses (ITEMS 13, IR 4.15, Later) | No "curse trunk"; a reserved run-modifier slot on the Depth gauge (told plainly). **Taken up by the heirlooms and curses design (segment 15; approved 2026-11-23):** item curses and Maladies move from Later to **M4** (agreed, heirlooms and curses Q5; HL-29), still with no curse trunk; Plain names and the content note in Settings (4.13; HL-27, M4); first-time lines (4.12; HL-28, a request); Oaths in the reserved slot (4.8, 4.9; HL-21, a request to the MAIN/RS owners; agreed, heirlooms and curses Q6; Later) |
| 34 | Waking City at camp 4 after a first win (RS 8.12, M4); three-door camps (M4); the Clear Key (Later) | Door station accepts them |
| 35 | Telemetry consent (MAIN 6.6, SV 17) | Settings row (owed by MAIN) |
| 36 | HR 5.7 non-fearless band | Door card, unchanged |
| 37 | Visible easier setting (MAIN 4.7, EV Q4) | Reserved unlit lamp on the Depth gauge; MAIN/RS decision (14.2) |
| 38 | "Training" in WO8 | Read as level and skill picks (CH-3) |
| 39 | TL 6.11 camp notables slot; LE 6.3 / ITEMS 10.4 "third camp display entry" (M2) | Notables panel, shop side only (7.1) (lead's call, after the check) |
| 40 | LE 6.4 item 7 and LE 15 coverage targets (coin on arrival covers per-body restock; the wish-list cover bands) | 7.2 and 7.3, checked by a new `CampRestockCoverage` report at both caps (9.2) (lead's call, after the check) |
| 41 | EV "Camp and hub": Tithe Bowl fact dedup through the FactLedger | Pedlar's Cart; FactLedger dedup kept unchanged (lead's call, after the check) |
| 42 | LE 8.1 Tithe Bowl guard "unavailable while the party holds a Wax Key" (XI's relaxation request pending) | Tithe Bowl shown greyed with its reason while a Wax Key is held, until the LE owner rules on XI's request (lead's call, after the check) |
| 43 | Starting kits (CH-24 no kit peg; 4.9 wording; 4.11 unlock objects; 4.12 creation; the lore layer) | **Taken up by the starting kits design (segment 14; approved 2026-11-23):** 4.9 wording (SK-8, M2); kit-variant bundles on the Heirloom shelf and their milestones (4.11; SK-7, M3); creation gear strip, warnings and the Oddment callout (4.12; SK-9, M2); the Oddment in 4.10, 4.15.1, 4.15.8, 4.16 and 9.1 (SK-6, M2 / M3) |

### 2.3 Findings (told plainly)

- **F1, act-palette drift.** RS 6.8's dressing bullet says "camp 2 carries a bigger act-change moment", but RS 6.2's act table puts the act changes at camps 1, 3 and 4 (Shallows F1, Midwater F2–3, Deep F4, Heart F5). CH-4 corrects it.
- **F2, camp time.** RS 0.1 counts "four camps of 3–4 min" inside the 104–115 min run, so about 12–16 min of camp is budgeted. CMP 7.4's floor-time gate excludes camp screens and no instrument measures camp time. (Several drafts wrongly said camps sit outside the run clock.)
- **F3, empty bedrolls for the dead** have no in-run referent (DH 6, MAIN 4.2).
- **F4, the keeper** is the only approved camp figure and is a vendor; the M3 keeper lines must stay service and Strand lines, or the client's vendor/lore line blurs.
- **F5, "Keeper"** also collides with BR's "keepers of the sleep" (naming pass).
- **F6, RC8** is a closed list; ambient lines need a new trigger class (CH-10).
- **F7, register** is set by floor; camps sit between floors (CH-11).
- **F8, the camp music box** sits next to the reserved Lullaby Glyph wind-up click, the Heart bed's box and the M3 Lullaby boss (CH-12).
- **F9, the Afterword must never fall silent** (`AfterwordExhaustion`), so it cannot move off the run-start card without changing an agreed report.
- **F10, the verb "Listen"** is a core floor action (door Listen, Lockside Listen, Ear Trumpet); the ambient verb needs another word (RC9).

---

## 3. Models and judges

### 3.1 The models

Three camp models were built in full; a cast-and-engine pass, a creative pass and a numbers pass followed; three judges scored and three critics (consistency, player, coverage) checked. Scores are per judge, 1–10.

| Model | Fit | Pacing | Atmosphere | Cost | Buildability | Judges' verdict |
|---|---|---|---|---|---|---|
| A, the Camp Card (static scene + station menu; menu hub) | 8 / 8 / 8 | 9 / 9 / 9 | 6 / 6 / 6 | 9 / 9 / 9 | 9 / 9 / 9 | Fastest, literal fit to MAIN 4.7; reads as a menu with wallpaper |
| B, the walkable Hollow and walkable Stairtop | 6 / 6 / 6 | 5 / 6 / 6 | 9 / 9 / 9 | 4 / 4 / 4 | 5 / 5 / 5 | Richest; invites the visit-everyone chore (~50 s a camp); determinism leakage risk; 1.5–2× art |
| C, the diorama with dolly and drift | 7 / 7 / 7 | 8 / 8 / 8 | 8 / 8 / 8 | 6 / 7 / 6 | 7 / 8 / 7 | Best frame; fix names, per-kit drift clips, waking-world hub, walkable M3 hub |

### 3.2 Composite "H" (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-23)

All three judges independently recommended the same composite:
- **Camp frame from C**: the diorama with a 0.4 s station dolly and a *generic* stand-and-turn party reaction (the 60+ per-kit drift clips rejected).
- **Arrival card, Camp List, Travel-on confirm and M2 hub screen from A**; the M3 hub as A's **non-walkable menu-room**.
- **Hub fiction from B and the cast pass**: the Stairtop (agreed, Q1).
- **Cast, names, charter, schema and engine from the cast pass (npc-lore), whole**, with its two determinism fixes (separate display self-reference cap; seeded runs write no lore).
- **Visual storytelling from the creative pass**: ribbon post, truthful bedrolls, coat peg, Last Hollow stillness.
- **Numbers fixes** (speculation restart, `BuyBatch`, act-keyed seam sets, Straggler pacing and pool depth), as corrected by the critics.

Where the judges split, the lead chose: potion stock keyed to the party at floor generation (B's key, preferred by judge 2 and the consistency critic; 7.2); the Afterword stays on the run-start card (player critic over the drafts; 4.15.6).

---

## 4. Rules

### 4.1 Camp form (agreed, Q3; change CH-22 for the input rows)

- **(agreed, Q3)** Each camp is one fixed isometric **diorama**, about 9×7 squares *(illustr.)*. No avatar, no walking, no rules squares, no ticks, no command-log entries from anything that moves. ITEMS 10.3 ("no floor, room or stairs at camp") holds without a reading.
- **Stations** around one fire: **the Fire** (rest summary, trophies), **the Bedrolls** (party view), **the Muster Post**, **the Pedlar's Cart** (vendor, Sell, Notables panel, M3 Tithe Bowl), **the Door or Doors**, and **the Edge of the Light** (where the Fireside sit). Every Fireside seat lies outside every station's focus frame and hotspot volume (AN10; `CampSetRecord`; V20s `AmbientNotServiceAnchor`) **(lead's call, after the check)**. The Fire station focuses on the ribbon post and the rest summary; the Log-Carrier feeds the fire from a heap of forgotten things on its far side, at the rim, never at the station anchor, so he never reads as the one who restores the party.
- **The dolly:** focusing a station moves the camera there in 0.4 s *(illustr.)*, and the nearest party figure stands and turns toward it (generic clip). UI-layer only; reduced motion and Quick camp make it a cut. V20t `CameraDriftNoCommand`.
- **The Camp List:** a left-side list mirroring every station, on the bumpers (stations only) for controller and keyboard. Ambient figures are **not** on the station ring; they sit on a separate **Hear out** sub-list on one face button (V20s `StationRingNoAmbient`).
- **(told plainly)** A walkable camp is rejected (Q3-B); B's sets may be authored so that a Later walkable option costs no new art.

### 4.2 The camp order as played (changes CH-1 ⚑, CH-2 ⚑, CH-26 ⚑; order unchanged)

The approved order is kept. Step 3 restores "to the post-catch-up maxima", so the rest **cannot** be shown before the catch-up picks (consistency critic). The presentation uses the three confirmed headings, Arrival, the Hollow and Travel on, with the same content and order as the proposal **(lead's call, after the check:** the earlier A–E phase list is regrouped under the client's three headings**)**.

#### Arrival (steps 1–7)

**A.1 Get-up (0 clicks, about 3 s, skippable).** The fire flares; Downed companions arrive supported by an ally and get up at their bedrolls (step 1, "wherever their bodies lay"). *Committed in the arrival write* (WO4; CH-2): get-up and, for each unit with a pending catch-up, **only its first pending offer**, or none **(lead's call, after the check)**.

**A.2 Catch-up (step 2), the one forced stop.** Hero first, then companions in party order (L3/6/9 only, CMP 8.2). Each pick is its own command with its WO8 write. Because the hero's catch-up can span more than one level (ENM 16) and ABL 10.2 filters each draw by the unit's current build, **a later level's offer cannot be drawn at arrival**: inside the same WO8 write as a pick, Core resolves that level and, if the unit has a further pending level, draws that level's offer from the key `levelup/camp/{k}/unit/{unitId}/level/{L}` with a fixed draw count (CH-2 adds the key to SV 4.3) **(lead's call, after the check)**. Companion 3/6/9 choices are CMP's fixed two-option grammar and draw nothing.
- **"Accept kit defaults"** (CH-17) issues the ordinary default pick commands for every companion with a pending choice at once and shows one line per companion ("Brask took Shieldwall · change"). **The change link is live only until the rest-summary command is issued; after that, a change goes through Rethink, once per companion per camp (CMP 8.2)** **(lead's call, after the check:** a free change after the rest would reorder the camp order and leave pool maxima undefined**)**. The hero's picks are always manual. The press is hidden on a profile's first 3 runs *(illustr.)*.

**A.3 Rest summary (steps 3–7, one core command).** **`CampRest` is a system command that Core issues automatically when no catch-up pick is pending** (immediately after WO4 when there is none). It is recorded in `run.log` with its seq and written with its own WO8 write before the rest-summary frame. A reload between WO4 and that write re-enters the catch-up phase when a pick is pending, or re-issues `CampRest` deterministically; `CampProbeResistance` covers the case **(lead's call, after the check)**. The screen is shaped like the DH 17.11 rest screen: HP and pools to full (post-catch-up maxima); lingering conditions ended (bandages off; torn blanket mended); "Supply 2 → 3"; the potion top-up, or "dose moved from <name>"; free identify ("The fire shows what you carry", IR verdict marks); the 6a card fills the door; card A and the Eye's card are ready on the post. Information still comes before the Muster.

#### The Hollow (steps 8–10, free order until Travel on)

Muster, vendor and Notables are concurrent stations; the Bedrolls are always open. CMP 7.11 already makes the Muster revisitable "so a newcomer can be geared at the vendor". The `choice` state is unchanged: browsing never commits.
- **The top-up re-check runs at the dismissal (CH-26 ⚑, a reading of CMP-22 timing).** When a dismissal or swap deletes the hero's bound dose, the DH 13.2 top-up is re-applied inside the dismissal command and its WO8 write: the dose moves, or a camp-issue potion is issued, then. If it has no belt or pack room, the dismissal's own confirm names the forfeit, so DH 13.2's "explicit choice" is made where it happens. Travel on therefore has nothing left to re-check (the step-11 re-check is kept as an assertion that finds nothing) **(lead's call, after the check)**.

#### Travel on (step 11)

- **The lit door's caption always carries the Party Forecast** (CMP 7.11: "Fewer companions..." and the carried-hire line), never as a modal. Unbought limited stock and an open slot with an unused hire are shown as **passive lines on the door card**, never as confirm reasons (CH-26 ⚑) **(lead's call, after the check)**.
- **Keyboard and mouse:** one click on the lit door. **Controller:** Travel on is a dedicated hold (0.6 s *(illustr.)*) reachable from any station; the door is never default-focused; the hold prompt shows any confirm lines, so **the hold is the confirm**.
- **A confirm appears only when something the party owns or is owed would be lost**, and lists it: camp 4 with a purse at least the cheapest vendor item (LE 6.15's line, restated); `choice` still unset (leaving dreams both entries and, on a phial camp, loses the Reprieve Phial, named) (7.1); a Perilous Door choice (M3). On keyboard and mouse that is one click plus the confirm; on controller the hold.
- **The sequence is exact:** confirm (if any) → door commit → GS1 `GenInputs` freeze → keep the speculative floor on a digest match or regenerate (7.5) → WO5. The figures walk through the door (cosmetic).
- **CH-26 fallback** (if the CMP owner refuses the narrowing of D1): D1's one-line confirmation showing the forecast appears whenever a Muster board with undeclined cards is declined, and the loss lines above join it.

### 4.3 Stations (lead's call)

- **Bedrolls (party view):** equip, hand-overs, stance re-pick (VG-17, Guarding warning, Shieldwall suggestion), spell swap (ABL 6.5; Arcanist pair; Herald L3 locked), Herald card swaps, Rethink, Deeds, LE 6.7 "unused since floor N" tags.
- **Muster Post:** CMP 7.11 two-tier cards and Party Forecast; `armReroll` and `eyeReroll` flip only after their WO8 write lands (`ProbeResistance`); one dismissal or swap ("X leaves for good"); same-visit undo; the 3 s first-recruit beat. Old Friend (M3) and Pledge cards (Later, "Met on floor N") are cards here, never figures.
- **Pedlar's Cart:** supply (greyed at cap 3); potions in limited stock (CH-16); tonics, Remedy, bases; Wax Key row with its reason line (M2); Ear Trumpet row (M3, conditional); Sell at 250‰ with `ItemVerdict`; the coin ledger; **Quick Restock** (one `BuyBatch` command with a preview naming every line, CH-18), offered by default on a profile's runs 1–10 *(illustr.)*. Tithe Bowl (M3): one purchase per camp at camps 1–4; at camp 4 only the Surveyor's Sketch (XI 4.6.3); its facts dedup through the FactLedger unchanged (EV); shown greyed with its reason while the party holds a Wax Key, until the LE owner rules on XI's relaxation request (LE 8.1) (lead's call, after the check). A seeded run's code appears on the cart's ledger tab and on the dream-track ribbon (SV 4.9); a personal run's never.
- **Notables (the cart's second panel):** the shop-or-reward choice (CH-15; 7.1), and, when TL 6.11's notables slot drew a Wax Key, that key as a shop-only third entry.
- **Door(s):** the trimmed next-floor card (RS 6.9); two physical doors dressed in each theme's palette and signature prop when Two Doors passes its gates (camps 1–2; camp 3 in M3 when the F4 pool reaches 3); the final-boss card or Last Hollow choice at camp 4; the Perilous toggle on Two Doors cards (M3, 7.6). The chosen door is lit and changeable until Travel on. A dream-track ribbon above the door names the next floor; M3 pins the previous floor's Atlas page; M3 adds the Lore roster preview in the card's detail tier. No pointer result ever (XI-24).

### 4.4 Saves at camp (changes CH-2 ⚑, CH-3 ⚑, CH-9 ⚑, CH-19 ⚑; readings of SV 4.8.3)

- **WO4** at floor exit writes `run.save`, carrying get-up and **only the first pending catch-up offer per unit** (A.1). Each later offer is drawn inside the WO8 write of the pick before it (A.2).
- **WO8** before the outcome frame of exactly the commands that draw, spend or reveal: each catch-up pick (with the next level's offer draw); the system command `CampRest` (identify reveals, card A resolves); Buy and `BuyBatch`; Sell; hire, dismiss (with the top-up re-check, 4.2), swap, undo; arm, gap and Eye rerolls; **`ChooseReward`** and the shop commit (7.1); the Tithe Bowl purchase. **"Training" reads as level and skill picks**; there is no training service.
- **Not WO8:** equips, hand-overs, stance, spell and card swaps, Rethink, the door choice, the Perilous toggle. They ride the next write.
- **Talk is UI-layer only:** no command, tick, seq or WO8 frame. Camp seen marks and the run-scoped lore state (4.16) are display-tagged data in `run.save` (a `loreSeen` kind in the existing `pendingProfile[]` plus the `loreRunState` record), riding each `run.save` rewrite, and flushed to `profile.save` only where SV already flushes (WO6, WO7 load, abandon) (CH-9). A crash at worst repeats a line. Hub lines are written by the hub lore write (4.16; CH-9).
- **WO7 is Load:** at camp it reads and validates `run.save`, flushes `pendingProfile`, and rewrites `run.save` by atomic replace; no suspend slot. **SV defines no write on quitting, and this design adds none: a quit at camp keeps only what the last WO4 or WO8 write holds** (told plainly after the check). Equips, hand-overs, swaps, Rethink, the door choice and the Perilous toggle made after the last such write are lost on quit; a reload shows the board, the purse and the lines as of that write **(lead's call, after the check:** the proposal's "quitting at camp rewrites `run.save`" described a write SV does not have**)**.
- **Speculation (CH-19, reading of SV 4.4):** after every committed camp command and after every non-WO8 change that enters `GenInputs` (the door choice, the Perilous toggle, a hire or dismissal), Core recomputes the candidate `GenInputs` digest. If it differs from the in-flight or finished job's digest, that job is cancelled or discarded and a new one starts after a 0.5 s debounce *(illustr.)*. A speculative result, a release-end unravel (SV 5.5) included, is held in Core memory only, never exposed to the display or to lore, and used only at Travel on on a digest match (`SpeculationEquivalence`, with an unravel case) **(lead's call, after the check)**. The "fire gutters; you walk into the dark" dressing shows only when a synchronous rebuild exceeds 300 ms.
- **Unravel at Travel on (SV 5.5):** the honest card over the dimmed diorama; the fire dies; the figures fall still and silent. No humour, no blame.

### 4.5 Time and click budget (change CH-5 ⚑)

- **(told plainly)** RS 0.1 budgets about 12–16 min of camps inside the 104–115 min run; nothing measures it.
- **Targets *(illustr.)*:** returning player, median per camp ≤ 90 s without a hire and ≤ 150 s with one; first visit to camp 1 ≤ 4 min; minimum inputs after catch-up: keyboard and mouse 1 (2 with a confirm), controller 1 hold (the hold is the confirm); ambient lore 0 required clicks, ≤ 30 s when engaged; Two Doors +15 s, Last Hollow choice +10 s (RS); Perilous +5 s.
- **Reports:** `CampSeconds` and `CampClicks` by camp index and party size, on a first-10-runs arm (the Log-Carrier's orientation beats included) and a veteran arm; `RunWallClock` (floors + camps + cards + end screens, median ≤ 120 min, with a new-player arm); `HubSeconds`; `DeathPathSeconds` (with a `DeathNightHubLines` count); `QuickCampShare`. These measure human time and choices, so they come from SV 6.11 human sessions until telemetry consent exists (9.2) (lead's call, after the check).
- **Cut order if a median misses:** (1) party turn clips; (2) the dolly; (3) arrival compresses to a card; (4) Straggler caption-only; (5) Quick camp default on.
- **Quick camp** (setting): skips animations; never auto-commits a decision.

### 4.6 Camp variation (changes CH-4 ⚑, CH-11 ⚑)

| Camp | Act seam | Dressing | Register | Figures |
|---|---|---|---|---|
| 1 | Shallows → Midwater | Seam set, half each palette | Comic allowed | Log-Carrier, Slate-Sketcher, Straggler (deepest pools) |
| 2 | Inside Midwater | The quiet camp: fullest fire, longest idles | Dry | Log-Carrier, Slate-Sketcher, Straggler |
| 3 | Midwater → Deep | Seam set | Dry | Log-Carrier, Slate-Sketcher, Straggler |
| 4, the Last Hollow | Deep → Heart | Low fire; music box nearly broken (approved) | Absent | Log-Carrier silent; Straggler half asleep (1 plain line); Stillwatch (≤ 1 Hear-out beat) |

- **Seams are keyed to the act palette** (2 seam sets), never to the theme pair (15 variants avoided); theme lives in residue overlays (M3), the Straggler and the door dressing.
- **Camp 4:** idle-bark cap 0; one plain keeper spend-down line; the far door shows the Heart's wall "always the same distance away"; no Lodestring, Ferryman or Blood Price. The far door stands off to one side of the diorama, so the Stillwatch watching it keeps a three-quarter face to the fixed camera. **No Grudge caption on the camp-4 door** (the Grudge stays on BR's boss plate and Ledger page; the proposal's door caption put profile-derived information on a pre-pick surface without a BR row) **(lead's call, after the check)**.
- **Peril tiers (M3):** one cosmetic tier-mark at camp (a guttering second fire *(illustr.)*), greyscale-gated.
- **Seeded runs:** the borrowed-hand mark on the ribbon.
- **Register rule (CH-11):** camp k uses the drier of floors k and k+1. Death-, downing- and memorial-adjacent lines are always sincere. **On a Night that ended in death, every line shown on the death path, the floor-k vignette and hub-entry lines included, is dry or sincere, never comic** (AN5; V20s `RegisterByVenue` rows keyed to the death-path venue; `VignetteNotComic`) **(lead's call, after the check)**.

### 4.7 The run told without words (lead's call)

- **Ribbon post:** one prop per beaten boss, from a per-boss display record `TrophyRecord {bossId, propRef}` held in this design's data (9.1), since RS 9.3's `keepsakeProps` is per theme and is F5 dressing **(lead's call, after the check:** keeps the ~14–16 per-boss props of 6 and edits no boss record**)**; neutral materials; never a container, Windfall or Washstand silhouette (RS 9.3, LP-24); Inspect shows a caption.
- **Ribbon and chalk:** the chosen door's ribbon is tied to the post; the declined door appears at the next camp as a chalk outline (one visual; at most one voice per run says it, `factOncePerRun`). **Camp chalk is pictorial, never plan-view:** a door drawn in elevation on the Slate-Sketcher's slate, never floor line-work, never in XI's chalk colour or line weight, so it can never be read as XI's Chalked squares or chalk maps (XI's information grammar) **(lead's call, after the check)**.
- **Truthful bedrolls:** one per member; a folded unrolled bedroll per open slot (none under the Lone Road, CH-23); a torn blanket for a companion Downed or Scarred this floor, mended in the rest summary; the top Deed carved (M2 if Deeds ship, else M3, per the CMP cut line); a dismissed companion rolls and carries off their bedroll. `bedroll.vacated` is reserved for the Later Mortal rung only.
- **Theme residue (M3):** Gaol rust flecks, Kennel burrs, Wake lily pollen, Choir damp, Archive lint, Carnival greasepaint, Supper frost, Gut stain (8 overlays); on cloaks and packs only, never limbs or weapons, with no strand-across-body silhouette, checked against the condition icons; no damage hue (RC4); cleared at Travel on **(lead's call, after the check:** Gaol and Wake authored to match the ×8 art count; soot is the Log-Carrier's tell and threads read as a web condition**)**.
- **Strands (M3):** carried Strands hang as knotted cords over the fire; the keeper names them (EV 4.5.7).
- **(told plainly)** The client's "empty bedrolls for the dead": companions are never killed and a hero's death ends the run, so no camp follows a death. Empty beds for the dead would be a false signal (RC9). Mourning lives in the hub.

### 4.8 Hub form (agreed, Q1, Q4, Q5; changes CH-6 ⚑, CH-13 ⚑)

- **M1:** death or victory screen, run record, then **Begin**.
- **M2 (decided "simple camp screen", MAIN 4.7):** one static Stairtop backdrop with menu rows: **Begin** (launcher: class only); **Continue**; **Play a run code** (code field, "A borrowed night: advances nothing", mismatch notices; SV 4.7/4.9); **Book of Nights** (run history with "run code", "older build" and unravelled marks; Grave Goods; codes of finished personal runs with a copy button); **Profiles**; **Settings**; the **Hearsay** log. One static figure, **the Copyist**, at the Book, and the floor-k Straggler vignette after a death on floor k (trigger in 5.1) **(agreed, Q4; CH-13)**.
- **Begin while a run is saved (lead's call, after the check).** One save slot per run (MAIN 4.1): while a `run.save` exists, **Begin** is replaced by **Continue** plus **"Abandon and begin"**, which runs SV's abandon path (flush `pendingProfile`, the `runHistory[]` entry, files removed) before the new run's WO1. "Begin again" appears only on the run record screen, where WO6 has already completed (4.10).
- **M3 (after M2 sign-off; CH-6):** the same backdrop as a **menu-room** diorama with dolly, never walkable. Stations: **the Banister** (launcher); **the Depth gauge** (difficulty tier and `nightmareTier` lamps as separate fields; a reserved unlit lamp below tier 0 for the visible easier setting; one reserved run-modifier slot, which the heirlooms and curses design asks the MAIN/RS owners to fill with Oaths, Later (a request, pending; 4.9; change HL-21)); **the Book of Nights** (Copyist; Atlas pages; records); **the Library** (codex, Boss Ledger and Grudge, Residents pages, Milestones, Veteran book, IR Whence, HZ Dream Rules); **the Memento wall** (Gilder); **the Unwoken Roll** (Roll-Reader); **the Heirloom shelf** (one starting boon: heirloom templates with exact text, Old Friend, Wayfarer's Belt; Lone Road toggle); **the Newel** (classes; species M4; dressed non-domestic: each class's mantle hangs from its own iron peg driven into the stair's newel post, with no rail, no hooks by a door and no hallway framing, so it never echoes EV's Heart "Coat Hooks by the Door" or RC2's empty coats on a rail; a role-ID gate row checks it, and the naming pass named it the Newel, NP-26) **(lead's call, after the check)**; **the Letter Rack** (seeded codes; daily nights Later) **(new lead's call:** renamed from the proposal's "Sealed Letters", which collides with EV's Strand name "Sealed Letter"**)**; **the hub fire** (Log-Carrier); settled Stragglers on the balcony (agreed, Q5; 5.1).
- **The Heirloom shelf (M3; heirlooms and curses design 4.1.3; change HL-9):** **6 hooks** *(illustr.)* in view, the rest wrapped on the lower board and swapped freely between runs; unlocks are never capped or deleted. **The boon picker lists every unlocked template**, hung ones first; hooks never limit the choice and their arrangement is display, outside `configHash`. **Four states that never look alike:** hung (on its hook); stored (a solid wrapped bundle with a byname tag); locked (a paper-cut outline on an empty hook shadow with its goal, worded to the credit rule, e.g. "Fell an F3+ boss with a crossbow in hand"); pending (an open bundle reading "Shelve it?" with the byname or family name, chosen **Shelve it** or **Not this time** through the hub's atomic write; Shelve it never displaces a hung template). Each card shows its exact line, band, usable-class chips, "Replaces: <piece>" per class and its Grip-marks (4.11). Confirming creation with a class that cannot use the held heirloom reopens the boon picker inline (4.10's "no longer valid" path). Kit-variant bundles, Old Friend and the Wayfarer's Belt keep their own row; the Lone Road toggle is unchanged. A role-ID gate row covers the four states in greyscale (6).
- **Fiction (agreed, Q1):** the Stairtop, "the one hollow that does not re-form", at the top of the stair down; it remembers because "the dream remembers you" (RS 7.5). A hollow that does not re-form is an **addition** to RS 7.5's agreed premise table ("Each night the dream re-forms"; "Camps?"; "Unlocks?"), so CH-6 flags RS 7.5 and the RS owner joins as an applier (lead's call, after the check). Lines stay premise-neutral (AN4); the premise stays open (RS 19.2). Rejected: B, a waking-world house; C, menu only.

### 4.9 Launcher (settled; told plainly; change CH-24 ⚑)

- R-SEED-6 (theme streak, Fallow K=3, F1-boss rule, onboarding preference, ≤ 8 redraws) runs silently and outside the seed; shared, daily and simulator runs bypass it (settled).
- No seed and no forecast before creation; no figure reads or hints at the forecast (`NoForecastRead`).
- Choices: class (species M4); one boon (M3); difficulty tier (M3; the ladder is M3 per MAIN 9); Peril tier (M3); Lone Road (M3).
- **Starting kits:** one default kit per class creation pick (Shade Twin Blades or Bow and Blade, Herald Shield hand or Focus hand); no other presets (VG row 12 rejected kit presets); variants ship only as boon options under the one-boon rule, so there is **no kit peg** (told plainly) (starting kits design 4.4; change SK-8, M2; meaning unchanged).
- **The Bobbin (M3; a request to the MAIN/CH owners, pending; charm abilities design 4.11; change CA-20; agreed, charm abilities Q4):** one boon option under the one-boon rule: choose one unlocked Common catch charm at run start, issued to the hero; never a button, Keep, Uncommon, heirloom, persistent or cursed. The boon picker shows each family as its one-line card in a list, with the "rarely fires for this unit" verdict for the chosen class on F1 and on F2+; no shelf object and no hub surface (CH-24). Earned by the milestone "a charm fired in a won fight" (4.11). The chosen family enters `configHash` (SV 4.6) and is checked against the run's `drawableSet` before WO1; off on shared, seeded and daily runs.
- **Oaths (Later; a request, pending; heirlooms and curses design 4.5; change HL-21; agreed, heirlooms and curses Q6):** the MAIN/RS owners of CH-24's run-modifier slot are asked to fill it with one opt-in Oath, chosen here: kind, not stats; content-only rewards; `requires[]` and `conflictsWith[]` validation; off on seeded and shared runs until the SV owner rules on HL-25. Until they rule, the slot stays unlit (CH-24).

### 4.10 After the run, and the death-path budget (lead's call; change CH-14 ⚑)

- **Death:** "You did not wake" → DH 17.12 recap (neutral; M3 fatal-room map) → run record (Grave Goods, "Fell with:") → the Stairtop in its post-death lighting state, the hero's **coat peg empty** (M3), the Roll's newest mark visible as an object (never modal or auto-opened; M3) → launcher. **At most 1 spoken sincere beat** between the run record and the launcher (the Roll-Reader's caption on the Roll, M3; in M2 none). The comic ambience bus is **silent** during the recap and on the death screen (RC8, agreed; 6). `DeathPathSeconds` median ≤ 20 s *(illustr.)* on the streak arm (CH-14).
- **The post-death lighting state reflects only the last Night** **(lead's call, after the check)**: one cooler, lower state for that hub visit, reset on the next Night and never stacked, so the room never encodes a death or streak count (AN11; `NoCumulativeCount` covers display states, not only lines). It is a warm-to-cool shift, not overall darkness; stairwell depth is reserved for Peril tiers (4.11).
- **A death Night in the hub (lead's call, after the check):** the unprompted hub cap is **1**, and that line is the death owner's (the Roll-Reader's caption on the Roll in M3, unless hidden) or, failing that, the floor-k vignette's (M2, or M3 with the Roll-Reader hidden or silent). Other figures speak only on Hear out, and only after the player focuses another station. No impression, exchange or comic line plays that Night (AN5, AN7).
- **Win:** the Memento goes to the wall (Gilder, M3), captioned "carried out of the Heart"; **the hero's peg holds the Memento's ribbon**, neither a coat returned nor a peg emptied, so no win reads as the hero coming back or waking **(lead's call, after the check)**; the Gilder pins the **carried** Oddment's tag to the corner of the Memento frame under the same caption, at no figure-line cost, and it is the first cut if `OneWinOwnerPerNight` refuses it (M3; starting kits design 4.5.2; change SK-6); the Copyist's page shows a clean hand (an object caption); the first win is spoken by **one figure only**, the Log-Carrier at the hub fire (M3), with the Copyist and the Gilder as object captions (`OneWinOwnerPerNight`); in M2 the Copyist's page caption carries it. The next ladder lamp lights. No "you woke", "back" or "kept going" language; the waking-light set piece stays BR Later.
- **First-win timing (lead's call, after the check):** outcome-keyed T0 lines key on "the last personal run's outcome = win, and first win". If the hub is skipped that Night (Begin again), they wait as deferred lines (`loreMemory.deferredT0[]`) for the next **non-death** hub entry; on a death Night they are suppressed and not marked seen (`CelebrationNeverOnDeathNight`).
- **Unravel:** "The dream came apart. Not your doing." plus the plain SV 5.5 fact; the Copyist's "a page came loose"; no Roll mark; no Roll-Reader; the hero's peg holds a loose thread (no mark). Audio: silence, then the bed; never the death stinger (6). No vignette.
- **Abandon:** neutral; no T0 line (nothing to farm); counts as a Night only if it reached camp 1. **An abandon writes nothing to `loreMemory` unless it reached camp 1, and never seats the vignette** (`AbandonWritesNoLore`) **(lead's call, after the check)**. `AbandonWritesNoLore` also covers the Oddment: start, quit, reload, abandon on F1 → `oddmentsSeen` unchanged (M2; starting kits design 4.5.2; change SK-6).
- **Seeded:** a marked entry; lines may play (from the hub's display root, 4.16); nothing written to lore, codex or unlocks. **The hub after a seeded run** reads the seeded `runHistory[]` entry for the win or death stinger only: no peg, Roll, lighting-state, vignette, chain or settle change **(lead's call, after the check)**.
- **Fast path:** **"Begin again"** sits on the run record screen, after the recap (skippable) and after "New in the dream" (4.11), and reaches class pick in ≤ 2 clicks, skipping the hub. It **carries forward last Night's launcher choices** (tier, Peril tier, Lone Road, and the same boon if still valid) as one editable line on class pick ("Same as last run: Tier 2 · Old Friend · change"); if any choice is newly unlocked or no longer valid, that line opens the boon picker inline **(lead's call, after the check)**. Chain beats are not spent while the hub is skipped. (Not "Go down again": "goes down" is the downing verb; told plainly.)
- **Pending heirloom offers (M3; heirlooms and curses design 4.1.2; change HL-6, a request to SV, pending; CH applies):** Begin again's inline boon picker lists pending offers with **Shelve it** and **Not this time**, and class pick shows one non-modal line, "An heirloom waits on the shelf."; nothing is lost to skipping the hub. There is no heirloom card on the death path or the win path, so `DeathPathSeconds` gains nothing. Fallback if SV refuses: the unlock applies at WO6 and shows at the next shelf visit (heirlooms and curses design 14.2).
- **The Bobbin (M3; change CA-20, a request to the MAIN/CH owners, pending; agreed, charm abilities Q4):** Begin again's inline boon picker offers it as in 4.9; a carried-forward Bobbin family that is no longer drawable is "no longer valid" and opens the picker inline (charm abilities design 4.11).
- **(new lead's call)** In M2, where the floor-k Straggler vignette sits on the hub screen after a death (CH-13), its line counts as the M2 death path's only ambient line and plays only after the recap closes; it is never death-keyed and never comic (4.6), so the M2 "none" spoken sincere beat above still holds.

### 4.11 Unlocks and milestones (M3; lead's call)

- After the recap, one **"New in the dream"** card names each unlock as content, "from your next night" (unlocks snapshot at run start, SV).
- Every unlock class has a hub object: a coat per class (subclass tag on its coat); a painted door on the stair per theme; a sealed frame per final boss; a kit tag in the Veteran book (the companion-kit unlock object only, never a hero kit variant; reading, starting kits design 4.4); loot unlocks in the Library (charm-family unlocks and the Bobbin's milestone included, M3: they use the Library's generic loot-unlock object and are not a charm hub surface under CH-24; a reading, change CA-21, CH confirms; charm abilities design 4.10, 4.11). V20s `UnlockHasHubObject`.
- **Kit variants (M3; starting kits design 4.4; change SK-7; agreed, starting kits Q1):** a variant is a boon option (a full mundane kit replacing one creation pick's default, plus one skill training), offered on the Heirloom shelf with its exact contents and skill shown before the pick. Its hub object is one bundle per class on the Heirloom shelf, tied in cloth and marked with the class glyph and a pattern, never colour alone; **no coat tag on the Newel** (CH-24). Variant milestones are achievement-keyed ("Reach floor 3 as a <class>", "Fell the floor-2 boss as a <class>"), hidden for a class not yet unlocked, and carry "run codes do not count"; "New in the dream" names the variant. A variant taken at the shelf pre-selects its class and creation pick; a later mismatch shows one non-blocking line, and confirming creation with a mismatch clears it and reopens the boon picker inline on 4.10's "no longer valid" path (also on Begin again). Trigger to revisit CH-24: a per-offer pick rate below 0.5 × fair share over 2 playtest rounds with ≥ 30 offers each *(illustr.)*.
- **Heirloom templates (M3; heirlooms and curses design 4.1.2; changes HL-1, HL-10; agreed, heirlooms and curses Q2):** earned only by achievements, never by deaths, counts or copied loot: **R1** class milestones ("Fell the floor-3 boss as a <class>", ordered after the variant milestones by difficulty only, with no prerequisite); **R2** Deed bequests (keyed to the Core `BossFelled` fact, HL-31; tier-gated; chosen later at the shelf, 4.8); **R3** Heart-won (the first personal win over each final boss). R4 Oath wins and R5 Redeemed templates are Later (HL-24). The hub object is the template on the Heirloom shelf; "New in the dream" names it; goals are plain words, hidden for a locked class, with "run codes do not count", never percentages or counts. One WO6 writes, and the hub reveals, in a fixed order: variant milestones, R1, the R2 offer, R3 (HL-10; CH confirms).
- **Grip-marks (M3; heirlooms and curses design 4.1.5; change HL-5):** display only, in profile display memory outside the key: an heirloom's chosen Deed byname and up to three Deed marks (boss and theme), never a count (AN11), a hero name or death text. Hub art: up to three notches on the grip wrap and the byname on the bundle's tag.
- **Milestones page** in the Library: earned milestones as marks; unearned ones as plain-worded goals only where MAIN wants pull; never percentages or completion (LP CF10, LE 6.12).
- Growth is never power: settled Stragglers; the Book thickens in bands; Peril deepens the stairwell (depth and colour, a channel kept apart from the post-death lighting state, 4.10).

### 4.12 Onboarding (lead's call)

- First launch goes straight to creation and F1. The hub first appears after run 1.
- **If run 1 ended in a death** (per wave; lead's call, after the check): **M2:** the static Stairtop screen with Begin; the Copyist silent; the floor-k vignette line if the death was on F1–F4 (never comic). **M3:** the stair, the empty peg and the Roll-Reader's one sincere caption. No intro, no reveals in either wave.
- **The orientation** is **3 short captions on stations** (the Banister, the Book, the Library in M3; the Begin row, the Book and Settings in M2), not a 20 s talk, and plays at **the first non-death hub visit, or the third hub visit, whichever comes first**, so it never follows a death on a floor-1 streak **(lead's call, after the check)**.
- Stations reveal via `uiMemory` (XI-21): Roll-Reader after the first death; Understudy after the first boss met; Gilder after the first unlock or win; shelf after the first unlock (a pending heirloom offer waits for the shelf's reveal, which carries it; reveal order as 4.11; heirlooms and curses design 4.1.2).
- Camp 1, first ever visit: the first time each irreversible action is reached, its consequence line shows inline (not modal), once per profile: Notables commit, dismissal, potion forfeit, Two Doors. One-line station callouts on a profile's first 2 runs. The Log-Carrier's three-beat orientation (display only) is spread **one beat per camp over camps 1–3 of run 1**; each beat is the Log-Carrier's one unprompted line at that camp, so AN7 and `PerVisitCaps` hold **(lead's call, after the check)**.
- **Creation and the kit (M2; starting kits design 4.6; change SK-9):** run 1's class card shows the simplified gear strip (kit silhouette, role word, AC and the hand-off line; Shade and Herald show two strips with the default pre-selected); from run 2 (`uiMemory`) the full strip shows set verbs as text, armour and AC, potions, coin and supply, and the "Also carried" heading with the Oddment's name. Creation warnings for a Strained kit item or AC below `kit.acFloor` are one line of shape plus text, never blocking and never auto-swapping gear; Floor1ClassSpread adds a worst-legal-rearrangement arm. The Oddment callout is one-time and non-modal, the first time the hero inspect opens at camp 1, never at F1 entry. M3: the variant pick-order line (4.11).
- **(new lead's call)** The first focus on any ambient figure shows the one-time tooltip "Has nothing to sell. Has things to say." (the risk mitigation of 15, made a rule), once per profile, recorded in `uiMemory`.
- Camp 1 and the first hub visit join the SV 6.11 human-session plan (CH-5).
- **Heirloom and curse first-time lines (a request to the CH owner, pending; heirlooms and curses design 4.8; change HL-28):** once per profile, in `uiMemory`, never modal: the first pending offer at the shelf or in Begin again's picker (M3); the first equip of a "Not yet read: may Cling" card, the inline content note before the first Malady preview (pointing to Plain names, 4.13), the first Malady chip and the first cast-off (M4); the first Oath lamp lit (Later). Once ruled, they join this list and the SV 6.11 plan.

### 4.13 Controller and accessibility (lead's call; change CH-22)

- Station ring on the bumpers; a "Go to" list; the Hear-out sub-list on a face button; Inspect parity; `NoHoverOnly`.
- Captions **persist until the next input or focus change**; "Hold captions" pins the last line in a corner tray (default on above 100% text scale). No timed text (V20s `AmbientTextNotTimed`).
- "Legible captions" (one legible font; default on above 125% text scale).
- Text scale 150%; reduced motion (fire flicker, chalk, dolly); colour-blind and greyscale gates on every camp and hub set.
- Settings: No idle chatter (no unprompted lines; Hear out still works), Hide memorials (Roll-Reader and Roll), Quick camp, mute chatter, telemetry consent (MAIN owes).
- **Plain names and the content note (M4; heirlooms and curses design 4.8, 5.3; change HL-27; agreed, heirlooms and curses Q1):** a display-only **Plain names** setting shows every Malady and curse name, on every surface, as its unique per-record `plainNameKey` label (e.g. "Malady: Easy Opening"); the **content note** on Maladies sits in Settings and is shown once inline before the first Malady preview (4.12).
- Profiles (M2): select, create, delete with typed confirm; "Profile memory was damaged and has been reset" when a checksum fails; reset never touches `runHistory[]` without consent.
- Continue states: camp save, floor save, "older build" (SV 4.10.1 notice), unravel found on load. Abandon from pause at camp and on floors: one confirm; code shown after.
- Replays (Later): from a Book page, with the build-mismatch line.
- Records: a fixed short list (deepest floor, wins per class as marks, first win), excluding seeded, unravelled and version-crossed (not comparable) runs (SV Q1, Q2, Q3); no times or percentages.

### 4.14 The ambient charter, AN1–AN13 (lead's call; agreed, Q2 for AN1–AN3; binding; V20)

- **AN1** Never sells, buys, hires or is hired; never gives or takes items, coin, supply, HP, conditions, non-public information or any rules state. Schema-enforced.
- **AN2** Never gates content, odds, options or achievements. Lore-only codex pages allowed; the codex is never a HUD input.
- **AN3** Never a tip, never a lie about rules. Every `LoreLine` carries an authored field `ruleTouch: none | restates`, set in tone review; a `restates` line carries a `publicRuleRef` that resolves in the data-defined `VenueSurfaceRegistry` {venue → surfaces and their facts}, and every `{token}` in its text binds to a `LoreContext` fact whose source surface is in that registry (`LoreCarriesNoFact`). Whether a line touches a rule at all stays a tone-review judgement, not a validator claim (lead's call, after the check). Unreliable about history only. Never reveals what a Lore check, Playbill or pointer would.
- **AN4** Never the Dreamer (face, voice, handwriting, bed, age, biography); never "waking" as a promise, hero motives or endings; the EV 4.11 premise test. A separate display-only **AmbientSelfRefCap** of 1 per run, never DV2.
- **AN5** Register by venue (CH-11). No humour on downings, deaths, the recap, memorials, unravels or Downed arrivals. On a Night that ended in death, every line on the death path, the vignette and hub-entry lines included, is dry or sincere (4.6). No ambient line uses "down", "go down" or "went down" (the downing verb) or jokes on it (`DreamerLine` and `AmbientVerbUnique` token checks) (lead's call, after the check).
- **AN6** Never speaks over a card, a pick, a WO8 frame, the Travel-on confirm, the recap or the unravel card.
- **AN7** Per-visit caps: camp ≤ 2 unprompted lines across the cast and ≤ 1 per figure; hub ≤ 3, **and 1 on a death Night** (the death owner's or the vignette's, 4.10); Hear out ≤ 1 beat per figure; camp 4 unprompted cap 0. A Straggler gives **at most 1 unprompted line plus at most 1 Hear-out line per visit; the Hear-out line only if unseen** (lead's call, after the check). Object captions are not unprompted speech, except the Roll-Reader's death caption on the Roll, which is the death Night's one line.
- **AN8** Silence is legal; a busy idle replaces a repeat (the Afterword alone keeps never-silent).
- **AN9** No RC7 reserved cue: no bells, horns, chimes, ticking, wind-up click, alarm, needle-scratch, Windfall pluck, candle exhale; **no breath-family sample anywhere** (BR `FinalBossNoDreamer`). No footstep or whisper family either (joiner footsteps are RC7-reserved; whispers are breath-family). Allowed textures, **one family per figure, never shared** (lead's call, after the check): low hum (Log-Carrier), slate scrape (Slate-Sketcher), hat-brim turn (Stillwatch), pencil (Trusty), lead-leather creak (Whipper-in), ribbon smoothing (Wreath-Carrier), towel wring (Off-Shift Singer), card riffle (Shelver), placard-cloth flap (Showcaller), card set on board (Placecard-Writer), strap buckle, soft leather with no clasp click (Subletter), page turn (Copyist), bench creak (Roll-Reader, blind-ID gated against door open and close), silk sleeve rustle (Understudy), brush (Gilder). (A figure with no line yet still carries its tag; `AmbientRecord.voiceTextureTag` is required.)
- **AN10** Never a roster or class silhouette, a role prop held ready (RC2) or a kit accent; faces visible (RC3) to the fixed camera, so mindless types never linger; **never at a service station** (the Fire's rest anchor, the Bedrolls, the Muster Post, the Pedlar's Cart and Notables, the Door(s), the Banister, the Heirloom shelf, the Depth gauge, the Letter Rack): at camp, every figure sits at the Edge of the Light, outside every station's focus frame and hotspot; in the hub, a figure may sit **beside a record-only object** (the Book, the Roll, the Memento wall, the hub fire) but is never its focus target or its interface, and its Hear out stays on the separate sub-list (V20s `AmbientNotServiceAnchor`, `StationRingNoAmbient`) (lead's call, after the check); never the Night Usher, a Pledge source, a captive, a Veteran in the flesh or a dismissed companion. No knitting, nursing, lullabies, tucking-in, child or parent figures (the Mother-Shape's knitting-spear). No figure ever handles the music box.
- **AN11** No counts (deaths, runs, wins, streaks), in lines or in display states (lighting, props); no pity; no comparison to other players or heroes ("further than most"); never "try something easier".
- **AN12** Never reads the launcher forecast, a seed, an uncommitted value or a rules stream.
- **AN13 (reserved)** Companions are a separate future speaker class with their own cap (0 until Later), never more than 1 line per camp beyond AN7, never at camp 4. Until then figures may *name* companions through validated proper-noun tokens.

### 4.15 The lore engine (lead's call; changes CH-10 ⚑, CH-14 ⚑)

**4.15.1 Fact set `LoreContext`** (display only, built per visit):
- *Committed run state (camp):* floor and theme cleared; boss beaten and Rendition; Yielded, Cowed and leader-killed flags per faction; Downed and Scarred this floor; companions (kit, name token, joined, dismissed, top Deed); hero class and species; the boss's prize, Memento or heirloom carried; this floor's Curiosity and option (`curiosityDigest`); next theme and declined door; Perilous chosen; camp index. Strands and purse band: keeper only.
- *Profile snapshot (run start for camps, **stored in `run.save`** so a reload or WO7 flush never changes it; hub entry for the hub, past nights only), merged with the run's own seen queue and `loreRunState` (4.16):* last personal run's outcome and Grave Goods death key (floor, theme, killer category); deepest floor; themes and bosses met (`bossMemory`, M3); first win; Peril first reached; codex pages; Curiosity seen flags (read-only); Straggler meetings; chain stages; seen marks.
- *Oddment facts (M3; starting kits design 4.5.2; change SK-6):* display booleans `oddmentCarried` and `oddmentLeftThisNight`, plus a hidden `strataTag` key (never shown in a caption). A Straggler of the matching stratum may have one T2 line under AN3, AN5 and AN7 caps and `factOncePerRun`, never on a death Night; no figure ever asks for, takes or returns the Oddment (AN1), and it never gates (AN2). These lines are the second cut.
- *Never:* forecast, seeds, counts as values (booleans and bands only), HP, odds, streak lengths. Streak reading stays "how the last run ended" (EV 0.1 item 7); the draft proposal to read streak counts is rejected.

**4.15.2 Triggers (CH-10):** camp arrival end (after the rest summary); idle 20 s *(illustr.)*; Hear out; hub entry after the recap closes; hub approach (M3 exchanges). Never in combat.

**4.15.3 Delivery:** Overheard (0 clicks, ≤ 10 words, persists to next input, logged); Hear out (1 click, 1–3 lines, ≤ 40 words, closable, never blocks Travel on); object captions (≤ 25 words); exchanges (M3 hub, 2–4 lines). No trees, no choices, no Ask topics (rejected: cost for little gain; told plainly). Every line goes to the **Hearsay** log.

**4.15.4 Tiers, per figure per trigger:** **T0 event** (first meeting with a boss, theme, death key, first win, first unravel; once per profile); **T1 chain** (a ladder of beats; advances only on sight, win or lose); **T2 context** (unseen first, then cooldown 5 Nights *(illustr.)*); **T3 idle** (cooldown 3 Nights, never twice a run; recurring lines rotate variants B/C leading with the current fact); **T4 silence** (busy idle).
Within a tier: most criteria matched (Ruskin), then a **novelty term** (a fact key in `lastFiredNights[factKey]` for each of the last 2 Nights yields to the next most specific unseen line), then lowest `loreShownCount`, then oldest `loreLastShownNight`, then a hash on display domain `lore` from the venue's root (4.16).

**Cross-figure arbitration (lead's call, after the check).** Triggers are evaluated in a fixed order: arrival end (camp) or hub entry (hub), then idle, then Hear out. On each trigger, all eligible (figure, line) candidates across the venue are gathered and ranked by (tier, specificity, novelty, `loreShownCount`, `loreLastShownNight`, hash); candidates are taken in rank order while respecting the per-figure, venue and death-Night caps (AN7), the T1 cap (4.15.5), `factOncePerRun` (the first taker wins; later candidates with that key drop), `OneDeathOwnerPerNight` and `OneWinOwnerPerNight`. **Fact-key owners** are declared where a figure owns a fact: the first win belongs to the Log-Carrier in M3 and to the Copyist's page caption in M2; the hub death reaction to the Roll-Reader (M3); the road not taken to the Slate-Sketcher at camp, else the Copyist in the hub.

**4.15.5 Pacing:** ≤ 1 T1 beat per Night across the whole hub (the vignette's T1 line takes it only through arbitration); ≤ 1 per Straggler per camp visit. A Straggler gives **at most 1 unprompted line plus at most 1 Hear-out line per visit, the Hear-out line only if unseen** (AN7). Unseen T1 beats may play as 0-click Overheard at arrival end, so chain completion never requires Hear out; **the one exception is the Stillwatch, which has no T1 chain** (5.1) and whose lines are excluded from Residents-page completion. A **Night** is a personal run that reaches camp 1 or ends in death, win or unravel. **`nightIndex`** (in `loreMemory`) increments once per Night, at WO6, or at abandon if camp 1 was reached, through `pendingProfile`; camps in a run use `nightIndex + 1` provisionally; the hub uses the index of the Night just finished (after a seeded run, the last personal Night) (lead's call, after the check). `factOncePerRun`: one fact key spoken by at most one figure per Night.

**4.15.6 Death ownership and the Afterword.** The **Afterword stays on the run-start card, unchanged** (EV 4.5.10; `AfterwordExhaustion` untouched) (lead's call, told plainly: the drafts' move into the hub is withdrawn). In M3 the **Roll-Reader** owns the hub death reaction with its own small sincere pool. In M2 no figure reacts to a death: the Copyist speaks no death-keyed line, the vignette is never death-keyed, and the Afterword on the next run-start card carries M2. **At most one figure reacts to a death per Night** (`OneDeathOwnerPerNight` asserts ≤ 1, and exactly 1 only when the M3 Roll-Reader is unhidden) (lead's call, after the check). **Grudge-keyed lines** (BR-35: the boss killed the hero the last time the profile met it) are death-adjacent: they carry `toneFlags.deathAdjacent`, register sincere, and belong only to the Roll-Reader's pool; otherwise the Grudge stays on BR's plate and Ledger page with no speaker.
- **`AfterwordNoEcho` (lead's call, after the check).** The echo window is the hub visit after run r plus the run-start card of run r+1. The Roll-Reader's selector calls EV's Afterword selector on the current profile (a pure function) and excludes that line's **key family**: an authored `familyId` on `LoreLine`, defaulting to the (floor, theme, killer category) tuple of the Grave Goods death key.

**4.15.7 Streak guarantee (told plainly).** In M2 the guarantee is the Afterword's (holds to about Night 10–15 on the f1only arm) plus the floor-k Straggler vignette after a floor-k death (CH-13). In M3, with 1 beat per Night, at least one unseen hub line every Night **through about Night 25 per hub visit**. With the fast path, it holds per visit, not per Night; beats wait. The tail after Night 25 depends on M3 chains, settled Stragglers and EV's Sequels and Ripening. The beat cap is never raised.

**4.15.8 Codex (M3; RS owns, segment 10 hosts):** a "Residents of the Fireside" page per figure filling as T1 beats are heard; Straggler pages under their theme; a completed chain adds an illustrated plate. Write-only from the run's view; never gates; FactLedger never reads it. The `ink` dream-track cosmetic for finished chains is **dropped** (it would turn Hear out into a completionist chore; told plainly). The Library also hosts an **"Oddments" page** (RS owns), filled on sight, write-only from the run's view, with one illustrated plate per completed kit pool behind "a win as <class>" (M3; starting kits design 4.5.2; change SK-6; agreed, starting kits Q3).

### 4.16 Determinism (lead's call; changes CH-7 ⚑, CH-8 ⚑, CH-9 ⚑)

- **Selection is a pure function of (snapshot ⊕ run queue, `LoreContext`, the `lore` domain)** (lead's call, after the check). The snapshot is the `loreMemory` copy taken at run start and **stored in `run.save`** (display section), never re-read from the profile mid-run. The run queue is the run's own `loreSeen` marks plus a run-scoped **`loreRunState {nightId, visitIndex, spokenFactKeys[], selfRefUsed, t3Played[], chainAdvancesThisRun[]}`**, held in `run.save` for **every** run kind. All of it is display-tagged, rides each `run.save` rewrite and sits outside the key. So no line, chain beat or `factOncePerRun` key repeats inside a run, `AmbientSelfRefCap` and "T3 never twice a run" have their counters, and a reload re-derives the same lines. Personal runs flush the queue to `loreMemory` through `pendingProfile`; **seeded runs, replays and the simulator discard it at run end**, so a seeded run never repeats a line inside itself (it repeats only across runs; told plainly). `visitIndex` counts venue entries within the Night and is stored, so a reload reuses it.
- **Two display roots (CH-8) (lead's call, after the check).** **Camp:** `DisplayKey(run)` / domain `lore` / (nightId, campIndex, visitIndex, speaker). **Hub:** a profile-scoped root H(profileId, `lore`, nightIndex, visitIndex, speaker), created outside Core and never derived from any run key (the hub sits between runs and has no `runSeed`). In a seeded run the camp root derives from the shared run key, but selection also reads each profile's own snapshot, so two players of one code do not see a fixed shared line list. **Oddment row (M2; starting kits design 4.5.2; change SK-6):** an explicit run-start row under the camp root, `DisplayKey(run)` / `lore` / (campIndex 0, visitIndex 0, speaker "oddment", heroIndex, poolId), with **no `nightId`** (profile-scoped), so two players of one code see the same Oddment; selected from the creation-screen run key and frozen into `run.save`'s display section at WO1; personal runs skip ids in the run-start snapshot's `oddmentsSeen`, seeded runs take the first entry and write nothing; replays read it from the save.
- Outside `manifestHash`; in `displayHash`; a text edit never moves a seed.
- Core never references `AmbientRecord`, `LoreLine`, `loreMemory`, `loreRunState` or the `lore` domain (V20s `AmbientCoreBlind`, DS12, `CoreNoProfileRef`).
- **Writes by run kind:** personal runs write everything; **seeded runs, replays and the simulator read the snapshot and write nothing** to `loreMemory` (SV Q1 and V19t unchanged; **SV Q1's permitted seen counts are `curiosityMemory.shownCount` and `fullShown` only; no `loreMemory` field is among them**); version-crossed runs are personal and write. Seeded runs therefore repeat ambient lines across runs (told plainly). **The Oddment mark (M2; starting kits design 4.5.2; change SK-6)** is queued as a `loreSeen`-kind entry in `pendingProfile` on the first camp-1 entry (WO4), or at WO6 if the run ends before camp 1, never at WO1, and reaches `loreMemory.oddmentsSeen[]` only through SV's existing flushes (WO6, WO7, abandon), personal runs only. Its `left` state ("Leave it by the fire", from the Fire station's rest anchor) is a UI-only display write that rides the next `run.save` rewrite (CH-9).
- **The hub lore write (CH-9) (lead's call, after the check).** After WO6 the run files are gone, so hub lines (the M2 Copyist, the vignette, the M3 cast, settled seats) are written by a **hub lore write**: on leaving the hub (Begin, Continue, "Abandon and begin", quit; a hub skipped by Begin again has nothing to write) `profile.save` is rewritten by atomic replace with the hub's seen marks, chain stages, `lingererHeard`, `settled[]` and `lingererLastOnBalcony[]`, idempotent by (nightIndex, visitIndex), personal-Night context only. A crash repeats at most the last hub lines. After a seeded run the hub writes nothing.
- **The Hearsay log** is stored, personal runs only, as a bounded `loreMemory.hearsay[]` of {lineId, nightIndex, venue} (most recent 500 *(illustr.)*; older entries stay readable through `seen[]` with each line's recorded `firstNight`), so "nothing is missable" has a record. A seeded run's lines appear in the Hearsay log for that session only.
- Tests: `LoreInert` (wipe or randomise `loreMemory`: same core hash; extends `DisplayMemoryInert`); `SeededRunLoreReadOnly` (asserts fully qualified paths: `loreMemory.*` unchanged); `AmbientProfileBlind` (`SharedSeedProfileBlind` shape; covers both display roots); `NoLineTwicePerRun` (personal and seeded runs); `AbandonWritesNoLore`; `HubLoreWriteIdempotent`.

### 4.17 No mechanical effect (agreed, Q2)

Rejected: +1 supply or HP for listening; hints, true or not; discounts; morale or inspiration; lucky tokens; rerolls; boss warnings; cheaper Muster cards; a rumour board (information at camp is trimmed by RS 6.9; a board means hire). Reasons: chores (Dragon Age, Three Houses, Pillars, Hades nectar); breaks `DisplayMemoryInert` and `ProfileBlind`/IP9; runs into MAIN 4.7, EV Q4, EV 7, LE 6.12; blurs the client's own line ("a figure that gives is a vendor charging zero"). Allowed: codex pages and plates, hub props and poses, restatement of an on-screen public rule. Rejected alternative: Q2-B, cosmetics plus one tiny effect.

---

## 5. Catalogue

### 5.1 The Fireside (lead's call; placeholder names, all 0 grep hits; names updated by the naming pass, NP-26)

Every figure has a want, a wound and a tell. Sample lines are tone samples *(illustr.)*. **Revised after the check (lead's call, after the check; told plainly after the check):** sample lines, props and textures that broke this design's own rules (AN4, AN5, AN9, AN10, AN11) are replaced below; the figures, their wants and their roles are unchanged.

**Camp regulars**

1. **The Log-Carrier** (M2; every camp; the M3 hub fire). Feeds the hollow's fire from the far side, at the rim, out of the Fire station's frame, with things the dream forgot (a button, a letter, a chair leg). Soot to the elbows; a coat patched in every stratum's palette; never a shawl or knitting. Wry, brief, plainly kind. Texture: low hum. Run 1 only: his three orientation beats, one per camp at camps 1–3 (4.12). Domain: the hollows; the party as it is (kits, Deeds, newcomers, dismissals, Downed today, by name token). Camp 4: silent; the fire's caption speaks. Owns the first-win line at the hub fire (M3).
   - camp 1, comic: "Gaol wood. Burns slow. Thinks it's got time to serve."
   - "Old letter tonight. Burns slower than it reads." (replaces "The fire doesn't charge", which framed the fire as a free service)
   - Downed arrival, sincere: "<Name> got up. Warm side's for the ones who get up." (name token, past tense; plays after the get-up)
   - Deed: "<Name> took the blow meant for you. Warm side for <Name>."
   - newcomer: "New face. Give them the bedroll nearest the light."
   - chain: "The dream thins where nobody wants anything. I try not to want much."
   - camp 3, dry: "Fire's smaller this deep. It isn't sulking. It's listening."
   - camp 4 caption: "The fire is low. Nobody has fed it."
   - hub: "Same fire, every night. The rest of the dream starts over. This doesn't."
2. **The Slate-Sketcher** (M2; camps 1–3). Kneels at a standing slate at the Edge of the Light, never in a door's frame, drawing places in **elevation** (hills, a river with fish, a stick-figure choir) that never agree with the floors; never plan-view line-work, never XI's chalk colour or line weight (4.7). Chalk-white knees; a tin of stubs. Dry, a little vain. Texture: slate scrape. Name flagged against XI's "Chalked" (A.2); resolved by the naming pass (renamed from the Chalkwright, NP-26). Domain: the dream's geography; the road not taken. Restates only the public door card.
   - "Next is the Choir. I've chalked it wet so it won't argue." (`publicRuleRef`: next-floor card)
   - next camp after Two Doors: "You went by the Archive. The other road's still chalked. Chalk keeps."
   - "Mind the slate. That's a river. The fish are mine."
   - camp 3: "Below this, I stop drawing. Below this, it draws you."
3. **The Stillwatch** (M2; camp 4 only). Seated at the Edge of the Light, side-on to the fire, watching the far door, which stands off to one side so the figure keeps a clear three-quarter face to the fixed camera (`CampSetRecord` set 4 seat anchor; role-ID row "ambient resident vs implied dream figure"); boots laced, hat in lap. Absent register. Texture: hat-brim turn. ≤ 1 Hear-out beat, never unprompted. **No T1 chain** (lead's call, after the check): T0 and T2 lines only, reachable by Hear out, keyed in M2 on `runHistory` facts (first camp-4 arrival, first win) and excluded from Residents-page completion, so nothing of his is missable; boss-met lines are M3 (`bossMemory`).
   - "Quiet now."
   - "The wall doesn't come closer. You go anyway."
   - M3, after first meeting the Night Governess: "The one with the lamp. She keeps the dark tidy."
   - after the profile's first win: "Someone went through that door. I noticed." (revised twice: no "came back up the stair", and no claim about the dream after a win, RS 19.2)

**Stragglers** (one per theme; camp k hosts floor k's theme; seated, prop set down, face visible; every prop passes the RS 7.4 role-ID and silhouette gates, CH-12)

4. **The Trusty** (Gaol, debt; M2). Grey smock; a pocket calendar he crosses off; no keys (the Turnkey's prop). Pencil texture.
   - "Six days left. Still haven't decided what I did."
   - after a Yield: "You let the lads walk. Time off for good behaviour. Yours, I mean."
   - Jailer of Hours beaten: "The Jailer's stopped. Clocks ran honest for a minute. Nobody knew what to do."
5. **The Whipper-in** (Kennel, duty; M2). Mud to the thigh; a coiled lead with no ring hardware (kept apart from BR's leash-ring mark); no horn or whistle on the model. Texture: lead-leather creak (the tongue-click and the boots withdrawn).
   - "Lost the hounds. Or they lost me. Either way, they're working."
   - Old Ragged beaten: "Ragged's beaten? Someone tell the portrait. Gently."
6. **The Wreath-Carrier** (Wake, grief; M2 with the Wake, else first M3 unlock). Seated, wreath laid flat with a blank ribbon (kept apart from the Hired Wailer). Texture: ribbon smoothing.
   - "Lovely service. Do you know who it was for? I keep meaning to ask." (`toneFlags.selfReference`; counts toward `AmbientSelfRefCap`)
   - Executor of the Will beaten: "Nobody finished reading the will. Most honest thing that's happened in there."
7. **The Off-Shift Singer** (Choir, vow; M2). Towel over wet shoulders; hoarse; no hymnal, no bell. Texture: towel wring (no voice).
   - "My shift's done. Someone else is holding the note. I can hear them not drowning."
   - Drowned Carillon beaten: "The big bell's quiet. They'll be bailing for a week."
8. **The Shelver** (Archive, craft; M2). Ink to the cuffs; blank index cards. Card riffle. Name flagged against the Weaver of the Index, the Index Well and the Index Crossbow (A.2); resolved by the naming pass (renamed from the Indexer, NP-26).
   - "Finished the index. The book isn't finished. Nothing's ever filed under itself."
   - Mouthless Librarian beaten: "She never had a mouth. Never stopped her. You did."
9. **The Showcaller** (Carnival, vanity; M2). Striped coat; a painted placard on a stick, laid down (no tickets: the Carnival's Ticket lock owns them); no cane, no torch, no megaphone. Texture: placard-cloth flap. Name flagged against the system term "bark" (A.2); resolved by the naming pass (renamed from the Barker, NP-26).
   - "Roll up. Roll up. ...That's it. That's the whole act now."
   - Madame Ashes beaten: "Madame took her bow. Nobody clapped. They haven't the hands for it."
10. **The Placecard-Writer** (Supper, hunger; M2 wherever Supper is drawn, which in M2 is F4, so camp 4 only, half asleep; dry at camp 3 from M3, when Supper joins the F3–4 band). Frost on the sleeves; **seated on the floor beside a stack of blank place cards, refusing a chair**; no tray, no ladle. Texture: card set on board.
    - "Every name but mine. The chart is law."
    - Head of the Table beaten: "The chair's still empty. Nobody's touched the soup."
11. **The Subletter** (Gut, debt; M2; absent register; "rent owed", never the Trusty's "time owed"). Slippers, never animated walking; a rolled mattress tied with string (no lid, no clasp: never a container silhouette); no ledger. Texture: strap buckle.
    - "Rent's paid. Nowhere else to go."
    - Pylorus beaten: "The door's shut. It was the front door."

The Heart has no Straggler (no camp after F5). M3/M4 themes each add one before entering the manifest (V20s `LingererPerDrawableTheme`).

**Hub cast**

12. **The Copyist** (M2 static at the Book; M3 at the desk beside the Book, never its interface). Ink-blotted gloves; the ledger thickens in bands. Dry, gently comic, never mocking; never comic on a death Night. Texture: page turn (not a nib scratch, which sits beside the reserved needle-scratch). No death-keyed line in M2.
    - first time on floor 3: "A new chapter. I had to cut fresh pages."
    - unravel: "A page came loose. That's the binding, not you."
    - seeded: "Someone else's handwriting on this one. I'll keep it. It won't move the book."
    - first win (a page caption, not speech; `OneWinOwnerPerNight`): "The whole stair, start to end. I've left the margin wide."
    - road not taken (only if no camp line played it): "You turned from the Carnival. I left that page blank."
13. **The Roll-Reader** (M3; after the first death only). Plain dark coat; hands folded on a closed book (no candle, no flame object); bench creak. Sincere only; never counts, jokes or names the fallen. Hidden with memorials. Owns the hub death reaction, as the Roll's caption.
    - Choir death: "Cold water and a long note. I'll sit with it a while."
    - "I write the names large. The dark reads badly."
    - F5 death: "It was the last door. I'll sit with that." (no comparison to others, AN11)
14. **The Understudy** (M3; after the first boss met). One sleeve of every costume; the mask hangs face-in from a sleeve at the hip, never held, never worn. Texture: silk sleeve rustle. Mocks bosses, never the dead; never impersonates the boss that killed the hero that Night; silent on a death Night unless heard out; final bosses get posture only. Impressions are text plus one generic "performing" pose, with a costume-sleeve swap per theme (8 sleeve states) and 2 final-boss postures (6). Never speaks a Grudge line (4.15.6).
    - as the Precentor: "Crescendo! ...No. They'd never cast me for the high notes."
    - as the Mouthless Librarian: "..." (a long, offended silence; a finger to the lips)
15. **The Gilder** (M3; after the first unlock or win). Gold leaf on the thumbs. Brush stroke. Name flagged against "Master in the Frame", the Frame-Master's Spear and EV's "figure frame" (A.2); resolved by the naming pass (renamed from the Framer, NP-26).
    - theme unlocked: "Another door to paint. The dream's remembered somewhere new."
    - Memento (wall caption): "Carried out of the Heart. I'll frame it plain. It doesn't need help."

**Settled Stragglers (M3; agreed, Q5).** **At most 3 visible**, rotating; a 6-line chain about leaving its stratum. **The rule, computed (lead's call, after the check):** `lingererHeard[themeId]` increments once per Night in which any line of that Straggler was displayed (Overheard, Hear out or the vignette), personal runs only. At each `nightIndex` increment, any Straggler with a count ≥ 3 *(illustr.)* is appended to `settled[]` in themeId order. Each hub visit seats the settled Stragglers with the lowest (`lingererLastOnBalcony`, themeId), excluding the vignette Straggler, 3 of them, or 2 when the vignette Straggler is itself settled, and the hub lore write sets their `lingererLastOnBalcony` to `nightIndex`; a balcony seat counts as seen for rotation whether or not it speaks. Never lost, killed or missable. Rejected: Q5-B, separate casts. **(new lead's call)** The floor-k Straggler vignette seat (below) is separate from the 3 settled seats; if the floor-k Straggler is itself settled, it takes the vignette seat and the balcony seats 2, so no figure appears twice and no more than 3 settled Stragglers are ever visible (corrected after the check: the earlier "its balcony seat passes on" allowed 4).

**Floor-k Straggler at the hub (CH-13; M2; agreed, Q4).** **One trigger predicate (lead's call, after the check):** a personal run, result = death, on floor k ∈ 1..4, with Straggler(theme of floor k) present in the manifest. Otherwise there is no vignette: never after an abandon, a win, an unravel (credited as a death at camp N, but not on a floor), a floor-5 death (the Heart has no Straggler) or an unshipped Straggler (the Wake, conditional in M2). Floor k's Straggler then sits as a vignette on the hub screen that Night; its line is T1 or T2, **dry or sincere, never comic** (4.6; `VignetteNotComic`), never the death reaction, and only after the recap closes. Its T1 line takes the hub's T1 slot only through arbitration (4.15.4). It counts toward `lingererHeard`.

**Overheard exchanges (M3):** Understudy: "How did the Kennel end?" Copyist: "Badly, for the hound." Gilder: "Where does this door go?" Log-Carrier: "Somewhere that thinks it's the bottom. They all do." Banned topics: deaths, the Dreamer, others' motives, the downing verb. Never on a death Night.

**Rejected figures:** nurse, lullaby-singer, knitter, mother, child (imply who sleeps); "a fellow dreamer ahead" (hero motives); a memorial ghost (second fallen-hero device); any former companion; the Night Usher; Heart roster types (Sleepwalker, Insomniac, Storybook Knight); the "Ones Who Stopped" (held; imply other sleepers); the Mender, Pilgrim, Hired Weeper, Tallyman, Porter, Archivist (name collisions, A.2).

**Keeper (service, not the Fireside):** service strings in M2 (UI text, not EV "keeper lines"); the EV-named Strand lines in M3; never a lore chain (CH-14).

### 5.2 Hub surfaces (M3)

| Station | Hosts | Figure (beside a record-only object, never its interface; AN10) |
|---|---|---|
| The Banister | Launcher | — |
| Depth gauge | Tier, Peril, reserved easier lamp, reserved run-modifier slot (Oaths requested, Later; HL-21) | — |
| Book of Nights | History, records, Grave Goods, Atlas, codes | Copyist |
| Library | Codex, Boss Ledger, Grudge, Residents, Milestones, Veteran book | Understudy nearby |
| Memento wall | Mementos | Gilder |
| Unwoken Roll | Roll (EV limits) | Roll-Reader |
| Heirloom shelf | One boon (6 hooks, the wrapped board, the four states and pending offers, M3; HL-9); Lone Road | — |
| The Newel (class coats on newel pegs) and the hero's peg | Classes; the hero's peg | — |
| Letter Rack | Seeded codes; daily Later | — |
| Hub fire | — | Log-Carrier |
| Balcony | — | Settled Stragglers (≤ 3) |

### 5.3 Creative moments

| Moment | Verdict | Wave |
|---|---|---|
| The fire remembers (ribbon post, ribbon, chalk outline) | Adopt | M2 |
| Truthful bedrolls | Adopt | M2 (carved Deed per CMP cut line) |
| Stragglers | Adopt | M2 |
| Stairtop cast | Adopt | M3 (Copyist static M2) |
| Book of Nights | Adopt | M2 screen, M3 desk |
| Begin again fast path | Adopt | M2 |
| Coat peg and the Roll | Adopt | M3 |
| Last Hollow stillness | Adopt | M2 |
| Ones Who Stopped | Hold (premise) | — |
| Seam camps (act-keyed); residue | Adopt | M2; residue M3 |
| Overheard, never checked; no "new dialogue" marker | Adopt | M2 |
| Residents who follow you up | Adopt | M3 |
| Lore advances on Nights, not wins | Adopt | M2 engine, M3 chains |
| Rumour board | Reject | — |
| Guttering fire transition | Adopt | M2 |
| Companion night-watch vignette | Hold | Later |
| Tiny flavour effects | Reject (agreed, Q2) | — |
| Walkable camp / hub | Reject (agreed, Q3) | — |
| Hub Ask topics | Reject | — |
| `ink` chain cosmetic | Reject | — |

### 5.4 Hooks decided

| Hook | Verdict |
|---|---|
| Tithe Bowl | M3, cart, camps 1–4, Sketch only at camp 4 |
| Ferryman's Note | First promotion candidate if `TonicShareOfSpend` > 25% |
| Blood Price | Second candidate; never camp 4 |
| The Long Shot | Later (adds an input to ENM 10.5 step 3) |
| Re-engraving | Later (breaks ITEMS 18.1 / ENM 15.3 "never rerolled"; camp time sink) |
| Lost Property | Later, only if greed gates miss |
| Coin owed | Off |
| Hawker's Hour | Floor event (EV-42) |
| Pledges, Testimony | Later, as Muster cards |
| Training | No service; level and skill picks |
| Camp trade-up | Stays rejected (IR 7) |

---

## 6. Art and audio (lead's call; change CH-12 ⚑)

**Art (the eager team's showcase)** *(illustr. counts)*:
- M2: 4 camp sets (2 act-keyed seam sets, the quiet camp, the Last Hollow); door dressings per theme; trophy props (~14–16, one per non-final boss, via `TrophyRecord`, 4.7); 1 post rig; bedroll states (plain, folded open slot, torn, carved, rolled/carried); 3 regular rigs; 8 Straggler rigs reusing faction materials with seated, prop-down idles (never the type's standing silhouette); generic stand-and-turn clips; the static Stairtop backdrop with the Copyist and Straggler vignette seats.
- M3: the menu-room Stairtop; 4 hub rigs; the Understudy's generic performing pose, 8 costume-sleeve states and 2 final-boss postures; settled-Straggler balcony poses; growth states (coats on newel pegs, painted doors, frames, Book bands, stairwell depth for Peril); one post-death lighting state and one post-win state (never cumulative, 4.10); residue overlays ×8 (4.7); Strand cords.
- Gates (CH-12): RS 7.4 greyscale, silhouette and colour-blind gates on every set; new role-ID rows "ambient resident vs its faction's roster types vs Muster card vs keeper", "ambient resident vs implied dream figure" (the Stillwatch), "hub coat display vs EV Coat Hooks and implied-figure silhouettes", the Heirloom shelf's four states (hung, stored, locked, pending) in greyscale (M3; HL-9), and every Straggler prop against reserved silhouettes and items (containers, the Ticket lock, the leash ring, role props, candles and the reserved lamp); occlusion check (no figure hides a station; no seat inside a station's focus frame or hotspot) (lead's call, after the check).

**Audio by venue:**

| Venue | Bed | States | Notes |
|---|---|---|---|
| Camp 1–3 | Fire crackle + camp music box | Box degrades by filtering, detune and dropped notes only | Box timbre outside reserved families; passes the 85% blind-ID gate against the Heart bed's box, **the Gaol bed's music box (RS 8.3; camp 1 follows a Gaol floor)**, the Lullaby boss, the floor-1 small-hour chime, the Windfall plucked-string row, the Lullaby Glyph wind-up click and the Dream tick; a felt-damped lower comb, away from the Gaol box's register; no wind-up click; never seen wound or played (CH-12; lead's call, after the check) |
| Camp 4 | Low fire + box nearly broken | No breath-family sample in any stem; **no mechanism click, stutter or ratchet noise** in any stem | Idle cap 0 |
| Hub (M2) | 1 bed, 2 stingers (win, death); unravel: silence, then the bed, never the death stinger | — | — |
| Hub (M3) | Hub bed | After win: warmer; after death: the post-death state, cooler and lower, for that visit only, never stacked; after unravel: silence then bed | — |
| Transitions | Floor→camp, camp→floor (walk into dark), recap→hub | — | — |
| Duck rule | Comic ambience bus **to silence** during the death recap, on the death screen and the rest of the death path, and on the unravel card (RC8, agreed); −12 dB *(illustr.)* on Downed arrivals at camp | — | Bus spec, not per line; an audio assertion in `PerVisitCaps` (lead's call, after the check) |

Each figure has one wordless texture in its own family, never shared (the list in AN9); every audio tag passes `ReservedCues`, which lists the footstep and whisper families explicitly for ambient tags. Text only, no VO in M2–M3; VO is a Later client option (voice makes repetition audible; 14.2).

---

## 7. New mechanics (lead's call)

### 7.1 The camp reward card (change CH-15 ⚑; M2)
- **The full entry set per camp (lead's call, after the check).** Reward = pick 1 of {entry 0, entry 1, the Reprieve Phial if this is a `campReward` phial camp}; the others are dreamed (lost). Shop = buy entry 0, entry 1 or both for coin; plus, **when TL 6.11's notables slot drew a Wax Key, that key as a third entry on the shop side only** (bought for coin, never the free reward pick, whatever the `choice`; LE 6.3, ITEMS 10.4: "a third camp display entry outside the budget"). The phial is never on the shop panel; a declined phial is lost for the run (CN 14.7).
- **`ChooseReward(entryIndex)` is one command** that sets `choice = reward` and grants the item atomically, with one WO8 write, so "reward chosen, nothing taken" cannot exist. The shop side is committed by its own explicit command (ITEMS 10.4: explicit and irreversible), one WO8 write, after which entries 0 and 1 are buyable.
- **`choice` unset at Travel on** dreams both entries and loses the phial; the Travel-on confirm lists it, with the phial named (4.2).
- No new draw, no new VP; LE-6 coverage unchanged; no further option (LE 6.12).
- Report `CampChoiceShare` (shop 35–65% *(illustr.)*), split by phial/no-phial camps; price is the knob.

### 7.2 Vendor potion stock (change CH-16 ⚑; M2)
- Stock is keyed to floor N's party count at generation: 2/2/3/3 for parties of 1–4 *(illustr.)*. A hire at this camp never changes it; Lone Road is not over-stocked; declining a recruit is never rewarded.
- **The stock is rules state, trimmed in Core (lead's call, after the check).** `loot/floor/{N}/vendorPotions` always draws `max(table)` entries (a fixed draw count). Core sets the stock to entries 0..count−1, with count = table[`partyCountAtGeneration`] read from floor N's `GenInputs`; the higher entries are discarded in Core, never merely hidden by the display. Buy and `BuyBatch` read that stock, and `OutOfStock` is computed on it. V20t `VendorStockTrimmedInCore`: a Buy of index ≥ count is rejected.
- **ITEMS 10.3's drawn `potionStock` range ([2, 3]) is retired** in favour of the party table, through CH-16's edit to ITEMS 10.3's data example.
- Coverage (LE 6.4 item 7, LE 15): coin on arrival should cover per-body restock; a new report `CampRestockCoverage` checks it at both caps (9.2).

### 7.3 Quick Restock as `BuyBatch` (change CH-18 ⚑; M2)
One compound command, one WO8 write. **Defined (lead's call, after the check):**
- The batch is an **explicit list of (itemId, targetUnit) lines** computed by Core from current state at preview time: first supply up to cap 3; then one ordinary healing potion for each unit holding none, in party order, hero first, while stock lasts (units past the stock are skipped and named in the preview). Placement is belt first, then pack, per ITEMS 6.2.
- If the purse is short, the command is **rejected whole** with `InsufficientCoin`; the preview shows the shortfall.
- The preview is invalidated by any command that changes the party, the purse or the stock (a hire, a dismissal, a Sell, a Buy).
- Cuts camp writes from about 10–16 to about 8–11.

### 7.4 Accept kit defaults (change CH-17 ⚑; M2)
One press issues the ordinary companion default picks (`KitChoicesDefaulted`); shown per companion; **the change link is live only until `CampRest` is issued, then a change is a Rethink (once per companion per camp, CMP 8.2)** (lead's call, after the check); hero manual; hidden on a profile's first 3 runs. V20t `AcceptDefaultsRestEquivalence`: the step-3 restore and step-7 snapshot equal those of a run in which the same picks were made one by one. (Replaces the numbers pass's silent auto-default, which would have taken choices without the player, against ABL 11.3.)

### 7.5 Speculation restart (change CH-19 ⚑; M2)
Restart when the candidate `GenInputs` digest changes after a committed camp command or a non-WO8 change that enters `GenInputs`; cancel or discard the stale job; debounced 0.5 s; a result (an unravel included) stays in Core memory, invisible to display and lore, until Travel on on a digest match (4.4); gutter dressing only above 300 ms.

### 7.6 The Perilous Door at the door station (change CH-20 ⚑; M3)
Each door card at a camp where **Two Doors is on** (RS 9.1: "M3 with full Two Doors") gets a Perilous toggle (behind the Peril-ladder unlock); never a trimmed single random-build card (lead's call, after the check); showing the one-tier-up Rendition kinds and "boss reward: choose 1 of 4" (LE-19). Changeable until Travel on; listed on the confirm; rides the next write; enters GenInputs (speculation restarts). **(request)** RS owner: offered on single-door cards, camp 4 included? Fallback: not offered there (at camp 4 the Last Hollow choice already fills the door).

### 7.7 Floor-k Straggler in the hub (change CH-13 ⚑; M2; agreed, Q4)
See 5.1. It brings the client's cast to the players who never reach a camp.

---

## 8. Numbers (toy, not simulator; all *(illustr.)*)

Toy models, not the simulator; scripts kept in the design lead's scratch area.

- **Camp reach** 97/87/70/49% (EV 0.1); 3.03 camps a run on average. Over 200 runs: average arm ~606 camps; streak arm ~120, all at camp 1; f1only arm 0.
- **Camp seconds (median mandatory, camps 1–4):** new player A 249/285/294/258, C 252/276/297/261; veteran A 100/108/113/97, C 102/123/116/99. Walking adds only +12–20 s per veteran camp; B's "+40–60 s" is the voluntary visit-everyone chore (~50 s).
- **(told plainly)** The numbers pass's catch-up input (1.2 decisions per body per camp) overstates companion decisions (picks come only at L3/6/9), so its "new player at cap 3 reaches ~124 min" overrun is **unverified** until `CampSeconds` is measured.
- **Writes per camp:** ~10–16 (bodies 1→4); with `BuyBatch` ~8–11. Entry-only speculation is kept at ~5% of camps; restart-on-change ~99%.
- **Straggler freshness (camp 1, 2 F1 themes):** pool 15 at 2 lines/visit collapses from visit 13 (0.23); pool 20 at 1 line/visit holds 1.00 to visit 30 and 0.90 after on the streak arm. Hence 1 line per visit and Gaol/Kennel at 20–30 lines. **(lead's call, after the check)** The toy counted camp visits only and is re-run before writing starts with the AN7 Straggler cap (1 unprompted plus 1 unseen Hear-out line), the floor-k vignette's draws after F1–F4 deaths, and camps 2–3, on the average, streak and f1only arms; Gaol and Kennel are sized to its result (likely 30+), and `LoreFreshness` for Stragglers is split into camp and vignette sources.
- **Hub freshness:** M2 Afterword-only holds to ~Night 10–15 on f1only (0.83 at Nights 11–25, 0.50 at 26–40). M3, 3 figures × 10 beats, cap 1: 1.00 through Night 25 on both arms; cap 2 drops to 0.86/0.68. Deepening F1 death keys from 3 to 5 lines adds ~+20 Nights.
- **Chain completion:** every-camp regular (12 beats) ~12 runs average, ~20 streak; hub figure (10 beats, cap 1 shared) ~30 runs; F1 Stragglers settle ~run 6.
- **Writing:** M2 ~250–280 lines, ~3.3k words (Log-Carrier 50, Slate-Sketcher 25, Stillwatch 12, Gaol/Kennel 25 each, Choir, Archive and Carnival ~16 each (two bosses, two camps and the F2/F3 vignette each), Wake, Supper and Gut ~6 each (one camp, absent or conditional), Copyist 30, keeper strings 20; the Straggler rebalance, lead's call after the check, is 66 lines against the earlier 60, inside the range); M3 +~450 lines, ~6k words. Versus EV's 11–12k words in M2. Localisation ~9k words × 6 languages ≈ 54k translated words, low five figures per pass (rates unverified); per-language font budget for the plain-font setting.
- **Pool sizing by key rate:** high-frequency keys (newcomer hired, Downed arrival, F1 Yield) get 8–10 lines each; first-event keys get 1.

---

## 9. Data and validator V20 (lead's call)

### 9.1 Records (all fields display-tagged; in `displayHash`)

```
AmbientRecord { id, nameKey, venueMask[camp1..4|hub], themeId?, waves, rigRef, idleSetRef,
  poseStates[], voiceTextureTag, typographyId, registerCeiling, silhouetteClass:"ambient",
  speakerKind:"hollowfolk" }   // no price, stock, inventory, kit, chassis, recruit, reward, command, effect

LoreLine { id, speaker, venue, mode[overheard|hearOut|caption|exchange], tier[T0..T4],
  criteria[{fact, op, value}], once, cooldownNights, chain?{chainId, stage, minNightGap},
  register[comic|dry|absent|sincere], toneFlags[deathAdjacent, memorial, selfReference, downedAdjacent],
  factKey?, familyId?, ruleTouch[none|restates], publicRuleRef?, textId, variantIds[],
  pronounTokens[], audioTag, maxWords }

loreMemory { version, checksum, nightIndex, seen[lineId]{firstNight}, loreShownCount[lineId],
  loreLastShownNight[lineId], lastFiredNights[factKey] (last 2), chainStage[chainId],
  lingererHeard[themeId], settled[], lingererLastOnBalcony[themeId], deferredT0[],
  hearsay[]{lineId, nightIndex, venue} (bounded),
  oddmentsSeen[] (ordered by last sighting; M2, starting kits design 4.5.2, SK-6) }   // bad checksum reads empty

loreRunState { nightId, visitIndex, spokenFactKeys[], selfRefUsed, t3Played[],
  chainAdvancesThisRun[] }   // in run.save, every run kind; discarded at end of
                                           // seeded runs, replays and simulator runs

VenueSurfaceRegistry { venue -> [{surfaceId, facts[]}] }   // AN3; data, display only

TrophyRecord { bossId, propRef }   // display only; 4.7

CampSetRecord { id, campIndex, actSeam, stationAnchors[], stationFocusFrames[], seatAnchors[],
  seatFaceAngle[], occlusionMask,
  leftCaptionKey }   // per-camp Oddment "left" caption, never an ordinal; also the Book of Nights
                     // caption (M2; starting kits design 4.5.2, SK-6)
```

**(lead's call, after the check)** `nightIndex`, `lastFiredNights`, `lingererLastOnBalcony`, `deferredT0[]`, `hearsay[]`, `firstNight`, `familyId`, `ruleTouch`, `pronounTokens`, `loreRunState`, `VenueSurfaceRegistry`, `TrophyRecord` and the camp-set focus frames and face angles were added so that every rule of 4.10, 4.15, 4.16 and 5.1 is computable. The proposal's `shownCount` and `lastShownNight` are renamed **`loreShownCount`** and **`loreLastShownNight`**, because `shownCount` is the EV seen count that seeded runs **do** write (SV Q1; V19t `SeededRunWritesSeenOnly`), and the same name inside `loreMemory` would make that whitelist ambiguous.

### 9.2 V20 `Hollowfolk` (registered in the consolidated suite by CH-25 ⚑)

- **(s) static:** `AmbientNoTrade`, `AmbientNoHire`, `AmbientNoEffect`, `AmbientCoreBlind` (analyzer), `LoreCarriesNoFact` (`ruleTouch = restates` implies a non-null `publicRuleRef` found in `VenueSurfaceRegistry`; every `{token}` binds to a `LoreContext` fact whose source surface is in that registry; the "no fact" judgement stays a tone-review gate), `LoreCriteriaResolve` (every fact and op typed against `LoreContext`), `NoForecastRead`, `AmbientTrigger` (CH-10 set), `RegisterByVenue` (with death-path venue rows), `DeathNeverComic`, `VignetteNotComic`, `GrudgeDeathOwner` (criteria reading the Grudge or `bossMemory.lastOutcome = killedHero` require `deathAdjacent` and speaker = the death owner), `DreamerLine` (forbidden imagery and tokens, incl. "down"/"go down"/"went down" and "back"/"kept going" after a win), `NoCumulativeCount` (lines and display states), `ReservedCues` (incl. no breath, footstep or whisper family), `AmbientTextureUnique`, `AmbientSelfRefCap` (separate from DV2), `ChainsAcyclicReachable`, `T0KeyReachable`, `PoolsNonEmptyOrSilenceDeclared`, `LingererPerDrawableTheme`, `StationRingNoAmbient`, `AmbientNotServiceAnchor` (no seat inside a service station's focus frame or hotspot; hub figures beside record-only objects only), `AmbientVerbUnique`, `AmbientTextNotTimed`, `UnlockHasHubObject`, `CampSetRecord` checks (station reach, occlusion, seat counts, face angles), `AmbientSilhouette` art tags; name collisions via the existing **V7.NameRegistry**, run with a substring grep as well as whole words.
- **(t) tests:** `LoreInert`, `SeededRunLoreReadOnly` (fully qualified paths; V19t unchanged), `AmbientProfileBlind` (both display roots), `NoLineTwicePerRun`, `AbandonWritesNoLore`, `HubLoreWriteIdempotent`, `OneDeathOwnerPerNight` (≤ 1), `OneWinOwnerPerNight`, `CelebrationNeverOnDeathNight`, `AfterwordNoEcho`, `PerVisitCaps` (incl. the death-Night cap and the duck-to-silence audio assertion), `CameraDriftNoCommand`, `CampProbeResistance` (catch-up chain draws, `CampRest` reload cases and WO8 rows, reusing V19t), `AcceptDefaultsRestEquivalence`, `VendorStockTrimmedInCore`, `SpeculationEquivalence` (with an unravel case), `CampTravelOnFloor` (clean = no forfeited camp-issue potion, `choice` committed, Perilous unchanged, and not camp 4 with a purse at or above the cheapest vendor item; keyboard and mouse 1 input clean and 2 with a confirm, controller 1 hold).
- **(r) reports, split by source (lead's call, after the check):**
  - *Simulator:* `LoreFreshness` (per Night and per visit; streak, f1only and fast-path arms; Stragglers split into camp and vignette sources), `HollowRepeatRate` (≤ 5% within 3 Nights), `KeyExhaustion`, `LoreDiscovery`, run in a **`LoreEmu`** sandbox that accumulates a throwaway `loreMemory` per trial (mirroring `LauncherEmu`, SV-13) and never writes a player profile (SV 4.6); `SpeculationHitRate`; `CampRestockCoverage` (LE 6.4 item 7 targets, both caps); `CampChoiceShare` under a declared bot policy (also from humans). The bot's Hear-out policy is declared per arm.
  - *Human (SV 6.11 sessions until telemetry consent exists, which MAIN owes):* `CampSeconds`, `CampClicks`, `RunWallClock`, `HubSeconds`, `DeathPathSeconds` (with `DeathNightHubLines`), `AmbientTalkRate` (alarm above ~60% on veterans), `QuietCampShare`, `QuickCampShare`, `CampChoiceShare`.
  - *Gates and fallback:* the simulator reports feed the M2 writing gate (pool sizes fixed before writing); the human reports feed the M2 sign-off's camp-time row. If no human data exists at M2 sign-off, the camp-time targets stay *(illustr.)*, the row is marked unmeasured at sign-off (told plainly after the check), no cut is applied without data (the dolly is agreed, Q3), and the row is re-checked when data arrives.

### 9.3 Tooling

A chain-graph and coverage export (the Hades lesson), required before M3 writing.

---

## 10. Staging by wave

- **M1:** no camp, no hub. Death and victory screens, run record, Begin.
- **M2:** diorama camp (4 sets), Arrival (get-up, catch-up, rest summary) / the Hollow / Travel on; Camp List; keeper vendor with `BuyBatch`; CH-15, CH-16, CH-17, CH-19, CH-26; trophies, ribbon, chalk, bedrolls; Log-Carrier, Slate-Sketcher, Stillwatch; the Stragglers (Gaol and Kennel deepest; Wake conditional); Hearsay log; static Stairtop screen with the Copyist and floor-k Straggler vignette (agreed, Q4); Profiles; `loreMemory` with `nightIndex`, the Hearsay record and the hub lore write; `loreRunState` in `run.save`; both `lore` display roots; V20 (s) and (t), registered by CH-25; `LoreEmu`; clock reports (human sessions) and freshness reports (simulator).
- **M3:** menu-room Stairtop (Roll-Reader, Understudy, Gilder, Log-Carrier at the hub fire, settled Stragglers (agreed, Q5), exchanges); the Newel (class coats on newel pegs), the hero's peg and the Roll home; Memento wall; Library with Residents, Milestones, Veteran book; unlock objects and "New in the dream"; boon shelf, Depth gauge, Peril, Lone Road; Perilous toggle; Tithe Bowl; Strand cords; Ear Trumpet row; Atlas page and Lore roster preview; camp-3 doors; residue; boss-met lines; codex plates; the Letter Rack; heirloom unlocks R1–R3, the shelf's hooks, four states and pending offers, Grip-marks (HL-1, HL-5, HL-6 request, HL-9, HL-10).
- **M4:** Stragglers for new themes; species coats; three-door camps; Waking City at camp 4 after a first win; Plain names and the content note (HL-27); first-time curse and Malady lines (HL-28, request).
- **Later:** companion speaker class (AN13); night-watch vignette; VO (Log-Carrier only; client option); daily nights; replays viewer; Pledges and Testimony; Re-engraving, the Long Shot, Lost Property; `bedroll.vacated` with the Mortal rung; Oaths in the run-modifier slot (HL-21, request); R4 and R5 heirloom templates (HL-24).

---

## 11. Ownership and hooks

| Owner | What this design needs from them (regenerated from 13 after the check) |
|---|---|
| RS | CH-1 (RS 6.8 reading); CH-4 drift; CH-5 (RS 0.1, 6.7, 10.1); CH-6 (RS 7.5 addition); CH-10 RC8 trigger class; CH-11 register; CH-12 music box and gate rows; CH-14 keeper bullet; CH-20 (request); CH-24; CH-25 (RS 11.2 suite row); codex hosting of Residents pages |
| CMP | CH-1 reading; CH-5 (CMP 7.4, read); CH-17 Accept kit defaults; CH-21 ambient grammar; CH-23 Lone Road bedrolls; CH-26 D1, Party Forecast and CMP-22 timing |
| DH | CH-1 reading (rest after catch-up; top-up in the rest summary); CH-16 (HE-6) |
| SV | CH-2 (WO4, WO8, the `levelup` key, `CampRest`); CH-3; CH-5 (SV 6.8, SV 6.11); CH-7 (with the main design owner); CH-8 domain and roots; CH-9 seen-mark queue, `loreRunState` and the hub lore write; CH-16 (`vendorPotions`); CH-18 `BuyBatch`; CH-19 speculation; CH-25 (SV 5.2 suite); V19t unchanged |
| ITEMS | CH-15 reward card; CH-16 potion stock and the retired `potionStock` range; CH-18 `BuyBatch`; CH-24 (ITEMS 13, informed) |
| LE | CH-15; CH-16; CH-20 (LE 6.12, confirms) |
| CN | CH-15 (phial) |
| TL | CH-15 (TL 6.11 notables slot, read; informed) |
| EV | CH-12 (EV 6); CH-14 keeper scope and Roll home; Afterword unchanged; Roll-Reader pool and `AfterwordNoEcho` |
| MAIN | CH-6 hub waves; CH-7 `loreMemory`; CH-9 (MAIN 6.6, read); CH-13 M2 hub figures (agreed, Q4); CH-20 (MAIN 4.7, read); CH-24; telemetry consent; the visible easier setting |
| XI / segment 10 | CH-22 Camp List and controller rows; hosts codex pages and hub surfaces; Tithe Bowl |
| TK | CH-24 (TK 24.2, informed) |
| Naming pass | Every placeholder (A.2), incl. "Keeper" vs "keepers of the sleep", "Book of Nights" vs other "Night" names, and the near-collisions flagged in A.2 |

---

## 12. Binding requirements and how they are met

| # | Requirement | Met by |
|---|---|---|
| 1 | The brief: NPCs at camp and hub who cannot be recruited and cannot sell, serving as set dressing and lore | 4.14 AN1–AN2, 5.1, 9.1 schema, V20s `AmbientNoTrade`, `AmbientNoHire`, `AmbientNoEffect`, `AmbientCoreBlind` |
| 2 | The brief: the camp between floors and the hub between runs (everything else left to the lead) | 4.1–4.13, 5.2, 6, 7 |
| 3 | The approved camp order is never reordered; restore follows the catch-up picks | 4.2 (CH-1 reading); `CampProbeResistance` |
| 4 | No ambient record can hold an economy, unit, command or effect field; Core never references ambient types or `loreMemory` | 9.1; `AmbientNoTrade`, `AmbientNoHire`, `AmbientNoEffect`, `AmbientCoreBlind`, `CoreNoProfileRef` |
| 5 | Service and lore never share a body; the keeper never carries a lore chain | 5.1 keeper; CH-14 |
| 6 | Every rest, reveal, draw and spend at camp is written before its outcome frame | 4.4 WO8 list; CH-2, CH-3; `CampProbeResistance` |
| 7 | Talk, camera, drift and lore selection never create a command, tick or seq | 4.1, 4.4; `CameraDriftNoCommand`, `LoreInert` |
| 8 | Seeded runs, replays and the simulator write nothing to `loreMemory` | 4.16; `SeededRunLoreReadOnly`, V19t `SeededRunWritesSeenOnly` unchanged |
| 9 | The ambient self-reference cap never shares DV2 | AN4; `AmbientSelfRefCap` |
| 10 | No figure reads the launcher forecast or a seed; no personal-run code shows during a run | AN12, 4.9; `NoForecastRead`; SV 4.9 |
| 11 | No line or display state counts deaths, runs, wins or streaks; no pity or comparison; no humour near death, downing, memorial or unravel, nor anywhere on a death Night's path | AN5, AN7, AN11, 4.10; `NoCumulativeCount`, `DeathNeverComic`, `VignetteNotComic`, `RegisterByVenue`, `PerVisitCaps` |
| 12 | At most one figure reacts to a death per Night (none in M2, the Roll-Reader in M3); one figure speaks the first win; the Afterword stays on the run-start card and is never echoed | 4.10, 4.15.6; `OneDeathOwnerPerNight`, `OneWinOwnerPerNight`, `AfterwordNoEcho`; EV `AfterwordExhaustion` untouched |
| 13 | Nothing is missable: every heard line is in the stored Hearsay log; chains advance on sight, win or lose; a skipped hub spends no beat; hub marks reach the profile through the hub lore write | 4.15.3–4.15.5, 4.16, 4.10 fast path; `ChainsAcyclicReachable`, `T0KeyReachable`, `HubLoreWriteIdempotent` |
| 14 | No ambient audio uses a reserved cue or a breath-family sample; no figure handles the music box | AN9, AN10, 6; `ReservedCues`; CH-12 |
| 15 | No walking is ever required; every station and figure has a non-mouse path; no timed text | 4.1, 4.13; `StationRingNoAmbient`, `AmbientTextNotTimed`, `NoHoverOnly` |
| 16 | The Unwoken Roll keeps every EV 4.5.10 limit; no figure names the fallen | 4.10, 5.1 Roll-Reader; CH-14 |
| 17 | Camp and hub time are reported, with the cut order applied if medians miss | 4.5; CH-5; `CampSeconds`, `RunWallClock`, `HubSeconds`, `DeathPathSeconds` |
| 18 | MAIN 4.7: unlocks are content, never power; one boon; hub simple first | 4.8, 4.11, 4.17; CH-6, CH-13 (agreed, Q4) |
| 19 | MAIN 6.6, SV 4.6: display outside the reproducibility key | 4.16; CH-7, CH-8; `LoreInert`, `AmbientProfileBlind` |
| 20 | RS 7.3 pillars, RC1–RC9, RS 7.5 premise kept open | AN4, AN5, AN9, AN10; CH-10, CH-11, CH-12; `DreamerLine`; agreed, Q1 |
| 21 | EV Q4: no mercy; streaks never change draws | 4.15.1 *Never*; 4.17 (agreed, Q2) |
| 22 | ITEMS 10.3/10.4, DH 13.2, CN 14.7 camp economy and phial | 7.1–7.3; CH-15, CH-16, CH-18 |
| 23 | Combat is key: camp must not bloat the run | 4.5 targets and cut order; 7.3, 7.4; Quick camp |
| 24 | No line repeats inside a run, and a reload shows the same lines (lead's call, after the check) | 4.16 stored snapshot and `loreRunState`; `NoLineTwicePerRun`, `CampProbeResistance` |
| 25 | No ambient figure sits at, or reads as the operator of, a service station (lead's call, after the check) | AN10, 4.1, 5.2; `AmbientNotServiceAnchor`, `StationRingNoAmbient`; role-ID rows (CH-12) |
| 26 | Every camp draw has a named key and draw order; every camp outcome a defined command (lead's call, after the check) | 4.2, 7.1–7.4; CH-2, CH-15, CH-16, CH-18; `CampProbeResistance`, `VendorStockTrimmedInCore` |

---

## 13. Changes to approved documents

Applied on 2026-11-23, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; as in the LE, TL, EP, IR, LP, EV, BR, XI and SV precedents, the Flag column also marks rows that only *read* such text without changing it, so that every touch of it is visible **(new lead's call:** this adds read flags to CH-5, CH-20 and CH-24, which the proposal's register did not flag**; lead's call, after the check:** flags added to CH-8, CH-19, CH-21, CH-22 and CH-23, and to the sections that CH-2, CH-5, CH-6, CH-9, CH-15 newly touch; CH-25 and CH-26 added, both flagged**)**. **The client's approval of 2026-11-23 covers the flagged rows** (with Q1–Q5 where a row names one). Told-plainly rows were told in 14.1 and, for those found in the check, 14.3, and confirmed by the client on 2026-11-23. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; recorded as readings, approved by the client with this document on 2026-11-23; each owner confirms its own reading):**
- The camp order's steps 3–7 are one rest-summary command shown after the catch-up picks; steps 8–10 are concurrent stations after card A and the Eye's card (CMP, DH, RS owners; CH-1).
- WO4 carries get-up and the first pending catch-up offer per unit; later offers are drawn inside the previous pick's WO8 write; `CampRest` is a logged system command; WO8's "training" means level and skill picks (SV owner; CH-2, CH-3).
- SV 4.4's speculative generation restarts when the candidate `GenInputs` digest changes, and its result stays in Core memory until Travel on (SV owner; CH-19).
- CMP-22's top-up re-check runs at the dismissal rather than at Travel on; the Party Forecast is the door's caption (CMP owner; CH-26, which also narrows D1's confirmation and is therefore a change, not only a reading).
- MAIN 4.7's "simple camp screen" may hold one static figure and the floor-k Straggler vignette (main design owner; CH-13; agreed, Q4).
- RS 6.8's "camp keeper ... lines" in M2 are service strings, not EV keeper lines (RS, EV owners; CH-14).
- CMP 7.5's Lone Road shows no open-slot bedrolls (CMP owner; CH-23).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| CH-1 | **CMP 7.11 camp order (lead's call, settled)**; **DH 13.1 (decided; order lead's call)**; RS 6.8 step table | Reading: Arrival (get-up), catch-up, then one rest summary for steps 3–7; steps 8–10 concurrent stations after card A and the Eye; `choice` state unchanged; order unchanged (4.2) | ⚑ **Decided** (DH 13.1, read); ⚑ settled order kept | Lead's call; CMP, DH, RS owners confirm | M2 |
| CH-2 | **SV 4.8.3 WO4, WO8 and the outcome rule (agreed, SV Q4)**; SV 4.3 master stream table (settled) | WO4 carries get-up and **only the first pending catch-up offer per unit**; each catch-up pick resolves its level and draws the next level's offer inside its own WO8 write, from the new key `levelup/camp/{k}/unit/{unitId}/level/{L}` with a fixed draw count (companion 3/6/9 choices draw nothing); `CampRest` is a Core-issued system command, logged in `run.log` with its seq, with its own WO8 write; reload re-enters catch-up or re-issues `CampRest` (4.2, 4.4) (extended after the check) | ⚑ **Agreed** (SV Q4 outcome rule, read and extended); ⚑ Settled (SV 4.3, a new key) | Lead's call; SV owner applies | M2 |
| CH-3 | **SV 4.8.3 WO8 (agreed, SV Q4)** | WO8 list limited to draw/spend/reveal commands (4.4); "training" = level and skill picks; re-picks and swaps ride the next write | ⚑ **Agreed** (SV Q4) | Lead's call; told plainly; SV owner applies | M2 |
| CH-4 | RS 6.8 dressing bullet ("Camps 1 and 3 ... camp 2 carries a bigger act-change moment") | Replace with: "Camps 1 and 3 carry the act-change moment; camp 2, inside Midwater, is the quiet camp; camp 4 is the Last Hollow (Deep → Heart)." Evidence: RS 6.2 act table (4.6) | ⚑ Settled (corrected) | Lead's call; told plainly (F1); RS owner applies | M2 |
| CH-5 | **RS 0.1 budget (agreed, read)**; RS 6.7, RS 10.1; **CMP 7.4 floor-time gate (read)**; SV 6.8; SV 6.11 human-session plan (settled) | "RS budgets about 12–16 min of camps; nothing measures it": add `CampSeconds`, `CampClicks`, `RunWallClock` (new-player arm), `HubSeconds`, `DeathPathSeconds` as reports, sourced from SV 6.11 human sessions until telemetry consent exists; camp 1 and the first hub visit join the SV 6.11 plan; the cut order (4.5, 4.12, 9.2) | ⚑ **Agreed** (RS 0.1, CMP 7.4, read); ⚑ Settled (SV 6.11) — read flags added (new lead's call; SV 6.11 after the check) | Lead's call; told plainly; RS, CMP, SV owners apply | M2 |
| CH-6 | **MAIN 4.7 Hub bullet (decided)**; **RS 7.5 macro premise table, "Camps?" and "Unlocks?" rows (agreed, RS Q3)** | Reading: M2 simple screen; M3 non-walkable menu-room after M2 sign-off, holding the 5.2 stations and up to 3 settled Stragglers (4.8). RS 7.5: an addition, one hollow (the Stairtop) does not re-form (added after the check) | ⚑ **Decided**; ⚑ **Agreed** (RS 7.5, an addition) | **Client (Q1, Q5, with this document)**; main design owner and RS owner apply | M2/M3 |
| CH-7 | **MAIN 6.6 profile (decided)** | Add `loreMemory` (display only; outside the key; checksum; bad checksum reads empty), with `nightIndex`, `loreShownCount`, `loreLastShownNight`, `lastFiredNights`, `lingererLastOnBalcony`, `deferredT0[]` and the bounded `hearsay[]` (9.1, 4.16) (fields extended after the check) | ⚑ **Decided** | Lead's call; main design and SV owners apply | M2 |
| CH-8 | **SV 4.3 display row, SV 4.6 (agreed)** | Display domain `lore` with two roots: camp `DisplayKey(run)` / `lore` / (nightId, campIndex, visitIndex, speaker); hub a profile-scoped root H(profileId, `lore`, nightIndex, visitIndex, speaker) created outside Core and never derived from a run key; an "ambient lines" display-content row naming both; `DisplayMemoryInert` covers `loreMemory`; `AmbientProfileBlind` covers both roots; V19 Conformance lists it (4.16) (roots added after the check) | ⚑ **Agreed** (SV 4.6, read and extended) — flag added after the check | Lead's call; SV owner applies | M2 |
| CH-9 | **SV 4.8.3 `pendingProfile` and write orders (agreed, SV Q1 read, SV Q4)**; **MAIN 6.6 (decided, read)** | Add a `loreSeen` kind to `run.save`'s `pendingProfile`, the stored run-start `loreMemory` snapshot and the `loreRunState` record (every run kind; display-tagged; flushed for personal runs only, at WO6, WO7, abandon; discarded for seeded runs, replays and the simulator); add the **hub lore write**: on leaving the hub, `profile.save` is rewritten by atomic replace with the hub's lore marks, idempotent by (nightIndex, visitIndex), personal Nights only (4.4, 4.16) (extended after the check) | ⚑ **Agreed** (SV Q1 seeded writes untouched; SV Q4 write orders extended); ⚑ **Decided** (MAIN 6.6, read) | Lead's call; SV owner applies | M2 |
| CH-10 | **RS 7.4 RC8 (agreed, RS Q3)**; V7 | Ambient trigger class outside combat (arrival end, idle, Hear out, hub entry after recap, hub approach); RC8 in combat unchanged; future speaker kind "companion" named (4.15.2, AN13) | ⚑ **Agreed** | Lead's call; RS owner applies | M2 |
| CH-11 | **RS 7.3 pillar 4 (agreed)**; RS 7.7 Tone Sheets | Camp k uses the drier of floors k and k+1; hub comic except death, memorial and unravel lines and every line on a death Night (dry or sincere only; added after the check); Tone Sheets gain Straggler sets (4.6, AN5) | ⚑ **Agreed** | Lead's call; RS owner applies | M2 |
| CH-12 | RS 6.8 lullaby bullet; **RS 7.4 RC7 and gates (agreed)**; RS 8.3 Gaol sound set (read); EV 6 | Camp box timbre and handling rule; blind-ID gate against the Heart box, the Gaol bed's box, the Lullaby boss, the small-hour chime, the Windfall row, the wind-up click and the Dream tick; degradation by filtering, detune and dropped notes only; no breath family and no mechanism click in camp-4 stems; role-ID, greyscale and colour-blind rows for camp and hub sets, the Stillwatch, the hub coat display and every Straggler prop (6, AN9, AN10) (extended after the check) | ⚑ **Agreed** (RC7, gates) | Lead's call; RS owner, audio and art apply | M2 |
| CH-13 | **MAIN 4.7 (decided, "simple camp screen")** | M2 hub screen holds the static Copyist and the floor-k Straggler vignette (4.8, 5.1, 7.7) | ⚑ **Decided** | **Client (Q4, with this document)**; main design owner applies | M2 |
| CH-14 | **EV 4.5.10 M3 bullet (agreed, EV Q1)**; RS 6.8 keeper bullet | Keeper: M2 service strings (not keeper lines), M3 Strand lines only; the Roll gains a hub home with the Roll-Reader (all limits kept); Roll-Reader pool with `AfterwordNoEcho`; Afterword unchanged (4.10, 4.15.6) | ⚑ **Agreed** | Lead's call; told plainly; EV and RS owners apply | M3 |
| CH-15 | **ITEMS 10.4 (decided via MAIN 4.1)**, ITEMS 24; LE 6.3, 6.4, 6.13; **CN 14.7 (agreed, CN Q2)**; TL 6.11 notables slot (settled, read) | Camp reward card content (7.1): reward pick 1 of {entry 0, entry 1, phial?}; `ChooseReward` one atomic command; a notables-slot Wax Key is shop-only; `choice` unset at Travel on dreams both entries and loses the phial; phial never in the shop (extended after the check) | ⚑ **Decided** (shop or reward); ⚑ **Agreed** (phial); ⚑ Settled (TL 6.11, read) | Lead's call; ITEMS, LE, CN owners apply | M2 |
| CH-16 | **ITEMS 10.3 (agreed)** and its data example; SV 4.3 `vendorPotions`; **LE 6.8 (agreed, LE Q4)**; DH HE-6 | Potion stock by party at floor-N generation; `vendorPotions` draws `max(table)` entries; Core trims the stock to the party count, discarding the rest; the ITEMS `potionStock` range draw ([2, 3]) is retired (7.2) (extended after the check) | ⚑ **Agreed** | Lead's call; ITEMS, LE, SV, DH owners apply | M2 |
| CH-17 | CMP 7.11 step 2, CMP 8.2 (settled; `KitChoicesDefaulted`, Rethink); ABL 11.3 (read) | "Accept kit defaults" press; its change link is live only until `CampRest`, then changes are Rethinks (4.2, 7.4) (narrowed after the check) | ⚑ Settled | Lead's call; CMP owner applies | M2 |
| CH-18 | **SV 4.8.3 (agreed, SV Q4)**; ITEMS 10.3; ITEMS 6.2 placement (read) | `BuyBatch` compound command: an explicit Core-computed line list (supply to cap, then one potion per unit holding none, hero first, while stock lasts; belt then pack), rejected whole on `InsufficientCoin`, invalidated by any party, purse or stock change, one WO8 write (7.3) (defined after the check) | ⚑ **Agreed** | Lead's call; SV and ITEMS owners apply | M2 |
| CH-19 | **SV 4.4 (agreed)** | Reading: speculation recomputes the candidate `GenInputs` digest after each committed camp command and each non-WO8 change entering `GenInputs`; a stale job is cancelled or discarded; debounced; a result (an unravel included) stays in Core memory until Travel on; gutter above 300 ms (4.4, 7.5) | ⚑ **Agreed** (SV 4.4, a reading) — flag added after the check | Lead's call; SV owner confirms | M2 |
| CH-20 | **MAIN 4.7 Perilous Door (decided, read)**; RS 9.1; **LE 6.12 (agreed, LE Q5, read)** | Perilous toggle at the door station, on Two Doors cards only (7.6); single-door cards and camp 4 by request | ⚑ **Decided**, **Agreed** (read) — read flags added (new lead's call) | Lead's call; RS owner (request), LE owner confirms | M3 |
| CH-21 | CMP 8.7 (settled) | Ambient grammar: no kit silhouettes; Veterans as portraits only; no Usher, Pledge source or former companion as ambient (AN10) | ⚑ Settled — flag added after the check | Lead's call; CMP owner applies | M2 |
| CH-22 | XI 4.10 (settled) | Camp List, Hear-out sub-list, hold captions, plain-font setting, controller Travel on as a dedicated hold reachable from any station, the hold being the confirm (4.1, 4.2, 4.13) | ⚑ Settled — flag added after the check | **Client (Q3, with this document)**; XI owner applies | M2 |
| CH-23 | CMP 7.5 (settled) | Lone Road camp shows no open-slot bedrolls (4.7) | ⚑ Settled — flag added after the check | Lead's call; CMP owner confirms | M3 |
| CH-24 | **MAIN 4.7 one boon (decided, read)**; TK 24.2; ITEMS 13 | Told plainly: no starting-kit peg (kit variants only as boon options); charms get no hub surface; meta-curses become one reserved run-modifier slot on the Depth gauge, owned by the MAIN/RS difficulty decision (4.9, 2.2) | ⚑ **Decided** (read) — read flag added (new lead's call) | Lead's call; told plainly; MAIN and RS owners | M3/Later |
| CH-25 | **RS 11.2 validator suite table (settled)**; **SV 5.2 consolidated suite and build dependency graph (settled)** | RS 11.2 gains a V20 `Hollowfolk` row (s/t/r split, M2; this design 9.2); SV 5.2's consolidated suite becomes V1–V20; the dependency graph places V20 as static and display-only, with no g phase; V19s registry completeness lists it (added after the check) | ⚑ Settled | Lead's call, after the check; RS and SV owners apply | M2 |
| CH-26 | **CMP 7.5 D1 (settled)**; **CMP 7.11 Party Forecast (settled)**; **CMP 7.6 top-up re-check, CMP-22 (settled)** | D1's one-line confirmation showing the forecast becomes the lit door's always-shown caption (shown before the click, never modal); the Travel-on confirm is narrowed to losses of something owned or owed (camp-4 coin at or above the cheapest vendor item, `choice` unset, a Perilous choice); the CMP-22 re-check runs inside the dismissal command, with any forfeit named on the dismissal's confirm (4.2) (added after the check). Fallback if refused: D1's confirmation whenever a board with undeclined cards is declined, with the loss lines joined to it | ⚑ Settled | Lead's call, after the check; CMP owner applies | M2 |

The options the client rejected (Q1-B and C, Q2-B, Q3-B and C, Q4-B, Q5-B) are not register rows.

---

## 14. Open items for the client

None. Approved by the client on 2026-11-23.

The client approved this document as a whole on 2026-11-23, including the lead's answers of 0.1 and Composite H (which the client had accepted by proceeding); every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the 41 lead's calls after the check in 14.3); the told-plainly items (those in 14.1 and "Told plainly after the check" T1–T13 in 14.3, all as written); and the readings recorded in 13; and its approval covers the flagged rows of 13. The five questions raised before writing were answered on 2026-11-23 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. Writing raised **no new client question**, and neither did the design check: every new lead's call in 14.1 and every lead's call after the check in 14.3 sits inside the five answers, keeps an approved rule, or is flagged against the text it touches in 13. The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them (including the visible easier setting, voice acting for the Log-Carrier, the owner-refusal fallbacks and the report-triggered items), and the naming pass remain future items.

### 14.1 This document

**The five answers (agreed, 2026-11-23), each the recommended option:**
- **Q1, hub fiction (CH-6):** option A, the Stairtop, a dream-side hollow that does not re-form; commits no premise. Rejected: B, a waking-world house; C, menu only.
- **Q2, ambient effects (4.17):** option A, no mechanical or informational effect at all; cosmetics (codex pages, hub props) and restatements of public rules already on screen only. Rejected: B, cosmetics plus one tiny effect.
- **Q3, camp form (CH-22):** option A, the diorama with a 0.4 s station dolly *(illustr.)*, no walking, the Camp List for keyboard and controller. Rejected: B, a walkable camp; C, a plain menu.
- **Q4, hub figures in M2 (CH-13):** option A, yes, minimal: the static Copyist at the Book of Nights and the floor-k Straggler vignette on the M2 simple camp screen (a reading of decided MAIN 4.7). Rejected: B, no hub figure until M3.
- **Q5, settled Stragglers (CH-6):** option A, yes, at most 3 visible in the hub, rotating. Rejected: B, separate casts.

**The lead's answers (0.1) and Composite H (3.2)** were accepted by the client by proceeding and confirmed by the client on 2026-11-23, with this document, including every told-plainly item below.

**Told plainly (confirmed by the client on 2026-11-23, with this document):**
1. "Empty bedrolls for the dead" cannot happen inside a run; camp shows only true signals and mourning lives in the hub (4.7).
2. No tiny flavour effects, not even a hint (4.17; agreed, Q2).
3. Camp time is budgeted (~12–16 min) but unmeasured; the drafts' "camps excluded" claim was wrong; the numbers pass's new-player overrun is unverified (4.5, 8).
4. The streak guarantee holds through about Night 25 in M3, per hub visit; after that it depends on later content; the beat cap stays at 1 (4.15.7).
5. The drafts' move of the Afterword into the hub is withdrawn; it stays on the run-start card (4.15.6).
6. The `ink` chain reward is dropped; Ask topics are rejected (4.15.3, 4.15.8).
7. The verb "Listen" is renamed "Hear out" (collision with the floor Listen action).
8. "Go down again" is renamed "Begin again" ("goes down" is the downing verb).
9. Starting kits get no hub peg; charms no hub surface; curses no trunk (CH-24).
10. Seeded runs repeat ambient lines (they write no lore memory), to keep SV Q1 unchanged (4.16).
11. A walkable camp and hub are rejected despite the art appeal (agreed, Q3).
12. Several names in the drafts collided with roster types and items (A.2).
13. RS 6.8's camp-2 act-change bullet contradicts RS 6.2 (F1; CH-4).
14. The research is partly from recall and must be checked before any claim is quoted (1).

**New lead's calls (made while writing; confirmed by the client on 2026-11-23, with this document):**
1. **The Letter Rack.** The proposal's hub station "Sealed Letters" is renamed **the Letter Rack**, because EV's Strand name "Sealed Letter" (EV 5.3 card D1, KC13) collides (4.8, 5.2). "Letter rack" has 0 hits.
2. **Near-collisions kept and flagged to the naming pass** (A.2): the camp phase **Arrival** shares its word with SV 6.2's simulator axis `Arrival` (a prose label, never an identifier); **Stairtop** sits next to FL's rules term **Stairhead** (renamed the Downstair, naming pass NP-26; Stairtop kept); the **coat rack and peg** (the coat rack renamed the Newel, naming pass NP-26) sit next to EV's Curiosity "Coat Hooks by the Door"; the "memento wall" keeps LE 6.11's existing lower-case term, now named the **Memento wall** as a station.
3. **Settled seats versus the vignette seat** (5.1): the floor-k Straggler vignette seat is separate from the 3 settled seats; a settled floor-k Straggler moves to the vignette seat and the balcony then seats 2, so Q5's "at most 3 visible" holds and no figure appears twice (corrected after the check).
4. **The M2 death path** (4.10): the floor-k vignette line counts as M2's only ambient line on the death path, plays only after the recap closes and is never death-keyed, so "in M2 none" spoken sincere beat holds alongside CH-13.
5. **The first-focus tooltip** "Has nothing to sell. Has things to say." becomes a rule (4.12), once per profile in `uiMemory`.
6. **Read flags** added to CH-5, CH-20 and CH-24 (13), and the Flag and Approval columns given in the XI and SV shape (extended after the check to CH-8, CH-19, CH-21, CH-22, CH-23 and the new rows CH-25, CH-26; 14.3 item 39).
7. **0.4 Intent, goals and anti-goals** added in the template's shape; **12** expanded into a requirement-to-section table in the template's shape (items 1–2 and 18–23 added; items 3–17 are the proposal's fifteen).
8. **Citation tidy-ups:** Old Friend cites CMP 7.2, the Veteran record CMP 8.7, and Wayfarer's Belt CMP 12 (its M3 staging row, its only home in CMP; corrected after the check, which found the earlier "CMP 7.2/8.7" for Wayfarer's Belt wrong); DV2 is cited to EV 4.5's anti-repetition table; Camp 1 and the first hub visit join SV **6.11**'s human-session plan.

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a lead's call (confirmed by the client on 2026-11-23, with this document) unless marked.

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| Visible easier setting | MAIN/RS difficulty decision (EV Q4 points to it) | A lamp is reserved below tier 0 on the Depth gauge; no figure substitutes for it; the lamp stays unlit until decided | Client (raised, not decided) |
| Voice acting for the Log-Carrier | Later wave review | Text only until then; VO limited to the Log-Carrier if taken | Client (raised, not decided; Later) |
| Camp time over budget | `CampSeconds` or `RunWallClock` medians miss (4.5) | The cut order, steps 1–5 | Lead |
| Death path too long | `DeathPathSeconds` median > 20 s *(illustr.)* on the streak arm, or `DeathNightHubLines` above 1 | Drop the coat-peg beat to a still; the Roll-Reader caption (or the vignette line) stays the only spoken beat | Lead |
| Ambient becomes a chore | `AmbientTalkRate` > ~60% on veterans | Reduce unprompted caps; check for any leaked value; No idle chatter default reviewed | Lead |
| Repetition | `HollowRepeatRate` > 5% within 3 Nights, or `KeyExhaustion` | Deepen the drained keys' pools first; never raise the beat cap | Lead |
| Shop or reward imbalance | `CampChoiceShare` outside 35–65% *(illustr.)* | Price is the knob; no fourth option | Lead; LE owner |
| Tonic share high | `TonicShareOfSpend` > 25% | Ferryman's Note promoted first, then Blood Price (never camp 4) | Lead; LE owner |
| Perilous on single-door cards and camp 4 | RS owner rules on the request (7.6) | Fallback: offered only on Two Doors cards; not at camp 4 | RS owner |
| Travel-on confirmation narrowed | CMP owner refuses CH-26 | D1's one-line confirmation whenever a board with undeclined cards is declined, with the loss lines joined to it; the Party Forecast caption and the dismissal-time re-check stay if accepted separately | Lead; CMP owner |
| Hub lore write or `loreRunState` | SV owner refuses part of CH-9 | Hub marks ride the next run's WO1 through `pendingProfile` (a crash in the hub repeats that visit's lines); without `loreRunState`, camps show only lines unseen in the stored snapshot that no earlier camp this run has queued, read from `pendingProfile` | Lead; SV owner |
| No human camp-time data at M2 sign-off | SV 6.11 hours short | The camp-time row is marked unmeasured; no cut applied without data | Lead |
| Wax Key guard on the Tithe Bowl | LE owner rules on XI's request (LE 8.1) | Until then, the Bowl is greyed with its reason while a Wax Key is held | LE owner |
| Owner refuses a reading (CH-1, CH-2, CH-3, CH-19) | Owner ruling | The approved order and writes stand as written; the presentation adapts (rest summary shown as separate lines) | Lead |
| Premise answered | RS 19.2 decided | Lines re-checked against AN4; the Stairtop fiction stays | Client; RS owner |
| New themes | M3/M4 manifest | One Straggler per drawable theme before entry (`LingererPerDrawableTheme`) | Writing |
| Naming pass | Scheduled | Every placeholder, incl. the A.2 flags | Naming pass |

### 14.3 After the design check

The design check (builder, consistency, player and art-narrative lenses) found two blockers in the camp's draw and save rules and one in the lore state, and a set of gaps in which this design broke its own charter. Every fix below sits inside the five agreed answers and the proposal the client confirmed; **none overrides a client decision**, and the check raised **no client question**. The figures, venues, waves and numbers the client confirmed stand; where a confirmed *sample line*, prop or texture broke a binding rule of this document, the sample was replaced and the figure kept.

**Lead's calls after the check (confirmed by the client on 2026-11-23, with this document):**
1. **The camp order regrouped** under the three confirmed headings: Arrival (A.1 get-up, A.2 catch-up, A.3 rest summary), the Hollow, Travel on; same content and order (4.2).
2. **Catch-up draws:** WO4 carries only the first pending offer per unit; each pick draws the next level's offer inside its own WO8 write from `levelup/camp/{k}/unit/{unitId}/level/{L}` (fixed draw count); companion 3/6/9 choices draw nothing (4.2; CH-2).
3. **`CampRest`**, a Core-issued system command logged in `run.log`, issues the rest when no pick is pending; reload re-enters catch-up or re-issues it (4.2; CH-2).
4. **Accept kit defaults:** the change link lives until `CampRest`; after it, changes are Rethinks (4.2, 7.4; CH-17).
5. **The top-up re-check runs at the dismissal**, its forfeit named on the dismissal's confirm; the Travel-on sequence is stated exactly (4.2; CH-26).
6. **Travel on:** the Party Forecast is the lit door's caption; unbought stock and an open slot are passive door-card lines; the confirm lists only losses of something owned or owed (camp-4 coin at or above the cheapest vendor item, `choice` unset, Perilous); on controller a dedicated hold from any station is the confirm; `CampTravelOnFloor` defines "clean" (4.2, 9.2; CH-22, CH-26).
7. **Quit at camp** keeps only the last WO4 or WO8 write; the proposal's "WO7: quitting rewrites `run.save`" is corrected to WO7 = Load (4.4).
8. **Speculation:** the digest is recomputed after every committed camp command and every non-WO8 `GenInputs` change; stale jobs are cancelled; results, unravels included, stay in Core memory until Travel on (4.4, 7.5; CH-19).
9. **The reward card's full entry set**, `ChooseReward` as one atomic command, the shop commit as an explicit command, the notables-slot Wax Key shop-only, and `choice` unset at Travel on dreaming both entries and the phial (7.1; CH-15).
10. **Potion stock trimmed in Core** from a fixed max-count draw; ITEMS' `potionStock` range retired (7.2; CH-16).
11. **`BuyBatch` defined** as an explicit line list, rejected whole when the purse is short, invalidated by party, purse or stock changes (7.3; CH-18).
12. **Lore selection inputs:** a run-start snapshot stored in `run.save`, plus the run's seen queue and `loreRunState`, for every run kind; nothing repeats inside a run (4.16; CH-9).
13. **`nightIndex`**, when it increments, `visitIndex`, `lastFiredNights`, and the renames `loreShownCount` and `loreLastShownNight` (4.15.5, 9.1; CH-7).
14. **Two display roots:** camp from `DisplayKey(run)`, hub from a profile-scoped root (4.16; CH-8).
15. **Cross-figure arbitration** with a fixed trigger order, one ranking across figures and declared fact-key owners (4.15.4).
16. **Death ownership is "at most one"** (none in M2); the `AfterwordNoEcho` window and `familyId`; Grudge lines belong to the Roll-Reader only (4.15.6).
17. **The hub lore write**, the stored Hearsay log, and the hub's state after a seeded run (4.10, 4.16; CH-9).
18. **The settled-Straggler rule computed** (5.1).
19. **Reports split by source:** simulator reports in a `LoreEmu` sandbox; human reports from SV 6.11 sessions; no cut applied without data (9.2).
20. **`LoreCarriesNoFact` made computable** with `ruleTouch` and a `VenueSurfaceRegistry` (AN3, 9.2).
21. **One vignette trigger:** personal run, death on floor 1–4, Straggler shipped; never after abandon, win, unravel or a floor-5 death; an abandon before camp 1 writes no lore (5.1, 4.10).
22. **Begin while a run is saved** becomes Continue plus "Abandon and begin" (4.8).
23. **Death Nights:** every line on the death path is dry or sincere; the hub's unprompted cap is 1; comic figures speak only on Hear out (4.6, 4.10, AN5, AN7).
24. **AN10 rescoped to service stations;** the Log-Carrier feeds the fire from its far side; the Slate-Sketcher draws on a slate at the edge; the Stillwatch keeps a visible face; hub figures sit beside record-only objects, never as their interface (AN10, 4.1, 5.1, 5.2).
25. **The first win:** one speaker (`OneWinOwnerPerNight`); deferred past a skipped hub and suppressed on a death Night; the peg holds the Memento's ribbon on a win and a loose thread after an unravel (4.10).
26. **The post-death lighting state** is one state for that visit, never stacked; Peril uses stairwell depth (4.10, 4.11, 6).
27. **Onboarding per wave;** the orientation becomes 3 station captions at the first non-death or third hub visit; the Log-Carrier's three beats spread over camps 1–3 of run 1 (4.12).
28. **"Begin again"** sits on the run record screen after the recap and "New in the dream", and carries last Night's launcher choices forward (4.10).
29. **The Stillwatch has no T1 chain** (5.1, 4.15.5).
30. **Straggler pacing** is 1 unprompted plus 1 unseen Hear-out line per visit; the freshness toy is re-run with vignette draws before writing; F2–3 Straggler pools rise to ~16 and Wake, Supper and Gut fall to ~6 (AN7, 8).
31. **Sample lines, props and textures revised** where they broke AN4, AN5, AN9, AN10 or AN11; one texture family per figure (5.1, AN9, 6).
32. **Audio:** the comic bus ducks to silence on the death path (RC8); an M2 unravel never plays the death stinger; the camp box is gated against the Gaol box and the other neighbours, with no mechanism noise at camp 4 (6; CH-12).
33. **Camp chalk is pictorial**, never XI's chalk grammar (4.7).
34. **The Newel** hangs class mantles on pegs in the stair's newel post, never a rail or a hall (4.8).
35. **`TrophyRecord`** per boss for the ribbon post; residue authored for all 8 themes, on cloaks and packs only (4.7, 9.1).
36. **The camp-4 Grudge door caption is dropped** (4.6).
37. **The Perilous toggle** appears only on Two Doors cards; single-door cards and camp 4 go to the RS request (7.6; CH-20).
38. **Hooks placed:** TL 6.11 notables slot, LE 6.4 coverage (`CampRestockCoverage`), EV Tithe Bowl dedup, the LE 8.1 Wax Key guard; DH's top-up and Stratum Card hook split (2.2).
39. **Register and bookkeeping:** CH-25 registers V20; CH-26 added; flags added to CH-8, CH-19, CH-21, CH-22, CH-23 and the new sections of CH-2, CH-5, CH-6, CH-9, CH-15; 11 and 17 regenerated from 13; records exclude version-crossed runs; the LE 6.15 threshold restored; the Wayfarer's Belt citation corrected.
40. **Names:** the near-collisions Lingerer/Linger(ing), Kindler/Kindle and Rekindle, Indexer/Index, Barker/bark, Sitter/Sit with, Framer/Frame, Chalkwright/Chalked and the camp phase "the Hollow"/"the Hollow ahead" and "Last Hollow" are flagged to the naming pass (A.2). The placeholder names the client confirmed are not renamed here. (Renamed by the naming pass, NP-26: Lingerer → Straggler, Kindler → Log-Carrier, Indexer → Shelver, Barker → Showcaller, Sitter → Roll-Reader, Framer → Gilder, Chalkwright → Slate-Sketcher, "the Hollow ahead" → "the next camp"; the Hollow and Last Hollow kept.)
41. **Small fixes:** camp 3's "fewer figures" dropped (the cast is the same); the Placecard-Writer's "dry at camp 3" marked M3; the Understudy's impressions given art counts (4.6, 5.1, 6).

**Told plainly after the check** (none overrides a client answer; confirmed by the client on 2026-11-23, all as written):
- **T1.** The proposal left several builder points undefined: how later catch-up offers are drawn, what issues the rest, how the reward is taken, how potion stock is trimmed, what `BuyBatch` buys, how lore remembers within a run, how Nights are counted and how hub lines are saved. All are defined now; none changes a client answer.
- **T2.** **Quitting at camp loses changes made since the last write.** SV has no quit-time write, and the proposal described one that does not exist. Equips, hand-overs, swaps, Rethink, the door choice and the Perilous toggle made after the last draw, spend or reveal are lost if the player quits. A reload shows the camp as it was at that write.
- **T3.** **CH-26 narrows a settled CMP rule.** D1's confirmation on every declined board becomes a caption on the door, and the modal confirm is kept for real losses only. Without this change the confirm would appear at almost every camp. The CMP owner may refuse, and the fallback is stated (14.2).
- **T4.** **Accept kit defaults is no longer free to change after the rest.** After the rest summary, a change is a Rethink.
- **T5.** (Figure names as at the check; renamed by the naming pass, NP-26, as in 14.3 item 40.) **Several confirmed sample lines broke this design's own rules and were rewritten:** the Kindler's "The fire doesn't charge", "Bring her in close" and "Want less for a minute"; the Stillwatch's "the dream kept going"; the Copyist's "Down and back"; the exchange's "They all go down. That's the joke."; the Whipper-in's "Ragged's down?"; the Sitter's "further than most"; the Head of the Table's "always going to be"; and the Understudy's Grudge line. Some props and textures were also changed, including the Subletter's trunk, the Barker's tickets, the Whipper-in's collar, the Sitter's candle, the Understudy's held mask, the boots, the whispers and the nib scratch. The figures themselves stand.
- **T6.** **After a win, the hero's peg holds the Memento's ribbon, not the returned coat.** "The coat returns" read as the hero coming back, which commits the open premise. The coat rack (now the Newel) is also re-dressed so that it never reads as the Dreamer's hallway.
- **T7.** **The charter's "never at a station" was broken by this design's own hub cast**, which places the Copyist at the Book and the Roll-Reader at the Roll. The rule now means service stations. In the hub, figures sit beside record-only objects.
- **T8.** **The first-hub-after-death rule as written could only be built in M3.** In M2 that screen shows no sincere line, only the vignette.
- **T9.** **The Stillwatch has no chain.** Its lines are optional Hear-out lines and count toward no completion.
- **T10.** **The camp-4 Grudge caption is dropped.** The Grudge stays on the boss plate.
- **T11.** **Human camp-time data may not exist at M2 sign-off.** If it does not, the row is marked unmeasured, and nothing agreed, such as the dolly, is cut without data.
- **T12.** **The lore machinery is more than a text feature usually carries.** It is display-only and tested (risk 14).
- **T13.** **Seeded runs no longer repeat a line inside one run.** They still repeat across runs, as told plainly item 10 says.

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | Camp bloat breaks the 2-hour ceiling | CH-5 reports with a new-player arm; cut order; Accept kit defaults; `BuyBatch`; Quick camp |
| 2 | Players read figures as vendors or recruits | Schema, `AmbientCoreBlind`, grammar, the "Has nothing to sell. Has things to say." first tooltip, role-ID rows |
| 3 | "Hear everyone" chore | Zero effect; 0-click chain delivery; Hearsay log; figures off the station ring; `AmbientTalkRate` alarm |
| 4 | Repetition on losing streaks | Straggler cap of 1 unprompted plus 1 unseen Hear-out line per visit; no line twice in a run; deep F1 pools sized by the re-run toy; floor-k Straggler in the hub; key-rate pool sizing; `KeyExhaustion`; silence |
| 5 | Tone slips (humour near death; Dreamer implications) | AN4/AN5/AN10; `DreamerLine`; tone review of every T0, T1, Roll-Reader and sincere line |
| 6 | Lore leaks tactics | `LoreCarriesNoFact` via `publicRuleRef`; tooling |
| 7 | Many owners must sign off | Most changes are readings; staged by wave; fallbacks stated (14.2) |
| 8 | Writing dependency web | Chain-graph tooling before M3 |
| 9 | Stragglers mimic units on stills | Seated prop-down poses (the Placecard-Writer included); props kept off reserved silhouettes; gate rows; caption-only fallback |
| 10 | Premise commitment in the hub | Q1 A (agreed); premise-neutral writing until RS 19.2 is answered |
| 11 | Wake slips, thinning camp-1 pools | Gaol and Kennel funded at 20–30 lines |
| 12 | Research from recall | Check every quoted claim before client use |
| 13 | Localisation cost (~54k translated words) | Text budgets per figure; plain-font budget per language; pronoun tokens in the `LoreLine` schema and the localisation brief; rates verified before commitment |
| 14 | The lore state is more machinery than a text feature usually carries (snapshot, run state, two roots, hub write) | Every piece is display-tagged and outside the key; `LoreInert`, `NoLineTwicePerRun`, `HubLoreWriteIdempotent`; the M2 cast is small, so the machinery is proven before M3's cast arrives |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the diorama size, dolly time, hold-to-confirm time, debounce, gutter threshold, idle trigger, cooldowns, settle threshold, the Accept-kit-defaults and Quick Restock run counts, potion stock counts, report targets, duck level, art and writing counts.
- Godot scene structure for the diorama, the Camp List, the hub screen and menu-room, with no physics and no rules state.
- Art and audio timings within 6.
- Line text beyond the samples, inside the charter and tone review.
- Names, until the naming pass.
- **Not tuning:** the camp order; the WO8 list; talk never a command; Core blind to ambient types and `loreMemory`; seeded runs write no lore; no effect of any kind from a figure **(agreed, Q2)**; no walking **(agreed, Q3)**; the Stairtop premise-neutral **(agreed, Q1)**; the M2 hub figures **(agreed, Q4)**; at most 3 settled Stragglers visible **(agreed, Q5)**; at most one death owner per Night and one first-win speaker; the hub cap of 1 on a death Night; the Afterword on the run-start card; the beat cap of 1 per Night; no counts, no pity; no reserved cues and no breath, footstep or whisper family; the comic bus ducked to silence on the death path (RC8, agreed); no timed text.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **The RS owner:** CH-1 (RS 6.8), CH-4, CH-5, CH-6 (RS 7.5), CH-10, CH-11, CH-12, CH-14 (keeper bullet), CH-20 (request), CH-24, CH-25 (RS 11.2); codex hosting of Residents pages; the premise (RS 19.2).
- **The CMP owner:** CH-1, CH-5 (CMP 7.4), CH-17, CH-21, CH-23, CH-26.
- **The DH owner:** CH-1 (DH 13.1), CH-16 (HE-6).
- **The SV owner (segments 11–12):** CH-2, CH-3, CH-5 (SV 6.8, 6.11), CH-7, CH-8, CH-9, CH-16 (`vendorPotions`), CH-18, CH-19, CH-25 (SV 5.2); V19t unchanged.
- **The ITEMS owner:** CH-15, CH-16, CH-18, CH-24 (ITEMS 13, informed).
- **The LE owner:** CH-15, CH-16, CH-20.
- **The CN owner:** CH-15 (phial).
- **The TL owner:** CH-15 (TL 6.11, informed).
- **The EV owner:** CH-12 (EV 6), CH-14.
- **The main design owner:** CH-6, CH-7, CH-9 (MAIN 6.6, read), CH-13, CH-20 (MAIN 4.7, read), CH-24; telemetry consent; the visible easier setting.
- **The XI owner (segment 10):** CH-22; codex pages and hub surfaces; Tithe Bowl placement.
- **The TK owner:** CH-24 (informed).
- **The simulator:** the simulator reports of 9.2 (r), in the `LoreEmu` sandbox, with the streak, f1only, fast-path and average arms and a declared Hear-out bot policy.
- **Human sessions (SV 6.11) and, later, telemetry:** the human reports of 9.2 (r), on the first-10-runs and veteran arms.
- **Art, audio and writing:** 5, 6; the chain-graph tooling (9.3).
- **Naming pass:** every placeholder here and the A.2 flags.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Critique resolutions

Kept as written; names as at the time (renamed by the naming pass, NP-26: Kindler → Log-Carrier, Lingerer → Straggler, Sitter → Roll-Reader; Nightmare → Peril for the ladder).

| Critic finding | Resolution |
|---|---|
| Arrival shows restore before catch-up (high) | Adopted: arrival → catch-up → rest summary command (4.2; CH-1, CH-2) |
| Phial cannot fit the reward card (high) | Adopted: pick 1 of 3; never in the shop (CH-15) |
| Tithe Bowl "never at camp 4" (medium) | Corrected: Sketch only at camp 4 |
| Seen-mark flush at WO5 undeclared (medium) | Adopted: queue in `run.save` pendingProfile; flush where SV flushes (CH-9) |
| Seeded writes; shared DV2 cap (medium) | Adopted: read-only seeded; `AmbientSelfRefCap` |
| Afterword relocation (medium/high) | Withdrawn; Afterword unchanged; Sitter pool with `AfterwordNoEcho`; M2 keeper text as service strings |
| Reserved audio textures (medium) | Adopted: no breath, exhale, tick or click; Stillwatch and Whipper-in textures changed |
| F-T1 overstated, silent auto-defaults (medium) | Replaced by Accept kit defaults; overrun marked unverified |
| Camp-time wording (low) | Corrected (CH-5) |
| Wave errors (`bossMemory`, ladder, Deeds) | Corrected (5.1, 4.9, 4.7) |
| Potion key split | Party at floor-N generation (CH-16) |
| Premise misquote in Q-A | Corrected (Q1 cites "few wake") |
| "Camp Ledger" collision | Renamed Camp List |
| WO8 over-listing | Limited (CH-3) |
| f1only never meets the cast (high) | Floor-k Lingerer in the hub (CH-13) |
| Death-path beat pile-up (high) | Death-path budget; Roll non-modal (4.10) |
| Fast path empties guarantee | Restated per hub visit; fast-path arm; third card line held |
| High-frequency keys drain | Key-rate pool sizing; novelty term; `KeyExhaustion` |
| Controller bloat | Ambient off the station ring |
| First hub visit after death | No intro then |
| Chain rewards make Hear out a chore | 0-click chain delivery; `ink` dropped |
| Companions mute at camp | Name tokens in Kindler lines; AN13 reserved |
| Road not taken told 4 times | One visual, one voice (`factOncePerRun`) |
| Fading captions are timed text | Persist; hold captions |
| Thin victory beat | Kindler owns first win; Stillwatch line revised |
| "Listen" verb collision (high) | Renamed "Hear out"; `AmbientVerbUnique` |
| Later hooks misread (high) | CH-24 restatements |
| Unlocks and milestones missing (high) | 4.11 |
| Perilous Door undesigned (high) | 7.6, CH-20 |
| Profiles, replays, continue, abandon, records | 4.13 |
| Accessibility gaps | Plain font, colour-blind gate, hold-to-confirm, captions |
| Validator coverage thin | V20 additions (9) |
| Audio beyond the fire | 6, audio table |
| "Night" overload | Hub figure renamed Copyist; button "Begin"/"Begin again"; "Book of Nights" to the naming pass |
| Onboarding for irreversible actions | Inline consequence lines (4.12) |
| Lone Road, Nightmare, seeded camp variation | CH-23; tier mark; ribbon mark |
| The design check of this document (builder, consistency, player, art-narrative) | Resolved in 14.3 (lead's calls after the check 1–41; told plainly T1–T13) |

### A.2 Names checked (case-insensitive, whole word, 30 approved documents and the reviews; re-run while writing)

Kept as written: the record of the check made while writing. The naming pass (approved 2026-11-24) since renamed, in this document (NP-26 unless named): Hollowfolk → the Fireside (V20 id `Hollowfolk` unchanged), Kindler → Log-Carrier, Chalkwright → Slate-Sketcher, Lingerer(s) → Straggler(s), Indexer → Shelver, Barker → Showcaller, Sitter → Roll-Reader, Framer → Gilder, Keeper's Cart → the Pedlar's Cart, Coat rack → the Newel, Quiet camp (setting) → No idle chatter, Seeded night → "Play a run code", "Residents of the Hollows" → "Residents of the Fireside", "the Hollow ahead" → "the next camp", Stairhead → the Downstair, Silk → the Archive (The Cobweb Archive), Wayfarer's Pack → Wayfarer's Belt (NP-30 cite), Head Archivist → Chief Archivist (register 5.5.5), Rekindle → Take Comfort (register 5.3.39); kept: Stairtop, Book of Nights, the Hollow, Last Hollow, Hear out, Begin again.

- **0 hits, used:** Kindler, Chalkwright, Stillwatch, Trusty, Whipper-in, Wreath-Carrier, Off-Shift Singer, Indexer, Barker, Placecard-Writer, Subletter, Copyist, Sitter, Understudy, Framer, Hollowfolk, Hearsay, Stairtop, Lingerer, Lingerers, `loreMemory`, Book of Nights, Camp List, Hear out, Begin again, `BuyBatch`, Accept kit defaults, Quick Restock, Quick camp, Quiet camp, Depth gauge, Heirloom shelf, Coat rack, Edge of the Light, Muster Post, Keeper's Cart, Letter rack, Seeded night, New in the dream, Residents of the Hollows, `LoreContext`, `AmbientRecord`, `LoreLine`, `CampSetRecord`, and every V20 row and report name in 9.2. **Added after the check, 0 hits:** `loreShownCount`, `loreLastShownNight`, `lastFiredNights`, `loreRunState`, `VenueSurfaceRegistry`, `TrophyRecord`, `ChooseReward`, `CampRest`, `InsufficientCoin` (a new rejection code), `LoreEmu`, `CampRestockCoverage`, `levelup/` keys, Newel, "Abandon and begin", and the new V20 rows of 9.2. `OutOfStock` and `partyCountAtGeneration` are existing terms, used as defined.
- **Collisions, not used:** Sleepwalker (Heart roster), Insomniac (Heart roster), Storybook Knight (Heart soldier), Mender, Hearth (Herald subclass), Pilgrim (Pilgrim's Reliquary), Hired Weeper (next to Hired Wailer), Tallyman ("Tally"), Porter (Porter's Satchel), Archivist (Head Archivist), Scribe, Daybook (Turnkey's Daybook), Sit with, Listen (core action), "Go down" (downing verb), Camp Ledger (LE camp ledger), Stray (EP K4), Landing (rules term), Threshold (common word), **Sealed Letters** (EV Strand "Sealed Letter"; renamed the Letter Rack, new lead's call), Pigeonhole (1 hit).
- **Near-collisions kept, flagged to the naming pass (new lead's call):** Arrival (SV 6.2 simulator axis `Arrival`); Stairtop (FL Stairhead); coat rack and peg (EV Curiosity "Coat Hooks by the Door"; the object is also re-dressed, 4.8); Memento wall (LE 6.11 "memento wall", the same object, kept deliberately).
- **Near-collisions found by the check (substring grep), flagged to the naming pass (lead's call, after the check):** Lingerer (ARC retired "Linger" against CON's "Lingering"; the rest summary shows "lingering conditions ended" on the same visit); Kindler (the Herald's Kindle and the Rekindle heal, at the station that restores HP); Indexer (Silk's Weaver of the Index, the Index Well, the Index Crossbow); Barker (the system term "bark", RC8); Sitter (EV W6 "Sit with"); Framer (Master in the Frame, the Frame-Master's Spear, EV's "figure frame"); Chalkwright (XI's "Chalked"); the camp phase **the Hollow** (LE 6.15's "the Hollow ahead", meaning the next camp, and RS's "Last Hollow"). The client confirmed the placeholder names and the three-phase presentation, so none is renamed here; the naming pass decides. V7.NameRegistry runs add a substring grep to the whole-word check.
- **Existing terms cited, confirmed present:** Jailer of Hours, Old Ragged, Drowned Carillon, Mouthless Librarian, Madame Ashes, Head of the Table, Pylorus, Executor of the Will, Night Governess, Precentor, Mother-Shape, Turnkey, Hired Wailer, Night Usher, Perilous Door, Lone Road, Old Friend, Wayfarer's Pack, Reprieve Phial, `campReward`, Tithe Bowl, Surveyor's Sketch, Ear Trumpet, Wax Key, Unwoken Roll, Afterword, `AfterwordExhaustion`, `KitChoicesDefaulted`, `ProbeResistance`, `SpeculationEquivalence`, `DisplayMemoryInert`, `pendingProfile`, `uiMemory`, `bossMemory`, `curiosityDigest`, `nightmareTier`, `keepsakeProps`, `FinalBossNoDreamer`, "keepers of the sleep" (BR), "the Hollow ahead sells rare" (LE 6.15), "few wake" (RS 7.5), XI 4.6.3 "only the Sketch is offered", DV2 inside the seed (EV 4.5), RS 6.8's camp-2 bullet, SV 4.8.3 WO1–WO8.
- **Prefixes:** `\bCH-[0-9]` 0 hits; `\bV20` 0 hits (specs and reviews).
- **Flagged to the naming pass:** "Keeper" (vs BR "keepers of the sleep"), "Book of Nights" and "Seeded night" (vs Night Usher, Night Governess, Nightmare), the near-collisions above, and every placeholder.

### A.3 CH cross-reference (regenerated after the check)

| Id | Sections here |
|---|---|
| CH-1 | 4.2 |
| CH-2 | 4.2, 4.4, 14.3 |
| CH-3 | 4.4 |
| CH-4 | 2.3 F1, 4.6 |
| CH-5 | 4.5, 4.12, 8, 9.2 |
| CH-6 | 0.2, 4.8, 5.2 |
| CH-7 | 4.15.5, 4.16, 9.1 |
| CH-8 | 4.16 |
| CH-9 | 4.4, 4.16 |
| CH-10 | 4.15.2, AN13 |
| CH-11 | 4.6, AN5 |
| CH-12 | 5.1, 6, AN9, AN10 |
| CH-13 | 0.2, 4.8, 4.10, 4.15.7, 5.1, 7.7 |
| CH-14 | 4.10, 4.15.6, 5.1 keeper |
| CH-15 | 0, 4.3, 4.4, 7.1 |
| CH-16 | 4.3, 7.2 |
| CH-17 | 4.2, 7.4 |
| CH-18 | 4.3, 7.3 |
| CH-19 | 4.4, 7.5 |
| CH-20 | 4.3, 7.6 |
| CH-21 | AN10 |
| CH-22 | 0.2, 4.1, 4.2, 4.13 |
| CH-23 | 4.7 |
| CH-24 | 2.2, 4.9 |
| CH-25 | header (Validator), 9.2 |
| CH-26 | 0, 4.2 |

## Appendix B: sources

- Game details in section 1 are from memory and general design knowledge, except the Hades line counts (web search in the research pass) and Valve's dynamic dialogue (Ruskin, GDC 2012); none is cited as a rule.
- The segment 13 proposal (the synthesis), its three models (A, B, C), the cast-and-engine pass, the creative pass, the numbers pass, the judges' and critics' reports, and toy scripts, illustrative only, kept in the design lead's scratch area, not in the repository.
- The 30 approved documents listed in the header and the cross-document comparison of 2026-11-23.
