using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmAudioScript : MonoBehaviour
{
    public float songBPM;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public float firstBeatOffset;
    public float endBeatOffset;

    public AudioClip[] easyClips;  
    public AudioClip[] normalClips; 
    public AudioClip[] hardClips;  

    private BeatSpawnerScript beatSpawnerScript;
    private BeatTargetScript beatTargetScript;
    private int nextBeat;
    private Coroutine muteRoutine;
    
    public AudioSource musicSource;
    
    IEnumerator TemporaryMute(float duration)
    {
        musicSource.mute = true;

        yield return new WaitForSeconds(duration);

        musicSource.mute = false;
        muteRoutine = null;
    }
   
    void Start()
    {
        firstBeatOffset -= 0.34f;
        
        musicSource = GetComponent<AudioSource>();
        
        // Load the correct clip based on difficulty
        AudioClip clipToPlay = normalClips[0]; // default
        
        if (DifficultyManager.Instance != null)
        {
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy)
            {
                clipToPlay = easyClips[0];
                songBPM = 45f;
                firstBeatOffset = 5f;
            }
                
            else if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard)
            {
                clipToPlay = hardClips[0];
                songBPM = 123;
                firstBeatOffset = 9.8f;
                endBeatOffset = 12f;
            }
        }
        
        musicSource.clip = clipToPlay;
        
        // Calculate the number of seconds in each beat
        secPerBeat = 60f / songBPM;

        // Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        // Start the music
        musicSource.Play();

        beatSpawnerScript = GameObject.Find("BeatSpawner").GetComponent<BeatSpawnerScript>();
    }

    void OnEnable()
    {
        BeatTargetScript.OnBeatFail += HandleBeatFail;
        BeatTargetScript.OnBeatSuccess += HandleBeatSuccess;
    }

    void OnDisable()
    {
        BeatTargetScript.OnBeatFail -= HandleBeatFail;
        BeatTargetScript.OnBeatSuccess -= HandleBeatSuccess;
    }

    public void HandleBeatSuccess()
    {
        if (muteRoutine != null) 
        {
            StopCoroutine(muteRoutine); 
            muteRoutine = null;
        }
        musicSource.mute = false;
    }

    private void HandleBeatFail()
    {
        if (muteRoutine != null)
        {
            StopCoroutine(muteRoutine);
            muteRoutine = null;
        }

        muteRoutine = StartCoroutine(TemporaryMute(10f));
    }

    void Update()
    {
        if (musicSource == null || musicSource.clip == null)
            return;

        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        if (songPosition >= musicSource.clip.length - endBeatOffset)
            return;

        songPositionInBeats = songPosition / secPerBeat;

        int currentBeat = Mathf.FloorToInt(songPositionInBeats);

        if (currentBeat >= nextBeat)
        {
            nextBeat += 1;
            beatSpawnerScript.SpawnObjectAtRandom();
            if (SceneManager.GetActiveScene().name == "CoOpScene")
            {
                beatSpawnerScript.SpawnObjectAtRandom();
            }
        }
    }
}