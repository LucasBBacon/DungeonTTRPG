using Dungeon.Sim.Tools;
using Xunit;

namespace Dungeon.Core.Tests;

/// <summary>
/// <see cref="ContentDirectory.Read"/> names files relative to the root with <c>/</c>, orders them ordinally,
/// and decodes strict UTF-8 so corrupt content fails loudly instead of being hashed (MAIN 6.5).
/// </summary>
public sealed class ContentDirectoryTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "dungeon-content-" + Guid.NewGuid().ToString("N"));

    public ContentDirectoryTests() => Directory.CreateDirectory(_root);

    public void Dispose() => Directory.Delete(_root, recursive: true);

    [Fact]
    public void Reads_json_files_named_with_slashes_in_ordinal_order()
    {
        Write("strings/en.json", "{\"a\": \"é\"}"u8.ToArray());
        Write("registry/checks.json", "{}"u8.ToArray());
        Write("Zeta.json", "{}"u8.ToArray());
        Write("notes.txt", "ignored"u8.ToArray());

        var files = ContentDirectory.Read(_root);

        Assert.Equal(["Zeta.json", "registry/checks.json", "strings/en.json"], files.Select(f => f.Name).ToArray());
        Assert.Equal("{\"a\": \"é\"}", files[2].Text);
    }

    [Fact]
    public void Invalid_utf8_fails_naming_the_file()
    {
        Write("strings/en.json", [(byte)'{', 0xC3, 0x28, (byte)'}']);

        var ex = Assert.Throws<InvalidDataException>(() => ContentDirectory.Read(_root));

        Assert.Contains("en.json", ex.Message);
        Assert.Contains("not valid UTF-8", ex.Message);
    }

    [Fact]
    public void Byte_order_mark_fails_naming_the_file()
    {
        Write("registry/checks.json", [0xEF, 0xBB, 0xBF, (byte)'{', (byte)'}']);

        var ex = Assert.Throws<InvalidDataException>(() => ContentDirectory.Read(_root));

        Assert.Contains("checks.json", ex.Message);
        Assert.Contains("byte order mark", ex.Message);
    }

    private void Write(string name, byte[] bytes)
    {
        string path = Path.Combine(_root, name);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllBytes(path, bytes);
    }
}
