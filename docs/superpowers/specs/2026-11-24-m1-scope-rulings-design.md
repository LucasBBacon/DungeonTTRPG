# M1 Scope Rulings: Design (roadmap decisions D-01, D-03, D-08)

Date: 2026-11-24
Status: ruled 2026-11-24. The client delegated D-03 and D-08 to the lead
("resolve to the best of your ability") and decided D-01 directly.
Parents: the implementation roadmap (`docs/roadmap/README.md`, open
decisions) and the approved designs it cites. Abbreviations as in the
roadmap index.

Marker used in owning designs: **(agreed 2026-11-24, roadmap D-0n)**.

How the rulings were made: for D-03 and D-08, three independent readers
collected the design text from different angles; three drafts were
written under different priorities (closest to the approved design,
smallest M1 build, least rework at M2); a panel of three judges scored
them and the best was synthesised; two rounds of skeptics then tried to
refute it against the designs. No draft was refuted. The fixable
findings of the second round are folded in below and listed in 4.

---

## 1. D-01: Folder names (client decision)

MAIN 6.1 follows the repository: the Godot project is `dungeon-ttrpg/`
(`DungeonTTRPG.csproj`), not `game/`. `src/Sim` and `content/` keep their
names and are created by roadmap step M1-01. Applied to MAIN 6.1.

---

## 2. D-03: How the M1 hero reaches level 3 before Old Ragged

### 2.1 Ruling

M1 ships the **floor-1 Forecourt Top-up** (RS 9.4, RT-8) and its **F1
guard** (BR 4.2.1, BR-3). Both rules keep their approved text; only their
wave moves from M2 to M1, and only for floor 1. Level scaling of
experience (ENM 16) ships in M1, as ENM 21 already stages it. Every M1
hero meets Old Ragged at level 3 or higher. No number, band value, curve
or boss kit changes.

1. **The event.** The core event `AntechamberCatchUp` sets experience :=
   max(current, 540) (540 = 270 + 270, ENM 16). The floor-1 run-state
   bool `antechamberCatchUpApplied` (RS 11) starts false and is set true
   by the committed command that fires the event. It is saved in the run
   save and rebuilt by replaying the command log (the suspend slot is
   M2). It never fires twice. A hero already at 540 or more still fires
   it: the flag is set, the overflow is recorded, and no level-up is
   raised. The event and its level-up screens draw from no stream.

2. **The trigger in M1.** Out of combat, the arena is reachable only
   through the floor-1 Forecourt (LG4, the LG5 floor-1 form,
   `ArenaEntrancesFromAntechamber`, arena-owned doorway squares, FL-13).
   - **(a)** An **out-of-combat** move path that enters a Forecourt
     square (`roomId` = Forecourt) ends on the first such square. This
     is a rule of the M1 move command itself, not one of the M2 noticing
     pre-stops. The move command runs its `KnowledgePass` for that step
     first; if the pass does not start combat, the event fires at the end
     of the same committed command and the level-up screen opens before
     any further command. If the pass starts combat, the event waits for
     combat end (item 3).
   - **(b)** An attempt to move into an arena square (doorways included)
     while the flag is false or its level-up is pending is refused at no
     cost, as a blocked square is: nothing commits, no action or draw is
     spent, and nothing is logged. In M1, (a) always fires first, so (b)
     is a defensive refusal; it never fires the event. RS 9.4's "the
     earlier of (a) and (b)" reads, in M1, as "(a); (b) refuses".
   - The move preview greys only the arena squares the party already
     knows (seen through the Arches) and shows the Forecourt Top-up line
     as the reason. Squares hidden by `AntechamberScreened` show nothing.
   - **Forced movement.** While the lock holds, forced movement of a
     **party unit** stops before an arena square exactly as it stops
     before a hazard square in M1 (CO 10.1): the unit stops short, with
     no collision and no collision damage. Forced movement of enemies is
     unchanged.
   - In M1, `ArenaEngaged` never falls on the same tick as the Top-up;
     the approved same-tick order still holds from M2. In M1 the order is
     a command-log order: `AntechamberCatchUp`, then `ArenaEngaged` on a
     later command.

