# M1-06 Reproducibility Key and Run Configuration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Every run is identified by a key `(runSeed, genVersion, manifestHash, configHash, unlockHash)` that fixes everything that can change its outcome. The run configuration snapshot and unlock snapshot travel with the run state and its saves, and V22 is registered with its one M1 row live.

**Architecture:**
- **Snapshots:** `src/Core/Run/` holds `RunConfig` (the RS 11.1 configuration snapshot with the SV-12 reserved fields) and `UnlockSnapshot` (sorted unlock ids, empty in M1). Both implement `ICanonical`, and `configHash`/`unlockHash` are `Digest.Of` over them.
- **Versions:** `GenVersion` and `RulesVersion` are Core constants.
- **The key:** `RunKey` assembles the tuple, with `manifestHash` parsed from the embedded `ContentHashes.Manifest`.
- **State and saves:** `GameState` gains `Config` and `Unlocks`, and the key is computed from them. `Snapshot` moves to save version 3, with a header (`saveVersion`, `rulesVersion`, `genVersion`, the three key hashes, `runSeed`) plus the two snapshots. Load checks that the stored hashes match the stored snapshots, and `StateDigest` covers the new fields.

**Tech Stack:** C# 12 / .NET 8; M1-03 `CanonicalWriter`/`Digest`; M1-04 content hashes and validator registry; xUnit (reflection for the coverage test).

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-06](../../roadmap/m1-vertical-slice.md#m1-06-reproducibility-key-and-run-configuration). It implements:
- **SV 4.6** (lines 701–729 of `docs/superpowers/specs/2026-11-23-seeds-validation-design.md`): the key, `configHash`, `unlockHash`, `genVersion`, and the two guards.
- **SV 4.10:** the versioning rules.
- **SV 9.2 `SaveHeader`:** fields only.
- **RS 11.1:** the snapshot fields (`docs/superpowers/specs/2026-11-21-run-structure-themes-design.md`, 2933–2975).
- **D-08** (`2026-11-24-m1-scope-rulings-design.md`, 3.3): `route.floorCount` = 1 in `configHash`.
- **HL-14 / HL-22 and V22 (m)** (`2026-11-23-heirlooms-curses-design.md`, 9.1–9.2, lines 905, 927, 969).
- **MAIN 6.6.**

## Global Constraints

- SV 4.6: "The reproducibility key … `(runSeed, genVersion, manifestHash, configHash, unlockHash)`."
- `genVersion`: "an integer code constant, bumped whenever generator output changes for any key".
- `configHash` covers "RS 11.1's configuration snapshot plus `difficulty.tier`, `boons[]` and `nightmareTier` (SV-12; field reserved in M1, live in M3), plus `runModifierId` … sorted canonically". `difficulty.tier` and `nightmareTier` are distinct fields.
- V19t `ConfigHashCoversRunConfig`: "a reflection test that every field of the configuration-snapshot type is written by the `configHash` canonical serialiser, plus a test that changing each field changes `configHash`."
- V22 is "registered at M1 with only the (m) `RunModifierIdInConfigHash` field row live" (HL-14).
- RS 11.1: `route.floorCount` is 1 in M1. `route.twoDoorsEnabled` and `route.lastHollowChoiceEnabled` default to false, and "load rejects `lastHollowChoiceEnabled` true with `twoDoorsEnabled` false". `pacing.trimmedFloors[]` defaults to empty. `recruitSchedule.companionCap` is 3. Launcher-only values (`route.streakRedrawMax`, `curiosity.fallowK`, `curiosity.fallowFloors`) are not in the snapshot.
- Outside the key: `displayHash`, `genContentHash` (M2 comparability), `runId`, profile memory, and the UI.
- The determinism analyzer stays green (DUN001–DUN012); no floating point; sorted collections only.
- Out of scope:
  - **M1-27:** the GS0.2 snapshot stage, `run.save`/`runId`/`seq`, the replay header and its `rulesVersion`-mismatch error.
  - **M2:** comparability (`comparable`, `genVersionsUsed[]`, share codes, build tag).
  - **M1-28 / M1-30:** the `layout.*`, `terrain.*` and `cover.*` snapshot fields. They are added by those steps, and the reflection test forces them into `configHash` when they land.
  - **M3:** live values for the grants.
  - **Later:** Oaths.
  - **Not placed in any step yet:** V19s `CoreNoProfileRef`, which is not in this step's Delivers (see call 7).

## Plan-level calls (mine, flagged for review)

1. **The key lives with the run state.** `GameState` gains `RunConfig Config` and `UnlockSnapshot Unlocks`, and `GameState.Key` computes the `RunKey`. The rules will read the config, since D-08's floor-1 victory reads `route.floorCount`, and "the same key twice gives byte-identical state" then compares real state.
   - Cost: save version 3, a `StateDigest` with two more fields, and updated persistence tests.
   - Seeds and stream keys are unchanged.
2. **Grant field types and M1 canonical values** (SV gives neither). `difficultyTier: int` = 0 (≥ 0); `nightmareTier: int` = 0 (0–3); `boons: ImmutableArray<string>` = empty (content-id strings, written as a set, so they are sorted); `runModifierId: string?` = null (a content id when present).
3. **RS 11.1 fields included now** are only those with a stated M1 value:
   - `route.floorCount` = 1;
   - `route.twoDoorsEnabled` = false and `route.lastHollowChoiceEnabled` = false;
   - `route.doorCamps` = empty;
   - `pacing.trimmedFloors` = empty;
   - `recruitSchedule.companionCap` = 3.

   `RunConfig.M1` is that default.
4. **Version constants:** `GenVersion.Current = 1` and `RulesVersion.Current = 1` (integers). The save version stays `Snapshot.CurrentVersion`, which becomes 3.
5. **`unlockHash` in M1** is `Digest.Of` over the empty `UnlockSnapshot` (a record holding one empty id set).
6. **Load and build-identity mismatches:**
   - A loaded save's stored `configHash` and `unlockHash` must equal those recomputed from its stored snapshots (integrity), otherwise `SnapshotFormatException`.
   - The stored `genVersion`, `rulesVersion` and `manifestHash` are kept and exposed, but a mismatch with the build is **not** rejected in M1. Comparability is M2, and the replay `rulesVersion` rule is M1-27.
7. **V19s `CoreNoProfileRef`** is not built here: no Profile type exists yet to ban. It is recorded as an open item for the step that introduces the profile (M2-23), in `docs/roadmap/README.md` as **D-14**.
8. **V22 registration:** `BuildTier` gains V22 (tier 3). `content/registry/checks.json` gains only the live row `V22.RunModifierIdInConfigHash` (phase `t`, class `build`, wave M1) and `V19.ConfigHashCoversRunConfig` (phase `t`). The later V22 rows are registered by their owning steps, as other validators' rows are.
9. **`Digest.Parse(string hex64)`** is added (an M1-04 deferred minor), so the key can hold the embedded manifest hash as a `Digest`.

## Review Focus

1. **A new `RunConfig` property that the serialiser forgets.** The reflection test must fail. It compares `typeof(RunConfig)`'s declared public instance properties with `RunConfig.WrittenFields`. The implementer proves this once by adding a throwaway property, watching the test fail, then removing it, and records it in the report. Task 1.
2. **Load-time validation:** `lastHollowChoiceEnabled` true with `twoDoorsEnabled` false; `floorCount` 0; a negative `companionCap`; `nightmareTier` 4; a non-content-id boon; duplicate boons; a non-content-id `runModifierId`. Each is rejected with a named field. Task 1, and in saves Task 3.
3. **Hash integrity in saves:** a save whose `configHash` was hand-edited, or whose config was edited without the hash, is refused. A save with a different `genVersion` or `manifestHash` still loads (call 6). Task 3.
4. **Boon order:** `["b","a"]` and `["a","b"]` give the same `configHash`. Task 1.
5. **Seeds and stream keys unmoved:** `RunStreamsTests`, the decision goldens and the stream-handle vectors are unchanged. Every task's full-suite run shows this.

---

### Task 1: `RunConfig`, `UnlockSnapshot`, versions, `Digest.Parse`

**Files:**
- Create: `src/Core/Run/RunConfig.cs`, `src/Core/Run/UnlockSnapshot.cs`, `src/Core/Run/Versions.cs`
- Modify: `src/Core/Canonical/Digest.cs` (`Parse`)
- Test: `tests/Core.Tests/RunConfigTests.cs`, `tests/Core.Tests/DigestTests.cs`

**Interfaces (namespace `Dungeon.Core`):**
- `public static class GenVersion { public const int Current = 1; }` and `public static class RulesVersion { public const int Current = 1; }`. Each has an XML doc stating when to bump it (SV 4.10).
- `public sealed record RunConfig(int RouteFloorCount, bool RouteTwoDoorsEnabled, bool RouteLastHollowChoiceEnabled, ImmutableArray<int> RouteDoorCamps, ImmutableArray<int> PacingTrimmedFloors, int RecruitCompanionCap, int DifficultyTier, ImmutableArray<string> Boons, int NightmareTier, string? RunModifierId) : ICanonical`.
  - **Instances:** `static RunConfig M1` holds call 3's values and call 2's grant values.
  - **Validation:** `static RunConfig Validate(RunConfig c)` throws `ArgumentException` naming the field for:
    - `RouteFloorCount < 1` or a negative cap;
    - the RS 11.1 two-doors rule;
    - a tier < 0, or `NightmareTier` outside 0–3;
    - a boon or `RunModifierId` that is not a `ContentId`, or duplicate boons;
    - door camps or trimmed floors outside 1..`RouteFloorCount`, or duplicated.
  - **Canonical form:** `WriteCanonical` is one record with ordinals in the declared parameter order. `RouteDoorCamps` and `PacingTrimmedFloors` are written as sets keyed by value. `Boons` is a set by id. `RunModifierId` is an optional string.
  - **Hash:** `Digest ConfigHash => Digest.Of(this)`.
  - **Coverage list:** `internal static ImmutableArray<string> WrittenFields` lists the parameter names in ordinal order, so the reflection test can compare against it.
- `public sealed record UnlockSnapshot(ImmutableArray<string> Ids) : ICanonical`:
  - `static UnlockSnapshot None` holds the empty set.
  - The ids are content ids, written as a set by id.
  - `Digest UnlockHash => Digest.Of(this)`.
- `Digest.Parse(string hex)`: exactly 64 lowercase hex characters, otherwise `FormatException`. It round-trips `ToHex()`.

- [ ] **Step 1: Write the failing tests.**
  - `ConfigHashCoversRunConfig` (V19t, the done-when):
    - **(a) Every field is written.** Read the public instance properties of `RunConfig` that are declared on it (excluding `ConfigHash` and the compiler's `EqualityContract`), ordinally sorted. They must equal `RunConfig.WrittenFields`, also sorted, so a new property not added to the serialiser fails the test (Review Focus 1).
    - **(b) Changing a field changes the hash.** For each field, a `with` copy of `RunConfig.M1` changed to another valid value must give a `ConfigHash` different from `M1.ConfigHash`. Write one case per field in a table: `RouteFloorCount` 2; `RouteTwoDoorsEnabled` true; `RouteLastHollowChoiceEnabled` true together with two doors; `RouteDoorCamps` `[1]`; `PacingTrimmedFloors` `[1]`; `RecruitCompanionCap` 2; `DifficultyTier` 1; `Boons` `["boon.a"]`; `NightmareTier` 1; `RunModifierId` `"oath.a"`.
    - **(c) The table is complete.** The table's field names must equal `WrittenFields`.
  - `RunModifierIdInConfigHash` (V22t (m)): `M1 with { RunModifierId = "oath.a" }` and `with { RunModifierId = "oath.b" }` and `M1` give three different hashes.
  - **Review Focus 4:** boons `["boon.b","boon.a"]` hash the same as `["boon.a","boon.b"]`.
  - **Review Focus 2:** each `Validate` rejection, asserting the parameter name.
  - **Defaults:** `RunConfig.M1.RouteFloorCount == 1` (D-08).
  - **Unlocks:** `UnlockSnapshot.None.UnlockHash` equals `Digest.Of` of a writer record holding an empty set, and adding an id changes it.
  - **`DigestTests`:** `Digest.Parse(d.ToHex()) == d`; uppercase, 63-character and non-hex inputs throw.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "RunConfigTests|DigestTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** per the Interfaces block.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: PASS, with no warnings and no DUN diagnostics.
- [ ] **Step 5: Commit** `git commit -m "M1-06: RunConfig and UnlockSnapshot with configHash and unlockHash; version constants"`

### Task 2: `RunKey` and the V19/V22 rows

**Files:**
- Create: `src/Core/Run/RunKey.cs`
- Modify:
  - `src/Core/Validation/BuildTier.cs`: V22 → tier 3, and update the doc line.
  - `content/registry/checks.json`: rows `V19.ConfigHashCoversRunConfig` and `V22.RunModifierIdInConfigHash`, both owner as named (the lead; heirlooms and curses), phase `t`, class `build`, wave M1, ciTier pr, action `fail the build`.
  - Regenerate `ContentHashes.g.cs` (`dotnet run --project src/Sim -- content-hashes`).
- Test: `tests/Core.Tests/RunKeyTests.cs`

**Interfaces:**
- `public readonly record struct RunKey(ulong RunSeed, int GenVersion, Digest ManifestHash, Digest ConfigHash, Digest UnlockHash) : ICanonical`:
  - `static RunKey For(ulong runSeed, RunConfig config, UnlockSnapshot unlocks)` validates the seed (`RunSeed.Validate`) and the config, takes `GenVersion.Current` and `Digest.Parse(ContentHashes.Manifest)`, and computes the two hashes.
  - `WriteCanonical` writes the five fields in tuple order, each `Digest` as its four `UInt64` words.
  - `Digest Id => Digest.Of(this)` is the key's own digest, for display and logs.

- [ ] **Step 1: Write the failing tests.**
  - `For` gives the tuple in order, and `ManifestHash.ToHex() == ContentHashes.Manifest`.
  - Two `For` calls with the same inputs give equal keys and identical canonical bytes.
  - Changing each input (seed, a config field, an unlock id) changes exactly the matching field and the `Id`.
  - A seed of 2^60 throws.
  - A config breaking `Validate` throws.
  - `BuildTier.Of(new CheckId("V22.RunModifierIdInConfigHash")) == 3`.
  - `RepositoryContentTests` stays green: these are `t` rows, so no static check is needed.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "RunKeyTests|ValidationFrameworkTests"`. Expected: FAIL.
- [ ] **Step 3: Implement**, add the two rows, and regenerate the content hashes.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: PASS.
- [ ] **Step 5: Commit** `git commit -m "M1-06: RunKey (runSeed, genVersion, manifestHash, configHash, unlockHash); register V19t and V22t (m) rows"`

### Task 3: The key in the run state and save version 3

**Files:**
- Modify:
  - `src/Core/State/GameState.cs`: add `RunConfig Config` and `UnlockSnapshot Unlocks` as positional members after `RunSeed`; add `RunKey Key => RunKey.For(RunSeed, Config, Unlocks)`; `Create(map, units, seed, RunConfig? config = null, UnlockSnapshot? unlocks = null)` defaults to `RunConfig.M1` and `UnlockSnapshot.None` and validates.
  - `src/Core/State/StateDigest.cs`: the root record grows from 7 to 9 fields, with `Config` and `Unlocks` appended as fields 7 and 8.
  - `src/Core/Persistence/Snapshot.cs`: version 3.
  - `src/Core/Persistence/CommandLog.cs`: it shares the version; the log gains nothing else, since the replay header is M1-27.
  - Every caller of the `GameState` constructor.
  - `tests/Core.Tests/PersistenceTests.cs`, `StateDigestTests.cs`.
  - `README.md` (layout: `src/Core/Run/`), `docs/roadmap/README.md` (D-14, call 7).
- Test: `tests/Core.Tests/RunDeterminismTests.cs`

**Interfaces:**
- Snapshot v3 JSON, in property order:
  1. `version` (= `saveVersion`, 3);
  2. `rulesVersion`;
  3. `genVersion`;
  4. `manifestHash`, `configHash`, `unlockHash` (64-hex strings);
  5. `runSeed`;
  6. `config` (an object with camelCase field names `routeFloorCount` … `runModifierId`; arrays sorted on write);
  7. `unlocks` (`{"ids": [...]}`);
  8. then the existing fields.
- **Load rules:** the config and unlocks are validated (`RunConfig.Validate`), and the stored `configHash` and `unlockHash` must equal those recomputed from them, otherwise `SnapshotFormatException("Invalid save: configHash does not match the stored config.")`. The stored `genVersion`, `rulesVersion` and `manifestHash` are parsed and kept, but not compared with the build (call 6).
- `public sealed record SaveHeader(int SaveVersion, int RulesVersion, int GenVersion, Digest ManifestHash, Digest ConfigHash, Digest UnlockHash, ulong RunSeed)`, with `static SaveHeader Read(string json)` returning it without loading state. It is the M1 subset of the SV 9.2 `SaveHeader`; the other fields arrive with M1-27 and M2.

- [ ] **Step 1: Write the failing tests.**
  - **`RunDeterminismTests.The_same_key_twice_gives_byte_identical_state`** (the done-when):
    - Two runs are built from the same seed, `RunConfig.M1` and `UnlockSnapshot.None` via `GameState.Create` on `Fixtures`' ring map, and the same command list is applied (a move, then a shrine step and its answer on the shrine fixture).
    - The two runs have equal `Key`s, byte-equal `StateDigest.Bytes`, and identical `Snapshot.Save` text.
    - A run whose config differs only in `RunModifierId` has a different `Key` and different `StateDigest.Bytes`.
  - **`PersistenceTests`:**
    - Save, load and save again is byte-identical, including config and unlocks.
    - `SaveHeader.Read` returns version 3, `RulesVersion.Current`, `GenVersion.Current` and the three hashes.
    - **Review Focus 3:**
      - an edited `configHash` is refused;
      - an edited `config.routeFloorCount` (hash unchanged) is refused;
      - an edited `genVersion` (99) still loads, with `SaveHeader.Read(...).GenVersion == 99`;
      - an edited `manifestHash` still loads.
    - **Review Focus 2 in saves:** `lastHollowChoiceEnabled` true with two doors off is refused.
    - Existing version tests move from 2 to 3; a version-2 save is refused with `SnapshotVersionException { Found = 2 }`.
  - **`StateDigestTests.Every_field_reaches_the_digest`** gains cases for `Config` (e.g. `DifficultyTier` 1) and `Unlocks` (one id).
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "RunDeterminismTests|PersistenceTests|StateDigestTests"`. Expected: FAIL.
- [ ] **Step 3: Implement**, update the callers and the docs, and add D-14.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`, `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` and the smoke build. Expected: all pass; `RunStreamsTests` and the decision goldens are unchanged (Review Focus 5).
- [ ] **Step 5: Commit** `git commit -m "M1-06: run config and unlocks in GameState; save version 3 with the key header"`
- [ ] **Step 6 (controller):** Godot hand-check (Play, shrine step, boon pick: unchanged behaviour), update issue #9, push, open the PR.
