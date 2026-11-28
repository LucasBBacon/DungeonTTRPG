using System.Collections.Immutable;
using System.Numerics;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using Xunit;
using static Dungeon.Core.Tests.AbilityFixtures;

namespace Dungeon.Core.Tests;

// These classes share their names with their namespaces, so name them explicitly inside this namespace.
using Damage = Dungeon.Core.Damage.Damage;
using Effects = Dungeon.Core.Effects.Effects;

/// <summary>
/// The preview (M1-11 Task 5; plan call 14; AB 8.3): draw-free, exact odds and expected damage, and equal to
/// resolution with full knowledge and reactions off (AB 16).
/// </summary>
public class PreviewTests
{
    private static readonly UnitId Hero = new(1);
    private static readonly UnitId Two = new(2);

    /// <summary>An attack: single, range 1, a Strength attack, 1d6+1 slashing.</summary>
    private const string Cut = "test.pv.cut";

    /// <summary>An attack: single, range 3, a Strength attack, 2d6 slashing and 1d4+1 fire.</summary>
    private const string Twin = "test.pv.twin";

    /// <summary>A save burst: range 4, radius 1, a reflex save, 1d6 fire and 2d4 cold.</summary>
    private const string Blast = "test.pv.blast";

    /// <summary>No roll, single, range 2: 2d6−3 lightning, so the per-part minimum of 1 binds.</summary>
    private const string Zap = "test.pv.zap";

    /// <summary>The largest even-sided attack the bound allows: 20d100+9999 slashing, 20d100 fire, 20d2 cold.</summary>
    private const string Huge = "test.pv.huge";

    /// <summary>An odd-sided attack on the exactness bound: 9d3 lightning (10d3 is refused at load).</summary>
    private const string Edge = "test.pv.edge";

    private const string AttackAdv = "pv.attack.adv";
    private const string AttackDis = "pv.attack.dis";
    private const string AgainstDis = "pv.against.dis";
    private const string SaveAdv = "pv.save.adv";
    private const string SaveDis = "pv.save.dis";

    private static readonly DamageType[] Types =
        [DamageType.Slashing, DamageType.Bludgeoning, DamageType.Fire, DamageType.Cold, DamageType.Lightning];

    private static readonly ImmutableArray<AbilityDefinition> Extra =
    [
        new(AbilityFixtures.File, Cut, AbilityKind.AtWill, new AbilityCost(CostKind.Action), null, new TargetSpec(TargetShape.Single, 1, 1, 0, false),
            new AttackResolve(new AttackSpec(Ability.Strength, true)), [new(DamageType.Slashing, 1, 6, 1)], [], null),
        new(AbilityFixtures.File, Twin, AbilityKind.Limited, new AbilityCost(CostKind.Action), null, new TargetSpec(TargetShape.Single, 3, 1, 0, false),
            new AttackResolve(new AttackSpec(Ability.Strength, true)), [new(DamageType.Slashing, 2, 6, 0), new(DamageType.Fire, 1, 4, 1)], [], null),
        new(AbilityFixtures.File, Blast, AbilityKind.Limited, new AbilityCost(CostKind.Action), null, new TargetSpec(TargetShape.Burst, 4, 0, 1, false),
            new SaveResolve(Save.Reflex), [new(DamageType.Fire, 1, 6, 0), new(DamageType.Cold, 2, 4, 0)], [], null),
        new(AbilityFixtures.File, Zap, AbilityKind.Limited, new AbilityCost(CostKind.Action), null, new TargetSpec(TargetShape.Single, 2, 1, 0, false),
            null, [new(DamageType.Lightning, 2, 6, -3)], [], null),
        new(AbilityFixtures.File, Huge, AbilityKind.Limited, new AbilityCost(CostKind.Action), null, new TargetSpec(TargetShape.Single, 1, 1, 0, false),
            new AttackResolve(new AttackSpec(Ability.Strength, true)),
            [new(DamageType.Slashing, 20, 100, 9999), new(DamageType.Fire, 20, 100, 0), new(DamageType.Cold, 20, 2, 0)], [], null),
        new(AbilityFixtures.File, Edge, AbilityKind.Limited, new AbilityCost(CostKind.Action), null, new TargetSpec(TargetShape.Single, 1, 1, 0, false),
            new AttackResolve(new AttackSpec(Ability.Strength, true)), [new(DamageType.Lightning, 9, 3, 0)], [], null),
    ];

