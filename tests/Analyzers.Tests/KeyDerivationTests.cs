using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>
/// DUN012: <c>KeyAlgebra.Derive</c>, <c>KeyAlgebra.H</c> and <c>DrawStream.Open</c> are banned outside the files named by
/// <c>DungeonKeyDerivationFiles</c> (DS1, SV 4.2); streams are opened through the generated <c>StreamHandles</c>.
/// </summary>
public class KeyDerivationTests
{
    private const string PropertyName = "DungeonKeyDerivationFiles";
    private const string ExemptFiles = "Random/KeyAlgebra.cs;Random/KeyedSort.cs;Random/StreamRegistry.cs;Random/StreamHandles.g.cs";

    private const string RawCalls = """
        using Dungeon.Core;

        public static class C
        {
            public static ulong A() => KeyAlgebra.Derive(1, "x");
            public static ulong B() => KeyAlgebra.H(1, "x");
            public static DrawStream S() => DrawStream.Open(1);
        }
        """;

    private static Dictionary<string, string> Properties(string files = ExemptFiles) =>
        new() { [PropertyName] = files };

    [Theory]
    [InlineData("_ = KeyAlgebra.Derive(1, \"x\");", "KeyAlgebra.Derive")]
    [InlineData("_ = KeyAlgebra.Derive(1, \"x\", 2, 3);", "KeyAlgebra.Derive")]
    [InlineData("_ = KeyAlgebra.H(1, \"x\");", "KeyAlgebra.H")]
    [InlineData("_ = DrawStream.Open(1);", "DrawStream.Open")]
    [InlineData("System.Func<ulong, string, ulong[], ulong> f = KeyAlgebra.Derive;", "KeyAlgebra.Derive")]
    [InlineData("var f = new System.Func<ulong, DrawStream>(DrawStream.Open);", "DrawStream.Open")]
    [InlineData("System.Func<ulong, string, ulong[], ulong> f = KeyAlgebra.H;", "KeyAlgebra.H")]
    public void Raw_derivation_outside_the_handle_files_is_reported(string statement, string member)
    {
        var d = Harness.Run(
            "using Dungeon.Core;\npublic static class C { public static void M() { " + statement + " } }",
            path: "src/Core/Other.cs",
            buildProperties: Properties());
        Assert.Equal(new[] { "DUN012" }, Harness.Ids(d));
        var diagnostic = Assert.Single(d);
        Assert.Equal(
            "DUN012 (DS1, SV 4.2): open streams only through StreamHandles; '" + member + "' is banned here",
            diagnostic.GetMessage(System.Globalization.CultureInfo.InvariantCulture));
    }

