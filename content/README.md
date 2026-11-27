# content/

JSON data files for the game: classes, species, feats, items, monsters and tables (MAIN 6.1), and the validation registries. Content is loaded and validated at startup; a bad reference or a missing field fails loudly with the file and field named (MAIN 6.5).

## Layout

| File | Holds |
|---|---|
| `registry/checks.json` | `{"checks": [...]}`: the check registry, one `CheckRow` per registered check (SV 9.1) |
| `registry/reports.json` | `{"reports": [...]}`: the report registry, one `ReportRow` per report (SV 9.1) |
| `registry/arms.json` | `{"arms": [...]}`: the simulator-arm registry, one `ArmRow` per arm (SV 9.1) |

Every file belongs to exactly one content kind (`ContentKinds.All` in `src/Core/Content`). A file no kind reads is an error.

## Loader rules

- Core is given each file's text and its name relative to this folder (`/` separators, ASCII); the host reads the disk. Files are taken in ordinal name order, so the order they are found in never matters.
- JSON is strict: no comments, no trailing commas, no unknown properties and no repeated properties.
- Numbers read by generation or rules are integers (DS13). A string, a fraction or a value too large for the field is refused. A rational is a JSON string in canonical form: an integer such as `"2"`, or a reduced `"p/q"` with `q > 1`, such as `"3/4"`.
- Every rules string is ASCII (SV 4.6).
- Every record has a stable string id, unique within its kind. Registry ids follow their own grammars: checks `Vn.Name` or `content.Part.Part`, arms `Axis.Value`, reports a letter then letters, digits and dots. Other content ids are `[a-z0-9_.:-]` (SV 4.1 K-rule 3).
- References (a check's `deps[]`, `uses[]` and `proofRef`; a report's `arms[]`) are resolved after every file has loaded.
- Every error names the file and the field as a JSON path, such as `registry/checks.json` at `$.checks[1].phase`.
