# M1-03 Canonical Writer and Digests Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** One canonical binary serialiser, `CanonicalWriter`, with its reader and a SHA-256 `Digest`, so that every later digest (stage digests, `GenInputs`, static-file hashes, replay core-state hashes) and every journal delta is built from the same bytes.

**Architecture:** `src/Core/Canonical/` holds `CanonicalWriter` (append-only byte buffer: little-endian fixed-width integers, ASCII strings, absent/present tags, length-prefixed lists and sorted sets, records whose fields are written in declared ordinal order and counted), `CanonicalReader` (its strict mirror, rejecting any byte string the writer could not have produced), `ICanonical`, `CanonicalFormatException`, and `Digest` (SHA-256 over the canonical bytes, full 256 bits stored, 128 bits shown). The first production consumer is `StateDigest.Of(GameState)`, the replay core-state hash.

**Tech Stack:** C# 12 / .NET 8, `System.Buffers.Binary.BinaryPrimitives`, `System.Security.Cryptography.SHA256`, xUnit. Must stay green under the M1-01 determinism analyzer (rules DUN001–DUN011, including generated code once PR #90 merges).

**Spec:** [`docs/roadmap/m1-vertical-slice.md` — M1-03](../../roadmap/m1-vertical-slice.md#m1-03-canonical-writer-and-digests), implementing SV 4.5 "The canonical byte serialisation and the digest function" (`docs/superpowers/specs/2026-11-23-seeds-validation-design.md`, line 695), DS4; SV 4.6 (rules-relevant strings are ASCII).

## Global Constraints

- SV 4.5, verbatim: "It writes little-endian fixed-width integers, fields in a declared schema ordinal (never reflection order), collections in their DS4 sorted order with a length prefix, and an explicit tag for an absent or optional field. Every digest is SHA-256 over those bytes, stored in full and shown truncated to 128 bits."
- DS4: "Lists are sorted by ordinal comparison of ASCII stable ids. Every sort has a total key … No unordered iteration."
- SV 4.6: every rules-relevant string field is ASCII. Display text never goes through `CanonicalWriter`.
- One writer for every digest and delta. No second serialiser for digests; JSON saves (`Snapshot`) stay as they are and are not digested.
- Determinism analyzer stays green: no floating point, no `GetHashCode`/`HashCode`, sorts only with `StringComparer.Ordinal` or a `[TotalComparer]`, no signed `/` or `%`, no Dictionary/HashSet enumeration.
- Out of scope: the pinned `FloorStatic` vector (M1-33), journal records and rolling replay hashes (later steps), stage digests (M1-27).

## The byte format (plan-level call; locked by the vectors in Task 1)

| Value | Bytes |
|---|---|
| `UInt8` | 1 byte |
| `Bool` | 1 byte, `0x00` false, `0x01` true |
| `Int32` / `UInt32` | 4 bytes little-endian (two's complement for signed) |
| `Int64` / `UInt64` | 8 bytes little-endian |
| `String` | `UInt32` byte length, then the ASCII bytes (U+0000–U+007F only) |
| Optional | `0x00` when absent; `0x01` then the value when present |
| List (order is data) | `UInt32` count, then each element |
| Set (order is not data) | `UInt32` count, then elements sorted by ordinal id (`string.CompareOrdinal`) or by `ulong` key ascending; duplicate ids/keys are an error |
| Record | its fields back to back, no header; fields are written in ordinal order 0, 1, 2 … and the count is checked against the declared field count |
| Enum | its member name as a `String` (stable if members are reordered) |

Inside a list or set each element is one value written with ordinal 0 (the writer opens a one-field frame per element).

## Plan-level calls (flagged for review)

1. **The format above.** SV 4.5 fixes the rules but not the encodings; these are the simplest choices that satisfy it. Records carry no per-field tags or schema header: the declared ordinal and field count are checked in code, which is what stops a field reorder from silently changing bytes.
2. **Strings are ASCII-only** (SV 4.6), so there is no Unicode normalisation question. A non-ASCII string is an `ArgumentException` on write and a `CanonicalFormatException` on read.
3. **The reader is strict**: truncated input, a tag other than 0/1, an unsorted or duplicate set, a length beyond the remaining bytes, or trailing bytes all throw `CanonicalFormatException`. So `Write(Read(b)) == b` for every accepted `b`.
4. **`Digest` is a value type of four `ulong` words** (big-endian from the SHA-256 bytes, so hex output follows byte order), giving value equality without arrays. `ToString()` is the 128-bit display form.
5. **First consumer:** `StateDigest.Of(GameState)` (the replay core-state hash of SV 4.5). Its test checks properties, not a pinned vector, because `GameState` grows in every M1 step; byte stability is pinned on a test-only schema instead.

## Review Focus

1. **Silent field reorder or omission:** writing ordinals out of order, skipping one, writing too many, or ending a record early must throw `InvalidOperationException` naming the expected and actual ordinal. Tests in Task 1.
2. **Set input order:** the same set given in any order gives the same bytes and the same digest; duplicate ids throw. Test in Tasks 1 and 3 (the done-when).
3. **Hostile bytes:** truncated buffers, a declared length of `0xFFFFFFFF`, a present-tag of `0x02`, a bool of `0x02`, a non-ASCII byte in a string, an unsorted set, and trailing bytes are rejected with `CanonicalFormatException` and never allocate the declared length. Tests in Task 2.
4. **Platform independence:** bytes come from `BinaryPrimitives` little-endian calls, never `BitConverter` or `MemoryMarshal` (host endianness). Byte vectors in Task 1 run on both CI OSs.
5. **Enum and negative values:** `Int32(-2)` is `feffffff`, `Int64(-1)` is eight `ff` bytes, and an enum is written by name, so reordering `Faction` members does not change a digest. Tests in Tasks 1 and 3.

---

### Task 1: `CanonicalWriter` and the byte vectors

**Files:**
- Create: `src/Core/Canonical/ICanonical.cs`, `src/Core/Canonical/CanonicalWriter.cs`
- Test: `tests/Core.Tests/CanonicalWriterTests.cs`

**Interfaces:**
- Produces (namespace `Dungeon.Core.Canonical`):
  - `public interface ICanonical { void WriteCanonical(CanonicalWriter w); }` — an implementation calls `w.BeginRecord(n)`, writes fields 0…n−1, then `w.EndRecord()`.
  - `public sealed class CanonicalWriter` with: `byte[] ToArray()`; `static byte[] Serialize(ICanonical value)`; `void BeginRecord(int fieldCount)`; `void EndRecord()`; field writers, each taking `int ordinal` first: `UInt8(int, byte)`, `Bool(int, bool)`, `Int32(int, int)`, `UInt32(int, uint)`, `Int64(int, long)`, `UInt64(int, ulong)`, `String(int, string)`, `Enum<TEnum>(int, TEnum) where TEnum : struct, System.Enum`, `OptionalInt32(int, int?)`, `OptionalUInt64(int, ulong?)`, `OptionalString(int, string?)`, `OptionalRecord<T>(int, T?) where T : class, ICanonical`, `Record<T>(int, T) where T : ICanonical`, `List<T>(int, IReadOnlyList<T> items, Action<CanonicalWriter, T> writeItem)`, `SetById<T>(int, IEnumerable<T> items, Func<T, string> id, Action<CanonicalWriter, T> writeItem)`, `SetByKey<T>(int, IEnumerable<T> items, Func<T, ulong> key, Action<CanonicalWriter, T> writeItem)`.
  - Errors: wrong ordinal, too few/many fields, a write outside any record, or a collection element that writes ≠ 1 value → `InvalidOperationException` (programming error); non-ASCII string or null string → `ArgumentException`/`ArgumentNullException`; duplicate set id/key → `ArgumentException` naming it.

- [ ] **Step 1: Write the failing tests** in `CanonicalWriterTests`. Define in the test file `Sample : ICanonical` (9 fields) and `Inner : ICanonical` (2 fields) exactly as below, then:
  - `Sample_bytes_are_pinned`: `CanonicalWriter.Serialize(sample)` as lowercase hex equals
    `efcdab8967452301feffffff09000000756e69742e6865726f0001070000000200000001000000610100000062020000000300000000000000ffffffffffffffff01ff0100000000000000` (75 bytes), where `sample` writes: 0 `UInt64 0x0123456789ABCDEF`; 1 `Int32 -2`; 2 `String "unit.hero"`; 3 `OptionalInt32 null`; 4 `OptionalInt32 7`; 5 `SetById` over `["b", "a"]` (each element `String(0, s)`); 6 `List<long>` `[3, -1]` (each `Int64(0, x)`); 7 `Bool true`; 8 `Record` `Inner { 0: UInt8 255, 1: UInt64 1 }`. (Computed by an independent Python model of the table above.) Note: field 4 is `OptionalInt32(4, 7)`, encoded `01 07000000`.
  - `Set_input_order_does_not_change_bytes` (Review Focus 2): `SetById` over `["b","a","c"]` and `["c","a","b"]` give identical bytes; `SetByKey` over keys `[5,1,3]` and `[3,5,1]` likewise; duplicates `["a","a"]` and keys `[2,2]` throw `ArgumentException`.
  - `Ordinal_errors_throw` (Review Focus 1): in a 2-field record, writing ordinal 1 first, writing 0 twice, `EndRecord()` after one field, and a third field all throw `InvalidOperationException`; a field write before any `BeginRecord` throws.
  - `List_element_must_write_exactly_one_value`: a `writeItem` that writes nothing, or two values, throws `InvalidOperationException`.
  - `Strings_must_be_ascii`: `String(0, "é")` throws `ArgumentException`; `String(0, null!)` throws `ArgumentNullException`; `String(0, "")` writes `00000000`.
  - `Enums_are_written_by_name` (Review Focus 5): `Enum(0, Faction.Enemy)` writes the bytes of `String(0, "Enemy")`; an undefined enum value `(Faction)99` throws `ArgumentException`.
  - `Negative_integers_are_little_endian_twos_complement`: `Int32(0, -2)` → `feffffff`; `Int64(0, -1)` → `ffffffffffffffff`; `UInt32(0, 1)` → `01000000`.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter CanonicalWriterTests` — Expected: FAIL to compile (`CanonicalWriter` not defined).
- [ ] **Step 3: Implement** `ICanonical` and `CanonicalWriter` per the Interfaces block and the byte table. Keep a stack of frames `(expectedFieldCount, nextOrdinal)`; collections push a one-field frame per element. Use `BinaryPrimitives.Write*LittleEndian` into a growing `byte[]` (or `ArrayBufferWriter<byte>`). Sets sort with `StringComparer.Ordinal` (id) or a `[TotalComparer]` `ulong` comparer, then reject equal neighbours. XML docs cite SV 4.5.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln --filter CanonicalWriterTests` — Expected: PASS. Then `dotnet test Dungeon.sln` and `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj` — all pass, no warnings, no DUN diagnostics.
- [ ] **Step 5: Commit** — `git commit -m "M1-03: add CanonicalWriter with pinned byte vectors (SV 4.5)"`

### Task 2: `CanonicalReader` and round trips

**Files:**
- Create: `src/Core/Canonical/CanonicalReader.cs`, `src/Core/Canonical/CanonicalFormatException.cs`
- Test: `tests/Core.Tests/CanonicalReaderTests.cs`

**Interfaces:**
- Consumes: `CanonicalWriter`, `ICanonical` (Task 1); the test `Sample`/`Inner` types (move them to `tests/Core.Tests/CanonicalSamples.cs` so both test classes share them, each with a `static Sample Read(CanonicalReader r)`).
- Produces: `public sealed class CanonicalFormatException : Exception` (message names the byte offset and what was wrong); `public sealed class CanonicalReader` with `CanonicalReader(ReadOnlyMemory<byte> bytes)`; `static T Deserialize<T>(ReadOnlyMemory<byte> bytes, Func<CanonicalReader, T> read)` (reads, then requires end of input); `BeginRecord(int fieldCount)`, `EndRecord()`; mirrors of every writer method: `ReadUInt8(int)`, `ReadBool(int)`, `ReadInt32(int)`, `ReadUInt32(int)`, `ReadInt64(int)`, `ReadUInt64(int)`, `ReadString(int)`, `ReadEnum<TEnum>(int)`, `ReadOptionalInt32(int) -> int?`, `ReadOptionalUInt64(int) -> ulong?`, `ReadOptionalString(int) -> string?`, `ReadOptionalRecord<T>(int, Func<CanonicalReader, T>) -> T?` (`T : class`), `ReadRecord<T>(int, Func<CanonicalReader, T>)`, `ReadList<T>(int, Func<CanonicalReader, T>) -> ImmutableArray<T>`, `ReadSetById<T>(int, Func<CanonicalReader, T>, Func<T, string> id) -> ImmutableArray<T>`, `ReadSetByKey<T>(int, Func<CanonicalReader, T>, Func<T, ulong> key) -> ImmutableArray<T>`. Same ordinal and field-count checks as the writer (`InvalidOperationException`).

- [ ] **Step 1: Write the failing tests.**
  - `Sample_round_trips`: `Deserialize(Serialize(sample), Sample.Read)` equals `sample` field by field, and re-serialising it gives the pinned 75 bytes of Task 1 (`Write(Read(b)) == b`).
  - `Every_prefix_of_the_sample_is_rejected` (Review Focus 3): for each length 0…74, deserialising the first n bytes throws `CanonicalFormatException`.
  - `Trailing_bytes_are_rejected`: the 75 bytes plus `00` throws.
  - `Malformed_values_are_rejected` (Review Focus 3), each by patching the pinned sample (byte map: 0–7 seed, 8–11 tick, 12–15 id length, 16–24 `unit.hero`, 25 absent tag, 26 present tag, 27–30 `7`, 31–34 set count, 35–38/39 `"a"`, 40–43/44 `"b"`, 45–48 list count, 49–64 the two `Int64`s, 65 bool, 66–74 `Inner`): the absent tag at offset 25 set to `02`; the bool at offset 65 set to `02`; a string byte at offset 17 set to `0x80`; the set's elements swapped (offset 39 `62`, offset 44 `61`); a string length of `ffffffff` at offset 12 (must throw without allocating — assert it throws `CanonicalFormatException`, not `OutOfMemoryException`); a set of two equal ids.
  - `Unknown_enum_name_is_rejected`: `String(0, "Neutral")` read with `ReadEnum<Faction>` throws `CanonicalFormatException`.
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter CanonicalReaderTests` — Expected: FAIL to compile.
- [ ] **Step 3: Implement** the reader as a strict mirror of the writer: check remaining length before every read and before allocating a string or list (a list of count c needs at least c bytes remaining); enum names parse with `Enum.TryParse(name, ignoreCase: false)` plus `Enum.IsDefined`, refusing numeric strings; sets verify strictly ascending ordinal ids / keys as they are read.
- [ ] **Step 4: Run** the same filter — Expected: PASS. Then `dotnet test Dungeon.sln` — all pass, no warnings.
- [ ] **Step 5: Commit** — `git commit -m "M1-03: add the strict CanonicalReader with round-trip and hostile-input tests"`

### Task 3: `Digest`, the state digest, and docs

**Files:**
- Create: `src/Core/Canonical/Digest.cs`, `src/Core/State/StateDigest.cs`
- Test: `tests/Core.Tests/DigestTests.cs`, `tests/Core.Tests/StateDigestTests.cs`
- Modify: `README.md` (layout block: `Canonical/  CanonicalWriter, CanonicalReader, SHA-256 Digest (SV 4.5)`)

**Interfaces:**
- Consumes: `CanonicalWriter`, `CanonicalReader`, `ICanonical` (Tasks 1–2); `GameState`, `Unit`, `PendingDecision`, `RunStreams`, `GridMap.ToRows()` (existing).
- Produces:
  - `public readonly record struct Digest(ulong W0, ulong W1, ulong W2, ulong W3)` with `static Digest Of(ReadOnlySpan<byte> bytes)` (`SHA256.HashData`; `W0` = bytes 0–7 big-endian, etc.), `static Digest Of(ICanonical value)`, `string ToHex()` (64 lowercase hex), `string ToShortHex()` (first 32 hex = 128 bits), `override string ToString() => ToShortHex()`.
  - `public static class StateDigest` with `Digest Of(GameState s)`: one record with ordinals 0 `RunSeed` (`UInt64`), 1 map rows (`List` of `String`), 2 units (`SetByKey` keyed by the unit id converted to `ulong`; a negative id throws `ArgumentException` before conversion), each unit a record {0 id `Int32`, 1 x `Int32`, 2 y `Int32`, 3 faction `Enum`, 4 boons `List<Int32>`}, 3 streams record {0 combat counter `UInt64`, 1 shrine counter `UInt64`} (keys are derived from `RunSeed`, so not written), 4 pending `OptionalRecord` {0 id, 1 unit, 2 kind `Enum`, 3 options `List<Int32>`, 4 x, 5 y}, 5 `Tick` (`Int64`), 6 `NextDecisionId` (`Int32`).

- [ ] **Step 1: Write the failing tests.**
  - `DigestTests.Empty_input_is_the_sha256_of_nothing`: `Digest.Of(ReadOnlySpan<byte>.Empty).ToHex() == "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"`.
  - `DigestTests.Sample_digest_is_pinned`: `Digest.Of(sample).ToHex() == "a31903eee094899ebdeeb4f11fb564a983884cec9be5e1dbc3d5fb0f1bc1348e"`, `ToShortHex() == "a31903eee094899ebdeeb4f11fb564a9"`, `ToString() == ToShortHex()`.
  - `DigestTests.Reordering_a_set_does_not_change_the_digest` (the done-when): a sample whose set is given as `["a","b"]` and one given as `["b","a"]` have equal `Digest`s; changing any one field value changes it.
  - `StateDigestTests`: two `GameState.Create` calls with the same units in different input orders give equal digests; a move changes the digest; a shrine step changes it again and `Snapshot.Load(Snapshot.Save(s))` keeps it; reordering the `Faction` enum's members cannot be tested at run time, so assert instead that the unit record writes the faction as the string `"Party"` (by serialising one unit and finding those bytes); a state with a negative unit id throws `ArgumentException`. No pinned hex (call 5).
- [ ] **Step 2: Run** `dotnet test Dungeon.sln --filter "DigestTests|StateDigestTests"` — Expected: FAIL to compile.
- [ ] **Step 3: Implement** `Digest` and `StateDigest` per the Interfaces block; hex via `Convert.ToHexString(...).ToLowerInvariant()`. Update the README layout block.
- [ ] **Step 4: Run** `dotnet test Dungeon.sln`, `dotnet build dungeon-ttrpg/DungeonTTRPG.csproj`, `dotnet build tests/AnalyzerSmoke/AnalyzerSmoke.csproj` (must still fail with DUN001) — Expected: all pass, no warnings.
- [ ] **Step 5: Commit** — `git commit -m "M1-03: add SHA-256 Digest and the GameState core-state digest"`
