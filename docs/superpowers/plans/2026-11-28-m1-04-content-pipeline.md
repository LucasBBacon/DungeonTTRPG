# M1-04 Content Pipeline and Validator Framework Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Game data loads from `content/` JSON with stable ids and fails loudly (file and field named) when wrong. Every check in the designs has a registered place to live: qualified ids, phases, failure classes, build tiers and a read-set cache. The naming checks V7.NameRegistry and V24 r1, r2 and r6 run, and the three content hashes are embedded in the build.

**Architecture:**
- **Loader:** `src/Core/Content/` holds a strict `System.Text.Json` loader. Core takes text and a relative file name, never paths, as `Snapshot.Load` does. It works over pluggable `ContentKind`s and produces an immutable `ContentSet`, then resolves cross-record references after every file has loaded.
- **Validator framework:** `src/Core/Validation/` holds the registry rows (`CheckRow`, `ReportRow`, `ArmRow`, loaded from `content/registry/`), code-side `IStaticCheck`s, a tier-ordered runner and a cache keyed by the digest of each check's read set.
- **Naming data:** `src/Sim` gains two tool commands. `name-list` exports the naming-pass markdown appendix to `content/naming/*.json`. `content-hashes` writes `src/Core/Content/ContentHashes.g.cs`.
- **Drift tests:** tests in `Core.Tests` fail if either generated artefact drifts from its source.

**Tech Stack:** C# 12 / .NET 8, System.Text.Json (reflection DTOs, as in `Snapshot`), the M1-03 `CanonicalWriter`/`Digest`, xUnit. Must stay green under the M1-01 determinism analyzer (DUN001–DUN011, generated code included).

