using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleScreenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    public void MousePlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MouseScene");
    }

    public void ControllerPlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ControllerScene");
    }
    public void CoOpPlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CoOpScene");
    }
    public void VersusPlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VersusScene");
    }
}