    // Every fixture ability but the ward: reactions are off (AB 8.3).
    private static readonly ImmutableArray<string> Held =
        [.. Definitions.Select(d => d.Id).Concat(Extra.Select(d => d.Id)).Where(id => id != Ward).OrderBy(id => id, StringComparer.Ordinal)];

    private static readonly string[] Usable = [Strike, Sure, Cut, Twin, Zap, SaveBurst, SelectiveBurst, AtWillCone, SaveLine, Blast];

    private static readonly RulesContext Rules = RulesContext.Create(
        ContestKit.LoadEffects(
        [
            ContestKit.Def(AttackAdv, "condition", ContestKit.Roll("attackRoll", "advantage")),
            ContestKit.Def(AttackDis, "condition", ContestKit.Roll("attackRoll", "disadvantage")),
            ContestKit.Def(AgainstDis, "condition", ContestKit.Roll("attacksAgainst", "disadvantage")),
            ContestKit.Def(SaveAdv, "condition", ContestKit.Roll("saveRoll", "advantage")),
            ContestKit.Def(SaveDis, "condition", ContestKit.Roll("saveRoll", "disadvantage")),
            .. Types.SelectMany(t => new[]
            {
                ContestKit.Def("pv." + DamageIds.Resistant(t), "trait", Flag(DamageIds.Resistant(t))),
                ContestKit.Def("pv." + DamageIds.Weak(t), "trait", Flag(DamageIds.Weak(t))),
                ContestKit.Def("pv." + DamageIds.Immune(t), "trait", Flag(DamageIds.Immune(t))),
            }),
            .. Enumerable.Range(-30, 61).Where(n => n != 0).SelectMany(n => new[]
            {
                ContestKit.Def(Tune("attack", n), "gear", ContestKit.Add("attack", n)),
                ContestKit.Def(Tune("armourClass", n), "gear", ContestKit.Add("armourClass", n)),
                ContestKit.Def(Tune("reflex", n), "gear", ContestKit.Add("reflex", n)),
                ContestKit.Def(Tune("abilityDifficulty", n), "gear", ContestKit.Add("abilityDifficulty", n)),
            }),
        ]),
        pools: PoolTable,
        abilities: [.. Definitions, .. Extra]);

    private static string Flag(string name) => $$"""{"target": "unit", "flag": "{{name}}"}""";

    private static string Tune(string number, int n) => $"pv.{number.ToLowerInvariant()}.{n}";

    private static GameState On(GameState s, UnitId unit, string effect) =>
        Effects.Apply(s, effect, unit, new EffectSource(unit));

    // Moves unit's number to exactly value, with one add effect.
    private static GameState Set(GameState s, UnitId unit, string number, long value)
    {
        long delta = value - Effects.Number(Rules, s, unit, number);
        return delta == 0 ? s : On(s, unit, Tune(number, (int)delta));
    }

    private static readonly GridMap Open = GridMap.Parse(".....", ".....", ".....", ".....", ".....");

