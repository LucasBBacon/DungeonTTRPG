using System.Collections.Immutable;
using System.Globalization;
using Xunit;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

public class PersistenceTests
{
    private static GameState State() => ShrineState();

    /// <summary><c>Snapshot.Save(ShrineState())</c> as M0 (save version 1, PCG32 streams) wrote it.</summary>
    private const string M0SnapshotV1 =
        "{\"version\":1,\"map\":[\".*.\"],\"units\":[{\"id\":1,\"x\":0,\"y\":0,\"faction\":\"Party\",\"boons\":[]}],"
        + "\"rng\":{\"generation\":{\"state\":8301130017339275205,\"inc\":3},\"combat\":{\"state\":2582658391323309601,\"inc\":5},"
        + "\"loot\":{\"state\":15310930839016895613,\"inc\":7}},\"pending\":null,\"tick\":0,\"nextDecisionId\":1}";

    /// <summary>The command log of <see cref="Script"/> under seed 7 as M0 (save version 1) wrote it.</summary>
    private const string M0LogV1 =
        "{\"version\":1,\"seed\":7,\"commands\":[{\"type\":\"move\",\"unit\":1,\"dir\":\"E\"},"
        + "{\"type\":\"answer\",\"decision\":1,\"option\":1},{\"type\":\"move\",\"unit\":1,\"dir\":\"E\"}]}";

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

