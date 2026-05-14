using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

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
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SingleplayerModeSelectScreen");
    }

    public void MultiPlayerButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MultiplayerModeSelectScreen");
    }

    public void MultiplayerVersusButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "VersusScene");
        SceneManager.LoadScene("StageSelect");
    }


    public void CoOpPlayButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "CoOpScene");
        SceneManager.LoadScene("DifficultySelect");
    }


    public void QuitButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Application.Quit();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame ||
            (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            GoBack();
        }
    }
    
    void GoBack()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}