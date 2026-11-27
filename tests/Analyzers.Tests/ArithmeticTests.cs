using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>
/// DUN010: signed <c>/</c> and <c>%</c> outside <c>IntMath</c>; DUN011: <c>unchecked</c> outside the
/// file named by <c>DungeonWrappingArithmeticFile</c> (DS11).
/// </summary>
public class ArithmeticTests
{
    private const string PropertyName = "DungeonWrappingArithmeticFile";

    private const string UncheckedSource = """
        public static class C
        {
            public static int Add(int a, int b) => unchecked(a + b);

            public static ulong Mul(ulong a, ulong b)
            {
                unchecked
                {
                    return a * b;
                }
            }
        }
        """;

    [Theory]
    [InlineData("int", "_ = a / b;")]
    [InlineData("long", "_ = a / b;")]
    [InlineData("int", "_ = a % b;")]
    [InlineData("long", "_ = a % b;")]
    [InlineData("int", "a /= b;")]
    [InlineData("long", "a /= b;")]
    [InlineData("int", "a %= b;")]
    [InlineData("long", "a %= b;")]
    [InlineData("int", "_ = a / 2;")]
    [InlineData("long", "_ = a / 2;")]
    [InlineData("int", "_ = System.Math.DivRem(a, b);")]
    [InlineData("long", "_ = System.Math.DivRem(a, b);")]
    public void Signed_division_is_reported(string type, string statement)
    {
        var d = Harness.Run($"public static class C {{ public static void M({type} a, {type} b) {{ {statement} }} }}");
        Assert.Equal(new[] { "DUN010" }, Harness.Ids(d));
    }

    [Theory]
    [InlineData("uint", "_ = a / b; _ = a % b; a /= b; a %= b;")]
    [InlineData("ulong", "_ = a / b; _ = a % b; a /= b; a %= b;")]
    [InlineData("uint", "_ = a / 2; _ = a % 2; a /= 2; a %= 2;")]
    [InlineData("int", "const int x = -7 / 2; const int y = -7 % 2; _ = x + y;")]
    [InlineData("int", "_ = Dungeon.Core.IntMath.FloorDiv(a, b); _ = Dungeon.Core.IntMath.FloorMod(a, b);")]
    [InlineData("long", "_ = Dungeon.Core.IntMath.FloorDiv(a, b); _ = Dungeon.Core.IntMath.FloorMod(a, b);")]
    public void Unsigned_constant_and_IntMath_division_is_clean(string type, string statement)
    {
        var d = Harness.Run($"public static class C {{ public static void M({type} a, {type} b) {{ {statement} }} }}");
        Assert.Empty(d);
    }

    [Fact]
    public void IntMath_itself_is_exempt_by_type_not_path()
    {
        var source = File.ReadAllText(Path.Combine(RepoRoot(), "src", "Core", "IntMath.cs"));
        var d = Harness.Run(source, path: "src/Core/IntMath.cs");
        Assert.Empty(d);
    }

    [Fact]
    public void Division_message_names_DS11_and_the_helpers()
    {
        var d = Harness.Run("public static class C { public static int M(int a, int b) => a / b; }");
        var message = Assert.Single(d).GetMessage();
        Assert.StartsWith("DUN010 (DS11)", message);
        Assert.Contains("FloorDiv", message);
    }

    [Fact]
    public void Unchecked_outside_named_file_is_reported()
    {
        var d = Harness.Run(
            UncheckedSource,
            path: "src/Core/Other.cs",
            buildProperties: new Dictionary<string, string> { [PropertyName] = "Random/Pcg32.cs" });
        Assert.Equal(new[] { "DUN011" }, Harness.Ids(d));
        Assert.Equal(2, d.Length);
    }

    [Fact]
    public void Unchecked_is_reported_when_no_file_is_named()
    {
        var d = Harness.Run(UncheckedSource, path: "src/Core/Random/Pcg32.cs");
        Assert.Equal(new[] { "DUN011" }, Harness.Ids(d));
        Assert.Equal(2, d.Length);
    }

    [Theory]
    [InlineData(@"C:\repo\src\Core\Random\Pcg32.cs", "Random/Pcg32.cs")]
    [InlineData(@"C:\repo\src\Core\Random\Pcg32.cs", @"Random\Pcg32.cs")]
    [InlineData("/repo/src/Core/Random/Pcg32.cs", "Random/Pcg32.cs")]
    [InlineData("/repo/src/Core/Random/Pcg32.cs", @"Random\Pcg32.cs")]
    public void Unchecked_allowed_in_named_file_with_either_separator(string path, string property)
    {
        var d = Harness.Run(
            UncheckedSource,
            path: path,
            buildProperties: new Dictionary<string, string> { [PropertyName] = property });
        Assert.Empty(d);
    }

    [Fact]
    public void Unchecked_in_a_file_that_only_ends_with_the_name_is_reported()
    {
        var d = Harness.Run(
            UncheckedSource,
            path: "/repo/src/Core/NotRandom/Pcg32.cs",
            buildProperties: new Dictionary<string, string> { [PropertyName] = "Random/Pcg32.cs" });
        Assert.Equal(new[] { "DUN011" }, Harness.Ids(d));
    }

    [Fact]
    public void Checked_is_clean()
    {
        var d = Harness.Run("public static class C { public static long M(long a) => checked(a + 1); }");
        Assert.Empty(d);
    }

    private static string RepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "Dungeon.sln")))
        {
            dir = dir.Parent;
        }
        return dir?.FullName ?? throw new InvalidOperationException("Dungeon.sln not found above " + AppContext.BaseDirectory);
    }
}
