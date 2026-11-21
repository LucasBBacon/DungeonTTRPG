# Traps, Locks and Checks: Design (Dungeon Generation, Segment 5)

Date: 2026-11-21
Status: Approved, 2026-11-21
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model, "STAT"),
`2026-11-16-ability-resource-model-design.md` (ability model, "ABL"),
`2026-11-16-conditions-design.md` (conditions design, "C"),
`2026-11-16-enemies-design.md` (enemies design, "ENM"),
`2026-11-16-items-inventory-design.md` (items design, "ITEMS"),
`2026-11-17-weapons-design.md` (weapons design),
`2026-11-17-armour-design.md` (armour design),
`2026-11-17-trinkets-design.md` (trinkets design, "TK"),
`2026-11-17-consumables-design.md` (consumables design, "CN"),
`2026-11-17-death-healing-design.md` (death and healing design, "DH"),
`2026-11-17-classes-concepts-design.md` (classes stage 1, "CL"),
the stage-2 class designs `2026-11-18-vanguard-class-design.md`,
`2026-11-18-shade-class-design.md` ("SH"),
`2026-11-20-arcanist-class-design.md` ("ARC")
and `2026-11-20-herald-class-design.md` ("HR"),
`2026-11-20-companions-design.md` (companions design, "CMP"),
`2026-11-21-run-structure-themes-design.md` ("RS", segment 1),
`2026-11-21-floor-layout-design.md` ("FL", segment 2),
`2026-11-21-room-terrain-design.md` ("RT", segment 3) and
`2026-11-21-hazards-design.md` ("HZ", segment 4, the most recent
template)

