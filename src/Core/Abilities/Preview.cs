using System;
using System.Collections.Immutable;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;

namespace Dungeon.Core.AbilityModel;

using Damage = Dungeon.Core.Damage.Damage;

/// <summary>What decides whether a previewed target takes the ability's damage.</summary>
public enum ContestKind
{
    /// <summary>An attack roll against the target: damage on a hit, the dice doubled on a crit.</summary>
    Attack,

    /// <summary>The target's save: damage when it fails.</summary>
    Save,

    /// <summary>No roll: the damage always lands.</summary>
    Automatic,
}

/// <summary>
/// One target's odds (AB 8.3; M1-11 plan call 14). <paramref name="Setup"/> is the contest resolution would build, null
/// for <see cref="ContestKind.Automatic"/>. <paramref name="Chance"/> is P(hit) for an attack, P(the save fails) for a
/// save, and 1 for automatic. <paramref name="Parts"/> are the ability's damage parts; <paramref name="ExpectedDamage"/>
/// is the exact mean of <see cref="DamageOutcome.Total"/> before the HP floors, weighted by the normal and crit chances.
/// </summary>
public sealed record TargetPreview(
    UnitId Target,
    ContestKind Kind,
    ContestSetup? Setup,
    Rational Chance,
    ImmutableArray<DamagePart> Parts,
    Rational ExpectedDamage);

/// <summary>
/// A use previewed (AB 8.3): <see cref="AbilityUse.CheckTarget"/>'s code, and when it is
/// <see cref="CommandError.None"/>, the area squares, the affected units in lock order and each one's odds. A refused
/// use has empty arrays.
/// </summary>
public sealed record AbilityPreview(
    CommandError Error,
    ImmutableArray<GridPos> Squares,
    ImmutableArray<UnitId> Affected,
    ImmutableArray<TargetPreview> Targets);

public static partial class AbilityUse
{
    private const int D20Faces = 20;

    /// <summary>
    /// Previews <paramref name="abilityId"/> used by <paramref name="user"/> on <paramref name="target"/> (AB 8.3; plan
    /// call 14). It draws nothing and changes nothing. It reads the same view as <see cref="CheckTarget"/> (plan call 4),
    /// pays the cost on it as resolution does, locks the targets with <see cref="Affected"/>, and builds each setup with
    /// the code resolution uses. Chances count the d20 faces (or the 400 face pairs under advantage or disadvantage) for
    /// which <see cref="Contests.ResolveAttack"/> hits or <see cref="Contests.ResolveCheck"/> fails. Expected damage is
    /// exact: each part's dice-sum distribution (an integer convolution) through <see cref="Damage.DealtByPart"/>, a crit
    /// doubling the dice. Reactions are left out; every target is previewed on the locked state, so damage dealt to an
    /// earlier target in the same use is not reflected.
    /// </summary>
    public static AbilityPreview Preview(
        RulesContext rules, GameState s, UnitId user, string abilityId, AbilityTarget target)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var view = View(rules, s);
        var error = Targetable(rules, view, user, abilityId, target);
        if (error != CommandError.None)
        {
            return new AbilityPreview(error, [], [], []);
        }

        var ability = rules.Ability(abilityId);
        var paid = Resolution.Pay(rules, view, user, ability).Next;
        var (squares, units) = Affected(rules, paid, user, ability, target);
        long difficulty = Resolution.Difficulty(rules, paid, user, ability);
        ImmutableArray<DamagePart> parts = ability.Damage.IsDefault ? [] : ability.Damage;

        var targets = ImmutableArray.CreateBuilder<TargetPreview>(units.Length);
        foreach (var unit in units)
        {
            var setup = Resolution.Prepare(rules, paid, user, ability, unit, difficulty);
            var (kind, normal, crit) = Odds(ability, setup);
            var expected = normal.Multiply(Mean(rules, paid, unit, parts, false));
            if (crit.P != 0)
            {
                expected = expected.Add(crit.Multiply(Mean(rules, paid, unit, parts, true)));
            }

            targets.Add(new TargetPreview(unit, kind, setup, normal.Add(crit), parts, expected));
        }

        return new AbilityPreview(CommandError.None, squares, units, targets.MoveToImmutable());
    }

    // The kind, and the chances the damage lands unmodified (a normal hit, a failed save, or always) and as a crit.
    private static (ContestKind Kind, Rational Normal, Rational Crit) Odds(AbilityDefinition ability, ContestSetup? setup)
    {
        if (setup is not { } contest)
        {
            return (ContestKind.Automatic, Ratio(1, 1), Ratio(0, 1));
        }

        bool attack = ability.Resolve is AttackResolve;
        long normal = 0;
        long crits = 0;
        long faces = 0;
        for (int first = 1; first <= D20Faces; first++)
        {
            int seconds = contest.Mode == RollMode.Normal ? 1 : D20Faces;
            for (int second = 1; second <= seconds; second++)
            {
                var roll = new D20Roll(contest.Mode, first, contest.Mode == RollMode.Normal ? null : second);
                faces++;
                if (attack)
                {
                    var result = Contests.ResolveAttack(contest, roll);
                    if (result.Crit)
                    {
                        crits++;
                    }
                    else if (result.Hit)
                    {
                        normal++;
                    }
                }
                else if (!Contests.ResolveCheck(contest, roll).Success)
                {
                    normal++;
                }
            }
        }

        return (attack ? ContestKind.Attack : ContestKind.Save, Ratio(normal, faces), Ratio(crits, faces));
    }

    // The exact mean of the parts' dealt total on target: the sum of each part's mean, since the parts are independent.
    private static Rational Mean(RulesContext rules, GameState s, UnitId target, ImmutableArray<DamagePart> parts, bool crit)
    {
        var mean = Ratio(0, 1);
        foreach (var part in parts)
        {
            int dice = crit ? checked(2 * part.Dice) : part.Dice;
            if (dice < 0 || (dice > 0 && part.Sides < 1))
            {
                throw new ArgumentException($"Damage part {part} cannot be rolled.", nameof(parts));
            }

            // counts[k]: the number of the sides^dice outcomes whose faces sum to dice + k.
            long[] counts = [1];
            for (int d = 0; d < dice; d++)
            {
                var next = new long[checked(counts.Length + part.Sides - 1)];
                for (int k = 0; k < counts.Length; k++)
                {
                    for (int f = 0; f < part.Sides; f++)
                    {
                        next[k + f] = checked(next[k + f] + counts[k]);
                    }
                }

                counts = next;
            }

            long weighted = 0;
            long outcomes = 0;
            for (int k = 0; k < counts.Length; k++)
            {
                long raw = checked(part.Flat + dice + k);
                weighted = checked(weighted + (counts[k] * Damage.DealtByPart(rules, s, target, part, raw)));
                outcomes = checked(outcomes + counts[k]);
            }

            mean = mean.Add(Ratio(weighted, outcomes));
        }

        return mean;
    }

    // p / q reduced; q > 0.
    private static Rational Ratio(long p, long q) => Rational.Of(p, 1).Multiply(Rational.Of(1, q));
}
