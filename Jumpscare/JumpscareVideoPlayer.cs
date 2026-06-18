using UnityEngine;

static class JumpscareVideoPlayer
{
    static void Init()
    {
        var rootGO = new GameObject("JumpscareCanvas");

        var canvas = rootGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999;

        var imageGO = new GameObject("RawImage");
        // imageGO.AddComponent<Image
    }
}