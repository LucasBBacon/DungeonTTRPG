using System.Collections.Immutable;
using Dungeon.Core.Canonical;
using Dungeon.Core.Content;
using Dungeon.Core.Damage;
using Dungeon.Core.Stats;

namespace Dungeon.Core.AbilityModel;

/// <summary>An ability's kind (AB 6.1). M1 content accepts <see cref="AtWill"/>, <see cref="Limited"/> and <see cref="Reaction"/>; the rest parse and are refused at load.</summary>
public enum AbilityKind
{
    /// <summary>Usable every turn at no pool cost.</summary>
    AtWill,

    /// <summary>Spends a pool (AB 6.2).</summary>
    Limited,

    /// <summary>Always on; reserved.</summary>
    Passive,

    /// <summary>Switched on and off; reserved.</summary>
    Toggle,

    /// <summary>Held until ended; reserved.</summary>
    Sustained,

    /// <summary>Used out of turn at a named point (AB 7.3).</summary>
    Reaction,
}

/// <summary>The cost forms of AB 7.1. M1 content accepts <see cref="Action"/>, <see cref="Bonus"/>, <see cref="Free"/>, <see cref="Move"/>, <see cref="None"/> and <see cref="Reaction"/>.</summary>
public enum CostKind
{
    /// <summary>The action.</summary>
    Action,

    /// <summary>The bonus action.</summary>
    Bonus,

    /// <summary>The reaction.</summary>
    Reaction,

    /// <summary>The attack cost; reserved to M1-14.</summary>
    Attack,

    /// <summary><c>move:N</c>: N squares of movement.</summary>
    Move,

    /// <summary>The free interaction.</summary>
    Free,

    /// <summary>On a hit; reserved to M1-16.</summary>
    OnHit,

    /// <summary><c>onEvent:&lt;id&gt;</c>; reserved to M1-20.</summary>
    OnEvent,

    /// <summary>No cost.</summary>
    None,
}

/// <summary>
/// An ability's one cost (AB 7.1). <paramref name="Move"/> is N for <see cref="CostKind.Move"/> and 0 otherwise;
/// <paramref name="Event"/> is the event id for <see cref="CostKind.OnEvent"/> and null otherwise.
/// </summary>
public readonly record struct AbilityCost(CostKind Kind, int Move = 0, string? Event = null) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(3);
        w.Enum(0, Kind);
        w.Int32(1, Move);
        w.OptionalString(2, Event);
        w.EndRecord();
    }
}

/// <summary>A pool spend, separate from the cost (AB 7.1): <paramref name="Count"/> (at least 1) of <paramref name="PoolId"/>.</summary>
public sealed record PoolUse(string PoolId, int Count) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(2);
        w.String(0, PoolId);
        w.Int32(1, Count);
        w.EndRecord();
    }
}

/// <summary>The shape of an ability's targeting (AB 8.1).</summary>
public enum TargetShape
{
    /// <summary>One or more units, each named.</summary>
    Single,

    /// <summary>Every square within a radius of a point.</summary>
    Burst,

    /// <summary>A cone from the user.</summary>
    Cone,

    /// <summary>A line from the user.</summary>
    Line,
}

/// <summary>
/// An ability's targeting. <paramref name="Size"/> is the radius of a burst or the length of a cone or line (0 for
/// <see cref="TargetShape.Single"/>); <paramref name="Count"/> applies only to <see cref="TargetShape.Single"/> (0
/// otherwise); <paramref name="Range"/> applies to <see cref="TargetShape.Single"/> and <see cref="TargetShape.Burst"/>
/// (0 otherwise, and a burst of range 0 is centred on the user). <paramref name="Selective"/> is the AB 8.2 tag.
/// </summary>
public sealed record TargetSpec(TargetShape Shape, int Range, int Count, int Size, bool Selective) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(5);
        w.Enum(0, Shape);
        w.Int32(1, Range);
        w.Int32(2, Count);
        w.Int32(3, Size);
        w.Bool(4, Selective);
        w.EndRecord();
    }
}

