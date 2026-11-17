# Conditions: Design

Date: 2026-11-16
Status: Approved, 2026-11-16
Parents: `2026-11-15-dungeon-rpg-design.md` (main design),
`2026-11-16-character-stat-model-design.md` (stat model) and
`2026-11-16-ability-resource-model-design.md` (ability model)

Items marked **(agreed)** were decided by the client on 2026-11-16, either
directly or by choosing the lead's recommendation. Items marked **(lead's
call)** were filled in by the design lead while writing this document and
are confirmed when it is approved. Items marked **(settled)** restate a rule
from a parent document. Items marked **(change)** amend an approved document
and are listed in section 24. Once approved, every item here is binding on
the builders.

Numbers in this document are illustrative unless a rule says otherwise. The
builders own them (section 23).

## 1. Intent

Conditions are the states a unit carries between one rule event and the
next: stunned, burning, hidden, rallied, weary. The parents name many of
them (riders, Rally, Hide, Frighten, grapples, downing) and leave the list
and its rules to this document. **(settled)**

The system should give fights texture and give every damage type its
identity, without burying a turn in saves, icons and bookkeeping, and
without ever letting control decide a permadeath run on its own. **(agreed)**

## 2. Goals, non-goals, anti-goals

### Goals

- One machinery for every condition, harmful or beneficial, so new content
  is data. **(settled, main design 6.4)**
- Control matters but never decides alone. The hero loses at most 1 turn in
  a row and at most 2 turns per fight to enemy hard control. **(agreed)**
- The number on an icon always means the same thing: this unit's turns
  left. **(agreed)**
