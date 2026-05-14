using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectedButton;

    public void Show()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void ResumeButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        PauseManager.instance.UnpauseGame();
    }

    public void TitleScreenButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        PauseManager.instance.GoToTitleScreen();
    }
}
