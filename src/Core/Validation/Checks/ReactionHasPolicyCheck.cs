using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Content;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>content.ABL145.ReactionHasPolicy</c> (AB 14.5: "a reaction has no automatic policy"; AB 7.3): a reaction record
/// needs one of the automatic policies. It fails a reaction whose policy is missing or <c>askMe</c> (the party's prompt,
/// not an automatic policy), a <c>whilePoolAtLeastHalf</c> reaction with no <c>pool</c> to measure, and an
/// <c>ifItChangesOutcome</c> reaction with a zero bonus, which could never change an outcome. It reads
/// <c>abilities/abilities.json</c>; every reaction there counts, whatever its unit.
/// </summary>
public sealed class ReactionHasPolicyCheck : IStaticCheck
{
    private const string File = "abilities/abilities.json";

    /// <inheritdoc />
    public CheckId Id { get; } = new("content.ABL145.ReactionHasPolicy");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [File];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        var records = content.RecordsIn(File);
        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is not AbilityDefinition { Reaction: { } reaction } ability)
            {
                continue;
            }

            string at = ContentJson.Item("$.abilities", i);
            string policy = ContentJson.Member(ContentJson.Member(at, "reaction"), "policy");
            switch (reaction.Policy)
            {
                case null or ReactionMode.AskMe:
                    failures.Add(new CheckFailure(
                        Id, File, policy, $"Reaction \"{ability.Id}\" has no automatic policy; give it always, ifItChangesOutcome, whilePoolAtLeastHalf or never (AB 7.3, 14.5)."));
                    break;
                case ReactionMode.WhilePoolAtLeastHalf when ability.Pool is null:
                    failures.Add(new CheckFailure(
                        Id, File, ContentJson.Member(at, "pool"), $"Reaction \"{ability.Id}\" uses whilePoolAtLeastHalf and spends no pool, so it has nothing to measure (AB 7.3)."));
                    break;
                case ReactionMode.IfItChangesOutcome when reaction.Bonus == 0:
                    failures.Add(new CheckFailure(
                        Id, File, ContentJson.Member(ContentJson.Member(at, "reaction"), "bonus"), $"Reaction \"{ability.Id}\" uses ifItChangesOutcome with a zero bonus, which never changes an outcome (AB 7.3)."));
                    break;
            }
        }

        return failures.ToImmutable();
    }
}
