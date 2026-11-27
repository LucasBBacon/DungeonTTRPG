# M1-09 Damage and HP Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Every unit has current HP, a derived maximum and a derived Recovery, and damage and healing go through one function in the DH 20.3 order: typed parts rolled on `combat` (dice twice on a crit), each part through the ladder, summed, then lowered.

**Architecture:**
- **HP state:** `Unit` gains one field, `int Hp` (current HP, DH 7.3: "Each unit stores current HP; the maximum is derived on demand"). `UnitStats` gains the HP basis: a party unit's HP tier (DH 7.1) or a monster's table HP (ENM 6.2). M1-19 fills the tier from class records and M1-22 the table HP from monster records; until then, code and fixtures fill them.
- **Derived numbers:** `maxHp` and `recovery` join the M1-07 registry as fold queries, so effects on Constitution flow into both. `DerivedNumber` gains an optional floor, applied after the whole fold (Recovery "minimum 1", DH 8).
- **Damage:** typed parts (`DamagePart`), a dice helper on `combat`, and the damage function in `src/Core/Damage/`: a draw step (`Roll`), a pure step (`Resolve`) and a write step (`Apply`), like the M1-08 contests. Ladder steps come from flags on the unit's active effects, so a trait is a `trait`-kind effect that sets them (SM 9: "A trait is a named bundle of effects").
- **Healing:** one pure clamp to the current maximum, with the overheal reported. Potions and rests (M1-18) call it.