3. **Deferral in combat.** If combat is active when the hero stands on a
   Forecourt square, the event waits for combat end, in this order:
   (i) banked kill shares are paid, each scaled at the hero's pre-Top-up
   level (ENM 16); (ii) if the flag is still false and the hero stands on
   a Forecourt square, the Top-up applies; (iii) one level-up screen
   resolves every level gained, in level order. If the hero is not on a
   Forecourt square at combat end, nothing carries over: the lock holds
   and the next Forecourt square fires (a).

4. **Out-of-combat level-up.** A level-up raised out of combat (in M1 only
   the Top-up; from M2 also the camp catch-up, ENM 16) opens the same
   pending-decision screen at once; nothing else commits until it
   resolves (AB 6.9 bars resolution only during combat). Two levels
   resolve in level order: the level-2 talent card (Drive Through,
   Pivot, Grim Resolve), then the level-3 subclass card (Weaponmaster,
   the only M1 subclass, which grants Practised Hand). The overflow is a
   plain number on the Forecourt Top-up line (naming pass 5.3.32).

5. **F1 guard (BR-3, from M1).** While `antechamberCatchUpApplied` is
   false, the boss group (Old Ragged; M1 has no escort) cannot notice the
   party (`ArenaEngaged` trigger (b) suppressed, CS5), **ignores noise and
   cannot join a fight** (ENM 12.4), and stays inside the arena. Item 2
   makes `ArenaEngaged` trigger (a) impossible before the flag is set.
   Take Places (BR-4), the Sanctum rest rule (BR-31), Glimpses (EV-2) and
   the Behind-you line stay M2; in M1 each of these steps is skipped, not
   stubbed, and an ordinary M1 combat start follows `ArenaEngaged`.

6. **Resting in the Sanctum in M1.** BR-31 stays M2. A rest in the
   Forecourt is governed by the settled condition "no aware hostile"
   (AB 15; DH 15). Retreat, rest and return is the behaviour BR already
   keeps (A-4).

7. **Experience before the door.** Experience is paid on kill per ENM 16
   (patrol 70, guard post 100, lair 140, boss 200), split by frozen cost,
   paid once, each share level-scaled by (100 − 25 × (hero level −
   homeRoom expected level))%, clamped to 50–150%, FloorDiv per share,
   against ENM 6.3's M1 expected levels. The door level is 3 whatever the
   room order, bypass or patrol, so D-03 no longer depends on D-07. The
   ENM 20.1 pool band (110–135% of 540 after one bypass) is unchanged; it
   is a pacing check, not the level-3 guarantee.

8. **Hero level for the floor-1 boss guards.** From M1, the reference
   hero and the lowest legal hero are taken at level 3 **for the floor-1
   boss term only**. The lowest legal hero keeps its definition (ENM 6.4,
   DH 21.1 step 3: tier 6, Con −1, at the AR-20 lowest legal AC); only
   its level changes. Non-boss encounter slots, including their share of
   the floor-1 cumulative round-1 budget, stay evaluated at their
   earliest legal room level as approved. `Floor1BossSustain` stays at
   level 3 (DH 21.1 step 6, VG-24) and now describes a guaranteed state
   (76 at Con +2, 58 at Con −1, need 48). Old Ragged's own level,
   `hpByPartySize[1]` and the M1 kit do not change.

