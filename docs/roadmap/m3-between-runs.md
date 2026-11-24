# M3 Between Runs

**Question it answers:** Do players come back? (MAIN 9)

**Scope:** profile grants live in the reproducibility key; unlocks;
starting boons; difficulty ladder and Peril tiers; heirlooms; charms
live; class and aspect unlocks; the menu-room Stairtop hub; in-run
additions (Two Doors at camp 3, Omens, Seams, Perilous Door, Windfall,
Dare and Wager rooms).

**Sign-off checklist:** SV 7 "M3".

Steps here are coarse on purpose: their detail depends on what M2's
balance gates find. Each step is refined before it starts.

---

### M3-01 Unlocks and profile grants

**Goal:** Unlocks, boons and difficulty enter the run through the unlock
snapshot and `configHash`.

**Implements:** SV 4.6 (live), 7 M3; CH 4.11; RS 8.12; RT-18; CA-21;
SV-12.

**Depends on:** M2-END.

**Done when:**
- [ ] Profile grants (`difficulty.tier`, `boons[]`, `nightmareTier`) are
  live in the key; V5 enumeration as written passes.

### M3-02 Difficulty ladder and Peril tiers

**Goal:** Peril tiers 1–3 and the Perilous Door change runs as designed.

**Implements:** ENM 10.1; RS 12; RT-19, RT-21; CH-20; BR-34; HZ-25;
CC-84.

**Depends on:** M3-01.

**Done when:**
- [ ] REL `Floor1ClassSpread` and F1 guard rows pass per Peril tier.
- [ ] DH 18.2 rows reported per tier.

### M3-03 Starting boons and kit variants

**Goal:** Starting boons, including kit variants, are offered and work.

**Implements:** SK 4.4; SK-7; CMP 12 (Old Friend); HL-12.

**Depends on:** M3-02.

**Done when:**
- [ ] V21 (k)–(l) pass; `KitNoDominance`, `KitVariantParity` pass.

### M3-04 Heirlooms

**Goal:** Heirlooms are earned, bequeathed and carried into new runs.

**Implements:** HL 4.1, 10; HL-1 to HL-8, HL-31; SK-4, SK-12;
ITEMS 14, 20.

**Depends on:** M3-03.

**Done when:**
- [ ] V22 (a)–(c), (h), (j), (l) pass.
- [ ] `HeirloomWinDelta` ≤ +3 pp; `HeirloomSlotParity` within ±3 pp.

### M3-05 Charms live

**Goal:** Charm abilities ship, behind their gates.

**Implements:** CA 4.3–4.11, 10; CA-2, CA-5, CA-11, CA-20, CA-24.

**Depends on:** M3-04.

**Done when:**
- [ ] CG1, CG2, CG3 gates read and recorded.
- [ ] V23 M3 rows pass.

### M3-06 Class and aspect unlocks

**Goal:** The first unlockable subclasses and aspects are playable.

**Implements:** CL 9.4; SH 14 (Trapwright); ARC 15; HR 15; TL-37.

**Depends on:** M3-05; decision D-30 (M3 unlock count).

**Done when:**
- [ ] `TrapwrightHasMechanism`, `SpringContentBound` pass.
- [ ] Heal isolation for Warcaller and Pennant passes.

### M3-07 In-run additions

**Goal:** Two Doors, Omens, Seams, Windfall, Dare and Wager rooms and
the other M3 floor content generate and validate.

**Implements:** RS 6.11, 12; LP 4.7; LE 6.12, 8.4; HZ (M3 rows);
TL (M3 rows); ROOM TR-27, TR-28; EV (R3, Sequels); BR 4.14; XI-22 to
XI-27; CC-97, CC-131.

**Depends on:** M3-06.

**Done when:**
- [ ] SV 7 M3 stream rows live (`windfall`, `wager`, `twinCoffer`,
  `h:envSave` Plunge and Dare, `h:windfallSteal`).
- [ ] V13s (o), V15 Windfall rows, V16 Sequel rows, V17 Gambit rows
  pass.
- [ ] SW Two Doors gates re-tested if they failed in M2.

### M3-08 Menu-room hub

**Goal:** The Stairtop hub becomes the menu room, with its objects and
memory.

**Implements:** CH 4.8, 4.11; CH-6; BR-35 (`bossMemory`).

**Depends on:** M3-07.

**Done when:**
- [ ] `UnlockHasHubObject` passes; `BossRepeatRate` with `bossMemory`
  reported.

### M3-END Sign-off and playtest

**Done when:**
- [ ] Every SV 7 "M3" checklist row is ticked.
- [ ] M3 goldens re-blessed with a `genVersion` bump; release set
  re-frozen.
- [ ] The M3 playtest answers "do players come back?", recorded in
  `docs/`.
