using System.Collections.Immutable;
using Dungeon.Core.Validation.Checks;

namespace Dungeon.Core.Validation;

/// <summary>The registered static checks; each has a phase-<c>s</c> row in <c>registry/checks.json</c>.</summary>
public static class StaticChecks
{
    /// <summary>Every registered static check.</summary>
    public static ImmutableArray<IStaticCheck> All { get; } =
        [
            new ContentLoadCheck(),
            new ClassDeclaredCheck(),
            new NameRegistryCheck(),
            new R1ExactEqualityCheck(),
            new R2RetiredReusedCheck(),
            new R6InternalByKeyCheck(),
            new GreyedReasonKeysCheck(),
            new StreamRowsCheck(),
            new DomainDisjointCheck(),
            new LabelCollisionCheck(),
            new PartyInstantDeathCheck(),
        ];
}
