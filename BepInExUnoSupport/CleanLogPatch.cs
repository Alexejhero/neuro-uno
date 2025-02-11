using BepInEx.Core.Logging.Interpolation;
using BepInEx.Logging;
using HarmonyLib;

namespace BepInExUnoSupport;

[HarmonyPatch]
internal static class CleanLogPatch
{
    [HarmonyPatch(typeof(ManualLogSource), nameof(ManualLogSource.LogWarning), typeof(BepInExWarningLogInterpolatedStringHandler))]
    [HarmonyPrefix]
    private static bool Prefix(BepInExWarningLogInterpolatedStringHandler logHandler)
    {
        string log = logHandler.ToString()!;
        return !log.Contains("Failed to preload") || !log.Contains("Newtonsoft.Json.dll");
    }
}
