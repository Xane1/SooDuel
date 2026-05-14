using UnityEngine;

public class StunDisplay : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip dmgSoundClip;

    private AudioSource audioSource;

    public GameObject stunDisplay;
    private void Awake()
    {
        animator = GetComponent<Animator>();
       
        audioSource = GetComponent<AudioSource>();
        
        audioSource.clip = dmgSoundClip;
        audioSource.Play();
        
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
