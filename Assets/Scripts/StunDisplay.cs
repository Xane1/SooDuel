using UnityEngine;

public class StunDisplay : MonoBehaviour
{
    private Animator animator;

    public GameObject stunDisplay;
    private void Awake()
    {
        animator = GetComponent<Animator>();
       // gameObject.SetActive(false);
    }

    public void BeginStun()
    {
        stunDisplay.SetActive(true);
        
        gameObject.SetActive(false);
    }
    
    public void PlayStun()
    {
        gameObject.SetActive(true);
    }
    
    public void OnStunComplete()
    {
        gameObject.SetActive(false);
    }
}
