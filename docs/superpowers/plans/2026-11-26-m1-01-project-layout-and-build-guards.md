# M1-01 Project Layout and Build Guards Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** The repository matches MAIN 6.1 and the build enforces the DS11 determinism rules on `Dungeon.Core` at compile time.

**Architecture:** A Roslyn analyzer project (`src/Analyzers`, `Dungeon.Analyzers`) reports one error-severity diagnostic per banned construct. `src/Core` loads it through a shared `build/Determinism.props`, which also turns on checked arithmetic in Debug and CI and names the one file allowed to use `unchecked`. The analyzer is unit-tested with in-memory compilations (one fixture per rule, plus clean fixtures that must stay silent); a small smoke project outside the solution proves the real MSBuild wiring fails a build. `FloorDiv`/`FloorMod` live in Core as `IntMath`. `src/Sim` and `content/` are created empty.

**Tech Stack:** C# 12 on .NET 8 (SDK pinned by `global.json`), Microsoft.CodeAnalysis.CSharp 4.8.0 (the compiler shipped with SDK 8.0.100, so the analyzer loads in every 8.0.x SDK), xUnit, GitHub Actions.

**Spec:** [`docs/roadmap/m1-vertical-slice.md` — M1-01](../../roadmap/m1-vertical-slice.md#m1-01-project-layout-and-build-guards), implementing MAIN 6.1, SV 4.1 (wrapping-arithmetic paragraph), SV 4.5 DS11, SV 9.5, SV-28.

## Global Constraints

- DS11 bans, in Core: `float`, `double`, `decimal`; `System.Random`; `DateTime.Now`/`UtcNow`; `Environment.TickCount`; `Guid.NewGuid`; `GetHashCode` and `HashCode`; culture-sensitive comparison or `ToUpper`/`ToLower`; `Dictionary`/`HashSet` enumeration; sorts without a registered total comparer; `Parallel.*`; any reference to GodotSharp.
- DS11: "`/` and `%` with a possibly negative operand use `FloorDiv`/`FloorMod`."
- DS11: "`CheckForOverflowUnderflow` is on in debug and CI, **except** inside the key-algebra file's explicit `unchecked { }` blocks, the only place the analyzer allows wrapping arithmetic."
- MAIN 6.1 projects: `src/Core` (no Godot), `src/Sim` (console app), `tests/Core.Tests`, `dungeon-ttrpg/` (`DungeonTTRPG.csproj`, not in `Dungeon.sln`), `content/` (JSON data).
- `Directory.Build.props` stays as is: net8.0, nullable on, warnings as errors, C# 12.
- Every diagnostic is `DiagnosticSeverity.Error`, category `Determinism`, and its message starts with its ID and `DS11`, then names the construct and what to use instead.
- The analyzer applies to Core only. Not to Sim, tests or the Godot project.
- Out of scope: the hostile-environment nightly (M1-40); the key-algebra file itself (M1-02); the content loader (M1-04).

## Plan-level calls (not fixed by the spec; flagged for review)

1. **Rule IDs** `DUN001`–`DUN011`, one per DS11 clause (table below).
2. **"Registered total comparer"** before any registry exists: a comparer is registered when its type carries `[Dungeon.Core.Determinism.TotalComparer]`, or it is `StringComparer.Ordinal`. `OrderBy`/`ThenBy` (stable) with no comparer are allowed only when the key type is an integral primitive or an enum. Later steps (DS4, K-rule 7) may tighten this.
3. **"Possibly negative operand"** is undecidable in general, so the rule is: `/`, `%`, `/=`, `%=`, `Math.DivRem` and the signed types' `DivRem` statics (`int.DivRem`, `long.DivRem`, ...) on signed integral types are banned unless both operands are compile-time constants. Unsigned types are allowed. `Dungeon.Core.IntMath` is exempt (it implements the helpers).
4. **The exempt file until M1-02:** `Random/Pcg32.cs`, which already wraps inside `unchecked` and is deleted by M1-02. M1-02 repoints the property to the key-algebra file.
5. **"Debug and CI":** `CheckForOverflowUnderflow` is true when `$(Configuration)` is `Debug` or `$(CI)` is `true` (GitHub Actions sets `CI=true`).
6. **MAIN 6.1 gains two rows** — `src/Analyzers` and `tests/Analyzers.Tests` — because SV 9.5 names the analyzer as tooling and the done-when needs MAIN 6.1 and the repository to agree. **Needs owner sign-off** (rule 5 of the roadmap: the designs win, so the design is amended, not ignored).

| ID | DS11 clause | Flags |
|---|---|---|
| DUN001 | floating point | any expression, declaration or type reference of `float`, `double`, `decimal` (incl. literals and `var` bound to them) |
| DUN002 | `System.Random` | any reference to `System.Random` |
| DUN003 | clocks and ambient entropy | `DateTime.Now/UtcNow/Today`, `DateTimeOffset.Now/UtcNow`, `Environment.TickCount/TickCount64`, `Guid.NewGuid`, `System.Diagnostics.Stopwatch`, `System.TimeProvider` |
| DUN004 | hashing | any call of a `GetHashCode` method; any reference to `System.HashCode` |
| DUN005 | culture | `ToUpper`/`ToLower` (non-Invariant: a `CultureInfo.InvariantCulture` argument is allowed); `string.Compare`/`CompareTo`/`StartsWith`/`EndsWith`/`IndexOf`/`LastIndexOf` overloads that compare culture-sensitively (no `StringComparison`, or a `CurrentCulture*`/`InvariantCulture*` one); `StringComparer.CurrentCulture*`/`InvariantCulture*`; `CultureInfo.CurrentCulture`; string comparison through `Comparer<T>.Default` where T is `string` or a tuple with a string element: `Comparer<T>.Default` itself, tuple `CompareTo`, `IComparable<string>.CompareTo` and any non-generic `IComparable.CompareTo`, a generic call binding such a T to a type parameter constrained to `IComparable`, `Enumerable.Min`/`Max`/`MinBy`/`MaxBy` and `List<T>`/`Array` `BinarySearch` with no comparer |
| DUN006 | unordered enumeration | `foreach`, `GetEnumerator`, `CopyTo`, a collection-expression spread, a `Keys`/`Values` read whose view is not itself one of these types, or any conversion to `IEnumerable`/`IEnumerable<T>` (or an interface extending it) of `Dictionary<,>`, `HashSet<>`, their `KeyCollection`/`ValueCollection`, `ImmutableDictionary<,>`, `ImmutableHashSet<>` and their `Builder`s, `FrozenDictionary<,>`, `FrozenSet<>`, `ConcurrentDictionary<,>`, a type derived from one, or a value typed as `IDictionary<,>`, `IReadOnlyDictionary<,>`, `ISet<>` or `IReadOnlySet<>` (its instance may be hash-ordered, e.g. from `System.Text.Json`; hold sorted collections by their concrete type). Lookups and the `CollectionExtensions` lookups (`GetValueOrDefault`/`TryAdd`/`Remove`) stay legal |
| DUN007 | sorts | `List<T>.Sort`, `Array.Sort`, `ImmutableArray<T>.Sort`, `ImmutableArray<T>.Builder.Sort`, `ImmutableList<T>.Sort` and its `Builder`'s, `MemoryExtensions.Sort`, `Enumerable.Order*`/`ThenBy*`, `SortedSet`/`SortedDictionary`/`SortedList` constructors, the `ImmutableSortedDictionary`/`ImmutableSortedSet` factories (`Create`, `CreateRange`, `CreateBuilder`, `ToImmutableSorted*`, collection expressions), unless the comparer is registered (call 2); `ImmutableSortedDictionary<,>.Empty`/`ImmutableSortedSet<>.Empty` always (default comparer) |
| DUN008 | parallelism | any member of `System.Threading.Tasks.Parallel`; any member of `System.Linq.ParallelEnumerable` (`AsParallel`, `Range`, `Repeat`, ...) |
| DUN009 | Godot | each source reference to a symbol in namespace `Godot` (at that location), and additionally once per compilation, at no location, when the compilation references an assembly named `GodotSharp` |
| DUN010 | division | call 3 (including the `int.DivRem`/`long.DivRem`/... statics) |
| DUN011 | wrapping arithmetic | an `unchecked` expression or statement in any file but the one named by `DungeonWrappingArithmeticFile` (relative to the project folder; leading `./` ignored; matched in full against `ProjectDir` + the property when the SDK supplies `ProjectDir`, else as a whole-segment path suffix) |

## Review Focus

1. **Compiler-generated members** (record `Equals`/`GetHashCode`, `ToString`) must not trip DUN004 — Core is full of records. Test: `Record_declarations_are_clean` in Task 4.
2. **Banned types reached without naming them:** `var x = Math.Sqrt(4)`, `set.ToList()`, `set.First()`, `foreach (var k in dict.Keys)`, a `HashSet` passed to an `IEnumerable<T>` parameter. Tests in Tasks 3 and 6.
3. **Compound and library division:** `a /= b`, `a %= b`, `Math.DivRem(a, b)` on `int`/`long`; while `uint`/`ulong` division and constant folding (`int x = -7 / 2;` as a constant) stay legal. Tests in Task 7.
4. **Exempt-file path matching** across `\` and `/` and absolute vs relative paths, so the exemption works on both CI OSs. Test: `Unchecked_allowed_in_named_file_with_either_separator` in Task 7.
5. **`FloorDiv`/`FloorMod` edges:** negative divisor, `int.MinValue / -1` (throws `OverflowException`), division by zero (throws `DivideByZeroException`), `long` overloads. Tests in Task 2.

---

### Task 1: Folder layout, `src/Sim` and `content/`

**Files:**
- Create: `src/Sim/Sim.csproj`, `src/Sim/Program.cs`, `content/README.md`
- Modify: `Dungeon.sln` (add Sim under the `src` folder), `README.md` (Repository layout block), `docs/superpowers/specs/2026-11-15-dungeon-rpg-design.md` §6.1 (two analyzer rows, call 6)
- Test: `tests/Core.Tests/LayoutTests.cs`

**Interfaces:**
- Produces: `RepoRoot.Find()` (in `LayoutTests.cs`, `internal static string`) — walks up from `AppContext.BaseDirectory` to the folder holding `Dungeon.sln`.

- [ ] **Step 1: Write the failing test** `LayoutTests.Repository_matches_MAIN_6_1`: asserts these exist under `RepoRoot.Find()`: `src/Core/Core.csproj`, `src/Sim/Sim.csproj`, `tests/Core.Tests/Core.Tests.csproj`, `dungeon-ttrpg/DungeonTTRPG.csproj`, `content/`; and that `Dungeon.sln` text contains `src\Sim\Sim.csproj` and does **not** contain `DungeonTTRPG.csproj`. (Analyzer paths are added to this test in Task 3.)
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter LayoutTests` — Expected: FAIL, `src/Sim/Sim.csproj` missing.
- [ ] **Step 3: Create Sim.** `Sim.csproj`: `OutputType Exe`, `AssemblyName Dungeon.Sim`, `RootNamespace Dungeon.Sim`, project reference to `../Core/Core.csproj`. `Program.cs`: top-level statements printing `Dungeon.Sim: no simulations yet (M1-38).` and returning 0. Add with `dotnet sln Dungeon.sln add src/Sim/Sim.csproj --solution-folder src`.
- [ ] **Step 4: Create `content/README.md`** (git keeps no empty folder): one paragraph — JSON data files (classes, species, feats, items, monsters, tables, MAIN 6.1), read by the Core loader from M1-04; numbers are integers or rationals (DS13).
- [ ] **Step 5: Run** `dotnet test Dungeon.sln --filter LayoutTests` — Expected: PASS. Also `dotnet run --project src/Sim` prints the line above.
- [ ] **Step 6: Update docs.** README layout block: add `src/Sim/`, `content/`. MAIN 6.1 table: add `src/Analyzers` (C# Roslyn analyzer, `netstandard2.0` — "Enforces the determinism rules on Core at compile time (SV DS11, 9.5)") and `tests/Analyzers.Tests` (C# test project — "Analyzer fixtures"), with a one-line note "Added by roadmap step M1-01."
- [ ] **Step 7: Commit** `git add src/Sim content tests/Core.Tests/LayoutTests.cs Dungeon.sln README.md docs/superpowers/specs/2026-11-15-dungeon-rpg-design.md` — `git commit -m "M1-01: add src/Sim and content/ per MAIN 6.1"`

### Task 2: `IntMath.FloorDiv` / `FloorMod`

**Files:**
- Create: `src/Core/IntMath.cs`
- Test: `tests/Core.Tests/IntMathTests.cs`

**Interfaces:**
- Produces (namespace `Dungeon.Core`): `public static class IntMath` with `int FloorDiv(int a, int b)`, `int FloorMod(int a, int b)`, `long FloorDiv(long a, long b)`, `long FloorMod(long a, long b)`. Contract: `FloorDiv(a,b) * b + FloorMod(a,b) == a`; `FloorMod` has the sign of `b` (or is 0). Its type's metadata name `Dungeon.Core.IntMath` is exempted from DUN010 in Task 7.

- [ ] **Step 1: Write the failing tests** as one `[Theory]` per method over `(a, b, expected)`:
  - `FloorDiv`: `(7,2,3) (-7,2,-4) (7,-2,-4) (-7,-2,3) (6,3,2) (-6,3,-2) (0,5,0)`
  - `FloorMod`: `(7,2,1) (-7,2,1) (7,-2,-1) (-7,-2,-1) (6,3,0) (-6,3,0) (0,5,0)`
  - the same rows through the `long` overloads;
  - `FloorDiv(int.MinValue, -1)` and `FloorDiv(long.MinValue, -1L)` throw `OverflowException`; `FloorMod(int.MinValue, -1) == 0`;
  - `FloorDiv(1, 0)` and `FloorMod(1, 0)` throw `DivideByZeroException`;
  - property test over a fixed grid `a ∈ [-20,20]`, `b ∈ [-6,6] \ {0}`: the contract identity holds and `0 <= |FloorMod| < |b|`.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter IntMathTests` — Expected: FAIL to compile, `IntMath` not defined.
- [ ] **Step 3: Implement** with C#'s truncating `/` and `%`, then adjust the quotient down by one (and the remainder by `b`) when the remainder is non-zero and its sign differs from `b`'s. `int.MinValue / -1` must throw `OverflowException` in every build configuration, so throw it explicitly rather than relying on the runtime. XML doc on the class: "The only place in Core where signed `/` and `%` are allowed (DS11, analyzer rule DUN010)."
- [ ] **Step 4: Run** `dotnet test Dungeon.sln --filter IntMathTests` — Expected: PASS.
- [ ] **Step 5: Commit** — `git commit -m "M1-01: add IntMath.FloorDiv and FloorMod (DS11)"`

### Task 3: Analyzer project, test harness, Core wiring and DUN001

**Files:**
- Create: `src/Analyzers/Analyzers.csproj`, `src/Analyzers/Rules.cs`, `src/Analyzers/FloatingPointAnalyzer.cs`
- Create: `tests/Analyzers.Tests/Analyzers.Tests.csproj`, `tests/Analyzers.Tests/Harness.cs`, `tests/Analyzers.Tests/FloatingPointTests.cs`, `tests/Analyzers.Tests/CleanCoreTests.cs`
- Create: `build/Determinism.props`
- Create: `tests/AnalyzerSmoke/AnalyzerSmoke.csproj`, `tests/AnalyzerSmoke/UsesDouble.cs` (not in `Dungeon.sln`)
- Modify: `src/Core/Core.csproj` (import the props), `Dungeon.sln` (add both new projects), `.github/workflows/ci.yml`, `tests/Core.Tests/LayoutTests.cs`

**Interfaces:**
- Produces: `Dungeon.Analyzers.Rules` — `public static class` holding one `DiagnosticDescriptor` per row of the ID table (`Rules.FloatingPoint`, `Rules.SystemRandom`, `Rules.Clock`, `Rules.Hashing`, `Rules.Culture`, `Rules.UnorderedEnumeration`, `Rules.Sort`, `Rules.Parallel`, `Rules.Godot`, `Rules.Division`, `Rules.Unchecked`), all declared here now so later tasks only add analyzers. Message format: `"{ID} (DS11): {0} is banned in Dungeon.Core; {1}"`.
- Produces: `Harness.Run(string source, string path = "Test.cs", IReadOnlyDictionary<string,string>? buildProperties = null, IEnumerable<MetadataReference>? extraReferences = null) -> ImmutableArray<Diagnostic>` — compiles `source` (C# 12, nullable on, references from `AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")` plus a reference to `Dungeon.Core.dll` so fixtures can use `IntMath` and `TotalComparerAttribute`) with **every** analyzer in `Dungeon.Analyzers`, asserts the compilation has no compiler errors, and returns analyzer diagnostics only. `buildProperties` keys are bare names (`"DungeonWrappingArithmeticFile"`), surfaced to analyzers as `build_property.<name>` through a test `AnalyzerConfigOptionsProvider`.
- Produces: `Harness.Ids(ImmutableArray<Diagnostic>) -> string[]` — distinct IDs, ordinal-sorted. Rule tests assert `Assert.Equal(new[] { "DUN00n" }, Harness.Ids(d))` and that each message contains `"DS11"` and the construct name.
- Produces: `build/Determinism.props` — the one place the analyzer, checked arithmetic and `DungeonWrappingArithmeticFile` are configured. Task 7 adds to it.

- [ ] **Step 1: Write the failing tests** in `FloatingPointTests`, one `[Fact]` per fixture, each expecting exactly `DUN001`: `double` field; `float` parameter; `decimal` local; `var x = 1.5;`; `var r = System.Math.Sqrt(4);`; `int n = (int)2.5;`. And `Clean_integer_code_reports_nothing` (an `int`/`long`/`ulong` class with arithmetic) expecting no diagnostics. Message test: `DUN001` message contains `"DS11"` and `"double"`.
- [ ] **Step 2: Create the projects.** `Analyzers.csproj`: `TargetFramework netstandard2.0` (overrides the props), `AssemblyName Dungeon.Analyzers`, `EnforceExtendedAnalyzerRules true`, `IsRoslynComponent true`, `NoWarn $(NoWarn);RS2008` (release tracking is not used), packages `Microsoft.CodeAnalysis.CSharp 4.8.0` and `Microsoft.CodeAnalysis.Analyzers 3.3.4` (`PrivateAssets all`). `Analyzers.Tests.csproj`: like `Core.Tests.csproj`, plus `Microsoft.CodeAnalysis.CSharp 4.8.0` and project references to the analyzer and to Core. Add both to `Dungeon.sln` (folders `src`, `tests`).
- [ ] **Step 3: Run** `dotnet test Dungeon.sln --filter FloatingPointTests` — Expected: FAIL, no diagnostics reported.
- [ ] **Step 4: Implement `Rules` and `FloatingPointAnalyzer`.** Register `OperationAction` (any operation whose `Type` is `float`/`double`/`decimal`) and `SymbolAction` (field, property, parameter, method return, local declarations of those types); report each syntax location once. `ConfigureGeneratedCodeAnalysis(None)`, `EnableConcurrentExecution()`.
- [ ] **Step 5: Run** `dotnet test Dungeon.sln --filter FloatingPointTests` — Expected: PASS.
- [ ] **Step 6: Wire into Core.** `build/Determinism.props` adds `<ProjectReference Include="$(MSBuildThisFileDirectory)../src/Analyzers/Analyzers.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" PrivateAssets="all" />`. `Core.csproj` imports it. The smoke project (`OutputType Library`, imports the same props) holds one file with a `double` field.
- [ ] **Step 7: Verify the real build.** `dotnet build Dungeon.sln` — Expected: success. `dotnet build tests/AnalyzerSmoke/AnalyzerSmoke.csproj` — Expected: FAIL, output contains `error DUN001` and `DS11`. `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` — Expected: success.
- [ ] **Step 8: CI and layout.** Add a step after the Godot build, shell bash: build the smoke project, expect a non-zero exit and `error DUN001` in the output, otherwise fail with "Analyzer is not wired into the build." Extend `LayoutTests` with `src/Analyzers/Analyzers.csproj` and `tests/Analyzers.Tests/Analyzers.Tests.csproj`.
- [ ] **Step 9: Commit** — `git commit -m "M1-01: add determinism analyzer with DUN001 and wire it into Core"`

### Task 4: Banned APIs — DUN002, DUN003, DUN004, DUN008, DUN009

**Files:**
- Create: `src/Analyzers/BannedApiAnalyzer.cs`, `tests/Analyzers.Tests/BannedApiTests.cs`
- Modify: `tests/Analyzers.Tests/CleanCoreTests.cs`

**Interfaces:**
- Consumes: `Rules.*`, `Harness.Run`, `Harness.Ids` (Task 3).

- [ ] **Step 1: Write the failing tests**, one fixture each, expecting exactly the ID shown:
  - DUN002: `new System.Random(1)`; a `System.Random` field; `System.Random.Shared.Next()`.
  - DUN003: `DateTime.Now`, `DateTime.UtcNow`, `DateTimeOffset.UtcNow`, `Environment.TickCount`, `Environment.TickCount64`, `Guid.NewGuid()`, `Stopwatch.StartNew()`.
  - DUN004: `"a".GetHashCode()`; `x.GetHashCode()` on an `int`; `HashCode.Combine(1, 2)`; `new HashCode()`.
  - DUN008: `Parallel.For(0, 2, _ => { })`; `new[] {1}.AsParallel()`.
  - DUN009: compile a fixture with an extra in-memory reference to an assembly named `GodotSharp` (build it in the test with `CSharpCompilation.Create("GodotSharp", …).EmitToImageReference()` containing `namespace Godot { public class Node {} }`) and a `Godot.Node` field — expects DUN009.
  - `CleanCoreTests.Record_declarations_are_clean`: a `record`, a `record struct`, and a class overriding `GetHashCode` with `=> 0` — expects no diagnostics (Review Focus 1).
  - `CleanCoreTests.Ordinal_and_invariant_calls_are_clean` is added in Task 5.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "BannedApiTests|CleanCoreTests"` — Expected: the banned fixtures FAIL (no diagnostics).
- [ ] **Step 3: Implement `BannedApiAnalyzer`** from a table of metadata names → rule, resolved once per compilation with `GetTypeByMetadataName` in `CompilationStartAction`; match on symbol identity (`SymbolEqualityComparer.Default`) in `OperationAction`s for invocations, member references, object creations and type references. DUN004 matches any method named `GetHashCode` overriding `object.GetHashCode` (so `int.GetHashCode` and user overrides are caught at the call site, never at the declaration). DUN009: in `CompilationAction`, report if `compilation.ReferencedAssemblyNames` contains `GodotSharp` (ordinal-ignore-case).
- [ ] **Step 4: Run** the same filter — Expected: PASS.
- [ ] **Step 5: Run** `dotnet build Dungeon.sln && dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` — Expected: both succeed (Core has none of these; records are clean).
- [ ] **Step 6: Commit** — `git commit -m "M1-01: ban Random, clocks, hashing, Parallel and Godot in Core (DUN002-004, 008, 009)"`

### Task 5: Culture and sorts — DUN005, DUN007, `TotalComparerAttribute`

**Files:**
- Create: `src/Core/Determinism/TotalComparerAttribute.cs`, `src/Analyzers/CultureAnalyzer.cs`, `src/Analyzers/SortAnalyzer.cs`
- Test: `tests/Analyzers.Tests/CultureTests.cs`, `tests/Analyzers.Tests/SortTests.cs`; modify `CleanCoreTests.cs`

**Interfaces:**
- Produces: `namespace Dungeon.Core.Determinism; [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)] public sealed class TotalComparerAttribute : Attribute` — XML doc: "Marks an `IComparer<T>` whose order is total over the values it sorts (DS4). Sorts in Core must use one (DS11, DUN007)." The analyzer finds it by metadata name `Dungeon.Core.Determinism.TotalComparerAttribute`.

- [ ] **Step 1: Write the failing tests.**
  - DUN005 (each alone): `s.ToUpper()`, `s.ToLower()`, `string.Compare(a, b)`, `a.CompareTo(b)`, `a.StartsWith("x")`, `a.EndsWith("x")`, `a.IndexOf("x")`, `string.Equals(a, b, StringComparison.CurrentCulture)`, `a.StartsWith("x", StringComparison.InvariantCultureIgnoreCase)`, `StringComparer.InvariantCulture`, `CultureInfo.CurrentCulture`.
  - DUN007 (each alone): `list.Sort()`; `list.Sort((x, y) => x - y)`; `Array.Sort(arr)`; `arr.AsSpan().Sort()`; `immutable.Sort()`; `names.OrderBy(n => n)` (string key); `items.OrderBy(i => i.Name)`; `new SortedSet<string>()`; `list.Sort(new UnmarkedComparer())`.
  - `CleanCoreTests.Ordinal_and_invariant_calls_are_clean`: `string.Equals(a, b, StringComparison.Ordinal)`, `a.StartsWith("x", StringComparison.Ordinal)`, `string.CompareOrdinal(a, b)`, `a.ToUpperInvariant()`, `a.IndexOf('x')` (char overload is ordinal), `list.Sort(new MarkedComparer())` where `MarkedComparer` carries `[TotalComparer]`, `list.Sort(StringComparer.Ordinal)`, `units.OrderBy(u => u.Id)` with an `int` key, `OrderBy` on an enum key, `ThenBy(..., new MarkedComparer())` — expects no diagnostics.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "CultureTests|SortTests|CleanCoreTests"` — Expected: FAIL.
- [ ] **Step 3: Implement `TotalComparerAttribute`, then the two analyzers.** Culture: an invocation of a `string` comparison method is culture-sensitive when the resolved overload has no `StringComparison` parameter and takes a `string` (not `char`) argument, or when the passed `StringComparison` is a constant other than `Ordinal`/`OrdinalIgnoreCase`; a non-constant `StringComparison` argument is also reported (cannot be proven). Sort: for each listed sort API, find the comparer argument; registered = its static type carries the attribute, or the expression is `StringComparer.Ordinal`; with no comparer, `Order*`/`ThenBy*` pass only for integral-primitive or enum keys; every other unregistered case reports.
- [ ] **Step 4: Run** the same filter — Expected: PASS.
- [ ] **Step 5: Run** `dotnet build Dungeon.sln && dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` — Expected: success (`GameState.Create` sorts by `int` key; `PersistenceJson.ParseEnum` uses `Ordinal`).
- [ ] **Step 6: Commit** — `git commit -m "M1-01: ban culture-sensitive comparison and unregistered sorts (DUN005, DUN007)"`

### Task 6: Unordered enumeration — DUN006

**Files:**
- Create: `src/Analyzers/UnorderedEnumerationAnalyzer.cs`, `tests/Analyzers.Tests/UnorderedEnumerationTests.cs`

**Interfaces:**
- Consumes: `Rules.UnorderedEnumeration`, `Harness` (Task 3).

- [ ] **Step 1: Write the failing tests**, each expecting exactly DUN006: `foreach` over a `Dictionary<int,int>`; over `dict.Keys`; over `dict.Values`; over a `HashSet<int>`; `set.ToList()`; `set.First()`; `dict.Select(kv => kv.Key)`; passing a `HashSet<int>` to an `IEnumerable<int>` parameter; `set.CopyTo(array)`; `dict.GetEnumerator()`; `[.. set]` spread; `foreach` over `ImmutableDictionary<int,int>` and `ImmutableHashSet<int>`. Clean fixture: `dict.TryGetValue`, `dict[k] = v`, `dict.ContainsKey`, `set.Contains`, `set.Count`, `foreach` over `ImmutableArray<int>`/`List<int>` — no diagnostics. (Lookups stay legal; only iteration order is banned.)
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter UnorderedEnumerationTests` — Expected: FAIL.
- [ ] **Step 3: Implement.** Report on: `IForEachLoopOperation` whose collection (conversions stripped) has an unordered type; `IConversionOperation` from an unordered type to `IEnumerable`/`IEnumerable<T>` (covers LINQ, parameters, spreads); invocations of `GetEnumerator` or `CopyTo` on one. "Unordered type" = original definition is one of the listed generic types or their nested `KeyCollection`/`ValueCollection`.
- [ ] **Step 4: Run** the same filter — Expected: PASS.
- [ ] **Step 5: Run** `dotnet build Dungeon.sln && dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` — Expected: success.
- [ ] **Step 6: Commit** — `git commit -m "M1-01: ban Dictionary and HashSet enumeration in Core (DUN006)"`

### Task 7: Division, `unchecked` and checked arithmetic — DUN010, DUN011

**Files:**
- Create: `src/Analyzers/ArithmeticAnalyzer.cs`, `tests/Analyzers.Tests/ArithmeticTests.cs`
- Modify: `build/Determinism.props`, `src/Core/Core.csproj`, `src/Core/IntMath.cs`, `src/Core/CoreAssembly.cs`, `tests/Core.Tests/ArchitectureTests.cs`

**Interfaces:**
- Consumes: `IntMath` (Task 2), `Harness` with `buildProperties` (Task 3).
- Produces: MSBuild property `DungeonWrappingArithmeticFile` (path relative to the project folder, `/` or `\`), set in `Core.csproj` to `Random/Pcg32.cs`; made visible with `<CompilerVisibleProperty Include="DungeonWrappingArithmeticFile" />` in the props. **M1-02 changes this value** to its key-algebra file.

- [ ] **Step 1: Write the failing tests.**
  - DUN010 (each alone, `int` and `long` variants): `a / b`, `a % b`, `a /= b`, `a %= b`, `a / 2` (variable left, constant right), `Math.DivRem(a, b)`.
  - Clean: `uint`/`ulong` `/` and `%`; `const int x = -7 / 2;`; `IntMath.FloorDiv(a, b)`; the exact text of `src/Core/IntMath.cs` compiled at path `src/Core/IntMath.cs` (exempt by type, not path).
  - DUN011: `unchecked(a + b)` and an `unchecked { }` block at path `src/Core/Other.cs` with `DungeonWrappingArithmeticFile=Random/Pcg32.cs` — expects DUN011; with no property set at all — expects DUN011.
  - `Unchecked_allowed_in_named_file_with_either_separator`: the same code at `C:\repo\src\Core\Random\Pcg32.cs` and at `/repo/src/Core/Random/Pcg32.cs`, property `Random/Pcg32.cs` and `Random\Pcg32.cs` — no diagnostics in all four combinations (Review Focus 4).
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter ArithmeticTests` — Expected: FAIL.
- [ ] **Step 3: Implement `ArithmeticAnalyzer`.** DUN010 on `IBinaryOperation` (Divide/Remainder) and `ICompoundAssignmentOperation` with a signed integral operand type and a non-constant result, and on `Math.DivRem` with signed arguments; skip when the containing type is `Dungeon.Core.IntMath`. DUN011 on `CheckedExpressionSyntax`/`CheckedStatementSyntax` with the `unchecked` keyword, unless the tree's path, with `\` normalised to `/`, ends with `/` + the normalised property value (ordinal, case-sensitive).
- [ ] **Step 4: Run** the same filter — Expected: PASS.
- [ ] **Step 5: Checked arithmetic.** In `build/Determinism.props`: `<CheckForOverflowUnderflow Condition="'$(Configuration)' == 'Debug' or '$(CI)' == 'true'">true</CheckForOverflowUnderflow>`. Prove it with `ArchitectureTests.Core_is_built_with_checked_arithmetic`: add `internal static int CheckedProbe(int a, int b) => a + b;` to `IntMath` and `[assembly: InternalsVisibleTo("Dungeon.Core.Tests")]` to `CoreAssembly.cs`; assert `IntMath.CheckedProbe(int.MaxValue, 1)` throws `OverflowException`. Tests run in Debug locally and in CI, so this fails if the props stop applying. (Write the test first and watch it fail before adding the property.)
- [ ] **Step 6: Run** `dotnet build Dungeon.sln && dotnet test Dungeon.sln && dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` — Expected: all succeed; the 126 M0 tests still pass under checked arithmetic (M0 already uses `checked` where the tick can overflow; `Pcg32` wraps inside its exempt `unchecked` blocks). Any failure here is a real overflow to fix, not to exempt.
- [ ] **Step 7: Commit** — `git commit -m "M1-01: ban signed division and stray unchecked; checked arithmetic in Debug and CI (DUN010, DUN011)"`

### Task 8: Close the step

**Files:**
- Modify: `README.md` ("Rules for the core" list), `docs/roadmap/m1-vertical-slice.md` (only if the build disagrees with the step text)

- [ ] **Step 1: Docs.** In README "Rules for the core", replace the hand-listed bans with: "Enforced at compile time by the determinism analyzer (`src/Analyzers`, rules DUN001–DUN011, SV DS11); see `src/Analyzers/Rules.cs`." Keep the save and display-layer lines.
- [ ] **Step 2: Full verification.** After `dotnet build-server shutdown` (so no stale analyzer is cached): `dotnet test Dungeon.sln` — PASS; `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` — success; smoke builds fail as designed. Push the branch and confirm CI is green on Ubuntu and Windows.
- [ ] **Step 3: Godot by hand** on Windows and Linux: open `dungeon-ttrpg/`, Build, Play `Main.tscn`, click-move once. No visible change is expected; note the result in the issue.
- [ ] **Step 4: Tick the issue's "Done when"** with evidence: rule fixtures → `Analyzers.Tests` test names per rule plus the smoke CI step; clean build → CI run link; architecture → `ArchitectureTests` and `LayoutTests`.
- [ ] **Step 5: Commit** — `git commit -m "M1-01: document the determinism analyzer"`
