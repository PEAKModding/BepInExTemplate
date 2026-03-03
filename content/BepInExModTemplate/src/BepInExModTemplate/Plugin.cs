using BepInEx;
using BepInEx.Logging;

namespace BepInExModTemplate;

//#if (!no-tutorial)
// Here are some basic resources on code style and naming conventions to help
// you in your first CSharp plugin!
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names
// https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces

// The BepInAutoPlugin attribute comes from the Hamunii.BepInEx.AutoPlugin
// NuGet package, and it will generate the BepInPlugin attribute for you!
// For more info, see https://github.com/Hamunii/BepInEx.AutoPlugin
//#endif

/// <summary>
/// The BepInEx plugin class of BepInExModTemplate.
/// </summary>
[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    private void Awake()
    {
//#if (!no-tutorial)
        // BepInEx gives us a logger which we can use to log information.
        // See https://lethal.wiki/dev/fundamentals/logging
//#endif
        Log = Logger;

//#if (!no-tutorial)
        // BepInEx also gives us a config file for easy configuration.
        // See https://lethal.wiki/dev/intermediate/custom-configs

        // We can apply our hooks here.
        // See https://lethal.wiki/dev/fundamentals/patching-code

        // Log our awake here so we can see it in LogOutput.log file
//#endif
        Log.LogInfo($"Plugin {Name} is loaded!");
    }
}
