using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    public static PlayerReference LocalPlayer;
    private void Awake()
    {
        LocalPlayer = this;
    }
}
