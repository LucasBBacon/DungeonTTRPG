using System;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Effects;

/// <summary>
/// What the fold queries and the engine read the effect rules from: the effect definitions of the content, and the code
/// registries (derived numbers, roll targets, flags, rule events and hooks; M1-07 plan call 5). Building one checks every
/// reference the content makes into the registries, so a misspelt id fails at startup with the file and field named
/// (MAIN 6.5), never in play.
/// </summary>
public sealed class RulesContext
{
    // Every array is ordinal by id, for binary search (DS4).
    private readonly ImmutableArray<EffectDefinition> _definitions;
    private readonly ImmutableArray<DerivedNumber> _numbers;
    private readonly ImmutableArray<string> _rollTargets;
    private readonly ImmutableArray<string> _flagTargets;
    private readonly ImmutableArray<string> _flags;
    private readonly ImmutableArray<RuleEvent> _events;
    private readonly ImmutableArray<PoolDefinition> _pools;
    private readonly ImmutableArray<AbilityDefinition> _abilities;

    private RulesContext(
        ImmutableArray<EffectDefinition> definitions,
        ImmutableArray<DerivedNumber> numbers,
        ImmutableArray<string> rollTargets,
        ImmutableArray<string> flags,
        ImmutableArray<RuleEvent> events,
        HookRegistry hooks,
        ImmutableArray<PoolDefinition> pools,
        ImmutableArray<AbilityDefinition> abilities)
    {
        _definitions = definitions;
        _numbers = numbers;
        _rollTargets = rollTargets;
        _flagTargets = SortedIds(Flags.Targets, nameof(Flags.Targets));
        _flags = flags;
        _events = events;
        Hooks = hooks;
        _pools = pools;
        _abilities = abilities;
    }

    /// <summary>
    /// No effect definitions, the core registries and no pools. The engine runs with it when no rules are given.
    /// </summary>
    public static RulesContext Empty { get; } = new(
        ImmutableArray<EffectDefinition>.Empty,
        SortedNumbers(DerivedNumbers.Core),
        SortedIds(RollTargets.Core, "rollTargets"),
        SortedIds(Flags.Core, "flags"),
        SortedEvents(RuleEvents.Core),
        HookRegistry.Core,
        ImmutableArray<PoolDefinition>.Empty,
        ImmutableArray<AbilityDefinition>.Empty);

    /// <summary>
    /// Builds the context for <paramref name="content"/>'s effect definitions. Each registry defaults to its core
    /// table (<see cref="DerivedNumbers.Core"/>, <see cref="RollTargets.Core"/>, <see cref="RuleEvents.Core"/>,
    /// <see cref="HookRegistry.Core"/>, <see cref="Flags.Core"/>); tests pass their own. The pool table (M1-10 plan
    /// call 7) defaults to none until the pool content records arrive (M1-19). Throws
    /// <see cref="ContentLoadException"/> naming the definition's file and path when an <c>add</c>, <c>halve</c> or
    /// <c>zero</c> target names no derived number, a <c>halve</c> or <c>zero</c> targets a number whose rule is not
    /// <see cref="NumberRule.Speed"/>, a <c>rollMode</c> target names no roll target, a <c>flag</c> target is not one of
    /// <see cref="Flags.Targets"/> or its flag names no registered flag, a trigger's <c>on</c> names no event, or an
    /// action names no registered hook, or a <c>unitDied</c> trigger gives an effect <c>"to": "subject"</c> (the subject
    /// has been removed). Throws <see cref="ArgumentException"/> if a registry repeats an id, or a derived
    /// number has no base, has a cap but is not an <see cref="NumberRule.AbilityScore"/> number, or has a floor but is an
    /// <see cref="NumberRule.AbilityScore"/> or <see cref="NumberRule.Speed"/> number, or a pool is null, has an id
    /// that is not a content id, an undefined refill tag or no maximum, or repeats an id.
    /// The ability table (M1-11 Task 3) is the content's ability records plus the code-built <paramref name="abilities"/>,
    /// ordinal by id; <see cref="ArgumentException"/> if a definition is null or an id repeats, and
    /// <see cref="ContentLoadException"/> naming the definition's file and path when a pool or an effect it names does not exist.
    /// </summary>
    public static RulesContext Create(
        ContentSet content,
        ImmutableArray<DerivedNumber>? numbers = null,
        ImmutableArray<string>? rollTargets = null,
        ImmutableArray<RuleEvent>? events = null,
        HookRegistry? hooks = null,
        ImmutableArray<string>? flags = null,
        ImmutableArray<PoolDefinition>? pools = null,
        ImmutableArray<AbilityDefinition>? abilities = null)
    {
        ArgumentNullException.ThrowIfNull(content);
        var context = new RulesContext(
            content.All<EffectDefinition>(),
            SortedNumbers(numbers ?? DerivedNumbers.Core),
            SortedIds(rollTargets ?? RollTargets.Core, nameof(rollTargets)),
            SortedIds(flags ?? Flags.Core, nameof(flags)),
            SortedEvents(events ?? RuleEvents.Core),
            hooks ?? HookRegistry.Core,
            SortedPools(pools ?? ImmutableArray<PoolDefinition>.Empty),
            SortedAbilities(content.All<AbilityDefinition>(), abilities ?? ImmutableArray<AbilityDefinition>.Empty));
        context.Validate(content);
        return context;
    }

