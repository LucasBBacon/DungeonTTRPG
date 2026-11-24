# Implementation Roadmap: Design

Date: 2026-11-24
Status: approved by client, 2026-11-24 (structure agreed in conversation)

## Intent

The approved designs in `docs/superpowers/specs` say *what* the game is,
but not the order in which to build it. The team needs one clear, linear
build sequence that a person or an agent can pick up and know exactly
what to build next and when it is finished.

## Working rules (client)

- Waterfall: steps run strictly in order. No step starts until the step
  before it is fully tested and working.
- No dates or time estimates. Every step has explicit completion
  conditions instead.
- Progress is tracked in GitHub (issues and, if useful, a GitHub
  Project).

## Approaches considered

| Approach | Trade-off | Outcome |
|---|---|---|
| A. One roadmap file | Simple, but long; an agent must load all of it to work one step | Not chosen |
| B. Roadmap plus every detailed task plan written now | Nothing left to plan, but huge and stale before use: later plans depend on code not yet written | Not chosen |
| C. Index plus one file per milestone; detailed task plans written just before each step | Small files to load; plans always written against real code; a few files to keep consistent | **Chosen** |

## Design

### Files

`docs/roadmap/`:

- `README.md` (the index): how to use the roadmap, the shared definition
  of done, how to read design references, and every step in build order
  with links. No status column: status lives only in GitHub.
- `m0-skeleton.md`, `m1-vertical-slice.md`, `m2-full-run.md`,
  `m3-between-runs.md`, `m4-breadth.md`: one per milestone, its steps in
  build order.

### Steps

Every step has a permanent ID (`M1-04`) and a fixed shape:

- **Goal:** one sentence.
- **Implements:** the design sections and change IDs it builds.
- **Depends on:** the step before it (and any other it reads from).
- **Delivers:** the concrete things that exist when it is done.
- **Done when:** a checklist of checkable conditions (tests, CI, a
  playtest check). Every item can be verified by someone other than the
  builder.
- **Out of scope:** what a reader might expect here but belongs to a
  named later step.

A step is sized to deliver one testable capability. Each milestone ends
with an `Mx-END` step: the milestone's sign-off checklist (seeds and
validation design 7) and its playtest.

### Detailed task plans

Only the step about to start gets a task-by-task plan (the M0 plan's
format, made with the writing-plans skill), saved in
`docs/superpowers/plans/` and linked from the step's issue.

### GitHub

One issue per step, titled with its ID and name. The body links to the
step's section and copies its "Done when" checklist. An issue closes only
when every box is ticked and CI is green.

## Calls left to the lead (marked as such)

- Step boundaries and order within a milestone, where the designs do not
  fix them.
- M4 is the thinnest milestone in the designs; its steps may stay
  coarser until M3 ends.
- The roadmap introduces no new rules. Where it finds a gap or conflict
  between designs, it records it as an open item instead of deciding it.
