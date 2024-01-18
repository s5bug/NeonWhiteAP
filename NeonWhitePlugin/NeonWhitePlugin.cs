using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace NeonWhitePlugin;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInProcess("Neon White.exe")]
public class NeonWhitePlugin : BaseUnityPlugin {
    public static ManualLogSource Log;
    public static Harmony Harmony = new(PluginInfo.PLUGIN_GUID);
    
    private void Awake() {
        Log = this.Logger;
        try {
            Harmony.PatchAll();
        } catch (Exception e) {
            Log.LogError(e);
        }
    }
}
