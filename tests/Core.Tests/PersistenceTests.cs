using System.Collections.Immutable;
using System.Globalization;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

public class PersistenceTests
{
    private static GameState State() => ShrineState();

    static readonly Command[] Script = {
        new MoveCommand(Hero, Direction.E),                 // onto the shrine
        new AnswerDecisionCommand(new DecisionId(1), 1),
        new MoveCommand(Hero, Direction.E) };

    [Fact]
    public void Snapshot_round_trips_including_pending_decision()
    {
        var s = Engine.Apply(State(), Script[0]).State;     // decision pending
        var loaded = Snapshot.Load(Snapshot.Save(s));
        Assert.Equal(Snapshot.Save(s), Snapshot.Save(loaded));
        Assert.Equal(s.Pending!.Options.AsEnumerable(), loaded.Pending!.Options);
        Assert.Equal(s.Rng, loaded.Rng);
    }

    [Fact]
    public void Loaded_state_continues_identically()
    {
        var mid = Engine.Apply(State(), Script[0]).State;
        var a = Replay.Run(mid, Script.Skip(1));
        var b = Replay.Run(Snapshot.Load(Snapshot.Save(mid)), Script.Skip(1));
        Assert.Equal(Snapshot.Save(a), Snapshot.Save(b));
    }

    [Fact]
    public void Replay_is_deterministic() =>
        Assert.Equal(Snapshot.Save(Replay.Run(State(), Script)), Snapshot.Save(Replay.Run(State(), Script)));

    [Fact]
    public void Command_log_round_trips()
    {
        var log = new CommandLog(7, [.. Script]);
        var loaded = CommandLog.Load(log.Save());
        Assert.Equal(7UL, loaded.Seed); Assert.Equal(Script, loaded.Commands);
    }

    [Fact]
    public void Replay_reports_the_rejected_command()
    {
        var ex = Assert.Throws<ReplayException>(() => Replay.Run(State(), new Command[] {
            new MoveCommand(Hero, Direction.E), new MoveCommand(Hero, Direction.E) }));
        Assert.Equal(1, ex.CommandIndex); Assert.Equal(CommandError.DecisionPending, ex.Error);
    }

