using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

static class JumpscareTimer
{
    static internal bool SkipFirstScene = false;
    static System.Random _rng = new System.Random();
    static IEnumerator _timer;

    internal static void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            if (_timer != null)
                JumpscarePlugin.Instance.StopCoroutine(_timer);
            _timer = null;
        }
        else
        {
            Debug.LogError(mode);
            if (SkipFirstScene)
            {
                SkipFirstScene = false;
                return;
            }

            _timer = JumpscareOnDelay();
            JumpscarePlugin.Instance.StartCoroutine(_timer);
        }
    }

    const int MaxSecondDelayForJumpscare = 10; // TODO: convert to ConfigEntry
    static IEnumerator JumpscareOnDelay()
    {
        yield return new WaitForSeconds(_rng.Next(MaxSecondDelayForJumpscare + 1));

        Debug.LogError("jumpscare time");
        JumpscareVideoPlayer.Player.Stop();
        JumpscareVideoPlayer.Player.Play();

        _timer = JumpscareOnDelay();
        JumpscarePlugin.Instance.StartCoroutine(_timer);
    }
}