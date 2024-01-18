using HarmonyLib;

namespace NeonWhiteAP.Patches;

[HarmonyPatch(typeof(LeaderboardIntegrationSteam))]
public class LeaderboardIntegrationSteamPatch {
    [HarmonyPrefix]
    [HarmonyPatch(nameof(LeaderboardIntegrationSteam.UploadScore_Internal))]
    private static bool UploadScore_Internal() {
        return false;
    }
}
