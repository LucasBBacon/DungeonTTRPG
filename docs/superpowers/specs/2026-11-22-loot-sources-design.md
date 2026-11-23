# Loot Sources and Placement: Design (Dungeon Generation, Segment 7c)

Date: 2026-11-22
Status: Approved, 2026-11-22
Parents (immediate): `2026-11-22-loot-economy-design.md` (loot economy design, "LE", segment 7a, approved 2026-11-22) and `2026-11-22-item-rolls-design.md` (item rolls design, "IR", segment 7b, approved 2026-11-22, **the most recent template**). Both are **the key parents**.
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
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change ids are RT-n),
`2026-11-21-floor-layout-design.md` ("FL", segment 2),
`2026-11-21-room-terrain-design.md` ("ROOM", segment 3; its change ids are TR-n),
`2026-11-21-hazards-design.md` ("HZ", segment 4),
`2026-11-21-traps-locks-design.md` ("TL", segment 5) and
`2026-11-21-encounter-placement-design.md` ("EP", segment 6).

Items marked **(agreed)** are the client's answers of 2026-11-22 to the six questions this design raised (Q1–Q5 before the document was written, and Q6, raised by the check, with the approval of this document; sections 0.2 and 14.1), each answered with the lead's recommendation (option A), or restate a client answer recorded in an approved document. The client also said that everything else in the lead's proposal stands as written: Model A (sockets by room role) as the skeleton, with grafts from Models B and C, the fleeting-loot draft and the catalogue; the 25-kind source taxonomy with ranks; socket derivation and the role table; LootBind square legality, draw-free, after `trapsResolve`; every 7a and 7b channel landing (the LE 6.14 and IR 4.18 hooks); visibility, Proprietor tags, tier marks on hover and the Treasure strip; clutter, the numbered click table, auto-close of quiet summaries, optional Glean, the container count cut and the 7c clock line at net ≤ 0; Furnishing stashes rejected in favour of inset containers; the Windfall in full; traps, hazards, encounters, stealth, Steal and Cow; floor 1; floor 5 and the Viaticum in the F5 lair room by the exit, with the Bedside Stand; determinism (the keyed Windfall grandchild of `lootPlan` with 4 draws on every floor); UI and segment hooks; CT1–CT14 and the five collision fixes; the per-theme catalogue for every theme; readability, art and audio; the new mechanics adopted, held and rejected (Trove, Crannies, Tip-offs, Tattler and Hawker held); the numbers and fixes (toy, PT1–PT15); data, V15 `LootSources`, reports and arms; staging; ownership and hooks; the binding requirements; LP-1 to LP-34; the told-plainly items; the risks; and the appendix resolutions and names. The client approved this document as a whole on 2026-11-22, with Q6 answered with option A.

