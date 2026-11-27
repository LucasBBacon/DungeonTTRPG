using System.Reflection;
using Xunit;

namespace Dungeon.Core.Tests;

public class ArchitectureTests
{
    private const BindingFlags Everything =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

    [Fact]
    public void Core_does_not_reference_Godot()
    {
        var refs = typeof(Dungeon.Core.CoreAssembly).Assembly.GetReferencedAssemblies();
        Assert.DoesNotContain(refs, r => r.Name!.Contains("Godot", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// DS11: <c>CheckForOverflowUnderflow</c> is on in Debug and CI. Tests run in Debug locally and in CI,
    /// so this fails if <c>build/Determinism.props</c> stops applying it to Core.
    /// </summary>
    [Fact]
    public void Core_is_built_with_checked_arithmetic()
    {
        Assert.Throws<OverflowException>(() => Dungeon.Core.IntMath.CheckedProbe(int.MaxValue, 1));
    }

    /// <summary>
    /// The determinism analyzer holds in Core without exceptions: no file switches a DUN rule off
    /// or suppresses a Determinism diagnostic (plan call 6; the last one went with PCG32).
    /// </summary>
    [Fact]
    public void Core_has_no_analyzer_suppressions()
    {
        var core = Path.Combine(RepoRoot.Find(), "src", "Core");
        var sep = Path.DirectorySeparatorChar;
        var offenders = Directory.EnumerateFiles(core, "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Contains($"{sep}obj{sep}", StringComparison.Ordinal)
                && !f.Contains($"{sep}bin{sep}", StringComparison.Ordinal))
            .Where(f =>
            {
                var text = File.ReadAllText(f);
                return text.Contains("#pragma warning disable DUN", StringComparison.Ordinal)
                    || text.Contains("SuppressMessage(\"Determinism\"", StringComparison.Ordinal);
            })
            .Select(f => Path.GetRelativePath(core, f))
            .ToList();
        Assert.Empty(offenders);
    }

    /// <summary>
    /// Global Constraints: integer rules only, the core's own random source, no unordered
    /// collections. Looks at every field, property, method signature and local variable in Core.
    /// </summary>
    [Fact]
    public void Core_uses_no_floating_point_no_system_random_and_no_unordered_collections()
    {
        var offenders = new List<string>();
        foreach (var type in typeof(Dungeon.Core.CoreAssembly).Assembly.GetTypes())
        {
            void Check(Type used, string where)
            {
                if (IsForbidden(used))
                {
                    offenders.Add($"{type.FullName}: {where} uses {used}");
                }
            }

            foreach (var f in type.GetFields(Everything))
            {
                Check(f.FieldType, $"field {f.Name}");
            }

            foreach (var p in type.GetProperties(Everything))
            {
                Check(p.PropertyType, $"property {p.Name}");
            }

            foreach (var m in type.GetMethods(Everything).Cast<MethodBase>().Concat(type.GetConstructors(Everything)))
            {
                if (m is MethodInfo info)
                {
                    Check(info.ReturnType, $"method {m.Name}");
                }

                foreach (var parameter in m.GetParameters())
                {
                    Check(parameter.ParameterType, $"method {m.Name}");
                }

                foreach (var local in m.GetMethodBody()?.LocalVariables ?? [])
                {
                    Check(local.LocalType, $"a local in {m.Name}");
                }
            }
        }

        Assert.Empty(offenders);
    }

    private static bool IsForbidden(Type t)
    {
        if (t.HasElementType)
        {
            return IsForbidden(t.GetElementType()!);
        }

        if (t == typeof(float) || t == typeof(double) || t == typeof(Random))
        {
            return true;
        }

        if (t.IsGenericType)
        {
            var open = t.GetGenericTypeDefinition();
            return open == typeof(Dictionary<,>) || open == typeof(HashSet<>)
                || t.GetGenericArguments().Any(IsForbidden);
        }

        return false;
    }
}
