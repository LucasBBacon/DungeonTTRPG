# Classes, Stage 2: The Herald: Design

Date: 2026-11-20
Status: Approved, 2026-11-20
Parents: `2026-11-17-classes-concepts-design.md` (classes stage 1,
approved; the direct parent), `2026-11-18-vanguard-class-design.md`,
`2026-11-18-shade-class-design.md` and
`2026-11-20-arcanist-class-design.md` (the approved stage-2 templates;
"Vanguard design", "Shade design" and "Arcanist design" below; the
Arcanist is the caster template), and through classes stage 1
`2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model),
`2026-11-16-ability-resource-model-design.md` (ability model),
`2026-11-16-conditions-design.md` (conditions design),
`2026-11-16-enemies-design.md` (enemies design),
`2026-11-16-items-inventory-design.md` (items design),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design),
`2026-11-17-consumables-design.md` (consumables design) and
`2026-11-17-death-healing-design.md` (death and healing design, "DH")

Items marked **(agreed)** were decided by the client on 2026-11-20 for
the three questions this design raised (Q1–Q3, sections 4 and 20), by
choosing the lead's recommendation in each case. Before answering Q1 the
client was told plainly, and accepted with it: that **defaults never
heal and the Dirge is the default lens**; that **the gates read the
non-healing Herald** (`Floor1ClassSpread` is read on the no-heal
defaults, and the heal-isolation gate measures heal cards against the
Herald without them); that **the
Hearth's own records heal nothing** (`HearthNoHeal`); and that **the DH
18.1 healer gate itself is unchanged**. Before answering Q3 the client
was told plainly that the floor-1 boss content rule is class-blind but
**motivated by the Herald**. The client also said that everything else
in the lead's proposal stands as written. The client approved this
document as a whole on 2026-11-20.

Items marked **(lead's call)** were filled in by the design lead; the
client confirmed them all on 2026-11-20, when it approved this document
as a whole, and they are binding. Items marked
**(settled)** restate a rule from an approved document; **(decided)**
marks a main-design client decision. Items marked **(change HR-n)** amend
an approved document and are listed, with the exact section they touch,
in section 18. Items marked **(told plainly)** narrow or re-read agreed
or settled text and were told to the client in plain words, with Q1 or
Q3 above or with the approval request (20.1); the client confirmed them
all on 2026-11-20. Every item here is binding on the builders.

This is **stage 2 of the classes design for the Herald**, the launch
Presence full caster for morale and support: its full table for levels
1–10, its signature verb (Cadence) and how it grows, its slot pools and
core kit, its full spell list and `classTags` audit, its two M2
subclasses (Dirge, Hearth), sketches of its two unlock aspects
(Warcaller, Pennant), the capstone Scatter, the numbers, the staging and
how every binding requirement on "the classes design" is met for this
class. The Herald is an **M2 class** and has no M1 slice. Classes stage
1 section 14 lists what stage 2 owes; section 13 below answers it for
the Herald.

The client's brief: "Great, the herald sounds like a great follow up
class. What we have to ensure with a heal-lenient class is that it's not
an absolute requirement to have one on every run, since we don't want
players to entirely rely on it, its use, like every other class, is to
aid in a mechanical gameplay niche while also being fun. I don't have
any further considerations for this class, so please go ahead with the
usual research and considerations." The lead's answer, which the client
accepted (Q1, **agreed**), is in section 5: **agree, and make it
measurable from both sides.** The Herald must never be required, and a
Herald who never heals must still be worth bringing.

Numbers in this document are illustrative unless a rule says otherwise,
and are marked *(illustr.)* where they first appear. They come from
scratch arithmetic and a crude solo floor-1 Monte Carlo proxy, not
simulator evidence; only the gaps between proxy rows mean anything. The
builders own them and the simulator calibrates them (section 17),
except where a value is marked as a client decision, a settled value or
a lead's number.

**Names updated by the naming pass** (naming pass design,
`2026-11-24-naming-pass-design.md`, approved 2026-11-24; NP rows
listed: **NP-38**, with its 13.1 cites under agreed Q2 and Q5). Player-facing
names below now read: Spur → **Urge**, Earned Spur → **Earned Urge**,
Interject → **Speak Over**, Undertone → **Ache**, Lead by Example →
**Show the Way**, Draw the Sting → **Ease the Pain**, Opening Cry →
**Clarion**, Homecoming → **Gather In**, Final Word → **Pronouncement**,
Raise the Pennant → **Hoist the Pennant**, Lay Down Arms → **Yield
Arms**, Calm Minds → **Quiet Minds**, Rekindle → **Take Comfort**,
Jarring Whispers → **Grating Murmur**, Totem → **Tipstaff** (agreed Q2),
the Totem kit → **Focus hand** and the shield kit → **Shield hand**,
Slot Recovery → **Slot Renewal** (agreed Q5), Rooted → **Tangled**
(agreed Q5), Surge → **Onslaught** (the Vanguard pool; agreed Q5), the
HP tier Standard → **sound**, the complexity Medium → **Involved**,
previews print **Demoralised** (never "broken"), and the cited names
Blight → Wilt, Second Wind → Patch Up, Catch Breath → Respite, Sculpt →
Whittle, Shockwave → Upheaval, Hold the Line → Bar the Way, Guiding
Radiance → Marking Light; the examples use Brask, Turnkey and Inmates.
Code ids (`spur`, `spurOnRout`, `openingCry`, `drawSting`,
`homecoming`, `rekindleAvailable`, `slotRecovery+1` and the like) are
unchanged. The grep-checked list and the avoided-names table below, and
Appendix A, are history and keep the names as checked on 2026-11-20.
The request to define or delete "Bring Them Home" (NP-38) is with this
document's owner and changes nothing here until ruled.

**Names are placeholders** pending the naming pass (a separate client
item). They are frozen for this document so that documents agree. Every
name new to this document was grep-checked as a whole word against the
15 approved documents and has no hits: Faltering, Undertone, Plain
Speech, Spur (the option; see the note below), Rouse, Ringing Blow,
Bewilder, Recoil, Show of Force, Dire Tidings, Doomsay, Give Ground, Lay
Down Arms, Fallen Idol, Bitter Truth, Pall, Final Word, Lament,
Overwhelm, Antiphon, Toll, Mourners, Passing Bell, Last Toll, Knell,
Terms of Surrender, Refrain, Parley, Hearthkeeper, Hearthsong,
Homecoming, Firelight, Soothe, Watchfire, Draw the Sting, Reach the
Fallen, Strike Now, War Cry, Lead by Example, Raise the Pennant, Colours
Forward, Carry the Colours, Shieldbearer, Quickstep, Drumroll, Guard
Step, Form Up, Offer Terms, Interject, Earned Spur.

Names avoided because they collide **(lead's call)**:

| Avoided name | Why |
|---|---|
| **Banner** (the brief's fourth subclass) | Already rejected in classes 10: it collides with the morale display banner and the group banner (enemies 14.8, 14.9). Stays **Pennant** |
| Dread (keyword, "Dread Pall", "Dread Chorus", "Long Dread") | An enemy aura; a banned head word (classes 10). The keyword is **Faltering** |
| Two Voices | Shares a head word with Harrowing Word's "two words" at level 5. Renamed **Antiphon** |
| Grim Refrain, Grim Tidings | "Grim" is the head word of the Vanguard's Grim Resolve |
| Steadying Spur, Steady Measure | "Steady" is Rally's option and a morale profile. Renamed **Soothe** |
| Tend, Tending | "Tend" is an items camp service, kept apart from Medicine's Treat |
| Order (a Warcaller option) | Command's option (enemies 14.4) |
| Graze | The greatsword's weapon verb. Renamed **Undertone** (renamed Ache, naming pass NP-38) |
| Falter (a spell) | Sits next to the keyword Faltering. Cut |
| Kindle, Field Dressing | Rejected mechanics (5.3). The names are retired so they never carry the rejected meaning |
| Halting Word | A duplicate of Bewilder. Merged |
| Knell or Keening as a base spell | Knell is kept for the Dirge's passive |

"Spur" sits next to the condition Spurred and "Interject" next to
Interpose. Neither is an equality collision; both are flagged to the
naming pass (resolved: Spur renamed Urge, Interject renamed Speak Over,
naming pass NP-38).

**SRD check (settled principle, classes stage 1; lead's call to repeat
it here).** The SRD-shaped mechanisms used here are spell slots, Slot
Renewal, bonus-action heal-at-range shapes, smite shapes and spells
shaped on Cause Fear, Fear, Healing Word, Dissonant Whispers, Dispel
Magic, Command and Calm Emotions. They were written from memory. **Every
SRD-shaped mechanism must be checked against the SRD 5.2.1 document
before any record is written**, and every record flagged `srd` cites the
entry it was checked against. Cause Fear and Command (its "halt" use)
are the ones most likely to differ. Class, subclass, option, feature and
spell names are original; no D&D-only name or text is used.

**Waves:** M1 is the vertical slice (one class, levels 1–3, solo), M2
the full run with four classes and companions, M3 profile and unlocks,
M4 breadth (species, multiclassing), Later everything after that no
milestone names. **(settled, main design 9)**

## 1. Intent

Stage 1 fixed what the Herald *is*: a Presence full caster and the
morale and support class of the launch four, whose verb is Cadence
("break them before you kill them"), with morale as its primary lean,
ally tempo and control as secondaries, healing only through spell cards
and the Hearth, slots and Cadence as its pools, Scatter as the
illustrative capstone, and four subclasses named (Dirge and Hearth at
M2, Warcaller and Pennant as unlocks). **(settled)** It left open how big
the pools are, what the at-will does in numbers, the full spell list,
what each subclass does, and how the class's one healing lean is kept
from becoming a requirement.

The answer in one sentence: **a Herald breaks enemies before it kills
them and moves its people; healing is a ribbon it can take, never the
reason to bring it.** Defaults never heal; the class clears floor 1 with
no heal card; healing competes with the class's identity for the same
bonus action and the same camp spend; and both sides of the client's
goal are measured: a gate that healing adds at most 10 points to a
Herald, and required reports that a Herald is never needed and that a
non-healing Herald is still worth bringing. **(agreed, Q1)**

## 2. Goals, non-goals, anti-goals

### Goals

- **"Not an absolute requirement on every run"** (the brief): a party
  without a Herald is never short of an answer (5.5), and the reliance
  report measures it (5.4).
- **"Players don't entirely rely on it"** (the brief): healing is never
  the reason to bring a Herald. The heal-isolation gate holds the value
  of heal cards to at most 10 points per subclass **(agreed, Q1)**.
- **"A mechanical niche that is fun"** (the brief): a Herald who never
  heals visibly changes outcomes through morale and tempo, measured by
  the desirability report (5.4).
- **Research-led design** (the brief): 4.1 surveys support and healer
  classes and names what is taken and avoided.
- **Three things to read at every level** (settled, ability 11.4;
  classes 7.6), with one spare row.
- **Answers in every room:** a Herald always has a route against
  fearless, mindless, boss or Emboldened rooms (5.7). The mindless room
  is the honest weak spot, and the trap-room report watches it with an
  escalation threshold (5.4).
- **Two M2 subclasses that are roles:** the morale breaker (Dirge) and
  the field medic who is never a heal-bot (Hearth).

### Non-goals

- **Companion kits in full.** This document sets the requirements for
  Herald recruits (7.17); the companions design writes them.
- **The enemies, conditions and consumables owners' rulings.** This
  document requests them (section 19) and states how the class works
  either way.
- **The multiclass model** (M4); only the dip guards are honoured here.
- **Final names** (naming pass) and **final numbers** (simulator).

### Anti-goals

- **A required healer.** No class or subclass is required; the DH 18.1
  gate stands unchanged **(agreed, Q10 of DH)**.
- **A quiet identity.** A support class whose morale half under-delivers
  is played as a healer anyway, which brings reliance back by the side
  door (5.1).
- **Healing growth by any route** other than a heal card's own tier or
  the Hearth's one widening (`HealGrowsOnlyByTierOrHearth`).
- **A heal in any default:** no creation default, recommended pick,
  badge, pre-tick, rotation rule or preview line names a heal card, and
  no creation default, recommended pick, L3 default or badge names the
  Hearth (`NoHealInDefaults`, 14.2). A player who chose the Hearth gets
  its non-heal rotation rule like any subclass.
- **A new pool, reading row or condition:** the condition budget of 32
  stays untouched; 6 of 8 limited abilities; 3 rows.
- **Created actions, Rallied grants, Rally refreshes, Cadence refills, a
  class Demoralise trigger, an Emboldened override, hard control,
  teleport, summons, ground zones or temporary HP.**
- **A hero coach.** The hero never gets suggestions (ability 11.3,
  **agreed**); every default here is a static default or a confirmation
  of the player's own action (the ARC-6 reading).

## 3. Constraints

### 3.1 What the parents already fix

All **(settled)**:

| Source | Binding text | What it means here |
|---|---|---|
| Classes 8.6 | Chassis: Presence / Wisdom; Sound 7; Will; medium ceiling, shield allowed (Shield hand or Focus hand kit pick); `weaponTraining {simple: all, martial: hands == 1}`; Tipstaff (Loom) never required; 2 of Leadership, Intimidation, Medicine, Lore, Perception; Involved; Cadence (Frighten, Spurred, Take Comfort); Rally unchanged; Harrowing Word; heal cards (Mending Word, exactly Recovery, may target the Herald); concentrator 2 of 2; Dirge and Hearth M2, Warcaller and Pennant unlocks; Scatter *(illustr.)* | 7.1 |
| Classes 8.6 **(agreed, stage 1 Q1)** | Leans: morale primary; ally tempo secondary; healing only through spell cards and the Hearth | 5, 6.3 |
| Classes 5.6 | `Floor1ClassSpread` ≤ 10 points, every pick and subclass, an M2 release gate; about 60% of limited power after level 1; `SeedHasTarget`; the per-round boss breakdown | 7.7, 11.5 |
| Classes 6.1–6.6 | Lean, never require; the spread rule; forbidden grants; the utility map (morale H P; control A P, H s; classes 6.4) (cross-document comparison 2026-11-23, CC-78); fallback damage types; reliance reports **(agreed, Q3 of stage 1)** | 5, 7.1 |
| Classes 7.3–7.8 | Subclasses and aspects; the published ladder, caster column; the cap of eight and offers; at most four things to read; dip guards; companions and hero classes | 7, 9 |
| Classes 8.2 | One `restDefensiveTool`, half Recovery, 1 per rest, 2 from 6; 2 kit potions; camp share 40–60% | 7.3, 11.10 |
| Classes 10 | Pennant, not Banner; banned head words | Front matter |
| Ability 6.3–6.7 | Rest and camp pools; one camp spend per turn; slots, Slot Renewal, spells known, one camp swap, upcasts only where data declares them; capstone camp 1 at level 9 | 7.5, 7.9 |
| Ability 7.1–7.3 | Declared costs; the universal Frighten, Rally and Demand Surrender; a policy on every reaction | 7.3, 7.4, 7.14 |
| Ability 8.1, 8.2, 8.4 | Areas ≤ 25 squares at L1–6 (49 at L7–10, ARC-9); friendly fire unless selective; no ground zones **(agreed)** | 8.1 |
| Ability 9.3–9.5 | One rider per target per use; rider save bounds; concentration | 7.2, 7.6 |
| Ability 11.3 | Suggestions for companions only; the hero never gets suggestions **(agreed)** | 7.14 |
| Stat model 5.4, 7.1, 8.4 | Presence pays most as the hero; Rally (steady or Spurred, never both, once per ally per fight); `mindless` types are psychic- and `mind`-immune | 5.7, 7.4 |
| Conditions 6.3, 12.1–12.3, 18.4, 18.5, 18.8, 19 | Re-application; tags (`fear`, `mind`); the immunity budget; boss resolve pips and Unyielding; Spurred; Bleeding, Poisoned, Withered; Frightened, Rallied, Emboldened, Demoralised; the interaction table | 7.3, 8.1, 10 |
| Enemies 10.4, 10.6, 14, 17.5 | Floor-1 cowable group on the main path (CL-10); themes at least half non-fearless; groups, profiles, T1–T9, the T5/T6 substitution, Emboldened and Command, Routed, Demand Surrender and Yield; the boss framework | 5.7, 7.3, 7.16 |
| DH 11.2, 12.1, 12.2, 18.1, 18.2, 19.3, 21.1 | Get-up rules **(agreed, Q4)**; healing sources; heal spells scale by targets; the healer gate **(agreed, Q10)**; death and win-rate targets **(agreed, Q11)**; the reference policy; `Floor1BossSustain` | 5, 7.12 |
| Consumables 13.1, 16.4–16.8; CN-11 | Scrolls in class; `classTags`; a scroll read uses the turn's camp spend **(agreed)** | 8.8 |
| Weapons 6.4, 11.2, 17.8 | Parity ≤ 110% (VG-12); versatile with a shield rolls one-handed; Loom | 7.1, 11.1 |
| Armour 10.4, 11 | The one-reaction-AC-bonus window; caster defence (item 2) and envelope (item 1); AR-20 at 11 | 7.12 |
| Trinkets 11 | The copy registry, rule 1 | 7.18 |
| Vanguard design, Shade design, Arcanist design | VG-10 (turn allowances count 0), VG-12 (parity reading), VG-13 (level-1 camp miss); SH-4 (per-rest cost waivers), SH-5, SH-27 (tools are not leans), SH-28; ARC-2 (`SpellTierBound`, `PayoffBound`), ARC-3, ARC-4, ARC-6 (`castDefaults` reading), ARC-8, ARC-13, ARC-15, ARC-22 (`VariantIsScroll`) | 7, 8, 11 |

### 3.2 Numeric conventions

**(settled, enemies 3.1; items 3.2)** Integer arithmetic only, with
`FloorDiv`; all rounding is down; percentages are permille integers;
nothing derived is stored. There is no d3 in the system (weapons 6.6),
and none is used here.

### 3.3 Findings in the approved documents

Gaps and facts found while designing; each shaped a rule here or a
change in section 18. **(lead's call)** Ids F1–F15 are local to this
table.

| # | Finding | Where it is resolved |
|---|---|---|
| F1 | The brief names the fourth subclass "Banner", a name already rejected (classes 10) | **Pennant** (9.4) |
| F2 | The universal Frighten has no stated range (ability 7.2; enemies 14.6 says only "Frighten's range") | Range 6 with line of sight for everyone, so Cadence leans on cost and the Tipstaff on range (HR-14; told plainly) |
| F3 | Loom is worded two ways (weapons 17.8: "Frighten and presence ranges +3"; classes 8.6: "+3 range on fear") | Loom applies to Frighten and to **hostile-facing** presence effects only; never heals, calls, Urge, Rally or Demand Surrender (HR-15) |
| F4 | Fearless is not fear-immune; mindless is psychic-immune *and* `mind`-immune | 5.7; Plain Speech (7.2); the content rule (Q3, **agreed**) |
| F5 | Nothing sets Frightened's `controlRank` against Dazed (the default boss `resolveThreshold`) | Ruling requested: equal to Dazed, so fear spends pips (HR-16a). The class works either way |
| F6 | The Panicked transform row (conditions 19) would make a Herald fear effect on a Frightened, non-Demoralised foe *end* its Frightened | The row ships with Panicked (Later); in M2 re-application follows conditions 6.3 (HR-17) |
| F7 | The repeat rule covers "Frighten attempts" only, so spell fear could re-lock a boss every turn once its pips are gone | After a Frightened from a source ends, every later fear effect from that source gives the target advantage on its save (HR-16b) |
| F8 | The bonus action is crowded: Rally, potions, swaps, Cadence Frighten, Take Comfort, bonus-action cards | Urge moves to an `onEvent` cost (HR-2); each subclass's default path takes load off the bonus action, never adds to it; Parley, the Dirge's expert L6 option, is the one recorded exception (9) |
| F9 | `hands == 1` does not match the weapons `hands` values (one-handed, versatile, two-handed) | Read as one-handed and versatile (HR-9) |
| F10 | The Herald holds the control row as a secondary (classes 6.4: "A P, H s") | Listed in `leans[]` (6.3) |
| F11 | The Hearth's "Mending Word also ends one Bleeding instance" does nothing: Bleeding already ends on any HP gain (conditions 18.5) | Replaced by poison, or Withered instead of healing (HR-7; told plainly) |
| F12 | "Help at range" already lost in the registry once (CO-B3: dropped for Spurred) | Onward cut and Exhort held at M2 (8.3) |
| F13 | A bonus-action Medicine get-up already exists on the Lifeline Pendant; trinkets 11 rule 1 fails the build on a class copy | No Hearth feature makes the get-up a bonus action (Hand Up dropped) |
| F14 | Stage 1 asks for Demand Surrender range 4, but classes 11.H lists Demand Surrender as "Unchanged" | An enemies 14.6 row plus an 11.H correction (HR-19) |
| F15 | The theme rule "no tag every enemy is immune to" is conditions 12.2, not enemies 10.6 | Cited correctly here (5.7) |

### 3.4 Terms

**(lead's call)** Used with these meanings everywhere below, beside the
stage-1 terms (classes 3.4) and the Vanguard, Shade and Arcanist
designs' terms (rank, Art, turn allowance, glint, pre-tick, arms), which
stand **(settled)**.

- **Broken:** Demoralised (enemies 14.3); a design shorthand only:
  previews and card text print "Demoralised" (naming pass, NP-38).
  **Rout-ready:** broken, not
  Emboldened and not `fearless`, so a Frighten or a limited fear effect
  that lands Routs it (T5, T6).
- **Faltering:** the derived predicate of 7.2 (a flag, never a
  condition).
- **Heal card:** a Herald spell record with a `healRecovery` effect.
  At M2 there is exactly one, Mending Word.
- **No-heal default:** the creation defaults of 7.15, which hold no heal
  card and no Hearth.
- **The three Herald arms:** no Herald; Herald without heal cards;
  Herald with heal cards (5.4). Beside them run two more Herald arms,
  "Herald, free draws" and the diagnostic "Herald, card added" (5.4).
- **Side by side:** an extra target is side by side when it is within
  2 squares of at least one base target chosen in the same use, and
  within the option's or card's own range from you (the Hearth, 9.2).
- **Hearth list, Hearth cure:** 9.2.
- **Camp spend:** the turn's one camp spend (ability 6.4).
- **The three beats:** scare or steady (bonus action), then speak
  (action), then step (Urge rides the action) (6.3).
- **Tie-break:** wherever a rule says "nearest", "lowest HP" or another
  ordering and two units tie, the order is distance from you, then
  current HP (lower first), then unit id **(lead's call)**.

## 4. Framework decisions

The client chose Q1–Q3 on 2026-11-20, each time the lead's
recommendation. **(agreed)** The rest are the lead's calls, which the
client said stand and confirmed when it approved this document on
2026-11-20.

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Holding "not required" and "not relied on" to account (Q1) | **Heal isolation is a gate, run per subclass:** Herald with heal cards − Herald without heal cards ≤ 10 points win rate, lead's target 5. **Herald reliance** (no Herald vs Herald ≤ 10 points) and the **desirability report** (Herald without heal cards − no Herald ≥ +2 points; heal-card pick rate in the reference arm ≤ 50%) are **required reports with escalation thresholds** (miss → the lead applies the levers in order → if they run out, back to the client). Desirability misses are fixed with morale and tempo levers, **never healing**. The DH 18.1 healer gate is unchanged (5.4; HR-26, HR-38) **(agreed; the told-plainly items in the front matter accepted with it)** | B: gate reliance now too (reliance swings 0 to +21 points on the unmeasured morale cut *(illustr.)*, so a gate could force the morale identity to be weakened). C: reports only, the stage-1 default (leaves "not relied on" unenforced) |
| 2 | Camp share at levels 1–3 (Q2) | **Accept the Herald's level 1–3 miss** (23 / about 36 / 33–37 *(illustr.)*) as ARC-4, SH-28 and VG-13 did; read levels 3–4 on the class mean over both subclasses; **Cadence stays 4 at levels 1–4** (11.10; HR-31) **(agreed)** | B: Cadence 3 at levels 1–4 (passes level 3 but costs about 3 floor-1 clear points in the binding split) |
| 3 | Floor-1 boss content rule (Q3) | **Class-blind: the floor-1 boss is never `mindless` and never psychic-immune.** `ClassGatedContent` is untouched. **It is motivated by the Herald**, and the client was told so: in that one legal room a heal starter added 30–35 points *(illustr.)*, which would make healing the reason to bring a Herald (7.16; HR-21) **(agreed)** | B: no rule; rely on Plain Speech and a rerun |
| 4 | Identity model | **Morale-primary, tempo-forward:** the agreed lean record unchanged, tempo taught as the visible beat after each action, healing never a beat of the turn (6) **(lead's call)** | Tempo-first as a model (reverses agreed stage-1 Q1; fails solo floor 1). The Verses engine (held Later) |
| 5 | At-will | **Harrowing Word with Ache** (1d10 + Pre on a failure, Pre on a success, none to minions on a success), **Faltering as save disadvantage**, **Plain Speech** (7.2; HR-3) **(lead's call; told plainly)** | The drafted 1d12 "negates" plus +1d8 (115% parity). Faltering as "+1 die" (kept only as the tested fallback, +1d4) |
| 6 | Cadence | **4 / 4 / 5 / 7 / 8** at levels 1–4 / 5–6 / 7–8 / 9 / 10; options Frighten, Urge, Take Comfort and the subclass option (7.3; HR-1) **(lead's call; levels 1–4 agreed, Q2)** | Stage-1 2 / 3 / 4 *(illustr.)* (fails floor 1; 73% camp share at L10). 3 at levels 1–4 (Q2 B) |
| 7 | Urge's cost | **Rides your action:** `onEvent: ownActionResolved`, 1 Cadence, once per round, so the bonus action holds the real choice (7.3; HR-2) **(lead's call; told plainly)** | Urge as a bonus action (crowds the bonus action; makes "Frighten or Urge" a false choice) |
| 8 | Healing bounds | **One base heal card,** Mending Word, targets 1 / 1 / 2 / 3 by tier, no badge, draw weight 800‰, never in a default (5.3; HR-6) **(lead's call)** | A second base heal card or a tier-2 heal step; Song of Rest; heals that scale with Presence; a reaction heal; Kindle; Field Dressing |
| 9 | The Hearth | **A field medic, never a heal-bot:** no Hearth record carries `healRecovery` (`HearthNoHeal`); Hearthkeeper widens a heal card the player chose, at tier 2+, in combat, on class casts (9.2; HR-7) **(lead's call; told plainly with Q1)** | A healing Hearthsong (every Hearth a healer without a card). Hearthkeeper "+1 at every tier" (fails the heal-isolation proxy, +9.6 to +11.3 *(illustr.)*) |
| 10 | Starting kit | **A longsword** (versatile, pool-free Parry) in both kits; the Shield hand is the default (AC 16) (7.11; HR-36) **(lead's call; told plainly)** | The mace (Topple, but no pool-free Parry) |
| 11 | Capstone | **Scatter** with a save at disadvantage for broken foes and a **second clause** that Frightens every other hostile in sight for a turn, bosses and fearless units included, never routing them (7.9; HR-5) **(lead's call; told plainly)** | Stage 1's text alone (dead in boss rooms). An automatic rout (early-end risk) |
| 12 | Level-3 default | **Dirge**; the Hearth is never a default; class means weigh both subclasses equally (7.15) **(lead's call; told plainly with Q1)** | Hearth as default (puts a heal lens in a default) |
| 13 | Reading | **Three rows:** at-wills; spellbook (every tier plus Scatter); Cadence. The overlay counts 0 (7.1) **(lead's call)** | — |
| 14 | Fear and boss pips | **Fear spends pips, with the repeat rule widened to every fear effect** (HX-1, HX-1b; HR-16) **(lead's call; a ruling for the conditions owner)** | Frightened ranked below the threshold (a boss at disadvantage from round 1 off a rest pool: the Herald near-required for bosses) |
| 15 | Spell list | **20 drawable cards, 19 that never heal,** plus 2 subclass spells; Onward cut, Exhort held Later (8) **(lead's call)** | Onward (fails `AllyUpliftBound`; repeats CO-B3) |

### 4.1 How other games handle support and healer classes

Mechanism references only. No names or text are taken. Checked during
research: PF2e Demoralize and its immunity (Archives of Nethys, Paizo
forums), the Battle Brothers morale dev blog, and the 4e warlord (a fan
wiki and an EN World thread). The rest is recall, flagged in Appendix B.
Every SRD-shaped mechanism is checked against SRD 5.2.1 before any record
is written **(settled, classes stage 1)**.

| Game / feature | What it does | We take | We avoid |
|---|---|---|---|
| **5E bard** (Bardic Inspiration, Cutting Words, Song of Rest) | Banked bonus dice; a reaction that cuts an enemy roll; extra healing at a short rest | The fun of acting on another unit's turn, held as Speak Over (M3 candidate) | Inspired tokens (a Later condition; the budget of 32 is full); Song of Rest, an uncounted rest-heal route that makes the class near-required |
| **5E cleric, Healing Word / Mass Healing Word** | A bonus-action heal at range; the mass version adds targets | **Mending Word:** cheap in tempo, fixed at exactly Recovery; higher tiers add targets, never HP | Big HP heals; heal size that scales with an ability |
| **4e warlord** | Inspiring Word (minor action, the ally spends a surge), Commander's Strike, granted shifts | Granted movement (Spurred); a granted attack that spends the ally's *own* reaction (Warcaller) | 4e made every leader a healer, and the leader role became mandatory. **That is the client's worry, so tempo stays apart from healing** |
| **PF2e Inspire Courage and the Marshal** | One-action status bonuses; fear auras both ways | Fear support split into frightening foes and ending allies' fear (Rouse) | Standing passive auras whose numbers are forgotten |
| **PF2e Demoralize** | Frightened 1–2, then immunity to the same character's repeat attempts | Confirms the settled same-source repeat rule; extended here to fear spells (HR-16b) | — |
| **Paladin auras** | Strong invisible passives | — | Invisible numbers; any aura shows as a ring and never stacks (Pennant) |
| **13th Age commander** | Command points earned in play, spent as interrupts | "Rides your own action" (Urge's `onEvent` cost; the Dirge's Refrain) | A pool refilled mid-fight (a forbidden refill outside rests) |
| **Darkest Dungeon** | The Vestal is optional because killing and stunning beat healing | Routs and Frightened as **preventive sustain**; a self-heal on every class | — |
| **Baldur's Gate 3** | Bonus-action Healing Word "yo-yo" revives of downed allies | — | Yo-yo healing, already blocked: Mending Word never targets or lifts Downed (settled) |
| **Divinity: Original Sin 2** | No dedicated healer; sustain moved to armour | The morale class has its own way to stop incoming damage | — |
| **XCOM 2 Specialist** | Remote medikit and buffs: a baseline every squad has, made remote | "Lean, never require": the Hearth's Treat at range 2 | — |
| **Battle Brothers** | Morale is core, but undead ignore it; the banner defends against fear | Fear defence (Rouse); a non-rout payoff against fearless foes (Faltering) | A morale class dead against undead |
| **Total War: Warhammer** | Undead "crumble" instead of routing | Morale pressure converted into another currency (Faltering's save disadvantage) | A damage-over-time on fearless units |
| **Fire Emblem dancers** | Refresh another unit's turn; the strongest support in the series | — | Created actions (forbidden). The Warcaller spends the ally's own reaction |
| **Into the Breach, Slay the Spire** | No healer; healing is scarce and costs an opportunity | Every heal costs the bonus action **and** the camp spend | — |
| **Dungeon World bard** | One move, two modes | One Cadence row, several options | — |
| **Pillars of Eternity Chanter** | A chant meter | — | Meters; the Verses engine is held Later for this reason |

**Combined lesson.** Support classes stay non-mandatory when three
things hold:
1. the baseline gives every party the healing and morale routes (the
   class-blind guarantee; universal Frighten, Rally, Demand Surrender);
2. the class's best turns are offensive or tempo turns;
3. healing competes for the same action and resource as the class's
   identity.

Defaults decide what most players do, so keeping heal cards out of every
default is the single strongest lever for "never relied on". The
opposite lesson matters as much: **a support class whose identity is
quiet will be played as a healer anyway.**

## 5. The client's brief: never required, never relied on

### 5.1 The question and the lead's answer

**The client's view.** The Herald must not be required on every run;
players must not rely on it; like every class, its use is a mechanical
niche that is also fun.

These match approved rules: healing is spread thin; there is no required
healer class; the DH 18.1 gate holds the healer gap to ≤ 10 points; DH
18.2 calibrates the no-healer arm to at least a quarter of runs; the
class-blind healing guarantee covers every party. **(settled)**

**Healing is already optional by structure,** and nothing here weakens
that:
- defaults never heal;
- every heal costs both the bonus action and the turn's camp spend;
- three of the four subclasses carry no healing at all;
- the Hearth's own records heal nothing (it only widens a heal card the
  player chose).

**The real risk is the opposite failure.** In the scratch proxies the
drafted Herald was **too weak** without healing: it cleared solo floor 1
30–38% of the time against 94–99% for the other launch classes
*(illustr.)*, and in one legal room (a mindless boss) a Mending Word
starter added 30–35 points. A class whose identity under-delivers gets
"fixed" by players taking the one contribution they can see, which is
the heal card. That is exactly the reliance the client wants to avoid,
arriving by the side door.

**The lead's answer (agreed, Q1): agree, with one refinement.** "Not
required" is two conditions, and both are measured:
1. **Not required:** a party without a Herald, and a Herald without heal
   cards, are both never short of answers (5.5, 5.6). Healing never
   becomes *the* reason to bring a Herald: the **heal-isolation gate**
   (5.4).
2. **Worth bringing:** a Herald who never heals must visibly change
   outcomes through morale and tempo. If it does not, players take heal
   cards to feel useful, and reliance returns. This is the **desirability
   report** (5.4). A miss is fixed with louder morale and tempo, **never
   with more healing**.

So most of this design makes the **no-heal Herald strong, visible and
fun**: a better at-will (7.2), a visible Faltering payoff, answers to
mindless, boss and Emboldened rooms (5.7), and previews that teach the
morale cascade (10.3).

### 5.2 How the Herald stays optional: morale and tempo are the job

**The niche.** The Herald breaks enemies before it kills them, and moves
its people:
- **Morale (primary):** Cadence Frighten routs a broken foe (T5); fear
  spells rout through T6; Scatter turns a break into a collapse; the
  Dirge deepens all of this.
- **Ally tempo (secondary):** Urge after each action; Rouse ends fear and
  Urges; the Drummer recruit.
- **Control (secondary, shared row):** Frightened's disadvantage and
  "cannot approach"; Dazed from Bewilder and Overwhelm.
- **Preventive sustain:** a Frightened foe attacks at disadvantage and
  cannot close; routed and yielded foes make no more attacks; fights end
  sooner.

**Every party already has the baseline,** so the Herald only *leans*
(`LeanWithoutBaseline`, settled):

| Herald job | Baseline every party has (universal routes only) | Other classes that also lean (not a baseline) |
|---|---|---|
| Morale | Any hero's Frighten (an action) and Intimidation; Demand Surrender; cowing; Rally; T1–T7 fire class-blind | The Shade's Cutthroat lens |
| Ally tempo | Rally's Spurred option; Help | The Vanguard's Sound the Advance |
| Ending allies' fear | Rally; tonics | — |
| Healing | The guarantee (0.70–1.00× max HP per member from floor 2; 2.16× solo on floor 1); rests; potions; every class's rest tool; Medicine; heal-kit recruits | — |

Only the middle column counts for `LeanWithoutBaseline` (classes 6.1
item 4): a baseline is a route any party can use, never another class's
feature **(lead's call)**.

### 5.3 How healing stays bounded

**(lead's call unless marked)**

| Bound | Rule |
|---|---|
| One base heal card | **Mending Word** (tier 1). Higher tiers add targets only: 1 / 1 / 2 / 3 at t1 / t2 / t3 / t4. Ability 6.5 (settled) allows a higher slot only where data declares what it adds, so the record declares a **tier-2 entry that adds no target** (HR-6): a tier-2 slot casts the tier-1 effect. The cast's tier (`cast.tier`) is the slot spent, which is what the Hearth reads (9.2; the slot rule in 8.1) **(new lead's call)** |
| Exactly Recovery | Each target heals exactly its Recovery, with no Medicine bonus (`HealPerTargetAboveRecovery`, settled) |
| Never revives | It never targets a Downed unit and never lifts Downed **(settled)**. Withered greys the target |
| Double price | A bonus action (no Cadence Frighten, Take Comfort or Rally that turn) **and** the turn's camp spend (no other slot spell, scroll, wand or Scatter that turn) |
| No tempo rides it | Urge fires on your *action*. A heal is a bonus action, so it never triggers Urge. No `mendingSpur`, no heal on rout, no heal on any capstone rider |
| No growth path | No offer, trigger, rank, focus or item changes heal targets, HP, range or cost (`HealGrowsOnlyByTierOrHearth`, `NoHealTrigger`). Loom never widens heals (HR-15). `slotRecovery+1` is excluded for the Herald (HR-35) |
| No defaults | No creation default, `recommendedPickOrder` entry, Recommended badge, pre-tick, rotation rule or preview line names a heal card; no creation default, recommended pick, L3 default or badge names the Hearth (`NoHealInDefaults`, 14.2) **(told plainly, accepted with Q1)** |
| Draw pressure | At most one heal card per draw (s1, 7.7); heal `drawWeight` 800‰; no `drawRequires` on heals; no badge. The card reads "Heal card: your party counts as having a healer. Uses your bonus action and your slot spell this turn." |
| Hearth | **No Hearth record heals** (`HearthNoHeal`) **(told plainly, accepted with Q1)**. Hearthkeeper adds one side-by-side target to a heal card *the player chose*, only at tier 2+, in combat, on class casts |
| Recruits | No Herald recruit kit heals (`NoHealOnHeraldKit`) |
| Multiclass | Heal cards upcast only to tiers the Herald's own class-level slot table grants. Two halves (HR-34): **data now**, every `healCard: true` record carries `upcastCap: classSlotTable` (content check `HealUpcastCapDeclared`); **runtime at M4**, the cast legality check caps a heal card's effective tier at the highest tier in the Herald's own class-level slot table (`HealUpcastByClassSlots`, enabled when multiclassing ships) |

**Rejected** (one line each):
- **Kindle** (Take Comfort on an ally): Take Comfort is self-only (settled), and
  DH 12.1 defines the rest tool as a self-heal.
- **Field Dressing** (a get-up that leaves the companion standing): DH
  11.2 (**agreed**, Q4) says "up on 1 HP, Prone".
- **A healing Hearthsong:** it would make every Hearth a healer without a
  card.
- **A second base heal card, or a tier-2 base heal step:** heal volume
  without a decision.
- **Song of Rest; heals that scale with Presence; a reaction Mending
  Word:** uncounted or growing heal routes.

### 5.4 The healer gate, the Herald arms, the heal-isolation gate and the reports

**The healer gate is unchanged (DH 18.1, agreed Q10).** **(settled)**
- A party has a healer when a member's kit holds a `healRecovery`
  ability whose targets include an ally other than self, judged on the
  kit snapshot, on the same seeds, under the reference policy.
- The healer and no-healer win rates differ by ≤ 10 points.
- DH 18.2's "no-healer arm at least a quarter" is a **calibration
  target, not a gate** (agreed Q11). A miss goes to the client.
- **When the snapshot is taken (new lead's call; a reading of DH 18.1,
  told plainly):** the Herald is the first class whose heal can arrive
  mid-run (a draw at L2, 5, 7 or 10, a camp swap, or a forced starter),
  so the snapshot is pinned to **the kit at the start of each fight**,
  and a run counts as "with healer" if **any** fight's snapshot
  qualifies. This matches the DH intent that a party which ever fields
  an ally heal is a healer party. The choice is recorded in the tuning
  file and the simulator spec; the per-fight classification columns
  stay reports beside it.

A Herald with Mending Word is a healer under this gate. A Herald without
a heal card, and a Hearth without a heal card, are not. Take Comfort is
self-only and never makes the Herald a healer.

**The Herald arms** (CL-9; DH 18.1 reports; change HR-26). Policy
forced, on the same seeds, under the reference policy (DH 19.3), each
Herald arm printed per subclass:

| Arm | Construction | Read for |
|---|---|---|
| **No Herald** | The hero is each of the other three launch classes in turn, each at its own creation defaults, on the same seeds; one row per replacement class is printed beside their unweighted mean, which is the arm's value. No Herald recruit | Reliance; desirability (a) |
| **Herald without heal cards** | The hero is a Herald of the named subclass at the no-heal defaults; heal cards removed from every draw and swap; "Hearth, no card" its own row | The heal-isolation gate (base); reliance; desirability (a) |
| **Herald with heal cards** | The hero is a Herald of the named subclass; Mending Word forced as the second starter, replacing Grating Murmur; draws otherwise free | The heal-isolation gate; reliance |
| **Herald, free draws** | The hero is a Herald of the named subclass at the no-heal defaults; no forcing; heal cards drawable as in play | Desirability (b) and the heal-card pick rate (heal telemetry) |
| **Herald, card added** (diagnostic) | The no-heal default starters **plus** Mending Word as an extra known spell, on the same seeds | The heal card's added value without the lost damage starter |

**New lead's calls on the arms:**
- In every Herald arm the hero is the Herald (only the hero can hold a
  heal card, because no Herald recruit kit heals), and the level-3
  subclass is forced to the row's subclass.
- **Recruits:** every arm draws recruits from the full pool under the
  scripted recruit policy (ability 14.6), pinned to the reference slot
  schedule and the written `MusterBestFit` policy (companions design
  11.3; change CMP-18). In the no-Herald arm only, a
  drawn Herald kit is replaced by a re-roll on a dedicated random
  substream, so every other recruit draw, heal-kit recruits included, is
  the same on the same seeds in every arm.
- **The gated reading is the swap** ("with heal cards" against "without
  heal cards"), because it is the player's real choice: a second starter
  is one or the other. The swap also removes Grating Murmur, so it
  understates what the card adds on top of a full kit. The **card-added
  arm** is printed beside it as a diagnostic: if "card added" −
  "without" exceeds 10 points for a subclass, the healing levers for
  that row run as for a gate miss.
- The no-Herald hero is the mean of the other launch classes, not the
  release class mix, so the reliance and desirability lines do not
  depend on which class happens to replace the Herald; the per-class
  rows show the spread.

**The heal-isolation gate (agreed, Q1; change HR-38).**
- **Rule:** for each M2 subclass (Dirge, Hearth), win rate of "Herald
  with heal cards" − win rate of "Herald without heal cards" **≤ 10
  points**. The **lead's target is 5**.
- **Status (new lead's call):** an **M2 release gate** read by the
  simulator, like `Floor1ClassSpread` (classes 5.6), on the release seed
  set under the reference arm. It is read at the first M2 simulator run
  with Mending Word, Hearthkeeper, the starter rule, `NoHealInDefaults`
  and the arm construction in data; earlier runs are diagnostics
  only.
- **The lead's target (new lead's call):** a gap above 5 and up to 10
  passes the gate; it is printed as a watch row, and the lead may apply
  the healing levers below (for that row) to bring it back toward 5. A
  gap above 10 fails the gate, and the lead must apply the levers.
- **Each subclass must pass on its own;** there is no class-mean reading
  of this gate. Warcaller and Pennant rows join when they ship (M3).
- **It reads the non-healing Herald as the base** (told plainly,
  accepted with Q1): the subtraction measures what choosing a heal card
  over the default damage starter adds (the card-added diagnostic
  measures the card on top of a full kit), so the gate can only be
  passed by keeping healing small, never by weakening the morale half.

**Healing levers (lead's call).** Never the guarantee, never by class,
never HP per target:
1. Hearthkeeper's side-by-side radius 2 → adjacent.
2. Hearthkeeper's heal widening from tier 3 instead of tier 2.
3. Drop Hearthkeeper's heal widening; keep the cure part.
4. Mending Word's upcast targets (t3 2 → 1).
5. Heal `drawWeight` 800 → 600‰.
6. A once-per-rest sub-limit on self-targeted Mending Word (counts 0).
7. Heal-specific sub-limits in Herald data (a per-rest cap on Mending
   Word casts) or heal targets in companion data; never the Herald's
   slot or Cadence pools, which carry the morale half.
8. Random potion weight for everyone.

**Which levers each miss uses (lead's call).** Apply in order, one at a
time with a rerun, and stop at the first that meets the line:
- **Heal isolation, Dirge row (or a base-class miss):** levers 4–6.
  Levers 1–3 touch only Hearthkeeper and cannot move the Dirge.
- **Heal isolation, Hearth row:** levers 1–6.
- **Heal isolation, either row:** the order stops at lever 6; if the
  gate still fails, the miss goes back to the client. Levers 7 and 8
  are not used here: 8 moves both arms almost equally.
- **A DH 18.1 healer-gap miss or a 2+ escalation:** levers 1–8. Levers 7
  and 8 are DH 18.1's own order ("heal-spell targets or pool sizes in
  class and companion data; then random potion weight"), refined for the
  Herald inside its first lever (HR-38); "never the guarantee, never by
  class" stands verbatim.

**The required reports with escalation thresholds (agreed, Q1).**

| Report | Construction | Threshold and response |
|---|---|---|
| **Herald reliance** | The no-Herald arm against the Herald arms. **New lead's call:** per subclass, take the **higher** of "with heal cards" and "without heal cards" (normally "with"), so reliance can never hide inside the heal card; the class value is the unweighted mean of those per-subclass maxima (Dirge, Hearth) minus the no-Herald arm. Per-subclass rows printed | A gap above 10 points escalates. **Reliance lever order (new lead's call):** (1) if heal isolation is more than half the gap, the healing levers for that row (above); (2) the class's lean size, as classes 6.6 orders it (Antiphon spread 2 → 1, Mourners 3 → 2, Urge II back to "one ally plus self", Cadence 8 → 7 at L10 within the camp-share band); (3) the universal baseline route's content density (classes 6.6). Never a forbidden grant |
| **Desirability (a)** | Herald without heal cards − no Herald, win rate | **≥ +2 points** *(illustr. threshold)*. Class mean escalates; per-subclass rows printed and answered with per-row data changes **(new lead's call)** |
| **Desirability (b)** | Heal-card pick rate in the reference arm, read on the **"Herald, free draws"** arm (the only arm where a heal card is offered and not forced), over every draw or swap that offers one. Printed for the class and **per subclass** **(new lead's call)** | **≤ 50%**, class and per subclass. A Hearth row above 50% is answered with the Hearth's "weak without a card" levers (9.2), never healing |
| Desirability, both | — | A miss is fixed with the morale and tempo levers, **never healing** (11.10): Earned Urge; Faltering reach (a `triggerMenu` entry made automatic); Waver (a request to the enemies owner); Speak Over at L4 |

**Escalation procedure (agreed, Q1; procedure new lead's call, the ARC
pattern):**
1. Read at the same first M2 simulator run as the heal-isolation gate;
   earlier runs are diagnostics.
2. The **class mean** escalates; per-subclass rows beyond the threshold
   are printed and answered with per-row data changes.
3. The lead applies the levers in order, one at a time, with a rerun
   after each, and stops at the first that meets the threshold.
   **(SV-26, read; M2)** The rerun covers the class-data blast radius at
   each gate's N, plus every PR0 and PR1 gate as a nightly sample; a
   lever that turns a higher-rung gate from PASS to FAIL is reverted and
   goes to the lead. The heal-isolation gate is a PR1 (REL) row, and
   reliance and desirability are ESC rows (seeds and validation design
   6.3, 6.9).
4. **No lever is applied that breaks another line:** a desirability
   lever that would push reliance above 10, or a reliance lever that
   would push desirability below +2 or the heal-isolation gate above 10,
   is skipped. The window is therefore +2 to +10 points.
5. **If the levers run out:** back to the client.
6. **No new gate** beyond heal isolation: classes 5.6 and DH 18.1 stay as
   written.

**Further report columns beside the gate** (none changes it; HR-26)
**(lead's call)**:

| Report | Construction |
|---|---|
| Healer classification columns | "Ever held an ally heal" per run and per fight, printed beside the gate's per-fight kit snapshot. A Herald who first takes Mending Word at L7 holds it in no fight snapshot on floors 1–3 |
| Healer count 0 / 1 / 2+ | Read the **2+ row first**, de-confounded from Herald morale. Escalation: 2+ minus 1 > 5 points → the healing levers (1–8) → back to the client. **While no heal kit ships, the 2+ row prints N/A**, and the DH 18.1 healer arm is exactly a Herald hero holding a heal card; the gate text is unchanged (companions design 5.4; change CMP-23, **agreed** Q3) |
| Heal-source breakdown | Herald hero card; Hearth widening; Herald recruit (always 0, printed); heal-kit recruit (prints 0 while no heal kit ships; companions design 5.4, CMP-23); scroll reads; **get-ups by route** (Medicine, Lifeline, Reach the Fallen) |
| `MoraleValueReport` | Per fight: enemy attack rolls removed by Rout, Yield, flight and Frightened disadvantage; rounds saved; potions saved |
| `AllyTempoValueReport` | Spurred moves used by allies; opportunity attacks avoided; attacks the step alone enabled; **party-wide Spurred moves per ally-turn** (escalation 0.4 *(illustr.)*); **Cadence spent per option** (Frighten and Antiphon, Urge and Soothe, Take Comfort, Refrain), with an escalation if Urge's share is above 50% at L1–4 on the novice or basic arm *(illustr.)*, answered first by the Urge pre-tick conditions (7.14) |
| Heal telemetry | Heal-card pick rate (on the free-draws arm); Mending Word turns against Cadence Frighten turns; overheal share; the out-of-combat heal-card share |
| Heal-starter delta | Default starter − Mending Word starter, in every `Floor1ClassSpread` split. Escalation above 10: the self-target sub-limit lever (lever 6) |
| Trap-room report | Herald clear rate split by each encounter's mindless, fear-immune and fearless share, with boss-only rounds split out, per subclass (5.7); a **fear-immune floor-1 boss** row; and an **escort aura answered solo** row (floor-1 boss rooms whose escort stays Emboldened, without Waver). **Escalation (new lead's call):** the Herald's clear rate in encounters with a mindless share ≥ 50% more than 10 points *(illustr.)* below the mean of the other launch classes in the same split → the trap-room levers in order: (1) `falterVsImmune` built into Plain Speech at L1 (its `triggerMenu` slot replaced); (2) `drawWeight` up on the mindless answers (Ringing Blow, Bewilder); then back to the client |
| Hearth-list frequency | How often hearth-list conditions are applied to the party, per floor and per theme, so the card-less Hearth's value can be read against its supply |

**Toy evidence** *(illustr.; five floors, party sizes 1 / 2 / 3 / 4 / 4,
N 10,000; not the simulator)*:
- **Healer gate:** healer 32.3–32.8% against no healer 26.8–27.5%, a gap
  of 4.8–6.0 points. Pass.
- **No-healer arm:** 25.0–27.5%, at the line.
- **Heal isolation:** base card +3 to +5 points; Hearth with the t2+
  reading +3.8 to +5.3 (pass); Hearth with "+1 at every tier" +9.6 to
  +11.3 (fail, rejected).
- **Healer count:** 0: 26.8% / 1: 32.0% / 2+: 43.6%. The 2+ row is
  confounded: every 2+ party in the toy holds a Herald hero.
- **Arms** (12% / 5% assumed morale cut): no Herald 25.5; Herald
  without heal cards (Dirge) 33.5–34.2; with a card +3.2 to +4.2; Hearth
  (t2+) +3.8. At a 6% / 2% cut the no-card arm reads 24.9–25.7.
  **Reliance depends entirely on the measured morale cut,** which is why
  it is a report and not a gate (agreed, Q1).

### 5.5 A party without a Herald is never short

Each Herald job has a baseline route every party has (5.2), and
`LeanWithoutBaseline` holds for every row. The new universal mechanics
(Waver if adopted, Pick the Question, the Break-chain preview, Offer
Terms if adopted) help every party, not only the Herald. The reliance
report (5.4) measures the claim.

### 5.6 A Herald who never heals is complete

- **What it keeps:** every core feature, three of four subclasses, the
  capstone, every default and the recommended build.
- **What it brings:**
  - fights end sooner;
  - routers flee, provoke opportunity attacks and drop their loot;
  - yields give floor facts;
  - foes attack at disadvantage and cannot reach the back line;
  - Spurred tempo for the party;
  - an at-will at 73–81% of the reference bare, rising to about 100%
    against faltering targets *(illustr.)*;
  - Scatter.
- **The evidence is a gate, not a promise.** `Floor1ClassSpread` is read
  on the **no-heal default** Herald (HR-10; told plainly, accepted with
  Q1), with heal-starter rows printed first and outside the mean. The
  class cannot ship if it needs healing to clear floor 1. The
  desirability report (5.4) measures the full run.

### 5.7 Where morale is a trap, and how the Herald stays useful

Facts checked in the approved documents **(settled)**:
- **Fearless is not fear-immune** (enemies 14.2; conditions 12.1). A
  fearless unit can still be Frightened; it just never routs.
- **Mindless** units are immune to psychic damage *and* to the `mind`
  tag (stat model 8.4; conditions 12.2). Frightened carries `mind` and
  `fear`, so mindless units blank fear entirely.
- Every boss is `fearless`, and a boss leads its escort. Escort members
  within 2 squares are Emboldened.
- No theme may be immune to one tag across every enemy (conditions
  12.2). Themes are at least half non-fearless (enemies 10.6). A single
  encounter can still skew.

| Room | What is muted | What still works | Honest residue |
|---|---|---|---|
| **Boss** | Rout, Yield, Demand Surrender | Fear spends a pip, then lands (HR-16a); the boss is faltering either way (unless it is `fear`-immune: 7.16). A Frightened boss cannot approach you and attacks you at disadvantage. Bewilder and Overwhelm (Dazed) spend pips. Fallen Idol reaches the escort. Scatter's second clause. Rouse against roars. Waver (if adopted) drops the escort's aura. T7 routs the escort when the boss falls | Pip attrition plus damage. The class's fun peaks before the boss; Waver is the fix |
| **Emboldened escort** | Frighten at disadvantage; Rout, T2 and T3 blocked within 2 squares | Frighten the leader (a Frightened leader cannot Rally; with Waver its aura drops). The universal Shove, or the shield bash's Push, moves a member out (Str 0, so the odds are modest). Give Ground once drawn (tier 2, so from the L5 draw at the earliest). An ally's Spurred Shove | Positional play that wants a party. **Solo on floor 1 without Waver the aura has only Shove and the bash, at Str 0: largely unanswered**, which the trap-room report watches |
| **Fearless, not mindless** (most undead) | Routs | Frightened lands fully (the whole defensive half); Faltering; every damage card; Scatter's second clause; the Hearth's cures against Withered and bleeding | No cascade; defence and damage only |
| **Mindless** | Every fear effect, and every psychic damage card | **Plain Speech** (Harrowing Word deals bludgeoning); Ringing Blow (weapon type); Bewilder and Overwhelm (Dazed has no tag); Give Ground (collision bludgeoning); Urge, Rouse; Unweaving; the longsword; `falterVsImmune` (with Plain Speech) | **The real trap.** Psychic cards go dead, which is honest. The floor-1 boss content rule (Q3, **agreed**) keeps it out of the class's one solo boss |
| **`fear`-immune, not mindless** | Fear | All damage, psychic included; `falterVsImmune`; the Dirge's `toll.vsFearImmune` | Damage and tempo |
| **Repeat fear on one unit** | Later fear from the same source is resisted (HR-16b) | Switch targets; damage | An intended bound; the preview says "Repeat: resisted" |

**Required report:** the trap-room report (5.4). **HI-4 camp line
(class-blind; HR-23):** "Next floor: theme X, about Y% non-fearless", so
any party can camp-swap cards before a mindless floor. Information only.

## 6. Identity models

### 6.1 The three models

| Model | Core idea | Strength | Weakness |
|---|---|---|---|
| **Morale-first** | Morale primary, tempo secondary, healing a ribbon; preventive sustain; policy-forced arms; an honest trap table | Matches the agreed lean record; the strongest evidence plan | Tempo invisible on the turn; the class reads quiet |
| **Tempo-first** (its author recommends "tempo-forward, morale-primary") | The party's tempo engine, designed in full | Supplies the "too required?" lens and the tempo guards | As a model it reverses agreed stage-1 Q1, fails solo floor 1 (no allies at L1–3), has no M2 tempo subclass, overlaps the Marshal, and risks the 4e "required leader" trap |
| **Verses** (a cadence-line flag; choruses; heals are "Hush" and never sing; its original kind names are retired working names, 6.3) | A sequencing engine that prices heals by chorus rate | The richest decision loop | Too much sequencing for an Involved-complexity class with the busiest bonus action. Detachable |

### 6.2 Verdict and judges' scores

Three judges scored each model (judge 1 / judge 2 / judge 3; 1–10;
"build cost low" means higher is cheaper). The mean is the mean of the
three; the total is the sum of the six means.

| Model | Depth and growth | Readability | Decision quality | Balance and safety | Fit with docs | Build cost low | Total | First place |
|---|---|---|---|---|---|---|---|---|
| **Morale-first** | 7 / 7 / 8 (7.3) | 8 / 8 / 8 (8.0) | 7 / 7 / 8 (7.3) | 8 / 8 / 8 (**8.0**) | 7 / 9 / 7 (7.7) | 8 / 8 / 7 (**7.7**) | **46.0** | 1 of 3 (judge 2) |
| **Tempo-first, as tempo-forward, morale-primary** | 7 / 6 / 7 (6.7) | 8 / 8 / 8 (8.0) | 8 / 7 / 9 (**8.0**) | 8 / 7 / 8 (7.7) | 9 / 7 / 8 (**8.0**) | 7 / 7 / 7 (7.0) | 45.3 | 2 of 3 (judges 1 and 3) |
| Verses | 9 / 9 / 8 (**8.7**) | 6 / 5 / 5 (5.3) | 9 / 8 / 7 (**8.0**) | 7 / 6 / 6 (6.3) | 7 / 6 / 5 (6.0) | 5 / 5 / 4 (4.7) | 38.7 | 0 of 3 (held Later) |

**Verdict (lead's call): "morale-primary, tempo-forward".** Judges 1 and
3 chose tempo-first *as tempo-forward, morale-primary*; judge 2 chose
morale-first. The totals sit 0.7 apart, inside the judges' noise.
**All three converge on the same design**: the agreed lean record with
tempo made the visible beat. Verses is held Later as a
playtest trial (10.1, V).

### 6.3 The spine and its grafts

**(lead's call)**

- **The `leans[]` record is unchanged (agreed, stage 1 Q1):** morale
  **P**, ally tempo **s**, control **s**, healing a ribbon on the class
  and a **sub** lens through the Hearth, Medicine/Treat **sub** (Hearth),
  companions and leadership **s** (hero only).
- **The turn is taught in three beats, in the order that pays; healing
  is never a beat:**
  1. **Scare or steady** (bonus action): Cadence Frighten, Take Comfort,
     Rally, Rouse, a potion or a swap. Scare first: a landed Frightened
     or a stripped pip makes the target faltering for your word this
     same turn (7.2, pip timing).
  2. **Speak** (action): Harrowing Word, a fear card, a damage card or a
     call.
  3. **Step** (Urge rides the action; 1 Cadence, once per round): you or
     one ally gains Spurred.

  A heal card *replaces* beat 1 and takes the turn's slot spell. Solo,
  the preview teaches "Urge yourself only when an enemy you have not
  Frightened is adjacent". It does **not** teach a self-Urge kite, which
  costs clears in the proxy.

| Graft | From | Where |
|---|---|---|
| The trap table; the arm classification and policy-forced arms; `MoraleValueReport`; HI-4; "Spurred grants and heals are never signature events" | Morale-first | 5.4, 5.7, 7.1 |
| The "too required?" lens (advantage-granting tempo makes a class required faster than healing, so Helped at range is cut); `SpurredSources`; `AllyUpliftBound`; `AllyTempoValueReport`; the Spurred step trail; the base heal ladder 1 / 1 / 2 / 3 | Tempo-first | 5.3, 7.14, 8 |
| The principle "Hush never sings": no heal ever starts, completes or rides a morale or tempo effect | Verses | 5.3 |

- **Held Later:** the Verses engine (a playtest trial; its kinds renamed
  so they never collide), used if the expert arm stays flat (11.6).
- **Cut:** an Urge as a bonus action; Helped at range (Onward, Exhort).

## 7. The core kit

### 7.1 Chassis

| Field | Value |
|---|---|
| Main / suggested second | Presence / Wisdom **(settled)** |
| `suggestedArray` | Pre +3, Wis +2, Con +1, Dex +1, Str 0, Int −1 **(lead's call)** |
| HP tier, save | Sound 7; Will **(settled)**. HP = 11 + L × (7 + Con) |
| Recovery | 4 + 2L + Con; Take Comfort heals half |
| Armour, shield | Medium ceiling; shield allowed **(settled)**. Shield hand or Focus hand is the creation pick (7.7) |
| `weaponTraining` | `{simple: all, martial: hands ∈ {oneHanded, versatile}}` (HR-9). Resolved families: rapier, flail, whip, shortsword, sabre, longsword, battleaxe, warhammer, war pick, trident, hand crossbow (one-handed, so legal; never in a kit). A versatile weapon with a shield rolls its one-handed die (weapons 11.2) |
| Focus | Tipstaff (presence, off hand), verb **Loom**: +3 range on Frighten and on hostile-facing presence effects (HR-15). **Never required** **(settled)** |
| Skills | 2 of Leadership, Intimidation, Medicine, Lore, Perception **(settled)**. Default **Intimidation + Medicine**: Cadence Frighten rolls Intimidation, and Medicine is close to compulsory solo (DH 8). The card says "Leadership: better recruits and prices; anyone can Rally" |
| Complexity | Involved **(settled)** |
| Concentrator | 2 of 2 **(settled)**. Terror and the Pennant's aura concentrate; no core feature does |
| Things to read | **3 at every level:** the at-wills row (Harrowing Word, the weapon); the spellbook (every tier, plus Scatter); Cadence (Frighten, Urge, Take Comfort and the subclass option). One row spare |
| Count 0 | The morale overlay (7.14), the `frighten`, `spur`, `drawSting` and `parley` turn allowances (7.8), the bonus-action tray header, the heal card's healer line |
| Limited abilities | Tiers 1–4 (4) + Cadence + Scatter = **6 of 8** |
| Not counted | The Take Comfort sub-limit **(settled)**; turn allowances (VG-10); per-rest cost waivers (SH-4); the Warcaller's per-ally round count |
| `fallbackDamageTypes` | Slashing (longsword) and piercing (javelin). Psychic is a control rider and never the fallback (classes 6.5). **Plain Speech** makes Harrowing Word bludgeoning against a psychic-immune target (HR-3) |
| `level1MechanicIds` | `harrowingWord`, `undertone`, `plainSpeech`, `faltering`, `cadence`, `cadenceFrighten`, `spur`, `rekindleHalf`, `slotSpellbook`, `moraleOverlay` |
| `signatureVerb` | `cadence`, without Take Comfort (dip guard) |
| `signatureEvents` | Frightened or Routed applied by the Herald; a Demoralise its loss event triggered; a Faltering save rolled with disadvantage; **a resolve pip spent to the Herald's fear or Dazed effect** (HR-4). Spurred grants and heals are deliberately not events |
| `seedTarget` | `cowableGroup` **(settled, CL-10)** |
| `companionMainAbility` | Wisdom **(settled)** |
| `leans[]` | 6.3 |
| Tools, not leans (SH-27 pattern) | Self-Urge (the mobility row is the Shade's); Give Ground's push (the forced-movement row is the Wrecker's); Unweaving (no cleanse lean); Take Comfort; Parry |
| Dip guards | `chassisOnly` on Take Comfort, armour, save, skill count and kit. `minClassLevel 9` on Scatter. `minClassLevel 3` on heal cards for a second class (classes 7.7 guard 4); a single-class Herald may take them from L1 **(settled)**. `upcastCap: classSlotTable` on heal cards now, `HealUpcastByClassSlots` at M4 (5.3; HR-34). `scalesBy: classLevel` on Cadence, the number of words and the Take Comfort sub-limit |

**Weaknesses on the card:** bosses, Emboldened escorts and
fearless-heavy rosters mute the morale half; mindless units blank fear
and psychic damage; bare at-will damage is modest; the bonus action is
crowded; the Focus hand has AC 14; as a companion it loses Presence.

**Level-1 identity line:** "I break them before I kill them: my word
bites the frightened, and I move my people."

### 7.2 At-wills (one reading row)

**Harrowing Word** (presence cantrip; HR-3; told plainly) **(lead's
call)**:
> **Action.** One hostile within **8** (11 with the Tipstaff's Loom) makes a
> **Will** save.
> - **On a failure:** **1d10 + Pre** psychic.
> - **On a success (Ache):** **Pre** psychic. A minion takes none
>   on a success (the Graze precedent).
> - **Faltering:** a faltering target makes this save **with
>   disadvantage**.
> - **Two words from class level 5:** two saves, against the same target
>   or two targets in range; each resolves on its own, in order, and
>   Faltering is judged per target **at each save roll**. So a Refrain
>   Frighten or an Ease the Pain that fires on word 1 can make word 2's
>   target faltering.
> - **Plain Speech:** against a psychic-immune target the word deals
>   **bludgeoning** instead, both its failure damage and Ache's
>   success damage, and attempts no rider.
> - Harrowing Word never applies Frightened and never affects an ally.
> - Its rider (Unfocused, or ending concentration) is attempted only
>   against a psychic-weak target or on a natural 1 on the save, at most
>   once per turn (ability 9.3–9.4). Under save disadvantage the natural
>   1 is read on the **kept** die, so the rider chance roughly doubles
>   against a faltering target (about 9.75% against 5%); this is
>   intended and counted in the `PayoffBound` faltering reading **(lead's
>   call)**.

**Faltering** (keyword, a derived predicate, `FlagNotCondition`). A
hostile is faltering **for a given save** if any entry that applies to
that save holds:
- it is Frightened (by anyone);
- it spent a resolve pip since the end of your last turn. Before your
  first turn of a fight, "your last turn" means combat start;
- it is in a data-listed extra case.

The predicate list is **one data record**, `faltering.predicates[]`, but
its entries reach different abilities, so each entry has the shape
`{id, condition, appliesTo[], requires?, expires?}` **(lead's call)**,
with `appliesTo` drawn from `harrowingWord`, `damageSpells`,
`spell:<id>` and `castFirstTarget`:

| Entry | Condition | `appliesTo` | `requires` / `expires` |
|---|---|---|---|
| `frightened` | Frightened by anyone | `harrowingWord`, `damageSpells` | — |
| `pipSpent` | spent a pip since the end of your last turn | `harrowingWord`, `damageSpells` | — |
| `knell` | Demoralised | `harrowingWord`, `damageSpells` | `subclass:dirge` |
| `tolled` | in your `tolled` set | `harrowingWord`, `spell:toll` | `dirge.lastToll`; the set expires at the end of your next turn |
| `falterVsProne`, `falterVsRestrained`, `falterVsChallenged` | Prone / Restrained / Challenged by an ally | `harrowingWord` | the trigger |
| `falterVsImmune` | `fear`- or `mind`-immune | `harrowingWord` | the trigger |
| `toll.vsFearImmune` | `fear`-immune, not mindless | `spell:toll` | the Dirge trigger |
| `falterOnUpcast` | the first target of an upcast damage cast | `castFirstTarget` | the trigger; that cast only |
| `drawSting`, `falterOnCure` | a source-held mark set on that hostile | `harrowingWord`, `damageSpells` | the option or trigger; `expires: endOfSourceTurn` |

It shows as one glint in the morale overlay (7.14), drawn when any entry
applies to the Harrowing Word.
- **Payoff:** disadvantage on the target's save against your Herald
  damage (the at-will and damage spells), as each entry's `appliesTo`
  allows. One setup step under `PayoffBound` ("save penalties
  included", ARC-2).
- **Why not "+1 die"** (stage 1, illustrative): a +1d4 die is about +1.25
  expected damage per word, a glint nobody feels; a visible roll ("they
  can't resist you") carries the same power; it pays *more* against the
  high-Will floor-1 boss (35% fail: 78% against 73% of the reference)
  and *less* at high fail rates, so the parity ceiling drops (11.1).
- **Fallback (lead's call):** if the simulator rerun or playtest prefers
  it, Faltering reverts to +1d4 on a failed save for the at-will and "+1
  die of its own size" for spells, which is the tested package.

**Pip timing (new lead's call, a reading of the definition).** "Since
the end of your last turn" includes the turn in progress, so a pip your
own bonus-action fear strips makes the target faltering at once, for
your action that same turn. The order that pays is therefore
**bonus-action fear first, then damage with the action** (the three
beats, 6.3); damage first wastes the payoff for that turn. The preview says "strips a pip; your word
bites it this turn". This replaces the proposal's wording ("faltering
only from the end of that turn"), which contradicted its own preview.

**Weapon attack.** The longsword (1d8 slashing one-handed, Parry) and 1
javelin (1d6 piercing, thrown 8, Pry; SK-2, M2; starting kits design
5.1). At Str 0 both are a
lockout-proof floor, not a damage plan.

### 7.3 Signature verb: Cadence (`pool.cadence`, rest): "break them before you kill them"

The sizes replace stage 1's illustrative 2 / 3 / 4 (HR-1, the ARC-11
pattern):

| Levels | 1–4 | 5–6 | 7–8 | 9 | 10 |
|---|---|---|---|---|---|
| Cadence *(illustr.)* | 4 **(agreed, Q2)** | 4 | 5 | 7 | 8 |

- `rankSchedule {core: [1, 5], other: [1, 8]}`: Urge II at L5, Frighten
  II at L8. `RankAddsNoDice` holds.
- The turn's question: **"Frighten, move or mend?"**

**Frighten** (rank I, L1):
> **Bonus action, 1 Cadence.** Make the universal Frighten (Intimidation
> against 10 + Will) against one hostile you can see within **6** (9
> with the Tipstaff).
> - A Demoralised, non-Emboldened target is **Routed** instead (T5), and
>   the roll has advantage **(settled)**.
> - Otherwise it is Frightened of you (`turns:2`, `sourceTied`).
> - The repeat rule stands (settled), widened to every fear effect
>   (HR-16b).
> - Shares the `frighten` turn allowance (`OneCadenceFrightenPerTurn`):
>   at most one Cadence Frighten of any form per turn.

- **Frighten II (L8):** range 8 (11 with the Tipstaff).
- The `cost` offer on Frighten stays excluded **(settled)**.

**Urge** (rank I, L1; a CL-5 class source):
> **No action. Cost `onEvent: ownActionResolved`, 1 Cadence, once per
> round** (turn allowance `spur`). After your action resolves on your
> turn, **you or one ally within 6** gains **Spurred**. A unit already
> holding Spurred cannot be chosen.

- **Urge II (L5):** two creatures within 6 (you may be one).
- **Heals never trigger it,** because heals are bonus actions.
- **Spurring yourself:** you use the token on your *next* turn (the
  condition text). Solo, it is pre-ticked only when an enemy you have
  not Frightened is adjacent.
- **Range 6.** Loom never widens it.
- **What counts as "your action" (new lead's call):** the action of your
  own turn, whatever it was (an at-will, a slot spell, Scatter, Dash,
  Help, a Medicine use); never a bonus action, a reaction or an
  `onHit` card on its own. A refused or illegal action resolves nothing
  and offers no Urge.
- **Order after the action (new lead's call):** (1) the action resolves
  in full, with its in-use effects (Refrain, or the Antiphon Refrain
  runs; Mourners); (2) the morale checkpoint pops its loss events (FIFO) and
  applies routs; (3) **`ownActionResolved` fires last, as a decision
  point**. Urge (or Soothe) is offered there, so `spurOnRout` and
  `sootheOnRout` read the routs of this use. Routers flee, and any
  opportunity attacks they provoke resolve, on their own turns as usual.
  Other `onEvent` triggers on the same action resolve at their own
  points before this one.
- **A decision, not an automatic spend:** the Urge target is
  pre-selected at the decision point, from static predicates (7.14), on
  the board as it now stands, so a dead or moved ally is never
  pre-selected. The hero always confirms (default as pre-ticked); the
  simulator arms take the pre-selection.
- **Earned Urge** (Urge costs no Cadence on a round your fear landed) is
  the **first desirability lever**, not M2 (11.10).

**Take Comfort** (the `restDefensiveTool`):
> **Bonus action, 1 Cadence plus its sub-limit.** Heal yourself
> FloorDiv(Recovery, 2). Your Medicine bonus does not apply.

- Self only **(settled)**. Sub-limit 1 per rest, 2 from L6 **(settled)**.
- `chassisOnly`; excluded from every offer and trigger; feeds
  `Floor1BossSustain`.
- Take Comfort is self-only, so it never makes the Herald a DH 18.1 healer.

**Global Cadence rules (lead's call):**
1. Every option costs 1 Cadence unless stated.
2. **Rest-tool reserve (the ARC-6 pattern):** a pre-ticked Urge or
   Clarion never takes Cadence below 1 while a Take Comfort use is left,
   and the rotation's Cadence Frighten rules (B2, B3, B6) obey the same
   reserve. Manual spends ignore the reserve. Nothing spends Cadence
   automatically for the hero: a pre-tick is a default the hero
   confirms.
3. A refused option refunds its Cadence.
4. Nothing refills Cadence mid-fight, routs included (a forbidden refill
   outside rests).
5. **The bonus-action sentence** (the tray header, the level-up card and
   the preview; it counts 0): "Scare first. One bonus action a turn:
   Frighten, Take Comfort, Rally, a bonus-action card, a potion or a weapon
   swap. Then your action; Urge rides it. A heal card is also your one
   slot spell this turn." The tray and the level-up card list the bonus
   action before the action. This narrows stage 1's "Mending Word and Cadence both use the
   bonus action" (HR-2, told plainly).

### 7.4 Cadence beside Rally

| Rule | Status |
|---|---|
| Rally is the universal Leadership use: a bonus action, once per ally per fight, Rallied or Spurred, never both | **(settled)** |
| No Herald record grants Rallied, refreshes Rally or changes Rally's cost (`NoHeraldRallied`). Calls never Rally "as part of the cast". The validator works over effect kinds: it fails on `condition.apply:rallied`, `rally.refresh` and any `rally.cost*`; **`rally.range` and `rally.targets` are allowed** (the Pennant's "Rally may target any ally in the aura", 9.4), since they grant nothing and leave once per ally per fight intact | Lead's call |
| Rally competes for the bonus action with Cadence Frighten, Take Comfort and bonus-action cards; Urge does not | Follows from 7.3 |
| Urge on a Rallied ally is legal: "never both" binds Rally's own output | Lead's reading; **told plainly** |
| Herald recruits cannot Rally with Presence (stat 5.4) | **(settled)** |

### 7.5 Slots and Slot Renewal

| L | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|---|---|---|---|---|---|---|---|---|---|---|
| t1 / t2 / t3 / t4 | 2 | 3 | 3/1 | 3/2 | 3/2/1 | 3/2/2 | 3/2/2/1 | 3/3/2/1 | 3/3/2/2 | 3/3/3/2 |

- One `camp` pool per tier **(settled)**.
- **Every slot cast is the turn's one camp spend.** That includes the
  bonus-action cards and the `onHit` card (Ringing Blow), so it never
  shares a turn with a scroll read, a wand charge or Scatter.
- **Slot Renewal 1 (settled):** at each short rest, the highest spent
  slot of tier 2 or lower returns. It counts as rest power (ARC-3).
- **No Slot Renewal 2;** `slotRecovery+1` is excluded for the Herald
  (HR-35). Extra recovered slots would be heal capacity in disguise. The
  exclusion lifts only if the DH 18.1 gate and the heal-isolation gate
  both pass with 3 points of margin.
- **Casts per floor** (camp fill + 2 Slot Renewals + Scatter): 4 / 5 /
  6 / 8 / 10 / 13 / 14 at L1 / 2 / 3 / 5 / 7 / 9 / 10 *(illustr.)*.

### 7.6 Concentration **(settled; restated)**

- One effect at a time. The check is d20 + Con against max(10, half the
  damage). It ends on Downed, Incapacitated or Unfocused.
- No core feature concentrates. Training comes only from an offer (the
  `chorus` lane).

### 7.7 Creation picks and the published ladder (classes 7.4, caster column)

**Kit pick (creation):**

| Kit | Set A | Set B | AC b1 / b2 / b3 (Dex +1) | Identity |
|---|---|---|---|---|
| **Shield hand** (default) | Longsword + shield (+2, bash 1d4 B Push) | 1 javelin (SK-2, M2) | 16 / 17 / 18 | Front-capable, sturdy solo; Ringing Blow's home |
| **Focus hand** | Longsword + Tipstaff (Loom) | 1 javelin (SK-2, M2) | 14 / 15 / 16 | Breaks morale from range 9–11 |

- **Starter rule:** two starters, at least one of which deals damage or
  applies Frightened. The confirm button stays disabled otherwise ("pick
  a spell that deals damage or frightens"). A heal card may be the second
  starter, never the only answer (`HeraldStarterHasAnswer`).
- **Spells known:** 2 / 3 / 4 / 4 / 5 / 5 / 6 / 6 / 6 / 7, one fewer from
  L7 if the L7 trigger option is taken, and one fewer at L10 if the offer
  is taken.
- **The L3 spell** is the subclass's own; it is never swapped. One swap
  at camp **(settled)**.
- **Spell-card draw:** 3 distinct cards by `drawWeight` **(settled)**,
  with these soft constraints:
  - (s1) at most one heal card;
  - (s2) at least one Recommended card;
  - (s3) no Expert card in the L2 draw;
  - (s4) at least one damage-bearing card when one is eligible;
  - the L7 draw holds a trigger option **(settled)**.

  `DrawSatisfiable` covers s1–s4, relaxing them in the order s2, s4,
  s3 if the eligible pool cannot satisfy all four; s1 is never relaxed
  **(new lead's call)**. `HeraldL2PlaysAlone` holds, with an allowance
  for self-target cards.
- **`Floor1ClassSpread` pick (HR-10, the ARC-15 pattern):**
  - a pick is kit × level-3 subclass, giving 4 rows;
  - starters are held at the **no-heal defaults** (told plainly,
    accepted with Q1);
  - the class mean is unweighted;
  - every legal starter combination is printed outside the mean,
    heal-card rows first.

**Published ladder:** spell cards at 2, 5, 7 and 10; the subclass (and
its tier-2 spell) at 3; +1 ability at 4 and 8 (L4 stands alone,
settled); the subclass verb change at 6; Scatter at 9, plus an offer; a
spell card or offer at 10.

### 7.8 Turn allowances and frequency limits (count 0; VG-10, SH-4)

**(lead's call)**

An allowance is a record `{id, period: turn | round}` (classes 3.4: a
round allowance is a pool of 1 refilled at the start of its owner's
turn; a turn allowance is cleared at the start of every unit's turn).

| Allowance | Covers | Period |
|---|---|---|
| `frighten` | Cadence Frighten, Antiphon, Refrain | `turn` (`OneCadenceFrightenPerTurn`) |
| `spur` | Urge, Soothe, Clarion, Colours Forward (M3) | `round` |
| `drawSting` | Ease the Pain (Hearth L6) | `round` |
| `parley` | Parley (Dirge L6) | `round` |
| Per-rest cost waivers | Antiphon III, Soothe III, Strike Now III (M3) | One per rest each (SH-4) |

- **`OneCadenceFrightenPerTurn` statically:** every option whose cost
  comes from `pool.cadence` and that carries a frighten effect kind
  (Frighten, Antiphon, Refrain) has `allowance.id = frighten` and
  `period = turn`.
- **Clarion and the first refill (new lead's call):** a `spur` use at
  `combatStart` marks the pool **spent through the end of your first
  turn**: the refill at the start of that turn is skipped. So Clarion
  consumes your first turn's Urge; Clarion, then a turn-1
  action, then Urge is refused (13.1).

No capacity or timescale offer targets an allowance (ARC-26).

### 7.9 Capstone: Scatter (`pool.scatter`, camp 1, `minClassLevel 9`; on the spellbook row)

> **Scatter. Action; the turn's camp spend.** Every hostile you can see
> takes a limited fear effect from you, through the full pipeline.
> Wards, `fear` and `mind` immunity, Rallied and Emboldened block or
> resist as usual.
> - **Demoralised foes run.** Each Demoralised hostile that is neither
>   fearless nor a boss makes a **Will save with disadvantage**. On a
>   failure it is **Routed** by T6 substitution. Routed wins.
> - **Everyone else falters.** Every other hostile in sight, **bosses
>   and fearless units included**, makes a Will save or is **Frightened
>   of you until the end of its next turn** (`turns:1`). On a boss with
>   pips, this spends a pip (HR-16a).
>
> Your subclass's rider applies (Art II, 9).

- **How the targets split (new lead's call):** the hostiles in sight are
  split **once, at declaration**. Set A: each Demoralised hostile that is
  not Emboldened, not a boss and not fearless (clause 1). Set B: every
  other hostile in sight (clause 2). Each target makes exactly **one**
  save. A target in set A that passes is unaffected: it never falls to
  clause 2. Clause 1 resolves before clause 2 inside one use, and loss
  events pop at the morale checkpoint after both, so clause 1's routs
  never change clause 2's targets.
- **Changes (HR-5, told plainly):**
  - Stage 1 says "each Demoralised enemy in sight that is not a boss".
    The second clause now reaches bosses and fearless units **without
    ever routing them**.
  - The first clause's save is this document's reading of "through the
    pipeline". It answers the early-end risk (11.9) instead of an
    automatic rout.
  - **Fallback (not needed):** the client confirmed the second clause
    on 2026-11-20 with the approval request (20.1). Had it been refused,
    the Dirge's Last Toll would have modified the first clause instead (it marks
    every hostile clause 1 targeted as `tolled`; its `turns:2` has no
    clause left to lengthen), and the Hearth's Watchfire, which modifies
    neither clause, is unchanged (9).
- **A Demoralised Emboldened foe** is not rout-ready; it is in set B and
  takes the second clause (Frightened, with advantage on its save)
  **(new lead's call, restating the substitution rule)**.
- **Bounds:** no damage, so the nova bound's damage clause is trivial;
  `CampOptionBound` passes (0 actions granted); no refill, reaction or
  second camp spend; Frightened is major, never hard; it creates no
  Demoralise (`ScatterTargets`: Demoralised comes only from T1–T4).
- **Report:** kill-equivalents at the morale scorer's 90% against the p95
  non-capstone turn (the proxy reads 2.99× against damage turns and 1.92×
  against the best morale-or-damage turn *(illustr.)*). Lever if it is
  ever gated: at most 3 routs per Scatter.
- **Tests:** in a mixed room, a Demoralised unit saves at disadvantage
  and routs on a failure; an Emboldened unit is Frightened, with
  advantage on its save; a boss loses a pip; a mindless unit is blocked;
  a Frightened, non-Demoralised unit keeps its Frightened (HR-17); a
  Demoralised unit that passes clause 1's save is neither Frightened nor
  Routed.

### 7.10 Pools by level *(illustr.)*

| Pool | L1 | L2 | L3 | L4 | L5 | L6 | L7 | L8 | L9 | L10 |
|---|---|---|---|---|---|---|---|---|---|---|
| Slots (camp) | 2 | 3 | 3/1 | 3/2 | 3/2/1 | 3/2/2 | 3/2/2/1 | 3/3/2/1 | 3/3/2/2 | 3/3/3/2 |
| Slot Renewal | 1 | 1 | 1 | 1 | 1 | 1 | 1 | 1 | 1 | 1 |
| Cadence (rest) | 4 | 4 | 4 | 4 | 4 | 4 | 5 | 5 | 7 | 8 |
| Take Comfort sub-limit (settled) | 1 | 1 | 1 | 1 | 1 | 2 | 2 | 2 | 2 | 2 |
| Scatter (camp) | — | — | — | — | — | — | — | — | 1 | 1 |
| Harrowing Word words | 1 | 1 | 1 | 1 | 2 | 2 | 2 | 2 | 2 | 2 |
| Urge / Frighten rank | I / I | I / I | I / I | I / I | II / I | II / I | II / I | II / II | II / II | II / II |

### 7.11 Starting kit *(illustr.; 2 potions settled)*

| Kit | Armour | Set A | Set B | Belt / coin |
|---|---|---|---|---|
| **Shield hand** (default) | Chain shirt (medium 13 + Dex; Ringed) | Longsword + shield | 1 javelin (SK-2, M2) | 2 healing potions, exactly 15 coin (`kit.coinStart`; SK-1, M1) |
| **Focus hand** | Chain shirt | Longsword + Tipstaff | 1 javelin (SK-2, M2) | Same |

- **The longsword, not a mace** (the draft proposal's weapon), for
  pool-free Parry (the Arcanist staff-Parry precedent). Topple is not
  available, told plainly (HR-36). On solo floor 1 the escort answer is
  the universal Shove or the shield bash's Push, both at Str 0, plus T7
  when the boss falls (7.16); Give Ground (from the L5 draw) and an
  ally's Spurred Shove come later.
- **Reaction order (policies, 7.14):** Parry is `ifItChangesOutcome`,
  `nonMinion`; otherwise the reaction stays free for opportunity attacks
  on fleeing routers.
- **Starting kits design (segment 14; approved 2026-11-23):** set B is
  **1 javelin**, not 2 (W 11.1: the javelin is not light, so it cannot
  sit in an off hand; gear never stacks, ITEMS 12.8; a thrown weapon
  stays with the thrower, W 9.3) (SK-2, M2; told plainly); coin is
  **exactly 15** (`kit.coinStart`; the camp-1 margin is 0, DH 21.5,
  LE 6.4 item 4) (SK-1, M1). The kits' named piece (Shield hand: the shield;
  Focus hand: the chain shirt), kit lines, Oddment pools and `kit.acFloor`
  (Shield hand 14, Focus hand 12 *(illustr.)*) are the starting kits design's
  (starting kits design 4.2, 4.5, 5.1). Kit variants (Macehand,
  Spearhand) ship only as boon options with one skill, never as a
  third creation pick (SK-7, M3; agreed, Q1; starting kits design 4.4,
  5.3).

### 7.12 Caster defence and sustain

**AC (armour 11 item 2 reading):**

| Build | L1–4 | L5–8 | L9–10 |
|---|---|---|---|
| Reference | 15 | 16 | 17 |
| Shield hand, Dex +1 | 16 | 17 | 18 |
| Focus hand, Dex +1 | 14 | 15 | 16 |
| Worst legal Dex −1: Shield hand / Focus hand | 14 / 12 | 15 / 13 | 16 / 14 |
| Maximum sustained (Dex +2, shield) | 17 | 18 | 19 |
| Maximum with Parry | 19 | 20 | 21 |

- Item 2 is not triggered (medium ceiling; CL-13e). AR-20's 11 is
  untouched; no guard rerun.
- The envelope passes: the maximum sits 1 below heavy + shield.
- There is no class AC and no class reaction.

**`Floor1BossSustain`** (L3; static need 48) *(illustr.)*:

| Con | Door HP | 3 potions | Take Comfort | Total | With one self Mending Word (report row only) |
|---|---|---|---|---|---|
| +1 | 24 | 33 | 5 | **62, pass** | 73 |
| −1 | 20 | 27 | 4 | **51, pass** | 60 |

It passes on Take Comfort alone. **The heal-card row never counts toward the
pass.** Report rows: at AC 14, Con −1 fails the AC-adjusted need (52.4);
with Parry it needs 44.0 and passes *(illustr.)*.

**Creation warnings** (never blocking): below Con +1; "Focus hand: AC 14,
one below the reference"; Dex −1; Intimidation untrained. From M2, a
rearrangement that Strains a kit item or drops sustained AC below the
pick's `kit.acFloor` also warns, in one line of shape plus text (SK-9,
M2; starting kits design 4.6).

**SK-2 rerun (M2):** with set B at 1 javelin, `Floor1BossSustain` and
this section's rows are rerun (starting kits design 2.3 F2). The
potions, hit points and AC above do not count javelins, so the rows
are expected to stand *(illustr.)*.

### 7.13 Offers, lanes and `triggerMenu`

| Lane | Weight | Carries |
|---|---|---|
| `voice` | 1500‰ | Intimidation and Leadership mastery; `capacity:cadence` (at most once per run); Frighten, Urge and Faltering triggers; Clarion |
| `chorus` | 1300‰ | Concentration training; the `cost` offer on a **non-heal** tier-1 spell (still a camp spend); `shape` on a fear area; spellbook triggers |
| `field` | 1200‰ | Medicine, Lore and Perception training; Perception mastery (Tactician reachable, never copied; CL-13c); Lore mastery. Medicine mastery stays at the universal 1000‰ until its signature use is designed, and is excluded if that use heals |

`offerLaneAdjust` ×1333‰: Dirge on `voice`, Hearth on `field`.

**`offerExcludes`:** the `cost` offer on Frighten **(settled)**; capacity
and cost on Take Comfort and its sub-limit; `capacity:scatter` and
`timescale:scatter`; `timescale:slots.*`; `slotRecovery+1` (HR-35);
capacity and timescale on any turn allowance; armour training past
medium; **every offer that adds heal targets, heal HP, heal range or a
heal card, or changes a heal card's cost** (`HealGrowsOnlyByTierOrHearth`).

**`triggerMenu`** (never repeated; ids illustrative; `NoHealTrigger`):

| Ability | Trigger | Text |
|---|---|---|
| harrowingWord | `falterVsProne` | A Prone target counts as faltering for you (pays off an ally's Topple or Shove) |
| harrowingWord | `falterVsRestrained` | A Restrained target counts as faltering for you |
| harrowingWord | `falterVsChallenged` | A target Challenged by an ally counts as faltering for you |
| harrowingWord | `falterVsImmune` | A `fear`- or `mind`-immune target counts as faltering for your **Harrowing Word**. Against a mindless target it pays through Plain Speech; psychic spells still deal 0 there. The first trap-room lever builds it into Plain Speech (5.4) |
| cadenceFrighten | `stareDown` | Advantage against an adjacent hostile |
| spur | `spurOnRout` | When your fear Routs a hostile, your Urge this round costs no Cadence (a cost waiver, VG-11) |
| spur | `openingCry` | At combat start, if not Surprised: `onEvent: combatStart`, 1 Cadence, you or one ally within 6 gains Spurred. It consumes your first turn's Urge: the `spur` pool stays spent through the end of your first turn (7.8). The hero gets a prompt, **unticked** by default; the arms use it only when the rest-tool reserve (7.3 rule 2) allows (HX-8) |
| spellbook | `falterOnUpcast` | An upcast damage spell's first target counts as faltering for that cast |

**Barred:** Rally refresh, or Rallied from a Herald source; Cadence
refills; any Spurred for the whole party every turn; any coupling of
heals to tempo or routs (`mendingSpur`, heal on rout); an override of
Emboldened; any second Cadence Frighten per turn.

### 7.14 Policies, `castDefaults`, `defaultRotation`, the morale overlay and previews

**Reaction policies** (ids below; on screen they print as "Always" for
`always` and "Only if it matters" for `ifItChangesOutcome`, never the
raw id; naming pass 5.9.14):
- **Parry** (longsword): `ifItChangesOutcome`, `nonMinion`.
- **Opportunity attack:** `always` otherwise; routers flee and provoke.
- **Shieldbearer recruit's Interpose:** `ifItChangesOutcome`.
- **Ringing Blow** (an `onHit` slot cast, possibly on an enemy's turn):
  the arms cast it `ifItChangesOutcome` (the bonus dice kill the target,
  or the fear lands on a rout-ready target), `nonMinion`, from the
  lowest legal slot tier; the rest-tool reserve does not apply (it spends
  slots, not Cadence). It is legal only if that turn's camp spend is
  unused. The hero gets a prompt on the hit, **unticked** by default
  **(lead's call)**.

**`castDefaults`** (static data, shown ticked; the player can untick; the
ARC-6 reading of agreed ability 11.3):
- **Frighten target:** pre-selects a rout-ready hostile (tie-break, 3.4).
  Otherwise nothing.
- **Urge** (computed at the decision point, 7.3):
  - **Eligible targets** (for any pre-selected Spurred grant: Urge,
    Rouse, Soothe): never a Downed, Prone, speed-0 or already-Spurred
    unit. A Prone or already-Spurred ally stays legal for Rouse's fear
    end and Soothe's cure only.
  - **In a party:** pre-ticked on the eligible ally adjacent to a
    hostile, lowest HP first, **only when** after paying Cadence stays
    at or above the Take Comfort reserve **plus 1** (a Frighten reserve)
    and no rout-ready hostile sits in your Frighten range. Otherwise
    shown unticked **(lead's call)**.
  - **Solo:** pre-ticked only when a hostile you have not Frightened is
    adjacent, with the same reserves.
  - **Soothe (Hearth):** the Urge becomes Soothe's cure, pre-ticked on
    an eligible creature holding a hearth-list condition, when one is in
    range.
- **Heal cards are never pre-ticked or pre-targeted.**
- The last-slot confirmation is reused unchanged (ARC-13).

**`defaultRotation`** (static; closed predicates; drives the novice and
basic arms and the ladder preview; the hero never auto-casts). The first
legal bonus-action rule runs, then the first legal action rule, then
Urge (the ARC schema, `{when, do, slotTiers, bonusAction?, requires?}`).
**No rule names a heal card** (`NoHealInDefaults`); the with-card,
card-added and free-draws arms take their heal rule from simulator
arm-policy data instead (below):

| # | When | Do |
|---|---|---|
| B1 | `hpAtMostHalf`, `rekindleAvailable` | Take Comfort |
| B2 | `routableInFrightenRange`, `cadenceAboveReserve` | Cadence Frighten (Dirge: Antiphon on a rout-ready pair) |
| B3 | `isBossFight`, `bossNotFaltering`, `notRepeatFear`, `cadenceAboveReserve` | Cadence Frighten the boss |
| B5 | `allyFrightened`, `rallyAvailableOn(ally)` | Rally (steady) |
| B6 | `nonBossActsBeforeMyNextTurn`, `cadenceAboveReserve`, `notRepeatFear` | Cadence Frighten the `highestThreat` |
| A1 | `routableInSpellRange ≥ 2` (≥ 1 solo when B2 was illegal) | `castBestFear` (tier 1; tier 2+ only in a boss fight) |
| A2 | `isBossFight` | `castBestSingle`, faltering target first (Dirge: Toll on the escort member whose fall reaches ≥ 2 Mourners) |
| H1 | `requires: subclass:hearth`; `partyMembersWithHearthListCondition ≥ 2` within the burst | Hearthsong |
| A3 | `nonBossAdjacent`, `fearSpellKnown` | `castBestFear` on it |
| A4 | — | Harrowing Word, faltering targets first, then the nearest |
| S | After the action (the decision point, 7.3) | Urge per the pre-tick (Hearth: Soothe per the pre-tick) |

(Rule ids keep their numbers; B4 moved to arm-policy data.)

**Predicates with exact static definitions (lead's call)**, added to the
closed vocabulary:
- `highestThreat`: the hostile in range with the highest expected
  damage per round from its profile's at-will (data), then the
  tie-break (3.4).
- `castBestFear`: among known fear cards legal on the target set and
  allowed `slotTiers`, the one with the most rout-ready targets in its
  area or target list, then the lowest tier, then record order.
- `castBestSingle`: among known single-target damage cards legal on the
  target and allowed `slotTiers`, the one with the highest static ‰ at
  the cast tier, then the lowest tier, then record order.
- `cadenceAboveReserve`: Cadence after paying stays at or above 1 while
  a Take Comfort use is left (7.3 rule 2).
- Every "nearest", "lowest HP" or "first" in a rule uses the tie-break
  (3.4).

**Arm-policy data (simulator only; never hero-visible; never in a
preview or the ladder) (lead's call):** `simArmPolicy.healRule` B4,
`healCardKnown`, `memberAtDeadly` → the lowest-tier heal card on that
member, inserted after B3. Only the with-card, card-added and free-draws
arms load it.

**Gated arm (consistency).** `Floor1ClassSpread` reads the **reference
arm** (the scorer; DH 19.3). B3 and the Urge pre-tick move the novice
and basic arms only; a sustain-sensitivity row with Take Comfort
unavailable is printed beside `Floor1BossSustain` (14.3). Ache,
Faltering, Parry and the Cadence size move the gated arm too (11.5).

**The morale overlay (lead's call; HR-37).** One family of enemy
markers, **at most one glint per unit**, in this priority:
1. **rout-ready** (a Frighten would rout it now);
2. **faltering**;
3. the **Frightened tether** to its source.

The **Break-chain preview** (HX-7) shows on hover only. The **Spurred
trail** (a dotted 2-square ring) shows only on its holder's turn. The
side-by-side glint shows only while targeting. The readability playtest
gate counts glints per screen with four units. Glints describe a
target's state and name no ability (the ARC 10.2 reading).

**Preview lines:**
- "Scare first: a frightened foe can't resist your word; then speak,
  then step."
- "After you act, Urge someone 2 squares."
- "Frighten a Demoralised foe and it flees."
- "Kill the leader, then Frighten: the group breaks."
- "Your word bites the frightened: they can't resist it."
- "Fearless foes still fear you; mindless ones don't, so daze or strike
  them."
- "A boss spends a pip on your fear; your word bites it this turn."
- "Repeat: resisted" / "Emboldened: disadvantage; cannot rout" /
  "Strips a pip" / "Psychic: immune (your word strikes as a blow)",
  shown only once the immunity is known; until then "Psychic:
  uncertain" (10.3).
- On a heal card only: "Uses your bonus action and your slot spell this
  turn."

### 7.15 Creation defaults: the recommended starter build

It needs no expertise and does not depend on healing **(lead's call)**.

| Choice | Default |
|---|---|
| Array | Pre +3, Wis +2, Con +1, Dex +1, Str 0, Int −1 |
| Kit | Shield hand (AC 16) |
| Skills | Intimidation + Medicine |
| Starters | **Trembling Word** (the action fear for when Cadence is spent) + **Grating Murmur** (damage that also breaks concentration). Shield hand alternative: Trembling Word + Ringing Blow |
| L2 order | Ringing Blow → Rouse → Bewilder → Recoil (Mending Word unranked) |
| L3 | **Dirge.** The Hearth is never a default; class means weigh both subclasses equally **(told plainly, accepted with Q1)** |
| L5 / L6 / L7 / L9 / L10 | Terror / Refrain / Pall / the offer draw / Pronouncement |

**`NoHealInDefaults`** covers every row above.

### 7.16 The floor-1 plan and the guarantee for the fearless boss (CL-10)

- **Before the boss (settled, CL-10):** floor 1's cowable group lies on
  the main path before the boss room, so the rout engine fires before
  the boss.
- **The boss room** (solo, levels 1–3: the Herald knows only its
  starters, the L2 tier-1 card and, at L3, its subclass spell):
  1. Bonus action (scare first): Cadence Frighten the boss. It spends a
     pip or lands; either way the boss is faltering (7.2, pip timing),
     unless it is `fear`-immune (below).
  2. Action: Harrowing Word, which the boss saves against with
     disadvantage.
  3. The escort's 2-square Emboldened aura: on floor 1 the only routes
     are the universal Shove (ability 7.2) and, on the Shield hand, the
     shield bash's Push verb, both rolled at Str 0, so the odds are
     modest and stated as such; and T7, which routs the escort when the
     boss falls. With Waver adopted, a landed Frightened on the boss
     drops the aura. Give Ground (tier 2) and an ally's Spurred Shove
     arrive only later. Without Waver the solo escort aura is largely
     unanswered on floor 1; the trap-room report prints it (5.4).
  4. Sustain: Take Comfort, potions, AC 16, Parry.
- **A `fear`-immune (not mindless) floor-1 boss** stays legal under the
  agreed rule. Cadence Frighten is blocked before resolve, so no pip is
  spent and step 1 makes nothing faltering. What remains: Bewilder (if
  known; Dazed has no tag, so it strips a pip and the boss is faltering),
  `falterVsImmune` if taken, and otherwise the at-will without
  disadvantage. The trap-room report prints a fear-immune floor-1 boss
  row; its first lever is `falterVsImmune` built into Plain Speech
  (5.4). This is a gap the content rule does not close; the client was
  told so plainly (20.1) and confirmed it on 2026-11-20.
- **Content rule (Q3, agreed; HR-21):** the floor-1 boss is never
  `mindless` and never psychic-immune. It is class-blind;
  `ClassGatedContent` is untouched. **It is motivated by one class, the
  Herald, and the client has been told so.** As data (**new lead's
  call**):
  - boss pool records carry `eligibleFloors` (the "pool per floor" of
    enemies 10.5 made a field), and the content check
    `Floor1BossMindReachable` runs on every composed boss whose
    `eligibleFloors` includes floor 1: it carries neither the `mindless`
    trait nor a psychic immunity. The generator never places any other
    boss on floor 1;
  - a content-test assertion: **every theme that can appear on floor 1
    has at least one compliant floor-1 boss**, so the generator, which
    has no fallback for bosses, never runs out (an undead theme needs a
    non-mindless leader without a psychic immunity to appear on floor 1);
  - the check reads exactly the agreed wording: a `mind` immunity alone
    (for example a fear-immune, non-mindless boss) does not fail it;
  - escort and reserve groups are not constrained by it, except that a
    theme below 75% non-fearless joins the floor-1 pool only with
    `F1EscortBreakable` (every escort and reserve group of its floor-1
    bosses drawn from non-fearless types) and a passing reference-arm
    `Floor1ClassSpread` in its split (run structure design 8.2; change
    RT-9, approved by the client 2026-11-21). **Narrowed for floors 2–5
    (M2; change BR-19, agreed by the client 2026-11-22, boss rooms Q4):**
    every F2–5 Boss Court, in every formation at every party size after
    AddCap, includes at least one non-fearless escort or reserve group
    (all members non-fearless types; `mindless` types and the patrol
    joiner never count), placed in the escort at fight start wherever
    the escort budget allows; checked by V17s (i) `BossCourtBreakable`
    (boss rooms design 4.7.4).
- **Levers if the spread misses,** in order (never healing; never asking
  the generator for a non-fearless boss):
  1. the Focus hand array default (a per-pick `suggestedArray`, HR-10b);
  2. `stareDown` built in at L1;
  3. Earned Urge;
  4. Faltering reverted to +1d4 plus 1d10 parity checks.
- **Open item (owed by the Herald owner):** the lever list above for a
  50–74% non-fearless floor-1 theme (run structure design 8.2 (c), 18).

### 7.17 Herald companions (requirements for the companions design)

**Every Herald recruit:**
- class `herald`, main ability Wisdom; sound HP 7, Will, medium
  armour; skills from Medicine, Lore and Perception;
- **no presence spells, no Frighten feature (settled)**, so non-casters
  at M2 (HR-12). The "single combined camp slot pool" rule does not
  reach them;
- they may read Herald-tagged scrolls in class (a scroll heal prints as
  its own source row);
- **no heal on any Herald kit** (`NoHealOnHeraldKit`, lead's call). This
  is stricter than classes 7.8 and makes the CL-9 "Herald recruit" heal
  row a permanent 0, printed. The "Mender" and "Steward" kits are
  rejected;
- a tempo signature.

**"Different slice" reading (HR-12):** a different slice means no
Frighten and no Take Comfort. Spurred is shared with the hero's Urge by
design (CL-5 names a Herald kit that grants Spurred).

| Kit | Gear | At-wills | Reaction | Pools | Choices |
|---|---|---|---|---|---|
| **Drummer** (the CL-5 kit) | Hide (Sure-Footed), spear | Weapon attack; Help | Opportunity attack (`nonMinion`) | `rest` 2 **Quickstep** (bonus action: one ally within 6 gains Spurred); `camp` 1 signature **Drumroll** (bonus action: up to 3 party units within 6, itself included, gain Spurred) | L3 Quickstep 3, or Drumroll range 8; L6 Quickstep on 2 allies, or Drumroll 4 targets; L9 Drumroll 2 per camp, or Quickstep 4 |
| **Shieldbearer** (the second protector class, 7.8) | Chain shirt, shield, mace | Weapon attack; Shove | Interpose (pool-free, `ifItChangesOutcome`); a Guarding shield is illegal on it (CL-7) | `rest` 2 **Guard Step** (gain Spurred yourself); `camp` 1 signature **Form Up** (up to 2 allies within 6 gain Spurred) | L3 Guard Step 3 uses, or Form Up range 8; L6 Guard Step also gives Spurred to one adjacent ally, or Form Up 3 targets; L9 Form Up 2 per camp, or Guard Step 4 uses (companions design 8.3; change CMP-25) |

- Spurred never stacks and is never granted to the whole party every
  turn.
- Recruiter's Eye (HX-10): **Adopted (companions design 9.2)**
  (change CMP-15). If the hero is trained in Leadership, each Muster
  shows one extra free card; with Leadership mastery, the player may
  also re-roll one free card once per camp. It never adds a unit, a
  hire or a slot, and never changes party weight.

### 7.18 `effectKinds` and registry

| Feature | effectKinds | Subject / trigger |
|---|---|---|
| Harrowing Word | `damage.spell.psychic`, `save.will.undertone`, `save.disadvantage:faltering`, `damage.fallback:bludgeoning{psychicImmune}`, `rider.weakOnly`, `scales.words` | hostile / Will save |
| Faltering | `predicate.faltering` (one data list) | hostile |
| Cadence Frighten | `frighten.cost:bonus`, `range.frighten`, `allowance:frighten` | hostile / Intimidation |
| Urge | `condition.apply:spurred` | self or ally within 6 / `ownActionResolved` |
| Take Comfort | `heal.recovery.half` (self) | self / bonus action |
| Scatter | `morale.rout.t6.area{save:disadvantage}`, `condition.apply:frightened.limited{turns:1}` | hostiles in sight / camp spend |
| Loom | `range+3{hostileFacing}` | own Frighten and hostile-facing presence effects |
| Drummer, Shieldbearer | `condition.apply:spurred`; `reactionAc.ally+2` | ally |

**Registry pairs, named first:**
- **Urge** against Rally's Spurred, Sound the Advance (Vanguard, on
  Onslaught), the Drummer and the rejected Spurred boots: same kind,
  different trigger, cost and subject. Passes on the Sound the Advance
  precedent.
- **Cadence Frighten** against the universal Frighten (its `cost` offer
  is excluded) and Make an Example.
- **Take Comfort** against Patch Up, Respite, Knit, Hungering and
  Stubborn Breath.
- **Harrowing Word** against Mote, the psychic rider and Fray.
  **Ache** against Graze: a different verb, a spell, a save success
  not a miss.
- **Scatter** against Terror, T7, Overchannel and Wellspring.
- **Give Ground** against the consumables Upheaval scroll (burst r1,
  pushed 2, hazard edges) and the Push weapon verb; **Recoil** against
  Upheaval's shape (burst r1 on you).
- **Loom** against Reach Out and Carry: they split by which way the
  effect faces.

### 7.19 Level table *(illustr.; reference array, both increases to Pre, Con +1)*

HP = 11 + L × (7 + Con); Recovery = 4 + 2L + Con; Take Comfort heals
FloorDiv(Recovery, 2). Spell difficulty = 8 + proficiency + Pre;
Intimidation = proficiency + Pre (trained).

| L | Floor | HP / Rec / Take Comfort | AC Shield hand / Focus hand | Prof / DC / Intim. | Slots | Cadence (Take Comfort uses) | Harrowing Word (fail / success) | Known | Automatic | Choice |
|---|---|---|---|---|---|---|---|---|---|---|
| 1 | 1 | 19 / 7 / 3 | 16 / 14 | +2 / 13 / +5 | 2 | 4 (1) | 1d10+3 / 3 | 2 | Kit; Harrowing Word, weapon; Faltering; Cadence (Frighten I, Urge I, Take Comfort ×1); overlay | **Kit + 2 starters** (the starter rule) |
| 2 | 1 | 27 / 9 / 4 | 16 / 14 | +2 / 13 / +5 | 3 | 4 (1) | same | 3 | Slots 3 | **Spell card** (no Expert) |
| 3 | 1→2 | 35 / 11 / 5 | 16 / 14 | +2 / 13 / +5 | 3/1 | 4 (1) | same | 4 | Tier 2; subclass L3 features | **Subclass** (its tier-2 spell) |
| 4 | 2 | 43 / 13 / 6 | 16 / 14 | +2 / 14 / +6 | 3/2 | 4 (1) | 1d10+4 / 4 | 4 | — | **+1 ability (settled)** |
| 5 | 2→3 | 51 / 15 / 7 | 17 / 15 | +3 / 15 / +7 | 3/2/1 | 4 (1) | **2 words** | 5 | Tier 3; Urge II; subclass rank II, Art I | **Spell card** |
| 6 | 3 | 59 / 17 / 8 | 17 / 15 | +3 / 15 / +7 | 3/2/2 | 4 (2) | same | 5 | Take Comfort ×2 (settled) | **Subclass feature** (changes a verb) |
| 7 | 3→4 | 67 / 19 / 9 | 17 / 15 | +3 / 15 / +7 | 3/2/2/1 | 5 (2) | same | 6 (5) | Tier 4; subclass rank III; area ceiling 49 | **Spell card** (holds a trigger option) |
| 8 | 4 | 75 / 21 / 10 | 17 / 15 | +3 / 16 / +8 | 3/3/2/1 | 5 (2) | 2 × (1d10+5 / 5) | 6 (5) | **Frighten II** | **+1 ability** |
| 9 | 4→5 | 83 / 23 / 11 | 18 / 16 | +4 / 17 / +9 | 3/3/2/2 | **7** (2) | same | 6 (5) | **Scatter** + subclass Art II | **Offer** (1 of 3) |
| 10 | 5 | 91 / 25 / 12 | 18 / 16 | +4 / 17 / +9 | 3/3/3/2 | **8** (2) | same | 7 (6 / 5) | — | **Spell card or offer** |

- "Known" counts spells known, the subclass spell included; brackets
  show one less from L7 if the L7 trigger option is taken, and one less
  again at L10 if the offer is taken.
- **Talents: none** (the caster column, as stage 1 7.4 draws it).
  Reshaping comes from Cadence ranks, the L7 trigger, offers and the
  subclass.

## 8. The full spell list

All entries are **M2** **(lead's call unless marked)**.

### 8.1 The contract every Herald spell obeys

- **Tradition and pools.** Presence; an action unless stated; one slot
  of its tier. **Every slot cast is the turn's one camp spend.**
- **Damage types (`HeraldDamageTypes`):** psychic, or the wielded
  weapon's physical type (Ringing Blow), or bludgeoning from collisions.
  Never radiant, necrotic or elemental. Rider-flagged psychic parts
  attempt the psychic rider; one rider per target per use (ability 9.3).
- **Faltering on damage spells:** a faltering target makes the spell's
  save with disadvantage. It is a class field (`faltering: true`), left
  out of `VariantIsScroll`'s comparison (HR-25), so **no scroll read
  ever falters**. One setup step under `PayoffBound` (≤ 3000‰).
- **Areas (`PresenceAreaSide`, HR-13).** A presence area with no damage
  part affects **one side by data**: fear and push areas hit enemies
  only; call and cure areas hit allies only. No Herald area has a damage
  part, so friendly fire (ability 8.2) never needs Whittle, and the
  area-damage row stays the Arcanist's alone. Area cap 25 squares at
  L1–6 and 49 at L7–10 (ARC-9): a burst or aura of r3 (7×7) is legal
  only from L7. The caster is never in its own areas (ARC-8), except
  presence call or cure areas whose record names the caster (Rouse,
  Hearthsong); HR-13 carries the exception to ability 8.2.
- **Loom (HR-15):** hostile-facing spells only. Ranges are written
  "8 (11)", with the Tipstaff's range in brackets.
- **Fear:** every Herald Frightened is a **limited fear effect** that
  runs the full pipeline:
  - a ward spends;
  - `fear` and `mind` immunity and Rallied block;
  - Emboldened gives advantage on the save;
  - at step 9 a Demoralised, non-Emboldened target is **Routed instead
    (T6)**;
  - Frightened is always "of you": source-tied and needing line of
    sight;
  - validators `RoutOnlyBySubstitution` and `NoClassDemoraliseTrigger`;
  - the repeat rule applies to spells (HR-16b), and the Panicked row is
    Later (HR-17).
- **Never:** ground zones, teleport or summons; AC, temporary HP,
  Rallied, Warded or Helped; any Later condition; a Rally, or any change
  to Rally's cost; a created action; **hard control** (the Herald list
  has no hard condition).
- **Heals:** exactly Recovery; tiers add targets only; never on a Downed
  unit; Withered greys the target; `camp` slots only. **Every heal
  target must be within the card's range, and each creature is healed at
  most once per cast**, for the base card and for the Hearth's widening.
- **Upcasts:** only where a record declares them, checked at the higher
  tier's arrival level (SP-2). Scrolls never upcast.
- **Which slot a spell may use (lead's call, restating ability 6.5):** a
  spell may be cast from its own tier's slot, or from a higher slot only
  where its `upcasts[]` declares that tier (a per-tier formula such as
  "+1d6 per tier" declares every higher tier). Its effect is the base
  plus the declared entries up to the slot's tier. `cast.tier` is the
  slot spent and is exposed to modifiers (Hearthkeeper reads it). So
  Mending Word from a tier-2 slot is legal through its declared empty
  tier-2 entry (5.3), and Recoil, which declares no upcast, cannot be
  cast from a tier-2 slot.
- **Conditions used:** Frightened, Dazed, Spurred, Unfocused (rider),
  Teetering (hazard edges), Withered, Bleeding and Poisoned (cure and
  heal readings), plus Routed (T6) and the Yielded disposition. **No new
  conditions; the budget of 32 is untouched.**

**Legend:** ★ Recommended; **Exp** Expert (low weight, never in the L2
draw); W = `drawWeight`; ‰ = the static `SpellTierBound` sample at
arrival level, with the Faltering figure in brackets (save disadvantage,
50% base fail) *(illustr.)*.

### 8.2 Tier 1 (from L1; reference 7.9). 8 cards

| Card | Cast | Effect | Upcast | W / ‰ | Best / worst |
|---|---|---|---|---|---|
| **Trembling Word** ★ (scroll; fear; SRD Cause Fear) | Action; one enemy within 6 (9) | Will or Frightened (`saveEnds {Will, cap 3}`); Demoralised → Routed (T6) | +1 enemy per tier | 1200 / 0 | A group whose leader just fell / mindless, Rallied |
| **Grating Murmur** ★ (scroll; SRD Dissonant Whispers) | Action; one enemy within 8 (11) | Will; 3d6 psychic, half on a success; rider-flagged | +1d6 per tier | 1200 / 997 (1163) | An enemy caster; a Frightened target / mindless |
| **Ringing Blow** ★ (smite shape) | `onHit`: when your melee weapon attack hits (an opportunity attack included), before damage; no action | +2d6 of the weapon's type (a crit doubles them); Will or Frightened of you (`saveEnds {Will, cap 2}`), a limited fear effect (T6). The fear is the spell's own effect, not a rider | +1d6 per tier | 1100 / 886 | **A mindless answer** (weapon type); the Shield hand's card / the Focus hand at range |
| **Rouse** ★ (call) | **Bonus action**; up to 3 creatures on your side within 6, you included | Each **ends Frightened** and gains Spurred. Never Rallied, never blocks fear, never touches Rally's count. Needs HR-16c | t3: every ally within 8; t4: every ally in sight | 1100 / 0 (`AllyUpliftBound` 1454‰ at L3) | After a boss roar / a turn the bonus action should rout |
| **Bewilder** (SRD Command, "halt") | Action; one enemy within 8 (11) | Will or **Dazed** `turns:1`. Dazed has no tag, so it lands on mindless units and strips a boss pip (conditions 18.3). Needs HR-18 | +1 enemy per tier | 900 / 0 | Constructs; bosses / a boss already holding a turn-part major |
| **Recoil** (fear) | Action; burst r1 on you, enemies only | Will or Frightened (`saveEnds {Will, cap 2}`); Demoralised → Routed | — | 800 / 0 | Surrounded solo / ranged enemies, mindless |
| **Mending Word** (scroll; the base class's **only** heal card; SRD Healing Word) | **Bonus action**; one creature within 6, **you included**; Loom never applies | Heals exactly its Recovery. Withered greys the target; never Downed; Bleeding ends on the HP gain | **Targets only:** t2 an entry that adds no target, t3 2, t4 3 (HR-6) | **800**, no badge / n/a | A member at Deadly when nothing better uses the bonus action / any turn a broken foe is in Frighten range |
| **Show of Force** **Exp** | Exploration only; 1 tick; a tier-1 slot | Advantage on your cow attempt against one eligible group this floor; never a retry; caps unchanged. Needs HR-22 | — | 300 / 0 | A fresh patrol before the stairs / fearless groups (ineligible) |

- **Rouse and the 0‰ trap-card flag.** It reads 0‰ under
  `SpellTierBound` and is valued by `CallUpliftReport`.
- **Ringing Blow is a camp spend on whatever turn it fires** (an enemy's
  turn included), so it shares that turn's one camp spend with nothing
  else **(new lead's call, restating ability 6.4 for an `onHit` cast)**.
  Its cast policy and the hero's on-hit prompt are in 7.14.

**Pending request (events design 4.8; EV-13; M3 if ruled):** a
Herald-lit Curiosity variant, "your Show of Force costs no slot this
floor", offered to this owner and the enemies owner. Curiosities offer
no Cow-advantage Grace and no cow, spare or interrogate menus; Show of
Force stays the slot spell above. Not ruled here; until the Herald owner
rules, the card is unchanged, and if refused the variant is not shipped
and nothing else changes (events design 14.2).

### 8.3 Tier 2 (from L3; reference 7.9). 4 drawable cards + the subclass spell

| Card | Cast | Effect | Upcast | W / ‰ |
|---|---|---|---|---|
| **Dire Tidings** ★ | Action; one enemy within 8 (11) | Will; 4d8 psychic, half on a success; rider-flagged | +1d8 per tier | 1200 / 1709 (1994) |
| **Doomsay** | Action; one enemy within 8 (11) | Will; 3d6 psychic, half on a success; **on a failure also Frightened** (`saveEnds {Will, cap 2}`); Demoralised → Routed after the damage | +1d6 per tier | 1000 / 997 (1163) + fear |
| **Give Ground** **Exp** | Action; burst r1 within 8 (11), enemies only | Will or pushed 2 squares straight away from you. Non-verb movement: collisions deal band bludgeoning; hazard edges give the Reflex save and Teetering; each target is its own hit for the 3-square sum (SP-5); Restrained, Tangled and Anchored units are not moved. A tool, not a lean | t4: burst r2 | 600 / 0 |
| **Yield Arms** **Exp** | Action; up to 2 enemies within 6 (9), each **Demoralised or Routed**; never a boss or a fearless unit; an elite only when Routed | Will or **Yields** (one floor fact per fight; experience unchanged). Not fear. Needs HR-19. **Cut if the enemies owner adopts Offer Terms (10); never both** | t4: 3 targets | 500 / 0 |

**Subclass spells** (never drawn, never swapped): **Toll** (Dirge) and
**Hearthsong** (Hearth).

**Cut: Onward** (Spurred + Helped on 3). It fails `AllyUpliftBound` at
2504–2893‰ *(illustr.)* and repeats the CO-B3 collision. **Exhort**
(Helped ×2, 700–1089‰) is held Later unless the registry reopens CO-B3.

### 8.4 Tier 3 (from L5; reference 17.3). 4 cards

| Card | Cast | Effect | Upcast | W / ‰ |
|---|---|---|---|---|
| **Terror** ★ (scroll; SRD Fear) | Action; cone L3, **enemies only** (HR-24); **concentration** | Will or Frightened (`saveEnds {Will, cap 3}`; also ends with concentration); Demoralised → Routed | — | 1300 / 0 |
| **Fallen Idol** (the boss-room tool) | Action; one enemy within 10 (13) | Will or Frightened (`saveEnds {Will, cap 3}`). **If it fails** (a pip spent counts as failing), each other member of its group within 2 makes a Will save or is Frightened (`saveEnds cap 2`). Every target: Demoralised, non-Emboldened → Routed | — | 1000 / 0 |
| **Bitter Truth** ★ | Action; up to 2 chosen enemies within 8 (11) | Will; 6d6 psychic each, half on a success; only the first target is rider-flagged | t4: +1d6 each | 1100 / 910 per target (1062) |
| **Unweaving** **Exp** (scroll; **shared with the Arcanist**; SRD Dispel Magic) | As the scroll | An enemy's spell buffs and concentration end; an ally's spell-sourced harmful conditions below hard end. A tool, not a lean | — | 400 / — |

**Upcasts available at t3:** Trembling Word (3 targets), Bewilder (3);
Doomsay (5d6), Dire Tidings (5d8; 975‰), Grating Murmur (5d6; 759‰),
Ringing Blow (4d6; 809‰); Rouse (every ally within 8); Mending Word (2
targets).

### 8.5 Tier 4 (from L7; reference 17.3). 4 cards

| Card | Cast | Effect | W / ‰ |
|---|---|---|---|
| **Pall** ★ | Action; burst r2 within 12 (15), enemies only, **no concentration** | Will or Frightened (`saveEnds {Will, cap 3}`); Demoralised → Routed. Lever if strong: concentration | 1300 / 0 |
| **Pronouncement** ★ | Action; one enemy within 8 (11) | Will; 8d8 psychic, half on a success; rider-flagged | 1200 / 1561 (1821) |
| **Lament** | Action; up to 3 chosen enemies within 10 (13) | Will; 4d6 psychic each, half on a success; **on a failure also Frightened** (`saveEnds cap 2`, T6) | 1000 / 607 per target (708) + fear |
| **Overwhelm** | Action; one enemy within 10 (13) | Will; on a failure **Dazed** `turns:1` **and** Frightened (`saveEnds cap 2`). Each runs the pipeline separately, in record order (`effects[]`: Dazed, then Frightened), so a boss can spend two pips; on a 1-pip boss the pip goes to Dazed and Frightened lands. On a mindless target only Dazed lands. On a Demoralised, non-Emboldened target the fear's substitution is checked first: it is Routed (T6) and Dazed is never applied. Not a composite lockout (Dazed is `oneOf`) | 900 / 0 |

**Upcasts available at t4:** Trembling Word (4), Bewilder (4), Give
Ground (r2), Yield Arms (3); Bitter Truth (+1d6), Dire Tidings (6d8;
1171‰), Doomsay (5d6), Ringing Blow (5d6; 1012‰), Grating Murmur (6d6;
910‰); Rouse (every ally in sight); Mending Word (3 targets).

### 8.6 Summary and recommended picks

| Tier | Fear | Damage (psychic or weapon) | Call | Unsettle / rite | Heal | Works on mindless |
|---|---|---|---|---|---|---|
| 1 | Trembling Word, Recoil, (Ringing Blow) | Grating Murmur, Ringing Blow | Rouse | Bewilder, Show of Force | Mending Word | Ringing Blow, Bewilder, Rouse |
| 2 | (Doomsay), the Dirge's Toll (fear only for the broken) | Dire Tidings, Doomsay | — | Give Ground, Yield Arms | — (the Hearth's Hearthsong cures, never heals) | Give Ground |
| 3 | Terror, Fallen Idol | Bitter Truth | (upcasts) | Unweaving | (upcast) | Unweaving |
| 4 | Pall, (Lament), (Overwhelm) | Pronouncement, Lament | (upcasts) | Overwhelm | (upcast) | Overwhelm (Dazed) |

- **Totals:** 20 drawable cards, **19 that never heal**, plus 2 subclass
  spells: 22 records, 21 that never heal. (The proposal's front summary
  said "21 drawable, 20 never heal"; the tables count 20 and 19. **Lead's
  call:** the tables stand.) If Offer Terms is adopted, Yield Arms is
  cut and the drawable count is 19. `DrawSatisfiable` holds.
- **Concentration:** Terror only. **Hard control:** none.
- **Recommended picks:** starters Trembling Word + Grating Murmur; L2
  Ringing Blow (Shield hand) or Rouse (Focus hand); L5 Terror; L7 Pall;
  L10 Pronouncement. The boss toolkit (Fallen Idol, Overwhelm, Bewilder) is the
  expert path.

**The heal lean inside the list.** One base card. Its cost and growth
are bounded as in 5.3. Its chance to appear in an L2 draw is about 54%
*(illustr.)*: visibility, not pressure. The non-healing list carries 5
pure fear cards and 4 more with a fear part (Ringing Blow, Doomsay,
Lament, Overwhelm), 7 damage cards with Faltering, 1 call, 4
mindless-proof unsettle and utility cards (Bewilder, Give Ground,
Unweaving, Overwhelm), and a boss toolkit.

### 8.7 Later reserves

| Reserve | Waits for |
|---|---|
| Heartening Cry (Bolstered) | Bolstered and the "no temporary HP" rule |
| Inspire (Inspired) | Inspired; Leadership mastery is its home |
| Unnerve (Shaken) | Shaken and T9 |
| Headlong Flight (Panicked) | Panicked and the transform row (HR-17) |
| Quiet Minds (Clear-minded) | Clear-minded |
| Hushing (Silenced; shared with the Arcanist reserve) | Silenced |
| Haunting Chorus (a fear aura) | The ability 8.4 aura ruling |
| Exhort (Helped at range) | The registry reopening CO-B3 |

**Rejected:** any area or mass heal (the consumables 16.7 cut); radiant
or necrotic Herald damage (the Votary's lane); psychic area damage (the
Arcanist's primary row); Rallied grants or a Rally inside a call; a
class Demoralise trigger.

### 8.8 `classTags` audit (consumables 16.4, 16.5; CL-12a; change HR-24)

| Item | Tier | Ruling |
|---|---|---|
| Trembling Word | 1 | **`herald`** (claimed) |
| Grating Murmur | 1 | **`herald`** (claimed); Faltering not compared (HR-25) |
| Mending Word | 1 | **`herald`** (settled); the self-target is not compared (HR-25), so the scroll keeps its own target rule as written (8.9 test 7: the scroll on self is illegal) |
| Marking Light, Withering Touch | 1 | **Later** (Votary or Hexer): radiant and necrotic would break `HeraldDamageTypes` |
| Terror | 3 | **`herald`** (claimed); the scroll row becomes **enemies only** for every reader |
| Unweaving | 3 | **`arcanist` + `herald`** (one shared record) |
| Blinding Flash, Wand of Outlining | 2, 1 | `arcanist` only (declined) |
| Wilt | 4 | **Later** (Rootwarden) |
| Every other `arcanist` mapping (Arcanist 8.10) | — | Unchanged |

**Consequences for the consumables owner:** 16.8 becomes 15 M2 scrolls;
the tier-4 pool holds only Icefall; optional lever (lead's call, not
required): a herald-tagged Pall scroll at tier 4, very rare. The
Arcanist audit is untouched apart from resolving its "+herald if listed"
and "herald if claimed" entries.

**Pending request (traps and locks design 9.2; TL-35; M3):**
Unbolting, a new tier-2 arcane and divine scroll (opens one lock or
Jammed lock without its consequence; never the Bossgate), needs a CN
16.4 `classTags` entry and this owner's sign-off, since this audit is
closed (HR-24). Not ruled here; until the Herald owner rules, the table
above is unchanged and Unbolting carries no `herald` tag.

### 8.9 Spell tests owed

1. **Pall, Fallen Idol, Recoil or Toll on a Frightened, non-Demoralised,
   Emboldened enemy** leaves its Frightened in place (HR-17).
2. **Doomsay on a Demoralised target:** the damage resolves, then
   Routed.
3. **Fallen Idol on a boss with pips:** the spent pip counts as failing,
   and the escort saves with advantage.
4. **Overwhelm:** on a 2-pip boss it spends both; on a 1-pip boss the
   pip goes to Dazed and Frightened lands; on a mindless target only
   Dazed lands; on a Demoralised, non-Emboldened target it is Routed and
   never Dazed.
5. **Give Ground on an Emboldened member:** the member is pushed out of
   the aura, and the next loss event includes it in T2.
6. **Ringing Blow on an opportunity attack** during an enemy's turn: one
   camp spend; a second that turn is refused.
7. **Mending Word targets:** Withered is greyed; Downed is illegal; self
   is legal; the *scroll* on self is illegal.
8. **Rouse on a Rallied or already-Spurred ally** changes only what it
   can.
9. **Yield Arms** is illegal on a boss, a fearless unit, or an elite
   that is not Routed.
10. **A Terror scroll read by a non-Herald** hits enemies only.
11. **Urge after Rouse or Mending Word** is unavailable (bonus action);
    after an action card it is available.
12. **A boss re-feared by spell** after its Frightened from you ended
    saves with advantage (HR-16b).
13. **Mending Word from a tier-2 slot** heals one target; with
    Hearthkeeper it heals two side by side, both within 6 (5.3, 9.2); a
    creature named twice is refused.
14. **Recoil from a tier-2 slot** is refused (no declared upcast; 8.1).

## 9. The subclasses

**Shared shape (the Arcanist 9 template) (lead's call):**
- **L3:** a signature Cadence option on the Cadence row (0 rows), ranked
  at L5 (II) and L7 (III), plus one tier-2 spell, always known.
- **L5:** Art I, that spell's tier-3 upcast.
- **L6:** a choice of 2, each changing a verb.
- **L9:** Art II, a Scatter rider that never heals HP (the Dirge's
  modifies the second clause; the Hearth's adds a party cure).
- **Passives** count 0.

No new pool (`SubclassAddsPool` passes). The cap stays at 6 of 8 and
reading at 3. **Each subclass's default path takes load off the bonus
action, never adds to it.** The one recorded exception is Parley, the
Dirge's expert L6 option, which adds a bonus-action Demand Surrender
(risk 8). Three of the four carry **zero healing**.

### 9.1 DIRGE (M2, full; the L3 default): "they break when I speak"

**Identity.** A mourner-herald whose voice tolls for the enemy. It waits
for the line to crack (a leader down, half the group lost, a last
survivor) and then breaks two at once, so their flight cracks the rest.
- **Deepens:** the morale primary through `twoTargetFrighten` (passes
  `SubclassCopiesBaseLean`).
- **Never adds a Demoralise trigger** (T1–T7 fixed): it produces more
  loss events, and the existing T2–T4 do the rest.
- **Not into:** fear durations (the Binder's lane), auras, bosses
  routing, ignoring Emboldened, cowing retries or healing.

**Card line:** "Break the line, then speak twice: two flee, and their
flight breaks the rest."

**Level 3:**

- **Antiphon** (signature Cadence option, `cadence.antiphon`):
  > **Bonus action, 1 Cadence.** Choose two hostiles within your
  > Frighten range that are **within 2 squares of each other**. Make the
  > universal Frighten against each, **one Intimidation roll per
  > target**, each with its own advantage (Demoralised) or disadvantage
  > (Emboldened, the repeat rule).
  > - Each Demoralised, non-Emboldened target the roll beats is Routed
  >   (T5).
  > - Any other target the roll beats is Frightened (`turns:2`).
  > - Shares the `frighten` allowance.
  - **Ranks:** II (L5): within 3 of each other. III (L7): once per rest,
    one Antiphon costs no Cadence (a cost waiver, SH-4). The "third
    target" draft is held as a lever-up.
  - **Two legal targets are required:** with only one hostile in range,
    or none within the spread, Antiphon is illegal and Frighten is used
    instead **(lead's call)**.
  - Both results resolve inside one use, so the checkpoint pops both
    losses in FIFO order, in target-selection order. Two routs can push
    past half losses, and T2 fires.
  - The preview shows both odds ("flees 91% / Emboldened 49%").
- **Toll** (tier-2 spell, always known; `spell.toll`):
  > **Action.** One hostile within 8 (11): Will save; **3d8 psychic**,
  > half on a success; Faltering applies on any outcome (marker,
  > AB 14.1) **(agreed 2026-11-28, roadmap D-22; M1-11 rulings 1.3)**.
  > - **Fear only for the Demoralised:** against a Demoralised,
  >   non-Emboldened target Toll is also a limited fear effect, and a
  >   failed save Routs it (T6). Otherwise Toll never Frightens its
  >   primary target.
  > - **Mourners:** if Toll drops a **non-minion** to 0 HP or Routs it,
  >   up to **3** other members of its group within **4** that can see
  >   it each make a Will save or are Frightened (`turns:1`). A
  >   Demoralised, non-Emboldened member that fails is Routed (T6).
  >   Nearest first (to the fallen target, then the tie-break, 3.4);
  >   Mourners take no damage.
  - **Timing (Q-D1, lead's call):** damage, then the primary's fear,
    then Mourners, all **inside the use, before the morale checkpoint**.
    A group broken by *this* kill is Frightened now and routed next
    turn. This keeps enemies 14.3's "no rout mid-use" and protects the
    early-end target.
  - **Bounds:** 1282‰ at L3 (1496 with Faltering) *(illustr.)*. Mourners
    carry no damage. `MournersWithinUse`: a Mourners rout never rings
    Mourners.
- **Knell** (passive glint): a Demoralised hostile counts as faltering
  for your Harrowing Word and damage spells. One entry on the Faltering
  data list, inside the static parity ceiling.
- **Terms of Surrender** (passive): with the Tipstaff in your active set,
  your Demand Surrender reaches **4** squares instead of 2. Loom does
  not stack. Targets, roll, advantage and the failure rule are unchanged
  (never a boss; an elite only when Routed). Needs HR-19.

**Art I, Passing Bell (L5; Toll's t3 upcast):** 4d8 psychic; Mourners
reach 6 squares and 4 members, Frightened `turns:2`. Tier 4: 5d8. The ‰
readings are 780 (910) at t3 and 975 (1138) at t4 *(illustr.)*. The
upcast's value is Mourners, which is honest for a morale card.

**Level 6 (choose 1):**

- **(a) Refrain** (default):
  > When a hostile **fails its save against your Harrowing Word** on
  > your turn (event `harrowingWordFailedSave`; Ache's success
  > damage never triggers it), you may pay **1 Cadence** to Frighten it
  > (or run Antiphon with it as one target) as part of that action,
  > using no bonus action.
  - It shares the **`frighten` allowance**, so it moves Frighten off the
    bonus action without adding throughput.
  - **How it resolves (lead's call):** Refrain runs the Cadence Frighten
    (or Antiphon) exactly as written: its normal range (6, 9 with the
    Tipstaff; Frighten II 8), its Intimidation roll against 10 + Will with
    Demoralised advantage, and T5 substitution. A target the word hit at
    range 7 without the Tipstaff is therefore ineligible. It resolves
    **immediately after the failed word's damage and before any later
    word in the same action**, so a Refrain Frighten on word 1 makes word
    2's target faltering. Its losses join the action's checkpoint (7.3,
    order after the action). For Antiphon, the partner is chosen at that
    moment under the normal spread rule; if no legal partner exists,
    Refrain runs a single Frighten.
  - With two words (L5+), Refrain fires on at most one failed save per
    turn, because of the allowance **(new lead's call, restating the
    allowance)**.
  - Lever: Refrain greys out on a turn you cast a heal card.
  - A cost lean beside the classes 7.5 exclusion (HR-11).
- **(b) Parley** (expert):
  > **Bonus action, 1 Cadence**, once per round (allowance `parley`):
  > Demand Surrender against a Demoralised or Routed hostile in your
  > range (2; 4 with Terms of Surrender).
  - Every enemies 14.6 rule holds. A Yield is permanent, and a Rally
    cannot undo it. HR-11 and HR-19.
  - It adds a bonus-action use, the one recorded exception to the
    subclass rule (9; risk 8). The expert takes it knowingly; the default
    is Refrain.

**Art II, Last Toll (L9; Scatter rider):**
- Scatter's second clause Frightens for `turns:2`.
- Every hostile it targeted is marked `tolled` until the end of your
  next turn (a source-held set, `FlagNotCondition`). A `tolled` hostile
  is faltering for your Harrowing Word (Plain Speech included) and Toll.
- Toll is psychic, so `tolled` pays nothing against mindless targets
  there.

**`triggerMenu`** (`requires: subclass:dirge`):

| Trigger | Text |
|---|---|
| `toll.vsFearImmune` | A `fear`-immune, non-mindless target counts as faltering for Toll |
| `surrender.afterRout` | A second Demand Surrender against a unit you Routed is rolled straight |
| `refrain.onToll` (`requires: dirge.refrain`) | Refrain may also follow Toll's failed save, still inside `frighten` |
| `antiphon.leaderPair` | When one Antiphon target is a group's leader, the other may be any member of that group in range |

**Barred:** Demoralise sources; cow retries; surrender on bosses or
healthy elites; Cadence back on a rout; longer fear durations beyond
Last Toll.

**Lane:** `offerLaneAdjust` ×1333‰ on `voice`.

**Exploits and bounds:**

| Risk | Bound |
|---|---|
| About 1.88 routs per Cadence against two Demoralised targets at L3 *(illustr.)* | Demoralised only from T1–T4; Emboldened; Rally; the early-end report run with every morale mechanic on (11.9) |
| Mourners chains | Within the use only; non-minion only; never from a Mourners rout |
| Frighten spam | The repeat rule per target, per source |
| Parley farming | Once per round; Cadence; target rules; one fact per fight |

**Levers if hot:** Antiphon spread 2 → 1; at L3–4 only one Antiphon
target may rout; Mourners 3 → 2.
**Levers if cold:** `toll.vsFearImmune` built in; Antiphon III's third
target; Mourners after the checkpoint on Passing Bell only.

**Weaknesses (card):** mindless rooms (it is mostly Toll and Plain
Speech there); boss-alone rounds give pips, not routs; Emboldened
escorts; it waits for losses; the Shield hand keeps Demand Surrender at
2; it never heals.

**Ladder:**

| L | Dirge gains |
|---|---|
| 3 | Toll; Antiphon I; Knell; Terms of Surrender |
| 5 | Antiphon II; Art I Passing Bell |
| 6 | Refrain **or** Parley |
| 7 | Antiphon III; Toll at t4 |
| 9 | Art II Last Toll |

**Data:**
- `mechanicIds`: `twoTargetFrighten`, `toll`, `mournersOnFall`, `knell`,
  `surrenderRange`, `refrain`, `parley`, `tolledMark`.
- Validators: `OneCadenceFrightenPerTurn`, `MournersWithinUse`,
  `DirgeNoDemoralise`, `SurrenderTargetsLegal`.
- effectKinds: `frighten.targets:2{spread}`,
  `fear.mourners{withinUse, nonMinion}`,
  `range.demandSurrender:4{totemActive}`,
  `frighten.cost:onEvent{harrowingWordFailedSave}`,
  `demandSurrender.cost:bonus+cadence`,
  `scatter.clause2.duration:turns2`, `predicate.faltering{tolled}`.
- Registry: Antiphon against Make an Example and boss roars; Toll
  against Doomsay and Grating Murmur; Mourners against Terror, T7 and
  Scatter; Parley against the universal Demand Surrender. All pass.
- **Signature events (Dirge):** the base list; a Mourners Frighten or
  rout counts as Frightened or Routed applied by the Herald.

### 9.2 HEARTH (M2, full; never a default): "gather to me"

**Identity.** The camp-fire keeper of the warband: a **field medic, not
a heal-bot**. It draws poison, rot and bleeding out of its people, gets
the fallen up from further away, keeps them moving, and makes a heal
card reach a huddle.
- **Leans:** the Medicine/Treat lens (**sub**) and the healing lens
  through heal cards only (**sub**). It adds no role.
- **Its niche complements the Dirge:** undead and poison floors
  (Withered from necrotic riders, bleeding claws, poison) are where
  morale is weakest, and where the Hearth is strongest.

**The design principle (lead's call; told plainly, accepted with Q1).**
**No Hearth record carries `healRecovery`** (`HearthNoHeal`). The
Hearth's only healing effect widens a heal card the player chose to
know. A Hearth without a heal card counts as a party with **no healer**
under DH 18.1. It is printed as its own row, and it is complete without
a card.

**Terms:**
- **Hearth list** (`hearthList`, a closed data list): `cond.poisoned`
  (not the lingering id), Bleeding, Withered. Later conditions join by
  data: Hobbled, Envenomed, Condemned, Calcifying stages 1–2. Never
  hard, agency, lingering, curse or control conditions.
- **Hearth cure:** ends one listed instance with no roll. It is not
  Treat, never grants Rallied and never blocks re-application.
- **Side by side:** an extra target must be within 2 squares of **at
  least one base target** chosen in the same use, and within the
  option's or card's own range from you (Soothe 6, or 8 at Soothe II;
  Mending Word 6). Encoded as `{extraTargets: 1, anchor: anyBaseTarget,
  maxDist: 2, inRange: true, effects: [...]}` (`SideBySideTarget`). The
  glint shows only while targeting.

**Level 3:**

- **Soothe** (signature Cadence option; a mode of Urge):
  > When you Urge, each creature you choose may **end one hearth-list
  > condition instead of** gaining Spurred (chosen per target). Same
  > Cadence, same `spur` allowance (once per round), range 6.
  - It rides your action, so the Hearth's signature never touches the
    bonus action.
  - It makes Urge useful on Prone allies, who cannot use Spurred.
  - A unit already holding Spurred may be chosen for Soothe's cure
    (Urge's "cannot be chosen" binds the Spurred grant only) **(new
    lead's call)**.
  - A cure with no HP may ride Urge; a heal may not (HR-7 reading).
  - **Ranks:** II (L5): range 8. III (L7): once per rest, one Soothe
    costs no Cadence (a cost waiver).
- **Hearthsong** (tier-2 spell, always known; `spell.hearthsong`; **no
  HP**):
  > **Action.** You and every ally in a burst r2 on you (allies only by
  > data) each end **one** hearth-list condition (priority: Withered,
  > Bleeding, poison) and gain **Spurred**. Spurred is greyed on a unit
  > that already holds it.
  - **Frightened is deliberately not on its list:** Rouse owns the fear
    cure, so the two cards differ by kind.
  - An action, so it never crowds the bonus action. Being an action, it
    is followed by Urge (or Soothe) as usual.
  - 0‰, so the trap-card flag watches it.
  - Bleeding ends here without an HP gain, so ending it matters.
- **Hearthkeeper** (passive):
  > **(a) Reach.** Your **Soothe** may reach one more creature side by
  > side with a chosen target; that extra creature may **only take the
  > cure**, never Spurred (`effects: [cure]`), so Urge stays one target
  > (two at Urge II) for the Hearth too. When you **cast** a Herald heal
  > card at **tier 2 or higher** while combat is active, it gains **+1
  > target**, side by side. HP per target stays exactly Recovery. Each
  > creature is healed at most once per cast.
  >
  > **(b) Cure.** Each target of your heal card also ends
  > `cond.poisoned` (the hearth-list entry; never the lingering id,
  > Envenomed or Nauseated). A heal card may target a **Withered**
  > creature: on it, the heal **ends Withered instead of healing** (the
  > radiant-heal precedent, conditions 18.5).
  - Class casts only, never scroll reads; `combatOnly` on (a).
  - **"Tier" is the slot spent** (5.3): a tier-2 slot gives Mending Word
    its tier-1 effect plus Hearthkeeper's target **(new lead's call)**.
  - **The level-up text leads with the cure reach:** "Hearthkeeper
    widens your Soothe. It also widens heal cards you choose to learn;
    you never have to learn one."
  - Heal-card targets by tier:

    | Tier | t1 | t2 | t3 | t4 |
    |---|---|---|---|---|
    | Base | 1 | 1 | 2 | 3 |
    | Hearth | 1 | 2 | 3 | 4 (party cap) |

  - "Cure first, then heal" on Withered is held as a lever-up: it raises
    heal volume on necrotic floors.
- **Firelight** (passive):
  > Your **Treat** reaches **2** squares (line of sight); it stays an
  > action. If you are not trained in Medicine, you become trained;
  > otherwise you gain another Herald list skill (the Loremaster
  > precedent).
  - This is stage 1's "Treat at range 2". The get-up is untouched at L3.

**Art I, Gather In (L5; Hearthsong's t3 upcast):** burst r2, as the
base card; each creature ends **every** hearth-list condition, which is
the upcast's gain. **Tier 4 (from L7):** burst r3, the first level the
area cap allows 49 squares (ARC-9; 8.1).

**Level 6 (choose 1):**

- **(a) Reach the Fallen** (default):
  > Your Medicine get-up reaches **2** squares, and the same action also
  > Treats that companion. It stays an action, once per companion per
  > fight (Lifeline counts), up on 1 HP and Prone, Scarred as usual.
  > Never `liftsDowned`.
  - It differs from the Lifeline Pendant by kind (a range step, not a
    cost step), so it passes the registry.
  - **With the Lifeline Pendant (lead's call):** the bundled Treat
    applies only when the get-up is taken as an action. A Lifeline
    bonus-action get-up may still use the range of 2, but it never
    Treats, so the Hearth's bar on a bonus-action Treat holds.
  - It touches agreed DH 11.2 (HR-28, told plainly).
- **(b) Ease the Pain** (expert):
  > When a hostile fails its save against your Harrowing Word (event
  > `harrowingWordFailedSave`), pay 1 Cadence, no action, once per round
  > (allowance `drawSting`). One party creature within 6 ends **every**
  > hearth-list condition **that hostile applied**. If any ended, that
  > hostile is faltering until the end of your turn (the `drawSting`
  > entry of 7.2, `expires: endOfSourceTurn`). With two words, this can
  > make word 2's target faltering.
  - It needs `sourceUnitId` on every condition instance (builders to
    confirm; the Arcanist design already adds the field, 14.1).

**Art II, Watchfire (L9; Scatter rider):** every party creature in sight
ends one hearth-list condition, or one Frightened from a hostile source.
It adds a cure to the capstone and **never HP**. It modifies neither
clause, so it is the same under the Scatter fallback (7.9). Its
Frightened end is added to conditions 18.8's ends list (HR-16c).

**`triggerMenu`** (`requires: subclass:hearth`; `NoHealTrigger`):

| Trigger | Text |
|---|---|
| `falterOnCure` | When your Soothe or Hearthsong ends a condition a hostile applied, that hostile is faltering until the end of your turn |
| `hearthsongOnAlly` | Hearthsong's burst may centre on a party creature within 6 |
| `sootheOnRout` | When your fear Routs a hostile, your Soothe this round costs no Cadence (a cost waiver, once per round) |

**Barred:** every heal-growth trigger or offer
(`HealGrowsOnlyByTierOrHearth`; Hearthkeeper is the only Hearth heal
growth); Take Comfort on allies; a cheaper get-up; a bonus-action Treat; a
second Urge per round; Rallied, Warded or Helped.

**Lane:** `offerLaneAdjust` ×1333‰ on `field`.
**`offerExcludes` (Hearth):** `cost` on the Medicine get-up; `cost` and
`shape` on Hearthsong; capacity and timescale on `drawSting`.

**Heal volume** *(illustr.; party of 4, 75% useful healing, 60%
side-by-side uptime)*:

| L | Two rests heal | Base Herald, 2 / 4 heal casts | Hearth, same casts |
|---|---|---|---|
| 3 | 120 | +25 / +50 | +30 / +59 |
| 5 | 160 | +34 / +68 | +40 / +81 |
| 10 | 248 | +56 / +112 | +68 / +135 |

That is about +4 to +9 points of floor healing over a carded base Herald
for the same slots. Every one of those slots is a fear spell not cast.
Heal-isolation proxy: +3.8 to +5.3 points (pass).

**Exploits and bounds:**
- **Pre-rest heal dumping:** `combatOnly`.
- **Get-up chains:** once per companion per fight across every route.
- **Roar immunity via Soothe:** Frightened is not on the hearth list, so
  it is impossible.
- **Tonic pricing:** one listed instance per use, paid in Cadence; the
  P3 tonic report gains a Hearth arm.
- **Withered trivialised:** "instead of healing" costs the heal; report
  Withered turns.
- **Two healers** (a carded Hearth plus a heal-kit recruit): the 2+ row
  (5.4).

**If the Hearth reads weak without a card** (the desirability report's
"Hearth, no card" row, or the Hearth's heal-card pick rate above 50%),
raise support, never HP **(lead's call)**:
1. **A condition-independent lever first:** Hearthkeeper's extra
   side-by-side creature from Soothe may also take Spurred, inside the
   `spur` allowance and watched by the party-wide Spurred report, so a
   card-less Hearth has a signature move in rooms with no hearth-list
   condition. The cure levers below cannot fix a condition-free floor;
2. Soothe II at L3;
3. Gather In's "every condition" at t2;
4. Soothe III at L5.

The hearth-list frequency report (5.4) says which of these the data
calls for.

**Weaknesses (card):** the base Herald's morale output, unaltered; weak
against rosters that apply no hearth-list conditions; Hearthkeeper's
heal half is worth nothing without a card; almost no gain on solo floor
1; the side-by-side reach wants a huddle, which meets enemy areas.

**Ladder:**

| L | Hearth gains |
|---|---|
| 3 | Soothe I; Hearthsong; Hearthkeeper; Firelight |
| 5 | Soothe II; Art I Gather In |
| 6 | Reach the Fallen **or** Ease the Pain |
| 7 | Soothe III; Gather In at t4 (burst r3) |
| 9 | Art II Watchfire |

**Data:**
- `mechanicIds`: `soothe`, `hearthsong`, `homecoming`, `hearthkeeper`,
  `hearthCure`, `firelight`, `reachTheFallen`, `drawTheSting`,
  `watchfire`.
- Data list `hearthList`.
- Validators: `HearthNoHeal`, `HearthCureList`, `SideBySideTarget`,
  `HearthkeeperClassCastOnly`.
- effectKinds: `spur.alt:cure.one{hearthList}`,
  `cure.one{hearthList}+condition.apply:spurred`,
  `spur.extraTarget{sideBySide, effects:[cure]}`,
  `heal.targets+1{tier≥2, sideBySide, combatOnly, classCast}`,
  `cure.one{cond.poisoned}`, `heal.endsWitheredInstead`,
  `treat.range:2`, `getUp.range:2+treat{actionOnly}`,
  `cure.all{hearthList, sourceIs}`,
  `scatter.rider:cure.one{hearthList | frightened.hostileSource}`.
- Registry: Soothe against the tonic, Kinward, Antidote and the cut
  Physician's Locket (passes: one instance, no roll, a closed list,
  narrower than Treat). Hearthsong against Rally's steady option, Rouse
  and Unweaving (passes by cure kind). Reach the Fallen against the
  Lifeline Pendant (a range step, not a cost step).
- **Signature events (Hearth):** the base list plus a hearth-list
  condition ended by a Hearth feature. Heals stay excluded.

### 9.3 WARCALLER (unlock aspect, M3; sketch): "when they flinch, we strike"

- **The one rule:** your fear opens a gap your allies strike through. It
  deepens ally tempo (H s). Stage 1 folded the Captain into this aspect.
- **Envelope (classes 6.3):** the ally spends **its own reaction**;
  **at-will attacks only**; `grantedBy` is set, so no class riders apply
  (Exploit applies, as weapons 6.3 states); never a created action and
  never the ally's limited ability.
- **Strike Now** (signature Cadence option):
  > **No action. Cost `onEvent: heraldFearLands`** (a hostile within 6
  > becomes Frightened of you or is Routed by you, on your turn; the pip
  > case is deliberately excluded). **1 Cadence:** one ally that can
  > reach it and still has its reaction makes one at-will attack against
  > it. Once per ally per round (counts 0).
  - Ranks: II two allies; III once per rest free.
  - Preview: "spends Brask's reaction: Interpose off this round".
  - Never auto-picks a protector whose ward is threatened.
- **War Cry** (tier-2, always known): Will or Frightened (`saveEnds
  cap 2`, T6). On a failed save, one ally may strike as above.
- **L6:** (a) **Strike or Shove** (the granted reaction may be a Shove,
  the escort answer), or (b) **Show the Way** (Strike Now also fires
  when your weapon hits; the fearless and mindless answer).
- **Art II:** each ally adjacent to a hostile that Scatter's second
  clause Frightened may strike, **capped at 2 allies**. The granted
  attacks count in Scatter's nova report.
- **Gate:** a companion-turn-time playtest and the ally-tempo reliance
  report.
- **Weaknesses:** it does nothing alone (it arrives at L3, after the
  solo floor); it drains the party's reactions.
- **Zero healing.** Its rows join the heal-isolation gate when it ships.

### 9.4 PENNANT (unlock aspect, M3; sketch; the brief's "Banner")

**"Banner" stays rejected** (classes 10).
- **The one rule:** while your Pennant flies, your tempo reaches everyone
  under it.
- **Readability:** one ring that moves with its bearer (not a ground
  zone); it never stacks; it counts 0 to read; it is the party's mirror
  of Emboldened. It is **never** a per-turn Spurred for the whole party.
- **Hoist the Pennant** (tier-2, always known; **concentration**, up to
  10 rounds):
  > An aura r2 on you.
  > (1) Once per round, **one** ally that starts its turn in the aura
  > without Spurred gains it.
  > (2) Your Rally may target any ally in the aura (`rally.targets`,
  > allowed by `NoHeraldRallied`, 7.4).
  - Art I (L5, its tier-3 upcast): your concentration checks on the
    Pennant have advantage. Tier 4 (from L7): the aura reaches r3, the
    first level the area cap allows it (ARC-9; 8.1).
- **Colours Forward** (Cadence option): rides Urge's event and its
  allowance. **2 Cadence:** every ally in the aura gains Spurred. It
  needs an `AllyUpliftBound` check before it ships.
- **L6:** (a) **Carry the Colours:** the aura is centred on an ally;
  concentration stays yours. (b) **Rush the Colours:** allies spend
  Spurred at once. It **waits on Spurred-now (HX-9)**; otherwise (a) is
  the only option.
- **Art II:** if the Pennant is up, every ally in the aura gains Spurred
  when Scatter resolves.
- **Waits on:** the ability 8.4 aura ruling; the lead's reading that one
  sustained Spurred per round is not "+speed" (the rejected Spurred
  boots).
- The fear-save advantage aura is **held** (too close to Emboldened; a
  budget ruling).
- **Weaknesses:** it uses a concentration (it cannot hold Terror); the
  ring wants bunching; it has the lowest morale output of the four.
- **Zero healing.**

### 9.5 Where morale is a trap, per subclass

| Room | Dirge | Hearth | Warcaller | Pennant |
|---|---|---|---|---|
| Cowable floors (CL-10) | **Strongest** | Base engine | Strong | Base + tempo |
| Boss room | Weak until T7; Toll, pips, Mourners on escort kills | Cures against boss afflictions | Strike or Shove | Mass Spurred |
| Mindless | **Weakest:** Plain Speech; Toll dead | **Best** on undead and poison floors | Show the Way | Tempo untouched |
| Emboldened | Disadvantage on Antiphon | — | Shove members out | — |

The Hearth's "Cures against boss afflictions" keeps its approved
meaning; Herald text never uses "Malady" (a reading; heirlooms and
curses design 4.3.5, 13).

**Report owed:** per-subclass clear rate by mindless share and by
boss-only rounds (the trap-room report, 5.4).

## 10. New mechanics considered

### 10.1 Table

**(lead's call)**

| # | Mechanic | Kind | Cost | Touches | Decision |
|---|---|---|---|---|---|
| HX-1 | **Fear spends pips:** Frightened's `controlRank` equals Dazed's | Ruling | S | Conditions 12.3, 17.3, 18.8; enemies 17.5 | **Adopted (M2), with HX-1b** (HR-16a); a ruling for the conditions owner |
| HX-1b | **The repeat rule covers every fear effect:** after a Frightened from a source ends, its later fear effects give the target advantage on the save | Ruling | S | Conditions 18.8 | **Adopted, required with HX-1** (HR-16b) |
| HX-2 | **Waver:** a Frightened leader projects no Emboldened aura | Universal | S | Enemies 14.3, 14.4; conditions 18.8 (Emboldened) | **Recommended to the enemies owner** (HR-20) |
| HX-3 | **Speak Over:** a Cadence reaction, ±2 to an initial save | Cadence option | S–M | Ability 7.3, 14.3; ARC-19; classes 7.4 L4 | **Held** (M3; the last desirability lever) |
| HX-4 | **Shout Down:** a reaction Frighten when a leader declares Command | Offer | S | Ability 14.3; enemies 14.4; classes 7.5 | **Held Later** (reaction crowding; a cost lean) |
| HX-5 | **Offer Terms:** a universal Leadership group Yield | Universal | M | Ability 7.2; enemies 14.6; stat model 7.1 | **The enemies owner's decision;** if adopted, Yield Arms is cut |
| HX-6 | **Pick the Question:** the player chooses the Yield's fact category | Universal | S | Enemies 14.6 | **Adopted (M2)** (HR-19) |
| HX-7 | **Break-chain preview** | Display | S–M | Enemies 14.10; ability 8.3 | **Adopted, required** (HR-37) |
| HX-8 | **Clarion** | Offer | S | Ability 7.1 | **Adopted (M2), `voice` lane** |
| HX-9 | **Double Time / Spurred-now** | Cast option | M | Conditions 18.4; companion turns | **Later** |
| HX-10 | **Recruiter's Eye** | Universal | S | Stat model 7.1 | **Adopted (companions design 9.2)** (CMP-15): with Leadership training, one extra free Muster card; with mastery, one free-card re-roll per camp; never a unit, hire or slot, never party weight |
| CM-1 | **Ache** | At-will | S | Classes 8.6 | **Adopted** (HR-3) |
| CM-2 | **Faltering as save disadvantage** | Keyword | S | Classes 8.6; `PayoffBound` | **Adopted;** fallback +1d4 (HR-3) |
| CM-3 | **Plain Speech** | At-will clause | S | Classes 8.6, 6.5 | **Adopted** (HR-3) |
| CM-4 | **Morale overlay family** | Display | S | Display rules | **Adopted, required** (HR-37) |
| CM-5 | Urge `onEvent` | Cost | S | Ability 7.1; classes 8.6 | **Adopted** (HR-2) |
| CM-6 | Scatter's second clause | Capstone | S | Classes 8.6 | **Adopted; told plainly** (HR-5) |
| CM-7 | Universal Frighten range 6 | Ruling | — | Ability 7.2; enemies 14.6 | **Adopted** (HR-14) |
| CM-8 | Loom hostile-facing only | Ruling | — | Weapons 17.8 | **Adopted** (HR-15) |
| TF-1 | Spurred step trail | Display | S | Conditions 18.4 display | **Adopted (inside CM-4)** |
| TF-2 | **Earned Urge** | Cost waiver | S | Classes 8.6 | **First desirability lever** |
| TF-6/7 | `AllyUpliftBound`, `SpurredSources` | Validators | S | Classes 12.2 | **Adopted** |
| HI-2 / TF-5 | `MoraleValueReport`, `AllyTempoValueReport` | Reports | M | DH 18.1; classes 12.3 | **Adopted** |
| HI-4 | Next-floor theme band at camp | UI | S | Enemies 10.6 | **Adopted** (HR-23) |
| V | Verses engine (cadence lines and choruses; its working kind names retired) | Engine | M | Classes 8.6 | **Later playtest trial;** its kinds renamed |
| — | Inspired tokens; Cutting Words against attacks; Song of Rest; Kindle; Field Dressing; a healing Hearthsong; Salve (a Cadence bonus-action Treat); Hand Up (a bonus-action get-up); heal on rout; `mendingSpur`; Rally in a call; a party-wide Spurred aura; Cadence refill on a rout; a class Demoralise trigger; Frighten ignoring Emboldened; Onward | — | — | — | **Rejected** |

### 10.2 Adopted in full: fear spends pips, with the repeat rule (HX-1, HX-1b)

**Rule (for the conditions owner):**
- Frightened's `controlRank` equals Dazed's, the default
  `resolveThreshold`.
- On a boss with pips, a fear effect that would land spends a pip
  instead, by any route. Once the pips are gone, it lands.
- **With it:** after a Frightened from a source ends, every later fear
  effect from that source gives the target advantage on its save. A
  Frighten check keeps its disadvantage **(settled)**.
- **What starts it (lead's call):** only a Frightened that landed and
  then ended. A pip spent (fear that never landed) does not start the
  repeat rule.
- **Where it is stored (lead's call):** the repeat set (source ids) is
  kept on the unit's life state, beside its resolve pips, not on the
  fight. For a boss, whose pips and phases persist across combat ends
  (enemies 17.5, A-4), the set persists with them, so a party that
  retreats and re-engages cannot reset the advantage while the pips stay
  spent.

**Why:**
- The other ruling (rank below the threshold) gives a boss disadvantage
  on every attack from round 1 off a bonus action and a rest pool. That
  makes the Herald near-required for bosses, the client's exact worry.
- Without HX-1b, spell fear could re-lock a boss every turn once its
  pips were gone.
- The floor-1 proxy barely moves either way (75.9 against 77.7
  *(illustr.)*).

**If the conditions owner declines HX-1 (lead's call):** the class still
works. A landed Frightened makes the boss faltering; a spent pip does
too. HX-1b is then still requested on its own, because the re-lock risk
exists under either ruling.

**Tests:** Frighten on a 2-pip boss: pip, pip, lands; after it ends, the
next fear spell is saved with advantage. A fear that only spent a pip
starts no repeat rule. After a retreat and a return, the re-feared boss
still saves with advantage. Bewilder + Cadence Frighten strip both
floor-1 pips in round 1: acceptable (Unyielding still downgrades hard
conditions), and reported as pips per boss fight by composition.

**HX-1 is universal.** Every hero's Frighten, the Shade's Make an
Example and scroll fear would now spend boss pips, which moves the pip
economy the Arcanist's approved evidence and `castDefaults` rest on
("strips a pip only") and every class's floor-1 boss fight. Under HX-1
and HX-1b this design therefore owes a rerun of `Floor1ClassSpread` for
all four launch classes and of the Arcanist pip-trap and novice reports
(14.3), and the HR-16 request to the conditions owner says so.

### 10.3 Adopted in full: Break-chain preview and the morale overlay (HX-7, CM-4)

- Any preview whose resolution causes loss events shows the **FIFO
  cascade** from party knowledge only. Example: "Rout Turnkey (T5) →
  losses pass half (T2): Inmates A and B Demoralised; B routs".
- It shows "aura drops" (Waver), "Fearless: Frightens only" and "Immune
  (known)". Unrevealed immunities show as "uncertain".
- On hover only, inside the overlay priority (7.14). It counts 0 to
  read. It describes outcomes of the player's own chosen action, so it
  is a preview, not a suggestion (ability 11.3).
- It is the main novice-arm lever: basic − novice reads 7.0–7.5 of 8 in
  the binding split *(illustr.)*.
- **Universal:** every party sees it, not only a Herald's.

### 10.4 Recommended to the enemies owner: Waver (HX-2)

- While a leader's Frightened penalties are active (its source in line
  of sight), its Emboldened aura is **suppressed**: members gain no
  Emboldened; Frighten has no aura disadvantage; Routed, T2 and T3 are
  no longer blocked.
- Suppression is **not an end**: no `onEnd` and no T1 (the C-18 edge
  semantics are unchanged). Members Demoralised during the gap stay so
  until a Rally.
- The aura returns when the leader stops being Frightened or loses sight
  of its source.
- **Why it matters:** it turns the boss room into a morale set piece,
  and it replaces any class override of Emboldened with a universal
  rule. It touches settled Emboldened text ("blocks Routed"), so it is
  the enemies owner's decision (HR-20).
- **Gate:** the enemies 14.3 target (under 10% of leader fights ended by
  morale before round 2), run with every adopted morale mechanic on.
- **Lever:** suppression only from round 2.
- **If declined:** the escort answer is the universal Shove or the
  shield bash's Push (solo on floor 1, at Str 0), Give Ground once drawn,
  an ally's Spurred Shove in a party, and T7 when the boss falls (7.16;
  16, risk 12).

### 10.5 Adopted: Pick the Question and Clarion (HX-6, HX-8)

- **Pick the Question (universal; HR-19):** when a fight's one floor
  fact is earned, the player chooses its category from the enemies 14.6
  list (trap, secret, the boss's Lore entry, a room's layout, the floor
  key: its carrier and its lock, F-KEY; change TL-23) (cross-document
  comparison 2026-11-23, CC-78); the priority order is the default
  selection. One fact per fight is
  unchanged.
  **Pending request (exploration and information design 4.6.4; XI-25;
  M3):** F-TREASURE joins the list after F-KEY (default priority
  unchanged), choosable through Pick the Question at most once per floor
  through Yield, inside the shared cap of 2; kill switch: above 50%
  *(illustr.)* of picks (`YieldCategoryMix`), restricted to the optional
  elite. Requested of this owner and the enemies owner (ENM 14.6). Not
  ruled here; until both owners rule, the list above is unchanged, and if
  refused the Lodestring's sources are the Tithe Bowl and a Curiosity
  gain only (exploration and information design 14.2).
- **Clarion (a `voice` offer; `triggerMenu` on Urge):** 7.13. It
  consumes your first turn's Urge: the `spur` pool stays spent through
  the end of your first turn, so the turn-start refill is skipped and it
  is never a second Urge (7.8). The hero's prompt is unticked by default,
  and the rest-tool reserve applies.

## 11. Numbers

All numbers here are *(illustr.)*: scratch arithmetic and a crude solo
floor-1 Monte Carlo, not the simulator.

### 11.1 Parity (weapons 6.4 ≤ 110%; VG-12)

Percent of the greatsword reference (7.9 / 17.3 / 19.2):

| Package | Case | L1 | L3 | L5 | L7 | L9 | L10 |
|---|---|---|---|---|---|---|---|
| Drafted (1d12 negates, +1d8) | Ceiling, 65% fail | **115** | **115** | **113** | **113** | 108 | 108 |
| Ache + Faltering +1d4 (the tested fallback) | Bare, 50% | 73 | 73 | 78 | 78 | 81 | 81 |
| | Ceiling, 60% / 65% | 99 / 104 | 99 / 104 | 102 / 106 | 102 / 106 | 102 / 106 | 102 / 106 |
| | Boss Will +5 (35% fail), faltering | 73 | 73 | 79 | 79 | 81 | 81 |
| **Ache + Faltering as save disadvantage (adopted)** | Ceiling, 50% / 60% / 65% | 90 / 96 / 99 | 90 / 96 / 99 | 94 / 100 / 102 | 94 / 100 / 102 | 95 / 100 / 102 | 95 / 100 / 102 |
| | Boss Will +5, faltering | **78** | **78** | 83 | 83 | 85 | 85 |
| 1d12 + Pre on a success, +1d4 | Ceiling, 65% fail | **112** | **112** | **114** | **114** | **113** | **113** (fails) |

- The adopted package passes at every level.
- It pays more against the high-Will boss and less at the static
  ceiling.
- The floor-averaged Dirge row (Antiphon, Knell, Refrain) can never
  exceed the static ceiling.
- The weapon fallback (longsword at Str 0) is 31% at L1, falling to 11%
  at L9: a lockout floor only.

### 11.2 Spell bounds

- Every damage record passes `SpellTierBound` (≤ 2000‰ single target)
  and `PayoffBound` (≤ 3000‰).
- With save-disadvantage Faltering, the worst case is Dire Tidings
  faltering at 60% fail, about 2096‰ against the single-target line
  *(illustr.)*. **Lead's call:** the static `SpellTierBound` reads the
  bare record (1709‰); the faltering figure is a `PayoffBound` reading
  (one setup step, ≤ 3000‰), as ARC-2 places save penalties.
- Calls read 0‰ and are reported by `CallUpliftReport` and checked by
  `AllyUpliftBound`: Rouse is 1454‰ at L3 and 1146‰ at L5 (pass);
  Onward 2504–2893‰ (fail, cut).
- **`AllyUpliftBound` defined (lead's call):** uplift‰ = 1000 × Σ over
  the creatures the record grants to, at its maximum legal target count
  (the caster included where the record names it), of each granted or
  ended condition's HP-equivalent value, ÷ the tier's single-target
  reference damage at the record's arrival level (the `SpellTierBound`
  reference). The line is **≤ 2000‰**, matching `SpellTierBound`'s
  single-target line; each upcast is read at its own arrival level
  (SP-2). The **condition value table** (Spurred, Helped, an ended
  Frightened, an ended hearth-list condition) is a tuning-file table
  *(illustr. values; the builders own them)*. The build fails on a miss.
- **Owed readings before records:** Hearthsong at L3 and L5, Gather In
  at L5 and L7 (its r3 tier-4 entry), Colours Forward (M3), and the
  Hearthkeeper Spurred lever if it is ever applied. Hearthsong grants
  Spurred to the caster and every ally in its burst, so a party of four
  is its worst case. **Lever if either fails:** Spurred only for the
  creatures it cured, then at most 3 creatures.

### 11.3 Sustain and AC

See 7.12. Static `Floor1BossSustain` passes on Take Comfort alone at both
Con values; the heal-card row never counts.

### 11.4 Morale value (solo floor 1, fix package; engine on / off)

| Theme | Clear on / off | Rounds per fight (Herald / Vanguard) | Routs / cowed per run |
|---|---|---|---|
| 100% non-fearless | 98.2 / 97.9 | 14% / 27% shorter | 2.80 / 1.58 |
| 62% non-fearless | 94.3 / 93.4 | 5% / 18% shorter | 1.99 / 1.00 |
| Fearless, not mindless | 97.5 / 97.4 | 2% / 5% shorter | 0.76 / 0.52 |
| Fully mindless encounter | 64.6 (Vanguard 98.8) | 3% longer | — |

**Reading.** The class-blind engine helps high-damage classes more,
because HP-line routs follow damage. That is why this design makes the
Herald's own payoff louder (Faltering, Plain Speech, Waver, the overlay)
and adds the desirability report.

### 11.5 `Floor1ClassSpread` (solo; basic arm; N 4000 per row; ≤ 10 points)

Comparison classes: Vanguard 99.0–99.5, Shade 93.7–94.7, Arcanist
95.9–96.8.

| Split | Herald, drafted (mean) | Fix package | Fix + Cadence 4 (adopted) | + Focus hand Dex +2 (lever) |
|---|---|---|---|---|
| 75–100% non-fearless, escort breakable | 37.7 | about 92 | — | 95.7 (spread 3.8) |
| 50–74%, breakable | 32.0 | — | — | 92.4 (6.6) |
| 75–100%, escort fearless | 35.9 | — | — | 92.5 (6.9) |
| **50–74%, escort fearless (binding)** | 30.1 | 82.8 | **85.5** | 89.1 (**10.2**); 90.4 (8.9) with the Tipstaff range modelled |
| Mindless boss (legal before Q3; not an approved split) | 8.7 | 25.8 | — | — (closed by the Q3 content rule; a Plain Speech rerun is still owed) |

**The fix package marginals** (Shield hand, 75–100%):

| Step | Clear |
|---|---|
| Drafted | 46.0 |
| + boss-Frighten rule | 50.1 |
| + no solo Urge pre-tick | 54.2 |
| + Ache (Faltering +1d4) | 92.2 |
| + longsword Parry | 96.8 |

**Honest status.**
- With Cadence 4 adopted and the Focus hand array held as a lever, the
  binding split reads **about 14 points behind on the basic arm in the
  proxy**.
- The gated arm is the **reference arm**, which is owed.
- The proxy cannot see Loom's range (+1.3), nor the save-disadvantage
  Faltering; the latter is worth about +5 parity points against the
  floor-1 boss.
- **This is risk 1 (16).** Levers in order: 7.16. Never healing.

**Heal-starter delta** (outside the mean): +0.5 to +5.1 in the approved
splits; +30 to +35 in the mindless-boss row, which the Q3 content rule
removes from floor 1.

**Mindless encounters stay legal** below the boss on every floor
(11.4: a fully mindless encounter reads 64.6 against the Vanguard's
98.8). So the `Floor1ClassSpread` print also carries a **"mindless share
≥ 50%" split** (printed, outside the gated splits), and the trap-room
report's escalation (5.4) answers it **(lead's call)**.

### 11.6 Skill expression

- **Basic − novice:** 2.2–7.5 against the limit of 8; it passes, but
  sits close in the binding split. The Break-chain preview and the
  overlay are the levers.
- **Expert − basic:** trivially ≤ 5 in the proxy; the simulator's expert
  arm is owed.
- **The damage axis is flat** (3–8%, against the Arcanist's 24–34%). The
  Herald's skill lives in morale sequencing, so its
  `SkillExpressionReport` arms add routs and Yields per fight and rounds
  saved. **The Verses engine** is the named answer if the expert arm
  stays flat.

### 11.7 The DH 18.1 toy and the three arms

See 5.4 for the toy evidence and the arm values. Reliance depends
entirely on the measured morale cut.

### 11.8 Ally tempo (HP-equivalents per floor)

| Source | L3 | L5 | L9 |
|---|---|---|---|
| Herald Urge | 8 | 42 | 95 |
| Herald calls | 23 | 40 | 55 |
| Rally's Spurred | 15 | 26 | 36 |
| Sound the Advance | 11 | 20 | 27 |
| Shade Fade (own tempo, primary) | 30 | 54 | 72 |

- **Watch:** at L9 a secondary lean overtakes the Shade's primary. The
  `LeanSpread` primary/secondary check is read at L9–10; the party-wide
  Spurred-per-ally-turn report has an escalation threshold of 0.4.
  Lever: Urge II narrows back to "one ally plus self".

### 11.9 Scatter and the early-end target

- Scatter has no damage, so the nova (damage reading) and
  `CampOptionBound` pass. Kill-equivalents read 2.99× (report).
- **The share of fights ended by morale before round 2** must be run
  with every adopted mechanic on together: Antiphon, Mourners, Scatter,
  Waver, and Offer Terms if adopted (HR-32).
- Lever order: (1) Waver from round 2; (2) Antiphon adjacent pairs; (3)
  at most 3 Scatter routs.

### 11.10 Camp share (40–60%; Take Comfort at half value) and the 60% rule

| Schedule | L1 | L3 (base / Dirge / Hearth) | L5 | L10 (base / Dirge / Hearth) | 60% rule |
|---|---|---|---|---|---|
| Stage 1 2 / 3 / 4 | 33 | 41 | 48 | 73 | pass |
| 3 / 4 / 5 / 7 | 27 | 41 / 38–40 / 41 | 48 | 62 / 55 / 61 | pass |
| **4 / 4 / 5 / 7 / 8 (adopted)** | **23** | **37 / 33 / 36** | 48 / 43 / 48 | **59 / 51 / 58** | L1 225‰; L3+5+9 increments 507‰: pass |

- **Levels 1–3 miss low: accepted (agreed, Q2; HR-31)**, as ARC-4, SH-28
  and VG-13 did: 23 at L1, about 36 at L2, 33–37 at L3 by subclass
  *(illustr.)*. Cadence stays 4 at levels 1–4, the floor-1 lever.
- **Levels 3–4 are read on the class mean over both subclasses**
  (agreed, Q2), subclass rows printed. **New lead's call:** level 4's
  class mean must reach the 40% floor; a miss there is not covered by
  the acceptance. **Level 4 estimate:** the extra tier-2 slot adds about
  4–5 points to level 3, so the class mean reads **about 38–40**
  *(illustr.)*, at or just under the line. Honestly, **no lever exists
  at L4** inside the agreed Cadence 4 and the settled slot table (the
  lever order below is for L10 reading high). A measured L4 miss
  therefore goes straight to the client (20.2), who would be asked
  whether to fold level 4 into the Q2 acceptance.
- The Hearth's Soothe adds rest value (slightly lower camp share from
  L3).
- Earned Urge, Speak Over (+1) and Verses (+1–3) are excluded from M2 on
  this basis too.
- **Lever order if L10 reads high:** Frighten re-weighting; Cadence 9 at
  L10. Slot Renewal 2 only with 3 points of healer margin.

**Desirability levers** (on a miss in 5.4; **never healing**), in
order:
1. Earned Urge;
2. Faltering reach (a `triggerMenu` entry made automatic);
3. Waver (a request to the enemies owner, if not already adopted);
4. Speak Over at L4.

### 11.11 Cap and reading

- 6 of 8 for the base class and every subclass, the sketches included.
- Reading is 3. Ache, Plain Speech, Knell, Hearthkeeper and every
  allowance count 0.

## 12. No M1 slice; the M2 cut

**(lead's call)** The Herald is an M2 class. **M1 needs nothing** from
this document: no M1 rule is written for the Herald, and none of the
engine work below is on the M1 path.

**The M2 Herald, levels 1–10:**
- the whole core: chassis; Harrowing Word with Ache, Faltering and
  Plain Speech; Cadence (Frighten, Urge, Take Comfort) with its ranks and
  global rules; slots and Slot Renewal; concentration as settled;
  Scatter with both clauses; offers, lanes and `triggerMenu` (Clarion included);
- the full spell list (8.2–8.5) and the `classTags` mapping (8.8);
- the morale overlay and the Break-chain preview; Pick the Question;
- `castDefaults`, `defaultRotation`, the creation defaults and previews;
- the heal-isolation gate (**agreed**, Q1) and every report in 5.4;
- every validator in 14.2;
- **two subclasses** (classes 13, M2 row): the Dirge and the Hearth;
- the Drummer and Shieldbearer recruit requirements (7.17).

**Conditions the Herald touches:** applies Frightened, Dazed, Spurred,
Unfocused (rider), Teetering (hazard edge), Routed (T5, T6) and the
Yielded disposition; ends Frightened (Rouse, Watchfire), Bleeding,
Poisoned and Withered (the Hearth); reads Demoralised, Emboldened,
Rallied, Prone, Restrained, Challenged, Concentrating and resolve pips.
No new condition.

## 13. Binding requirements and how they are met

| Requirement (source) | How met | Status |
|---|---|---|
| Full table L1–10 (classes 14) | 7.19 | Met |
| Signature verb, published ladder, reading ≤ 4, cap of eight (classes 7) | Cadence; 7.7; reading 3; 6 of 8 | Met |
| Lean, never require; forbidden grants (classes 6) | 5.2, 5.5; no created actions, no Rallied, no +speed | Met |
| `LeanSpread`, `LeanWithoutBaseline` (classes 6.4) | Morale H + the Shade lens = 2; control A P / H s; tools recorded as tools | Met |
| HP tier; `restDefensiveTool` ≤ half Recovery (DH 7, 12.1) | Sound 7; Take Comfort half, self, sub-limit | Met |
| Heals exactly Recovery; higher tiers add targets (DH 12.2) | Mending Word 1 / 1 / 2 / 3 (HR-6 reading) | Met |
| Mending Word may target the Herald (classes 8.6) | Self allowed; `VariantIsScroll` exception (HR-25) | Met |
| At most one healing-lean class; gap ≤ 10 (DH 18.1, **agreed** Q10) | Herald, cards only; gate unchanged; reports beside it | Met; owed to the simulator |
| No-healer arm at least a quarter (DH 18.2, **agreed** Q11) | Calibration target, reported | Owed |
| Three Herald arms, healer count, heal-source breakdown (CL-9) | 5.4, policy-forced, per subclass, plus the free-draws and card-added arms | Met |
| **Heal isolation ≤ 10 points per subclass (agreed, Q1)** | 5.4; HR-38 | **New gate; owed to the simulator** |
| **Reliance and desirability reports with escalation (agreed, Q1)** | 5.4 | Owed |
| `Floor1BossSustain` with and without a heal card (DH 21.1; classes 8.6) | 7.12 | Met |
| `Floor1ClassSpread` (classes 5.6) | No-heal default, kit × subclass (HR-10) | **Owed; at risk (16)** |
| Floor-1 deaths under about 5% per class (DH 18.2) | Reference arm, as DH 19.3 | Report owed |
| Parity ≤ 110% (weapons 6.4; VG-12) | 11.1 | Met *(illustr.)* |
| `SpellTierBound`, `PayoffBound`, `CampOptionBound` (ARC-2) | 11.2; Scatter | Met |
| Camp share 40–60%; 60% rule (ability 6.3; classes 8.2) | 11.10 | L1–3 miss accepted (**agreed**, Q2; HR-31); L4–10 owed |
| `SkillExpressionReport`, four arms (Arcanist 5.4) | Morale columns added (11.6) | Owed |
| Dip guards (classes 7.7) | 7.1 | Met |
| Offers, lanes, excludes, `triggerMenu`, policies, static `castDefaults` and `defaultRotation`, creation defaults (classes 7, 12) | 7.13–7.15 | Met |
| The hero never gets suggestions (ability 11.3, **agreed**) | `castDefaults` static, shown ticked, untickable; glints name no ability; the Break-chain preview describes the player's own action | Met (the ARC-6 reading) |
| `effectKinds` and registry (trinkets 11, 23) | 7.18, 9; Hand Up dropped (rule 1) | Met; rulings owed (19) |
| Companions: Wisdom main, no Presence spells or Frighten, tempo signature, protector kit on two classes (classes 7.8) | 7.17 | Met |
| Spurred class source; Rally unchanged (CL-5; stat 7.1) | Urge; `NoHeraldRallied` | Met |
| T6 for fear spells; no class Demoralise trigger (CL-1; enemies 14.3) | `RoutOnlyBySubstitution`, `NoClassDemoraliseTrigger` | Met |
| Floor-1 cowable group before the boss (CL-10) | 7.16 | Met |
| Capstone camp 1 at L9; changes the primary lean; nova ≤ 2.0 (ability 6.7; classes 14) | Scatter 7.9 | Met (report owed) |
| `classTags` audit (consumables 16.4–16.5) | 8.8 | Met (HR-24) |
| Fallback damage type (classes 6.5) | Weapon types; Plain Speech | Met (HR-3) |
| Concentrator 2; Tipstaff never required (classes 8.6) | 7.1 | Met |
| Caster AC (AR-20; armour 11) | 7.12 | Met |
| `signatureEvents`, `seedTarget` (classes 12.1) | 7.1 | Met (HR-4) |
| Composite-lockout test with Spurred (classes 14) | 13.1 | Owed (test) |
| Pennant, not Banner (classes 10) | 9.4 | Met |

### 13.1 Test additions

- Ache deals nothing to a minion on a success.
- Refrain and Ease the Pain fire only on a failed save; Refrain at most
  once per turn with two words.
- Plain Speech on a psychic-immune target deals bludgeoning with no
  rider.
- Urge never fires from a bonus action or a reaction, is greyed on an
  already-Spurred unit, and is offered after any own-turn action.
- Soothe may cure an already-Spurred unit.
- The composite-lockout test includes Urge's move after a Bar the Way
  stop.
- Scatter in a mixed room (7.9).
- HX-1 / HX-1b on a 2-pip boss (10.2).
- The HR-17 Panicked case.
- Faltering from a pip stripped earlier in the same turn (7.2), and
  before the Herald's first turn (from combat start).
- With two words, a Refrain Frighten or an Ease the Pain on word 1
  makes word 2's target faltering (7.2, 9.1, 9.2).
- Refrain against a target the word hit at range 7 without the Tipstaff is
  refused (9.1); Antiphon with one legal target is refused (9.1).
- Clarion, then a turn-1 action, then Urge: the Urge is refused
  (7.8).
- Urge after a kill whose loss events rout a group: `spurOnRout` waives
  the cost. Urge after the pre-selected ally died during the action:
  the pre-selection moves to the next eligible creature (7.3, 7.14).
- The Urge pre-tick never names a Downed, Prone, speed-0 or
  already-Spurred unit (7.14).
- Mending Word from a tier-2 slot, with and without Hearthkeeper;
  Recoil from a tier-2 slot is refused (8.1).
- Hearthkeeper's extra Soothe creature can take the cure but not
  Spurred (9.2).
- The level-up card and the tray list the bonus action before the
  action (7.3 rule 5).
- No `defaultRotation` rule or preview line names a heal card; B4 loads
  only in the with-card, card-added and free-draws arms (7.14).
- The spell tests in 8.9.
- `Floor1BossMindReachable` rejects a mindless or psychic-immune boss
  placed on floor 1, accepts a fear-immune non-mindless one, and every
  theme able to appear on floor 1 has a compliant floor-1 boss (7.16).
- The arm construction: every recruit draw is the same on the same
  seeds in every arm, except a Herald kit re-rolled on the dedicated
  substream in the no-Herald arm (5.4).

## 14. Data, validation and simulator

### 14.1 Record fields added

**(lead's call; illustrative shape)** Beside the classes 12.1 record and
the Vanguard, Shade and Arcanist designs' fields:
- on the class: `castDefaults`, `defaultRotation` (rules `{when, do,
  slotTiers, bonusAction?, requires?}` over the closed predicate
  vocabulary of 7.14), `recommendedPickOrder`, `creationWarnings[]`, `rankSchedule`,
  `fallbackDamageTypes`, `kitPicks[]` (Shield hand, Focus hand);
- on spell records: `drawWeight`, `badge` (`recommended` | `expert`),
  `upcasts[{tier, adds}]` (an entry may add nothing, 8.1),
  `concentration`, `riderFlag`, `faltering`, `presenceAreaSide`
  (`enemies` | `allies`), `healCard`, `upcastCap` (`classSlotTable` on
  every heal card), ordered `effects[]`, `srd` (the SRD 5.2.1 entry
  cited);
- on casts: `cast.tier` (the slot spent), exposed to modifiers;
- on option records: `cost.onEvent` (`ownActionResolved`,
  `harrowingWordFailedSave`, `combatStart`, `heraldFearLands`),
  `allowance` as a record `{id, period: turn | round}` with ids
  `frighten`, `spur`, `drawSting`, `parley`, `reserveForRestTool`;
- the data list `faltering.predicates[]` (one record; entries
  `{id, condition, appliesTo[], requires?, expires?}`, 7.2) and
  `hearthList`;
- on condition instances: `sourceUnitId` (already in the Arcanist
  design, 14.1);
- source-held state: `tolled`; the `drawSting` and `falterOnCure` marks;
- on each unit's life state: the HX-1b repeat set, beside its pips
  (10.2);
- on subclasses: `offerLaneAdjust`, `artId`, `scatterRiderId`;
- on boss pool records: `eligibleFloors`, read by
  `Floor1BossMindReachable` (7.16; HR-21);
- simulator arm-policy data: `simArmPolicy.healRule` (B4, 7.14), never
  hero-visible;
- in the tuning file: the heal-isolation gate line (10 points) and the
  lead's target (5); the reliance (10), desirability (+2; 50%), 2+
  healer (5), party-Spurred (0.4), Urge Cadence share (50%) and
  trap-room (10) thresholds; the DH 18.1 snapshot reading (per-fight
  kit, any fight qualifies, 5.4); the `AllyUpliftBound` line (2000‰)
  and condition value table (11.2); the novice-response probabilities
  reused from the Arcanist design.

### 14.2 Content validation

The build fails when any of these fail (VG-11): `HeraldDamageTypes`,
`RoutOnlyBySubstitution`, `NoClassDemoraliseTrigger`, `ScatterTargets`,
`NoHeraldRallied`, `SpurredSources`, `AllyUpliftBound`,
`PresenceAreaSide`, `LoomHostileOnly`, `OneCadenceFrightenPerTurn`,
`NoHealInDefaults`, `NoHealTrigger`, `HealGrowsOnlyByTierOrHearth`,
`NoHealOnHeraldKit`, `HeraldHealSources` (`healRecovery` only on Mending
Word and Take Comfort), `HearthNoHeal`, `HearthCureList`, `SideBySideTarget`,
`HearthkeeperClassCastOnly`, `HealUpcastCapDeclared` (every
`healCard: true` record carries `upcastCap: classSlotTable`),
`HeraldStarterHasAnswer`, `HeraldL2PlaysAlone`, `DrawSatisfiable`
(s1–s4), `MournersWithinUse`, `DirgeNoDemoralise`,
`SurrenderTargetsLegal`, `YieldTargetsOnlyBroken`,
`Floor1BossMindReachable` (HR-21); the Arcanist checks that apply
(`SpellTierBound`, `PayoffBound`, `CampOptionBound` as scoped by ARC-2,
`VariantIsScroll` with HR-25, `AreaCapByLevel`); the Vanguard checks as
written (`TurnAllowanceNoDamage`, `FlagNotCondition`, `RankAddsNoDice`,
`RankKeepsBounds`); and the stage-1 checks (`ForbiddenClassGrant`,
`LeanSpread`, `LeanWithoutBaseline`, `SubclassCopiesBaseLean`,
`SubclassAddsPool`, `NameCollision`, the cap of eight, the copy
registry).

**(2026-11-23; SV-20, seeds and validation design 5.1–5.2; lead's call;
M1 / M2)** These are pre-generation content checks: they keep their HR
and classes codes, have no V id and are registered as `content.*` rows
(classes 12.2 with the HR codes), all build-phase. `NameCollision` is
owned by `V7.NameRegistry` (manifest-driven); the Herald's names feed
that registry, and this document calls the check rather than owning it.
`Floor1BossMindReachable` (HR-21) is owned and run by V17s; V2 checks
coverage only.

**Not a content check:** `HealUpcastByClassSlots` is a runtime cast
legality rule (5.3), enabled at M4 when multiclassing ships; its data
half is `HealUpcastCapDeclared` above.

**How the Herald-specific checks work (new lead's call):**
- **`HearthNoHeal`** fails if any record whose `requires` includes
  `subclass:hearth` carries a `healRecovery` effect kind (Hearthkeeper's
  `heal.targets+1` modifies a heal card's targets and is not a
  `healRecovery` effect).
- **`NoHealInDefaults`** fails if any id in `castDefaults`,
  `defaultRotation`, any preview or ladder line, `recommendedPickOrder`,
  the creation defaults or a `recommended` badge resolves to a record
  with `healCard: true`; and if the creation defaults,
  `recommendedPickOrder`, the L3 default or any badge resolves to
  `subclass:hearth`. `defaultRotation` rules carrying
  `requires: subclass:hearth` are legal when they name only non-heal
  records (Soothe, Hearthsong; rule H1). The heal rule B4 lives in
  `simArmPolicy`, which is simulator-only and never hero-visible, so no
  exemption is needed.
- **`NoHeraldRallied`** fails on the effect kinds
  `condition.apply:rallied`, `rally.refresh` and any `rally.cost*`;
  `rally.range` and `rally.targets` pass (7.4).
- **`AllyUpliftBound`** as defined in 11.2 (≤ 2000‰).
- **`OneCadenceFrightenPerTurn`** as defined in 7.8.
- **`SideBySideTarget`** checks the encoding of 9.2 (anchor any base
  target, 2 squares, in range, the extra target's effects listed).

### 14.3 Simulator reports owed

- **The heal-isolation gate** per subclass (5.4; HR-38).
- **The reliance and desirability reports** with their escalation
  thresholds (5.4).
- The Herald arms (no Herald, without, with, free draws, card added;
  5.4), the healer classification columns, the healer count with the 2+
  escalation, the heal-source breakdown, heal telemetry (pick rate on
  the free-draws arm, class and per subclass), the heal-starter delta.
- `MoraleValueReport`, `AllyTempoValueReport` (with the party-wide
  Spurred escalation and the Cadence spend per option).
- The trap-room report by mindless, fear-immune and fearless share, with
  boss-only rounds split out, per subclass, with the fear-immune
  floor-1 boss row, the solo escort-aura row and its escalation (5.4).
- The hearth-list frequency report, per floor and theme.
- **Under HX-1 and HX-1b:** a rerun of `Floor1ClassSpread` for all four
  launch classes and of the Arcanist pip-trap and novice reports (10.2).
- `Floor1ClassSpread` per kit × subclass on the no-heal defaults, every
  legal starter combination printed (heal-card rows first); the per-class
  floor-1 death share against DH 18.2 (reference arm).
- The per-round breakdown of the solo boss (classes 5.6): signature
  share in rounds 2+ of the floor-1 boss fight, by level 1–3 and
  subclass.
- `SkillExpressionReport` with the morale columns (11.6).
- Parity at the worst legal build, per subclass; the capstone report;
  the early-end target with every morale mechanic on (HR-32).
- Camp share under the three measures, L3–4 on the class mean, and the
  60% rule.
- `Floor1BossSustain` sensitivity (Con −1, AC 14, Take Comfort unavailable).
- Pips spent per boss fight by composition (10.2).
- The P3 tonic report with a Hearth arm.

**(change SV-22; M2)** The `Skill` arms `Novice`, `Basic` and `Expert`
are generalised from arcanist 5.4 to the Herald (request to the ARC
owner; fallback: the Herald owner writes a novice script by the M2
planning gate); the Herald arms above keep their constructions
(seeds and validation design 6.2).

## 15. Staging

**(lead's call)**

| Wave | Content |
|---|---|
| **M1** | Nothing. No M1 rule is needed for the Herald |
| **M2** | Section 12: the full core; the 20-card list; Scatter with both clauses; the Dirge and the Hearth in full; the Drummer and Shieldbearer kit requirements; offers and triggers (Clarion); the morale overlay and Break-chain preview; Pick the Question; the heal-isolation gate and every report; all validators; the `classTags` mapping. **Needs CL-1** (approved and applied) |
| **M2 prerequisites owed by other owners, before records** | HR-16 and HR-17 (conditions owner); HR-14 (ability owner); HR-18, HR-19, HR-22 (conditions and enemies owners); HR-21, the content rule (enemies owner, **agreed** Q3); HR-24 (consumables owner); `sourceUnitId` on instances (Ease the Pain); the SRD check |
| **M2, the owner's decision** | Waver (HR-20); Offer Terms (Yield Arms cut if adopted) |
| **M2 cut fallbacks** | Ease the Pain waits on `sourceUnitId` (Reach the Fallen alone ships); Yield Arms waits on HR-19 (cut if it slips); if HX-1 is declined, the class ships on the other ruling with HX-1b (10.2) |
| **M3** | Warcaller (playtest gate); Pennant (after the 8.4 aura ruling); Speak Over as the last held desirability lever |
| **M4** | The runtime `HealUpcastByClassSlots` check and the multiclass dip guards in effect (the `upcastCap` data and `HealUpcastCapDeclared` ship at M2) |
| **Later** | The Verses engine trial; Double Time / Spurred-now (then Rush the Colours); Exhort (if CO-B3 is reopened); Shout Down; the 8.7 reserves; "Bring Them Home" |

## 16. Risks

| # | Risk | Mitigation, report and lever |
|---|---|---|
| 1 | **Floor-1 spread** misses in the binding split (about 14 points behind on the basic proxy arm with Cadence 4) | The reference arm is owed; the 7.16 levers in order; never healing; back to the client if they run out (classes 5.6) |
| 2 | **Morale under-delivers**, so players heal to feel useful (reliance by the side door) | The desirability report with its escalation (**agreed**, Q1); louder payoff (Faltering, Plain Speech, the overlay); Waver; the 11.10 levers, never healing |
| 3 | **Heal isolation or the healer gap runs hot** (a carded Hearth plus a heal-kit recruit) | The heal-isolation gate (**agreed**, Q1); the 2+ row read first; the healing lever order (5.4) |
| 4 | **Reliance runs hot** once morale is measured | The reliance report with its lever order (5.4); the window +2 to +10 keeps the levers from fighting |
| 5 | Morale ends fights too early once Antiphon, Mourners, Scatter and Waver stack | The early-end report with all mechanics on; levers (11.9) |
| 6 | Boss fear-lock | HX-1b (HR-16b), required with HX-1, requested alone if HX-1 is declined |
| 7 | Mindless-skewed encounters mute the class (a fully mindless encounter about 34 points behind the Vanguard in the proxy) | Plain Speech, the non-psychic cards, the HI-4 camp line, the Q3 content rule (**agreed**); the trap-room report with its escalation, first lever `falterVsImmune` built into Plain Speech (5.4) |
| 8 | Bonus-action and glint load on an Involved-complexity class (novice arm 7.0–7.5 of 8) | Urge off the bonus action; the subclasses' default paths unload it (Parley, the Dirge's expert option, is the one exception that adds a bonus-action use); Hearthsong an action; one overlay; Break-chain on hover |
| 9 | Spurred aggregate (a secondary beating a primary at L9) | The party-wide Spurred report; Urge II narrows back to "one ally plus self" |
| 10 | Camp share L1–3 low (accepted, **agreed** Q2), L4 and L10 at the edges | L4 read on the class mean against 40%; Cadence 8 at L10; levers (11.10) |
| 11 | Reaction crowding (Parry against opportunity attacks on routers) | Policy order; morale-value rerun; Speak Over and Shout Down held |
| 12 | Owners decline HR-16 or HR-20 | The class works under either F5 ruling. Without Waver the escort answer is Shove or the shield bash's Push (at Str 0), Give Ground once drawn, a Spurred Shove in a party, and T7; **solo on floor 1 the escort aura is largely unanswered**, printed in the trap-room report |
| 13 | Multiclass heal dip (M4) | `HealUpcastByClassSlots` |
| 14 | Faltering as disadvantage reads worse in the rerun | Fallback +1d4 (the tested package) |
| 15 | **All evidence is proxy** | Every number is owed a simulator rerun before it settles |
| 16 | **Recall-based survey and SRD shapes** | Check against SRD 5.2.1 before data |

## 17. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: pool sizes inside the shape of 7.10,
  Harrowing Word dice inside the parity rows, spell dice and areas
  inside `SpellTierBound`, `drawWeight`, lane weights and
  `offerLaneAdjust`, trigger ids, report thresholds
  other than the agreed ones, and lever sizes. The starting coin is no
  longer tuning: exactly 15 (starting kits design 4.2; change SK-1).
- Level-up card, glint, overlay, tray, preview-line and tooltip layout.
- Wording of option, spell and trigger texts within the rules here,
  until the naming pass.
- **Not tuning:** morale-primary, tempo-forward; three things to read;
  one camp spend per turn; slot tiers as settled; no Slot Renewal 2;
  Urge riding the action; Take Comfort self-only; one base heal card at
  exactly Recovery with targets 1 / 1 / 2 / 3; `HearthNoHeal`;
  `NoHealInDefaults`; the Dirge as the L3 default; the heal-isolation
  gate at 10 points per subclass, and the reliance and desirability
  reports with their escalation (**agreed**, Q1); the accepted level 1–3
  camp miss and Cadence 4 at levels 1–4 (**agreed**, Q2); the floor-1
  boss content rule (**agreed**, Q3); no new condition; no hero
  suggestion; `castDefaults` from static data only.

## 18. Changes to approved documents

Applied on 2026-11-20, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and the rows owed
by other owners (19; listed below) are recorded as such. The unlock-only
parts are recorded now and apply only when that aspect ships: the M3
event kind `heraldFearLands` in HR-29 (the Warcaller, 9.3), and the
Warcaller and Pennant rows of the heal-isolation gate (HR-38; 5.4).
Every row names the exact section it touches. Rows that touch **agreed**
or **settled** lines are flagged in bold. The client's Q1–Q3 answers
(2026-11-20) approve the rows that name a question; rows marked "told
plainly" were accepted with Q1 or Q3 or told with the approval request
(20.1) and confirmed by the client on 2026-11-20; the rest were
confirmed with the document on 2026-11-20.

**Narrowings recorded inside this design (no approved text changes;
recorded in classes 18):**
- Rally stays once per ally per fight; no Herald record grants Rallied;
- Take Comfort stays self-only; Mending Word never targets Downed;
- healing grows only by a heal card's tier or the Hearth's one widening;
- the Dirge adds no Demoralise trigger (T1–T7 fixed);
- Scatter never routs a boss or a fearless unit;
- "Banner" stays rejected (Pennant).

| # | Document / section (current status) | Change | Approved by |
|---|---|---|---|
| HR-1 | Classes 8.6, Cadence bullet (`rest` 2 / 3 / 4 *(illustr.)*) | Cadence 4 / 4 / 5 / 7 / 8 at levels 1–4 / 5–6 / 7–8 / 9 / 10 | Lead; levels 1–4 **client (agreed 2026-11-20, Q2)** |
| HR-2 | Classes 8.6, Cadence bullet (Spurred sub-bullet) and the "Bonus-action conflict" bullet | Urge costs `onEvent: ownActionResolved`, once per round. The conflict line becomes "Mending Word, Cadence's Frighten and Take Comfort, and Rally share the bonus action". Urge on a Rallied ally is legal | Lead; **told plainly** |
| HR-3 | Classes 8.6, At-wills bullet and Fallback type bullet; classes 6.5 (reading); **classes 6.4, "Damage types and riders" cap column (reading)** | Harrowing Word: 1d10 + Pre on a failure, Pre on a success (minions none); Faltering = save disadvantage, replacing "+1 die" (fallback +1d4); Plain Speech (bludgeoning against psychic-immune targets, failure and Ache damage alike). Read against the 6.4 cap ("no switching types per enemy"): Plain Speech is an automatic fallback on immunity, never a player switch | Lead; **told plainly** |
| HR-4 | Classes 8.6, Signature events bullet; classes 12.1 | Add "a resolve pip spent to the Herald's fear or Dazed effect" (its own row; Dazed is a non-fear source); "Harrowing Word's bonus die dealt" becomes "a Faltering save rolled with disadvantage" | Lead |
| HR-5 | Classes 8.6, Growth bullet (Scatter) | First clause: Will save with disadvantage. New second clause Frightens every other hostile in sight `turns:1`, **bosses and fearless units included** (stage 1 says "not a boss"; it never routs them) | Lead; **told plainly** |
| HR-6 | Classes 8.6, heal-cards sub-bullet; DH 12.1 "Class heal spells" row (reading: "higher tiers add targets"); ability 6.5 (reading: "declares what that adds") | Mending Word targets 1 / 1 / 2 / 3; its tier-2 upcast entry is declared and adds no target, so a tier-2 slot is legal (8.1); the cast's tier is the slot spent | Lead; told plainly |
| HR-7 | Classes 8.6, Subclasses bullet, Hearth sub-bullet ("heal tiers add targets; Treat at range 2; Mending Word also ends one poison or Bleeding instance") | Hearthkeeper: +1 side-by-side target on class casts at t2+ in combat, and on Soothe. The heal card ends one poison condition; on a Withered target it ends Withered instead of healing. No Hearth record heals (`HearthNoHeal`). Cures with no HP may ride Urge; heals may not | Lead; **told plainly** (`HearthNoHeal` accepted with Q1) |
| HR-8 | Classes 8.6, Subclasses bullet, Dirge sub-bullet | Antiphon (not "Two Voices"), one roll per target; Toll with Mourners; Knell; Refrain or Parley; Last Toll | Lead |
| HR-9 | Classes 8.6 chassis table (`hands == 1`) | Read as one-handed and versatile | Lead |
| HR-10 | Classes 5.6, `Floor1ClassSpread` "Input" sub-bullet; (b) classes 5.6 and 12.1 if the lever is used | (a) Herald pick = kit × L3 subclass; starters at the no-heal defaults; heal rows printed first, outside the mean. (b) Lever only: a creation pick may carry its own `suggestedArray` (Focus hand Dex +2) | Lead (ARC-15 pattern); (a) **told plainly, accepted with Q1**; (b) told plainly if used |
| HR-11 | Classes 7.5, the Frighten `cost` exclusion note | Refrain (`onEvent`, shared `frighten` allowance) and Parley (Demand Surrender as a bonus action) are subclass verb changes; the exclusion stands | Lead |
| HR-12 | Classes 7.8 | Herald recruits are non-casters at M2; `NoHealOnHeraldKit` (the CL-9 "Herald recruit" row is a permanent 0); "different slice" = no Frighten or Take Comfort, Spurred shared (CL-5) | Lead |
| HR-13 | Classes 6.4; ability 8.2 (ARC-8 line "A caster is never affected by its own casts' areas") | `PresenceAreaSide`: damage-free presence areas are one-sided by data. Ability 8.2's ARC-8 line gains "except presence call or cure areas whose record names the caster" (Rouse, Hearthsong) | Lead |
| **HR-14** | Ability 7.2, Frighten row (**settled**); enemies 14.6 table ("Frighten's range") | Universal Frighten: range 6, line of sight | Ability owner; **touches a settled row; told plainly** |
| HR-15 | Weapons 17.8 (Loom); classes 8.6 Foci row | Loom +3 on Frighten and hostile-facing presence effects only | Lead |
| HR-16 | Conditions 18.8 (Frightened), 12.3, 17.3; 18.5 (Withered, Poisoned, Bleeding) | (a) Frightened `controlRank` = Dazed's. (b) **The repeat rule** ("further Frighten attempts … disadvantage") widened: later fear effects from that source give advantage on the save. (c) Ends lists gain: Rouse (allies) and Watchfire (allies) on 18.8 Frightened; Hearth cures (closed list); "a Hearth heal card ends Withered instead of healing". (d) The repeat set is kept on the unit's life state beside its pips, and a pip spent does not start it (10.2). The request notes that HX-1 is universal and that this design owes a rerun of `Floor1ClassSpread` for all four classes and of the Arcanist pip-trap and novice reports (10.2) | Conditions owner; (b) **touches a settled interaction line; told plainly** |
| HR-17 | Conditions 19 transform row; 18.8 Frightened interaction line | The Panicked transform ships with Panicked (Later); in M2, re-application follows 6.3 | Conditions owner |
| HR-18 | Conditions 18.3, Dazed sources | Add presence spells (Bewilder, Overwhelm) | Conditions owner |
| HR-19 | Enemies 14.6 ("one disposition, reached two ways"; "within 2 squares"; the fact priority); classes 11.H, Demand Surrender row ("Unchanged") | Range 4 for a Dirge with the Tipstaff (Loom does not stack); Parley's cost; Yield Arms **or** Offer Terms as a third route to Yielded (never both); the player picks the fact category (HX-6); correct 11.H to "changed" | Enemies owner; lead |
| HR-20 | Enemies 14.4 (Emboldened, C-9); conditions 18.8 Emboldened | Waver: suppressed while the leader's Frightened is active; no `onEnd`, no T1 | Enemies owner; **touches the settled "blocks Routed"** |
| HR-21 | Enemies 17.5 boss framework and 10.5 generator procedure; enemies 20.1 content validation; stat model 8.4 (reading) | The floor-1 boss is never `mindless` and never psychic-immune (class-blind); boss pool records carry `eligibleFloors`; content check `Floor1BossMindReachable` (a `mind` immunity alone passes); every theme able to appear on floor 1 has a compliant floor-1 boss. Motivated by the Herald, as the client was told | **Client (agreed 2026-11-20, Q3)**; enemies owner applies |
| HR-22 | Enemies 14.8 | Show of Force: advantage on one cow attempt | Enemies owner |
| HR-23 | Enemies 10.6 (display); camp UI | Next floor's theme and its non-fearless band, class-blind | Lead and enemies owner |
| HR-24 | Consumables 16.4 Terror row; 16.4–16.5 `classTags`; 16.8 count | Terror enemies-only; the 8.8 mapping; 18 → 15 scrolls; the tier-4 pool noted (optional Pall scroll lever) | Consumables owner; lead |
| HR-25 | Arcanist design 8.1, the `VariantIsScroll` "not compared" list (ARC-22 / SP-7) | Add `faltering` and whether the caster may be a target | Lead |
| HR-26 | DH 18.1, "Reports beside the gate" bullet; classes 6.6, 12.3 | Classification columns (report only); policy-forced arms per subclass with the 5.4 construction; the "Hearth, no card" row; the 2+ row with escalation; source rows (Hearth, scroll, get-ups by route); `MoraleValueReport`, `AllyTempoValueReport`, heal telemetry, the trap-room report. **The DH 18.1 gate is unchanged** | Lead (reports only); **told plainly, accepted with Q1** |
| HR-27 | Classes 12.2, validator list | The 14.2 list | Lead |
| HR-28 | DH 11.2, get-up row; ability 7.2 (Medicine) | Reach the Fallen (Hearth L6 option): get-up at range 2, the same action Treats (only when the get-up is taken as an action, never through the Lifeline bonus-action variant); once per fight, 1 HP, Prone unchanged | Lead; **touches agreed DH Q4; told plainly** |
| HR-29 | Ability 14.3 (event kinds) | `harrowingWordFailedSave`, `ownActionResolved`, `heraldFearLands` (M3), `combatStart` | Lead |
| HR-30 | Classes 10, names table | Faltering (not Dread), Pennant restated, Antiphon, Ache, Soothe and the frozen names; avoided names as in the front matter | Lead |
| HR-31 | Ability 6.3; classes 8.2, camp-pool chassis bullet; classes 11.B, camp-share row; Vanguard design VG-13 | The Herald's level 1–3 camp miss accepted; levels 3–4 read on the class mean over both subclasses, subclass rows printed. **Widens an agreed reading** | **Client (agreed 2026-11-20, Q2)** |
| HR-32 | Enemies 20.3 reports | The early-end target reported with every adopted morale mechanic on, with and without a Dirge | Lead |
| HR-33 | Classes 6.4, tokens row | Any Herald Helped would sit under ally tempo; none ships at M2 (CO-B3 stands) | Lead |
| HR-34 | Ability 6.5; classes 7.7 | Data now: `upcastCap: classSlotTable` on every heal card (`HealUpcastCapDeclared`). Runtime at M4: `HealUpcastByClassSlots` caps a heal card's effective tier at the Herald's own class-level slot table | Lead |
| HR-35 | Ability 6.5, offer exclusions | `slotRecovery+1` excluded for the Herald (a starting exclusion) | Lead |
| HR-36 | Classes 14, the starting-kit requirement (fills it; no approved document names a Herald weapon) | Kits carry a longsword (Parry). The draft proposal's mace (Topple) is rejected (4, row 10); on solo floor 1 the escort answer is Shove or the shield bash's Push at Str 0, not Topple (7.16) | Lead; told plainly |
| HR-37 | Enemies 14.10; ability 8.3; conditions 18.4 (display) | Break-chain preview; the morale overlay family and priority; the Spurred trail; the glint count in the readability playtest gate | Lead |
| HR-38 | Classes 12.3, simulator reports (new gate line); classes 6.6, "Healing" bullet; DH 18.1, "Reports beside the gate" bullet (a pointer) and "Levers if it fails" bullet (a Herald refinement inside its first lever); **classes 6.1 item 5** ("Healing keeps its approved gate") and **classes 5.6 "What a miss means"** (the M2 release gate list), both **agreed text, approved by the client (Q1, 2026-11-20)** | **The heal-isolation gate:** per M2 subclass, Herald with heal cards − Herald without ≤ 10 points win rate (lead's target 5), an M2 release gate on the reference arm (5.4). **The reliance and desirability reports** as required reports with escalation thresholds. Classes 6.1 item 5 reads "Healing keeps its approved gate (DH 18.1, agreed) and gains the heal-isolation gate (Herald design 5.4)"; the 5.6 gate list gains the heal-isolation gate, and the reliance reports stay reports. DH 18.1's first lever gains, for the Herald, "heal-specific sub-limits in Herald data; never the Herald's slot or Cadence pools"; "never the guarantee, and never by class" stands verbatim. The DH 18.1 gate itself is unchanged; its kit snapshot is read per fight (5.4, a reading) | **Client (agreed 2026-11-20, Q1)** for the gate and the reports' status; lead for the construction |

**Rows owed by other owners before records:** HR-14, HR-16–HR-22,
HR-24.

**Not changed (checked):** DH 18.1's gate definition and DH 18.2
(**agreed**); classes 5.6's 10-point spread (**agreed**); ability 11.3
(**agreed**; read, not changed); ability 8.4 (**agreed**; no ground
zones); CN-11 (**agreed**); DH 11.2's "up on 1 HP, Prone" (HR-28 changes
the range and adds the bundled Treat, nothing else); armour 11 item 2 and AR-20 (11 stands); enemies 14.3 T1–T9;
Rally's once-per-ally-per-fight limit; `ClassGatedContent`; the
32-condition budget.

**Check after applying (lead's call):** a grep for "rest` 2 / 3 / 4" in
classes 8.6 finds only history; a grep for "Mending Word and Cadence
both use the bonus action" in classes 8.6 finds the HR-2 wording; a
grep for "Two Voices" finds only history; a grep for "Banner" in a
subclass sense finds none; a grep for "Unchanged" on the classes 11.H
Demand Surrender row finds "changed"; a grep for "18 scrolls" in
consumables 16.8 finds none, and "15 scrolls" is found there.

