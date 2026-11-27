using System;
using System.Collections.Immutable;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>V19.DomainDisjoint</c> (K-rule 6): on any one parent, a stream child and a keyed-hash domain never share a label,
/// and every keyed domain is prefixed <c>h:</c>. A domain is a <c>keyed</c> row under a <c>keyed</c> node (plan call 3).
/// The route's <c>themePerm</c>, <c>bossOrder</c>, <c>finalOrder</c> and <c>omen</c> under <c>dungeon/route</c> are the
/// approved unprefixed domains (SV 4.3). A failure is reported at the later row of the file.
/// </summary>
public sealed class DomainDisjointCheck : IStaticCheck
{
    /// <summary>The prefix of a new keyed domain (K-rule 6).</summary>
    public const string DomainPrefix = "h:";

    private const string StreamsFile = "registry/streams.json";
    private const string RouteId = "dungeon/route";

    // The approved unprefixed domains under dungeon/route (SV 4.3), ordinal.
    private static readonly ImmutableArray<string> RouteDomains = ["bossOrder", "finalOrder", "omen", "themePerm"];

    /// <inheritdoc />
    public CheckId Id { get; } = new("V19.DomainDisjoint");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [StreamsFile];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        ArgumentNullException.ThrowIfNull(content);
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        var rows = content.RecordsIn(StreamsFile);
        for (int j = 0; j < rows.Length; j++)
        {
            var row = (StreamRow)rows[j];
            string field = Member(Item("$.streams", j), "label");
            bool domain = StreamRowsCheck.IsDomain(row, content);
            if (domain && !row.Label.StartsWith(DomainPrefix, StringComparison.Ordinal) && !IsApprovedRouteDomain(row))
            {
                failures.Add(new CheckFailure(Id, StreamsFile, field, $"the keyed domain \"{row.Label}\" is not prefixed {DomainPrefix}."));
            }

            for (int i = 0; i < j; i++)
            {
                var earlier = (StreamRow)rows[i];
                if (string.Equals(earlier.Parent, row.Parent, StringComparison.Ordinal)
                    && string.Equals(earlier.Label, row.Label, StringComparison.Ordinal)
                    && StreamRowsCheck.IsDomain(earlier, content) != domain)
                {
                    failures.Add(new CheckFailure(
                        Id,
                        StreamsFile,
                        field,
                        $"\"{row.Label}\" under \"{row.Parent}\" is both a stream child and a keyed domain (\"{earlier.Id}\")."));
                }
            }
        }

        return failures.ToImmutable();
    }

    private static bool IsApprovedRouteDomain(StreamRow row) =>
        string.Equals(row.Parent, RouteId, StringComparison.Ordinal)
        && ImmutableArray.BinarySearch(RouteDomains, row.Label, StringComparer.Ordinal) >= 0;
}
