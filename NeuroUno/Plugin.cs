using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using NeuroSdk;
using UnityEngine;

namespace NeuroUno;

[BepInAutoPlugin, UsedImplicitly]
public sealed partial class Plugin : BasePlugin
{
    public override void Load()
    {
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Id);
        NeuroSdkSetup.Initialize("Uno");
    }
}