    [Theory] [InlineData("")] [InlineData("not json")] [InlineData("{}")] [InlineData("{\"version\":2}")]
    public void Load_rejects_malformed_input(string json) =>
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(json));

    [Fact]
    public void Load_rejects_newer_version()
    {
        var json = Snapshot.Save(State()).Replace("\"version\":2", "\"version\":3");
        var ex = Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(json));
        Assert.Equal(3, ex.Found);
        Assert.Contains("this build reads up to version 2", ex.Message);
    }

    /// <summary>Review Focus 3: real M0 saves are refused as an old version, not half loaded.</summary>
    [Fact]
    public void M0_snapshot_and_log_are_refused_as_version_1()
    {
        var snapshot = Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(M0SnapshotV1));
        Assert.Equal(1, snapshot.Found);
        Assert.Contains("this build no longer reads it (it reads version 2)", snapshot.Message);
        Assert.Equal(1, Assert.Throws<SnapshotVersionException>(() => CommandLog.Load(M0LogV1)).Found);
    }

    [Theory] [InlineData("{\"version\":1}", 1)] [InlineData("{\"version\":0}", 0)]
    public void Older_versions_are_a_version_error(string json, int found) =>
        Assert.Equal(found, Assert.Throws<SnapshotVersionException>(() => Snapshot.Load(json)).Found);

    /// <summary>Review Focus 3: a run seed of 2^60 or more is not a run seed.</summary>
    [Fact]
    public void Load_rejects_an_out_of_range_run_seed()
    {
        var good = Snapshot.Save(State());
        Assert.Contains("\"runSeed\":7,", good);
        Assert.Throws<SnapshotFormatException>(
            () => Snapshot.Load(good.Replace("\"runSeed\":7,", "\"runSeed\":1152921504606846976,")));
        Assert.Throws<SnapshotFormatException>(
            () => CommandLog.Load("{\"version\":2,\"seed\":1152921504606846976,\"commands\":[]}"));
    }

    /// <summary>Saves hold the run seed and the counters; keys are re-derived on load (plan call 3).</summary>
    [Fact]
    public void Stream_counters_survive_a_save_after_a_shrine_step()
    {
        var s = Engine.Apply(State(), Script[0]).State;
        var json = Snapshot.Save(s);
        Assert.Contains("\"runSeed\":7,", json);
        Assert.Contains("\"rng\":{\"combat\":0,\"shrine\":3}", json);
        var loaded = Snapshot.Load(json);
        Assert.Equal(json, Snapshot.Save(loaded));
        Assert.Equal(s.Rng, loaded.Rng);
        Assert.Equal(7UL, loaded.RunSeed);
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
        Assert.StartsWith("{\"version\":2,", json);
        Assert.DoesNotContain('\n', json);
        Assert.Contains("\"map\":[\".*.\"]", json);
        Assert.StartsWith("{\"version\":2,\"seed\":7,", new CommandLog(7, [.. Script]).Save());
    }

    [Fact]
    public void Newer_version_is_reported_before_content_is_validated() =>
        Assert.Equal(3, Assert.Throws<SnapshotVersionException>(
            () => Snapshot.Load("{\"version\":3,\"map\":42}")).Found);

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
    [InlineData("{\"version\":\"2\"}")]
    [InlineData("{\"version\":1.5}")]
    [InlineData("{\"map\":[]}")]
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
    [InlineData("\"combat\":0", "\"combat\":-1")]                  // negative counter
    [InlineData("\"shrine\":0", "\"shrine\":\"0\"")]
    [InlineData("\"combat\":0,", "")]                               // a stream missing
    [InlineData("\"runSeed\":7,", "")]
    [InlineData("\"runSeed\":7", "\"runSeed\":-7")]
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
    [InlineData("\"version\":2", "\"version\":3,\"version\":2")]
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
            () => CommandLog.Load("{\"version\":2,\"seed\":1,\"seed\":2,\"commands\":[]}"));

    [Theory] [InlineData("2147483648")] [InlineData("3000000000")]
    public void Version_too_large_for_an_int_is_still_a_newer_version(string version)
    {
        var json = Snapshot.Save(State()).Replace("\"version\":2", "\"version\":" + version);
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
    [InlineData("{\"version\":2}")]
    [InlineData("{\"version\":2,\"seed\":7}")]
    [InlineData("{\"version\":2,\"seed\":-1,\"commands\":[]}")]
    [InlineData("{\"version\":2,\"seed\":7,\"commands\":[null]}")]
    [InlineData("{\"version\":2,\"seed\":7,\"commands\":[{\"type\":\"jump\"}]}")]
    [InlineData("{\"version\":2,\"seed\":7,\"commands\":[{\"type\":\"move\",\"unit\":1}]}")]
    [InlineData("{\"version\":2,\"seed\":7,\"commands\":[{\"type\":\"move\",\"unit\":1,\"dir\":\"Up\"}]}")]
    [InlineData("{\"version\":2,\"seed\":7,\"commands\":[{\"type\":\"move\",\"unit\":1,\"dir\":\"E\",\"option\":0}]}")]
    [InlineData("{\"version\":2,\"seed\":7,\"commands\":[{\"type\":\"answer\",\"decision\":1}]}")]
    public void Command_log_load_rejects_malformed_input(string json) =>
        Assert.Throws<SnapshotFormatException>(() => CommandLog.Load(json));

    [Fact]
    public void Command_log_load_rejects_newer_version() =>
        Assert.Equal(3, Assert.Throws<SnapshotVersionException>(
            () => CommandLog.Load("{\"version\":3,\"seed\":7,\"commands\":[]}")).Found);

    /// <summary>A stream at counter 2^64 - 1 can never draw again, so such a save is refused at load (A1).</summary>
    [Fact]
    public void Load_rejects_an_exhausted_stream_counter()
    {
        var json = Snapshot.Save(Engine.Apply(State(), Script[0]).State);
        Assert.Contains("\"rng\":{\"combat\":0,\"shrine\":3}", json);
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(
            json.Replace("\"combat\":0,", "\"combat\":18446744073709551615,")));
        Assert.Throws<SnapshotFormatException>(() => Snapshot.Load(
            json.Replace("\"shrine\":3}", "\"shrine\":18446744073709551615}")));
        var last = Snapshot.Load(json.Replace("\"shrine\":3}", "\"shrine\":18446744073709551614}"));
        Assert.Equal(18446744073709551614UL, last.Rng.Shrine.Counter);
    }

    /// <summary>Saves store the seed, not keys (plan call 3), so streams that disagree with the seed cannot be saved (B1).</summary>
    [Fact]
    public void Snapshot_save_refuses_streams_that_do_not_belong_to_the_run_seed()
    {
        var s = State();
        Assert.Contains("stream", Assert.Throws<InvalidOperationException>(
            () => Snapshot.Save(s with { RunSeed = 8 })).Message);
        Assert.Contains("stream", Assert.Throws<InvalidOperationException>(
            () => Snapshot.Save(s with { Rng = RunStreams.FromSeed(8) })).Message);
        Assert.Contains("2^60", Assert.Throws<InvalidOperationException>(
            () => Snapshot.Save(s with { RunSeed = RunSeed.Limit })).Message);
    }

    /// <summary>A log whose seed its own Load would refuse cannot be written (B2).</summary>
    [Fact]
    public void Command_log_save_refuses_an_out_of_range_seed()
        => Assert.Contains("2^60", Assert.Throws<InvalidOperationException>(
            () => new CommandLog(RunSeed.Limit, ImmutableArray<Command>.Empty).Save()).Message);

    [Fact]
    public void Command_log_keeps_the_largest_run_seed()
    {
        var log = new CommandLog(RunSeed.Limit - 1, ImmutableArray<Command>.Empty);
        Assert.Equal(RunSeed.Limit - 1, CommandLog.Load(log.Save()).Seed);
    }
}
