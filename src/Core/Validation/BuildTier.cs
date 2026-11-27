using System;
using System.Globalization;

namespace Dungeon.Core.Validation;

/// <summary>
/// The build tier of a check (SV 5.3, plan call 5): 0 content load and V19s; 1 other content suites;
/// 2 V7s, V1s; 3 V24s, V8s to V11s, V17s, V18s, V20s, V21s; 4 V12s; 5 V13s to V16s; 6 V2 to V6.
/// The content load row is a <c>content.*</c> id whose last part is <c>Load</c>.
/// </summary>
public static class BuildTier
{
    private const string LoadPart = ".Load";

    /// <summary>The tier of <paramref name="id"/>; throws <see cref="ArgumentException"/> for a validator with no tier.</summary>
    public static int Of(CheckId id)
    {
        if (id.IsContent)
        {
            return id.Value.EndsWith(LoadPart, StringComparison.Ordinal) ? 0 : 1;
        }

        int n = int.Parse(id.Validator.AsSpan(1), NumberStyles.None, CultureInfo.InvariantCulture);
        return n switch
        {
            19 => 0,
            1 or 7 => 2,
            8 or 9 or 10 or 11 or 17 or 18 or 20 or 21 or 24 => 3,
            12 => 4,
            >= 13 and <= 16 => 5,
            >= 2 and <= 6 => 6,
            _ => throw new ArgumentException($"{id.Validator} has no build tier (SV 5.3).", nameof(id)),
        };
    }
}
