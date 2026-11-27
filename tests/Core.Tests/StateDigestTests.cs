using Dungeon.Core.Canonical;
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

    [Fact]
    public void A_negative_unit_id_throws()
    {
        var s = RingState();
        var bad = s with { Units = [U(-1, 1, 1)] };
        Assert.Throws<ArgumentException>(() => StateDigest.Of(bad));
    }
}
