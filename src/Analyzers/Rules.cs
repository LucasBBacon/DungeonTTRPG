using Microsoft.CodeAnalysis;

namespace Dungeon.Analyzers;

/// <summary>
/// One descriptor per DS11 clause. Every rule is an error in category <c>Determinism</c>;
/// its message names the banned construct (<c>{0}</c>) and what to use instead (<c>{1}</c>).
/// </summary>
public static class Rules
{
    private const string Category = "Determinism";

    /// <summary>DUN001: <c>float</c>, <c>double</c> and <c>decimal</c>.</summary>
    public static readonly DiagnosticDescriptor FloatingPoint = Create("DUN001", "Floating point is banned in Core");

    /// <summary>DUN002: <c>System.Random</c>.</summary>
    public static readonly DiagnosticDescriptor SystemRandom = Create("DUN002", "System.Random is banned in Core");

    /// <summary>DUN003: clocks and ambient entropy.</summary>
    public static readonly DiagnosticDescriptor Clock = Create("DUN003", "Clocks and ambient entropy are banned in Core");

    /// <summary>DUN004: <c>GetHashCode</c> and <c>System.HashCode</c>.</summary>
    public static readonly DiagnosticDescriptor Hashing = Create("DUN004", "Runtime hashing is banned in Core");

    /// <summary>DUN005: culture-sensitive string operations.</summary>
    public static readonly DiagnosticDescriptor Culture = Create("DUN005", "Culture-sensitive string operations are banned in Core");

    /// <summary>DUN006: enumeration of unordered collections.</summary>
    public static readonly DiagnosticDescriptor UnorderedEnumeration = Create("DUN006", "Enumerating an unordered collection is banned in Core");

    /// <summary>DUN007: sorts without a registered total comparer.</summary>
    public static readonly DiagnosticDescriptor Sort = Create("DUN007", "Sorting without a registered total comparer is banned in Core");

    /// <summary>DUN008: parallelism.</summary>
    public static readonly DiagnosticDescriptor Parallel = Create("DUN008", "Parallel execution is banned in Core");

    /// <summary>DUN009: any reference to GodotSharp.</summary>
    public static readonly DiagnosticDescriptor Godot = Create("DUN009", "Godot is banned in Core");

    /// <summary>DUN010: signed <c>/</c> and <c>%</c> outside <c>IntMath</c>.</summary>
    public static readonly DiagnosticDescriptor Division = Create("DUN010", "Signed division and remainder are banned in Core");

    /// <summary>DUN011: wrapping arithmetic outside the key-algebra file.</summary>
    public static readonly DiagnosticDescriptor Unchecked = Create("DUN011", "Wrapping arithmetic is banned in Core");

    private static DiagnosticDescriptor Create(string id, string title) =>
        new(
            id,
            title,
            id + " (DS11): {0} is banned in Dungeon.Core; {1}",
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
}
