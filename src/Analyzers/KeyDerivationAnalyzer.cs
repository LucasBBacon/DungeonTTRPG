using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dungeon.Analyzers;

/// <summary>
/// DUN012 (DS1, SV 4.2): every way of opening a stream or deriving a key by hand, in any file not named by the
/// <c>DungeonKeyDerivationFiles</c> build property (<c>;</c>- or <c>|</c>-separated paths relative to the project folder,
/// matched as the DUN011 file is). Reported: an invocation of, or method-group reference to,
/// <c>Dungeon.Core.KeyAlgebra.Derive</c>, <c>KeyAlgebra.H</c>, <c>KeyAlgebra.Absorb</c> or <c>Dungeon.Core.DrawStream.Open</c>;
/// and a <c>new</c> (any constructor, target-typed or not), an explicit <c>default</c> or a <c>with</c> expression of a
/// forgeable type: <c>DrawStream</c>, <c>Dungeon.Core.StreamHandle</c>, or a generated key struct (marked
/// <c>[Dungeon.Core.StreamKey]</c>). Matching is by symbol, so <c>using static</c> and aliases do not hide a call. <c>LabelId</c>, <c>Draw</c>, <c>Bound</c> and <c>Mix</c> stay allowed; a stream's own
/// <c>Next</c>, <c>NextBelow</c> and <c>Skip</c> advance a stream that was opened through a handle.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class KeyDerivationAnalyzer : DiagnosticAnalyzer
{
    /// <summary>The MSBuild property naming the files allowed to derive keys and open streams raw.</summary>
    public const string KeyDerivationFilesProperty = "DungeonKeyDerivationFiles";

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rules.KeyDerivation);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        // Generated code is Core code too; only the generated handle file is exempt, and only by name.
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            var options = start.Options.AnalyzerConfigOptionsProvider.GlobalOptions;
            options.TryGetValue("build_property." + KeyDerivationFilesProperty, out var files);
            options.TryGetValue("build_property.ProjectDir", out var projectDir);
            var allowed = ProjectPath.AllowedList(files, projectDir);
            start.RegisterOperationAction(
                c => Analyze(c, ((IInvocationOperation)c.Operation).TargetMethod, allowed),
                OperationKind.Invocation);
            start.RegisterOperationAction(
                c => Analyze(c, ((IMethodReferenceOperation)c.Operation).Method, allowed),
                OperationKind.MethodReference);
            start.RegisterOperationAction(
                c => Report(c, Forgeable(c.Operation.Type) is { } name ? "new " + name : null, allowed),
                OperationKind.ObjectCreation);
            start.RegisterOperationAction(
                c => Report(c, Forgeable(c.Operation.Type) is { } name ? "default(" + name + ")" : null, allowed),
                OperationKind.DefaultValue);
            start.RegisterOperationAction(
                c => Report(c, Forgeable(((IWithOperation)c.Operation).Operand.Type) is { } name ? name + " with" : null, allowed),
                OperationKind.With);
        });
    }

    private static void Analyze(OperationAnalysisContext context, IMethodSymbol method, IReadOnlyList<ProjectPath> allowed)
        => Report(context, BannedMember(method), allowed);

    /// <summary>
    /// The type's name when building it by hand would carry a key no handle derived: <c>DrawStream</c>,
    /// <c>StreamHandle</c> or a <c>[StreamKey]</c> struct; otherwise null.
    /// </summary>
    private static string? Forgeable(ITypeSymbol? type)
    {
        if (type is null)
        {
            return null;
        }

        var name = type.OriginalDefinition.ToDisplayString();
        if (name is "Dungeon.Core.DrawStream" or "Dungeon.Core.StreamHandle")
        {
            return type.Name;
        }

        foreach (var attribute in type.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString() == "Dungeon.Core.StreamKeyAttribute")
            {
                return type.Name;
            }
        }

        return null;
    }

    private static void Report(OperationAnalysisContext context, string? member, IReadOnlyList<ProjectPath> allowed)
    {
        if (member is null)
        {
            return;
        }

        var filePath = context.Operation.Syntax.SyntaxTree.FilePath;
        if (allowed.Any(path => path.Matches(filePath)))
        {
            return;
        }
        context.ReportDiagnostic(Diagnostic.Create(Rules.KeyDerivation, context.Operation.Syntax.GetLocation(), member));
    }

    /// <summary>The banned member's display name (<c>KeyAlgebra.Derive</c>, ...), or null when it is allowed.</summary>
    private static string? BannedMember(IMethodSymbol method)
    {
        var owner = method.ContainingType?.OriginalDefinition.ToDisplayString();
        return (owner, method.Name) switch
        {
            ("Dungeon.Core.KeyAlgebra", "Derive") => "KeyAlgebra.Derive",
            ("Dungeon.Core.KeyAlgebra", "H") => "KeyAlgebra.H",
            ("Dungeon.Core.KeyAlgebra", "Absorb") => "KeyAlgebra.Absorb",
            ("Dungeon.Core.DrawStream", "Open") => "DrawStream.Open",
            _ => null,
        };
    }
}
