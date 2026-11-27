using System;
using System.Collections.Immutable;
using Dungeon.Core.Content;

namespace Dungeon.Core.Effects;

/// <summary>
/// A C# hook: what a trigger's <c>{"hook": id}</c> action runs when data cannot express the effect (MAIN 6.5; AB 14.4).
/// It returns the next state and may raise events through <see cref="HookContext.Emit"/>. It is pure: it may not draw
/// except through a stream it threads into the state it returns.
/// </summary>
/// <param name="ctx">The trigger's event, instance and args, and the state to change.</param>
/// <returns>The next state; never null.</returns>
public delegate GameState Hook(HookContext ctx);

/// <summary>
/// What a <see cref="Hook"/> is called with: the rules, the current state, the event that fired the trigger, the
/// instance whose trigger it is, the action's args, and <see cref="Emit"/>, which appends an event to the command's
/// list and to the trigger queue.
/// </summary>
public sealed record HookContext(
    RulesContext Rules,
    GameState State,
    GameEvent Event,
    EffectInstance Instance,
    ImmutableArray<string> Args,
    Action<GameEvent> Emit);

/// <summary>
/// The hooks that content may name, by content id (M1-07 plan call 5). Immutable: <see cref="With"/> returns a new
/// registry. The entries are kept ordinal by id, so lookup is a binary search and nothing enumerates a dictionary.
/// </summary>
public sealed class HookRegistry
{
    private readonly ImmutableArray<Entry> _entries;

    private HookRegistry(ImmutableArray<Entry> entries) => _entries = entries;

    /// <summary>The production hooks. M1-07 ships none; later steps add them, and tests build their own.</summary>
    public static HookRegistry Core { get; } = new(ImmutableArray<Entry>.Empty);

    /// <summary>
    /// This registry plus <paramref name="hook"/> under <paramref name="id"/>. Throws <see cref="ArgumentException"/> if
    /// <paramref name="id"/> is not a content id or is already registered.
    /// </summary>
    public HookRegistry With(string id, Hook hook)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(hook);
        if (!ContentId.IsValid(id))
        {
            throw new ArgumentException($"\"{id}\" is not a content id.", nameof(id));
        }

        int at = IndexOf(id);
        if (at >= 0)
        {
            throw new ArgumentException($"Hook \"{id}\" is registered twice.", nameof(id));
        }

        return new HookRegistry(_entries.Insert(~at, new Entry(id, hook)));
    }

    /// <summary>True if a hook is registered under <paramref name="id"/>.</summary>
    public bool Contains(string id) => IndexOf(id) >= 0;

    /// <summary>The hook <paramref name="id"/>. Throws <see cref="ArgumentException"/> if there is none.</summary>
    internal Hook Get(string id)
    {
        int at = IndexOf(id);
        return at >= 0 ? _entries[at].Hook : throw new ArgumentException($"No hook \"{id}\".", nameof(id));
    }

    // The entry's index, or the bitwise complement of where it would be inserted.
    private int IndexOf(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        int lo = 0;
        int hi = _entries.Length - 1;
        while (lo <= hi)
        {
            int mid = lo + ((hi - lo) >> 1);
            int c = string.CompareOrdinal(_entries[mid].Id, id);
            if (c == 0)
            {
                return mid;
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

        return ~lo;
    }

    private readonly record struct Entry(string Id, Hook Hook);
}
