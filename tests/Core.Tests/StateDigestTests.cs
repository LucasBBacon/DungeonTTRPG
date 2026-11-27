using Dungeon.Core.Canonical;
using Dungeon.Core.Effects;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// The replay core-state digest. Checked by properties, not a pinned value, because
/// <see cref="GameState"/> grows in every M1 step (plan call 5).
/// </summary>
public class StateDigestTests
{
    private static GameState TwoUnits() => RingState(U(2, 3, 3));

    [Fact]
    public void Same_state_gives_the_same_digest() =>
        Assert.Equal(StateDigest.Of(TwoUnits()), StateDigest.Of(TwoUnits()));

    [Fact]
    public void Unit_order_does_not_change_the_digest()
    {
        var s = TwoUnits();
        var reversed = s with { Units = [.. s.Units.Reverse()] };
        Assert.Equal(StateDigest.Of(s), StateDigest.Of(reversed));
    }

    [Fact]
    public void A_move_changes_the_digest()
    {
        var s = RingState();
        var moved = Engine.Apply(s, new MoveCommand(Hero, Direction.E)).State;
        Assert.NotEqual(StateDigest.Of(s), StateDigest.Of(moved));
    }

    [Fact]
    public void A_shrine_step_changes_the_digest_and_survives_a_save()
    {
        var s = ShrineState();
        var atShrine = Engine.Apply(s, new MoveCommand(Hero, Direction.E)).State;
        Assert.NotEqual(StateDigest.Of(s), StateDigest.Of(atShrine));
        Assert.Equal(StateDigest.Of(atShrine), StateDigest.Of(Snapshot.Load(Snapshot.Save(atShrine))));
    }

    [Fact]
    public void Run_seed_is_part_of_the_digest() =>
        Assert.NotEqual(StateDigest.Of(ShrineState(7)), StateDigest.Of(ShrineState(8)));

    [Fact]
    public void Faction_is_written_by_name()
    {
        var w = new CanonicalWriter();
        w.BeginRecord(1);
        w.String(0, "Party");
        w.EndRecord();
        var party = Convert.ToHexString(w.ToArray()).ToLowerInvariant();
        var state = Convert.ToHexString(StateDigest.Bytes(ShrineState())).ToLowerInvariant();
        Assert.Contains(party, state);
        Assert.Equal(Digest.Of(StateDigest.Bytes(ShrineState())), StateDigest.Of(ShrineState()));
    }

    // Keys are derived from the seed and not written, so a state whose keys disagree is refused, as Save does.
    [Fact]
    public void Streams_from_another_seed_throw()
    {
        var s = ShrineState(7);
        var forged = s with { Rng = RunStreams.FromSeed(8) };
        Assert.Throws<ArgumentException>(() => StateDigest.Of(forged));
    }

    // GameState.Create, Save and Load accept negative ids, so the digest must too (final review C2).
    [Fact]
    public void Negative_unit_ids_are_digested_in_id_order()
    {
        var s = RingState(U(-5, 3, 3));
        var reversed = s with { Units = [.. s.Units.Reverse()] };
        Assert.Equal(StateDigest.Of(s), StateDigest.Of(reversed));
        Assert.NotEqual(StateDigest.Of(s), StateDigest.Of(RingState(U(5, 3, 3))));
    }

    // Every field must reach the digest: change exactly one and the digest must move (final review C1).
    [Fact]
    public void Every_field_reaches_the_digest()
    {
        var atShrine = Engine.Apply(ShrineState(), new MoveCommand(Hero, Direction.E)).State;
        var pending = atShrine.Pending!;
        var hero = atShrine.Units[0];
        GameState[] changed =
        [
            atShrine with { RunSeed = 8, Rng = RunStreams.FromSeed(8) with { Shrine = RunStreams.FromSeed(8).Shrine.Skip(3) } },
            atShrine with { Map = atShrine.Map.With(new GridPos(2, 0), Tile.Wall) },
            atShrine with { Units = [hero with { Id = new UnitId(2) }] },
            atShrine with { Units = [hero with { Pos = new GridPos(0, 0) }] },
            atShrine with { Units = [hero with { Faction = Faction.Enemy }] },
            atShrine with { Units = [hero with { Boons = [3] }] },
            atShrine with { Rng = atShrine.Rng with { Combat = atShrine.Rng.Combat.Skip(1) } },
            atShrine with { Rng = atShrine.Rng with { Shrine = atShrine.Rng.Shrine.Skip(1) } },
            atShrine with { Pending = null },
            atShrine with { Pending = pending with { Id = new DecisionId(9) } },
            atShrine with { Pending = pending with { Unit = new UnitId(9) } },
            atShrine with { Pending = pending with { Options = [pending.Options[1], pending.Options[0], pending.Options[2]] } },
            atShrine with { Pending = pending with { Source = new GridPos(0, 0) } },
            atShrine with { Tick = atShrine.Tick + 1 },
            atShrine with { NextDecisionId = atShrine.NextDecisionId + 1 },
            atShrine with { Config = RunConfig.M1 with { DifficultyTier = 1 } },
            atShrine with { Unlocks = new UnlockSnapshot(["unlock.a"]) },
            atShrine with { Effects = [new EffectInstance(1, "test.effect", Hero, EffectSource.Environment)] },
            atShrine with { NextEffectSeq = atShrine.NextEffectSeq + 1 },
        ];
        var baseline = StateDigest.Of(atShrine);
        for (int i = 0; i < changed.Length; i++)
        {
            Assert.True(baseline != StateDigest.Of(changed[i]), $"Change {i} did not reach the digest.");
        }
    }
}
