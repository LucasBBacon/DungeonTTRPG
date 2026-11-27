using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Validation.Checks;

/// <summary>The shared shape of the V24 checks over named records (tier 3, after <c>V7.NameRegistry</c>).</summary>
public abstract class NamedRecordCheck : IStaticCheck
{
    private readonly ImmutableArray<ContentKind> _kinds;

    /// <summary>The check over the given kinds, reading their <see cref="INamedRecordSource"/>s and <paramref name="files"/>.</summary>
    protected NamedRecordCheck(string id, IEnumerable<ContentKind> kinds, params string[] files)
    {
        ArgumentNullException.ThrowIfNull(kinds);
        Id = new CheckId(id);
        _kinds = kinds.ToImmutableArray();
        Reads = [.. NamedRecords.SourceFiles(_kinds), .. files];
    }

    /// <inheritdoc />
    public CheckId Id { get; }

    /// <inheritdoc />
    public ImmutableArray<string> Reads { get; }

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        ArgumentNullException.ThrowIfNull(content);
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        Check(content, NamedRecords.Collect(content, _kinds), (record, field, problem) =>
            failures.Add(new CheckFailure(Id, record.File, field, problem)));
        return failures.ToImmutable();
    }

    /// <summary>
    /// Checks <paramref name="records"/>, calling <paramref name="fail"/> with the record, the field the
    /// failure is at (<see cref="NamedRecord.Field"/>, <see cref="NamedRecord.KeyField"/>, ...) and the problem.
    /// </summary>
    protected abstract void Check(ContentSet content, ImmutableArray<NamedRecord> records, Action<NamedRecord, string, string> fail);
}

/// <summary>
/// <c>V24.R1ExactEquality</c>: two named records whose display names are ordinal-equal fail, at the
/// later one, unless both carry the same non-null <see cref="NamedRecord.MeaningId"/>.
/// </summary>
public sealed class R1ExactEqualityCheck : NamedRecordCheck
{
    /// <summary>The check over <see cref="ContentKinds.All"/>.</summary>
    public R1ExactEqualityCheck()
        : this(ContentKinds.All)
    {
    }

    /// <summary>The check over the given kinds.</summary>
    public R1ExactEqualityCheck(IEnumerable<ContentKind> kinds)
        : base("V24.R1ExactEquality", kinds)
    {
    }

    /// <inheritdoc />
    protected override void Check(ContentSet content, ImmutableArray<NamedRecord> records, Action<NamedRecord, string, string> fail)
    {
        // The first record of each name, kept ordinal by name for lookup.
        var firstNames = new List<string>();
        var firsts = new List<NamedRecord>();
        foreach (var record in records)
        {
            int at = firstNames.BinarySearch(record.DisplayName, StringComparer.Ordinal);
            if (at < 0)
            {
                firstNames.Insert(~at, record.DisplayName);
                firsts.Insert(~at, record);
                continue;
            }

            var first = firsts[at];
            bool sameMeaning = first.MeaningId is not null
                && string.Equals(first.MeaningId, record.MeaningId, StringComparison.Ordinal);
            if (!sameMeaning)
            {
                fail(record, record.Field, $"\"{record.DisplayName}\" is already the name at {first.File} {first.Field}, and the two do not share a meaningId.");
            }
        }
    }
}

/// <summary><c>V24.R2RetiredReused</c>: a named record whose (display name, category) is a retired pair fails.</summary>
public sealed class R2RetiredReusedCheck : NamedRecordCheck
{
    /// <summary>The check over <see cref="ContentKinds.All"/>.</summary>
    public R2RetiredReusedCheck()
        : this(ContentKinds.All)
    {
    }

    /// <summary>The check over the given kinds.</summary>
    public R2RetiredReusedCheck(IEnumerable<ContentKind> kinds)
        : base("V24.R2RetiredReused", kinds, NamingFiles.Retired)
    {
    }

    /// <inheritdoc />
    protected override void Check(ContentSet content, ImmutableArray<NamedRecord> records, Action<NamedRecord, string, string> fail)
    {
        var retired = content.All<RetiredNameRecord>();
        foreach (var record in records)
        {
            bool reused = retired.Any(r =>
                string.Equals(r.Name, record.DisplayName, StringComparison.Ordinal)
                && string.Equals(r.Category, record.Category, StringComparison.Ordinal));
            if (reused)
            {
                fail(record, record.Field, $"\"{record.DisplayName}\" is a retired {record.Category} name (naming/retired_names.json).");
            }
        }
    }
}

/// <summary>
/// <c>V24.R6InternalByKey</c>: an internal record has no display key; every other record has one, and
/// that key is in <c>strings/en.json</c>.
/// </summary>
public sealed class R6InternalByKeyCheck : NamedRecordCheck
{
    /// <summary>The check over <see cref="ContentKinds.All"/>.</summary>
    public R6InternalByKeyCheck()
        : this(ContentKinds.All)
    {
    }

    /// <summary>The check over the given kinds.</summary>
    public R6InternalByKeyCheck(IEnumerable<ContentKind> kinds)
        : base("V24.R6InternalByKey", kinds, NamingFiles.Strings)
    {
    }

    /// <inheritdoc />
    protected override void Check(ContentSet content, ImmutableArray<NamedRecord> records, Action<NamedRecord, string, string> fail)
    {
        foreach (var record in records)
        {
            if (record.Internal)
            {
                if (record.DisplayKey is not null)
                {
                    fail(record, record.KeyField, $"an internal record is never printed, so it has no display key, not \"{record.DisplayKey}\".");
                }
            }
            else if (record.DisplayKey is null)
            {
                fail(record, record.KeyField, "a record shown on screen needs a display key.");
            }
            else if (content.Find<StringEntry>(record.DisplayKey) is null)
            {
                fail(record, record.KeyField, $"display key \"{record.DisplayKey}\" is not in strings/en.json.");
            }
        }
    }
}

/// <summary><c>V24.GreyedReasonKeys</c>: every greyed reason has its string <c>&lt;doc&gt;.greyed.&lt;id&gt;</c> in <c>strings/en.json</c>.</summary>
public sealed class GreyedReasonKeysCheck : IStaticCheck
{
    /// <inheritdoc />
    public CheckId Id { get; } = new("V24.GreyedReasonKeys");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [NamingFiles.GreyedReasons, NamingFiles.Strings];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        ArgumentNullException.ThrowIfNull(content);
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        var reasons = content.RecordsIn(NamingFiles.GreyedReasons);
        for (int i = 0; i < reasons.Length; i++)
        {
            var reason = (GreyedReason)reasons[i];
            if (content.Find<StringEntry>(reason.StringKey) is null)
            {
                failures.Add(new CheckFailure(
                    Id,
                    NamingFiles.GreyedReasons,
                    Member(Item("$.reasons", i), "id"),
                    $"greyed reason \"{reason.Id}\" has no string \"{reason.StringKey}\" in strings/en.json."));
            }
        }

        return failures.ToImmutable();
    }
}
