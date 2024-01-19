using HarmonyLib;

namespace NeonWhiteAP.Patches;

[HarmonyPatch(typeof(SteamAchievements))]
public class SteamAchievementsPatch {
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SteamAchievements.Initialize))]
    private static bool Initialize(ref bool __result) {
        __result = false;
        return false;
    }
}
