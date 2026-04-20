using BepInEx;
using BepInEx.Logging;

namespace NoTutorialAndGitIncludeTest;

/// <summary>
/// The BepInEx plugin class of NoTutorialAndGitIncludeTest.
/// </summary>
[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    private void Awake()
    {
        Log = Logger;

        Log.LogInfo($"Plugin {Name} is loaded!");
    }
}
