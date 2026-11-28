using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Damage;
using Dungeon.Core.Effects;
using Dungeon.Core.Stats;
using static Dungeon.Core.Tests.Fixtures;

namespace Dungeon.Core.Tests;

/// <summary>
/// The ability fixtures shared by the M1-11 tests (Tasks 4 to 8 and <c>CombatScenarioTests</c>): code-built definitions,
/// the pools they spend, a few effects that fix a contest's outcome, and builders for units that hold every fixture
/// ability.
/// </summary>
public static class AbilityFixtures
{
    /// <summary>The file name code-built fixture definitions carry.</summary>
    public const string File = "test/abilities";

    /// <summary>A melee strike: single, range 1, a Strength attack, 1d6 slashing.</summary>
    public const string Strike = "test.strike";

    /// <summary>A sure strike: single, range 1, no roll, 1d4+1 bludgeoning (always at least 2).</summary>
    public const string Sure = "test.sure";

    /// <summary>A bonus-action jab: single, range 1, no roll, a flat 1.</summary>
    public const string Jab = "test.jab";

    /// <summary>A <c>move:2</c> dash-strike: single, range 1, no roll, a flat 1.</summary>
    public const string Dash = "test.dash";

    /// <summary>A free poke: single, range 1, no roll, a flat 1.</summary>
    public const string Poke = "test.poke";

    /// <summary>A camp-pool burst: burst range 2, radius 1, no roll, a flat 3 fire; spends 1 of <see cref="CampPool"/>.</summary>
    public const string CampBurst = "test.camp_burst";

    /// <summary>A save burst: burst range 6, radius 1, a reflex save, 1d6 fire.</summary>
    public const string SaveBurst = "test.save_burst";

    /// <summary>As <see cref="SaveBurst"/>, tagged <c>selective</c>.</summary>
    public const string SelectiveBurst = "test.selective_burst";

    /// <summary>An at-will cone: length 2, a reflex save, 1d4 cold.</summary>
    public const string AtWillCone = "test.cone";

    /// <summary>A line: length 4, a reflex save, 1d4 lightning.</summary>
    public const string SaveLine = "test.line";

    /// <summary>
    /// A ward: a reaction on the holder's own save, +2, policy <c>never</c>. Every <see cref="Party"/> and
    /// <see cref="Foe"/> unit holds it, so it never fires by default and resolution stays reaction-free unless a test
    /// sets a policy or gives a unit one of the <see cref="Wards"/>.
    /// </summary>
    public const string Ward = "test.ward";

    /// <summary>A shield ward (Task 7): a reaction on an attack roll against the holder, +2 to the target, always.</summary>
    public const string ShieldWard = "ward.shield";

    /// <summary>As <see cref="ShieldWard"/>, spending 1 of <c>pool.turn</c>.</summary>
    public const string TurnWard = "ward.turn";

    /// <summary>As <see cref="ShieldWard"/>, spending 1 of <see cref="CampPool"/>.</summary>
    public const string CampWard = "ward.camp";

    /// <summary>A save ward (Task 7): a reaction on the holder's own save, +2 to the bonus, always.</summary>
    public const string SaveWard = "ward.save";

    /// <summary>A camp pool with 3 uses, so a second spend in a turn meets the camp rule, not an empty pool.</summary>
    public const string CampPool = "pool.camp";

    /// <summary>Reflex −50: every reflex save fails.</summary>
    public const string Clumsy = "test.clumsy";

    /// <summary>Reflex +50: every reflex save succeeds.</summary>
    public const string Nimble = "test.nimble";

    /// <summary>Armour class −50: every attack hits but a natural 1.</summary>
    public const string Exposed = "test.exposed";

    /// <summary>The fixture pools: <see cref="CampPool"/> (3), <c>pool.fight</c> (1) and <c>pool.turn</c> (1).</summary>
    public static readonly ImmutableArray<PoolDefinition> PoolTable =
    [
        new(CampPool, RefillTag.Camp, _ => 3),
        new("pool.fight", RefillTag.Fight, _ => 1),
        new("pool.turn", RefillTag.Turn, _ => 1),
    ];

