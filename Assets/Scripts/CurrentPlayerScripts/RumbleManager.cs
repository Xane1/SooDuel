using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration, Gamepad pad)
    {
        if (pad != null)
        {
            pad.SetMotorSpeeds(lowFrequency, highFrequency);
            StartCoroutine(StopRumbleAfterTime(duration, pad));
        }
    }

    private IEnumerator StopRumbleAfterTime(float duration, Gamepad pad)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0f, 0f);
    }
}