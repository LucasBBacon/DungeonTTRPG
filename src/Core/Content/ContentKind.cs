using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Conditions;
using Dungeon.Core.Effects;
using Dungeon.Core.Validation;

namespace Dungeon.Core.Content;

/// <summary>
/// One kind of content: the single file it is read from and how its records are read and checked.
/// Every kind is optional; a kind whose file is absent has no records.
/// </summary>
public abstract class ContentKind
{
    /// <summary>The exact relative file name, such as <c>registry/checks.json</c>.</summary>
    public abstract string FileName { get; }

    /// <summary>The name of the file's top-level record array, used in error paths such as <c>$.checks[1].id</c>.</summary>
    public abstract string ArrayName { get; }

    /// <summary>The name of the property that holds a record's id, used when a repeated id is named; <c>id</c> by default.</summary>
    public virtual string IdField => "id";

    /// <summary>True if generation reads this kind, so it is part of <c>genContentHash</c> (SV 4.6).</summary>
    public virtual bool ReadByGeneration => false;

    /// <summary>
    /// True if this kind's record ids follow the content-id grammar (<see cref="ContentId"/>, K-rule 3), so they may
    /// enter a key as <c>LabelId(id)</c> and <c>V19.LabelCollision</c> scans them; registry and naming ids keep their own grammars.
    /// </summary>
    public virtual bool IdsAreContentIds => false;

    /// <summary>Reads the file's records in file order. Throws <see cref="ContentLoadException"/> naming the field.</summary>
    public abstract ImmutableArray<ContentRecord> Read(ContentFile file);

    /// <summary>
    /// Checks this kind's references once every file has loaded. Throws <see cref="ContentLoadException"/>
    /// naming the referring file and field. The default has no references.
    /// </summary>
    public virtual void Resolve(ContentSet set)
    {
    }
}

/// <summary>The registered content kinds.</summary>
public static class ContentKinds
{
    /// <summary>Every kind the loader knows, in ordinal file-name order.</summary>
    public static ImmutableArray<ContentKind> All { get; } =
        [
            new AbilitiesKind(),
            new ConditionsKind(),
            new EffectsKind(),
            new GreyedReasonKind(),
            new NameListKind(),
            new RetiredNamesKind(),
            new ArmKind(),
            new CheckKind(),
            new ReportKind(),
            new StreamKind(),
            new StringsKind(),
        ];
}
