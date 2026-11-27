# DungeonTTRPG

A turn-based RPG played in runs through a randomly generated dungeon. The goal is tabletop-style
character building (species, class, multiclassing, a meaningful choice at almost every level) with a
different build and a different dungeon every run. A run takes 1–2 hours, death ends it, and some
progress carries over between runs.

The hard part is the rules, not the rendering. The project is set up so the rules stay cheap to test,
simulate and change: they live in a plain C# library with no Godot dependency, and Godot only draws
what that library says happened.

> **Status:** milestone **M0 (Skeleton)**. The rules core, its tests and a placeholder isometric
> scene that moves one unit are in place. Most of the game exists so far as approved design documents
> in [`docs/superpowers/specs`](docs/superpowers/specs).

## Quick start

Requirements:

- [.NET SDK 8](https://dotnet.microsoft.com/download) (pinned in [`global.json`](global.json))
- [Godot 4.7, .NET build](https://godotengine.org/download) (to run the game scene)

Build and test the rules core:

```powershell
dotnet test Dungeon.sln
```

Build the Godot project (it is not part of `Dungeon.sln`):

```powershell
dotnet build dungeon-ttrpg/DungeonTTRPG.csproj
```

Run the game: open [`dungeon-ttrpg/project.godot`](dungeon-ttrpg/project.godot) in Godot 4.7 (.NET)
and press Play. The main scene is [`Main.tscn`](dungeon-ttrpg/Main.tscn).

### Controls (M0 demo)

| Input | Action |
|---|---|
| Left click | Move the unit to a square |
| Q / E | Rotate the camera |
| 1 / 2 / 3 | Choose an option in a pending decision |

All inputs are Godot input actions, so they can be remapped in the project settings.

## Repository layout

```
Dungeon.sln                 Core, Sim, the analyzer and their tests (not the Godot project)
Directory.Build.props       Shared settings: net8.0, nullable, warnings as errors
src/Core/                   Dungeon.Core: the rules engine (no Godot)
  Engine/                   Commands, events, Engine.Apply
  State/                    Immutable GameState and its core-state digest
  Grid/                     GridPos, GridMap
  Queries/                  Read-only questions, e.g. pathfinding
  Random/                   Key algebra (SplitMix64) and run streams
  Persistence/              Versioned snapshots, command logs, replay
  Canonical/                CanonicalWriter, CanonicalReader, SHA-256 Digest (SV 4.5)
  Content/                  Content loader (strict JSON, integers and p/q rationals, ASCII rule strings), ContentSet, the three content hashes (ContentHashes.g.cs is generated)
  Validation/               Validator framework: CheckRow/ReportRow/ArmRow registries, phases, failure classes, build tiers 0-6, read-set cache; Checks/ holds V19.ClassDeclared, V7.NameRegistry, V24 r1/r2/r6 and the content load check
src/Sim/                    Dungeon.Sim: console app for headless simulations (balance) and the two content generators below
src/Analyzers/              Dungeon.Analyzers: Roslyn analyzer enforcing the determinism rules on Core
tests/Core.Tests/           xUnit tests, including architecture rules
tests/Analyzers.Tests/      xUnit tests for the analyzer, one fixture per rule
tests/AnalyzerSmoke/        Must fail to build with DUN001; proves the analyzer is wired (not in Dungeon.sln)
build/Determinism.props     Wires the analyzer and checked arithmetic into Core
content/                    JSON data files: classes, species, feats, items, monsters, tables
  registry/                 Check, report and simulator-arm registries (SV 9.1)
  naming/                   Name list and retired names (generated from the naming-pass appendix), greyed-reason registry
  strings/                  English strings table
dungeon-ttrpg/              Godot 4.7 C# project (display layer)
  scripts/                  GameController, BoardView, UnitView, IsoCamera, DecisionPanel
  addons/godot_ai/          Third-party Godot AI (MCP) editor plugin, MIT
docs/superpowers/
  specs/                    Approved design documents
  plans/                    Implementation plans per milestone
  reviews/                  Cross-document reviews
```

## Content pipeline

Content is JSON under [`content/`](content/README.md), loaded by `src/Core/Content` and checked by
`src/Core/Validation`. A bad reference, a missing field or a duplicate id fails with the file and the
field named. The validators run as part of `dotnet test`.

Three generated outputs are committed, and a test fails if any is stale:

```powershell
dotnet run --project src/Sim -- name-list        # content/naming/name_list.json and retired_names.json, from the naming-pass appendix
dotnet run --project src/Sim -- content-hashes   # src/Core/Content/ContentHashes.g.cs (manifestHash, displayHash, genContentHash)
dotnet run --project src/Sim -- stream-handles   # src/Core/Random/StreamHandles.g.cs, the typed stream handles, from registry/streams.json
```

Run `name-list` after editing the appendix, `content-hashes` after any change under `content/`, and `stream-handles` after any change to `registry/streams.json`.

## Architecture

```
 input ──► GameController ──► Command ──► Engine.Apply(state, command) ──► (new state, events)
                ▲                                                                │
                └──────────── plays events as animation, one at a time ◄─────────┘
```

- **`src/Core` is the only place game rules live.** `Engine.Apply` is a pure function: it takes an
  immutable `GameState` and a command and returns a new state plus a list of events.
- **The Godot project never changes game state.** It turns clicks into commands and plays back the
  events it gets. What is legal comes from `Engine.Apply` and the query functions.
- **Runs are deterministic.** The same seed and the same commands always produce the same run, so a
  run can be saved as a command log and replayed exactly.
- **Pending decisions.** When the rules need a choice, a decision is raised and the only valid command
  is an answer to it.

### Rules for the core

These are enforced by tests and CI, not just convention:

- Core has no floating point, `System.Random`, clocks, runtime hashing, culture-sensitive string
  operations, hash-ordered enumeration, sorts without a registered total comparer, parallelism, Godot,
  signed `/` and `%` outside `IntMath`, or wrapping arithmetic outside one named file. The determinism
  analyzer (`src/Analyzers`, rules DUN001–DUN011) enforces this at compile time; it implements DS11, rule 11
  of the draw-stability rules in section 4.5 of the
  [seeds and validation design](docs/superpowers/specs/2026-11-23-seeds-validation-design.md) ("SV").
  See `src/Analyzers/Rules.cs`. Code Roslyn treats as generated (`<auto-generated>` headers,
  `*.g.cs`/`*.designer.cs`/`*.generated.cs`, `[GeneratedCode]`, source-generator output) is checked
  too: source-generated Core code must follow the same rules, including the output of any .NET
  generator Core adopts (`[GeneratedRegex]`, JSON source generation). Generated code that needs
  wrapping arithmetic must call the key-algebra file, never emit `unchecked`. The no-Godot-in-Core
  rule is also covered by `ArchitectureTests`.
- All randomness comes from keys derived from the 60-bit run seed (SplitMix64 key algebra, SV 4.1);
  a stream is a key and a counter, and every rule draws a fixed number of values.
- Core is meant to open streams only through the generated `StreamHandles` (DS1, SV 4.2): a key comes
  from the generated derivation chain (`Streams.Dungeon(runSeed).Floor(n)...`), and a stream from that
  key's `Open()` or its `Handle` given to a `StageRun`. DUN012 reports, in any file not named by the
  `DungeonKeyDerivationFiles` property (Core names `Random/KeyAlgebra.cs`, `Random/KeyedSort.cs`,
  `Random/StreamRegistry.cs` and `Random/StreamHandles.g.cs`): a call to, or method group of,
  `KeyAlgebra.Derive`, `KeyAlgebra.H`, `KeyAlgebra.Absorb` or `DrawStream.Open`; and a `new`, an explicit
  `default` or a `with` of a `DrawStream`, a `StreamHandle` or a generated `[StreamKey]` key struct (whose
  constructors are internal and whose properties are get-only). It does not see an implicitly zeroed
  `DrawStream`, `StreamHandle` or key struct (array element, unassigned field), or `Draw`/`Mix` on a key
  computed by hand; review covers those. A handle's `Open()` returns a plain `DrawStream` that no ledger
  counts; only a `StageRun` ledgers draws.
- Every stream is a row of `content/registry/streams.json` (SV 4.3, 9.1), and its key is the derivation the row's
  `label` and `indices` name. `dotnet run --project src/Sim -- stream-handles` generates `StreamHandles.g.cs`
  (the `Streams` entry point and one key struct per row) with the Core loader, and a test fails if it is stale.
  A generation stage opens streams through `StageRun`, which gives each a fresh counter at 0 and closes its
  `DrawLedger` at the stage end: a row that drew other than its count class allows raises `DrawLedgerMismatch`
  (a defect, in every build), and a row of a later wave than `Wave.Build` is inert and expects 0 draws.
- Saves carry a version number; a corrupt or newer save fails with a typed error and never
  half-loads.
- CI fails if rule checks (`IsWalkable`, `CheckStep`, `.Step(`) show up in `dungeon-ttrpg/scripts`.

Grid convention: square grid, 8 directions, diagonals cost the same as orthogonal steps, no corner
cutting past walls. `X` grows east, `Y` grows south.

## Continuous integration

[`.github/workflows/ci.yml`](.github/workflows/ci.yml) runs on every push and pull request, on
Ubuntu and Windows:

1. `dotnet test Dungeon.sln`
2. `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`, so a core change cannot silently break the
   Godot project
3. A check that the display layer contains no rule logic

## Roadmap

| Milestone | Contents |
|---|---|
| **M0 Skeleton** (current) | Solution, tests, command and event loop, placeholder isometric grid, one moving unit |
| M1 Vertical slice | One class, levels 1–3, one generated floor, three enemy types, one boss, basic loot, permadeath |
| M2 Full run | Four classes, companions, five floors, level 10, pick-one-of-three offers, save and resume |
| M3 Between runs | Profile, Stairtop hub, unlocks, starting boons, difficulty ladder |
| M4 Breadth | Species, multiclassing, more content, balance passes with the simulator |

The full plan is in section 9 of the
[main design document](docs/superpowers/specs/2026-11-15-dungeon-rpg-design.md).

## Design documents

The design is written before the code and approved section by section. Start with the
[main design](docs/superpowers/specs/2026-11-15-dungeon-rpg-design.md); the other files in
[`docs/superpowers/specs`](docs/superpowers/specs) go into detail on characters and classes, items,
combat, dungeon generation, the camp and hub, and naming. Each change to an approved document is
recorded with an ID (for example `NP-12` or `CH-7`) so it can be traced back to the design that
made it.

## Contributing

- Put every game rule in `src/Core` and cover it with a test in `tests/Core.Tests`.
- Keep the Godot scripts to input and presentation.
- Run `dotnet test Dungeon.sln` and build the Godot project before opening a pull request.
- Warnings are errors; fix them rather than suppressing them.

## Licence

No licence has been chosen for this project yet. The bundled `godot_ai` addon is MIT-licensed; see
[`dungeon-ttrpg/godot-ai-LICENSE.txt`](dungeon-ttrpg/godot-ai-LICENSE.txt).
