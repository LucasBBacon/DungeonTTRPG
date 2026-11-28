# M1-11 Rulings: Design (roadmap decisions D-22, D-23)

Date: 2026-11-28
Status: Approved, 2026-11-28. The client answered Q1 to Q5 (option A each time) and approved this record as a whole on 2026-11-28, including every lead call in 8.
Parents: the open decisions D-22 and D-23 that step M1-11 raised
(`docs/roadmap/README.md` on the M1-11 branch), the M1-11 plan
(`docs/superpowers/plans/2026-11-27-m1-11-abilities-reactions-areas.md`),
the M1-10 rulings record (`2026-11-27-m1-10-rulings-design.md`, now on
main), the M1 scope rulings (`2026-11-24-m1-scope-rulings-design.md`)
and the approved designs they cite. Abbreviations as in the roadmap
index (ROOM is the room terrain design).

Marker used in owning designs: **(agreed 2026-11-28, roadmap D-22 / D-23)**.

How the rulings were made: each decision got a research brief. For each
brief, independent readers collected the design text and the M1-11 code,
a draft set out the options, and a critic tried to refute it against the
designs and the code. A cross-check then read the two briefs against
each other and against the M1-10 rulings record. Where the cross-check
corrected a brief, the correction stands here, and section 7 lists the
ones worth knowing. One reading in the cross-check is corrected in turn:
AB 14.4 does not move out-of-combat contests to `checks`; it leaves
contests on the combat stream (2.4; 7 item 4). Comparisons with other
games (D&D 5th edition, Pathfinder 2nd edition) were made from memory or
community reports and not checked against those texts. The client then
answered five questions, Q1 to Q5, and chose option A each time. For Q4
the client kept AB 9.2 against the brief's recommendation; see 7 item 1.
The lead's own calls that fill gaps the answers leave are marked
**(lead's call)** and listed in 8; the client approved them with this
record on 2026-11-28.

Where to look: sections 1, 2, 7 and 8 cite designs by section. Line
numbers and code paths are in the change rows (6). Design line numbers
refer to this branch; roadmap, plan, vocabulary, registry and code line
numbers refer to the M1-11 branch.

Sections 3 to 5 are left out on purpose, so that the change rows (6),
Told plainly (7) and Lead calls (8) have the same numbers in every
rulings record.

Terms used throughout:
- **Later**: the list of features recorded in `m4-breadth.md` so nobody
  builds them early.
- **F1**: floor 1, the first floor of the dungeon.
- **Wave**: a group of roadmap steps that ship together, such as M2.
- **The D-22 step**: the first roadmap step that ships content which is
  half on a success. By its Implements list (CN 11–16, with Fire Bomb
  and the M2 scrolls staged in M2 by CN 20) that is **M2-03**, unless an
  earlier step ships such content first.
- **The D-23 wave**: the first wave that ships content which rolls
  outside a fight without starting one.
- **Half on a success**: an effect whose record says a target that
  succeeds on its save takes half damage (`onSuccess: "half"` in ability
  data; `save: "reflexHalf"` in Keg data, ROOM 11.1). The designs also
  call it "save-for-half"; this record uses that word only when quoting.
- **Damage part**: one typed piece of a hit, such as Icefall's 2d8
  bludgeoning and its 4d6 cold. Each part is resolved on its own (AB
  9.1; SM 8.3).
- **The ladder**: weak (damage plus half again), normal (as rolled),
  resistant (half), immune (none) (SM 8.3). A part the target is not
  immune to deals at least 1.
- **FloorDiv(x, 2)**: half of x, rounded down (7 becomes 3).
- **Rider**: a condition a damage part can carry, attempted on its own
  save (AB 9.1, 9.2).
- **Opener**: an ability use made out of combat that starts a fight,
  because its targets or area include a hostile unit or because it is
  noticed (DH 10.4). The unit that makes it is the **striker**.
- **Stream**: a sequence of random numbers that dice are taken from, in
  order. The **combat stream** feeds every roll in a fight. Taking a die
  from a stream is a **draw**; each draw moves every later roll on that
  stream along by one.
- **`checks` set**: a set of rolls made outside a fight. Each roll is
  worked out from a fixed label (who rolls, for what, against whom)
  rather than taken from a running sequence, so the order you do things
  in, or reloading, cannot change it (SV 4.3; TL 6.22). The existing
  sets have names such as `h:save` and `h:notice`; the designs call each
  one a **domain**.
- **Tick**: the unit of time outside a fight (Take time costs 8 ticks,
  TL 6.6).
- **Fishing**: reloading and retrying to get a better roll.
- **Rules version**: a number the game raises whenever a command's
  result changes; replays recorded under an older number stop being
  valid (SV 4.10). The **generation version** does the same for floor
  generation, and the **save version** for the saved-game format.
- **Content hash**: a fingerprint of the content files. If it changes,
  the saved games and goldens built on that content have to be redone.
  A record's **canonical form** is what the hash is computed from.
- **Goldens**: recorded reference runs that the tests replay and
  compare against.
- **Reserved and inert**: listed in the stream registry but used by
  nothing yet.
- **V19t**: the automated check `ExplorationCombatIsolation`: nothing
  done outside a fight changes the next fight's dice (SV 5.2).

---

## 0. Summary

| Decision | Question | Ruling | Built by |
|---|---|---|---|
| D-22 (Q1) | Where does a successful save's halving sit in the damage order? | Each part is halved on its own in DH 20.3 step 2, before the ladder; the ladder and its minimum of 1 follow unchanged; every later step reads the halved parts | The D-22 step (M2-03), in the shared damage function |
| D-22 (Q2) | How do odd amounts round? | Down (FloorDiv), then the existing minimum of 1. Harm 2/3/3 → 1/1/1; Keg 4/6/8 → 2/3/4. Retune bands, not rounding | Same |
| D-22 (Q3) | Do non-damage effects apply on a successful save? | Only on a failure, by default. A record may mark an effect "on any outcome" (Toll's Faltering); the tooltip shows the marker | Same |
| D-22 (Q4) | Does a success against a half-on-a-success effect still attempt a rider? | Yes. AB 9.2 stands: a halved part still deals damage, so it attempts its rider on the rider's own save; a weak target still gets the rider automatically (AB 9.1). No AB change for Q4 (the AB rows in 6 come from other rulings) | M2-03, the first step with both riders (M2-01) and half-on-a-success content |
| D-23 (Q5) | Which dice does an out-of-combat roll use? | M1 keeps the `NotInCombat` refusal. From the D-23 wave, each out-of-combat roll that does not start a fight and has no `checks` set of its own uses a new keyed `checks` set (`h:roll`), keyed by source, verb, target and a per-source attempt index; never the tick, never a floor-wide use counter. An opener is the first command of its fight; a refusal is decided before any noticing roll. A rules version bump when lifted, no generation version bump | M1: a reserved registry row only. The D-23 wave lights it |

Section 1 sets out D-22 and the rules around it; section 2 sets out
D-23. Every document edit is a row in section 6. Sections 3 to 5 are
not used, so that sections 6 to 8 match the M1-10 rulings record.

---

## 1. D-22: How a successful save halves damage (Q1 to Q4)

### 1.1 Where the halving sits (Q1)

**Ruling.**

- **Per part, before the ladder.** On a successful save against an
  effect that is half on a success, each damage part is halved on its
  own in DH 20.3 step 2 (the same rule is repeated in DH 15.2 item 1,
  and both places change), before the ladder:
  1. the part's raw amount (its dice plus its flat amount, after any
     flat changes) becomes FloorDiv(raw, 2). Placing the halving after
     the flat changes is a **(lead's call)** that keeps SM 8.3's order
     ("flat changes first, then the ladder step");
  2. the ladder then applies unchanged (SM 8.3), including "damage that
     is not immune is at least 1";
  3. the other per-part steps of DH 20.3 (shells, type ends, layer
     breaks and damage transforms) and temporary HP follow, as today.
- **Everything after the halving reads the halved parts and their
  sum**: temporary HP (Later, inside step 2), the sum (step 3), Duck
  (step 3a, see 1.5), the floors (step 4, including the undetected-trap
  test that sums the other parts), the save chain (step 5),
  concentration (AB 9.5), the Arcanist marks (ARC 10.3) and the danger
  band (DH 17.3).
- **One shared routine.** The halving lives in the shared damage
  function (DH 20.3), not in the ability resolver. Traps (TL 6.3:
  "strike: damage, Reflex half"), consequence locks (TL 6.11, the Barb),
  hazards (HZ 6.3, Strike), terrain Kegs (ROOM 11.1, `save:
  "reflexHalf"`), death bursts (ENM 17.2, Volatile) and abilities all
  use it, and so do the other world records that say "Reflex for half"
  (terrain clouds and sacs, boss-room strikes; MS-33). Both data
  encodings, `onSuccess: "half"` and `save: "reflexHalf"`, mean this one
  rule.
- **It draws nothing.** Halving is arithmetic on dice already rolled.
  Which stream the dice come from is 1.5 and D-23.
- **Example.** Icefall deals bludgeoning 2d8 and cold 4d6, Reflex half
  (CN 16.4). The target resists cold and makes its save. The dice roll 9
  bludgeoning and 14 cold. Halved: 4 and 7. Then the ladder: bludgeoning
  is normal, so 4; cold is resisted, so FloorDiv(7, 2) = 3. Total: 7.
- **When it is built.** No M1 content is half on a success: the M1
  abilities file is empty, and the F1 traps TL 12 allows (the tin-can
  tripwire and the Wire Loop) deal no damage. For now the game still
  refuses to load any record marked half on a success. The D-22 step
  lifts that refusal, in the same change that adds D-23's out-of-combat
  dice (8 item 12).

**Rejected.**

- Halve each part after the ladder (option B): needs a second minimum
  of 1, reverses SM 8.3's "ladder last" order, and gains nothing. It
  differs from A only against weak targets. Raw 7 on a success: under A,
  halve to 3, then weak adds half, giving 4; under B, weak makes it 10,
  then halve, giving 5.
- Halve the summed total, like Duck (option C). Rejected because:
  - temporary HP would soak the parts before they were halved;
  - the undetected-trap test would compare full-size other parts with a
    halved total, so an undetected trap could kill (HP 5, parts 6 and 6,
    a success);
  - the Arcanist marks would have no per-part amount to count;
  - the preview would need a new combined calculation.

  Nothing in the designs asks for it.

### 1.2 Rounding (Q2)

**Ruling.**

- **Round down** (FloorDiv), then the existing per-part minimum of 1
  unless immune. This matches every other halving in the game: the
  ladder, Duck, Conduct and concentration.
- **Flat bands** (ROOM 3.4): harm 2/3/3 → 1/1/1; Keg 4/6/8 → 2/3/4. The
  same rule holds for any flat band a record makes half on a success,
  collision and contact bands included. (How Overturn's doubled band
  halves is lead's call 9, in 1.5.)
- **If the low bands feel too strong after a success, retune the band
  values, not the rounding.** A harm-band 3 that halves to 1 is a cut of
  two thirds; that is a band question for the ROOM and HZ owners.

**Rejected.**

- Round up (harm 2/3/3 → 1/2/2): a second rounding rule for one case.

### 1.3 Non-damage effects (Q3)

**Ruling.**

- **By default, an effect's non-damage effects apply on a failure
  only.** M1-11 already does this for saves (a success applies nothing).
  Doomsay ("on a failure also Frightened", HR 8.3) and Upheaval ("on a
  failed save pushed 2", CN 16.4) are examples.
- **A record may mark an effect "on any outcome".** It then applies on a
  success as well. Toll's Faltering ("half on a success; Faltering
  applies", HR 9.1) uses the marker.
- **The tooltip shows the marker**, so the player can see that a
  success does not avoid that effect.
- Where the marker sits in the data is the AB owner's data-shape choice
  (AB 14.1, MS-11). Records without it read as failure only, so no
  existing record changes.

**Rejected.**

- Failure only, with no marker: implements Toll wrongly.
- Any outcome by default: implements Doomsay and Upheaval wrongly, and
  makes most save effects unavoidable.

### 1.4 Riders (Q4)

**Ruling.**

- **AB 9.2 is kept unchanged.** A rider's save "is a separate roll from
  the ability's own save". A part halved by a successful save still
  deals damage, so it still attempts its rider, on the rider's own
  separate save.
- **A weak target still gets the rider automatically** (AB 9.1), halved
  or not.
- **The bounds of AB 9.3 hold as written**: at most one rider per target
  per use, chosen in the AB 9.3 order.
- **No AB change for Q4.** M1 has no riders; M2-01 builds them, and the
  D-22 step (M2-03) then exercises this rule.
- **Effects that read damage count the halved amount.** The Arcanist
  marks already count "save-for-half damage" (ARC 10.3, data rule 2);
  Toll's Mourners trigger on a drop to 0 or a Rout, whatever the save
  (HR 9.1).

**Rejected.**

- No rider attempt on a success (the research brief's recommendation).
  This would amend AB 9.2 and its Detonation example. It would also
  mean that a target which saves never risks the rider, on AB's own
  sample record (AB 14.1) and on Fire Bomb, Grating Murmur, Dire
  Tidings, Icefall, Highfall and Frost Shard, which is most targets in
  practice.

### 1.5 Lead's calls around halving

None of these changes M1 play.

- **Minion first (lead's call).** The Minion rank rule (ENM 7.1: "none
  on a successful save against 'half on success' effects") is checked
  before the halving. A Minion that succeeds takes 0; the per-part
  minimum of 1 does not apply to it. A Minion that fails takes full
  damage.
- **Tumble Clear (lead's call).** Its half on a failure (SH 7.6: "a
  success takes none and a failure takes half") uses the same per-part
  routine, FloorDiv then the ladder and the minimum of 1. Its success
  takes 0, as written.
- **Duck (lead's call).** A successful save is never Duck-eligible.
  Duck III (SH 7.3, Duck's L8 upgrade) halves area damage only after a
  failed Reflex save, and never stacks with halving by a save or with
  Tumble Clear: damage halved by a save comes only from a success, and
  SH 7.3 already excludes damage Tumble Clear halved. Duck stays at DH
  20.3 step 3a, on the total.
- **Crits (lead's call).** A save never crits. Only an attack roll crits
  (DH 9; stat model 6), and the M1-11 resolver already lands a failed
  save with no crit. A two-part use with an attack and a separate burst
  (Frost Shard) crits only its attack's event.
- **Multipliers (lead's call).** Any multiplier an effect applies to its
  failure amount is applied before the halving. Overturn (AB 7.2, M3):
  failure 2× the collision band plus Prone; success FloorDiv(2 × band,
  2), which is the band.
- **Same dice on a success (lead's call).** A successful save against a
  half-on-a-success effect rolls the same damage dice a failure would,
  from the same source: in a fight, the combat stream; outside a fight,
  `h:roll` (2.2). This means a save's result never changes which dice
  later rolls in the fight get. (Today M1-11 rolls no damage on a
  success; that changes in the D-22 step.)
- **Partial halving needs its own shape (lead's call).** Frost Shard is
  an attack part (piercing 1d10) plus a cold 2d6 burst, Reflex half (CN
  16.4), and its burst is already a separate damage event (CN 14.4; CO
  9.3). So `onSuccess` cannot be one flag on a whole use. AB 14.1 gains a
  per-part save shape: a secondary area with its own `resolve` and its
  own `onSuccess`. Highfall and Icefall halve every part and need no
  such shape.
- **Existing records keep their fingerprint (lead's call).** The new
  `onSuccess` field is written to a save's canonical form only when it
  is not `none`. Records that do not use it are stored exactly as
  before, so their content hashes do not move and no saved game or
  golden has to be redone for them.
- **The log marks a halved part (lead's call).** Each part's outcome
  records that it was halved, so the combat log can show "halved" beside
  the part.

### 1.6 What else reads the halved damage

These follow from Q1 and Q2, or from the cross-check.

- **Concentration.** The check's difficulty (AB 9.5: "the higher of 10
  and half the damage taken"; "the parts of one hit are summed first")
  reads the halved total, which is DH 20.3's step-3a value. DH calls that
  value the "pre-save total", meaning before the save chain (DH 15.2);
  the text gains a gloss so it is not read as "before the saving throw".
- **The undetected-trap floor** (DH 10.4) sums the halved other parts.
  With halving per part, no new rule is needed.
- **Temporary HP** (Later) absorbs the halved parts. The Bolstered
  entry's question (a) about part order (M1-10 rulings 1.1) now covers
  halved parts too.
- **Preview.** The expected damage shown before a use (AB 8.3) stays
  exact: it weighs the full damage by the chance the target fails, and
  the halved damage by the chance it succeeds. That is the existing
  exact preview (M1-11 `Preview.cs`, "the exact mean") applied to a
  result that now has a success branch; it adds no new rule. The game
  also checks, when it loads each record, that this sum can never grow
  too large to compute. The largest approved half-on-a-success records
  are 8d8: Pronouncement (HR 8.5, M2) and Wilt (CN 16.4, Later);
  Detonation and Lightning Line are 8d6. The danger band (DH 17.3 item
  3: "half or none per record") halves per part the same way.

---

## 2. D-23: Which dice an out-of-combat roll uses (Q5)

### 2.1 Ruling for M1

- **The promise.** Nothing you do outside a fight ever changes the dice
  of the next fight (MAIN 6.3; SV goal 2; checked by V19t). Every
  out-of-combat roll SV 4.3 places has its own `checks` set: object
  checks, trap and lock saves, noticing, stealing, lurker Stealth,
  hazard saves, Curiosity checks and the Truant's steal (the domain
  names are in MS-26). An ability's or a trap's own dice, and
  out-of-combat Hide and Search, have none.
- **M1 keeps the `NotInCombat` refusal.** Out of combat, an ability use
  that is not an opener and would roll (a save, an attack or damage
  dice) is refused with `NotInCombat`, at no cost. It changes nothing:
  no M1 content can reach it. The abilities file is empty, healing never
  rolls (DH 8; CN 11), the Vanguard's extra dice are only rolled as part
  of an attack, and attacking a hostile is an opener. Flat-damage and
  effect-only uses still work out of combat.
- **The reserved row is registered now (lead's call).** `h:roll` (2.2)
  is registered in M1 with its key shape, reserved and inert, before the
  M1 goldens are recorded, as `h:lurkStealth` was. Listing a set that
  nothing uses yet changes no roll (SV 4.5, DS7), though it does change
  the content hashes (6.3). The other unregistered sets (`h:envSave`,
  `h:curiosity`, `h:windfallSteal`) stay unregistered until their waves
  (7 item 12).
- **M1-35 is unblocked.** V19t already passes, because refusals are
  free.

### 2.2 The rule from the D-23 wave

- **One roll set for everything else.** Some content will roll outside
  a fight without starting one. From the D-23 wave, any such roll that
  has no `checks` set of its own uses a single new set under
  `dungeon/floor/{N}/checks`, working name `h:roll`. This covers ability
  saves, attack rolls, damage and healing dice, rider saves,
  concentration checks and follow-on rolls such as Toll's Mourners. The
  client's "every" also reaches item-use checks with no set of their
  own, such as an out-of-class scroll read (CN 13.2: "always a roll").
  It also covers a trap's damage dice. The trap's save stays on `h:save`
  (TL 6.22), but no design said where its damage dice come from, so they
  come from `h:roll`.
- **The label of each roll** is the client's key: the source (the
  ability, item or trap), the verb (what is being rolled: save, attack,
  damage, rider, concentration and so on), the target, and a per-source
  attempt index (a count of that source's earlier attempts, so a retry
  gets a fresh die), then the die. This is TL 6.22's anti-fishing
  pattern. The acting unit is not part of the key, so swapping party
  members cannot fish for a better roll. Only the field encoding (the
  index types, and how a source and a verb are written) is left to the
  D-23 wave **(lead's call)**; MS-39 registers a provisional one.
- **Never the tick, and never a floor-wide use counter.** A tick key can
  be fished: reload, take one step, roll again. A single floor-wide
  counter can be gamed: after a reload the player knows the next
  result, spends it on a cheap use and saves the good one. A per-source
  attempt index has neither problem. SV 4.3 makes every index in its
  domain table part of the saved state, so the attempt index is saved.
- **The working name is `h:roll` (lead's call)**, owner SV. It may be
  renamed before the M1 goldens.
- **What it costs.** Adding the new roll set changes no other roll (SV
  4.5, DS7). Lifting the refusal changes what a command does, so the
  rules version rises and older replays stop being valid (SV 4.10).
  Floor generation is untouched, so there is no generation version bump.
  A save version bump is needed only if the attempt index is a new saved
  value **(lead's call)**. The work also includes: reaction windows that
  can open out of combat (today a frame needs combat, `GameState.cs`);
  widening the M1-11 `Draws()` check, or asserting that the combat
  stream is never drawn out of combat; and giving the dice functions a
  stream source. M1-30 (`h:object`) and M1-35 (`h:notice`) give them
  one first, and the single refactor below builds on that.
- **One refactor with D-22 (lead's call).** D-22 adds a halved input to
  the shared damage function, and D-23 adds a stream source to the same
  function. They are planned and built as one change. The rules version
  rises once if both arrive in the same step (M2-03, if its consumables
  can be used out of combat without starting a fight; lead's call 17);
  otherwise the function is still reworked once, and each lift of a
  refusal costs its own bump.

### 2.3 Noticing order and openers

- **An opener is the first command of its fight**, not a command made
  outside the fight. Combat start runs first (DH 10.4; M1-10 rulings
  5), so the opener's initiative and its own rolls are fight rolls on
  the combat stream. This follows from MAIN 6.3 and SV 4.3, which put
  the noticing roll that starts a fight on `checks`: the roll before the
  fight is out of combat, and everything after it is a fight roll.
  V19t's wording ("every out-of-combat command") gains this clause.
- **Refusal comes before noticing.** After M1-35, a use can start a
  fight because it is noticed (DH 10.4). The game must decide whether to
  refuse the use before it makes the hidden noticing roll, and without
  looking at that roll. Two things go wrong otherwise: a refused use
  would have changed the saved game, and refusals must cost nothing; and
  whether the use was accepted or refused would give away the hidden
  roll.

### 2.4 M1-14's contests and checks (lead's call)

- AB 14.4 moves only named out-of-combat rolls to `checks` (object
  checks, trap and lock saves, noticing, Steal, lurker Stealth, hazard
  saves and Curiosity checks) and leaves contests on the combat stream
  ("unit attacks, saves in combat and contests use the combat stream").
  The M1-11 refusal only covers ability uses. Contests and M1-14's
  actions that every unit has roll through a different route
  (`Contests.cs`), so the refusal does not protect them.
- **So M1-14 routes every contest or check of its own that does not
  start a fight to `checks`, or refuses it.** That is out-of-combat
  Hide and Search, Shove and grapple, Escape and Frighten, and Rally if
  D-16 gives it a roll; Help rolls nothing. No contest draws the combat
  stream out of combat. This extends AB 14.4's pattern; AB 14.4 does
  not say it. A route to a `checks` set that does not yet exist is a new
  keyed consumer (SV 4.5, DS6: "a new consumer gets a new key") and is
  recorded in M1-14's plan.

### 2.5 Rejected

- Build the roll set in M1 now (option B). Rejected: no M1 content
  would use it, yet it would change every place the game rolls dice,
  need reactions to work outside fights, raise the rules and save
  versions, and widen M1-35, the step waiting on this answer.
- A run-wide "explore" sequence with its own saved counter (option C):
  each out-of-combat roll would shift every later one, so exploration
  order would matter again, which TL 6.22 removed ("order-independent
  ... reload-proof"); and it duplicates `checks`.
- Draw the combat stream and exempt these uses from V19t: breaks MAIN
  6.3, SV goal 2 and V19t.
- Fixed averages out of combat: a rules change for AB, DH and CN, and it
  contradicts "fixed dice per record" (CN 4, decision 10) and "always a
  roll" for scrolls (CN 13.2).

---

## 6. Change rows

Rows MS-1 to MS-57 (MS = M1-11 rulings change row) list every document
edit. 6.1 edits the design specs on this branch; the client approved
this record on 2026-11-28 and those edits were applied that day. 6.2 edits the roadmap, plans, vocabulary, registry,
tests and code comments, which live on the M1-11 branch; apply them
there or on a follow-up branch. 6.3 lists the tests later steps must
add, and 6.4 the code each owning step changes. A row marked **(lead's
call)** puts into the designs a call listed in 8, approved with this
record.

### 6.1 Specs, edited on this branch (applied 2026-11-28)

| Row | Target | Change | Ruling |
|---|---|---|---|
| MS-1 | DH 15.2, item 1, sub-step 2 (line 1291) | "the per-part steps, temporary HP included (14)" → "the per-part steps, as 20.3 step 2: on a successful save against an effect that is half on a success, each part's raw amount, after flat changes, first becomes FloorDiv(raw, 2), with any failure multiplier applied first and the Minion rank rule checked first (ENM 7.1); then the ladder with its minimum of 1, the other per-part steps and temporary HP (14) (agreed 2026-11-28, roadmap D-22 / D-23)" (**lead's call**: flat changes, multiplier, Minion) | D-22 |
| MS-2 | DH 15.2, concentration paragraph (lines 1314-1319) | Gloss "pre-save total": "before the save chain, not before a saving throw; it already reflects any halving by a save (step 2)" | D-22 |
| MS-3 | DH 20.3, step 2 (lines 2019-2020) | Add the halving clause first in step 2: FloorDiv(raw, 2) per part, after flat changes (**lead's call**), before the ladder, the ladder and minimum of 1 unchanged; any failure multiplier applied before halving (**lead's call**); a Minion that succeeds takes 0 (**lead's call**); Tumble Clear's half on a failure uses the same routine (**lead's call**); halving draws nothing; one routine for abilities, traps, locks, hazards, Kegs, death bursts, terrain clouds and sacs, and boss-room strikes; the part's outcome records that it was halved (**lead's call**) | D-22 |
| MS-4 | DH 20.3, step 3 and 3a (lines 2021-2026) | Add: a successful save is never Duck-eligible; Duck III applies only after a failed save, never to damage halved by a save or by Tumble Clear (**lead's call**). Gloss "pre-save total" as in MS-2 | D-22 |
| MS-5 | DH 9 (lines 650-652) | Add: a save never crits; a success against a half-on-a-success effect rolls the same damage dice on the same stream a failure would (**lead's call**) | D-22 |
| MS-6 | DH 10.4, party areas out of combat (lines 723-726) | Add: such a use that rolls is refused in M1 (`NotInCombat`); from the D-23 wave its rolls use `h:roll` (SV 4.3). Whether it is refused is decided before any noticing roll | D-23 |
| MS-7 | DH 17.3, item 3 (lines 1543-1546) | "with half or none per record" → "with half (per part, before the ladder; 20.3 step 2) or none per record" | D-22 |
| MS-8 | SM 8.3 (lines 451-455) | Cross-reference only: a halving by a save applies to the raw amount after flat changes and before the ladder step (DH 20.3 step 2) (**lead's call**: the place after flat changes) | D-22 |
| MS-9 | CO 9.3, per-part steps and save chain (lines 735-746) | Add before step 1: "on a successful save against an effect that is half on a success, the part's raw amount is halved (death and healing design 20.3 step 2)"; in the chain sentence, "these per-part steps" reads the halved parts | D-22 |
| MS-10 | AB 9.5 (line 807) | "The parts of one hit are summed first" → add "after any halving by a save, so the check reads the halved total (DH 20.3 step 3a)" | D-22 |
| MS-11 | AB 14.1, data shape (lines 1140-1156) | Document `onSuccess`: `none` (default) or `half`, per DH 20.3 step 2; written to the canonical form only when not `none` (**lead's call**). Add the effect marker "on any outcome" (default failure only; shown in the tooltip); where it sits in the data is the AB owner's choice. Add the per-part save shape for partial halving: a secondary area with its own `resolve` and `onSuccess` (Frost Shard) (**lead's call**) | D-22 |
| MS-12 | AB 14.4 (lines 1213-1224) | Add: an out-of-combat roll that does not start a fight and has no `checks` set of its own is refused in M1 (`NotInCombat`) and, from the D-23 wave, uses `h:roll` (SV 4.3). Out of combat, M1-14's contests and checks (Hide, Search, Shove, grapple, Escape, Frighten; Rally if D-16 gives it a roll) never draw the combat stream: they use `checks` or are refused (**lead's call**, extending this bullet's pattern) | D-23 |
| MS-13 | AB 7.2, Overturn row (line 484) | "success half" → "success half of the failure amount (the 2× applies first; DH 20.3 step 2), which is the band" (**lead's call**) | D-22 |
| MS-14 | ENM 7.1, Minion row (line 647) | Add: "checked before the halving (death and healing design 20.3 step 2): a Minion that succeeds takes 0, with no minimum of 1" (**lead's call**) | D-22 |
| MS-15 | ENM 17.2, Volatile (line 3198) | "Reflex for half" → add "(per part, death and healing design 20.3 step 2)" | D-22 |
| MS-16 | ROOM 11.1, Keg record (line 2612) | Note beside `save:"reflexHalf"`: it means the D-22 halving (DH 20.3 step 2), Keg 4/6/8 → 2/3/4 on a success | D-22 |
| MS-17 | ROOM 3.4, bands (line 590) | Add: a band halved by a successful save rounds down per part with a minimum of 1 (harm 2/3/3 → 1/1/1, Keg 4/6/8 → 2/3/4); if low bands then feel too strong, retune the bands, not the rounding | D-22 |
| MS-18 | SH 7.3, Duck row L8 (line 787) | Add: "never on damage halved by a save (DH 20.3 step 3a)" (**lead's call**) | D-22 |
| MS-19 | SH 7.6, Tumble Clear (lines 958-959) | Add: "the half on a failure is the per-part halving of DH 20.3 step 2, minimum of 1 per part; a success takes 0" (**lead's call**) | D-22 |
| MS-20 | HR 9.1, Toll (lines 1870-1872) | "Faltering applies" → "Faltering applies on any outcome (marker, AB 14.1)" | D-22 |
| MS-21 | CN 16.4, Frost Shard (line 1385) | Add: the burst is a secondary area with its own Reflex save and `onSuccess: half` (AB 14.1 per-part save shape); the attack part is never halved (**lead's call**) | D-22 |
| MS-22 | TL 6.3, strike (line 1005), and TL 6.11, Barb row (line 1565) | Add: "half" is the per-part halving of DH 20.3 step 2 (Barb: harm 2/3/3 → 1/1/1 on a success). Barb: the poison condition applies on a failure only (the Q3 default) unless the TL owner marks it "on any outcome" | D-22 |
| MS-23 | TL 6.22, out-of-combat trap saves (lines 2337-2346); TL 6.6, where a Botch lands (lines 1240-1245) | Add: the save stays on `h:save`; the trap's damage dice, on a failure or a successful save against its half, come from `h:roll` out of combat from the D-23 wave (under TL 12 M1 ships no such trap) and from the combat stream in combat (**lead's call**: the same dice on a success). The same holds for a Botch that springs the trap on the worker (the TL 6.23 preview, lines 2374-2376: "1d4+1 piercing, Reflex half") | D-22, D-23 |
| MS-24 | TL 12, M1 row (line 3324) | Add: any damaging F1 trap needs D-06, D-22 and D-23 decided together; under this row M1 ships none (**lead's call**) | D-22, D-23 |
| MS-25 | HZ 6.3, Strike row (line 1056) | "Save for half" → add "(per part, death and healing design 20.3 step 2)". Strike runs in the round-end step in combat. In M3 Dare rooms it acts per out-of-combat tick (HZ 6.3 line 1080; HZ 6.10), and its save is `h:envSave` (lines 966-969). Its damage is a flat band, so it draws no damage dice either way | D-22 |
| MS-26 | SV 4.3, keyed-check domains (lines 599-606) | Add a row: `h:roll` (working name, **lead's call**): sourceId, verb, targetId, attemptIndex (per source), die; every out-of-combat roll that does not start a fight and has no other domain (ability saves, attack rolls, damage and healing dice, rider saves, concentration checks, follow-on rolls, item-use checks such as CN 13.2 scroll reads, trap damage dice); never the tick, never a floor-wide counter; owner SV; wave: the D-23 wave. Registered in M1 as reserved and inert (**lead's call**) | D-23 |
| MS-27 | SV 4.3, line 608 | Add: an opener counts as the first command of its fight; its rolls are on the combat stream (DH 10.4). Whether a use is refused out of combat is decided before any noticing roll | D-23 |
| MS-28 | SV 5.2, V19t `ExplorationCombatIsolation` (line 1002) | Add the opener clause: the command that declares an opener is the first command of its fight, not an out-of-combat command | D-23 |
| MS-29 | SV 7, M1 checklist, streams registered (line 1486) | Add `h:roll` **reserved and inert**, beside `h:lurkStealth` (**lead's call**) | D-23 |
| MS-30 | SV 10, M1 staging row (line 1781) | "SV-6 (object, save, notice, steal; lurkStealth reserved)" → add "; `h:roll` reserved" (**lead's call**) | D-23 |
| MS-31 | SV 7 and SV 10, the D-23 wave's checklist and staging row (the M2 ones are lines 1535 and 1782) | Add "`h:roll` live" to the D-23 wave's rows, once that wave is known (M2 if M2-03 is the D-23 step; **lead's call**) | D-23 |
| MS-32 | AB 8.3, Preview (lines 677-679) | Add: with `onSuccess: half`, expected damage weighs the halved parts by the chance of a success; the rider chance includes a success (Q4); an "on any outcome" effect shows its marker (Q3) | D-22 |
| MS-33 | ROOM 6.3.5, Kegs (lines 1352-1353); ROOM 6.6, Ignition (line 1799); BR 4.13 (line 740) and BR 5.4 (line 920); HZ 7.5 to 7.7, per-hazard Strike rows (lines 2132, 2191, 2197, 2215, 2236) | Cross-reference beside each "Reflex for half" or "Reflex half": "(per part, death and healing design 20.3 step 2)". The Carnival keg's Oiled payout is Oiled's own rule, not a Keg effect; whether it applies on a success is a question for the ROOM owner (7 item 2) | D-22 |

### 6.2 Roadmap, plans, vocabulary, registry, tests and code comments, on the M1-11 branch

Apply these on the M1-11 branch, where the cited line numbers hold, or
on a follow-up branch.

| Row | Target | Change | Ruling |
|---|---|---|---|
| MS-34 | `docs/roadmap/README.md`, D-22 (line 176) | Strike through; "Resolved 2026-11-28 (client): halve each part before the ladder (DH 20.3 step 2), round down, minimum of 1; effects failure only unless marked; riders kept (AB 9.2)". Add: covers traps, locks, hazards, Keg `reflexHalf` (ROOM 11.1) and death bursts, not only abilities; built by M2-03 with D-23 as one refactor (**lead's call**); link section 1 of this record | D-22 |
| MS-35 | `docs/roadmap/README.md`, D-23 (line 177) | Strike through; "Resolved 2026-11-28 (client): M1 keeps `NotInCombat`; from the D-23 wave, the keyed `checks` set `h:roll` (SV 4.3)". "SV 1001" → "SV 1002". Blocks: M1-35 is unblocked (its Depends-on never listed D-23). Add: covers trap damage dice, rider saves, concentration checks and item-use checks; link section 2 of this record | D-23 |
| MS-36 | `docs/roadmap/README.md`, D-06 (line 160) | Add: a damaging F1 trap needs D-22 and D-23 beside D-06 (co-gates); TL 12's M1 traps deal none (**lead's call**); link sections 1.1 and 2.2 of this record | D-22, D-23 |
| MS-37 | `docs/roadmap/m1-vertical-slice.md`, M1-05 (lines 151-157) | "`h:lurkStealth` reserved and inert" → add "; `h:roll` reserved and inert (M1-11 rulings record 2.1)" (**lead's call**) | D-23 |
| MS-38 | `m1-vertical-slice.md`, M1-14 (lines 440-465) | Delivers: every M1-14 contest or check that does not start a fight (out-of-combat Hide, Search, Shove, grapple, Escape, Frighten; Rally if D-16 gives it a roll) uses `checks` or is refused; none draws the combat stream out of combat; `Contests.cs:102` and `:123` are the routes to change (**lead's call**, extending AB 14.4's pattern). Done-when: the M1-14 test of 6.3 | D-23 |
| MS-39 | `m1-vertical-slice.md`, M1-25 (line 739) | Note beside "Danger band": until the D-22 step it prices only a failure, because no M1 content is half on a success (DH 17.3 item 3) | D-22 |
| MS-40 | `m1-vertical-slice.md`, M1-30 (lines 860-895) | Depends on: "D-06 (floor-1 traps and keys)" → "D-06 (with D-22 and D-23 for any damaging F1 trap; none under TL 12)" (**lead's call**) | D-22, D-23 |
| MS-41 | `m1-vertical-slice.md`, M1-35 (lines 1052-1104) | Delivers: whether a use is refused out of combat is decided before and independently of any noticing roll; V19t counts an opener as the first command of its fight. Done-when: the M1-35 tests of 6.3 | D-23 |
| MS-42 | `docs/roadmap/m2-full-run.md`, M2-03 (lines 54-68) | Delivers: D-22's halving in the shared damage function (DH 20.3 step 2), lifting the load refusal of `half`; the per-part save shape and Frost Shard; the single D-22 and D-23 refactor, and the D-23 lift too if its consumables can be used out of combat without starting a fight (**lead's call**). Preconditions: the AB owner answers whether an area rolls damage once or per target (7 item 14) and whether a Minion that succeeds attempts a flagged rider (7 item 15). Done-when: the D-22 tests of 6.3 | D-22, D-23 |
| MS-43 | `m2-full-run.md`, M2-06 (lines 97-107) | Done-when: the Shade tests of 6.3 (Tumble Clear per part; Duck III never on damage halved by a save) (**lead's call**) | D-22 |
| MS-44 | `m2-full-run.md`, M2-07 (lines 109-119) and M2-08 (lines 121-132) | M2-07: the Arcanist marks count the halved parts (ARC 10.3). M2-08: Toll's "on any outcome" marker; Pronouncement (8d8) passes the per-record preview check | D-22 |
| MS-45 | `m2-full-run.md`, M2-10 (lines 150-162) | Done-when: Volatile's death burst halves per part on a success (ENM 17.2) | D-22 |
| MS-46 | `m2-full-run.md`, M2-12 (lines 178-192) and M2-13 (lines 194-211) | M2-12: Keg `reflexHalf` uses the shared routine (4/6/8 → 2/3/4). M2-13: hazard Strike halves per part in combat (its Dare room form is M3) | D-22 |
| MS-47 | `m2-full-run.md`, M2-14 (lines 213-222) | Done-when: the Barb halves per part with its condition on a failure only; a trap's damage dice, and a Botch's, come from the combat stream in combat and from `h:roll` out of combat once the D-23 wave has lit it | D-22, D-23 |
| MS-48 | `docs/roadmap/m4-breadth.md`, Bolstered precondition (lines 91-94) | Add to question (a): temporary HP absorbs the parts after any halving by a save (D-22, section 1.6 of this record) | D-22 |
| MS-49 | `docs/effects-vocabulary.md`, lines 336-337 | Line 336: "Refused at load. Open decision D-22 ..." → "Refused at load until the D-22 step; ruled (D-22, M1-11 rulings record 1)"; third column "D-22" → "M2-03". Line 337: "No stream is ruled ... Open decision D-23" → "Ruled (D-23, record 2): kept in M1; the reserved `h:roll` set (**lead's call**) from the D-23 wave"; third column "D-23 (blocks M1-35)" → "the D-23 wave" | D-22, D-23 |
| MS-50 | M1-11 plan: owner ruling 4 (line 29); lines 46, 80, 251, 483 and 491 | Line 29: append "D-22 resolved 2026-11-28 (M1-11 rulings record 1); the refusal stays until the D-22 step". Line 46: "SV 1001" → "SV 1002"; "Out-of-combat rolls are D-23" → "D-23, resolved 2026-11-28 (record 2)". Line 80: append "D-23 ruled 2026-11-28 (record 2): kept in M1; `h:roll` from the D-23 wave". Line 251: append "D-22 ruled; refused until the D-22 step". Line 483: mark the `onSuccess: half` and out-of-combat reserved rows as ruled (D-22, D-23). Line 491: "SV 1001" → "SV 1002"; drop "blocks M1-35" | D-22, D-23 |
| MS-51 | `content/registry/streams.json`, after the `h:lurkStealth` row (lines 276-292) | Add a reserved, inert row in the `h:lurkStealth` shape: `path` `dungeon/floor/{N}/checks/h:roll/{sourceId:}/{verb:}/{targetId:}/{attemptIndex}/{die}`, `alias` `[]`, `parent` `dungeon/floor/{N}/checks`, `label` `h:roll`, `indices` sourceId, verb and targetId `labelId`, attemptIndex and die `ulong`, `class` keyed, `stage` play, `readers` `[]`, `consumers` `[]`, `retryScope` floorKey, `rules` `[]`, `keyType` core, `owner` SV, `source` `["SV 4.3"]`, `wave` M2 as the placeholder that keeps it inert in M1, moved if the D-23 wave is later (**lead's call**: name, index types and placeholder wave) | D-23 |
| MS-52 | `tests/Core.Tests/StreamChecksTests.cs:298-306`; `src/Core/Random/StreamHandles.g.cs`; `src/Core/Content/ContentHashes.g.cs` | `The_repository_keeps_the_inert_rows_at_M2`: add the `h:roll` path to the expected list. Regenerate the stream handles (`src/Sim/Tools/StreamHandlesWriter.cs`) and the content hashes (`ContentHashTests.cs:97-99`): `streams.json` is in the manifest and `genContentHash` (`StreamKind.cs:8-10`), so `manifestHash` in the reproducibility key (`RunKey.cs:21`) changes too | D-23 |
| MS-53 | `src/Core/Abilities/AbilityDefinition.cs:150`; `src/Core/Abilities/AbilitiesKind.cs:19` and `:164-171`; `src/Core/Abilities/AbilityRules.cs:20`; `content/README.md:9` | Doc comments: "`onSuccess: half` is ruled (D-22, M1-11 rulings record 1) and built in the D-22 step; written to the canonical form only when not `none` (**lead's call**)". AbilitiesKind.cs:19 and content/README.md:9: "`half` is ruled (D-22) but not yet built". Load message: "\"half\" is ruled (D-22) but not yet built; a save supports onSuccess \"none\" only." (still names D-22, so AbilityRecordTests.cs:195-200 holds) | D-22 |
| MS-54 | `src/Core/Abilities/AbilityUse.cs:115`, `:261` and `:270-273`; `src/Core/Engine/Engine.cs:32`; `tests/Core.Tests/UseAbilityTests.cs:114` | Comments: "plan call 5; D-23 ruled 2026-11-28: the refusal stays in M1; the D-23 wave uses `h:roll` (M1-11 rulings record 2)"; at :261 add "decided before any noticing roll (record 2.3)" | D-23 |
| MS-55 | `src/Core/Damage/DamageFunction.cs:79`, `:111-113`, `:128`, `:185-186` | Line 79: "the combat stream; out of combat `h:roll`, added with D-22's halved input as one refactor (**lead's call**; M1-11 rulings record 8 item 12)". Lines 111-113: step 2 gains "halving by a save: FloorDiv per part before the ladder (D-22), in the D-22 step". Line 128: "3a. Duck (Shade): M2; never on damage halved by a save or by Tumble Clear (D-22; **lead's call**)". Lines 185-186: the D-17 question now covers halved parts too (record 1.6) | D-22, D-23 |
| MS-56 | `src/Core/Abilities/Resolution.cs:344-348` | Comment: "a save never crits (**lead's call**); with `onSuccess: half` a success rolls the same dice on the same stream (**lead's call**) and deals the halved parts; effects only on a failure unless marked (D-22, record 1.3, 1.5)" | D-22 |
| MS-57 | `src/Core/Abilities/Preview.cs:144` | Comment on `CheckPreviewable`: "with `onSuccess: half`, the bound gains a halved term, checked per record at load; the largest approved records are 8d8 (D-22, record 1.6)" | D-22 |

### 6.3 Tests each later step must add

**M1-11 (or its follow-up branch)**
- The reserved `h:roll` row loads; it has no consumers and no draw
  moves. The stream handles and content hashes are regenerated
  (`manifestHash` and `genContentHash` change), which is why the row
  goes in before the M1-39 goldens (**lead's call**).
- The `NotInCombat` refusal test and the "refusals are free" tests stay
  as they are.

**M1-14**
- Out of combat, each M1-14 contest or check that does not start a
  fight (Hide, Search, Shove, grapple, Escape, Frighten; Rally if D-16
  gives it a roll) leaves the combat counter unchanged: each is on
  `checks` or refused at no cost (**lead's call**).

**M1-35**
- V19t `ExplorationCombatIsolation` with the opener clause: the
  declaring command draws the combat stream as its fight's first
  command.
- A drawing non-opener out of combat is refused before any noticing
  roll: the state is unchanged and no `h:notice` die is computed, whether
  or not the use would have been noticed.

**M2-03, the D-22 step**
- The loader accepts `onSuccess: half`; the content hash of an existing
  `none` save ability is unchanged (**lead's call**).
- A two-part fixture that is half on a success: each part FloorDiv(raw,
  2) after flat changes (**lead's call**), then the ladder, minimum of 1;
  a resistant part about a quarter; an immune part 0.
- Flat bands: harm 2/3/3 → 1/1/1; Keg 4/6/8 → 2/3/4 (through the Keg's
  `reflexHalf` and through `onSuccess: half`).
- A success rolls as many damage dice as a failure, on the same stream
  (**lead's call**); a save never crits (**lead's call**).
- A Minion that succeeds takes 0 (**lead's call**).
- A failure multiplier applies before halving (fixture; Overturn itself
  is M3) (**lead's call**).
- Non-damage effects apply on a failure only; an "on any outcome"
  effect applies on a success too; the tooltip shows the marker.
- A successful save still attempts the part's rider on its own save; a
  weak target gets the rider automatically.
- The undetected-trap floor with a halved part; the concentration
  difficulty reads the halved total.
- Preview equals resolution and the odds are exact with a two-part
  fixture that is half on a success; the preview bound holds per record
  at load, sized for 8d8 (Pronouncement).
- Frost Shard: the burst halves on a success and the attack part never
  does (**lead's call**).
- The combat log marks a halved part (**lead's call**).
- The danger band prices a success as the per-part halving.
- Unchanged: the M1-09 draw-order tests, the ladder tests and the
  seed-7 replay golden.

**M2-06 (the Shade)**
- Tumble Clear: a success takes 0; a failure takes the per-part half,
  minimum of 1 per part (**lead's call**).
- Duck III is offered after a failed Reflex save and never on damage
  halved by a save or by Tumble Clear (**lead's call**).

**M2-07, M2-08, M2-10, M2-12, M2-13, M2-14**
- The Arcanist marks count the halved parts (M2-07); Toll's Faltering
  applies on a success (M2-08); Volatile halves per part (M2-10); the
  Keg's `reflexHalf` goes through the shared routine (M2-12); hazard
  Strike halves per part (M2-13); the Barb's condition applies only on
  a failure (M2-14).

**The D-23 wave**
- An out-of-combat non-opener use that rolls draws `h:roll` and leaves
  the combat counter unchanged; a V19t script with an out-of-combat Fire
  Bomb.
- Reload, take a step, use again: the same roll (no tick in the key).
- Two different sources used in either order give the same rolls (no
  floor-wide counter).
- Out of combat, a trap's damage dice (failure or success) and a rider
  save draw `h:roll`.
- The `NotInCombat` refusal test (`UseAbilityTests.cs:352-358`) is
  replaced by these tests.
- If D-22 and D-23 arrive in the same step, the rules version rises once
  for both (**lead's call**).

**Bolstered (Later)**
- Temporary HP absorbs the halved parts.

### 6.4 Code the owning steps change

| Seam (M1-11 branch) | Change | Owner |
|---|---|---|
| `src/Core/Damage/DamageFunction.cs:17-21`, `PartOutcome` | Gains a halved flag for the log (**lead's call**) | M2-03 |
| `DamageFunction.cs:114-126` (`Damage.Resolve`, step 2) and `:164-165` (`DealtByPart`) | Take the halved input: FloorDiv per part before the ladder | M2-03 |
| `DamageFunction.cs:79` and `Resolution.cs:288-289` | Take a stream source instead of `s.Rng.Combat`; one refactor with the line above (**lead's call**) | M2-03, or the D-23 step if later |
| `src/Core/Abilities/AbilityDefinition.cs:150-160`, `SaveResolve` | Gains `OnSuccess` (written only when not `none`) and the per-part save shape; the effect gains the "on any outcome" marker | M2-03 |
| `src/Core/Abilities/AbilityRules.cs:20, 91` | Drop "the only save form" once `half` loads | M2-03 |
| `src/Core/Abilities/Resolution.cs:344-382` | A success with `onSuccess: half` rolls the dice and deals the halved parts; effects on a failure only unless marked | M2-03 |
| `src/Core/Abilities/Preview.cs:26-38` (`TargetPreview`) and `:144` (`CheckPreviewable`) | `Chance` and the expected damage gain the success branch; the bound gains a halved term | M2-03 |
| `src/Core/Run/Versions.cs:24`, `RulesVersion` | Rises with the lift of each refusal | M2-03 and the D-23 step |
| `src/Core/Stats/Contests.cs:102` and `:123` | Out of combat, route to `checks` or refuse | M1-14 |
| `src/Core/State/GameState.cs:331` | Reaction windows that can open out of combat | The D-23 step |
| `src/Core/Abilities/AbilityUse.cs:274-289`, `Draws()` | Widen it, or assert that the combat stream is never drawn out of combat | The D-23 step |
| `tests/Core.Tests/UseAbilityTests.cs:352-358` | Replace the refusal test (6.3) | The D-23 step |

---

## 7. Told plainly

Points the research turned up that you should know about, beyond the
five questions.

1. **Q4 went against the research brief.** The brief recommended no
   rider attempt on a successful save. The lead contested it, and you
   kept AB 9.2. In play: a target that saves against a Fire Bomb still
   risks Burning, on Burning's own save.
2. **Most world damage that is half on a success is flat.** On the
   harm, Keg and collision bands, rounding matters more than per part
   against total. Harm 3 → 1 is a two-thirds cut; Q2 retunes the bands
   if that plays too strong. Two world records leave Q3 open in their
   wording: the Barb's poison condition (TL 6.11) is failure only under
   the Q3 default unless TL marks it, and whether the Carnival keg's
   Oiled payout (ROOM 6.3.5) applies on a success is a question for the
   ROOM owner.
3. **Neither ruling changes M1 play.** No M1 ability, trap or hazard is
   half on a success, and no M1 content can roll out of combat without
   starting a fight. The Minion rank ships in M1, so its "none on a
   success" rule exists but never comes into play until the D-22 step.
4. **M1 has one live gap, at M1-14.** `Contests.cs:123` draws the
   combat stream, and `NotInCombat` guards only ability uses. Until
   M1-14 routes or refuses them, an out-of-combat Hide or Search would
   draw fight dice. The cross-check read AB 14.4 as already keeping
   out-of-combat contests off the combat stream; it does not, so 2.4's
   route is a lead's call extending AB 14.4's pattern. Help rolls
   nothing, and Rally rolls only if D-16 gives it a roll.
5. **The danger band ships in M1** (M1-25), and until the D-22 step it
   prices only a failure, because nothing in M1 is half on a success.
6. **Two data encodings mean one rule.** Ability data says `onSuccess:
   "half"`; Keg data says `save: "reflexHalf"` (ROOM 11.1). Both must map
   to the shared routine, or a Keg and a Fire Bomb would halve
   differently.
7. **Frost Shard halves only part of its damage.** Its attack part is
   never halved; only its burst is. So `onSuccess` cannot be one flag on
   the whole use, and AB 14.1 gains a per-part save shape (1.5).
8. **The preview check must allow for 8d8, not 8d6.** The briefs sized
   it for Detonation (8d6). Pronouncement (HR 8.5, M2) and Wilt (CN 16.4,
   Later) are 8d8.
9. **The trap damage figures in the briefs were not F1 traps.** "3
   (1d3)" is F1's cap on hidden-trap damage (`HiddenTrapBound`), not a
   real trap's damage. "1d4+1 piercing", from the preview of a botched
   disarm, is F2's `HiddenTrapBound` figure (TL 6.10, 6.23). TL 12's M1
   traps deal no damage, which is why D-22 and D-23 matter to M1 only if
   D-06 departs from TL 12. The D-23 brief leaned towards fixed F1 trap
   damage with no code; that was dropped, because a trap strike is
   Reflex half by definition (TL 6.3), so it still needs `h:save` and
   D-22's halving.
10. **Hazard Strike acts out of combat only in Dare rooms (M3).**
    Strike runs in the round-end step in combat. In M3 Dare rooms it
    acts per out-of-combat tick, and its save is `h:envSave` (HZ 6.3,
    6.10). Its damage is a flat band, so it draws no damage dice either
    way. `h:envSave` covers falls (M2) and Plunge and Dare rooms (M3)
    (SV 4.3).
11. **Two proposed ways of labelling rolls could be gamed.** 2.2
    explains both and rules them out.
12. **Only `h:roll` is registered early.** `h:envSave`, `h:curiosity`
    and `h:windfallSteal` are named in SV 4.3 but not yet in the
    registry; they stay unregistered until their waves.
13. **The roadmap's D-23 row overstated its blocker.** It said D-23
    blocks M1-35, but M1-35's Depends-on never listed it, and the row
    cited SV 1001 where the check is at SV 1002. MS-35 fixes both.
14. **Area damage is rolled per target.** The M1-11 resolver rolls an
    area's damage separately for each target; D&D 5th edition rolls once
    for all. Whether the designs specify one roll or one per target was
    not checked. This record does not change it; it is a precondition on
    M2-03 for the AB owner (MS-42).
15. **Riders on a Minion that saves are not settled.** A weak Minion's
    rider follows AB 9.1 as written (Q4's second half). What is open is
    a flagged rider on a Minion that succeeds and so takes 0: Q4's
    reason (a halved part still deals damage) does not reach a part that
    deals nothing. This changes neither Q4 nor AB; it is a precondition
    on M2-03 for the AB owner (MS-42).
16. **"Pre-save total" can be misread.** DH uses it for the total before
    the save chain. With saving throws now halving damage, MS-2 and MS-4
    add a gloss.
17. **Duck III does halve area damage, but only after a failed save.**
    The brief called a save "never Duck-eligible", which overstated it;
    1.5 states the exact rule.
18. **The greyed reason stays "Only in combat"** (`content/strings/en.json`
    line 16). The brief offered clearer wording; the step that lifts the
    refusal can change it.
19. **The M1-10 rulings record is on main.** The briefs, written on the
    M1-11 branch, called it missing; it was on another branch then, and
    its links now resolve.

---

## 8. Lead calls

Each of the seventeen calls below fills a gap the client's answers
leave, and each is marked in its section **(lead's call)**. Items 1 to
12 were set out with the client's answers; items 13 to 17 were found in
review. The client approved all of them with this record on 2026-11-28. Rulings that
come straight from the client's answers or from the cross-check are not
lead calls: per part before the ladder (Q1); round down (Q2); the "on any
outcome" marker (Q3); riders kept (Q4); the keyed roll set, its key, the
opener clause and the noticing order (Q5); concentration reading the
halved total and the per-record preview check (cross-check).

1. **Register the reserved `checks` row now**, inert, as
   `h:lurkStealth` was. Its working name, `h:roll` (owner SV, renameable
   before the M1 goldens), is part of this call (2.1, 2.2).
2. **M1-14 routes every out-of-combat contest or check that does not
   start a fight to `checks`, or refuses it**: Hide, Search, Shove,
   grapple, Escape and Frighten, and Rally if D-16 gives it a roll; Help
   rolls nothing. No combat draws out of combat. This extends AB 14.4's
   pattern; AB 14.4 itself leaves contests on the combat stream (2.4).
3. **D-06, D-22 and D-23 are decided together for any damaging F1
   trap**, and under TL 12 M1 ships none (MS-24, MS-36, MS-40).
4. **A successful save against a half-on-a-success effect rolls its
   damage dice on the same stream a failure would**: in combat the
   combat stream, out of combat `h:roll` (1.5).
5. **A save never crits** (1.5).
6. **Duck III never stacks with halving by a save or with Tumble Clear**
   (1.5).
7. **The Minion rank rule comes first**: a Minion that succeeds takes 0
   (1.5).
8. **Tumble Clear's half on a failure uses the same per-part routine
   and minimum of 1** (1.5).
9. **Any failure multiplier (Overturn's 2×) applies before halving**
   (1.5).
10. **Frost Shard-style partial halving needs a per-part save shape**, a
    row in AB 14.1 (1.5).
11. **`onSuccess` is written to the canonical form only when not
    `none`**, so existing content hashes do not move (1.5).
12. **D-22's and D-23's changes to the damage function are planned as
    one refactor.** The rules version rises once if both arrive in the
    same step; otherwise the function is still reworked once and each
    lift costs its own bump (2.2).
13. **The halving sits after flat changes**, keeping SM 8.3's order
    (1.1; MS-1, MS-3, MS-8).
14. **The log marks a halved part** (1.5; 6.4).
15. **A save version bump is needed only if the attempt index is a new
    saved value** (2.2).
16. **`h:roll`'s field encoding is provisional**: the index types and
    the placeholder wave M2 in the registered row (MS-51) may be changed
    by the D-23 wave (2.2).
17. **M2-03 owns D-22, and also D-23 and the single refactor if its
    consumables can be used out of combat without starting a fight**;
    the other M2 owners are as in MS-42 to MS-47 (2.2; 6.2).
