using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>DUN002-004, 008, 009: banned APIs (DS11).</summary>
public class BannedApiTests
{
    [Theory]
    [InlineData("class C { public static object M() => new System.Random(1); }")]
    [InlineData("class C { System.Random _r = null!; public System.Random R => _r; }")]
    [InlineData("class C { public static int M() => System.Random.Shared.Next(); }")]
    public void System_random_is_reported(string source) =>
        Assert.Equal(new[] { "DUN002" }, Harness.Ids(Harness.Run(source)));

    [Theory]
    [InlineData("System.DateTime.Now")]
    [InlineData("System.DateTime.UtcNow")]
    [InlineData("System.DateTimeOffset.UtcNow")]
    [InlineData("System.Environment.TickCount")]
    [InlineData("System.Environment.TickCount64")]
    [InlineData("System.Guid.NewGuid()")]
    [InlineData("System.Diagnostics.Stopwatch.StartNew()")]
    public void Clocks_and_ambient_entropy_are_reported(string expression) =>
        Assert.Equal(
            new[] { "DUN003" },
            Harness.Ids(Harness.Run($"class C {{ public static void M() {{ _ = {expression}; }} }}")));

    [Theory]
    [InlineData("class C { public static int M() => \"a\".GetHashCode(); }")]
    [InlineData("class C { public static int M() { int x = 1; return x.GetHashCode(); } }")]
    [InlineData("class C { public static int M() => System.HashCode.Combine(1, 2); }")]
    [InlineData("class C { public static object M() => new System.HashCode(); }")]
    public void Hashing_is_reported(string source) =>
        Assert.Equal(new[] { "DUN004" }, Harness.Ids(Harness.Run(source)));

    [Theory]
    [InlineData("class C { public static void M() { System.Threading.Tasks.Parallel.For(0, 2, _ => { }); } }")]
    [InlineData("class C { public static object M() => System.Linq.ParallelEnumerable.AsParallel(new[] { 1 }); }")]
    [InlineData("using System.Linq; class C { public static object M() => new[] { 1 }.AsParallel(); }")]
    public void Parallelism_is_reported(string source) =>
        Assert.Equal(new[] { "DUN008" }, Harness.Ids(Harness.Run(source)));

    [Fact]
    public void Godot_symbol_reference_is_reported()
    {
        var d = Harness.Run(
            "class C { Godot.Node _n = null!; public Godot.Node N => _n; }",
            extraReferences: new[] { GodotSharp() });
        Assert.Equal(new[] { "DUN009" }, Harness.Ids(d));
    }

    [Fact]
    public void Godot_assembly_reference_alone_is_reported()
    {
        var d = Harness.Run("class C { }", extraReferences: new[] { GodotSharp() });
        Assert.Equal(new[] { "DUN009" }, Harness.Ids(d));
    }

    [Fact]
    public void Godot_symbol_in_a_differently_named_assembly_is_reported()
    {
        var d = Harness.Run(
            "class C { Godot.Node _n = null!; public Godot.Node N => _n; }",
            extraReferences: new[] { GodotSharp("NotGodot") });
        Assert.Equal(new[] { "DUN009" }, Harness.Ids(d));
    }

    /// <summary>An in-memory assembly declaring <c>Godot.Node</c>.</summary>
    private static MetadataReference GodotSharp(string assemblyName = "GodotSharp")
    {
        var compilation = CSharpCompilation.Create(
            assemblyName,
            new[] { CSharpSyntaxTree.ParseText("namespace Godot { public class Node {} }") },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        using var stream = new MemoryStream();
        var result = compilation.Emit(stream);
        Assert.True(result.Success, string.Join(Environment.NewLine, result.Diagnostics));
        return MetadataReference.CreateFromImage(stream.ToArray());
    }
}
