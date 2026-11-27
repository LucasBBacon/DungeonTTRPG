using System.Collections.Immutable;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation;

/// <summary>
/// A content kind whose records reach the screen. The naming checks (<c>V7.NameRegistry</c> and V24) read
/// its <see cref="NamedRecords"/>. No repository kind is one in M1-04; the owning steps add them.
/// </summary>
public interface INamedRecordSource
{
    /// <summary>This kind's named records in <paramref name="set"/>, in file order.</summary>
    ImmutableArray<NamedRecord> NamedRecords(ContentSet set);
}
