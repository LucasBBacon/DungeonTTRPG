# M1-10 Rulings: Design (roadmap decisions D-17, D-18, D-19)

Date: 2026-11-27
Status: Approved, 2026-11-27. The client answered Q1 to Q5 (option A each time) and approved this record as a whole on 2026-11-27, including every lead call in 8.
Parents: the open decisions D-17, D-18 and D-19 that step M1-10 raised
(`docs/roadmap/README.md` on the M1-10 branch), the M1-10 plan
(`docs/superpowers/plans/2026-11-27-m1-10-turns-combat-and-death.md`),
the M1 scope rulings (`2026-11-24-m1-scope-rulings-design.md`) and the
approved designs they cite. Abbreviations as in the roadmap index.

Marker used in owning designs: **(agreed 2026-11-27, roadmap D-17 / D-18 / D-19)**.

How the rulings were made: each decision got a research brief. For each
brief, independent readers collected the design text and the M1-10 code,
a draft set out the options, and a critic tried to refute it against the
designs and the code. A cross-check then read the four briefs (D-17,
D-18, D-19(a), D-19(b)) against each other. Where the cross-check
corrected a brief, the correction stands here. One citation in the
cross-check and the answers is corrected too: the push rule is at TL
6.19, line 2083 on this branch (cited elsewhere as 2079), and the
Grappled-and-Prone stand is at CO 18.4, line 2026 (cited elsewhere as
2028). Comparisons with other games (D&D 5th edition, Baldur's Gate 3,
Solasta, XCOM, Divinity) and the SRD 5.2.1 wording on speed changes and
Dash were made from memory and not checked against those texts. The
client then answered five questions, Q1 to Q5, and chose option A each
time. The lead's own calls that fill gaps the answers leave are marked
**(lead's call)** and listed in 8; the client confirmed them all on
2026-11-27.

Where to look: sections 1 to 5, 7 and 8 cite designs by section. Line
numbers are in the change rows (6). Design line numbers refer to this
branch; roadmap, plan and code line numbers refer to the M1-10 branch.

Terms used throughout:
- **Later**: the list of features recorded in `m4-breadth.md` so nobody
  builds them early.
- **F1**: floor 1, the first floor of the dungeon.
- **Opener**: a hostile action taken out of combat that starts a fight
  (DH 10.4, DH 20.5). The unit that makes it is the **striker**.
- **Turn budget**: what a unit may still spend this turn: its action,
  bonus action, movement and reaction.
- **Use limits** ("pools"): limits that refill either at the start of
  each of the unit's turns, which makes them once per round (Practised
  Hand, for example), or once per fight.

---

## 0. Summary

| Decision | Question | Ruling | Built by |
|---|---|---|---|
| D-17 (Q1) | Who owns the test of temporary HP against the undetected-trap floor (the rule that an undetected trap cannot take a hero below HP 1)? | The deferral stands. A Later entry "Bolstered (temporary HP)" owns the full test. Three questions go to the DH owner first | The step that builds Bolstered (Later) |
| D-18 (Q2) | How a speed change partway through a turn affects movement left | Subtract, not rescale: movement left = max(0, current speed + granted movement − movement used), rechecked every step | Subtraction already built (M1-10). M1-13 adds per-square costs, M1-14 Dash, M1-20 Bar the Way |
| D-18 (Q3) | Standing when movement left is less than the stand cost | Standing costs the action instead | M1-13 |
| D-19(a) (Q4) | Is TL-19's "trap conditions on F1 end at combat start" M1 or M2? | M2. M1 ships no trap that can apply a condition. The M2 behaviour is recorded now | M1-30 (M1 part and a guard check); M2 (the condition end) |
| D-19(b) (Q5) | Initiative against an opener; does the opener cost the striker? | Combat start runs in full first. The opener then resolves before any turn and is charged to the striker's round-1 turn | M1-11, with steps from M1-12, M1-13 and M1-35 |

The combined combat-start order is in section 5. Every document edit is
a row in section 6.

---

## 1. D-17: Temporary HP and the undetected-trap floor (Q1)

### 1.1 Ruling

- **The floor.** An undetected trap cannot reduce a hero below HP 1
  (DH 10.4, DH 15.2).
- **The deferral stands.** Temporary HP exists only as Bolstered, which
  is Later (DH 14; DH 22; CO 18.9). Only class features and spells grant
  it, never items (DH 14). M1-10 keeps its reduced test (HP 5 +
  undetected trap 10 → HP 1, `DamageClamped{undetectedTrap}`, the save
  chain never reached).
- **A real owner.** The roadmap has no step that adds temporary HP, so
  D-17's owner "the step that adds temporary HP (Later)" points at
  nothing. A new entry in the Later list of `docs/roadmap/m4-breadth.md`,
  **"Bolstered (temporary HP)"**, owns D-17, as D-14 is owned by M2-23.
- **Its Done-when is the full expected result.** The result is already
  settled in DH 15.2 (confirmed by the client on 2026-11-17), DH 14 and
  DH 10.4. No new rule is made. The entry's Done-when reads:
  - HP 5, temporary HP 3, an undetected trap deals 10;
  - temporary HP absorbs 3, so the total after absorption is 7;
  - the floor clamps it: temporary HP 0, HP 1;
  - `DamageClamped{undetectedTrap}` is emitted;
  - the save chain is not reached.
