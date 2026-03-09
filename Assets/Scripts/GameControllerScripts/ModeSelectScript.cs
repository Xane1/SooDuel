using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ModeSelectScreenScript : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame ||
            Gamepad.current?.buttonEast.wasPressedThisFrame == true)
        {
            GoBack();
        }
    }

    // SINGLEPLAYER MODE BUTTONS
    public void SinglePlayerControllerButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "ControllerScene");
        SceneManager.LoadScene("DifficultySelect");
    }

    public void SinglePlayerMouseButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "MouseScene");
        SceneManager.LoadScene("DifficultySelect");
    }

    // MULTIPLAYER MODE BUTTONS
    public void MultiplayerCoOpButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "CoOpScene");
        SceneManager.LoadScene("DifficultySelect");
    }

    public void MultiplayerVersusButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "VersusScene");
        SceneManager.LoadScene("DifficultySelect");
    }

    void GoBack()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}