    [Theory]
    [InlineData("_ = new DrawStream(1, 0);", "new DrawStream")]
    [InlineData("DrawStream s = new(1, 0);", "new DrawStream")]
    [InlineData("_ = new DrawStream();", "new DrawStream")]
    [InlineData("_ = new DrawStream { Key = 1 };", "new DrawStream")]
    [InlineData("_ = default(DrawStream);", "default(DrawStream)")]
    [InlineData("DrawStream s = default;", "default(DrawStream)")]
    [InlineData("_ = KeyAlgebra.Absorb(1, 42);", "KeyAlgebra.Absorb")]
    [InlineData("System.Func<ulong, ulong, ulong> f = KeyAlgebra.Absorb;", "KeyAlgebra.Absorb")]
    public void Building_a_stream_or_key_by_hand_is_reported(string statement, string member)
    {
        var d = Harness.Run(
            "using Dungeon.Core;\npublic static class C { public static void M() { " + statement + " } }",
            path: "src/Core/Other.cs",
            buildProperties: Properties());
        Assert.Equal(new[] { "DUN012" }, Harness.Ids(d));
        var diagnostic = Assert.Single(d);
        Assert.Equal(
            "DUN012 (DS1, SV 4.2): open streams only through StreamHandles; '" + member + "' is banned here",
            diagnostic.GetMessage(System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void Rekeying_a_stream_with_a_with_expression_is_reported()
    {
        var d = Harness.Run(
            """
            using Dungeon.Core;

            public static class C
            {
                public static DrawStream M(DrawStream s) => s with { Key = 7 };
            }
            """,
            path: "src/Core/Other.cs",
            buildProperties: Properties());
        var diagnostic = Assert.Single(d);
        Assert.Equal("DUN012", diagnostic.Id);
        Assert.Equal(
            "DUN012 (DS1, SV 4.2): open streams only through StreamHandles; 'DrawStream with' is banned here",
            diagnostic.GetMessage(System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void Hand_built_streams_are_clean_in_the_key_algebra_file()
    {
        var d = Harness.Run(
            """
            using Dungeon.Core;

            public static class C
            {
                public static DrawStream A() => new DrawStream(KeyAlgebra.Absorb(1, 2), 0);
                public static DrawStream B(DrawStream s) => s with { Counter = 3 };
                public static DrawStream D() => default;
            }
            """,
            path: "/repo/src/Core/Random/KeyAlgebra.cs",
            buildProperties: Properties());
        Assert.Empty(d);
    }

    [Fact]
    public void Other_records_with_and_default_are_clean()
    {
        var d = Harness.Run(
            """
            using Dungeon.Core;

            public readonly record struct P(int X);

            public static class C
            {
                public static P M(P p, DrawStream s)
                {
                    var q = p with { X = 1 };
                    P r = default;
                    var (_, next) = s.Next();
                    _ = next.Skip(2);
                    return new P(q.X + r.X);
                }
            }
            """,
            path: "src/Core/Other.cs",
            buildProperties: Properties());
        Assert.Empty(d);
    }

    [Fact]
    public void A_using_static_call_is_reported()
    {
        var d = Harness.Run(
            """
            using static Dungeon.Core.KeyAlgebra;

            public static class C
            {
                public static ulong M() => Derive(1, "x") ^ H(1, "y");
            }
            """,
            path: "src/Core/Other.cs",
            buildProperties: Properties());
        Assert.Equal(new[] { "DUN012" }, Harness.Ids(d));
        Assert.Equal(2, d.Length);
    }

    [Fact]
    public void A_call_through_an_alias_is_reported()
    {
        var d = Harness.Run(
            """
            using Stream = Dungeon.Core.DrawStream;
            using Keys = Dungeon.Core.KeyAlgebra;

            public static class C
            {
                public static Stream M() => Stream.Open(Keys.Derive(1, "x"));
            }
            """,
            path: "src/Core/Other.cs",
            buildProperties: Properties());
        Assert.Equal(new[] { "DUN012" }, Harness.Ids(d));
        Assert.Equal(2, d.Length);
    }

    [Fact]
    public void Raw_derivation_is_reported_when_no_file_is_named()
    {
        var d = Harness.Run(RawCalls, path: "src/Core/Random/StreamHandles.g.cs");
        Assert.Equal(new[] { "DUN012" }, Harness.Ids(d));
        Assert.Equal(3, d.Length);
    }

    [Fact]
    public void Generated_code_outside_the_handle_files_is_reported()
    {
        var d = Harness.Run(RawCalls, path: "src/Core/Random/Other.g.cs", buildProperties: Properties());
        Assert.Equal(new[] { "DUN012" }, Harness.Ids(d));
        Assert.Equal(3, d.Length);
    }

    [Theory]
    [InlineData(@"C:\repo\src\Core\Random\StreamHandles.g.cs", ExemptFiles)]
    [InlineData("/repo/src/Core/Random/StreamHandles.g.cs", ExemptFiles)]
    [InlineData(@"C:\repo\src\Core\Random\StreamHandles.g.cs", @"Random\KeyAlgebra.cs;Random\StreamHandles.g.cs")]
    [InlineData("/repo/src/Core/Random/StreamHandles.g.cs", @"Random\KeyAlgebra.cs; .\Random\StreamHandles.g.cs ")]
    [InlineData(@"C:\repo\src\Core\Random\StreamHandles.g.cs", "Random/KeyAlgebra.cs|Random/StreamHandles.g.cs")]
    [InlineData("/repo/src/Core/Random/KeyAlgebra.cs", ExemptFiles)]
    [InlineData("/repo/src/Core/Random/KeyedSort.cs", ExemptFiles)]
    [InlineData("/repo/src/Core/Random/StreamRegistry.cs", ExemptFiles)]
    public void The_handle_files_are_clean_with_either_separator(string path, string files)
    {
        var d = Harness.Run(RawCalls, path: path, buildProperties: Properties(files));
        Assert.Empty(d);
    }

    [Theory]
    [InlineData(@"C:\repo\src\Core\Random\StreamHandles.g.cs", @"C:\repo\src\Core\")]
    [InlineData("/repo/src/Core/Random/StreamHandles.g.cs", "/repo/src/Core")]
    public void The_handle_files_are_clean_under_the_project_folder(string path, string projectDir)
    {
        var d = Harness.Run(
            RawCalls,
            path: path,
            buildProperties: new Dictionary<string, string> { [PropertyName] = ExemptFiles, ["ProjectDir"] = projectDir });
        Assert.Empty(d);
    }

    [Fact]
    public void A_deeper_file_with_the_same_suffix_is_reported()
    {
        var d = Harness.Run(
            RawCalls,
            path: "/repo/src/Core/Other/Random/StreamHandles.g.cs",
            buildProperties: new Dictionary<string, string> { [PropertyName] = ExemptFiles, ["ProjectDir"] = "/repo/src/Core/" });
        Assert.Equal(new[] { "DUN012" }, Harness.Ids(d));
    }

    [Fact]
    public void LabelId_Draw_Bound_and_Mix_are_clean()
    {
        var d = Harness.Run(
            """
            using Dungeon.Core;

            public static class C
            {
                public static ulong M(DrawStream s)
                {
                    var (u, _) = s.Next();
                    return KeyAlgebra.LabelId("x") ^ KeyAlgebra.Draw(1, 0) ^ KeyAlgebra.Bound(u, 6) ^ KeyAlgebra.Mix(2);
                }
            }
            """,
            path: "src/Core/Other.cs",
            buildProperties: Properties());
        Assert.Empty(d);
    }
}
