using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core;

/// <summary>
/// The stream registry, <c>registry/streams.json</c> (SV 9.1 <c>StreamRow</c>). Generation reads it, so it is part
/// of <c>genContentHash</c> (plan call 9). A row's <c>path</c> is <c>/</c>-separated segments, each a label
/// <c>[A-Za-z0-9_.:-]+</c> or an index <c>{name}</c> (<c>{name:}</c> for a <c>labelId</c>), with <c>name</c>
/// <c>[A-Za-z][A-Za-z0-9]*</c> (plan call 2). A <c>length</c> is only on a <c>vector</c> row and a <c>cap</c> only on a
/// <c>per-entry</c> row; <c>parent</c> must name a row of this file.
/// </summary>
public sealed class StreamKind : ContentKind
{
    private static readonly (string Token, CountClass Value)[] Classes =
    [
        ("node", CountClass.Node),
        ("fixed", CountClass.Fixed),
        ("fixed-per-data", CountClass.FixedPerData),
        ("leaf-formula", CountClass.LeafFormula),
        ("per-entry", CountClass.PerEntry),
        ("vector", CountClass.Vector),
        ("keyed", CountClass.Keyed),
        ("sequential-saved", CountClass.SequentialSaved),
        ("display", CountClass.Display),
        ("sim", CountClass.Sim),
    ];

    private static readonly (string Token, IndexType Value)[] IndexTypes =
    [
        ("ulong", IndexType.ULong),
        ("uint", IndexType.UInt),
        ("labelId", IndexType.LabelId),
    ];

    /// <inheritdoc />
    public override string FileName => "registry/streams.json";

    /// <inheritdoc />
    public override string ArrayName => "streams";

    /// <inheritdoc />
    public override string IdField => "path";

    /// <inheritdoc />
    public override bool ReadByGeneration => true;