    // The hero (1,1) beside foe 2 (2,1); the hero's turn of round 1.
    private static GameState Duel(ulong seed = 7) =>
        InCombat(GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Held }, Foe(2, 2, 1) with { Abilities = Held }], seed, hero: Hero), 1, 2);

    // --- Fixed cases ---

    [Fact]
    public void Preview_draws_nothing()
    {
        var s = Duel();
        var before = Snapshot.Save(s);
        var counter = s.Rng.Combat.Counter;

        var p = AbilityUse.Preview(Rules, s, Hero, Twin, new UnitsTarget([Two]));

        Assert.Equal(CommandError.None, p.Error);
        Assert.Single(p.Targets);
        Assert.Equal(before, Snapshot.Save(s));
        Assert.Equal(counter, s.Rng.Combat.Counter);

        // Out of combat too: the preview reads the tick-reset view and leaves the state alone.
        var calm = GameState.Create(Open, [Party(1, 1, 1) with { Abilities = Held }, Party(3, 3, 3) with { Abilities = Held }], 7, hero: Hero);
        var calmBefore = Snapshot.Save(calm);
        var q = AbilityUse.Preview(Rules, calm, Hero, CampBurst, new PointTarget(new GridPos(3, 3)));
        Assert.Equal(CommandError.None, q.Error);
        Assert.Equal<UnitId>([new UnitId(3)], q.Affected.ToArray());
        var only = Assert.Single(q.Targets);
        Assert.Equal((ContestKind.Automatic, (ContestSetup?)null, Rational.Of(1, 1), Rational.Of(3, 1)), (only.Kind, only.Setup, only.Chance, only.ExpectedDamage));
        Assert.Equal(calmBefore, Snapshot.Save(calm));
    }

    [Fact]
    public void Hit_chance_counts_faces()
    {
        var s = Set(Set(Duel(), Hero, "attack", 3 - BaseBonus(Duel())), Two, "armourClass", 13);
        Assert.Equal(3, Contests.PrepareAttack(Rules, s, Hero, Two, new AttackSpec(Ability.Strength, true)).Bonus);

        var t = Assert.Single(AbilityUse.Preview(Rules, s, Hero, Strike, new UnitsTarget([Two])).Targets);
        Assert.Equal((ContestKind.Attack, new ContestSetup(3, 13, RollMode.Normal)), (t.Kind, t.Setup!.Value));
        Assert.Equal(Rational.Of(11, 20), t.Chance);

        var adv = Assert.Single(AbilityUse.Preview(Rules, On(s, Hero, AttackAdv), Hero, Strike, new UnitsTarget([Two])).Targets);
        Assert.Equal(new ContestSetup(3, 13, RollMode.Advantage), adv.Setup);
        Assert.Equal(Rational.Of(400 - (9 * 9), 400), adv.Chance);
    }

    [Fact]
    public void Expected_damage_is_exact()
    {
        // Bonus 2 against 13: hits on 11-20, a crit on 20. 1d6+1 averages 9/2; the crit 2d6+1 averages 8.
        var s = Set(Set(Duel(), Hero, "attack", 2 - BaseBonus(Duel())), Two, "armourClass", 13);

        var t = Assert.Single(AbilityUse.Preview(Rules, s, Hero, Cut, new UnitsTarget([Two])).Targets);
        Assert.Equal(Rational.Of(1, 2), t.Chance);
        Assert.Equal<DamagePart>([new(DamageType.Slashing, 1, 6, 1)], t.Parts.ToArray());
        Assert.Equal(Rational.Of(97, 40), t.ExpectedDamage);
        Assert.Equal(Rational.Of(9, 20).Multiply(Rational.Of(9, 2)).Add(Rational.Of(1, 20).Multiply(Rational.Of(8, 1))), t.ExpectedDamage);

        // Resistant: each roll is halved and rounded down before averaging (normal mean 2, crit mean 15/4).
        var resistant = On(s, Two, "pv." + DamageIds.Resistant(DamageType.Slashing));
        var r = Assert.Single(AbilityUse.Preview(Rules, resistant, Hero, Cut, new UnitsTarget([Two])).Targets);
        Assert.Equal(Rational.Of(87, 80), r.ExpectedDamage);
        Assert.NotEqual(Rational.Of(97, 80), r.ExpectedDamage);
    }

    [Fact]
    public void A_refused_preview_has_the_code_and_nothing_else()
    {
        var s = Duel();
        var p = AbilityUse.Preview(Rules, s, Hero, Strike, new UnitsTarget([new UnitId(99)]));
        Assert.Equal(CommandError.InvalidTarget, p.Error);
        Assert.Empty(p.Squares);
        Assert.Empty(p.Affected);
        Assert.Empty(p.Targets);
        Assert.Equal(CommandError.UnknownUnit, AbilityUse.Preview(Rules, s, new UnitId(99), Strike, new UnitsTarget([Two])).Error);
    }

    private static long BaseBonus(GameState s) =>
        Contests.PrepareAttack(Rules, s, Hero, Two, new AttackSpec(Ability.Strength, true)).Bonus;

    // --- The exactness bound (review fix 1) ---

    public static TheoryData<string, string, string> WorstCases()
    {
        var data = new TheoryData<string, string, string>();
        foreach (var ability in new[] { Huge, Edge })
        {
            foreach (var step in new[] { "", "resistant.", "weak.", "immune." })
            {
                foreach (var mode in new[] { "", AttackAdv, AttackDis })
                {
                    data.Add(ability, step, mode);
                }
            }
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(WorstCases))]
    public void The_largest_allowed_parts_preview_exactly(string abilityId, string step, string mode)
    {
        // Bonus 9 against 20: normal hits on 11-19 (or their pairs), a crit on 20.
        var s = Set(Set(Duel(), Hero, "attack", 9 - BaseBonus(Duel())), Two, "armourClass", 20);
        if (step.Length > 0)
        {
            foreach (var type in Types)
            {
                s = On(s, Two, "pv." + step + type.ToString().ToLowerInvariant());
            }
        }

        if (mode.Length > 0)
        {
            s = On(s, Hero, mode);
        }

        Assert.Null(AbilityUse.CheckPreviewable(Rules.Ability(abilityId).Damage));
        var t = Assert.Single(AbilityUse.Preview(Rules, s, Hero, abilityId, new UnitsTarget([Two])).Targets);
        var (normal, crits, den) = Faces(t);
        Assert.True(crits > 0 && normal > 0);

        // An independent oracle, in BigInteger: per part, the naive dice-sum distribution and Damage.Resolve on a
        // one-part roll for each sum; for the even-sided Huge, whose crit has 40d100, the closed form of the bound's proof.
        var (nNum, nDen) = abilityId == Huge ? ClosedForm(s, t, false) : Naive(s, t, false);
        var (cNum, cDen) = abilityId == Huge ? ClosedForm(s, t, true) : Naive(s, t, true);
        var num = (normal * nNum * cDen) + (crits * cNum * nDen);
        var q = den * nDen * cDen;
        Assert.Equal(num * t.ExpectedDamage.Q, q * t.ExpectedDamage.P);
    }

    [Fact]
    public void The_bound_sits_where_the_proof_puts_it()
    {
        static ImmutableArray<DamagePart> One(int dice, int sides, int flat) => [new(DamageType.Fire, dice, sides, flat)];

        Assert.Null(AbilityUse.CheckPreviewable(One(9, 3, 0)));
        Assert.Equal((-1, ""), Where(AbilityUse.CheckPreviewable(One(10, 3, 0))));
        Assert.Null(AbilityUse.CheckPreviewable(One(20, 100, 9999)));
        Assert.Null(AbilityUse.CheckPreviewable(One(8, 8, 0)));
        Assert.Equal((0, "dice"), Where(AbilityUse.CheckPreviewable(One(21, 6, 0))));
        Assert.Equal((0, "dice"), Where(AbilityUse.CheckPreviewable(One(1, 101, 0))));
        Assert.Equal((0, "flat"), Where(AbilityUse.CheckPreviewable(One(0, 0, 10000))));
        Assert.Equal((0, "flat"), Where(AbilityUse.CheckPreviewable(One(0, 0, -10000))));

        // A negative flat that can take even-sided dice below 2 counts sides^dice: 8d8-10 does so only without a crit
        // (8^8 · 4) and fits; 8d8-20 does so with one too (8^8 · 8^16) and is past the bound.
        Assert.Null(AbilityUse.CheckPreviewable(One(8, 8, -10)));
        Assert.Equal((-1, ""), Where(AbilityUse.CheckPreviewable(One(8, 8, -20))));

        // The proof's lemma, by brute force on small parts at every step: the reduced denominator of a part's mean
        // divides L(m).
        foreach (var lstep in Enum.GetValues<LadderStep>())
        {
            for (int sides = 1; sides <= 6; sides++)
            {
                for (int m = 0; m <= 4; m++)
                {
                    for (int flat = -6; flat <= 3; flat++)
                    {
                        long total = 0, outcomes = 0;
                        foreach (var faces in Outcomes(m, sides))
                        {
                            total += Ladder.Apply(lstep, flat + faces.Sum());
                            outcomes++;
                        }

                        var mean = Ratio(total, outcomes);
                        long bound = 1;
                        if (m > 0 && sides > 1)
                        {
                            bound = sides % 2 == 0 && flat + m >= 2 ? 4 : (long)BigInteger.Pow(sides, m);
                        }

                        Assert.True(bound % mean.Q == 0, $"{lstep} {m}d{sides} {flat}: {mean}");
                    }
                }
            }
        }
    }

    private static (int Part, string Field) Where((int Part, string Field, string Problem)? bad) =>
        (bad!.Value.Part, bad.Value.Field);

    // The mean dealt total over one case, as an unreduced BigInteger fraction: per part, the naive convolution of its
    // dice, and Damage.Resolve on a one-part roll whose faces reach each sum.
    private static (BigInteger Num, BigInteger Den) Naive(GameState s, TargetPreview t, bool crit)
    {
        (BigInteger Num, BigInteger Den) sum = (0, 1);
        foreach (var part in t.Parts)
        {
            int m = crit ? 2 * part.Dice : part.Dice;
            var counts = new BigInteger[(m * part.Sides) + 1];
            counts[0] = 1;
            for (int d = 0; d < m; d++)
            {
                var next = new BigInteger[counts.Length];
                for (int k = 0; k < counts.Length; k++)
                {
                    for (int f = 1; f <= part.Sides && k + f < counts.Length; f++)
                    {
                        next[k + f] += counts[k];
                    }
                }

                counts = next;
            }

            BigInteger num = 0, den = 0;
            for (int k = m; k < counts.Length; k++)
            {
                if (counts[k].IsZero)
                {
                    continue;
                }

                // m faces summing to k: all ones, then raise each in turn up to its sides.
                var faces = Enumerable.Repeat(1, m).ToArray();
                int left = k - m;
                for (int i = 0; i < m && left > 0; i++)
                {
                    int add = Math.Min(left, part.Sides - 1);
                    faces[i] += add;
                    left -= add;
                }

                var roll = new DamageRoll([new PartRoll(part, [.. faces], part.Flat + k)], crit);
                num += counts[k] * Damage.Resolve(Rules, s, t.Target, roll).Total;
                den += counts[k];
            }

            sum = ((sum.Num * den) + (num * sum.Den), sum.Den * den);
        }

        return sum;
    }

    // The closed form for even-sided parts whose raw amount is always at least 2: E[raw] = flat + m(s + 1) / 2 and
    // P(raw odd) = 1/2, through the target's step; over a denominator of 4.
    private static (BigInteger Num, BigInteger Den) ClosedForm(GameState s, TargetPreview t, bool crit)
    {
        BigInteger quarter = 0;
        foreach (var part in t.Parts)
        {
            int m = crit ? 2 * part.Dice : part.Dice;
            Assert.True(part.Sides % 2 == 0 && part.Flat + m >= 2);
            BigInteger raw4 = (4L * part.Flat) + (2L * m * (part.Sides + 1)); // 4 E[raw]
            BigInteger half4 = (raw4 - 2) / 2;                                 // 4 E[floor(raw / 2)] = 4 (E[raw] - 1/2) / 2
            quarter += Ladder.StepFor(Rules, s, t.Target, part.Type) switch
            {
                LadderStep.Normal => raw4,
                LadderStep.Resistant => half4,
                LadderStep.Weak => raw4 + half4,
                _ => BigInteger.Zero,
            };
        }

        return (quarter, 4);
    }

    // --- The property (AB 16) ---

    private const int Seeds = 200;

    private sealed record Case(GameState State, UnitId User, string AbilityId, AbilityTarget Target);

    // A random 5x5 map with a few walls, 2-3 party units and 2-3 foes with random ladder flags, roll modes and tuned
    // numbers, in a fight at a random unit's turn, and a random use by it.
    private static Case Generate(int seed)
    {
        var rng = new Random(seed);
        const int Size = 5;
        var rows = new char[Size][];
        for (int y = 0; y < Size; y++)
        {
            rows[y] = new char[Size];
            for (int x = 0; x < Size; x++)
            {
                rows[y][x] = rng.Next(100) < 12 ? '#' : '.';
            }
        }

        var floor = new List<GridPos>();
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (rows[y][x] == '.')
                {
                    floor.Add(new GridPos(x, y));
                }
            }
        }

        int party = 2 + rng.Next(2);
        int foes = 2 + rng.Next(2);
        var units = new List<Unit>();
        for (int i = 1; i <= party + foes; i++)
        {
            var pos = floor[rng.Next(floor.Count)];
            floor.Remove(pos);
            var unit = i <= party ? Party(i, pos.X, pos.Y) : Foe(i, pos.X, pos.Y, hp: 1 + rng.Next(12));
            units.Add(unit with { Abilities = Held });
        }

        var map = GridMap.Parse([.. rows.Select(r => new string(r))]);
        var order = units.Select(u => u.Id.Value).ToArray();
        var s = InCombat(GameState.Create(map, units, (ulong)seed, hero: Hero), order);

        foreach (var unit in units)
        {
            var id = unit.Id;
            if (rng.Next(3) == 0)
            {
                s = On(s, id, "pv." + DamageIds.Resistant(Types[rng.Next(Types.Length)]));
            }

            if (rng.Next(3) == 0)
            {
                s = On(s, id, "pv." + DamageIds.Weak(Types[rng.Next(Types.Length)]));
            }

            string[] modes = [AttackAdv, AttackDis, AgainstDis, SaveAdv, SaveDis];
            if (rng.Next(2) == 0)
            {
                s = On(s, id, modes[rng.Next(modes.Length)]);
            }

            s = Set(s, id, "armourClass", 8 + rng.Next(12));
            s = Set(s, id, "attack", Effects.Number(Rules, s, id, "attack") + rng.Next(-3, 4));
            s = Set(s, id, "reflex", rng.Next(-2, 8));
            s = Set(s, id, "abilityDifficulty", 8 + rng.Next(10));
        }

        var user = units[rng.Next(units.Count)].Id;
        s = s with { Combat = s.Combat! with { Current = s.Combat.Order.IndexOf(user) } };
        var abilityId = Usable[rng.Next(Usable.Length)];
        var spec = Rules.Ability(abilityId).Target!;

        // Mostly aimed at another unit, so most uses lock targets; sometimes anywhere, so some are refused.
        var from = units.Single(u => u.Id == user).Pos;
        var near = units.Where(u => u.Id != user && GridPos.Distance(from, u.Pos) <= Math.Max(1, spec.Range)).ToList();
        var other = rng.Next(8) > 0 && near.Count > 0 ? near[rng.Next(near.Count)] : units[rng.Next(units.Count)];
        AbilityTarget target = spec.Shape switch
        {
            TargetShape.Single => new UnitsTarget([other.Id]),
            TargetShape.Burst => new PointTarget(new GridPos(other.Pos.X + rng.Next(-1, 2), other.Pos.Y + rng.Next(-1, 2))),
            _ => new AimTarget(Toward(from, other.Pos) ?? DirectionExt.All[rng.Next(8)]),
        };
        return new Case(s, user, abilityId, target);
    }

    // The direction whose step best points from a to b (the signs of the offset), or null when they coincide.
    private static Direction? Toward(GridPos a, GridPos b)
    {
        var step = new GridPos(Math.Sign(b.X - a.X), Math.Sign(b.Y - a.Y));
        return DirectionExt.All.Cast<Direction?>().FirstOrDefault(d => d!.Value.Offset() == step);
    }

    public static TheoryData<int> SeedData()
    {
        var data = new TheoryData<int>();
        for (int seed = 1; seed <= Seeds; seed++)
        {
            data.Add(seed);
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(SeedData))]
    public void Preview_equals_resolution(int seed)
    {
        var (s, user, abilityId, target) = Generate(seed);
        var p = AbilityUse.Preview(Rules, s, user, abilityId, target);
        var r = Engine.Apply(s, new UseAbilityCommand(user, abilityId, target), Rules);

        Assert.Equal(r.Error, p.Error);
        if (!r.Ok)
        {
            Assert.Empty(p.Targets);
            return;
        }

        var ability = Rules.Ability(abilityId);
        var events = r.Events;
        Assert.Equal(p.Affected.ToArray(), events.OfType<TargetsLocked>().Single().Targets.ToArray());
        Assert.Equal(p.Affected.ToArray(), p.Targets.Select(t => t.Target).ToArray());
        Assert.Equal(AbilityUse.Affected(Rules, s, user, ability, target).Squares.ToArray(), p.Squares.ToArray());

        foreach (var t in p.Targets)
        {
            Assert.Equal(ability.Damage.ToArray(), t.Parts.ToArray());
            switch (t.Kind)
            {
                case ContestKind.Attack:
                {
                    var rolled = events.OfType<AttackRolled>().Single(e => e.Target == t.Target);
                    Assert.Equal(user, rolled.Attacker);
                    var res = rolled.Result;
                    Assert.Equal(new ContestSetup(res.Total - res.Roll.Kept, res.Target, res.Roll.Mode), t.Setup);
                    Assert.Equal(res, Contests.ResolveAttack(t.Setup!.Value, res.Roll));
                    break;
                }

                case ContestKind.Save:
                {
                    var res = events.OfType<SaveRolled>().Single(e => e.Target == t.Target).Result;
                    Assert.Equal(new ContestSetup(res.Total - res.Roll.Kept, res.Target, res.Roll.Mode), t.Setup);
                    Assert.Equal(res, Contests.ResolveCheck(t.Setup!.Value, res.Roll));
                    break;
                }

                default:
                    Assert.Null(t.Setup);
                    break;
            }

            int at = events.IndexOf(events.OfType<DamageRolled>().SingleOrDefault(e => e.Target == t.Target)!);
            if (at < 0)
            {
                continue;
            }

            var roll = ((DamageRolled)events[at]).Roll;
            Assert.Equal(t.Parts.ToArray(), roll.Parts.Select(x => x.Part).ToArray());
            var dealt = events.Skip(at).OfType<DamageDealt>().First(e => e.Target == t.Target);
            Assert.Equal(Damage.Resolve(Rules, s, t.Target, roll).Total, dealt.Total);
        }
    }

    [Fact]
    public void Preview_odds_are_exact()
    {
        bool resistant = false, weak = false, advantage = false, disadvantage = false, crit = false, save = false;
        int used = 0, refused = 0, previewed = 0;
        for (int seed = 1; seed <= Seeds; seed++)
        {
            var (s, user, abilityId, target) = Generate(seed);
            var p = AbilityUse.Preview(Rules, s, user, abilityId, target);
            used += p.Targets.IsEmpty ? 0 : 1;
            refused += p.Error == CommandError.None ? 0 : 1;
            previewed += p.Targets.Length;
            foreach (var t in p.Targets)
            {
                var (normal, crits, den) = Faces(t);
                Assert.Equal(Ratio(normal + crits, den), t.Chance);
                crit |= crits > 0;
                save |= t.Kind == ContestKind.Save;
                advantage |= t.Setup?.Mode == RollMode.Advantage;
                disadvantage |= t.Setup?.Mode == RollMode.Disadvantage;
                foreach (var part in t.Parts)
                {
                    var step = Ladder.StepFor(Rules, s, t.Target, part.Type);
                    resistant |= step == LadderStep.Resistant;
                    weak |= step == LadderStep.Weak;
                }

                var expected = Ratio(normal, den).Multiply(Mean(s, t, false));
                if (crits > 0)
                {
                    expected = expected.Add(Ratio(crits, den).Multiply(Mean(s, t, true)));
                }

                Assert.Equal(expected, t.ExpectedDamage);
            }
        }

        Assert.True(resistant && weak && advantage && disadvantage && crit && save, $"{resistant} {weak} {advantage} {disadvantage} {crit} {save}");
        Assert.True(used >= 100 && refused >= 30 && previewed >= 130, $"{used} uses with targets, {refused} refused, {previewed} targets");
    }

    // The faces (or face pairs) that land a normal hit or fail the save, those that crit, and the face count.
    private static (long Normal, long Crits, long Den) Faces(TargetPreview t)
    {
        if (t.Kind == ContestKind.Automatic)
        {
            return (1, 0, 1);
        }

        var setup = t.Setup!.Value;
        var rolls = setup.Mode == RollMode.Normal
            ? Enumerable.Range(1, 20).Select(f => new D20Roll(RollMode.Normal, f, null)).ToList()
            : Enumerable.Range(1, 20).SelectMany(a => Enumerable.Range(1, 20).Select(b => new D20Roll(setup.Mode, a, b))).ToList();
        long normal = 0, crits = 0;
        foreach (var roll in rolls)
        {
            if (t.Kind == ContestKind.Attack)
            {
                var res = Contests.ResolveAttack(setup, roll);
                crits += res.Crit ? 1 : 0;
                normal += res.Hit && !res.Crit ? 1 : 0;
            }
            else
            {
                normal += Contests.ResolveCheck(setup, roll).Success ? 0 : 1;
            }
        }

        return (normal, crits, rolls.Count);
    }

    // The mean of Damage.Resolve's total over every dice outcome of every part jointly, by brute force.
    private static Rational Mean(GameState s, TargetPreview t, bool crit)
    {
        if (t.Parts.IsEmpty)
        {
            return Rational.Of(0, 1);
        }

        var faces = t.Parts.Select(part => Outcomes(crit ? 2 * part.Dice : part.Dice, part.Sides)).ToArray();
        long sum = 0, count = 0;
        foreach (var combo in Product(faces, 0))
        {
            var parts = t.Parts.Select((part, i) => new PartRoll(part, combo[i], part.Flat + combo[i].Sum())).ToImmutableArray();
            sum += Damage.Resolve(Rules, s, t.Target, new DamageRoll(parts, crit)).Total;
            count++;
        }

        return Ratio(sum, count);
    }

    private static List<ImmutableArray<int>> Outcomes(int dice, int sides)
    {
        var all = new List<ImmutableArray<int>> { ImmutableArray<int>.Empty };
        for (int d = 0; d < dice; d++)
        {
            all = [.. all.SelectMany(prefix => Enumerable.Range(1, sides).Select(f => prefix.Add(f)))];
        }

        return all;
    }

    private static IEnumerable<ImmutableArray<int>[]> Product(List<ImmutableArray<int>>[] faces, int i)
    {
        if (i == faces.Length)
        {
            yield return new ImmutableArray<int>[faces.Length];
            yield break;
        }

        foreach (var rest in Product(faces, i + 1))
        {
            foreach (var f in faces[i])
            {
                var copy = (ImmutableArray<int>[])rest.Clone();
                copy[i] = f;
                yield return copy;
            }
        }
    }

    private static Rational Ratio(long p, long q) => Rational.Of(p, 1).Multiply(Rational.Of(1, q));
}
