using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public PauseMenuScript pauseMenu;
    public bool isPaused = false;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseMenu.Show();
        //SwitchActionMap("UI");
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseMenu.gameObject.SetActive(false);
      //  SwitchActionMap("Player");
    }

    public void TogglePause()
    {
        if (isPaused) UnpauseGame();
        else PauseGame();
    }
    
    public void GoToTitleScreen()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("TitleScreen");
    }

    public void DisableAllCursors()
    {
        foreach (HybridCursor cursor in FindObjectsByType<HybridCursor>(FindObjectsSortMode.None))
        {
            cursor.enabled = false;
        }
    }
}