Items marked **(agreed)** are the client's answers of 2026-11-21 to the
three questions this design raised (Q1–Q3, sections 0.2 and 19.1), each
answered with the lead's recommendation (option A), or restate a client
answer recorded in an approved document. The client also said that
everything else in the lead's proposal stands as written: the method
(Model C as the chassis, on layer L's check core, with hybrid M's
grammar and Model B's fairness ideas), the check charter CC1–CC8, the DC
ladder, the `traps` and `checks` streams, the Margin rule, Take time,
detection, the taxonomy, the deliberate-triggering rules, the knowledge
model, the damage bounds and caps, the floor-1 package, density and
placement TP1–TP12, locks, keys and the Wax Key, doors, chests and
containers, Wager rooms, the DEP-S1 fallback, class spread, solo play
and companions, noise and hearing, the clock allowance, seeds and
determinism, UI and previews, the full per-theme catalogue across all
ten themes, readability, art, VFX and audio, the adopted, deferred and
rejected mechanics, the numbers and fixes, data, V11, reports and arms,
the staging, ownership and hooks, the binding requirements, the TL-n
changes, the told-plainly items, the future items, the risks and the
critique resolutions and names. The client approved this document as a
whole on 2026-11-21.

**The nine brief questions** (BQ1–BQ9 here, to keep them apart from this
document's client questions Q1–Q3) were answered by the lead in the
proposal (0.1). The client accepted those answers by proceeding to
Q1–Q3; they are recorded as the **lead's answers**, and the client
confirmed them all on 2026-11-21, when it approved this document as a
whole. **BQ6 ("no consumable lockpicks") was contested by the lead**
against the brief's suggestion; the client confirmed it with this
document on 2026-11-21 (19.1).

Items marked **(lead's call)** were filled in by the design lead inside
approved rules and were in the proposal the client answered; the client
confirmed them all on 2026-11-21, when it approved this document as a
whole, and they are binding. Items marked **new lead's call** were made
while writing this document, beyond that proposal; they are listed in
19.1 and the client confirmed them all on 2026-11-21. Items marked
**(lead's call, after the check)** were made while revising this
document after the design check that followed the client's Q1–Q3
answers; they are listed in 19.1, the client confirmed them all on
2026-11-21, and they never override a client answer. Items
marked **(settled)** restate a rule from an approved document;
**(decided)** marks a main-design client decision. Items marked
**(change TL-n)** amend an approved document and are listed, with the
exact section they touch, in section 17; rows that touch **decided** or
**agreed** text are flagged there, and the client's approval of this
document on 2026-11-21 (with Q1–Q3 where a row names one) covers them.
Items marked **(told plainly)** narrow or re-read agreed or settled
text, or state an unwelcome forecast, and were told to the client in
plain words (19.1, including those told after the check); the client
confirmed them all on 2026-11-21. Every item here is binding on the
builders.

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer: Q1–Q3 of this document (2026-11-21), or one recorded in an approved document, restated |
| **lead's answer (BQn)** | The lead's answer to brief question n, accepted by the client by proceeding; confirmed by the client with this document on 2026-11-21. BQ6 was contested by the lead and was confirmed on 2026-11-21 |
| **(lead's call)** | Filled in by the design lead inside approved rules, in the answered proposal; confirmed by the client with this document on 2026-11-21 |
| **new lead's call** | A lead's call made while writing this document, beyond the answered proposal; listed in 19.1 and confirmed by the client on 2026-11-21 |
| **(lead's call, after the check)** | A lead's call made in response to the design check that followed the client's answers; listed in 19.1 and confirmed by the client on 2026-11-21; never overrides a client answer |
| **(change TL-n)** | Amends an approved document; listed with its exact section in 17 |
| **(told plainly)** | Narrows or re-reads agreed or settled text, or states an unwelcome forecast; told to the client in plain words (19.1) and confirmed by the client on 2026-11-21 |
| *(illustr.)* | An illustrative number, owned by the builders and calibrated by the M2 simulator (10, 11). Never binding |

**The client's brief:** dungeon generation segment 5 of 12, traps, locks
and checks, with nine questions: (1) should traps trigger on a failed
disarm; (2) is a triggered trap the full effect; (3) may players step on
known traps on purpose; (4) do enemies trigger hidden traps, pass
through them or pathfind around them; (5) special locks with a
consequence on a failed pick; (6) consumable lockpicks; (7) mimic
chests, triggered on a pick attempt or only when opened; (8) a
purchasable or lootable consumable master key; (9) trap rooms with good
loot that risk the whole party. And: "feel free to make this
interesting, the limit is the interest and how well it merges with
current mechanics." The approved documents owe this segment the DC
ladder, trap records, Locked Portal DCs, the DH-32 trap ids, the door
verbs, the Squeeze and Plank costs, the keyring and cell caches,
Hairline variants, theme trap flavours and the universal trap rows
(3.2).

The client's three answers (Q1–Q3, **agreed**, 2026-11-21) are in 0.2:
**a shown Hunch zone counts as a warning (Q1); the mimic is each theme's
own lurker in container form, at most 1 per floor on F2–5 from M3 (Q2);
and the basic vendor stocks 1 Wax Key only when the party has no
Mechanics-trained member, merged with the companions gap card (Q3).**

**Change prefix.** This design uses **TL-n** ("traps and locks"). A grep
for `TL-` across the 21 approved documents finds 0 hits. This document
issues one register, TL-1 to TL-43 (17; TL-42 and TL-43 added after the
check); no id has two meanings (the
research drafts' overlapping blocks TL-1..30, TL-30..44 and TL-40..60
are all renumbered into it). Local id schemes, each grep-checked as a
whole word with 0 hits:
- brief questions **BQ1–BQ9** **(new lead's call:** the proposal
  numbered them Q1–Q9, which collides with this document's client
  questions Q1–Q3**)**;
- tensions **KT1–KT26**;
- the check charter **CC1–CC8**;
- placement rules **TP1–TP12**;
- toy fixes **NF-a to NF-n**;
- Appendix A findings **KF** (fun), **KB** (balance) and **KK**
  (consistency).

The validator id **V11** is free (V1–V7 RS, V8 FL, V9 RT, V10 HZ); a
grep finds 0 hits.

**Names are placeholders** until the client naming pass. Each was
grep-checked case-insensitively as a whole word across the 21 approved
documents; the list, the hits read in context and the renames are in
A.5. **New lead's call:** the Supper's Wager room is renamed from the
proposal's "the Head of the Table" (the Supper boss, RS 8.9, and HZ
6.17's final course) to **the Top Table** (0 hits). **Lead's call, after
the check:** seven more renames clear collisions the check found (A.5):
Mourners' Veil → **Crepe Veil**, Overdue cage → **Closed-Stack cage**,
Silence Glyph → **Shush Glyph**, Pincushion Volley → **Darning Volley**,
Censer Tripwire → **Thurible Wire**, Paper-Cut Drawer → **Paper-Cut
Tray**, Spinning Target → **Blade Wheel**; and the Kennel's horn
tripwire is re-told as the **tin-can tripwire** (8; TL-43).

**Numbers in this document are illustrative** unless a rule says
otherwise, and are marked *(illustr.)* where they first appear. Every
rate and share comes from crude toy models (10, Appendix B); the M2
simulator owns every published figure.

Waves: **M1** slice, **M2** full run, **M3** between runs (profile,
unlocks, ladder), **M4** breadth, **Later** **(settled, main design
9)**.

Abbreviations: RC = the readability charter RC1–RC9 (RS 7.4); RE = RS's
tension ids; LG = FL's graph invariants; KP = RT's placement rules; VP =
HZ's placement rules; EnvDC, EnvRound and `CapRound` are HZ's (6.3.5,
6.11).

---

## 0. At a glance

### 0.1 The answers to the brief

**What a trap is, against a hazard (settled, HZ-1).** A hazard is
visible from reveal, acts by a rule, and is never found by a check or
disarmed. A trap is hidden until detected, or is a known armed device.
It is sprung by a trigger, and it can be detected, disarmed and
re-armed. The test is: "would detection change anything? Then it is a
trap." An open trapdoor is a Void (a hazard). A closed trapdoor that
springs is a trap. Cords never arm traps. This segment owns every trap,
every lock, the door verbs, the DC ladder and one-off checks.

**The design in one line.** *The house is rigged, and the house knows
it.* Most traps are tactical terrain that sits where fights already
happen. Natives step around the triggers but stand under the
chandeliers. The party arrives as intruders: it reads the tells, finds
the triggers, and turns the devices on their owners. Locks are optional
doors to optional loot. Every lock offers at least three routes and
shows its consequence on its face. Information is never a roll. A roll
exists only where a failure costs something, and every roll is previewed
with exact odds.

**BQ1. Should traps trigger on a failed disarm? Yes, but only on a bad
failure (lead's answer; change TL-4).** Every device check (disarm,
pick, hand-trigger) uses one graded result, the **Margin rule**:
- **Open**: the check meets the DC. The device is disarmed or opened
  silently.
- **Jam**: the check fails by 1–4. Noise 4. The device stays armed and
  live, but no hands can work it again this floor. Walking round, Bear
  it, Trigger Shot, Pole it and weighted plates all remain.
- **Botch**: the check fails by 5 or more. Jam, plus the device's own
  consequence: a trap springs.

Each object gets **one roll per hand verb per floor** (pick, disarm,
hand-trigger), drawn from a keyed stream, so reloading or swapping who
tries changes nothing. Force retries by its own rule (6.11), and Squeeze
and Plank roll once per traversal (6.22).

Reasons:
- The stat model already demands that every failure costs something
  (STAT 7.2, **agreed**: "No free retries… noise, a sprung trap, a
  jammed lock").
- If every failure sprang the trap, disarming would be barely safer than
  walking in, and players would stop disarming. At equal success, the
  toy shows the margin cuts springs from 28–36% to 8–16%.
- The fail-by-5 band is familiar from 5E, PF1 and PF2e, and it gives the
  exact three-part preview a deterministic game needs: *"Disarm (Scout)
  65% · Jam 20% · Botch 15%: springs, hits you, floored."*

**Told plainly:** out of combat, a trained member can **Take time** on
an Easy or Standard trap (no roll, 8 ticks of patrol movement). So for a
skilled party, BQ1 mostly bites on Hard devices, in combat, on
consequence locks and in Wager rooms. That is deliberate: risk sits
where the decisions are. To keep real rolls on every floor, from F2 at
least one out-of-combat trap or lock object per floor is tagged
`noTakeTime` (Hard, a consequence lock, or a tagged showcase such as
House Keys on Strings) **(lead's call, after the check; TP12)**.

**BQ2. If it triggers, is it the full effect? Yes, the trap's own full
damage and its normal save, with no hidden half-damage band (lead's
answer).** There are two carve-outs:
1. A Botch never fires a set-piece **hard part**.
2. **Where it lands follows the trap's geometry** (`botchHits`). A plate
   or catch hits the person working it. A wall-mounted device (a
   chandelier cleat, a portcullis winch) hits its area, and the worker
   may be standing outside that area. The rule card and the preview say
   which.

A half-strength botch would be a second, hidden rule (RC1), would need a
second preview, and would teach that botching is cheaper than walking
in. The fairness already exists:
- out of combat, everything is floored at 1 HP (DH 10.4, **agreed**);
- in combat, the trap was known, so the preview carries the lethal tag
  (DH 17.6);
- trap damage now counts inside the EnvRound cap;
- no single spring can exceed `CapRound` (TL-18).

**BQ3. Can players step on known traps on purpose? Yes, always (lead's
answer; change TL-12).** Entering a known trigger square by your own
movement is legal (RC1: the game never forbids a legal move because of
what you know). It is previewed. It asks for confirmation the first time
per trap. It gives **Bear it**: advantage on the trap's save, and every
damaging trap has a save.

Good reasons to do it:
- spend a one-shot trap before a fight;
- ring an alarm tripwire as a deliberate lure;
- open a trapdoor into a Void before a fight (F2+, in a proven Void
  room, 6.3);
- spring a device whose area holds enemies;
- save the ticks of a disarm out of combat.

**Told plainly:** Bear it is *not* the universal answer for a party
without Mechanics. The toy shows that bearing 30% of route traps costs
such parties 5–7.6 percentage points of floor survival, because floored
out-of-combat chip still arrives at the next fight. The universal answer
is **walk round**. Every trap leaves a detour of +4 squares or less
(`TrapNeverSeals`), and auto-walk takes that detour by default. The
Bear-it preview shows "HP after: X, until next rest".

Every party also gets ways to spring a trap from a distance in M2:
Trigger Shot (replaces one attack), the hand-trigger (an action), **Pole
it** (a reach weapon within 2) and weighted plates (Shunt or Kick a
piece onto it).

**Told plainly:** in this ruleset, "a character with a high enough stat
notices" means **Intelligence**. Trap sense is 10 + Int, plus
proficiency when trained in Mechanics. Perception finds lurkers, the
boxed lurker (the mimic) and the "wide berth" clue. Medicine smells
poison traps, and Arcana reads rune traps.

**BQ4. Do enemies trigger hidden traps, pass through them, or pathfind
around them? Natives know their floor and path around trigger squares,
but they stand in device areas ("the house trusts its own traps")
(lead's answer; change TL-16).** That is the main lure play: spring the
chandelier while they are under it. Details:
- A native visibly detouring around empty floor (the **wide berth**) is
  a fair clue, and seeing it turns that trap into a Hunch for the party.
- **Mindless types** treat non-lethal traps as floor and blunder in. The
  stupidity floor (ENM 11.3 #7) still keeps everyone out of lethal ones.
- A monster springing a trap in the party's sight Reveals it.
- Pushes onto known triggers use the hazard-edge save. Monsters get no
  floor (DH 10.4).

Three fairness rules close the exploits:
1. **Once Bitten.** A group hurt by a device spring stops trusting
   *every* known device area for the rest of the floor.
2. **The opener rule.** A spring on an unaware group starts combat and
   *is* the springer's surprise action. It never adds a free round.
3. **Revealed-only scoring.** The AI scores only traps the party has
   Revealed when it pushes party units, so it never weaponises
   information the party lacks.

Traps the party re-arms out of a group's sight, unknown to that group
(per-group belief), arrive in M3 with the Trapwright (TL-17).
*Rejected:* monsters setting off hidden traps at random (that reads as
luck), and monsters ignoring traps altogether (that kills push and
pull).

**BQ5. Special locks with a consequence on a failed pick? Yes (lead's
answer; change TL-22).** Each kind has a glyph on the padlock decal
(padlocks only, RC3; one glyph per rule, RC5) and a rule card that
everyone can read before touching it. The consequence fires on a pick
Botch, and for some kinds on any force. The lock kind decides which verb
to use:

| Kind | Wave | Pick Botch | Forcing |
|---|---|---|---|
| **Plain** | M1 | Jam only | Normal |
| **Crier** | M2 | Noise 10 at the lock | Every force attempt is noise 10 |
| **Barb** | M2 | Harm-band damage plus the theme's poison condition on the picker; floored out of combat | Never fires (forcing is safe here) |
| **Tattle** | M2 | Wakes the existing, pre-placed Dormant group beyond, which Listen reports first | Wakes them too |
| **Runed** | M3 | — | — (Arcana may pick it) |
| **Hour** | M3 | — | — (opens itself on the floor's Landmark tick) |

A consequence lock's cache draws +1 rarity step inside the fixed budget.
*Rejected:* summoning guards (it breaks the encounter budget and reads
as a gotcha), floor-wide alarms (FL 9.1), destroyed contents (ENM 15.6)
and broken picks (there are none).

**BQ6. Should lockpicking require consumable lockpicks? No (lead's
answer, contested; confirmed by the client on 2026-11-21; change TL-26).**
- ITEMS 2 (lead's call) cut "tools as a category: dungeon checks are
  party-best skills and tools would only add bookkeeping". "Who has the
  key" is a settled anti-goal.
- Picking is already trained-only, so picks would gate the same parties
  twice.
- The toy confirms the genre pattern. Scarce picks lock up to 1.9 locks
  per floor behind luck. Abundant picks are a 5–40 coin tax with no
  decision in it. A counter spent only on a Botch barely moves (0–0.19
  per floor).

The consumable fantasy goes to the **Wax Key** (BQ8), which serves the
parties that *lack* the skill instead of taxing those that have it, and
to the optional **Oil the Works** (M3: an existing oil flask gives
advantage and a silent Jam). The client confirmed "no lockpicks" on
2026-11-21. If the client ever reopens BQ6, the least harmful form of
picks is a size-0 party counter spent only on a Botch (TL-26; 19.2). The
lead rates it lower.

**BQ7. Mimic chests: trigger on a lockpick attempt, or only when opened?
Any touch wakes it (open, pick, force, key or attack); adjacency never
does. Who touches it decides how (lead's answer; the route agreed, Q2;
change TL-28).**
- A Mechanics-trained hand trying to pick it "feels teeth". The mimic is
  revealed before it bites: no Ambush Strike, no damage, nobody
  Surprised. Its lid still grips the hand (Grappled), so the reveal is a
  moment, not a menu result. Picking is therefore the skilled player's
  safe test, and Open auto-routes to the best Mechanics member, or else
  to the highest-HP member.
- An untrained open or force takes the Ambush Strike. If nobody had
  marked the mimic, that strike is floored at 1 HP and capped at 20% of
  the lowest legal hero's HP. In the toy this cuts excess deaths for new
  players from about 12% of runs to 0.08%.
- A mimic the party marked first can be attacked while it is Surprised,
  or (M3) Fed 1 supply to hand over its contents with no fight.
- The Wax Key "will not take a shape": on a padlocked boxed lurker it
  reveals the mimic and is not spent. A boxed lurker that presents as an
  unlocked container refuses the Wax Key before any spend, exactly like
  a real one, so the key is never a free detector **(lead's call, after
  the check; 6.14)**.

The mimic is the theme's **own lurker in container form**, the **boxed
lurker** (the Silk's Margin-Crawler in a catalogue drawer, the Gut's
Gutworm in a swallowed strongbox) **(agreed, Q2)**. It is not a
keyhole-wearing new type. Its tell is that lurker's own at-rest tell,
which the player has already learned (RC2), re-authored at container
scale. Party passive Perception can also mark it. The Lore roster
lists the lurker's type on floor entry, as it always does (STAT 7.1, ENM
10.6), but it does not say whether that lurker is boxed **(told plainly
after the check:** the proposal's "a floor without one carries no
paranoia" is withdrawn, since it would need a new placement-level
reveal in agreed text**)**. M3, F2–5, at most 1 per floor; more themes
gain one as they gain lurkers.

**BQ8. A purchasable or lootable consumable master key? Yes: the Wax Key
(lead's answer; change TL-24, TL-25)**, "a key of grey wax that takes
the shape of one lock, then sets" (RC9: the name tells the truth).
- **Form:** a run-scoped party counter (cap 2, no carrier) priced
  24/28/32/36 coin by camp depth *(illustr.)*. Out of combat only.
- **Opens:** any one Locked Portal, locked container or Jammed lock,
  silently, without firing the lock's consequence.
- **Never opens:** the Bossgate, Grilles, a Latch from its blind side,
  secrets, Riddle locks or Wager cages. It never locks anything.
- **Channels:** sundries from F2; a camp notables slot; and **the basic
  vendor, which stocks 1 Wax Key only when the party has no
  Mechanics-trained member, merged with the companions gap card (agreed,
  Q3; 6.11)**.

It converts coin into access, never into loot beyond the fixed budget.
Its job is to give a solo Vanguard, or a party whose Scout died, a quiet
route to optional loot, and to give Mechanics parties a way past
consequence and Hard locks.

**BQ9. Trap rooms with good loot, risking the whole party? Yes: Wager
rooms, M3 (lead's answer; change TL-29).** They share the single
Dare-room slot (at most 1 Dare-or-Wager per F2–5 floor). They are
signposted, the prize is visible through a Grille before you commit,
"traps: n" shows on entry, and the exits are never sealed.

**Told plainly:** under the agreed DH rules, traps out of combat cannot
kill. So "risking the whole party" has to run through combat:
- **Unguarded form.** A visible 3-pip **Disturbance track** fills one
  pip per resolved event: a spring, a Jam, a Botch (one pip, not three),
  any act of noise 4 or more, each use of Take time, every 8 ticks of
  other careful work, and the lift of the prize (the greed trigger)
  **(one rule: lead's call, after the check; 6.15)**. At 3 pips it wakes
  a full-strength guard group from the floor's own budget, and the party
  fights hurt on ground whose traps are now known and lethal to both
  sides. The reward is graded on the pips before the lift: 0 pips,
  choose 1 of 3 caged prizes; 1–2 pips, choose 1 of 2; guard woken, the
  plinth prize only. No route to the prize walks round every trap.
- **Guarded form.** A garrison stands among revealed devices, and the
  party wins by turning the room on them.

Rewards stay inside the fixed budget, and experience still comes only
from monsters.

**The Trapwright (DEP-S1): the agreed fallback is applied (SH 14, agreed
Q2; told plainly; change TL-37).** The Swordsworn ships in M2. The
Trapwright is the first Shade unlock in M3, as the client asked ("keep
it for later").
- **What this segment ships in M2:** every trap record field the
  Trapwright needs (SH 8.2 plus this document's fields), the universal
  hand-trigger and Trigger Shot, and the universal 500‰ re-arm.
- **What runs as an M2 report, not a generator gate:**
  `TrapwrightHasMechanism` (with its dead-room rate) and
  `SpringContentBound`, the SH-16 density check. The call is made once,
  at the M2 planning gate (SH 14), and rests on the client's agreed
  default, "keep it for later": this design does not put the SH-16
  check in the M2 gate set, so the fallback applies automatically.
  **Told plainly after the check:** TP12 (6.20) is an M2 generator gate
  whose main-path minimums overlap SH 8.2(a)'s trap-on-path guarantee.
  It is kept because every party needs those traps, not for the
  Trapwright, and it is not the SH-16 check: it carries no dead-room
  rate and no `SpringContentBound`. So the overlap does not move the
  Trapwright into M2.
- **What arrives at M3:** the density gate, per-group belief and
  witness-only secrecy for party-armed traps, and the Trap Eye re-scope.

This is told plainly, not asked. Model C's draft line "the Trapwright
places traps" is rejected: SH 8.2 says it never builds, and placing
traps is the Sapper's lane (SH-26).

### 0.2 The client's three decisions (agreed, 2026-11-21)

Each was answered with the recommended option (A); the client's choices
are final.

| # | Question | Answer **(agreed)** | Rejected | Change |
|---|---|---|---|---|
| Q1 | Warned or floored for traps (narrows the agreed DH 10.4 / DH Q6 floor) | **A.** A shown Hunch zone counts as a warning. Out of combat, or when pushed in, it stays floored. Walking in by choice in combat, the preview shows the floor's worst trap with the lethal tag (6.5) | B: Hunch zones fully floored like hidden traps (an anti-detection incentive). C: no Hunch zones, traps only Revealed or Hidden (20–57% of traps unseen for no-Mechanics parties) | TL-8 ⚑ |
| Q2 | Mimic route | **A.** The boxed lurker: each theme's own lurker in container form (Silk Margin-Crawler drawer, Gut Gutworm strongbox) in M3, more themes as they gain lurkers, at most 1 per floor, F2–5; no roster exception (6.14) | B: a cross-theme mindless keyhole mimic (needs an exception to RE9, V1 and the roster records) | TL-28 ⚑ |
| Q3 | Wax Key at the basic vendor (changes the agreed ITEMS 10.3 list) | **A.** The basic vendor stocks 1 Wax Key only when the party has no Mechanics-trained member, merged with the companions gap card (6.11) | B: Wax Keys from loot and the camp notables slot only | TL-25 ⚑ |

### 0.3 Decisions in brief

| # | Decision | Wave | Marking |
|---|---|---|---|
| 1 | Check charter CC1–CC8. Three check families: object checks, unit contests, thresholds | M1 | lead's call; TL-1 |
| 2 | One DC ladder: Standard = EnvDC, F1 Standard = Plank 12; Easy −3, Hard +4, no Hard on F1; force = DC +2 | M1 | lead's call; TL-2 |
| 3 | `traps` generation stream; keyed `checks` stream for object checks and out-of-combat trap saves | M1 | lead's call; TL-3 ⚑ (ABL 14.4 settled, main 6.3 decided, extended) |
| 4 | Margin rule (Open / Jam / Botch), one roll per object per hand verb per floor; force retries; Squeeze and Plank per traversal | M1 locks, M2 traps | lead's answer, BQ1/BQ2; TL-4 |
| 5 | Take time: Easy and Standard only, out of combat, 8 ticks; not on consequence locks, Squeeze or Plank | M1 locks, M2 traps | lead's call; TL-5 |
| 6 | Detection by trap sense with Mechanics proficiency; Revealed / Hunch (−5) / Hidden; Probe from inside or beside a Hunch zone; no search verb | M2 | lead's call; TL-6 ⚑ (STAT 6, agreed), TL-7 |
| 7 | Warned or floored for traps: a shown Hunch zone is a warning | M2 | **agreed, Q1**; TL-8 ⚑ (narrows DH 10.4, agreed) |
| 8 | Three families: Devices (body always visible, always rigged), Snares, Catches | M2 | lead's call; TL-10 |
| 9 | Bear it, step over, walk-round default; universal spring rows; weighted plates; Pole it; handles | M2 | lead's answer, BQ3; TL-12 to TL-14 |
| 10 | Knowledge model M2: natives avoid triggers and trust areas; Once Bitten; mindless blunder; Revealed-only AI pushes | M2 | lead's answer, BQ4; TL-16 (enemies owner) |
| 11 | Damage bounds: hideable traps ≤ 20%, devices and set pieces ≤ `CapRound`; traps inside EnvRound except self-caused | M2 | lead's call; TL-18 |
| 12 | Floor-1 package: lesson trap first; nothing fight-adjacent; no push edges; conditions end at combat start | M1/M2 | lead's call; TL-19 |
| 13 | Locks: retryable force with a guarantee; Splinter from F2; consequence kinds; Keybearers; Wax Key; no lockpicks | M1–M3 | lead's answers, BQ5, BQ6, BQ8; TL-21 to TL-26 |
| 14 | Wax Key at the basic vendor when no Mechanics member, merged with the gap card | M2 | **agreed, Q3**; TL-25 ⚑ |
| 15 | Boxed lurker (the mimic) | M3 | lead's answer, BQ7; **agreed, Q2**; TL-28 ⚑ |
| 16 | Wager rooms sharing the Dare slot | M3 | lead's answer, BQ9; TL-29 |
| 17 | DEP-S1 fallback applied; Trapwright M3 | M2 gate | told plainly; TL-37 ⚑ |
| 18 | Segment-5 clock allowance ≤ +0.3 min and ≤ +5 clicks at party 4, F4–5, with mandatory levers | M2 | lead's call, told plainly; TL-38 |
| 19 | V11 `TrapsLocks`, reports, arms, `trapDigest` | M1 skeleton, M2 | lead's call; TL-20, 11 |

---

## 1. Intent

The stat model gave the dungeon one skill for traps and locks
(Mechanics, Int), one loud alternative (Athletics), party-best checks,
trained-only hand work and "no free retries" **(agreed, STAT 7.1,
7.2)**. The death and healing design floored out-of-combat and
undetected-trap damage and made detected traps lethal and tagged
**(agreed, DH 10.4, Q6)**. The Shade design wrote the trap record the
Trapwright needs and the fallback if it is late **(agreed, SH 8.2,
14)**. The floor layout design placed Locked Portals, Hairline secrets
and Squeezes; the room terrain design wrote the Plank; the hazards
design drew the hazard / trap line and the EnvDC and EnvRound numbers
this segment must cite. None of them said what DC a lock has, what a
failed disarm does, how a trap is seen, whether monsters know their own
traps, or what a chest may hide.

The answer in one sentence: **traps are mostly visible-bodied devices
placed where fights happen and known to the natives who live beside
them; they are found by an Int threshold, never by a roll; every roll
that exists costs something and shows exact odds; every lock is
optional, shows its consequence and has at least three routes, one of
them open to anyone; and nothing hidden can kill.**

## 2. Goals, non-goals, anti-goals

### Goals

- **Traps are tactical terrain both sides use:** devices sit where
  fights happen, natives stand in their areas, and the party turns them
  on their owners (6.8, 6.9).
- **Int matters, and so does Mechanics training,** without gating
  anything: detection is an Int threshold with proficiency; every trap
  and lock has an untrained route (6.1 CC4, 6.17).
- **Every failure costs something, and the cost is shown before the
  roll** (STAT 7.2; 6.1 CC2, CC3).
- **Locks are interesting choices,** not taxes: consequence kinds read
  on their face, three or more routes, and a master key that serves the
  parties that lack the skill (6.11).
- **Nothing hidden kills** (DH 10.4), and a known trap is never a safe
  tool (6.5, 6.10).
- **Every seed works:** V11 proves placement at build time, and every
  act-time check is reload-proof (6.22, 11).
- **The clock is respected:** segment 5 spends at most +0.3 min and +5
  clicks per floor at party 4 on F4–5 (6.21, TL-38).

### Non-goals

- **Hazards.** Segment 4 (HZ-1; 13).
- **Encounter fill, loot tables, events and boss interiors.** Segments
  6–9; this document hands them hooks (13).
- **Party-placed traps.** The Sapper's lane (SH-26); Later.
- **Monster trap use, chained springs.** Later (SH 14).
- **New condition ids.** Every trap reuses its theme's 6 (C 16; TL-39).
- **Final names, numbers and art:** the naming pass, the simulator and
  the art team.

### Anti-goals

- **Murder by information:** a hero killed by a trap it could not have
  known about.
- **Search spam:** a search verb used at every dead end (NetHack), or
  rolls re-made until they pass.
- **Tool tax:** consumable picks or kits (ITEMS 2); "who has the key"
  (settled).
- **Gotcha content:** mimics with no tell, decoy device bodies, fake
  plates, empty "trapped" chests.
- **Gated content:** anything required behind a check, a class or a
  skill (LG2, LG14, CL 6.1 rule 4).
- **Loot loss:** nothing destroys contents (ENM 15.6).
- **Rerolling by swapping:** a check result that changes with who tries
  or with a reload.

## 3. Constraints

### 3.1 What the parents already fix

The 21 approved documents were grepped for trap, lock, key, Mechanics,
trap sense, disarm, mimic, chest, Hairline, Latch, Portal, Dare room,
vendor, Trapwright and DEP-S1, and each hit was read in context. These
are honoured as written.

- **Skills (STAT 5.4, 6, 7.1, 7.2):**
  - ten skills; no Investigation, no Sleight of Hand, no Thieves' Tools;
  - Mechanics (Int) finds and disarms traps, picks locks quietly and
    finds secrets;
  - Athletics forces doors, bars and chests loudly;
  - dungeon checks use the best party member **(agreed)**;
  - pick, disarm and re-arm are trained-only **(lead's call)**;
  - "No free retries" **(agreed)**;
  - no natural 20 or 1 on skill checks;
  - trap sense = 10 + Int of the best member, with no proficiency
    (STAT 6, **agreed**; changed by TL-6);
  - passives include proficiency when trained;
  - the Mechanics mastery signature slot is open (the builders').
- **DH 10.4 (agreed, Q6):**
  - out-of-combat damage is floored at 1 HP;
  - undetected-trap damage is floored in and out of combat, and a trap
    that starts combat counts as undetected;
  - the `detectedAtFire` snapshot (widened to an enum by TL-8);
  - detected traps in combat are lethal and tagged (17.6);
  - "hidden information never kills, and a known trap is never a safe
    tool";
  - DH-32: trap ids and the snapshot are M1.
- **C 11.3, 11.6, 7.2:**
  - known armed traps are hazards for forced movement, pathing and
    followers (SH-15, M2 regardless);
  - trap hard control fires only when sprung, follows the own-turn rule,
    never appears on F1, appears on F2 only in set pieces, and counts
    toward the per-fight cap;
  - RT-16 is open;
  - C 10.4: no single failed save is lethal to the hero.
- **ENM:**
  - the AI reads true state (11.2);
  - the stupidity floor (11.3 #7);
  - noise radii 4 / 6 / 8 / 10, −3 per closed door (12.3);
  - Locked Portals stop monster paths (12.4);
  - interrogation names "the nearest undiscovered trap" (14.6);
  - Steal is one try per carrier per floor (15.5, **agreed**);
  - loot is never destroyed (15.6);
  - XP comes only from monster groups (16);
  - the Ambush template's bait may be a container (17.4).
- **FL:**
  - Locked = Door with the padlock decal; pick quiet or force loud 6;
    optional content only (LG2, LG14, `SecretNeverCritical`);
  - Hairline: no roll, one tell, no decoys (**agreed**, FL Q3);
  - Squeeze on "the existing check stream";
  - `PortalMinimums` trap sockets;
  - FL-8 door verbs owned here;
  - the Gaol keyring cache within 1 room of the Turnkey;
  - the Strongroom spur (M3);
  - Drop-bar returns to the client.
- **RT:**
  - Plank DC 12;
  - a Furnishing never springs a trap ("segment 5 may narrow");
  - trap damage is outside the one-instance cap;
  - Kegs are sturdy;
  - Portals never break, but "door shattering stays segment 5's";
  - `NoCheapKill` (**agreed**, RT Q3);
  - RT-20: no hazards on F1.
- **HZ:**
  - the HZ-1 line;
  - the sprung twins (HZ 6.1);
  - EnvDC 13/14/15 ("segment 5's ladder should cite this");
  - the EnvRound cap (does not yet include traps);
  - Warned or floored;
  - Dare rooms (M3, at most 1 per floor);
  - HZ limit 2: no act-time draws except saves.
- **RS:**
  - RC1–RC9: RC3 reserves the keyhole for mindless types, "locks read as
    padlocks"; RC7 reserves trap and alarm cues;
  - RE9: at most 1 mindless type per theme;
  - V7;
  - per-theme trap flavours;
  - the Turnkey keyring;
  - "Bell" head words are avoided beside the Herald's Passing Bell.
- **ITEMS:**
  - tools are not a category **(lead's call)**;
  - "who has the key" is an anti-goal **(settled)**;
  - keys are `party(keys)`, size 0, floor-scoped (5.3);
  - the agreed basic vendor list (10.3);
  - coin never creates loot beyond the budget.
- **CMP:** the gap card (7.3 G2) with Mechanics third in `gapOrder`,
  "met" when any party unit, the hero included, is trained; the time
  model and lever order (7.4).
- **SH:**
  - the DEP-S1 record fields (8.2);
  - `TrapwrightHasMechanism`;
  - `SpringContentBound`;
  - NM-1;
  - Sure Hands (`delve`, 7.11);
  - Jury-Rig's per-fight jam and its once-per-floor re-arm; "the
    universal action route stays unlimited at its action cost" (8.2);
  - the agreed Q2 automatic fallback (14);
  - "never builds" (8.2).
- **Main 6.3 (decided):** "Opening a chest must not change the next
  attack roll." **ABL 14.4 (settled):** "Every roll uses the combat
  stream."
- **Clock:**
  - floors run 15–20 min;
  - FL 6.10 `ExploreSeconds` ≤ 3.5 min and `ExploreClicks` ≤ 40 at party
    4 on F4–5, passing only narrowly;
  - terrain and hazards already use the +1.6 tolerance;
  - segment 5 has no allowance.

### 3.2 What the approved documents owe this design

Each is met in section 14.

| # | Owed | Source |
|---|---|---|
| O1 | DCs and the ladder | FL 9.2, RT 13, HZ 13 |
| O2 | Trap records and `TrapwrightHasMechanism` | SH 14, RS 14 |
| O3 | Locked Portal DCs in M1 | FL 12 |
| O4 | The DH-32 trap ids and snapshot in M1 | DH-32 |
| O5 | The FL-8 door verbs | FL-8, ABL 7.2 |
| O6 | The Squeeze check and the out-of-combat Plank cost | FL 9.2, RT 6.2.6 |
| O7 | The keyring and cell caches; sphincters | RS 8.3, 8.10, FL 7.2 |
| O8 | Drop-bar and Wedge staging | FL (Drop-bar with the client), SH-18 |
| O9 | Hairline variants | FL 9.2 |
| O10 | Theme `trapFlavours` | RS 8.0–8.12 |
| O11 | Trap conditions under RT-16 | C 11.6, RS 8.0 |
| O12 | The CMP 7.4 time-model rows | CMP 7.4 |
| O13 | The universal trap rows if the Trapwright falls back | SH 14, C 11.6, ABL 7.2 |

### 3.3 Tensions found and their resolutions

| Id | Tension | Resolution |
|---|---|---|
| KT1 | FL's "existing check stream" does not exist; main 6.3 conflicts with ABL 14.4 | Keyed `checks` stream (TL-3), flagged |
| KT2 | Trap sense carries no proficiency, so Mechanics training is inert for detection; 40–63% of traps stay Hidden | Proficiency when Mechanics-trained (TL-6) |
| KT3 | DH 10.4 against "risk the whole party" | Wager risk runs through combat (TL-29), told plainly |
| KT4 | C 10.4 against lethal detected traps; traps escape every cap | Traps inside EnvRound, `TrapLethalityBound` ≤ `CapRound` (TL-18) |
| KT5 | Lockpicks against ITEMS 2 | No picks (TL-26) |
| KT6 | Master key against floor-scoped keys | Run-scoped counter, not `category: key` (TL-24) |
| KT7 | Keyhole mimic against RE9, RC3 and V7 | `containerForm` of the existing lurker (TL-28) **(agreed, Q2)** |
| KT8 | Gaol tripwires on the Bypass against the stealth guarantee | `NoiseTrapVisible`, `BypassClean` (TP7, TP8) |
| KT9 | Known F1 traps would create hazard edges (TR-29) | Pushes stop before F1 triggers (TL-19) |
| KT10 | Boss chandelier: RT 6.11 against segment 9 | Arena instances belong to segment 9 and use this segment's records (TL-40) |
| KT11 | No clock allowance | Allowance as a new budget line plus mandatory levers (TL-38) |
| KT12 | M1 owes trap ids and lock DCs | M1 row (12) |
| KT13 | "Shattered door" noise against "Portals never break" | Splinter breaks the panel, the Portal stands (TL-21), told plainly |
| KT14 | Universal trap rows have no numbers if the Trapwright falls back | Written here (TL-13, TL-15) |
| KT15 | Model C's "Trapwright places traps" against SH 8.2 | Rejected |
| KT16 | A Hunch counted as undetected rewards not detecting | Warned or floored (TL-8) **(agreed, Q1)** |
| KT17 | The EnvRound cap could launder deliberate springs | Self-caused damage is counted outside the cap (TL-18) |
| KT18 | Area trust plus universal re-arm farms unaware groups | Once Bitten, the opener rule, re-arm once per floor (TL-15, TL-16) |
| KT19 | Hidden noise traps start lethal fights | `NoiseTrapVisible` (TP8) |
| KT20 | Out-of-combat trap saves on the combat stream break main 6.3 | Those saves go on `checks` (TL-3) |
| KT21 | Jams that end with the fight give a free second roll | Universal Jams last to floor end (TL-4) |
| KT22 | Take time erases Sure Hands and BQ5 | Not on consequence locks or Hard; Sure Hands cuts its ticks (TL-5); Shade owner told |
| KT23 | Out-of-combat traps become detour chores | `TrapHasStake` (TP5) |
| KT24 | Making the hand-trigger "replace one attack" changes a settled ABL row | Kept as an Action; only Trigger Shot replaces an attack (TL-13) |
| KT25 | Exploration "has no clock", yet ticks move patrols | Ticks are the real price; the patrol forecast is shown on every card |
| KT26 | Wager guard: an XP faucet and retry scouting | Guard drawn from the floor budget; pips persist; one attempt after a wake (TL-29) |

### 3.4 Terms

| Term | Meaning |
|---|---|
| **Trap** | Room content that is hidden until detected, or a known armed device, sprung by a trigger, and detectable, disarmable and re-armable (HZ-1; 6.3) |
| **Device** | A trap whose `area` is separate from its trigger; its body and `mechanismSquare` are always visible, and every body over walkable squares is rigged (6.3) |
| **Snare** | A point trap: the effect lands on the trigger square (6.3) |
| **Catch** | A trap on a lock, lid or Door (6.3) |
| **Set piece** | A known device of up to 9 squares, at most 1 per F2–5 floor; the only home of a trap's hard part (6.3) |
| **Handle** | A mechanism that springs its device with an action and no check (6.7) |
| **Margin rule** | The graded result of every object check: **Open** (meets the DC), **Jam** (fails by 1–4), **Botch** (fails by 5+) (6.6) |
| **Take time** | A no-roll, out-of-combat, channelled threshold on Easy and Standard devices and Plain locks: 10 + bonus, 8 ticks; never on `noTakeTime` objects (6.6) |
| **Trap sense** | The best 10 + Int among living, non-Downed party units, plus proficiency for a Mechanics-trained unit (6.4) |
| **Revealed / Hunch / Hidden** | The three detection grades; they never drop (6.4) |
| **Hunch zone** | A true zone, with a seeded offset drawn at generation, that contains every trigger square of a Hunch-grade trap and no other trap's trigger; never a decoy (6.4) |
| **Probe** | A no-roll, noise-0 act, from inside or orthogonally beside a Hunch zone, that reveals every trap whose trigger lies in the zone (6.4) |
| **Hunch stop** | Auto-walk's stop on the last square before a new Hunch zone: Probe, Walk round or Push through; out of combat it auto-Probes unless a patrol is due (6.4) |
| **Wide berth** | A native step in party sight whose shortest path crossed an armed trigger it avoided; raises that trap to at least Hunch (6.4) |
| **Bear it** | Entering a known trigger by your own movement, as an explicit choice: the trap springs with advantage on its save (6.7) |
| **Pole it** | A reach-weapon user springs a known plate, wire or catch host within 2 (6.7) |
| **Weighted plate** | A Furnishing whose Shunt, Kick or Overturn ends on a plate or trapdoor springs it (6.7) |
| **Once Bitten** | A group with any member inside the area of a resolved device spring treats every known device area as a hazard for the rest of the floor (`bitten`) (6.8) |
| **Opener rule** | A spring that affects an unaware group is the springer's surprise action; it never adds a round (6.8) |
| **Consequence lock** | A Crier, Barb, Tattle or Runed lock; its glyph and rule card show the consequence (6.11) |
| **Splinter** | From F2, a force Botch on a Door breaks its panel and opens the lock: noise 10, it never closes again; the Portal stands (6.11) |
| **Keybearer** | The one unit per floor that carries the floor key, shown by a key pip on its plate (6.11) |
| **Lockside Listen** | Picking or key-opening a door lock also Listens through it (6.11) |
| **Wax Key** | A run-scoped party counter (cap 2) that opens one ordinary lock silently, without its consequence (6.11) |
| **Riddle lock** | An M3 lock opened by a clue found elsewhere on the floor, never by a check (6.11, 7) |
| **Boxed lurker** | A theme's existing lurker placed in container form (`containerForm`) (6.14) |
| **Wager room** | A signposted trap side room sharing the Dare slot, with caged prizes (6.15) |
| **Disturbance track** | A Wager room's visible 3-pip meter, one pip per resolved event; the room's one clock; at 3 its guard wakes (6.15) |
| **`trapsResolve`** | The second generation pass, after segment 6 places encounters: binds Tattles and Keybearers and checks the placement rules that need monsters (6.22) |
| **Lesson trap** | The first main-path trap on F1: `alwaysKnown`, labelled, noise only (6.19) |
| **`checks` stream** | The keyed act-time stream for object checks and out-of-combat trap and lock saves (6.22) |

---

## 4. How other games handle traps, locks and mimics

Sources were checked where marked (bg3.wiki, DCSS 0.23 notes, rephial
Angband 4.1.0, Dized Gloomhaven, AoN PF2e). The rest is from memory and
treated as such.

| Game | What it does | We take | We avoid |
|---|---|---|---|
| Baldur's Gate 3 (checked) | Hidden proximity rolls; consumable disarm kits; picks break; shove into traps | Party-wide passive detection; shoving into traps | Tool tax; unseen re-rolled detection that invites reloads |
| DCSS 0.23 (checked) | Generated ground traps revealed by default; most mechanical traps cut | Traps as visible tactical terrain | Per-square hidden damage |
| Angband 4.1 (checked), NetHack | Active searching removed (Angband); search spam (NetHack) | No search verb anywhere | Searching every dead end |
| 5E DMG, PF1/Kingmaker, PF2e | Fail by 5+ springs; PF2e critical failure | The Margin rule | Unbounded hidden damage |
| Pillars of Eternity | One Mechanics skill; optional picks bridge a gap | One skill; consumables that bypass, not gate | Scouting-mode tax |
| Darkest Dungeon | Curio and provision pairing; failed disarm springs | Keys as optional shortcuts | Must-pack checklists |
| Dark Souls, Gungeon, Isaac | Mimic tells (chain, wiggle, spikes); attack-to-test | A learnable tell that never lies; tests have a price | Tell-less gotchas |
| Spelunky, Grimrock | Anything triggers; a crate on the plate; the greed trigger | Weighted plates; the greed trigger | — |
| Gloomhaven (fan rules site) | Monsters avoid traps unless no path exists; push and pull | Natives avoid triggers; forced movement | — |
| Hades, Brogue, Isaac | Stakes shown before entry; choose-one vaults | Wager room signposting; cages | Hidden stakes |
| Invisible Inc., Thief, Dishonored | Consequence known before acting; noise as the cost | Consequence glyphs; noise as failure currency | — |
| Skyrim, Fallout | Picks abundant and meaningless | — | Consumable picks |
| Deus Ex | Several routes per door | At least three routes per lock | — |
| Into the Breach, XCOM | Exact previews; distrusted odds | Exact three-part previews from an honest stream | Fudged odds |
| Wizardry, Etrian Odyssey | Opaque chest-trap identification | — | Opaque, run-ending outcomes |
| The Alexandrian (three-clue rule), OSR | Telegraph traps; several clues | Wide berth, Hunch zones, count line, interrogation | Choice-free detect-then-disarm |

**Lessons that bind the design:**
1. Information is a threshold; rolls only where failure costs.
2. Show the consequence before the touch.
3. Several routes per obstacle, one open to anyone.
4. Tells never lie; no decoys.
5. No tool tax.
6. Monsters live with their traps, and that is the lure.
7. Exact odds from an honest, reload-proof stream.

---

## 5. Trap models

### 5.1 The models

- **Model A, classic checks.** The 5E loop. Notice = max(passive
  Perception, trap sense). Room-scoped Search. The Touchy second roll.
- **Model B, information first.** Every trap at least a Qualm zone.
  Probe. Take Time. A diegetic tell per kind.
- **Model C, traps as tactical terrain.** Devices where fights happen.
  Natives trust areas. Graded detection. A check charter.
- **Layer L, locks, keys and checks.** Margin rule, Take time, keyed
  stream, consequence locks, Wax Key, Keybearers.
- **Hybrid M (new mechanics M-1 to M-16).** The cleanest grammar:
  Open/Jam/Botch, band −5, Careful work, `containerForm` mimic.

### 5.2 Judges' scores (1–10)

Three judges scored every proposal ("low build cost" is scored high when
the cost is low).

| Judge | Model | Interest | Fairness | Low tedium | Theme | Determinism, AI | Class spread | Fit | Low build cost |
|---|---|---|---|---|---|---|---|---|---|
| 1 | A | 6 | 7 | 4 | 6 | 7 | 7 | 5 | 6 |
| 1 | B | 7 | 9 | 6 | 7 | 8 | 6 | 7 | 4 |
| 1 | C | **9** | 8 | **8** | **9** | 7 | **8** | **8** | 5 |
| 2 | A | 6 | 6 | 4 | 6 | 7 | 6 | 5 | 6 |
| 2 | B | 6 | **9** | 6 | 7 | **9** | 6 | 7 | 4 |
| 2 | C | **8** | 7 | **8** | **8** | 6 | **7** | 7 | 5 |
| 2 | M (hybrid) | 8 | 8 | 7 | 7 | 8 | 7 | **8** | **6** |
| 3 | A | 6 | 7 | 4 | 5 | 7 | 6 | 6 | 6 |
| 3 | B | 6 | **9** | 6 | 6 | **9** | 6 | 7 | 4 |
| 3 | C | **9** | 8 | **8** | **8** | 8 | **7** | **8** | 5 |

### 5.3 Verdict and corrections

**Model C is the chassis (lead's call; stands as written), built on
layer L's check core, using hybrid M's grammar and Model B's fairness
ideas, and corrected by the numbers pass and the three critiques.**

- **Why C.** It is the only model that fits the clock with its levers
  (+0.20 to +0.27 min against A's +0.56 to +0.68 and B's +0.51 to
  +0.67). It also puts traps where the game is about combat.
- **Why not A.** Its max(Perception, trap sense) detects 87–100% of
  traps, which leaves Search with no content and kills the Int lane. Its
  second roll adds springs.
- **Why not B.** It carries the most art and the most clicks, and
  treating every trap as a Qualm flattens what Int is worth.

Corrections applied to C:
- drop `unitId` from the checks key;
- defer per-group belief to M3;
- AI pushes score only Revealed traps;
- `NoCheapKill` covers traps;
- no rigged devices on F1;
- device bands at about 0.9 × `CapRound`;
- all of A.1 to A.3.

---

## 6. Rules in full

### 6.1 Check charter (lead's call; change TL-1)

- **CC1.** Information is a threshold, never a roll: trap detection,
  Hairline tells, Listen, readings, mimic marks.
- **CC2.** A roll exists only where a failure costs something, and that
  cost is printed on the preview.
- **CC3.** Every roll shows exact odds in three parts where the grades
  differ.
- **CC4.** Every trap and every lock has at least 3 routes, and at least
  1 of them needs no training and no class (CL 6.1 rule 4).
- **CC5.** No check, lock or trap gates required content (LG2 and LG14
  extended by `LockNeverCritical`, `TrapNeverSeals`).
- **CC6.** DCs and counts never scale with party size (the HZ 6.2.3
  precedent). Party-best checks already favour bigger parties.
- **CC7.** Act-time object checks, and out-of-combat saves forced by
  traps or locks, use the keyed `checks` stream. Attacks, and saves made
  in combat, use the combat stream.
- **CC8.** Out of combat, the best eligible member is chosen
  automatically, including companions, and walks to the device. The card
  shows who acts and lets the player override. In combat, the acting
  unit checks with its own bonus, must be adjacent, and spends its
  action.

**Families.**
- **Object checks:** pick, force, disarm, hand-trigger, Squeeze, Plank.
- **Unit contests:** Steal, Shove, lurker Stealth, and saves, all as
  approved.
- **Thresholds:** Take time, detection, Probe, readings.

*Rejected:* rolled detection (Model A), a room Search (tedium; dead with
Int detection).

### 6.2 The DC ladder (lead's call; change TL-2) *(illustr.)*

| Floor | Easy (−3) | **Standard** | Hard (+4) | Notes |
|---|---|---|---|---|
| F1 | 9 | **12** | — | Standard = RT Plank 12 |
| F2–3 | 10 | **13** | 17 | Standard = EnvDC 13 (HZ 6.3.5) |
| F4 | 11 | **14** | 18 | EnvDC 14 |
| F5 | 12 | **15** | 19 | EnvDC 15 |

- `detectDifficulty`, `disarmDifficulty`, the pick DC and a trap's save
  DC all read this table. The trap's save DC is Standard for its floor.
- **Force DC = lock DC + 2.**
- Tier: Easy = tier 1, Standard = 2, Hard = 3. Trigger Shot AC stays 10
  + 2 × tier **(settled)**.
- **Flat rows keep their approved values:** Plank and Squeeze 12 on
  every floor, heft 8/12, Overturn 15, Pitted repair 13, Wedge 10 +
  Mechanics.
- There is no Hard tier on F1.

Fit with the stat model (trained Int +3 specialist; Open / Jam / Botch):

| Case | Bonus | DC | Odds |
|---|---|---|---|
| F1 Standard, L1 | +5 | 12 | 70 / 20 / 10 |
| F2 Standard, L3 | +5 | 13 | 65 / 20 / 15 |
| F3 Standard, L5 | +7 | 13 | 75 / 20 / 5 |
| F4 Standard, L7 | +7 | 14 | 70 / 20 / 10 |
| F5 Standard, L9 | +9 | 15 | 75 / 20 / 5 |
| F5 Hard, L9 | +9 | 19 | 55 / 20 / 25 |
| F5 Hard, mastery | +13 | 19 | 75 / 20 / 5 |

A side-trained Int +1 member sits at 55–60 / 20 / 20–25 on Standard,
which is why the best-member rule picks the specialist.

### 6.3 Taxonomy (lead's call; change TL-10)

**Three families, by readability:**

| Family | What it is | How it is seen |
|---|---|---|
| **Device** | A body whose `area` is separate from its trigger. Its `mechanismSquare` (cleat, winch, pawl, lever) sits by the body | Body and mechanism are **always visible**, and **every such body over walkable squares is rigged**: there are no decoys (RC1; the FL 7.8 chandelier precedent). Its trigger may be hidden |
| **Snare** | A point trap: the effect lands on the trigger square | Hidden until detected |
| **Catch** | A trap on a lock, lid or Door | Its padlock glyph or lid tell; hidden catches are detected like snares |

**Triggers (`triggerKind`):**
- **plate:** 1 square; any unit entering, or a weighted piece (TL-14).
- **wire:** a 2-square line; a known wire is stepped over at +1 movement
  (TL-12); forced movement across it always springs it.
- **trapdoor:** 1–4 squares. Units on it take its `drop` damage plus
  Prone and are placed on the nearest walkable non-trigger square; **no
  unit ever falls as part of a spring**, so no fall band, Dreamfall or
  `FallFloor` joins the spring (**lead's call, after the check**). Then:
  - on F1, and in any room that is not a V10-proven Void room with its
    theme's Void skin, it **recloses**;
  - from F2, in a V10-proven Void room (`SnagProven`) of a theme with a
    Void skin, its squares become an open **Void** (TL-31). That Void
    counts toward HZ's Void-room quotas (HZ 6.4), and from then on it
    is an ordinary HZ Void: anything later pushed in resolves HZ's Void
    branch (the C 10.2 edge save; a party unit Dreamfalls to the Snag; a
    monster's `voidFall` pays out and counts as a loss under ENM 14.3
    and ENM 16 as written).
  - A Botch that hits the worker (`botchHits: worker`) applies the
    effect where the worker stands, never as a fall.
- **glyph** (rune): any unit entering; detected and disarmed by Arcana
  or Mechanics.
- **catch:** springs on Open, Pick or Force of its host, in the order of
  6.13; key and Wax Key routes never spring it. A catch on a Door is
  legal: its host is not a trigger square (TP1).
- **M3 only (TL-41):** `room` (a loud act in the room) and `scent`
  (hound families).

**Effects (`effect`, each with a `loud` flag):**
- **strike:** damage, Reflex half.
- **drop:** damage plus Prone.
- **snare:** Grappled, escape at the floor's Standard.
- **spray:** a condition from the theme's 6 with a Fortitude save, under
  RT-16.
- **alarm:** noise 10 only.
- **shove:** push 1 along a fixed line, under C 10.x.

*Rejected effects:*
- teleport outside a ruled relocation (the M3 relocation traps and the
  Mirror Glyph wait for the ability model's teleport rule, ABL 8; TL-41),
  item loss, loot destruction, summoning;
- Portals that seal or split the party (RS 7.6). The portcullis stays
  damage-only in M2 (TL-31).

**Tells (lead's call; when they render: lead's call, after the
check).** Every trap carries a diegetic tell: on a wall, above head
height, or on the device body. Never on a fake floor tile (RC1+). This
is the art team's main canvas.
- **A snare's or catch's tell renders only once the trap is
  Revealed.** It appears through a Reveal transition that reads as the
  object settling (dust falls, the object "settles"), never as a spawn.
  Hunch shows only the zone (6.4); Hidden shows nothing. So a player who
  learns the tells still cannot find a Hidden trap without trap sense,
  and the Int lane holds.
- **Device bodies and mechanisms are always shown** (they are the
  device's tell); the device's trigger follows its grade.
- **Reserved tell silhouettes:** no dressing in a theme uses the object
  family of that theme's trap tells (the per-theme lists in 7), as RT
  7.1 did for Kegs. V9/V11 check it as content (8; TL-43).
- **No trap tell moves at idle.** Idle motion on containers and
  Furnishings is reserved for lurkers, boxed lurkers included (RC2).
- **No trap tell reuses an HZ Cord, vent or Snag prop family** (8).

**Set pieces:** at most 1 per F2–5 floor, always known, and the only
home for a trap's hard part (C 11.6). In M2, F2 set pieces ship with
`hardPart: null` until the death report clears (a reading inside C 11.6;
KB11).

### 6.4 Detection (lead's call; changes TL-6, TL-7, TL-9)

**Trap sense** is the highest over the living, non-Downed units in the
party roster, companions included, whichever unit has line of sight
**(eligibility: lead's call, after the check)**. For each trap it is
the maximum of the formulas that apply to it:
- 10 + Int, for anyone;
- 10 + Int + proficiency, for a Mechanics-trained unit (double with
  mastery);
- for traps tagged `rune` (glyphs), also 10 + Int + Arcana proficiency
  (an alternative, not a replacement);
- for `poison` traps, also passive Medicine (10 + Wis + proficiency),
  which gives the Herald and Vanguard a lane.

Grades are **monotonic**: they never drop, so a death or a companion
leaving never hides a trap again.

**Told plainly:** Perception finds lurkers and boxed lurkers, not traps.
**Trap Eye** (SH-19) then needs a new lean; the recommendation is double
proficiency on trap sense, the Shade owner's call.

**When the check runs:** when a party unit first has line of sight
within 6 squares to a trigger square or mechanism; that tick is stored
as `seenByParty`. Every seen trap is re-evaluated on any rise in trap
sense. It draws nothing.
- **Per square (lead's call, after the check).** Detection runs after
  every square of any party movement, before the next square is
  entered. A new Revealed or Hunch result **interrupts** the move,
  manual or auto-walk, and the unit keeps its remaining movement. So no
  unit steps onto a trap it learned of mid-move without a chance to
  re-plan.
- As a second guard, a trap whose grade changed during the current move
  or action counts as undetected for that action (`gradeChangedDuringAction`;
  6.5).

| Grade | Condition | Shown |
|---|---|---|
| **Revealed** | trap sense ≥ `detectDifficulty` | Trigger squares, area, mechanism mark, rule card, and the tell (6.3) |
| **Hunch** | trap sense ≥ `detectDifficulty` − 5 | A true zone (below) that contains every trigger square. Never a decoy (`HunchTrue`). For a catch, a Hunch badge on its host container or Door instead of a zone |
| **Hidden** | below that | Nothing. Floored (DH 10.4) |

**Hunch zone geometry (lead's call, after the check).** `hunchZone` is
the trigger's bounding box padded to at least 3×3 (2 wide in corridors)
*(illustr.)*, shifted by an offset drawn at generation from the
positions that still contain every trigger square, and clipped to
walkable squares. V11 `HunchTrue` rejects a zone whose clipped area is
not larger than the trigger area (the zone would hide nothing), and
`HunchZoneExclusive` (TP1) rejects any other trap's trigger square
inside it.

**Probe** is a new ABL 7.2 row:
- any unit, from any square inside the zone or orthogonally adjacent to
  it, without entering further; no roll, noise 0;
- 2 ticks out of combat; in combat an action from such a square;
- reveals **every trap whose trigger squares lie in the probed zone**
  (`HunchZoneExclusive` makes that the zone's own trap; a Hidden trap
  there would be revealed too);
- offered **only** at a Hunch zone (the Hairline grammar). So Probe
  never makes a trap less safe.

**There is no search-for-traps verb anywhere.** The ABL Search action
stays for hidden units only.

**The Hunch stop (clock lever plus decision).** Auto-walk stops on the
last square before a new Hunch zone, with one click and three choices:
**Probe** (default, offered from that square), **Walk round** (if the
detour is +4 squares or less), or **Push through** (floored out of
combat). **Out of combat it auto-Probes at 0 clicks** unless the patrol
forecast shows a crossing within 2 ticks, in which case the three-way
stop appears; in combat the stop always appears **(lead's call, after
the check:** out of combat Probe dominated, so the stop was a click,
not a decision**)**.

**Other ways a trap is Revealed, all deterministic:**
- it springs in the party's sight;
- Yield interrogation or Pick the Question;
- a reading (6.12).

**Wide berth (trigger: lead's call, after the check).** After each
monster move, if a native step in party line of sight had an
unconstrained shortest path through an armed trigger square and its
actual path avoided it, that trap's grade rises to at least Hunch. A
one-shot cue shows the cause: a brief path-bend trail on that native
(telegraph tier, at most 1.0 s) and one pulse of the new zone linked to
it (8).

**Count line (FL-20):** "traps found: k · open Hunch zones: h". It is
shown from F2. **There is no "/ n".** A total would invite
floor-sweeping for traps that are floored anyway (KF7).

**Fit.**
- A solo Vanguard (Int 0, sense 10) Reveals F1 Easy traps, gets Hunches
  on Standard traps on F1–F5, and misses Hard ones (floored). The clock
  toy for this arm is owed (11.5).
- A trained Int +3 member at L3 (sense 15) Reveals every Standard trap.
- A late Arcanist (sense 19) Reveals everything.
- In the toy, mixed parties Reveal 75–90%. Hidden traps for no-Mechanics
  parties fall to 2–13%, all Hard.
- **Lever:** the Hard share of non-fight traps on F4–5 is 15% or less.

*Rejected:* max(Perception, trap sense) (kills the Int lane); a Qualm
zone for every trap (flattens Int; kept only as the −5 Hunch band); Read
the Maker (overlaps Hunch and wide berth).

### 6.5 Warned or floored for traps (agreed, Q1; change TL-8; narrows DH 10.4, agreed)

A visible Hunch zone is a **warning**, as in HZ's "Warned **or**
floored". Damage from a **Hunch-grade** trap whose shown zone the unit
entered is floored only when:
- (a) combat is not active;
- (b) the unit was moved in by forced movement; or
- (c) the zone first appeared this round **(new lead's call:** not part
  of the client's Q1 answer; listed in 19.1**)**.

A unit that enters a shown zone by its own movement in combat sees a
preview with the floor's worst trap and the lethal tag, and the damage
is then ordinary. Hidden traps stay floored in and out of combat (DH
10.4 kept).

**The trap's own grade decides (lead's call, after the check).** The
floor is decided by each trap's own grade at fire time, never by which
zone a square lies in:
- a **Hidden** trap is always floored, even inside another trap's zone
  (`HunchZoneExclusive` keeps that case out of generation in any
  event);
- a **Hunch** trap follows (a), (b) and (c);
- a **Revealed** trap follows DH 10.4 as written (lethal and tagged in
  combat);
- a trap whose grade changed during the current move or action counts
  as undetected for that action (6.4).

**The snapshot (lead's call, after the check; TL-8).** The boolean
`detectedAtFire` (DH-32) becomes an enum, `detectionAtFire {hidden |
hunchFloored | hunchWarned | revealed}`, set by the test above. DH
10.4's "Trap provenance" floor applies to `hidden` and `hunchFloored`;
condition ticks store the same value in place of
`sourceTrapUndetected`. Without this the agreed DH 10.4 mechanism would
still floor every Hunch trap, and the Q1 answer could not be built.

**Self-caused (lead's call, after the check).** Damage from a chosen
in-combat entry into a shown Hunch zone is self-caused (6.10): counted
in full, outside the EnvRound cap. So a full cap never makes walking
through unprobed zones free, and the preview's "up to X" ignores the
EnvRound remainder.

**"The floor's worst trap" (new lead's call).** The preview shows the
highest maximum, after this unit's weakness (the HZ 6.11.5 method),
among the floor's traps the party has not Revealed, as "up to X". It is
a floor-level figure computed from the floor's own records, so it tells
the player nothing about which trap, if any, sits under that square. It
never exceeds `CapRound` (TL-18). The lethal tag (DH 17.5, 17.6) shows
that the entry is not floored, and turns red, **and changes shape to a
cracked heart**, when "up to X" would take the unit to 0, so the state
survives colour-blind simulation (8; lead's call, after the check).

**Why.** Without this, leaving a trap unprobed would be safer than
revealing it. A fight fought inside unprobed zones would be safe for the
party and lethal for monsters, and a level-up or a companion joining
would make a party more fragile (KB1). Probe never makes a trap less
safe than leaving its zone alone.

**Party-caused springs.** A spring the party causes that starts combat
keeps the settled reading: "a trap that starts combat counts as
undetected", so its damage to party units is floored. Monsters have no
floor (KK11).

*Rejected (Q1):* B, Hunch zones fully floored like hidden traps (it is
safer not to look, and the design would need extra patches); C, no Hunch
zones (20–57% of traps go unseen for parties without Mechanics).

### 6.6 Hand work: the Margin rule, Take time, re-arm (lead's answer, BQ1; changes TL-4, TL-5, TL-11, TL-15)

| Use | Training | Time | Open | Jam (−1 to −4) | Botch (−5 or worse) |
|---|---|---|---|---|---|
| **Disarm** (at a trigger or mechanism) | Mechanics, trained only **(settled)**; Arcana for glyphs | 2 ticks out; action in | Disarmed, silent; `disarmedBy` | Noise 4; no hand work this floor; trigger stays live | Springs: full effect and its save, no hard part, lands by `botchHits` |
| **Pick** (6.11) | Mechanics, trained only | 1 tick out; action in | Opens, silent | Noise 4; picking closed this floor | Jam plus the lock's consequence |
| **Hand-trigger** | Mechanics, untrained allowed (the settled row has no training clause) | Action (1 tick out) | Springs; the unit is causer | Noise 4 and a Jam | Same as Jam |
| **Force** (6.11) **(row: lead's call, after the check)** | Athletics, anyone | 1 tick out; action in | Opens (noise 6, or the kind's noise) | Noise 6 (or the kind's); `forceFails` + 1; DC − 2; forcing stays open | As a Jam; on a Door from F2 also a **Splinter**, which opens the lock. On a container or on F1, an ordinary failure. The third try opens with no roll |

- **One roll per object per hand verb per floor** (pick, disarm,
  hand-trigger). Force retries by its own row; Squeeze and Plank roll
  per traversal (6.22). Universal Jams last to the end of the floor even
  when rolled in combat, so a combat attempt never buys a second try
  afterwards (KB5). Only the Trapwright's own Jury-Rig jam keeps its
  per-fight scope (SH 8.2).
- **Where a Botch lands (`botchHits`; lead's call, after the check):**

| `botchHits` | Resolution |
|---|---|
| `worker` | The effect resolves on the worker as if it stood on the trigger square (`botchOrigin`, recorded). A push follows the trap's fixed line from where the worker stands. No relocation and no fall. The damage is self-caused (6.10). No other unit is hit |
| `area` | Resolves exactly like a normal spring. The worker's own damage is self-caused; every other party unit in the area (followers included) takes involuntary damage, inside EnvRound |

- **Take time** (TL-5):
  - out of combat only; no roll; the result is 10 + bonus;
  - costs **8 ticks** *(illustr.)*;
  - offered only when it succeeds and only on **Easy or Standard**
    devices and Plain locks;
  - **never** on Hard, never on a consequence lock (a spent Crier
    included), never on an object tagged `noTakeTime`, never on the
    Squeeze or Plank (those keep their approved rolls); inside a Wager
    room **each use fills one pip**, whatever its tick cost (6.15);
  - **a channelled act (lead's call, after the check):** each tick runs
    the patrol and hearing step; the result applies after the final
    tick. If combat starts or the worker is interrupted, the act ends
    with no result, and since Take time draws nothing, nothing is spent;
  - the card shows the patrol forecast ("a patrol crosses the hearing
    outline in 4 ticks").
- **Sure Hands** (SH 7.11) keeps its d20 floor on rolls and also cuts
  Take time to 3 ticks. The Shade owner is told that Take time otherwise
  removes most of its value (toy: about +0).
- **Easy Plain locks** are picked automatically on walk-in by a party
  with a trained member (Take time, its full ticks, no card, 0 extra
  clicks), **only when the patrol forecast shows no crossing within
  those ticks**. Otherwise the normal lock card appears with Take time
  preselected (1 click) **(lead's call, after the check)**.
- **Re-arm** (the settled row, now numbered):
  - trained Mechanics, adjacent; 2 ticks out of combat, an action in
    combat;
  - the trap fires at **500‰ with no hard part, never stacking** (SH-15
    brought forward to M2 for everyone, so the universal row never beats
    the Trapwright's);
  - **once per trap per floor** (`rearmUsedThisFloor`);
  - not while any group the trap has hit (`hitGroupIds`) is Alerted or
    in combat.
  - Monsters never re-arm (Later). Traps never reset themselves;
    anything with a rhythm is a Stir (HZ).
  - **New lead's call:** this replaces SH 8.2's "the universal action
    route stays unlimited at its action cost" (TL-15). Jury-Rig's own
    re-arm keeps its separate once-per-trap-per-floor counter
    (`juryRigRearmedThisFloor`), so a Trapwright can re-arm one trap at
    most twice per floor, once by each route.
- **One spring per trap per round** (NM-1; `lastSpringRound`). Each
  spring increments `springIndex`, which keys its saves (6.22).

*Rejected:* any failure springs (springs 28–36% against 8–16%); the
Touchy second roll (more springs and noise for +9 points); a half-damage
Botch (a hidden second rule).

### 6.7 Deliberate triggering and the other answers (lead's answer, BQ3; changes TL-12, TL-13, TL-14)

1. **Walk round** is the default. Known triggers are hazards for
   pathing, auto-walk and followers (C 11.3). TP3 guarantees a detour of
   +4 squares or less.
2. **Step over** a known wire: moving through a known wire square
   defaults to stepping over it at +1 movement, and the path planner
   includes the cost. If the movement left cannot pay the +1, the move
   stops before the wire; it never springs the wire implicitly
   **(lead's call, after the check)**.
3. **Bear it:**
   - entering a known trigger by your own movement springs the trap with
     **advantage on its save**; on a wire, Bear it happens only as an
     explicit "spring it" choice in the move preview;
   - every damaging trap has a save;
   - the first entry per trap asks for confirmation, and moving an ally
     onto one asks too (DH 17.7);
   - the preview shows "floored" out of combat and the lethal tag in
     combat, plus "HP after: X, until next rest";
   - if a failed save would leave the unit below 50% HP, the preview
     shows a warning; it is never a block (RC1).
4. **Trigger Shot** (universal from M2): replaces one attack. A thrown
   or launcher attack against AC 10 + 2 × tier at a trigger square **or
   a mechanism**. A hit springs; a miss is noise 4.
   - **Improvised throw (lead's call, after the check; TL-42):** for
     Trigger Shot alone, any unit may throw a held weapon or item as an
     improvised throw: range 4, no damage, the item lands on the target
     square. This is a narrow exception to weapons 9.3 ("no improvised
     throw"), asked of the weapons owner. If refused, Trigger Shot needs
     a thrown weapon or a launcher, and the solo guarantee rests on
     walk-round and Pole it (6.17).
   - **Out of combat (lead's call, after the check):** Trigger Shot is
     an object check on the `checks` stream, one try per trap per floor;
     a miss is noise 4 and closes that route for the trap this floor. In
     combat it is an attack on the combat stream as before.
5. **Hand-trigger:** stays an **Action** (the ABL 7.2 settled row is
   kept; KK4). **Handles:** a mechanism tagged `handle` (a cleat, a
   protruding book, a winch pawl) springs with an action and no check.
6. **Pole it:** a unit with a reach weapon springs a known plate, wire
   or catch host within 2 squares **(catches added: lead's call, after
   the check)**. An action in combat, 1 tick out of combat, no roll, the
   trap's noise. On a catch, the effect lands on the host's square with
   no hand on it, so nobody is hit unless a unit stands there. The
   Vanguard's ten-foot pole.
7. **Weighted plates (lead's call, after the check, on the ordering):**
   - a Furnishing whose Shunt, Kick or Overturn **ends** on a plate or
     trapdoor springs it, with the mover as causer; squares the piece
     only passes over spring nothing;
   - wires and glyphs ignore pieces; Kegs stay sturdy;
   - a known marker is never hidden;
   - so **one act springs at most one trap**. The trap's damage stays
     outside the RT 6.4 one-instance cap (TL-18 kept), and the piece's
     own collision instance is unaffected. When an act both collides and
     springs, both go through EnvRound in a fixed order: collision
     first, then the spring.
8. **Shove** a monster onto a known trigger (C 10.2 edge save).
9. **Probe**, the **Wax Key** (on a lock with a catch, the key opens the
   lock without springing the catch; 6.13), a companion acting as best
   member, or **leave it**: every trap is optional off the main path.

### 6.8 Enemies and traps (lead's answer, BQ4; change TL-16 in M2, TL-17 in M3; enemies owner)

**M2 knowledge (K1/K2).**
- **Natives** (every generated group and their joiners) know the floor's
  trap states (true state, ENM 11.2):
  - trigger squares of armed traps are hazards for them, with lethal
    ones barred (invariant 7) and non-lethal ones costed (C 17.3);
  - they step over known wires;
  - **they do not avoid device areas**: entering an area springs nothing
    ("the house trusts its own traps").
- **Mindless types** treat non-lethal traps as floor and spring them.
  Invariant 7 still applies on true state.
- **Once Bitten:** a group with any member inside the area of a device
  spring that resolves, whatever the save and the damage, treats every
  known device area on the floor as a hazard for the rest of the floor.
  This is one bit per group (`bitten`). Bitten units standing in an area
  leave it on their first move, and every unit that later joins that
  group inherits the bit **(precise trigger: lead's call, after the
  check)**. Rule card: "The house trusts its traps until one bites."
- **Opener rule (order: lead's call, after the check):**
  1. the spring resolves fully as an out-of-combat event, its saves on
     the `checks` stream;
  2. if it affected (damaged or conditioned) any member of an unaware
     group, combat starts with that group, and the spring *was* the
     springing unit's surprise action. Other party units take surprise
     actions only if their own ABL conditions hold. It never adds a
     round, and it never stacks with Ambush;
  3. if no unit was affected (a snare or alarm, or the group outside the
     area), the result is only the spring's noise.
- **AI fairness:**
  - monster pushes and Shunts score only traps the party has Revealed;
  - `NoCheapKill` (**agreed**, RT Q3) extends to trap springs a monster
    causes;
  - a monster with no path while under attack breaks line of sight and
    reroutes, or returns home, rather than standing still (an ENM 11.3
    behaviour request).
- **Fallback if the enemies owner refuses the Revealed-only scoring:**
  rigged-fight density drops until rigged share × party-hit probability
  ≤ 0.03 (NF-e).

**M3 (with the Trapwright, TL-17).** Per-group belief (`armed`/`spent`),
and witness-only secrecy: a trap the party re-arms out of a group's
sight is unknown to that group. This is the A-5 narrowing the Hidden
rule set as precedent. It makes Killing Ground and Drag Line work.
Monster trap use, chained springs and summons' knowledge stay **Later**.

**Lure play:**
- spring a device whose area holds natives (the first spring on a
  group);
- push or Drag onto a Revealed trigger;
- draw a mindless type across a plate;
- ring an alarm tripwire to pull a group to prepared ground (6.18
  limits).

*Rejected:* monsters setting off hidden traps at random; monsters
ignoring traps; natives avoiding device areas (the simulator arm keeps
it as a control, 11.5).

### 6.9 Traps in combat (lead's call)

- **Rigged fight rooms (F2+):**
  - a fight host carries 0–2 traps, at most 1 of them a Device;
  - device bodies are visible from the doorway;
  - segment 6 may start groups in device areas (at most half a group's
    members), never on trigger squares.
- **Device value (NF-f):** fight-room devices use a fixed band of about
  0.9 × `CapRound`, max ≤ `CapRound` after weakness (6.10), with Reflex
  half. Riders (Prone,
  Soaked, shove) add value. At these bands a full-action spring on about
  1.8 natives nets +0.03 to +0.40 hero-actions in the toy, and Trigger
  Shot (one attack) nets +0.5 to +0.9. Risk: if the simulator shows the
  hand-trigger is never used, it may replace one attack, sent to the
  Shade owner first (19.2).
- **A Revealed trap in combat is lethal for everyone.** Party units sit
  inside the EnvRound cap; monsters do not.
- **Hard parts:** F3+, plus F2 set pieces after M2. At most once per
  fight. Never on a Botch, a re-arm or a reaction.
- **Budget:** each rigged fight's expected party-side trap damage is
  charged to segment 6's encounter budget, so DH 18.2 calibration holds
  (NF-d).
- **Rounds:** a rigged fight adds at most +0.1 rounds
  (`RoundsPerFightDelta`).

### 6.10 Damage, caps and fairness (lead's call; change TL-18)

Values are *(illustr.)*. Lowest legal hero HP at floor entry is 16 / 26
/ 36 / 46 / 56.

| Floor | `CapRound` (30%) | `HiddenTrapBound` (20%): snares and catches, max | Device and set piece: fixed band ≤ Cap |
|---|---|---|---|
| F1 | 4 | 3 (1d3) | no fight devices; one off-path device ≤ 4 |
| F2 | 7 | 5 (1d4+1) | 7 |
| F3 | 10 | 7 (2d3+1) | 9 |
| F4 | 13 | 9 (2d4+1) | 12 |
| F5 | 16 | 11 (2d4+3) | 14 |

- **`TrapLethalityBound` (V11):** every spring's maximum after weakness
  (through its own theme's conditions, the HZ 6.11.5 method) is at most
  `CapRound`, set pieces included. The catalogue's band P and its 40%
  bound are deleted. With "every damaging trap has a save", this honours
  C 10.4.
- **Printed maximums are after weakness (lead's call, after the
  check).** Every band maximum in the table above is the maximum after
  weakness. A band whose damage type is one a condition in its own
  theme's 6 makes targets weak to (Soaked: cold and lightning; Oiled:
  fire; and any other weakness step in the theme's 6) uses a
  pre-weakness maximum ≤ floor(`CapRound` / 1.5), or is made untyped.
  This re-derives the Responsory (F3 cold, max 6; 7.5) and binds every
  Choir cold or lightning device and every Carnival fire device. V11
  reports the after-weakness figure for each record.
- **EnvRound (changes HZ 6.11 item 2, HZ-7):** involuntary trap damage
  to a party unit joins the cap's included list. **Self-caused damage**
  (Bear it, a party-caused spring on oneself, a Botch the unit itself
  rolled, and a chosen in-combat entry into a shown Hunch zone, 6.5) is
  counted in full outside the cap and does not fill it. Without that
  rule, a full cap would make walking onto traps free (KB2).
- **Floors:** Hidden traps, Hunch traps out of combat, by forced
  movement or in their zone's first round, a trap whose grade changed
  during the current action, and party-caused springs that start combat
  are all floored (DH 10.4; TL-8). The floor follows each trap's own
  grade (6.5).
- **Trapdoors:** a spring never includes a fall (6.3), so no fall band
  joins `TrapLethalityBound` or the EnvRound order.
- Teetering is never applied directly by a trap (RT-14 / HZ-6 kept).

### 6.11 Locks: pick, force, keys, consequence locks, master key (lead's answers, BQ5, BQ6, BQ8; changes TL-21 to TL-26)

**Where locks appear:** Locked Portals (FL), padlocked containers
(segment 7 sockets tagged `lockable`), locked sphincter caches (Gut),
and locked Hairline caches (M3). **Optional content only**
(`LockNeverCritical`, extending LG14 to containers; HE-3/4/7 sockets are
always Plain).

**Routes for every lock:**

| Route | Who | Cost | Result |
|---|---|---|---|
| **Pick** | Mechanics, trained only | 1 tick out, action in, quiet | Margin rule; one roll per lock per floor |
| **Take time (pick)** | trained | 8 ticks | Easy and Standard Plain locks only |
| **Force** | Athletics, anyone | 1 tick (action in), **noise 6 per try** | DC = lock DC + 2. **Retryable:** each failure lowers the DC by 2, and **the third try always opens**, with no roll (6.6 Force row) |
| **Floor key** | holder | 0 ticks, silent | Used automatically on walk-in |
| **Wax Key** | anyone | 1 tick, silent | Opens it; no consequence; clears a Jam |
| **Unbolting** (M3) | caster or off-class reader | action, range 6, noise 10 | Opens it; no consequence; usable in combat |

- **Force resolution (lead's call, after the check):** every force
  failure, Jam or Botch, costs the try's noise (6, or the kind's),
  increments `forceFails` and lowers the DC by 2; a force failure never
  closes forcing. A force Botch on a container, or on any lock on F1, is
  an ordinary failure (contents are never harmed). The third try opens
  with no roll, and per-try consequences still fire on it (Crier noise,
  Tattle wake, Runed sting). `forceOpenedBy` records how it opened.
- **Splinter:** from F2, a force Botch on a Door splinters its panel
  **and opens the lock**. Noise 10 (the settled "shattered door"). The
  Door can never close again and loses its −3 hearing. The Splinter is
  an act-only VFX (splinters burst within 1.0 s and vanish); the
  permanent state is an empty frame with a jagged edge on the frame
  itself, with no floor debris and no loose panel (RC1 residue rule; the
  Gaol's Turnkey panel prop stays unique) **(lead's call, after the
  check)**. The **Portal stands and connects the same
  rooms** (a reading of RT 6.3.4 and RS 7.6, told plainly). **On F1 a
  force Botch is plain noise 6** (toy: weak solo forcers would otherwise
  Splinter 34–53% of the time). Option B if the reading is refused:
  noise 10, and the Door still closes. `HearingGap` and `ChainPullRate`
  are reported after Splinters.
- A picked or forced Locked Portal becomes an ordinary unlocked Door, so
  monsters path through it from then on.
- **No party relock verb** (ABL 15.1). Wedge stays the Trapwright's
  (SH-18), and Drop-bar stays with the client.
- **Force-point validator (TP10):** a Locked Portal's force point sits
  at path distance ≥ 7 from every Dormant group home and every Circuit
  room, except for intended Tattle locks. This keeps lock-driven joins
  under `ChainPullRate` (toy: no-Mechanics parties otherwise pay 0.8–1.2
  joins per floor). It is checked in the `trapsResolve` pass, after
  segment 6 places groups (6.22); a failing lock is downgraded to Plain
  with its consequence dropped, or segment 6 is asked to re-place.

**Consequence lock kinds (TL-22):**

| Kind | Glyph on the padlock | From | Pick Botch | Force | Wave |
|---|---|---|---|---|---|
| **Plain** | none | F1 | Jam only | normal | M1 |
| **Crier** | sound-wave arcs | F2 (the Gaol may place 1 on F1, after room 2) | Noise 10 at the lock (RC7 alarm cue); the hearing outline previews who comes; joiner caps hold | every try is noise 10 | M2 |
| **Barb** | a barb | F2 | Picker takes the harm band (2/3/3) plus the theme's poison condition (RT-16, else damage only), Fortitude half; floored out of combat | never fires | M2 |
| **Tattle** | a door with an exclamation bar ("the room beyond hears") | F3 (the Wake's Viewing Door on F1, after room 2; 7.4) | The pre-placed Dormant group in the room beyond becomes Alerted. Listen reports it first. Never spawns. **Portal hosts only** (V11). Bound in the `trapsResolve` pass: a candidate with no group beyond resolves to a Crier with no extra draw | any try wakes it | M2 |
| **Runed** | one angular sigil in a square frame (no ring) | F4 | Pick with Mechanics or Arcana; Botch is a harm-band sting plus Jam; Unweaving turns it Plain | each try stings | M3 |
| **Hour** | an hourglass (no clock hands or digits) | Gaol, Wake | Plain behaviour; also opens itself on the floor's Landmark tick (HZ-19) | normal | M3 |

**Glyphs redrawn (lead's call, after the check):** no lock glyph uses a
face part (RC3: faces mean minds, and the revealed boxed lurker shows a
face), a ring (the RC4 spirit shape) or a clock face (HF11: one clock
per room). The full glyph set is in 8.

- Take time is never offered on Crier (spent or not), Barb or Tattle
  locks. That is where BQ5 lives: roll it, force it, Wax-Key it, or
  leave it.
- **Set off a Crier:** any unit may deliberately sound one (1 tick,
  noise 10). After that the lock behaves as Plain for picking and
  forcing (`crierSpent`), but Take time stays barred. Groups drawn by a
  Crier, sounded, picked or forced, arrive **Alerted and cannot be
  Surprised** that fight, as for alarm traps (6.18; TL-30) **(lead's
  call, after the check:** otherwise a Crier is a once-per-lock lure
  better than any alarm**)**.
- A consequence lock's container draws +1 rarity step inside the budget
  (a segment 7 hook). The lock card hints at its contents ("holds a
  notable").

**Floor keys and Keybearers (TL-23):**
- Keys stay `party(keys)`, size 0, floor-scoped (ITEMS 5.3 kept).
- One **Keybearer** per floor at most, with a key pip on its plate. This
  generalises the Gaol Turnkey (RS 8.3) and the Gut's "house keys on
  strings".
- The key is gained by kill, Steal (one try per carrier, **agreed**),
  Yield, Cow, or the Gut's key tripwire. Handing over the key on a Yield
  and on a Cow is a new outcome for ENM 14.6 (the Yield drop) and ENM
  14.8 (a Cowed group "keeps its notable item"), asked of the enemies
  owner in TL-23; if refused, Cow does not yield the key, and the key
  comes by kill, Steal, Yield (if that part is accepted) or the tripwire
  **(lead's call, after the check)**.
- On pickup the matching lock is highlighted, and the key is used
  automatically on walk-in.
- A new interrogation category, "the floor key: its carrier and its
  lock", is added to ENM 14.6 (an agreed list).
- `KeyReachable`: a key is never behind its own lock; the carrier sits
  within 2 rooms of the lock (Gaol: within 1, FL 7.2); key chains are
  capped at 2 (M3). The `traps` pass draws the Keybearer slot; the
  `trapsResolve` pass binds it to a placed unit and checks the distance.
  If no placed unit fits, the key goes to a container within 2 rooms
  (`carrier: containerId`), else the lock is downgraded to Plain (6.22).
- **Lockside Listen:** a pick, or opening any door lock, also Listens
  through that door (an FL-18 addition; segment 10 owns the display).

**The Wax Key (TL-24):**
- a run-scoped party counter `waxKeys`, cap 2, size 0, not `category:
  key`, so it survives the stairs;
- out of combat only;
- price by camp 24 / 28 / 32 / 36 *(illustr.)*, checked per camp after
  the Leadership discount against peg K1 (≥ 2 × potion) and peg K2 (≤
  the mean value of a locked cache on the next floor);
- at cap, a dropped key stays in its container;
- it is refused, before any spend, on anything without a lock
  (`WaxKeyRefused`), judged on the presented state, boxed lurkers
  included (6.14);
- never opens the Bossgate, Grilles, a Latch from its blind side,
  secrets, Riddle locks or Wager cages; never locks anything;
- channels: sundries from F2 (at most 1 per floor), one camp notables
  slot, and the basic vendor under Q3 (below).

**The Wax Key at the basic vendor (agreed, Q3; change TL-25).** The
basic vendor stocks **1 Wax Key only when the party has no
Mechanics-trained member**, merged with the companions gap card (CMP 7.3
G2). The merge rules are **new lead's calls**:
- **One test, one snapshot.** The stock reads the gap card's own
  "Mechanics met" test (any party unit, the hero included, is trained in
  Mechanics), on the same camp-entry snapshot (after catch-up and the
  top-up, CMP 7.11). It draws nothing.
- **One display.** When the test fails, the gap card's coverage strip
  prints "Mechanics: Wax Key stocked at the vendor" beside the Mechanics
  need, whether or not card A offers a Mechanics kit.
- **Stock.** Limited stock of 1 (`vendor.waxKeyStock`), like potions
  (ITEMS 10.3); priced at that camp's depth price; greyed `OutOfStock`
  when sold, and `WaxKeysFull` at the cap of 2. Unbought stock is
  destroyed on leaving camp (ITEMS 10.3).
- **Stable for the visit.** Hiring a Mechanics companion at that Muster
  does not withdraw the stocked key; the next camp reads the new party.
- The camp notables slot (one Wax Key at most) is separate and
  unchanged.

*Rejected (Q3):* B, Wax Keys from loot and the camp notables slot only.

**No lockpicks (lead's answer, BQ6, contested; TL-26).** Oil the Works
is an M3 option: spend an existing oil flask on one rolled attempt for
advantage, and a failed roll makes no noise. The client confirmed BQ6
on 2026-11-21; the fallback, if the client ever reopens it: a size-0
"Picks" counter, spent only on a Botch (19.2).

### 6.12 Doors, stuck and barred doors, secrets, readings (lead's call; changes TL-33, TL-34)

- **FL-8 door verbs, now owned here:**
  - open: M1, by moving in;
  - close: M2, 1 tick out of combat; in combat an action, once per door
    per round, never with a hostile adjacent;
  - close is refused on a Splintered Door;
  - monsters never close or lock.
- **Stuck doors are rejected as a generic type:** a tax with no
  decision. The **Swollen door** (M3, Choir and Wake, optional content
  only) is a no-roll threshold: if passive Athletics ≥ Easy it opens
  quietly; otherwise noise 6 and +2 ticks.
- **Barred doors** are FL's Latches.
- **Drop-bar** is with the client (FL). If adopted, the recommendation
  is M3 with the anti-rest guard: monsters force it at 10 + the
  dropper's Athletics (loud 6), a patrol whose Circuit hits it forces
  it, and it never satisfies a short rest's "no aware hostile" by
  itself.
- **Hairline:** the agreed no-roll rules are kept. M3 variants:
  - a **locked Hairline** cache (Plain or Crier only);
  - a **trapped Hairline** cache (a catch);
  - the Lore-inscription reveal in Silk and Choir, which widens the
    agreed FL Q3 rule and goes to the client at M3 (19.2).
- **Readings** (one-off room checks): thresholds with no roll. Arcana
  reads runes and shrines, Lore reads inscriptions, Mechanics reads
  mechanisms. Each yields a floor fact (a trap Revealed, the Keybearer,
  a lock kind). Segment 8 owns the content (M3).
- **Squeeze and Plank:** approved failure costs kept; the rolls move to
  the `checks` stream. As approved (FL 9.2, RT 6.2.6), each unit checks
  as it enters and may try again in later rounds, and a backtrack
  re-rolls: each check uses the object's `traversalIndex`, incremented
  on every check in and out of combat, so they are exempt from "one
  roll per hand verb" while the key still has no unit (6.22) **(lead's
  call, after the check)**.

### 6.13 Chests and containers (lead's call; change TL-27; segment 7 hook)

| Kind | Rule |
|---|---|
| **Plain** | No check. **Every HE-3/4/7 container is Plain** (`HeNoLock`) |
| **Padlocked** | A lock kind from 6.11. Gaol caches sit behind Grille-fronted cells (FL 7.2 lesson room 3) |
| **Catch** | A trap on the lid: detected like a snare (a Hunch shows as a badge on the host); disarm, Bear it, Pole it (springs it with no hand on the lid) |
| **Keyed** | A floor key, or the general routes |
| **Boxed lurker** | M3 (6.14) |

- Contents are never destroyed by any result. A Jammed container can
  always be forced or Wax-Keyed.
- Locked containers show a padlock base decal on the hasp, with the
  kind glyph (8), and never a keyhole. Unlocked containers show no
  padlock.
- **Open routing (moved here from 6.14; lead's call, after the
  check):** opening any container, real or not, routes to the best
  Mechanics member, otherwise to the highest-HP member. The same rule
  for every container means routing never leaks a boxed lurker.
- **Host-catch resolution order (lead's call, after the check):**
  1. **Key and Wax Key routes never spring a catch.** This is a general
     rule; the Confession Box's "honest key" (7.2) is an instance of it,
     not an exception.
  2. **Pick, Force or Open** on a host with an armed catch: the lock
     check resolves first. On Open, the catch springs unless it is
     Revealed (a Revealed catch must be dealt with first, or the card
     says it will spring). On a Botch, the lock's consequence fires
     first, then the catch springs. A Jam springs nothing.
  3. The lock card shows "the catch will spring" whenever it would.
     `LockRecord.catchTrapId` links the two.
- Opening gives one loot summary (ITEMS 8.1).

### 6.14 Mimics: the boxed lurker (lead's answer, BQ7; agreed, Q2; change TL-28; M3)

- **What it is:** a `containerForm` placement of a theme's **existing
  lurker**. It is ENM 17.4's Ambush bait container with the bait *being*
  the lurker.
  - **Silk:** the Margin-Crawler in a catalogue drawer; rest tell "a
    page that turns by itself" (RS 8.7).
  - **Gut:** the Gutworm in a swallowed strongbox; rest tell "a pulsing
    bulge" (RS 8.10).
  - Both rest tells are re-authored at container scale and readable from
    6 squares (an art ask). The Gutworm's strongbox is organ-swollen and
    has no padlock hasp, so it never shares a form with the Tenant's
    Keyed strongbox (7.9); the Silk's drawer never shares a host with a
    catch (the Paper-Cut Tray moved to a catalogue tray lid, 7.6).
  - It adds no roster type, no mindless count (RE9) and no keyhole (RC3,
    V7). Its revealed form shows a face (RC3, skittish).
- **Presentation (lead's call, after the check):** a boxed-lurker socket
  takes the lock state its host socket would have had, drawn at
  generation: unlocked, or a Plain padlock (`lockId?`). The padlock is
  real, part of the creature's shell, and pickable. The Gutworm's
  strongbox is always unlocked. Pick and Wax Key apply only to a
  padlocked boxed lurker. On an unlocked one, the Wax Key is refused
  before any spend exactly as on a real unlocked container, with no
  reveal.
  - **More themes as they gain lurkers (agreed, Q2):** a theme gains a
    boxed lurker only when its roster gains a lurker type, through that
    theme's own roster process; no roster exception is ever made for it.
- **Concealment:** it makes no placement Stealth roll. It is **marked**
  when party passive Perception ≥ 10 + its proficiency (12–14)
  *(illustr.)* (a change to the ENM lurker row for container
  placements), checked at the same moment as trap detection: when a
  party unit first has line of sight within 6 squares, and again on any
  rise (`markedByParty` on the container) **(timing: lead's call, after
  the check)**. Lore lists the lurker's type on the floor roster, as for
  every roster; it does not say whether one is boxed (0.1).
- **Touch table:**

| Touch | Result |
|---|---|
| Party had marked it, and attacks | The boxed lurker is **Surprised** |
| Unmarked, attacked **(row: lead's call, after the check)** | Revealed; the attack resolves normally; no Ambush Strike; nobody Surprised; 1 tick, noise 6, as on a real container. Attack-to-test has one price, real or not |
| Mechanics-trained Pick (padlocked only) or Open | "Feels teeth": revealed, the hand is Grappled by the lid, **no damage, no Ambush Strike**, nobody Surprised |
| Wax Key (padlocked only) | "The wax will not take a shape." Revealed, the key is not spent, combat starts, nobody Surprised |
| Untrained Open, Force or floor key | Ambush Strike (Poisoned opener, settled) plus Grappled. If unmarked: **floored at 1 HP and capped at 20% of the lowest legal HP** (a DH 10.4 extension) |
| Feed (M3) | A marked boxed lurker given 1 supply hands over its socket's contents and goes Dormant. No XP (ENM 16) |
| Attack-to-test on a real container | 1 tick, noise 6, nothing happens; no roll is made |

- **"Feels teeth" is a UI callout** over the opener (global skin, RC7)
  plus the boxed lurker's reveal cue, **not a bark** (RC8 keeps barks off
  act resolution) **(lead's call, after the check)**.
- **Open routing** follows 6.13, for every container.
- **Adjacency never wakes it;** any touch does (open, pick, force, key
  or attack).
- **Placement:**
  - F2–5, at most 1 per floor;
  - never F1, never in main-path rooms 0–2, the Sanctum, HE sockets, a
    Wager reward or a key's target;
  - budgeted by segment 6 as a lurker encounter; the socket is drawn in
    the `traps` pass and confirmed against the budget in the
    `trapsResolve` pass (dropped, as a real container, if it does not
    fit; 6.22);
  - drops its socket's reserved contents plus its carried item.
- The "keyhole = mouth" line is not used.

*Rejected (Q2):* B, a cross-theme mindless mimic wearing the keyhole (it
needs an exception to RE9, V1 and the roster records, and would lie
under RC3 by giving a minded, hungry thing the mindless mark).

### 6.15 Trap rooms: Wager rooms (lead's answer, BQ9; change TL-29; M3)

- **Site:** a side-room Site on F2–5 that **shares the Dare slot** (HZ
  6.10: "≤ 1 Dare" becomes "≤ 1 Dare-or-Wager"). It may take FL's F2–3
  Strongroom Site (FL owner's call). Never on F1 at any tier. Never
  critical, at most 9×9. Path distance ≥ 9 from other Dormant homes
  (checked in the `trapsResolve` pass; 6.22).
- **Signposted:** a Portal glyph, Listen hears "mechanisms", a Stratum
  Card line, and the prize visible through a Grille.
- **On entry:** "traps: n" is shown, and every trap inside is **raised
  to at least Hunch**, whatever the party's trap sense (a grade override,
  `WagerNoHidden`; lead's call, after the check). There are no Hidden
  traps inside.
- **Unguarded form:**
  - 3–5 traps of mixed families, laid as a route that wants several
    answers (disarm, Bear it, step over, Trigger Shot, Pole it, a
    weighted piece);
  - **TP3 exception (lead's call, after the check):** inside a Wager
    room the exits are never sealed, but triggers **may close every
    line to the plinth** (`WagerWalkRoundRate` must report 0). The
    visible non-skill line (KF5) is proven by V11 `WagerNonSkillLine`:
    at least one route to the plinth that a no-Mechanics party clears
    for at most 1 pip, using only step-over wires, Pole it or weighted
    plates;
  - **the Disturbance track (one rule; lead's call, after the check):**
    3 pips, one pip per resolved event, never more than one per act:
    - a spring, a Jam or a Botch (a Botch is one pip, not three);
    - any act of noise 4 or more inside the room (a burn, a force try);
    - each use of Take time, whatever its tick cost;
    - other careful work (a rolled disarm, a Probe, a re-arm) adds its
      ticks to `tickAccumulator`, one pip per 8 ticks;
    - lifting the prize (`prizeLifted`);
  - Second Nature's Take time is not offered inside a Wager room (9.2);
  - **the grade is snapshotted when the prize is lifted, before the
    lift's own pip** (`gradeAtLift`). The lift pip then counts toward
    the wake and arms the greed plates;
  - **greed plates:** 1–2 plates, announced at the lift
    (`greedPlates`), arm at the next pip event after the lift, not on a
    tick countdown (or at once if the lift's pip woke the guard). The
    Disturbance track is the room's one clock (HF11), so no other
    countdown runs in a Wager room;
  - at 3 pips, the room's guard (a **full-strength group from the
    floor's own segment-6 budget**, placed Dormant in the room's Alcove;
    waking it moves it, it does not add one) wakes;
  - the guard takes the room's joiner slot, so F3–5 allows at most 1
    more joiner.
- **Guarded form:** a garrison from the floor's budget stands among 3–4
  revealed devices (natives in the areas). This is the true whole-party
  wager.
- **Persistence:** pips and revealed state persist for the floor, and
  there is no second attempt once the guard has woken.
- **Reward (segment 7), read from `gradeAtLift`:**
  - 0 pips: choose 1 of 3 caged prizes;
  - 1–2 pips: choose 1 of 2;
  - woken before the lift: the plinth prize only.
  - The cage set is pre-rolled at generation: **3 distinct notables** at
    +1 rarity step, one of them the plinth prize; the grade decides how
    many are offered, and only one is ever taken **(lead's call, after
    the check:** the proposal's "previews of one pre-rolled notable"
    emptied the choice**)**.
- **Exits are never sealed.** No short rest inside.
- **Reports:** `WagerTakenRate`, `WagerWakeRate`, `WagerDeathShare`
  (against DH 18.2), `WagerXpDelta` (must be 0), `WagerWalkRoundRate`
  (must be 0), and the taken arm of `FloorTimeByTheme` ≤ 90 s.

### 6.16 The Trapwright (told plainly; change TL-37)

See 0.1. The record superset (11.1) ships in M2.
`TrapwrightHasMechanism`, `SpringContentBound` and `DeadRoomRate` are M2
reports and become M3 gates. Model C's fight-adjacent devices meet "≥ 2
main-path traps, ≥ 1 within 6 of a fight room" by construction, and
TP12 gates those minimums for every party; that overlap does not
satisfy the SH-16 density check, which the fallback keeps for M3 (0.1,
told plainly after the check).

The Trapwright's identity at M3:
- Jury-Rig as a bonus action at range 2, then 6;
- Killing Ground reactions;
- Drag Line, Caught and Wedge;
- jam-free springs;
- the per-fight Jury-Rig jam scope;
- Trap Eye re-scoped;
- the HZ 6.15 re-time hook.

Before M3, level-3 parity is rerun with the universal rows present (SH
14 modelled 109.9 / 107.3%).

### 6.17 Class spread, solo play and companions (lead's call)

| Who | Traps | Locks |
|---|---|---|
| **Shade** | Quiet hands; Sure Hands cuts Take time to 3 ticks; Stealth to a mechanism before a fight (the opener rule); Lockside Listen; Steal the keyring; Trapwright M3 | Pick; consequence locks by roll |
| **Arcanist** | The eyes (best Int trap sense); Mechanics option; rune traps by Arcana; Gust onto triggers | Pick if trained; Runed locks (M3); Unbolting |
| **Vanguard** | Force; Pole it; Bear it with high HP; Shove onto triggers; Kick or Shunt pieces; Trigger Shot with thrown weapons; Medicine smells poison | Force, the loud lock class; the High-Striker |
| **Herald** | Perception marks boxed lurkers; Medicine smells poison and treats Barb poison; Pick the Question; Yield and Cow for keys | Unbolting; Leadership discount on the Wax Key |
| **Anyone** | Walk round, Probe, Trigger Shot, Bear it, wide-berth reads | Wax Key, floor keys, force |
| **Companions** | Scout and Tinker are the Mechanics coverage (gap card). Out of combat they act automatically as best member; in combat, on command (CMP 6.16) | Same; the vendor's Wax Key fills the gap when no member is trained (Q3) |

**Told plainly:** in the toy, companions do as much Mechanics work as
the Shade and Arcanist combined. The Shade's trap identity is access and
timing, not raw odds.

**Solo guarantee:**
- every lock is optional and forceable (the third try opens), or opens
  with a key or the Wax Key;
- every trap has a walk-round, Pole it, Trigger Shot or Bear it.

**Loot target (one target, KB16):** notables behind locks or traps ≤ 20%
per floor, ≥ 80% of those reachable without Mechanics, and a value loss
≤ 10% for no-Mechanics parties.

### 6.18 Noise, hearing and patrols (lead's call; change TL-30)

| Act | Noise |
|---|---|
| Open pick or disarm, Take time (pick or disarm), key, Wax Key, Probe | 0 |
| Jam; hand-trigger failure; Trigger Shot miss; quiet spring | 4 |
| Force try; attack-to-test | 6 |
| Loud spring; alarm effect; Crier; Splinter (F2+); Unbolting | 10 |

- All radii follow ENM 12.3, with −3 per closed door. The hearing
  outline previews every noisy option ("this Crier would wake 2
  groups").
- **Alarm traps** are `oneShot`, or re-arm at most once per floor.
  Groups that arrive because of an alarm, **or of a Crier** (6.11),
  arrive **Alerted and cannot be Surprised that fight**. A lure buys
  position, not surprise.
- **`NoiseTrapVisible` (TP8):** any trap whose effect makes noise ≥ 4
  has `detectDifficulty` ≤ Easy or is `alwaysKnown`, and is never within
  hearing of a group whose join would exceed the floor's joiner cap (the
  hearing part is checked in the `trapsResolve` pass; a failing trap is
  dropped; 6.22). Hidden information never starts a lethal fight.

### 6.19 Floor 1 (lead's call; change TL-19)

- 1–2 traps, including the **lesson trap**: the first trap on the main
  path, `alwaysKnown`, labelled, noise only (Gaol bell tripwire, Kennel
  tin-can tripwire, Wake creaking board). The lesson trap is excluded
  from the out-of-combat trap count and from TP5 `TrapHasStake`: its
  stake is the lesson **(lead's call, after the check)**.
- No trap in main-path rooms 0–1, and no damaging trap in rooms 0–2
  (mirrors DH 17.1 and ENM invariant 6).
- No F1 trap trigger on a patrol Circuit or within 6 of a fight host. No
  rigged fight devices. **So the Gaol's Circuit thresholds take no trap
  on F1**, and FL 7.2's request for Circuit tripwires is met only by
  Bypass alarms (TP7); told to the FL owner (TL-20) **(told plainly
  after the check)**.
- **Pushes stop before F1 trigger squares** (the M1 rule kept on F1).
  This narrows C 11.3 and SH-15 on F1, keeps TR-29 true and reads RT-20
  as "traps are devices, not terrain hazards".
- **Trap conditions on F1 end when combat starts** (a Grappled solo hero
  never enters a fight snared).
- Detect DC ≤ F1 Standard. Damage ≤ 3 for snares and catches; one
  off-path device ≤ 4 (`CapRound` F1; the Gaol's Clock-Weight). No hard
  control (C 11.6).
- Locks: Plain, plus at most 1 consequence lock after room 2: the Gaol's
  Crier, or the Wake's Viewing Door (a Tattle; new lead's call, 7.4). No
  Splinter on F1. No mimic. No Wager room.

### 6.20 Density and placement (lead's call; change TL-20) *(illustr.)*

| | F1 | F2–3 | F4–5 |
|---|---|---|---|
| Traps | 1–2 (lesson included) | 3–4 | 4–5 |
| …in or within 6 of fight hosts | 0 | ≥ 60% | ≥ 60% |
| Out-of-combat traps | ≤ 1 (lesson trap excluded) | 1–2 | ≤ 2 (fallback switch: 1) |
| Hard detect | 0 | ≤ 10% | ≤ 15% of non-fight traps; ≤ 2 per floor |
| Set piece | 0 | ≤ 1 | ≤ 1 |
| Locks | 1–2 | 1–3 | 1–2 |
| Consequence locks | ≤ 1 (Gaol Crier or Wake Viewing Door) | ≤ 1 | ≤ 2 |
| Keybearer | ≤ 1 (the Gaol's guaranteed; Kennel and Wake may place theirs) | ≤ 1 | ≤ 1 |
| Boxed lurker (M3) | 0 | ≤ 1 (Silk) | ≤ 1 (Gut) |
| Dare-or-Wager (M3) | 0 | ≤ 1 | ≤ 1 |

**Placement rules TP1–TP12 (V11):**
- **TP1 Exclusions.** No trigger square on:
  - a Portal square, Squeeze, Plank, Snag, Stir footprint, Hinge or
    Postern radius;
  - within 1 of a Void edge (2 on F2–3);
  - an HE container square, a Clearway, stairs, or the only square of a
    `fleeExit` path;
  - inside another trap's `hunchZone` (`HunchZoneExclusive`; lead's
    call, after the check).
  - A catch on a Door is allowed: its host is not a trigger square, so
    FL 13's "0→1 Portal" socket may be filled by a Door catch or by a
    threshold wire outside the Portal square (lead's call, after the
    check).
- **TP2 Start squares.** No group starts on a trigger square (checked in
  `trapsResolve`; on failure segment 6 re-places that group, else the
  trap is dropped).
- **TP3 `TrapNeverSeals`.** Every trigger has a walk-round of +4 squares
  or less, and never fills a main-path corridor's width or a doorway.
  **Wager exception (lead's call, after the check):** inside a Wager
  room, triggers may close every line to the plinth, but never an exit
  (6.15).
- **TP4 Device areas.** ≤ 4 squares (set pieces ≤ 9); never cover a
  `fleeExit`.
- **TP5 `TrapHasStake`.** Every out-of-combat trap guards something:
  - a cache (as a catch);
  - a shortcut saving ≥ 6 squares or bypassing a Circuit;
  - a Keybearer approach;
  - a Wager room;
  - a Bypass (as an alarm).
  - If nothing fits, no trap is placed (KF3). The F1 lesson trap is
    exempt (6.19).
- **TP6** At most 2 traps per fight host; at most 1 Device. Boss rooms
  carry none (segment 9).
- **TP7 `BypassClean`.** Bypass and Circuit traps are alarms at Easy
  detect or `alwaysKnown`.
- **TP8 `NoiseTrapVisible`** (6.18).
- **TP9 `LockNeverCritical`, `KeyReachable`, `HeNoLock`.**
- **TP10** Force-point distance (6.11).
- **TP11** The floor-1 package (6.19).
- **TP12** `PortalMinimums` sockets are filled. From F2: ≥ 2 main-path
  traps, ≥ 1 before the first fight (Revealed or Easy), ≥ 1 within 6 of
  a fight host; and ≥ 1 out-of-combat trap or lock object tagged
  `noTakeTime` (Hard, a consequence lock, or a tagged showcase) **(the
  last clause: lead's call, after the check; 6.6)**.

**Which pass checks what (lead's call, after the check; 6.22):** the
`traps` pass checks TP1, TP3, TP4, TP5, TP6, TP7, TP9's
`LockNeverCritical` and `HeNoLock`, TP11, TP12, `HunchTrue` and the
bounds. The `trapsResolve` pass, after segment 6, checks TP2, TP8's
hearing clause, TP10, `KeyReachable`, the Tattle binding, the Wager
distance and guard, and the boxed-lurker budget.

**Room roles:**
- corridors and thresholds: wires and plates;
- fight hosts: devices;
- side rooms: catches;
- the Sanctum, antechamber, arena, Alcoves and rest spots: none.

### 6.21 The floor clock (lead's call, told plainly; change TL-38)

- **Ask:** a segment-5 allowance of ≤ +0.3 min `ExploreSeconds` and ≤ +5
  `ExploreClicks` per floor at party 4 on F4–5. This is a **new budget
  line**, not a lever in the agreed CMP 7.4 lever order (KK20).
- **How it sits beside FL's commitment (lead's call, after the check;
  told plainly):** FL 6.10's `ExploreSeconds` ≤ 3.5 min and
  `ExploreClicks` ≤ 40 (FL-16) stay the commitment for the layout,
  terrain and hazard content they already cover. Segment 5's line sits
  on top and is reported separately, so the F4–5 party-4 total may reach
  3.8 min and 45 clicks. That raises the FL-16 figure for segment-5
  content only, which TL-38 flags to the FL owner. The whole floor still
  answers to the agreed CMP floor-time gate.
- **Cut order on a miss (a rule, not only a risk response):** if
  `TrapLockSeconds` or `ExploreClicksDelta` misses, or the CMP
  floor-time gate fails with segment 5 over its line, cuts apply in this
  order until it passes: (1) the fallback switch (F4–5 out-of-combat
  traps drop to 1); (2) at most 1 Hunch-tier trap per F4–5 floor; (3)
  at most 1 lock on F4–5.
- **Mandatory M2 levers:**
  - Probe folded into the single Hunch stop, auto-Probed out of combat
    unless a patrol is due (6.4);
  - the lock card preselects the best route (1 click, 1.5 s read);
  - key walk-ins and Easy Plain auto-picks cost 0 clicks;
  - auto-walk defaults to walk-round;
  - ≤ 2 Hard-detect traps and ≤ 2 locks on F4–5.
- **Fallback switch:** F4–5 out-of-combat traps drop to 1 (the first
  cut above).
- **CMP 7.4 rows:**
  - lock card 1 click / 1 tick (Take time 8);
  - disarm 1 / 2;
  - Probe inside the stop 0 / 2 (auto-Probe 0 / 2);
  - Pole it 1 / 1;
  - force 1 / 1 per try;
  - re-arm 1 / 2.
- **Toy projection:** +0.20 to +0.27 min and ≤ 4.4 clicks (F1 solo
  +0.12).
- **New lead's call:** the Q3 vendor Wax Key adds no dungeon clicks (a
  camp purchase), and its use is the lock card's 1 click / 1 tick.

### 6.22 Seeds and determinism (lead's call; change TL-3)

- **Generation in two passes (lead's call, after the check).** Several
  rules need monsters that are not yet placed when traps are drawn, so:
  1. **`traps`**, a child of each floor's dungeon substream, after
     `hazards` and before encounter placement, with fixed draw counts
     per socket (R-SEED-3). It draws only geometry and kinds:
     - trap kinds, positions, tiers and DCs;
     - Hunch zone offsets;
     - lock kinds, including the Tattle candidates;
     - Keybearer slots;
     - boxed-lurker sockets;
     - Wager layouts.
     V11's structural rules check this pass (6.20).
  2. **`trapsResolve`**, a second child stream with fixed draws, run
     after segment 6 places encounters. It resolves each Tattle
     candidate (its group, or a Crier), binds each Keybearer slot to a
     placed unit, confirms boxed lurkers against the budget, and checks
     TP2, TP8's hearing clause, TP10, `KeyReachable` and the Wager
     distance and guard.
  - **Failure actions, per rule:** TP2: segment 6 re-places that group,
    else the trap is dropped. TP8: the trap is dropped. TP10: the lock
    becomes Plain. `KeyReachable`: the key moves to a container within
    2 rooms, else the lock becomes Plain. Wager distance or guard: the
    Site reverts to an ordinary side room. Boxed lurker over budget: the
    socket becomes a real container.
  - **Retry and terminal fallback:** a V11 failure retries only the pass
    that failed, once, at seed + 1 (HE-7 style). If the retry also
    fails, the failing traps are removed and failing locks become Plain,
    keeping fixed draw counts, and V11 runs again. If it still fails,
    the floor ships with the segment-5 minimum (TP12's sockets filled
    with `alwaysKnown` Easy alarms) and the `V11Fallback` report counts
    it.
- **Act-time object checks** draw `d20 = H(floorSeed, objectId,
  checkKind, attemptIndex, die)`, **with no unit in the key**, so
  swapping members cannot fish for a better roll. Advantage uses `die` 0
  and 1. **`attemptIndex` per kind (lead's call, after the check):**
  - pick, disarm, hand-trigger: always 0 (one roll per hand verb per
    floor);
  - force: `forceFails`;
  - Squeeze and Plank: the object's `traversalIndex`, incremented on
    every check in and out of combat (6.12);
  - out-of-combat Trigger Shot (6.7): always 0, kind `triggerShot`;
  - the Iced padlock's Smash (7.8): always 0.
- **Out-of-combat saves forced by traps or consequence locks** draw
  `H(floorSeed, objectId, 'save', unitId, springIndex, die)`, where
  `springIndex` counts the trap's springs (or the lock's consequence
  firings), so a re-armed trap's second spring draws a fresh die, and
  Bear it's advantage uses `die` 0 and 1. In-combat saves and in-combat
  attack rolls (Trigger Shot included) stay on the combat stream. An
  out-of-combat attack against an object (Trigger Shot, the Iced
  padlock's Smash) is on `checks`, so a device shot between fights never
  moves the next fight's rolls (main 6.3). Attack-to-test on a container
  makes no roll.
- **Properties:** order-independent (opening a chest never moves the
  next attack roll; main 6.3 kept), reload-proof and replayable.
- **What this amends:** ABL 14.4 (settled) narrows to "unit attacks,
  saves in combat and contests". HZ limit 2 is read as "hazards make no
  act-time draws; traps' object checks use `checks`".
- **The Q3 vendor stock draws nothing** (new lead's call; 6.11); the gap
  card keeps its own `recruit` substream (CMP 7.3).
- **Fallback if refused:** every object check becomes a threshold
  (19.2).
- **Builders' option:** salt `checks` with a run id for daily or shared
  seeds.

### 6.23 UI and previews (lead's call; segment 10 owns the display)

- **Glyphs** (the RC5+ rows, pattern registry and timbres are in 8;
  revised after the check):
  - one cross-theme trap glyph on Revealed triggers, with a
    `triggerKind` sub-mark; the device area outline in the trap rim on
    hover (always shown for Devices); a gear mark on the mechanism;
  - the trap link: a dashed tether from trigger to mechanism to area, on
    hover, and always for the lesson trap;
  - state badges: armed, jammed, sprung, disarmed, party-armed (M3);
  - the Hunch zone: a dotted perimeter rim with question-mark corner
    ticks and no fill (not a hatch), with its three-way stop;
  - the Disturbance track: one cross-theme 3-pip meter on the Wager
    Portal glyph and in the room header (6.15).
- **Three-part preview:** *"Disarm (Scout) 65% · Jam 20% (noise 4) ·
  Botch 15%: 1d4+1 piercing, Reflex half; hits you; floored (out of
  combat) / LETHAL (in combat)."*
- **Hunch entry in combat (Q1):** *"Hunch zone: up to 9 if a trap is
  here · not floored"* with the lethal tag (6.5); the cracked-heart
  shape when it would take the unit to 0.
- **Botch preview with a catch:** the lock card adds "the catch will
  spring" whenever it would (6.13).
- **Lock card:**
  - kind glyph and rule card;
  - the hearing outline per route;
  - the patrol forecast;
  - Take time where offered;
  - the preselected route;
  - the contents hint;
  - the Wax Key count.
- **Prompts:**
  - confirmation on the first deliberate entry per trap and on moving an
    ally onto one;
  - the Bear-it HP line and its below-50% warning.
- **Count line:** "traps found: k · open Hunch zones: h".
- **Plates:** the Keybearer pip.
- **Camp:** the vendor's Wax Key line and the gap card's coverage-strip
  line (Q3; 6.11).
- **Codex pages:**
  - "Natives know their floor; the house trusts its traps until one
    bites";
  - "Device bodies never lie";
  - "A padlock is a lock";
  - "Boxed lurkers show their tell".
- **Death recap:** shows trap provenance (`detectionAtFire`, 6.5).
- **Codex:** the "wide berth" line joins the natives page (6.4).

---

## 7. Per-theme catalogue

**(lead's call; stands as written, with the renames of A.5 and the
re-tells and re-bodies made after the check, each marked where it
falls)**

Conditions come only from the theme's 6 (TL-39, the RT-16 ruling
request). If RT-16 rules damage-only, every condition below drops. All
damage is within the 6.10 bands, after weakness. Set pieces ≤
`CapRound`.

**Reserved tell silhouettes (lead's call, after the check; 6.3, 8):**
each theme's dressing avoids the object families of its own trap tells
and device bodies, listed per theme below as "Reserved".

### 7.1 Summary

| Theme (floors) | Traps | Locks and keys | Boxed lurker | Wager room |
|---|---|---|---|---|
| Gaol (F1) | bell tripwire (lesson), Appeals trapdoor, Clock-Weight (off-path on F1), Confession Box, portcullis (damage only) | Plain cell padlocks (M1), 1 Crier, Jailer's keyring, Hour lock (M3) | — | — (F1) |
| Kennel (F1; M1 subset) | Snare (M1 record), tin-can tripwire (lesson), Gin Jaw; Scent Bladder, Deadfall (M3) | Plain pen padlock (M1), Huntsman's chest (Keyed) | — | — |
| Wake (F1, conditional) | creaking board (lesson), Closing Coffin, Crepe Veil (rune), Gramophone, Lid Rack | Plain, the Will-Box (Keyed), Viewing Door (Tattle, after room 2), Hour lock (M3) | — | — |
| Choir (F2–3) | Sluice, Drowning Grate, Belfry Wire, Responsory (rune), Drowned Chandelier (set piece) | Plain, Barb, Vestry door (Keyed), Hymn Riddle lock (M3) | — | the Loft of Responses |
| Silk (F2–3) | Shelf-Pull, Ink-Pot Plate, Paper-Cut Tray, Whispering Catalogue; Shush Glyph (M3) | Plain, Crier, web seal (fire), Closed-Stack cage (Keyed), Index Riddle lock (M3) | Margin-Crawler drawer | the Restricted Section |
| Carnival (F2–3) | Stage Trapdoor, Blade Wheel, Jack-in-the-Box, Oil Bucket; Mirror Glyph (M3) | Plain, Crier, High-Striker, Ticket lock (M3), Ring key (Keyed) | — | the Last Act (guarded) |
| Supper (F4) | Icicle Chandelier (set piece), Dumbwaiter Drop, Taster's Plate, Cold Seat | Plain, Barb, Tattle, Iced padlock, cellar door (Keyed), Place-Setting Riddle lock (M3) | — | the Top Table |
| Gut (F4) | Lip Catch, Acid Gland, House Keys on Strings, Spore Bladder | Plain/Barb sphincter caches, Fed sphincter (M3), Tenant's strongbox (Keyed) | Gutworm strongbox | the Stomach |
| Heart (F5) | Lullaby Glyph (rune), Hand Under the Bed, Rattle Wire, Darning Volley | Plain diary lock, Nursery door (Keyed + Tattle), Runed (M3), Story Riddle lock (M3) | — | the Night Nursery |
| Cathedral (M4) | Rose Pane, Thurible Wire, Collection Plate, Pew Rack (damage-only until its 6 exists) | Rood Screen gate | — | the Rose Window |

### 7.2 The Gaol of Small Hours (F1; M2 core)

**Law:** Lockdown (doors, noise, keys). **6:** Bleeding, Staggered,
Grappled, Dazed (boss only), Prone, Frightened. Traps never apply Dazed.
Mindless types: 0.

- **Bell tripwire** (wire; lesson trap):
  - Tell: a brass hand-bell on a wall bracket, its cord running down the
    wall.
  - Detect Easy; disarm Easy (snip it, 1 tick).
  - Step over once known, or ring it as a lure. The noise 10 comes
    **from the bracket**, which may hang a room away, and the hearing
    outline shows who comes.
  - `oneShot` alarm (TL-30). RC7 alarm cue under a dull "tock".
- **Appeals trapdoor** (trapdoor):
  - Tell: a brass APPEALS plaque, and a hinge-rod cover above it.
  - Detect Standard. Drops and **recloses**: Prone plus 2 bludgeoning.
  - Shunt filing crates onto it. Shove an Inmate onto it (from F2
    equivalents only; no push edges on F1).
- **Clock-Weight** (device, `handle`):
  - A hanging iron weight. A hidden plate 2–3 squares away is the
    trigger, and the pendulum pawl is the handle.
  - **On F1 it guards an optional cache, away from fight hosts.** It is
    the showcase where "the Turnkey walks under the weight" teaches the
    rule. It returns as a fight-room device in the F2+ catalogue
    grammar.
  - Damage 4 bludgeoning plus Staggered (the one F1 off-path device,
    6.19). The proposal's "every clock shows the same hour for one
    frame" VFX is cut: a single frame is unreadable, and a room holds
    one clock (HF11) **(lead's call, after the check)**.
- **Confession Box** (catch on a cache):
  - Tell: a pin-pricked speaking grille.
  - Damage 2 piercing plus Bleeding. `oneShot`.
  - The Jailer's keyring opens it cleanly ("the confessor's key is
    honest"): an instance of 6.13's rule that keys never spring a
    catch.
- **Lockdown portcullis** (device on an Arch on optional content or the
  Circuit):
  - Tell: a spiked drop frame in the lintel slot (FL 8.1).
  - **Damage only in M2** (RS 8.3).
  - The Arch-to-Grille "Bar" effect is M3, flagged against RS 8.3 and RS
    7.6. If adopted, it is lifted by "Winch up" (Athletics Standard,
    loud 6), which monsters may also do.
- **Locks:**
  - cell padlocks: Plain, M1; force is the solo Vanguard's route;
  - 1 Crier after room 2;
  - the **Jailer's keyring** (Keybearer: the Turnkey) opens every
    cell-tagged lock;
  - Hour lock: hourglass-plated cell doors that open when the Small
    Hour Strikes (M3); no clock faces on doors (HF11; lead's call, after
    the check).
- **Reserved:** wall-bracket hand-bells, brass plaques over hinge-rod
  covers, hanging iron weights, speaking grilles, spiked lintel frames.
  Non-device clocks never hang over walkable squares.

### 7.3 The Kennel Wood (F1; M1 subset)

**6:** Bleeding, Grappled, Dazed, Prone, Frightened. Traps never apply
Dazed.

- **Snare** (plate):
  - Tell: a bent sapling above head height.
  - Effect: Grappled (escape at Standard; an adjacent ally cuts it free
    with an action and no check). No damage.
  - Ends at combat start on F1.
  - **M1 known-armed DH-32 record.**
- **Tin-can tripwire** (wire; lesson trap; re-told from RS 8.4's "horn
  tripwire", **lead's call, after the check**; TL-43):
  - Tell: a string of tin cans on a branch above head height.
  - Noise 10 at the cans; the spring plays the reserved alarm cue, never
    a horn (RS 8.4 reserves horns for joiners). Pull the Pack to a log
    chokepoint.
  - A horn on a post read as the HZ Horn Post Cord: a hazard and a trap
    with one silhouette.
- **Gin Jaw** (plate):
  - Tell: a staking chain.
  - Damage 3 piercing plus Bleeding (it stands in for Hobbled, which is
    Later).
  - Kick a feed barrel onto it, or Shunt a hay bale onto it.
- **M3:**
  - **Scent Bladder:** `scent` trigger, noise 8 heard by hound families
    only.
  - **Deadfall:** device; leaves a Low cover log; F2+ grammar only.
- **Locks:** pen padlock (Plain, M1); the Huntsman's lodge chest (Keyed;
  the Keybearer is the Huntsman).
- **Reserved:** bent saplings, tin cans on string, staking chains.

### 7.4 The Endless Wake (F1; with the Wake's wave)

**6:** Staggered, Unfocused, Bleeding, Dazed (boss), Prone, Frightened.

The Late Uncle owns the "coffin with a lurker" tell (RC2). Trapped
coffins show a pulley cord, cache coffins show the container marker, and
a coffin never shows two of these at once. Coffin lids stay horizontal
(RC2).

- **Creaking board** (plate; lesson trap): a rucked runner rug. Noise 6;
  Dormant guests hear at half radius.
- **Closing Coffin** (catch): a counterweight wire (thin, taut, with a
  brass eye; never the rope-with-handle read of an HZ Cord) to a ceiling
  pulley. Damage 3 plus Staggered. Pole it (6.7).
- **Crepe Veil** (glyph; renamed from "Mourners' Veil", a Herald word,
  after the check): a crepe garland overhead. Damage 2 psychic plus
  Unfocused. Arcana or Mechanics.
- **Gramophone** (plate, M2):
  - Stepping on the listener's square plays it for 3 ticks at noise 6
    per tick. Any adjacent unit stops it.
  - The Wake's lure.
  - Body: a cabinet gramophone with its lid closed and no horn (a
    forward-pointing tube is an RC2 piece ban). Its cue is a
    needle-scratch over a crackle bed, with no melody, so it never
    quotes the Heart's lullaby or any bed (RC7; 8) **(lead's call, after
    the check)**.
- **Lid Rack** (device): lids stacked flat on a high rack. Damage only.
- **Locks:**
  - Plain;
  - the Will-Box (Keyed; the Keybearer is the Undertaker);
  - the Viewing Door (Tattle; after room 2; never wakes a group in rooms
    0–2);
  - Hour lock (M3).
- **Reserved:** rucked runner rugs, ceiling pulleys with counterweights,
  crepe garlands, closed cabinet gramophones, high lid racks.

**New lead's call:** the Viewing Door is a Tattle lock on F1, below
6.11's "from F3" row. It is the one exception, kept from the proposal's
catalogue, and it counts as the Wake's floor-1 consequence lock in 6.20
(≤ 1), in place of the Gaol's Crier; 6.19's floor-1 lock line reads
"Plain, plus 1 consequence lock after room 2 (the Gaol's Crier or the
Wake's Viewing Door)".

### 7.5 The Drowned Choir (F2–3)

**6:** Soaked, Chilled, Staggered, Grappled, Prone, Frightened. No push
effect within 2 of a Void edge.

- **Sluice** (device, `oneShot`): a sluice-gate hatch in the wall with
  its gate visible (re-bodied after the check from "a gargoyle mouth",
  which read as an HZ vent face). Soaked plus push 1 over a 2×3 line.
  Pairs with lightning (the Conduct Law), both ways. It is the sprung
  twin HZ 6.1 kept for this segment (the Sluice Wheel Cord is HZ's).
- **Drowning Grate** (plate):
  - Grappled by the undertow plus Soaked.
  - Shunt hymnal crates onto it. Shove a Verger in.
- **Belfry Wire** (wire to a visible bell device; renamed from the
  catalogue's "Passing Bell", the Herald's Art I): noise 10, and units
  within 2 of the bell are Staggered. Trip it while a group stands
  beneath.
- **Responsory** (glyph, device):
  - The lectern square is the trigger. A remote 3×3 circle elsewhere in
    the nave is the area.
  - Damage 6 cold maximum plus Chilled (F3): cold is a Soaked
    weakness, so its pre-weakness maximum is floor(10 / 1.5) = 6, and 9
    after weakness stays inside `CapRound` (6.10; re-derived after the
    check from the proposal's 9).
  - Natives avoid the lectern and stand in the circle.
- **Drowned Chandelier** (set piece, `handle`, F2+): 2×2, damage ≤
  `CapRound` plus Prone. Leaves Low debris (M3).
- **Locks:**
  - Plain;
  - Barb;
  - the Vestry door (Keyed; the Keybearer is the Deacon);
  - the **Hymn Riddle lock** (M3): the hymnboard elsewhere on the floor
    gives the number, and a Lore-trained party reads it at once.
- **Wager room:** the Loft of Responses. Numbered stalls, where
  off-verse stalls are Responsory triggers. The Disturbance track is the
  cross-theme meter (6.15); each pip plays a one-shot creak of the loft
  timbers, outside every Stir family and never a bell (8). The Dormant
  Choristers below climb up when it fills.
- **Reserved:** sluice-gate hatches, lecterns on walkable squares,
  hanging chandeliers over walkable squares (non-device lighting is wall
  sconces only), and non-device bells over walkable squares (they sit
  only in the non-walkable belfry backdrop).

### 7.6 The Silk Archive (F2–3)

**Law:** fire is the key. **6:** Restrained, Poisoned, Unfocused,
Grappled, Prone, Frightened.

- **Shelf-Pull** (device, `handle`):
  - Tell: one protruding book at head height.
  - A tripwire is the trigger.
  - Damage in a line of 3 (the stack topples).
  - Pull the book while a Spine-Binder stands in the line.
  - Distinct from the Leaning Stacks Stir (HZ).
- **Ink-Pot Plate** (plate, `poison`, `paper`): Poisoned, silent.
  Medicine smells it. **Fire disarms it** (TL-32).
- **Paper-Cut Tray** (catch, `paper`; moved after the check from a
  drawer to a card-catalogue tray lid, so it never shares a host with
  the Margin-Crawler drawer):
  - Tell: one blank tray label.
  - Grappled; while held, it whispers at noise 4 per tick.
  - Fire disarms it with the contents untouched.
- **Whispering Catalogue** (plate, `paper`): reads your name aloud at
  noise 10. Detect Easy (`NoiseTrapVisible`). Burn it (noise 4), or
  spring it as a lure.
- **Shush Glyph** (M3, `room` trigger; renamed after the check from
  "Silence Glyph", which echoed the Silk Mood's "Silence Kept"):
  - A room rule, **always known on entry**: a SHUSH plaque and candles
    that never flicker.
  - The first loud act in the room hurts the actor with psychic damage
    plus Unfocused.
  - Let the monsters open the fight here.
- **Locks:**
  - Plain;
  - Crier;
  - **web seal** (webbing over a padlock base): any fire opens it (noise
    4), or pick or force it;
  - the Closed-Stack cage (Keyed; the Keybearer is the Head Archivist;
    renamed after the check from "Overdue cage", a Silk Mood name; a
    padlocked cage, unlike the padlock-free Wager cages);
  - the **Index Riddle lock** (M3).
- **Boxed lurker:** the Margin-Crawler in a catalogue drawer.
- **Wager room:** the Restricted Section. Slow hands, or burning through
  (fast, but each burn is noise 4 and fills a pip).
- **Reserved:** protruding books at head height, blank tray labels,
  SHUSH plaques. Ordinary shelves keep their books flush.

### 7.7 The Carnival of the Last Evening (F2–3)

**Law:** oil and fire, both ways. **6:** Burning, Smouldering, Oiled,
Staggered, Prone, Frightened.

- **Stage Trapdoor** (trapdoor):
  - Tell: a painted star under a spotlight rig.
  - M2: Prone in place, and it **recloses**: the Carnival has no Void
    skin in M2 (HZ's M2 art budget has the Choir's and the Silk's), so
    no Carnival trapdoor leaves a Void until it has one and a V10-proven
    Void room (6.3; lead's call, after the check).
  - M3: relocation to the wings, same room, ≤ 6 squares, in combat
    (TL-41), once the ability model's teleport rule exists (ABL 8);
    otherwise Prone in place.
- **Blade Wheel** (device, `handle`; re-bodied and renamed after the
  check from "Spinning Target", whose flat round knife board echoed the
  Knife-Thrower's target board, an RT RC2 piece ban): a wall-bracket
  rack of blades that spins out along the wall. Damage to the first unit
  in a line of 4.
- **Jack-in-the-Box** (catch on a prize trunk):
  - Tell: a crank wound to its stop with a paper "DO NOT" tag. It never
    moves at rest (idle motion is the lurker's grammar; 6.3). Never a
    keyhole.
  - Oiled, push 1, honk (noise 6).
- **Oil Bucket** (catch on a Door):
  - Tell: a bucket rim over the lintel.
  - Oiled to the opener and the square behind.
  - Natives path round that Door. A party that re-hangs it over a
    patrol's Door can then light the patrol (re-arm once per floor).
- **Mirror Glyph** (M3): swaps the stepper with the nearest unit in its
  frame, previewed. Mirrors render no reflections (RC1). A swap is a
  teleport, so it waits for the ability model's teleport rule (ABL 8;
  TL-41); if that rule is refused, the Mirror Glyph is cut.
- **Locks:**
  - Plain;
  - Crier;
  - the **High-Striker** (a no-roll Strength threshold: passive
    Athletics ≥ Standard opens a padlocked prize cage, and it sounds the
    reserved alarm cue at noise 10; the Vanguard's lock);
  - the Ticket lock ("Admit One", M3: coin buys access, never loot);
  - the Ring key (Keyed; the Keybearer is the Ringmaster).
- **Wager room:** the Last Act (guarded). A garrison stands in the ring
  among revealed trapdoors, buckets, the Blade Wheel and braziers.
- **Reserved:** painted stars under spotlight rigs, buckets over
  lintels, wound cranks with tags, wall blade racks.

### 7.8 The Long Supper (F4; F3 from M3)

**Law:** freeze and shatter. **6:** Chilled, Frozen (hard), Staggered,
Poisoned, Prone, Frightened.

- **Icicle Chandelier** (set piece, `handle`; FL 7.8):
  - 3×3 area, damage ≤ `CapRound` cold and piercing, plus Chilled.
  - **Hard part:** Frozen on the centre square, once per fight. Never on
    a Botch or a re-arm.
  - The boss-arena instance belongs to segment 9 and uses this record
    (TL-40).
- **Dumbwaiter Drop** (trapdoor):
  - M2: Prone in place.
  - M3 relocation: out of combat to the linked scullery; in combat only
    within the same room (the HZ in-combat Swallow rejection holds).
    It waits for the ability model's teleport rule (ABL 8; TL-41);
    otherwise it stays Prone in place.
- **Taster's Plate** (catch, `poison`): silver tarnished black round one
  fork. Medicine smells it.
- **Cold Seat** (plate; the head-of-table square, a "Reserved" card):
  Chilled plus Staggered.
- **Locks:**
  - Plain;
  - Barb;
  - Tattle;
  - the **Iced padlock** (a frosted skin on the padlock base; picking
    it has disadvantage; any fire thaws it
    to normal; a bludgeoning Smash shatters it at Athletics Easy, noise
    6);
  - the cellar door (Keyed; the Keybearer is the Sommelier);
  - the **Place-Setting Riddle lock** (M3).
- **New lead's call:** the Iced padlock's Smash is RT's existing Smash
  verb (ABL 7.2 row) extended to that one padlock: out of combat 1 tick,
  in combat it replaces one attack; one roll on the `checks` stream (a
  Botch is a Jam); a shattered Iced padlock opens its host like a force,
  with no Splinter.
- **Wager room:** the **Top Table** (renamed; A.5).
  - Lifting the cloche is the lift pip; its greed plates are two place
    settings that arm at the next pip (6.15). The proposal's "gong" and
    "chandeliers drop in announced course order" are cut after the
    check: a course order is a second countdown beside the track
    (HF11), and it echoed the Supper's Courses Stir and its course bell.
    Each pip plays a non-pitched metal wash, outside the course-bell
    family (8).
  - Dormant Frozen Footmen sit at the table. They are mindless, so they
    walk through traps, and the party can use that.
- **Reserved:** "Reserved" cards, silver tarnished round one piece,
  hanging chandeliers over walkable squares (non-device lighting is
  wall sconces only), dumbwaiter hatches in the floor.

### 7.9 The Gut of the World (F4)

**Law:** attrition. **6:** Corroded, Withered, Poisoned, Grappled,
Prone, Frightened.

- **Lip Catch** (catch on a sphincter Door; renamed from "Swallow", an
  HZ name):
  - Tell: stained lips and a bile drip line. No twitch: idle motion is
    the lurker's grammar (6.3; lead's call, after the check).
  - Acid damage plus Grappled. While a unit is clamped, the Portal is
    blocked and counts as closed for hearing.
  - Shove a monster in to plug the doorway.
- **Acid Gland** (device, `oneShot`; the HZ 6.1 sprung twin "acid
  geyser", renamed here, A.5): a 3×3 area round a floor gland; the
  mechanism is a wall valve-knot that cannot read as a vent mouth.
  Corroded.
- **House Keys on Strings** (wire; tagged `noTakeTime`, lead's call,
  after the check):
  - Step over it at the usual +1, **or** disarm it, by roll only, to
    gain the Tenant's key.
  - A Jam here is the universal noise 4 (one rule card; the proposal's
    "jangle, noise 6" is withdrawn after the check). A Botch springs it:
    noise 10 plus Grappled.
  - The best risk/reward choice in the segment, now that no party can
    take it at zero risk.
- **Spore Bladder** (plate, `poison`): Withered (blocks healing). Wither
  a monster before the Spore-Priest heals it.
- **Locks:**
  - sphincter caches (a padlock on the rim; Plain or Barb, with an acid
    Botch);
  - the **Fed sphincter** (M3: 1 supply opens it with no check; its
    padlock carries the supply glyph);
  - the Tenant's strongbox (Keyed by the tripwire's key; a padlocked
    strongbox, never organ-swollen);
  - the Landlord's ledger lock (Keybearer).
- **Boxed lurker:** the Gutworm in a swallowed strongbox, organ-swollen
  and with no hasp (6.14).
- **Wager room:** the Stomach. A gland field to a central island. The
  Disturbance track is the cross-theme meter (6.15), with a one-shot
  cue per pip outside the Gut Stirs' gurgle-pop family; the Peristalsis
  motion is never used for it (lead's call, after the check). The
  Antibody Knights are the guard.
- **Reserved:** stained lips with drip lines, wall valve-knots, strings
  of keys, spore bladders.

### 7.10 The Dreamer's Heart (F5)

**Law:** waking. **6:** Unfocused, Poisoned, Dazed, Restrained, Prone,
Frightened. No hard control.

- **Lullaby Glyph** (glyph, device):
  - Tell: a mobile turning without wind.
  - Dazed within radius 2 (a legal turn-part major, C 11.6).
  - Natives stand in the radius.
  - It may quote the lullaby only as a broken visual fragment (RS 8.11):
    its cue is the mobile's dry wind-up click with no melody, so no trap
    cue borrows the bed (8; lead's call, after the check).
- **Hand Under the Bed** (plate):
  - Tell: a child's shoe left facing the bed and a valance hem torn in
    finger-width slits. Nothing moves at rest (6.3; re-told after the
    check).
  - Psychic damage plus Restrained.
  - The hand is VFX on springing only, never dressing (RC1).
- **Rattle Wire** (wire; renamed from "Waking Bell"): noise 10, with a
  paper or bead shaker cue, outside the HZ Horn Post's wooden rattle
  family (8). A "rudely woken" morale rider is a request to the enemies
  owner (TL-30; fallback: noise 10 only).
- **Darning Volley** (wire, `poison`; renamed after the check from
  "Pincushion Volley", which echoed RT's pincushion face): Poisoned in a
  line of 4.
- **Paper burns** applies to story-page devices.
- The catalogue's "Weary" set piece is removed: Weary is not in the
  Heart's 6.
- **Locks:**
  - Plain diary lock (a Lore reading gives a floor fact, M3);
  - the Nursery door (Keyed by the Mother-Shape, plus Tattle);
  - Runed (M3);
  - the **Story Riddle lock** (M3).
- **Wager room:** the Night Nursery. Dreaming cot groups wake and break
  early (the Law).
- **Reserved:** wind-up mobiles over walkable squares, a shoe facing a
  bed, slit valances, darning needles in a frame.

### 7.11 The Inverted Cathedral (M4)

Damage-only until its 6 is authored:
- **Rose Pane** (leaves a rose-window Void, a fall upward to the
  chandelier Snag, HZ-26);
- **Thurible Wire** (renamed after the check from "Censer Tripwire":
  the Cathedral already has HZ's censer-rope Cord, and a trap must not
  share its read);
- **Collection Plate** (pay 5 coin, or it bites);
- **Pew Rack** (leaves Low cover).

Lock: the Rood Screen gate. Wager room: the Rose Window.

---

## 8. Readability, art, VFX and audio

**(lead's call; the tables, the pattern registry, the timbre table, the
art budget and the bans below: lead's call, after the check, in the HZ 8
format)**

- **RC1:**
  - device bodies are always rigged;
  - Hunch zones always contain a trap;
  - no fake plates, empty "trapped" chests or lying hovers;
  - a forced lock shows a hanging padlock, and a Splinter shows an empty
    frame with a jagged edge on the frame itself (true states; no floor
    debris, no loose panel; 6.11);
  - **snare and catch tells render only from Revealed**, through a
    Reveal transition that reads as the object settling, never as a
    spawn (6.3);
  - **no trap tell moves at idle**; idle motion on containers and
    Furnishings is reserved for lurkers and boxed lurkers (RC2);
  - **kit silhouettes:** no dressing in a theme shares a silhouette
    family with that theme's device bodies or trap tells (the per-theme
    "Reserved" lists in 7, as RT did for Kegs). Hanging dressing over
    walkable squares is limited to families the theme's devices do not
    use. V9/V11 check the lists as content (TL-43).
- **RC2/RC3:**
  - boxed lurkers use their own rest tells, re-authored at container
    scale, and show a face when revealed;
  - **device bodies follow RT's RC2 piece bans** (no chair, tray, door,
    upright coffin lid, shield, tall staff, forward-pointing tube,
    orbiting element or roster prop). The Blade Wheel, the Sluice hatch
    and the hornless Gramophone were re-bodied to meet them (7);
  - every lock is read as a **padlock base decal plus a kind glyph**;
    theme locks are skins on that base (webbing over a padlock, a
    padlock on a sphincter's rim, a frosted padlock). Wager cages carry
    no padlock, only the Wager glyph;
  - no lock glyph uses a face part, a ring or a clock face (6.11);
  - V7 adds "no keyhole on any container".
- **Hazard/trap line:** no trap tell, mechanism or device body reuses an
  HZ Cord, vent or Snag prop family. Trap cords are thin taut wire with
  a brass eye; the thick rope-with-handle read stays HZ's Cords. The
  mechanism mark is a gear, never a pip; "pip" means a counter only.
  The glance test (HZ HF11) covers trap trigger, mechanism and device
  body against vent, Cord, Snag, Stir, the Hairline tell and HZ's Stir
  glyphs.

**RC5+ extension table** (extends RT's four-mark table, RT 8.1, and
HZ's RC5+ rows):

| Record | Base mark | Motion mark | Danger mark | Cover mark |
|---|---|---|---|---|
| Trap trigger (Revealed) | Cross-theme trap glyph with a `triggerKind` sub-mark: plate dot, wire bar spanning both squares, trapdoor hinge edge, glyph sigil (no ring) | — | Hazard tier, in the new **trap rim** | — |
| Device area | — | Push arrows for `shove` effects | Thin outline in the trap rim at hazard tier, plus the damage-type icon; the RC4 hue on the icon only, never as a fill | — |
| Mechanism | Gear mark on the mechanism square; a handle sub-mark for `handle` | — | — | — |
| Trap link | Dashed tether, trigger → mechanism → area, on hover (always for the lesson trap); its dash and end cap differ from HZ's Snag link glyph | — | — | — |
| Hunch zone | — | — | Dotted perimeter rim with question-mark corner ticks, no fill, at hazard tier minus one | — |
| Hunch catch | Hunch badge on the host container or Door | — | — | — |
| State badges | Corner badge set: armed, jammed, sprung, disarmed, party-armed (M3) | — | — | — |
| Lock | Padlock base decal plus kind glyph (below); a Hunch catch badge where it applies | — | — | — |
| Wager Portal | Wager glyph plus the 3-pip Disturbance meter | — | — | — |

**Lock glyph set:** Plain (none); Crier (sound-wave arcs); Barb (a
barb); Tattle (a door with an exclamation bar); Runed (an angular sigil
in a square frame); Hour (an hourglass); Keyed (a key-tooth notch, shown
matched after pickup); Riddle (a text-line glyph, with "no Wax Key" on
its card); Fed and Ticket (a supply or coin glyph); High-Striker (a
threshold glyph). States: intact, hanging (forced), Jammed, splintered
(Doors).

**RC6+ order:** revealed trap triggers and device areas sit at hazard
tier; Hunch zones sit just below it.

**Pattern registry additions:** the trap rim, the Hunch dotted rim with
question-mark ticks, the trap-link dash. The Hunch zone is **not a
hatch**: hatching is Rough's, and an amber hatch over Rough or debris
would differ only by hue. Its hue comes from the telegraph-neutral range,
away from the fire and damage keys, and is verified against fire squares
and Rough under deuteranopia, protanopia, tritanopia and greyscale. The
lethal tag changes shape (a cracked heart) as well as turning red.

**Gates:** "trap trigger against harm square", "device area against
latent footprint" and "Hunch against Rough" join the greyscale and
colour-blind gates; device bodies against dressing join the 85%
role-ID gate ("device or dressing"); "boxed lurker or container"
screenshots (the Silk drawer against the Paper-Cut Tray, the Gutworm
strongbox against the Tenant's) join it too. Interior trapdoor Voids are
tested at 4 rotations in `TemplateReadability`.

**RC7 timbre table** (checked against HZ's table and the RS 8.x Sound
lines):

| Cue | Timbre family | Kept out of |
|---|---|---|
| Trap spring | A dry mechanical snap-release, theme-neutral, under the effect SFX | Every bed |
| Jam | A soft metallic click | — |
| Botch | The snap plus a pained foley | — |
| Alarm (alarm traps, the Belfry Wire, the tin-can tripwire, the High-Striker, the Crier) | **One** reserved alarm-bell cue, the same pitch everywhere, with at most a theme tail | The struck bronze of the fallen-bell piece (TR-20); horns (RS 8.4); the Small Hour Strikes chime |
| Rattle Wire | Paper or bead shaker | The wooden rattle (HZ Horn Post) |
| Lullaby Glyph | The mobile's dry wind-up click, no melody | The Heart bed's lullaby and music box |
| Gramophone | Needle-scratch plus a crackle bed, no melody | Every bed |
| Wager pip (Top Table) | A non-pitched metal wash | The Supper's course bell |
| Wager pip (other themes) | A one-shot theme foley (loft creak, a dry tendon click in the Gut) | Every Stir family |
| Boxed lurker reveal | The lurker's own reveal cue | — |

The alarm is no longer pitched per theme: one reserved cue cannot be
ten variants. Each row joins the audio blind-ID gate (85%).

- **"Feels teeth"** is a UI callout over the opener (global skin) plus
  the boxed lurker's reveal cue, never a bark (RC8; 6.14).
- **Disturbance track:** one cross-theme UI meter, the Wager room's one
  clock (HF11). Theme flavour is a one-shot VFX and cue per pip, never
  the Peristalsis motion, a course order or a bell (6.15).

**Art budget** *(illustr.)*: about 4–5 assets per trap record (tell,
body if a Device, spring VFX, sprung state, disarmed state).

| Wave | Items |
|---|---|
| **M2 shared** | 6 effect-family spring VFX (strike, drop, snare, spray, alarm, shove); 1 hand-work animation set (open, jam, botch); Probe; the Reveal transition; the wide-berth path-bend cue; the trap link; the RC5+ marks above; lock base decal, the M2 kind glyphs (Plain, Crier, Barb, Tattle, Keyed) and states; the Splinter VFX and frame state; Wax Key use and refusal; a Keybearer keyring prop (small, hip-mounted, inside RC2); the Botch landing showing `botchHits`; the trapdoor-to-Void transition (hinged leaves drop, the shaft mesh spawns under the floor plane, the lip glyph and drop rim appear on every edge); the vendor's Wax Key icon and the gap card's coverage line (Q3); the Once Bitten idle glance |
| **M2 per theme** | The M2 catalogue's tells and bodies (7). **First blockout set:** the Gaol M2 core: bell tripwire, Appeals trapdoor, Clock-Weight, Confession Box, Plain and Crier padlocks, the Jailer's keyring |
| **M3** | Runed, Hour, Riddle, Fed and Ticket glyphs; the boxed-lurker reveal and "wax will not take a shape" (2 themes, Q2); the Wager kit (Portal glyph, Grille, plinth, 3 cages, the track meter, pip cues); prize cage open; relocation VFX (if the teleport rule lands); debris leave-behinds |

No lidded Furnishing needs renaming under the `containerForm` route (the
catalogue's TL-44 dropped). The Carnival gets no interior Void skin in
M2, so its trapdoors reclose (7.7).

---

## 9. New mechanics considered

### 9.1 Table

| Mechanic | Verdict | Wave |
|---|---|---|
| Margin rule, one roll per object per hand verb (M-1, L) | Adopted (TL-4) | M1/M2 |
| Touchy second roll (A) | Rejected: more springs and noise for +9 points | — |
| Room Search (A) | Rejected: tedium; dead with Int detection | — |
| max(Perception, trap sense) (A) | Rejected: kills the Int lane | — |
| Qualm for every trap (B) | Rejected as default; adopted as the band −5 Hunch | M2 |
| Take time / Careful work (B, L, M) | Adopted with limits (TL-5) | M1/M2 |
| Medicine smells poison; Arcana for runes | Adopted (TL-6) | M2 |
| Bear it / Eyes Open | Adopted (TL-12) | M2 |
| Pole it; handles; weighted plates | Adopted (TL-13, TL-14) | M2 |
| Once Bitten; opener rule | Adopted (TL-16), from the fun and balance critiques | M2 |
| Per-group belief | Deferred (TL-17) | M3 |
| Consequence locks | Adopted (TL-22) | M2/M3 |
| Wax Key | Adopted (TL-24); at the basic vendor when no Mechanics member **(agreed, Q3; TL-25)** | M2 |
| Lockpicks; Pick counter | Rejected (BQ6, contested); counter as the client's fallback | — |
| Oil the Works | Optional (TL-26) | M3 |
| Keybearers; Lockside Listen | Adopted (TL-23) | M2 |
| Keyhole mimic (Lidmaw/Lidmouth) | Rejected **(agreed, Q2)** | — |
| `containerForm` boxed lurker | Adopted (TL-28) **(agreed, Q2)** | M3 |
| Wager rooms | Adopted (TL-29) | M3 |
| Leave-behind trapdoor Voids, debris | Adopted (TL-31): Voids only in V10-proven Void rooms of themes with a Void skin, no fall at the spring (lead's call, after the check) | M2 (Voids gated) / M3 |
| Paper burns; High-Striker; Riddle locks; Fed sphincter; Ticket lock | Adopted (TL-32) | M2/M3 |
| Relocation traps and the Mirror Glyph; scent and room triggers | Adopted (TL-41); relocation and the swap wait for the ability model's teleport rule (ABL 8), else Prone in place and the Mirror Glyph cut (lead's call, after the check) | M3 |
| Read the Maker (reveal on disarm success by 5+) | Rejected: overlaps Hunch and wide berth | — |
| Second Nature mastery signature | Proposed to the builders (TL-36) | M2+ |
| Unbolting scroll | Adopted (TL-35; needs a CN 16.4 `classTags` entry, lead's call, after the check) | M3 |
| Stuck doors | Rejected; Swollen door M3 | M3 |
| Summoning locks, half-damage Botch, loot-destroying traps, trap salvage | Rejected | — |

### 9.2 Adopted in full (those not already in section 6)

- **Second Nature (TL-36):** the Mechanics mastery signature (proposed
  to the builders, who own the slot under STAT 7.2). Take time costs 2
  ticks and reaches Hard devices, and once per fight it can be used in
  combat as an action on an adjacent device. **It is not offered inside
  a Wager room, on a consequence lock or on a `noTakeTime` object**
  (lead's call, after the check: otherwise a mastery character empties
  the Wager at no risk).
- **Unbolting (TL-35):** a tier-2 scroll (arcane and divine; off-class
  readers use Arcana against 10 + tier). Action, range 6: opens one lock
  or Jammed lock without its consequence, noise 10. Unweaving turns a
  Runed lock Plain. It never opens the Bossgate. As a new arcane and
  divine scroll it needs a CN 16.4 `classTags` entry and the Arcanist
  (ARC 8.10) and Herald (HR 8.8) owners' sign-off, since the CN 16.4
  coverage-or-cut audit is closed (ARC-24, HR-24) (TL-35).

---

## 10. Numbers and fixes (illustrative; toy, not simulator)

| # | Finding | Fix |
|---|---|---|
| NF-a | Approved trap sense leaves 40–63% Hidden; +4–6 pp floor deaths through chip | Proficiency when trained; Hunch band (TL-6, TL-7) |
| NF-b | max(Perception, trap sense) detects 87–100%, so Search has no content | Int-only detection |
| NF-c | Band −4 leaves 20% Hidden for no-Mechanics parties on F5 | Band −5 (13%); Hard ≤ 15% |
| NF-d | Rigged share 0.6 × party-hit 0.2 breaks `TrapHeroDeaths` ≤ 2% and F1 < 5% | Revealed-only AI pushes, `NoCheapKill`, no F1 rigged fights, budget charge |
| NF-e | Safe product: rigged share × party-hit ≤ 0.03 | Fallback if the enemies owner refuses |
| NF-f | Device springs are worth less than an attack from L5 | ≈ 0.9 × `CapRound` bands, riders, Trigger Shot replaces one attack |
| NF-g | Strict one-try force blocks 36–66% of optional locks | Retryable force; third try opens |
| NF-h | F1 Splinter on weak forcers 34–53% | No Splinter on F1 |
| NF-i | Lock noise 0.8–1.2 joins per floor for no-Mechanics parties | TP10 force-point distance; quiet key routes |
| NF-j | Picks: luck-gated, or a tax | No picks |
| NF-k | Wax Key K2 margin of 2–6 coin at 30 | Depth pricing 24–36 |
| NF-l | Mimic as drafted: ~12% of new-player runs die from mimic fights | 20% cap, marking threshold, opener routing (0.08%) |
| NF-m | Clock: only C with levers fits (+0.20–0.27 min) | Mandatory levers; allowance asked |
| NF-n | Band P breaks `CapRound` on F2–4 | Set pieces ≤ `CapRound` |

Further toy results:
- Margin rule against "any failure springs": 8–16% against 28–36%
  springs.
- Take time makes out-of-combat disarm 92–100% safe. **Told plainly
  after the check:** for trained parties that puts nearly all Easy and
  Standard out-of-combat hand work under Take time; `TakeTimeShare` is
  therefore a target band (11.4), and from F2 at least one object per
  floor is `noTakeTime` (TP12).
- Wager rooms with Take time inside: specialists wake the guard 0% of
  the time. That is why each Take time use costs a pip there (per use,
  not per tick, after the check: Sure Hands and Second Nature would
  otherwise bring the 0% back).
- A −30% guard is safer than an ordinary fight. That is why the guard is
  full-strength.
- No-Mechanics parties bearing 30% of route traps: −5 to −7.6 pp. That
  is why walk-round is the default.
- **Told plainly:** the Q3 vendor key is not in the toy. Its effect on
  `NoMechanicsLootLoss` and `WaxKeyUse` is owed to the M2 simulator; K1
  and K2 are checked per camp as for every channel (6.11).

---

## 11. Data, validators and simulator (change TL-20, TL-10)

### 11.1 Records (C# rules core, JSON content)

**(lead's call; the fields marked † added after the check, lead's call,
after the check)**

- **TrapRecord.**
  - SH 8.2 fields: `trapId`, `triggerSquares`, `mechanismSquare`,
    `area`, `effect`, `tier`, `detectDifficulty`, `disarmDifficulty`,
    `armed`, `knownToParty`, `loud`, `rearmable`, `hardPart` +
    `hardFallback`.
  - New fields: `family`, `triggerKind`, `botchHits {worker|area}`,
    `handle`, `tags {rune, poison, paper}`, `alwaysKnown`, `lessonTrap`,
    `setPiece`, `save`, `band`, `condition?`, `hunchZone {squares}`
    (the offset is drawn at generation, so the record keeps only the
    squares †), `tellId`, `noTakeTime` †, `wagerId?` †.
- **TrapState:**
  - `state {hidden|hunch|revealed|sprung|disarmed|rearmed}` (detection
    grades never drop);
  - `jammedUntil`, `disarmedBy`, `rearmedBy`, `rearmUsedThisFloor`;
  - `juryRigRearmedThisFloor`, `hardPartUsedThisFight`;
  - `detectionAtFire {hidden|hunchFloored|hunchWarned|revealed}`
    (DH-32's `detectedAtFire`, widened to an enum †; 6.5);
  - `seenByParty` (first line-of-sight tick) †, `gradeSinceRound` †,
    `gradeChangedDuringAction` †;
  - `hitGroupIds[]` †, `springIndex` †, `lastSpringRound` †;
  - `bearConfirmed` (the first-entry confirmation was given) †;
  - `triggerShotUsed` (out of combat, per floor) †;
  - `botchOrigin?` (when `botchHits: worker`) †;
  - `traversalIndex` (Squeeze and Plank objects) †;
  - M3: `beliefByGroup`.
- **GroupState:** `bitten` (inherited by joiners).
- **LockRecord:**
  - `lockId`, `host {portal|container}`, `kind`, `tier`, `dc`,
    `forceDc`;
  - `forceFails`, `forceOpenedBy` †, `pickRolled`, `jammed`,
    `splintered`, `crierSpent`;
  - `keyId?`, `tattleGroupId?` (Portal hosts only), `catchTrapId?` †.
- **KeyRecord:** `keyId`, `lockIds`, `carrier {unitId|containerId}`
  (the slot drawn in `traps`, bound to a placed unit in `trapsResolve`
  †).
- **Counters:** `party.waxKeys`.
- **Vendor (Q3; new lead's call):** `vendor.waxKeyStock` (0 or 1), set
  at camp entry from the gap card's party snapshot; `waxKeyPriceByCamp`
  in the tuning file.
- **Container additions** (segment 7): `lockId?`, `trapId?`,
  `boxedLurker`, `noCheck`, `markedByParty` †, `heldWaxKey` (a dropped
  Wax Key left at the cap) †.
- **WagerRecord:** `form`, `pips`, `guardGroupId`, `cageSet` (3 distinct
  notables, one the plinth prize †), `woken`, `tickAccumulator` †,
  `prizeLifted` †, `gradeAtLift` †, `greedPlates` (squares and the pip
  that arms them) †.
- **Events:**
  - `CheckResolved{objectId, kind, d20s, bonus, dc, band}`;
  - `TrapSprung{trapId, causer, detectionAtFire}`;
  - `TrapResolved{trapId, route ∈ {walkRound, stepOver, bear, disarm,
    takeTime, triggerShot, handTrigger, handle, pole, weighted, shove,
    probe, left}, unitId}` †, emitted when the trap leaves the party's
    path, or at floor end;
  - `LockOpened{route}`;
  - `ConsequenceFired`;
  - `DoorSplintered`;
  - `KeyUsed`;
  - `WaxKeyRefused`;
  - `V11Fallback` † (6.22).
- **Save fields:** every state above. A `trapDigest` sits beside
  `terrainDigest`, and includes every † field.

### 11.2 Streams and save

- `traps`: a child of each floor's dungeon substream, after `hazards`,
  before encounter placement; fixed draws per socket (R-SEED-3);
  geometry and kinds only (6.22).
- `trapsResolve`: a second child stream with fixed draws, after segment
  6 places encounters; binds Tattles and Keybearers and runs the
  monster-dependent checks (6.22). A V11 failure retries only the
  failing pass, once, at seed + 1, then falls back as 6.22 says.
- `checks`: object checks keyed by `(floorSeed, objectId, checkKind,
  attemptIndex, die)`, no unit, with `attemptIndex` defined per kind in
  6.22; out-of-combat trap and lock saves keyed by `(floorSeed,
  objectId, 'save', unitId, springIndex, die)`.
- The vendor stock draws nothing; the gap card keeps CMP's `recruit`
  substream.
- `trapDigest` beside `terrainDigest` and `hazardDigest`; every
  TrapState, LockRecord state, GroupState bit, WagerRecord and
  `party.waxKeys` is saved.

### 11.3 V11 `TrapsLocks`

Proven structurally where possible; otherwise a fixed-seed property test
(N = 50 per floor *(illustr.)*). The rules split across the two passes
as 6.20 lists (lead's call, after the check):
- TP1–TP12 (TP1 now includes `HunchZoneExclusive`);
- `HunchTrue` (a zone must hide something: its clipped area exceeds the
  trigger area);
- `WagerNonSkillLine` (6.15) and the Tattle host rule (Portal hosts
  only);
- `HiddenTrapBound`, `TrapLethalityBound` (after weakness; the report
  prints each record's after-weakness maximum), `EveryDamagingTrapHasSave`;
- `F1TrapRules`;
- `MimicPlacement`;
- `RiskRoomShare` (≤ 1 Dare-or-Wager);
- `HardControlGates`;
- `HarmfulConditionsRecount` (the RS V1 hook);
- `NoKeyholeOnContainers` (with V7);
- `ReservedTellSilhouettes` (the per-theme "Reserved" lists of 7,
  checked against theme dressing; with V9).

**M2 reports, M3 gates:** `TrapwrightHasMechanism`,
`SpringContentBound`, `DeadRoomRate`.

### 11.4 Reports and gates *(illustr.)*

- **Damage and deaths:**
  - `TrapDamageShare`: 3–8% of party damage taken; 5–12% of damage to
    monsters in rigged fights;
  - `TrapHeroDeaths` ≤ 2%: the share of runs with at least one hero
    death whose killing instance has source = trap (definition: lead's
    call, after the check);
  - `UndetectedChipShare` ≤ 10%: floored-trap damage divided by all
    party damage taken, per floor;
  - `HunchEntriesInCombat`.
- **Check outcomes:**
  - `DisarmOutcomeShares`, `JamRate`;
  - `TakeTimeShare`: a target band of 50–75% of out-of-combat hand
    work for trained parties (restated after the check: the toy's
    92–100% made the old "retune above 80%" trigger fire by
    construction; TP12's `noTakeTime` object brings it into the band);
  - `RouteChoiceMix` (from `TrapResolved`), `LockRouteMix`;
  - `EatRate`, `ChipBeforeFight`.
- **Loot access:** `LootGatedShare`, `NoMechanicsLootLoss` by class and
  party size.
- **Clock:** `TrapLockSeconds` ≤ 0.3 min; `ExploreClicksDelta` ≤ 5;
  `RoundsPerFightDelta` ≤ 0.1.
- **AI and lures:**
  - `OpeningSpringRate` (above 70% means the puzzle is solved: vary
    devices and riders);
  - `PreCombatSpringDamageShare` ≤ 5%;
  - `NativeAreaCatchRate`, `MindlessSprings`;
  - `ChainPullFromTraps`, `LockNoiseJoinRate`;
  - `NoPathStallRounds`.
- **Items:** `WaxKeyUse` (including unspent at run end), split by
  channel (sundries, notables, vendor) **(new lead's call, for Q3)**.
- **Generation:** `V11Fallback` (floors shipped with the segment-5
  minimum; 6.22).
- **M3:** `BoxedLurkerOpenerHits`, the Wager reports (6.15), including
  `WagerWalkRoundRate` (must be 0).

### 11.5 Simulator arms

- baseline;
- no-trap control;
- hidden-heavy (40% Hard);
- no-Mechanics parties (solo Vanguard; Vanguard + Herald), with the Q3
  vendor key on and off **(new lead's call)**, and with the clock toy
  run for this arm (the Hunch stop at nearly every Standard trap;
  lead's call, after the check);
- flat-fail disarm;
- natives-avoid-areas (the value of the lure);
- hand-trigger as one attack.

---

## 12. Staging

**(lead's call; the Q1–Q3 content agreed)**

| Wave | Contents |
|---|---|
| **M1** | Check charter; ladder; `traps`, `trapsResolve` and `checks` streams; Margin rule for locks; Take time for locks; Plain Locked Portals with retryable force (no Splinter on F1); TrapRecord skeleton with DH-32 ids and `detectionAtFire`; optionally the Kennel tin-can tripwire lesson trap, and optionally the Snare as the M1 known-armed DH-32 record off the main path (each known, armed; a hazard for pathing and previews under TL-19); V11 skeleton (`LockNeverCritical`, `HeNoLock`, `KeyReachable`). The solo Vanguard forces; nothing is gated |
| **M2** | Detection (trap sense with proficiency, Hunch −5, Probe stop, count line, Medicine, Arcana); Warned or floored (Q1); Margin rule for traps; Take time for traps; Bear it; universal hand-trigger, Trigger Shot (with the improvised throw if TL-42 is accepted), handles, Pole it, weighted plates, re-arm; K1/K2 knowledge with Once Bitten and the opener rule; caps and bounds; floor-1 package; density and placement; Crier, Barb and Tattle locks; Splinter from F2; Keybearers; Lockside Listen; Wax Key, with the vendor stock when no Mechanics member (Q3); containers; alarm limits; trapdoor Voids in V10-proven Void rooms of themes with a Void skin (Choir, Silk); paper burns; High-Striker; door close verb; the catalogue for the M2 themes; clock allowance; RT-16 ruling; reports (Trapwright reports included) |
| **M3** | Boxed lurker (Q2: Silk and Gut); Wager rooms; Runed and Hour locks; Unbolting; Riddle locks; Fed sphincter; Ticket lock; locked and trapped Hairline; readings; Swollen door; relocation traps and the Mirror Glyph (with the ability model's teleport rule); scent and room triggers; debris leave-behinds; portcullis Bar (if adopted); Oil the Works; per-group belief and witness secrecy; **the Trapwright** with its density gate; Drop-bar if the client agrees |
| **M4** | Inverted Cathedral traps |
| **Later** | Monster trap use; chained springs; summon and wanderer knowledge; party-placed traps (Sapper); party relock (only after a safe-rest ruling); boxed lurkers for themes that gain a lurker (Q2) |

---

## 13. What segment 5 owns, and hooks handed to later segments

**(lead's call)**

**Owns:**
- the check charter, ladder and `checks` stream;
- trap records and rules, including the sprung twins (HZ 6.1);
- detection;
- the knowledge-model requests;
- locks, keys and the Wax Key (its vendor rule with the items and
  companions owners);
- door verbs (FL-8);
- Squeeze and Plank costs (kept);
- Hairline variants;
- boxed-lurker rules;
- Wager-room rules;
- V11.

**Hooks handed off:**

| Segment | Receives from segment 5 |
|---|---|
| **6 Encounters** | The `trapsResolve` pass runs after segment 6's placement and may ask it to re-place a group (TP2); groups never start on triggers; areas allowed; Keybearer tag; Tattle Dormant group; boxed-lurker budget; Wager guards from the floor budget; rigged-fight trap damage charged to the budget; alarm and Crier arrivals Alerted |
| **7 Loot** | Container kinds; `lockable` sockets; +1 rarity step behind consequence locks and in Wagers; graded cages; Wax Key draws; boxed-lurker drops |
| **8 Events** | Readings; Riddle-lock clues; Hour locks on Landmark ticks; Dream Fragments read the threshold rule |
| **9 Boss arenas** | No guaranteed boss-room traps; arena devices use the TrapRecord format (the Supper chandelier); Clearways stay clear; the Bossgate never opens by Wax Key or Unbolting |
| **10 Information** | Every display in 6.23; Listen "a mechanism"; Lockside Listen; the codex pages; the camp's Wax Key line (Q3) |
| **11–12 Seeds and validation** | The streams, V11, `trapDigest`, save fields, the reports, `FloorTimeByTheme` with segment-5 rows |

---

## 14. Binding requirements and how they are met

| Requirement | Source | How it is met |
|---|---|---|
| O1–O13 (3.2) | FL, RT, HZ, SH, RS, DH, CMP | 6.2 (O1), 11.1 (O2), 12 M1 (O3, O4), 6.12 (O5, O6, O8, O9), 7 (O7, O10), TL-39 (O11), 6.21 (O12), 6.6–6.7 (O13) |
| No free retries | STAT 7.2 **(agreed)** | Margin rule; one roll per object; force pays noise each try; Take time pays ticks |
| Pick, disarm, re-arm trained-only | STAT 7.2 (lead's call) | Kept; untrained routes elsewhere |
| Hidden information never kills; a known trap is never safe | DH 10.4 **(agreed, Q6)** | Hidden floored by each trap's own grade; Warned or floored **(agreed, Q1)**; per-square detection with move interrupts; `NoiseTrapVisible`; lethal tags |
| Trap ids and snapshot in M1 | DH-32 | M1 record skeleton |
| No trap hard control on F1; F2 set pieces only | C 11.6 | Kept; F2 hard parts null in M2 |
| Known traps are hazards from M2 | C 11.3, SH-15 | Kept, except F1 pushes stop before triggers (TL-19) |
| Locks on optional content only | FL LG2, LG14 | `LockNeverCritical`; HE always Plain |
| Hairline no roll, no decoys | FL-17 **(agreed, FL Q3)** | Kept; widening is a client item at M3 |
| Ladder cites EnvDC | HZ 6.3.5 | TL-2 |
| No tools; who-has-the-key | ITEMS 2 | No picks; counters; automatic keys |
| The basic vendor list | ITEMS 10.3 **(agreed)** | Changed only by Q3 **(agreed)**: 1 Wax Key when no Mechanics member (TL-25) |
| Opening a chest never moves an attack roll | main 6.3 **(decided)** | `checks` stream, including out-of-combat saves and out-of-combat attacks on objects |
| Keyhole reserved; locks are padlocks | RC3 | No keyhole anywhere in segment 5 |
| ≤ 1 mindless type per theme | RE9 | `containerForm` uses existing lurkers **(agreed, Q2)** |
| No safe rest | ABL 15.1 **(agreed)** | No relock; Drop-bar guard |
| Loot never destroyed | ENM 15.6 | Force and Wax Key always remain |
| XP only from monsters | ENM 16 | Wager guard from the budget; Feed gives no XP |
| Never scale by party size | HZ 6.2.3 | CC6 |
| No content needs a class | CL 6.1 rule 4 | CC4; solo guarantee |
| DEP-S1 call | SH 14 **(agreed, Q2)** | Fallback applied (TL-37) |
| Clock gates | FL 6.10, CMP 7.4 | Allowance plus levers (TL-38) |
| Condition budget | C 16 | No new ids; the theme's 6 only (TL-39) |

---

## 15. Risks

| # | Risk | Signal | Response |
|---|---|---|---|
| 1 | **Clock.** The F4–5 party-4 gate passes only narrowly, and segment 5 asks for +0.3 min | `TrapLockSeconds`; `ExploreClicksDelta` | The binding cut order of 6.21: the fallback switch (out-of-combat traps on F4–5 drop to 1), ≤ 1 Hunch-tier trap, ≤ 1 lock on F4–5 |
| 2 | **The enemies owner may refuse TL-16** (Revealed-only scoring, area trust, Once Bitten) | The owner's ruling | Rigged share × party-hit ≤ 0.03 (NF-e) |
| 3 | **"Natives stand in areas" may read as AI stupidity** | Playtest notes; `NativeAreaCatchRate` | Once Bitten, the codex card, the idle glance animation |
| 4 | **TL-3 narrows a settled ABL line** | The ABL owner | The all-threshold fallback is the only alternative that keeps main 6.3 |
| 5 | **TL-8 narrows agreed DH text** | — | Agreed with Q1; `HunchEntriesInCombat` watched |
| 6 | **Take time makes specialists routine** on Easy and Standard | `TakeTimeShare` against its 50–75% band | The `noTakeTime` object per floor from F2 (TP12); more `noTakeTime` tags if the band is missed |
| 7 | **Sure Hands and Trap Eye** both need the Shade owner's re-scope | The owner's ruling | TL-5, TL-6 rows |
| 8 | **Device value is thin** at the `CapRound` bound | `NativeAreaCatchRate`; `RouteChoiceMix` | Riders; the hand-trigger-as-attack future item (19.2) |
| 9 | **RT-16 is unruled** | The conditions owner | Barb and spray effects fall back to damage-only |
| 10 | **Boxed lurkers are rare** (two themes) under Q2 | `BoxedLurkerOpenerHits` | **Agreed, Q2**: more themes as they gain lurkers |
| 11 | **The Splinter reading** of "Portals never break" | The RS and RT owners | Option B is ready |
| 12 | **Visual load:** trap glyphs, Hunch rims, Hairline tells, Stirs and Snags all compete in the glance test | The glance test | An art readability pass |
| 13 | **Many owners must sign off:** enemies, Shade, DH, FL, CMP, items, consumables, conditions, RS | Owner rows in 18 | M2 rows without their owner ruling ship in the fallback form named in 19.2 |
| 14 | **The vendor key could crowd out the Mechanics gap card** (Q3): a party may prefer coin to a hire | `WaxKeyUse` by channel; CMP's coverage reports | Stock stays 1 per camp; the price pegs K1/K2 hold; the gap card is printed first |
| 15 | **All evidence is toy** | — | Every number is owed a simulator rerun before it settles |

---

## 16. Left to the builders

**(lead's call)**

- Every *(illustr.)* value: the ladder's numbers, Take time ticks, Hunch
  zone sizes, bands, `CapRound` and `HiddenTrapBound` percentages,
  density counts, noise values, Wax Key prices, the mimic marking
  threshold, report thresholds other than the stated gates.
- The Mechanics mastery signature (Second Nature is the lead's proposal;
  STAT 7.2 leaves the slot to the builders).
- Trap and lock sockets themselves, within 6.20 and V11.
- Skins and tells inside each theme's catalogue.
- Godot scene structure for trap bodies, Hunch overlays, lock cards and
  the Disturbance track, with no physics.
- Art, VFX and audio inside the charter (8).
- Names, until the naming pass.
- **Not tuning:** information is never a roll; the Margin rule's grades
  and one roll per object per hand verb per floor (force retries per
  6.11; Squeeze and Plank per traversal); no unit in the `checks` key;
  the two generation passes; at
  least 3 routes per trap and lock, one open to anyone; no lock on
  required content; device bodies always rigged and Hunch zones always
  true; hidden traps floored; Warned or floored with a shown Hunch as a
  warning **(agreed, Q1)**; no keyhole on any container and no roster
  exception for mimics **(agreed, Q2)**; the vendor Wax Key only when no
  Mechanics member, 1 per camp **(agreed, Q3)**; no lockpicks; never
  scaled by party size; build-time proof.

---

## 17. Changes to approved documents

Applied on 2026-11-21, when the client approved this document. **This
document edits no parent document itself**; the rows are applied by the
owners of the sections they touch **(lead's call)**, and rows owed by
other owners are listed again in 18. Every row names the exact section
it touches. Rows that touch **decided** or **agreed** text are flagged
in the "Flag" column; **the client's approval of 2026-11-21 covers
them** (with Q1–Q3 where a row names one; the answers of 2026-11-21
already agreed TL-8, TL-25 and TL-28 in substance). **New lead's call:** the Flag
column also marks rows that only *read* agreed text without changing it
(TL-1, TL-4, TL-5, TL-10, TL-16, TL-21, TL-22, TL-31), beyond the
proposal's ⚑ set, so that every touch of agreed text is visible.
**Lead's call, after the check:** TL-6 is now flagged (it changes the
agreed STAT 6 trap-sense row, which the proposal had marked a lead's
call), TL-23 names ENM 14.8, TL-31 names HZ 6.4 and its gates, and TL-42
and TL-43 are added; every row touched after the check says so.
Told-plainly rows were told in 19.1 and confirmed by the client on
2026-11-21. TL-16, TL-17, TL-30, TL-34, TL-35,
TL-39, TL-41, TL-42 and TL-43 are requests to other owners, recorded as
pending requests; they change nothing until their owner rules, and 19.2
names each fallback. M3, M4 and Later rows are recorded now as applied
text marked with their wave and take effect at that wave; TL-37 takes
effect at the M2 gate.

**Narrowings recorded inside this design (no approved text changes):**
- STAT 7.2's "No free retries" is met by the Margin rule, per-try force
  noise and Take time's ticks (read, not changed);
- RT-20 reads "traps are devices, not terrain hazards" on F1; TR-29
  stays true because pushes stop before F1 triggers;
- HZ limit 2 reads "hazards make no act-time draws; traps' object checks
  use `checks`";
- the sprung twins of HZ 6.1 are this segment's traps, with HZ's Stir
  and Cord twins unchanged.

| # | Document / section (current status) | Change | Flag | Approved by |
|---|---|---|---|---|
| TL-1 | STAT 7.2 "No free retries" (**agreed**; a reading); ABL 7.2 | Check charter CC1–CC8; three check families (6.1) | **Agreed** (read, not changed) | Lead's call, with this document |
| TL-2 | HZ 6.3.5 (cited); RT 6.2.6 Plank (kept); FL 9.2 Locked row "DCs to segment 5" | DC ladder; force +2 (6.2) | — | Lead's call |
| **TL-3** | **Main 6.3 streams (decided; extended like FL-2 / TR-17)**; ABL 14.4 (settled; narrowed to "unit attacks, saves in combat and contests"); FL 9.2 and ABL 7.2 Squeeze row "existing check stream"; RT 6.2.6 Plank (per-traversal rolls kept); HZ limit 2 (reading) | `traps` and `trapsResolve` streams (two passes, after the check); keyed `checks` stream without unit, `attemptIndex` per kind; out-of-combat trap and lock saves on `checks`, keyed by `springIndex`; out-of-combat attacks on objects on `checks` (after the check) (6.22) | **Decided** (main 6.3, extended); told plainly | Client, with this document; ABL owner applies |
| TL-4 | STAT 7.2 (**agreed**; reading); ABL 7.2 disarm, trigger and Locked rows; FL 9.2 Locked row; SH 8.2 / 13.1 `jammedUntil` | Margin rule; one roll per object per hand verb per floor (force, Squeeze and Plank by their own rules); universal Jam to floor end; Jam blocks hands only; the Force row (after the check) (6.6) | **Agreed** (read, not changed) | Lead's answer, BQ1/BQ2, with this document |
| TL-5 | STAT 7.2 threshold clause (**agreed**; reading); ABL 7.2 (new row); SH 7.11 Sure Hands | Take time (Easy/Standard, 8 ticks, channelled, not consequence locks, `noTakeTime` objects, Squeeze or Plank; one pip per use in a Wager); Sure Hands cuts it to 3; Easy Plain auto-pick only with no patrol due (6.6; the last three after the check) | **Agreed** (read, not changed) | Lead's call; Shade owner applies |
| **TL-6** | **STAT 6 trap-sense row (agreed: "All rows (agreed)"; changed)**; STAT 7.1 Arcana and Medicine rows (reading); SH-19 Trap Eye | Proficiency when Mechanics-trained; Arcana for runes; Medicine for poison; living, non-Downed units only; monotonic grades; Trap Eye re-scoped (6.4) | **Agreed** (STAT 6, changed; corrected after the check) | **Client, with this document**; stat model owner applies; Shade owner re-scopes Trap Eye |
| TL-7 | ABL 7.2 (new Probe row; Search row unchanged); ABL movement (the detection interrupt); FL 6.10 auto-walk | Revealed / Hunch / Hidden; per-square detection with move interrupts; Probe from inside or beside a zone; the Hunch stop (auto-Probe out of combat); wide berth with a defined trigger; no search-for-traps verb (6.4; the interrupt, Probe position, auto-Probe and wide-berth trigger after the check) | — | Lead's call |
| **TL-8** | **DH 10.4 (agreed, DH Q6; narrowed: a new trap row; its "Trap provenance" bullet widened)**; **DH-32 (the `detectedAtFire` snapshot becomes the enum `detectionAtFire`)**; DH 17.5, 17.6 (the lethal tag on a Hunch entry); HZ "Warned or floored" (precedent) | A shown Hunch zone is a warning; floored only out of combat or by forced movement (agreed), and in its first round (new lead's call); the floor follows each trap's own grade; a chosen in-combat Hunch entry is self-caused; the floor's-worst-trap preview; party-caused combat-starting springs floored for the party (6.5; the enum, own-grade and self-caused rules after the check) | **Agreed** (DH Q6 narrowed); **Q1** | **Client (agreed 2026-11-21, Q1)**, with this document; DH owner applies |
| TL-9 | FL-20 count line | "traps found: k · open Hunch zones: h", no total (6.4) | — | FL owner |
| TL-10 | RS 13 "Traps and locks" hook (**agreed** hook text, filled); HZ 13; FL 13; SH 8.2; RC1+ | Taxonomy; device bodies always rigged; diegetic tells; record superset (6.3, 11.1) | **Agreed** (RS hook, filled as a reading) | Lead's call |
| TL-11 | C 11.6 (reading) | Botch lands by `botchHits`; never the hard part (6.6) | — | Lead's call |
| TL-12 | DH 17.6 / 17.7 (extended to own movement); C 10.4; C 11.3 / 5.5 (kept); ABL movement | Bear it; every damaging trap has a save; step over wires by default (Bear it on a wire only by explicit choice; after the check); walk-round default; HP preview and warning (6.7) | — | DH owner applies |
| TL-13 | SH-15 / NM-1 staging; ABL 7.2 note "The Trapwright's trap rows … apply when the Trapwright ships" and new rows (hand-trigger kept an Action) | Universal hand-trigger, Trigger Shot (one attack, mechanisms included; out of combat a one-try object check on `checks`, after the check), handles, Pole it (plates, wires and catch hosts; 1 tick out of combat; after the check) (6.7) | — | Lead's call; Shade owner told |
| TL-14 | RT 6.3.6 and the RT 13 row "a Furnishing never springs a trap" (a lead's call; invited narrowing); RT 6.4 | Weighted plates (the end square only, after the check); one act springs at most one trap; collision then spring through EnvRound, the trap outside the one-instance cap (6.7) | — | Lead's call |
| TL-15 | C 11.6 "the rest of SH-15 … when the Trapwright ships"; ABL 7.2 note; SH 14; **SH 8.2 Jury-Rig Re-arm bullet "the universal action route stays unlimited at its action cost"** (new lead's call: named here) | Universal 500‰ re-arm, no hard part, once per trap per floor, not while a hit group is Alerted; Jury-Rig keeps its own counter (6.6) | — | Lead's call; Shade owner applies |
| TL-16 | ENM 11.2 (A-5); ENM 11.3 behaviour rows (invariant 7 kept); C 17.3; **RT `NoCheapKill` (agreed, RT Q3; extended, TR-15)**; the Automaton card; the SH card "monsters avoid known traps" | Natives avoid triggers and trust areas; Once Bitten; opener rule; mindless blunder; Revealed-only AI push scoring; no-path reroute (6.8) | **Agreed** (RT Q3, extended) | Request; **enemies owner** rules |
| TL-17 | ENM 11.2 | Per-group belief; witness-only secrecy (6.8) | — | Request; enemies owner; M3 |
| TL-18 | HZ 6.11 item 2 / HZ-7; RT 6.4 (exclusion kept); C 10.4; C 11.6 (reading) | Bands (printed after weakness, after the check); `HiddenTrapBound` 20%; `TrapLethalityBound` ≤ `CapRound`; traps in EnvRound except self-caused (a chosen in-combat Hunch entry added after the check); budget charge; F2 hard parts null in M2 (6.9, 6.10) | — | Lead's call; HZ and conditions owners apply |
| TL-19 | C 11.3 / SH-15 (narrowed on F1); RT-20 (reading); TR-29 (kept true); DH 17.1; ENM invariant 6; RS 8.2(d)(f) | The floor-1 package, with the Wake's Viewing Door as the one F1 Tattle (6.19, 7.4) | — | Lead's call |
| TL-20 | FL LG2, LG14, LG7 `SkippablePool`, `PortalMinimums`; FL 13 trap-socket hook (the 0→1 Portal socket filled by a Door catch or a threshold wire, after the check); FL 7.2 tripwire request (the Gaol's Circuit takes no trap on F1; Bypass alarms only; told to the FL owner after the check); HZ VP rules; RS 11.2 validator list | TP1–TP12; density; V11 in two passes; the retry and terminal fallback (6.20, 6.22, 11) | — | Lead's call; FL owner |
| TL-21 | FL 9.2 Locked row; STAT 7.1 Athletics and Mechanics rows; ABL 7.2 (new rows); RT 6.3.4 and **RS 7.6 "Portals never break" (agreed; reading; told plainly)**; ENM 12.3 | Pick; retryable force with the guarantee; Splinter from F2 (option B ready); no relock; TP10 (6.11) | **Agreed** (RS 7.6, read); told plainly | Lead's answer, BQ5, with this document |
| TL-22 | FL 8.1 decal kit; **RC3 / RC5 / RC7 (agreed, RS Q3 charter; extended)**; RT-16; HZ-19; RS 8.3 | Crier, Barb, Tattle (Portal hosts only); Runed, Hour; set off a Crier; rarity step; glyphs redrawn without face parts, rings or clock faces, and the padlock-base-plus-glyph read (after the check) (6.11, 8) | **Agreed** (RC charter, extended) | Lead's answer, BQ5; art |
| **TL-23** | ITEMS 5.3 (kept); RS 8.3 / 8.10; **ENM 14.6 category list (agreed; adds a category)**; **ENM 14.6 Yield drop bullet and ENM 14.8 Cowed success ("keeps its notable item") (named after the check; a new outcome: the floor key is handed over)**; ENM 15.5; FL-18 | Keybearers; automatic keys; `KeyReachable` (bound in `trapsResolve`); the interrogation category; the key on Yield and Cow (fallback: Cow does not yield it); Lockside Listen (6.11) | **Agreed** (ENM 14.6; 14.8 as its status stands) | Client, with this document; enemies and FL owners apply |
| TL-24 | ITEMS 5.3 counter list, 18.1, 19.1, category enum (lead's calls); CN 9 / 10 | Wax Key (run-scoped counter, cap 2, depth price, pegs K1 and K2) (6.11) | — | Lead's answer, BQ8; items and consumables owners apply |
| **TL-25** | **ITEMS 10.3 basic vendor list (agreed)**; ITEMS 18.1 (`vendor.waxKeyStock`); CMP 7.3 G2 gap card (the coverage strip line) | The basic vendor stocks 1 Wax Key only when the party has no Mechanics-trained member, read from the gap card's snapshot and printed on its strip (6.11) | **Agreed** (ITEMS 10.3); **Q3** | **Client (agreed 2026-11-21, Q3)**, with this document; items and companions owners apply |
| TL-26 | ITEMS 2 and 17 (kept); CN 16 oil uses | No lockpicks; Oil the Works; the Pick-counter fallback (6.11) | — | Lead's answer, BQ6 (contested), confirmed by the client on 2026-11-21; M3 for Oil the Works |
| TL-27 | HE-3/4/7 (**agreed**; kept); ENM 15.6; ITEMS 18.1 | Container kinds; contents never destroyed (6.13) | — (kept) | Lead's call; segment 7 |
| **TL-28** | ENM 17.4; ENM lurker row and guile / Stealth (container placements); **RS 7.4 RC1 / RC2 (agreed; reading)**; RS 7.4 RC8 (read, not changed: "feels teeth" is a callout, not a bark); RS 8.7 / 8.10; **DH 10.4 (agreed; extended to openers)**; V7; STAT 7.1 and ENM 10.6 (read, not changed: Lore shows the type only) | The boxed lurker: presentation, marking timing, the unmarked-attack row, open routing for all containers (after the check) (6.13, 6.14) | **Agreed** (RC, DH 10.4); **Q2** | **Client (agreed 2026-11-21, Q2)**, with this document; enemies and RS owners, art; M3 |
| TL-29 | HZ 6.10 / HZ-21 cap; HZ HF11 (one clock per room; read); FL Strongroom row (lead's call); DH 10.4 Dare bullet (kept); DH 18 reports | Wager rooms; after the check: the TP3 exception and `WagerNonSkillLine`, the one-rule Disturbance track, `gradeAtLift`, distinct caged notables, the Hunch override, greed plates on pips (6.15) | — | Lead's answer, BQ9; HZ and FL owners; M3 |
| TL-30 | ENM 12.3 / 12.5 (reading); ENM 14.3 (a new morale trigger, added after the check) | Alarm traps one-shot; alarm and Crier arrivals Alerted, not Surprisable (Crier added after the check); the Rattle Wire's "rudely woken" morale rider (fallback: noise 10 only) (6.18, 6.11, 7.10) | — | Request; enemies owner; the rider M2 or M3 as the owner rules |
| **TL-31** | HZ 6.1; **HZ 6.4 and the HZ Void gates (agreed, HZ Q2)**: V10 `SnagProven` and HZ's Void-room quotas (a runtime Void inside a proven Void room, counted in the quotas: flagged); C 10.2 edge save; ENM 14.3 and ENM 16 (`voidFall`, read as written); RT-20; RS 8.3; **RS 7.6 (agreed)** | Trapdoor Voids from F2 only in V10-proven Void rooms of themes with a Void skin; no fall at the spring (units placed on the nearest square); F1 and elsewhere reclose; debris (M3); portcullis damage-only in M2, Bar in M3 (6.3, 7; revised after the check) | **Agreed** (HZ Q2 gates: a runtime Void flagged; RS 7.6: read) | Lead's call, with this document; HZ owner applies |
| TL-32 | RS 8.x Laws; ITEMS 2; **ABL 7.2 Smash row** (new lead's call: the Iced padlock) | Paper burns; High-Striker; Ticket lock; Fed sphincter; Riddle locks; Smash on the Iced padlock (7) | — | RS owner; ABL owner applies the Smash note |
| **TL-33** | FL-8; FL 9.2; **FL-17 (agreed, FL Q3; the Lore widening goes to the client at M3)**; ABL 15.1 | Door verbs; stuck doors rejected; Swollen door; no relock; the Drop-bar guard; Hairline variants (6.12) | **Agreed** (FL Q3) | Lead's call; FL owner; client at M3 |
| TL-34 | STAT 6 passives; RS 9.1 | Readings as thresholds (6.12) | — | Request; segment 8; M3 |
| TL-35 | CN 16.4 tier-2 reserves; CN 16.4 `classTags` (the closed coverage-or-cut audit, ARC-24, HR-24); ARC 8.10; HR 8.8 (the last three named after the check) | Unbolting with a `classTags` entry; Unweaving against Runed (9.2) | — | Request; consumables, Arcanist and Herald owners; M3 |
| TL-36 | STAT 7.2 builders' slot | Second Nature (a proposal) (9.2) | — | Builders |
| **TL-37** | **SH 14 (agreed, SH Q2; applied)**; CL 13 rows | DEP-S1 fallback; records M2; reports M2, gates M3; "places traps" rejected (6.16) | **Agreed** (SH Q2, applied); told plainly | Client, with this document |
| TL-38 | FL 6.10 and **FL-16** (`ExploreSeconds` ≤ 3.5 min, `ExploreClicks` ≤ 40 at party 4 on F4–5: raised to 3.8 min and 45 for segment-5 content only, named after the check); CMP 7.4 time model; HZ-27 envelope; C 5.5 | Segment-5 allowance as a new budget line on top of FL's commitment; mandatory levers; the binding cut order (6.21) | — (told plainly) | Lead's call; FL and CMP owners |
| TL-39 | C 11.6; RS 8.0 (RT-16) | Ruling request: the theme's 6, otherwise damage only (7) | — (request) | Conditions owner, before M2 rosters freeze |
| TL-40 | RT 6.11; HZ 6.1; RS 13 | Arena devices are segment-9 instances in TrapRecord format (7.8) | — | Lead's call |
| TL-41 | ENM 12.3 hearing (a scent modifier); HZ 6.10 Plunge and the in-combat Swallow rejection; FL Chute; **ABL 8, the missing teleport rule (CN 16.4, a dependency named after the check)** | Scent and room triggers; relocation traps (in combat, same room only) and the Mirror Glyph, once a teleport rule exists; fallback Prone in place and the Mirror Glyph cut (6.3, 7) | — (request) | Enemies, HZ and ability model owners; M3 |
| TL-42 | Weapons 9.3 "There is no improvised throw of a non-thrown weapon" (an exception, not a change to the rule) | Trigger Shot alone may throw a held weapon or item as an improvised throw: range 4, no damage, the item lands on the target square (6.7) | — (request) | Lead's call, after the check; weapons owner; fallback: Trigger Shot needs a thrown weapon or a launcher; M2 |
| TL-43 | RS 8.4 Kennel trap flavour "horn tripwire" (re-told as the tin-can tripwire); RS 8.x theme dressing and RT 8.1 kit silhouettes (the per-theme reserved tell and device-body families); RS 7.4 RC7 (one alarm cue, no longer pitched per theme; read) | The tin-can tripwire; reserved tell silhouettes; device bodies under RT's RC2 piece bans; trap tells never idle and never reuse HZ prop families (6.3, 7, 8) | — (request) | Lead's call, after the check; RS owner and art; M2 |

Waves: TL-1 to TL-5 and TL-21 in M1 (traps' parts of TL-4 and TL-5 in
M2); TL-6 to TL-16, TL-18 to TL-20, TL-22 (Crier, Barb, Tattle), TL-23
to TL-27, TL-30, TL-31 (Voids in proven Void rooms), TL-32 (M2 parts),
TL-33 (close verb), TL-36, TL-38, TL-39, TL-42 and TL-43 in M2; TL-17, TL-22 (Runed, Hour),
TL-28, TL-29, TL-34, TL-35 and TL-41 in M3; TL-37 at the M2 gate; TL-40
with segment 9.

---

## 18. Depends on systems not yet designed

The owed rows are the single list in 17; by owner:

- **The enemies owner:** TL-16 (11.2, 11.3 rows, `NoCheapKill`
  extension, the no-path reroute), TL-17 (M3), TL-23 (14.6 category;
  the key on Yield, 14.6, and on Cow, 14.8), TL-28 (the lurker row for
  container placements), TL-30 (12.3, 12.5; Crier arrivals; the "rudely
  woken" Rattle Wire rider, 14.3), TL-31 (`voidFall` read as written),
  TL-41 (scent).
- **The DH owner:** TL-8 (10.4 trap row and "Trap provenance" bullet,
  DH-32's enum, 17.5, 17.6), TL-12 (17.6, 17.7), TL-28 (10.4 opener
  extension), TL-29 (18 reports).
- **The stat model owner:** TL-6 (the agreed STAT 6 trap-sense row).
- **The ability model owner:** TL-3 (14.4 narrowing), TL-4, TL-5, TL-7
  (Probe row, the detection interrupt), TL-13, TL-15, TL-21 (new rows),
  TL-32 (the Smash note), TL-41 (the teleport rule, ABL 8).
- **The weapons owner:** TL-42 (the Trigger Shot improvised throw).
- **The conditions owner:** TL-18 (10.4 wording), TL-19 (11.3 on F1),
  TL-39 (the RT-16 ruling).
- **The Shade owner:** TL-5 (Sure Hands), TL-6 (Trap Eye re-scope),
  TL-13, TL-15 (SH 8.2 re-arm text), TL-37 (the parity rerun before M3).
- **The items owner:** TL-24, TL-25 (10.3 and 18.1), TL-26.
- **The companions owner:** TL-25 (the gap card's coverage-strip line),
  TL-38 (7.4 rows).
- **The consumables owner:** TL-24 (CN 9 / 10), TL-26 (oil), TL-35
  (Unbolting and its `classTags` entry).
- **The Arcanist and Herald owners:** TL-35 (ARC 8.10, HR 8.8).
- **The RS owner:** TL-10, TL-22 (RC charter), TL-28 (RS 8.7, 8.10),
  TL-31, TL-32, TL-40, TL-43 (the tin-can tripwire, reserved
  silhouettes, the single alarm cue).
- **The floor layout owner:** TL-2, TL-9, TL-20 (the FL 13 socket and
  the FL 7.2 Gaol Circuit answer), TL-21, TL-23 (FL-18), TL-29
  (Strongroom), TL-33, TL-38 (FL-16).
- **The room terrain owner:** TL-14, TL-18, TL-40.
- **The hazards owner:** TL-18 (HZ-7, 6.11 item 2), TL-29 (6.10 cap),
  TL-31 (HZ 6.4 runtime Voids in proven rooms), TL-41.
- **Dungeon generation segments 6–12:** every hook in 13.
- **The simulator:** the arms of 11.5 and the reports of 11.4.
- **The art team:** the art budget, RC5+ rows, pattern registry and
  timbre table of 8; TL-43.
- **Naming pass:** every placeholder name here.

---

## 19. Open items for the client

None. Approved by the client on 2026-11-21.

The client approved this document as a whole on 2026-11-21, including
the lead's answers to the nine brief questions BQ1–BQ9 (including the
contested BQ6, "no consumable lockpicks"), every lead's call (the lead's
calls of the answered proposal, the new lead's calls in 19.1 and the
lead's calls after the check in 19.1), the told-plainly items (including
"Told plainly after the check" in 19.1) and the narrowings recorded in
17, and its approval covers the flagged rows of 17. The three questions
it raised were answered on 2026-11-21 **(agreed)**; the client also said
everything else in the proposal stands as written. The future items in
19.2, which come back only if a report, an owner's refusal, a wave or a
later design triggers them, and the naming pass remain future items.

### 19.1 This document

**The three answers (agreed, 2026-11-21), each the recommended option:**

- **Q1, Warned or floored for traps:** option A. A shown Hunch zone
  counts as a warning. Out of combat, or when pushed in, it stays
  floored; walking in by choice in combat, the preview shows the floor's
  worst trap with the lethal tag.
  It narrows agreed DH 10.4 / DH Q6 (6.5; TL-8). Rejected: B, Hunch
  zones fully floored; C, no Hunch zones.
- **Q2, the mimic route:** option A. The boxed lurker: each theme's own
  lurker in container form (the Silk's Margin-Crawler drawer, the Gut's
  Gutworm strongbox) in M3, more themes as they gain lurkers, at most 1
  per floor on F2–5; no roster exception (6.14; TL-28). Rejected: B, a
  cross-theme keyhole mimic.
- **Q3, the Wax Key at the basic vendor:** option A. The basic vendor
  stocks 1 Wax Key only when the party has no Mechanics-trained member,
  merged with the companions gap card. It changes the agreed ITEMS 10.3
  list (6.11; TL-25). Rejected: B, loot and camp notables only.

**The lead's answers to the brief (accepted by the client by proceeding;
confirmed by the client on 2026-11-21, with this document):**
- **BQ1:** a failed disarm springs the trap only on a Botch (fail by
  5+); a Jam (fail by 1–4) is noise 4 and closes hand work for the floor
  (0.1, 6.6).
- **BQ2:** a spring is the trap's full effect with its normal save; a
  Botch never fires a hard part and lands by `botchHits` (0.1, 6.6).
- **BQ3:** stepping on a known trap is always legal, previewed and
  confirmed once, with advantage on the save (Bear it); walk-round is
  the default (0.1, 6.7).
- **BQ4:** natives path around triggers and stand in device areas;
  mindless types blunder into non-lethal traps; Once Bitten, the opener
  rule and Revealed-only scoring close the exploits (0.1, 6.8).
- **BQ5:** consequence locks (Crier, Barb, Tattle in M2; Runed, Hour in
  M3), each with a glyph and a rule card (0.1, 6.11).
- **BQ6 (contested by the lead):** no consumable lockpicks; the
  consumable fantasy goes to the Wax Key and Oil the Works. **The client
  confirmed it on 2026-11-21, with this document**; if the client ever
  reopens it, the fallback is a size-0 counter spent only on a Botch
  (19.2).
- **BQ7:** any touch wakes a mimic, adjacency never does; a trained pick
  "feels teeth" safely; the route is Q2's boxed lurker (0.1, 6.14).
- **BQ8:** the Wax Key, a run-scoped counter, cap 2, out of combat,
  never on the Bossgate, Grilles, secrets, Riddle locks or Wager cages
  (0.1, 6.11).
- **BQ9:** Wager rooms, M3, sharing the Dare slot, with the risk running
  through combat (0.1, 6.15).

**Told plainly; confirmed by the client on 2026-11-21:**
- **Take time makes BQ1 bite mostly on Hard devices, in combat, on
  consequence locks and in Wager rooms** (0.1, 6.6).
- **Bear it is not the universal answer for parties without Mechanics;**
  walk-round is (−5 to −7.6 pp of floor survival in the toy) (0.1, 10).
- **"Notices" means Intelligence:** Perception finds lurkers, boxed
  lurkers and the wide berth, not traps; Trap Eye needs a re-scope (0.1,
  6.4).
- **"Risking the whole party" runs through combat,** because traps out
  of combat cannot kill under DH 10.4 (0.1, 6.15).
- **The DEP-S1 fallback applies:** the Swordsworn ships in M2, the
  Trapwright is the first Shade unlock in M3 (0.1, 6.16).
- **Splinter reads "Portals never break" as "the Portal stands, the
  panel breaks"** (6.11; option B ready).
- **Companions do as much Mechanics work as the Shade and Arcanist
  combined** in the toy; the Shade's trap identity is access and timing
  (6.17).
- **TL-3 narrows settled ABL 14.4** to keep decided main 6.3 (6.22).
- **F1 pushes stop before trigger squares,** narrowing C 11.3 and SH-15
  on F1 (6.19).
- **The clock allowance is a new budget line,** not a CMP 7.4 lever, and
  the F4–5 gate passes only narrowly (6.21, risk 1).
- **The Q3 vendor key is not in the toy;** its loot and use effects are
  owed to the M2 simulator (10).

**New lead's calls made while writing this document** (beyond the
answered proposal; confirmed by the client on 2026-11-21, with this
document):
1. **Brief questions are labelled BQ1–BQ9** to keep them apart from this
   document's Q1–Q3 (front matter).
2. **The Supper's Wager room is renamed the Top Table** (the proposal's
   "the Head of the Table" is the Supper boss, RS 8.9, and HZ 6.17's
   final course) (7.8, A.5).
3. **"The floor's worst trap" in Q1's preview** is the highest maximum,
   after the unit's weakness, among the floor's traps not yet Revealed,
   shown as "up to X" with the lethal tag (red when X would take the
   unit to 0); it is a floor-level figure that reveals nothing about the
   zone (6.5, 6.23).
4. **The Q3 merge rules:** the stock reads the gap card's "Mechanics
   met" test on its camp-entry snapshot and draws nothing; the gap
   card's coverage strip prints the stocked key; limited stock of 1
   (`vendor.waxKeyStock`) at that camp's depth price, `OutOfStock` and
   `WaxKeysFull` greying, destroyed on leaving camp; hiring a Mechanics
   companion at that Muster does not withdraw it; the notables slot is
   separate (6.11, 11.1).
5. **Q2's "more themes as they gain lurkers"** happens only through a
   theme's own roster process, never by a roster exception; recorded as
   a Later row (6.14, 12).
6. **The universal re-arm limit replaces SH 8.2's "the universal action
   route stays unlimited"**, named in TL-15; Jury-Rig keeps its own
   once-per-trap-per-floor counter, so a Trapwright re-arms one trap at
   most twice per floor (6.6).
7. **The Wake's Viewing Door is the one F1 Tattle,** and a floor-1 floor
   holds at most 1 consequence lock after room 2 (the Gaol's Crier or
   the Wake's Viewing Door) (7.4, 6.19, 6.20).
8. **The Iced padlock's Smash** is RT's Smash verb extended to that
   padlock: 1 tick out of combat, replaces one attack in combat, one
   `checks` roll, opens like a force with no Splinter (7.8; TL-32).
9. **The Flag column marks readings of agreed text** (TL-1, TL-4, TL-5,
   TL-10, TL-16, TL-21, TL-22, TL-31), not only changes (17).
10. **`WaxKeyUse` is split by channel,** and the no-Mechanics simulator
    arm runs with the vendor key on and off (11.4, 11.5).
11. **The vendor key adds no dungeon clicks** to the segment-5 clock
    line (6.21).
12. **Risk 14:** the vendor key could crowd out the Mechanics hire;
    watched by channel (15).
13. **Document shape:** the proposal's sections are renumbered to the HZ
    template (its 4.x rules are 6.x here), adding Intent, Goals, Terms,
    the binding lessons, Left to the builders and Depends on.
14. **A Hunch zone is also floored in the round it first appears**
    (6.5 (c)). It is not part of the client's Q1 answer, which floors a
    shown zone only out of combat or when pushed in; it is a fairness
    patch for a zone that appears mid-round (6.5; TL-8).

**Told plainly after the check** (the design check that followed the
client's Q1–Q3 answers; confirmed by the client on 2026-11-21, with
this document):
- **Lore does not say whether a boxed lurker is on the floor.** The
  proposal's "a floor without one carries no paranoia" is withdrawn:
  the agreed Lore reveal shows roster types only, and making it show
  placements would change agreed STAT 7.1 and ENM 10.6. Perception
  marking, the tell, Pick routing and the 20% cap carry the fairness
  (0.1, 6.14).
- **The DEP-S1 fallback rests on the client's "keep it for later"
  default at the M2 planning gate**, not on "the density check is not
  in M2": TP12 is an M2 gate that overlaps SH 8.2(a)'s trap-on-path
  minimum, but it is not the SH-16 check (0.1, 6.16).
- **TL-6 changes agreed text.** The STAT 6 trap-sense row is agreed
  ("All rows (agreed)"); the proposal had marked it a lead's call. It
  is now flagged, and the client's approval of 2026-11-21 covers it (17).
- **The Gaol's F1 Circuit takes no tripwire.** FL 7.2's request for
  Circuit tripwires is met only by Bypass alarms on F1 (6.19; TL-20).
- **The clock line sits on top of FL's commitment:** F4–5 at party 4
  may reach 3.8 min and 45 clicks, with a binding cut order on a miss
  (6.21; TL-38).
- **`TakeTimeShare` is now a 50–75% band, not an 80% trigger,** and
  from F2 one out-of-combat object per floor refuses Take time: the toy
  showed trained parties would otherwise Take time on nearly everything
  (6.6, 11.4).
- **Trapdoor Voids are rarer than the proposal implied:** only in
  proven Void rooms of themes with a Void skin (the Choir and the Silk
  in M2), and a spring never drops anyone into one. The Carnival's
  trapdoors reclose in M2 (6.3, 7.7).
- **The M3 relocation traps and the Mirror Glyph wait for a teleport
  rule** the ability model does not yet have (ABL 8); without it they
  stay Prone in place and the Mirror Glyph is cut (6.3, TL-41).
- **The Trigger Shot improvised throw needs the weapons owner's
  exception** to weapons 9.3; without it a party with no thrown weapon
  or launcher relies on walk-round, Pole it and the hand-trigger
  (6.7; TL-42).

**Lead's calls made after the check** (each is marked "lead's call,
after the check" where it falls; confirmed by the client on 2026-11-21,
with this document;
none overrides a client answer):
1. **Two generation passes:** `traps` (geometry and kinds) and
   `trapsResolve` (after segment 6), with a named failure action per
   rule, retry of the failing pass only, and a terminal fallback to the
   segment-5 minimum with a `V11Fallback` count (6.20, 6.22, 11).
2. **The floor follows each trap's own grade**, and the snapshot
   becomes `detectionAtFire {hidden|hunchFloored|hunchWarned|revealed}`
   (6.5; TL-8). A chosen in-combat Hunch entry is self-caused (6.5,
   6.10).
3. **Detection runs per square and interrupts movement;** a grade
   changed during an action counts as undetected for it (6.4).
4. **`attemptIndex` per check kind,** Squeeze and Plank per traversal,
   and saves keyed by `springIndex` with a `die` (6.12, 6.22).
5. **Band maximums are after weakness;** weakness-typed bands use a
   pre-weakness maximum ≤ floor(`CapRound` / 1.5); the Responsory is
   re-derived to 6 (6.10, 7.5).
6. **Trapdoors never drop a unit into a fall;** Voids open only in
   proven Void rooms with a Void skin and count toward HZ's quotas
   (6.3; TL-31).
7. **The `botchHits` resolution table** and `botchOrigin` (6.6).
8. **The Force row and force resolution:** failures never close
   forcing; a Door Botch from F2 Splinters and opens; a container Botch
   is an ordinary failure; the third try opens with no roll and still
   fires per-try consequences (6.6, 6.11). "Take time force" is deleted.
9. **The host-catch order:** keys and Wax Keys never spring a catch;
   the lock resolves first; a Botch fires the lock's consequence, then
   the catch (6.13).
10. **Boxed-lurker presentation:** unlocked or a real Plain padlock,
    drawn at generation; the Wax Key judged on the presented state;
    open routing for every container; marking timing; the
    unmarked-attack row; "feels teeth" as a callout (6.13, 6.14).
11. **Weighted plates spring only on the square a piece ends on;**
    collision, then spring, through EnvRound (6.7).
12. **Probe from inside or beside a zone; the zone geometry; the Hunch
    badge for catches; auto-Probe out of combat unless a patrol is
    due** (6.4).
13. **Trap-sense eligibility** (living, non-Downed roster units),
    alternatives per tag, monotonic grades (6.4).
14. **The opener rule's order and Once Bitten's trigger** (6.8).
15. **TrapState, LockRecord, container and WagerRecord fields** and the
    `TrapResolved` event; the `TrapHeroDeaths` and `UndetectedChipShare`
    definitions (11).
16. **Take time is channelled;** Easy Plain auto-pick only with no
    patrol due (6.6).
17. **Step over is the default on a known wire;** Bear it on a wire is
    an explicit choice (6.7).
18. **Out-of-combat Trigger Shot is a one-try object check on
    `checks`;** the improvised throw (TL-42); Pole it reaches catch
    hosts and costs 1 tick out of combat (6.7).
19. **Wager rooms:** every trap raised to at least Hunch; the TP3
    exception with `WagerNonSkillLine`; one Disturbance rule (one pip
    per event, per Take time use); `gradeAtLift`; 3 distinct caged
    notables; greed plates arm on the next pip; Second Nature not
    offered inside (6.15, 9.2).
20. **From F2, one `noTakeTime` object per floor;** House Keys on
    Strings tagged; `TakeTimeShare` as a band (6.6, 6.20, 7.9, 11.4).
21. **Crier arrivals are Alerted and not Surprisable;** a spent Crier
    stays barred from Take time (6.11, 6.18).
22. **F1 fixes:** snares and catches ≤ 3, one off-path device ≤ 4; the
    lesson trap exempt from the out-of-combat count and TP5; the F1
    Keybearer cell ≤ 1 (6.19, 6.20).
23. **Tattle on Portal hosts only;** the wide-berth trigger and cue; the
    FL 13 Door-catch socket (6.4, 6.11, 6.20).
24. **The key on Yield and Cow is asked of the enemies owner** with a
    fallback (6.11; TL-23); the Rattle Wire rider joins TL-30.
25. **Readability:** the RC5+ table, pattern registry, gates, timbre
    table and art budget; tells render from Revealed; reserved tell
    silhouettes; no idle motion in tells; device bodies under RT's
    piece bans; the redrawn lock glyphs and padlock base; the Hunch
    zone's dotted rim; the cracked-heart lethal tag; the Splinter state;
    the Disturbance meter as the room's one clock (6.3, 6.11, 6.23, 8).
26. **Re-tells and re-bodies:** the tin-can tripwire (TL-43); the
    Sluice hatch; the hornless Gramophone; the Blade Wheel; the static
    tells of the Jack-in-the-Box, the Lip Catch and Hand Under the Bed;
    the Paper-Cut Tray; the Gutworm's hasp-free strongbox; the Acid
    Gland's valve-knot; the Clock-Weight VFX and clock-faced doors cut;
    the Top Table's course order cut (7).
27. **Renames:** Crepe Veil, Closed-Stack cage, Shush Glyph, Darning
    Volley, Thurible Wire, Paper-Cut Tray, Blade Wheel; "acid geyser →
    Acid Gland" recorded (A.5).
28. **The clock line on top of FL-16, with a binding cut order** (6.21;
    TL-38).
29. **Unbolting needs a `classTags` entry and the Arcanist and Herald
    owners** (9.2; TL-35); relocation and the Mirror Glyph depend on
    ABL 8 (TL-41).

### 19.2 Future items triggered by reports, owners or later designs

These come back to the client only if a report, an owner's refusal or a
later design triggers them:
- **At M3:** should a Lore-trained member also reveal Hairline tells in
  the Silk and Choir (widens agreed FL Q3)? Recommended: yes, M3.
- **Drop-bar** (already with the client from FL). Recommended: M3 with
  the anti-rest guard (6.12).
- **If `RouteChoiceMix` shows the hand-trigger is never used:** allow it
  to replace one attack (a settled ABL row; the Shade owner's parity
  rerun first).
- **If the client reopens BQ6 (confirmed on 2026-11-21):** the size-0 "Picks" counter
  spent only on a Botch (TL-26).
- **If the enemies owner refuses TL-16:** rigged share × party-hit ≤
  0.03 (NF-e).
- **If the ABL owner refuses TL-3:** every object check becomes a
  threshold.
- **If the Splinter reading is refused:** option B (noise 10, the Door
  still closes).
- **If RT-16 rules damage-only:** every trap condition drops (TL-39).
- **The portcullis Bar (M3),** flagged against RS 8.3 and RS 7.6, if
  adopted.
- **Boxed lurkers for themes that gain a lurker** (Q2; Later).
- **If the ability model defines no teleport rule:** the relocation
  traps stay Prone in place and the Mirror Glyph is cut (TL-41).
- **If the weapons owner refuses TL-42:** Trigger Shot needs a thrown
  weapon or a launcher.
- **If the enemies owner refuses the key on Cow (TL-23):** Cow does not
  yield the key.
- **If the enemies owner refuses the "rudely woken" rider (TL-30):** the
  Rattle Wire is noise 10 only.
- **If `TakeTimeShare` misses its 50–75% band:** more `noTakeTime` tags
  per floor (risk 6).
- **If `V11Fallback` is non-zero in the simulator:** the failing rules
  are retuned before M2 content locks.
- **The naming pass.**

---

## Appendix A: how the critique findings were resolved

### A.1 Fun critique

| # | Finding | Resolution |
|---|---|---|
| KF1 | The device lure is a dominant, repeatable opener | Once Bitten; opener rule; re-arm once per floor and not while a hit group is Alerted; `OpeningSpringRate` report (TL-15, TL-16) |
| KF2 | Take time plus auto-routing make BQ1/BQ5 ceremony | Take time not on Hard or consequence locks; patrol forecast; Sure Hands cuts ticks; Easy Plain auto-pick (TL-5) |
| KF3 | Out-of-combat traps are detour chores | `TrapHasStake` (TP5) |
| KF4 | Repeatable alarm lures | Alarm traps one-shot; arrivals Alerted and not Surprisable; Crier spent once sounded (TL-30, TL-22) |
| KF5 | Wager rooms binary; Take time ban arbitrary | Take time costs a pip per use; graded cages; visible non-skill line, proven by `WagerNonSkillLine` after the check; containment ≥ 9 (TL-29) |
| KF6 | Mimics delight nobody; tell unresolved | `containerForm`; container-scale tell; feels teeth still grips; fight drops contents plus carried item; Feed (TL-28) **(agreed, Q2)** |
| KF7 | "k / n" invites sweeping | Total dropped; open Hunch zones shown (TL-9) |
| KF8 | Shade has little M2 trap fun | Sure Hands Take-time cut; stealth to the mechanism as opener; Lockside Listen |
| KF9 | Probe in the auto-walk stop removes the decision | Three-way stop (6.4); after the check, out of combat it auto-Probes unless a patrol is due, so the stop appears only where it is a decision |
| KF10 | Wax Key hoarding | Contents hint; consequence locks cannot be Taken-time; depth pricing |
| KF11 | Bear-it readability | HP-after preview; idle glance tell |

### A.2 Balance critique

| # | Finding | Resolution |
|---|---|---|
| KB1 | Hunch-as-undetected rewards not detecting | Warned or floored (TL-8) **(agreed, Q1)** |
| KB2 | EnvRound cap laundering | Self-caused damage outside the cap (TL-18) |
| KB3 | Farming unaware groups | Opener rule; Once Bitten; re-arm once per floor (TL-15, TL-16) |
| KB4 | Hidden noise traps start lethal fights | `NoiseTrapVisible` (TP8); Shush Glyph (then the Silence Glyph) made a known room rule |
| KB5 | Fight-end Jams give a second try | Universal Jams to floor end (TL-4) |
| KB6 | Wager XP faucet, retry scouting, joiners | Guard from the budget; pips persist; no retry after a wake; guard takes the joiner slot |
| KB7 | Bear it oversold | Walk-round default; HP preview and warning; BQ3 wording |
| KB8 | F1 snare into combat; fight-adjacent conflict | Conditions end at combat start; no F1 trap within 6 of a fight host or on a Circuit |
| KB9 | Out-of-combat saves on the combat stream | `checks` stream (TL-3) |
| KB10 | Wax Key depth arbitrage, overflow | Depth pricing; stays in container; refused on plain; vendor stock 1 per camp (Q3) |
| KB11 | Conflicting bounds; F2 hard parts at party 2 | One bound set (TL-18); F2 hard parts null in M2 |
| KB12 | One act, two traps | One spring per act (TL-14) |
| KB13 | No-path pockets; portcullis stranding | Reroute behaviour request; portcullis damage-only in M2 |
| KB14 | Take time erases the Shade edge | 8 ticks; Sure Hands cut; Second Nature reaches Hard |
| KB15 | Clock may fail the first gate | Mandatory levers; fallback switch |
| KB16 | Validator ignores Circuits; loot targets disagree | TP10 includes Circuit rooms; one loot target (6.17) |
| KB17 | Shared seeds known in advance | Builders' option: salt with run id |
| KB18 | Count line never reaches k = n | Total dropped |
| KB19 | Steal-the-keyring is a fight trigger | Previewed on the lock card |
| KB20 | Oil the Works redundant | Kept M3 optional: matters on rolled consequence and Hard attempts |

### A.3 Consistency critique

| # | Finding | Resolution |
|---|---|---|
| KK1 | Keyhole mimic breaks RE9/RC3 | `containerForm` (TL-28) **(agreed, Q2)**; the catalogue's TL-44 dropped |
| KK2 | TL id collisions | One register TL-1..41; TL-42 and TL-43 added after the check |
| KK3 | Catch and Barb saves on the combat stream | `checks` stream out of combat |
| KK4 | Hand-trigger "one attack" changes a settled row | Kept an Action; future item 19.2 |
| KK5 | Revealed-only scoring is a change | Flagged TL-16 with fallback |
| KK6 | F1 traps against RT-20/TR-29 | TL-19 as written; the catalogue's "TR-29 amended" deleted |
| KK7 | Name collisions | Bell→Crier, Sleeper→Tattle, Slip→Jam, Cunning→Hard, Burst→Splinter, Seal→Riddle lock, Passing Bell→Belfry Wire, Sure Hands→Second Nature, Needle→Barb, Swallow→Lip Catch, Waking Bell→Rattle Wire, Music Box→Gramophone, Empty Chair→Cold Seat, Lidmaw/Lidmouth unused, Rigged room→Wager room; and, while writing, Head of the Table→Top Table; after the check, acid geyser→Acid Gland, Mourners' Veil→Crepe Veil, Overdue cage→Closed-Stack cage, Silence Glyph→Shush Glyph, Pincushion Volley→Darning Volley, Censer Tripwire→Thurible Wire, Paper-Cut Drawer→Paper-Cut Tray, Spinning Target→Blade Wheel, horn tripwire→tin-can tripwire (A.5) |
| KK8 | Three spring bounds | One set (TL-18); band P and 40% deleted |
| KK9 | Ladder variants; Narrow row | One ladder; Plank and Squeeze stay 12 |
| KK10 | Force retry rules mixed | One rule; Splinter flagged; hearing reported |
| KK11 | Party-caused spring against DH | Floored for the party when it starts combat (TL-8) |
| KK12 | Take time on Squeeze and Plank | Excluded |
| KK13 | Lore-Hairline widening | Client item at M3 |
| KK14 | Wager on F1; guard budget | No F1 Wager; guard from the budget |
| KK15 | Heart Weary outside its 6 | Removed; catalogue conditions checked |
| KK16 | Mimic guile change | Explicit container-placement threshold in the ENM lurker row (TL-28) |
| KK17 | Items rows status | Separate rows TL-24, 25, 26 with status |
| KK18 | Interrogation and Listen are list changes | Flagged in TL-23 |
| KK19 | Density tables differ | One table (6.20) |
| KK20 | Clock allowance as a lever | A new budget line (TL-38) |
| KK21 | Sure Hands and Trap Eye | Shade-owner rows TL-5, TL-6 |
| KK22 | Portcullis Bar against RS | Damage-only in M2 |
| KK23 | DEP-S1 parity reruns; "places traps" | M3 gate item; rejected |
| KK24 | M3 dependencies leaking | Hour, scent, relocation are M3 only |
| KK25 | M1 lesson trap as a hazard | Stated in the M1 row |

### A.4 Judges' grafts and toy fixes

Every graft listed by the three judges is adopted, with these
exceptions:
- the hand-trigger stays an Action (KK4);
- the keyhole mimic is rejected **(agreed, Q2)** (KK1);
- Wager "no Take time" becomes "Take time costs a pip" (KF5).

NF-a to NF-n are applied as in section 10.

### A.5 Names

Each new name was grep-checked case-insensitively as a whole word across
the 21 approved documents and has 0 hits: Jam (only as the Shade's own
word, same meaning), Botch, Hunch, Hunch zone, Probe, Take time, Bear
it, Pole it, Walk round, Push through, Step over, wide berth, Crier,
Barb lock, Tattle, Runed, Hour lock, Riddle lock, Splinter, Keybearer,
Lockside Listen, Wax Key, Unbolting, Oil the Works, Swollen door,
High-Striker, Wager room, Disturbance track, Once Bitten, Second Nature,
Margin rule, boxed lurker, Lesson trap, Belfry Wire, Rattle Wire, Cold
Seat, Lip Catch, Gramophone, Clock-Weight, Confession Box, Gin Jaw,
Scent Bladder, Deadfall, Closing Coffin, Lid Rack,
Drowning Grate, Responsory, Drowned Chandelier, Shelf-Pull, Stage
Trapdoor, Jack-in-the-Box,
Oil Bucket, Mirror Glyph, Taster's Plate, Acid Gland, Spore Bladder,
Lullaby Glyph, Hand Under the Bed, Rose Pane,
Collection Plate, Pew Rack, Rood Screen, Will-Box, Viewing
Door, Vestry door, Ring key, Ticket lock, Iced padlock,
Fed sphincter, web seal, Hymn, Index, Place-Setting and Story Riddle
locks, Loft of Responses, Restricted Section, Last Act, **Top Table**,
the Stomach, Night Nursery, Rose Window, Winch up, `containerForm`,
and, re-checked after the check (the proposal's names that the check
found colliding in context are replaced; see "Renamed after the
check"), Crepe Veil, Closed-Stack cage, Shush Glyph,
Darning Volley, Thurible Wire, Paper-Cut Tray, Blade Wheel, tin-can
tripwire, valve-knot, trap rim, trap link, cracked heart,
`trapsResolve`, `TrapResolved`, `detectionAtFire`, `noTakeTime`,
`HunchZoneExclusive`, `WagerNonSkillLine`, `WagerNoHidden`,
`WagerWalkRoundRate`, `ReservedTellSilhouettes`, `V11Fallback`,
`botchOrigin`, `gradeAtLift`, `greedPlates`, `tickAccumulator`,
`traversalIndex`, `springIndex`, `catchTrapId`, `forceOpenedBy`,
`markedByParty`, `seenByParty`, `hitGroupIds`,
`botchHits`, `TrapHasStake`, `NoiseTrapVisible`, `TrapNeverSeals`,
`LockNeverCritical`, `KeyReachable`, `HiddenTrapBound`,
`TrapLethalityBound`, `HunchTrue`, `HeNoLock`, `BypassClean`,
`TrapsLocks`, `trapDigest`, `waxKeys`, `rearmUsedThisFloor`,
`TrapRecord`, `TrapState`, `LockRecord`, `KeyRecord`, `WagerRecord`,
`GroupState`, `CheckResolved`, `TrapSprung`, `LockOpened`,
`ConsequenceFired`, `DoorSplintered`, `KeyUsed`, `WaxKeyRefused`, V11,
TL- (TL-1 to TL-43), BQ1–BQ9, CC1–CC8, TP1–TP12, KT1–KT26, NF-a, KF, KB, KK.

**Hits read in context and kept:**
- "Runed" and "Hour lock": 0 hits as names.
- "Jammed" and `jammedUntil`: STAT prose and SH 8.2 / 13.1, same
  meaning.
- "Iced": RS adjectives ("iced decanter"); kept for the Supper padlock,
  flagged to the naming pass.
- "Catch" as a family word: "Antechamber Catch-up" (RS) and the class
  ability Catch Breath are different words in context; kept, flagged to
  the naming pass.
- "Smash": RT's verb (ABL 7.2), reused, not renamed, for the Iced
  padlock.
- "Lore roster": RS's term for the Lore roster reveal (STAT 7.1, ENM
  10.6), used as written.
- "Sluice": the HZ 6.1 sprung twin ("the sluice one-shot Soak"), kept as
  the Choir trap; the Sluice Wheel Cord stays HZ's.
- **Renamed after the check (lead's call, after the check):** "acid
  geyser" (RS 8.10, HZ 6.1's sprung twin) → **Acid Gland**, a rename,
  not a use as written; "Mourners' Veil" (a Herald word) → Crepe Veil;
  "Overdue cage" and "Silence Glyph" (the Silk Mood's "Overdue" and
  "Silence Kept") → Closed-Stack cage and Shush Glyph; "Pincushion
  Volley" (RT's pincushion face) → Darning Volley; "Censer Tripwire"
  (HZ's censer-rope Cord in the same theme) → Thurible Wire; "Paper-Cut
  Drawer" → Paper-Cut Tray (moved off the drawer, 7.6); "Spinning
  Target" → Blade Wheel (re-bodied, 7.7); RS 8.4's "horn tripwire" →
  the tin-can tripwire (re-told, TL-43). "Hush" (5 hits) and "Hatpin"
  (2 hits) were avoided.
- Theme trap flavours already named in RS (the bell tripwire, the
  Appeals trapdoor, the Dumbwaiter Drop, the Icicle
  Chandelier, the Whispering Catalogue, the Ink-Pot Plate, House Keys on
  Strings) and keyholders already in RS rosters (the Turnkey, the
  Jailer, the Huntsman, the Undertaker, the Deacon, the Head Archivist,
  the Ringmaster, the Sommelier, the Landlord, the Tenant, the
  Mother-Shape): used as written.
- Trigger Shot, the hand-trigger, Trap Eye, Sure Hands, Unweaving,
  Margin-Crawler and Gutworm: existing names, used with their approved
  meanings.
- "Lift" and "Hold" were avoided as verb names: the portcullis uses
  "Winch up".

**Avoided because taken:**
- Sure Hands (SH 7.11); Slip (ABL "slip past"); Brace; Vault (SH NM-11);
  Gauntlet; Reliquary; Ward and Warded; Seal (ARC); Sleeper (ENM lair
  template); Cunning; Burst; Knell and Toll;
- Bell head words (beside the Herald's Passing Bell); Swallow (HZ);
  Music Box (HZ/RS); Empty Chair (RS Obsession); Rigged (Trapwright
  tagline); Strongroom and Dare room (taken, reused only by reference);
  the Head of the Table (the Supper boss, RS 8.9; HZ 6.17);
- "trap room" (Herald/DH report); Lockbreaker (CMP); Mimic (SRD name; RS
  V7 prefers original names); Patient; Lucid; Key alone (the Lucid Key).

---

## Appendix B: sources

- Checked: bg3.wiki (Baldur's Gate 3 traps, disarm kits, lockpicks);
  DCSS 0.23 release notes (trap removal and revealed traps); rephial.org
  Angband 4.1.0 notes (searching removed); Dized Gloomhaven rules
  (monsters and traps, forced movement); Archives of Nethys PF2e
  (critical failure on Disable a Device; complex hazards).
- From memory, treated as such: D&D 5E DMG and PF1 / Kingmaker trap
  rules; Pillars of Eternity; Darkest Dungeon; Dark Souls, Enter the
  Gungeon and The Binding of Isaac mimics; Spelunky; Legend of Grimrock;
  Hades; Brogue; Invisible Inc., Thief and Dishonored; Skyrim and
  Fallout lockpicking; Deus Ex; Into the Breach and XCOM; Wizardry and
  Etrian Odyssey; The Alexandrian's three-clue rule and OSR practice.
- Toy models: the proposal's numbers pass (detection grades, Margin-rule
  spring rates, Take time, force retries, Splinter, Wax Key pegs, mimic
  deaths, Wager wake rates, clock deltas). Every figure is owed to the
  M2 simulator (10, 11).
