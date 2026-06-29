using UnityEngine;

public class MultiplayerModeManager : MonoBehaviour
{
    public static MultiplayerModeManager Instance;
    public enum MultiplayerMode {CoOp, Versus};
    public MultiplayerMode CurrentMultiplayerMode = MultiplayerMode.Versus;
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
