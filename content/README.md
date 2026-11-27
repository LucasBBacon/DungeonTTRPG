# content/

JSON data files for the game: classes, species, feats, items, monsters and tables (MAIN 6.1), and the validation registries. Content is loaded and validated by `dotnet test` (`RepositoryContentTests`) and by the `dotnet run --project src/Sim -- content-hashes` command; MAIN 6.5's load at game startup arrives when the game first reads content. A bad reference or a missing field fails loudly with the file and field named (MAIN 6.5).

## Layout

| File | Holds |
|---|---|
| `effects/effects.json` | `{"effects": [...]}`: the effect definitions, one `EffectDefinition` per effect, `{id, kind, modifiers?, triggers?}` with content ids; the vocabulary (modifier operations, trigger forms, source kinds and their stacking) is [`docs/effects-vocabulary.md`](../docs/effects-vocabulary.md). Each `applyEffect` must name an effect of this file; modifier targets, trigger events and hook ids are checked against the code registries when the rules context is built |
| `registry/checks.json` | `{"checks": [...]}`: the check registry, one `CheckRow` per registered check (SV 9.1) |
| `registry/reports.json` | `{"reports": [...]}`: the report registry, one `ReportRow` per report (SV 9.1) |
| `registry/arms.json` | `{"arms": [...]}`: the simulator-arm registry, one `ArmRow` per arm (SV 9.1) |
| `registry/streams.json` | `{"streams": [...]}`: the stream registry, one `StreamRow` per draw stream, keyed node or keyed-hash domain (SV 9.1); generation reads it, so it is part of `genContentHash`; `dotnet run --project src/Sim -- stream-handles` generates the typed stream handles (`src/Core/Random/StreamHandles.g.cs`) from it, and analyzer rule DUN012 reports `KeyAlgebra.Derive`, `H` and `Absorb`, `DrawStream.Open`, and any `new`, explicit `default` or `with` of a `DrawStream`, `StreamHandle` or generated key struct, outside the files named by Core's `DungeonKeyDerivationFiles` (`Random/KeyAlgebra.cs`, `Random/KeyedSort.cs`, `Random/StreamRegistry.cs` and `Random/StreamHandles.g.cs`) |
| `naming/name_list.json` | `{"entries": [...]}`: the naming-pass name list, generated from its appendix by `dotnet run --project src/Sim -- name-list`; do not edit by hand |
| `naming/retired_names.json` | `{"retired": [...]}`: the retired (name, category) pairs, generated with the name list |
| `naming/greyed_reasons.json` | `{"reasons": [...]}`: the greyed-reason registry, `{id, doc}`; each reason needs the string `<doc>.greyed.<id>` |
| `strings/en.json` | `{"strings": [...]}`: the English strings table, `{key, text, field}`, with `text` in NFC |

Every file belongs to exactly one content kind (`ContentKinds.All` in `src/Core/Content`). A file no kind reads is an error.

## Loader rules

- Core is given each file's text and its name relative to this folder (`/` separators, ASCII); the host reads the disk. Files are taken in ordinal name order, so the order they are found in never matters.
- JSON is strict: no comments, no trailing commas, no unknown properties and no repeated properties.
- Numbers read by generation or rules are integers (DS13). A string, a fraction or a value too large for the field is refused. A rational is a JSON string in canonical form: an integer such as `"2"`, or a reduced `"p/q"` with `q > 1`, such as `"3/4"`.
- Every rules string is ASCII (SV 4.6). Display fields (names, strings-table text) may be any UTF-8 in NFC.
- Every record has a stable string id, unique within its kind. Registry ids follow their own grammars: checks `Vn.Name` or `content.Part.Part`, arms `Axis.Value`, reports a letter then letters, digits and dots. Other content ids are `[a-z0-9_.:-]` (SV 4.1 K-rule 3).
- References (a check's `deps[]`, `uses[]` and `proofRef`; a report's `arms[]`; an effect trigger's `applyEffect`) are resolved after every file has loaded.
- Every error names the file and the field as a JSON path, such as `registry/checks.json` at `$.checks[1].phase`.

Every present rules string is non-empty: a blanked value is as missing as an absent one. A field with nothing to say is left out where the schema makes it optional; a required field that can mean "no change" (an arm's `configDelta`, for a baseline arm) writes `"none"` until its schema arrives with M1-06.
