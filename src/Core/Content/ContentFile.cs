using System;

namespace Dungeon.Core.Content;

/// <summary>
/// One content file as text. <see cref="Name"/> is relative to <c>content/</c>, uses <c>/</c> and is
/// ASCII, such as <c>registry/checks.json</c>. Core is given text and a name, never a path; the host reads the disk.
/// </summary>
public sealed record ContentFile(string Name, string Text)
{
    /// <summary>The relative file name.</summary>
    public string Name { get; init; } = Name ?? throw new ArgumentNullException(nameof(Name));

    /// <summary>The file's JSON text.</summary>
    public string Text { get; init; } = Text ?? throw new ArgumentNullException(nameof(Text));
}