    /// <summary>Every fixture definition.</summary>
    public static readonly ImmutableArray<AbilityDefinition> Definitions =
    [
        Single(Strike, AbilityKind.AtWill, new AbilityCost(CostKind.Action), new AttackResolve(new AttackSpec(Ability.Strength, true)), Part(DamageType.Slashing, 1, 6, 0)),
        Single(Sure, AbilityKind.AtWill, new AbilityCost(CostKind.Action), null, Part(DamageType.Bludgeoning, 1, 4, 1)),
        Single(Jab, AbilityKind.AtWill, new AbilityCost(CostKind.Bonus), null, Part(DamageType.Bludgeoning, 0, 0, 1)),
        Single(Dash, AbilityKind.AtWill, new AbilityCost(CostKind.Move, 2), null, Part(DamageType.Bludgeoning, 0, 0, 1)),
        Single(Poke, AbilityKind.AtWill, new AbilityCost(CostKind.Free), null, Part(DamageType.Bludgeoning, 0, 0, 1)),
        Area(CampBurst, AbilityKind.Limited, new PoolUse(CampPool, 1), new TargetSpec(TargetShape.Burst, 2, 0, 1, false), null, Part(DamageType.Fire, 0, 0, 3)),
        Area(SaveBurst, AbilityKind.Limited, null, new TargetSpec(TargetShape.Burst, 6, 0, 1, false), new SaveResolve(Save.Reflex), Part(DamageType.Fire, 1, 6, 0)),
        Area(SelectiveBurst, AbilityKind.Limited, null, new TargetSpec(TargetShape.Burst, 6, 0, 1, true), new SaveResolve(Save.Reflex), Part(DamageType.Fire, 1, 6, 0)),
        Area(AtWillCone, AbilityKind.AtWill, null, new TargetSpec(TargetShape.Cone, 0, 0, 2, false), new SaveResolve(Save.Reflex), Part(DamageType.Cold, 1, 4, 0)),
        Area(SaveLine, AbilityKind.Limited, null, new TargetSpec(TargetShape.Line, 0, 0, 4, false), new SaveResolve(Save.Reflex), Part(DamageType.Lightning, 1, 4, 0)),
        new AbilityDefinition(
            File, Ward, AbilityKind.Reaction, new AbilityCost(CostKind.Reaction), null, null, null, [], [],
            new ReactionSpec(ReactionTrigger.OwnSave, 2, ReactionMode.Never)),
    ];

    /// <summary>
    /// The Task 7 wards: <see cref="ShieldWard"/>, <see cref="TurnWard"/>, <see cref="CampWard"/> and
    /// <see cref="SaveWard"/>. They are in <see cref="Rules"/> but not in <see cref="Definitions"/>, so no
    /// <see cref="Party"/> or <see cref="Foe"/> unit holds one unless a test gives it.
    /// </summary>
    public static readonly ImmutableArray<AbilityDefinition> Wards =
    [
        Reaction(ShieldWard, ReactionTrigger.AttackRoll, null),
        Reaction(TurnWard, ReactionTrigger.AttackRoll, new PoolUse("pool.turn", 1)),
        Reaction(CampWard, ReactionTrigger.AttackRoll, new PoolUse(CampPool, 1)),
        Reaction(SaveWard, ReactionTrigger.OwnSave, null),
    ];

    /// <summary>Every fixture ability id, ordinal: what <see cref="Party"/> and <see cref="Foe"/> units hold.</summary>
    public static readonly ImmutableArray<string> AllIds =
        [.. Definitions.Select(d => d.Id).OrderBy(id => id, StringComparer.Ordinal)];

    /// <summary>The fixture effects, the core registries, <see cref="Pools"/>, <see cref="Definitions"/> and <see cref="Wards"/>.</summary>
    public static readonly RulesContext Rules = RulesContext.Create(
        ContestKit.LoadEffects(
            ContestKit.Def(Clumsy, "condition", ContestKit.Add("reflex", -50)),
            ContestKit.Def(Exposed, "condition", ContestKit.Add("armourClass", -50)),
            ContestKit.Def(Nimble, "condition", ContestKit.Add("reflex", 50))),
        pools: PoolTable,
        abilities: [.. Definitions, .. Wards]);

    /// <summary>A party unit on <see cref="Fixtures.Stats"/>, full HP, holding every fixture ability.</summary>
    public static Unit Party(int id, int x, int y, int hp = FullHp) => U(id, x, y) with { Hp = hp, Abilities = AllIds };

    /// <summary>An enemy on <see cref="Fixtures.EnemyStats"/> (table HP 12), holding every fixture ability.</summary>
    public static Unit Foe(int id, int x, int y, int hp = 12) =>
        new(new UnitId(id), new GridPos(x, y), Faction.Enemy, [], EnemyStats, hp) { Abilities = AllIds };

    /// <summary><paramref name="s"/> in round 1 of a fight in the given order, slot 0 holding the turn.</summary>
    public static GameState InCombat(GameState s, params int[] order) =>
        s with { Combat = new CombatState([.. order.Select(i => new UnitId(i))], 0, 1) };

