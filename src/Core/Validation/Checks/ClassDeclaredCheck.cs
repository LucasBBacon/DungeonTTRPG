using System.Collections.Immutable;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Validation.Checks;

/// <summary>
/// <c>V19.ClassDeclared</c>: every registered check declares one failure class (plan call 1). A row fails
/// when its class does not parse, when an <c>s</c>/<c>t</c> row's class is not <c>build</c> or a
/// <c>g</c>/<c>r</c> row's class is <c>build</c>, or when a <c>floorRetry</c> row has no <c>proofRef</c>.
/// </summary>
public sealed class ClassDeclaredCheck : IStaticCheck
{
    private const string ChecksFile = "registry/checks.json";

    /// <inheritdoc />
    public CheckId Id { get; } = new("V19.ClassDeclared");

    /// <inheritdoc />
    public ImmutableArray<string> Reads => [ChecksFile];

    /// <inheritdoc />
    public ImmutableArray<CheckFailure> Run(ContentSet content)
    {
        var failures = ImmutableArray.CreateBuilder<CheckFailure>();
        var rows = content.RecordsIn(ChecksFile);
        for (int i = 0; i < rows.Length; i++)
        {
            var row = (CheckRow)rows[i];
            string at = Item("$.checks", i);
            if (row.ParsedClass is not { } cls)
            {
                failures.Add(Fail(Member(at, "class"), $"\"{row.Class}\" is not a failure class."));
                continue;
            }

            bool buildPhase = row.Phase is Phase.S or Phase.T;
            if (buildPhase && cls.Kind != FailureKind.Build)
            {
                failures.Add(Fail(Member(at, "class"), $"a phase-{row.PhaseText} row fails the build, so its class is build, not {cls}."));
            }
            else if (!buildPhase && cls.Kind == FailureKind.Build)
            {
                failures.Add(Fail(Member(at, "class"), $"a phase-{row.PhaseText} row acts by its class, which cannot be build."));
            }

            if (cls.Kind == FailureKind.FloorRetry && row.ProofRef is null)
            {
                failures.Add(Fail(Member(at, "proofRef"), "a floorRetry row needs a proofRef."));
            }
        }

        return failures.ToImmutable();
    }

    private CheckFailure Fail(string field, string problem) => new(Id, ChecksFile, field, problem);
}
