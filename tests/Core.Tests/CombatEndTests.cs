using System.Collections.Immutable;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

// The class shares its name with its namespace, so name it explicitly inside this namespace.
using Damage = Dungeon.Core.Damage.Damage;

/// <summary>
/// Combat end in the DH 20.5 order (M1-10 Task 5; plan calls 11, 12): <see cref="CombatEnd.EndIfWon"/> once no enemy
/// remains, the no-op cases, the floors returning after a fight, and Review Focus 1's last enemy dying on its own turn.
/// </summary>
public class CombatEndTests
{
    private static readonly UnitId Foe = new(2);
    private static readonly RulesContext Rules = ContestKit.Rules;

    private static readonly DamageRoll Lethal = Fixed(30);

    // The hero (1, party, HP 17) at (1,1) and a foe (2, enemy, HP 12) at (3,1), out of combat, seed 7.
    private static GameState Out() =>
        GameState.Create(
            GridMap.Parse(".....", ".....", "....."),
            [
                new Unit(Hero, new GridPos(1, 1), Faction.Party, [], Fixtures.Stats, FullHp),
                new Unit(Foe, new GridPos(3, 1), Faction.Enemy, [], EnemyStats, 12),
            ],
            7,
            hero: Hero);

    // As Out, in combat with order [Hero, Foe], the given slot current, round 2.
    private static GameState In(int current) => Out() with { Combat = new CombatState([Hero, Foe], current, 2) };

    [Fact]
    public void Combat_ends_when_no_enemy_remains()
    {
        var (killed, s) = Harm.Deal(Rules, In(0), Foe, Lethal);
        Assert.Equal<GameEvent>([new DamageDealt(1, Foe, 30, 0), new UnitDied(Foe, new GridPos(3, 1))], killed.Events.ToArray());

        // Harm.Deal never ends combat: combatActive stays true until the combat-end procedure has run (DH 10.3).
        Assert.True(s.CombatActive);

        var (events, next) = CombatEnd.EndIfWon(Rules, s);

        Assert.Equal<GameEvent>([new TurnEnded(Hero), new CombatEnded(CombatEndReason.Win)], events.ToArray());
        Assert.Null(next.Combat);
        Assert.False(next.CombatActive);
        Assert.Null(next.Ended);
        Assert.Equal(FullHp, next.FindUnit(Hero)!.Hp);
    }

    [Fact]
    public void Combat_does_not_end_while_an_enemy_remains()
    {
        var (_, s) = Harm.Deal(Rules, In(0), Foe, Fixed(5));

        var (events, next) = CombatEnd.EndIfWon(Rules, s);

        Assert.Empty(events);
        Assert.Same(s, next);
    }

    [Fact]
    public void Combat_does_not_end_out_of_combat()
    {
        // No enemy at all, but no fight either.
        var s = GameState.Create(GridMap.Parse("....."), [U(1, 0, 0)], 7, hero: Hero);

        var (events, next) = CombatEnd.EndIfWon(Rules, s);

        Assert.Empty(events);
        Assert.Same(s, next);
    }

    [Fact]
    public void Combat_does_not_end_on_an_ended_run()
    {
        // The last enemy is gone, but the hero's death ended the run: none of DH 20.5 runs.
        var (_, afterFoe) = Harm.Deal(Rules, In(0), Foe, Lethal);
        var (_, s) = Harm.Deal(Rules, afterFoe, Hero, Lethal);
        Assert.NotNull(s.Ended);

        var (events, next) = CombatEnd.EndIfWon(Rules, s);

        Assert.Empty(events);
        Assert.Same(s, next);
        Assert.True(next.CombatActive);
    }

    [Fact]
    public void After_combat_ends_party_damage_is_floored_again()
    {
        var (_, s) = Harm.Deal(Rules, In(0), Foe, Lethal);

        // In combat the hit would be lethal.
        var inCombat = Damage.Resolve(Rules, s, Hero, Lethal);
        Assert.Equal((0, (ClampReason?)null), (inCombat.HpAfter, inCombat.Clamp));

        var (_, ended) = CombatEnd.EndIfWon(Rules, s);
        var (result, next) = Harm.Deal(Rules, ended, Hero, Lethal);

        Assert.Equal((1, (ClampReason?)ClampReason.OutOfCombat), (result.Outcome.HpAfter, result.Outcome.Clamp));
        Assert.Equal<GameEvent>(
            [new DamageDealt(2, Hero, 30, 1), new DamageClamped(2, Hero, ClampReason.OutOfCombat)], result.Events.ToArray());
        Assert.Equal(1, next.FindUnit(Hero)!.Hp);
        Assert.Null(next.Ended);
    }

    // Review Focus 1: the last enemy dies on its own turn. Its turn is vacated with no TurnEnded; combat end skips step
    // 1, and no party turn starts afterwards.
    [Fact]
    public void The_last_enemy_dying_on_its_own_turn_starts_no_party_turn()
    {
        var (killed, s) = Harm.Deal(Rules, In(1), Foe, Lethal);

        Assert.Equal<GameEvent>([new DamageDealt(1, Foe, 30, 0), new UnitDied(Foe, new GridPos(3, 1))], killed.Events.ToArray());
        Assert.True(s.Combat!.TurnVacated);

        var (events, ended) = CombatEnd.EndIfWon(Rules, s);
        Assert.Equal<GameEvent>([new CombatEnded(CombatEndReason.Win)], events.ToArray());
        Assert.Null(ended.Combat);

        var (resumed, after) = Turns.ResumeIfVacated(Rules, ended);
        Assert.Empty(resumed);
        Assert.Same(ended, after);
    }

    // --- Helpers ---

    // One slashing part with no dice and a fixed raw amount.
    private static DamageRoll Fixed(int raw) =>
        new([new PartRoll(new DamagePart(DamageType.Slashing, 0, 0, raw), [], raw)], false);
}
