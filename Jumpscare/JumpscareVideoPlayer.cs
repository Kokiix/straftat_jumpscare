using System.IO;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

static class JumpscareVideoPlayer
{
    static RawImage Image;
    internal static VideoPlayer Player;

    internal static void Init(Scene scene, LoadSceneMode mode)
    {
        if (!Player && scene.name == "MainMenu")
        {
            Init();
        }
    }

    // Overload for debug
    internal static void Init()
    {
        var UI = Object.FindObjectOfType<PauseManager>().transform;

        var jumpscareGO = new GameObject("Jumpscare");
        jumpscareGO.transform.SetParent(UI, false);

        // var rendTexture = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);
        var rendTexture = new RenderTexture(1920, 1080, 0);
        rendTexture.Create();

        Image = jumpscareGO.AddComponent<RawImage>();
        Image.texture = rendTexture;
        var imgTransform = Image.GetComponent<RectTransform>();
        imgTransform.anchorMin = Vector2.zero;
        imgTransform.anchorMax = Vector2.one;
        imgTransform.offsetMin = Vector2.zero;
        imgTransform.offsetMax = Vector2.zero;
        Image.enabled = false;

        Player = jumpscareGO.AddComponent<VideoPlayer>();
        Player.playOnAwake = false;
        Player.url = Path.Combine(Path.Combine(Paths.PluginPath, "DEVELOPMENT-BUILD-Jumpscare Mod"), "jumpscare.mp4"); // TODO: use other path method
        Player.renderMode = VideoRenderMode.RenderTexture;
        Player.targetTexture = rendTexture;
        Player.started += (VideoPlayer src) => Image.enabled = true;
        Player.loopPointReached += (VideoPlayer src) => Image.enabled = false;
    }
}