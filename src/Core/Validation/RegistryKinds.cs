using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Dungeon.Core.Content;
using static Dungeon.Core.Content.ContentJson;

namespace Dungeon.Core.Validation;

/// <summary>
/// The check registry, <c>registry/checks.json</c> (SV 9.1). Ids follow
/// <c>^V[1-9][0-9]*\.[A-Za-z][A-Za-z0-9]*$</c> or <c>^content(\.[A-Za-z0-9]+)+$</c>; <c>deps[]</c>,
/// <c>uses[]</c> and <c>proofRef</c> must name registered checks.
/// </summary>
public sealed class CheckKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "registry/checks.json";

    /// <inheritdoc />
    public override string ArrayName => "checks";

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.checks", ContentJson.Read<ChecksDto>(file).Checks);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.checks", i);
            var c = Need(file, at, rows[i]);
            result.Add(new CheckRow(
                file.Name,
                RegistryIds.Check(file, Member(at, "id"), c.Id),
                RuleString(file, Member(at, "owner"), c.Owner),
                ParseLowerEnum<Phase>(file, Member(at, "phase"), c.Phase),
                OptionalRuleString(file, Member(at, "point"), c.Point),
                RuleStrings(file, Member(at, "deps"), c.Deps),
                RuleStrings(file, Member(at, "uses"), c.Uses),
                RuleString(file, Member(at, "class"), c.Class),
                RuleString(file, Member(at, "action"), c.Action),
                OptionalRuleString(file, Member(at, "rerunSpan"), c.RerunSpan),
                OptionalRuleString(file, Member(at, "proofRef"), c.ProofRef),
                RuleString(file, Member(at, "wave"), c.Wave),
                RuleString(file, Member(at, "ciTier"), c.CiTier),
                c.BudgetMs));
        }

        return result.MoveToImmutable();
    }

    /// <inheritdoc />
    public override void Resolve(ContentSet set)
    {
        var rows = set.RecordsIn(FileName);
        for (int i = 0; i < rows.Length; i++)
        {
            var row = (CheckRow)rows[i];
            string at = Item("$.checks", i);
            NeedChecks(set, Member(at, "deps"), row.Deps);
            NeedChecks(set, Member(at, "uses"), row.Uses);
            if (row.ProofRef is { } proof)
            {
                NeedCheck(set, Member(at, "proofRef"), proof);
            }
        }
    }

    private void NeedChecks(ContentSet set, string field, ImmutableArray<string> ids)
    {
        for (int j = 0; j < ids.Length; j++)
        {
            NeedCheck(set, Item(field, j), ids[j]);
        }
    }

    private void NeedCheck(ContentSet set, string field, string id)
    {
        if (set.Find<CheckRow>(id) is null)
        {
            throw new ContentLoadException(FileName, field, $"\"{id}\" names no registered check.");
        }
    }

    private sealed class ChecksDto
    {
        [JsonPropertyName("checks")] public CheckDto?[]? Checks { get; set; }
    }

    private sealed class CheckDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("owner")] public string? Owner { get; set; }
        [JsonPropertyName("phase")] public string? Phase { get; set; }
        [JsonPropertyName("point")] public string? Point { get; set; }
        [JsonPropertyName("deps")] public string?[]? Deps { get; set; }
        [JsonPropertyName("uses")] public string?[]? Uses { get; set; }
        [JsonPropertyName("class")] public string? Class { get; set; }
        [JsonPropertyName("action")] public string? Action { get; set; }
        [JsonPropertyName("rerunSpan")] public string? RerunSpan { get; set; }
        [JsonPropertyName("proofRef")] public string? ProofRef { get; set; }
        [JsonPropertyName("wave")] public string? Wave { get; set; }
        [JsonPropertyName("ciTier")] public string? CiTier { get; set; }
        [JsonPropertyName("budgetMs")] public int? BudgetMs { get; set; }
    }
}

