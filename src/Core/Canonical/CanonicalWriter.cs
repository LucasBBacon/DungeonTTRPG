using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using Dungeon.Core.Determinism;

namespace Dungeon.Core.Canonical;

/// <summary>
/// The one canonical binary serialiser for digests and journal deltas (SV 4.5). It writes
/// little-endian fixed-width integers, ASCII strings with a <c>UInt32</c> length, a
/// <c>00</c>/<c>01</c> tag before every optional value, length-prefixed lists in their given
/// order, length-prefixed sets in DS4 order (ordinal id or ascending key), and records whose
/// fields are written in declared ordinal order with the count checked. Enums are written by name.
/// A record has no header: the ordinal and count checks are what stop a field reorder from
/// silently changing the bytes. Misuse (a wrong ordinal, a missing field) is a programming error
/// and throws <see cref="InvalidOperationException"/>.
/// </summary>
public sealed class CanonicalWriter
{
    private static readonly System.Text.UTF8Encoding StrictUtf8 = new(false, throwOnInvalidBytes: true);

    private readonly ArrayBufferWriter<byte> _buffer = new();
    private readonly List<Frame> _frames = new();
    private bool _recordSlotOpen;
    private bool _rootDone;

    /// <summary>The bytes of the complete root record. Throws until that record has been ended.</summary>
    public byte[] ToArray()
    {
        if (_frames.Count != 0 || !_rootDone)
        {
            throw new InvalidOperationException("The root record is not complete.");
        }

        return _buffer.WrittenSpan.ToArray();
    }

    /// <summary>The canonical bytes of <paramref name="value"/>, written as one root record.</summary>
    public static byte[] Serialize(ICanonical value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var w = new CanonicalWriter();
        value.WriteCanonical(w);
        if (w._frames.Count != 0 || !w._rootDone)
        {
            throw new InvalidOperationException("Canonical value did not write exactly one complete record.");
        }

        return w._buffer.WrittenSpan.ToArray();
    }

    /// <summary>
    /// Opens a record of <paramref name="fieldCount"/> fields. Allowed as the root record, or as
    /// the value of a <see cref="Record{T}"/> or <see cref="OptionalRecord{T}"/> field.
    /// </summary>
    public void BeginRecord(int fieldCount)
    {
        if (fieldCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(fieldCount), fieldCount, "Field count must not be negative.");
        }

        if (_recordSlotOpen)
        {
            _recordSlotOpen = false;
        }
        else if (_frames.Count != 0 || _rootDone)
        {
            throw new InvalidOperationException(
                "A nested record must be written through Record or OptionalRecord, and a writer holds one root record.");
        }

