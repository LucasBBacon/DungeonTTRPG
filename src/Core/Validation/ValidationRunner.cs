using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation;

/// <summary>
/// Runs the static (<c>s</c>) checks over loaded content, sequentially, in ascending (tier, ordinal id)
/// order (SV 5.3). A check whose read-set digest has a cached result is not run again.
/// </summary>
public static class ValidationRunner
{
    /// <summary>
    /// Runs <paramref name="checks"/> over <paramref name="content"/>. Every check must have a
    /// <see cref="CheckRow"/> with phase <c>s</c>, or <see cref="InvalidOperationException"/> names it.
    /// </summary>
    public static ValidationReport Run(ContentSet content, IReadOnlyList<IStaticCheck> checks, CheckCache cache)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(checks);
        ArgumentNullException.ThrowIfNull(cache);

        var ordered = checks
            .Select(c => c ?? throw new ArgumentException("A check is null.", nameof(checks)))
            .OrderBy(c => BuildTier.Of(c.Id))
            .ThenBy(c => c.Id.Value, StringComparer.Ordinal)
            .ToImmutableArray();

        for (int i = 0; i < ordered.Length; i++)
        {
            var id = ordered[i].Id;
            if (i > 0 && ordered[i - 1].Id == id)
            {
                throw new ArgumentException($"{id} is given twice.", nameof(checks));
            }

            var row = content.Find<CheckRow>(id.Value)
                ?? throw new InvalidOperationException($"{id} has no row in registry/checks.json.");
            if (row.Phase != Phase.S)
            {
                throw new InvalidOperationException($"{id} is a static check, but its row has phase {row.PhaseText}, not s.");
            }
        }

        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        foreach (var check in ordered)
        {
            var readSet = content.DigestOf(check.Reads);
            if (!cache.TryGet(check.Id, readSet, out var result))
            {
                result = check.Run(content);
                cache.Put(check.Id, readSet, result);
            }

            failures.AddRange(result);
        }

        return new ValidationReport(failures
            .OrderBy(f => f.Check.Value, StringComparer.Ordinal)
            .ThenBy(f => f.File, StringComparer.Ordinal)
            .ThenBy(f => f.Field, StringComparer.Ordinal)
            .ThenBy(f => f.Problem, StringComparer.Ordinal)
            .ToImmutableArray());
    }
}
