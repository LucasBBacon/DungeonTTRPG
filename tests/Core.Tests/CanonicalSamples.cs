using Dungeon.Core.Canonical;

namespace Dungeon.Core.Tests;

/// <summary>The 2-field record nested in <see cref="Sample"/>.</summary>
internal sealed record Inner(byte Flags, ulong Count) : ICanonical
{
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(2);
        w.UInt8(0, Flags);
        w.UInt64(1, Count);
        w.EndRecord();
    }

    public static Inner Read(CanonicalReader r)
    {
        r.BeginRecord(2);
        var inner = new Inner(r.ReadUInt8(0), r.ReadUInt64(1));
        r.EndRecord();
        return inner;
    }
}

/// <summary>A test-only schema that exercises every value kind of the canonical format once.</summary>
internal sealed record Sample(
    ulong Seed,
    int Tick,
    string Id,
    int? Absent,
    int? Present,
    IReadOnlyList<string> Tags,
    IReadOnlyList<long> Values,
    bool Flag,
    Inner Inner) : ICanonical
{
    /// <summary>The pinned sample; its bytes were computed by an independent Python model of the format.</summary>
    public static Sample Pinned => new(
        0x0123456789ABCDEFUL, -2, "unit.hero", null, 7, ["b", "a"], [3L, -1L], true, new Inner(255, 1));

    /// <summary>
    /// The pinned 75 bytes. Byte map: 0–7 seed, 8–11 tick, 12–15 id length, 16–24 <c>unit.hero</c>,
    /// 25 absent tag, 26 present tag, 27–30 <c>7</c>, 31–34 set count, 35–39 <c>"a"</c>,
    /// 40–44 <c>"b"</c>, 45–48 list count, 49–64 the two <c>Int64</c>s, 65 bool, 66–74 <see cref="Inner"/>.
    /// </summary>
    public const string PinnedHex =
        "efcdab8967452301feffffff09000000756e69742e6865726f0001070000000200000001000000610100000062"
        + "020000000300000000000000ffffffffffffffff01ff0100000000000000";

    public static byte[] PinnedBytes => Convert.FromHexString(PinnedHex);

    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(9);
        w.UInt64(0, Seed);
        w.Int32(1, Tick);
        w.String(2, Id);
        w.OptionalInt32(3, Absent);
        w.OptionalInt32(4, Present);
        w.SetById(5, Tags, t => t, (e, t) => e.String(0, t));
        w.List(6, Values, (e, v) => e.Int64(0, v));
        w.Bool(7, Flag);
        w.Record(8, Inner);
        w.EndRecord();
    }

    public static Sample Read(CanonicalReader r)
    {
        r.BeginRecord(9);
        var sample = new Sample(
            r.ReadUInt64(0),
            r.ReadInt32(1),
            r.ReadString(2),
            r.ReadOptionalInt32(3),
            r.ReadOptionalInt32(4),
            r.ReadSetById(5, e => e.ReadString(0), t => t),
            r.ReadList(6, e => e.ReadInt64(0)),
            r.ReadBool(7),
            r.ReadRecord(8, Inner.Read));
        r.EndRecord();
        return sample;
    }
}