    [Theory] [InlineData("")] [InlineData("not json")] [InlineData("{}")] [InlineData("{\"version\":1}")]
    public void Load_rejects_malformed_input(string json) =>
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(json));

    [Fact]
    public void Load_rejects_newer_version()
    {
        var json = Snapshot.Save(State()).Replace("\"version\":1", "\"version\":2");
        Assert.Equal(2, Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(json)).Found);
    }

    [Fact]
    public void Load_rejects_inconsistent_state()   // unit standing in a wall
    {
        var json = Snapshot.Save(State()).Replace("\".*.\"", "\"#*.\"");
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(json));
    }

    // Beyond the brief: the format contract and the corrupt-save edge cases.

    [Fact]
    public void Save_is_single_line_json_with_version_first_and_plain_map_rows()
    {
        var json = Snapshot.Save(State());
        Assert.StartsWith("{\"version\":1,", json);
        Assert.DoesNotContain('\n', json);
        Assert.Contains("\"map\":[\".*.\"]", json);
        Assert.StartsWith("{\"version\":1,\"seed\":7,", new CommandLog(7, [.. Script]).Save());
    }

    [Fact]
    public void Newer_version_is_reported_before_content_is_validated() =>
        Assert.Equal(2, Assert.Throws<SnapshotVersionException>(
            () => Snapshot.Load("{\"version\":2,\"map\":42}")).Found);

    [Fact]
    public void Numbers_do_not_depend_on_the_current_culture()
    {
        var expected = Snapshot.Save(Replay.Run(State(), Script));
        var before = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
            Assert.Equal(expected, Snapshot.Save(Snapshot.Load(expected)));
        }
        finally
        {
            CultureInfo.CurrentCulture = before;
        }
    }

    [Theory]
    [InlineData("null")]
    [InlineData("[]")]
    [InlineData("{\"version\":0}")]
    [InlineData("{\"version\":\"1\"}")]
    [InlineData("{\"version\":1.5}")]
    public void Load_rejects_bad_documents_and_versions(string json) =>
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(json));

    [Theory]
    [InlineData("\"tick\":0", "\"tick\":-1")]                       // negative clock
    [InlineData("\"tick\":0", "\"tick\":\"0\"")]                    // wrong type
    [InlineData("\"tick\":0", "\"tick\":0,\"extra\":1")]            // unknown property
    [InlineData("\"nextDecisionId\":1", "\"nextDecisionId\":0")]
    [InlineData("\"faction\":\"Party\"", "\"faction\":\"Nobody\"")]
    [InlineData("\"faction\":\"Party\"", "\"faction\":\"0\"")]      // numeric enum text
    [InlineData("\"x\":0", "\"x\":7")]                              // unit off the map
    [InlineData("\"boons\":[]", "\"boons\":null")]
    [InlineData("\"map\":[\".*.\"]", "\"map\":[\".*.\",\"..\"]")]   // ragged map
    [InlineData("\"map\":[\".*.\"]", "\"map\":[\".?.\"]")]          // unknown tile
    [InlineData("\"map\":[\".*.\"]", "\"map\":[null]")]
    [InlineData("\"units\":[", "\"units\":[null,")]
    public void Load_rejects_corrupt_content(string find, string replace)
    {
        var good = Snapshot.Save(State());
        Assert.Contains(find, good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(find, replace)));
    }

    [Fact]
    public void Load_rejects_duplicate_units()
    {
        var good = Snapshot.Save(State());
        int start = good.IndexOf("{\"id\":1", StringComparison.Ordinal);
        int end = good.IndexOf('}', start) + 1;
        var unit = good[start..end];
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(unit, unit + "," + unit)));
    }

    [Fact]
    public void Load_rejects_even_rng_increment()
    {
        var s = State();
        var bad = s with { Rng = s.Rng with { Loot = new RngState(5, 4) } };
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(bad)));
    }

    [Fact]
    public void Load_rejects_corrupt_pending_decision()
    {
        var s = Engine.Apply(State(), Script[0]).State;
        var p = s.Pending!;
        var bad = new[]
        {
            s with { Pending = p with { Unit = new UnitId(99) } },      // unknown unit
            s with { Pending = p with { Source = new GridPos(9, 9) } }, // off the map
            s with { Pending = p with { Options = [] } },               // nothing to choose
            s with { Pending = p with { Id = new DecisionId(5) } },     // id not issued yet
        };
        Assert.All(bad, b => Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(b))));

        var json = Snapshot.Save(s);
        Assert.Contains("\"kind\":\"ShrineBoon\"", json);
        Assert.Throws<SnapshotFormatException>(
            () => Snapshot.Load(json.Replace("\"kind\":\"ShrineBoon\"", "\"kind\":\"Riddle\"")));
    }

    [Fact]
    public void Load_rejects_a_pending_decision_the_rules_cannot_produce()
    {
        var start = GameState.Create(GridMap.Parse(".*.", "..#"), new[] { U(1, 0, 0), U(2, 0, 1) }, 7);
        var s = Engine.Apply(start, Script[0]).State;
        var p = s.Pending!;
        Assert.Equal(Snapshot.Save(s), Snapshot.Save(Snapshot.Load(Snapshot.Save(s))));

        var bad = new[]
        {
            s with { Pending = p with { Source = new GridPos(2, 1) } },         // a wall
            s with { Pending = p with { Source = new GridPos(1, 1) } },         // plain floor
            s with { Pending = p with { Unit = new UnitId(2) } },               // unit is not on the source
            s with { Pending = p with { Options = [3, 3, 3] } },                // repeated option
            s with { Pending = p with { Options = [-7, 1, 2] } },               // below the boon range
            s with { Pending = p with { Options = [1, 2, 10] } },               // above the boon range
            s with { Pending = p with { Options = [1] } },                      // too few
            s with { Pending = p with { Options = [1, 2, 3, 4, 5, 6] } },       // too many
            s with { Units = s.Units.SetItem(0, s.Units[0] with { Boons = [10] }) },    // unknown boon on a unit
            s with { Units = s.Units.SetItem(0, s.Units[0] with { Boons = [-1] }) },
        };
        Assert.All(bad, b => Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(Snapshot.Save(b))));
    }

    [Theory]
    [InlineData("\"version\":1", "\"version\":2,\"version\":1")]
    [InlineData("\"tick\":0", "\"tick\":5,\"tick\":0")]
    [InlineData("\"x\":0", "\"x\":2,\"x\":0")]                        // inside a unit
    [InlineData("\"pending\":null,", "")]                               // a field Save always writes
    public void Load_rejects_duplicate_and_missing_properties(string find, string replace)
    {
        var good = Snapshot.Save(State());
        Assert.Contains(find, good);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(good.Replace(find, replace)));
    }

    [Fact]
    public void Command_log_load_rejects_duplicate_properties() =>
        Assert.Throws<SnapshotFormatException>(
            () => CommandLog.Load("{\"version\":1,\"seed\":1,\"seed\":2,\"commands\":[]}"));

    [Theory] [InlineData("2147483648")] [InlineData("3000000000")]
    public void Version_too_large_for_an_int_is_still_a_newer_version(string version)
    {
        var json = Snapshot.Save(State()).Replace("\"version\":1", "\"version\":" + version);
        Assert.Equal(int.MaxValue, Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(json)).Found);
    }

    [Fact]
    public void Command_log_save_names_the_problem_instead_of_crashing()
    {
        Assert.Contains("Commands", Assert.Throws<InvalidOperationException>(
            () => new CommandLog(7, default).Save()).Message);
        Assert.Contains("direction", Assert.Throws<InvalidOperationException>(
            () => new CommandLog(7, [new MoveCommand(Hero, (Direction)99)]).Save()).Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("{}")]
    [InlineData("{\"version\":1}")]
    [InlineData("{\"version\":1,\"seed\":7}")]
    [InlineData("{\"version\":1,\"seed\":-1,\"commands\":[]}")]
    [InlineData("{\"version\":1,\"seed\":7,\"commands\":[null]}")]
    [InlineData("{\"version\":1,\"seed\":7,\"commands\":[{\"type\":\"jump\"}]}")]
    [InlineData("{\"version\":1,\"seed\":7,\"commands\":[{\"type\":\"move\",\"unit\":1}]}")]
    [InlineData("{\"version\":1,\"seed\":7,\"commands\":[{\"type\":\"move\",\"unit\":1,\"dir\":\"Up\"}]}")]
    [InlineData("{\"version\":1,\"seed\":7,\"commands\":[{\"type\":\"move\",\"unit\":1,\"dir\":\"E\",\"option\":0}]}")]
    [InlineData("{\"version\":1,\"seed\":7,\"commands\":[{\"type\":\"answer\",\"decision\":1}]}")]
    public void Command_log_load_rejects_malformed_input(string json) =>
        Assert.Throws<SnapshotFormatException>(() => CommandLog.Load(json));

    [Fact]
    public void Command_log_load_rejects_newer_version() =>
        Assert.Equal(3, Assert.Throws<SnapshotVersionException>(
            () => CommandLog.Load("{\"version\":3,\"seed\":7,\"commands\":[]}")).Found);

    [Fact]
    public void Command_log_keeps_a_full_range_seed()
    {
        var log = new CommandLog(ulong.MaxValue, ImmutableArray<Command>.Empty);
        Assert.Equal(ulong.MaxValue, CommandLog.Load(log.Save()).Seed);
    }
}
