using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    public AudioSource musicSource;
    
   // public float songBPM;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public float firstBeatOffset;
    public float endBeatOffset;
    void Start()
    {
        firstBeatOffset -= 0.34f;
        
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
        
        //Calculate the number of seconds in each beat
      //  secPerBeat =  60f / songBPM;

        //Record the time when the music starts
    //    dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play(); 
    }

    // Update is called once per frame
 /*   void Update()
    {

        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        if (songPosition >= musicSource.clip.length - endBeatOffset)
            return;

        //determine how many beats since the song started
    //    songPositionInBeats = songPosition / secPerBeat;

        int currentBeat = Mathf.FloorToInt(songPositionInBeats);
    } */
}