/// <summary>How an ability resolves against each target: an attack roll or a save.</summary>
public abstract record ResolveSpec : ICanonical
{
    /// <inheritdoc />
    public abstract void WriteCanonical(CanonicalWriter w);
}

/// <summary>An attack roll with <paramref name="Attack"/>'s ability and training.</summary>
public sealed record AttackResolve(AttackSpec Attack) : ResolveSpec
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(3);
        w.String(0, "attack");
        w.Enum(1, Attack.Ability);
        w.Bool(2, Attack.Trained);
        w.EndRecord();
    }
}

/// <summary>
/// A save by the target. A success means no effect (owner ruling 4). <c>onSuccess: half</c> is ruled (D-22, M1-11 rulings
/// record 1) and built in the D-22 step; written to the canonical form only when not <c>none</c> (lead's call).
/// </summary>
public sealed record SaveResolve(Save Save) : ResolveSpec
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(2);
        w.String(0, "save");
        w.Enum(1, Save);
        w.EndRecord();
    }
}

/// <summary>How a reaction decides on its own (AB 7.3). <see cref="AskMe"/> is the party's prompt, never an automatic policy.</summary>
public enum ReactionMode
{
    /// <summary>Always reacts.</summary>
    Always,

    /// <summary>Reacts only when the bonus changes the outcome.</summary>
    IfItChangesOutcome,

    /// <summary>Reacts while the pool's remaining is at least ceil(max / 2).</summary>
    WhilePoolAtLeastHalf,

    /// <summary>Never reacts.</summary>
    Never,

    /// <summary>Asks the player; not an automatic policy.</summary>
    AskMe,
}

/// <summary>The named points reactions hook into (AB 14.3). M1-11 has the two of AB 7.3's rule text.</summary>
public enum ReactionTrigger
{
    /// <summary>Between an attack roll against the holder and the hit being decided.</summary>
    AttackRoll,

    /// <summary>Between the holder's own save roll and its result.</summary>
    OwnSave,
}

/// <summary>A reaction's trigger, its roll <paramref name="Bonus"/> and its default <paramref name="Policy"/> (null if the record gives none).</summary>
public sealed record ReactionSpec(ReactionTrigger Trigger, int Bonus, ReactionMode? Policy) : ICanonical
{
    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(3);
        w.Enum(0, Trigger);
        w.Int32(1, Bonus);
        w.OptionalString(2, Policy?.ToString());
        w.EndRecord();
    }
}

/// <summary>
/// An ability record (AB 6.1, 7.1, 14.3) from <c>abilities/abilities.json</c>, or built in code (W 20.1's attack ability
/// is derived, never stored). <see cref="Target"/> is required unless <see cref="Kind"/> is
/// <see cref="AbilityKind.Reaction"/>, and null for one; <see cref="Reaction"/> is set exactly for a reaction.
/// </summary>
public sealed record AbilityDefinition(
    string File,
    string Id,
    AbilityKind Kind,
    AbilityCost Cost,
    PoolUse? Pool,
    TargetSpec? Target,
    ResolveSpec? Resolve,
    ImmutableArray<DamagePart> Damage,
    ImmutableArray<string> Effects,
    ReactionSpec? Reaction) : ContentRecord(File, Id)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(9);
        w.String(0, Id);
        w.Enum(1, Kind);
        w.Record(2, Cost);
        w.OptionalRecord(3, Pool);
        w.OptionalRecord(4, Target);
        w.OptionalRecord(5, Resolve);
        w.List(6, Damage, (iw, d) => iw.Record(0, new DamageBytes(d)));
        w.List(7, Effects, (iw, e) => iw.String(0, e));
        w.OptionalRecord(8, Reaction);
        w.EndRecord();
    }

    private readonly record struct DamageBytes(DamagePart Part) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(6);
            w.Enum(0, Part.Type);
            w.Int32(1, Part.Dice);
            w.Int32(2, Part.Sides);
            w.Int32(3, Part.Flat);
            w.OptionalString(4, Part.Trap?.TrapId);
            w.OptionalString(5, Part.Trap?.DetectionAtFire.ToString());
            w.EndRecord();
        }
    }
}