    /// <summary>The effect definition <paramref name="id"/>. Throws <see cref="ArgumentException"/> if there is none.</summary>
    public EffectDefinition Definition(string id) =>
        Find(_definitions, id, d => d.Id) ?? throw new ArgumentException($"No effect definition \"{id}\".", nameof(id));

    /// <summary>The ability <paramref name="id"/>. Throws <see cref="ArgumentException"/> if there is none.</summary>
    public AbilityDefinition Ability(string id) =>
        Find(_abilities, id, a => a.Id) ?? throw new ArgumentException($"No ability \"{id}\".", nameof(id));

    /// <summary>The ability table, ordinal by id.</summary>
    public ImmutableArray<AbilityDefinition> Abilities => _abilities;

    /// <summary>The derived number <paramref name="id"/>. Throws <see cref="ArgumentException"/> if there is none.</summary>
    public DerivedNumber Number(string id) =>
        Find(_numbers, id, n => n.Id) ?? throw new ArgumentException($"No derived number \"{id}\".", nameof(id));

    /// <summary>The pool <paramref name="id"/>. Throws <see cref="ArgumentException"/> if there is none.</summary>
    public PoolDefinition Pool(string id) =>
        FindPool(id) ?? throw new ArgumentException($"No pool \"{id}\".", nameof(id));

    /// <summary>The pool table, ordinal by id.</summary>
    public ImmutableArray<PoolDefinition> Pools => _pools;

    /// <summary>True if <paramref name="id"/> is a registered roll target.</summary>
    public bool IsRollTarget(string id) => Contains(_rollTargets, id);

    /// <summary>True if <paramref name="target"/> is a flag target and <paramref name="name"/> a registered flag.</summary>
    public bool IsFlag(string target, string name) => Contains(_flagTargets, target) && Contains(_flags, name);

    /// <summary>The ability <paramref name="id"/>, or null if there is none.</summary>
    internal AbilityDefinition? FindAbility(string id) => Find(_abilities, id, a => a.Id);

    /// <summary>The pool <paramref name="id"/>, or null if there is none.</summary>
    internal PoolDefinition? FindPool(string id) => Find(_pools, id, p => p.Id);

    /// <summary>The hooks content may name.</summary>
    internal HookRegistry Hooks { get; }

    /// <summary>The registered rule events <paramref name="e"/> raises, ordinal by id (most often none or one).</summary>
    internal ImmutableArray<RuleEvent> EventsRaisedBy(GameEvent e)
    {
        var raised = ImmutableArray<RuleEvent>.Empty;
        foreach (var ruleEvent in _events)
        {
            if (ruleEvent.EventType == e.GetType())
            {
                raised = raised.Add(ruleEvent);
            }
        }

        return raised;
    }