## 19. Depends on systems not yet designed

- **The conditions owner:** HR-16 (a) and (b), required before records
  (HX-1b alone if HX-1 is declined); HR-16c ends lists (Watchfire
  included); HR-16d, the repeat set's storage; HR-17; HR-18. HX-1 is
  universal, so its adoption carries the four-class `Floor1ClassSpread`
  and Arcanist pip-trap reruns (10.2).
- **The enemies owner:** Waver (HR-20); Offer Terms (HX-5); HR-19
  (Demand Surrender range, Pick the Question, Yield Arms); HR-21 (the
  content rule, **agreed** Q3) as authoring data and a content check;
  HR-22; HR-23; the early-end report (HR-32).
- **The events design (segment 8):** EV-13, the Herald-lit "Show of
  Force costs no slot this floor" variant, a pending request to this
  owner and the enemies owner (M3 if ruled; 8.2; events design 4.8).
- **The exploration and information design (segment 10):** XI-25,
  F-TREASURE through Yield via Pick the Question (once per floor, after
  F-KEY; kill switch), a pending request to this owner and the enemies
  owner (M3 if ruled; 10.5; exploration and information design 4.6.4).
- **The boss rooms design (segment 9):** BR-19 narrows HR-21's
  escort-and-reserve clause for floors 2–5 (**agreed**, boss rooms Q4,
  2026-11-22; M2; 7.16); the Herald answer windows, the "Resolve
  Broken" beat and `ClassBossContribution`'s fear-target availability
  row are segment 9's (boss rooms design 4.7.4, 9.3).
