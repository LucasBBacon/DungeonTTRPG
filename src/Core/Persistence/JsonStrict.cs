using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace Dungeon.Core;

/// <summary>
/// Strictness the JSON reader does not give by itself, shared by saves and content. The reader keeps
/// the last of two properties with the same name; a repeat means the file was altered or mistyped,
/// and it must not be half understood.
/// </summary>
internal static class JsonStrict
{
    /// <summary>The JSON path (such as <c>$.checks[0].owner</c>) of the first repeated property, or null if none repeats.</summary>
    public static string? FirstDuplicate(JsonElement element) => FirstDuplicate(element, out _);

    /// <summary>As <see cref="FirstDuplicate(JsonElement)"/>, also giving the repeated property's name.</summary>
    public static string? FirstDuplicate(JsonElement element, out string? name) => Walk(element, "$", out name);

    private static string? Walk(JsonElement element, string path, out string? name)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var seen = new List<string>();
            foreach (var property in element.EnumerateObject())
            {
                string here = path + "." + property.Name;
                if (seen.Contains(property.Name))
                {
                    name = property.Name;
                    return here;
                }

                seen.Add(property.Name);
                if (Walk(property.Value, here, out name) is { } inner)
                {
                    return inner;
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            int i = 0;
            foreach (var item in element.EnumerateArray())
            {
                if (Walk(item, path + "[" + i.ToString(CultureInfo.InvariantCulture) + "]", out name) is { } inner)
                {
                    return inner;
                }

                i++;
            }
        }

        name = null;
        return null;
    }
}