**Tech Stack:** C# 12 / .NET 8; the M1-07 effects registries, folds and flags; the M1-08 stat block and contests; the M1-05 `combat` stream; the M1-06 save and digest patterns; xUnit.

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-09](../../roadmap/m1-vertical-slice.md#m1-09-damage-and-hp), implementing:
- **SM 8.1, 8.3, 8.4, 9, 11** (`2026-11-16-character-stat-model-design.md`, lines 385–403, 440–505, 522–554, 603–613): the ten types, the ladder and its order of operations, traits as effects.
- **AB 9.1** (`2026-11-16-ability-resource-model-design.md`, lines 727–742), M1 without riders.
- **DH 7, 8, 9, 15.2, 20.3** (`2026-11-17-death-healing-design.md`, lines 525–662, 1260–1295, 1990–2030), and DH-4, DH-5, DH-6 (lines 2457–2459).
- **D-02** (`docs/roadmap/README.md`, line 156): the crit wording, checked against SRD 5.2.1 (below).
- `docs/effects-vocabulary.md`, sections 3, 5 and 8–9.

## D-02: the SRD 5.2.1 crit check (done 2026-11-27)

SRD 5.2.1 (CC-BY-4.0, May 2025), page 16, "Critical Hits": a crit rolls the attack's damage dice twice and adds them together; modifiers are added once, as normal; extra damage dice from features (its example is Sneak Attack) are rolled twice too. Page 13, "Rolling 20 or 1" applies to attack rolls only. This matches DH 9 ("A crit rolls all damage dice twice ... Exploit dice double with the weapon dice; flat terms and Precision do not") and SM 6. No change to the designs. The ladder deliberately departs from the SRD (Weak is +half, not double; a resistance and a weakness cancel), and SM 8.3 governs. Recorded on issue #12.

## Owner rulings for this step (2026-11-27)

1. **The minimum of 1 applies to each part.** A non-immune part deals at least 1, so a hit of two typed parts deals at least 2 (SM 8.3: "Damage that is not immune is at least 1"; "Mixed damage resolves each part separately").
2. **The HP numbers live in code**, in one named table (`DeathNumbers`) with DH references: `hp.adventurerBonus` 11, `hp.classTiers` 6 / 7 / 8, `recovery.base` 4, `recovery.perLevel` 2. They are client and lead numbers, not builder tuning (DH 7.1, 8). `death.json` (DH 20.1) arrives with the first real tuning lever; until then there is no tuning-file kind.

## Global Constraints

- DH 7.1: "Max HP = `hp.adventurerBonus` 11 + level × max(1, classHp + Con)." Tiers "slight 6, sound 7, sturdy 8". "Never rolled; at least 1 per level." "Monsters keep their table HP (enemies 6.2); no bonus."
- DH 7.3: "Each unit stores current HP; the maximum is derived on demand." "Every other change to the maximum only clamps current HP."
- DH 8: "Recovery = `recovery.base` 4 + `recovery.perLevel` 2 × level + Con, minimum 1." "Recovery reads neither the class HP tier nor Scarred."
- DH 9: "A crit rolls all damage dice twice, for every unit, party and monsters alike." "flat terms and Precision do not" double.
- SM 8.3: Weak "Damage + half"; Resistant "Half damage"; Immune "No damage". "Two sources of the same resistance count once." "A resistance and a weakness to the same type cancel to normal." "Order of operations: flat changes first, then the ladder step, rounded down." "Damage that is not immune is at least 1."
- SM 9: "Damage travels as a list of typed parts. Each part passes through flat modifiers, then the target's ladder step for its type." "Until riders arrive in M2, a weakness in M1 is extra damage only. The damage step should leave a clear place for the rider to be added."
- AB 9.1: "Damage is a list of typed parts, each resolved separately." M1 has no riders.
- DH 20.3 is the normative order; this step builds steps 2 (the ladder), 3, 6 and the HP side of 7 (plan call 1).
- SV 4.3: damage rolled in combat draws on `combat`.
- SM 3: integer arithmetic only, rounding down (`IntMath.FloorDiv` for any division).
- The determinism analyzer stays green: no floating point, no Dictionary or HashSet enumeration, `[TotalComparer]` on every sort, and `DrawStream` only through `Next()`.
- Out of scope:
  - **M1-10:** DH 20.3 step 4 (the out-of-combat and trap floors), what happens at 0 HP (death, `RunEnded`, removing a dead monster), `combatActive`, level-up HP (DH 7.3–7.4).
  - **M1-11:** events for damage and healing, with the ability pipeline that raises them.
  - **M1-12:** clamping current HP when an effect lowers the maximum (`HitPoints.ClampToMax` is provided here; the application pipeline calls it).
  - **M1-16, M1-20:** weapon and ability damage parts (Precision, exploit dice); they build `DamagePart` lists.
  - **M1-18:** potions, rests and the Medicine bonus's use; `HealApplied` and its sources.
  - **M1-22:** the trait budget and immunity checks (SM 8.4, ENM 20.1) with monster records.
  - **M2 or later:** riders, layers and shells, transforms, Duck (DH 20.3 step 3a, Shade), the save chain (Death-warded, the phial; step 5), Downed and companions (step 1; no companions in M1), "ignores resistance", the necrotic heal block, Scarred.
  - **Later (Bolstered):** temporary HP (roadmap D-17, [M1-10 rulings record](../specs/2026-11-27-m1-10-rulings-design.md) 1).

## Plan-level calls (mine, flagged for review)

1. **Which DH 20.3 steps this step builds.** Step 2 is the ladder (no shells, transforms or temporary HP in M1); step 3 sums; step 6 lowers HP, never below 0; step 7 reports that HP reached 0 and goes no further. Steps 1, 3a, 4 and 5 belong to later steps (Global Constraints). `Resolve` lists the steps in order in its body, with each later step as a comment naming its owner, so M1-10 inserts step 4 between the sum and the lowering.
2. **The HP basis on the stat block.** `UnitStats` gains `HpTier HpTier` and `int TableHp`. `HpTier` is `Table`, `Slight` (6), `Sound` (7), `Sturdy` (8). `GameState.Create` rejects: a party unit whose tier is `Table`; an enemy whose tier is not `Table`; `TableHp` other than 0 with a tier; `TableHp` below 1 with `Table`; `Hp` below 0. It does not check `Hp` against the maximum: a save loads through `Create` before its effects are back, and an effect may raise the maximum. Damage, healing and `ClampToMax` keep `Hp` at or below the maximum.
3. **Save version 6** ("v6 (M1-09) adds current HP and the HP basis"); the log follows. `UnitDto` gains `hp` at order 6, after `stats`; `StatsDto` gains `hpTier` (by name) and `tableHp`. The digest's unit record grows from 6 to 7 fields (field 6, `Hp`), and the stats record gains the two fields at its end. `RulesVersion` stays 1: no command changes.
4. **Number rows.** `maxHp`: Stack, floor 1. Base: with a tier, 11 + level × max(1, tier + folded Constitution); with `Table`, `TableHp`. `recovery`: Stack, floor 1, base 4 + 2 × level + folded Constitution, for every unit. A level-0 tiered unit has maximum 11.
5. **A floor on a derived number.** `DerivedNumber` gains `long? Floor` as an optional last parameter (default null), applied after the whole fold, cap included. `RulesContext` rejects a floor on an `AbilityScore` number (SM 12 leaves ability floors open) and on a `Speed` number (speed 0 must override everything, CO 6.7).
6. **Ladder steps are flags.** `Flags.Core` gains 30 names, `resistant.<type>`, `weak.<type>` and `immune.<type>`, one of each per damage type (type ids `slashing`, `piercing`, `bludgeoning`, `fire`, `cold`, `lightning`, `acid`, `psychic`, `radiant`, `necrotic`). Flags are booleans, so two sources of a resistance count once by construction. The step for a type: `immune` set → Immune (it wins over a weakness); else both `resistant` and `weak` → Normal; else `resistant` → Resistant; else `weak` → Weak; else Normal.
7. **Part arithmetic.** A part's raw amount is the sum of its dice plus its flat amount ("flat changes first"). Then Weak gives raw + FloorDiv(raw, 2), Resistant FloorDiv(raw, 2), Normal raw, Immune 0. A non-immune result below 1 becomes 1 (owner ruling 1). M1 has no flat damage modifiers from effects; the part's own flat is the only flat change.
8. **Crit dice.** On a crit each part draws `2 × Dice` dice; its flat is added once. Draw order: parts in list order, each part's dice in order, one `combat` draw per die. A die of `n` sides is `Bound(u, n) + 1` (`Dice.Die`). `crit.mode` `doubleDice` is the only mode, so no switch is built.
9. **Rider place.** `DamagePart` has no `rider` flag in M1 (AB 9.1 "M1, no riders"). `PartOutcome` records each part's type, step and amount dealt: the M2 rider step reads it ("a target weak to a part's type gets that type's rider automatically").
10. **Healing.** `HitPoints.Heal` adds an amount up to the current maximum and reports the overheal. A negative amount throws. It does not check Downed (none in M1) or the necrotic block (M2), and it does not refuse a unit at 0 HP: M1-10 owns death and gates what may target a dead unit.
11. **No events and no commands.** Like the M1-08 contests, the functions return outcomes and states. M1-11 raises events when the ability pipeline calls them. `diceBonus` stays reserved: no M1-09 rule or content gives a dice bonus, and the vocabulary row moves to "the first step whose content gives one".
12. **`combat` consumers.** Task 4 adds `"damage rolls"` to the row's `consumers` in `streams.json`. This moves the content and manifest hashes (regenerate with `content-hashes`), not the stream key: seed 7 stays `0xb1fbfb392345d2f0`.

## Review Focus

1. **A flat penalty below zero:** a part 1d4 − 5 that rolls 2 deals 1, not 0 or −3; the same part against an immune target deals 0 (Task 3).
2. **Two resistances and one weakness:** two `trait` effects each giving `resistant.fire` and a `condition` giving `weak.fire` read Normal; 7 fire deals 7 (Task 3).
3. **Overkill and repeat damage:** 30 damage to a unit on 12 HP leaves 0 and reports `ReachedZero`; a second hit on that unit still leaves 0 (Task 4).
4. **Heal against a lowered maximum:** a unit on 17 of 17 whose Constitution falls by 2 through an effect has maximum 15; `ClampToMax` sets HP to 15, and healing 5 then gives 15 with overheal 5 (Task 4).
5. **Per-level minimum under effects:** a tier-6 unit at level 3 with stored Constitution −3 and a −4 condition on Constitution has maximum 11 + 3 × 1 = 14, not 11 + 3 × (−1) (Task 2).

---

### Task 1: Current HP and the HP basis, persisted

**Files:**
- Create: `src/Core/Stats/HitPoints.cs` (`HpTier`, `DeathNumbers`)
- Modify: `src/Core/Stats/Stats.cs` (`UnitStats`), `src/Core/State/GameState.cs` (`Unit`, `CheckStats`), `src/Core/Persistence/Snapshot.cs` (v6, `UnitDto.hp`, `StatsDto` fields), `src/Core/State/StateDigest.cs` (unit record 7 fields, stats record), `dungeon-ttrpg/scripts/DemoLevel.cs` (the hero's tier and HP)
- Modify tests: `tests/Core.Tests/Fixtures.cs`, every direct `new Unit(...)` and `new UnitStats(...)` in tests, `PersistenceTests.cs` (every `version\":N` literal moves up by one; v5 joins the retired versions), `StateDigestTests.cs`
- Test: `tests/Core.Tests/HitPointStateTests.cs`

**Interfaces (namespace `Dungeon.Core.Stats`, except `Unit`):**
- `public enum HpTier { Table = 0, Slight = 6, Sound = 7, Sturdy = 8 }`: the value is the class amount per level.
- `public static class DeathNumbers` with `public const int AdventurerBonus = 11;`, `RecoveryBase = 4;`, `RecoveryPerLevel = 2;` (doc comments cite DH 7.1 and 8 and owner ruling 2).
- `public sealed record UnitStats(int Level, Abilities Abilities, Ability MainAbility, Save TrainedSave, ImmutableArray<Skill> TrainedSkills, HpTier HpTier, int TableHp)`.
- `public sealed record Unit(UnitId Id, GridPos Pos, Faction Faction, ImmutableArray<int> Boons, UnitStats Stats, int Hp)`.
- `Fixtures.Stats` gains `HpTier.Slight`, `TableHp` 0 (maximum 17 at level 1, Constitution 0); `Fixtures.FullHp = 17`; `Fixtures.U` passes it. `Fixtures.EnemyStats`: as `Stats` with `HpTier.Table`, `TableHp` 12.

- [ ] **Step 1: Write the failing tests** in `HitPointStateTests`:
  - `Create_rejects_a_party_unit_on_table_hp`, `Create_rejects_an_enemy_with_an_hp_tier`, `Create_rejects_table_hp_beside_a_tier`, `Create_rejects_table_hp_below_one`, `Create_rejects_negative_hp`: each `Assert.Throws<ArgumentException>`, with the message naming the unit id and the field.
  - `Create_accepts_zero_hp` and `Create_accepts_hp_above_the_unfolded_maximum` (plan call 2).
  - `Hp_and_the_hp_basis_survive_save_and_load`: a party unit on 9 HP, tier `Sturdy`, and an enemy on table HP 12 at 5 HP round-trip; the saved text has `"version":6`, `"hp":9` and `"hpTier":"Sturdy"`.
  - `A_v5_save_is_refused`: `SnapshotVersionException`, as for the earlier retired versions.
  - `A_save_missing_hp_is_a_format_error`: removing the `hp` key gives `SnapshotFormatException`.
  - In `StateDigestTests`: changing only one unit's `Hp`, only its `HpTier`, or only its `TableHp` changes the digest.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "HitPointStateTests|StateDigestTests"`. Expected: build fails (no `HpTier`, `Unit` has five fields).
- [ ] **Step 3: Implement** the types, the `CheckStats` rules of plan call 2 (messages in the existing `Invalid units: unit N ...` form), the v6 save (doc comment on `CurrentVersion` gains the v6 line), and the digest fields. Update the fixtures, the tests that build units, and `DemoLevel.cs` (the hero is `Slight` on full HP, 17 with its stats; check by computing, not by guessing).
- [ ] **Step 4: Run** `dotnet test Dungeon.sln` and `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`. Expected: all pass, 0 warnings.
- [ ] **Step 5: Commit** `M1-09: current HP and the HP basis on every unit; save version 6`.

### Task 2: Max HP and Recovery as derived numbers

**Files:**
- Modify: `src/Core/Effects/Registries.cs` (`DerivedNumber.Floor`), `src/Core/Effects/Effects.cs` (apply the floor last), `src/Core/Effects/RulesContext.cs` (reject a floor on `AbilityScore` and `Speed`), `src/Core/Stats/StatNumbers.cs` (two rows), `src/Core/Stats/HitPoints.cs` (`HitPoints.Max`, `HitPoints.Recovery`), `docs/effects-vocabulary.md` (section 5 and 5.3: the floor and the two rows; section 9 M1-09 row)
- Modify tests: `tests/Core.Tests/EffectsKindTests.cs` (`The_core_registries_hold_the_core_rows`)
- Test: `tests/Core.Tests/HitPointNumberTests.cs`, and in `EffectFoldTests.cs` the floor tests

**Interfaces:**
- `public sealed record DerivedNumber(string Id, NumberRule Rule, NumberBase Base, long? Cap, long? Floor = null)`; the constant-base constructor gains the same optional `long? floor = null`.
- `HitPoints.Max(RulesContext rules, GameState s, UnitId unit) => Effects.Number(rules, s, unit, "maxHp")`, and `HitPoints.Recovery(...)` reading `"recovery"`.
- Number ids `maxHp` and `recovery` (plan call 4).

- [ ] **Step 1: Write the failing tests:**
  - In `EffectFoldTests`: `A_floor_binds_after_every_modifier` (a Stack number with base 2, floor 1, and `add -5` reads 1); `A_floor_on_an_ability_score_or_speed_number_is_rejected` (`RulesContext` throws `ArgumentException` naming the id).
  - `HitPointNumberTests.The_DH_7_1_reference_table_is_reproduced`, a theory over (tier, Con, level, max): reference `Slight`, +1: (1, 18), (2, 25), (3, 32), (5, 46), (7, 60), (10, 81); lowest legal `Slight`, −1: (1, 16), (2, 21), (3, 26), (5, 36), (7, 46), (10, 61); `Sound`, +2: (1, 20), (2, 29), (3, 38), (5, 56), (7, 74), (10, 101); `Sturdy`, with the Constitution each row implies: (+3, 1, 22), (+3, 2, 33), (+3, 3, 44), (+4, 5, 71), (+4, 7, 95), (+5, 10, 141).
  - `The_DH_8_recovery_table_is_reproduced`, a theory over (Con, level, recovery), the DH 8 columns exactly: reference +1: (1, 7), (3, 11), (5, 15), (7, 19), (10, 25); Con −1: (1, 5), (3, 9), (5, 13), (7, 17), (10, 23); Con max (+3 at levels 1–3, +4 at 5–7, +5 at 10): (1, 9), (3, 13), (5, 18), (7, 22), (10, 29). And `Rest_with_Medicine_matches_DH_8`: Recovery + `medicineRestBonus` with Wisdom +2 trained in Medicine gives 11, 15, 20, 24, 31 at levels 1, 3, 5, 7, 10 (Con +1).
  - `The_per_level_minimum_reads_folded_constitution` (Review Focus 5): 14.
  - `An_effect_on_constitution_moves_max_hp_and_recovery`: a `gear` +1 on Constitution raises a level-3 reference hero's maximum from 32 to 35 and Recovery from 11 to 12.
  - `Table_hp_ignores_level_and_constitution`: an enemy at table HP 12, level 3, Constitution +4 has maximum 12.
  - `Recovery_has_a_floor_of_one`: level 0, Constitution −3, a −4 condition on Constitution gives 1. `Recovery_reads_no_hp_tier`: the same stats on `Slight` and `Sturdy` give the same Recovery.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "HitPointNumberTests|EffectFoldTests"`. Expected: build fails (no `Floor`, no `maxHp`).
- [ ] **Step 3: Implement** the floor (in `Effects.Fold`, after the rule's result), its `RulesContext` check, the two rows (reading `DeathNumbers` and `(int)HpTier`), and the `HitPoints` reads. Update the registry-rows test and the vocabulary doc.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass.
- [ ] **Step 5: Commit** `M1-09: max HP and Recovery as fold queries; a floor on derived numbers`.

### Task 3: Damage types, parts and the ladder

**Files:**
- Create: `src/Core/Damage/DamageTypes.cs` (`DamageType`, `LadderStep`, `DamageIds`, `DamagePart`), `src/Core/Damage/Ladder.cs`
- Modify: `src/Core/Effects/Registries.cs` (`Flags.Core` gains the 30 names, built from `DamageIds`), `docs/effects-vocabulary.md` (flags table; section 9 M1-09 row)
- Modify tests: `EffectsKindTests.The_core_registries_hold_the_core_rows`
- Test: `tests/Core.Tests/LadderTests.cs`

**Interfaces (namespace `Dungeon.Core.Damage`):**
- `public enum DamageType { Slashing, Piercing, Bludgeoning, Fire, Cold, Lightning, Acid, Psychic, Radiant, Necrotic }` (SM 8.1 order).
- `public enum LadderStep { Weak, Normal, Resistant, Immune }`.
- `public static class DamageIds`: `string Of(DamageType t)` (`"fire"`); `string Resistant(DamageType t)` (`"resistant.fire"`), `Weak(...)`, `Immune(...)`; `ImmutableArray<string> LadderFlags` (all 30, ordinal-sorted).
- `public readonly record struct DamagePart(DamageType Type, int Dice, int Sides, int Flat)`; `Dice` ≥ 0, `Sides` ≥ 1 when `Dice` > 0. The function that consumes parts (Task 4) throws `ArgumentException` naming the part's index otherwise.
- `public static class Ladder`:
  - `LadderStep StepFor(RulesContext rules, GameState s, UnitId target, DamageType type)`: plan call 6, reading `Effects.Flag(rules, s, target, "unit", ...)`.
  - `long Apply(LadderStep step, long raw)`: plan call 7, owner ruling 1.

- [ ] **Step 1: Write the failing tests** in `LadderTests` (effects built with the M1-07 test helpers, `trait` and `condition` kinds):
  - `Ladder_arithmetic_matches_SM_8_3`, a theory over (step, raw, dealt): (Weak, 7, 10), (Weak, 1, 1), (Normal, 7, 7), (Resistant, 7, 3), (Resistant, 1, 1), (Immune, 7, 0), (Normal, −3, 1), (Resistant, 0, 1), (Immune, −3, 0).
  - `No_flag_reads_normal`; `A_resistant_flag_reads_resistant`; `A_weak_flag_reads_weak`.
  - `Two_resistances_count_once`: two effects with `resistant.fire` read Resistant (and 9 fire deals 4, not 2).
  - `Resistance_and_weakness_cancel` and Review Focus 2: two resistances and one weakness read Normal.
  - `Immunity_wins_over_weakness`: `immune.psychic` and `weak.psychic` read Immune.
  - `Steps_are_per_type`: `resistant.fire` leaves cold Normal.
  - `A_trait_effect_carries_its_profile`: a `trait`-kind effect `trait.test-skeletal` with `resistant.piercing` and `weak.bludgeoning` gives exactly those steps (SM 8.4: a trait "always means the same profile").
  - `DamageIds_cover_every_type_once`: 10 ids, 30 distinct flag names, `LadderFlags` ordinal-sorted, all present in `Flags.Core`.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter LadderTests`. Expected: build fails.
- [ ] **Step 3: Implement** the types, `DamageIds`, the flag rows, and `Ladder`, using `IntMath.FloorDiv` for both halvings.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: all pass, the analyzer green.
- [ ] **Step 5: Commit** `M1-09: ten damage types, typed parts, the ladder from effect flags`.

### Task 4: The damage function, crits and healing

**Files:**
- Create: `src/Core/Damage/DamageFunction.cs` (`Damage`, `DamageRoll`, `PartRoll`, `PartOutcome`, `DamageOutcome`)
- Modify: `src/Core/Stats/Dice.cs` (`Dice.Die`), `src/Core/Stats/HitPoints.cs` (`Heal`, `HealOutcome`, `ClampToMax`), `content/registry/streams.json` (consumers), `src/Core/Content/ContentHashes.g.cs` (regenerated), `docs/effects-vocabulary.md` (section 8, the `diceBonus` row), `docs/roadmap/README.md` (next step: M1-10, once M1-09 is merged), `README.md` (a `Damage/` layout line)
- Test: `tests/Core.Tests/DamageFunctionTests.cs`, `tests/Core.Tests/HealingTests.cs`, `tests/Core.Tests/DamageReplayTests.cs`

**Interfaces:**
- `Dice.Die(DrawStream stream, int sides) -> (int Face, DrawStream Next)`: one draw, `(int)KeyAlgebra.Bound(u, (ulong)sides) + 1`; throws `ArgumentOutOfRangeException` for `sides` < 1.
- `public readonly record struct PartRoll(DamagePart Part, ImmutableArray<int> Faces, long Raw)`: `Raw` = sum of `Faces` + `Part.Flat`.
- `public sealed record DamageRoll(ImmutableArray<PartRoll> Parts, bool Crit)`.
- `public readonly record struct PartOutcome(DamageType Type, LadderStep Step, long Raw, long Dealt)`.
- `public sealed record DamageOutcome(UnitId Target, ImmutableArray<PartOutcome> Parts, long Total, int HpBefore, int HpAfter, bool ReachedZero)`.
- `public static class Damage` (namespace `Dungeon.Core.Damage`):
  - `(DamageRoll Roll, GameState Next) Roll(GameState s, ImmutableArray<DamagePart> parts, bool crit)`: draws on `s.Rng.Combat` only (plan call 8), validates the parts.
  - `DamageOutcome Resolve(RulesContext rules, GameState s, UnitId target, DamageRoll roll)`: pure; plan call 1's steps in order; `HpAfter` = max(0, min(`Hp`, `maxHp`) − `Total`); `ReachedZero` = `HpAfter` == 0.
  - `(DamageOutcome Outcome, GameState Next) Apply(RulesContext rules, GameState s, UnitId target, DamageRoll roll)`: `Resolve`, then writes `HpAfter`.
  - `(DamageOutcome Outcome, GameState Next) Deal(RulesContext rules, GameState s, UnitId target, ImmutableArray<DamagePart> parts, bool crit)`: `Roll` then `Apply`.
  - An unknown target throws `ArgumentException`, as the contests do.
- `public readonly record struct HealOutcome(UnitId Target, long Amount, long Healed, long Overheal, int HpAfter)`.
- `HitPoints.Heal(RulesContext rules, GameState s, UnitId target, long amount) -> (HealOutcome, GameState)` (plan call 10) and `HitPoints.ClampToMax(RulesContext rules, GameState s, UnitId target) -> GameState`.

- [ ] **Step 1: Write the failing tests:**
  - `DamageFunctionTests`:
    - `Die_faces_cover_one_to_sides`: over 2 000 draws of a d6 from seed 7, every face 1–6 appears and none other.
    - `A_normal_roll_draws_one_die_per_dice`: parts 2d6 + 1d4 advance `combat` by exactly 3; `Rng.Shrine` is unchanged.
    - `A_crit_draws_every_part_twice_and_adds_flat_once`: 1d6 + 3 on a crit draws 2 dice, and `Raw` = the two faces + 3. On the same seed, the crit's first face equals the normal roll's face (same draw order).
    - `A_part_with_no_dice_draws_nothing`: 0d0 + 4 advances `combat` by 0 and deals 4.
    - `Parts_resolve_separately_and_sum`: 5 slashing and 6 fire, rolled with fixed faces built through `PartRoll` directly, against `resistant.fire`: outcomes (Normal, 5) and (Resistant, 3), total 8, HP 17 → 9.
    - `Each_part_deals_at_least_one` (owner ruling 1): two resisted parts of raw 1 each total 2.
    - Review Focus 1: 1d4 − 5 with face 2 deals 1; against `immune.slashing` it deals 0 and HP is unchanged.
    - Review Focus 3: 30 on 12 HP leaves 0 with `ReachedZero`; a second `Apply` leaves 0, total still reported.
    - `Resolve_is_pure`: calling `Resolve` leaves the state's save text unchanged, and `Apply` then writes exactly `HpAfter`.
    - `An_invalid_part_is_rejected`: `Dice` −1, or `Sides` 0 with `Dice` 2, throws `ArgumentException` naming index 1 when it is the second part.
  - `HealingTests`:
    - `Heal_stops_at_the_maximum`: 10 of 17 healed by 5 gives 15, overheal 0; healed by 9 gives 17, overheal 2.
    - Review Focus 4, through `ClampToMax` and `Heal`.
    - `A_negative_heal_is_rejected`: `ArgumentOutOfRangeException`.
    - `Heal_with_Recovery_heals_DH_8_amounts`: a level-1 reference hero (Con +1) on 5 HP healed by `HitPoints.Recovery` reaches 12.
  - `DamageReplayTests.A_scripted_attack_and_damage_sequence_replays_identically`: seed 7, the M1-08 `ContestKit` units, three rounds of `Contests.Attack` then, on a hit, `Damage.Deal` with 1d8 + 2 slashing (`crit` from the attack result). Run twice: the same save text. Run with a save and load after round 1: the same save text. Pin the final HP values as a golden after the first green run, and say so in the test's comment.
- [ ] **Step 2: Run** `dotnet test tests/Core.Tests --filter "DamageFunctionTests|HealingTests|DamageReplayTests"`. Expected: build fails.
- [ ] **Step 3: Implement** `Dice.Die`, the damage function, and healing. In `Resolve`, write the DH 20.3 steps as numbered comments, the later ones naming M1-10 (step 4, and step 7's consequences) M2 (steps 1, 3a, 5, the per-part shells) and Later (Bolstered: temporary HP).
- [ ] **Step 4: Content and docs.** Add `"damage rolls"` to the `combat` row's `consumers`; run the `content-hashes` and `stream-handles` generators (the stream-handles output must not change). Update the vocabulary doc's `diceBonus` row (plan call 11), the README layout line, and the roadmap README next-step line.
- [ ] **Step 5: Run** `dotnet test Dungeon.sln`, `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`, and the analyzer smoke build. Expected: all pass, 0 warnings, the smoke build still reports DUN001, no generator drift.
- [ ] **Step 6: Commit** `M1-09: the damage function in DH 20.3 order; crits roll dice twice; healing clamps to the maximum`.

## Done when → evidence

- SM 11 ladder and order-of-operations tests pass: `LadderTests`, and `DamageFunctionTests` (`Parts_resolve_separately_and_sum`, `Each_part_deals_at_least_one`, the flat-penalty case).
- The DH 7.1 reference table (18/25/32) is reproduced by test: `HitPointNumberTests.The_DH_7_1_reference_table_is_reproduced` (the whole table), with the DH 8 Recovery table beside it.
- Crit wording checked against SRD 5.2.1: the D-02 section above, recorded on issue #12; `A_crit_draws_every_part_twice_and_adds_flat_once`.