9. **Checks and reports.**
   - V17t M1 row (added to the M1 V17 subset): `AntechamberCatchUp`
     precedes `ArenaEngaged` in the command log; no arena entry is
     accepted and no `ArenaEngaged` fires while the flag is false or its
     level-up is pending.
   - V19t `TopUpNoDraw`: across `AntechamberCatchUp` and its level-up,
     the `combat` counter is unchanged and the DrawLedger attributes no
     draw to either.
   - Runtime assert over the F1 release set: every arena arrival has the
     flag true and the hero at level 3 or higher; arena squares are
     reachable out of combat only through the Forecourt.
   - Reports (no gates): `AntechamberCatchUpRate` for the Vanguard, split
     bypass and full clear; F1 `BossDeathShare` as a baseline for the M2
     gate.
   - Goldens: of the 4 M1 replays (SV 4.11, unchanged count), one must
     include a bypass and the Forecourt level-up, and one a Top-up
     deferred to combat end.

### 2.2 Change rows affected

| Row | Was | Now |
|---|---|---|
| RT-8 | Forecourt Top-up, M2 | Floor-1 Top-up M1; camp catch-up and per-class reports keep their waves |
| BR-3 | F1 guard, M2 | M1 |
| VG-24 | `Floor1BossSustain` at level 3 | Unchanged; now backed by an M1 mechanism |
| BR-4, BR-31, EV-2, BR-22 | M2 | Unchanged |

### 2.3 Rejected

- Keep the Top-up in M2 and let M1 heroes meet the boss at level 2:
  bypass runs always arrive at level 2 (pool 410–482 against 540), which
  VG 10.3 records as failing (45 against 48), and M2 would change the
  door level of every golden.
- Rely on natural experience: full clears land within a few points of
  540; bypasses never do.
- Re-pin the guards at level 2 for M1: reopens VG-24 and DH-26, still
  fails at Con −1, and needs an M1-only Old Ragged.
- Raise floor-1 experience or add an encounter: breaks the ENM 16 band
  table and M2 would revert it.
- Start the hero at level 2: contradicts VG 7.14 and AB 10.1.
- Top-up without the guard: a notice through an Arch would start the
  boss fight before the level-up.
- Pull in the whole M2 Forecourt package (Take Places, BR-31, Glimpses):
  none is needed for the level guarantee.

---

## 3. D-08: Boss reward card, coin, and the end of a one-floor run

### 3.1 Boss reward card: not offered in M1

- In M1, Old Ragged's prize is the signature cache only: Hound-Collar
  Mail with its "Guards:" line (IR-13). `BossDefeated` opens the cache
  and the Bossgate. There is no pick-one-of-three screen.
- The card offer, the fourth dreamed option, Ledger B counting, the
  offer UI, `bossCardFallback` and the hero guarantee stay M2 with their
  change rows (LE-3, LE-4, IR-8, IR-14, BR-28).
- LP 4.1's "M1" tag on the `bossCard` kind means the kind is enumerated
  from M1 so the 25 kinds cover every channel. LootBind skips it.
- Formats are reserved so M2 changes values, not schemas: the LootPlan
  `bossCard` field serialises as `options: []`, `taken: none`,
  `fallback: none`; `LootLedger` carries a `bossCard` line fixed at 0.
- Streams: `loot/lootPlan/{N}/bossCard/*` is registered in M1 with
  `wave: M2`. A registered row whose wave is later than the build's
  wave is **inert**: the DrawLedger expects 0 draws on it. From M2 it
  consumes its reserved 9 × 4. This is the `lineRoll` and
  `h:lurkStealth` precedent, now stated as a rule in R-SEED-3 (RS 6.4):
  reserved counts are consumed even when unused **by live kinds**; an
  inert row counts 0 until its wave. `bossCardFallback` is not
  registered until M2.
- M1 floor-1 values: Floor VP 11 (field 8 + signature 3), Ledger A 6.
  LE 6.3's F1 value of 12 (adding the C card) applies from M2.
- When M2 adds the card, F1 goldens are re-baked once and labelled
  expected; the same wave already re-bakes F1 for new Blueprints and
  content (FL 12). V19t block independence proves enabling the block
  moves no other block's draw positions; identities downstream of the
  card may change through IR 4.17's accumulation order (not an SV-5 bug).

