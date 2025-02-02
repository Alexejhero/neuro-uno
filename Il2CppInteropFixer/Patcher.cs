using System.Reflection;
using BepInEx.Logging;
using BepInEx.Preloader.Core.Patching;
using HarmonyLib;
using JetBrains.Annotations;

namespace Il2CppInteropFixer;

[PatcherPluginInfo(Id, Id, "1.0.0")]
[UsedImplicitly]
public sealed class Patcher : BasePatcher
{
    private const string Id = "Il2CppInteropFixer";

    internal static ManualLogSource Logger;

    public override void Initialize()
    {
        Logger = Log;

        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Id);
    }
}