- **The ability owner:** HR-14 (universal Frighten range); HR-29 event
  kinds.
- **The consumables owner:** HR-24 (Terror enemies-only, the `classTags`
  mapping, the scroll count).
- **The registry owner (trinkets 11):** the pairs named in 7.18 and 9,
  including Give Ground against the Upheaval scroll and the Push verb,
  and Recoil against Upheaval's shape; Reach the Fallen against the
  Lifeline Pendant.
- **Companions:** the Drummer and Shieldbearer kits (7.17); Recruiter's
  Eye **adopted (companions design 9.2; CMP-15)**: one extra free
  Muster card with Leadership training, one free-card re-roll per camp
  with mastery, never a unit, hire, slot or party weight.
- **The simulator:** the heal-isolation gate; the three arms; reliance
  and desirability; `Floor1ClassSpread` on the reference arm; parity,
  camp share, sustain, early-end, trap-room and skill-expression
  reports. Pending (SV-22, M2): the generalised `Skill.*` arms, or the
  Herald owner's novice script by the M2 planning gate (14.3; seeds and
  validation design 6.2). Levers follow the precedence PR0–PR5 and the
  blast-radius rerun (SV-26, read; 5.4; seeds and validation design
  6.9).
- **The starting kits design (segment 14):** the starting-kit hooks of
  7.11 and 7.12 (the Focus hand creation warning, HR-10b per-pick arrays) are
  taken up (starting kits design 2.2, 4.2, 4.6). Applied 2026-11-23:
  SK-1 (7.11 coin exactly 15, M1) and SK-2 (set B 1 javelin in 7.2, 7.7
  and 7.11; the 7.12 rerun; M2). Informed: SK-9 creation warnings (7.12,
  M2). Kit variants are boon options only (SK-7, M3; agreed, Q1).
