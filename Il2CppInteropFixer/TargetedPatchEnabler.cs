using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;

namespace Il2CppInteropFixer;

[HarmonyPatch]
public static class TargetedPatchEnabler
{
    [HarmonyTargetMethods, UsedImplicitly]
    private static IEnumerable<MethodInfo> TargetMethods() => Enumerable.Select(
    [
        "Il2CppInterop.Runtime.Injection.Hooks.Class_FromIl2CppType_Hook:FindTargetMethod",
        "Il2CppInterop.Runtime.Injection.Hooks.Class_FromName_Hook:FindTargetMethod",
        "Il2CppInterop.Runtime.Injection.Hooks.Class_GetFieldDefaultValue_Hook:FindClassGetFieldDefaultValueXref",
        "Il2CppInterop.Runtime.Injection.Hooks.GenericMethod_GetMethod_Hook:FindTargetMethod",
        "Il2CppInterop.Runtime.Injection.Hooks.MetadataCache_GetTypeInfoFromTypeDefinitionIndex_Hook:FindGetTypeInfoFromTypeDefinitionIndex",
    ], n => AccessTools.Method(n));

    [HarmonyPrefix, UsedImplicitly]
    private static void Prefix()
    {
        UnwrapIl2CppCallPatch.Enable = true;
    }

    [HarmonyPostfix, UsedImplicitly]
    private static void Postfix()
    {
        UnwrapIl2CppCallPatch.Enable = false;
    }
}
