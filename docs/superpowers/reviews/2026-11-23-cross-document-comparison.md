# Cross-document comparison report (2026-11-23)

Prefix check: I ran `grep -il 'CC-[0-9]'` over all 30 docs in docs/superpowers/specs/ and it matched 0 files. The CC- prefix is unused, so this report uses it. I edited no design doc.

## 0. Summary in plain words

- **Input:** 217 verified findings.
- **Duplicates:** 17 were the same issue seen from both sides, so about 200 distinct issues remain. The merged pairs and groups are:
  - W/AR M1 soldier and artillery kits
  - CON 9.3 / DH 15.2 Duck halving
  - CON 9.3 / DH 20.3 party floors
  - BR / ROOM / SV arena stamp at GS4
  - HZ class hooks: the Shade hook, the Arcanist Gust and DEP-A1 items, the VG HewCeiling term and ROOM O13 (five findings)
  - SH / ARC floor-pacing lever
  - TL-34 status in RS and TL
  - IR-12 segment-9 support in RS and IR
  - RS reproducibility-key restatements (two findings)
  - XI V18g to V18r labels (two findings)
  - ITEMS / SV trader block order (two findings)
  - ABL / VG camp-share miss lists
  - Ear Trumpet stock in ITEMS 10.3 (MAIN finding and ITEMS finding)
- **By severity:** 9 major, 109 minor, 99 trivial.
- **By kind:** 87 stale text, 42 contradiction, 20 open hook never answered, 15 status or marking error, 14 missing definition, 9 name collision, 8 wave mismatch, 8 broken reference, 8 other, 6 number mismatch.
- **Shape of the problem:** Most of this is drift, not disagreement. A later change row was applied to some restatements of a rule and missed others. Or a request was recorded on the asking side only.
- **Needs the client:** one item (Q-1, coin spent at the Ticket lock).

The few that matter most:

1. **MAIN legend contradicts itself (CC-1).** A find-and-replace left it saying that "(decided)" means unconfirmed. Every later doc relies on that marker.
2. **M1 Kennel kits.** RS and EP give the M1 Huntsman a boar spear and the M1 Poacher a crossbow. W and AR still fix M1 as warhammer-and-shield and sling, for verb-teaching reasons (CC-2).
3. **Saves across patches.** ITEMS 5.1 says a save resumes only under the same inventory.json. Agreed SV Q3 says it loads on the new build (CC-3).
4. **TK trinket tables.** The IR-4 3–5 curated-table floor, as inserted into TK rule 28, cannot be met by most trinket families (CC-4).
5. **V11 failure order.** TL and SV give different V11 failure ladders, which breaks M1 golden-seed parity (CC-5).
6. **Floor-1 theme redraw.** R-SEED-6 reads a past run's floor-1 theme that no profile field stores (CC-6).
7. **Void ship gate.** The agreed Q2 gate rests on a "Shade hazard hook" that the Shade doc never records. The same goes for HZ's Arcanist and Vanguard asks (CC-7).
8. **Coin spent mid-floor.** The M3 Ticket lock spends coin on a floor, against an agreed "coin is spent only at camp" (Q-1).

## 1. Questions for the client

**Q-1. May coin be spent on a dungeon floor (the Ticket lock)?**