    private void Validate(ContentSet content)
    {
        foreach (string file in content.KindFiles)
        {
            var records = content.RecordsIn(file);
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] is EffectDefinition definition)
                {
                    ValidateDefinition(definition, Item("$.effects", i));
                }
            }
        }

        // Content abilities name their JSON path by file position; code-built ones by id.
        foreach (var ability in _abilities)
        {
            ValidateAbility(content, ability);
        }
    }

    private void ValidateAbility(ContentSet content, AbilityDefinition ability)
    {
        int index = IndexIn(content, ability);
        string at = index >= 0 ? Item("$.abilities", index) : $"$.abilities[{ability.Id}]";

        // The loader's structural rules, for code-built definitions too (M1-11 final review C5).
        if (AbilityRules.Check(ability, at) is { } broken)
        {
            throw new ContentLoadException(ability.File, broken.Field, broken.Problem);
        }

        if (ability.Pool is { } use && FindPool(use.PoolId) is null)
        {
            throw new ContentLoadException(ability.File, Member(Member(at, "pool"), "id"), $"\"{use.PoolId}\" names no pool.");
        }

        // The preview's exactness bound (plan call 14), for code-built definitions too.
        if (AbilityUse.CheckPreviewable(ability.Damage) is { } bad)
        {
            string damage = Member(at, "damage");
            throw new ContentLoadException(
                ability.File, bad.Part < 0 ? damage : Member(Item(damage, bad.Part), bad.Field), bad.Problem);
        }

        for (int e = 0; e < ability.Effects.Length; e++)
        {
            if (Find(_definitions, ability.Effects[e], d => d.Id) is null)
            {
                throw new ContentLoadException(
                    ability.File, Item(Member(at, "effects"), e), $"\"{ability.Effects[e]}\" names no effect.");
            }
        }
    }

    // The record's position in its own file, or -1 for a code-built definition, whose path then names the id.
    private static int IndexIn(ContentSet content, AbilityDefinition ability)
    {
        var records = content.RecordsIn(ability.File);
        for (int i = 0; i < records.Length; i++)
        {
            if (ReferenceEquals(records[i], ability))
            {
                return i;
            }
        }

        return -1;
    }

    private void ValidateDefinition(EffectDefinition definition, string at)
    {
        for (int j = 0; j < definition.Modifiers.Length; j++)
        {
            var m = definition.Modifiers[j];
            string field = Item(Member(at, "modifiers"), j);
            switch (m.Op)
            {
                case ModifierOp.Add or ModifierOp.Halve or ModifierOp.Zero:
                    var number = Find(_numbers, m.Target, n => n.Id)
                        ?? throw new ContentLoadException(
                            definition.File, Member(field, "target"), $"\"{m.Target}\" names no derived number.");
                    if (m.Op != ModifierOp.Add && number.Rule != NumberRule.Speed)
                    {
                        string op = m.Op == ModifierOp.Halve ? "halve" : "zero";
                        throw new ContentLoadException(
                            definition.File, Member(field, op), $"{op} applies only to a speed-rule number; \"{m.Target}\" is not one.");
                    }

                    break;
                case ModifierOp.RollMode:
                    if (!IsRollTarget(m.Target))
                    {
                        throw new ContentLoadException(
                            definition.File, Member(field, "target"), $"\"{m.Target}\" names no roll target.");
                    }

                    break;
                case ModifierOp.Flag:
                    if (!Contains(_flagTargets, m.Target))
                    {
                        throw new ContentLoadException(
                            definition.File, Member(field, "target"), $"\"{m.Target}\" is not a flag target (\"unit\").");
                    }

                    if (m.FlagName is not { } flag || !Contains(_flags, flag))
                    {
                        throw new ContentLoadException(
                            definition.File, Member(field, "flag"), $"\"{m.FlagName}\" names no flag.");
                    }

                    break;
            }
        }

        for (int t = 0; t < definition.Triggers.Length; t++)
        {
            var trigger = definition.Triggers[t];
            string field = Item(Member(at, "triggers"), t);
            if (Find(_events, trigger.On, e => e.Id) is null)
            {
                throw new ContentLoadException(definition.File, Member(field, "on"), $"\"{trigger.On}\" names no rule event.");
            }

            for (int a = 0; a < trigger.Do.Length; a++)
            {
                var action = trigger.Do[a];
                if (action.Hook is { } hook && !Hooks.Contains(hook))
                {
                    throw new ContentLoadException(
                        definition.File, Member(Item(Member(field, "do"), a), "hook"), $"\"{hook}\" names no hook.");
                }

                // unitDied is raised after its subject has been removed, so no effect can be given to it.
                if (action.ApplyEffect is not null && action.To == TriggerTarget.Subject
                    && string.Equals(trigger.On, "unitDied", StringComparison.Ordinal))
                {
                    throw new ContentLoadException(
                        definition.File,
                        Member(Item(Member(field, "do"), a), "to"),
                        "a unitDied trigger cannot give an effect to its subject, which has been removed.");
                }
            }
        }
    }

    private static ImmutableArray<DerivedNumber> SortedNumbers(ImmutableArray<DerivedNumber> numbers)
    {
        var sorted = numbers
            .Select(n => n ?? throw new ArgumentException("A derived number is null.", nameof(numbers)))
            .Select(n => n.Base is not null && n.Id is not null
                ? n
                : throw new ArgumentException("A derived number has no id or no base.", nameof(numbers)))
            .Select(n => n.Cap is null || n.Rule == NumberRule.AbilityScore
                ? n
                : throw new ArgumentException(
                    $"Derived number \"{n.Id}\" has a cap, but only an {nameof(NumberRule.AbilityScore)} number may have one.",
                    nameof(numbers)))
            .Select(n => n.Floor is null || n.Rule == NumberRule.Stack
                ? n
                : throw new ArgumentException(
                    $"Derived number \"{n.Id}\" has a floor, but an {n.Rule} number may not have one.",
                    nameof(numbers)))
            .OrderBy(n => n.Id, StringComparer.Ordinal)
            .ToImmutableArray();
        for (int i = 1; i < sorted.Length; i++)
        {
            if (string.Equals(sorted[i - 1].Id, sorted[i].Id, StringComparison.Ordinal))
            {
                throw new ArgumentException($"Derived number \"{sorted[i].Id}\" is registered twice.", nameof(numbers));
            }
        }

        return sorted;
    }

    private static ImmutableArray<PoolDefinition> SortedPools(ImmutableArray<PoolDefinition> pools)
    {
        var sorted = pools
            .Select(p => p ?? throw new ArgumentException("A pool is null.", nameof(pools)))
            .Select(p => ContentId.IsValid(p.Id)
                ? p
                : throw new ArgumentException($"Pool id \"{p.Id}\" is not a content id ([a-z0-9_.:-]).", nameof(pools)))
            .Select(p => Enum.IsDefined(p.Refill)
                ? p
                : throw new ArgumentException($"Pool \"{p.Id}\" has an undefined refill tag.", nameof(pools)))
            .Select(p => p.Max is not null
                ? p
                : throw new ArgumentException($"Pool \"{p.Id}\" has no maximum.", nameof(pools)))
            .OrderBy(p => p.Id, StringComparer.Ordinal)
            .ToImmutableArray();
        for (int i = 1; i < sorted.Length; i++)
        {
            if (string.Equals(sorted[i - 1].Id, sorted[i].Id, StringComparison.Ordinal))
            {
                throw new ArgumentException($"Pool \"{sorted[i].Id}\" is registered twice.", nameof(pools));
            }
        }

        return sorted;
    }

    private static ImmutableArray<AbilityDefinition> SortedAbilities(
        ImmutableArray<AbilityDefinition> fromContent, ImmutableArray<AbilityDefinition> built)
    {
        var sorted = fromContent
            .Concat(built.Select(a => a ?? throw new ArgumentException("An ability is null.", nameof(built))))
            .OrderBy(a => a.Id, StringComparer.Ordinal)
            .ToImmutableArray();
        for (int i = 1; i < sorted.Length; i++)
        {
            if (string.Equals(sorted[i - 1].Id, sorted[i].Id, StringComparison.Ordinal))
            {
                throw new ArgumentException($"Ability \"{sorted[i].Id}\" is registered twice.", nameof(built));
            }
        }

        return sorted;
    }

    private static ImmutableArray<RuleEvent> SortedEvents(ImmutableArray<RuleEvent> events)
    {
        var sorted = events
            .Select(e => e ?? throw new ArgumentException("A rule event is null.", nameof(events)))
            .OrderBy(e => e.Id, StringComparer.Ordinal)
            .ToImmutableArray();
        for (int i = 1; i < sorted.Length; i++)
        {
            if (string.Equals(sorted[i - 1].Id, sorted[i].Id, StringComparison.Ordinal))
            {
                throw new ArgumentException($"Rule event \"{sorted[i].Id}\" is registered twice.", nameof(events));
            }
        }

        return sorted;
    }

    private static ImmutableArray<string> SortedIds(ImmutableArray<string> ids, string name)
    {
        var sorted = ids
            .Select(id => id ?? throw new ArgumentException("An id is null.", name))
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToImmutableArray();
        for (int i = 1; i < sorted.Length; i++)
        {
            if (string.Equals(sorted[i - 1], sorted[i], StringComparison.Ordinal))
            {
                throw new ArgumentException($"\"{sorted[i]}\" is registered twice.", name);
            }
        }

        return sorted;
    }

    private static bool Contains(ImmutableArray<string> sorted, string id) =>
        ImmutableArray.BinarySearch(sorted, id, StringComparer.Ordinal) >= 0;

    private static T? Find<T>(ImmutableArray<T> sorted, string id, Func<T, string> idOf)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(id);
        int lo = 0;
        int hi = sorted.Length - 1;
        while (lo <= hi)
        {
            int mid = lo + ((hi - lo) >> 1);
            int c = string.CompareOrdinal(idOf(sorted[mid]), id);
            if (c == 0)
            {
                return sorted[mid];
            }

            if (c < 0)
            {
                lo = mid + 1;
            }
            else
            {
                hi = mid - 1;
            }
        }

        return null;
    }
}
