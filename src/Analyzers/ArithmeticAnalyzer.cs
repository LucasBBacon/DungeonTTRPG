using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Dungeon.Analyzers;

/// <summary>
/// DUN010: <c>/</c>, <c>%</c>, <c>/=</c>, <c>%=</c> and <c>Math.DivRem</c> on signed integral types,
/// unless the result is a compile-time constant or the code is inside <c>Dungeon.Core.IntMath</c>.
/// DUN011: an <c>unchecked</c> expression or statement in any file but the one named by the
/// <c>DungeonWrappingArithmeticFile</c> build property (DS11).
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ArithmeticAnalyzer : DiagnosticAnalyzer
{
    /// <summary>The MSBuild property naming the one file allowed to wrap, relative to the project folder.</summary>
    public const string WrappingArithmeticFileProperty = "DungeonWrappingArithmeticFile";

    private const string DivisionAdvice = "use IntMath.FloorDiv or IntMath.FloorMod";
    private const string IntMathName = "Dungeon.Core.IntMath";

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rules.Division, Rules.Unchecked);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterOperationAction(AnalyzeBinary, OperationKind.Binary);
        context.RegisterOperationAction(AnalyzeCompoundAssignment, OperationKind.CompoundAssignment);
        context.RegisterOperationAction(AnalyzeInvocation, OperationKind.Invocation);
        context.RegisterCompilationStartAction(start =>
        {
            start.Options.AnalyzerConfigOptionsProvider.GlobalOptions.TryGetValue(
                "build_property." + WrappingArithmeticFileProperty,
                out var allowedFile);
            var suffix = string.IsNullOrWhiteSpace(allowedFile) ? null : "/" + Normalize(allowedFile!.Trim());
            start.RegisterSyntaxNodeAction(
                c => AnalyzeUnchecked(c, suffix),
                SyntaxKind.UncheckedExpression,
                SyntaxKind.UncheckedStatement);
        });
    }

    private static void AnalyzeBinary(OperationAnalysisContext context)
    {
        var binary = (IBinaryOperation)context.Operation;
        if (binary.OperatorKind is not (BinaryOperatorKind.Divide or BinaryOperatorKind.Remainder)
            || binary.OperatorMethod is not null
            || binary.ConstantValue.HasValue
            || !(IsSignedIntegral(binary.LeftOperand.Type) || IsSignedIntegral(binary.RightOperand.Type))
            || IsInsideIntMath(context.ContainingSymbol))
        {
            return;
        }
        Report(context, OperatorText(binary.OperatorKind, compound: false), binary.Type ?? binary.LeftOperand.Type);
    }

    private static void AnalyzeCompoundAssignment(OperationAnalysisContext context)
    {
        var compound = (ICompoundAssignmentOperation)context.Operation;
        if (compound.OperatorKind is not (BinaryOperatorKind.Divide or BinaryOperatorKind.Remainder)
            || compound.OperatorMethod is not null
            || !(IsSignedIntegral(compound.Target.Type) || IsSignedIntegral(compound.Value.Type))
            || IsInsideIntMath(context.ContainingSymbol))
        {
            return;
        }
        Report(context, OperatorText(compound.OperatorKind, compound: true), compound.Target.Type);
    }

    private static void AnalyzeInvocation(OperationAnalysisContext context)
    {
        var invocation = (IInvocationOperation)context.Operation;
        var method = invocation.TargetMethod;
        if (method.Name != "DivRem"
            || method.ContainingType?.ToDisplayString() != "System.Math"
            || method.Parameters.Length == 0
            || !IsSignedIntegral(method.Parameters[0].Type)
            || invocation.Arguments.Take(2).All(a => a.Value.ConstantValue.HasValue)
            || IsInsideIntMath(context.ContainingSymbol))
        {
            return;
        }
        Report(context, "Math.DivRem", method.Parameters[0].Type);
    }

    private static void AnalyzeUnchecked(SyntaxNodeAnalysisContext context, string? allowedSuffix)
    {
        if (allowedSuffix is not null
            && Normalize(context.Node.SyntaxTree.FilePath).EndsWith(allowedSuffix, System.StringComparison.Ordinal))
        {
            return;
        }

        var keyword = context.Node switch
        {
            CheckedExpressionSyntax expression => expression.Keyword,
            CheckedStatementSyntax statement => statement.Keyword,
            _ => default,
        };
        var advice = allowedSuffix is null
            ? "no file is named by " + WrappingArithmeticFileProperty + ", so wrapping arithmetic is not allowed anywhere"
            : "wrapping arithmetic belongs only in the file named by " + WrappingArithmeticFileProperty;
        context.ReportDiagnostic(Diagnostic.Create(Rules.Unchecked, keyword.GetLocation(), "unchecked", advice));
    }

    private static void Report(OperationAnalysisContext context, string construct, ITypeSymbol? type) =>
        context.ReportDiagnostic(Diagnostic.Create(
            Rules.Division,
            context.Operation.Syntax.GetLocation(),
            "signed " + construct + " on " + (type?.ToDisplayString() ?? "a signed integer"),
            DivisionAdvice));

    private static string OperatorText(BinaryOperatorKind kind, bool compound) =>
        (kind == BinaryOperatorKind.Divide ? "'/" : "'%") + (compound ? "='" : "'");

    private static bool IsSignedIntegral(ITypeSymbol? type) =>
        type?.SpecialType is SpecialType.System_SByte
            or SpecialType.System_Int16
            or SpecialType.System_Int32
            or SpecialType.System_Int64
            or SpecialType.System_IntPtr;

    private static bool IsInsideIntMath(ISymbol? symbol)
    {
        for (var type = symbol as INamedTypeSymbol ?? symbol?.ContainingType; type is not null; type = type.ContainingType)
        {
            if (type.ToDisplayString() == IntMathName)
            {
                return true;
            }
        }
        return false;
    }

    private static string Normalize(string path) => path.Replace('\\', '/');
}
