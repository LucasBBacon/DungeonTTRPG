# Charm Abilities: Design (Segment 16, the TK 24.2 pass)

Date: 2026-11-24
Status: Approved, 2026-11-24
Parents (immediate): `2026-11-17-trinkets-design.md` (trinkets design, "TK", whose 24.2 is this pass), `2026-11-23-heirlooms-curses-design.md` (heirlooms and curses design, "HL", segment 15, approved 2026-11-23, **the most recent template**), `2026-11-23-starting-kits-design.md` ("SK", segment 14), `2026-11-23-camp-hub-design.md` ("CH", segment 13), `2026-11-23-seeds-validation-design.md` ("SV", segments 11–12), `2026-11-23-exploration-information-design.md` ("XI", segment 10), the loot designs `2026-11-22-loot-economy-design.md` ("LE", 7a), `2026-11-22-item-rolls-design.md` ("IR", 7b) and `2026-11-22-loot-sources-design.md` ("LP", 7c), `2026-11-22-events-design.md` ("EV", segment 8), `2026-11-22-boss-rooms-design.md` ("BR", segment 9), and `2026-11-23-cross-document-comparison.md` (reviews, "CC").
Other parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` ("STAT"),
`2026-11-16-ability-resource-model-design.md` ("ABL"),
`2026-11-16-conditions-design.md` ("CON"),
`2026-11-16-enemies-design.md` ("ENM"),
`2026-11-16-items-inventory-design.md` ("ITEMS"),
`2026-11-17-weapons-design.md` ("W"),
`2026-11-17-armour-design.md` ("AR"),
`2026-11-17-consumables-design.md` ("CN"),
`2026-11-17-death-healing-design.md` ("DH"),
`2026-11-17-classes-concepts-design.md` ("CL"),
`2026-11-18-vanguard-class-design.md` ("VG"),
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC"),
`2026-11-20-herald-class-design.md` ("HR"),
`2026-11-20-companions-design.md` ("CMP"),
`2026-11-21-run-structure-themes-design.md` ("RS"; its change ids are RT-n), `2026-11-21-floor-layout-design.md` ("FL"), `2026-11-21-room-terrain-design.md` ("ROOM"), `2026-11-21-hazards-design.md` ("HZ"), `2026-11-21-traps-locks-design.md` ("TL") and `2026-11-21-encounter-placement-design.md` ("EP").
All 33 approved specifications and the cross-document comparison were read (TK and HL in full) or grepped for: charm, trinket, talisman, knot, token, Wardstone, Ward-Knot, Trophy Tally, stored moment, ButtonCap, Warded, Helped, once per fight, passive, previewHint, denylist, one-table rule, value equals budget, 24.2, TK Q1, TK Q2, TK Q3, Bloodknot, Kinstone, Scrollwright, Muse's Token, Spotted, flanking, Recoil, Held, armed, Catch, Catch-up, Patina, accent, Due, boon and every placeholder name in 5; after the check also for pillow, bell, breath, bead, knock, pip bar, silhouetteClass, Hunch catch, knotted-sack, `armsSatisfied`, `KitNoTrinket`, `UnlockHasHubObject`, `plateMarks`, `IKnowledgeQuery` and the one-fight rule. Every rule cites the sections it relies on. **This document edits no parent document** (13).

Items marked **(agreed)** are the client's answers of 2026-11-24 (final) to the five questions this design raised (Q1–Q5; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document; parent documents' questions are always named with their document ("TK Q1", "TK Q2", "TK Q3", "SV Q1", "IR Q4") so that they never read as this document's Q1–Q5. The client also said that everything else in the lead's proposal stands as written: the shapes Spend, Catch, Keep and Shaper (Later); the closed `charmTriggers` list; `forecastHint` and `plateHint`; feedback and sounds; two charms per unit; the catalogue with its statuses; the data gates G1–G3 (renamed **CG1–CG3** after the check, ids only, because G1–G3 are taken in DH, CMP, EP, W and HL; 14.3 T1); the reports and shadow records; the data and validator V23 `CharmLane`; staging; ownership and hooks; the binding requirements; CA-1 to CA-25 (CA-26 and CA-27 added after the check); the risks; and the appendix resolutions and names. Names are placeholders pending the naming pass.

**The lead's answers (0.1) and the recommended frame (3.3)** were accepted by the client by proceeding to the five questions. They are recorded here as the lead's answers; the client confirmed them on 2026-11-24, when it approved this document as a whole, **including every told-plainly item of 14.1**: small passives fit only as small moments, and a "+1" reading is banned; outputs stay Warded and Helped until Spotted is ruled; the drafts' boon budget reason is corrected; TK 24.2's timing is split (framework, reports and shadow records now, content after M2 data); a third of runs may see no charm; the Wardstone envelope toy result is a measurement question; Scrollwright's Token leaves the charm lane; "talisman" leaves charm cards and the Patina "charm" becomes "accent"; and the rejected candidates.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-24, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal, mostly from the corpus check; they are listed in 14.1 and the client confirmed them all on 2026-11-24. Items marked **(lead's call, after the check)** were made in answer to the design check of this document (builder, consistency, player and art-and-audio lenses); none overrides a client answer; they are listed in 14.3 with the "Told plainly after the check" items (T1–T17), and the client confirmed them all on 2026-11-24. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change CA-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-24 (with Q1–Q5 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge, a critic, a model or a draft, or state an unwelcome forecast, and were told to the client in plain words (14.1; those found in the check, 14.3, T1–T17); the client confirmed them all on 2026-11-24. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q5 of this document (2026-11-24, final), or one recorded in an approved document (named with its document), restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal (the lead's answers of 0.1 and the frame of 3.3 included); confirmed by the client with this document on 2026-11-24 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-24 |
| **(lead's call, after the check)** | A lead's call made in answer to the design check; never overrides a client answer; listed in 14.3, with the "Told plainly after the check" items, and confirmed by the client on 2026-11-24 |
| **(change CA-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text are flagged ⚑ there; the client's approval of 2026-11-24 covers them |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a model, a judge or a critic, or states an unwelcome forecast; told to the client in plain words (14.1; those found in the check, 14.3) and confirmed by the client on 2026-11-24 |
| **(request)** | Asks another owner to rule; changes nothing until that owner rules; the fallback is stated (14.2) |
| **(hook)** | Owned by another design; this design states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator or human sessions (8, 9, 10). Never binding. The landing envelope numbers (600‰, 500‰; TK-4) and ButtonCap 1 (agreed TK Q3) are **binding** and are never marked *(illustr.)* |

**The client's brief.** "Go ahead with the charm abilities, small passives sounds right, I have nothing else specific for this, but you can research further if this is a good fit for the system, and if so how it would look. I look forward to seeing your research and considerations." Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; **combat is key**; contest the client or approved views where warranted.

**Change prefix.** This design uses **CA-n**. A grep for `\bCA-[0-9]+` across `docs/` (the 33 approved documents and the reviews) finds **0 hits** (re-run while writing, 2026-11-24). The four research drafts behind this document numbered CA rows independently and inconsistently (CA-7 meant Spurred in one, the read count in another); this document issues **one register, CA-1 to CA-27** (13; CA-26 and CA-27 were added after the check), and no id has two meanings in it (A.3).

**Validator.** **V23 `CharmLane`** is free: a grep for `\bV23\b` finds **0 hits** (re-run while writing). V1–V7 belong to RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV, V17 BR, V18 XI, V19 SV, V20 CH, V21 SK and V22 HL (`HeirloomsCurses`, HL-14, now listed in RS 11.2 and SV 5.2 as V1–V22). V23 joins the consolidated suite only through **CA-18** (RS 11.2 suite table; SV 5.2 becomes V1–V23), as V22 did through HL-14. The drafts' four names (`CharmAbilities`, `CharmTriggers`, `CharmString`, `CharmLane`) collapse to `CharmLane`.

**What "charm" already means (settled; agreed TK Q1, TK Q3).** A charm is a **trinket kind**, location None (count 2), lane "**Stored moments:** Warded and Helped from the charm's own pool or trigger", signature shape "Button or once-per-fight trigger: a moment you choose when to spend" (TK 5.5). It sits under ButtonCap (at most one trinket-granted button per unit, charms the only button carriers in M2; agreed TK Q3), the denylist (TK 6.2), the caps of TK 7.1, zero added damage (agreed TK Q4), value equals budget (ITEMS 12.5), the one-table rule (AR 5.5; TK rule 15), the mandatory `previewHint` (TK 9.1), and `fight`/`rest` pools with `camp` Rare-only (TK 9.3). The M2 roster is **Wardstone, Ward-Knot, Trophy Tally** (TK 13.7). Charms are never cursed and never heirlooms (HL 4.6, HL-26). TK 24.2 is the agreed future pass this document fills.

---

## 0. At a glance

- **Verdict: "small passives" fits, in one reading only (told plainly).** A small passive here is a **small moment**: an automatic rule that waits for a named combat outcome, fires at most once per fight, and gives a **visible** Warded or Helped to you (or, on a `rest`-pool major, to one adjacent ally). It is never a small number that is always on. The always-on reading is already dead under the denylist, the buff test and zero added damage, and stays dead.
- **The shape already exists.** TK 5.5's None row allows "once-per-fight trigger", TK 9.1's Passive row is "event-shaped, named trigger, mandatory previewHint, never a number", and **Trophy Tally** ships exactly this way. This pass grows that shape; it does not invent a new lane.
- **Growth goes into triggers, not outputs or buttons (told plainly).** ButtonCap is 1; the button outputs are used up by approved cuts (Luckstone, Help-whistle, Spurred boots; TK 13.9); the M1+M2 condition budget is 32 of 32 (CON 16). So charms stay Warded/Helped and differ by **what they catch**. That is the Slay the Spire pattern, and in a positional tactics game it moves the decision from "when do I press" to "what do I set up".
- **Four shapes (change CA-1; agreed, Q1):** **Spend** (the existing button, ▸), **Catch** (automatic once-per-fight small moment, ◈; the growth shape), **Keep** (a catch fills the charm, the one button spends it; one gated record, Ashen Bead), **Shaper** (Later). Counters are rejected for now. TK 5.5's shape cell becomes "**Charms catch moments:** a token earned from a named moment in the fight, or one button you spend"; "stored moments" stays as the domain words.
- **A closed `charmTriggers` vocabulary of outcomes, never sources (change CA-2, CA-3):** "forced movement you cause is stopped early", never "your Push". Never class, verb or item ids; never fight-start or round timers; never flanking (a rejected rule); never out-of-combat Listen, stealth or Search.
- **Three new runtime limits (change CA-5; agreed, Q2):** one charm fires per unit per event (`CharmFireOnce`); a trinket output never fires a charm (no chains); **one caught token per output per receiving unit per fight** (closes Helped stacking into the Shade's Exploit).
- **Exact resolution (lead's call, after the check; 4.6):** catches are queued and resolve only after the use that caused them is complete, so a token never affects the rest of that use; order is core event sequence, then ascending unit order, then slot order; a provenance rule decides what counts as trinket-caused; a blocked catch waits if the block can lift this fight and is **missed** if it cannot, or if its once-per-fight moment has passed; every charm per-fight state follows BR 4.2.5's one-fight rule in the Sanctum.
- **Catches that read another location's subject (Prone, hazards, Teetering) are allowed to grant Warded or Helped only, never changing how the subject resolves (change CA-2; agreed, Q3).** This amends agreed TK Q1 domains. The base batch stays domain-clean except Jolt Knot's danger branch.
- **First content batch, M3 (*illustr.* names; several renamed after the check, T5):** Hangnail, Jolt Knot (base content); Minder's Pebble, Second's Hairpin, Bloodknot (shadowed in its `rest`-charged form), Walnut, Wishbone and Meeting Stone (unlock candidates, each through CG1; the last two added after the check so that the shipped batch can meet the setup share, T11); Ashen Bead (the one Keep record, gated on CG2). Rejected or retired as written: Near-Miss/Blink Bone, Milk-tooth, Press-Stone, Bier-Knot, Held-Breath Knot; Scrollwright's Token moves to TK 19 exploration-first.
- **Rarity, ButtonCap, slots (change CA-24; agreed, Q5):** Common and Uncommon only; **TK Q2 stays closed** (no Rare charms); **ButtonCap stays 1**, and Catches never count toward it; up to 2 charms per unit (None count 2); no slot change. The charm string is held as a future M4 item.
- **Charm boon: the Bobbin (the agreed Underpillow, renamed after the check, T4; change CA-20, CA-26; agreed, Q4).** The drafts' "budget" reason for declining was wrong (told plainly): heirloom boons already sit outside the per-floor budget (ITEMS 14; HL 4.1). At M3, the player may take as the one starting boon a **Common, catch-only charm** chosen from the unlocked catches; never a button, Keep, Uncommon, heirloom, persistent or cursed; gated on a parity report and a boon pick share of at most about 40% *(illustr.)*. After the check: the boon itself is earned by a milestone (MAIN 4.7 "earned boon"), goes to the hero at WO1, and is off on shared, seeded and daily runs (4.11).
- **Read count (change CA-10 ⚑, request to CL; corrected after the check, T2):** CL 7.6's current text does **not** count a Catch 0: its automatic-reaction bullet is for pool-free reactions, and a Catch has a `fight` pool and marks nothing on the battlefield, so under today's text a Catch counts 1 and **Trophy Tally already puts a Vanguard at 5**. CA-10 asks the CL owner for two bullets: a Catch with a plate ready pip counts 0, and an items row for the one trinket button. Fallback: catches count 1, and the "rarely fires"/Vanguard verdict covers it.
- **Name fixes (change CA-12, CA-13):** drop "talisman" as a charm display word (it is the Reprieve Phial's item category and an IR 4.2 fine category); rename the Patina "charm" ornament to "accent". The working label "Catch" also meets TL's approved trap kind **Catch** and TL's "Hunch catch" badge, so player-facing text never prints it: cards use TK 15's keyword line under the ◈ glyph, and logs and Inspect say "fires" (new lead's call; extended after the check; 2.3 F13).
- **Timing (change CA-19):** TK 24.2's timing is a lead's call, so the framework is decided now and **M2 instrumentation plus simulator-only shadow records** start now; **content approval still waits** for CG1 (simulator) and CG2 (M2 playtest). The M2 roster in TK 13.7 stands.

### 0.1 The lead's answers in plain words (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-24)

1. **Do small passives fit?** Yes, and better than more buttons. But "small" must mean small in rules, not small in numbers. A charm passive is a sentence like "*the first time a harmful condition fails to land on you, you gain Helped*": one named moment, once per fight, a token you can see, a preview that tells you when you are about to trigger it, and a flash and log line when it fires. "+1 to saves" never fits: it is invisible, it becomes best-in-slot, and the approved rules already ban it. In player-facing text call them **small moments**, so nobody expects "+1" (told plainly).
2. **Why it fits this game specifically.**
   - (a) The button side is full: a self-Helped button copies Steady Aim, an ally-Helped button copies Sigil of Aid, Spurred reads as speed, and two ward buttons already ship.
   - (b) Automatic catches cost companions nothing on their under-10-second turns (ABL 11.4).
   - (c) They reward the tactical setup the game is about: shove a foe into a wall, guard a falling friend, stand where the blows land.
   - (d) They are honestly small: one Helped catch moves party output by under 1% per fight *(illustr.)*.
3. **How it would look.**
   - On the card: one line in TK 15's trinket format, "◈ **Hangnail:** once per fight, automatic: when a harmful condition fails to land on you, you gain **Helped**" (the card format corrected after the check: the ◈ glyph carries the shape and the card never prints the word "Catch"; 4.2).
   - On the unit plate: one small cluster of two pips, one per charm, with a filled pip while ready and a hollow pip once spent (6).
   - In the move or attack forecast: "If the shove stops short → Jolt Knot: Helped".
   - On fire: a small dream beat (the knot tightens), a floater, a log line, and one of only two output cues (placeholder timbres after the check: a soft mallet tap on a hollow gourd for Warded, a marble rolling to rest in a shallow bowl for Helped; the first proposal's bell and breath were reserved sounds, T3).
4. **What a unit can wear.** Up to two charms (the None location holds two), still at most one button. Two catches can sit together, but a unit receives at most one Helped and one Warded from charm catches per fight (agreed, Q2), so two Helped catches on one unit share one catch a fight; the rest screen says so (4.7).
5. **What changes in M2.** Nothing the player sees except two text fixes ("talisman" leaves charm cards; the plate gets ready/spent states for Trophy Tally). The build gains reports and simulator-only shadow charms so that the post-M2 decision has data. (Trophy Tally's sound moves to the Helped output cue at M3, with the batch; after the check.)
6. **What waits for data.** Which new charms ship, whether the Keep shape ships, whether Wardstone stays a button, whether charms need a bigger share of loot (a third of runs currently see none *(illustr.)*; told plainly), and any new output such as Spotted.
7. **What the client decided.** Five questions, all answered with option A on 2026-11-24 (0.2): the lane wording and shapes (Q1), the new limits (Q2), cross-location triggers (Q3), the charm boon (Q4), and keeping Rare charms closed, ButtonCap at 1 and no slot change (Q5).

### 0.2 The client's five decisions (agreed, 2026-11-24, final)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Lane wording and shapes | **A.** TK 5.5's shape cell restated as "Charms catch moments: a token earned from a named moment in the fight, or one button you spend"; Catch is the growth shape; one gated Keep record (Ashen Knot; renamed the Ashen Bead after the check, T5); "stored moments" stays as the domain words (4.2) | B: current wording, Tally-shaped catches only, no Keep. C: buttons only | CA-1 ⚑ |
| Q2 | New TK 7.1 limits | **A.** `CharmFireOnce` plus no-chain plus one caught token per output per unit per fight (4.6) | B: the first two only. C: no new limits | CA-5 ⚑ |
| Q3 | Cross-location triggers (amends agreed TK Q1 domains) | **A.** Allowed to grant Warded or Helped only, never changing how the subject resolves; the base batch stays domain-clean except Jolt Knot's danger branch (4.3) | B: domain-clean only | CA-2 ⚑ |
| Q4 | Charm boon (CH-24) | **A.** The Underpillow (placeholder; renamed the Bobbin after the check, T4) at M3: choose one unlocked Common catch at run start; never a button, Keep, Uncommon, heirloom, persistent or cursed; gated on a parity report and a boon pick share of at most about 40% *(illustr.)* (4.11) | B: no charm boon | CA-20 ⚑ |
| Q5 | Rarity, ButtonCap, slots | **A.** TK Q2 stays closed (no Rare charms); ButtonCap stays 1; no slot change; the charm string held as a future M4 item (4.8, 4.12) | B: Rare charms with `RareWornCap` 1. C: a charm string outside the slots | CA-24 ⚑ |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Meaning of "small passive": an event-shaped Catch; never a number (0.1, 4.2) | Now | lead's call; told plainly |
| 2 | Shapes: Spend, Catch, Keep (one gated record), Shaper (Later); counters rejected; lane wording (4.2) | Now (spec), M3 | **agreed, Q1**; CA-1 ⚑ |
| 3 | Triggers: closed `charmTriggers`, outcome-keyed, setup/reactive class; cross-location reads grant tokens only (4.3) | Now (spec), M3 | **agreed, Q3**; CA-2 ⚑, CA-3 |
| 4 | Outputs: Warded, Helped; Spurred and the excluded list never; Spotted requested for M4 (4.4) | Now / M4 | CA-4; request CA-23 |
| 5 | New limits: `CharmFireOnce`, no-chain, one caught token per output per unit per fight (4.6) | M3 | **agreed, Q2**; CA-5 ⚑ |
| 6 | Values and pools: Catch = `fight` minor, value 1; ally Warded = `rest` 1/1/1 major, value 2 (4.5) | M3 | lead's call; CA-7 |
| 7 | Policies: Helped catches `always`; Warded catches `always` or `spareForButton`; `onBlocked: defer` (4.5) | M3 | lead's call; CA-8 |
| 8 | Rarity C/U; TK Q2 closed; ButtonCap 1; 2 charms per unit; no slot change (4.8, 4.9) | Now | **agreed, Q5**; CA-24 ⚑ |
| 9 | Boon: the Bobbin (the agreed Underpillow, renamed), a Common catch charm boon, earned, gated (4.11) | M3 | **agreed, Q4**; CA-20 ⚑, CA-26 |
| 10 | Read count: Catch 0 and the button, both requested (4.7) | Before M2 UI | CA-10 ⚑ (request) |
| 11 | Names: "talisman" out; Patina "charm" → "accent"; Recoil, Lodestone, Spite, Duck, Crooked, Held, armed avoided; "Catch" never printed (4.2, A.2) | M2 text | CA-12, CA-13; new lead's call |
| 12 | Validator V23 `CharmLane` (s/r/t); rules 29–31 (9) | M2 | CA-18 ⚑ |
| 13 | Timing: framework, reports and shadow records now; content after CG1/CG2 (10) | Now | lead's call; CA-19 |
| 14 | Corrections found in the corpus check while writing: the Helped window reading (CA-9), the "Catch" trap-kind collision, the boon and the F1 Due, the boon's choice list and seed key, the TK-4 measurement interim (2.3 F13–F16; 14.1) | M2–M3 | new lead's call |
| 15 | Fixes after the design check: resolution timing, order, provenance, once and recurring triggers, missed and waiting states, receiver-keyed caps, Sanctum state, exact predicates, actor attribute, forecast through party knowledge, dormant reveal, Keep mechanics, the boon's placement and earn rule, shadow modes and gate denominators, Bloodknot's rest form, Tally's filter, Second's target, card format, plate states, output cues, silhouettes, names, localisation keys, gate ids (14.3) | M2–M3 | lead's call, after the check; CA-10 ⚑, CA-26, CA-27 |

### 0.4 Intent, goals and anti-goals (lead's call)

**Intent.** Charms make a fight's small turns of fortune visible and worth setting up, without adding a number anywhere.

**Goals.**
1. G-a: charms feel like *moments*, not modifiers; each charm is one line.
2. G-b: each fires at least once per 3 fights for at least one intended wearer (TK 2 Goals; TK 18.3).
3. G-c: a charm is fun as the party's only charm.
4. G-d: companions wear them naturally.
5. G-e: the art team gets dream objects with a visible "catch".

(These goal labels are G-a to G-e so that they never read as the data gates CG1–CG3 of 10, or as the G-numbered ids of DH, CMP, EP, W and HL.)

**Anti-goals.** Stat sticks; invisible procs; best-in-slot; proc chains; a must-wear charm; trigger chasing as bookkeeping; dead loot locked to one weapon verb; per-charm sound soup; mid-hostile-turn prompts.

---

## 1. Survey

Games from memory unless marked; uncertain details marked (unsure). Two web sources were checked by the tabletop researcher (PF2 talismans, Archives of Nethys; 5E DMG charms, conflicting sources). No web search was run while writing this document.

| Source | Mechanism | Praised for | Criticised for | Lesson taken |
|---|---|---|---|---|
| Hollow Knight charms | Notch budget, swap at benches; mostly always-on, some event-shaped | Build identity; unprinted synergy; bench swap | A few must-haves, some dead charms | Rest-gated equip is right; synergy should emerge, never be printed (set bonuses stay banned, AR 5.9) |
| Slay the Spire relics | Unlimited count; event triggers; counters on icon; flash on fire | Readable despite dozens of relics | Some forgettable relics | Few outputs × many triggers; lit/flash feedback → Catch |
| Hades keepsakes | One equipped, swapped between regions; many stat-shaped (unsure) | One-item clarity | Stat keepsakes are bland | One event-shaped item works; stats removed by our denylist |
| Darkest Dungeon trinkets | Two slots, ±X trade-offs, class locks, sets | Real two-slot choice | Number soup; dead class-locked loot | Keep 2 slots; no numbers, locks or sets |
| Dead Cells mutations | Passive slots; on-kill effects | Build tempo | Heal-on-kill dominated | On-kill healing is a must-pick; denylisted |
| Risk of Rain items | Unlimited stacking, proc chains | Power fantasy | Visual noise, invisible proc maths | Identity keys, **no-chain rule**, one floater per event |
| Diablo 2 charms | Inventory stat sticks | — | Inventory tetris, "carry best charms" | A charm is never a stat stick |
| Path of Exile 2 charms | Automatic, belt-slotted, fire on an event, refill from kills (unsure on counts) | Concept | Early reception "forgettable" | Automatic charms must be *felt*: floor ≥1 per 3 fights, visible fire |
| Into the Breach pilots | One deterministic passive per pilot | Readable at a glance | — | Deterministic, positional triggers suit tactics |
| Fire Emblem skills | Chance procs vs deterministic conditionals | Vantage/Wrath plannable | Astra/Luna feel random | Threshold and adjacency triggers, no chance roll |
| XCOM 2 PCS, DOS2 runes | Flat stats | — | Dull; Mobility best-in-slot | The textbook failure "small passives" must avoid |
| Inscryption sigils, Dicey Dungeons | One icon = one rule, condition printed | Clarity | — | The icon is the text; the condition shows on the plate |
| Wildfrost, Monster Train, Griftlands | Hard capacity limits on small upgrades (unsure on numbers) | Legible stacking | — | Hard count caps beat tuning |
| PF2 talismans (checked) | Affixed consumable, one per item, triggered free action, then spent | Cheap reactive tools | Hoarded and forgotten | The game must watch triggers for the player (automatic policy + previewHint) |
| 5E DMG charms (conflicting sources) | Supernatural gifts, few uses, then gone | Memorable gifts | — | That role is our Graces (EV-12); charms must be the repeatable version |
| 13th Age, Blades in the Dark, Into the Odd (memory) | Recharge rolls; spend a resource at a chosen moment; one strange power per item | Authorship; singular items | — | Keep = "catch now, spend later"; one line plus a Whence flavour line |
| 5E attunement (memory) | Count cap of 3 | Stops item bloat | — | A count cap beats a power cap; we already have 2 slots + ButtonCap 1 |

**Cross-cutting conclusions:** small passives work when event-shaped, capped and visibly firing; they fail when they carry numbers, stack without limit or chain invisibly; in deterministic tactics games, positional and threshold triggers beat chance procs; players love unprinted synergy more than printed sets.

---

## 2. Audit

### 2.1 What the approved documents fix (settled unless a flagged CA changes it)

1. **Lane and shape** (agreed TK Q1; settled AR 13.4 via TK-1): None = "stored moments, capacity"; display words "talisman, knot, token" (TK 5.6; see 2.3 F1); never-here: fight-start automatic Warded (Warding Lining), save rerolls (Inspired, Later), coin.
2. **ButtonCap** (agreed TK Q3): 1 trinket-granted button per unit; charms only in M2; buttons combat-only, after initiative (TK 9.2); E+ never adds one; ABL 10.2's eight-limited cap exempts item abilities and ButtonCap bounds them (TK-20); `trinketButtonCap` is a client decision (TK 17.2, 21).
3. **Effect kind** (TK 6.1 item 6): "Granting Warded or Helped from the trinket's own pool or trigger, to you or one adjacent ally." Outputs minor or token only (rule 24).
4. **Denylist** (TK 6.2, closed, rule 16): AC; roll mode against the wearer; attack bonus; always-on advantage; added damage incl. retaliation; HP/temp HP; speed; flat saves; always-on resistance; tag immunity; Steeled/Unyielding/Rallied grants; hard or major conditions; extra actions/reactions; class pools or spells; healing; coin; slot grants; auras, passive party effects, `grants.whileCarried`; acting on class abilities by id or tag.
5. **Caps** (TK 7.1): ReactionCap (off-turn ally effects; TK-3); ButtonCap; DuplicateAnswer + identity keys; `AlreadyWarded`; `AlreadyWardedThisFight` (one trinket Warded per fight); one received trinket prevention per round; `TrinketHelpedLimit` (one trinket Helped per round, Sigil-cheapened Help included); slot order; suppression never refusal.
6. **Values** (ITEMS 12.5; TK 8.1): C=1, U=2, R=3; passive verb with a named trigger or a `fight` minor = 1; major (ally Warded) = 2, `rest` 1/1/1, jewellery only; `camp` = Rare only; `never` banned; `fight` uses minor only (TK-18); **ABL 6.2 "No major power is per-fight."**
7. **Buff test** (ITEMS 12.4): every property declares economy, charge, targeting, prevent, rider, type or cost; always-on numeric modifiers rejected; ≤3 properties, ≤1 rolled (IR-4).
8. **One-table rule** (AR 5.5 agreed; AR 17.1 rule 11; TK rule 15 domain map).
9. **Copy registry** (TK 11; CL-11; VG-18): bidirectional, any charge level; trinkets act only on universal actions, conditions, item use, movement, own pools and "any damaging use"; class-neutral (rule 26: ≥2 launch classes or `anyCompanion`); Luckstone cut as Steady Aim; Help-whistle cut as Sigil; Vex already grants Helped.
10. **Readability** (ABL 11.4, settled): ≤4 things to read per unit; CL 7.6 counting (a passive counts 0 if it marks the battlefield, otherwise 1; glint or row-modifier passive 0; pool row 1; **a pool-free reaction with an automatic policy counts 0**; a Catch is neither, 2.3 F3); Vanguard at 4 at every level; companions target 3, `KitReadCount` ≤4.
11. **Card charter** (TK 15 + ITEMS 8.2, merged IR-9; IR 6.1 wins): glyphs ⛨ ⟲ ✦ ◈ ▸; line format "<Keyword>: once per fight, automatic: ..." with the keyword inside the family name ("Names carry the verb"); writes "charm (none)", never "Charm" (Charmed is Later); fire = floater + log line; greyed reasons listed; icons kit-bashed (kind silhouette × material × element tint × lane glyph); no new cue sounds (RC7).
12. **Conditions budget** (CON 16, 18.1): M1+M2 = 32/32; Later conditions sit outside; a Later grant needs a budget ruling (CL 6.3). Spotted is a Later harmful token (CON 18.9).
13. **Monster use** (TK 12): Wardstone carried-eligible; Ward-Knot elites only, never boss encounters; Tally container-only; carried outputs count toward a type's 2.
14. **Landing envelope** (TK 18.3; binding TK-4): with the maximum legal trinket and armour prevention loadout, each signature condition lands ≥600‰ as often as trinket-free; a boss's hard control lands at least once per boss fight in ≥500‰ of fights; a hero row under maximum companion support.
15. **Trigger floor** (TK 18.3): "At least once per 3 fights for its wearer". **Pick ceiling:** no family >50% of eligible wearers within any class.
16. **Sources** (TK-27; IR 4.3 `lane.jewel.charm`; LE 6.2 trinket share 15% F1, 20% F2–F5): never a boss signature (agreed TK Q2; IR-13); trader trinkets Later (TK 19); events never grant notables (EV 4.4.8, `NoEventLoot`); companions carry no kit trinket (CMP 8.4); V21 `KitNoTrinket`; camp notables shop (ITEMS 10.4).
17. **Heirlooms and boons** (decided MAIN 4.7; settled ITEMS 14): one starting boon; heirloom boons are Uncommon items **outside the floor loot budget**, value for display only, counted as party-owned for the Dream's Due; "unlocks are content, never power".
18. **CH-24** (MAIN 4.7, decided, read): charms get no hub surface; "may add a boon type later (told plainly)" (CH 2.2 hook 32); TK 24.2 names "a charm boon type, offered only under the main design's one-boon rule".
19. **TK 24.2** (agreed TK Q3 wish; **scope and timing lead's call**): fixed frame = ButtonCap 1, denylist, caps of section 7, zero added damage, value equals budget; "Anything that would change one of these goes to the client as a question in that pass"; inputs are button-centred; "after the first M2 playtest data ... and before any Later charm entry is approved. Until then the M2 charm roster in 13.7 stands."
20. **Item rolls** (agreed IR Q4): lane weights carry 0‰ theme bias; families in a category compete equally; `requiresBias` ×3.
21. **Windows** (CON 5.6, lead's call, settled): "`untilSourceTurnEnd` ends when the source's next turn-end step completes. A window opened during the source's own turn runs to its following turn." (read in 2.3 F8).

Added after the check (settled, read; lead's call, after the check):

22. **One fight in the Sanctum** (BR 4.2.5, BR-31): while `sanctumEngaged` is set and the boss lives, a combat end in the Sanctum does not reset the party's fight pools or once-per-fight counters; they reset at `BossDefeated`, or when the party ends a combat outside the Sanctum.
23. **Trinket targets** (TK 6.1 item 6; ITEMS 19.1 rule 22): Warded or Helped "to you or one adjacent ally"; `targets` is `self`, `adjacentAlly`, or `reach2Ally` (Hand over and Administer only).
24. **Trophy Tally's approved line** (TK 13.7) has **no minion filter**: "the first time any damage event reduces a hostile to 0 HP while it is adjacent to you at the moment of the drop".
25. **Reserved sounds** (RS RC7; HZ RC7 timbre table; CH AN9; BR 4.13 `FinalBossNoDreamer`): alarm bells and the struck-bronze fallen-bell cue, the small-hour chime, the Windfall pluck (LP-7), the candle exhale, and the dry-breath family (the Heart Stir cue) are reserved; CH AN9 bars any breath-family sample, and HL 6 keeps "no bells, chimes or breath".
26. **The F1 hero-arms predicate** (HL 4.1.4, HL-3): `armsSatisfied = heroArmsTier ≥ U OR the hero holds an instance flagged heirloom`; a trinket boon placed at run start needs an SK-12 worn-slot row and a V21 `KitNoTrinket` waiver (HL 4.6; SK 7 item 2; HL-26); shared, seeded and daily runs use canonical grants and get no heirloom (HL 4.1.7).
27. **Hub objects** (CH 4.11): every unlock class has a hub object, loot unlocks in the Library (V20s `UnlockHasHubObject`); charms get no hub surface (CH-24).
28. **The Dreamer** (RS 7.5): nothing may show or imply who the Dreamer is: no face, portrait, name, age or biography.
29. **Plate budget** (XI `budgets.plateMarks: 2`), which party plates already spend on the Bloodied and low marks (DH-20); every new surface is an XI `SurfaceRecord` (V18s `SurfaceRegistered`).

### 2.2 Every hook placed

| Hook (source) | Placed in |
|---|---|
| TK 24.2 the pass itself | This document; CA-19 timing split |
| TK 18.3 button press rate → 24.2 | 10 CG2 (restated as rest-charge spend share) |
| TK 20 risk "charms feel thin" | 4.4 outputs, 5 catalogue (ward and ally-target records), 10 probe |
| TK 13.7 "All Later charm entries go through 24.2" | 5.2 statuses for Bloodknot, Kinstone, Scrollwright's Token, Last-Breath, Muse's Token |
| TK 23 / 20 Inspired owner; Kinstone `sourceTied` | 5.2 (held); 11 |
| CH-24, CH 2.2 hook 32, MAIN 4.7 boon | 4.11, CA-20, Q4 |
| HL 4.6 / HL-26 charms never cursed or heirlooms | 4.11 (Bobbin never persists); 12 |
| IR 4.3 `lane.jewel.charm`; IR 4.6 roll concentration report-only | 4.10, 8.6 |
| IR 9.4 "leader-emblem charms" | CA-14 (dangling pointer) |
| CC-4 Wardstone's rolled table leaves 1 | 4.8 (catch lines join `props.trinket.charm`) |
| CC-55 "Wardstone of the Tally" epithet | 5.4 sample text |
| IR 5.3 theme tables (Kennel: Tally; Long Supper, Dreamer's Heart: Wardstone, Ward-Knot) | 4.10 (unchanged; display only) |
| CN 25.3 / CN 14 ward duplicates rejected | 2.1 (charms own the consumable-free ward) |
| CON 18.8 "Heroic-Inspiration-style starting boon" | 4.11 (precedent for boon-shaped sources) |
| CMP 8.4, V21 `KitNoTrinket` | 4.9, 4.11 (the boon is not a kit) |
| SK RC9 accents never hang like a charm | 6 |
| SV 4.6 `unlockHash`, `drawableSet`; SQ41 V5 every-subset cost | 4.10, CA-21 |
| SV 5.2 registry completeness; SV 6.10 and 9.1 (report registry), 6.2 (arms), 6.5 (minimum N), 6.11 (human sessions) | 9, 10, CA-18 |
| SV Q1 human sessions (seeded runs advance no unlocks); SV Q4 suspend journal | 10 CG2, 7 item 2 |
| BR-31 Sanctum one fight; BR-20 tag immunities; Sprawled | 4.3.1, 4.5, 4.6, 8.1, 9.3 |
| EV-12 Graces (one-shot Warded/Helped) | 1, 4.3 (Graces raise ward-blocked catch rates) |
| XI 4.10 `uiMemory`; XI `NoHoverOnly` | 4.7 |
| RS 7.4 greyscale and colour-blind gates; RC4, RC5, RC7, RC9 | 6 |
| ABL 7.3 reaction prompts | 4.5 (no charm uses the prompt path; after the check) |
| ABL 13 / 11.3 party scorer | CA-22 |
| TL trap kind "Catch" (TL 3.4 terms, 6.3 taxonomy) | 2.3 F13; A.2 (new lead's call) |
| LE 6.12 / ITEMS 14 Dream's Due "counts as owned" | 4.11 (new lead's call; 2.3 F15) |
| BR 4.2.5 one-fight rule (BR-31) | 4.6, 9.1, V23t (after the check) |
| SK 7 item 2, SK-3, SK-12, SK 4.5.2 Oddment pool; V21 `KitNoTrinket`; HL 4.1.7 | 4.11, CA-26 (after the check) |
| CH 4.11 `UnlockHasHubObject`; CH 4.9, 4.10 boon picker | 4.10, 4.11, CA-21 (after the check) |
| LP Windfall knotted-sack mark; XI Lodestring "never a knot"; TL "Hunch catch" badge | 6, A.2 (after the check) |
| XI `SurfaceRecord`, `plateMarks: 2`, `IKnowledgeQuery`, `KnowledgeLeak` | 4.7, 6, CA-27 (after the check) |
| RS timbre table, `ReservedCues`, the 85% audio blind-ID gate (XI 6) | 6, CA-27 (after the check) |

### 2.3 Findings (told plainly)

- **F1. "Talisman" collides** (not in CC). It is a TK 5.6 charm display word, and also the Reprieve Phial's item category (CN-1, DH-35; ABL 7.2 "never a talisman in combat"), and an IR 4.2 fine loot category (`loot.categories`, tactical group; family cap "talisman 1"). CA-12.
- **F2. The Patina "charm" collides** (IR 6.2 "at most one charm" per theme on a fixed socket, the IR art table "Charms about 2 / about 8 more", the IR schema; RS 8.0 `theme.loot.patina`). SK already renamed its own field `accentId` for this reason. The art team must not budget charms twice. CA-13.
- **F3. Charm reads are unruled** (CL 7.6). The Vanguard sits at 4 at every level, so an approved Wardstone already makes 5. **Corrected after the check (T2):** the trinket button is not the only gap. A Catch is not "a pool-free reaction" (it has a `fight` 1 pool, ABL 6.2, TK 9.3 `fightUses`; a self Catch is not a reaction but a TK 9.1 Passive), and CL 7.6's passive bullet counts a passive 0 only if it marks the battlefield, which a reactive catch deliberately does not. Read literally, CL 7.6 already counts the approved Trophy Tally as 1, so a Vanguard wearing Tally is at 5 today. CA-10 now asks for both rulings. CA-10.
- **F4. TK 5.5's shape wording "a moment you choose when to spend" is already broken by Trophy Tally.** CA-1.
- **F5. ABL 6.2 forces any automatic ally-Warded charm onto `rest` 1/1/1.** Easy to miss. CA-7.
- **F6. Helped is not damage-neutral.** It switches on Exploit's extra dice for Dexterity attacks (W 6.3). Every Helped charm counts inside the +10% precision ceiling (TK 11 item 6).
- **F7. The 24.2 inputs are all button-centred.** Without new reports, M2 yields no data on passives at all. CA-16.
- **F8. A charm-caught Helped on the wearer's own turn: corrected while writing (new lead's call; told plainly).** The proposal feared that a catch on your own turn after your last attack could expire at once, citing a CON 5.6 wording ("ends when the source's next turn-end step begins") that differs from W-7 ("ends at the end of its next turn"). The approved CON 5.6 text actually reads "ends when the source's next turn-end step **completes**. A window opened during the source's own turn runs to its following turn", which agrees with W-7 and already gives the meaning the proposal asked for. **CA-9 is therefore a reading for the CON owner to confirm, not a text change**; the "Helped wasted" report and the provenance log stay, because a caught Helped can still go unused.
- **F9. Flanking does not exist** (SH 9 table; ROOM 6; BA-7). No charm may bring it back.
- **F10. IR 9.4 points to "leader-emblem charms, as TK and CN Later lists"**, which neither lists. CA-14.
- **F11. A third of runs see no charm** *(illustr.)*: about 9 trinkets per run under an equal lane split gives λ ≈ 1.1 charm finds, P(0) ≈ 33%. The real figure must come from the IR 4.2 weight pipeline (charms have no Rare tier, so they leave F3+ Rare draws), not the toy split. 8.6.
- **F12. The approved charm roster contains no Rare charm and Rare charms would stack** (two None slots). Agreed TK Q2's reason still stands (agreed, Q5).
- **F13. "Catch" is an approved trap kind (new lead's call).** TL 3.4 (terms) and 6.3 (taxonomy) define **Catch** as "A trap on a lock, lid or Door", with "catch host" and "hidden catches" in TL text. The proposal knew "Catch-up" and the boss "catch square" but not this. The charm shape keeps "Catch" as a working label only: the data field (`charm.shape: catch`) sits on the charm record and never shares an enum with TL's trap kinds, but player-facing text must not say "Catch" on both a trap and a charm. The naming pass rules the shape's display word **before M3 card text is frozen**; V23s's name-collision check (9.3) flags it until then. **Extended after the check (lead's call, after the check; T12):** TL's RC5 pattern registry also has a "Hunch catch" badge on containers and Doors, so "catch" is a board-mark name as well as a trap kind. Player-facing text therefore never prints "Catch" or "caught" on any surface, whatever the naming pass rules: cards use TK 15's keyword line under the ◈ glyph ("◈ **Hangnail:** once per fight, automatic: ..."), the log says "Hangnail fired: Ysolde gains Helped", Inspect says "fires when ...", and no hint uses the word. V23s's name grep covers card, log, Inspect and hint strings. "Catch" stays a design and data label only.
- **F14. CA-17 needs an interim (new lead's call).** Until the lead's measurement clarification is recorded, CG1 reports the landing envelope under both readings (the literal maximum legal loadout, TK 18.3, and the realistic loadout), and **the literal reading is the binding gate**, because it is TK's current text. No catch ward passes CG1 on the realistic reading alone.
- **F15. The Bobbin and the F1 Due (new lead's call).** The proposal counted the boon charm "as owned for the F1 Due (as the HL-3 heirloom waiver pattern)". HL-3 waives the F1 Due because an heirloom fills a hero **arms** slot; a charm is not arms. So the Bobbin's charm counts as party-owned for the Dream's Due's owned and duplicate checks (as ITEMS 14 and LE 6.12 treat an heirloom), but it **never satisfies the F1 hero-arms Due and never triggers the HL-3 waiver**. 4.11. **After the check:** HL-3's predicate reads "the hero holds an instance flagged heirloom", so the instance carries its own flag `boonCharm` and **never** the `heirloom` flag; a V13g (g) case asserts that it never sets `armsSatisfied` (CA-26).
- **F16. The Bobbin needs a choice list and a seed place (new lead's call).** The proposal's "one unlocked Common catch" did not say whether Trophy Tally and the base families count, or how the choice enters the run key. 4.11 fixes both.
- **F17. The design check (after the check).** The builder, consistency, player and art-and-audio lenses found gaps in resolution timing, order, provenance, blocked catches, Sanctum state, predicates, previews, the boon's placement, gate inputs, sounds, plate states, card format, icons, names and keys. Each is answered in place, marked **(lead's call, after the check)**, and listed in 14.3 with T1–T17.

---

## 3. Models and judges

### 3.1 The models

- **Model A, Stored moments.** Buttons (B) unchanged, automatic Catch (T) as the growth shape, plus a held moment (H): a catch fills a pip, the one button spends it. (Its label "Held" is not used: 4.2.)
- **Model B, Small triggered passives.** Same frame, more conservative: S1 buttons, S2 moment triggers, S3 shapers (Later), S4 held and counter shapes (Later); once per fight by default; class-verb keys and per-round triggers rejected.
- **Model C, The charm string.** A per-unit string of 2–3 beads with a knot capacity. **C-add** sits outside the trinket slots (a slot grant, banned by TK 6.2; breaks AR 5.3's count of 2). **C-slot** is a charm worn in a slot holding beads.
- **The catalogue.** A 30-row adopt/hold/reject list using the A/B frame.
- **The numbers pass.** A toy model run on all four, with fixes.

### 3.2 Judges' scores (fit / fun / balance / clarity / cost, out of 10; three judges)

| Model | Judge 1 | Judge 2 | Judge 3 | Mean |
|---|---|---|---|---|
| A, Stored moments | 9 / 7 / 7 / 8 / 7 | 9 / 8 / 7 / 8 / 7 | 9 / 7 / 7 / 8 / 7 | 9.0 / 7.3 / 7.0 / 8.0 / 7.0 |
| B, Small triggered passives | 9 / 7 / 7 / 8 / 8 | 9 / 7 / 8 / 8 / 8 | 9 / 6 / 7 / 8 / 8 | 9.0 / 6.7 / 7.3 / 8.0 / 8.0 |
| C, Charm string | 3 / 8 / 4 / 4 / 3 | 3 / 7 / 4 / 4 / 3 | 3 / 8 / 4 / 4 / 3 | 3.0 / 7.7 / 4.0 / 4.0 / 3.0 |

All three judges: A and B are one design at two levels of ambition; take the content from the catalogue after the numbers fixes; reject C for M2 and M3; keep C-slot as an M4 evaluation brief only; reject C-add.

### 3.3 Model B's discipline with Model A's one Keep record (lead's call; accepted by the client by proceeding; confirmed by the client on 2026-11-24)

**Model B's discipline as the spine, Model A's single gated Keep record, the catalogue's content corrected by the numbers pass and the three critics.** Concretely:
- From B: once per fight default; outcome keys; shapers and counters Later; a per-round charm cap held as a report-triggered fallback only.
- From A: one Keep record (Ashen Bead) per wave at most, gated on CG2; it keeps the agreed word "stored moments" meaningful (agreed, Q1).
- From the numbers pass: per-output caught-token cap (agreed, Q2); per-role and per-fight-class gate readings; two-branch merges for sub-floor catches; the lane-weight question; the "rarely fires for this unit" verdict.
- From the critics: the boon reasoning corrected; 7.1 additions sent to the client (answered, Q2); cross-domain triggers flagged (answered, Q3); the Prone "idles" argument dropped; Helped timing, blocked-catch, dormant-line, minion-filter, preview-split, solo-party, unlock-set, localisation, accessibility and SV-versioning gaps closed.
- From C: its CG3 test ("if two charms already bind caps often, a third line is dead") and its "a moment I spend or moments I catch" framing, as the M4 brief (agreed, Q5).

**Contested (told plainly):**
- *Against the client's word:* "small passives" read literally fails here; "small moments" delivers the intent.
- *Against all four drafts and all three judges:* declining the charm boon "because it is loot outside the budget" is wrong; heirloom boons are outside the budget by approved rule. The honest trade-off is in 4.11.
- *Against the drafts:* the new 7.1 limits were not the lead's to make (TK 24.2 fixed-frame clause); catches on Prone/hazard/Teetering are not a "reading" of rule 15 but a change to agreed TK Q1 domains. Both went to the client (Q2, Q3).
- *Against the numbers pass:* its "the approved Wardstone already fails TK-4 at maximum loadout" is a toy result, not a finding; TK-4's signature row is relative ("≥60% as often as trinket-free") and a boss that keeps using control lands after wards are spent. It becomes a measurement question to the lead (CA-17).
- *Against TK 24.2's timing:* the framework, reports and shadow records start now; only content approval waits.
- *Against the approved Later list:* Scrollwright's Token is out of the charm lane (fails rule 17); it moves, not dies.

---

## 4. Rules

### 4.1 The frame (settled; unchanged)

ButtonCap 1; the denylist; the caps of TK 7.1 (with the three additions of 4.6, agreed, Q2); zero added damage; value equals budget; the one-table rule; mandatory previewHint; `fight`/`rest` pools, `camp` Rare-only, `never` banned; buttons combat-only after initiative; combat-first (rule 17); rule 26 class neutrality; no auras or passive party effects (TK 4 decision 7); one adjacent ally per trigger (rule 22); charms never cursed, never heirlooms; equip at rests only.

### 4.2 Shapes (change CA-1 ⚑; agreed, Q1; working labels pending the naming pass)

| Shape | Glyph | Behaviour | Pool and value | ButtonCap | Wave |
|---|---|---|---|---|---|
| **Spend** (existing) | ▸ | Bonus action, combat only: grant the token | `rest` 1/1/2 (self, v1); `rest` 1/1/1 (ally, major, v2) | Uses it | M2 |
| **Catch** | ◈ (plate: one pip, 6) | Automatic: when the named outcome happens, the token goes to its target | `fight` 1 minor, v1; ally-Warded Catch = `rest` 1/1/1 major, v2, Uncommon (ABL 6.2) | Never | M3 |
| **Keep** | ▸ (plate: one pip, framed, 6) | A catch fills the charm; the button spends it as a bonus action; the fill expires at the fight-resources reset (BR 4.2.5; no boss-door pre-ward; TK 9.2, TK 13.9) | Self v1; ally v2, `rest` 1/1/1, the rest charge spent by the button (Keep mechanics below) | Uses it | M3 if CG2, else M4; ≤1 record per wave *(illustr.)* |
| **Shaper** | ◈ (no pool) | Changes a token the wearer already holds; never creates one | value 1 | Never | Later (needs TK 6.1 effect kind 15) |
| Counter (Nth event) | — | Rejected for now (TK 4.1 avoids counters the plate cannot show) | — | — | Revisit after CG2 |

- **Lane wording (change CA-1; agreed, Q1):** TK 5.5 None row "Signature shape" becomes "**Charms catch moments:** a token earned from a named moment in the fight, or one button you spend". The agreed domain words "stored moments" (TK Q1; AR 13.4) are unchanged.
- **Rejected for Q1 (agreed):** B, the current wording with Tally-shaped catches only and no Keep; C, buttons only (the button outputs are used up).
- **Card grammar (lead's call; corrected after the check, lead's call, after the check; T12):** every charm line keeps TK 15's trinket line format, with the family's property keyword (contained in the family name, "Names carry the verb") where the drafts put a shape word. Catch: "◈ **<Keyword>:** once per fight, automatic: when <outcome>, <receiver> gain(s) <token>"; Spend: "▸ **<Keyword>:** bonus action, combat only: <receiver> gain(s) <token>"; Keep: "▸ **<Keyword>:** once per fight, automatic: when <outcome>, the charm fills; bonus action: spend it, <receiver> gain(s) <token>". The glyph carries the shape, so no card ever prints "Catch", "Spend" or "Keep" (this settles F13 for cards). "Once per boss encounter" (4.5) is not on the card: it is shown in Inspect and as the Sanctum fight's card note (BR-31). Each body line is at most the longest approved TK trinket line plus 35% (localisation), and passes CardFit at 150% (V23s).
- **Keep mechanics (lead's call, after the check):** the fill spends nothing; the button spend spends the `rest` charge; at most one fill per fight (a `fightUses` entry); a qualifying event while the charm is already filled is **ignored**, never waiting; a fill that is never spent expires at the fight-resources reset and costs no charge; a Keep greyed by ButtonCap (TK 7.1 slot order) is not live and never fills, so a filled, greyed Keep cannot occur.
- **Why "Held" and "armed" are not used (told plainly):** Held is a CON condition (parent list "Stunned, Held, Asleep, Frozen"); `armed` is the ENM gear tag (only armed types carry trinkets) and a trap state. "Catch" sits near the approved "Catch-up" step, the boss "catch square", TL's trap kind **Catch** and TL's "Hunch catch" badge (2.3 F13); it is kept as a design and data label only, never printed to the player, and goes to the naming pass.

### 4.3 Triggers: the closed `charmTriggers` vocabulary (change CA-2 ⚑, CA-3; agreed, Q3)

**Rules for every trigger (lead's call):**
1. **Outcomes, never sources.** A trigger qualifies on a core event (4.3.1); the catch itself resolves only after the use that caused the event is complete (4.6 item 4). Never a class ability id, weapon verb id (Push, Cleave, Vex, Nick) or item id (TK 11 item 4; CL-11; VG-18).
2. **Combat events only.** Never `combatStart` or `roundN` (copies Warding Lining; no decision; overlaps Graces); never out-of-combat Listen, stealth, Search or disarm (rule 17).
3. **Minion filter (corrected after the check, T7).** A trigger token carries **no** filter; `nonMinion` is a separate data filter (TK 9.1 automatic defaults). New hostile-keyed records declare `nonMinion` by default, or swarm themes push catches to always-fires. **Trophy Tally keeps its approved, unfiltered line in M2 and after** (TK 13.7; 2.1 item 24); filtering it would need a flagged CA row on TK 13.7 and CON 18.9's Tally source, which this document does not make (lead's call, after the check).
4. **Class and actor.** Each trigger is either **setup** (the outcome comes from an action of a party unit the player controls and previews, the wearer's or an ally's; the forecast can name it) or **reactive** (it comes from a hostile's action, a hazard, or a reaction). Each token also carries an **actor** attribute (`wearer`, `ally`, `hostile`, `any`); `offTurnForWearer` is derived from it (any actor but `wearer`, or a wearer's reaction) and, for ally-target records, drives V23s's `reactionShaped` check and ReactionCap (TK-3) (lead's call, after the check). **Setup share (corrected after the check, T11):** at least half of the *shipped* M3 batch should be setup catches *(illustr.)*, because setup catches carry the "what to set up" decision. This is a CG3 check on the shipped batch, not a promise about the candidate list: the candidate list therefore adds Wishbone and Meeting Stone to the M3 unlock candidates (5.1), and if Second's Hairpin is rejected, Meeting Stone takes its setup place (14.2). If the batch that passes CG1 is still under half setup, the lead may ship it only with the shortfall told to the client at CG3 (lead's call, after the check).
5. **No flanking**, no party-wide catches (decision 7; rule 22), no numeric "underdog" triggers.
6. **Cross-location subjects (agreed, Q3).** Prone, Teetering and hazard squares are feet subjects (TK 5.5 Feet row, settled AR 13.4; agreed TK Q1 "pieces keep the approved location domains strictly"). Rule text: "*a charm may read an outcome event in another location's subject only to grant Warded or Helped, never to change how that subject resolves*". This is a change to agreed TK Q1 domains, not a reading. **The base batch stays domain-clean except Jolt Knot's danger branch.** Rejected for Q3: B, domain-clean catches only.

| Token | Outcome (player words) | Class | Actor | Repeat | Domain-clean? | Notes |
|---|---|---|---|---|---|---|
| `adjacentHostileDropped` | A hostile drops to 0 HP while adjacent to you | setup | any | recurring | Yes | Trophy Tally's existing trigger, unfiltered (rule 3); new records add `nonMinion` |
| `harmfulRebuffed` | A harmful condition fails to land on you: you pass its save, or your Warded blocks it | reactive | any | recurring | Yes | A gear prevention, a tag immunity or a condition already present is not a rebuff (4.3.1); merges the drafts' `savedAgainstHarmful` and `wardSpentOnYou` (each fails the floor alone, 8.1) |
| `forcedMoveStoppedEarly` | Forced movement you cause is stopped early (W 7.2 rule 4; VG-8) | setup | wearer | recurring | Yes | Best preview fit (W 7.2 rule 7); on a reaction, no forecast |
| `movedHostileEndsInDanger` | A hostile you moved ends in a harm or hazard square or Teetering | setup | wearer | recurring | **No (feet)**; legal under Q3 | F2+ only (RT-20, HZ-19); per-theme rate; `crossLocation` |
| `youFirstBloodied` | You first drop to half HP or below this fight | reactive | any | **once** | Yes | Bloodied is already on the plate (DH-20); never if the fight starts with you at or below half |
| `allyFirstBloodiedAdjacent` | An ally adjacent to you first drops to half HP this fight | reactive | any | recurring (once per ally) | Yes | Union over allies: rate risk (8.1) |
| `allyDownedNear` | An ally within 2 squares of you is Downed | reactive | any | recurring | Yes | Companions only (a hero downing ends the run) |
| `harmfulLandsOnYou` | The first harmful condition lands on you this fight | reactive | any | **once** | Yes | The Ashen Bead's Keep fill |
| `missedWhileDodging` | The first attack that misses you while you are Dodging | reactive | hostile | **once** | Yes | Registry check vs held "Dodge as bonus action" |
| `oppAttackHit` | Your opportunity attack hits | reactive | wearer (reaction) | recurring | Yes | Registry check vs Set verb and Vanguard zone features |
| `hitTwoOrMore` | One use of yours damages 2+ hostiles | setup | wearer | recurring | Yes | Arcanist-leaning; counted per use |
| `allyMovedFoeToYou` | An ally's forced movement leaves a hostile newly adjacent to you | setup | ally | recurring | Yes | Rate risk; hard preview |
| `hostileFrightenedAdjacent` | An adjacent hostile becomes Frightened | reactive | any | recurring | Yes | Herald Frighten registry check |
| `hostileProneAdjacent` | An adjacent hostile becomes Prone | setup | any | recurring | **No (feet)**; legal under Q3 | Shadow only (Topple-skewed); `crossLocation` |
| `allyKillAdjacent` | An adjacent ally drops a hostile that is adjacent to you | setup | ally | recurring | Yes | Feeds Second's Hairpin; narrowed after the check so that the receiver is adjacent (TK 6.1 item 6; ITEMS 19.1 rule 22; T8) |

#### 4.3.1 Exact predicates (lead's call, after the check)

The table above is player wording; this is the executable rule. Distances use the main design's grid (MAIN 4.3, decided: 8 directions, diagonal cost 1), so "adjacent" is Chebyshev distance 1 and "within 2 squares" is Chebyshev distance ≤ 2, read from positions at the moment of the core event. The **event unit** is one core event instance (each `HostileDropped`, each `ConditionApplication`, each `ForcedMove`), except `hitTwoOrMore`, which is one per use (TK 7.1, "one channel per use"). "First ... this fight" is counted from the fight-resources reset (4.6 item 7), never from a combat start in a Sanctum re-engagement.

| Token | Core event and fields | State tested |
|---|---|---|
| `adjacentHostileDropped` | `HpChanged{unit: hostile, oldHp > 0, newHp = 0, cause: damage}` | Wearer at Chebyshev 1 from the hostile at the event; record filters applied (none for Tally) |
| `harmfulRebuffed` | `ConditionResisted{target: wearer, condition.harmful, reason}` | `reason ∈ {savePassed, wardBlocked}`. Excluded: `immune` (creature and BR-20 tags), `gearPrevented` (TK 7.3, AR-51), `alreadyPresent`, and an application that never reached the save or ward step. A save-less auto-apply qualifies only by `wardBlocked`. A rider that applies two conditions on one save is one event |
| `forcedMoveStoppedEarly` | `ForcedMove{source: wearer, moved < requested, stopReason ∈ {piece, unit, wall, edge}}` | A combined forced move (W 7.2 rule 4) counts if the wearer is a contributing source |
| `movedHostileEndsInDanger` | `ForcedMove{source: wearer, target: hostile}` resolved | The hostile's final square is a harm or hazard square, or it is Teetering, after the move's own resolution |
| `youFirstBloodied` | `HpChanged{unit: wearer, oldHp > max/2, newHp ≤ max/2}` | The first such event since the reset; never if HP ≤ max/2 at the reset or at the fight's first combat start; healing above half and dropping again does not qualify again |
| `allyFirstBloodiedAdjacent` | `HpChanged{unit: ally, oldHp > max/2, newHp ≤ max/2}` | That ally's first such event since the reset; that ally at Chebyshev 1 from the wearer at the event; an ally bloodied at the fight's start never qualifies |
| `allyDownedNear` | `DownedApplied{unit: ally companion}` | That ally at Chebyshev ≤ 2 from the wearer at the event |
| `harmfulLandsOnYou` | `ConditionApplied{target: wearer, condition.harmful, new: true}` | The first since the reset; a refresh of a condition already present is not "new" |
| `missedWhileDodging` | `AttackResolved{target: wearer, result: miss}` | Wearer has Dodging at W 7.5 step 6; the first since the reset |
| `oppAttackHit` | `AttackResolved{attacker: wearer, kind: opportunity, result: hit}` | — |
| `hitTwoOrMore` | `UseResolved{user: wearer}` | ≥ 2 distinct hostiles took damage in that use |
| `allyMovedFoeToYou` | `ForcedMove{source: ally, target: hostile}` resolved | The hostile was not at Chebyshev 1 from the wearer before the move and is after it |
| `hostileFrightenedAdjacent` | `ConditionApplied{target: hostile, condition: Frightened, new: true}` | Hostile at Chebyshev 1 from the wearer at the event |
| `hostileProneAdjacent` | `ConditionApplied{target: hostile, condition: Prone, new: true}` | Hostile at Chebyshev 1 from the wearer at the event |
| `allyKillAdjacent` | `HpChanged{unit: hostile, oldHp > 0, newHp = 0, cause: damage, attacker: ally}` | Both the hostile and the attacking ally at Chebyshev 1 from the wearer at the event; record filters applied |

Event and field names are illustrative; the builders map them to the core event log (TK 17). **Repeat** is part of the vocabulary entry: a `once` token can qualify at most one event per fight per wearer; a `recurring` token can qualify again (for `allyFirstBloodiedAdjacent`, once per ally). It decides whether a blocked catch waits or is missed (4.5).

**Rejected triggers (told plainly):** flanking; weapon-verb, class or item ids; `combatStart`, `roundN`; Listen, stealth, Search, traps; crits (head's subject, too rare); hazard-edge saves (feet); escape or stand (hands, feet); kill at range (the Herald "Helped at range" lens, HR-33); `attackMissedYou` as written (always-fires, 8.1); "an opportunity attack against you misses" (negative-EV provoking teaches bad play; sits on the `missedBy` reaction hook used by Riposte and Parry, ABL 7.3); `pressedByTwo` (near fight-start ward for the Vanguard); `endMoveBesideDowned` (fails the floor); `furnishingStrike` (too rare).

**Dropped argument (told plainly):** the drafts rejected Prone-keyed Helped because it "idles". That is wrong: Helped sits on the wearer (CON 18.9), a roll that already has advantage does not use it (W 7.5 pipeline step 2, "consume Helped if it applies"), so it carries to a later attack. Prone-keyed catches are held back for the Topple-skew reason (and, before Q3, the domain reason), not that one.

### 4.4 Outputs (change CA-4; request CA-23)

- **M2–M4:** Warded and Helped only (TK 6.1 item 6), to you or one adjacent ally, under every TK 7.1 cap.
- **Never a trinket output, in any lane (change CA-4, a narrowing):** Spurred (CON 18.4 closed list; CL-5; TK 13.9 Spurred boots), Rallied, Steeled, Unyielding, Dodging, Hidden and Veiled (back), Revealed (head), Shielded (neck; Bulwark Locket), Empowered (a ladder step is added damage, TK 6.2), Bolstered, Blurred, Death-warded (DH-22, DH-47), healing, temporary HP.
- **Spotted (request CA-23):** the one output that changes what the party does ("the next attack roll against it has advantage", CON 18.9). Ask the CON owner to rule Spotted for **M4** rather than Later, adjacent-sourced only, behind the Herald "too required" lens (HR 5.3) and the carrier rule (a type holds ≤2 harmful conditions). Fallback: Later, as now. Until it is ruled, expect many charms to read "you gain Helped" (told plainly). **After the check (T14):** a charm that applies Spotted to a hostile also needs a new effect kind in TK 6.1 item 6 (today Warded or Helped, to you or an adjacent ally), a change to the None lane's domain words in TK 5.5 ("Warded and Helped", agreed TK Q1) and to TK 17.2's domain map, so CA-23 is approved by the CON owner **and then the client** (agreed TK Q1 lane).
- **Inspired (corrected after the check, T13):** stays with the Leadership-mastery owner (Muse's Token held). TK 5.5's None row lists "save rerolls (Inspired, Later)" under Never here, and TK 6.1 item 6 allows only Warded and Helped, so an Inspired charm would change agreed TK Q1 lane text: it returns to the client as a question, never as a lead's adoption.
- **Ally-target Helped catch (told plainly; lead's ruling pending):** the Help-whistle was cut as a *button* duplicate of Sigil of Aid. A *catch* that hands Helped to the adjacent ally whose kill set it up is not a chosen Help action. The lead proposes that it passes the registry; Second's Hairpin (5.1) is held until the lead rules, before shadow results are read (11). Its receiver is always an adjacent ally, `target: adjacentAlly` (TK 6.1 item 6; ITEMS 19.1 rule 22), never a ranged killer (corrected after the check, T8).

### 4.5 Values, pools and policies (change CA-7, CA-8)

- **Catch:** `fight` 1, minor, value 1 (TK 8.1; TK-18).
- **Self-Warded Catch (new value rule, *illustr.*):** keeps value 1 only if its simulated fire rate is ≤600‰ **in elite and in boss fights, read separately**, not on average (Bloodknot averages ~463‰ but fires ~910‰ in hard fights, 8.1). Above that it is a de facto fight-start ward and becomes a `rest`-charged line. **Bloodknot already fails this test on the toy** (about 900‰ in elite and 950‰ in boss fights, 8.1), so it is shadowed and gated from M2 in its `rest`-charged form: `rest` 1/1/2, automatic, value 1, like Wardstone's pool (5.1 row 8; corrected after the check, lead's call, after the check; T6). Any other self-Warded catch that fails the per-class test is shadowed and gated in its `rest`-charged form, never in its `fight` form.
- **Two-branch catches:** two `charmTriggers` tokens, one output, once per fight, are one value-1 property (the Visor exception of TK 8.1 extended as a data shape; CA-7, after the check), provided the merged rate passes CG1 (Jolt Knot, Minder's Pebble).
- **Ally-Warded Catch or Keep:** a major: `rest` 1/1/1, value 2, Uncommon, reaction-shaped off-turn (ReactionCap; TK-3) (ABL 6.2, settled).
- **Spend:** unchanged (Wardstone `rest` 1/1/2; Ward-Knot 1/1/1).
- **Sanctum (BR-31; BR 4.2.5):** once per fight = once per boss encounter, because every charm per-fight state follows the one-fight rule (4.6 item 7). The card does not print it; Inspect and the Sanctum fight's card note do (4.2).
- **Output duration (lead's call, after the check):** a charm-caught Helped is recorded on the output with `untilSourceTurnEnd`, the wearer as source (as Trophy Tally and the Vex verb, W-7), and a Helped caught during the wearer's own turn lasts until the end of the wearer's **next** turn, stated on the record itself. CA-9 then only confirms that this matches CON 5.6; a refusal of the reading cannot break the setup catches (14.2).
- **Policies (change CA-8):**
  - Helped catches: `always` only. No "ask me": it would prompt mid-hostile-turn for a token that costs nothing to take.
  - Warded catches: `always`, or `spareForButton` set at rest ("does not fire while this unit holds a ward-button charge"), so Bloodknot never eats the ward a player is saving for the boss. No mid-turn prompt.
  - Ally-target catches and majors **(corrected after the check, lead's call, after the check):** `always`, with the receiver fixed by the trigger (the bloodied adjacent ally for Walnut; the adjacent killing ally for Second's Hairpin). No reaction prompt ever, so the anti-goal "mid-hostile-turn prompts" (0.4) holds; ReactionCap (TK-3) still applies off-turn. The ABL 7.3 reaction-prompt path is not used by any charm.
  - The policy choice is per (unit, property), set at rests only, and is **saved state**: in the save, the suspend journal and replays (CA-11).
- **Blocked catches (lead's call; made exact after the check, lead's call, after the check; T9):** every catch inherits Trophy Tally's rule (TK 13.7, "Not spent if you already hold Helped or have had a trinket Helped this round"): a blocked catch is **not spent**. What happens next depends on whether the block can lift this fight:
  - **Defers** (`onBlocked: defer`; shown to the player as "waits"): no grant, no spend, the charm stays armed for the **next** qualifying event. Used only when the token is `recurring` and the block can lift this fight: `TrinketHelpedLimit` (per round), "already holds Helped", `AlreadyWarded`, `spareForButton` while a ward-button charge is held, or `CharmFireOnce` losing to another charm. Plate: the waiting state (6); log "Hangnail waits: Ysolde already holds Helped."
  - **Missed:** no grant, no spend, and the charm is done for this fight. Used when the token is `once` and its one event has passed, or when the block cannot lift this fight: the per-output cap (`CharmCaughtThisFight`) or `AlreadyWardedThisFight`. Plate: the spent state; log "Bloodknot missed: Bram already had a trinket ward this fight." A missed charm never pulses.
  - **Wasted** is reserved for a granted Helped or Warded that expires unconsumed; it is a report and a log line, never a forecast or a fire outcome. A Helped held on a roll that already has advantage is not wasted: it is not consumed and carries over (W 7.5 step 2; 4.3).

### 4.6 New runtime limits (change CA-5 ⚑; agreed, Q2)

These add rows to TK 7.1, which TK 24.2 places in the fixed frame; the client chose all three (agreed, Q2).

1. **`CharmFireOnce`:** when one event qualifies two charms on the same unit, only one fires; one floater per event. **Order (made exact after the check, lead's call, after the check; T9):** the candidates are taken with `once` tokens first, then `recurring` ones, and in slot order within each; a candidate that is blocked (4.5) is skipped; the first unblocked candidate fires. Every other qualifying candidate on that unit defers if its token is `recurring` and is missed if it is `once`. Putting `once` tokens first means a charm whose only moment is now (Bloodknot, the Ashen Bead's fill) is not lost to a charm that can fire again later.
2. **No-chain:** an event *caused by a trinket output* (a ward applied, a channel's rider landing) never fills or fires a charm. `harmfulRebuffed` is legal: the hostile's application is the event, the ward only answers it. **Provenance (lead's call, after the check):** an event is trinket-caused only if the effect resolution that produced it has a trinket property as its **root source** (a `TrinketPropertyFired` root in the core event log). Modifiers never taint: advantage from a Helped (charm-caught or not), a channel's damage-type shift, and a ward answering a hostile's application leave the event player- or hostile-caused. A combined forced move (W 7.2 rule 4) is player-caused if any non-trinket part contributes.
3. **One caught token per output per receiving unit per fight:** a unit **receives** at most one Helped and at most one Warded from charm catches per fight (Warded is already capped by `AlreadyWardedThisFight`). The flags are keyed on the receiver, `charmCaughtHelpedThisFight[unit]` and `charmCaughtWardedThisFight[unit]`, because the reason is stacking on the receiver (lead's call, after the check). Only a Catch grant sets them; a Keep fill sets nothing; Keep and Spend grants are governed by `AlreadyWardedThisFight` and `TrinketHelpedLimit` only. Reason: two Helped catches on an Exploit Shade give ~+9.4% *(illustr.)*, at the +10% precision ceiling, and neither `CharmFireOnce` nor `TrinketHelpedLimit` (per round) stops it. Greyed reason key `CharmCaughtThisFight`, shown as "Already gained a Helped from a charm this fight" (5.4). The cap counts catches from every charm, the Bobbin's charm included (new lead's call; 4.11). **So two Helped catches on one unit share one catch a fight** (told plainly after the check, T10; the equip-time verdict of 4.7).

**How catches resolve (lead's call, after the check; T15, T16).** These rules make the three limits deterministic; they add no limit.

4. **After the use.** Qualifying events are queued, and catches resolve only after the triggering use is complete: after W 7.5 step 15 (Events) for a weapon attack, after ABL 14.3 step 6 for an ability use, after the full conditions pipeline and the gear-prevention order (TK 7.3) for a stand-alone application (a hazard, a stand-alone condition), and after each reaction (an opportunity attack, a `missedBy` or `attackAgainstOther` reaction attack, ABL 7.3) as its own use. **A token never affects the rest of the use that caused it:** a Bloodknot Warded gained when step 8's damage bloodies you does not block step 9's rider of the same hit; a Helped from a drop at step 8 or a stop at step 10 does not advantage the step-12 Cleave or Skewer follow-up.
5. **Order.** Queued catches resolve by core event sequence number (the core event log's order; "event order" means this), then ascending unit order of the wearers (the TK 7.3 step-3 convention), then the `CharmFireOnce` order of item 1. Shared caps (`TrinketHelpedLimit`, `AlreadyWardedThisFight`, ReactionCap, the per-output flags) are read and written in that order, so one `HostileDropped` qualifying Trophy Tally on two adjacent wearers, or one application qualifying one unit's Hangnail and an adjacent ally's Walnut, always resolves the same way.
6. **Dormant and newly revealed lines.** A rolled catch or Keep line revealed mid-fight (4.8) stays inert until the next fight-resources reset: no ready pip, no place in the `CharmFireOnce` order, cannot fire (TK 7.2's deferred recompute).
7. **One fight in the Sanctum.** Every charm per-fight state (`fightUses`, the per-output flags, `charmKept`, the defer state, and TK's `trinketWardedThisFight`) is reset only by the **fight-resources reset event** of BR 4.2.5: a combat end outside the Sanctum, or `BossDefeated`; never by a combat start or a Sanctum combat end while `sanctumEngaged` is set and the boss lives. A Keep fill expires on the same event.

**Rejected for Q2 (agreed):** B, `CharmFireOnce` and no-chain only, with Helped stacking managed through the precision report; C, no new limits.

**Not adopted:** a per-round charm cap (`CharmTriggerCap`), held as a report-triggered fallback for the client if CG2's plate-read or two-charm reports demand it (14.2). **The DeadLine warning** stays a lead's call (TK-10 already defines DeadLine as non-suppressing): it now also runs at **party scope** at rests ("Bloodknot on Bram will use his trinket ward before Ward-Knot can").

### 4.7 Readability, preview and plate (change CA-6, CA-10 ⚑)

- **Read count (request to CL; change CA-10 ⚑, settled CL 7.6 / ABL 11.4; corrected after the check, T2):**
  - Under CL 7.6's current text a Catch counts **1**: it is not "a pool-free reaction" (it has a `fight` pool), and its passive bullet counts 0 only for a passive that marks the battlefield, which a reactive catch does not. So the approved Trophy Tally already puts a Vanguard at 5 today. CA-10 part 1 asks the CL owner for a new bullet: **"a once-per-fight automatic trinket catch with a plate ready pip counts 0"** (setup catches also show in the forecast). Fallback if refused: catches count 1, and the "rarely fires for this unit" and Vanguard read verdicts cover it.
  - CA-10 part 2: a trinket **button** (Spend or Keep). Options for the CL owner: **(a, recommended)** a new CL 7.6 bullet: an **items row** (belt consumables plus the one trinket button) sits *outside* the four-per-unit class count, checked by the companion <10 s gate; (b) the button counts 1, with a Vanguard warning verdict; (c) accept 5. (The drafts' "shares a row with belt items, counting 1 together" still adds 1, because CL 7.6 counts no belt items today; told plainly.) Fallback if the CL owner does not rule before M2 UI: option (b).
- **Preview split (lead's call; made exact after the check, lead's call, after the check):**
  - Every record has a `plateHint` (TK 9.1's mandatory previewHint, serialized under that name); setup catches add a `forecastHint`; reactive catches never declare one (field table in 9.2).
  - **forecastHint:** shown in the move or attack forecast of **any party unit's** previewed action, for every party wearer's setup catch that is ready and whose predicate (4.3.1) some roll outcome of that action could satisfy. It is phrased conditionally and names the outcome ("If it drops → Trophy Tally: Helped"; "If the shove stops short → Jolt Knot: Helped"). It is computed only through `IKnowledgeQuery` and party knowledge (XI), using the same landing square as the W 7.2 rule 7 preview, so an unseen blocker or an unrevealed hazard square never produces a hint (XI `KnowledgeLeak` cases, added to V23t). A forced move made on a reaction shows no forecast.
  - Defer and missed status in the forecast are evaluated against the current cap state, assuming the caught token: the hint then reads "→ waits" or "→ missed" as a whole string (6). The forecast never says "wasted" (4.5).
  - **plateHint:** the pip's Inspect text ("fires when a harmful condition fails to land on you"), **never on a hostile's intent telegraph** (no leaked roll information, XI).
- **Plate (corrected after the check; 6):** a unit's charms show as **one** plate mark, a cluster of two pips, so they cost one of XI's two plate marks; pip states ready, spent, kept, waiting and dormant (no pip) are told apart by shape and pattern, never hue alone (RC4; RS 7.4 greyscale and colour-blind gates); motion is an extra cue only; Inspect text for every state (XI `NoHoverOnly`).
- **Equip-time verdicts (non-suppressing, change CA-6):**
  - "rarely fires for this unit", read from the fire-rate row for the unit's class or companion role, the **current party size** and the **current floor band** (F1, F2+; 8.1): Tally on an Arcanist is silent ~79% of fights *(illustr.)*; Jolt Knot on a Herald on F1.
  - "no ally to receive this" in a solo party.
  - **"Shares its Helped with <charm>: only one can fire a fight"** (lead's call, after the check; T10), when a unit would wear two catches with the same output; shown at rests and in the Bobbin picker; with a party-scope suggestion to move one to a companion.
  - Party-scope DeadLine.
- **Spend-side feedback:** when a charm-caught Helped is spent, the log shows provenance ("Advantage (Jolt Knot)"), so the payoff shows when it happens.
- **First sight:** a `uiMemory` callout on the first charm pickup, first catch fire and first Keep fill (XI 4.10). The first-charm callout also fires on the Bobbin pick if it has not fired yet (after the check).

### 4.8 Rarity, rolled lines and dormancy (change CA-24 ⚑; agreed, Q5)

- **Common:** one Catch line, or one self Spend line.
- **Uncommon:** one major (ally Warded Catch, Keep or Spend), or the obvious line plus one rolled line from `props.trinket.charm`. Catch lines join that table, which fixes CC-4 (Wardstone's legal roll set leaves 1) and allows "Wardstone of the Jolt" (IR-9 epithet, CC-55).
- **Rule 28 extended:** no second button; no second line with the same (trigger, output) on one charm. **After the check (lead's call, after the check):** a rolled catch line never has the same output as the item's obvious line (no Tally line on a Hangnail), and a Warded catch line never rolls onto a ward-button charm (no Bloodknot line on a Wardstone), because the per-output cap and `AlreadyWardedThisFight` would almost always block the second line, a dead property (AR 17.1 rule 14). V23s checks it.
- **Dormant lines (lead's call; ITEMS 11):** a rolled catch or Keep line may be the item's one dormant property. A dormant line has no ready pip, no hint, no slot-order place, is ignored by `CharmFireOnce` and every verdict. A line revealed mid-fight shows at once on the card but stays inert until the next fight-resources reset (4.6 item 6; TK 7.2's deferred recompute) (after the check). The card shows "? dormant · Arcana". Dormant time is excluded from fire-rate reports and reported separately. The obvious line is never a dormant catch (rule 28).
- **Rare: TK Q2 stays closed (agreed, Q5).** Two None slots would let a unit stack two Rares (TK Q2's own reason); HL-26 needs no Rare curse host; a "small" lane needs no value 3. If the client ever reopens TK Q2, it comes with `RareWornCap` 1 per unit. **Rejected for Q5 (agreed):** B, Rare charms with `RareWornCap` 1; C, a charm string outside the slots.

### 4.9 Count, companions and monsters

- **Count (agreed, Q5):** up to 2 charms per unit (None count 2; AR 5.4). No slot change; no string. ButtonCap 1.
- **Companions (lead's call):** the best Catch wearers (no presses, trinkets are their build lever, TK 11 item 9; no kit trinket, CMP 8.4). Request CA-22: the party scorer values an expected catch at its preview value, so suggestions position companions to trigger charms. Reports: companion vs hero fire rate; median companion turn time with charms.
- **Monsters:** new families are container-only in their first wave (as Tally, TK 12); carried eligibility needs the ENM 6.4 guard rerun and rule 25. No monster ever carries a Keep.

### 4.10 Sources and volume (hook; change CA-21, CA-25)

- **Channels unchanged:** containers, carried (existing buttons only), camp channel, boss channel, LootPlan Due and offers; the camp **notables shop** (ITEMS 10.4) is a live charm channel, and catch charms are priced by the existing value-to-price rule (read); theme "Answers here" display weights; `requiresBias` ×3 for "requires a ward source" leans.
- **Never:** boss signatures (agreed TK Q2; TK 22); trader sales (TK 19 Later); event grants (EV 4.4.8, `NoEventLoot`; the drafts' "Curiosity offers a charm" and "event bead" lines are struck).
- **Unlocks (change CA-21, request to SV):** new families enter as M3 content under MAIN 4.7 bullet 1, named in "New in the dream" (e.g. "win with a companion who was Downed" unlocks Minder's Pebble). **Two families are base content at M3** (Hangnail, Jolt Knot), so a new profile's charm pool is not two-thirds buttons. Charm unlocks change a generation pool, so they are `unlockHash` and `drawableSet` inputs (SV 4.6), unlike heirloom unlocks (HL-11); the V5 every-subset cost note (SV SQ41) applies. **Hub object (reading, lead's call, after the check):** charm-family unlocks use the Library's generic loot-unlock object (CH 4.11), which is not a charm hub surface under CH-24, so V20s `UnlockHasHubObject` passes and no charm shelf appears (CA-21).
- **Volume:** see F11 and 8.6. The lane-weight question (IR 4.2; LE 6.2) is a **global** change inside the unchanged trinket VP share, never theme-keyed (agreed IR Q4), owned by IR/LE with the client; it is a future item triggered by CG1/CG2 (14.2). A third of runs may see no charm *(illustr.)*; this is an economy question for later, not a reason to add slots (told plainly).

### 4.11 The charm boon: the Bobbin (change CA-20 ⚑; agreed, Q4)

**Correction (told plainly).** All four drafts and all three judges declined a charm boon because it is "loot outside the per-floor budget". That is not a blocker: the approved boon example in MAIN 4.7 is an heirloom, and heirloom boons are Uncommon items outside the floor loot budget with display-only value (ITEMS 14; HL 4.1.4). HL 4.6 forbids charm *heirlooms*, not a charm boon. CH-24 and TK 24.2 leave a boon type open.

**The honest trade-off.** A charm boon: (+) fixes visibility for the player who wants the mechanic (P(0 charms) ≈ 33% *(illustr.)*); (+) adds variety (choice among unlocked catches), which MAIN 4.7 boons are for; (−) competes for the one boon with heirlooms, kit variants and Old Friend; (−) needs its own parity report (as the M4 amulet heirlooms, HL-26); (−) could become the default boon.

**Rule (agreed, Q4):**
- **Bobbin** *(placeholder name, 0 hits; the agreed "Underpillow" renamed after the check because "pillow" names the Heart's dust-pillow Keg and the under-pillow motif implies the Dreamer's age, RS 7.5; T4)*: at run start, as the one starting boon, choose one **unlocked Common Catch** charm. Value 1, never a button, never a Keep, never Uncommon, never an heirloom, never persistent (a fresh instance each run; no Grip-marks), never cursed.
- **Earned (lead's call, after the check; T4):** MAIN 4.7 (decided) says the player picks "a single earned boon", so the Bobbin is itself unlocked by a milestone tied to the mechanic: **"a charm fired in a won fight"**, named in "New in the dream" (CH 4.11 Library object). A new profile therefore meets a catch firing before it can choose one. The choice list inside it is unchanged by this.
- **The choice list (new lead's call; 2.3 F16):** Trophy Tally, the M3 base families (Hangnail, Jolt Knot) and every unlocked Common Catch family that is shipped in the profile's content set. Never a `simOnly` record, a HOLD row, a Spend line or an Uncommon major. The list therefore never has fewer than three entries at M3.
- **Who and where (lead's call, after the check; CA-26):** the charm goes to the **hero**, issued at WO1 through SK-3 into the hero's first empty None slot with no displacement (kits carry no trinket, V21 `KitNoTrinket`, so both None slots are empty; V23s asserts it). The unit and slot are recorded in `boons[]` with the family id. This needs an SK 7 item 2 worn-slot row (SK-12) and a V21 `KitNoTrinket` waiver limited to the Bobbin instance (HL 4.6, HL-26 pattern), requested from the SK owner. A Bobbin boon run draws the default kit's Oddment pool (SK 4.5.2); the boon carries none of its own.
- **The seed key (new lead's call):** the chosen family id enters `configHash` as the boon choice, as an heirloom template id does (HL 0.4 G5; SV 4.6); the instance carries no rolled line, so nothing else enters the key. The launcher validates the family id against the run's `drawableSet` before WO1 (after the check).
- **Shared, seeded and daily runs (lead's call, after the check):** the Bobbin is **off**: those runs use canonical grants and get no heirloom (HL 4.1.7), and a family unlocked on one profile may not be unlocked on another.
- **The picker (lead's call, after the check):** offered in the launcher's or Begin again's boon picker (CH 4.9, 4.10), with no shelf object (CH-24). It shows each family as its one-line card in a list layout (as SK does for Oddment names), with the "rarely fires for this unit" verdict for the chosen hero class on F1 and on F2+ ("Jolt Knot: rarely fires for a Herald on the first floor").
- **The instance (new lead's call):** treated like an heirloom instance for economy only, but flagged `boonCharm` and **never** `heirloom` (lead's call, after the check): outside the floor loot budget, value for display only, unsellable, never a Curiosity's "Item offered" price (as HL-4), lost at the stairs if left in leftovers. It is an ordinary charm in every rule (caps, `CharmFireOnce`, the per-output cap of 4.6, `DuplicateAnswer` identity keys, ReactionCap).
- **The Dream's Due (new lead's call; 2.3 F15):** the Bobbin charm counts as party-owned for the Due's owned and duplicate checks (ITEMS 14; LE 6.12), but it **never satisfies the F1 hero-arms Due and never triggers the HL-3 waiver** (a charm is not arms). This replaces the proposal's "counted as owned for the F1 Due (as the HL-3 heirloom waiver pattern)". A V13g (g) case asserts that a `boonCharm` instance never sets `armsSatisfied` (CA-26; after the check).
- Counted in the LE 6.5 parity report against the heirloom boon (an Uncommon); a Common catch is strictly below an heirloom's value.
- **Gates:** boon pick share ≤40% of boon choices *(illustr.)*, and a pick-share **floor** report, ≥5% of boon choices among profiles that have unlocked it *(illustr.; after the check)*, both read from human sessions and telemetry (the SV human instrument, 9.4), never from the simulator; win-rate delta ≤ the heirloom `HeirloomWinDelta` cap (+3 points), read in V23r against a heirloom-boon arm (9.3). If it is under-picked, the lead withdraws it or changes its presentation (placement, fit verdict), never its rarity or value (14.2).
- M3, with the unlock batch. Request to the MAIN/CH owners (CH 2.2 hook 32; one-boon rule).
- **Rejected for Q4 (agreed):** B, no charm boon, with visibility from base families and the lane-weight question only.

### 4.12 Model C, recorded (lead's call; agreed, Q5)

C-add is rejected (TK 6.2 slot-grant row; AR 5.3 count 2; per-unit power outside the budget). C-slot, the charm string, is held as a **future M4 item** and an **M4 evaluation brief** only, if CG2 and the CG3 bind-rate test pass: one string per unit (None count 1 for charms), 2–3 beads by knots, a 3-knot string counted under `RareWornCap` 1, "a moment I spend or moments I catch" as the choice. Its volume need (8–12 beads per run vs ~1.1 finds) makes it infeasible without a lane-weight change. It returns to the client only through 14.2.

---

## 5. Catalogue

All names are placeholders pending the naming pass; every name was grepped (A.2). Catch lines are `fight` 1, minor, value 1, self output, Common, unless stated. The hint column gives the `plateHint` (TK 9.1's mandatory previewHint) and, for setup catches, the `forecastHint` (field table in 9.2; corrected after the check). Every entry is checked against the denylist, the caps, the one-table rule and zero added damage; it outputs only charm-lane tokens and adds no damage (Helped feeds Exploit: precision ceiling report). Statuses are updated for the client's answers (Q1–Q5) and for the design check (objects, renames, Bloodknot's pool, Second's target, two added M3 candidates).

### 5.1 Approved roster and new candidates

| # | Name | Object (silhouette class) | Rarity | Shape | Rule | Pool / value | plateHint / forecastHint | Favours | Wave | Status |
|---|---|---|---|---|---|---|---|---|---|---|
| 1 | **Wardstone** | Approved (TK 15) | C | Spend | Bonus action, combat only: you gain Warded(1) | `rest` 1/1/2 · v1 | "Bonus action: you gain Warded" | All | M2 | **KEEP** (settled). If CG2's spend share is low, supersede (new id, `supersededBy`) with a Bloodknot-style catch |
| 2 | **Ward-Knot** | Approved (TK 15) | U major | Spend | One adjacent ally gains Warded(1) | `rest` 1/1/1 · v2 | "Bonus action: an adjacent ally gains Warded" | Support, companions | M2 | **KEEP** (settled) |
| 3 | **Trophy Tally** | Approved (TK 15) | C | Catch (setup) | `adjacentHostileDropped` → Helped (unfiltered, as approved) | `fight` · v1 | plate "A hostile drops beside you → Helped"; forecast "If it drops → Trophy Tally: Helped" | Front line, melee companions | M2 | **KEEP** (settled; no minion filter, 4.3 rule 3); card notes "front line"; plate gets ready/spent states; on the Bobbin list |
| 4 | **Hangnail** | Nail paring (crescent) | C | Catch (reactive) | `harmfulRebuffed` → Helped: a harmful condition fails to land on you (save passed, or Warded blocks it) | `fight` · v1 | plate "Save passed or ward blocks → Helped" | Front line, high-save builds; plays with Wardstone, Ward-Knot, Graces | M3 base | **ADOPT** (merged from the drafts' Spite Knot/Scab Knot; domain-clean); on the Bobbin list |
| 5 | **Jolt Knot** | Knotted cord (knot) | C | Catch (setup) | `forcedMoveStoppedEarly` **or** `movedHostileEndsInDanger` → Helped (two-branch, one value-1 property, CA-7) | `fight` · v1 | plate "Your forced move stops short, or ends a foe in danger → Helped"; forecast "If the shove stops short → Jolt Knot: Helped" | Vanguard, Arcanist (Gust), anyone who Shoves | M3 base | **ADOPT** (agreed, Q3: both branches; the danger branch is the base batch's one cross-location read; F2+ only); gated on its F2+ rate reading, with an F1 "rarely fires" verdict (8.1; T17); on the Bobbin list |
| 6 | **Minder's Pebble** | Smooth pebble (stone) | C | Catch (reactive) | `allyDownedNear` **or** `allyFirstBloodiedAdjacent` → Helped | `fight` · v1 | plate "An ally beside you is bloodied, or one within 2 is Downed → Helped" | Companion "minders" | M3 unlock candidate | **HOLD** for CG1: union over allies may always-fire; shadow two variants (adjacent-only bloodied; Downed-only, `anyCompanion`). Renamed from Widow's Knot (gendered; implied a death, RC9; T5) |
| 7 | **Second's Hairpin** | Hairpin (pin) | C | Catch (setup) | `allyKillAdjacent` → **that adjacent ally** gains Helped (`target: adjacentAlly`) | `fight` · v1 | plate "An adjacent ally drops a foe beside you → they gain Helped"; forecast "If it drops → Second's Hairpin: <ally> gains Helped" | Support, companions | M3 unlock candidate | **HOLD** for the lead's ally-Helped ruling (4.4) and ReactionCap shaping; narrowed after the check so the receiver is adjacent (TK 6.1 item 6; ITEMS 19.1 rule 22; T8); the monotony answer; if rejected, Meeting Stone takes its setup place |
| 8 | **Bloodknot** | Knotted lock of hair (hair) | C | Catch (reactive) | `youFirstBloodied` → Warded | **`rest` 1/1/2, automatic · v1** (after the check; T6) | plate "You drop to half HP → Warded" | Front line | M3 unlock candidate | **HOLD** for CG1 (envelope; the per-fight-class test already fails in its `fight` form, so it is shadowed only as `rest`-charged, 4.5); `spareForButton` policy; party DeadLine with Wardstone/Ward-Knot (approved Later entry, TK 13.7) |
| 9 | **Walnut** | Walnut shell (nut) | U major | Catch (reactive, off-turn) | `allyFirstBloodiedAdjacent` → that ally gains Warded | `rest` 1/1/1 · v2 | plate "An adjacent ally drops to half HP → they gain Warded" | Companions, support | M3 unlock candidate | **HOLD** for CG1 envelope hero row; ReactionCap; policy `always`, receiver fixed by the trigger, never a prompt (4.5); Kinward (neck) prevents neck conditions, Warded is charm-owned, so the one-table rule holds. Renamed from Swaddle Knot (implied the Dreamer's age, RS 7.5; T5) |
| 10 | **Ashen Bead** | Cinder bead (bead) | U major | Keep | `harmfulLandsOnYou` fills it; bonus action: one adjacent ally gains Warded | `rest` 1/1/1 · v2 | plate "A harmful condition lands on you → fills; bonus action: an adjacent ally gains Warded" | Front line handing protection on | M3 if CG2, else M4 | **HOLD** for CG2 (agreed, Q1: the one gated Keep record); renamed from Lodestone (XI-13 collision) and then from Ashen Knot (silhouettes, T5); `charmKept` state; Keep mechanics of 4.2 |
| 11 | **Twitch Bone** | Small bone (bone) | C | Catch (reactive) | `missedWhileDodging` → Helped | `fight` · v1 | plate "An attack misses you while you Dodge → Helped" | Anyone who Dodges | M4 | **HOLD**: Dodge-rate check; registry vs held "Dodge as bonus action" (renamed from Duck Knot: Duck is a CON/ABL step) |
| 12 | **Lockjaw** | Jawbone (jaw) | C | Catch (reactive) | `oppAttackHit` → Helped | `fight` · v1 | plate "Your opportunity attack hits → Helped" | Zone-control front line | M4 | **HOLD**: registry vs Set verb, Vanguard zone features; one-class lean |
| 13 | **Wishbone** | Wishbone (fork) | C | Catch (setup) | `hitTwoOrMore` → Helped | `fight` · v1 | plate "One use damages 2+ foes → Helped"; forecast "If it hits 2+ → Wishbone: Helped" | Arcanist, Cleave users | M3 unlock candidate (after the check; T11) | **HOLD**: Arcanist lean; precision ceiling; must pass rule 26 on the rate report through a non-Arcanist class or `anyCompanion` (8.1) |
| 14 | **Meeting Stone** | Holed stone (ring) | C | Catch (setup) | `allyMovedFoeToYou` → Helped | `fight` · v1 | plate "An ally's forced move brings a foe beside you → Helped"; forecast "If it lands beside <wearer> → Meeting Stone: Helped" | Team setups | M3 unlock candidate (after the check; T11) | **HOLD**: rate; preview difficulty; Second's Hairpin's setup replacement if that row is rejected |
| 15 | **Shy Stone** | Split stone (split stone) | C | Catch (reactive) | `hostileFrightenedAdjacent` → Helped | `fight` · v1 | plate "An adjacent foe becomes Frightened → Helped" | Herald parties | M4 | **HOLD**: Herald Frighten registry check. Renamed from Calm Stone (Calm Emotions, the Herald's Calm Minds; T5) |
| 16 | **Quietus Knot** | — | C | Catch (setup) | First non-minion kill → Warded | `fight` · v1 | plate "Your first kill → Warded" | All | — | **HOLD**: near always-fires; Tally's ward twin; registry vs No Witnesses, Mourners |
| 17 | **Relay Thread** | — | C | Shaper | Once per fight, an unused trinket Helped you would lose passes to an adjacent ally | — · v1 | plate "An unspent trinket Helped → an adjacent ally" | Companions | Later | **HOLD**: needs effect kind 15; receiver's `TrinketHelpedLimit` applies. Renamed from Hand-me-down Knot (age imagery; T5) |
| 18 | **Stay Thread** | — | C | Shaper | Your trinket Warded lasts to fight end | — · v1 | plate "Your trinket Warded lasts to fight end" | Front line | Later | **HOLD**: Warded's fallback duration is unverified (lead confirms). Renamed from Cradle Knot (age imagery; T5) |
| 19 | **Whiff-Mark** | — | C | Catch (reactive) | First adjacent hostile whose attack misses you becomes Spotted | `fight` · v1 | plate "An adjacent foe misses you → it is Spotted" | Tanks | M4/Later | **HOLD**: Spotted ruling (CA-23, then the client); counts toward a carrier's 2 harmful conditions |
| 20 | **Acorn Cap** | — | C | Spend | Bonus action: an adjacent hostile becomes Spotted | `rest` 1/1/2 · v1 | "Bonus action: an adjacent foe is Spotted" | All | M4/Later | **HOLD**: adjacent only (Tinder Ring precedent); Spotted ruling (CA-23, then the client); renamed from Crooked Finger (HL Crooked Road) and then from Pointing Bone (a living people's practice, HL NR3; T5) |

Hints are written in the trigger's literal rule words (the drafts' idioms "Shrug it off", "It quails", "Stand by them", "Catch them leaving" and "Hurt fills the knot" were replaced after the check; flavour lives in the Whence line). No hint, log or Inspect string uses "catch" (2.3 F13). Each shipped M3 family has its own silhouette class; at most two "Knot" families ship in one wave (Jolt Knot, Bloodknot), and V23s checks that no two shipped charm icons share a silhouette class (6; after the check).

### 5.2 Approved Later entries (TK 13.7)

| Entry | Status | Reason |
|---|---|---|
| Bloodknot | Row 8 above, HOLD for CG1, shadowed `rest`-charged | Shares the one trinket Warded per fight |
| Kinstone | HOLD, M4 | Needs the `sourceTied` ruling and a ruling that the receiver counts it as its trinket Warded |
| Scrollwright's Token | **MOVE** to TK 19 exploration-first trinkets (Later) (change CA-15; told plainly) | Not a combat moment (fails rule 17); the CN 25 / ID-3 revisit lever is kept |
| Last-Breath Charm | Stays retired (DH-47, agreed DH Q7) | — |
| Muse's Token | HOLD Later | Rare (TK Q2 closed, agreed, Q5); needs the Leadership-mastery owner; an Inspired charm would change TK 5.5's None "Never here" cell (agreed TK Q1) and TK 6.1 item 6, so it returns to the client as a question, never as a lead's adoption (corrected after the check; T13) |

### 5.3 Rejected (told plainly)

| Idea | Reason |
|---|---|
| Any +N, two-charm set bonus | Denylist; buff test; AR 5.9 |
| Fight-start or round-N wards | Warding Lining; TK 13.9; Graces |
| Near-Miss / Blink Bone (`attackMissedYou`) | Always-fires ~972‰ for the Vanguard *(illustr.)*: a stat in disguise |
| Blink Bone re-keyed to "an opportunity attack against you misses" | Negative-EV provoking; `missedBy` overlap |
| Milk-tooth (`hostileProneAdjacent`) | Topple skew (shadow only); feet subject (legal under Q3, still not adopted); its object implies the Dreamer's age (RS 7.5; after the check) |
| Press-Stone (`pressedByTwo` → Warded) | Near fight-start ward for the Vanguard; envelope |
| Bier-Knot (`endMoveBesideDowned`) | Fails the floor (44–128‰) |
| Gallows Penny (standalone) | Fails the floor alone; merged into Jolt Knot |
| Held-Breath Knot (Keep → ally Helped button) | The cut Help-whistle under another name |
| Spurred outputs; kill → Hidden | CON 18.4; Rally; back; No Witnesses |
| Flank or pincer triggers | No flanking (SH 9; BA-7; Beset is the Shade's) |
| Weapon-verb triggers (Vex, Nick, Cleave, Push ids) | TK 11 item 4; dead loot under family caps; Vex already grants Helped |
| Listen, Search, stealth catches | Rule 17; combat-only |
| Party-wide grants | Decision 7; rule 22 |
| Echoing or unspent tokens (Ward Echo, Doubled Knot) | Ward-stacking lock; Inspired copy (both already cut in TK 13.9) |
| Nth-event counters (Tally-stick) | Deferred (TK 4.1) |
| Rare charms, Rare beads | TK Q2 (agreed, Q5) |

### 5.4 Display surfaces, sample text (*illustr.*; rewritten after the check to the TK 15 line format and whole strings)

- Card: "**Jolt Knot** · charm (none)" / "◈ **Jolt:** once per fight, automatic: when your forced movement stops short, or the foe you moved ends in danger, you gain Helped". Whence: "*Tied by someone who was always being pushed.*" In a Sanctum fight only, the card note "Once per boss encounter" (BR-31).
- Notable: "Wardstone of the Jolt" (IR-9 epithet).
- Forecast (one template): "If the shove stops short → Jolt Knot: Helped"; with a block, the whole strings "If the shove stops short → Jolt Knot waits" and "If the shove stops short → Jolt Knot: missed".
- Inspect, reactive: "Hangnail · ready · fires when a harmful condition fails to land on you".
- Log: "Hangnail fired: Ysolde gains Helped." / "Advantage (Hangnail)." / "Hangnail waits: Ysolde already holds Helped." / "Bloodknot missed: Bram already had a trinket ward this fight."
- Greyed: "A trinket ward was already used on Bram this fight" (`AlreadyWardedThisFight`); "Already gained a Helped from a charm this fight" (`CharmCaughtThisFight`; the identifier stays in data only).
- Boon picker: "**Bobbin** · take one small moment into the dream", then a list of icon cards, one per family, each with its card line and fit verdict (no joined sentence).

---

## 6. Art and audio (lead's call; revised after the check, lead's call, after the check)

- **Objects.** Each charm is a dream object that *remembers*, and none implies who the Dreamer is or how old (RS 7.5; after the check, T4): a knotted cord, a knotted lock of hair, a nail paring, a smooth pebble, a holed stone, a hairpin, a walnut shell, a cinder bead, a wishbone, a small bone, a cracked button, a pressed flower. The first proposal's milk tooth, swaddling scrap and under-pillow motif are dropped (they imply a child), and so is the bent penny (it reads as coin, which the lane's Never here list excludes, and RC9/EV-25 treat as a misleading read). Kit-bashed icons from TK 15's recipe (kind silhouette × material × element tint × lane glyph), but charms have no element, so **each shipped family gets its own silhouette class** (5.1 column "Object"); at most two "Knot" families per wave; V23s and the art gate check that no two shipped charm icons share a silhouette class, so the CG2 probe "tell your two charms apart" is not lost to a hue-only difference. **Icon-only**; no worn model (jewellery is icon-only on the party view, TK 15); nothing hangs from kit accents (SK RC9).
- **Plate (one shared set; after the check).** A unit's charms are **one** plate mark: a pair of pips, one per worn charm, so charms cost one of XI's two plate marks (`plateMarks: 2`). The pair sits on a plain rounded backing with no sack, knot or spool outline, kept apart from the Windfall knotted-sack mark (LP) and the Lodestring spool (XI, "never a knot"), and gated against both in greyscale. States, each one shape that holds in greyscale and with motion off (RC4; RS 7.4):

  | State | Pip | Inspect icon | Extra cue (never the only one) |
  |---|---|---|---|
  | Ready | Filled pip | Icon at full value | — |
  | Spent (also missed) | Hollow pip | Icon at about 40% value | — |
  | Waiting (a deferred catch) | Filled pip crossed by one bar | Icon at full value with the same bar | A slow pulse; never an hourglass (TL's Hour-lock glyph) or a clock |
  | Kept (Keep filled) | Filled pip inside a square frame (the ▸ family) | Icon with the square frame | — |
  | Dormant | No pip | "? dormant · Arcana" on the card | — |

  The pip patterns join the RC5 pattern registry, and the pip pair, the forecast line and the fire beat are registered as XI `SurfaceRecord` rows (V18s `SurfaceRegistered`) (CA-27).
- **Fire beats:** three reusable 2D beats over the unit, one per silhouette class, then the token: *tighten* (knot, hair), *settle* (stone, ring, nut, bead: a small drop and settle), *flex* (crescent, pin, fork: a bend and spring back). Only beats M3 uses ship. No element VFX (Patina rules); never implies light, heat or motion as an effect (RC4, RC9). Under reduced motion each beat is a static outline flash of the icon for the beat's length.
- **Audio (corrected after the check; T3).** Two **output cues** in the global UI family, never one sound per charm. The first proposal's "muffled bell" and "released breath" are withdrawn: bells, struck bronze and chimes are RC7-reserved, and the dry-breath family is HZ's Heart Stir cue (BR `FinalBossNoDreamer`; CH AN9 bars any breath-family sample). Placeholder timbres, pending the RS and audio owners' confirmation (CA-27, a pending request):

  | Cue | Placeholder timbre | Kept out of |
  |---|---|---|
  | Warded output | A soft mallet tap on a hollow gourd | Bells, struck bronze, chimes; the Carillon's muffled underwater thud (BR); door open and close, knock foley (EV); ticks and clicks; CH AN9's figure families (bench creak, slate scrape, card set on board) |
  | Helped output | A marble rolling to rest in a shallow bowl | Breath and whisper families; the Windfall pluck (LP-7) and any plucked string; TL's paper or bead shaker (Rattle Wire) and HZ's wooden rattle; cloth and leather (HL, AN9); paper, page and card families; HL's clay press, reed swell, ash crumble and wood hook settle |

  Each cue gets a row in the RS timbre table, is tagged for `ReservedCues`, and runs pairwise in the 85% audio blind-ID gate (XI 6). They play **only when a charm catch fires**: not on Wardstone or Ward-Knot presses, and not on Warded or Helped from class or consumable sources, so they never become a condition-application sound against RC7's global condition skin. (They are not called "stings", IR 6.3's word for the boss banner.) Trophy Tally moves to the Helped cue at **M3**, with the batch, so M2 changes no sound.
- **The Bobbin** has no object of its own on the hub (charms get no hub surface, CH-24); it is offered in the launcher's or Begin again's boon picker (CH 4.9, 4.10) on an existing boon-card frame with no bed, pillow or child motif (corrected after the check; T4).
- **M3 volume *(illustr.)*:** 7 distinct silhouettes for the new M3 records plus Wishbone and Meeting Stone if they ship (up to 9), 1 plate-state set, 3 beats, 2 output cues (Tally's re-map included). M4: about the same again.
- **Writing and localisation (change CA-18 data; rewritten after the check):** every string is a whole-string key per **record × line × surface**: card line, `plateHint`, `forecastHint` (setup only), Inspect per state (ready, spent, waiting, kept, dormant), fire log, waits log, missed log, wasted log, Whence; never per trigger × output, and never built from fragments (two-branch and ally-target lines are whole strings of their own). Named placeholders are `{unit}`, `{charm}`, `{action}` and `{piece}`, each with gender and case notes for translators; one forecast template (5.4); waits and missed forecasts are whole strings; the boon picker is a list of icon cards, never a joined sentence. Hints use the trigger's literal rule words; any idiom carries SK's idiom tag, and flavour lives in the Whence line. +35% length; CardFit at 150% (SK charter; CH risk 13; HL NR10). Volume, recounted: about 10 records × 13 surfaces, plus verdicts, greyed reasons, `uiMemory` callouts and the boon picker, ≈ 150 keys and ~3k words at M3 *(illustr.)*.

---

## 7. New mechanics (lead's call unless marked)

1. **Catch** shape: automatic, `fight` minor, outcome-keyed, outside ButtonCap (CA-1; agreed, Q1).
2. **Keep** shape and `charmKept[(unit, propertyId)]` state, with the Keep mechanics of 4.2, reset only by the fight-resources reset event (BR 4.2.5; 4.6 item 7; corrected after the check), in the save and the suspend journal (SV Q4) (CA-11; M2 simulator-only for the shadow records, M3 live).
3. **`charmTriggers` / `charmOutputs`** closed vocabularies with setup/reactive class, the `actor` attribute, `once`/`recurring` repeat and the exact predicates of 4.3.1 (CA-3; cross-location reads agreed, Q3; actor, repeat and predicates after the check).
4. **`CharmFireOnce`, no-chain, one caught token per output per receiving unit per fight** (CA-5 ⚑; agreed, Q2), with the order, provenance, after-the-use resolution and one-fight state of 4.6 (after the check); M2 simulator-only for the shadow records, M3 live.
5. **`onBlocked: defer`** for every catch (Tally's rule generalised), with the defer, missed and wasted meanings of 4.5 (after the check).
6. **Policies** `always` / `spareForButton` (CA-8); the choice per (unit, property) is saved state (CA-11; after the check).
7. **forecastHint / plateHint** split; waits and missed forecasts, computed through party knowledge (CA-6; 4.7).
8. **Verdicts:** party-scope DeadLine; "rarely fires for this unit" (by party size and floor band); "no ally to receive this"; "shares its Helped with <charm>" (CA-6; the last after the check).
9. **Dormant catch rules** (4.8), with mid-fight reveals inert until the reset (4.6 item 6).
10. **Bobbin** boon type, its earn milestone, its choice list, its placement on the hero and its `configHash` entry (CA-20, CA-26; agreed, Q4; choice list and key new lead's call; earn rule and placement after the check).
11. **Event** `CharmCaught{unit, receiver, property, trigger, eventSeq, outcome: fired|deferred|missed, shadow: bool}` beside `TrinketPropertyFired`; `wasted` is logged separately when a granted token expires unconsumed (after the check).
12. **`simOnly` records** for shadow simulation (CA-18).

---

## 8. Numbers (toy model, not the simulator; all *(illustr.)*)

Assumptions: ~4.5 rounds per fight; ~25 fights per run; fight mix easy 40% / standard 40% / elite 15% / boss 5%; ~3 fights per rest; hit 60%, advantage 84%; ~9 damage per hit, Exploit ~7; party DPR ~32; Shade ~54 damage per fight with ~50% of attacks already advantaged. Fire rate per fight = 1 − (1 − p)^4.5. Scripts: the design lead's scratch area (`toy.py`, `toy2.py`). Minions are **not** yet filtered in these rates; new records must be re-run with `nonMinion` (4.3; Tally stays unfiltered). **Party size is not an axis of this toy** (after the check): the rates are for a mid-size party and must be re-run at party size 1 and 4 before shadowing (kill-keyed and ally-keyed catches swing hard with size; Minder's Pebble, Second's Hairpin and Walnut cannot fire solo).

### 8.1 Fire rates per fight (‰; Vanguard / Shade / Arcanist / Herald / melee companion / ranged companion)

| Trigger | Rates | Reading |
|---|---|---|
| `adjacentHostileDropped` (Tally) | 673 / 634 / 206 / 313 / 591 / 168 | Front line only; below the 333 floor for Arcanist and ranged |
| `attackMissedYou` | 972 / 856 / 437 / 726 / 900 / 378 | Always-fires: rejected |
| `savedAgainstHarmful` alone | 279 / 206 / 243 / 243 / 206 / 168 | Fails the floor → merged |
| `wardSpentOnYou` with a ward source | 378 / 243 / 206 / 243 / 313 / 168 | Marginal → merged |
| `harmfulRebuffed` (merged) | ~450–550 / ~380 / ~400 / ~400 / ~420 / ~300 (estimate) | Passes for most roles; re-run |
| `forcedMoveStoppedEarly` | 378 / 87 / 243 / 87 / 128 / 44 | Vanguard only |
| `forcedIntoDanger` (F2+; token `movedHostileEndsInDanger`) | 243 / 87 / 206 / 44 / 87 / 44 | 0 on F1 |
| Jolt Knot (merged, F2+) | ~450 Vanguard, ~400 Arcanist | Passes for 2 launch classes on F2+ |
| `youFirstBloodied` | ~463 average; easy 150, standard 550, elite 900, boss 950 | Per-fight-class reading needed |
| `hostileProneAdjacent` | 378 / 206 / 128 / 128 / 243 / 87 | Topple-skewed |
| `allyDownedNear` | ~87 | Fails alone |
| Minder's Pebble (union) | ~450–550 claimed; **likely higher** (union over up to 3 allies) | Recompute before shadowing |
| `endMoveBesideDowned` | 44–128 | Fails |
| `hitTwoOrMore` | 313 / 168 / 799 / 206 / 128 / 87 | Arcanist |
| `oppAttackHit` | 437 / 206 / 44 / 128 / 313 / 44 | Zone front line |

**Floor reading (change CA-16; told plainly).** TK 18.3 already says "at least once per 3 fights for its wearer". The new part is that **rule 26 must also hold on the fire-rate report**: at least one launch class at ≥333‰, **and** a second launch class **or** `anyCompanion` at ≥333‰. (The critics' "two launch classes" reading would kill nearly every expressive catch; rule 26's `anyCompanion` branch is kept.) Leaning is allowed by design and shown in the equip verdict ("fires often for front liners"); the 50% pick ceiling guards against leaning becoming mandatory. Always-fires **gate** (not a warning; corrected after the check): >850‰ in any fight class fails CG1 *(illustr.)*. **On F1, Jolt Knot reads only its stopped-early branch** (378‰ Vanguard), so the rate report reads Jolt Knot per floor and the "rarely fires for this unit" verdict uses the floor's own rate (new lead's call).

**How the floor is read (lead's call, after the check):** rule 26 is read separately per **floor band** (F1; F2 and deeper) and per **party size** (1, the F1 and Lone Road case; 4, the modal size of F3 and deeper), with a verdict for each cell. A candidate ships only if it passes on **F2+ at party size 4**; a failing F1 or size-1 cell does not block shipping but drives the "rarely fires for this unit" verdict on that floor and party size, and is shown in the Bobbin picker. Told plainly (T17): **Jolt Knot fails the second-class branch on F1** (Vanguard 378, Arcanist 243 *(illustr.)*); it ships on its F2+ reading with an F1 verdict.

### 8.2 Value against the budget

- Helped catch, non-Exploit wearer: +0.24 × 9 ≈ +2.2 damage per fire; at 600‰ ≈ +1.3 per fight ≈ 0.9% of fight HP. Correct for a Common "habit" (TK 8.2).
- Helped catch, Exploit Shade: ≈ +4.0 per fire; at ~630‰ ≈ +4.7% Shade damage; **two catches ≈ +9.4%** → the per-output cap (4.6; agreed, Q2).
- Bloodknot vs Wardstone: Wardstone ≈ 0.33 uses per fight (L1–8), 0.67 (L9–10), pressable in round 1; Bloodknot ≈ 0.46 per fight overall, ≈ 0.91 in hard fights, but mid-fight. Equal value 1 is fair **on average only**: Bloodknot fails the 4.5 per-fight-class test (≈ 0.91 in hard fights), so it is shadowed and gated in its `rest`-charged form (`rest` 1/1/2, automatic, value 1), where Wardstone's round-1 timing against Bloodknot's mid-fight timing is the fair comparison (corrected after the check; T6).

### 8.3 Envelope toy (boss hard land, 650‰ baseline)

| Loadout | Boss hard land |
|---|---|
| 1 ward catch | ~601‰ |
| 2 ward catches | ~552‰ |
| 4 mid-fight ward catches | ~455‰ |
| 4 round-1 Wardstones | ~280‰ |

**Reading (told plainly; corrected).** The toy assumes "first attempt blocked, no retry", which overstates the loss: each unit gets one trinket Warded per fight, so a boss that keeps using control lands later. The toy suggests the literal "maximum legal loadout" reading of TK-4 **could** be tight; only the simulator can say. Question to the lead (CA-17): is TK-4 measured at the literal maximum legal loadout (TK 18.3) or at a realistic loadout (the 95th percentile of ward-charm count per party from the canonical-unlock natural-drop arm, plus "no catch ward is worse than the approved Wardstone row")? **Both readings, defined after the check:** the literal maximum legal loadout is every unit wearing the candidate plus the legal second charm and trinkets with the highest prevention; the realistic loadout is the 95th percentile above, reported only (lead's call, after the check). This is a clarification; the binding 600/500 numbers are not touched. **Interim (new lead's call; 2.3 F14):** CG1 reports both readings and the literal one binds until the clarification is recorded. Under ~1.1 charms per run, a party holds 2+ ward charms in ~3–5% of runs.

### 8.4 Fight length, damage, DH 18.2

One charm per party: fight length ≈ −1%; damage taken ≈ −1% (Helped) to −2% (Warded). Win rate ≈ +0.5 to +1.5 points per charm-holding run, inside the 25–33% noise. Ward catches concentrate in elite and boss fights; watch the "≥50% of deaths at F3–F5 elites or bosses" reading in the no-healer arm. No DH 18.2 risk at the expected loadout.

### 8.5 ButtonCap bind

P(2+ button charms in a run): M2 roster (2 of 3 buttons) ≈ 17%; buttons diluted to 30% ≈ 4.4%; at 20% ≈ 2.1%. Per-unit bind is lower still. **This is the strongest data-free reason for the catch batch, and why two base Catch families matter for new profiles** (4.10). Keep records raise the button share: ≤1 per wave.

### 8.6 Visibility

Equal 8-lane toy: λ ≈ 1.1; P(0) ≈ 33%, P(1) ≈ 37%, P(2+) ≈ 30%. At a charm-lane weight of ~1/5 of trinket draws: λ ≈ 1.8, P(0) ≈ 17%. With the Bobbin taken: P(0) = 0 for that run. The builder must recompute through the IR 4.2 weight pipeline and the LE/LP drop model (V14/V15 reports), per floor and **per unlock set** (minimal and canonical), including the camp shop and carried Wardstones.

### 8.7 Forgotten charms

At 600‰ a charm is silent in 40% of fights, still ~15 fires per run. The risk is the mismatched wearer, answered by the "rarely fires" verdict and the scorer request.

---

## 9. Data and validator V23

### 9.1 Records (*illustr.*; extended after the check)

```
charm {
  shape: spend | catch | keep | shaper,
  catch: <charmTriggers token> | [token, token],   // two-branch allowed (4.5)
  catchClass: setup | reactive,
  filters: [nonMinion, ...],                         // none on Trophy Tally
  output: { condition: Warded | Helped,
            target: self | adjacentAlly,             // the receiver is fixed by the trigger
            duration: untilSourceTurnEnd, source: wearer },   // 4.5 output duration
  pool: fight | rest, bands?,
  policy: always | spareForButton,                   // no prompt policy (4.5)
  onBlocked: defer,                                  // defer or missed by repeat (4.5)
  iconSilhouette,                                    // unique per shipped family (6)
  readyDisplay, plateHint, forecastHint? (setup only),
  answers[], usefulFor[], requires?,
  boonEligible?: bool,                               // Bobbin list (4.11)
  simOnly?: bool, supersededBy?: id
}
charmTriggers entry { token, catchClass, actor: wearer | ally | hostile | any,
                      repeat: once | recurring, crossLocation?: bool, predicate }
