using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleScreenScript : MonoBehaviour
{
    private void Start()
    {
        // Ensure SimpleDifficultyManager exists
        if (DifficultyManager.Instance == null)
        {
            var managerObject = new GameObject("SimpleDifficultyManager");
            managerObject.AddComponent<DifficultyManager>();
        }
    }

    public void SinglePlayerButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SingleplayerModeSelectScreen");
    }
    
    public void MultiPlayerButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MultiplayerModeSelectScreen");
    }
    public void MultiplayerVersusButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "VersusScene");
        SceneManager.LoadScene("StageSelect");
    }

    public void MousePlayButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "MouseScene");
        SceneManager.LoadScene("DifficultySelect");
    }

    public void ControllerPlayButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "ControllerScene");
        SceneManager.LoadScene("DifficultySelect");
    }

    public void CoOpPlayButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "CoOpScene");
        SceneManager.LoadScene("DifficultySelect");
    }

    public void VersusPlayButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "VersusScene");
        SceneManager.LoadScene("StageSelect");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            GoBack();
        }
    }

    void GoBack()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}