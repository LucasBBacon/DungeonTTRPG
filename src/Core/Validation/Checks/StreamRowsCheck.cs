using System;
using System.Collections.Immutable;
using System.Text;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>V19.StreamRows</c>: every stream row is well formed (SV 4.2, SV 9.1, plan calls 2 and 3). A row fails when its id
/// is not <c>parent + "/" + label + ("/{index}")*</c> (<c>{name:}</c> for a <c>labelId</c> index; a root row's id starts at
/// its label); when a <c>fixed</c>, <c>fixed-per-data</c>, <c>leaf-formula</c> or <c>per-entry</c> row has no
/// <c>countFormula</c>, or a <c>node</c>, <c>vector</c>, <c>keyed</c>, <c>display</c> or <c>sequential-saved</c> row has one;
/// when a <c>vector</c> has no <c>length</c> or a <c>per-entry</c> row no <c>cap</c>; when a <c>fixed</c> formula has
/// parameters; when a <c>display</c> row's <c>keyType</c> is not <c>display</c>; when a <c>sim</c> row descends from a
/// root other than <c>simRoot</c>; or when a row sits under a keyed domain. The loader already refuses a
/// <c>length</c> or <c>cap</c> on any other class.
/// </summary>
public sealed class StreamRowsCheck : IStaticCheck
{
    /// <summary>The root every <c>sim</c> row descends from (SV 4.3).</summary>
    public const string SimRoot = "simRoot";

    private const string StreamsFile = "registry/streams.json";

    /// <inheritdoc />
    public CheckId Id { get; } = new("V19.StreamRows");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [StreamsFile];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        ArgumentNullException.ThrowIfNull(content);
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        var rows = content.RecordsIn(StreamsFile);
        for (int i = 0; i < rows.Length; i++)
        {
            var row = (StreamRow)rows[i];
            string at = Item("$.streams", i);
            void Fail(string field, string problem) => failures.Add(new CheckFailure(Id, StreamsFile, Member(at, field), problem));

            string expected = ExpectedId(row);
            if (!string.Equals(row.Id, expected, StringComparison.Ordinal))
            {
                Fail("path", $"\"{row.Id}\" is not its parent, label and indices: expected \"{expected}\".");
            }

            if (TakesCount(row.Class) is { } takes && takes != (row.Count is not null))
            {
                Fail("countFormula", takes ? $"a {Token(row.Class)} row needs a countFormula." : $"a {Token(row.Class)} row takes no countFormula.");
            }

            if (row.Class == CountClass.Vector && row.Length is null)
            {
                Fail("length", "a vector row needs a length.");
            }

            if (row.Class == CountClass.PerEntry && row.Cap is null)
            {
                Fail("cap", "a per-entry row needs a cap.");
            }

            if (row.Class == CountClass.Fixed && row.Count is { IsOwed: false } fixedCount && !fixedCount.Parameters.IsEmpty)
            {
                Fail("countFormula", $"a fixed row's count is a constant, but \"{fixedCount}\" reads {string.Join(", ", fixedCount.Parameters)}.");
            }

            if (row.Class == CountClass.Display && row.KeyType != KeyType.Display)
            {
                Fail("keyType", "a display row's keyType is display.");
            }

            if (row.Class == CountClass.Sim && Root(row, content) is var root && !string.Equals(root, SimRoot, StringComparison.Ordinal))
            {
                Fail("parent", $"a sim row descends from {SimRoot}, not from \"{root}\".");
            }

            if (row.Parent is { } parentId && content.Find<StreamRow>(parentId) is { } parent && IsDomain(parent, content))
            {
                Fail("parent", $"\"{parentId}\" is a keyed domain, which has no stream children.");
            }
        }

        return failures.ToImmutable();
    }

    /// <summary>
    /// True if <paramref name="row"/> is a keyed-hash domain: a <c>keyed</c> row whose parent is a <c>keyed</c> node
    /// (plan call 3), such as <c>h:object</c> under <c>checks</c>.
    /// </summary>
    internal static bool IsDomain(StreamRow row, ContentSet content) =>
        row.Class == CountClass.Keyed
        && row.Parent is { } parent
        && content.Find<StreamRow>(parent) is { Class: CountClass.Keyed };

    // The id plan call 2 gives a row: the parent's id, the label, then one {name} or {name:} segment per index.
    private static string ExpectedId(StreamRow row)
    {
        var sb = new StringBuilder();
        if (row.Parent is { } parent)
        {
            sb.Append(parent).Append('/');
        }

        sb.Append(row.Label);
        foreach (var index in row.Indices)
        {
            sb.Append("/{").Append(index.Name).Append(index.Type == IndexType.LabelId ? ":}" : "}");
        }

        return sb.ToString();
    }

    // True if the class needs a countFormula, false if it takes none, null if either is allowed (sim).
    private static bool? TakesCount(CountClass cls) => cls switch
    {
        CountClass.Fixed or CountClass.FixedPerData or CountClass.LeafFormula or CountClass.PerEntry => true,
        CountClass.Node or CountClass.Vector or CountClass.Keyed or CountClass.SequentialSaved or CountClass.Display => false,
        _ => null,
    };

    private static string Token(CountClass cls) => cls switch
    {
        CountClass.Node => "node",
        CountClass.Fixed => "fixed",
        CountClass.FixedPerData => "fixed-per-data",
        CountClass.LeafFormula => "leaf-formula",
        CountClass.PerEntry => "per-entry",
        CountClass.Vector => "vector",
        CountClass.Keyed => "keyed",
        CountClass.SequentialSaved => "sequential-saved",
        CountClass.Display => "display",
        _ => "sim",
    };

    // The id of the row's root, following parents; a parent cycle stops at the row count.
    private static string Root(StreamRow row, ContentSet content)
    {
        var at = row;
        int limit = content.RecordsIn(StreamsFile).Length;
        while (limit-- > 0 && at.Parent is { } parent && content.Find<StreamRow>(parent) is { } next)
        {
            at = next;
        }

        return at.Id;
    }
}