/// <summary>
/// The report registry, <c>registry/reports.json</c> (SV 9.1). Ids follow <c>^[A-Za-z][A-Za-z0-9.]*$</c>;
/// <c>arms[]</c> must name registered arms.
/// </summary>
public sealed class ReportKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "registry/reports.json";

    /// <inheritdoc />
    public override string ArrayName => "reports";

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.reports", ContentJson.Read<ReportsDto>(file).Reports);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.reports", i);
            var r = Need(file, at, rows[i]);
            result.Add(new ReportRow(
                file.Name,
                RegistryIds.Report(file, Member(at, "id"), r.Id),
                OptionalRuleString(file, Member(at, "family"), r.Family),
                RuleStrings(file, Member(at, "keys"), r.Keys),
                RuleString(file, Member(at, "owner"), r.Owner),
                RuleString(file, Member(at, "definition"), r.Definition),
                RuleString(file, Member(at, "unit"), r.Unit),
                ParseEnum<Instrument>(file, Member(at, "instrument"), r.Instrument),
                RuleStrings(file, Member(at, "arms"), r.Arms),
                RuleString(file, Member(at, "class"), r.Class),
                RationalField(file, Member(at, "threshold"), r.Threshold),
                ParseLowerEnum<ThresholdStatus>(file, Member(at, "thresholdStatus"), r.ThresholdStatus),
                ParseLowerEnum<Sided>(file, Member(at, "sided"), r.Sided),
                RationalField(file, Member(at, "z"), r.Z),
                RuleString(file, Member(at, "stat"), r.Stat),
                Need(file, Member(at, "N"), r.N),
                RuleString(file, Member(at, "wave"), r.Wave),
                RuleString(file, Member(at, "ciTier"), r.CiTier),
                OptionalRuleString(file, Member(at, "leverOrderRef"), r.LeverOrderRef)));
        }

        return result.MoveToImmutable();
    }

    /// <inheritdoc />
    public override void Resolve(ContentSet set)
    {
        var rows = set.RecordsIn(FileName);
        for (int i = 0; i < rows.Length; i++)
        {
            var row = (ReportRow)rows[i];
            string field = Member(Item("$.reports", i), "arms");
            for (int j = 0; j < row.Arms.Length; j++)
            {
                if (set.Find<ArmRow>(row.Arms[j]) is null)
                {
                    throw new ContentLoadException(FileName, Item(field, j), $"\"{row.Arms[j]}\" names no registered arm.");
                }
            }
        }
    }

    private sealed class ReportsDto
    {
        [JsonPropertyName("reports")] public ReportDto?[]? Reports { get; set; }
    }

    private sealed class ReportDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("family")] public string? Family { get; set; }
        [JsonPropertyName("keys")] public string?[]? Keys { get; set; }
        [JsonPropertyName("owner")] public string? Owner { get; set; }
        [JsonPropertyName("definition")] public string? Definition { get; set; }
        [JsonPropertyName("unit")] public string? Unit { get; set; }
        [JsonPropertyName("instrument")] public string? Instrument { get; set; }
        [JsonPropertyName("arms")] public string?[]? Arms { get; set; }
        [JsonPropertyName("class")] public string? Class { get; set; }
        [JsonPropertyName("threshold")] public string? Threshold { get; set; }
        [JsonPropertyName("thresholdStatus")] public string? ThresholdStatus { get; set; }
        [JsonPropertyName("sided")] public string? Sided { get; set; }
        [JsonPropertyName("z")] public string? Z { get; set; }
        [JsonPropertyName("stat")] public string? Stat { get; set; }
        [JsonPropertyName("N")] public int? N { get; set; }
        [JsonPropertyName("wave")] public string? Wave { get; set; }
        [JsonPropertyName("ciTier")] public string? CiTier { get; set; }
        [JsonPropertyName("leverOrderRef")] public string? LeverOrderRef { get; set; }
    }
}

/// <summary>The simulator-arm registry, <c>registry/arms.json</c> (SV 9.1). Ids follow <c>^[A-Za-z][A-Za-z0-9]*\.[A-Za-z0-9]+$</c>.</summary>
public sealed class ArmKind : ContentKind
{
    /// <inheritdoc />
    public override string FileName => "registry/arms.json";

    /// <inheritdoc />
    public override string ArrayName => "arms";

