using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Tests;

/// <summary>
/// The test-only fixture conditions (CO 18.1; M1-12a Task 3): one definition per duration model and intensity kind, never
/// loaded by the game. They are read through the real <c>conditions/conditions.json</c> loader, beside a few plain
/// effects (<see cref="Concentrating"/>, the test sustainer, and <see cref="Plain"/>).
/// </summary>
public static class ConditionFixtures
{
    /// <summary>Hard, <c>turns:1</c>, armour class −2.</summary>
    public const string Stun = "test.cond.stun";

    /// <summary>Minor, <c>turns:3</c>, speed −2.</summary>
    public const string Slow = "test.cond.slow";

    /// <summary>Major, <c>turns:1</c>, denies <c>oneOf</c>, a turn-part major.</summary>
    public const string Daze = "test.cond.daze";

    /// <summary>A token: <c>charges:1 {on: unitMoved, fallback: turns:3}</c>, intensity charges.</summary>
    public const string Token = "test.cond.token";

    /// <summary>Minor, <c>untilSourceTurnStart</c>.</summary>
    public const string MarkStart = "test.cond.mark_start";

    /// <summary>Minor, <c>untilSourceTurnEnd</c>.</summary>
    public const string MarkEnd = "test.cond.mark_end";

    /// <summary>Hard, <c>turns:1</c>, <c>sourceTied</c>.</summary>
    public const string Tied = "test.cond.tied";

    /// <summary>Hard, <c>saveEnds {will, cap 2, graceTurns 1}</c>, <c>combatOnly</c>.</summary>
    public const string Held = "test.cond.held";

    /// <summary>Minor, <c>decay</c>, intensity decay (value cap 3), an acid tick.</summary>
    public const string Venom = "test.cond.venom";

    /// <summary>Major, <c>trigger {event: unitMoved, cap 3}</c>.</summary>
    public const string Trig = "test.cond.trig";

    /// <summary>Major, <c>sustained {max 2}</c>.</summary>
    public const string Sustained = "test.cond.sustained";

    /// <summary>Major, <c>staged {stages 3, fortitude, cap 3, then: test.cond.stun}</c>, <c>combatOnly</c>, intensity staged.</summary>
    public const string Calcify = "test.cond.calcify";

    /// <summary>Minor, <c>turns:2</c>, a fire tick, intensity valued (value cap 5).</summary>
    public const string Burn = "test.cond.burn";

    /// <summary>Major, <c>turns:2</c>, <c>combatStartEnds</c>, <c>noProne</c>, out of combat <c>turns:10</c>.</summary>
    public const string Asleep = "test.cond.asleep";

    /// <summary>Major, <c>[sustained {max 2}, saveEnds {will, cap 2}]</c>, <c>combatOnly</c>.</summary>
    public const string Combined = "test.cond.combined";

    /// <summary>Minor, <c>untilRest</c>, intensity accumulating (level cap 2).</summary>
    public const string Weary = "test.cond.weary";

    /// <summary>Beneficial, <c>untilCamp</c>, intensity valued (value cap 5, no refresh).</summary>
    public const string Blessed = "test.cond.blessed";

    /// <summary>Beneficial, <c>turns:10</c>, <c>combatOnly</c>.</summary>
    public const string Ward = "test.cond.ward";

    /// <summary>Hard, <c>turns:1</c>, tagged <c>engineState</c> (a Surprised stand-in).</summary>
    public const string Surprised = "test.cond.surprised";

    /// <summary>Minor, <c>turns:3</c>; a <c>unitMoved</c> trigger (scope any) gives its holder <see cref="Plain"/>.</summary>
    public const string Watch = "test.cond.watch";

    /// <summary>A plain ability effect that sustains a <see cref="Sustained"/> instance (the test sustainer).</summary>
    public const string Concentrating = "test.concentrating";

    /// <summary>A plain trait with no modifiers.</summary>
    public const string Plain = "test.plain";

