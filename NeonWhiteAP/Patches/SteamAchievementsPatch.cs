using HarmonyLib;

namespace NeonWhiteAP.Patches;

[HarmonyPatch(typeof(SteamAchievements))]
public class SteamAchievementsPatch {
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SteamAchievements.StoreStats))]
    private static bool OnSetVisible() {
        return false;
    }
}
