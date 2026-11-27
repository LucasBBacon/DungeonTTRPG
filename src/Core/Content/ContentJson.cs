using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dungeon.Core.Content;

/// <summary>
/// The strict JSON reading shared by content kinds, after <c>PersistenceJson</c>: unknown properties,
/// repeated properties, comments and trailing commas are refused, numbers bind only to integer types
/// (DS13), and every failure is a <see cref="ContentLoadException"/> naming the file and the JSON path.
/// </summary>
internal static class ContentJson
{
    private static readonly JsonDocumentOptions DocumentOptions = new()
    {
        CommentHandling = JsonCommentHandling.Disallow,
        AllowTrailingCommas = false,
    };

    private static readonly JsonSerializerOptions Options = new()
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        ReadCommentHandling = JsonCommentHandling.Disallow,
        AllowTrailingCommas = false,
    };

    /// <summary>Parses <paramref name="file"/> into <typeparamref name="TDto"/>; the root must be a JSON object.</summary>
    public static TDto Read<TDto>(ContentFile file)
        where TDto : class
    {
        ArgumentNullException.ThrowIfNull(file);
        JsonDocument doc;
        try
        {
            doc = JsonDocument.Parse(file.Text, DocumentOptions);
        }
        catch (JsonException ex)
        {
            throw new ContentLoadException(file.Name, ex.Path ?? "$", $"not valid JSON: {ex.Message}");
        }

        using (doc)
        {
            if (doc.RootElement.ValueKind != JsonValueKind.Object)
            {
                throw new ContentLoadException(file.Name, "$", "the document is not a JSON object.");
            }

            if (JsonStrict.FirstDuplicate(doc.RootElement) is { } repeated)
            {
                throw new ContentLoadException(file.Name, repeated, "the property appears twice.");
            }

            try
            {
                return doc.RootElement.Deserialize<TDto>(Options)
                    ?? throw new ContentLoadException(file.Name, "$", "the document is null.");
            }
            catch (JsonException ex)
            {
                throw new ContentLoadException(file.Name, ex.Path ?? "$", ex.Message);
            }
        }
    }

    /// <summary>The path of a property: <c>Member("$.checks[0]", "phase")</c> is <c>$.checks[0].phase</c>.</summary>
    public static string Member(string path, string name) => path + "." + name;

    /// <summary>The path of an array item: <c>Item("$.checks", 1)</c> is <c>$.checks[1]</c>.</summary>
    public static string Item(string path, int index) =>
        path + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";

    /// <summary>A required value.</summary>
    public static T Need<T>(ContentFile file, string field, T? value)
        where T : struct =>
        value ?? throw Missing(file, field);

    /// <summary>A required value.</summary>
    public static T Need<T>(ContentFile file, string field, T? value)
        where T : class =>
        value ?? throw Missing(file, field);

    /// <summary>
    /// A required rules string, which must be non-empty ASCII (SV 4.6). An empty string is a blanked
    /// field, as missing as an absent one (MAIN 6.5). Optional fields and list items go through here
    /// too, so a present value is never empty.
    /// </summary>
    public static string RuleString(ContentFile file, string field, string? s)
    {
        string value = Need(file, field, s);
        if (value.Length == 0)
        {
            throw new ContentLoadException(file.Name, field, "the value is empty.");
        }

        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] > '\u007F')
            {
                throw new ContentLoadException(
                    file.Name,
                    field,
                    $"rules strings are ASCII (SV 4.6); character {i} is U+{((int)value[i]).ToString("X4", CultureInfo.InvariantCulture)}.");
            }
        }

        return value;
    }

    /// <summary>
    /// A required display string: any UTF-8 text, not limited to ASCII, and excluded from
    /// <c>manifestHash</c> (SV 4.6). NFC is checked outside Core, which cannot normalise (plan call 8).
    /// </summary>
    public static string Display(ContentFile file, string field, string? s) => Need(file, field, s);

    /// <summary>An optional rules string: absent or null gives null, otherwise as <see cref="RuleString"/>.</summary>
    public static string? OptionalRuleString(ContentFile file, string field, string? s) =>
        s is null ? null : RuleString(file, field, s);

    /// <summary>A required array of rules strings; each item is named <c>field[j]</c>.</summary>
    public static ImmutableArray<string> RuleStrings(ContentFile file, string field, string?[]? items)
    {
        var array = Need(file, field, items);
        var result = ImmutableArray.CreateBuilder<string>(array.Length);
        for (int j = 0; j < array.Length; j++)
        {
            result.Add(RuleString(file, Item(field, j), array[j]));
        }

        return result.MoveToImmutable();
    }

    /// <summary>A required content id (SV 4.1 K-rule 3).</summary>
    public static ContentId Id(ContentFile file, string field, string? s)
    {
        string value = RuleString(file, field, s);
        if (!ContentId.IsValid(value))
        {
            throw new ContentLoadException(file.Name, field, $"\"{value}\" is not a content id; ids use [a-z0-9_.:-].");
        }

        return new ContentId(value);
    }

    /// <summary>A required rational, written as a JSON string in canonical form: <c>"p"</c> or reduced <c>"p/q"</c> with q &gt; 1 (DS13).</summary>
    public static Rational RationalField(ContentFile file, string field, string? s)
    {
        string value = RuleString(file, field, s);
        if (!Rational.TryParse(value, out var r))
        {
            throw new ContentLoadException(
                file.Name, field, $"\"{value}\" is not a canonical rational (an integer, or reduced p/q with q > 1).");
        }

        return r;
    }

    /// <summary>A required enum, by its exact member name. Numbers and other spellings are refused.</summary>
    public static T ParseEnum<T>(ContentFile file, string field, string? s)
        where T : struct, Enum =>
        ParseEnum<T>(file, field, s, lowerFirst: false);

    /// <summary>
    /// A required enum written as its member name with the first letter in lower case, as the specs write
    /// tokens (<c>Agreed</c> is <c>"agreed"</c>, <c>One</c> is <c>"one"</c>).
    /// </summary>
    public static T ParseLowerEnum<T>(ContentFile file, string field, string? s)
        where T : struct, Enum =>
        ParseEnum<T>(file, field, s, lowerFirst: true);

    private static T ParseEnum<T>(ContentFile file, string field, string? s, bool lowerFirst)
        where T : struct, Enum
    {
        string needed = RuleString(file, field, s);
        foreach (var value in Enum.GetValues<T>())
        {
            string name = value.ToString();
            if (lowerFirst && name.Length > 0)
            {
                name = char.ToLowerInvariant(name[0]) + name.Substring(1);
            }

            if (string.Equals(name, needed, StringComparison.Ordinal))
            {
                return value;
            }
        }

        throw new ContentLoadException(file.Name, field, $"unknown {typeof(T).Name} \"{needed}\".");
    }

    private static ContentLoadException Missing(ContentFile file, string field) =>
        new(file.Name, field, "the field is missing.");
}