- **The heirlooms and curses design (segment 15; approved
  2026-11-23):** no HL row changes this document. Reading: 9.5's
  "Cures against boss afflictions" keeps its approved meaning, and
  Herald text never uses "Malady" (heirlooms and curses design 4.3.5,
  13). **Pending request (heirlooms and curses design 5.1, 11; M3):**
  the Herald owner confirms the R1 Herald heirloom, the Thumb-Worn
  Tipstaff (a Tipstaff, 7.7), and its line **Carrying Voice**: your Rally
  reaches +2 / +2 / +3 squares (b1 / b2 / b3). Until confirmed, the
  line is the heirlooms and curses design's, not a Herald rule.
- **The ability 8.4 aura ruling:** Pennant, Haunting Chorus.
- **Builders:** `sourceUnitId` on every condition instance (Ease the
  Pain).
- **Naming pass:** every placeholder name here; "Spur" next to Spurred
  and "Interject" next to Interpose flagged (resolved: renamed Urge and
  Speak Over, naming pass NP-38).
- **Lead's checks before data:** every SRD shape in this document
  against SRD 5.2.1 (Cause Fear and Command first); `NameCollision` over
  the final names (run by `V7.NameRegistry`; SV-20, seeds and validation
  design 5.2).

## 20. Open items for the client

