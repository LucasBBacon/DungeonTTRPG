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

    [Fact]
    public void Ordinal_and_invariant_calls_are_clean()
    {
        var d = Harness.Run("""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using Dungeon.Core.Determinism;

            public enum Kind { A, B }
            public sealed class Unit
            {
                public int Id { get; init; }
                public Kind Kind { get; init; }
                public string Name { get; init; } = "";
            }

            [TotalComparer]
            public sealed class MarkedComparer : IComparer<string>
            {
                public int Compare(string? x, string? y) => string.CompareOrdinal(x, y);
            }

            public static class C
            {
                public static object M(string a, string b, List<string> list, List<Unit> units)
                {
                    bool equal = string.Equals(a, b, StringComparison.Ordinal);
                    bool starts = a.StartsWith("x", StringComparison.Ordinal);
                    int ordinal = string.CompareOrdinal(a, b);
                    string upper = a.ToUpperInvariant();
                    int index = a.IndexOf('x');
                    list.Sort(new MarkedComparer());
                    list.Sort(StringComparer.Ordinal);
                    var byId = units.OrderBy(u => u.Id).ToList();
                    var byKind = units.OrderBy(u => u.Kind).ThenBy(u => u.Name, new MarkedComparer()).ToList();
                    return (equal, starts, ordinal, upper, index, byId, byKind);
                }
            }
            """);
        Assert.Empty(d);
    }
}
