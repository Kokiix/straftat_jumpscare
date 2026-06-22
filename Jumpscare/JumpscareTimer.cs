using System;
using System.Collections;
using BepInEx.Configuration;
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
            if (SkipFirstScene)
            {
                SkipFirstScene = false;
                return;
            }

            _timer = JumpscareOnDelay();
            JumpscarePlugin.Instance.StartCoroutine(_timer);
        }
    }

    internal static ConfigEntry<double> MaxMinsBtwnJumpscares;
    static IEnumerator JumpscareOnDelay()
    {
        yield return new WaitForSeconds(_rng.Next((int)Math.Round(MaxMinsBtwnJumpscares.Value * 60)) + 1);

        Debug.LogError("jumpscare time");
        JumpscareVideoPlayer.Player.Stop();
        JumpscareVideoPlayer.Player.Play();

        _timer = JumpscareOnDelay();
        JumpscarePlugin.Instance.StartCoroutine(_timer);
    }

    internal static void RestartTimer(object sender, EventArgs e)
    {
        JumpscarePlugin.Instance.StopCoroutine(_timer);
        _timer = JumpscareOnDelay();
        JumpscarePlugin.Instance.StartCoroutine(_timer);
    }
}