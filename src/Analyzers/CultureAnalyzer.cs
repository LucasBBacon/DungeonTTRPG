using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dungeon.Analyzers;

/// <summary>
/// DUN005: culture-sensitive string operations (DS11). Reports <c>ToUpper</c>/<c>ToLower</c> on
/// <c>string</c> and <c>char</c> unless the culture argument is <c>CultureInfo.InvariantCulture</c>;
/// <c>string</c> comparison overloads that take no <c>StringComparison</c> and compare strings rather than
/// chars; any <c>StringComparison</c> argument that is not a constant <c>Ordinal</c>/<c>OrdinalIgnoreCase</c>;
/// the culture-aware <c>StringComparer</c> properties; <c>CultureInfo.CurrentCulture</c>/<c>CurrentUICulture</c>;
/// and string comparison by <c>Comparer&lt;T&gt;.Default</c> under another name, where a "string type" is
/// <c>string</c> or a tuple with a string element: <c>Comparer&lt;T&gt;.Default</c> for a string type;
/// <c>CompareTo</c> on a tuple of a string type (it compares elements with <c>Comparer&lt;T&gt;.Default</c>);
/// <c>IComparable&lt;T&gt;.CompareTo</c> for a string type and any non-generic <c>IComparable.CompareTo</c>;
/// a generic method call that binds a string type to a type parameter constrained to <c>IComparable</c>;
/// <c>Enumerable.Min</c>/<c>Max</c>/<c>MinBy</c>/<c>MaxBy</c> over a string type without a comparer; and
/// <c>List&lt;T&gt;.BinarySearch</c>/<c>Array.BinarySearch</c> over a string type without a comparer.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class CultureAnalyzer : DiagnosticAnalyzer
{
    private const string CaseAdvice = "use ToUpperInvariant or ToLowerInvariant";
    private const string ComparisonAdvice = "pass StringComparison.Ordinal or OrdinalIgnoreCase";
    private const string ComparerAdvice = "use StringComparer.Ordinal or OrdinalIgnoreCase";
    private const string CultureAdvice = "use CultureInfo.InvariantCulture";
    private const string DefaultComparerAdvice = "compare strings with StringComparer.Ordinal";

    /// <summary>String methods whose overloads without a <c>StringComparison</c> compare by culture.</summary>
    private static readonly ImmutableHashSet<string> ComparisonMethods = ImmutableHashSet.Create(
        StringComparer.Ordinal, "Compare", "CompareTo", "StartsWith", "EndsWith", "IndexOf", "LastIndexOf");

    private static readonly ImmutableHashSet<string> CaseMethods = ImmutableHashSet.Create(
        StringComparer.Ordinal, "ToUpper", "ToLower");

    private static readonly ImmutableHashSet<string> CultureComparers = ImmutableHashSet.Create(
        StringComparer.Ordinal,
        "CurrentCulture",
        "CurrentCultureIgnoreCase",
        "InvariantCulture",
        "InvariantCultureIgnoreCase");

    private static readonly ImmutableHashSet<string> AmbientCultures = ImmutableHashSet.Create(
        StringComparer.Ordinal, "CurrentCulture", "CurrentUICulture");

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rules.Culture);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            var compilation = start.Compilation;
            var known = new KnownTypes(
                compilation.GetTypeByMetadataName("System.StringComparison"),
                compilation.GetTypeByMetadataName("System.StringComparer"),
                compilation.GetTypeByMetadataName("System.Globalization.CultureInfo"),
                compilation.GetTypeByMetadataName("System.Collections.Generic.Comparer`1"),
                compilation.GetTypeByMetadataName("System.IComparable`1"),
                compilation.GetTypeByMetadataName("System.IComparable"),
                compilation.GetTypeByMetadataName("System.Collections.Generic.IComparer`1"),
                compilation.GetTypeByMetadataName("System.Collections.IComparer"),
                compilation.GetTypeByMetadataName("System.Linq.Enumerable"),
                compilation.GetTypeByMetadataName("System.Collections.Generic.List`1"),
                compilation.GetSpecialType(SpecialType.System_Array));

            start.RegisterOperationAction(
                c => AnalyzeInvocation(c, (IInvocationOperation)c.Operation, known),
                OperationKind.Invocation);
            start.RegisterOperationAction(
                c => AnalyzeMethodGroup(c, (IMethodReferenceOperation)c.Operation, known.StringComparison),
                OperationKind.MethodReference);
            start.RegisterOperationAction(
                c => AnalyzeProperty(c, (IPropertyReferenceOperation)c.Operation, known),
                OperationKind.PropertyReference);
        });
    }

    private static void AnalyzeInvocation(
        OperationAnalysisContext context, IInvocationOperation invocation, KnownTypes known)
    {
        var method = invocation.TargetMethod;
        var stringComparison = known.StringComparison;
        if (IsCultureSensitiveOverload(method, stringComparison))
        {
            if (!IsInvariantCaseConversion(invocation, known))
            {
                Report(context, invocation.Syntax.GetLocation(), Describe(method), AdviceFor(method));
            }
            return;
        }

        if (DefaultComparison(invocation, known) is { } construct)
        {
            Report(context, invocation.Syntax.GetLocation(), construct, DefaultComparerAdvice);
            return;
        }

        if (stringComparison is null)
        {
            return;
        }

        // Any method taking a StringComparison: only a constant Ordinal or OrdinalIgnoreCase is provably ordinal.
        foreach (var argument in invocation.Arguments)
        {
            if (argument.Parameter is { } parameter
                && SymbolEqualityComparer.Default.Equals(parameter.Type, stringComparison)
                && !IsOrdinalConstant(argument.Value))
            {
                Report(
                    context,
                    invocation.Syntax.GetLocation(),
                    Describe(method) + " with a culture-sensitive or non-constant StringComparison",
                    ComparisonAdvice);
                return;
            }
        }
    }

    private static void AnalyzeMethodGroup(
        OperationAnalysisContext context, IMethodReferenceOperation reference, INamedTypeSymbol? stringComparison)
    {
        // A method group such as names.Select(char.ToUpper) runs the same culture-sensitive overload.
        var method = reference.Method;
        if (IsCultureSensitiveOverload(method, stringComparison))
        {
            Report(context, reference.Syntax.GetLocation(), Describe(method), AdviceFor(method));
        }
    }

    private static void AnalyzeProperty(
        OperationAnalysisContext context,
        IPropertyReferenceOperation reference,
        KnownTypes known)
    {
        var property = reference.Property;
        var owner = property.ContainingType;
        var stringComparer = known.StringComparer;
        var cultureInfo = known.CultureInfo;
        if (property.Name == "Default"
            && property.IsStatic
            && owner is not null
            && Is(owner.OriginalDefinition, known.ComparerOfT)
            && IsStringType(owner.TypeArguments[0]))
        {
            Report(
                context,
                reference.Syntax.GetLocation(),
                "Comparer<" + owner.TypeArguments[0].ToDisplayString() + ">.Default (culture-sensitive for strings)",
                DefaultComparerAdvice);
        }
        else if (stringComparer is not null
            && SymbolEqualityComparer.Default.Equals(owner, stringComparer)
            && CultureComparers.Contains(property.Name))
        {
            Report(context, reference.Syntax.GetLocation(), "StringComparer." + property.Name, ComparerAdvice);
        }
        else if (cultureInfo is not null
            && SymbolEqualityComparer.Default.Equals(owner, cultureInfo)
            && AmbientCultures.Contains(property.Name))
        {
            Report(context, reference.Syntax.GetLocation(), "CultureInfo." + property.Name, CultureAdvice);
        }
    }

    /// <summary>
    /// True for <c>ToUpper</c>/<c>ToLower</c> on <c>string</c> or <c>char</c>, and for a
    /// <c>string</c> comparison overload that has no <c>StringComparison</c> parameter and does not
    /// compare a <c>char</c> (the char overloads are ordinal).
    /// </summary>
    private static bool IsCultureSensitiveOverload(IMethodSymbol method, INamedTypeSymbol? stringComparison)
    {
        var owner = method.ContainingType?.SpecialType;
        if (CaseMethods.Contains(method.Name))
        {
            return owner is SpecialType.System_String or SpecialType.System_Char;
        }

        if (owner != SpecialType.System_String || !ComparisonMethods.Contains(method.Name))
        {
            return false;
        }

        foreach (var parameter in method.Parameters)
        {
            if (SymbolEqualityComparer.Default.Equals(parameter.Type, stringComparison))
            {
                return false;
            }
        }

        return method.Parameters.Length == 0 || method.Parameters[0].Type.SpecialType != SpecialType.System_Char;
    }

    /// <summary>
    /// <c>ToUpper</c>/<c>ToLower</c> given <c>CultureInfo.InvariantCulture</c>: the same as the
    /// <c>*Invariant</c> methods, so allowed (the table bans only non-Invariant case conversion).
    /// </summary>
    private static bool IsInvariantCaseConversion(IInvocationOperation invocation, KnownTypes known) =>
        CaseMethods.Contains(invocation.TargetMethod.Name)
        && invocation.Arguments.Any(a =>
            a.Parameter is { } parameter
            && Is(parameter.Type, known.CultureInfo)
            && Strip(a.Value) is IPropertyReferenceOperation { Property: { Name: "InvariantCulture", IsStatic: true } property }
            && Is(property.ContainingType, known.CultureInfo));

    /// <summary>
    /// The construct to report when <paramref name="invocation"/> compares strings through
    /// <c>Comparer&lt;T&gt;.Default</c> or <c>IComparable</c> under another name; null when it does not.
    /// </summary>
    private static string? DefaultComparison(IInvocationOperation invocation, KnownTypes known)
    {
        var method = invocation.TargetMethod;
        var owner = method.ContainingType;
        if (owner is null)
        {
            return null;
        }

        if (method.Name == "CompareTo")
        {
            if (IsTuple(owner) && IsStringType(owner) && !method.Parameters.Any(p => IsComparerType(p.Type, known)))
            {
                return owner.ToDisplayString() + ".CompareTo (it compares the string element by culture)";
            }
            if (Is(owner.OriginalDefinition, known.IComparableOfT) && IsStringType(owner.TypeArguments[0]))
            {
                return owner.ToDisplayString() + ".CompareTo";
            }
            if (Is(owner, known.IComparable))
            {
                return "IComparable.CompareTo (the instance may be a string)";
            }
        }

        if (Is(owner, known.Enumerable) && !HasComparerArgument(invocation, known))
        {
            ITypeSymbol? key = method.Name switch
            {
                "Min" or "Max" => method.ReturnType,
                "MinBy" or "MaxBy" when method.TypeArguments.Length == 2 => method.TypeArguments[1],
                _ => null,
            };
            if (IsStringType(key))
            {
                return "Enumerable." + method.Name + " over " + key!.ToDisplayString() + " without a comparer";
            }
        }

        if (method.Name == "BinarySearch" && !HasComparerArgument(invocation, known))
        {
            var element = BinarySearchElement(invocation, known);
            if (IsStringType(element))
            {
                return owner.Name + ".BinarySearch over " + element!.ToDisplayString() + " without a comparer";
            }
        }

        // Generic code reaching IComparable<string>.CompareTo: Cmp<T>(a, b) where T : IComparable<T>, called with string.
        for (var i = 0; i < method.TypeParameters.Length && i < method.TypeArguments.Length; i++)
        {
            if (IsComparableConstrained(method.TypeParameters[i], known) && IsStringType(method.TypeArguments[i]))
            {
                return method.Name + "<" + method.TypeArguments[i].ToDisplayString()
                    + "> (its type parameter is compared through IComparable)";
            }
        }

        return null;
    }

    /// <summary>The element type a <c>List&lt;T&gt;</c> or <c>Array</c> binary search compares; null for other owners.</summary>
    private static ITypeSymbol? BinarySearchElement(IInvocationOperation invocation, KnownTypes known)
    {
        var method = invocation.TargetMethod;
        var owner = method.ContainingType;
        if (Is(owner.OriginalDefinition, known.ListOfT))
        {
            return owner.TypeArguments[0];
        }
        if (!Is(owner, known.Array))
        {
            return null;
        }
        if (method.TypeArguments.Length > 0)
        {
            return method.TypeArguments[0];
        }

        // Array.BinarySearch(Array, ...): the array argument's element type, else the searched value's type.
        foreach (var argument in invocation.Arguments)
        {
            if (Strip(argument.Value).Type is IArrayTypeSymbol array)
            {
                return array.ElementType;
            }
        }
        foreach (var argument in invocation.Arguments)
        {
            if (argument.Parameter?.Type.SpecialType == SpecialType.System_Object)
            {
                return Strip(argument.Value).Type;
            }
        }
        return null;
    }

    private static bool IsComparableConstrained(ITypeParameterSymbol parameter, KnownTypes known) =>
        parameter.ConstraintTypes.Any(t =>
            Is(t.OriginalDefinition, known.IComparableOfT) || Is(t, known.IComparable));

    /// <summary>True when an explicit, non-null comparer argument is passed.</summary>
    private static bool HasComparerArgument(IInvocationOperation invocation, KnownTypes known) =>
        invocation.Arguments.Any(a =>
            a.Parameter is { } parameter
            && a.ArgumentKind != ArgumentKind.DefaultValue
            && IsComparerType(parameter.Type, known)
            && !(Strip(a.Value).ConstantValue is { HasValue: true, Value: null }));

    private static bool IsComparerType(ITypeSymbol type, KnownTypes known) =>
        Is(type.OriginalDefinition, known.IComparerOfT) || Is(type, known.IComparer);

    /// <summary><c>string</c>, or a tuple (<c>ValueTuple</c>/<c>Tuple</c>) or nullable with a string type element, at any depth.</summary>
    private static bool IsStringType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }
        if (type.SpecialType == SpecialType.System_String)
        {
            return true;
        }
        return type is INamedTypeSymbol named
            && (IsTuple(named) || named.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
            && named.TypeArguments.Any(IsStringType);
    }

    private static bool IsTuple(INamedTypeSymbol type) =>
        type.IsTupleType
        || (type.IsGenericType
            && type.Name is "ValueTuple" or "Tuple"
            && type.ContainingNamespace?.ToDisplayString() == "System");

    private static IOperation Strip(IOperation operation)
    {
        while (operation is IConversionOperation conversion)
        {
            operation = conversion.Operand;
        }
        return operation;
    }

    private static bool Is(ITypeSymbol? type, INamedTypeSymbol? known) =>
        type is not null && known is not null && SymbolEqualityComparer.Default.Equals(type, known);

    private static bool IsOrdinalConstant(IOperation value) =>
        value.ConstantValue is { HasValue: true, Value: int raw }
        && (raw == (int)StringComparison.Ordinal || raw == (int)StringComparison.OrdinalIgnoreCase);

    private static string Describe(IMethodSymbol method) =>
        method.ContainingType.Name + "." + method.Name;

    private static string AdviceFor(IMethodSymbol method) =>
        CaseMethods.Contains(method.Name) ? CaseAdvice : ComparisonAdvice;

    private static void Report(OperationAnalysisContext context, Location location, string construct, string advice) =>
        context.ReportDiagnostic(Diagnostic.Create(Rules.Culture, location, construct, advice));

    /// <summary>The symbols one compilation resolves for the rule.</summary>
    private sealed class KnownTypes
    {
        internal KnownTypes(
            INamedTypeSymbol? stringComparison,
            INamedTypeSymbol? stringComparer,
            INamedTypeSymbol? cultureInfo,
            INamedTypeSymbol? comparerOfT,
            INamedTypeSymbol? iComparableOfT,
            INamedTypeSymbol? iComparable,
            INamedTypeSymbol? iComparerOfT,
            INamedTypeSymbol? iComparer,
            INamedTypeSymbol? enumerable,
            INamedTypeSymbol? listOfT,
            INamedTypeSymbol? array)
        {
            StringComparison = stringComparison;
            StringComparer = stringComparer;
            CultureInfo = cultureInfo;
            ComparerOfT = comparerOfT;
            IComparableOfT = iComparableOfT;
            IComparable = iComparable;
            IComparerOfT = iComparerOfT;
            IComparer = iComparer;
            Enumerable = enumerable;
            ListOfT = listOfT;
            Array = array;
        }

        internal INamedTypeSymbol? StringComparison { get; }

        internal INamedTypeSymbol? StringComparer { get; }

        internal INamedTypeSymbol? CultureInfo { get; }

        internal INamedTypeSymbol? ComparerOfT { get; }

        internal INamedTypeSymbol? IComparableOfT { get; }

        internal INamedTypeSymbol? IComparable { get; }

        internal INamedTypeSymbol? IComparerOfT { get; }

        internal INamedTypeSymbol? IComparer { get; }

        internal INamedTypeSymbol? Enumerable { get; }

        internal INamedTypeSymbol? ListOfT { get; }

        internal INamedTypeSymbol? Array { get; }
    }
}
