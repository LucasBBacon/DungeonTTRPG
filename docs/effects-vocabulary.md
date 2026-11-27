# Effects vocabulary

How rule exceptions are written as data (MAIN 6.4, M1-07). Every exception in the game, from a condition to a
weapon verb or a trait, is an **effect**: a set of **modifiers**, which change a number, a roll's mode or a flag,
and **triggers**, which react to a rule event. The engine folds modifiers and fires triggers the same way, whatever
the source. New content should need new data, and only rarely new code; what data cannot say is a C# **hook**
named from data by id (MAIN 6.5).

This document is the contract between content and the rules engine. It names every key the loader accepts, how
each source stacks, the order triggers fire in, and what is reserved for later steps.

Spec references: MAIN (main design), SM (stat model), CO (conditions), AB (abilities), W (weapons), AR (armour),
DH (damage and HP), CN (consumables), ITEMS (items).

## 1. Where effects live

Effect definitions are in `content/effects/effects.json`:

```json
{"effects": [
  {"id": "trait.fleet", "kind": "trait", "modifiers": [{"target": "speed", "add": 1}]}
]}
```

A definition is a template. A unit gains an **instance** of it (a holder, a source and a creation number) when
something applies it; the instances are part of the run state, the definitions are content. Nothing derived is
stored: every number is folded on demand from the base and the active instances (SM 6, 9), so the display can ask
"what would this change?" of a hypothetical state.

The loader is strict, like every content file (`content/README.md`): no unknown keys, no repeated keys, integers
only, ASCII rules strings, and every error names the file and the JSON path, such as
`effects/effects.json` at `$.effects[0].modifiers[1]`.

## 2. The definition schema

| Key | Required | Value |
|---|---|---|
| `id` | yes | A content id, `[a-z0-9_.:-]`, unique in the file. |
| `kind` | yes | The source kind (section 4): `condition`, `trait`, `gear`, `item`, `ability` or `environment`. |
| `modifiers` | no | A list of modifiers (section 3), in order. Absent means none. |
| `triggers` | no | A list of triggers (section 6), in order. Absent means none. |

## 3. Modifiers

A modifier is `{"target": ..., <one operation>}`. The target names a derived number (section 5), a roll target or
a flag target. It has **exactly one** operation key; none or two is a load error at the modifier's path.

| Operation | Example | Meaning |
|---|---|---|
| `add` | `{"target": "speed", "add": 2}` | Adds a signed integer to the number. `-2` is a penalty. |
| `halve` | `{"target": "speed", "halve": true}` | Halves the number after the flat changes, rounding down. At most one halving applies, however many there are. The value is always `true`. Only a `Speed`-rule number (section 5.1) takes it. |
| `zero` | `{"target": "speed", "zero": true}` | Sets the number to 0, overriding every other modifier. The value is always `true`. Only a `Speed`-rule number takes it. |
| `rollMode` | `{"target": "attacksAgainst", "rollMode": "advantage"}` | Gives a roll `advantage` or `disadvantage`, folded by D-15 (section 5.4). `normal` is not written: it is only ever a fold result. |
| `flag` | `{"target": "unit", "flag": "hidden"}` | Sets the named boolean flag on the holder while the effect is active. The target is always `unit`. |

A disadvantage on the holder's own attacks:

```json
{"target": "attackRoll", "rollMode": "disadvantage"}
```

The roll targets in M1-07 are `attackRoll` (attack rolls the holder makes) and `attacksAgainst` (attack rolls made
against the holder).

Targets are checked against the code registries when the rules context is built (MAIN 6.5), so a misspelt target
fails at startup with the file and field named, never in play:

- `add`, `halve` and `zero` must name a derived number (`DerivedNumbers.Core`). `halve` or `zero` on a number whose
  rule is not `Speed` is a load error at `$.effects[i].modifiers[j].halve` (or `.zero`).
