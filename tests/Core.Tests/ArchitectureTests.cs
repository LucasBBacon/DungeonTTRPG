using Xunit;

namespace Dungeon.Core.Tests;

public class ArchitectureTests
{
    [Fact]
    public void Core_does_not_reference_Godot()
    {
        var refs = typeof(Dungeon.Core.CoreAssembly).Assembly.GetReferencedAssemblies();
        Assert.DoesNotContain(refs, r => r.Name!.Contains("Godot", StringComparison.OrdinalIgnoreCase));
    }
}
