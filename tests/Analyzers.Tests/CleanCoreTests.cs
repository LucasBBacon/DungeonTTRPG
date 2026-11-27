using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>Code that obeys DS11 must produce no analyzer diagnostics.</summary>
public class CleanCoreTests
{
    [Fact]
    public void Clean_integer_code_reports_nothing()
    {
        var d = Harness.Run("""
            public sealed class Counter
            {
                private int _count;
                private long _total;
                private ulong _bits;

                public long Add(int value)
                {
                    _count += value;
                    _total = _total * 3 + _count - 1;
                    _bits = (_bits << 1) | 1UL;
                    return _total + (long)_bits;
                }
            }
            """);
        Assert.Empty(d);
    }

    [Fact]
    public void Record_declarations_are_clean()
    {
        var d = Harness.Run("""
            public record Point(int X, int Y);
            public readonly record struct Cell(int Row, int Col);
            public class Keyed
            {
                public override int GetHashCode() => 0;
                public override bool Equals(object? obj) => obj is Keyed;
            }
            """);
        Assert.Empty(d);
    }
}
