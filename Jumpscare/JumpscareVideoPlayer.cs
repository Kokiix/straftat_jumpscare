using System.IO;
using BepInEx;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

static class JumpscareVideoPlayer
{
    internal static VideoPlayer Player;

    static void Init()
    {
        var rootGO = new GameObject("JumpscareVideoPlayer");
        Object.DontDestroyOnLoad(rootGO);

        var panelSettings = ScriptableObject.CreateInstance<PanelSettings>();
        panelSettings.sortingOrder = 9999;
        panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        panelSettings.referenceResolution = new Vector2Int(1920, 1080);

        Player = rootGO.AddComponent<VideoPlayer>();
        Player.url = Path.Combine(Paths.PluginPath, "jumpscare.webm");
        Player.renderMode = VideoRenderMode.RenderTexture;
        var renderTexture = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
        Player.targetTexture = renderTexture;

        var videoElement = new VisualElement();
        // temp
        videoElement.style.width = 250;
        videoElement.style.height = 250;
        videoElement.pickingMode = PickingMode.Ignore;
        videoElement.style.backgroundImage = new StyleBackground(Background.FromRenderTexture(renderTexture));

        var uiDoc = rootGO.AddComponent<UIDocument>();
        uiDoc.panelSettings = panelSettings;

        var rootVisual = uiDoc.rootVisualElement;
        rootVisual.style.width = Length.Percent(100);
        rootVisual.style.height = Length.Percent(100);
        rootVisual.style.alignItems = Align.Center;
        rootVisual.style.justifyContent = Justify.Center;
        rootVisual.Add(videoElement);
    }
}