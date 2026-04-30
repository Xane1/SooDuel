using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenShotScript : MonoBehaviour
{
    private int screenshotCount = 0;
    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            ScreenCapture.CaptureScreenshot($"Screenshot_{screenshotCount++}.png");
        }
    }
}
