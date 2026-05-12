using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MapSelector : MonoBehaviour
{
    private string targetScene;

    void Start()
    {
        if (MapManager.Instance == null)
        {
            var managerObject = new GameObject("MapManager");
            managerObject.AddComponent<MapManager>();
        }
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
        SceneManager.LoadScene("TitleScreen");
    }

    public void SelectAncient()
    {
        MapManager.Instance.CurrentStage = MapManager.Stage.Ancient;
        SceneManager.LoadScene("DifficultySelect");
    }

    public void SelectMedieval()
    {  
        MapManager.Instance.CurrentStage = MapManager.Stage.Medieval;
        SceneManager.LoadScene("DifficultySelect");
    }

    public void SelectPresent()
    {
        MapManager.Instance.CurrentStage = MapManager.Stage.Present;
        SceneManager.LoadScene("DifficultySelect");
    }
}