- Every harmful condition longer than 1 turn has an answer in the fight
  besides waiting. **(lead's call)**
- At most four things to read per unit. **(settled, ability model 11.4)**
- Combos between damage types and terrain come up often enough to learn,
  through fixed map squares, never through spreading surfaces. **(agreed)**

### Non-goals

- Spreading, evolving or player-placed ground surfaces. Fixed map squares
  may apply conditions (section 11); they never spread or change, with the
  single exception of oil burning away. **(agreed)**
- 5E's condition list or wording. We borrow its shape. **(settled)**
- Random condition tables (random Confusion, berserk rolls). **(lead's
  call)**
- Monster conditions that the one-step AI scorer cannot value. **(lead's
  call)**

### Anti-goals

- **Stun-lock.** A unit losing turn after turn to control, the hero above
  all, under permadeath.
- **Runaway stacking.** Conditions that grow with every hit. **(settled,
  stat model anti-goal)**
- **Dead numbers.** Plain flat +1 buffs that change no decision. **(settled,
  stat model anti-goal)**
- **Bookkeeping.** A countdown or interaction the player must track that
  the game could show.
- **Icon soup.** More conditions on screen than a player can read.
- **Protected content.** Names and mechanics come from SRD 5.1 or 5.2 with
  attribution, or are original. **(settled)**

## 3. Constraints

From the parents:

- Integer arithmetic, rounding down; deterministic; nothing derived is
  stored; rules in the pure C# core; content in JSON. **(settled)**
- Every rule exception is a modifier or a trigger in the effects system.
  **(settled)**
- The turn is movement, one action, one bonus action, one reaction.
  **(settled)**
- One d20 per contest; no opposed rolls. **(settled)**
- Riders: at most one per target per ability use; at-will riders at most
  once per turn per attacker; rider damage never triggers riders; a rider is
  not attempted on a target that already has that condition. **(settled,
  ability model 9.3)** This document adds the psychic exception and
  `worsen` (6.2, 6.4; change C8).
- Out of combat, one round is one tick. **(settled, ability model 9.6)**
- The player controls every party member. **(settled, main design 4.2)**
- Concentration is a Constitution check, one per damage event. **(settled)**

## 4. Framework decisions

Three framework decisions shape everything else. The client chose all
three on 2026-11-16. **(agreed)**

| # | Decision | Chosen | Rejected, and why |
|---|---|---|---|
| 1 | Which clock counts a condition down | **D3:** conditions on units count down on the wearer's turns; windows run on the source's turns; an own-turn rule for hard conditions | Source clock with a skip rule (a stun's cost still depended on initiative order); wearer clock for everything (allies' "until my next turn" windows broke) |
| 2 | Re-application and intensity | **S2:** one instance per condition per unit, with an optional value | Binary only (damage over time had nowhere to keep its strength); additive stacks (contradicts settled rider rules and the runaway-stacking anti-goal) |
| 3 | Limits on control | **C-B:** Steeled, plus party-facing hard control is 1 turn only, a per-fight cap, composite lockouts count as hard, depth gates for monster hard control, and boss resolve then Unyielding | Steeled alone (two stunners could still take every other turn); no hard control on the party at all (monsters felt toothless) |

The client also answered three questions **(agreed)**:

1. **Control against the hero:** monsters may take one turn at a time, from
   floor 2 elites and bosses only, under every guard in section 7.
2. **Downing leaves a mark:** a companion that gets up gains Scarred
   (section 14, catalogue I). Scarred ships in M2.
3. **Terrain may apply conditions,** from fixed squares only (section 11).
   The condition squares (water, web, fire, oil slick), their condition
   data, and the hazard-edge, landing and drag-into-hazard rules ship in
   M2. In M1, pushes and drags stop before hazard squares.

## 5. Durations and the clock

### 5.1 Duration models

**(agreed for the clock; the rest lead's call)**

| Model | Meaning | Used for |
|---|---|---|
| `turns:N` | Wearer clock, counted by the test in 5.2. The own-turn rule (5.2) applies to hard conditions. | Most conditions |
| `untilSourceTurnStart` / `untilSourceTurnEnd` | A window the source opens, on the source's clock (5.6). | Marks, guards, Dodging (with the wearer as source) |
| `saveEnds {save, cap}` | A repeat save at the end of the wearer's turn, against the difficulty recorded at application. The cap, in wearer turns, is mandatory. The save may not be one the condition itself auto-fails. | Hard control on enemies; strong mind effects |
| `charges:N {on, fallback}` | Used up by a named event. The fallback is mandatory and may be any duration model except `charges`; it is validated against the token band. | Tokens |
| `decay` | The value acts at turn start, then drops by 1 at the end of each wearer turn; the condition ends at 0. It lasts at most `value` turns, so validation caps the starting value at the tier's maximum turns. | Envenomed |
| `trigger {event, cap}` | Ends on an event, optionally with a cap in wearer turns. | Hidden, Prone, Grappled, Asleep |
| `sustained {max}` | Ends with the source's concentration, or after `max` of the **wearer's** turns. | Spell control and buffs |
| `staged {stages, save, cap}` | Calcifying only (6.5). | Calcifying |
| `untilRest` / `untilCamp` | Lingering. Counts only rest and camp events, never ticks. | Exploration-scale conditions |

- **Combined terminators:** `duration` may be a list. The instance ends on
  whichever terminator comes first, for example
  `[{"sustained": {"max": 2}}, {"saveEnds": {"save": "will", "cap": 2}}]`.
  Each terminator is validated against the tier band. **(lead's call)**
- **Out-of-combat durations:** a condition may give a separate
  `outOfCombat` duration in ticks. It is validated against the
  out-of-combat limit (5.5), not against the in-combat bands. **(lead's
  call)**
- **Flags:** `combatOnly` (ends at combat end) and `sourceTied` (ends when
  the source is downed, dies or leaves).
- **Which clock:** every condition placed on a unit, sustained ones
  included, counts on the wearer's turns. The source clock of ability
  model 9.6 (`rounds:N`, counted at the end of the source's turn) remains
  only for the lifetime of an aura, of a concentration effect itself, and
  of effects that are not on a unit. `rounds:N` is not a condition
  duration. **(agreed; change C1)**

### 5.2 Counting and the own-turn rule

**(agreed for the rule; the counting mechanics lead's call)**

Each turn has three phases: the turn-start step, the Act phase (the unit's
own actions and movement) and the turn-end step (5.4). The core keeps one
global phase counter, which advances by 1 at the start of every phase of
every unit, in combat and out. Each instance stores the counter's value
when it was applied (`appliedPhase`); an instance that replaces another
(6.3) takes a fresh one.

- **Counting test:** a wearer turn counts toward an instance only if the
  instance's `appliedPhase` is earlier than that turn's Act phase. So an
  application made during the wearer's own turn-start step (an aura
  reapplying Silenced, a spread from a neighbour's Burning, Engulfed's
  damage) counts that turn; one made during its Act phase or turn-end step
  does not. The same test decides which turn ends count for `turns`
  countdowns, `saveEnds` rolls and caps, `trigger` caps, `sustained` maxima
  and fallbacks.
- **Own-turn window:** the wearer's Act phase, from the end of its
  turn-start step to the start of its turn-end step. Out of combat it is
  the step phase of the tick (5.5).
- **The own-turn rule:** a hard condition applied in the own-turn window
  (a trap sprung mid-move, for example) ends at the end of that turn,
  whatever its duration model, and Steeled follows. If it was sustained,
  the target is released from the concentration effect; the caster's
  concentration is not broken. Without this rule, a trap on the hero's own
  turn would take the rest of that turn and the whole next one.
- **Off-turn:** anything applied during the wearer's turn-start step, its
  turn-end step, another unit's turn, or a boss action is off-turn. Boss
  actions resolve only after the acting unit's turn-end step has completed
  (5.4), so a boss action at the end of the hero's turn costs the hero its
  next turn. **(lead's call)**
- An own-turn hard condition costs no turn start, so it does not count
  toward the per-fight cap (5.7). **(lead's call)**

### 5.3 Tier bands

Enforced by content validation. **(lead's call; numbers are the rule's
bounds, not illustrative)**

| Tier | Default | Maximum |
|---|---|---|
| Token | Until used | Fallback of 3 turns |
| Minor | 2–3 turns | 3 turns |
| Major | 1–2 turns | Save-ends or trigger with cap 3. **Party-facing turn-part majors** (7.2) are `turns:1`. |
| Hard | 1 turn | Save-ends or trigger with cap 2, **enemy-facing only**. Party-facing hard control is `turns:1`, or a trigger with cap 1. **(agreed)** |
| Beneficial | 1–10 turns | Over 10 turns on an ally needs concentration **(settled, ability model 9.6)** |

- **Party-facing** means the target is on the party's side when the
  condition is applied. A condition used both ways states each duration as
  an application-level override (17.1), and each is validated against its
  band.
- A sustained harmful condition may last up to its tier's cap in wearer
  turns. Beyond that it needs a repeat save.
- **Self-ended triggers:** a trigger the wearer can end with its own
  movement or one action (stand, Escape, pick up a weapon, step away) needs
  no cap. This covers Prone, Grappled, Disarmed and Teetering.
- A beneficial `combatOnly` token needs no fallback (Death-warded).
- Out-of-combat durations are checked against 5.5, not this table.
- Self-only toggles (ability model 6.1) are exempt from the 10-round rule,
  because 9.6 bounds effects placed on allies. **(change C5)**

### 5.4 Turn order of condition steps

**(lead's call)** One fixed list. Each numbered item runs over instances in
`seq` order.

**Turn-start step**

1. The turn budget resets (movement, action, bonus action, reaction).
2. Cooldowns count down (ability model 12.1).
3. Harmful ticks resolve as one batch (6.8), Engulfed's damage included.
4. **Zero-hit-point check.** A unit at 0 hit points is downed or dies now,
   and the rest of its turn-start step is skipped. A downed unit still runs
   its turn-end steps (8). **Gated on the death and healing design (change
   CN-15):** the tick batch's save chain (9.3) runs where the damage lands,
   before hit points are lowered, so a saved unit never reaches this check
   at 0.
5. Beneficial ticks (Regenerating).
6. Aura pulses that reach the unit (pipeline step 2; aura immunity, 7.3).
7. Start-of-turn triggers and checks: Burning's spread, Tormented's line of
   sight.
8. The lost-turn check (5.7).

**Act phase:** the unit's turn. Conditions gained here are governed by the
own-turn rule (5.2).

**Turn-end step**

1. Repeat saves: `saveEnds` and `staged`.
2. Decay drops.
3. Countdowns and expiry. Each instance that ends fires its `onEnd`
   relations, and the Steeled rule (7.2), immediately, before the next
   instance is processed.
4. Trigger-end checks: Hidden's line of sight, Engulfed's expulsion.
5. Terrain `onTurnEnd` applications (11.2).

**After the turn-end step,** boss actions resolve. A round ends after the
last initiative slot's turn-end step and the boss actions that follow it.

### 5.5 Out of combat

- One round is one tick. **(settled)**
- **Every unit in the loaded level ticks,** including monsters the party
  fled. A tick runs every unit's turn-start step in unit order, then the
  step phase (the hero's step or action, follower moves and monster
  behaviour), then every unit's turn-end step in unit order. The step phase
  is every unit's Act phase for 5.2. **(lead's call)**
- Followers' pathing avoids squares that apply harmful conditions.
  **(lead's call)**
- Harmful ticks out of combat cannot take a unit below 1 hit point.
  Condemned's burst counts as a tick for this rule. **(lead's call)**
- **Out-of-combat limit:** a non-lingering harmful condition lasts at most
  20 ticks out of combat. **(lead's call)**
- **Combat start:** an instance on an `outOfCombat` duration switches to its
  in-combat duration, counted from then, if that is shorter. An Asleep unit
  loses Asleep (reason `combatStart`, no Steeled) and gains Surprised. The
  monster behaviour "asleep" of main design 4.3 is a behaviour, not this
  condition, and is renamed "dormant" **(change C15)**.
  `hardTurnsLostThisFight` resets.
- **Combat end:** the current unit's turn-end step completes, then
  `combatOnly` instances end (reason `combatEnd`). A downed companion gets
  up only when the party wins **(settled, main design 4.2)**; after any
  other end it stays Downed until Medicine gets it up.
- A 20-tick short rest outlasts every turn-counted condition. **(follows
  from ability model 15.1)**

### 5.6 Windows

**(lead's call)**

- `untilSourceTurnStart` ends when the source's next turn-start step
  begins; `untilSourceTurnEnd` ends when the source's next turn-end step
  completes. A window opened during the source's own turn runs to its
  following turn.
- If the source is dead, downed, gone or not in the initiative order, the
  window ends at the end of the current round (5.4). A source that joins
  mid-round opens windows normally.
- Out of combat the source has a part in every tick (5.5), so a window
  lasts until the next tick.

### 5.7 Lost turn starts

**(agreed that the cap exists; the definition lead's call)**

- At the end of the turn-start step (after ticks and triggers), a unit has
  **lost its turn start** if it holds a hard instance, or an instance
  marked hard as a composite lockout (7.2), whose source is hostile to it
  or is the environment (11.6).
- Surprised, Downed, and instances from the unit itself or its allies (an
  ally freezing it to protect it, a willing Banish) never count. Nor does an
  own-turn hard condition (5.2), which never reaches a turn start. Nor do
  morale states, which are not in the hard tier (enemies design 14.9).
- Each lost turn start adds 1 to `hardTurnsLostThisFight`. When it reaches
  2, Unyielding (`combatOnly`) is applied at once. The hard instance
  already held runs its course.
- The counter resets at combat start.

## 6. Intensity and re-application

**(agreed for 6.1–6.5; the rest lead's call)**

### 6.1 One instance

1. One instance per condition per unit. Conditions of different kinds
   coexist.
2. An instance may carry a value (a damage over time strength, an armour
   class penalty, a stage). The value comes from the rider band table
   (ability model 9.3) or the source's data, capped by the condition's
   `valueCap`.

### 6.2 Riders

- A rider is not attempted on a unit that has the condition it would apply.
  **(settled)** Transforms (9.1) are taken into account when checking this.
- **Psychic exception:** its rider is attempted if the target is
  concentrating or is not already Unfocused. **(change C8)**
- An application with `worsen` bypasses this gate (6.4).

### 6.3 Re-application by non-riders

- Durations are compared by **effective end**: the wearer turn end at which
  the instance would end, using the cap for `saveEnds`, `trigger` and
  `sustained`, and infinity for `untilRest` and `untilCamp`. The existing
  instance uses what it has left; the new one uses its full duration under
  the counting test (5.2).
- A higher value replaces the instance.
- An equal value replaces it only if its effective end is later.
- A replacing instance takes everything from the new application: value,
  duration, `seq`, `appliedPhase`, source, recorded difficulty,
  `sustainedBy` and binding.
- Otherwise it has no effect, and the preview says so ("No effect: already
  Burning").
- The `noRefresh` intensity flag makes every re-application have no effect
  (Condemned).
- Nothing adds to a value or extends a duration, except 6.4 and 6.5.

### 6.4 Worsen

- Strength grows only through an explicit `worsen:N` property.
- An application with `worsen` bypasses the rider gate (6.2). If the target
  already has the instance, its value rises by N, up to `valueCap`, and its
  duration is unchanged. If it has none, the condition applies at its base
  value with no worsen bonus.
- `worsen` is allowed only on limited or cooldown abilities. Content
  validation enforces this.

### 6.5 Accumulating levels and the staged exception

- Only lingering conditions accumulate levels, with a `levelCap` of 2–3 per
  condition (Weary). For them, this rule replaces 6.3: a new application
  adds one level, up to the cap. A level gained by an existing instance is
  not a new lingering condition for the cap in section 14.
- **Staged exception (lead's call):** a `staged` condition has a fixed
  number of stages (at most 3) set in its data. It advances one stage only
  on a failed repeat save of its own, never by re-application, which has no
  effect. Its last stage applies a named hard condition, which enters the
  pipeline at step 2 (9.2) with the save already made. Calcifying is the
  only staged condition; validation rejects others.

### 6.6 Hard exclusivity

A unit under a hard condition, an instance marked hard as a composite
lockout, or Surprised, cannot gain another hard condition (reason
`alreadyControlled`). Morale states are not hard and are not blocked by
it, nor by Steeled (enemies design 14.9).

### 6.7 The same-number rule

For modifiers that come from conditions **(change C3)**:

- only the worst penalty and the best bonus to each derived number apply; a
  bonus and a penalty both apply;
- only the highest dice bonus applies to a given roll;
- advantage and disadvantage follow the 5E rule: any number of sources give
  one, and one of each cancels;
- speed: flat changes first, then at most one halving; speed 0 overrides
  everything;
- crawling while Prone and dragging a held unit are movement-cost
  multipliers, applied after the single halving; standing costs half the
  current effective speed, minimum 1. **(lead's call)**

Gear and traits still stack as the stat model says.

### 6.8 Damage over time

- Values are capped by proficiency band (Bleeding, illustrative: 2/4/6).
- **One batch:** at turn start, harmful ticks resolve one at a time in
  `seq` order, each through the ladder, temporary hit points and
  `damageType` ends. The results are summed. If the sum exceeds the cap (a
  quarter of the unit's maximum hit points, minimum the largest single
  tick), the later ticks in `seq` order are reduced first.
- The batch is **one damage event** (9.3) for concentration, for waking an
  Asleep unit and for Shielded. **(change C10)**
- **Ticks are not hits.** They never trigger riders **(settled, ability
  model 9.3)**, never pay out Oiled and never burn away an oil square.
- Taxes a condition charges for the wearer's own action (Shocked, Hobbled)
  force no concentration check.

## 7. Tiers and control limits

### 7.1 Tiers

**(lead's call, within the agreed C-B framework)**

| Tier | Definition | Who may apply it |
|---|---|---|
| Token | One stored payoff, used up by an event | Anyone |
| Minor | A tax, or a shift in one number | Anyone, including at-will riders |
| Major | Removes one part of the turn, or one category of options | Limited abilities, critical hits, weak targets; at-wills only rarely (ability model 9.4); plus the settled universal skill actions (grapple, Frighten), which are skill contests rather than riders |
| Hard | Removes the whole turn, or takes over choices (`agency`). Morale states (Routed included) are not in this tier (enemies design 14.9). | **Only** limited abilities, monster `uses` or `cooldown` abilities, and traps and set pieces (11.6). Never at-wills, reactions or terrain squares. |

An ability with a property that can transform a condition into a hard one
(`freezing`) counts as hard-capable for the depth gates and the hard row
above. **(lead's call)**

### 7.2 Guards against stun-lock

**(agreed; mechanics lead's call)**

- **Steeled:** when any hard instance (including one marked hard as a
  lockout) or Surprised ends, for any reason except `combatStart` (5.5),
  the unit gains Steeled
  directly (pipeline step 9). It is immune to the hard tier until the end
  of its next turn-end step. This is an engine rule, not a relation in the
  data.
- **Party-facing hard control** lasts 1 turn only (5.3).
- **Per-fight cap:** a unit that has lost 2 turn starts (5.7) in one fight
  gains Unyielding for the rest of that fight. This applies to every unit,
  party and monsters alike.
- **Composite lockout:** each condition declares what it `denies`:
  `action`, `bonus`, or `oneOf` (Dazed: the unit keeps one of the two). At
  step 2, and again at the step 7 re-check (9.2), the core takes the
  incoming condition plus every existing instance whose effective end
  (6.3) is at or after the wearer's next turn end. If both the action and
  the bonus action are denied after each `oneOf` takes whichever part is
  left, the incoming condition completes a lockout. It is then treated as
  hard: hard blocks, exclusivity, the depth gates, Unyielding (which
  resists it, reason `unyielding`), Steeled and the per-fight cap all
  apply, and the instance stays marked hard for its lifetime.
- **Depth gates for monster hard control against the party:** never on
  floor 1; on floor 2, elites and bosses only. They are checked at runtime
  in step 2 and the step 7 re-check, so a transform result (a Soaked hero
  frozen on floor 1 gets the fallback, Chilled) and a lockout built from
  monster majors are caught too. Content validation also checks the data.
  Traps follow the same gates (11.6). "Elite" means `rank: elite` on the
  composed source unit, read at application (enemies design 7.1).
- **Composure for the party (lead's call):** after a turn-part major
  (Dazed, Distracted, Nauseated, Hampered, Silenced) ends on a party unit,
  it is immune to hostile turn-part majors until the end of its next
  turn-end step. Without this, two cooldown bruisers could Daze the hero
  every turn.
- **Bosses:** resolve pips, then Unyielding's downgrades and composure
  window (12.3).

### 7.3 Further limits

**(lead's call)**

- **Auras:** a unit that succeeds on its save against an aura's condition
  is immune to that aura for the rest of the fight.
- **Agency** (the condition chooses the unit's actions) is enemy-only,
  enforced by validation, because the player controls every party member
  (main design 4.2). Monster versions are written directly in their
  restriction form: a monster's "dominate" applies Charmed.

## 8. Ending conditions and cures

**(lead's call unless marked)**

Every harmful condition that can last more than 1 turn declares an in-fight
end besides waiting. Validation enforces this. There are two exemptions:
morale states (Emboldened, Demoralised, Routed, Cowed; enemies design
14.9), and lingering conditions, which end at rest.

| Cure tag | Cost | Ends |
|---|---|---|
| `heal` | Any hit point gain | Bleeding, Condemned |
| `treat` | **Medicine Treat (change C4):** an action, adjacent. One Medicine roll is compared with each curable instance's recorded difficulty, and every instance the roll meets ends. The action is spent either way. | Every `poison` condition, Bleeding, Withered, Hobbled, Condemned, Calcifying at stages 1–2; Diseased and Scarred during a rest, except a `treatProof` Scarred (gated on the death and healing design; change CN-14) |
| `rally` | Rally: a bonus action, once per ally per fight **(settled)** | The `fear` tag |
| `escape` | An action: Athletics or Acrobatics against 10 + the holder's Fortitude, or against the binding's difficulty | Grappled, Restrained, Rooted, Engulfed |
| `stand` | Half the current effective speed, minimum 1 (6.7). **At speed 0, standing costs the action.** | Prone |
| `shakeOff` | Half speed (the action at speed 0). One use ends every shake-off condition on the unit. | Burning, Smouldering, Corroded (rinse), Oiled (wipe), Mired |
| `damageType:X` | Damage of type X, at least 1 | Fire ends Chilled, Frozen, Soaked and Oiled; cold ends Burning and Smouldering; bludgeoning ends Frozen; a binding's breaker type ends Restrained or Rooted |
| `damage` | Any damage | Asleep |
| `tonic` | A consumable (items design). As a bonus action it ends one minor condition; drunk as an action it ends one major. Thrown at an ally (action, range `throwAllyRange`): minor only **(change CN-17; consumables design 11)**. Given to an adjacent ally (action): minor only (items design 7.1). Never hard or lingering. A `cureImmune` list lets a condition opt out. | By tier |
| `cleanse` | Spells that declare the tiers and tags they end | By content |
| `sourceGone`, `combatEnd`, `rest`, `camp` | Automatic | By flag |
| `bossShed` | Boss action (M2) | One condition of the boss's choice |

- **Difficulty without a source:** an instance with no source (Scarred,
  terrain without a stated difficulty) uses the fixed difficulty in its own
  data or the rest's data.
- **Turns go on for everyone.** Every unit in the initiative order runs its
  turn-start and turn-end steps whatever its state: downed, banished,
  petrified or incapacitated.
- **Suppression** skips ticks, effects and triggers. It never stops
  countdowns, `saveEnds` rolls or decay. **Turn-counted** means `turns`,
  `saveEnds`, `decay`, `staged` and `charges` fallbacks.
- **Never suppressed:** Steeled, Unyielding and the engine states (18.2).
- **Downing** a companion suppresses its harmful turn-counted conditions;
  their countdowns continue. Lingering conditions stay. **(agreed)**
- A burst whose countdown reaches 0 while suppressed is held as
  `pendingBurst` and fires when the suppression ends.
- Every end emits `ConditionEnded {reason}`, shown in the log as, for
  example, "Burning ended: shaken off".

## 9. Interactions and the application pipeline

**(lead's call)**

### 9.1 Relation kinds

Relations are data, never implicit opposites.

| Kind | Meaning |
|---|---|
| `implies` | A derived, unstored flag on another condition, which ends with its parent |
| `onApply` | Gaining A applies a separate instance of B, which can outlive A |
| `onEnd {reasons, apply}` | When A ends for one of the listed reasons, it applies B |
| `cancel` | Incoming A meets existing B: both are removed and A does not apply. Declared on both sides; validation checks symmetry. |
| `override` | Incoming A removes B, then A applies |
| `suppress` | While A is present, B does nothing; B's countdown continues |
| `blocks` | While A is present, B cannot be gained |
| `transform {consumes}` | Incoming condition or damage on existing A produces R, with a mandatory fallback. `consumes` (default true) means A ends with reason `transformed`, firing no `onEnd`, when R is applied. |

### 9.2 Application pipeline

Each step that stops the pipeline emits `ConditionResisted {reason}`.

1. **Rider gate** (6.2).
2. **Blocks:** tag immunity, Steeled, hard exclusivity, the depth gates and
   party-facing limits (7.2), the composite lockout test (7.2), Unyielding
   composure and party composure, aura immunity, condition blocks, the
   lingering cap (14). No roll is made.
3. **Contest:** a save, a skill check against the defence, or automatic on
   a weak target.
   **Gear prevention (between steps 3 and 4; change AR-51; armour design
   14.1; change TK-21; trinkets design 7.3):** after a failed contest or an
   automatic landing, gear that stops the condition (Ringed and Double Ring
   against Bleeding, Unbowed against Prone) and trinket properties
   (Graveward, Kinward, Heartward) stop it here, before a ward is spent.
   Emits `ConditionResisted {reason: ArmourVerb}` or
   `ConditionResisted {reason: TrinketProperty}`. Candidates are tried in
   order, and the first that fires stops the condition (the rest are not
   evaluated or spent): armour and shield verbs on the target, in slot
   order; then the target's own trinket preventions, in slot order; then
   ally preventions, in ascending unit order of the wearers. Per-round
   limits: a unit receives at most one trinket prevention per round, from
   any wearer, counted only when a trinket stops a condition; an ally
   prevention yields only when step 4 would actually spend a Warded charge
   on this application; a unit gains at most one Helped per round from a
   trinket source. Outside combat, each condition application is its own
   round.
4. **Ward:** a Warded charge is spent here, at most once per ability use
   per target.
5. **Boss resolve, then Unyielding downgrade** (12.3). A pip cancels only
   this condition application, never the damage or other effects of the
   same use.
6. **Transform:** checked against existing instances in creation order; the
   first match wins. If the result is a tier the source may not apply to
   this target (Steeled, hard exclusivity, the depth gates, the
   party-facing limits), the fallback is used.
7. **Re-check:** if step 5 or step 6 changed the condition, steps 2, the
   gear step, 4 and 5 run once more against the new condition
   **(change TK-21)**; a downgraded condition gets
   its own resolve check. A second change is not re-checked: the fallback
   is used, or the condition is resisted if there is none.
8. **Cancel**, then **override**, against existing instances in creation
   order. A cancel ends processing.
9. **Stacking** (6.3), then the instance is applied (a consumed transform
   source ends now), then `onApply` runs, then engine grants.
   **Morale substitution (not a transform row):** when a Frighten or a
   limited fear effect would be applied here to a Demoralised enemy that
   is not Emboldened, the condition is discarded (no `onApply`; a step 6
   transform it took part in is not applied and its source is not
   consumed) and the morale engine applies Routed. Demoralised is not
   consumed. Routed wins over the Frightened + limited fear → Panicked
   transform (enemies design 14.3).

**Morale states** (Emboldened, Demoralised, Routed, Cowed) are applied by
the morale engine outside steps 2–7, so Steeled, hard exclusivity, wards,
resolve and composure never see them. The Frighten of T5 and T6 still
passes through the pipeline as Frightened; Routed is substituted only at
step 9 (enemies design 14.3, 14.9).

**Entry points** **(lead's call)**:

| Origin | Enters at | Skips | Gear step **(change TK-21)** |
|---|---|---|---|
| Ability use (rider or not) | Step 1 (non-riders pass it) | — | Runs |
| Universal skill actions (grapple, Frighten) | Step 2 | — | Runs |
| Terrain square or trap (11) | Step 2 | Contest as the square's data says | Runs (no contest: an automatic landing) |
| Aura pulse | Step 2 | — | Runs |
| Burning's spread | Step 2 | Contest is a Reflex save | Runs |
| `onApply` and `onEnd` relations | Step 2 | Contest and ward | Skipped |
| Staged last stage (Calcifying) | Step 2 | Contest (already made) and ward | Skipped |
| Engine grants: Steeled, Unyielding, Surprised, Downed, Scarred | Step 9 | Everything before it | Skipped |
| A talisman's Scarred (gated on the death and healing design; change CN-15; consumables design 14.4) | Applied at once, outside the step 9 queue | Everything | Skipped |

### 9.3 Damage events

A **damage event** is one ability use against one target, one turn-start
tick batch (6.8), or one other separate damage source (a collision, a
Retaliating shroud, a Condemned burst). **(lead's call)**

For each damage part, in data order:

1. Ladder shifts and shells apply during the damage step.
2. `damageType` ends and layer breaks run.
3. Damage-triggered transforms run.

**Save chain (gated on the death and healing design; change CN-15;
consumables design 14.3, 14.4):** after these per-part steps, the event's
damage is summed over all its parts, before hit points are lowered. If the
unit is not Downed and the total would take it to 0 or less, the chain runs
in order and the first link that fires ends it: Last Ward (attack events;
the hit becomes a miss) → Death-warded → talisman. The concentration
difficulty uses the pre-save total. Final owner order: death and healing
design.

- At most one transform per damage event, and a transform never triggers
  another.
- Order exception: a fire hit that ends Soaked attempts no Burning rider.
- **Phase changes** (12.3) resolve after the whole ability use, once every
  target's damage, riders and effects have resolved. Riders from the hit
  that crosses the threshold land first and are then cleared.

### 9.4 Triggered free attacks and redirects

For Suppressed and Retaliating:

- they never fire while a reaction, an opportunity attack, a boss action or
  another free attack is resolving;
- they never fire during forced movement;
- an at-will's rider still counts once per turn.

Riposting and Guarded spend the wearer's reaction, so reaction nesting stays
as settled (ability model 7.3). A defence that spends charges is not spent
on an attack that already has disadvantage from another source.

## 10. Positional rules

**(lead's call; 10.1 is M1 except where marked; 10.2 and 10.3 are M2)**

### 10.1 Forced movement

- Each square must increase the distance from the source (push) or decrease
  it (pull). On ties, the square on the straight line from the source is
  used, then the fixed square order (ability model 8.1); the player, or the
  AI, may pick another tied square as an input.
- It stops at walls, obstacles and units. If stopped early, the moved unit
  and any unit it hits each take band bludgeoning damage once. This is not a
  rider. Stopping before a hazard is not stopping early and deals no
  damage. A collision never applies Prone unless the ability says so.
- It never provokes opportunity attacks.
- Rooted, Restrained and Anchored units are not moved.
- **Shift (change W-8):** a third kind beside push and pull. The unit steps
  along the ring of squares adjacent to the source, each step to a ring
  square 8-adjacent to the current one; never through the source's square.
  It stops at units and walls, is blocked by Rooted, Restrained and
  Anchored, never provokes, and in M1 stops before hazards (weapons design
  7.6).
- **One move per hit (change W-8):** all forced movement one hit causes
  sums to at most 3 squares and resolves once. Collision damage applies
  only if a non-verb source contributed to the move; verb-sourced movement
  moves a unit at most once per round (weapons design 7.2 rule 4). Armour
  verb signatures that move a target (Bull Rush) are verb-sourced
  **(change AR-52; armour design 14.4)**.
- **Drags:** a holder dragging a Grappled unit (18.4) is forced movement
  for opportunity attacks, the triggers in 9.4, hazard edges and terrain.
- **M1:** pushes and drags stop before hazard squares, including terrain
  squares (11.3). A holder that crosses a hazard cannot take the held unit
  with it: the drag stops at the edge.
- **M2:** a unit dragged into a hazard gets the hazard-edge save (10.2); on
  a success it stops on the last safe square and the grapple ends.

### 10.2 Hazard edge

Forced movement into a hazard gives a Reflex save. On a success the unit
stops on the last safe square and gains Teetering.

### 10.3 Landing

When Flying, Jumping or Levitating ends over a hazard, the unit makes a
Reflex save. On a success it lands Prone on the nearest safe square.

### 10.4 Hazard lethality

Hazard design must guarantee that no single failed save, and no entry
without a save, is lethal to the hero. This holds in every wave.

## 11. Fixed terrain

**(agreed unless marked)**

### 11.1 The rule

- Static map squares may apply conditions when a unit enters them, when a
  unit ends its turn on them, or both.
- The squares are placed by floor generation from the theme (main design
  4.6) and are part of the map. Abilities still leave nothing on the ground
  (ability model 8.4). **(lead's call; change C11)**
- **Squares never spread or change**, with one exception (11.4). This keeps
  the non-goal on spreading and evolving surfaces.

### 11.2 Squares

Illustrative set:

| Square | On enter | On turn end there | Wave |
|---|---|---|---|
| Water | Soaked | Soaked | M2 |
| Web | Restrained (the web is the binding; breakers fire and slashing) | Restrained | M2 |
| Fire | Burning | Burning | M2 |
| Oil slick | Oiled | Oiled | M2 |

- A square's data names the condition, its trigger (`onEnter`,
  `onTurnEnd` or both), its contest (a save type and difficulty, or
  automatic), its value and duration, and for a binding its breakers and
  escape difficulty. The value may not exceed the band of the floor's
  expected levels. **(lead's call)**
- **Squares apply minor and major conditions only, never hard ones.**
  Validation enforces this. **(lead's call)**
- **Entry** is any change of a unit's square to this square: its own
  movement, forced movement, a drag, placement, a return from Banished or
  an expulsion. Units standing on a square when combat starts have not
  entered it. `onEnter` fires once per entry. **(lead's call)**
- Squares a Jumping or Flying unit crosses are ignored. Its landing
  square, and a Flying unit's square at the end of its turn, apply only if
  the square is not tagged as crossable by that state. **(settled
  principle, main design 4.3; detail lead's call)**
- A unit that gains speed 0 mid-move (a web's Restrained) stops there and
  loses the rest of its movement. **(lead's call)**
- `onTurnEnd` applications run last in the turn-end step (5.4). Under the
  counting test (5.2), a condition gained there is not counted down or
  saved against in the same turn end. **(lead's call)**
- Applications follow the normal pipeline (9.2) and re-application rules
  (6.3). Standing in water keeps Soaked at its full duration; it never
  extends past it.
- Breaking a binding ends the unit's Restrained, never the square: a web
  square still applies Restrained to the next unit. **(lead's call)**
- Out of combat, squares apply on entry and at every tick's turn-end step,
  as in combat, and their damage over time follows the 1 hit point floor
  (5.5).

### 11.3 Terrain squares are hazards

**(lead's call)**

Every square that applies a harmful condition is a hazard for:

- the M1 rule that pushes and drags stop before hazard squares (10.1);
- the hazard-edge, landing and drag rules (10.1–10.3);
- the hazard lethality rule (10.4);
- conditions that move a unit for it (Commanded, Confused), which never
  enter a hazard;
- followers' pathing out of combat (5.5).

### 11.4 Fire on oil

The single exception to "squares never change". **(agreed that it exists;
the outcome is the lead's call)**

- An oil square burns away when a fire area includes it, or when a fire
  damage part of at least 1 from a hit is dealt to a unit standing on it.
  Ticks never burn it away (6.8).
- **The square becomes plain floor.** It never becomes a fire square. This
  keeps every square free of timers: the payoff lives on the unit, through
  Oiled (catalogue E). **(lead's call)**
- Burn-aways resolve after the damage event, in the fixed square order
  (ability model 8.1).

### 11.5 Staging

- **M1:** no condition terrain. Pushes and drags stop before hazard
  squares.
- **M2:** water, web, fire and oil-slick squares; their condition data;
  Oiled and the burn-away rule; the hazard-edge, landing and
  drag-into-hazard rules. **(agreed)**

### 11.6 Sources, and traps

**(lead's call)**

- **Source `environment`:** terrain applications have this source. It
  counts as hostile to every unit, for the per-fight cap (5.7), the
  party-facing limits and composure (7.2). It is never `sourceTied`, and
  its difficulty comes from the square's data.
- **Traps are not terrain squares.** They are one-shot dungeon content,
  owned by dungeon generation and the stat model's Mechanics uses, and they
  can be sprung, disarmed and re-armed. Their conditions enter the pipeline
  at step 2 with source `environment`.
- **Trap hard control against the party** fires on being sprung only,
  follows the own-turn rule, never appears on floor 1, appears on floor 2
  only in set-piece traps, and counts toward the per-fight cap whenever it
  costs a turn start.

## 12. Immunities and bosses

### 12.1 Tag registry

**(lead's call)** Validation reads this one table.

| Tag | Immunity allowed | Counts as control | Carried by |
|---|---|---|---|
| `mind` | Yes | Yes | Asleep, Charmed, Thralled, Confused, Enraged, Commanded, Frightened, Shaken, Panicked, Unfocused, Tormented |
| `fear` | Yes | Yes | Shaken, Frightened, Panicked, Tormented, Cowed |
| `sleep` | Yes | Yes | Asleep |
| `restraint` | Yes | Yes | Grappled, Restrained, Rooted, Mired, Engulfed |
| `sight` | Yes | Yes | Blinded, Squinting |
| `sound` | Yes | Yes | Sonic effects |
| `goad` | Yes | Yes | Challenged, Enraged |
| `prone` | Yes | Yes (positional) | Prone |
| `petrify` | Yes | Yes | Calcifying, Petrified |
| `poison` | Yes | No | Poisoned, Envenomed, Nauseated |
| `bleed` | Yes | No | Bleeding, Hobbled |
| `disease` | Yes | No | Diseased |
| `agency`, `morale`, `dot`, `awareness`, `lingering`, `wet` | No | No | Classification only |

"Control" is a column of this table, not a tag.

`fearless` is a morale profile read by the morale engine, not a tag
immunity (the `morale` tag allows none). `mindless` implies `fearless`
(enemies design 14.2).

### 12.2 Immunity budget

**(change C6)**

- At most 2 condition-tag immunities per non-boss trait set, at most 1 of
  them a control tag.
- No floor theme where every enemy is immune to the same tag.
- Examples: skeletal = bleed + poison; construct = poison + mind (through
  Mindless); ooze = bleed + prone.
- **Mindless** also gives `mind` immunity. The stat model's "the one
  immunity allowed outside bosses" is reworded to mean damage types.
- **Condition ladder shifts are not traits or layers (change C7).** Every
  ladder shift that comes from a condition (Soaked, Oiled, Exposed,
  Resisting, Shredded, Sapped, Shielded, Raging, Petrified, Ill-Starred,
  Frozen's ice shell) is outside the two-resistance budget.
- **Revealing immunities:** they are hidden until Lore reveals them or play
  shows them. The first "Immune" result reveals the immunity for the rest of
  the run. **(settled principle, stat model 7.2)**

### 12.3 Bosses

**(agreed for resolve and Unyielding; details lead's call)**

- **Resolve:** each condition has an integer `controlRank` in its data, and
  each boss a `resolveThreshold` on the same scale. A pip is spent whenever
  a condition whose rank is at or above the threshold would land, by any
  route: a failed save, a successful skill check, or an automatic
  weak-target rider. The test is static data, so the rules core never runs
  the AI to decide it. **(change C2)**
- **Unyielding** is shown from the start, because it is a basic rule (stat
  model 7.2):
  - after the pips are gone, a hard condition becomes its downgrade (map in
    catalogue A);
  - it holds at most one turn-part major at a time (Dazed, Distracted,
    Nauseated, Hampered, Silenced), and after one ends it is immune to that
    group for 1 turn;
  - it resists any condition that would complete a composite lockout;
  - after Challenged ends, it is immune to Challenged for 1 turn.
- **Hampered** never blocks boss actions or phase abilities.
- **Shed:** a boss action that ends one harmful condition (M2, with boss
  actions). Phase changes end all harmful conditions and grant Steeled,
  after the ability use that caused them (9.3). M1 bosses meet the ability
  model 12.2 rule on sustained effects through resolve and phase changes.
  **(change C2)**
- Minor conditions, and majors below the threshold, land normally.

## 13. Buffs

**(lead's call unless marked)**

- Buffs use the same machinery with a polarity flag. There is no hard tier
  for buffs.
- Each buff must change the action economy, be a charge, change targeting,
  prevent a loss, or carry a cost. Plain flat +1s are dead numbers.
- **Pre-buffing (settled):** buffs on allies lasting over 10 rounds, or
  until rest or camp, need concentration. Under C1, these 10 rounds are 10
  of the wearer's turns.
- **Exemption (change C5):**
  - only `event` sources (shrines, floor events, camp events) and the single
    starting boon may grant boons until rest or camp without concentration;
  - consumables keep the 10-turn cap;
  - one lingering boon per unit; a new one replaces the old;
  - Death-warded is always `combatOnly`.
- **Universal actions as visible states:** Dodging, Helped, Hidden,
  Concentrating.

## 14. Exploration-scale conditions

**(lead's call unless marked)**

- **Durations:** `untilRest` or `untilCamp`. They never tick.
- **Drained, never helpless:** they never deny an action, the bonus action
  or movement down to 0. **(settled principle, ability model 2)**
- **At most 2 harmful lingering conditions per unit.** Further ones are
  blocked. A level gained by an existing instance (6.5) is not a new one.
  **Scarred is exempt:** it always lands and does not count toward the 2,
  because the client made it the fixed cost of a downing or a talisman
  save. **(lead's call; the talisman part gated on the death and healing
  design, change CN-14)**
  Later: an item curse's cost is a `whileEquipped` item property, not a
  lingering condition, and **Cursed (Clinging) from an item is exempt from
  the cap of 2**, as Scarred is (items design 13).
- A camp-bought **Remedy** item ends one non-curse lingering condition.
- **Cures:** Treat or a Mechanics repair during a rest; shrines or Arcana
  for curses; camp clears everything.
- **Scarred (agreed):** a companion that is downed and gets up, or any
  unit saved by a talisman (the hero included), gains Scarred: maximum hit
  points −10% until camp. Only the first downing of a unit on each floor
  gives it. Medicine Treat during a short rest removes it, except a
  talisman's `treatProof` Scarred. Details in catalogue I. **(The talisman
  parts gated on the death and healing design; change CN-14, consumables
  design 14.4, 14.6)**

## 15. Display and knowledge

**(lead's call)**

### 15.1 Plate

- The icon row holds harmful minor, major and hard conditions only. Hard
  and major icons always show, and the row grows as needed; only minors
  collapse into "+N", once the row holds 3 icons.
- Tokens collapse into one diamond cluster with a count.
- Links draw as lines: Grappling, Concentrating, source-tied conditions.
- Steeled, Unyielding and Vigilant draw as rings.
- Bolstered is a segment of the hit point bar.
- Lingering conditions show on the party bar, with one grey pip on the
  battlefield plate.
- Frame shape shows polarity: square for harmful, round for beneficial,
  diamond for tokens.
- The bottom-right number is turns left; the top-left is the value. Windows
  (`untilSource…`) show the source's portrait instead of a number.

### 15.2 Model and initiative bar

Hard control is drawn on the model, and the lost turn is greyed in the
initiative bar.

### 15.3 Buttons and tooltips

- A greyed button names its one reason (ability model 15.4).
- The tooltip has 3 lines: what it does, how it ends, its source.

### 15.4 Preview

The core computes it (ability model 8.3). It shows:

- chance, duration and value;
- "No effect: already Burning";
- "Will Freeze (Soaked)";
- "Will spend a resolve pip" once Lore has revealed the boss's threshold,
  and "May spend a resolve pip" before;
- on a movement path, each terrain condition the path and destination would
  apply ("Entering: Soaked").

### 15.5 Knowledge

- Active conditions are visible to all, **except** `awareness` conditions
  (Hidden, Invisible, Veiled). Those are visible only to their own side and
  to units that have noticed them.
- Terrain squares, and the conditions they apply, are visible once seen.
- Lore reveals immunities, which conditions an enemy's abilities inflict,
  and boss thresholds.

## 16. Content budgets

Enforced by content validation. **(lead's call)**

- At most 32 distinct conditions in M1 and M2 together, not counting engine
  states. Ids that share a `family` (Poisoned and its lingering form, the
  Exposed and Cursed variants) count once. Separately, a budget of **4
  morale states** (Emboldened, Demoralised, Routed, Cowed), hard-capped,
  enemy-only, shown as a frame tint or banner (enemies design 14.9).
- At most 6 distinct harmful conditions per floor theme. Conditions applied
  by the theme's terrain squares count, and so do the implicit rider
  conditions of every damage type the theme's monsters deal; the fire
  rider's Burning and Smouldering count as 2 (enemies design 9.2).
- At most 2 conditions per enemy type, counted by the rule of enemies
  design 9.2. Bosses: at most 2 per phase kit, 3 across phases.
- A theme that places water squares must also field a cold or lightning
  threat, and may not rely mainly on fire and acid enemies, because water
  would otherwise be a free cure and a fire resistance.

## 17. Data and the core

**(lead's call)**

### 17.1 Data shape

Illustrative:

```json
{ "id": "cond.stunned", "family": "stunned", "polarity": "harmful", "tier": "hard", "tags": [],
  "implies": ["cond.incapacitated"],
  "effects": [ { "mod": "attacksAgainst", "advantage": true }, { "mod": "autoFailSave", "saves": ["reflex"] } ],
  "denies": { "action": true, "bonus": true },
  "duration": [ { "turns": 1 } ], "intensity": { "kind": "binary" },
  "flags": { "combatOnly": false, "sourceTied": false },
  "cures": [], "cureImmune": [],
  "relations": [],
  "boss": { "downgrade": "cond.dazed" }, "controlRank": 9,
  "ai": { "denyPct": 100 },
  "display": { "icon": "stun", "onModel": "fx.stun", "grey": "Stunned" } }
```

```json
{ "id": "terrain.web", "hazard": true, "crossableBy": ["jumping", "flying"],
  "apply": [ { "condition": "cond.restrained", "on": ["onEnter", "onTurnEnd"],
               "contest": { "save": "reflex", "dc": 12 }, "duration": [ { "turns": 2 } ],
               "binding": { "name": "web", "breakers": ["fire", "slashing"], "escapeDc": 12 } } ] }
```

Condition data also holds, where the condition uses them:

- `family` (budget counting, 16); `denies` (7.2); `controlRank` (12.3);
- `intensity`: `kind` (binary, valued, decay, accumulating, staged,
  charges), `valueCap`, `levelCap`, `stages`, `noRefresh`;
- `flags`: `combatOnly`, `sourceTied`, `turnPartGroup` (the composure
  groups), `noProne`;
- `cures` (the tags in 8), `cureImmune`, and `extraSave` triggers (Held,
  Thralled);
- `relations`, including `onEnd` reason lists and each transform's
  `consumes`;
- `outOfCombat` duration (5.1).

**Application-level overrides:** an ability's `apply[]` entry, a trait or a
terrain square may override `duration` and `value`. Each override is
validated against the tier band for the side it targets (5.3).

### 17.2 State

- **Instance:** `conditionId`, `seq` (global creation order), `value` (or
  level or stage), `remaining`, `appliedPhase`, `sourceUnitId?` (or
  `environment`), `recordedDifficulty?`, `sustainedBy?`, `markedHard`
  (lockout), `binding?` with its breakers, `damageType?` and `amount?`
  (Condemned, Exposed, Resisting), `hazardId?` (Teetering), `charges?`,
  `pendingBurst?`, `extraSaveUsedThisRound`.
- **Per unit, per fight:** `hardTurnsLostThisFight`, aura immunities,
  composure and Challenged immunity timers, Rally used, the same-source
  Frighten penalty.
- **Per unit, per floor or run:** `downedThisFloor`, the Scarred Treat
  attempt used this rest, the lingering boon.
- **Global:** the phase counter (5.2).
- **Per map:** oil squares that have burned away.
- Implied flags, suppression, blocks and combined modifiers are queries,
  never stored. **(settled principle)**

### 17.3 AI control value

Integers; chances out of 20; one division at the end, rounding down. This
replaces the ability model's "control value per round … set in the
condition's data" (its section 13) with a computed value whose percentage
is set in the data **(change C14)**.

```
T            = target's best expected one-step score next turn over at-wills AND ready limited/cooldown
               abilities, net of its limited cost; bosses add expected boss-action score per round
defensive    = opposing side's extra expected damage vs target with the condition (computed)
offensive    = T − target's best score with the condition (computed, minimum 0)
setup        = payoff ability's added score × allies with a known payoff acting before expiry
fail20       = the condition's fail chance, out of 20
roundsLeft   = estimated fight length (ability model 13) − current round, minimum 1
effTurns20   = 20 × min(turns, roundsLeft); saveEnds: 20 + (min(cap, roundsLeft) − 1) × fail20
term         = (T × denyPct + 100 × (defensive + offensive + setup)) × effTurns20 × fail20 / 40000
```

- **The term is 0** if the target already holds the condition at an equal
  or higher value, is known to be immune, is Steeled or hard-controlled (for
  hard conditions), holds a visible ward, or is in a composure window for
  that condition (7.2, 12.3).
- **Against a boss with pips:** if the condition's `controlRank` is at or
  above the boss's known `resolveThreshold`, the term is the boss's
  `pipValue`.
- **Against an Unyielding target without pips** (a boss, or any unit that
  reached the per-fight cap), a hard condition is scored as its downgrade's
  term.
- **Damage over time** is scored as expected damage, capped at hit points.
- **Terrain on a path:** the position term counts the control value of the
  conditions a step's path and destination would apply to the mover, as a
  cost. **(lead's call)**
- **Terrain by forced movement:** for each push, pull or drag destination,
  the term adds the control value and damage over time of the conditions
  that square would apply to the moved unit, multiplied by the chance the
  hazard-edge save fails. **(lead's call)**
- **Conditions the one-step scorer cannot value** (Commanded's word choice,
  Levitating's push-and-drop combo) are left out of monster kits and
  suggestions.
- **Authoring default:** a boss's `resolveThreshold` defaults to the
  `controlRank` of Dazed. Builders may check it against the scorer (a pip
  should be worth spending on conditions worth about half the boss's own
  T), but the scorer never decides a pip.

Default `denyPct`, a plain integer percentage. This is the single source;
there are no per-family tables. `defensive`, `offensive` and `setup` are
always added by the formula.

| Condition | denyPct |
|---|---|
| Hard | 100 |
| Engulfed | 80 |
| Nauseated | 70 |
| Dazed | 50 |
| Frightened | 40 vs melee, 25 vs ranged |
| Rooted, Restrained | 30 vs melee, 0 vs ranged |
| Grappled | 30 vs melee, 10 vs ranged |
| Challenged | 30 |
| Distracted | 20 |
| Prone, Chilled | 15 vs melee, 0 vs ranged |
| Staggered | 15, plus 10 for each known protective reaction |
| Silenced, Hampered, Disarmed, Charmed, Blinded, Squinting, Unfocused, Harried | 0; their value comes through `offensive` (T − best score under the restriction) |
| Setup conditions (Soaked, Oiled, Exposed, Spotted, Shaken, Teetering) | 0; their value comes through `setup` |

## 18. Catalogue

**(lead's call unless marked)** Values, dice and band numbers are
illustrative. Each entry gives tier and wave, then effect, intensity,
duration and ends, sources, and interactions.

### 18.1 Waves

- **M1:** the vertical slice, which has no riders.
- **M2:** with riders, companions, boss actions and condition terrain
  (including oil slicks and Oiled, as the client asked).
- **Later:** updates. Each must pass the simulator's "expired with no
  effect" gate (20.3) before it ships.

| Wave | Conditions | Engine and movement states |
|---|---|---|
| M1 | 10: Surprised, Prone, Grappled, Frightened, Hidden, Dodging, Stunned, Steeled, Dazed, Bleeding | 4: Incapacitated, Grappling, Concentrating (if the slice class casts), Unyielding |
| M2 | 22: Staggered, Unfocused, Restrained, Chilled, Teetering, Spurred, Burning, Smouldering, Corroded, Withered, Poisoned (one family with its lingering form), Soaked, Oiled, Frozen, Blinded, Revealed, Rallied, Challenged, Helped, Warded, Weary, Scarred | 3: Downed, Flying, Jumping |
| M2 (morale) | 4 morale states, in their own budget (16): Emboldened, Demoralised, Routed, Cowed (enemies design 14) | — |
| Later | 70, listed in each family below (Notched added, change W-12) | 2: Levitating, Phasing |

M1 and M2 together give 32 conditions, the budget's limit (16), with
Scarred and Oiled included. **(lead's call)** The morale states are outside
it, in the separate morale budget. Later conditions are outside the
32-condition budget (16) **(change W-12)**.

In M1, Bleeding comes from a non-rider monster claw and exercises ticks,
values and re-application. Dazed exercises resolve, Unyielding and party
composure. In M1, Rally applies the `rally` cure directly; its Rallied and
Spurred options arrive in M2.

**Test fixtures (lead's call):** the M1 content uses no cancel, override,
transform or suppress, and no `saveEnds`, `charges`, `decay` or `staged`
duration. The M1 test suite therefore ships test-only fixture conditions,
never loaded by the game, that cover each relation kind, each duration
model and the step 7 re-check, so that the whole pipeline is proven in M1
(main design: "Does the effects system hold?").

### 18.2 A. Engine states

- **Incapacitated** — hard (derived flag only) · **M1**
  - Effect: no action, bonus action or reaction; own movement 0; forced
    movement still moves it. Ends its concentration, its Hidden, the
    grapples it holds and the Challenged it imposes. Counts for hard
    exclusivity.
  - Intensity: binary, never stored.
  - Duration: while a parent exists (Stunned, Held, Asleep, Frozen,
    Petrified, Banished, Downed).
  - Sources: `implies` only.
  - Interactions: collapses into the parent's icon.
- **Surprised** — state (outside Steeled and resolve) · **M1**
  - Effect: no movement, action, bonus action or reaction until its first
    turn ends. Does **not** end concentration or Hidden. Counts as hard for
    exclusivity.
  - Intensity: binary.
  - Duration: ends at the end of its first turn; grants Steeled.
  - Sources: the noticing rule **(settled, main design 4.3)**; units still
    Asleep when combat starts.
  - Interactions: no stun can stack on an ambush.
- **Downed** — state · **M2**
  - Effect: implies Incapacitated; onApply Prone; suppresses harmful
    turn-counted conditions (8: their countdowns, saves and decay
    continue); ends concentration. It keeps its initiative slot and runs
    its turn steps.
  - Intensity: binary.
  - Duration: trigger. Ends on Medicine (trained, action) or a won fight,
    getting up on 1 hit point **(settled)**; after any other combat end it
    stays Downed until Medicine (5.5). For the hero, the run ends
    **(settled)**.
  - Sources: 0 hit points (companions), at the zero-hit-point check or
    when damage lands; an engine grant (9.2). **Gated on the death and
    healing design (change CN-15):** where damage lands, the save chain
    (9.3) runs first, so a saved unit is not downed.
  - Interactions: the first downing on a floor sets `downedThisFloor`, and
    getting up from it grants Scarred **(agreed)** (engine grant, 18.10).
    **Gated on the death and healing design (change CN-14):** a talisman
    save does not set `downedThisFloor` (consumables design 14.4).
- **Concentrating** — state · **M1** if the slice class casts
  - Effect: draws a link to each instance it sustains; the tooltip shows
    the check difficulty.
  - Intensity: one per unit **(settled)**.
  - Duration: ends on downed or incapacitated, the psychic rider, a failed
    check, a new concentration effect, or by choice **(settled)**. Linked
    instances end with `concentrationBroken`.
  - Interactions: Unfocused and Shaken give disadvantage on its checks.
- **Grappling** — holder state · **M1**
  - Effect: linked to one Grappled unit. It can drag it: each square
    costs double movement (a cost multiplier, 6.7), and the held unit
    follows into the square it left. A drag is forced movement (10.1) and,
    in M1, stops before hazards. Release is free. Willing or downed allies
    are grabbed with no check.
  - Duration: ends with the link.
  - Interactions: a Rooted, Restrained or Anchored held unit cannot be
    dragged.
- **Steeled** — beneficial state · **M1**
  - Effect: immune to the hard tier; transforms that would produce a hard
    condition use their fallback.
  - Intensity: binary.
  - Duration: until the end of its next turn-end step that begins after it
    is gained.
  - Sources: an engine grant (7.2) when any hard instance (including one
    marked hard as a lockout) or Surprised ends, for any reason except
    `combatStart`.
  - Interactions: shown as a ring; never suppressed (8); the AI hard term
    becomes 0.
- **Unyielding** — trait state · **M1**
  - Effect:
    - after the pips are gone, a hard condition becomes its downgrade;
    - it holds one turn-part major at a time, with a 1-turn immunity to
      that group after it ends;
    - it resists any condition that would complete a composite lockout;
    - it gets a 1-turn immunity to Challenged after Challenged ends.
  - Duration: permanent on bosses (and elites, by choice). Any unit gains
    it, `combatOnly`, after losing 2 turn starts (5.7) **(agreed)**. Never
    suppressed (8).
  - Downgrade map:

    | Hard condition | Downgrade |
    |---|---|
    | Stunned | Dazed |
    | Held | Rooted |
    | Asleep | No effect |
    | Frozen | Chilled |
    | Petrified | Restrained |
    | Banished | Dazed |
    | Commanded | Dazed |
    | Confused | Distracted |
    | Thralled | Charmed |
    | Panicked | Frightened |
    | Enraged | Challenged |
    | Engulfed | Grappled |

### 18.3 B. Turn denial

- **Stunned** — hard · **M1**
  - Effect: implies Incapacitated; automatically fails Reflex saves;
    attacks against it have advantage.
  - Intensity: binary.
  - Duration: `turns:1` (own-turn rule). Steeled follows.
  - Sources: limited only — a stunning-blow Technique (Fortitude), tier 2+
    spells (Will), a brute's skull-crack (cooldown 3), rune traps (11.6).
    Party-facing only from floor 2 elites, bosses and set-piece traps
    **(agreed; traps lead's call)**.
  - Interactions: downgrades to Dazed; sets up Reflex-save areas.
- **Held** — hard · Later
  - Effect: implies Incapacitated; attacks against it have advantage;
    automatically fails Reflex; **no automatic critical hits**.
  - Intensity: binary.
  - Duration:
    - enemy-facing: `[{sustained: {max: 2}}, {saveEnds: {Will, cap 2}}]`,
      with the first damage event each round granting an extra save
      (`extraSave`);
    - party-facing: `turns:1`.
  - Sources: a Hold-Person-shaped tier 2 spell (SRD); a ghoul-like claw
    (cooldown 3).
  - Interactions: breaking the caster's concentration frees the target;
    downgrades to Rooted.
- **Asleep** — hard · Later
  - Effect: implies Incapacitated; onApply Prone; attacks against it have
    advantage.
  - Intensity: binary. The `noProne` flag covers trance effects.
  - Duration: `trigger`; ends on any damage, or when an adjacent ally
    spends its action to wake it. Cap 2 enemy-facing, cap 1 party-facing.
    `outOfCombat`: until damage, a loud event or 20 ticks; at combat start
    it ends and the unit is Surprised instead (5.5).
  - Sources: Sleep-shaped spell (SRD, gated to targets at or below half
    hit points), drugged darts, lullabies (cooldown), gas traps.
  - Interactions: tags `mind` and `sleep`; Asleep at combat start means
    Surprised (5.5); on bosses, no effect. Not the "dormant" monster
    behaviour (C15).
- **Banished** — hard · Later
  - Effect: off the grid; cannot act or be targeted; its auras stop; its
    other conditions are suppressed (engine states excepted, 8). Its
    return is an entry (11.2).
  - Intensity: binary.
  - Duration: `turns:1`, returning to its square or the nearest free one.
    A high-tier version is sustained with `saveEnds {Will, cap 2}`,
    enemy-facing.
  - Sources: Banishment-shaped spell (SRD); a willing ally takes no save.
  - Interactions: a rescue tool for an ally (an ally's Banish costs no turn
    start for the cap, 5.7); downgrades to Dazed.
- **Commanded** — hard, agency, enemy-only · Later
  - Effect: one word for its next turn: Approach, Flee, Grovel (falls
    Prone) or Halt. Commanded movement never enters a hazard.
  - Intensity: binary.
  - Duration: `turns:1`, one Will save on application.
  - Sources: Command-shaped spell (SRD); Presence class abilities.
  - Interactions: Flee draws opportunity attacks; Grovel's Prone outlives
    it; not used in suggestions; downgrades to Dazed.
- **Engulfed** — hard · Later (depth 3+)
  - Effect: inside the engulfer; can only attack the engulfer, at
    disadvantage; takes the engulfer's damage at turn start (counts toward
    the tick cap); only single-target heals and Treat reach it.
  - Intensity: binary.
  - Duration: trigger, cap 1 party-facing: expelled at the end of its next
    turn (turn-end step 4); or Escape (action); or the engulfer takes at
    least its threshold in one damage event; or the engulfer is downed.
  - Sources: monster `uses` or `cooldown` only.
  - Interactions: overrides Grappled; Unfettered does not reach it (hard,
    18.4).
- **Calcifying** — staged: minor, then major, then hard · Later
  - Effect:
    - stage 1: speed halved, no Dash;
    - stage 2: Restrained's effects, without its escape route;
    - stage 3: Calcifying ends and Petrified is applied.
  - Intensity: `staged`, 3 stages, the one staged condition (6.5);
    re-application has no effect.
  - Duration: `staged {stages: 3, save: Fortitude, cap: 3}`. A Fortitude
    save at the end of each of its turns: a success ends the chain; a
    failure advances one stage. It ends at the cap.
  - Sources: gaze abilities (cooldown), cursed shrines (traps, 11.6).
  - Interactions:
    - cured at stages 1–2 by a tonic (an action at stage 2), Treat or a
      cleanse;
    - the stage-3 Petrified enters the pipeline at step 2 (9.2), so a
      Steeled unit, a party unit behind a depth gate, or a boss that spends
      a pip, stays at stage 2 until its next save or the cap;
    - Petrified gained in the turn-end step is off-turn (5.2), so it costs
      the next turn.
- **Petrified** — hard · Later
  - Effect: implies Incapacitated; resistant to all damage; automatically
    fails Reflex and Fortitude; all its other conditions are suppressed,
    except the engine states (8).
  - Intensity: binary.
  - Duration: enemy-facing `saveEnds {Will, cap 2}`; party-facing
    `turns:1`.
  - Sources: Calcifying stage 3; set-piece traps (11.6).
  - Interactions: never shattered; downgrades to Restrained.
- **Dazed** — major · **M1**
  - Effect: movement plus either its action or its bonus action; no
    reactions until the end of its next turn. `denies: oneOf`.
  - Intensity: binary.
  - Duration: `turns:1`.
  - Sources: concussive Techniques, limited bludgeoning abilities, Hasted's
    onEnd, the Stunned downgrade.
  - Interactions: Dazed + Distracted leaves the action only; its
    `controlRank` is the default `resolveThreshold` (17.3), so it strips
    pips; a turn-part major, so party composure follows it (7.2).
- **Distracted** — major · Later
  - Effect: no bonus action until the end of its next turn.
  - Intensity: binary.
  - Duration: `turns:1`.
  - Sources: feints, illusion cantrips (rarely), jesters (cooldown), the
    Confused downgrade.
  - Interactions: stops potions, Rally and Disarmed recovery; it can still
    drink a tonic as an action; with Nauseated it is a composite lockout.
- **Nauseated** — major · Later
  - Effect: no action until the end of its next turn.
  - Intensity: binary, `poison` tag.
  - Duration: `turns:1`.
  - Sources: stench auras (a successful save gives immunity to that aura
    for the fight), gas traps.
  - Interactions: a tonic as a bonus action still works; with Distracted
    it is a composite lockout.
- **Staggered** — major · **M2**
  - Effect: no reactions until the end of its next turn. This switches off
    opportunity attacks, wards, Guarded, Riposting, Challenged's punishment
    and Harried's punishments.
  - Intensity: binary.
  - Duration: `turns:1`, combatOnly.
  - Sources: bludgeoning rider (Fortitude) **(change C9)**; automatic on
    bludgeoning-weak, Brittle or shattered Frozen targets; shield bashes
    with the Bashing Rim property or a Technique, not the base bash
    **(change W-9; weapons design 17.9)**.
  - Interactions: does not release grapples.
- **Silenced** — major · Later
  - Effect: cannot use limited `spell` abilities or `vocal` abilities
    (Frighten, Rally, scrolls). **At-will cantrips still work.**
  - Intensity: binary.
  - Duration: `turns:2` enemy-facing, `turns:1` party-facing (5.3). The
    aura version reapplies `turns:1` at each turn start inside the aura,
    which counts that turn (5.2); a successful save gives immunity for the
    fight.
  - Sources: Silence-shaped spell (SRD) placed on a unit; monsters
    (cooldown).
  - Interactions: tonic as an action; leaving the aura; breaking the aura
    holder's concentration.
- **Hampered** — major · Later
  - Effect: at-wills and passives only; no `rest` or `camp` pools, `uses`
    or cooldowns. Consumables still work. Never blocks boss actions or
    phase abilities.
  - Intensity: binary.
  - Duration: `turns:1` (2 from a higher slot).
  - Sources: anti-magic or anti-technique limited abilities; null runes.
  - Interactions: the anti-nova tool; the preview names what it blocks.
- **Unfocused** — minor · **M2** (depends on C8)
  - Effect: cannot begin concentration; disadvantage on concentration
    checks.
  - Intensity: binary, `mind` tag.
  - Duration: `turns:1` from the psychic rider, `turns:2` from limited
    abilities.
  - Sources: psychic rider (C8); mind-blast monsters.
  - Interactions: Clear-minded or a tonic ends it.
- **Disarmed** — major · Later (needs the items design)
  - Effect: drops its weapon into its square. A bonus action picks it up or
    switches to a spare. Units with natural weapons instead have
    disadvantage on weapon attacks until they recover (bonus action).
  - Intensity: binary.
  - Duration: trigger, combatOnly. An issue weapon returns at combat end
    and cannot be taken. A dropped notable weapon may be seized with the
    free interaction, becomes party loot and does not return at combat
    end; the owner uses its `improvised` attack (enemies design 15.5).
    Party side: a party weapon still on the ground at combat end returns
    to its owner (items design 7.1). A Clinging weapon is never dropped
    (Later, items design 13). Monster side: a party unit on the square
    blocks recovery only once the owner has moved off it **(change W-9)**.
  - Save: Fortitude against the source's ability difficulty, with
    advantage when wielding two-handed **(change W-9)**.
  - Sources: disarm Techniques, whip or flail properties, monster cooldown
    abilities **(change W-9)**.
  - Interactions: Distracted prevents recovery. Only the main hand drops;
    shields are never Disarmed. A Disarmed focus loses only its verb and
    properties; casting continues. Sure Grip and Clinging are immune
    **(change W-9; weapons design 16)**.
- **Harried** — minor · Later
  - Effect: its ranged attacks, and abilities aimed more than 1 square
    away, provoke opportunity attacks from adjacent hostiles.
  - Intensity: binary.
  - Duration: `turns:2`.
  - Sources: skirmisher Techniques.
  - Interactions: Staggered neighbours cannot punish it.
- **Suppressed** — minor · Later
  - Effect: if it willingly leaves its square, the source makes one free
    ranged at-will attack, then Suppressed ends.
  - Intensity: binary, one source.
  - Duration: `untilSourceTurnStart`; ends if the source loses line of
    sight.
  - Sources: covering-shot Techniques.
  - Interactions: follows the triggered-free-attack rule (9.4).

### 18.4 C. Movement and position

- **Prone** — minor · **M1**
  - Effect:
    - adjacent attackers have advantage; attackers at 2+ squares have
      disadvantage;
    - its own attacks have disadvantage;
    - it crawls at 2 movement per square;
    - standing costs half its current effective speed, minimum 1, or the
      action at speed 0 (6.7);
    - applying it ends Flying and Jumping (landing rule);
    - dropping prone deliberately is free.
  - Intensity: binary.
  - Duration: trigger until it stands (self-ended, no cap, 5.3);
    combatOnly.
  - Sources, tagged `shove`, `trip`, `collision` or `onApply`: Shove, trip
    Techniques, the Topple verb (tagged `trip`), Asleep, Downed, Grovel.
    Topple applies at most once per round per target; condition verbs
    never fire on reactions **(change W-10; weapons design 7.2)**.
  - Interactions: `prone` immunity; Anchored blocks shove, trip and
    collision sources; Levitating blocks it.
- **Grappled** — major · **M1**
  - Effect: speed 0; disadvantage on attacks against anyone but the holder;
    only the holder can drag it.
  - Intensity: binary, one holder.
  - Duration: trigger (self-ended, no cap, 5.3), combatOnly, sourceTied.
    Ends on:
    - Escape (action, Athletics or Acrobatics against 10 + the holder's
      Fortitude); **a success also stands it up for free**;
    - the holder being incapacitated, downed or dead, or releasing it;
    - separation by more than 1 square;
    - Unfettered.
  - Sources: the grapple universal action **(settled)**; monster grabs.
    Grabs that also knock Prone must be on cooldown.
  - Interactions: with Prone it can still stand by spending the action; on
    bosses, resolve covers the skill-check route; a drag is forced
    movement and, in M1, stops before hazards; from M2, a drag into a
    hazard gives the hazard-edge save (10.1).
- **Restrained** — major · **M2**
  - Effect: implies Rooted's flags (speed 0, not moved by forced movement);
    attacks against it have advantage; its attacks and Reflex saves have
    disadvantage. Each application names a binding and its breaker types.
  - Intensity: binary.
  - Duration: `turns:2`, or `sustained {max: 3}` (wearer turns). The
    instance stores its binding and breaker types. Ends on:
    - Escape (action, against the recorded difficulty);
    - an adjacent ally's action (frees it automatically);
    - breaker damage (web: fire or slashing; net: slashing; chains: acid);
    - a tonic drunk as an action;
    - Unfettered;
    - `sourceGone` for coils.
  - Sources: webs (including web squares, 11.2), Entangle-shaped spells
    (SRD), nets (monster and trap sources; the party net is a Later
    consumable, consumables design 16.2) **(change CN-16)**, constrictor
    coils.
  - Interactions: overrides a stored Rooted; Reflex disadvantage sets up
    areas; speed 0 gained mid-move ends the move (11.2).
- **Rooted** — major · Later
  - Effect: speed 0; no Dash or Jump; not moved by forced movement; a
    flying unit hovers in place.
  - Intensity: binary.
  - Duration: 1–2 turns. Ends on Escape (action), a breaker type, a tonic
    drunk as an action, or Unfettered.
  - Sources: vines, pinning shots, snares; the Held downgrade.
  - Interactions: makes a Teetering edge safe; matters to melee units,
    barely to ranged ones.
- **Chilled** — minor · **M2**
  - Effect: speed halved; Dash adds the halved speed.
  - Intensity: binary.
  - Duration: `turns:2`. Ends on fire damage of at least 1, a tonic, or
    Unfettered; cancels with Burning, Smouldering or Hasted.
  - Sources: cold rider; automatic on cold-weak or Soaked targets. This is
    the stat model's "slows movement" (8.1).
  - Interactions: `freezing` cold turns it into Frozen (fallback: no
    effect).
- **Mired** — minor · Later
  - Effect: cannot Dash, Disengage or slip past.
  - Intensity: binary.
  - Duration: `turns:2`. Ends on shakeOff, a tonic or Unfettered.
  - Sources: tar, bog monsters, sticky flasks.
  - Interactions: blocks Hasted's bonus-action Dash and Disengage.
- **Grounded** — major · Later (only if themes have flyers over hazards)
  - Effect: cannot gain Flying, Jumping or Levitating; applying it ends
    them (landing rule).
  - Intensity: binary.
  - Duration: `turns:2`; tonic.
  - Sources: bolas, harpoons.
- **Teetering** — minor · **M2**
  - Effect: on a hazard's edge; its attacks have disadvantage; its next
    save against being forced into that hazard has disadvantage.
  - Intensity: binary, tied to one hazard.
  - Duration: trigger (self-ended, no cap, 5.3). Ends when it moves to a
    square not adjacent to the hazard, enters the hazard, or a later forced
    movement resolves (not the one that created it). The instance stores
    the hazard.
  - Sources: a successful hazard-edge save (10.2), including against
    terrain squares.
  - Interactions: Rooted and Anchored make the edge safe.
- **Hobbled** — minor · Later
  - Effect: the first time each turn it moves 3+ squares by its own
    movement, it takes N untyped damage.
  - Intensity: valued.
  - Duration: 2–3 turns. Ends on Treat or a tonic, **not** a heal.
  - Sources: hamstring Techniques, bear traps.
  - Interactions: being dragged does not trigger it.
- **Hasted** — beneficial · Later
  - Effect: +3 speed; Dash or Disengage as a bonus action; **no extra
    action**.
  - Intensity: binary.
  - Duration: sustained up to 10 turns.
  - Sources: Haste-shaped spell (SRD, camp tier) **(change CN-16)**.
  - Interactions: onEnd for any reason except a cancel gives Dazed for its
    next turn; cancels Chilled; Mired blocks the bonus actions.
- **Fleet** — beneficial · Later
  - Effect: +2 speed; opportunity attacks against it have disadvantage;
    advantage on slip-past checks.
  - Intensity: binary.
  - Duration: up to 10 turns.
  - Sources: Longstrider-shaped spell (SRD) **(change CN-16)**.
- **Spurred** — token · **M2**
  - Effect: one free move of up to 2 squares during its next turn; costs no
    speed and provokes no opportunity attacks.
  - Intensity: 1 charge.
  - Duration: fallback is the end of its next turn.
  - Sources: Rally's free-step option **(settled, stat model 7.1)**.
  - Interactions: unusable at speed 0 or while Prone; Rally gives this or
    Rallied, never both.
- **Anchored** — beneficial · Later
  - Effect: not moved by forced movement; cannot be knocked Prone by shove,
    trip or collision; cannot Dash or Jump.
  - Intensity: binary.
  - Duration: `turns:1` (Brace), or a self toggle.
  - Sources: stances, Brace.
  - Interactions: the onApply Prone from Asleep or Downed still applies.
- **Unfettered** — beneficial · Later
  - Effect: blocks and, on application, overrides minor and major
    `restraint` conditions, Chilled and Mired; standing costs 1 square.
    Never Engulfed (hard).
  - Intensity: binary.
  - Duration: sustained up to 10 turns **(change CN-16; no potion grants
    it, consumables design 16.7)**.
  - Sources: Freedom-of-Movement-shaped spell (SRD).
  - Interactions: does not stop forced movement, Prone or Grounded.
- **Movement states:**
  - **Flying** and **Jumping** · **M2** **(settled, main design 4.3)**. A
    jump is atomic. A failed jump costs the movement and never causes a
    fall.
  - **Levitating** · Later. On an unwilling target: speed 0, cannot be
    Prone, forced movement moves it double distance; Fortitude save, then
    `saveEnds {Fortitude, cap 3}`. Its push-and-drop combo is left out of
    monster kits and suggestions.
  - **Phasing** · Later. Moves through units and phaseable obstacles, never
    walls.

### 18.5 D. Damage over time and erosion

- **Bleeding** — minor · **M1**
  - Effect: at turn start, N untyped damage (ignores the ladder and
    layers).
  - Intensity: valued by band (illustrative 2/4/6). `worsen` only from
    limited or cooldown abilities.
  - Duration: `turns:3`. Ends on any hit point gain, Treat or a tonic. In
    M1, any hit point gain is the in-fight end; Treat and tonics arrive in
    M2. **(lead's call)**
  - Sources: a non-rider monster claw in M1; the slashing rider in M2;
    serrated loot.
  - Interactions: `bleed` immunity; Withered suppresses the heal end;
    Frozen and Petrified suppress the tick.
- **Burning** — minor · **M2**
  - Effect: at turn start, N fire damage through the ladder (a tick, not
    a hit, 6.8). It then spreads to units, never squares (turn-start step
    7): the first adjacent unit (fixed unit order) that is not burning
    makes a Reflex save or gains Smouldering (pipeline step 2).
  - Intensity: valued by band (illustrative 2/4/6).
  - Duration: `turns:2` (`turns:3` at band 3). Ends on shakeOff, a tonic
    or cold damage; cancels with Chilled or Soaked.
  - Sources: fire rider; automatic on fire-weak targets (a torch counts);
    Oiled's payout; fire squares (11.2).
  - Interactions: overrides Smouldering; a Burning unit that enters water
    loses both (cancel).
- **Smouldering** — minor · **M2**
  - Effect: the lowest fire band as a tick; never spreads.
  - Intensity: binary.
  - Duration: `turns:1`. Same ends as Burning; overridden by Burning.
  - Sources: Burning's spread only.
- **Envenomed** — minor · Later
  - Effect: at turn start, N × the band's venom step (illustrative 1/2/3)
    untyped damage; N drops by 1 at the end of its turn (decay, 5.4).
  - Intensity: `decay`, N at most 3 (the minor tier's maximum turns, 5.1).
    `worsen` only on cooldown bites.
  - Duration: ends at 0, or on Treat or a tonic; **not** healing.
  - Sources: spider and serpent cooldown bites; poisoner features.
  - Interactions: `poison` tag; Treat ends every poison condition at once.
- **Shocked** — minor · Later
  - Effect: after each attack roll or save-forcing ability it uses, it
    takes N lightning damage (once per use).
  - Intensity: valued.
  - Duration: `turns:2`. Ends on a tonic, or on **Discharge** (bonus
    action: ends it and deals N lightning to one adjacent unit, with no
    roll, rider or arc).
  - Sources: limited lightning abilities. The lightning rider stays the
    arc **(settled, stat model 8.1)**.
  - Interactions: Soaked adds +half to the tax.
- **Condemned** — minor · Later
  - Effect: a visible 3-turn countdown, then one burst of the recorded type
    and amount through the ladder (its own damage event, 9.3). A burst due
    while suppressed is held as `pendingBurst` (8). Out of combat it counts
    as a tick for the 1 hit point floor (5.5).
  - Intensity: binary, `noRefresh` (6.3).
  - Duration: ends early on a heal, Treat, a tonic, or `sourceGone`.
  - Sources: elite and boss limited abilities only.
  - Interactions: Withered suppresses its heal end; a boss with both
    Condemned and Withered needs content review; Frozen's shell resists the
    burst.
- **Corroded** — minor · **M2**
  - Effect: armour class −N (+1 against heavy armour or armoured traits,
    read as `CountsAsHeavy`; armour design 6.1) **(change AR-12)**.
    On application it strips one unbroken layer not tagged acidproof.
  - Intensity: valued by band (illustrative 1/2/3).
  - Duration: `turns:3`. Ends on shakeOff (rinse), a tonic, or Soaked
    (override).
  - Sources: acid rider, oozes, rust-themed monsters.
  - Interactions: shares the armour class slot under the same-number rule;
    outside the reduction cap (armour design 9.2); ends Frozen; Warded
    absorbs it at step 4 (it does not strip wards).
- **Exposed (type)** — minor · Later
  - Effect: one ladder step weaker to the named type; never past weak;
    immunity unaffected.
  - Intensity: binary per type.
  - Duration: `turns:2`; tonic.
  - Sources: limited "expose" abilities.
  - Interactions: **Brittle** is a preset: Exposed (bludgeoning),
    `charges:1`, fallback 3 turns. Shielded nets out to normal through the
    ladder.
- **Shredded** — minor · Later
  - Effect: all its resistances one step lower; cannot gain layers or
    Warded.
  - Intensity: binary.
  - Duration: `turns:2`; tonic.
  - Sources: rare piercing or acid Techniques.
- **Withered** — minor · **M2**
  - Effect: cannot regain hit points or gain temporary hit points.
  - Intensity: binary.
  - Duration: `turns:2`. Ends on Treat, a tonic, or a radiant-tagged heal
    (which ends Withered instead of healing).
  - Sources: necrotic rider; undead claws.
  - Interactions: the stat model's "blocks healing" (8.1); suppresses
    Regenerating and the heal ends of Bleeding and Condemned; does not
    block Medicine getting a downed ally up.
- **Sapped** — minor · Later
  - Effect: its damage parts resolve one ladder step toward resistant.
  - Intensity: binary.
  - Duration: `turns:2`; tonic.
  - Sources: draining monsters; Ray-of-Enfeeblement-shaped spell (SRD).
  - Interactions: cancels Empowered.
- **Poisoned** — minor · **M2**
  - Two ids in one `family` (16), so they never share an instance and the
    effects never depend on the duration **(lead's call)**:
    - `cond.poisoned`: disadvantage on attack rolls and skill checks;
      `turns:3`; ends on Treat or a tonic;
    - `cond.poisoned.lingering`: disadvantage on skill checks and −2 to
      passive values; `untilRest`; ends on Treat during a rest, a Remedy,
      or a short rest.
  - Intensity: binary each.
  - Sources: monster bites (on-hit traits gated like riders), traps.
  - Interactions: `poison` tag; a condition, not a damage type **(settled,
    stat model 8.1)**.

### 18.6 E. Elemental combos

- **Soaked** — minor · **M2**
  - Effect: weak to cold and lightning; resistant to fire; lightning arcs
    from a hit on it reach every adjacent unit (arcs never re-arc).
  - Intensity: binary.
  - Duration: `turns:3`. Ends on fire damage (that hit attempts no Burning
    rider) or a tonic. A ring-shifted fire part ends Soaked and attempts no
    Burning rider, as any fire hit **(change TK-25; trinkets design 9.4)**.
  - Sources: water monsters and spells; water flasks, which can be thrown
    at an ally to put out Burning; water squares (11.2).
  - Interactions: cancels Burning and Smouldering; overrides Corroded and
    Conspicuous; Chilled from a `freezing` source becomes Frozen (fallback
    Chilled), which consumes Soaked.
- **Oiled** — minor · **M2**
  - Effect: weak to fire until a fire hit lands. That hit takes +half,
    applies Burning automatically one band higher, and ends Oiled.
  - Intensity: binary.
  - Duration: `turns:3`; shakeOff or a tonic.
  - Sources: oil flasks, slimes; oil-slick squares (11.2), which ship with
    it. Only a fire hit pays it out; ticks never do (6.8).
  - Interactions: with Soaked, fire is normal and no automatic Burning
    lands; the fire hit that pays out Oiled also burns away an oil square
    under the unit (11.4). A ring-shifted fire part (trinkets design 9.4)
    does not pay out Oiled: no +half, no band-higher Burning, Oiled stays
    and no oil square burns; Burning lands as an ordinary automatic rider
    at its normal band **(change TK-25)**.
- **Frozen** — hard · **M2**
  - Effect: implies Incapacitated; an ice shell (a condition ladder shift,
    not a layer, C7) resists every type except fire and bludgeoning and is
    weak to bludgeoning; attacks against it get no advantage.
  - Intensity: binary.
  - Duration: `turns:1` (own-turn rule). Ends on fire, on bludgeoning (a
    shatter: the shell's weakness step, applied once, so the hit takes
    +half and no more; Stagger lands automatically) or when Corroded is
    applied. Steeled follows.
  - Sources: transform only, from a limited `freezing` cold ability on a
    Soaked or Chilled unit; the transform consumes Soaked or Chilled. The
    depth gates apply at runtime (7.2): on floor 1 a monster's freezing
    attack on a Soaked hero gives Chilled.
  - Interactions: suppresses Bleeding; freezing an ally protects it (only
    a limited fire ability can thaw an ally); downgrades to Chilled.

### 18.7 F. Senses and stealth

- **Blinded** — major · **M2**
  - Effect: can target only within 1 square; its attacks have
    disadvantage; attacks against it have advantage; it "sees" only
    adjacent units. Line of sight and fog of war are unchanged.
  - Intensity: binary.
  - Duration: `turns:1` (2 at band 3); enemy-facing spells use
    `saveEnds {Fortitude, cap 2}`. Ends on a tonic drunk as an action, or a
    cleanse.
  - Sources: radiant rider on a target that is not hidden **(change C9)**;
    flash powder (monster and trap sources) **(change CN-16)**; glare
    traps.
  - Interactions: overrides and blocks Squinting; suppresses Frightened
    and Charmed unless the source is adjacent; `sight` tag.
- **Squinting** — minor · Later
  - Effect: its attacks against targets more than 2 squares away have
    disadvantage; passive perception −5.
  - Intensity: binary.
  - Duration: `turns:2`; tonic.
  - Sources: the radiant rider's fallback for sources that may not apply a
    major condition; light auras.
  - Interactions: a limited `searing` radiant ability turns it into Blinded
    (fallback: no effect).
- **Revealed** — minor · **M2**
  - Effect: on application it ends Hidden, Invisible, Veiled and Blurred;
    it cannot regain them; attacks against it ignore concealment. Its
    outline shows only while an opposing unit has line of sight.
  - Intensity: binary.
  - Duration: `turns:2`; tonic.
  - Sources: radiant rider on a hidden or invisible target **(change C9)**
    (normal rider rules; automatic only on radiant-weak targets);
    Faerie-Fire-shaped outline spells (SRD).
- **Hidden** — beneficial · **M1**
  - Effect: binary per observer. Unnoticing enemies cannot single-target it
    (areas still hit it); its attacks against them have advantage; it
    provokes no opportunity attacks from them.
  - Intensity: binary per observer; nothing is stored.
  - Duration: trigger. Ends:
    - after it resolves an attack or harmful ability;
    - if it ends its turn in line of sight of an enemy within 3 squares;
    - for one enemy, when that enemy Searches successfully (its action,
      Perception against the hider's passive stealth);
    - on Revealed or Incapacitated.
  - Sources: Hide (Stealth against each enemy's passive perception; the
    settled one-roll form, stat model 6).
  - Interactions: `awareness` tag, so only its own side sees it.
- **Invisible** — beneficial · Later
  - Effect: hidden from every enemy with no check; Search fails against it;
    it does not end near enemies.
  - Intensity: binary.
  - Duration: sustained up to 10 turns **(change CN-16)**. Ends after it
    attacks or uses a harmful ability, or on Revealed or Incapacitated.
  - Sources: Invisibility-shaped spell (SRD).
  - Interactions: Vigilant enemies within 3 squares ignore it.
- **Veiled** — beneficial · Later
  - Effect: attacks against it from more than 2 squares away have
    disadvantage; it may Hide in the open if every enemy that sees it is
    more than 2 squares away.
  - Intensity: binary.
  - Duration: `turns:3`, or sustained.
  - Sources: shadow spells cast on a unit; smoke cloaks.
- **Vigilant** — beneficial · Later
  - Effect: cannot be Surprised; Hidden, Invisible and Veiled units within
    3 squares count as noticed by it; advantage on Perception.
  - Intensity: binary.
  - Duration: `turns:10`, or sustained.
  - Sources: See-Invisibility-shaped spell (SRD); Perception mastery.
- **Deafened** — mixed, minor · Later
  - Effect: immune to harmful `sound` effects; cannot receive Rally;
    Intimidation against it has disadvantage; it cannot hear for noticing
    or for being the party's listener.
  - Intensity: binary.
  - Duration: `turns:3`; out of combat, sustained up to 10 ticks.
  - Sources: thunderclap spells; wax-plug consumables.
- **Hushed** — beneficial · Later
  - Effect: its actions make no noise for hearing range; loud checks become
    quiet; it ignores the heavy armour stealth penalty, passive and checks
    (Clatter) **(change AR-13; armour design 7.3)**.
  - Intensity: binary.
  - Duration: sustained, or event `untilRest`. Ends when it attacks or
    combat starts.
  - Sources: Pass-without-Trace-shaped spell (SRD).
  - Interactions: cancels Conspicuous.

### 18.8 G. Mind, fear, morale, targeting

- **Shaken** — minor · Later
  - Effect: disadvantage on Will saves and concentration checks;
    Intimidation against it has advantage; it cannot Rally or Frighten.
  - Intensity: binary.
  - Duration: `turns:2`, combatOnly. Ends on rally, Clear-minded or a
    tonic.
  - Sources:
    - an ally within 3 squares downed or killed (minion deaths do not
      trigger it);
    - a **party** Frighten that misses by 4 or less;
    - fear auras (a successful save gives immunity for the fight).
  - Interactions: Frightened overrides it; suppresses Inspired.
- **Frightened** — major · **M1**
  - Effect: tied to one source, and only while that source is in line of
    sight: disadvantage on attacks and skill checks; cannot willingly move
    closer to the source; no opportunity attacks against it.
  - Intensity: binary, one source.
  - Duration: `turns:2`, combatOnly, sourceTied; enemy-facing spells use
    `saveEnds {Will, cap 3}`. Ends on rally, Clear-minded, `sourceGone` or a
    tonic drunk as an action; breaking line of sight switches its penalties
    off.
  - Sources: Frighten (Intimidation against 10 + Will) **(settled)**;
    Fear-shaped spells (SRD); boss roars (cooldown).
  - Interactions: after it ends, further Frighten attempts by the same
    source have disadvantage for the fight; overrides Shaken; a limited
    fear effect on an enemy raises it to Panicked.
- **Panicked** — hard, agency, enemy-only · Later
  - Effect: Dashes as far from the source as it can; no attacks or
    reactions; takes the Dodge action if cornered. Its movement never
    enters a hazard.
  - Intensity: binary.
  - Duration: `turns:1`. Steeled follows, then onEnd gives Frightened for
    1 turn.
  - Sources: limited fear abilities.
  - Interactions: blocked by Emboldened, Rallied and Clear-minded;
    downgrades to Frightened.
- **Rallied** — beneficial · **M2**
  - Effect: on application it ends `fear` conditions (and Routed or
    Demoralised if an enemy leader applies it); it blocks `fear` while
    present.
  - Intensity: binary.
  - Duration: `turns:2`, combatOnly.
  - Sources: Rally's "steady" option **(settled, stat model 7.1)**; enemy
    leader rallies (cooldown) **(change CN-16)**.
  - Interactions: Deafened units cannot be Rallied. An enemy leader's
    Rally includes itself and keeps the once-per-ally-per-fight limit; a
    Frightened leader cannot Rally (enemies design 14.4).
- **Emboldened** — beneficial, morale state · **M2**
  - Effect: while within 2 squares of its living leader, for members of
    the leader's own group only: advantage on saves against fear; Frighten
    against it has disadvantage; cannot be Panicked or Routed; shielded
    from the loss triggers T2 and T3 (enemies design 14.3).
  - Intensity: binary, source-tied aura.
  - Duration: while in range.
  - Sources: the leader's aura (Command, enemies design 14.4).
  - Interactions: ends with no `onEnd`; the morale engine's T1 Demoralises
    the non-fearless members when the leader falls, whether it dies or is
    Routed, Yielded or Fled (enemies design 14.3).
- **Demoralised** — minor, morale state · **M2**
  - Effect: Intimidation against it has advantage; at or below its morale
    profile's rout line it becomes Routed: ½ hit points for `normal` and
    `skittish`, ¼ for `steady`, at once for a minion Demoralised by T1 or
    T2 (enemies design 14.3).
  - Intensity: binary.
  - Duration: combatOnly; ends when a living leader of its group Rallies
    it.
  - Sources: the morale engine's loss triggers T1–T4, cost-weighted and
    edge-triggered (enemies design 14.3). The loss triggers skip
    Emboldened members; `fearless` members are never Demoralised.
- **Routed** — morale state, agency, enemy-only · **M2**
  - Effect: flees at full speed and Dashes; attacks only when cornered;
    makes no opportunity attacks; throws down its notable item; leaves the
    fight and the floor if it ends a turn unseen by the party, without
    warning anyone or returning (enemies design 14.5).
  - Intensity: binary.
  - Duration: combatOnly; a leader's rally ends it and removes its cost
    from the group's losses.
  - Sources: morale breaks; the morale substitution for a Frighten or a
    limited fear effect on a Demoralised enemy that is not Emboldened
    (9.2, step 9).
  - Interactions: bosses are immune; wins over the Frightened + limited
    fear → Panicked transform (enemies design 14.3).
- **Cowed** — exploration state, morale state, enemy-only · **M2**
  - Effect: a weak group will not start combat and steps aside for the
    party. Any harmful act against a member breaks it, and the attacked
    members become Shaken. Experience is paid once per member.
  - Intensity: binary, group-wide.
  - Duration: until the floor ends.
  - Sources: out-of-combat Intimidation **(settled job, stat model 7.1)**;
    eligibility and the `broken` flag as in enemies design 14.8.
- **Charmed** — major · Later
  - Effect: cannot attack or harm the source, or include it in a harmful
    area; no opportunity attacks against it; the source has advantage on
    Presence checks against it. A restriction, so it is allowed on party
    units.
  - Intensity: binary, one source.
  - Duration: `saveEnds {Will, cap 3}`. Ends at once if the source's side
    damages or harms it, and on `sourceGone`, Clear-minded or a tonic drunk
    as an action.
  - Sources: Charm-Person-shaped spell (SRD); siren-like gazes (cooldown).
  - Interactions: overrides Frightened from the same source; the Thralled
    downgrade.
- **Thralled** — hard, agency, enemy-only · Later
  - Effect: the source's side commands its movement and at-wills; no
    reactions.
  - Intensity: binary.
  - Duration: `saveEnds {Will, cap 2}`; damage from the source's side
    grants an extra save.
  - Sources: Dominate-Person-shaped tier 4 spell (SRD). Never on bosses
    before the downgrade applies.
  - Interactions: downgrades to Charmed.
- **Confused** — hard, agency, enemy-only · Later
  - Effect: deterministic. It moves to the nearest unit of any side (ties
    by unit order, never into hazards) and attacks it with its best
    at-will; no limited abilities or reactions; Dodges if no unit is
    reachable.
  - Intensity: binary.
  - Duration: `saveEnds {Will, cap 2}`.
  - Sources: Confusion-shaped spell (SRD), without the random table.
  - Interactions: the preview shows its next target; downgrades to
    Distracted.
- **Enraged** — hard, agency, enemy-only · Later
  - Effect: must move to the source and attack it; its at-will hits deal
    +2 damage; attacks against it have advantage.
  - Intensity: binary.
  - Duration: `turns:1`, or `saveEnds {Will, cap 2}`.
  - Sources: a tank's limited Provoke.
  - Interactions: downgrades to Challenged.
- **Challenged** — minor · **M2**
  - Effect: its attacks against anyone but the challenger have
    disadvantage; leaving the challenger's reach provokes even if it
    Disengaged.
  - Intensity: binary, one source, `goad` tag (so it works on mindless
    units and bosses).
  - Duration: `turns:2`, combatOnly. Ends when the challenger is downed,
    incapacitated, dead or more than 6 squares away, or targets another
    unit with a harmful effect; tonic.
  - Sources: tank challenges; bodyguard elites.
  - Interactions: Unyielding's composure window; the boss shed action can
    remove it.
- **Tormented** — minor · Later
  - Effect: tied to one source; N psychic damage at turn start. The tick
    is skipped, and Tormented ends, if the source is out of line of sight.
  - Intensity: valued.
  - Duration: `turns:3`. Ends on rally, Clear-minded, a tonic or
    `sourceGone`.
  - Sources: ghosts (cooldown); haunting spells.
  - Interactions: the damage over time that positioning answers.
- **Clear-minded** — beneficial · Later
  - Effect: blocks `mind` and `fear`; on application it ends minor and
    major mind and fear conditions (hard ones are left to Steeled).
  - Intensity: binary.
  - Duration: `turns:3`, or sustained.
  - Sources: Calm-Emotions-shaped spell (SRD).
- **Inspired** — token · Later
  - Effect: once, rerolls one of its own failed d20 tests and keeps the new
    roll.
  - Intensity: 1 charge.
  - Duration: fallback 3 turns; event `untilRest`. Its policy defaults to
    `ifItChangesOutcome` on saves.
  - Sources: Leadership mastery; shrines; Heroic-Inspiration-style starting
    boon (SRD 5.2).
  - Interactions: Shaken suppresses it.

### 18.9 H. Buffs and tokens

- **Dodging** — beneficial · **M1**
  - Effect: attacks against it have disadvantage; advantage on Reflex
    saves.
  - Duration: `untilSourceTurnStart` with itself as source; ends on
    Incapacitated.
  - Sources: the Dodge universal action **(settled, ability model 7.2)**.
- **Helped** — token · **M2**
  - Effect: its next attack roll or skill check has advantage; a roll that
    already has advantage does not use it.
  - Duration: `charges:1`, fallback `untilSourceTurnStart`, sourceTied.
    From the Vex verb the fallback is `untilSourceTurnEnd`, with the wearer
    as source, so it ends at the end of its next turn **(change W-7)**.
  - Sources: the Help action **(settled, ability model 7.2)**; the Vex verb
    **(change W-7; weapons design 7.1)**; trinkets (Trophy Tally, fallback
    `untilSourceTurnEnd` with the wearer as source; Help made cheaper by
    Sigil of Aid counts under the trinket limit) **(change TK-22; trinkets
    design 7.1, 13.7)**.
- **Spotted** — harmful token · Later
  - Effect: the next attack roll against it has advantage.
  - Duration: `charges:1`, fallback `untilSourceTurnStart`.
  - Sources: spotting at-wills; pack hunters.
  - Interactions: the same token type as Helped, on the other side.
- **Blessed** — beneficial · Later
  - Effect: +1d4 to attack rolls and saves.
  - Duration: sustained up to 10 turns.
  - Sources: Bless (SRD).
  - Interactions: cancels Baned.
- **Baned** — minor · Later
  - Effect: −1d4 to attack rolls and saves.
  - Duration: sustained, up to 3 turns.
  - Sources: Bane (SRD); cult priests.
  - Interactions: cancels Blessed.
- **Warded** — token · **M2**
  - Effect: the next harmful condition that would land on it is blocked
    (pipeline step 4). Not lingering conditions, Hasted's lethargy, Downed
    or Surprised.
  - Intensity: 1–2 charges.
  - Duration: fallback 3 turns.
  - Sources: ward spells; charms; bosses at phase change; monster affixes
    (`oncePerLife`, enemies design 7.2).
  - Interactions: the preview says "will spend ward"; a warded boss keeps
    its pips. A trinket-granted Warded has no effect on a unit already
    Warded; a unit receives at most one trinket-granted Warded per fight
    **(change TK-22; trinkets design 7.1)**.
- **Shielded** — token · Later
  - Effect: its next damage event resolves each part one ladder step toward
    resistant. Used only if it changes something.
  - Duration: `charges:1`, fallback 3 turns.
  - Sources: defensive Techniques; stoneskin-style potions.
- **Fortified** — beneficial · Later
  - Effect: +2 armour class; critical hits against it count as normal hits.
    Does not stack with the Raise shield property: the bonus is
    max(Fortified 2, Raise 2 unless Pry), so Fortified applies against Pry
    (armour design 6.1). Its crit downgrade comes first and spends nothing
    (armour design 8.1 rule 7) **(change AR-14)**.
  - Duration: up to 10 turns.
  - Sources: Shield-of-Faith-shaped spell (SRD); stances.
- **Resisting (type)** — beneficial · Later
  - Effect: resistant to one type; advantage on saves against that type's
    rider.
  - Duration: up to 10 turns.
  - Sources: Protection-from-Energy-shaped spell (SRD); potions.
- **Regenerating** — beneficial · Later
  - Effect: regains N hit points at turn start. This is healing, so it ends
    Bleeding after Bleeding's tick.
  - Duration: up to 10 turns.
  - Interactions: Withered suppresses it.
- **Bolstered** — beneficial · Later
  - Effect: N temporary hit points (shown on the hit point bar); a higher
    value replaces.
  - Duration: combatOnly, or 10 turns.
  - Interactions: Withered blocks gaining it.
- **Empowered** — token · Later
  - Effect: its next damaging hit is one ladder step weaker for that
    target, so its rider lands automatically (within ability model 9.3's
    bounds).
  - Duration: `charges:1`, fallback 3 turns; used on the first target hit.
  - Interactions: cancels Sapped.
- **Coated** — token · Later (needs the items design)
  - Effect: its next 3 weapon hits that attempt the coating's rider; a
    charge is spent only on such a hit (ability model 9.3 gates apply)
    **(Later; change CN-18)**.
  - Intensity: charges, cap 3.
  - Duration: `charges:3`, fallback `turns:3`.
  - Sources: consumable oils: `coating` consumables tagged `launcher`
    (special arrows and bolts) or `blade` (edge oils) (consumables design)
    **(change W-11)**. Coatings carry a named condition rider only; no
    element shift and no own-type rider flag (consumables design 7.5)
    **(Later; change CN-18)**.
- **Heightened (ability)** — beneficial · Later
  - Effect: +2 to one ability, within the +5 cap, and advantage on checks
    with that ability.
  - Duration: up to 10 turns.
  - Sources: Enhance-Ability-shaped spell (SRD) **(change CN-16)**.
  - Interactions: the highest bonus only **(settled, stat model 5.1)**.
- **Death-warded** — token · Later
  - Effect: the first drop to 0 hit points leaves it on 1 instead.
  - Duration: `charges:1`, always combatOnly, so it needs no fallback
    (5.3).
  - Sources: Death-Ward-shaped tier 3 spell (SRD); rare charm; elite
    enemies, never bosses (affix grants are `oncePerLife`, enemies design
    7.2).
  - Interactions: **gated on the death and healing design (change
    CN-15):** second link of the save chain (9.3), after Last Ward and
    before the talisman. The talisman also fires out of combat, unlike
    `combatOnly` Death-warded; recorded, deferred to the death and healing
    design (consumables design 14.2).
- **Sanctified** — beneficial · Later
  - Effect: an enemy targeting it must first pass a Will save or choose
    another target (its pool is not spent).
  - Duration: up to 10 turns; ends when it attacks or harms anyone.
  - Sources: Sanctuary-shaped spell (SRD).
- **Blurred** — beneficial · Later
  - Effect: attacks against it have disadvantage and spend a charge. Not
    spent on an attack that already has disadvantage.
  - Intensity: charges, cap 3.
  - Duration: `charges:3`, fallback `turns:3`.
  - Sources: Blur or Mirror-Image-shaped spell (SRD).
  - Interactions: Revealed overrides it.
- **Riposting** — beneficial · Later
  - Effect: when an adjacent enemy's melee attack misses it, it makes an
    at-will melee attack using **its reaction**.
  - Duration: `untilSourceTurnStart` with itself as source.
  - Sources: fencer stances.
  - Interactions: Staggered switches it off.
- **Retaliating** — beneficial · Later
  - Effect: an adjacent enemy that hits it in melee takes N damage of the
    shroud's type. This is not a hit and never carries a rider.
  - Duration: sustained up to 10 turns.
  - Sources: Fire-Shield-shaped spell (SRD).
- **Guarded** — beneficial · Later
  - Effect: while the guardian is adjacent, the first attack each round
    against the guarded unit is redirected to the guardian, using the
    guardian's reaction.
  - Duration: up to 10 turns, sourceTied.
  - Sources: Protect Technique.
  - Interactions: a Staggered guardian cannot redirect.
- **Raging** — beneficial · Later (class content)
  - Effect: resistant to slashing, piercing and bludgeoning; +2 damage on
    Strength hits; advantage on Athletics and Fortitude; cannot cast or
    concentrate.
  - Duration: sustained up to 10 turns; ends if it neither attacked nor was
    damaged since its last turn.
  - Sources: Rage (SRD).
- **Reckless** — mixed · Later (self toggle)
  - Effect: advantage on its Strength melee attacks; after a turn in which
    it attacked, attacks against it have advantage until its next turn.
  - Sources: Reckless Attack (SRD).
- **Inured** — beneficial · Later
  - Effect: advantage on saves against `poison` conditions; Envenomed ticks
    of 2 or less do nothing.
  - Duration: up to 10 turns.
  - Sources: antitoxin potions.

### 18.10 I. Exploration-scale (lingering)

- **Weary** — minor · **M2**
  - Effect: −1 per level to all d20 tests.
  - Intensity: accumulating, `levelCap` 2. A new application adds a level
    (6.5); a level-up never counts as a new lingering condition (14).
  - Duration: one level clears per short rest; camp clears all.
  - Sources: failed hazard checks, gas traps, draining monsters.
  - Interactions: adapted from SRD 5.2 Exhaustion.
- **Scarred** — minor · **M2** **(agreed)**
  - Effect: maximum hit points reduced by floor(maximum / 10), minimum 1,
    applied last as a modifier. Current hit points above the new maximum
    drop to it. **(lead's call)**
  - Intensity: binary. A Lame variant (−1 speed) is a later flavour.
  - Duration: `untilCamp`. Medicine Treat during a short rest removes it:
    one attempt per Scarred unit per short rest, by one healer, against a
    fixed difficulty in the rest data (illustrative 10 + floor). Treat
    resolves before Recovery at rest completion. A failed attempt can be
    tried again only at the next short rest **(lead's call, from stat model
    7.2's no free retries)**. A Remedy also ends it. **Gated on the death
    and healing design (change CN-14):** a `treatProof` instance (a
    talisman save) is not offered for Treat and uses no attempt; a Remedy
    or camp ends it and its flag (consumables design 14.6).
  - Sources: an engine grant (9.2) when a companion gets up from Downed, by
    either route (Medicine or a won fight), after its first downing of this
    floor. `downedThisFloor` is set by that first downing and resets on a
    floor change. Later downings on the same floor give nothing. **Gated on
    the death and healing design (change CN-14):** a talisman save (the
    holder, the hero included; consumables design 14.4). The hero is never
    Scarred by a downing (its downing ends the run); a talisman save may
    Scar it.
  - Interactions: exempt from the cap of 2 harmful lingering conditions:
    it always lands and does not count toward the cap **(lead's call, 14)**;
    Warded does not block it; while a companion is downed its harmful
    turn-counted conditions are suppressed.
- **Diseased (strain)** — minor · Later
  - Effect: a 3-stage list per strain (for example Grave Fever, Shaking
    Rot, Sight-blight). Stages cost numbers, Recovery or one disadvantage,
    never a turn part.
  - Intensity: worsens one stage at each short rest without treatment.
  - Duration: `untilCamp`; Treat during a rest cures it.
  - Sources: rats and plague undead (Fortitude save); tainted fountains.
- **Cursed (curse)** — minor · Later
  - Variants:
    - Ill-Omened: its first d20 test each fight has disadvantage;
    - Leaden: one named pool's maximum −1;
    - Ill-Starred (type): weak to one damage type;
    - Clinging: a cursed item cannot be unequipped (needs the items
      design).
  - Duration: `untilCamp`; a shrine, or Arcana at a rune (a failure gives
    Weary 1).
  - Sources: cursed loot, desecrated shrines.
  - Interactions: an item curse's cost is a `whileEquipped` item property,
    not a lingering condition. Clinging from an item is exempt from the cap
    of 2 harmful lingering conditions, as Scarred is (items design 13).
- **Restless** — minor · Later
  - Effect: its next short rest gives half Recovery and no Slot Recovery.
  - Duration: until that rest.
  - Sources: haunted floors.
- **Conspicuous** — minor · Later
  - Effect: its contribution to party passive stealth is −5 (stealth uses
    the worst member); hearing range of its fights +3.
  - Duration: `untilRest`; ended by Soaked or washing.
  - Sources: alarm dye, stench monsters, spores.
  - Interactions: cancels Hushed.
- **Well-fed** — beneficial · Later
  - Effect: maximum hit points + level, healed when gained.
  - Duration: until the first short rest or the next camp (event source,
    C5).
  - Sources: camp meal events.
- **Pitted Armour** — minor · Later **(change AR-11; armour design 9.4)**
  - Effect: the worn or natural armour value −1 per level (a gear-layer
    change), never below 10; never Dexterity, the shield or the unarmoured
    base. It does not land on a unit with no armour value; the rider then
    counts as not attempted.
  - Intensity: one instance with intensity 1–2 (6.5); a second landing
    raises it.
  - Duration: `untilCamp`; a Mechanics repair during a short rest
    (`pittedRepairDifficulty`), any member, one attempt per instance per
    rest (`lastRepairRestId`). A success removes the whole instance.
  - Sources: rust monster (SRD); the Crushing weapon property
    (enemy-facing; reduces the monster's armour value, natural armour
    included; Later); the acid ooze touch (Later) **(change W-16; weapons
    design 5.5, 17.10, 18.3)**. Rust lands only on `metal` armour (worn
    armour tagged `metal`, or natural armour by its `metal` flag); acid and
    Crushing land on any armour. Floor 2 and deeper, visible from the type
    ("Pits armour"); counts toward that type's 2 conditions.
  - Interactions: Blued (Later) makes an armour immune; stacks with
    Corroded (a different layer); shares the reduction cap of 4 with
    piercing by the reduction it actually applies (armour design 9.1);
    counts toward the cap of 2 harmful lingering conditions (14) and
    follows the unit.
- **Notched** — minor · Later **(agreed; change W-12; weapons design 5.6)**
  - Effect: the weapons in the weapon set that was active when Notched
    landed roll their dice one size smaller (minimum d4) and lose their
    verb. On a focus it suppresses only the focus verb. Shields and natural
    weapons are unaffected, except that a Reliquary Shield is a focus:
    Notched may land through it and suppresses Spare only, never the AC
    **(change AR-15; armour design 10.2)**.
  - Intensity: binary. The instance records the instanceIds of the items
    in that set when it lands (as Teetering records its hazard), not the
    set's slot. It applies only if the active set holds at least one weapon
    or focus it would affect (not a shield, a natural weapon or a Sure Grip
    weapon); otherwise it does not apply, takes no lingering slot, and the
    rider or effect counts as not attempted. A unit that already has
    Notched cannot gain it again ("No effect: already Notched", 6.3).
  - Duration: `untilCamp`. A Mechanics repair during a short rest ends it:
    fixed difficulty (`notchedRepairDifficulty`), one attempt per instance
    per rest, no free retry (stat model 7.2); any party member may attempt
    it, trained or not; the instance stores `lastRepairRestId`.
  - Sources: rust, acid and ooze-themed monsters only, floor 2 and deeper,
    visible from the monster type ("Notches weapons"); counts toward that
    type's 2 conditions (enemies design 9.2). Party-facing only.
  - Interactions: counts toward the cap of 2 harmful lingering conditions
    (14); swapping to the spare set (a bonus action) escapes it, as does
    handing a recorded weapon to an ally; the Sure Grip property makes a
    weapon immune. A condition on the unit, never item state.

### 18.11 Merged or cut

Record of the catalogue's shaping. **(lead's call)**

| Item | Outcome |
|---|---|
| Paralysed | Became Held, with no automatic critical hits |
| Dominated | Became Thralled |
| Berserk, random Confusion | Became deterministic Confused |
| Slowed (spell) | The spell applies Chilled + Dazed, each with `[{sustained: {max: 3}}, {saveEnds: {Will, cap 3}}]` enemy-facing |
| Muddled | Became Unfocused |
| Wracked | Folded into Shocked (an untyped curse preset is possible) |
| Goaded, Taunted, Pinned, Tethered | Folded into Challenged |
| Quickened | Became Spurred (Quickened is a distinctive Pathfinder name) |
| Steadied | Renamed Rallied |
| Healing blocked | Renamed Withered |
| Doomed | Renamed Condemned |
| Wounded | Became Scarred, with a different trigger |
| Dazzled | Renamed Squinting |
| Marked | Renamed Spotted |
| Weakened | Renamed Sapped |
| Sundered | Folded into Corroded's armour-category clause |
| Brittle | A preset of Exposed |
| Hunted | Moved to class features as a buff on the hunter |
| Hexed | Folded into Baned |
| Elusive | Folded into Fleet |
| Keen | Folded into Empowered |
| Lame | A Scarred variant |
| Noisy, Reeking | Merged as Conspicuous |
| Entranced | Asleep with the `noProne` flag |
| Delayed, Off-Balance, Slick, Focused, Siphoning, Jinxed | Cut: no niche, or the scorer cannot value them |
| Lean rest | Cut: a free refill |
| Encumbered | Retired: the hard slot cap replaces it; additions are blocked, nothing is ejected (items design 5.4) |
| Quarry | Moved to the enemies design (11.4): a no-effect AI focus telegraph, not a condition |
| Drained | Dropped as a name |

## 19. Interaction table

**(lead's call)**

| Kind | Condition | With | Result |
|---|---|---|---|
| implies | Stunned, Held, Asleep, Frozen, Petrified, Banished, Downed | Incapacitated | No action, bonus action, reaction or own movement; ends concentration, Hidden, held grapples and imposed Challenged |
| implies | Restrained | Rooted flags | Speed 0, not moved by forced movement |
| onApply | Asleep, Downed, Commanded (Grovel) | Prone | A separate Prone that outlives the parent |
| onApply | Grappled | Grappling on the holder | A linked pair |
| engine | Any hard instance (lockouts included), Surprised | Steeled | 1-turn immunity to the hard tier; an engine rule (7.2), not data |
| engine | Downed (got up, first downing this floor) | Scarred | Maximum hit points −10% (minimum 1) until camp |
| onEnd | Hasted (any end except a cancel) | Dazed | Lethargy turn |
| onEnd | Panicked | Frightened, 1 turn, same source | Steps down the fear ladder |
| cancel | Hasted ↔ Chilled | | Both removed |
| cancel | Blessed ↔ Baned | | Both removed |
| cancel | Burning ↔ Chilled; Burning ↔ Soaked | | Both removed |
| cancel | Smouldering ↔ Chilled; Smouldering ↔ Soaked | | Both removed |
| cancel | Empowered ↔ Sapped | | Both removed |
| cancel | Hushed ↔ Conspicuous | | Both removed |
| override | Revealed | Hidden, Invisible, Veiled, Blurred | Ended and blocked |
| override | Burning | Smouldering | Replaced |
| override | Frightened | Shaken | Escalation |
| override | Blinded | Squinting | Replaced |
| override | Restrained | Rooted | Replaced |
| override | Soaked | Corroded, Conspicuous | Washed off |
| override | Charmed | Frightened (same source) | Replaced |
| override | Engulfed | Grappled | Replaced |
| override | Rallied | `fear` tag | Ended and blocked |
| override | Clear-minded | Minor and major `mind`/`fear` | Ended and blocked |
| override | Unfettered | Minor and major `restraint`, Chilled, Mired | Ended and blocked |
| override | Prone | Flying, Jumping | Landing rule |
| override | Grounded, any condition that implies Incapacitated | Flying, Jumping, Levitating | Landing rule |
| suppress | Withered | Regenerating; heal ends of Bleeding and Condemned | Paused |
| suppress | Frozen | Bleeding tick | Paused |
| suppress | Petrified, Banished | All other conditions except engine states | Paused; countdowns continue |
| suppress | Downed | Harmful turn-counted conditions | Paused |
| suppress | Shaken | Inspired | Paused |
| suppress | Blinded | Frightened, Charmed (unless the source is adjacent) | Penalties off |
| suppress | Vigilant (observer) | Hidden, Invisible, Veiled within 3 squares | Treated as noticed |
| blocks | Steeled | Hard tier | Resisted (`steeled`) |
| blocks | Any hard condition, Surprised | Another hard condition | Resisted (`alreadyControlled`) |
| blocks | Unyielding | Hard (downgrades); a second turn-part major; a condition completing a lockout; turn-part majors and Challenged for 1 turn after one ends | Downgrade or resist |
| blocks | Party composure (7.2) | Hostile turn-part majors, until the end of the next turn after one ends | Resisted (`composure`) |
| blocks | Emboldened | Panicked, Routed | |
| blocks | Demoralised | Emboldened | |
| blocks | Withered | Bolstered | |
| blocks | Shredded | Warded, layers | |
| blocks | Grounded | Flying, Jumping, Levitating | |
| blocks | Levitating | Prone | |
| blocks | Anchored | Forced movement; Prone from shove, trip or collision | |
| blocks | Deafened | `sound` effects, Rally | |
| blocks | Mired | Dash, Disengage, slip past (including Hasted's) | |
| blocks | Vigilant | Surprised | |
| blocks | Aura save success | That aura's condition, for the fight | |
| blocks | Two harmful lingering conditions | A third harmful lingering condition (never Scarred) | |
| transform | Soaked + Chilled from a `freezing` source | Frozen | Consumes Soaked. Fallback Chilled; re-checked against Steeled, the depth gates and resolve |
| transform | Chilled + `freezing` cold | Frozen | Consumes Chilled. Fallback: no effect |
| transform | Frightened + a limited fear effect (enemy target) | Panicked | Consumes Frightened. Fallback: no effect |
| transform | Squinting + limited `searing` radiant | Blinded | Consumes Squinting. Fallback: no effect |
| staged | Calcifying stage 2 + failed save | Petrified | Not a transform (6.5). Ends Calcifying; Petrified enters at step 2; if blocked, stays at stage 2 |
| damage end | Fire ≥1 | Chilled, Frozen, Soaked, Oiled | Ends; on Oiled the hit takes +half and applies Burning one band higher; on Soaked the hit attempts no Burning rider. A ring-shifted fire part (trinkets design 9.4) does not pay out or end Oiled (Burning lands at its normal band) but ends Soaked as any fire hit **(change TK-25)** |
| damage end | Cold ≥1 | Burning, Smouldering | Ends |
| tick | Any harmful tick | Oiled, oil squares | Never: ticks are not hits (6.8) |
| damage end | Bludgeoning ≥1 | Frozen | Shatter: the shell's weakness (+half, once) and automatic Stagger |
| damage end | Breaker type | Restrained, Rooted | Ends (the unit's condition, never a web square) |
| damage end | Any damage | Asleep | Wakes after the damage resolves |
| apply end | Corroded applied | Frozen | Ends; also strips one layer |
| ladder | Soaked | Cold, lightning weak; fire resistant | Cold and lightning riders automatic; lightning arcs to all adjacent |
| ladder | Soaked + Oiled | Fire | Normal: no automatic Burning |
| ladder | Exposed or Brittle + Resisting or Shielded | | Net normal |
| ladder | Empowered + Shielded | | Net normal |
| terrain | Water square | Soaked | On enter and turn end; a Burning unit entering loses both (cancel) |
| terrain | Web square | Restrained | On enter and turn end; the square is never destroyed |
| terrain | Fire square | Burning | On enter and turn end |
| terrain | Oil-slick square (M2) | Oiled | On enter and turn end |
| terrain | Fire area or fire damage ≥1 on an oil square | The square | Becomes plain floor (11.4) |
| combo | Prone + any speed-0 condition | | Standing costs the action |
| combo | Grappled + hazard or terrain square | | M1: the drag stops before it. M2: a drag into it gives the hazard-edge save; a success ends the grapple (10.1) |
| combo | Teetering + forced movement | | Hazard save at disadvantage |
| combo | Stunned or Frozen ally + Reflex area | | Automatic fail (Stunned) or resistance (Frozen) |
| combo | Withered + Bleeding or Condemned | | The heal end is locked out (slashing plus necrotic) |
| combo | Restrained + area | | Reflex disadvantage |
| combo | Staggered | Opportunity attacks, Guarded, Riposting, Challenged and Harried punishments | All switched off |
| combo | Distracted + Nauseated | | Composite lockout: treated as hard, including the depth gates and the per-fight cap |
| combo | Unfocused after the psychic rider | | Concentration broken and cannot restart |

## 20. Validation, testing and simulator

**(lead's call)**

### 20.1 Content validation

The build fails when:

- a condition, or an application-level override, breaks its tier band
  (5.3), including the party-facing hard limit, party-facing turn-part
  majors, save-ends caps, decay starting values, charges fallbacks and the
  out-of-combat limit (5.5);
- monster or trap hard control, or a hard-capable property such as
  `freezing` (7.1), is placed where the depth gates forbid it against the
  party (7.2, 11.6);
- a terrain square applies a hard condition (11.2);
- a staged duration appears on any condition but Calcifying (6.5);
- a save-ends save is one the condition auto-fails;
- a hard condition has other than exactly one downgrade;
- a hard condition comes from an at-will, a reaction or a terrain square
  (7.1);
- `worsen` appears on an ability that is not limited or on cooldown;
- a cancel is not declared on both sides, or a relation partner does not
  resolve;
- a transform has no fallback or two relations cover the same pair;
- a tag is not in the registry (12.1);
- a trait set breaks the tag immunity budget (12.2);
- agency appears in party-facing content;
- a lingering condition denies a turn part;
- a harmful condition longer than 1 turn has no in-fight end besides
  waiting, outside the two exemptions (8);
- a content budget is broken (16), counting terrain conditions per theme
  and each `family` once;
- a terrain square's condition does not resolve, has no trigger, or exceeds
  its floor's band;
- condition IDs are not unique;
- a condition lacks AI, `controlRank`, `denies` or display data, or a boss
  lacks `resolveThreshold`.

### 20.2 Tests

- **Unit tests:**
  - clock edge cases: own turn, reaction, boss action after the hero's
    turn (costs the next turn), source died, out of combat, an aura's
    turn-start reapplication counting that turn, a condition gained in the
    turn-end step (Calcifying, terrain) not counted that turn;
  - pipeline order, including the re-check: freezing on a Soaked, Steeled
    unit gives Chilled, and on a Soaked boss with pips spends a pip; a
    floor 1 monster's freezing attack on a Soaked hero gives Chilled; an
    Unyielding downgrade is re-checked against composure;
  - each entry point in 9.2's table skips exactly the listed steps;
  - the tick batch: seq order, the cap reducing later ticks, one
    concentration check, no Oiled payout;
  - each relation kind;
  - the one-transform rule;
  - composite lockout, including that it counts toward the per-fight cap
    and is resisted from a floor 1 monster;
  - the per-fight cap: a lockout turn start counts; an own-turn hard
    condition, Surprised, Downed and an ally's freeze do not;
  - party composure after Dazed;
  - resolve by each route (save and skill check in M1, fixtures for the
    rider route);
  - an opportunity attack missing a Riposting unit;
  - terrain: `onEnter` fires once per entry, including forced entry;
    `onTurnEnd` runs last; an oil square becomes plain floor after fire; a
    web square survives a broken binding;
  - M1 pushes and drags stop before hazard and terrain squares; an M2 drag
    into a hazard gives the edge save and ends the grapple on a success;
  - Scarred: only the first downing per floor; the maximum and current hit
    points, including a maximum of 9 (reduction 1); Treat at rest, one
    attempt per rest; it lands with the lingering cap full.
  - In M1, the fixture conditions (18.1) cover every relation kind and
    duration model.
- **Property tests:**
  - the preview matches resolution, including terrain on a movement path;
  - no party unit loses more than 1 consecutive turn start to hard
    control;
  - no unit loses more than 2 turn starts per fight (5.7), lockouts
    included;
  - after a hostile turn-part major ends on a party unit, its next turn
    holds no hostile turn-part major.
- **Replay tests** across rests, camp and floor changes, so per-floor
  Scarred state and burned oil squares are reproduced.

### 20.3 Simulator reports

Each tied to a risk:

- turns lost to hard control, and the longest chain;
- turn parts lost to majors per unit per round;
- turns spent Grappled and Prone;
- end reasons, and the share of conditions that expire with no effect (the
  shipping gate for Later conditions);
- cures and tonics used per fight;
- resolve pips spent;
- concentration checks per round;
- hero deaths after a lost turn, and from hazards and terrain;
- conditions applied by terrain per fight;
- lingering penalties carried into boss fights, Scarred included;
- every condition chosen by the AI in at least one scenario.

## 21. Staging

**(lead's call)**

- **M1** (the slice, no riders): the 10 M1 conditions and 4 engine states;
  the wearer clock, the phase counter and own-turn rule; `turns`, window,
  `trigger` and `sustained` durations in content, and every duration model
  and relation kind through test fixtures (18.1); the whole pipeline;
  Steeled, the per-fight cap, composite lockouts, depth gates, party
  composure and Unyielding; resolve by the save and skill-check routes;
  ticks, values, the tick cap and re-application through Bleeding; the AI
  control value; validation; plate, preview and tooltip display. Forced
  movement (10.1); pushes and drags stop before hazard squares. No
  condition terrain.
- **M2:** the 22 M2 conditions and Downed, Flying and Jumping; riders;
  Treat and tonics; Scarred and its rest-screen support; water, web, fire
  and oil-slick squares with the burn-away rule; hazard edges, landing and
  drags into hazards; boss shed actions; lingering conditions and rest
  cures; the four morale states (enemies design 14).
- **Later:** the remaining catalogue, each behind the simulator gate.

## 22. Risks

- **The catalogue outgrows the screen.** Over a hundred conditions are
  conceived. The budgets (16), the icon rules and the simulator's "expired
  with no effect" gate are the guard. Without them, the target of four
  things to read per unit (ability model 11.4) fails.
- **The AI decides whether conditions matter.** If the T definition or a
  hook is wrong, a condition is never chosen, and boss pips are spent on
  the wrong effects. Mitigation: a simulator check that every condition is
  chosen in at least one scenario, and a scenario where Hampered on a boss
  with a ready cooldown must score above its threshold.
- **Stacked majors can still dominate** short of a composite lockout.
  Dazed + Silenced on a caster is an example. Party composure (7.2) blunts
  it against the party, not against monsters. Watch turn parts lost per
  unit per round.
- **Damage over time still bites a fragile level 1 hero.** The band caps,
  the per-turn tick cap and the 1 hit point floor out of combat need tuning
  in the simulator. M1's Bleeding is the test.
- **Terrain makes combos common.** Water, web, fire and oil squares raise
  how often Soaked, Restrained, Burning and Oiled appear, and water makes
  Frozen reachable; the runtime depth gates (7.2) keep that off floor 1.
  The per-theme budget counts them, and the simulator reports terrain
  conditions per fight. The AI must value terrain on a path and as a push
  destination (17.3), or companions' suggestions will walk into fire.
- **Scarred stacks with attrition.** A companion downed early on each floor
  carries −10% maximum hit points into the boss. Watch lingering penalties
  carried into boss fights.
- **Pre-buffing returns through event boons** if shrines and events are
  generous. The one-lingering-boon rule is the lever.
- **Tonic supply becomes the real balance knob.** Track tonics used per
  fight; pricing belongs to the items design.
- **Rules owned by designs that do not exist yet:** hazard placement
  (dungeon generation); Disarmed, Coated, Clinging,
  Pitted Armour and tonics (items; Disarmed's rules are now in weapons
  design 16 and Pitted Armour's in armour design 9.4, both still Later);
  the rest of death and healing.
- **Protected content.**
  - Generic names that match other games' conditions need a check before
    shipping. Dazed is the one kept; the fallback name is "Addled".
  - Every spell shape cited (Bless, Bane, Haste, Longstrider, Shield of
    Faith, Protection from Energy, Death Ward, Sanctuary, Blur, Mirror
    Image, Fire Shield, Invisibility, See Invisibility, Freedom of Movement,
    Calm Emotions, Enhance Ability, Pass without Trace, Hold Person, Sleep,
    Banishment, Command, Confusion, Charm Person, Dominate Person, Fear,
    Silence, Faerie Fire, Entangle, Web, Ray of Enfeeblement, Rage, Reckless
    Attack, Exhaustion, Heroic Inspiration, the rust monster, the gelatinous
    cube's Engulf) must be checked against SRD 5.1 and 5.2 and attributed.
  - No product-identity monsters: original descriptions only.
- **The changes are interlocked.** The wearer clock (C1) is the change most
  catalogue entries depend on. If it is not applied as written, every
  turn-counted entry and the Steeled guarantee must be rewritten.

## 23. Left to the builders

- All numbers: band tables, durations within the tier bands, tick values,
  difficulties, `denyPct` values, `controlRank` values,
  `resolveThreshold`s and pip values, the Scarred percentage within
  playtest tuning (its minimum of 1 is a rule), terrain values.
- Each rider's save type (settled, stat model 12).
- Icons, model effects, colours and plate layout within section 15.
- Which terrain squares each theme uses, within the budgets.
- Hazard placement in floor generation.
- The JSON schema, built by the core developers with the content designer.

## 24. Changes to approved documents

Applied on 2026-11-16, when this document was approved. Each change names
the exact section it touches.

| # | Change | Document and section |
|---|---|---|
| C1 | Conditions on units, sustained ones included, count down on the wearer's turns (`turns:N`, counted by the phase test of this document's 5.2); windows (`untilSourceTurnStart`/`End`) run on the source's turns; a hard condition applied during the wearer's own turn ends at that turn's end and Steeled follows. The source clock and `rounds:N` remain only for auras, concentration effects themselves and effects not on a unit. The 10-round limits on beneficial effects on allies read as 10 of the wearer's turns. | Main design 4.3, bullet "In combat, durations count in rounds" (conditions on units count the wearer's turns; windows, auras and concentration effects the source's); ability model 9.6, first bullet (`turns:N` for conditions on units) and second bullet ("In combat, a round counts down…"); ability model 9.6, last bullet, and 14.5, bullet "a beneficial effect longer than 10 rounds lacks concentration" (10 of the wearer's turns) |
| C2 | Resolve: a pip is spent whenever a condition whose `controlRank` is at or above the boss's `resolveThreshold` (static data) would land, by any route: failed save, successful skill check, or automatic weak-target rider; it cancels only that condition. Every boss has Unyielding (downgrades, composure window, lockout resistance). Phase changes end all harmful conditions and grant Steeled, after the ability use that caused them. | Ability model 12.2, "Resolve" bullet (replaces "control value is at or above a threshold"); ability model 12.2, new "Unyielding" bullet, and "Phases" bullet (add the phase-change effect); pointers to this document's 12.3 |
| C3 | Same-number rule for condition modifiers: worst penalty and best bonus per derived number; highest dice bonus per roll; 5E advantage rule; one speed halving, speed 0 overrides. Gear and traits stack as before. | Stat model 9, bullet "Other derived numbers stack normally…" |
| C4 | New Medicine use in a fight: Treat, an action, adjacent; one roll against each instance's recorded difficulty; ends poison conditions, Bleeding, Withered, Hobbled, Condemned and Calcifying (stages 1–2). At a short rest: Diseased and Scarred (fixed difficulty, one attempt per Scarred unit per rest). | Stat model 7.1, Medicine row, "In a fight" and "In the dungeon" columns; ability model 7.2, new table row |
| C5 | Event boons (shrines, floor and camp events) and the starting boon may last until rest or camp without concentration; self-only toggles are exempt from the 10-round rule; one lingering boon per unit. | Ability model 9.6, last bullet; ability model 14.5, bullet "a beneficial effect longer than 10 rounds lacks concentration" |
| C6 | Condition-tag immunity budget (2 per non-boss trait set, at most 1 control tag; theme rule); Mindless also grants `mind`; "the one immunity allowed outside bosses" means damage types. | Stat model 8.4, "mindless" bullet and budget bullets |
| C7 | Every ladder shift that comes from a condition (including Soaked, Oiled, Exposed, Resisting, Shredded, Sapped, Shielded, Raging, Petrified, Ill-Starred and Frozen's shell) is not a trait or layer and does not count toward the two-resistance budget. | Stat model 8.4, budget bullet; stat model 8.5, last bullet |
| C8 | The psychic rider also applies Unfocused for 1 turn, and is attempted on a concentrating target or one not already Unfocused. | Stat model 8.1, Psychic row; ability model 9.3, bullet "A rider is not attempted on a target that already has that condition" (add the psychic exception and `worsen`) |
| C9 | Clarifies riders: Stagger means no reactions until the end of the target's next turn; the radiant rider applies Revealed to a hidden or invisible target and Blinded otherwise. | Stat model 8.1, Bludgeoning and Radiant rows |
| C10 | All turn-start ticks together are one damage event for concentration. | Ability model 9.5, bullet "One check per damage event" |
| C11 | Fixed terrain: static map squares may apply minor and major conditions on enter or turn end; squares never spread or change, except that fire burns an oil square to plain floor. The held-back item becomes "spreading or evolving surfaces". Abilities still leave nothing on the ground. | Stat model 2, Non-goals, bullet "Terrain surfaces such as burning oil or conducting water"; ability model 2, Non-goals, bullet "Terrain surfaces and persistent ground zones"; ability model 8.4 (add: fixed map terrain is not a ground zone); main design 2, Non-goals, new bullet "Spreading, evolving or player-placed ground surfaces; fixed condition squares are allowed (conditions design 11)" (the client's answer referred to this as a main design non-goal; until now it lived only in the stat and ability models); main design 4.6, pointer that theme hazards include condition terrain |
| C12 | A companion that gets up from its first downing on a floor gains Scarred (maximum hit points −10% until camp; Treat at a short rest removes it). | Main design 4.2, bullet on downed companions; stat model 10, "Death and healing" bullet (pointer to this document) |
| C13 | The conditions list no longer depends on an undesigned system. | Ability model 20, "Conditions" bullet; stat model 10, "Conditions" bullet (both point to this document) |
| C14 | A condition's control value is computed (this document's 17.3): `denyPct` is set in the condition's data and the rest is computed; resolve no longer reads the scorer. | Ability model 13, "Units" bullet ("A condition's control value per round is set in the condition's data") |
| C15 | The out-of-combat monster behaviour "asleep" is renamed "dormant", so it is not confused with the Asleep condition. | Main design 4.3, bullet "Enemies outside combat are asleep, stationary or patrolling" |

## 25. Depends on systems not yet designed

- **Items and inventory:** the generic layer is settled by the items design
  (2026-11-16). Open in its category designs (items design 17): Disarmed,
  Coated, Clinging.
  Weapons and foci are no longer pending: see the weapons
  design (2026-11-17). Disarmed and Coated stay Later; starting
  kits remain open in their category designs **(change W-15)**. Armour and shields are no longer
  pending, and Pitted Armour is settled: see the armour design
  (2026-11-17, 9.4) **(change AR-16)**. Consumables, scrolls and wands
  are settled in the consumables design (2026-11-17); coatings and Coated
  stay Later **(change CN-26)**.
- **Enemies:** morale (Demoralised, Routed, Emboldened, Cowed) is resolved
  by enemies design 14; elite definitions for the depth gates (7.1) and
  enemy condition kits within the budget (9.2) are settled there too.
- **Dungeon content:** traps (11.6), their set-piece status for the depth
  gates, and which floors hold them.
- **Dungeon generation:** hazard and terrain placement by theme, and the
  hazard lethality guarantee.
- **Death and healing:** the rest of it. Scarred is settled here.
- **Boss actions** (M2): the shed action.

## 26. Open items for the client

None. Approved by the client on 2026-11-16.
