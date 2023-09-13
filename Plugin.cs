using BepInEx;
using HarmonyLib;

namespace ieishi.mod.yotogimichi
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string HarmonyId = "ieishi.mod.yotogmichi";
        public Harmony Harmony { get; } = new Harmony(HarmonyId);

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.PatchAll();
        }
    }
}
