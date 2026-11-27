using System;

namespace Dungeon.Core.Determinism;

/// <summary>
/// Marks an <c>IComparer&lt;T&gt;</c> whose order is total over the values it sorts (DS4).
/// Sorts in Core must use one (DS11, DUN007).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class TotalComparerAttribute : Attribute
{
}
