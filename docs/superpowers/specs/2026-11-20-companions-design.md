# Companions: Design

Date: 2026-11-20
Status: Approved, 2026-11-20
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design),
`2026-11-17-consumables-design.md` (consumables design),
`2026-11-17-death-healing-design.md` (death and healing design, "DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "classes"), and
the approved stage-2 class designs `2026-11-18-vanguard-class-design.md`,
`2026-11-18-shade-class-design.md`, `2026-11-20-arcanist-class-design.md`
and `2026-11-20-herald-class-design.md` ("Vanguard design", "Shade
design", "Arcanist design" and "Herald design" below; the Herald design
is the most recent template)

Items marked **(agreed)** were decided by the client on 2026-11-20 for
the three questions this design raised (Q1–Q3, sections 4, 5 and 18), by
choosing the lead's recommendation in each case. Before answering Q1 the
client was told plainly, and accepted with it: that **there is no Lone
Wolf bonus**; that **recruiting stays the normal, better path**; that **a
full decline is a real challenge at roughly half the reference win rate,
never parity**; that **the Lone Road and the milestones (M3) unlock
content, never stats**; and that **the numbers are a direction until the
M2 simulator sets them**. The client also said that everything else in
the lead's proposal stands as written. The client approved this
document as a whole on 2026-11-20.

Items marked **(lead's call)** were filled in by the design lead inside
approved rules; the client confirmed them all on 2026-11-20, when it
approved this document as a whole, and they are binding. Items marked **(settled)** restate a rule from an approved
document; **(decided)** marks a main-design client decision. Items marked
**(change CMP-n)** amend an approved document and are listed, with the
exact section they touch, in section 16; rows that touch **decided** or
**agreed** text are flagged there, and the client's approval of this
document on 2026-11-20 (with Q1–Q3 where a row names one) covers them.
Items marked **(told plainly)** narrow or re-read agreed or settled text
and were told to the client in plain words, with Q1 above or with the
approval request (18.1); the client confirmed them all on 2026-11-20.
Every item here is binding on the builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q3 of this document (2026-11-20), or one recorded in an approved document, restated |
| **(lead's call)** | Filled in by the design lead inside approved rules; confirmed by the client with this document on 2026-11-20 |
| **(change CMP-n)** | Amends an approved document; listed with its exact section in 16 |
| **(told plainly)** | Narrows or re-reads agreed or settled text; told to the client in plain words (18.1) and confirmed by the client on 2026-11-20 |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the simulator (15) |

This is **the companions design**: how often companions are offered,
what is guaranteed and what is not, how a player declines, dismisses or
swaps them, what declining costs and how it is compensated, how large the
party may grow and how that size is held to the clock, what a companion
is (chassis, progression, the eight M2 kits, equipment, Downed and
Scarred, control and AI), the numbers, the staging, and how every binding
requirement on "the companions design" in the approved documents is met.
Companions are **M2**; profile and unlocks are M3 **(settled, main design
9)**.

The client's brief: "Great work. Let's go deal with the companions, how
frequently should they show up and should they be guaranteed at certain
points in the run? Should the player be able to decline them if they want
to? Again, do the research, and check against the current systems." The
lead's answers, which the client accepted (Q1–Q3, **agreed**), are in
section 5: **one recruit offer at each of camps 1–3, guaranteed as an
offer and in its coverage, never as a hire; declining is always legal and
is made a real challenge by a small shortfall discount, never parity; the
cap of three companions is decided by a measured floor-time gate.**

**Change prefix.** This design uses **CMP-n**. "CO-n" is already taken by
the consumables design (CO-1 to CO-19); the research and model drafts used
five clashing schemes (CO, CP, CP-B, MC, CMP), and every draft proposal
was renumbered once into the single table in section 16. A grep for
`CMP-` across the 16 approved documents finds no hits. CMP-20 is
withdrawn and kept as a gap so the numbers stay stable.

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every win
rate, minute and share comes from a crude toy run model (scratchpad
`comp_toy.py`, `gen.py`, `sweep*.py`, `sens.py`), **not the simulator**.
The toy's per-floor damage factors were tuned so that the reference
schedule (party 1 / 2 / 3 / 4 / 4) meets DH 18.2; every other arm is an
extrapolation outside the calibrated range. **Read the numbers as
direction and size, never as values to put in data (told plainly,
accepted with Q1).**

**Names are placeholders** pending the naming pass (a separate client
item). They are frozen here so that documents agree. Every name new to
this document was grep-checked as a whole word against the 16 approved
documents and has no hits: Muster, Travel on, Party Forecast, Pledge,
Rethink, Lone Road, Old Friend, Wayfarer's Pack, Levy soldier, `kitBound`,
Gatekeeper, Brawler, Scout, Outrider, Tinker, Hedgemage, Jostle, Close
Ranks, Barge, Heave, Gap Sight, Sidestep, Staggering Shot, Flank Sense,
Underfoot, Knife Work, Poultice, Deeds, Veteran (as a record name),
Parting Word.

Names avoided because they collide **(lead's call)**:

| Avoided name (draft) | Collides with | Used instead |
|---|---|---|
| Warden (kit) | Warden's Coif (trinkets, armour); Corridor Warden (weapons) | **Gatekeeper** |
| Breaker (kit) | Rejected in classes 10 (the conditions' "breaker types") | **Brawler** |
| Adept (kit) | A planned unlock class (classes 9.1, "Adept (monk-type)") | **Tinker** |
| Skirmisher (kit) | The enemy role "skirmisher" (enemies 17.1) | **Outrider** |
| Lockbreaker | Head word "Breaker" | **Scout** |
| Hedge-warder | Head word "Ward" (Warded, Warding) | **Hedgemage** |
| Bulwark (kit) | Bulwark Stance, Bulwark Locket | Not used |
| Press, Hew (kit riders) | Vanguard hero Techniques | **Jostle**, **Barge** |
| Fade, Duck (kit abilities) | Shade hero Fade and Duck | **Sidestep**; Duck removed |
| Pinning Shot | Weapons' Pinning property | **Staggering Shot** |
| Spotter's Opening | Enemy affix "Spotter" | **Gap Sight** |
| Sweep | The rejected Vanguard "Universal sweep" | **Heave** (single target) |
| Mend | Herald's Mending Word | **Poultice** (Later reserve only, 9.5) |
| Salve | A rejected Herald mechanic | Not used |
| Drill | Vanguard Stance Drill | **Rethink** |
| Lone Banner, Carried Banner | "Banner" is rejected (classes 10; Herald front matter) | **Lone Road**, "carried hire" |
| Recruit soldier (enemy) | Player "recruits" | **Levy soldier** (CMP-27) |
| Recruit Board | Kept short | **Muster** |

The kit names the Herald design fixed are kept: Drummer, Shieldbearer,
Quickstep, Drumroll, Guard Step, Form Up **(settled, Herald design
7.17)**. Shared mechanic names (Interpose, Shieldwall, Hold the Line,
Opening, Mote, Gust, Flare) are used on purpose: each names the same rule
as on the hero, applied to a simplified slice **(settled, classes 7.8)**.
They carry `sharedWithKits` in data, so `SliceNotCopy` checks kits only
against each class's `heroOnlyFeatureIds` (11.2) **(lead's call)**.

**Waves:** M1 is the vertical slice (one class, levels 1–3, solo), M2 the
full run with four classes and companions, M3 profile and unlocks, M4
breadth (species, multiclassing), Later everything after that no
milestone names. **(settled, main design 9)** Nothing in this document is
on the M1 path.

## 1. Intent

The main design fixed that the party is one hero plus "up to 2–3
companions" recruited during the run, each with a fixed class and
lighter progression, all controlled by the player, never killed, and
offered as "a chance to recruit" at the camps between floors **(decided,
main 4.1, 4.2)**. The enemies design priced a companion at 8 against the
hero's 15 and calibrated every floor on an expected party of 1 / 2 / 3 /
4 / 4 **(agreed, enemies 10.1; enemies 6.3)**. The ability model fixed a
companion's kit shape and its suggestions; the classes design fixed that
a kit is a slice of its class's signature verb; the death and healing
design fixed Downed, get-ups, Scarred and the recruit kit potion. None of
them said how often a recruit is offered, what is guaranteed, whether a
player may refuse, or what refusing costs.

The answer in one sentence: **companions come on a fixed, visible camp
schedule, the offer and its coverage are guaranteed but a hire never is,
and a player who declines meets smaller fights and a real challenge, not
a trap and not a reward.** Recruiting stays the normal, better path; the
pace of a four-unit party is measured before balance work freezes, and
the cap falls to two if the clock cannot hold it **(agreed, Q1, Q2)**.

## 2. Goals, non-goals, anti-goals

### Goals

- **"How frequently should they show up"** (the brief): one recruit slot
  opens at each of camps 1, 2 and 3, and every camp with an open slot
  shows an offer (7.1).
- **"Guaranteed at certain points"** (the brief): the **offer** and its
  **coverage** are guaranteed and printed; a hire is never forced (7.3).
- **"Decline them if they want to"** (the brief): any card or the whole
  board can be declined in one click, with the consequence shown; a
  companion can be dismissed or swapped at camp; nothing is missable
  (7.5, 7.6).
- **Declining is a choice, not a trap:** a full decline wins at roughly
  half the reference rate, dropping one companion costs at most about 10
  points, and no smaller party beats the full one (7.7) **(agreed, Q1)**.
- **Research-led design** (the brief): 4.1 surveys how other games handle
  companions and names what is taken and avoided.
- **The clock holds:** floors stay inside the decided 15–20 minutes, and
  the cap is decided by measurement (7.4) **(agreed, Q2)**.
- **Four class shapes learned once:** each kit is a slice of its class's
  signature verb, reads in under 10 seconds and has at most three things
  to read (8.3).

### Non-goals

- **The camp and hub screens in full.** This document fixes the Muster's
  rules, order and contents (7.11); layout belongs to the camp and hub
  design and the builders.
- **The enemies owner's solo composition table, the conditions owner's
  rulings and the consumables owner's DEP-1 reading.** This document
  requests them (17) and states how companions work either way.
- **Final kit stat sheets** (stage-2 data, calibrated by the simulator),
  **final names** (naming pass) and **final numbers** (simulator).
- **Mortal companions, bonds and personalities** (Later).

### Anti-goals

- **A trap:** a decline that is legal but near-certain death (the toy's
  0.3% under the agreed weights) **(agreed, Q1)**.
- **A must-take line:** any in-run power bonus for empty slots (a Lone
  Wolf buff), which the toy shows makes a buffed duo beat the full party
  **(told plainly, accepted with Q1)**.
- **Missable recruits, pity timers and hidden state** (DH 5.4(f)'s
  reasons).
- **A recruit priced twice:** gold, an experience share or the camp's
  shop-or-reward choice on top of party weight.
- **Mid-floor joins:** they break "weight fixed at generation" (enemies
  10.1).
- **A coach for the hero, or an auto-played companion:** suggestions stay
  off by default and one step at a time; the hero never gets them
  **(agreed, ability 11.3; ability 5)**.
- **Four full character sheets** (main design anti-goal "decision
  fatigue"): no ability increases, one 2-option choice at levels 3, 6 and
  9, numeric steps only.
- **A healer by the side door:** no recruit kit heals at M2 **(agreed,
  Q3)**.

## 3. Constraints

### 3.1 What the parents already fix

The 16 approved documents were grepped for "companion", "recruit", party
size, solo, Downed, Lifeline, kit, captive, Recruiter and
`Floor1ClassSpread`, and each hit was read in context. Mentions: DH 110,
classes 73, Herald 41, consumables 32, trinkets 22, ability 21, items 18,
Vanguard 16, conditions 14, enemies 13, stat model 11, weapons 11, main
10, armour 10, Arcanist 10, Shade 5.

| Source | Binding text | What it means here |
|---|---|---|
| Main 4.1 **(decided)** | Camps give "full rest, a shop or reward, and a chance to recruit"; a run is 5 floors of 15–20 minutes **(change RT-1, run structure design 6.1)**; one save slot per run | Recruiting is a camp service apart from the shop-or-reward choice (items 10.4, I-13); the clock (7.4) |
| Main 4.2 **(decided)** | "Up to 2–3 companions"; fixed class; automatic levelling with a choice at a few key levels; the player controls every member; companions are downed, never killed | Cap 3 inside "2–3" (7.4); 8.1–8.2; 8.5 |
| Main 4.7 **(decided)** | Unlocks add companions; one starting boon; no permanent stat boosts | Kit unlocks and Lone Road recognition (M3), never stats (7.7) |
| Main 6.3 **(decided)** | One seed; separate random streams | The `recruit` stream (7.2; CMP-3) |
| Main 9 **(settled)** | Companions M2; profile M3 | Staging (12) |
| Enemies 10.1 **(agreed for party weight)** | Party weight 15 + 8 per companion, **actual** party, fixed when the floor is generated; a companion lost mid-floor does not lower budgets **(lead's call)** | Joins and dismissals at camp only (7.6); the shortfall term (7.7; CMP-5) |
| Enemies 6.3 | Expected size 1 / 2 / 3 / 4 / 4, "a calibration assumption only; budgets use the actual party" | The slot ladder reproduces it (7.1); CMP-6 |
| Enemies 4 (client answer 2), 10.1 | "Party weight grows sublinearly: hero 15 + 8 per companion, using the actual party, fixed at floor generation" **(agreed)** | The shortfall term (7.7; CMP-5) |
| Enemies 6.4, 10.5, 17.4, 17.5 | Guards; the generator and template content tests at party sizes 1–4; solo minion caps; boss HP ≈ 6 × the party's typical damage per round × 0.85 | 7.9; CMP-7, CMP-8 |
| Enemies 16 | Experience not split by party size; level scaling off the hero; camp catch-up raises the hero | Level equals the hero's (8.2; CMP-17) |
| Enemies 14.6, 21, 25 | Captives as recruits are a Later idea for this design; no spare, rob or execute menu | Pledges, Later (7.12; CMP-10) |
| Ability 11.1 **(lead's call)** | 2 at-wills, up to 3 limited abilities, 1 reaction, passives; at least one `rest` and one `camp` pool; exactly one signature `camp` ability | 8.3, `KitShape` |
| Ability 11.2 | Automatic **(settled)**; 2–3 kit upgrades at levels 3, 6, 9; a recruit joins at camp with full pools **(lead's call)** | 8.2; CMP-16 |
| Ability 11.3 **(agreed)** | Suggestions off by default; one key accepts one step; nothing is ever taken without the player; the hero never gets suggestions | 8.6; CMP-19 only on the client's yes |
| Ability 11.4, 5 | Playtest target under 10 s per companion turn, at most 4 things to read; auto-played companion turns rejected | 7.4, 8.3 |
| Ability 14.6 | Out-of-combat choices in the simulator (recruits) are scripted policies | `MusterBestFit` (11.3; CMP-18) |
| Ability 6.9 | Level-ups and companion choices resolve at combat end, together, hero first | 8.2 |
| Classes 6.3 **(agreed)** | Forbidden grants for kits: extra units, teleport, +speed, a second reaction, more loot or gold, `liftsDowned`, a second camp spend, Rally beyond once per ally per fight, a freshly created action for an ally | 8.3 movement ruling; `ForbiddenClassGrant` |
| Classes 6.4 | Healing: one class at most; companions and leadership: "better recruits and prices (a Herald hero)", no extra units | Q3 (5.4); Recruiter's Eye (9.2) |
| Classes 6.6 **(agreed)** | Reliance "without" arms strip a class's kits from recruit pools; a rule only that class meets is waived and printed | 7.2 |
| Classes 7.3 | The subclass lands at level 3, the end of solo floor 1 | Camp 1 follows it (7.1) |
| Classes 7.8 **(lead's call)** | Launch class ids; a kit is a slice of the signature verb, no subclass; caster kits have one combined `camp` slot pool counted as one limited ability; same-class recruits carry a different slice and are weighted down; Herald recruits Wisdom, no Presence spells, no Frighten; recruit-pool rules per floor | 7.2, 7.3, 8.3; CMP-11 |
| Stat model 5.4 **(agreed)** | Best-member checks; Stealth uses the worst member; Presence always the hero's | `NoPresenceOnKit`; the forecast (7.11) |
| Stat model 5.5, 7.1, 7.2 | Companions take HP, save, skills and main ability from their class; Leadership "better recruits"; "Companions come with fixed skills" **(agreed)** | 8.1; CMP-13, CMP-14 |
| DH 7.1 **(agreed)** | HP 11 + L × max(1, tier + Con); `hp.companionBonus` 11, may only be lowered | 8.1 |
| DH 13.1 (order lead's call) | Camp order: get up, restore, end conditions, +1 supply, top-up, vendor opens | The camp order (7.11; CMP-31) |
| DH 11 **(decided / agreed)** | Downed inert; get-up after a win, a short rest or camp; Medicine once per companion per fight; Scarred on the first downing per floor; never killed | 8.5, unchanged |
| DH 5.5 G3, 5.6, 13.2 **(agreed)** | 1 potion in every recruit kit (a client number); the guarantee is class- and composition-blind; the hero top-up moves a companion's dose first | `kitBound` (7.6; CMP-21, CMP-22) |
| DH 17.1 **(agreed)** | No surprise on floor-1 rooms 0–1 "while the hero is the only party unit" | Floor 1 is always solo (7.10) |
| DH 18.1, 18.2 **(agreed)** | Healer gate ≤ 10 points, recruit heal kits counted; no-healer arm at least a quarter | Q3 (5.4); CMP-23 |
| DH 21.3 | HE-6 tables n = 1–4 on floors 2–5 | Every reachable size is fair (7.9) |
| Items 6, 15, 10.4–10.6, 12.3, 12.8, 18.1, 19.2 | Per-unit ownership, party-wide placement; a recruit arrives with its kit record; a Downed companion keeps everything; the shop-or-reward choice; Sell at camp, `unsellable` sells for 0; record-level flags; the stack rule; `ItemInstance` and its exhaustive transition table | 7.6, 8.4; CMP-21 |
| Trinkets 11 rule 9, 18.3 | Trinkets are companions' main build lever; companion slot fill at the boss ≤ 25% empty **(settled)** | 8.4; CMP-30 |
| Armour 11 item 2 (AR-20) | The lowest legal party AC includes companions | `CompanionAcFloor` (8.1) |
| Armour 6.1, 10.4, 11 item 1 | The closed AC query; one reaction AC bonus per roll, step-0 window fills counting as it; the charged envelope (≥ 15%) | Close Ranks as a step-0 fill (8.3); `KitAcEnvelope`; CMP-34 |
| Conditions (Staggered, Prone, Spurred sources) | Closed source lists; Spurred names Herald companion kits (CL-5) | Staggering Shot, Barge (8.3; CMP-32); the movement ruling (8.3) |
| Classes 6.4, 6.5 | No single-type kit; every class names a "slowed, never locked out" fallback (the Arcanist's is physical) | Arcanist kits carry a staff or dagger; `KitHasFallbackDamage` (8.3) |
| Weapons 11.3 | The D-c fallback, switched only if the M2 companion-turn median exceeds 10 s and off-hand turns are measurably slower | Lever 4 keeps that trigger (7.4); CMP-28 |
| Consumables 13.8 DEP-1 (binding) | At least one recruitable non-caster per floor's recruit pool is trained in Arcana | Gap card (7.3); CMP-12 |
| Herald design 7.17, 10.1 | Drummer and Shieldbearer as specified; `NoHealOnHeraldKit`; Recruiter's Eye (HX-10) handed over as optional | 8.3, 9.2 |
| Vanguard design 7.5, 18 | A Vanguard kit's stance is fixed by its kit; no Momentum or Arts | 8.3 |
| Shade design 6.5, 13.3, 18 | The Opening trigger registry (Unready, Beset, tiers); no Vanish Arts or From Nowhere; stealth and own-tempo reliance with and without a Shade recruit | 8.3, 11.3; CMP-33 |
| Arcanist design 7.2, 19 | No Overchannel or subclass Arts; scorer-chosen beam order for companions; Prime's solo clause | 8.3, 8.6 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored. Party weight and its shortfall term are
integers. `expectedCompanions` is therefore computed, never stored
(7.7).

### 3.3 What the approved documents owe this design

| Owed by | Requirement | Where met |
|---|---|---|
| DH 5.8 item 8, DH 27 | Protector tools on kits; Medicine spread across recruits; 1 potion per recruit kit | 8.3, 7.6 |
| Consumables 13.8 DEP-1 (binding) | An Arcana-trained non-caster per floor's recruit pool | 7.3; CMP-12 |
| Classes 7.8 | Recruit-pool rules per floor (protector on ≥ 2 classes, Arcana non-caster, Medicine, Mechanics, ≤ 1 ally-heal kit never as signature); optional locked-class previews | 7.2, 7.3; 12 (M3) |
| Classes 6.6, Herald design 5.4 | "Without" arms that strip a class's kits and re-roll slot by slot on a dedicated substream | 7.2 |
| Ability 14.6 | The scripted recruit policy for the simulator | 11.3 |
| Ability 20 | Kit counts for M2 | 8.3 (eight) |
| Herald design 7.17, 10.1 | Drummer and Shieldbearer; Recruiter's Eye (HX-10) | 8.3, 9.2 |
| Vanguard design 7.5, 18 | Stance fixed by kit; no Momentum or Arts | 8.3 |
| Shade design 18, 13.3 | No Vanish Arts or From Nowhere; own-tempo reliance with and without a Shade recruit | 8.3, 11.3 |
| Arcanist design 19, 7.2 | No Overchannel or subclass Arts; Prime's solo clause; scorer-chosen beam order | 8.3, 8.6 |
| Armour design 13.2, 22 | Companions and ability increases; kit armour against AR-20 | 8.1 |
| Weapons design 26, 11.3 | Weapon training per kit; the D-c fallback tied to the companion-turn median | 8.3, 7.4 |
| Trinkets design 23 | Medicine and Lore spread across kits | 8.3 |
| Items 10.8; consumables 9–10, 13.7 | The companion "drink it" suggestion; placement preferences; the boss reserve | 8.4 |
| Enemies 25, 18.1 | Captives as recruits (Later); the rival party needs kits proven in play | 7.12; 12 |

### 3.4 Tensions found and their resolutions

Gaps and contradictions found while checking against the current
systems; each shaped a rule here or a change in section 16. **(lead's
call)** Ids T1–T15 are local to this table.

| # | Tension | Resolution |
|---|---|---|
| T1 | Main 4.2 "2–3" against party-of-4 calibration everywhere (enemies 6.3; enemies 10.1 "four 39"; HE-6 n = 4; items 5.2's "party of four"; Drumroll's L6 "4 targets"; Beastbound's "3 slots", classes 9.1) | Cap 3 inside "2–3", decided by the floor-time gate, with cap 2 as the written fallback (7.4) **(agreed, Q2)** |
| T2 | Enemies 10.1 says "recruiting a fourth companion is a net gain"; the weights stop at four **members** | Wording fix to "a fourth party member" (CMP-4) |
| T3 | Main 4.1 "a chance to recruit" reads as a random chance | A Muster at every camp with an open slot (CMP-1) |
| T4 | No legal host for a "heal-kit recruit" (`NoHealOnHeraldKit`; no other class has an ally heal), yet DH 18.1, classes 6.6 and 7.8 and Herald design 5.4 count one | None at M2; the rows print 0 (5.4; CMP-23) **(agreed, Q3)** |
| T5 | Classes 7.8's four pool requirements cannot all fit three cards, and "per floor pool" is not tied to what the player sees | Read per Muster board together with the current party, with printed waivers (7.3; CMP-11) |
| T6 | Enemies 17.5's boss HP does not say actual or expected party | The actual party at generation, mandatory (CMP-7) |
| T7 | Declining is assumed legal (consumables 9.3 "some runs decline recruits"; HE-6 n = 1) but the toy shows it is a trap under the agreed weights | The shortfall discount (7.7; CMP-5, CMP-6) **(agreed, Q1)** |
| T8 | Enemies 20.3 reads "flat across sizes validates the party weights"; HP-equivalents cannot read flat under 15 / +8 and the 6-unit cap, and 10.1 itself calls recruiting a net gain | Read against an expected curve, plus a hero-death-per-fight row by party size (CMP-9) |
| T9 | Vanguard design 7.14's level-2 default "Pivot in M2 when two or more companions are recruited at the level-up" can never fire: level 2 always falls on solo floor 1 | Delete the clause (CMP-26) |
| T10 | Pacing: no approved document sizes floor time as the party grows | The M2 floor-time gate, the lever order and the cap fallback (7.4) **(agreed, Q2)** |
| T11 | The recruit kit potion and kit gear could be farmed by recruit-dismiss churn | `kitBound` items (7.6; CMP-21) |
| T12 | Camp-3 recruits arrive with empty trinket slots against trinkets 18.3 | Report by join camp; the `anyCompanion` loot-weighting lever (CMP-30) |
| T13 | Classes 6.6 strips a class's kits; Herald design 5.4 re-rolls slot by slot | One mechanism: the base board is resolved, then only X's cards are replaced, slot by slot, on a dedicated `armReroll` child (7.2) |
| T14 | Armour 13.2 and 22 leave "companions without ability increases" open | No increases; a fixed array per kit (8.1; CMP-16) |
| T15 | Armour 12.3's enemy "Recruit soldier" collides with the player's "recruit" | Levy soldier (CMP-27) |

**Citation corrections found while checking (lead's call).** The research
drafts cited some sections loosely; this document cites them as they
stand: the Vanguard level-2 default is in Vanguard design **7.14** (its
companions dependency in **18**); the Shade's companions dependency is in
Shade design **18**; the armour companions dependency is armour design
**22**; the trinket slot-fill report is trinkets design **18.3** and its
companions dependency **23**; the boss HP bullet is enemies **17.5**.

### 3.5 Terms

**(lead's call)** Used with these meanings everywhere below, beside the
terms of the classes design (3.4) and the stage-2 class designs, which
stand **(settled)**.

- **Muster:** the recruit board shown at a camp (7.2).
- **Slot ladder:** after camp *k* the party may hold at most
  `min(k, cap)` companions (7.1).
- **Hire:** a companion joining the party at a Muster; a **swap** is a
  dismissal and a hire in one visit.
- **Carried hire:** one extra hire at the next camp, earned by hiring
  nobody at a camp where a slot was open (7.5).
- **Gap card (card A):** the board card resolved at camp entry by the gap
  procedure (7.3); it covers the first unmet coverage need not already
  offered by a free card, or is a plain draw when none is open. **Free
  cards:** every other card, fixed at floor generation (7.2).
- **Coverage needs:** Medicine, protector, Mechanics, Arcana for scrolls,
  in that order (7.3).
- **Expected companions:** `expectedCompanions(floor)`, computed from
  `recruitSchedule.slotOpensAtCamp` and `companionCap` (the agreed
  notation `expectedCompanions[floor]` names this function): 0 / 1 / 2 /
  3 / 3 at cap 3, 0 / 1 / 2 / 2 / 2 at cap 2 (7.7).
- **Shortfall:** expected companions − actual companions on a floor,
  floored at 0 (7.7).
- **Reference schedule:** a hire at each of camps 1, 2 and 3 (party 1 /
  2 / 3 / 4 / 4).
- **Travel on:** the one-click action that leaves camp, declining any
  unhired card (7.5).
- **`kitBound`:** an item instance issued by a recruit kit, recorded by
  its instance field `boundToUnitId` (7.6).
- **Join card:** the confirmation card on hiring, where the flex skill
  and any backlog choices are set (8.1, 8.2).

## 4. Framework decisions

The client chose Q1–Q3 on 2026-11-20, each time the lead's
recommendation **(agreed)**. The rest are the lead's calls, which the
client said stand as written; the client confirmed them with this
document on 2026-11-20, and they are binding.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Declining: should a smaller party be a fair challenge? (Q1) | **The shortfall discount.** On floors 2–5, `partyWeight = 15 + 8c − shortfall[expected − actual]`, expected = `recruitSchedule.expectedCompanions[floor]` (computed, 7.7), `partyWeight.shortfall` [0, 2, 3, 4] *(illustr.)*; tuning keys read against `SoloRunReport`, shipped only with the solo composition rule and `SoloBudgetMonotone`; floor 1 and the reference party unchanged; boss HP reads the actual party (7.7; CMP-5, CMP-6, CMP-7) **(agreed; the told-plainly items in the front matter accepted with it)** | B: keep the agreed weights (a full decline wins about 0.3%: a trap). C: keep the weights and add a sustained-damage cap for under-sized parties (untested) |
| 2 | How many companions, given the clock? (Q2) | **Cap 3 companions (a party of 4), decided by measurement:** `FloorTimeByPartySize` is the first M2 gate (median floor time at party 4 on floors 4–5 ≤ 20 minutes) plus a blind party-of-3 vs party-of-4 playtest before balance freezes; the lever order of 7.4; if floors 4–5 still run long after the levers, the cap becomes 2 (camp 3 swap-only, read exactly in 7.1; expected 0 / 1 / 2 / 2 / 2); every new report runs at both caps from the start; the accept-rest-of-turn key is prototyped behind a playtest flag and **the client decides whether it ships** after seeing it (7.4; CMP-2, CMP-19, CMP-29) **(agreed)** | B: cap 2 now (re-reads every calibration column; narrower parties). C: cap 3 with no gate (the toy puts floors 4–5 at about 27–29 minutes) |
| 3 | Should any companion heal allies at M2? (Q3) | **No.** No recruit kit heals allies at M2; the heal-kit recruit rows print 0; healing stays the Herald's lean (classes 6.4, "one class at most"); the Hedgemage's Poultice is kept written as a Later reserve (5.4, 9.5; CMP-23) **(agreed)** | B: ship Poultice on the Hedgemage at M2 as a new class-healing source measured by the healer gate |
| 4 | Recruitment model | **Model A, the camp schedule**, with grafts (6) **(lead's call; judges unanimous)** | B, found in the dungeon (dual composition at W and W + 8). C, the choice economy (charges twice for one unit) |
| 5 | Cadence | One slot at each of camps 1–3; a Muster of 3 cards *(illustr.)*; at most one hire per camp plus one non-stacking carried hire; camps only; floor 1 always solo (7.1) **(lead's call)** | Mid-floor joins; a random chance per camp; unlimited carry |
| 6 | Guarantees | **The offer and its coverage, never a hire:** a Muster at every camp with an open slot; a gap card for the first unmet need (Medicine, protector, Mechanics, Arcana), never healing, waivers printed; no pity timer (7.3) **(lead's call)** | A guaranteed hire or party size by floor X (budgets already read the actual party); a pity timer (DH 5.4(f)) |
| 7 | Declining, dismissal and swap | Travel on in one click; dismissal and swap at camp only, one per camp (a plain dismissal or a swap); `kitBound` kit items (7.5, 7.6) **(lead's call)** | Mid-floor dismissal (pure loss, griefable); a reserve bench |
| 8 | Cost of a recruit | **Free;** the price is party weight, shown on the card (7.8) **(lead's call)** | Gold; the camp's shop or reward; an experience share |
| 9 | Level | **Always the hero's;** missed 3/6/9 choices defaulted on the join card; no ability increases (8.2; CMP-16, CMP-17) **(lead's call)** | "A level behind" (one party-level column in enemies 6.3; a lagging late recruit is a trap). Main-ability increases at 4 and 8 (+16 points of win rate in the toy; forces an enemy retune) |
| 10 | Skills | **Fixed skills plus one flex skill chosen on the join card** from two class-list options, fixed from then on (8.1; CMP-13) **(lead's call; told plainly)** | Fixed tags alone (the toy's kit convergence) |
| 11 | Kits | **Eight at M2,** two per launch class, including the Herald's Drummer and Shieldbearer exactly as Herald design 7.17 (8.3) **(lead's call)** | One kit per class (too little variety); three per class (M4) |
| 12 | Control | **The player controls every unit (decided);** suggestions off by default, one step per key **(agreed, ability 11.3)**; riders declared before the roll; a ready glint; the accept-rest-of-turn key prototyped only (8.6) **(agreed for the key's status, Q2)** | An auto mode (ability 5) |
| 13 | Determinism | A `recruit` RNG stream, per-camp and per-slot substreams, free-card candidate lists pre-drawn and resolved at floor generation, the gap card resolved at camp entry by a fixed procedure on its own substream (7.2, 7.3; CMP-3) **(lead's call)** | Boards rolled on entry (save-scummable); the whole board fixed at generation (stale against mid-floor skill gains) |
| 14 | Leadership | **Recruiter's Eye (HX-10):** +1 free card with Leadership trained; one re-roll per camp with mastery; never more units (9.2; CMP-14, CMP-15) **(lead's call)** | A count increase (forbidden, classes 6.3); loyalty rolls |

### 4.1 How other games handle companions

Mechanism references only. No names or text are taken. Checked during
research: the OSE SRD (retainers), UA Sidekicks 2018 and the D&D Beyond
article on Tasha's sidekicks, the Gloomhaven rules site (solo play and
scenario level), the DOS2 Lone Wolf wiki, PC Gamer and Siliconera on
Darkest Dungeon 2, the Wildfrost wiki, and the Mystery Dungeon wiki
(Shiren allies). The rest is recall, flagged *(recall)* here and in
Appendix B.

| Game / feature | What it does | We take | We avoid |
|---|---|---|---|
| **OSR retainers (B/X, OSE)** | Hired in town; wages plus a treasure and XP share; Charisma caps the count and keys a loyalty roll | Leadership improves the **offer**, never the count | Wages and XP shares (party weight already prices the unit, so they would charge twice); loyalty dice on a unit the player fully controls (the enemies framework rejects morale rolls for the same reason) |
| **5E sidekicks (UA 2018, Tasha's)** | Level with the party; a reduced feature track with few choices | "Lighter" = fewer choices at the **same** level | A level gap |
| **PF1 / 3.5 Leadership** | A near-full cohort for one feat; unpriced in encounter maths; widely banned | An extra body must be priced exactly once | Any class or feat granting units (forbidden, classes 6.3) |
| **PF2e** *(recall)* | Per-PC encounter budget adjustment; animal companions need an action to command | Body count priced in the encounter budget | An action tax on commanding |
| **Gloomhaven** | Monster level by party level; solo (one mind, several characters) raises monster level | One mind with perfect information coordinates better, which justifies pricing a companion below a hero (8 against 15) | — |
| **Darkest Dungeon 1 and 2** | A stagecoach slate of free recruits, a roster cap; DD2 inns replace the fallen | A free slate refreshed on a schedule; opportunity cost, not gold | A fixed party of 4, which leaves no solo path |
| **FTL** *(recall)* | Crew from stores and events; a cap; dismiss at will | Refusable offers; a cap that forces a swap choice | Crew droughts that decide runs (we guarantee the offer) |
| **XCOM** | Squad size grows with upgrades | One new unit per floor: complexity grows at a learnable pace | — |
| **Fire Emblem** | Recruits at fixed chapters, many missable | — | Missable recruits; nothing here is missable |
| **Into the Breach, ToME** | Rescues mid-mission pay out at the hub; escorts give a reward instead of joining | **Pledges**: found in the dungeon, decided at camp (Later) | Mid-floor joins |
| **Wildfrost** | Companions from map nodes; a forced accept you can bench | Recruitment visible ahead (the next-camp preview) | A bench (roster management in a 1–2 hour run) |
| **Slay the Spire, Monster Train** | "Skip" is always offered; skipping is legitimate because offers keep coming | Declining is one click; offers keep coming | — |
| **DOS2 Lone Wolf** | Big HP, AP and attribute bonuses at party ≤ 2; widely seen as the strongest setup | The lesson, confirmed in our toy: a buffed duo beats the full party | **Any in-run power bonus for empty slots** |
| **Baldur's Gate 3** | Party of 4; dismiss at camp; solo a popular self-imposed challenge with no buff | Dismiss at camp; solo as recognition | — |
| **Battle Brothers** | Hire fee plus daily wages and food | — | Ongoing wages (tedious; a death spiral) |
| **Pathfinder: Wrath** *(recall)* | Custom mercenaries for scaling gold; a turn-based AI toggle | Suggest-and-accept to cut turn time | Paid custom mercenaries that undercut companions |
| **DCSS** *(recall)* | God allies, bought mercenaries, the single scaling ancestor | A few distinct, predictable companions beat many generic ones | AI noise |
| **Pokemon Mystery Dungeon** *(recall)* | Defeated wild monsters may ask to join | — | "Defeated foes join": our monsters have no class (stat model 5.5) |
| **Shiren** | Rare scripted allies on specific floors | Companions are optional extras | Allies that die stupidly (ours are never killed) |

**Combined lesson.** Availability is set by venue and cadence. Cost is
whatever prices the extra action economy, and in this game that is party
weight. Loyalty is replaced by permanence plus a dismiss option.
Progression is the same level with fewer choices. The solo–party balance
comes from encounter scaling by body count, priced below linear because
one mind controls all units. **Any reward for a smaller party must be
scaling or recognition, never stats.**

## 5. The client's brief: how often, guaranteed, declinable

### 5.1 The questions and the answers

**How often should companions show up?** **(lead's call; CMP-1, CMP-2)**
- **One recruit slot opens at each of camps 1, 2 and 3.** Every camp with
  an open slot shows a **Muster** of 3 candidate cards *(illustr.)*; at a
  full party the Muster becomes a 3-card swap board.
- **At most one hire per camp,** plus one **carried hire** if the
  previous camp was declined in full. The carried hire never stacks.
- **Cap: 3 companions, a party of 4** **(agreed, Q2)**, inside main 4.2's
  decided "2–3". An always-hiring player has a party of 1 / 2 / 3 / 4 / 4
  on floors 1–5, exactly the expected-size column the enemies design was
  calibrated on (enemies 6.3).
- **Camps only.** Nothing joins mid-floor, and **floor 1 is always solo**
  for every class. Captives, wanderers and shrines become **Pledges**:
  named extra cards at the next camp. They stay **Later**, as approved;
  the run-state slot is reserved at M2.

**Should they be guaranteed at certain points?** **(lead's call;
CMP-11, CMP-12)**
- **Guarantee the offer and its coverage, never a hire.** Every camp 1–3
  shows a Muster, deterministically. Main 4.1's "a chance to recruit" no
  longer means a random chance.
- **Coverage:** one card on each board (the **gap card**) covers the
  first need the party lacks, in the order Medicine, protector,
  Mechanics, Arcana for scrolls, that no other card on the board already
  offers. Needs the party already meets are waived, and the waiver is
  printed. The gap card **never** targets healing.
- **No pity timer** (DH 5.4(f): hidden state, exploitable, seed-unstable)
  and **no "party of at least N by floor X"**: budgets already read the
  actual party, so fairness does not need one.
- These guarantees leave the healer gate (DH 18.1), the class-blind
  healing guarantee (DH 5.6), `Floor1ClassSpread` and the solo floor-1
  rules untouched.

**Should the player be able to decline them?** **(agreed, Q1)**
- **Yes, always.** Any card or the whole board can be declined in one
  click ("Travel on"), and the confirmation shows the consequence.
  Companions can be dismissed or swapped **at camp only**. Nothing is
  missable.
- **Under the agreed weights declining was a trap,** not a choice: in the
  toy a run that declines every recruit wins about 0.3% against about 27%
  for the reference party, and dropping only the last companion costs
  about 13 points, above the client's own 10-point convention. Potions,
  extra rests, a coin stipend and the solo round-1 guard each move this by
  0–2 points; a Lone Wolf buff fixes solo play but overshoots (a buffed
  duo beats the full party).
- **The fix is the shortfall discount** (5.2, 7.7): smaller fights on
  floors 2–5 for a party below the schedule's size. The reference party
  and floor 1 are untouched; boss HP reads the actual party.

### 5.2 Declining: a real challenge, never a trap, never parity

**(agreed, Q1; the told-plainly items accepted with it)**

- **What the client chose:** option A, the shortfall discount, read
  against `SoloRunReport` and shipped only with the solo composition rule
  and `SoloBudgetMonotone` (7.7).
- **Told plainly and accepted:** there is **no Lone Wolf bonus**;
  recruiting stays the **normal, better path**; a full decline is a **real
  challenge at roughly half the reference win rate, never parity**; the
  **Lone Road and the milestones (M3) unlock content, never stats**; the
  **numbers are a direction** until the M2 simulator sets them.
- **Why not parity:** parity would need either solo buffs, which become
  the must-take line, or weaker companions, which make recruiting
  worthless.
- **Rejected:** B, keep the agreed weights (declining legal but a
  near-certain loss; Lone Road a badge only); C, keep the weights and cap
  sustained damage for under-sized parties (untested). C's
  sustained-damage row survives only as a diagnostic arm (11.3).

### 5.3 The party cap and the pacing gate

**(agreed, Q2)**

- **What the client chose:** option A. Cap 3, **decided by measurement**:
  `FloorTimeByPartySize` is the first M2 gate and a blind party-of-3 vs
  party-of-4 playtest runs before balance freezes; the lever order in
  7.4; if floors 4–5 still run long after the levers, the cap becomes 2.
  Every new report runs at both caps from the start. The
  accept-rest-of-turn key is prototyped behind a playtest flag, and the
  **client decides whether it ships** after seeing it.
- **Why a gate:** the toy puts a party of 4 at about 27–29 minutes per
  floor on floors 4–5 against the decided 15–20, and about 23 even with
  every lever *(illustr.; toy)*. Cap 3 matches every calibration column;
  cap 2 is safer on the clock. Measurement, not the toy, decides.
- **Rejected:** B, cap 2 now; C, cap 3 with no gate.

### 5.4 No ally heal at M2

**(agreed, Q3)**

- No recruit kit heals allies at M2 (`NoAllyHealKitAtM2`). The
  "heal-kit recruit" rows in DH 18.1, classes 6.6 and Herald design 5.4
  print 0, and the "healer count 2+" row prints N/A while no heal kit
  ships (CMP-23).
- Healing stays the Herald's lean (classes 6.4: "one class at most").
  While no heal kit ships, the DH 18.1 healer arm is exactly a Herald hero
  holding a heal card; the gate's text is unchanged **(told plainly)**.
- The Hedgemage's **Poultice** (half Recovery, a rest sub-limit of 1,
  never the signature) is kept written as a **Later reserve** (9.5). It
  returns only through the client.
- The Medic role badge says "gets allies up; no healing spells" (8.7).
- **Rejected:** B, Poultice on the Hedgemage at M2.

## 6. Recruitment models

### 6.1 The three models

- **Model A, the camp schedule.** One slot opens at each of camps 1–3;
  every camp with an open slot shows a board; free; camps only; floor 1
  solo; in-dungeon finds become next-camp Pledges later.
- **Model B, found in the dungeon.** Boss cells after every boss plus one
  side captive per floor on floors 2–4 who can join on the spot.
  Mid-floor joins are priced by composing every encounter twice (at W and
  W + 8) and switching unsighted encounters on a join.
- **Model C, the choice economy.** Recruiting costs the camp choice (a
  third arm beside the notables shop and the reward card) or a coin fee;
  the next camp's board is previewed; one candidate can be held over.

### 6.2 Verdict and judges' scores

Three judges, each scoring 1–10 (higher is better; "build cost low" is
low build cost). Cells give the three judges' scores and their mean.

| Model | Fun and attachment | Decision quality | Balance safety | Fit with docs | Pacing | Build cost low | Sum of means |
|---|---|---|---|---|---|---|---|
| **Camp schedule (A)** | 6/6/6 = 6.0 | 7/6/7 = 6.7 | 8/7/8 = 7.7 | 9/9/9 = 9.0 | 6/6/7 = 6.3 | 8/8/8 = 8.0 | **43.7** |
| Found in the dungeon (B) | 8/8/8 = 8.0 | 7/7/7 = 7.0 | 5/5/5 = 5.0 | 5/5/5 = 5.0 | 5/5/5 = 5.0 | 4/4/3 = 3.7 | 33.7 |
| Choice economy (C) | 5/6/5 = 5.3 | 6/5/6 = 5.7 | 5/5/5 = 5.0 | 3/4/3 = 3.3 | 5/6/6 = 5.7 | 6/6/6 = 6.0 | 31.0 |

**Model A is the backbone (unanimous; lead's call).** It reproduces the
calibration sizes by construction, keeps the weight fixed at generation,
keeps floor 1 solo (the toy's floor-1 deaths are identical in every arm,
1.9% of runs *(illustr.)*), and keeps recruiting a free, separate camp
service, as main 4.1 already says.

**Model B is rejected as a backbone.** Its central move (W and W + 8
dual composition) breaks the cleanest agreed rule, doubles generation and
content-test load, gives scouts (above all the Shade) a free gain by
freeing captives after sighting rooms, punishes the Stealth bypass, makes
the optional elite room near-mandatory, and brings units earlier into
floors that already overrun. Its flavour is kept for Pledges (Later):
**Meet at camp** and **Send home**.

**Model C is rejected.** It changes decided and agreed text to charge
twice for a unit the weight already prices, and it taxes the weakest
point of the run (camp 1, coming off solo floor 1). Its cheap ideas are
kept: **the board drawn at floor generation** and **the next-camp
preview**. Its Hold card is not needed, because nothing is missable.

### 6.3 Grafts onto Model A

**(lead's call)**

| Graft | From | Adopted as |
|---|---|---|
| Shortfall weight discount | Toy; all three judges | 7.7; CMP-5, CMP-6; keyed to a config value, not the enemies 6.3 column **(agreed, Q1)** |
| Boss HP reads the actual party | Toy; all judges | Mandatory; CMP-7 |
| One-step carried hire (not unlimited carry) | New mechanics; all judges | 7.5: **+1 hire, no extra card** |
| Party Forecast line | New mechanics | 7.11, framed as sizing, never as a bonus |
| Next-camp preview | Model C | 7.2: the free cards exactly, plus placeholders for card A and the Eye's card |
| Board drawn at floor generation | Model C | 7.2: free cards drawn and fixed at generation; card A resolved at camp entry by the gap procedure (7.3) |
| Pledges, Meet at camp, Send home / Parting Word | Model B | 7.12, **Later** (wave unchanged) |
| Re-pick one kit choice per camp | New mechanics (Drill) | **Rethink**, M2 (8.2) |
| Pre-declared riders; one 3/6/9 choice grammar | Kits | 8.2, 8.3 |
| Recruit policy with a combat-value term; named arms | Toy | 11.3; CMP-18 |
| Spread coverage; more kits at M4 | Toy | The flex skill (8.1); more kits at M4 |
| Potion once per slot (draft) | Model A | **Replaced** by `kitBound` (7.6) |
| Enemies 20.3 flag | Toy | CMP-9 |

## 7. Recruitment rules in full

### 7.1 Cadence and the slot ladder (M2)

**(lead's call; change CMP-1, CMP-2; cap agreed, Q2)**

- **Slot ladder.** After camp *k* the party may hold at most
  `min(k, cap)` companions; cap 3 (`recruitSchedule.companionCap`). The
  cap is a **single switch**: `slotOpensAtCamp` is always the first
  `companionCap` camps, and `expectedCompanions(floor)` is computed from
  them (7.7); `SlotScheduleMatchesCap` checks both.
- **Hires per camp.** At most **one**, plus the **carried hire** (7.5).
- **Floor 1 has no companion source of any kind** (`NoFloor1Companion`).
  No boon, event, Pledge or offer places a unit before camp 1.

| Camp | Reached after | Ladder | Muster shown | Hires this visit |
|---|---|---|---|---|
| — | — (floor 1) | 0 | never | 0 |
| Camp 1 | Floor-1 boss (hero at level 3 since the floor-1 antechamber, subclass chosen there before the boss, classes 7.3; change RT-8, run structure design 9.4) | 1 | always | 1 |
| Camp 2 | Floor-2 boss | 2 | always (open slot, or swap) | 1, or 2 with a carried hire |
| Camp 3 | Floor-3 boss | 3 | always (open slot, or swap) | 1, or 2 with a carried hire |
| Camp 4 | Floor-4 boss | 3 | open slot or swap | 1, or 2 with a carried hire |

- An always-hiring player is at 1 / 2 / 3 / 4 / 4: the enemies 6.3
  column, the Herald design 5.4 toy schedule, HE-6's n = 4 cells and
  items 5.2's "party of four".
- **Stranded slot (told plainly).** The carried hire holds one hire
  only. A player who declines at camps 1 and 2 can hire at most 2 at
  camp 3; with 5 floors the third slot is always reachable through camp 4
  **(change RT-1, run structure design 6.1)**.
- **Under the cap-2 fallback** (7.4) the ladder is `min(k, 2)`,
  `slotOpensAtCamp` becomes [1, 2] and `expectedCompanions` becomes 0 /
  1 / 2 / 2 / 2. **No new slot opens at camp 3.** A party below the cap
  there still gets a hire board (a carried hire honoured, up to 2
  companions); a full party gets a swap board. This is the exact reading
  of the client's "camp 3 swap-only", which describes the always-hiring
  player **(lead's call; told plainly)**. Nothing else in this section
  changes.

### 7.2 Candidates and generation (M2)

**Board size** **(lead's call)**
- **3 cards** *(illustr.; `cardsBase`)*: card A (the gap card, 7.3),
  always resolved at camp entry, plus `cardsBase − 1` free cards fixed at
  floor generation.
- **+1 free card** with Recruiter's Eye (9.2), from a slot that is always
  pre-drawn and shown only when the Eye applies at camp entry. **Max 4
  cards** (`cardsMax`).
- **The board shrinks when kits run out (new lead's call).** A free slot
  with no legal kit (exhaustion, below) is left empty; the Eye's card is
  then skipped. This can happen when the party holds both kits of one
  class, or in a "without X" arm, where only three classes remain.
  `BoardShrinkLegal` (a test) checks that the base board never falls
  below `cardsBase` cards while the corpus has legal kits.
- A carried hire adds a hire, **not** a card.
- At a full party: a swap board of the same construction.

**Free cards** **(lead's call; classes 7.8)**
- Free slots 1 to `cardsMax − 1` (slot 3 is the Eye's) are filled in slot
  order. Equal weight per kit (`offerWeightPermille`); the hero's class
  ×½ *(illustr.; `sameClassAsHeroWeightPermille` 500)*.
- **Free cards never share a class** with each other.
- **No kit already in the party** (`NoDuplicateKitInParty`, which
  compares `kitId` only), and never two identical kits on a board.
- At most one ally-heal kit per board (zero at M2, **agreed** Q3).
- A shown, unhired kit **can reappear** at later camps (nothing is
  missable). Only a dismissed **companion** (the named unit) never
  returns; its kit may be drawn again under a new name.
- Card A follows its own rule (7.3): it avoids the free cards' classes
  whenever a legal kit allows, and is exempt from the distinct-class rule
  only when none does, which the board prints ("Two Vanguard cards: no
  other kit covers protector") **(new lead's call)**.

**Determinism (change CMP-3)**
- A new RNG stream `recruit` beside dungeon, combat and loot (main 6.3).
- **At floor-k generation**, each free slot *s* of camp k (slots 1 to
  `cardsMax − 1`, always all of them) pre-draws a fixed ordered
  **candidate list** *(illustr.: 8 entries, `candidateListLength`)* on
  substream `(runSeed, recruit, k, slot s)`. Each entry is drawn with a
  fixed number of draws (the DH 12.5 pattern; `RecruitDrawFixedCount`) and
  holds `{kitId, elementFamily?, nameId, traitId}`: an Arcanist kit's
  element family is part of its entry, so the preview shows it.
- **Free cards depend only on the roster,** which cannot change mid-floor
  (joins and dismissals are camp-only). Each slot takes the first entry
  of its list that is legal (not in the party, not the class of an
  earlier free slot); they are resolved at generation and are exact
  (`PreviewMatchesBoard`).
- **Exhaustion (new lead's call).** If no entry of a list is legal, the
  slot keeps drawing entries on the same substream, up to
  `candidateDrawMax` *(illustr.: 32)* entries in all, then takes the first
  legal kit in data order; if no kit is legal at all, the slot is empty.
- **Card A is resolved at camp entry** by the gap procedure (7.3) on its
  own substream `(runSeed, recruit, k, gap, needId)` (`needId` `none` when
  no need is open). The hero's skills can change mid-floor through
  level-up skill offers (stat model 7.2, items 10), so resolving it at
  generation could offer a need the hero has just trained.
- **Read at camp entry, after catch-up (7.11):** the party's skills (for
  the gap need), the hero's Leadership training and mastery (for the Eye's
  card and re-roll, 9.2).
- Children of the stream: `armReroll` (the "without X" arms, below),
  `eyeReroll` (Recruiter's Eye's re-roll, 9.2), `cosmetic` (palette
  variant), and the reserved `pledge` key (7.12). Each is keyed by camp
  and slot, so no arm or option shifts another's draws.
- Re-entering a saved camp shows the same board. A choice at camp k
  changes camp k + 1 only through the visible roster and the gap
  procedure.

**Next-camp preview (M2)**
- The floor-entry card and the floor map show "Waiting at the next
  camp:" with free cards 1 to `cardsBase − 1` exactly (class icon, role
  tag, element family), a placeholder "+1 card, chosen when you arrive
  (covers what your party lacks)", and, when the hero is trained in
  Leadership, the Eye's card marked "+1 with Leadership". **(told plainly:
  the free cards are exact, card A a placeholder)**

**Reliance arms (classes 6.6, Herald design 5.4)** **(new lead's call:
one mechanism)**
- A "without X" arm first resolves the **base-arm board**, then replaces
  only X's cards: the free cards in slot order, each drawn on `(runSeed,
  recruit, k, slot s, armReroll)` with a fixed draw count from kits whose
  class is not X, not in the party, and not the class of a free card
  before it or of a non-X free card after it; then card A, only if it is
  of class X, by re-running the gap procedure with X's kits removed on
  `(runSeed, recruit, k, gap, needId, armReroll)`. Every other card is
  identical across arms by construction, and the Eye's visibility never
  changes a non-Eye card.
- A need only X could meet is waived and the waiver printed
  **(settled)**.

**Named cards from M3 and Later (lead's call).** An **Old Friend** card
(M3, 12) replaces free slot 1 at camp 1 and counts for the distinct-class
rule (the other free slots are taken around it); it is never card A. A
**Pledge** card (Later, 7.12) sits outside the board: it adds a card,
never a hire, and is exempt from the distinct-class rule, but
`NoDuplicateKitInParty` still applies to it.

**Muster card shape (run state).** `MusterCard {kitId, elementFamily?,
nameId, traitId, preselectedFlex?, isGap, gapNeedId?, waivers[],
slot}` **(lead's call)**.

### 7.3 Guarantees and pity (M2)

- **G1, the guaranteed offer.** Every camp with an open slot shows a
  Muster; a full party sees a swap board. Never a guaranteed hire. This
  mirrors DH 5.1's "availability per floor, never delivery per fight".
  **(lead's call)**
- **G2, guaranteed coverage: the gap card.** **(lead's call; change
  CMP-11, CMP-12)**
  - **Needs, in fixed order** (`recruitSchedule.gapOrder`):
    1. **Medicine-trained** (the get-up route; DH 8 calls it close to
       compulsory);
    2. **Protector kit** (DH 5.8 item 8);
    3. **Mechanics-trained**;
    4. **Arcana for scrolls** (DEP-1).
  - **Met by the party:** Medicine, Mechanics and Arcana are met if
    **any party unit, the hero included,** is trained (best-member
    skills; DEP-1 read as "the party can read any scroll", flagged to the
    consumables owner, CMP-12). **Protector is met only by a companion
    kit**: it exists to protect the hero, so a Vanguard hero does not
    meet it.
  - **The gap procedure (new lead's call; replaces "the first legal
    entry of a pre-drawn list").** At camp entry, after catch-up and the
    top-up (7.11), with the party's skills snapshotted and the free cards
    already fixed:
    1. A kit is **legal for a need** if it can cover it, counting its
       flex options (8.1), is not in the party, is not identical to a
       free card on the board, and (in a "without X" arm) is not of class
       X. For the Arcana need only non-caster kits count (below).
    2. Take the first need in `gapOrder` that the party does not meet,
       that no free card on the board can already cover ("offered", shown
       on that card's coverage strip), and that has at least one legal
       kit. Every unmet need skipped for having no legal kit is waived
       and printed ("Mechanics: no kit available this camp").
    3. Draw card A from that need's legal kits in **preference tiers**:
       (i) a class on no free card and not the hero's class; (ii) a class
       on no free card; (iii) any legal kit. The draw uses the first
       non-empty tier, weighted by `offerWeightPermille`, on `(runSeed,
       recruit, k, gap, needId)` with a fixed draw count. Tier (iii) is the
       only case where card A shares a class with a free card, and the
       board prints it. A forced hero-class card A (tier (i) empty) is
       printed as a note ("Only Shade kits read scrolls without spells").
    4. If no need is left, card A is drawn from every kit not in the
       party and not identical to a free card, in tiers (ii) then (iii),
       weighted by `offerWeightPermille` with the hero's class ×½, on
       `(…, gap, none)`.
    5. If card A covers its need through a flex skill, that option is
       preselected on the join card; so is a free card's flex option when
       that card is what "offers" an open need in step 2.
  - **The Arcana need is covered by a non-caster only (new lead's
    call).** Card A for the Arcana need is drawn from non-caster kits
    (the Scout, fixed; the Outrider, flex). DEP-1's bullet "a recruitable
    non-caster … trained in Arcana" is therefore met literally on every
    board where Arcana is the first need left by step 2 (a Scout free
    card counts, being one); on a board where an earlier need takes card
    A, it rests on CMP-12's party reading **(told plainly; CMP-12)**. The
    Hedgemage's Arcana counts when it is **in the party** (CMP-12's party
    reading), never as card A for that need. In the "without Shade" arm
    the need is waived and printed (classes 7.8; Shade design 13.3).
  - **Shown:** a **coverage strip on every card and on the party bar**,
    with a board header such as "Your party lacks: Medicine". There is no
    single "correct card" ribbon. Waivers are printed ("Arcana: covered
    by your hero").
  - **The vendor's Wax Key line (agreed 2026-11-21, traps and locks
    design Q3; change TL-25; M2).** When the party does not meet the
    Mechanics need, the basic vendor stocks 1 Wax Key (items 10.3),
    read from this card's own "Mechanics met" test on the same
    camp-entry snapshot; it draws nothing and the `recruit` substream is
    untouched. The coverage strip then prints "Mechanics: Wax Key
    stocked at the vendor" beside the Mechanics need, whether or not
    card A offers a Mechanics kit. Hiring a Mechanics companion at that
    Muster does not withdraw the stocked key; the next camp reads the
    new party (traps and locks design 6.11).
  - **Never healing** (`GapCardNeverHeal`). Coverage is guaranteed on
    the **offer**, never on the party.
  - **Feasibility:** `RecruitPoolSatisfiable` runs **this exact
    procedure** (free cards fixed first, then card A) over every hero
    class × hero skill set × reachable party × "without X" arm × Eye on
    and off × cap 3 and cap 2, and asserts: it always ends in a legal
    board; in the base arm, card A exists and covers the first unmet,
    unoffered need whenever the corpus holds a kit for it outside the
    party; every waiver is printed and caused only by having no legal
    kit. It never searches for "some legal board". A named test: "free
    cards Brawler + Drummer, open need protector: card A is a Gatekeeper
    or Shieldbearer (tier iii, printed)".
- **G3, no pity timer.** DH 5.4(f)'s reasons apply. The only memory is
  the visible carried hire. **(lead's call)** (Read by change LE-13: the
  Dream's Due is a loot rule, not recruitment pity; G3 unchanged; loot
  economy design 6.6.)
- **G4, no "party ≥ N by floor X".** Budgets use the actual party; what
  is required instead is that every reachable size is fair (7.9).
  **(lead's call)**

### 7.4 Party cap and the pacing gate

**Cap 3 companions (a party of 4)** **(agreed, Q2; inside decided
"2–3"; change CMP-2)**

| | Cap 3 (chosen) | Cap 2 (written fallback) |
|---|---|---|
| Calibration | Matches enemies 6.3, weights to 39, HE-6 n = 4, items 5.2, Drumroll's L6 "4 targets", Beastbound's "3 slots", the four-unit readability gates | Floor 4–5 expected size, toy evidence and Drumroll's L6 option need re-reading |
| Composition | All four classes can share a party | Four coverage needs squeezed into three bodies |
| Pacing (toy) | About 27–29 min on floors 4–5; about 23 with levers *(illustr.)* | About 21 with levers *(illustr.)* |

**The cap is decided by measurement before balance freezes (agreed,
Q2):**
1. `FloorTimeByPartySize` is the **first** M2 gate, measured on a
   vertical slice: median floor time at party size 4 on floors 4–5 ≤ 20
   minutes (the decided band, main 4.1).
2. A **blind playtest arm** runs party-of-3 and party-of-4 sessions and
   asks testers which felt better, not only which was faster.
3. Every new report (7.7, 11.4) is run at **both caps** from the start.
4. If the gate fails after the lever order below, the cap drops to 2
   (no new slot at camp 3, read exactly in 7.1; `expectedCompanions`
   becomes 0 / 1 / 2 / 2 / 2). The fallback is written into CMP-2.

**How the gate is read (new lead's call):**
- The gate is read on **human playtest sessions** of the vertical slice,
  not on the toy or the simulator's time model; the simulator's turn
  counts are a report beside it.
- **Exploration terms in the time model (change FL-16, floor layout
  design 6.10, 11.4).** The simulator's time model takes the floor
  layout walker's terms: seconds per tick and a cost table per action
  *(illustr.; the companions owner calibrates)*: a step 1 tick; open a
  door 1 click; a container N clicks by type (filled by change LP-8,
  loot sources design 4.6; M2: a quiet Plain open 1 click, auto-closing;
  a Plain open with a notable or healing sundry 2; Twin Coffer 2; Glean
  1 + 1 per notable decision; Steal 2; Viaticum or boss card offer 2; a
  consequence lock 3; values *(illustr.)* until the walker measures
  them); Listen 1 click and 2 ticks
  (0 ticks when passive); Sound the wall 1 click and 6 ticks; a Squeeze
  1 click, +4 ticks on a failure; a Latch 1 click and 1 tick; a reveal 1
  click; each auto-walk stop 1 click. Layout commits, at party 4 on
  floors 4–5, to `ExploreSeconds` ≤ 3.5 min and `ExploreClicks` ≤ 40 as
  medians of the walker's lean policy over the release seed set, and to
  Circuit isolation; these are layout reports beside the gate, not the
  gate. Layout is a gate input, and floors 4–5 at party 4 pass only
  narrowly on the toy, so the cap-2 fallback stays likely (floor layout
  design 10.2). **No layout lever** is added to the lever order below
  (agreed Q2 levers untouched).
- **Hazards in the time model (change HZ-27, hazards design 10; told
  plainly).** Hazards take `HazardMinutesDelta` ≤ +0.3 min per floor
  *inside* the terrain package's +1.6 min tolerance (room terrain
  design TN13), gated together as terrain plus hazards ≤ +1.6; a miss
  fires the hazards cut order (extending room terrain design risk 7's).
  **No hazard lever** is added to the lever order below (agreed
  Q2 levers untouched).
- **Traps and locks in the time model (change TL-38, traps and locks
  design 6.21; told plainly; M2).** Segment 5 takes its own budget line
  of ≤ +0.3 min `ExploreSeconds` and ≤ +5 `ExploreClicks` per floor at
  party 4 on floors 4–5, on top of layout's commitment and reported
  separately (`TrapLockSeconds`, `ExploreClicksDelta`), so the layout
  total there may reach 3.8 min and 45 clicks for segment-5 content
  only. Cost-table rows (clicks / ticks): the lock card 1 / 1 (Take
  time 8); disarm 1 / 2; Probe inside the Hunch stop 0 / 2 (auto-Probe
  0 / 2); Pole it 1 / 1; force 1 / 1 per try; re-arm 1 / 2; the vendor
  Wax Key adds no dungeon clicks. The whole floor still answers to the
  gate; a miss with segment 5 over its line fires that design's cut
  order (6.21). **No trap or lock lever** is added to the lever order
  below (agreed Q2 levers untouched).
- **Encounter placement in the time model (change EP-33, encounter
  placement design 6.18; told plainly; M2).** Segment 6 takes its own
  clock line beside TL-38's: ≤ +0.2 rounds per fight on average and no
  net minutes at party 4 on floors 4–5 *(illustr.)*, reported
  separately; a miss fires that design's cut order before release.
  **No encounter-placement lever** is added to the lever order below
  (agreed Q2 levers untouched).
- **Loot sources in the time model (change LP-10, loot sources design
  4.6; read; M2 / M3).** Segment 7c takes its own clock line beside
  TL-38's and EP-33's: net ≤ 0 minutes and ≤ 0 clicks on floor 3 heavy
  and floors 4–5 at party 4, against a re-run walker baseline, until
  this gate passes with margin; Windfall rounds are converted at this
  time model's seconds per tick (the companions owner calibrates). A
  miss fires that design's cut order **before** any lever below. **No
  loot lever** is added to the lever order below (agreed Q2 levers
  untouched).
- **Definition.** Floor time is the **active time** from the core's
  timestamped `FloorEntered` event to its `BossDefeated` event on that
  floor (every floor ends at its boss), excluding camp screens, pause
  menus and any idle gap over 60 s *(illustr.)*. The gate value is the
  **median over floors 4 and 5** of at least 12 sessions *(illustr.)*
  per cap arm. The core emits these telemetry events from M2-first.
- **Party size is forced.** Gate sessions start from a prepared save at
  floor 4 with the reference schedule's party (4 units; 3 for the blind
  arm's party-of-3 sessions), so testers cannot decline below the size
  being measured.
- **Shipped defaults.** The gate is read on sessions played with the
  shipped defaults: suggestions off (ability 11.3), and the
  accept-rest-of-turn flag off unless the client has said yes.
  Suggestions-on sessions are reported **beside** the gate, never inside
  it.
- **The slow tail is reported, not gated.** The 75th-percentile floor
  time at party 4 on floors 4–5 is reported beside the median; if it
  exceeds 23 minutes *(illustr.)* while the median passes, the lead
  brings it to the client as a future item (18.2). The agreed gate stays
  the median **(agreed, Q2)**.
- **What the slice must hold:** floors 4–5 with their M2 templates and
  one boss each; the eight kits at levels 7–10; the four hero classes at
  those levels; grouped playback (lever 2). Content not in the slice is
  stubbed only where it does not take player time.
- **The drop to cap 2 is pre-authorised** by the agreed Q2 answer: if the
  gate still fails after the levers, the lead applies it and reports it to
  the client; it is not asked again. (Read by change LE-7: the cap-2 coin
  pool row follows `companionCap`; loot economy design 6.4.)
- **Lever 5 needs the client first.** If the client declines the
  accept-rest-of-turn key after seeing the prototype, lever 5 is skipped
  and the gate is read after lever 4.
- **The blind playtest is an input, not a trigger.** If testers clearly
  prefer party 3 *(illustr.: ≥ 60% of sessions)* while the gate passes,
  the lead brings that to the client as a future item (18.2); it never
  drops the cap by itself.

**Lever order** if floors run long **(lead's call; levers 5–6 agreed,
Q2)**:
1. kit trims to 2–3 reads; pre-declared riders (8.3); a per-companion
   "ready" glint (the internal design target is a 5–6 s companion turn);
2. enemy playback fast-forward and **grouped playback by intent** (all
   minions animate together; an M2 requirement);
3. fewer or shorter fights on floors 4–5 (enemies owner): floors 4–5
   drop to 2 guard posts, the floor-4 boss pays 300 experience and the
   floor-5 lair 240, read from `pacing.trimmedFloors`; applied only in
   this order, never by default **(change RT-7, run structure design
   6.6)**;
4. `companion.extraAttackAsDie` (CMP-28); the weapons D-c fallback only
   when its own weapons 11.3 trigger fires (companion-turn median over 10
   s and off-hand turns measurably slower); no M2 kit dual-wields, so D-c
   speeds the hero's turns only;
5. the **accept-rest-of-turn key** (prototyped in M2 behind a playtest
   flag; ships only on the client's yes; CMP-19);
6. cap 2.

**Told plainly:** the 10 s per companion turn target (ability 11.4) does
not fit a party of 4 into a 20-minute floor. Either the per-turn time,
the late fight count or the cap must give. The 5–6 s figure is an
internal design target; ability 11.4's playtest target is unchanged.

### 7.5 Declining (M2)

**(lead's call; the right to decline agreed, Q1)**

- **D1.** Any card, or the whole board, is declined with **Travel on**:
  one click plus a one-line confirmation showing the forecast (7.11).
- **D2, the carried hire.** If no one is hired at a camp where a slot was
  open **on arrival**, the next camp allows **one extra hire** (up to 2),
  with no extra card. It holds one hire and never stacks. With 3 cards
  and 2 hires, deferring gives less choice than two separate boards, so
  it is never a better-pick tool. **A dismissal never creates a carried
  hire (new lead's call):** only an open slot left unfilled does.
- **D3.** Nothing is missable: unhired kits can reappear (7.2).
- **A declared Lone Road run (M3; new lead's call)** hides the Musters
  and is otherwise identical to declining every card: the same shortfall
  weights, no extra bonus.

### 7.6 Dismissal and swap (M2)

**(lead's call)**

- **Camp only.** A mid-floor dismissal is never offered: weight is fixed,
  so it would be pure loss and a griefable misclick. A Downed companion is
  up by camp step 1, so it never blocks.
- **One dismissal per camp** (`recruitSchedule.dismissalsPerCamp` 1),
  covering a plain dismissal and a swap alike: a dismissal and a hire in
  the same visit **is** the swap. The same-visit undo of a hire (below)
  is not a dismissal. **(lead's call; new lead's call for the count)**
- **Swap:** it uses one of the camp's hires. The newcomer joins at the
  hero's level with defaulted choices, so swapping never costs levels.
  **A camp's hires may be spent on joins or on the one swap (new lead's
  call):** with a carried hire and an open slot, a player may swap one
  companion and hire one.
- **Swap forecast (new lead's call).** A swap made while the party has an
  open slot shows on its confirmation: "Swapping uses this camp's hire:
  your party stays at N" (no carried hire is earned, because someone was
  hired; the 4-floor line is deleted, change RT-1).
- **A plain dismissal** (no newcomer) frees the slot; the ladder lets a
  later camp refill it. Its confirmation says "X leaves for good".
- **What leaves (change CMP-21):** every item instance issued by a
  recruit kit is **`kitBound`**: it carries the instance field
  `boundToUnitId` (null on every other instance), set when the kit is
  issued. A bound instance:
  - sells and scraps for 0 and shows "Unsellable: kit": the approved
    `unsellable` treatment (items 12.3, 10.6) applied to the instance
    **(new lead's call: reuse the approved rule rather than a new sell
    rule)**;
  - never stacks with an instance of a different `boundToUnitId` (items
    12.8's stack test gains this field), so two companions' kit potions
    stay two stacks and the core always knows whose dose leaves;
  - is deleted when its unit is dismissed, from wherever it is in the
    party (`ItemDestroyed{dismissed}`; a drunk potion is simply gone),
    including a dose the DH 13.2 top-up moved to the hero, which keeps
    its `boundToUnitId`;
  - can be equipped by anyone while its unit is in the party.
  The kit potion is the **ordinary healing potion record** with
  `boundToUnitId` set, so DH 13.2 counts it as a healing potion (CMP-21).
  Looted gear, trinkets and other consumables stay with the party (items
  6.2). Dismiss is greyed `PackFull` until there is room, as Buy is.
- **Same-visit undo (new lead's call).** A hire may be undone before
  Travel on. The core restores the Muster snapshot taken at that hire:
  the card is shown again and hireable, the hires used and the carried
  hire are restored, the newcomer's bound items are deleted, and any other
  item it holds (gear bought or moved onto it) goes to the hero or the
  pack by 6.2 placement; the undo is greyed `PackFull` until there is
  room. Its join-card and Rethink choices are discarded. **An undone swap
  does not bring back the dismissed companion:** the swap confirmation
  says "X leaves for good", and the dismissal stays used.
- **Top-up re-check (change CMP-22; narrowed).** On Travel on, the DH
  13.2 procedure runs again **only if a dismissal this visit deleted a
  healing potion the hero held** (a moved bound dose). It then runs in
  full: first it moves a companion's dose to the hero (a newcomer's kit
  potion included), and only if no party unit holds one does it issue
  the camp-issue potion. Selling, handing over or drinking potions at
  camp never triggers it, so handing potions around still never buys a
  free one **(told plainly)**.
- `recruitKitPotions` stays 1 **(agreed, DH 5.5)**; nothing in this rule
  changes how it is issued.

### 7.7 Compensation: declining is neither a trap nor a must-take

**What compensates (in order)**

1. **Scaling: the shortfall discount (agreed, Q1; change CMP-5,
   CMP-6).** On floors 2–5:
   `partyWeight = 15 + 8c − shortfall[expected − actual]`, where c is the
   actual companion count, `expected = recruitSchedule.expectedCompanions
   [floor]`, and `partyWeight.shortfall` = [0, 2, 3, 4] *(illustr.; the
   cap-3 key, below)*
   indexed by the number of missing companions. Floor 1 and the reference
   party are unchanged.
   - **Reading (new lead's call):** `expectedCompanions(floor)` is
     **computed in code, never stored** (3.2): the number of camps in
     `slotOpensAtCamp` before `floor`, capped at `companionCap`, which is
     min(floor − 1, cap). It follows the slot ladder and the cap, not the
     enemies 6.3 prose column. `floor` is the floor being generated (floor
     N + 1 is generated when the party leaves camp N, items 10.4), 1-based.
     The shortfall index is clamped to 0 below and to the table's last
     entry above.
   - **One key per cap (new lead's call):** `partyWeight.shortfall.cap3`
     (4 entries, [0, 2, 3, 4] *(illustr.)*) and
     `partyWeight.shortfall.cap2` (3 entries, starting at [0, 2, 3]
     *(illustr.)*), each of length `companionCap + 1` and tuned by its own
     reports, since every report runs at both caps. The live key follows
     `companionCap`.
   - **Bound (new lead's call):** no key entry may take a solo party below
     `soloWeightMin` (10 *(illustr.)*, the lowest weight the solo
     composition table covers): 15 − the key's last entry ≥
     `soloWeightMin`. `SoloBudgetMonotone` fails the build otherwise, so
     the escalation below can never leave the validated range.
   - Worked *(illustr.)*: floor 3 solo, 15 − 3 = 12; floor 4 duo,
     23 − 3 = 20; floor 4 trio, 31 − 2 = 29; floor 5 solo, 15 − 4 = 11.
   - Shipped as **tuning keys read against `SoloRunReport`**, never as
     formula constants: solo results sit on a cliff (one standard unit is
     10 of a budget of `soloWeightMin` to 15).
   - Shipped **only with a solo composition rule**: solo on floors 2–5
     uses at most 1 standard unit plus the minion cap, with the remainder
     spent by a fixed table owned by the enemies owner. The table is
     **deterministic** (no RNG): it maps (floor, band, weight) to one
     composition. Validator `SoloBudgetMonotone`: for each floor 2–5, each
     band and each integer weight *w* from 15 − the live key's largest
     entry to 15, the table's fill(*w*) and expected round damage ERD(*w*)
     are **non-decreasing in *w***, where ERD = Σ over units of (hit
     permille against the reference solo hero's AC at the floor's entry
     level × mean damage per round). It enumerates every cell exactly.
   - **Formations on the solo table (change EP-19, encounter placement
     design 6.3; M1 static parts, M2 the rest).** The solo table gains a
     formation column; `SoloShapeMonotone` extends `SoloBudgetMonotone`
     (as weight rises, the standard's level never falls, threat lanes at
     fight start never decrease, front positions ≥ back positions); a
     solo hero on floors 2–5 meets no ambush form and `billetScale` 0
     (500‰ on floor 1); placement gates read `partyCountAtGeneration`.
   - **What it does not touch (new lead's call):** experience (fixed at
     the band's nominal value, enemies 16) and the per-floor loot budget
     (main 4.6; enemies 15.3) are unchanged by the shortfall; only
     encounter budgets move. Found coin is part of that budget: a
     fixed pool per party, keyed by the active `companionCap` (the
     expected-party count above read at that cap), never the actual party (change LE-7, read; **agreed 2026-11-22, loot economy
     Q4**; loot economy design 6.4, 6.8).
2. **Fairness (change CMP-8):** the enemies 6.4 guards and the DH 21.1
   rows at every reachable party size; the solo round-1 row also on solo
   floors 2–5. Kept as fairness only: the toy shows it never binds (solo
   deaths come from round 2 on) **(told plainly)**.
3. **Choice:** the carried hire; nothing missable.
4. **What a small party keeps:** experience is not split (enemies 16);
   guaranteed healing per member is higher (HE-6 n = 1); loot is shared
   among fewer bodies.
5. **Recognition (M3):** a declared **Lone Road** run (no Musters; a
   badge on the run record) and milestones: "Win on the Lone Road", "Win
   with at most one companion", "Beat the floor-2 boss with no
   companions", "Win with all four classes in the party". They unlock
   kits, cosmetics or a starting-boon option, **never stats** (main 4.7)
   **(told plainly, accepted with Q1)**. Lone Road and the milestones
   count a win for any hero class.

**Rejected** **(lead's call; Lone Wolf told plainly with Q1)**
- **Lone Wolf** stat bonuses: the toy's buffed duo (32.6%) beats the full
  party (27.2%) *(illustr.)*; it would also rewrite shared formulas
  (classes 6.1).
- **Muster stipend,** extra potions, an extra rest: about 0–2 points each
  in the toy; cost without payoff. Parked as measured levers only.
- **Dismiss-for-reward:** a churn income line.

**Targets (reports; the client may promote any to a gate)** **(lead's
call)** Ids CT-1 to CT-6 ("companion targets") are local to this table.
Every target is read at each cap, in cap terms; win rates are in
permille, on the same seeds.

| Id | Target |
|---|---|
| CT-1 `DeclineOneSpread` | Ending with `cap − 1` companions instead of `cap` costs ≤ 100‰ (10 points) *(the client's 10 reused)*: max over *k* of (`Reference` − `DeclineAt{k}`) |
| CT-2 `DeclineTwoSpread` | Ending with `cap − 2` companions costs ≤ 150‰ *(illustr.)*: max over *j < k* of (`Reference` − `DeclineTwo{j,k}`) |
| CT-3 `SoloRunReport` | Full decline (`AlwaysDecline`): win rate 40–70% of the reference *(illustr.)*. Below → the lead raises the shortfall keys. Above the reference → something is mispriced |
| CT-4 | Every deferral arm ≤ the reference **under the Oracle arm** (11.3), not just Reference |
| CT-5 | No smaller party beats the reference |
| CT-6 `SoloClassSpread` | Solo win rate spread across hero classes ≤ 100‰ *(illustr.)*. Levers: companion-reading class features (class owners); floor 1 untouched |

**Escalation (new lead's call).** The lead moves the shortfall keys one
step at a time with a rerun, inside three bounds: CT-5 (no smaller party
beats the reference), CT-1 (the last companion keeps its value) and the
`soloWeightMin` bound above. If CT-3 cannot be met inside those bounds,
or `SoloBudgetMonotone` cannot be met by the solo composition table, the
miss goes to the client (18.2); the lead never adds a stat bonus or
touches floor 1 or the reference weights.

### 7.8 Costs (M2)

**Free (lead's call).** The price is party weight, the worst-member
Stealth change, one more turn per round and the spread of loot and
potions, all shown on the card.

Rejected: **gold** (charges twice, competes with potions under the P1
peg, snowballs poor runs); **recruit instead of the shop or reward**
(changes decided main 4.1 and agreed I-13; taxes the weakest camps); **an
experience share** (breaks the enemies 16 curve).

### 7.9 Encounter scaling

- The budget formula is unchanged except the shortfall term (7.7):
  weight × band% × depth% × ladder%, weight fixed when leaving camp
  **(agreed, enemies 10.1)**.
- Joins and dismissals happen only at camp, before the next floor is
  generated, so pricing is always exact; "a companion lost mid-floor does
  not lower budgets" never punishes a choice.
- **Boss HP reads the actual party at generation (CMP-7, mandatory).**
  Reading the expected size gives a 0% solo win rate in the toy.
  - **Data (new lead's call).** Each boss record carries
    `hpByPartySize[1..4]`, integers authored to enemies 17.5's "6 × the
    party's typical damage per round × 0.85" at the floor's entry level
    for a party of that size, calibrated by the simulator. The boss's HP is
    the entry for the **actual** party size when the floor is generated.
    A hand-designed boss keeps a computable HP without a per-kit damage
    model.
  - **Escort (new lead's call).** The Boss Court's escort budget is the
    **shortfall-adjusted** party weight × 200% × depth% × ladder%, as for
    every band; boss HP **ignores** the shortfall. A declined party meets a
    smaller escort and a boss sized for its real body count.
  - A content test row per boss per party size 1–4 (CMP-8).
- The enemies 10.5 generator and 17.4 template content tests, the 6.4
  permadeath guards and the AR-20 lowest-legal-AC runs are **re-run at
  every weight the run can reach**, including the shortfall minimum
  (CMP-8) **(told plainly)**. The CMP-8 tests extend to formations and
  the solo column: a formation is drawable at a weight only if its core
  positions fit the fill down to the shortfall minimum
  (`FormationSurvivesFill`; encounter placement design 6.3, 11.2;
  EP-19; M2).
- Enemies 20.3's "flat across sizes" row is re-read against an expected
  curve, plus a hero-death-per-fight row by party size (CMP-9).

### 7.10 Solo floor 1

**(settled; held by construction)** `NoFloor1Companion`. DH 17.1's "only
party unit" is always true on floor 1; the floor-1 solo rows, solo minion
caps, HE-6's floor-1 n = 1 row and the solo `Floor1ClassSpread` gate are
untouched. Classes 7.3 still holds: the subclass lands at the end of the
solo floor, just before the first recruit. A Pledge found on floor 1
(Later) is used only at camp 1.

### 7.11 The camp and the Muster screen

**Camp order (lead's call; change CMP-31 on DH 13.1; step 6a and the
door commit at step 11 change RT-10, RT-11, run structure design 6.8):**
1. Downed companions get up (DH 13.1 step 1);
2. **camp catch-up** (enemies 16) for the hero and every companion, with
   their level-up choices and any skill offer resolved here, hero first
   (ability 6.9; CMP-17);
3. all HP and every pool restored, to the post-catch-up maxima (DH 13.1
   step 2);
4. every lingering condition ends, Scarred included (DH 13.1 step 3);
5. +1 supply (DH 13.1 step 4);
6. the hero's potion top-up (DH 13.1 step 5; DH 13.2);
6a. **the next-floor card** (random build); when route choice is on (run
    structure design 6.11), **both door cards** at camps 1–2; at
    camp 4, **the final-boss card**, or **the Last Hollow choice** when
    the Last Hollow choice is on (run structure design 6.11). Information
    comes before the Muster, so hires, swaps, purchases and Herald card
    swaps can respond to it;
7. the party's skills, the hero's Leadership training and mastery are
   snapshotted; card A is resolved (7.3) and the Eye's card shown or
   skipped (9.2);
8. **Muster** (revisitable until Travel on, so a newcomer can be geared
   at the vendor); a recruit joins at the post-catch-up hero level;
9. vendor (DH 13.1 step 6);
10. shop or reward (unchanged, **decided**, main 4.1; items 10.4);
11. Travel on (the narrowed top-up re-check, 7.6; when route choice is
    on (run structure design 6.11), it commits the chosen door, which can
    be changed until then; floor N + 1 is generated with the final
    party).

The hero already reached level 3 at the floor-1 antechamber **(change
RT-8, run structure design 9.4)**, so the recruit's join card at camp 1
shows the L3 choice; a skill the
catch-up grants (say Medicine) moves card A to the next need (11.5).

**Two-tier card (lead's call):**
- **Front:** portrait and silhouette, generated name, one primary role
  tag (Protector, Tempo, Control, Skills, Striker, Area, Ward; the kit's
  `primaryRoleTag`, 8.3), the secondary **Medic** badge when a fixed skill
  or the preselected flex is Medicine, a one-line verb, the coverage
  strip. Read in a glance.
- **Detail on focus:** class icon, kit name, skills and flex options,
  signature line, L3/6/9 preview, trait line, "Can be Downed, never
  killed", and the **Party Forecast** line.

**Party Forecast** (computed by the core; on every card and on Travel
on) **(lead's call)**:
- next floor's sizing, framed as sizing, never as a bonus: "Floor 3
  fights sized for 3 (weight 31)" or "Floor 3 sized for you alone (weight
  12)";
- coverage change ("covers Mechanics");
- worst-member Stealth ("Party Stealth +4 → +1");
- turn load ("+1 turn per round");
- on Travel on: "Fewer companions: smaller fights, but every blow lands
  on fewer of you", and the carried-hire line (7.1; the stranded-slot
  line is deleted, change RT-1);
- on a swap made while a slot is open: the swap forecast lines (7.6).

**First-recruit beat (M2, cosmetic; lead's call):** on a run's first
hire, the companion's signature plays as a 3-second intro, and the first
floor-2 preview highlights "your new ally can do X". A one-time prompt
offers to turn on companion suggestions; the default stays off
**(agreed, ability 11.3)**.

### 7.12 In-dungeon sources (Later; slot reserved at M2)

**(lead's call; change CMP-10; wave unchanged, Later)**
- **Pledges:** a freed captive (an optional room feature on floors 2–4),
  a wanderer or a shrine gives a named candidate who waits at the
  **next** camp as an extra card outside the 3, marked "Met on floor N".
  It never joins mid-floor, never fights and is never in the weight.
  The captive is the encounter placement design's Captive form (a
  chained non-combatant behind a formation; `captiveSocket` reserved;
  EP-39, encounter placement design 9.1; Later).
- On freeing: **Meet at camp** (nothing freed is missable) or **Send
  home**, which gives a **Parting Word**: one floor fact chosen from the
  enemies 14.6 categories (information, inside the loot budget, not a
  class grant).
- At most 1 Pledge per floor; a Pledge adds a card, never a hire.
- **Yielded monsters: rejected** (no class, stat model 5.5; class ids are
  launch ids, classes 7.8; mercy would become a lottery, enemies 14.6).
- **Yielded rival-party survivors:** Later, with the rival party
  (enemies 18.1).
- Run state reserves `pledges[]` at M2 with its element shape fixed now,
  `PledgeState {kitId, sourceFloor, sourceKind (captive | wanderer |
  shrine), nameId, metAtFloor}`, and the substream key `(runSeed,
  recruit, floor, pledge)` is reserved and used by no M2 code, so adding
  Pledges later changes no schema, no save and no M2 draw **(new lead's
  call)**. Promoting captives to M4 is a future item for the client.

## 8. What a companion is

### 8.1 Chassis

| Field | Rule | Source / status |
|---|---|---|
| Class | A launch class id | **(settled, classes 7.8)** |
| HP tier, save, armour ceiling, shields, weapon training | The class record's | **(settled, stat model 5.5)** |
| Skills | **Fixed skills, plus one flex skill chosen on the join card from two options on the class list; fixed from then on.** No Presence-keyed skill (Intimidation, Leadership) | **(told plainly; change CMP-13, stat model 7.2)**; `NoPresenceOnKit`, `KitSkillsOnClassList` |
| Ability array | Fixed per kit, the hero spread +3 / +2 / +1 / +1 / 0 / −1; main +3; −1 never on main | **(lead's call, stat model 5.1)**; `CompanionArrayLegal` |
| Ability increases | **None.** `companion.mainIncreases` exists as a lever, set to `[]` | **(lead's call; change CMP-16)**; answers armour 13.2, 22 |
| Main ability | The class's main; Herald kits Wisdom | **(settled, classes 7.8)** |
| HP | 11 + L × max(1, tier + Con) | **(agreed, DH 7.1)** |
| Recovery, proficiency, AC, saves, initiative, speed | Stat model rows, unchanged | **(settled)** |
| `restDefensiveTool` | **Not required on kits;** it binds class records (`Floor1BossSustain`), and floor 1 is solo | **(lead's call; change CMP-24)** |
| Kit potion | 1, `kitBound` | **(agreed, DH 5.5)**; CMP-21 |

**Why the flex skill.** Under a coverage-greedy pick, the toy's final
parties converged on the two kits that each covered two needs (25–31% of
slots each), while the three most distinct combat kits fell to 1–5%
*(illustr.)*. Spreading fixed tags alone did not fix it. Moving one skill
to the player's choice lets the player take the kit they want in combat
and still cover a missing skill; protector stays kit-bound. This is
**expected** to fix convergence but is **not yet measured**: `gen.py`
must be re-run with the flex pick before M2 sign-off. If it fails, the
fallback is the policy's combat-value term plus more kits at M4.

**Illustrative HP** (DH 7.1) *(illustr.)*, by each kit's own Con, at
levels 3 / 5 / 7 / 10: Vanguard kits (tier 8, Con +2) 41 / 61 / 81 /
111; tier 7 with Con +2 (Outrider, Drummer, Shieldbearer) 38 / 56 / 74 /
101; tier 7 with Con +1 (Scout) 35 / 51 / 67 / 91; Arcanist kits (6,
+1) 32 / 46 / 60 / 81.

`CompanionAcFloor` **(lead's call)**: every kit's AC from its starting
gear and fixed Dexterity is at or above the class-derived lowest legal AC
at levels 1, 5 and 10, so companions never lower the AR-20 bound.

### 8.2 Progression and key choices

- **Level always equals the hero's (change CMP-16, CMP-17).** A recruit
  joins at the hero's level with full HP and pools, levels on the same
  combat-end screen, hero first (ability 6.9), and is raised with the
  hero at camp catch-up (enemies 16). A Downed companion still levels and
  stays on 0 HP (DH 21.4). **(lead's call)**
- **Rejected: "a level behind".** Enemies 6.3 has one party-level column;
  experience scales off the hero; a lagging late recruit is a trap.
- **Automatic gains** *(illustr.)*: proficiency and HP every level; at
  level 5 the class scaling step (Vanguard kits Extra Attack; Shade kits'
  Opening open tier +2 → 1d6, as for the hero; Arcanist kits a second
  Mote beam and slot pool +1; Herald kits one weapon-die step, which keeps
  their turns short); at level 10, +1 use on the kit's `rest` pool.
- **One choice grammar for all kits** (the Herald design 7.17 Drummer
  pattern): 2 options per level, option A the kit default, one confirm,
  each option showing its computed change **(lead's call)**. The generic
  shape, which each kit fills with its own rows:

| Level | Option A | Option B |
|---|---|---|
| 3 | Rest ability +1 use | Signature range, size or accuracy +1 step |
| 6 | Rest ability +1 step (a second target where it has one, else +1 use or +1 square) | Signature +1 target |
| 9 | Signature 2 per camp (a caster kit: slot pool +1) | Rest ability +1 use |

  **Per-kit rows (lead's call; numbers illustr., shapes fixed):**

| Kit | L3 (A / B) | L6 (A / B) | L9 (A / B) |
|---|---|---|---|
| Gatekeeper | Jostle 3 uses / Close Ranks reaches allies within 2 | Jostle push 2 / Close Ranks lasts to the end of your next turn | Close Ranks 2 per camp / Jostle 4 uses |
| Brawler | Barge 3 uses / Heave push 4 | Barge on a target up to one size larger / Heave a second target adjacent to the first (own roll) | Heave 2 per camp / Barge 4 uses |
| Scout | Sidestep 3 uses / Staggering Shot +2 to hit | Sidestep 4 uses / Staggering Shot a second target within 2 of the first (own roll) | Staggering Shot 2 per camp / Sidestep 5 uses |
| Outrider | Underfoot 3 uses / Knife Work +2 to hit | Underfoot 4 uses / Knife Work's two attacks may target two adjacent hostiles | Knife Work 2 per camp / Underfoot 5 uses |
| Tinker | Flare 3 uses / Spatter range +4 | Flare 4 uses / Shockwave push +1 | Slot pool +1 / Flare 5 uses |
| Hedgemage | Flare 3 uses / Grasping Roots range +4 | Flare 4 uses / Drench burst radius 1 → 2 (inside 25 squares) | Slot pool +1 / Flare 5 uses |
| Drummer | As Herald design 7.17 **(settled)** | | |
| Shieldbearer | As 8.3 (CMP-25) | | |

  Options are numeric, range, size, duration or target-count steps only
  (`KitChoicesNumericOnly`; no new mode, effect or action), which keeps
  them clear of the forbidden grants and of re-tuning exploits. Every
  choice has a default (`KitChoicesDefaulted`).
- **Backlog on the join card.** Every recruit joins at level 3 or higher,
  so the L3 choice (and L6 for a recruit who joins at level 6 or higher)
  appears inline on the join card, defaulted.
- **Rethink (M2; lead's call):** at camp, once per camp per companion,
  re-pick one of its 3/6/9 choices. Free, not a camp-pool spend. Never
  re-picks a kit's stance (Vanguard design 7.5), class, skills or flex
  skill.

### 8.3 The eight M2 kits

**Distinctness (`SliceNotCopy`, checked by `effectKinds` and by name)**
**(lead's call; classes 7.8)**:

| Class | The hero keeps | The kit's slice |
|---|---|---|
| Vanguard | Techniques decided **after** the hit; Momentum; Surge; Second Wind; Arts; stance re-pick; subclass | Riders **declared before the roll**, spent only on a hit; stance fixed by the kit |
| Shade | Stealth tier; Hidden strikes; Vanish; Fade; Duck; From Nowhere; subclass | The **open tier** only, one Opening trigger per kit; never Hide-to-strike |
| Arcanist | The element pair; Interplay; full Weft; slot tiers; Overchannel; Arts; Knit | One element family (drawn with the candidate entry, 7.2); one combined `camp` slot pool with 2 fixed, named spells; Flare as its reaction; a simple weapon as its physical fallback |
| Herald | Cadence (Frighten, Spur, Rekindle); presence spells; Rally; Leadership and Intimidation | Spurred grants on its own pools (CL-5); Wisdom main; no heal |

A recruit of the hero's class is therefore a different slice by
construction; the ×½ weight and card A's hero-class tier (7.3) are about
variety, not identity. Each class lists its hero-only features in
`heroOnlyFeatureIds` (Momentum, Surge, Second Wind, Arts, the Techniques,
Stand Fast, the subclass features; Vanish, Fade, Duck, From Nowhere, the
stealth tier; Overchannel, Weft, Interplay, Knit, Sculpt; Frighten, Spur,
Rekindle, presence spells, Rally), which `SliceNotCopy` reads (11.2).

**Kit table** *(names placeholders; numbers illustr.; stat sheets are
stage-2 data)*:

| Kit | Class / primary role | Array | Skills: fixed; flex (choose 1) | Gear |
|---|---|---|---|---|
| **Gatekeeper** | Vanguard, Protector | Str +3, Con +2, Wis +1, Dex +1, Int 0, Pre −1 | Athletics; Medicine or Perception | Chain mail, shield, warhammer |
| **Brawler** | Vanguard, Control | Str +3, Con +2, Dex +1, Wis +1, Pre 0, Int −1 | Athletics; Medicine or Lore | Chain mail, maul |
| **Scout** | Shade, Skills | Dex +3, Int +2, Wis +1, Con +1, Str 0, Pre −1 | Arcana, Mechanics; Perception or Stealth | Leather, shortbow, dagger |
| **Outrider** | Shade, Striker | Dex +3, Con +2, Wis +1, Int +1, Str 0, Pre −1 | Stealth, Acrobatics; Medicine or Arcana | Leather, short sword, darts |
| **Tinker** | Arcanist, Area | Int +3, Dex +2, Con +1, Wis +1, Str 0, Pre −1 | Mechanics; Lore or Medicine | Leather, dagger |
| **Hedgemage** | Arcanist, Ward | Int +3, Dex +2, Wis +1, Con +1, Pre 0, Str −1 | Arcana; Medicine or Perception | Leather, staff |
| **Drummer** | Herald, Tempo | Wis +3, Con +2, Dex +1, Str +1, Int 0, Pre −1 | Perception; Medicine or Lore | Hide (Sure-Footed), spear **(settled, Herald design 7.17)** |
| **Shieldbearer** | Herald, Protector | Wis +3, Con +2, Str +1, Dex +1, Int 0, Pre −1 | Medicine; Lore or Perception | Chain shirt, shield, mace **(settled)** |

Every skill is on its class's list (classes 8.3–8.6), and each kit's
skill count equals its class's (2, or 3 for the Shade). **Role tags (new
lead's call):** each kit has exactly one `primaryRoleTag` from Protector,
Tempo, Control, Skills, Striker, Area, Ward, and no two M2 kits of one
class share one (`KitRoleTagUnique`). **Medic** is a secondary badge,
shown when a fixed skill or the chosen (or preselected) flex skill is
Medicine (the Shieldbearer always).

**Fallback damage (new lead's call; classes 6.4, 6.5).** Every kit has a
"slowed, never locked out" fallback in `fallbackDamageTypes`: the
Arcanist kits carry a simple weapon inside the class training (the
Tinker a dagger, piercing; the Hedgemage a staff, bludgeoning), as the
Arcanist hero's fallback is physical; a weapon attack is the universal
action, not a kit at-will. Every other kit's weapon gives it a physical
type (`KitHasFallbackDamage`).

**Abilities** (each kit: 2 at-wills, ≤ 3 limited, 1 reaction, one camp
signature; at most 3 things to read) **(lead's call unless marked)**:

- **Gatekeeper.** At-wills: weapon attack; Shove. Reaction: **Interpose**
  (stance fixed Shieldwall; pool-free; +2 AC for an adjacent ally;
  `ifItChangesOutcome`; requires a shield; never with a Guarding shield,
  CL-7). `rest` 2 **Jostle**: declared before the roll; on a hit, push 1.
  `camp` 1 signature **Close Ranks**: action; until the start of your
  next turn, attack rolls against you and each ally adjacent to you get a
  **step-0 window fill** of +1 AC *(illustr.)* (armour 10.4), and you
  cannot move this turn. **(new lead's call; change CMP-34)** A window
  fill counts as the one reaction AC bonus on that roll (armour 10.4,
  11 item 1), so it never stacks with Interpose, Parry or any other
  reaction bonus, and it adds no term to the closed AC query (armour 6.1).
  It differs from the Vanguard hero's Stand Fast (adjacent allies only,
  shield, subclass rider): it covers the Gatekeeper too, needs no shield
  and carries no rider. `KitAcEnvelope` checks it inside the charged
  envelope.
- **Brawler.** At-wills: weapon attack; Shove. Reaction: opportunity
  attack, stance fixed **Hold the Line**. `rest` 2 **Barge**: declared
  before the roll; on a hit, the target is knocked Prone if it is your
  size or smaller (tagged `trip`, so Anchored and `prone` immunity apply;
  a kit source added to the Prone sources, CMP-32). `camp` 1 signature
  **Heave**: a weapon attack; on a hit, push the target up to 3 squares.
  Single target: the rejected Vanguard universal sweep stays rejected.
- **Scout.** At-wills: launcher attack; dagger. Reaction: opportunity
  attack (`nonMinion`). Passive **Gap Sight**: once per round, the open
  tier Opening (+2, 1d6 from level 5) on a launcher or thrown hit against
  an **Unready or Staggered** target, shown as a glint **(new lead's
  call: Staggered added so the passive lives past round 1; it pairs with
  Staggering Shot and the party's bludgeoning riders; a kit-only trigger
  for the Shade owner's registry, CMP-33)**. `rest` 2 **Sidestep**: bonus
  action, Disengage only (no Hide, no Dash). `camp` 1 signature
  **Staggering Shot**: a launcher attack; on a hit the target is
  Staggered (a new Staggered source, CMP-32).
- **Outrider.** At-wills: melee; thrown. Reaction: opportunity attack
  (`nonMinion`). Passive **Flank Sense**: once per round, the open-tier
  Opening on a melee hit against a **Beset** target. `rest` 2
  **Underfoot**: bonus action; Disengage, and you may move through
  hostiles' squares this turn. `camp` 1 signature **Knife Work**: an
  action; two melee attacks against one target, each counting as against
  a Beset target, and Flank Sense's once-per-round limit is lifted for
  this action **(new lead's call: the old text added nothing when Beset
  was already true; this one always beats the best at-will; the Shade
  owner's registry, CMP-33)**.
- **Tinker.** At-wills: **Mote** (its element family; two beams from
  level 5); **Gust**. Reaction: **Flare** on a `rest` 2 pool (+2 AC,
  `ifItChangesOutcome`). `camp`: one combined slot pool (2; 3 at level 5;
  4 at level 9) holding 2 fixed spells from the Arcanist list with
  `classTags`: **Spatter** (family template, resolved to the kit's
  family; the **signature**) and **Shockwave** (family-free push), never
  Binder- or Alembic-exclusive. Tier automatic from level **(new lead's
  call: spells named)**.
- **Hedgemage.** At-wills: **Mote** (its element family); **Help**.
  Reaction: Flare (`rest` 2). A slot pool of the same shape holding
  **Grasping Roots** (family-free lockdown, concentration; the
  **signature**) and **Drench** (family-free; a willing ally may skip the
  save and is put out). It differs from the Tinker in an at-will, both
  spells and its signature `effectKinds` (`KitsDistinctWithinClass`)
  **(new lead's call)**. Its Arcana, as a caster's, covers the scroll need
  only through CMP-12's party reading (7.3). **No ally heal at M2
  (agreed, Q3).**
- **Element family.** An Arcanist kit's family is drawn with its
  candidate entry (7.2) and fixed for the companion; family templates
  ([F]) resolve to that family's variant through `spellTemplate.families`,
  so no per-family spell table is needed. Two Tinkers of different
  families are the same kit for `NoDuplicateKitInParty`.
- **Drummer.** Exactly Herald design 7.17: weapon attack and Help;
  `nonMinion` opportunity attack; `rest` 2 Quickstep; `camp` 1 Drumroll;
  L3 / L6 / L9 as written there **(settled)**.
- **Shieldbearer.** Exactly Herald design 7.17: weapon attack and Shove;
  pool-free Interpose (`ifItChangesOutcome`; a Guarding shield is
  illegal); `rest` 2 Guard Step; `camp` 1 Form Up **(settled)**.
  **Choices written here (change CMP-25):** L3 Guard Step 3 uses, or Form
  Up range 8; L6 Guard Step also gives Spurred to one adjacent ally, or
  Form Up 3 targets; L9 Form Up 2 per camp, or Guard Step 4 uses. Spurred
  never stacks and is never granted to the whole party every turn
  **(settled)**.

**Limited-ability count (settled, classes 7.8).** A caster kit's combined
slot pool **counts as one** of the three limited abilities. Arcanist kits
therefore carry two (Flare's pool and the slot pool); every other kit
carries two. `KitShape` counts the slot pool once, as approved; ability
11.1 is not changed.

**Movement in kits (lead's call; classes 6.3):** kits may grant
Disengage and moving through hostile squares; they may not grant Dash or
any extra movement, which reads as "+speed (for kits)", **except Spurred
on Herald-class kits**, which the conditions design names as a source
(CL-5; Herald design 7.17) **(settled exemption)**. `ForbiddenClassGrant`
encodes that one exemption when run on kits. The draft "Cover Me" (move
up to your speed) and the Scout's bonus-action Dash are removed.

**Corpus coverage (`KitCorpusCoverage`):**
- Protector: Gatekeeper, Shieldbearer (2 classes) **(settled
  requirement, classes 7.8; DH 27)**.
- Arcana non-caster: Scout (fixed), Outrider (flex) (DEP-1).
- Medicine: Shieldbearer (fixed); Gatekeeper, Brawler, Outrider, Tinker,
  Hedgemage, Drummer (flex).
- Mechanics: Scout, Tinker (fixed).
- Stealth: Outrider (fixed), Scout (flex), so a Shade hero's bypass can
  survive recruiting.
- Lore: Brawler, Tinker, Drummer, Shieldbearer (flex) (trinkets 23's
  spread input).
- Ally heal: none **(agreed, Q3)**.

No kit carries Square Up, so the Vanguard Square Up II pairing list does
not apply (Vanguard design 8.2). Weapon families are inside each class's
`weaponTraining` (`KitWeaponTrained`; weapons 26).

### 8.4 Equipment, consumables and inventory

**(lead's call; settled where marked)**
- A companion equips anything its class trainings allow: weapon
  families, the armour ceiling, shields, trinkets whose `requires` it
  meets, a belt of 2 (hard max 3), a pack of max(2, 3 + Str) **(settled,
  items 5.2, 6)**. Weapon verbs apply automatically (weapons I1). Fitted
  armour is aimed at companions.
- Kit rows with `requires` (Interpose and Shieldwall: a shield; Staggering
  Shot: a launcher) grey out with their reason if broken (ability 15.4);
  regear happens out of combat as usual.
- Presence-keyed gear stays the hero's: those `requires` fail on
  companions, and loot steering already reads `requires`.
- Consumables: companions drink, hand over, administer and throw as any
  unit; they read scrolls in class by `classTags` (Herald kits read
  Herald-tagged scrolls, Herald design 7.17) and out of class only through
  Arcana (stat model 7.2). The items 10.8 "drink it" suggestion and the
  consumables 13.7 boss reserve apply to companions through the party
  scorer.
- Per-unit ownership, one run purse spent at camp, a Downed companion
  keeps everything **(settled, items 6, 15)**. Leadership prices read the
  hero only (items 10.5).
- **No kit trinket.** It would be loot outside the per-floor budget (main
  4.6). Trinkets 18.3's ≤ 25% empty slot fill is reported **by join
  camp**; its lever is `anyCompanion` loot weighting (CMP-30), keyed by
  party size so it fires at party ≥ 3 only (M2 lever; item rolls design
  4.3, IR-17).

### 8.5 HP, Downed and Scarred

**No new rule (settled; DH 11, conditions, main 4.2).** Downed is inert
and its reaction policies are off; a body is never lost at the stairs;
get-up after a win (a morale win counts), a short rest or camp; Medicine
once per companion per fight, Lifeline counting; the first downing per
floor sets Scarred (max HP −10% until camp or Treat). The Weary lever
stays off. UI: the card says "Can be Downed, never killed"; the party bar
shows Downed, Scarred and where each body lies; the DH 17.9 get-up
preview and 17.3 danger band cover four units.

### 8.6 Control, AI, policies and previews

- **The player controls every unit (decided, main 4.2).** Suggestions
  off by default, one key accepts one step **(agreed, ability 11.3)**.
- **Kit data carries:** `reactionPolicy` (Interpose and Flare
  `ifItChangesOutcome`; opportunity attacks `nonMinion`),
  `defaultRotation`, an ordered list of `{abilityId, condition (always |
  targetUnready | targetStaggered | allyAdjacentThreatened | allyBelowHalf
  | …, a closed enum), targetRule (nearest | lowestHp | threateningAlly |
  scorer, a closed enum)}` plus `potionRule {hpBelowPermille}` (the
  scorer's tie-breaks and the simulator's fallback; `RotationAbilitiesOnKit`
  checks that every `abilityId` belongs to the kit), and `castDefaults` (a
  slot spell's selective area pre-ticked when an ally is in it; Jostle and
  Barge pre-declared). Kits carry no `techniquePolicy`: Techniques are
  the hero's. **(lead's call)**
- **Pre-declared riders:** Jostle and Barge are toggles in the attack
  preview, set before the roll. A companion turn never stops for a
  mid-turn prompt (pacing), and "decide after the hit" stays the Vanguard
  hero's.
- **Scorer sub-choices** (Arcanist design 7.2: beam order and arc targets
  for companions) appear only as visible defaults the player can change,
  never as something taken without the player.
- **Ready glint:** a per-companion glint marks the scored best move
  without opening menus. Attack, move and area previews show friendly
  fire (ability 8.3).
- **Accept-rest-of-turn key (agreed status, Q2):** prototyped in M2
  behind a playtest flag (still pressed by the player each turn; the hero
  never included). Shipping it changes agreed ability 11.3 and is the
  client's call after seeing it (CMP-19). **The flag is off in every
  release build until the client says yes (new lead's call).** A full auto
  mode stays rejected (ability 5).
- **Arcanist Prime's solo clause** ("no companion in the fight") reads
  the actual roster per fight **(settled)**.
- **Pending request (change LP-33, loot sources design 4.7.8; M3):** add
  a `targetRule` value `windfall` to the closed enum, so the Windfall
  Truant is a legal target for suggestions (off by default) and for the
  simulator's companion policy, with the condition "reachable this turn
  without ending in the reach of more than 1 hostile". Never an
  autonomous act and no "Nab it" order (the player controls every unit,
  decided, main 4.2). Not a rule until the companions owner rules; if
  refused, no suggestion points at a Truant and the simulator's policy
  targets it only in its own arm (loot sources design 14.2). Reported:
  `CompanionChaseDowns`.

### 8.7 Readability and attachment

**(lead's call)**
- One silhouette and accent per kit; the class palette for the body.
- Role tags: one primary per kit (Protector, Tempo, Control, Skills,
  Striker, Area, Ward; 8.3); the secondary **Medic** badge (gets allies
  up; **no healing spells**, said on the badge) when the kit's fixed or
  chosen flex skill is Medicine.
- Party bar per unit: HP, pool pips with refill icons, signature pip,
  Downed or Scarred, supply count, an off-screen body arrow.
- **Deeds (M2, cosmetic):** the core logs a few memorable events per
  companion ("Interposed the blow that would have killed you, floor 3",
  "Got the hero up", "Most damage on the floor-4 boss"). The top deed
  shows on the party-bar tooltip and the run summary; companions are
  named on the death screen and run record ("Fell with: Brask the
  Gatekeeper").
- M3: a cosmetic **Veteran** record remembers kits that took part in a
  win; Old Friend can recall that name.
- Later: barks keyed to deeds; named personalities.

## 9. New mechanics considered

### 9.1 Table

**(lead's call unless marked)** Ids CX-n ("companion mechanic") are
local to this table; F1, F5 and F6 are the fun critique's proposals
(A.2).

| Id | Mechanic | Verdict | Wave |
|---|---|---|---|
| CX-1 | Muster (camp board, slot ladder) | **Adopted** (7.1, 7.2) | M2 |
| CX-2 | Gap card with printed waivers | **Adopted**, coverage strip instead of a single ribbon (7.3) | M2 |
| CX-3 | Carried hire (one step) | **Adopted**, +1 hire, no extra card (7.5) | M2 |
| CX-4 | Party Forecast line | **Adopted**, framed as sizing (7.11) | M2 |
| CX-5 | Recruiter's Eye (HX-10) | **Adopted** (9.2) | M2 (cut line → M3) |
| CX-6 | Camp swap and dismiss | **Adopted** with `kitBound` (7.6) | M2 |
| CX-7 | Retrain a kit choice (Rethink) | **Adopted** (8.2) | M2 |
| CX-8 | Pledges from captives, wanderers, shrines | **Adopted, Later** (7.12) | Later |
| CX-9a | Shortfall discount | **Adopted (agreed, Q1)** (7.7, 9.4) | M2 |
| CX-9b | Solo-deep fairness row | **Adopted** as fairness only (7.7) | M2 |
| CX-9c | Muster stipend, extra potion, extra rest | **Parked**: measured, about 0–2 points | — |
| CX-9d | Lone Wolf stat bonus | **Rejected** (dominant in the toy; told plainly with Q1) | — |
| CX-9e | Sustained-damage cap for under-sized parties | **Rejected** as a rule (Q1 C); kept as a diagnostic arm (11.3) | — |
| CX-10 | Lone Road and milestones | **Adopted** (content, never stats) | M3 |
| CX-11 | Old Friend boon, kit unlocks, locked-class previews | **Adopted** | M3 |
| CX-12 | Follow orders / accept-rest-of-turn key | **Prototype** behind a flag; ships on the client's yes **(agreed, Q2)** | M2 test |
| F1 | Join-card flex skill | **Adopted** (8.1) | M2 |
| F5 | Two-tier card; first-recruit beat | **Adopted** (7.11) | M2 |
| F6 | Deeds line; Veteran record | **Adopted** (8.7) | M2 / M3 |
| — | Next-camp preview | **Adopted** (7.2) | M2 |
| — | `kitBound` kit items | **Adopted** (7.6, 9.3) | M2 |
| — | Reserve bench | **Rejected** (roster management; potion cycling) | — |
| — | Companion bonds or Sworn pairing | **Later** (adds a read; brushes the forbidden grants) | Later |
| — | Second per-fight signature | **Rejected** (a second spend) | — |
| — | Gold, reward-card or XP recruit costs | **Rejected** (7.8) | — |
| — | Mid-floor joins with W + 8 repricing | **Rejected** (6.2) | — |
| — | Yielded-monster recruits | **Rejected** (7.12) | — |
| — | Dismiss-for-reward | **Rejected** (7.7) | — |
| — | Companion main-ability increases | **Rejected** at M2; lever off (8.1) | — |
| — | Ally-heal kit (Poultice) at M2 | **Rejected (agreed, Q3)**; written as a Later reserve (9.5) | Later reserve |

### 9.2 Adopted in full: Recruiter's Eye (HX-10)

**(lead's call; change CMP-14, CMP-15)**

> **Recruiter's Eye** (universal, camp; read from the hero only, at camp
> entry). If the hero is trained in Leadership, each Muster shows one
> extra free card. With Leadership mastery, the player may also re-roll
> one free card once per camp. It never adds a unit, a hire or a slot,
> and never changes party weight.

- **Mechanics (new lead's call).** The extra card is free slot 3, always
  pre-drawn at generation (7.2); it is shown when the hero is trained at
  camp entry and skipped when no legal kit fills it. The re-roll draws
  afresh on its own child `(runSeed, recruit, k, slot s, eyeReroll)`,
  with a fixed draw count, from the same legal pool as the slot (class on
  no other card, not in the party); it never touches card A and never
  shifts an arm's draws.
- A threshold, not a roll (stat model 5.4; the items 10.5 precedent). It
  is stat model 7.1's "better recruits" (CMP-14) and the Herald hero's
  secondary lean under classes 6.4's "no extra units" (CMP-15).
- The extra card is a free card; it never stacks with a carried hire
  (which adds no card), so it cannot make deferral a better-pick tool.
- Toy: a small effect (a Herald hero's all-four-classes share 58% → 62%
  *(illustr.)*). Report `LeadershipRecruitUplift` ≤ 10 points; the
  Herald design 5.4 arms run with it on and off.

### 9.3 Adopted in full: `kitBound` (closing the churn farms)

See 7.6. Simulator arm `SwapChurn` (swap every camp, sell everything)
must not raise coin above the reference by more than one potion's price
per run. Validators `KitItemsBound`, `KitPotionIssuedWithKit`.
**(lead's call)**

### 9.4 Adopted in full: the shortfall discount

See 7.7 **(agreed, Q1)**. Config keys `partyWeight.shortfall.cap3` and
`.cap2`, read against the computed `expectedCompanions(floor)`; the solo
composition table (enemies owner); `SoloBudgetMonotone`; boss HP from
the actual party (CMP-7).

### 9.5 Later reserve: Poultice (Hedgemage)

**(agreed, Q3: kept written, not shipped)** Written so a later decision
starts from a checked shape; it returns **only through the client**.

> **Poultice** (Hedgemage, Later reserve). Action; one ally within 6
> *(illustr.)* other than a Downed unit regains HP equal to **half its
> Recovery**. It spends one use of the Hedgemage's `rest` pool, with a
> sub-limit of **1 per rest**. **Never the signature.**

- If adopted it is a **new class-healing source** under classes 6.4
  ("one class at most") and a heal kit under DH 18.1; the healer gate and
  the 2+ row measure it, and the "heal-kit recruit" rows stop printing 0.
- It never lifts Downed (DH 11.5) and never scales with an ability
  (DH 12.2).

## 10. Numbers

All from the toy (10,000 runs per arm; 95% CI about ±0.9 points at
27%). **Toy, not simulator; direction and size only** *(illustr.
throughout)*.

### 10.1 Win rate by schedule (agreed weights)

| Schedule (companions joining) | Win % | Floor-1 deaths % | Death share F1/F2/F3/F4/F5 |
|---|---|---|---|
| Reference: camps 1, 2, 3 | 27.2 | 1.9 | 3/17/27/31/23 |
| Solo throughout | 0.3 | 1.9 | 2/31/46/18/3 |
| 1 companion, camp 1 | 5.3 | 1.9 | 2/13/40/34/11 |
| 2 companions, camps 1, 2 | 13.9 | 1.9 | 2/14/23/40/20 |
| 2 companions, camps 2, 3 | 7.8 | 1.9 | 2/34/33/20/11 |
| Defer then double (0/2/3/3) | 19.7 | 1.9 | 2/38/20/23/16 |

- **Floor-1 deaths are identical in every arm:** floor 1 stays solo.
- **`CompanionUpliftByClass`** (uplift_c = win‰ of `Reference` − win‰
  of `AlwaysDecline` for hero class c; the report is max over c of
  |uplift_c − mean uplift|, target ≤ 100‰ *(illustr.)*, 11.4): Vanguard
  +250, Shade +237, Arcanist +306, Herald +279; maximum deviation 38‰
  (passes 100).
  In the first toy version, before the class rows were balanced, it was
  19: read it only after class calibration.
- Every empty slot on floor 2 roughly doubles floor-2 deaths.

### 10.2 Declining and compensation

| Compensation (full decline) | Solo win % | Solo / ref |
|---|---|---|
| None | 0.3 | 0.01 |
| Solo round-1 row (CMP-8) | 0.3 | 0.01 (never binds) |
| +1 / +2 potions per declined camp | 1.2 / 2.3 | 0.04 / 0.08 |
| Extra short rest while party ≤ 2 | 0.9 | 0.03 |
| Lone Wolf (HP ×1.3, damage ×1.2 at ≤ 2) | 5.4 | 0.20 (and a buffed duo 32.6 beats the reference 27.2) |
| Boss HP from the expected party | 0.0 | 0.00 |

| Shortfall for 1/2/3 missing | 2 comp | Solo | DeclineOneSpread | Decline-two | Solo / ref | Defer then double |
|---|---|---|---|---|---|---|
| 0/0/0 (agreed weights) | 13.9 | 0.3 | 13.2 | 21.8 | 0.01 | 19.7 |
| **2/3/4 (chosen direction, agreed Q1)** | 21.4 | 16.2 | **5.7** | **9.5** | **0.60** | 26.6 |
| 2/4/5 | 21.4 | 26.5 | 5.7 | 5.1 | 0.98 (too generous) | 26.6 |
| 3/4/4 | 27.1 | 18.8 | 0.1 (third companion worthless) | 3.9 | 0.69 | 28.6 |

Sensitivity (kit damage ×0.9 / ×1.1; protector draw off) keeps the
direction. **Fix:** the shortfall discount as tuning keys (7.7), boss HP
from the actual party (CMP-7), and the 2/3/4 values as a direction only.

### 10.3 Encounter scaling per unit

Floor-4 lair, agreed weights: party HP-equivalents lost / hero death per
fight / enemy units: n = 1 58% / 17.3% / 2.4; n = 4 27% / 0.1% / 4.7.
Four units against one is strongly super-linear (hero death per fight
falls about 170×): target dilution. Raising the per-companion weight
toward 20 never brings n = 4 to solo pressure, because the 6-unit cap
binds from about +14. **Fix:** CMP-9 (enemies 20.3 read against an
expected curve).

### 10.4 Pacing

| Party size | Minutes per floor, F2–5 (default) | Companion 10 s | Levers (companion 6 s, enemy 1.5 s) |
|---|---|---|---|
| 1 | 17.8 | 17.8 | 16.6 |
| 2 | 21.0 | 21.9 | 18.7 |
| 3 | 24.3 | 26.0 | 20.9 |
| 4 | 27.3 (F4 28.5, F5 28.8) | 29.9 | 23.0 (F5 24.2) |

Reference run about 117 minutes. Each companion adds about 3.2 minutes
per floor at 8 s per turn. Toy rounds per floor are 27–30, above the
research estimate of 21. **Fix:** 7.4's gate, lever order and cap
fallback **(agreed, Q2)**.

### 10.5 Companion relevance at floor 5

No ability increases: companions together deal 64% of floor-5 damage
(each 18–28%; hero 36%). +1 main at levels 4 and 8 adds about 16 points
of win rate, which would force an enemy retune. Win rate moves about 0.75
points per 1% of kit damage. **Fix:** no increases; calibrate kit sheets
before quoting any schedule number; `HeroShareReport` (11.4) guards the
hero against being outshone.

### 10.6 Candidate generation (20,000 runs per hero class)

- Coverage of all four needs after camp 3: 99–100% with the gap card,
  83–88% without. **Keep the gap card.**
- Heal kit seen by camp 3: 60–78% (had one existed).
- **Convergence failed** under the toy's BestFit policy (now
  `Reference`): two two-need kits took 25–31% of final-party slots each;
  three kits under 5%; hero-class duplicates 84–85% for Vanguard and
  Shade heroes; the ×½ weight barely mattered. The old gap card drove
  the duplicates (a Gatekeeper for a Vanguard hero's protector need; a
  Scout for a Shade hero's Mechanics and Arcana needs). **Fix:** the
  flex skill (8.1), a combat-value term in the policy, an Oracle arm for
  kit value (11.3), card A's hero-class tier (7.3: a Shieldbearer for a
  Vanguard hero's protector need, a Tinker for a Shade hero's Mechanics
  need; the Arcana need for a Shade hero stays forced and is printed),
  `HeroClassDuplicateShare` (11.4), more kits at M4. **The flex-skill
  and card-A re-run of `gen.py` is owed before M2 sign-off.**

### 10.7 Healer readings: not evidence

The companions toy's healer section (no-healer arm 22.2%, healer gap
13–20 at the reference, 2+ − 1 = +11) **disagrees with the Herald design
5.4 toy** (no-healer 25.0–27.5%, gap about 5–6), and both are
uncalibrated. **This section is not evidence for DH 18.1 or 18.2 (told
plainly).** Before any heal decision is revisited, the two toys' heal
models (heal card size, rest counts, potion economy) are reconciled; the
gate reads only the M2 simulator, pinned to the reference schedule and
the written recruit policy (CMP-18, CMP-29). The M2 default of no heal
kit **(agreed, Q3)** rests on classes 6.4 and simplicity, not on these
numbers.

## 11. Data, validation and simulator

### 11.1 Records and configuration

**(lead's call; field names are the builders' within these shapes)**

- **`CompanionKit`** {id, classId, primaryRoleTag, fixedSkills,
  flexSkills[2], array, gear, fallbackDamageTypes[], kitPotions 1,
  atWills[2], reaction{id, policy}, pools, signatureCampAbility,
  passives, slotSpells[]? (spell or family-template ids), choices{3, 6, 9:
  [A, B], default}, sliceOf, effectKinds, stanceFixed?, hasElementFamily,
  companionMainAbility?, protector, healAlly (false at M2),
  defaultRotation, potionRule, castDefaults, offerWeightPermille,
  combatValuePermille (simulator-calibrated, read by `MusterBestFit`),
  unlock?, silhouetteId, nameTableId}.
- **Class record additions:** `heroOnlyFeatureIds[]` (8.3); features
  shared with kits carry `sharedWithKits` (Interpose, Shieldwall, Hold the
  Line, Opening, Mote, Gust, Flare, Spurred).
- **Boss record addition:** `hpByPartySize[1..4]` (7.9; CMP-7).
- **Config `recruitSchedule`** {slotOpensAtCamp [1, 2, 3], companionCap
  3, cardsBase 3, cardsMax 4, carriedHireMax 1, dismissalsPerCamp 1,
  sameClassAsHeroWeightPermille 500, candidateListLength 8,
  candidateDrawMax 32, gapOrder [medicine, protector, mechanics, arcana]}
  *(illustr. where a number)*. `expectedCompanions(floor)` is computed,
  not stored (7.7). Under the cap-2 fallback: companionCap 2,
  slotOpensAtCamp [1, 2].
- **Keys `partyWeight.shortfall.cap3`** [0, 2, 3, 4] and
  **`partyWeight.shortfall.cap2`** [0, 2, 3] *(illustr.; agreed shape,
  Q1)*; **key `partyWeight.soloWeightMin`** 10 *(illustr.)*.
- **Key `companion.mainIncreases`** `[]`; **key
  `companion.extraAttackAsDie`** false (CMP-28).
- **Item instance field `boundToUnitId`** (null unless kit-issued;
  `kitBound` is derived from it; the `unsellable` treatment applies to
  the instance; part of the stack test) (7.6; CMP-21).
- **Run state** `companionSlots[3]`, `carriedHire`, `pledges[]` of
  `PledgeState` (reserved, Later, 7.12), the per-camp Muster state
  (`MusterCard[]`, 7.2; hires used; `dismissalUsed`; the snapshot per
  hire for the same-visit undo), `rethinkUsed` per companion per camp,
  `deeds[]` per companion.
- **Telemetry events** `FloorEntered`, `BossDefeated`, camp-screen and
  pause enter and exit, timestamped (7.4).
- **Playtest flag** `ui.acceptRestOfTurn` (off; never on in a release
  build without the client's yes).

### 11.2 Content validation

**(lead's call)** Each fails the build with the record and field named.

| Validator | Checks |
|---|---|
| `NoFloor1Companion` | No source places a unit before camp 1 |
| `CompanionCapWithinWeights` | `companionCap` ≤ 3 (the agreed weights stop at four members) |
| `SlotScheduleMatchesCap` | `slotOpensAtCamp` is exactly the camps 1 to `companionCap` (length equals the cap); the computed `expectedCompanions(floor)` equals min(floor − 1, cap) and never falls; each shortfall key has length `companionCap + 1` for its cap |
| `KitShape` | 2 at-wills, ≤ 3 limited (slot pool counts once), 1 reaction, ≥ 1 `rest` and ≥ 1 `camp` pool |
| `KitReadCount` | ≤ 4 things to read (design target 3) |
| `OneSignaturePerKit` | Exists (ability 11.1) |
| `ForbiddenClassGrant` | Exists, run on kits (classes 6.3), with one exemption: Spurred on Herald-class kits (CL-5) |
| `NoHealOnHeraldKit` | Exists (Herald design 7.17) |
| `NoAllyHealKitAtM2` | No M2 kit carries an ally `healRecovery` |
| `ProtectorKitsOnTwoClasses` | Protector kits exist on ≥ 2 classes |
| `RecruitPoolSatisfiable` | Runs the actual construction (free cards, then the gap procedure, 7.3) over every hero class × skill set × reachable party × "without X" arm × Eye on and off × both caps; asserts a legal board, card A for the first unmet unoffered need whenever a corpus kit outside the party covers it, and waivers only for lack of a legal kit, printed |
| `GapCardNeverHeal` | The gap procedure never targets healing |
| `GapArcanaNonCaster` | Card A for the Arcana need is a non-caster kit (7.3) |
| `BoardShrinkLegal` (test) | A board shrinks only when a slot has no legal kit; the base board never falls below `cardsBase` while legal kits exist (7.2) |
| `NoDuplicateKitInParty` | No board offers a kit already in the party |
| `SliceNotCopy` | A kit shares no `effectKinds` row or name with its class's `heroOnlyFeatureIds`; features marked `sharedWithKits` are exempt (8.3) |
| `KitsDistinctWithinClass` | Two kits of one class share at most one at-will and never the signature's `effectKinds` |
| `KitRoleTagUnique` | Each kit has exactly one `primaryRoleTag` from the list (8.3); no two M2 kits of one class share it |
| `KitHasFallbackDamage` | Every kit has a non-empty `fallbackDamageTypes` reachable without its slot spells (classes 6.5) |
| `KitAcEnvelope` | Every kit defence feature (Interpose, Flare, Close Ranks as a step-0 fill) is inside armour 11 item 1's charged envelope at levels 1–10 (CMP-34) |
| `RotationAbilitiesOnKit` | Every `defaultRotation` `abilityId` belongs to the kit |
| `FixedStanceOnVanguardKit` | Vanguard kits carry `stanceFixed` |
| `KitChoicesDefaulted` | Every 3/6/9 choice has a default |
| `KitChoicesNumericOnly` | Every option is a numeric, range, size, duration or target-count step; no new mode, effect or action |
| `KitSkillsOnClassList` | Every fixed and flex skill is on the class list; counts match the class |
| `NoPresenceOnKit` | No Intimidation, Leadership or Presence-keyed ability |
| `CompanionArrayLegal` | +3 / +2 / +1 / +1 / 0 / −1, main +3 |
| `CompanionAcFloor` | Kit AC ≥ the class-derived lowest legal AC at L1, 5, 10 |
| `KitWeaponTrained` | Kit weapons inside the class's training |
| `KitCorpusCoverage` | The coverage list of 8.3 holds |
| `RecruitDrawFixedCount` | Each candidate-list entry uses a fixed number of draws |
| `PreviewMatchesBoard` | Free cards 1 to `cardsBase − 1` shown in the preview equal the board's, and the Eye's card equals it when shown |
| `KitItemsBound`, `KitPotionIssuedWithKit` | Every kit-issued instance has `boundToUnitId` set to its unit; the kit potion is the ordinary healing potion record, issued with the kit |
| `SoloBudgetMonotone` | For floors 2–5 × band × every weight from 15 − the live shortfall key's largest entry to 15, the deterministic solo table's fill and ERD are non-decreasing in weight (7.7); fails if a key leaves the `soloWeightMin` bound |

### 11.3 Simulator arms (change CMP-18)

**Cap is a run-config axis of every arm,** not an arm: every arm and
report runs at cap 3 and cap 2 **(agreed, Q2)**. All arms of a
comparison use the same seeds.

- **`Reference`** (the written `MusterBestFit`), complete **(new lead's
  call)**:
  - always hires while a slot is open (the carried hire too); never
    declines, dismisses or swaps; never uses Rethink;
  - card order: covers the most unmet needs, then a class new to the
    party, then `combatValuePermille`, then data order;
  - the flex skill is set to the first unmet need it covers, else option
    1; 3/6/9 choices take the kit default;
  - with Leadership mastery, re-rolls the free card that ranks lowest by
    the same order.
- **`Oracle`** **(new lead's call)**: at each Muster it enumerates every
  legal action (hire each card × each flex option, each swap × each flex
  option, Travel on). For each action it runs R rollouts *(illustr.:
  200)* that continue under `Reference`, on seeds `hash(runSeed, camp,
  rolloutIndex)` from a simulator-only stream, with common random numbers
  across actions; it never draws on the run's own streams. It takes the
  action with the highest win permille, ties by data order.
- **`DiversityFirst`**.
- **`AlwaysDecline`**; **`DeclineAt{k}`** (decline camp k, never use the
  carried hire, hire at the other camps under `Reference`);
  **`DeclineTwo{j,k}`** (likewise for two camps); `DeferThenDouble` ×
  {Reference, Oracle, Eye}.
- **Forced-kit arms** `ForceKit{kit}` (that kit hired at camp 1 whenever
  legal, `Reference` otherwise), for `KitPickValue`.
- `SwapChurn`; `Without<Class>` (including the Shade-recruit arm owed to
  Shade design 13.3), each with Recruiter's Eye on and off (Herald design
  5.4); `SoloPerClass`;
- `SustainedRowNoShortfall`, **kept as a diagnostic (new lead's call):**
  Q1 C was rejected as a rule, but the arm is cheap and is the measured
  fallback if the shortfall keys cannot meet `SoloBudgetMonotone`;
- `HealSeeking` is **not run** while no heal kit ships **(agreed, Q3)**.

### 11.4 Reports owed (M2)

- **`FloorTimeByPartySize`** (the first M2 gate, **agreed** Q2; defined
  in 7.4), with its 75th percentile and the suggestions-on sessions
  beside it; companion-turn time by kit; suggestion accept rate.
- `SoloRunReport`, `DeclineOneSpread`, `DeclineTwoSpread`,
  `SoloClassSpread` (7.7 targets CT-1 to CT-6).
- `CompanionUpliftByClass`: uplift_c = win‰(`Reference`, hero class c) −
  win‰(`AlwaysDecline`, hero class c); report max over c of |uplift_c −
  mean uplift| ≤ 100‰ *(illustr.)*.
- **`HeroClassDuplicateShare`** **(new lead's call)**: the share of final
  parties holding a kit of the hero's class, by hero class; target ≤ 50%
  *(illustr.)*; read in the owed `gen.py` re-run.
- Per-kit **passive and signature fire rate per fight** beside
  companion-turn time by kit, so a dead passive shows up.
- `HeroShareReport`: hero damage share ≥ the highest single companion's;
  hero kill share ≥ 30% *(illustr.)*; hero-retreat rate.
- `KitPickValue`: win‰(`ForceKit{kit}`) − win‰(`Reference`) on the same
  seeds, beside Oracle pick shares and human playtest picks, never damage
  share; `SlateDiversity`.
- `LeadershipRecruitUplift`; `ShadeBypassByPartySize`.
- Trinket slot fill by join camp (CMP-30); DH 21.5 downings by party
  size; healer counts by party size, small parties reported separately.
- Enemies 20.3 HP-equivalents against the expected curve and hero death
  per fight by party size (CMP-9).
- Loot sources reports and arms (change LP-32, loot sources design 9.3,
  9.4; read; M2, with M3 Windfall rows): `LootClicksPerFloor`,
  `WindfallSeconds` and `WindfallRoundsDelta` inside the 7c clock line
  (7.4), `CompanionChaseDowns`, and the party-size-by-floor arm under the
  7.1 ladder.

### 11.5 Test additions

**(lead's call)**
- A run that declines camp 1 gets 2 hires and 3 cards at camp 2; a
  second decline does not stack.
- A dismissal at a full party creates no carried hire.
- The same seed shows the same board on camp re-entry and across a
  save and resume; a mid-floor hero skill gain changes only card A.
- Free cards Brawler + Drummer with the protector need open: card A is a
  Gatekeeper or Shieldbearer, and the shared class is printed.
- A Vanguard hero's protector need draws the Shieldbearer whenever no
  Herald card is on the board.
- In a "without X" arm every non-X card is identical to the base arm,
  with Recruiter's Eye on and off; Eye-on and Eye-off runs give the same
  non-Eye cards.
- A hero at level 2 at camp 1 is raised to 3 before the Muster, and the
  recruit's join card shows the L3 choice; a catch-up that grants
  Medicine moves card A to the next need.
- A dismissed companion's bound dose leaves the hero's belt, and the
  Travel-on re-check first moves another companion's dose (a newcomer's
  kit potion included), else issues the camp-issue potion; selling or
  handing over potions at camp never triggers an issue.
- Two companions' kit potions never merge into one stack.
- `kitBound` items sell for 0; looted gear on a dismissed companion stays
  with the party (or the dismissal is greyed `PackFull`).
- A second dismissal in one visit is refused; the same-visit undo of a
  hire is not a dismissal and returns the newcomer's other items to the
  hero or pack.
- Cap 2: a party that declined camp 2 gets a hire board at camp 3 (2
  hires with the carried hire, up to 2 companions); a full party gets a
  swap board.
- A swap made while a slot is open shows the swap forecast lines.
- Shortfall: floor 3 solo generates at weight 12, floor 1 solo at 15,
  the reference party at 15 + 8c on every floor; the boss HP is the
  record's `hpByPartySize` entry for the actual party and the escort uses
  the shortfall-adjusted weight.
- Rethink never offers a stance, skill or flex re-pick.
- A recruit hired at level 6 shows its L3 and L6 choices defaulted on the
  join card.
- Recruiter's Eye adds a card and never a hire; the re-roll never
  touches card A; a hero who gains Leadership mid-floor sees the Eye's
  pre-drawn card at the next camp.

## 12. Staging

**(lead's call; the M2-first row agreed, Q2)**

| Wave | Contents |
|---|---|
| **M1** | Nothing. M1 is solo (main 9) |
| **M2, first** | Vertical-slice timing: the telemetry events, `FloorTimeByPartySize` (the gate, defined in 7.4), the blind party-of-3 vs party-of-4 playtest, the accept-rest-of-turn prototype (flagged). The slice holds floors 4–5 with their templates and one boss each, the eight kits at levels 7–10 and grouped playback (7.4). The cap is fixed from these before balance work freezes |
| **M2** | Musters at camps 1–3 (7); the slot ladder; 3 cards (+1 Eye); gap card and coverage strip; carried hire; decline, dismiss and swap with `kitBound`; next-camp preview; Party Forecast; two-tier card; first-recruit beat; the 8 kits with flex skill, fixed arrays, level equal to the hero's, one choice grammar, Rethink; the `recruit` stream; the shortfall discount with the solo composition table and `SoloBudgetMonotone` (**agreed**, Q1); boss HP from the actual party; guard rows at every reachable size; the recruit policy and arms; all validators and reports; Deeds; Recruiter's Eye (cut line → M3) |
| **M2 prerequisites owed by other owners, before records** | CMP-5 to CMP-9, CMP-17 and the solo composition table (enemies owner); CMP-12 (consumables owner); CMP-21 (items owner); CMP-22, CMP-23 and CMP-31 (DH owner); CMP-26 (Vanguard owner); CMP-27 (naming pass); CMP-30 (trinkets owner); CMP-32 (conditions owner); CMP-33 (Shade owner); CMP-34 (armour owner) |
| **M2 cut fallbacks** | Recruiter's Eye → M3; Rethink → M3; Deeds → M3. Never cut: the gap card, `kitBound`, the shortfall discount, boss HP from the actual party, the floor-time gate |
| **M3** | Lone Road; milestones; kit unlocks (main 4.7); the Old Friend starting boon (a named unlocked kit among camp 1's cards, replacing a free draw, never the gap card, declinable; it is the run's one starting boon, competing with skill-granting boons, main 4.7); Wayfarer's Pack (+1 belt slot within the hard max 3) as a milestone boon option; locked-class non-caster previews (classes 7.8); the Veteran record |
| **M4** | A third kit per class, including a second Arcana non-caster; any heal kit the client approves |
| **Later** | Pledges (captives, wanderers, shrines; Meet at camp; Send home); rival-party survivors; bonds; barks and personalities; mortal companions (the DH Later rung); Beastbound's bonded companion (classes 9.1); the Muster stipend (parked); Poultice (only through the client) |

## 13. Binding requirements and how they are met

| Requirement (source) | Where met | Status |
|---|---|---|
| Camps give "a chance to recruit" (main 4.1, **decided**) | 7.1; CMP-1 | Met (re-read as a Muster) |
| Up to 2–3 companions; fixed class; automatic levelling, few choices; the player controls all (main 4.2, **decided**) | 7.4, 8.1–8.2, 8.6 | Met (cap 3, **agreed** Q2; fallback 2) |
| Companions never killed; Downed, get-ups, Scarred (main 4.2; DH 11) | 8.5 | Met (unchanged) |
| Floor 1 solo; no surprise while the hero is the only unit (classes 5.2; DH 17.1) | 7.10 | Met by construction |
| Party weight 15 + 8, actual, fixed at generation (enemies 10.1, **agreed**) | 7.9 | Met; shortfall term **agreed** Q1 (CMP-5) |
| Kit shape (ability 11.1) | 8.3 | Met; the slot pool counts once (classes 7.8) |
| 3/6/9 choices; joins at camp with full pools (ability 11.2) | 8.2 | Met; CMP-16 adds the level lock and Rethink |
| Suggestions off; nothing taken without the player (ability 11.3, **agreed**) | 8.6 | Met; the turn key only on the client's yes (**agreed** Q2) |
| Under 10 s per companion turn; ≤ 4 reads (ability 11.4; classes 11.F) | 8.3 (3 reads), 7.4 | Met in shape; told plainly it is not enough for pacing |
| Scripted recruit policy (ability 14.6) | 11.3; CMP-18 | Met |
| Kit counts for M2 (ability 20) | 8.3 | Met (eight) |
| Launch class ids; slice; no subclass; caster single pool; same-class different slice, weighted down (classes 7.8) | 8.3, 7.2 | Met |
| Forbidden grants (classes 6.3, **agreed**) | 8.3 (movement ruling), 7.7 | Met |
| Recruit-pool rules per floor (classes 7.8) | 7.3; CMP-11 | Met per board plus party |
| Protector kits on ≥ 2 classes (classes 7.8; DH 27) | Gatekeeper, Shieldbearer | Met |
| Arcana non-caster (consumables 13.8 DEP-1, binding) | 7.3 (non-caster card A where Arcana is the first need left), Scout, Outrider flex; CMP-12 | Met; on other boards through the party reading, told plainly |
| Medicine spread; 1 potion per recruit kit (DH 27; DH 5.5) | 8.3, 7.6 | Met |
| ≤ 1 ally-heal kit, never the signature (classes 7.8) | Zero at M2 (**agreed** Q3) | Met |
| Reliance "without" arms; slot-by-slot re-roll on a dedicated substream (classes 6.6; Herald design 5.4) | 7.2 (`armReroll`) | Met |
| Herald recruits: Wisdom, no Presence spells, no Frighten, no heal; Drummer, Shieldbearer (Herald design 7.17) | 8.3; CMP-25 | Met |
| Recruiter's Eye (Herald design 7.17, 10.1, 19; HX-10) | 9.2; CMP-15 | Met |
| No single-type kit; a fallback damage type (classes 6.4, 6.5) | 8.3 | Met (`KitHasFallbackDamage`) |
| The charged AC envelope (armour 11 item 1) | 8.3 Close Ranks; CMP-34 | Met (`KitAcEnvelope`) |
| Vanguard: stance fixed by kit; no Momentum or Arts (Vanguard design 7.5, 18) | 8.3 | Met |
| Shade: no Vanish Arts or From Nowhere; recruit reliance arm (Shade design 18, 13.3) | 8.3, 11.3 | Met |
| Arcanist: no Overchannel or Arts; Prime's solo clause; beam order (Arcanist design 19, 7.2) | 8.3, 8.6 | Met |
| Presence and Leadership the hero's (stat model 5.4; items 10.5) | 8.1, 9.2 | Met (`NoPresenceOnKit`) |
| Fixed skills (stat model 7.2, **agreed**) | 8.1; CMP-13 | Met on the told-plainly reading |
| Better recruits (stat model 7.1) | 9.2; CMP-14 | Met |
| HP formula, companion bonus 11 (DH 7.1, **agreed**) | 8.1 | Met |
| Healer gate ≤ 10; no-healer arm ≥ a quarter (DH 18.1, 18.2, **agreed**) | 10.7; CMP-23 | Unchanged; rows print 0 |
| Class-blind healing guarantee (DH 5.6) | 7.6 | Met; recruits never deliver the guarantee |
| Kit AC in AR-20 (armour 11 item 2) | 8.1 `CompanionAcFloor` | Met |
| Ability increases for companions (armour 13.2, 22) | 8.1 | Met (none) |
| Weapon families per kit; D-c (weapons 26, 11.3) | 8.3; CMP-28 | Met |
| Trinkets the build lever; ≤ 25% empty (trinkets 11 rule 9, 18.3) | 8.4; CMP-30 | Reported by join camp |
| Medicine and Lore spread input (trinkets 23) | 8.3 | Met; trinkets owner told |
| Recruit arrives with its kit record (items 15) | 7.6 | Met; kit items `kitBound` |
| Companions in unlocks; one starting boon (main 4.7) | 12 (M3) | Met |
| Captives as recruits (enemies 14.6, 21, 25) | 7.12; CMP-10 | Later, defined |
| Deterministic streams (main 6.3) | 7.2; CMP-3 | Met |
| A run is 1–2 h; floors 15–20 min (main 2, 4.1, **decided**) | 7.4 | **At risk;** gate and fallback (**agreed** Q2) |

## 14. Risks

| # | Risk | Signal | Response |
|---|---|---|---|
| 1 | **Pacing at party 4** (the largest) | `FloorTimeByPartySize` > 20 min on floors 4–5 | Lever order (7.4); cap 2, pre-authorised (**agreed** Q2) |
| 2 | Shortfall values sit on a cliff | `SoloBudgetMonotone` fails; solo lumpy | Solo composition table; tuning keys, not constants; escalation (7.7) |
| 3 | The shortfall overshoots or undershoots | `SoloRunReport` outside 40–70%; CT-5 fails | One step at a time inside CT-1, CT-5 and the `soloWeightMin` bound; back to the client if it cannot fit (7.7) |
| 4 | Deferral becomes an exploit | `DeferThenDouble × Oracle` > reference | The carried hire already gives no card; cut the carried hire if needed |
| 5 | Kit convergence; hero-class duplicates | `KitPickValue` (Oracle) or human picks < 15% / > 60% *(illustr.)*; `HeroClassDuplicateShare` > 50% | Flex skill and card A's hero-class tier (owed re-run); combat-value term; next lever `sameClassAsHeroWeightPermille` 500 → 250; M4 kits |
| 6 | Hero outshone | `HeroShareReport` fails; hero-retreat rate up | Kit damage factors, never hero buffs |
| 7 | Solo favours the Vanguard | `SoloClassSpread` > 10 | Class owners' companion-reading features; floor 1 untouched |
| 8 | Shade bypass taxed by recruits | `ShadeBypassByPartySize` collapses after camp 1 | Stealth kits; Shade owner; the worst-member rule unchanged |
| 9 | Healer gate misread | The two toys disagree | Reconcile; read the M2 simulator only (10.7) |
| 10 | Late recruits miss trinket fill | Fill by join camp > 25% empty | `anyCompanion` loot weighting (CMP-30) |
| 11 | Churn farms | `SwapChurn` above one potion's price | `kitBound` (in place) |
| 12 | Camp 1 overload | Camp-screen time; tester notes | Two-tier card; defaulted L3; Rethink |
| 13 | Thin attachment | Playtest notes; DH risk 8 | Deeds, names on the run record, Veteran (M3) |
| 14 | A predictable 1 → 2 → 3 → 4 rhythm | Playtest notes | Next-camp preview; Pledges (Later); M4 kits |
| 15 | Kit power sensitivity (about 0.75 points per 1% damage) | Companion hit-rate report | Calibrate kits before quoting schedule numbers |
| 16 | Dependencies on other owners | CMP-5–CMP-9, CMP-12 or CMP-31–CMP-34 refused | Escalate; without CMP-7 a declined party meets a boss sized for 4; without the solo composition table the shortfall cannot ship; without CMP-32–CMP-34 the affected kit text is reworked, never shipped unapproved |
| 17 | **All evidence is toy** | — | Every number is owed a simulator rerun before it settles |
| 18 | **Recall-based survey** | — | Mechanisms only; no names or text taken |

## 15. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: board size inside 3–4, candidate-list length,
  the ×½ same-class weight, the shortfall keys (inside 7.7's escalation),
  kit arrays inside `CompanionArrayLegal`, kit dice, pool sizes and areas
  inside the kit shape, Close Ranks' numbers, the per-kit 3/6/9 numbers
  (8.2), `candidateDrawMax`, `soloWeightMin`, the Oracle's rollout count,
  the gate's session count and idle threshold, report thresholds other
  than the agreed ones, and lever sizes.
- Muster, card, forecast, preview, party-bar, glint and Deeds layout;
  name tables, trait lines and silhouettes.
- Wording of kit, card and forecast texts within the rules here, until
  the naming pass.
- **Not tuning:** camps 1–3 as the slot schedule; at most one hire per
  camp plus one non-stacking carried hire; one dismissal or swap per
  camp; camps only; floor 1 always solo; the gap procedure's order and
  tiers; the cap of 3 with its measured fallback to 2 (**agreed**, Q2);
  the gap order and "never healing"; no pity timer; free recruiting;
  `kitBound`; level equal to the hero's; no ability increases; one flex
  skill on the join card; numeric-only choices; the shortfall discount's
  shape, floor 1 and the reference party untouched (**agreed**, Q1); boss
  HP from the actual party; no Lone Wolf bonus; recognition never stats;
  no ally-heal kit at M2 (**agreed**, Q3); suggestions off by default;
  the accept-rest-of-turn key off until the client says yes.

## 16. Changes to approved documents

Applied on 2026-11-20, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and the rows owed
by other owners (17; listed below) are recorded as such. No row applies
only when an unlock ships: the M3 unlock content (Lone Road, milestones,
Old Friend, kit unlocks) needs no row here (12). The conditional rows
are recorded now and apply only when their condition is met: CMP-19
only if the client says yes after the M2 prototype; CMP-10 records a
Later idea and changes no wave; CMP-20 is withdrawn. Every row names the
exact section it touches. Rows that touch **decided** or **agreed** text
are flagged in bold; **the client's approval of 2026-11-20 covers
them** (with Q1–Q3 where a row names one); the told-plainly rows were
told with Q1 or with the approval request (18.1) and confirmed by the
client on 2026-11-20; the rest were confirmed with the document on
2026-11-20.

**Narrowings recorded inside this design (no approved text changes):**
- no kit grants Dash or any extra movement (classes 6.3, "+speed"),
  except Spurred on Herald-class kits (CL-5);
- `NoHealOnHeraldKit` stands, and no other kit heals at M2;
- the hero never gets suggestions; a full auto mode stays rejected;
- `recruitKitPotions` stays 1; the camp top-up rule is unchanged except
  the narrowed extra check point (CMP-22);
- the Weary lever stays off.

| # | Document / section (current status) | Change | Approved by |
|---|---|---|---|
| **CMP-1** | Main 4.1, bullet "Between floors the party reaches a camp: full rest, a shop or reward, and a chance to recruit." (**decided**) | "a chance to recruit" → "a **Muster**: a recruit offer at every camp while the party has an open slot, a swap offer otherwise; hiring is optional" | **Client, with this document (decided text)** |
| **CMP-2** | Main 4.2, bullet "Up to 2–3 companions are recruited during the run…" (**decided**) | "Up to 3 companions (a party of 4). One slot opens at each of camps 1–3; at most one hire per camp plus one carried hire; any offer may be declined; one dismissal or swap per camp, at camp only. If the M2 floor-time gate fails after the levers, the cap is 2: no new slot opens at camp 3, and a party below the cap there still gets a hire board." | **Client (agreed 2026-11-20, Q2)** |
| **CMP-3** | Main 6.3, bullet "Separate random streams for dungeon generation, combat rolls and loot" (**decided**) | Add a `recruit` stream with per-camp, per-slot and gap substreams and `armReroll`, `eyeReroll`, `cosmetic` and reserved `pledge` children | **Client, with this document (decided text extended)** |
| **CMP-4** | Enemies 10.1, "Party weight" paragraph, "so recruiting a fourth companion is a net gain" (**agreed** paragraph) | → "a fourth party member" (a wording fix) | Lead; **touches an agreed paragraph, client with this document** |
| **CMP-5** | Enemies 10.1, "Party weight" paragraph (**agreed**); enemies 4, client answer 2 ("Party weight grows sublinearly: hero 15 + 8 per companion, using the actual party, fixed at floor generation") (**agreed**) | 10.1, add: "On floors 2–5, less `partyWeight.shortfall[expected − actual]` (config per cap, *illustr.* [0, 2, 3, 4] at cap 3), where expected is `expectedCompanions(floor)`, computed from the companions design's slot schedule for the floor being generated; shipped with the deterministic solo composition table and `SoloBudgetMonotone`". Enemies 4, answer 2, add: "less the shortfall term on floors 2–5 (10.1; companions design, Q1)" | **Client (agreed 2026-11-20, Q1)**; enemies owner applies |
| **CMP-6** | Enemies 6.3, sentence under the floor table: "'Expected size' is a calibration assumption only; budgets use the actual party (10.1)." | Add: "The shortfall term in 10.1 reads `expectedCompanions(floor)`, computed from the slot schedule and the cap, not this column." | **Client (agreed 2026-11-20, Q1)** |
| CMP-7 | Enemies 17.5, boss HP bullet "HP ≈ 6 × the party's typical damage per round × 0.85"; enemies 17.4 Boss Court | Add "of the **actual** party at floor generation, stored per boss as `hpByPartySize[1..4]` (authored to this formula, simulator-calibrated) and read by the actual size; the escort budget uses the shortfall-adjusted weight × 200% × depth% × ladder%; HP ignores the shortfall"; a content-test row per boss per party size | Lead (mandatory); enemies owner |
| CMP-8 | Enemies 6.4 guards; DH 21.1 guard rows; enemies 10.5 generator content test and 17.4 template content test; armour 11 item 2 (AR-20) runs | Evaluate at every reachable party size and weight (down to the shortfall minimum); the floor-1 solo round-1 row also applies to solo parties on floors 2–5 | Lead; release-gate inputs, **told plainly** |
| CMP-9 | Enemies 20.3, Combat row "(flat across sizes validates the party weights)" | Read HP-equivalents against an expected curve; add hero death per fight by party size | Lead; enemies owner |
| CMP-10 | Enemies 14.6, "Captives as recruits are a Later idea for the companions design"; enemies 21 Later list; enemies 25 | "Captives as next-camp Pledges, never mid-floor joins (Later)"; Meet at camp and Send home / Parting Word | Lead (wave unchanged) |
| CMP-11 | Classes 7.8, "Recruit-pool rules per floor" (lead's call) | "Each floor's pool" = each Muster board together with the current party; a gap card fills the first unmet need; needs already met are waived and printed; the Arcana need's gap card is a non-caster; "at most one ally-heal kit per board" (zero at M2) | Lead; client told |
| **CMP-12** | Consumables 13.8 DEP-1, bullet 2 (**binding dependency**) | Read as met by a board card (a non-caster, 7.3) **or** any party member trained in Arcana (the party can read any scroll) | **Consumables owner; client told (told plainly)** |
| **CMP-13** | Stat model 7.2, "Companions come with fixed skills." (**agreed**) | "…fixed skills, plus one flex skill chosen on the join card from two options on its class list, fixed from then on." | **Client, with this document (agreed text; told plainly)** |
| CMP-14 | Stat model 7.1, Leadership row "Better recruits, shop prices and event outcomes" | "Better recruits" defined as Recruiter's Eye (companions 9.2) | Lead |
| CMP-15 | Herald design 10.1, HX-10 row "Handed to the companions design (optional)"; Herald design 7.17, last bullet ("Recruiter's Eye (HX-10) is handed to the companions design as optional"); Herald design 19, companions bullet ("Recruiter's Eye (optional)") | → "Adopted (companions design 9.2)", with the 9.2 text, in all three places | Lead |
| CMP-16 | Ability 11.2 | Add: "A companion's level always equals the hero's; a recruit joins at the hero's level with missed choices defaulted on the join card; one 2-option 3/6/9 grammar (numeric steps only); Rethink: one choice re-picked per companion per camp, never a stance; no ability increases (fixed array; `companion.mainIncreases` off)." | Lead (lead's-call rows) |
| CMP-17 | Enemies 16, "Camp catch-up" bullet ("at camp the hero is raised…") | Add "and companions with it, before the camp restore and the Muster (companions design 7.11)" | Lead; enemies owner applies |
| CMP-18 | Ability 14.6, M2 bullet ("recruits … are scripted policies") | Define `MusterBestFit` as written in full in 11.3 (with `combatValuePermille`) and the arms there; the DH 18.1, classes 6.6 and Herald design 5.4 readings pin to the reference schedule and this policy | Lead |
| **CMP-19** | Ability 11.3, "one key accepts it" and "Nothing is ever taken without the player" (**agreed**) | An optional accept-rest-of-turn key (player-pressed each turn; never the hero), shipped only if the client says yes after the M2 prototype | **Client (agreed 2026-11-20, Q2, for the prototype; shipping is the client's later call)** |
| CMP-20 | — | Withdrawn (main-ability increases rejected; kept as an off lever under CMP-16) | — |
| CMP-21 | Items 10.6 (Sell), 15 ("A recruit arrives with its kit record"), 12.3 (flags), 12.8 (stack test), 18.1 (`ItemInstance` and its transition table), 19.2 (invariants); DH 13.2 ("A 'healing potion' here is …") | `ItemInstance` gains `boundToUnitId` (null unless kit-issued; `kitBound` derived); a bound instance gets the `unsellable` treatment (sells and scraps for 0); 12.8: stacks merge only when `boundToUnitId` is also identical; 18.1 rows "kit issue → 6.2 placement, on hire, camp, `ItemCreated{kit}`" and "party place → deleted, on dismissal or same-visit undo, camp, `ItemDestroyed{dismissed}`"; 19.2 unchanged in form (the table stays exhaustive); DH 13.2: a kit-issued ordinary healing potion counts as a healing potion | Lead; items owner; DH owner for the 13.2 sentence |
| **CMP-22** | DH 13.2, "Checked on arrival at camp, over the whole party" (**agreed**, G5) | Add: "Run again on leaving camp only if a dismissal there deleted a healing potion the hero held (a kit-bound dose); it then moves a companion's dose first and issues only if no unit holds one. Selling, handing over or drinking potions at camp never triggers it." | **DH owner; client told (told plainly)** |
| **CMP-23** | DH 18.1, "Reports beside the gate" heal-source list; classes 6.6, "Healing" bullet (CL-9 rows); Herald design 5.4 healer-count rows (**agreed** rows) | "Heal-kit recruit" prints 0 and "healer count 2+" prints N/A while no heal kit ships; the gate text is unchanged; at M2 its healer arm is exactly a Herald hero holding a heal card | **Client (agreed 2026-11-20, Q3); told plainly to the DH and Herald owners** |
| CMP-24 | Classes 8.2 and DH 12.1 (`restDefensiveTool`) | "Binds class records, not companion kits." | Lead |
| CMP-25 | Herald design 7.17, Shieldbearer "Choices" cell ("As the Drummer's pattern") | Write the rows in 8.3 | Lead |
| CMP-26 | Vanguard design 7.14, defaults table, Level 2 row: "Pivot in M2 when two or more companions are recruited at the level-up" | Delete the clause (unreachable: level 2 always falls on solo floor 1); Drive Through stays the default | Vanguard owner |
| CMP-27 | Armour 12.3 table, row "Recruit soldier (M2; a separate L0 type…)" | → "Levy soldier" | Naming pass |
| CMP-28 | Weapons 11.3, beside the D-c fallback bullet | Add the companion lever `companion.extraAttackAsDie` (off) | Lead |
| **CMP-29** | Classes 6.6 and 5.6; DH 18.1 (**agreed**, Q10); DH 18.2 (**agreed**, Q11) | Add the 11.4 reports; the DH 18.1 and 18.2 pass or fail verdicts read the M2 simulator only, never a toy; DH 18.2's "checked against playtests" **stands** as a check beside the verdict, and a disagreement goes to the client; `FloorTimeByPartySize` as the first M2 gate | Lead; **touches agreed text: client with this document (told plainly); the gate: client (agreed 2026-11-20, Q2)** |
| CMP-30 | Trinkets 18.3, "Companion slot fill at the boss" row | Report split by join camp; lever `anyCompanion` loot weighting; the metric unchanged | Lead; trinkets owner |
| CMP-31 | DH 13.1, the camp order (order lead's call) | Insert "camp catch-up for the hero and companions, with their choices (enemies 16)" between step 1 (get up) and step 2 (restore), and "the Muster (companions design 7.11), revisitable until leaving camp" between step 5 (top-up) and step 6 (vendor) | Lead; DH owner |
| CMP-32 | Conditions, Staggered "Sources"; Prone "Sources" | Staggered: add "companion kit signatures (Staggering Shot; M2)". Prone: add "kit riders (Barge), tagged `trip`" | Lead; conditions owner |
| CMP-33 | Shade design 6.5, the Opening trigger registry | Add the kit-only rows: Gap Sight (open tier, launcher or thrown, Unready or Staggered, once per round; Scout kit only); Knife Work (two Beset melee attacks, Flank Sense's once-per-round limit lifted for that action; Outrider kit only). Neither is a hero trigger; checked against Lone Blade and Caught | Lead; Shade owner |
| CMP-34 | Armour 11 item 1, "Inputs" ("The maxima include every class defence feature…") | Add "and every companion kit defence feature (Interpose, Flare, Close Ranks as a step-0 window fill, 10.4)" | Lead; armour owner |

**Rows owed by other owners before records** (the same list as 12 and
17): CMP-5 to CMP-9, CMP-17 and the solo composition table (enemies
owner); CMP-12 (consumables owner); CMP-21 (items owner); CMP-22, CMP-23
and CMP-31 (DH owner); CMP-26 (Vanguard owner); CMP-27 (naming pass);
CMP-30 (trinkets owner); CMP-32 (conditions owner); CMP-33 (Shade
owner); CMP-34 (armour owner).

**Not changed (checked):** party weight 15 + 8 and its "actual, fixed at
generation" (**agreed**; CMP-5 adds a term, nothing else); DH 18.1's gate
definition and DH 18.2 (**agreed**); `recruitKitPotions` 1 (**agreed**);
ability 11.4's 10 s playtest target; main 4.1's shop-or-reward choice and
items I-13 (**agreed**); DH 17.1 (**agreed**); classes 6.3's forbidden
grants (**agreed**); `hp.companionBonus` 11; the Weary lever (off).

**Check after applying (lead's call):** a grep for "a chance to recruit"
in main 4.1 finds only history; a grep for "fourth companion" in enemies
10.1 finds none; a grep for "Pivot in M2 when two or more companions" in
the Vanguard design finds none; a grep for "Recruit soldier" in the
armour design finds none; a grep for "Handed to the companions design"
in the Herald design, **case-insensitive and across the whole document**,
finds none, and a case-insensitive grep for "Recruiter's Eye (optional)"
and "handed to the companions design as optional" finds none; a grep for
"fixed skills." in stat model 7.2 finds the CMP-13 wording.

## 17. Depends on systems not yet designed

The owed rows are the single list in 16 (and 12); by owner:

- **The enemies owner:** CMP-5 and CMP-6 (the shortfall term, **agreed**
  Q1); the **deterministic solo composition table** for floors 2–5 at
  weights from `soloWeightMin` to 15 and `SoloBudgetMonotone`; CMP-7
  (boss HP from the actual party as `hpByPartySize`, mandatory); CMP-8
  (guards and content tests at every reachable size); CMP-9; CMP-17;
  fewer or shorter fights on floors 4–5 as lever 3 (7.4).
- **The consumables owner:** CMP-12 (DEP-1's party reading).
- **The items owner:** CMP-21 (`boundToUnitId`, the stack test, the two
  transition rows).
- **The DH owner:** CMP-21's 13.2 sentence; CMP-22 (the narrowed top-up
  re-check); CMP-23 (rows print 0); CMP-31 (the camp order).
- **The conditions owner:** CMP-32 (Staggered and Prone sources).
- **The Shade owner:** CMP-33 (Gap Sight and Knife Work in the registry).
- **The armour owner:** CMP-34 (kit defence features in the envelope
  inputs).
- **The ability owner:** CMP-16, CMP-18; CMP-19 only if the client says
  yes.
- **The Vanguard owner:** CMP-26. **The trinkets owner:** CMP-30 and the
  Medicine and Lore spread input (8.3).
- **The camp and hub design:** the Muster screen inside the camp order of
  7.11; the camp reward card (items 10.4) is unchanged.
- **The simulator:** every arm in 11.3 and report in 11.4 at both caps;
  the flex-skill `gen.py` re-run before M2 sign-off; reconciling the two
  toys' heal models (10.7).
- **Playtests:** the floor-time gate and the blind party-of-3 vs
  party-of-4 arm (**agreed** Q2); the accept-rest-of-turn prototype;
  companion-turn time by kit; suggestion accept rate; human kit picks.
- **Dungeon generation (Later):** captive rooms and shrines for Pledges
  (captives: encounter placement design 9.1, EP-39).
- **The profile (M3):** Lone Road, milestones, Old Friend, Veteran, kit
  unlocks.
- **Naming pass:** every placeholder name here; CMP-27.

## 18. Open items for the client

None. Approved by the client on 2026-11-20, as a whole, including every
lead's call, the told-plainly items in 18.1 and the new lead's calls
listed there. The three questions were answered **(agreed,
2026-11-20)**. The future items in 18.2, which come back only if a
report or a later design triggers them, and the naming pass remain
future items.

### 18.1 This document

The three questions this design raised were answered on 2026-11-20
**(agreed)**, each with the recommended option:

- **Q1, declining:** option A, the shortfall discount. On floors 2–5,
  `partyWeight = 15 + 8c − shortfall[expected − actual]`, expected =
  `recruitSchedule.expectedCompanions[floor]` (computed, 7.7), shortfall
  [0, 2, 3, 4] *(illustr.)*, read against `SoloRunReport`, shipped only
  with the solo composition rule and `SoloBudgetMonotone`; floor 1 and
  the reference
  party unchanged; boss HP reads the actual party. Told plainly and
  accepted with it: no Lone Wolf bonus; recruiting stays the normal,
  better path; a full decline is a real challenge at roughly half the
  reference win rate, never parity; Lone Road and the milestones (M3)
  unlock content, never stats; the numbers are a direction until the M2
  simulator sets them. Rejected: B (keep the agreed weights) and C (a
  sustained-damage cap).
- **Q2, the party cap:** option A. Cap 3 companions (a party of 4),
  decided by measurement: `FloorTimeByPartySize` is the first M2 gate
  (median floor time at party 4 on floors 4–5 ≤ 20 minutes) plus a blind
  party-of-3 vs party-of-4 playtest before balance freezes; the lever
  order of 7.4; if floors 4–5 still run long after the levers, the cap
  becomes 2 (camp 3 swap-only, read exactly in 7.1; expected 0 / 1 / 2 /
  2 / 2); every new report runs at both caps from the start; the
  accept-rest-of-turn key is prototyped in M2 behind a playtest flag,
  and the client decides whether it ships after seeing it. Rejected: B
  (cap 2 now) and C (cap 3, no gate).
- **Q3, ally heals:** option A. No recruit kit heals allies at M2; the
  heal-kit recruit rows print 0; healing stays the Herald's lean (classes
  6.4, one class at most); the Hedgemage's Poultice (half Recovery, rest
  sub-limit 1, never the signature) is kept written as a Later reserve.
  Rejected: B (Poultice at M2).

**Told plainly with the approval request; confirmed by the client on
2026-11-20:**
- companion skills: "fixed" now means fixed once chosen on the join card
  (CMP-13, touching agreed stat model 7.2);
- the 10 s per companion turn target alone cannot fit a party of 4 into
  20 minutes; the internal design target is 5–6 s; ability 11.4 is
  unchanged;
- while no heal kit ships, the DH 18.1 healer arm is exactly a Herald
  hero holding a heal card, so it largely overlaps the Herald
  heal-isolation gate; the gate's text is unchanged (CMP-23);
- the guard rows and the AR-20 and content-test runs are re-run at every
  reachable party size, and the solo round-1 row applies on solo floors
  2–5 (CMP-8);
- the next-camp preview shows the free cards exactly, card A as a
  placeholder and the Eye's card as "+1 with Leadership";
- the DH 13.2 top-up runs again on Travel on only when a dismissal there
  deleted a potion the hero held; selling or handing potions around never
  triggers it (CMP-22, touching agreed G5);
- DEP-1 is read as "the party can read any scroll" (CMP-12); a
  non-caster Arcana card is guaranteed on the board only where Arcana is
  the first need left, and otherwise DEP-1 rests on that party reading
  (7.3);
- "camp 3 swap-only" under the cap-2 fallback is read exactly: no new
  slot opens at camp 3, and a party below the cap there still hires
  (7.1; CMP-2);
- DH 18.1 and 18.2 verdicts read the M2 simulator only; DH 18.2's
  "checked against playtests" stands beside them (CMP-29);
- the stranded slot: a player who declines camps 1 and 2 can hire at
  most 2 at camp 3, and with 5 floors the third slot is always reachable
  through camp 4 (7.1; change RT-1, run structure design 6.1);
- the companions toy's healer figures are not evidence for any gate
  (10.7);
- CMP-1, CMP-3 and CMP-4 touch decided or agreed text (main 4.1, main
  6.3, enemies 10.1) as wording or extensions; the client's approval
  on 2026-11-20 covers them.

**New lead's calls in this document** (beyond the proposal the client
said stands; listed so the approval covers them; confirmed by the
client with the document on 2026-11-20):
- **the Arcana need's gap card is a non-caster** (the Scout, or the
  Outrider through its flex), so DEP-1's "recruitable non-caster" is met
  literally on every board where Arcana is the first need left; the
  Hedgemage's Arcana counts only once in the party; validator
  `GapArcanaNonCaster` (7.3);
- **the gap procedure** (7.3): card A resolved at camp entry on its own
  substream from the first unmet need that no free card already offers
  and that has a legal kit, in preference tiers (a class on no free card
  and not the hero's, then any class on no free card, then any, printed);
  skipped needs waived and printed; `RecruitPoolSatisfiable` runs this
  exact procedure;
- **generation** (7.2): every free slot (the Eye's included) pre-drawn
  and resolved at floor generation; exhaustion continues on the same
  substream, then data order; a board shrinks only for lack of legal
  kits; the element family drawn with the entry; `MusterCard`;
- **one reliance-arm mechanism** (7.2): the base board, then only X's
  cards replaced on `armReroll`; the Eye's re-roll on its own
  `eyeReroll` child;
- **the camp order** (7.11; CMP-31): catch-up before the restore, and
  the snapshot and card A after the top-up and before the Muster;
- **how the floor-time gate is read** (7.4): on human playtest sessions
  with the shipped defaults and a forced party size, from `FloorEntered`
  to `BossDefeated`, active time only, at least 12 sessions per arm
  *(illustr.)*; the 75th percentile and the suggestions-on sessions
  reported beside it, never inside it;
- **the drop to cap 2 is pre-authorised** by the agreed Q2 answer: the
  lead applies it and reports it, without asking again (7.4);
- **lever 5 waits on the client:** if the client declines the
  accept-rest-of-turn key, lever 5 is skipped and the gate is read after
  lever 4 (7.4);
- **the blind playtest is an input, not a trigger:** a clear preference
  for party 3 *(illustr.: ≥ 60%)* while the gate passes goes to the
  client as a future item, never drops the cap by itself (7.4; 18.2);
- **the playtest flag is off in every release build** until the client
  says yes (8.6; 11.1);
- **the shortfall reading** (7.7): `expectedCompanions(floor)` is
  computed from `slotOpensAtCamp` and the cap, never stored; `floor` is
  the floor being generated; the index is clamped at 0 and at the
  table's last entry; the cap is one switch (7.1);
- **the shortfall leaves experience and the loot budget unchanged**
  (7.7); only encounter budgets move;
- **the shortfall escalation** (7.7): one key step at a time with a
  rerun, bounded by CT-1, CT-5 and `soloWeightMin`; if CT-3 or
  `SoloBudgetMonotone` cannot be met inside those bounds, back to the
  client; never a stat bonus, never
  floor 1 or the reference weights;
- **a carried hire is earned only by an open slot left unfilled on
  arrival;** a dismissal never creates one (7.5);
- **a camp's hires may be spent on joins or the one swap,** so a carried
  hire allows a swap plus a hire (7.6);
- **one dismissal per camp** (`dismissalsPerCamp` 1), a swap counting as
  it; the same-visit undo restores a snapshot and never brings back a
  swapped-out companion; the swap forecast (7.6);
- **a declared Lone Road run uses the same shortfall weights** and no
  extra bonus (7.5);
- **`kitBound` is an instance field, `boundToUnitId`,** with the
  approved `unsellable` treatment for its sell rule and its own stack
  test; the kit potion is the ordinary record, so DH 13.2 counts it
  (7.6; CMP-21);
- **the shortfall keys are per cap** with a `soloWeightMin` bound, and
  the solo composition table is deterministic (7.7);
- **boss HP as `hpByPartySize`** by the actual size, the escort on the
  shortfall-adjusted weight (7.9; CMP-7);
- **the simulator policies in full** (11.3): `Reference`, `Oracle`,
  `DeclineAt{k}`, `DeclineTwo{j,k}`, `ForceKit{kit}`; cap as an axis of
  every arm; targets renamed CT-1 to CT-6 in cap terms;
  `HeroClassDuplicateShare` and the passive fire-rate report (11.4);
- **kit changes after checking** (8.3): Close Ranks as a step-0 window
  fill (CMP-34); Gap Sight on Unready or Staggered and Knife Work as two
  Beset attacks (CMP-33); Staggering Shot and Barge as condition sources
  (CMP-32); the Arcanist kits' named spells, the Hedgemage's Help
  at-will, and a staff or dagger fallback; one primary role tag per kit
  with Medic as a secondary badge; the per-kit 3/6/9 rows (8.2);
  Spurred exempted from the movement ruling on Herald kits (settled,
  CL-5);
- **`PledgeState` and its reserved substream key** (7.12);
- **Old Friend and Pledge cards and the distinct-class rule** (7.2): Old
  Friend counts for it; a Pledge sits outside the board and is exempt,
  but `NoDuplicateKitInParty` applies;
- **`SustainedRowNoShortfall` kept as a diagnostic arm** although Q1 C
  was rejected, and `HealSeeking` not run while no heal kit ships (11.3);
- **Poultice's written shape** (9.5): an action, one ally within 6
  *(illustr.)*, half Recovery, spending the Hedgemage's `rest` pool with a
  sub-limit of 1 per rest, never Downed, never the signature;
- **the M2 cut fallbacks** (12): Recruiter's Eye, Rethink and Deeds may
  slip to M3; the gap card, `kitBound`, the shortfall discount, boss HP
  from the actual party and the floor-time gate never slip;
- **the citation corrections** (3.4): Vanguard design 7.14 and 18, Shade
  design 18, armour 22, trinkets 18.3 and 23, enemies 17.5;
- **the validators added beyond the proposal:** `GapArcanaNonCaster`;
  `SlotScheduleMatchesCap`; `BoardShrinkLegal`; `KitsDistinctWithinClass`;
  `KitRoleTagUnique`; `KitHasFallbackDamage`; `KitAcEnvelope`;
  `RotationAbilitiesOnKit`; `SoloBudgetMonotone` defined exactly;
- **the test additions** (11.5).

### 18.2 Future items triggered by reports

These come back to the client only if a report or a later design
triggers them:
- the floor-time gate failing after every lever (the cap drops to 2,
  pre-authorised; the client is told);
- a clear blind-playtest preference for party 3 while the gate passes;
- the floor-time 75th percentile above 23 minutes *(illustr.)* while the
  median passes;
- whether the accept-rest-of-turn key ships, after the M2 prototype
  (Q2);
- `SoloRunReport` outside 40–70% of the reference, or `SoloBudgetMonotone`
  failing, after the shortfall escalation runs out (Q1);
- `DeclineOneSpread` above 10 points or any smaller party beating the
  reference after the levers;
- the healer gate or the no-healer arm missing on the M2 simulator (DH
  18.1, 18.2; no heal kit is added without the client, Q3);
- any heal kit, Poultice included (Q3; M4 or Later);
- promoting captives (Pledges) to M4;
- promoting any 7.7 target to a gate;
- an owner declining CMP-5–CMP-9 or CMP-12 in a way the design cannot
  absorb;
- the naming pass.

## Appendix A: how the critique findings were resolved

### A.1 Judges

All three judges chose Model A with grafts; every graft is resolved in
6.3. Judge-split items: the carried slot (unlimited vs one step) → one
step, +1 hire and no card; Recruiter's Eye's wave (M2 optional vs M3) →
M2 with an M3 cut line; Rethink (M2 vs "M2 or M3") → M2 with an M3 cut
line; the heal kit → Q3, no heal kit (**agreed**); the "about 5–6 s" turn
target → an internal design target, told plainly.

### A.2 Fun critique

| # | Finding | Resolution |
|---|---|---|
| 1 | Recruit choice becomes "take the utility kit"; the gap ribbon names the "correct" card | Flex skill on the join card (8.1, CMP-13); coverage strip on every card, no single ribbon (7.3); `KitPickValue` on human picks and Oracle value (11.4); gen re-run owed |
| 2 | Declining is a trap; the fix must read as sizing | Q1 A (**agreed**); the forecast framed as sizing (7.11); CMP-5 with CMP-7; no stipend; Lone Road a milestone |
| 3 | Four-unit control is a chore; pacing fails | Turn key prototyped in M2 behind a flag (**agreed** Q2); blind cap-2 arm; pre-declared riders, 3 reads, ready glint; grouped playback as an M2 requirement (7.4) |
| 4 | Camp 1 overloaded | Two-tier card; first-recruit beat; Muster before vendor and shop or reward (7.11) |
| 5 | Companions hard to care about | Deeds; names on the death screen and run record; M3 Veteran (8.7) |
| 6 | Shade identity taxed | Stealth on the Outrider (fixed) and Scout (flex); `ShadeBypassByPartySize` (8.3, 11.4) |
| 7 | Carry-forward disagreement | One-step carried hire (7.5) |
| 8 | Healer expectations | The Medic badge says "no healing spells" (8.7); Q3 (**agreed**) |
| 9 | Owner flags | CMP-26, CMP-4, CMP-27 |
| F1, F5, F6 | Proposals | Adopted (9.1: the join-card flex skill; the two-tier card and first-recruit beat; Deeds and Veteran). The critique's other proposals are resolved in rows 1–8 above (the turn-key prototype and the blind arm under Q2) |

### A.3 Balance critique

| # | Finding | Resolution |
|---|---|---|
| S1-1 | The shortfall turns a calibration column into an input; values on a cliff; cap-2 re-keying | Keyed to the computed `expectedCompanions(floor)` (CMP-5, CMP-6); one key per cap; `SoloBudgetMonotone` over the deterministic solo table from `soloWeightMin` to 15; the sustained-damage arm kept as a diagnostic (11.3) |
| S1-2 | Pacing fails at the recommended cap; balance work would be thrown away | Cap decided first in M2; all reports at both caps; told plainly about the 10 s target (7.4, 12, 18.1) |
| S2-1 | Swap churn sells kit bases | `kitBound` (CMP-21); `SwapChurn` arm |
| S2-2 | Defer + bank + Eye is a better-pick tool | The carried hire adds no card; CT-4 under the Oracle arm; `DeferThenDouble × {Reference, Oracle, Eye}` |
| S2-3 | Two toys' healer numbers disagree | The companions toy's healer section marked not evidence; reconcile; M2 simulator only (10.7, CMP-29) |
| S2-4 | Zero heal kits empty or confound the healer rows | Rows print 0 / N/A; gate meaning told plainly; gate text unchanged (CMP-23) |
| S2-5 | A pre-rolled board goes stale against mid-floor skill gains | Free cards fixed at generation; card A resolved at camp entry by the gap procedure; the preview shows the free cards exactly and placeholders for card A and the Eye's card; `PreviewMatchesBoard` (7.2, 7.3) |
| S2-6 | Solo viability per class unmeasured | `SoloPerClass` arm; `SoloClassSpread`; milestones class-blind (7.7) |
| S3-1 | Companions may outshine the hero | `HeroShareReport`, hero-retreat rate; the lever is kit damage (11.4) |
| S3-2 | Rethink as a future multiplier | `KitChoicesNumericOnly` (8.2) |
| S3-3 | Pick share measured under BestFit | Oracle arm; `ForceKit` arms; human playtest picks (11.4) |
| S3-4 | Potion-once-per-slot underspecified | Superseded by `kitBound`; the potion is issued with the kit and leaves with it |
| S3-5 | The toy's reference is correct by construction | Numbers presented as direction; Q1 says "about half" (front matter, 10) |

### A.4 Consistency critique

| # | Finding | Resolution |
|---|---|---|
| 1 | "CO-n" already used by consumables; five schemes | One prefix, CMP-n, single table (16) |
| 2 | The shortfall contradicts enemies 6.3's "calibration only" sentence; touches more gates | CMP-6 on that sentence; CMP-8 re-runs 6.4, AR-20 and 10.2; CMP-9 for 20.3 |
| 3 | A board pre-rolled at generation cannot compute the gap card | Free cards at generation, card A by the gap procedure at camp entry (7.2, 7.3) |
| 4 | Arcanist kits exceed three limited abilities | Classes 7.8 already counts the slot pool once; kits carry Flare's pool plus the slot pool (8.3); ability 11.1 unchanged |
| 5 | Kit and ability name collisions | All renamed (front matter); `SliceNotCopy` checks names too; Duck removed; riders renamed with their own semantics |
| 6 | "Slice of the Arcanist's Weft mend" is false (Knit is self-only) | No heal kit at M2 (**agreed** Q3); any heal kit is a new class-healing source (9.5) |
| 7 | The healer gate already fails in the companions toy | Not evidence; reconcile; escalation deferred to the M2 simulator (10.7) |
| 8 | "Never reappears" vs kit count; distinct classes vs 5 cards; the bank strands the third slot | Unhired kits reappear; max 4 cards, shrinking when legal kits run out; card A's tier rule; the stranded slot stated on the forecast (7.1, 7.2, 7.3) |
| 9 | Unflagged changes to agreed text (turn time, camp catch-up, pacing gate, Drill, turn key) | Told plainly (18.1); CMP-17; the gate via Q2 (**agreed**); CMP-16; CMP-19 via Q2 |
| 10 | Potion-once-per-slot changes a client number's application | Replaced by `kitBound`; `recruitKitPotions` untouched |
| 11 | DEP-1 reading disputed | Any party member with Arcana, and a non-caster gap card (7.3); consumables owner flagged (CMP-12) |
| 12 | Cover Me, Dash and Sweep vs forbidden grants and a rejected item | Movement ruling; Close Ranks, Sidestep, Heave (8.3) |
| 13 | Passed requirements half-answered | `potionRule` and boss reserve (8.4, 8.6); no Square Up (8.3); Shade recruit arm (11.3); weapon training (8.3); trinkets owner told (8.3) |
| 14 | Waves inconsistent | One table (12); Pledges stay Later |
| 15 | Citations | Vanguard design 7.14 (CMP-26); armour 12.3 (CMP-27); enemies 20.3 wording confirmed (CMP-9); the corrections in 3.4 |
| 16 | Warden skills differ; Scout / Lockbreaker duplicate names | A single kit table (8.3) |
| 17 | Old Friend and the one starting boon | Stated as the run's one boon (12) |

## Appendix B: sources

Confirmed by search during research: the OSE SRD (Retainers); UA
Sidekicks (2018); D&D Beyond on Tasha's sidekicks; Gloomhaven solo play
and scenario level (rules.dized.com); DOS2 Lone Wolf (Fextralife); the PC
Gamer Darkest Dungeon 2 guide; Siliconera on the DD2 Bounty Hunter; the
Wildfrost wiki (companions); the Mystery Dungeon wiki (Shiren allies);
BG3 solo play and the Lone Wolf mod (Sportskeeda, PC Gamer); Wildfrost
design (MCV, TechRadar).

**Recall, not confirmed:** PF1 / 3.5 Leadership history, PF2e encounter
adjustments and Command an Animal, 13th Age animal companions,
Torchbearer hirelings, FTL crew details, Slay the Spire's rarity offset,
Darkest Dungeon 1 stagecoach details, Pathfinder: Wrath mercenaries, DCSS
allies, Pokemon Mystery Dungeon recruiting.

Toy model and outputs (scratch, not project files):
`/tmp/claude-0/-home-user-DungeonTTRPG/ffb71713-6cd0-57b7-8daf-5787f0bc3857/scratchpad/comp_toy.py`,
`.../scratchpad/exp_out2.md`, `.../scratchpad/sweep_out.md`,
`.../scratchpad/sweep2_out.md`, `.../scratchpad/gen_out.md`,
`.../scratchpad/sens.py` (full prefix as on the first line).