### 3.2 Coin: drops as approved, with no sink

- Kit coin 15, `CoinChanged{delta: 15, source: kit}` at WO1 before F1
  generation (SK-1, SK-3). F1 pool 35: salvage FloorDiv(35 × 120, 1000) =
  4, found 31, main-path floor 17, `coinLumpMin` 3; no lock lumps, no
  Heavy Purse on F1.
- Coin shows on the HUD counter and the loot summary only, has nothing
  to spend in M1, and is lost at run end. ITEMS 20's M2 "coin" means
  coin **spending** at camp.
- `LootLedger` records kit coin on its own `kit` line, outside Ledger C;
  V13g (i) "pools sum exactly" compares 35 against floor sources only.
- If V13g (i) fails on some seeds (main-path floor against
  `coinLumpMin`), it is an M1 BUILD failure handled by the LootBind
  fallbacks and the retry ladder; a systematic failure goes to the LE
  owner before the M1 goldens.
- **LE-14** is one GS17 floor-commit routine with two call sites, in the
  SV 4.8.3 order: stage digests closed; `floor-N-<hash>.static` written
  and flushed; then `run.save` replaced atomically. A run exists only
  once that `run.save` lands; an orphan static file is deleted on load by
  the `runId` and `seq` rule (SV 4.8.1). M1 calls it at WO1 for F1 (no
  `pendingProfile`, no profile write: the profile is M2). M2 calls the
  same routine unchanged at WO5. M1 builds no camp-exit stub.

### 3.3 The Downstair and victory

- The Bossgate (an Arch after `BossDefeated`) and the Downstair with its
  stairs interact square are built as FL 6.11 and FL-6 describe.
- `BossDefeated` opens the Bossgate and the signature cache and does not
  end the run. The hero may loot the cache, the arena and the rest of the
  floor; living groups act normally.
- Using the stairs runs ITEMS 9 as approved (FL 12, FL 14, ITEMS 24,
  agreed FL Q2), with no M1 trim:
  - The left-behind warning lists the party's leftovers markers and flee
    piles, and names (never opens) containers the party has seen but not
    opened. It never shows what an unopened container or a living
    monster holds (`KnowledgeLeak` case). It offers Descend or Stay;
    Stay closes it with no state change and no event.
  - On Descend each leftover and flee-pile instance is scrapped for
    FloorDiv(base × count × scrapPermille, 1000), per instance, summed
    per marker (ITEMS 10.5 item 6), `scrapPermille` 100 *(illustr.)*,
    base from ITEMS 10.5 steps 1–2 (or `basePrice` for mundane goods).
  - Every family that can be scrapped in M1 carries a `basePrice` > 0;
    M1 takes the SK 8.4 scale as placeholder values (SK-11 pending), and
    a missing price fails content validation, as ITEMS requires.
  - Scrap emits `ItemScrapped` and `CoinChanged{source: scrap}`; other
    floor-scoped instances get `ItemDestroyed{lost}`. ITEMS 20's M2
    "stairs Scrap" moves to M1, agreeing with FL 12 and ITEMS 24.
- **Victory is data.** `route.floorCount` (integer) is in the RS 11.1
  configuration snapshot and so in `configHash`: 1 in M1, 5 from M2
  (RT-1). The route pre-roll's draw count does not depend on it.
  Descending the Downstair of floor N = `route.floorCount` emits
  `RunEnded{cause: victory}` as the last event. From M2, F5 has no
  Downstair (FL 6.11), so the rule is dormant and victory stays at the
  last keeper's `BossDefeated` (BR 4.13, unchanged from M2; told plainly
  that M1 reads it differently because F1 is the last floor).
