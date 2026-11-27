using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>V19t Determinism (same key twice) at M1-06: the key fixes the state (SV 4.6).</summary>
public class RunDeterminismTests
{
    private static GameState Play(RunConfig config)
    {
        var s = GameState.Create(GridMap.Parse(".*."), [U(1, 0, 0)], 7, config, UnlockSnapshot.None);
        s = Engine.Apply(s, new MoveCommand(Hero, Direction.E)).State;   // onto the shrine
        return Engine.Apply(s, new AnswerDecisionCommand(s.Pending!.Id, 0)).State;
    }

    [Fact]
    public void The_same_key_twice_gives_byte_identical_state()
    {
        var a = Play(RunConfig.M1);
        var b = Play(RunConfig.M1);
        Assert.Equal(a.Key, b.Key);
        Assert.Equal(StateDigest.Bytes(a), StateDigest.Bytes(b));
        Assert.Equal(Snapshot.Save(a), Snapshot.Save(b));
    }

    [Fact]
    public void A_different_run_modifier_is_a_different_key_and_state()
    {
        var a = Play(RunConfig.M1);
        var b = Play(RunConfig.M1 with { RunModifierId = "oath.a" });
        Assert.NotEqual(a.Key, b.Key);
        Assert.NotEqual(StateDigest.Bytes(a), StateDigest.Bytes(b));
    }

    [Fact]
    public void Create_defaults_to_the_M1_config_and_no_unlocks()
    {
        var s = ShrineState();
        Assert.Same(RunConfig.M1, s.Config);
        Assert.Same(UnlockSnapshot.None, s.Unlocks);
        Assert.Equal(RunKey.For(7, RunConfig.M1, UnlockSnapshot.None), s.Key);
    }

    [Fact]
    public void Create_validates_the_config() =>
        Assert.ThrowsAny<ArgumentException>(
            () => GameState.Create(GridMap.Parse(".*."), [U(1, 0, 0)], 7, RunConfig.M1 with { RouteFloorCount = 0 }));
}
