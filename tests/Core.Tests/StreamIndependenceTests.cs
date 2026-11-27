using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Dungeon.Sim.Tools;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// V19t <c>StreamIndependence</c> (SV 5.2, DS1, DS7): adding a dummy child or consumer moves nothing else. Dummy rows
/// live only in this fixture, never in <c>content/</c>.
/// </summary>
public class StreamIndependenceTests
{
    private const string StreamsFile = "registry/streams.json";
    private const string LayoutId = "dungeon/floor/{N}/attempt/{a}/layout";
    private const string AttemptDummyId = "dungeon/floor/{N}/attempt/{a}/zzDummy";
    private const string LootDummyId = "loot/zzDummy";
    private const ulong Rs = 0x0123456789ABCDEFUL;

    private static readonly KeyValuePair<string, ulong>[] LayoutParameters =
        [new("sites", 0), new("maxNodes", 0), new("maxEdges", 0)];

    private static readonly Lazy<ContentFile[]> Files = new(() =>
        ContentDirectory.Read(Path.Combine(RepoRoot.Find(), "content")));

    private static readonly Lazy<ContentSet> Original = new(() => ContentLoader.Load(Files.Value));
    private static readonly Lazy<ContentSet> WithDummies = new(() => ContentLoader.Load(WithDummyRows(Files.Value)));

    [Fact]
    public void The_check_is_registered_as_a_pr_build_check()
    {
        var checks = Original.Value.Find<CheckRow>("V19.StreamIndependence");

        Assert.NotNull(checks);
        Assert.Equal(Phase.T, checks.Phase);
    }

    [Fact]
    public void A_new_child_and_consumer_move_nothing_else()
    {
        var before = Original.Value;
        var after = WithDummies.Value;
        Assert.Equal(before.All<StreamRow>().Length + 2, after.All<StreamRow>().Length);

        // Every original row keeps its key.
        foreach (var row in before.All<StreamRow>())
        {
            var indices = SampleIndices(before, row.Id);
            Assert.True(
                StreamRegistry.KeyOf(before, row.Id, Rs, indices) == StreamRegistry.KeyOf(after, row.Id, Rs, indices),
                $"{row.Id}: its key moved when dummy rows were added.");
        }

        // The layout stream draws the same values and closes with its count, with or without the dummies drawn.
        ulong[] alone = RunLayout(after, drawDummies: false);
        ulong[] beside = RunLayout(after, drawDummies: true);
        Assert.Equal(10, alone.Length);
        Assert.Equal(alone, beside);
        Assert.Equal(alone, RunLayout(before, drawDummies: false));

        // Every original handle member line survives unchanged in the regenerated text.
        var newLines = StreamHandlesWriter.Write(after).Split('\n').ToHashSet(StringComparer.Ordinal);
        foreach (string line in StreamHandlesWriter.Write(before).Split('\n'))
        {
            Assert.True(newLines.Contains(line), $"A generated line moved or changed: {line}");
        }
    }

    // Opens the real layout stream at GS2 and draws 10 values; optionally draws the two dummies 5 times each.
    private static ulong[] RunLayout(ContentSet set, bool drawDummies)
    {
        var run = new StageRun(set, "GS2", LayoutParameters);
        var layout = run.Open(Streams.Dungeon(Rs).Floor(1).Attempt(0).Layout.Handle);
        if (drawDummies)
        {
            var attemptDummy = run.Open(new StreamHandle(AttemptDummyId, StreamRegistry.KeyOf(set, AttemptDummyId, Rs, [new(1, null), new(0, null)])));
            var lootDummy = run.Open(new StreamHandle(LootDummyId, StreamRegistry.KeyOf(set, LootDummyId, Rs, [])));
            for (int i = 0; i < 5; i++)
            {
                attemptDummy.Next();
                lootDummy.Next();
            }
        }

        var values = new ulong[10];
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = layout.Next();
        }

        run.Close();
        return values;
    }

    // The repository files with two fixed dummy rows added to streams.json: a child of attempt, and one under loot.
    private static ContentFile[] WithDummyRows(ContentFile[] files) =>
        [.. files.Select(f => f.Name == StreamsFile ? f with { Text = AddRows(f.Text) } : f)];

    private static string AddRows(string text)
    {
        int open = text.IndexOf('[', StringComparison.Ordinal);
        return text[..(open + 1)]
            + DummyRow(AttemptDummyId, "dungeon/floor/{N}/attempt/{a}") + ","
            + DummyRow(LootDummyId, "loot") + ","
            + text[(open + 1)..];
    }

    private static string DummyRow(string path, string parent) =>
        "{\"path\": \"" + path + "\", \"alias\": [], \"parent\": \"" + parent + "\", \"label\": \"zzDummy\", \"indices\": [], "
        + "\"class\": \"fixed\", \"countFormula\": \"5\", \"stage\": \"GS2\", \"readers\": [], \"consumers\": [], "
        + "\"retryScope\": \"never\", \"rules\": [], \"keyType\": \"core\", \"owner\": \"MAIN\", \"source\": [], \"wave\": \"M1\"}";

    private static StreamIndexValue[] SampleIndices(ContentSet set, string rowId)
    {
        var chain = new List<StreamRow>();
        for (string? id = rowId; id is not null; id = chain[^1].Parent)
        {
            chain.Add(set.Find<StreamRow>(id) ?? throw new InvalidOperationException(id));
        }

        chain.Reverse();
        var values = new List<StreamIndexValue>();
        ulong n = 0;
        foreach (var index in chain.SelectMany(r => r.Indices))
        {
            values.Add(index.Type == IndexType.LabelId ? new StreamIndexValue(0, "a.b") : new StreamIndexValue(++n, null));
        }

        return [.. values];
    }
}
