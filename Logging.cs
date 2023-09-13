using BepInEx.Logging;

namespace ieishi.mod.yotogimichi
{
    public static class Logging
    {
        public static void Log(object data, LogLevel level = LogLevel.Info)
        {
            var log = new ManualLogSource(PluginInfo.PLUGIN_NAME); // The source name is shown in BepInEx log
            Logger.Sources.Add(log);
            log.Log(level, data);
            Logger.Sources.Remove(log);
        }
    }
}