None. Approved by the client on 2026-11-20, as a whole, including every
lead's call, the told-plainly items in 20.1 and the new lead's calls
listed there. The future items in 20.2, which come back only if a report
or a later design triggers them, and the naming pass remain future
items.

### 20.1 This document

The three questions this design raised were answered on 2026-11-20
**(agreed)**, each with the recommended option:

- **Q1, holding "not required" and "not relied on" to account:** option
  A. Heal isolation is a **gate**, run per subclass: Herald with heal
  cards − Herald without ≤ 10 points win rate, lead's target 5. Herald
  reliance (no Herald vs Herald ≤ 10 points) and the desirability report
  (Herald without heal cards − no Herald ≥ +2 points; heal-card pick
  rate in the reference arm ≤ 50%) are **required reports with
  escalation thresholds** (miss → the lead applies the levers in order →
  if they run out, back to the client). Desirability misses are fixed
  with morale and tempo levers, never healing. The DH 18.1 healer gate
  itself is unchanged. Told plainly and accepted with it: defaults never
  heal and the Dirge is the default lens; the gate reads the
  non-healing Herald; Hearth records heal nothing (`HearthNoHeal`).
  Rejected: B (gate reliance now) and C (reports only).
- **Q2, the camp share at levels 1–3:** option A. Accept the Herald's
  level 1–3 miss (23 / about 36 / 33–37 *(illustr.)*), as ARC-4, SH-28
  and VG-13 did; read levels 3–4 on the class mean over both subclasses;
  Cadence stays 4 at levels 1–4. Rejected: B (Cadence 3 at levels 1–4).
