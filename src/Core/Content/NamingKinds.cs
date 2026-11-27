using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Dungeon.Core.Canonical;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Content;

/// <summary>
/// The exported name list, <c>naming/name_list.json</c> (owner ruling 2): one record per harvested entry
/// of the naming-pass appendix, in file order, with id <c>"&lt;doc&gt;:&lt;index&gt;"</c>. Every field
/// but the id is display-tagged. Written by <c>dotnet run --project src/Sim -- name-list</c>.
/// </summary>
public sealed class NameListKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "naming/name_list.json";

    /// <inheritdoc />
    public override string ArrayName => "entries";

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.entries", ContentJson.Read<EntriesDto>(file).Entries);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.entries", i);
            var e = Need(file, at, rows[i]);
            result.Add(new NameListRecord(
                file.Name,
                NamingIds.Id(file, Member(at, "id"), e.Id),
                Display(file, Member(at, "doc"), e.Doc),
                Display(file, Member(at, "name"), e.Name),
                Display(file, Member(at, "category"), e.Category),
                Display(file, Member(at, "status"), e.Status),
                Display(file, Member(at, "outcome"), e.Outcome),
                Display(file, Member(at, "outcomeKind"), e.OutcomeKind),
                e.Note));
        }

        return result.MoveToImmutable();
    }

    private sealed class EntriesDto
    {
        [JsonPropertyName("entries")] public EntryDto?[]? Entries { get; set; }
    }

    private sealed class EntryDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("doc")] public string? Doc { get; set; }
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("category")] public string? Category { get; set; }
        [JsonPropertyName("status")] public string? Status { get; set; }
        [JsonPropertyName("outcome")] public string? Outcome { get; set; }
        [JsonPropertyName("outcomeKind")] public string? OutcomeKind { get; set; }
        [JsonPropertyName("note")] public string? Note { get; set; }
    }
}

/// <summary>
/// The retired names, <c>naming/retired_names.json</c>, derived from the name list (plan call 7). Ids are
/// <c>r0000</c>, <c>r0001</c>, ... in the file's ordinal (name, category) order, since names can be
/// non-ASCII. Every field but the id is display-tagged.
/// </summary>
public sealed class RetiredNamesKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "naming/retired_names.json";

    /// <inheritdoc />
    public override string ArrayName => "retired";

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.retired", ContentJson.Read<RetiredDto>(file).Retired);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.retired", i);
            var r = Need(file, at, rows[i]);
            result.Add(new RetiredNameRecord(
                file.Name,
                NamingIds.Id(file, Member(at, "id"), r.Id),
                Display(file, Member(at, "name"), r.Name),
                Display(file, Member(at, "category"), r.Category),
                Display(file, Member(at, "doc"), r.Doc),
                Display(file, Member(at, "outcome"), r.Outcome)));
        }

        return result.MoveToImmutable();
    }

    private sealed class RetiredDto
    {
        [JsonPropertyName("retired")] public NameDto?[]? Retired { get; set; }
    }

    private sealed class NameDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("category")] public string? Category { get; set; }
        [JsonPropertyName("doc")] public string? Doc { get; set; }
        [JsonPropertyName("outcome")] public string? Outcome { get; set; }
    }
}

/// <summary>
/// The greyed-reason registry, <c>naming/greyed_reasons.json</c>: records <c>{id, doc}</c>, both rules
/// fields. Each reason's string is <c>&lt;doc&gt;.greyed.&lt;id&gt;</c> in <c>strings/en.json</c>
/// (<c>V24.GreyedReasonKeys</c>). The owning steps add the reasons (owner ruling 3).
/// </summary>
public sealed class GreyedReasonKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "naming/greyed_reasons.json";

    /// <inheritdoc />
    public override string ArrayName => "reasons";

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.reasons", ContentJson.Read<ReasonsDto>(file).Reasons);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.reasons", i);
            var r = Need(file, at, rows[i]);
            result.Add(new GreyedReason(
                file.Name,
                Id(file, Member(at, "id"), r.Id).Value,
                RuleString(file, Member(at, "doc"), r.Doc)));
        }

        return result.MoveToImmutable();
    }

    private sealed class ReasonsDto
    {
        [JsonPropertyName("reasons")] public ReasonDto?[]? Reasons { get; set; }
    }

    private sealed class ReasonDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("doc")] public string? Doc { get; set; }
    }
}

