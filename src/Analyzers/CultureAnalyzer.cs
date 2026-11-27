using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dungeon.Analyzers;

/// <summary>
/// DUN005: culture-sensitive string operations (DS11). Reports <c>ToUpper</c>/<c>ToLower</c> on
/// <c>string</c> and <c>char</c>; <c>string</c> comparison overloads that take no
/// <c>StringComparison</c> and compare strings rather than chars; any <c>StringComparison</c>
/// argument that is not a constant <c>Ordinal</c>/<c>OrdinalIgnoreCase</c>; the culture-aware
/// <c>StringComparer</c> properties; and <c>CultureInfo.CurrentCulture</c>/<c>CurrentUICulture</c>.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class CultureAnalyzer : DiagnosticAnalyzer
{
    private const string CaseAdvice = "use ToUpperInvariant or ToLowerInvariant";
    private const string ComparisonAdvice = "pass StringComparison.Ordinal or OrdinalIgnoreCase";
    private const string ComparerAdvice = "use StringComparer.Ordinal or OrdinalIgnoreCase";
    private const string CultureAdvice = "use CultureInfo.InvariantCulture";

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
            var stringComparison = start.Compilation.GetTypeByMetadataName("System.StringComparison");
            var stringComparer = start.Compilation.GetTypeByMetadataName("System.StringComparer");
            var cultureInfo = start.Compilation.GetTypeByMetadataName("System.Globalization.CultureInfo");

            start.RegisterOperationAction(
                c => AnalyzeInvocation(c, (IInvocationOperation)c.Operation, stringComparison),
                OperationKind.Invocation);
            start.RegisterOperationAction(
                c => AnalyzeMethodGroup(c, (IMethodReferenceOperation)c.Operation, stringComparison),
                OperationKind.MethodReference);
            start.RegisterOperationAction(
                c => AnalyzeProperty(c, (IPropertyReferenceOperation)c.Operation, stringComparer, cultureInfo),
                OperationKind.PropertyReference);
        });
    }

    private static void AnalyzeInvocation(
        OperationAnalysisContext context, IInvocationOperation invocation, INamedTypeSymbol? stringComparison)
    {
        var method = invocation.TargetMethod;
        if (IsCultureSensitiveOverload(method, stringComparison))
        {
            Report(context, invocation.Syntax.GetLocation(), Describe(method), AdviceFor(method));
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
        INamedTypeSymbol? stringComparer,
        INamedTypeSymbol? cultureInfo)
    {
        var property = reference.Property;
        var owner = property.ContainingType;
        if (stringComparer is not null
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

    private static bool IsOrdinalConstant(IOperation value) =>
        value.ConstantValue is { HasValue: true, Value: int raw }
        && (raw == (int)StringComparison.Ordinal || raw == (int)StringComparison.OrdinalIgnoreCase);

    private static string Describe(IMethodSymbol method) =>
        method.ContainingType.Name + "." + method.Name;

    private static string AdviceFor(IMethodSymbol method) =>
        CaseMethods.Contains(method.Name) ? CaseAdvice : ComparisonAdvice;

    private static void Report(OperationAnalysisContext context, Location location, string construct, string advice) =>
        context.ReportDiagnostic(Diagnostic.Create(Rules.Culture, location, construct, advice));
}