- **Q3, the floor-1 boss content rule:** option A. Class-blind: the
  floor-1 boss is never `mindless` and never psychic-immune;
  `ClassGatedContent` untouched; this document states plainly that the
  rule is motivated by the Herald. Rejected: B (no rule; rely on Plain
  Speech and a rerun).

**Told plainly with the approval request; confirmed by the client on
2026-11-20:**
- universal Frighten range 6 (HR-14);
- the Loom reading (HR-15);
- Urge's `onEvent` cost and the narrowed conflict line (HR-2);
- Ache, Faltering as save disadvantage, and Plain Speech (HR-3),
  with Plain Speech read against the classes 6.4 cap ("no switching
  types per enemy") as an automatic fallback on immunity, not a switch;
- Scatter's save and its second clause reaching bosses (HR-5);
- the Mending Word ladder 1 / 1 / 2 / 3, with a declared tier-2 entry
  that adds no target, read against ability 6.5 (HR-6);
- the Hearth text fix (HR-7);
- Reach the Fallen's get-up at range 2, touching agreed DH Q4 (HR-28);
- the longsword rather than the draft's mace, and no Topple: on solo
  floor 1 the escort aura has only Shove and the shield bash's Push at
  Str 0 (and T7), so without Waver it is largely unanswered there
  (HR-36; 7.16);
- a `fear`-immune, non-mindless floor-1 boss stays legal under the
  agreed Q3 rule; there Cadence fear makes nothing faltering, which the
  trap-room report watches with its levers (7.16);
- the DH 18.1 kit snapshot read per fight, with a run counted "with
  healer" if any fight's snapshot qualifies (5.4; the gate itself
  unchanged);
