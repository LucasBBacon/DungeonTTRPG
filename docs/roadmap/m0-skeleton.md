# M0 Skeleton

**Question it answers:** does the split between the rules core and the
display work? (MAIN 9)

**Status: complete.** All steps below are done. This file is kept as a
record; nothing here needs building.

Detailed plan: [`2026-11-15-m0-skeleton.md`](../superpowers/plans/2026-11-15-m0-skeleton.md)
(its checkboxes were not ticked as work landed; this file is the record).

---

### M0-01 Rules core and display skeleton

**Goal:** A Godot-free C# rules core with a deterministic command and
event loop, pending decisions, saves and replays, and a Godot scene that
moves one unit through it.

**Implements:** MAIN 6.1, 6.2, 6.3 (M0 subset), 6.6 (snapshot and log),
6.8; M0 plan tasks 1–8.

**Delivered:**
- `Dungeon.sln` with `src/Core` (`Dungeon.Core`) and `tests/Core.Tests`.
- `Engine.Apply(state, command)` returning new state and events;
  `MoveCommand`, `AnswerDecisionCommand`; pending decisions.
- `Pcg32` seeded streams (generation, combat, loot).
- `GridMap`, `GridPos`, 8-direction movement, no corner cutting.
- Versioned `Snapshot`, `CommandLog`, `Replay` with typed errors.
- `Queries.FindPath`.
- Godot 4.7 project `dungeon-ttrpg/` with isometric board, click-to-move,
  camera rotation, decision panel.
- CI on Ubuntu and Windows: tests, Godot project build, display-layer
  rule grep.

**Done when:**
- [x] `dotnet test Dungeon.sln` passes on Ubuntu and Windows in CI
  (126 tests, 2026-11-24).
- [x] `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` succeeds.
- [x] The M0 plan's Task 8 manual Godot checks pass on Windows and Linux
  (confirmed by the team, 2026-11-24).
- [x] Architecture tests: no Godot reference, no floating point, no
  `System.Random`, no unordered collections in Core.

### M0-END Sign-off

- [x] The core/display split works, including a decision that pauses the
  rules mid-command.

**Carried forward to M1** (the approved designs moved on after M0 was
planned; see step M1-02 onward):
- `Pcg32` is replaced by the SplitMix64 key algebra (SV-1).
- `Engine.DrawBoons` uses a rejection loop whose draw count depends on
  values drawn; this breaks the fixed-draw-count rule (SV DS3).
- JSON snapshots become the canonical save format of SV 4.8.
- The folder names in MAIN 6.1 differed from the repository; resolved
  by D-01 (MAIN 6.1 now names `dungeon-ttrpg/`; M1-01 creates `content/`
  and `src/Sim`).
