using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using NeuroSdk;
using NeuroSdk.Actions;
using NeuroSdk.Il2Cpp;
using NeuroUno.Actions;
using UnityEngine;

namespace NeuroUno;

[BepInAutoPlugin, UsedImplicitly]
public sealed partial class Plugin : BasePlugin
{
    public override void Load()
    {
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Id);
        NeuroSdkSetup.Initialize("Uno");

        AddComponent<test>();
    }
}

[RegisterInIl2Cpp]
public class test : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            NeuroActionHandler.RegisterActions(new PlayCardAction(), new CallUnoAction());
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            NeuroActionHandler.UnregisterActions(new PlayCardAction(), new CallUnoAction());
        }
    }
}