- HR-38 also touches agreed classes 6.1 item 5 and classes 5.6 "What a
  miss means", as the Q1 decision implies;
- Parley, the Dirge's expert L6 option, adds a bonus-action use, the one
  exception to the subclass rule (9);
- the widened repeat-fear rule (HR-16b);
- Pennant, not Banner;
- Urge on a Rallied ally is legal (7.4).

**New lead's calls in this document** (beyond the proposal the client
said stands; listed so the approval covers them; confirmed by the
client with the document on 2026-11-20):
- **the heal-isolation gate's status and reading** (5.4): an M2 release
  gate on the reference arm, read at the first M2 simulator run with
  the listed items in data; each subclass passes on its own; a gap
  above 5 and up to 10 passes as a watch row on which the lead may apply
  the healing levers; above 10 the levers are required; Warcaller and
  Pennant rows join when they ship;
- **the arm construction** (5.4): the hero is the Herald in every
  Herald arm, the level-3 subclass is forced to the row's subclass, and
  the forced Mending Word replaces Grating Murmur as the second
  starter in the gated "with" arm; the **no-Herald hero is each of the
  other three launch classes** on the same seeds, read on their mean
  with per-class rows; recruits are drawn from the full pool in every
  arm, a Herald kit re-rolled on a dedicated substream in the no-Herald
  arm only; a **"Herald, free draws" arm** where desirability (b) and
  the heal-card pick rate are read (class and per subclass); a
  diagnostic **"card added" arm** that escalates through the healing
  levers above 10 points;
