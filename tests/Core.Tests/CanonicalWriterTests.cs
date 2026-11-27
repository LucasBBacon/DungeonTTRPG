using Dungeon.Core.Canonical;
using Xunit;

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

    public const string PinnedHex =
        "efcdab8967452301feffffff09000000756e69742e6865726f0001070000000200000001000000610100000062"
        + "020000000300000000000000ffffffffffffffff01ff0100000000000000";

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
}

public class CanonicalWriterTests
{
    private static string Hex(byte[] bytes) => Convert.ToHexString(bytes).ToLowerInvariant();

    /// <summary>Writes one record of one field through <paramref name="write"/> and returns the bytes as hex.</summary>
    private static string One(Action<CanonicalWriter> write)
    {
        var w = new CanonicalWriter();
        w.BeginRecord(1);
        write(w);
        w.EndRecord();
        return Hex(w.ToArray());
    }

    [Fact]
    public void Sample_bytes_are_pinned()
    {
        var bytes = CanonicalWriter.Serialize(Sample.Pinned);
        Assert.Equal(75, bytes.Length);
        Assert.Equal(Sample.PinnedHex, Hex(bytes));
    }

    [Fact]
    public void Set_input_order_does_not_change_bytes()
    {
        Assert.Equal(
            One(w => w.SetById(0, new[] { "b", "a", "c" }, s => s, (e, s) => e.String(0, s))),
            One(w => w.SetById(0, new[] { "c", "a", "b" }, s => s, (e, s) => e.String(0, s))));
        Assert.Equal(
            One(w => w.SetByKey(0, new ulong[] { 5, 1, 3 }, k => k, (e, k) => e.UInt64(0, k))),
            One(w => w.SetByKey(0, new ulong[] { 3, 5, 1 }, k => k, (e, k) => e.UInt64(0, k))));
    }

    [Fact]
    public void Set_duplicates_throw()
    {
        Assert.Throws<ArgumentException>(
            () => One(w => w.SetById(0, new[] { "a", "a" }, s => s, (e, s) => e.String(0, s))));
        Assert.Throws<ArgumentException>(
            () => One(w => w.SetByKey(0, new ulong[] { 2, 2 }, k => k, (e, k) => e.UInt64(0, k))));
    }

    [Fact]
    public void Ordinal_errors_throw()
    {
        static CanonicalWriter Two()
        {
            var w = new CanonicalWriter();
            w.BeginRecord(2);
            return w;
        }

        Assert.Throws<InvalidOperationException>(() => Two().Int32(1, 0));

        var twice = Two();
        twice.Int32(0, 0);
        Assert.Throws<InvalidOperationException>(() => twice.Int32(0, 0));

        var early = Two();
        early.Int32(0, 0);
        Assert.Throws<InvalidOperationException>(() => early.EndRecord());

        var extra = Two();
        extra.Int32(0, 0);
        extra.Int32(1, 0);
        Assert.Throws<InvalidOperationException>(() => extra.Int32(2, 0));

        Assert.Throws<InvalidOperationException>(() => new CanonicalWriter().Int32(0, 0));
    }

    [Fact]
    public void List_element_must_write_exactly_one_value()
    {
        Assert.Throws<InvalidOperationException>(
            () => One(w => w.List(0, new[] { 1 }, (e, v) => { })));
        Assert.Throws<InvalidOperationException>(
            () => One(w => w.List(0, new[] { 1 }, (e, v) => { e.Int32(0, v); e.Int32(0, v); })));
    }

    [Fact]
    public void Strings_must_be_ascii()
    {
        Assert.Throws<ArgumentException>(() => One(w => w.String(0, "é")));
        Assert.Throws<ArgumentNullException>(() => One(w => w.String(0, null!)));
        Assert.Equal("00000000", One(w => w.String(0, "")));
    }

    [Fact]
    public void Enums_are_written_by_name()
    {
        Assert.Equal(One(w => w.String(0, "Enemy")), One(w => w.Enum(0, Faction.Enemy)));
        Assert.Throws<ArgumentException>(() => One(w => w.Enum(0, (Faction)99)));
    }

    [Fact]
    public void Negative_integers_are_little_endian_twos_complement()
    {
        Assert.Equal("feffffff", One(w => w.Int32(0, -2)));
        Assert.Equal("ffffffffffffffff", One(w => w.Int64(0, -1)));
        Assert.Equal("01000000", One(w => w.UInt32(0, 1)));
    }
}
