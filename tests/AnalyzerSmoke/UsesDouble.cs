namespace Dungeon.AnalyzerSmoke;

/// <summary>Breaks DS11 on purpose: the build must stop with DUN001.</summary>
public static class UsesDouble
{
    /// <summary>A banned floating-point field.</summary>
    public static double Value;
}
