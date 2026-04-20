using BepInEx;
using BepInEx.Logging;

namespace ManyOptionsTest;

/// <summary>
/// The BepInEx plugin class of ManyOptionsTest.
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
