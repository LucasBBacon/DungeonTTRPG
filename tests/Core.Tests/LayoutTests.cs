using System.Text.RegularExpressions;
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
        Assert.DoesNotContain("DungeonTTRPG.csproj", sln);
    }

    /// <summary>
    /// Every MAIN 6.1 project except the Godot one is in <c>Dungeon.sln</c>, under its solution folder, so
    /// <c>dotnet test Dungeon.sln</c> (CI) builds the analyzer and runs its tests.
    /// </summary>
    [Theory]
    [InlineData(@"src\Core\Core.csproj", "src")]
    [InlineData(@"src\Sim\Sim.csproj", "src")]
    [InlineData(@"src\Analyzers\Analyzers.csproj", "src")]
    [InlineData(@"tests\Core.Tests\Core.Tests.csproj", "tests")]
    [InlineData(@"tests\Analyzers.Tests\Analyzers.Tests.csproj", "tests")]
    public void Solution_holds_project_under_its_folder(string projectPath, string folder)
    {
        var sln = File.ReadAllText(Path.Combine(RepoRoot.Find(), "Dungeon.sln"));
        var projects = Regex.Matches(sln, "^Project\\(\"\\{[^}]+\\}\"\\) = \"([^\"]+)\", \"([^\"]+)\", \"(\\{[^}]+\\})\"", RegexOptions.Multiline)
            .Select(m => (Name: m.Groups[1].Value, Path: m.Groups[2].Value, Guid: m.Groups[3].Value))
            .ToList();

        var project = projects.SingleOrDefault(p => p.Path == projectPath);
        Assert.True(project.Guid is not null, projectPath + " is not in Dungeon.sln");
        var solutionFolder = Assert.Single(projects, p => p.Name == folder && p.Path == folder);
        Assert.Matches(
            new Regex("^\\s*" + Regex.Escape(project.Guid) + " = " + Regex.Escape(solutionFolder.Guid) + "\\s*$", RegexOptions.Multiline),
            sln);
    }
}
