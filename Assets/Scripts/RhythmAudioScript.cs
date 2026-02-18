using System.Collections;
using UnityEngine;

public class RhythmAudioScript : MonoBehaviour
{
    public float songBPM;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public float firstBeatOffset;
    public float endBeatOffset;

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
        
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
        
        //Calculate the number of seconds in each beat
        secPerBeat =  60f / songBPM;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play(); 

        
        beatSpawnerScript = GameObject.Find("BeatSpawner").GetComponent<BeatSpawnerScript>();
    
    }

    // Update is called once per frame
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
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        if(songPosition >= musicSource.clip.length - endBeatOffset)
            return;
        
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        
        int currentBeat = Mathf.FloorToInt(songPositionInBeats);
        
        if (currentBeat >= nextBeat)
        {
            nextBeat += 1;
            beatSpawnerScript.SpawnObjectAtRandom();
        }
    }
}
