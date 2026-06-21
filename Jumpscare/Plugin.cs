using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using ComputerysModdingUtilities;
using HarmonyLib;
using HarmonyLib.Tools;
using Jumpscare;
using UnityEngine;
using UnityEngine.SceneManagement;

[assembly: StraftatMod(isVanillaCompatible: true)]

[BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class JumpscarePlugin : BaseUnityPlugin
{
    internal static JumpscarePlugin Instance;
    Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

    void Awake()
    {
        this.gameObject.hideFlags = HideFlags.HideAndDontSave;
        Instance = this;

        _harmony.PatchAll();
        SceneManager.sceneLoaded += JumpscareVideoPlayer.Init;
        SceneManager.sceneLoaded += JumpscareTimer.OnSceneLoad;
    }

    void OnDestroy()
    {
        _harmony.UnpatchSelf();
        SceneManager.sceneLoaded -= JumpscareVideoPlayer.Init;
        SceneManager.sceneLoaded -= JumpscareTimer.OnSceneLoad;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!JumpscareVideoPlayer.Player)
            {
                JumpscareVideoPlayer.Init();
            }
            JumpscareVideoPlayer.Player.Stop();
            JumpscareVideoPlayer.Player.Play();
        }
    }
}