        _frames.Add(new Frame(fieldCount));
    }

    /// <summary>Closes the current record; every declared field must have been written.</summary>
    public void EndRecord()
    {
        var frame = Top("EndRecord");
        if (frame.Next != frame.Expected)
        {
            throw new InvalidOperationException(
                $"Record ended after {frame.Next} of {frame.Expected} fields; expected ordinal {frame.Next} next.");
        }

        _frames.RemoveAt(_frames.Count - 1);
        if (_frames.Count == 0)
        {
            _rootDone = true;
        }
    }

    /// <summary>Writes one byte.</summary>
    public void UInt8(int ordinal, byte value)
    {
        Field(ordinal);
        RawUInt8(value);
    }

    /// <summary>Writes <c>01</c> for true and <c>00</c> for false.</summary>
    public void Bool(int ordinal, bool value)
    {
        Field(ordinal);
        RawUInt8(value ? (byte)1 : (byte)0);
    }

    /// <summary>Writes 4 bytes, little-endian two's complement.</summary>
    public void Int32(int ordinal, int value)
    {
        Field(ordinal);
        BinaryPrimitives.WriteInt32LittleEndian(_buffer.GetSpan(4), value);
        _buffer.Advance(4);
    }

    /// <summary>Writes 4 bytes, little-endian.</summary>
    public void UInt32(int ordinal, uint value)
    {
        Field(ordinal);
        RawUInt32(value);
    }

    /// <summary>Writes 8 bytes, little-endian two's complement.</summary>
    public void Int64(int ordinal, long value)
    {
        Field(ordinal);
        BinaryPrimitives.WriteInt64LittleEndian(_buffer.GetSpan(8), value);
        _buffer.Advance(8);
    }

    /// <summary>Writes 8 bytes, little-endian.</summary>
    public void UInt64(int ordinal, ulong value)
    {
        Field(ordinal);
        BinaryPrimitives.WriteUInt64LittleEndian(_buffer.GetSpan(8), value);
        _buffer.Advance(8);
    }

    /// <summary>Writes a <c>UInt32</c> byte length then the ASCII bytes. Rules strings are ASCII (SV 4.6).</summary>
    public void String(int ordinal, string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        RequireAscii(value);
        Field(ordinal);
        RawString(value);
    }

    /// <summary>
    /// Writes a <c>UInt32</c> byte length then the UTF-8 bytes, for display fields only (plan call 8);
    /// rules fields use <see cref="String"/>. A lone surrogate throws <see cref="ArgumentException"/>.
    /// </summary>
    public void Text(int ordinal, string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        byte[] bytes = StrictUtf8.GetBytes(value);
        Field(ordinal);
        RawUInt32(checked((uint)bytes.Length));
        bytes.CopyTo(_buffer.GetSpan(bytes.Length));
        _buffer.Advance(bytes.Length);
    }

    /// <summary>Writes an enum member by its name, so reordering members does not change bytes.</summary>
    public void Enum<TEnum>(int ordinal, TEnum value)
        where TEnum : struct, System.Enum
    {
        string? name = System.Enum.GetName(value);
        if (name is null)
        {
            throw new ArgumentException($"{typeof(TEnum).Name} has no member with value {value}.", nameof(value));
        }

        String(ordinal, name);
    }

    /// <summary>Writes <c>00</c> when absent, or <c>01</c> then the 4-byte value.</summary>
    public void OptionalInt32(int ordinal, int? value)
    {
        Field(ordinal);
        RawUInt8(value.HasValue ? (byte)1 : (byte)0);
        if (value.HasValue)
        {
            BinaryPrimitives.WriteInt32LittleEndian(_buffer.GetSpan(4), value.Value);
            _buffer.Advance(4);
        }
    }

    /// <summary>Writes <c>00</c> when absent, or <c>01</c> then the 8-byte value.</summary>
    public void OptionalUInt64(int ordinal, ulong? value)
    {
        Field(ordinal);
        RawUInt8(value.HasValue ? (byte)1 : (byte)0);
        if (value.HasValue)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(_buffer.GetSpan(8), value.Value);
            _buffer.Advance(8);
        }
    }

    /// <summary>Writes <c>00</c> when absent, or <c>01</c> then the string.</summary>
    public void OptionalString(int ordinal, string? value)
    {
        if (value is not null)
        {
            RequireAscii(value);
        }

        Field(ordinal);
        RawUInt8(value is null ? (byte)0 : (byte)1);
        if (value is not null)
        {
            RawString(value);
        }
    }

    /// <summary>Writes <c>00</c> when absent, or <c>01</c> then the record.</summary>
    public void OptionalRecord<T>(int ordinal, T? value)
        where T : class, ICanonical
    {
        Field(ordinal);
        RawUInt8(value is null ? (byte)0 : (byte)1);
        if (value is not null)
        {
            WriteNested(value);
        }
    }

    /// <summary>Writes a nested record as this field's value.</summary>
    public void Record<T>(int ordinal, T value)
        where T : ICanonical
    {
        ArgumentNullException.ThrowIfNull(value);
        Field(ordinal);
        WriteNested(value);
    }

    /// <summary>
    /// Writes a <c>UInt32</c> count then each item in the given order (the order is data).
    /// <paramref name="writeItem"/> writes exactly one value, with ordinal 0.
    /// </summary>
    public void List<T>(int ordinal, IReadOnlyList<T> items, Action<CanonicalWriter, T> writeItem)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(writeItem);
        Field(ordinal);
        RawUInt32(checked((uint)items.Count));
        for (int i = 0; i < items.Count; i++)
        {
            WriteElement(items[i], writeItem);
        }
    }

    /// <summary>
    /// Writes a set: a <c>UInt32</c> count then the items sorted by ordinal comparison of their
    /// stable ids (DS4). Two items with the same id throw <see cref="ArgumentException"/>.
    /// </summary>
    public void SetById<T>(int ordinal, IEnumerable<T> items, Func<T, string> id, Action<CanonicalWriter, T> writeItem)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(writeItem);
        var entries = new List<KeyValuePair<string, T>>();
        foreach (var item in items)
        {
            entries.Add(new KeyValuePair<string, T>(id(item) ?? throw new ArgumentException("A set item has a null id.", nameof(id)), item));
        }

        entries.Sort(new IdComparer<T>());
        for (int i = 1; i < entries.Count; i++)
        {
            if (string.Equals(entries[i - 1].Key, entries[i].Key, StringComparison.Ordinal))
            {
                throw new ArgumentException($"Set has two items with id '{entries[i].Key}'.", nameof(items));
            }
        }

        Field(ordinal);
        RawUInt32(checked((uint)entries.Count));
        foreach (var entry in entries)
        {
            WriteElement(entry.Value, writeItem);
        }
    }

    /// <summary>
    /// Writes a set: a <c>UInt32</c> count then the items in ascending key order. Two items with
    /// the same key throw <see cref="ArgumentException"/>.
    /// </summary>
    public void SetByKey<T>(int ordinal, IEnumerable<T> items, Func<T, ulong> key, Action<CanonicalWriter, T> writeItem)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(writeItem);
        var entries = new List<KeyValuePair<ulong, T>>();
        foreach (var item in items)
        {
            entries.Add(new KeyValuePair<ulong, T>(key(item), item));
        }

        entries.Sort(new KeyComparer<T>());
        for (int i = 1; i < entries.Count; i++)
        {
            if (entries[i - 1].Key == entries[i].Key)
            {
                throw new ArgumentException($"Set has two items with key {entries[i].Key}.", nameof(items));
            }
        }

        Field(ordinal);
        RawUInt32(checked((uint)entries.Count));
        foreach (var entry in entries)
        {
            WriteElement(entry.Value, writeItem);
        }
    }

    private void WriteElement<T>(T item, Action<CanonicalWriter, T> writeItem)
    {
        int depth = _frames.Count;
        int start = _buffer.WrittenCount;
        _frames.Add(new Frame(1));
        writeItem(this, item);
        if (_frames.Count != depth + 1 || _frames[depth].Next != 1)
        {
            throw new InvalidOperationException("A collection element must write exactly one value, with ordinal 0.");
        }

        // The reader bounds a count by the bytes left, which needs every element to take a byte.
        if (_buffer.WrittenCount == start)
        {
            throw new InvalidOperationException("A collection element must not be empty (a record with no fields).");
        }

        _frames.RemoveAt(depth);
    }

    private void WriteNested(ICanonical value)
    {
        int depth = _frames.Count;
        _recordSlotOpen = true;
        value.WriteCanonical(this);
        if (_recordSlotOpen || _frames.Count != depth)
        {
            _recordSlotOpen = false;
            throw new InvalidOperationException($"{value.GetType().Name} did not write exactly one complete record.");
        }
    }

    private void Field(int ordinal)
    {
        var frame = Top("A field write");
        if (_recordSlotOpen)
        {
            throw new InvalidOperationException("A nested record must start with BeginRecord.");
        }

        if (frame.Next >= frame.Expected)
        {
            throw new InvalidOperationException(
                $"Record declared {frame.Expected} fields, but ordinal {ordinal} was written.");
        }

        if (ordinal != frame.Next)
        {
            throw new InvalidOperationException($"Expected ordinal {frame.Next}, got {ordinal}.");
        }

        frame.Next++;
    }

    private Frame Top(string what)
    {
        if (_frames.Count == 0)
        {
            throw new InvalidOperationException($"{what} outside any record.");
        }

        return _frames[_frames.Count - 1];
    }

    private void RawUInt8(byte value)
    {
        _buffer.GetSpan(1)[0] = value;
        _buffer.Advance(1);
    }

    private void RawUInt32(uint value)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(_buffer.GetSpan(4), value);
        _buffer.Advance(4);
    }

    private void RawString(string value)
    {
        RawUInt32(checked((uint)value.Length));
        var span = _buffer.GetSpan(value.Length);
        for (int i = 0; i < value.Length; i++)
        {
            span[i] = (byte)value[i];
        }

        _buffer.Advance(value.Length);
    }

    private static void RequireAscii(string value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] > '\u007F')
            {
                throw new ArgumentException(
                    $"Canonical strings are ASCII (SV 4.6); character {i} is U+{(int)value[i]:X4}.", nameof(value));
            }
        }
    }

    private sealed class Frame(int expected)
    {
        public int Expected { get; } = expected;

        public int Next { get; set; }
    }

    [TotalComparer]
    private sealed class IdComparer<T> : IComparer<KeyValuePair<string, T>>
    {
        public int Compare(KeyValuePair<string, T> x, KeyValuePair<string, T> y) =>
            string.CompareOrdinal(x.Key, y.Key);
    }

    [TotalComparer]
    private sealed class KeyComparer<T> : IComparer<KeyValuePair<ulong, T>>
    {
        public int Compare(KeyValuePair<ulong, T> x, KeyValuePair<ulong, T> y) => x.Key.CompareTo(y.Key);
    }
}
