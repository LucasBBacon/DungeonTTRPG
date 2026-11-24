# Naming Pass: Design (the client naming pass; the future item named by all 34 approved designs)

Date: 2026-11-24
Status: **Draft for the client.** Six questions (Q1–Q6, 14). Nothing here is approved yet.
Parents (immediate): every approved specification in `docs/superpowers/specs/` (34 documents) and `docs/superpowers/reviews/2026-11-23-cross-document-comparison.md` (reviews, "CC"). House style and template: `2026-11-24-charm-abilities-design.md` ("CA", segment 16, approved 2026-11-24, **the most recent template**). Abbreviations as in CA's front matter: MAIN, STAT, ABL, CON, ENM, ITEMS, W, AR, CN, DH, CL, TK, VG, SH, ARC, HR, CMP, RS (its change ids are RT-n), FL, RT (room and terrain; CA calls it "ROOM"), HZ, TL, EP, LE, IR, LP, EV, BR, XI, SV, CH, SK, HL, CA.

All 34 approved documents and the review were read for this pass through a harvest of **every player-facing name** (2,893 harvested entries, 2,769 distinct strings; 8.1), every naming-pass commitment and flag (205 mentions of "naming pass" across 27 files), and a cross-registry collision sweep (about 250 collision rows; 2.3). Every final name in 5 was grepped case-insensitively, as a whole word, across `docs/superpowers` (specs and reviews) while writing, 2026-11-24, and the **sites of every current name** in 5 were produced by the same script, not by hand (A.1, completeness critic). **This document edits no parent document** (13).

**Scope (settled; the corpus).** The naming pass decides **player-facing names only**: display strings, labels, buttons, chips, notices, fallbacks, Plain-names labels and every `localeTermOverride`. Internal ids, field names and code identifiers are never renamed unless they reach the screen (rule N-Q, 4.7); the id-to-display map lives in one string table, so applying this pass changes data and text, not code. Where an id does reach the screen today (a greyed reason printed raw, a Grace id, a behaviour-profile name on a hover), this pass rules it **internal** and supplies a display string instead.

Markings follow CA:

