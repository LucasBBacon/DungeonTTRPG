# Loot Economy and Curves: Design (Dungeon Generation, Segment 7a)

Date: 2026-11-22
Status: Approved, 2026-11-22
Parents: `2026-11-15-dungeon-rpg-design.md` (main design, "MAIN"),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "CON"),
`2026-11-16-enemies-design.md` (enemies design, "ENM"),
`2026-11-16-items-inventory-design.md` (items design, "ITEMS", **the key
parent**),
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
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1; its change
ids are RT-n),
`2026-11-21-floor-layout-design.md` ("FL", segment 2),
`2026-11-21-room-terrain-design.md` ("ROOM", segment 3; its change ids
are TR-n; abbreviated ROOM here so it never reads as an RS change id),
`2026-11-21-hazards-design.md` ("HZ", segment 4),
`2026-11-21-traps-locks-design.md` ("TL", segment 5) and
`2026-11-21-encounter-placement-design.md` ("EP", segment 6, the most
recent template)

Items marked **(agreed)** are the client's answers of 2026-11-22 to the
six questions this design raised (Q1–Q6, sections 0.2 and 18.1), each
answered with the lead's recommendation (option A), or restate a client
answer recorded in an approved document. The client also said that
everything else in the lead's proposal stands as written: Model A (fixed
spoils lists) trimmed, with grafts from Models B and C; the three ledgers
(count, value in VP, coin); rarity decks with two equal-VP variants on a
`lootPlan` child stream; the boss channel; offer sets counted once; the
camp channel of 2; the coin pool of 35 / 50 / 70 / 90 / 0 per party (with
the toy's Hmid fix recorded as written); Sell 250‰; the Risk Ledger; K2
made computable (with K2b); the Dream's Due alternating between the hero
and the lowest-geared member; determinism with the atomic camp-exit
commit and `LootLedger`; the Treasure count and Grave Goods; the Heavy
Purse; category weights and the F5 tactical cap; the elite intel line;
the M3 meta hooks; real entries in the bait cache and the boxed lurker;
the Twin Coffer (M3); Curses at a discount (Later; M4 by change HL-29); V13 `LootEconomy`,
the reports, loot last in the DH 18.2 lever order and kPC first; the
camp sinks passed as hooks; the told-plainly items; the risks; and the
appendix resolutions and names. The client approved this document as a
whole on 2026-11-22, including the Q5 wording: "that floor's camp card"
reads as the Perilous floor's ENM 17.5 boss reward card, shown before
its camp, which becomes choose 1 of 4 (6.12, 18.1).

Items marked **(lead's call)** were filled in by the design lead inside
approved rules and were in the proposal the client answered; the client
confirmed them all on 2026-11-22, when it approved this document as a
whole, and they are binding. Items marked **(new lead's call)** were
made while writing this document, beyond that proposal; they are listed
in 18.1 and the client confirmed them all on 2026-11-22. Items marked
**(lead's call, after the check)** were made when this document was
revised after its design check; they are listed in 18.1 with the "Told
plainly after the check" list, the client confirmed them all on
2026-11-22, and they never override a client answer. Items marked
**(settled)** restate a rule from an approved document;
**(decided)** marks a main-design client decision. Items marked
**(change LE-n)** amend an approved document and are listed, with the
exact section they touch, in section 16; rows that touch **decided** or
**agreed** text are flagged there, and the client's approval of this
document on 2026-11-22 (with Q1–Q6 where a row names one) covers them.
Items marked **(told plainly)** narrow or re-read agreed or settled
text, contest a judge, a critic or the brief, or state an unwelcome
forecast, and were told to the client in plain words (18.1, including
those told after the check); the client confirmed them all on
2026-11-22. Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q6 of this document (2026-11-22), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-22 |
| **(new lead's call)** | A lead's call made while writing this document, beyond the answered proposal; listed in 18.1 and confirmed by the client on 2026-11-22 |
| **(lead's call, after the check)** | A lead's call made when revising this document after its design check; listed in 18.1 and confirmed by the client on 2026-11-22; never overrides a client answer |
| **(change LE-n)** | Amends an approved document; listed with its exact section in 16 |
| **(told plainly)** | Narrows or re-reads approved text, contests a judge, a critic or the brief, or states an unwelcome forecast; told to the client in plain words (18.1) and confirmed by the client on 2026-11-22 |
| **(hook)** | Owned by another segment or design; 7a states only what it needs |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (9, 10). Never binding |

**The client's brief.** Dungeon generation segment 7a of 12: **loot
economy and curves**. The client split loot into 7a (economy and curves),
7b (item rolls and loot tables) and 7c (sources and placement), and keeps
**shops and vendors as hooks only**, owned later by the camp and hub
design. 7a owns: the value budget per floor and per run and its currency;
the split by category and depth; rarity curves by depth (floor 1 and
boss floors); coin income against sinks; guarantees, bad-luck protection,
pity and anti-hoarding; party-size scaling; loot against experience and
the power curve against the enemy depth curve and DH 18.2; risk premiums
inside or outside the budget; determinism and the loot stream; how the
M3 meta touches the economy; and the hooks to 7b and 7c. The client had
no specific considerations and asked for the usual research and
considerations, interesting where cheap. Standing guidance: not an exact
5E port; not limited by quantity; combat is key; contest the client or
approved views where warranted.

The client's six answers (Q1–Q6, **agreed**, 2026-11-22) are in 0.2:
**floor 5 pays before the arena (Q1); a Wax Key opens a consequence cache
but takes its ordinary identity (Q2); salvage is a 12% share of a fixed
coin pool (Q3); coin is per party, never read from the actual party
(Q4); the Perilous Door reveals a pre-drawn fourth option (Q5); and the
risk incentive is fixed by encounter and experience tuning, never by more
loot (Q6).**

**Change prefix.** This design uses **LE-n** ("loot economy"). A grep for
`\bLE-[0-9]+` across the 23 approved documents finds 0 hits. This
document issues one register, **LE-1 to LE-24** (16); no id has two
meanings. The research drafts' colliding local numbers (three LE-1..18
sets, the model drafts' LE-1..20 and the mechanics brief's LE-20..32) are
all renumbered into it; the cross-reference is in A.6. Local id schemes,
each grep-checked as a whole word with 0 hits:
- tensions **LX1–LX17**;
- Appendix A findings **LF** (fun), **LB** (balance) and **LK**
  (consistency) (`LT` was rejected: 3 hits);
- socket risk ranks **SR0–SR5** **(new lead's call:** the proposal said
  "R0–R5", and `R3` and `R4` are RS route-walk rule names (8 whole-word
  hits)**)**;
- this document's client questions **Q1–Q6** (local, as in every parent).

**Validator.** **V13 `LootEconomy`** is free: V1–V7 RS, V8 FL, V9 ROOM,
V10 HZ, V11 TL, V12 EP; a grep for `\bV1[3-9]\b` finds 0 hits.

**Names are placeholders** until the client naming pass. Each was
grep-checked case-insensitively as a whole word across the 23 approved
documents; the list and the renames forced by collisions are in A.5. The
check forced four renames of draft names: "Threshold Cache" (RS 8.0 keeps
"Threshold" out of new names) → **Viaticum**; "trophy" and "Tally" (the
Trophy Tally trinket and its Tally property, TK) → **Memento** and
**Treasure count**; "Marked Purse" ("Marked" is the retired name of
Spotted, CON) → **Heavy Purse**. A critic's proposed "keepsake" also
collides (the RS Keepsake dressing) and is not used.

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every
rate and share comes from crude toy models (9, Appendix B); the M2
simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile,
unlocks, ladder), **M4** breadth, **Later** **(settled, main design 9)**.

Abbreviations: F1–F5 = floors 1–5; camps 1–4 (camp 4 is the **Last
Hollow**, RS 6.5); C / U / R / VR = Common, Uncommon, Rare, Very rare;
**VP** = value points, the ITEMS 12.6 offer-value unit; "party 2" = a
party of two units; ‰ = permille.

---

## 0. At a glance

### 0.1 The lead's answers to the brief

**The design in one line.** *A fixed spoils list per floor (the approved
notable count, an exact-value rarity deck and a fixed per-party coin
pool), dealt to sockets by risk, with one announced Dream's Due per
floor, floor 5 paying before the arena, and loot as the last lever on
the death curve.*

1. **The budget unit (change LE-1).** Three ledgers, in a fixed order:
   - **Ledger A, the count:** the approved count of notables per channel
     (ENM 15.3 step 1, **settled**; 6–8 per floor including the boss
     signature *(illustr.)*). It is the clutter and click contract and
     never grows. An offer set (choose 1 of N) counts as 1. (Read and
     kept by change LP-11; 7c's click table fills it, loot sources
     design 4.6.)
   - **Ledger B, value:** the VP of the notables, C1 / U2 / R3 / VR4
     (ITEMS 12.5–12.6, **settled**), fixed per floor by a **rarity
     deck**. It moves only by the **step allowance** (at most +2 per
     floor, fixed by which sockets exist at generation).
   - **Ledger C, coin:** a fixed per-floor pool; salvage is a share of
     it.
   - Sundries, supply and the DH healing package sit outside the count
     and inside the budget (CN-19, **settled**): reported, never traded
     against notables. Keys are access, worth 0 VP.
   - **Gold-equivalent (VP × 20) is a report view only**, not the master
     unit: prices carry category permilles and the Leadership discount,
     so coin is derived from value, never the reverse. A separate "power
     budget" is rejected: power = VP × **kPC**, the encounter-point worth
     of one equipped VP on a party character, which the M2 simulator
     measures before any deck is tuned (6.5).
2. **How loot is split (change LE-17).** Gear (weapons and foci, armour
   and shields, trinkets) is about 85% of field VP on F1, falling to
   about 65% on F5; the rest is scrolls, wands and notable flasks; the F5
   tactical share is capped at 40% of F5 field VP *(illustr.)*. These are
   smart-loot **weights**, never fixed slots. Healing is the DH package,
   untouched. The coin pool is about 15–18% of a floor's notable value
   in gold-equivalent (VP × 20) on F1–F4 and 0 on F5.
3. **Rarity curves (changes LE-2, LE-3, LE-6).** One deck per floor, with
   2 equal-VP variants where the gates allow: F1 `C C U U U` (8); F2
   `C U U U U U` (11); F3 `U U U R R R` (15); F4 `U R R R R VR` (18); F5
   `U R R R R R` (17) plus the Viaticum *(illustr.)*. Front-loaded
   count, back-loaded value: an item found on floor N serves about 5 − N
   floors. Boss floors: the signature takes the top tier **that has a
   legal record for its category** plus its authored property; the boss
   reward card is one counted pick at C / U / R / R (F1–F4), never VR.
   Camp stock is graded for the floor it serves and never VR.
4. **Coin sources and sinks (changes LE-7 to LE-9, LE-16).** Coin pool
   35 / 50 / 70 / 90 / 0 per party *(illustr.)* at companion cap 3, keyed
   by the expected-party table at the active `companionCap` (35 / 50 /
   70 / 75 / 0 at cap 2), never read from the actual party **(agreed,
   Q4)**; salvage a 12% pre-assigned share of that pool **(agreed, Q3)**;
   Sell 250‰ (from the illustrative 400‰). Sinks are the approved vendor, the notables shop (2 entries at every
   camp), the conditional Wax Key and, M3, the Ticket lock. Hire is free
   (CMP 7.8, **settled**): coin never buys bodies. Camp 4 (the Last
   Hollow) is the spend-down camp; F5 pays no coin **(agreed, Q1)**. New
   camp sinks are **hooks** for the camp and hub owner, with no prices
   from 7a.
5. **Guarantees and no dead runs (change LE-13).** The DH package stays
   exactly as approved (client numbers). Added: **the Dream's Due**:
   every floor, unconditionally, one announced strict upgrade on the main
   path with no check, never carried, never substituted. Target: the hero
   on F1, F3 and F5; the member furthest below the gear curve on F2 and
   F4, scored on a fixed deal of the party's items so one item never
   covers four bodies. It is **not** the pity DH 5.4 rejects: nothing
   hidden, no HP trigger; same seed and party, same Due (told plainly,
   with its one steering route stated). Value bad luck and runaway luck
   are removed by construction (the deck). No pity counters.
6. **Party size (changes LE-7, LE-13).** **Per party**, as approved (CMP
   7.7 items 1 and 4; ENM 15.3). A solo hero and a party of 4 get the same
   deck and the same coin **(agreed, Q4)**; recruiting is never a loot
   decision. Model A's Berth entries are rejected. Thin gear per body is
   handled by fit (the Due's alternation, fit premiums, the camp channel
   of 2, and the CMP-30 `anyCompanion` lever when its report fires), not
   by volume.
7. **Risk premiums (changes LE-10, LE-11).** Paid **inside** the budget:
   - **allocation** first: the best deck cards are dealt to the riskiest
     sockets, with only fixed-count shuffles on `lootPlan` (6.9 item 2);
   - the approved "+1 rarity step" second, from the **step allowance**
     of at most 2 per floor, fixed by which sockets exist;
   - a **fit premium** where a tier gate blocks the step;
   - **choice width** in offer sets.
   No swap-down tax on the main path. A Wax Key opens a consequence cache
   safely but takes its ordinary identity **(agreed, Q2)**; so do the
   spell routes that skip the consequence (Unbolting, Unweaving). Told
   plainly: in the toy no loot premium inside a fixed budget repays death
   risk (the elite room: −17 win points against +0.8 from loot), so the
   incentive to take risk comes from encounter and experience tuning
   **(agreed, Q6)**, flagged to the ENM, EP and DH owners.
8. **Determinism (change LE-14).** Everything is fixed at floor
   generation: deck, variant, allocation, steps, the Due, coin lumps,
   dreamed options, the Wax-Key identities, camp stock. Nothing rolls at
   open, kill, Steal or save. Every new draw runs on a **`lootPlan` child
   stream** in a **LootPlan** step just before ENM 10.5 step 3, and step
   3 keeps its approved order, now as keyed per-floor, per-entry blocks
   (change SV-5; M1; seeds and validation design 4.3); a draw-free **LootBind**
   pass after TL's `trapsResolve` settles what depends on placed units
   and resolved locks (salvage, the purse, coin lumps) and remaps any
   socket that pass changed. Floor N+1's generation is committed in the
   same save write as camp exit, closing a reload-and-rehire leak the toy
   found.
9. **Meta hooks (change LE-19).** Nightmare tiers and the ladder change
   nothing in the economy (harder is not richer, RT-21). Omens never
   touch the spoils list (LootDoorInvariant, RS 6.4). Unlocks enter at
   existing tier budgets with smart-loot weight normalised per `requires`
   lane, so they never dilute an on-build find. Dream Fragments and
   heirlooms are 0 VP. The Perilous Door reveals a pre-drawn fourth
   dreamed option **(agreed, Q5)**, read as the Perilous floor's boss
   reward card (ENM 17.5), shown before its camp; because that reading
   names a different screen from the agreed words "that floor's camp
   card", it was put to the client, who accepted it on 2026-11-22
   (6.12, 18.1).

### 0.2 The client's six decisions (agreed, 2026-11-22)

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Should floor 5 pay before the arena? | **A.** F5 coin and salvage 0; no camp channel after F5; no final reward card; the final boss carries and uses its signature, which drops as a 0-VP **Memento**; its share becomes the **Viaticum**, a choose-1-of-3 of boss tools before the arena; the Last Hollow is the spend-down camp (6.11) | B: keep F5 as approved (its coin, card and signature arrive after the last fight: dead value, told plainly) | LE-5 ⚑ |
| Q2 | Should a Wax Key still buy the consequence-lock premium safely? | **A.** The cache holds two pre-rolled identities; roll or force pays the stepped one, a Wax Key the ordinary one (6.9) | B: the consequence still fires on a Wax Key (changes TL 6.11's table); C: leave it (the Wax Key becomes a must-buy without Mechanics and K2 bounds nothing) | LE-11 ⚑ |
| Q3 | Should salvage become a share of a fixed coin pool? | **A.** 12% of the floor pool, pre-assigned to armed units; the per-unit line still shows; Cowed groups pay none (6.4) | B: keep the per-unit formula (read literally with the 10–15% ratio, about 2,000+ coin per run) | LE-8 ⚑ |
| Q4 | Should coin follow the party? | **A.** Coin per party from the expected-party table; solo and full parties get the same coin (CMP 7.7 holds) (6.4, 6.8) | B: +15% per actual companion (pays hiring; changes the budget under a shortfall) | LE-7 |
| Q5 | What does the Perilous Door pay in loot? (M3) | **A.** It reveals the pre-drawn fourth shadow option on that floor's camp card; draws are door-independent; the RS owner confirms (6.12; "camp card" read as the Perilous floor's ENM 17.5 boss reward card, choose 1 of 4, accepted by the client on 2026-11-22, 18.1) | B: a profile mark and a memento-wall frame only | LE-19 ⚑ |
| Q6 | If the M2 simulator still shows the elite room and the Annex as a large net loss, where does the fix come from? | **A.** From encounter and experience tuning (ENM 15.2, 16; EP; DH 18.2); loot stays fixed, paying risk only in allocation, the elite intel line, tier pips and choice width (6.9) | B: raise the step allowance to 3 on floors with an elite room or Annex | — (a request to the ENM, EP and DH owners, 17) |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Model A (fixed spoils list) as the base, trimmed: no Berth entries, no early Rare on F2, no fixed class quotas; grafts from B (fit fallback, targeting) and C (counted boss card, offer sets) | M1/M2 | lead's call |
| 2 | Three ledgers; the count stays the contract; VP exact per deck plus at most 2 steps | M1/M2 | lead's call; LE-1 ⚑ |
| 3 | Rarity decks with 2 equal-VP variants where the gates allow, on a `lootPlan` child stream; exact-VP variant shapes (6.3) | M1 (F1), M2 | lead's call; variant shapes new lead's call; LE-2 ⚑ |
| 4 | Boss channel: signature at the top legal tier for its category; reward card counted in Ledger B and caps, outside Ledger A, never VR | M2 | lead's call; LE-3 |
| 5 | Offer sets count once; unchosen options are **dreamed options** outside VP and caps | M2 | lead's call; LE-4 |
| 6 | Floor 5 pays before the arena: coin 0, no camp channel, no final card, the Viaticum, the Memento | M2 | **agreed, Q1**; LE-5 ⚑ |
| 7 | Camp channel of 2 at every camp 1–4, graded for floor N+1, never VR | M2 | lead's call; LE-6 ⚑ |
| 8 | Coin pool 35 / 50 / 70 / 90 / 0 per party at cap 3 (keyed by `companionCap`), lumps by risk rank after the locks resolve, F1 main-path floor ≥ 17 | M2 (F1 pool M1) | lead's call; **agreed, Q4**; keyed by cap: lead's call, after the check; LE-7 |
| 9 | Salvage as a 12% share of the pool, pre-assigned per fight, then per armed unit | M1 (F1), M2 | **agreed, Q3**; LE-8 ⚑ |
| 10 | Sell 250‰ (Sell 200‰ fallback) | M2 | lead's call (tuning); LE-9 |
| 11 | Risk Ledger: socket ranks, allocation, step allowance ≤ 2, fit premium, tier marks, no swap-down | M2 (locks, elite); M3 (rest) | lead's call; LE-10 ⚑ |
| 12 | A Wax-Keyed consequence cache yields its ordinary identity; the opening history selects (6.9 item 4) | M2 | **agreed, Q2**; LE-11 ⚑ |
| 13 | K2 made computable on a rule-derived expected Wax-Key value per floor; K2b band | M2 | lead's call; LE-12 |
| 14 | The Dream's Due, alternating hero / lowest member | M1 (F1 hero arms), M2 | lead's call, told plainly (DH 5.4 reading); LE-13 |
| 15 | Determinism: the LootPlan step on a keyed child stream, unchanged approved order, the draw-free LootBind pass after `trapsResolve`, atomic camp-exit commit, `LootLedger` | M1 | lead's call; LootPlan and LootBind: lead's call, after the check; LE-14 ⚑ |
| 16 | Treasure count and Grave Goods (run record) | M2 | lead's call; LE-15 |
| 17 | Heavy Purse on F2–F4 | M2 | lead's call; LE-16 ⚑ |
| 18 | Category weights by depth; F5 tactical cap; `foundSupplyPerFloor` pre-wired | M2 | lead's call; LE-17 |
| 19 | Elite intel line (a boss fact when the elite resolves or its notable is stolen) | M2 | lead's call; LE-18 |
| 20 | M3 meta hooks; the Perilous Door reveal (the fourth option drawn from M2, shown in M3) | M3 | **agreed, Q5**; the screen it names (the boss reward card) accepted by the client on 2026-11-22 (18.1); LE-19 ⚑ |
| 21 | The bait cache and the boxed lurker hold real +0 entries; the Annex has no prize of its own | M2/M3 | lead's call; LE-20 ⚑ |
| 22 | Twin Coffer | M3 | lead's call; LE-21 |
| 23 | Curses at a discount | Later; M4 by change HL-29 (agreed, heirlooms and curses Q5) | lead's call; LE-22 |
| 24 | V13 `LootEconomy`; reports; "loot last" in the DH 18.2 lever order; kPC first | M1 static, M2 | lead's call; LE-23, LE-24 ⚑ |
| 25 | Risk incentive fixed by encounter and experience tuning, not loot | M2 | **agreed, Q6**; request to ENM, EP, DH |
| 26 | Camp sinks (the Wish, the Tithe Bowl, the Ferryman's Note, Blood Price) passed as hooks, no ids, no prices; the Tithe Bowl since filled by segment 10 (change XI-24; M3; 8.1) | — | hook |

---

## 1. Intent

The approved documents fixed *that* notable loot is a fixed, curated,
pre-rolled per-floor budget with no junk, *how many* notables each
channel holds, *what* a rarity tier is worth (C1 / U2 / R3 / VR4), and
*that* coin exists, is run-scoped and is spent only at camp **(settled,
MAIN 4.6; ENM 15; ITEMS 10, 12)**, at a Ticket lock (M3) and, Later, at a
wandering trader **(agreed 2026-11-23, cross-document comparison Q-1;
ITEMS 10.1; cross-document comparison 2026-11-23, CC-8)**. None of them said what the budget is
measured in, how value rises by depth, how much coin a floor holds, what
"+1 rarity step" means against the depth gates, what pays for risk, or
what the last floor pays when no camp follows it.

The answer in one sentence: **every floor carries one fixed spoils list
(a count, an exact-value deck and a coin pool) dealt to sockets by risk,
with an announced upgrade every floor, so luck changes what you find and
where, never how much, and the power curve stays under the enemy depth
curve so deaths land where DH 18.2 wants them.**

---

## 2. Goals, non-goals, anti-goals

### Goals

- **Fixed totals, variable contents.** Value and coin per floor are exact
  per seed shape; what and where vary **(lead's call)**.
- **Guarantee fit, not rarity.** The felt bad luck is "nothing for me";
  the Due answers it every floor **(lead's call)**.
- **Pay risk in allocation and choice width,** inside the budget
  **(lead's call; agreed, Q6)**.
- **Coin sits below desire at every camp,** with a spend-down point
  before coin dies **(lead's call)**.
- **Loot is the last lever** on the death curve **(lead's call; LE-24)**.
- **Deterministic** at floor generation **(settled, MAIN 6.3; ENM 15.3
  step 5)**.

### Non-goals

- Shop and vendor presentation, stock scaling and new camp sinks: hooks
  to the camp and hub owner **(the client's brief)**.
- Item records, property tables and smart-loot weights: 7b.
- Socket tagging, placement and per-encounter caps: 7c.
- A meta currency, stash or coin interest **(agreed, ITEMS 10.1, 10.7)**.

### Anti-goals

- **Grind to win:** coin, Sell and trade never create items beyond the
  budget **(settled, ITEMS 2)**; killing more never pays more coin than
  the pool (6.4).
- **Pity timers:** hidden mercy keyed to low HP or dry spells **(settled,
  DH 2 anti-goals; DH 5.4 (f))**.
- **Hire-to-farm:** coin or loot that rises with the actual party
  **(agreed, Q4; CMP 7.7)**.
- **Dead value:** coin, cards or items that arrive when nothing can be
  done with them **(agreed, Q1)**.
- **Harder is richer:** ladder and Nightmare tiers never raise loot
  **(lead's call; RT-21, decided)**.
- **Second currencies, magic find, snowball items, items as currency.**
  **(lead's call)**

---

## 3. Constraints

### 3.1 What the parents already fix

- **Fixed budget, no grind.** Notable loot is a fixed per-floor budget,
  split by channel and pre-rolled on the loot stream; enemies carry part
  of it and never add to it (MAIN 4.6, **decided**; M-5). Kill count
  never raises loot or experience beyond a fixed amount (ENM 2,
  **agreed**). Coin, Sell and trade never create items beyond the budget,
  which includes the camp channel and, Later, the trader (ITEMS 2, 10.6).
- **The budget is a count** (ENM 15.3 step 1, **settled**): about 40%
  containers and secrets, 40% enemy-carried, 20% boss; 6–8 per floor,
  2–3 carried, plus 1–2 supply and sundries (outside the count, inside
  the budget, CN-19); a `camp` channel of 1–2 (I-10); M1's own split
  (containers, at most 1 carried weapon or armour notable, the
  signature). The boss reward card is a separate screen outside the
  3-per-encounter cap (ENM 15.1).
- **Draws** (ENM 15.3 step 2, **agreed**): smart loot by `requires`;
  owned and same-floor bases rerolled; family caps per run; at most 8
  rerolls, then a `substituted` supply or tonic; guaranteed draws never
  substituted (DH-27). The Reprieve Phial is one pre-roll per run
  replacing one notable draw (CN 14.7, **agreed**).
- **Rarity** (ITEMS 12.5, lead's call): C1 / U2 / R3 (F3+) / VR4 (F4+, at
  most 1 per floor); value = tier budget exactly; signatures "independent
  of rarity", top allowed tier plus an authored property, no cost add.
  `trinketRarePerFloorMax` 1 (a client number, TK); worn pieces (the
  trinket-kind armour pieces) ≤ U (AR 5.5; body armour and shields follow
  the ordinary gates; Rare body armour exists, AR 14);
  `scrollFloorCap` [1, 2, 3, 4, 4]; wands from F2; curses Rare+ (Later; M4 by change HL-29).
- **Value and prices** (ITEMS 10.5, 12.6–12.7): one VP unit for items,
  offers and enemies; k = 1 provisional; price = value × 20 × category
  permille; the Leadership discount on the hero only, ≤ 25%; Sell 250‰
  (6.4 item 6; the parent's illustrative 400‰ replaced as stale text by
  change SK-17, M2; starting kits design 2.3 F9) and Scrap 100‰
  *(illustr.)* on the undiscounted base, at one flat rate (**agreed**,
  kept). Supply 8, potion and tonic 12, Remedy 30, Wax Key 24 /
  28 / 32 / 36 by camp (TL 6.11). Pegs P1–P5, K1, K2 (CN 9.3).
- **Coin** (ITEMS 10.1–10.2, **agreed**): one run purse, spent only at
  camp, at a Ticket lock (M3) and, Later, at a wandering trader **(agreed
  2026-11-23, cross-document comparison Q-1; cross-document comparison
  2026-11-23, CC-8)**, lost at run end; found coin from containers and caches; salvage
  about 1 + level ÷ 2 per resolved armed unit, sized at 10–15% of found
  coin (ENM 15.7, I-11); Sell and Scrap a separate stream; starting coin
  **exactly 15** per hero class kit (`kit.coinStart`; change SK-1, M1;
  starting kits design 4.2), issued before F1 generation as
  `CoinChanged{source: kit}` and counted in `LootLedger` (change SK-3;
  starting kits design 4.1); companion kits carry no coin.
- **Camp** (ITEMS 10.3–10.4, **agreed**): a basic vendor at every camp
  (supply, limited pre-rolled potions, tonics and Remedy, bases, the
  conditional Wax Key, TL-25); an irreversible shop-or-reward choice; the
  camp channel is drawn in floor N's draw whether or not the shop is
  chosen; floor N+1 is generated at camp exit; unbought stock destroyed;
  no stash.
- **Healing** (DH 12.4–12.5, client numbers): kits 2 / 1;
  `healingSundryMin` 1 (main path, first half, no check, never carried,
  never substituted); `healingSundryMax` 3; `supplyMainPathMin` 1; the
  camp top-up; HE-7 reserves 2 no-check main-path containers. DH 18.2
  targets: F1 hero deaths < 5%; most deaths on F3–5 at elites and
  bosses; win rate 1/4–1/3, the no-healer arm ≥ 1/4.
- **Companions:** hire is free (CMP 7.8); the shortfall leaves experience
  and the per-floor loot budget unchanged, and "loot is shared among
  fewer bodies" is what a small party keeps (CMP 7.7 items 1 and 4);
  `kitBound` items sell for 0; `SwapChurn` ≤ one potion's price per run
  (CMP 9.3).
- **Premium hooks:** a consequence-lock cache "+1 rarity step inside the
  budget" (TL 6.11); the Dare cache +1 step (HZ 6.10); the Wager: 3
  distinct notables at +1 step, 1 of 3 / 1 of 2 / plinth by pips (TL
  6.15); the elite room's carried notable guaranteed (ENM 15.2); the
  Annex pays 0 experience and its reward is the Wager, Dare or box
  (EP-26, **agreed**); ≤ 20% of notables behind locks or traps and a
  no-Mechanics value loss ≤ 10% (TL 6.17).
- **Determinism:** loot is drawn only in ENM 10.5 step 3 with fixed
  counts ("the loot stream is consumed only in this step"); locked at
  generation; LootDoorInvariant (RS 6.4). Step 3 runs inside segment 6's
  order, after layout, hazards, TL's `traps` pass and step 2's slot
  plan, and **before** carrier reservation (step 4), composition (steps
  5–11) and TL's `trapsResolve` pass (ENM 10.5; EP 6.1; TL 6.22), which
  binds Keybearers, resolves Tattles, downgrades failing locks to Plain,
  reverts failing Wager Sites and confirms or drops boxed lurkers and
  the Annex.
- **Experience** fixed by band: pool 710 against a need of 540, clamp
  50–150%, catch-ups (ENM 16; RT-8).
- **Meta:** unlocks are content, never power (MAIN 4.7, **decided**);
  Nightmare adds kind, not stats (RT-21, **decided**); heirlooms are
  outside the budget (ITEMS 14).

### 3.2 What the approved documents owe this design

- the budget currency and any value total (ENM 15.3; ITEMS 22; MAIN 10
  "loot rates");
- rarity weights by depth, and per-channel counts;
- found coin per floor (**numbered nowhere**);
- what "+1 rarity step" means against minFloor, the VR cap and the Rare
  trinket cap (TL 6.11, 6.15; HZ 6.10; HZ 16 "Dare budgets and reward
  steps");
- K2's "mean value of a locked cache" (CN 9.3);
- the boss reward card's budget status (ENM 15.1);
- floor-5 economics;
- DH 21.5's camp-1 coin report (≥ 32); FL 6.8's forecast rests-gate
  lever "found supply 1 per floor";
- the phial pre-roll balance; `tier4ScrollsPerRunBeforeFinalBoss`;
- the simulator reports of ITEMS 19.3, CN 19.3, DH 21.5, TK 18.3 and TL
  11.4;
- the container contents of FL 13, TL 13 and EP 13 (the bait cache,
  activity containers, the Champion lair cache, boxed-lurker contents,
  Annex prizes).

### 3.3 Tensions found and their resolutions

| Id | Tension | Resolution |
|---|---|---|
| LX1 | The budget is a count; risk premiums and K2 need a value | Three ledgers; the count stays the contract (LE-1) |
| LX2 | "+1 step" against minFloor (F1–F2), the VR cap (F4–5), the Rare trinket cap, the thin M2 VR roster | Allocation first, allowance second, fit premium at a gate (LE-10) |
| LX3 | The salvage peg, read literally, implies about 2,000+ coin per run against about 100–200 useful spend per camp | Fixed coin pool; salvage a share of it (LE-7, LE-8; agreed, Q3) |
| LX4 | Supply sets healing; the FL forecast says the rests gate will fire; a higher supply price is not a lever (DH 5.7) | `foundSupplyPerFloor` pre-wired (LE-17); coin is never the rests lever |
| LX5 | Per-party count: solo rich per body, party of 4 thin | Per party kept (agreed, Q4); fit targeting (LE-13); camp channel of 2 |
| LX6 | Smart loot reads the party; LootDoorInvariant forbids theme input | Only party-side inputs; Omens never touch spoils (LE-19) |
| LX7 | The Steal guarantee and elite Steal < 15% against locks ≤ 20% | TL 6.17 kept; the Heavy Purse never on the elite (LE-16) |
| LX8 | Boss share 20% against the card and a VR signature on F4–5 | Card outside Ledger A, never VR; signature at the top legal tier for its category (LE-3) |
| LX9 | Shops are hooks, yet ITEMS, CN and TL fix vendor rows | 7a sets loot-channel counts and pegs only; presentation to camp and hub |
| LX10 | Pity rejected (DH 5.4) against "no dead runs" | Deck plus the Dream's Due, a standing announced rule (LE-13) |
| LX11 | Containers against clicks and the clock (FL 6.10, TL-38) | No new containers; the Due shares an HE-7 socket; `TallyChaseSeconds`. (Read and kept by change LP-1: the Windfall is a unit socket, so no container is added; loot sources design 4.7.6) |
| LX12 | Sell 40% + Scrap 10% pays hauling | Sell 250‰ (LE-9) |
| LX13 | K1 sits on its edge at the 25% discount (18 = 18) | Kept (≥ holds), told plainly; V13 warns on re-pricing |
| LX14 | F5 coin, Sell, final card and signature are dead value | LE-5 (agreed, Q1) |
| LX15 | A Wax Key opens a consequence lock with no consequence (TL 6.11), so it buys the premium risk-free | LE-11 (agreed, Q2) |
| LX16 | Every risk source is net negative in the toy; loot inside a budget cannot repay death | Flag to ENM, EP, DH (agreed, Q6); elite intel line (LE-18) |
| LX17 | Loot is drawn in ENM 10.5 step 3, before carriers, composition and `trapsResolve` fix the sockets it is dealt to (found by the check) | The LootPlan step on predicted sockets and the draw-free LootBind pass with named remaps (LE-14; 6.9, 6.10) **(lead's call, after the check)** |

### 3.4 Corrections to the brief and to the research (told plainly)

- **"Pilfer" does not exist** in any spec. The Shade has the universal
  Steal (ENM 15.5) and the *Light Fingers* upgrade.
- **Companion hire is free** (CMP 7.8): there is no hire-cost sink.
- **Pity timers are rejected** (DH 2 anti-goals and DH 5.4 (f): "hidden
  mercy keyed to low HP or dry spells").
- **Dream Fragments are M3 Lore readings giving boss intel**, not a
  currency (RS 9.1; TL-34).
- **The consequence-lock rule is TL 6.11**, not 6.12 (doors and secrets)
  as the brief and one research track cite.
- **"The Dreamer's Heart" exists**: it is the fixed F5 theme (RS 6.5,
  8.11; EP 7.10). A critic said otherwise; the tone line is kept.
- **`tier4ScrollsPerRunBeforeFinalBoss` is off by default** (`null`; CN
  18.1, 21): it is the lead's pre-agreed lever, engaged at 1 only if the
  scroll-nova reports fire, because `scrollReadSetsCampSpend` is `true`
  (CN-11, approved). The proposal read it as binding at 1; 6.3 and 6.11
  read it as "while the lever allows" **(new lead's call)**. (Change
  IR-16 later defaults it to 1 from M2 while the item rolls usability
  floor ships; CN 18.1; item rolls design 4.7 item 9.)

### 3.5 Terms

| Term | Meaning |
|---|---|
| **Spoils list** | One floor's loot plan: notable entries, the DH sundry and supply block, key entries and the coin pool (6.1) |
| **Ledger A / B / C** | The count of notables; their value in VP; the coin (6.1) |
| **Rarity deck** | The floor's fixed multiset of field-card tiers, with its variants (6.3) |
| **Field card** | A deck tier assigned to a field socket (container, secret cache or carrier) |
| **Socket rank SR0–SR5** | A socket's risk class, tagged by 7c (6.9) |
| **Step socket** | A socket an approved rule pays "+1 rarity step" (consequence lock, Dare, Wager; the Annex has no step socket of its own, 6.9) |
| **Step allowance** | At most 2 tier raises per floor, fixed by which step sockets exist (6.9) |
| **Fit premium** | A card kept at its tier, its identity drawn only from the target member's `requires` lane (a hard filter) (6.9) |
| **Offer set** | A choose-1-of-N presentation; counts 1 in Ledger A; unchosen options are **dreamed options** outside VP and caps (6.3) |
| **Dreamed option** | A pre-rolled option that is not taken (an offer set's unchosen options, the Perilous fourth option, the unused identity of a consequence cache); destroyed with `ItemDestroyed{dreamedAway}`. The client's Q5 wording calls it a "shadow option"; renamed because `shadow` is the Shade's offer lane (SH) **(lead's call, after the check)** |
| **The Dream's Due** | The floor's announced strict upgrade for a named member (6.6). "The Due" is prose shorthand only: UI text and keys always say "Dream's Due" / `dreamsDue`, clear of HZ's "due square" and "due overlays" **(lead's call, after the check)** |
| **LootPlan** | The generation step 3a, on `lootPlan`, just before ENM 10.5 step 3: it fixes every tier, socket, step and new identity (6.10) **(lead's call, after the check)** |
| **LootBind** | The draw-free pass after TL's `trapsResolve`: socket remaps, salvage shares, the Heavy Purse, coin lumps (6.10) **(lead's call, after the check)** |
| **Tier mark** | The 1–4 filled diamonds with the tier word that preview a step socket's tier (6.15; the client's "tier pips", renamed because pips already mean several things) **(lead's call, after the check)** |
| **Wax-Key identity** | A consequence cache's second pre-rolled item, paid when it is opened with no exposure to its consequence (a Wax Key, Unbolting, Unweaving) (6.9) |
| **Wax-Key value** | The rule-derived expected value a Wax Key yields at a floor's locked sockets, computed from data, never from a generated floor (6.4 item 9) |
| **The Viaticum** | F5's pre-arena offer set of boss tools (6.11) |
| **Memento** | The final boss's signature as a 0-VP run-record card (6.11) |
| **Treasure count** | The Stratum Card's count of field notables (8.2) |
| **Grave Goods** | The run-record line of what died with the hero (8.2) |
| **Heavy Purse** | 20% of found coin carried by one unit on F2–F4 (8.3) |
| **kPC** | The encounter-point worth of one equipped VP on a party character (6.5) |
| **`lootPlan`** | The child stream of the loot stream that carries every new 7a draw (6.10) |
| **`LootLedger`** | The saved per-floor record of the spoils list (6.10, 10.1) |

---

## 4. How other games handle loot economies

From the three research tracks (videogame loot, tabletop treasure,
reward psychology). Items marked *(recall)* were not web-verified;
Brogue's metering (source), Slay the Spire's rare offset and PF2e Table
10-9 were confirmed.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Slay the Spire | Fights pay about 10–20 gold, elites about 25–35, bosses about 100 *(recall)*; 3-card rewards; rare offset −5% +1% per common, reset on rare (confirmed); shop removal price rises per use | Income buys about 2–3 meaningful things per shop; rising-price sinks; elites as the visible premium | Hidden roll-history pity |
| Hades | Rewards previewed on doors; Chaos gates trade HP for a boon; Heat bounties are one-time | Preview the stake (tier marks); pay-in-blood as a camp hook; harder is not richer | Permanent stat meta (the Mirror) |
| Darkest Dungeon 1/2 | Provisioning before each expedition; late gold floods | "Buy supplies before descending" = our vendor; a spend-down point | Late gold floods |
| Into the Breach | One currency; rewards shown on objectives | One coin with clear sinks | Several currencies |
| Monster Train | Fixed gold per battle; branch rewards shown | Deterministic income lets players plan | — |
| Inscryption | Teeth, bones, pelts | — | Several small currencies |
| Dead Cells | Fixed Scrolls of Power per biome; cursed and timed premium chests | A guaranteed power budget per floor; opt-in risk sockets | Extra value for risk beyond the budget |
| Binding of Isaac | Guaranteed treasure / boss / shop per floor; pools empty on draw; devil deals | Structural slots; family caps (approved) | Max-HP trades (DH owns HP) |
| Enter the Gungeon | "Magnificence" lowers top-tier odds after top-tier finds *(recall)* | Bound high rolls too (the fixed deck) | — |
| Risk of Rain 2 | Chest cost scales with difficulty; about 79/20/1 rarity *(recall)*; +50% credits per player (partly confirmed) | Depth-scaled prices (the Wax Key's 24–36) | Linear per-player loot |
| Diablo 2/3/4 | Item level = monster level; smart loot; magic find | Smart loot (approved) | Magic-find stats; floods |
| Path of Exile | Currency = crafting orbs | — | Items as currency |
| Brogue | Per-kind frequencies rise per level and fall per spawn; depth guarantees; a food clock; choose-1 vaults (source-confirmed) | Constructive guarantees reading only generation state; choose-1 vaults count once | Player-state pity |
| DCSS | Item volume by depth; plentiful gold, random shops *(recall)* | Gold scarce against desirable stock | Useless late gold |
| Spelunky | Gold as score; a ghost timer | Time pressure against exhaustive looting (our floor-time gate) | Coin as score (rewards hoarding) |
| Wildfrost | Gold bonus for calling waves early | — (bookkeeping) | — |
| Balatro | Interest $1 per $5, capped *(recall)* | Save-or-spend tension only as an optional M3 hook | Default interest (fights spend-down) |
| BG3, DOS2 | Mid-game gold floods; barter and sell loops | Low flat Sell, no buy-back, finite stock (approved) | A high Sell rate |
| XCOM | Fixed income against more sinks than coin | Triage as the decision engine | — |
| FTL | Scrap Recovery Arm snowball; random stores | Guarantee a minimum offer | Economy snowball items |
| Loop Hero, Necrodancer | Meta resources | — | Meta currency (ITEMS 10.1, agreed) |
| Cogmind | Parts are loot and HP | Attrition insurance goes through potions and supply | — |
| D&D 5E DMG 2014 | Hoards by CR band; rarity gated by level | Rarity gated by depth (ITEMS 12.5) | Gold with nothing to buy |
| D&D 2024 DMG | About 100 items per party by level 20 *(secondhand)* | Budget per party per tier | — |
| D&D 4E parcels | Fixed parcels per level; ±1 parcel per PC; wish lists | A fixed multiset, assigned; a wish list as a camp hook | Linear per-PC scaling |
| PF2e Table 10-9 | Level 1: 175 gp, 40 gp currency; per extra PC +1 permanent, +2 consumables (confirmed) | Coin and items as separate ledgers | Per-PC scaling (CMP 7.7) |
| 13th Age | Quirks take over when items exceed level | A model for Later curses | — |
| OSR B/X, Shadowdark, Mork Borg | XP for gold; quality steps; scarce grim silver | Coin matters only when it converts; a purse that never feels rich | XP for gold (experience is fixed by band) |
| Blades in the Dark, Gloomhaven | A cap per downtime; loot tokens scale with level; limited shop copies | Capped sinks per camp; finite stock | — |
| Tetris 7-bag, PRD | Sampling without replacement | Exact totals, variable order (the deck) | Independent rolls |

**Cross-cutting lessons.** (a) Fixed totals with variable contents beat
independent rolls for short deterministic runs. (b) Guarantee **fit**,
not rarity: the felt bad luck is "nothing for me". (c) Premiums should
mostly be allocation and choice width, not extra value. (d) Bound high
rolls too. (e) Income sits below desire at every shop, with a spend-down
point before coin dies. (f) Sell-back stays low and flat. (g) Preview the
stake. (h) Harder modes need not be richer. (i) Normalise pools per build
tag so unlocks never dilute. (j) Avoid snowball items, magic find and
items as currency.

---

## 5. Economy models

### 5.1 The models

- **Z, the approved status quo:** a count per channel, independent rarity
  rolls, salvage per unit as written. The toy's reference.
- **A, fixed spoils lists:** per floor and party size, a list of entries
  (tier, class, channel, flags) with equal-VP variants; Berth entries
  [0, 1, 1, 2] per companion; an early Rare on F2; a coin pool; an
  upgrade entry; risk allocation.
- **B, value budget:** gold-equivalent as the master unit; rarity curves
  with a leash; coin as the remainder; a reserve for premiums; rarity and
  fit debt counters; a purse governor.
- **C, choice economy:** loot as picks; offers of width 2–3 at a wayside
  coffer, a vault, elite spoils, the boss card and camp; a fit rule.

### 5.2 Judges' scores (mean of three judges, 1–10)

| Model | Excitement | No dead runs | Choice | Curve control | Determinism | Party fairness | Fit with docs | Low build cost |
|---|---|---|---|---|---|---|---|---|
| A, fixed spoils lists | 5.7 | 9.0 | 6.0 | 8.3 | 9.0 | 6.0 | 7.0 | 7.0 |
| B, value budget | 5.0 | 8.0 | 5.0 | 7.0 | 7.7 | 7.0 | 5.0 | 4.0 |
| C, choice economy | 7.3 | 8.3 | 9.0 | 4.0 | 8.0 | 6.7 | 6.0 | 5.0 |

### 5.3 Verdict and corrections (lead's call)

**Model A as the base**, unanimous among the judges, because it has the
flattest luck curve in the toy (F3 gear deciles 0.88–1.03 in the steep
world, against 0.74–1.27 for Z), the cheapest generator (about 79 toy
lines) and the closest fit to MAIN 4.6 and ENM 15.3. Trimmed:

- **Berth entries dropped:** they push F4 to 9 counted entries and make
  hiring a loot decision against CMP 7.7.
- **The early Rare on F2 dropped:** the fit premium makes it needless.
- **Fixed class quotas dropped** for smart-loot category weights (6.2);
  A hoarded the most tactical items at death (83% of deaths).
- **Coin re-tuned** per the toy (9).

Grafted: B's blocked-step fallback ("one step's worth, never nothing")
and B's hero-aware targeting (reshaped as a standing alternation, 6.6);
C's counted boss card and general offer-set rule, and C's "the elite
shows its item in use" as presentation.

Rejected from B: gold-equivalent as the master unit, coin as the
remainder (a rich gear floor feels poor at camp), the leash (clamped
0.05× per run), the debt counters and the purse governor (no measurable
effect: win 0.307 against 0.303; they also contest DH 5.4). Rejected from
C as a base: it overpowers through selection (F3 party gear 18.7 against
12.5; win 0.351 against 0.31; even its deepest fix stays above A), draws
14–17 options per floor and costs 30–50 s of decisions per floor.

---

## 6. Rules in full

### 6.1 The budget unit and budgets (lead's call; changes LE-1, LE-2)

1. **The spoils list.** Each floor's loot plan is one spoils list, built
   at floor generation in the LootPlan step, before any identity is
   drawn, and settled draw-free in the LootBind pass (6.10):
   - (a) notable entries `{tier, channel, socket, flags}` (flags: `due`,
     `step`, `fitPremium`, `offerSet`, `phial`, `substituted`,
     `waxKeyIdentity`);
   - (b) the DH sundry and supply block, **exactly as approved**;
   - (c) key entries (Wax Key sundries; floor keys are access);
   - (d) one coin pool with its salvage share.
2. **Ledger A (count) is the contract.** Field entries plus the boss
   signature (or, on F5, the Viaticum) fall inside 6–8 per floor
   *(illustr.)*. An offer set counts 1. The boss reward card is
   **outside** Ledger A (it is a separate screen, ENM 15.1, **settled**)
   but inside Ledger B and family caps once taken (6.3). The camp channel
   is its own channel (ENM 15.3, **settled**).
3. **Ledger B (VP)** = Σ taken tier budgets, the signature's authored
   property reported as +1. Exact per floor for a given socket set; the
   band across seeds is base to base + 2 (the step allowance, 6.9).
   Taken VP can fall below base by 1 per consequence cache opened with
   no exposure (a Wax Key, Unbolting or Unweaving takes the identity a
   tier below), so the band of **taken** VP is base − (such caches) to
   base + 2: that shortfall is the agreed price of the safe route (Q2)
   **(lead's call, after the check)**.
4. **Ledger C (coin)** = the floor's coin pool (6.4), plus any logged
   fallback coin (6.4 item 8).
5. **Reporting views.** Gold-equivalent = VP × 20; sundries in
   VP-equivalent (price ÷ 20); `HealSupplyPerFloor` (DH). None is
   budgeted against notables.
6. **Touches:** ENM 15.3 step 1's sentence "Value comes only from each
   item's record" becomes "Each entry's tier is fixed by the floor's
   spoils list; identity and properties come from the record" (LE-1:
   step 1 carries change M-5, derived from the decided MAIN 4.6; the
   meaning of MAIN 4.6 is kept and strengthened).
7. **Curiosities add nothing to the ledgers (read by change EV-7; M2).**
   Segment 8's gains are facts, Graces, routes and Strands only: no VP,
   notables, rarity steps, offer widths, sockets, redraws or coin, so
   Ledgers A, B and C are unchanged (events design 4.4.1, 4.4.8).

### 6.2 Category split (lead's call; change LE-17)

| Floor | Weapons and foci | Armour and shields | Trinkets | Scrolls, wands, notable flasks |
|---|---|---|---|---|
| F1 | ~40% | ~30% | ~15% | ~15% |
| F2 | ~35% | ~25% | ~20% | ~20% (wands from F2) |
| F3 | ~30% | ~20% | ~20% | ~30% |
| F4 | ~30% | ~20% | ~20% | ~30% (Greater Fire Bomb F4+) |
| F5 | ~25% | ~15% | ~20% | ≤ 40% (final-boss tools, capped) |

*(illustr.)*, target shares of field VP, steered by smart-loot category
multipliers (`loot.categoryWeight[f]`), never fixed slots; a multiplier
steers the expected share, it does not enforce it (`LootMixByCategory`
reports the result). 7b applies per-category clamps after the tier: an
illegal (tier, category) pair re-weights the category, never the tier.

- **Weight composition (lead's call, after the check; made concrete by
  change IR-3, item rolls design 4.2; M2).** For every weighted identity
  draw, a legal record's weight is an Int64 computed factor by factor in
  a fixed order, with a FloorDiv after each factor:
  `categoryWeight[f][group]`, split **lane > family > record** (divided by
  the number of lanes of the group, of families in the lane and of
  records in the family, each counted on the **static legal set** at
  that tier, never on the survivors); × `requiresBias` if `onParty`
  (else 1); × `anyCompanion`; × the usability factor; × the socket terms
  (`showsInUse` ×2 on carried cards, IR-12; the null Scion pull lever,
  IR-18). The "lane" is the record's one `lootLane` from the closed
  `loot.lanes` (change IR-1; M1 data on F1 records, M2 full); the four
  categories above are the category groups, over 7b's fine categories.
  `requiresBias` is 3 by default and 4 for the boss card's options; on a
  targeted socket it is evaluated against the target member alone;
  `anyCompanion` is 1 unless the CMP-30 lever has fired (CMP 7.7, 18 risk
  10), and from M2 the lever has a party-size key (≥ 3, IR-17). **Usability
  floor (change IR-2; agreed, item rolls Q1; M2):** one `UsableBy` query;
  **hard** on every chosen, guaranteed or VR draw (the Due, fit premiums,
  the arms floor, every offer option and the fourth, the Viaticum, both
  dual identities, camp entries, VR cards); **soft** on C/U/R field cards
  (×100‰ *(illustr.)* when no member can use the record) (item rolls
  design 4.3). **Filters are not weights:** the Dream's Due, a fit premium
  and the arms floor below restrict the candidate set to their target
  (member, slot or lane) first, and the weights then apply inside that
  set.
- **The F5 tactical cap** is enforced during the F5 field draws in
  channel order: once tactical VP drawn so far + the next card's VP would
  exceed `FloorDiv(f5TacticalMaxPermille × F5 field VP, 1000)` (6 of 17
  at 400‰ *(illustr.)*), the tactical categories take weight 0 for the
  rest of the floor. The Viaticum is excluded from the base and the
  count. **Beside it on F3–F4 (change IR-16; M2):**
  `rareTacticalFieldMaxPerFloor` 2 *(illustr.)*: once that many Rare
  scrolls, wands and notable flasks are drawn in field draws, Rare
  tactical records take weight 0 for the rest of the floor, in channel
  order (item rolls design 4.7 item 9).
- **Per-run arms floor:** by the end of F2 the hero has been offered at
  least 1 weapon-or-focus and 1 armour-or-shield notable it can use. The
  F1 Due covers arms. **Evaluated at F2's LootPlan (lead's call, after
  the check):** if F1's `LootLedger` holds no armour-or-shield notable
  the hero could use, the first F2 container card in allocation order
  that is not the Due is constrained to armour-or-shield for the hero,
  drawn like the Due (its identity on `lootPlan`, 6.10; the parent ghost
  draw is retired by change SV-5, M1).
- **Healing:** the DH package only. Offers never contain healing
  potions.
- **Keys:** Wax Key sundries stay at most 1 per floor from F2 (TL 6.11,
  **settled**), outside the `sundriesPerFloor` doses (a reading, told
  plainly).
- **Information counters (M3; changes XI-23, XI-26):** the Lodestring
  and the Ear Trumpet (conditional, agreed exploration and information
  Q3) are party counters (cap 1, size 0), never notables, deck cards or
  scrolls, so they sit outside this split and the `sundriesPerFloor`
  doses (exploration and information design 4.5.2, 4.6.2).
- **Supply:** the approved 1–2 per floor, pre-wired as
  `foundSupplyPerFloor`, so DH 5.7 lever 1 (FL 6.8's forecast) is a
  tuning change only.
- **Charm lane (hook; change CA-25; after CG1/CG2; charm abilities
  design 4.10):** catch charms sit inside the unchanged Trinkets share
  and are priced in the camp notables shop by the existing rule (read).
  *Future item (IR/LE owners + client):* if CG1/CG2 show P(0 charms)
  above ~25% *(illustr.)*, a global charm-lane weight change inside the
  unchanged trinket VP share, never theme-keyed (agreed IR Q4) (18.2).
- **Report:** `LootMixByCategory`.

### 6.3 Rarity curves (lead's call; changes LE-2, LE-3, LE-4, LE-6)

**Field decks** *(illustr.)*, dealt onto the field sockets in the
LootPlan step by the one allocation algorithm of 6.9 item 2 (by tier and
socket rank, with fixed-count within-rank shuffles on `lootPlan`). Each
floor has a binding **base variant** and, where the gates allow, an
**alternate variant** of exactly the same VP; one fixed draw on
`lootPlan` picks between them (1:1 *(illustr.)*).

| Floor | Base variant (binding shape) | Alternate variant *(illustr.)* | Field VP | Signature (M2) | Boss card | Ledger A | Floor VP |
|---|---|---|---|---|---|---|---|
| F1 | C C U U U | none: one variant | 8 | U + authored ≈ 3 | C (a tier-1 scroll), 1 of 3 | 6 | 12 |
| F2 | C U U U U U | **fit variant:** the same cards, one U a fit premium | 11 | U + authored ≈ 3 | U, 1 of 3 | 7 | 16 |
| F3 | U U U R R R | **spiky:** C U R R R R | 15 | R + authored ≈ 4 | R, 1 of 3 | 7 | 22 |
| F4 | U R R R R VR (the run's first VR beside a U) | **fit variant:** the same cards, one R a fit premium | 18 | R + authored ≈ 4 (VR + authored ≈ 5 once VR gear records exist) | R, 1 of 3 | 7 | 25 |
| F5 | U R R R R R | **spiky:** C R R R R VR | 17 | none (the Memento, 0 VP); the Viaticum at R ≈ 3 | none | 7 (6 field + the Viaticum) | 20 |

**Variant rule (new lead's call; told plainly).** "Spiky" means the same
VP concentrated into fewer, higher cards **where the depth gates allow
it**. Both variants of a floor are **exactly** equal in VP and legal at
every gate (V13s (b)); there is no 1-VP tolerance. The proposal's draft
cells for F2–F5 did not all sum (its F3 spiky draft came to 13, not 15)
and leaned on "a remainder held in the allowance", which would have let
the allowance exceed 2 on some seeds against Q6. The fixed shapes are:
F1 one variant; F2 and F4 a **fit variant** (no exact legal spiky shape
exists: no Rare before F3, and F4 already holds its one VR); F3 and F5 a
true **spiky** variant (C U R R R R = 15; C R R R R VR = 17). Only the
base variants are binding shapes; the alternates are illustrative and
the builders may change them within V13s (b).

**Fit variant (lead's call, after the check).** The fit premium attaches
to the lowest-ranked card of the named tier (U on F2, R on F4) left after
the Dream's Due is reserved (ties by socketId). It targets the member
with the largest gear deficit under the Due scorer (6.6 item 2),
ignoring floor parity, and is drawn as any fit premium (6.9 item 3). If
the Due took the only card of that tier, the fit variant has no fit
premium and is logged; its VP is unchanged.

- **F1** is common-heavy and solo-tuned; its gear can never be a floor-1
  death lever (DH 18.2; 6.5).
- **F3** is the Rare turn; **F4** holds the run's first VR card.
- **M2 roster wall (told plainly).** The only M2 VR record is Icefall (a
  tier-4 scroll, family cap 2), and `tier4ScrollsPerRunBeforeFinalBoss`,
  when the lead engages it at 1, allows one before the final boss (CN
  13.4, 21; off by default, 3.4; **defaulted to 1 from M2 while the
  item rolls usability floor ships**, change IR-16, item rolls design
  4.7 item 9). So in M2:
  - the **F4 VR card is Icefall while its family cap, the VR
    `perFloorMax` and the lever allow, and only when a living member at
    generation can read it** (in class, or with trained Arcana, CN Q1)
    **(reader clause: lead's call, after the check)**; otherwise R + fit
    premium, logged in `SubstitutedVP`. A party with no arcane or primal
    reader never meets a VR it can only sell;
  - the **F5 spiky variant's VR card is R + fit premium** (the F5
    tactical cap and the Viaticum already carry the final-boss tools);
  - the **F4 signature is R + authored**;
  - a VR card drawn down to R logs its 1-VP shortfall in
    `SubstitutedVP` (Ledger B reports it; no coin is added);
  - **relaxation order (change IR-7; M2):** an empty draw first relaxes
    its socket's own target preferences, then the ladder rungs in order
    (lift Scion-line memory; category re-weight at T; this tier-stable
    path, VR cards only; the `substituted` supply or tonic), so the
    tier-stable path always runs before supply or tonic, and no tier ever
    changes silently (every change logged); guaranteed draws never reach
    supply or tonic (item rolls design 4.7 item 8).
  VR gear records are a 7b and ITEMS content request (2–3 records: one
  weapon, one armour or shield per lane group); until they exist, the F4
  field VR stays the only Very rare of the run.
- **Signature (LE-3).** "The floor's top allowed tier" (ITEMS 12.5) is
  read as the top tier **that has a legal record for the signature's
  category**, plus the authored property; 7b makes it exact as the
  highest tier in the floor's drawn deck variant, capped by the boss's
  authored forms (change IR-13; item rolls design 4.13). **The pending
  VR-cap exemption reading is withdrawn (change IR-13; agreed, item rolls
  Q5):** a VR signature **consumes its floor's VR slot** (the deck's VR
  card takes the tier-stable path, logged in `SubstitutedVP`).
  *Pending request to the ITEMS owner:* if the ITEMS owner refuses the
  slot rule, LE-3 stands as approved (signatures exempt from the VR
  `perFloorMax`, with 18.2's fallback). In M2 the question does not
  arise (the F4 signature is R).
- **Boss reward card (LE-3).** One offer set, width 3, at C / U / R / R
  on F1–F4, **never VR**; content per CN 10's boss-card row (scrolls,
  wands; the talisman from F3, **settled**). Counted in Ledger B and
  toward family caps once taken; outside Ledger A and the
  3-per-encounter cap (ENM 15.1, kept). No final-boss card (6.11).
  Offered in the arena after `BossDefeated`, beside the opened cache
  (read unchanged by the boss rooms design 4.11.2; change BR-28).
  - **Usability (lead's call, after the check).** "Usable" reads the
    party snapshot at generation (camp exit), the only party state any
    draw may read; a death on the floor does not re-roll the card. Outside
    their class, scrolls and wands need trained Arcana (CN Q1), so a solo
    Vanguard or a martial party with no Arcana could otherwise face a
    card with nothing usable. **Fallback (per option; change IR-8;
    agreed, item rolls Q1; M2):** each option with no usable record is
    drawn **on its own** as **usable gear at the same tier**, with the
    usability filter and ordinary weights (`requiresBias` 4, `onParty`
    over the whole party), **not** fit-filtered, on `bossCardFallback`
    entry = its option index (6.10), and logged
    `BossCardUsabilityFallback`. If that set is empty too, the offer
    terminal ladder runs (lift Scion-line memory for that option; a
    usable notable tool at the same tier; usable gear or a tool one tier
    lower, logged in `SubstitutedVP`); an offer option never falls to
    supply or tonic. **Hero guarantee:** if no option 0–2 is usable by
    the hero, the Theirs option is redrawn as hero-usable gear on
    `bossCardFallback` entry 4 (item rolls design 4.8). This widens CN
    10's boss-card row by a fallback only (LE-3).
- **Offer sets (LE-4).** Every choose-1-of-N (the boss card, the Wager
  cage set, the Viaticum, the Twin Coffer) counts 1 in Ledger A and its
  taken option's tier in Ledger B. Unchosen options are **dreamed
  options**: pre-rolled on `lootPlan`, outside VP and family caps,
  destroyed when the choice resolves (`ItemDestroyed{dreamedAway}`).
  **Narrowed for memory only (change IR-6; agreed, item rolls Q2;
  M2):** dreamed options are inside Scion-line memory, `oncePerRun` and
  gear floor keys, because memory reads what generation produced, taken
  or not (item rolls design 4.7). **Offer slots (change IR-8; agreed,
  item rolls Q1; M2; the Wager M3):** options 0–3 are the Yours, Theirs,
  Wayward and fourth slots, built in option-index order; families
  distinct, fine categories or effect tags distinct, at most 1 damage
  option on the boss card and the Wager (item rolls design 4.8).
- **Camp channel (LE-6).** 2 entries at every camp 1–4 (inside the
  approved 1–2), drawn in floor N's draw (**settled**), **graded for
  floor N+1**: tier and minFloor read N+1; family caps and owned bases
  read N (**settled**); the null Scion pull lever (change IR-18; M2
  lever) applies to camp entries as to F3+ field cards (item rolls
  design 4.14). Tiers: camp 1 U, U; camp 2 R, U; camp 3 R, U;
  camp 4 R, R *(illustr.)*. **Never VR**, so camp stock can never stack
  against the next floor's VR cap. The phial's minFloor at a camp reads
  the generated floor N, not N+1 (CN 14.7 unchanged; told plainly). No
  camp channel after F5. Where TL's "one camp notables slot" draws a Wax
  Key, it is a third camp display entry outside Ledgers A and B (keys
  are access; a reading of TL 6.11's channels bullet, told plainly),
  **shop side only** on the camp reward card: bought for coin, never the
  free reward pick, whatever the `choice` (change CH-15; M2; camp and hub
  design 7.1). Its
  presence is one fixed draw at `loot.campWaxKeyPermille` on its own
  `lootPlan` grandchild, always consumed, camps 1–4 **(lead's call, after
  the check)**. **Caps:** bought camp stock counts toward family caps
  (which read N, **settled**) but never toward floor N+1's per-floor tier
  caps (`trinketRarePerFloorMax`, `scrollFloorCap`, the VR `perFloorMax`),
  which bind only what floor N+1's own spoils list deals **(lead's call,
  after the check)**.
- **Run totals** *(illustr.)*: field + boss ≈ 95 VP over about 34
  counted entries; camp channel ≈ 4–20 VP taken. `CampVpShareOfRun` ≤
  about 15%. AR 5.1's 33–42 notables per run holds (cross-document
  comparison 2026-11-23, CC-50); AR 5.7's per-slot
  arithmetic is recomputed in the M2 simulator against these decks (a
  hook to AR).

### 6.4 Coin and sinks (shop hooks only) (lead's call; agreed, Q3, Q4; changes LE-7, LE-8, LE-9, LE-12, LE-16)

1. **Coin pool per floor** *(illustr.)*: **35 / 50 / 70 / 90 / 0**, per
   party **(agreed, Q4)**. It is set against the *expected* party's
   restock costs and **never reads the actual party** (CMP 7.7 item 1:
   coin is part of the fixed budget, ITEMS 2).
   - **Keyed by `companionCap` (lead's call, after the check).** The
     expected-party table is CMP 7.7's `expectedCompanions(floor)` at the
     active cap: 1 / 2 / 3 / 4 / 4 bodies at cap 3, 1 / 2 / 3 / 3 / 3 at
     the pre-authorised cap 2 (CMP 7.4; RS 10.1 calls cap 2 the
     predicted outcome). So `loot.coinPool` has one row per cap: cap 3
     **35 / 50 / 70 / 90 / 0**, cap 2 **35 / 50 / 70 / 75 / 0**
     *(illustr.)*. The cap is a build setting, not the party, so Q4 holds
     (told plainly after the check).
   **Which table:** the proposal carried two coin configurations, the
   pool as proposed here and the toy's tested **Hmid** fix (35 / 45 / 55
   / 65 / 0 with +15% per actual companion, Sell 250‰, camp channel 2;
   9). This document adopts **35 / 50 / 70 / 90 / 0 per party**, as the
   proposal recommended: Hmid's +15% per companion is option B of Q4,
   which the client rejected, and its lower flat pool was tuned against
   that uplift. Hmid stays recorded in 9 as toy evidence only; the toy
   must be re-run on this configuration before M2 (told plainly).
2. **Salvage share (LE-8; agreed, Q3).** 120‰ of the pool, pre-assigned
   at generation to the floor's armed units. Each resolved unit (killed,
   Yielded, Routed) pays its share on the fight's coin line (the per-unit
   display is kept); Cowed groups pay none, and their share is lost
   (**settled** rule, kept). Killing more can never pay more than the
   pool. The I-11 ratio (10–15%) holds by construction.
   - **When and over whom (lead's call, after the check).** Shares are
     computed in the draw-free LootBind pass (6.10), after `trapsResolve`,
     over the units in the final `GroupPlacement` that are armed and not
     summoned or joining later (alarm and Crier arrivals are counted with
     their home group). The Annex group counts (it is a placed fight); a
     boxed lurker does not (it is a container, TL 6.14).
   - **Per fight first, then per unit (lead's call, after the check).**
     A share of 4–10 coin spread over 10+ units would leave most units at
     0. So: (1) each encounter group with an armed unit gets 1 coin, in
     main-path order then by groupId, while the share lasts; (2) the rest
     is split over those groups by Σ armed level, largest remainder, ties
     by groupId; (3) inside a group, its coin goes to its armed units by
     level, largest remainder, ties by unitId. Every fight with armed
     units pays at least 1 while the share allows.
   - **Display (reading of "the per-unit line still shows", Q3, lead's
     call, after the check).** The loot summary's coin line shows the
     fight's salvage total ("Salvage: 2"), with one per-unit line for
     each unit whose share is non-zero; units with no share show no
     line, and a fight whose total is 0 shows no salvage line.
3. **Integer split (new lead's call).** Salvage = `FloorDiv(pool × 120,
   1000)`; found coin = pool − salvage. So *(illustr.)*: salvage 4 / 6 /
   8 / 10 / 0 and found coin 31 / 44 / 62 / 80 / 0.
4. **Found coin** is split into integer lumps over the **coin sockets**,
   in the draw-free LootBind pass after `trapsResolve`, **no draw**
   **(lead's call, after the check)**.
   - **Coin sockets** are containers and caches only (ITEMS 10.2: found
     coin comes from containers and caches), including HE containers
     beside their DH entries; never carriers, except the Heavy Purse
     and the Windfall (M3, change LP-1: an SR2 unit socket taking a
     weight-3 share, never a K2 lump; carved in LootBind after the
     Windfall is final, on the pre-escape pool, never re-carved; loot
     sources design 4.4, 4.7.6).
     Weights by rank: SR0 1, SR1 2, SR2 3, SR3–SR5 4
     (`loot.rankWeights`); minimum lump 3 (`loot.coinLumpMin`); largest
     remainder, ties by socketId.
   - **Carve order:** (1) on F1, the **main-path floor**: at least 17
     coin, split equally over F1's main-path coin sockets by largest
     remainder (so camp 1 holds ≥ 32 with the kit's 15, DH 21.5); (2)
     **lock lumps** (item 9); (3) **the Heavy Purse** on F2–F4 (LE-16,
     8.3): `FloorDiv(found × 200, 1000)` = 8 / 12 / 16 *(illustr.;* the
     proposal's table showed 9 on F2 before integer rounding*)*; (4) the
     rest by weight over the remaining coin sockets.
   - **Carve cap:** lock lumps + the purse ≤ 60% of found coin
     (`loot.carveCapPermille` 600 *(illustr.)*). A coin-only lock beyond
     the cap meets K2 with non-healing sundry doses in place of coin (1
     per 12 coin), inside the DH ranges; if the doses are not there,
     7c asks segment 5 to place fewer locks (TL 6.20's lock counts are
     ranges) **(hook)**.
   - **Overflow:** if the remainder is below 3 × the remaining coin
     sockets, the lowest-weighted of them (then the highest socketId)
     take one non-healing sundry dose instead of coin, one at a time,
     until it fits; when no dose is left, 7c removes that container (FL
     6.9 allows a leaf to hold a set piece or dressing instead), logged
     `CoinSocketTrimmed`. No container is ever left empty (FL 6.9
     `EmptyDeadEndRate` 0).
   - **Sundry doses used here** (lock sundries, overflow, F5 conversion)
     are doses of the DH-owned `sundriesPerFloor` block, inside its count
     and ranges: the DH 12.5 block draws them as approved, and LootBind
     only re-places them (non-healing doses first, in DH order); their
     approved placement draws (DH 12.5 step 7(c)) are still consumed and
     their results ignored for re-placed doses, logged **(lead's call,
     after the check; LE-12)**.
5. **Floor 5** **(agreed, Q1)**: pool 0, salvage 0. **The coin and
   salvage lines are hidden on F5** rather than shown at 0 (RC7:
   mechanics surfaces stay tone-free); the pressed-moth flavour ("in the
   Dreamer's Heart coins are pressed moths") is told once, on the F5
   Stratum Card (6.15) **(lead's call, after the check)**. **Every coin
   fallback on F5** (a waived Due, a fit premium with no lane) becomes
   sundry doses at 1 per 12 coin inside `sundriesPerFloor`,
   `offensiveSundryMax` and `healingSundryMax` (non-healing first); any
   remainder is logged `F5Unspent` (LE-5).
   - **F5 fillers (lead's call, after the check).** With no coin, F5's
     loot-bearing containers can hold only notables, sundry doses and
     supply. So: F5 has at most 1 non-notable lock; F5 loot-bearing
     containers ≤ F5 field notables + sundry doses + supply entries, net
     of what that lock uses; any further leaf is a set piece or dressing
     (FL 6.9), never an empty container (V13g (h)).
6. **Sell 250‰** (LE-9; the approved 400‰ is illustrative; the flat rate
   is **agreed** and kept). Scrap 100‰. Report `SellShareOfCoin`, target
   ≤ 40%; above it, Sell 200‰ before any change to the pool. Fewer
   notables are born unusable because the boss card's options carry the
   higher `requiresBias` (4 against 3) and the Dream's Due and fit
   premiums are filters (6.2).
7. **Sinks** (all owned by the camp and hub design; 7a states only
   targets and pegs) **(hook)**; **taken up by the camp and hub design
   (segment 13, approved 2026-11-23; changes CH-15, CH-16; its 7.1–7.3)**:

   | Sink | Status |
   |---|---|
   | Vendor: supply, limited potions, tonics, Remedy, bases | Approved (ITEMS 10.3); potion stock keyed to the party at floor-N generation and trimmed in Core (change CH-16; M2; 6.8); Quick Restock as one `BuyBatch` command (camp and hub design 7.3) |
   | Notables shop: the camp channel's 2 entries | Approved shape; count by LE-6. **The camp reward card (change CH-15; M2; camp and hub design 7.1):** reward = pick 1 of {entry 0, entry 1, the Reprieve Phial on a `campReward` phial camp}, one atomic `ChooseReward` command; or buy entry 0, entry 1 or both; the phial never on the shop panel; `choice` unset at Travel on dreams both entries and loses the phial; no new draw, no new VP, no further option |
   | Wax Key (vendor, conditional; camp slot; sundries) | Approved (TL 6.11, TL-25) |
   | Ticket lock (M3) | Approved (TL-32); a floor coin spend, allowed by ITEMS 10.1 as amended **(agreed 2026-11-23, cross-document comparison Q-1; cross-document comparison 2026-11-23, CC-8)**; peg proposal: price ≤ 60% of its cache's VP × 20 |
   | Trader and services | Later (ITEMS 10.9) |
   | Companion hire | **None**: free (CMP 7.8) |
   | Tithe Bowl (M3): a Lodestring or a Surveyor's Sketch | Filled by segment 10 (change XI-24; exploration and information design 4.6.3); Sketch 15 coin *(illustr.)*, Lodestring pegged at or above a healing potion at that camp (8.1) |
   | Candidate hooks (no ids, no prices): the Wish, the Ferryman's Note, Blood Price | Hook (8.1, 12) |

   **Coverage targets** (design intent passed to the camp owner): coin on
   arrival covers restock at every camp, for the largest legal party at
   that camp at both caps (restock costed per body, 7) **(lead's call,
   after the check)**; it covers 0.6–0.8 of a reference
   wish list (restock, a tonic per member, Remedy, one camp entry, the
   Wax Key at 50% weight) at camps 1–3 and 0.8–0.95 at camp 4; a party
   that skips optional restock can afford one camp-1 Uncommon (checked
   by the camp and hub design's `CampRestockCoverage` report at both
   caps; its 7.2, 9.2). Health
   signals: tonics ≤ 25% of spend (above it, a fun sink is missing and
   one camp hook should be promoted to M2: the Ferryman's Note first,
   then Blood Price, never camp 4; camp and hub design 5.4, 14.2); `CoinUnspentAtRunEnd` median
   ≤ 12 (reference) and ≤ 36 (solo; told plainly: the solo surplus is the
   approved "fewer bodies" compensation and can only buy capped goods).
8. **Fallback coin (new lead's call).** Where a rule pays "20 coin"
   instead of value (a waived Due, 6.6; a gate-blocked step with no fit
   lane, 6.9), the coin is **added on top of the pool**, logged in
   `LootLedger.coin.fallback`, and never on F5 (item 5). It replaces
   value the floor failed to deliver (20 = one VP's gold-equivalent), so
   the budget is not exceeded; V13g (i) checks the pool's own sum without
   it.
9. **K2 made computable (LE-12).** Floor N+1 is generated at camp exit,
   after any purchase, so no generated floor exists when the key is
   priced. **"The mean value of a locked cache on the next floor"** is
   therefore a **rule-derived expected Wax-Key value** per floor,
   computed from data **(lead's call, after the check)**:
   `E[WaxKeyValue(f)] = (Σ over the expected lock mix of f of lockValue)
   ÷ expected lock count`, where a coin-only lock's value is its lump +
   its sundry's price, and the notable lock's value is the price
   (undiscounted, VP × 20 × the category permille, averaged over
   `categoryWeight[f]`) of the identity a Wax Key would take there (for
   a consequence cache, its Wax-Key identity, 6.9). The expected lock
   mix and count come from TL 6.20's ranges at their midpoints
   *(illustr.)*.
   - **K2:** the **undiscounted** Wax Key price ≤ that value (the key is
     worth buying even at full price).
   - **K2b (new peg):** the Wax Key price **after the 25% Leadership
     discount** ≥ 50% of that value (the key is never a must-buy even at
     its cheapest).
   - V13s checks both statically for every camp 1–4 (camp 4 against
     F5). The realised per-floor value is stored in `LootLedger` as a
     report only, never a check.
   - **Lock contents rule (lead's call, after the check).** TL 6.17's ≤
     20% notables behind locks or traps binds: the quota is
     `FloorDiv(200 × Ledger A, 1000)` (1 on every floor at 6–8). It goes
     to the highest-priority locked or trapped socket: **the Wager
     first** (its set piece exists to pay), then a consequence lock, then
     any other lock, ties by rank then socketId. (A Dare cache sits behind
     a hazard, not a lock or trap, and is outside the quota.)
     Every other locked socket holds a coin lump plus 1 non-healing
     sundry whose total meets K2 (lumps ≈ 12 / 16 / 20 on F2–F4
     *(illustr.)*; F1 locks hold weighted lumps only, since no Wax Key is
     sold before camp 1).
   - **A coin-only consequence lock** pays its step in coin: its lump is
     the next lump step (16 / 20 / 24 on F2–F4 *(illustr.)*) when it is
     opened with exposure (6.9 item 4), and the ordinary lump when opened
     with none; the difference is withheld and logged
     (`LootLedger.coin.withheld`). It holds no second identity.
   - **F5** (no coin): the notable-bearing lock, if any, takes an R card;
     at most 1 other lock, holding 3 non-healing sundry doses (36),
     inside the F5 range; where that cannot fit, 7c asks segment 5 to
     drop the extra lock **(hook)**.
   - **K1** (Wax Key ≥ 2 × potion) holds at the 25% Leadership discount
     with equality (18 = 18). Kept, told plainly; V13s (j) warns if any
     re-pricing breaks it.

### 6.5 The power curve (lead's call; change LE-24)

| Floor entry | Hero level (ENM 16) | Depth % (ENM 10.1) | Hero equipped VP target, p50 | Party equipped VP target | Companions |
|---|---|---|---|---|---|
| F1 | 1 (3 by the boss, RT-8) | 80 | 0 (kit) | 0–1 | — |
| F2 | ~3 | 90 | ~3 | ~2.5 | 1–3 behind the hero |
| F3 | ~5 | 100 | ~5 | ~7 | |
| F4 | ~7 | 105 | ~7 | ~12 | |
| F5 | ~9 | 110 | ~8 | ~14.5 | |

*(illustr.)*. The party target closes about 50–65% of the depth and
monster-hit-rate pressure gap, leaving residual pressure rising from 1.00
to 1.25, so deaths stay on F3–5 (DH 18.2). Items stay build-shaping
sidegrades (ITEMS 2 goal) and never break +5 (STAT 2).

- **Told plainly:** at k = 1 every model over-closes by about 1.7–2.4× at
  F4 (toy party gear 20–28 against about 12), and the hero fills 4 power
  slots by F4. So **kPC is step 1 of the M2 simulator**, measured per
  ladder tier. **Measurement (lead's call, after the check):** for each
  floor and ladder tier, give the reference party +n equipped VP (n = 1,
  2, 4) and find the per-floor encounter-budget increase that restores
  the reference win rate; kPC = that increase ÷ n, averaged over n. If
  kPC ≤ 0.65, the decks stand; if kPC ≥ 1, lean the F2–F3 decks one card
  toward C / U; between 0.65 and 1, lean them by the linear share
  ((kPC − 0.65) ÷ 0.35 of one card, rounded to the nearer whole card per
  floor, F3 first).
- **Trip-wire (reordered after the check to follow DH 18.2):** if F3–5's
  share of deaths in the reference arm falls below 60%, deaths are
  landing early, which DH 18.2 sends to encounter levers first: (1) the
  DH 18.2 floor-1 and encounter-tail rows; then (2) the Due and fit;
  then (3) the camp channel's second-entry tier; then (4) lean the F2–F3
  decks one card; never the healing package.
- **Loot is the last lever** in the DH 18.2 order: enemy budgets and
  guard rows first; then the Due count and fit; then the decks. Loot is
  never a lever for F1 deaths (LE-24).
- **Gear power share:** the arm that leaves every notable behind wins
  35–60% as often as the reference. Above 85%, loot is irrelevant (raise
  property values); below 20%, luck decides runs (tighten variants).
- **Late finds:** `LateFindRelevance`, the share of F4–F5 notables
  someone equips or uses, ≥ 50% (the approved ITEMS 19.3 "≥ 70% after
  F2" will likely fail on F4–5; told plainly).

### 6.6 Guarantees and pity (lead's call; change LE-13)

1. **The DH package is untouched** (client numbers): its block, order,
   sockets and announcement (G6) stay as approved.
2. **The Dream's Due.** Every floor, unconditionally, one field entry is
   a **Due notable**. The scorer below is one deterministic procedure
   over the party snapshot at generation (camp exit) **(scorer: lead's
   call, after the check)**:
   - **The gear curve:** `loot.gearCurve[level] = {mainHand, offHand,
     armour, trinket1, trinket2}` in VP *(illustr.)*, read at each
     member's level. Consumables never count.
   - **The fixed deal (party-owned usable, reshaped after the check).**
     Every item the party owns (any member's slots or pack, heirlooms
     included) is dealt to members slot by slot, ignoring who holds it:
     slots in fixed order (main hand, off hand, armour, trinket 1,
     trinket 2); for each slot, the highest-VP unassigned item goes to
     the usable member (trained, Strength) with the highest level, then
     party order, whose slot is still unfilled; repeat until no item
     fits. **On equal VP, the higher tier is dealt**, so a held heirloom
     is the slot's dealt item (change SK-4; M3; starting kits design
     4.3). Each item fills exactly one member's slot, so one longsword
     never covers four bodies; a slot that gets nothing is **empty** for
     that member. Because the deal ignores the current holder, transfers
     between members change nothing (closes LB7; the V13g (g) property
     test). A `kitBound` kit is dealt only to its own member and counts
     0 VP, so a new recruit naturally draws the F2 or F4 Due.
   - **Deficit:** per member, Σ over the five slots of max(0, curve −
     dealt VP in that slot).
   - **Target, a standing rule:** F1, F3 and F5 → **the hero**; F2 and
     F4 → the living member (in the snapshot) with the largest deficit,
     ties to the hero, then party order. A solo hero is always the
     target.
   - **Slot:** the target's slot with the largest per-slot deficit, ties
     by the fixed slot order.
   - **What it must be:** a strict upgrade by the core comparison (ITEMS
     6.3; ENM 15.8) for that slot: usable, and a tier above the slot's
     dealt item, or filling an empty slot. **On F4–F5 only,** if no
     strict upgrade exists in any slot, a marked **sidegrade** (same
     tier, a `requires` lane or property line that no dealt item in that
     slot has) is legal, tagged "another way" (kept clear of RS's "the
     road not taken"). **7b's candidate test (change IR-10; M1 for the
     F1 Due, M2):** a **two-hander** must exceed both the dealt main hand
     and off hand, or find them empty; a **trinket** must exceed the
     lower dealt trinket or fill an empty trinket slot; the test reads
     fixed lines only. **Items sold or dropped at this camp count as
     still dealt** for the deficit and the slot test (*pending
     confirmation by the DH owner*; if refused, this item as approved).
     This covers sold hero kit pieces (a reading recorded by the starting
     kits design, its 4.3 and 13; pending DH as stated here).
     Inside the filter, `requiresBias` 3 is evaluated against the
     **target alone**. Held lever `loot.levers.dueSidegradeFromFloor` = 4
     (one data key, cross-document comparison 2026-11-23, CC-110; the
     approved F4–F5 sidegrade); moving it is a change to this item for the
     client and DH owner (item rolls design 4.9).
   - **Which card:** the lowest deck card with a tier above the slot's
     dealt item; else, if the slot is empty, the lowest card; else, on
     F4–F5, the lowest card at the slot's tier as a sidegrade; else the
     next slot in deficit order; else the waiver. The deck's VP is
     unchanged. **Except on F1** (the hero-arms rule below), where the
     Due always takes a U card, never a C.
   - **F1 hero arms rule:** kits are mundane, so the F1 Due is a U weapon
     or focus for the hero (combat is key; it answers the DH 5.8 floor-1
     choke point without touching healing). M1. V13g (g) checks it.
     **Heirloom precedence (change SK-4; M3; starting kits design 4.3;
     *DH and IR owners confirm*; widened by change HL-3 ⚑; M3;
     heirlooms and curses design 4.1.4; *DH, IR and SK owners
     confirm*):** a hero whose arms already hold U or better, **or who
     holds any heirloom (weapon, focus, shield or armour, a two-hander
     and the Burdened twin included)**, is treated as "arms satisfied",
     and the F1 Due takes the 20-coin waiver below; it never retargets
     (to armour or otherwise). The predicate is `armsSatisfied =
     heroArmsTier ≥ U OR the hero holds an instance flagged heirloom`,
     read from the issued instances after SK-12 placement, never from
     `boons[]`; the 20 coin sits in the Due's container slot, never as
     kit coin. Report `HeirloomSlotParity` (heirlooms and curses design
     9.3). *Pending request (changes CA-20 ⚑, CA-26; M3; charm abilities
     design 4.11):* the Bobbin boon's charm is flagged `boonCharm`, never
     `heirloom`, so it never sets `armsSatisfied` and never triggers this
     waiver (a charm is not arms); it counts as party-owned for the
     Due's owned and duplicate checks only (6.12; V13g (g)).
   - **Placement:** a main-path, no-check Plain container; an HE-7
     socket is fine (containers hold any number of items), and it may
     share that socket with the DH block. Never on a carrier. Never
     substituted. It takes the floor's first container-channel slot,
     constrained, with its fixed draw count; the approved DH block keeps
     its position (6.10).
   - **Waiver:** if no legal record exists for the target, log
     `DreamsDueWaived` and place 20 coin (fallback coin, 6.4 item 8; F5:
     sundry doses, 6.4 item 5). The Due never retargets.
   - **Announced** on the Stratum Card, for example "The dream owes
     Brask something." It names the target, never the item (surfaces in
     6.15).
3. **Why this is not the rejected pity (told plainly; restated after the
   check).** The DH anti-goal is a lead's call, so this is a lead's
   reading for the DH owner to confirm. The Due is announced, not
   hidden; it fires every floor whatever happened; its target is fixed by
   floor parity and the owned gear at generation, never by HP or a
   counter of dry floors; same seed and party, same Due. **Honestly
   stated:** because the F2 / F4 target reads owned gear, a party that
   declines pickups can steer which member the F2 or F4 Due names (DH 5.4
   (f) names "skip pickups" as the exploit it rejects). It is bounded:
   the Due's VP comes from the fixed deck, so steering changes who gets
   one card, never how much the floor holds, and declining finds costs
   the party those finds. This is part of the LE-13 reading for the DH
   owner. B's rarity and fit debt counters and the research "lean pity"
   and fit meter **are** dry-spell triggers and are rejected.
4. **Boss card and the Viaticum:** usable options by the generation
   snapshot, with the fallbacks of 6.3 and 6.11.
5. **No dead runs:** value variance is 0 by construction (the deck), apart
   from the agreed safe-route shortfall (6.1 item 3); fit variance is
   bounded by the Due. Reports: `HeroUpgradeDrought`,
   `HeroDreamsDueShare` (target ≥ 40%; the parity rule gives 60% at
   worst; at party size 4, ≤ 70% *(illustr.)*, so the F2 / F4 Dues do
   not drift to the hero), `DreamsDueWaivedRate`, `WinRateByLootLuck`
   (p10 ≥ 0.70–0.75× the base, p90 ≤ 1.25–1.30×), hero p10 / p50 at F3 ≥
   0.65.
6. **Class and lane fairness:** V13s (n) checks that every `requires` lane
   (made concrete as the record's `lootLane`, change IR-1; item rolls
   design 4.3) has
   at least 3 legal records at every deck tier on every floor
   *(illustr.)*; `RequiresHitRate` and `DreamsDueWaivedRate` are reported
   by hero class with a gap ≤ 10 points. 7b fills gaps.
7. **Levers, not defaults:** a second Due at party size ≥ 3 (counted
   inside the deck; `loot.dreamsDue.extraDueAtParty`, `null` by default).

### 6.7 Anti-hoarding and anti-flood (lead's call)

- **Flood is prevented by construction:** fixed VP and coin, family caps,
  no stash, Sell 250‰, F5 coin 0, unchosen options destroyed.
- **Coin hoarding:** the Last Hollow spend-down; no coin interest by
  default (interest rewards hoarding); the **Grave Goods** run-record
  line (8.2).
- **Consumable hoarding** (the toy: 2.9–3.3 potions and, in A and Z, a
  tactical item in 83–86% of deaths): the F5 tactical cap (6.2);
  Viaticum tools shaped for the arena (6.11); a hook to CN 8 so the
  companion "use it" suggester also fires for scrolls at elite and boss
  starts; an "unused since floor N" tag in the camp party view (segment
  10) **(hooks)**.
- **Reports:** `HoardIndex`, `TooGoodToUse`, `PotionsHeldAtDeath`. If the
  median is ≥ 2 potions held at death, the bottleneck is use, not supply:
  the suggester thresholds and potion-use UI move first, never the DH
  package.
- No new punishing rule.

### 6.8 Party size (lead's call; agreed, Q4)

- **Counts and VP per party** (CMP 7.7 items 1 and 4; ENM 15.3,
  **settled**). Berth entries rejected. A shared seed gives the same deck
  at every party size; identities differ only through smart loot, as
  approved.
- **Coin per party** **(agreed, Q4)**, from the expected party's table
  at the active `companionCap`, never the actual party (6.4 item 1).
  *Rejected (Q4):* B, +15% per actual companion: a hire-to-farm return
  against `SwapChurn` (CMP 9.3), and a shortfall would change the loot
  budget (CMP 7.7 item 1).
- **Thin gear in a party of 4** is answered by fit: the Due's F2 / F4
  largest-deficit target, scored on the fixed deal so one item never
  covers several bodies (6.6 item 2); the camp channel of 2; fit
  premiums targeting the largest-deficit member; and the CMP-30
  `anyCompanion` lever when its join-camp fill report fires (CMP 7.7,
  18 risk 10), not as a standing weight. **The lever gains a party-size
  key (change IR-17; M2 lever):** it can fire at party size ≥ 3 only
  (1500‰ *(illustr.)*), read by `CompanionTrinketEmpty` (item rolls
  design 4.3).
- **Hooks:** vendor potion stock scaling with party size (DH HE-6 thins
  to 0.70× at n = 4) goes to the camp and hub owner. **Taken up (change
  CH-16; M2; camp and hub design 7.2):** the stock is keyed to floor N's
  party count at generation (2/2/3/3 for parties of 1–4 *(illustr.)*), so
  a hire at the camp never changes it and declining a recruit is never
  rewarded; `loot/floor/{N}/vendorPotions` always draws `max(table)`
  entries and Core trims the stock to that count, discarding the rest
  (rules state, never merely hidden by the display); ITEMS 10.3's drawn
  `potionStock` range ([2, 3]) is retired.
- **Reports:** `GearPerBodyByPartySize`; the solo / reference win ratio
  (toy 0.29 / 0.32, passes). Revisit only if companions reach F3 entry
  with no notable.

### 6.9 Risk premiums: the Risk Ledger (lead's call; agreed, Q2, Q6; changes LE-10, LE-11, LE-18, LE-20)

1. **Socket ranks** (7c tags every socket):

   | Rank | Sockets |
   |---|---|
   | SR5 | Wager cage set, Dare cache (M3) |
   | SR4 | Elite-room carrier or lair cache; Strongroom (M3, F2–3) |
   | SR3 | Consequence-lock cache; for a consequence-locked Portal, the highest-ranked container in the room beyond |
   | SR2 | Padlocked, Catch or Keyed caches; secret caches; the bait cache; the boxed lurker (M3); the Keybearer cache; **the Windfall (M3, change LP-1; agreed, loot sources Q1):** a unit socket outside the lock quota, inside the premium share, with no step, created only in step 8 (item 2) |
   | SR1 | Off-path containers, Alcoves, optional-fight carriers |
   | SR0 | Main-path containers and carriers |

   **Readings (loot sources design 4.1, 13; to this owner):** "lair
   cache" here and in 6.14 means the *elite room's* SR4 cache; the
   main-path lair's container is SR0 (`lairContainer`). Gb guard-post
   carriers are optional-fight carriers (SR1). The Gaol Hour-lock cell
   cache is SR2, not SR3. The F5 **Viaticum** is **unranked** (boss
   channel; change LP-1): an offer outside the deal and the within-rank
   shuffles (6.11 item 5).

   **The Annex has no prize socket of its own (lead's call, after the
   check).** A boxed lurker is always the floor's Annex (TL 6.14; EP-26),
   and the Annex serves exactly one of the Wager guard, the Dare
   garrison, the boxed lurker or the Tattle beyond-room (EP 6.13). Its
   prize is the prize of what it serves: the Wager or Dare socket (SR5,
   with that socket's one step, never a second), the boxed lurker (SR2,
   a +0 entry), or nothing extra for the Tattle form (the room beyond
   holds what its own sockets hold).

2. **Allocation: one algorithm, in the LootPlan step (rewritten after the
   check; lead's call, after the check).** It runs on `lootPlan`
   (6.10) just before ENM 10.5 step 3, over the **provisional socket
   set**: layout's containers and caches, hazard sockets (the Dare),
   the `traps` pass's lock kinds, Tattle candidates, Keybearer slots,
   boxed-lurker sockets and Wager layouts, step 2's slot plan, and the
   carrier sockets predicted from step 4's fixed order (the elite room,
   the lair, guard posts by main-path index, the patrol). Step 4 takes
   no draw, so the prediction is exact; anything that later passes
   change is remapped by LootBind (item 7).
   1. **Split the deck by channel.** Carried cards = min(
      `loot.carriedPerFloor[f]` (2 / 2 / 3 / 3 / 3 *(illustr.)*; M1:
      at most 1 carried weapon or armour notable), the predicted
      carrier sockets, the per-encounter caps (1 on F1–2, 2 deeper));
      container cards = deck size − carried cards.
   2. **Reserve** the Due card (6.6) and the HE sockets. Allocation reads
      only the DH **socket reservations** (the HE-7 and HE-4 eligible
      sets, fixed by layout), never the DH draws, which come later on
      the parent stream (6.10 item 2). The Due may share an HE-7 socket
      with the DH block.
   3. **Order** sockets by (rank desc, socketId asc) and cards by (tier
      desc, deck index asc).
   4. **Hard filters, in this order:**
      - (a) **the locked-or-trapped quota** (6.4 item 9): only the
        quota's locked or trapped socket(s), chosen by that item's
        priority, receive a card in the deal; every other locked socket
        becomes a coin-and-sundry socket with no card; carried cards
        never count as behind a lock;
      - (b) **secrets:** at most 1 card, never the deck's top card;
      - (c) **the elite carrier** takes the highest carrier-legal
        **gear** card (weapon or focus, armour or shield, trinket); its
        identity is filtered to `showsInUse` records when that set is
        non-empty, so "presented in use" holds (elite preference, change
        IR-12; M2; reported in `EliteShowsInUseRate` and `EliteKpcDelta`,
        item rolls design 4.12). "Gear" here includes trinkets, so a
        per-option offer fallback may hold one (a reading recorded in the
        item rolls design, 4.8). A
        consumable is never the elite's card: a caster-tagged elite reads
        a carried scroll (ENM 15.4), so it would cast the run's best
        card at the party and destroy the premium. A consumable top card
        goes to the elite room's lair cache (also SR4) if one exists,
        else to the next socket in order;
      - (d) **trimmed floors** (RT-7): the top card on the elite or lair
        carrier, never an Alcove;
      - (e) carried cards only to carrier-legal subsets (ENM 15.3 step
        4).
   5. **Deal** the remaining cards in card order onto the remaining
      sockets in socket order.
   6. **Within-rank shuffles:** a Fisher–Yates over the cards on SR0,
      over SR1 and over SR2, each with a fixed draw count padded to
      `loot.maxSocketsPerRank` (12 *(illustr.)*); then one fixed draw
      that, at `loot.crossRankSwapPermille` (250 *(illustr.)*), swaps
      the best SR1 card with the worst SR2 card. So "locked usually
      holds better" is true but "locked always holds the second best" is
      not (LF4).
   7. **Steps** (item 3) are then applied in socket order (SR5, then
      SR3, then socketId) until the allowance is spent.
   8. **The Windfall card move (M3; change LP-1; agreed, loot sources
      Q1; loot sources design 4.7.6).** Draw-free, after step 7, so the
      step-6 shuffles and step-7 swap never touch it; the `windfall`
      socket is created here and is never in the steps 1–7 socket list.
      The `windfall` grandchild's 4 draws are taken at its start (6.10
      item 1). The first that exists: (i) the lowest-tier carried card
      of the host encounter, never the elite's, passing (f1) and (g),
      ties to the highest deck index; (ii) else the lowest-tier card on
      a plain SR1 container passing (f1) and (g), that container keeping
      coin plus 1 non-healing sundry; (iii) else no Windfall on this
      floor (its draws consumed, `windfallCarry` set). New filters:
      - (f1), card-level, at step 8: exactly 1 card; never the deck's
        top card or a VR; the Due, the DH block or the Viaticum; a key,
        a signature or a step card; a dual-identity card;
      - (f2), identity-level, at the parent draw: the Windfall's slot is
        drawn filter-first (6.10 item 1) with `oncePerRun` records
        excluded;
      - (g): never a card from a secret, the quota socket, a trimmed
        floor's Alcove reward, a Wager, Dare or Strongroom, or the elite
        carrier.

      The card is tallied in its **origin channel** with an `onWindfall`
      tag (ENM 15.3 split unchanged). Filters (a)–(g) are asserted
      **after step 8** ((f2) after the parent draw), by V13g and V15.
   - **Parent channel slots.** The parent stream's channel slot *i*
     (ENM 10.5 step 3) maps to the *i*-th socket of that channel in this
     allocation order; its ordinary identity is drawn there, filtered to
     that socket's tier (6.10 item 2). **The mapping is fixed at the end
     of step 7** (change LP-1): the Windfall keeps its origin socket's
     slot index *i*, and no slot is recounted after the card moves.
   - *Hook (a reading, to this owner; loot sources design 4.11):* the
     same latent shuffle issue exists, pre-existing, for secret filter
     (b) and the quota card at steps 6–7.
3. **The step rule (one reading of TL 6.11, TL 6.15 and HZ 6.10).** A
   step socket's card must be at least one tier above the **reference
   tier**: the lower median of the floor's SR0 ordinary card tiers,
   excluding the Due and the DH block **(defined after the check; lead's
   call, after the check)**.
   - **Allocation first:** if the deck holds such a card, the socket
     takes it; floor VP is unchanged.
   - **Allowance second:** otherwise the socket's card is raised one tier
     from the **step allowance**, at most 2 per floor, fixed by which
     step sockets exist at generation; floor VP rises by 1 per step (band
     base to base + 2). The allowance stays 2 on every floor **(agreed,
     Q6:** option B's allowance of 3 was rejected**)**. Raises are
     granted in socket order (SR5, then SR3, then socketId); a step
     socket after the second takes the fit premium.
   - **Fit premium at a gate:** if the raise would break minFloor, the VR
     cap or roster, `trinketRarePerFloorMax`, `scrollFloorCap` or worn
     pieces ≤ U (AR 5.5), the socket keeps its tier and its identity is
     drawn only from the `requires` lane of the member with the largest
     deficit under the Due scorer (6.6 item 2) (a hard filter;
     `StepFitPremium`). Only if no fit lane exists does it add 20
     fallback coin (6.4 item 8; F5: sundries). (Read, kept, by change
     IR-7: "no fit lane exists" reads as no usable record in the
     target's lanes at the tier; the socket then keeps an ordinary
     hard-usable identity and adds the 20 fallback coin; item rolls
     design 4.7 item 8.)
   - **No swap-down tax:** no main-path card ever drops to pay for a
     step. Judge 3's swap and the "Common becomes a sundry plus 20 coin"
     step-down are deleted (told plainly; they punish the safe player
     invisibly).
   - Told plainly: on F2 a consequence lock pays fit, not tier (Rare is
     F3+; the reference tier on F2 is C or U, and a U reference needs an
     illegal R); on F1 allocation alone usually pays the step (the main
     path holds C).
4. **The Wax Key and consequence caches (LE-11; agreed, Q2).** TL 6.11
   lets a Wax Key open a Crier, Barb or Tattle lock silently, without its
   consequence (the lead's answer BQ5, accepted). With allocation, that
   would buy the riskiest socket's premium risk-free. Rule: a
   consequence-lock cache that holds a card holds **two pre-rolled
   identities** on `lootPlan`:
   - the **stepped identity**, paid when the lock is opened by roll or
     force;
   - the **Wax-Key identity**, paid when it is opened by a Wax Key.
   - **Mixed routes (lead's call, after the check).** The core persists
     `consequenceExposed` and `openedVia` on the `SpoilsEntry`.
     `consequenceExposed` turns true on any pick try or force try made
     while the lock still has its consequence, and when a Crier is
     sounded on purpose (`crierSpent`). At opening, the stepped identity
     is paid if `consequenceExposed`, else the Wax-Key identity. So a
     failed pick or force followed by a Wax Key pays the stepped
     identity (the risk was taken); a Crier sounded and then picked as
     Plain pays the stepped identity; a lock opened by a Wax Key,
     **Unbolting** or after **Unweaving** turned a Runed lock Plain, with
     no earlier try, pays the Wax-Key identity.
   - **Unbolting and Unweaving pay the Wax-Key identity (lead's call,
     after the check; replaces the draft's new lead's call 9).** TL 6.11
     says Unbolting "opens it; no consequence", so paying the stepped
     identity would hand casters the premium without the risk, the leak
     Q2 closed. Both the Arcanist and the Herald have Unbolting (TL
     6.17), so neither route is a second free key.
   - **The Barb under force (told plainly after the check).** Q2's words
     are "roll or force pays the stepped one", and they are kept: a Barb
     never fires on force (TL 6.11), so forcing a Barb buys the stepped
     identity at the price of force's own noise (6 per try, up to 3
     tries). The TL owner may give the Barb a force consequence if
     `WaxKeyConsequenceUse` shows Barb forcing dominates (hook).
   - **Portal locks:** for a consequence-locked Portal, the two
     identities apply only to the single highest-ranked card socket in
     the room beyond (SR3); the room's other sockets are ordinary
     locked-room sockets under the quota (6.4 item 9).
   - **Wax-Key tier (new lead's call):** one tier below the stepped
     identity's tier, never below C; where the step was paid by a fit
     premium at a gate (no tier raise), the Wax-Key identity is the same
     tier drawn without the fit filter.
   - **Wax-Key sibling (change IR-11; M2):** the stepped identity is
     drawn first; the Wax-Key identity is filtered to the **same fine
     category and a different family**, preferring a different lane;
     if none, any category (logged `WaxKeySiblingRelaxed`). Neither
     identity may be `oncePerRun` (item rolls design 4.11).
   - The unused identity is a dreamed option outside VP and caps. **Lock
     card wording** (TL 6.11's contents hint; 6.15; changed by IR-11,
     item rolls design 4.11): "notable" becomes each identity's
     **fine-category noun** (weapon, armour, shield, focus, trinket,
     scroll, wand, tool), with the rarity word in its frame-shape
     glyph: a tier step reads "holds a Rare weapon; by key, an Uncommon
     weapon"; a fit step reads "holds an Uncommon weapon, chosen for
     <name>; by key, an ordinary Uncommon weapon"; after a relaxed
     sibling each identity shows its own noun ("by key, an Uncommon
     shield") **(fit wording: lead's call, after the check)**,
     the name read from the same generation snapshot the fit premium
     reads, so the premium is visible even when both tiers match. The
     key stays a safe, priced route; the premium pays only the risk.
     The opening history selects; it never draws (6.10).
   - A coin-only consequence lock pays its step in coin (6.4 item 9).
   - *Rejected (Q2):* B, the consequence still fires on a Wax Key; C,
     leave it.
5. **Caps.**
   - TL 6.17 binds: notables behind locks or traps ≤ 20% per floor (the
     quota of 6.4 item 9); a no-Mechanics value loss ≤ 10% (consequence
     locks can be forced).
   - Premium share: 35–50% of field VP behind optional risk.
   - **Lean route** (main path, guarantees, boss channel): 45–55% of
     floor VP *(illustr.; the toy's A sat at 0.60)*.
   - Secrets and trimmed floors: as filtered in item 2; report
     `LeanShareTrimmed`.
6. **Specific sources.**
   - **Elite room:** the highest carrier-legal gear card (item 2); the
     approved guarantee becomes allocation; presented in use during the
     fight (C's "try before you take", presentation only). **Plus the
     elite intel line (LE-18):** when the elite is resolved (killed,
     Yielded or Routed; cross-document comparison 2026-11-23, CC-38) or
     its notable is stolen, whichever comes
     first, the party learns one non-power boss fact for that floor (the
     boss signature item's card) **(trigger: lead's call, after the
     check)**. The fact is recorded in the floor's one **FactLedger**
     (fact instances and the `bossQueue`, shared with Yield
     interrogation, Curiosities, Fragments, Parting Words and the Tithe
     Bowl); if that fact is already known, the line takes the next
     unknown instance by the ledger's fall-through (change EV-8; M2;
     events design 4.4.2). Surfaces in 6.15. The intel line stays first
     in each floor's `bossQueue`, ahead of the Rendition, Guise,
     formation and Lore facts (read unchanged; change BR-29; M2; boss
     rooms design 4.12).
   - **Wager (M3):** 3 distinct records at the stepped tier, at most 1
     VR, the others fit premiums; counts 1; pips grade width (approved).
     It takes the locked-or-trapped quota first (6.4 item 9).
   - **Dare (M3):** the step card; sealed after two failures (approved), a
     real loss the player chose. Width by first attempt is left to the HZ
     owner (not adopted).
   - **Annex (M3):** 0 experience (approved); no prize socket of its own
     (item 1).
   - **Boxed lurker (M3, LE-20):** a +0 entry at SR2: its socket's
     container entry plus its carried item, read as its one carried
     consumable (a sundry dose), so no new carrier slot exists. Feeding 1
     supply hands over the container entry (approved).
   - **Bait cache (LE-20):** a real ordinary entry at SR2 ("the cheese is
     real"), so Mousetrap lures are worth taking; the separate prop family
     keeps ordinary chests honest (EP 6.8.1). A solo party never meets a
     Mousetrap (EP), so bait value is party-size dependent: reported.
     (Read: the bait cache and its placement are M2, with the Mousetrap;
     loot sources design 4.1, 13.)
   - **Strongroom (M3, F2–3):** SR4.
   - **Curiosities (segment 8; changes EV-7, EV-15; M2; events design
     4.4.1, 4.4.5):** a card may frame its room's container with
     dressing, never reshape it (`CuriosityLootBlind`); that container
     opens with no check whatever is chosen, and its contents are never
     an option outcome. No priced access to a socket that is otherwise
     free.
     - *Pending request (EV-15, to 7c and this owner):* a LootBind flag
       `stepFunded`; a card's priced extra route may open early only an
       ordinary lock with `stepFunded = false` (no value step under item
       3), never HE, the Due or a boxed lurker, never priced in ticks
       alone, contents unchanged. If refused: no priced lock routes
       (events design 14.2).
     - *Pending (EV-7, to this owner and 7c):* the F-CACHE fact (the
       contents of one optional, unstepped, non-HE, non-Due container)
       counted as exposure under this section, respecting CT2
       `ContainerArtBlind` and the `known` field. If refused, its card
       gains F-ROOM (events design 14.2).
     - *Pending (XI-22, to this owner; M3):* whether an F-TREASURE
       pointer (one room, tier word, qualifier; 8.2) counts as exposure
       under this section; recommended **not exposure** (no identity,
       weaker than F-CACHE). If ruled exposure, it is counted as ruled
       and the Lodestring price is re-pegged (exploration and
       information design 4.6.5, 14.2).
     - *M3 if HZ-23 (EV-36):* bypass routes only as skins of FL-generated
       Chutes (`GraphUnchanged`, `XpBandAfterEvents`; events design
       4.4.1, 9.2).
7. **LootBind remaps (lead's call, after the check).** After
   `trapsResolve`, the draw-free LootBind pass (6.10) applies these named
   remaps, logs each as `LootBindRemap` in `LootLedger`, and only then
   do the V13g checks run:
   - **A lock downgraded to Plain** (TP10, `KeyReachable`, the V11
     terminal fallback): the socket keeps its card. On a consequence
     cache, the Wax-Key identity becomes the only identity and the
     stepped identity a dreamed option; the allowance step it used is
     released in the ledger (`stepsUsed` − 1; nothing is redrawn). A
     coin-only consequence lock pays its ordinary lump.
   - **A Tattle resolved to a Crier:** no change (both are consequence
     kinds).
   - **A Wager Site reverted to a side room:** its first option moves,
     as a single item at its tier, to the lowest-id container in that
     room; the other options become dreamed options; the step stays
     spent and is logged.
   - **A boxed lurker dropped** (the socket becomes a real container,
     TL 6.22): it keeps its container entry, and its carried sundry dose
     joins the container.
   - **The Annex dropped:** what it served falls to its approved failure
     action (EP 6.13; TL 6.22) and the loot follows the rows above; a
     Dare whose garrison is dropped keeps its card.
   - **A carrier that falls back to a container** (ENM 10.5 step 8) or
     whose group moves (step 9, TP2): its card goes to a same-room
     container, keeping its tier (6.14).
   - **KinSwap (change IR-12; M2):** over the carried cards of one
     encounter, a single draw-free pass swaps the carriers of two cards
     of the same rank and tier only when the carriers' kin score
     (issue family 3, shield kind 2, armour category 1) strictly rises
     and both stay carrier-legal; identities never change, so
     LootDoorInvariant holds; each swap is logged `KinSwap`. A carried
     line the final carrier rejects is replaced, draw-free, by its
     pre-picked alternate line, else the same-room container fallback
     above (item rolls design 4.12). The Windfall's card is excluded
     from KinSwap and the carrier fallback (change LP-1).
   - **Offer pacing trim (change LP-30; M3):** at most 2 offer-form
     field sources per floor among the Wager, the Twin Coffer and the
     Viaticum; draw-free, after the reverts above: the Viaticum is never
     dropped; the Twin Coffer is dropped first (it reverts to a plain
     container keeping its Yours identity; Wayward becomes a dreamed
     option); then the Wager (the revert above) (loot sources design 7).
   - **A Windfall moved or dropped (change LP-1; M3):**
     `WindfallMoved{from,to}` when its host room can no longer hold it
     (the card returns to its origin as resolved above, then the
     draw-free step-8 source order is re-run on the next eligible host);
     `WindfallDropped` when no host passes (the card stays at its
     origin; `windfallCarry` set) (loot sources design 4.3 step 3).
   - **Salvage, the Heavy Purse and the coin lumps** are computed here
     (6.4 items 2, 4; 8.3).
   - **LootBind's numbered order (change LP-1; loot sources design
     4.3):** these remaps and the offer-pacing trim; container squares;
     the Windfall host check, move or drop; home, Rathole and route;
     container merges; Proprietor tags; coin lumps, the Heavy Purse and
     salvage (after the Windfall is final); then the V13g and V15g
     checks.
8. **Risk does not pay in loot (told plainly; agreed, Q6).** In the toy
   (no experience or skill modelled) the elite room costs 16–17 win
   points against +0.8 from its loot; experience does not pay either
   (pool 710 − 160 = 550 ≥ 540 need, and the catch-ups correct any
   shortfall). Loot inside a fixed budget cannot fix this at about 0.1
   win point per VP. **If the M2 simulator, with experience and a
   skilled arm, still shows the elite room and the Annex as a large net
   loss, the fix comes from encounter and experience tuning** (ENM 15.2,
   16; EP; DH 18.2): the elite room's death cost (budget, watcher rules)
   is the real lever. Loot stays fixed and pays risk only in allocation,
   the elite intel line, tier pips (the tier marks of 6.15) and choice
   width. This is a request to the ENM, EP and DH owners (17).
   `EliteNetWinDelta` and `RiskSocketNetWinDelta` (target −5 to +2
   points per source) are M2 release reports. *Rejected (Q6):* B, a step
   allowance of 3 on floors with an elite room or Annex.

### 6.10 Determinism and the loot stream (lead's call; change LE-14)

1. **The `lootPlan` child stream (keyed after the check; lead's call,
   after the check).** `lootPlan` = `loot/lootPlan/{N}` =
   `Derive(loot, "lootPlan", N)`, derived from the `loot` key, never
   from a counter or state (change SV-1; M1; the key algebra, path
   grammar and `Bound` of seeds and validation design 4.1–4.2), one per
   floor, so no two floors share a plan (the run-level phial pre-roll is
   on its own key, `loot/run`, a vector of always 3 draws, change SV-5;
   seeds and validation design 4.3). Every consumer draws from its
   own keyed grandchild, `loot/lootPlan/{N}/{kind}/{entryIndex}` =
   `Derive(lootPlan, kind, entryIndex)` (the label hashed by `LabelId`
   inside `Derive`; each a registry row, change SV-2),
   so adding a consumer (an M3 feature, a fallback) never shifts another
   consumer's draws. Every identity draw reserves **9 draws** (1 + the 8
   approved rerolls), consumed even when unused, so counts never depend
   on data (R-SEED-3; V13 (m) test).

   | Kind | Draws per entry | Entries |
   |---|---|---|
   | `variant` | 1 | 1 per floor |
   | `dealShuffle` | `maxSocketsPerRank` − 1 per rank | SR0, SR1, SR2 |
   | `crossRankSwap` | 1 | 1 |
   | `dreamsDue` | 9 | 1 |
   | `armsFloor` | 9 | 1 (F2; consumed on every floor, used only when constrained) |
   | `fitPremium` | 9 | per fit-premium entry |
   | `waxKeyIdentity` | 9 | per consequence cache with a card |
   | `bossCard` | 9 | 4 options (3 shown, the fourth a dreamed option) |
   | `bossCardFallback` | 9 | 5: entry = option index 0–3, plus entry 4, the hero-guarantee redraw (change IR-14; M2) |
   | `viaticum` | 9 | 4 options (F5) |
   | `viaticumFallback` | 9 | 5 (F5): as `bossCardFallback` (change IR-14; M2) |
   | `wager` | 9 | 4 options (M3) |
   | `twinCoffer` | 9 | 1 alternate (M3) |
   | `campWaxKey` | 1 | 1 per camp 1–4 |
   | `lineRoll:<source>` | 4: line, alternate line, element, wand charges | 1 per identity of every source, dreamed options and unused fallbacks included; keyed `entryIndex × 16 + optionIndex` (change IR-14; reserved M1, inert; used M2) |
   | `curseGate` | 2 | 1 per floor (change IR-20; reserved M2, used from M4 by change HL-29) |
   | `windfall` | 4: presence, host, pad, pad | 1 per floor, F1–F5, consumed on every floor, door, party size and theme, at the start of 6.9 item 2 step 8 (change LP-1; M3; loot sources design 4.7.10) |

   The table is the segment-11 hand-off, taken up as registry rows
   (seeds and validation design 4.3); entry indices follow the
   allocation order (6.9 item 2). *Pending request to this owner
   (segments 11–12; M2):* the V13g (e) re-deal's LR2 key,
   `loot/lootPlan/{N}/dealShuffle/retry1`, drawn as `dealShuffle`; if
   not confirmed, that key stands as written (seeds and validation
   design 4.3, 14.2). **Filter-first (change IR-14; M1):**
   each identity's filters are applied before its first draw, which
   picks the record; the other 8 reserved draws are consumed as padding,
   the same distribution as the reroll loop at a fixed count, locked
   before M1 golden seeds (item rolls design 4.1, 4.17).
2. **The approved ENM 10.5 step 3 order is unchanged**, now as the order
   in which keyed blocks are evaluated (change SV-5 ⚑; lead's call;
   approved by the client, 2026-11-23; M1): step 3 is keyed per floor
   and per entry block, `loot/floor/{N}/{block}/{entry}`, 9 draws per
   identity entry (filter-first), blocks in approved order:
   `containers`, `carried`, the boss channel, `trader` (Later), then any
   returned trader slots on `containers` (Later), `dhBlock` (the DH 12.5
   block), `resistant`, `wandCharges`, `camp` (the next camp's notables),
   `vendorPotions`, `traderService` (the trader's service draw, Later)
   (ITEMS 10.9; ENM 10.5; cross-document comparison 2026-11-23, CC-45).
   Approved positions become block addresses, so no
   block's draws move when another block's entry count changes (answers
   LB8, LK13; seeds and validation design 4.3). **What the blocks mean
   (lead's call, after the check; dummies retired by SV-5):**
   - the **counts** come from the deck and 6.9 item 2 (field 5 / 6 / 6 /
     6 / 6, camp 2); the former count draws are retired, not drawn;
   - **ordinary identities** are drawn there, channel slot *i* on the
     *i*-th socket of that channel in allocation order, filtered to that
     socket's tier;
   - the **boss channel** draws only the signature; every option, the
     first included, is drawn on `lootPlan`, and no parent draw is made
     for the options (read by change IR-13: the signature's form is
     chosen draw-free; item rolls design 4.13);
   - the **DH block** keeps its place in the block order, after the
     notables, and may share an HE-7 socket with the Due (6.6).
   *(Retired: SV-5 was approved by the client on 2026-11-23, so the
   refusal fallback, the sequential parent stream with padded blocks
   and a GS1 counter restore, no longer applies; cross-document
   comparison 2026-11-23, CC-108.)*
3. **The Due's slot (new lead's call; tier fixed after the check).** The
   Due takes the first container slot. That slot's former parent
   **ghost draw** is retired (change SV-5; M1): with keyed blocks there
   is no position to hold, so it is not drawn, and nothing is counted
   toward caps or same-floor bases for it. The
   Due's own identity is drawn on `lootPlan` (`dreamsDue`), and later
   items treat it as this floor's base for rerolls. The arms-floor card
   (6.2) is drawn the same way.
4. **Generation order (lead's call, after the check).** Layout →
   `hazards` → `traps` → ENM 10.5 step 2 → **SocketPlan** (7c,
   draw-free, builds the provisional socket set; change LP-1) → **step
   3a, LootPlan** (on `lootPlan`: variant, deal, steps, the Due, dual
   identities, offer options; 6.9 item 2; then the Windfall's step 8
   inside step 3a, M3) → step 3 (keyed `loot/floor/{N}` blocks in the
   approved order, change SV-5) →
   steps 4–11 → `trapsResolve` → **LootBind** (draw-free: remaps,
   salvage, the purse, coin lumps; 6.9 item 7, in its numbered
   sub-order, loot sources design 4.3) → the V13g checks at **AP2**,
   the shared floor assertion point after LootBind and EventPlace
   (change SV-9 ⚑; lead's call; approved by the client, 2026-11-23;
   M1). HE-7's structural point, **AP1**, runs before loot, after
   `traps`; one canonical pipeline GS0–GS17 places SocketPlan at GS9,
   LootPlan at GS10, step 3 at GS11 and LootBind at GS14, and F(N+1)'s
   `GenInputs` is frozen at Travel on from the camp-exit state (seeds
   and validation design 4.4, 5.4). LootPlan reads only the provisional
   socket set; LootBind reads the final one. ENM 10.5 step 3's "the loot
   stream is consumed only in this step" becomes "only in steps 3a and
   3" (LE-14). Segment 8 reads this order unchanged: EventDraw runs
   after layout, and the draw-free EventPlace after LootBind, before the
   V13g checks; neither reads nor changes loot (`NoEventLoot`; read by
   change EV-21; events design 4.10, 9.2).
5. **Nothing rolls** at open, kill order, Steal, pick or reload (ENM 15.3
   step 5, **settled**). The opening history selects between two
   pre-rolled identities (6.9 item 4); it never draws.
6. **Atomic commit:** floor N+1's generation and the camp-exit save are
   one write. The toy found that otherwise reloading camp and hiring a
   different Muster candidate rerolls that floor's identities (about 3
   rerolls of value). The Muster free cards, vendor stock and the camp
   channel are already fixed in floor N's draw (**settled**); card A
   resolves at camp entry on its own keyed `recruit/camp/{k}/gap`
   (CMP 7.3; text fix, change SV-31). The commit is write order WO5 at
   Travel on, F(N+1)'s own first safe point (read by change SV-14;
   seeds and validation design 4.8.3).
7. **Inputs from outside the stream:** the party's `requires` tags and
   owned bases (**settled**), plus, for the Due, fit premiums and the
   usability fallbacks, the same party snapshot at generation (owned
   gear, levels, training, living members) and floor parity (a reading
   of the same inputs). **Widened (changes IR-2, IR-6; M2):** the
   usability floor reads that same snapshot, and Scion-line memory and
   `oncePerRun` read the run-wide `LootLedger.generated[]` (generation
   outputs only, never player actions; signatures sit in
   `bossContent[]` and feed no exclusion) (item rolls design 4.3, 4.7).
   Nothing else: the purse, the theme and the door are never read.
   **Element Echo (change IR-19; agreed, item rolls Q4: held for an M3
   client decision):** an option, default off and false in release
   builds, that would let offensive element-choice lines read the found
   floor's elemental `counterType`; until the client adopts it, this
   item stands (item rolls design 4.4).
8. **`LootLedger`** per floor is saved (ITEMS 18.4) for reports and V13.

### 6.11 Floor 5 pays before the arena (agreed, Q1; change LE-5)

No camp follows F5 (ITEMS 9; the trader is F2–4 only). F5 coin, F5 Sell
value, the final boss card and the final signature are therefore dead
value; CN 14.7 already says the last boss card "comes too late to
matter".

1. **Coin and salvage 0** (6.4); every coin fallback becomes sundry
   doses.
2. **No camp channel** after F5.
3. **No final boss reward card.**
4. **The final boss still carries and uses its signature** (ENM 15.2,
   15.4). On death it drops as a **Memento**: a run-record item card
   with full stats, captioned "carried out of the Heart", 0 VP; from M3 a
   cosmetic memento wall in the profile (never power, MAIN 4.7).
   **Caption and surfaces (lead's call, after the check):** the draft's
   "what you took from the Dreamer" implied who the Dreamer is, which RS
   7.5 forbids; the caption is premise-neutral and joins the RS 7.4
   tone-by-depth review list. No loot summary follows the final kill,
   so the Memento appears on the victory screen and the run record only,
   and never on a loss. **Unarmed final boss (change IR-13; agreed, item
   rolls Q5):** "carries and uses" assumes an armed final boss; an
   unarmed one (none in M2) **guards** its signature in a signature
   cache in its lair (plate line "Guards: <signature name>"), shown
   during the fight, and it still drops as the 0-VP Memento at victory
   (item rolls design 4.13).
5. **The Viaticum** takes the F5 boss share: an offer set, choose 1 of 3,
   at R, counted 1 in Ledger A.
   - **Options (corrected after the check; lead's call, after the
     check):** distinct Rare boss tools: Rare wands and tier-3 scrolls
     (CN 9.1: tier-1 scroll = C, tier-2 = U, tier-3 = R, tier-4 = VR), at
     most 1 damage scroll (tag `use.damage`, kept as an alias of
     `effectTag` damage, change IR-23; *pending request to the CN
     owner*; M2); no tier-3+ scroll once
     `tier4ScrollsPerRunBeforeFinalBoss`, if engaged, is spent; never the
     phial (the Viaticum is a boss-channel offer, not a container draw).
     The draft's "bombs" are dropped: no bomb exists at Rare (Fire Bomb
     C, Greater Fire Bomb U), so Rare bomb shapes go to 7b as a Later
     content item.
   - **Usability:** all 3 options must be usable by a member in the
     generation snapshot (6.3). Each unusable option is redrawn at R as
     **usable gear**, with the usability filter and ordinary weights
     (`requiresBias` 3), not fit-filtered, on `viaticumFallback` entry =
     its option index, logged `BossCardUsabilityFallback` (change IR-8;
     agreed, item rolls Q1; M2); then 6.3's offer terminal ladder. **Hero
     guarantee:** if no option is usable by the hero, the Theirs option
     is redrawn as hero-usable gear on `viaticumFallback` entry 4 (item
     rolls design 4.8, 4.10). A martial party with no Arcana
     therefore still gets a real last choice.
   - **Placement (rewritten after the check):** a main-path Plain
     container after the lair that is not in a Circuit room, not in a
     fight host (HE-4's exclusion) and not in the Antechamber (on F2–5 a
     patrol Circuit waypoint, RS; FL 6.7, 6.11); else a container in the
     lair room. Never a side room (the whole F5 boss share must not be
     missable), never a Furnishing (ROOM 6.3.4). The FL and ENM owners
     confirm the room. It is counted on the lean route. *(Reading, loot
     sources design 4.4, 4.10: on F5 nothing lies between L and F, so
     the fallback is the real rule; the Viaticum takes the lair room's
     host socket, kind `viaticum`, on its exit side toward the
     Forecourt, outside the start zone, on the walk line; pending FL and
     ENM confirmation, 18.2.)* **Host (change LP-14; agreed, loot
     sources Q5; M2):** the Bedside Stand, a nightstand with three
     drawers, whose silhouette hosts nothing else. Offer pacing never
     drops it (change LP-30; 6.9 item 7).
   - **Surfaces (lead's call, after the check):** the F5 Stratum Card
     says it exists ("Before the Heart's chamber, the dream sets out
     provisions: choose one"); opening its container opens the
     boss-reward offer UI at width 3 (ENM 17.5 "reuses the offer UI"),
     not the loot summary, which has no choose-one mode (ENM 15.8; ITEMS
     8.1). The two unchosen options dissolve on confirm
     (`ItemDestroyed{dreamedAway}`).
   - **Report:** `ViaticumBossShare`, read against CN 19.3's 25% alarm on
     scroll-and-bomb boss damage.
   - **Boss side (read; change BR-28; M2; boss rooms design 4.11.2):**
     every final Rendition is simulated with each of the three Rare
     tools held in turn; each final boss's 33% (d) arrival is clustered
     so area tools score; no final boss is damage-immune (BR-20).
   - **Later (7b content):** arena-keyed single-use tools ("only in the
     Heart's arena") are the cleanest anti-hoard shape; not adopted now.
6. **Effect:** the Last Hollow (camp 4) is the deliberate spend-down camp.
   Camp 4 says so plainly (6.15): coin left at its exit is lost.
7. *Rejected (Q1):* B, keep F5 as approved.

### 6.12 Meta hooks (agreed, Q5; change LE-19)

- **Unlocks** add records at existing tier budgets (value = budget,
  **settled**). Smart-loot weight is normalised per `requires` lane, and
  the Due's candidate pool likewise, so unlocking never lowers the odds
  of an on-build find nor raises `DreamsDueWaivedRate`. Report
  `RequiresHitRate` before and after unlocks; target a drop ≤ 5 points.
  (Met by 7b's lane > family > record split over `lootLane`, where an
  unlocked record dilutes only its own family; change IR-1; item rolls
  design 4.2, 4.16; M3.)
- **Ladder and Nightmare tiers:** zero economy effect (decks, coin, camp
  channel, allowance). The ladder's +10% encounter budgets widen the gap
  against a fixed loot curve, so kPC is reported per tier. Segment 8's
  Nightmare **Provisos** add printed Curiosity costs only, never better
  gains (M3; change EV-31; events design 4.9).
- **Omens never touch the spoils list** (LootDoorInvariant; RS 9.1
  "theme-keyed loot" rejected). The research ideas of Omen reshapes and
  an interest Omen are dropped unless the RS owner rules otherwise.
- **The Perilous Door (M3; RT-21, decided; agreed, Q5).** Every width-3
  offer (the boss card, the Wager set, the Viaticum) pre-draws a fourth
  dreamed option on `lootPlan` **from M2**, whatever the door and
  whatever the wave, so draws and identities are door-independent and
  switching the reveal on in M3 changes no seed (the RS precedent:
  "switching Two Doors on changes no seed", RS 6.4) **(drawn from M2:
  lead's call, after the check)**. On a floor entered through the
  Perilous Door, that floor's camp card reveals the fourth option.
  On that floor the boss uses its Nightmare Rendition one tier up (cap
  3), never an extra boss action, and the Stratum Card and Playbill show
  it before commitment; this design's 1-of-4 card is read unchanged
  (change BR-34; M3; boss rooms design 4.14.2).
  **The fourth and memory (change IR-6; agreed, item rolls Q2; M2):**
  the fourth is drawn by the Wayward rule, never a `oncePerRun` record
  (it might never be presented), and it writes Scion-line memory and
  gear floor keys like any offer option (item rolls design 4.7 items 5
  and 6, 4.8).
  - **Reading (new lead's call; told plainly after the check; accepted
    by the client on 2026-11-22):** "that floor's camp card" is read as the **Perilous
    floor's boss reward card (ENM 17.5)**, shown at the end of that
    floor, before its camp: it becomes choose 1 of 4. Three approved
    screens could answer to "camp card": the ENM 17.5 boss reward card;
    the ITEMS 10.4 **camp reward card** (the alternative to the notables
    shop, which ITEMS 10.4 says "is not the enemies 17.5 boss reward
    card"); and the RS 6.9 camp next-floor card. The boss reward card is
    the only width-3 offer shown before that camp and the only one with
    a pre-drawn fourth option; the ITEMS 10.4 camp reward card's content
    belongs to the camp and hub owner and 7a pre-draws nothing for it
    (the camp and hub design adds no further option: change CH-15, its
    7.1);
    the RS 6.9 card shows no loot. Because this names a different screen
    from the agreed words, it was listed in 18.1 under its own heading;
    **the client accepted this reading on 2026-11-22.** The alternative
    specified before approval (the reveal on the ITEMS 10.4 camp reward
    card, as a hook to the camp and hub owner with its own pre-drawn
    fourth option on a `lootPlan` grandchild, `campRewardReveal`, the
    boss card staying width 3) is not adopted.
  - The Wager keeps its pip grading; F5 has no card and no door. The
    taken option counts as the card's one pick (Ledger B, family caps);
    the rest stay dreamed options.
  - **Not a door bounty.** RS 9.1 rejected "Door bounties (reward bias by
    door)". The reveal is not one: it adds no draw, uses the same pool,
    and changes only what is shown (LE-19).
  - **The mark (lead's call, after the check):** the fourth option sits
    at the right of the offer UI with the Perilous Door glyph as a
    corner badge and the one-line caption "The door's gift", in the
    global skin (RC7), with no extra colour.
  - The RS owner confirms the LootDoorInvariant reading (the door
    changes what is shown, never what is drawn).
  - **The toggle (read by change CH-20; M3; camp and hub design 7.6):**
    the Perilous Door is chosen by a Perilous toggle at the camp's door
    station, on Two Doors cards only, behind the Nightmare-ladder
    unlock; changeable until Travel on, listed on its confirm, and
    entering `GenInputs`. This design's reveal and 1-of-4 card are read
    unchanged (this owner confirms). *Pending request (CH-20, to the RS
    owner):* offer it on single-door cards and at camp 4; if refused, it
    is not offered there (camp and hub design 14.2).
  - *Rejected (Q5):* B, a profile mark and a memento-wall frame only.
- **Dream Fragments:** 0 VP, 0 coin; folded into segment 8's F-BOSS
  options on Lore thresholds, one boss-fact queue in the FactLedger
  (M3; change EV-29; events design 4.9). **Heirloom:** outside the budget
  (**settled**); counts as party-owned for the Due (its precedence in the
  fixed deal and the F1 Due: 6.6 item 2, change SK-4; any heirloom slot
  by change HL-3, M3). Earning (R1–R3 at M3; R4, R5 Later) and the
  Heirloom shelf are content unlocks, never loot or coin; an heirloom is
  never a Curiosity "Item offered" price (change HL-4, EV's; M3);
  Grip-marks are display only (heirlooms and curses design 4.1.2–4.1.6).
- **The Bobbin (charm boon; M3; change CA-20 ⚑; agreed, charm abilities
  Q4; charm abilities design 4.11):** one unlocked Common Catch charm as
  the one starting boon, issued to the hero, outside the budget like an
  heirloom boon (value for display only, unsellable, never a Curiosity
  "Item offered" price), flagged `boonCharm`, never `heirloom`; off on
  shared, seeded and daily runs. Counted against the heirloom boon in
  the parity report (charm abilities design 4.11). *Pending request
  (CA-20, the Due reading, to this owner):* the Bobbin's charm counts
  as party-owned for the Dream's Due's owned and duplicate checks, but
  never as F1 hero arms and never for the HL-3 waiver (6.6 item 2); the
  V13g (g) case is CA-26 (10.2).
- **Daily and shared seeds:** shared seeds (M2) do not fix the hero;
  daily seeds (Later) fix class and species so smart-loot runs are
  comparable (R-SEED-5 hook; shared and daily rules in seeds and
  validation design 4.7, change SV-17, read; cross-document comparison
  2026-11-23, CC-109). *Pending request (SK-16, to the SV owner; M3):*
  on shared seeds, kit-variant boons are replaced by the default kit for
  the chosen creation pick, other boons following SV's canonical-grant
  rule; this is also the fallback until the SV owner rules (starting
  kits design 4.7, 14.2).

### 6.13 Hooks to 7b (what the tables must produce)

Taken up by the item rolls design (`2026-11-22-item-rolls-design.md`,
segment 7b, approved 2026-11-22; changes IR-1 to IR-23), which states
how each hook below is met; the content requests stay open.

- A record for each deck card at an exact tier and category, value =
  budget; category re-weighting when a (tier, category) pair is illegal.
- The Due filter: "a strict upgrade (or, F4–5, a marked sidegrade) for
  member X in slot S against the fixed deal of party-owned items" (6.6).
- The fit-premium draw: a hard filter to the target member's lane, then
  the ordinary weights (6.2).
- Distinct triples for the Wager, the boss card and the Viaticum; the
  fourth dreamed option; the Twin Coffer pair; the usability fallbacks
  (6.3, 6.11).
- Wax-Key identities for consequence caches.
- Camp entries graded for N+1, never VR.
- The weight composition of 6.2: category multipliers by depth;
  `requiresBias` (3, and 4 for the boss card's options); the CMP-30
  `anyCompanion` lever when its report fires.
- Lane-normalised weights after unlocks; ≥ 3 legal records per lane per
  deck tier.
- A tier-stable substitution path with `SubstitutedVP` (made a per-socket
  relaxation order by change IR-7: target relaxations, then the ladder
  rungs; tier-stable before supply or tonic; no silent tier change; item
  rolls design 4.7 item 8; M2).
- **The phial swap (lead's call, after the check):** the phial (CN 14.7)
  takes the lowest-ranked Rare **container** card left after allocation
  that is not the Due, not a fit premium and not a dual-identity cache,
  ties by socketId; it is never carried. A `campReward` phial takes that
  card off the floor: the field deck loses it, Ledger A drops by 1
  (logged; V13s (a) allows this one exemption on the phial's floor), and
  its VP is counted in the camp channel. On the camp reward card it is
  the third reward pick only, never on the shop panel; declined, or
  left with `choice` unset at Travel on, it is lost for the run (change
  CH-15; M2; camp and hub design 7.1). **The boss-channel phial
  (change IR-8; M2):** when CN 14.7's pre-roll (agreed, CN Q2) places
  the phial in the boss channel (an F3–F4 boss card, never F5), it takes
  the boss card's Wayward slot (option index 2), whose identity and
  `lineRoll` draws are consumed as ghosts (item rolls design 4.1).
- Carrier-legal subsets for carried cards (ENM 15.3 step 4); the elite
  carrier's gear-only subset (6.9 item 2).
- Content requests: 2–3 VR gear records; Rare bomb shapes and
  arena-keyed tools for the Viaticum (Later).

### 6.14 Hooks to 7c (what each source receives)

Taken up by the loot sources design (`2026-11-22-loot-sources-design.md`,
segment 7c, approved 2026-11-22; changes LP-1 to LP-34), which states
how each hook below is met (its 4.4); its pending requests stay open.

| Source | Receives |
|---|---|
| HE-7 main-path Plain containers | The DH block (approved) and the Due; coin weight 1 |
| Other SR0 containers | Ordinary cards; coin weight 1 |
| SR0 carriers | Ordinary carried cards; no coin (salvage pays fights) |
| SR1 off-path containers, Alcoves | Ordinary cards; coin 2 |
| SR1 optional-fight carriers | Ordinary carried cards; no coin |
| SR2 locked, secret, Keybearer caches | The quota card (6.4 item 9; ≤ 1 notable in secrets, never the top) or a K2-sized coin lump + 1 non-healing sundry; coin 3 |
| SR3 consequence-lock caches | The quota card as stepped identity + Wax-Key identity, or a coin-only lump at the next lump step; coin 4 |
| SR4 elite carrier | The highest carrier-legal gear card; the elite intel line |
| SR4 elite-room lair cache, Strongroom | A consumable top card if one is dealt; coin 4. ("Lair cache" is the *elite room's* cache; the main-path lair's SR0 container is `lairContainer`, a reading, loot sources design 4.1) |
| SR5 Wager, Dare (M3) | Stepped cards / triples; coin 4. The Annex has no prize of its own (6.9 item 1) |
| Bait cache, boxed lurker | Real SR2 entries (the boxed lurker + its carried sundry) |
| Activity containers | Coin and sundries |
| Heavy Purse carrier (F2–F4) | 20% of found coin, bound in LootBind (8.3) |
| Boss | Signature (+ the counted card); F5 the Memento |
| F5 main path after the lair | The Viaticum (6.11 item 5); by the reading recorded there, the lair room's host socket, kind `viaticum`, unranked, on the Bedside Stand (changes LP-1, LP-14; loot sources design 4.10) |
| SR2 Windfall (M3, change LP-1) | 1 card under (f)/(g) from the host's carried allotment or a plain SR1 container (6.9 item 2 step 8); coin weight 3, never a lump; no salvage, no purse (loot sources design 4.4, 4.7.6) |
| Encounters | Salvage shares, per fight then per unit (6.4 item 2) |
| Every container | Never empty (6.4 items 4–5) |

Caps 7c keeps (the Windfall's card counts toward its **final** host
encounter's 3 notables and carried cap, M3, change LP-1): 3 notables per encounter; carried 1 per encounter on
F1–2, 2 deeper; at least 1 Steal target (M2, **settled**); a carrier
fallback to a same-room container keeps its card (6.9 item 7); trimmed
floors keep the full deck as visible Alcoves, the top card on a carrier;
HZ 13 fallen loot on the edge, no item loss (the sole exception a
Windfall that gets away, M3, change LP-2); TL 11.1 container fields
(`noCheck`, `heldWaxKey`, `lockId`); on F5 at most 1 non-notable lock and
no more loot-bearing containers than fillers (6.4 item 5).

### 6.15 UI (hooks to segment 10)

- **Ordering (lead's call, after the check).** The Stratum Card shows,
  in this order and at most 1 line each: RS facts; DH guarantees; counts
  (rooms, treasures); announcements (the Dream's Due, the purse, the
  Viaticum on F5). The party bar consolidates its floor tallies into one
  floor strip of icon-prefixed counts in a fixed order (potion, rooms,
  traps, treasures), so the word "found" is not repeated. **The rooms
  cell (change XI-11; M2):** reads "rooms e / t", where t is the count
  line's main-path rooms plus side rooms and e counts Room nodes entered
  from that same set (Alcoves and corridor segments never count); never
  split by role (exploration and information design 4.4.4).
- **Stratum Card:** "Treasures: n" (8.2); the Dream's Due line naming
  its target; on F2–F4 "A heavy purse walks this floor"; on F5 "Before
  the Heart's chamber, the dream sets out provisions: choose one" and the
  pressed-moth line ("in the Dreamer's Heart, coins are pressed moths"),
  told once there. On a Windfall floor (M3) the Treasure line reads
  "Treasures: 7 · one may get away", no new line (loot sources design
  4.5).
- **Treasure field** (the floor strip's "k / n · best remaining" cell;
  "Treasure strip" retired as a synonym, change XI-19; M2; exploration
  and information design 4.9.9; FL 9.2's "treasures: k / n" on the count
  line reads as this field): "k / n" (8.2); once k ≥ n / 2 it adds "best
  remaining: <tier word>", computed from unfound entries and hidden when
  none remain, so floor entry is not a spoiler. **By source (change
  LP-28; M2):** hover breaks n into counts only (open, locked, hidden,
  carried, prize, windfall), never tier, item family or room (loot
  sources design 4.5). **Spent field (change XI-11; M2):** when "best
  remaining" is Common or nothing countable is left, the field drops one
  RC6 value step and reads "nothing worth chasing" or "nothing countable
  left"; while k < n / 2 only "nothing countable left" can fire. The
  field also feeds the BR 4.16 arena-entry Behind-you line (exploration
  and information design 4.4.4, 4.4.5).
- **Pointers (change XI-23; M3):** "counts only, never locations" holds
  on every free surface; a paid or earned pointer (the Lodestring;
  F-TREASURE through Yield or a Curiosity gain) may name one room (8.2).
  It resolves only on the floor entered, never on camp or Two Doors
  cards, and its string is a map line plus a world trace of 1 s or
  less, never a persistent floor decal; a pointed notable's loot card
  gets a "the string ends here" frame, display only (exploration and
  information design 4.6.2, 4.6.3).
- **The Dream's Due:** the loot-summary entry is tagged "The dream's due:
  for <name>", with Take defaulted to that member. Once shown, the
  Stratum and strip line turns to a "paid" state. If the target has left
  the party or died before it is found, the line reads "<name> did not
  live to collect" and the item stays an ordinary find. A sidegrade Due
  is tagged "another way".
- **Tier marks** (renamed from "tier pips"): on the lock card and the
  container hover only (a container hover on the full map counts as
  that hover: reading, change XI-7; exploration and information design
  4.3.2), never a world-space floor decal: 1 to 4 filled
  diamonds (C / U / R / VR), each with its tier word, a shape distinct
  from every existing pip family (the Wager's Disturbance meter, Cord
  pips, vent trigger pips, the Keybearer pip, boss pips). Added to the
  RC5 pattern registry. Container art never reads rarity (read by
  change LP-15: CT2 `ContainerArtBlind`, loot sources design 5.1).
  Rarity is never carried by hue alone, and any
  rarity tint stays off the RC4 damage-hue key. On Wager cages the tier
  mark sits on the offer card, never beside the Disturbance meter. Never
  the category. Lock-card wording per 6.9 item 4, including the fit
  case ("chosen for <name>"), with each identity's fine-category noun in
  place of "notable" (change IR-11; item rolls design 4.11; M2).
- **Loot marks on plates:** the elite's item is shown in use. A carrier's
  loot is one badge in ENM 5.6's loot-mark slot: the notable's star, a
  coin disc for the Heavy Purse, or both merged into one "loot" badge
  with a count; with the TL key pip, a plate carries at most 2 marks.
  Hover reads, for example, "Carries: 12 coin". Purse badges count in
  the ENM glints-per-screen gate. **Steal preview:** the action's hover
  lists the carrier's stealable items as rows (notable, purse with its
  amount, key); the player picks one on confirm (8.3).
- **Salvage:** the fight's total on the loot summary's coin line, with
  per-unit lines only for non-zero shares (6.4 item 2). Coin and salvage
  lines are hidden on F5.
- **Elite intel line:** shown in the loot summary's existing floor-fact
  slot (ENM 15.8) when it fires (6.9 item 6), and kept afterwards under
  the boss name on the pause screen's floor-info panel.
- **The Viaticum:** opens the boss-reward offer UI at width 3, not the
  loot summary (6.11 item 5). Its host is the **Bedside Stand** (change
  LP-14; agreed, loot sources Q5; M2), the second named exception to IR
  4.18's "container art never implies contents" (loot sources design
  4.10).
- **Camp:** the coin ledger; "the Hollow ahead sells rare" (the next
  camp's top tier); "unused since floor N" tags. **Camp 4 (the Last
  Hollow):** a plain line on the coin ledger, "No camp after floor 5.
  Coin left at camp exit is lost.", repeated as a soft confirm on the
  camp-exit button when the purse is at least the cheapest vendor item.
  (Placed by the camp and hub design: the ledger and its lines at the
  Keeper's Cart, the tags in the Bedrolls party view, and the soft
  confirm as a line of the Travel-on confirm; its 4.2, 4.3.)
- **Run record:** Grave Goods (8.2); the Memento card on the victory
  screen and run record only (6.11 item 4).
- **Perilous floor (M3):** the boss card's fourth option, marked as in
  6.12.
- **Audio (hooks to segment 10 and audio):** the Dream's Due find and the
  `dreamedAway` dissolve are short UI stings in the global UI timbre, not
  reserved gameplay cues; the purse drop reuses the existing coin pickup
  sound. No loot cue joins the RC7 reserved list unless the RS owner asks
  for one. *Pending request to the RS owner (change LP-7; M3):* a
  Windfall timbre row (a plucked, dry string family) and 6 reserved
  Windfall cues (loot sources design 6). **Container opens (change
  LP-23; M2):** one global open cue plus a theme foley tail (CT12; loot
  sources design 5.1).

---

## 7. Target tables by floor (all illustr.)

| | F1 | F2 | F3 | F4 | F5 |
|---|---|---|---|---|---|
| Ledger A (field + signature or Viaticum) | 6 | 7 | 7 | 7 | 7 |
| Field deck (base) | C C U U U | C U U U U U | U U U R R R | U R R R R VR | U R R R R R |
| Alternate variant | — | fit variant | C U R R R R | fit variant | C R R R R VR |
| Field VP | 8 | 11 | 15 | 18 | 17 |
| Signature / Viaticum VP | 3 | 3 | 4 | 4 (M2) | Viaticum 3 |
| Boss card | C (1) | U (2) | R (3) | R (3) | none |
| Floor VP (base) | 12 | 16 | 22 | 25 | 20 |
| Step allowance | ≤ 2 | ≤ 2 | ≤ 2 | ≤ 2 | ≤ 2 |
| Coin pool, cap 3 (salvage 12% inside) | 35 | 50 | 70 | 90 | 0 |
| Coin pool, cap 2 | 35 | 50 | 70 | 75 | 0 |
| Salvage / found coin (cap 3) | 4 / 31 | 6 / 44 | 8 / 62 | 10 / 80 | 0 / 0 |
| Heavy Purse (cap 3) | — | 8 | 12 | 16 | — |
| Lock lump (consequence, exposed) | — | 12 (16) | 16 (20) | 20 (24) | — (sundries) |
| Carve cap (lumps + purse) | — | 26 | 37 | 48 | — |
| Carried cards | 2 | 2 | 3 | 3 | 3 |
| Camp channel after (2 entries) | U, U | R, U | R, U | R, R | none |
| Due target | hero (arms) | largest deficit | hero | largest deficit | hero |
| Hero equipped VP at entry, p50 | 0 | ~3 | ~5 | ~7 | ~8 |
| Healing (DH, unchanged) | kit 2 + 1 guaranteed | 1 + top-up | 1 + top-up | 1 (2 by raise) | 1 (2 by raise) |
| Found supply | 1–2 | 1–2 | 1–2 | 1–2 | 1–2 |
| Lean route share | 45–55% | 45–55% | 45–55% | 45–55% | 45–55% |
| Wax Key price at the preceding camp | — | 24 | 28 | 32 | 36 |

**Camp ledger** (expected party, Sell 250‰, rough scratch arithmetic;
recomputed after the check with restock costed per body). Assumptions
*(illustr.)*: restock = 8 supply + 12 per body of the next floor's
expected party (2 / 3 / 4 / 4 bodies at cap 3; 2 / 3 / 3 / 3 at cap 2);
the wish list = restock + a tonic per body (12) + Remedy (30) + the camp
channel's first entry at 600‰ (U 24, R 36) + the Wax Key at 50% weight.

| Camp | Purse on arrival (carry + pool + Sell) | Restock | Wish list | Coverage |
|---|---|---|---|---|
| 1 | 15 + 35 + ~23 ≈ 73 | 32 | ~122 | ~0.60 |
| 2 | ~10 + 50 + ~33 ≈ 93 | 44 | ~160 | ~0.58 (below band: first lever, camp 2's first entry drops R → U, tiers U, U: wish ~148, ~0.63; second, an F2 pool of 55: ~0.66) |
| 3 | ~10 + 70 + ~47 ≈ 127 | 56 (cap 2: 44) | ~186 (cap 2: ~162) | ~0.68 (cap 2: ~0.78) |
| 4 (Last Hollow) | ~15 + 90 + ~60 ≈ 165 (cap 2: ~15 + 75 + ~60 ≈ 150) | 56 (cap 2: 44) | ~188 (cap 2: ~164) | ~0.88 (cap 2: ~0.91; spend-down) |

Restock is covered at every camp at both caps for the largest legal
party (the schedule's party, CMP 7.7). The draft's ledger used a flat
restock column (32 / 32 / 52 / 52) that did not scale with bodies, and
its camp-2 lever ("the second entry stays U") was a no-op, since camp
2's second entry is already U (fixed after the check).

Told plainly: these decks and coin differ slightly from the toy's tested
Hmid fix (the toy read coin from the actual party and counted the boss
card); the toy must be re-run on this configuration before M2 (6.4 item
1).

*Pending request (SK-11, to this owner and the ITEMS owner; M2):* adopt
the starting kits design's mundane basePrice scale (its 8.4), which also
sets vendor prices for mundane bases, with a camp-1 coverage re-run of
this ledger at Sell 250‰ and 200‰. Until the owners rule, the scale
stays in data labelled `placeholder`; fallback: the owners' own prices
and a `KitResaleBand` re-run (starting kits design 4.3, 14.2).

---

## 8. New mechanics considered

### 8.1 Table

| Mechanic | What it adds | Cost | Wave | Verdict |
|---|---|---|---|---|
| Risk Ledger (ranks, allocation, allowance, fit premium, tier marks) | Makes the "+1 step" checkable | Low | M2 / M3 | **Adopted** (6.9; LE-10) |
| The Dream's Due | A fit guarantee, not pity | Low–medium | M1 hero arms, M2 | **Adopted** (6.6; LE-13) |
| The Viaticum + the Memento | Removes F5 dead value; a last pre-boss decision | Low | M2 | **Adopted** (6.11; LE-5; agreed, Q1) |
| Treasure count + Grave Goods | Routing information; a known stopping point; a hoarding signal | Very low | M2 | **Adopted** (8.2; LE-15) |
| Heavy Purse | A named coin target for combat and Steal, carved from the pool | Low | M2 | **Adopted** (8.3; LE-16) |
| Elite intel line | A non-power premium for the elite room | Very low | M2 | **Adopted** (6.9; LE-18) |
| Twin Coffer | Choice width at no value | Low | M3 | **Adopted** (8.4; LE-21) |
| Curses at a discount | "Cursed items worth more", safely priced | Medium | Later; M4 by change HL-29 | **Adopted with Curses** (8.5; LE-22) |
| The Wish (a paid camp mark steering the Due) | Agency; a late sink | Low | M3 | **Hook** to camp and hub (adds a third outside input to ENM 10.5 step 3; that owner flags it); held Later by the camp and hub design (its 5.4) |
| The Tithe Bowl (one camp service, one purchase per camp: a Lodestring or a Surveyor's Sketch; change XI-24) | An information sink | Low | M3 | **Hook, filled by segment 10** (exploration and information design 4.6.3): the Lodestring (pegged at or above a healing potion) or the Sketch (15 coin *(illustr.)*; room and Alcove outlines with edges and the Stairhead at the next floor's entry, never secret nodes); both resolve at the entered floor's entry or use, never on camp or Two Doors cards; the Lodestring option is greyed while one is held and not offered at camp 4. Names socket classes "behind risk", never "behind a lock"; unavailable when the party holds a Wax Key; its fact deduplicated through segment 8's per-floor FactLedger, change EV-8, events design 4.4.2. *Pending request (XI-24, to this owner):* relax the Wax Key guard for both options; if refused, the guard stays (exploration and information design 14.2); until this owner rules, the camp and hub design shows the Bowl at the Keeper's Cart greyed with its reason while a Wax Key is held (its 4.3, 14.2) |
| The Ferryman's Note (borrow against the next floor's coin) | Spend-now tension | Low | M3 | **Hook**, capped: ≤ 30% of the next pool, once per run, never camp 4; replaces interest; the first promotion candidate if `TonicShareOfSpend` > 25% (camp and hub design 5.4) |
| Blood Price (a `treatProof` Scarred for +1 step on the camp item) | Pay in blood | Low | M3 | **Hook**: uses one of the next floor's 2 allowance steps (no swap-down); once per run; never camp 4; never VR (the camp cap); the second promotion candidate (camp and hub design 5.4) |
| Wager Pot | Stakes at the Wager | Very low | Later | **Parked** (creates coin; a hoarding pull) |
| Rarity and fit debt counters; purse governor | — | Medium | — | **Rejected** (no measured effect; contest DH 5.4) |
| Berth entries; an early F2 Rare; fixed class quotas | — | — | — | **Rejected** (5.3) |
| Elite spoils lair-cache swap (Model C) | Choice at the elite | Medium | — | **Not adopted** (clock); presentation only |
| Dare width by attempt | Clean-win texture | Low | M3 | Left to the HZ owner |
| Coin interest; Dream Tithe; salvage shards; coin weight; coin as score; companion upkeep; per-body shares; kill bounties; an F1 Antechamber catch-up cache | — | — | — | **Rejected** (meta currency, second currencies, hoarding pulls, CMP 7.8, ENM 2, player-state pity) |

The Dream's Due is in full in 6.6; the Viaticum and the Memento in 6.11.

### 8.2 Treasure count and Grave Goods (lead's call; change LE-15; M2)

- **Treasure count:** n = field notables (containers, secret caches,
  carried), plus the Viaticum on F5; the signature, camp stock and
  sundries are not counted; F1 excludes the secret cache (FL-20): a
  found F1 secret-cache notable raises neither k nor n and shows as "+1
  secret". Offer sets count 1, and count as found when shown. Found k
  counts a notable shown on a loot summary card, taken or left.
- **n falls by 1, with a visible log line, whenever a notable is put out
  of reach by a rule the party can see (lead's call, after the
  check):** a carrier's use of its notable ("what is used is gone"),
  **only when the use is seen** (line of sight or a combat-log line:
  "a treasure was spent by its bearer"); a Dare sealed after two
  failures; the Twin Coffer's untaken alternate (it counts 1 already, so
  it never inflates n); **a Windfall that gets away** (M3; change LP-2;
  agreed, loot sources Q1), by any cause of `WindfallGotAway{cause}`
  (Rathole, faded, party fled), with "The <skin> got away with a <tier
  word> treasure" and `ItemDestroyed{gotAway}`, the sole exception to
  "loot is never destroyed" (n counts the Windfall's card; a Steal on
  it raises k; one left Dormant at the stairs leaves n unchanged; loot
  sources design 4.5, 4.7.7). An unseen use leaves n as it is, so k / n can
  stay short, which the count states plainly ("some treasures may be
  gone") rather than hiding. Carriers only consume consumable notables;
  gear is never consumed (ENM 15.3–15.4, **settled**), and the elite
  carries only gear (6.9 item 2).
- **"Best remaining"** (6.15) reads the unfound entries, not the floor's
  best card, and is hidden when none remain.
- **Grave Goods (reshaped after the check; lead's call, after the
  check):** not a new death-screen line. It is the run-record version of
  DH 17.12's unspent line, in DH's neutral voice, for wins and losses:
  "Still carried when the dream closed: 43 coin, 2 potions, a Rare
  scroll" (on a win, "Carried out: …"); with "Treasures found 31 of 38"
  and "Spent 212 coin". The draft's "Buried with …" is dropped: nothing
  is buried in a dream, and the premise's death line is "You did not
  wake" (RS 7.5). Profile-side, never a score. **From M3 (change
  LP-29):** Grave Goods gains "Windfalls nabbed k of m" and "Got away:
  <tier word>" (the tier word of each, never its family or identity),
  never a profile achievement or completion stat (loot sources design
  4.5, 7). Segment 8's display-only **Afterword** line reads the same
  death key (floor, theme, killer category) as this record, and never
  changes what is drawn or offered (read by change EV-26; M2 lean;
  events design 4.5.10). **Kit names and the Oddment (change SK-6; M2;
  agreed, starting kits Q2, Q3; starting kits design 4.5.1, 4.5.2):**
  Grave Goods also lists each named kit piece by its given name and fate,
  read from the named-piece log, and the hero's Oddment in its state
  (the "Still carried when the dream closed:" or "Left by a fire:"
  heading, the name below); display-only, never counted in Treasures,
  Spent or any ledger. An heirloom's fate is listed the same way (M3;
  heirlooms and curses design 4.1.6). *Pending request (HL-20, to the
  DH owner; M4):* Grave Goods also lists active Maladies and worn cursed
  items, in the neutral voice from the event log, display-only; if
  refused, it lists curses only (heirlooms and curses design 4.3.5,
  14.2).
- **Treasure field by source (change LP-28; M2; "Treasure strip"
  renamed by change XI-19):** see 6.15; the
  "windfall" count drops by 1 on a nab, a Steal or a getaway, and a
  nabbed card counts under "carried" until it is taken (loot sources
  design 4.5).
- **Pointers narrow "counts only, never locations" (change XI-23 ⚑;
  M3; exploration and information design 4.6):** free surfaces stay
  counts only; a paid or earned pointer may name one room. **F-TREASURE
  (change XI-22; M3; this owner applies the selector):** candidates are
  unfound field notables at container sockets on the current floor, read
  draw-free from LootBind's bound placement and the `LootLedger` flags;
  excluded are secret caches (agreed, exploration and information Q5),
  carried notables, boxed-lurker containers, Windfalls, the Viaticum, the
  signature cache, HE reservations and all of F1. Ranking is role-blind:
  tier descending, then the room's macro-grid position, then the
  container's square. Output: one room, its tier word and the qualifier
  "plain cache" or "behind risk" (from the socket class); at most 2 per
  floor across all channels. Fact channels on F2–F5; the Lodestring (a
  run-scoped party counter, cap 1, never a notable or deck card) on
  F2–F4 only. It draws nothing and never adds, moves, upgrades or
  re-rolls loot: n, k and Ledgers A, B, C are unchanged. **Value
  (told plainly):** a pointer adds no value to the floor but can raise
  the share a hurried party collects (toy: +0.35 VP for lean players,
  inside the base-to-base+2 band); *pending this owner's confirmation of
  the band* (exploration and information design 4.6.2).
- **Floor Atlas (change XI-29; M3):** the run record also keeps each
  floor's final map with "Treasures found k of n" (no rooms-entered
  fraction), profile-side display, never a score or achievement, never
  read by rules (exploration and information design 7).
- **Report:** `TallyChaseSeconds` (time after the last main-path room
  spent only raising k), median ≤ 30 s.

### 8.3 Heavy Purse (lead's call; change LE-16; M2)

- On F2–F4, 20% of found coin *(illustr.)* is carved onto one carrier,
  bound in the draw-free LootBind pass after `trapsResolve` **(binding:
  lead's call, after the check)**. The chain: the Keybearer unit bound by
  `trapsResolve`; else the lair leader; else the highest-level non-elite
  unit in the lair; else the highest-level non-elite non-minion unit on
  the floor, ties by unitId; **never the elite Champion** (protects the
  elite-room Steal-under-15% target). If no unit qualifies, the purse's
  coin returns to the found-coin lumps. A key that fell back to a
  container takes no purse with it.
- It drops on resolve (killed, Yielded, Routed); a Cowed carrier keeps it
  (as for notables, **settled**).
- **Steal:** a successful Steal takes **one** of {notable, purse, key},
  the thief's choice, keeping the one-try-per-carrier spirit (ENM 15.5,
  change LE-16); the Steal preview lists them (6.15). A stolen purse
  forfeits that unit's salvage share. Report `StealCoinShare`, ≤ 25% of
  found coin for a Shade party.
- **Steal on a Dormant Truant (M3; change LP-6; agreed, loot sources
  Q1):** card only (this rule holds); its coin spills as a pile,
  "guarded" until the host group resolves, then Taken. The party has
  the room's one Steal try (`stealTryUsed`): a host carrier or the
  Truant, not both. `StealCoinShare` counts nothing from a Windfall,
  because its coin is Taken, not stolen (loot sources design 4.7.8).
- Never on F1 or F5.

### 8.4 Twin Coffer (lead's call; change LE-21; M3)

A container kind (TL 6.13) holding 2 pre-rolled notables of the same card
tier and different categories; taking one dissolves the other ("it was
only a dream") inside the loot summary; counts 1; the alternate is a
dreamed option. At most 1 per floor, F2+, off-path, never an HE socket.
It carries a twin-lid kind mark (change LP-26, CT8; M3). **Offer pacing
(change LP-30; M3):** at most 2 offer-form field sources per floor among
the Wager, the Twin Coffer and the Viaticum; the Twin Coffer is the
first dropped by the draw-free trim (6.9 item 7), reverting to a plain
container that keeps its Yours identity, its Wayward a dreamed option.
Report `OffersPerFloor` (loot sources design 6, 7).

### 8.5 Curses at a discount (lead's call; change LE-22; M4, moved from Later by change HL-29 ⚑; agreed, heirlooms and curses Q5)

A cursed card keeps its deck tier T for VP, price, Sell and Scrap; its
benefit is authored to T+1's budget and the curse pays the difference.
The carrier cost add reads the benefit value (cursed carriers hit
harder). Never F1, never on a Due, at most 1 per floor (ITEMS 13).
**Made concrete (change IR-20; M4 by change HL-29, its draws reserved from M2):**
curses are authored **cursed twins** (`cursedTwinOf`, same family and
base), never rolled; a per-floor `curseGate` (2 draws, always consumed
from M2) makes "at most 1 per floor" structural; eligible only on Rare+
gear field cards, F2+; also barred from fit premiums, the arms floor,
every offer option and the fourth, the Viaticum, camp entries,
signatures, heirlooms, consumables and rings; a cursed carrier's cost
add uses the benefit's T+1 value (item rolls design 4.15).
**M4 (change EV-43; moved from Later by change HL-29):** segment 8's
shrine curse cures and curse bargains touch these curses; conditions and
items owners (events design 13).
**Taken up by the heirlooms and curses design (segment 15, approved
2026-11-23; its 4.2, 10):** this section is kept whole and only its wave
moves. At M4 item curses ship with Clinging, the "Not yet read: may
Cling" chip and first-equip line (change HL-15, ITEMS and IR owners;
agreed, heirlooms and curses Q3; presentation only, never reading
whether a card is cursed), the malign hint and five folk cost lines
(change HL-16: `whileEquipped`, none feeding a hard-condition transform;
Eyebitten and Ill-Slept Later), with Maladies, the desecrated-shrine
source and EV-43's shrines; cure text is generated from the hosts in
the build (camp or a shrine at M4; rune hosts stay Later). *Pending
reading (HL-29, to the CON owner):* the M4 Cursed family sits outside
CON 16's M1–M2 32 and passes CON 20.3's gate; if refused, this section
stays Later unchanged (heirlooms and curses design 14.2). The curse
trunk (CH-24) stays Later.
**Burdened twin (change HL-23 ⚑; Later):** the one exception to
"never on heirlooms": an heirloom template may author a Burdened twin
(`burdenedTwinOf`), the **identical** U benefit plus one folk cost line,
with no T+1 discount, reachable only under the Oath of the Burdened
Hand; never a plain boon (heirlooms and curses design 4.7).
**Redeemed (change HL-24; Later):** wearing a known cursed twin when a
final boss falls, in a personal win, unlocks that family's distinct
uncursed U Redeemed template (R5), a content unlock, never loot
(heirlooms and curses design 4.1.2).
*Pending request (HL-21, to the MAIN and RS owners of the CH-24 slot;
Later; agreed, heirlooms and curses Q6):* under the Oath of the Crooked
Road both `curseGate` draws are consumed as normal and only draw 1's
result is overridden; a forced twin carries its benefit at card tier T,
not T+1, so the run is never richer (6.12; `OathNeverEases`). If the
owners refuse or defer, the slot stays unlit and this rule is held
(heirlooms and curses design 4.5, 14.2).

---

## 9. Numbers and fixes (illustrative; toy, not simulator)

Scripts in the design lead's scratchpad (`le7a_sim.py`, `le7a_calib.py`,
`le7a_report.py`, `le7a_r2.py`, `le7a_scum.py`, plus `curves.py`,
`gu.py`; Appendix B). Two worlds calibrated on Z to DH 18.2 (soft γ 0.5,
steep γ 1.0); 6,000 runs per arm. **Limits:** no experience or level
gain, no Steal, no carrier cost add, crude potion and tactical models, no
player skill.

**Death targets and arms (soft world).**

| Model | Win | F1 deaths | F3–5 share | Lean win | Full win | Solo win | Starved ratio |
|---|---|---|---|---|---|---|---|
| Z | .300 | .035 | .76 | .402 | .235 | .282 | .46 |
| A | .311 | .035 | .75 | .406 | .263 | .295 | .47 |
| B | .307 | .035 | .75 | .392 | .260 | .275 | .43 |
| C | .351 | .034 | .75 | .441 | .275 | .311 | .38 |

**Luck (steep world, F3 gear decile win ÷ base, p10 / p90).** Z .74 /
1.27; A .88 / 1.03; B .87 / 1.11; C .85 / 1.04. All pass ≥ 0.70; Z is
marginal. Rarity luck barely matters; fit and slot saturation drive the
variance, so the Due is the dead-run tool and the deck is the curve
tool.

**Coin.**

| Arm | Median unspent at victory | At death | Sell share | Shop take |
|---|---|---|---|---|
| Z (salvage peg literal) | 2,551 | 1,026 | .07–.21 | — |
| A as proposed | 83 | 111 | .35 | 0 / 0 / .67 / .53 |
| **Hmid fix** (35/45/55/65/0 +15% per companion, Sell 250, camp channel 2) | **20** | 64 | .40 | 8–46% |
| Cfix4 | 11 | 66 | .35 | — |

Hmid is the toy's best-tested coin arm and is recorded as written. It is
**not** the adopted table: its per-companion uplift is Q4's rejected
option B, so this document adopts the per-party pool 35 / 50 / 70 / 90 /
0 the proposal recommended (6.4 item 1), owed a toy re-run before M2.

**Risk sources (Δwin always against never, points; the loot part in
brackets).** Elite −16 to −17 (+0.7 to +0.8); consequence lock −2.7 to
−0.3; off-path −1.0 to +1.4; Dare / Wager −1.6 to −0.6. About 0.1 win
point per VP.

**Save-scum.** At generation (approved): 0. At open: +0.4–0.5 tier per
notable (Z); the camp-reload rehire leak: about 3 rerolls (fixed by
LE-14).

**Fixes adopted.**

| Failure | Fix | Where |
|---|---|---|
| Coin flood under the literal salvage peg | Fixed pool; salvage a share | LE-7, LE-8 |
| Surplus coin in A, B, C | A lower pool, Sell 250‰ | LE-7, LE-9 |
| A one-item shop is never bought | Camp channel of 2 | LE-6 |
| VR roster wall (VR blocked 0.21–0.51 per run) | F5 VR → R + fit; F4 VR Icefall-or-fit; signature at the legal tier | 6.3 |
| C's power creep | C not adopted | 5.3 |
| Risk net negative | Flag; elite intel; Q6 | 6.9 |
| Over-closure at k = 1 | kPC first; trip-wire; F4 deck at 6 cards | 6.5 |
| Hero dry floors in A (5.5% below curve) | The Due parity rule (B's targeting reached 3.6%) | LE-13 |
| Camp reload leak | Atomic commit | LE-14 |
| Tactical hoarding | F5 cap; Viaticum shape; suggester hook | 6.7 |

---

## 10. Data, validator V13 and simulator (changes LE-23, LE-24)

### 10.1 Records and keys (C# rules core; `inventory.json` `loot.*`, all illustr.)

```
loot.rarityDeck[floor] = { variants: [[tier...]], alternateKind: "none" | "fit" | "spiky" }
loot.carriedPerFloor = [2, 2, 3, 3, 3]          // M1: ≤ 1 carried weapon or armour
loot.signatureTierRule = "topLegalForCategory"
loot.bossCardTier = [C, U, R, R, none]
loot.bossCard = { width: 3, perilousReveal: true }   // the reveal, M3 (6.12)
loot.campChannel = { count: 2, tiers: [[U,U],[R,U],[R,U],[R,R]], maxTier: R, gradeReadsNext: true }
loot.campWaxKeyPermille = 250
loot.stepAllowancePerFloor = 2
loot.rankWeights = { SR0:1, SR1:2, SR2:3, SR3:4, SR4:4, SR5:4 }   // coin sockets only (containers, caches)
loot.maxSocketsPerRank = 12; loot.crossRankSwapPermille = 250
loot.coinPool = { cap3: [35, 50, 70, 90, 0], cap2: [35, 50, 70, 75, 0] }
loot.salvagePermille = 120; loot.coinLumpMin = 3; loot.carveCapPermille = 600
loot.f1MainPathCoinMin = 17
loot.lockLump = [0, 12, 16, 20, 0]; loot.consequenceLockLump = [0, 16, 20, 24, 0]
loot.heavyPurse = { permille: 200, floors: [2,3,4] }
loot.fallbackCoin = 20; loot.f5SundryCoinPerDose = 12; loot.f5NonNotableLocksMax = 1
loot.categoryWeight[floor]; loot.f5TacticalMaxPermille = 400
loot.gearCurve[level] = { mainHand, offHand, armour, trinket1, trinket2 }   // VP
loot.dreamsDue = { heroFloors: [1,3,5], f1Tier: U, waiveCoin: 20, extraDueAtParty: null }
loot.levers.dueSidegradeFromFloor = 4   // the F4–F5 sidegrade; one key (item rolls design 9.1; cross-document comparison 2026-11-23, CC-110)
loot.requiresBias = { default: 3, bossCard: 4 }
loot.premiumShareBand = [350, 500]; loot.leanRouteBand = [450, 550]
loot.foundSupplyPerFloor = [1, 2]
loot.f5 = { coin: 0, campChannel: 0, bossCard: false, signature: "memento",
            viaticum: { tier: R, width: 3, categories: ["wand", "scroll"],
                        maxDamageScrolls: 1, damageTag: "use.damage",
                        scrollTierForRarity: { C: 1, U: 2, R: 3, VR: 4 } } }
loot.offerDreamedExtra = 1   // the fourth dreamed option on every width-3 offer, drawn from M2
sellPermille = 250
```

`loot.stepAllowancePerFloor` 2 is **not tuning** (agreed, Q6: raising it
needs the client). `loot.f5.coin` 0, `loot.f5.bossCard` false and
`loot.f5.campChannel` 0 are **not tuning** (agreed, Q1). Coin never reads
the actual party (agreed, Q4): `loot.coinPool` is keyed by the build's
`companionCap`, never by the party **(new lead's call:** these marked as
client keys, as DH marks its package keys**)**. Everything else is
*(illustr.)* tuning.

Runtime **(provenance and ledger fields added after the check; lead's
call, after the check)**:
- `SpoilsEntry{id, floor, tier, channel, socketId, rank, flags[],
  recordId, vp, dreamedOptionIds[], waxKeyIdentityId?, openedVia?,
  consequenceExposed}`;
- item instances gain `spoilsEntryId?`, `foundFloor`, `channel` and
  `lastUsedFloor` (for `ViaticumBossShare`, `LateFindRelevance`, Grave
  Goods, `HoardIndex` and the "unused since floor N" tag);
- `LootLedger{floor, variant, entries[], vpBase, stepsUsed, vpTaken,
  substitutedVp, treasure{n, k, decrements[]}, coin{pool, salvage[],
  lumps[], purse{carrierId, amount}, fallback, withheld}, sundries,
  supply, waxKeys, dueTarget, dueSlot, fitPremiums, waived[],
  bossCard{options[], taken?, fallback}, viaticum{options[], taken?},
  campChannel{entries[], waxKey}, dreamedOptions[], remaps[],
  realisedWaxKeyValue, f5Unspent}` (ITEMS 18.4).
- Events: `DreamsDueWaived`, `StepFitPremium`, `OfferResolved`,
  `BossCardUsabilityFallback`, `LootBindRemap`, `CoinSocketTrimmed`,
  `ItemDestroyed{dreamedAway}` (a new reason on the ITEMS 18.3 event),
  `CoinChanged` (existing; this design adds no source; ITEMS 18.3's sources gain `ticketLock` for the Ticket lock, agreed 2026-11-23, cross-document comparison Q-1).

### 10.2 V13 `LootEconomy` (split after the check into static, generation-time and test parts; the M1 parts on F1)

**(Split: lead's call, after the check.)** V8–V12 each name a failure
action; so does every generation-time clause here.

**Phase model (change SV-20 ⚑; lead's call; this owner applies; M1
model, M2 complete; seeds and validation design 5.1, 5.2).** Each check
has a qualified id `V13.CheckName` and a phase: V13s `s` (fails the
build), V13g `g` at AP2, V13t `t` (the (m) test, fails the build). Each
V13g row declares one failure class from its named action: (d)
`defect`; (e) `stageRetry(dealShuffle)`; (f) `report` (its card move
an LR1 repair); (g) by its named action, the waiver; (h), (i)
`repair`; any row with no named action, `defect`. Waves as in 11: M1
(a), (c), (g), (i) on F1, (m); M2 the rest; M3 (o).

**V13s, static over the data** (every floor × party size 1–4 × cap 2 and
3 × variant × unlock set, where the unlock sets are none, all, and each
single unlock):
- (a) Ledger A per floor is 6–8 including the signature (or the
  Viaticum), less 1 on a `campReward` phial's floor (6.13); the boss
  card is outside A; deck size and carried counts fit the channel split.
- (b) Both variants of a floor are **exactly** equal in VP and legal at
  every gate (new lead's call: no 1-VP tolerance).
- (c) Tier legality: minFloor; VR ≤ 1 per floor (a VR signature
  consumes its floor's VR slot, change IR-13; *pending the ITEMS owner*:
  if refused, signatures exempt, with 18.2's fallback);
  `trinketRarePerFloorMax`; worn pieces ≤ U (AR 5.5);
  `scrollFloorCap`; wands F2+; the boss card ≤ R; camp entries ≤ R; the
  Viaticum's options legal at R; every deck card has ≥ 1 legal record at
  its tier and category on the shipping roster, or a logged roster
  fallback (6.3).
- (j) Pegs at every camp: P1; K1 (warn if a re-price breaks equality);
  K2 with the undiscounted price and K2b with the discounted price,
  against the rule-derived expected Wax-Key value (6.4 item 9),
  including camp 4 against F5.
- (k) Restock covered at every camp for the largest legal party at both
  caps, on the per-body ledger (7).
- (l) No loot or coin line reads the actual party size.
- (n) ≥ 3 legal records per `requires` lane (the record's `lootLane`,
  change IR-1) per deck tier *(illustr.)*;
  for each class with no Arcana in kit, the boss-card and Viaticum
  fallbacks have legal gear at every card tier.
- (o) M3: lane-normalised weights and the Due pool after unlocks
  (`UnlockValueNeutral`).

**V13g, at generation**, at AP2, the shared floor assertion point after
LootBind and EventPlace (change SV-9; seeds and validation design 5.4),
each with its named action:
- (d) Offer sets count 1; dreamed options never in VP or caps. *Action:*
  a defect (assert).
- (e) The step rule holds per socket (reference tier = lower median of
  SR0 ordinary cards, excluding the Due and the DH block; allocation,
  allowance ≤ 2, or a logged fit premium); no main-path card is ever
  lowered. *Action:* re-deal with the next within-rank permutation
  (one retry), then convert the failing step to a fit premium. The
  re-deal is the LR2 stage retry on `dealShuffle/retry1`: it re-runs
  GS10 for that kind only, then GS14–GS15; the fit premium after it is
  approved behaviour, reported only, not an LR4 fallback (change SV-10
  ⚑; lead's call; M2; seeds and validation design 5.4, 5.5; the key is
  a pending request to this owner, 6.10 item 1).
- (f) TL 6.17: locked or trapped notables ≤ the quota;
  `NoMechanicsLootLoss` ≤ 10%; secrets ≤ 1 notable, never the top;
  premium and lean bands. *Action:* move the extra card to the
  highest-ranked SR0 container and make its socket coin-only; a band
  miss is logged and the floor ships. *Pending request to this owner
  (segment 12; M2):* `NoMechanicsLootLoss` is a by-class rate, so it
  is read as TL's REP row unless this owner defines a per-floor
  quantity (change SV-24; seeds and validation design 6.10, 14.2).
- (g) The Dream's Due is present (or `DreamsDueWaived`), main path, no
  check, never carried, never substituted; on F1 a U weapon or focus
  for the hero; invariant under intra-party transfers (a property test
  over the fixed deal); a named heirloom case: a hero whose arms hold a
  U-or-better heirloom gets the waiver, never a retarget (change SK-4;
  M3; starting kits design 4.3); widened by change HL-3 (M3): one case
  per heirloom slot class (weapon, focus, shield, armour) and one for
  the Burdened twin (Later), each the waiver (heirlooms and curses
  design 4.1.4). *Pending request (change CA-26; M3; charm abilities
  design 4.11):* one case asserting that a `boonCharm` instance (the
  Bobbin's charm) never sets `armsSatisfied`; if this owner refuses, the
  Bobbin is held (charm abilities design 14.2). *Action:* waiver (6.6).
- (h) F5: coin 0, salvage 0 and their lines hidden; no camp channel, no
  boss card, the Memento at 0 VP; the Viaticum present, all 3 options
  usable, outside the Antechamber, the Circuit and fight hosts; every F5
  fallback converted to sundries within ranges; at most 1 non-notable
  lock; loot-bearing containers ≤ fillers. *Action:* 7c removes the
  extra containers or locks in fixed order (highest socketId first),
  logged. The camp-4 coin warning is present (a UI presence check).
- (i) Coin: pools sum exactly (fallback and withheld coin excluded);
  lumps integer ≥ minimum; the carve ≤ 60% of found coin; F1 main-path
  coin ≥ 17; the Heavy Purse ≤ 20% of found coin, absent on F1 and F5;
  non-healing doses ≥ coin-only locks beyond the carve cap. *Action:*
  the overflow rules of 6.4 item 4, then 7c drops the extra lock.

**Simulator and tests:**
- the coverage bands of 6.4 item 7 (formerly (k)) are simulator reports
  (10.3);
- (m) **an integration test:** draw counts fixed and identical across
  seeds, doors, waves and opening methods; every `loot/floor/{N}` block
  entry consumes its declared count and perturbing one block's entry
  count moves no other block (V19t block independence; the ghost and
  dummy count draws are retired, change SV-5); `loot/run` always 3
  draws; every `lootPlan` grandchild consumes its reserved count; the fourth
  dreamed option drawn on every width-3 offer from M2, door or no door;
  generation committed with the camp-exit save (WO5). The V19g
  DrawLedger calls (m) at every stage end; this row stays its citation
  (change SV-19; seeds and validation design 5.2).

**V14 `ItemRolls` (change IR-21; M1 parts (a), (b), (d), (h), (j) and
the draw-count test; the rest M2)** runs beside V13 and checks 7b's
records, lanes, weights, memory, offers and signatures (item rolls
design 9.2).

**V15 `LootSources` (change LP-31; M1 rows on F1, M2, M3 Windfall
rows)** runs beside V13 and V14, split the same way (V15s, V15g, V15t)
at AP2 after V13g and `V16g.SlotLegal` (change SV-9; seeds and
validation design 5.4), and checks 7c's sockets,
squares, role table, caps, offer pacing, Proprietor tags and the
Windfall; it asserts filters (a)–(g) after step 8 (loot sources design
9.2).

### 10.3 Simulator reports (calibration, read against DH 18.2)

- **Power:** `kPC` (step 1, per ladder tier, measured as in 6.5),
  `GearPowerShare` (the starved arm), `HeroGearVpCurve` /
  `CompanionGearVpCurve` p10 / p50 / p90, `WinRateByLootLuck`,
  `HeroUpgradeDrought`, `HeroDreamsDueShare` (≤ 70% at party size 4),
  `DreamsDueWaivedRate` (by class), `LateFindRelevance`,
  `RequiresHitRate` (by class), `BossCardUsabilityFallback` rate (by
  class).
- **Coin:** `CampCoverage` (the bands of 6.4 item 7, at both caps),
  `SellShareOfCoin`, `CoinUnspentAtRunEnd` (reference and solo),
  `KitSoldAtCamp1` (hero kit gear stays sellable at the Sell rate;
  agreed, starting kits Q4; starting kits design 4.3),
  `ShopTakeRate`, `TonicShareOfSpend`, `CampVpShareOfRun`,
  `StealCoinShare`, `F5Unspent`, `CoinSocketTrimmed`.
- **Risk:** `EliteNetWinDelta`, `RiskSocketNetWinDelta`,
  `PremiumTakeRate`, `LeanRouteShare`, `LeanShareTrimmed`,
  `WaxKeyConsequenceUse` (by lock kind and route), `LootBindRemap` rate.
- **Hoarding:** `HoardIndex`, `TooGoodToUse`, `PotionsHeldAtDeath`,
  `ViaticumBossShare`.
- **Party:** `GearPerBodyByPartySize`, bait value by party size.
- **Clock:** `TallyChaseSeconds` (one report-registry row, owned here;
  other segments read it; change SV-24; M2; seeds and validation
  design 6.10).
- **Loot mix:** `LootMixByCategory`, `SubstitutedVP`.
- Existing reports kept (ITEMS 19.3, CN 19.3, DH 21.5, TK 18.3, TL
  11.4).
- **7b reports (change IR-22; M2):** item rolls design 9.3 (among them
  `RequiresHitRateBySize`, `OfferUsableRate`, `RecordRepeatRate`,
  `RelaxationStepRate`, `StepDownPerRun`, `ShrugShare`,
  `EliteShowsInUseRate`); 7a re-runs `SellShareOfCoin`, K1/K2,
  `CampCoverage` and `LootMixByCategory` against them.
- **7c reports (change LP-32; M2, with M3 Windfall rows):** loot
  sources design 9.3 (among them `LootClicksPerFloor`,
  `ContainersPerFloor`, `OffersPerFloor`, `WindfallEscapedVP`,
  `WindfallEscapedCoin`, `WindfallsPerRun`), with its arms (9.4).

### 10.4 Simulator arms

Reference, lean, full, solo (Lone Road), starved, steep, `SwapChurn`
(CMP 9.3), no-Mechanics (TL 6.17), skilled scout (for the elite question,
Q6), and a Wax-Key-always arm (`WaxKeyConsequenceUse`).

**Qualified arms (change SV-22 ⚑; lead's call; M2; seeds and
validation design 6.2).** Each arm is a vector over the catalogue's
axes, every name qualified: `SwapChurn` is `Muster.SwapChurn`; solo is
`Party` forced 1; no-Mechanics, skilled scout and Wax-Key-always are
`Toggles` baselines; the reference arm is the catalogue defaults; the
other names keep their meaning under their qualified ids.

---

## 11. Staging

| Wave | Contents |
|---|---|
| **M1** | The F1 deck (single variant, inside M1's own split; at most 1 carried weapon or armour); the hero-arms Due on F1; the coin counter (F1 pool 35, main-path floor 17, the salvage share of 4 per fight then per unit, LE-7, LE-8); the LootPlan step on the keyed `lootPlan` stream and the LootBind pass; `LootLedger`; the atomic commit; V13s (a), (c); V13g (g), (i) on F1; the (m) integration test |
| **M2** | Decks F1–F5 with variants; boss card counting; the camp channel of 2 graded for N+1; the coin pool, salvage share, Sell 250‰; K2 / K2b; the full Due with the fixed-deal scorer; the Risk Ledger for consequence locks, locks, secrets, the elite and the bait cache; Wax-Key identities and the mixed-route rule; the fourth dreamed option drawn (not shown); the usability fallbacks; the elite intel line; the F5 reshape (Viaticum, Memento, the camp-4 warning); Treasure count and Grave Goods; the Heavy Purse; category weights and the F5 cap; V13 in full; all reports, kPC first; the camp reward card's content and party-keyed vendor potion stock (camp and hub owner; changes CH-15, CH-16; camp and hub design 7.1, 7.2) |
| **M3** | Wager, Dare, boxed lurker and Strongroom ranks (the Annex has no rank of its own); the Twin Coffer; the Perilous Door reveal (Q5; shown only, the option drawn since M2); unlock normalisation; the memento wall (the Memento wall station in the hub; camp and hub design 5.2); camp hooks if the camp owner takes them (the camp and hub design takes the Tithe Bowl in M3, promotes the Ferryman's Note, then Blood Price, only on `TonicShareOfSpend`, and holds the Wish Later; its 5.4); the Perilous toggle at the camp's door station (change CH-20; camp and hub design 7.6) |
| **M4** | Curses at a discount (8.5; moved from Later by change HL-29 ⚑; agreed, heirlooms and curses Q5; *CON budget reading pending*, else Later unchanged; heirlooms and curses design 10) |
| **Later** | The trader carve (ITEMS 10.9, I-14); daily seeds fixing class; arena-keyed Viaticum tools; the Burdened twin, Redeemed and the Crooked Road rule (8.5; changes HL-23, HL-24, HL-21 as a request) |

---

## 12. What 7a owns, and hooks handed to other segments

**Owns:** the ledgers and budget unit; decks, variants, tiers by depth;
the boss and camp channel tiers and counts; the coin pool, salvage
share and Sell rate; the step rule and allowance; the Due; offer-set
counting; F5 economics; the K2 definition; the allocation algorithm and
the LootBind remaps; the stream plan and commit; V13; the reports.

**Does not own:** records and weights (7b); sockets and placement (7c);
shop presentation and camp sinks (camp and hub); the display (segment
10).

| Owner | Hook |
|---|---|
| **7b, item rolls and tables** | Records per (tier, category); smart-loot and category weights; the Due, fit, triple and dreamed-option filters; substitution; VR gear content (6.13) |
| **7c, sources and placement** | Socket tags and ranks (the input to 7a's allocation, 6.9 item 2); lump placement; the Viaticum room; Twin Coffer placement; trimmed-floor sockets; per-encounter caps; trimming containers on F5 and on coin overflow (6.4, 6.14). **Taken up by the loot sources design (segment 7c, approved 2026-11-22; its 4.4, 11)** |
| **Camp and hub** | Shop presentation; the camp reward card's content; vendor stock scaling; the Wish, the Tithe Bowl (its options filled by segment 10, change XI-24, 8.1), the Ferryman's Note, Blood Price; the coverage targets as design intent. **Taken up by the camp and hub design (segment 13, approved 2026-11-23; changes CH-15, CH-16, CH-20):** the Keeper's Cart and Notables panel (its 4.3); the camp reward card's content (7.1); vendor potion stock keyed to the party and trimmed in Core (7.2); `BuyBatch` Quick Restock (7.3); the coverage targets, checked by `CampRestockCoverage` (7.2, 9.2); the camp-4 line and soft confirm (4.2); the Tithe Bowl at the cart (M3; 4.3); the Ferryman's Note and Blood Price as promotion candidates, the Wish Later (5.4); Grave Goods in the Book of Nights and the Memento wall (M3; 5.2); the Perilous toggle at the door station (M3; 7.6); its request on that toggle, to the RS owner, and XI's Wax Key guard request to this owner stay open (its 11, 14.2) |
| **Segment 8 (Dream Fragments)** | The boss-fact family the elite intel line shares. **Taken up by the events design (segment 8, approved 2026-11-22; changes EV-7, EV-8, EV-15):** the per-floor FactLedger (6.9 item 6); its pending requests to 7a and 7c, `stepFunded` and F-CACHE, stay open (events design 4.4, 11) |
| **Segment 9 (bosses)** | Signature records; the Memento card. **Taken up by the boss rooms design (segment 9, approved 2026-11-22; changes BR-28, BR-29, BR-34, all reading this design unchanged):** the card, Viaticum and Memento interplay (its 4.11.2); the `bossQueue` order behind the elite intel line (its 4.12); the Perilous Door's Nightmare Rendition beside the 1-of-4 card (its 4.14.2; M3) |
| **Segment 10 (UI) and audio** | The Stratum Card order, the floor strip, tier marks, plate loot badges, the Steal preview, the Dream's Due tags, the Viaticum offer UI, the camp-4 warning, ledgers, tags, the audio stings (6.15). **Taken up by the exploration and information design (segment 10, approved 2026-11-23; changes XI-7, XI-11, XI-19, XI-22–XI-24, XI-26, XI-29):** the Treasure field naming, rooms cell and spent field (6.15); F-TREASURE and the Lodestring (8.2); the Tithe Bowl (8.1); its requests to this owner, the exposure ruling (6.9 item 6) and the Wax Key guard (8.1), stay open (its 4.6, 4.9.9, 11) |
| **Segment 11 (seeds)** | The `lootPlan` derivation and keyed grandchildren, the draw table (6.10 item 1), step 3a and the LootBind position, the ghost and dummy draws, the atomic commit. **Taken up by the seeds and validation design (segments 11–12, approved 2026-11-23; changes SV-1, SV-2, SV-5, SV-9, SV-14):** the key algebra and the `lootPlan` registry rows (its 4.1–4.3); step 3 keyed per floor and per entry block, the ghost and dummy draws retired (4.3); GS10 and GS14 in the pipeline (4.4); the atomic commit as WO5 (4.8.3); its request to this owner, the `dealShuffle/retry1` key, stays open (6.10 item 1; its 11, 14.2) |
| **Segment 12 (validation)** | V13s, V13g, the (m) integration test, and the reports. **Taken up by the seeds and validation design (segments 11–12, approved 2026-11-23; changes SV-9, SV-10, SV-19, SV-20, SV-22, SV-24):** V13 in the phase model with failure classes (its 5.1, 5.2); V13g at AP2 (5.4); (e) as LR2 (5.5); (m) called by the V19g DrawLedger (5.2); the arm catalogue (6.2) and the report registry (6.10); its request to this owner, the V13g (f) quantity, stays open (10.2; its 11, 14.2) |
| **Heirlooms and curses (segment 15)** | The heirloom precedence in the Due (6.6 item 2) and the curse hook (8.5). **Taken up by the heirlooms and curses design (segment 15, approved 2026-11-23; changes HL-3, HL-23, HL-29 applied by this owner, with HL-20 and HL-21 as requests):** any heirloom slot waives the F1 Due, with V13g (g) cases per slot class (6.6 item 2, 10.2; M3; its 4.1.4); heirlooms never a Curiosity price (HL-4) and earning, the shelf and Grip-marks as content and display only (6.12; its 4.1); Curses at a discount moved to M4 with Maladies (8.5, 11; HL-29; its 4.2, 10), the CON budget reading pending; the Burdened twin and Redeemed (8.5; Later); its requests, HL-20 (Grave Goods, to DH; 8.2) and HL-21 (the Crooked Road, to the MAIN and RS owners; 8.5; Later), stay open (its 11, 14.2) |
| **Charm abilities (segment 16, the TK 24.2 pass)** | The trinket share and the Due's owned checks, read by the charm lane (6.2, 6.6 item 2, 6.12). **Taken up by the charm abilities design (segment 16, approved 2026-11-24; CA-20, CA-25, CA-26):** the trinket share unchanged, catch charms priced by the existing rule (6.2; its 4.10); the Bobbin boon outside the budget, never arms for the F1 Due (6.6 item 2, 6.12; M3; its 4.11); its requests to this owner, CA-20 (the Due reading) and CA-26 (the V13g (g) case, 10.2), and the lane-weight future item (CA-25) stay open (18.2; its 11, 14.2) |
| **ENM / EP / DH owners** | The elite room's and the Annex's risk cost (LX16; agreed, Q6) |
| **TL owner** | The mixed-route rule and `consequenceExposed`; Unbolting and Unweaving paying the Wax-Key identity; a Barb force consequence if `WaxKeyConsequenceUse` fires; the Ticket peg; fewer locks where the carve cap or F5 fillers bind (6.4, 6.9) |
| **FL owner** | The Viaticum room |
| **CN owner** | The suggester for scrolls; Viaticum options against `tier4ScrollsPerRunBeforeFinalBoss`; the boss-card usability fallback (CN 10) |
| **ITEMS owner** | The signature VR-cap reading (replaced by IR-13's pending request: a VR signature consumes the floor's VR slot; item rolls design 4.13, 14.2) |
| **RS owner** | The Perilous Door reading (Q5); the Memento caption on the tone review list (RS 7.4) |
| **ENM owner** | The elite carrier's gear-only card; the elite intel trigger; step 3a and the dummy count draws (LE-14; the dummies retired and step 3 keyed per block by change SV-5) |
| **DH owner** | The Due reading; lock and overflow sundries re-placed inside the DH block (DH 12.5 step 7(c)); Grave Goods as the run-record form of DH 17.12's unspent line |
| **Companions owner** | Coin keyed by `companionCap` (CMP 7.4, 7.7); the CMP-30 lever as a lever only |
| **Starting kits** | The kit's coin and 0 VP, read by the F1 Due and the camp-1 ledger (3.1, 6.5, 6.6, 7). **Taken up by the starting kits design (segment 14, approved 2026-11-23; changes SK-1, SK-3, SK-4, SK-6, SK-17, with SK-11 and SK-16 as requests):** coin exactly 15 as `CoinChanged{source: kit}` in `LootLedger` (3.1; its 4.1, 4.2); heirloom precedence and the tie-break (6.6 item 2, V13g (g); M3; its 4.3); kit names and the Oddment in Grave Goods (8.2; M2; its 4.5); the stale Sell rate (3.1); `KitSoldAtCamp1` (10.3); its requests, SK-11 (the price scale, to this owner and ITEMS) and SK-16 (shared seeds, to SV), stay open (7, 6.12; its 11, 14.2) |

---

## 13. Binding requirements and how they are met

| Requirement | Source | Met by |
|---|---|---|
| Fixed per-floor budget; enemies never add | MAIN 4.6 (**decided**); ENM 2 (**agreed**) | Ledger A unchanged; coin a fixed pool; salvage inside it |
| Count per channel; 6–8; camp channel | ENM 15.3 step 1 | 6–7 per floor + camp 2 |
| No junk; ≤ 3 per encounter; carried caps | ENM 15.1, 15.3 | Kept; 7c |
| Smart loot, rerolls, family caps, guaranteed never substituted | ENM 15.3 step 2; DH-27 | Kept; the Due is guaranteed |
| Value = tier budget; gates | ITEMS 12.5 | Decks legal by V13s (c) |
| Loot only in ENM 10.5 step 3; fixed counts; locked at generation | ENM 10.5, 15.3 step 5; DH 12.5; MAIN 6.3 (**decided**) | Step 3a on a keyed child stream (LE-14 extends "only in this step" to steps 3a and 3); approved order kept as the block order of keyed `loot/floor/{N}` blocks, ghost and dummy draws retired (change SV-5); the draw-free LootBind |
| LootDoorInvariant | RS 6.4 | No theme, door or Omen input; the fourth option drawn on every seed |
| Coin run-scoped, no meta currency | ITEMS 10.1 (**agreed**) | Kept; F5 coin 0; no interest |
| Salvage 10–15% of found coin | ITEMS 10.2; I-11 | Holds by construction (12%) |
| Vendor list, limited stock, shop-or-reward, no stash | ITEMS 10.3–10.7 (**agreed**) | Referenced only; camp channel count set |
| Pegs P1–P5, K1, K2 | CN 9.3 | V13s (j); K2 defined on a rule-derived value |
| Healing package | DH 12.4–12.5 (client numbers) | Untouched |
| No pity timers | DH 5.4 anti-goal | The Due is standing and announced (told plainly) |
| Camp 1 ≥ 32 coin | DH 21.5 | F1 main-path floor 17 |
| Death targets | DH 18.2 (**agreed**) | Loot last; kPC; trip-wire |
| Hire free; shortfall leaves loot unchanged; `SwapChurn`; cap 2 pre-authorised | CMP 7.4, 7.7 (**agreed**, CMP Q1, Q2), 7.8, 9.3 | Per party, never the actual party (agreed, Q4); the pool keyed by `companionCap` |
| A Steal target per floor; elite Steal < 15% | ENM 10.4, 15.5 | Kept; the purse never on the elite |
| Locks optional; HE containers Plain; contents never destroyed | TL 6.11, 6.13, TL-27 | Kept (the sole exception, a Windfall that gets away, is not a container: M3, change LP-2, loot sources design 4.7.7) |
| ≤ 20% notables behind locks; no-Mechanics loss ≤ 10% | TL 6.17 | The quota (6.4 item 9); V13g (f) |
| +1 step (consequence lock, Dare, Wager) | TL 6.11, 6.15; HZ 6.10 | One reading (6.9) |
| Annex 0 experience, its prize is the reward | EP-26 (**agreed**) | The prize of what it serves (Wager or Dare SR5 step, boxed lurker +0); no second step (6.9 item 1) |
| Bait cache prop family | EP 6.8.1 | A real +0 entry |
| Furnishings never hold loot | ROOM 6.3.4 | The Viaticum is a container |
| Worn pieces Common or Uncommon only | AR 5.5 | V13s (c); body armour and shields follow the ordinary gates |
| Nothing implies who the Dreamer is | RS 7.5 (**agreed**, RS Q3) | The Memento caption is premise-neutral (6.11) |
| `EmptyDeadEndRate` 0; Alcoves on trimmed floors | FL 6.9 | Every container holds something (coin overflow and F5 filler rules, 6.4); the top card on a carrier |
| Content unlocks never power | MAIN 4.7 (**decided**) | Lane-normalised, same tiers |
| Nightmare kind, not stats | RT-21 (**decided**) | Zero economy effect |
| Phial one pre-roll; channels | CN 14.7 (**agreed**) | Kept; minFloor reads the generated floor; the F5 card removed from its channels (LE-5) |
| `tier4ScrollsPerRunBeforeFinalBoss` | CN 13.4, 21 | The VR card and the Viaticum obey it when engaged |
| Classes never grant more loot or gold | CL 6.3 | No class economy verbs |

---

## 14. Risks

| # | Risk | Mitigation |
|---|---|---|
| 1 | **kPC unknown.** Every VP claim rests on k = 1 (ITEMS 12.7, provisional); the toy over-closes | kPC first; the trip-wire; F4 at 6 cards |
| 2 | **Risk does not pay.** If ENM, EP and DH do not move, optional rooms become "never" | Q6 routes the fix to them; the release reports; the elite intel line |
| 3 | **Fixed totals remove the jackpot.** Spiky variants are thin on F1–F3 because of the gates | Tier marks, the half-found "best remaining" reveal, the Wager, the Viaticum and the Twin Coffer, the within-rank shuffles and the SR1–SR2 swap |
| 4 | **Late saturation.** F4–5 finds may mostly be Sell fodder | Sidegrade Dues, VR gear content, `LateFindRelevance` |
| 5 | **The M2 VR roster is one scroll**; F4–5 lean on fit premiums | `SubstitutedVP`; the VR gear content request |
| 6 | **The Due reads party state at generation.** A scorer bug becomes a loot bug; shared seeds differ by party (as smart loot already does) | The fixed deal; the V13g (g) property test; `DreamsDueWaivedRate` by class; `HeroDreamsDueShare` at party 4 |
| 7 | **Coin numbers are a toy ledger**; camp 2 sits below its coverage band; the adopted table was not the toy's tested arm | A toy re-run before M2; the camp-2 levers in 7 |
| 8 | **Solo coin surplus** persists by design | Reported; it buys only capped goods |
| 9 | **Hoarding** may persist despite the cap and suggester | `HoardIndex`; use-side levers first |
| 10 | **Owner dependencies:** DH (the Due reading), TL (the mixed-route rule, Unbolting), FL (the Viaticum room), ITEMS (signature VR), RS (Q5), camp and hub (hooks) | Each has a fallback in 18.2 |
| 11 | **Clock:** the Treasure count may invite completionism | `TallyChaseSeconds` |
| 12 | **All evidence is toy** | Every number is owed a simulator re-run before it settles |
| 13 | **Two generation passes.** LootPlan predicts sockets that later passes can change | Named LootBind remaps, logged; V13g after LootBind; `LootBindRemap` rate |
| 14 | **The Q5 screen.** The client may have meant the ITEMS 10.4 camp reward card | Closed: the client accepted the boss reward card reading on 2026-11-22 (6.12, 18.1) |

Rows 11–14 are in number order (cross-document comparison 2026-11-23,
CC-110).

---

## 15. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: deck shapes other than the base variants
  (within V13s (b)), the variant split, camp channel tiers, coin pools,
  lump weights and minimum, lock lumps, the carve cap, the Heavy Purse
  share, the fallback coin, Sell and Scrap, category weights, the F5
  tactical cap, `requiresBias`, the gear curve, carried counts per floor,
  the shuffle pad and swap rate, the camp Wax Key rate, premium and lean
  bands, report thresholds other than the stated gates.
- Record contents and lane coverage within 6.13 (7b).
- Socket tagging and lump placement within 6.14 (7c).
- Godot scene structure for the Stratum lines, tier marks, the floor
  strip, the coin ledger and the Memento card, with no physics.
- Names, until the naming pass.
- **Not tuning:** the three ledgers and the count as the contract; value
  = tier budget; exact-VP variants; the step allowance ≤ 2 **(agreed,
  Q6)**; no swap-down; the Due every floor, announced, never substituted,
  its parity targets; no pity counters; F5 coin, salvage, camp channel
  and boss card at 0 **(agreed, Q1)**; two identities on a consequence
  cache **(agreed, Q2)**; salvage inside the pool **(agreed, Q3)**; coin
  never read from the actual party **(agreed, Q4)**; the fourth dreamed
  option drawn on every width-3 offer **(agreed, Q5)**; nothing rolls
  after generation; the LootPlan step and the LootBind remaps; the atomic camp-exit commit; camp stock never VR; the boss
  card never VR; no economy effect from ladder, Nightmare tiers or Omens.

---

## 16. Changes to approved documents

Applied on 2026-11-22, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and rows owed by
other owners are listed again in 17. Every row names the exact section
it touches. Rows that touch **decided** or **agreed** text are flagged
in the "Flag" column; **the client's approval of 2026-11-22 covers
them** (with Q1–Q6 where a row names one). **New lead's call:** as in the TL and EP precedents, the
Flag column also marks rows that only *read* agreed or decided text
without changing it, so that every touch of agreed text is visible; and
LE-8, LE-10, LE-13, LE-16, LE-20 and LE-24 are flagged although the
proposal's 0.2 had not flagged them all. Told-plainly rows were told in
18.1 and confirmed by the client on 2026-11-22. Rows marked "request"
are requests to other owners, recorded as pending requests; they change
nothing until their owner rules, and 18.2 names each fallback. M3, M4
and Later rows are recorded now as applied text marked with their wave
and take effect at that wave.

**Readings recorded inside this design (no approved text changes):**
- ITEMS 12.5's "the floor's top allowed tier" reads "the top tier that
  has a legal record for the signature's category" (6.3); the
  signature VR-cap exemption reading is **withdrawn** (change IR-13): a
  VR signature consumes its floor's VR slot, *pending the ITEMS owner*,
  with 18.2's fallback (cross-document comparison 2026-11-23, CC-107);
- TL 6.11's "+1 rarity step inside the budget", TL 6.15's "+1 rarity
  step" and HZ 6.10's "+1 rarity step" read as the one step rule of 6.9;
- TL 6.11's "one camp notables slot" Wax Key reads as a third camp
  display entry outside Ledgers A and B (6.3);
- Wax Key sundries sit outside `sundriesPerFloor` doses (6.2);
- CN 14.7's phial minFloor at a camp reads the generated floor N (6.3);
- DH 5.4's pity anti-goal does not cover a standing, announced Due (6.6;
  the DH owner confirms);
- RS 6.4's LootDoorInvariant holds when the door changes only what is
  shown (6.12; the RS owner confirms);
- `tier4ScrollsPerRunBeforeFinalBoss` is engaged at 1 by default from
  M2 (change IR-16; 3.4); it binds while engaged (cross-document
  comparison 2026-11-23, CC-107);
- Q5's "that floor's camp card" reads as the Perilous floor's boss
  reward card (ENM 17.5), accepted by the client on 2026-11-22 (6.12,
  18.1);
- Q2's "roll or force pays the stepped one" covers forcing a Barb, whose
  consequence never fires on force (6.9 item 4);
- EP-26's "its prize is the reward" reads as the prize of what the Annex
  serves, with no socket of its own (6.9 item 1);
- CMP 7.7's expected-party table is read at the active `companionCap`
  (6.4 item 1).

| # | Document / section (current status) | Change | Flag | Approval |
|---|---|---|---|---|
| **LE-1** | **ENM 15.3 step 1 (change M-5 text, derived from MAIN 4.6, decided)**; ITEMS 2 anti-goal "Grind to win" (kept); ITEMS 12.6 (read) | Three ledgers; the spoils list; step 1's "Value comes only from each item's record" becomes "Each entry's tier is fixed by the floor's spoils list; identity and properties come from the record" (6.1) | **Decided** (MAIN 4.6, kept and strengthened) | Client, with this document; enemies owner applies |
| **LE-2** | ITEMS 12.5 (a new key `loot.rarityDeck` beside `rarity`; minFloor kept); ITEMS 5.1 tuning file; **ENM 15.3 step 1 illustrative counts (agreed lineage, M-5)**; ENM 10.5 step 3 | Rarity decks with base and alternate variants of exactly equal VP (6.3) | **Decided** (MAIN 4.6 lineage) | Client, with this document; items and enemies owners apply |
| LE-3 | ITEMS 12.5 signature bullet (reading: top legal tier for its category; VR cap exemption); ENM 15.1 last paragraph (the card outside the cap, kept); ENM 15.2 Boss row; ENM 17.5 "Signature item" paragraph; CN 10 "Boss reward card" channel row (a usability fallback added, after the check) | Boss channel: signature at the top legal tier for its category; the reward card counted in Ledger B and family caps, outside Ledger A, ≤ R; options usable by the generation snapshot, else redrawn as fit-premium gear (6.3) | — | Lead's call; ITEMS owner confirms the VR-cap reading; CN owner applies the fallback |
| LE-4 | ITEMS 10.4 "Family caps for stock" (read); TL 6.15 Reward bullet (read); ENM 15.3 step 2; ITEMS 18.3 events (`ItemDestroyed` gains the reason `dreamedAway`) | Offer sets count once; dreamed options outside VP and caps; `ItemDestroyed{dreamedAway}` (6.3) | — | Lead's call; enemies and items owners apply |
| **LE-5** | ENM 15.2 Boss row (F5); ENM 15.3 step 1 split (F5); ENM 15.7 (F5); **ENM 17.5 "Signature item" paragraph (F5)**; ITEMS 10.2, 10.4 (no camp channel after F5); ITEMS 12.5 signature bullet (F5: the Memento); **CN 10 "Boss reward card" channel row and CN 14.7 phial channels (agreed, CN Q2; F5 card removed)**; RS 6.5 (read: the Last Hollow as the spend-down camp); FL 6.7, 6.11 and RS Antechamber rows (read: the Viaticum is placed on the main path after the lair, outside the Antechamber, the Circuit and fight hosts); DH 12.5 step 7 (F5 coin fallbacks and lock sundries are doses inside `sundriesPerFloor`; the count is unchanged); RS 7.4 tone review list (the Memento caption) | F5: coin and salvage 0, their lines hidden; no camp channel; no final card; the Memento (premise-neutral caption); the Viaticum (Rare wands and tier-3 scrolls, usable options); F5 coin fallbacks to sundries; F5 fillers and at most 1 non-notable lock (6.4, 6.11) | **Agreed** (CN 14.7); **Q1** | **Client (agreed 2026-11-22, Q1)**, with this document; enemies, items, CN owners apply; FL and ENM owners confirm the room |
| **LE-6** | **ITEMS 10.4 (agreed: shop or reward; `notablesShop` [1, 2] → 2)**; ITEMS 12.5 Depth column (read: camp entries graded for N+1); ENM 15.3 step 1 camp channel; TL 6.11 channels bullet (read: the Wax Key slot as a third display entry); CN 14.7 (read: phial minFloor reads N) | Camp channel of 2 at every camp 1–4, graded for N+1, never VR (6.3) | **Agreed** (ITEMS 10.4, count set inside its range) | Client, with this document; items owner applies |
| **LE-7** | ITEMS 10.2 "Found coin: containers and caches" (gains numbers); ENM 15.7 bullet 2; **CMP 7.7 item 1 (agreed, CMP Q1; read: coin is part of the unchanged per-floor budget)**; **CMP 7.4 (agreed, CMP Q2; read: the pre-authorised cap 2 selects the cap-2 pool row)** | Coin pool 35 / 50 / 70 / 90 / 0 per party at cap 3 and 35 / 50 / 70 / 75 / 0 at cap 2, keyed by `companionCap`, never the actual party; lumps by rank over containers and caches, computed in LootBind; the F1 main-path floor 17; lock lumps; the carve cap and overflow; fallback coin (6.4) | **Agreed** (CMP 7.7, read); **Q4** | **Client (agreed 2026-11-22, Q4)**, with this document; items and enemies owners apply |
| **LE-8** | ENM 15.7 bullets 1 and 3 ("Coin exists, so salvage stays as written"); **ENM 15.1 Issue gear row "armed units add a little salvage coin" and rule 1 "at most it becomes one line of coin" (agreed; kept in meaning)**; ITEMS 10.2 Salvage bullet; I-11 ratio (kept) | Salvage as a 12% pre-assigned share of the pool, per fight then per armed unit, computed in LootBind; the per-unit display kept as the fight total plus a line per non-zero share; Cowed groups pay none (6.4) | **Agreed** (ENM 15.1, kept in meaning); **Q3** | **Client (agreed 2026-11-22, Q3)**, with this document; enemies and items owners apply |
| **LE-9** | ITEMS 5.1 `sellPermille` 400 → 250; **ITEMS 10.6 "one flat rate (illustr. 40% of price) … (agreed)" (the flat rate kept)**; ITEMS 10.9 barter text "between Sell (40%)" (read at 25%); CN 9.2 talisman row "Sell at 40% = 36" (read at 25%: 22) | Sell 250‰, Sell 200‰ fallback; `requiresBias` 4 for the boss card's options (6.2, 6.4) | **Agreed** (ITEMS 10.6, read; the illustrative rate only) | Lead's call (tuning, told plainly); items owner applies |
| **LE-10** | TL 6.11 consequence-lock bullet ("+1 rarity step", lock card hint); TL 6.13 (rank tags); TL 6.14 (rank); **TL 6.15 Reward bullet (agreed lineage, EP Q2 Annex)**; HZ 6.10 Dare container; ENM 15.2 Elite row (guarantee read as allocation); FL Strongroom row (rank); FL Alcove rule (trimmed floors); TL 6.17 loot target (read: the quota and its priority); TL 6.20 consequence-lock counts (read: one quota card per floor); TL 6.22 (LootBind after `trapsResolve`); EP 6.13 (read: the Annex has no prize of its own) | Socket ranks SR0–SR5; the allocation algorithm; the step rule (reference tier, allowance ≤ 2, fit premium); no swap-down; tier marks; caps and the locked-or-trapped quota; the trimmed-floor rule; the LootBind remaps (6.9) | **Agreed** (TL 6.15 lineage, read) | Lead's call; TL, HZ, FL and enemies owners apply |
| **LE-11** | **TL 6.11 consequence-lock bullet and the BQ5 line "roll it, force it, Wax-Key it, or leave it" (the lead's answer accepted by the client)**; TL 6.11 Wax Key route row (read: no consequence, kept); TL 6.11 Unbolting row and Runed row's Unweaving (read: no consequence, so they pay the Wax-Key identity); TL 6.11 Crier sounding (`crierSpent`, read as exposure) | A consequence cache holds a stepped and a Wax-Key identity; `consequenceExposed` selects across mixed routes; Portal locks apply it to one socket beyond (6.9) | **Agreed** (BQ5, accepted); **Q2** | **Client (agreed 2026-11-22, Q2)**, with this document; TL owner applies |
| LE-12 | CN 9.3 K2 bullet (defined; K2b added); TL 6.11 Wax Key bullet (peg wording); DH 12.5 step 7(c) (lock and overflow sundries re-placed by LootBind; the draws still consumed) | K2 on a rule-derived expected Wax-Key value, undiscounted; K2b discounted; the lock contents rule and coin-only consequence locks; F5 K2 (6.4) | — | Lead's call; CN, TL and DH owners apply |
| **LE-13** | ENM 15.3 step 2 (a constrained first container slot, the ghost draw); DH 2 anti-goals and DH 5.4 (f) "pity timers" (reading, lead's call); **DH 5.5 package and 12.4 HE-7 (agreed, DH Q3; read: socket shared, package untouched)**; CMP 7.3 G3 (read); RS 6.9 Stratum Card row (the Due line) | The Dream's Due, scored on the fixed deal of party-owned items (6.6) | **Agreed** (DH package, read, untouched) | Lead's call; DH owner confirms the reading; enemies and RS owners apply |
| **LE-14** | ENM 10.5 step 3 (its sentence "The loot stream is consumed only in this step" becomes "only in steps 3a and 3"; a new step 3a, LootPlan) and its last paragraph; EP 6.1 and TL 6.22 (read: the generation order, with LootBind after `trapsResolve`); **MAIN 6.3 streams (decided; extended by a child stream as FL-2, TR-17, TL-3 and EP-1 did)**; ITEMS 10.4 "Floor N+1 is generated when the party leaves camp" (the atomic commit); ITEMS 18.4; RS 6.4 R-SEED-3 seed tree | The keyed `lootPlan` child stream and its draw table; step 3a; the approved order unchanged, with dummy count draws; the ghost draw; LootBind; the atomic camp-exit commit; `LootLedger` (6.10) | **Decided** (MAIN 6.3, extended) | Client, with this document; RS and segment-11 owners apply |
| LE-15 | FL 9.2 (the FL-20 count line gains a field); RS 6.9 Stratum Card row; DH 17.12 header and its unspent line (Grave Goods becomes its run-record form, in DH's neutral voice) | Treasure count, Grave Goods, the Memento card (8.2, 6.11) | — | Lead's call; FL, RS and DH owners apply |
| **LE-16** | ITEMS 10.2 (found coin gains carriers); ENM 15.2 (coin only); **ENM 15.5 Steal (agreed; change S-5)**: "one of {notable, purse, key}"; TL 6.11 / TL-23 Keybearer; ENM 5.6 loot-mark slot and the glints-per-screen gate; RS 6.9 Stratum Card row ("A heavy purse walks this floor") | The Heavy Purse, bound in LootBind; Steal takes one of three, with a preview; one merged loot badge (8.3, 6.15) | **Agreed** (ENM 15.5, one-try spirit kept) | Lead's call; enemies owner applies |
| LE-17 | ENM 15.3 step 2 (a category multiplier); CN 10 (ranges unchanged); DH 5.7 lever 1 (pre-wired); FL 6.8 forecast (read) | Category weights by depth and the weight composition; the per-run arms floor; the F5 tactical cap; Wax Key sundries outside doses; `foundSupplyPerFloor` (6.2) | — | Lead's call; enemies owner applies |
| LE-18 | ENM 15.2 Elite row (the elite carries gear only); ENM 15.8 floor-fact slot; RS 6.9 | The elite intel line, fired when the elite resolves or its notable is stolen (6.9) | — | Lead's call; enemies owner applies; segment 8 told |
| **LE-19** | **MAIN 4.7 (decided; consistent, read)**; RS 6.4 LootDoorInvariant (read), RS 9.1 Perilous Door, Omens and "Door bounties" rows (read: the reveal adds no draw and is not a bounty); **RT-21 (decided)**; ITEMS 14 (read: the heirloom counts as owned for the Due) | M3 meta: unlock normalisation (the Due pool included), zero Nightmare and ladder effect, Omens never touch spoils, the fourth dreamed option drawn from M2, the Perilous Door reveal on the boss card (the screen accepted by the client on 2026-11-22, 18.1) (6.12) | **Decided** (MAIN 4.7, RT-21, read); **Q5** | **Client (agreed 2026-11-22, Q5)**, with this document; RS owner confirms the reading; M3 |
| **LE-20** | EP 6.8.1 (bait cache contents); **TL 6.14 (agreed, TL Q2: the boxed lurker; contents read)** | Bait cache and boxed lurker contents (6.9) | **Agreed** (TL 6.14, read) | Lead's call; EP and TL owners apply; M2 / M3 |
| LE-21 | TL 6.13 (a new container kind); TL-27 (contents never destroyed: the alternate is a dreamed option, read); ITEMS 18.3 (`dreamedAway`) | The Twin Coffer (8.4) | — | Lead's call; TL owner applies; M3 |
| LE-22 | ITEMS 12.5 ("value equals tier budget exactly" gains a cursed exception); ITEMS 13 | Curses at a discount (8.5) | — | Lead's call; Later |
| LE-23 | RS 11.2 validator table | V13 `LootEconomy` in three parts: V13s static, V13g at generation with named actions, the (m) integration test (10.2) | — | Lead's call; RS owner applies |
| **LE-24** | ITEMS 19.3; CN 19.3; **DH 18.2 lever order (agreed, DH Q11 targets; the lever list extended, targets unchanged)**; DH 21.5 | Reports; "loot last" in the DH lever order, the trip-wire following it; kPC first, with its measurement (6.5, 10.3) | **Agreed** (DH 18.2 lineage) | Lead's call; DH, items and CN owners apply |

Waves: LE-1, LE-2 (F1), LE-7 (F1 pool, main-path floor), LE-8 (the F1
salvage share), LE-13 (F1 hero arms), LE-14 and LE-23 (V13s, the F1
V13g parts and the (m) test) in M1; LE-2 to LE-13, LE-15 to LE-18, LE-20 (bait cache), LE-23
and LE-24 in M2; LE-19, LE-20 (boxed lurker) and LE-21 in M3; LE-22
Later.

---

## 17. Depends on systems not yet designed

The owed rows are the single list in 16; by owner:

- **The enemies owner:** LE-1, LE-2 (15.3, 10.5), LE-3 (15.2, 17.5),
  LE-4, LE-5 (15.2, 15.3, 15.7, 17.5), LE-7, LE-8 (15.1, 15.7), LE-10
  (15.2), LE-13 (15.3 step 2), LE-14 (10.5 step 3a), LE-16 (5.6, 15.5),
  LE-17, LE-18 (the gear-only elite card, 15.8); **the risk
  request (agreed, Q6):** if `EliteNetWinDelta` misses −5 to +2, the
  elite room's death cost (budget, watcher rules) and experience (ENM
  15.2, 16) move, not loot.
- **The encounter placement owner:** LE-10 (EP 6.13, the Annex read),
  LE-14 (EP 6.1, the order), LE-20 (6.8.1); the Annex's net cost under
  Q6.
- **The DH owner:** LE-5 and LE-12 (doses re-placed inside DH 12.5),
  LE-13 (the pity reading, including the stated steering route), LE-15
  (17.12, Grave Goods as its run-record form), LE-24 (the lever order
  and the reordered trip-wire, 21.5); Q6's DH 18.2 tuning.
- **The items owner:** LE-2, LE-3 (the signature VR-cap reading), LE-5,
  LE-4 and LE-21 (18.3 `dreamedAway`), LE-6, LE-7, LE-8, LE-9, LE-14
  (18.4), LE-16, LE-22, LE-24 (19.3).
- **The consumables owner:** LE-3 (the CN 10 usability fallback), LE-5
  (CN 10, 14.7), LE-9 (CN 9.2 talisman Sell), LE-12 (K2, K2b), LE-24
  (19.3); the scroll suggester (6.7); Viaticum options.
- **The traps owner:** LE-10 (6.17, 6.20, 6.22), LE-11 (the mixed-route
  rule; Unbolting and Unweaving pay the Wax-Key identity), LE-12, LE-16,
  LE-20 (6.14), LE-21; a Barb force consequence if its report fires;
  fewer locks where the carve cap or the F5 fillers bind.
- **The hazards owner:** LE-10 (HZ 6.10 Dare); Dare width by attempt.
- **The floor layout owner:** LE-10 (Strongroom, Alcoves), LE-15 (FL-20
  count line); the Viaticum room.
- **The RS owner:** LE-5 (the Memento caption on the tone review list),
  LE-13, LE-15 and LE-16 (Stratum Card), LE-14 (seed tree), LE-18,
  LE-19 (the Perilous Door reading, Door bounties), LE-23 (V13).
- **The armour owner:** AR 5.7's per-slot arithmetic recomputed against
  the decks (6.3).
- **The trinkets owner:** `trinketRarePerFloorMax` against the decks;
  TK 18.3 reports kept.
- **The companions owner:** LE-7 (the pool keyed by `companionCap`, CMP
  7.4); CMP-30 `anyCompanion` stays a lever that fires on its report
  (6.8); no change row.
- **Dungeon generation segments 7b, 7c, 8–12:** every hook in 12.
- **The camp and hub design:** the sinks, coverage targets and vendor
  scaling of 6.4 and 6.8; the camp-4 warning. The ITEMS 10.4 camp
  reward card owes no fourth option: the client read Q5 as the boss
  reward card on 2026-11-22 (6.12). **Taken up by the camp and hub
  design (segment 13, approved 2026-11-23; changes CH-15, CH-16, CH-20;
  its 7.1, 7.2, 7.6, 17).**
- **The simulator:** the arms of 10.4 and the reports of 10.3, kPC
  first; the toy re-run on the adopted coin table.
- **Naming pass:** every placeholder name here.

---

## 18. Open items for the client

None. Approved by the client on 2026-11-22.

The client approved this document as a whole on 2026-11-22, including
every lead's call (the lead's calls of the answered proposal, the new
lead's calls in 18.1 and the lead's calls after the check in 18.1), the
told-plainly items (including "Told plainly after the check" in 18.1)
and the readings recorded in 16, and its approval covers the flagged
rows of 16. The one item that needed the client's explicit answer, which
screen Q5's "camp card" names, was answered on 2026-11-22: the client
accepted this document's reading, the Perilous floor's ENM 17.5 boss
reward card, shown before its camp, which becomes choose 1 of 4 (18.1,
6.12). The six questions it raised were answered on 2026-11-22
**(agreed)**; the client also said everything else in the proposal
stands as written. The future items in 18.2, which come back only if a
report, an owner's refusal, a wave or a later design triggers them, and
the naming pass remain future items.

### 18.1 This document

**The six answers (agreed, 2026-11-22), each the recommended option:**

- **Q1, floor 5 pays before the arena:** option A. F5 coin and salvage
  0; no camp channel after F5; no final reward card; the final boss
  carries and uses its signature, which drops as a 0-VP Memento; its
  share becomes the Viaticum, a choose-1-of-3 of boss tools before the
  arena; the Last Hollow is the spend-down camp (6.11; LE-5). Rejected:
  B, keep F5 as approved.
- **Q2, the Wax Key and consequence locks:** option A. The cache holds
  two pre-rolled identities; roll or force pays the stepped one, a Wax
  Key the ordinary one (6.9; LE-11). Rejected: B, the consequence still
  fires; C, leave it.
- **Q3, salvage as a share of a fixed coin pool:** option A. 12% of the
  floor pool, pre-assigned to armed units; the per-unit line still
  shows; Cowed groups pay none (6.4; LE-8). Rejected: B, keep the
  per-unit formula.
- **Q4, coin per party:** option A. Coin per party from the
  expected-party table; solo and full parties get the same coin (CMP 7.7
  holds) (6.4, 6.8; LE-7). Rejected: B, +15% per companion.
- **Q5, Perilous Door loot (M3):** option A. It reveals the pre-drawn
  fourth shadow option on that floor's camp card; draws are
  door-independent; the RS owner confirms (6.12; LE-19). The screen:
  the Perilous floor's ENM 17.5 boss reward card, choose 1 of 4
  (accepted by the client on 2026-11-22; below). Rejected: B, a profile
  mark only.
- **Q6, the risk incentive fix:** option A. If the M2 simulator still
  shows the elite room and the Annex as a large net loss, the fix comes
  from encounter and experience tuning (ENM 15.2, 16; EP; DH 18.2); loot
  stays fixed, paying risk only in allocation, the elite intel line,
  tier pips and choice width (6.9). Rejected: B, step allowance 3.

**Which coin table (stated for the record).** The proposal gave the
adopted pool (35 / 50 / 70 / 90 / 0 per party) and the toy's tested
Hmid fix (35 / 45 / 55 / 65 / 0 with +15% per companion). This document
adopts the per-party pool the proposal recommended; Hmid stays as toy
evidence in 9 (6.4 item 1).

**New lead's calls (made while writing; confirmed by the client on
2026-11-22, with this document):**
1. **Exact-VP variants and fixed alternate shapes** (6.3; V13s (b)): F1
   one variant; F2 and F4 a fit variant (the same cards, one card a
   fit premium); F3 spiky C U R R R R; F5 spiky C R R R R VR. The
   proposal's "within 1 VP held in the allowance" tolerance is dropped.
2. **`tier4ScrollsPerRunBeforeFinalBoss` read as off by default** (3.4,
   6.3, 6.11); a VR card drawn down to R in M2 logs its shortfall in
   `SubstitutedVP`, with no coin added.
3. **Socket ranks renamed SR0–SR5** (the proposal's R3 and R4 collide
   with RS route rules).
4. **Integer coin split:** salvage `FloorDiv(pool × 120, 1000)`; the
   Heavy Purse `FloorDiv(found × 200, 1000)`, so F2's purse is 8, not
   the proposal's 9 (6.4 items 3–4).
5. **Fallback coin** (a waived Due, a gate-blocked step with no lane) is
   added on top of the pool, logged, never on F5, and excluded from
   V13g (i)'s pool sum (6.4 item 8).
6. **The Due takes the deck card at its tier**, so the deck's VP is
   unchanged (6.6; the card rule made exact after the check).
7. **The Due's ghost draw:** the first container slot's approved parent
   draw is made and discarded; the Due's identity is drawn on `lootPlan`
   (6.10 item 3).
8. **The Wax-Key identity's tier** is one tier below the stepped
   identity (never below C), or the same tier without the fit filter
   where the step was paid by a fit premium (6.9 item 4).
9. *(Withdrawn after the check: the draft had Unbolting pay the stepped
   identity; see "after the check" item 6.)*
10. *(Moved after the check to its own heading below: the Perilous Door
    reading.)*
11. **Client keys marked not tuning:** the step allowance 2 (Q6), the F5
    zeros (Q1) and coin never reading the actual party (Q4) (10.1).
12. **Flags added in 16:** LE-8, LE-10, LE-13, LE-16, LE-20 and LE-24
    flagged, and reads of agreed or decided text flagged, as in the TL
    and EP precedents.

**Differs from the wording of an agreed answer (Q5): accepted by the
client on 2026-11-22.** The client agreed that the Perilous Door "reveals the
pre-drawn fourth shadow option on that floor's camp card". Three
approved screens could be called a camp card: the **ENM 17.5 boss reward
card** (the width-3 offer at the end of the floor, before its camp); the
**ITEMS 10.4 camp reward card** (the alternative to the notables shop,
which ITEMS 10.4 says is not the boss reward card, and whose content
belongs to the camp and hub owner); and the **RS 6.9 camp next-floor
card** (no loot). This document reads the words as the **boss reward
card**, the only width-3 offer shown before that camp with a pre-drawn
fourth option, and builds it so (6.12). Had the client meant the ITEMS
10.4 camp reward card, the reveal would have become a hook to the camp
and hub owner with its own pre-drawn fourth option, and the boss card
would have stayed width 3. **The client's answer (2026-11-22):** the
client accepted this document's reading: the Perilous floor's ENM 17.5
boss reward card, shown before its camp, becomes choose 1 of 4; the
camp reward card alternative is not adopted. (Also: the client's
"shadow option" is renamed "dreamed option", and "tier pips" "tier marks", as
placeholder names only, 3.5.)

**Lead's calls after the check** (each marked "(lead's call, after the
check)" where it stands; confirmed by the client on 2026-11-22, with
this document; none overrides a client answer):
1. **Generation in two steps:** LootPlan (step 3a, on `lootPlan`, over
   the provisional socket set) and the draw-free LootBind pass after
   `trapsResolve`, with named remaps; V13g after LootBind (6.9 items 2,
   7; 6.10 item 4).
2. **One allocation algorithm:** the channel split with
   `loot.carriedPerFloor`, reservations from DH socket sets only, the
   sort, the hard filters in order (the quota, secrets, the elite's gear
   card, trimmed floors, carrier-legal subsets), the deal, within-rank
   shuffles plus one cross-rank swap, parent channel slots mapped to
   sockets (6.9 item 2).
3. **The keyed stream:** `lootPlan` per floor; a keyed grandchild per
   consumer; 9 draws reserved per identity; the draw table; dummy count
   draws; every boss-card option on `lootPlan` (6.10).
4. **The step rule's reference tier** is the lower median of SR0
   ordinary cards excluding the Due and the DH block; raises in socket
   order (6.9 item 3).
5. **The locked-or-trapped quota** goes Wager first, then a consequence
   lock, then other locks; a coin-only consequence lock pays its step as
   a larger lump, withheld on a Wax Key (6.4 item 9).
6. **Mixed routes:** `consequenceExposed` selects the identity; Unbolting
   and Unweaving pay the Wax-Key identity (replacing the draft's call 9:
   TL 6.11 gives Unbolting no consequence, so the draft leaked the
   premium to the Arcanist and the Herald); Portal locks apply the rule to
   one socket beyond (6.9 item 4).
7. **LootBind remaps:** a lock downgraded to Plain pays its Wax-Key
   identity and releases its step; a reverted Wager moves its first
   option; a dropped boxed lurker keeps its entry; the Annex has no prize
   of its own (6.9 items 1, 7).
8. **The Due's scorer:** `loot.gearCurve`, the fixed deal of
   party-owned items (one item fills one member's slot), deficit,
   target, slot, card and waiver without retarget; the F1 Due always U;
   `HeroDreamsDueShare` ≤ 70% at party size 4 (6.6).
9. **Weight composition and filters:** one formula; `requiresBias`
   (renamed from `loot.partyWeight`) 4 only for the boss card; the Due,
   fit premiums and the arms floor as filters; the F5 tactical cap
   enforced during the F5 draws; the arms floor at F2's LootPlan (6.2).
10. **Coin:** the pool keyed by `companionCap` (cap 2: 35 / 50 / 70 / 75
    / 0); coin sockets are containers and caches only; the carve order
    and 60% carve cap; overflow to sundries, then 7c trims a container;
    lock and overflow sundries are DH-block doses re-placed by LootBind
    (6.4 items 1, 4).
11. **Salvage per fight, then per unit,** over the final placement, with
    the display read as the fight total plus a line per non-zero share
    (6.4 item 2).
12. **K2 and K2b on a rule-derived expected value,** K2 undiscounted,
    K2b discounted, checked statically (6.4 item 9).
13. **F5:** coin and salvage lines hidden; at most 1 non-notable lock;
    loot-bearing containers ≤ fillers (6.4 item 5).
14. **Usability:** the boss card and the Viaticum read the generation
    snapshot; unusable options become fit-premium gear (6.3, 6.11); the
    F4 VR is Icefall only when someone can read it (6.3); the elite
    carries gear only (6.9 item 2).
15. **The Viaticum:** Rare wands and tier-3 scrolls only (no Rare bomb
    exists); main path after the lair, outside the Circuit and fight
    hosts, never a side room; its Stratum line and offer UI (6.11).
16. **The Memento caption** "carried out of the Heart", on the victory
    screen and run record only (6.11).
17. **The Heavy Purse** bound in LootBind by a fixed chain (8.3); one
    merged loot badge on plates; the Steal preview (6.15).
18. **The Treasure count** falls on every seen loss; "best remaining"
    from unfound entries; the Viaticum in F5's n; the F1 secret as "+1
    secret" (8.2).
19. **Grave Goods** as the run-record form of DH 17.12's unspent line, in
    its voice, for wins and losses (8.2).
20. **UI and audio hooks:** the Stratum Card order and the floor strip;
    tier marks; the Dream's Due tags and states; the elite intel trigger
    and surfaces; the camp-4 warning; the Perilous mark; the audio stings
    (6.9 item 6, 6.12, 6.15).
21. **The fourth dreamed option is drawn from M2** on every width-3
    offer, so switching the reveal on in M3 changes no seed (6.12).
22. **Names:** "dreamed option" (for "shadow"), "tier mark" (for "tier
    pip"), `loot.requiresBias`, `dreamsDue` in keys and UI (3.5; A.5).
23. **V13 split** into V13s (static), V13g (generation-time, each with an
    action) and the (m) integration test; unlock sets limited to none,
    all and each single unlock (10.2).
24. **Reports and records:** item provenance fields, the `LootLedger`
    additions, the kPC measurement and the 0.65–1 interpolation (6.5,
    10.1, 10.3).
25. **The trip-wire follows DH 18.2:** encounter rows first, then the Due
    and fit, then the camp second entry, then the decks (6.5).
26. **The camp ledger recomputed per body;** camp 2's first lever is its
    first entry R → U (7).

**Told plainly; confirmed by the client on 2026-11-22:**
- The proposal's draft variant cells did not all sum (its F3 spiky came
  to 13); the fixed shapes replace them (6.3).
- The adopted coin table is not the toy's tested arm; the toy must be
  re-run on it before M2, and camp 2 sits below its coverage band in the
  scratch ledger (6.4, 7).
- Risk does not pay in loot inside a fixed budget (the elite room: −17
  win points against +0.8); the fix is routed to encounter and
  experience tuning (6.9; agreed, Q6).
- At k = 1 every model over-closes by about 1.7–2.4× at F4; kPC is the
  simulator's first step (6.5).
- The Due is a lead's reading of the DH 5.4 anti-goal, not an exception
  to it; the DH owner confirms (6.6).
- The M2 VR roster is one scroll (Icefall); F4–5 lean on fit premiums
  until VR gear records exist (6.3).
- K1 holds only with equality at the 25% discount (18 = 18) (6.4).
- ITEMS 19.3's "≥ 70% of notables after F2 equipped" will likely fail on
  F4–5; `LateFindRelevance` ≥ 50% is the working target (6.5).
- The solo coin surplus is by design (6.4).
- On F2 a consequence lock pays fit, not tier; on F1 allocation usually
  pays the step (6.9).
- The swap-down tax and the "Common becomes a sundry plus 20 coin"
  step-down are deleted (6.9).
- Wax Key sundries sit outside `sundriesPerFloor`; the TL camp slot's
  Wax Key is a third display entry; the phial's minFloor at a camp reads
  N (6.2, 6.3).
- The corrections to the brief in 3.4 ("Pilfer", hire cost, pity
  timers, Dream Fragments, TL 6.11 not 6.12, the Dreamer's Heart).

**Told plainly after the check** (confirmed by the client on
2026-11-22, with this document; none changes a client answer):
- **The draft's generation order did not work.** It drew loot tiers in
  ENM 10.5 step 3, before carriers, composition and `trapsResolve`
  existed, and had no rule for sockets those later passes change. The
  LootPlan step and the LootBind pass fix this (6.9, 6.10).
- **Q5 names a screen this document reads differently** (the heading
  above; the client accepted the reading on 2026-11-22).
- **Q2's words are kept for the Barb:** forcing a Barb pays the stepped
  identity though the Barb never fires on force; the exposure is force's
  noise. The TL owner may add a force consequence if the report fires
  (6.9 item 4).
- **Q2's mixed routes:** a party that tries a pick or force and then uses
  a Wax Key gets the stepped identity, because it took the risk; a
  party that only uses a Wax Key, Unbolting or Unweaving gets the
  ordinary one (6.9 item 4).
- **Q3's "per-unit line still shows"** is read as: the fight's salvage
  total, with a line for each unit whose share is not 0; F1's share of
  4 coin would otherwise read "+0" on most units (6.4 item 2).
- **Q4 holds at both companion caps:** the pool is keyed by the build's
  `companionCap`, never the party; at cap 2 the F4 pool is 75, not 90,
  or camp 4 would be overfunded by about 25% (6.4 item 1).
- **Taken VP can fall below base** by 1 per consequence cache opened
  safely; that is the agreed price of the safe route (Q2) (6.1).
- **The Due can be steered by declining pickups** on F2 and F4, within
  one card's worth; told to the DH owner with the LE-13 reading (6.6).
- **The draft's "armour ≤ U" was wrong:** AR 5.5 caps only worn pieces;
  body armour and shields can be Rare (3.1, 6.9, V13s (c)).
- **The draft's Viaticum could offer bombs at Rare,** which do not exist;
  and its boss card and Viaticum could offer nothing usable to a martial
  party without Arcana. Both are fixed by fallbacks (6.3, 6.11).
- **The draft's elite could carry the run's best scroll and cast it at
  the party.** The elite now carries gear only (6.9 item 2).
- **The draft's Memento caption implied who the Dreamer is** (against RS
  7.5); replaced (6.11).
- **The draft's coin share claim** ("10–15% of a floor's value") did not
  match its own numbers: the pool is about 15–18% of notable value in
  gold-equivalent (0.1).
- **The draft's camp-2 lever was a no-op** and its restock column did not
  scale with bodies; both are fixed (7).
- **The draft's trip-wire made loot the first lever,** against "loot is
  the last lever"; it now follows DH 18.2 (6.5).

### 18.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal, a
wave or a later design triggers them; each fallback is a new lead's call
(confirmed by the client on 2026-11-22, with this document):
- **If the M2 simulator still shows the elite room or the Annex as a
  large net loss (`EliteNetWinDelta` below −5):** the ENM, EP and DH
  owners tune the elite room's cost and experience (agreed, Q6); if they
  cannot, the miss goes to the client, and loot still stays fixed.
- **If kPC ≥ 1:** the F2–F3 decks lean one card toward C / U, and by the
  linear share between 0.65 and 1 (6.5); no client change.
- **If F3–5's share of deaths falls below 60%:** the trip-wire of 6.5,
  DH 18.2's encounter levers first.
- **If `SellShareOfCoin` exceeds 40%:** Sell 200‰ before any pool
  change (6.4).
- **If camp 2's coverage stays below 0.6 after the re-run:** camp 2's
  first entry drops R → U (tiers U, U), then an F2 pool of 55 (7).
- **If the DH owner refuses the Due reading (LE-13):** the Due ships
  with the hero as the target on every floor (no lowest-member
  alternation) and still announced; if the owner refuses the Due
  outright, it goes back to the client.
- **If the ITEMS owner refuses "a VR signature consumes the VR slot"**
  (change IR-13, replacing the earlier VR-cap exemption fallback): LE-3
  stands as approved, signatures exempt from the VR cap (V13s (c)), and
  a floor whose field deck holds a VR draws its signature at R +
  authored (item rolls design 4.13, 14.2).
- **If the TL owner refuses LE-11's Unbolting and Unweaving reading:**
  Unbolting and Unweaving fire the lock's consequence, and then pay the
  stepped identity (the risk is taken); they never pay the stepped
  identity with no consequence.
- **If `WaxKeyConsequenceUse` shows Barb forcing dominates:** the TL
  owner may give the Barb a force consequence; Q2's wording stays.
- **If the RS owner refuses the Perilous Door reading (LE-19):** option B
  applies (a profile mark and a memento-wall frame), since a door that
  changes draws would break LootDoorInvariant.
- *(Closed on 2026-11-22: the client accepted the boss reward card
  reading of Q5, so the ITEMS 10.4 camp reward card fallback is not
  needed, 6.12.)*
- **If the FL owner cannot place the Viaticum on the main path after the
  lair outside the Circuit and fight hosts:** a container in the lair
  room, never a side room, never the Antechamber. *(Read by 7c: on F5
  nothing lies between L and F, so this fallback is the real rule, the
  lair room's host socket; pending FL and ENM confirmation. If they
  refuse the reading, the primary rule stands and segment 2 adds a
  post-lair main-path container on F5; loot sources design 4.10, 14.2.)*
- **If F5 K2 or the F5 fillers cannot be met:** segment 5 drops the
  extra F5 lock, and 7c the extra container (6.4).
- **If `TonicShareOfSpend` exceeds 25%:** one camp hook is promoted to M2
  by the camp and hub owner: the Ferryman's Note first, then Blood Price,
  never camp 4 (camp and hub design 5.4, 14.2).
- **If `CampChoiceShare` falls outside 35–65% *(illustr.)*:** price is
  the knob; no fourth option (camp and hub design 7.1, 14.2; lead and
  this owner).
- **If `PotionsHeldAtDeath` median ≥ 2:** the suggester and potion-use
  UI move first, never the DH package (6.7).
- **If `HeroDreamsDueShare` exceeds 70% at party size 4:** ties in the
  F2 / F4 target go to the lowest-level member instead of the hero.
- **Pending requests from segment 8 (changes EV-7, EV-15; to this owner
  and 7c):** if `stepFunded` is refused, there are no priced lock routes;
  if F-CACHE is refused, its card gains F-ROOM (6.9 item 6; events
  design 14.2).
- **Pending requests from segments 11–12 (to this owner):** if the
  `dealShuffle/retry1` key is not confirmed, the key stands as written;
  if no per-floor V13g (f) quantity is defined, `NoMechanicsLootLoss`
  stays TL's REP row (6.10, 10.2; seeds and validation design 14.2).
  *(The SV-5 refusal fallback is retired: SV-5 was approved on
  2026-11-23; cross-document comparison 2026-11-23, CC-108.)*
- **Pending items from the starting kits design (segment 14; its 14.2):**
  if the DH or IR owner refuses change SK-4, the tie-break gap remains
  and heirlooms ship only with a named V13g (g) case agreed by this
  owner; if `KitSoldAtCamp1` shows camp-1 coverage moved beyond 6.4's
  band, the held `kitIssue` 0-Sell lever goes to the client, never
  applied without it; if F1 solo deaths for a kit exceed 5%, a held
  class-themed C dose in the empty belt slot, carved from F1 sundries,
  goes to the DH owner and this owner; SK-11 and SK-16 as pending
  requests (7, 6.12).
- **Pending items from the heirlooms and curses design (segment 15;
  its 14.2):** if the DH or IR owner refuses HL-3's any-slot waiver, no
  armour or shield heirloom templates ship at M3 and three weapon or
  focus families replace them (6.6 item 2); heirlooms do not ship until
  SK-4's IR, DH and LE confirmations close; if CON refuses HL-29's
  budget reading, Curses at a discount stays Later unchanged (8.5, 11);
  if `CursedTakeRate` falls below 20%, the benefit budget is reviewed
  with this owner, never as a loot lever; HL-20 (Grave Goods, to DH;
  8.2) and HL-21 (the Crooked Road, to the MAIN and RS owners; 8.5;
  Later) as pending requests.
- **Pending items from the charm abilities design (segment 16; its
  14.2):** CA-20 (the Bobbin's Due reading, 6.6 item 2, 6.12) and CA-26
  (the V13g (g) `boonCharm` case, 10.2) as pending requests to this
  owner (M3); if this owner refuses CA-26, the Bobbin is held until it
  can be placed; the charm lane weight (CA-25, 6.2) is a future item for
  the IR and LE owners and the client, triggered by CG1/CG2.
- **The naming pass** renames every placeholder.

---

## Appendix A: how the critique findings were resolved, and names

### A.1 Fun critique (LF)

| Id | Finding | Resolution |
|---|---|---|
| LF1 | Risk is a losing trade; optional content becomes a trap; the Annex near-pure downside | Q6 (agreed) with release reports (`RiskSocketNetWinDelta` −5 to +2); the elite intel line; the elite item shown in use; the Annex pays through the Wager or Dare it serves, with that socket's step (6.9) |
| LF2 | Judges split on the step; swap-down punishes the safe player | One reading: allocation, allowance ≤ 2, fit premium; swap-down deleted (6.9) |
| LF3 | Gear saturates by F4; the Due waives late; F4's first VR is a scroll | kPC and F4 at 6 cards; F4–5 sidegrade Dues; the VR gear content request; `LateFindRelevance` (6.5, 6.6) |
| LF4 | The floor becomes fully readable | Spiky variants where gates allow; "best remaining" revealed after half found; within-rank shuffles and the SR1–SR2 swap (6.3, 6.9, 6.15) |
| LF5 | Recruits steal the hero's Due | The parity rule: hero on F1, F3, F5; `HeroDreamsDueShare` ≥ 40% (6.6) |
| LF6 | Tactical and potion hoarding | The F5 tactical cap; the Viaticum shape; the suggester hook; `PotionsHeldAtDeath` (6.7) |
| LF7 | Coin has nothing fun to buy early | The camp-1 affordability check; the tonic-share signal; "the Hollow ahead sells rare" (6.4, 6.15) |
| LF8 | Lane fairness unverified | V13s (n); reports by class (6.6) |
| LF9 | F5 loses its fanfare | The Memento card and the memento wall (6.11) |
| LF10 | Meta hooks weak | The Perilous Door reveal (agreed, Q5); its fallback is a cosmetic mark (18.2) |
| LF11 | Tally completionism; unseen spends feel bad | `TallyChaseSeconds`; decrement on every seen loss; the carried glint (approved) (8.2) |
| LF12 | Reload leak; per-companion coin split; purse glint | Atomic commit (LE-14); per party (agreed, Q4); a coin-disc badge in the loot-mark slot (6.15) |

### A.2 Balance critique (LB)

| Id | Finding | Resolution |
|---|---|---|
| LB1 | A Wax Key buys the consequence premium risk-free; K2 vacuous; Tattle bypasses the Annex wake | Two identities (LE-11; agreed, Q2) with the mixed-route rule; K2 and K2b on a rule-derived value (LE-12); the Tattle beyond-room's Portal applies the rule to one socket beyond (6.9) |
| LB2 | Nothing pays for the elite room; experience does not either | Told plainly; routed to ENM, EP, DH (agreed, Q6); the elite intel line; the lean band 45–55% |
| LB3 | Per-actual-companion coin snowballs and contradicts CMP 7.7 | Coin from the expected-party table, never actual (LE-7; agreed, Q4) |
| LB4 | F5 coin leaks through "+20 coin" fallbacks | All F5 coin fallbacks become sundry doses; V13g (h) (6.4) |
| LB5 | The camp channel front-loads VP; VR stacking | Camp entries never VR; the second entry at the lower tier; `CampVpShareOfRun`; the trip-wire trims it after DH 18.2's encounter levers and the Due (6.3, 6.5) |
| LB6 | The pre-arena cache trips the boss-damage alarm; the antechamber is patrolled | Tool limits; never the Antechamber, a Circuit room or a fight host; `ViaticumBossShare`; FL confirms (6.11) |
| LB7 | The Due steerable by transfers or empty slots | The fixed deal of party-owned items; the V13g (g) property test; the pickup route stated plainly (6.6) |
| LB8 | The Due before the DH block shifts approved draws | Approved order unchanged; new draws on the child stream; the ghost draw (6.10) |
| LB9 | Swap against allowance | Allowance adopted; swap deleted (6.9) |
| LB10 | Reload leak at camp | Atomic commit; V13 (m) test (6.10) |
| LB11 | Steal + purse + Light Fingers | The purse never on the elite; Steal takes one of three; salvage forfeited; `StealCoinShare` (8.3) |
| LB12 | Haul premium; solo surplus | Sell 250‰ (200 fallback); `requiresBias` 4 for the boss card, the Due a filter; the solo report (6.4) |
| LB13 | Boss card tier against VR | Card ≤ R (LE-3) |
| LB14 | Over-closure | kPC first; trip-wire at F3–5 deaths < 60% (6.5) |
| LB15 | Trimmed floors concentrate value | The top card on a carrier; `LeanShareTrimmed` (6.9) |
| LB16 | Tally and Tithe leaks aim the Wax Key | Decrement only on seen losses; the Tithe hook names "behind risk" only and is off with a Wax Key (8.1, 8.2) |
| LB17 | M3 hooks add power or coin | Ferryman capped at 30%, once, never camp 4; Blood Price uses the allowance, never VR; curses never F1 or on a Due; the Due pool normalised (8.1) |
| LB18 | Renumber; K1 edge; camp-1 floor | One register; K1 stated; F1 main-path coin 17 (6.4) |

### A.3 Consistency critique (LK)

| Id | Finding | Resolution |
|---|---|---|
| LK1 | F4 reaches 9 counted entries | The boss card outside Ledger A (consistent with ENM 15.1) **and** the F4 field deck cut to 6 cards; V13s (a) states the ledger (6.3) |
| LK2 | K2 fails at camp 4 with F5 coin 0 | The F5 lock contents rule (an R card or 3 sundry doses, at most 1 non-notable lock); segment 5 drops an extra lock if needed; V13s (j) (6.4) |
| LK3 | Coin injected on F5; the swap sundry breaches ranges | F5 fallbacks to sundries within all ranges; the swap step deleted (6.4, 6.9) |
| LK4 | Three step readings; TL 6.11 not 6.12 | One reading in LE-10 naming TL 6.11, TL 6.15, HZ 6.10; the section corrected (3.4) |
| LK5 | "Hero first when below the curve" is dry-spell pity | Replaced by a standing parity rule; the DH owner confirms the reading; counters rejected (6.6) |
| LK6 | No VR gear in M2; the F4 signature impossible | The signature at the top legal tier for its category; F4 VR Icefall (when someone can read it) or fit; the V13s (c) roster check (6.3) |
| LK7 | The F5 reshape touches more text | All sections listed in LE-5 (CN 10, CN 14.7, Antechamber placement) |
| LK8 | The salvage change touches agreed rows | LE-8 lists ENM 15.1, 15.7 bullets 1 and 3, ITEMS 10.2; display (read as the fight total plus non-zero lines) and the Cowed rule kept; flagged |
| LK9 | +15% per companion conflicts with per party | Rejected (Q4, option B) |
| LK10 | Marked Purse: Steal of coin; "Marked" retired | Renamed Heavy Purse; ENM 15.5 and ITEMS 10.2 flagged (LE-16) |
| LK11 | Camp items are camp-owner decisions; the Wish input; the Blood Price swap | 7a sets only the channel count; hooks without ids or prices; the Wish input flagged to its owner; Blood Price reshaped onto the allowance (8.1) |
| LK12 | The phial under N+1 grading | The phial's minFloor reads the generated floor N (6.3) |
| LK13 | The stream order needs the DH owner | Approved order unchanged; the child stream; the ghost draw (6.10) |
| LK14 | "Trophy" and "Tally" taken | Memento, Treasure count. The critic's "keepsake" also collides (RS Keepsake) and is not used |
| LK15 | "Antechamber offer", "Vault", "Dreamer's Heart" | The first two not used; **told plainly:** the Dreamer's Heart is the F5 theme (RS; EP 7.10), so the tone line stands. Also renamed: "Threshold Cache" → Viaticum (RS 8.0 avoids "Threshold") |
| LK16 | LE ids collide | One register LE-1..24 (A.6) |
| LK17 | Client flags on illustrative numbers | Sell 250‰ is a lead's tuning call; client questions limited to Q1–Q6 |
| LK18 | Unassigned hooks | 6.14 and 12 assign HZ 13 (7c), HZ 16 (7a, 6.9), EP 13 (7c with 7a values), ROOM 6.3.4 (the Viaticum), FL 6.9 (7c, every container non-empty, F5 included, with the overflow and filler rules of 6.4), TL 11.1 fields (7c), AR 5.1 / 5.7 (recomputed in M2; cross-document comparison 2026-11-23, CC-50) |
| LK19 | Bait cache honesty | A real +0 entry in LE-20; party-size dependence reported |
| LK20 | Waves: Keybearer, Twin Coffer, Perilous Door | The purse follows a fixed chain from the bound Keybearer (8.3); the Twin Coffer M3 with TL-27 flagged; the Perilous Door M3 with RS |

### A.4 Judges' grafts taken

- From B: the blocked-step fallback ("one step's worth, never nothing"),
  reshaped as the fit premium and fallback coin (6.9, 6.4); hero-aware
  targeting, reshaped as the Due's parity rule (6.6); category weights
  (6.2).
- From C: the counted boss card and the general offer-set rule (6.3);
  "the elite shows its item in use", presentation only (6.9).
- Rejected from both as listed in 5.3.

### A.5 Names checked (case-insensitive whole-word grep across the 23 approved documents)

| Name | Hits | Use |
|---|---|---|
| spoils list, `SpoilsEntry` | 0 | Floor loot plan |
| rarity deck | 0 | Tier multiset |
| alternate variant, fit variant, spiky | 0 | Deck variants |
| Dream's Due ("the dream owes": 0) | 0 | The guaranteed upgrade |
| Viaticum | 0 | F5 pre-arena offer (replaces "Threshold Cache"; "Threshold" is on RS 8.0's avoided list) |
| Memento, memento wall | 0 | Final signature on the run record (replaces "trophy": Trophy Tally, TK) |
| Grave Goods | 0 | Run-record hoard line |
| Treasure count | 0 | Stratum count (replaces "Tally": a TK property) |
| Heavy Purse | 0 | Carried coin (replaces "Marked Purse": Marked → Spotted, CON) |
| Twin Coffer | 0 | M3 container kind |
| Risk Ledger, `LootLedger` | 0 | Section name; record |
| step allowance, step socket, fit premium, tier mark (replaces "tier pip": pips already name the Wager meter, Cord, vent, Keybearer and boss pips), Wax-Key value, Wax-Key identity | 0 | Rule terms |
| dreamed option (replaces "shadow": the Shade's `shadow` offer lane, SH) | 0 | An unchosen pre-rolled option |
| `dreamsDue`, `DreamsDueWaived` ("Due" alone collides with HZ's "due square" and "due overlays"; prose shorthand only) | 0 | The Dream's Due in keys and UI |
| `loot.requiresBias` (replaces `loot.partyWeight`: ENM and CMP's encounter-budget `partyWeight`) | 0 | Smart-loot bias key |
| LootPlan, LootBind, `consequenceExposed`, `gearCurve`, `carriedPerFloor`, "another way", "The door's gift" | 0 | Generation steps, fields, keys and UI tags (after the check) |
| SR0–SR5 | 0 | Socket ranks (new lead's call; "R3", "R4": 8 hits in RS, rejected) |
| ghost draw, fallback coin | 0 | Stream and coin terms (new lead's calls) |
| `lootPlan` | 0 | Child stream |
| `SubstitutedVP`, `F5Unspent`, `LootEconomy`, kPC, K2b | 0 | Report, validator and peg names |
| moth coin | 0 | Retired after the check (the F5 coin line is hidden; the flavour is on the Stratum Card) |
| Wish, Tithe Bowl, Ferryman, Blood Price | 0 | Camp hooks (the owner names them) |
| keepsake | 22 | **Not used** (RS Keepsake dressing) |
| Vault | in use (SH Vault) | **Not used** |
| LX, LF, LB, LK prefixes | 0 | Local ids (`LT` had 3 hits, rejected) |
| LE-n, V13 | 0 | Change prefix; validator id |

"Brask" in the Due's example line is an existing illustrative companion
name (the CMP death-screen example "Fell with: Brask …"), used here only
as an example.

### A.6 LE cross-reference (source drafts → this register)

The videogame, tabletop and reward-psychology tracks (each LE-1..18), the
three model drafts (A LE-1..16, B LE-1..20, C LE-1..19) and the
mechanics brief (LE-20..32) are renumbered into LE-1..24 by subject:
ledgers → LE-1; decks and variants → LE-2; boss channel → LE-3; offer
sets → LE-4; F5 → LE-5; camp channel → LE-6; coin → LE-7; salvage →
LE-8; Sell → LE-9; Risk Ledger and steps → LE-10; Wax-Key identities →
LE-11 (new, from LB1); K2 → LE-12; upgrade entry / G-U / lane notable /
Due → LE-13; determinism → LE-14; tally and ledger lines → LE-15; purse →
LE-16; category, keys, supply → LE-17; elite intel → LE-18 (new, from
LB2); meta → LE-19; bait and boxed lurker → LE-20; Twin Coffer → LE-21;
curses → LE-22; V13 → LE-23; reports and lever order → LE-24. The Wish,
the Tithe Bowl, the Ferryman's Note and Blood Price keep no ids (hooks).

---

## Appendix B: sources

- **Research tracks:** videogame loot economies, tabletop treasure
  systems, reward psychology (section 4; *(recall)* items unverified;
  Brogue's item metering source, Slay the Spire's rare offset and PF2e
  Table 10-9 confirmed).
- **Toy models** (the design lead's scratchpad; not project files):
  `le7a_sim.py` (models Z, A, B, C, two worlds, 6,000 runs per arm),
  `le7a_calib.py` (calibration on Z to DH 18.2), `le7a_report.py`,
  `le7a_r2.py` (fix arms, Hmid, Cfix4), `le7a_scum.py` (save-scum and the
  camp-reload leak), `curves.py`, `gu.py`.
- **Spec sections verified while writing:** TL 6.11–6.17; ENM 10.5,
  15.1–15.7, 16, 17.5; ITEMS 2, 5.1, 9, 10.1–10.9, 12.5–12.7, 13, 14,
  18.4, 19.3, 22–24; DH 2, 5.4–5.8, 12.4–12.5, 18.2, 21.5; CN 9.3, 10,
  13.4, 14.7, 18.1, 19.3, 21; CMP 7.7, 7.8, 9.3; RS 6.4, 6.5, 6.8, 6.9,
  6.11, 9.1, 11.2; HZ 6.10; FL 6.8–6.11, 9.2; EP 6.8.1, 6.13, and the EP
  house style.
