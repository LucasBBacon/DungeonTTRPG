using System.Collections.Immutable;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>The unit stat block (M1-08 task 1): its ids, its validation and its save format.</summary>
public class UnitStatsTests
{
    private static GameState WithStats(UnitStats stats) =>
        GameState.Create(GridMap.Parse("..."), [U(4, 0, 0) with { Stats = stats }], 7);

    [Fact]
    public void StatIds_cover_every_enum_value_once()
    {
        var abilities = System.Enum.GetValues<Ability>().Select(StatIds.Number).ToArray();
        var saves = System.Enum.GetValues<Save>().Select(StatIds.Number).ToArray();
        var skills = System.Enum.GetValues<Skill>().Select(StatIds.Number).ToArray();
        Assert.Equal(abilities.Length, abilities.Distinct().Count());
        Assert.Equal(saves.Length, saves.Distinct().Count());
        Assert.Equal(skills.Length, skills.Distinct().Count());
        Assert.Equal(["strength", "dexterity", "constitution", "intelligence", "wisdom", "presence"], abilities);
        Assert.Equal(["fortitude", "reflex", "will"], saves);
        Assert.Equal(
            ["athletics", "acrobatics", "stealth", "mechanics", "perception", "intimidation", "leadership", "medicine", "lore"],
            skills);
        Assert.Equal(["fortitudeSave", "reflexSave", "willSave"],
            System.Enum.GetValues<Save>().Select(StatIds.RollTarget).ToArray());
        Assert.Equal(System.Enum.GetValues<Skill>().Select(k => StatIds.Number(k) + "Check"),
            System.Enum.GetValues<Skill>().Select(StatIds.RollTarget));

        Assert.Equal(
            [Ability.Strength, Ability.Dexterity, Ability.Dexterity, Ability.Intelligence, Ability.Wisdom,
                Ability.Presence, Ability.Presence, Ability.Wisdom, Ability.Intelligence],
            System.Enum.GetValues<Skill>().Select(StatIds.AbilityOf).ToArray());
        Assert.Equal((Ability.Strength, Ability.Constitution), StatIds.AbilitiesOf(Save.Fortitude));
        Assert.Equal((Ability.Dexterity, Ability.Intelligence), StatIds.AbilitiesOf(Save.Reflex));
        Assert.Equal((Ability.Wisdom, Ability.Presence), StatIds.AbilitiesOf(Save.Will));
    }

    [Fact]
    public void Abilities_are_read_by_ability()
    {
        var a = new Abilities(1, 2, 3, 4, 5, -1);
        Assert.Equal([1, 2, 3, 4, 5, -1], System.Enum.GetValues<Ability>().Select(x => a[x]).ToArray());
    }

    public static TheoryData<string, UnitStats> InvalidBlocks => new()
    {
        { "level -1", Fixtures.Stats with { Level = -1 } },
        { "level 13", Fixtures.Stats with { Level = 13 } },
        { "ability -4", Fixtures.Stats with { Abilities = Fixtures.Stats.Abilities with { Wisdom = -4 } } },
        { "ability +6", Fixtures.Stats with { Abilities = Fixtures.Stats.Abilities with { Strength = 6 } } },
        { "default skills", Fixtures.Stats with { TrainedSkills = default } },
        { "unsorted skills", Fixtures.Stats with { TrainedSkills = [Skill.Stealth, Skill.Athletics] } },
        { "repeated skill", Fixtures.Stats with { TrainedSkills = [Skill.Lore, Skill.Lore] } },
        { "unknown skill", Fixtures.Stats with { TrainedSkills = [(Skill)99] } },
        { "unknown main ability", Fixtures.Stats with { MainAbility = (Ability)99 } },
        { "unknown save", Fixtures.Stats with { TrainedSave = (Save)99 } },
    };

    [Theory]
    [MemberData(nameof(InvalidBlocks))]
    public void Create_rejects_an_invalid_stat_block(string why, UnitStats stats)
    {
        var ex = Assert.Throws<ArgumentException>(() => WithStats(stats));
        Assert.True(ex.Message.Contains("unit 4"), $"{why}: {ex.Message}");
    }

    [Fact]
    public void Create_rejects_a_missing_stat_block()
    {
        var ex = Assert.Throws<ArgumentException>(() => WithStats(null!));
        Assert.Contains("unit 4", ex.Message);
    }

    [Fact]
    public void Create_accepts_the_stat_block_bounds()
    {
        WithStats(Fixtures.Stats with { Level = 0, Abilities = new Abilities(-3, -3, -3, -3, -3, -3) });
        WithStats(Fixtures.Stats with { Level = 12, Abilities = new Abilities(5, 5, 5, 5, 5, 5) });
    }

    private static GameState Detailed() => WithStats(new UnitStats(
        5, new Abilities(3, 2, 1, 1, 0, -1), Ability.Dexterity, Save.Reflex, [Skill.Acrobatics, Skill.Stealth],
        HpTier.Slight, 0));

    [Fact]
    public void Stats_round_trip_through_a_save()
    {
        var s = Detailed();
        var json = Snapshot.Save(s);
        Assert.Contains(
            "\"stats\":{\"level\":5,\"abilities\":{\"strength\":3,\"dexterity\":2,\"constitution\":1,\"intelligence\":1,"
            + "\"wisdom\":0,\"presence\":-1},\"mainAbility\":\"Dexterity\",\"trainedSave\":\"Reflex\","
            + "\"trainedSkills\":[\"Acrobatics\",\"Stealth\"],\"hpTier\":\"Slight\",\"tableHp\":0}",
            json);
        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        var stats = loaded.Units[0].Stats;
        Assert.Equal(5, stats.Level);
        Assert.Equal(Ability.Dexterity, stats.MainAbility);
        Assert.True(stats.IsTrained(Skill.Stealth));
        Assert.False(stats.IsTrained(Skill.Lore));
    }

    [Theory]
    [InlineData("\"stats\":{\"level\":5,", "\"nostats\":{\"level\":5,")]
    [InlineData("\"mainAbility\":\"Dexterity\"", "\"mainAbility\":1")]
    [InlineData("\"Acrobatics\"", "\"Juggling\"")]
    [InlineData("\"level\":5", "\"level\":13")]
    [InlineData("\"strength\":3", "\"strength\":6")]
    [InlineData("\"Acrobatics\",\"Stealth\"", "\"Stealth\",\"Acrobatics\"")]
    public void Load_rejects_a_bad_stats_block(string from, string to)
    {
        var json = Snapshot.Save(Detailed());
        Assert.Contains(from, json);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(json.Replace(from, to)));
    }
}
