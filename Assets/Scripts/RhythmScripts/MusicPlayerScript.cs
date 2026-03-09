using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    public AudioSource musicSource;
    
    public AudioClip[] easyClips;   // Drag your 2 easy audio files here
    public AudioClip[] normalClips; // Drag your 2 normal audio files here
    public AudioClip[] hardClips;   // Drag your 2 hard audio files here
    
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public float firstBeatOffset;
    public float endBeatOffset;

    void Start()
    {
        firstBeatOffset -= 0.34f;
        
        musicSource = GetComponent<AudioSource>();
        
        // Load the correct clip based on difficulty
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