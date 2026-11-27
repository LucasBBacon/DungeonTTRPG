using Dungeon.Core.Content;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>The M1-07 done-when: an effect written only as JSON changes a number and fires a trigger.</summary>
public class EffectsEndToEndTests
{
    private const string Json = """
        {"effects": [
          {"id": "test.haste", "kind": "trait", "modifiers": [{"target": "speed", "add": 2}]},
          {"id": "test.stride", "kind": "trait", "triggers": [
            {"on": "unitMoved", "scope": "holder", "do": [{"applyEffect": "test.haste", "to": "holder"}]}
          ]}
        ]}
        """;

    [Fact]
    public void A_json_only_effect_changes_a_number_and_fires_a_trigger()
    {
        var rules = RulesContext.Create(
            ContentLoader.Load([new ContentFile("effects/effects.json", Json)], [new EffectsKind()]));
        var s = Effects.Apply(RingState(), "test.stride", Hero, new EffectSource(Hero));
        Assert.Equal(6, Effects.Number(rules, s, Hero, "speed"));

        var r = Engine.Apply(s, new MoveCommand(Hero, Direction.E), rules);
        Assert.True(r.Ok);
        Assert.Equal<GameEvent>(
            [
                new UnitMoved(Hero, new GridPos(1, 1), new GridPos(2, 1)),
                new TickAdvanced(1),
                new EffectApplied(2, "test.haste", Hero, new EffectSource(Hero)),
            ],
            r.Events.ToArray());
        Assert.Equal(8, Effects.Number(rules, r.State, Hero, "speed"));

        // Traits stack, so a second move gives a second haste.
        r = Engine.Apply(r.State, new MoveCommand(Hero, Direction.E), rules);
        Assert.Equal(new EffectApplied(3, "test.haste", Hero, new EffectSource(Hero)), r.Events[^1]);
        Assert.Equal(10, Effects.Number(rules, r.State, Hero, "speed"));
    }
}
