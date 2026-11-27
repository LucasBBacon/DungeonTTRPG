namespace Dungeon.Core;

/// <summary>
/// The generator's version, a member of the reproducibility key (SV 4.6). Bump it whenever generator
/// code changes the output for any key (SV 4.10); a golden digest change without a bump fails CI.
/// </summary>
public static class GenVersion
{
    /// <summary>The generator version this build implements.</summary>
    public const int Current = 1;
}

/// <summary>
/// The rules core's version (SV 4.10). Bump it whenever rules-core code changes what a command does;
/// a replay is valid only on a build with the same key and rules version (SV 4.7, M1-27).
/// </summary>
public static class RulesVersion
{
    /// <summary>The rules version this build implements.</summary>
    public const int Current = 1;
}
