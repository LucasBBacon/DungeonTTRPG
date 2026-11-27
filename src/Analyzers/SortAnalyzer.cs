using System;
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
/// DUN007: sorts without a registered total comparer (DS11). Covers <c>Sort</c> on
/// <c>List&lt;T&gt;</c>, <c>Array</c>, <c>ImmutableArray&lt;T&gt;</c> and its <c>Builder</c>,
/// <c>ImmutableList&lt;T&gt;</c> and its <c>Builder</c>, and <c>MemoryExtensions</c>;
/// <c>Enumerable.Order*</c>/<c>ThenBy*</c>; the <c>SortedSet</c>, <c>SortedDictionary</c> and
/// <c>SortedList</c> constructors, including collection expressions that build one; and the
/// <c>ImmutableSortedDictionary</c>/<c>ImmutableSortedSet</c> factories (<c>Create</c>, <c>CreateRange</c>,
/// <c>CreateBuilder</c>, <c>ToImmutableSorted*</c>, collection expressions) and their static <c>Empty</c>
/// (which always uses the default comparer). A comparer is registered when its static type carries
/// <c>[Dungeon.Core.Determinism.TotalComparer]</c> or it is <c>StringComparer.Ordinal</c>. With no
/// comparer, <c>Order*</c>/<c>ThenBy*</c> pass only for an integral primitive or enum key.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SortAnalyzer : DiagnosticAnalyzer
{
    private const string AttributeName = "Dungeon.Core.Determinism.TotalComparerAttribute";
    private const string Advice = "pass a comparer marked [TotalComparer] or StringComparer.Ordinal";

    private static readonly string[] SortOwners =
    {
        "System.Collections.Generic.List`1",
        "System.Array",
        "System.Collections.Immutable.ImmutableArray`1",
        "System.Collections.Immutable.ImmutableArray`1+Builder",
        "System.Collections.Immutable.ImmutableList`1",
        "System.Collections.Immutable.ImmutableList`1+Builder",
        "System.MemoryExtensions",
    };

    /// <summary>Static factory classes whose methods build an immutable sorted collection.</summary>
    private static readonly string[] SortedFactories =
    {
        "System.Collections.Immutable.ImmutableSortedDictionary",
        "System.Collections.Immutable.ImmutableSortedSet",
    };

    /// <summary>Immutable sorted collections: their static <c>Empty</c> uses the default comparer.</summary>
    private static readonly string[] ImmutableSortedCollections =
    {
        "System.Collections.Immutable.ImmutableSortedDictionary`2",
        "System.Collections.Immutable.ImmutableSortedSet`1",
    };

    private static readonly ImmutableHashSet<string> FactoryMethods = ImmutableHashSet.Create(
        StringComparer.Ordinal,
        "Create",
        "CreateRange",
        "CreateBuilder",
        "ToImmutableSortedDictionary",
        "ToImmutableSortedSet");

    private static readonly string[] SortedCollections =
    {
        "System.Collections.Generic.SortedSet`1",
        "System.Collections.Generic.SortedDictionary`2",
        "System.Collections.Generic.SortedList`2",
    };

    /// <summary><c>Enumerable</c> methods whose key is the element itself (the rest take a key selector).</summary>
    private static readonly ImmutableHashSet<string> ElementKeyed = ImmutableHashSet.Create(
        StringComparer.Ordinal, "Order", "OrderDescending");

    private static readonly ImmutableHashSet<string> LinqSorts = ImmutableHashSet.Create(
        StringComparer.Ordinal, "Order", "OrderDescending", "OrderBy", "OrderByDescending", "ThenBy", "ThenByDescending");

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rules.Sort);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            var compilation = start.Compilation;
            var known = new KnownTypes(
                Resolve(compilation, SortOwners),
                Resolve(compilation, SortedCollections),
                Resolve(compilation, SortedFactories),
                Resolve(compilation, ImmutableSortedCollections),
                compilation.GetTypeByMetadataName("System.Linq.Enumerable"),
                compilation.GetTypeByMetadataName(AttributeName),
                compilation.GetTypeByMetadataName("System.StringComparer"),
                Resolve(compilation, new[]
                {
                    "System.Collections.Generic.IComparer`1",
                    "System.Collections.IComparer",
                    "System.Comparison`1",
                }));

            start.RegisterOperationAction(
                c => AnalyzeInvocation(c, (IInvocationOperation)c.Operation, known),
                OperationKind.Invocation);
            start.RegisterOperationAction(
                c => AnalyzeCreation(c, (IObjectCreationOperation)c.Operation, known),
                OperationKind.ObjectCreation);
            start.RegisterOperationAction(
                c => AnalyzeEmpty(c, (IMemberReferenceOperation)c.Operation, known),
                OperationKind.FieldReference,
                OperationKind.PropertyReference);
            start.RegisterSyntaxNodeAction(
                c => AnalyzeCollectionExpression(c, (CollectionExpressionSyntax)c.Node, known),
                SyntaxKind.CollectionExpression);
        });
    }

    private static void AnalyzeInvocation(OperationAnalysisContext context, IInvocationOperation invocation, KnownTypes known)
    {
        var method = invocation.TargetMethod;
        var owner = method.ContainingType?.OriginalDefinition;
        if (owner is null)
        {
            return;
        }

        if (method.Name == "Sort" && known.SortOwners.Contains(owner))
        {
            if (!HasRegisteredComparer(invocation.Arguments, known))
            {
                Report(context, invocation.Syntax.GetLocation(), owner.Name + ".Sort without a registered comparer");
            }
        }
        else if (FactoryMethods.Contains(method.Name) && known.SortedFactories.Contains(owner))
        {
            if (!HasRegisteredComparer(invocation.Arguments, known))
            {
                Report(context, invocation.Syntax.GetLocation(), owner.Name + "." + method.Name + " without a registered comparer");
            }
        }
        else if (SymbolEqualityComparer.Default.Equals(owner, known.Enumerable) && LinqSorts.Contains(method.Name))
        {
            var comparer = FindComparer(invocation.Arguments, known);
            var ok = comparer is null
                ? IsTotalKey(method.TypeArguments[ElementKeyed.Contains(method.Name) ? 0 : 1])
                : IsRegistered(comparer, known);
            if (!ok)
            {
                Report(context, invocation.Syntax.GetLocation(), "Enumerable." + method.Name + " without a registered comparer");
            }
        }
    }

    private static void AnalyzeCreation(OperationAnalysisContext context, IObjectCreationOperation creation, KnownTypes known)
    {
        var type = creation.Constructor?.ContainingType?.OriginalDefinition;
        if (type is not null
            && known.SortedCollections.Contains(type)
            && !HasRegisteredComparer(creation.Arguments, known))
        {
            Report(context, creation.Syntax.GetLocation(), "new " + type.Name + " without a registered comparer");
        }
    }

    /// <summary>
    /// <c>ImmutableSortedDictionary&lt;K, V&gt;.Empty</c> and <c>ImmutableSortedSet&lt;T&gt;.Empty</c> are built
    /// with the default comparer, so every read is reported; build with a factory that takes a comparer instead.
    /// </summary>
    private static void AnalyzeEmpty(OperationAnalysisContext context, IMemberReferenceOperation reference, KnownTypes known)
    {
        var member = reference.Member;
        if (member.IsStatic
            && member.Name == "Empty"
            && member.ContainingType?.OriginalDefinition is { } owner
            && known.ImmutableSortedCollections.Contains(owner))
        {
            Report(context, reference.Syntax.GetLocation(), owner.Name + ".Empty (it uses the default comparer)");
        }
    }

    /// <summary>
    /// A collection expression (<c>SortedSet&lt;string&gt; s = [];</c>) always calls the parameterless
    /// constructor or the comparer-less <c>Create</c>, so it can never pass a comparer: every one that builds a
    /// sorted collection is reported.
    /// </summary>
    private static void AnalyzeCollectionExpression(SyntaxNodeAnalysisContext context, CollectionExpressionSyntax node, KnownTypes known)
    {
        var type = context.SemanticModel.GetTypeInfo(node, context.CancellationToken).ConvertedType?.OriginalDefinition;
        if (type is INamedTypeSymbol named
            && (known.SortedCollections.Contains(named) || known.ImmutableSortedCollections.Contains(named)))
        {
            context.ReportDiagnostic(Diagnostic.Create(
                Rules.Sort, node.GetLocation(), "collection expression of " + named.Name + " (it cannot take a comparer)", Advice));
        }
    }

    private static bool HasRegisteredComparer(ImmutableArray<IArgumentOperation> arguments, KnownTypes known) =>
        FindComparer(arguments, known) is { } comparer && IsRegistered(comparer, known);

    /// <summary>The explicitly passed comparer or comparison argument, or null when there is none.</summary>
    private static IOperation? FindComparer(ImmutableArray<IArgumentOperation> arguments, KnownTypes known)
    {
        foreach (var argument in arguments)
        {
            if (argument.Parameter is { } parameter
                && argument.ArgumentKind != ArgumentKind.DefaultValue
                && IsComparerType(parameter.OriginalDefinition.Type, known))
            {
                return argument.Value;
            }
        }
        return null;
    }

    private static bool IsComparerType(ITypeSymbol type, KnownTypes known) => type switch
    {
        // MemoryExtensions.Sort<T, TComparer>(Span<T>, TComparer) where TComparer : IComparer<T>.
        ITypeParameterSymbol parameter => parameter.ConstraintTypes.Any(t => IsComparerType(t, known)),
        INamedTypeSymbol named => known.ComparerTypes.Contains(named.OriginalDefinition),
        _ => false,
    };

    private static bool IsRegistered(IOperation comparer, KnownTypes known)
    {
        while (comparer is IConversionOperation conversion)
        {
            comparer = conversion.Operand;
        }

        if (comparer is IPropertyReferenceOperation { Property.Name: "Ordinal" } property
            && SymbolEqualityComparer.Default.Equals(property.Property.ContainingType, known.StringComparer))
        {
            return true;
        }

        return known.Attribute is not null
            && comparer.Type is { } type
            && type.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, known.Attribute));
    }

    private static bool IsTotalKey(ITypeSymbol key) =>
        key.TypeKind == TypeKind.Enum
        || key.SpecialType is SpecialType.System_SByte
            or SpecialType.System_Byte
            or SpecialType.System_Int16
            or SpecialType.System_UInt16
            or SpecialType.System_Int32
            or SpecialType.System_UInt32
            or SpecialType.System_Int64
            or SpecialType.System_UInt64
            or SpecialType.System_Char;

    private static ImmutableHashSet<INamedTypeSymbol> Resolve(Compilation compilation, IEnumerable<string> names) =>
        names
            .Select(compilation.GetTypeByMetadataName)
            .Where(t => t is not null)
            .Select(t => t!)
            .ToImmutableHashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

    private static void Report(OperationAnalysisContext context, Location location, string construct) =>
        context.ReportDiagnostic(Diagnostic.Create(Rules.Sort, location, construct, Advice));

    /// <summary>The symbols one compilation resolves for the rule.</summary>
    private sealed class KnownTypes
    {
        internal KnownTypes(
            ImmutableHashSet<INamedTypeSymbol> sortOwners,
            ImmutableHashSet<INamedTypeSymbol> sortedCollections,
            ImmutableHashSet<INamedTypeSymbol> sortedFactories,
            ImmutableHashSet<INamedTypeSymbol> immutableSortedCollections,
            INamedTypeSymbol? enumerable,
            INamedTypeSymbol? attribute,
            INamedTypeSymbol? stringComparer,
            ImmutableHashSet<INamedTypeSymbol> comparerTypes)
        {
            SortOwners = sortOwners;
            SortedCollections = sortedCollections;
            SortedFactories = sortedFactories;
            ImmutableSortedCollections = immutableSortedCollections;
            Enumerable = enumerable;
            Attribute = attribute;
            StringComparer = stringComparer;
            ComparerTypes = comparerTypes;
        }

        internal ImmutableHashSet<INamedTypeSymbol> SortOwners { get; }

        internal ImmutableHashSet<INamedTypeSymbol> SortedCollections { get; }

        internal ImmutableHashSet<INamedTypeSymbol> SortedFactories { get; }

        internal ImmutableHashSet<INamedTypeSymbol> ImmutableSortedCollections { get; }

        internal INamedTypeSymbol? Enumerable { get; }

        internal INamedTypeSymbol? Attribute { get; }

        internal INamedTypeSymbol? StringComparer { get; }

        internal ImmutableHashSet<INamedTypeSymbol> ComparerTypes { get; }
    }
}
