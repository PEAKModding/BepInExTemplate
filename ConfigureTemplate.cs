#!/usr/bin/env dotnet

// Configure the following values:
var stringsToReplace = new Dictionary<string, string>()
{
    // Name from Steam directory
    { "_GameName_", "Lethal Company" },
    // Name with spaces removed
    { "_GameNameNoSpaces_", "LethalCompany" },
    // Used for template `shortName`. Determines the command for dotnet new, e.g. dotnet new lcmod
    { "_GameNameShortNoSpacesLowercase_", "lc" },
    // GitHub repo & NuGet package prefix
    { "_TemplateAuthorNoSpaces_", "LethalCompanyModding" },
    // See: <https://thunderstore.io/api/experimental/community/>
    { "_ThunderstoreGameIdentifier_", "lethal-company" },
};

// After configuration is done, execute this script with `dotnet run ConfigureTemplate.cs`.
// The rest of the script should be ignored.

var thisFileName = "ConfigureTemplate.cs";
var currentDir = AppContext.BaseDirectory;
if (!File.Exists(Path.Combine(currentDir, thisFileName)))
{
    throw new InvalidOperationException(
        $"Current directory '{currentDir}' must contain this script '{thisFileName}'"
    );
}

foreach (var filePath in Directory.EnumerateFiles("", "*", SearchOption.AllDirectories))
{
    var fileName = Path.GetFileName(filePath);
    if (fileName == thisFileName)
        continue;

    var originalText = File.ReadAllText(filePath);
    var text = originalText;
    foreach (var (from, to) in stringsToReplace)
    {
        text = text.Replace(from, to, StringComparison.Ordinal);
    }

    if (text != originalText)
    {
        var relativePath = Path.GetRelativePath(currentDir, filePath);
        Console.WriteLine($"Replaced strings in file '{relativePath}'");
        File.WriteAllText(filePath, text);
    }
}

Console.WriteLine($"Press any key to close");
Console.ReadKey(true);
