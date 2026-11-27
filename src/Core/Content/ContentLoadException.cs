using System;

namespace Dungeon.Core.Content;

/// <summary>
/// A content file failed to load or validate. It always names the file and the field (a JSON path
/// such as <c>$.checks[1].phase</c>), so a bad reference or a missing field fails loudly (MAIN 6.5).
/// </summary>
public sealed class ContentLoadException : Exception
{
    public ContentLoadException(string file, string field, string problem)
        : base($"Content error in {file} at {field}: {problem}")
    {
        File = file;
        Field = field;
    }

    /// <summary>The content file the problem is in.</summary>
    public string File { get; }

    /// <summary>The JSON path of the offending field.</summary>
    public string Field { get; }
}
