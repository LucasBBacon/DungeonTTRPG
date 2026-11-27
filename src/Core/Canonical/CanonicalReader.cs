using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Dungeon.Core.Canonical;

/// <summary>
/// The strict mirror of <see cref="CanonicalWriter"/>. It accepts only byte strings the writer
/// could have produced, so writing back anything it reads gives the same bytes. Malformed input
/// throws <see cref="CanonicalFormatException"/>; reading fields in the wrong order is a
/// programming error and throws <see cref="InvalidOperationException"/>, as in the writer.
/// </summary>
public sealed class CanonicalReader
{
    private static readonly System.Text.UTF8Encoding StrictUtf8 = new(false, throwOnInvalidBytes: true);

    private readonly ReadOnlyMemory<byte> _bytes;
    private readonly List<Frame> _frames = new();
    private int _position;
    private bool _recordSlotOpen;
    private bool _rootDone;

    public CanonicalReader(ReadOnlyMemory<byte> bytes)
    {
        _bytes = bytes;
    }

    /// <summary>Reads one root value with <paramref name="read"/> and requires the input to end there.</summary>
    public static T Deserialize<T>(ReadOnlyMemory<byte> bytes, Func<CanonicalReader, T> read)
    {
        ArgumentNullException.ThrowIfNull(read);
        var r = new CanonicalReader(bytes);
        var value = read(r);
        if (r._frames.Count != 0 || !r._rootDone)
        {
            throw new InvalidOperationException("The read function did not read exactly one complete record.");
        }

        if (r._position != bytes.Length)
        {
            throw new CanonicalFormatException(r._position, $"{bytes.Length - r._position} trailing bytes.");
        }

        return value;
    }

