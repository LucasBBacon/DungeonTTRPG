using System;

namespace Dungeon.Core.Validation;

/// <summary>
/// A qualified check id (SV 5.1, plan call 3): <c>Vn.CheckName</c> (<c>^V[1-9][0-9]*\.[A-Za-z][A-Za-z0-9]*$</c>)
/// or a content suite <c>content.Part.Part</c> (<c>^content(\.[A-Za-z0-9]+)+$</c>).
/// </summary>
public readonly record struct CheckId
{
    private const string ContentValidator = "content";

    /// <summary>Makes a check id; throws <see cref="ArgumentException"/> if <paramref name="value"/> is outside the grammar.</summary>
    public CheckId(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (!IsValid(value))
        {
            throw new ArgumentException($"\"{value}\" is not a check id (Vn.Name or content.Part).", nameof(value));
        }

        Value = value;
    }

    /// <summary>The id as written.</summary>
    public string Value { get; }

    /// <summary>The validator part: <c>"V7"</c>, <c>"V24"</c>, or <c>"content"</c>.</summary>
    public string Validator => Value.Substring(0, Value.IndexOf('.'));

    /// <summary>True if <paramref name="s"/> follows the check-id grammar.</summary>
    public static bool IsValid(string? s) => s is not null && RegistryIds.IsCheck(s);

    /// <summary>True for a <c>content.*</c> id.</summary>
    public bool IsContent => string.Equals(Validator, ContentValidator, StringComparison.Ordinal);

    /// <inheritdoc />
    public override string ToString() => Value;
}
