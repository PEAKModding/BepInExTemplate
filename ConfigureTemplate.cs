#!/usr/bin/env dotnet
#:property PublishAot=false

// Configure the following values:

// Name from Steam directory
const string GameName = "PEAK";

// Name with spaces removed. Used for MSBuild properties & readme example
const string GameNameNoSpaces = "PEAK";

// Used for template `shortName`. Determines the command for dotnet new, e.g. dotnet new lcmod
const string GameNameShortNoSpacesLowercase = "peak";

// A comma-separated list of the Template NuGet package authors
const string TemplateAuthors = "Robyn, Hamunii";

// The GitHub account name of the owner of the repo
const string TemplateRepoAuthorNoSpaces = "PEAKModding";

// These combine into NuGet package id, e.g. LethalCompanyModding.BepInExTemplate
const string TemplateNuGetPackagePrefixNoSpaces = TemplateRepoAuthorNoSpaces;
const string TemplatePackageNameNoSpaces = "BepInExTemplate";

// Metadata for NuGet package for which repository the package points to
const string TemplatePackageProjectUrl =
    $"https://github.com/{TemplateRepoAuthorNoSpaces}/{TemplatePackageNameNoSpaces}";

// See: <https://thunderstore.io/api/experimental/community/>
const string ThunderstoreGameIdentifier = "peak";

// The BepInEx package for the Thunderstore community
const string BepInExPackFullName = "BepInEx-BepInExPack_PEAK";
const string BepInExPackVersion = "5.4.75301";

// A valid TFM https://learn.microsoft.com/en-us/dotnet/standard/frameworks#supported-target-frameworks
const string PluginTargetFramework = "netstandard2.1";

// NuGet GameLibs package as a fallback if local references aren't found or used
const string GameLibsPackage = "UnityEngine.Modules";
const string GameLibsVersion = "6000.0.36";

// After configuration is done, execute this script with `dotnet run ConfigureTemplate.cs`.
// The rest of the script should be ignored.
// ================================================================================================

var stringsToReplace = new Dictionary<string, string>(StringComparer.Ordinal)
{
    { "_GameName_", GameName },
    { "_GameNameNoSpaces_", GameNameNoSpaces },
    { "_GameNameShortNoSpacesLowercase_", GameNameShortNoSpacesLowercase },
    { "_TemplateAuthors_", TemplateAuthors },
    { "_TemplateRepoAuthorNoSpaces_", TemplateRepoAuthorNoSpaces },
    { "_TemplateNuGetPackagePrefixNoSpaces_", TemplateNuGetPackagePrefixNoSpaces },
    { "_TemplatePackageNameNoSpaces_", TemplatePackageNameNoSpaces },
    { "_TemplatePackageProjectUrl_", TemplatePackageProjectUrl },
    { "_ThunderstoreGameIdentifier_", ThunderstoreGameIdentifier },
    { "_BepInExPackFullName_", BepInExPackFullName },
    { "_BepInExPackVersion_", BepInExPackVersion },
    {
        "<PackageReference Include=\"UnityEngine.Modules\" Version=\"6000.0.36\" PrivateAssets=\"all\" />",
        $"<PackageReference Include=\"{GameLibsPackage}\" Version=\"{GameLibsVersion}\" PrivateAssets=\"all\" />"
    },
};

var thisFileName = "ConfigureTemplate.cs";
var dir = Directory.GetCurrentDirectory();
if (!File.Exists(Path.Combine(dir, thisFileName)))
{
    throw new InvalidOperationException(
        $"Current directory '{dir}' must contain this script '{thisFileName}'"
    );
}

RenameFileSystemEntriesRecursive(Path.Combine(dir, "tests"));

var src = Directory.EnumerateFiles(Path.Combine(dir, "src"), "*", SearchOption.AllDirectories);
var tests = Directory.EnumerateFiles(Path.Combine(dir, "tests"), "*", SearchOption.AllDirectories);

foreach (var filePath in src.Concat(tests).Concat([Path.Combine(dir, "README.md")]))
{
    var fileName = Path.GetFileName(filePath);

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
        var relativePath = Path.GetRelativePath(dir, filePath);
        Console.WriteLine($"Replaced strings in file '{relativePath}'");
        File.WriteAllText(filePath, text);
    }
}

void RenameFileSystemEntriesRecursive(string path)
{
    var subDir = Directory.EnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
    foreach (var entryPath in subDir)
    {
        var entryName = Path.GetFileName(entryPath);

        var newEntryName = entryName;
        foreach (var (from, to) in stringsToReplace)
        {
            newEntryName = newEntryName.Replace(from, to, StringComparison.Ordinal);
        }
        var newEntryPath = Path.Combine(Path.GetDirectoryName(entryPath) ?? "", newEntryName);

        if (newEntryPath != entryPath)
        {
            var relativePath = Path.GetRelativePath(dir, entryPath);
            Console.WriteLine($"Renamed filesystem entry '{relativePath}' => '{newEntryName}'");

            if (File.Exists(entryPath))
                MoveOrMergeOverwrite(entryPath, newEntryPath);
            else
                MoveOrMergeOverwrite(entryPath, newEntryPath);
        }

        if (Directory.Exists(entryPath))
        {
            RenameFileSystemEntriesRecursive(newEntryPath);
        }
    }
}

void MoveOrMergeOverwrite(string sourceDirName, string destDirName)
{
    if (!Directory.Exists(destDirName))
    {
        Directory.Move(sourceDirName, destDirName);
        return;
    }

    foreach (var file in Directory.EnumerateFiles(sourceDirName))
    {
        var fileName = Path.GetFileName(file);
        var dest = Path.Combine(destDirName, fileName);
        File.Move(file, dest, overwrite: true);
    }

    foreach (var dir in Directory.EnumerateDirectories(sourceDirName))
    {
        var dirName = Path.GetFileName(dir);
        MoveOrMergeOverwrite(dir, Path.Combine(destDirName, dirName));
    }

    Directory.Delete(sourceDirName);
}

Console.WriteLine($"Press any key to close");
Console.ReadKey(true);
