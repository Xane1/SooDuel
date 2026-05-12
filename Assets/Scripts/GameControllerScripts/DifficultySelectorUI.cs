using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DifficultySelectorUI : MonoBehaviour
{
    private string targetScene;

    void Start()
    {
        if (DifficultyManager.Instance == null)
        {
            var managerObject = new GameObject("DifficultyManager");
            managerObject.AddComponent<DifficultyManager>();
        }

        // Get the scene name that was passed in
        targetScene = PlayerPrefs.GetString("TargetScene", "ControllerScene");
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current?.buttonEast.wasPressedThisFrame == true)
        {
            GoBack();
        }
    }

    private void GoBack()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void SelectEasy()
    {
        DifficultyManager.Instance.CurrentDifficulty = DifficultyManager.Difficulty.Easy;
        SceneManager.LoadScene(targetScene);
    }

    public void SelectNormal()
    {
        DifficultyManager.Instance.CurrentDifficulty = DifficultyManager.Difficulty.Normal;
        SceneManager.LoadScene(targetScene);
    }

    public void SelectHard()
    {
        DifficultyManager.Instance.CurrentDifficulty = DifficultyManager.Difficulty.Hard;
        SceneManager.LoadScene(targetScene);
    }
}