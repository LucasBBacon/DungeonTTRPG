using System.Text;
using Dungeon.Sim.Tools;

// Commands: `name-list` exports the naming-pass appendix to content/naming (owner ruling 2).
if (args.Length == 1 && args[0] == "name-list")
{
    string root = FindRepoRoot();
    var utf8 = new UTF8Encoding(false, throwOnInvalidBytes: true);
    string appendix = utf8.GetString(File.ReadAllBytes(
        Path.Combine(root, "docs", "superpowers", "specs", "2026-11-24-naming-pass-name-list.md")));
    var entries = NameListExporter.Parse(appendix);

    string naming = Path.Combine(root, "content", "naming");
    Directory.CreateDirectory(naming);
    File.WriteAllBytes(Path.Combine(naming, "name_list.json"), utf8.GetBytes(NameListExporter.ToNameListJson(entries)));
    File.WriteAllBytes(Path.Combine(naming, "retired_names.json"), utf8.GetBytes(NameListExporter.ToRetiredNamesJson(entries)));
    Console.WriteLine($"Wrote {entries.Length} entries and {NameListExporter.Retired(entries).Length} retired names to content/naming.");
    return 0;
}

if (args.Length > 0)
{
    Console.Error.WriteLine($"Unknown command: {string.Join(' ', args)}. Commands: name-list.");
    return 1;
}

Console.WriteLine("Dungeon.Sim: no simulations yet (M1-38).");
return 0;

// The folder above the current one that holds Dungeon.sln.
static string FindRepoRoot()
{
    for (var dir = new DirectoryInfo(Directory.GetCurrentDirectory()); dir is not null; dir = dir.Parent)
    {
        if (File.Exists(Path.Combine(dir.FullName, "Dungeon.sln")))
        {
            return dir.FullName;
        }
    }

    throw new InvalidOperationException("Dungeon.sln not found above " + Directory.GetCurrentDirectory());
}