- `rollMode` must name a roll target (`RollTargets.Core`).
- `flag` must have the target `unit` (`Flags.Targets`), and its flag must name a registered flag (`Flags.Core` in
  `src/Core/Effects/Registries.cs`), or it fails at `.target` or `.flag`. M1-07 registers no flag names, so every
  `flag` modifier fails at load until the step that reads the flag registers it (section 9).

## 4. Source kinds and stacking

The effect's `kind` sets how its modifiers stack on a number.

| Kind | Used for | Stacking on a number |
|---|---|---|
| `condition` | Prone, Frightened, Bleeding ... (CO) | **Same-number rule** (CO 6.7): only the best bonus and the worst penalty apply; a bonus and a penalty both apply. |
| `trait` | Species, class and feat traits | Stacks: every modifier sums (SM 9). |
| `gear` | Worn or wielded weapons, armour, shields | Stacks. |
| `item` | Used or carried items, such as a potion's lasting effect | Stacks. |
| `ability` | The lasting effect of an ability | Stacks. |
| `environment` | Terrain and rooms; there is no source unit | Stacks. |

The two groups then **add**: the folded condition total plus the summed total of everything else. So gear and
traits stack with conditions, as CO 6.7 says ("Gear and traits still stack as the stat model says").

Worked examples, all on `speed`:

| Active modifiers | Change |
|---|---|
| condition +2, condition +2 | +2 (best bonus only) |
| gear +2, gear +2 | +4 (sums) |
| condition +2, condition -3 | -1 (best bonus +2, worst penalty -3) |
| gear +2, condition +2, condition -1 | +3 (gear +2, plus conditions +2 - 1) |

## 5. Derived numbers

A derived number is a row in a code table (`DerivedNumbers.Core` in `src/Core/Effects/Registries.cs`): an id, a
**number rule**, a **base** per unit and, for an `AbilityScore` number only, a cap. All arithmetic is integer,
rounding down (SM 3).

The base is a pure function of the unit (`NumberBase` in `Registries.cs`), not one constant: nothing derived is
stored (SM 9), and most SM 6 numbers start from other numbers. A base reads what the unit stores and may read the
same unit's other derived numbers, already folded with their effects (max HP reads Constitution; a save reads two
abilities). A number whose base is the same for every unit, such as `speed`, uses a constant. A base that reads
itself, directly or through other bases, throws with the path (`a -> b -> a`); it is a defect in the table, caught by
tests.

Only an `AbilityScore` number may have a cap: the ability +5 is the only cap the designs give (SM 5.1, 5.5). The
registry rejects a cap on a `Stack` or `Speed` number at startup. The cap binds the base plus the bonus, and the
penalty applies after it (SM 5.1: "The hard cap is +5 after all bonuses"; "A bonus and a penalty both apply"). With
base 5, a +2 bonus and a -2 penalty, the result is min(5 + 2, 5) - 2 = 3: the bonus at the cap is wasted, and the
penalty shows.

### 5.1 Number rules

| Rule | Fold |
|---|---|
| `Stack` | The source-kind regime of section 4, added to the base. Most numbers (SM 9: "Other derived numbers stack normally unless a rule above says otherwise"). |
| `AbilityScore` | Overrides the kind regime for this number: **across all sources**, the base plus the highest bonus, capped, plus the worst penalty: min(base + bonus, cap) + penalty (SM 5.1). A trait +2 and a gear +1 give +2; penalties -1 and -2 give -2. With base 4, a +3 bonus and cap 5, the result is 5, not 7; add a -1 penalty and it is 4. No floor: SM 12 leaves open how far an effect can push an ability below -1. |
| `Speed` | The `Stack` fold for the flat changes, then the speed fold below. |

### 5.2 The speed fold (CO 6.7)

1. Flat changes first: the base plus the `add` modifiers, folded per section 4.
2. Then at most one halving: if any `halve` is active, halve once, rounding down (7 becomes 3). Two halvings
   still halve once.
3. Speed 0 overrides everything: if any `zero` is active, the result is 0, whatever else applies, including an
   `add` applied later.

### 5.3 The M1-07 rows

