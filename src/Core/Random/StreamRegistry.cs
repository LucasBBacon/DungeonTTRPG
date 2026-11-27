using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Dungeon.Core.Content;

namespace Dungeon.Core;

/// <summary>
/// A drawable stream: its registry row id (lookup and display only) and its key (SV 4.2). Only the generated handles
/// build one (<c>XKey.Handle</c>): the constructor is internal and the properties are get-only, and DUN012 reports a
/// <c>new</c>, an explicit <c>default</c> or a <c>with</c> of it outside the key-derivation files, so a Core file
/// cannot put a key the registry never derived behind a row id.
/// </summary>
public readonly record struct StreamHandle
{
    internal StreamHandle(string rowId, ulong key)
    {
        RowId = rowId;
        Key = key;
    }

    /// <summary>The <see cref="StreamRow"/> id, the path with index placeholders (plan call 2).</summary>
    public string RowId { get; }

    /// <summary>The stream's key.</summary>
    public ulong Key { get; }
}

/// <summary>
/// Marks a generated stream key struct of <c>StreamHandles.g.cs</c>. DUN012 reports a <c>new</c>, an explicit
/// <c>default</c> or a <c>with</c> of a marked type outside the key-derivation files, so its key comes only from the
/// generated derivation chain.
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false)]
public sealed class StreamKeyAttribute : Attribute
{
}

/// <summary>One index value given to <see cref="StreamRegistry.KeyOf"/>: a number, or the text of a <c>labelId</c> index.</summary>
/// <param name="Number">The value of a <c>ulong</c> or <c>uint</c> index; ignored for a <c>labelId</c> index.</param>
/// <param name="Text">The id of a <c>labelId</c> index, absorbed as <c>LabelId(Text)</c>; null for a numeric index.</param>
public readonly record struct StreamIndexValue(ulong Number, string? Text);

/// <summary>
/// Keys from the stream registry, <c>registry/streams.json</c> (SV 4.2, SV 9.1). The generated handles in
/// <c>StreamHandles.g.cs</c> must equal <see cref="KeyOf"/> for every row; this is the reference derivation they are
/// tested against.
/// </summary>
public static class StreamRegistry
{
    /// <summary>
    /// The key of <paramref name="rowId"/>: walks the parent chain from <paramref name="runSeed"/>, root first, with one
    /// <see cref="KeyAlgebra.Derive"/> call per row on its label and the indices it absorbs (SV 4.2). The
    /// <paramref name="indices"/> are every index along the chain, in order. For a keyed domain this is the key
    /// <see cref="KeyAlgebra.H"/> draws from, so <c>H = Draw(KeyOf(...), 0)</c>.
    /// </summary>
    /// <exception cref="ArgumentNullException">An argument is null.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="rowId"/> names no stream row; the index count differs from the chain's; a numeric index has text,
    /// or a <c>labelId</c> index has none; or a <c>uint</c> index is above <see cref="uint.MaxValue"/>.
    /// </exception>
    public static ulong KeyOf(ContentSet set, string rowId, ulong runSeed, IReadOnlyList<StreamIndexValue> indices)
    {
        ArgumentNullException.ThrowIfNull(set);
        ArgumentNullException.ThrowIfNull(rowId);
        ArgumentNullException.ThrowIfNull(indices);

        var chain = Chain(set, rowId);
        int total = 0;
        foreach (var row in chain)
        {
            total += row.Indices.Length;
        }

        if (total != indices.Count)
        {
            throw new ArgumentException($"\"{rowId}\" takes {total} indices along its chain, not {indices.Count}.", nameof(indices));
        }

        ulong key = runSeed;
        int at = 0;
        foreach (var row in chain)
        {
            var parts = new ulong[row.Indices.Length];
            for (int j = 0; j < parts.Length; j++)
            {
                parts[j] = Absorbed(row, row.Indices[j], indices[at]);
                at++;
            }

            key = KeyAlgebra.Derive(key, row.Label, parts);
        }

        return key;
    }

    /// <summary>
    /// True if <paramref name="row"/> is a keyed-hash domain: a <c>keyed</c> row whose parent is a <c>keyed</c> node
    /// (plan call 3), such as <c>h:object</c> under <c>checks</c>. A domain has no key struct; its parent exposes it as
    /// an <see cref="KeyAlgebra.H"/> method.
    /// </summary>
    /// <exception cref="ArgumentNullException">An argument is null.</exception>
    public static bool IsDomain(ContentSet set, StreamRow row)
    {
        ArgumentNullException.ThrowIfNull(set);
        ArgumentNullException.ThrowIfNull(row);
        return row.Class == CountClass.Keyed
            && row.Parent is { } parent
            && set.Find<StreamRow>(parent) is { Class: CountClass.Keyed };
    }

    // The rows from the root to rowId.
    private static ImmutableArray<StreamRow> Chain(ContentSet set, string rowId)
    {
        int limit = set.All<StreamRow>().Length;
        var chain = new List<StreamRow>();
        for (string? id = rowId; id is not null; id = chain[chain.Count - 1].Parent)
        {
            if (chain.Count == limit)
            {
                throw new ArgumentException($"\"{rowId}\" has a parent cycle.", nameof(rowId));
            }

            chain.Add(set.Find<StreamRow>(id) ?? throw new ArgumentException($"\"{id}\" names no stream row.", nameof(rowId)));
        }

        chain.Reverse();
        return [.. chain];
    }

    private static ulong Absorbed(StreamRow row, StreamIndex index, StreamIndexValue value)
    {
        switch (index.Type)
        {
            case IndexType.LabelId:
                return value.Text is { } text
                    ? KeyAlgebra.LabelId(text)
                    : throw new ArgumentException($"\"{row.Id}\" index {index.Name} is a labelId and needs text.", "indices");
            case IndexType.UInt when value.Number > uint.MaxValue:
                throw new ArgumentException($"\"{row.Id}\" index {index.Name} is a uint; {value.Number} is above its range.", "indices");
            default:
                return value.Text is null
                    ? value.Number
                    : throw new ArgumentException($"\"{row.Id}\" index {index.Name} is numeric and takes no text.", "indices");
        }
    }
}
