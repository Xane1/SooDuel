using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public enum Stage { Ancient, Medieval, Present }
    public Stage CurrentStage = Stage.Present;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
