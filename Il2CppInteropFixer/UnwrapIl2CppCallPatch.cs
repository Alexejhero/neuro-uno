using System.Reflection;
using HarmonyLib;
using Il2CppInterop.Common.XrefScans;
using JetBrains.Annotations;

namespace Il2CppInteropFixer;

[HarmonyPatch]
internal static class UnwrapIl2CppCallPatch
{
    public static bool Enable { get; set; }

    [HarmonyTargetMethod, UsedImplicitly]
    private static MethodInfo TargetMethod() => AccessTools.Method("Il2CppInterop.Runtime.Injection.InjectorHelpers:GetIl2CppExport");

    [HarmonyPostfix, UsedImplicitly]
    private static void Postfix(ref nint __result, MethodBase __originalMethod)
    {
        if (!Enable) return;

        Patcher.Logger.LogInfo("Unwrapping call from method " + __originalMethod.Name);
        __result = XrefScannerLowLevel.JumpTargets(__result).ElementAt(1);
    }
}