/// <summary>
/// The English strings table, <c>strings/en.json</c>: records <c>{key, text, field}</c>. <c>key</c> is the
/// record's id, an ASCII rules field <c>&lt;doc&gt;.&lt;registry&gt;.&lt;id&gt;</c>; <c>text</c> is display
/// (UTF-8, NFC, checked outside Core by plan call 8); <c>field</c> is <c>name</c>, <c>label</c>,
/// <c>line</c> or <c>notice</c>.
/// </summary>
public sealed class StringsKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "strings/en.json";

    /// <inheritdoc />
    public override string ArrayName => "strings";

    /// <inheritdoc />
    public override string IdField => "key";

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.strings", ContentJson.Read<StringsDto>(file).Strings);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.strings", i);
            var s = Need(file, at, rows[i]);
            result.Add(new StringEntry(
                file.Name,
                NamingIds.Key(file, Member(at, "key"), s.Key),
                Display(file, Member(at, "text"), s.Text),
                ParseLowerEnum<StringField>(file, Member(at, "field"), s.Field)));
        }

        return result.MoveToImmutable();
    }

    private sealed class StringsDto
    {
        [JsonPropertyName("strings")] public StringDto?[]? Strings { get; set; }
    }

    private sealed class StringDto
    {
        [JsonPropertyName("key")] public string? Key { get; set; }
        [JsonPropertyName("text")] public string? Text { get; set; }
        [JsonPropertyName("field")] public string? Field { get; set; }
    }
}

/// <summary>
/// One harvested name-list entry. Only <see cref="ContentRecord.Id"/> is a rules field; the rest are
/// display-tagged and hashed in <c>displayHash</c>, not <c>manifestHash</c> (SV 4.6).
/// </summary>
public sealed record NameListRecord(
    string File, string Id, string Doc, string Name, string Category, string Status, string Outcome, string OutcomeKind, string? Note)
    : ContentRecord(File, Id)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(1);
        w.String(0, Id);
        w.EndRecord();
    }
}

/// <summary>A retired (name, category) pair (plan call 7). Only <see cref="ContentRecord.Id"/> is a rules field.</summary>
public sealed record RetiredNameRecord(string File, string Id, string Name, string Category, string Doc, string Outcome)
    : ContentRecord(File, Id)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(1);
        w.String(0, Id);
        w.EndRecord();
    }
}

/// <summary>A greyed reason: why an action is shown but unavailable. Its string is <see cref="StringKey"/>.</summary>
public sealed record GreyedReason(string File, string Id, string Doc) : ContentRecord(File, Id)
{
    /// <summary>The key of this reason's string: <c>&lt;doc&gt;.greyed.&lt;id&gt;</c>.</summary>
    public string StringKey => Doc + ".greyed." + Id;

    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(2);
        w.String(0, Id);
        w.String(1, Doc);
        w.EndRecord();
    }
}

/// <summary>Which kind of on-screen text a string is.</summary>
public enum StringField
{
    /// <summary>A name.</summary>
    Name,

    /// <summary>A label.</summary>
    Label,

    /// <summary>A line (a sentence).</summary>
    Line,

    /// <summary>A notice.</summary>
    Notice,
}

/// <summary>One entry of the strings table; its id is its key. <see cref="Text"/> is display-tagged.</summary>
public sealed record StringEntry(string File, string Key, string Text, StringField Field) : ContentRecord(File, Key)
{
    /// <inheritdoc />
    public override void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(2);
        w.String(0, Id);
        w.Enum(1, Field);
        w.EndRecord();
    }
}

/// <summary>The id grammars of the naming kinds.</summary>
internal static class NamingIds
{
    /// <summary>
    /// A name-list or retired-name id: non-empty ASCII with no control characters. These ids carry the
    /// source's document names, so they do not use the content-id rule.
    /// </summary>
    public static string Id(ContentFile file, string field, string? s)
    {
        string value = RuleString(file, field, s);
        if (value.Length == 0 || HasControl(value))
        {
            throw new ContentLoadException(file.Name, field, $"\"{value}\" is not a name-list id (non-empty, no control characters).");
        }

        return value;
    }

    /// <summary>
    /// A strings key <c>&lt;doc&gt;.&lt;registry&gt;.&lt;id&gt;</c>: at least three non-empty dot-separated
    /// parts of <c>[A-Za-z0-9_:-]</c>; the id part may itself hold dots.
    /// </summary>
    public static string Key(ContentFile file, string field, string? s)
    {
        string value = RuleString(file, field, s);
        var parts = value.Split('.');
        bool valid = parts.Length >= 3;
        foreach (var part in parts)
        {
            valid &= part.Length > 0 && IsKeyPart(part);
        }

        return valid
            ? value
            : throw new ContentLoadException(file.Name, field, $"\"{value}\" is not a strings key (<doc>.<registry>.<id>).");
    }

    /// <summary>True if <paramref name="s"/> holds a control character.</summary>
    public static bool HasControl(string s)
    {
        foreach (var c in s)
        {
            if (char.IsControl(c))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsKeyPart(string part)
    {
        foreach (var c in part)
        {
            bool ok = (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c is '_' or ':' or '-';
            if (!ok)
            {
                return false;
            }
        }

        return true;
    }
}
