using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace ieishi.mod.yotogimichi
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string HarmonyId = "ieishi.mod.yotogmichi";
        public Harmony Harmony { get; } = new Harmony(HarmonyId);

        public static Plugin Instance { get; private set; }
        public static Configuration Configuration { get; private set; }

        private void Awake()
        {
            Instance = this;
            Configuration = new Configuration();

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.PatchAll();
        }
    }
}
