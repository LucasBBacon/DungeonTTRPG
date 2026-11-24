# M4 Breadth

**Question it answers:** Is every run different? (MAIN 9)

**Scope:** species; multiclassing (only once single-class paths are fun,
MAIN 4.5); item curses and Maladies; the Inverted Cathedral and the
Waking City; larger theme pools; third kit variants; charm breadth;
balance passes with the simulator.

**Sign-off checklist:** SV 7 "M4 and Later".

M4 is the thinnest milestone in the designs. These steps are placeholders
in build order and will be rewritten after M3-END.

---

### M4-01 Species

**Goal:** Heroes choose a species, carried in `GenInputs`.

**Implements:** CL-11; SV 7 M4 (species in `GenInputs`).

**Depends on:** M3-END.

**Done when:** [ ] the CL-11 copy-registry checks pass for every
species trait; species is part of the floor generation identity
(`GenInputs` digest changes with species, test).

### M4-02 Multiclassing

**Goal:** A hero can multiclass within the dip guards.

**Implements:** CL 7.7, CL-8; AB 6.5; SM 5.5; HR-34.

**Depends on:** M4-01; decision D-40 (model (a) or (b), CL 7.7).

**Done when:** [ ] dip guards 1–8 hold; `HealUpcastByClassSlots`
passes; the multiclass precision report is produced.

### M4-03 Curses and Maladies

**Goal:** Cursed items, Clinging and Maladies ship.

**Implements:** LE 8.5; HL 4.2, 4.3; HL-15 to HL-20, HL-29; EV-43.

**Depends on:** M4-02; the conditions owner's budget reading (else
Later).

**Done when:** [ ] V14 (m), V22 (d), (g), (k), (m) pass;
`CursedTakeRate` 20–60%; `MaladyDeathShare` ≤ +2 pp.

### M4-04 Content breadth

**Goal:** Theme pools of 3–4 per tier, the Cathedral and the Waking
City, third kit variants and the M4 charm families.

**Implements:** RS (M4 rows); BR (Waking City); EP (Cathedral roster);
CMP 12; SK (third variant); CA (M4 families); LP-34.

**Depends on:** M4-03.

**Done when:** [ ] Cathedral validators and rows pass; every new record
passes its content suite.

### M4-END Sign-off and playtest

**Done when:**
- [ ] SV 7 "M4" rows ticked (Cathedral rows; tuning-only rows; species
  in `GenInputs`; V5 prefix policy).
- [ ] The M4 playtest answers "is every run different?", recorded in
  `docs/`.

---

## Later (not scheduled)

Recorded so nobody builds them early: daily seeds; the trader; `pledge`;
leaderboards; Oaths; share codes with heirlooms; multi-square bosses;
the Rival Party; Peril 4–5; the Clear Key; units blocking sight; the 69
Later conditions. Sources: SV 7 "Later" and each design's staging table.
