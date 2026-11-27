# M1-07 Effects System Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Every rule exception is a modifier or a trigger, folded and fired the same way whatever its source. Effects are defined in JSON; active instances live in the run state; derived numbers are pure fold queries; triggers fire on rule events in a fixed order; anything data cannot say is a C# hook referenced by id.

**Architecture:**
- **Effect definitions** (`content/effects/effects.json`, `EffectsKind`): each has an id, a source `kind` (condition, trait, gear, ability, item, environment), `modifiers[]` and `triggers[]`.
- **Rules context:** a `RulesContext` holds the loaded definitions plus three code registries: derived numbers, rule events and hooks. Building it validates every reference against them, failing with the file and field named.
- **Run state:** active effects are `EffectInstance`s in `GameState.Effects`, numbered by a creation counter.
- **Fold queries:** `Effects.Number` and `Effects.RollModeOf` fold modifiers on demand. The stacking regime is chosen by source kind and by number rule, and nothing derived is stored.
- **Triggers:** `Engine.Apply(state, command, rules)` dispatches each emitted event to matching triggers in (priority, source unit order, creation order). Trigger actions are `applyEffect` (data) or `hook` (C#), and the chain is bounded.
- **Vocabulary document:** `docs/effects-vocabulary.md` records the vocabulary. The other developer approves it in this step's PR.

**Tech Stack:** C# 12 / .NET 8; the M1-04 content loader; M1-03 canonical writer and digests; M1-06 save version and digest patterns; xUnit.

**Spec:** [`docs/roadmap/m1-vertical-slice.md`, M1-07](../../roadmap/m1-vertical-slice.md#m1-07-effects-system), implementing:
- **MAIN 6.4 / 6.5** (`2026-11-15-dungeon-rpg-design.md`, lines 593–615).
- **SM 3, 9** (`2026-11-16-character-stat-model-design.md`, 98–107, 522–554), and SM 5.1 for the ability non-stacking rule.
- **AB 6.1, 6.2, 14.3–14.4, 18** (`2026-11-16-ability-resource-model-design.md`).
- **CO 6.7, 17.1–17.2, 23** (`2026-11-16-conditions-design.md`).
- **DH 20.3–20.4** (`2026-11-17-death-healing-design.md`).

## Owner ruling for this step (2026-11-28, client)

**D-15, advantage netting: the 5E rule.** Any number of advantage sources give advantage, any number of disadvantage sources give disadvantage, and one of each cancels to a normal roll, whatever the counts (CO 6.7). Weapons 7.5 ("net them (one cancels one)") is amended to "fold them (conditions 6.7: any advantage and any disadvantage cancel)", and Overlap and Vex read the folded mode. Task 1 records this.

## Global Constraints

- MAIN 6.4: modifiers "change a number or a flag"; triggers "react to a rule event"; "The rules engine raises events at fixed points … and asks all active effects to modify or respond. New content should need new data, and only rarely new code."
- MAIN 6.5: "Effects that cannot be expressed in data are implemented in C# and referenced from data by ID"; a bad reference "fails loudly with the file and field named".
- SM 6/9: "Nothing derived is stored. Each number is computed on demand." The display asks "what would +1 Strength change?", so folds must work on a hypothetical state. They are pure and make no draws.
- SM 5.1: "Bonuses to the same ability do not stack: only the highest applies … only the worst [penalty] applies. A bonus and a penalty both apply." "The hard cap is +5 after all bonuses."
- CO 6.7 (conditions only): "only the worst penalty and the best bonus to each derived number apply; a bonus and a penalty both apply; only the highest dice bonus applies to a given roll; advantage and disadvantage … any number of sources give one, and one of each cancels; speed: flat changes first, then at most one halving; speed 0 overrides everything." "Gear and traits still stack as the stat model says." SM 9: "Other derived numbers stack normally unless a rule above says otherwise."
- AB 14.4: "Several triggers on one event run in order of priority, then source unit order, then effect creation order." "Effects that data cannot express … are C# hooks referenced from data by ID."
- SM 3: integer arithmetic only, all rounding down (`IntMath.FloorDiv`).
- The determinism analyzer stays green: sorted `ImmutableArray`s, a `[TotalComparer]` on every sort, no floating point, checked arithmetic.
- Out of scope:
  - **M1-08:** the six abilities and the SM 6 derived numbers.
  - **M1-09:** damage parts and the ladder.
  - **M1-10:** turn and duration clocks.
  - **M1-11:** ability use steps and reactions.
  - **M1-12 / M1-13:** the condition pipeline.
  - **M1-09 or later:** dice bonuses. They are reserved in the vocabulary; their fold is "highest only" and is implemented when dice exist.

## Plan-level calls (mine; the vocabulary document carries them for the other developer's approval)

1. **The stacking regime comes from the effect's source kind:**
   - `condition` modifiers use the same-number rule: the best bonus plus the worst penalty.
   - `trait`, `gear`, `item`, `ability` and `environment` modifiers stack (they sum).
   - Both groups then combine by addition, so "gear and traits stack with conditions".
   - A derived number may also declare the `abilityScore` rule. It overrides the kind regime for that number: across all sources, the highest bonus plus the worst penalty, then the cap (SM 5.1).
2. **Modifier operations implemented now:**
   - `add` (signed integer);
   - `halve` (at most one halving, applied after the flat sum, rounding down);
   - `zero` (overrides everything to 0);
   - `rollMode` (`advantage` | `disadvantage`, folded by D-15);
   - `flag` (sets a named boolean).

   `diceBonus` is reserved in the document and rejected by the loader until dice exist.
3. **The trigger schema is `{on, priority, scope, do[]}`:**
   - `on`: a registered event id.
   - `priority`: an integer, default 0, where **lower fires first**.
   - `scope`: `holder` fires when the event's subject unit is the effect's holder; `any` fires for any subject.
   - `do`: an ordered list of `{applyEffect, to: holder|subject}` or `{hook, args?}`.
4. **Source unit order** is the source unit's position in `GameState.Units`, which is sorted by id. Environment sources (no unit) order **after** all units. Creation order is `EffectInstance.Seq`, from `GameState.NextEffectSeq`, which starts at 1 and never reuses a value. Priority, source and seq together are a total order.
5. **Code registries are tables passed into `RulesContext`.** `DerivedNumbers.Core`, `RuleEvents.Core` and `Hooks.Core` are the production tables. Tests add fixture numbers and hooks without touching them.
   - M1-07's production tables hold `speed` (base 6, SM 6, with the same-number speed fold), the roll targets `attackRoll` and `attacksAgainst`, the event `unitMoved`, and no hooks.
   - Later steps add rows.
6. **`Engine.Apply(state, command)` keeps its signature** and runs with `RulesContext.Empty` (no effects), so M0 behaviour, Godot and the goldens are unchanged. `Engine.Apply(state, command, rules)` is the effect-aware entry point.
7. **Trigger chains:**
   - Events are processed first in, first out within one `Apply`.
   - An action's events join the end of the queue.
   - A command may fire at most `Effects.MaxTriggerFirings = 64` triggers. Past that, `TriggerLoopException` is thrown, which is a `defect`.
8. **Save version 4** adds `effects` (instances sorted by seq) and `nextEffectSeq`; `StateDigest` grows from 9 to 11 fields. `RulesVersion` stays 1: with no effects, every command does what it did (call 6).

## Review Focus

1. **Stacking per source kind:** two `condition` +2 bonuses on `speed` give +2, but two `gear` +2s give +4. A condition +2 and −3 give −1; gear +2 plus condition +2 plus condition −1 gives +3. Each mixture has a test. Task 2.
2. **The ability rule and the cap:** on an `abilityScore` number, a trait +2 and a gear +1 give +2 (highest only), and penalties −1 and −2 give −2. The cap applies after both: base 4 + 3 = 5, not 7. Task 2.
3. **Speed:** flat changes, then one halving (two `halve` modifiers halve once); `zero` beats everything, including a later `add`; halving rounds down (7 → 3). Task 2.
4. **Trigger order and determinism:** equal priorities fall back to source unit order, then seq. An environment source fires after unit sources. Shuffling `GameState.Effects` cannot change firing order. A trigger that re-fires itself stops at 64 with `TriggerLoopException`. Task 3.
5. **Load-time references:** an unknown number, event, hook or `applyEffect` id, or a `diceBonus`, each fails with the file and field named (`effects/effects.json`, `$.effects[i].…`). Task 1 covers the content side and Task 3 the hook side.

---

### Task 1: The vocabulary document, the registries and the `effects` content kind

**Files:**
- Create:
  - `docs/effects-vocabulary.md`
  - `src/Core/Effects/EffectDefinition.cs` (`EffectSourceKind`, `Modifier`, `ModifierOp`, `Trigger`, `TriggerAction`, `TriggerScope`, `EffectDefinition`)
  - `src/Core/Effects/Registries.cs` (`NumberRule`, `DerivedNumber`, `DerivedNumbers`, `RollTargets`, `RuleEvents`)
  - `src/Core/Effects/EffectsKind.cs`
  - `content/effects/effects.json` (`{"effects": []}`)
- Modify:
  - `src/Core/Content/ContentKind.cs`: add `new EffectsKind()` to `ContentKinds.All`.
  - `content/README.md`: a layout row.
  - `docs/superpowers/specs/2026-11-17-weapons-design.md` line 799: the D-15 wording.
  - `docs/roadmap/README.md`: D-15, struck as resolved, citing CO 6.7 and weapons 7.5.
  - Regenerate `ContentHashes.g.cs`.
- Test: `tests/Core.Tests/EffectsKindTests.cs`

**Interfaces (namespace `Dungeon.Core.Effects`):**
- `public enum EffectSourceKind { Condition, Trait, Gear, Item, Ability, Environment }`, written lower-case in JSON.
- `public enum ModifierOp { Add, Halve, Zero, RollMode, Flag }`.
- `public sealed record Modifier(string Target, ModifierOp Op, long Value, RollMode Mode, string? FlagName)`.
  - JSON: `{"target":"speed","add":2}`, `{"target":"speed","halve":true}`, `{"target":"speed","zero":true}`, `{"target":"attacksAgainst","rollMode":"advantage"}` and `{"target":"<flagTarget>","flag":"<name>"}`.
  - Exactly one operation key per modifier.
- `public enum RollMode { Normal, Advantage, Disadvantage }`.
- `public enum TriggerScope { Holder, Any }`.
- `public sealed record TriggerAction(string? ApplyEffect, TriggerTarget To, string? Hook, ImmutableArray<string> Args)`, with exactly one of `ApplyEffect` or `Hook` set. `public enum TriggerTarget { Holder, Subject }`.
- `public sealed record Trigger(string On, int Priority, TriggerScope Scope, ImmutableArray<TriggerAction> Do)`.
- `public sealed record EffectDefinition(string File, string Id, EffectSourceKind Kind, ImmutableArray<Modifier> Modifiers, ImmutableArray<Trigger> Triggers) : ContentRecord(File, Id)`. Ids are content ids, and `WriteCanonical` writes every field.
- `public enum NumberRule { Stack, AbilityScore, Speed }`.
- `public sealed record DerivedNumber(string Id, NumberRule Rule, long Base, long? Cap)`.
- `public static class DerivedNumbers { ImmutableArray<DerivedNumber> Core }` holds `speed` (`Speed`, base 6).
- `public static class RollTargets { ImmutableArray<string> Core }` holds `attackRoll` and `attacksAgainst`.
- `public static class RuleEvents { ImmutableArray<string> Core }` holds `unitMoved`.
- `EffectsKind` reads `effects/effects.json`. At load it rejects:
  - an unknown key, or zero or two operation keys;
  - `diceBonus` (reserved);
  - an empty `do`;
  - an action with both or neither of `applyEffect` and `hook`;
  - a bad enum value.

  `Resolve` checks that `applyEffect` ids exist in the file. Number, roll-target, event and hook ids are checked when the `RulesContext` is built (Task 3), because tests extend those registries.
- **`docs/effects-vocabulary.md`** covers:
  - the definition schema, with one JSON example per operation and trigger form;
  - every source kind and its stacking regime (call 1), the number rules, and the speed fold;
  - D-15;
  - trigger order (call 3–4) and the chain bound (call 7);
  - hooks;
  - the reserved items (`diceBonus`, durations from M1-10/12, riders from M2);
  - a table mapping each later step (M1-08 … M1-18) to the vocabulary rows it will use.

  It ends with a line saying the other developer approves it in this step's PR.

- [ ] **Step 1: Write the failing tests** in `EffectsKindTests`. They load fixture `ContentFile`s through `ContentLoader.Load(files, [new EffectsKind()])`:
  - A valid file loads every operation form and both trigger action forms.
  - Each rejection in the Interfaces list names `effects/effects.json` and the exact JSON path, e.g. `$.effects[0].modifiers[0]` for two operation keys or `$.effects[0].triggers[0].do[0].applyEffect` for a missing effect.
  - The repository file loads empty, and `ContentHashes` match after regeneration.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter EffectsKindTests`. Expected: FAIL to compile.
- [ ] **Step 3: Implement** the types, the kind, the empty content file, the docs and D-15; then `dotnet run --project src/Sim -- content-hashes`.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`. Expected: PASS, with no warnings.
- [ ] **Step 5: Commit** `git commit -m "M1-07: effects vocabulary document, effect definitions content kind, and code registries; record D-15"`

### Task 2: Active effects in the run state and the fold queries

**Files:**
- Create: `src/Core/Effects/EffectInstance.cs`, `src/Core/Effects/RulesContext.cs`, `src/Core/Effects/Effects.cs`
- Modify:
  - `src/Core/State/GameState.cs`: add `ImmutableArray<EffectInstance> Effects` and `long NextEffectSeq` after `Unlocks`. `Create` starts them empty and 1.
  - `src/Core/State/StateDigest.cs`: fields 9 and 10.
  - `src/Core/Persistence/Snapshot.cs`: version 4, `effects` and `nextEffectSeq`. Load rejects duplicate or non-positive seqs, a seq ≥ `nextEffectSeq`, unknown holder or source units, and unsorted instances.
  - `tests/Core.Tests/PersistenceTests.cs`: version numbers 3 → 4 and 4 → 5.
  - `tests/Core.Tests/StateDigestTests.cs`: two coverage cases.
- Test: `tests/Core.Tests/EffectFoldTests.cs`

**Interfaces:**
- `public readonly record struct EffectSource(UnitId? Unit)`, with `static EffectSource Environment => new(null)`.
- `public sealed record EffectInstance(long Seq, string EffectId, UnitId Holder, EffectSource Source) : ICanonical`.
- `public sealed class RulesContext`:
  - `static RulesContext Empty` (no definitions; the core registries).
  - `static RulesContext Create(ContentSet content, ImmutableArray<DerivedNumber>? numbers = null, ImmutableArray<string>? rollTargets = null, ImmutableArray<string>? events = null, HookRegistry? hooks = null)`. It validates every modifier target against numbers plus roll targets, every `on` against events, and every hook id against hooks. A failure throws `ContentLoadException` naming the definition's file and path.
  - `EffectDefinition Definition(string id)`; `DerivedNumber Number(string id)`.
  - `HookRegistry` is introduced in Task 3. Task 2 declares the optional parameter as `object? hooks = null`, and Task 3 replaces it.
- `public static class Effects`:
  - `long Number(RulesContext rules, GameState s, UnitId unit, string numberId)`;
  - `RollMode RollModeOf(RulesContext rules, GameState s, UnitId unit, string rollTarget)`;
  - `bool Flag(RulesContext rules, GameState s, UnitId unit, string target, string name)`;
  - `GameState Apply(GameState s, string effectId, UnitId holder, EffectSource source)` (assigns `NextEffectSeq` and increments it, checked);
  - `GameState Remove(GameState s, long seq)`.
  - All folds are pure and make no draws. They read the instances whose holder is `unit`, in seq order.
- **Fold algorithm** for `Number`:

  ```
  stackSum      = Σ add over non-condition modifiers
  condBonus     = max(0, max add over condition modifiers)
  condPenalty   = min(0, min add over condition modifiers)
  flat          = Base + stackSum + condBonus + condPenalty                  (NumberRule.Stack, NumberRule.Speed)
  AbilityScore: flat = Base + max(0, max add, all sources) + min(0, min add, all sources); then min(flat, Cap)
  Speed:        if any zero → 0; else if any halve → FloorDiv(flat, 2); never below 0
  ```

  - `RollModeOf` is D-15: any `Advantage` and any `Disadvantage` gives `Normal`; otherwise whichever is present; otherwise `Normal`.
  - "What would +1 change" is the same fold on `Effects.Apply(state, …)`, a hypothetical state. No separate API.

- [ ] **Step 1: Write the failing tests** in `EffectFoldTests`. They use fixture definitions plus fixture numbers: `test.stack` (Stack, base 10), `test.ability` (AbilityScore, base 4, cap 5), and the core `speed`.
  - Every case in Review Focus 1–3.
  - Roll mode: none → Normal; 2 advantage → Advantage; 2 advantage + 1 disadvantage → **Normal** (D-15); 1 disadvantage → Disadvantage.
  - `Flag` true when present.
  - Folding never changes the state (`Snapshot.Save` before and after is equal), and the result is independent of the order of `Effects` (a shuffled array).
  - `Effects.Apply` assigns seq 1, 2, 3; `Remove` keeps `NextEffectSeq`.
  - Persistence: round trip with two effects; each Load rejection above; version 3 refused.
  - Digest coverage: `Effects` and `NextEffectSeq` both move the digest.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "EffectFoldTests|PersistenceTests|StateDigestTests"`. Expected: FAIL.
- [ ] **Step 3: Implement.**
- [ ] **Step 4: Run** `dotnet test Dungeon.sln` and `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`. Expected: PASS; `RunStreamsTests` and the decision goldens are unchanged.
- [ ] **Step 5: Commit** `git commit -m "M1-07: active effects in the run state and the fold queries (stacking, same-number, ability rule, speed, D-15 roll mode)"`

### Task 3: Triggers, hooks and the effect-aware engine

**Files:**
- Create: `src/Core/Effects/HookRegistry.cs`, `src/Core/Effects/TriggerDispatch.cs`, `src/Core/Effects/TriggerLoopException.cs`
- Modify:
  - `src/Core/Engine/Engine.cs`: add the `Apply(state, command, rules)` overload; the old overload calls it with `RulesContext.Empty`.
  - `src/Core/Engine/Events.cs`: add `EffectApplied(long Seq, string EffectId, UnitId Holder, EffectSource Source)` and `HookFired(string HookId, long Seq)`.
  - `src/Core/Effects/RulesContext.cs`: the `HookRegistry` parameter.
  - `docs/effects-vocabulary.md`: the event-to-subject table.
- Test: `tests/Core.Tests/TriggerTests.cs`, `tests/Core.Tests/EffectsEndToEndTests.cs`

**Interfaces:**
- `public delegate GameState Hook(HookContext ctx)`, where `public sealed record HookContext(RulesContext Rules, GameState State, GameEvent Event, EffectInstance Instance, ImmutableArray<string> Args, Action<GameEvent> Emit)`.
  - A hook returns the next state and may emit events. It may not draw except through a stream it threads into the returned state.
- `public sealed class HookRegistry`:
  - `static HookRegistry Core` (empty in M1-07);
  - `HookRegistry With(string id, Hook hook)` (ids are content ids, and a duplicate throws);
  - `bool Contains(string id)`.
  - Stored as an id-sorted `ImmutableArray`, so lookup needs no `Dictionary` enumeration.
- **Event subjects:** `RuleEvents` maps each event id to the `GameEvent` type and a subject function, e.g. `unitMoved` → `UnitMoved`, subject `Unit`.
- **Dispatch:**
  1. After a command's handler runs, every event it emitted is queued in order.
  2. For each queued event with a registered id, the matching triggers are collected: instances whose definition has a trigger with that `on`, with scope `any`, or scope `holder` where the instance's holder is the subject.
  3. They are sorted by (priority ascending, source unit index with environment last, seq ascending) using a `[TotalComparer]`.
  4. Each trigger's `do` runs in order: `applyEffect` calls `Effects.Apply` (source = the instance's holder, holder = `Holder` or `Subject`) and emits `EffectApplied`; `hook` calls the registered delegate and emits `HookFired`.
  5. Emitted events are appended to the result list and the queue.
  6. Each fired trigger counts toward `MaxTriggerFirings = 64`; reaching it throws `TriggerLoopException(eventIndex, firings)`.
- `StepResult.Events` holds the command's own events, followed by the trigger-produced events in firing order.

- [ ] **Step 1: Write the failing tests.**
  - **`TriggerTests`** (Review Focus 4):
    - Four instances on the hero, each with a `unitMoved` trigger whose hook emits `HookFired(…, seq)`, are created in this order:
      - A: priority 1, source unit 1 (seq 1)
      - B: priority 0, source environment (seq 2)
      - C: priority 0, source unit 2 (seq 3)
      - D: priority 0, source unit 1 (seq 4)

      One move fires them in seq order **4, 3, 2, 1**: D (priority 0, unit 1), C (priority 0, unit 2), B (priority 0, environment, last among units), then A (priority 1).
    - Shuffling `Effects` gives the same order.
    - Scope `holder` versus `any`.
    - `applyEffect` to the subject versus the holder.
    - A hook runs, gets its args, and emits `HookFired`.
    - A self-re-applying trigger throws `TriggerLoopException` after 64 firings.
    - An event no trigger listens to changes nothing.
    - `Engine.Apply(state, command)` with no rules gives the same result as before: `MoveTests` and `DecisionTests` stay green.
  - **Load-time hooks** (Review Focus 5): an unknown hook id, event id or number target in a definition makes `RulesContext.Create` throw `ContentLoadException` with `effects/effects.json` and the path.
  - **`EffectsEndToEndTests.A_json_only_effect_changes_a_number_and_fires_a_trigger`** (the done-when):
    - Fixture JSON (no new C#) defines `test.haste`, a trait modifier `{"target":"speed","add":2}`, and `test.stride`, a trait whose trigger `{"on":"unitMoved","scope":"holder","do":[{"applyEffect":"test.haste","to":"holder"}]}`.
    - The hero holds `test.stride` with speed 6. One `MoveCommand` through `Engine.Apply(state, cmd, rules)` emits `UnitMoved` then `EffectApplied(…, "test.haste", Hero, …)`, after which `Effects.Number(rules, s, Hero, "speed") == 8`.
    - A second move gives 10, because gear and traits stack.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "TriggerTests|EffectsEndToEndTests"`. Expected: FAIL.
- [ ] **Step 3: Implement**, and update the vocabulary document.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`, the Godot build and the smoke build. Expected: PASS.
- [ ] **Step 5: Commit** `git commit -m "M1-07: trigger dispatch in priority, source unit and creation order; C# hooks by id; effect-aware Engine.Apply"`
- [ ] **Step 6 (controller):** README layout (`src/Core/Effects/`), the Godot hand-check (behaviour unchanged), issue #10, push, and open the PR. The PR asks the other developer to approve `docs/effects-vocabulary.md`.
