using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using NeuroSdk;
using UnityEngine;
using CollectionExtensions = BepInEx.Unity.IL2CPP.Utils.Collections.CollectionExtensions;

namespace NeuroUno;

[BepInAutoPlugin, UsedImplicitly]
public sealed partial class Plugin : BasePlugin
{
    public override void Load()
    {
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Id);
        AddComponent<Test>();
    }
}

public class Test : MonoBehaviour
{
    public void Start()
    {
        NeuroSdkSetup.Initialize("Test");
    }
}
