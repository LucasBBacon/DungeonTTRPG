using Dungeon.Core.Canonical;
using Xunit;

namespace Dungeon.Core.Tests;

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

    private sealed class Empty : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(0);
            w.EndRecord();
        }
    }

    // ToArray must not hand out the bytes of an incomplete record (final review A2).
    [Fact]
    public void ToArray_requires_a_complete_root_record()
    {
        Assert.Throws<InvalidOperationException>(() => new CanonicalWriter().ToArray());
        var open = new CanonicalWriter();
        open.BeginRecord(2);
        open.Int32(0, 1);
        Assert.Throws<InvalidOperationException>(() => open.ToArray());
    }

    // The reader rejects a count larger than the bytes left, which is only sound if no element is empty.
    [Fact]
    public void Collection_element_must_not_be_empty() =>
        Assert.Throws<InvalidOperationException>(
            () => One(w => w.List(0, new[] { new Empty() }, (e, v) => e.Record(0, v))));

    [Fact]
    public void Strings_must_be_ascii()
    {
        Assert.Throws<ArgumentException>(() => One(w => w.String(0, "é")));
        Assert.Throws<ArgumentNullException>(() => One(w => w.String(0, null!)));
        Assert.Equal("00000000", One(w => w.String(0, "")));
    }

    [Fact]
    public void Text_is_utf8_with_a_byte_length()
    {
        Assert.Equal("06000000c39c6e646572", One(w => w.Text(0, "Ünder")));
        Assert.Equal("00000000", One(w => w.Text(0, "")));
        Assert.ThrowsAny<ArgumentException>(() => One(w => w.Text(0, "\uD800")));
        Assert.ThrowsAny<ArgumentException>(() => One(w => w.Text(0, "a\uDC00b")));
        Assert.Throws<ArgumentNullException>(() => One(w => w.Text(0, null!)));
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