- `RunEnded` becomes `RunEnded{cause, killingEventId?}`, cause one of
  `heroDeath` (with `killingEventId`), `victory` (from M1), `unravelled`
  (reserved for SV-10's release clean end, M2; `RunUnravelled` precedes
  it, order confirmed by the SV owner).
- **Run end, M1 form of WO6.** On `victory` or `heroDeath` the host:
  1. replaces `run.save` atomically with a copy carrying the result
     record (cause, floor, hero level, rounds, run seed, `runId`; next
     `seq`), so a crash from here on can never revive the run;
  2. shows the victory or death screen, then the run record;
  3. removes `run.save` and `floor-1-<hash>.static`;
  4. offers Begin with a fresh seed.
  Loading a `run.save` that carries a result record goes straight to the
  run record, then step 3. There is no slot journal, `pendingProfile`,
  `runHistory` or profile write in M1 (all M2). The M1 victory screen
  shows no coin, Grave Goods or Memento.
- MAIN 4.1's decided rule "the save is deleted on death" is **extended**
  (not changed) to "deleted when the run ends, by death or victory".
- The simulator reports "cleared" (no placed group has a living member
  at Descend) and "left early" separately; its bot descends once the
  boss is dead and the signature cache is looted. Win rate = Descend
  from F1. DH 21.5 rows that need a camp, companions or a party (camp-1
  coin, camp potions, companion downings, the healer split) are not
  printed in M1; they start in M2.

### 3.4 Rejected

- Ship the F1 card as specified (three tier-1 scrolls): M1 has no scroll
  content and a solo Vanguard cannot use scrolls.
- Ship the card with the gear fallback: pulls LE-3, LE-4, IR-8, IR-14 and
  the offer UI into M1.
- Draw and discard the reserved bossCard draws in M1: contradicts the
  "reserved and inert" pattern.
- Drop the kind, field and ledger line in M1: the schema would change at
  M2.
- Stub a camp-exit commit: it has no trigger in a one-floor run.
- Stop dropping coin: breaks V13g (i).
- Victory at `BossDefeated`: the signature cache and the Downstair would
  go unused.
- Defer Scrap to M2: contradicts FL 12, FL 14, ITEMS 24 and FL Q2.
- A Scrap-0 fallback for a missing price: contradicts the ITEMS rule
  that every sellable record has a price above 0.
- `RunEnded` with only {heroDeath, victory}: SV-10 would force a second
  schema change.

---

## 4. Skeptic findings folded in after the second round

| Finding | Resolution |
|---|---|
| Arena lock as walls would add collision damage | 2.1 item 2: party forced movement stops short, as before hazards; no collision |
| Forecourt stop applied in combat too | Out-of-combat moves only |
| Stop placed among the M2 noticing pre-stops | A rule of the M1 move command, after its `KnowledgePass` |
| Whole round-1 budget moved to level 3 | Boss term only (2.1 item 8) |
| Guard missed noise joining | Boss group ignores noise and cannot join while the flag is false (item 5) |
| V17t row had no M1 owner | Added to the M1 V17 subset (SV 5.2, SV 7) |
| Flag written to an M2 journal | Run save and command log only |
| 6 replays vs SV 4.11's 4 | Count stays 4; two replays must cover the Top-up cases |
| `TopUpNoDraw` unmeasurable on keyed domains | Combat counter plus DrawLedger attribution |
| WO6 crash could revive a finished run | Result record written into `run.save` before the screens |
| Inert bossCard not visible in data | Registry row `wave: M2` makes it inert |
| R-SEED-3 not amended in RS 6.4 | RS 6.4 amended |
| Warning could leak unopened contents | Names containers only; `KnowledgeLeak` case |
| "Cleared" undefined | Defined in 3.3 |
| `unravel` vs `unravelled` | `unravelled` |
| Scrap-0 fallback vs price > 0 rule | Missing price fails validation instead |
| MAIN 4.1 decided line edited | Marked as an extension |
| V13g (i) failure had no owner | 3.2 |
