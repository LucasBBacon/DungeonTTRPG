using System.Collections.Immutable;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>Compiles a fixture with every analyzer in <c>Dungeon.Analyzers</c> and returns what they report.</summary>
internal static class Harness
{
    private static readonly Lazy<ImmutableArray<MetadataReference>> References = new(LoadReferences);
    private static readonly Lazy<ImmutableArray<DiagnosticAnalyzer>> Analyzers = new(LoadAnalyzers);

    /// <summary>
    /// Compiles <paramref name="source"/> as C# 12 with nullable on, asserts it has no compiler errors,
    /// and returns the analyzer diagnostics only. <paramref name="buildProperties"/> keys are bare
    /// names, surfaced to analyzers as <c>build_property.&lt;name&gt;</c>. <paramref name="assemblyName"/> names the
    /// fixture assembly; <c>"Core.Tests"</c> is the name <c>Dungeon.Core</c> grants its internals to.
    /// </summary>
    internal static ImmutableArray<Diagnostic> Run(
        string source,
        string path = "Test.cs",
        IReadOnlyDictionary<string, string>? buildProperties = null,
        IEnumerable<MetadataReference>? extraReferences = null,
        string assemblyName = "Fixture")
    {
        var tree = CSharpSyntaxTree.ParseText(
            source,
            new CSharpParseOptions(LanguageVersion.CSharp12),
            path);
        var compilation = CSharpCompilation.Create(
            assemblyName,
            new[] { tree },
            References.Value.AddRange(extraReferences ?? Enumerable.Empty<MetadataReference>()),
            new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                nullableContextOptions: NullableContextOptions.Enable));

        var errors = compilation.GetDiagnostics().Where(x => x.Severity == DiagnosticSeverity.Error).ToList();
        Assert.True(errors.Count == 0, "Fixture does not compile:\n" + string.Join("\n", errors));

        var options = new AnalyzerOptions(
            ImmutableArray<AdditionalText>.Empty,
            new BuildPropertyOptionsProvider(buildProperties ?? new Dictionary<string, string>()));
        return compilation
            .WithAnalyzers(Analyzers.Value, options)
            .GetAnalyzerDiagnosticsAsync()
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>The distinct diagnostic IDs in <paramref name="diagnostics"/>, ordinal-sorted.</summary>
    internal static string[] Ids(ImmutableArray<Diagnostic> diagnostics) =>
        diagnostics.Select(x => x.Id).Distinct().OrderBy(x => x, StringComparer.Ordinal).ToArray();

    private static ImmutableArray<MetadataReference> LoadReferences()
    {
        var trusted = (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!;
        return trusted
            .Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => (MetadataReference)MetadataReference.CreateFromFile(p))
            .Append(MetadataReference.CreateFromFile(typeof(Dungeon.Core.IntMath).Assembly.Location))
            .ToImmutableArray();
    }

    private static ImmutableArray<DiagnosticAnalyzer> LoadAnalyzers() =>
        typeof(Rules).Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract
                && typeof(DiagnosticAnalyzer).IsAssignableFrom(t)
                && t.GetCustomAttribute<DiagnosticAnalyzerAttribute>() is not null)
            .Select(t => (DiagnosticAnalyzer)Activator.CreateInstance(t)!)
            .ToImmutableArray();

    /// <summary>Serves test build properties as the global <c>build_property.*</c> options.</summary>
    private sealed class BuildPropertyOptionsProvider : AnalyzerConfigOptionsProvider
    {
        private readonly Options _global;

        internal BuildPropertyOptionsProvider(IReadOnlyDictionary<string, string> buildProperties) =>
            _global = new Options(buildProperties.ToDictionary(
                kv => "build_property." + kv.Key,
                kv => kv.Value,
                StringComparer.OrdinalIgnoreCase));

        public override AnalyzerConfigOptions GlobalOptions => _global;

        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => Options.Empty;

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => Options.Empty;
    }

    private sealed class Options : AnalyzerConfigOptions
    {
        internal static readonly Options Empty = new(new Dictionary<string, string>());

        private readonly Dictionary<string, string> _values;

        internal Options(Dictionary<string, string> values) => _values = values;

        public override bool TryGetValue(string key, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out string? value) =>
            _values.TryGetValue(key, out value);
    }
}