| Id | Rule | Base | Cap | Source |
|---|---|---|---|---|
| `speed` | `Speed` | 6 | none | SM 6 |

M1-08 adds the six abilities (`AbilityScore`, cap 5, base the unit's stored modifier) and the other SM 6 numbers,
whose bases read the abilities and proficiency through `NumberBaseArgs.Number`. The floors some of them have
(Recovery minimum 1, DH 612) are M1-08's and M1-09's to shape; this step adds no floor field.

### 5.4 Roll mode and D-15

**D-15 (resolved 2026-11-28, client): the 5E rule.** Any number of advantage sources give advantage, any number of
disadvantage sources give disadvantage, and one of each cancels to a normal roll, whatever the counts (CO 6.7).

| Active `rollMode` modifiers on a roll | Mode |
|---|---|
| none | normal |
| 2 advantage | advantage |
| 2 advantage, 1 disadvantage | **normal** |
| 1 disadvantage | disadvantage |

Weapons 7.5 used to say "net them (one cancels one)"; it now says "fold them (conditions 6.7: any advantage and any
disadvantage cancel)", and Overlap and Vex read the folded mode. The roll mode does not depend on source kind.

## 6. Triggers

A trigger reacts to a rule event (MAIN 6.4):

```json
{"on": "unitMoved", "priority": 0, "scope": "holder", "do": [{"applyEffect": "cond.winded", "to": "holder"}]}
```

| Key | Required | Value |
|---|---|---|
| `on` | yes | A registered rule-event id (section 6.4). M1-07 registers `unitMoved` (a unit finished a move; the subject is that unit). |
| `priority` | no | An integer, default 0. **Lower fires first.** |
| `scope` | yes | `holder`: fires only when the event's subject is the effect's holder. `any`: fires for every subject. |
| `do` | yes | A non-empty list of actions, run in order. |

### 6.1 Actions

Each action has **exactly one** of `applyEffect` or `hook`.

Apply an effect of this file, to the holder of the triggering effect or to the event's subject:

```json
{"applyEffect": "cond.winded", "to": "subject"}
```

`to` is required on this form (`holder` or `subject`), and `applyEffect` must name an effect in
`effects/effects.json`; a missing one fails at load, at `$.effects[i].triggers[j].do[k].applyEffect`.

Call a C# hook, with optional string arguments:

```json
{"hook": "push", "args": ["1"]}
```

`to` belongs only to the first form and `args` only to the second; either on the wrong form is a load error.

Each action reports itself as an event. `applyEffect` gives the effect to its target with the triggering effect's
holder as the source and raises `EffectApplied(seq, effectId, holder, source)`, where `seq` is the new instance's
creation number. `hook` raises `HookFired(hookId, seq)`, where `seq` is the triggering instance's, and then calls
the hook; the events the hook raises follow `HookFired`.

### 6.2 Trigger order (AB 14.4)

When several triggers answer one event they fire in this order:

1. **Priority**, lower first.
2. **Source unit order**: the source unit's position in the run's unit list, which is sorted by unit id.
   Environment sources have no unit and fire **after** every unit source.
3. **Creation order**: the instance's creation number, which starts at 1 and is never reused.

Together these are a total order, so the firing order never depends on how instances happen to be stored. (If
one definition has two triggers on the same event, they fire in file order.)

The triggers that answer an event are the instances active when the event is reached in the queue; an instance
applied while that event is being answered first answers the events after it.

### 6.3 Chains and their bound

An action can raise further events (applying an effect raises one, a hook may raise others). Within one command,
events are processed first in, first out: an action's events join the end of the queue. A command may fire at most
**64** triggers (`Effects.MaxTriggerFirings`); past that the engine throws `TriggerLoopException`, a defect, so a
trigger that re-fires itself is caught by tests rather than hanging the game.

A command's result lists its own events first, then the trigger-produced events in firing order. A rejected
command fires nothing.

### 6.4 Rule events and their subjects