```
Vocabularies `charmTriggers`, `charmOutputs` live in `inventory.json` as a lead's-call block via the TK-11 path (TK 17.2; ITEMS 5.1). `crossLocation: true` marks a token that reads another location's subject (`movedHostileEndsInDanger`, `hostileProneAdjacent`), so V23s can enforce Q3's grant-only rule (new lead's call). `offTurnForWearer` is derived from `actor` (4.3 rule 4). The drafts' `catchAlly` target is withdrawn (Second's Hairpin uses `adjacentAlly`; after the check).

**State (corrected after the check):** `fightUses`; `charmKept[(unit, prop)]`; the receiver-keyed flags `charmCaughtHelpedThisFight[unit]` and `charmCaughtWardedThisFight[unit]` behind `CharmCaughtThisFight`; the armed/missed state of each catch; the per-(unit, property) policy choice. Every per-fight entry is reset only by the fight-resources reset event (BR 4.2.5; 4.6 item 7), never at a combat start. All of it is in the save, the suspend journal and replays (CA-11). Event `CharmCaught` (7 item 11).

**Resolution (corrected after the check):** catches are queued and resolve after the triggering use is complete (4.6 item 4), by core event sequence, then ascending unit order of the wearers, then the `CharmFireOnce` order (4.6 items 1, 5). No RNG. Replay bit-identical (TK 17). The `boonEligible` flag is derived by the build from 4.11's list, never hand-set on a HOLD or `simOnly` record (new lead's call).

**Shadow records (defined after the check, lead's call, after the check).** A `simOnly` record runs in one of two registered modes (SV 6.2 arm catalogue; SV 6.10 and 9.1 report registry):
1. **Observer:** evaluates the predicate and every cap against a cloned state, never mutates the run, and emits `CharmCaught{shadow: true}`; real caps are read, never written. Used for fire, defer, missed and bind rates.
2. **Forced-loadout arm:** the record is worn and live in a simulator run, paired with a same-seed no-charm partner arm; used for the precision ceiling, win delta and envelope.

### 9.2 Content rules (ITEMS 19.1, new rules 29–31; change CA-18)

ITEMS 19.1 ends at rule 28 today; rules 29–31 are free (re-checked while writing).
- **Rule 29:** every charm declares shape, catchClass, readyDisplay, iconSilhouette and the hint fields below; hints name the outcome in the trigger's literal rule words. Required fields by shape and class (after the check; `plateHint` is TK 9.1's mandatory previewHint under its serialized name):

  | Shape and class | `plateHint` | `forecastHint` |
  |---|---|---|
  | Spend | Required | Never |
  | Keep | Required | Never |
  | Catch, setup | Required | Required |
  | Catch, reactive | Required | Never |
  | Shaper | Required | Never |
- **Rule 30:** catch in `charmTriggers` and a combat event (never `combatStart`, `roundN`); output in `charmOutputs`; no class, verb, ability or item id keys; no chain; `onBlocked` and `filters` declared; a `crossLocation` token only with a Warded or Helped output and no effect on the subject (agreed, Q3).
- **Rule 31:** a Spend or Keep holds the item's one button (rule 19); a shaper acts only on a held token and never creates one.

### 9.3 V23 `CharmLane` (registered by CA-18; owner TK; phase split per SV-20)

- **V23s (static):** rules 29–31; vocabularies; no id keys; no chain; ally-Warded on `rest` only; off-turn ally targets `reactionShaped` (from `actor`, 4.3 rule 4); `usefulFor` (rule 26); copy-registry declarations present; `simOnly` records never in any loot table, channel, `drawableSet`, boon list or shipped manifest; `boonEligible` only on Common Catch records that are shipped; name-collision grep (AR 17.1 rule 13 pattern), with "Catch" flagged until the naming pass rules (2.3 F13); CardFit 150%; a reactive catch declaring a forecastHint fails (the 9.2 field table). **Added after the check:** no player-facing string (card, log, Inspect, hint) contains "catch" or "caught" (2.3 F13); each card body line ≤ the longest approved TK trinket line plus 35% (4.2); no two shipped charm icons share an `iconSilhouette` (6); the rule-28 output checks (4.8); every `charmTriggers` entry declares `actor` and `repeat`; `offTurnForWearer` is derived from `actor`; a `boonCharm` instance never carries the `heirloom` flag (4.11); the Bobbin's slot is an empty None slot at WO1.
- **V23r (report):** fire rate per charm per fight, split by role, party size (1–4), floor, theme, fight class, boss and phase, and unlock set; fired/deferred/missed, and wasted (a granted token that expired unconsumed); a **missed rate** per charm, counted as non-fires by CG1; "second same-output charm never fired" as its own line; the setup share of the candidate and shipped batches (CG3); rule 26 on the rate report per floor band and party size (8.1); the always-fires gate; per-fight-class self-ward test; bind rates, each as **qualifying events blocked per wearer-fight** (`TrinketHelpedLimit`, `CharmCaughtThisFight`, ButtonCap, DuplicateAnswer ≤100‰, the E+ veto); precision ceiling ≤+10% with Exploit counted; Helped spent on an attack ≥600‰ of catches *(illustr.)*; the landing envelope under both CA-17 readings (2.3 F14; loadouts defined in 8.3); the Bobbin's win delta against a heirloom-boon arm (M3; its pick share is a human-session and telemetry report, 9.4, not a simulator one). In M2 it runs on Tally, Wardstone and Ward-Knot plus the shadow records, in the two shadow modes of 9.1.
- **V23t (test):** replay determinism of `charmKept`, the caught flags and deferred catches; save/suspend round-trip, the policy choice included; the Bobbin choice, unit and slot in `configHash`. **Cases added after the check:** Bloodknot against the same hit's rider (no block); Tally against a Cleave follow-up (no advantage); Hangnail against an opportunity attack (resolves after the reaction); one drop qualifying Tally on two wearers (unit order); one application qualifying Hangnail and an adjacent ally's Walnut; a Sanctum step-out and re-engage (no reset, Keep fill kept); a once trigger blocked (missed, spent pip) and a recurring trigger blocked (defers); provenance cases (a kill with charm-caught advantage, a combined forced move, a channel type shift, a ward answering a condition); a line revealed mid-fight (inert until the reset); `KnowledgeLeak` cases for the forecast (an unseen blocker, an unrevealed hazard square).

### 9.4 Reports (change CA-16; TK 18.3; AR 17.3; registered in SV 6.10 and 9.1, arms in SV 6.2, minimum N in SV 6.5)

Catch rate per charm (splits above); missed rate per charm; Helped and Warded wasted (granted, then expired unconsumed); Helped spent on attack vs skill check; rest-charge spend share before next rest (Spend); kept-unspent (Keep); two-charm units; charm share of worn slots; pick share by class and companion; hero vs companion fire rate; companion turn time with charms; plate reads per unit; ward-button greyed by a catch; dormant share; charm-lane finds per run per unit set; Bobbin win delta (simulator, M3); Bobbin pick share, ceiling and floor (human sessions and telemetry, the SV human instrument, SV 6.11; M3). Each report names its source (sim, human, telemetry) and its comparison arm (after the check).

---

## 10. Staging with data gates (lead's call; CA-19)

| Wave | Content | Gate |
|---|---|---|
| **Now (paper)** | Verdict; shapes (agreed, Q1); vocabularies and reject list; outputs; CA-4 Spurred record; the Q2, Q3 and Q5 answers recorded; read ruling request (CA-10, before M2 UI); name fixes (CA-12–CA-14) and the "Catch" rule (never printed); V23 and rules 29–31 as specifications; TK-4 question (CA-17) with its interim; timing split (CA-19) | Client approval of this document |
| **M2** | Roster unchanged (TK 13.7 stands). "Talisman" off charm cards; Patina accent rename; ready/spent plate states for Tally; V23s/V23r on the M2 roster; **reports (CA-16)**; **simulator-only shadow records** (`simOnly`) for catalogue 4–16, in both shadow modes (9.1), with the CA-5 limits, the 4.6 resolution rules and the CA-11 state (`charmKept`, the caught flags) implemented **simulator-only** so that CG1 reads them as the client chose (after the check); Bloodknot shadowed `rest`-charged | None for players |
| **M3** | Base: Hangnail, Jolt Knot. Unlocks: those of Minder's Pebble, Second's Hairpin, Bloodknot (`rest`-charged), Walnut, Wishbone and Meeting Stone that pass CG1. Keep: Ashen Bead if CG2 (agreed, Q1). New limits live (agreed, Q2). Bobbin boon (agreed, Q4). Trophy Tally's sound moves to the Helped output cue | CG1, CG2, CG3 setup share; Bobbin parity and pick-share gates |
| **M4** | Breadth: Twitch, Lockjaw, Shy (each through CG1), and Wishbone or Meeting Stone if they missed M3; Kinstone; Spotted outputs if CA-23 lands; Ashen Bead if it missed M3; C-slot charm string evaluation (agreed, Q5) | CG1, CG3 |
| **Later** | Shapers; counters; Muse's Token; exploration-first Scrollwright's Token (TK 19) | Owners |

**CG1, simulator, before playtest (per candidate):** rule 26 on the rate report, passed on F2+ at party size 4, with the F1 and size-1 cells driving verdicts (8.1); always-fires ≤850‰ in every fight class (a gate), with missed fires counted as non-fires; a self-Warded catch read in its `rest`-charged form when it fails the 4.5 test; landing envelope at the binding 600‰/500‰ under the measurement the lead sets (CA-17; the literal reading binds until then, 2.3 F14); bind rates ≤100‰ of qualifying events per wearer-fight; precision ceiling ≤+10% with Exploit; Helped spent on an attack ≥600‰; re-run with `nonMinion`; Minder's union recomputed.

**CG2, M2 playtest:**
- **Spend metric restated (told plainly):** "press rate ≥0.5 per fight" is wrong for rest pools (a player who saves Wardstone for the elite would fail it). Use: **share of rest charges spent before the next rest ≥700‰** *(illustr.)*, with the elite/boss share reported, not gated. Decides Keep and Wardstone's future.
- No family >50% within a class; charm share of worn slots ≤35%; median companion turn <10 s (settled); plate reads ≤4; "name what your charm did" ≥80%; "tell your two charms apart" ≥70% *(illustr.)*.
- **Sample:** natural drops give too few observations (P(0) ≈ 33%). CG2 runs **forced-loadout human sessions** as a registered SV human-session arm (SV 6.11, 6.2): the assigned loadout is a `configHash` field of the session's runs, so the SV key holds it; they advance no unlocks (SV Q1); the Bobbin is off in them (4.11) (corrected after the check), minimum ≥30 charm-holding runs per metric *(illustr.)*. A gate that cannot reach its N is "simulator-only, playtest-confirmatory" and cannot fail for lack of data. Natural drops are used for visibility and pick share only.

**CG3, approval:** each wave approved by the lead; the shipped M3 batch's setup share is counted, and a batch under half setup ships only with the shortfall told to the client (4.3 rule 4; after the check); Keep ships only with its own comprehension probe passing; C-slot only if the two-charm bind rate shows a third line would not be dead; Rare never unless the client reopens TK Q2.

**Remains with the client:** the lane-weight question (14.2); any ButtonCap change; any reopening of TK Q2; the C-slot string at M4; `CharmTriggerCap` if triggered; an Inspired or Spotted charm, which would change the agreed charm lane (T13, T14).

---

## 11. Ownership and hooks

| Item | Owner | Needed by |
|---|---|---|
| Lane wording, shapes, vocabularies, catalogue, values | TK owner (lead); wording agreed, Q1 | Now / M3 |
| 7.1 limits | Client (agreed, Q2); TK owner applies | M3 |
| Cross-location triggers | Client (agreed, Q3); TK and AR owners apply | M3 |
| Read rulings (Catch 0; the trinket button) | CL owner + lead (request CA-10) | Before M2 UI |
| Helped window on own-turn catches (reading) | CON owner (request CA-9, a confirmation) | M3 |
| Spotted at M4 | CON owner, Herald lens (request CA-23), then the client (agreed TK Q1 lane) | M4 |
| Patina rename; IR 9.4 pointer | IR, RS owners (requests) | M2 |
| V23 registration, SV 5.2, report and arm registry, `unlockHash` inputs | RS, SV owners (requests) | M2 |
| Party scorer | ABL owner (request CA-22) | M3 |
| Bobbin | Client (agreed, Q4); MAIN/CH owners (request); SV owner for the `configHash` entry; LE owner for the Due reading and the V13g (g) case; SK owner for the worn-slot row, the `KitNoTrinket` waiver and the Oddment pool (CA-26) | M3 |
| Plate pip pair, forecast line and fire beat as XI surfaces; pip patterns; output cues in the timbre table | XI, RS owners and the audio owner (request CA-27) | M2 (Tally's pips), M3 |
| "Catch" design word (never printed) | Naming pass | Before M3 card-text freeze |
| Lane weight | IR/LE owners + client | After CG1/CG2 |
| Copy-registry rulings (Twitch, Lockjaw, Shy, Quietus, Second's) and the ally-Helped catch ruling | TK owner + class owners (CC-54 pattern) | Before shadow results are read |
| Kinstone `sourceTied`; Warded fallback duration (Stay Thread) | Lead | M4 / Later |
| TK-4 measurement (CA-17) | Lead | Before CG1 |

---

## 12. Binding requirements and how they are met

| Requirement | Met by |
|---|---|
| ButtonCap 1 (agreed TK Q3) | Catches never count; Keep ≤1 per wave; kept by agreed Q5 |
| Denylist (TK 6.2) | Token outputs only; 5.3 rejections; V23s |
| Caps of TK 7.1 | Unchanged; the three additions chosen by the client (agreed, Q2) |
| Zero added damage (agreed TK Q4) | No damage outputs; Helped's Exploit effect counted in the precision ceiling |
| Value equals budget (ITEMS 12.5) | 4.5 values; no string, so no per-string accounting |
| One-table rule; domains (agreed TK Q1) | Cross-location reads grant tokens only (agreed, Q3); domain-clean base batch except Jolt Knot's danger branch; V23s `crossLocation` check |
| previewHint mandatory (TK 9.1) | `plateHint` (the serialized previewHint) on every record; `forecastHint` on setup catches (9.2 field table) |
| Pools (TK 9.3; ABL 6.2) | `fight` minor or `rest`; majors `rest` 1/1/1 |
| Combat-first (rule 17) | Combat events only; Scrollwright's Token moved |
| Class neutrality (rule 26) | Outcome keys; rule 26 on the rate report |
| No auras (decision 7); rule 22 | Self or one adjacent ally; `catchAlly` withdrawn, Second's Hairpin narrowed (after the check) |
| ≤4 reads (ABL 11.4) | Both rulings requested (CA-10: Catch 0 with a plate ready pip; the button); until then a Catch counts 1 and the verdicts cover the Vanguard (corrected after the check) |
| Landing envelope 600/500 (binding TK-4) | CG1; measurement question CA-17; literal reading binds meanwhile |
| Never cursed, never heirlooms (HL 4.6) | Bobbin is a fresh, uncursed Common each run |
| One starting boon, earned (decided MAIN 4.7) | The Bobbin is one boon option, never stacked, unlocked by a milestone (after the check) |
| Events never grant loot (EV 4.4.8) | No event channel |
| Determinism (TK 17) | No RNG; after-the-use resolution and a total order (4.6); V23t; the Bobbin choice, unit and slot in `configHash` |
| Unlocks are content, never power (MAIN 4.7) | Unlocks add families, not strength; base families for new profiles |
| M2 roster stands until data (TK 24.2) | No M2 content change; Trophy Tally keeps its unfiltered line and its M2 sound |
| One fight in the Sanctum (BR 4.2.5) | Every charm per-fight state reset only by the fight-resources reset event (4.6 item 7) |
| The Dreamer is never implied (RS 7.5); reserved sounds (RC7, AN9, HZ) | Age-neutral objects and names; the Bobbin renamed; output cues outside every reserved family (6) |

---

## 13. Changes to approved documents

Applied on 2026-11-24, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch (lead's call), and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; as in the SK, CH and HL precedents, the Flag column also marks rows that only *read* such text without changing it. **The client's approval of 2026-11-24 covers the flagged rows** (with Q1–Q5 where a row names one). Told-plainly rows were told in 14.1 and, for those found in the check, 14.3, and confirmed by the client on 2026-11-24. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; recorded as readings, approved by the client with this document on 2026-11-24; each owner confirms its own reading):**
- A Catch is a TK 9.1 Passive.
- Trophy Tally's "not spent if blocked" is the general catch rule.
- BR-31 and BR 4.2.5's one-fight rule make once per fight once per boss encounter, and the fight-resources reset event is the only reset of charm per-fight state (after the check).
- Helped on an already-advantaged roll carries over (W 7.5 step 2), so Prone-keyed Helped does not idle.
- CON 5.6's "a window opened during the source's own turn runs to its following turn" covers a charm-caught Helped gained on the wearer's own turn (new lead's call; CA-9 becomes a confirmation).
- The Bobbin's charm counts as party-owned for the Dream's Due's owned and duplicate checks but never as F1 hero arms (ITEMS 14, LE 6.12, HL-3 read; new lead's call); it carries the `boonCharm` flag, never `heirloom` (after the check).
- Charm-family unlocks use the Library's generic loot-unlock object (CH 4.11), which is not a charm hub surface under CH-24 (after the check).
- Trophy Tally's approved line carries no minion filter and keeps none (TK 13.7, read; after the check).

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| CA-1 | TK 5.5 None row "Signature shape"; TK 2 Goals; TK 9.1 shapes table; TK 9.2 | Shapes Spend, Catch, Keep, Shaper; lane wording "Charms catch moments: a token earned from a named moment in the fight, or one button you spend"; one gated Keep record | ⚑ Agreed-adjacent (TK Q1 row; domain words unchanged) | **Client (agreed, Q1)**; TK owner applies | Now |
| CA-2 | TK 6.1 item 6; TK 11 item 4; TK 5.5 Feet row; TK rule 15 domain map | Triggers from `charmTriggers`, outcome events from any source; cross-location reads grant Warded or Helped only, never changing how the subject resolves; receivers stay "you or one adjacent ally" (no new target value; after the check) | ⚑ Agreed (TK Q1); ⚑ Settled (AR 13.4) | **Client (agreed, Q3)**; TK, AR owners apply | M3 |
| CA-3 | TK 17.2 `trinketSubjects`/`trinketDomains`; ITEMS 5.1 via TK-11 | `charmTriggers`, `charmOutputs` vocabularies (with `crossLocation`, and after the check `actor`, `repeat` and the 4.3.1 predicates); `charm{}` block | — | Lead | Now (spec) |
| CA-4 | TK 13.9; TK 6.2 | "Spurred is never a trinket output"; excluded-output list (a narrowing) | — | Lead | Now |
| CA-5 | TK 7.1; TK-10 greyed reasons; TK 17 event log | `CharmFireOnce`; no-chain; one caught token per output per receiving unit per fight (`CharmCaughtThisFight`); after the check, the 4.6 order (`once` first, then slot order), provenance rule, after-the-use resolution, unit order and one-fight state | ⚑ Fixed frame (TK 24.2) | **Client (agreed, Q2)**; the resolution rules are lead's calls, after the check | M2 simulator-only (shadow records), M3 live |
| CA-6 | TK 15; TK-10 | Party-scope DeadLine; "rarely fires" (by floor band and party size); "no ally to receive"; "shares its Helped"; waits/missed forecasts through party knowledge; forecastHint/plateHint | — | Lead | M2 (plate), M3 |
| CA-7 | TK 8.1 | Catch value 1; self-Warded catch value 1 only if ≤600‰ per elite and boss class *(illustr.)*, else `rest`-charged (Bloodknot `rest` 1/1/2); ally-Warded automatics `rest` 1/1/1 v2 (ABL 6.2 read); after the check, a new TK 8.1 row: "a two-branch catch (two `charmTriggers`, one output, once per fight) is one value-1 property", provided its merged rate passes CG1 (the Visor exception as a data shape; Jolt Knot, Minder's Pebble) | — | Lead | M3 |
| CA-8 | TK 9.1 policies | Helped catches `always`; Warded catches `always`/`spareForButton`; ally-target catches and majors `always` with a fixed receiver, never a prompt (corrected after the check); `onBlocked: defer` with the defer, missed and wasted meanings of 4.5 | — | Lead | M3 |
| CA-9 | CON 5.6; CON 18.9 Helped; W-7 | **Reading, not a text change (new lead's call; 2.3 F8):** CON 5.6 already gives a window opened on the source's own turn its following turn, so a charm-caught Helped gained on the wearer's own turn lasts through the wearer's next turn; the CON owner confirms | ⚑ Settled (CON, read) | **Request** to CON owner (confirmation); fallback: the output record already states the duration (4.5), so a refusal changes nothing in play; report "Helped wasted" and log provenance (after the check) | M3 |
| CA-10 | CL 7.6; ABL 11.4 | Two new bullets (corrected after the check): (1) "a once-per-fight automatic trinket catch with a plate ready pip counts 0" (under today's text a Catch counts 1 and Trophy Tally already puts a Vanguard at 5); (2) an "items row" outside the class four for the one trinket button | ⚑ Settled | **Request** to CL owner + lead; fallbacks: (1) catches count 1, covered by the verdicts; (2) option (b) Vanguard verdict | Before M2 UI |
| CA-11 | TK 17.3; AR 16.3 state table; save schema; suspend journal (SV Q4) | `charmKept` state and the receiver-keyed caught flags; the armed/missed state; the per-(unit, property) policy choice; all keyed to the BR 4.2.5 fight-resources reset; `CharmCaught` event; schema version bump | — | Lead; SV owner applies | M2 simulator-only (shadow records), M3 live |
| CA-12 | TK 5.5, 5.6, 15 display words; name registry (AR 17.1 rule 13) | Drop "talisman" (CN-1, DH-35, ABL 7.2, IR 4.2 fine category); review "token"; register charm names and shape words; flag "Catch" against TL 3.4/6.3 and TL's "Hunch catch" badge, never printed (new lead's call; extended after the check) | ⚑ Agreed-adjacent (TK Q1 table, Kinds cell; display word only; flag added after the check) | Lead; naming pass | M2 text |
| CA-13 | IR 6.2 Patina bullet, IR 6.2 art table, IR schema; RS 8.0 `theme.loot.patina` | Patina "charm" → "accent" | — | **Request** IR, RS | M2 |
| CA-14 | IR 9.4 content requests ("leader-emblem charms") | Fix the pointer (name Muse's Token / Leadership owner, or delete) | — | **Request** IR | M2 |
| CA-15 | TK 13.7; TK 19 | Scrollwright's Token moves to exploration-first Later (CN 25 / ID-3 kept); Later statuses of 5.2 | — | Lead | Now |
| CA-16 | TK 18.3; AR 17.3 | Reports of 9.4; rule 26 on the rate report, read per floor band and party size; per-fight-class, party-size, boss, floor, unlock-set splits; missed rates; bind rates per wearer-fight | — | Lead | M2 |
| CA-17 | TK 18.3 landing envelope row, TK-4 | Clarify the "maximum legal loadout" measurement (binding numbers unchanged); interim: both readings reported, the literal one binds (new lead's call); both loadouts defined (8.3; after the check) | — | Lead (clarification) | Before CG1 |
| CA-18 | ITEMS 19.1 rules 29–31; RS 11.2 suite table; SV 5.2 (V1–V22 → V1–V23); SV 6.10 and 9.1 (report registry), SV 6.2 (arms: the two shadow modes, the forced-loadout human-session arm, the heirloom-boon comparison arm), SV 6.5 (minimum N) | V23 `CharmLane` s/r/t; `simOnly`; `boonEligible`; localisation keys | ⚑ Settled (SV 5.2) | **Request** RS, SV owners | M2 |
| CA-19 | TK 24.2 "When" | Split: framework, reports and shadow records now; content approval after CG1/CG2 | — | Lead (timing is the lead's) | Now |
| CA-20 | TK 24.2 camp bullet; CH 2.2 hook 32; CH 4.9, 4.10 (boon picker); MAIN 4.7 (decided, read); ITEMS 14 (read); SV 4.6 `configHash`; HL 4.1.7 (read) | Bobbin boon type; its earn milestone, choice list, economy flags (`boonCharm`), Due reading, `configHash` entry (family, unit, slot), off on shared, seeded and daily runs (4.11) | ⚑ Decided (read); ⚑ Settled (ITEMS 14, read) | **Client (agreed, Q4)**; request MAIN/CH, SV, LE | M3 |
| CA-21 | MAIN 4.7 bullet 1 (read); SV 4.6 `unlockHash`, `drawableSet`; SV SQ41; CH 4.11 (read) | Charm families as unlocks, two base; unlock-set inputs; charm unlocks use the Library's generic loot-unlock object, so V20s `UnlockHasHubObject` passes (after the check) | ⚑ Decided (read) | **Request** SV; CH owner confirms the reading | M3 |
| CA-22 | ABL 13; ABL 11.3 | Party scorer values expected catches | — | **Request** ABL | M3 |
| CA-23 | CON 18.9 Helped, Warded, Spotted "Sources" (TK-22 pattern); CON 16; after the check, TK 5.5 None row, TK 6.1 item 6, TK 17.2 domain map | List approved charm families; rule Spotted at M4, adjacent-only; a Spotted charm needs a new effect kind and a change to the None lane's domain words | ⚑ Settled (CON 16 budget reading); ⚑ Agreed (TK Q1 lane, for Spotted) | **Request** CON, **then the client** for the lane change; fallback Later | M3 / M4 |
| CA-24 | TK 8.2; TK rule 21; TK rule 28; ITEMS 11; TK 7.2 (read) | TK Q2 closed; ButtonCap 1; no slot change; rule 28 extended (no duplicate (trigger, output); after the check, no rolled catch with the obvious line's output and no Warded catch on a ward-button charm); dormant catch rules (mid-fight reveals inert until the reset); charm string held as a future M4 item | ⚑ Agreed (TK Q2, TK Q3, restated) | **Client (agreed, Q5)** | M3 |
| CA-25 | ITEMS 10.4 (read); IR 4.2; LE 6.2 | Catch charms priced by the existing rule in the camp shop; lane-weight question recorded as a future item (global, IR Q4) | — | Hook / future (14.2) | After CG1/CG2 |
| CA-26 | SK 7 item 2 (worn-slot rows, SK-12); SK-3 issue; SK 4.5.2 (Oddment pool, read); V21 `KitNoTrinket`; LE V13g (g); HL 4.6 / HL-26 (read) | **Added after the check:** a worn-slot row for the Bobbin's charm (the hero's first empty None slot, issued at WO1 through SK-3, no displacement); a `KitNoTrinket` waiver limited to the `boonCharm` instance; a Bobbin run draws the default kit's Oddment pool; a V13g (g) case asserting a `boonCharm` instance never sets `armsSatisfied` | ⚑ Settled (V21, HL-3 predicate read) | **Request** SK, LE owners | M3 |
| CA-27 | XI `SurfaceRecord` registry (V18s `SurfaceRegistered`); XI `budgets.plateMarks` (read); RS RC5 pattern registry; RS timbre table; `ReservedCues`; the 85% audio blind-ID gate | **Added after the check:** register the charm pip pair (one plate mark), the forecast line and the fire beat as XI surfaces; the five pip states in the pattern registry, greyscale-gated against the Windfall mark and the Lodestring spool; two output-cue rows in the timbre table with their "kept out of" families, tagged for `ReservedCues` and run pairwise in the blind-ID gate | — | **Request** XI, RS and audio owners | M2 (Tally's pips), M3 |

---

## 14. Open items for the client

None. Approved by the client on 2026-11-24.

The client approved this document as a whole on 2026-11-24, including the lead's answers of 0.1 and the frame of 3.3 (which the client had accepted by proceeding); every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the 33 lead's calls after the check in 14.3); the told-plainly items (those in 14.1 and "Told plainly after the check" T1–T17 in 14.3, all as written); and the readings recorded in 13; and its approval covers the flagged rows of 13. The five questions raised before writing were answered on 2026-11-24 **(agreed)**, each with the recommended option (A). Writing raised **no new client question**: every new lead's call sits inside the five answers, corrects the proposal's own text against an approved rule, or is flagged against the text it touches in 13. **The design check raised none either**: every lead's call after the check (14.3) sits inside the five answers, keeps an approved rule, or is flagged against the text it touches; those that change what the client may have pictured are told plainly (T1–T17). The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them (including the owner-refusal fallbacks, the report-triggered items and the two later routes to the client for an Inspired charm and Spotted, T13 and T14), and the naming pass remain future items.

### 14.1 This document

**The five answers (agreed, 2026-11-24, final), each the recommended option:**
- **Q1, lane wording and shapes (CA-1):** option A, TK 5.5's shape cell restated as "Charms catch moments: a token earned from a named moment in the fight, or one button you spend"; Catch is the growth shape; one gated Keep record (Ashen Knot; renamed the Ashen Bead after the check, T5); "stored moments" stays as the domain words. Rejected: B, current wording, Tally-shaped catches only, no Keep; C, buttons only.
- **Q2, new TK 7.1 limits (CA-5):** option A, `CharmFireOnce` plus no-chain plus one caught token per output per unit per fight. Rejected: B, the first two only; C, no new limits.
- **Q3, cross-location triggers (CA-2; amends agreed TK Q1 domains):** option A, allowed to grant Warded or Helped only, never changing how the subject resolves; the base batch stays domain-clean except Jolt Knot's danger branch. Rejected: B, domain-clean only.
- **Q4, charm boon (CH-24; CA-20):** option A, the Underpillow (placeholder; now the Bobbin, T4) at M3: choose one unlocked Common catch at run start; never a button, Keep, Uncommon, heirloom, persistent or cursed; gated on a parity report and a boon pick share of at most about 40% *(illustr.)*. Rejected: B, no charm boon.
- **Q5, rarity, ButtonCap, slots (CA-24):** option A, TK Q2 stays closed (no Rare charms), ButtonCap stays 1, no slot change; the charm string held as a future M4 item. Rejected: B, Rare charms with `RareWornCap` 1; C, a charm string outside the slots.

**The lead's answers (0.1) and the frame of 3.3** were accepted by the client by proceeding and confirmed by the client on 2026-11-24, with this document, including every told-plainly item below.

**Told plainly (confirmed by the client on 2026-11-24, with this document):**
1. "Small passives" fits only as "small moments"; a "+1" reading is banned and stays banned (0.1 item 1).
2. Outputs stay Warded and Helped; expect many charms to read "you gain Helped" until Spotted is ruled (CA-23). Variety comes from what each charm catches and who receives it (4.4).
3. The four drafts and three judges declined a charm boon for a budget reason that approved rules contradict; the lead corrected it, and the client chose the Bobbin (4.11; agreed, Q4).
4. TK 24.2's timing is split: framework, reports and shadow records now; content waits for M2 data (CG1, CG2) (CA-19, 10).
5. A third of runs may see no charm *(illustr.)*; this is an economy question for later, not a reason to add slots (2.3 F11, 4.10).
6. The numbers pass's "approved Wardstone fails the envelope" is a toy result; it is a measurement question to the lead (CA-17), not a finding (8.3).
7. Scrollwright's Token leaves the charm lane for exploration-first trinkets (CA-15, 5.2).
8. "Talisman" leaves charm cards; the Patina "charm" becomes "accent" (CA-12, CA-13).
9. Rejected candidates and their reasons (5.3), including several the drafts proposed: Near-Miss, the Blink Bone re-key, Milk-tooth, Press-Stone, Bier-Knot, Gallows Penny (standalone), Held-Breath Knot.
10. The new 7.1 limits and the cross-location reading were the client's to decide, not the lead's (answered, Q2, Q3).
11. The drafts' "Prone-keyed Helped idles" argument was wrong (4.3).
12. The drafts' items-row remedy for the button read still added 1 (4.7).
13. CG2's "press rate ≥0.5 per fight" was the wrong metric for rest pools; it is restated as a rest-charge spend share (10).
14. The proposal misquoted CON 5.6; the approved text already handles own-turn Helped (2.3 F8; found while writing).
15. "Catch" is also TL's trap kind; the shape word may change at the naming pass (2.3 F13; found while writing). After the check it is never printed to the player at all (T12).

**New lead's calls (made while writing, beyond the answered proposal; confirmed by the client on 2026-11-24, with this document):**
1. **CA-9 becomes a reading (F8):** CON 5.6 already says a window opened during the source's own turn runs to its following turn; CA-9 asks the CON owner only to confirm; the "Helped wasted" report and provenance log stay (2.3, 13).
2. **"Catch" flagged against TL's trap kind (F13):** the naming pass rules the display word before M3 card-text freeze; fallback, the card drops the word and keeps the ◈ glyph; V23s flags it until then; Lockjaw's hint reviewed (2.3, 4.2, 5.1, 9.3). *Superseded after the check (14.3 item 13; T12): the word is never printed on any surface.*
3. **TK-4 interim (F14):** until CA-17's clarification, CG1 reports both readings and the literal maximum-loadout reading binds (2.3, 8.3, 10).
4. **The Bobbin and the Due (F15):** party-owned for the Dream's Due's owned and duplicate checks; never F1 hero arms; never the HL-3 waiver; replaces the proposal's "counted as owned for the F1 Due" (4.11, 13).
5. **The Bobbin's choice list (F16):** Trophy Tally, the M3 base families and every shipped unlocked Common Catch; never `simOnly`, HOLD, Spend or Uncommon; at least three entries at M3 (4.11).
6. **The Bobbin's seed place:** the chosen family id enters `configHash` as the boon choice, as an heirloom template id does; V23t checks it (4.11, 9.3).
7. **The Bobbin instance's economy flags:** outside the floor loot budget, display-only value, unsellable, never a Curiosity price, lost at the stairs if left; an ordinary charm in every rule, including the per-output cap of 4.6 (4.6, 4.11).
8. **The boon's picture:** no hub object (CH-24); the boon picker shows the chosen charm's icon on a boon-card frame (6). *Amended after the check (T4): the pillow-shaped frame is dropped; an existing frame with no bed, pillow or child motif.*
9. **`crossLocation` tokens in data:** marks `movedHostileEndsInDanger` and `hostileProneAdjacent`; rule 30 and V23s enforce Q3's grant-only rule (9.1, 9.2).
10. **`boonEligible` derived, never hand-set** on HOLD or `simOnly` records (9.1, 9.3).
11. **Jolt Knot read per floor:** on F1 only its stopped-early branch can fire, so its rate report and "rarely fires" verdict use the floor's own rate (8.1).
12. **The caught-token flags** live in the per-unit fight state, in the save and the suspend journal with `charmKept` (9.1, CA-11). *Amended after the check (14.3 items 3, 6): keyed on the receiver, and reset only by the BR 4.2.5 fight-resources reset event, not at fight start.*
13. **Goal labels G-a to G-e** in 0.4, so goals never read as the data gates (now CG1–CG3).
14. **Citations corrected from the proposal's line numbers to sections:** CON 5.6 (not "l.330"), W 7.5 step 2 (not "W l.781"), IR 6.2 and 9.4 (not "l.835", "l.1076"), TK 13.7 for Tally's blocked rule (not "TK l.982"), TK 18.3 (not "l.1418"); the CL 7.6 automatic-reaction bullet quoted in full.

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a lead's call (confirmed by the client on 2026-11-24, with this document) unless marked.

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| **Lane weight** | CG1/CG2 show P(0 charms) above ~25% *(illustr.)* on the canonical unlock set | A global charm-lane weight change inside the unchanged trinket VP share, never theme-keyed (IR Q4) | IR/LE owners + client |
| **`CharmTriggerCap`** | Plate reads exceed 4, or two-charm units bind often | A per-round charm cap | Client |
| **Wardstone supersession** | Rest-charge spend share below 700‰ *(illustr.)* | New id, `supersededBy`, a Bloodknot-style catch | Lead |
| **Spotted** | CA-23 ruled | Whiff-Mark, Acorn Cap at M4; fallback: Later | CON owner, then client (agreed TK Q1 lane; T14) |
| **C-slot charm string** | M4, if CG3's bind test passes (agreed, Q5) | Evaluation brief to the client | Client |
| **Muse's Token / Inspired** | The Leadership-mastery owner rules | Returns **to the client as a question**: an Inspired charm changes TK 5.5's None "Never here" cell (agreed TK Q1) and TK 6.1 item 6; if allowed, an Uncommon catch, never Rare (corrected after the check; T13) | Leadership owner, then client |
| **Read ruling refused** | CL owner declines CA-10 part 2, option (a) | Option (b): the button counts 1, with a Vanguard warning verdict | CL owner + lead |
| **Catch read refused** | CL owner declines CA-10 part 1 | Catches count 1; the "rarely fires" and Vanguard read verdicts cover it; the plate-read probe (CG2) decides whether `CharmTriggerCap` is raised (after the check) | CL owner + lead |
| **CA-9 refused** | CON owner reads CON 5.6 otherwise | Nothing changes in play: the output record states the duration (4.5); "Helped wasted" and provenance are still reported (after the check) | CON owner |
| **Bobbin refused or over gate** | MAIN/CH decline, or pick share >40% or win delta >+3 points | The boon is withdrawn or narrowed (fewer families on the list) and Q4-B's outcome applies | Lead; client informed |
| **Bobbin under-picked** | Pick share below the floor (≥5% of boon choices among profiles that unlocked it *(illustr.)*) at M3 | The lead withdraws it or changes its presentation (picker placement, fit verdict); never its rarity or value (agreed, Q4) (after the check) | Lead; client informed |
| **SK or LE owner refuses CA-26** | The worn-slot row, `KitNoTrinket` waiver or V13g case is declined | The Bobbin is held until it can be placed without breaking V21 (the "Bobbin refused" row's outcome until then) (after the check) | Lead; client informed |
| **Setup share short** | The CG1 survivors leave the shipped M3 batch under half setup | Wishbone or Meeting Stone forward if they pass; otherwise ship with the shortfall told to the client at CG3 (4.3 rule 4; after the check) | Lead; client informed |
| **Keep (Ashen Bead) fails CG2** | Its comprehension probe or spend share fails | Moves to M4 or is dropped | Lead |
| **Second's Hairpin** | The lead's ally-Helped ruling | Adopted for M3 unlocks, or rejected as a Help-whistle copy; if rejected, Meeting Stone takes its setup place (after the check) | Lead + class owners |
| **TK Q2 reopened** | The client reopens Rare charms | Comes with `RareWornCap` 1 per unit | Client |

### 14.3 After the design check

The design check (builder, consistency, player and art-and-audio lenses) was answered inside the client's five answers. No client answer is overridden; where a fix narrows or re-reads an answer's wording, it is told plainly below. **No new client question was raised.** Two later routes to the client are made explicit (an Inspired charm, T13; Spotted, T14); both are future items that come back only if their owner moves.

**Lead's calls after the check (confirmed by the client on 2026-11-24, with this document):**
1. **After-the-use resolution:** qualifying events are queued, and catches resolve only after the triggering use (W 7.5 step 15; ABL 14.3 step 6; the full conditions pipeline and TK 7.3 for a stand-alone application; each reaction as its own use); a token never affects the rest of the use that caused it (4.6 item 4; V23t; T15).
2. **A total order:** core event sequence, then ascending unit order of the wearers (TK 7.3 step 3), then the `CharmFireOnce` order; shared caps read and written in that order (4.6 item 5).
3. **One fight in the Sanctum:** every charm per-fight state, and the Keep fill, reset only by BR 4.2.5's fight-resources reset event (4.6 item 7; 9.1; V23t; T16).
4. **Blocked catches made exact:** `once` and `recurring` tokens; defer (still armed) only for a recurring token under a block that can lift this fight; **missed** (spent pip, a log line, no pulse) otherwise; `CharmFireOnce` takes `once` tokens first, then slot order, skipping blocked candidates; "wasted" only for a granted token that expires unconsumed, never a forecast; "already advantaged" is not wasted (4.5, 4.6 item 1, 7 item 11; T9).
5. **Provenance:** trinket-caused only with a `TrinketPropertyFired` root; modifiers never taint; a combined forced move is player-caused if any non-trinket part contributes (4.6 item 2; V23t).
6. **Receiver-keyed per-output flags,** set only by Catch grants; Keep fills set nothing; Keep and Spend grants under the existing caps (4.6 item 3; 9.1; T10).
7. **Exact predicates** for every token: core event, fields, state tested, Chebyshev distance (MAIN 4.3), event unit, "first" from the reset, the `harmfulRebuffed` include and exclude list (4.3.1).
8. **The `actor` attribute** and derived `offTurnForWearer`; setup redefined as any controlled party unit's previewed action; a reaction-time forced move shows no forecast (4.3 rule 4, 4.7).
9. **Forecast rules:** shown when any roll outcome could satisfy the predicate, phrased conditionally, computed only through `IKnowledgeQuery` and party knowledge with W 7.2 rule 7's landing square, caps read with the token assumed; `KnowledgeLeak` cases (4.7; V23t).
10. **Mid-fight reveals inert** until the reset (4.6 item 6; 4.8; V23t).
11. **Keep mechanics:** the fill spends nothing, the button spends the rest charge, one fill per fight, later events ignored, an unspent fill costs nothing, a greyed Keep never fills (4.2).
12. **Hint fields by shape and class;** `plateHint` is TK 9.1's previewHint under its serialized name; V23s checks the table (9.2).
13. **TK 15's card line format** (keyword, "once per fight, automatic:"), the glyph carrying the shape, "Catch" never printed on any surface, a length row, the boss note moved to Inspect and the Sanctum card note (4.2; 2.3 F13; 5.4; T12).
14. **The boon's placement and earn rule:** earned by a milestone; the hero; WO1 through SK-3 into an empty None slot; `boonCharm`, never `heirloom`, with a V13g (g) case; the default kit's Oddment pool; off on shared, seeded and daily runs; the launcher checks `drawableSet`; offered in the launcher or Begin again picker as a list with fit verdicts; a pick-share floor report (4.11; CA-26; T4).
15. **Ally-target policies:** `always` with a receiver fixed by the trigger, never a prompt; the per-(unit, property) policy choice is saved state (4.5; CA-8, CA-11).
16. **Shadow modes:** an observer on cloned state and a forced-loadout arm with a same-seed no-charm partner; forced-loadout human sessions as a registered SV arm with the loadout in `configHash`; Bobbin pick share from human sessions and telemetry, win delta against a heirloom-boon arm (9.1, 9.3, 9.4, 10).
17. **Computable gates:** rule 26 per floor band and party size, a candidate passing on F2+ at size 4; bind rates per wearer-fight; both CA-17 loadouts defined; always-fires is a gate; missed fires count as non-fires (8.1, 8.3, 10; T17).
18. **Staging:** the CA-5 limits, the 4.6 resolution rules and the CA-11 state run simulator-only at M2 for the shadow records, live at M3 (10, 13).
19. **Bloodknot shadowed `rest`-charged** (`rest` 1/1/2, value 1); 8.2's "equal value" qualified (4.5, 5.1, 8.2; T6).
20. **Two-branch catches** as one value-1 property, a new TK 8.1 row in CA-7 (4.5; 13).
21. **Trophy Tally unfiltered;** `nonMinion` a separate data filter (4.3 rule 3; T7).
22. **Second's Hairpin narrowed** to an adjacent killing ally; `catchAlly` withdrawn (4.4, 5.1, 9.1; T8).
23. **Rule 28 extended to outputs:** no rolled catch with the obvious line's output; no Warded catch on a ward-button charm (4.8).
24. **"Shares its Helped" verdict,** the spent state for whole-fight blocks, a party-scope move suggestion and its own V23r line (4.7; T10).
25. **Setup share as a CG3 check,** Wishbone and Meeting Stone added as M3 candidates, Meeting Stone named as Second's Hairpin's replacement (4.3 rule 4, 5.1, 10, 14.2; T11).
26. **The Helped duration stated on the output record,** so CA-9's refusal cannot break setup catches (4.5; 14.2).
27. **The hub-object reading** for charm unlocks (4.10; CA-21).
28. **Inspired and Spotted routed to the client** behind their owners (4.4, 5.2, 14.2; CA-23; T13, T14).
29. **Art:** age-neutral objects; one silhouette class per shipped family, at most two "Knot" families per wave; beats per silhouette class with reduced-motion forms; one pip-pair plate mark with a five-state table, kept apart from the Windfall mark and the Lodestring spool; surfaces and patterns registered (6; CA-27).
30. **Audio:** two output cues on non-reserved placeholder timbres, timbre-table rows, `ReservedCues`, the blind-ID gate, played only on a charm catch; Tally's re-map at M3 (6; CA-27; T3).
31. **Localisation:** whole strings per record × line × surface, named placeholders with translator notes, one forecast template, whole waits and missed strings, a list-layout picker, literal hints, a recount (6, 5.4).
32. **Names:** the renames of T4 and T5; the greyed reason in player words (5.1, 5.4, A.2).
33. **Register hygiene:** the CA-12 flag; CA-23's added sections and flag; CA-5, CA-11 waves; the SV citations (6.10, 9.1, 6.2, 6.5) and RC4 with RS 7.4 for plate states; the gate ids CG1–CG3; CA-26 and CA-27 added (13; T1).

**Told plainly after the check** (none overrides a client answer; confirmed by the client on 2026-11-24, all as written):
- **T1. The data gates are renamed CG1–CG3.** "G1–G3" are already ids in DH, CMP, EP, W and HL, and SV rejects single-letter numbered schemes for that reason. The gates themselves are unchanged.
- **T2. Under today's CL 7.6 a Catch counts 1, not 0.** The proposal read it as a pool-free automatic reaction; it is neither. So the approved Trophy Tally already puts a Vanguard at 5 reads, and the button is not the only gap. CA-10 now asks the CL owner for a Catch bullet as well as the button bullet; if refused, catches count 1 and the verdicts cover it.
- **T3. The two sounds the proposal described cannot be used.** Bells are reserved cue sounds, and the dry-breath family is the Heart Stir cue (no breath-family sample anywhere, CH AN9). The placeholders are now a soft mallet tap on a hollow gourd (Warded) and a marble rolling to rest in a bowl (Helped), pending the audio owner's confirmation (CA-27). Trophy Tally's sound changes at M3, not M2.
- **T4. The Underpillow is renamed the Bobbin, is earned, and goes to the hero.** "Pillow" names the Heart's harmful dust-pillow Keg, and the under-pillow, milk-tooth and swaddling imagery implies the Dreamer is a child, which RS 7.5 forbids. MAIN 4.7 says boons are earned, so the Bobbin unlocks when a charm first fires in a won fight; a new profile's first runs do not offer it. It is off on shared, seeded and daily runs (as heirlooms are). Q4's rule, never-list and gates are unchanged.
- **T5. Several placeholders were renamed:** Widow's Knot → Minder's Pebble (gendered, and on a Downed trigger it implied a death); Second's Knot → Second's Hairpin; Swaddle Knot → Walnut (age imagery); Ashen Knot → Ashen Bead (Q1's one Keep record, the same rule); Calm Stone → Shy Stone (Calm Emotions, Calm Minds); Pointing Bone → Acorn Cap (a living people's practice); Hand-me-down Knot → Relay Thread and Cradle Knot → Stay Thread (age imagery). Six of the seven M3 records were knots, which would make their icons differ by hue only. All are still placeholders for the naming pass.
- **T6. Bloodknot, as the catalogue listed it, already failed this document's own per-fight test** (about 900‰ in elite and 950‰ in boss fights on the toy). It is shadowed only as a rest-charged ward (`rest` 1/1/2).
- **T7. Trophy Tally keeps its approved, unfiltered trigger.** The draft's token wording added a minion filter that would have changed an approved M2 line.
- **T8. Second's Hairpin helps only an adjacent killer.** The draft's "that ally" could be at range, which no trinket target allows.
- **T9. A once-per-fight catch can be lost.** If its one moment comes while it is blocked (the unit already had a trinket ward this fight, or another charm fires on the same event), it is missed for that fight and shows as spent, never as "waiting". To lose fewer of them, `CharmFireOnce` now picks a once-per-fight catch ahead of a repeating one, whatever the slot order.
- **T10. Two Helped catches on one unit share one catch a fight.** Q2's "per unit" is read as the unit that receives the token; once one Helped catch fires, the other is spent for the fight. The rest screen says so, and suggests moving one to a companion.
- **T11. The M3 batch as first listed had two setup catches out of seven,** against this document's own "at least half" aim. That aim is now a check on the shipped batch; Wishbone and Meeting Stone are added as M3 candidates; a short batch ships only with the shortfall told to you.
- **T12. Charm cards use the approved trinket line format,** and the word "Catch" (a trap kind and a Hunch badge) is never shown to the player anywhere.
- **T13. An Inspired charm (Muse's Token) would change the agreed charm lane,** so it would come back to you as a question, not as the lead's adoption.
- **T14. A Spotted charm also changes the agreed charm lane** (a new kind of effect on a hostile), so CA-23 goes to the CON owner and then to you.
- **T15. A caught token never helps the rest of the use that caused it.** A Bloodknot ward gained from a hit's damage does not block that same hit's rider, and a Helped from a drop does not advantage that swing's Cleave.
- **T16. In a boss Sanctum, charms follow the one-fight rule.** Stepping out and back does not refresh them, and a filled Keep is kept.
- **T17. Jolt Knot fails the "fires for two kinds of unit" test on the first floor,** where only its stopped-short branch can fire. It ships on its deeper-floor reading, with a "rarely fires" note on F1. Every candidate is now read per floor band and per party size (1 and 4).

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | Output monotony ("you gain Helped" everywhere) | Second's Hairpin (ally target); Warded and major records in the batch; Spotted request; "tell your charms apart" probe |
| 2 | Visibility (P(0) ≈ 33% *(illustr.)*) | Base families; Bobbin (agreed, Q4); lane-weight future item |
| 3 | Always-fires or never-fires catches | Per-role, per-fight-class CG1; merges; `nonMinion` re-run |
| 4 | Helped into Exploit precision | Per-output cap (agreed, Q2); precision ceiling report |
| 5 | Ward catches vs envelope | Per-fight-class value test; `spareForButton`; CA-17 with the literal-reading interim; CG1 |
| 6 | Helped expiring unseen | CA-9 reading; provenance log; "Helped spent on attack" gate |
| 7 | Read count (Vanguard 5, with Tally already today) | CA-10's two requests before M2 UI; verdicts as fallback |
| 8 | Trigger chasing as tedium | Playtest probe; scorer request so suggestions do the bookkeeping |
| 9 | Copy-registry collisions (Twitch, Lockjaw, Shy, Quietus, Second's) | Held until rulings; outcome keys |
| 10 | Solo parties | "No ally to receive" verdict; party-size reports |
| 11 | Unlocks distort new-profile data | Two base families; minimal-set reports |
| 12 | Shadow records leaking | `simOnly` V23s check, including the boon list |
| 13 | Statistically thin playtest | Forced-loadout sessions; minimum N; "cannot fail for lack of data" |
| 14 | Localisation fragments | Whole-string keys per record × line × surface; CardFit; length row |
| 15 | Starting before data needs revision later | Only framework now; content waits |
| 16 | Survey details from memory | Marked (unsure); no rule depends on them |
| 17 | The Bobbin becomes the default boon | ≤40% pick-share gate *(illustr.)*; `HeirloomWinDelta`-level win cap; the 14.2 fallback |
| 18 | Cross-location reads erode the agreed domains (Q3) | Grant-only rule; `crossLocation` tag; rule 30; one cross-location token in the base batch |
| 19 | The "Catch" word collides with TL's trap kind and Hunch badge | Never printed on any surface (after the check); naming pass for the design word |
| 20 | Once-per-fight catches lost to blocks read as broken charms | Missed state shown as spent, with a log reason; `once` before `recurring` in `CharmFireOnce`; missed rate in V23r (after the check) |
| 21 | Two same-output catches on one unit feel like a dead slot | "Shares its Helped" verdict; move suggestion; V23r line (after the check) |
| 22 | Non-replayable or implementer-dependent catch results | After-the-use resolution; a total order; provenance rule; exact predicates; V23t cases (after the check) |
| 23 | Sounds or images read as the Dreamer or as reserved cues | Age-neutral objects and names; output cues outside reserved families, blind-ID gated (after the check) |
| 24 | The Bobbin is under-picked (dead UI) | Pick-share floor report; presentation fallback (after the check) |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: fire-rate thresholds (333‰ floor reading, 850‰ always-fires, 600‰ self-ward test), spend-share 700‰, pick-share 40% and the 5% floor, plate-read and probe percentages, minimum N, the M3 batch's setup share, art and writing counts, the output-cue timbres (with the audio owner), the toy numbers of 8.
- Godot scene structure for the plate pip pair, fire beats, forecast lines and the boon picker, with no rules state (the display never holds rules).
- Line text beyond the samples, inside the card grammar and the localisation charter.
- Names, until the naming pass (A.2 flags, the "Catch" design word first; never printed meanwhile).
- **Not tuning:** ButtonCap 1 **(agreed TK Q3; agreed, Q5)**; no Rare charms **(agreed TK Q2; agreed, Q5)**; 2 charms per unit, no slot change **(agreed, Q5)**; the three new limits **(agreed, Q2)**; cross-location reads grant tokens only **(agreed, Q3)**; the lane wording and one gated Keep record **(agreed, Q1)**; the Bobbin's never-list (button, Keep, Uncommon, heirloom, persistent, cursed) **(agreed, Q4)**; the landing envelope numbers 600‰/500‰ (binding TK-4); outcome keys, never ids; combat events only; no RNG.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **TK owner (lead):** CA-1 to CA-8, CA-11, CA-12, CA-15 to CA-17, CA-19, CA-24; the copy-registry and ally-Helped rulings.
- **AR owner:** CA-2 (AR 13.4 read), CA-11 (AR 16.3 state table).
- **ITEMS owner:** CA-3 (ITEMS 5.1), CA-18 (rules 29–31), CA-20 (ITEMS 14 read), CA-24 (ITEMS 11), CA-25 (ITEMS 10.4 read).
- **CON owner:** CA-9 (confirmation), CA-23 (Spotted at M4; Sources rows; then the client for the lane change).
- **CL owner:** CA-10 (both bullets).
- **ABL owner:** CA-22.
- **IR owner:** CA-13, CA-14, CA-25 (lane weight, with LE).
- **LE owner:** CA-20 (Due reading; LE 6.5 parity), CA-25, CA-26 (V13g (g) case).
- **RS owner:** CA-13 (`theme.loot.patina`), CA-18 (RS 11.2), CA-27 (pattern registry, timbre table).
- **SK owner:** CA-26 (worn-slot row, `KitNoTrinket` waiver, Oddment pool).
- **XI owner:** CA-27 (`SurfaceRecord` rows, plate-mark budget read); the forecast's `KnowledgeLeak` cases.
- **SV owner:** CA-11 (save and journal), CA-18 (SV 5.2; SV 6.10, 9.1, 6.2, 6.5), CA-20 (`configHash`), CA-21 (`unlockHash`, `drawableSet`); the forced-loadout human-session arm.
- **Main design and CH owners:** CA-20 (the Bobbin under the one-boon rule, earned; CH 2.2 hook 32; CH 4.9, 4.10 picker), CA-21 (MAIN 4.7 bullet 1 read; CH 4.11 hub-object reading).
- **Class owners (VG, SH, ARC, HR) and CMP owner:** copy-registry rulings for held rows; companion turn-time reports.
- **The simulator:** V23r, the 9.4 reports, the shadow records, CG1.
- **Human sessions (SV human-session plan):** CG2 forced-loadout sessions, comprehension probes, the Bobbin pick share.
- **Art, audio and writing:** 6; localisation keys; CA-27 with the audio owner.
- **Naming pass:** every placeholder, with the A.2 flags and the "implies the Dreamer's age" check; the "Catch" design word first.

---

## Appendix A: critique resolutions and names

### A.1 Critique resolutions

| Critic finding | Resolution |
|---|---|
| Boon decline rests on a false budget argument (consistency, high) | Corrected; Bobbin (4.11; agreed, Q4) |
| 7.1 additions are fixed-frame changes (consistency, high) | Sent to the client; agreed, Q2; CA-5 ⚑; DeadLine stays lead's |
| Feet-domain triggers dressed as a reading (consistency, high) | Sent to the client; agreed, Q3; CA-2 ⚑; domain-clean base batch except Jolt Knot's danger branch |
| Items-row remedy still adds 1 (consistency, medium) | Option (a) placed outside the class four (4.7) |
| Prone "idles" wrong (consistency, medium) | Dropped; Prone held for skew (4.3) |
| Name collisions Recoil, Lodestone, Spite, Catch, Held (consistency, medium; coverage, high) | Renamed or avoided; "Catch" to the naming pass, now also flagged against TL (A.2) |
| Wardstone fails TK-4 overstated (consistency, medium) | Question CA-17 with an interim (8.3) |
| Per-role floor "new" overclaimed (consistency, medium) | Restated as rule 26 on the existing TK 18.3 report |
| Scrollwright drift; IR 9.4 pointer (consistency, medium) | CA-15 move; CA-14 request |
| CA numbering and V23 name drift (consistency, player, coverage, low) | One register CA-1–CA-25; V23 `CharmLane` |
| Lane weight must be global (consistency, low) | 4.10; IR Q4 cited |
| Helped can vanish unseen (player, high) | CA-9 (now a reading of CON 5.6); provenance log; spend gate |
| Glint reading wrong for reactive catches (player, high) | setup/reactive class; plateHint; a CL 7.6 Catch bullet requested (CA-10 part 1; corrected after the check, T2) |
| Gates unmeasurable (player, high; coverage, medium) | Rest-charge spend share; forced-loadout sessions; minimum N |
| Blink Bone re-key teaches bad play (player, medium) | Rejected; Twitch Bone (Dodge) held |
| Minder's Pebble union always-fires (player, medium) | HOLD; recompute; two narrowed variants |
| Ward catch vs saved ward button (player, medium) | `spareForButton`; party DeadLine; greyed reason |
| Blocked catches undefined (player, medium; coverage, high) | `onBlocked: defer`, with the defer, missed and wasted meanings made exact after the check (4.5) |
| Monotony (player, medium) | Second's Hairpin; batch mix; probe; Spotted request |
| Per-role floor kills expression (player, medium) | 1 class + (2nd class or `anyCompanion`) |
| Onboarding (player, low) | `uiMemory` callouts; Tally teaches |
| Unlocks break new-player data (coverage, high) | Two base families; unlock-set reports; `unlockHash` |
| V23 registration and phase split (coverage, high) | CA-18; V23s/r/t |
| Dormant lines (coverage, high) | 4.8 |
| Minion filter (coverage, high) | `nonMinion` default |
| Talisman also IR 4.2; Patina art table (coverage, medium) | CA-12, CA-13 widened |
| Localisation (coverage, medium) | 6, CA-18 |
| Accessibility and ask-me prompts (coverage, medium) | 4.5, 4.7 |
| Preview for hostile-caused catches (coverage, medium) | forecastHint/plateHint |
| Party size and solo (coverage, medium) | Verdict; reports |
| Shadow records and Wardstone versioning (coverage, medium) | `simOnly`; `supersededBy`; `charmKept` in save/journal |
| Sources thin; camp shop; λ method (coverage, medium) | 4.10; 8.6 |
| Bosses, events (coverage, medium) | Per-boss/phase reports; event line struck |
| Art states and audio re-map (coverage, low) | 6 |
| The design check (builder, consistency, player and art-and-audio lenses; after the check) | Answered in place; every fix listed in 14.3 with T1–T17; CA-26 and CA-27 added |

### A.2 Names checked (case-insensitive, whole word and substring, the 33 approved documents and the reviews; re-run while writing, 2026-11-24, and again after the check)

- **Prefixes:** `\bCA-[0-9]+` 0 hits (CA-26 and CA-27 included); `\bV23\b` 0 hits; `\bCG[0-9]` 0 hits (the data gates, renamed after the check: `\bG[1-3]\b` has hits in DH, W, CMP, EP, BR, EV, CH, HL and SV, and SV rejects single-letter numbered schemes); ITEMS 19.1 rules 29–31 unused (rules end at 28).
- **0 hits, used:** Hangnail, Jolt (Jolt Knot), Minder's Pebble, Second's Hairpin (Hairpin 0), Walnut, Ashen Bead (Ashen 0), Wishbone, Lockjaw, Quietus, Relay Thread, Stay Thread, Whiff (Whiff-Mark), Bobbin, Meeting Stone, Shy Stone, Twitch Bone, Acorn Cap (Acorn 0), Shaper; the objects nail paring, pebble, hairpin, walnut, cinder bead, pressed flower; the timbres gourd, mallet and marble; the identifiers `charmKept`, `readyDisplay`, `spareForButton`, `charmTriggers`, `charmOutputs`, `CharmFireOnce`, `CharmCaught`, `CharmCaughtThisFight`, `charmCaughtHelpedThisFight`, `charmCaughtWardedThisFight`, `CharmLane`, `CharmTriggerCap`, `RareWornCap`, `forecastHint`, `plateHint`, `simOnly`, `onBlocked`, `catchClass`, `crossLocation`, `boonEligible`, `boonCharm`, `iconSilhouette`, `offTurnForWearer`; every trigger token of 4.3 (`adjacentHostileDropped`, `harmfulRebuffed`, `forcedMoveStoppedEarly`, `movedHostileEndsInDanger`, `youFirstBloodied`, `allyFirstBloodiedAdjacent`, `allyDownedNear`, `harmfulLandsOnYou`, `missedWhileDodging`, `oppAttackHit`, `hitTwoOrMore`, `allyMovedFoeToYou`, `hostileFrightenedAdjacent`, `hostileProneAdjacent`, `allyKillAdjacent`) and the rejected ones (`attackMissedYou`, `savedAgainstHarmful`, `wardSpentOnYou`, `pressedByTwo`, `endMoveBesideDowned`, `furnishingStrike`); the phrases "small moment", "Charms catch", "output cue" and "fight-resources reset".
- **Hits, used with reason:** Bloodknot (the approved TK Later entry); Trophy Tally, Wardstone, Ward-Knot, Kinstone (approved); `supersededBy` (5, HL's template supersession field: the same meaning, reused); `nonMinion` (43, the approved TK filter, reused); DeadLine (6, the approved TK-10 verdict, extended); `HeirloomWinDelta` (5, HL's report, cited as a cap); Twitch (4, generic prose, no term); Shy (2, RS prose "don't be shy"); "Second's" (generic "second" prose only); "missed", "waits" (generic prose; used as plate and log states, never as ids); `TrinketPropertyFired` (approved TK event, read for provenance).
- **Hits, avoided after the check:** `silhouetteClass` (a CH ambient-figure field) → `iconSilhouette`; "pip bar" (the ENM morale pip bar on plates) → "pip pair"; Underpillow (substring "pillow": the Heart's dust-pillow Keg in ROOM and HZ, and HL's dropped "Cold Pillow") → Bobbin; Widow's Knot (gendered, HL NR3; implied a death, RC9) → Minder's Pebble; Swaddle, Cradle, Hand-me-down, Milk-tooth (imply the Dreamer's age, RS 7.5); Pointing Bone (a living people's practice, HL NR3) → Acorn Cap; Calm Stone (Calm Emotions; the Herald's Calm Minds) → Shy Stone; "Tattler" (TL's Tattle lock); bell, breath, bead shaker, knock foley, felt (audio families reserved or near, 6); "sting" (IR 6.3's boss-banner word) → "output cue"; "hourglass" (TL's Hour-lock glyph; never a plate state).
- **Hits, rejected names (no clash in use):** Near-Miss (1, HZ prose "a near-miss noise"); Ward Echo and Doubled Knot (TK 13.9 cut entries, cited as such); Blink, Milk-tooth, Press-Stone, Bier-Knot, Held-Breath, Gallows Penny, Tally-stick (0 hits; rejected anyway).
- **Hits, avoided:** Recoil (Herald ability), Lodestone (XI-13 retired item), Spite (7), Duck (CON/ABL step), Crooked (HL Crooked Road), Held (CON condition), armed (ENM tag, trap state), Keepsake (72), Pocket (XI Pocket, Deep Pockets), Bedside (66), Sleeper (65), Shoulder (17), Elbow (SK "Felt Elbows"), Kith (11), Bruise (7), Scab (substring of "Worn Scabbard", SK/CMP), Blink (TK Ring of Passage), Milk (rejected entry anyway), Mourner, Witness, Tumbler, Brink, Opportunist, Rebound, Undertow (Arcanist spell), Trophy (as a new prefix), talisman (CN category; CA-12), Holdfast (12 hits).
- **Kept as design and data labels, never printed (flagged):** "Catch" (255 whole-word matching lines; **TL's trap kind Catch**, TL 3.4 and 6.3, new lead's call; **TL's "Hunch catch" badge** in the RC5 registry, after the check; the Antechamber "Catch-up"; the boss "catch square"); "Keep" (generic hits; XI "Keep card"); "Spend" (generic; 5 case-insensitive "Spend:" hits, none a term). Cards show the ◈ and ▸ glyphs and the family keyword instead (4.2).
- **Plate and mark distinctness (after the check):** the charm pip pair is checked against the LP Windfall knotted-sack mark, the XI Lodestring spool ("never a knot"), the ENM morale pip bar and the TL Hour-lock hourglass, in greyscale (6; CA-27).

### A.3 CA cross-reference to the drafts

| This register | Model A | Model B | Catalogue | Model C |
|---|---|---|---|---|
| CA-1 shapes/wording | CA-1, CA-2 | CA-1, CA-2 | CA-1 | CA-1 |
| CA-2/CA-3 triggers | CA-3 | CA-3 | CA-2, CA-3 | CA-2 |
| CA-4 Spurred | CA-7 | CA-4 | CA-4 | CA-4 |
| CA-5 limits | CA-6 | CA-5 | CA-5 | CA-5 |
| CA-10 reads | CA-10 | CA-7 | CA-7 | CA-7 |
| CA-12 talisman | CA-13 | CA-14 | CA-8 | CA-3 |
| CA-13 Patina | CA-14 | CA-15 | CA-10 | CA-10 |
| CA-16 reports | CA-17 | CA-13 | CA-12 | CA-11 |
| CA-18 V23 | CA-15 | CA-11 | V23 | V23 |
| CA-19 timing | CA-16 | CA-10 | CA-13 | — |
| CA-20 boon | CA-12 (decline) | CA-8 (decline) | CA-14 (decline) | CA-8 (decline) |
| CA-22 scorer | CA-11 | CA-12 | — | CA-14 |
| CA-24 Q2/rule 28 | CA-9 | CA-9 | CA-9 | CA-9, CA-13 |

The drafts' local numbers are retired and never cited in this document's text; CA-1 to CA-25 each have one meaning. CA-6, CA-7, CA-8, CA-9, CA-11, CA-14, CA-15, CA-17, CA-21, CA-23 and CA-25 were issued by the synthesis and map to no single draft number; CA-26 and CA-27 were added after the design check. Each CA row is cited in (recomputed after the check):

| Row | Sections |
|---|---|
| CA-1 | front matter, 0, 0.2, 0.3, 2.3, 4, 7, 13, 14.1, 17, A.1 |
| CA-2 | 0, 0.2, 0.3, 4, 13, 14.1, 17, A.1 |
| CA-3 | 0, 0.3, 4, 7, 13, 17 |
| CA-4 | 0.3, 4, 10, 13 |
| CA-5 | 0, 0.2, 0.3, 4, 7, 10, 13, 14.1, 14.3, A.1 |
| CA-6 | 4, 7, 13 |
| CA-7 | front matter, 0.3, 2.3, 4, 5, 13, 14.3 |
| CA-8 | 0.3, 4, 7, 13, 14.3, 17 |
| CA-9 | 0.3, 2.3, 4, 11, 13, 14.1, 14.2, 14.3, 15, 17, A.1 |
| CA-10 | 0, 0.3, 2.3, 4, 10, 11, 12, 13, 14.2, 14.3, 15, 17 |
| CA-11 | 4, 7, 9, 10, 13, 14.1, 14.3, 17 |
| CA-12 | 0, 0.3, 2.3, 10, 13, 14.1, 14.3, 17, A.1, A.2 |
| CA-13 | 0, 0.3, 2.3, 13, 14.1, 17, A.1 |
| CA-14 | 2.2, 2.3, 10, 13, 17, A.1 |
| CA-15 | 5, 13, 14.1, 17, A.1 |
| CA-16 | 2.3, 8, 9, 10, 13 |
| CA-17 | 2.3, 3, 8, 9, 10, 11, 12, 13, 14.1, 14.3, 15, 17, A.1 |
| CA-18 | front matter, 0.3, 2.2, 6, 7, 9, 13, 17, A.1 |
| CA-19 | 0, 0.3, 2.2, 10, 13, 14.1, 17 |
| CA-20 | 0, 0.2, 0.3, 2.2, 4, 7, 13, 14.1, 17 |
| CA-21 | 2.2, 4, 13, 14.3, 17 |
| CA-22 | 2.2, 4, 11, 13, 17 |
| CA-23 | 0.3, 4, 5, 10, 11, 13, 14.1, 14.2, 14.3, 17 |
| CA-24 | 0, 0.2, 0.3, 4, 13, 14.1, 17 |
| CA-25 | front matter, 4, 13, 17, A.1 |
| CA-26 | front matter, 0, 0.3, 2.2, 2.3, 4, 7, 11, 13, 14.2, 14.3, 17, A.2 |
| CA-27 | front matter, 0.3, 2.2, 6, 11, 13, 14.3, 17, A.2 |

## Appendix B: sources

- Corpus: the 33 approved specifications and `docs/superpowers/reviews/2026-11-23-cross-document-comparison.md`; key sections TK 5.5, 5.6, 6.1, 6.2, 7.1, 8.1, 9.1–9.3, 13.7–13.9, 18.3 (TK-4), 24.2; CL 7.6; CON 5.6, 16, 18.4, 18.9; ABL 6.2, 7.3, 10.2, 11.4; W 6.3, 7.5; IR 4.2, 4.3, 6.2, 9.4; LE 6.2, 6.5, 6.12; ITEMS 12.4, 12.5, 14, 19.1; MAIN 4.7; CH 2.2 (hook 32); SV 4.6, 4.8, 5.2; HL 4.1, 4.6, HL-3, HL-26; TL 3.4, 6.3.
- Web (tabletop researcher): Archives of Nethys PF2 talismans; 5E charm summaries (conflicting).
- Game details in section 1 are from memory and general design knowledge, marked (unsure) where uncertain; none is cited as a rule, and each must be checked before it is quoted to anyone outside the team.
- The segment 16 proposal (the synthesis), its three models (A, B, C), the catalogue and the numbers pass, the judges' and critics' reports, and the toy scripts (`toy.py`, `toy2.py`), illustrative only, kept in the design lead's scratch area, not in the repository.
