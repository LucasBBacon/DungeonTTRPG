using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Dungeon.Core.Validation;
using Dungeon.Core.Validation.Checks;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// <c>content.DH21.PartyInstantDeath</c> (DH 10.2, 21.2; M1-10 plan call 15): the effects vocabulary has no operation
/// that sets or zeroes current HP, and the maximum HP keeps its floor of 1. A tripwire for the step that adds one.
/// </summary>
public class PartyInstantDeathCheckTests
{
    private static readonly CheckId Id = new("content.DH21.PartyInstantDeath");

    [Fact]
    public void The_check_passes_on_the_current_vocabulary()
    {
        var check = new PartyInstantDeathCheck();

        Assert.Equal(Id, check.Id);
        Assert.Empty(check.Reads);
        Assert.Empty(check.Run(ContentLoader.Load([])));
    }

    [Fact]
    public void The_check_fails_when_max_hp_loses_its_floor()
    {
        var unfloored = DerivedNumbers.Core
            .Select(n => n.Id == HitPoints.MaxHpId ? n with { Floor = null } : n)
            .ToImmutableArray();
        var missing = DerivedNumbers.Core.Where(n => n.Id != HitPoints.MaxHpId).ToImmutableArray();

        foreach (var numbers in new[] { unfloored, missing })
        {
            var failure = Assert.Single(new PartyInstantDeathCheck(numbers).Run(ContentLoader.Load([])));
            Assert.Equal(Id, failure.Check);
            Assert.Equal("src/Core/Effects/Registries.cs", failure.File);
            Assert.Contains("maxHp", failure.Field, StringComparison.Ordinal);
        }
    }

    [Fact]
    public void The_check_names_an_unreviewed_modifier_op()
    {
        var check = new PartyInstantDeathCheck(ops: [.. Enum.GetValues<ModifierOp>(), (ModifierOp)99]);

        var failure = Assert.Single(check.Run(ContentLoader.Load([])));
        Assert.Equal(Id, failure.Check);
        Assert.Equal("src/Core/Effects/EffectDefinition.cs", failure.File);
        Assert.Contains("99", failure.Field, StringComparison.Ordinal);
    }
}
