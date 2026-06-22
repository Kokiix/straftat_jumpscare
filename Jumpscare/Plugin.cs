using System.IO;
using BepInEx;
using ComputerysModdingUtilities;
using HarmonyLib;
using Jumpscare;
using ModMenu.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[assembly: StraftatMod(isVanillaCompatible: true)]

[BepInDependency(CustomLevelsReborn.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency(ModMenu.PluginInfo.guid, BepInDependency.DependencyFlags.SoftDependency)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class JumpscarePlugin : BaseUnityPlugin
{
    internal static JumpscarePlugin Instance;
    Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

    void Awake()
    {
        this.gameObject.hideFlags = HideFlags.HideAndDontSave;
        Instance = this;

        InitConfig();

        var CLRLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(CustomLevelsReborn.MyPluginInfo.PLUGIN_GUID);
        if (CLRLoaded) JumpscareTimer.SkipFirstScene = true;

        var ModMenuLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(ModMenu.PluginInfo.guid);
        if (ModMenuLoaded) InitInitModMenu();

        _harmony.PatchAll();
        SceneManager.sceneLoaded += JumpscareVideoPlayer.Init;
        SceneManager.sceneLoaded += JumpscareTimer.OnSceneLoad;
    }

    void InitConfig()
    {
        JumpscareTimer.MaxMinsBtwnJumpscares = Config.Bind("General", "Max Time Between Jumpscares (minutes)", 7.5f,
        "The jumpscare time is chosen between 0 min and this value. The timer only stops and resets when you exit to the main menu.");

        JumpscareTimer.MaxMinsBtwnJumpscares.SettingChanged += JumpscareTimer.RestartTimer;
    }

    void InitInitModMenu()
    {
        ModMenuCustomisation.SetPluginDescription("Play random jumpscares.");
        ModMenuCustomisation.RegisterContentBuilder(InitModMenu);
    }

    void InitModMenu(OptionListContext c)
    {
        c.AppendButton(
            "Jumpscare Video File Location",
            "Click to copy",
            () =>
            {
                GUIUtility.systemCopyBuffer = Path.GetDirectoryName(Info.Location);
                PauseManager.Instance.WriteOfflineLog("Copied path to clipboard!");
            });
        c.AppendTextBox("(The file must be exactly named \"jumpscare\".mp4)");
    }

    // Debug
    void OnDestroy()
    {
        _harmony.UnpatchSelf();
        SceneManager.sceneLoaded -= JumpscareVideoPlayer.Init;
        SceneManager.sceneLoaded -= JumpscareTimer.OnSceneLoad;
    }

    // Debug
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.T))
    //     {
    //         if (!JumpscareVideoPlayer.Player)
    //         {
    //             JumpscareVideoPlayer.Init();
    //         }
    //         JumpscareVideoPlayer.Player.Stop();
    //         JumpscareVideoPlayer.Player.Play();
    //     }
    // }
}