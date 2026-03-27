using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerReference : MonoBehaviour
{
    public static PlayerReference LocalPlayer;

    [SerializeField] private GameObject AttackObject;
    [SerializeField] private GameObject AttackIndicator;
    private void Awake()
    {
        LocalPlayer = this;
        if (SceneManager.GetActiveScene().name == "CoOpScene")
        {
            Destroy(AttackObject);
            Destroy(AttackIndicator);
        }
    }
}
