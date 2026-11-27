# M1-08 Stats and Contests Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Units carry the stat block SM 9 stores (level, six abilities, main ability, trained save, trained skills), every M1-08 derived number is a fold query over it, and every contest (attack, save, skill against a unit or a difficulty, initiative) is one d20 roll drawn on the saved `combat` stream.

**Architecture:**
- **Stat block:** `Unit` gains one field, `UnitStats Stats`. The core enums `Ability`, `Save` and `Skill` are fixed lists (SM 9: "fixed lists in core code"). Heroes and monsters use the same shape (SM 5.5). M1-19 fills it from class records and M1-22 from monster records; until then, code and fixtures fill it.
- **Derived numbers:** the M1-07 registry (`DerivedNumbers.Core`) gains the M1-08 rows, built in `src/Core/Stats/StatNumbers.cs`. Each base reads the stat block and other numbers through `NumberBaseArgs.Number`, so effects on an ability flow into every number that reads it.
- **Contests:** pure preparation (bonus, target and folded roll mode; no draws), a dice helper that draws on `combat`, and pure resolution from the drawn die. A preview (M1-11, M1-23, M1-25) is preparation plus resolution, with no draw.
- **Initiative:** a pure roll over a list of units that returns the order and the advanced state. M1-10 stores the order when the fight state exists.

