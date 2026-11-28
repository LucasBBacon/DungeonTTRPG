using System;
using System.Collections.Immutable;
using System.Numerics;
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
    /// <summary>The most dice a damage part may roll (before a crit doubles them), so the preview stays cheap.</summary>
    public const int MaxPartDice = 20;

    /// <summary>The most sides a damage part's die may have.</summary>
    public const int MaxPartSides = 100;

    /// <summary>The largest magnitude of a damage part's flat amount.</summary>
    public const int MaxPartFlat = 9999;

    private const int D20Faces = 20;

    // The most faces a chance counts: 400 face pairs under advantage or disadvantage.
    private const int MaxFaces = D20Faces * D20Faces;

    /// <summary>
    /// Previews <paramref name="abilityId"/> used by <paramref name="user"/> on <paramref name="target"/> (AB 8.3; plan
    /// call 14). It draws nothing and changes nothing. It reads the same view as <see cref="CheckTarget"/> (plan call 4),
    /// pays the cost on it as resolution does, locks the targets with <see cref="Affected"/>, and builds each setup with
    /// the code resolution uses. Chances count the d20 faces (or the 400 face pairs under advantage or disadvantage) for
    /// which <see cref="Contests.ResolveAttack"/> hits or <see cref="Contests.ResolveCheck"/> fails. Expected damage is
    /// exact: each part's dice-sum distribution (an integer convolution) through the ladder step
    /// <see cref="Damage.DealtByPart"/> applies, a crit doubling the dice, summed in <see cref="BigInteger"/> and reduced
    /// once. <see cref="CheckPreviewable"/>, which the loader and <see cref="RulesContext"/> enforce, guarantees the
    /// reduced result fits a <see cref="Rational"/>, so the preview never overflows on an ability the rules hold.
    /// Reactions are left out; every target is previewed on the locked state, so damage dealt to an earlier target in
    /// the same use is not reflected.
    /// </summary>
    public static AbilityPreview Preview(
        RulesContext rules, GameState s, UnitId user, string abilityId, AbilityTarget target)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(s);
        var view = View(rules, s, user, abilityId, target);
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
            var (kind, normal, crits, faces) = Odds(ability, setup);

            // (normal · E[normal] + crits · E[crit]) / faces, exact.
            var expected = Scale(Mean(rules, paid, unit, parts, false), normal);
            if (crits != 0)
            {
                expected = Sum(expected, Scale(Mean(rules, paid, unit, parts, true), crits));
            }

            var damage = Reduce(expected.Num, expected.Den * faces);
            targets.Add(new TargetPreview(unit, kind, setup, Ratio(normal + crits, faces), parts, damage));
        }

        return new AbilityPreview(CommandError.None, squares, units, targets.MoveToImmutable());
    }

    /// <summary>
    /// Whether <paramref name="parts"/> can be previewed exactly (plan call 14). Null when they can; otherwise the index
    /// of the offending part (-1 for the list as a whole), the part's field (<c>dice</c> or <c>flat</c>; empty for the
    /// list) and the problem. The ability loader and <see cref="RulesContext"/> (for code-built definitions) refuse what
    /// this refuses, so <see cref="Preview"/> never throws on an ability the rules hold.
    /// <para>
    /// Each part needs 0 to <see cref="MaxPartDice"/> dice of 1 to <see cref="MaxPartSides"/> sides and a flat of
    /// magnitude at most <see cref="MaxPartFlat"/>. Then the list must fit a <c>long</c> rational, by this bound. Over m
    /// dice of s sides, the denominator of a part's mean dealt amount divides L(m): 1 when m = 0 or s = 1; 4 when s is
    /// even and flat + m &gt;= 2; s^m otherwise. Proof: the mean is a sum of integer amounts times probabilities of
    /// denominator s^m, so s^m always works. When s is even and flat + m &gt;= 2, every raw amount is at least 2, so no
    /// ladder step reaches the minimum of 1, and the dealt amount is raw (normal), floor(raw / 2) (resistant),
    /// raw + floor(raw / 2) (weak) or 0 (immune). With floor(raw / 2) = (raw - [raw odd]) / 2, E[raw] = flat +
    /// m(s + 1) / 2 of denominator 2, and P(raw odd) = 1/2 (an even-sided die is odd or even with equal chance), the mean
    /// has a denominator dividing 4. A target's expected damage is (normal · E[normal] + crits · E[crit]) / faces with at
    /// most 400 faces, so its reduced denominator is at most Q = 400 · Π L(n) · L(2n) over the parts; every dealt amount
    /// lies in 0..A with A = Σ (2 · (|flat| + 2 · n · s) + 1), so its numerator is at most A · Q. The list is accepted
    /// when A · Q &lt;= <see cref="long.MaxValue"/>.
    /// </para>
    /// </summary>
    public static (int Part, string Field, string Problem)? CheckPreviewable(ImmutableArray<DamagePart> parts)
    {
        if (parts.IsDefaultOrEmpty)
        {
            return null;
        }

        BigInteger q = MaxFaces;
        BigInteger a = BigInteger.Zero;
        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            if (part.Dice < 0 || part.Dice > MaxPartDice)
            {
                return (i, "dice", $"a part rolls 0 to {MaxPartDice} dice, so its preview stays exact and cheap; this one rolls {part.Dice}.");
            }

            if (part.Dice > 0 && (part.Sides < 1 || part.Sides > MaxPartSides))
            {
                return (i, "dice", $"a die has 1 to {MaxPartSides} sides, so its preview stays exact and cheap; this one has {part.Sides}.");
            }

            if (part.Flat < -MaxPartFlat || part.Flat > MaxPartFlat)
            {
                return (i, "flat", $"a flat amount lies in -{MaxPartFlat}..{MaxPartFlat}; this one is {part.Flat}.");
            }

            q *= DenominatorBound(part, part.Dice) * DenominatorBound(part, 2 * part.Dice);
            a += (2 * (Math.Abs((long)part.Flat) + (2L * part.Dice * part.Sides))) + 1;
        }

        return a * q <= long.MaxValue
            ? null
            : (-1, string.Empty, "the damage list is too large for an exact preview: its expected damage could need more than 64 bits (odd-sided dice, and dice a negative flat can take below 2, count sides^dice).");
    }

    // L(m) in CheckPreviewable: a bound on the denominator of one part's mean dealt amount over m dice.
    private static BigInteger DenominatorBound(DamagePart part, int m)
    {
        if (m == 0 || part.Sides == 1)
        {
            return BigInteger.One;
        }

        return IntMath.FloorMod(part.Sides, 2) == 0 && (long)part.Flat + m >= 2 ? new BigInteger(4) : BigInteger.Pow(part.Sides, m);
    }

    // The kind; the faces (or face pairs) for which the damage lands unmodified (a normal hit, a failed save) and as a
    // crit; and the face count. Automatic is 1 of 1.
    private static (ContestKind Kind, long Normal, long Crits, long Faces) Odds(AbilityDefinition ability, ContestSetup? setup)
    {
        if (setup is not { } contest)
        {
            return (ContestKind.Automatic, 1, 0, 1);
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

        return (attack ? ContestKind.Attack : ContestKind.Save, normal, crits, faces);
    }

    // The exact mean of the parts' dealt total on target, as an unreduced fraction: the sum of each part's mean, since
    // the parts are independent.
    private static (BigInteger Num, BigInteger Den) Mean(
        RulesContext rules, GameState s, UnitId target, ImmutableArray<DamagePart> parts, bool crit)
    {
        (BigInteger Num, BigInteger Den) mean = (BigInteger.Zero, BigInteger.One);
        foreach (var part in parts)
        {
            int dice = crit ? checked(2 * part.Dice) : part.Dice;
            if (dice < 0 || (dice > 0 && part.Sides < 1))
            {
                throw new ArgumentException($"Damage part {part} cannot be rolled.", nameof(parts));
            }

            // counts[k]: how many of the sides^dice outcomes sum to dice + k. Each die convolves by a sliding window
            // of `sides` entries.
            BigInteger[] counts = [BigInteger.One];
            for (int d = 0; d < dice; d++)
            {
                var next = new BigInteger[checked(counts.Length + part.Sides - 1)];
                var window = BigInteger.Zero;
                for (int j = 0; j < next.Length; j++)
                {
                    if (j < counts.Length)
                    {
                        window += counts[j];
                    }

                    if (j >= part.Sides)
                    {
                        window -= counts[j - part.Sides];
                    }

                    next[j] = window;
                }

                counts = next;
            }

            // Damage.DealtByPart is Ladder.Apply at the target's step for the part's type; the step is read once.
            var step = Ladder.StepFor(rules, s, target, part.Type);
            var weighted = BigInteger.Zero;
            var outcomes = BigInteger.Zero;
            for (int k = 0; k < counts.Length; k++)
            {
                long raw = checked(part.Flat + dice + k);
                weighted += counts[k] * Ladder.Apply(step, raw);
                outcomes += counts[k];
            }

            mean = Sum(mean, (weighted, outcomes));
        }

        return mean;
    }

    private static (BigInteger Num, BigInteger Den) Scale((BigInteger Num, BigInteger Den) x, long by) =>
        (x.Num * by, x.Den);

    private static (BigInteger Num, BigInteger Den) Sum((BigInteger Num, BigInteger Den) x, (BigInteger Num, BigInteger Den) y) =>
        ((x.Num * y.Den) + (y.Num * x.Den), x.Den * y.Den);

    // num / den reduced, as a Rational; den > 0 and num >= 0. CheckPreviewable guarantees both fit a long.
    private static Rational Reduce(BigInteger num, BigInteger den)
    {
        var g = BigInteger.GreatestCommonDivisor(num, den);
        return Rational.Of((long)BigInteger.Divide(num, g), (long)BigInteger.Divide(den, g));
    }

    // p / q reduced; q > 0.
    private static Rational Ratio(long p, long q) => Rational.Of(p, 1).Multiply(Rational.Of(1, q));
}
