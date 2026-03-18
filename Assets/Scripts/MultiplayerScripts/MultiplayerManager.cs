using UnityEngine;
using UnityEngine.InputSystem;
public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private GameObject newPlayerPrefab;
    public PlayerInputManager inputManager;

    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
        PlayerInputManager.instance.DisableJoining(); //Disables joining after player 1 is in
    }

    public void EnableJoining() //Called after tutorial is disabled and player 1 is in
    {
        PlayerInputManager.instance.EnableJoining();
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        if (player.playerIndex == 0)
        {
            PlayerInputManager.instance.DisableJoining(); //Blocks simultaneous joins
            PlayerInputManager.instance.playerPrefab = newPlayerPrefab; //Swaps to P2 prefab
            PlayerInputManager.instance.EnableJoining();
        }
        else if (player.playerIndex == 1)
        {
            PlayerInputManager.instance.DisableJoining(); //Disables joining after both are in
        }
    }
}