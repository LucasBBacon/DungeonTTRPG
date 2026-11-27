using System.Collections.Immutable;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation;

/// <summary>
/// The <c>s</c> part of a registered check: it reads loaded content and reports failures. Its
/// <see cref="Reads"/> is its declared read set (plan call 4), the key of its cached result.
/// </summary>
public interface IStaticCheck
{
    /// <summary>The check's registered id; its <see cref="CheckRow"/> must have phase <c>s</c>.</summary>
    CheckId Id { get; }

    /// <summary>The content file names the check reads, such as <c>registry/checks.json</c>.</summary>
    ImmutableArray<string> Reads { get; }

    /// <summary>Runs the check; an empty result is a pass.</summary>
    ImmutableArray<CheckFailure> Run(ContentSet content);
}