**Tech Stack:** C# 12 / .NET 8; the M1-07 effects registries and folds; the M1-05 stream handles (`combat` is already registered, saved and digested); the M1-06 save and digest patterns; xUnit.

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-08](../../roadmap/m1-vertical-slice.md#m1-08-stats-and-contests), implementing:
- **SM 5.1, 5.5, 6, 7.1–7.2, 9, 11** (`2026-11-16-character-stat-model-design.md`, lines 138–157, 214–226, 238–308, 309–350, 522–554, 603–613).
- **DH 9** (`2026-11-17-death-healing-design.md`, lines 646–660): natural 20 and 1.
- **ENM 5.1–5.2** (`2026-11-16-enemies-design.md`, lines 212–262): what a monster declares and how it derives.
- **SV 4.3** (`2026-11-23-seeds-validation-design.md`, line 535): the `combat` stream's uses.
- **D-15** (`docs/roadmap/README.md`, line 169) and `docs/effects-vocabulary.md`, sections 5 and 9.

## Owner rulings for this step (2026-11-27)

1. **Scope.** HP and Recovery stay with M1-09, carrying capacity with M1-15, and the full AC query with M1-17. M1-08 builds AC as the unarmoured row only, 10 + Dexterity (SM 6, "unarmoured row a (lead's call)"), so attacks have a target. M1-17 replaces the base and keeps the id. Task 4 amends the roadmap's M1-08 Delivers line in this PR (README rule 5).
2. **Guile** moves to M1-22, which owns monster roles. The same roadmap amendment records it.
3. **Leadership:** M1-08 adds it as a skill (enum value, number, roll target) and nothing more. Whether Rally rolls Leadership or needs training in it is recorded as **D-16, blocking M1-14**.

## Global Constraints

- SM 5.1: "An ability is a single modifier. There are no scores." "The hard cap is +5 after all bonuses." SM 5.5: "The +5 cap binds every unit, bosses included. Monsters may go below −1." ENM 5.1: monster abilities "Range −3 to +5".
- SM 9: "A unit stores its six base abilities, level, class and trained skills. Everything in section 6 is a query that folds in modifiers from active effects." SM 6: "Nothing derived is stored."
- SM 6: "Exactly one d20 is rolled, against a fixed number. There are no opposed rolls." Attack: "attack bonus against armour class". Save: "the save against the attacker's ability difficulty". Skill against a unit: "10 + the target's matching save, including its save training … Hiding and noticing meet passive perception or passive stealth."
- SM 6 / DH 9: "Natural 20 and natural 1 apply to attack rolls only: a 20 always hits and is a critical hit, a 1 always misses. Saves and skill checks have no automatic results."
- SM 6: initiative is "d20 + Intelligence, rolled once per fight"; "Initiative ties break by higher Intelligence, then by a fixed unit order held in the state" (the `Units` id order, GameState.cs line 27).
- SV 4.3: `combat` serves "in-combat attacks, saves, contests, initiative". Every new draw goes through it (README definition of done).
- SM 3: integer arithmetic only, rounding down.
- The determinism analyzer stays green: a `[TotalComparer]` on every sort (pattern: `TriggerDispatch.FiringOrder`), no floating point, and `DrawStream` only through `Next()`, never constructed.
- Out of scope:
  - **M1-09:** max HP, Recovery, crit damage (D-02).
  - **M1-10:** the fight state and storing the initiative order; shared minion slots (ENM 12.4).
  - **M1-12:** `autoFailSave` and saves qualified by tag.
  - **M1-14:** the contest actions and commands (shove, hide, Rally) and trained-only refusals.
  - **M1-15:** carrying capacity and Strength read without conditions.
  - **M1-16:** weapon training and the Str/Dex choice (they arrive as the `AttackSpec` input).
  - **M1-17:** armoured AC, Clatter on passive stealth, the gear roll-mode and crit-downgrade caps.
  - **M1-22:** guile, monster records, authored speed.
  - **M2:** mastery (double proficiency), Arcana, party-wide passives (best or worst member).

## Plan-level calls (mine, flagged for review)

1. **Stat block shape.** `UnitStats(int Level, Abilities Abilities, Ability MainAbility, Save TrainedSave, ImmutableArray<Skill> TrainedSkills)`, with `Abilities` a record struct of six ints (value equality, no default-array trap). `GameState.Create` rejects: level outside 0–12 (SM 6 proficiency row); a stored ability outside −3..+5 (ENM 5.1, SM 5.5); default or unsorted or repeated `TrainedSkills`. No class id is stored yet; M1-19 may add one.
2. **The `Skill` enum holds the nine M1 skills.** Arcana arrives in M2. Adding an enum value later keeps v5 saves readable.
3. **Save version 5** ("v5 (M1-08) adds unit stats"); the log follows. `UnitDto` gains `stats` at order 5, after `boons`. Enums are written by name. The digest's unit record grows from 5 to 6 fields (field 5, a stats record). The state record stays at 11 fields. `RulesVersion` stays 1: no command changes.
4. **Number ids** (camelCase, like `speed`):
   - Abilities `strength` … `presence`: rule `AbilityScore`, cap 5, base = the stored value.
   - `proficiency`: Stack, base by level: 0–4 → 2, 5–8 → 3, 9–12 → 4.
   - Saves `fortitude`, `reflex`, `will`: Stack, base = the better of the two folded abilities, + `proficiency` if it is the trained save.
   - `abilityDifficulty`: Stack, 8 + `proficiency` + the folded main ability.
   - Skills `athletics`, `acrobatics`, `stealth`, `mechanics`, `perception`, `intimidation`, `leadership`, `medicine`, `lore`: Stack, the folded ability + `proficiency` if trained.
   - `passivePerception`, `passiveStealth`: Stack, 10 + the folded skill number, so effects on the skill count in the passive (SM 6: "+ active effects").
   - `armourClass`: Stack, 10 + folded Dexterity (ruling 1).
   - `initiative`: Stack, folded Intelligence (effects may add).
   - `attack`: Stack, base 0. It holds flat bonuses to every attack the unit makes. The contest adds the attack's ability and proficiency (SM 6, "Ability + proficiency if trained in the weapon").
   - `medicineRestBonus`: Stack, folded Wisdom + `proficiency` if trained in Medicine, else 0 (DH 8: "Wis + proficiency"; kept apart from Recovery for CN 19.2).
   - `speed` keeps its constant base 6.
5. **Roll targets** added to `RollTargets.Core`: `saveRoll` (every save), `fortitudeSave`, `reflexSave`, `willSave`, `skillCheck` (every skill check), and one per skill, `athleticsCheck` … `loreCheck`. Conditions name both kinds: CO "disadvantage on attack rolls and skill checks"; "disadvantage on Will saves".
6. **One fold over several sources.** `Effects.RollModeOf(rules, s, sources)` ORs advantage and disadvantage over every (unit, target) pair, then applies D-15 once. An attack folds `(attacker, attackRoll)` with `(defender, attacksAgainst)`. Folding two already-folded modes would be wrong: the attacker's advantage and disadvantage cancel, but the defender's disadvantage then wins alone.
7. **Draw count.** A normal roll draws 1 d20; an advantage or disadvantage roll draws 2, in order, and keeps the higher or the lower. The first die is the plain die (Shade Fade II: "the others the first drawn"). The mode is folded before the draw (W 7.5 step 2), so the draw count depends only on state.
8. **Ties succeed:** a total that meets the target succeeds (TL "Smooth (meets the DC)"; W "total meets its AC"). Recorded in `docs/effects-vocabulary.md`.
9. **Crit is its own flag.** `AttackResult` keeps `Hit` and `Crit` apart, so M1-17's downgrade can clear `Crit` and keep the hit (AR 8.1: "A downgraded natural 20 still hits automatically; it is no longer a crit").
10. **No commands.** The contest actions arrive in M1-14. The replay Done-when is met by a scripted sequence of contest calls, run twice and across a save and load.
11. **`combat` consumers.** Task 3 fills the row's `consumers` in `streams.json`. This moves the content and manifest hashes (regenerate with `content-hashes`), but not the stream key: seed 7 stays `0xb1fbfb392345d2f0`.

## Review Focus

1. **Cap and penalty on a stored +5:** a unit stored at Strength +5 with a trait +2 and a condition −2 reads 3, and its Fortitude reads 3 if Constitution is lower (Task 2).
2. **A save after folding:** Strength +1, Constitution +2, a condition −3 on Constitution. Fortitude takes the better of the folded values, +1, not +2 (Task 2).
3. **Opposing roll-mode sources across units:** the attacker has advantage and disadvantage, the defender gives `attacksAgainst` disadvantage. The mode is normal (one draw), not disadvantage (Task 3).
4. **Naturals at the edge:** a natural 1 with a total above AC misses; a natural 20 with a total below AC hits and crits; a save whose natural 20 falls short fails; a check whose natural 1 meets the difficulty succeeds (Task 3).
5. **Initiative input order:** the same units passed in a different order draw the same dice for the same units (draws go in id order) and give the same order (Task 4).

---

### Task 1: The unit stat block and its persistence

**Files:**
- Create: `src/Core/Stats/Stats.cs` (enums, `Abilities`, `UnitStats`, `StatIds`)
- Modify: `src/Core/State/GameState.cs` (`Unit`, `Create` validation), `src/Core/Persistence/Snapshot.cs` (v5, `StatsDto`), `src/Core/State/StateDigest.cs` (unit record 6 fields)
- Modify tests: `tests/Core.Tests/Fixtures.cs`, `MoveTests.cs` (lines 37, 43, 64), `PathfindingTests.cs` (46, 49), `PersistenceTests.cs` (every `version\":N` literal moves up by one; add v4 to the retired versions), `StateDigestTests.cs` (new rows)
- Test: `tests/Core.Tests/UnitStatsTests.cs`

**Interfaces (namespace `Dungeon.Core.Stats`, except `Unit`):**
- `public enum Ability { Strength, Dexterity, Constitution, Intelligence, Wisdom, Presence }`
- `public enum Save { Fortitude, Reflex, Will }`
- `public enum Skill { Athletics, Acrobatics, Stealth, Mechanics, Perception, Intimidation, Leadership, Medicine, Lore }`
- `public readonly record struct Abilities(int Strength, int Dexterity, int Constitution, int Intelligence, int Wisdom, int Presence)` with `public int this[Ability a]`.
- `public sealed record UnitStats(int Level, Abilities Abilities, Ability MainAbility, Save TrainedSave, ImmutableArray<Skill> TrainedSkills)` with `public bool IsTrained(Skill s)`.
- `public static class StatIds`:
  - `string Number(Ability a)` gives `"strength"`;
  - `string Number(Save s)` gives `"fortitude"`;
  - `string Number(Skill s)` gives `"athletics"`;
  - `string RollTarget(Save s)` gives `"fortitudeSave"`;
  - `string RollTarget(Skill s)` gives `"athleticsCheck"`;
  - `Ability AbilityOf(Skill s)`, per SM 7.1: Athletics Str; Acrobatics, Stealth Dex; Mechanics, Lore Int; Perception, Medicine Wis; Intimidation, Leadership Presence;
  - `(Ability, Ability) AbilitiesOf(Save s)`, per SM 6: Fortitude (Str, Con), Reflex (Dex, Int), Will (Wis, Presence).
- `public sealed record Unit(UnitId Id, GridPos Pos, Faction Faction, ImmutableArray<int> Boons, UnitStats Stats)`
- `Fixtures.Stats`: level 1, all abilities 0, main Strength, trained Fortitude, no skills. `Fixtures.U` uses it.

- [ ] **Step 1: Write the failing tests** in `UnitStatsTests`:
  - `StatIds_cover_every_enum_value_once`: ids are distinct and ordinal-sorted per table, and `AbilityOf` and `AbilitiesOf` match the lists above.
  - `Create_rejects_an_invalid_stat_block` [Theory]: level −1 and 13; an ability of −4 or +6; a default `TrainedSkills`; `[Stealth, Athletics]` (unsorted); `[Lore, Lore]`. Each throws `ArgumentException` naming the unit id.
  - `Create_accepts_the_stat_block_bounds`: level 0 and 12, abilities −3 and +5.
  - `Stats_round_trip_through_a_save`: a unit with level 5, abilities (3, 2, 1, 1, 0, −1), main Dexterity, trained Reflex, skills [Acrobatics, Stealth]. The save contains `"stats":{"level":5,"abilities":{"strength":3,"dexterity":2,"constitution":1,"intelligence":1,"wisdom":0,"presence":-1},"mainAbility":"Dexterity","trainedSave":"Reflex","trainedSkills":["Acrobatics","Stealth"]}`, and save, load, save is identical.
  - `Load_rejects_a_bad_stats_block` [Theory]: a missing `stats`, a numeric enum, an unknown skill name, level 13. Each throws `SnapshotFormatException`.
  - `PersistenceTests`: v4 joins the retired versions (`Found == 4`).
  - `StateDigestTests.Every_field_reaches_the_digest`: one row per stats field (level, each ability, main ability, trained save, skills).
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "UnitStatsTests|PersistenceTests|StateDigestTests"`. Expected: FAIL to compile (`UnitStats` is missing).
- [ ] **Step 3: Implement.**
  - `Stats.cs`.
  - `Unit.Stats` and the `Create` checks, next to the `Boons` guard.
  - Snapshot v5: `StatsDto` and `AbilitiesDto` with `[JsonRequired]`, read with `PersistenceJson.ParseEnum`. Update the version-history comment.
  - The digest: field 5 of the unit record is a stats record (level, six abilities, main ability, trained save, skills as a list).
  - Fix the seven `Unit` constructions.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: PASS with no warnings; the M0 v1 golden is still refused.
- [ ] **Step 5: Commit** `git commit -m "M1-08: unit stat block (level, abilities, main ability, trained save and skills); save version 5; stats in the digest"`

---

### Task 2: The derived numbers and the non-stacking rule (Done-when 1)

**Files:**
- Create: `src/Core/Stats/StatNumbers.cs`
- Modify: `src/Core/Effects/Registries.cs`
  - `DerivedNumbers.Core` = `speed` plus `StatNumbers.Rows`.
  - `RollTargets.Core` gains the call 5 targets.
- Modify: `src/Core/Effects/Effects.cs`: the several-source `RollModeOf` (call 6).
- Modify: `docs/effects-vocabulary.md`:
  - 5.3 rows table: every call 4 row;
  - 5.4: roll targets, the several-source fold, ties succeed (call 8);
  - 9: the M1-08 row, saying guile moved to M1-22 and HP to M1-09.
- Test: `tests/Core.Tests/DerivedNumberTests.cs`; `EffectsKindTests.The_core_registries_hold_the_m1_07_rows` becomes `..._the_core_rows` and asserts the full id lists.

**Interfaces:**
- `public static class StatNumbers` (namespace `Dungeon.Core.Stats`):
  - `public static ImmutableArray<DerivedNumber> Rows { get; }`, the rows of call 4.
  - `public static ImmutableArray<string> RollTargets { get; }`, the targets of call 5.
- `Effects.RollModeOf(RulesContext rules, GameState s, IEnumerable<(UnitId Unit, string Target)> sources) -> RollMode`.
  - It throws `ArgumentException` for an unknown target or unit, or no sources.
  - The existing single-pair overload calls it.

- [ ] **Step 1: Write the failing tests** in `DerivedNumberTests`. All use `RulesContext.Create(content, flags: …)` with the production rows and fixture effects on the numbers.
  - `Each_ability_reads_its_stored_value` [Theory over `Ability`].
  - `Proficiency_follows_the_level_table` [Theory]: levels 0, 4, 5, 8, 9, 12 give 2, 2, 3, 3, 4, 4.
  - `A_save_takes_the_better_ability_plus_proficiency_if_trained` [Theory]:
    - Str 1, Con 2, untrained: Fortitude 2.
    - Same, trained at level 5: 5.
    - Dex −1, Int 3: Reflex 3.
    - Wis 0, Presence 0, trained at level 1: Will 2.
  - `A_save_reads_the_abilities_after_their_effects`: Review Focus 2.
  - `A_stored_plus_five_still_shows_its_penalty`: Review Focus 1.
  - `Ability_difficulty_is_eight_plus_proficiency_plus_the_main_ability`: main Wisdom +3 at level 5 gives 14.
  - `A_skill_is_its_ability_plus_proficiency_if_trained` [Theory over `Skill`]: abilities (1, 2, 3, 4, 5, −1), level 1, trained in that skill only. Each skill = its ability + 2. An untrained Stealth = 2.
  - `Passives_are_ten_plus_the_skill`:
    - Wis 2, trained Perception at level 1: passive perception 14.
    - Dex 0, untrained: passive stealth 10.
    - A gear `add` +5 on `perception` makes passive perception 19.
  - `Armour_class_unarmoured_is_ten_plus_dexterity`: Dex 3 gives 13; a condition −2 on `dexterity` gives 11.
  - `Initiative_is_intelligence`; `Attack_has_base_zero`.
  - `Medicine_rest_bonus_is_wisdom_plus_proficiency_only_if_trained`: Wis 2 at level 5 gives 5 trained and 0 untrained.
  - `Ability_non_stacking_across_kinds` [Theory]:
    - Trait +2 and gear +1 on `strength` give +2.
    - Penalties −1 and −2 give −2.
    - Base 4, trait +3, gear −2 gives 3.
  - `Several_sources_fold_once`: Review Focus 3, through `RollModeOf(rules, s, [(attacker, "attackRoll"), (defender, "attacksAgainst")])`.
  - `Every_roll_target_and_number_is_a_valid_modifier_target`: content naming each id loads.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "DerivedNumberTests|EffectsKindTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** `StatNumbers.Rows` with each base written as in call 4, reading `a.Unit.Stats` and `a.Number(StatIds.Number(...))`. Implement the several-source `RollModeOf`. Update the vocabulary doc.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: PASS. The M1-07 fold tests are unchanged: their fixture tables add the core rows, and no id clashes.
- [ ] **Step 5: Commit** `git commit -m "M1-08: the SM 6 numbers for M1-08 as fold queries; save and skill roll targets; one roll-mode fold over several sources"`

---

### Task 3: Dice and contests (Done-when 2)

**Files:**
- Create: `src/Core/Stats/Dice.cs`, `src/Core/Stats/Contests.cs`
- Modify: `content/registry/streams.json`, the `combat` row: `"consumers": ["attack rolls", "saves", "skill contests", "initiative"]`.
- Regenerate: `dotnet run --project src/Sim -- content-hashes` (`ContentHashes.g.cs`). Then `dotnet run --project src/Sim -- stream-handles` must leave `StreamHandles.g.cs` unchanged.
- Test: `tests/Core.Tests/ContestTests.cs`, `tests/Core.Tests/NaturalRollTests.cs`

**Interfaces (namespace `Dungeon.Core.Stats`):**
- `public readonly record struct D20Roll(RollMode Mode, int First, int? Second)` with `int Kept`.
  - `Second` is null exactly when the mode is normal.
  - `Kept` is the higher die for advantage and the lower for disadvantage.
- `public static class Dice`: `public static (D20Roll Roll, DrawStream Next) D20(DrawStream stream, RollMode mode)`, using `stream.Next()` and `KeyAlgebra.D20` (call 7).
- `public readonly record struct AttackSpec(Ability Ability, bool Trained)`
- `public enum Defence { Fortitude, Reflex, Will, PassivePerception, PassiveStealth }`
- `public readonly record struct ContestSetup(long Bonus, long Target, RollMode Mode)`
- `public readonly record struct AttackResult(D20Roll Roll, long Total, long Target, bool Hit, bool Crit)`
- `public readonly record struct CheckResult(D20Roll Roll, long Total, long Target, bool Success)`
- `public static class Contests`:
  - **Pure preparation (no draws):**
    - `PrepareAttack(RulesContext, GameState, UnitId attacker, UnitId defender, AttackSpec)`
      - Bonus = folded ability + (trained ? `proficiency` : 0) + `attack`.
      - Target = the defender's `armourClass`.
      - Mode = the fold of `attackRoll` and `attacksAgainst`.
    - `PrepareSave(…, UnitId defender, Save save, long difficulty)`
      - Bonus = the save number.
      - Mode = the fold of `saveRoll` and the save's target.
    - `PrepareSkill(…, UnitId user, Skill skill, long difficulty)`
      - Mode = the fold of `skillCheck` and the skill's target.
    - `PrepareSkillVsUnit(…, UnitId user, Skill skill, UnitId target, Defence defence)`
      - Target = 10 + the save for a save defence, or the passive number itself for a passive defence.
  - **Pure resolution:**
    - `ResolveAttack(ContestSetup, D20Roll) -> AttackResult`
      - Kept 20: hit and crit.
      - Kept 1: miss.
      - Otherwise: hit when total ≥ target.
    - `ResolveCheck(ContestSetup, D20Roll) -> CheckResult`: success when total ≥ target, never branching on the die.
  - **Drawing:** `Attack`, `SaveRoll`, `SkillCheck` and `SkillVsUnit`, with the same parameters as their `Prepare…`, returning `(result, GameState next)`.
    - `next` differs from the input only in `Rng.Combat`.
    - These are the only code that advances `Rng.Combat`, besides `Initiative`.

- [ ] **Step 1: Write the failing tests.** For the forced naturals, find seeds in the test with a `[MemberData]` search over seeds 1..10000 for the first stream whose first draw gives the wanted d20, then assert on that seed. Pin each seed found as a constant, with a comment giving its die.
  - `NaturalRollTests` (Review Focus 4):
    - `A_natural_20_hits_and_crits_below_the_armour_class`
    - `A_natural_1_misses_above_the_armour_class`
    - `A_save_with_a_natural_20_below_the_difficulty_fails`
    - `A_skill_check_with_a_natural_1_meeting_the_difficulty_succeeds`
    - `A_total_equal_to_the_target_succeeds` (attack and check)
    - `Crit_is_cleared_independently_of_hit`: `result with { Crit = false }` keeps `Hit`.
  - `ContestTests`:
    - `Dice_draw_one_die_normal_and_two_with_a_mode` [Theory]: counter +1 / +2 / +2; `Kept` = First, max, min; `First` equals the normal roll's die from the same stream.
    - `Each_contest_advances_only_the_combat_counter`: the `Snapshot.Save` texts differ only in the combat counter, and `Rng.Shrine` is unchanged.
    - `Prepare_draws_nothing`: the state is unchanged.
    - `Prepare_attack_combines_ability_proficiency_and_flat_adds`:
      - Str 3, trained, level 5, gear +1 on `attack`, defender Dex 2. Bonus 7, target 12.
      - Untrained: bonus 4.
    - `Prepare_save_reads_the_save_and_both_roll_targets`: a `saveRoll` advantage and a `willSave` disadvantage on a Will save gives normal.
    - `Skill_against_a_unit_meets_ten_plus_the_matching_save_including_training`: the target trained in Fortitude at level 5 with Str 2 gives 15.
    - `Skill_against_a_passive_meets_the_passive_itself`.
    - `Several_sources_fold_once_in_an_attack`: Review Focus 3, through `PrepareAttack`, with one draw.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "ContestTests|NaturalRollTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** `Dice` and `Contests`. Edit the `combat` row, run `content-hashes`, and run `stream-handles` to check it makes no diff.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: PASS. `RunStreamsTests` still pins `combat` at `0xb1fbfb392345d2f0`, and `ContentHashTests` passes. A golden that depends on the manifest hash (for example a pinned `RunKey`) may move only through the regenerated `ContentHashes.g.cs`. Report it, never hand-edit it.
- [ ] **Step 5: Commit** `git commit -m "M1-08: d20 rolls on the combat stream; attack, save and skill contests with pure preparation and resolution; natural 20 and 1 on attacks only"`

---

### Task 4: Initiative and the scripted contest replay (Done-when 3)

**Files:**
- Create: `src/Core/Stats/Initiative.cs`
- Test: `tests/Core.Tests/InitiativeTests.cs`, `tests/Core.Tests/ContestReplayTests.cs`
- Modify (controller step): `docs/roadmap/m1-vertical-slice.md` (the M1-08 Delivers line, rulings 1–2; M1-22 gains guile), `docs/roadmap/README.md` (the D-16 row; line 214 "Next step to start"), `README.md` (layout line `Stats/`).

**Interfaces (namespace `Dungeon.Core.Stats`):**
- `public readonly record struct InitiativeEntry(UnitId Unit, int Natural, long Total, long Intelligence)`
- `public static class Initiative`: `public static (ImmutableArray<InitiativeEntry> Order, GameState Next) Roll(RulesContext rules, GameState s, IEnumerable<UnitId> units)`.
  - It rejects unknown or repeated ids, or an empty list.
  - It draws one normal d20 per unit in ascending id order.
  - Total = natural + `initiative`.
  - It sorts by total descending, then folded `intelligence` descending, then id ascending, with a `[TotalComparer]`.

- [ ] **Step 1: Write the failing tests.**
  - `InitiativeTests`:
    - `Initiative_orders_by_total_then_intelligence_then_id`. Use fixed dice through a seed search as in Task 3, and pick abilities to force each tie-break.
    - `Initiative_draws_in_id_order_whatever_the_input_order` (Review Focus 5).
    - `Initiative_rejects_unknown_repeated_or_no_units`.
    - `Initiative_reads_effects_on_intelligence`: a +2 on `intelligence` changes both the total and the tie-break.
  - `ContestReplayTests`:
    - `A_scripted_contest_sequence_replays_identically`. The script is a fixed list of steps (initiative for two units; an attack with advantage; a save against the attacker's `abilityDifficulty`; a skill against a unit's Reflex; a skill check against 15; an attack with disadvantage). Run it twice from the same state and compare:
      - the list of results;
      - `Snapshot.Save`;
      - `StateDigest.Bytes`;
      - `Rng.Combat.Counter` against the number of dice drawn.
    - `A_contest_sequence_continues_identically_after_a_save_and_load`: split the script at step 3.
    - `The_seed_7_contest_dice_are_pinned`: the naturals of the script on seed 7 as a golden list.
    - `A_move_leaves_the_combat_counter_unchanged` (SV `ExplorationCombatIsolation`).
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "InitiativeTests|ContestReplayTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** `Initiative.Roll`.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`, `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` and the analyzer smoke build. Expected: PASS, 0 warnings.
- [ ] **Step 5: Commit** `git commit -m "M1-08: initiative on the combat stream (total, Intelligence, unit order); scripted contest replay"`
- [ ] **Step 6 (controller):**
  - The roadmap amendment and the D-16 row.
  - README line 214 and the layout line.
  - The Godot hand-check (behaviour unchanged; the M0 save is still refused).
  - Issue #11, push, and open the PR.