- **Three questions for the DH owner, before Bolstered is built.** They
  do not matter in M1, which has no temporary HP.
  - **(a) Part order.** Temporary HP absorbs per part, in the order the
    content lists the parts (CO 9.3; DH 15.2). The DH 10.4 rule for a
    hit that mixes trap damage with other damage asks whether "the other
    parts alone would take the unit to 0". Together, the order of the
    parts can decide life or death.
    - Example: the hero has HP 5 and temporary HP 3. One hit has two
      parts: a sword part of 6 and an undetected-trap part of 4.
    - Sword listed first: temporary HP absorbs 3 of the sword. The sword
      now deals 3, which alone would not kill the hero, so the floor
      applies and the hero ends on HP 1.
    - Trap listed first: temporary HP absorbs 3 of the trap. The sword
      still deals 6, enough to kill alone, so the floor does not apply
      and the hero dies.
    - Question: should the result depend on the order the parts are
      listed?
  - **(b) Does the other-parts check count temporary HP?** Today the
    game compares the other parts against HP alone.
  - **(c) What does the combat log say?** Today the damage message shows
    the total before the floor is applied, so the D-17 test cannot yet
    say whether the log should read 10, 7, or "7, 3 absorbed". The DH
    owner rules this, and the test then checks the log as well as HP.
- **Stale labels are fixed.** "M2 or later" for temporary HP becomes
  "Later" in a code comment, the M1-09 plan and the M1-10 plan (every
  line is listed in MR-31 and MR-32). A code comment at the mixed-hit
  check points to questions (a) to (c), so whoever builds Bolstered sees
  them.
- **Determinism.** Absorption draws no dice, and every die is rolled
  before the damage function runs. Adding temporary HP later moves no
  random draw.

### 1.2 Rejected

- A placeholder test that is switched off: nothing exists yet to test,
  and the project has no switched-off tests so far.
- A bare temporary-HP value in M1-10: builds a Later feature early,
  reverses the 2026-11-27 owner ruling in the M1-10 plan, and changes
  M1 saves and goldens.

---

## 2. D-18: Speed changes partway through a turn (Q2, Q3)

### 2.1 The subtract rule

The game already works this way, and a test already checks it. This
ruling makes it final. The word "rescale" in the roadmap row and the
plan was wrong (see 7 item 1).

Movement is counted in points: most squares cost 1, crawling and Rough
cost 2. "Movement used" and "movement left" are always in points.

1. **Movement left = max(0, current speed + granted movement −
   movement used).**
   - Current speed is speed after every speed effect, worked out as CO
     6.7 says: flat changes first, then at most one halving, and speed 0
     overrides everything.
   - Granted movement is extra movement an effect adds this turn, such
     as Dash (2.2).
   - The game records movement used, never movement left.
2. **Rechecked before every step.** It is never worked out in advance
   for a whole path. A change up or down applies from the next step.
