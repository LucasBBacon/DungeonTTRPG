using Microsoft.CodeAnalysis;

namespace Dungeon.Analyzers;

/// <summary>
/// One descriptor per DS11 clause, plus DUN012 for raw key derivation. Every rule is an error in category
/// <c>Determinism</c>; a DS11 message names the banned construct (<c>{0}</c>) and what to use instead (<c>{1}</c>).
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

    /// <summary>
    /// DUN012: raw <c>KeyAlgebra.Derive</c>, <c>KeyAlgebra.H</c>, <c>KeyAlgebra.Absorb</c> and <c>DrawStream.Open</c>, and a
    /// hand-built <c>DrawStream</c> (<c>new</c>, explicit <c>default</c>, <c>with</c>), outside the stream handle
    /// files (DS1, SV 4.2). Its message names the banned member or construct (<c>{0}</c>).
    /// </summary>
    public static readonly DiagnosticDescriptor KeyDerivation = Create(
        "DUN012",
        "Raw key derivation is banned in Core",
        "DUN012 (DS1, SV 4.2): open streams only through StreamHandles; '{0}' is banned here");

    private static DiagnosticDescriptor Create(string id, string title) =>
        Create(id, title, id + " (DS11): {0} is banned in Dungeon.Core; {1}");

    private static DiagnosticDescriptor Create(string id, string title, string message) =>
        new(
            id,
            title,
            message,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
}
