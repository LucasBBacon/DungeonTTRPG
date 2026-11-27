using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dungeon.Analyzers;

/// <summary>
/// DUN002, DUN003, DUN004, DUN008 and DUN009: banned library APIs (DS11). Banned types and members
/// are resolved once per compilation from a table of metadata names and matched by symbol identity.
/// DUN009 is reported at each source location that references a symbol in namespace <c>Godot</c>, and
/// additionally once per compilation, at no location, if the compilation references an assembly named
/// <c>GodotSharp</c> (even when no symbol from it is used).
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class BannedApiAnalyzer : DiagnosticAnalyzer
{
    private const string GodotAssembly = "GodotSharp";
    private const string GodotAdvice = "keep Godot out of Core";

    private static readonly Entry[] Table =
    {
        new("System.Random", null, Rules.SystemRandom, "use the seeded Dungeon.Core PCG stream"),
        new("System.DateTime", new[] { "Now", "UtcNow", "Today" }, Rules.Clock, "take time as an explicit input"),
        new("System.DateTimeOffset", new[] { "Now", "UtcNow" }, Rules.Clock, "take time as an explicit input"),
        new("System.Environment", new[] { "TickCount", "TickCount64" }, Rules.Clock, "take time as an explicit input"),
        new("System.Guid", new[] { "NewGuid" }, Rules.Clock, "derive ids from the seeded stream"),
        new("System.Diagnostics.Stopwatch", null, Rules.Clock, "take time as an explicit input"),
        new("System.TimeProvider", null, Rules.Clock, "take time as an explicit input"),
        new("System.HashCode", null, Rules.Hashing, "use an explicit stable hash"),
        new("System.Threading.Tasks.Parallel", null, Rules.Parallel, "run sequentially"),
        new("System.Linq.ParallelEnumerable", null, Rules.Parallel, "run sequentially"),
    };

    private static readonly ImmutableArray<OperationKind> OperationKinds = ImmutableArray.Create(
        OperationKind.ObjectCreation,
        OperationKind.Invocation,
        OperationKind.FieldReference,
        OperationKind.PropertyReference,
        OperationKind.MethodReference,
        OperationKind.EventReference,
        OperationKind.VariableDeclarator,
        OperationKind.TypeOf,
        OperationKind.IsType,
        OperationKind.DefaultValue);

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rules.SystemRandom, Rules.Clock, Rules.Hashing, Rules.Parallel, Rules.Godot);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            var banned = new Dictionary<INamedTypeSymbol, Entry>(SymbolEqualityComparer.Default);
            foreach (var entry in Table)
            {
                if (start.Compilation.GetTypeByMetadataName(entry.MetadataName) is { } type)
                {
                    banned[type] = entry;
                }
            }

            var checker = new Checker(banned);
            start.RegisterOperationAction(c => AnalyzeOperation(c, checker), OperationKinds);
            start.RegisterSymbolAction(
                c => AnalyzeSymbol(c, checker),
                SymbolKind.Field,
                SymbolKind.Property,
                SymbolKind.Event,
                SymbolKind.Method,
                SymbolKind.NamedType);
            start.RegisterCompilationEndAction(c =>
            {
                if (c.Compilation.ReferencedAssemblyNames.Any(
                        a => string.Equals(a.Name, GodotAssembly, StringComparison.OrdinalIgnoreCase)))
                {
                    c.ReportDiagnostic(Diagnostic.Create(
                        Rules.Godot, Location.None, "a reference to " + GodotAssembly, GodotAdvice));
                }
            });
        });
    }

    private static void AnalyzeOperation(OperationAnalysisContext context, Checker checker)
    {
        var operation = context.Operation;
        var location = operation.Syntax.GetLocation();
        void Check(ISymbol? symbol, Location at) => checker.Check(symbol, at, context.ReportDiagnostic);

        Check(operation.Type, location);
        switch (operation)
        {
            case IInvocationOperation invocation:
                Check(invocation.TargetMethod, location);
                if (invocation.TargetMethod.Name == "GetHashCode")
                {
                    checker.Report(Rules.Hashing, "GetHashCode", "use an explicit stable hash", location, context.ReportDiagnostic);
                }
                break;
            case IMemberReferenceOperation member:
                Check(member.Member, location);
                break;
            case IObjectCreationOperation creation:
                Check(creation.Constructor?.ContainingType, location);
                break;
            case IVariableDeclaratorOperation declarator:
                Check(declarator.Symbol.Type, declarator.Symbol.Locations.FirstOrDefault(l => l.IsInSource) ?? location);
                break;
            case ITypeOfOperation typeOf:
                Check(typeOf.TypeOperand, location);
                break;
            case IIsTypeOperation isType:
                Check(isType.TypeOperand, location);
                break;
        }
    }

    private static void AnalyzeSymbol(SymbolAnalysisContext context, Checker checker)
    {
        var symbol = context.Symbol;
        var location = symbol.Locations.FirstOrDefault(l => l.IsInSource) ?? Location.None;
        void Check(ISymbol? s) => checker.Check(s, location, context.ReportDiagnostic);

        switch (symbol)
        {
            case IFieldSymbol field:
                Check(field.Type);
                break;
            case IPropertySymbol property:
                Check(property.Type);
                foreach (var parameter in property.Parameters)
                {
                    Check(parameter.Type);
                }
                break;
            case IEventSymbol @event:
                Check(@event.Type);
                break;
            case IMethodSymbol method when method.AssociatedSymbol is null:
                // Accessors are covered by their property or event.
                Check(method.ReturnType);
                foreach (var parameter in method.Parameters)
                {
                    Check(parameter.Type);
                }
                break;
            case INamedTypeSymbol type:
                Check(type.BaseType);
                foreach (var @interface in type.Interfaces)
                {
                    Check(@interface);
                }
                break;
        }
    }

    /// <summary>One banned type: every member when <c>Members</c> is null, otherwise just those named.</summary>
    private sealed class Entry
    {
        internal Entry(string metadataName, string[]? members, DiagnosticDescriptor rule, string advice)
        {
            MetadataName = metadataName;
            Members = members;
            Rule = rule;
            Advice = advice;
        }

        internal string MetadataName { get; }

        internal string[]? Members { get; }

        internal DiagnosticDescriptor Rule { get; }

        internal string Advice { get; }
    }

    /// <summary>Per-compilation matcher; reports each rule at most once per location.</summary>
    private sealed class Checker
    {
        private readonly Dictionary<INamedTypeSymbol, Entry> _banned;
        private readonly ConcurrentDictionary<(Location, string), byte> _reported = new();

        internal Checker(Dictionary<INamedTypeSymbol, Entry> banned) => _banned = banned;

        /// <summary>Reports <paramref name="symbol"/> (a type, or a member of one) if it is banned.</summary>
        internal void Check(ISymbol? symbol, Location location, Action<Diagnostic> report)
        {
            if (symbol is null || !location.IsInSource)
            {
                return;
            }

            if (symbol is ITypeSymbol type)
            {
                CheckType(type, location, report);
            }
            else if (symbol.ContainingType is { } owner)
            {
                // A member is banned when its containing type is, and the entry covers its name.
                var original = owner.OriginalDefinition;
                if (_banned.TryGetValue(original, out var entry)
                    && (entry.Members is null || entry.Members.Contains(symbol.Name, StringComparer.Ordinal)))
                {
                    Report(entry.Rule, original.Name + "." + symbol.Name, entry.Advice, location, report);
                }
                else
                {
                    CheckType(owner, location, report);
                }
            }
        }

        internal void Report(DiagnosticDescriptor rule, string construct, string advice, Location location, Action<Diagnostic> report)
        {
            if (location.IsInSource && _reported.TryAdd((location, rule.Id), 0))
            {
                report(Diagnostic.Create(rule, location, construct, advice));
            }
        }

        private void CheckType(ITypeSymbol? type, Location location, Action<Diagnostic> report)
        {
            switch (type)
            {
                case IArrayTypeSymbol array:
                    CheckType(array.ElementType, location, report);
                    break;
                case IPointerTypeSymbol pointer:
                    CheckType(pointer.PointedAtType, location, report);
                    break;
                case INamedTypeSymbol named:
                    // A type is flagged as a whole only when every member is banned; selective entries
                    // (DateTime, Environment, ...) are flagged at the member.
                    if (_banned.TryGetValue(named.OriginalDefinition, out var entry) && entry.Members is null)
                    {
                        Report(entry.Rule, named.OriginalDefinition.Name, entry.Advice, location, report);
                    }
                    else if (IsInGodotNamespace(named))
                    {
                        Report(Rules.Godot, named.ToDisplayString(), GodotAdvice, location, report);
                    }
                    foreach (var argument in named.TypeArguments)
                    {
                        CheckType(argument, location, report);
                    }
                    CheckType(named.ContainingType, location, report);
                    break;
            }
        }

        private static bool IsInGodotNamespace(INamedTypeSymbol type)
        {
            // The outermost namespace below global must be Godot.
            var ns = type.ContainingNamespace;
            while (ns is { IsGlobalNamespace: false })
            {
                if (ns.ContainingNamespace is { IsGlobalNamespace: true })
                {
                    return ns.Name == "Godot";
                }
                ns = ns.ContainingNamespace;
            }
            return false;
        }
    }
}
