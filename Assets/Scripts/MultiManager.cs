using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MultiManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public MultiplayerGameManager versusGameManager;
    public CoOpMultiplayerGameManager coopGameManager;

    public GameObject versusSongs;
    public GameObject coopSongs;
    

    public GameObject versusPlayerManager;
    public GameObject coopPlayerManager;
    void Start()
    {
        if (MultiplayerModeManager.Instance.CurrentMultiplayerMode == MultiplayerModeManager.MultiplayerMode.CoOp)
        {
            coopGameManager.enabled = true;
            versusGameManager.enabled = false;

            coopSongs.SetActive(true);
            versusSongs.SetActive(false);
            
            
            coopPlayerManager.SetActive(true);
            versusPlayerManager.SetActive(false);
        }
        
        else if (MultiplayerModeManager.Instance.CurrentMultiplayerMode == MultiplayerModeManager.MultiplayerMode.Versus)
        {
            versusGameManager.enabled = true;
            coopGameManager.enabled = false;
            
            versusSongs.SetActive(true);
            coopSongs.SetActive(false);
            
            versusPlayerManager.SetActive(true);
            coopPlayerManager.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