    /// <summary>The fixture records, as JSON objects.</summary>
    public static readonly ImmutableArray<string> Records =
    [
        $$$"""{"id": "{{{Stun}}}", "polarity": "harmful", "tier": "hard", "duration": [{"turns": 1}], "modifiers": [{"target": "armourClass", "add": -2}]}""",
        $$$"""{"id": "{{{Slow}}}", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 3}], "modifiers": [{"target": "speed", "add": -2}]}""",
        $$$"""{"id": "{{{Daze}}}", "polarity": "harmful", "tier": "major", "denies": {"oneOf": true}, "flags": {"turnPartGroup": true}, "duration": [{"turns": 1}]}""",
        $$$$"""{"id": "{{{{Token}}}}", "polarity": "token", "intensity": {"kind": "charges"}, "duration": [{"charges": {"count": 1, "on": "unitMoved", "fallback": {"turns": 3}}}]}""",
        $$$"""{"id": "{{{MarkStart}}}", "polarity": "harmful", "tier": "minor", "duration": [{"untilSourceTurnStart": true}]}""",
        $$$"""{"id": "{{{MarkEnd}}}", "polarity": "harmful", "tier": "minor", "duration": [{"untilSourceTurnEnd": true}]}""",
        $$$"""{"id": "{{{Tied}}}", "polarity": "harmful", "tier": "hard", "flags": {"sourceTied": true}, "duration": [{"turns": 1}]}""",
        $$$"""{"id": "{{{Held}}}", "polarity": "harmful", "tier": "hard", "flags": {"combatOnly": true}, "duration": [{"saveEnds": {"save": "will", "cap": 2, "graceTurns": 1}}]}""",
        $$$"""{"id": "{{{Venom}}}", "polarity": "harmful", "tier": "minor", "intensity": {"kind": "decay", "valueCap": 3}, "tick": {"damageType": "acid"}, "duration": [{"decay": true}]}""",
        $$$"""{"id": "{{{Trig}}}", "polarity": "harmful", "tier": "major", "duration": [{"trigger": {"event": "unitMoved", "cap": 3}}]}""",
        $$$"""{"id": "{{{Sustained}}}", "polarity": "harmful", "tier": "major", "duration": [{"sustained": {"max": 2}}]}""",
        $$$"""{"id": "{{{Calcify}}}", "polarity": "harmful", "tier": "major", "flags": {"combatOnly": true}, "intensity": {"kind": "staged", "stages": 3}, "duration": [{"staged": {"stages": 3, "save": "fortitude", "cap": 3, "then": "{{{Stun}}}"}}]}""",
        $$$"""{"id": "{{{Burn}}}", "polarity": "harmful", "tier": "minor", "intensity": {"kind": "valued", "valueCap": 5}, "tick": {"damageType": "fire"}, "cures": ["shakeOff", "damageType:cold"], "duration": [{"turns": 2}]}""",
        $$$"""{"id": "{{{Asleep}}}", "polarity": "harmful", "tier": "major", "flags": {"combatStartEnds": true, "noProne": true}, "duration": [{"turns": 2}], "outOfCombat": [{"turns": 10}]}""",
        $$$"""{"id": "{{{Combined}}}", "polarity": "harmful", "tier": "major", "flags": {"combatOnly": true}, "duration": [{"sustained": {"max": 2}}, {"saveEnds": {"save": "will", "cap": 2}}]}""",
        $$$"""{"id": "{{{Weary}}}", "polarity": "harmful", "tier": "minor", "intensity": {"kind": "accumulating", "levelCap": 2}, "duration": [{"untilRest": true}]}""",
        $$$"""{"id": "{{{Blessed}}}", "polarity": "beneficial", "intensity": {"kind": "valued", "valueCap": 5, "noRefresh": true}, "duration": [{"untilCamp": true}]}""",
        $$$"""{"id": "{{{Ward}}}", "polarity": "beneficial", "flags": {"combatOnly": true}, "duration": [{"turns": 10}]}""",
        $$$"""{"id": "{{{Watch}}}", "polarity": "harmful", "tier": "minor", "duration": [{"turns": 3}], "triggers": [{"on": "unitMoved", "scope": "any", "do": [{"applyEffect": "{{{Plain}}}", "to": "holder"}]}]}""",
        $$$"""{"id": "{{{Surprised}}}", "polarity": "harmful", "tier": "hard", "tags": ["engineState"], "duration": [{"turns": 1}]}""",
    ];

    /// <summary>The plain fixture effects, as JSON objects.</summary>
    public static readonly ImmutableArray<string> EffectRecords =
    [
        $$$"""{"id": "{{{Concentrating}}}", "kind": "ability"}""",
        $$$"""{"id": "{{{Plain}}}", "kind": "trait"}""",
    ];

    /// <summary>The fixture content: <see cref="EffectRecords"/> and <see cref="Records"/>.</summary>
    public static readonly ContentSet Content = Load(EffectRecords, Records);

    /// <summary>The rules for <see cref="Content"/>, with the core registries.</summary>
    public static readonly RulesContext Rules = RulesContext.Create(Content);

    /// <summary>A <c>conditions/conditions.json</c> file holding <paramref name="records"/>.</summary>
    public static ContentFile ConditionsFile(params string[] records) =>
        new("conditions/conditions.json", "{\"conditions\": [" + string.Join(", ", records) + "]}");

    /// <summary>An <c>effects/effects.json</c> file holding <paramref name="records"/>.</summary>
    public static ContentFile EffectsFile(params string[] records) =>
        new("effects/effects.json", "{\"effects\": [" + string.Join(", ", records) + "]}");

    /// <summary>Loads the two files with the effects and conditions kinds.</summary>
    public static ContentSet Load(IEnumerable<string> effects, IEnumerable<string> conditions) =>
        ContentLoader.Load(
            [EffectsFile([.. effects]), ConditionsFile([.. conditions])],
            [new EffectsKind(), new Dungeon.Core.Conditions.ConditionsKind()]);
}
