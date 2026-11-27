using System.Text;
using Dungeon.Core.Content;

namespace Dungeon.Sim.Tools;

/// <summary>
/// Reads a content folder into <see cref="ContentFile"/>s: every <c>*.json</c> under it, named relative to it
/// with <c>/</c>, in ordinal order by name. Decoding is strict UTF-8 without a byte order mark, so invalid bytes
/// or a BOM fail loudly with the file named instead of being replaced or dropped (MAIN 6.5).
/// </summary>
public static class ContentDirectory
{
    private static readonly UTF8Encoding Strict = new(false, throwOnInvalidBytes: true);

    /// <summary>The content files under <paramref name="contentRoot"/>, ordinal by name.</summary>
    /// <exception cref="InvalidDataException">A file holds invalid UTF-8 or starts with a UTF-8 byte order mark.</exception>
    public static ContentFile[] Read(string contentRoot) =>
        Directory.EnumerateFiles(contentRoot, "*.json", SearchOption.AllDirectories)
            .Select(path => new ContentFile(Path.GetRelativePath(contentRoot, path).Replace('\\', '/'), Decode(path)))
            .OrderBy(f => f.Name, StringComparer.Ordinal)
            .ToArray();

    private static string Decode(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            throw new InvalidDataException($"{path}: starts with a UTF-8 byte order mark; content files are UTF-8 without a BOM.");
        }

        try
        {
            return Strict.GetString(bytes);
        }
        catch (DecoderFallbackException ex)
        {
            throw new InvalidDataException($"{path}: not valid UTF-8 ({ex.Message})", ex);
        }
    }
}