- **The conflict:** Your agreed rule (items design 10.1, and framework decision 3) says coin is spent only at camp, and Later at a wandering trader. The traps and locks design added an M3 lock, the Ticket lock ("Admit One"), where coin buys access to a cache. The loot economy design lists it as an approved coin sink. That change was the lead's call and never came to you, so it cannot override your rule.
- **A (recommended):** Allow it. Amend items 10.1 to "spent only at camp, at a Ticket lock (M3), and, Later, at a wandering trader". Add the lock to the coin-change event sources, and correct the restatements in the events and loot economy designs.
- **B:** Keep "camp only". The Ticket lock takes something else (supply, a token, or HP), and the loot economy drops it as a coin sink.
- **Why A:** The lock is M3, optional and access-only (it never buys loot). It gives coin a meaningful use on the floor, and the economy design has already priced it (price no more than 60% of the cache's value). B also works, but it loses a sink that the economy was tuned around.

No other item needs you. Every other conflict is between lead's calls, or it is a restatement that drifted from a rule you already decided.

## 2. Lead's resolutions (major)

**CC-1. MAIN legend self-contradiction (MAIN header, lines 6–8).**
- *Problem:* The approval commit (ffe3bfc) replaced every "(proposal)" with "(decided)", including the one in the legend sentence. The legend now says "(decided)" means both "agreed with the client" and "the lead's calls, not confirmed". It also defines none of the markers used later.
- *Authority:* The approved meaning, which git shows (344f6df) and every later doc uses: "(decided)" marks a main-design client decision.
- *Fix, MAIN:*
  - Replace the second sentence with "No (proposal) items remain since the client's approval of 2026-11-15."
  - Add legend lines for:
    - "(agreed YYYY-MM-DD, <design> Qn)"
    - "read", "a reading" and "an addition"
    - "hook" and "pending"
    - "illustr."
    - M1–M4 and Later (section 9)
  - Name the design in bare agreed markers. In MAIN 2, give TR-27 its marker ("room terrain Q5") and change the end marker to "hazards Q3". Write "exploration Q1" and "seeds Q1" at MAIN 6.6.

**CC-2. M1 soldier and artillery kits (W 15.3, 15.4, 21.3, 22; AR 10.5, 12.3, 13.6; RS 8.4 Kennel roster, M1 subset and 13 M1 row; EP 7.3 K1; IR 5 Kennel row).**
- *Problem:*
  - RS 8.4 gives the M1 Huntsman a boar spear (reach, piercing) plus Shove, and the Poacher a crossbow.
  - W fixes the M1 soldier as warhammer and shield (Push) plus Interpose plus Shove, and the M1 artillery as a sling (Push). The reason is that only Push fires against a solo hero.
  - A spear (Skewer) or pike (Set, barred at room level 1) Huntsman, and a crossbow (Skewer) Poacher, never teach their verb in M1.
  - AR verified AC bands only for the W loadouts. The warhammer soldier is legal at L1–4, and EP K1 places the Huntsman at −1.
  - No change row reconciles any of this.
- *Authority:* W 15.3 and AR 12.3, 13.6 and 10.5 stand until amended. All of these are lead's calls.
- *Fix (minimal):* Add an RS change row (RT-n) that re-dresses the RS 8.4 kits onto the W chassis:
  - Huntsman: a warhammer-and-shield soldier dressed as a hunting maul and a boar-board shield, plus Shove.
  - Poacher: sling artillery dressed as a hunting sling.
  - Keep "boar spear" only for a promoted or F2+ pike variant (never at room level 1, W 15.1).
- *Then:*
  - **RS 8.4:** update the roster cells.
  - **IR 5:** update the Kennel row's base families.
  - **EP 7.3:** re-check K1 so that the Huntsman's level after the −1 shift lies in AR 12.3's legal L1–4. If not, raise the K1 room or drop the shift.
  - **AR 13.6:** name the types ("Kennel Mastiff (claw brute), Huntsman (warhammer soldier), Poacher (sling artillery); RS 8.4").
  - **W:** no change.
- *Fallback:* If the RS owner insists on the spear and crossbow, a W row must amend 15.3, 15.4, 21.3 and 22, re-run 15.1, and add AR 12.3 rows. That is more work and loses the verb lesson.

**CC-3. Save across a tuning patch (ITEMS 5.1 lines 312–316; SV 4.10, 4.10.1; also TK 17.2, CN 18.1, DH 20).**
- *Problem:* ITEMS (lead's call) says a save resumes only under the same inventory.json hash. SV (agreed, SV Q3) says a run continues on the new build. If stackMax changes, every saved stack changes, because count = stackMax − spent.
- *Authority:* SV, because it is agreed.
- *Fix:*
  - **ITEMS 5.1:** replace the sentence with: "A save records the hash of inventory.json; a run crossing a patch continues on the new build and is marked not comparable (seeds and validation design 4.10.1). Each stack instance stores its stackMax at creation, so a stackMax change never alters a saved stack's dose count (change SV-15)."
  - **TK 17.2, CN 18.1, DH 20:** make the same wording change. Add SaveMigrationGoldens coverage of a stackMax change to SV 4.10.

**CC-4. Trinket curated tables (TK 18.1 rule 28; TK 14.1; IR 4.6, the IR record schema at line 985, IR 15).**
- *Problem:* IR-4 requires 3–5 curated entries per family. After rule 28's own exclusions:
  - hands and feet leave 2;
  - channel rings leave 2;
  - Wardstone's charm table leaves 1.
- *Authority:* TK as the category owner, applied through IR-4's own extension.
- *Fix:*
  - **TK rule 28:** add "except trinket families (props.trinket.*), whose curated table is the legal set left after this rule's exclusions (at least 1)".
  - **IR 4.6:** add the same exception, noting that "never the whole table" does not apply to trinkets.
  - **IR schema:** change the comment to "(3–5 per family; trinkets per TK rule 28)".
  - V14 and TK 18.3 then read the exception.

**CC-5. V11 failure ladder (TL 6.22 lines 2249–2268, TL 11.2; SV 5.4 AP1 items 1 and 3, the AP2 trapsResolve act, SV 5.5 LR1).**
- *Problem:* The two docs order the steps differently.
  - TL: per-rule actions, then the keyed retry1, then removal and Plain with a re-check, then V11Fallback.
  - SV: removal and Plain first, then retry1.
  - The results differ, and M1 golden parity breaks.
- *Authority:* TL. It owns V11, and the SV-10 text applied in TL is the approved order.
- *Fix:*
  - **SV AP1 item 1:** "V11g1 per-rule failure actions (LR1); on failure traps/retry1 (LR2); if that fails, TL removal of failing traps and Plain for failing locks (draw-free), then V11g1 again".
  - **SV AP1 item 3:** "V11 → LR2, then TL removal and Plain with a re-check, then LR4 V11Fallback".
  - **SV AP2 act:** apply the same sequence.
  - **SV LR1 row:** add "TL post-retry removal and Plain".
  - **TL 6.22 ladder paragraph:** add the clause "the post-retry removal and Plain is a draw-free LR1 repair taken after LR2 and before LR4".

**CC-6. Floor-1 theme memory missing (RS 6.4 R-SEED-6 line 761; EV 9.1 profile block; MAIN 6.6; SV 4.6).**
- *Problem:* The rule redraws when the forecast floor-1 theme equals that of each of the three previous runs. No field stores a past run's theme, and "previous runs" has no scope (seeded runs never advance recentRuns).
- *Authority:* Lead's call, following the BR-25 pattern.
- *Fix:*
  - Add f1ThemeId to each recentRuns entry. It is written at run start for every launched personal run and is outside the reproducibility key.
  - **R-SEED-6:** say "each of the last three personal runs (recentRuns[].f1ThemeId)".
  - Add the field to EV 9.1, to MAIN 6.6 beside f1BossId, and to the SV 4.6 profile-memory row.

**CC-7. Hazards' class-owner requests recorded on one side only. This merges five findings.**
- *Locations:* HZ 6.4 Void ship gates (agreed Q2) at lines 1410–1419, 6.15 at 1804–1808, 12 M2 gated row, O13 and 18; SH 8.2/14/18; ARC 16/19; VG 6.4 rule 8 and 18; ROOM 3.4 and KP2.
- *Problem:* No HZ change row targets SH, ARC or VG, so none of these docs records what HZ asks of it:
  - **Shade:** the Shade hazard hook, which is a Void ship gate. HZ also asks for the Trapwright's Jury-Rig Stir re-time.
  - **Arcanist:** a second Gust charge in Void rooms, and counting Stirs, Cords, Voids and vents for DEP-A1 and KP2.
  - **Vanguard:** the HewCeiling Void-removal term.
- *Also:* HZ 6.15 states the DEP-A1 extension as fact, although ROOM owns that definition.
- *Authority:* HZ asks. The owing docs must record the asks. ROOM owns the definition of "interactive feature".
- *Fix:*
  - **HZ:** add row HZ-31 ("— (request)", owners SH, ARC, VG, ROOM; M2) to the HZ 17 request list. In HZ 6.15, reword the Arcanist row to "would count for DEP-A1 (request, O13)".
  - **SH 18, with a pointer in 8.2:** "Pending request (hazards design 6.15, 18): a Shade hazard hook owed before Voids ship (candidates: Operate a Cord without breaking Hidden; push-1 from Hidden at Void edges; VoidClassDeathGap ≤ BASE + 2). Until it is delivered, Voids do not ship. Also the Trapwright Jury-Rig Stir re-time (C ± 1, never below 2; M3, the Shade owner's call)."
  - **ARC 16 and 19:** pending request for the second Gust charge in Void rooms, and for the DEP-A1/KP2 extension.
  - **VG 6.4 rule 8 and VG 18:** pending request for the removal term: "a Void removal is valued as the target's remaining HP, capped by Clutch, for FallsByWrecker against HewCeiling".
  - **ROOM 3.4 and KP2:** record the extension as a pending request.

**CC-8. Ticket lock and coin.** This is the client question Q-1. Apply option A or B once the client answers. Until then, mark TL 7.7, LE 3 and the LE sink-table row "pending client ruling (Q-1)".

## 3. Lead's resolutions (minor and trivial), grouped by doc

Each entry gives the target, the fix and the authority in brackets. "→" means "change to".

**MAIN**
- CC-9: MAIN 10.
  - Point to the four approved class designs for feature lists, subclasses, capstones and kits.
  - Delete the "feature lists (stage 2)" bullet.
  - "starting kits remain open" → "set in the four class designs".
  - CL 20.2: "stays future work" → taken up by the class designs.
- CC-10: MAIN 4.1 vendor list. Add the conditional Wax Key (M2) and Ear Trumpet (M3) (TL-25, XI-26).
- CC-11: MAIN 9 M3 row. The profile file, curiosityMemory, recentRuns[].f1BossId, runHistory[] and uiMemory arrive in M2 (6.6).
- CC-12: MAIN 6.3 Phial sentence. Add "pending the consumables owner's confirmation; fallback in SV 14.2" (CN 14.7 still pending).
- CC-13: MAIN 6.3. Two corrections (SV, HZ, ENM):
  - h:envSave → "falls M2, Plunge and Dare rooms M3".
  - h:steal → "reserved M1, live M2".

**STAT**
- CC-14: STAT 10 bullet → "starting kits and Clinging. Pitted Armour is in armour 9.4 and stays Later." (AR-37)
- CC-15: STAT 12. "class hit points" are not builder numbers. The flat 11 and tiers 6/7/8 are client numbers, and Recovery 4/2 are lead's numbers (DH 7.1, 8).
- CC-16: STAT 2. "Designed next" → "designed separately in the ability design". Cite room terrain 6.3, 6.3.7, 6.6–6.8 and 6.11, plus CON 11.
- CC-17: STAT 6 Passive stealth row → the minimum over members of 10 + Dex + proficiency if trained in Stealth (double with mastery) + effects, less Clatter. Extend the passive bullet to worst-member values.
- CC-18: STAT 6 Passive perception row. Against a lurker it is the highest over living, non-Downed units with line of sight (EP-3, XI-2).

**ABL**
- CC-19: ABL 15.1. Two fixes:
  - Replace the old "already seen within hearing range" sentence with the noticing-predicate PatrolForecast window: earned forecasts only, and a glimpse shows only the pip (XI-6).
  - Add the M3 location bans "no short rest while any party unit is in a Dare room (HZ-21) or a Wager room (TL-29)", with Rest-button reasons in 15.4 and a pointer from DH 11.3.
- CC-20: ABL 6.3. Add the Shade's accepted miss at levels 1–4 (SH-28). Also VG 10.4: add the HR-31 bullet (Herald levels 1–3).
- CC-21: ABL 11.2 → "one of two kit upgrades" (CMP-16).
- CC-22: ABL 14.5. Add "or 49 at levels 7–10" (ARC-9).
- CC-23: ABL 14.4 → "object checks (no unit in key), trap and lock saves (keyed by unit and springIndex)" (TL 6.22, SV 4.3).

**CON**
- CC-24: CON 18.8 Cowed. Shaken is "Later; in M2 they start with no state" (ENM 14.8).
- CC-25: CON 25 and 22. Mark traps, hazard placement, Shed and tonics as settled with pointers: TL 6.9/6.10/6.19/6.20, HZ 6.2/6.11, ENM 17.5 (BR-7 pending) and CN 11. Keep TR-13, RT-16/TL-39, HZ-13 and HZ-17 as pending.
- CC-26: CON 21 M2. Add bile squares, harm squares and the contact-hazard obstacle branch (CON 11.5).
- CC-27: CON 10.2 and 18.4. A second Void failure leaves the unit on its last safe square, Prone and Teetering, with the band floored at 1 HP (HZ-9, HZ 6.4 item 4).
- CC-28: Party floors (merged CON 9.3 / DH 20.3) (DH 10.4).
  - DH 20.3 step 4: clamp when any DH 10.4 floor applies: out of combat, undetected trap, FallFloor, unwarned Stir act, or lurker opener after its 20% cap.
  - DH 20.4: extend the DamageClamped reasons.
  - CON 9.3: point the parenthetical to DH 10.4.
- CC-29: Duck (merged). The pre-save total for concentration is the step-3a value. Apply in CON 9.3 and DH 15.2 (insert step 3a), and add both to SH-6's targets (SH-6).
- CC-30: CON 18.7 Revealed. Use "an opposing unit that contributes sight (for the party, living and non-Downed)" (XI-2). CON 18.10 landing: "not a shield other than a Reliquary Shield" (AR-15).

**ENM**
- CC-31: ENM 12.2 BR-3/BR-4 block. Applied: "confirmed by the main design owner (MAIN 4.3)". Keep only the enemies owner's ruling on combat-start order pending.
- CC-32: ENM 6.4 BR-8 rows. Make them conditional on BR-7 (no Strike on F1) and on BR-6 (Downbeats). Make the same edit in BR 4.3.3.
- CC-33: ENM 14.4 Waver. Re-mark it "Pending request (HR-20, HX-2): … not yet ruled". Make 14.10 and 20.3 read "if Waver is adopted". Adoption is the enemies owner's ruling.
- CC-34: ENM 15.5 Steal key → H(floorKey/checks, "h:windfallSteal", windfallId, 0, die) (SV-6).
- CC-35: ENM 10.2 examples. Recompose so each meets the M2 fill curve (F1 lair 15–16, F3 lair 40–43, F5 lair 56–60, F1 elite room 19), or say that the test asserts only the M1 window.
- CC-36: ENM 10.1. Add the EP 6.2 exceptions: the F1 hard ceiling, Cuckoo 850‰, and the F4–5 +2 cap.
- CC-37: ENM 22/25 dependencies → settled, provisional only to simulator calibration. Add answered-by pointers in 25.
- CC-38: Trivial ENM fixes.
  - 18.2 "22–27 types" → 56 (RT-5).
  - Wave key → M1/M2/M3/M4/Later, using the BR form. The same fix is optional in ITEMS, W and AR.
  - 15.2 and LE 6.9: drop "or Cowed".
  - 19.1 JSON: add dexSlot, and remove salvageCoin (LE-8).

**ITEMS**
- CC-39: ITEMS 5.1 ePlusGate. Add landingEnvelopeMinPermille 600 and bossHardLandMinPermille 500 (TK-4).
- CC-40: ITEMS 18.3. Add the `dismissed` reason (CMP-21). 18.1: add a dreamedAway transition row (LE-4, LE-21). 7.3: add WaxKeysFull (TL-25).
- CC-41: ITEMS 16 Strength row. Derived from strengthClass and never on the record. 12.3: "weapon, focus, armour and shield write neither" (W-6, AR-36).
- CC-42: ITEMS 22 "Not tuning". Add every 5.1 not-tuning key and the agreed family caps.
- CC-43: ITEMS 13 and 19.1 rule 2. A cursed twin's value is its own tier T, and its benefit is authored to T+1 (LE-22, IR-20).
- CC-44: Lodestring and Ear Trumpet as size-0 party counters (XI-23, XI-26).
  - Add both counters to ITEMS 5.3, the 12.3 counter enum and the 19.1 rule 5 size-0 list.
  - 10.3 limited-stock and lapse sentences: include the Ear Trumpet. This is the merged duplicate of CC-10.
- CC-45: Trader block order (merged ITEMS 10.9 / SV 4.3 / LE 6.10) (ENM 10.5 and ITEMS 10.9, which SV-32 wrote).
  - In SV 4.3 and LE 6.10, move trader to just after the boss channel, followed by returned trader slots on containers.
  - Add a traderService block (Later).
- CC-46: ITEMS 21, 24 and 17 row 3. Mark them answered with pointers (CMP 8.3/8.4, class kits, FL, TL, LP, SV). Ask the TK owner, inside IR-14, to restate the TK-27 order as LootPlan identities, then containers, carried, boss, camp.

**W**
- CC-47: W 26 and the 6.4 note. Record two pending items:
  - SH N-5's predicted launcher-floor failures, to be answered after the M2 parity run.
  - IR 9.4's content requests (Rare weapon and focus records ≥ 3 per lane; 4–6 value-2 majors), with the IR 14.2 fallback.
- CC-48: W 5.6. Reliquary Shield exception: Notched lands through it and suppresses Spare only (AR-15).
- CC-49: Trivial W fixes.
  - 18.6 Headsman's Greataxe: "no channel until its M3 boss" (BR-27).
  - 17.8 and 22 staff: M2 (CL-13d).
  - 26 classes and companions bullets: point to the class designs and CMP.

**AR**
- CC-50: AR 5.7. Add the M2 simulator recompute note (LE 6.3 hook). In LE 6.3 and LK18, "AR 5.2" → "AR 5.1".
- CC-51: AR 22. Kits are settled in the class designs and CMP. Acrobatics is met by FL 6.4 and ROOM 6.2.6, and only the simulator evidence is owed.
- CC-52: AR 16.2 acEnvelope. Add coverMinHitPermille 250 (TR-4, agreed).

**TK**
- CC-53: TK 8.2 and 24.3. Record IR 9.4's request for 2–3 Rare neck families as pending, held under TK 24.3's report trigger. IR 9.4 wave → "M2 request; TK holds it under 24.3".
- CC-54: TK 11 item 1 and 23. Record the owed registry rulings:
  - Duskhem against No Witnesses III and Long Shadow;
  - Slinger's Bracers against Toss and Dust (VG-18);
  - the Herald pairs, including Reach the Fallen against the Lifeline Pendant.
- CC-55: TK 14.2 example names → suffix epithets: "Strider's Boots of the Runner", "Ember Ring of Aid", "Wardstone of the Tally" (IR-9).

**CN**
- CC-56: CN 16.4 and 16.3/10. Record IR 9.4's pending requests: Herald tier-2 and tier-3 scrolls (M2), and Later Rare bomb shapes and arena-keyed Viaticum tools.
- CC-57: CON 9.3, through a new CN change row. Each attack roll or dart of a multi-attack ability, and a separately resolved burst, is its own damage event (CN 14.4, ARC 8).
- CC-58: Trivial CN fixes.
  - 16.5 Snaring → "a loot rule only; all wands floor 2+ (ARC-24)".
  - 14.7 step 3 → "Ledger A drops by 1 (logged exemption), VP counted in the camp channel" (LE 6.13).
  - 10 → "death and healing Q3".
  - 3.1 → Last Ward and Last-Breath Charm retired (DH-45).

**DH**
- CC-59: DH 17.12 and XI 4.9.8. One floor-entry card, headed by the Stratum Card, in slot 8. TL's first-entry prompt takes slot 7 (RS 6.9, LE 6.15, XI 4.9.9).
- CC-60: Trivial DH fixes.
  - 27 Companions: answered by CMP 7.6/8.3.
  - 5.8 item 10: the ladder also adds kind from M3 (RT-21).
  - 11.2: "Applied by the DH owner; ThresholdDipRate still reported". Also note it in BR 14.1 item 4.
  - 17.1: attribute the tier 2+ clause to HZ-25 (hazards Q4).

**CL**
- CC-61: CL 8.6. CL-1 is applied and T6 is at M2. CL 7.3: `challenge` → `squareUp`. CL 19: CL-10 hosted by FL 6.7 (FL-10) and EV-14; seedTarget realised by EP 6.17.

**VG**
- CC-62: VG 3.4, 6.4 rule 6 and 6.6. Two TR-10 points:
  - A successful contact-hazard or harm-square save is an obstacle stop, so it counts as stopped early.
  - The purely verb-sourced contact push is a second named collision exception.
- CC-63: VG 7.5, 7.7 and 8.2. Against a Covered attack, a +2 fill is not offered and cover applies instead (TR-2). The 13.3 reports count only offered fills.
- CC-64: VG 18. Record the pending LP, IR and SV asks: ignoresWindfall, classLanes and subclassLanes, and the conditional SV-22 novice script. Mark companions, the generation hooks and the sister class docs as answered.
- CC-65: Trivial VG fixes.
  - 7.6 standOnBreath → stand.free.
  - 11, 13 and the risks row: M1 reports at levels 1–3 only, with level 4 moved to M2.
  - Naming pass: Upend vs Upended. Rename BR's boss state (for example "Floored") after a whole-word grep.

**BR**
- CC-66: BR 4.7 Vanguard row. Brink fires beside a harm square, never beside a contact hazard (TR-10).
- CC-67: BR 5.4 Weaver Device → one handle with 3 charges, cutting one lane per use (BR 4.7.1, V17s f).
- CC-68: GS4 stamp (merged BR / ROOM / SV).
  - The ROOM and BR owners confirm SV-9 now, and all three docs drop the pending text.
  - Until then, make BR 4.1.3, ROOM 6.2.5 and the SV 4.4 rows conditional, and move "stamp at GS4" in BR 4.17 and 11 to pending.
- CC-69: BR-16 extension. RS 8.4 "horn" → "View Halloo". RS 8.8 and RS 13 "cage" → "Menagerie Wagon (wagon bolt)".
- CC-70: BR 10 M1 row. List the V17 M1 rows explicitly: (a)–(c) on Old Ragged's kit, (f) root-column Deny, the Den Guise stamp proof, V17g.StampProven at AP1, start zones and lanes. Point SV 5.2 and RS 11.2 at that list.
- CC-71: Trivial BR fixes.
  - 0.1, 4.2.5, 11 and BR-31: "party cooldowns" → "the party's fight pools and once-per-fight counters".
  - 11 and 17 owner tables: add BR-5, 8, 10, 13, 19, 41, 29 and 40.
  - BR-39 wave → M1 subset/M2/M3.
  - 4.12: add C2.
  - 5.5 Chandelier: state whether Expose's Prone applies.

**SH**
- CC-72: Floor-pacing lever (merged SH / ARC). The request is answered by the Antechamber Catch-up (RS 9.4, RT-8) (RS).
  - SH 10.4, 10.7, 12, 15, 18 and 19.2: close it, keeping the level-2 arrival rate only as a check that the catch-up fires.
  - ARC 11.8, 5.4 item 4 and 19: lever 5 is in force, so the levers are run out after 1–4.
- CC-73: SH 5.4, 8.3 heading and staging, and 7.1. The Swordsworn ships in M2 (TL-37).
- CC-74: SH 18 and 13.3. Record EP's PartySurpriseShare input request (EP 6.8). Companions bullet: answered by CMP 8.3/11.3. 15 trap risk: reword to trap density.

**ARC**
- CC-75: ARC 8.10 classTags. Set the final values from HR 8.8 (HR-24) and close the ARC 16 and 19 Herald items.
- CC-76: ARC 7.2, 9.1 and 11.1. A failed contact-hazard save from Gust or Reel deals the typed collision band 2/3/3. It is environment damage, counted in the at-will rider bound (TR-10, agreed Q3).
- CC-77: Trivial ARC fixes.
  - 15, 16 and 19: mark ARC-14, ARC-17, BN-1 and the hazard-edge rules as written. "No document commits the generator" → KP2.
  - 8.1: add pre-proven Stir states (HZ-18).
  - 11.9: L9 and L10 → 15 and 16 (illustr.).

**HR**
- CC-78: HR 3.1 → "morale H P; control A P, H s" (CL 6.4). HR 10.5: add the floor key (F-KEY) to the categories (TL-23).

**CMP**
- CC-79: CMP 11.5 test. The hero arrives at camp 1 at level 3 (RT-8). CMP 7.4: add the travel and noticing terms, or point to FL 11.4 (XI-8).
- CC-80: CMP 11.1. Add lootLanes[] to CompanionKit. In CMP 17, record IR's kit-lane and Deeds asks (IR 4.3, 6.1).
- CC-81: Trivial CMP fixes.
  - 16 and 17 "same list" wording.
  - 7.2 keying → per SV 4.3 (SV-31).
  - 7.10 → "no Pledge source on floor 1", and 7.12 → floors 2–4 for all sources (EV 4.6).
  - TL 6.17: "CMP 6.16" → "CMP 8.6".

**RS**
- CC-82: RS 8.10, 12 and 19.2. Remove the RT-13 declined/deferred branches (the client accepted it).
- CC-83: RS 6.4 and 11.1. party.cap → recruitSchedule.companionCap.
- CC-84: RS 11.1 and SV 4.6. Define the two fields (MAIN 4.7 decided, "kind, not stats"):
  - difficulty.tier drives ladder%.
  - nightmareTier (M3) drives the kind additions only and never changes ladder%.
  - Name the read field in ENM 10.1, HZ 6.19, BR 4.14.2 and EV 4.9.
- CC-85: RS 11.2 V-table. V13 adds "(o) M3", V14 adds "(m) Later", V16 → "M1 stub, M2, Sequel M3, Ripening M3-late or M4" (EV-28). Apply the same in SV 5.2 and make the SV 7 M3 Ripening items conditional.
- CC-86: RS 12 M1. Tall and Low pieces as blockers only, per ROOM 12, through a new TR change row. Reword ROOM 6.11's reading to match.
- CC-87: Trivial RS fixes.
  - 9.1: TL-34 is accepted (EV-30). Also TL 17: drop TL-34 from the pending list.
  - 8.4, 8.7 and 8.9, and IR 4.12/14.2: segment 9 supports, and only the RS owner rules.
  - 2 and 14: add genVersion to the reproducibility key.
  - 12 and 18 owed lists: mark the applied RT rows as applied.

**FL**
- CC-88: FL 6.7. On floor 1 the Bypass rejoins at room 3, which is on the Circuit. FL 6.6: hooded-cot seeds have no pair (EP-13). FL 12: minimap data moves to M1 (XI-7).
- CC-89: Trivial FL fixes.
  - 6.8: pending EP-23 answers FL-21 (d).
  - EP 6.12 lever 4: F4–5 → F2–3.
  - 6.11: EV-2 reading recorded in RS 9.4.
  - 13: AP1 and AP2.
  - 6.8: 310 is binding (RT-7).
  - 9.2: spell out n and k.

**ROOM**
- CC-90: ROOM 6.5.2 and 6.10. The area cap is one shared counter across contact, harm and Void squares (HZ-9).
- CC-91: ROOM 6.2.5 and 11.2. Add the clock and void layer tags and their eligibility (HZ supplies the rule). Tagged slots never take a cluttered layer.
- CC-92: HZ 6.2.2. "May host the signature Stir when the reserved slot falls on it", with no preference.
- CC-93: Trivial ROOM fixes.
  - 13 and 18: add taken-up notes for segments 4–9.
  - 6.5.7: band per HZ 6.4 (contact or harm band, typed by skin).

**HZ**
- CC-94: HZ VP7 and 11.3. Add "no contact hazard within 3 of a boss lane" (BR-1).
- CC-95: HZ 6.4 Downed row. "Moved only by an ally's grapple-drag, which stops before a Void" (DH 11.4).
- CC-96: Redefine DownedAfterDreamfall as party units reaching 0 HP within one round of placement. Rename it ZeroHpAfterDreamfall and apply in HZ 6.4, 11, 11.4 and SV.
- CC-97: Plunge is M3. Fix SV 4.3, SV 7 M2/M3, HZ 11.2 line 937 and MAIN 6.3 (merged with CC-13).
- CC-98: Trivial HZ fixes.
  - 6.1: Kennel gate → pen bar.
  - Acid geyser → Acid Gland (6.1, 13).
  - 17: add HZ-23 to the request list.
  - HZ-11 and HZ-18: "RT 6.11" → "ROOM 6.11".

**TL**
- CC-99: Trivial TL fixes.
  - 0.1, 7.4 and 19.1 Tattle: "a Crier until the Annex (M3); always on F1".
  - 6.1 Curiosity key → h:curiosity (SV-6).

**EP**
- CC-100: EP 6.5. Remove the Gauntlet from Dormant guard posts (ENM 17.4).
- CC-101: EP 6.1 and 3.4. floorSeed = floor key dungeon/floor/{N}, and encounters is under the attempt node (SV-1, SV-31).
- CC-102: EP 9.2 LandingShooters. Add "Waylay on F2: 1".
- CC-103: EP-12. Extend it to the 6.20 surfaces as a request to RS RC5+.
- CC-104: EP 6.3. Every solo-table cell lands inside the ENM 10.1 window and at or above fillTarget. State minion levels and re-tune the illustrative bands.
- CC-105: EP 6.20 and DH 17.11, in the EP-16 text. Groups last seen Alerted, Searching or Pursuing show "last seen <state> at tick N; route unknown" and no forecast (XI 4.8.1).
- CC-106: Trivial EP fixes.
  - Qualify the XI Q2 and BR Q4 citations.
  - Move risk row 18 below row 17.
  - "start start shape" → "start shape".

**LE**
- CC-107: LE 16. Mark the signature VR exemption withdrawn (IR-13). The tier-4 scroll lever is engaged at 1 by default (IR-16).
- CC-108: LE 6.10 item 2 and 18.2. Retire the SV-5 refusal fallback. SV 14.2 and 4.3: close it too.
- CC-109: LE 6.12. Shared seeds (M2) do not fix the hero, and daily seeds (Later) do (SV 4.7).
- CC-110: Trivial LE fixes.
  - 10.1: one data key, loot.levers.dueSidegradeFromFloor.
  - Reorder risk rows 11–14.

**IR**
- CC-111: IR 4.13 and 4.17. The boss-channel dummy is retired. Frozen order: containers, carried, camp, as keyed blocks (SV-5).
- CC-112: Trivial IR fixes.
  - 4.18: lair = arena is confirmed (BR 4.11.1).
  - 14.2 and 15: AR, TL and ITEMS rows "Not triggered: applied".
  - 5.2: Vellum Gambeson is guarded.
  - 6.2: second forms "about 5".

**LP**
- CC-113: LP 0.1, 2.1 and 13. "RT 0.1 item 5 / RT Q5" → ROOM. Add LP to SV-31's list.
- CC-114: LP 0.1 and 14.1 item 17. Windfall key → Derive(lootPlan, "windfall", 0) (SV-1).
- CC-115: Trivial LP fixes.
  - "Treasure strip" → "Treasure field" in live text.
  - 0.1 LP-16..20: "fixes requested (pending)".
  - Q → "empty or special".
  - 4.6: log drawer toast.

**EV**
- CC-116: EV 4.6, 14, 11 and 17. Cut 1 is answered as graded (BR-26).
- CC-117: EV 4.2, 9.3, 4.5.4, 14.2 and risk 3. F1CuriosityPresence = 1000‰ under F1HostAlways, and a shortfall is a defect (SV-30).
- CC-118: EV 4.5.5. Board highlight while the card is open, and the full map shows the facts (XI 5).
- CC-119: EV 9.1. pendingMemory → PendingProfileWrite{curiosityLast} in pendingProfile[]. The recentRuns entry is pushed at run start (WO1) and completed at end or abandon. SV WO6: add the completion.
- CC-120: Trivial EV fixes.
  - 14.2: drop "H5 becomes F-ROOM".
  - Table A: relabel Q1-B as a bound.
  - Qualify XI Q2 and Q5.
  - 4.13 and 11: answered notes.
  - V16s ThemeWrongDetail: all floors.
  - Q → "empty or special".

**XI**
- CC-121: XI 4.12. Line 849 → base plus journal. Fix the "rewritten after every command" summaries in 0.3, 12, 15 and 16, and in the XI-28 row. Bare "Q4" → "SV Q4".
- CC-122: XI 4.3.5 and 4.6.3. V18r for BubbleNeverPositioned and ChalkNeverStub (SV-20, merged).
- CC-123: XI 14.1 item 4. Entered on any completed step (4.1.1).
- CC-124: XI 4.7 and 9.1. Re-sight narrowing is pending with the FL owner. stopOnResight defaults to true until it is ruled.
- CC-125: XI 4.10. Add the Floor-info binding: F, Menu (Start).
- CC-126: Trivial XI fixes.
  - LE 6.9 → 6.15 (two places).
  - LP 4.x → LP 3.3, 7.
  - FactGained → FactDrawn.
  - 2.1: tense fix.

**SV**
- CC-127: SV 6.2, 6.9 PR1 and 12. PatrolFightRate is an agreed gate with an illustrative 60% owned by EP, not a client number.
- CC-128: SV 5.2 single-owner table. EV 9.2, IR 9.2 (h) and RS 11.2 V9 mark the shared checks as calls to their owners (SV-20).
- CC-129: SV 4.6. streakRedrawMax 8 is illustrative and launcher-only (RS 11.1).
- CC-130: SV 4.9 build tag. The rules character hashes rulesVersion and the rules-relevant manifestHash.
- CC-131: SV 8, 7 M3 and 15 risk 3. Two Doors re-tests camps 1–2 (and gate 7) in M3. Camp 3 is a separate M3 extension once the floor-4 pool is 3 or more (RS 6.11, RS 12).
- CC-132: Trivial SV fix. Line 24: DS1–DS15 and WO1–WO8.

## 4. Told plainly

- **The MAIN legend misstates your decisions (CC-1).** A find-and-replace at approval turned "proposal" into "decided" inside the legend itself. Read literally, the legend says every decided rule is unconfirmed. Your decisions were never in doubt. This is a typo-level fix that restores the approved meaning.
- **A lead's call contradicts your agreed rule (Q-1).** The Ticket lock spends coin on a floor, against your "coin is spent only at camp". Your ruling is needed.
- **Your agreed rule on saves across patches wins (CC-3).** You agreed that a save carries across a patch (SV Q3). The items design still said the opposite (a lead's call), and the items design now yields.
- **A ship gate you agreed has an owner who did not know (CC-7).** The hazards design made "a Shade hazard hook" one of your agreed Void ship gates (hazards Q2). The Shade design never recorded the request, so Voids cannot ship until the Shade owner delivers it. It is now recorded as pending.
- **One gate could never fail (CC-96).** DownedAfterDreamfall counted heroes Downed after a fall. Under your rule that the hero has no Downed state, it always read 0. It now counts any party unit reaching 0 HP within one round of the fall. The bar stays at 0 on the solo row.
- **One number was overstated as yours (CC-127).** The seeds design listed PatrolFightRate 60% as a client number. You agreed the gate. The 60% is illustrative and owned by encounter placement.
- **One agreed wave option was dropped (CC-85).** The seeds and run-structure checklists put Ripening in M3. You agreed "M3-late or M4", and the M3 sign-off is now conditional so the M4 option survives.
- **One settled rule was misquoted (CC-95).** The hazards design said a Downed body is "never moved". The settled rule is that only an ally's grapple-drag moves it. No rule changes.

## 5. Not changed

- **Historical text stays.** Change-log rows, appendix and critique resolutions, and EP-1, FL "lead's calls after the check" item 5, SH D9 and SH-16 are history.
- **Some pending requests are recorded on both sides and stay as they are:**
  - BR-6, BR-7 and BR-13 with the enemies owner;
  - TL-43 (horn tripwire);
  - EP-16 (only its display wording is fixed in CC-105);
  - LP-11, HZ-23, TR-13, RT-14 to RT-16, the IR-12 RS-owner ruling, and LE's dealShuffle and V13g (f) requests.
- **Already-correct pieces in the stale bullets stay:**
  - CN 10 "fixed budget never has a hole": the VP is conserved in the camp channel.
  - The SH 18 stage-2 bullet is still true.
  - ARC "No Binder record carries damage.*": contact damage is environment damage.
  - The VG level-9 envelope: cover is suppressed under Raise.
  - IR's Weaver carriage cell resolves to "carried" under IR 624.
  - CMP's "owed by other owners" lists keeping applied rows is the corpus convention (RS does the same). Only the "same list" wording is fixed.
- **Illustrative numbers that differ but are not binding stay:** FL and EP budgets, the ARC cast counts (fixed only for internal arithmetic), and the LE loot rates under MAIN 10 "All numbers".
- **Pointer-level fixes need no rule decision:** ENM 10.1's flat lair floor and other pointer-backed summaries (CC-36 and similar) are clarifications only.
- **STAT 6 preamble "exact values left to the builders"** is generic, and its HP and Recovery rows already point to DH.
- **RS 9.4 and BR need no edits** for CC-72 and CC-116. They are authoritative, and only the restating docs change.
## 6. Client ruling (2026-11-23)

- **Q-1: option A (agreed 2026-11-23, cross-document comparison Q-1).** Coin may be spent only at camp, at a Ticket lock (M3), and, Later, at a wandering trader. ITEMS 10.1 is amended; the Ticket lock joins the coin-change event sources; the EV and LE restatements are corrected. CC-8 is applied as option A.
- The lead's resolutions in 2 and 3 (lead's calls) and the told-plainly items in 4 were presented to the client with Q-1 and applied on 2026-11-23.
