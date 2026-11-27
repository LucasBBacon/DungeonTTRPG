using Dungeon.Core.Canonical;
using Xunit;

namespace Dungeon.Core.Tests;

public class DigestTests
{
    [Fact]
    public void Parse_round_trips_ToHex()
    {
        var d = Digest.Of(Sample.Pinned);
        Assert.Equal(d, Digest.Parse(d.ToHex()));
    }

    [Theory]
    [InlineData("A31903EEE094899EBDEEB4F11FB564A983884CEC9BE5E1DBC3D5FB0F1BC1348E")]
    [InlineData("a31903eee094899ebdeeb4f11fb564a983884cec9be5e1dbc3d5fb0f1bc1348")]
    [InlineData("z31903eee094899ebdeeb4f11fb564a983884cec9be5e1dbc3d5fb0f1bc1348e")]
    [InlineData("")]
    public void Parse_rejects_anything_but_64_lowercase_hex(string hex) =>
        Assert.Throws<FormatException>(() => Digest.Parse(hex));

    [Fact]
    public void Empty_input_is_the_sha256_of_nothing() =>
        Assert.Equal(
            "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
            Digest.Of(ReadOnlySpan<byte>.Empty).ToHex());

    [Fact]
    public void Sample_digest_is_pinned()
    {
        var d = Digest.Of(Sample.Pinned);
        Assert.Equal("a31903eee094899ebdeeb4f11fb564a983884cec9be5e1dbc3d5fb0f1bc1348e", d.ToHex());
        Assert.Equal("a31903eee094899ebdeeb4f11fb564a9", d.ToShortHex());
        Assert.Equal(d.ToShortHex(), d.ToString());
        Assert.Equal(d, Digest.Of(Sample.PinnedBytes));
    }

    [Fact]
    public void Reordering_a_set_does_not_change_the_digest()
    {
        var ab = Sample.Pinned with { Tags = ["a", "b"] };
        var ba = Sample.Pinned with { Tags = ["b", "a"] };
        Assert.Equal(Digest.Of(ab), Digest.Of(ba));
    }

    [Fact]
    public void Changing_any_field_changes_the_digest()
    {
        var pinned = Digest.Of(Sample.Pinned);
        Sample[] changed =
        [
            Sample.Pinned with { Seed = 1 },
            Sample.Pinned with { Tick = -1 },
            Sample.Pinned with { Id = "unit.hera" },
            Sample.Pinned with { Absent = 0 },
            Sample.Pinned with { Present = null },
            Sample.Pinned with { Tags = ["a", "c"] },
            Sample.Pinned with { Values = [-1L, 3L] },
            Sample.Pinned with { Flag = false },
            Sample.Pinned with { Inner = new Inner(254, 1) },
        ];
        foreach (var sample in changed)
        {
            Assert.NotEqual(pinned, Digest.Of(sample));
        }
    }
}
