using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>V7.NameRegistry</c> (tier 2): every named record of every <see cref="INamedRecordSource"/> has a
/// non-empty display name with no control character, in a category that <c>naming/name_list.json</c> uses.
/// NFC is not checked here, since Core cannot normalise (plan call 8).
/// </summary>
public sealed class NameRegistryCheck : IStaticCheck
{
    private readonly ImmutableArray<ContentKind> _kinds;

    /// <summary>The check over <see cref="ContentKinds.All"/>.</summary>
    public NameRegistryCheck()
        : this(ContentKinds.All)
    {
    }

    /// <summary>The check over the given kinds, of which the <see cref="INamedRecordSource"/>s are read.</summary>
    public NameRegistryCheck(IEnumerable<ContentKind> kinds)
    {
        ArgumentNullException.ThrowIfNull(kinds);
        _kinds = kinds.ToImmutableArray();
        Reads = [.. NamedRecords.SourceFiles(_kinds), NamingFiles.NameList, NamingFiles.Retired];
    }

    /// <inheritdoc />
    public CheckId Id { get; } = new("V7.NameRegistry");

    /// <inheritdoc />
    public ImmutableArray<string> Reads { get; }

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        ArgumentNullException.ThrowIfNull(content);
        var categories = NamedRecords.Sorted(content.All<NameListRecord>().Select(e => e.Category));
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        foreach (var record in NamedRecords.Collect(content, _kinds))
        {
            if (record.DisplayName.Length == 0)
            {
                failures.Add(Fail(record, "the display name is empty."));
            }
            else if (NamingIds.HasControl(record.DisplayName))
            {
                failures.Add(Fail(record, $"the display name \"{record.DisplayName}\" holds a control character."));
            }

            if (!NamedRecords.Contains(categories, record.Category))
            {
                failures.Add(new CheckFailure(
                    Id, record.File, record.CategoryField, $"category \"{record.Category}\" is not a category of naming/name_list.json."));
            }
        }

        return failures.ToImmutable();
    }

    private CheckFailure Fail(NamedRecord record, string problem) => new(Id, record.File, record.Field, problem);
}

/// <summary>The naming files the naming checks read.</summary>
internal static class NamingFiles
{
    /// <summary>The exported name list.</summary>
    public const string NameList = "naming/name_list.json";

    /// <summary>The retired names.</summary>
    public const string Retired = "naming/retired_names.json";

    /// <summary>The greyed-reason registry.</summary>
    public const string GreyedReasons = "naming/greyed_reasons.json";

    /// <summary>The English strings table.</summary>
    public const string Strings = "strings/en.json";
}
