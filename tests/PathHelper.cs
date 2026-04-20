namespace Template.Tests;

internal static class PathHelper
{
    public static string RootDir { get; } = GetRootDir();
    public static string TemplateContentDir { get; } =
        Path.Combine(RootDir, "src", "content");

    private static string GetRootDir()
    {
        string assemblyPath = typeof(BepInExTemplateTest).Assembly.Location;
        DirectoryInfo? rootDir = new FileInfo(assemblyPath).Directory;
        // iterate up the directory tree until we find where the solution lives (or we hit the file system root)
        while (
            rootDir != null
            && !File.Exists(Path.Combine(rootDir.FullName, "Template.slnx"))
        )
        {
            rootDir = rootDir.Parent;
        }
        string? rootPath = rootDir?.FullName;

        if (string.IsNullOrEmpty(rootPath))
        {
            throw new InvalidOperationException("The codebase root was not found");
        }
        return rootPath;
    }
}
