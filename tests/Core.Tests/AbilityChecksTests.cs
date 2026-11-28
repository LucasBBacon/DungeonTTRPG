using Dungeon.Core.AbilityModel;
using Dungeon.Core.Content;
using Dungeon.Core.Validation;
using Dungeon.Core.Validation.Checks;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>The two AB 14.5 checks over ability records: <c>content.ABL145.ReactionHasPolicy</c> and <c>content.ABL145.AreaCapByLevel</c>.</summary>
public class AbilityChecksTests
{
    private const string AbilitiesFile = "abilities/abilities.json";

    // --- ReactionHasPolicy ---

    [Fact]
    public void The_reaction_check_is_registered_and_reads_abilities()
    {
        var check = new ReactionHasPolicyCheck();
        Assert.Equal(new CheckId("content.ABL145.ReactionHasPolicy"), check.Id);
        Assert.Equal<string>([AbilitiesFile], check.Reads.ToArray());
        Assert.Contains(StaticChecks.All, c => c.Id == check.Id);
        Assert.Empty(check.Run(Load()));
    }

    [Theory]
    [InlineData("")]
    [InlineData(""", "policy": {"kind": "askMe"}""")]
    public void A_reaction_without_an_automatic_policy_fails(string policy)
    {
        var failure = Assert.Single(new ReactionHasPolicyCheck().Run(Load(Reaction("test.r", 2, policy))));

        Assert.Equal(new CheckId("content.ABL145.ReactionHasPolicy"), failure.Check);
        Assert.Equal(AbilitiesFile, failure.File);
        Assert.Equal("$.abilities[0].reaction.policy", failure.Field);
    }

    [Fact]
    public void Never_passes()
    {
        Assert.Empty(new ReactionHasPolicyCheck().Run(Load(Reaction("test.r", 2, """, "policy": {"kind": "never"}"""))));
    }

    [Fact]
    public void Always_passes()
    {
        Assert.Empty(new ReactionHasPolicyCheck().Run(Load(Reaction("test.r", 2, """, "policy": {"kind": "always"}"""))));
    }

    [Fact]
    public void While_pool_at_least_half_needs_a_pool()
    {
        const string Policy = """, "policy": {"kind": "whilePoolAtLeastHalf"}""";
        var failure = Assert.Single(new ReactionHasPolicyCheck().Run(Load(Reaction("test.r", 1, Policy))));
        Assert.Equal("$.abilities[0].pool", failure.Field);

        Assert.Empty(new ReactionHasPolicyCheck().Run(Load(Reaction("test.r", 1, Policy, """, "pool": {"id": "pool.a", "count": 1}"""))));
    }

    [Theory]
    [InlineData("ifItChangesOutcome", 0)]
    [InlineData("ifItChangesOutcome", -1)]
    [InlineData("always", 0)]
    [InlineData("always", -1)]
    [InlineData("never", 0)]
    [InlineData("never", -1)]
    public void A_reaction_needs_a_bonus_of_at_least_one(string kind, int bonus)
    {
        // M1-11 reactions are bonus wards (plan call 11): a bonus below 1 never helps, and under "always" it hurts.
        string policy = $$""", "policy": {"kind": "{{kind}}"}""";
        var failure = Assert.Single(new ReactionHasPolicyCheck().Run(Load(Reaction("test.r", bonus, policy))));
        Assert.Equal("$.abilities[0].reaction.bonus", failure.Field);

        Assert.Empty(new ReactionHasPolicyCheck().Run(Load(Reaction("test.r", 1, policy))));
    }

    [Fact]
    public void The_reaction_check_names_each_failing_record_by_index_and_skips_other_abilities()
    {
        var content = Load(
            Strike("test.s", """{"shape": "single", "range": 1}"""),
            Reaction("test.ok", 1, """, "policy": {"kind": "always"}"""),
            Reaction("test.bad", 1, ""));

        var failure = Assert.Single(new ReactionHasPolicyCheck().Run(content));
        Assert.Equal("$.abilities[2].reaction.policy", failure.Field);
    }

    // --- AreaCapByLevel ---

    [Fact]
    public void The_area_check_is_registered_and_reads_abilities()
    {
        var check = new AreaCapByLevelCheck();
        Assert.Equal(new CheckId("content.ABL145.AreaCapByLevel"), check.Id);
        Assert.Equal<string>([AbilitiesFile], check.Reads.ToArray());
        Assert.Contains(StaticChecks.All, c => c.Id == check.Id);
        Assert.Empty(check.Run(Load()));
    }

    [Theory]
    [InlineData("""{"shape": "burst", "radius": 3}""")]
    [InlineData("""{"shape": "cone", "length": 5}""")]
    [InlineData("""{"shape": "line", "length": 26}""")]
    public void An_area_over_25_squares_fails(string target)
    {
        var failure = Assert.Single(new AreaCapByLevelCheck().Run(Load(Strike("test.a", target))));

        Assert.Equal(new CheckId("content.ABL145.AreaCapByLevel"), failure.Check);
        Assert.Equal(AbilitiesFile, failure.File);
        Assert.Equal("$.abilities[0].target", failure.Field);
        Assert.Contains("25", failure.Problem, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("""{"shape": "burst", "radius": 2}""")]
    [InlineData("""{"shape": "cone", "length": 4}""")]
    [InlineData("""{"shape": "line", "length": 25}""")]
    [InlineData("""{"shape": "single", "range": 99, "count": 40}""")]
    public void An_area_of_25_squares_or_fewer_passes(string target)
    {
        Assert.Empty(new AreaCapByLevelCheck().Run(Load(Strike("test.a", target))));
    }

    [Fact]
    public void A_reaction_has_no_area_to_check()
    {
        Assert.Empty(new AreaCapByLevelCheck().Run(Load(Reaction("test.r", 1, ""))));
    }

    [Fact]
    public void AreaCap_is_25_to_level_6_and_49_from_7()
    {
        for (int level = 1; level <= 6; level++)
        {
            Assert.Equal(25, AreaCapByLevelCheck.AreaCap(level));
        }

        for (int level = 7; level <= 10; level++)
        {
            Assert.Equal(49, AreaCapByLevelCheck.AreaCap(level));
        }

        Assert.Throws<ArgumentOutOfRangeException>(() => AreaCapByLevelCheck.AreaCap(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => AreaCapByLevelCheck.AreaCap(11));
    }

    // --- Helpers ---

    private static string Reaction(string id, int bonus, string policy, string extra = "") =>
        $$"""{"id": "{{id}}", "kind": "reaction", "cost": "reaction", "reaction": {"trigger": "attackRoll", "bonus": {{bonus}}{{policy}}}{{extra}}}""";

    private static string Strike(string id, string target) =>
        $$"""{"id": "{{id}}", "kind": "atWill", "cost": "action", "target": {{target}}}""";

    private static ContentSet Load(params string[] records) =>
        ContentLoader.Load(
            [new ContentFile(AbilitiesFile, "{\"abilities\": [" + string.Join(", ", records) + "]}")],
            [new AbilitiesKind()]);
}