A rule event is a row in a code table (`RuleEvents.Core` in `src/Core/Effects/Registries.cs`): the id a trigger's
`on` names, the engine event that raises it, and its **subject**, the unit a `holder`-scope trigger compares with
its holder and `"to": "subject"` gives an effect to.

| Event id | Raised by | Subject |
|---|---|---|
| `unitMoved` | `UnitMoved` (a unit finished a one-square move) | The unit that moved |

`EffectApplied` and `HookFired` are engine events but not rule events in M1-07: no trigger can name them. Later
steps add rows (section 9).

## 7. Hooks

What data cannot express is a C# hook (MAIN 6.5; AB 14.4), registered in a code table (`HookRegistry.Core`) under
a content id and named from data with `{"hook": "<id>", "args": [...]}`. A hook is a function from a
`HookContext` (the rules, the current state, the event, the triggering instance, the args and an `Emit` callback)
to the next state. It is pure: it may not draw except through a stream it threads into the state it returns. The
engine calls it when its trigger fires, in the trigger order of section 6.2, and any events it emits join the queue
of section 6.3. An unknown hook id fails when the rules context is built, naming the file and field. M1-07 ships no
production hooks; later steps add them, and tests register their own.

## 8. Reserved

These keys and ideas are named now so content does not invent its own, and are refused or absent until their step:

| Item | Status | Arrives |
|---|---|---|
| `diceBonus` modifier | Refused at load (`$.effects[i].modifiers[j].diceBonus`). Its fold will be "only the highest dice bonus applies to a given roll" (CO 6.7). | M1-09 or later, when dice exist |
| Durations and clocks (turn start and end, `combatOnly`, refill tags) | Not in the schema: an instance lasts until it is removed. | M1-10 (clocks), M1-12 (condition durations) |
| Condition application pipeline, relations and exclusivity | Not in the schema; conditions are plain `condition`-kind effects for now. | M1-12, M1-13 |
| Damage riders (AB 9.1) | Not in the schema. | M2 |

## 9. Which later step uses what

| Step | Vocabulary rows it uses |
|---|---|
| M1-08 Stats and contests | `add` on new numbers; the `AbilityScore` rule and its cap for the six abilities; the `Stack` rule for the other SM 6 numbers, with per-unit bases that read the abilities; `rollMode` on `attackRoll` and `attacksAgainst`, folded by D-15; new roll targets for saves and skills. |
| M1-09 Damage and HP | `add` on HP numbers (max HP, Recovery); the reserved `diceBonus` becomes live; resistances and weaknesses as `flag` or new numbers. |
| M1-10 Turns, lifecycle and death | New rule events (turn start, turn end, combat start and end); durations (section 8). |
| M1-11 Abilities, reactions and areas | `ability`-kind effects; `applyEffect` actions; hooks for reactions; new events for ability use steps. |
| M1-12 Conditions core | `condition`-kind effects and the same-number rule; `applyEffect` to `subject`; trigger order and the chain bound. |
| M1-13 M1 conditions, forced movement | `zero` and `halve` on `speed` (Grappled, Prone); `rollMode` (Prone, Frightened, Hidden, Dodging); `flag` (Incapacitated); hooks for push, pull and shift; `unitMoved` triggers. |
| M1-14 Universal and skill actions | `rollMode` from Help; `flag` from Dodge and Hide; `unitMoved` with `scope: any` for opportunity attacks. |
| M1-15 Items and inventory | `item`-kind effects; numbers that must read without conditions (capacity reads Strength without conditions). |
| M1-16 Weapons and attacks | `gear`-kind effects; `rollMode` (ranged in melee), folded by D-15 for Overlap and Vex; hooks for verbs (Push, Topple, Cleave ...). |
| M1-17 Armour, AC and shields | `gear`-kind `add` on AC; the gear roll-mode cap (AR 8.1) as a hook or rule over the `rollMode` fold; `flag` (Raise). |
| M1-18 Healing, rests and supply | `item`-kind effects; hooks for healing; rest events. |

## 10. Approval

The other developer approves this vocabulary in the M1-07 pull request.
