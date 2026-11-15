using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>Helpers shared by more than one test file.</summary>
internal static class Fixtures
{
    public static readonly UnitId Hero = new(1);

    public static Unit U(int id, int x, int y) => new(new UnitId(id), new(x, y), Faction.Party, []);

    public static void AssertRejected(GameState s, Command c, CommandError e)
    {
        var r = Engine.Apply(s, c);
        Assert.Equal(e, r.Error); Assert.Empty(r.Events); Assert.Same(s, r.State);
    }
}
