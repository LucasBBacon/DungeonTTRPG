using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Determinism;
using Dungeon.Core.Effects;

namespace Dungeon.Core.Stats;

using Effects = Dungeon.Core.Effects.Effects;

/// <summary>One unit's initiative: the d20, the total with the <c>initiative</c> number, and the folded Intelligence.</summary>
public readonly record struct InitiativeEntry(UnitId Unit, int Natural, long Total, long Intelligence);

/// <summary>Initiative, rolled once per fight (SM 6): d20 + Intelligence, ties by Intelligence then unit order.</summary>
public static class Initiative
{
    /// <summary>
    /// Draws one normal d20 per unit from <c>Rng.Combat</c> in ascending id order, whatever the order of <paramref name="units"/>,
    /// and returns the entries sorted by total, then Intelligence (both descending), then id (ascending).
    /// </summary>
    /// <exception cref="ArgumentException">No units, an unknown id or a repeated id; nothing is drawn.</exception>
    public static (ImmutableArray<InitiativeEntry> Order, GameState Next) Roll(
        RulesContext rules, GameState s, IEnumerable<UnitId> units)
    {
        ArgumentNullException.ThrowIfNull(units);
        var ids = units.ToList();
        if (ids.Count == 0)
        {
            throw new ArgumentException("Initiative needs at least one unit.", nameof(units));
        }

        ids.Sort(IdOrder.Instance);
        for (int i = 0; i < ids.Count; i++)
        {
            if (s.FindUnit(ids[i]) is null)
            {
                throw new ArgumentException($"Unknown unit {ids[i].Value}.", nameof(units));
            }

            if (i > 0 && ids[i] == ids[i - 1])
            {
                throw new ArgumentException($"Unit {ids[i].Value} is listed twice.", nameof(units));
            }
        }

        var stream = s.Rng.Combat;
        var entries = new List<InitiativeEntry>(ids.Count);
        foreach (var id in ids)
        {
            var (roll, next) = Dice.D20(stream, RollMode.Normal);
            stream = next;
            entries.Add(new InitiativeEntry(
                id,
                roll.First,
                roll.First + Effects.Number(rules, s, id, "initiative"),
                Effects.Number(rules, s, id, StatIds.Number(Ability.Intelligence))));
        }

        entries.Sort(OutcomeOrder.Instance);
        return ([.. entries], s with { Rng = s.Rng with { Combat = stream } });
    }

    [TotalComparer]
    private sealed class IdOrder : IComparer<UnitId>
    {
        public static readonly IdOrder Instance = new();

        public int Compare(UnitId x, UnitId y) => x.Value.CompareTo(y.Value);
    }

    // Total descending, Intelligence descending, id ascending: total, since ids are distinct.
    [TotalComparer]
    private sealed class OutcomeOrder : IComparer<InitiativeEntry>
    {
        public static readonly OutcomeOrder Instance = new();

        public int Compare(InitiativeEntry x, InitiativeEntry y)
        {
            int c = y.Total.CompareTo(x.Total);
            if (c == 0)
            {
                c = y.Intelligence.CompareTo(x.Intelligence);
            }

            return c != 0 ? c : x.Unit.Value.CompareTo(y.Unit.Value);
        }
    }
}
