namespace Dungeon.Core.Validation;

/// <summary>
/// The phase of a check part (SV 5.1), written <c>"s"</c>, <c>"g"</c>, <c>"r"</c> or <c>"t"</c>.
/// <c>s</c> and <c>t</c> parts fail the build; <c>g</c> and <c>r</c> parts act by their registered class.
/// </summary>
public enum Phase
{
    /// <summary>A static build check over content.</summary>
    S,

    /// <summary>A generation-time check.</summary>
    G,

    /// <summary>A runtime assertion.</summary>
    R,

    /// <summary>A test-time check.</summary>
    T,
}
