using UnityEngine;
using UnityEngine.InputSystem;

//PLACE ON PLAYER MANAGER OBJECT
public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private GameObject newPlayerPrefab; // The new Player Prefab to use.

    public PlayerInputManager inputManager;

    private void Update()
    {
        if (GameObject.FindWithTag("Player1")) //Checking for a game object with the tag
        {
            PlayerInputManager.instance.playerPrefab = newPlayerPrefab; //If yes, changes the player prefab field to your selected prefab
        }
    }
}