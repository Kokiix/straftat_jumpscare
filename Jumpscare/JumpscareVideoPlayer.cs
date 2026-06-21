using System.IO;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

static class JumpscareVideoPlayer
{
    internal static VideoPlayer Player;

    internal static void Init(Scene scene, LoadSceneMode mode)
    {
        if (!Player && scene.name == "MainMenu")
        {
            Init();
        }
    }

    // For debug
    internal static void Init()
    {
        var UI = Object.FindObjectOfType<PauseManager>().transform;

        var jumpscareGO = new GameObject("Jumpscare");
        jumpscareGO.transform.SetParent(UI);

        var rendTexture = new RenderTexture();

        var img = jumpscareGO.AddComponent<RawImage>();
        img.texture = rendTexture;

        Player = jumpscareGO.AddComponent<VideoPlayer>();
        Player.playOnAwake = false;
        Player.url = Path.Combine(Paths.PluginPath, "jumpscare.mp4");
        Player.renderMode = VideoRenderMode.RenderTexture;
        Player.targetTexture = rendTexture;
    }
}