| Marking | Meaning |
|---|---|
| **(settled)** | Restates a rule from an approved document |
| **(decided)** | A main-design client decision, restated |
| **(agreed)** | A client answer recorded in an approved document, named with its document ("CH Q1", "HL Q1"), restated; this document's own questions are Q1–Q6 |
| **(lead's call)** | Filled in by the design lead inside approved rules; binding once the client approves this document |
| **(new lead's call)** | A lead's call made while writing, beyond the group proposals; listed in 14.2 |
| **(lead's call, after the check)** | Made in answer to the design check (tone, clarity, completeness critics; A.1); never overrides a client answer |
| **(change NP-n)** | Amends an approved document; listed with its exact sections in 13. Rows that touch **settled**, **decided** or **agreed** text, or a client-confirmed name, are flagged ⚑ |
| **(told plainly)** | Narrows or re-reads approved text, contests the brief, an approved document or a critic, or states an unwelcome forecast; listed in 14.2 |
| **(request)** | Asks another owner to rule; changes nothing until that owner rules; the fallback is stated |
| **(hook)** | Owned by another design; this design states only what it needs |
| *(illustr.)* | Illustrative; never binding |

Rename types used in 5: **M** mechanical (a placeholder, an unclear name or a restatement becomes final; the doc owner applies it once this document is approved); **C** client (the current name is approved-final, client-agreed or client-confirmed, or the choice is a weighty taste call; the client decides it in 14); **keep**; **keep (waiver)** (kept with a recorded waiver, 9.3); **internal** (never printed); **retired** (registered so it cannot return).

**The brief.** The naming pass is a separate client item that every approved design defers to ("Names are placeholders pending the naming pass"). Standing guidance: not an exact 5E port; the run is a dream and the premise stays uncommitted (RS 7.5); Grim Surreal tone; **combat is key**; contest the client or approved views where warranted.

**Change prefix.** This design uses **NP-n**. A grep for `\bNP-[0-9]+` across `docs/` (34 approved documents and the review) finds **0 hits** (re-run while writing, 2026-11-24).

**Validator.** **V24 `NamingPass`** is free: a grep for `\bV24\b` finds **0 hits** (re-run while writing). V1–V7 belong to RS, V8 FL, V9 RT, V10 HZ, V11 TL, V12 EP, V13 LE, V14 IR, V15 LP, V16 EV, V17 BR, V18 XI, V19 SV, V20 CH, V21 SK, V22 HL and V23 CA. V24 joins the consolidated suite only through **NP-1** (RS 11.2 suite table; SV 5.2 becomes V1–V24), as V23 did through CA-18. `V7.NameRegistry` stays the registry (RS 11.2, SV-20, settled); V24 is the rule set that runs on it (9).

---

## 0. At a glance

- **What the pass decides.** One final player-facing name for every placeholder in the corpus, one ruling for every flag the 34 documents sent here (2.2), and the standing rules (4) and machinery (9) that keep later content inside those rules. **2,893 harvested entries** resolve as: **282 renamed** (including client-decided ones), **2,509 final as written**, **65 kept with a recorded waiver**, **35 ruled internal only**, **2 retired** (8.1; Appendix B lists each). The register in 5 has **329 rows**: 263 mechanical renames, 58 client renames, and the keeps, waivers and retirements that needed a ruling.
- **Most names stand.** The world the client approved is specific and good. 87% of harvested names become final unchanged. Renames are spent where a rule is broken: SRD or another game's name, a real sacrament or slur, a premise leak, a name that lies (RC9), or a real collision a player would meet on the same screen.
- **The style guide in brief (4).** Two layers: a closed, plain **keyword layer** carries every rule; an open **flavour layer** never carries a rule a keyword does not also state on the same card. One register per category. Names may be whimsical but never lie. One word, one on-screen meaning. No D&D brand, no SRD name doing its SRD job, no exact match to another game. Premise words (dream, sleep, wake, nightmare, bed) only under one logged waiver list. No string is assembled at runtime. Upgrades keep their name. Every new name runs the Name Gate (4.10).
- **The biggest renames.**
  - *Legal and outside IP:* Fireball → **Detonation (fire)**, Scorching Rays → **Rays (fire)**, Blight → **Wilt**, Stoneskin Draught → **Slate Draught**, Holy Water → **Lamp Water**, Antitoxin → **Bitter Draught**, Second Wind → **Dig In**, Surge → **Onslaught**, Sculpt → **Whittle**, Overwatch → **Ready Shot**, Volley → **Arrowfall**, Steady Aim → **Settled Aim**, Bull Rush → **Bowl Over**, Dreamfall → **Freefall**, Night-Mare → **Pale Mare**, Restricted Section → **The Chained Shelves**, Keepsakes → **Souvenirs**, the D&D 4e role set broken (Artillery → **Marksman**, Controller → **Hinderer**, Minion role → **Swarmer**).
  - *Sensitivity and premise:* Viaticum → **Parting Fare**, Last Rites → **Final Respects**, Totem → **Tipstaff**, Coffle → **Lashed Pair**, Lockjaw → **Jawbone**, Gout → **Deluge**, Lame → **Limping**, "Hang it?" → **"Shelve it?"**, Bedside Stand → **the Washstand**, Nightmare tiers → **Peril tiers**, Middle Dream → **Midwater**.
  - *Collisions a player meets:* Stairhead → **the Downstair** (Stairtop stays), Wary → **Watchful**, Rooted → **Tangled**, the printed word "catch" removed everywhere (TL family **Clasp**, Catch Breath → **Respite**, Caught → **Trapped**, Catcher → **Courier**, lane wording "Charms **hold** moments"), Keeper's Cart → **the Pedlar's Cart**, arena Device → **Counterpiece**, Cross-Reference (boss) → **Footnote**, Spur → **Urge**, Interject → **Speak Over**, Hold the Line → **Bar the Way**.
- **What needs the client (14).** Six questions: **Q1** the premise-word waiver list (W-DREAM-1); **Q2** three sensitivity renames inside agreed or approved text (Viaticum, Bedside Stand, Totem); **Q3** the Team Cherry cluster (Silk Archive → Cobweb Archive; Hollowfolk → the Fireside); **Q4** the difficulty-ladder name and MAIN's "boon" wording (Peril tiers; "offers"); **Q5** one bundle of approved rules names renamed for SRD, IP or collision; **Q6** the class names, the hub name and the title. Every other row is mechanical.
- **What it adds.** V24 `NamingPass` (9.3), eight data files (srd names and allowlist, IP floor, echo list, sensitivity list, gazetteer, reserved premise words, retired names; 9.1), a closed **Rules Glossary** of 60 keywords at M2 (6.1), the **Plain names** setting extended from curses to charms, trinket lines, affixes and boss banners (6.4), and translator notes on every name (7).
- **Timing (10).** Mechanical finals apply before any record is written (M2 records gate); CMP-27 and SK-13 close now; the charm display word is ruled now, ahead of the M3 card-text freeze CA set; the Malady sensitivity read and the locale passes stay at M4.

### 0.1 The lead's answers in plain words (lead's call)

1. **What does the naming pass decide?** Every name a player can read. It does not touch code ids. For each of the 2,893 harvested names it gives one outcome (Appendix B), and for each flag the documents raised it gives one ruling (2.2). It also fixes the rules that later names must pass (4) and the check that enforces them (9), so the corpus does not drift again (RS risk 13).
2. **The style guide in one paragraph.** Rules travel in plain keywords (Bleeding, Warded, Resistant, Shove); flavour names (Hangnail, the Jailer of Hours, Nine Rivets) are labels that may hint but never lie. Each category has one register: classes are occupations, conditions are one plain adjective, items are an object plus one displaced adjective, bosses are titles, places are plain nouns made strange, curses and Curiosities may be whimsical because nobody reads them under combat pressure. Tone comes from voice (barks, Whence lines, Reveries), not from odd spellings. Dream, sleep and wake words stay off names unless the client keeps them on a short waiver list (Q1).
3. **The biggest renames, and why.** Twenty-odd names copy SRD spells or features doing the same job, or another game's title (Fireball, Second Wind, Overwatch, Dreamfall); four use real sacraments or a slavery term (Viaticum, Last Rites, Holy Water, Coffle); three are living illnesses or an ableist word (Lockjaw, Gout, Lame); one is a living people's sacred word (Totem). These are must-fix. Then the collisions a player would actually meet on one screen: Wary beside Weary, Rooted beside Routed, Stairhead beside Stairtop, a vendor called Keeper beside "keepers of the sleep", "catch" printed on cards that CA bans it from.
4. **What stays.** The four class names, the Stairtop, the theme names except the Silk Archive, the boss names, the Hollow and Last Hollow (camp scope only), the Book of Nights, Oddment and "Leave it by the fire", Reprieve Phial, Starting gear (final; SK-13 closes), the Wake theme and the Dreamer's Heart (under Q1).
5. **What needs the client.** Renames of names the client agreed or that approved documents fixed as final, and the taste calls: premise words (Q1), the three sensitivity renames in agreed text (Q2), the Hollow/Silk cluster (Q3), the difficulty name (Q4), one bundle of approved rules names (Q5), and the class names and title (Q6). Each has a recommended option A.
6. **What the pass will not do.** It does not rename code. It does not pick a game title (none is in the corpus; the store title needs a trademark clearance search, which is a legal step, 15 R11). It does not reopen agreed mechanics; where a name lied, it renames the name, except two places where the rule itself should change, which are requests to the owner (the Treadwheel stove, 5.5; Wayfarer's Belt keeps its effect).

### 0.2 Decisions in brief

| Topic | Decision | Where |
|---|---|---|
| Two layers | Closed keyword layer (60 at M2) carries every rule; flavour names are labels | 4.1, 6.1 |
| Registers | One register per category, with caps | 4.2 |
| Never lie (RC9) | Blind-guess test; keyword-in-flavour only if the thing applies that keyword | 4.4 |
| SRD | Function test (HOUSE-SRD-2), an explicit allowlist (conditions, rarities, six masteries, base families, actions, Extra Attack, Concentration, damage types) | 4.5 |
| Outside IP | Echo list; exact matches fail; clusters of one studio must be broken | 4.5 |
| Sensitivity | Blocklist outranks NR1; sacraments, slurs, living diagnoses, living sacred practices, hanging words fail | 4.6 |
| Premise words | Reserved; one waiver list W-DREAM-1 (Q1) | 4.7 |
| One word, one meaning | Signature, Dormant, Plate, tick, kit, boon, Night, Catch each get one meaning | 4.8 |
| Internal-only families | Behaviour profiles, encounter templates, Stir parts, socket classes, EP catalogue titles, Grace ids, payoff kinds, Reverie label | 4.8 |
| Head-word cap | At most two live names per category share a head word | 4.9 |
| Name Gate | G1–G14 before any record | 4.10 |
| Validator | V24 `NamingPass` on `V7.NameRegistry` | 9 |
| Catch | Never a keyword or a name anywhere; ruled now for CA | 5.3, NP-14 |
| Stairtop / Stairhead | Stairtop stays; Stairhead → the Downstair | 5.6 |

---

## 1. Survey

**What the corpus already has.** Thirty documents carry the same front-matter line ("Names are placeholders until the client naming pass"), each with its own grep-checked new names and an "avoided because taken" table. That discipline worked inside each document and failed across them: each grep ran against the documents approved before it, so later documents collide with earlier ones in ways the earlier ones could not see (Stairtop and Stairhead; Keeper's Cart and "keepers of the sleep"; the Lullabist's Lullaby and the boss The Lullaby), and several fixes in one document re-broke another (the Catch family survived TL A.5 and was then banned by CA V23s). Classes 10 `NameCollision`, AR 17.1 rule 13, TK 15, V14 (f), V20s, V21 (g), V22 (e) and V23s each check a slice; none checks the SRD lists, other games, or the sensitivity lists as data (research brief, constraints G).

**What other games teach (research; the games named in the corpus's own references).**
- *Two layers.* Slay the Spire (a closed bold keyword set with tooltips), Into the Breach (function-named mechs, one-word statuses) and Hades (the slot noun: Strike, Flourish; flavour in the modifier) keep rules in plain words and flavour in names. This is the model for 4.1.
- *One register per category.* Darkest Dungeon (occupations for classes, "The X" bosses, plain afflictions), Outer Wilds (one naming source per culture: minerals for Hearthians, plants for Nomai). This is the model for 4.2 and the Fireside's trade-noun rule.
- *Hint truthfully.* Hollow Knight charms, Inscryption sigils. Where names only hint (Bloodborne runes, Sunless Sea stats), players need tooltips. This is RC9.
- *Upgrades keep their name.* Slay the Spire's "+" works; Hollow Knight's spell renames confuse.
- *Every one of them has a glossary* (Codex, Hunter's Journal, Ship Log, Pentiment's marked terms). This is 6.
- *Where they trip.* Real-world leaks (Slay the Spire's Lagavulin, Disco Elysium's Inland Empire), unpronounceable spellings (Yahar'gul), strings assembled in code (Caves of Qud). This is 4.6, N-G and 7.
- *Premise words.* Hunter's Dream, the Dream Nail and the Dreamers commit their games to a premise; Outer Wilds stays oblique for a long time. RS 7.5 wants the oblique route (4.7).

**What the law and the house rules allow (research, constraints; not legal advice).** SRD 5.1 and 5.2 are CC-BY-4.0 with attribution; CC-BY grants no trademark rights, so "D&D", WotC, setting brands and Product Identity names stay out (LEGAL-1, LEGAL-3). The house layer goes further than the law: RS 7.6 and HL-30 already ban SRD spell names and SRD and 5E class and subclass feature names on new display names (agreed RS Q3, HL Q1). The corpus also adopts SRD words on purpose (conditions, rarity tiers, six masteries, base weapon and armour families). A mechanical ban would fail the corpus's own vocabulary, so the pass needs a function test and an allowlist (4.5).

---

## 2. Audit

### 2.1 What the approved documents commit the naming pass to (settled)

Every document lists the pass as a future item that comes back to the client (CH, HL and SK mark it "Scheduled"; BR, EV, IR, LP, XI, SV give its owner as "Client"). The rows below are every commitment, with the flags each document sent and where this document answers it.

| Doc | Commitment (site) | Flags sent to the pass | Answered |
|---|---|---|---|
| MAIN | No "naming pass" text; cites other designs' names with change ids | "boon" used for 4.5 level-up offers and 4.7 starting boon; 4.7 "a simple camp screen"; "background perk" (RS 7.6); "Dormant" vs Asleep (CON 5.5); the visible difficulty setting and telemetry consent have no names yet (4.7, 6.6); Last Hollow / the Hollow / "the one hollow that does not re-form" | Q4; 5.9; NP-29; 6.1 (Easier lamp, "Share play data"); 5.6 Q3 |
| STAT | 8.1 "Names are placeholders; theming comes later (agreed)" for ten damage types and three families; 5.1/13 Presence and species renames kept | Mastery signature names left to builders except Tactician; Second Nature (TL-36) open; Elemental vs Elementalist; Resistant vs Resisting; Bleed/Corrodes/Slows vs conditions; Mastery vs SRD Weapon Mastery | 4.5 allowlist (damage types kept, theming still open); 5.3 rider wording; 5.4 Old Hand; Q5 Resisting display |
| ABL | 2 and 17 "Protected content" check before shipping; 18 names left to builders; 6.5 Slot Recovery named apart from Recovery | Second Wind, Extra Attack, Dash, Disengage, Dodge, Help, Hide, Search, Opportunity attack, Surge; "Hear out" vs Listen (CH-17); "signature" in three systems | 4.5 allowlist; 5.1 Dig In, Onslaught; Q5 Slot Renewal; 4.8 Signature |
| CON | 22 Risks: generic names checked, Dazed kept with Addled reserved; every SRD spell shape attributed; 18.11 rename history; C15 asleep → dormant; "low" not "critical"; EV-11 `arenaSeal` id | Rooted vs Routed (unflagged); Raging/Reckless (unflagged); Lame (unflagged) | 5.3, Q5; Addled reserved (9.1 retired list) |
| ENM | "No enemy names or lore are proposed here"; 22 names matching other games checked; 23 trait list, affix names, enemy names and "the final name of Tactician" to builders; 17.5 Upended restatement pending (CC-65) | Protector ×3; Command ×4; Signature; Charger, Sentinel (PHB feats); Relentless, Blurred, Vampiric, Keen (SRD) | 5.1, 5.4 affixes; 4.8 profiles internal; NP-6, NP-10 |
| ITEMS | 10.6 SK-13 (M2): the hero starting-gear term; 8.2 fit line; 10.9 Tend; 10.2 Sell/Scrap; 12.5 SRD rarity names; intro: illustrative names only | Embercleaver (unflagged) | SK-13 closed (Starting gear final); 5.1 |
| W | Header SRD check; 7.4 Set not Brace, "verb" never "mastery"; 18.6 Vowkeeper; 18.5 Snap Casting; 17.11 cut names; 24 notables to builders | Shatter, Thunderclap, Steady Aim, Volley, Spare, Set, Warding/Wardwood, Doorwarden/Corridor Warden, Honed ×2, Opener, Skirmisher's Spear (all unflagged) | 5.1, 5.3, 5.8; Q5 |
| AR | 17.1 rule 13 validation; App. A roster renames; SRD names checked; Last Ward retired | "Catch" (CA pointer); Bull Rush, Hide family vs Hide action, Hailguard/Hailward, Warding Lining, Deep Stance, "-warden" notables (unflagged) | 5.1, 5.4, 5.8; 4.8 ("Hide armour", "Plate armour") |
| TK | 5.6 "'Token' is reviewed by the naming pass"; 15 names carry the verb; 13 catalogue names illustrative; 21 family names to builders | Steadfast, Clearmind, Holder, Seeker, Ember, Vigil, "charm (none)", Hunter's Tally (all resolved in TK); "Catch" never printed | Token dropped as a display word (5.9); 5.8 |
| CN | 17 scroll and wand names follow the ability; 22 placeholder names; 24 protected-name check; 14 Reprieve Phial checked | Fireball, Blight (SRD verbatim); Scorching Rays, Antitoxin, Holy Water, Stoneskin; Wand names; Serrating/Serrated; Thunderstone (RC9) | 5.1, 5.4, 5.8 |
| DH | 7.1 "HP tier" a lead's name; 24 placeholder wording to builders; 15.3 RetiredDropPrevention; 26 Last Ward grep | "bloodied"; Second Wind and the Death-Ward-shaped spell (RS 7.6) | Q5 (hurt, sound); 5.1; the Death-Ward-shaped spell gets "Stay the Hand" when it is written (NP-37) |
| CL | Front matter: placeholders frozen, NameCollision; 10 four rest tools need distinct first words; 14, 19, 20.2 naming pass a client item; risk "placeholder names colour how the concepts feel" | Rename table (Volley camp ability unnamed until stage 2); HR-30 frozen names; Duck vs Dodging pending; Mending Word shares a scroll name on purpose | 5.4 rest tools (Dig In, Respite, Knit, Take Heart); Duck vs Dodging: ruled distinct (6.1 glossary entries differ, V24 r3 keyword pair test passes) |
| VG | Header placeholders frozen; Hold the Line to the pass unchanged; 18 NameCollision by V7.NameRegistry | Warden's Reach id; Called Out; Drive Through; the Form sequence (unflagged) | 5.4 Bar the Way; Warden's Reach never displayed (4.8) |
| SH | Preamble placeholders frozen; avoided table; 16 and 18 naming pass | Old Wound, Bad Footing, Blind Side, Trigger Shot not in the grep list; cited placeholders (Grey Lacing, Close Shot, Throwhand, Cased, Gap Sight, Knife Work) | Checked (A.2): all pass; 5.8 Hemp Lacing |
| ARC | Front matter placeholders, avoided table; ARC-1 Sculpt; 8.1 Template (family) | Fireball and Scorching Rays overrides; Sculpt Spells echo; Tempest; Holding; Coffle; Gout | 5.1, 5.2, 5.4; 4.5 rule N13 |
| HR | Front matter placeholders; Spur vs Spurred and Interject vs Interpose flagged; 9.5 never "Malady"; HL heirloom pending | Hobgoblin/Goblins and Brennan in examples; names outside the frozen list unchecked (Opening Cry, Waver, Pick the Question, Strike or Shove, Rush the Colours, Bring Them Home) | 5.4; A.2 checks; "Bring Them Home" undefined: deleted unless HR defines it (request, NP-38) |
| CMP | Front matter placeholders; avoided table; CMP-27 owed by the pass before records | Ward tag; Gap Sight vs Gap card (internal); Muster vs Muster Post; Deeds vs Deed bynames | NP-7; 5.9 Buffer; Gap card internal; Muster and Muster Post one meaning (both the recruiting surface); Deeds one pipeline (4.5 waiver W-11) |
| RS | 7.6 naming registers and rules (HL-13, HL-30) for the pass at M2; 7.4 tone-by-depth review; 8.0 avoided table; 15 risk 13; 18 every placeholder | Upended restatement (CC-65); Patina "charm" → "accent" (CA-13); tin-can tripwire; Lullaby ×4; Night-Mare; Figment; Swallow/Wail; Doppel; Oathbroken; Jailer/Head/Last head words; Frozen Footman | 5.2, 5.5; NP-6; CA-13 (hook, unchanged) |
| FL | Front matter placeholders; avoided table; 19.1 Arch client-confirmed | sluice gates; curtain skins; Sanctum; Two Wings ×2; the Ring family; The Last Hour | 5.6 |
| RT | Front matter placeholders; avoided table; 19.3 renames | Lane capitalised in 6.2.3 | 5.6 (lower case) |
| HZ | Front matter placeholders; "Cord" check; A.5 avoided list | Dreamer Turns Over (BR-30 tone flag); VP1–VP10 id collision (internal); "six names collide" lists eight; Lever, quake, swallow in prose | 5.2, 5.5; HZ count mismatch corrected (request, NP-16); "Cord" kept as the keyword, binding prose says "bindings" |
| TL | Front matter placeholders; A.5 "Iced" and "Catch" flagged | mimic in prose and `MimicPlacement`; Snare ×3; Rose Window ×3; Nursery ×2; Scent/Spore Bladder; Restricted Section | 5.3, 5.5; "mimic" never on screen (V24 r7); Scent Bladder → Scent Sack (NP-17) |
| EP | Front matter placeholders; A.5 renames; formation names player-facing (6.20) | Mousetrap; Sleeping Ward; Wake Order | 5.2, 5.5 |
| LE | Preamble placeholders; "Dream's Due" never "Due"; Memento caption on tone review | Wish, Tithe Bowl, Ferryman's Note, Blood Price unnamed hooks; Viaticum | 5.1 The Long Shot; Q2 Parting Fare; Tithe Bowl and The Boatman's Chit (Ferryman's Note) to the RS 7.4 tone review |
| IR | Preamble placeholders; Oxblood, Precentor, Carillon, Vellum, Swallowed boss-tied | Wayward; "Answers here" | 5.8, 5.9 |
| LP | Front matter placeholders; Alms Crab "stays a placeholder for the naming pass" | Treasure strip still in LP-28; Silverfish and Kennel fox role-ID passes | 5.2 Pew Crab; NP-22 |
| EV | Header placeholders; Seating Chart and Last Page kept as theme callbacks | Rent Due; Omen Retelling; three mirror cards; Left Luggage ×2; Last Page ×3; Visitors'/Petitions Slot; The Stair Count; The Lost Glove; Take a bow ×3; Night Usher ×2 | 5.5 EV rows; Omen Retelling kept (waiver W-15); The Lost Glove → The Odd Glove; Ca5's "Take a bow" → "Wave to the crowd" |
| BR | Header placeholders; Prompts "rename if it confuses"; Upended renamed Sprawled | Reading Hall, Probate Room, Mourner's Embrace, Read the Will, Clapper Swing, Cold Ring, Inkblot, Flex and Stamp, Bile Spit, Mouthful, Cell Zero Slam unchecked; See Also hits | 5.5; A.2 checks |
| XI | Header placeholders; XI-19 canonical names; "Upended" restatement pending | — | NP-6, NP-25 |
| SV | Seed and notice words owned by the pass (4.7, 4.9, 4.10.1, 5.5, 14.2) | "seeded" rejected as a field value | 5.9 run code family |
| CH | Every placeholder; A.2 flags (Keeper, Book of Nights, Seeded night, Arrival, Stairtop, coat rack, Memento wall, Lingerer, Kindler, Indexer, Barker, Sitter, Framer, Chalkwright, the Hollow) | "placeholder names the client confirmed are not renamed here" | 5.6, 5.7; Q3 |
| SK | SK-13 "kit"; Oddment; "Leave it by the fire"; A.2 flags (Iron Rim, Patched Twice, Waxed String, Copper Links, Green Bark, Boss Nail, usher, Gatesplitter); ribbon rosette tone look | — | 5.8; SK-13 closed |
| HL | Every placeholder; A.2 flags (Oath, Bequest, Not yet read, First Cut, Answer, Kneel, Thumb-Worn Totem, Set aside, Stair-Worn Byrnie, Gatesplitter); spares Sullens and Frets; T12 renames; second-look list | Jactitation NR1 checks pending | 5.2, 5.3, 5.8; 7.3 |
| CA | Every charm name, the Bobbin and the shape words; "Catch" design word first, before M3 card-text freeze; CA-12 token review; T5 renamed placeholders | Agreed Q1/Q4 text still records Ashen Knot and Underpillow; "Charms catch moments" | NP-14; 5.8; Q5 (lane wording) |
| CC | CC-65 Upend vs Upended | — | NP-6 |

### 2.2 Every flag, ruled

Every flag in 2.1 has exactly one ruling. The compact index (full rows in 5; the harvest status of each harvested name in Appendix B):

- **Explicitly flagged to the pass (86 harvested entries).** Renamed 49, retired 1, ruled internal 5, kept with a waiver 7, kept final 24 (Arch, Bossgate, Drape, Knit, Levy soldier, Oddment, "Leave it by the fire", Starting gear, Stairtop, Arrival, the Hollow, Book of Nights, Memento wall, the Newel, coat peg (prose only), the one-time version notice, and the boss-tied signatures, each with its reason in 5 or Appendix B).
- **"Catch" (CA F13, CA-12, TL A.5, AR 17.1 r13).** Ruled now: "catch" and "caught" are never a keyword or a name anywhere (4.8); the CA shape label stays an internal design label with no display word (the ◈ glyph is spoken "stored moment"); every printed occurrence is renamed (5.3). This closes CA 11's row "before M3 card-text freeze" ahead of time.
- **"Token" (TK 5.6).** Dropped as a charm display word; cards show the object noun; two item names take "Fob".
- **SK-13.** "kit" is the companion word only; the hero chip "Starting gear" is final; "Kit strip" → "Gear strip".
- **CMP-27.** "Levy soldier" approved; AR 12.3 applies it (NP-7).
- **CC-65.** "Sprawled" final; ENM 17.5, XI 5 and RS RC5 restate it; "Upended" retired (NP-6).
- **HR 19.** Spur → Urge; Interject → Speak Over.
- **VG 18.** Hold the Line → Bar the Way.
- **HZ "Cord".** Kept as the capitalised keyword; the binding-spell prose stops saying "cords" (NP-31).
- **BR "Prompts".** → "Boss-action warnings".
- **TL "Iced".** → "Frost-seized padlock".
- **HL spares.** The Frets retired; the Sullens held on the second-look list.
- **CH "the client confirmed these placeholders".** Kept unless a rule breaks; the two that change a confirmed concept (Hollowfolk; the Keeper's Cart's name, not its role) are Q3 and a mechanical rename respectively (told plainly, 14.2 T10).
- **The CA "implies the Dreamer's age" check.** Run on every Heart rename (5.2); it rejected Hobby-Horse, Rocking Horse, The Toy Cupboard and Rattle (A.1).

### 2.3 Collisions found (the sweep)

The sweep produced about 250 collision rows (exact equality, near pairs, external IP, tone, register clashes, names that lie). Each is resolved in 5; the clusters, by severity:

**Must-fix (legal, sensitivity, lies, exact cross-registry equality).** Fireball; Blight and Sight-blight; Scorching Rays; the rest tools (Second Wind, Catch Breath, Rekindle); Upended; Answer ×2; Berserker, Raging, Reckless; Overwatch; Last Rites; Coffle; Weary/Wary; Rooted/Routed; Cross-Reference ×2; Lame; Antitoxin; Holy Water; Stoneskin; Totem; Hunter's Longbow; Catcher's Bracers; Lockjaw; Sigil of Aid; Stairtop/Stairhead; Last Hollow/Hollowfolk/Silk (ECHO-3); Dreamfall; Night-Mare; Protector ×3; Frozen Footman; Lullaby ×4; Lingerer; Restricted Section; Snare ×3; The Dreamer Turns Over; Oathbroken; Viaticum; Wayfarer's Pack; Token; the Catch family; Device ×2; kit ×4; Quiet camp ×2.

**Should-fix (overloads and near pairs).** Sculpt; Hold the ×5; Spur; Interject; Sound the ×2; Fire ×5 (charm log verb); Kneel; First Cut; the 4e role set; the Mass Effect cluster (Adept); Bastion; Volley, Steady Aim, Shatter; Bull Rush; Bloodletter; Skyfall; Gout; Conduct/Conduit; Leaden/Leader; Withered/Weathered; Clutch/Clench; Undertone/Undertow; Dissect/Dissolve; Crescendo ×2; Clangour ×2; Honed ×2; Resistant ×3; Charge ×2; Command ×5; Trick Shot/Trigger Shot; the Opening stem; Stand Fast/Steadfast; Sure ×4; Set and Spare; Kick ×3; Shockwave; Cold Ring; Read the Will; Raise; Ill- ×4; Choler/Collared (kept, 7.3); Antidote/Antitoxin; Unshaken; Held ×5; Distracted (EP modifier → "Busy −2", NP-35); Strike ×6; Turn; Portal; Mousetrap; Signature ×11; Protector; Kiter; Opener ×2; Ambush ×2; Picket and Pack templates; Weak ×3; Bleed/Corrodes/Slows; Slot Recovery; tick ×3; Dormant ×4; veteran and ragged tints; Standard ×3; Revealed/Hidden grades; Snare; Cord; The Last Evening; Prompts; the unravel words (Q1); Seeded run/night; Night ×10; the Hollow ×3; Wayward; Bequest/Hang it; Oath (kept, waiver); Keep/Kept; Spend/Spent; Coat rack; Keeper; Grave Goods ×2; carry ×10 (two renamed); Worn items/Trinkets; Treasure strip; Plain ×6; complexity words; Open grade; Plate ×5; Lead beat (spoken "main beat"); broken ×3; Spare; Kennel trap Snare; Lockdown Lever; Hot stove (request); Tide ×3; Lane; bell words ×8; Ring ×7; Last ×8; Hour ×6; Two Wings; curtain and gate skins; Rose Window; Reading Hall; Head of the ×3; Strongman; Figment; Gall Slick; Doppel; Sanctum (waiver); the -warden notables; the Gate/-breaker/-splitter stems; the given-name flags; the Worn cluster; Stair-Worn; Oxblood; Swallowed; Kinstone and the stone cluster; Porter's ×2; Trophy pole; Embercleaver; Heirloom + Deeds (kept, waiver W-11); Launcher and Blades; Second's Hairpin (kept once Second Wind moves); Rent Due; Coat Hooks by the Door; mirrors ×3; Slots ×2; Usher ×2; Left Luggage ×2; Last Page ×3.

**Acceptable with a recorded waiver (9.3).** The Ember family; Warded, Chilling, Blurred, Regenerating, Riposting, Retaliating affix mirrors; Wand of Sparks and Wand of Rime Lance (ARC-20); Mending Word; "another way" (IR and LE share one meaning); Cell Zero; the Den (template internal); the Oath form; Dazed with Addled reserved; church titles under the cap; Gust; Traced; Alerted; Swallow; Wail; Disinherit; the Rousing Engine; "the road not taken"; Afterhold/Aftershock; Shard/Shardling and Fan/Fan Throw; the Jailer and Turnkey families; Chalked and Chalkwright (now Slate-Sketcher, moot); Hearth and Heart.

### 2.4 Findings (told plainly)

- **F1.** The per-document greps were sound and the system still drifted: each grep saw only earlier documents, and nothing checked SRD lists, other games or sensitivity lists as data. The fix is machinery (9), not more care.
- **F2.** About twenty names are SRD spells or features doing the SRD job, another game's title, or a film or book phrase. The house rules (RS 7.6, HL-30; agreed) already forbid them; the documents' own checks missed them because the checks are internal greps.
- **F3.** Four names use real sacraments (Viaticum, Last Rites, Holy Water) or a slavery term (Coffle), the same ground on which HL rejected "Lent". One approved focus kind (Totem) is a living people's sacred word. These pass every existing validator.
- **F4.** The pass's own group drafts contradicted each other on about 40 names and proposed several replacements that broke the same rules (Mired is already a condition; Unbowed is already an armour property; Shelter is an armour property; Snap meets Snap Casting; Bile Slick breaks HL NR6; Running Noose and Picture-Hanger bring back hanging). The critics caught these (A.1); 5 carries one final per row.
- **F5.** The corpus does not ban SRD words; it adopts some on purpose. A pure blocklist fails the corpus's own core vocabulary (4.5 contests the literal reading of RS 7.6 and HL-30 NR2).
- **F6.** CA V23s's "never print catch on any surface" is right in effect but too broad as written (a substring ban on a common verb) and inconsistent with approved text that prints it ("Charms catch moments"; Shade's trigger Caught; TL's Lip Catch). 4.8 restates it as "never a keyword or a name"; ordinary prose in barks may say "catch".
- **F7.** HL-27's Plain names are unique only by prefix. Screen readers read the tag; "First Stumble" would be read the same for a Malady and a curse. Tags must be unique within scope (6.4).
- **F8.** Three approved names are in agreed question text (Viaticum in LE Q1; Bedside Stand's silhouette exception in LP Q5; "Charms catch moments" in CA Q1), so their renames need the client even though they are wording only (Q2, Q5).

---

## 3. Approach

**How this document was made (lead's call).**
1. *Harvest.* One harvester per approved document listed every player-facing name with category, harvest status (approved-final, placeholder, unclear, flagged-to-naming-pass) and sites, and every naming-pass commitment and flag (2.1).
2. *Research.* Three briefs: naming conventions in comparable games, legal and cultural constraints, and the corpus's own naming rules (1).
3. *Collision sweep.* Four sweeps across the harvest (exact equality, near pairs by edit distance and shared first letters, external IP, tone and register).
4. *Style guide and group proposals.* A draft style guide and four group proposals (combat terms; items; world; interface and system).
5. *Critique.* Three critics (tone, clarity, completeness) read all five drafts.
6. *Synthesis (this document).* One final per row, chosen by the critics' lenses: the more truthful name wins; then the name with fewer collisions against the corpus **and the other winners**; then the shorter, more speakable one; then the one that keeps the client's world. Every winner was re-grepped (5, last column), and the current-name sites were generated by script (5, sixth column).

**Which side of a collision moves (lead's call).** The more important, more central or more approved name keeps the word: an agreed client name over a placeholder; a keyword over a flavour name; a boss over an enemy type ability; a class ability over a boss move when Classes 10 forbids equality (Cross-Reference stays with the Loremaster); the more truthful holder over the less truthful one (Routed over Rooted; Weary over Wary is the exception made for readability on plates, Q5).

**Candidates rejected in the synthesis.** Listed per row in 5's reason column and in A.1 (for example Landing, Exit Stair, Long Fall, Drop-through, Depth tiers, Regulars, Snap, Shelter, Shielding, Iron-Willed, Mired, Stuck, Bile Slick, Running Noose, Picture-Hanger, Rattle, Effigy, Psalter Crab, Benison, Treacle, Thump, Couch, Book of Runs, Shooter, Spieler).

---

## 4. The naming style guide (binding rules; lead's call; adopted by NP-2)

These rules are the **N-rules**. They add to, and never relax, RS 7.6, RC1–RC9, RS 8.0, Classes 10 `NameCollision`, W 7.4, TK 15, AR 17.1 rule 13, IR 4.6/6.1, V14 (f), HL NR1–NR10 and HL-27/30, SK 4.5.1–4.5.2, CH AN1–AN13, CA-12/V23s, SV 4.9 and XI-19 (all settled or agreed). Where a rule here narrows an approved rule, it says so and is listed in 14.2.

### 4.1 Two layers (N1)
- **N1a.** Every rule-bearing term (condition, damage type and family, ladder step, ability, save and skill, universal action and verb, resource and counter, area shape, awareness and morale state, refill tag, rarity tier) is a **keyword**: one plain word or a short fixed phrase from the closed Rules Glossary (6.1). No keyword has a synonym.
- **N1b.** Effect lines, previews, greyed reasons and logs refer to mechanics only through keywords and numbers.
- **N1c.** Flavour names (abilities, spells, items, notables, trinkets, charms, heirlooms, curses, enemies, bosses, places, hazards, traps, Curiosities, camp figures, given names, Oddments) are **labels**. The rule line sits above the name on every card (HL NR7 extended to all cards).
- **N1d (slot nouns).** Things that fill the same mechanical slot share a head noun and put flavour in the modifier (Hades pattern). Reserved slot nouns: **Signature** (the announced boss move only), **Attack** (the boss menu's basic move), **Device** (TL trap family only).

### 4.2 Registers by category (N2)

| Category | Register and form | Cap (English source, characters) |
|---|---|---|
| Conditions | One plain adjective or past participle; unique icon and colour | 12 |
| Damage types and families | Plain physical or elemental noun | 10 |
| Abilities, saves, skills, derived stats | Plain; one word or a fixed two-word phrase | 16 |
| Universal actions | Imperative verb first, 1–3 words | 16 |
| Weapon verbs | One imperative verb | 8 |
| Armour verbs | One adjective for the wearer's state | 10 |
| Properties | Truthful modifier or modifier + noun, 1–2 words | 16 |
| Classes | Occupational noun, one word | 12 |
| Subclasses | Role or craft noun, one word | 12 |
| Class abilities and pools | Verb-led or modifier + noun, 1–3 words; one sub-register per class (Vanguard drill commands; Shade thieves' cant; Arcanist workshop words; Herald speech acts) | 18 |
| Spells | Arcanist: template noun + "(family)"; Herald: a speech phrase | 16 |
| Enemy roles, ranks | Plain functional word; profiles internal | 10 |
| Enemy affixes | One truthful adjective; an exact condition word only if it grants that condition | 10 |
| Common enemies | Theme trade or role (+ material); Title Case on plates, lower case in prose | 18 |
| Bosses | "The <Role>", "The <Role> of the <Noun>", or "<Name>, the <Epithet>" | 28 (head 14) |
| Boss abilities | Diegetic verb phrase under the universal banner (RC7); never a party ability's word | 18 |
| Item base families | Plain object noun (SRD families allowed, 4.5) | 14 |
| Notables and signatures | Owner-trade, wear or material adjective + **family noun last** | 24 |
| Epithets | Suffix "of the <Noun>" only; never on a base that contains "of" | +14 |
| Bynames | "the <Agent-noun>", only after a Deed | 16 |
| Trinkets | <property-word>'s <kind> or <adjective> <kind>; the property is the verb (TK 15) | 22 |
| Charms | A small ordinary object; no kind word beyond the wave cap | 16 |
| Consumables | Function + container | 18 |
| Scrolls and wands | Follow the ability (ARC-20); wands never take epithets | 22 |
| Heirlooms | Wear or age adjective + family noun | 22 |
| Curse cost lines | Folk adjective (HL register 2) | 12 |
| Maladies | Obsolete humoral or theological noun (NR1) | 14 |
| Oaths | "Oath of the <Adjective> <Noun>" (NR5) | 30 |
| Themes | "The <Adjective> <Noun>" or "The <Noun> of the <Noun>"; one short form | 30 |
| Acts | One depth noun | 10 |
| Places | Plain topographic or architectural noun made strange by one modifier | 22 |
| Portal and terrain kinds | Plain rules noun | 10 |
| Hazards and Stirs | "<The Noun> <verbs>" or "The <Noun>" | 28 |
| Traps and locks | Object + mechanism noun | 18 |
| Curiosity cards | Whimsy budget: "The <object>" or a short imperative | 34 |
| Strands | Past participial phrase | 16 |
| Camp figures (the Fireside) | Period trade or craft noun, one compound; never a personal name or roster word (CH 5.1) | 16 |
| Companion kits | Occupational noun | 12 |
| Kit given names | Two plain words of material, wear or mark (SK 4.5.1) | 18 |
| Oddments | A resident trade's possessive + small object, no article (SK 4.5.2) | 24 |
| UI buttons / labels / notices | Sentence case; buttons are verbs ≤ 2 words; labels nouns ≤ 3 words; notices one sentence ≤ 18 words | 14 / 20 / — |
| Settings | Describe the effect | 24 |
| Meta options and difficulty | May use a comic or mismatched register; always truthful | 24 |

- **N2a (whimsy budget).** Long, odd or surreal names only where nobody reads under combat time pressure (Curiosities, Strands, Oddments, curses, camp figures, meta options, lore items, boss titles). The combat HUD reads at a glance.
- **N2b (one naming source per culture).** Recorded in the registry: the Fireside = period trade noun; Gaol = jail trades; Kennel = hunt trades; each theme's roster keeps its trade family (N-H).
- **N2c (caps are checked at +35%)** against the narrowest widget (7.1).

### 4.3 Form (N3)
- Title Case for names and keywords; sentence case for UI. Capitalisation never carries meaning (the "Cord"/"cords" lesson). No case transforms in code (L10N-5).
- Names never store an article; an `article` field and per-locale templates render it (L10N-4). A Curiosity whose article is part of the joke is a whole-phrase key with a translator note.
- Every countable name has an authored plural. Lists show "<n> × <Name>".
- Possessives only on notables, trinkets, heirlooms and Oddments, only with a trade or role noun, one per name, never a personal name, deity, saint, hero or Dreamer (NR3, AN4).
- At most one hyphen per name; no digits (digits on the map mean the room's clock); no apostrophes except the possessive; no diacritics; glyphs live in their own field with a spoken label (A11Y-2).
- Coined words: at most three syllables, English sound patterns, one obvious pronunciation (read aloud).
- **Upgrades keep their name** (N-J): "Whittle II", "Urge I". Elites show by frame and affix, never by a prefix; only consumable strength tiers use "Greater".

### 4.4 Never lie (N4; RC9 applied)
- **N4a (blind-guess test).** Shown the name alone, a new player's guessed effect, element, target or threat must not contradict the rule line. An opaque guess passes when an icon and the rule line are present; a misleading one never passes.
- **N4b (keyword in flavour).** A flavour name may contain a keyword or keyword stem only if the thing applies, grants or reads that keyword. Passes: Staggering Shot (applies Staggered), Wardstone (grants Warded), Trapped (reads a unit in the Trapwright's trap). Fails (renamed in 5): Frozen Footman, Venom Oil, Unshaken, Grasping Roots, Chilled Pour, Iron-Willed, Hidden Reserves, Shielding.
- **N4c.** Element words imply that element (no "thunder", "shock" or "fire" on non-matching effects); "Pack" means the inventory place; "bar" means a Grille; "trap" means a TL trap.

### 4.5 SRD, legal and outside IP (N5; contests the literal reading of RS 7.6 / HL-30 NR2, told plainly T1)
- **N5a (hard floor).** No WotC brand, Product Identity monster, named-wizard prefix, setting or character name (LEGAL-1, LEGAL-3). No "compatible with D&D" claim.
- **N5b (multi-word SRD names)** fail as exact phrases, case-insensitive (HOUSE-SRD-1).
- **N5c (single-word SRD names)** fail only where the thing does broadly the SRD job (HOUSE-SRD-2): a "Haste" that grants actions fails; "haste" in a bark passes.
- **N5d (the allowlist, `srd_allow.json`).** Adopted with attribution and passing: condition names; rarity tiers; the six adopted 5.2 masteries (Cleave, Graze, Nick, Push, Topple, Vex); SRD weapon, armour and gear base families; the six abilities and saves; the ten damage types and three families (their theming stays open, STAT 8.1, agreed); the universal actions Dash, Disengage, Dodge, Help, Hide, Search, Shove, Grapple, Attack and Opportunity attack; Extra Attack; Concentration; short rest; spell slot; Pack Tactics and the cited traits; Keen (sense trait). Any other SRD hit fails.
- **N5e (echo list, ECHO-1..4).** An exact match to another game's named item, ability, area, currency or title fails. A shared generic word in a matching mechanic is flagged. Two or more echoes of one studio in one scope must be broken by a rename (cluster test). Seed list: Darkest Dungeon, Hollow Knight and Silksong, Hades and Hades II, Souls and Bloodborne, Slay the Spire, Mass Effect, Dreamfall, plus the game titles found (Overwatch, Bastion, Condemned).
- **N5f (real world, N-L; new lead's call, told plainly T2).** A gazetteer check against real places, brands, products, people and well-known film and book titles fails exact matches. This is new, not text in RS 7.6 (research, corpus-rules: the brief overstated RS 7.6); real people and places already fail through HL NR3.
- **N5g (store-facing names).** The title and any name used in marketing (hub, classes, themes if marketed) get a trademark clearance and exact-match search before a store page (SEARCH-1); that is legal's step.

### 4.6 Sensitivity (N6)
- The blocklist (`sensitivity.json`) **outranks NR1** (MH-1; told plainly T3): an obsolete clinical term that is now a slur fails however old it is.
- Fails: living diagnoses and stigma words (MH-2), slurs and slur-verbs (CULT-2), a living people's sacred practice (CULT-1), real religions' figures, sacraments and holy-war words (CULT-3; RS 7.6's "invented liturgies only"), hate-symbol names and numbers (CULT-4), gendered terms (NR2), suicide and self-harm words including **hang, hanged, hanger, noose** as substrings of display strings (new lead's call; the "Strung Marionette" precedent).
- Church titles (Deacon, Precentor) are generic usage and count under RS 7.6's church cap; the sensitivity read sees them.
- **'bile'** never appears in a display string except Atrabile (HL NR6, settled).

### 4.7 Premise words (N7; RS 7.5, AN4; Q1)
- **Reserved:** dream, dreamer, sleep, slumber, asleep, wake, waking, woke, awake, nightmare, reverie, lucid, lullaby, bed, mattress, pillow, cradle, and the CA age-implying list.
- **Names may not contain them** except on the waiver list **W-DREAM-1** (Q1, option A):
  - **(a) UI furniture that names the run's frame:** Dream card, Dream track, Dream step, Dream's Due, Dream map, "What the dream kept", "New in the dream", "the dream unravels", Dream Rules pages, the Unwoken Roll, "You did not wake", Asleep and Dormant (approved conditions and states).
  - **(b) Named F5 Heart and Wake-theme content, listed one by one:** The Endless Wake; The Dreamer's Heart; Sleepwalker; Insomniac; Lullabist; The Lullaby (boss); Lullaby Glyph; Nearer Waking; Fitful Sleep; Foot of the Bed; the Turned-Down Bed; Dark Under the Bed; Hand Under the Bed; The Bed Turns Over.
  - **(c) Everything else is banned**, including "bed" and "mattress" outside the Heart. The Waking City stays gated (renamed before M4 or waived by the client then).
- Flavour lines may use premise words within NR8 and AN4; names may not (C9 of the style draft; told plainly T4).
- **Never**: any name implying the Dreamer's identity, face, **age** or bed outside list (b); any waking promise; any hero motive.

### 4.8 One word, one meaning (N8)

| Word | Its one on-screen meaning | Every other use |
|---|---|---|
| Signature | The boss's announced move | Weapon verb signatures show the property name; subclass "Signature Technique / Tricks option" show as their own names; "Signature item" shows as "the boss's prize" (Q5); `signature cache` internal |
| Dormant | The enemy awareness state | Item line "? untold" (Q5); charm pip "Idle" |
| Plate | The unit information plate | Armour shows "Plate armour"; the trap trigger is a "pressure slab"; Offertory Dish |
| Hide | The universal action | Armour shows "Hide armour" |
| tick | The time unit | Shield glyphs spoken "shield mark / broken mark"; the Dream tick cue has no text |
| kit | A companion package | Hero gear "Starting gear"; "Gear strip"; Herald picks "Shield hand / Focus hand" |
| boon | The starting boon | MAIN 4.5 pool says "offers" (Q4) |
| Night | Book of Nights and the Night Usher, Night Governess (proper names) | No unit of play, tier, market, card or Wager room |
| Catch / caught | Never a keyword or a name | Clasp, Respite, Trapped, Unbraced, Courier, Lip Clamp, grab square, Forecourt Top-up, Hunch badge, "Facing the door", "Charms hold moments"; ordinary prose in barks may say "catch" |
| Strike | The attack-roll word in rules text | Boss menu "Attack" (Q5); Stir part internal |
| Hollow | The camp phase and Last Hollow (camp scope) | Hollowfolk → the Fireside (Q3); "the next camp" |
| Stair | The Stairtop (hub) and the Downstair (floor exit) | Hub station the Banister |
| Ward | Warded and items that grant it | Role tag Buffer; -ward neck suffix kept as a recorded waiver (W-13) |

**Families ruled internal (never printed; V24 r6).** Behaviour profiles (Charger, Holder, Kiter, Mob, Automaton, Boss, Raider, Protector, Disruptor, Commander, Stalker, Ambusher, Opportunist, Sentinel); encounter templates (ENM 17.4; the Lore preview shows EP formation names); HZ Stir parts (Strike, Turn, Portal, Act, Taint, Emit, Overturn, Mute round); BR Counterpiece payoff kinds (Cancel, Deny, Divide, Divert, Sate, Expose, Release); socket classes; EP catalogue titles (G1…H6) and FT1–FT3; EV Grace ids (G-Ward…; the chip shows "Grace: Warded"); FL Site and SiteRule; CA shape labels (Spend, Catch, Keep, Shaper); HL Bequest; IR Reverie label (the line shows in the Whence pane unlabelled); the Antechamber (the room is the Forecourt on screen); reaction-policy and greyed-reason ids (each gets a display key, 5.9); CMP Gap card, Free cards, slot ladder; SV build tag, check character and preset; `MimicPlacement` ("mimic" never on screen).

### 4.9 Head-word cap (N9)
In one category (and across the class layer), at most **two** live names share a head word or stem of four or more letters. Over the cap → rename (the CA Knot rule made general). Currently at cap after this pass and watched: Knot, Stone, Bone (Twitch Bone, Wishbone; Jawbone ships in a different wave, W-16), Pebble, Bead, Long, Far, Last, Deep, Hold, Second, Sure, Ring, Iron, Hand, Final, Hour. "-ward" on the neck lane and "Ember" items are recorded cluster waivers (W-13, W-1).

### 4.10 The Name Gate (N10; how a new name is checked)
Every new or renamed player-facing name passes, in order: **G1** register (4.2); **G2** blind guess (4.4); **G3** keyword collision (exact, keyword stem inside flavour, keyword pairs at edit distance ≤ 2, shared first three letters within a family, homophones); **G4** flavour collision (exact equality across all registries fails; shared head word in a category, edit distance ≤ 2, same first four letters and length band, and filtered substrings flag); **G5** SRD (4.5); **G6** IP floor; **G7** echo; **G8** sensitivity; **G9** premise; **G10** real world; **G11** localisation (translator note, +35% pseudo-loc fit, no stitched fragments, locale blocklist); **G12** accessibility (no TTS homograph, not a UI button verb, SSML hint for obscure words, no colour-only sibling pair); **G13** tone by depth (RS 7.4); **G14** search and trademark (store-facing names only). A flag needs a recorded waiver (reason, approver, date); a fail blocks the record. G3–G10 run in V24 (9.3); G1, G2, G11–G14 are review gates with recorded results.

**Substring rule (contests V14 (f), NR6 and V20s as written; told plainly T5).** Substring checks run only where the shorter string is a keyword, or is five or more letters and both names sit in the same category or on the same surface. Everything else uses whole word, head word and edit distance. Unfiltered substring greps on "Ward" (223 hits), "Answer" (82) or "Fire" drown the real hits. Recorded waivers stay valid.

### 4.11 Lifecycle (N11)
`draft → placeholder (frozen, grep-checked) → proposed (naming pass) → approved-final → shipped`; a rename of an approved-final name needs the client, an NP or later change id, owner restatements in every citing document, and a post-apply grep that finds the old name only in history notes. Old names go to `retired_names.json` and never return with a new meaning.

### 4.12 Smaller rules
- **N12 (epithets).** A base name never contains "of"; wands never take epithets (fixes "Sigil of Aid of ?").
- **N13 (Arcanist overrides).** A `variantOverrides` name must pass G5; if it fails, the "Template (family)" form wins and the scroll is renamed.
- **N14 (example names).** Example text uses only registered coined names (Brask, Tolly); never SRD monsters or real surnames.

---

## 5. The name register (lead's call; C rows per 14; applied by NP-4 to NP-38)

One row per ruling. **Final names are binding once the client approves this document** (C rows as answered in 14). Names not in this register and not in 4.8's internal families are **final as written** (Appendix B lists every harvested name with its outcome). Columns: the current name; the final; the reason (the rule it breaks, and the candidate rejected where a critic rejected one); the sections the source documents name; the **sites of the current name**, generated by a whole-word, case-insensitive script over the 34 specs and the review (document abbreviation and hit count; history notes are included, so restatements can be checked against this list); the type (M mechanical, **C** client ⚑ with its question in the reason or in 14, keep, waiver, retired); and the whole-word hit count of the final name across the same corpus (0 is free; any hit was read, A.2).

Where a current name is a common word with several senses (Open, Hidden, Strike, Set, Spare, Charge, Hold, Kick), the row renames only the sense in its first column; the other senses stay. Internal ids named in a row stay unchanged (N-Q).

### 5.1 Legal, SRD and outside-IP

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.1.1 | Fireball | **Detonation (fire)** | Exact SRD spell; the override is dropped and the Template (family) rule wins (4.6 N13) | CN 16.4; ARC 8.4 variantOverrides | 16: ABL 1, CN 2, ARC 10, IR 3 | M | 10 |
| 5.1.2 | Scorching Rays | **Rays (fire)** | One letter from SRD Scorching Ray, same mechanic | CN 16.4; ARC 8.3 | 7: CN 3, ARC 2, IR 2 | M | 13 |
| 5.1.3 | Blight | **Wilt** | Exact SRD spell doing the SRD job | CN 16.4 | 7: CON 1, CN 2, ARC 1, HR 1, HL 2 | M | 0 |
| 5.1.4 | Sight-blight | **Sight-rot** | Carries the SRD spell word Blight | CON 18.10 | 1: CON 1 | M | 0 |
| 5.1.5 | Stoneskin Draught | **Slate Draught** | SRD spell name; 'stone' cluster | CN 16.1 | 1: CN 1 | M | 0 |
| 5.1.6 | Antitoxin | **Bitter Draught** | Exact SRD gear name; 'Anti-' crowds Antidote Locket. Bitter Truth (HR) shares a head word across scopes: warn, waiver W-12 | CN 16.1 | 2: CON 1, CN 1 | M | 0 |
| 5.1.7 | Holy Water | **Lamp Water** | SRD gear and a real sacramental (RS 7.6 invented liturgies only) | CN 16.2, 12 | 9: CN 6, SH 2, IR 1 | M | 0 |
| 5.1.8 | Resistance Draught | **Tempering Draught** | Near SRD Potion of Resistance; keyword stem Resist- inside a flavour name | CN 16.1 | 2: CN 1, HZ 1 | M | 0 |
| 5.1.9 | Second Wind | **Dig In** | Exact SRD Fighter feature; rest tools need distinct first words (CL 10) | ABL 6.1, 6.2, 6.6, 10.3; CL 8.2, 8.3, 10; VG 7.x; DH 4 d19, 12.1, DH-10 | 77: ABL 3, CL 21, DH 18, TK 1, VG 30, ARC 1, CMP 2, HR 1 | M | 0 |
| 5.1.10 | Surge | **Onslaught** | Grants an extra action: does the SRD Action Surge job (HOUSE-SRD-2) | ABL 6.1, 6.2, 6.6, 14.1; CL 6.4, 8.3; VG 3.4, 7.3, 8.x | 121: ABL 4, CL 17, CN 7, DH 1, TK 1, SH 1, VG 76, ARC 9, CMP 2, HR 2, BR 1 | **C** ⚑ | 0 |
| 5.1.11 | Berserker | **Ravager** | SRD Barbarian subclass (HOUSE-SRD-4) | CL 9.1 | 5: CL 4, VG 1 | M | 0 |
| 5.1.12 | Raging | **Frenzied** | Echoes SRD Rage (HOUSE-SRD-4) | CON 18.9 | 5: STAT 1, CON 3, CL 1 | **C** ⚑ | 0 |
| 5.1.13 | Reckless | **Heedless** | Echoes SRD Reckless Attack | CON 18.9 | 5: ABL 1, CON 3, CL 1 | **C** ⚑ | 0 |
| 5.1.14 | Sculpt | **Whittle** | Echoes SRD Sculpt Spells (HL-30 bans SRD class-feature names); ARC-1 flag | ARC 3.3 F1, 7.5, 18, ARC-1; CL 8.5, 10 | 84: CL 9, ARC 73, CMP 1, HR 1 | M | 1 |
| 5.1.15 | Overwatch | **Ready Shot** | Blizzard game title and XCOM term (ECHO-2) | W 8, 18.3 | 38: ENM 2, W 5, SH 1, EP 28, IR 1, LP 1 | **C** ⚑ | 0 |
| 5.1.16 | Volley | **Arrowfall** | SRD Hunter feature | W 18.3; CL 10 rename table | 13: CL 1, W 1, TL 11 | **C** ⚑ | 0 |
| 5.1.17 | Steady Aim | **Settled Aim** | SRD 5.2 Rogue feature | W 7.6, 18.4, App A | 14: TK 5, W 7, CA 2 | **C** ⚑ | 0 |
| 5.1.18 | Shatter | **Fracturing** | SRD spell name | W 5.5, 18.3 | 33: CON 2, ITEMS 1, CN 2, W 10, ARC 4, EP 1, HZ 1, RT 2, RS 4, TL 1, EV 1, IR 3, HL 1 | **C** ⚑ | 0 |
| 5.1.19 | Bull Rush | **Bowl Over** | D&D 3.x / Pathfinder manoeuvre | AR 14.4, AR-49, AR-52; CL 10 | 12: CON 1, AR 4, CL 1, W 1, ARC 1, RT 4 | **C** ⚑ | 0 |
| 5.1.20 | Bloodletter | **Gashing** | Warhammer unit name (ECHO-2) | W 18.2 | 2: W 2 | **C** ⚑ | 0 |
| 5.1.21 | Thunderclap Greatclub | **Thudding Greatclub** | Non-SRD D&D cantrip; no thunder damage type (RC9) | W 18.6 | 5: W 1, IR 4 | **C** ⚑ | 0 |
| 5.1.22 | Thunderstone | **Blowback Jar** | No thunder type, no damage, only pushes (RC9); D&D 3.x item | CN 16.2, 12 | 5: CN 3, SH 1, ARC 1 | M | 0 |
| 5.1.23 | Skyfall | **Highfall** | Film title (N-L) | ARC 8.5 | 7: ARC 7 | M | 0 |
| 5.1.24 | Restricted Section | **The Chained Shelves** | Exact Harry Potter phrase (ECHO-2) | TL 7.6 | 5: TL 3, LP 2 | M | 0 |
| 5.1.25 | Night-Mare | **Pale Mare** | One hyphen from SRD Nightmare; Night overload; the nursery-toy candidates failed the Dreamer's-age check | RS 8.11; EP 7.10 | 11: EP 5, RT 1, RS 5 | M | 0 |
| 5.1.26 | Doppel | **Reflection** | SRD Doppelganger, same function (HOUSE-SRD-2); 'Second Self' re-opened the Second cluster | RS 8.12 | 1: RS 1 | M | 2 |
| 5.1.27 | Bile Ooze | **Gall Slick** | SRD 'X Ooze' pattern; 'bile' is banned on display strings (HL NR6, Atrabile the only waiver) | RS 8.10; EP | 7: EP 3, RS 4 | M | 0 |
| 5.1.28 | Bile Spit | **Sour Spit** | 'bile' banned on display strings (HL NR6) | BR 5.5 | 2: BR 2 | M | 0 |
| 5.1.29 | Relentless | **Undying** | SRD trait Relentless / Relentless Endurance | ENM 17.2 | 2: ENM 2 | M | 0 |
| 5.1.30 | Vampiric | **Leeching** | SRD Vampiric Touch | ENM 17.2 | 2: ENM 2 | M | 0 |
| 5.1.31 | Hunter | **Fixated** | Hunter's Mark and the ranger Hunter; it declares Quarry, so Fixated is truthful; clears Hunter's Longbow and Huntsman | ENM 17.2 | 23: CON 1, ENM 3, CL 4, CN 1, TK 5, W 4, CMP 1, EP 1, FL 2, IR 1 | M | 0 |
| 5.1.32 | Dreamfall | **Freefall** | Funcom game title (ECHO-2, ECHO-4) and a premise word used as a rules keyword (N-I) | HZ 0.1, 3.4, 6.4; CON; TL; ABL; DH | 34: ABL 1, CON 1, DH 2, HZ 29, TL 1 | M | 0 |
| 5.1.33 | Keepsakes | **Souvenirs** | Hades' exact category name (ECHO-1) | RS 3.4, 9.3 | 20: DH 1, FL 1, RS 10, BR 1, LP 1, HL 1, SK 3, CA 2 | M | 0 |
| 5.1.34 | Embercleaver | **Ember Longsword** | Magic: The Gathering card (illustrative example only) | ITEMS 8.2, 12.10 | 1: ITEMS 1 | M | 7 |
| 5.1.35 | Wish | **The Long Shot** | Single-word SRD spell on a hook that grants something (HOUSE-SRD-2); LE A.5 left it to the owner | LE 3.x, 6.15, A.5; CH 4.x | 14: LE 10, CH 4 | M | 0 |
| 5.1.36 | Wayward | **Windblown** | Echoes Hollow Knight's Wayward Compass inside the Team Cherry cluster (ECHO-3) | IR 0.1, 4.8, A.5 | 48: ENM 1, CN 1, BR 1, IR 38, LE 5, LP 2 | M | 0 |
| 5.1.37 | Aid | **of the Helper (epithet); Helping Sigil (family)** | SRD spell; an 'of' base breaks the IR suffix grammar; Helping is truthful because the sigil grants Helped | TK 13.6, 14.1, 14.2 | 10: CON 1, CL 2, TK 5, CA 2 | M | 0 |
| 5.1.38 | Blade Dance | **Steel Waltz** | WoW / Guild Wars ability name (ECHO-2) | SH 8.3 | 18: AR 1, SH 17 | M | 0 |
| 5.1.39 | Kinstone | **Kin Pebble** | Exact match for Zelda: Minish Cap Kinstones (ECHO-2); 'stone' cluster | TK 13.7; CA 5.1 | 13: TK 6, CA 7 | M | 0 |
| 5.1.40 | Oathbroken knight | **Faithless knight** | 5E Oathbreaker (NR2) and the HL Oath register (NR5) | RS 8.11 grim-reading set | 6: RS 2, HL 4 | M | 0 |
| 5.1.41 | Brennan | **Brask** | A real surname in example text (NR3); Brask is the corpus's registered example name | HR 9.3 | 1: HR 1 | M | 6 |
| 5.1.42 | Hobgoblin | **Turnkey (example)** | SRD monster names in the Break-chain example (RS 7.6) | HR 10.3 | 1: HR 1 | M | 46 |
| 5.1.43 | Goblins | **Inmates (example)** | SRD monster names in the Break-chain example | HR 10.3 | 2: HR 1, LP 1 | M | 24 |
| 5.1.44 | Adept | **Ascetic** | Mass Effect class cluster with Vanguard, Soldier, Sentinel (ECHO-3); Q6 | CL 9.1, 20.2 | 6: CL 3, VG 1, CMP 2 | **C** ⚑ | 0 |
| 5.1.45 | Bastion | **Rampart** | Supergiant game title and 2024 DMG Bastions (ECHO-2); Q6 | CL 8.3, 10; VG 8.2 and its ladder lines | 59: CL 11, SH 2, VG 46 | **C** ⚑ | 0 |
| 5.1.46 | Artillery | **Marksman** | The eight roles are D&D 4e's role set; three swaps break the fingerprint | ENM 8.1, 17.1; W 15.3 | 118: ENM 16, AR 4, CL 1, W 20, VG 1, EP 35, RT 18, RS 16, TL 1, SK 1, CC 5 | **C** ⚑ | 0 |
| 5.1.47 | Controller | **Hinderer** | As above | ENM 17.1 | 86: MAIN 1, ENM 14, ITEMS 3, CL 2, ARC 5, EP 17, RS 8, BR 1, LP 1, CH 15, XI 16, SK 3 | **C** ⚑ | 0 |
| 5.1.48 | Minion | **Swarmer (role); Minion stays the rank** | One word was on two axes (rank and role) | ENM 7.1, 17.1; EP; W | 180: ABL 1, CON 2, ENM 50, AR 4, CL 1, TK 1, W 29, SH 5, VG 4, ARC 3, CMP 3, HR 4, EP 27, RS 18, TL 1, BR 7, EV 1, LE 1, LP 9, CA 8, CC 1 | **C** ⚑ | 0 |

### 5.2 Sensitivity, tone and premise

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.2.1 | Viaticum | **Parting Fare** | Real Catholic sacrament (RS 7.6, CULT-3; 'Lent' precedent); in agreed LE Q1 text: Q2 | LE 0.2, 3.5, 6.11, 6.15, A.5, LK15; LP 4.10, LP-14; BR; ENM 15.x; IR 4.14 (id stays) | 230: ENM 6, ITEMS 4, CN 5, TK 2, CMP 1, FL 5, RT 1, RS 3, TL 2, BR 14, EV 2, IR 51, LE 72, LP 52, XI 5, HL 2, SV 2, CC 1 | **C** ⚑ | 0 |
| 5.2.2 | Bedside Stand | **the Washstand** | 'Bedside' places a bed beside the keepers on F5 (AN4; the Underpillow reasoning); Q5-agreed silhouette exception unchanged: Q2 | LP 0.1, 4.10, 5.3, LP-14, A.5; LE | 55: ENM 1, FL 1, RS 5, EV 6, IR 3, LE 3, LP 34, CH 1, XI 1 | **C** ⚑ | 0 |
| 5.2.3 | Totem | **Tipstaff (focus kind)** | Ojibwe doodem, a living sacred practice (CULT-1, NR3); PHB Totem Warrior. Rattle (25 hits, infant and death-rattle) and Effigy (burning or hanging figure) rejected: Q2 | W 17.8; CL 8.6; HR 7.7, 7.11; SK 5.1 | 90: ENM 1, CL 9, W 2, ARC 1, HR 39, RS 2, IR 2, CH 1, HL 10, SK 23 | **C** ⚑ | 0 |
| 5.2.4 | Thumb-Worn Totem | **Thumb-Worn Tipstaff** | Follows the focus-kind rename | HL 5.1 row 4, A.2 | 7: HL 7 | **C** ⚑ | 0 |
| 5.2.5 | Totem kit | **Focus hand** | Kit is the companion word (P-2); Totem retired | HR 7.7, 7.11; SK 5.1 | 4: HR 4 | M | 0 |
| 5.2.6 | Shield kit | **Shield hand** | Kit is the companion word (P-2) | HR 7.7, 7.11; SK 5.1 | 13: CL 3, HR 7, SK 3 | M | 2 |
| 5.2.7 | Last Rites | **Final Respects** | Real Christian sacrament (RS 7.6, CULT-3) | RS 8.5; BR 5.3 | 3: RS 1, BR 2 | M | 0 |
| 5.2.8 | Coffle | **Lashed Pair** | Chained line of enslaved people (CULT-2); Linked Shackle reused the Binder's Shackle head; Tether is on ARC's avoided list | ARC 9.1 | 6: ARC 6 | M | 0 |
| 5.2.9 | Lockjaw | **Jawbone** | Common name for tetanus (MH-2); Marvel name. 1 hit is CA's own object column | CA 5.1 row 12; risk 9 | 7: TK 1, CA 6 | M | 1 |
| 5.2.10 | Gout | **Deluge** | Living medical condition (NR1b spirit, MH-2); Outpour is an RT rule, Spout reads as a kettle | ARC 8.5 | 3: ARC 3 | M | 0 |
| 5.2.11 | Lame | **Limping** | Ableist term (MH-1 outranks NR1) | CON 18.10, 18.11 | 2: CON 2 | M | 0 |
| 5.2.12 | Hang it? | **Shelve it?** | Hanging imagery (RS 7.6 'Strung' precedent) | HL 4.1.2, 5.4 | 21: ITEMS 1, DH 1, CH 4, HL 15 | M | 0 |
| 5.2.13 | Running Noose | **(rejected candidate)** | Hanging imagery; not adopted (A.1) | naming-pass drafts only | 0: 0 (drafts only) | retired | — |
| 5.2.14 | Smother | **Heavy Blanket (fallback)** | Suffocation word on a nursery boss (RS 7.6, P4); Tuck In stays | RS 8.11; BR 5.6, 14.2 | 4: RS 2, BR 2 | M | 0 |
| 5.2.15 | The Dreamer Turns Over | **The Bed Turns Over** | Points at the Dreamer (RS 7.5, AN4, BR-30); the HZ owner's fallback, kept under W-DREAM-1 (b) Heart bed names: Q1 | HZ 6.17, 7.10; BR 4.13, 14.2 | 10: HZ 4, BR 6 | **C** ⚑ | 3 |
| 5.2.16 | Lucid Key | **the Clear Key** | Lucid commits to lucid dreaming (N-I) | RS 4, 9.1, 12 | 16: FL 3, HZ 1, RS 7, TL 1, EV 1, CH 1, XI 2 | M | 0 |
| 5.2.17 | Middle Dream | **Midwater** | Dream word in an act name; matches the water family (Shallows, Midwater, Deep, Heart) | RS 3.4, 6.2, 8.0; EV pool tags | 21: RS 17, CH 4 | M | 0 |
| 5.2.18 | Nightmare tiers | **Peril tiers** | SRD monster Nightmare, the Night overload, N-I; pairs truthfully with the Perilous Door; in approved MAIN 4.7 wording: Q4 | MAIN 4.7; RS 3.4, 9.1; BR 4.14.2; CH 4.8, 4.10, 4.11; EV; HZ 6.19; LE; ENM | 32: MAIN 3, ENM 1, HZ 9, RS 9, BR 2, EV 1, LE 4, CH 3 | **C** ⚑ | 0 |
| 5.2.19 | Nightmare Rendition | **Peril Rendition** | Follows Peril tiers: Q4 | RS 9.1; BR 4.14.2; MAIN 4.7 | 7: HZ 1, BR 4, LE 2 | **C** ⚑ | 0 |
| 5.2.20 | Nightmare Provisos | **Peril Provisos** | Follows Peril tiers | EV-31 | 4: EV 3, HL 1 | M | 0 |
| 5.2.21 | Other Dreamers | **Strangers** | Dream word; implies other dreamers (RS 7.5) | RS 9.1, 12 | 2: RS 2 | M | 0 |
| 5.2.22 | Wake Order | **Alarm Order** | Wake word outside the Wake theme (N-I) | EP 9.1 EP-37 | 5: ENM 1, EP 4 | M | 0 |
| 5.2.23 | Wakes at 33% | **Joins at 33%** | Wake word; also more accurate | BR 4.5.2 | 2: ENM 1, BR 1 | M | 0 |
| 5.2.24 | Sleeping Ward | **the Heap** | Re-blurs the Dormant/Asleep split (CON 5.5, C15); 'Ward' crowded | EP 6.6.6, 6.10, App C | 37: ENM 9, EP 27, FL 1 | M | 0 |
| 5.2.25 | Lullaby | **Soft Song (Lullabist ability only)** | Equalled the M3 final boss The Lullaby (NameCollision); the boss keeps the word | RS 8.11 | 45: EP 1, HZ 1, RS 10, TL 7, BR 3, EV 7, LP 1, CH 7, XI 1, HL 7 | M | 0 |
| 5.2.26 | Slumber | **Drowse** | Sleep word next to SRD Sleep; Later | CN; ARC 8.8 | 3: CN 1, ARC 1, RS 1 | M | 0 |
| 5.2.27 | Homecoming | **Gather In** | Reads as a waking promise (AN4, RS 7.5) | HR 9.2 | 8: HR 8 | M | 0 |
| 5.2.28 | Oath of the Iron Sleep | **Oath of the Cold Iron** | Sleep word (N-I); head noun Iron stays unique (NR5) | HL 5.2, 14.3 T12 | 14: ENM 3, HL 11 | M | 0 |
| 5.2.29 | Ill-Slept | **Heavy-Eyed** | Breaks the Ill- cluster and drops the sleep word; 'Unrested' carried the Rest keyword | HL 5.2 #11 | 19: CON 1, ITEMS 2, LE 1, HL 15 | M | 0 |
| 5.2.30 | Alms Crab | **Pew Crab** | Alms is a church practice in the church-capped Choir; Psalter (a liturgical book) rejected | LP 5.3 | 13: RS 1, LP 11, XI 1 | M | 0 |
| 5.2.31 | Offertory Plate | **Offertory Dish** | Plate is the enemy-info UI and an armour family | LP 5.3 | 5: LP 5 | M | 0 |
| 5.2.32 | Stair-Worn Byrnie | **Road-Scuffed Mail** | Stairtop pun flag (HL); byrnie is hard for TTS | HL 5.1 row 10, 14.3 T12 | 6: HL 6 | M | 0 |
| 5.2.33 | The Waking City | **(held, gated before M4)** | Waking promise (AN4, N-I); rename before M4 or an explicit client waiver: Q1 option | RS 6.3, 8.12, 12 | 6: RS 4, BR 2 | retired | — |

### 5.3 Keyword layer

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.3.1 | Wary | **Watchful** | Edit distance 1 and near-homophone of the party condition Weary (2 hits are cut item names) | ENM 12.1; EP 6.21 | 61: ABL 1, ENM 15, SH 8, EP 30, FL 2, HZ 1, BR 4 | **C** ⚑ | 3 |
| 5.3.2 | Rooted | **Tangled** | Homophone of the agreed morale state Routed (TTS reads both the same); Mired is already a condition, Snared meets Snare | CON 18.4 and its source lists | 32: CON 18, W 4, VG 3, ARC 1, HR 1, HZ 3, RT 1, HL 1 | **C** ⚑ | 0 |
| 5.3.3 | Upended | **Sprawled** | CC-65 restatements owed | ENM 17.5; XI 5; RS 7.4 RC5 | 17: ENM 1, VG 1, RS 1, BR 12, XI 1, CC 1 | M | 36 |
| 5.3.4 | Recruit soldier | **Levy soldier** | CMP-27, before records | AR 12.3 | 4: CMP 4 | M | 5 |
| 5.3.5 | Resisting | **Resistant (type) on screen** | The condition prints the ladder keyword it grants; one word per meaning | CON 18.9; STAT 8.3, 8.4 | 15: STAT 2, CON 5, CN 4, TK 1, ARC 3 | **C** ⚑ | 0 |
| 5.3.6 | bloodied | **hurt** | D&D 4e / SRD 5.2 signature word on the HUD; 'wounded' was a retired condition | DH 17.8; CON 15.1 | 14: CON 1, DH 4, HZ 1, BR 1, XI 1, CA 6 | **C** ⚑ | 16 |
| 5.3.7 | standard (HP tier) | **sound** | Standard is a rank, a DC tier and an HP tier | DH 7.1; CL chassis | 199: ABL 1, ENM 32, ITEMS 6, AR 2, CL 2, CN 4, DH 4, W 1, SH 4, ARC 2, CMP 3, HR 4, EP 48, FL 5, HZ 1, RT 8, RS 7, TL 31, BR 1, EV 14, IR 1, LP 5, XI 5, SV 4, SK 2, CA 2 | **C** ⚑ | 89 |
| 5.3.8 | Ill-Starred | **Star-Crossed** | Four Ill- curse words (CONF-1); Ill-Omened keeps the prefix | CON 12.2, 18.10, C7 | 14: STAT 1, CON 3, ITEMS 2, HL 8 | **C** ⚑ | 0 |
| 5.3.9 | Leaden | **Plodding** | Edit distance 1 from Leader; 'lead' is a TTS homograph | CON 18.10 | 35: CON 1, ITEMS 2, HL 32 | **C** ⚑ | 0 |
| 5.3.10 | Ill-Wished | **Begrudged** | Breaks the Ill- cluster; 'Overcast' read as spell over-casting next to Overchannel | HL 5.2 #5 | 7: ITEMS 1, HL 6 | M | 0 |
| 5.3.11 | First Stumble | **Stumbles Early (cost line)** | Duplicated the Spleen's Malady plain label (tag must be unique, 6.4) | HL 5.2 | 3: HL 3 | M | 0 |
| 5.3.12 | the Frets | **(retired spare)** | NR1b mood word; near Fray | HL A.2 | 1: HL 1 | retired | — |
| 5.3.13 | Approach | **Come** | Commanded words are the SRD Command spell verbatim (HOUSE-SRD-1) | CON 18.3 | 40: ABL 1, CON 1, ENM 6, ITEMS 2, DH 1, W 1, HR 2, EP 9, FL 7, HZ 2, RT 3, RS 1, TL 1, LP 1, CH 2 | **C** ⚑ | 170 |
| 5.3.14 | Grovel | **Crouch** | As above | CON 18.3 | 4: CON 4 | **C** ⚑ | 1 |
| 5.3.15 | Halt | **Wait** | As above | CON 18.3 | 5: CON 1, HR 2, LP 2 | **C** ⚑ | 51 |
| 5.3.16 | Flee | **Run** | As above | CON 18.3 | 81: STAT 2, CON 2, ENM 6, ITEMS 8, DH 6, W 1, HR 4, EP 1, FL 24, HZ 4, RT 4, RS 5, BR 3, LP 3, XI 8 | **C** ⚑ | 2091 |
| 5.3.17 | Bleed | **Bleeding (printed)** | Rider wording prints the exact condition keyword (N-B) | STAT 8.1, 10 | 21: STAT 2, CON 4, AR 3, DH 2, TK 5, W 2, RS 2, IR 1 | M | 112 |
| 5.3.18 | Corrodes | **Corroded (printed)** | As above | STAT 8.1 | 1: STAT 1 | M | 101 |
| 5.3.19 | Slows | **Chilled (printed)** | As above | STAT 8.1 | 9: STAT 1, CON 1, AR 1, SH 1, ARC 1, RT 1, BR 1, HL 2 | M | 130 |
| 5.3.20 | broken (Herald previews) | **Demoralised (printed)** | Keyword only (N-A) | HR 3.4, 10.3 | 123: MAIN 1, ABL 1, STAT 5, CON 5, ENM 18, CL 3, CN 1, W 4, SH 10, VG 1, ARC 2, CMP 1, HR 11, EP 14, FL 1, HZ 1, RT 18, RS 3, TL 2, BR 7, LP 2, CH 3, XI 3, SV 2, SK 1, CA 2, CC 1 | M | 107 |
| 5.3.21 | Resolve Broken | **Resolve Spent** | 'broken' has three printed meanings | BR 4.7.4, 6 | 8: RS 1, BR 6, XI 1 | M | 0 |
| 5.3.22 | Revealed (trap grade) | **Found** | Revealed and Hidden are unit conditions | TL 3.4, 6.4, 6.23 | 166: ABL 3, STAT 3, CON 10, ENM 12, ITEMS 4, CL 1, CN 3, DH 7, TK 12, SH 2, ARC 8, EP 6, FL 1, HZ 1, RT 1, RS 6, TL 51, BR 1, EV 3, LE 1, LP 1, XI 18, HL 5, CA 5, CC 1 | M | 213 |
| 5.3.23 | Hidden (trap grade) | **Unfound** | As above | TL 3.4, 6.4 | 482: MAIN 1, ABL 9, STAT 4, CON 20, ENM 29, ITEMS 7, CL 12, CN 3, DH 27, TK 26, W 2, SH 55, VG 3, ARC 4, CMP 4, HR 1, EP 59, FL 5, HZ 13, RT 13, RS 4, TL 55, BR 4, EV 9, IR 13, LE 15, LP 12, CH 9, XI 35, HL 14, SK 10, CA 3, CC 2 | M | 25 |
| 5.3.24 | Open (check grade) | **Clean** | Open is the Shade's open tier and every door verb | TL 0.1, 3.4, 6.6 | 635: MAIN 7, ABL 6, STAT 3, CON 20, ENM 23, ITEMS 9, AR 7, CL 9, CN 4, DH 5, TK 8, W 8, SH 79, VG 3, ARC 3, CMP 37, HR 3, EP 24, FL 89, HZ 24, RT 22, RS 22, TL 49, BR 7, EV 16, IR 11, LE 18, LP 51, CH 16, XI 29, HL 4, SV 6, SK 7, CA 3, CC 3 | M | 54 |
| 5.3.25 | Easy (DC tier) | **Low** | Would share a stem with the Easier lamp | TL 6.2 | 68: ABL 2, ENM 1, DH 1, ARC 5, EP 1, FL 4, RS 3, TL 27, LP 1, CH 1, XI 12, HL 7, CA 3 | M | 331 |
| 5.3.26 | Standard (DC tier) | **Fair** | Standard overload | TL 6.2 | 199: ABL 1, ENM 32, ITEMS 6, AR 2, CL 2, CN 4, DH 4, W 1, SH 4, ARC 2, CMP 3, HR 4, EP 48, FL 5, HZ 1, RT 8, RS 7, TL 31, BR 1, EV 14, IR 1, LP 5, XI 5, SV 4, SK 2, CA 2 | M | 44 |
| 5.3.27 | Weighted plate | **pressure slab** | Plate is the UI plate, armour and trigger kind | TL 3.4, 6.7 | 2: RT 1, TL 1 | M | 0 |
| 5.3.28 | Catch (trap family) | **Clasp** | 'Catch' is never printed (CA-12, V23s); Snap collided with Snap Casting | TL 3.4, 6.3, A.5 | 550: MAIN 1, ABL 7, STAT 1, ENM 5, ITEMS 7, AR 2, CL 6, DH 4, TK 34, SH 42, ARC 5, CMP 16, HR 1, FL 10, RT 1, RS 29, TL 59, BR 33, EV 3, IR 1, LE 7, LP 23, CH 34, XI 2, SV 11, SK 1, CA 203, CC 2 | M | 4 |
| 5.3.29 | Hunch catch | **Hunch badge** | Catch ban | TL 6.4; CA 2.3 F13 | 11: TK 1, RS 1, TL 2, CA 7 | M | 4 |
| 5.3.30 | Lip Catch | **Lip Clamp** | Catch ban | TL 7.x | 9: TL 6, LP 3 | M | 0 |
| 5.3.31 | catch square | **grab square** | Catch ban | BR 4.x | 12: BR 9, CA 3 | M | 0 |
| 5.3.32 | Antechamber Catch-up | **Forecourt Top-up** | Catch ban and the doubled room name | RS 9.4 | 27: ENM 1, CL 1, SH 6, ARC 3, RS 9, TL 1, BR 3, EV 1, LE 1, CC 1 | M | 0 |
| 5.3.33 | Catch Breath | **Respite** | Catch ban; 'Breather' shared the Breath stem with Stubborn Breath | CL 8.2, 8.4; SH 7.3, 7.4 | 37: CL 3, SH 31, ARC 1, HR 1, TL 1 | M | 0 |
| 5.3.34 | Caught | **Trapped** | Catch ban; truthful (the trigger reads a unit caught in the Trapwright's trap); 29 prose hits | SH 6.5, 8.2; CL 7.5, 8.4 | 111: CON 3, AR 3, CL 3, TK 3, SH 48, VG 1, ARC 1, CMP 1, EP 11, HZ 2, TL 1, IR 1, XI 1, SV 5, SK 2, CA 25 | M | 29 |
| 5.3.35 | Caught Out | **Unbraced** | Catch ban; Wrong-Footed built a Footing cluster | SH 8.2 | 12: SH 11, EP 1 | M | 0 |
| 5.3.36 | Catcher | **Courier** | Catch ban; Courier states the reach-2 hand-over | TK 13.2, 14.1 | 7: TK 7 | M | 0 |
| 5.3.37 | Catcher's Bracers | **Courier's Bracers** | Follows the property | TK 13.2 | 3: TK 3 | M | 0 |
| 5.3.38 | Caught facing the door | **Facing the door** | Catch ban | EP 6.20 | 8: EP 7, XI 1 | M | 9 |
| 5.3.39 | Rekindle | **Take Heart** | Kindle stem (retired Kindle, CH Kindler at the HP fire) reads as a heal (RC9); a Herald speech act | CL 8.2, 8.6, 10; HR 7.3 | 61: CL 7, ARC 1, CMP 2, HR 49, CH 2 | M | 0 |
| 5.3.40 | Kick (Cord verb) | **Knock** | Kick is also RT's party verb | HZ 6.3.4 | 80: ABL 5, STAT 1, ENM 3, DH 2, EP 1, HZ 9, RT 52, RS 1, TL 5, XI 1 | M | 34 |
| 5.3.41 | Hold (Cord verb) | **Sustain** | Hold is the Held keyword head | HZ 6.3.4 | 528: MAIN 2, ABL 10, STAT 2, CON 5, ENM 27, ITEMS 5, AR 1, CL 19, CN 3, DH 2, TK 11, W 4, SH 5, VG 62, ARC 11, CMP 9, HR 5, EP 84, FL 21, HZ 47, RT 16, RS 25, TL 5, BR 12, EV 5, IR 5, LE 12, LP 11, CH 20, XI 22, HL 10, SV 20, SK 4, CA 26 | M | 26 |
| 5.3.42 | Set (pike verb) | **Receive** | Collides with weapon set / spare set; Plant and Brace taken; 'Couch' read as a sofa | W 7.1, 7.4, 13, 17.5 | 1318: MAIN 5, ABL 19, STAT 7, CON 29, ENM 38, ITEMS 57, AR 30, CL 25, CN 23, DH 27, TK 19, W 91, SH 42, VG 69, ARC 25, CMP 13, HR 26, EP 51, FL 43, HZ 20, RT 25, RS 54, TL 41, BR 61, EV 26, IR 38, LE 33, LP 48, CH 25, XI 28, HL 33, SV 93, SK 121, CA 29, CC 4 | **C** ⚑ | 9 |
| 5.3.43 | Spare (focus verb) | **Withhold** | Collides with Spare weapon set and Spare Trick; near Spare the Dying | W 5.6, 17.8 | 138: ABL 6, STAT 3, CON 3, ENM 2, ITEMS 26, AR 10, CL 3, W 9, SH 20, VG 30, ARC 3, CMP 1, HR 3, EV 3, IR 2, CH 1, HL 3, SK 9, CC 1 | **C** ⚑ | 0 |
| 5.3.44 | Sound the wall | **Tap the wall** | Shared 'Sound the' head with Sound the Advance; truthful | FL 9.2, avoided table; ABL 7.2 | 24: ABL 2, STAT 1, CMP 1, FL 15, RS 1, LP 3, XI 1 | M | 0 |
| 5.3.45 | Conduct | **Charged Water** | Edit distance 1 from Conduit; 'Live Water' used a TTS homograph | RT 6.8 | 64: ABL 2, STAT 1, CON 9, ENM 3, SH 1, EP 4, HZ 8, RT 31, RS 2, TL 1, BR 2 | M | 0 |
| 5.3.46 | Kneel | **Lower your head** | Kneel is an EV option verb (A11Y-4); 'Bow' is a TTS homograph | HL 4.3.3 | 11: FL 1, RS 2, BR 1, EV 2, HL 5 | M | 0 |
| 5.3.47 | Pass by | **Walk on** | Near 'passing' (HL-30 heirloom blocklist) | HL 4.3.3 | 5: ARC 1, EV 1, HL 2, SV 1 | M | 1 |

### 5.4 Classes and class abilities

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.4.1 | Hold the Line | **Bar the Way** | Near-copy of the non-SRD Cavalier feature; 'Hold the' head shared by three categories; Stand Ground shares Stand with Stand Fast | CL 8.3, 10; VG header, 4.1, 7.4, 18, App A C-10; CMP shared names | 75: ENM 1, AR 1, CL 17, W 1, VG 49, ARC 1, CMP 2, HR 1, EP 2 | M | 0 |
| 5.4.2 | Spur | **Urge** | Grants Spurred: keyword inside a flavour name (HR 19 flag) | HR 7.3, 19, App A 23; CL 8.6 | 165: CL 6, W 1, CMP 2, HR 126, EP 2, FL 15, TL 1, EV 10, LP 2 | M | 0 |
| 5.4.3 | Earned Spur | **Earned Urge** | Follows Spur | HR 7.3, TF-2, 11.10 | 9: HR 9 | M | 0 |
| 5.4.4 | Interject | **Speak Over** | Same Inter- stem and slot as Interpose (HR 19 flag) | HR 4.1, HX-3, 11.10, 19 | 15: HR 15 | M | 0 |
| 5.4.5 | Undertone | **Ache** | Near Undertow (ARC spell); 'Under' cluster; truthful residual damage | HR 7.2, CM-1; CL 8.6, 10 | 26: CL 4, HR 22 | M | 0 |
| 5.4.6 | Lead by Example | **Show the Way** | Tail shared with Make an Example; 'lead' homograph | HR 9.3 | 3: HR 3 | M | 0 |
| 5.4.7 | Draw the Sting | **Ease the Pain** | 'sting' is the IR boss-banner word; Draw shared with Draw Out; 'Ease the Hurt' would meet the plate word hurt | HR 9.2 | 9: HR 9 | M | 0 |
| 5.4.8 | Opening Cry | **Clarion** | Opening stem (Shade) and War Cry | HR 7.13, HX-8, 10.5 | 11: HR 11 | M | 0 |
| 5.4.9 | Final Word | **Pronouncement** | Word tail used four times; Final head shared with Final Bow | HR 8.5 | 4: HR 4 | M | 0 |
| 5.4.10 | Raise the Pennant | **Hoist the Pennant** | Starts with the shield property Raise | HR 9.4 | 2: HR 2 | M | 0 |
| 5.4.11 | Lay Down Arms | **Yield Arms** | 'Down' is an AN5 word inside the Downed keyword | HR 8.3 | 13: ENM 2, HR 11 | M | 0 |
| 5.4.12 | Calm Minds | **Quiet Minds** | Echoes SRD Calm Emotions | HR 8.7 | 4: HR 1, CA 3 | M | 0 |
| 5.4.13 | Trick Shot | **Bank Shot** | Near-homophone of the universal action Trigger Shot | SH 8.2 | 11: SH 11 | M | 0 |
| 5.4.14 | Held Guard | **Closed Guard** | Held keyword inside a flavour name; pairs with Open Guard; 'Locked' is an FL Portal kind | SH 8.3 | 15: AR 1, W 1, SH 13 | M | 0 |
| 5.4.15 | Deep Reserves | **Fresh Reserves** | Deep cap (act name plus four class names); 'Hidden Reserves' carried the Hidden condition | VG 7.6 | 17: VG 17 | M | 0 |
| 5.4.16 | Charge (Sculpt mode) | **Load** | Charge is the Brute's ability (NameCollision); Store met the charm Stored pip | ARC 7.5 | 190: ABL 4, CON 10, ENM 4, ITEMS 3, AR 20, CN 24, DH 3, TK 30, W 30, VG 1, ARC 13, CMP 2, HR 1, EP 2, HZ 4, RS 1, TL 2, BR 8, EV 8, IR 3, CH 2, CA 13, CC 2 | M | 127 |
| 5.4.17 | Tempest | **Weatherwork** | Non-SRD PHB Tempest Domain; Thunderhead lied (no thunder type); Squall is a template | ARC 4, 7.3 | 10: ARC 10 | M | 0 |
| 5.4.18 | Dissect | **Lay Bare** | Same first letters and length band as Dissolve (CONF-1) | ARC 9.3 | 3: ARC 3 | M | 0 |
| 5.4.19 | Shockwave | **Groundswell** | 'Shock' implies lightning but it deals bludgeoning (RC9); Thump read as comedy | CN 16.4; HR and VG registry rows | 39: CN 4, VG 1, ARC 14, CMP 2, HR 4, HZ 1, RT 13 | M | 0 |
| 5.4.20 | Grasping Roots | **Grasping Weeds** | Implies Rooted/Tangled but applies Restrained (RC9) | CN 16.4 | 17: CN 2, ARC 12, CMP 2, HZ 1 | M | 0 |
| 5.4.21 | Holding | **Arrest (merged)** | Same Hold Person shape as the Arcanist's Arrest; ARC avoids Hold/Held | CN 16.4; ARC 8.8; HR 8.7 | 104: MAIN 2, STAT 3, CON 1, ENM 2, ITEMS 4, AR 5, CL 2, CN 2, DH 8, TK 2, W 3, VG 1, ARC 9, CMP 6, HR 7, EP 3, FL 4, HZ 10, RT 5, RS 2, TL 1, IR 1, LE 2, LP 6, CH 4, XI 4, SV 1, SK 1, CA 3 | M | 12 |
| 5.4.22 | Slowing | **Dragfoot** | Near SRD Slow; Treacle read as comedy | CN 16.4 | 18: CN 2, ARC 14, IR 2 | M | 0 |
| 5.4.23 | Blessing | **Warm Word** | Near SRD Bless; Benison is a liturgical word | CN 16.4 | 1: CN 1 | M | 0 |
| 5.4.24 | Banishing | **Cast Out** | Near SRD Banishment; 'Dismissal' is the CMP companion action (54 hits) | CN; ARC 8.8 | 2: CN 1, ARC 1 | M | 0 |
| 5.4.25 | Guiding Radiance | **Marking Light** | Borrows Guiding from SRD Guiding Bolt | CN 16.4 | 4: CN 2, ARC 1, HR 1 | M | 0 |
| 5.4.26 | Jarring Whispers | **Grating Murmur** | Echoes SRD Dissonant Whispers | CN 16.4 | 17: CN 2, ARC 1, HR 11, IR 3 | M | 0 |
| 5.4.27 | Wand of Snaring | **Wand of Fetters** | Snare is the TL trap family; fetters is the Binder's own truthful word | CN 16.5 | 4: CN 2, ARC 2 | M | 0 |
| 5.4.28 | Ember Burst | **Cinder Spray** | Fifth Ember form (CONF-2); illustrative | ABL 10.3, 14.1 | 1: ABL 1 | M | 0 |
| 5.4.29 | Second Nature | **Old Hand** | Clears the Second head word cluster | STAT 7.2; TL 9.2 | 11: STAT 1, TL 10 | M | 0 |
| 5.4.30 | Deep Stance | **Low Stance** | Deep cap; stance vocabulary beside Vanguard stances | AR 14.4, App A | 8: AR 4, TK 1, IR 3 | **C** ⚑ | 0 |
| 5.4.31 | Sure Grip | **Fast Grip** | 'Sure' head word in four categories; Locked is an FL Portal kind | W 18.2 | 10: CON 3, AR 2, W 5 | **C** ⚑ | 0 |
| 5.4.32 | Opener | **Forestroke** | Near Shade Opening and TL's Opener rule | W 18.2 | 86: ABL 1, CON 1, ENM 7, CL 1, CN 2, DH 2, TK 6, W 6, SH 4, EP 18, HZ 2, RS 8, TL 21, IR 4, SK 2, CC 1 | **C** ⚑ | 0 |
| 5.4.33 | Opener rule | **First-spring rule** | Same stem as the property and Shade Opening | TL 6.x | 16: ENM 1, TK 2, EP 2, TL 11 | M | 0 |
| 5.4.34 | Unshaken | **Unflinching** | Guards Frightened, not Shaken (RC9) | AR 5.6, 13.5, App A; TK 13.1 | 6: AR 3, TK 3 | **C** ⚑ | 0 |
| 5.4.35 | Weathered | **Fleece-Lined** | Edit distance 2 from Withered; Hardwearing read as retail | AR 5.6, 5.8, 13.5; TK 13.4, 14.1 | 21: AR 2, TK 15, IR 4 | **C** ⚑ | 0 |
| 5.4.36 | Weathered Cloak | **Fleece-Lined Cloak** | Follows the property | AR 5.6; TK 13.4 | 8: AR 1, TK 4, IR 3 | **C** ⚑ | 0 |
| 5.4.37 | Resistant (element) | **<element>-proof (e.g. Fireproof)** | Ladder keyword inside a property name; finalised with damage-type theming | AR 14.3 | 80: STAT 2, CON 7, ENM 1, AR 6, CL 1, TK 12, ARC 4, RS 9, BR 2, IR 27, LE 1, SV 8 | **C** ⚑ | — |
| 5.4.38 | Clench | **Iron Grip** | Near Clutch, Cinch, Clinging (CONF-1) | TK 13.2, 15, App A M1 | 3: TK 3 | M | 0 |
| 5.4.39 | Clenching Gauntlets | **Iron-Grip Gauntlets** | Follows the property | TK 13.2 | 1: TK 1 | M | 0 |
| 5.4.40 | Antidote | **Venomproof** | Concept duplicated with the consumable Antitoxin | TK 13.5, 14.1 | 27: TK 14, HR 1, HZ 1, RS 4, IR 7 | M | 0 |
| 5.4.41 | Honed (affix) | **Razored** | Equalled the weapon property Honed (AR 17.1 r13) | ENM 17.2 | 13: ENM 1, CN 2, TK 4, W 5, IR 1 | M | 0 |
| 5.4.42 | Protector (affix) | **Guardian** | Equalled the profile and the role tag; Shielding carried the Shielded condition; 7 prose hits only | ENM 17.2 | 47: ENM 3, CL 9, DH 2, VG 2, CMP 25, HR 3, EP 3 | M | 7 |
| 5.4.43 | Commanding | **Banneret** | Command cluster; Captain is a retired class, Leading meets Leader | ENM 17.2 | 10: ENM 6, CMP 1, EP 1, RS 2 | M | 0 |
| 5.4.44 | Steadfast | **Dauntless** | Near-homophone of Stand Fast; near Steeled | ENM 17.2 | 6: ENM 1, AR 2, TK 3 | M | 0 |
| 5.4.45 | Unbreakable | **Unbending** | Echoes Unyielding and HK's Unbreakable Heart; Iron-Willed carried the Will stat; Unbowed is an armour property | ENM 17.2 | 10: ENM 4, ITEMS 1, TK 5 | M | 0 |
| 5.4.46 | Thorned | **Retaliating** | Its effect is retaliation; mirrors the condition it grants (waiver W-3) | ENM 17.2 | 7: ENM 2, HL 5 | M | 4 |

### 5.5 Enemies and bosses

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.5.1 | Frozen Footman | **Still Footman** | Carries Frozen but applies no Frozen (RC9, N-K) | RS 8.9 | 2: EP 1, RS 1 | M | 0 |
| 5.5.2 | Figment | **Shadow-Puppet** | HZ 'figments' are unattackable act visuals (RC1) | RS 8.11; HZ 8 | 11: EP 3, HZ 3, RS 5 | M | 0 |
| 5.5.3 | Strongman | **Weightlifter** | Near-substring of the boss The Strongest Man Who Ever Lived | RS 8.8; EP 7.7 | 16: ENM 2, EP 3, HZ 2, RT 2, RS 1, BR 3, IR 3 | M | 0 |
| 5.5.4 | Strongman's Ring | **the Lifting Stage** | Ring family and the enemy rename | BR 5.4 | 2: BR 2 | M | 0 |
| 5.5.5 | Head Archivist | **Chief Archivist** | 'Head of the ...' head word | RS 8.7 | 12: EP 2, RS 2, TL 2, IR 1, LP 4, CH 1 | M | 0 |
| 5.5.6 | Crescendo (Storm Organist) | **Swell** | Type ability equalled the Precentor signature (RC7); 4 hits are Organ Swells, same theme | RS 8.6 | 7: RS 2, BR 4, CH 1 | M | 5 |
| 5.5.7 | Clangour (Insomniac) | **Din** | Equalled the Rousing Engine signature | RS 8.11 | 9: RS 2, BR 7 | M | 2 |
| 5.5.8 | Cross-Reference (boss) | **Footnote** | Equalled the Loremaster L6 option (NameCollision); the class keeps it | RS 8.7; BR 5.4 | 34: ARC 3, EP 3, RS 1, BR 8, EV 2, IR 3, LE 2, LP 3, CH 1, XI 3, HL 1, SV 2, SK 1, CA 1 | M | 0 |
| 5.5.9 | Read the Will | **Read the Testament** | Will is a printed defence stat ('Poor save: Will') | BR 5.3 | 5: BR 5 | M | 0 |
| 5.5.10 | Cold Ring | **Frost Tone** | Read as a trinket in the X Ring family (RC9); Frost Peal added a bell word | BR 5.4 | 2: BR 2 | M | 0 |
| 5.5.11 | Hold the Note | **Long Note** | Third 'Hold the' name; 1 hit is the CH Choir line it echoes | BR 5.4 | 2: BR 2 | M | 1 |
| 5.5.12 | Spear Lunge | **Running Thrust** | Reused the party verb Lunge | BR 5.3 | 4: BR 4 | M | 0 |
| 5.5.13 | Mourner's Embrace | **Black-Crepe Embrace** | Mourners is a Herald word RS and TL already avoid | BR 5.3 | 2: BR 2 | M | 0 |
| 5.5.14 | Chilled Pour | **Cold Pour** | Carries Chilled, applies Frozen (RC9) | RS 8.9 | 1: RS 1 | M | 0 |
| 5.5.15 | Command (boss action) | **Direct** | Duplicated the leader ability Command (N-C); approved A-3: Q5 | ENM 13.1, 17.5 | 220: MAIN 11, ABL 3, CON 3, ENM 22, ITEMS 13, CL 2, CN 1, DH 7, CMP 5, HR 8, FL 1, HZ 1, RS 11, TL 1, BR 11, EV 4, LE 2, CH 36, XI 30, SV 42, SK 5, CC 1 | **C** ⚑ | 8 |
| 5.5.16 | Strike (boss action) | **Attack** | One slot noun per slot (N-C); Strike is Shade, Herald and Stir vocabulary: Q5 | ENM 17.5; ABL 12.2 | 316: ABL 4, STAT 1, ENM 11, AR 2, CL 9, CN 1, DH 6, TK 14, W 11, SH 142, VG 25, ARC 2, CMP 1, HR 12, EP 17, HZ 9, RT 7, TL 8, BR 16, EV 1, IR 15, HL 1, CC 1 | **C** ⚑ | 896 |
| 5.5.17 | Prompts | **Boss-action warnings** | Bare 'Prompt' reads as an input prompt (BR A.5 flag) | BR 7, BR-41 | 28: AR 1, HZ 1, TL 3, BR 14, XI 5, CA 4 | M | 0 |
| 5.5.18 | Device (arena) | **Counterpiece** | BR 4.7.1 says it is not a trap, yet borrowed TL's trap-family word (RC9) | BR 4.7.1, 5.x, 13 BR-16; HZ 6.17; RS 13 | 289: ENM 7, CL 1, SH 7, EP 11, HZ 3, RT 3, RS 21, TL 89, BR 127, EV 8, CH 2, XI 5, HL 2, SK 2, CC 1 | M | 0 |
| 5.5.19 | Returns Bell | **Returns Stamp** | Bell head word (RS 8.0, RC7) | BR 5.4, 6 | 4: BR 4 | M | 0 |
| 5.5.20 | Mourning Curtains | **Mourning Blinds** | Curtain word (Drape rename, Curtain of Flame) | BR 5.3 | 3: BR 3 | M | 0 |
| 5.5.21 | the pen bar | **the pen board** | Bars only on Grilles (FL-7) | BR 4.7.1, 5.3 | 19: HZ 6, RS 1, BR 11, CC 1 | M | 0 |
| 5.5.22 | Reading Hall | **the Lectern Hall** | Next to the avoided Reading Room; unchecked in BR A.5 | BR 5.4 | 1: BR 1 | M | 0 |
| 5.5.23 | Head of the Hall | **the Dais** | 'Head of the ...' doubling with the boss | BR 5.5 | 2: BR 2 | M | 1 |
| 5.5.24 | Tide High | **Flood Stage** | Tide is an HZ rules noun; High Water, Spring Tide nearby | BR 5.4 | 4: HZ 2, BR 2 | M | 0 |
| 5.5.25 | veteran (tint) | **(no word: tint plus arrow; tooltip 'stronger than usual')** | Collides with the CMP Veteran record and book | ENM 7.1, 7.3 | 33: ENM 1, CMP 10, RS 1, EV 2, CH 12, HL 2, SV 2, SK 3 | M | 0 |
| 5.5.26 | ragged (tint) | **(no word: tint plus arrow; tooltip 'weaker than usual')** | Collides with the boss Old Ragged | ENM 7.1, 7.3 | 103: ENM 3, ITEMS 1, AR 1, EP 1, HZ 1, RT 4, RS 8, BR 48, EV 1, IR 24, LP 5, CH 4, SV 1, CC 1 | M | 0 |
| 5.5.27 | Snare (Kennel trap) | **Wire Loop** | Equalled its own family; 'Running Noose' is hanging imagery | TL 7.3 | 15: SH 1, RS 2, TL 12 | M | 0 |
| 5.5.28 | Mousetrap | **Baited Room** | Not a TL trap; Trap sense, Probe and disarm never apply (RC9) | EP 0.1 CQ1, 3.4, 6.8.1 | 86: ENM 8, EP 62, LE 3, LP 11, SV 2 | M | 0 |
| 5.5.29 | bell tripwire | **keyring tripwire** | Bell head word (RS 8.0, RC7) | RS 8.2, 8.3; TL 7.2 | 9: HZ 2, RS 2, TL 5 | M | 0 |
| 5.5.30 | Blade Wheel | **Swing Blade** | Next to the RS Wheel of Knives in the same theme | TL 7.7 | 10: TL 10 | M | 0 |
| 5.5.31 | Iced padlock | **Frost-seized padlock** | TL A.5 flag: Iced hits RS adjectives | TL 7.8 | 21: ABL 1, RS 3, TL 13, IR 1, LP 3 | M | 0 |
| 5.5.32 | Last Act | **The Prop Room** | Last overload; Green Room is a BR reserve-socket term | TL 7.7 | 7: TL 3, EV 2, LP 2 | M | 0 |
| 5.5.33 | The Stomach | **The Craw** | Substring of the Blueprint Two Stomachs | TL 7.9 | 5: TL 3, BR 1, LP 1 | M | 0 |
| 5.5.34 | Night Nursery | **The Airing Cupboard** | Night overload; next to Nursery door; Toy Cupboard failed the Dreamer's-age check | TL 7.10 | 5: TL 3, LP 2 | M | 0 |
| 5.5.35 | Rose Window | **The Tracery Loft** | Clashed with Rose Pane and the HZ rose-window Void | TL 7.11 | 6: EP 1, TL 3, LP 2 | M | 0 |
| 5.5.36 | Rood Screen gate | **Rood Screen door** | No 'gate' Door word (FC-8) | TL 7.11 | 4: TL 2, LP 2 | M | 0 |
| 5.5.37 | The Lockdown Lever | **The Lockdown Crank** | HZ A.5 bans 'Lever' as a name | HZ 7.2 | 4: FL 1, HZ 3 | M | 0 |
| 5.5.38 | Spring Tide | **King Tide** | Tide head word and the Shade verb Spring | HZ 6.9 | 1: HZ 1 | M | 0 |
| 5.5.39 | The Last Evening | **Last Show** | HZ 19.1 call 8: the theme name wins | HZ 6.9 | 16: EP 1, FL 1, HZ 4, RT 1, RS 3, TL 1, EV 2, IR 2, LP 1 | M | 0 |
| 5.5.40 | bellpull | **service pull** | Bell head word | HZ 7.8 | 1: HZ 1 | M | 0 |
| 5.5.41 | The Mourning Cord | **The Mourning Ribbon** | Cord is the rules keyword (N-K) | HZ 7.4 | 4: FL 1, HZ 3 | M | 0 |

### 5.6 Places, themes, camp and hub

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.6.1 | Stairhead | **the Downstair** | Shared the head 'Stair' with the hub; 'Back to Stairhead' read as back to the hub. Down-stair and Stair-top now point opposite ways | FL 3.4, 6.11, 8.1; XI 4.7, 4.10; BR; ENM 15.x; ITEMS; DH; RS; HZ; LE; LP; CH; ABL | 89: ABL 1, ENM 3, ITEMS 3, DH 2, FL 41, HZ 1, RS 4, BR 11, LE 1, LP 2, CH 2, XI 18 | M | 0 |
| 5.6.2 | Back to Stairhead | **To the Downstair** | Follows the rename; drops the UI verb Back (A11Y-4, AN5) | XI 4.7, 4.10 | 6: XI 6 | M | 0 |
| 5.6.3 | the Stair (hub station) | **the Banister** | Third Stair name in the hub; 'the First Step' met HL #2's 'first step' | CH 4.8, 4.12, 5.2 | 12: EV 1, CH 10, HL 1 | M | 0 |
| 5.6.4 | The Silk Archive | **The Cobweb Archive (short form: the Archive)** | Team Cherry cluster (ECHO-3, ECHO-4); the Law is webs and silence, so truthful: Q3 | RS 8.1, 8.7 and every Silk row (FL 7.6, HZ 7.6, TL 7.6, EP 7.6, LP 5.3, EV 5.3, IR 5.3, BR 5.4, CH 5.1) | 18: EP 1, FL 1, HZ 1, RT 1, RS 6, TL 1, EV 4, IR 2, LP 1 | **C** ⚑ | 0 |
| 5.6.5 | Hollowfolk | **the Fireside** | Hollow in three scopes plus the Team Cherry cluster; 'the Regulars' met the XI density label Regular: Q3 | CH 0, 0.1, 5.1, A.2, V20 (id stays); SK 4.5.1, 4.5.2; RS 7.4; EV; SV | 27: RS 1, CH 15, SV 3, SK 8 | **C** ⚑ | 0 |
| 5.6.6 | the Hollow ahead | **the next camp** | Removes the third sense of Hollow | LE 6.7, 6.15 | 6: LE 2, CH 4 | M | 19 |
| 5.6.7 | Keeper's Cart | **the Pedlar's Cart** | Collides with BR 'keepers of the sleep' (RC9) | CH 2.3 F5, 0.3, 4.1, 4.3, 5.1, 11; RS 6.8; ITEMS 10.3; LE 6.15 | 18: ITEMS 1, TL 2, EV 1, IR 1, LE 3, CH 8, XI 2 | M | 0 |
| 5.6.8 | Coat rack | **the Newel** | CH's own offered alternative; clears EV Coat Hooks and RC2 | CH 4.8, 5.2, 14.1, 14.3, A.2 | 11: EV 1, CH 9, SK 1 | M | 1 |
| 5.6.9 | trophy pole | **ribbon post** | LE renamed trophy for Trophy Tally | CH 4.7 | 7: LP 1, CH 6 | M | 0 |
| 5.6.10 | Antechamber | **Forecourt (on screen; antechamber stays an id)** | One room had two printed names | RS 3.4, 6.6, 9.4; FL 3.4, 6.11 (FL-5) | 151: ENM 11, CL 2, DH 1, SH 6, ARC 3, CMP 3, EP 1, FL 60, HZ 2, RS 31, TL 2, BR 3, EV 1, LE 10, LP 3, HL 10, CA 1, CC 1 | M | 0 |
| 5.6.11 | Night Market | **Lantern Market** | Night overload | RS 6.3, 8.12 | 2: RS 2 | M | 0 |
| 5.6.12 | Two Wings | **Split Block** | Near-duplicate of Two Wings and a Chapel | FL 7.2 | 1: FL 1 | M | 0 |
| 5.6.13 | Hunting Ring | **Hunting Circle** | Ring family and trinket kind word | FL 7.3 | 1: FL 1 | M | 0 |
| 5.6.14 | The Long Table | **Banquet Run** | Next to the theme name The Long Supper | FL 7.8 | 6: FL 1, HZ 5 | M | 0 |
| 5.6.15 | The Last Hour | **The Sentence Corridor** | Hour overload implied a timer (RC9); Last overload | FL 7.2 | 1: FL 1 | M | 0 |
| 5.6.16 | Drop-bar doors | **Slam doors** | Bars only on Grilles (FL-7; FC-8) | FL 9.1 | 19: ABL 1, FL 9, TL 9 | M | 0 |
| 5.6.17 | Way Back | **Return Latch** | Next to the Downstair action; 'back' is AN5 | FL 9.1 | 11: ITEMS 1, FL 10 | M | 0 |
| 5.6.18 | sluice gates | **sluice doors** | Matches the matrix skin; no gate Door skin | FL 7.5 | 2: FL 1, RS 1 | M | 0 |
| 5.6.19 | drawn curtain | **drawn hanging** | Drape replaced Curtain | FL 8.1 | 1: FL 1 | M | 0 |
| 5.6.20 | big-top curtain wall | **big-top canvas wall** | As above | FL 8.1 | 1: FL 1 | M | 0 |
| 5.6.21 | lodge doors | **great-hall doors** | Shared words with the Door skin lodge door | FL 8.1 | 1: FL 1 | M | 0 |
| 5.6.22 | Fairground Gate | **Fairground Turnstile** | No gate Door word | FL 7.7 | 1: FL 1 | M | 0 |
| 5.6.23 | Mist | **Fog (if revived)** | Rhymed with Haze and Glaze | RT deferred | 6: RT 6 | M | 59 |
| 5.6.24 | Seams | **Seams (keep)** | CH 'seam camps' is internal art; no clash | RS 6.2, 9.1 | 15: MAIN 1, FL 4, RS 5, CH 1, XI 2, HL 1, SK 1 | keep | — |
| 5.6.25 | Book of Nights | **Book of Nights (keep)** | The one sanctioned Night in a UI title; 'Book of Runs' made a bad pun on a memorial | CH 0.1, 4.8, 5.2, 5.3 | 25: MAIN 1, LE 1, CH 14, XI 1, HL 4, SK 4 | keep | — |
| 5.6.26 | Stairtop | **Stairtop (keep)** | Agreed CH Q1 concept; fits the place register; Stairhead moves instead | CH; MAIN 4.7; RS 7.5 | 43: MAIN 3, RS 3, EV 1, CH 24, HL 3, SK 9 | keep | — |
| 5.6.27 | Last Hollow | **Last Hollow (keep)** | Hollow stays in the camp scope only | RS 3.4, 6.5, 6.11; MAIN 4.1 | 61: MAIN 1, ENM 1, CMP 2, RS 28, BR 4, LE 6, CH 14, SV 5 | keep | — |
| 5.6.28 | Sanctum | **Sanctum (keep; waiver W-8)** | Private Sanctum is a two-word SRD spell doing another job (HOUSE-SRD-2); the rest-ban line is agreed (BR Q2) | FL; BR; ABL 15.1 | 120: ABL 6, CON 1, ENM 3, AR 1, DH 7, TK 1, EP 2, FL 17, HZ 5, RS 1, TL 3, BR 46, EV 3, LP 3, XI 1, CA 20 | keep (waiver) | — |

### 5.7 Camp figures and NPCs

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.7.1 | Kindler | **Log-Carrier** | Kindle and Rekindle are heal words at the HP station (AN3, RC9); Firewatcher met the Herald's Watchfire | CH 5.1 | 34: CH 34 | M | 0 |
| 5.7.2 | Chalkwright | **Slate-Sketcher** | XI 'Chalked' grammar; Slate-Drawer heard as furniture | CH 5.1 | 14: CH 14 | M | 0 |
| 5.7.3 | Lingerer | **Straggler** | 'lingering conditions ended' in the same rest summary | CH 5.1, 14.3; MAIN; SK | 71: MAIN 1, RS 3, CH 60, SK 7 | M | 0 |
| 5.7.4 | Indexer | **Shelver** | Index cluster | CH 5.1 | 5: CH 5 | M | 0 |
| 5.7.5 | Barker | **Showcaller** | bark is an RC8 system term; Spieler means swindler | CH 5.1 | 7: CH 6, SK 1 | M | 0 |
| 5.7.6 | Sitter | **Roll-Reader** | EV 'Sit with'; it sits by the Unwoken Roll | CH 5.1 | 42: RS 1, EV 5, CH 36 | M | 0 |
| 5.7.7 | Framer | **Gilder** | Frame cluster; Picture-Hanger carried 'hang' | CH 5.1 | 17: CH 14, HL 2, SK 1 | M | 0 |

### 5.8 Items, trinkets, charms, heirlooms, kits

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.8.1 | Hunter's Longbow | **Fowler's Longbow** | Affix word on a notable (TK renamed Hunter's Tally for this) | W 18.6, App A | 3: W 2, IR 1 | **C** ⚑ | 0 |
| 5.8.2 | Skirmisher's Spear | **Forager's Spear** | Enemy role inside an item name (AR 17.1 r13) | W 18.6 | 3: W 1, IR 2 | **C** ⚑ | 0 |
| 5.8.3 | Duellist's Rapier | **Courtier's Rapier** | duellist is a monster type | W 18.6 | 6: W 1, IR 4, SK 1 | **C** ⚑ | 0 |
| 5.8.4 | Corridor Warden | **Corridor Crossbow** | Reads as a guard role (RC9); -warden cap | W 18.6 | 5: W 1, CMP 1, RS 1, IR 2 | **C** ⚑ | 0 |
| 5.8.5 | Wardwood Staff | **Rowan Staff** | Near-identical to its own property Warding Staff | W 18.4, 18.6 | 3: W 1, IR 2 | **C** ⚑ | 0 |
| 5.8.6 | Bellwarden's Mail | **Hound-Collar Mail** | Bell* ban and the -warden cap; keeps the Kennel echo; Houndstooth misread the material | AR 14.6; IR 4.13, IR-13; RS 8.4; ENM 15.2 | 32: ENM 1, AR 3, RS 1, BR 3, IR 19, LP 3, HL 2 | **C** ⚑ | 0 |
| 5.8.7 | Ringwarden's Shirt | **Linkmender's Shirt** | -warden cap; ring kind word | AR 14.6 | 3: AR 1, IR 2 | M | 0 |
| 5.8.8 | Gatewarden's Shield | **Wicket Shield** | -warden and Gate clusters | AR 14.6 | 2: AR 1, IR 1 | M | 0 |
| 5.8.9 | Hailward Shield | **Hailstruck Shield** | Kept apart from Hailguard | AR 14.6 | 2: AR 1, IR 1 | M | 0 |
| 5.8.10 | Hushmail Hauberk | **Muffled Hauberk** | Hushed condition stem; truthful (carries Muffled) | AR 14.6 | 5: AR 1, IR 2, HL 2 | M | 0 |
| 5.8.11 | Knight's Plate | **Tourney Plate** | Roster word Knight inside an item name | AR 14.6 | 4: AR 1, IR 3 | M | 0 |
| 5.8.12 | Ironbark Hide | **Ironwood Vest** | Hide action; bark (RC8) | AR 14.6 | 4: AR 1, IR 3 | M | 0 |
| 5.8.13 | Pass-Keeper's Shield | **Pass-Watch Shield** | Keeper cluster | AR 14.6 | 1: AR 1 | M | 0 |
| 5.8.14 | Oxblood Brigandine | **Damask Brigandine** | RS palette word; colour word as the only distinction (A11Y-1) | IR 5.3, A.5 | 6: BR 2, IR 4 | M | 0 |
| 5.8.15 | Duskward Amulet | **Gloamward Amulet** | Breaks the Dusk cluster | TK 13.5 | 1: TK 1 | M | 0 |
| 5.8.16 | Torc of Seasons | **Seasonal Torc** | 'of' base breaks the epithet grammar (4.6 N12) | TK 13.5 | 7: TK 7 | M | 0 |
| 5.8.17 | Scrollwright's Token | **Scrollwright's Fob** | Token is no longer a display word (TK 5.6 review) | TK 13.7, 13.9, 19 | 17: CN 6, TK 2, CA 9 | M | 0 |
| 5.8.18 | Muse's Token | **Muse's Fob** | As above | TK 13.7 | 14: TK 4, IR 2, CA 8 | M | 0 |
| 5.8.19 | Quietus Knot | **Snuffed Wick** | Death-blow meaning (Widow's Knot precedent); third Knot family; Quiet cluster | CA 5.1 row 16 | 2: TK 1, CA 1 | M | 0 |
| 5.8.20 | Relay Thread | **Relay Ribbon** | Thread rejected corpus-wide; two charms shared a head word | CA 5.1 row 17 | 3: CA 3 | M | 0 |
| 5.8.21 | Stay Thread | **Anchor Bead** | As above; Stay is a UI verb | CA 5.1 row 18 | 4: CA 4 | M | 0 |
| 5.8.22 | Answer (heirloom line) | **Rejoinder** | Equalled the Shade trigger Answer (NameCollision) | HL 5.1 row 5, F18 | 427: MAIN 2, CON 2, ENM 9, ITEMS 3, AR 17, CL 15, CN 9, DH 11, TK 9, W 5, SH 48, VG 4, ARC 4, CMP 7, HR 16, EP 28, FL 7, HZ 7, RT 10, RS 8, TL 42, BR 22, EV 25, IR 15, LE 14, LP 8, CH 9, XI 18, HL 19, SV 10, SK 13, CA 11 | M | 0 |
| 5.8.23 | First Cut | **Quick Blood** | Cut tail in three categories (HL F18) | HL 5.1 row 2 | 11: EP 1, LP 1, CH 1, HL 6, SK 2 | M | 0 |
| 5.8.24 | Held Ground | **Planted** | Held keyword inside a flavour name; Firm Footing built a Footing cluster | HL 5.1 row 10 | 5: HL 5 | M | 9 |
| 5.8.25 | Set aside | **Not this time** | Prose hits; button wording | HL 4.1.2 | 17: ITEMS 1, DH 1, RS 1, CH 2, HL 10, SK 2 | M | 0 |
| 5.8.26 | Carried to the night's end | **Carried to the end** | Night overload | HL 4.1.5 | 4: IR 1, HL 3 | M | 0 |
| 5.8.27 | Wayfarer's Pack | **Wayfarer's Belt** | It grants a belt slot (RC9) | CMP 12; CH 4.8; HL 4.5; SK | 20: CMP 3, CH 7, HL 5, SK 5 | M | 0 |
| 5.8.28 | Grey Lacing | **Hemp Lacing** | Shared Grey with Grey Mantle (V21 g) | SK 5.1; SH 7.10 | 4: SK 4 | M | 0 |
| 5.8.29 | Patched Twice | **Leather Facing** | Twice cluster and Patched Gambeson (SK A.2) | SK 5.1, A.2 | 3: SK 3 | M | 0 |
| 5.8.30 | Copper Links | **Copper Gorget** | XI 'links'; Copper Collar met EV Collared | SK 5.1, A.2 | 4: SK 4 | M | 0 |
| 5.8.31 | Iron Rim | **Banded Face** | Bashing Rim (RC9); Iron cap | SK 5.1, A.2; CMP 8.7 | 4: CMP 1, SK 3 | M | 0 |
| 5.8.32 | Waxed String | **Linen Whipping** | Lodestring and Wax Key | SK 5.1, A.2 | 4: CMP 1, SK 3 | M | 0 |
| 5.8.33 | Worn Scabbard | **Old Scabbard** | -Worn heirloom cluster; Oxhide carried hide | SK 5.1 | 4: CMP 1, SK 2, CA 1 | M | 0 |
| 5.8.34 | Green Bark | **Green Wood** | bark (RC8) | SK 5.1, A.2 | 4: CMP 1, SK 3 | M | 0 |
| 5.8.35 | Boss Nail | **Domed Nail** | Boss rank (RC9) | SK 5.1, A.2; CMP 8.7 | 5: CMP 1, HL 1, SK 3 | M | 0 |
| 5.8.36 | Gatesplitter | **the Ironbiter (sample byname)** | Gatebreaker and Skullsplitter stems; Hinge-Taker carried an FL Portal kind | SK A.2; HL A.2 | 8: HL 5, SK 3 | M | 0 |
| 5.8.37 | Porter's armband | **Lamp-trimmer's armband** | Echoes the Porter's Satchel trinket (SK 4.5.2) | SK 0.1, 4.5.2, 5.1, 5.4 | 9: SK 9 | M | 0 |
| 5.8.38 | Blades (Shade kit) | **Twin Blades** | Group word Blades | SH 7.10; SK 5.1 | 46: CL 1, W 3, SH 12, TL 1, EV 1, IR 2, LE 1, CH 2, HL 2, SK 20, CA 1 | M | 0 |
| 5.8.39 | Launcher (Shade kit) | **Bow and Blade** | Weapon group word launcher | SH 4 decision 14, 7.10; SK 5.1 | 372: MAIN 5, ABL 2, STAT 1, CON 1, ITEMS 1, CL 12, CN 6, W 44, SH 32, CMP 5, RT 5, RS 27, TL 5, BR 55, EV 49, IR 3, CH 18, HL 6, SV 65, SK 23, CA 5, CC 2 | M | 0 |
| 5.8.40 | Kit strip | **Gear strip** | Kit is the companion word (P-2) | SK 4.6, 5.4, SK-9 | 18: ABL 1, SH 1, CH 2, XI 2, SK 12 | M | 0 |
| 5.8.41 | Venom Oil | **Poison Oil** | Rider is Poisoned, not Envenomed (N-K) | CN 16.3, 7.5 | 1: CN 1 | M | 0 |
| 5.8.42 | Venom Arrows | **Poison Arrows** | As above | CN 16.3 | 1: CN 1 | M | 0 |
| 5.8.43 | Serrated Arrows | **Serrating Arrows** | One stem with Serrating Oil | CN 16.3, CO-9 | 1: CN 1 | M | 0 |
| 5.8.44 | Prize Balloon | **Fairground Balloon** | Prize hamper is the Plain family | LP 5.3 | 4: LP 4 | M | 0 |
| 5.8.45 | Grave-goods casket | **Burial casket** | Grave Goods is the run-record line | LP 4.7.11 | 7: RT 1, TL 1, LP 5 | M | 0 |
| 5.8.46 | Swallowed bauble | **Gut-Lodged bauble** | Swallowed kept for the signature only | LP 5.3 | 3: LP 3 | M | 0 |
| 5.8.47 | Ring key | **Ringmaster's Key** | ring is a trinket kind word (waiver W-14 for the title word) | TL 7.7 | 6: TL 3, IR 1, LP 2 | M | 0 |
| 5.8.48 | Answers here | **Shines here** | Answer is the Shade trigger; Fits here would meet the fit line | IR 0.1, 5.1, 6.1 | 18: ITEMS 1, IR 16, CA 1 | M | 0 |
| 5.8.49 | talisman (category) | **belt trigger** | Talisman reads as a charm in ordinary English (RC9); id stays | CN 14; IR 4.2 | 197: ABL 5, STAT 1, CON 14, ENM 2, ITEMS 26, CN 95, DH 16, TK 4, IR 16, LE 3, CA 15 | M | 2 |

### 5.9 Interface and system terms

| # | Current | Final | Reason | Sections named by the docs | Grep sites of the current name (whole word, case-insensitive; file and hits) | Type | Final-name grep |
|---|---|---|---|---|---|---|---|
| 5.9.1 | Seeded run | **Run code (launcher: 'Play a run code')** | One concept had two names; 'seeded' rejected by SV A.5 | SV 4.7, 4.9, 14.2; CH 4.8 | 30: MAIN 2, RS 1, EV 1, CH 8, SV 18 | M | 1 |
| 5.9.2 | Seeded night | **Play a run code (hub row)** | As above; Night overload | CH 4.8 | 3: CH 3 | M | 0 |
| 5.9.3 | version-crossed | **older build** | Plain and truthful | SV 4.9, 4.10.1 | 12: CH 5, SV 7 | M | 0 |
| 5.9.4 | Quiet camp | **No idle chatter** | 'the quiet camp' is camp 2's name | CH 4.13 | 8: RS 1, CH 7 | M | 0 |
| 5.9.5 | Plain text for speakers | **Legible captions** | Six 'Plain' head words | CH 4.13 | 2: CH 1, XI 1 | M | 0 |
| 5.9.6 | background perk | **Calling perk** | Background is a D&D branded concept (RS 7.6) | MAIN 4.7 | 7: MAIN 1, HL 1, SK 5 | **C** ⚑ | 0 |
| 5.9.7 | boons (MAIN 4.5) | **offers** | boon meant two things; the 4.7 starting boon keeps it: Q4 | MAIN 4.5 | 111: MAIN 6, ABL 2, CON 3, ITEMS 5, AR 1, W 1, CMP 2, HZ 1, RS 1, EV 2, IR 1, LE 3, CH 1, HL 10, SV 19, SK 45, CA 8 | **C** ⚑ | 245 |
| 5.9.8 | Simple (complexity) | **Straightforward** | Reused the training word | CL 8.3–8.6, 12.1 | 74: MAIN 3, ITEMS 3, CL 11, W 9, SH 3, VG 3, ARC 13, CMP 2, HR 2, EP 2, BR 1, IR 4, CH 10, SK 8 | M | 0 |
| 5.9.9 | Complex (complexity) | **Intricate** | Reused category words | CL 8.3–8.6, 12.1 | 16: CL 4, ARC 6, HZ 4, TL 1, BR 1 | M | 0 |
| 5.9.10 | Medium (complexity) | **Involved** | Reused the armour category word | CL 8.3–8.6, 12.1 | 171: ABL 1, STAT 1, ENM 4, AR 53, CL 16, CN 1, DH 1, SH 2, VG 1, HR 10, RT 3, BR 24, IR 3, LE 4, CH 6, XI 4, HL 1, SK 15, CA 21 | M | 1 |
| 5.9.11 | Weak: Will | **Poor save: Will** | Weak is the damage-ladder keyword | ENM 5.3 | 3: ENM 1, ARC 1, EV 1 | M | 0 |
| 5.9.12 | Treasure strip | **Treasure field** | XI-19 retirement not yet applied | LE 6.15; LP 4.12, LP-28 | 13: LE 2, LP 5, XI 5, CC 1 | M | 31 |
| 5.9.13 | Trinkets (slot row) | **Worn items** | One slot had two names; AR-30 agreed | ITEMS 5.2 | 445: MAIN 6, ABL 6, STAT 2, CON 7, ENM 4, ITEMS 62, AR 47, CL 25, CN 20, DH 10, TK 84, W 1, SH 6, VG 10, ARC 8, CMP 24, HR 7, EP 2, FL 2, HZ 10, RT 9, RS 17, TL 2, BR 1, EV 1, IR 41, LE 7, LP 1, CH 1, XI 1, HL 3, SV 1, SK 4, CA 11, CC 2 | M | 6 |
| 5.9.14 | Reaction policies | **Always / Only if it matters / While half full / Never / Ask me** | Raw ids must never show (P-3) | ABL 7.3 | 29: ABL 1, CON 1, ENM 1, AR 3, CL 2, W 1, SH 2, VG 3, ARC 5, CMP 4, HR 5, RT 1 | M | 0 |
| 5.9.15 | dormant (item) | **untold ('? untold')** | Dormant is the enemy awareness state (P-2); agreed ITEMS term: Q5 | ITEMS 4 d4, 11; IR 6.1; TK 15; CA 4.8 | 347: MAIN 2, STAT 4, CON 3, ENM 36, ITEMS 26, AR 1, CN 1, DH 5, TK 24, W 1, SH 4, VG 1, EP 104, FL 9, HZ 6, RS 17, TL 13, IR 16, LE 2, LP 47, XI 7, CA 17, CC 1 | **C** ⚑ | 0 |
| 5.9.16 | Kept (charm pip) | **Stored** | Collides with the internal Keep label and Keep card; matches the lane words 'stored moments' | CA 4.7, 6 | 631: MAIN 5, ABL 4, STAT 2, CON 4, ENM 10, ITEMS 9, AR 3, CL 6, CN 6, DH 18, TK 9, W 3, SH 9, VG 3, ARC 10, CMP 18, HR 12, EP 9, FL 23, HZ 18, RT 20, RS 26, TL 45, BR 33, EV 19, IR 35, LE 42, LP 31, CH 23, XI 30, HL 24, SV 88, SK 17, CA 17 | M | 153 |
| 5.9.17 | Spent (charm pip) | **Used** | Collides with spent field and Resolve Spent | CA 4.7, 6 | 396: MAIN 2, ABL 19, STAT 1, CON 15, ENM 9, ITEMS 20, AR 20, CL 11, CN 13, DH 7, TK 35, W 12, SH 27, VG 21, ARC 9, CMP 5, HR 27, EP 4, FL 2, HZ 4, RT 7, RS 2, TL 17, BR 10, EV 3, IR 6, LE 11, LP 1, CH 2, XI 26, HL 3, SV 9, CA 27, CC 9 | M | 383 |
| 5.9.18 | Dormant (charm pip) | **Idle** | Dormant is the enemy state; untold is the item line | CA 4.7, 6 | 347: MAIN 2, STAT 4, CON 3, ENM 36, ITEMS 26, AR 1, CN 1, DH 5, TK 24, W 1, SH 4, VG 1, EP 104, FL 9, HZ 6, RS 17, TL 13, IR 16, LE 2, LP 47, XI 7, CA 17, CC 1 | M | 76 |
| 5.9.19 | fired (charm log) | **went off** | 'fired' sits next to Fire damage in combat logs (RC9) | CA 4.5, 5.4 | 31: MAIN 1, CON 2, ENM 1, DH 2, TK 1, W 1, VG 1, ARC 1, EP 2, HZ 1, EV 4, IR 2, LE 2, LP 1, CH 1, SV 1, CA 7 | M | 0 |
| 5.9.20 | Charms catch moments | **Charms hold moments** | Catch ban; agreed CA Q1 / TK 5.5 text: Q5 | CA 0.2 Q1, CA-1; TK 5.5 | 7: TK 2, CA 5 | **C** ⚑ | 0 |
| 5.9.21 | spent field | **cleared field** | Spent pip collision (history) | XI 4.4.5 | 16: LE 2, XI 14 | M | 0 |
| 5.9.22 | Trace on glimpse | **Trace on sight** | Glimpse is an EV card form | XI 4.1.5 | 9: XI 9 | M | 0 |
| 5.9.23 | Held (badge) | **paused** | Held is a condition (P-2) | LP 4.7.4 | 489: MAIN 4, ABL 5, STAT 5, CON 20, ENM 7, ITEMS 3, AR 3, CL 5, CN 3, DH 7, TK 10, W 7, SH 33, VG 6, ARC 30, CMP 6, HR 31, EP 2, FL 2, HZ 15, RT 10, RS 26, TL 5, BR 8, EV 12, IR 26, LE 15, LP 63, CH 15, XI 9, HL 29, SV 7, SK 24, CA 35, CC 1 | M | 5 |
| 5.9.24 | Signature item | **the boss's prize (display)** | Signature reserved for the announced boss move: Q5 | ITEMS I-9; IR | 16: ENM 8, ITEMS 3, RS 1, LE 3, CH 1 | **C** ⚑ | 0 |
| 5.9.25 | Slot Recovery | **Slot Renewal** | Recovery is the HP resource (ABL 6.5 itself flags it): Q5 | ABL 6.5 | 70: ABL 5, CON 1, ENM 1, CL 4, CN 3, DH 5, TK 1, W 1, VG 1, ARC 40, HR 8 | **C** ⚑ | 0 |
| 5.9.26 | Ward (role tag) | **Buffer** | Reused the Ward head word CMP avoided; Shelter is an AR property | CMP 8.3 | 349: ABL 4, CON 11, ENM 18, ITEMS 2, AR 15, CL 5, CN 22, DH 39, TK 52, W 4, SH 26, ARC 5, CMP 5, HR 2, EP 51, FL 2, RS 2, TL 2, BR 2, EV 14, IR 4, LP 1, CA 60, CC 1 | M | 0 |
| 5.9.27 | Protector (role tag) | **Defender** | Protector stays an internal profile; 4 prose hits | CMP 8.3 | 47: ENM 3, CL 9, DH 2, VG 2, CMP 25, HR 3, EP 3 | M | 12 |
| 5.9.28 | Carried hire | **Held-over hire** | 'carry' stem in ten strings | CMP 3.5, 7.5 | 35: MAIN 1, CMP 32, LP 2 | M | 0 |
| 5.9.29 | Same as last night | **Same as last run** | Night is not a unit of play | CH 4.10, 4.15.5 | 1: CH 1 | M | 0 |
| 5.9.30 | this code cannot be dreamed on this build | **This run code can't be played on this build.** | Dream word in an error (P-1) | SV 5.5 | 3: SV 3 | M | 0 |

---

## 6. Glossary and Plain names (lead's call; NP-2)

### 6.1 The Rules Glossary (closed)
One entry per keyword: name, one-line definition, icon, colour, tooltip, related keywords, translator note. Reachable from every keyword tooltip in combat and out. **Closed**: adding a keyword is a lead's call and every document that uses it is updated. Lore text never redefines a keyword (N-M).

**Tier 1, the HUD tier (M2; 60 core entries plus the condition set):**
- **Conditions:** the M1+M2 set of CON 16 (32 of 32; settled), each with its icon. Later conditions enter as their content ships, after the renames in 5.3 (Tangled, Frenzied, Heedless, Limping as flavour of Scarred).
- **Damage:** the ten damage types and three families (STAT 8.1; on the SRD allowlist; theming open); the ladder **Weak, Normal, Resistant, Immune** (one meaning each; "Resisting (type)" prints "Resistant (type)", Q5).
- **Abilities, saves and skills:** the six abilities with Presence (STAT 5.1), the saves (Will printed as "Will", never inside a boss move), the skill tiers including **Mastery** (skills only; never on a verb, W 7.4).
- **Universal actions:** Attack, Move, Dash, Disengage, Dodge, Help, Hide, Search, Shove, Grapple, Slip past, Probe, Steal, Treat, Tend, Escape, Rally, Frighten, Opportunity attack, Trigger Shot, Hear out (camp), Tap the wall.
- **Resources and counters:** HP, Recovery, Supply, Coin, spell slot, pool, Concentration, Dream's Due (W-DREAM-1 a).
- **Area shapes:** Burst, Aura, Cone, Line (printed as icons with a tooltip).
- **Awareness and morale:** Dormant, Stationary, Patrolling, Alerted, Engaged, Pursuing, Searching, **Watchful** (Q5); Emboldened, Demoralised, **Routed**, Cowed.
- **Rarity:** the SRD 5.1 tier names (ITEMS 12.5; attributed).
- **Exploration grades:** trap grades **Found / Hunch / Unfound**; check grades **Clean / Jam / Botch**; DC tiers **Low / Fair / Hard**.

**Tier 2 (Later and M3+):** keywords enter with their content (Cord, Freefall, Clasp, Counterpiece, Peril tier). Each new keyword passes G3 against the whole glossary.

**Settings and system words given names here (MAIN pending items):** the visible difficulty setting below tier 0 is **Easier** (the setting reads "Difficulty: Easier"; the hub shows the **Easier lamp**); the telemetry consent row is **Share play data** (On/Off); the reserved run-modifier slot is the **Oath slot** on the Depth gauge (HL); the Later "Story" rung becomes the Easier rung and "Hard" keeps the CMP wording **Mortal companions**.

### 6.2 The Lore Codex (open; RS owns, CH 4.15.8 hosts; hook)
Pages unlock as the player plays: Residents (the Fireside), Old Maladies, Oddments, the Boss Ledger, Dream Rules pages, Hearsay. Flavour only; entries link to glossary keywords and never restate a rule differently.

### 6.3 Translator glossary
Every player-facing key carries: literal sense, register (4.2), tone note, grammatical gender and number, `article` field, plural form, translate / transliterate / keep, wordplay to keep or drop, and the narrowest widget it appears in. This is HL NR10 extended to every name (7).

### 6.4 Plain names (extends HL-27; agreed HL Q1 for the setting itself)
A display-only setting. When on, every flavour-layer name in a scope that has a Plain label is replaced on screen by a rule-word label. Data, ids, Whence and lore are unchanged.

| Scope | Plain label form | Example |
|---|---|---|
| Malady (HL-27) | `Malady: <rule tag>` | Choler → Malady: Easy Opening |
| Curse cost line (HL-27) | `Cursed: <rule tag>` | Plodding → Cursed: One Use Fewer *(illustr.)* |
| Oath (HL-27) | `Oath: <rule tag>` | Oath of the Short Fire → Oath: Shorter Rests *(illustr.)* |
| Charm (new) | `Charm: <output keyword> on <trigger>` | Hangnail → Charm: Helped when a condition fails |
| Trinket and notable property (new) | the property's keyword line | Strider → +1 speed *(illustr.)* |
| Enemy affix (new) | the granted keyword or a short rule | Plated → Heavy armour *(illustr.)* |
| Boss ability on the banner (new) | keyword effect under the boss name | Countdown → Area 3 · Dazed *(illustr.)* |
| Curiosities, themes, places, camp figures, given names | unchanged (whimsy is the content) | — |

Rules: a label is keyword-only and at most 24 characters; **the rule tag alone is unique within its scope** (contests HL-27's prefix uniqueness; told plainly T7), so Ill-Omened's and the Spleen's shared "First Stumble" split (the curse cost line becomes "Stumbles Early"); the screen reader announces the Plain label **first** for scopes with obscure names (Maladies, curses, Oaths), even with the setting off (A11Y-3); labels pass G3, G8, G11 and G12.

---

## 7. Localisation and accessibility (lead's call; NP-2)

### 7.1 Length and pseudo-localisation
- Budget **+35%** for display names and **+50%** for strings under 10 characters (L10N-1). Every chip, plate, card and banner truncation test runs on pseudo-localised strings at that inflation before content lock. The caps in 4.2 are English source lengths checked against the narrowest widget at the inflated length.
- No ToUpper or ToLower on names; "VICTORY" becomes an authored "Victory" (ITEMS 8.2; NP-11). Turkish dotted and dotless i and German ß are why.

### 7.2 No assembled strings
- Every sentence is one whole-string key with named placeholders; translators get the full list of values each placeholder can take (L10N-3). Fragments that are assembled today are listed for the owners: "Oath of the <adj> <noun>" (HL; stored as whole names), "<Name> of ?" (IR; one key per family with the epithet slot), "<byname>. Felled the <boss>." (IR), "Maladies abroad: <names>" (HL-19; list template with locale separators), "Grace: <condition>" (EV), "Guards: <signature name>" (ENM 15.2).
- Names carry no article or possessive the grammar must change (L10N-4); the article field renders it.

### 7.3 Locale traps known now (NR10)
- **Choler**: Spanish *cólera* means cholera; `localeTermOverride` for es (and a check for Choler/Collared near-homophony in en speech).
- **Melancholia, Spleen**: everyday words in Romance and German locales; Melancholia keeps its mandatory tone sign-off and Atrabile fallback (agreed HL Q1).
- **Acedia**: Italian *accidia* is the deadly sin; locale review.
- **Hyphenated compounds and puns** (Grip-marks, Heart-won, Thumb-Worn, Road-Scuffed, Star-Crossed, Iron-Grip, Hound-Collar, Log-Carrier, Slate-Sketcher, Roll-Reader): translator notes say what to keep (meaning, sound or shape).
- **Possessives with a trade noun** (Fowler's, Courtier's, Forager's, Scrollwright's, Lamp-trimmer's): translate the trade; note gender.
- **Proper nouns**: every name carries a translate / transliterate / keep flag. Recommended keep (transliterate if needed): Stairtop, Brask, Tolly, Old Ragged, Madame Ashes. Recommended translate: everything else.
- Each shipping locale runs its own blocklist, echo list, collision grep and native-speaker sensitivity read before M4 content lock (PROC-2; settled NR10 timing).

### 7.4 Accessibility
- **Spoken labels for every glyph** used in a name or verdict (A11Y-2): ⛨ "prevents", ⟲ "costs or reach", ✦ "channels", ◈ "stored moment", ▸ "button", ? "untold", ▲/▼ "stronger/weaker than usual", ⇄ "another way", = "same", ✕ "cannot use"; Rhythm strip beats "off beat / main beat / big beat" (BR 4.3.1; "Lead" and "Down" are a TTS homograph and an AN5 word).
- **Pronunciation or SSML hints** (A11Y-3) on Atrabile, Phrenitis, Pituita, Adustion, Jactitation, Tarantism, Acedia, Choler, Precentor, Pylorus, Antrum, Seneschal, Gilder, Wind-Down Hammer ("wind" as in clock). Maladies announce the Plain label first (6.4).
- **No TTS homograph** in a new name (lead, wind, tear, bow, wound, close, live, row): this rejected "Live Water", "Bow your head" and the "Lead beat" label.
- **No name is a UI button verb** (Rest, Answer, Kneel, Cancel, Back, Accept, Continue): this renamed Answer (heirloom line), Kneel, "Back to Stairhead" and kept Counterpiece payoff kinds internal.
- **No colour-only sibling pair** (A11Y-1): Oxblood Brigandine → Damask Brigandine; colour words stay only where icon and shape also differ (Grey Mantle).
- **No glyph-confusable spellings** (l/I/1, O/0, rn/m) in key names; none found in the finals.

---

## 8. Numbers (counts)

### 8.1 The harvest
| Measure | Count |
|---|---|
| Harvested entries (all 34 approved documents) | 2,893 |
| Distinct strings | 2,769 |
| Harvest status: placeholder / approved-final / unclear / flagged-to-naming-pass | 1,683 / 623 / 501 / 86 |
| Largest categories: system term / UI surface / ability / verb or action / condition / property / item family | 521 / 392 / 335 / 159 / 137 / 136 / 131 |
| Mentions of "naming pass" in the corpus | 205, in 27 files |
| Collision rows in the sweep | about 250 |

### 8.2 Outcomes of the harvested entries (Appendix B)
| Harvest status | Final as written | Renamed | Waiver | Internal | Retired | Total |
|---|---|---|---|---|---|---|
| placeholder | 1,465 | 162 | 46 | 9 | 1 | 1,683 |
| approved-final | 579 | 35 | 8 | 1 | 0 | 623 |
| unclear | 441 | 36 | 4 | 20 | 0 | 501 |
| flagged-to-naming-pass | 24 | 49 | 7 | 5 | 1 | 86 |
| **Total** | **2,509** | **282** | **65** | **35** | **2** | **2,893** |

Appendix B's outcome per entry is matched by script against the register in 5 (exact name, base name without a parenthetical, or any " / " part). Where a generic word has two senses (Open, Hidden, Strike, Set), the register row names the renamed sense; the other senses stay final, so Appendix B can show "final as written" for the kept sense. **Section 5 governs.**

### 8.3 The register (section 5)
| Group | Mechanical | Client | Keep / waiver / retired | Rows |
|---|---|---|---|---|
| 5.1 Legal, SRD and outside IP | 33 | 15 | 0 | 48 |
| 5.2 Sensitivity, tone and premise | 24 | 7 | 2 | 33 |
| 5.3 Keyword layer | 33 | 13 | 1 | 47 |
| 5.4 Classes and class abilities | 39 | 7 | 0 | 46 |
| 5.5 Enemies and bosses | 39 | 2 | 0 | 41 |
| 5.6 Places, themes, camp and hub | 21 | 2 | 5 | 28 |
| 5.7 Camp figures and NPCs | 7 | 0 | 0 | 7 |
| 5.8 Items, trinkets, charms, heirlooms, kits | 43 | 6 | 0 | 49 |
| 5.9 Interface and system terms | 24 | 6 | 0 | 30 |
| **Total** | **263** | **58** | **8** | **329** |

### 8.4 Other counts
- Client questions: **6** (Q1–Q6), covering the 58 client rows (Q5 carries 45 of them, in three bundles).
- Change rows: **NP-1 to NP-38** (13).
- Recorded waivers: **W-1 to W-24** plus **W-DREAM-1** (9.3).
- Families ruled internal: **14** (4.8).
- New data files: **8** (9.1). New validator: **1** (V24, 9 rules).
- Rules Glossary HUD tier: **60 core entries plus the 32 M1+M2 conditions** (6.1).
- Candidates rejected in the synthesis after the critique: **about 60** (A.1).
- Current names whose own whole-word grep hit more than 50 lines (high-traffic restatements): Stairhead 87, Viaticum 230, Nightmare tiers 165, Silk 162 (theme word), Dreamfall 34, Upended 16 left (5, sixth column).

---

## 9. Data and validator (lead's call; NP-1)

### 9.1 Data (owner: the lead; seeded at M2; manifest-listed so `V7.NameRegistry` loads them)
| File | Contents | Gate |
|---|---|---|
| `srd_names.json` | Every SRD 5.1 and 5.2 spell, monster, magic item, feat, class, subclass and class-feature name, from the SRD documents themselves (never from memory; LEGAL-2) | G5 |
| `srd_allow.json` | The allowlist of 4.5 N5d, each entry with its attribution line | G5 |
| `ip_floor.json` | LEGAL-1 and LEGAL-3 brands, Product Identity monsters, named-wizard prefixes, settings, characters | G6 |
| `echo_list.json` | ECHO-1 seeds, the game titles found, each with studio id for the cluster test | G7 |
| `sensitivity.json` | Slurs, living diagnoses, living sacred practices, religions' figures and sacraments, hate symbols, gendered terms, suicide and hanging words, "bile" (except Atrabile); with per-locale parts | G8 |
| `gazetteer.json` | Real places, brands, products, people, well-known film and book titles | G10 |
| `reserved_premise.json` | The reserved words of 4.7 and the W-DREAM-1 list | G9 |
| `retired_names.json` | Every old name from 5 (including Upended, Stairhead, Hollowfolk if Q3 A, Lingerer, Keeper's Cart, Dreamfall, Silk Archive if Q3 A, Viaticum if Q2 A, Night-Mare, Mousetrap, Antechamber Catch-up, Underpillow, Ashen Knot, Treasure strip, Recruit soldier, Linger, Hush, Last Ward, Last-Breath Charm, Ward Draught, the CN 16.7 cuts), and every rejected candidate from A.1 | G4 |

Each name record in the string table gains: `register` (4.2 row), `article`, `plural`, `properNoun`, `translate` (translate / transliterate / keep), `ssml` (optional), `plainLabel` (where 6.4 applies), `waiver` (id, if any), `internal` (true for 4.8 families). These are text fields, not code.

### 9.2 Content rules
- Every display name has a register row, an article value and a plural (V24 r8).
- Every waiver names its id, reason, approver and date (V24 r9).
- No record is written for a name still marked placeholder after its wave's freeze (10).

### 9.3 V24 `NamingPass` (registered by NP-1; owner the lead; runs on `V7.NameRegistry`; phase split per SV-20: static rows at build, string rows at content build)
| Rule | Check | Result |
|---|---|---|
| r1 | Exact equality across all registries (Classes 10, TK 15, AR 17.1 r13, V14 (f), V21 (g) pool), whole word, on display strings, fallbacks, Plain labels, epithets, bynames, given names and every `localeTermOverride` | fail |
| r2 | Retired name reused (`retired_names.json`) | fail |
| r3 | Keyword collisions (G3): keyword stem inside a flavour name without the thing applying it; keyword pairs at edit distance ≤ 2 or sharing three first letters in a family; known homophone pairs | fail |
| r4 | SRD (G5), IP floor (G6), sensitivity (G8), gazetteer exact (G10) | fail (allowlist and waivers pass) |
| r5 | Echo (G7) and the cluster test | exact fail; shared word flag |
| r6 | Internal families (4.8) never on a display surface; any camelCase id on screen | fail |
| r7 | Premise words (G9) outside W-DREAM-1; "mimic" on screen | fail |
| r8 | Register row, caps at +35%, article and plural fields present; head-word cap (4.9); filtered substring rule (4.10) | flag (caps fail at content lock) |
| r9 | Waiver records complete | fail |

**Waivers recorded now (9.3; each with reason in 5 or 2.3):** W-1 the Ember family (CONF-2); W-2 affix mirrors Warded, Chilling, Blurred, Regenerating, Riposting (each grants exactly its condition); W-3 Retaliating (the Thorned rename, a condition mirror); W-4 Wand of Sparks, Wand of Rime Lance, Mending Word (ARC-20); W-5 Alerted (PHB Alert feat, generic); W-6 Swallow and Wail (generic verbs, partial SRD match); W-7 Disinherit ("inherit" governs heirloom strings only); W-8 Sanctum (two-word SRD spell, different job; agreed BR Q2 line); W-9 "the road not taken" (public-domain title); W-10 the Rousing Engine (contains the Herald's Rouse; boss title); W-11 Heirloom with Deeds, and "boon" for the starting boon (Darkest Dungeon and Hades echoes; one word, one scope each); W-12 Bitter Draught beside Bitter Truth (cross-scope head word); W-13 the "-ward" neck-lane suffix (TK amulets and torcs, "prevents X"); W-14 Ringmaster's Key (title word, not the kind word); W-15 Omen Retelling (EV; the Omen word is the card's subject); W-16 Bone charms across waves (Twitch Bone and Wishbone M3, Jawbone M4); W-17 Gust and Traced (single generic words, HOUSE-SRD-2); W-18 "Not yet read: may Cling" (1 EV prose hit); W-19 the Oath form against the Sacred Oath pattern (no name match); W-20 Dazed with Addled reserved; W-21 Shade (one Team Cherry echo once the cluster is broken; if Q3 is answered C, Q6 B applies); W-22 Tactician, Weaponmaster, Loremaster, Binder, Elementalist, Inspire (generic echoes); W-23 Cell Zero (one place reused) and the Den (template internal); W-24 church titles under the cap (Deacon, Precentor, Chorister, Sacristy, Vestry). **W-DREAM-1** (Q1).

### 9.4 Reports
`NamingDrift` (new; registered in SV 6.10 by NP-1): per build, the count of display strings that fail r1–r9, the waiver count by id, and retired names found in text. Target zero fails at each wave's freeze.

---

## 10. Staging (lead's call)

| Stage | What lands | Gate |
|---|---|---|
| **Now, on approval** | NP-1 (V24, data-file stubs, `retired_names.json` seeded with every old name in 5); NP-2 (the N-rules); NP-3 (W-DREAM-1, per Q1); CMP-27 closed (NP-7); SK-13 closed (NP-27); CC-65 restatements (NP-6); the "Catch" ruling for CA (NP-14) | Owners apply their rows; post-apply grep for every old name finds only history notes (12) |
| **M1 records** | Every M1 name in 5 applied before its record is written; V24 static rows r1, r2, r6 live | No M1 record with a retired or placeholder name |
| **M2 (full run)** | All M2-content finals applied; V24 all rows live (as V22 (e), V14 (f), V20s, V21 (g) go live at M2); the RS 7.4 tone-by-depth review of boss and signature names (settled gate) runs on the finals; Rules Glossary HUD tier; Plain names scopes; translator notes on every M2 key | `NamingDrift` zero fails at M2 freeze |
| **M3** | The charm display words are already ruled (NP-14), so the M3 card-text freeze CA set has nothing pending from this pass; M3 content (charms, Seams, Heart Wager rooms, the Cobweb Archive per Q3) applies its finals | `NamingDrift` zero |
| **M4** | The sensitivity read of Malady names, Malady flavour, folk cost lines and codex plates, due before M4 content lock and held at content-complete so fallbacks (Atrabile, Adustion, Jactitation, Savourless) can still be swapped in (settled HL); locale passes (7.3); The Waking City renamed or waived (Q1) | Locale `NamingDrift` zero; sensitivity sign-off |
| **Later** | Later classes and conditions (Ravager, Frenzied, Heedless, Tangled, Ascetic per Q6) enter through G1–G14 | — |
| **Store page** | Trademark clearance and exact-match search for the title and marketed names (SEARCH-1) | Legal sign-off |

---

## 11. Ownership

| Item | Owner | When |
|---|---|---|
| The N-rules, the Rules Glossary, the registry data files, V24 and `NamingDrift` | The lead | Now; seeded M2 |
| Final names and waivers | The naming pass (this document; the client for the C rows) | On approval |
| Restating each renamed name in its document | Each document's owner, by NP row | On approval, by wave |
| The RS 7.4 tone-by-depth review of boss, signature and bark names | RS owner and the tone review (settled) | M2 |
| Sensitivity read (G8 waivers; Maladies; Totem replacement) | Sensitivity reader | M4 content-complete (Maladies); before records (Tipstaff, Q2) |
| Locale passes | Localisation owner per locale | Before M4 content lock |
| Trademark and store search (G14) | Legal | Before the store page |
| SRD 5.2.1 check of every SRD shape and the `srd_names.json` source text | The lead with each class owner (settled: classes 19, ARC 19, HR 19) | Before records |
| LE hook names (The Long Shot; Tithe Bowl, The Boatman's Chit to tone review; Blood Price) | Camp and hub owner with LE (LE A.5) | M3 |
| HZ Treadwheel stove (rule, not name) | HZ owner (request, NP-16) | M2 |

---

## 12. Binding requirements and how they are met

| Requirement (source) | How this document meets it |
|---|---|
| RS 7.6: no D&D-specific names; invented liturgies only; no suicide imagery, asylum, child victims, ethnic caricature, meme parody (agreed RS Q3) | 4.5, 4.6; 5.1 SRD and IP renames; 5.2 Viaticum, Last Rites, Holy Water, Coffle, Hang it, Smother |
| RC9: names may be whimsical but never lie (agreed RS Q3) | 4.4 blind-guess test and N4b; 5 renames of Frozen Footman, Venom Oil, Unshaken, Thunderstone, Shockwave, Grasping Roots, Chilled Pour, Wayfarer's Pack, Corridor Warden, Mousetrap, Device, Cold Ring, The Last Hour |
| RS 7.5 premise; CA "implies the Dreamer's age" check | 4.7, W-DREAM-1 (Q1); 5.2; age check rejected Hobby-Horse, Rocking Horse, Toy Cupboard, Rattle |
| RS 8.0 frozen names; V7.NameRegistry | Registry stays V7's; V24 runs on it (9); retired names registered |
| Classes 10 NameCollision; four rest tools with distinct first words | Dig In, Respite, Knit, Take Heart (5.1, 5.3); Cross-Reference, Answer, Lullaby, Charge, Honed, Protector exact equalities removed |
| W 7.4 "verb" never "mastery"; TK 15; AR 17.1 r13 | N4b; V24 r1, r3; Hunter's Longbow, Skirmisher's Spear, Honed, Unshaken, Catcher, Clench |
| IR 4.6/6.1 bynames, suffix epithets; V14 (f) | N12; Torc of Seasons, Sigil of Aid; Gatesplitter sample → the Ironbiter |
| HL NR1–NR10, registers, blocklist (agreed HL Q1) | Kept and extended: MH-1 outranks NR1 (T3); bile rule held (Gall Slick, Sour Spit); Oath of the Cold Iron (NR5 head noun unique); Plain tags unique (T7); translator notes (7) |
| SK 4.5.1 given names, 4.5.2 Oddments (agreed SK Q2, Q3) | Eight given-name flags resolved (5.8); Porter's armband → Lamp-trimmer's armband; usher line → carter |
| CH Hollowfolk trade names, AN1–AN13 | Fireside figures stay trade nouns (5.7); AN3 (Kindler), AN4 (Homecoming, Bedside), AN5 ("Back", "Down") applied |
| CA-12, V23s: "Catch" never printed; "token" reviewed; "talisman" dropped | 4.8 Catch row; NP-14; Token dropped; "belt trigger" category |
| SK-13 starting-gear term | Starting gear final; kit is the companion word |
| CMP-27 Levy soldier before records | NP-7, now |
| CC-65 Sprawled restatements | NP-6, now |
| SV 4.9 seed and notice words | 5.9 run-code family; NP-24 |
| XI-19 canonical names | Treasure strip retired in LE, LP (NP-22, NP-25) |
| HR 19 Spur / Interject; VG 18 Hold the Line; HZ Cord; BR Prompts; TL Iced | 2.2 rulings; 5 rows |
| Every name grep-checked (all designs) | Final-name grep column in 5; current-name sites by script (A.2) |
| Internal ids untouched (N-Q, the brief) | 4.8 internal families; every rename is a display string; ids such as `talisman`, `antechamber`, `viaticumFallback`, `arenaSeal`, `Hollowfolk` validator name stay |
| This document edits no parent document | 13 rows are applied by owners |

---

## 13. Changes to approved documents

Applied by the owners of the sections they touch, on the client's approval of this document; **this document edits no parent document itself**. Every row names its documents and sections; the renamed names for each row are the rows of 5 whose "Sections" column names that document (5 is the exact list; each row below summarises). Rows that touch **decided**, **agreed** or **settled** text, or a client-confirmed name, are flagged ⚑; those need the answer of the question named. Every row carries the same check after applying: a whole-word grep for each old name finds it only in history notes, rename records and this document.

| # | Document / sections | Change | Flag | Approval | Wave |
|---|---|---|---|---|---|
| NP-1 | RS 11.2 suite table; SV 5.2 (V1–V24), SV 6.10 | Register V24 `NamingPass` and the report `NamingDrift`; manifest lists the eight data files (9) | — | Lead | Now |
| NP-2 | RS 7.6 naming registers and rules; HL 5.3 NR2, NR6, HL-27; CL 10 NameCollision; AR 17.1 rule 13; TK 15; IR 9.2 V14 (f); CH V20s; SK V21 (g); CA 9.3 V23s | Adopt the N-rules of 4 by reference: blocklist outranks NR1; SRD function test and allowlist; echo and gazetteer checks; filtered substring rule; Plain tags unique in scope; V23s restated as "Catch is never a keyword or a name" | ⚑ Agreed (RS Q3, HL Q1, CA Q1); told plainly T1, T3, T5, T6, T7 | Client (approval of this document) | Now |
| NP-3 | RS 7.5; CH 4.14 AN4; SK 4.5.2; BR 4.13; CA 4.11 check | W-DREAM-1 (4.7) | ⚑ Agreed (RS Q3) | **Client, Q1** | Now |
| NP-4 | ABL 6.1, 6.2, 6.5, 6.6, 7.3, 10.3, 14.1 | Second Wind → Dig In; Surge → Onslaught (pool.surge id stays); Slot Recovery → Slot Renewal; reaction-policy labels; Ember Burst → Cinder Spray | ⚑ Settled (ABL 6) | **Client, Q5** (Surge, Slot Renewal); lead (rest) | Now |
| NP-5 | CL 6.4, 7.5, 8.2–8.6, 9.1, 10 (rename table), 12.1, 20.2 | Rest tools; Rekindle → Take Heart; Hold the Line → Bar the Way; Berserker → Ravager; Adept → Ascetic; Bastion → Rampart; complexity words; table rows for every class rename in 5.4 | ⚑ Settled (CL 10 freeze) | **Client, Q6** (Adept, Bastion); lead (rest) | Now |
| NP-6 | ENM 17.5 (l.3396); XI 5 catalogue (BR row); RS 7.4 RC5 | Restate Upended → Sprawled (CC-65) | — | Owners (already approved in BR) | Now |
| NP-7 | AR 12.3 | Recruit soldier → Levy soldier (CMP-27) | — | Naming pass (CMP-27 names it) | Now |
| NP-8 | CON 15.1, 18.3, 18.4, 18.9, 18.10, 18.11, 12.2, C7; STAT 8.1, 8.3, 8.4, 10 | Rooted → Tangled; Raging → Frenzied; Reckless → Heedless; Lame → Limping; Ill-Starred → Star-Crossed; Leaden → Plodding; Sight-blight → Sight-rot; Commanded words Come/Run/Crouch/Wait; Resisting prints "Resistant (type)"; "bloodied" → "hurt"; rider wording prints keywords | ⚑ Settled (CON 18 approved names) | **Client, Q5** | Now (Later names at their wave) |
| NP-9 | STAT 7.2, 12 | Second Nature → Old Hand; Tactician kept (W-22) | — | Lead | Now |
| NP-10 | ENM 5.3, 7.1, 7.3, 8.1, 12.1, 13.1, 17.1–17.5, 22, 23 | Roles Marksman, Hinderer, Swarmer (Minion stays the rank); tints wordless; Wary → Watchful; affixes (5.1, 5.4); profiles and templates internal; boss menu Attack and Direct; "Poor save: Will" | ⚑ Settled (ENM 17.1, A-3 approved) | **Client, Q5** | Now |
| NP-11 | ITEMS 4 decision 4, 5.2, 8.2, 10.3, 11, 12.10, I-9 | Trinkets row → Worn items; "Victory" authored case; Embercleaver example → Ember Longsword; item "dormant" → "untold"; Signature item shows "the boss's prize"; Keeper's Cart cite → Pedlar's Cart | ⚑ Agreed (ITEMS dormant term; I-9) | **Client, Q5** (dormant, Signature item); lead (rest) | Now |
| NP-12 | W 5.5, 5.6, 7.1, 7.4, 7.6, 8, 13, 15.3, 17.5, 17.8, 18.2–18.6, App A | Overwatch, Volley, Steady Aim, Shatter, Bloodletter, Set, Spare, Opener, Sure Grip, Thunderclap Greatclub, Hunter's Longbow, Skirmisher's Spear, Duellist's Rapier, Corridor Warden, Wardwood Staff; "sling artillery" loadout label → "sling marksman"; Totem focus kind → Tipstaff | ⚑ Settled (W approved names) | **Client, Q5** (Q2 for Tipstaff) | Now |
| NP-13 | AR 5.6, 5.8, 13.1, 13.5, 14.3, 14.4, 14.6, App A | Bull Rush → Bowl Over; Deep Stance → Low Stance; Unshaken → Unflinching; Weathered → Fleece-Lined; Resistant (element) → "<element>-proof"; "Hide armour" and "Plate armour" display forms; armour notables (5.8) | ⚑ Settled (AR approved names) | **Client, Q5** (incl. Bellwarden's Mail); other notables lead (illustrative) | Now |
| NP-14 | CA 0.2 Q1 record, CA-1 row, 2.3 F13, 4.2, 4.5, 4.7, 5.1, 5.4, 6, 9.3, 11, 14.1 Q1 and Q4 records, T5 | "Catch" ruled (no display word; ◈ spoken "stored moment"); lane wording "Charms hold moments"; pip states Ready / Used / Waiting / Stored / Idle; log verb "went off"; Lockjaw → Jawbone; Quietus Knot → Snuffed Wick; Relay Thread → Relay Ribbon; Stay Thread → Anchor Bead; Kinstone → Kin Pebble; Q1 and Q4 records gain rename notes (Ashen Knot → Ashen Bead; Underpillow → Bobbin); closes CA 11's naming row | ⚑ Agreed (CA Q1) | **Client, Q5** (lane wording); lead (rest) | Now (charm names at M3) |
| NP-15 | TK 5.5, 5.6, 13.1–13.9, 14.1, 14.2, 15, 19, App A M1 | Token dropped as a display word; Catcher → Courier; Clench → Iron Grip; Weathered → Fleece-Lined; Duskward → Gloamward; Torc of Seasons → Seasonal Torc; Sigil of Aid → Helping Sigil, epithet "of the Helper"; Antidote → Venomproof; Scrollwright's and Muse's Token → Fob; Kinstone → Kin Pebble; lane wording as NP-14 | ⚑ Agreed (TK Q1 lane cell) | **Client, Q5** (lane wording, Weathered); lead (rest) | Now |
| NP-16 | HZ 0.1, 3.4, 6.3, 6.3.4, 6.4, 6.9, 6.17, 6.18, 7.2, 7.4, 7.8, 7.10, A.5, 19.1 | Dreamfall → Freefall (`ZeroHpAfterDreamfall` id stays); Cord verbs Knock and Sustain; Stir parts internal (the Small Hour Strikes lessonTag reworded); The Lockdown Crank; King Tide; Last Show; service pull; The Mourning Ribbon; The Dreamer Turns Over → The Bed Turns Over; front matter "six names collide" corrected to eight; **request**: type the Treadwheel stove as fire, or display it as a Bladed or heavy hazard (RC9; fallback: the stove's Hot label is replaced by "Heavy") | ⚑ Settled (BR-30 tone flag) | **Client, Q1** (Bed); lead (rest); HZ owner (request) | Now / M2 |
| NP-17 | TL 0.1, 3.4, 6.2, 6.3, 6.4, 6.6, 6.7, 6.14, 6.23, 7.2–7.11, A.5 | Trap grades Found/Hunch/Unfound; check grades Clean/Jam/Botch; DC tiers Low/Fair/Hard; family Clasp; Hunch badge; Lip Clamp; pressure slab; Kennel Snare → Wire Loop; Blade Wheel → Swing Blade; Frost-seized padlock; Wager rooms (5.5); Rood Screen door; Scent Bladder → Scent Sack; Opener rule → First-spring rule; "mimic" never on screen | — | Lead | Now |
| NP-18 | FL 3.4, 6.11, 7.2, 7.3, 7.5, 7.7, 7.8, 8.1, 9.1, 9.2, avoided table | Stairhead → the Downstair (minimap glyph label); Blueprint renames; The Sentence Corridor; Slam doors; Return Latch; Tap the wall; skin words (sluice doors, drawn hanging, canvas wall, great-hall doors, Fairground Turnstile) | — | Lead | Now |
| NP-19 | RT 6.2.3, 6.8, deferred list | Conduct → Charged Water; "lane" lower case; Mist → Fog if revived | — | Lead | Now |
| NP-20 | RS 3.4, 4, 6.2, 6.3, 6.5, 6.8, 7.4, 8.0–8.12, 9.1, 9.3, 9.4, 12, 13 | The Silk Archive → The Cobweb Archive; Middle Dream → Midwater; Nightmare tiers → Peril tiers (Peril Rendition); Lucid Key → Clear Key; Keepsakes → Souvenirs; Other Dreamers → Strangers; Night Market → Lantern Market; Antechamber shown as Forecourt; Forecourt Top-up; roster and kit renames (5.5); bell tripwire → keyring tripwire; Faithless knight; "keeper" vendor prose → Pedlar | ⚑ Settled (RS frozen names); ⚑ Decided (MAIN 4.7 cites the tiers) | **Client, Q3** (Archive), **Q4** (Peril); lead (rest) | Now (M3 content at M3) |
| NP-21 | BR 4.2.1, 4.3.1, 4.5.2, 4.7.1, 4.7.4, 4.13, 5.3–5.6, 6, 7, 13 BR-16, 14.2, A.5 | Counterpiece (payoff kinds internal); Boss-action warnings; Resolve Spent; Joins at 33%; grab square; Returns Stamp; Mourning Blinds; the pen board; the Lectern Hall; the Dais; the Lifting Stage; Flood Stage; Footnote; Read the Testament; Frost Tone; Long Note; Running Thrust; Black-Crepe Embrace; Final Respects; Sour Spit; Heavy Blanket fallback; The Bed Turns Over; Probate Room, Clapper Swing, Inkblot, Flex and Stamp, Mouthful, Cell Zero Slam added to A.5 as checked | ⚑ Settled (BR-30 flag) | **Client, Q1** (Bed); lead (rest) | Now |
| NP-22 | LE 0.2 Q1 record, 3.5, 6.7, 6.11, 6.15, A.5, LK15; LP 0.1, 4.7.4, 4.7.11, 4.10, 4.12, 5.3, LP-14, LP-18, LP-28, A.5 | Viaticum → Parting Fare (`viaticumFallback`, `BedsideStandOnly` ids stay); Bedside Stand → the Washstand; "the Hollow ahead" → "the next camp"; Treasure strip retired; Pew Crab; Offertory Dish; Fairground Balloon; Burial casket; Gut-Lodged bauble; "Held" badge → "paused"; the Wish → The Long Shot | ⚑ Agreed (LE Q1 text; LP Q5 exception) | **Client, Q2**; lead (rest) | Now |
| NP-23 | IR 0.1, 4.8, 4.13, 4.14, 5.1, 5.3, 6.1, 9.2, IR-13, A.5 | Wayward → Windblown; "Answers here" → "Shines here"; Oxblood → Damask Brigandine; plate text "Guards: Hound-Collar Mail"; Reverie label internal; Patina list drops "vellum" | — | Lead | Now |
| NP-24 | SV 4.7, 4.9, 4.10.1, 5.5, 14.2 | Run code family: "Run code" label; launcher and hub row "Play a run code"; mark "run code"; "Copy run code (plays differently for others)"; entry line (whole-string key); "older build"; notices say "this run code"; "This run code can't be played on this build."; unravel card as two keys ("The dream unravels." / "Your profile is unchanged.") | ⚑ Agreed (SV Q2 quotes "the dream unravels"; kept) | Lead (within Q1 A) | Now |
| NP-25 | XI 4.1.5, 4.4.5, 4.7, 4.9.9, 4.10 | "To the Downstair"; spent field → cleared field; Trace on sight; Treasure strip retired where cited | — | Lead | Now |
| NP-26 | CH 0, 0.1, 0.3, 2.3 F5, 4.1, 4.3, 4.7, 4.8, 4.10, 4.12, 4.13, 4.15.5, 5.1, 5.2, 5.3, 11, 14.1, 14.3, A.2 | Hollowfolk → the Fireside (the V20 validator id stays `Hollowfolk`); Log-Carrier, Slate-Sketcher, Straggler, Shelver, Showcaller, Roll-Reader, Gilder; the Pedlar's Cart; the Banister; the Newel; ribbon post; No idle chatter; Legible captions; "Play a run code"; "Same as last run"; Library page "Residents of the Fireside" | ⚑ Agreed (CH Q1 hub concept; client-confirmed placeholders) | **Client, Q3**; lead (rest; told plainly T10) | Now |
| NP-27 | SK 0.1, 2.3 F8, 4.5.1, 4.5.2, 4.6, 5.1, 5.2, 5.4, 11, 14.2, A.2, SK-9, SK-13 | SK-13 closed (Starting gear final); given names (5.8); Gear strip; Lamp-trimmer's armband; "a carter's thread"; kit picks Twin Blades, Bow and Blade, Shield hand, Focus hand | ⚑ Agreed (SK Q2, Q3 rules applied, not changed) | Lead | Now |
| NP-28 | HL 4.1.2, 4.1.5, 4.3.3, 5.1, 5.2, 5.3, 5.4, 14.3 T12, A.2 | Rejoinder; Quick Blood; Planted; Shelve it?; Not this time; Carried to the end; Lower your head / Walk on; Begrudged; Heavy-Eyed; Stumbles Early; Oath of the Cold Iron; the Frets retired; Road-Scuffed Mail; Thumb-Worn Tipstaff; the Ironbiter sample | ⚑ Agreed (HL Q1 registers applied, not changed) | **Client, Q2** (Tipstaff); lead (rest) | Now |
| NP-29 | MAIN 4.5, 4.7, 6.6, 10 | "boons" in the level-up pool → "offers"; "background perk" → "Calling perk"; "Nightmare" tier wording → Peril; "a simple camp screen" → "hub screen"; Easier lamp; Share play data | ⚑ Decided (MAIN client-approved) | **Client, Q4** | Now |
| NP-30 | CMP front matter, 3.5, 7.5, 8.3, 12 | Wayfarer's Pack → Wayfarer's Belt; role tags Buffer and Defender; Carried hire → Held-over hire; shared-name list Hold the Line → Bar the Way | — | Lead | Now |
| NP-31 | CN 12, 14, 16.1–16.5, 16.7, 17, CO-9 | SRD and tone renames (5.1, 5.4, 5.8); Holding merged into Arrest; "cords" in binding prose → "bindings"; CO-9 records the prior names; 17's "names follow the ability" yields to N13 | ⚑ Settled (CN 17); told plainly T15 | Lead | Now |
| NP-32 | ARC 3.3 F1, 4, 7.3, 7.5, 8.1, 8.3–8.5, 8.8, 9.1, 9.3, 18, ARC-1 | Fireball and Scorching Rays overrides dropped; Whittle (modes Selective, Widen, Load); Weatherwork; Highfall; Deluge; Lashed Pair; Lay Bare; Holding merged | — | Lead | Now |
| NP-33 | VG header, 3.4, 4.1, 7.3, 7.4, 7.6, 8.1, 8.2, 18, App A C-10 | Bar the Way; Onslaught and Onslaught Art; Fresh Reserves; Rampart (Q6); Warden's Reach never displayed | ⚑ Settled (VG names frozen) | **Client, Q6** (Rampart); lead (rest) | Now |
| NP-34 | SH preamble, 6.5, 7.3, 7.4, 7.10, 8.2, 8.3 | Respite; Trapped; Unbraced; Bank Shot; Closed Guard; Steel Waltz; Hemp Lacing cite; kit picks | — | Lead | Now |
| NP-35 | EP 0.1, 3.4, 6.6.6, 6.8.1, 6.10, 6.20, 6.21, 7.7, 7.10, 9.1, App C | Baited Room; the Heap; Alarm Order; "Facing the door"; Pale Mare and Weightlifter cites; the "Distracted −2" noticing modifier shows "Busy −2" | — | Lead | Now |
| NP-36 | EV 4.4.3, 5.3 (G5, G6, Gu1, H4, I-2, D5, D9, K3, Ca5), A.5, EV-31 | The Petitions Box; The Borrowed Pardon; Rent in Arrears; The Coat Hooks; The Late Portrait; The Usher's Lamp; Strand "Unclaimed"; "READ THE END?"; "Wave to the crowd"; The Odd Glove; Peril Provisos; Grace chip "Grace: <condition>" | — | Lead | Now |
| NP-37 | DH 4 decision 19, 7.1, 12.1, 15.2, 17.8, 24, 27 | Dig In; HP tier "sound"; plate mark "hurt"; the Death-Ward-shaped spell named "Stay the Hand" when written | ⚑ Agreed (DH decision 5 wording "HP tier") | **Client, Q5** | Now |
| NP-38 | HR 3.4, 4.1, 7.2, 7.3, 7.7, 7.11, 7.13, 8.3, 8.5, 8.7, 9.2, 9.3, 9.4, 10.3, 15, 19, HR-30 | Urge, Earned Urge; Speak Over; Ache; Show the Way; Ease the Pain; Clarion; Gather In; Pronouncement; Hoist the Pennant; Yield Arms; Quiet Minds; Take Heart; previews print "Demoralised"; examples Brask and Turnkey/Inmates; Tipstaff (Q2); **request**: define "Bring Them Home" (15) or delete it (fallback: deleted) | ⚑ Settled (HR-30 freeze) | **Client, Q2** (Tipstaff); lead (rest) | Now |

---

## 14. Questions for the client

Only renames of client-agreed or decided names and weighty taste calls are asked. Everything else in 5 is mechanical and is approved with this document as a whole.

### 14.1 The six questions

**Q1. Dream, sleep and wake words on screen (W-DREAM-1; NP-3).** The premise is deliberately uncommitted (RS 7.5). Today more than twenty names carry dream words, several mechanical (Dreamfall, Nightmare tiers, Wake Order).
- **A (recommended).** Reserve the words (4.7). Allow them only on (a) the run's UI frame (Dream card, track, step, Dream's Due, "the dream unravels", the Unwoken Roll) and (b) named F5 Heart and Wake-theme content listed one by one, including "The Bed Turns Over" for the Night Governess hazard. Rename the rest (Freefall, Peril tiers, Midwater, Alarm Order, Strangers, Clear Key). Keep The Waking City gated until M4.
- **B.** As A, but no Heart bed names: "The Bed Turns Over" becomes "The Mattress Heaves", and Foot of the Bed, the Turned-Down Bed and the two "Under the Bed" names are renamed too.
- **C.** No reservation: keep every current dream name.

**Q2. Three sensitivity renames inside agreed or approved text (NP-12, NP-22, NP-28, NP-38).**
- *Viaticum* is the Catholic sacrament given to the dying (in agreed LE Q1 text). RS 7.6 allows invented liturgies only; HL rejected "Lent" on the same ground. → **Parting Fare**.
- *Bedside Stand* (the Parting Fare host; LP Q5 silhouette exception) puts a bed beside the keepers on F5 (AN4; why Underpillow was retired). → **the Washstand**; the silhouette exception is unchanged.
- *Totem* (the Herald off-hand focus kind, approved in W, CL, HR, and the heirloom Thumb-Worn Totem) is a living people's sacred word (Ojibwe *doodem*) and a PHB subclass. → **Tipstaff** (a court officer's staff; "Thumb-Worn Tipstaff"), pending the sensitivity read.
- **A (recommended).** All three as above.
- **B.** Viaticum and Totem only; keep Bedside Stand.
- **C.** Keep all three and send them to the sensitivity read at M4.

**Q3. The Hollow and Silk cluster (NP-20, NP-26).** Hollow (camp phase, Last Hollow, Hollowfolk), the Silk Archive, charms and a dream frame together read as Hollow Knight and Silksong (Team Cherry). No one name infringes; together they read as derivative (ECHO-3). Hollowfolk is a placeholder the client confirmed (CH).
- **A (recommended).** The Silk Archive → **The Cobweb Archive** (short form "the Archive"; truthful to its Law of webs and silence); Hollowfolk → **the Fireside**; "the Hollow" and Last Hollow stay, in the camp scope only; Wayward → Windblown (mechanical).
- **B.** The Silk Archive only; Hollowfolk stays.
- **C.** Keep all; record an echo waiver.

**Q4. The difficulty ladder and MAIN's wording (NP-29, NP-20).** "Nightmare tiers" carries the SRD monster Nightmare, the Night overload and a premise word.
- **A (recommended).** **Peril tiers** ("Peril 1, Peril 2 …"; Peril Rendition; Peril Provisos), paired with the Perilous Door; MAIN 4.5's "feats, boons and upgrades" reads "offers" so "boon" means only the starting boon; "background perk" → "Calling perk".
- **B.** **Depth tiers** instead of Peril (shares a word with the Depth gauge and floor depth); the rest as A.
- **C.** Keep "Nightmare tiers" and "boons" as written.

**Q5. Approved rules names renamed for SRD, outside IP or on-screen collisions (NP-4, NP-8, NP-10, NP-11, NP-12, NP-13, NP-14, NP-15, NP-37).** Three bundles:
- *(a) SRD and outside IP:* Surge → Onslaught; Raging → Frenzied; Reckless → Heedless; Overwatch → Ready Shot; Volley → Arrowfall; Steady Aim → Settled Aim; Shatter → Fracturing; Bull Rush → Bowl Over; Bloodletter → Gashing; Thunderclap Greatclub → Thudding Greatclub; the enemy roles Artillery → Marksman, Controller → Hinderer, Minion role → Swarmer; Commanded words Come / Run / Crouch / Wait; "bloodied" → "hurt".
- *(b) Collisions a player meets on one screen:* Wary → Watchful (beside Weary); Rooted → Tangled (beside Routed); Resisting prints "Resistant (type)"; HP tier "standard" → "sound"; Ill-Starred → Star-Crossed; Leaden → Plodding; Set → Receive; Spare → Withhold; Opener → Forestroke; Sure Grip → Fast Grip; Deep Stance → Low Stance; Unshaken → Unflinching; Weathered → Fleece-Lined; Resistant (element) → "<element>-proof"; boss menu Strike → Attack and Command → Direct; item "dormant" → "untold"; "Signature item" shows as "the boss's prize"; Slot Recovery → Slot Renewal; CA lane wording "Charms catch moments" → "Charms hold moments".
- *(c) Notables named with roles or affixes:* Hunter's Longbow → Fowler's; Skirmisher's Spear → Forager's; Duellist's Rapier → Courtier's; Corridor Warden → Corridor Crossbow; Wardwood Staff → Rowan Staff; the F1 boss signature Bellwarden's Mail → Hound-Collar Mail (Bell* and "-warden").
- **A (recommended).** All three bundles.
- **B.** (a) and the two homophone pairs (Wary, Rooted) only; keep the rest of (b) and (c).
- **C.** None; record waivers.

**Q6. Class names, the hub and the title (NP-5, NP-33).**
- **A (recommended).** Keep **Vanguard, Shade, Arcanist, Herald** and the **Stairtop** (Stairhead moves instead). Rename the unlock class Adept → **Ascetic** (Mass Effect cluster with Vanguard, Soldier, Sentinel) and the Vanguard subclass Bastion → **Rampart** (a Supergiant title and the 2024 DMG system). No game title is ruled here; any title and marketed name gets the trademark and store search (SEARCH-1) before the store page.
- **B.** As A, and also rename Shade → **Footpad** (removes the last Team Cherry echo; Shade is a Hollow Knight term).
- **C.** Keep every class and subclass name as written.

### 14.2 Told plainly, and new lead's calls

- **T1.** The literal reading of RS 7.6 and HL-30 NR2 ("no SRD names") would fail the corpus's own conditions, rarities, masteries and base families. 4.5 replaces it with a function test and an allowlist (NP-2).
- **T2.** The real-world gazetteer check (N5f) is new; RS 7.6 does not ban real places as written (the brief overstated it). It is a new lead's call.
- **T3.** The sensitivity blocklist outranks HL NR1: "obsolete for a hundred years" never clears a word that is now a slur.
- **T4.** Premise words are restricted in names only; flavour lines keep NR8 (the dream is the subject of Malady flavour) and AN4.
- **T5.** Unfiltered substring checks (V14 (f), NR6, V20s as written) are unworkable noise; 4.10 filters them.
- **T6.** CA V23s's "never print catch on any surface" is restated as "never a keyword or a name"; barks may say "catch". Approved text that prints it is renamed.
- **T7.** HL-27's Plain names were unique only by prefix; the tag must be unique.
- **T8.** The pass's own group drafts contradicted each other on about 40 names and several replacements broke the same rules; the critics' check fixed them (A.1).
- **T9.** 87% of harvested names stay as written; the pass is narrower than "rename every placeholder" suggests.
- **T10.** CH kept placeholders "the client confirmed". This pass renames seven Fireside figures (rule breaks: heal words, keyword collisions) and the Keeper's Cart (RC9 against "keepers of the sleep") as mechanical rows; only the umbrella Hollowfolk goes to the client (Q3).
- **T11.** Two places where the rule, not the name, should change are requests: the Treadwheel stove (HZ) and "Bring Them Home" (HR).
- **T12.** Peril tiers depend on the Perilous Door keeping its name; rename them together or not at all.
- **T13.** No trademark search was done; that is legal's step before a store page.
- **T14.** Weary keeps its word over Wary because the party condition is printed more often; the enemy state moves. Routed keeps its word over Rooted because it is agreed (C-3) and Rooted is Later.
- **T15.** CN 17's "scroll names follow the ability" yields to N13 when the ability name is an SRD spell.
- **New lead's calls (beyond the group drafts):** the W-DREAM-1 list structure (a)/(b)/(c); hanging words in the sensitivity list; the filtered substring rule; Tipstaff, Pale Mare, Tangled, Unbending, Deluge, Banneret, Gall Slick, Sour Spit, Respite, Take Heart, Bar the Way, Weatherwork, Arrowfall, Fresh Reserves, Load, Cast Out, Wand of Fetters, The Airing Cupboard, the Ironbiter, Lamp Water (each chosen after a critic rejected the drafts' candidates; A.1); V24 and `NamingDrift`.

### 14.3 Future items
The naming pass itself closes with this document. Items that come back only if triggered: the M4 sensitivity read (Malady fallbacks, Tipstaff if Q2 A); locale passes; The Waking City before M4 (Q1); the damage-type theming (STAT 8.1, agreed open) and with it the "<element>-proof" property names; names for content not yet written (Later classes, the Death-Ward-shaped spell, LE's Blood Price and the CH hooks); trademark clearance for marketed names.

---

## 15. Risks

| # | Risk | Signal | Response |
|---|---|---|---|
| R1 | Restatements leave old names behind (the Upended pattern) | `NamingDrift` > 0; post-apply grep hits outside history | V24 r2 fails retired names at build; NP rows each carry the grep check |
| R2 | Mechanical renames touch about 90 sections in 34 documents and slow other work | Owners queue | Rows are display strings only; apply per wave (10); ids stay |
| R3 | A new final collides with a later document | V24 r1–r3 | The registry runs on every new segment (SV-20) |
| R4 | The allowlist becomes a loophole | SRD hits pass as "adopted" | Each allowlist entry names its attribution and source section; additions are lead's calls |
| R5 | Plain names and keyword discipline make the game read dry | Playtest | Tone lives in voice (barks, Whence, Reveries); the whimsy budget keeps meta names free |
| R6 | Downstair still reads as the hub ("Stair" stem) | Playtest the "To the Downstair" action | Fallback: Exit Stair (0 hits at check; A.1) |
| R7 | Peril tied to the Perilous Door | A later rename of the Door | Rename both together (T12) |
| R8 | Tipstaff fails the sensitivity read or reads as legal jargon | Sensitivity read | Fallback candidates Sceptre (0 hits) or Bough (0); not Rattle or Effigy |
| R9 | W-DREAM-1 creeps (new Heart names assume the waiver) | New (b) entries | (b) is a list, not a scope: each new entry is a lead's call recorded in 9.1 |
| R10 | Locale traps beyond those known (7.3) | Native read | PROC-2 per locale before M4 lock |
| R11 | The title or a marketed name is not clearable | Legal search | SEARCH-1 before any store page; this pass picks no title |
| R12 | Echo clusters re-form through new content | V24 r5 cluster test | The echo list is data and grows |
| R13 | Prose hits on common finals (Found, Low, Clean, Fair, Attack, Run) hide future collisions | Grep noise | These are keywords in the closed glossary; V24 checks them by meaning (keyword table), not by prose grep |
| R14 | Darkest Dungeon (Heirloom + Deeds) and Hades (boon) echoes kept | Player or press comparison | Recorded waiver W-11; revisit if a second echo of either studio appears (cluster test) |

---

## Appendix A: critique resolutions and names

### A.1 Critique resolutions
Three critics read the style-guide draft and the four group proposals (combat, items, world, interface). Every finding and its resolution:

| Critic and finding | Resolution |
|---|---|
| **Clarity / completeness (must-fix): the drafts gave different finals for the same name** (Stairhead ×3, hub Stair ×2, Dreamfall ×4, Nightmare tiers ×2, Last Hollow ×3, Hollowfolk ×2, Silk ×2, Catch family ×2, arena Device ×4, item dormant ×2, seeded ×2, Night-Mare ×2, Lullaby ability ×2, Crescendo/Clangour, Cold Ring, Hold the Note, Spear Lunge, Hold the Line, roles, affixes, role tags, Cord verbs, Answer, First Cut, Answers here, Set aside, Carried to…, Second Wind, Catch Breath, Interject, Sculpt, Gout, Skyfall, Restricted Section, token, Brennan, Wail/Swallow, Seams, Sound the wall, boss Strike, Prompts, tints, Deeds) | One final per row in 5 (the downstair, the Banister, Freefall, Peril tiers, Last Hollow kept, the Fireside (Q3), Cobweb Archive (Q3), Clasp, Counterpiece, untold, run code, Pale Mare, Soft Song, Swell/Din, Frost Tone, Long Note, Running Thrust, Bar the Way, Marksman/Hinderer/Swarmer, Fixated/Retaliating/Banneret/Guardian, Buffer/Defender, Knock/Sustain, Rejoinder, Quick Blood, Shines here, Not this time, Carried to the end, Dig In, Respite, Speak Over, Whittle, Deluge, Highfall, The Chained Shelves, token dropped, Brask, Wail/Swallow kept (W-6), Seams kept, Tap the wall, Attack, Boss-action warnings, wordless tints, Deeds kept (W-11)). Losing candidates go to `retired_names.json` |
| **Completeness (must-fix): 'bile' on display strings** (Bile Slick, Bile Spit) | Gall Slick, Sour Spit; "bile" in `sensitivity.json` with the Atrabile waiver |
| **Completeness (must-fix): replacements that broke the corpus's own rules** (Thunderhead, Linked Shackle, Captain, Shelter, Regulars, Iron-Willed, Dawnwater, Snared, Tracking/Marker, the Hand cluster) | Weatherwork, Lashed Pair, Banneret, Buffer, the Fireside, Unbending, Lamp Water, Tangled, Fixated, "of the Helper" |
| **Completeness (must-fix): flags with no resolution** (the Wish; Scent/Spore Bladder; Nursery door; mimic; The Lost Glove; Take a bow ×3; Omen Retelling; BR names outside A.5; HZ prose; Calling perk grep) | The Long Shot; Scent Sack; Nursery door kept (the Wager room moved); "mimic" off screen (V24 r7); The Odd Glove; Ca5 "Wave to the crowd"; Omen Retelling W-15; BR names checked (A.2); HZ prose reworded (NP-16); Calling 4 prose hits, 0 as a perk |
| **Completeness (must-fix): site lists wrong or incomplete** (Dreamfall, Viaticum, Nightmare tiers, Bile Ooze, Wayfarer's Pack, Lingerer) | Sites generated by script for every row (5, sixth column) |
| **Clarity (must-fix): Snap collides with Snap Casting** | Clasp |
| **Clarity (must-fix): N-K failures among replacements** (Shelter, Covering, Shielding, Hidden Reserves, Tracking, Steal a Breath) | Buffer, Guardian, Fresh Reserves, Fixated, Respite |
| **Clarity (must-fix): new names colliding with each other** (Unclaimed ×2, First Step / Bad First Step, hurt / Ease the Hurt, Store / Stored / Prop Store, Slate Draught / Slate-Drawer, Firewatcher / Watchfire, Jar ×2, Strongman's Stage) | Windblown for Wayward and Unclaimed for the Strand; the Banister and Stumbles Early; Ease the Pain; Load and The Prop Room; Slate-Sketcher; Log-Carrier; Knock and Blowback Jar; the Lifting Stage |
| **Clarity (should-fix): renames re-opening clusters** (Second Self, shield notch, Spent Wick, Stay Pin, Kin Button, Ironbark Vest, Oxhide Scabbard, Copper Collar, Psalter Crab, Recollect) | Reflection, "shield mark", Snuffed Wick, Anchor Bead, Kin Pebble, Ironwood Vest, Old Scabbard, Copper Gorget, Pew Crab, Take Heart |
| **Clarity (should-fix): a Footing cluster and Breath stem** | Unbraced, Planted, Respite |
| **Clarity (should-fix): names that read as something else** (Wand of Lashing, Overcast, Live Water, Stuck, Unrested) | Wand of Fetters, Begrudged, Charged Water, Tangled, Heavy-Eyed |
| **Clarity (should-fix): "ahead of the dream", "Back to the Exit Stair"** | Forecourt Top-up; "To the Downstair" |
| **Clarity (should-fix): two nouns for the seeded flow** | "Run code" on every surface |
| **Clarity (should-fix): heirloom and trinket divergences; "Locked" twice; "Dug-in Heels"** | Rejoinder, Quick Blood, Iron Grip, Fast Grip, Closed Guard; Dug-in Heels dropped |
| **Clarity (should-fix): phrase names that cannot inflect or are opaque** (Not Past Me, Benison, Spieler, Sending Off) | Bar the Way, Warm Word, Showcaller, Cast Out |
| **Clarity (acceptable): Clean disputed; Easy vs Easier** | Clean kept (prose hits only); DC tier Low |
| **Tone (high): three dream-word policies** | One list, W-DREAM-1 (4.7, Q1) |
| **Tone (high): Running Noose and Picture-Hanger bring back hanging** | Wire Loop, Gilder; hanging words in `sensitivity.json` |
| **Tone (high): comic names on harm moments** (Treacle, Gush/Spout, Thump, Croon, Couch, Stuck) | Dragfoot, Deluge, Groundswell, Soft Song, Receive, Tangled |
| **Tone (high): the Dreamer's age returns** (Hobby-Horse, Rocking Horse, Toy Cupboard, Rattle) | Pale Mare, The Airing Cupboard, Tipstaff |
| **Tone (high): Totem replacements fail** (Rattle, Effigy) | Tipstaff (Q2; sensitivity read; fallbacks in R8) |
| **Tone (high): church words return** (Psalter Crab, Benison, Lamp-Blessed Water) | Pew Crab, Warm Word, Lamp Water |
| **Tone (medium): collisions among new names** (Regulars, Shelter, Shielding, Signal Sigil, Hinge-Taker, Rival Delvers, Unclaimed ×2) | the Fireside, Buffer, Guardian, Helping Sigil, the Ironbiter, Strangers, Windblown |
| **Tone (medium): First Step vs Bad First Step** | the Banister; Stumbles Early |
| **Tone (medium): Depth tiers lies (floor depth)** | Peril tiers recommended (Q4 A); Depth is option B |
| **Tone (medium): Book of Runs pun** | Book of Nights kept |
| **Tone (medium): Shooter** | Marksman |
| **Tone (medium): head-word cap broken by renames** (Iron ×5, Long ×4, Hand ×6, Final ×3) | Unbending, Banded Face, Oath of the Cold Iron; Running Thrust, Long Fall dropped; "of the Helper"; Pronouncement (Final Respects kept, cap 2: Final Bow, Final Respects; "the Final Act" is a title epithet) |
| **Tone (medium): Coffle replacements** | Lashed Pair |
| **Tone (medium): Untold for charm pips** | Idle for pips; untold for items only |
| **Tone (medium): Spieler, Slate-Drawer** | Showcaller, Slate-Sketcher |
| **Tone (medium): Frost Peal, Spent Wick** | Frost Tone, Snuffed Wick |
| **Tone (medium): Houndstooth Mail, Hardwearing, Weightlifter, Prop Store** | Hound-Collar Mail, Fleece-Lined, Weightlifter kept (period gymnasium word; Barbell-Swinger read as comic), The Prop Room |
| **Tone (low): Grit Teeth, Not Past Me, Captaining, Sending Off** | Dig In, Bar the Way, Banneret, Cast Out |
| **Tone (low): unravel card mixes voices** | Two keys (NP-24) |
| **Tone (low): Overcast, Recollect** | Begrudged, Take Heart |
| **Completeness (should-fix): C/M tags inconsistent** | Rule: any name in a client-approved or agreed text is C or named in Q text; Wary, Nightmare tiers, Surge are C (Q5, Q4); Bellwarden's Mail (approved, AR 14.6) becomes C in Q5 (c); Lingerer stays M (a CH placeholder that MAIN only cites); Last Hollow is kept |
| **Completeness (should-fix): style guide's own contradictions** (bed reserved but "The Bed Turns Over" adopted; the nurse line; Psalter; Bone cap) | W-DREAM-1 (b) lists the Heart bed names (Q1); nurse line deleted; Pew Crab; W-16 Bone across waves |
| **Completeness (should-fix): grep evidence unreliable** | One scripted pass; every final's count is in 5's last column; no row is "verify" |
| **Completeness (should-fix): Totem sites incomplete** | NP-12, NP-5, NP-38, NP-27, NP-28 list W, CL, HR, SK and HL |
| **Completeness (nit): small residual collisions** (Found vs "found 1 of", Ringmaster's Key, Low Stance vs Low Cut, Hail of Shafts, Cinder ×3, Unrested, Still ×2) | Found kept (same sense); W-14; Low Stance kept (different classes of thing); Arrowfall; Cinder Spray kept (illustrative); Heavy-Eyed; Oath of the Cold Iron |

### A.2 Names checked (case-insensitive, whole word, `docs/superpowers` specs and review; re-run while writing, 2026-11-24)

- **Every final in 5** is checked; the count is in 5's last column. Finals with 0 hits are free. Finals with hits were read: keywords and keyword printing rows (Bleeding, Corroded, Chilled, Demoralised, Found, Unfound, Clean, Low, Fair, Attack, Run, Come, Wait, Stored, Used, Treasure field, Worn items, Sprawled, Levy soldier) are the same meaning by design; prose-only hits (Knock 34, Sustain 26, Load 127, Idle 76, Receive 9, Trapped 29, Planted 9, Defender 12, Guardian 7, Direct 8, Swell 5 (Organ Swells, same theme), Din 2, Clasp 4 (a cut trinket and prose), Retaliating 4 (the condition it mirrors), Reflection 2, Long Note 1 (the CH line it echoes), the Dais 1, the Newel 1 (CH's own candidate), Jawbone 1 (CA's object column), Watchful 3 (cut item names), "the next camp" 19 (same sense), "belt trigger" 2 (the Phial, same sense), Detonation and Rays (the templates themselves), Brask 6 and Turnkey/Inmates (registered roster names)).
- **Candidates rejected because of hits found in this check:** Mired (an existing condition), Unbowed (an existing armour property), Outpour (an RT rule), Dismissal (the CMP companion action), Emblem ("Fire Emblem" in prose and leader-emblem charms), Regulars (XI density label Regular), Shelter (AR property), Snap (Snap Casting), Green Room (BR reserve socket), Stairwell (CH prose), Rattle (25 hits), Linen Press (CH nursery prop).
- **Sites of every current name** (5, sixth column) are whole-word counts per document by script; a current name with 0 sites is one the drafts introduced and this pass rejected (Running Noose).
- **Names the source documents had not checked, now checked:** SH Old Wound, Bad Footing, Blind Side, Trigger Shot (pass); HR Opening Cry (renamed), Waver, Pick the Question, Strike or Shove (pass), Rush the Colours (pass), Bring Them Home (request, NP-38); BR Probate Room, Clapper Swing, Inkblot, Flex and Stamp, Mouthful, Cell Zero Slam (pass), See Also (prose hits in FL and EP; kept as a Weaver move; the boss's Cross-Reference became Footnote).
- **Prefix and validator ids:** `\bNP-[0-9]+` 0 hits; `\bV24\b` 0 hits.

---

