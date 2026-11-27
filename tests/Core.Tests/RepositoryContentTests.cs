using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The repository's own <c>content/</c> loads and passes every static check, so content validation runs under <c>dotnet test</c>.</summary>
public class RepositoryContentTests
{
    [Fact]
    public void Repository_content_loads_and_passes_validation()
    {
        var content = ContentLoader.Load(ReadContent());

        var report = ValidationRunner.Run(content, StaticChecks.All, new CheckCache());

        Assert.True(
            report.Passed,
            string.Join(Environment.NewLine, report.Failures.Select(f => $"{f.File} {f.Field}: {f.Problem}")));

        // Code against file: every registered check has a row, and every phase-s row has a check.
        var codeIds = StaticChecks.All.Select(c => c.Id.Value).OrderBy(s => s, StringComparer.Ordinal).ToArray();
        var rowIds = content.All<CheckRow>().Where(r => r.Phase == Phase.S).Select(r => r.Id).ToArray();
        Assert.All(codeIds, id => Assert.NotNull(content.Find<CheckRow>(id)));
        Assert.Equal(rowIds, codeIds);
    }

    /// <summary>Every <c>*.json</c> under <c>content/</c>, named relative to it with <c>/</c>, in ordinal order.</summary>
    private static IEnumerable<ContentFile> ReadContent()
    {
        string root = Path.Combine(RepoRoot.Find(), "content");
        return Directory.EnumerateFiles(root, "*.json", SearchOption.AllDirectories)
            .Select(path => new ContentFile(Path.GetRelativePath(root, path).Replace('\\', '/'), File.ReadAllText(path)))
            .OrderBy(f => f.Name, StringComparer.Ordinal)
            .ToArray();
    }
}