- **the DH 18.1 snapshot pinned per fight** (any fight qualifying makes
  a healer run) (5.4);
- **reliance is read per subclass against the higher Herald arm**, then
  averaged over the subclasses, with its own lever order (heal levers
  first if heal isolation is more than half the gap; then classes 6.6's
  lean size; then baseline content density) (5.4);
- **which healing levers each miss uses** (5.4): Dirge or base rows
  levers 4–6; Hearth rows 1–6; heal isolation stops at 6, then the
  client; levers 7–8 only for the healer gap and the 2+ row;
- **the trap-room escalation** and the hearth-list frequency report, and
  the Urge Cadence-share escalation (5.4);
- **the escalation procedure** (5.4): class mean escalates, per-subclass
  rows get per-row data changes; one lever at a time with a rerun; no
  lever applied that breaks another line, which gives the +2 to +10
  window;
- **the cast's tier is the slot spent,** and a spell may use a higher
  slot only where its record declares that tier (ability 6.5), so
  Mending Word's empty tier-2 entry gives a tier-2 cast its tier-1
  effect plus Hearthkeeper's target (5.3, 8.1, 9.2); every heal target
  in range, each creature healed at most once per cast;
- **pip timing:** a pip your own fear stripped earlier this turn makes
  the target faltering at once, replacing the proposal's contradictory
  wording (7.2);
- **what "your action" means for Urge, and the order after the action**
  (in-use effects, then the checkpoint, then the Urge decision point)
  (7.3);
- **the Faltering predicate shape** (`appliesTo`, `requires`,
  `expires`), Faltering judged at each save roll, and "your last turn"
  meaning combat start before your first turn (7.2);
- **the rider's natural 1 read on the kept die** (7.2);
- **allowances as `{id, period}` records**, with `parley` added, and
  Clarion consuming the first turn's Urge (7.8; 7.13);
- **Scatter's targets split once at declaration**, one save each (7.9);
- **the Urge pre-tick conditions** (eligible targets; the Frighten
  reserve; no rout-ready hostile in Frighten range) and the B2/B3
  reserve guard (7.14);
- **Ringing Blow's cast policy and on-hit prompt** (7.14);
- **heal rule B4 moved to simulator arm-policy data;** the Hearth's
  rotation rule H1; the exact predicate definitions and the tie-break
  (3.4, 7.14);
- **Overwhelm's effect order** (8.5);
- **Antiphon needs two legal targets; Refrain's range, timing and
  partner** (9.1);
- **Hearthkeeper's extra Soothe creature takes the cure only;** the
  side-by-side encoding; Reach the Fallen's Treat only as an action;
  Gather In r2 at t3 and r3 at t4 (9.2); the Pennant's Art I (9.4);
- **the condition-independent Hearth lever** first in the "weak without
  a card" list (9.2);
- **the HX-1b repeat set stored on the unit's life state**, and a spent
  pip starting no repeat rule (10.2);
- **`AllyUpliftBound`'s formula and line** (11.2);
- **`eligibleFloors` and the per-theme floor-1 boss assertion** (7.16);
- **the `DrawSatisfiable` relaxation order** s2, s4, s3, never s1 (7.7);
- **a Demoralised Emboldened foe takes Scatter's second clause** (7.9);
- **`Floor1BossMindReachable`** as the content check for the Q3 rule
  (7.16; HR-21);
- **the caster is in its own call and cure areas** where the card names
  "you" (Rouse, Hearthsong), an exception to the ARC-8 wording (8.1;
  HR-13);
- **Ringing Blow spends that turn's camp spend** on whatever turn it
  fires (8.2);
- **the spell count corrected** to 20 drawable cards, 19 that never heal
  (8.6);
- **Refrain fires at most once per turn** with two words (9.1);
- **Soothe may cure an already-Spurred unit** (9.2);
- **signature events per subclass** (9.1, 9.2);
- **HX-1b is requested alone if HX-1 is declined** (10.2);
- **the static `SpellTierBound` reads the bare record;** the faltering
  figure is a `PayoffBound` reading (11.2);
- **level 4's class-mean camp share must reach 40%** (11.10), with no
  L4 lever inside the agreed Cadence 4, so a measured miss goes to the
  client;
- **how the Herald-specific checks work** (`HearthNoHeal`,
  `NoHealInDefaults`, `NoHeraldRallied`, the split of
  `HealUpcastByClassSlots`) (14.2);
- **HR-38** recorded as its own row for the client-approved gate, with
  HR-26 kept for the reports.

### 20.2 Future items triggered by reports

These come back to the client only if a report or a later design
triggers them:
- the heal-isolation gate still failing for a subclass after the healing
  lever order runs out (Q1);
- the reliance report above 10 points, or the desirability report below
  +2 points or above a 50% heal-card pick rate, after their levers run
  out (Q1);
- the 2+ healer row above 5 points after the healing levers run out;
- a `Floor1ClassSpread` miss whose levers run out (classes 5.6);
- a floor-1 death share above about 5% for the Herald (DH 18.2), read
  on the reference arm;
- the no-healer arm below a quarter (DH 18.2, a calibration target);
- a level-4 class-mean camp share below 40% (11.10; no L4 lever exists
  inside the agreed Cadence 4, so the question would be whether to fold
  level 4 into the Q2 acceptance);
- the trap-room report still escalating after its levers (5.4);
- an owner declining HR-16 or HR-20 in a way the class cannot absorb;
- a hero coach, only if the client asks (ability 11.3);
- the naming pass.

## Appendix A: how the critique findings were resolved

This appendix is history and keeps the names as they stood on
2026-11-20; the naming pass renamed several of them (Spur → Urge,
Earned Spur → Earned Urge, Interject → Speak Over, Undertone → Ache,
Lay Down Arms → Yield Arms, Totem → Tipstaff and the Totem kit → Focus
hand; naming pass NP-38).

**Research conflicts (computed brief against approved text):**

| Brief item | Resolution |
|---|---|
| "Banner" | Pennant |
| Cadence pool as described | Kept; Spur's cost changed (HR-2) |
| "Mending Word also ends Bleeding" | Poison, or Withered instead of healing (HR-7) |
| "Two Voices" | Antiphon (HR-8) |
| "+3 range on fear" | The hostile-facing reading (HR-15) |
| Scatter "neither fearless nor a boss" | Kept for routs; the second clause added (HR-5) |
| Hearth "Treat at range 2" | Firelight |
| "Healing only via cards and Hearth" | Held strictly: no Hearth record heals |

**Judges' grafts:** all adopted as listed in 6.3, except: Faltering +1d4
replaced by save disadvantage, with +1d4 kept as the fallback (fun
critique, item 2); the Totem Dex +2 default held as a lever
(consistency critique, item 2); Keening dropped in favour of Toll (two
of three judges, and the fun critique); Hand Up dropped (consistency
critique, item 3); Exhort held Later (CO-B3). Judge-split items, as
decided: heal isolation a gate and reliance a report (**agreed**, Q1);
Refrain on the shared `frighten` allowance; Interject held; Offer Terms
the enemies owner's decision.

**Critic: fun**

| # | Finding | Resolution |
|---|---|---|
| 1 | Mindless targets make the "speak" beat dead | Plain Speech; triggers rescoped; the Q3 content rule (**agreed**) |
| 2 | The +1d4 payoff is invisible | Faltering becomes save disadvantage; one predicate list; one glint |
| 3 | "Every-turn tempo" is false at L1–4 | Cadence 4 at L1–4 (**agreed**, Q2); the solo self-Spur kite removed; Earned Spur the first desirability lever |
| 4 | The Hearth nudges toward a card | Hearthkeeper reaches Soothe without a card; the text leads with the cures |
| 5 | The Hearth crowds the bonus action | Soothe rides Spur; Hearthsong is an action; Hand Up dropped; Refrain on `frighten` |
| 6 | Five surrender verbs | Lay Down Arms **or** Offer Terms; Parley the Dirge's only verb; Pick the Question adopted |
| 7 | Thin boss climax | HX-1 + Waver as a package (Waver to the enemies owner) |
| 8 | Glint inflation | One overlay family, priority order, hover-only chain, glint-count playtest gate |
| 9 | Dirge spell choice | Toll; the passive renamed Knell; Antiphon III becomes a waiver |
| 10 | Spell list | s4 added; Trembling Word's preview reason; Onward cut |
| 11 | Verses | Later trial; kinds renamed |
| 12 | Interject | Held; tied to Cadence 4 and Earned Spur if revived |
| — | Desirability gate | A required report with escalation (**agreed**, Q1) |

**Critic: balance**

| # | Finding | Resolution |
|---|---|---|
| C1 | Floor-1 spread not closed | Undertone and the boss rule shipped; Cadence 4 (**agreed**, Q2); Dex +2 a lever; the Q3 content rule (**agreed**); reference arm owed; risk 1 |
| C2a | Undertone kills minions | Minions take none on a success |
| C2b | `onEvent` on every word | `harrowingWordFailedSave` everywhere; tested |
| C2c | Parry takes the router reaction; Topple lost | Policy order; morale rerun; the solo floor-1 escort answer is Shove or the shield bash's Push at Str 0 and T7 (Give Ground and a Spurred Shove come later); told plainly |
| H1 | Spell fear re-locks bosses | HX-1b / HR-16b, required with HX-1 |
| H2 | 2+ healer row hidden; quarter at the line | 2+ row with escalation; "quarter" a calibration target (DH 18.2) |
| H3 | "Ever held" redefines the gate | Report columns only; the gate stays the kit snapshot, read per fight (5.4) |
| H4 | The card-less Hearth hides sustain | Own row; get-ups by route; Hand Up dropped |
| H5 | Helped at range | Onward cut; Exhort Later |
| H6 | Multiclass heal upcast | `HealUpcastByClassSlots` |
| M1 | Scatter with no save | Save with disadvantage; early-end report with everything on |
| M2 | Spurred aggregate | Party-wide report; Spur II lever; `LeanSpread` at L9–10 |
| M3 | A reliance gate could weaken morale | A report until measured (**agreed**, Q1) |
| M4 | Camp share | Cadence 8 at L10; the L1–3 miss accepted (**agreed**, Q2) |
| M5 | Warcaller nova | Counted; capped at 2 allies; pip case dropped |
| M6 | Round-1 double pip strip | Accepted with HX-1b; reported |
| L1 | Ladders disagree | One table (5.3, 9.2) |
| L2 | Withered | "Instead of healing" at M2; cure-first held as a lever-up |
| L3 | Hearthsong duplicates Rouse | An action; no Frightened on its list |
| L4 | False mindless answers | Rescoped; Plain Speech makes Harrowing Word's version real |
| L5 | Novice arm | Preview and overlay required; Verses, Interject and Double Time out of M2 |
| L6 | Spur on a Rallied ally | Told plainly |

**Critic: consistency**

| # | Finding | Resolution |
|---|---|---|
| 1 | The fix package was measured on the basic arm | 7.14, 11.5: the basic arm is a lower bound; gated-arm fixes listed separately |
| 2 | Totem Dex +2 not expressible | Lever only, via HR-10b |
| 3 | Hand Up against Lifeline (trinkets rule 1) | Hand Up dropped; Reach the Fallen is a range step |
| 4 | 8.6 sentences rewritten without rows | HR-2, HR-5, HR-6, HR-7, each with a row; Scatter rider fallback stated |
| 5 | Rejected Hearth mechanics linger | Kindle, Field Dressing and a healing Hearthsong stripped; names retired |
| 6 | "Quarter" mislabelled as a gate | Calibration target; the new gate is a client decision (**agreed**, Q1; HR-38) |
| 7 | Waver overrides settled text | The enemies owner's decision; rows named; T1 edge semantics; 14.3 gate |
| 8 | Cost-on-cost leans | HR-11; Shout Down held; one allowance model (`frighten`) |
| 9 | Third route to Yielded | One row; never both |
| 10 | Demand Surrender row missed in 11.H | HR-19 with the 11.H correction |
| 11 | Cure end sources | One consolidated row (HR-16c); the P3 tonic report with a Hearth arm |
| 12 | Wrong ground for rejecting Salve | Rejected on the approved lean text ("Treat at range 2") and redundancy with the universal cost offer; `cost:treat` excluded if it ever shipped |
| 13 | Firelight's get-up range touched an agreed row | L3 Firelight is Treat only; the get-up range moves to the L6 option with HR-28, told plainly |
| 14 | Interject as a lean | Held; if revived, a tool, an L4 row and the ARC-19 widening |
| 15 | Helped at range | HR-33; none at M2 |
| 16 | Drummer slice | HR-12 reading |
| 17 | Herald recruit heals | `NoHealOnHeraldKit`; the 0 row printed |
| 18 | Camp-share acceptance is class-specific | Q2 for the Herald, L1–3 explicit (**agreed**; HR-31) |
| 19 | Content rule changes enemy authoring | HR-21; class-blind; honest motive (**agreed**, Q3) |
| 20 | False mindless answers | Rescoped; F4 citation fixed to conditions 12.2 |
| 21 | `classTags` consequences | HR-24, HR-25; Unweaving claimed |
| 22 | One value per record | Fixed: Harrowing Word, Cadence, Mending Word, Hearthsong, Dirge spell |
| 23 | Names | Dread, Order, Steady and the duplicated Toll / Refrain removed; HX-1 is a ruling with no name, so the Later "Unnerve" reserve keeps its name; Spur and Interject flagged (renamed Urge and Speak Over, naming pass NP-38) |
| 24 | Id collisions | One HR-n series; flags on agreed and settled rows |
| 25 | Smaller items | Double Time Later; HR-17 before records; Scatter save told plainly; the Dazed signature event as its own row (HR-4) |

**Client round (2026-11-20):** Q1 A, Q2 A, Q3 A, each the
recommendation; everything else stands. This document converts the
options to decided rules, records the rejected alternatives in one line
each (section 4, 20.1), adds HR-38 for the client-approved gate, and
adds the new lead's calls listed in 20.1.

**Post-decision check (2026-11-20; builder, consistency and play
lenses):** every finding was fixed in place or merged, apart from two
resolved inside the client's decisions: Plain Speech and the
`triggerMenu` stand as accepted, so `falterVsImmune` built into Plain
Speech is the first trap-room lever rather than an M2 change; and Parley
stays a bonus action, recorded as the one exception to the subclass
rule (9). The new lead's calls this check produced are listed in 20.1.

**Client approval (2026-11-20):** the client approved this document as
a whole, confirming every lead's call, every told-plainly item and the
new lead's calls listed in 20.1.

## Appendix B: sources

Checked during research: PF2e Demoralize and its immunity (Archives of
Nethys, Paizo forums); the Battle Brothers morale dev blog; the 4e
warlord (a fan wiki and an EN World thread). From memory and to be
checked before citation: the 5E bard and cleric (Bardic Inspiration,
Cutting Words, Song of Rest, Healing Word, Mass Healing Word), PF2e
Inspire Courage and the Marshal, paladin auras, the 13th Age commander,
Darkest Dungeon, Baldur's Gate 3, Divinity: Original Sin 2, the XCOM 2
Specialist, Total War: Warhammer, Fire Emblem dancers, Into the Breach,
Slay the Spire, the Dungeon World bard and the Pillars of Eternity
Chanter. Every SRD shape is checked against SRD 5.2.1 before records are
written.
