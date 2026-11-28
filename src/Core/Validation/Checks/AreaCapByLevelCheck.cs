using System;
using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>content.ABL145.AreaCapByLevel</c> (AB 14.5, 8.1): "No area covers more than 25 squares at levels 1-6, or more than
/// 49 at levels 7-10." It fails a burst, cone or line record whose geometric size (<see cref="Areas.ShapeSize"/>)
/// exceeds <see cref="AreaCap"/>(1). Ability records carry no level yet, so every M1 record counts as level 1-6; M1-19
/// (class abilities) and M1-22 (monster abilities) supply the levels. It reads <c>abilities/abilities.json</c>.
/// </summary>
public sealed class AreaCapByLevelCheck : IStaticCheck
{
    private const string File = "abilities/abilities.json";

    /// <summary>The most squares an area may cover at <paramref name="level"/> (1 to 10): 25 up to level 6, 49 from level 7.</summary>
    public static int AreaCap(int level) => level switch
    {
        >= 1 and <= 6 => 25,
        >= 7 and <= 10 => 49,
        _ => throw new ArgumentOutOfRangeException(nameof(level), level, "A level is 1 to 10."),
    };

    /// <inheritdoc />
    public CheckId Id { get; } = new("content.ABL145.AreaCapByLevel");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [File];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        var records = content.RecordsIn(File);
        int cap = AreaCap(1);
        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is not AbilityDefinition { Target: { } target } ability || target.Shape == TargetShape.Single)
            {
                continue;
            }

            string at = ContentJson.Member(ContentJson.Item("$.abilities", i), "target");
            if (target.Size < 1)
            {
                failures.Add(new CheckFailure(Id, File, at, $"Ability \"{ability.Id}\" has an area of size {target.Size}; a size is at least 1."));
                continue;
            }

            AreaShape shape = target.Shape switch
            {
                TargetShape.Burst => new Burst(target.Size),
                TargetShape.Cone => new Cone(target.Size),
                _ => new Line(target.Size),
            };
            int size = Areas.ShapeSize(shape);
            if (size > cap)
            {
                failures.Add(new CheckFailure(
                    Id, File, at, $"Ability \"{ability.Id}\" covers {size} squares, over the cap of {cap} at levels 1-6 (AB 8.1, 14.5)."));
            }
        }

        return failures.ToImmutable();
    }
}