    /// <summary><paramref name="s"/> with <paramref name="effect"/> on <paramref name="unit"/>, from itself.</summary>
    public static GameState With(GameState s, int unit, string effect) =>
        Dungeon.Core.Effects.Effects.Apply(s, effect, new UnitId(unit), new EffectSource(new UnitId(unit)));

    /// <summary>A reaction: cost <c>reaction</c>, no target, +2 on <paramref name="trigger"/>, <paramref name="policy"/> by default.</summary>
    public static AbilityDefinition Reaction(
        string id, ReactionTrigger trigger, PoolUse? pool, ReactionMode policy = ReactionMode.Always, int bonus = 2) =>
        new(File, id, AbilityKind.Reaction, new AbilityCost(CostKind.Reaction), pool, null, null, [], [],
            new ReactionSpec(trigger, bonus, policy));

    // --- M1-12a Task 1: the held events of a suspended use ---

    /// <summary>A two-target strike: single, range 1, up to 2 units, a Strength attack, 1d6 slashing.</summary>
    public const string TwoStrike = "test.two_strike";

    /// <summary>An attack-roll ward, +2, that the held-events tests set to "ask me", "never" or "always".</summary>
    public const string AskWard = "ward.ask";

    /// <summary>A second attack-roll ward, after <see cref="AskWard"/> by id, for a re-ask.</summary>
    public const string AskWard2 = "ward.ask_2";

    /// <summary>A trait whose <c>unitDied</c> trigger (scope any) applies <see cref="ResistSlashing"/> to its holder.</summary>
    public const string Vengeful = "test.vengeful";

    /// <summary>A trait with the <c>resistant.slashing</c> flag.</summary>
    public const string ResistSlashing = "test.resist_slashing";

    /// <summary>A trait whose <c>combatStarted</c> trigger (scope any) applies <see cref="Rallied"/> to its holder.</summary>
    public const string Rally = "test.rally";

    /// <summary>A plain trait, the output of <see cref="Rally"/>.</summary>
    public const string Rallied = "test.rallied";

    /// <summary>
    /// The held-events rules: <see cref="Rules"/>'s effects with <see cref="Vengeful"/>, <see cref="ResistSlashing"/>,
    /// <see cref="Rally"/> and <see cref="Rallied"/>; its pools and abilities with <see cref="TwoStrike"/>,
    /// <see cref="AskWard"/> and <see cref="AskWard2"/>.
    /// </summary>
    public static readonly RulesContext HeldRules = RulesContext.Create(
        ContestKit.LoadEffects(
            ContestKit.Def(Clumsy, "condition", ContestKit.Add("reflex", -50)),
            ContestKit.Def(Exposed, "condition", ContestKit.Add("armourClass", -50)),
            ContestKit.Def(Nimble, "condition", ContestKit.Add("reflex", 50)),
            ContestKit.Def(ResistSlashing, "trait", """{"target": "unit", "flag": "resistant.slashing"}"""),
            ContestKit.Def(Rallied, "trait"),
            $$"""{"id": "{{Vengeful}}", "kind": "trait", "triggers": [{"on": "unitDied", "scope": "any", "do": [{"applyEffect": "{{ResistSlashing}}", "to": "holder"}]}]}""",
            $$"""{"id": "{{Rally}}", "kind": "trait", "triggers": [{"on": "combatStarted", "scope": "any", "do": [{"applyEffect": "{{Rallied}}", "to": "holder"}]}]}"""),
        pools: PoolTable,
        abilities:
        [
            .. Definitions,
            .. Wards,
            Reaction(AskWard, ReactionTrigger.AttackRoll, null),
            Reaction(AskWard2, ReactionTrigger.AttackRoll, null),
            new AbilityDefinition(
                File, TwoStrike, AbilityKind.AtWill, new AbilityCost(CostKind.Action), null,
                new TargetSpec(TargetShape.Single, 1, 2, 0, false), new AttackResolve(new AttackSpec(Ability.Strength, true)),
                [Part(DamageType.Slashing, 1, 6, 0)], [], null),
        ]);

    private static DamagePart Part(DamageType type, int dice, int sides, int flat) => new(type, dice, sides, flat);

    private static AbilityDefinition Single(string id, AbilityKind kind, AbilityCost cost, ResolveSpec? resolve, DamagePart damage) =>
        new(File, id, kind, cost, null, new TargetSpec(TargetShape.Single, 1, 1, 0, false), resolve, [damage], [], null);

    private static AbilityDefinition Area(
        string id, AbilityKind kind, PoolUse? pool, TargetSpec target, ResolveSpec? resolve, DamagePart damage) =>
        new(File, id, kind, new AbilityCost(CostKind.Action), pool, target, resolve, [damage], [], null);
}
