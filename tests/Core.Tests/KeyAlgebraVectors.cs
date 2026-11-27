using System;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The SV 4.1 reference vectors for <see cref="KeyAlgebra"/>, reproduced with an independent port.</summary>
public class KeyAlgebraVectors
{
    private const ulong RunSeed = 0x0123456789ABCDEFUL;

    private static ulong Dungeon => KeyAlgebra.Derive(RunSeed, "dungeon");
    private static ulong Floor1 => KeyAlgebra.Derive(Dungeon, "floor", 1);

    [Fact]
    public void Draw_of_zero_key()
        => Assert.Equal(0xe220a8397b1dcdafUL, KeyAlgebra.Draw(0, 0));

    [Fact]
    public void LabelId_vectors()
    {
        Assert.Equal(0x9c4f784aa5ce135fUL, KeyAlgebra.LabelId("layout"));
        Assert.Equal(0xcbf29ce484222325UL, KeyAlgebra.LabelId(""));
    }

    [Fact]
    public void Dungeon_and_floor_keys()
    {
        Assert.Equal(0x99b0558c8449420fUL, Dungeon);
        Assert.Equal(0x8224e58417674634UL, Floor1);
    }

    [Fact]
    public void Layout_path_and_d20()
    {
        var attempt = KeyAlgebra.Derive(Floor1, "attempt", 0);
        Assert.Equal(0x408b2970acba60f5UL, attempt);
        var layout = KeyAlgebra.Derive(attempt, "layout");
        Assert.Equal(0xffc7031c11fad2b3UL, layout);
        Assert.Equal(0x5500e3c923d386c0UL, KeyAlgebra.Draw(layout, 0));
        Assert.Equal(7, KeyAlgebra.D20(KeyAlgebra.Draw(layout, 0)));
    }

    [Fact]
    public void Curiosities_key()
    {
        var k = KeyAlgebra.Derive(Floor1, "curiosities");
        Assert.Equal(0x2aad85054c0f15e0UL, k);
        Assert.Equal(0x6cd955ee2b0e5b74UL, KeyAlgebra.Draw(k, 0));
    }

    [Fact]
    public void Label_plus_index_is_one_derive_call_not_two()
    {
        var rejected = KeyAlgebra.Derive(KeyAlgebra.Derive(Dungeon, "floor"), "1");
        Assert.Equal(0x770d8047d64bd714UL, rejected);
        Assert.NotEqual(Floor1, rejected);
    }

    [Fact]
    public void Route_and_boss_keys()
    {
        var route = KeyAlgebra.Derive(Dungeon, "route");
        Assert.Equal(0xccd4caa81e146b07UL, route);
        var boss = KeyAlgebra.Derive(route, "boss9", KeyAlgebra.LabelId("boss.warden"));
        Assert.Equal(0xfeb6e74ee2462524UL, boss);
        Assert.Equal(0x6d404c06a9d65b42UL, KeyAlgebra.Draw(boss, 0));
        Assert.Equal(0xd9f923d397b2e462UL, KeyAlgebra.Draw(boss, 1));
    }

    [Fact]
    public void Recruit_entry_key()
    {
        var k = KeyAlgebra.Derive(KeyAlgebra.Derive(KeyAlgebra.Derive(KeyAlgebra.Derive(RunSeed, "recruit"), "camp", 1), "slot", 0), "entry", 3);
        Assert.Equal(0x5937484078fa307aUL, k);
    }

    [Fact]
    public void Arity_is_part_of_the_key()
    {
        var lp = KeyAlgebra.Derive(KeyAlgebra.Derive(RunSeed, "loot"), "lootPlan", 1);
        Assert.Equal(0xef8041b38b01fd70UL, KeyAlgebra.Derive(lp, "windfall", 0));
        Assert.Equal(0x5eae58fb0fa3e82cUL, KeyAlgebra.Derive(lp, "windfall"));
    }

    [Fact]
    public void Keyed_hash_and_d20()
    {
        var checks = KeyAlgebra.Derive(Floor1, "checks");
        Assert.Equal(0xb8e7d03a7635ebcaUL, checks);
        var h = KeyAlgebra.H(checks, "h:notice", KeyAlgebra.LabelId("unit.hero"), KeyAlgebra.LabelId("unit.mastiff.1"), 0, 0);
        Assert.Equal(0xab0885a7f33fd7adUL, h);
        Assert.Equal(14, KeyAlgebra.D20(h));
    }

    [Fact]
    public void Bound_vectors()
    {
        Assert.Equal(0UL, KeyAlgebra.Bound(0xDEADBEEFCAFEBABEUL, 1));
        Assert.Equal(19UL, KeyAlgebra.Bound(ulong.MaxValue, 20));
        Assert.Equal(0x6f56df77e57f5d5fUL, KeyAlgebra.Bound(0xDEADBEEFCAFEBABEUL, 1UL << 63));
    }

    [Theory]
    [InlineData("İ")]
    [InlineData("a/b")]
    [InlineData("a b")]
    [InlineData("é")]
    public void LabelId_rejects_characters_outside_the_grammar(string s)
        => Assert.Throws<ArgumentException>("s", () => KeyAlgebra.LabelId(s));

    [Fact]
    public void LabelId_rejects_null_and_accepts_mixed_case()
    {
        Assert.Throws<ArgumentNullException>(() => KeyAlgebra.LabelId(null!));
        KeyAlgebra.LabelId("lootPlan");
    }

    [Fact]
    public void Bound_rejects_zero_bound_in_every_build()
    {
        var e = Assert.Throws<ArgumentOutOfRangeException>(() => KeyAlgebra.Bound(5, 0));
        Assert.Equal("n", e.ParamName);
    }

    [Fact]
    public void Bound_does_not_overflow_at_the_extreme()
        => Assert.Equal(ulong.MaxValue - 1, KeyAlgebra.Bound(ulong.MaxValue, ulong.MaxValue));

    [Fact]
    public void D20_covers_one_to_twenty()
    {
        Assert.Equal(1, KeyAlgebra.D20(0));
        Assert.Equal(20, KeyAlgebra.D20(ulong.MaxValue));
    }
}
