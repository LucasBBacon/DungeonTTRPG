using System;
using System.Collections.Immutable;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Combat;

/// <summary>
/// When a pool's spent count resets to zero (AB 6.2: "Every pool has exactly one refill tag"): <see cref="Turn"/> at
/// the unit's turn start (and each tick out of combat, AB 6.8), <see cref="Fight"/> at combat start, <see cref="Rest"/>
/// at a completed short rest and at camp, <see cref="Camp"/> at camp only, and <see cref="Never"/> never.
/// </summary>
public enum RefillTag { Turn, Fight, Rest, Camp, Never }

/// <summary>
/// A pool (AB 6.2; M1-10 plan call 7): its content id, its refill tag and its maximum, derived on demand for a unit
/// (<see cref="NumberBase"/>: the state, the unit and the unit's folded derived numbers). The pool table is a code
/// table on <see cref="RulesContext"/> until the pool content records arrive (M1-19).
/// </summary>
public sealed record PoolDefinition(string Id, RefillTag Refill, NumberBase Max);

/// <summary>
/// Pool queries and changes (AB 6.2, 6.4). The maximum is derived on demand; a unit stores only its spent counts
/// (<see cref="Unit.Spent"/>), sorted by pool id in ordinal order, each count at least 1: a count refilled to zero is
/// dropped. Current = maximum − spent, never below zero. A level-up never refills anything.
/// </summary>
public static class Pools
{
    /// <summary>
    /// <paramref name="unit"/>'s maximum of <paramref name="poolId"/>, derived from the pool's
    /// <see cref="PoolDefinition.Max"/>. Throws <see cref="ArgumentException"/> for an unknown pool or unit.
    /// </summary>
    public static long Max(RulesContext rules, GameState s, UnitId unit, string poolId)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var pool = rules.Pool(poolId);
        return MaxOf(rules, s, Find(s, unit), pool);
    }

    /// <summary>
    /// How much of <paramref name="poolId"/> <paramref name="unit"/> has spent: its stored count, or 0. Throws
    /// <see cref="ArgumentException"/> for an unknown unit.
    /// </summary>
    public static int Spent(GameState s, UnitId unit, string poolId)
    {
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(poolId);
        return SpentOf(Find(s, unit), poolId);
    }

    /// <summary>
    /// <paramref name="unit"/>'s current <paramref name="poolId"/>: max(0, maximum − spent) (AB 6.2: a lowered maximum
    /// never pushes a pool negative). Throws <see cref="ArgumentException"/> for an unknown pool or unit.
    /// </summary>
    public static long Current(RulesContext rules, GameState s, UnitId unit, string poolId)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var pool = rules.Pool(poolId);
        var holder = Find(s, unit);
        return CurrentOf(rules, s, holder, pool);
    }

    /// <summary>
    /// <paramref name="unit"/> spends <paramref name="count"/> of <paramref name="poolId"/>. Refused, with
    /// <paramref name="s"/> itself as the state, when the current count is below <paramref name="count"/>, or when the
    /// pool is a <see cref="RefillTag.Camp"/> pool and the unit's <see cref="TurnBudget.CampSpent"/> is set (AB 6.4: "at
    /// most once per turn", on any unit's turn). A camp spend sets the flag. Throws
    /// <see cref="ArgumentOutOfRangeException"/> for a count below 1 and <see cref="ArgumentException"/> for an unknown
    /// pool or unit.
    /// </summary>
    public static (bool Ok, GameState Next) Spend(RulesContext rules, GameState s, UnitId unit, string poolId, int count)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        if (count < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "A pool spend is at least 1.");
        }

        var pool = rules.Pool(poolId);
        var holder = Find(s, unit);
        bool camp = pool.Refill == RefillTag.Camp;
        if ((camp && holder.Budget.CampSpent) || CurrentOf(rules, s, holder, pool) < count)
        {
            return (false, s);
        }

        var spent = WithSpent(holder.Spent, pool.Id, checked(SpentOf(holder, pool.Id) + count));
        var next = holder with
        {
            Spent = spent,
            Budget = camp ? holder.Budget with { CampSpent = true } : holder.Budget,
        };
        return (true, s with { Units = s.Units.SetItem(s.Units.IndexOf(holder), next) });
    }

    /// <summary>
    /// Zeroes <paramref name="unit"/>'s spent counts of the pools refilled by <paramref name="tag"/>: the pools with that
    /// tag, and for <see cref="RefillTag.Camp"/> the <see cref="RefillTag.Rest"/> pools too (AB 6.2: <c>rest</c> refills at
    /// "a completed short rest, and camp"). Spent counts of pools the rules do not define are left alone. It does not
    /// touch the turn budget. Throws <see cref="ArgumentException"/> for <see cref="RefillTag.Never"/> (nothing refills
    /// it) or an unknown unit, and <see cref="ArgumentOutOfRangeException"/> for an undefined tag.
    /// </summary>
    public static GameState Refill(RulesContext rules, GameState s, UnitId unit, RefillTag tag)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        CheckRefillable(tag);
        var holder = Find(s, unit);
        var refilled = Refilled(rules, holder, tag);
        return ReferenceEquals(refilled, holder) ? s : s with { Units = s.Units.SetItem(s.Units.IndexOf(holder), refilled) };
    }

    /// <summary>
    /// <see cref="Refill"/>, then <paramref name="kept"/>'s counts written back (M1-11 plan call 6: an opener's charge to
    /// its striker's first turn survives that turn's refill): each kept pool's spent count becomes at least its kept
    /// count. Throws as <see cref="Refill"/> does.
    /// </summary>
    internal static GameState RefillKeeping(
        RulesContext rules, GameState s, UnitId unit, RefillTag tag, ImmutableArray<PoolSpend> kept)
    {
        var next = Refill(rules, s, unit, tag);
        var refilled = Find(next, unit);
        var holder = refilled;
        foreach (var keep in kept)
        {
            if (keep.Spent > SpentOf(holder, keep.PoolId))
            {
                holder = holder with { Spent = WithSpent(holder.Spent, keep.PoolId, keep.Spent) };
            }
        }

        return ReferenceEquals(holder, refilled)
            ? next
            : next with { Units = next.Units.SetItem(next.Units.IndexOf(refilled), holder) };
    }

    /// <summary>
    /// <see cref="Refill"/> for every unit in the state, in id order. Throws as <see cref="Refill"/> does for the tag.
    /// </summary>
    public static GameState RefillAll(RulesContext rules, GameState s, RefillTag tag)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        CheckRefillable(tag);
        var units = s.Units.ToBuilder();
        bool changed = false;
        for (int i = 0; i < units.Count; i++)
        {
            var refilled = Refilled(rules, units[i], tag);
            if (!ReferenceEquals(refilled, units[i]))
            {
                units[i] = refilled;
                changed = true;
            }
        }

        return changed ? s with { Units = units.ToImmutable() } : s;
    }

    private static void CheckRefillable(RefillTag tag)
    {
        if (!Enum.IsDefined(tag))
        {
            throw new ArgumentOutOfRangeException(nameof(tag), tag, "Not a refill tag.");
        }

        if (tag == RefillTag.Never)
        {
            throw new ArgumentException("A never pool is never refilled.", nameof(tag));
        }
    }

    // The unit with every spent count refilled by the tag dropped, or the unit itself if there is none.
    private static Unit Refilled(RulesContext rules, Unit unit, RefillTag tag)
    {
        var kept = ImmutableArray.CreateBuilder<PoolSpend>(unit.Spent.Length);
        foreach (var spend in unit.Spent)
        {
            var pool = rules.FindPool(spend.PoolId);
            bool refills = pool is not null
                && (pool.Refill == tag || (tag == RefillTag.Camp && pool.Refill == RefillTag.Rest));
            if (!refills)
            {
                kept.Add(spend);
            }
        }

        return kept.Count == unit.Spent.Length ? unit : unit with { Spent = kept.ToImmutable() };
    }

    private static long MaxOf(RulesContext rules, GameState s, Unit unit, PoolDefinition pool) =>
        pool.Max(new NumberBaseArgs(rules, s, unit, ImmutableArray<string>.Empty));

    private static long CurrentOf(RulesContext rules, GameState s, Unit unit, PoolDefinition pool) =>
        Math.Max(0, checked(MaxOf(rules, s, unit, pool) - SpentOf(unit, pool.Id)));

    private static int SpentOf(Unit unit, string poolId)
    {
        foreach (var spend in unit.Spent)
        {
            if (string.Equals(spend.PoolId, poolId, StringComparison.Ordinal))
            {
                return spend.Spent;
            }
        }

        return 0;
    }

    // The spent counts with poolId's set to value: sorted by ordinal pool id, a zero dropped.
    private static ImmutableArray<PoolSpend> WithSpent(ImmutableArray<PoolSpend> spent, string poolId, int value)
    {
        var builder = ImmutableArray.CreateBuilder<PoolSpend>(spent.Length + 1);
        bool placed = false;
        foreach (var spend in spent)
        {
            int c = string.CompareOrdinal(spend.PoolId, poolId);
            if (c == 0)
            {
                continue;
            }

            if (c > 0 && !placed)
            {
                if (value > 0)
                {
                    builder.Add(new PoolSpend(poolId, value));
                }

                placed = true;
            }

            builder.Add(spend);
        }

        if (!placed && value > 0)
        {
            builder.Add(new PoolSpend(poolId, value));
        }

        return builder.ToImmutable();
    }

    private static Unit Find(GameState s, UnitId id) =>
        s.FindUnit(id) ?? throw new ArgumentException($"Unit {id.Value} is not in the state.", nameof(id));
}
