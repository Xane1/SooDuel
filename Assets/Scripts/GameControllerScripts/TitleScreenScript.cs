using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;

public class TitleScreenScript : MonoBehaviour
{
    public GameObject credits;
    public GameObject ButtonText;
    
    private void Start()
    {
        // Ensure DifficultyManager exists
        if (DifficultyManager.Instance == null)
        {
            var managerObject = new GameObject("SimpleDifficultyManager");
            managerObject.AddComponent<DifficultyManager>();
        }

        if (MultiplayerModeManager.Instance == null)
        {
            var managerObject = new GameObject("MultiplayerModeManager");
            managerObject.AddComponent<MultiplayerModeManager>();
        }
        StartCoroutine(ResetButtonVisuals());
    }
    public void SinglePlayerButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "SingleplayerUscene");
        PlayerPrefs.SetString("SelectedButton", "Controller");
        SceneManager.LoadScene("Tutorial Screen");
    }

    private IEnumerator ResetButtonVisuals()
    {
        foreach (var button in FindObjectsOfType<Selectable>())
        {
            button.OnPointerEnter(new PointerEventData(EventSystem.current));
            yield return null;
            button.OnPointerExit(new PointerEventData(EventSystem.current));
        }
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
        PlayerPrefs.SetString("SelectedButton", "Versus");
        MultiplayerModeManager.Instance.CurrentMultiplayerMode = MultiplayerModeManager.MultiplayerMode.Versus;
        SceneManager.LoadScene("Tutorial Screen");
    }


    public void CoOpPlayButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        PlayerPrefs.SetString("TargetScene", "VersusScene");
        PlayerPrefs.SetString("SelectedButton", "CoOp");
        MultiplayerModeManager.Instance.CurrentMultiplayerMode = MultiplayerModeManager.MultiplayerMode.CoOp;
        SceneManager.LoadScene("Tutorial Screen");
    }

    public void CreditsButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
        credits.SetActive(true);
        ButtonText.SetActive(false);
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
        ButtonText.SetActive(true);
    }
}