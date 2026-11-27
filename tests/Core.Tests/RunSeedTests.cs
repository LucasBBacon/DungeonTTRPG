using System;
using Xunit;

namespace Dungeon.Core.Tests;

public class RunSeedTests
{
    [Theory]
    [InlineData(0UL)]
    [InlineData((1UL << 60) - 1)]
    public void Valid_seeds_pass(ulong seed)
        => Assert.Equal(seed, RunSeed.Validate(seed, "seed"));

    [Theory]
    [InlineData(1UL << 60)]
    [InlineData(ulong.MaxValue)]
    public void Out_of_range_seeds_throw_with_param_name(ulong seed)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => RunSeed.Validate(seed, "runSeed"));
        Assert.Equal("runSeed", ex.ParamName);
    }
}
