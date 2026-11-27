using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>V19.LabelCollision</c> (SV 5.2): the 64-bit collision scan across every registered stream label and every record id
/// of a kind whose ids are content ids (<see cref="ContentKind.IdsAreContentIds"/>). Each distinct string is hashed with
/// <c>KeyAlgebra.LabelId</c>; two distinct strings with one value fail at the later one (stream labels first, in file
/// order, then each kind's ids in file order). The same string twice is not a collision.
/// </summary>
public sealed class LabelCollisionCheck : IStaticCheck
{
    private const string StreamsFile = "registry/streams.json";

    private readonly ImmutableArray<ContentKind> _kinds;
    private readonly Func<string, ulong> _labelId;

    /// <summary>The check over <see cref="ContentKinds.All"/> with <c>KeyAlgebra.LabelId</c>.</summary>
    public LabelCollisionCheck()
        : this(ContentKinds.All, KeyAlgebra.LabelId)
    {
    }

    /// <summary>The check over <see cref="ContentKinds.All"/> with the given hash, so a fixture can force a collision.</summary>
    public LabelCollisionCheck(Func<string, ulong> labelId)
        : this(ContentKinds.All, labelId)
    {
    }

    /// <summary>The check over the given kinds with the given hash.</summary>
    public LabelCollisionCheck(IEnumerable<ContentKind> kinds, Func<string, ulong> labelId)
    {
        ArgumentNullException.ThrowIfNull(kinds);
        ArgumentNullException.ThrowIfNull(labelId);
        _kinds = kinds.ToImmutableArray();
        _labelId = labelId;
        Reads = new[] { StreamsFile }
            .Concat(_kinds.Select(k => k.FileName))
            .Distinct(StringComparer.Ordinal)
            .ToImmutableArray();
    }

    /// <inheritdoc />
    public CheckId Id { get; } = new("V19.LabelCollision");

    /// <inheritdoc />
    public ImmutableArray<string> Reads { get; }

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        ArgumentNullException.ThrowIfNull(content);
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();

        // Every value seen so far, ascending, with the first string that gave it.
        var values = new List<ulong>();
        var owners = new List<string>();
        void Scan(string s, string file, string field)
        {
            ulong value = _labelId(s);
            int at = values.BinarySearch(value);
            if (at < 0)
            {
                values.Insert(~at, value);
                owners.Insert(~at, s);
            }
            else if (!string.Equals(owners[at], s, StringComparison.Ordinal))
            {
                failures.Add(new CheckFailure(Id, file, field, $"\"{s}\" and \"{owners[at]}\" share the 64-bit label id 0x{value:x16}."));
            }
        }

        var streams = content.RecordsIn(StreamsFile);
        for (int i = 0; i < streams.Length; i++)
        {
            Scan(((StreamRow)streams[i]).Label, StreamsFile, Member(Item("$.streams", i), "label"));
        }

        foreach (var kind in _kinds)
        {
            if (!kind.IdsAreContentIds)
            {
                continue;
            }

            var records = content.RecordsIn(kind.FileName);
            for (int i = 0; i < records.Length; i++)
            {
                Scan(records[i].Id, kind.FileName, Member(Item("$." + kind.ArrayName, i), kind.IdField));
            }
        }

        return failures.ToImmutable();
    }
}