Items marked **(lead's call)** were filled in by the design lead inside approved rules and were in the proposal the client answered; the client confirmed them all on 2026-11-22, when it approved this document as a whole, and they are binding. Items marked **(new lead's call)** were made while writing this document, beyond that proposal; they are listed in 14.1 and the client confirmed them all on 2026-11-22. Items marked **(lead's call, after the check)** were made when the written document was checked by the builder, consistency, play and art-and-UX lenses; they are listed in 14.1 with the check's "told plainly" items, the client confirmed them all on 2026-11-22, and none changes a client answer. Items marked **(settled)** restate a rule from an approved document; **(decided)** marks a main-design client decision. Items marked **(change LP-n)** amend an approved document and are listed, with the exact sections they touch, in 13; rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there, and the client's approval of this document on 2026-11-22 (with Q1–Q6 where a row names one) covers them. Items marked **(told plainly)** narrow or re-read approved text, contest the brief, the client, a judge or a critic, or state an unwelcome forecast, and were told to the client in plain words (14.1, including those told after the check); the client confirmed them all on 2026-11-22. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q6 of this document (2026-11-22), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-22 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(lead's call, after the check)** | A lead's call made when the written document was checked; never overrides a client answer; listed in 14.1 and confirmed by the client on 2026-11-22 |
| **(change LP-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ there |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, the client, a judge or a critic, or states an unwelcome forecast; told to the client in plain words (14.1) and confirmed by the client on 2026-11-22 |
| **(hook)** | Owned by another segment or design; 7c states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator (8, 9). Never binding |

**The client's brief.** Dungeon generation segment 7c: **loot sources and placement**, the last part of loot. 7a owns value, 7b owns identity, 7c owns where every source sits and what it looks like. Shops stay hooks. The client also asked directly: *should there be loot that disappears after a certain number of turns: a container or one-hit entity that appears in combat or exploration and must be broken or reached in time?* Standing guidance: not an exact 5E port; design is not limited by quantity; the art team is eager; combat is key; contest the client or approved views where warranted.

The client's five answers (Q1–Q5, **agreed**, 2026-11-22) are in 0.2: **fleeting loot is the Windfall Truant, an in-combat M3 pilot on F2–F4 holding a real card from the fixed deck, lost visibly if it gets away (Q1); the pilot runs at party ≥ 3, solo opens on its own simulator arm, duo is held (Q2); the exploration forms (the Ebb and the exploration Truant) are M4 (Q3); the Rabbit, the Crumb Mouse and the Alms Crab are the pilot skins, with the client's other skins re-skinned, kept as M4 Ebbs or excluded (Q4); and the Bedside Stand is the second named exception to "container art never implies contents" (Q5).** The check raised a sixth question, the Windfall frequency; the client answered it on 2026-11-22, with its approval of this document: **about 0.6–0.9 Windfalls per run *(illustr.)* for the M3 pilot, with presence 1000‰ on F3–F4; the simulator owns the figure** (Q6, **agreed**, option A).

**Change prefix.** This design uses **LP-n** ("loot placement"). A case-insensitive grep for `\bLP-[0-9]` across the 25 approved documents finds 0 hits (re-run while writing). This document issues one register, **LP-1 to LP-34** (13); no id has two meanings. The research and model drafts used colliding local LP numbers; all are renumbered into this register (A.6).

**Validator.** **V15 `LootSources`** is free: V1–V7 RS, V8 FL, V9 ROOM, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR; a grep for `\bV15` finds 0 hits (re-run while writing).

**Local id schemes** (each whole-word grep-checked, 0 hits): tensions **PX1–PX18**; toy failures **PT1–PT15**; cross-theme catalogue rules **CT1–CT15** (CT15 added after the check); Appendix A findings **CF** (fun), **CB** (balance) and **CK** (consistency). The draft id "PF" was rejected (7 hits). This document's client questions **Q1–Q6** are local, as in every parent (Q6 raised by the check and answered with the approval of this document).

**Names are placeholders** until the client naming pass. Every new name was grep-checked case-insensitively as a whole word across the 25 approved documents (A.5). While writing, four collisions forced renames **(new lead's call)**: the Truant's route marks are the **scurry trail** (the draft's "route footprints" collides with the Stir and Keg *footprint*, 50 hits); the ledger tag is **`onWindfall`** (the draft's `heldBy` is a VG data predicate); the inset container's RC5 mark is the **wall-set mark** ("inset dashed" is the harm-square rim, ROOM 8.1 and HZ 8); and the escape cue is the **got-away cue** (IR renamed "sting" away from loot cues; "sting" stays the boss and Stir telegraph word).

**Numbers in this document are illustrative** unless a rule says otherwise, and are marked *(illustr.)* where they first appear. Every rate and share comes from the 7c toy (8, Appendix B); the simulator owns every published figure. **One exception (new lead's call):** the pilot's **C = 3** (and the solo arm's C = 4) are the client's agreed values (Q1, Q2) and are not tuning; a gate miss that would move them returns to the client (14.2).

Waves: **M1** slice, **M2** full run, **M3** between runs (profile, unlocks, ladder), **M4** breadth, **Later** **(settled, main design 9)**.

Abbreviations: F1–F5 = floors 1–5; C / U / R / VR = Common, Uncommon, Rare, Very rare (in tier contexts); **C** alone in the Windfall rules = its countdown digit, as in HZ 6.3; **VP** = value points (ITEMS 12.6); "the Due" = the Dream's Due (LE 6.6); **SR0–SR5** = socket risk ranks (LE 6.9 item 1); G, Gb, L, Q = main-path guard post, route-avoidable guard post, lair, quiet room (FL 6.5); HE-n = DH healing-economy reservations; pp = percentage points; ‰ = permille.

---

## 0. At a glance

### 0.1 The lead's answers to the brief

**The design in one line:** *every treasure has a room, a square, a readable risk and a counted click; and one rare, harmless thing per floor wakes when the fight starts and runs for the wall with a card you can see.*

**The client's question: should loot disappear after a number of turns?** **Yes, in one narrow and honest form: the Windfall Truant. It is in-combat, ships in M3 as a pilot, holds a real card from the floor's fixed deck, and is lost for good, visibly, if it gets away** **(agreed, Q1)**.

- **Form.** The Truant is a small, faced, harmless animal hauling a plain sackcloth bundle bigger than itself, such as the Carnival's Conjuror's Rabbit or the Supper's Crumb Mouse. It never wears its theme's faction motif (4.7.1).
  - It lies **Dormant, visible at room reveal**, in a guard post or lair. While Dormant nothing touches it but Steal.
  - When a fight engages its host group it wakes. From round 2 it hops 2 squares *(illustr.)* per Dream step along a route drawn as a **scurry trail** toward a **Rathole** in its own room's wall.
  - Its countdown digit, **C = 3** **(agreed, Q1)**, is the room's one clock (HZ 6.2.2). It shows on its floor ring and on the Dream card, and only ever counts down: a Surprised start or the solo arm's extra step shows as a "held" step, never a higher digit (4.7.4).
  - Once awake, any **action** aimed at it nabs it, as do any area or damage that includes its square and any forced movement. There is no roll.
  - It never fights, never calls help and has no morale. Monsters ignore it.
  - If the fight ends while it is still in the room, it is nabbed: **win the fight, or spend one action.**
- **Contested: it never "appears".** RC1 forbids spawn reads, EP forbids event spawns, and determinism needs a bound square. So the Windfall is pre-placed and seen at reveal. The dream logic is in how it **wakes and leaves**, never in how it arrives (told plainly).
- **Contested: no extra value.** Its card comes **out of the fixed deck**, taken from its host fight's own carried allotment (never the elite's), or else from a plain off-path container. Extra value would break Ledger A ("never grows"), MAIN 4.6 (decided) and "harder is not richer". It would also become expected income that every miss then taxes. The thrill is paid in things that cost no VP: a visible tier, a reserved cue family and a run-record line (told plainly; **agreed, Q1**: option C rejected).
- **What a miss costs.** That card and its coin share, nothing else. n falls by 1 with the log line "The <skin> got away with a <tier word> treasure". This is the **one named exception** to "loot is never destroyed" (LP-2). Kill method still never destroys anything.
  - The Dream's Due, the DH package, the Viaticum, keys, signatures and secrets' cards can **never** be on a Windfall (V15).
  - The realistic expected loss is about **0.1–0.15 VP per run** *(illustr.)*. It is a tempo decision rather than a value tax, which is right under permadeath.
- **How often.** At most 1 per floor, on **F2–F4 only**, with a draw-free carry to the next floor when a host is lost. The proposal's toy gave about **1.2–1.4 per run** *(illustr.)*, the figure the client agreed with Q1.
  - **Told plainly after the check: that figure is out of reach as agreed.** The toy assumed party 4 on every floor and a Truant skin on every theme. Under the companion ladder (CMP 7.1: party at most 1 / 2 / 3 / 4 / 4 on F1–F5) and Q2's party ≥ 3, **F2 is never eligible unless the solo arm opens**, F3 is eligible only for a party with two companions by camp 2 (a hire at each camp, or a carried hire), and F4 only for a party of 3 or more. Under Q4 only the Choir, the Carnival and the Supper have M3 skins, so **Silk and Gut floors host no Truant** (4.7.2), and under RS Cut 2 (no Supper) F4 never does.
  - The lead therefore raises F3 and F4 presence to 1000‰ *(illustr.)* (lead's call, after the check). The lead's re-estimate for an always-hiring party is about **0.6–0.9 per run** *(illustr.)*, about 0.3–0.5 at the old 500‰, and lower for a player who declines a hire. The simulator owns the figure (9.4). **The client accepted this frequency for the M3 pilot on 2026-11-22** **(agreed, Q6**, option A; 14.1**)**.
  - It never appears on F1, F5 or trimmed floors.
  - It never appears in the elite room, the arena, the Forecourt, Sanctum or Antechamber, Circuit rooms, ambush forms, Kill Box, Crossfire or Pincer, Wager, Dare or Strongroom Sites, or any room with another clock.
- **Fairness.** It deals no damage, so Warned or floored and the EnvRound cap are untouched.
  - Every hop is outlined a full turn ahead. Blocking its route holds the clock **once**, the HZ 6.3 slip precedent.
  - **The pilot runs at party ≥ 3 only** **(agreed, Q2)**. The toy's solo and duo arms failed every greed gate (8). Solo gets its own simulator arm at C = 4 and opens only if it passes; duo is held. This is told plainly: in M3, a solo-hero or duo run never sees fleeting loot unless the solo arm passes, and because every party is at most 2 on F2, **no run sees a Windfall on F2** unless the solo arm passes (told plainly after the check).
- **Shade showcase.** A Dormant Truant is a legal Steal target. It is the room's one Steal try, and it takes the card while the coin spills.
- **Exploration form (the Ebb, a fading cache).** Held to **M4** with the exploration Truant **(agreed, Q3)**. In the toy it was either a free walking check for careful players or an attention tax for casual ones, and the decision appeared on only 12–35% of placements (told plainly: the client's "during exploration" case has no M3 form).
- **Not M2.** "Nothing timed in M2" is agreed (RT 0.1 item 5, RT Q5, TR-27). The Dream step is M3 outside the M2 Stir pilot (HZ-12), so **M3 adoption reopens nothing agreed**.
- **The client's skins** **(agreed, Q4)**.
  - The Rabbit (Carnival), the Crumb Mouse (Supper) and the Alms Crab (Choir) are the Truant pilot.
  - The Carnival balloon and the Choir plate are kept, as Ebb skins (M4). The Gut bauble is kept as an Ebb, outside Peristalsis rooms.
  - The Gaol prisoner reads as the Escapee roster type. The Supper trolley is a rules piece. The Heart toy collides with toy pieces. A Silk page is the boxed lurker's rest tell. All four are re-skinned or excluded (5, told plainly).
  - F1 and F5 themes have no Windfall. A coin-only F1 teaching Ebb is rejected (Q4 option B).

**Source taxonomy.** Every source is one tagged socket with one kind and one rank (SR0–SR5), or is explicitly unranked: the signature cache, the boss card, activity containers. There are 25 kinds (4.1), and the only new one is `windfall`. **Furnishing-hidden stashes are rejected.** "Furnishings never hold loot" is settled (ROOM anti-goal, 6.3.4, 13), and the brief's idea is answered by **inset containers** set into walls (told plainly).

**Placement.** Model A, **sockets by room role**, with one socket table per role and depth (4.2):
- one card-bearing container per room;
- guarded loot sits behind its guards;
- SR0 containers sit within 2 squares of the walk line;
- side-room containers are visible from the threshold.

**LootBind** binds each socket to one legal square, draw-free, after `trapsResolve` (4.3).

**Signposting.** At reveal the player sees each container's kind silhouette, the padlock base and kind glyph, the carried-loot glint, the purse badge, the key pip, the Hairline and the Windfall plate. Tier marks show **on hover only** (LE 6.15). Each container hover carries a **Proprietor tag** ("The turnkey's confiscation tin"), which tells a story and never a category. The Treasure strip (k / n) stays global and gains a by-source breakdown on hover.

**Clutter and the clock.**
- **The real click saving is auto-close of quiet summaries** (coin and non-healing sundries only), at −9 to −11 clicks per floor in the toy. Glean, one-click opening of a quiet room, is an optional convenience saving about 0.25 clicks (told plainly).
- The container count is **cut, not the cap raised**: coin-only containers are merged and Alcoves default to set pieces.
- 7c gets its own clock line, **net ≤ 0** minutes and clicks on the gated floors, once the walker re-baselines container clicks.

**Theme skins.** Each theme gets a container family that never shares a silhouette with its Furnishings, bait, lurker or Windfall. Container art never implies contents, with two named exceptions: the signature cache (IR 4.13) and the Bedside Stand **(agreed, Q5)**. Each theme gets one named **signature loot moment** (5). Five collisions found in approved skins are fixed (LP-16 to LP-20).

**Determinism.**
- Identities come from LootPlan, and the Windfall's card moves in a new draw-free step 8 after allocation.
- The Windfall uses a keyed grandchild, `Derive(lootPlan, FNV-1a("windfall"), 0)`, with 4 draws consumed on every floor.
- Route, Rathole and C are bound draw-free in LootBind, in a fixed numbered order (4.3).
- At runtime nothing is drawn except the existing Steal check, moved to a keyed hash.

### 0.2 The client's six decisions (agreed, 2026-11-22)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Fleeting loot: form and budget | **A.** The Windfall Truant in M3, in combat, F2–F4, about 1.2–1.4 per run, holding a real card from the fixed deck (from its host fight's carried allotment), lost visibly if it gets away, nabbed by any action or by winning the fight; pre-placed and visible at reveal (never spawned); C = 3; route outlined a turn ahead; slip once when blocked; Shade Steal while Dormant (4.7) | B: coin and a sundry only (safer, likely ignored); C: extra value outside the budget (breaks Ledger A, MAIN 4.6, "harder is not richer"); D: no fleeting loot | LP-1 ⚑ to LP-7 |
| Q2 | Party size | **A.** Pilot at party ≥ 3; solo opens on its own simulator arm (C = 4) if greed downs ≤ +2 pp; duo held (4.7.2) | B: all party sizes from the start (the toy says solo and duo fail) | LP-3 ⚑ |
| Q3 | The exploration form | **A.** The Ebb (forking geometry only) and the exploration Truant in M4 (4.7.11) | B: the Ebb in M3 after the Truant pilot passes | LP-34 |
| Q4 | Skins | **A.** Rabbit (Carnival), Crumb Mouse (Supper) and Alms Crab (Choir) are the Truant pilot; balloon, plate and bauble kept as M4 Ebbs; Gaol prisoner, Supper trolley, Heart toy and Silk page contested and excluded; F1 and F5 themes excluded (5) | B: an M4 F1 coin-only teaching Ebb (a Gaol confiscation sack, outside n) on top of A | LP-7 ⚑ |
| Q5 | The Bedside Stand | **A.** Accepted as a second named exception to "container art never implies contents", because it opens a different UI (4.10, 5.3) | B: an ordinary biscuit tin opens the offer UI | LP-14 ⚑ |
| Q6 | The Windfall frequency (raised by the check; answered with the approval of this document) | **A.** Accept about 0.6–0.9 Windfalls per run *(illustr.)* for the M3 pilot, with presence 1000‰ *(illustr.)* on F3–F4; the simulator owns the figure (`WindfallsPerRun`), and the pilot's gates decide whether to widen it in M4. Q1's "about 1.2–1.4 per run" cannot be met under Q2, Q4 and the companion ladder (4.7.2, 14.1) | B: Silverfish (Silk) and Rent Snail (Gut) in the M3 pilot (changes Q4); C: a duo arm in M3 at C = 4 (changes Q2) | — |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Model A, sockets by room role, is the skeleton. Grafts: Model B's Proprietor tags and wall hosts, Model C's preview and Tidy no-leak rule, the catalogue's family rules (3.3) | M1 (F1 subset) / M2 | lead's call |
| 2 | Source taxonomy: 25 kinds, one rank each (4.1) | M1–M3 | lead's call; LP-1 ⚑ |
| 3 | LootBind square legality, draw-free, after `trapsResolve`, in a numbered order (4.3) | M1 / M2 | lead's call; order after the check |
| 4 | Furnishing stashes rejected; inset containers instead (4.3, 7) | M2 | told plainly; LP-12 ⚑ |
| 5 | Numbered click table; auto-close quiet summaries; optional Glean (4.6) | M2 | lead's call; LP-8, LP-9 |
| 6 | Container count cut, not cap raised; 7c clock line net ≤ 0 (4.2, 4.6) | M2 | lead's call; LP-10 ⚑, LP-11 |
| 7 | Proprietor tags on container hover and summary header (4.5) | M2 | lead's call; LP-13 |
| 8 | Viaticum in the F5 lair room by the exit (a reading); Bedside Stand host (4.10) | M2 | reading; **agreed, Q5**; LP-14 ⚑ |
| 9 | Catalogue rules CT1–CT14 and five collision fixes (5); CT15, inset forms and signature-moment deliverables (after the check) | M2 | lead's call; LP-15 to LP-25 |
| 10 | **Windfall Truant pilot**: in-combat, party ≥ 3, C = 3, action nab, fight-end nab, slip once, real card from the host's carried allotment (4.7); about 0.6–0.9 per run *(illustr.)*, 1000‰ on F3–F4 (Q6) | M3 | **agreed, Q1, Q2, Q6**; LP-1 to LP-7 ⚑ |
| 11 | Ebb and the exploration Truant (4.7.11) | M4 | **agreed, Q3**; LP-34 |
| 12 | Pilot skins Rabbit, Crumb Mouse, Alms Crab; Ebb skins kept; contested skins excluded (5) | M3 / M4 | **agreed, Q4** |
| 13 | Uncorked glint state; Treasure strip by source (7) | M2 | lead's call; LP-27, LP-28 |
| 14 | V15 `LootSources`, reports, simulator arms (9) | M1–M3 | lead's call; LP-31, LP-32 |
| 15 | Trove, Crannies, Tip-offs, Tattler, Hawker (7) | Later / hook | lead's call |

### 0.4 Intent, goals and anti-goals

**Intent.** Every treasure on a floor should have a place a player can read: a room role, a rank they can see, a square they can reach and a cost they choose. 7c adds **no value and no hidden information**. Its one new source, the Windfall, adds a clock to a small, known stake.

**Goals.**
1. Every 7a and 7b channel lands on a named socket with a rank, a room and a square.
2. A player can read what kind of risk guards a treasure at reveal, and how good it is on hover.
3. The click and minute cost of looting is numbered and stays inside the floor clock.
4. The Windfall is tense, fair under permadeath, deterministic and rare.
5. Every theme skins its loot without lying about kind or contents.

**Non-goals.** The economy, tiers and counts (7a); which record a card becomes (7b); shop presentation (camp and hub); the screens themselves (segment 10); boss arenas (segment 9).

**Anti-goals.**
- A smash-everything chore.
- Pixel hunts and Search spam.
- Loot destroyed by kill method.
- Container art that implies contents (two named exceptions: the signature cache and the Bedside Stand).
- Floor-wide timers.
- A 100% completion badge.
- A greed trap whose loss cascades.

---

## 1. How games place loot and handle fleeting loot

| Game | What it does | We take | We avoid |
|---|---|---|---|
| B/X stocking | Treasure sits with monsters on 3 in 6, with traps on 2 in 6, empty rooms on 1 in 6 | The Risk Ledger shape: guarded, trapped, hidden | Random rolls at placement |
| 5E DMG | Small individual treasure; the lair hoard | Capped carried loot; the elite cache and signature cache as payoffs | Per-kill coin |
| PF2e, Frosthaven | A fixed budget or deck dealt to places | 7a Model A, unchanged | Per-PC scaling (7a stays per party, agreed) |
| OSR keys; the Alexandrian | One concrete spot per treasure; secrets need several pointers | LootBind binds one square per socket; Hairline plus "+1 secret" plus the Treasure strip | One-clue secrets |
| Gloomhaven | Looting costs tempo; monsters never loot | Free-interaction pickup, ENM 15.5 | Losing unclaimed tokens at scenario end |
| Divinity: OS2, BG3 | Diegetic clutter; hidden stashes | Theme-true containers, Proprietor tags | Click cost by container count |
| Hades, Slay the Spire | Reward previews at the door or node | Threshold previews; tier on hover | — |
| Into the Breach time pods | A 1-HP telegraphed object; the enemies are the real threat | The Truant: shown route, positional, no damage | Enemies destroying loot |
| Fire Emblem thieves | A runner with a predictable destination, intercepted by bodies | A fixed route; blocking slips it once | Indefinite body-walls |
| XCOM 2 timed drops | A visible turn count, permanent loss | A digit on the map | Dash-into-overwatch pickups; "explosives destroy loot" |
| Diablo treasure goblins | Rarity, an audio tell, a runner that never fights | Rarity; a reserved cue family | Random escape portals; kiting into fresh packs |
| Dark Souls crystal lizards | Pre-placed, seen before committing, one shot | Visible at reveal | Real-time burrowing |
| DCSS portals; Mario timers | Count turns, escalate the tell | A hop patter pitched up on the last count | — |
| PoE strongboxes; D3 cursed chests | The player starts the clock | The Ebb (M4) starts at the threshold | — |
| Spelunky ghost; Isaac and Dead Cells doors | Floor-wide timers | — | Floor-wide timers (FOMO, a speedrun tax) |
| Wildfrost, FFT | An always-visible integer counter | The Dream-card digit | — |
| Brogue vaults | Visible, spatial, deterministic risk | Offer sets; visible consequences | — |
| Invisible Inc | A stepped, visible alarm; the exit always known | One clock per room, shown | — |

The web-confirmed items (XCOM 2 loot timer, Into the Breach time pods, Hades troves) are cited in the research briefs. The rest is from memory, with confidence stated there (Appendix B).

---

## 2. The current-systems audit

### 2.1 What the approved documents fix for 7c (settled unless changed by a flagged LP)

- **Budget.**
  - MAIN 4.6 (decided): notable loot is a fixed, pre-rolled per-floor budget.
  - ENM 2 (agreed): kills never raise loot.
  - LE 6.1: Ledger A never grows.
  - LE 3.3 LX11: no new containers.
- **Ranks and allocation.**
  - LE 6.9 item 1 gives SR0–SR5.
  - Item 2 gives allocation steps 1–7 with filters (a)–(e), within-rank shuffles and the 250‰ cross-rank swap.
  - Item 3 gives the step allowance ≤ 2 (agreed, LE Q6).
  - The quota is 1 per floor. Premium share is 35–50%, lean route 45–55%, and no-Mechanics value loss ≤ 10%.
- **Per-source table.** LE 6.14 gives the per-source table and the caps 7c keeps:
  - 3 notables per encounter;
  - carried 1 per encounter on F1–2 and 2 deeper;
  - at least 1 Steal target;
  - trimmed floors keep the deck as visible Alcoves;
  - F5 allows at most 1 non-notable lock, and loot-bearing containers ≤ fillers.
- **Guarantees.**
  - The Due (LE 6.6).
  - DH HE-3/4/7 (DH 12.4–12.5).
  - The Viaticum (LE 6.11 item 5, agreed LE Q1).
  - The signature cache (IR 4.13, agreed IR Q5).
- **Carried loot.** ENM 15.2–15.8:
  - loot is never destroyed by terrain, damage type or kill method (15.6; TL 6.13, TL-27; HZ 13);
  - Routed carriers throw their notable down (C-8);
  - enemies never pick up items (15.5);
  - every drop in a fight is auto-gathered (15.8).
- **Steal.** ENM 15.5, S-5, STAT 7.1 Stealth row: only a Dormant, Stationary or Patrolling carrier; one try per carrier per floor; it takes one of {notable, purse, key} (LE-16).
- **Containers.** TL 6.13 kinds: Plain, Padlocked, Catch, Keyed, boxed lurker (M3), Twin Coffer (M3). `HeNoLock`, `LockNeverCritical`. Attack-to-test on a container does nothing (TL 6.14).
- **Clocks.**
  - HZ 6.2.2: one clock per room, never waived; a digit on the map always means the room's clock.
  - HZ 6.3: the Dream step, with Stir `firstAct` ≥ 2, the held Turn's one-round slip and `holdSlip` (6.3.4).
  - C 5.5: one round is one tick.
  - "Nothing timed in M2" is agreed (RT Q5, TR-27), reopened only for the HZ M2 Stir pilot (HZ-12).
- **Readability.** RS 7.4 RC1–RC9 (agreed, RS Q3): no spawns, no idle object motion, faces mean minds, keyhole reserved, reserved cues, names never lie. LE 6.15: tier marks only on hover and the lock card, and no loot cue joins RC7 unless the RS owner asks.
- **Determinism.** LE 6.10 (keyed `lootPlan` grandchildren, fixed draws), RS 6.4 LootDoorInvariant, and the order layout → hazards → traps → ENM 10.5 step 2 → LootPlan → steps 3–11 → `trapsResolve` → LootBind → V13g (LE 6.10 item 4). **Amended by SV-9 (M1):** the order is now the canonical pipeline GS0–GS17 with two assertion points: AP1, HE-7's structural point after traps and before loot, and AP2, the shared point after LootBind and EventPlace, where V13g, `V16g.SlotLegal` and then V15g run; SocketPlan is GS9 (after EP's 2a), LootPlan 3a with step 8 is GS10, loot step 3 GS11, LootBind GS14 (seeds and validation design 4.4, 5.4).
- **Clock.**
  - FL 6.10 (3.5 min and 40 clicks at party 4 on F4–5).
  - TL-38 (+0.3 min, +5 clicks).
  - EP-33 (+0.2 rounds).
  - The CMP 7.4 floor-time gate (agreed, CMP Q2). The toy lean case already fails it by about 0.2 min, and F3 heavy is over (FL 10.3).

### 2.2 What the approved documents owe 7c

- LE 6.14 and LE 12 (the 7c row): socket tags and ranks, lump placement, the Viaticum room, Twin Coffer placement, trimmed-floor sockets, per-encounter caps, F5 and overflow trims.
- IR 4.18: identity bundles per socket; the signature cache placed in the lair of every boss whose form is `guarded` (read as the boss arena, 4.4, 13); container art never implies contents.
- IR 5.3: per-theme container notes. The Heart has none.
- TL 13, EP 13, HZ 13, FL 13, ROOM 13, RS 13: container kinds, `lockable` sockets, bait-cache family, activity containers, fallen loot, HE reservations, "Furnishings never hold loot", Stratum lines.
- FL 11.4 and CMP 7.4: "a container N clicks by type". **This was never numbered.**

### 2.3 Tensions and resolutions

| # | Tension | Resolution |
|---|---|---|
| PX1 | Fleeting loot against "loot is never destroyed" (ENM 15.6, TL 6.13, TL-27, HZ 13) | One named exception: a Windfall's card and coin leave the floor when its shown count ends. Kill method, damage, terrain, traps, locks and force still never destroy anything (LP-2 ⚑) |
| PX2 | Fleeting loot against "nothing timed in M2" (agreed) | M3. The Dream step is already M3 outside the Stir pilot, so nothing agreed is reopened |
| PX3 | A countdown against one clock per room | It **is** the room's clock, shown as a digit, never placed with another clock. Disguising it as pips would be a lying exception, and is rejected (told plainly) |
| PX4 | A one-hit thing against "minions never carry items" | Not a minion. A new non-combatant unit row (LP-3) |
| PX5 | RC1 spawn and idle-motion bans; RC3 faces | A **unit**, present at reveal, moving only in ≤ 1.0 s acts. **Always a faced animal, never an object**, because a faceless unit must show the reserved keyhole (RC3) and an object-shaped unit reads as a boxed lurker (CK1) |
| PX6 | A fixed deck against an exciting reward | A real card taken from inside the deck. Excitement through visible tier, cues and the run record |
| PX7 | F3–F4 card starvation in the container channel (toy: 0 SR1 cards) | The card comes **from the host fight's carried allotment** first, never from secrets or the quota (lead's call; CB5, CF1) |
| PX8 | Allocation shuffles can move filtered cards onto a Windfall (LE 6.9 item 2 steps 6–7) | The Windfall is not in the shuffle at all. A new draw-free step 8 moves its card after step 7, and filters are asserted after step 8 (CB2, CK4) |
| PX9 | Blocking that holds C forever | Slip once (HZ 6.3 precedent), then C falls. It fades where it stands at 0 (CB1) |
| PX10 | A catch as a free auto-hit target for on-hit riders | A nab is not an attack roll, a hit, damage dealt or a kill (CB3) |
| PX11 | Herald Cow is group-only and fresh groups are not Cow-eligible | No advertised Herald payoff. Only the neutral rule "a host group resolved without combat leaves a free Take" (CF8, CK16) |
| PX12 | The container cap against role tables (toy: 97–100% of floors over a cap of 9–10) | Cut the count (merge coin-only containers; Alcove default a set piece), with a hard cap of 12 and a target of ≤ Ledger A + 3 (CB6, CF5) |
| PX13 | Glean's claimed saving (3–5 clicks) against the toy (about 0.25) | Auto-close of quiet summaries is the lever. Glean is optional (told plainly) |
| PX14 | The Viaticum's primary rule "after the lair" is structurally empty on F5 | A reading, not a change: LE's own fallback, the lair room, is the rule. The FL and ENM owners confirm (CK18) |
| PX15 | Model A's reading "containers are not opened in combat" against TL's in-combat lock verbs | Dropped. 7c adds no rule (CK3, CB9) |
| PX16 | "Catch" collides with the TL Catch lock kind | Player-facing verb **Nab**, action **Grab** (CK8) |
| PX17 | The roll-forward needs persisted state, and formations are known only after LootPlan | Presence is decided in LootPlan on formation-independent rules. Formation conflicts move the host within the floor in LootBind. A persisted `windfallCarry` flag lives in the camp-exit write (CB4, CK14) |
| PX18 | A whole-sack Steal against LE-16 "one of {notable, purse, key}" | Steal takes the card only. The coin spills. The Truant is the room's one Steal try (CB7, CF4) |

---

## 3. Placement models

### 3.1 The models

- **A, sockets by role.** A fixed socket table per role and depth. LootBind deals by rank. Inset containers. The Windfall as Truant and Ebb.
- **B, diegetic stashes.** Proprietors and "Whose lines", wall-face containers, Crannies with a passive-Perception tell, Tip-offs, Gleaning, quiet summaries. The Windfall in three forms, including an exploration Truant at pace parity.
- **C, dynamic sources.** A's static base plus a dynamic layer: Truant, combat and exploration Ebb, and the **Trove**, a speed-graded offer. Tidy. Offer pacing.

### 3.2 Judges' scores (mean of three judges, 1–10)

| Criterion | A, sockets by role | B, diegetic stashes | C, dynamic sources |
|---|---|---|---|
| Excitement | 6.3 | 7.0 | 8.0 |
| Fairness | 7.0 | 6.0 | 5.3 |
| Readability | 8.7 | 6.7 | 5.7 |
| Low clutter and clock | 6.3 | 6.0 | 5.0 |
| Theme expression | 6.0 | 9.0 | 7.0 |
| Determinism and testability | 8.7 | 7.3 | 7.0 |
| Fit with approved docs | 8.7 | 6.7 | 6.0 |
| Low build cost | 7.0 | 4.0 | 3.0 |
| **Mean** | **7.3** | **6.6** | **5.9** |

### 3.3 Verdict and corrections (lead's call)

All three judges chose **Model A as the skeleton**, and it stands as written (the client, 2026-11-22). Grafts:

- **From B:**
  - **Proprietor tags**, renamed from "Whose line" because "line" is IR's rolled-line term (told plainly).
  - Wall hosts, merged with A's insets.
  - Auto-close of quiet summaries.
  - The always-consumed draw table.
- **From C:**
  - C = 3.
  - The threshold preview.
  - `MainPathDetour`.
  - Tidy's no-leak boxed-lurker rule, for Glean.
  - Offer pacing.
- **From the fleeting-loot draft:**
  - "A nab is not a kill".
  - "Slip, not freeze".
  - `ItemDestroyed{gotAway}`.
  - The catchability proofs.
- **From the catalogue:** CT1–CT14 and the collision fixes.
- **Rejected or held:**
  - B's Crannies and Tip-offs (Later; on F3–F4 they point at coin, and they double unseen-counted FOMO for low-Perception solo heroes).
  - B's pace-parity exploration Truant (M4, agreed Q3).
  - C's Trove (Later: party-size bias, card starvation, a third timed family).
  - C's hidden-from-entry home square (rejected: +2.7 pp downs against +0.3).
  - C's "C falls when blocked" (rejected: it kills the Vanguard intercept).
  - Every free-grab design (rejected: +10 to +12 pp downs).

---

## 4. Rules in full

### 4.1 Source taxonomy (lead's call; change LP-1)

Each source is one `LootSocketRecord` with exactly one `kind` and one `rank`, or `unranked`.

| Kind | Form | Rank (LE 6.9) | Channel | Wave | Notes |
|---|---|---|---|---|---|
| `mainPlain` | Plain container, main path | SR0 | container | M1 | HE-7 and HE-4 flags |
| `dueHost` | The first HE-7 `mainPlain` in main-path order | SR0 | container | M1 | Shares with the DH block (LE 6.6) |
| `lairContainer` | Plain container in the main-path lair (L) | SR0 | container | M2 | F2–F4 only. On F5 the lair room's host socket takes kind `viaticum` instead (lead's call, after the check). Distinct from the SR4 elite cache (told plainly: "lair cache" in LE 6.9 means the *elite room's* cache) |
| `carrierMain` | Carrier in a main-path fight | SR0 | carried | M1 | ENM 15.3 step 4 order |
| `carrierOptional` | Carrier in an optional fight (Gb, side room) | SR1 | carried | M2 | Gb reads as optional: **a reading of LE 6.9 item 1, to the LE owner** |
| `sideContainer` | Container in a side room or loop-back Site | SR1 | container | M2 | Visible from the threshold |
| `alcoveCache` | The one container of an Alcove leaf | SR1 | container | M2 | Default occupant is a set piece where segment 3 offers one (LP-11). On F1 a Curiosity may take that set-piece slot first in its host order; it never empties a leaf and never displaces a container the cap needs (events design 4.2; EV-2, M2) |
| `lockedCache` | Padlocked, Catch, Keyed | SR2 | container | M2 | Quota card, or coin plus 1 non-healing sundry |
| `keyTarget` | The Keyed cache a Keybearer's key opens | SR2 | container | M2 | `KeyReachable` |
| `secretCache` | The cache in a Hairline space | SR2 | container | M2 | ≤ 1 card, never the top card. F1 outside n |
| `baitCache` | Mousetrap bait (EP 6.8.1) | SR2 | container | M2 | Its own prop family. M2 with the Mousetrap (EP 6.8.1, LE 6.9 item 6; corrected after the check) |
| `boxedLurker` | `containerForm` lurker (TL 6.14) | SR2 | container | M3 | — |
| **`windfall`** | **Truant unit** | **SR2 (new row, LP-1)** | **inherits its card's origin channel** | **M3** | Created only in step 8; never in the steps 1–7 socket list, ordering or deal (4.7.6) |
| `consequenceCache` | Crier, Barb, Tattle (TL 6.11) | SR3 | container | M2 | Two identities |
| `eliteCarrier` | The elite's gear | SR4 | carried | M2 | `showsInUse` |
| `eliteCache` | Container in the elite room | SR4 | container | M2 | Consumable top card, or "Guards: cache". Never locked (lead's call: the fight is the lock) |
| `strongroom` | Strongroom spur | SR4 | container | M3 | F2–3 |
| `wagerCage` | Cage set plus plinth | SR5 | container | M3 | Counts 1 |
| `dareCache` | Dare reward | SR5 | container | M3 | Seals after two failures |
| `twinCoffer` | A `sideContainer` or `alcoveCache` kind | SR1 (SR2 if locked) | container | M3 | ≤ 1 per floor, F2+ |
| `activity` | Dice pot, feast plate, etc. | unranked | coin and sundries | M2 | EP 13 |
| `purse` | Heavy Purse tag on a carrier | unranked (carrier tag) | coin | M2 | LE 8.3 chain. A tag on a carrier socket, not a socket of its own, and outside the deal. Never on the elite or a Windfall |
| `signatureCache` | The guarded signature | unranked | boss | M1 | IR 4.13 |
| `bossCard` | Reward screen | unranked | boss | M1 | A ledger kind with no world object and no square; LootBind skips it |
| `viaticum` | F5 provisions container, in the lair room's host socket | unranked (boss channel; `leanRoute` flag) | boss share | M2 | A boss-channel offer, not a container draw (LE 6.11 item 5): outside the deal and the within-rank shuffles. Opens the width-3 offer UI (lead's call, after the check: the proposal ranked it SR0) |

**Not world objects:** salvage, the camp channel (hook), the Memento (victory screen) and `bossCard`'s screen have no square. Of these, only `bossCard` is a kind, kept in the ledger so the 25 kinds cover every channel. **The Annex has no socket**: its prize is what it serves (settled, LE 6.9 item 1).

### 4.2 Socket derivation, the role table and depth (lead's call; change LP-11)

**Where it runs.** 7c's **SocketPlan** builds LootPlan's provisional socket set (LE 6.9 item 2), draw-free. It runs at GS9, after EP's 2a (the Mousetrap bait cache), reading encounter steps 2 and 2a only; EP's 10b activity props are read by LootBind, not SocketPlan (SV-9, M1). The Windfall grandchild's draws are not SocketPlan's: they are taken at the start of step 8 inside 3a (GS10; 4.7.10) (SV-31 text fix; seeds and validation design 4.4). It reads:
- layout `containerSockets[]`;
- Alcove leaves and `secretSites[]`;
- the hazards pass's Dare socket;
- the traps pass's lock kinds, Keybearer slot and key target, Tattle candidates, boxed-lurker sockets and Wager layouts;
- ENM step 2's slot plan and the carriers predicted by step 4's fixed order;
- the provisional armed roster (IR 4.18);
- HE reservations.

**`socketId` convention.** Room main-path index (then `branchIndex`), then square index. This is A's default and needs no reading. B's deepest-first order is **not** adopted, because it would change step-7 and parent-slot mapping (CK17).

**How the role table becomes sockets (lead's call, after the check).** SocketPlan **selects** from layout `containerSockets[]` (and the other inputs above); it never creates a container square. The table lives in data as `loot.roleTable[role][floor] {min, max, ranks, coinWeight, never[]}` (9.1).
- Per room, in `socketId` order, SocketPlan takes sockets up to `max`. It keeps `min`, and more only where an HE or Due reservation needs one (the "2 only where an HE need demands" row). Draw-free: a range "0–1" means take 1 if the layout offers a legal socket, else 0.
- Sockets the traps pass has already bound (Catch hosts, lock kinds, boxed lurkers, the key target) are always selected; they count toward `max`.
- **`cardEligible`.** Each selected socket carries a `cardEligible` flag. It is true on the first container socket of a room in `socketId` order and false on every later one, except the named exceptions of room rule 1 (the elite cache, the Wager set, an HE-7 socket in a room with a separate Alcove). The step-5 deal (LE 6.9 item 2) skips sockets with `cardEligible` false, which is how "one card-bearing container per room" is enforced; they take coin and sundries only.

**The walk line (lead's call, after the check).** The **walk line** of a room is the shortest walkable path from its party-entry Portal to its main-path exit Portal, under 4.7.3's fixed neighbour order. In a room with one Portal (a side room, an Alcove, a dead end), it is the shortest path from the entry Portal to the room's walkable square farthest from it (ties by square index). FL uses the phrase informally; this is the binding definition for 4.2, 4.3, 4.4, 4.10, `MainPathDetour` and `LootDetourSquares`.

**Role table** (counts *(illustr.)*; ranks settled):

| Room role (FL 6.5) | Container sockets | Rank | May hold | Coin weight | Never |
|---|---|---|---|---|---|
| Entry (room 0) | F1: 1 Plain no-check; F2–5: 0–1 | SR0 | DH block, coin floor lump | 1 | A lock, Windfall, secret |
| Main-path non-fight rooms (incl. Q) | 1; 2 only where an HE need demands | SR0 | Due (first HE-7), HE-3/4, ordinary cards | 1 | Locks on HE. Segment 8 may skin, never add (`CuriosityLootBlind`; events design 4.4.5, EV-15) |
| Main-path guard post (G) | 0–1 Plain + carriers | SR0 | Ordinary and carried cards; **Truant host (M3)** | 1 | A lock on an HE socket |
| Route-avoidable guard post (Gb) | 0–1 + carriers | SR1 | Ordinary and carried; Truant host | 2 | An HE socket |
| Lair (L) | 1 lair container + carriers; purse chain | SR0 | Ordinary cards; F5 Viaticum; Truant host (F2–4) | 1 | — |
| Elite room | Elite carrier + 0–1 elite cache | SR4 | Top gear card; consumable top card to the cache | 4 | Purse, Windfall, lock on the cache |
| Forecourt (F2–5) | 0–1 Plain no-check | SR0 | HE-7 #2 where FL allows; never HE-4 | 1 | Viaticum, Windfall, lock |
| Arena | Signature cache only (guarded forms) | unranked | Signature | 0 | Any other container (segment 9) |
| Stairhead, corridors | 0 | — | — | — | Loot |
| Side room / loop-back Site | 1 | SR1 / SR2 locked / SR3 consequence | Ordinary, quota, step cards; Twin Coffer | 2/3/4 | HE |
| Circuit rooms | 0 | — | — | — | Windfall, HE-4, Viaticum |
| Alcove leaf | Exactly 1 occupant: container, cache or set piece | SR1 (SR2 if locked) | Ordinary card; trimmed-floor reward | 2 | Top card |
| Hairline space | 1 secret cache | SR2 | ≤ 1 card, never the top card | 3 | Due, HE, key target, Windfall card source |
| Strongroom spur (M3, F2–3) | 1 | SR4 | Step-eligible | 4 | — |
| Wager / Dare Site (M3) | Prize | SR5 | Triples / step card | 4 | A second clock |
| Mousetrap guard post (M2, EP 6.8.1) | + bait cache | SR2 | Real ordinary entry | 3 | Solo, F1 |

**Room rules (lead's calls):**
1. **One card-bearing container per room.** Exceptions:
   - the elite room (carrier plus cache);
   - the Wager (counts 1);
   - a room with HE-7 and a separate Alcove.

   **At most 1 coin-only container per room**: two are merged in LootBind (rule 5, phase 2).
2. **Guarded loot sits behind its guards.** A fight host's container is on the far side of the start zone from the party-entry Portal, never inside it.
3. **Previewed.** Side-room and Alcove containers are in line of sight from the entry threshold (Model C; the Hades lesson). Read, not changed, by segment 10's Detour quote (exploration and information design 4.4.3; XI-10, M2).
4. **Walk line.** SR0 and HE containers have their open square within 2 squares of the room's walk line (`MainPathDetour`; HE-7 within 1).
5. **Container count** (LP-11).
   - Target `ContainersPerFloor` ≤ Ledger A + 3. Hard cap `loot.containerCap` = 8 / 12 / 12 / 12 / 10 *(illustr.)*; on F5 the cap is also never above the filler count (LE 6.14), so F5's effective cap is min(10, fillers) (lead's call, after the check: the proposal wrote "F5 rule"). Activity containers count; the signature cache does not.
   - **Two phases (lead's call, after the check),** because SocketPlan runs before LootPlan and coin-only status is known only after the deal:
     1. **Phase 1, SocketPlan (before allocation).** Activity slots are reserved first from ENM step 2's slot plan (EP's props), then HE, Due, key-target and traps-bound sockets. Card-eligible sockets (beyond the reserved ones) are limited to the known count of container cards (deck size − carried cards, LE 6.9 item 2 step 1), since more could never hold a card. Further sockets are taken only while the total stays ≤ the cap; an Alcove whose occupant would exceed it takes its set piece (segment 3 ask).
     2. **Phase 2, LootBind (after allocation, step 5 of 4.3).** Coin-only containers in one room are merged into the lowest-`socketId` survivor: their coin weights and sundries are summed onto it, its HE flags are kept, and the merged squares are freed. Each merge is logged `ContainerCapTrim{merged:[ids]}`.
   - **Fewer locks is a design-time lever, not a generation-time request.** If the walker shows the cap binding, the TL tuning for non-quota coin locks (LE 6.4 item 4) is lowered by the TL owner; no pass is asked again during generation.
   - Never trim a card-bearing, HE, Due, Viaticum or key-target container. `EmptyDeadEndRate` stays 0.
   - Told plainly: the toy role tables produced 12.5–14.5 containers per floor, so these cuts are needed, and the walker must confirm them.
6. **Never empty** (settled, LE 6.4). Coin overflow becomes sundries, then `CoinSocketTrimmed`.

**Pending request (segment 10; XI-27, M2).** `SideRoomPayoffMix` (FL, LP and EV owners): at most 60% *(illustr.)* of side rooms pay loot only; the rest also pay a fact, a shortcut, XP or a pointer, with no VP change (exploration and information design 4.4.6). Not a rule until the 7c, FL and EV owners rule; it is the first lever if `SideRoomVisitRate` falls below its band (exploration and information design 14.2).

**By depth.**

| Floor | Rules |
|---|---|
| F1 (always solo) | Plain-dominant. ≤ 1 consequence lock after room 2. Coin floor ≥ 17. Secret only on Gaol or Wake (0–1), shown "+1 secret", outside n. No Windfall, Wager, Dare, lurker, Mousetrap, Strongroom or Twin Coffer (4.9) |
| F2–F3 | Full table. Strongroom (M3). Dare or Wager ≤ 1. Twin Coffer ≤ 1. Purse on. Windfall eligible by floor; in practice F2 only through the solo arm once opened, and F3 only at party ≥ 3 on a skinned theme (4.7.2) |
| F4 | As F2–F3 without the Strongroom. Windfall eligible at party ≥ 3 on a skinned theme (4.7.2) |
| F5 | No coin or salvage lines, no purse, no boss card, no camp channel. Viaticum in the lair room. ≤ 1 non-notable lock. Loot-bearing ≤ fillers. No Windfall. Memento on the victory screen only |
| Trimmed (RT-7) | Deck kept as visible Alcoves; top card on the elite or lair carrier. No Windfall |

### 4.3 LootBind: binding to rooms and squares (draw-free; after `trapsResolve`; lead's call; changes LP-12, LP-25)

**Square legality** applies to every container square, its **open square**, and every Truant home and route square. A square is never:
- a Clearway square, a 1-wide chokepoint, a Portal square or its inner neighbour, or inside a Hinge or Postern 8-neighbourhood;
- a hazard square, a Void-adjacent square, a Stir footprint or due square, or a Tide socket;
- a trap trigger square or a Hunch-zone square. A Catch container's own catch is the exception;
- a Furnishing or piece square, or within 1 of a Keg footprint;
- an enemy start zone (Truant: ≥ 2 squares from every monster start square);
- an arena entrance band;
- a water, web, oil, bile, bedsheet, harm or fire square (CT14, LP-25).

The open square is walkable, safe, and reachable from the walk line (4.2) without crossing a hazard, trap or Void.

**Traps-bound sockets (lead's call, after the check).** Sockets the traps pass has already bound to a square (a Catch host and its trigger, a lock, a boxed lurker, the key target) **keep the traps pass's square**. LootBind only checks them against this list; a failure is a V15g failure (`LootBindUnplaceable`, handled by its class as in step 2 below; SV-10), never a rebind, so a Catch is never separated from its trigger square.

**Choice by role.** Each role ranks the legal squares by a scoring tuple, lowest first, then by face N, E, S, W (lead's call, after the check: the proposal named regions, not a key):
- SR0 and HE: (distance from the walk line, path distance to the exit Portal, square index).
- Side rooms and Alcoves: (not in line of sight from the entry threshold, then −(path distance from the entry Portal), square index): the farthest square seen from the threshold.
- Guarded loot: (in or within 2 of a front start square, then −(path distance from the party-entry Portal), square index): the rear of the room, behind its guards.
- Secret: (−(distance from the Hairline space's entry wall), square index).
- Lair container: (path distance to the exit Portal, square index).
- Truant: 4.7.3.

**Inset host preferred** for SR0, Alcove and secret sockets when the template offers a wall face (LP-12). An inset container is a container set into a wall face, with its open square in front; it is never a Furnishing and never a hidden stash (ROOM 6.3.4, settled, reaffirmed). **Its RC5 mark is the wall-set mark** **(new lead's call)**: a solid base bracket on the wall face, never a dashed rim, so it never reads as the harm square's "inset dashed" rim (ROOM 8.1, HZ 8).
- **Cutaway (lead's call, after the check).** Under FL 8.1's 4-way rotation every wall can render as a stub. Each inset container also projects its wall-set mark as a solid bracket onto its open square, drawn whatever the wall's cutaway state (the HZ hazard-face floor-decal precedent, ROOM 8.1). Inset meshes sit wholly below stub height, so a stubbed wall never cuts them.
- **Form.** Each theme's inset is mounted at waist height on its bracket, at most half a wall square wide, with no floor-level opening; opened, it shows a shallow, empty back, never a passage (5.2 inset table).

**Spacing.** No two containers are 8-adjacent, except the Wager cage set.

**LootBind order (lead's call, after the check).** LootBind runs these steps in this order, draw-free; the result depends on the order, so the order is binding. Every remap is logged `LootBindRemap{reason}` (added to LE 6.9 item 7's list by LP-1).
1. **LE 6.9 item 7 remaps,** unchanged and in that item's order: lock downgrade, Tattle to Crier, Wager reverted, lurker dropped, Annex dropped, carrier fallback, KinSwap; then the offer-pacing trim (7, LP-30). **The Windfall's card is excluded** from KinSwap and the carrier fallback: it is no longer on a carrier.
2. **Container squares** (non-traps-bound sockets), by role (above). If a socket has no legal square:
   1. another legal square in the same room under the relaxed tie order (role tuple dropped, square index only);
   2. else merge its entry into the room's existing container, which keeps the room at one card-bearing container;
   3. else, for a non-HE, non-Due socket, the parent room's Alcove, only if the Alcove's occupant is a container of the same or a higher rank (never a set piece);
   4. else a V15g failure, `LootBindUnplaceable{socketId}`, acted on by its class (SV-10; seeds and validation design 5.1, 5.5): on a non-HE, non-Due socket, the LR4 terminal fallback **`LootBindDegrade`** (the socket becomes a merge, else coin-only; logged `GenerationFallback{floor, fallback, check}`; BUDGET ≤ 0.3% *(illustr.)*), replacing the action-less hard failure (M1; budget read M2); on an HE or Due socket, a defect (LR5), which the V15s binding proof (9.2) makes impossible.
   **Pending request (segments 11–12; SV-10):** the 7c owner confirms `LootBindDegrade`'s merge-or-coin rule. Not a rule until the 7c owner rules; if not confirmed, 7c's own failure order above applies to the socket, ending in coin-only, and the row stays a BUDGET ≤ 0.3% (seeds and validation design 14.2).
   HE and Due sockets may use fallback 1 only, else fail (a defect, LR5). 7c never moves them to another room.
3. **Windfall host check.** If the host room now has a formation or form the Windfall may not share (Kill Box, Crossfire, Pincer, an ambush form, the Annex, a boxed lurker), or the room has no legal Rathole, home and route under 4.7.3, the Windfall **moves** (`WindfallMoved{from,to}`):
   - the card first returns to its origin, as resolved after step 1 (below);
   - the next eligible host in `socketId` order is tried; the draw-free step-8 source order (4.7.6: (i) the new host's carried allotment, then (ii) a plain SR1 container) is re-run against it, with filters (f1) and (g) re-asserted;
   - the new host is taken only if its encounter stays within 3 notables and the carried caps (LE 6.14) with the Windfall counted, and the floor keeps ≥ 1 Steal target;
   - if no host passes, the Windfall is **dropped** (`WindfallDropped`), and `windfallCarry` is set (4.7.10).
   **Origin on a drop or move.** `WindfallInstance.originSocketId` records the socket the card came from. The card returns to whatever that socket resolved to after step 1: the carrier, the same-room container if the carrier fell back, or the KinSwap partner's carrier.
4. **Home, Rathole and route** (4.7.3), for the Windfall that survives step 3.
5. **Container merges** (4.2 rule 5, phase 2).
6. **Proprietor tags** (4.5), on the surviving containers.
7. **Coin lumps, the Heavy Purse and salvage** (LE 6.4 items 2 and 4, 8.3). These run after the Windfall is final, so a dropped Windfall's weight-3 share returns to the pool before lumps are carved.
8. Then the V13g and V15g checks at AP2, the shared assertion point after LootBind and EventPlace, in the order V13g, `V16g.SlotLegal`, V15g (SV-9, M1; seeds and validation design 5.4).

**Pending request (segment 8; EV-15, M2).** A LootBind flag `stepFunded` on each locked container, false when the lock carries no value step, so a Curiosity may price an extra route to an ordinary, non-HE, non-Due, non-boxed lock with contents unchanged (events design 4.4.5). Not a rule until the 7c and LE owners rule; if refused, segment 8 has no priced lock routes (events design 14.2).

### 4.4 How every 7a and 7b channel lands (the LE 6.14 and IR 4.18 hooks; lead's call)

| Channel | Lands on | 7c rule |
|---|---|---|
| Ordinary deck cards | Ranked sockets | LE 6.9 item 2 deal on 7c's tags |
| Carried cards | `carrierMain` / `carrierOptional` / `eliteCarrier` | Steal-reachable: never a nook, never a ward behind two fronts on F1–2 (EP 13). Caps settled. The Windfall card counts toward its **final** host encounter's 3 and carried cap, re-checked after any move (4.3 step 3) |
| Lock quota card | Quota socket (Wager > consequence > any lock) | Others get coin plus a sundry |
| Consequence (two identities) | `consequenceCache`; for a Portal lock, the highest-ranked card socket beyond | No beyond-socket: coin-only next lump step |
| Keybearer key | Carrier (key pip); target within 2 rooms (Gaol 1) | Never behind its own lock. Never on a Windfall |
| Secrets, Alcoves | `secretCache`, `alcoveCache` | Secret cards are never a Windfall source (filter (g)) |
| Wager, Dare, Strongroom | Their Sites | TL 6.15, HZ 6.10, FL 9.2 unchanged |
| Bait cache, boxed lurker | Their sockets | Real SR2 entries |
| **The Dream's Due** | **First HE-7 in main-path order**, with the DH block | No special host (CT6). Hover "The dream's due: for <name>" |
| DH HE-3/4/7 | Layout reservations (7c never moves them) | Plain, no check |
| **Viaticum (F5)** | **The lair room's host socket (kind `viaticum`, replacing `lairContainer` on F5), exit side toward the Forecourt, outside the start zone, on the walk line** | **A reading** of LE 6.11 item 5 (its fallback is the real rule, because nothing lies between L and F on F5). The FL and ENM owners confirm (LE 18.2). Opens the width-3 offer UI. Host skin: the Bedside Stand (LP-14; **agreed, Q5**) |
| **Signature cache** | In the boss's arena, for `guarded` forms (a reading: IR 4.13 and 4.18's "the lair of every boss" means the boss arena, not FL 6.5's L room; 13, lead's call, after the check; confirmed by segment 9, boss rooms design 4.11.1; BR-27, M2) | ≥ 3 squares from both entrance Arches, outside the boss start zone and arena-clock footprints, visible from the entry. Barred until `BossDefeated` (`BossResolved` is its alias; boss rooms design 4.2.4; BR-37, M2). Segment 9 owns the final square: each arena template's `cacheCandidates[]` applies these filters (boss rooms design 4.1.1) |
| Boss card, Memento | Screens | No placement |
| Coin pool, lumps | By weight: SR0 1, SR1 2, SR2 3, SR3–5 4 | **Windfall: weight-3 share, never a K2 lump** (toy: a lump fails `StealCoinShare` at 45%). Carved in LootBind step 7, after the Windfall is final (4.3) |
| Heavy Purse | LE 8.3 chain | Never on the elite or a Windfall |
| Salvage | Per fight | Windfalls pay none |
| Camp channel | Hook | 7c places nothing |
| Activity containers | EP's props | Coin and sundries; count toward the cap |
| **Windfall** | 4.7 | Card moved in allocation step 8 |

### 4.5 Visibility, signposting and the Treasure count (lead's calls; M2 unless stated; changes LP-13, LP-28)

| Source | Seen at reveal | On hover | After |
|---|---|---|---|
| Plain container | Kind silhouette (theme family) | Proprietor tag; tier marks (1–4 diamonds plus tier word) if it holds a card, else the coin glyph | Lid open, no residue |
| Locked / consequence | Padlock base decal plus kind glyph, never a keyhole | Lock card: tier mark, fine-category noun (IR-11) | — |
| Secret | The Hairline only | — | Sound the wall reveals it |
| Carried | ENM glint, purse badge, key pip (≤ 2 plate marks) | "Carries: …" | Use reveals the item |
| Elite | Item shown in use | — | Elite intel line |
| Wager / Dare | Portal glyph, Listen, Stratum line, prize through the Grille | Offer card tier marks | — |
| Signature cache | Barred lid, item-icon plate | "Guards: <name>" | Opens on `BossDefeated` (`BossResolved` is its alias; BR-37) |
| **Truant (M3)** | Unit plate with the **Windfall mark**; Dormant glyph; digit at its start value on a hollow floor ring | "Carries: <tier word> <fine-category noun> and coin · runs when a fight starts · gets away in <c0>"; the scurry trail and Rathole decal | Awake: digit on a solid floor ring and the Dream card; next hop outlined |

- **Truant hover (lead's call, after the check).** `<c0>` is the displayed start digit, 3 (`WindfallInstance.c0`); held steps from a Surprised start or the solo arm show as a "held" badge, never a higher number (4.7.4). If the live party falls below the allowed size, the hover drops "runs when a fight starts · gets away in …" and reads "Carries: …" only, and the Stratum suffix is dropped for that floor.
- **Proprietor tag** (LP-13). The hover and summary header read "<proprietor possessive> <skin noun>", such as "The turnkey's confiscation tin" (on a padlocked skin, "The turnkey's locker").
  - It is draw-free and assigned in LootBind step 6 (4.3), never by the card.
  - **Selection (lead's call, after the check).** Rank families are **safe** (SR0), **off-path** (SR1), **guarded** (SR2–SR3) and **prize** (SR4–SR5); unranked sockets take no tag except the signature cache's "Guards:" line. Sockets are tagged in `socketId` order. Candidates are the theme's `ProprietorRecord`s whose `roomRoles`, `rankFamilies` and `hosts` all match and whose uses so far are below `maxUses`. The pick is by (anchor matches the room's anchor first, then the most matching non-empty lists, then `id`). With no candidate, the theme's required `generic` record is used and `ProprietorFallback{socketId}` is logged. `ProprietorFallbackRate` = fallbacks ÷ tagged sockets.
  - **The noun is the skin's (lead's call, after the check).** Only the possessive varies; the noun comes from the container's `skinId`, so the tag never names a kind the silhouette is not. Lock-kind nouns (strongbox, safe, locker) never sit on an unlocked skin (V15s `ProprietorNounMatchesSkin`).
  - **Noun ban:** never a category or tier (no armoury, apothecary or weapon rack), and never threat or safety (V15 `ProprietorNounBan`).
  - **Lurker parity:** a boxed lurker resolves as a Plain host of the same socket, takes exactly the tag of the container it imitates and consumes a use. A bait cache takes the theme's `generic` tag plus its bait mark.
- **Treasure count** (LE 8.2, extended by LP-2):
  - n counts the Windfall's card.
  - **When a Windfall gets away, n falls by 1** with the log line "The <skin> got away with a <tier word> treasure". This applies to all three causes of `WindfallGotAway{cause}`: the Rathole, fading in place, and the party fleeing (4.7.7). It is always seen: the digit was on the map and the leave act plays.
  - A Truant taken by Steal raises k.
  - A Truant left Dormant when the party takes the stairs is lost like any unopened source (ITEMS 9), and **n is unchanged** (unseen; "some treasures may be gone").
- **Treasure strip by source** (LP-28, M2). Hover breaks n into counts only (open, locked, hidden, carried, prize, windfall), never tier, item family or room. The "windfall" count drops by 1 on a nab, a Steal or a getaway; a nabbed card then counts under "carried" until it is taken. The F1 "+1 secret" rule is unchanged. (Renamed after the check from "by family", which collides with the ITEMS and IR item family.)
- **Stratum Card.** No new line. On a Windfall floor the Treasure line reads "Treasures: 7 · one may get away" (LE 6.15 allows ≤ 1 line per category). Told plainly: this primes the hunt, which is intended Hades-style signposting.
- **Run record.** Grave Goods gains "Windfalls nabbed k of m" and "Got away: <tier word>", in DH's neutral voice. **Never** a profile achievement or completion stat (CF10).

### 4.6 Clutter, pickup and the clock (M2 unless stated; changes LP-8 to LP-11)

**Click table.** This fills FL 11.4 and CMP 7.4's "a container N clicks by type" (change LP-8). Values are *(illustr.)* until the walker measures them.

| Action | Clicks | Ticks |
|---|---|---|
| Open a Plain container, quiet summary (coin and non-healing sundries only): it **auto-closes** after 1.5 s *(illustr.)*, coin to the purse, sundries to the suggested recipient | 1 | 1 |
| Open a Plain container holding a notable or a healing sundry (open, Take or Take all) | 2 | 1 |
| Lock card (TL 6.21) | +1 | +1 + hand work |
| Key walk-in, Easy Plain auto-pick (TL 6.21) | 0 | as TL |
| Consequence lock | 3 | +1 |
| Twin Coffer | 2 | 1 |
| Secret: Sound the wall + open | 1 + 1–2 | 6 + 1 |
| Glean (optional) | 1 (+1 per notable decision) | 1 per container |
| Steal | 2 | 1 |
| Truant | 0 extra (fight summary) | — |
| Viaticum / boss card offer | 2 | 0 |

**Auto-close of quiet summaries** (LP-8; touches ITEMS 7 and 8.1). Out of combat, a summary holding only coin and non-healing sundries that all fit takes everything and closes itself, logging to the floor strip. A summary with a notable, a healing sundry or any choice stays open. Toy saving: −9 to −11 clicks per floor.
- **"All fit" (lead's call, after the check)** uses party-wide placement (ITEMS Option D): each sundry goes to the suggested recipient, else the next member with room. If any item fails to place, the summary stays open.
- A merged Glean summary auto-closes under the same test.
- **Accessibility (lead's call, after the check).** The timer pauses while pointer or controller focus is on the summary. A settings toggle (on by default) and a duration setting (`loot.summaryAutoCloseMs`) are offered. Each auto-closed summary's floor-strip log line is clickable and reopens a reassign view for its sundries until the party leaves the room.
- Each auto-close logs `SummaryAutoClosed{containerIds, coin, sundries}`, which feeds `QuietSummaryShare`.

**Glean** (LP-9; M2, optional; lead's call).
- **When:** out of combat, in a room with no awake un-Cowed hostile in sight. One click opens every **visible Plain unlocked** container in `socketId` order into **one merged summary** (still one screen, ITEMS 8.1). Each open routes per TL 6.13.
- **Excluded:** locked, Catch or consequence containers, bait caches, Twin Coffers, Wager and Dare prizes, the signature cache before `BossDefeated`, Windfalls, and the Viaticum and any other container that opens an offer UI (it cannot join a merged summary; added after the check).
- **No-leak rule (Model C):** an **unmarked boxed-lurker host is included**, because it reads as Plain. If Glean reaches it, it springs exactly as a single open would, and Glean stops with the rest unopened. Glean never reveals what a single open would not (V15 `GleanNoLeak`).
- **Also stops on:** a new Revealed or Hunch result, a noticing roll, or a patrol forecast crossing within 2 ticks.
- Told plainly: the toy saving is about 0.25 clicks per floor. Glean ships only if playtest shows players want it.

**Pickup.** In combat, ENM 15.8 auto-gather and free-interaction pickup are unchanged. **7c adds no rule about opening containers in combat**: container work in combat follows TL 6.11 and 6.13 as written (CK3, CB9). A hook asks the ITEMS owner to confirm that a Plain open costs an action in combat.

**Walking.** `LootDetourSquares` is reported (SR0 median ≤ 2).

**7c clock line** (LP-10, beside TL-38, EP-33, HZ-27).
1. **First, re-run the FL walker** with container clicks made explicit. Every 7c saving and cost is measured against *that* baseline, never against an assumed 2 clicks per container (CB6).
   - **Baseline (lead's call, after the check):** the walker on LE's container counts before any 7c cut (no cap, no merges), with this click table's rows applied, auto-close off, Glean off and no Windfall.
   - **7c arm:** every 7c rule on.
   - **Net** = 7c arm − baseline, in clicks (walker) and in minutes. Minutes = walker minutes + `WindfallRoundsDelta` × the seconds per tick of the CMP 7.4 time model (one round is one tick, C 5.5; the companions owner calibrates that constant).
2. **The 7c line is net ≤ 0 minutes and ≤ 0 clicks** on F3 heavy and F4–5 party 4 until CMP 7.4 passes with margin. Auto-close is booked as a credit, and the Windfall's cost (about +0.05 min per Windfall floor, toy) sits inside it.
3. **Cut order on a miss, before any CMP 7.4 lever (reordered after the check; lead's call, after the check):**
   1. `containerCap` −1 on F3–F5 (F5: 10 → 9);
   2. Alcove set-piece share up;
   3. only then the Windfall: presence −250‰ on F3 and F4. **Any Windfall step that would take its frequency lower, or turn it off, returns to the client** (14.2), because the Windfall is the client's agreed answer (Q1). The proposal's "Windfall on F2 only" step is struck: F2 is not reachable at party ≥ 3, so it meant "Windfall off".

### 4.7 Fleeting loot: the Windfall in full (M3; agreed, Q1, Q2, Q6; changes LP-1 to LP-7)

#### 4.7.1 What it is

- **Class.** A **non-combatant unit** (LP-3). It is not a roster type, minion, monster or group member. Its faction is "dream".
- **Body.** It occupies one square. **Units may move through its square but not end a move there** (Model C, so it never walls a corridor). It gives no cover and never provokes or makes opportunity attacks.
- **Economy.** 0 XP, 0 salvage. Never a purse or key carrier. Outside the ambush cap, the joiner caps and the encounter budget.
- **Morale.** None. It is never Routed, Cowed or made to Yield, so C-8 is untouched. Its removal is not a morale event.
- **Combat end.** **It never counts as a hostile**, so it never keeps combat alive (LP-5).
- **Look.**
  - **Always a small, faced, non-fearless animal hauling a bundle larger than itself** (RC3: faces mean minds; LP-7 adds the RC2 Truant grammar). Never an object, because a faceless unit must show the reserved keyhole and an object-shaped unit reads as a boxed lurker (CK1). Never humanoid, which avoids the Escapee, Messenger (EP-35) and Thief reads.
  - The bundle is generic, with the Windfall mark, and never an item silhouette (IR 4.18). **Every Truant drags the same cross-theme bundle (lead's call, after the check):** undyed sackcloth tied with the Windfall mark, larger than the animal, visible in every pose. It is the only bundle grammar.
  - **No faction motif (lead's call, after the check).** A Truant, its bundle and its Rathole never use its theme's faction motif or minion palette (RS 8: the Carnival's stripes and greasepaint, the Supper's napkins and place cards, and the like), because a faction motif says "the enemy's side" and the Truant is in the "dream" faction. Theme identity goes on the animal only. V15s skin vetting and the role-ID gate add the row "Truant or faction minion" (CT1).
  - Its posture is the approved **Dormant** posture and glyph. "Dozing" is art direction only (CK10). Its eyes and limbs stay out in every pose, Dormant included; it never withdraws into a shell or vessel (the boxed-lurker read, RC1).
- ⚑ **ENM 14.5** ("a visibly flagged 'runner' role is a Later option") **and ENM 21's Later list** ("the runner role") hold the runner as **Later**; ENM 17.1's Thief row is a Later role too. LP-3 pulls forward a **non-hostile** runner only, into M3. That is a wave change on an approved Later list, told plainly (CK21). The hostile Thief stays Later. **(New lead's call:** the proposal cited "ENM 14.5 and 17.1"; the runner's Later entries are ENM 14.5 and ENM 21, and 17.1's Thief row is read, not changed.**)**

#### 4.7.2 Where (hosts and exclusions)

- **Floors.** F2–F4, at most 1 per floor, presence `loot.windfallPermille` = 0 / 500 / 1000 / 1000 / 0 *(illustr.)*. Never on trimmed floors. **(Lead's call, after the check:** F3 and F4 raised from 500‰, because the party and skin rules below already make the Windfall rare and the agreed frequency was otherwise far out of reach; F2 stays at 500‰ and is reached only through the solo arm.**)** The client accepted the resulting frequency, about 0.6–0.9 per run *(illustr.)* for the M3 pilot with 1000‰ on F3–F4, on 2026-11-22 **(agreed, Q6**, option A**)**; the simulator owns the figure (9.3 `WindfallsPerRun`).
- **Party** **(agreed, Q2)**. Live party size ≥ 3, read at generation (the LootDoorInvariant input "same seed and party") **and** again when it would wake (combat start or its host group becoming Engaged, 4.7.4; CB8).
  - **Live party size (lead's call, after the check)** = the hero plus companions who are not dead; downed members count. At generation it is the party leaving camp.
  - If the live size at that moment is below the allowed size, the Truant **never wakes**. It stays Dormant and is a plain Take after the fight (1 click, 1 tick, nabbed).
  - **Solo** opens only if its own simulator arm (C = 4) shows greed downs ≤ +2 pp (14.2). **Duo is held**: no duo arm opens it in M3, and duo is reported only.
  - **(New lead's call.)** A floor whose party is below the allowed sizes at generation still consumes its 4 draws; it hosts no Windfall and **sets no `windfallCarry`** (party ineligibility is not a lost host).
  - **The party schedule (told plainly after the check).** Under CMP 7.1 the party is at most 1 / 2 / 3 / 4 / 4 on F1–F5. So, while duo is held, **F2 never hosts a Windfall unless the solo arm opens** (then only for a hero who declined at camp 1); F3 hosts one only for a party with two companions by camp 2 (a hire at each camp, or a declined camp 1 and a carried hire at camp 2); F4 for a party of 3 or more. The M3 pilot is in practice F3–F4.
- **Skin (lead's call, after the check).** The floor's theme must have a non-empty pilot skin list for the current wave (M3: the Choir, the Carnival and the Supper, agreed Q4). This is formation-independent and checked in LootPlan. A theme with no skin is treated like party ineligibility: the 4 draws are consumed, there is no Windfall, and no `windfallCarry` is set. In M3, **Silk and Gut floors and the M3 pool themes without a skin (Moth Wedding, Mirror Court, Ossuary Opera) never host a Truant** (told plainly after the check).
- **Hosts.** A main-path guard post (G) or Gb, or the main-path lair, at main-path index ≥ 3 and not the floor's first fight. **"First fight" (lead's call, after the check)** is the G, Gb or L fight room with the lowest main-path index, whether or not the party later skips a Gb.
- **Never in:**
  - the elite room, the arena, the Forecourt, Sanctum, Antechamber or Stairhead;
  - Circuit rooms or the cover room;
  - rooms with a Void;
  - any room with a clock (Stir, Chainpost, Now-Cord, ticking Landmark, Wager, Dare, Courses, Open Window);
  - Wager, Dare or Strongroom Sites;
  - ambush forms (Mousetrap, Cuckoo, Waylay), or rooms with a boxed lurker or the Annex;
  - Kill Box, Crossfire or Pincer formations;
  - an HE socket's room on the HE-4 path.
- **In LootPlan,** host eligibility uses only **formation-independent** rules: floor, party size, the theme's skin list, room role, main-path index, clocks and Sites (all known after the hazards and traps passes). Formation conflicts, the Annex (chosen by EP) and a boxed lurker are resolved in LootBind (4.3 step 3).
- **Segment 8 (lead's call, after the check).** A clock Landmark is never placed in a room flagged `windfallHost`: the Windfall has priority. V15g's `OneClockPerRoom` runs after segment 8 (4.13).

#### 4.7.3 Home, route and Rathole (draw-free, LootBind)

**Order (lead's call, after the check):** the Rathole first, then the home at exactly route length L from it, then the route. The proposal chose the three independently, so nothing made the route exactly L long.

1. **Rathole.** A wall-decal square on a side wall of the host room, never the entry wall, not a Portal and not adjacent to one, not within 2 wall squares of a Hairline wall face, and never on a dead-end stub. It maximises path distance from the party-entry Portal. Ties go by square index.
2. **Home square.** The first square in square-index order that meets all of:
   - in the group area, ≥ 2 squares from every monster start square, ≥ 4 path squares from the party-entry Portal, and not within 1 move of the entry Portal;
   - **in line of sight from the entry threshold** (visible at reveal; hidden-from-entry is rejected);
   - legal under 4.3;
   - its route (item 3) to the Rathole front square is **exactly L** squares long.
   **L** = `hop` × (3 − `wakeDelay`) = 2 × 2 = 4 squares *(illustr.; with the agreed C = 3)*. L is the same for every party size, because extra steps (Surprised, the solo arm) are held steps, not extra hops (4.7.4). L is stored on `WindfallInstance.routeLength`.
   If no such home exists, the host is lost: the Windfall moves, else drops (4.3 step 3).
3. **Route.** The shortest path home → Rathole front square.
   - Fixed neighbour order N, NE, E, SE, S, SW, W, NW, then square index.
   - It avoids start zones by ≥ 2 squares where possible, plus hazard, Void-adjacent, trap-trigger, Hunch and due squares, Clearways, chokepoints and Portal neighbours.
   - It stays inside the host room. It never runs through the enemy line.
   - The route is drawn at reveal as the **scurry trail** (paired dots, one pair per route square; RC5, LP-7; 6).
4. **Rathole form (lead's call, after the check).** One fixed cross-theme shape in the RC5 registry: an **arched opening at skirting height with the Windfall mark on its lintel**. It is never a crack, seam, cloth flap, hatch, drawer or door. Per-theme dressing changes only its material (5.3). Because a camera-side wall can render as a stub (FL 8.1), every Rathole also projects a floor decal onto its front square, a half-rim plus the Rathole icon, drawn whatever the wall's cutaway state; the got-away act ends on that decal, with the bundle shown crossing the rim. This decal is the precedent for segment 10's wall-mark projection (exploration and information design 4.9.7; XI-18).
5. **Never a theme's trap, Stir or Wager prop as the Rathole, nor any Portal skin (Drape, Squeeze fill, Latch, Door) or Hairline dressing** (V15 `WindfallSkinLegal`; the Supper dumbwaiter and serving hatch, and the Carnival painted flat and tent flap, are banned, CK2, CK22; extended after the check).

#### 4.7.4 Waking, the count and the hop (HZ 6.3 sub-step 3b; change LP-4)

- **Dormant means untouched (lead's call, after the check).** While Dormant with its host group unresolved, nothing nabs it except Steal (4.7.8): no action, area, damage or push affects it, and it is untargetable. An area, damage or push that **starts** combat resolves after combat start and the wake, and nabs it only if it still includes its square then. This closes the free pre-wake nab by a fight-opening area.
- **Wakes (lead's call, after the check)** when **any member of its host group becomes Engaged** (ENM 12) in any fight, or when a fight starts in its room, whichever comes first, if the live party is allowed (≥ 3, or solo once opened); or at once on a failed Steal. Its route stays in its own room. So pulling the host group into a corridor still wakes it and starts its count. The proposal's "combat start in its room" let a pulled group leave it asleep.
- **C** = 3 Dream steps **(agreed, Q1)**, or 4 if the party is Surprised (the Stir `firstAct` precedent). The solo arm, once opened, uses C = 4 **(agreed, Q2)**, and 5 if Surprised **(lead's call, after the check:** the Surprised solo value extends the Stir precedent; Q2 agreed only C = 4**)**.
- **Shown as a countdown that never rises (lead's call, after the check).** The digit starts at **3** in every case. Each extra step above 3 (Surprised, the solo arm, both for 5) is a **held step** right after the stand-up: the digit does not fall, and the Dream card and floor ring show a "held" badge with its reason ("Surprised", "Alone"). Held steps are not the slip and do not set `slipUsed`. The effective count is unchanged; a count shown at reveal never goes up.
- **Sub-step 3b, Windfall hops,** runs after Remnant countdowns and before announcements:
  - **Round-1 Dream step:** it stands up, does not move, and C falls to 2 (`wakeDelay` 1). Held steps follow, if any.
  - **Each later Dream step:** it hops up to `hop` squares along its route, and C falls by 1. At the Dream step where C reaches 0 it **gets away**: through the Rathole if it stands on the front square, else it fades where it stands. Either way it leaves with its bundle.
  - **Square by square (lead's call, after the check).** A hop is checked one route square at a time. It never enters a square holding a unit, or a harmful or obstacle state (fire, web, oil, a Keg; anything created or moved there at runtime), so hopping never damages it.
  - **Blocked** means the hop moved fewer than `hop` squares and did not reach the Rathole front square, even if it moved part of the way. **The first blocked hop holds C** ("held · slip used" on the card; the HZ 6.3 slip precedent). **On any later blocked hop C falls anyway.** Any unit can block it, a monster by accident included; the "slip used" badge keeps a used slip visible. One body cannot freeze it (CB1, CF9).
  - **Early arrival (new lead's call).** If it reaches its Rathole front square while C > 0, it waits there, still a target, and gets away only at the Dream step where C reaches 0. It never leaves early. With the exact-L route (4.7.3) and held steps taken before any hop, it cannot normally happen; the rule stays as a safety.
  - **After 3b,** sub-step 4 outlines its next hop in the **next-hop outline** (RC5; never the due-square hatch). Every unit gets at least one turn between seeing a hop and the hop.
- **Combat end (lead's call, after the check, extending LP-5).** The combat-end check runs **after each Dream sub-step**, not only between Dream steps. Combat ends at the moment the last hostile resolves (a 1c down, a 2b Rout or Yield included). If combat has ended before sub-step 3b, 3b and 4 are skipped and a Truant still in the room is nabbed (LP-5).
- **The party flees.** It gets away at once (`WindfallGotAway{cause: partyFled}`).

#### 4.7.5 Nabbing it (no roll, no draw; change LP-3)

- **What nabs it, once awake:**
  - any **action** that targets it: an attack action, an action-cost spell or ability, a thrown item, or **Grab**, an action from an adjacent square;
  - any area or effect **from any source, on any action economy,** that includes its square (monster areas too, "the dream gives");
  - any damage from any source (Kegs, collisions, ticks);
  - any forced movement applied to it.
- **What does not.** Bonus actions and reactions cannot target it. It never provokes, so no opportunity attack. **There is no free-interaction nab** (toy: a free grab added +10 to +12 pp downs).
- **Out of combat (new lead's call).** While its host group is unresolved, the only verb on a Dormant Truant is **Steal** (4.7.8); Grab and attacks are greyed with the reason "guarded" (ITEMS 7.3), so no out-of-combat free nab exists. Once its host group is resolved without combat it is a free Take (4.7.8, Cow). **Areas and effects (lead's call, after the check):** while Dormant it is untouched by anything but Steal, from any source (4.7.4); "any area from any source" applies only once it is awake.
- **Never woke (lead's call, after the check).** If its host group is resolved in a fight the Truant never woke for, it is a free Take only when the live party was below the allowed size; in every other case the Engaged trigger (4.7.4) has already woken it.
- **A nab is not an attack roll, not a hit, not damage dealt and not a kill** (CB3).
  - No on-hit, on-damage or "you hit" trigger fires.
  - No carry, Opening or stealth die is consumed or gained.
  - No concentration or reaction is provoked.
  - No XP, salvage, on-kill or on-down trigger, Mark transfer or morale checkpoint.
  - Areas count it as no extra target for "each creature hit" riders.
  - Each class owner confirms the wording (V15s: every ability with an `onHit` tag lists `ignoresWindfall`).
- **Nabbed.** The bundle drops on its square or the nearest safe square (ENM 15.6 order), and the Truant settles back into the dream (removed, no residue but the pile). Mid-fight, the dropped card and coin are a normal dropped pile (free-interaction pickup allowed, auto-gathered at combat end, ENM 15.8). Log: "Nabbed the <skin>". The card moves `windfall(unitId)` → `ground` (`ItemDropped`; 4.7.7).
- **Kill method never destroys contents.** Fire, areas, pushes and Voids all leave the bundle (it is never pushed into a Void, because forced movement nabs it first).

#### 4.7.6 Contents and budget (agreed, Q1; change LP-1; lead's call for the card source)

- **Socket.** `windfall`, a new **SR2** row. It is outside the lock quota, inside the premium share, with **no step**. It is a unit socket, not a container, so LX11 holds and no container is added (told plainly).
- **The `windfall` socket is not in the steps 1–7 socket list (lead's call, after the check).** It is created only in step 8, so step 3's ordering and step 5's deal never see it.
- **The card moves in a new allocation step 8.** This is draw-free and runs after LE 6.9 item 2 step 7, so the step-6 shuffles and step-7 swap never touch it (CB2, CK4). The grandchild's 4 draws are taken at the start of step 8 (4.7.10). Take the first that exists:
  1. **(i)** the lowest-tier **carried card of the host encounter**, never the elite's, passing filters (f1) and (g). Ties go to the highest deck index.
  2. **(ii)** else the lowest-tier card on a **plain SR1 container** passing (f1) and (g); that container keeps coin plus 1 non-healing sundry.
  3. **(iii)** else **no Windfall on this floor**. Its draws are consumed and `windfallCarry` is set.
- **Ledger and channel tallies** record the card in its **origin channel** with an **`onWindfall`** tag **(new lead's call:** renamed from the draft's `heldBy`, a VG data predicate**)**. The ENM 15.3 split is unchanged. **The channel-slot mapping is fixed at the end of step 7** (lead's call, after the check), and the Windfall keeps its origin socket's slot index *i*: no carried or container slot is recounted after the card moves.
- **Filter (f), new, in two parts (lead's call, after the check),** because ordinary identities are drawn later, on the parent stream (LE 6.10 items 2 and 4):
  - **(f1), card-level, at step 8:** exactly 1 card. Never the deck's top card or a VR; the Due, the DH block or the Viaticum; a key, a signature or a step card; a dual-identity card.
  - **(f2), identity-level, at the parent draw:** the Windfall's slot is drawn filter-first (LE 6.10 item 1) with `oncePerRun` records excluded, so it can never hold one.
- **Filter (g), new:** never a card from a secret, the quota socket, a trimmed floor's Alcove reward, a Wager, Dare or Strongroom, or the elite carrier. Secrets keep their card, so careful exploration never pays less on a Windfall floor (CB5).
- **Why the host's carried card (lead's call; told plainly).** The toy showed the F3–F4 container channel is down to 3 cards (the Due, the quota and the secret's), so a container-only source would leave the Truant coin-only on 2 of 3 floors, which is a coin-tax goblin (CF1). Taking a carried card from the fight that hosts it keeps the stake inside the same fight, starves no off-path source and leaves the Steal target count intact (V15). **This source was not simulated** in the toy, and is the first M3 simulator arm. The client chose it with Q1 option A.
- **Lowest tier kept; a richer arm is run (lead's call, after the check).** The play check found that the lowest-tier rule makes the visible stake usually a Common, so a nab avoids a loss rather than gaining anything. The rule stays, because the client accepted the Windfall as written and a higher tier raises the VP at risk under permadeath. The first M3 arm also runs a **highest eligible tier** variant (still under (f1) and (g), so never the top card or a VR) and the **width-2 offer on nab** as report-only arms. Either is adopted only if it passes the same `WindfallGreedDowns` ≤ +1 pp gate, and the tier rule change returns to the client (14.2).
- **Coin.** A weight-3 share from the pool, carved at generation (LootBind step 7, after the Windfall is final) on the pre-escape pool and **never re-carved** (CB13). Never a K2 lump.
- **Excitement without VP.**
  - A visible tier at reveal.
  - The reserved cue family.
  - The run-record line.
  - **A width-2 offer on nab (Yours plus Wayward) is held as an M4 lever**, to be used only if `WindfallIgnoreShare` is high. If adopted it uses its own keyed grandchild, so it can be added later without shifting other streams (CB12).
- **Contested: extra value.** Rejected **(agreed, Q1**: option C rejected**)**.

#### 4.7.7 Getting away

- **Three causes, one treatment (lead's call, after the check).** At C = 0 through the Rathole (`rathole`), at C = 0 fading where it stands short of it (`faded`), or at once when the party flees (`partyFled`), it leaves with its bundle: "The <skin> got away with a <tier word> treasure". The event is `WindfallGotAway{cause, cardRef, tier, vp, coin}`, and every cause applies n − 1, `ItemDestroyed{gotAway}`, the coin removal and the log line.
- **Card.** Recorded as `ItemDestroyed{gotAway}` (LP-2, a new ITEMS 18.3 reason). **Not a dreamed option**: it never enters Scion memory or "What the dream kept" (CK6). (Its generation already wrote Scion-line memory, as every generated identity does, IR 4.7.)
- **Item location (lead's call, after the check; LP-2).** While on a Truant the card's ITEMS 18.1 `location` is a new value **`windfall(unitId)`** (`carried(monsterId)` is for monsters). LP-2 adds four rows to the ITEMS 18.1 location-transition table, which 19.2 asserts: generation → `windfall` (the floor loot draw, extending the first row; `ItemCreated`); `windfall` → `ground` (nab; `ItemDropped`); `windfall` → thief's `pack`, else `leftovers` (Steal; `ItemTaken` or `ItemLeft`); `windfall` → deleted (getaway; `ItemDestroyed{gotAway}`). `windfall` → deleted at the stairs joins the existing stairs row (`ItemDestroyed{lost}`).
- **Coin.** Removed from found coin, reported as `WindfallEscapedCoin`.
- **Treasure count.** n − 1. Grave Goods: "Got away: <tier word>".
- **Untouched by construction (V15 `WindfallOffGuarantees`):** the Due, DH, Viaticum, keys, signatures, secrets and the lean band.

#### 4.7.8 Steal, Cow, classes, AI and companions (changes LP-6, LP-33)

- **Steal on a Dormant Truant** (LP-6 ⚑ settled ENM 15.5, S-5, STAT 7.1 Stealth row, LE-16; **agreed, Q1**).
  - **Conditions:** adjacent; no awake un-Cowed hostile in its room has line of sight to the thief.
  - **Check:** Stealth against the Truant's fixed passive **20, −5 Dormant = 15** *(illustr.; tuned so `WindfallStealShare` ≤ 40% in Shade parties)*. The value is a fixed row, not the group trait Keen (CK9). Light Fingers gives advantage.
  - **Roll:** made on the keyed hash `d = H(floorKey/checks, "h:windfallSteal", windfallId, 0, die)`, `d20 = 1 + Bound(d, 20)`, on `checks` (the keyed-check domain `h:windfallSteal`; SV-1, SV-6, M3; seeds and validation design 4.1, 4.3), with `die` 0, and 0 and 1 for advantage, as in TL 6.22, so it never shifts the combat stream (CB7; die index added after the check).
  - **The room's one Steal try.** The party steals either a carrier in the host group or the Truant, not both (CF4). **(Lead's call, after the check:)** the host group's runtime state carries `stealTryUsed`, set by either Steal path and checked by both.
  - **Success takes the card only** (LE-16 holds). The coin spills on its square as a dropped pile. **The pile is "guarded" while its host group is unresolved** (lead's call, after the check, matching 4.7.5); it becomes a 1-tick Take once the group is resolved, or is auto-gathered at the end of a fight. The Truant plays its stolen act (6) and leaves. k + 1. Logged `WindfallStolen{unit}`; `WindfallState.state` becomes `stolen`.
  - **Failure** starts the fight as any failed Steal does (nobody Surprised). The Truant wakes **at its starting count for the party size** (3, or the solo arm's 4) **after the live-size check**: below the allowed size it stays Dormant (lead's call, after the check). There is no extra Tattle (rejecting Model C's squeal).
  - The elite-room "< 15% notables without a fight" metric excludes the Truant. `StealCoinShare` counts nothing from a Windfall, because its coin is Taken, not stolen.
- **Cow.** Cow never targets a Windfall. If the host group is resolved **without combat** (Cowed, Yielded, Routed or gone), the Dormant Truant never wakes and is a free Take. Told plainly: fresh guard-post and lair groups are rarely Cow-eligible (ENM 14.8), so this fires on under about 5% of hosts and is **not** advertised as a Herald payoff (CF8, CK16).
- **Classes.**
  - Vanguard: a body on its next route square slips it once; Shove, Press and collisions nab it.
  - Arcanist: any area or action cantrip nabs it.
  - Shade: Steal before the fight, or ranged.
  - Herald and every hero: an action or a thrown item.
  - **V15 `WindfallCatchable`** proves a nab line for each class at the floor's level. **Algorithm (lead's call, after the check):** for each class, with its base speed and its level-legal action ranges, ignoring enemies: from any square within one move of the party-entry Portal, some square reachable within r rounds can target the Truant's bound route position at round r, for some r ≤ 3 (the displayed count; held steps only add time). The home is also never within 1 move of the entry Portal (4.7.3). The proposal's "no party start square adjacent to home" is dropped: party start squares exist only in ambush templates, which never host a Windfall.
- **Monster AI.** Ignores it. The scorer never targets, protects, paths for or values it (ENM 15.4 and 15.5 hold), and area scorers neither value nor avoid its square. Accidental blocking is allowed and deterministic.
- **Companions** (LP-33, a request to the CMP owner; recast after the check). **The player controls every companion (decided, MAIN 4.2; CMP 8.6).** A companion never acts on the Truant by itself, and there is no "Nab it" order. LP-33 only makes the Truant a legal target for CMP 8.6's suggestions (off by default) and for the simulator's companion policy: a `targetRule` value `windfall` with the condition "reachable this turn without ending in the reach of more than 1 hostile". The proposal's borrowed "DH 17.3 danger band" is dropped, because that band is the hero's alone. Reported: `CompanionChaseDowns`.

#### 4.7.9 Fairness under permadeath

- It deals no damage. Warned or floored, the EnvRound cap and DH 10.4 are untouched.
- It is visible at reveal. Its route and Rathole are on the map, and every hop is outlined a turn ahead.
- The route stays inside its room, away from start zones. The chase never pulls the party into an unseen room (the Diablo lesson).
- The answer is one action, and winning nabs it. Auto-gather means nobody walks to the drop.
- **Told plainly (CF2).** In the toy almost every party spends the action (nab rate 0.97; `FightEndNabShare` 0.05). The decision is **when** to spend it, not whether. We therefore **drop `FightEndNabShare` as a gate** and keep it as a report. The Truant is honestly a small tempo choice with a visible stake.
- **Gates** (*illustr.*):

  | Report | Target |
  |---|---|
  | `WindfallGreedDowns` / `WindfallChaseDeathShare` | ≤ +1 pp against the control arm at party ≥ 3 (solo arm: ≤ +2 pp to open, agreed Q2) |
  | `WindfallFirstActionShare` | ≤ 40% |
  | `WindfallNabRate` | 80–95% |
  | `WindfallRoundsDelta` | ≤ +0.2, averaged over host fights |
  | `WindfallStealShare` (Shade parties) | ≤ 40% |
  | `WindfallBlockedRounds` | ≤ 1 per Truant, enforced by rule |

- **How the gates are computed (lead's call, after the check).**
  - **`WindfallGreedDowns`** = (downs per host fight with the Windfall on) − (downs per the same fight with it off), on **paired seeds** (same floor, same fight, the Windfall the only difference), under section 8's policy mix (30% greedy, 40% balanced, 20% smart, 10% ignore). It is expressed in percentage points of the per-fight down probability, counted per host fight (not per run).
  - **`WindfallChaseDeathShare`** = deaths in host fights where the dying unit spent an action on the Truant within its last 2 turns, ÷ deaths in host fights; gated as its Windfall-on minus Windfall-off difference on the same paired seeds.
  - **Confidence.** A gate passes only when the upper bound of the 95% confidence interval on the paired difference is within the target, over at least 20,000 paired host fights per party size *(illustr.; the simulator owner may raise it)*. This matters most for the solo arm, whose pass opens solo without a further client step (Q2).

#### 4.7.10 Frequency, carry and determinism

- **Grandchild.** `Derive(lootPlan, "windfall", 0)` from the `lootPlan` key under the one key algebra (`LabelId` is FNV-1a, an input to `Mix` only), path `loot/lootPlan/{N}/windfall/0`, a stream-registry row (SV-1, SV-2, M1 rule; seeds and validation design 4.1–4.3), in LE 6.10 item 1's scheme (`lootPlan` is already per floor, so the kind is `windfall` and the entry index 0; renamed after the check from the proposal's `lootPlan/windfall/<floor>`). It has **4 draws consumed on every floor, F1–F5**, door or no door, host or no host, party size or skin or not, taken at the **start of step 8** (4.7.6):
  1. presence: on when `Bound(draw, 1000)` < `windfallPermille[f]` (multiply-shift replaces `mod 1000`; SV-1, M1 rule);
  2. host: `eligible[Bound(draw, count)]` (SV-1), with the formation-independent eligible hosts ordered by `socketId` (unused when there are none; `Bound` is never called with count 0);
  3. pad;
  4. pad.
  LP-1 adds this as a new row of LE 6.10 item 1's draw table (`windfall`, 4 draws, 1 entry per floor, F1–F5) and step 8 to item 4's generation order (lead's call, after the check).
- **Skin.** `skins[socketOrdinal mod len]`, where `skins` is the theme's pilot skin list for the wave and `socketOrdinal` is the host socket's position among the floor's sockets in `socketId` order. No draw. An empty list means no Windfall (4.7.2).
- **Carry** (CB4, CK14). `windfallCarry` is a persisted run-state boolean written in the atomic camp-exit save. **Its lifetime (lead's call, after the check):**
  - When set, the next floor's presence is forced on if a legal host exists. The presence draw is still consumed.
  - It is set only by a lost host (`WindfallDropped`) or no card source (4.7.6 (iii)), never by party or skin ineligibility (4.7.2). It never stacks.
  - It is **cleared when a floor's Windfall survives LootBind**, whether presence was forced or would have passed anyway.
  - It **survives** floors that are ineligible by party or skin (it waits for an eligible floor).
  - It is **cleared at the end of F4 generation** whatever happens.
  - Set and clear are written in the same atomic camp-exit save.
  - Toy: about 1.2 per run without carry, about 1.4 with it, **at party 4 and with every theme skinned**. The lead's re-estimate under the party ladder and the M3 skins, about 0.6–0.9 per run *(illustr.)*, is in 0.1 and was accepted for the M3 pilot (agreed, Q6); the simulator reports the real figure by theme mix (9.3 `WindfallsPerRun`).
- **Runtime.** No draws at wake, hop, nab, slip or escape. The only roll is the keyed-hash Steal. Save and resume mid-fight persist `WindfallState{c, state, square, slipUsed, heldSteps}` and the host group's `stealTryUsed`. Reload changes nothing.
- **LootDoorInvariant.** Card identities never change by door. Presence and host may differ by door (placement), which is logged. V15 golden seeds check this (golden seeds and the release seed set: seeds and validation design 4.11; SV-18).

#### 4.7.11 The Ebb and the exploration Truant (M4; agreed, Q3; change LP-34)

- **Ebb.** A Plain, never-locked fading container kind (LP-34).
  - It sits on a **dry plain square** in an off-path side room or Alcove, visible from the threshold with its digit.
  - Its count starts when a party unit first crosses the threshold, at **C = 8–12 ticks** *(illustr.)*.
  - Placed **only on forking geometry**: the safe route is ≥ the fast route + 2, and the fast route crosses one shown risk (a Hunch zone, a Squeeze, a Latch).
  - It uses **click-to-move with per-step ticks**, never one click per step. Both routes' tick counts are shown.
  - It fades one notch per tick, as an act of ≤ 1.0 s added to RC1's closed motion list.
  - At 0, `gotAway`. If a fight starts in its room, the remaining count converts to Dream steps.
  - Skins **(agreed, Q4)**: the Choir Offertory Plate (sinking into dry flagstones), the Carnival Prize Balloon (lifting on its sandbag string) and the Gut swallowed bauble (absorbed into a lining fold; never in a Peristalsis room).
- **Exploration Truant.** A Dormant Truant in a group-free side room that wakes on a noticing band and runs on chase ticks, placed only where the generator proves an intercept line. M4, because out-of-combat targeting needs the ability owner and main 4.3 (CF7).
- Told plainly: the client's "during exploration" case has **no M3 form** (agreed, Q3).
- **No F1 Ebb** **(agreed, Q4**: option B, a coin-only F1 teaching Ebb, rejected**)**.

#### 4.7.12 Rejected and Later

| Option | Verdict |
|---|---|
| Mid-fight spawn or landing pod | Rejected (RC1, EP 13, determinism). M4 at most, as a pre-announced fall onto due squares |
| Coin and sundry only | Rejected (agreed, Q1 option B: likely ignored) |
| Extra value | Rejected (agreed, Q1 option C) |
| No fleeting loot | Rejected (agreed, Q1 option D) |
| All party sizes from the start | Rejected (agreed, Q2 option B) |
| Ebb in M3 | Rejected (agreed, Q3 option B) |
| F1 coin-only teaching Ebb | Rejected (agreed, Q4 option B) |
| Calls help (Tattler) | Later: it funds an unbudgeted fight and punishes chasing twice |
| Enemies destroy it | Rejected (ENM 15.6) |
| Free-interaction grab | Rejected (+10 to +12 pp downs) |
| Hidden-from-entry home | Rejected (+2.7 pp downs) |
| C falls while blocked | Rejected (kills the intercept). Slip once instead |
| Pips not a digit | Rejected (a lying exception) |
| Object-skinned Truants, Flotsam | Rejected as Truants. Object skins are Ebbs only |
| Void exit | M4 Nightmare-tier option, Ebb only |
| Trove (speed-graded offer) | Later (party-size bias, card starvation, third timed family) |
| Hawker | Segment 8 hook, "the Hawker's Hour", untimed; Later, a skin of the ITEMS 10.9 trader (events design EV-42) |
| Lost Property buy-back | Camp hook, tested only if greed gates miss |
| Herald shout holds it | Later |

### 4.8 Traps, hazards, encounters, stealth, Steal and Cow (lead's call)

- **Traps and locks.**
  - Lock counts follow TL 6.20. Fewer locks is a design-time TL tuning lever if the cap binds, never a generation-time request (4.2 rule 5).
  - Traps-bound sockets keep the traps pass's square (4.3).
  - `HeNoLock` and `LockNeverCritical` hold.
  - A Catch is the only trap on a container square.
  - Hunch zones may lie on the path to SR1+ sockets, never to an SR0 or HE open square.
  - The Wax Key is refused on secrets and Wager cages (TL).
  - Boxed lurkers sit ≥ 3 squares from any Truant home.
- **Hazards.**
  - No source on hazard, Void-adjacent, Stir-footprint or Tide squares.
  - The Dare reward sits at the footprint's far end.
  - Fallen loot lands on the edge (HZ 13).
  - A Windfall never shares a room with a clock.
- **Encounters.**
  - Guarded loot sits behind its guards (4.2).
  - Carriers are Steal-reachable.
  - Activity containers count toward the cap.
  - The Annex adds no prize.
  - Monsters never pick up items.
- **Stealth and Steal.**
  - ≥ 1 Steal target per floor, settled. V15 checks that it survives the Windfall's card move.
  - Steal takes one of {notable, purse, key}.
  - The Heavy Purse is never on the elite.
- **Cow.** A Cowed carrier keeps its loot, except the floor key (TL-23). A Cowed group pays no salvage (ENM 15.7). Tribute is not adopted (7).

### 4.9 Floor 1 (M1 for the Kennel subset; M2)

- Always solo (EP 6.17).
- Containers 5–6, cap 8 *(illustr.)*.
- Plain-dominant, with ≤ 1 consequence lock after room 2.
- Main-path coin ≥ 17; the first lump in the entry container.
- The Due is a U weapon or focus for the hero, on the first HE-7.
- Secret 0–1 (Gaol and Wake), "+1 secret", outside n.
- ≤ 1 carried notable per encounter; carried consumables self-targeting only.
- No lurker in rooms 0–2; no surprise in rooms 0–1.
- No Windfall, Ebb, Wager, Dare, lurker, Mousetrap, Strongroom, Twin Coffer or Heavy Purse.
- Proprietor tags teach that room logic predicts loot.
- **M1:** containers, at most 1 carried weapon or armour notable, the Due, and **Old Ragged guarding Bellwarden's Mail** in its den (the first signature cache).
- Told plainly: the client's Gaol "escaping prisoner" is declined for F1. F1 teaches with nothing timed (RT-20, DH 17.1), and a prisoner reads as the Escapee (RC1, RC2) **(agreed, Q4)**.

### 4.10 Floor 5, the Viaticum and boss loot (segment 9 hook; agreed, Q5; change LP-14)

- **Viaticum.** On F5 the lair room's host socket takes kind `viaticum` (unranked, boss channel) in place of `lairContainer`, so the room keeps one card-bearing container. It sits on the lair room's exit side toward the Forecourt, outside the start zone, on the walk line. This is a **reading of LE 6.11 item 5**, whose stated fallback is the real rule. **The FL and ENM owners confirm** (LE 18.2). Taking it during or after the lair fight follows TL as written.
  - It opens the boss-reward offer UI at width 3. Its host is the **Bedside Stand** (LP-14), a nightstand with three drawers, accepted as the **second named exception** to IR 4.18's "container art never implies contents", because an ordinary tin would lie about what clicking it does **(agreed, Q5)**.
  - **Scale and open act (lead's call, after the check).** The proposal's "child-scale" means the scale of a child's furniture to a unit, never the Heart's giant nursery scale: it fits one square, stands below unit shoulder height, and has three short drawers stacked on legs with an open underside, so it never reads as a Tall piece (ROOM's wardrobe and block tower). Its open act: the three drawers slide out together in ≤ 1.0 s, then the width-3 offer UI opens; the opened state shows three empty drawers. **The Heart's drawer-slide Latch skin is banned from every F5 lair room**, and the role-ID gate adds "Bedside Stand or Latch, Furnishing".
  - **The Bedside Stand silhouette hosts only the Viaticum** **(new lead's call)**: no other container, Furnishing, piece or Keepsake on any floor uses it (V15s `BedsideStandOnly`), so the exception never leaks.
  - The lean band is computed with the Viaticum counted in every arm, stealth or fight (CB10).
- **Signature cache.** One per guarded-form boss, in its arena, per 4.4. Unranked, never locked or trapped, holds nothing else, opens on `BossDefeated` (boss rooms design 4.2.4; BR-37, M2).
- **Armed boss:** carries its signature. Segment 9 authors each boss's `armed` flag and issue gear so a carried form is the piece the boss visibly uses, chassis only; no second form is chosen on F5 (boss rooms design 4.11.1; BR-27, M2). **Boss card:** C/U/R/R on F1–F4, separate screen. **Memento:** victory screen.

### 4.11 Determinism (lead's call; LE 6.10, RS 6.4)

- **SocketPlan** runs before step 3a (LootPlan), draw-free, and builds the provisional socket set (4.2).
- **LootPlan (step 3a).**
  - The normal allocation, steps 1–7, unchanged. The channel-slot mapping is fixed at the end of step 7.
  - **New step 8** (Windfall card move), draw-free, inside step 3a. The `windfall` grandchild's 4 fixed draws are taken at its start, on every floor (4.7.10).
- **Step 3 (parent stream).** The Windfall's slot is drawn filter-first without `oncePerRun` records (filter (f2), 4.7.6).
- **LootBind.** Draw-free, in 4.3's numbered order: item-7 remaps and the offer-pacing trim; container squares; the Windfall host check, move or drop; home, Rathole, route and C; merges; Proprietors and skins; coin lumps, purse and salvage.
- **Runtime.** No draws at open, Glean, wake, hop, nab, slip or escape. The only existing checks are Steal (Truant: keyed hash), lock checks and noticing; out of combat, Steal and noticing roll on the floor's `checks` keyed domains, never the combat stream (SV-6; seeds and validation design 4.3).
- **Assertions.** V13g and V15 assert every filter (a)–(g) **after step 8** ((f2) after the parent draw). A hook goes to the LE owner: the same latent shuffle issue exists for secret filter (b) and the quota card at steps 6–7 (CB2, pre-existing).
- **Client switches add no draws.** Opening the solo arm (Q2) changes no draw count or position: the 4 draws are consumed at every party size (V15 draw-count test).

### 4.12 UI hooks (segment 10)

- Container hover: Proprietor tag, tier marks, lock card.
- Treasure strip k / n with the by-source hover; "best remaining"; the "got away" log.
  - **Naming (XI-19, M2):** the cell is the floor strip's **Treasure field**; "Treasure strip" is retired as a synonym here and in 4.5 (exploration and information design 4.9.9).
  - **Counts only, narrowed (XI-23 ⚑, M3):** the by-source hover and every other free surface stay counts only, never a location; a paid or earned pointer (the Lodestring, F-TREASURE) may name one room, with its tier word and the "plain cache" / "behind risk" qualifier, notables only, class-blind (exploration and information design 4.6.1, 4.6.2).
- Stratum Treasure line suffix.
- Auto-close quiet summaries, their pause on focus, the settings toggle and duration, and their clickable log lines; the Glean button and merged summary.
- **Windfall:**
  - the non-combatant plate frame, Windfall mark and Dormant glyph;
  - the templated hover line;
  - the scurry trail, the next-hop outline, the Rathole decal and its front-square floor decal;
  - the digit on its floor ring (hollow while Dormant, solid while awake) and the Dream card, and the "held" badge with its reason ("Surprised", "Alone", "slip used");
  - the Steal preview row "Windfall: card (coin spills)";
  - the greyed "guarded" reason on Grab, attacks and the spilled coin out of combat;
  - the Truant as a companion suggestion target (LP-33; suggestions off by default);
  - the run-record lines.
- The Uncorked glint state.
- The Twin Coffer twin-lid mark.
- The Bedside Stand opening the width-3 offer UI.

### 4.13 Hooks to segments 8, 9 and 10

- **Segment 8 (events).**
  - Q rooms and Forecourt micro-events may **skin** a 7c socket but never add one. Answered by `CuriosityLootBlind`: a Curiosity frames its room's existing container with wall or backdrop dressing only; the container keeps its silhouette and CT2 art, opens with no check, and its contents are never an option outcome (events design 4.4.5; EV-15, M2).
  - No event spawns or moves units (EP 13), so Windfalls are 7c's alone.
  - A ticking Landmark makes a clock room. **Segment 8 never places a clock Landmark in a room flagged `windfallHost`** (the Windfall has priority; lead's call, after the check), and V15g's `OneClockPerRoom` runs after segment 8. Segment 8 places no Landmarks at all (events design 4.2; EV-35, M2). EventPlace runs draw-free after LootBind and before V13g / V15g (`OneClockPerRoom`); loot never reads Curiosities (events design 4.10; EV-21, M2).
  - Curiosity props never use a container, bait-cache, Windfall or Bedside Stand silhouette (LP-14, LP-24 read; events design 4.11; EV-25, M2).
  - Dream Fragments may carry Windfall lore and claim the elite intel family.
  - "The Hawker's Hour" is offered as an untimed bargain event. **Later:** a skin of the ITEMS 10.9 trader, with the items owner and camp and hub (events design 4.13; EV-42).
  - **Alcove occupancy (EV-2, M2; 7c applies):** an F1 Curiosity's first host is an Alcove set piece, taking the set-piece slot LP-11 prefers, never emptying a leaf or displacing a container the cap needs (events design 4.2).
  - **Pending requests to 7c (segment 8; not rules until ruled):**
    - `stepFunded`, a LootBind flag for priced extra routes to unstepped ordinary locks (4.3; events design 4.4.5; EV-15, M2). If refused: no priced lock routes (events design 14.2).
    - F-CACHE, a fact naming the contents of one optional, unstepped, non-HE, non-Due container, counted as exposure under LE 6.9 and respecting CT2 `ContainerArtBlind` and the `known` field (events design 4.4.1; EV-7, M2; with the LE owner). If refused: S2 gains F-ROOM (events design 14.2).
- **Segment 9 (boss rooms).**
  - The signature cache's arena square list, using 4.4's filters, and confirmation of the reading that IR 4.13 and 4.18's "lair of every boss" means the arena (13). **Answered:** `cacheCandidates[]` per arena template (boss rooms design 4.1.1); "lair = arena" confirmed (boss rooms design 4.11.1; BR-27, M2).
  - The boss card screen and the Memento (boss rooms design 4.11.2).
  - No Windfall or other loot clock in arenas, the Antechamber or the Sanctum. **Answered:** no Windfall in arenas (boss rooms design 11).
  - The Viaticum sits in the lair before the Forecourt.
- **Segment 10 (UI).** 4.12. **Answered** by the exploration and information design (approved 2026-11-23; its 5 and 4.9.9): the Treasure field naming (XI-19), the counts-only narrowing for paid or earned pointers (XI-23, M3) and the pending `SideRoomPayoffMix` request (4.2; XI-27).

---

## 5. Per-theme source catalogue (lead's call; agreed, Q4 for the Windfall skins; changes LP-15 to LP-25)

### 5.1 Cross-theme rules (CT; lead's calls; M2 unless stated)

| Rule | Content | Change |
|---|---|---|
| CT1 | Silhouette families never overlap within a theme: loot containers, catch hosts, boxed-lurker host (shares with loot by design), bait cache, Furnishings and Low pieces, Kegs, reserved trap tells, Truants, cross-theme variants (signature cache, Wager cages, Viaticum host). Role-ID gate rows: "container or Furnishing", "container or bait", "Truant or minion", "Truant or boxed lurker"; **added after the check:** "Truant or faction minion", "inset container or Latch, Door or Hairline", "Rathole or Portal skin", "Bedside Stand or Latch, Furnishing". Truants and their bundles never use the theme's faction motif or minion palette (4.7.1) | LP-15 |
| CT2 | **ContainerArtBlind.** Two variants per family, chosen by a display hash of (floorSeed, containerId) (the `display` key's `containerArt` domain over (floor, containerId), a `DisplayKey` that Core never references; SV-2, M1; seeds and validation design 4.3). No size step by rank, no glow, beam or tinted lid | LP-15 |
| CT3 | Locks are skins on one padlock base plus a kind glyph. Never a keyhole, face part, ring or clock face | (settled TL 8) |
| CT4 | Containers never idle. Open act ≤ 1.0 s. The opened state is true (lid up, empty). Truants are units with a Dormant breathing idle | (settled RC1) |
| CT5 | Tier marks are UI only, colour-blind safe by count and word | (settled LE 6.15) |
| CT6 | **No Due shrine.** The Due sits in an ordinary HE-7 container; its moment is UI (told plainly, contesting the brief) | — |
| CT7 | Signature cache: one cross-theme variant; a theme changes only materials | (settled IR 4.13) |
| CT8 | Twin Coffer twin-lid kind mark (RC5) | LP-26 (M3) |
| CT9 | Carried loot: the ENM glint is the only rule mark; a purse prop is art only | (settled) |
| CT10 | Keybearers: key pip plus a hip keyring prop. Only the material changes | (settled TL-23) |
| CT11 | **The Treasure strip stays global** (RC7; contesting a per-theme skin) | — |
| CT12 | Global container-open cue plus an optional quiet theme foley tail | LP-23 |
| CT13 | Per-theme Hairline dressing beside the universal tell, only where a Hairline exists; no decoys | LP-22 |
| CT14 | Containers and Truants only on plain walkable squares | LP-25 |
| CT15 | **Insets never read as Portals (lead's call, after the check).** Each theme's inset is waist-high on its wall-set bracket, at most half a wall square wide, with no floor-level opening; opened, a shallow empty back, never a passage. It never shares a silhouette with the theme's Latch, Door or Hairline skins (FL Portal skin table: hatch, book-lift, dumbwaiter, drawer slide; the Gut's sphincter Door). `ContainerFamilyDistinct` checks it | LP-15 |

**Keepsake dressing on F5 never uses a container, bait or Windfall silhouette** (LP-24), nor the Bedside Stand's (4.10).

### 5.2 Summary

| Theme (floors) | Plain family | Named containers and locks | M3 prizes | Activity | Truant (M3 pilot / M4) | Ebb (M4) | Signature loot moment |
|---|---|---|---|---|---|---|---|
| Gaol (F1) | Confiscation tins | Cell padlocks, 1 Crier, Confession Box catch, Hour lock (M3) | — | Dice pot | none (F1) | none | The keyring cell cache seen through a Grille |
| Kennel (F1) | Poacher's creels | Pen box (M1), Huntsman's lodge chest | — | — | none | none | Old Ragged guarding Bellwarden's Mail (M1) |
| Wake (F1 cond.) | Grave-goods caskets | Will-Box, Viewing Door (Tattle) | — | — | none | none | The Will-Box in the Undertaker's reach |
| Choir (F2–3) | Alms chests | Barb, Vestry door | Loft of Responses; Sacristy | — | **Alms Crab (pilot)** | Offertory Plate | The Vestry door cache (M2); the Sacristy treasury (M3) |
| Silk (F2–3) | Archive chests | Web seal, Crier, Closed-Stack cage | Restricted Section; Rare Books Room | — | Silverfish (M4) | none | The Closed-Stack cage key from the Head Archivist |
| Carnival (F2–3) | Prize hampers | Crier, High-Striker cage, Ring key, Ticket (M3) | Last Act domes; Box Office | Busker's hat | **Conjuror's Rabbit (pilot)** | Prize Balloon | The High-Striker prize cage |
| Supper (F4) | Silver canteens | Barb, Iced padlock, Tattle, cellar door | Top Table cloches; Plate Room (F3 only) | Feast plate | **Crumb Mouse (pilot)** | none | The cellar door cache (M2); the Top Table under cloches (M3) |
| Gut (F4) | Sphincter caches, savings tins | Tenant's strongbox, Landlord's ledger safe | Stomach birdcages | Rent pot | Rent Snail (M4) | Swallowed bauble | The Landlord carrying the rent (Heavy Purse) |
| Heart (F5) | Biscuit tins | Diary box, Nursery door | Night Nursery birdcages | Knitting basket | none (F5) | none | The Bedside Stand Viaticum |
| Inverted Cathedral (M4) | Up-chained chests | Rood Screen gate | Rose Window lanterns | — | none | Rising Votive | TBD with roster |

**Signature loot moments (lead's call, after the check).** A moment is a guaranteed socket plus one bespoke asset and one cue, so art and audio can schedule it. Every theme has one from its first shipping wave; an M3 site upgrades it when its wave ships.

| Theme | Host socket and rank | First wave | Guarantee | Bespoke asset | Cue |
|---|---|---|---|---|---|
| Gaol | Keyed cell cache seen through a Grille (`keyTarget`, SR2) | M2 | Once per floor where the Turnkey carries the key | Grille reveal framing on the cache | Tin scrape tail under the global open cue (CT12) |
| Kennel | Old Ragged's signature cache (unranked) | M1 | Every Kennel floor (guarded form) | Moss-wood cache open act ≤ 1.0 s | Theme foley tail |
| Wake | The Will-Box (`keyTarget`, SR2) | M2 cond. | Where the Undertaker exists | Lid-lift open act ≤ 1.0 s | Theme foley tail |
| Choir | M2: the Vestry door Keyed cache from the Deacon (SR2). M3: the Sacristy treasury (`strongroom`, SR4) | M2 / M3 | Where the Deacon exists; the Sacristy on F2–3 Strongroom floors | Vestry door reveal framing; the Sacristy open act | Theme foley tail |
| Silk | The Closed-Stack cage from the Head Archivist (`keyTarget`, SR2) | M2 | Where the Head Archivist exists | Cage-shutter open act ≤ 1.0 s | Theme foley tail |
| Carnival | The High-Striker prize cage (`lockedCache`, SR2) | M2 | Once per Carnival floor with a cage lock | Threshold-glyph reveal framing | Theme foley tail |
| Supper | M2: the cellar door cache from the Sommelier (SR2). M3: the Top Table under cloches (`wagerCage`, SR5) | M2 / M3 | Where the Sommelier exists; the Top Table where a Wager is placed | Cellar door reveal framing; the approved cloche lift | Theme foley tail |
| Gut | The Landlord carrying the rent (`carrierMain` with the Heavy Purse) | M2 | Where the Landlord exists | Purse-badge reveal on the Landlord's plate | Coin-weight foley tail on the drop |
| Heart | The Bedside Stand Viaticum (`viaticum`, unranked) | M2 | Every F5 | Three-drawer open act (4.10) | Theme foley tail |
| Inverted Cathedral | TBD with roster | M4 | — | — | — |

**Inset forms (lead's call, after the check; CT15).** Every inset is waist-high on its wall-set bracket, at most half a wall square wide, no floor-level opening, opened state a shallow empty back.

| Theme | Inset form | Must not read as |
|---|---|---|
| Gaol | A whitewashed wall tin with a stencilled tag | The Gaol Latch hatch |
| Kennel | A strapped wicker wall creel on a peg rail | A sapling hang (Reserved) |
| Wake | A small grave-goods niche box with a horizontal lid | The Wake Latch hatch; a coffin |
| Choir | An oak alms niche with iron bands, on a dry wall | The Choir Latch hatch; the Offertory Plate |
| Silk | An ink-lacquered wall pigeonhole box | The Silk book-lift Latch; a book or page |
| Carnival | A gilt-tagged hamper hung on a wall bracket | The Drape (tent flap) |
| Supper | An oxblood canteen in a wall niche | The Supper dumbwaiter Latch; the serving hatch |
| Gut | A sphincter cache below half the Door aperture, pale dry rim, fixed static (no peristaltic idle within 1 square) | The Gut sphincter Door; the Lip Catch |
| Heart | A biscuit tin on a small wall shelf | The Heart drawer-slide Latch |

### 5.3 Each theme

**The Gaol of Small Hours (F1; M2 first blockout set).** Everything of value was confiscated on admission.
- **Plain: the confiscation tin.** A stencilled tin deed-box with a paper tag, flaking whitewash on iron. No dial or clock face. Distinct from lidded pails (round, bail handle) and filing crates.
- **Locks.**
  - Cell padlocks; one Crier after room 2 (SR3).
  - The Hour lock (M3, Portal). Its cell cache is read as **SR2, not SR3**: a question to the TL and LE owners.
  - The **Confession Box** catch host, the one container with a speaking grille.
- **Keybearer.** The Turnkey with the Jailer's keyring. It is also the classic Steal target.
- **Elite cache.** A tin in the Keyring Office.
- **Hairline.** Whitewash newer than the wall.
- **Activity.** A dice pot.
- **Proprietors.** The turnkey, the warden, the confiscation clerk, the chaplain. The noun is always the skin's ("The warden's confiscation tin"), never a lock-kind noun on an unlocked skin (4.5; corrected after the check from "the warden (strongbox)" and "the chaplain (alms box)").
- **Windfall.** None **(agreed, Q4)**. If F1 ever opens (Later, a new client decision): a rat dragging the standard sackcloth bundle to an arched Rathole beside a drain, never a prisoner and never the grate itself.
- **Foley.** A dry tin scrape, audible over the 5/4 ticking bed.

**The Kennel Wood (F1; M1 subset).** A dead lord's hunting effects and the poachers' takings. Hounds hold nothing.
- **Plain: the poacher's creel** (M1). Strapped wicker, never hung on a sapling (Reserved).
- **Pen box** (M1).
- **The Huntsman's lodge chest** (Keyed; Keybearer the Huntsman).
- **Elite cache.** A creel in the Trophy Room.
- **Signature cache.** Old Ragged's moss-wood cache, "Guards: Bellwarden's Mail".
- **LP-21.** The Houndmaster's horn is never the notable, because horns are reserved for joiners.
- **Windfall.** None. The fox (Later) needs a role-ID pass against the Whelp and Gaunt Hound.

**The Endless Wake (F1, conditional).** Grave goods and bequests.
- **Plain: the grave-goods casket** (LP-18). Knee-high, lid horizontal. **Full coffins never hold loot**, which removes the three-way coffin read (cover, lurker tell, loot).
- **The Will-Box** (Keyed; the Undertaker).
- **The Viewing Door** (Tattle).
- **Elite cache.** In the Parlour.
- **Signature.** The Guest of Honour guards the Lying-in-State Leathers.
- **Hairline.** The portrait whose eyes are open (approved).
- **Keybearer.** No watch on the key prop (stopped clocks).
- **Windfall.** None.

**The Drowned Choir (F2–3).** The faithful pay to keep the hymn singing. Loot is on dry ground only.
- **Plain: the alms chest.** Oak, iron bands, a domed lid, no coin slot, never dripping.
- **Barb** (damage only).
- **The Vestry door** (Keyed; the Deacon).
- **Hymn Riddle** (M3).
- **Wager: the Loft of Responses.** Three choir-screen cages.
- **Strongroom: the Sacristy.**
- **Hairline.** A dry patch above the water lines.
- **Purse.** The Deacon first.
- **Truant (M3 pilot; agreed, Q4): the Alms Crab.** **Re-specified after the check (lead's call, after the check):** a hermit crab whose shell is a barnacled bell-clapper, never a cup, box, plate or other vessel, with eye stalks and mouthparts for a face. It drags the standard sackcloth bundle behind it, larger than its body. Its eye stalks and legs stay out in every pose, Dormant included; its Dormant breathing idle plays on the eye stalks only. It scuttles the **dry causeway** to a Rathole at the foot of a dry plinth, well away from the water-line band where the Hairline dressing sits. Its route never enters water or a Grate or Sluice area, and it is never in an Organ Swells, Hymn or clapper room. The proposal's alms cup and wall crack were dropped: the cup sat in the alms chest's family and read as a container moving at idle (the boxed-lurker tell), and a crack at the water line read as a decoy Hairline. The role-ID gate adds Crab-against-alms-chest screenshots. (The name stays a placeholder for the naming pass.)
- **Ebb (M4; agreed, Q4).** The Offertory Plate sinking ring by ring into the flagstones. **Not on water**: the water-square exception is dropped (CK13).

**The Silk Archive (F2–3).** Borrowers are shelved, and so are their belongings.
- **Plain: the archive chest.** Ink lacquer, never book-shaped, no paper skin.
- **Catch host.** The card-catalogue tray.
- **Locks.** Web seal (fire opens it), Crier, the Closed-Stack cage (Keyed; Head Archivist), Index Riddle (M3).
- **Bait** (LP-16). The bait cache is an **ink-lacquered document case**, not EP's reading tray (RC2 tray ban; second Silk tray).
- **Boxed lurker.** The catalogue drawer (M3).
- **Prizes.** Restricted Section (vellum-shuttered cages), Rare Books Room.
- **Hairline.** Worn gilt at hand height.
- **Three small boxes** (tray, drawer, chest) join TL's screenshot gate.
- **Truant (M4): the Silverfish** hauling the standard sackcloth bundle to an arched Rathole at the foot of a stack plinth (never a crack; 4.7.3). It needs a role-ID pass against the Inkling minion and the Margin-Crawler. **Never a page** (the lurker's rest tell; agreed, Q4).

**The Carnival of the Last Evening (F2–3).** Prizes nobody won and the night's takings.
- **Plain: the prize hamper.** Wicker, gilt tag, bruise-purple ribbon.
- **LP-17.** The Jack-in-the-Box catch sits on a prize hamper, not a costume trunk (a Furnishing).
- **Locks.** Crier; the High-Striker cage (threshold glyph); the Ring key (Keyed; the Ringmaster); the Ticket lock (M3; coin buys access, never loot).
- **Wager: the Last Act.** Glass domes, never barred, never padlocked.
- **Strongroom: the Box Office.**
- **Activity: the busker's hat** (never a bucket).
- **Hairline.** A poster pasted over a seam.
- **Truant (M3 pilot; agreed, Q4): the Conjuror's Rabbit.** A white rabbit with a ticket punched through one ear, dragging the standard unstriped sackcloth bundle (stripes are the Carnival's faction motif) to a Rathole at the foot of a wall, rimmed with sawdust. Never a painted flat, which is the secret cache's panel (CK22), and never cloth: the proposal's tent-flap gap was the Carnival's Drape Portal skin (corrected after the check). Its route avoids oil, fire and Keg footprints. Never in a Pit Breathes, Turning Ring or Wheel of Knives room. It answers the client's Mario-rabbit reference.
- **Ebb (M4; agreed, Q4).** The Prize Balloon on a sandbag weight, lifting a notch per tick. A hit pops it and the bundle drops (never destroyed).

**The Long Supper (F4; F3–4 from M3).** The household plate laid by.
- **Plain: the silver canteen.** Oxblood lacquer. **No frost** (frost on a lock is the Iced padlock's alone).
- **LP-19.** The Taster's Plate tell is one tarnished fork in a canteen's velvet.
- **Locks.** Barb (Poisoned), Iced padlock, Tattle, the cellar door (Keyed; the Sommelier), Place-Setting Riddle (M3).
- **Wager: the Top Table** (approved cloches).
- **Strongroom: the Plate Room** (F3 only).
- **Activity: the uncovered feast plate.**
- **Truant (M3 pilot; agreed, Q4): the Crumb Mouse.** Dressed by its whiskers and a dusting of crumbs, dragging the standard sackcloth bundle, to **a mousehole in the wainscot** in the arched Rathole form (4.7.3). The proposal's napkin linen and place-card string were dropped after the check: they are the Supper's faction motif. Never in a Courses or Open Window room. Its route is never adjacent to Hot pieces.
- **Contested skins (agreed, Q4).** The client's dessert trolley (the serving trolley is a rules piece). **The cloche** (the Top Table Wager's lift). **The dumbwaiter and serving hatch** (the Dumbwaiter Drop trap and the Courses Stir emitter) (CK2).

**The Gut of the World (F4).** The tenants' savings, and what the gut swallowed.
- **Plain: sphincter caches** (wall) **with a pale dry rim** (LP-20; lips and drip lines are the Lip Catch's). **Savings tins.** No chest-shaped loot, because rent chests are Furnishings.
- **Keyed.** The Tenant's strongbox. **Keybearer cache.** The Landlord's ledger safe (low, iron).
- **Bait** (LP-16). A tenant's sewing basket, not a third strongbox.
- **Boxed lurker.** The swallowed strongbox (M3).
- **Wager: the Stomach** (swallowed birdcages).
- **Activity: the rent pot.**
- **Purse: the Landlord** ("Carries: n coin" is the rent).
- **Truant (M4): the Rent Snail** (hop 1, so its C comes from the catchability proof).
- **Ebb (M4; agreed, Q4).** The swallowed bauble absorbed ring by ring into a lining fold. Never in a Brim, Peristalsis or Acid Tide room.

**The Dreamer's Heart (F5).** What a sleeper keeps close. All F5 rules apply.
- **Plain: the biscuit tin** (not the toy chest, hatbox or blocks; no bedsheet cloth).
- **Diary box** (Plain diary lock). **The Nursery door** (Keyed plus Tattle).
- **Viaticum host: the Bedside Stand** (LP-14; **agreed, Q5**). A unit-scale nightstand with three short drawers on legs, one square, below unit shoulder height (4.10), which opens the width-3 offer UI. It is the second named exception to IR 4.18, because an ordinary tin would lie about what clicking it does (RC1). Its silhouette hosts nothing else (4.10).
- **Wager: the Night Nursery** (birdcages, never mobiles).
- **Activity: the knitting basket.**
- **Windfall.** None **(agreed, Q4)**. The runaway toy is contested: toys are pieces, and a wind-up toy is mindless (keyhole).

**The Inverted Cathedral (M4; stub).**
- Up-chained chests, the Rood Screen gate, Rose Window lanterns.
- The Rising Votive Ebb, never near a rose-window Void.
- Blocked by V14 until its roster exists.

---

## 6. Readability, art and audio (lead's call; changes LP-7, LP-12, LP-23, LP-24, LP-26)

- **RC5 registry (LP-7, LP-12, LP-26).** Each new mark has a positive shape, a four-mark slot (base, motion, danger, cover) and an RC6 tier **(lead's call, after the check)**:

  | Mark | Shape | Slot | RC6 tier | Notes |
  |---|---|---|---|---|
  | Windfall mark | A knotted-sack glyph | base (on the plate and bundle) | unit plate | Distinct from the bait, lurker, signature and lock marks |
  | Scurry trail | Small paired dots, one pair per route square | base | floor-decal value while Dormant; never above floor tier | Never a dash, a hatch or a three-stroke tally |
  | Next-hop outline | A solid pawprint outline on the 1–2 landing squares | motion | telegraph tier, **only while awake** | Never the due-square hatch, the Spoor tally, the trap-link dash or EP's fall-back chevron |
  | Rathole | The arched opening (4.7.3) plus a front-square half-rim floor decal | base | floor decal | Drawn whatever the wall's cutaway state |
  | Count digit | The digit plus a clock-pip on the floor ring; **hollow ring while Dormant, solid while awake** | danger | telegraph tier while awake | State never depends on hue (HZ: a digit plus a clock-pip for countdowns) |
  | "Held" badge | A small bar across the clock-pip, with its reason on hover | danger | telegraph | Surprised, Alone, slip used |
  | Wall-set mark | A solid base bracket on the wall face, projected as a solid bracket onto the open square | base | floor decal | Never a dashed rim |
  | Twin-lid mark | As CT8 | base | container | — |

  Every row joins the greyscale pass and the deuteranopia, protanopia and tritanopia passes with every state in frame (Dormant, awake, held, nabbed).
- **RC1.**
  - The Truant is a unit: a Dormant breathing idle, motion only in its ≤ 1.0 s acts.
  - **Closed act table (lead's call, after the check).** Each act is ≤ 1.0 s and reuses approved states. Rule: **a loss always takes the bundle and plays the got-away cue; a gain always leaves a pile.**

    | Act | When | What shows |
    |---|---|---|
    | dormant | idle until it wakes | breathing idle (the Crab: eye stalks only) |
    | stand | round-1 Dream step | rises, bundle shouldered |
    | hop | each hop | 1–2 squares along the trail |
    | held | a held step or a block | flinch toward the blocker or the cause; the "held" badge |
    | wait-at-hole | at the Rathole front square while C > 0 (safety case) | alert loop, no travel |
    | leave-by-Rathole | got away through the Rathole | ducks through the arch, bundle crossing the rim |
    | fade-with-bundle | got away in place, or the party fled | animal and bundle dim together; no pile |
    | nabbed | a nab | the animal settles away; the bundle stays as a dropped pile with the ENM drop glint |
    | stolen | a successful Steal | as nabbed, but only the coin pile stays |
  - **Cower only while awake (lead's call, after the check).** The cower plays only while awake or held, when a party unit is adjacent. A Dormant Truant keeps the Dormant posture and glyph whatever is adjacent, so a thief's approach never reads as "it noticed you"; a Steal resolves straight from Dormant into the stolen act or the stand act.
  - Nothing remains after it resolves, except a gain's pile.
  - The Ebb's fade act joins the closed walkable-square motion list only with the M4 change (LP-34).
- **RC7, a reserved cue family** (LP-7; **a request to the RS owner**, as LE 6.15 requires, CK12). **A Windfall timbre row (lead's call, after the check):** a plucked, dry string family that no theme bed uses, kept out of chimes, bells, ticks and the Lullaby Glyph's wind-up click (the HZ and TL timbre tables; the Choir bed's muffled low chimes). One cue per state:
  - **wake:** a short rising pluck trill when it wakes;
  - **stand-up:** one soft scurry patter (C to 2);
  - **hop:** a 2-step patter, one step per square moved, pitched up a step when the new C is 1. It plays after the theme-neutral Dream tick, not on top of it. (The proposal's "escalating at C = 2 and C = 1" could not be heard: with C = 3 only one hop happens before the escape.)
  - **held:** a muted, damped pluck;
  - **nabbed:** a cloth thump plus one bright pluck, distinct from the coin pickup and the monster-removal cue;
  - **got-away:** a descending pluck run into a hollow wall "pop".
  - These six cues join the audio blind-ID gate at 85% in the Choir, Carnival and Supper mixes.
  - Rationale: these are clock telegraphs, as with Stirs, not reward stings (told plainly).
  - Until approved: "pending RS owner".
  - No barks, no laughter (RC8).
- **Containers.** The global open cue plus an optional theme foley tail (CT12).
- **Art budget (lead's call, after the check: counts added).** Counts are assets *(illustr.)*; a pose or open act counts as one.

  | Wave | Per unit | Units | Total |
  |---|---|---|---|
  | M1 | Kennel creel (2 variants × closed/open = 4), pen box (2), signature cache (2) | — | about 8 |
  | M2 | Per theme: Plain family 2 variants × closed/open (4), inset variant closed/open (2), named containers (about 3), activity container (1), signature-moment asset (1), foley tail (1): about 12 | 8 core themes | about 96, plus the Bedside Stand (closed, open act, opened = 3) and the Wake (about 12, last in M2, conditional) |
  | M3 | Per Truant skin: 9 acts, bundle, Rathole dressing, 6 cues: about 17 | 3 pilot skins | about 51, plus Wager cages and Twin Coffers |
  | M4 | Silverfish and Rent Snail (about 17 each), Ebb skins, the Cathedral family | — | about 60 |

  - **The art team's surplus goes to M2 first**, because those assets are seen on every floor. The conditional Wake family is last in M2.
  - **Cut order if M2 is short:** inset kits first (insets fall back to free-standing containers), then the second Plain variant (CT2 then uses one variant per family and a display-hash mirror), never the signature-moment assets.

---

## 7. New mechanics considered

| Mechanic | Verdict | Wave | Change |
|---|---|---|---|
| Windfall Truant (in combat) | **Adopted** (pilot; agreed, Q1) | M3 | LP-1 to LP-7 |
| Auto-close quiet summaries | **Adopted**: the real click saving | M2 | LP-8 |
| Numbered click table | **Adopted** | M2 | LP-8 |
| Inset (wall) containers | **Adopted** (A plus B merged) | M2 | LP-12 |
| Proprietor tags | **Adopted** (from B, renamed) | M2 | LP-13 |
| Treasure strip by source | **Adopted** | M2 | LP-28 |
| Uncorked glint state | **Adopted** if the ENM owner agrees, else M3 | M2 | LP-27 |
| Glean (one-click quiet room) | **Optional convenience** | M2 | LP-9 |
| "Got away" run-record line | **Adopted** | M3 | LP-29 |
| Offer pacing ≤ 2 offer-form field sources per floor | **Adopted** | M3 | LP-30 |
| Ebb (forking only); exploration Truant | Held (agreed, Q3) | M4 | LP-34 (M4) |
| Width-2 offer on nab | Held lever; a report-only M3 simulator arm (after the check); adoption returns to the client | M4 | — |
| Blocked Hairline (secret behind a light Furnishing, cleared by Shunt) | Held M3 candidate if the clock allows | M3? | — |
| Chained Key (key on a wall hook, Unhook by Stealth) | Held M3 candidate | M3? | — |
| Crannies; Tip-offs | Later (F3–F4 starvation; FOMO for low-Perception solo heroes). Tip-offs' objections answered by segment 10's class-blind, notables-only Lodestring (exploration and information design 4.6.5; XI-23, M3); Tip-offs stay Later | Later | — |
| Trove | Later | Later | — |
| Tattler, Shepherded escort, Herald halt | Later | Later | — |
| Hawker; Lost Property | Segment 8 / camp hooks | hook | — |
| Tribute (Cowed groups pay salvage) | Not adopted: reopens agreed ENM 15.7 Q3. Only if a `CowRate` report shows Cow is under-used | Later | — |
| Grapnel; Doorstop | Later | Later | — |
| F1 coin-only teaching Ebb | **Rejected** (agreed, Q4 option B) | — | — |
| Furnishing stashes; a Search verb; loot in crates | **Rejected** (settled ROOM; TL anti-goal) | — | — |
| Dropped loot tumbling into Voids; patrols picking up coin | **Rejected** (ENM 15.5, 15.6, HZ 13) | — | — |

**Adopted in full.**

- **Uncorked** (LP-27). A carried consumable notable has a data use threshold, such as the HP fraction at which a self-heal fires. While the carrier is at or below it, its existing glint shows an "uncorked" state. This is a state read, not a new mark and not an intent read. Hover: "Uncorked: may use its <tier word> <noun>". If used in sight, LE 8.2 already gives n − 1. **Its look (lead's call, after the check):** a shape sub-state of the glint (the glint glyph with an open stopper notch), never a hue change. It joins the pattern registry, the greyscale and colour-blind passes, and counts in ENM's glints-per-screen gate.
- **Treasure strip by source** (LP-28). Counts only. It gives routing information (is Sound the wall worth 6 ticks?) without locations.
- **"Got away" line** (LP-29). Profile-side, at run end only, listing the tier word of each Windfall that got away, never its item family or identity (matching 4.5; corrected after the check). Kept apart from IR's "What the dream kept".
- **Offer pacing** (LP-30). At most 2 offer-form field sources per floor among Wager, Twin Coffer and Viaticum, plus the width-2 Windfall offer if it ever ships. Report `OffersPerFloor`. **Draw-free trim (lead's call, after the check),** checked by V15g: the Viaticum is never dropped; the Twin Coffer is dropped first (it reverts to a plain container keeping its Yours identity; Wayward becomes a dreamed option); then the Wager (LE 6.9 item 7's revert). This runs in LootBind step 1, with the other item-7 reverts.
- **Auto-close, click table, inset containers, Proprietor tags, Glean:** 4.5–4.6.

---

## 8. Numbers with fixes (toy, not simulator; all illustr.)

The scripts and outputs are listed in Appendix B.

**Truant, party 4, no pre-fight Steal** (nab, Δdowns per fight, Δrounds):

| Config | Greedy | Balanced | Smart | Ignore (nabbed by fight end) |
|---|---|---|---|---|
| A (free grab, C 3–5) | 1.00 / +0.105 / +0.18 | 1.00 / +0.115 / +0.18 | 1.00 / +0.113 / +0.15 | 0.84 |
| C (C = 3, hidden home) | 1.00 / +0.161 / +0.45 | 1.00 / +0.230 / +0.59 | 0.82 / +0.010 / +0.12 | 0.55 |
| FD (C 4–5, action) | 1.00 / +0.025 / +0.29 | 1.00 / +0.037 / +0.35 | 0.95 / −0.005 / +0.05 | 0.94 |
| **REC (C = 3, action nab, visible)** | 1.00 / +0.022 / +0.29 | 1.00 / +0.032 / +0.35 | 0.84 / +0.012 / +0.13 | 0.58 |

**Mixed population** (30% greedy, 40% balanced, 20% smart, 10% ignore), pre-fight Steal on:

| Config | Comp | Nab | Δdowns | Δrounds |
|---|---|---|---|---|
| REC | trio / quad | 0.97 | **+0.3 to +0.8 pp** | +0.14 |
| REC | duo | 0.96 | +7.0 pp | +0.42 |
| A, C | solo | 0.87–0.99 | +1.9 to +16.1 pp | up to +0.39 |
| FD | solo | 0.88–0.98 | +1.5 to +3.5 pp | up to +0.44 |

**Toy failures and fixes:**

| # | Failure | Fix (where) |
|---|---|---|
| PT1 | Free grab: +10 to +12 pp downs | Action nab (4.7.5) |
| PT2 | Fight-end nab with C ≥ 4 is a freebie (84–94% when ignored) | C = 3 (4.7.4; agreed, Q1) |
| PT3 | Hidden-from-entry: +2.7 pp against +0.3 | Visible home (4.7.3) |
| PT4 | Solo and duo fail greed gates | Party ≥ 3 pilot; solo arm (agreed, Q2) |
| PT5 | The 55–80% nab band is unreachable at fair cost | 80–95%; gate on cost (4.7.9) |
| PT6 | Shade Steal 76% at Dormant −5 | Fixed passive 20 −5 = 15; the room's one Steal try (4.7.8) |
| PT7 | K2 lump fails `StealCoinShare` (45%) | Weight-3 share; Steal takes the card only |
| PT8 | F3–F4 card starvation | Card from the host's carried allotment (4.7.6; **unsimulated**) |
| PT9 | Container cap breached on 97–100% of floors | Cut the count; cap 12 (4.2) |
| PT10 | Glean saves about 0.25, not 3–5 | Auto-close (4.6) |
| PT11 | Ebb fork rate 12–35%; Tight shape is an attention tax; auto-walk-off clicks | Ebb to M4, forking only, click-to-move (agreed, Q3) |
| PT12 | 19–34% of planned Windfalls dropped by exclusions | In-floor move, then carry (4.7.10) |
| PT13 | Conditional draws break LootDoorInvariant on 20% of floor–door pairs | Keyed grandchild, every floor |
| PT14 | The Cow payoff is nearly dead | Not advertised |
| PT15 | Trove widths keyed by party size | Trove to Later |

**Other toy results:**
- Realistic escaped VP is about 0.10–0.15 per run.
- Expected Windfalls per run are about 1.2, or about 1.4 with carry, **at party 4 on every floor and with every F2–F4 theme skinned**. Told plainly after the check: neither holds in M3 (0.1, 4.7.2); the lead's re-estimate is about 0.6–0.9 per run at the raised presence, for an always-hiring party, the figure the client accepted for the M3 pilot (agreed, Q6).
- Loot clicks are 19–21 of the 40-click budget **before** walker confirmation, which is the largest unknown on the floor clock (told plainly).
- Full-clear rate (k = n) at party 4 is about 0.71.

**Not modelled:**
- deliberate body-blocking, now bounded by the slip-once rule;
- monster areas;
- companion AI;
- surprise;
- deaths as distinct from downs;
- the true LE shuffles;
- the carried-card source;
- real walker values;
- the per-theme host mix;
- the party size by floor under the CMP 7.1 ladder, and hire-declining rates (added after the check: the toy's party 4 on every floor is why its frequency was too high);
- themes without a Truant skin.

---

## 9. Data, validator V15 and simulator (changes LP-31, LP-32)

### 9.1 Records and keys (`loot.sockets.json`, `loot.windfall.json`; all illustr.)

```
LootSocketRecord { socketId, kind, rank SR0..SR5|Unranked, roomId, roleTag, mainPathIndex?,
  square?, face?, openSquare?, footprint free|inset, containerKindId, skinId, proprietorId,
  channel, coinWeight, cardEligible, trapsBound,
  flags{he3,he4,he7,noCheck,dueHost,viaticum,windfallHost,leanRoute},
  spoilsEntryId?, lockId?, heldWaxKey, onWindfall? }
RoleTableRow { role, floor, min, max, ranks[], coinWeight, never[] }   // loot.roleTable
ProprietorRecord { id, theme, nameKey, possessiveKey, roomRoles[], rankFamilies[], hosts[],
  skins{host×lock→skinId}, anchor?, maxUses }
WindfallKindRecord { id, theme, name, faced:true, hop, wakeDelay 1, cDefault 3, cSurprised 4,
  passive 20, ratholeDressing,
  poses{dormant,stand,hop,held,waitAtHole,leaveByRathole,fadeWithBundle,nabbed,stolen},
  cues{wake,standUp,hop,held,nabbed,gotAway}, ruleCard, wave }
WindfallInstance { id, kindId, skinId, socketId, originSocketId, hostRoomIdPlanned,
  hostRoomIdFinal, homeSquare, rathole, route SquareRef[], routeLength, cGen, c0 3,
  heldStepsAtStart, cardOrigin (hostCarried|sr1Container), spoilsEntryId, coin }
WindfallState { c, state (dormant|awake|held|nabbed|stolen|gotAway), square, slipUsed,
  heldSteps }
HostGroupState.stealTryUsed : bool   // either Steal path sets it; both check it
RunState.windfallCarry : bool   // camp-exit atomic write; cleared when a Windfall survives
                                // LootBind, and at the end of F4 generation
ItemInstance.location += windfall(unitId)   // ITEMS 18.1, via LP-2
Config: loot.containerCap[f] (8/12/12/12/10), loot.roleTable, loot.detourMax 2,
  loot.summaryAutoCloseMs 1500, loot.summaryAutoCloseEnabled, loot.gleanEnabled,
  loot.windfallPermille[f] (0/500/1000/1000/0), loot.windfallParties {3,4},
  loot.windfallCSolo 4, loot.windfallCSoloSurprised 5,
  loot.windfallSkins[theme][wave]
Events: LootBindRemap{reason}, LootBindUnplaceable{socketId}, WindfallMoved{from,to},
  WindfallDropped, WindfallWoke{trigger}, WindfallHopped{from,to,movedSquares,blocked},
  WindfallHeld{reason}, WindfallNabbed{byUnit?,method,round}, WindfallStolen{unit},
  WindfallGotAway{cause (rathole|faded|partyFled), cardRef, tier, vp, coin},
  ItemDestroyed{gotAway}, ContainerCapTrim{merged[]}, ProprietorFallback{socketId},
  SummaryAutoClosed{containerIds[], coin, sundries}, GleanOpened{ids[]}
```

**Client keys, not tuning (new lead's call):** `cDefault` 3 and `cSurprised` 4 (Q1); `loot.windfallParties` = {3, 4}, gaining 1 only when the solo arm passes, and `loot.windfallCSolo` 4 (Q2); `loot.windfallCSoloSurprised` 5 is a lead's call after the check (the Stir precedent), not a client key; the Windfall wave M3 and the Ebb wave M4 (Q3); the pilot skin list per theme (Q4); the Bedside Stand as the Viaticum host (Q5). The draft's `loot.windfallMinParty 3` is replaced by the `windfallParties` set, so duo can stay held while solo opens. Every other number is tuning.

### 9.2 V15 `LootSources` (change LP-31; beside V13 and V14 in RS 11.2 and LE 10.2)

Split like V13 and run at the shared assertion point (AP2) after V13g and `V16g.SlotLegal`:

- **Phase model and assertion point (SV-9, SV-20; M1 model, M2 complete; seeds and validation design 5.1, 5.2, 5.4).** Checks carry qualified ids (`V15.CheckName`) with one phase each; `s` and `t` parts fail the build; V15g runs at AP2 after V13g and `V16g.SlotLegal`, and acts by its failure class: `repair` (7c's LP fallbacks: square fallbacks, remaps, Windfall move or drop, `ProprietorFallback`, offer trim; LR1), then `waivable(LootBindDegrade)` (LR4, 4.3); on an HE or Due socket, `defect` (LR5). Waves: M1 rows (below); M2; M3 Windfall rows.
- **Pending request (segments 11–12; SV-20, M1):** the 7c owner itemises V15's M1 rows before the M1 goldens. Not a rule until the 7c owner rules; fallback: segments 11–12 itemise the M1 subset from this section (the rows touching F1 binding), and the 7c owner confirms it on the M1 checklist (seeds and validation design 14.2).

- **V15s (static).**
  - Every theme has a skin for every container kind it hosts.
  - `ContainerFamilyDistinct` (with V7).
  - `ContainerArtBlind`.
  - `ProprietorNounBan`, `ProprietorNounMatchesSkin` and lurker parity; every theme has a `generic` Proprietor record.
  - `TruantHasFace`; Truants only in F2–F4 themes; no Truant, bundle or Rathole uses its theme's faction motif or minion palette.
  - Rathole never a trap, Stir or Wager prop, a Portal skin or a Hairline dressing, and always the arched cross-theme form (`WindfallSkinLegal`).
  - Insets never share a silhouette with the theme's Latch, Door or Hairline skins (CT15, in `ContainerFamilyDistinct`).
  - `BedsideStandOnly` (new lead's call, 4.10).
  - Every `onHit` ability lists `ignoresWindfall`.
  - Cue ids are in RC7 once the RS owner approves.
  - **HE/Due socket-binding proof** (LR0): `LootBindUnplaceable` on an HE or Due socket is impossible by construction, so V15g's HE/Due case is a `defect`, held at 0 (SV-10). **Pending request (segments 11–12; SV-10, SV-20; M1 assert, M2 proof):** the 7c owner supplies the proof. Not a rule until the 7c owner rules; fallback: M1 ships V15g's assert as `defect` with the soak at 0, and M2 sign-off stays blocked until the proof exists (told plainly: no M2 fallback; seeds and validation design 14.2).
- **V15g (generation).**
  - Every socket has one kind and rank, or is unranked, and is bound to a legal square (4.3); traps-bound sockets kept their square; no `LootBindUnplaceable`.
  - The role table (`loot.roleTable`) is met; ≤ 1 card-bearing container per room (`cardEligible`); ≤ 1 coin-only container per room after merges.
  - Offer pacing: ≤ 2 offer-form field sources per floor after the trim (LP-30).
  - `MainPathDetour`; `SideRoomPreview`.
  - Cap and `EmptyDeadEndRate` 0.
  - Due on the first HE-7. Viaticum room. Signature cache rules. HE Plain.
  - Caps: 3 notables per encounter including the Windfall; carried caps; ≥ 1 Steal target **after step 8**; F5 locks and fillers.
  - Filters (a)–(g) asserted **after step 8**.
  - **Windfall:**
    - F2–F4, ≤ 1, not trimmed, party size in `windfallParties`, and the floor's theme has a non-empty pilot skin list for the wave;
    - host exclusions, including the Annex, a boxed lurker and the first fight, checked on the final host;
    - the final host encounter's 3 notables and carried caps with the Windfall counted;
    - `V10.OneClockPerRoom` (V10's check, run at V15g; SV-20), run after segment 8;
    - `WindfallRouteSafe` (route legality; route length exactly L = 4, stored as `routeLength`);
    - `WindfallCatchable` (4.7.8's algorithm for each class at floor level; home ≥ 4 path squares from, and not within 1 move of, the entry Portal);
    - `WindfallOffGuarantees` (the Due, DH, Viaticum, keys, secrets and lean band unchanged with the Windfall forced to escape);
    - `WindfallBudgetInside` (deck VP and Ledger A unchanged).
  - `GleanNoLeak`.
  - The `windfall` grandchild's draw count (4) is equal on every floor, door, party size and theme (kept as a citation; V19g's DrawLedger calls it at every stage end; seeds and validation design 5.2).
- **V15t (tests).**
  - LootDoorInvariant golden seeds, with Windfalls present, moved, dropped and carried (goldens and the release seed set, which covers Windfall present and absent: seeds and validation design 4.11; SV-18).
  - Reload after camp exit replays the carry. Carry cases: set by a drop; kept across a party- or skin-ineligible floor; not stacked when presence would pass anyway; cleared when a Windfall survives LootBind; cleared at the end of F4 generation.
  - Resume mid-fight replays hops including `slipUsed` and held steps.
  - Dormant is untouched: a fight-opening area over its square does not nab it unless the square is still included after the wake.
  - Wake on Engaged: a host group pulled into a corridor wakes it.
  - Combat end inside a Dream step: the last hostile Routed at 2b on the step where C would reach 0 gives a nab.
  - Partial block: the first square free, the second occupied, gives one square moved, slip used, C held.
  - Steal: one try per host group across both paths; advantage uses `die` 0 and 1; a failed Steal below the allowed party size leaves it Dormant.
  - Glean order and stops.

### 9.3 Reports (change LP-32; M2, with M3 Windfall rows; targets illustr.)

| Report | Target |
|---|---|
| `LootClicksPerFloor` (by source) | Inside the 7c line |
| `QuietSummaryShare` | — (from `SummaryAutoClosed`) |
| `GleanClickSaving` | — |
| `LootDetourSquares` | SR0 median ≤ 2 |
| `ContainersPerFloor` | ≤ Ledger A + 3 |
| `ContainerCapTrim` | — |
| `ContainersOpened` | Paranoia alarm at −10% (kept; one row, 7c owns it, the others read; SV-24) |
| `ContainerMisreadRate` | — |
| `LootTakenShare` | — |
| `SecretFoundRate{lodestring}` (one keyed family with XI's `SecretFindRate`; SV-24) | — |
| `TallyChaseSeconds` | ≤ 30 s (kept; LE's row, 7c reads it; SV-24) |
| `OffersPerFloor` | ≤ 2 |
| `ProprietorFallbackRate` | ≤ 10% (fallbacks ÷ tagged sockets) |
| `WindfallNabRate` | 80–95% |
| `WindfallNabByClass` | No class < 0.6× the best |
| `WindfallGreedDowns` / `WindfallChaseDeathShare` | ≤ +1 pp at party ≥ 3; solo arm ≤ +2 pp to open (agreed, Q2). Defined in 4.7.9 |
| `WindfallsPerRun` | Report, by theme mix, party schedule and RS cut (added after the check; measured against the about 0.6–0.9 per run agreed with Q6) |
| `WindfallFirstActionShare` | ≤ 40% |
| `FightEndNabShare` | Report only |
| `WindfallStealShare` | ≤ 40% Shade parties |
| `WindfallRoundsDelta` | ≤ +0.2 averaged |
| `WindfallSeconds` | Inside the 7c line |
| `WindfallEscapedVP` | ≤ 0.5 mean per run (from `WindfallGotAway.vp`) |
| `WindfallEscapedCoin` | — |
| `WindfallBlockedRounds` | — |
| `WindfallIgnoreShare` | — |
| `CompanionChaseDowns` | — |
| `SecretCardShareWithWindfall` | ≥ control |

**Report registry (SV-24, M2; seeds and validation design 6.3, 6.10, 6.12).** Each row above is one report-registry row with one result class: the Windfall gate rows SHIP (M3); `ProprietorFallbackRate` BUDGET; `ContainersOpened` ALARM; the 7c clock line (LP-10) BIND, fired before any CMP 7.4 lever; the rest REP, WATCH or FELT. `LootBindDegrade` is a BUDGET row (≤ 0.3% *(illustr.)*; 4.3).

### 9.4 Simulator arms

**Qualified names (SV-22, M2; seeds and validation design 6.2).** The 7a-only baseline is the toggle `LP.Loot7aOnly`; the policies are the `LootPolicy` axis (`LootPolicy.Greedy`, `Balanced`, `Smart`, `Ignore`, `Blocker`, `Pull`); the declining party ladder is `Party.ScheduleDeclining` (M3). **Pending request (segments 11–12; SV-22):** the 7c owner names the default `LootPolicy`. Not a rule until the 7c owner rules; fallback `LootPolicy.Balanced` (seeds and validation design 14.2).

- 7a-only baseline against 7c;
- the walker re-baseline;
- auto-close on and off; Glean on and off;
- Windfall off, on, forced-escape and forced-carry;
- greedy, balanced, smart, ignore and blocker policies;
- party sizes 1–4, with **a solo arm at C = 4** (agreed, Q2) and **a carried-card-source arm** (the first M3 arm);
- **added after the check:** the party-size-by-floor distribution under the CMP 7.1 ladder with declining rates, and the per-theme host mix (M3 pool and RS Cut 2), both in the first M3 arm; a **pull** policy (the host group drawn out of its room); report-only arms for the **highest eligible tier** card and the **width-2 offer on nab** (4.7.6).

---

## 10. Staging by wave

| Wave | 7c content |
|---|---|
| **M1** | F1 role-table subset (Kennel), LootBind square legality, Plain containers and the pen box, the Due on the first HE-7, HE-3/7, the coin floor, ≤ 1 carried notable, Old Ragged's signature cache, V15 M1 rows (itemised by the 7c owner, a pending request; 9.2) |
| **M2** | Full socket table and ranks; all LootBind rules and remaps; Keybearer and key target; secrets and Alcoves; bait-cache placement with the Mousetrap (EP 6.8.1; moved from M3 after the check); the Heavy Purse; Steal targets; the Viaticum lair-room binding and the Bedside Stand (agreed, Q5); tier marks, Proprietor tags and the Treasure strip by source; the numbered click table and auto-close; optional Glean; inset containers; the container count and cap; the 7c clock line (after the walker re-baseline); catalogue CT1–CT15, the inset forms and the signature loot moments, and LP-15 to LP-25; Uncorked (if agreed); V15s and V15g; M2 reports |
| **M3** | **Windfall Truant pilot** (Rabbit, Crumb Mouse, Alms Crab; party ≥ 3; agreed, Q1, Q2, Q4); the solo arm and, if it passes, solo at C = 4; Wager, Dare, Strongroom, boxed lurker and Twin Coffer placement; offer pacing; the "Got away" line; held candidates Blocked Hairline and Chained Key if the clock allows |
| **M4** | Ebb (forking only) and the exploration Truant (agreed, Q3); remaining Truant skins (Silverfish, Rent Snail) and Ebb skins; the width-2 offer lever; the Void-exit Ebb at Nightmare tier; the Inverted Cathedral family. **No F1 teaching Ebb** (agreed, Q4) |
| **Later** | Trove, Crannies, Tip-offs, Tattler, Shepherded, Herald halt, Lost Property, Tribute, Grapnel, Doorstop; duo Windfalls (held, Q2); F1/F5 Windfall skins only if a new client decision opens those floors |

---

## 11. What 7c owns, and hooks handed on

| Owner | What |
|---|---|
| **7c** | The source taxonomy; SocketPlan and the role table; LootBind binding and remaps; the channel landings; signposting rules, Proprietor tags and the Treasure strip wording; the click table, auto-close and Glean; the container count and cap; the 7c clock line; container art grammar and the per-theme catalogue; the Windfall (all of it); V15 and the 7c reports |
| **7a (approved)** | Budgets, decks, tiers, ledgers, coin. Hooks: step 8, filters (f1), (f2) and (g), the new rows (the `windfall` socket row and LE 6.10 item 1's draw-table row), the generation order (LE 6.10 item 4), and the latent step-6 issue for secrets and the quota (LP-1) |
| **7b (approved)** | Identities and bundles. Hooks: the Bedside Stand as IR 4.18's second named exception (LP-14); Proprietor data on `theme.loot` (LP-13) |
| **Enemies owner** | The Windfall unit row; the Later runner pulled forward; Steal on a Truant; the combat-end condition and its per-sub-step check; the Engaged wake trigger; the Uncorked state; LP-21 |
| **Hazards owner** | Sub-step 3b and the clock list (LP-4) |
| **Traps owner** | Auto-close and Glean summary wording; the Hour-lock SR reading; fewer locks as a design-time tuning lever when caps bind; LP-17 to LP-20 |
| **Items owner** | `gotAway`; the `windfall(unitId)` location and its transitions (ITEMS 18.1, 19.2); auto-close; confirm the action cost of a Plain open in combat; the "guarded" greyed reason |
| **FL owner** | Confirm the Viaticum room; inset sockets; the walker re-baseline |
| **Segment 3 (ROOM)** | Alcove set pieces |
| **Segment 5 (TL)** | Fewer locks (design-time lever) |
| **RS owner** | RC1, RC2, RC5 and RC7 entries, the Windfall timbre row and the role-ID gate rows; the Heart container notes; skin vetting |
| **EP owner** | LP-16 |
| **CMP owner** | The Truant as a suggestion and simulator-policy target (LP-33) |
| **Class owners** | `ignoresWindfall` |
| **Segments 8, 9, 10** | 4.13 |
| **Camp and hub owner** | The Lost Property hook |

---

## 12. Binding requirements and how they are met

| Requirement | How it is met |
|---|---|
| Respect 7a's fixed deck and Ledger A | The Windfall card is moved inside the deck (step 8); no container added; V15 `WindfallBudgetInside` |
| Guarantees never at risk | Filters (f) and (g); `WindfallOffGuarantees`; HE sockets Plain and unmoved |
| Loot never destroyed by method | LP-2 is a single named escape exception; kill method protected |
| One clock per room | The Windfall is the room's clock; never placed with another; `OneClockPerRoom` |
| Nothing timed in M2 | The Windfall is M3; no reopening |
| RC1–RC9 | Pre-placed units; faced animals only; reserved marks and cues (pending the RS owner); container grammar CT1–CT15; one Truant bundle and Rathole grammar; no faction motif |
| Determinism and LootDoorInvariant | Keyed grandchild with 4 draws every floor; draw-free LootBind; keyed-hash Steal; golden seeds |
| Floor clock and CMP 7.4 | Walker re-baseline; net ≤ 0 line; cut order before CMP levers |
| Permadeath fairness | No damage; visible hops; action nab; fight-end nab; party ≥ 3; gates |
| Steal and Cow settled rules | Steal takes the card only; the room's one try; Cow untouched |
| F1 teaching floor | No timed loot on F1; Proprietor tags teach |
| LE 6.14 and IR 4.18 owed hooks | 4.4 (every channel landed); signature cache 4.4, 4.10 |
| The client's question on fleeting loot | 4.7 (agreed, Q1–Q4) |
| Change discipline | LP-1 to LP-34 with exact sections; ⚑ on decided, agreed and settled rows |

---

## 13. Changes to approved documents

Applied on 2026-11-22, when the client approved this document. **This document edits no parent document itself**; the rows are applied by the owners of the sections they touch **(lead's call)**, and rows owed by other owners are listed again in 17. Every row names the exact sections it touches. Rows that touch **decided**, **agreed** or **settled** text are flagged ⚑ in the "Flag" column; **the client's approval of 2026-11-22 covers them** (with Q1–Q6 where a row names one). As in the LE, TL, EP and IR precedents, the Flag column also marks rows that only *read* agreed, decided or settled text without changing it, so that every touch of such text is visible **(new lead's call:** this adds flags to LP-7, LP-10, LP-12, LP-14, LP-15, LP-21, LP-24, LP-25 and LP-34, which the proposal's register did not flag**)**. **After the check (lead's call, after the check),** flags are added to LP-3, LP-5, LP-8, LP-9, LP-13, LP-32 and LP-33, which touched agreed or settled text without one, and LP-1 also flags LE 6.10. Told-plainly rows were told in 14.1 and confirmed by the client on 2026-11-22. Rows marked "request" are requests to other owners, recorded as pending requests; they change nothing until their owner rules, and 14.2 names each fallback. M3, M4 and Later rows are recorded now as applied text marked with their wave and take effect at that wave.

**Readings recorded inside this design (no approved text changes; each goes to its owner for confirmation):**
- Gb carriers are SR1 (LE 6.9 item 1; LE owner).
- The Viaticum sits in the F5 lair room: LE 6.11 item 5's fallback is the real rule, because nothing lies between L and F on F5 (FL and ENM owners; LE 18.2).
- The Gaol Hour-lock cell cache is SR2, not SR3 (TL and LE owners).
- "Nothing timed in M2" holds: the Windfall is M3 (RT 0.1 item 5, RT Q5, TR-27, HZ-12).
- The latent step-6 shuffle issue for secret filter (b) and the quota card is pre-existing (LE 6.9 item 2; LE owner).
- "Lair cache" in LE 6.9 item 1 and LE 6.14 means the *elite room's* SR4 cache; the main-path lair's SR0 container is `lairContainer` (told plainly).
- **(Added after the check.)** "The lair of every boss" in IR 4.13 and 4.18 means the boss's **arena**, not FL 6.5's L room, which 7c uses for `lairContainer` (IR owner and segment 9; confirmed by segment 9, boss rooms design 4.11.1; BR-27).
- **(Added after the check.)** The bait cache and its placement are M2, with the Mousetrap (EP 6.8.1, LE 6.9 item 6, LE-20); the proposal's M3 wave was an error, not a change.

| # | Document / section (current status) | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| **LP-1** | LE 6.9 item 1 (new SR2 `windfall` row; `viaticum` unranked, boss channel); LE 6.9 item 2 (new **step 8** draw-free card move; new filters (f1), (f2), (g); assertions after step 8; slot mapping fixed at step 7); LE 6.9 item 7 (remaps `WindfallMoved`, `WindfallDropped`; LootBind's numbered order, 4.3); **LE 6.10 item 1** (new draw-table row: `windfall`, 4 draws, 1 entry per floor, F1–F5) and **LE 6.10 item 4** (SocketPlan before step 3a; step 8 inside step 3a; the LootBind sub-order) (both added after the check); LE 6.14 (new row: "1 card under (f)/(g) from the host's carried allotment or a plain SR1 container; coin weight 3, never a lump"); LE 6.4 (weight 3); ENM 15.3 step 1 (card tallied in its origin channel, `onWindfall`); reads **LE 3.3 LX11** (no container added) and **MAIN 4.6 (decided)** | Windfall socket and stake (4.1, 4.7.6) | ⚑ **Decided** (MAIN 4.6, read); ⚑ **Settled** (LE 6.9, LE 6.10, LE 6.14); **Q1** | **Client (Q1, agreed 2026-11-22)**, with this document; LE owner applies; enemies owner (15.3 step 1) | M3 |
| **LP-2** | ENM 15.6 "Loot is never destroyed"; TL 6.13 "Contents are never destroyed by any result"; TL-27; HZ 13 "no item loss"; LE 8.2 n-falls list; ITEMS 18.1 (new `location` value `windfall(unitId)` and four location-transition rows), ITEMS 18.3 (new `gotAway` reason) and ITEMS 19.2 (the transition assertion) (sections corrected after the check) | Sole exception: "a Windfall's card and coin leave the floor when its shown count ends; kill method, damage, terrain, traps, locks and force still never destroy loot". New `ItemDestroyed{gotAway}`, not a dreamed option (4.7.7) | ⚑ **Settled** (ENM 15.6, TL 6.13, TL-27, HZ 13, ITEMS 18.1); **Q1** | **Client (Q1, agreed 2026-11-22)**, with this document; enemies, traps, hazards and items owners apply | M3 |
| **LP-3** | ENM 7.1 (rank table: new non-combatant Windfall row); **ENM 14.5 and ENM 21 Later list ("the runner role"; settled)**: a non-hostile runner pulled to M3, a wave change on an approved Later list; ENM 17.1 Thief row (read: stays Later); ITEMS 7.3 (the "guarded" greyed reason) | Unit class; nab rules; a nab is not a hit, damage or a kill; out-of-combat verbs (4.7.1, 4.7.5) | ⚑ **Settled** (ENM 14.5, 21 Later list; ITEMS 7.3, its list settled from ABL 15.4); **Q1, Q2** | **Client (Q1, Q2, agreed 2026-11-22)**, with this document; enemies owner applies; items owner (7.3) | M3 |
| **LP-4** | HZ 6.2.2 clock list; HZ 6.3 sub-step order (3b) and the slip precedent extended (slip once; HZ 6.3 Turn and 6.3.4 `holdSlip`); C 5.4. Reads **RT 0.1 item 5, RT Q5, TR-27 (agreed)** and HZ-12 (no reopening) | Windfall clock and hop; early arrival waits (4.7.4) | ⚑ **Settled** (HZ 6.2.2 "never waived", read and kept: the Windfall is the room's one clock); ⚑ **Agreed** (RT Q5, read) | Lead's call; HZ owner applies | M3 |
| **LP-5** | MAIN 4.3 combat-end sentence (as changed by M-3); ENM 14.6 end-of-fight rule; ENM 15.8 (a live Windfall in the fight's room is nabbed at combat end); ENM 15.6 "Party flees" bullet (it gets away) | Fight-end and flee rules; end ordering, with the combat-end check after each Dream sub-step (4.7.4) **(new lead's call:** exact sections named; the proposal said "ENM combat-end condition" and "party-flee paragraph"**)** | ⚑ **Decided** (MAIN 4.3, read: a Windfall is never a hostile, so the sentence holds as written); ⚑ **Agreed** (ENM 15.6 "drop on death, rout or yield", ENM 15.8 "one per fight") (added after the check) | Lead's call; enemies owner applies | M3 |
| **LP-6** | ENM 15.5 / S-5 (agreed); STAT 7.1 Stealth row; LE-16; LE 8.3 `StealCoinShare`; TL 6.22 (keyed hash with its `die` index); ITEMS 18.1 (the Steal transition from `windfall`) | Steal on a Dormant Truant: card only, coin spills and is guarded until the group resolves, the room's one try (`stealTryUsed`), fixed passive (4.7.8) | ⚑ **Agreed** (ENM 15.5, S-5); ⚑ **Settled** (STAT 7.1, LE-16); **Q1** | **Client (Q1, agreed 2026-11-22: "Shade Steal while Dormant")**, with this document; enemies and items owners apply | M3 |
| **LP-7** | RS 7.4 RC1 (the Truant's closed act table), RC2 (Truant grammar: faced small animal, one cross-theme sackcloth bundle, no faction motif), RC3 (read), RC5 (Windfall mark, scurry trail, next-hop outline, Rathole form and floor decal, countdown ring and held badge, with shapes, slots and tiers), RC7 (a Windfall timbre row and 6 reserved cues; LE 6.15 sentence); RS 7.4 role-ID gate rows (extended after the check) | Windfall readability (4.7.1, 4.7.3, 6) | ⚑ **Agreed** (RS 7.4, RS Q3); **Q4** | **Client (Q4, agreed 2026-11-22)** for the skins; RS owner applies the charter entries (RC7 cues a **request**) | M3 |
| LP-8 | FL 11.4 and CMP 7.4 ("N clicks by type"); ITEMS 7, 8.1; TL 6.13 summary wording | Click table; auto-close quiet summaries, with party-wide fit, pause on focus and the settings toggle (4.6) | ⚑ **Agreed** (CMP 7.4, CMP Q2); ⚑ **Settled** (ITEMS 8.1) (added after the check) | Lead's call; items, traps owners apply | M2 |
| LP-9 | TL 6.13 "Opening gives one loot summary"; ITEMS 8.1 (one screen kept) | Glean, optional, with the no-leak rule (4.6) | ⚑ **Settled** (ITEMS 8.1, TL 6.13) (added after the check) | Lead's call; traps, items owners apply | M2 |
| LP-10 | FL 6.10 (new line beside TL-38, EP-33, HZ-27); reads **CMP 7.4 (agreed, CMP Q2)** | 7c clock line net ≤ 0; walker re-baseline; cut order (4.6) | ⚑ **Agreed** (CMP 7.4, read) | Lead's call; FL owner applies | M2 / M3 |
| LP-11 | LE 0.1 click contract (read); FL 13 container sockets; FL `EmptyDeadEndRate` (Alcove set-piece default, segment 3 ask) | One card container per room (`cardEligible`), ≤ 1 coin-only, the two-phase cap and merges, the role table as data, the walk-line definition and binding, preview (4.2) | — | Lead's call; FL owner; segment 3 (request) | M2 |
| LP-12 | FL 11.1 container sockets; TL 6.13 host forms; RC5 base mark (the wall-set mark). **Reaffirms** ROOM 6.3.4 and 13 "Furnishings never hold loot" (no change) | Inset containers (4.3) | ⚑ **Settled** (ROOM, read) | Lead's call; FL, traps and RS owners apply | M2 |
| LP-13 | IR 5.1 `theme.loot` (proprietors); IR 5.3; reads IR 4.18, IR-15, RC9 | Proprietor tags; selection order; the noun from the skin; noun ban; lurker parity (4.5) | ⚑ **Agreed** (IR 5.1 `theme.loot`, IR Q4) (added after the check) | Lead's call; IR owner applies | M2 |
| **LP-14** | IR 4.18 (second named exception); IR 4.10; LE 6.15 (Viaticum line); RC5 registry. **Reading only** of **LE 6.11 item 5 (agreed, LE Q1)** for the room | Bedside Stand Viaticum host; its silhouette hosts nothing else (4.10) | ⚑ **Agreed** (LE 6.11 item 5, read; IR 4.18 "one named exception", from IR Q5); **Q5** | **Client (Q5, agreed 2026-11-22)**, with this document; IR owner applies; FL and ENM owners confirm the room | M2 |
| LP-15 | ROOM 8.1 kit silhouettes; TL 8 and V7; EP 6.8.1 (generalised); RS 7.4 role-ID gate rows; IR 4.18 (read); LE 6.15 (no world rarity read) | CT1 `ContainerFamilyDistinct` (with the rows added after the check); CT2 `ContainerArtBlind`; CT15 insets never read as Portals (5.1) | ⚑ **Agreed** (RS 7.4 gates, RS Q3) | Lead's call; RS owner applies the gate rows | M2 |
| LP-16 | EP 7.6 (S3 Mousetrap), EP 7.9 (GU4), EP 6.8.1 | Bait re-skins: Silk document case, Gut sewing basket (5.3) | — | Lead's call; EP owner applies (request) | M2 |
| LP-17 | TL 7.7 Jack-in-the-Box row | Host is a prize hamper (5.3) | — | Lead's call; TL owner applies (request) | M2 |
| LP-18 | TL 7.4 opening paragraph; ROOM 7.4 | Wake grave-goods caskets; full coffins never loot (5.3) | — | Lead's call; TL owner applies (request) | M2 cond. |
| LP-19 | TL 7.8 Taster's Plate row (reading) | Tell on a canteen's fork (5.3) | — | Lead's call; TL owner applies (request) | M2 |
| LP-20 | TL 7.9 Reserved list and Lip Catch row (reading) | Pale dry rim on Plain sphincter caches (5.3) | — | Lead's call; TL owner applies (request) | M2 |
| LP-21 | ENM 15.2 Leader row ("the banner or horn may be the item"), narrowed; reads **RS 8.4 (agreed, RS Q4 catalogue)** | Kennel horn never the notable (5.3) | ⚑ **Agreed** (RS 8.4, read) | Lead's call; enemies owner applies | M2 |
| LP-22 | FL 7.4 (Wake precedent extended), FL 9.2 Hairline (read) | Per-theme Hairline dressing (CT13) | — | Lead's call | M2 optional art |
| LP-23 | IR 6.3 (read); LE 6.15 audio; TL 8 theme-tail precedent | Global open cue plus theme foley tail (CT12) | — | Lead's call | M2 |
| LP-24 | RS 9.3; RC1 | Keepsakes never use container, bait, Windfall or Bedside Stand silhouettes (5.1) | ⚑ **Agreed** (RS 7.4 RC1, read) | Lead's call; RS owner applies | M2 |
| LP-25 | New 7c rule; reads **RS 8.6 (agreed, RS Q4 catalogue)** | Containers and Truants on plain walkable squares only (CT14, 4.3) | ⚑ **Agreed** (RS 8.6, read) | Lead's call | M2 |
| LP-26 | TL 6.13 Twin Coffer row; LE 8.4; RC5 | Twin-lid kind mark (CT8) | — | Lead's call; traps owner applies | M3 |
| LP-27 | ENM 15.2, 15.4; RC5 | Uncorked glint state (7) | — | Lead's call; enemies owner applies (request) | M2 (if agreed) |
| LP-28 | LE 8.2, 6.15 | Treasure strip by source (counts on hover) (4.5) | — | Lead's call; LE owner applies | M2 |
| LP-29 | LE 8.2 Grave Goods; IR 4.8 (read) | "Got away" run-record line; "Windfalls nabbed k of m" (7) | — | Lead's call | M3 |
| LP-30 | TL 6.15, LE 8.4, LE 6.11; LE 6.9 item 7 (the Twin Coffer and Wager reverts used by the trim) | Offer pacing ≤ 2 per floor, with a draw-free trim (7) | — | Lead's call | M3 |
| LP-31 | RS 11.2 validator table; LE 10.2 (beside V13, V14) | V15 `LootSources` (9.2) | — | Lead's call; RS owner applies | M1–M3 |
| LP-32 | LE 10.3 reports; FL 11.3; CMP 7.4 report list | Reports and arms (9.3, 9.4) | ⚑ **Agreed** (CMP 7.4, read) (added after the check) | Lead's call | M2 / M3 |
| LP-33 | CMP 8.6 (the closed `targetRule` enum and the suggestion scorer; the simulator's companion policy); reads **MAIN 4.2 (decided)** | The Truant as a legal suggestion and simulator-policy target, never an autonomous act; no "Nab it" order (4.7.8; recast after the check) | ⚑ **Decided** (MAIN 4.2, read); ⚑ **Agreed** (CMP 8.6, ability 11.3 suggestion status) | Lead's call; CMP owner (request) | M3 |
| LP-34 | TL 6.13 kinds (Ebb); RS 7.4 RC1 closed motion list (fade act); C 5.5 and HZ 6.10 (readings) | Ebb container kind (4.7.11) | ⚑ **Agreed** (RS 7.4 RC1); **Q3** | **Client (Q3, agreed 2026-11-22: held to M4)**; traps and RS owners | M4 |

Waves: LP-31 (V15 M1 rows) and the M1 subsets of 4.2 to 4.4 in M1; LP-8 to LP-28 except LP-26 in M2; LP-1 to LP-7, LP-26, LP-29, LP-30 and LP-33 in M3; LP-34 in M4.

---

## 14. Open items for the client

None. Approved by the client on 2026-11-22.

The client approved this document as a whole on 2026-11-22, including every lead's call (the lead's calls of the answered proposal, the new lead's calls in 14.1 and the lead's calls after the check in 14.1), the told-plainly items (including "Told plainly after the check" in 14.1: among them that Q1's "about 1.2–1.4 per run" is out of reach, the container-first cut order, the lowest-tier card rule and the dropped "Nab it" order), the readings recorded in 13, and its approval covers the flagged rows of 13. The five questions raised before writing were answered on 2026-11-22 **(agreed)**, each with the recommended option (A); the client also said everything else in the proposal stands as written. The check raised one further question, Q6, the Windfall frequency; the client answered it with the approval of this document, with the recommended option (A): about 0.6–0.9 Windfalls per run *(illustr.)* for the M3 pilot, with presence 1000‰ on F3–F4, the simulator owning the figure **(agreed, Q6)**. Every other lead's call after the check sits inside the six answers, keeps an approved rule, or is flagged against the text it touches. The future items in 14.2, which come back only if a report, an owner's refusal, a wave or a later design triggers them, and the naming pass remain future items.

### 14.1 This document

**The six answers (agreed, 2026-11-22), each the recommended option:**

- **Q1, fleeting loot form and budget (LP-1 to LP-7):** option A. The Windfall Truant in M3, in combat, F2–F4, about 1.2–1.4 per run, holding a real card from the fixed deck (from its host fight's carried allotment), lost visibly if it gets away, nabbed by any action or by winning the fight; pre-placed and visible at reveal (never spawned); C = 3; route outlined a turn ahead; slip once when blocked; Shade Steal while Dormant (4.7). Rejected: B, coin and sundry only; C, extra value; D, none.
- **Q2, party size (LP-3):** option A. Pilot at party ≥ 3; solo opens on its own simulator arm (C = 4) if greed downs ≤ +2 pp; duo held (4.7.2). Rejected: B, all sizes.
- **Q3, exploration form (LP-34):** option A. The Ebb (forking geometry only) and the exploration Truant in M4 (4.7.11). Rejected: B, the Ebb in M3.
- **Q4, skins (LP-7):** option A. Rabbit (Carnival), Crumb Mouse (Supper) and Alms Crab (Choir) are the Truant pilot; balloon, plate and bauble kept as M4 Ebbs; Gaol prisoner, Supper trolley, Heart toy and Silk page contested and excluded; F1 and F5 themes excluded (5). Rejected: B, an F1 coin-only teaching Ebb.
- **Q5, the Bedside Stand (LP-14):** option A. Accepted as a second named exception to "container art never implies contents" because it opens a different UI (4.10, 5.3). Rejected: B, an ordinary biscuit tin.

**New lead's calls (made while writing; confirmed by the client on 2026-11-22, with this document):**
1. **Document shape:** the proposal's section numbers 0–13 are kept so every cross-reference holds; intent, goals and anti-goals sit in 0 as 0.4 (non-goals added); the client questions become 14 (open items); the closing parts follow as 15 (risks), 16 (left to the builders), 17 (depends on) and Appendix B (sources), as in IR.
2. **Scurry trail** names the Truant's route marks; the draft's "route footprints" collides with the Stir and Keg *footprint* (50 hits) (0.1, 4.7.3, 6).
3. **`onWindfall`** names the ledger tag; the draft's `heldBy` is an existing VG data predicate (4.7.6, 9.1).
4. **The wall-set mark** names the inset container's RC5 base mark, a solid bracket, never a dashed rim, so it never reads as the harm square's "inset dashed" rim (4.3, 6).
5. **The got-away cue** replaces the draft's "got-away sting"; IR moved loot cues off "sting", which stays the boss and Stir telegraph word (6).
6. **LP-3's sections corrected:** the runner's Later entries are ENM 14.5 and ENM 21's Later list, not ENM 17.1; 17.1's Thief row is read only; the rank table is ENM 7.1 (4.7.1, 13).
7. **LP-5's sections named:** MAIN 4.3 (as changed by M-3), ENM 14.6, ENM 15.8 and ENM 15.6's "Party flees" bullet; MAIN 4.3 is flagged as read (13).
8. **Early arrival waits:** a Truant that reaches its Rathole front square while C > 0 waits there, still a target, and gets away only when C reaches 0 (4.7.4). Without this, the Surprised C = 4 and the solo C = 4 would be shorter, not longer, than shown. (After the check, extra steps are held steps and the route is exactly L long, so this is kept only as a safety.)
9. **No out-of-combat free nab:** while its host group is unresolved, the only verb on a Dormant Truant is Steal; Grab and attacks are greyed "guarded" (ITEMS 7.3) (4.7.5). Otherwise sneaking adjacent and attacking would nab it outside the fight the clock belongs to.
10. **Party ineligibility sets no carry:** a floor below the allowed party sizes consumes its 4 draws, hosts no Windfall and sets no `windfallCarry` (4.7.2, 4.7.10).
11. **Client keys, not tuning:** C = 3 and Surprised C = 4 (Q1); `loot.windfallParties` = {3, 4} with solo added only on the solo arm's pass, and `loot.windfallCSolo` = 4 (Q2), replacing the draft's single `windfallMinParty`; the waves (Q3); the pilot skins (Q4); the Bedside Stand host (Q5) (9.1). A gate miss that would move them returns to the client (14.2).
12. **The Bedside Stand silhouette hosts only the Viaticum** (V15s `BedsideStandOnly`), so the second exception never leaks (4.10, 5.1, LP-24).
13. **Flags in 13** also mark reads of agreed, decided or settled text (LE, TL, EP and IR precedent): LP-7, LP-10, LP-12, LP-14, LP-15, LP-21, LP-24, LP-25 and LP-34 are flagged; LP-1 names MAIN 4.6 (decided, read) and LP-5 names MAIN 4.3 (13).
14. **`ProprietorFallbackRate`** (≤ 10%, from the risk list) joins the reports (9.3).
15. **Owner-refusal fallbacks** in 14.2, each stated so a refusal never blocks M2 or the M3 pilot.

**Lead's calls after the check (confirmed by the client on 2026-11-22, with this document):**
1. **LootBind runs in a fixed numbered order** (item-7 remaps and the offer-pacing trim; container squares and their fallbacks; the Windfall host check, move or drop; home, Rathole and route; merges; Proprietors; coin, purse and salvage). The Windfall card is excluded from KinSwap and carrier fallback; a moved Windfall re-sources its card against the new host; a drop returns the card to its origin as resolved and its coin weight to the pool (4.3).
2. **Traps-bound sockets keep their square;** other sockets use a four-step square fallback ending in a hard failure, `LootBindUnplaceable`; HE and Due sockets never leave their room (4.3).
3. **The walk line is defined,** and each role's square choice has a scoring tuple (4.2, 4.3).
4. **The role table is data** (`loot.roleTable`); SocketPlan selects layout sockets up to `max`; **`cardEligible`** enforces one card-bearing container per room in the deal (4.2, 9.1).
5. **The container cap runs in two phases** (SocketPlan, then LootBind merges); F5's cap is min(10, fillers); fewer locks is a design-time TL lever (4.2).
6. **Step 8 details:** the `windfall` socket is outside steps 1–7; filter (f) splits into card-level (f1) and identity-level (f2, filter-first at the parent draw); the slot mapping is fixed at step 7 (4.7.6).
7. **Skin eligibility:** a floor whose theme has no pilot skin hosts no Windfall and sets no carry; skin = `skins[socketOrdinal mod len]` (4.7.2, 4.7.10).
8. **Presence raised to 1000‰ on F3 and F4** *(illustr.)*, F2 kept at 500‰ for the solo arm (4.7.2).
9. **Rathole first, then a home at exactly L = 4 path squares, then the route;** L is the same for every party size (4.7.3).
10. **Dormant means untouched** except by Steal; an area that opens combat resolves after the wake (4.7.4, 4.7.5).
11. **It wakes when any host-group member becomes Engaged,** or a fight starts in its room, so pulling the group out still starts the count (4.7.4).
12. **The count never rises on screen:** the digit starts at 3, and Surprised or solo extra steps are held steps with a reason badge; **the solo Surprised count of 5** extends the Stir precedent (4.7.4).
13. **Hops are checked square by square;** a partial hop is a block; the Truant never enters a unit or a harmful or obstacle square; a used slip shows as "held · slip used" (4.7.4).
14. **The combat-end check runs after each Dream sub-step** (4.7.4, LP-5).
15. **Steal:** a host-group `stealTryUsed` flag; a `die` index for advantage; a failed Steal wakes it at the party's starting count after the live-size check; a `stolen` state and `WindfallStolen`; spilled coin is guarded until the group resolves (4.7.8).
16. **Carry lifetime** defined (4.7.10).
17. **Grandchild key** `Derive(lootPlan, FNV-1a("windfall"), 0)`, its draws taken at the start of step 8 and mapped to values; LE 6.10 items 1 and 4 added to LP-1 (4.7.10, 13).
18. **The Annex, a boxed lurker and the first fight** are checked on the final host in LootBind; segment 8 never puts a clock Landmark in a Windfall room; "first fight" defined (4.7.2, 4.13).
19. **`WindfallCatchable`'s algorithm** is stated; the party-start clause is dropped (4.7.8).
20. **The greed and chase-death gates are defined** on paired seeds under the section 8 policy mix, with a confidence rule (4.7.9).
21. **The 7c clock line's baseline is defined,** and the cut order now trims containers before touching the Windfall; any Windfall cut returns to the client (4.6).
22. **Offer pacing has a draw-free trim;** the Viaticum is unranked (boss channel), replaces `lairContainer` on F5 and is excluded from Glean (4.1, 4.10, 7).
23. **Auto-close** uses party-wide fit, pauses on focus, has a settings toggle and duration, and logs a reopenable line (4.6).
24. **Getaway causes** share one event and one treatment; the strip's "windfall" count moves on nab, Steal or getaway (4.5, 4.7.7).
25. **Live party size** defined; `WindfallInstance` gains its missing fields (4.7.2, 9.1).
26. **A `windfall(unitId)` item location** and four ITEMS 18.1 transitions (4.7.7, LP-2).
27. **LP-33 recast:** the Truant is a suggestion and simulator-policy target only; no "Nab it" order (4.7.8).
28. **The lowest-tier card source stays;** a highest-eligible-tier arm and the width-2 offer run as report-only M3 arms (4.7.6).
29. **Truant art grammar:** one cross-theme sackcloth bundle; no faction motif; the Alms Crab re-specified (bell-clapper shell, limbs always out); the Rabbit's bundle unstriped and its Rathole a sawdust-rimmed opening, not a tent flap; the Mouse's napkin and place-card string dropped (4.7.1, 5.3).
30. **The Rathole has one arched cross-theme form** and a front-square floor decal for the cutaway; `WindfallSkinLegal` bans Portal skins and Hairline dressing (4.7.3).
31. **Insets:** a cutaway floor bracket, meshes below stub height, a per-theme inset form table, and CT15 (insets never read as Portals) (4.3, 5.1, 5.2).
32. **Marks have shapes, slots and tiers;** the countdown uses a hollow or solid ring so state never depends on hue (6).
33. **A Windfall timbre row and 6 cues** replace the proposal's 4 (6).
34. **A closed act table of 9 acts;** a loss always takes the bundle, a gain always leaves a pile; the cower plays only while awake (6).
35. **The Bedside Stand** is unit-scale, with a defined open act; the Heart's drawer-slide Latch is banned from F5 lair rooms (4.10).
36. **Signature loot moments** get a deliverable table, with M2 moments for the Choir and the Supper (5.2).
37. **Proprietor selection** is defined, and the noun is always the skin's (4.5).
38. **Uncorked** is a shape sub-state of the glint, never a hue (7).
39. **The art budget** has asset counts per wave and a cut order (6).
40. **Corrections:** the bait cache is M2 (as EP and LE already say); "by family" becomes "by source"; the "Got away" line shows the tier word only; the signature cache's "lair" is read as the arena (13); missed flags added to LP-3, LP-5, LP-8, LP-9, LP-13, LP-32 and LP-33; four catalogue names added to A.5.

**Q6, the Windfall frequency (raised by the check; agreed, 2026-11-22, with the approval of this document):** option A. Q1 agreed "about 1.2–1.4 per run". That figure came from a toy at party 4 on every floor with every theme skinned. Under Q2 (party ≥ 3, duo held), the companion ladder (party at most 1 / 2 / 3 / 4 / 4) and Q4 (M3 skins only for the Choir, the Carnival and the Supper), F2 is never eligible without the solo arm, and Silk and Gut floors never host a Truant. Even with presence raised to 1000‰ on F3 and F4, the lead's estimate is **about 0.6–0.9 per run for an always-hiring party** *(illustr.)*, lower for a player who declines a hire; under RS Cut 2 only F3 can host one.
- **A (agreed).** Accept about 0.6–0.9 per run *(illustr.)* for the M3 pilot, with 1000‰ on F3–F4; the simulator owns the figure and reports `WindfallsPerRun`, and the pilot's gates decide whether to widen it in M4.
- Rejected: **B**, bring the Silverfish (Silk) and the Rent Snail (Gut) into the M3 pilot (changes Q4; +2 skins and their role-ID passes); about 0.9–1.3 per run *(illustr.)*. **C**, open a duo arm in M3 at C = 4 like solo (changes Q2), so F2 can host; only if it passes ≤ +1 pp.

**Told plainly after the check (confirmed by the client on 2026-11-22):**
- The agreed "about 1.2–1.4 per run" is out of reach under Q2, Q4 and the companion ladder; F2 is never eligible without the solo arm; Silk and Gut floors host no Truant in M3; under RS Cut 2 F4 never hosts one (0.1, 4.7.2); the client accepted about 0.6–0.9 for the M3 pilot (agreed, Q6).
- The proposal's cut order turned the Windfall off before trimming containers; that is not the lead's call, so containers are cut first and any Windfall cut returns to the client (4.6).
- The lowest-tier card rule makes the visible stake usually a Common; the lead keeps it (the client accepted the Windfall as written) and runs richer variants as report-only arms (4.7.6).
- Companions never chase the Truant by themselves: the player controls every unit (MAIN 4.2), so the proposal's "Nab it" order and autonomous rule are dropped (4.7.8).
- Two pilot skins borrowed their faction's motif and the Alms Crab's cup read as a moving container; the Rabbit's tent flap was the Carnival's Drape Portal and the Crab's crack a decoy Hairline. All are re-specified inside Q4's choice of animals (5.3).
- The bait cache was mis-staged as M3; it is M2 with the Mousetrap, as approved (4.1, 10).

**Told plainly (gathered from where they stand; confirmed by the client on 2026-11-22):**
- The Windfall never "appears": RC1, EP 13 and determinism require it to be pre-placed and visible at reveal; the dream logic is in how it wakes and leaves (0.1).
- No extra value: its card comes out of the fixed deck; extra value would break Ledger A, MAIN 4.6 and "harder is not richer" (0.1, 4.7.6).
- In M3 a solo-hero or duo run never sees fleeting loot unless the solo arm passes; duo is held (0.1, 4.7.2).
- The client's "during exploration" case has no M3 form (4.7.11).
- The Gaol prisoner, Supper trolley, Heart toy and Silk page are declined as skins: they read as the Escapee, a rules piece, a toy piece and the lurker's rest tell (0.1, 5.3).
- The Truant is honestly a small tempo choice: the toy nab rate is 0.97, so `FightEndNabShare` is dropped as a gate and kept as a report (4.7.9).
- The carried-card source was not simulated; it is the first M3 arm (4.7.6, 8).
- Furnishing stashes are rejected; inset containers answer the brief (0.1, 4.3, 7).
- Glean saves about 0.25 clicks per floor, not 3–5; auto-close is the real lever (4.6).
- The toy role tables produced 12.5–14.5 containers per floor; the count must be cut and the walker must confirm it (4.2).
- Loot clicks are 19–21 of the 40-click budget before walker confirmation, the largest unknown on the floor clock (8).
- "Lair cache" in LE 6.9 means the elite room's cache (4.1, 13).
- LP-3 pulls a non-hostile runner from an approved Later list into M3 (4.7.1).
- The Windfall is a unit socket, not a container, so LX11 holds (4.7.6).
- The Stratum suffix "one may get away" primes the hunt, by design (4.5).
- The Cow payoff fires on under about 5% of hosts and is not advertised as a Herald payoff (4.7.8).
- The RC7 cues are clock telegraphs, not reward stings, and need the RS owner (6).
- CT6 contests the brief's Due shrine; CT11 contests a per-theme Treasure strip (5.1).
- "Proprietor tag" replaces the draft's "Whose line", because "line" is IR's rolled-line term (3.3).
- Pips instead of a digit would be a lying exception (2.3 PX3).

### 14.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a wave or a later design triggers them; each fallback is a new lead's call or a lead's call after the check (confirmed by the client on 2026-11-22, with this document):

| Item | Trigger | What happens | Owner / approval |
|---|---|---|---|
| Solo Windfalls | The solo arm (C = 4) shows `WindfallGreedDowns` ≤ +2 pp | `windfallParties` gains 1; solo uses C = 4 (agreed, Q2) | Lead (pre-approved by Q2) |
| Duo Windfalls | A later duo arm passes ≤ +1 pp | Returns to the client (held under Q2) | Client |
| Windfall gate miss at party ≥ 3 | `WindfallGreedDowns` > +1 pp or `WindfallRoundsDelta` > +0.2 | Route geometry first (lead); any lower frequency, turning it off or any change to C returns to the client (changed after the check: the proposal's "F2 only, then off" was a client matter) | Lead for geometry; client otherwise |
| Windfall frequency | `WindfallsPerRun` from the first M3 arm, by theme mix and party schedule | Measured against the about 0.6–0.9 per run agreed with Q6 (option A); widening it in M4 follows the pilot's gates, and any lower frequency or turning it off returns to the client (row above) | Client (agreed, Q6) |
| Richer Windfall stake | The highest-eligible-tier or width-2 offer arm passes ≤ +1 pp | Returns to the client before adoption (the Windfall was accepted as written) | Client |
| Width-2 offer on nab | `WindfallIgnoreShare` high | Its own keyed grandchild; offer pacing counts it; adoption returns to the client (row above) | Lead (M4 lever); client |
| Lost Property buy-back | Greed gates miss | Camp hook tested | Camp and hub owner |
| 7c clock line miss | Walker shows net > 0 | The 4.6 cut order (containers first), before any CMP 7.4 lever; its Windfall step returns to the client | Lead; client for the Windfall step |
| Glean | Playtest shows no demand | Not shipped | Lead |
| Tribute | `CowRate` shows Cow under-used | Returns to the client (reopens ENM 15.7 Q3) | Client; enemies owner |
| **LE owner refuses** step 8 | — | The Windfall card source becomes (ii) only, inside step 7's order with filters asserted after it; the host-carried source is dropped and logged (new lead's call) | LE owner |
| **Enemies owner refuses** the non-combatant row or the runner pull-forward | — | The Windfall stays M4 with the Ebb; M3 ships no fleeting loot (new lead's call) | Enemies owner; client told |
| **HZ owner refuses** sub-step 3b | — | The hop runs as a Remnant-like countdown inside sub-step 3, same order relative to announcements (new lead's call) | HZ owner |
| **RS owner refuses** the RC7 cues | — | The telegraph relies on visuals and the global UI timbre; the audio blind-ID gate is not run for Windfalls (new lead's call) | RS owner |
| **IR owner refuses** the Bedside Stand exception | — | Returns to the client (Q5 was agreed); meanwhile a biscuit tin with the offer UI and a hover line "Provisions: choose one" (new lead's call) | IR owner; client |
| **FL or ENM owner refuses** the Viaticum room reading | — | LE 6.11 item 5's primary rule stands and segment 2 adds a post-lair main-path container on F5 (new lead's call) | FL, ENM owners |
| **EP or TL owner refuses** LP-16 to LP-20 | — | The approved skin stays; V15s logs the collision as a role-ID gate row (new lead's call) | EP, TL owners |
| **CMP owner refuses** the Truant as a suggestion target | — | No suggestion ever points at a Truant; the player still controls every unit, and the simulator's companion policy targets it only in its own arm (changed after the check) | CMP owner |
| **Class owners refuse** `ignoresWindfall` | — | The Windfall pilot waits for the owning class; `WindfallNabByClass` reported (new lead's call) | Class owners |
| **Segment 3 offers no set pieces** | — | The Alcove default stays a container; the cap is logged, not enforced (risk 7) | Segment 3 |
| Exploration Truant and Ebb | M4 | Specified at M4 with the ability owner and main 4.3 | Lead; client at M4 |
| F1 or F5 Windfalls | A new client decision | F1 and F5 skins authored | Client |
| The Inverted Cathedral | M4 theme design | Its loot family completed; V14 blocks shipping until then | RS owner; segment 9 |
| Naming pass | — | Every placeholder name here | Client |

---

## 15. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | **The Truant is a small tempo tax more than a decision** (nab rate 0.97). The stake is real but rarely lost, and the lowest-tier rule makes it usually a Common. It is the most expensive part of 7c per appearance | Route geometry; the highest-tier and width-2 report arms in M3 (4.7.6); any richer stake or cut returns to the client |
| 2 | **The carried-card source is unsimulated.** It may thin a fight's visible carried loot or interact with the Steal-target count | The first M3 arm; V15 ≥ 1 Steal target after step 8 |
| 3 | **Clicks are unmeasured.** Loot takes about 19–21 of the 40-click budget in the toy, and CMP 7.4 already fails the lean case. The walker re-baseline may force deeper container cuts | Walker first; net ≤ 0 line; cut order |
| 4 | **Party-size and skin limits** leave solo and duo players without the feature in M3, make F2 ineligible for everyone, and leave Silk and Gut floors without it: about 0.6–0.9 Windfalls per run against Q1's 1.2–1.4 | The solo arm (agreed, Q2); presence 1000‰ on F3–F4; the client accepted about 0.6–0.9 for the M3 pilot (agreed, Q6), and the simulator owns the figure |
| 5 | **The Dream card gets busier** | A glance-test row; one clock per room keeps each room readable |
| 6 | **RC7 cues** need the RS owner. Until then the telegraph relies on visuals | Request; fallback in 14.2 |
| 7 | **The Alcove set-piece default** depends on segment 3 supplying set pieces. If none exist, the cap is logged, not enforced | Request; fallback in 14.2 |
| 8 | **Proprietor tags** cost writing across 9 themes. Fallback rates may be high on small themes | `ProprietorFallbackRate` ≤ 10% |
| 9 | **Glean** may feel like it "tricked" players when a lurker springs | Watch `ContainersOpened`; Glean optional |
| 10 | **Art:** 3 faced Truant skins (about 17 assets each) for under 1 appearance per run | The pilot limits this (agreed, Q4); the asset table and cut order (6) |
| 11 | **Owner dependencies:** LE (step 8, the draw-table row), ENM (unit row, runner, Engaged wake), HZ (3b), RS (cues, timbre row, charter), IR (Bedside Stand, the arena reading), ITEMS (the `windfall` location), FL and ENM (Viaticum room), EP and TL (skins), CMP (suggestion target), class owners | Each has a fallback in 14.2 |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the role-table counts; `containerCap`; `detourMax`; `summaryAutoCloseMs`; the click table until the walker measures it; `windfallPermille`; the hop length; the Truant passive; the Ebb's C; every report target other than the stated gates.
- Proprietor writing per theme within the noun ban; container variants within CT1–CT15; theme foley tails; Hairline dressing.
- Godot scene structure for containers, inset kits, the Truant plate, the scurry trail, the next-hop outline, the Rathole decal and the Bedside Stand, with no physics.
- Names, until the naming pass.
- **Not tuning:** the role table's ranks and "never" column; one card-bearing container per room (`cardEligible`); the walk-line definition; LootBind legality and its numbered draw-free order after `trapsResolve`; traps-bound sockets keep their square; step 8 and filters (f) and (g), asserted after step 8; the 4 draws on every floor; no runtime draws but the keyed-hash Steal; the Windfall pre-placed and visible at reveal, never spawned; C = 3 and Surprised C = 4 **(agreed, Q1)**; party sizes {3, 4} with solo only by its arm **(agreed, Q2)**; the Windfall in M3 and the Ebb in M4 **(agreed, Q3)**; the pilot skins and the exclusions **(agreed, Q4)**; the Bedside Stand as the only Viaticum host **(agreed, Q5)**; slip once; held steps, never a rising digit; Dormant untouched but by Steal; a nab is not a hit, damage or a kill; no free-interaction nab; the room's one Steal try; card only on Steal; the one named escape exception (LP-2).

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 13; by owner:

- **The enemies owner:** LP-1 (15.3 step 1), LP-2 (15.6), LP-3 (7.1, 14.5, 21), LP-5 (14.6, 15.6, 15.8), LP-6 (15.5), LP-21 (15.2), LP-27 (15.2, 15.4); the Viaticum room confirmation.
- **The items owner:** LP-2 (18.1 location and transitions, 18.3, 19.2), LP-3 (7.3 "guarded"), LP-6, LP-8 (7, 8.1), LP-9 (8.1); the in-combat Plain open cost.
- **The traps owner:** LP-2 (6.13, TL-27), LP-8, LP-9 (6.13), LP-12, LP-17 to LP-20, LP-26, LP-34; the Hour-lock reading; fewer locks.
- **The hazards owner:** LP-2 (13), LP-4 (6.2.2, 6.3).
- **The FL owner:** LP-8 (11.4), LP-10 (6.10), LP-11 (13), LP-12 (11.1), LP-22, LP-32 (11.3); the walker re-baseline; the Viaticum room.
- **The room terrain owner (segment 3):** Alcove set pieces (LP-11).
- **The EP owner:** LP-16.
- **The RS owner:** LP-7, LP-15, LP-24, LP-31, LP-34; the RC7 cue request and the Windfall timbre row; skin vetting.
- **Dungeon generation 7a (LE):** LP-1 (6.9, 6.10 items 1 and 4, 6.14), LP-2 (8.2), LP-6 (8.3), LP-28, LP-29, LP-30, LP-32; the readings in 13.
- **Dungeon generation 7b (IR):** LP-13, LP-14.
- **The companions owner:** LP-8 (7.4), LP-33 (8.6).
- **The class owners:** `ignoresWindfall` (V15s).
- **Segment 8 (events), segment 9 (bosses), segment 10 (UI) and audio:** 4.12, 4.13, 6; segment 8's no-clock-Landmark rule in Windfall rooms; segment 9's arena reading. Segment 8 is now designed (events design, approved 2026-11-22): its EV-2, EV-7, EV-15, EV-21, EV-25, EV-35 and EV-42 rows touch 7c (4.13); `stepFunded` and F-CACHE are pending requests to 7c. Segment 9 is now designed (boss rooms design, approved 2026-11-22): its BR-27 and BR-37 rows touch 7c (4.4, 4.10, 4.13). Segment 10 is now designed (exploration and information design, approved 2026-11-23): its XI-19, XI-23 and XI-27 rows touch 7c (4.2, 4.12, 4.13, 7), and XI-10 and XI-18 read 4.2 and 4.7.3; `SideRoomPayoffMix` is a pending request to 7c.
- **Segment 11 (seeds):** the `windfall` grandchild (LE 6.10 item 1's new row), SocketPlan, step 8 and LootBind's sub-order in the frozen generation order (LE 6.10 item 4). **Answered** by the seeds and validation design (approved 2026-11-23; its 4.3 and 4.4): `loot/lootPlan/{N}/windfall/0`, fixed 4, attempt-invariant (M3); SocketPlan at GS9, step 8's Windfall draws at the start of step 8 inside 3a (GS10), LootBind at GS14 in 4.3's numbered order; its SV-1, SV-2, SV-6, SV-9 and SV-31 rows touch 7c (2.1, 4.2, 4.3, 4.7.8, 4.7.10, 4.11, 5.1 CT2).
- **Segment 12 (validation):** V15 and the 7c reports. **Answered** by the seeds and validation design (approved 2026-11-23; its 5.1, 5.2, 5.4, 5.5, 6.2, 6.10): V15's qualified phases and failure classes, V15g at AP2 after `V16g.SlotLegal`, `LootBindDegrade` at LR4, the report-registry rows and the qualified arms; its SV-10, SV-18, SV-20, SV-22 and SV-24 rows touch 7c (4.3, 9.2–9.4, 10). Pending requests to 7c (not rules until ruled; fallbacks in its 14.2): the V15 M1 rows, the HE/Due binding proof, the default `LootPolicy`, and the merge-or-coin rule of `LootBindDegrade`.
- **The camp and hub design:** Lost Property; the Hawker's Hour with segment 8 (Later; events design EV-42).
- **The simulator:** every report of 9.3 and every arm of 9.4.
- **Art, audio and writing:** 5, 6; Proprietor tags.
- **Naming pass:** every placeholder name here.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Fun critique (CF)

| # | Finding | Resolution |
|---|---|---|
| CF1 | Coin-only Truant on F3–F4 has no thrill | Card from the host fight's carried allotment; never a coin-only Windfall in M3 (4.7.6) |
| CF2 | REC fails its own FightEndCatchShare gate | Told plainly; gate dropped, kept as a report; combat-end ordering stated (4.7.4, 4.7.9) |
| CF3 | Solo and duo get nothing | Solo arm at C = 4 opens solo if ≤ +2 pp (agreed, Q2); duo held; told plainly |
| CF4 | Shade Steal pre-empts about 60% | Card only, coin spills; the room's one Steal try; passive 15; `WindfallStealShare` ≤ 40% |
| CF5 | Coin tins bring back click fatigue | Count cut (merge, set pieces), cap 12 not 13, auto-close; walker first |
| CF6 | Themes feel the same | Proprietor tags in M2 for all themes; a signature loot moment per theme (5.2) |
| CF7 | Exploration unanswered | Told plainly; agreed, Q3; exploration Truant specified for M4 |
| CF8 | The Herald play is advertised but dead | Claim struck; neutral rule kept |
| CF9 | A held Truant becomes terrain | Slip once; units pass through its square |
| CF10 | FOMO lines | Run record neutral, never an achievement; Stratum suffix kept as honest signposting |

### A.2 Balance critique (CB)

| # | Finding | Resolution |
|---|---|---|
| CB1 | Blocking holds C forever | Slip once, then C falls; fade where it stands |
| CB2 | Shuffles and swap break filters | Step 8 outside the shuffle; assert after step 8; pre-existing secret and quota issue hooked to the LE owner |
| CB3 | Auto-hit target feeds on-hit triggers | A nab is not a hit, damage or a kill; `ignoresWindfall` |
| CB4 | Roll-forward has no state; door dependence | Presence in LootPlan on formation-independent rules; in-floor move; persisted `windfallCarry`; golden seeds |
| CB5 | Windfall takes the secret's card | Filter (g) bans secret and quota sources; carried-card source first |
| CB6 | Clock double-counting | Walker re-baseline; net ≤ 0 line; cut count before cap |
| CB7 | Whole-sack Steal; stream shift | Card only; keyed-hash roll |
| CB8 | Party size changes mid-run | Live-size check at combat start; Dormant plain Take if below 3 |
| CB9 | "No container opens in combat" removes a lifeline | Rule dropped |
| CB10 | Viaticum in a fight host | Lair room exit side; lean band in every arm; owners confirm |
| CB11 | Cow loop on broken groups | Neutral rule kept; exclusion lever if `CowRate` on Windfall rooms is high |
| CB12 | Width-2 offer creep | Held to M4 on its own keyed grandchild |
| CB13 | Coin accounting on escape | Pre-escape carve; never re-carved |
| CB14 | Reload | Persist `WindfallState` including `slipUsed`; V15t |

### A.3 Consistency critique (CK)

| # | Finding | Resolution |
|---|---|---|
| CK1 | Object Truants break RC3 and the lurker read | Truants are faced animals only; objects are Ebbs |
| CK2 | Supper cloche, dumbwaiter and hatch | Crumb Mouse to a wainscot mousehole; Rathole prop ban |
| CK3 | No-open-in-combat contradicts TL | Dropped |
| CK4 | Filters not shuffle-stable | Step 8 |
| CK5 | LP register collisions | One register LP-1..34; A.6 cross-reference |
| CK6 | Escape reason inconsistent; "dreamed option" misuse | `gotAway` only; not a dreamed option |
| CK7 | No Clearway or choke rule for the Truant body | 4.3 legality; pass-through rule |
| CK8 | "Catch" collides with the TL Catch | Nab / Grab |
| CK9 | Steal rule lives in two places; Keen misuse | LP-6 names ENM 15.5, S-5, STAT 7.1, LE-16; fixed passive row |
| CK10 | "Dozing" posture | Dormant posture and glyph |
| CK11 | Fight-end and flee changes unflagged | LP-5, with exact sections named while writing |
| CK12 | RC7 cues need the RS owner | Flagged request; one list (4 in the proposal; 6 with a timbre row after the check, 6) |
| CK13 | Ebb motion; plate on water | LP-34 (M4); dry squares only |
| CK14 | Gating and roll-forward determinism | 4.7.10 |
| CK15 | Clock math hides a deficit | Net ≤ 0 line |
| CK16 | Herald payoff stale | Struck |
| CK17 | Gb SR1 and socketId order readings | Gb reading to the LE owner; square-index default kept |
| CK18 | Viaticum is a reading; Bedside Stand is a change | As stated (LP-14, 13) |
| CK19 | Bolt-hole echoes Bolt | **Rathole** |
| CK20 | Action-economy scope | Only actions target it; areas from any source |
| CK21 | Mislabelled wave reopening | Corrected: the runner Later→M3 is the real flag (LP-3); its sections corrected while writing to ENM 14.5 and 21 |
| CK22 | Carnival painted flat near the secret | Tent-flap gap |
| CK23 | Glean exclusions contradictory | C's no-leak rule |
| CK24 | LE 6.14 SR2 row inheritance | New row in LP-1 |

### A.4 Judges' grafts taken

All three judges' winner (Model A) is adopted as the skeleton (3.3). Grafts: B's Proprietor tags (renamed), wall hosts merged with A's insets, auto-close of quiet summaries and the always-consumed draw table; C's C = 3, threshold preview, `MainPathDetour`, Tidy's no-leak rule and offer pacing; the fleeting-loot draft's "a nab is not a kill", "slip, not freeze", `ItemDestroyed{gotAway}` and catchability proofs; the catalogue's CT1–CT14 and collision fixes. Not taken: B's Crannies, Tip-offs and pace-parity exploration Truant; C's Trove, hidden-from-entry home and "C falls when blocked"; every free-grab design.

### A.5 Names checked (case-insensitive, whole word, all 25 approved documents)

Re-run while writing this document (2026-11-22).

- **0 hits, used:** LP-[0-9], V15, Windfall, Truant, Ebb, Nab, Nabbed, Grab, Rathole, Glean, Proprietor, Proprietor tag, Bedside, Bedside Stand, Alms Crab, Silverfish, Crumb, Crumb Mouse, Conjuror, Rent Snail, Uncorked, gotAway, windfallCarry, `wakeDelay`, `MainPathDetour`, `ContainerArtBlind`, `ContainerFamilyDistinct`, SocketPlan, `LootSocketRecord`, quiet summary, auto-close, Offertory Plate, Prize Balloon, Rising Votive, Trove, Crannies, Tip-offs, Tattler, Hawker, Lost Property, Blocked Hairline, Chained Key, Shepherded, Tribute, Grapnel, Doorstop, the container family names (confiscation tin, creel, pen box, grave-goods casket, alms chest, archive chest, prize hamper, busker's hat, silver canteen, savings tin, biscuit tin, document case, sewing basket), and the local id schemes PX, PT, CT, CF, CB, CK. **Added while writing (new lead's call):** scurry trail, `onWindfall`, wall-set mark, got-away cue, `windfallParties`, `windfallCSolo`, `BedsideStandOnly`, `ProprietorFallbackRate`, next-hop outline, Windfall mark. ("Wake chime", "hop tick", "nabbed chime" and "Nab it" were used in the proposal and are retired after the check.)
- **0 hits, used (added after the check):** Sacristy, Box Office, Plate Room, Rare Books Room (catalogue names missed in the first list); `stealTryUsed`, `cardEligible`, `roleTable`, `RoleTableRow`, `trapsBound`, `originSocketId`, `LootBindUnplaceable`, `WindfallStolen`, `WindfallHeld`, `WindfallsPerRun`, `SummaryAutoClosed`, `ProprietorFallback`, `ProprietorNounMatchesSkin`, rank family, held step, wait-at-hole, sackcloth, pawprint, knotted-sack, skirting, pluck, bell-clapper, CT15.
- **Avoided after the check:** "chevron" for the next-hop outline (EP's fall-back chevron, 1 hit) → pawprint outline; "burrow" for the Rathole form (EP's GU4 Burrow template, 1 hit) → arched opening; "walk line" has 1 informal FL use and is now defined here (4.2), with that use consistent.
- **Existing meaning, used deliberately:** clock-pip (HZ's countdown mark, 1 hit; reused for the Truant's countdown, after the check), Treasure strip (LE 6.15, 1 hit; extended), tier word (LE 6.15), Confession Box and Will-Box (TL 7.2, 7.4; 5 hits each, the approved containers), sphincter cache (TL 7.9, FL, IR; the approved Gut container), Hour lock (TL), poacher (RS, EP and IR Kennel wording), Dormant (ENM posture), Grave Goods (LE 8.2), "Guards:" (ENM 15.2), Treasure count (LE 8.2).
- **Taken or avoided:**
  - Quarry, Escapee, Stray, Runaway, runner/Runner, Thief, Chase.
  - **Catch** (TL lock kind; hence Nab), Fade, Vanish, Fleet, Keepsake, Lure, Stash, Sweep, Hourglass, Countdown.
  - **Keen** (an ENM group trait; not reused for the Truant), Keeper, Owner.
  - "Whose line" (IR's rolled lines; renamed Proprietor tag), cloche (the Top Table Wager), Dumbwaiter Drop, serving hatch.
  - PF (7 hits; not used).
  - Bolt and Bolt-hole (Bolt renamed Latch in FL; hence Rathole).
  - "Dozing" (replaced by the approved Dormant).
  - **While writing:** "footprints" for the route (Stir and Keg footprints, 50 hits) → scurry trail; `heldBy` (VG data predicate, 1 hit) → `onWindfall`; "inset" for the RC5 mark ("inset dashed", the harm-square rim, 3 hits) → wall-set mark (the noun phrase "inset container" has 0 hits and is kept); "sting" for a loot cue (RC7 boss and Stir term) → got-away cue; `windfallMinParty` (draft key) → `windfallParties`.

### A.6 LP cross-reference (source drafts → this register)

Draft-local numbers; never read as this register.

- **Model A:** LP-1→1, 2→2, 3→2, 4→3, 5→4, 6→6, 7→9, 8→8/10, 9→7, 10 (width-2 offer)→held, 11→reading (Viaticum).
- **Model B:** LP-1→13, 2→12, 3 (Crannies)→Later, 4→8/9, 5→8, 6→10, 7→not adopted, 8→1, 9→2, 10→3, 11→4, 12→7, 13 (Tip-offs)→Later, 14→31, 15→32.
- **Model C:** LP-1→1, 2→2, 3→2, 4→4, 5→34, 6→3, 7→6, 8→7, 9→1, 10→8/9, 11→10, 12→1 (filter f), 13 (Trove)→Later, 14→30, 15→11, 16→31, 17→32.
- **Fleeting draft:** LP-1→4, 2→4, 3→2, 4→3, 5→1, 6→2/29, 7→6, 8→7/34, 9→4.7.10, 10→4.7.2, 11→10, 12→31, 13→33, 14→Q4 B (rejected).
- **New mechanics:** LP-1→1, 2→2, 3→4, 4→3, 5→7, 6→6, 7→34, 8→10, 9→9, 10→27, 11→28, 12 (Blocked Hairline)→held, 13 (Chained Key)→held, 14→11, 15→29, 16→31.
- **Catalogue:** LP-31→15, 32→15, 33→14, 34→26, 35→16, 36→17, 37→18, 38→19, 39→20, 40→7, 41→22, 42→23, 43→24, 44→25, 45→21.

## Appendix B: sources

Research briefs (web-confirmed: the XCOM 2 loot timer, Into the Breach time pods, Hades troves); other game details are from memory, with confidence stated in the briefs. Toy scripts and outputs (not project files), under `/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/7c/`: `truant.py`, `run_truant.py`, `ebb.py`, `floors.py`, `determinism.py`, `misc.py`, `trove.py`, `trove2.py`, `calib.py`, and the outputs `out_mix.txt`, `out_nosteal.txt`, `out_smart.txt`, `out_steal.txt`, `out_v2_nosteal.txt` and the `res_*.json` files. The proposal this document converts is `/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/loot-sources-synthesis.md`. No parent document was modified.
