using Xunit;

namespace Dungeon.Analyzers.Tests;

/// <summary>DUN001: floating point is banned in Core (DS11).</summary>
public class FloatingPointTests
{
    [Fact]
    public void Double_field_is_reported()
    {
        var d = Harness.Run("class C { double _x; public double X => _x; }");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(d));
    }

    [Fact]
    public void Float_parameter_is_reported()
    {
        var d = Harness.Run("class C { public static void M(float f) { } }");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(d));
    }

    [Fact]
    public void Decimal_local_is_reported()
    {
        var d = Harness.Run("class C { public static void M() { decimal m = 0; _ = m; } }");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(d));
    }

    [Fact]
    public void Var_bound_to_double_literal_is_reported()
    {
        var d = Harness.Run("class C { public static void M() { var x = 1.5; _ = x; } }");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(d));
    }

    [Fact]
    public void Var_bound_to_Math_Sqrt_is_reported()
    {
        var d = Harness.Run("class C { public static void M() { var r = System.Math.Sqrt(4); _ = r; } }");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(d));
    }

    [Fact]
    public void Cast_of_double_literal_to_int_is_reported()
    {
        var d = Harness.Run("class C { public static int M() { int n = (int)2.5; return n; } }");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(d));
    }

    [Fact]
    public void Delegate_signature_is_reported()
    {
        var returns = Harness.Run("public delegate double D1();");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(returns));
        var takes = Harness.Run("public delegate void D2(float f);");
        Assert.Equal(new[] { "DUN001" }, Harness.Ids(takes));
    }

    [Fact]
    public void Message_names_DS11_and_the_type()
    {
        var d = Harness.Run("class C { double _x; public double X => _x; }");
        Assert.Contains(d, x => x.Id == "DUN001");
        var message = d.First(x => x.Id == "DUN001").GetMessage();
        Assert.Contains("DS11", message);
        Assert.Contains("double", message);
    }
}
