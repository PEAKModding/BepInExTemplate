#!/usr/bin/env dotnet

// Configure the following values:

// Name from Steam directory
const string GameName = "Lethal Company";

// Name with spaces removed. Used for MSBuild properties & readme example
const string GameNameNoSpaces = "LethalCompany";

// Used for template `shortName`. Determines the command for dotnet new, e.g. dotnet new lcmod
const string GameNameShortNoSpacesLowercase = "lc";

// The GitHub account name of the owner of the repo
const string TemplateRepoAuthorNoSpaces = "LethalCompanyModding";

// These combine into NuGet package id, e.g. LethalCompanyModding.BepInExTemplate
const string TemplateNuGetPackagePrefixNoSpaces = TemplateRepoAuthorNoSpaces;
const string TemplatePackageNameNoSpaces = "BepInExTemplate";

// Metadata for NuGet package for which repository the package points to
const string TemplatePackageProjectUrl =
    $"https://github.com/{TemplateRepoAuthorNoSpaces}/{TemplatePackageNameNoSpaces}";

// See: <https://thunderstore.io/api/experimental/community/>
const string ThunderstoreGameIdentifier = "lethal-company";

// The BepInEx package for the Thunderstore community
const string BepInExPackFullName = "BepInEx-BepInExPack";
const string BepInExPackVersion = "5.4.2305";

// A valid TFM https://learn.microsoft.com/en-us/dotnet/standard/frameworks#supported-target-frameworks
const string PluginTargetFramework = "netstandard2.1";

// After configuration is done, execute this script with `dotnet run ConfigureTemplate.cs`.
// The rest of the script should be ignored.
// ================================================================================================

var stringsToReplace = new Dictionary<string, string>()
{
    { "_GameName_", GameName },
    { "_GameNameNoSpaces_", GameNameNoSpaces },
    { "_GameNameShortNoSpacesLowercase_", GameNameShortNoSpacesLowercase },
    { "_TemplateRepoAuthorNoSpaces_", TemplateRepoAuthorNoSpaces },
    { "_TemplateNuGetPackagePrefixNoSpaces_", TemplateNuGetPackagePrefixNoSpaces },
    { "_TemplatePackageProjectUrl_", TemplatePackageProjectUrl },
    { "_TemplatePackageNameNoSpaces_", TemplatePackageNameNoSpaces },
    { "_ThunderstoreGameIdentifier_", ThunderstoreGameIdentifier },
    { "_BepInExPackFullName_", BepInExPackFullName },
    { "_BepInExPackVersion_", BepInExPackVersion },
};

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

    if (fileName == "BepInExModTemplate.csproj")
    {
        // We want the BepInExTemplateBase to successfully build the plugin to ease testing,
        // which is why we do this like so.
        text = text.Replace(
            "<TargetFramework>netstandard2.1</TargetFramework>",
            $"<TargetFramework>{PluginTargetFramework}</TargetFramework>",
            StringComparison.Ordinal
        );
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
