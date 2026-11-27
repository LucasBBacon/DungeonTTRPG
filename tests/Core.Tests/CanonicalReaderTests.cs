using Dungeon.Core.Canonical;
using Xunit;

namespace Dungeon.Core.Tests;

public class CanonicalReaderTests
{
    private static Sample Read(byte[] bytes) => CanonicalReader.Deserialize(bytes, Sample.Read);

    private static byte[] Patched(params (int Offset, byte Value)[] patches)
    {
        var bytes = Sample.PinnedBytes;
        foreach (var (offset, value) in patches)
        {
            bytes[offset] = value;
        }

        return bytes;
    }

    [Fact]
    public void Sample_round_trips()
    {
        var read = Read(CanonicalWriter.Serialize(Sample.Pinned));
        Assert.Equal(0x0123456789ABCDEFUL, read.Seed);
        Assert.Equal(-2, read.Tick);
        Assert.Equal("unit.hero", read.Id);
        Assert.Null(read.Absent);
        Assert.Equal(7, read.Present);
        Assert.Equal(new[] { "a", "b" }, read.Tags);
        Assert.Equal(new[] { 3L, -1L }, read.Values);
        Assert.True(read.Flag);
        Assert.Equal(new Inner(255, 1), read.Inner);
        Assert.Equal(Sample.PinnedHex, Convert.ToHexString(CanonicalWriter.Serialize(read)).ToLowerInvariant());
    }

    [Fact]
    public void Every_prefix_of_the_sample_is_rejected()
    {
        var bytes = Sample.PinnedBytes;
        for (int n = 0; n < bytes.Length; n++)
        {
            var prefix = bytes[..n];
            Assert.Throws<CanonicalFormatException>(() => Read(prefix));
        }
    }

    [Fact]
    public void Trailing_bytes_are_rejected() =>
        Assert.Throws<CanonicalFormatException>(() => Read([.. Sample.PinnedBytes, 0x00]));

    [Theory]
    [InlineData(25, 0x02)] // absent tag
    [InlineData(26, 0x02)] // present tag
    [InlineData(65, 0x02)] // bool
    [InlineData(17, 0x80)] // non-ASCII string byte
    public void Malformed_value_is_rejected(int offset, byte value) =>
        Assert.Throws<CanonicalFormatException>(() => Read(Patched((offset, value))));

    [Fact]
    public void Unsorted_set_is_rejected() =>
        Assert.Throws<CanonicalFormatException>(() => Read(Patched((39, 0x62), (44, 0x61))));

    [Fact]
    public void Duplicate_set_ids_are_rejected() =>
        Assert.Throws<CanonicalFormatException>(() => Read(Patched((44, 0x61))));

    [Fact]
    public void Huge_length_is_rejected_without_allocating() =>
        Assert.Throws<CanonicalFormatException>(() => Read(Patched((12, 0xff), (13, 0xff), (14, 0xff), (15, 0xff))));

    [Fact]
    public void Huge_list_count_is_rejected_without_allocating() =>
        Assert.Throws<CanonicalFormatException>(() => Read(Patched((45, 0xff), (46, 0xff), (47, 0xff), (48, 0x7f))));

    [Fact]
    public void Unknown_enum_name_is_rejected()
    {
        var w = new CanonicalWriter();
        w.BeginRecord(1);
        w.String(0, "Neutral");
        w.EndRecord();
        Assert.Throws<CanonicalFormatException>(() => CanonicalReader.Deserialize(w.ToArray(), r =>
        {
            r.BeginRecord(1);
            var f = r.ReadEnum<Faction>(0);
            r.EndRecord();
            return f;
        }));
    }

    [Fact]
    public void Numeric_enum_name_is_rejected()
    {
        var w = new CanonicalWriter();
        w.BeginRecord(1);
        w.String(0, "1");
        w.EndRecord();
        Assert.Throws<CanonicalFormatException>(() => CanonicalReader.Deserialize(w.ToArray(), r =>
        {
            r.BeginRecord(1);
            var f = r.ReadEnum<Faction>(0);
            r.EndRecord();
            return f;
        }));
    }

    [Fact]
    public void Reader_ordinal_errors_throw()
    {
        var r = new CanonicalReader(Sample.PinnedBytes);
        r.BeginRecord(9);
        Assert.Throws<InvalidOperationException>(() => r.ReadInt32(1));
    }
}