    /// <summary>Opens a record of <paramref name="fieldCount"/> fields (the root, or a record field's value).</summary>
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
                "A nested record must be read through ReadRecord or ReadOptionalRecord, and a reader holds one root record.");
        }

        _frames.Add(new Frame(fieldCount));
    }

    /// <summary>Closes the current record; every declared field must have been read.</summary>
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

    public byte ReadUInt8(int ordinal)
    {
        Field(ordinal);
        return RawUInt8();
    }

    public bool ReadBool(int ordinal)
    {
        Field(ordinal);
        return RawTag("bool");
    }

    public int ReadInt32(int ordinal)
    {
        Field(ordinal);
        return BinaryPrimitives.ReadInt32LittleEndian(Take(4));
    }

    public uint ReadUInt32(int ordinal)
    {
        Field(ordinal);
        return RawUInt32();
    }

    public long ReadInt64(int ordinal)
    {
        Field(ordinal);
        return BinaryPrimitives.ReadInt64LittleEndian(Take(8));
    }

    public ulong ReadUInt64(int ordinal)
    {
        Field(ordinal);
        return BinaryPrimitives.ReadUInt64LittleEndian(Take(8));
    }

    public string ReadString(int ordinal)
    {
        Field(ordinal);
        return RawString();
    }

    /// <summary>
    /// Reads display text written by <see cref="CanonicalWriter.Text"/>: a <c>UInt32</c> byte length then
    /// UTF-8. Bytes that are not valid UTF-8 throw <see cref="CanonicalFormatException"/>.
    /// </summary>
    public string ReadText(int ordinal)
    {
        Field(ordinal);
        int at = _position;
        uint length = RawUInt32();
        if (length > (uint)(_bytes.Length - _position))
        {
            throw new CanonicalFormatException(at, $"text length {length} exceeds the {_bytes.Length - _position} bytes left.");
        }

        var span = Take((int)length);
        try
        {
            return StrictUtf8.GetString(span);
        }
        catch (System.Text.DecoderFallbackException)
        {
            throw new CanonicalFormatException(at + 4, "text is not valid UTF-8.");
        }
    }

    /// <summary>Reads an enum written by name. Only an exact member name is accepted, never a number.</summary>
    public TEnum ReadEnum<TEnum>(int ordinal)
        where TEnum : struct, Enum
    {
        int at = _position;
        string name = ReadString(ordinal);
        foreach (var value in Enum.GetValues<TEnum>())
        {
            if (string.Equals(Enum.GetName(value), name, StringComparison.Ordinal))
            {
                return value;
            }
        }

        throw new CanonicalFormatException(at, $"'{name}' is not a member of {typeof(TEnum).Name}.");
    }

    public int? ReadOptionalInt32(int ordinal)
    {
        Field(ordinal);
        return RawTag("optional tag") ? BinaryPrimitives.ReadInt32LittleEndian(Take(4)) : null;
    }

    public ulong? ReadOptionalUInt64(int ordinal)
    {
        Field(ordinal);
        return RawTag("optional tag") ? BinaryPrimitives.ReadUInt64LittleEndian(Take(8)) : null;
    }

    public string? ReadOptionalString(int ordinal)
    {
        Field(ordinal);
        return RawTag("optional tag") ? RawString() : null;
    }

    public T? ReadOptionalRecord<T>(int ordinal, Func<CanonicalReader, T> read)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(read);
        Field(ordinal);
        return RawTag("optional tag") ? ReadNested(read) : null;
    }

    public T ReadRecord<T>(int ordinal, Func<CanonicalReader, T> read)
    {
        ArgumentNullException.ThrowIfNull(read);
        Field(ordinal);
        return ReadNested(read);
    }

    /// <summary>Reads a list in its written order. Each element is one value read with ordinal 0.</summary>
    public ImmutableArray<T> ReadList<T>(int ordinal, Func<CanonicalReader, T> readItem)
    {
        ArgumentNullException.ThrowIfNull(readItem);
        Field(ordinal);
        int count = RawCount();
        var items = ImmutableArray.CreateBuilder<T>(count);
        for (int i = 0; i < count; i++)
        {
            items.Add(ReadElement(readItem));
        }

        return items.MoveToImmutable();
    }

    /// <summary>Reads a set by id; the ids must be strictly ascending by ordinal comparison.</summary>
    public ImmutableArray<T> ReadSetById<T>(int ordinal, Func<CanonicalReader, T> readItem, Func<T, string> id)
    {
        ArgumentNullException.ThrowIfNull(readItem);
        ArgumentNullException.ThrowIfNull(id);
        Field(ordinal);
        int count = RawCount();
        var items = ImmutableArray.CreateBuilder<T>(count);
        string? previous = null;
        for (int i = 0; i < count; i++)
        {
            int at = _position;
            var item = ReadElement(readItem);
            string current = id(item);
            if (previous is not null && string.CompareOrdinal(previous, current) >= 0)
            {
                throw new CanonicalFormatException(at, $"set id '{current}' is not after '{previous}'.");
            }

            previous = current;
            items.Add(item);
        }

        return items.MoveToImmutable();
    }

    /// <summary>Reads a set by key; the keys must be strictly ascending.</summary>
    public ImmutableArray<T> ReadSetByKey<T>(int ordinal, Func<CanonicalReader, T> readItem, Func<T, ulong> key)
    {
        ArgumentNullException.ThrowIfNull(readItem);
        ArgumentNullException.ThrowIfNull(key);
        Field(ordinal);
        int count = RawCount();
        var items = ImmutableArray.CreateBuilder<T>(count);
        ulong? previous = null;
        for (int i = 0; i < count; i++)
        {
            int at = _position;
            var item = ReadElement(readItem);
            ulong current = key(item);
            if (previous.HasValue && previous.Value >= current)
            {
                throw new CanonicalFormatException(at, $"set key {current} is not after {previous.Value}.");
            }

            previous = current;
            items.Add(item);
        }

        return items.MoveToImmutable();
    }

    private T ReadElement<T>(Func<CanonicalReader, T> readItem)
    {
        int depth = _frames.Count;
        int start = _position;
        _frames.Add(new Frame(1));
        var item = readItem(this);
        if (_frames.Count != depth + 1 || _frames[depth].Next != 1)
        {
            throw new InvalidOperationException("A collection element must read exactly one value, with ordinal 0.");
        }

        // The writer refuses empty elements, so bytes holding one were not written by it.
        if (_position == start)
        {
            throw new CanonicalFormatException(start, "a collection element is empty.");
        }

        _frames.RemoveAt(depth);
        return item;
    }

    private T ReadNested<T>(Func<CanonicalReader, T> read)
    {
        int depth = _frames.Count;
        _recordSlotOpen = true;
        var value = read(this);
        if (_recordSlotOpen || _frames.Count != depth)
        {
            _recordSlotOpen = false;
            throw new InvalidOperationException("The read function did not read exactly one complete record.");
        }

        return value;
    }

    private void Field(int ordinal)
    {
        var frame = Top("A field read");
        if (_recordSlotOpen)
        {
            throw new InvalidOperationException("A nested record must start with BeginRecord.");
        }

        if (frame.Next >= frame.Expected)
        {
            throw new InvalidOperationException(
                $"Record declared {frame.Expected} fields, but ordinal {ordinal} was read.");
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

    private ReadOnlySpan<byte> Take(int count)
    {
        if (_bytes.Length - _position < count)
        {
            throw new CanonicalFormatException(
                _position, $"needs {count} bytes, {_bytes.Length - _position} remain.");
        }

        var span = _bytes.Span.Slice(_position, count);
        _position += count;
        return span;
    }

    private byte RawUInt8() => Take(1)[0];

    private uint RawUInt32() => BinaryPrimitives.ReadUInt32LittleEndian(Take(4));

    private bool RawTag(string what)
    {
        int at = _position;
        byte tag = RawUInt8();
        return tag switch
        {
            0 => false,
            1 => true,
            _ => throw new CanonicalFormatException(at, $"{what} is {tag}, not 0 or 1."),
        };
    }

    // Every element takes at least one byte (the writer refuses empty elements), so a count
    // larger than the bytes left is malformed and is rejected before anything is allocated.
    private int RawCount()
    {
        int at = _position;
        uint count = RawUInt32();
        if (count > (uint)(_bytes.Length - _position))
        {
            throw new CanonicalFormatException(at, $"count {count} exceeds the {_bytes.Length - _position} bytes left.");
        }

        return (int)count;
    }

    private string RawString()
    {
        int at = _position;
        uint length = RawUInt32();
        if (length > (uint)(_bytes.Length - _position))
        {
            throw new CanonicalFormatException(at, $"string length {length} exceeds the {_bytes.Length - _position} bytes left.");
        }

        var span = Take((int)length);
        var chars = new char[span.Length];
        for (int i = 0; i < span.Length; i++)
        {
            if (span[i] > 0x7F)
            {
                throw new CanonicalFormatException(at + 4 + i, $"byte 0x{span[i]:X2} is not ASCII.");
            }

            chars[i] = (char)span[i];
        }

        return new string(chars);
    }

    private sealed class Frame(int expected)
    {
        public int Expected { get; } = expected;

        public int Next { get; set; }
    }
}
