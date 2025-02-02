using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace NeuroUno;

[BepInAutoPlugin]
public sealed partial class Plugin : BasePlugin
{
    public override void Load()
    {
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Id);
    }
}
