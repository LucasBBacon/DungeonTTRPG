using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>Current HP and the HP basis on every unit (M1-09 task 1): validation, the save and the digest.</summary>
public class HitPointStateTests
{
    private static Unit Foe(int id, int x, int y, int hp = 12) =>
        new(new UnitId(id), new(x, y), Faction.Enemy, [], EnemyStats, hp);

    private static GameState Build(params Unit[] units) => GameState.Create(GridMap.Parse("..."), units, 7);

    private static void AssertRejected(Unit unit, string field)
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(unit));
        Assert.Contains($"unit {unit.Id.Value}", ex.Message);
        Assert.Contains(field, ex.Message);
    }

    [Fact]
    public void Create_rejects_a_party_unit_on_table_hp() =>
        AssertRejected(U(4, 0, 0) with { Stats = Fixtures.Stats with { HpTier = HpTier.Table, TableHp = 12 } }, "HP tier");

    [Fact]
    public void Create_rejects_an_enemy_with_an_hp_tier() =>
        AssertRejected(Foe(5, 0, 0) with { Stats = EnemyStats with { HpTier = HpTier.Sound, TableHp = 0 } }, "HP tier");

    [Fact]
    public void Create_rejects_table_hp_beside_a_tier() =>
        AssertRejected(U(4, 0, 0) with { Stats = Fixtures.Stats with { TableHp = 3 } }, "table HP");

    [Fact]
    public void Create_rejects_table_hp_below_one() =>
        AssertRejected(Foe(5, 0, 0) with { Stats = EnemyStats with { TableHp = 0 } }, "table HP");

    [Fact]
    public void Create_rejects_negative_hp() => AssertRejected(U(4, 0, 0) with { Hp = -1 }, "HP -1");

    [Fact]
    public void Create_rejects_an_unknown_hp_tier() =>
        AssertRejected(U(4, 0, 0) with { Stats = Fixtures.Stats with { HpTier = (HpTier)5 } }, "HP tier");

    [Fact]
    public void Create_accepts_zero_hp()
    {
        var s = Build(U(4, 0, 0) with { Hp = 0 }, Foe(5, 1, 0, hp: 0));
        Assert.Equal([0, 0], s.Units.Select(u => u.Hp).ToArray());
    }

    // Plan call 2: Create does not check HP against the maximum. Level 1, Constitution 0, Slight: 11 + 1 x 6 = 17.
    [Fact]
    public void Create_accepts_hp_above_the_unfolded_maximum()
    {
        var s = Build(U(4, 0, 0) with { Hp = 18 }, Foe(5, 1, 0, hp: 13));
        Assert.Equal([18, 13], s.Units.Select(u => u.Hp).ToArray());
    }

    [Fact]
    public void Hp_and_the_hp_basis_survive_save_and_load()
    {
        var s = Build(
            U(4, 0, 0) with { Stats = Fixtures.Stats with { HpTier = HpTier.Sturdy }, Hp = 9 },
            Foe(5, 2, 0, hp: 5));
        var json = Snapshot.Save(s);
        Assert.StartsWith("{\"version\":6,", json);
        Assert.Contains("\"hp\":9", json);
        Assert.Contains("\"hpTier\":\"Sturdy\"", json);
        Assert.Contains("\"hpTier\":\"Table\",\"tableHp\":12", json);
        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal((HpTier.Sturdy, 0, 9), (loaded.Units[0].Stats.HpTier, loaded.Units[0].Stats.TableHp, loaded.Units[0].Hp));
        Assert.Equal((HpTier.Table, 12, 5), (loaded.Units[1].Stats.HpTier, loaded.Units[1].Stats.TableHp, loaded.Units[1].Hp));
    }

    // Save version 6 (M1-09) adds current HP and the HP basis; version 5 saves are refused, not half loaded.
    [Fact]
    public void A_v5_save_is_refused()
    {
        var v5 = Snapshot.Save(ShrineState()).Replace("\"version\":6", "\"version\":5");
        Assert.Equal(5, Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(v5)).Found);
    }

    [Fact]
    public void A_save_missing_hp_is_a_format_error()
    {
        var good = Snapshot.Save(ShrineState());
        Assert.Contains(",\"hp\":17", good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(",\"hp\":17", "")));
    }

    [Fact]
    public void A_save_with_a_bad_hp_basis_is_a_format_error()
    {
        var good = Snapshot.Save(ShrineState());
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace("\"hp\":17", "\"hp\":-1")));
        Assert.Throws<SnapshotFormatException>(
            () => Snapshot.Load(good.Replace("\"hpTier\":\"Slight\"", "\"hpTier\":\"Table\"")));
        Assert.Throws<SnapshotFormatException>(
            () => Snapshot.Load(good.Replace("\"hpTier\":\"Slight\"", "\"hpTier\":\"Huge\"")));
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(",\"tableHp\":0", "")));
    }

    [Fact]
    public void Fixture_units_start_on_full_hp()
    {
        Assert.Equal(17, FullHp);
        Assert.Equal(FullHp, U(1, 0, 0).Hp);
        Assert.Equal((HpTier.Slight, 0), (Fixtures.Stats.HpTier, Fixtures.Stats.TableHp));
        Assert.Equal((HpTier.Table, 12), (EnemyStats.HpTier, EnemyStats.TableHp));
    }

    [Fact]
    public void HpTier_values_are_the_class_amounts_and_the_death_numbers_match_dh()
    {
        Assert.Equal([0, 6, 7, 8], System.Enum.GetValues<HpTier>().Select(t => (int)t).ToArray());
        Assert.Equal((11, 4, 2), (DeathNumbers.AdventurerBonus, DeathNumbers.RecoveryBase, DeathNumbers.RecoveryPerLevel));
    }
}
