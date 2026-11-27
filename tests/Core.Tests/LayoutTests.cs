using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>Locates the repository root so layout tests can inspect files outside the test assembly.</summary>
internal static class RepoRoot
{
    /// <summary>Walks up from the test output folder to the folder that holds <c>Dungeon.sln</c>.</summary>
    internal static string Find()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "Dungeon.sln")))
            {
                return dir.FullName;
            }
            dir = dir.Parent;
        }
        throw new InvalidOperationException("Dungeon.sln not found above " + AppContext.BaseDirectory);
    }
}

/// <summary>Checks that the repository matches the project table in MAIN 6.1.</summary>
public class LayoutTests
{
    [Fact]
    public void Repository_matches_MAIN_6_1()
    {
        var root = RepoRoot.Find();

        foreach (var file in new[]
        {
            "src/Core/Core.csproj",
            "src/Sim/Sim.csproj",
            "tests/Core.Tests/Core.Tests.csproj",
            "src/Analyzers/Analyzers.csproj",
            "tests/Analyzers.Tests/Analyzers.Tests.csproj",
            "dungeon-ttrpg/DungeonTTRPG.csproj",
        })
        {
            Assert.True(File.Exists(Path.Combine(root, file)), file + " is missing");
        }
        Assert.True(Directory.Exists(Path.Combine(root, "content")), "content/ is missing");

        var sln = File.ReadAllText(Path.Combine(root, "Dungeon.sln"));
        Assert.Contains(@"src\Sim\Sim.csproj", sln);
        Assert.DoesNotContain("DungeonTTRPG.csproj", sln);
    }
}
