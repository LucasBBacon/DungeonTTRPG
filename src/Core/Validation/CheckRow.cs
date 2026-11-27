using System.Collections.Immutable;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation;

/// <summary>
/// A check-registry row, with exactly the SV 9.1 fields:
/// <c>{id: "Vn.Name", owner, phase: s|g|r|t, point?, deps[], uses[], class, action, rerunSpan?, proofRef?, wave, ciTier, budgetMs?}</c>.
/// <see cref="Phase"/> and <see cref="Class"/> are kept as written here and parsed by the validator framework.
/// <see cref="Deps"/>, <see cref="Uses"/> and <see cref="ProofRef"/> name check ids and are resolved at load.
/// </summary>
public sealed record CheckRow(
    string File,
    string Id,
    string Owner,
    string Phase,
    string? Point,
    ImmutableArray<string> Deps,
    ImmutableArray<string> Uses,
    string Class,
    string Action,
    string? RerunSpan,
    string? ProofRef,
    string Wave,
    string CiTier,
    int? BudgetMs) : ContentRecord(File, Id)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(13);
        w.String(0, Id);
        w.String(1, Owner);
        w.String(2, Phase);
        w.OptionalString(3, Point);
        w.List(4, Deps, (iw, s) => iw.String(0, s));
        w.List(5, Uses, (iw, s) => iw.String(0, s));
        w.String(6, Class);
        w.String(7, Action);
        w.OptionalString(8, RerunSpan);
        w.OptionalString(9, ProofRef);
        w.String(10, Wave);
        w.String(11, CiTier);
        w.OptionalInt32(12, BudgetMs);
        w.EndRecord();
    }
}