**Spec:** [`docs/roadmap/m1-vertical-slice.md` — M1-04](../../roadmap/m1-vertical-slice.md#m1-04-content-pipeline-and-validator-framework), implementing:
- MAIN 6.5
- SV (`docs/superpowers/specs/2026-11-23-seeds-validation-design.md`): DS13 (line 689), 4.6 hashes (lines 704–707), 5.1 phases and failure classes (919–938), 5.2 (V7, V19, V24 rows), 5.3 tiers and caching (1065–1080), 9.1 registries (1700–1710), SV-19 (partial), SV-20 (model)
- NP (`docs/superpowers/specs/2026-11-24-naming-pass-design.md`): 9.1, 9.1.1, the V24 rows at 954–966, staging "M1 records" (1013)
- the name list `docs/superpowers/specs/2026-11-24-naming-pass-name-list.md`

## Owner rulings for this step (2026-11-28, client)

1. **D-12, split rule.** Content record ids are ASCII `[a-z0-9_.:-]` (K-rule 3 as written). Stream and domain labels (code constants passed to `LabelId`) may also use capitals, `[A-Za-z0-9_.:-]`, so every SV 4.1 vector stays valid. Task 1 records this in SV 4.1 K-rule 3 and strikes D-12 in `docs/roadmap/README.md`.
2. **Name list exported to JSON.** A deterministic converter turns the markdown appendix into `content/naming/name_list.json`, and derives `content/naming/retired_names.json`, both committed. A test fails on drift.
3. **Greyed-reason ids and strings come from their owning steps.** M1-04 ships the greyed-reason registry (empty) and the check, proven on fixtures. No player-facing copy is written here.
4. **Content hashes over canonical bytes, embedded as `ContentHashes.g.cs`.** Each record is hashed through `CanonicalWriter`, which gives sorted ids, fixed-width integers, ASCII rule strings and UTF-8 display text, all independent of JSON layout. A test fails if `content/` changed without regenerating. Task 5 adds a note to SV 4.6 recording this.

## Global Constraints

- MAIN 6.5: "Content is JSON, loaded and validated at startup. A bad reference or a missing field fails loudly with the file and field named. Every content item has a stable string ID."
- DS13: "Content numbers read by generation or rules are integers or rationals. The Core loader (`System.Text.Json` into integer types, never Godot JSON) rejects non-integer values in those fields. A rational's canonical form is a reduced `p/q` with q > 0."
- SV 4.6: every rules-relevant string field is ASCII and the loader rejects non-ASCII there; fields the schema tags `display` are excluded from `manifestHash` and hashed as `displayHash`; `genContentHash` is "the subset of the manifest that generation reads" (its use is M2); the hashes are "computed in the build pipeline and embedded in the build, never at run time".
- SV 5.1: qualified ids `Vn.CheckName`, one owner each; phases `s` build, `g` generation, `r` runtime assertion, `t` test; "`s` and `t` parts fail the build; `g` and `r` parts act by their registered class"; failure classes `repair`, `stageRetry(stage)`, `floorRetry` (needs `proofRef`), `waivable(fallback)`, `neverWaived`, `defect`, `runtimeAssert`, `report`.
- SV 9.1 rows, verbatim:
  - `CheckRow {id: "Vn.Name", owner, phase: s|g|r|t, point?, deps[], uses[], class, action, rerunSpan?, proofRef?, wave, ciTier, budgetMs?}`
  - `ReportRow {id, family?, keys[], owner, definition, unit, instrument: S|W|L|V|G|H, arms[], class, threshold, thresholdStatus: agreed|settled|lead|illustr, sided: one|two, z, stat, N, wave, ciTier, leverOrderRef?}`
  - `ArmRow {id: "Axis.Value", owner, configDelta}`
- SV 5.3 tiers: 0 content load and schema, V19s; 1 content suites; 2 V7s, V1s; 3 V8s–V11s, V17s, V18s, V20s, V21s; 4 V12s; 5 V13s–V16s; 6 V2–V6; `t` parts after their `s` parts. "Results are cached by content hash keyed by each check's declared read set."
- DS4/DS11: no unordered enumeration (sorted `ImmutableArray`s; dictionaries for lookup only), ordinal comparisons, no floating point, no clocks, no parallelism (the tiers run sequentially in Core), no signed `/`/`%` outside `IntMath`.
- Out of scope: `streams.json` and handle source generators (M1-05); the reproducibility key and `configHash` (M1-06); V19s completeness over paths, stages, checks and reports (M1-27); per-owner content schemas (M1-09 onward); the NP data files other than the two derived here (M2); V24 rows r3–r9 and r3b (M2); a persistent proof cache across runs (later, when the cold budget needs it).

## Plan-level calls (mine, flagged for review)

1. **ClassDeclared for every row.** The roadmap says "every registered check declares one failure class"; SV gives classes to `g`/`r` rows only. Every `CheckRow` therefore carries `class`: `g`/`r` rows take one of the eight SV classes, and `s`/`t` rows take the ninth value `build` ("fails the build", SV 5.1). `V19.ClassDeclared` enforces that pairing and that `floorRetry` has a `proofRef`.
2. **Class syntax as SV writes it:** `"stageRetry(traps)"`, `"waivable(ArenaStampFallback)"`, `"floorRetry"`. These parse to a `FailureClass` (kind + optional argument). `stageRetry` and `waivable` need an argument; the others take none.
3. **Registry row ids follow their own grammars, not the content-id rule:**
   - checks: `^V[1-9][0-9]*\.[A-Za-z][A-Za-z0-9]*$` or `^content(\.[A-Za-z0-9]+)+$`
   - arms: `^[A-Za-z][A-Za-z0-9]*\.[A-Za-z0-9]+$`
   - reports: `^[A-Za-z][A-Za-z0-9.]*$`
4. **Read sets are declared in code** (`IStaticCheck.Reads`: content file names), since `CheckRow` has no reads field. The cache is in memory, per test run.
5. **Tier of a check** comes from a code table over its validator (the SV 5.3 table), with V24 placed at tier 3, after V7 at tier 2. `content.*` load rows are tier 0 and other `content.*` suites tier 1.
6. **`genContentHash`** is computed now over the kinds that declare `ReadByGeneration` (none in M1-04, so it is the digest of the empty manifest) and used from M2.
7. **Retired names** = entries whose outcome starts with `retired`, `deleted` or `dropped`, plus the names of entries whose outcome starts with `renamed →` (a whole rename; `renamed in part` is excluded because the part cannot be told without NP 5). Grouped names (`A / B / C`, and comma lists after a colon) split into one retired record each. V24 r2 matches on (name, category) by ordinal equality.
8. **Display text** (name-list names, strings-table text) may be any UTF-8 but must be NFC. The exporter and the strings drift test check NFC outside Core; Core hashes the UTF-8 bytes as given. `CanonicalWriter`/`CanonicalReader` gain `Text`/`ReadText` (UTF-8, length-prefixed) for display fields only.

## Review Focus

1. **Error location for every failure path:** each of type mismatch (`"x"` where an int is expected), fractional number, integer overflow, non-ASCII rule string, unknown property, duplicate property, missing field, bad reference and duplicate id must give a `ContentLoadException` whose `File` and `Field` (a JSON path such as `$.checks[1].phase`) are right. Test matrix in Task 2.
2. **Order independence:** shuffled file order and CRLF line endings must give the same `ContentSet` and the same three hashes. Tests in Tasks 2 and 5.
3. **Exporter on the real appendix:** all 2,893 entries parse, including the one line with trailing text after the bold outcome and the 14 non-ASCII names; section headers attribute each entry to its document; the output is byte-stable on both OSs (LF, no BOM). Tests in Task 4.
4. **Rationals:** `"6/8"`, `"3/0"`, `"-3/-4"`, `"3/-4"`, `"1/1"` vs `1`, `" 3/4"`, `"3/4/5"`, and overflow at `long` bounds. Only reduced `p/q` with q > 0, or a plain integer, is accepted. Tests in Task 1.
5. **Cache invalidation:** editing a file in a check's read set reruns that check; editing an unrelated file does not. Test in Task 3.

---

### Task 1: Ids, rationals, load errors, and the D-12 ruling

**Files:**
- Create: `src/Core/Content/ContentId.cs`, `src/Core/Content/Rational.cs`, `src/Core/Content/ContentLoadException.cs`
- Test: `tests/Core.Tests/ContentIdTests.cs`, `tests/Core.Tests/RationalTests.cs`
- Modify: `docs/superpowers/specs/2026-11-23-seeds-validation-design.md` (K-rule 3 at line ~463: append "Content record ids are `[a-z0-9_.:-]`; stream and domain labels may also use capitals, `[A-Za-z0-9_.:-]` (agreed 2026-11-28, roadmap D-12).") and `docs/roadmap/README.md` (strike the D-12 row like D-01, pointing to SV 4.1 K-rule 3)

**Interfaces (namespace `Dungeon.Core.Content`):**
- `public readonly record struct ContentId(string Value)` with `static bool IsValid(string s)` (non-empty, every char in `[a-z0-9_.:-]`) and `static ContentId Parse(string s)` (`ArgumentException` naming the bad char otherwise). `ToString() => Value`.
- `public readonly record struct Rational(long P, long Q)`: canonical by construction. `static Rational Of(long p, long q)` throws if `q <= 0` or `gcd(|p|, q) != 1`. `static bool TryParse(string s, out Rational r)` accepts exactly `-?[0-9]+` (q = 1) or `-?[0-9]+/[0-9]+`, reduced, with q > 0. `ToString()` gives `"p/q"`, or `"p"` when q = 1. gcd uses unsigned arithmetic or `IntMath` (DUN010).
- `public sealed class ContentLoadException : Exception` with `string File`, `string Field`, and message `"Content error in {File} at {Field}: {problem}"`.

- [ ] **Step 1: Write the failing tests.**
  - `ContentIdTests`: valid `"boss.old_ragged"`, `"a:b-c_1"`; invalid `""`, `"Boss"`, `"a b"`, `"é"`, `"a/b"`. Parse's message names the character.
  - `RationalTests` (Review Focus 4): `TryParse("3/4")` gives (3,4); `"-3/4"` gives (-3,4); `"5"` gives (5,1); `"0"` gives (0,1). Rejected: `"6/8"`, `"3/0"`, `"-3/-4"`, `"3/-4"`, `"1/1"`, `" 3/4"`, `"3/4 "`, `"3/4/5"`, `""`, `"+3/4"`, `"0/5"` (not reduced: canonical zero is `"0"`), and `"9223372036854775808"` (overflow). `ToString()` round-trips every accepted input. `Of(2, 4)` throws.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "ContentIdTests|RationalTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** the three types. Make the two doc edits.
- [ ] **Step 4: Run** the filter, then `dotnet test Dungeon.sln`. Expected: PASS, with no warnings.
- [ ] **Step 5: Commit** `git commit -m "M1-04: content ids, canonical rationals and load errors; record D-12"`

### Task 2: Strict loader, `ContentSet`, and the registry kinds

**Files:**
- Create: `src/Core/Content/ContentFile.cs`, `ContentKind.cs`, `ContentSet.cs`, `ContentLoader.cs`, `ContentJson.cs`; `src/Core/Validation/CheckRow.cs`, `ReportRow.cs`, `ArmRow.cs`, `RegistryKinds.cs`; `src/Core/Persistence/JsonStrict.cs` (shared duplicate-property walk)
- Create content: `content/registry/checks.json` (`{"checks": []}` for now), `content/registry/reports.json` (`{"reports": []}`), `content/registry/arms.json` (`{"arms": []}`)
- Modify: `src/Core/Persistence/Snapshot.cs` (use `JsonStrict`), `content/README.md` (the folder layout and the loader rules)
- Test: `tests/Core.Tests/ContentLoaderTests.cs`

**Interfaces:**
- `public sealed record ContentFile(string Name, string Text)`. `Name` is relative to `content/`, uses `/`, and is ASCII.
- `public abstract class ContentKind`:
  - `string FileName` (exact relative name, e.g. `"registry/checks.json"`)
  - `bool ReadByGeneration` (default false)
  - `abstract ImmutableArray<ContentRecord> Read(ContentFile file)`, which throws `ContentLoadException`
  - `virtual void Resolve(ContentSet set)`, for the reference checks, which throws `ContentLoadException` naming the referring file and field
- `public abstract record ContentRecord(string File, string Id) : ICanonical`. `Id` is the record's id string (ASCII); its grammar is checked by its kind. `WriteCanonical` is abstract and writes the rules fields, with `Id` as field 0. `ContentSet.DigestOf` needs it from this task on. Task 5 adds the optional `IDisplayCanonical` for display fields; a record with only display fields writes just its `Id`.
- `public sealed class ContentSet` provides:
  - `ImmutableArray<ContentFile> Files` (ordinal by name)
  - `ImmutableArray<T> All<T>() where T : ContentRecord` (ordinal by id)
  - `T? Find<T>(string id)`
  - `Digest DigestOf(IEnumerable<string> fileNames)` (SHA-256 over the canonical bytes of those files' records, in ordinal file order; used by the cache)
- `public static class ContentLoader`:
  - `ContentSet Load(IEnumerable<ContentFile> files)` uses `ContentKinds.All`; `Load(IEnumerable<ContentFile> files, IReadOnlyList<ContentKind> kinds)` is for tests.
  - Files are sorted ordinally, and each file is matched to exactly one kind by `FileName`. An unknown file or a repeated file name throws. A kind whose file is absent has no records: every kind is optional, so tests can supply only the files they need.
  - Duplicate ids within a kind throw, naming the second record's file and `$.<array>[i].id`.
  - `Resolve` runs after all files load.
- `internal static class ContentJson` wraps the `Snapshot` strictness for content:
  - `JsonUnmappedMemberHandling.Disallow`, the `JsonStrict` duplicate-property walk and comments disallowed.
  - It maps `JsonException.Path` to `Field` and fills `File`.
  - Helpers: `Need`, `RuleString(file, field, s)` (ASCII), `Id(file, field, s)` (`ContentId`), `RationalField`, `ParseEnum<T>` (exact name).
- `CheckRow`, `ReportRow`, `ArmRow` are records with exactly the SV 9.1 fields:
  - `CheckRow` `Class` is a `FailureClass` (Task 3 defines it; in this task keep it as the raw string and parse it in Task 3).
  - `ReportRow` `threshold` and `z` are `Rational`; `N` is `int`.
  - `ArmRow.ConfigDelta` is an ASCII string (its schema arrives with M1-06).
  - References resolved by `Resolve`: `CheckRow.deps[]`, `uses[]` and `proofRef` name check ids; `ReportRow.arms[]` names arm ids.
- `public static class ContentKinds` with `ImmutableArray<ContentKind> All` (the three registry kinds now; Task 4 adds the naming kinds).

- [ ] **Step 1: Write the failing tests** in `ContentLoaderTests`. Build inputs as `ContentFile` lists in code, with a helper `Check(...)` emitting one check-row JSON object.
  - **The three done-when fixtures**, each asserting `ContentLoadException.File == "registry/checks.json"` and the exact `Field`:
    - a check missing `"phase"` → `$.checks[0].phase`;
    - a check whose `deps` names `"V99.Missing"` → `$.checks[0].deps[0]`;
    - two checks with id `"V19.ClassDeclared"` → `$.checks[1].id`.
  - **Review Focus 1 matrix:**
    - `"budgetMs": "5"` (type mismatch) → `$.checks[0].budgetMs`
    - `"budgetMs": 1.5` (fraction) → same field
    - `"budgetMs": 99999999999` (overflow of int) → same field
    - `"owner": "Lé"` (non-ASCII) → `$.checks[0].owner`
    - an extra `"colour"` property → `$.checks[0].colour`
    - a duplicated `"owner"` → `$.checks[0].owner`
    - a report whose `arms` names a missing arm → `registry/reports.json`, `$.reports[0].arms[0]`
    - a `"threshold": "6/8"` → `$.reports[0].threshold`
  - **File-level errors:** an unknown file `"registry/nope.json"`, and the same file name given twice, each throw with `Field == "$"`. A load without `registry/arms.json` succeeds with no arms.
  - **Review Focus 2:** the same three files in reversed order, and with CRLF line endings, give `All<CheckRow>()` with equal ids in the same order.
  - **Happy path:** a valid check with `deps` naming another valid check loads, and `Find<CheckRow>("V19.ClassDeclared")` returns it.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter ContentLoaderTests`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** per the Interfaces block.
  - Extract `RejectDuplicateProperties` from `Snapshot.cs` into `JsonStrict.FirstDuplicate(JsonElement) -> string?` (the JSON path of the first repeat). `Snapshot` keeps its behaviour and messages.
  - Create the three empty registry files and update `content/README.md`.
- [ ] **Step 4: Run** the filter, then `dotnet test Dungeon.sln` and `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`. Expected: all pass, including the persistence tests; no warnings or DUN diagnostics.
- [ ] **Step 5: Commit** `git commit -m "M1-04: strict content loader with file and field errors; check, report and arm registries"`

### Task 3: Validator framework, `V19.ClassDeclared`, and validation under `dotnet test`

**Files:**
- Create: `src/Core/Validation/Phase.cs` (enum `S, G, R, T`, read from `"s"`/`"g"`/`"r"`/`"t"`), `FailureClass.cs`, `CheckId.cs`, `IStaticCheck.cs`, `CheckResult.cs`, `BuildTier.cs`, `ValidationRunner.cs`, `CheckCache.cs`, `Checks/ClassDeclaredCheck.cs`, `Checks/ContentLoadCheck.cs`, `StaticChecks.cs`
- Modify: `content/registry/checks.json` (real rows for every registered check, below), `CheckRow` (parsed `FailureClass`, `Phase`)
- Test: `tests/Core.Tests/ValidationFrameworkTests.cs`, `tests/Core.Tests/RepositoryContentTests.cs`

**Interfaces:**
- `public readonly record struct FailureClass(FailureKind Kind, string? Argument)`:
  - `enum FailureKind { Repair, StageRetry, FloorRetry, Waivable, NeverWaived, Defect, RuntimeAssert, Report, Build }`, written `repair`, `stageRetry`, `floorRetry`, `waivable`, `neverWaived`, `defect`, `runtimeAssert`, `report`, `build`.
  - `static bool TryParse(string s, out FailureClass c)`: an argument is ASCII `[A-Za-z0-9_.:-]+` in `(...)`; `StageRetry`/`Waivable` require one; all other kinds forbid one.
- `public readonly record struct CheckId(string Value)` with `static bool IsValid(string)` (plan call 3 grammar) and `string Validator` (`"V7"`, `"V24"`, or `"content"`).
- `public interface IStaticCheck`: `CheckId Id { get; }`; `ImmutableArray<string> Reads { get; }` (content file names); `ImmutableArray<CheckFailure> Run(ContentSet content)`.
- `public sealed record CheckFailure(CheckId Check, string File, string Field, string Problem)`.
- `public static class BuildTier`: `int Of(CheckId id)`. Values: `content` load row → 0; other `content.*` → 1; V19 → 0; V7, V1 → 2; V24 → 3; V8, V9, V10, V11, V17, V18, V20, V21 → 3; V12 → 4; V13, V14, V15, V16 → 5; V2, V3, V4, V5, V6 → 6. Any other validator throws `ArgumentException`.
- `public sealed class CheckCache`: `bool TryGet(CheckId id, Digest readSet, out ImmutableArray<CheckFailure> failures)`; `void Put(...)`; `int Hits`, `int Runs` (for tests).
- `public static class ValidationRunner`:
  - `ValidationReport Run(ContentSet content, IReadOnlyList<IStaticCheck> checks, CheckCache cache)`.
  - Every check must have a `CheckRow` whose phase is `s` (else `InvalidOperationException` naming the id). Checks run in ascending (tier, ordinal id) order. A check is skipped when the cache holds a result for `content.DigestOf(check.Reads)`.
  - `ValidationReport` has `ImmutableArray<CheckFailure> Failures` (ordinal by check, file, field) and `bool Passed`.
- `ClassDeclaredCheck` (`V19.ClassDeclared`, reads `registry/checks.json`) fails a row when:
  - its class fails `FailureClass.TryParse`;
  - an `s`/`t` row has a class other than `build`, or a `g`/`r` row has `build`;
  - a `floorRetry` row has no `proofRef`.
  - The field is `$.checks[i].class` (or `.proofRef`).
- `ContentLoadCheck` (`content.MAIN65.Load`, reads nothing) always passes: reaching the runner means the load succeeded. It exists so the load has a registered row.
- `StaticChecks.All`: the registered checks (Task 4 adds the naming ones).
- `content/registry/checks.json` rows, all `"owner"`, `"wave": "M1"`, `"ciTier": "pr"`, `"deps": []`, `"uses": []`, `"action": "fail the build"`:
  - `content.MAIN65.Load` (owner `MAIN`, phase `s`, class `build`)
  - `V19.ClassDeclared` (owner `the lead`, phase `s`, class `build`)
  - Task 4 adds `V7.NameRegistry`, `V24.R1ExactEquality`, `V24.R2RetiredReused`, `V24.R6InternalByKey` and `V24.GreyedReasonKeys`.

- [ ] **Step 1: Write the failing tests.**
  - `ValidationFrameworkTests`:
    - `FailureClass.TryParse` accepts `"repair"`, `"stageRetry(traps)"`, `"waivable(ArenaStampFallback)"`, `"build"`, and rejects `"stageRetry"`, `"repair(x)"`, `"waivable()"`, `"Repair"`, `"stageRetry(a b)"`.
    - `ClassDeclared` fails for:
      - an `s` row with class `defect`;
      - a `g` row with `build`;
      - a `floorRetry` row without `proofRef` (field `$.checks[0].proofRef`);
      - an unparseable class.
    - `ClassDeclared` passes a valid mixed set.
    - The runner orders by tier: a V24 check registered before a V19 check runs after it (record the order in a test check).
    - The runner throws for a check with no row, and for a row with phase `g`.
    - Cache (Review Focus 5): run twice on the same content; the second run has `Runs` unchanged and `Hits` +1. Change a file in the check's read set → rerun. Change only `registry/arms.json` for a check reading `registry/checks.json` → no rerun.
    - `BuildTier.Of(new CheckId("V99.X"))` throws.
  - `RepositoryContentTests.Repository_content_loads_and_passes_validation` (done-when "content validation runs as part of `dotnet test`"):
    - It reads every `*.json` under `RepoRoot.Find()/content` (relative names, `/` separators, ordinal order), loads them and runs `StaticChecks.All`.
    - It asserts `Passed`, printing every failure as `file field: problem`.
    - It also asserts that every check in `StaticChecks.All` has a row, and every row with phase `s` has a check. This is the code-versus-file comparison for checks.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "ValidationFrameworkTests|RepositoryContentTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** per the Interfaces block, and fill `checks.json` with the two rows.
- [ ] **Step 4: Run** the filter, then `dotnet test Dungeon.sln`. Expected: PASS.
- [ ] **Step 5: Commit** `git commit -m "M1-04: validator framework (phases, failure classes, tiers, read-set cache) and V19.ClassDeclared"`

### Task 4: Name-list export, `V7.NameRegistry`, V24 r1/r2/r6, and greyed-reason keys

**Files:**
- Create in Sim: `src/Sim/Tools/NameListExporter.cs`, plus command dispatch in `src/Sim/Program.cs` (`dotnet run --project src/Sim -- name-list` reads the appendix and writes the two JSON files)
- Generated and committed: `content/naming/name_list.json`, `content/naming/retired_names.json`
- Create content: `content/naming/greyed_reasons.json` (`{"reasons": []}`), `content/strings/en.json` (`{"strings": []}`)
- Create in Core: `src/Core/Content/NamingKinds.cs` (four kinds, below), `src/Core/Validation/NamedRecord.cs`, `INamedRecordSource.cs`, `Checks/NameRegistry.cs`, `Checks/V24Checks.cs`
- Modify: `tests/Core.Tests/Core.Tests.csproj` (ProjectReference to `src/Sim/Sim.csproj`), `ContentKinds.All`, `StaticChecks.All`, `content/registry/checks.json`
- Test: `tests/Core.Tests/NameListExporterTests.cs`, `tests/Core.Tests/NamingChecksTests.cs`

**Interfaces:**
- **`NameListExporter` (namespace `Dungeon.Sim.Tools`):**
  - **Signatures:** `public static ImmutableArray<NameListEntry> Parse(string markdown)`; `public static string ToNameListJson(ImmutableArray<NameListEntry>)`; `public static string ToRetiredNamesJson(ImmutableArray<NameListEntry>)`.
  - **Entry type:** `public sealed record NameListEntry(string Doc, string Name, string Category, string Status, string Outcome, string OutcomeKind, string? Note)`.
  - **Section headers:** a header `#### B.<n> <doc> (<count>)` sets `Doc`. The count must equal the number of entries in that section, otherwise `FormatException`.
  - **Entry lines:** an entry line matches `^- (.+) · (.+?) · (.+?) · \*\*(.+?)\*\*(.*)$`. Group 5, trimmed, is `Note`, or null when empty.
  - **`OutcomeKind`:** the first word of `Outcome`, lowercased with ASCII only and leading punctuation stripped, mapped to one of `final`, `renamed`, `internal`, `retired`, `held`, `deleted`, `dropped`, `never`, or `other`.
  - **Rejected input:** any other line starting with `- ` throws `FormatException` naming the line number. All names must be NFC.
  - **JSON output:**
    - Shape: `{"entries": [...]}` in file order, `{"retired": [...]}` ordinal by (name, category).
    - Formatting: two-space indent, LF, no BOM, a trailing newline, and non-ASCII written as UTF-8 (`JavaScriptEncoder.UnsafeRelaxedJsonEscaping` is acceptable since this is data, not HTML).
  - **Retired records:** `{name, category, doc, outcome}`, derived per plan call 7.
- **Naming kinds (Core):**
  - `naming/name_list.json`: records with id `"<doc>:<index>"` (index 0-based within the doc). Every field is display-tagged.
  - `naming/retired_names.json`: id `"r<index>"`, a four-digit zero-padded index in the file's (name, category) order. Names can be non-ASCII (4 are), so they cannot be ids. Every field is display-tagged.
  - `naming/greyed_reasons.json`: records `{id: ContentId, doc: ASCII}`; rules fields.
  - `strings/en.json`: records `{key, text, field: name|label|line|notice}`. `key` is ASCII `<doc>.<registry>.<id>` and a rules field; `text` is display (NFC UTF-8); `field` is a rules field.
  - The name-list and retired ids may contain any ASCII the source has, so these two kinds use their own id check (non-empty, no control characters), not `ContentId`.
- **`NamedRecord` and sources:**
  - `public sealed record NamedRecord(string File, string Field, string DisplayName, string Category, string? MeaningId, bool Internal, string? DisplayKey)`
  - `public interface INamedRecordSource { ImmutableArray<NamedRecord> NamedRecords(ContentSet set); }`. Kinds whose records reach the screen implement it. In M1-04 no repository kind does; tests register a fixture kind.
- **`NameRegistry` (`V7.NameRegistry`, tier 2):** it reads every `INamedRecordSource` plus the two naming files, and fails when a named record's `DisplayName` is empty or contains a control character, or its `Category` is not one of the categories present in `name_list.json`. NFC is not checked in Core, which cannot normalise (plan call 8): the exporter rejects non-NFC names, and `NamingChecksTests.Strings_table_text_is_nfc` checks `strings/en.json` with `string.IsNormalized()` in the test project.
- **V24 checks (tier 3), each a separate `IStaticCheck`:**
  - `V24.R1ExactEquality`: two named records with ordinal-equal `DisplayName` and not the same non-null `MeaningId` fail, at the second record's file and field.
  - `V24.R2RetiredReused`: a named record whose (`DisplayName`, `Category`) ordinally equals a retired record fails.
  - `V24.R6InternalByKey`: fails when a record with `Internal = true` has a `DisplayKey`, when a non-internal record lacks one, or when a key is missing from `strings/en.json`.
  - `V24.GreyedReasonKeys`: every `greyed_reasons.json` record needs the key `<doc>.greyed.<id>` in `strings/en.json`.
- **`checks.json`:** add the five rows (owner `RS` for V7 and `the lead` for V24; phase `s`; class `build`; `uses` for the V24 rows `["V7.NameRegistry"]`).

- [ ] **Step 1: Write the failing tests.**
  - **`NameListExporterTests`** (Review Focus 3):
    - `Parse` of the real appendix (read via `RepoRoot.Find()`) gives 2,893 entries and 34 distinct `Doc`s. The first entry is (`dungeon-rpg`, `run`, `system term`, `unclear`, `final as written`, `final`).
    - The `Held Ground` entry has `Note` starting `(first Planted`.
    - `OutcomeKind` counts are exactly: final 2510, renamed 327, internal 41, retired 6, held 3, never 2, dropped 1, deleted 1, other 2. The two `other` outcomes begin `Sprawled final;` and `ruled:`. (`OutcomeKind` is the leading run of ASCII letters of `Outcome`, lowercased, if it is one of the eight kinds, else `other`.)
    - The retired file holds one record per distinct (name, category) pair. Those come from the 311 entries plan call 7 selects, and duplicates are dropped, keeping the first.
    - `Underpillow` gives a retired record (`Underpillow`, `heirloom`), and `Embercleaver of ?` (renamed →) gives one.
    - A section whose count disagrees throws.
    - An unparseable `- ` line throws naming its line number.
  - **`NameListExporterTests.Committed_files_match_the_appendix`:** regenerate both JSON strings from the appendix and assert byte equality with `content/naming/name_list.json` and `retired_names.json`, normalising nothing. The failure message is "run `dotnet run --project src/Sim -- name-list`".
  - **`NamingChecksTests`** use a fixture kind `test/named.json` implementing `INamedRecordSource` with records `{id, name, category, meaningId?, internal, key?}`:
    - r1 fails for two records named `"Old Ragged"` with different `meaningId`s and passes when both carry `meaningId: "boss.old_ragged"`.
    - r2 fails for a record named `"Underpillow"` with category `"heirloom"` and passes for the same name in category `"place"`.
    - r6 fails when an internal record has a key, when a non-internal record has no key, and when a key is absent from `strings/en.json`; it passes when all three are right.
    - The greyed-reason check fails for reason `notincombat` of doc `ITEMS` with no `ITEMS.greyed.notincombat` key, and passes once that key exists.
    - `RepositoryContentTests` still passes with the real, empty registries.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "NameListExporterTests|NamingChecksTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** the exporter, the command, the kinds and the checks, then run `dotnet run --project src/Sim -- name-list` to generate the two files and commit them. Add the five rows to `checks.json`.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: PASS, with `Committed_files_match_the_appendix` green on a fresh checkout (check `.gitattributes` treats `content/**/*.json` as `text eol=lf`; add that rule if it does not).
- [ ] **Step 5: Commit** `git commit -m "M1-04: name list exported to content/naming, V7.NameRegistry, V24 r1/r2/r6 and greyed-reason keys"`

### Task 5: Content hashes embedded in the build

**Files:**
- Modify: `src/Core/Canonical/CanonicalWriter.cs` and `CanonicalReader.cs` (add `Text`/`ReadText`, UTF-8 with a `UInt32` byte length; reading invalid UTF-8 throws `CanonicalFormatException`), `tests/Core.Tests/CanonicalWriterTests.cs`, `CanonicalReaderTests.cs`
- Create: `src/Core/Content/IDisplayCanonical.cs`, `src/Core/Content/ContentHashing.cs`, `src/Core/Content/ContentHashes.g.cs` (generated), `src/Sim/Tools/ContentHashesWriter.cs` (+ the `content-hashes` command)
- Modify: every record type from Tasks 2 and 4 (rules fields via `ICanonical.WriteCanonical`; display fields via `IDisplayCanonical.WriteDisplay`), `docs/superpowers/specs/2026-11-23-seeds-validation-design.md` 4.6 (one sentence recording owner ruling 4)
- Test: `tests/Core.Tests/ContentHashTests.cs`

**Interfaces:**
- `CanonicalWriter.Text(int ordinal, string value)` and `CanonicalReader.ReadText(int ordinal)`. UTF-8 is encoded with `new UTF8Encoding(false, throwOnInvalidBytes: true)`, so a lone surrogate throws `ArgumentException` on write.
- `public interface IDisplayCanonical { void WriteDisplay(CanonicalWriter w); }` lives in `Dungeon.Core.Content`.
- `public readonly record struct ContentHashSet(Digest Manifest, Digest Display, Digest GenContent)`.
- `public static class ContentHashing` with `ContentHashSet Compute(ContentSet set)`:
  - **Manifest:** one root record with field 0, a `SetById` over kinds by `FileName`. Each kind is a record of {0 file name `String`, 1 `SetById` over its records by `Id`, each record's `WriteCanonical`}.
  - **Display:** the same structure with `WriteDisplay`, and only records that implement it.
  - **GenContent:** the manifest structure over kinds with `ReadByGeneration` only.
- `ContentHashes.g.cs` starts with `// <auto-generated/>` (so it is analyzer-checked) and contains `public static class ContentHashes { public const string Manifest = "<64 hex>"; public const string Display = "..."; public const string GenContent = "..."; }`.
- `ContentHashesWriter.Write(ContentHashSet) -> string` is the exact file text: LF and a trailing newline.

- [ ] **Step 1: Write the failing tests.**
  - Writer and reader:
    - `Text(0, "Ünder")` gives a 4-byte length of 6 followed by the UTF-8 bytes `c3 9c 6e 64 65 72`, and round-trips.
    - A lone surrogate throws on write.
    - The bytes `ff` as text throw `CanonicalFormatException` on read.
  - `ContentHashTests`:
    - **Review Focus 2:** content in reversed file order and with CRLF gives equal `ContentHashSet`s.
    - **Field tags:** editing a display field (a strings `text`) changes `Display` but not `Manifest`; editing a rules field (a check `owner`) changes `Manifest` but not `Display`.
    - **No generation kinds yet:** `GenContent` is unchanged by both edits.
    - **Drift guard:** `Embedded_hashes_match_content` computes from the repository `content/` and asserts equality with `ContentHashes.Manifest`, `.Display` and `.GenContent`. The failure message is "run `dotnet run --project src/Sim -- content-hashes`".
    - **Generator output:** `ContentHashesWriter.Write(...)` of the computed set equals the committed `ContentHashes.g.cs` text.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "ContentHashTests|CanonicalWriterTests|CanonicalReaderTests"`. Expected: FAIL to compile.
- [ ] **Step 3: Implement**, then run `dotnet run --project src/Sim -- content-hashes` to write `ContentHashes.g.cs`, and make the SV 4.6 note.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`, `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`, and `dotnet build tests/AnalyzerSmoke/AnalyzerSmoke.csproj` (must still fail with DUN001). Expected: all pass, with no warnings.
- [ ] **Step 5: Commit** `git commit -m "M1-04: manifestHash, displayHash and genContentHash embedded in the build"`

### Task 6: Close the step

**Files:**
- Modify: `README.md` (layout: `content/registry/`, `content/naming/`, `content/strings/`, `src/Core/Content/`, `src/Core/Validation/`, and the two Sim commands), `docs/roadmap/m1-vertical-slice.md` only if the build disagrees with the step text

- [ ] **Step 1: Edit** the README.
- [ ] **Step 2: Verify:** `dotnet test Dungeon.sln`, the Godot build and the smoke build. Expected: as in Task 5.
- [ ] **Step 3: Commit** `git commit -m "M1-04: document the content pipeline"`
- [ ] **Step 4 (controller):** tick issue #7 with evidence, then push and open the PR.
