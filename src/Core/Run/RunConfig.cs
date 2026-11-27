using System;
using System.Collections.Immutable;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;

namespace Dungeon.Core;

/// <summary>
/// The run configuration snapshot (RS 11.1), fixed at run start, with the profile grants SV-12 reserves
/// (<see cref="DifficultyTier"/>, <see cref="Boons"/>, <see cref="NightmareTier"/>) and the run-modifier
/// slot (<see cref="RunModifierId"/>, HL-22). Every field is in <see cref="ConfigHash"/> (SV 4.6). The
/// RS 11.1 layout, terrain and cover fields arrive with their owners (M1-28, M1-30); V19t
/// <c>ConfigHashCoversRunConfig</c> forces each new field into the hash.
/// </summary>
/// <param name="RouteFloorCount">Floors in the run: 1 in M1 (D-08), 5 from M2.</param>
/// <param name="RouteTwoDoorsEnabled">RS 11.1; off by default.</param>
/// <param name="RouteLastHollowChoiceEnabled">RS 11.1; off by default, and only with two doors.</param>
/// <param name="RouteDoorCamps">Camps that offer two doors (M2: [1, 2]); a set of camp numbers.</param>
/// <param name="PacingTrimmedFloors">Floors that use the trimmed room counts; a set, empty by default.</param>
/// <param name="RecruitCompanionCap">The companion cap: 3, or 2 under the fallback.</param>
/// <param name="DifficultyTier">The difficulty tier: drives ladder%. Reserved in M1 (0), live in M3.</param>
/// <param name="Boons">The starting boons, a set of content ids. Reserved in M1 (empty), live in M3.</param>
/// <param name="NightmareTier">The Peril tier, 0 (off) to 3: kind additions only. Reserved in M1 (0), live in M3.</param>
/// <param name="RunModifierId">The run-modifier (Oath) slot, a content id or none. Reserved in M1, live Later.</param>
public sealed record RunConfig(
    int RouteFloorCount,
    bool RouteTwoDoorsEnabled,
    bool RouteLastHollowChoiceEnabled,
    ImmutableArray<int> RouteDoorCamps,
    ImmutableArray<int> PacingTrimmedFloors,
    int RecruitCompanionCap,
    int DifficultyTier,
    ImmutableArray<string> Boons,
    int NightmareTier,
    string? RunModifierId) : ICanonical
{
    /// <summary>The highest Peril tier (CH: Peril 1-3).</summary>
    public const int MaxNightmareTier = 3;

    /// <summary>The M1 configuration: RS 11.1 defaults, one floor (D-08), and the grants at their canonical values.</summary>
    public static RunConfig M1 { get; } = new(1, false, false, [], [], 3, 0, [], 0, null);

    /// <summary>The fields <see cref="WriteCanonical"/> writes, in ordinal order (the schema).</summary>
    internal static ImmutableArray<string> WrittenFields { get; } =
    [
        nameof(RouteFloorCount),
        nameof(RouteTwoDoorsEnabled),
        nameof(RouteLastHollowChoiceEnabled),
        nameof(RouteDoorCamps),
        nameof(PacingTrimmedFloors),
        nameof(RecruitCompanionCap),
        nameof(DifficultyTier),
        nameof(Boons),
        nameof(NightmareTier),
        nameof(RunModifierId),
    ];

    /// <summary>SHA-256 over this snapshot's canonical bytes (SV 4.6). Validates the snapshot first.</summary>
    public Digest ConfigHash => Digest.Of(Validate(this));

    /// <summary>Returns <paramref name="c"/> if it is a valid snapshot; otherwise throws naming the field.</summary>
    /// <exception cref="ArgumentException">A field is out of range or malformed; <c>ParamName</c> is the field.</exception>
    public static RunConfig Validate(RunConfig c)
    {
        ArgumentNullException.ThrowIfNull(c);
        if (c.RouteFloorCount < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(RouteFloorCount), c.RouteFloorCount, "A run has at least one floor.");
        }

        if (c.RouteLastHollowChoiceEnabled && !c.RouteTwoDoorsEnabled)
        {
            throw new ArgumentException("The last-hollow choice needs two doors (RS 11.1).", nameof(RouteLastHollowChoiceEnabled));
        }

        RequireNumberSet(c.RouteDoorCamps, c.RouteFloorCount, nameof(RouteDoorCamps));
        RequireNumberSet(c.PacingTrimmedFloors, c.RouteFloorCount, nameof(PacingTrimmedFloors));
        if (c.RecruitCompanionCap < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(RecruitCompanionCap), c.RecruitCompanionCap, "The companion cap is not negative.");
        }

        if (c.DifficultyTier < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(DifficultyTier), c.DifficultyTier, "The difficulty tier is not negative.");
        }

        if (c.NightmareTier < 0 || c.NightmareTier > MaxNightmareTier)
        {
            throw new ArgumentOutOfRangeException(nameof(NightmareTier), c.NightmareTier, "The Peril tier is 0 to 3.");
        }

        RequireIdSet(c.Boons, nameof(Boons));
        if (c.RunModifierId is not null && !ContentId.IsValid(c.RunModifierId))
        {
            throw new ArgumentException($"\"{c.RunModifierId}\" is not a content id.", nameof(RunModifierId));
        }

        return c;
    }

    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(10);
        w.Int32(0, RouteFloorCount);
        w.Bool(1, RouteTwoDoorsEnabled);
        w.Bool(2, RouteLastHollowChoiceEnabled);
        w.SetByKey(3, RouteDoorCamps, n => (ulong)n, (e, n) => e.Int32(0, n));
        w.SetByKey(4, PacingTrimmedFloors, n => (ulong)n, (e, n) => e.Int32(0, n));
        w.Int32(5, RecruitCompanionCap);
        w.Int32(6, DifficultyTier);
        w.SetById(7, Boons, b => b, (e, b) => e.String(0, b));
        w.Int32(8, NightmareTier);
        w.OptionalString(9, RunModifierId);
        w.EndRecord();
    }

    // A set of floor or camp numbers: each in 1..floorCount, no repeats.
    private static void RequireNumberSet(ImmutableArray<int> numbers, int floorCount, string field)
    {
        if (numbers.IsDefault)
        {
            throw new ArgumentException("The set is uninitialised.", field);
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] < 1 || numbers[i] > floorCount)
            {
                throw new ArgumentOutOfRangeException(field, numbers[i], $"Values are 1 to {floorCount}.");
            }

            for (int j = 0; j < i; j++)
            {
                if (numbers[j] == numbers[i])
                {
                    throw new ArgumentException($"{numbers[i]} appears twice.", field);
                }
            }
        }
    }

    // A set of content ids, no repeats.
    internal static void RequireIdSet(ImmutableArray<string> ids, string field)
    {
        if (ids.IsDefault)
        {
            throw new ArgumentException("The set is uninitialised.", field);
        }

        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] is null || !ContentId.IsValid(ids[i]))
            {
                throw new ArgumentException($"\"{ids[i]}\" is not a content id.", field);
            }

            for (int j = 0; j < i; j++)
            {
                if (string.Equals(ids[j], ids[i], StringComparison.Ordinal))
                {
                    throw new ArgumentException($"\"{ids[i]}\" appears twice.", field);
                }
            }
        }
    }
}