    /// <summary>The <c>class</c> token <paramref name="cls"/> is spelled as in <c>registry/streams.json</c>.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="cls"/> is not a <see cref="CountClass"/> member.</exception>
    internal static string ClassToken(CountClass cls)
    {
        foreach (var (token, value) in Classes)
        {
            if (value == cls)
            {
                return token;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(cls), cls, "no class token.");
    }

    /// <summary>True if <paramref name="s"/> is a label: one or more of <c>[A-Za-z0-9_.:-]</c>.</summary>
    public static bool IsLabel(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        foreach (var c in s)
        {
            if (!IsLetter(c) && !IsDigit(c) && c != '_' && c != '.' && c != ':' && c != '-')
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>True if <paramref name="s"/> is an index name: <c>[A-Za-z][A-Za-z0-9]*</c>.</summary>
    public static bool IsIndexName(string s)
    {
        if (string.IsNullOrEmpty(s) || !IsLetter(s[0]))
        {
            return false;
        }

        foreach (var c in s)
        {
            if (!IsLetter(c) && !IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>True if <paramref name="s"/> follows the row-id grammar of plan call 2.</summary>
    public static bool IsPath(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        foreach (var segment in s.Split('/'))
        {
            if (segment.Length >= 2 && segment[0] == '{' && segment[segment.Length - 1] == '}')
            {
                string name = segment.Substring(1, segment.Length - 2);
                if (name.EndsWith(':'))
                {
                    name = name.Substring(0, name.Length - 1);
                }

                if (!IsIndexName(name))
                {
                    return false;
                }
            }
            else if (!IsLabel(segment))
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.streams", ContentJson.Read<StreamsDto>(file).Streams);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.streams", i);
            var r = Need(file, at, rows[i]);
            string path = Grammar(file, Member(at, "path"), r.Path, IsPath, "a stream path (label and {index} segments separated by /)");
            string? parent = OptionalRuleString(file, Member(at, "parent"), r.Parent);
            string label = Grammar(file, Member(at, "label"), r.Label, IsLabel, "a label ([A-Za-z0-9_.:-]+)");
            var indices = Indices(file, Member(at, "indices"), r.Indices);
            var cls = Token(file, Member(at, "class"), r.Class, Classes, "count class");
            var count = OptionalFormula(file, Member(at, "countFormula"), r.CountFormula);
            if (r.Length is not null && cls != CountClass.Vector)
            {
                throw new ContentLoadException(file.Name, Member(at, "length"), "only a vector row has a length.");
            }

            if (r.Cap is not null && cls != CountClass.PerEntry)
            {
                throw new ContentLoadException(file.Name, Member(at, "cap"), "only a per-entry row has a cap.");
            }

            result.Add(new StreamRow(
                file.Name,
                path,
                parent,
                label,
                indices,
                cls,
                count,
                r.Length,
                OptionalFormula(file, Member(at, "cap"), r.Cap),
                OptionalRuleString(file, Member(at, "stage"), r.Stage),
                ParseLowerEnum<RetryScope>(file, Member(at, "retryScope"), r.RetryScope),
                ParseLowerEnum<KeyType>(file, Member(at, "keyType"), r.KeyType),
                RuleString(file, Member(at, "owner"), r.Owner),
                WaveField(file, Member(at, "wave"), r.Wave),
                RuleStrings(file, Member(at, "alias"), r.Alias),
                RuleStrings(file, Member(at, "readers"), r.Readers),
                RuleStrings(file, Member(at, "consumers"), r.Consumers),
                RuleStrings(file, Member(at, "rules"), r.Rules),
                RuleStrings(file, Member(at, "source"), r.Source)));
        }

        return result.MoveToImmutable();
    }

    /// <inheritdoc />
    public override void Resolve(ContentSet set)
    {
        var rows = set.RecordsIn(FileName);
        for (int i = 0; i < rows.Length; i++)
        {
            var row = (StreamRow)rows[i];
            if (row.Parent is { } parent && set.Find<StreamRow>(parent) is null)
            {
                throw new ContentLoadException(FileName, Member(Item("$.streams", i), "parent"), $"\"{parent}\" names no stream row.");
            }
        }
    }

    private static ImmutableArray<StreamIndex> Indices(ContentFile file, string field, IndexDto?[]? items)
    {
        var array = Need(file, field, items);
        var result = ImmutableArray.CreateBuilder<StreamIndex>(array.Length);
        for (int j = 0; j < array.Length; j++)
        {
            string at = Item(field, j);
            var x = Need(file, at, array[j]);
            result.Add(new StreamIndex(
                Grammar(file, Member(at, "name"), x.Name, IsIndexName, "an index name ([A-Za-z][A-Za-z0-9]*)"),
                Token(file, Member(at, "type"), x.Type, IndexTypes, "index type")));
        }

        return result.MoveToImmutable();
    }

    private static CountFormula? OptionalFormula(ContentFile file, string field, string? s)
    {
        if (OptionalRuleString(file, field, s) is not { } text)
        {
            return null;
        }

        return CountFormula.TryParse(text, out var f, out var error) ? f : throw new ContentLoadException(file.Name, field, error);
    }

    private static Wave WaveField(ContentFile file, string field, string? s)
    {
        string text = RuleString(file, field, s);
        return Waves.TryParse(text, out var wave) ? wave : throw new ContentLoadException(file.Name, field, $"unknown Wave \"{text}\".");
    }

    private static T Token<T>(ContentFile file, string field, string? s, (string Token, T Value)[] table, string what)
    {
        string text = RuleString(file, field, s);
        foreach (var (token, value) in table)
        {
            if (string.Equals(token, text, StringComparison.Ordinal))
            {
                return value;
            }
        }

        throw new ContentLoadException(file.Name, field, $"unknown {what} \"{text}\".");
    }

    private static string Grammar(ContentFile file, string field, string? s, Func<string, bool> grammar, string what)
    {
        string text = RuleString(file, field, s);
        return grammar(text) ? text : throw new ContentLoadException(file.Name, field, $"\"{text}\" is not {what}.");
    }

    private static bool IsLetter(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');

    private static bool IsDigit(char c) => c >= '0' && c <= '9';

    private sealed class StreamsDto
    {
        [JsonPropertyName("streams")] public StreamDto?[]? Streams { get; set; }
    }

    private sealed class StreamDto
    {
        [JsonPropertyName("path")] public string? Path { get; set; }
        [JsonPropertyName("alias")] public string?[]? Alias { get; set; }
        [JsonPropertyName("parent")] public string? Parent { get; set; }
        [JsonPropertyName("label")] public string? Label { get; set; }
        [JsonPropertyName("indices")] public IndexDto?[]? Indices { get; set; }
        [JsonPropertyName("class")] public string? Class { get; set; }
        [JsonPropertyName("countFormula")] public string? CountFormula { get; set; }
        [JsonPropertyName("length")] public ulong? Length { get; set; }
        [JsonPropertyName("cap")] public string? Cap { get; set; }
        [JsonPropertyName("stage")] public string? Stage { get; set; }
        [JsonPropertyName("readers")] public string?[]? Readers { get; set; }
        [JsonPropertyName("consumers")] public string?[]? Consumers { get; set; }
        [JsonPropertyName("retryScope")] public string? RetryScope { get; set; }
        [JsonPropertyName("rules")] public string?[]? Rules { get; set; }
        [JsonPropertyName("keyType")] public string? KeyType { get; set; }
        [JsonPropertyName("owner")] public string? Owner { get; set; }
        [JsonPropertyName("source")] public string?[]? Source { get; set; }
        [JsonPropertyName("wave")] public string? Wave { get; set; }
    }

    private sealed class IndexDto
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("type")] public string? Type { get; set; }
    }
}
