using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>content.DH21.PartyInstantDeath</c> (DH 10.2, 21.2; M1-10 plan call 15): instant death, save-or-die and
/// set-HP-to-0 are refused for party targets. The effects vocabulary has no operation that sets or zeroes current HP,
/// so this is a tripwire for the step that adds one. It fails when <see cref="ModifierOp"/> has a value outside the
/// reviewed allow-list, and when the <c>maxHp</c> derived number loses its floor of 1. It reads code, not content:
/// <see cref="Reads"/> is empty and the <see cref="ContentSet"/> is ignored. Failures name the code file and member.
/// </summary>
/// <param name="numbers">The derived-number table; <see cref="DerivedNumbers.Core"/> when null.</param>
/// <param name="ops">The modifier operations; every <see cref="ModifierOp"/> value when null.</param>
public sealed class PartyInstantDeathCheck(ImmutableArray<DerivedNumber>? numbers = null, IEnumerable<ModifierOp>? ops = null)
    : IStaticCheck
{
    private const string OpsFile = "src/Core/Effects/EffectDefinition.cs";
    private const string NumbersFile = "src/Core/Effects/Registries.cs";

    // The operations reviewed against DH 10.2: none sets or zeroes current HP (Zero sets a derived number to 0).
    private static readonly ImmutableArray<ModifierOp> Reviewed =
        [ModifierOp.Add, ModifierOp.Halve, ModifierOp.Zero, ModifierOp.RollMode, ModifierOp.Flag];

    private readonly ImmutableArray<DerivedNumber> _numbers = numbers ?? DerivedNumbers.Core;
    private readonly ImmutableArray<ModifierOp> _ops = (ops ?? Enum.GetValues<ModifierOp>())
        .Distinct()
        .OrderBy(o => (int)o)
        .ToImmutableArray();

    /// <inheritdoc />
    public CheckId Id { get; } = new("content.DH21.PartyInstantDeath");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();

        foreach (var op in _ops)
        {
            if (!Reviewed.Contains(op))
            {
                failures.Add(new CheckFailure(
                    Id,
                    OpsFile,
                    $"ModifierOp.{op}",
                    "An unreviewed modifier operation; add it to the PartyInstantDeath allow-list once it is shown not to set or zero a party unit's current HP (DH 10.2)."));
            }
        }

        var maxHp = _numbers.FirstOrDefault(n => string.Equals(n.Id, HitPoints.MaxHpId, StringComparison.Ordinal));
        if (maxHp is null || maxHp.Floor is not >= 1)
        {
            failures.Add(new CheckFailure(
                Id,
                NumbersFile,
                $"DerivedNumbers.Core[{HitPoints.MaxHpId}].Floor",
                maxHp is null
                    ? "The maxHp derived number is missing."
                    : "The maxHp derived number has lost its floor of 1, so a maximum of 0 could kill a party unit outright (DH 10.2)."));
        }

        return failures.ToImmutable();
    }
}
