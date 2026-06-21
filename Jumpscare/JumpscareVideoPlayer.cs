using System.IO;
using BepInEx;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

static class JumpscareVideoPlayer
{
    internal static VideoPlayer Player;

    internal static void Init()
    {
        var rootGO = new GameObject("JumpscareVideoPlayer");
        Object.DontDestroyOnLoad(rootGO);

        Player = rootGO.AddComponent<VideoPlayer>();
        Player.playOnAwake = false;
        Player.renderMode = VideoRenderMode.CameraFarPlane;
        Player.url = Path.Combine(Paths.PluginPath, "jumpscare.mp4");
        Debug.LogError("initialized");
    }
}