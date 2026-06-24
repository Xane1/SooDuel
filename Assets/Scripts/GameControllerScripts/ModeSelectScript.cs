using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class ModeSelectScreenScript : MonoBehaviour
{
    
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current?.buttonEast.wasPressedThisFrame == true)
        {
            GoBack();
        }
    }

    // SINGLEPLAYER MODE BUTTONS
    public void SinglePlayerControllerButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "ControllerScene");
        PlayerPrefs.SetString("SelectedButton", "Controller");
        SceneManager.LoadScene("Tutorial Screen");
    }

    public void SinglePlayerMouseButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "MouseScene");
        PlayerPrefs.SetString("SelectedButton", "Mouse");
        SceneManager.LoadScene("Tutorial Screen");
    }

    // MULTIPLAYER MODE BUTTONS
    public void MultiplayerCoOpButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "CoOpScene");
        SceneManager.LoadScene("StageSelect");
    }

    public void MultiplayerVersusButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "VersusScene");
        SceneManager.LoadScene("StageSelect");
    }

    void GoBack()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}