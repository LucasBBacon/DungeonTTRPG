using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon.Analyzers;

/// <summary>
/// An exempt file named by a build property, relative to the project folder (DUN011, DUN012). Separators become
/// <c>/</c> and leading <c>./</c> segments are dropped. When the SDK supplies <c>ProjectDir</c> (every real build
/// does) the path is anchored there and must match in full, so a deeper file with the same tail is not exempt;
/// without it (bare compilations) the path must match a whole-segment suffix.
/// </summary>
internal sealed class ProjectPath
{
    private readonly string _path;
    private readonly bool _anchored;

    private ProjectPath(string path, bool anchored)
    {
        _path = path;
        _anchored = anchored;
    }

    /// <summary>The exempt file named by <paramref name="property"/>, or null when it names none.</summary>
    internal static ProjectPath? Allowed(string? property, string? projectDir)
    {
        if (string.IsNullOrWhiteSpace(property))
        {
            return null;
        }

        var relative = Normalize(property!.Trim());
        while (relative.StartsWith("./", StringComparison.Ordinal))
        {
            relative = relative.Substring(2);
        }
        relative = relative.TrimStart('/');
        if (relative.Length == 0)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(projectDir))
        {
            return new ProjectPath("/" + relative, anchored: false);
        }
        var root = Normalize(projectDir!.Trim()).TrimEnd('/');
        return new ProjectPath(root + "/" + relative, anchored: true);
    }

    /// <summary>
    /// The exempt files named by a <c>;</c>-separated <paramref name="property"/>; empty entries are skipped.
    /// <c>|</c> separates too: <c>build/Determinism.props</c> swaps <c>;</c> for it before the property reaches the
    /// generated .editorconfig, where <c>;</c> would start a comment.
    /// </summary>
    internal static IReadOnlyList<ProjectPath> AllowedList(string? property, string? projectDir) =>
        (property ?? string.Empty)
            .Split(';', '|')
            .Select(entry => Allowed(entry, projectDir))
            .Where(path => path is not null)
            .Select(path => path!)
            .ToArray();

    /// <summary>
    /// True when <paramref name="filePath"/> is this file. An anchored path is compared ignoring case: MSBuild hands
    /// the same project folder to both properties, and a case-only difference on a case-insensitive file system
    /// names the same file.
    /// </summary>
    internal bool Matches(string filePath)
    {
        var normalized = Normalize(filePath);
        return _anchored
            ? string.Equals(normalized, _path, StringComparison.OrdinalIgnoreCase)
            : normalized.EndsWith(_path, StringComparison.Ordinal);
    }

    private static string Normalize(string path) => path.Replace('\\', '/');
}