3. **Speed 0 stops movement.** While current speed is 0, movement left
   is 0 whatever movement has been granted (CO 6.7: speed 0 overrides
   everything). Granted movement is not lost: it counts again if speed
   returns in the same turn **(lead's call)**. If speed returns later in
   the same turn, the unit may move its speed (plus any granted
   movement) minus the movement it has already used. In M1 this happens
   only when a Grappled unit (speed 0, CO 18.4) escapes (M1-14).
4. **A move cut short by a reaction keeps its unspent movement** for a
   later move this turn (W 8). Subtraction gives this with no extra
   state.
5. **Movement used counts points, not squares.** Crawling and Rough
   cost 2 per square, never more than 2 (CO 18.4). Standing costs half
   the current speed, minimum 1 (CO 6.7). M1-13 replaces today's flat 1
   per step with this per-step cost.
6. **Forced and free moves never add to movement used.** Forced
   movement, a free stand (Escape's, CO 18.4) and any move a design
   marks free leave movement used unchanged.
7. **Terrain that ends a move.** The CO 11.2 rule that a unit "stops
   there and loses the rest of its movement" covers terrain only (CL
   Appendix A, W-2), and terrain is M2. M2 will fit it to this rule.
8. **The preview reads the same function.** The move preview and every
   reason shown for a refused move use the same movement-left function
   and the same per-step cost. No second formula exists.
9. **No reset at the start of each round.**
   - In combat, a unit's turn budget resets only at the start of its own
     turn and when a fight starts.
   - Out of combat, every tick resets every budget (AB 6.8), except the
     tick a declared opener's command would take (8 item 4). Combat end
     resets them too.
   - Outside its own turn, a unit uses only its reaction and its use
     limits.
   - A round reset would undo the opener's cost before the striker's
     turn comes up (4.1).
10. **Incapacitated is handled separately (lead's call).** It stops the
    unit moving on its own (CO 18.2), but it does not set speed to 0. So
    the stand cost and the reason shown for a refused move stay correct.

### 2.2 The shapes of Dash and Bar the Way

Each is built by the step that first needs it. The ruling fixes the
shape now so neither step invents its own way of tracking movement.

- **Dash (M1-14) adds granted movement** equal to the unit's current
  speed at the moment of the Dash (Q2). The number is fixed then and
  does not follow later speed changes. The two possible readings (fixed
  at the Dash, or Dash count × current speed) differ only when speed
  changes after a Dash in the same turn: in M1 only Strained (if a
  shield swap triggers it) or Grappled could do that; a halving needs
  Chilled (M2).
- **Bar the Way (M1-20) stops the mover for the rest of its turn** (a
  per-turn stopped marker, Q2).
  - Raising its speed afterwards does not undo the stop.
  - Movement granted after the stop, by Dash or Spurred, still counts
    (Q2). This is CL 8.3 as written: Bar the Way "sets the mover's
    remaining movement for this turn to 0. Its speed is unchanged ...
    Movement granted afterwards by Dash ... or by Spurred is added as
    normal" (also W 8). CL Appendix A, W-3, keeps it apart from a move
    cut short.
  - A stopped unit's movement left is the movement granted after the
    stop, minus the movement it uses after the stop **(lead's call)**.
  - It must pass the composite-lockout test (CL 14).
- **Why a stop marker and not "mark all movement used".** In M1 the two
  give the same result. From M2, a later speed raise would hand back
  movement the stop took away.
- **Versions.** Each new thing the game tracks per turn changes the save
  format, so the step that adds it raises the save version. M1-11
  raises it first (to 8, 5). M1-14 (granted movement) and M1-20 (the
  stopped marker) raise it again after that, unless steps share a raise.
  Each of those two also raises the rules version, since a command then
  does something different **(lead's call)**. Both new values are
  cleared when a fight starts (5, step 4) and follow the first-turn rule
  (5, step 9).

### 2.3 The stand gap (Q3)

- **Rule.** If a Prone unit's movement left is less than the stand cost
  (half its current speed, minimum 1), standing costs its action
  instead (Q3). When movement left covers the cost, the unit pays the
  cost in movement, as CO 6.7 says.
- **Standing by the action** spends no movement. If the action is
  already used, the unit cannot stand this turn **(lead's call)**.
- **Basis.** It extends two rules CO already has: "At speed 0, standing
  costs the action" (CO 8; CO 18.4, Prone), and a Grappled and Prone
  unit "can still stand by spending the action" (CO 18.4, Grappled).
  The speed-0 case becomes one case of it: movement left is 0, which is
  less than any stand cost.
- **Example.** A Prone unit at speed 6 crawls 2 squares (4 movement). It
  has 2 left; standing costs 3. It may stand by spending its action.

### 2.4 Rejected

- Rescale what is left in proportion (left × new / old): not the D&D
  5th edition rule (5E), needs a rounding policy, makes results depend
  on the order effects were applied, contradicts the existing test, and
  gives nothing back when speed returns after 0.
- Confirm subtraction only and leave Dash and Bar the Way open: each
  step would invent its own state, and "mark all movement used" breaks
  in M2.
- Refuse the stand when movement falls short (5E): it strands a unit
  that has its action, and CO already gives the action route at speed 0
  and for Grappled and Prone.

---

## 3. D-19(a): Trap conditions at combat start (Q4)

### 3.1 Ruling for M1

- **TL-19's condition end is M2.** CO 11.6 stamps it M2, as do the TL
  staging table (TL 12: "floor-1 package") and RS 8.2. For the
  condition end, the roadmap was the outlier. (CO 11.3 and RS 8.2 also
  stamp the push half M2; MR-13 and MR-15 move that half to M1, below.)
- **M1 ships no trap that can apply a condition.**
  - The lesson trap is noise only (TL 6.19; the Kennel tin-can
    tripwire, TL 7.3).
  - The Wire Loop, if D-06 keeps it, ships as a known, armed DH-32
    record and a pathing hazard **without** its Grappled effect (TL 7.3;
    TL 12).
- **M1-30 builds only the M1 part of TL-19**: pushes stop before F1
  trigger squares (TL 6.19: "the M1 rule kept on F1"), using M1-13's
  stop-short check. It adds a **guard check: no M1 F1 trap record
  applies a condition**. The check fails loudly if anyone gives the Wire
  Loop its grab in M1.
- **M1-30 no longer waits on D-19(a).** It still waits on D-06 and D-10.
- **M1 plays the same.** An M1 hero is never snared by a trap, so the F1
  promise "a Grappled solo hero never enters a fight snared" (TL 6.19)
  holds trivially. No rule code, golden or replay changes.

### 3.2 The M2 behaviour, recorded now

1. **One step.** The end of F1 trap conditions is one clause inside a
   single CO 5.5 combat-start condition step. That step runs before the
   surprise grant and before initiative (Q4; 5, step 2).
2. **It selects by trap.** It ends only condition instances on F1 that
   carry a trap id (Q4). Trap conditions and terrain conditions both
   enter with the source "environment" (CO 11.6), so the source alone
   cannot tell them apart. Every condition a trap applies records which
   trap applied it (its DH-32 id) and whether the trap was detected when
   it fired (cross-check). Conditions from terrain, units, abilities or
   the hero's own actions are never touched.
3. **No randomness.** Ending a condition draws nothing, so the combat
   dice and the initiative rolls are the same whether or not the step
   ends anything.
4. **Messages.** Each end emits `ConditionEnded{reason: combatStart}`,
   after `CombatStarted` and before the first `TurnStarted` (Q4), one
   message per ended condition, listed by unit and then in the order
   the conditions were applied. The state changes in step 2; the
   messages are held and emitted after `CombatStarted` (cross-check; 5,
   step 7). `combatStart` joins the automatic end reasons (CO 8); CO 5.5
   already uses it for Asleep.
5. **A trap whose spring starts the fight never leaves its condition on
   the hero (lead's call; Q4 names it a new lead call).** This goes
   beyond TL 6.19, which says only that trap conditions end when combat
   starts. Normally the spring resolves first and combat starts after it
   (TL 6.8; TL 0.1; DH 10.4), so the combat-start step removes the
   condition.
6. **If some path ever starts the fight first, the trap's condition is
   not applied to a party unit on F1 (lead's call).** This is how item 5
   holds when the spring does not resolve first.

### 3.3 Rejected

- The Wire Loop grabs in M1 and M1 builds the rule. Today Grappled
  always has a holder the victim escapes from (CO 18.4); a trap's grab
  would need a Grappled with no holder. It would also need trap ids on
  conditions, a floor number on the state, and an M1 way to spring a
  known trap. All of that moves into M1-13 for one optional trap.
- Trap conditions carry into combat (5E): drops TL 6.19's own call, and
  a solo M1 hero has no ally to cut them free.

---

## 4. D-19(b): Initiative and the opener (Q5)

### 4.1 Ruling

- **An opener** is a hostile action taken out of combat that starts a
  fight; it runs the combat-start step "before it pays its cost and
  resolves" (DH 10.4; DH 20.5). The unit that makes it is the
  **striker**.
- **The opener is checked when it is declared, at step 0 of the
  combat-start order (5), before anything else in it runs.** A refused opener starts no fight and costs nothing (AB 14.3:
  declared, then validated, then the cost is paid) **(lead's call)**.
- **Declaring an opener runs combat start in full**, in the order of
  section 5: who is in the fight, the condition step, surprise, fresh
  turn budgets and per-fight use limits, initiative, then the fight is
  recorded and announced.
- **Who is in the fight, and who is Surprised, is fixed at declaration**
  (EP 6.8.2). The opener's result never changes them. A kill only
  removes the killed unit.
- **The opener then resolves before any turn starts.** It is the
  striker's surprise opener (EP 6.8.2, item 4; for a trap spring, TL 0.1
  and TL 6.8 call it the springer's surprise action), and it is charged
  to the striker's round-1 turn:
  - its action is spent;
  - any once-per-round use (such as Practised Hand, CL 8.3) the opener
    spent stays spent (CL 3.4);
  - its movement and bonus action are kept;
  - once-per-turn flags still clear normally when the first unit in
    initiative order starts its turn (CL 3.4). They are not carried.
- **At most one opener per side** (EP 6.8.2, item 4; TL 6.8).
- **The initiative bar shows a spent-action pip** on the striker, so a
  striker that rolled low and watches enemies act first sees why.
- **Then the first unit in initiative order (slot 0) starts its turn.**
  The striker's first turn of the fight neither resets nor refills what
  the opener spent.
- **In M1 only the striker makes an opener (lead's call).** TL 6.8's
  "other party units take surprise actions only if their own ABL
  conditions hold" means none in M1, which has a solo hero.

### 4.2 Why

- Initiative belongs to combat start: trinket buttons work "only after
  initiative is rolled" (TK 9.2), and with DH 10.4 initiative therefore
  comes before the opener resolves. A pending ENM 12.2 request (not yet
  ruled) also places Take Places before initiative.
- Every passage that bears on cost says the opener is not free: a
  spring "never adds a free round" (TL 0.1); the opening blow "counts as
  an own-turn hit" (SH 7.5); EP rejects opener-stacking exploits (EP 4).
- Rolling initiative first keeps the dice order predictable. Every fight
  rolls one initiative die per fighter, a number known when the opener
  is declared, and then the opener's own dice. Striking first would make
  the number of initiative dice depend on what the strike did.
- Today the game starts the first unit's turn as soon as combat starts,
  and that refreshes its budget. So an opener charged afterwards costs
  nothing unless the striker happens to act first. This ruling closes
  that gap.

### 4.3 Rejected

- A free opener, as in Baldur's Gate 3: two actions before a Surprised
  victim acts; contradicts TL 0.1 and EP 4.
- The opener costs the striker's whole round-1 turn: punishes stealth
  play and the Vanguard's round-1 positioning.
- Strike first, then initiative: the number of initiative dice would
  depend on the strike; contradicts TK 9.2 and DH 10.4.
- The click only starts combat and the attack waits for the striker's
  turn: contradicts DH 10.4 ("pays its cost and resolves").
- Moving the striker to the front of round 1: no design supports it.

---

## 5. The combined combat-start order

In play terms: when a fight starts, the game decides who is in it, ends
conditions that end at combat start, applies surprise, refreshes
everyone's turn resources, and rolls initiative. Only then is the fight
announced. Then any opener resolves, and only then does the first turn
begin.

This replaces the combat-start function as it stands today. Each step
names the roadmap step that builds it.

0. **Checks that a fight can start.** Built (M1-10). A declared opener
   is also checked here; a refused opener starts no fight (4.1).
1. **Who is in the fight**, from the declared or noticing action (EP
   6.8.2). Owner: M1-35. Until then every unit joins, in id order.
2. **The CO 5.5 combat-start condition step**, no randomness. Owner:
   M1-12.
   - durations that run out of combat switch to their in-combat
     duration;
   - the per-fight condition counters reset;
   - Asleep → Surprised (Later): Asleep is removed first, or hard
     exclusivity (CO 6.6) blocks the Surprised;
   - the TL-19 end of F1 trap conditions, selected by trap id (M2, 3.2).
3. **Surprise.** Monster groups (EP 6.8.2, item 3), from M1-13
   (Surprised) and M1-35 (awareness and the F1 surprise rule). The party
   side from M2 (MAIN 4.3; EP 12), with the DH 17.1 exemption. Nothing
   later in the combat-start step (steps 4 to 7) may add a hard
   condition.
4. **Fresh turn budgets, then the per-fight use limits refill.** Built
   (M1-10). Later budget values (M1-14 granted movement, M1-20 stopped
   marker) reset here too.
5. **Initiative** over the participants, on the combat dice, in id
   order. Built (M1-10); narrowed to participants by M1-35.
6. **Record the fight** (round 1, first unit in initiative order) **with
   a "before round 1" marker.** The game counts as in combat from here.
   Owner: M1-11. The marker is new; the existing "turn holder died"
   marker keeps its meaning (cross-check).
7. **Messages:** `CombatStarted` with the initiative order, then the
   messages held from steps 2 and 3, listed by unit and then in the
   order the conditions were applied. Owner: M1-11 (order), M1-12 and
   M1-13 (contents).
8. **If there is an opener** (owner: M1-11):
   - charge its cost to the striker's round 1: the action and any
     once-per-round use limits it spent, not once-per-turn flags; mark
     the striker "opener charged, first turn not yet started";
   - lock targets and resolve. A reaction the opener triggers may
     resolve while the "before round 1" marker is set; moves and End
     Turn are refused then;
   - then the win check. If the fight is won, no `TurnStarted` or
     `TurnEnded` follows.
   At most one opener per side.
9. **Clear the "before round 1" marker; the first unit in initiative
   order starts its turn.** When the striker's first turn of the fight
   starts, turn start neither resets nor refills what the opener spent
   (its action and its once-per-round use-limit spends); the rest of its
   budget resets as normal, and once-per-turn flags and the camp-spend
   flag clear. The striker's "opener charged, first turn not yet
   started" mark then clears. The mark lives on the unit, not on the
   fight, because the striker may act in a later slot, after the "before
   round 1" marker has gone. Owner: M1-11.

Because the opener now costs the striker something, M1-11 raises the
save version (to 8) and the rules version (to 3); the "before round 1"
marker and the per-unit mark are both saved and checked in the game's
state fingerprint. Reordering the steps does not change how many
initiative dice are rolled, so the existing initiative test holds while
everyone takes part.

---

## 6. Change rows

Rows MR-1 to MR-46 (MR = M1-10 rulings change row) list every document
edit. 6.1 edits the design specs on this branch; it was applied on
2026-11-27. 6.2 edits the roadmap, plans and code comments on the M1-10
branch and is still to apply there; 6.3 lists the tests later steps
must add. Rows marked **(lead's call)** carry a lead's call from 8,
confirmed by the client 2026-11-27; this includes lead call 3, so MR-24
and the matching parts of MR-44 apply.

### 6.1 Specs, edited on this branch (applied 2026-11-27)

| Row | Target | Change | Ruling |
|---|---|---|---|
| MR-1 | DH 10.4, mixed events (lines 765-768) | Add a note: open for the DH owner before Bolstered is built (roadmap D-17): (a) whether the check should be independent of part order; (b) whether it counts temporary HP; (c) what the combat log shows for an absorbed and clamped hit | D-17 |
| MR-2 | AB 14.2 (line 1154) | "movement left" → "movement used; movement left = max(0, current speed + granted movement − movement used), and 0 while current speed is 0". Add: outside its own turn, a unit reads only its reaction and its use limits | D-18 |
| MR-3 | AB 7.2, Dash (line 444) | Dash adds granted movement equal to the current speed at the Dash, fixed then (2.2) | D-18 |
| MR-4 | CO 6.7 (lines 455-459) | Add the subtract rule (2.1 items 1-6, including the speed-0 override of granted movement, **lead's call**) and the stand gap (2.3) | D-18 |
| MR-5 | CO 8, `stand` row (line 565) | "At speed 0, or when movement left is less than the stand cost, standing costs the action." | D-18 |
| MR-6 | CO 18.4, Prone (lines 1999-2000) and Grappled interactions (line 2026) | Same extension as MR-5, pointing to 6.7 | D-18 |
| MR-7 | CO 11.2 (lines 1019-1020) | Add: fitted to the D-18 subtract rule in M2 | D-18 |
| MR-8 | CL 8.3, Bar the Way (lines 1184-1188) | Add the per-turn stopped marker: a later speed raise cannot undo it; movement granted after the stop still adds; a stopped unit's movement left is the movement granted after the stop minus movement used after it (**lead's call**) (2.2) | D-18 |
| MR-9 | VG 7.4, Bar the Way (line 841) | "remaining movement this turn becomes 0" → the per-turn stopped marker, as MR-8 | D-18 |
| MR-10 | W 8 (lines 1012-1016) | Same sentence as MR-8, and Dash adds granted movement equal to current speed at the Dash | D-18 |
| MR-11 | CO 5.5, combat start (lines 312-316) | State the one combat-start condition step and its place: before the surprise grant and before initiative; Asleep removed before Surprised; messages held and emitted after `CombatStarted` | D-19(a), D-19(b) |
| MR-12 | CO 8, automatic end reasons (line 571) | Add `combatStart` | D-19(a) |
| MR-13 | CO 11.3, known armed traps (lines 1062-1065) | Mark the F1 push stop M1 (M1-30) and the condition end M2 | D-19(a) |
| MR-14 | CO 11.6 (lines 1142-1157) | Add: a condition a trap applies stores the trap's DH-32 id and `detectionAtFire`; the M2 behaviour of 3.2, including items 5 and 6 (**lead's call**); keep the M2 stamp and point to this record | D-19(a) |
| MR-15 | RS 8.2, floor-1 traps and locks (line 1805) | Mark the F1 push stop M1 (M1-30); the rest of the package stays M2 | D-19(a) |
| MR-16 | TL 0.3, row 12 (line 497) | Say which half is M1: pushes stop before F1 trigger squares; the condition end is M2 | D-19(a) |
| MR-17 | TL 6.19 (lines 2083-2087) | Mark the push rule M1 and the condition end M2; add 3.2 items 5 and 6 (**lead's call**) | D-19(a) |
| MR-18 | TL 12, M1 row (line 3305) | If D-06 keeps the Wire Loop, it ships in M1 as a known, armed DH-32 record and pathing hazard without its Grappled effect | D-19(a) |
| MR-19 | TL 17, TL-19 row (line 3482) | Add the wave: "M1 push stop and pathing record; M2 package and condition end" | D-19(a) |
| MR-20 | DH 10.4 (lines 719-724) and DH 20.5 (lines 2049-2054) | Add: an opener is validated at declaration and a refused one starts no fight (**lead's call**); initiative is rolled in the combat-start step; the opener resolves before any turn and is charged to the striker's round-1 turn (4.1) | D-19(b) |
| MR-21 | TL 6.8, first-spring rule (lines 1384-1388) | Define "surprise action" as the opener charged to the striker's round-1 turn; "other party units" means none in M1 (**lead's call**) | D-19(b) |
| MR-22 | EP 6.8.2 (lines 1630-1634) | Add: the triggering action's result never re-picks membership or Surprised; the full order is this record's section 5 | D-19(b) |
| MR-23 | AB 6.8 (lines 387-392) | Add: the command that declares an opener runs combat start first and takes no out-of-combat tick; a move that leads to noticing keeps its tick, which runs before the knowledge pass and combat start (8 item 4) (**lead's call**) | D-19(b) |
| MR-24 | M1 scope rulings 2.1 item 5, F1 guard (lines 98-100) | Add: while `antechamberCatchUpApplied` is false or its level-up is pending, an attack, ability or shove that targets or includes a boss-group unit is refused at no cost, with a reason code (8 item 3) (**lead's call; confirmed by the client 2026-11-27**) | D-19(b) |

### 6.2 Roadmap, plans and code comments, on the M1-10 branch

Apply these on the M1-10 branch, where the cited line numbers hold;
some of the files also exist on main.

| Row | Target | Change | Ruling |
|---|---|---|---|
| MR-25 | `docs/roadmap/README.md`, D-06 (line 160) | Add two questions and link this record 7 item 3: (i) are known traps pathing hazards in M1 or only from M2 (TL 12 vs CO 11.3, TL 14)? (ii) how does a known lesson trap spring in M1 (TL 0.3 row 9; M1-30's goal)? | D-19(a) |
| MR-26 | `docs/roadmap/README.md`, D-17 (line 171) | Strike through; owner "Later: Bolstered (temporary HP), m4-breadth Later list"; add the full expected result and questions (a) to (c); link this record 1 | D-17 |
| MR-27 | `docs/roadmap/m4-breadth.md`, Later list (lines 75-80) | The Later section is one prose sentence with no entries or Done-when fields. Add a sub-entry format below it (name, owns, Done-when) and the first entry: "Bolstered (temporary HP; DH 14): owns D-17", Done-when: the 1.1 result; the danger band counts temporary HP (DH 17.3: death chance and Deadly are judged against current HP + temporary HP; test); precondition: the DH owner has ruled on (a) to (c) | D-17 |
| MR-28 | `docs/roadmap/m1-vertical-slice.md`, M1-10 Done-when (line 316) | "the temporary-HP half is D-17" → "the temporary-HP half is owned by the Later entry Bolstered (D-17)" | D-17 |
| MR-29 | `src/Core/Damage/DamageFunction.cs:111-112` | Comment: "temporary HP: Later (Bolstered, DH 14; test owed by roadmap D-17; order per DH 15.2 / 20.3)" | D-17 |
| MR-30 | `src/Core/Damage/DamageFunction.cs:172-176` | Comment: "D-17: once temporary HP absorbs per part, rule whether `others` counts before or after absorption, whether part order may decide life or death (DH 10.4 vs CO 9.3), and what the log shows" | D-17 |
| MR-31 | M1-09 plan (lines 51, 202) | "M2 or later" / "M2" for temporary HP → "Later (Bolstered)". Line 55 ("no ... temporary HP in M1") is accurate and stays | D-17 |
| MR-32 | M1-10 plan: line 50; line 254; line 266 | Line 50: "M2 or later" for temporary HP → "Later (Bolstered)". Line 254: D-17's owner → the Later entry Bolstered. Line 266: "D-17 records the temporary-HP half" → "the Later entry Bolstered owns the temporary-HP half (D-17)" | D-17 |
| MR-33 | `docs/roadmap/README.md`, D-18 (line 172) | Strike through; "subtract, not rescale" with 2.1-2.3 in one line; drop "(Slowed, Prone)"; link this record 2 | D-18 |
| MR-34 | M1-10 plan: line 255; call 5 (line 58); line 47 | Line 255: "rescaled" → "subtract, not rescale"; drop "whose Slowed and Prone change speed mid-turn". Call 5: point to this record 2. Line 47: Bar the Way's "movement to 0" → the per-turn stopped marker (2.2) | D-18 |
| MR-35 | `m1-vertical-slice.md`, M1-13 (lines 373-397) | Depends on: drop D-18. Delivers: the per-step movement-point cost (replaces the flat 1 per step, Engine.cs:157), the stand gap rule, the Surprised grant at combat start (section 5, step 3). Done-when: the M1-13 tests of 6.3 | D-18, D-19(b) |
| MR-36 | `m1-vertical-slice.md`, M1-14 (lines 399-418) and M1-20 (lines 544-562) | M1-14 owns the granted-movement value (Dash). M1-20 owns the Bar the Way stopped marker. Each raises the snapshot version, the state digest shape (StateDigest.cs:94-104), the GameState validation (GameState.cs:268-273) and RulesVersion after M1-11's (**lead's call** for RulesVersion). Done-when: the M1-14 and M1-20 tests of 6.3 (M1-20's composite lockout, line 562, stays; add the fixture speed-raise test) | D-18 |
| MR-37 | `src/Core/Combat/TurnBudget.cs:3-7` and `Turns.cs:150-153` | Doc comments cite D-18 (this record 2.1) instead of "plan call 5" alone | D-18 |
| MR-38 | `docs/roadmap/README.md`, D-19 (line 173) | Strike through; (a) and (b) one line each; blocked steps were M1-11 and M1-35 (b), not M1-30; link this record 3, 4 and 5 | D-19 |
| MR-39 | M1-10 plan: D-19 row (line 256); call 6 (line 59) | Line 256: no longer blocks M1-30; blocks M1-11 and M1-35; link this record 3-5. Call 6: the combat-start sentence points to section 5, steps 6-9 and the first-turn rule; the tick sentence points to 8 item 4 | D-19 |
| MR-40 | `m1-vertical-slice.md`, M1-11 (lines 321-345) | Depends on: add D-19(b). Delivers: section 5, steps 6-9 (the "before round 1" marker, held messages, opener validation at declaration, opener charge, the per-unit "opener charged, first turn not yet started" mark, the first-turn rule); the declared opener's command takes no out-of-combat tick (8 item 4, **lead's call**); snapshot v8, the state digest shape (StateDigest.cs) and GameState validation for the marker and the mark; RulesVersion 3. Done-when: the M1-11 tests of 6.3 | D-19(b) |
| MR-41 | `m1-vertical-slice.md`, M1-12 (lines 347-371) | Delivers: the CO 5.5 combat-start condition step (section 5, step 2), placed before the surprise grant and before initiative. Done-when: the M1-12 tests of 6.3 | D-19 |
| MR-42 | `m1-vertical-slice.md`, M1-25 (lines 667-692) | Delivers: the initiative bar's spent-action pip on an opener's striker (4.1). Done-when: the M1-25 test of 6.3 | D-19(b) |
| MR-43 | `m1-vertical-slice.md`, M1-30 (lines 800-830) | Implements: TL-19 → "TL-19 (M1 part: pushes stop before F1 trigger squares)". Depends on: drop D-19. Done-when: replace "`F1TrapRules` passes" with the guard check (`F1TrapRules` is M2: RS V11, line 3001) (**lead's call**) | D-19(a) |
| MR-44 | `m1-vertical-slice.md`, M1-35 (lines 988-1033) | Depends on: D-19 resolved. Delivers: membership from the declared action (section 5, step 1); combat start and the win check narrowed to participants; hostile targeting of the boss group refused before the catch-up (8 item 3, **lead's call; confirmed by the client 2026-11-27**). Done-when: the M1-35 tests of 6.3 | D-19(b) |
| MR-45 | M1-10 plan, line 45 | "`detectionAtFire` stored on condition instances a trap applied" moves from M1-13 to M2, and the instance also stores the trap's DH-32 id | D-19(a) |
| MR-46 | `src/Core/Combat/Turns.cs:44-53` | Re-point the line 49 comment and move it above `Initiative.Roll` (line 45): "CO 5.5 combat-start condition step (M1-12); TL-19's clause is M2 (D-19(a)); order per the M1-10 rulings record section 5". Add a comment at line 52: "M1-11 splits this off: the before-round-1 marker and opener first (D-19(b))" | D-19 |

### 6.3 Tests each later step must add

**M1-11**
- The opener spends the striker's action whatever its place in
  initiative: one seed with the striker first, one with it later.
- An opener that spends a once-per-round use limit leaves it spent
  through the striker's round-1 turn; the rest of the striker's budget
  resets at its turn start.
- Once-per-turn flags clear when the first unit in initiative order
  starts its turn.
- The striker keeps its movement and bonus action.
- The opener's messages come after `CombatStarted` and before the first
  `TurnStarted`.
- Golden replay: after the fight starts, exactly N initiative dice have
  been rolled (N = number of fighters), and the opener's attack roll
  comes next.
- An opener that kills the first unit in initiative order leads to the
  next unit starting round 1.
- An opener that wins the fight emits no `TurnStarted` or `TurnEnded`.
- A reaction triggered by the opener resolves before round 1; moves and
  End Turn are refused then.
- At most one opener per side.
- A refused opener starts no fight and costs nothing (**lead's call**).
- The command that declares an opener advances no tick and runs no tick
  reset, and the opener's charge survives (**lead's call**).
- Reshape CombatScenarioTests.cs:76-92 to go through the new path.

**M1-12**
- The combat-start condition step runs before the surprise grant and
  before initiative; its `ConditionEnded` messages follow
  `CombatStarted`.
- Durations that run out of combat switch; the per-fight condition
  counters reset.
- The number of initiative dice is unchanged.

**M1-13**
- Crawling costs 2 per square, and a crawl step is refused with 1 left.
- The stand cost is taken from the current speed.
- Stand gap: movement left below the cost spends the action and no
  movement; with the action spent, the stand is refused (**lead's
  call**).
- Grappled (speed 0) refuses movement, even with granted movement.
- Movement left is the same whatever order effects were applied in.
- Forced movement leaves movement used unchanged.
- Surprised is granted before initiative, its messages are held and
  follow `CombatStarted`, and nothing later in combat start adds a hard
  condition.

**M1-14**
- Escape in the same turn gives back speed minus movement used.
- Dash adds the current speed at the Dash; a later speed change does
  not alter it.

**M1-20**
- The composite-lockout test (CL 14).
- A fixture speed raise after a Bar the Way stop gives back nothing.

**M1-25**
- The initiative bar shows the striker's spent-action pip (display).

**M1-30**
- The guard check: no M1 F1 trap record applies a condition.
- A push toward an F1 trigger square stops before it.

**M1-35**
- Membership is fixed at declaration.
- The win check counts participants only: a fight ends while a monster
  lives elsewhere on the floor.
- Hostile targeting of the boss group is refused, with a reason, before
  the catch-up (**lead's call; confirmed by the client 2026-11-27**).

**Bolstered (Later)**
- The D-17 Done-when (1.1), with the combat log the DH owner rules
  under question (c).
- The danger band counts temporary HP.

**M2**
- A trap-tagged condition ends on F1 at combat start; terrain and unit
  conditions survive.
- Messages read `CombatStarted`, `ConditionEnded{combatStart}`,
  `TurnStarted`.
- The number of combat dice drawn is unchanged.
- A fight started by a spring leaves the hero unsnared.

---

## 7. Told plainly

Points the research turned up that you should know about, beyond the
five questions.

1. **The "rescale" wording was wrong.** The roadmap row and the M1-10
   plan asked whether movement left is "rescaled". The game never
   rescaled; it subtracts. D-18 confirms what was already built.
2. **Slowed and Prone do not change speed partway through a turn in
   M1.** Slowed is Chilled + Dazed (CO 18.11), and Chilled is M2. Prone
   changes the cost of a square, not speed (CO 18.4). In M1 the only
   mid-turn speed changes are Grappled (speed 0) and perhaps Strained
   (speed −2, AB 7.2), since swapping weapon sets, shield included, is a
   bonus action. Classes grant no speed bonuses (CL 6.4). So D-18 mattered
   less to M1 than the row suggested. No existing test covers a speed
   raise, a halving, or speed coming back after 0; only a flat −2 is
   checked. The M1-14 and M1-20 tests (6.3) cover two of the three; a
   halving cannot be tested before M2. The trap-wire +1 cost is M2 (AB
   7.2) and not an M1 issue.
3. **Known traps as hazards: a second clash nobody had flagged.** TL
   12's M1 row makes known traps "a hazard for pathing and previews".
   CO 11.3 says known armed traps become hazards in M2, and TL 14 says
   "Known traps are hazards from M2". **And how does a known lesson trap
   spring in M1?** Every way to spring a known trap ("Bear it", the
   universal spring rows) is M2 (TL 0.3, row 9), yet M1-30's goal names
   lesson traps. If nothing springs it, its noise lesson never plays.
   Both questions are added to D-06 (MR-25), which still decides whether
   M1 ships traps at all.
4. **`F1TrapRules` is in the wrong wave in the roadmap.** M1-30 asks for
   it, but the M1 V11 skeleton is only `LockNeverCritical`, `HeNoLock`
   and `KeyReachable` (RS V11; TL 12). MR-43 replaces it with the guard
   check.
5. **Combat end must be narrowed to participants, by M1-35.** The win
   check counts every enemy on the floor, and combat start enrols every
   unit. Once the floor has more than one group, a fight would never end
   while any monster lives elsewhere.
6. **Old Ragged and a ranged shot during the F1 guard.** While the guard
   holds, the boss group "cannot notice the party" (M1 scope rulings 2.1
   item 5), yet an attack on a group "is noticed automatically" (ENM
   12.2), and nothing stops a shot from the Forecourt through the
   Arches. That would start the boss fight before the catch-up and break
   the V17t row. The lead called for a refusal (8 item 3), and the
   client approved it on 2026-11-27.
7. **Silent kill against combat start (M2).** A silent kill still runs
   combat start, with its initiative dice, and EP 6.8.2 item 3 would
   mark the group Surprised. SH 8.1 says the target's group does not
   notice, so the target is not Surprised. Unresolved; for M2.
8. **Monster-side openers (M2).** The design is symmetric: monsters can
   open in M2 (lurkers). The DH 21.1 rows that read "before the hero's
   first turn" have to be checked against an opener charged to the
   striker's round 1. For M2.
9. **The opener's out-of-combat tick.** Out of combat each command
   advances one tick, and every tick resets use limits and turn budgets
   (AB 6.8). If that tick ran after combat start, it would wipe the
   opener's charge; if before, a patrol step could change who is in the
   fight. The lead rules that the command declaring an opener runs
   combat start first and takes no tick (8 item 4). A move that leads to
   noticing keeps its tick, which runs before noticing and combat start.
10. **The M1-10 trap scenario runs in the other order.** The test
    starts combat and then deals the trap damage. TL 6.8 resolves the
    spring first and then starts combat. Harmless in M1 (the floor holds
    either way), but M2's 3.2 item 5 depends on it.
11. **No M1 trap opens a fight.** A noisy trap starts a fight by being
    noticed, not as an opener; trap-spring openers are M2 (TL 12). This
    is why 3.2 items 5 and 6 and the trap-spring half of 4.1 are M2
    matters.
12. **What the D-17 critics found.**
    - The claim that M1-10's deferral breaks "hidden information never
      kills" (DH 10.4) was dropped: an undetected trap still cannot kill
      in M1.
    - Bolstered was already tracked among "the 69 Later conditions" in
      `m4-breadth.md`; only the test and the questions in 1.1 had no
      owner.
    - The DH summary row Q7 opens "order floors and Downed discard →
      temporary HP", which out of context reads as floors first. Its own
      next sentence gives the real order (temporary HP before floors,
      DH 15.2), so no edit is needed there; MR-29 cites DH 15.2 / 20.3.
    - Absorbing per part in content order is not a hidden rule: the
      designs say so. The open point is whether that is good design
      (1.1 question (a)).
13. **Two side notes on openers.** AB 13's "opener" is a monster
    opening-ability flag, not evidence about openers in this sense. The
    meaning of "surprise round" (ENM 9.3, ENM 11.3, EP 3.3 row ET3)
    was not settled here; one brief suggested it mean "round 1, before a
    Surprised unit's first turn ends". Deferred to M2, when party
    surprise and lurkers arrive.

---

## 8. Lead calls

Each fills a gap the client's answers leave, and each is marked in its
section **(lead's call)**. All twelve were confirmed by the client
2026-11-27, when the client approved this record as a whole. Rulings
that come from the answers (Dash fixed at the Dash, Q2; messages after
`CombatStarted` and trap ids on conditions, Q4) or from the cross-check
(the new "before round 1" marker) are not lead calls.

1. **Speed 0 overrides granted movement** (2.1 item 3). While current
   speed is 0, movement left is 0; granted movement counts again if
   speed returns in the same turn.
2. **Standing by the action spends no movement, the action route is
   only for the shortfall, and with the action used the stand is
   refused this turn** (2.3).
3. **Refuse hostile targeting of the boss group before the antechamber
   catch-up** (7 item 6): an attack, ability or shove that targets or
   includes a boss-group unit is refused at no cost, with a reason code,
   while `antechamberCatchUpApplied` is false or its level-up is
   pending. Confirmed by the client 2026-11-27, so MR-24 and its part
   of MR-44 apply.
4. **The command that declares an opener runs combat start first and
   takes no out-of-combat tick** (7 item 9), so no patrol step or
   condition tick can change who is in the fight or wipe the opener's
   charge. A move that leads to noticing keeps its tick, which runs
   before noticing and combat start.
5. **Incapacitated is handled separately** (2.1 item 10): it is not a
   speed-0 effect.
6. **A stopped unit's movement left is the movement granted after the
   Bar the Way stop, minus movement used after it** (2.2).
7. **M1-14 and M1-20 each raise the rules version** with the save
   version (2.2).
8. **A trap whose spring starts the fight never leaves its condition on
   the hero** (3.2 item 5; Q4 names this a new lead call).
9. **If a fight ever starts before a spring's condition lands, that
   condition is not applied to a party unit on F1** (3.2 item 6).
10. **An opener is validated at declaration; a refused opener starts no
    fight and costs nothing** (4.1; section 5, step 0).
11. **In M1 only the striker makes an opener** (4.1).
12. **`F1TrapRules` leaves M1-30's Done-when**, replaced by the guard
    check (MR-43).