    /// <inheritdoc />
    public override ImmutableArray<ContentRecord> Read(ContentFile file)
    {
        var rows = Need(file, "$.arms", ContentJson.Read<ArmsDto>(file).Arms);
        var result = ImmutableArray.CreateBuilder<ContentRecord>(rows.Length);
        for (int i = 0; i < rows.Length; i++)
        {
            string at = Item("$.arms", i);
            var a = Need(file, at, rows[i]);
            result.Add(new ArmRow(
                file.Name,
                RegistryIds.Arm(file, Member(at, "id"), a.Id),
                RuleString(file, Member(at, "owner"), a.Owner),
                RuleString(file, Member(at, "configDelta"), a.ConfigDelta)));
        }

        return result.MoveToImmutable();
    }

    private sealed class ArmsDto
    {
        [JsonPropertyName("arms")] public ArmDto?[]? Arms { get; set; }
    }

    private sealed class ArmDto
    {
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("owner")] public string? Owner { get; set; }
        [JsonPropertyName("configDelta")] public string? ConfigDelta { get; set; }
    }
}

/// <summary>The registry id grammars (plan call 3); registry rows do not use the content-id rule.</summary>
internal static class RegistryIds
{
    private const string ContentPrefix = "content.";

    /// <summary>A required check id: <c>Vn.Name</c> or <c>content.Part.Part</c>.</summary>
    public static string Check(ContentFile file, string field, string? s) =>
        Valid(file, field, RuleString(file, field, s), IsCheck, "a check id (Vn.Name or content.Part)");

    /// <summary>A required report id: a letter, then letters, digits and dots.</summary>
    public static string Report(ContentFile file, string field, string? s) =>
        Valid(file, field, RuleString(file, field, s), IsReport, "a report id (a letter, then letters, digits and dots)");

    /// <summary>A required arm id: <c>Axis.Value</c>.</summary>
    public static string Arm(ContentFile file, string field, string? s) =>
        Valid(file, field, RuleString(file, field, s), IsArm, "an arm id (Axis.Value)");

    /// <summary><c>^V[1-9][0-9]*\.[A-Za-z][A-Za-z0-9]*$</c> or <c>^content(\.[A-Za-z0-9]+)+$</c>.</summary>
    public static bool IsCheck(string s)
    {
        if (s.StartsWith(ContentPrefix, StringComparison.Ordinal))
        {
            return AlnumParts(s.Substring(ContentPrefix.Length), maxParts: int.MaxValue);
        }

        int dot = s.IndexOf('.');
        if (s.Length < 2 || s[0] != 'V' || s[1] < '1' || s[1] > '9' || dot < 0)
        {
            return false;
        }

        for (int i = 2; i < dot; i++)
        {
            if (!IsDigit(s[i]))
            {
                return false;
            }
        }

        string name = s.Substring(dot + 1);
        return name.Length > 0 && IsLetter(name[0]) && AllAlnum(name);
    }

    /// <summary><c>^[A-Za-z][A-Za-z0-9]*\.[A-Za-z0-9]+$</c>.</summary>
    public static bool IsArm(string s) =>
        s.Length > 0 && IsLetter(s[0]) && s.Split('.').Length == 2 && AlnumParts(s, maxParts: 2);

    /// <summary><c>^[A-Za-z][A-Za-z0-9.]*$</c>.</summary>
    public static bool IsReport(string s)
    {
        if (s.Length == 0 || !IsLetter(s[0]))
        {
            return false;
        }

        foreach (var c in s)
        {
            if (!IsLetter(c) && !IsDigit(c) && c != '.')
            {
                return false;
            }
        }

        return true;
    }

    private static string Valid(ContentFile file, string field, string s, Func<string, bool> grammar, string what) =>
        grammar(s) ? s : throw new ContentLoadException(file.Name, field, $"\"{s}\" is not {what}.");

    // One to maxParts dot-separated parts, each a non-empty [A-Za-z0-9]+.
    private static bool AlnumParts(string s, int maxParts)
    {
        var parts = s.Split('.');
        if (parts.Length > maxParts)
        {
            return false;
        }

        foreach (var part in parts)
        {
            if (part.Length == 0 || !AllAlnum(part))
            {
                return false;
            }
        }

        return true;
    }

    private static bool AllAlnum(string s)
    {
        foreach (var c in s)
        {
            if (!IsLetter(c) && !IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsLetter(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');

    private static bool IsDigit(char c) => c >= '0' && c <= '9';
}
