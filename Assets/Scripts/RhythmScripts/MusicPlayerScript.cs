using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    public AudioSource musicSource;
    
    public AudioClip[] easyClips;   
    public AudioClip[] normalClips; 
    public AudioClip[] hardClips;   
    

    void Start()
    {
        //firstBeatOffset -= 0.34f;
        
        musicSource = GetComponent<AudioSource>();
        
        // Loads the correct clip based on difficulty
        AudioClip clipToPlay = normalClips[0]; // default
        
        if (DifficultyManager.Instance != null)
        {
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy)
                clipToPlay = easyClips[0];
            else if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard)
                clipToPlay = hardClips[0];
        }
        
        musicSource.clip = clipToPlay;
        musicSource.Play(); 
    }
}