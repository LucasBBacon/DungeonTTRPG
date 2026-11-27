using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dungeon.Analyzers;

/// <summary>
/// DUN001: reports any expression, declaration or type reference of <c>float</c>, <c>double</c>
/// or <c>decimal</c>, including literals and <c>var</c> bound to them (DS11).
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class FloatingPointAnalyzer : DiagnosticAnalyzer
{
    private const string Advice = "use int or long fixed-point arithmetic";

    private static readonly ImmutableArray<OperationKind> AllOperationKinds =
        Enum.GetValues(typeof(OperationKind))
            .Cast<OperationKind>()
            .Where(k => k != OperationKind.None)
            .Distinct()
            .ToImmutableArray();

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rules.FloatingPoint);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            var reported = new ConcurrentDictionary<Location, byte>();
            start.RegisterOperationAction(c => AnalyzeOperation(c, reported), AllOperationKinds);
            start.RegisterSymbolAction(
                c => AnalyzeSymbol(c, reported),
                SymbolKind.Field,
                SymbolKind.Property,
                SymbolKind.Event,
                SymbolKind.Method,
                SymbolKind.NamedType);
        });
    }

    private static void AnalyzeOperation(OperationAnalysisContext context, ConcurrentDictionary<Location, byte> reported)
    {
        var operation = context.Operation;
        void Report(ITypeSymbol? found, Location location) =>
            ReportOnce(found, location, reported, context.ReportDiagnostic);

        // Report the outermost floating expression only; its floating operands are the same construct.
        var own = FindFloating(operation.Type);
        if (own is not null && FindFloating(operation.Parent?.Type) is null)
        {
            Report(own, operation.Syntax.GetLocation());
        }

        switch (operation)
        {
            case IVariableDeclaratorOperation declarator:
                Report(FindFloating(declarator.Symbol.Type), FirstSourceLocation(declarator.Symbol));
                break;
            case ITypeOfOperation typeOf:
                Report(FindFloating(typeOf.TypeOperand), operation.Syntax.GetLocation());
                break;
            case ISizeOfOperation sizeOf:
                Report(FindFloating(sizeOf.TypeOperand), operation.Syntax.GetLocation());
                break;
            case IIsTypeOperation isType:
                Report(FindFloating(isType.TypeOperand), operation.Syntax.GetLocation());
                break;
            case IPatternOperation pattern:
                Report(FindFloating(pattern.NarrowedType), operation.Syntax.GetLocation());
                break;
            case IInvocationOperation invocation:
                foreach (var argument in invocation.TargetMethod.TypeArguments)
                {
                    Report(FindFloating(argument), operation.Syntax.GetLocation());
                }
                break;
            case ILocalFunctionOperation localFunction:
                AnalyzeSignature(localFunction.Symbol, Report);
                break;
            case IAnonymousFunctionOperation lambda:
                AnalyzeSignature(lambda.Symbol, Report);
                break;
        }
    }

    private static void AnalyzeSymbol(SymbolAnalysisContext context, ConcurrentDictionary<Location, byte> reported)
    {
        void Report(ITypeSymbol? found, Location location) =>
            ReportOnce(found, location, reported, context.ReportDiagnostic);

        var symbol = context.Symbol;
        var location = FirstSourceLocation(symbol);
        switch (symbol)
        {
            case IFieldSymbol field:
                Report(FindFloating(field.Type), location);
                break;
            case IPropertySymbol property:
                Report(FindFloating(property.Type), location);
                foreach (var parameter in property.Parameters)
                {
                    Report(FindFloating(parameter.Type), FirstSourceLocation(parameter));
                }
                break;
            case IEventSymbol @event:
                Report(FindFloating(@event.Type), location);
                break;
            case IMethodSymbol method when method.AssociatedSymbol is null:
                // Accessors are covered by their property or event.
                AnalyzeSignature(method, Report);
                break;
            case INamedTypeSymbol type:
                Report(FindFloating(type.BaseType), location);
                foreach (var @interface in type.Interfaces)
                {
                    Report(FindFloating(@interface), location);
                }
                // A delegate's implicitly declared Invoke never reaches the method case.
                if (type.DelegateInvokeMethod is { } invoke)
                {
                    AnalyzeSignature(invoke, Report);
                }
                break;
        }
    }

    private static void AnalyzeSignature(IMethodSymbol method, Action<ITypeSymbol?, Location> report)
    {
        report(FindFloating(method.ReturnType), FirstSourceLocation(method));
        foreach (var parameter in method.Parameters)
        {
            report(FindFloating(parameter.Type), FirstSourceLocation(parameter));
        }
    }

    private static void ReportOnce(
        ITypeSymbol? found,
        Location location,
        ConcurrentDictionary<Location, byte> reported,
        Action<Diagnostic> report)
    {
        if (found is null || !location.IsInSource || !reported.TryAdd(location, 0))
        {
            return;
        }
        report(Diagnostic.Create(Rules.FloatingPoint, location, found.ToDisplayString(), Advice));
    }

    private static Location FirstSourceLocation(ISymbol symbol) =>
        symbol.Locations.FirstOrDefault(l => l.IsInSource) ?? Location.None;

    /// <summary>The first <c>float</c>, <c>double</c> or <c>decimal</c> in <paramref name="type"/>, including element and type arguments.</summary>
    private static ITypeSymbol? FindFloating(ITypeSymbol? type)
    {
        switch (type)
        {
            case null:
                return null;
            case { SpecialType: SpecialType.System_Single or SpecialType.System_Double or SpecialType.System_Decimal }:
                return type;
            case IArrayTypeSymbol array:
                return FindFloating(array.ElementType);
            case IPointerTypeSymbol pointer:
                return FindFloating(pointer.PointedAtType);
            case INamedTypeSymbol named:
                return named.TypeArguments.Select(FindFloating).FirstOrDefault(t => t is not null)
                    ?? FindFloating(named.ContainingType);
            default:
                return null;
        }
    }
}
