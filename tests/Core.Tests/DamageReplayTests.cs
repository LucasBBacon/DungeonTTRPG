using System.Collections.Immutable;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Damage = Dungeon.Core.Damage.Damage;

/// <summary>
/// A scripted attack-and-damage sequence on <c>combat</c> replays identically and survives a save and load (M1-09 Task 4).
/// </summary>
public class DamageReplayTests
{
    private static readonly UnitId Foe = ContestKit.Foe;
    private static readonly RulesContext Rules = ContestKit.Rules;

    private static readonly ImmutableArray<DamagePart> Longsword = [new DamagePart(DamageType.Slashing, 1, 8, 2)];

    // The M1-08 replay units (the hero level 3 with Strength +3, the foe level 2), without the M1-08 roll-mode effects:
    // with them the foe never hits on seed 7, and this sequence should land damage on both sides.
    private static GameState Start() =>
        ContestKit.StateOf(
            ContestKit.Block(ContestKit.Zero.With(Ability.Strength, 3).With(Ability.Intelligence, 2).With(Ability.Wisdom, 1), 3, Save.Will, Skill.Athletics),
            ContestKit.Block(ContestKit.Zero.With(Ability.Dexterity, 1).With(Ability.Intelligence, 1), 2, Save.Reflex),
            7);

    // One round: the hero attacks the foe, then the foe attacks the hero; a hit deals 1d8 + 2 slashing, a crit doubling
    // the die.
    private static GameState Round(GameState s)
    {
        s = Strike(s, Hero, Foe);
        return Strike(s, Foe, Hero);
    }

    private static GameState Strike(GameState s, UnitId attacker, UnitId defender)
    {
        var (attack, next) = Contests.Attack(Rules, s, attacker, defender, new AttackSpec(Ability.Strength, true));
        return attack.Hit ? Damage.Deal(Rules, next, defender, Longsword, attack.Crit).Next : next;
    }

    private static GameState Rounds(GameState s, int count)
    {
        for (int i = 0; i < count; i++)
        {
            s = Round(s);
        }

        return s;
    }

    // Pinned from the first green run on seed 7 (a golden, not a hand-derived value): the hero's and the foe's HP after
    // three rounds. A change here means the draw order or the damage arithmetic moved. (HP reaching 0 has no
    // consequence until M1-10, so a unit at 0 keeps attacking here.)
    private static readonly (int Hero, int Foe) Seed7FinalHp = (0, 10);

    [Fact]
    public void A_scripted_attack_and_damage_sequence_replays_identically()
    {
        var a = Rounds(Start(), 3);
        var b = Rounds(Start(), 3);
        Assert.Equal(Snapshot.Save(a), Snapshot.Save(b));
        Assert.Equal(StateDigest.Bytes(a), StateDigest.Bytes(b));

        var mid = Rounds(Start(), 1);
        var split = Rounds(Snapshot.Load(Snapshot.Save(mid)), 2);
        Assert.Equal(Snapshot.Save(a), Snapshot.Save(split));

        Assert.Equal(Start().Rng.Shrine, a.Rng.Shrine);
        Assert.Equal(Seed7FinalHp, (a.FindUnit(Hero)!.Hp, a.FindUnit(Foe)!.Hp));
    }
}
