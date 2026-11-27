using System.Collections.Immutable;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>content.MAIN65.Load</c>: content loads and validates at startup (MAIN 6.5). The loader enforces
/// it by throwing; reaching the runner means the load succeeded, so this always passes. It exists so the
/// load has a registered row.
/// </summary>
public sealed class ContentLoadCheck : IStaticCheck
{
    /// <inheritdoc />
    public CheckId Id { get; } = new("content.MAIN65.Load");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content) => [];
}
