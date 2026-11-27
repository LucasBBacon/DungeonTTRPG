using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dungeon.Analyzers;

/// <summary>
/// DUN006: enumeration of unordered collections (DS11). An unordered type is <c>Dictionary&lt;,&gt;</c>,
/// <c>HashSet&lt;&gt;</c>, the dictionary's nested <c>KeyCollection</c>/<c>ValueCollection</c>,
/// <c>ImmutableDictionary&lt;,&gt;</c>, <c>ImmutableHashSet&lt;&gt;</c>, <c>ConcurrentDictionary&lt;,&gt;</c>, or a
/// type derived from one. Reported: <c>foreach</c> over one; any conversion of one to
/// <c>IEnumerable</c>/<c>IEnumerable&lt;T&gt;</c> (LINQ, parameters); a collection-expression spread of
/// one; and calls of its <c>GetEnumerator</c> or <c>CopyTo</c>. Lookups (<c>TryGetValue</c>, indexers,
/// <c>Contains</c>, <c>Count</c>) stay legal.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UnorderedEnumerationAnalyzer : DiagnosticAnalyzer
{
    private const string Advice =
        "iterate an ordered collection instead (List<T>, ImmutableArray<T>, or a sorted collection with a registered comparer)";

    private static readonly string[] UnorderedTypeNames =
    {
        "System.Collections.Generic.Dictionary`2",
        "System.Collections.Generic.Dictionary`2+KeyCollection",
        "System.Collections.Generic.Dictionary`2+ValueCollection",
        "System.Collections.Generic.HashSet`1",
        "System.Collections.Immutable.ImmutableDictionary`2",
        "System.Collections.Immutable.ImmutableHashSet`1",
        "System.Collections.Concurrent.ConcurrentDictionary`2",
    };

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rules.UnorderedEnumeration);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            var compilation = start.Compilation;
            var unordered = UnorderedTypeNames
                .Select(compilation.GetTypeByMetadataName)
                .Where(t => t is not null)
                .Select(t => t!)
                .ToImmutableHashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
            var enumerables = new[]
                {
                    compilation.GetTypeByMetadataName("System.Collections.IEnumerable"),
                    compilation.GetSpecialType(SpecialType.System_Collections_Generic_IEnumerable_T),
                }
                .Where(t => t is not null)
                .Select(t => t!)
                .ToImmutableHashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

            start.RegisterOperationAction(
                c =>
                {
                    if (c.Operation is IForEachLoopOperation loop)
                    {
                        AnalyzeForEach(c, loop, unordered);
                    }
                },
                OperationKind.Loop);
            start.RegisterOperationAction(
                c => AnalyzeConversion(c, (IConversionOperation)c.Operation, unordered, enumerables),
                OperationKind.Conversion);
            start.RegisterSyntaxNodeAction(
                c => AnalyzeSpread(c, (SpreadElementSyntax)c.Node, unordered),
                SyntaxKind.SpreadElement);
            start.RegisterOperationAction(
                c => AnalyzeInvocation(c, (IInvocationOperation)c.Operation, unordered),
                OperationKind.Invocation);
        });
    }

    private static void AnalyzeForEach(
        OperationAnalysisContext context, IForEachLoopOperation loop, ImmutableHashSet<INamedTypeSymbol> unordered)
    {
        var collection = StripConversions(loop.Collection);
        if (UnorderedName(collection.Type, unordered) is { } name)
        {
            Report(context, collection.Syntax.GetLocation(), "foreach over " + name);
        }
    }

    private static void AnalyzeConversion(
        OperationAnalysisContext context,
        IConversionOperation conversion,
        ImmutableHashSet<INamedTypeSymbol> unordered,
        ImmutableHashSet<INamedTypeSymbol> enumerables)
    {
        // A foreach's own collection conversion is reported once, by AnalyzeForEach.
        if (conversion.Parent is IForEachLoopOperation)
        {
            return;
        }

        if (conversion.Type is INamedTypeSymbol target
            && enumerables.Contains(target.OriginalDefinition)
            && UnorderedName(conversion.Operand.Type, unordered) is { } name)
        {
            Report(context, conversion.Operand.Syntax.GetLocation(), "converting " + name + " to " + target.Name);
        }
    }

    /// <summary>A collection-expression spread (<c>[.. set]</c>) enumerates its operand; Roslyn 4.8 has no spread operation, so this works on syntax.</summary>
    private static void AnalyzeSpread(
        SyntaxNodeAnalysisContext context, SpreadElementSyntax spread, ImmutableHashSet<INamedTypeSymbol> unordered)
    {
        var type = context.SemanticModel.GetTypeInfo(spread.Expression, context.CancellationToken).Type;
        if (UnorderedName(type, unordered) is { } name)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                Rules.UnorderedEnumeration, spread.Expression.GetLocation(), "spreading " + name, Advice));
        }
    }

    private static void AnalyzeInvocation(
        OperationAnalysisContext context, IInvocationOperation invocation, ImmutableHashSet<INamedTypeSymbol> unordered)
    {
        var method = invocation.TargetMethod;
        if (method.Name is "GetEnumerator" or "CopyTo"
            && UnorderedName(invocation.Instance?.Type ?? method.ContainingType, unordered) is { } name)
        {
            Report(context, invocation.Syntax.GetLocation(), name + "." + method.Name);
        }
    }

    private static IOperation StripConversions(IOperation operation)
    {
        while (operation is IConversionOperation conversion)
        {
            operation = conversion.Operand;
        }
        return operation;
    }

    /// <summary>The unordered type <paramref name="type"/> is or derives from, by name; null when there is none.</summary>
    private static string? UnorderedName(ITypeSymbol? type, ImmutableHashSet<INamedTypeSymbol> unordered)
    {
        for (var current = type as INamedTypeSymbol; current is not null; current = current.BaseType)
        {
            if (unordered.Contains(current.OriginalDefinition))
            {
                return current.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            }
        }
        return null;
    }

    private static void Report(OperationAnalysisContext context, Location location, string construct) =>
        context.ReportDiagnostic(Diagnostic.Create(Rules.UnorderedEnumeration, location, construct, Advice));
}
